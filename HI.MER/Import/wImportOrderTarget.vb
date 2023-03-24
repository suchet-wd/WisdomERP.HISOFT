Imports System
Imports System.Data
Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Data.OleDb
Imports System.Linq
Imports System.Collections.Generic
Imports System.Windows
Imports System.Windows.Forms
Imports System.Windows.Forms.Control
Imports System.Reflection
Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Configuration
Imports System.Globalization
Imports System.Threading
Imports System.Diagnostics
Imports DevExpress
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Columns

Imports DevExpress.Utils
Imports DevExpress.XtraEditors.ButtonEdit
Imports DevExpress.XtraEditors.SimpleButton
Imports System.Drawing
Imports Microsoft.Win32.SafeHandles
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Interaction
Imports System.Security.AccessControl
'Imports DevExpress.XtraSpreadsheet
'Imports DevExpress.Spreadsheet

Public Class wImportOrderTarget

#Region "Variable Declaration"
    Private Const _DBEnum As Integer = HI.Conn.DB.DataBaseName.DB_MERCHAN

    '...form dialog confirm map size not exists in system master file
    Private _wMapSizeImportOrder As wMapSizeImportOrder
    Private _wMapColorSizeData As wMapColorAndSizeImportOrderTarget

    Private _bInitialGridBandView As Boolean
    Private _bLoadFirstTime As Boolean = True

    Private tSql As String
    Private _SystemFilePath As String = Application.StartupPath & IIf(Microsoft.VisualBasic.Right(Application.StartupPath, 1) = "\", "", "\") & "Order"

    Private oDBdtImport As System.Data.DataTable
    Private oDBdtSrcOrder As System.Data.DataTable
    Private oDBdtMapSize As System.Data.DataTable
    Private oDBdtStyle As System.Data.DataTable
    Private _oDBdtUserImportMapSizeManual As System.Data.DataTable

    Private oGridViewSrcOrder As DevExpress.XtraGrid.Views.Grid.GridView
    Private oGridViewConfirmImport As DevExpress.XtraGrid.Views.Grid.GridView

    Private _bCatchSrcFile As Boolean

    Private startTime As DateTime

    Private _oSplash As HI.TL.SplashScreen = Nothing
    Private _tSplashText As String

    '...validate file is opened
    Private Declare Function CreateFile Lib "kernel32" Alias "CreateFileA" (ByVal lpFileName As String, _
                                                                            ByVal dwDesiredAccess As Long, _
                                                                            ByVal dwShareMode As Long, _
                                                                            ByVal lpSecurityAttributes As Long, _
                                                                            ByVal dwCreationDisposition As Long, _
                                                                            ByVal dwFlagsAndAttributes As Long, _
                                                                            ByVal hTemplateFile As Long) As Long

    Private Declare Function CloseHandle Lib "kernel32" (ByVal hFile As Long) As Long

#End Region


#Region "Method"
    ' Method
    Shared Function FileIsOpen4(ByVal pathfile As String) As Boolean
        Const GENERIC_READ As Long = &H80000000
        Const INVALID_HANDLE_VALUE As Long = -1
        Const OPEN_EXISTING As Long = 3
        Const FILE_ATTRIBUTE_NORMAL As Long = &H80
        Dim hFile As Long

        If System.IO.File.Exists(pathfile) Then
            Try
                ' note that FILE_ATTRIBUTE_NORMAL (&H80) has a different value than VB's constant vbNormal (0)!
                hFile = CreateFile(pathfile, GENERIC_READ, 0, 0, OPEN_EXISTING, FILE_ATTRIBUTE_NORMAL, 0&)
                ' this will evaluate to either -1 (File in use) or 0 (File free)
                Return hFile = INVALID_HANDLE_VALUE
            Catch ex As Exception
                MessageBox.Show(ex.ToString)
            Finally
                CloseHandle(hFile)
            End Try
        Else
            Return True
        End If
    End Function

    <DllImport("kernel32.dll", CharSet:=CharSet.Auto, SetLastError:=True)> _
    Private Shared Function CreateFile(lpFileName As String, dwDesiredAccess As FileSystemRights, dwShareMode As FileShare, securityAttrs As IntPtr, dwCreationDisposition As FileMode, dwFlagsAndAttributes As FileOptions, _
    hTemplateFile As IntPtr) As SafeFileHandle
    End Function

    Const ERROR_SHARING_VIOLATION As Integer = 32

    Private Function IsFileInUse(fileName As String) As Boolean
        Dim inUse As Boolean = False

        Dim fileHandle As SafeFileHandle = CreateFile(fileName, FileSystemRights.Modify, FileShare.Write, IntPtr.Zero, FileMode.OpenOrCreate, FileOptions.None, IntPtr.Zero)

        If fileHandle.IsInvalid Then
            If Marshal.GetLastWin32Error() = ERROR_SHARING_VIOLATION Then
                inUse = True
            End If
        End If

        fileHandle.Close()

        Return inUse

    End Function

#End Region

#Region "Property"

    Private _SysMerTeamId As Integer = 0
    Property SysMerTeamId As Integer
        Get
            Return _SysMerTeamId
        End Get
        Set(value As Integer)
            _SysMerTeamId = value
        End Set
    End Property
    Private _ProcComplete As Boolean = False
    Public Property ProcComplete As Boolean
        Get
            Return _ProcComplete
        End Get
        Set(ByVal value As Boolean)
            _ProcComplete = value
        End Set
    End Property

    Private _bPromptImport As Boolean = False
    Public Property bPromptImport As Boolean
        Get
            Return bPromptImport
        End Get
        Set(value As Boolean)
            _bPromptImport = value
        End Set
    End Property

#End Region

#Region "Method"

    Private Shared Function W_PRCbValidateFileIsOpen(ByVal ptPathfile As String) As Boolean
        Const GENERIC_READ As Long = &H80000000
        Const INVALID_HANDLE_VALUE As Long = -1
        Const OPEN_EXISTING As Long = 3
        Const FILE_ATTRIBUTE_NORMAL As Long = &H80
        Dim hFile As Long

        If System.IO.File.Exists(ptPathfile) Then
            Try
                ' note that FILE_ATTRIBUTE_NORMAL (&H80) has a different value than VB's constant vbNormal (0)!
                hFile = CreateFile(ptPathfile, GENERIC_READ, 0, 0, OPEN_EXISTING, FILE_ATTRIBUTE_NORMAL, 0&)
                ' this will evaluate to either -1 (File in use) or 0 (File free)
                Return hFile = INVALID_HANDLE_VALUE
            Catch ex As Exception
                MessageBox.Show(ex.ToString)
                Return False
            Finally
                CloseHandle(hFile)
            End Try
        Else
            Return True
        End If

    End Function

#End Region


#Region "Procedure"

    Private Function CheckNewStyle() As Boolean

        Try
            Dim _Qry As String = ""
            Dim _FNHSysStyleId As Integer = Val(HI.TL.RunID.GetRunNoID("TMERMStyle", "FNHSysStyleId", Conn.DB.DataBaseName.DB_MASTER).ToString())
            _FNHSysStyleId = _FNHSysStyleId - 1

            _Qry = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle(FNHSysStyleId, FTStyleCode, FTStyleNameTH, FTStyleNameEN, FTRemark, FTStateActive, FNHSysCustId,FNCMDisPer)"
            _Qry &= vbCrLf & " SELECT " & _FNHSysStyleId & "  + Row_Number() Over (Order By FTStyleMCode ) AS FNHSysStyleId"
            _Qry &= vbCrLf & ",FTStyleMCode AS FTStyleCode"
            _Qry &= vbCrLf & ",FTStyleMCode AS FTStyleNameTH"
            _Qry &= vbCrLf & ",FTStyleMCode AS FTStyleNameEN "
            _Qry &= vbCrLf & "	,'' AS FTRemark "
            _Qry &= vbCrLf & ",'1' AS FTStateActive"
            _Qry &= vbCrLf & "," & Integer.Parse(Val(FNHSysCustId.Properties.Tag.ToString)) & " AS FNHSysCustId,2.0 AS FNCMDisPer"
            _Qry &= vbCrLf & "  FROM"
            _Qry &= vbCrLf & " (SELECT A.FTPONo, A.FTSeason, A.FTStyle, A.FNGrpNo, S.FTSeasonCode,A.FNHSysSeasonId, A.FTStyle + S.FTSeasonCode AS FTStyleMCode"
            _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempImportOrderTargetPMC AS A WITH(NOLOCK) INNER JOIN"
            _Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMSeason AS S WITH(NOLOCK)  ON A.FNHSysSeasonId = S.FNHSysSeasonId"
            _Qry &= vbCrLf & " WHERE  (A.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "')"
            _Qry &= vbCrLf & "  ) AS A LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS B WITH(NOLOCK)  ON A.FTStyleMCode = B.FTStyleCode"
            _Qry &= vbCrLf & "     WHERE B.FTStyleCode Is NULL"
            _Qry &= vbCrLf & " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTStyle ( FNHSysStyleId, FTStyleCode, FTStyleNameTH, FTStyleNameEN, FNHSysCustId, FNHSysSeasonId, FTStateActive) "
            _Qry &= vbCrLf & "  SELECT B.FNHSysStyleId,B.FTStyleCode,B.FTStyleNameTH ,B.FTStyleNameEN," & Integer.Parse(Val(FNHSysCustId.Properties.Tag.ToString)) & " AS FNHSysCustId , A.FNHSysSeasonId ,'1'"
            _Qry &= vbCrLf & " FROM "
            _Qry &= vbCrLf & " 	 (SELECT A.FTPONo, A.FTSeason, A.FTStyle, A.FNGrpNo, S.FTSeasonCode,A.FNHSysSeasonId, A.FTStyle + S.FTSeasonCode AS FTStyleMCode"
            _Qry &= vbCrLf & "  FROM      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempImportOrderTargetPMC AS A WITH(NOLOCK) INNER JOIN"
            _Qry &= vbCrLf & "     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMSeason AS S WITH(NOLOCK)  ON A.FNHSysSeasonId = S.FNHSysSeasonId"
            _Qry &= vbCrLf & " WHERE  (A.FTUserLogin =N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "')"
            _Qry &= vbCrLf & "  ) AS A LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS B WITH(NOLOCK)  ON A.FTStyleMCode = B.FTStyleCode                                          "
            _Qry &= vbCrLf & "  WHERE NOT(B.FTStyleCode IN (SELECT FTStyleCode FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTStyle  ))"
            _Qry &= vbCrLf & "  UPDATE A SET FNHSysStyleId=B.FNHSysStyleId"
            _Qry &= vbCrLf & "  FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempImportOrderTargetPMC AS A "
            _Qry &= vbCrLf & "  INNER JOIN ( SELECT FTPONo,FTSeason,FTStyle,FNGrpNo,B.FNHSysStyleId"
            _Qry &= vbCrLf & "   FROM"
            _Qry &= vbCrLf & "  (SELECT A.FTPONo, A.FTSeason, A.FTStyle, A.FNGrpNo, S.FTSeasonCode,A.FNHSysSeasonId, A.FTStyle + S.FTSeasonCode AS FTStyleMCode"
            _Qry &= vbCrLf & "  FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempImportOrderTargetPMC AS A WITH(NOLOCK) INNER JOIN"
            _Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMSeason AS S WITH(NOLOCK)  ON A.FNHSysSeasonId = S.FNHSysSeasonId"
            _Qry &= vbCrLf & "  WHERE  (A.FTUserLogin =N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "')"
            _Qry &= vbCrLf & "  ) AS A LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS B WITH(NOLOCK)  ON A.FTStyleMCode = B.FTStyleCode"
            _Qry &= vbCrLf & " ) AS B ON A.FTPONo=B.FTPONo AND A.FTStyle =B.FTStyle AND A.FNGrpNo=B.FNGrpNo"
            _Qry &= vbCrLf & "  WHERE (A.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "')"

            HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

        Catch ex As Exception
            Return False
        End Try
        Return True

    End Function

    Private Function CheckColorWay() As Boolean
        Try
            Dim _Qry As String = ""
            Dim _FNHSysMatColorSeq As Integer = Integer.Parse(Val(HI.Conn.SQLConn.GetField("SELECT TOP 1 FNMatColorSeq FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMatColor WITH(NOLOCK)  ORDER BY FNMatColorSeq DESC", Conn.DB.DataBaseName.DB_MASTER, "0")))
            Dim _FNHSysMatColorId As Integer = Val(HI.TL.RunID.GetRunNoID("TMERMMatColor", "FNHSysMatColorId", Conn.DB.DataBaseName.DB_MASTER).ToString())

            If _FNHSysMatColorSeq > 0 Then
                _FNHSysMatColorSeq = _FNHSysMatColorSeq - 1
            Else
                _FNHSysMatColorSeq = 0
            End If

            If _FNHSysMatColorId > 0 Then
                _FNHSysMatColorId = _FNHSysMatColorId - 1
            Else
                _FNHSysMatColorId = 0
            End If

            _Qry = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMatColor(FNHSysMatColorId, FTMatColorCode, FNMatColorSeq, FTMatColorNameTH, FTMatColorNameEN, FTRemark, FTStateActive)"
            _Qry &= vbCrLf & " SELECT   " & _FNHSysMatColorId & "  + Row_Number() Over (Order By FTColor )  AS FNHSysMatColorId "
            _Qry &= vbCrLf & " ,FTColor AS FTMatColorCode"
            _Qry &= vbCrLf & " ," & _FNHSysMatColorSeq & "  + Row_Number() Over (Order By FTColor ) AS FNMatColorSeq"
            _Qry &= vbCrLf & " ,FTColor AS FTMatColorNameTH"
            _Qry &= vbCrLf & " ,FTColor AS FTMatColorNameEN"
            _Qry &= vbCrLf & "	 ,'' AS FTRemark"
            _Qry &= vbCrLf & " ,'1' AS FTStateActive"
            _Qry &= vbCrLf & " FROM (SELECT A.FTColor"
            _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempImportOrderTargetPMC_D AS A WITH(NOLOCK) LEFT JOIN"
            _Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMatColor AS MC WITH(NOLOCK) ON A.FTColor = MC.FTMatColorCode"
            _Qry &= vbCrLf & " WHERE  (A.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "') AND MC.FTMatColorCode IS NULL"
            _Qry &= vbCrLf & " GROUP BY A.FTColor) AS A"
            _Qry &= vbCrLf & " UPDATE A SET FTMapColor = MC.FTMatColorCode"
            _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempImportOrderTargetPMC_D AS A INNER JOIN"
            _Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMatColor AS MC ON A.FTColor = MC.FTMatColorCode"
            _Qry &= vbCrLf & " WHERE  (A.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "') AND ISNULL(A.FTMapColor,'')=''"

            HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function

    Private Function CheckSize() As Boolean
        Try
            Dim _Qry As String = ""
            Dim _FNMatSizeSeq As Integer = Integer.Parse(Val(HI.Conn.SQLConn.GetField("SELECT TOP 1 FNMatSizeSeq FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMatColor WITH(NOLOCK) ORDER BY FNMatColorSeq DESC", Conn.DB.DataBaseName.DB_MASTER, "0")))
            Dim _FNHSysMatSizeId As Integer = Val(HI.TL.RunID.GetRunNoID("TMERMMatSize", "FNHSysMatSizeId", Conn.DB.DataBaseName.DB_MASTER).ToString())
            Dim _dtmapSize As DataTable

            _Qry = "   UPDATE A SET FTMapSize =MS.FTMatSizeCode"
            _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempImportOrderTargetPMC_D AS A INNER JOIN"
            _Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERMMapSize AS MPS ON A.FTSize = MPS.FTMapSize INNER JOIN"
            _Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMatSize AS MS ON MPS.FTMapSizeExtension = MS.FTMatSizeCode"
            _Qry &= vbCrLf & " WHERE  (A.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "') AND ISNULL(A.FTMapSize,'')=''"
            _Qry &= vbCrLf & " UPDATE A SET FTMapSize =MS.FTMatSizeCode"
            _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempImportOrderTargetPMC_D AS A INNER JOIN"
            _Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMatSize AS MS ON A.FTSize = MS.FTMatSizeCode"
            _Qry &= vbCrLf & " WHERE  (A.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "') AND ISNULL(A.FTMapSize,'')='' "

            HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

            _Qry = "   SELECT A.FTSize AS FTSizeCodeNotExists,'' AS FTMapSizeExtend"
            _Qry &= vbCrLf & "  FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempImportOrderTargetPMC_D AS A LEFT OUTER JOIN"
            _Qry &= vbCrLf & "           [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMatSize AS MS ON A.FTMapSize = MS.FTMatSizeCode"
            _Qry &= vbCrLf & "  WHERE  (A.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "') "
            _Qry &= vbCrLf & "  AND MS.FTMatSizeCode IS NULL"
            _Qry &= vbCrLf & "  GROUP BY A.FTSize"
            _dtmapSize = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

            If _dtmapSize.Rows.Count > 0 Then

                _wMapSizeImportOrder = New wMapSizeImportOrder(_dtmapSize)

                HI.TL.HandlerControl.AddHandlerObj(_wMapSizeImportOrder)

                Dim oSysLang As New HI.ST.SysLanguage

                Try
                    Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleID, _wMapSizeImportOrder.Name.ToString.Trim, _wMapSizeImportOrder)

                    Call HI.ST.Lang.SP_SETxLanguage(_wMapSizeImportOrder)

                    _wMapSizeImportOrder.TopMost = True
                    _wMapSizeImportOrder.Refresh()

                    _oDBdtUserImportMapSizeManual = Nothing

                    With _wMapSizeImportOrder
                        .DTUserImportMapSize = Nothing
                        If .ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                            _oDBdtUserImportMapSizeManual = .DTUserImportMapSize.Copy()
                        Else

                            If Not DBNull.Value.Equals(_dtmapSize) And _dtmapSize.Rows.Count > 0 Then

                                For Each oDataRowNotMapSize As System.Data.DataRow In _dtmapSize.Rows
                                    '...add new FNHSysMatSizeId:FTMatSizeCode in System TMERMMatSize [HITECH_MASTER]
                                    Dim nFNHSysMatSizeId As Integer

                                    nFNHSysMatSizeId = Val(HI.TL.RunID.GetRunNoID("TMERMMatSize", "FNHSysMatSizeId", HI.Conn.DB.DataBaseName.DB_MASTER).ToString())

                                    tSql = ""
                                    tSql = "DECLARE @FNMatSizeSeqMax AS INT;"
                                    tSql &= Environment.NewLine & "SELECT @FNMatSizeSeqMax = MAX(A.FNMatSizeSeq)"
                                    tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMatSize] AS A WITH(NOLOCK)"
                                    tSql &= Environment.NewLine & "GROUP BY A.FNHSysMatSizeId;"
                                    tSql &= Environment.NewLine & "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMatSize] ([FTInsUser],[FDInsDate],[FTInsTime]"
                                    tSql &= Environment.NewLine & "                                                                                                    ,[FTUpdUser],[FDUpdDate],[FTUpdTime]"
                                    tSql &= Environment.NewLine & "                                                                                                    ,[FNHSysMatSizeId],[FTMatSizeCode],[FNMatSizeSeq]"
                                    tSql &= Environment.NewLine & "							                                                                           ,[FTMatSizeNameTH],[FTMatSizeNameEN],[FTRemark],[FTStateActive])"
                                    tSql &= Environment.NewLine & "VALUES(NULL, NULL, NULL"
                                    tSql &= ",NULL, NULL, NULL"
                                    tSql &= ", " & nFNHSysMatSizeId & ", N'" & HI.UL.ULF.rpQuoted(oDataRowNotMapSize!FTSizeCodeNotExists.ToString) & "', (ISNULL(@FNMatSizeSeqMax, 0) + 1)"
                                    tSql &= ", N'" & HI.UL.ULF.rpQuoted(oDataRowNotMapSize!FTSizeCodeNotExists.ToString) & "', N'" & HI.UL.ULF.rpQuoted(oDataRowNotMapSize!FTSizeCodeNotExists.ToString) & "', '', '1');"

                                    If HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MASTER) = True Then
                                    End If

                                Next

                            End If

                        End If

                    End With

                    _wMapSizeImportOrder.TopMost = False
                    _wMapSizeImportOrder.Refresh()

                Catch ex As Exception
                End Try


                _Qry = "   UPDATE A SET FTMapSize =MS.FTMatSizeCode"
                _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempImportOrderTargetPMC_D AS A INNER JOIN"
                _Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERMMapSize AS MPS ON A.FTSize = MPS.FTMapSize INNER JOIN"
                _Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMatSize AS MS ON MPS.FTMapSizeExtension = MS.FTMatSizeCode"
                _Qry &= vbCrLf & " WHERE  (A.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "') AND ISNULL(A.FTMapSize,'')=''"
                _Qry &= vbCrLf & " UPDATE A SET FTMapSize =MS.FTMatSizeCode"
                _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempImportOrderTargetPMC_D AS A INNER JOIN"
                _Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMatSize AS MS ON A.FTSize = MS.FTMatSizeCode"
                _Qry &= vbCrLf & " WHERE  (A.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "') AND ISNULL(A.FTMapSize,'')='' "

                HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)


                Call W_PRCbLoadMapSize()

            End If

        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function

    Private Function CheckNewStyleCMS() As Boolean

        Try
            Dim _Qry As String = ""
            Dim _FNHSysStyleId As Integer = Val(HI.TL.RunID.GetRunNoID("TMERMStyle", "FNHSysStyleId", Conn.DB.DataBaseName.DB_MASTER).ToString())
            _FNHSysStyleId = _FNHSysStyleId - 1

            _Qry = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle(FNHSysStyleId, FTStyleCode, FTStyleNameTH, FTStyleNameEN, FTRemark, FTStateActive, FNHSysCustId)"
            _Qry &= vbCrLf & " SELECT " & _FNHSysStyleId & "  + Row_Number() Over (Order By FTStyleMCode ) AS FNHSysStyleId"
            _Qry &= vbCrLf & ",FTStyleMCode AS FTStyleCode"
            _Qry &= vbCrLf & ",FTStyleMCode AS FTStyleNameTH"
            _Qry &= vbCrLf & ",FTStyleMCode AS FTStyleNameEN "
            _Qry &= vbCrLf & "	,'' AS FTRemark "
            _Qry &= vbCrLf & ",'1' AS FTStateActive"
            _Qry &= vbCrLf & "," & Integer.Parse(Val(FNHSysCustId.Properties.Tag.ToString)) & " AS FNHSysCustId"
            _Qry &= vbCrLf & "  FROM"
            _Qry &= vbCrLf & " (SELECT A.FTStyle, S.FTSeasonCode,A.FNHSysSeasonId, A.FTStyle + S.FTSeasonCode AS FTStyleMCode"
            _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempImportOrderTargetCMS AS A WITH(NOLOCK) INNER JOIN"
            _Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMSeason AS S WITH(NOLOCK)  ON A.FNHSysSeasonId = S.FNHSysSeasonId"
            _Qry &= vbCrLf & " WHERE  (A.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "')"
            _Qry &= vbCrLf & " GROUP BY A.FTStyle, S.FTSeasonCode,A.FNHSysSeasonId, A.FTStyle + S.FTSeasonCode "
            _Qry &= vbCrLf & "  ) AS A LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS B WITH(NOLOCK)  ON A.FTStyleMCode = B.FTStyleCode"
            _Qry &= vbCrLf & "     WHERE B.FTStyleCode Is NULL"
            _Qry &= vbCrLf & " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTStyle ( FNHSysStyleId, FTStyleCode, FTStyleNameTH, FTStyleNameEN, FNHSysCustId, FNHSysSeasonId, FTStateActive) "
            _Qry &= vbCrLf & "  SELECT B.FNHSysStyleId,B.FTStyleCode,B.FTStyleNameTH ,B.FTStyleNameEN," & Integer.Parse(Val(FNHSysCustId.Properties.Tag.ToString)) & " AS FNHSysCustId , A.FNHSysSeasonId ,'1'"
            _Qry &= vbCrLf & " FROM "
            _Qry &= vbCrLf & " 	 (SELECT A.FTStyle, S.FTSeasonCode,A.FNHSysSeasonId, A.FTStyle + S.FTSeasonCode AS FTStyleMCode"
            _Qry &= vbCrLf & "  FROM      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempImportOrderTargetCMS AS A WITH(NOLOCK) INNER JOIN"
            _Qry &= vbCrLf & "     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMSeason AS S WITH(NOLOCK)  ON A.FNHSysSeasonId = S.FNHSysSeasonId"
            _Qry &= vbCrLf & " WHERE  (A.FTUserLogin =N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "')"
            _Qry &= vbCrLf & " GROUP BY A.FTStyle, S.FTSeasonCode,A.FNHSysSeasonId, A.FTStyle + S.FTSeasonCode "
            _Qry &= vbCrLf & "  ) AS A LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS B WITH(NOLOCK)  ON A.FTStyleMCode = B.FTStyleCode                                          "
            _Qry &= vbCrLf & "  WHERE NOT(B.FTStyleCode IN (SELECT FTStyleCode FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTStyle  ))"
            _Qry &= vbCrLf & "  UPDATE A SET FNHSysStyleId=B.FNHSysStyleId"
            _Qry &= vbCrLf & "  FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempImportOrderTargetCMS AS A "
            _Qry &= vbCrLf & "  INNER JOIN ( SELECT FTStyle,B.FNHSysStyleId"
            _Qry &= vbCrLf & "   FROM"
            _Qry &= vbCrLf & "  (SELECT  A.FTStyle,  S.FTSeasonCode,A.FNHSysSeasonId, A.FTStyle + S.FTSeasonCode AS FTStyleMCode"
            _Qry &= vbCrLf & "  FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempImportOrderTargetCMS AS A WITH(NOLOCK) INNER JOIN"
            _Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMSeason AS S WITH(NOLOCK)  ON A.FNHSysSeasonId = S.FNHSysSeasonId"
            _Qry &= vbCrLf & "  WHERE  (A.FTUserLogin =N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "')"
            _Qry &= vbCrLf & "  ) AS A LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS B WITH(NOLOCK)  ON A.FTStyleMCode = B.FTStyleCode"
            _Qry &= vbCrLf & " ) AS B ON A.FTStyle =B.FTStyle "
            _Qry &= vbCrLf & "  WHERE (A.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "')"

            HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

            _Qry = "  UPDATE A "
            _Qry &= vbCrLf & "   SET FTOrderNoRef = ISNULL((SELECT TOP 1 FTOrderNo"
            _Qry &= vbCrLf & " FROM TMERTOrder AS X WITH(NOLOCK) "
            _Qry &= vbCrLf & " WHERE  X.FNHSysStyleId=A.FNHSysStyleId"
            _Qry &= vbCrLf & " AND    X.FTPORef =A.FTComitNo"
            _Qry &= vbCrLf & "  ),'') "
            _Qry &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempImportOrderTargetCMS AS A "
            _Qry &= vbCrLf & "  WHERE  (A.FTUserLogin =N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "')"

            HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)


        Catch ex As Exception
            Return False
        End Try
        Return True

    End Function

    Private Function CheckMappingColorWayAndSize() As Boolean
        Dim _StateProcessColorwaySize As Boolean = False
        Try
            Dim _Qry As String = ""
            Dim _dt As DataTable


            _Qry = "   UPDATE A SET  FTMapColor =C.FTMatColorCode"
            _Qry &= vbCrLf & ",FTMapSize=S.FTMatSizeCode "
            _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempImportOrderTargetCMS AS A INNER JOIN"
            _Qry &= vbCrLf & "         [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMapColorSize AS MCS WITH(NOLOCK)  ON A.FTColor = MCS.FTMapColorSizeCode INNER JOIN"
            _Qry &= vbCrLf & "         [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMatColor AS C WITH(NOLOCK)  ON MCS.FNHSysMatColorId = C.FNHSysMatColorId INNER JOIN"
            _Qry &= vbCrLf & "         [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMatSize AS S WITH(NOLOCK) ON MCS.FNHSysMatSizeId = S.FNHSysMatSizeId"
            _Qry &= vbCrLf & " WHERE  (A.FTUserLogin =N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "')"

            HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

            _Qry = "  SELECT A.FTColor, '' AS FNHSysMatColorId, '' AS FNHSysMatSizeId,0 AS FNHSysMatColorId_Hide,0 AS FNHSysMatSizeId_Hide"
            _Qry &= vbCrLf & "  FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempImportOrderTargetCMS AS A WITH(NOLOCK)  LEFT OUTER JOIN"
            _Qry &= vbCrLf & "           [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMapColorSize AS MCS WITH(NOLOCK) ON A.FTColor = MCS.FTMapColorSizeCode"
            _Qry &= vbCrLf & " WHERE  (A.FTUserLogin =N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "') "
            _Qry &= vbCrLf & " AND (MCS.FTMapColorSizeCode IS NULL)"
            _Qry &= vbCrLf & " GROUP BY A.FTColor"
            _Qry &= vbCrLf & " ORDER BY A.FTColor"
            _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

            If _dt.Rows.Count > 0 Then


                _wMapColorSizeData = New wMapColorAndSizeImportOrderTarget(_dt)

                HI.TL.HandlerControl.AddHandlerObj(_wMapColorSizeData)

                Dim oSysLang As New HI.ST.SysLanguage

                Try
                    Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleID, _wMapColorSizeData.Name.ToString.Trim, _wMapColorSizeData)

                    Call HI.ST.Lang.SP_SETxLanguage(_wMapColorSizeData)


                    With _wMapColorSizeData
                        .DTUserImportMapSize = Nothing
                        .ShowDialog()

                        If .StateProcess = True Then
                            _StateProcessColorwaySize = True
                        Else
                            _StateProcessColorwaySize = False
                        End If

                    End With


                Catch ex As Exception
                End Try

                If _StateProcessColorwaySize Then
                    _Qry = "   UPDATE A SET  FTMapColor =C.FTMatColorCode"
                    _Qry &= vbCrLf & ",FTMapSize=S.FTMatSizeCode "
                    _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempImportOrderTargetCMS AS A INNER JOIN"
                    _Qry &= vbCrLf & "         [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMapColorSize AS MCS WITH(NOLOCK)  ON A.FTColor = MCS.FTMapColorSizeCode INNER JOIN"
                    _Qry &= vbCrLf & "         [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMatColor AS C WITH(NOLOCK)  ON MCS.FNHSysMatColorId = C.FNHSysMatColorId INNER JOIN"
                    _Qry &= vbCrLf & "         [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMatSize AS S WITH(NOLOCK) ON MCS.FNHSysMatSizeId = S.FNHSysMatSizeId"
                    _Qry &= vbCrLf & " WHERE  (A.FTUserLogin =N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "') AND FTMapColor='' "

                    HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

                End If


            Else
                _StateProcessColorwaySize = True
            End If

        Catch ex As Exception
            Return False
        End Try
        Return _StateProcessColorwaySize
    End Function

    Private Function ImportFileToTemp(dt As DataTable) As Boolean
        Try

            Dim _Qry As String = ""

            _Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempImportOrderTargetPMC WHERE FTUserLogin='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
            HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

            _Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempImportOrderTargetPMC_D WHERE FTUserLogin='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
            HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

            Dim RowIdx As Integer = 0
            Dim _CommitNo As String = ""
            Dim _Season As String = ""
            Dim _Program As String = ""
            Dim _ProductName As String = ""
            Dim _Style As String = ""
            Dim _TotalQuantity As Integer = 0
            Dim _StyleQuantity As Integer = 0
            Dim _Ship As String = ""
            Dim _StateColor As Boolean = False
            Dim _StateNew As Boolean = True
            Dim _FNFOBPrice As Double = 0
            Dim _FNStorePrice As Double = 0
            Dim _FNHSysMerTeamId As Integer = -1
            Dim _FTColor As String = ""
            Dim _FTSize As String = ""
            Dim _FTColorPer As String = ""
            Dim _FTSizePer As String = ""
            Dim _SizeQuantity As Integer = 0
            Dim FNGrpNo As Integer = 1

            For Each R As DataRow In dt.Rows

                Select Case RowIdx
                    Case 0, 1, 4, 5, 6, 7
                    Case 2
                        Try
                            _Program = ((R!F2.ToString).Split(":")(1)).Trim()
                        Catch ex As Exception
                        End Try

                        Try
                            _CommitNo = ((R!F3.ToString).Split(":")(1)).Trim()
                        Catch ex As Exception
                        End Try

                        Try
                            _Season = ((R!F4.ToString).Split(":")(1)).Trim()
                        Catch ex As Exception
                        End Try
                    Case 3
                        Try
                            _TotalQuantity = Integer.Parse((((R!F3.ToString).Split(":")(1)).Trim()).Replace(",", ""))
                        Catch ex As Exception

                        End Try
                    Case Else
                        If _StateColor = False Then

                            Select Case R!F1.ToString.ToLower
                                Case "Product Name".ToLower
                                    _ProductName = R!F2.ToString
                                Case "PLM Product ID".ToLower
                                    _Style = Microsoft.VisualBasic.Right(R!F2.ToString, 6)
                                Case "In Store Date".ToLower
                                    _Ship = R!F2.ToString
                                Case "FOB Cost".ToLower

                                    Dim _tmp As String = R!F2.ToString
                                    Dim _FTNetUnitPrice As String = ""
                                    _FTNetUnitPrice = ""
                                    For Each C As Char In _tmp.ToCharArray

                                        If IsNumeric(C) Or C = "." Then
                                            _FTNetUnitPrice = _FTNetUnitPrice & C
                                        End If
                                    Next

                                    _FNFOBPrice = 0

                                    Try
                                        _FNFOBPrice = Double.Parse(_FTNetUnitPrice)
                                    Catch ex As Exception
                                    End Try

                                Case "Retail".ToLower
                                    Dim _tmp As String = R!F2.ToString
                                    Dim _FTNetUnitPrice As String = ""
                                    _FTNetUnitPrice = ""
                                    For Each C As Char In _tmp.ToCharArray

                                        If IsNumeric(C) Or C = "." Then
                                            _FTNetUnitPrice = _FTNetUnitPrice & C
                                        End If
                                    Next

                                    _FNStorePrice = 0

                                    Try
                                        _FNStorePrice = Double.Parse(_FTNetUnitPrice)
                                    Catch ex As Exception
                                    End Try
                                Case "Product Units".ToLower
                                    Try
                                        _TotalQuantity = Integer.Parse((((R!F2.ToString)).Trim()).Replace(",", ""))
                                    Catch ex As Exception

                                    End Try
                                Case "Color".ToLower

                                    _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempImportOrderTargetPMC"
                                    _Qry &= vbCrLf & " ("
                                    _Qry &= vbCrLf & "    FTUserLogin, FTPONo, FTSeason, FTStyle, FTOrderDate, FDShipDate, FTProdTypeDescription, FNHSysMainCategoryId"
                                    _Qry &= vbCrLf & "  , FNHSysPlantId, FNHSysGenderId, FNHSysBuyGrpId,  FNHSysProdTypeId"
                                    _Qry &= vbCrLf & "  , FNHSysCountryId, FNHSysBuyerId, FNHSysStyleId"
                                    _Qry &= vbCrLf & " ,FNHSysMerTeamId, FNGrandQuantity, FNQuantity, FNFOBPrice, FNStorePrice, FTGenerateOrderNo,FNGrpNo,FNHSysSeasonId"
                                    _Qry &= vbCrLf & " ) "
                                    _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                                    _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(_CommitNo) & "'"
                                    _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(_Season) & "'"
                                    _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(_Style) & "'"
                                    _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & ""
                                    _Qry &= vbCrLf & " ,'" & HI.UL.ULDate.ConvertEnDB(_Ship) & "'"
                                    _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(_ProductName) & "',-1,-1,-1," & Integer.Parse(Val(FNHSysBuyGrpId.Properties.Tag.ToString())) & ",-1,-1,-1,-1"
                                    _Qry &= vbCrLf & "," & _FNHSysMerTeamId & ""
                                    _Qry &= vbCrLf & "," & _TotalQuantity & ""
                                    _Qry &= vbCrLf & "," & _StyleQuantity & ""
                                    _Qry &= vbCrLf & "," & _FNFOBPrice & ""
                                    _Qry &= vbCrLf & "," & _FNStorePrice & ""
                                    _Qry &= vbCrLf & ",''"
                                    _Qry &= vbCrLf & "," & FNGrpNo & ""
                                    _Qry &= vbCrLf & "," & Integer.Parse(Val(FNHSysSeasonId.Properties.Tag.ToString)) & ""

                                    If HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_MERCHAN) = False Then
                                        _Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempImportOrderTargetPMC WHERE FTUserLogin='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                                        HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

                                        _Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempImportOrderTargetPMC_D WHERE FTUserLogin='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                                        HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

                                        Return False
                                    End If

                                    _StateColor = True

                            End Select
                        Else
                            Select Case R!F1.ToString.ToLower
                                Case "TOTAL :".ToLower

                                    FNGrpNo = FNGrpNo + 1

                                    _StateColor = False
                                Case Else

                                    If R!F1.ToString <> "" Then

                                        _FTColor = Microsoft.VisualBasic.Left(R!F1.ToString.Trim, 30)
                                        _FTColorPer = R!F2.ToString.Trim

                                        If _FTColorPer = "" Then
                                            _FTColorPer = "100"
                                        End If

                                    End If

                                    _FTSize = R!F5.ToString.Trim
                                    _FTSizePer = R!F6.ToString.Trim

                                    _SizeQuantity = 0

                                    If IsNumeric(_FTSizePer) Then
                                        _SizeQuantity = Integer.Parse((_TotalQuantity * Double.Parse(_FTSizePer)) / 100)
                                    End If

                                    _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempImportOrderTargetPMC_D"
                                    _Qry &= vbCrLf & " ("
                                    _Qry &= vbCrLf & "  FTUserLogin, FTPONo, FTSeason, FTStyle, FDShipDate,FTProdTypeDescription, FTColor"
                                    _Qry &= vbCrLf & ", FTColorPer, FTSize, FTSizePer, FTMapColor, FTMapSize, FNQuantity,FNGrpNo"
                                    _Qry &= vbCrLf & " ) "
                                    _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                                    _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(_CommitNo) & "'"
                                    _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(_Season) & "'"
                                    _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(_Style) & "'"
                                    _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(_Ship) & "'"
                                    _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(_ProductName) & "'"
                                    _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(_FTColor) & "'"
                                    _Qry &= vbCrLf & "," & Val(_FTColorPer) & ""
                                    _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(_FTSize) & "'"
                                    _Qry &= vbCrLf & "," & Val(_FTSizePer) & ""
                                    _Qry &= vbCrLf & ",''"
                                    _Qry &= vbCrLf & ",''"
                                    _Qry &= vbCrLf & "," & _SizeQuantity & ""
                                    _Qry &= vbCrLf & "," & FNGrpNo & ""

                                    If HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_MERCHAN) = False Then
                                        _Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempImportOrderTargetPMC WHERE FTUserLogin='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                                        HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

                                        _Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempImportOrderTargetPMC_D WHERE FTUserLogin='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                                        HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

                                        Return False
                                    End If

                            End Select
                        End If
                End Select

                RowIdx = RowIdx + 1
            Next

          


        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function

    Private Function ImportFilePMCPDFToTemp(dt As DataTable, Program As String, Season As String, CommitNo As String, TotalQuantity As Integer) As Boolean
        Try


            Dim _Qry As String = ""

            _Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempImportOrderTargetPMC WHERE FTUserLogin='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
            HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

            _Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempImportOrderTargetPMC_D WHERE FTUserLogin='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
            HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

            Dim RowIdx As Integer = 0
            Dim _CommitNo As String = ""
            Dim _Season As String = ""
            Dim _Program As String = ""
            Dim _ProductName As String = ""
            Dim _Style As String = ""
            Dim _TotalQuantity As Integer = 0
            Dim _StyleQuantity As Integer = 0
            Dim _Ship As String = ""
            Dim _StateColor As Boolean = False
            Dim _StateNew As Boolean = True
            Dim _FNFOBPrice As Double = 0
            Dim _FNStorePrice As Double = 0
            Dim _FNHSysMerTeamId As Integer = -1
            Dim _FTColor As String = ""
            Dim _FTSize As String = ""
            Dim _FTColorPer As String = ""
            Dim _FTSizePer As String = ""
            Dim _SizeQuantity As Integer = 0
            Dim FNGrpNo As Integer = 0
            Dim FNGrpNoRun As Integer = 0

            _Program = Program
            _Season = Season
            _CommitNo = CommitNo
            _TotalQuantity = TotalQuantity
            _Ship = HI.UL.ULDate.ConvertEnDB(HI.UL.ULDate.AddMonth(HI.ST.UserInfo.LogINDate, 5))

            For Each R As DataRow In dt.Rows
                _Style = Microsoft.VisualBasic.Right(R!F8.ToString, 8)
                _ProductName = R!F14.ToString

                Dim _tmp As String = R!F15.ToString
                Dim _FTNetUnitPrice As String = ""
                _FTNetUnitPrice = ""

                For Each C As Char In _tmp.ToCharArray

                    If IsNumeric(C) Or C = "." Then
                        _FTNetUnitPrice = _FTNetUnitPrice & C
                    End If

                Next

                _FNFOBPrice = 0

                Try
                    _FNFOBPrice = Double.Parse(_FTNetUnitPrice)
                Catch ex As Exception
                End Try

                _tmp = R!F17.ToString

                _FTNetUnitPrice = ""
                For Each C As Char In _tmp.ToCharArray

                    If IsNumeric(C) Or C = "." Then
                        _FTNetUnitPrice = _FTNetUnitPrice & C
                    End If
                Next

                _FNStorePrice = 0

                Try
                    _FNStorePrice = Double.Parse(_FTNetUnitPrice)
                Catch ex As Exception
                End Try

                Try
                    _TotalQuantity = Integer.Parse((((R!F18.ToString))).Replace(",", ""))
                Catch ex As Exception

                End Try

                _FTColor = R!F2.ToString.Trim()
                _FTColorPer = "100"
                _FTSize = R!F3.ToString.Trim
                _FTSizePer = "100"

                _SizeQuantity = _TotalQuantity

                _Qry = " Select FNGrpNo"
                _Qry &= vbCrLf & "    FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempImportOrderTargetPMC AS A WITH(NOLOCK)"
                _Qry &= vbCrLf & " WHERE  (FTUserLogin ='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "') "
                _Qry &= vbCrLf & " AND (FTPONo ='" & HI.UL.ULF.rpQuoted(_CommitNo) & "')"
                _Qry &= vbCrLf & "  AND (FTSeason ='" & HI.UL.ULF.rpQuoted(_Season) & "') "
                _Qry &= vbCrLf & " AND (FTStyle ='" & HI.UL.ULF.rpQuoted(_Style) & "') "
                _Qry &= vbCrLf & " AND (FDShipDate = '" & HI.UL.ULDate.ConvertEnDB(_Ship) & "') "
                _Qry &= vbCrLf & " AND (FTProdTypeDescription = '" & HI.UL.ULF.rpQuoted(_ProductName) & "')"
                FNGrpNoRun = Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MERCHAN, "0")))

                If FNGrpNoRun <= 0 Then
                    FNGrpNo = FNGrpNo + 1

                    _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempImportOrderTargetPMC"
                    _Qry &= vbCrLf & " ("
                    _Qry &= vbCrLf & "    FTUserLogin, FTPONo, FTSeason, FTStyle, FTOrderDate, FDShipDate, FTProdTypeDescription, FNHSysMainCategoryId"
                    _Qry &= vbCrLf & "  , FNHSysPlantId, FNHSysGenderId, FNHSysBuyGrpId,  FNHSysProdTypeId"
                    _Qry &= vbCrLf & "  , FNHSysCountryId, FNHSysBuyerId, FNHSysStyleId"
                    _Qry &= vbCrLf & " ,FNHSysMerTeamId, FNGrandQuantity, FNQuantity, FNFOBPrice, FNStorePrice, FTGenerateOrderNo,FNGrpNo,FNHSysSeasonId"
                    _Qry &= vbCrLf & " ) "
                    _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(_CommitNo) & "'"
                    _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(_Season) & "'"
                    _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(_Style) & "'"
                    _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & ""
                    _Qry &= vbCrLf & " ,'" & HI.UL.ULDate.ConvertEnDB(_Ship) & "'"
                    _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(_ProductName) & "',-1,-1,-1," & Integer.Parse(Val(FNHSysBuyGrpId.Properties.Tag.ToString())) & ",-1,-1,-1,-1"
                    _Qry &= vbCrLf & "," & _FNHSysMerTeamId & ""
                    _Qry &= vbCrLf & "," & _TotalQuantity & ""
                    _Qry &= vbCrLf & "," & _StyleQuantity & ""
                    _Qry &= vbCrLf & "," & _FNFOBPrice & ""
                    _Qry &= vbCrLf & "," & _FNStorePrice & ""
                    _Qry &= vbCrLf & ",''"
                    _Qry &= vbCrLf & "," & FNGrpNo & ""
                    _Qry &= vbCrLf & "," & Integer.Parse(Val(FNHSysSeasonId.Properties.Tag.ToString)) & ""

                    If HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_MERCHAN) = False Then
                        _Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempImportOrderTargetPMC WHERE FTUserLogin='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                        HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

                        _Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempImportOrderTargetPMC_D WHERE FTUserLogin='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                        HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

                        Return False
                    End If

                    FNGrpNoRun = FNGrpNo
                End If
               
                _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempImportOrderTargetPMC_D"
                _Qry &= vbCrLf & " ("
                _Qry &= vbCrLf & "  FTUserLogin, FTPONo, FTSeason, FTStyle, FDShipDate,FTProdTypeDescription, FTColor"
                _Qry &= vbCrLf & ", FTColorPer, FTSize, FTSizePer, FTMapColor, FTMapSize, FNQuantity,FNGrpNo,FNFOBPrice,FNStorePrice"
                _Qry &= vbCrLf & " ) "
                _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(_CommitNo) & "'"
                _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(_Season) & "'"
                _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(_Style) & "'"
                _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(_Ship) & "'"
                _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(_ProductName) & "'"
                _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(_FTColor) & "'"
                _Qry &= vbCrLf & "," & Val(_FTColorPer) & ""
                _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(_FTSize) & "'"
                _Qry &= vbCrLf & "," & Val(_FTSizePer) & ""
                _Qry &= vbCrLf & ",''"
                _Qry &= vbCrLf & ",''"
                _Qry &= vbCrLf & "," & _SizeQuantity & ""
                _Qry &= vbCrLf & "," & FNGrpNoRun & ""
                _Qry &= vbCrLf & "," & _FNFOBPrice & ""
                _Qry &= vbCrLf & "," & _FNStorePrice & ""

                If HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_MERCHAN) = False Then

                    _Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempImportOrderTargetPMC WHERE FTUserLogin='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                    HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

                    _Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempImportOrderTargetPMC_D WHERE FTUserLogin='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                    HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

                    Return False
                End If

   
                RowIdx = RowIdx + 1
            Next

        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function

    Private Function ImportFileCMSToTemp(dt As DataTable) As Boolean
        Try

            Dim _Qry As String = ""

            _Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempImportOrderTargetCMS WHERE FTUserLogin='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
            HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

            _Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempImportOrderTargetCMS_D WHERE FTUserLogin='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
            HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

            Dim RowIdx As Integer = 0
            Dim _CommitNo As String = ""
            Dim _Season As String = ""
            Dim _Program As String = ""
            Dim _ProductName As String = ""
            Dim _Style As String = ""
            Dim _TotalQuantity As Integer = 0
            Dim _StyleQuantity As Integer = 0
            Dim _Ship As String = ""
            Dim _StateColor As Boolean = False
            Dim _StateNew As Boolean = True
            Dim _FNFOBPrice As Double = 0
            Dim _FNStorePrice As Double = 0
            Dim _FNHSysMerTeamId As Integer = -1
            Dim _FTColor As String = ""
            Dim _FTSize As String = ""
            Dim _FTColorPer As String = ""
            Dim _FTSizePer As String = ""
            Dim _SizeQuantity As Integer = 0
            Dim FNGrpNo As Integer = 1
            Dim _dtBreakdown As New DataTable

            _FNHSysMerTeamId = Me.SysMerTeamId

            With _dtBreakdown
                .Columns.Add("FNSeqBreakdown", GetType(Integer))
                .Columns.Add("FNRowInd", GetType(Integer))
                .Columns.Add("FNRowLastInd", GetType(Integer))
            End With
            Dim _FNSeqBreakdown As Integer = 1
            Dim _FNRowSeq As Integer = 0
            dt.Columns.Add("FNSeqBreakdown", GetType(Integer))
            dt.Columns.Add("FNRowSeq", GetType(Integer))

            dt.BeginInit()
            For Each R As DataRow In dt.Rows
                _FNRowSeq = _FNRowSeq + 1

                R!FNRowSeq = _FNRowSeq


                If R!F1.ToString.ToLower = "Commitment Report: PO Item-Breakdown".ToLower Then
                    R!FNSeqBreakdown = _FNSeqBreakdown
                    _dtBreakdown.Rows.Add(_FNSeqBreakdown, _FNRowSeq, -1)

                    For Each Rx As DataRow In _dtBreakdown.Select("FNSeqBreakdown<" & _FNSeqBreakdown & "", "FNSeqBreakdown DESC")
                        Rx!FNRowLastInd = _FNRowSeq - 1
                        Exit For
                    Next

                    _FNSeqBreakdown = _FNSeqBreakdown + 1

                ElseIf R!F1.ToString.ToLower = "Commitment Report: Purchase Orders by OTB".ToLower Then
                    For Each Rx As DataRow In _dtBreakdown.Select("FNSeqBreakdown<" & _FNSeqBreakdown & "", "FNSeqBreakdown DESC")
                        Rx!FNRowLastInd = _FNRowSeq - 1
                        Exit For
                    Next
                End If
            Next
            dt.EndInit()

            _CommitNo = (dt.Rows(3)!F2.ToString).Trim

            RowIdx = 1
            _StateColor = False
            For Each R As DataRow In dt.Rows

                Select Case RowIdx
                    Case 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10
                    Case 11
                        If R!F1.ToString.Trim.ToLower = "Description".ToLower And R!F4.ToString.Trim.ToLower = "Item #".ToLower Then
                            _StateColor = True
                        End If

                    Case Else
                        If _StateColor = True Then
                            If R!F1.ToString.Trim.ToLower = "Ttl Units".ToLower Or R!F1.ToString.Trim.ToLower = "Commitment Report: PO Item-Breakdown".ToLower Or R!F1.ToString.Trim.ToLower = "" Then
                                _StateColor = False
                                Exit For

                            Else

                                _Qry = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempImportOrderTargetCMS (FTUserLogin, FTComitNo, FTUPC, FTItem, FTStyle, FTOrderDate"
                                _Qry &= vbCrLf & " , FNHSysMainCategoryId, FNHSysPlantId, FNHSysGenderId, FNHSysBuyGrpId"
                                _Qry &= vbCrLf & ", FNHSysMerTeamId, FNHSysProdTypeId, FNHSysCountryId"
                                _Qry &= vbCrLf & ",FNHSysBuyerId, FNHSysStyleId, FTColor, FTMapColor, FTMapSize, FNQuantity, FNFOBPrice, FNHSysSeasonId"
                                _Qry &= vbCrLf & " )"
                                _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_CommitNo) & "'"
                                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!F7.ToString) & "'"
                                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!F4.ToString) & "'"
                                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Microsoft.VisualBasic.Right(R!F8.ToString, 6)) & "'"
                                _Qry &= vbCrLf & ",'" & HI.UL.ULDate.ConvertEnDB(Me.FDOrderDate.Text) & "'"
                                _Qry &= vbCrLf & ",-1,-1,-1," & Integer.Parse(Val(FNHSysBuyGrpId.Properties.Tag.ToString())) & ""
                                _Qry &= vbCrLf & "," & _FNHSysMerTeamId & ",-1,-1,-1,-1"
                                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!F1.ToString) & "'"
                                _Qry &= vbCrLf & ",'',''"
                                _Qry &= vbCrLf & "," & Integer.Parse(Val(R!F21.ToString.Replace(",", ""))) & ""
                                _Qry &= vbCrLf & "," & Double.Parse(Val(R!F16.ToString.Replace(",", ""))) & ""
                                _Qry &= vbCrLf & "," & Integer.Parse(Val(Me.FNHSysSeasonId.Properties.Tag.ToString)) & ""

                                HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)


                            End If

                        End If
                End Select

                RowIdx = RowIdx + 1
            Next

            Dim _StartRow As Integer = 0
            Dim _EedRow As Integer = 0
            Dim _SeqBreakdown As Integer = 0
            Dim _Color As String = ""
            Dim _PONo As String = ""
            Dim _ShipDate As String = ""
            Dim _INStoreDate As String = ""
            Dim _CMT As String = ""
            Dim _TCV As String = ""
            Dim _Item As String = ""
            Dim _FTCountry As String = ""
            Dim _Quantity As Integer = 0
            Dim StringFilter As String = ""
            Dim RIdx As Integer = 1
            For Each R As DataRow In _dtBreakdown.Rows

                _SeqBreakdown = Integer.Parse(Val(R!FNSeqBreakdown.ToString))
                _StartRow = Integer.Parse(Val(R!FNRowInd.ToString))
                _EedRow = Integer.Parse(Val(R!FNRowLastInd.ToString))
                RIdx = 1

                Dim _tmpdt As DataTable = dt.Select("FNRowSeq>=" & _StartRow & " AND FNRowSeq<=" & _EedRow & " ", "FNRowSeq").CopyToDataTable
                Dim _TextData As String = ""
                If Not (_tmpdt Is Nothing) Then
                    For Each Rx As DataRow In _tmpdt.Rows

                        Select Case RIdx
                            Case 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12
                            Case Else

                                _TextData = Rx!F1.ToString.Trim
                                Select Case _TextData.ToLower
                                    Case "Ttl Cost".ToLower, "Ttl Units".ToLower, "Ttl Retl".ToLower, "Ttl IMU".ToLower, "Description".ToLower
                                    Case Else
                                        _Color = Rx!F1.ToString
                                        _Item = Rx!F2.ToString

                                        For Each Col As DataColumn In dt.Columns
                                            _TextData = _tmpdt.Rows(11).Item(Col.ColumnName.ToString).ToString.Trim
                                            Select Case _TextData.ToLower
                                                Case "Units".ToLower
                                                    _Quantity = Integer.Parse(Val(Rx.Item(Col.ColumnName.ToString).ToString.Replace(",", "")))

                                                    If _Quantity > 0 Then
                                    
                                                        _PONo = _tmpdt.Rows(2).Item(_tmpdt.Columns.IndexOf(Col.ColumnName.ToString) - 3).ToString.Trim
                                                        _ShipDate = _tmpdt.Rows(4).Item(_tmpdt.Columns.IndexOf(Col.ColumnName.ToString) - 3).ToString.Trim
                                                        _INStoreDate = _tmpdt.Rows(6).Item(_tmpdt.Columns.IndexOf(Col.ColumnName.ToString) - 3).ToString.Trim
                                                        _CMT = _tmpdt.Rows(8).Item(_tmpdt.Columns.IndexOf(Col.ColumnName.ToString) - 3).ToString.Trim
                                                        _TCV = _tmpdt.Rows(10).Item(_tmpdt.Columns.IndexOf(Col.ColumnName.ToString) - 3).ToString.Trim

                                                        _FTCountry = ""


                                                        If _PONo.Trim <> "" Then
                                                            For Each Str As String In _PONo.Split(",")
                                                                StringFilter = "F1='" & HI.UL.ULF.rpQuoted(Microsoft.VisualBasic.Right(_CMT, _CMT.Length - 2)) & "' AND F3='" & HI.UL.ULF.rpQuoted(Str.Trim()) & "'  AND F8='" & HI.UL.ULF.rpQuoted(_INStoreDate) & "'"
                                                                For Each Rct As DataRow In dt.Select(StringFilter)
                                                                    _FTCountry = Rct!F19.ToString.Trim
                                                                    _ShipDate = Rct!F12.ToString.Trim
                                                                    Exit For
                                                                Next

                                                            Next
                                                        Else
                                                            _PONo = "Waiting"


                                                            If _CMT <> "" And _CMT.Length > 3 Then
                                                                StringFilter = "(F1='" & HI.UL.ULF.rpQuoted(_CMT) & "' OR F1='" & HI.UL.ULF.rpQuoted(Microsoft.VisualBasic.Right(_CMT, _CMT.Length - 2)) & "'  )  AND F8='" & HI.UL.ULF.rpQuoted(_INStoreDate) & "'"
                                                                For Each Rct As DataRow In dt.Select(StringFilter)

                                                                    _FTCountry = Rct!F19.ToString.Trim
                                                                    _ShipDate = Rct!F12.ToString.Trim

                                                                    Exit For
                                                                Next
                                                            End If
                                                        End If

                                                        If _ShipDate = "" Or IsDate(_ShipDate) = False Then
                                                            _ShipDate = _INStoreDate
                                                        End If

                                                        If _ShipDate = "" Then
                                                            _ShipDate = HI.ST.UserInfo.LogINDate
                                                        End If

                                                        If IsDate(_ShipDate) = False Then
                                                            _ShipDate = HI.ST.UserInfo.LogINDate
                                                        End If

                                                        _Qry = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempImportOrderTargetCMS_D ("
                                                        _Qry &= vbCrLf & "  FTUserLogin, FTComitNo, FTPORef, FTItem, FTColor"
                                                        _Qry &= vbCrLf & " , FDShipDate,FDStoreDate, FNQuantity, FTCmTOtb"
                                                        _Qry &= vbCrLf & " , FNHSysPlantId, FNHSysCountryId,FTCountry"
                                                        _Qry &= vbCrLf & " )"
                                                        _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                                                        _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_CommitNo) & "'"
                                                        _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_PONo) & "'"
                                                        _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_Item) & "'"
                                                        _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_Color) & "'"
                                                        _Qry &= vbCrLf & ",'" & HI.UL.ULDate.ConvertEnDB(_ShipDate) & "'"
                                                        _Qry &= vbCrLf & ",'" & HI.UL.ULDate.ConvertEnDB(_INStoreDate) & "'"
                                                        _Qry &= vbCrLf & "," & _Quantity & ""
                                                        _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_CMT) & "'"
                                                        _Qry &= vbCrLf & ",0"
                                                        _Qry &= vbCrLf & ",0"
                                                        _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_FTCountry) & "'"

                                                        HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

                                                    End If

                                            End Select

                                        Next

                                End Select

                        End Select

                        RIdx = RIdx + 1
                    Next
                End If

               

            Next

        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function

    Private Sub ShowPMCPDFData()
        Call InitGridPMC(Nothing, Nothing)
        Try

            Dim _dt2 As DataTable
            Dim _dtShow As New DataTable
            Dim _Qry As String = ""
            Dim _dtSize As New DataTable

            With _dtSize
                .Columns.Add("FTMapSize", GetType(String))
                .Columns.Add("FNHSysMatSizeId", GetType(String))
                .Columns.Add("FNMatSizeSeq", GetType(Double))
            End With

            With _dtShow
                .Columns.Add("FTPONo", GetType(String))
                .Columns.Add("FNGrpNo", GetType(Integer))
                .Columns.Add("FTStyleCode", GetType(String))
                .Columns.Add("FTProdTypeDescription", GetType(String))
                .Columns.Add("FNFOBPrice", GetType(Double))
                .Columns.Add("FNStorePrice", GetType(Double))
                .Columns.Add("FTMapColor", GetType(String))
            End With

            _Qry = "  SELECT A.FTPONo,A.FNGrpNo, ST.FTStyleCode, A.FTProdTypeDescription, B.FNFOBPrice, B.FNStorePrice, B.FTMapColor, B.FTMapSize, B.FNQuantity, MS.FNHSysMatSizeId, MS.FNMatSizeSeq"
            _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempImportOrderTargetPMC AS A WITH(NOLOCK) INNER JOIN"
            _Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempImportOrderTargetPMC_D AS B WITH(NOLOCK) ON A.FTUserLogin = B.FTUserLogin AND A.FTPONo = B.FTPONo AND A.FTStyle = B.FTStyle AND A.FNGrpNo = B.FNGrpNo INNER JOIN"
            _Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMatSize AS MS WITH(NOLOCK) ON B.FTMapSize = MS.FTMatSizeCode LEFT OUTER JOIN"
            _Qry &= vbCrLf & "           [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS ST WITH(NOLOCK) ON A.FNHSysStyleId = ST.FNHSysStyleId"
            _Qry &= vbCrLf & "  WHERE   (A.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "')"
            _Qry &= vbCrLf & " ORDER BY  MS.FNMatSizeSeq"

            _dt2 = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)
            Dim _StrFilter As String = ""

            For Each R As DataRow In _dt2.Rows

                _StrFilter = "FTPONo='" & HI.UL.ULF.rpQuoted(R!FTPONo.ToString) & "' AND FNGrpNo=" & Val(R!FNGrpNo.ToString) & " AND FTStyleCode='" & HI.UL.ULF.rpQuoted(R!FTStyleCode.ToString) & "' AND FTProdTypeDescription='" & HI.UL.ULF.rpQuoted(R!FTProdTypeDescription.ToString) & "' AND FTMapColor ='" & HI.UL.ULF.rpQuoted(R!FTMapColor.ToString) & "'"

                If _dtShow.Select(_StrFilter).Length <= 0 Then
                    _dtShow.Rows.Add(R!FTPONo.ToString, Val(R!FNGrpNo), R!FTStyleCode.ToString, R!FTProdTypeDescription.ToString, Val(R!FNFOBPrice), Val(R!FNStorePrice.ToString), R!FTMapColor.ToString)
                End If

                If _dtShow.Columns.IndexOf("C" & R!FNHSysMatSizeId.ToString) < 0 Then
                    _dtShow.Columns.Add("C" & R!FNHSysMatSizeId.ToString, GetType(Integer))
                End If

                For Each Rx As DataRow In _dtShow.Select(_StrFilter)
                    Rx.Item("C" & R!FNHSysMatSizeId.ToString) = Val(R!FNQuantity)
                Next

                If _dtSize.Select("FNHSysMatSizeId=" & Val(R!FNHSysMatSizeId.ToString) & "").Length <= 0 Then
                    _dtSize.Rows.Add(R!FTMapSize.ToString, Val(R!FNHSysMatSizeId.ToString), Val(R!FNMatSizeSeq.ToString))
                End If

            Next

            Call InitGridPMC(_dtShow, _dtSize)


        Catch ex As Exception
        End Try

    End Sub

    Private Sub ShowPMCData()
        Call InitGridPMC(Nothing, Nothing)
        Try

            Dim _dt2 As DataTable
            Dim _dtShow As New DataTable
            Dim _Qry As String = ""
            Dim _dtSize As New DataTable

            With _dtSize
                .Columns.Add("FTMapSize", GetType(String))
                .Columns.Add("FNHSysMatSizeId", GetType(String))
                .Columns.Add("FNMatSizeSeq", GetType(Double))
            End With

            With _dtShow
                .Columns.Add("FTPONo", GetType(String))
                .Columns.Add("FNGrpNo", GetType(Integer))
                .Columns.Add("FTStyleCode", GetType(String))
                .Columns.Add("FTProdTypeDescription", GetType(String))
                .Columns.Add("FNFOBPrice", GetType(Double))
                .Columns.Add("FNStorePrice", GetType(Double))
                .Columns.Add("FTMapColor", GetType(String))
            End With

            _Qry = "  SELECT A.FTPONo,A.FNGrpNo, ST.FTStyleCode, A.FTProdTypeDescription, A.FNFOBPrice, A.FNStorePrice, B.FTMapColor, B.FTMapSize, B.FNQuantity, MS.FNHSysMatSizeId, MS.FNMatSizeSeq"
            _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempImportOrderTargetPMC AS A WITH(NOLOCK) INNER JOIN"
            _Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempImportOrderTargetPMC_D AS B WITH(NOLOCK) ON A.FTUserLogin = B.FTUserLogin AND A.FTPONo = B.FTPONo AND A.FTStyle = B.FTStyle AND A.FNGrpNo = B.FNGrpNo INNER JOIN"
            _Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMatSize AS MS WITH(NOLOCK) ON B.FTMapSize = MS.FTMatSizeCode LEFT OUTER JOIN"
            _Qry &= vbCrLf & "           [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS ST WITH(NOLOCK) ON A.FNHSysStyleId = ST.FNHSysStyleId"
            _Qry &= vbCrLf & "  WHERE   (A.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "')"
            _Qry &= vbCrLf & " ORDER BY  MS.FNMatSizeSeq"

            _dt2 = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)
            Dim _StrFilter As String = ""

            For Each R As DataRow In _dt2.Rows

                _StrFilter = "FTPONo='" & HI.UL.ULF.rpQuoted(R!FTPONo.ToString) & "' AND FNGrpNo=" & Val(R!FNGrpNo.ToString) & " AND FTStyleCode='" & HI.UL.ULF.rpQuoted(R!FTStyleCode.ToString) & "' AND FTProdTypeDescription='" & HI.UL.ULF.rpQuoted(R!FTProdTypeDescription.ToString) & "' AND FTMapColor ='" & HI.UL.ULF.rpQuoted(R!FTMapColor.ToString) & "'"

                If _dtShow.Select(_StrFilter).Length <= 0 Then
                    _dtShow.Rows.Add(R!FTPONo.ToString, Val(R!FNGrpNo), R!FTStyleCode.ToString, R!FTProdTypeDescription.ToString, Val(R!FNFOBPrice), Val(R!FNStorePrice.ToString), R!FTMapColor.ToString)
                End If

                If _dtShow.Columns.IndexOf("C" & R!FNHSysMatSizeId.ToString) < 0 Then
                    _dtShow.Columns.Add("C" & R!FNHSysMatSizeId.ToString, GetType(Integer))
                End If

                For Each Rx As DataRow In _dtShow.Select(_StrFilter)
                    Rx.Item("C" & R!FNHSysMatSizeId.ToString) = Val(R!FNQuantity)
                Next

                If _dtSize.Select("FNHSysMatSizeId=" & Val(R!FNHSysMatSizeId.ToString) & "").Length <= 0 Then
                    _dtSize.Rows.Add(R!FTMapSize.ToString, Val(R!FNHSysMatSizeId.ToString), Val(R!FNMatSizeSeq.ToString))
                End If

            Next

            Call InitGridPMC(_dtShow, _dtSize)


        Catch ex As Exception
        End Try

    End Sub

    Private Sub InitGridPMC(Optional _dt As DataTable = Nothing, Optional _dtsize As DataTable = Nothing)

        Dim _colcount As Integer = 0
        With Me.ogvConfirmImport

            For I As Integer = .Columns.Count - 1 To 0 Step -1
                Select Case .Columns(I).FieldName.ToString.ToUpper

                    Case "FTPONo".ToUpper, "FNGrpNo".ToUpper, "FTStyleCode".ToUpper, "FTProdTypeDescription".ToUpper, "FNFOBPrice".ToUpper, "FNStorePrice".ToUpper, "FTMapColor".ToUpper
                        .Columns(I).OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                    Case Else
                        .Columns.Remove(.Columns(I))
                End Select
            Next


            If Not (_dtsize Is Nothing) Then

                For Each R As DataRow In _dtsize.Rows

                    Dim ColG As New DevExpress.XtraGrid.Columns.GridColumn
                    With ColG
                        .Visible = True
                        .FieldName = "C" & R!FNHSysMatSizeId.ToString
                        .Name = "C" & R!FNHSysMatSizeId.ToString
                        .Caption = R!FTMapSize.ToString


                    End With

                    .Columns.Add(ColG)

                    With .Columns("C" & R!FNHSysMatSizeId.ToString)

                        .OptionsFilter.AllowAutoFilter = False
                        .OptionsFilter.AllowFilter = False
                        .DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                        .DisplayFormat.FormatString = "{0:n0}"
                        .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                        .AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far

                        With .OptionsColumn
                            .AllowMove = False
                            .AllowGroup = DevExpress.Utils.DefaultBoolean.False
                            .AllowSort = DevExpress.Utils.DefaultBoolean.False
                            .AllowEdit = False
                            .AllowMove = False

                        End With

                    End With

                    .Columns("C" & R!FNHSysMatSizeId.ToString).Summary.Add(DevExpress.Data.SummaryItemType.Sum)
                    .Columns("C" & R!FNHSysMatSizeId.ToString).SummaryItem.DisplayFormat = "{0:n0}"
                    .Columns("C" & R!FNHSysMatSizeId.ToString).Width = 60

                Next

            End If

        End With

        Me.ogdConfirmImport.DataSource = _dt

    End Sub

    Private Sub ShowCMSData()
        Dim _Qry As String = ""
        Dim _dt As DataTable
        _Qry = " SELECT A.FTComitNo,A.FTStyleCode,FTOrderNoRef,FTPORef"
        _Qry &= vbCrLf & " 	,CASE WHEN ISDATE(FDShipDate) = 1 THEN Convert(datetime,FDShipDate)  ELSE NULL END AS FDShipDate"
        _Qry &= vbCrLf & " 	,FTCountry,FTColor,FTMapColor,FTMapSize,FNFOBPrice,B.FNPrice"
        _Qry &= vbCrLf & " 	,CASE WHEN B.FTOrderNo IS NULL  THEN '1' ELSE '0' END AS FTSTateColor"
        _Qry &= vbCrLf & " 	,CASE WHEN B.FNPrice IS NULL  THEN '1'  ELSE CASE WHEN B.FNPrice <>  A.FNFOBPrice THEN '1' ELSE '0' END  END AS FTSTatePrice"
        _Qry &= vbCrLf & "   FROM"
        _Qry &= vbCrLf & "  (SELECT B.FTComitNo, ST.FTStyleCode, B.FTOrderNoRef, B.FTColor, B.FTMapColor, B.FTMapSize"
        _Qry &= vbCrLf & " 	, B.FNFOBPrice, A.FDShipDate, A.FNQuantity, A.FTCountry, A.FTPORef"
        _Qry &= vbCrLf & "  FROM      [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempImportOrderTargetCMS_D AS A WITH(NOLOCK) INNER JOIN"
        _Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempImportOrderTargetCMS AS B  WITH(NOLOCK)  ON A.FTUserLogin = B.FTUserLogin AND A.FTItem = B.FTItem AND A.FTColor = B.FTColor INNER JOIN"
        _Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS ST  WITH(NOLOCK) ON B.FNHSysStyleId = ST.FNHSysStyleId"
        _Qry &= vbCrLf & "  WHERE  (A.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "')"
        _Qry &= vbCrLf & "  ) AS A LEFT OUTER JOIN"
        _Qry &= vbCrLf & "  (SELECT FTOrderNo, FTColorway, FTSizeBreakDown, MAX(FNPrice) AS FNPrice"
        _Qry &= vbCrLf & "  FROM     [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_BreakDown AS A WITH (NOLOCK)"
        _Qry &= vbCrLf & "  GROUP BY FTOrderNo, FTColorway, FTSizeBreakDown) AS B ON A.FTOrderNoRef = B.FTOrderNo AND A.FTMapColor = B.FTColorway AND A.FTMapSize = B.FTSizeBreakDown"
        _Qry &= vbCrLf & "  ORDER BY A.FTStyleCode,A.FDShipDate"

        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

        ogccms.DataSource = _dt.Copy
    End Sub
#End Region
#Region "Procedure And Function"

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.

        '===================================================== _wMapSizeImportOrder =======================================================
        _wMapSizeImportOrder = New wMapSizeImportOrder(Nothing)

        HI.TL.HandlerControl.AddHandlerObj(_wMapSizeImportOrder)

        Dim oSysLang As New HI.ST.SysLanguage

        Try
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleID, _wMapSizeImportOrder.Name.ToString.Trim, _wMapSizeImportOrder)
        Catch ex As Exception
            '...Nothing 
        End Try
        '==================================================================================================================================
        Call InitGridPMC(Nothing, Nothing)

    End Sub

    

    Private Function W_PRCbValidateProgrammeVendor() As Boolean
        Dim bRetProgrammeVendor As Boolean = False

        Try
  
            If Not bRetProgrammeVendor AndAlso Me.FNHSysVenderPramId.Text.Trim() <> "" Then
                'bRetProgrammeVendor = True

                If Not bRetProgrammeVendor AndAlso Me.FNHSysSeasonId.Text.Trim() <> "" Then
                    'bRetProgrammeVendor = True

                    If Not bRetProgrammeVendor AndAlso Me.FNHSysUnitId.Text.Trim <> "" Then
                        'bRetProgrammeVendor = True

                        If Not bRetProgrammeVendor AndAlso Me.FNHSysCurId.Text.Trim <> "" Then
                            'bRetProgrammeVendor = True

                            If Not bRetProgrammeVendor AndAlso Me.FDOrderDate.Text.Trim() <> "" Then
                                bRetProgrammeVendor = True
                            Else
                                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.FDOrderDate_lbl.Text)
                                Me.FDOrderDate.Focus()
                            End If

                        Else
                            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.FNHSysCurId_lbl.Text)
                            Me.FNHSysCurId.Focus()
                        End If

                    Else
                        HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.FNHSysUnitId_lbl.Text)
                        Me.FNHSysUnitId.Focus()
                    End If

                Else
                    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.FNHSysSeasonId_lbl.Text)
                    Me.FNHSysSeasonId.Focus()
                End If

            Else
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.FNHSysVenderPramId_lbl.Text)
                Me.FNHSysVenderPramId.Focus()
            End If

        Catch ex As Exception
           
        End Try

        Return bRetProgrammeVendor

    End Function

    Private Function W_PRCbValidateConfirmGenerateFactoryOrder() As Boolean
        Dim _bRet As Boolean = False
        Try
            If Me.FNHSysCustId.Text.Trim() <> "" Then
                If Me.FNHSysVenderPramId.Text.Trim() <> "" Then
                    If Me.FNHSysCmpRunId.Text.Trim() <> "" Then
                        If Me.FNHSysBuyId.Text.Trim() <> "" Then
                            If Me.FNHSysCurId.Text.Trim <> "" Then

                                If Me.FDOrderDate.Text <> "" Then
                                    'If W_PRCbAddNewStyleImport() = True Then
                                    _bRet = True
                                    'End If
                                Else
                                    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, Me.FDOrderDate_lbl.Text)
                                    Me.FDOrderDate.Focus()
                                End If

                            Else
                                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.FNHSysCurId_lbl.Text)
                                Me.FNHSysCurId.Focus()
                            End If

                        Else
                            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.FNHSysBuyId_lbl.Text)
                            Me.FNHSysBuyId.Focus()
                        End If
                    Else
                        HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.FNHSysCmpRunId_lbl.Text)
                        Me.FNHSysCmpRunId.Focus()
                    End If
                Else
                    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.FNHSysVenderPramId_lbl.Text)
                    Me.FNHSysVenderPramId.Focus()
                End If
            Else
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.FNHSysCustId_lbl.Text)
                Me.FNHSysCustId.Focus()
            End If
        Catch ex As Exception

        End Try

        Return _bRet

    End Function

    Private Sub W_PRCxInitialGridBandView()
        Dim bRet As Boolean = _bInitialGridBandView

        Dim oDBdtMatSize As System.Data.DataTable

        tSql = ""
        tSql = ";WITH cteImportSize(FTMatSize)"
        tSql &= Environment.NewLine & "AS (SELECT DISTINCT L1.FTSizeBreakdownCode"
        tSql &= Environment.NewLine & "    FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTImportSalesmanSizeBreakdownTemp] AS L1"
        tSql &= Environment.NewLine & "    WHERE L1.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
        tSql &= Environment.NewLine & "    )"
        tSql &= Environment.NewLine & "--SELECT B.FNHSysMatSizeId, A.FTMatSize, B.FTMatSizeNameEN, B.FTMatSizeNameEN"
        tSql &= Environment.NewLine & "SELECT B.FTMatSizeCode"
        tSql &= Environment.NewLine & "FROM cteImportSize AS A INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMatSize] AS B WITH(NOLOCK) ON A.FTMatSize = B.FTMatSizeCode"
        tSql &= Environment.NewLine & "ORDER BY B.FNMatSizeSeq ASC;"

        oDBdtMatSize = HI.Conn.SQLConn.GetDataTable(tSql, HI.Conn.DB.DataBaseName.DB_MASTER)

        Try
            Dim oGridBandedView As DevExpress.XtraGrid.Views.Grid.GridView = Me.ogdConfirmImport.Views(0)

            oGridBandedView.ClearDocument()

            ' Make the group footers always visible.
            oGridBandedView.GroupFooterShowMode = GroupFooterShowMode.VisibleAlways

            oGridBandedView.Columns("FTMerTeamCode").Group()
            oGridBandedView.Columns("FTMerTeamCode").SortIndex = 0
            oGridBandedView.Columns("FTMerTeamCode").SortOrder = DevExpress.Data.ColumnSortOrder.Ascending

            oGridBandedView.Columns("FTPONo").Group()
            oGridBandedView.Columns("FTPONo").SortIndex = 1
            oGridBandedView.Columns("FTPONo").SortOrder = DevExpress.Data.ColumnSortOrder.Ascending

            If Not oDBdtMatSize Is Nothing AndAlso oDBdtMatSize.Rows.Count > 0 Then

                Dim nLoopMatSize As Integer


                For nLoopMatSize = 0 To oDBdtMatSize.Rows.Count - 1
                    Dim oItemSummary As GridGroupSummaryItem = New GridGroupSummaryItem
                    oItemSummary.FieldName = "FNQuantity" & oDBdtMatSize.Rows(nLoopMatSize).Item("FTMatSizeCode").ToString()
                    oItemSummary.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    oItemSummary.DisplayFormat = "{0:n0}"
                    oItemSummary.ShowInGroupColumnFooter = oGridBandedView.Columns("FNQuantity" & oDBdtMatSize.Rows(nLoopMatSize).Item("FTMatSizeCode").ToString())
                    oGridBandedView.GroupSummary.Add(oItemSummary)

                Next nLoopMatSize

            End If

            oGridBandedView.GroupFooterShowMode = GroupFooterShowMode.VisibleIfExpanded
            oGridBandedView.ExpandAllGroups()
            oGridBandedView.OptionsView.ColumnAutoWidth = False
            oGridBandedView.RefreshData()

            _bInitialGridBandView = True

        Catch ex As Exception
            _bInitialGridBandView = False
        End Try

    End Sub

    Private Function PRCnUpdateImportTime(ByVal paramImportTime As Integer) As Integer
        Static nCntImport As Integer = 0 'The Static variable nCntImport is initialized to 0 only one time
        System.Diagnostics.Debug.Print(String.Format("nCntImport before update : {0}", nCntImport))
        nCntImport = nCntImport + paramImportTime
        System.Diagnostics.Debug.Print(String.Format("nCntImport after update : {0}", nCntImport))
        Return nCntImport
    End Function

    Private Function W_PRCbClsr() As Boolean
        Try
   
            'II
            Me.ogdConfirmImport.DataSource = Nothing
            Me.ogdConfirmImport.Refresh()
            Call InitGridPMC(Nothing, Nothing)

            'Call W_PRCxInitialGridBandView()

            _bCatchSrcFile = False

            HI.TL.HandlerControl.ClearControl(Me)

            Me.FNHSysCustId.Focus()
            Me.FNHSysCustId.SelectionStart = 0
            Me.FNHSysCustId.SelectionLength = Len(Me.FNHSysCustId.Text)

            Me.FNHSysUnitId.Text = "PCS"
            Me.FNHSysCurId.Text = "US"

        Catch ex As Exception
        End Try

        Return True

    End Function

    Private Shared Function W_PRCbValidateIsFileLocked(exception As Exception) As Boolean
        Dim errorCode As Integer = Marshal.GetHRForException(exception) And ((1 << 16) - 1)
        Return errorCode = 32 OrElse errorCode = 33
    End Function

    ' This function checks to see if a file is open or not. If the file is
    ' already open, it returns True. If the file is not open, it returns
    ' False. Otherwise, a run-time error occurs because there is
    ' some other problem accessing the file.

    'Function IsFileOpen(filename As String)
    '    Dim filenum As Integer, errnum As Integer

    '    On Error Resume Next   ' Turn error checking off.
    '    filenum = FreeFile()   ' Get a free file number.
    '    ' Attempt to open the file and lock it.
    '    Open filename For Input Lock Read As #filenum
    '    Close #filenum          ' Close the file.
    '    errnum = CInt(Err.ToString)           ' Save the error number that occurred.
    '    On Error GoTo 0        ' Turn error checking back on.
    '    ' Check to see which error occurred.
    '    Select Case errnum
    '        ' No error occurred.
    '        ' File is NOT already open by another user.
    '        Case 0
    '            IsFileOpen = False
    '            ' Error number for "Permission Denied."
    '            ' File is already opened by another user.
    '        Case 70
    '            IsFileOpen = True
    '            ' Another error occurred.
    '        Case Else
    '            Error errnum
    '    End Select

    'End Function

   
   

    Private Function W_PRCbValidateMatchMerMainCategoryType(ByVal oDBdt As System.Data.DataTable) As Boolean
        Dim bRet As Boolean = False

        Try
            If Not oDBdt Is Nothing And oDBdt.Rows.Count > 0 Then

                Dim oDBdtSrc As System.Data.DataTable

                oDBdtSrc = oDBdt.Copy()

                Dim tColMerMainCategoryType As String = "F37"

                Dim tTexValidateMainCategoryType As String

                tTexValidateMainCategoryType = ""

                For Each oDataRowColMainCategoryType As System.Data.DataRow In oDBdtSrc.Rows
                    For Each oColMainCategoryXX As System.Data.DataColumn In oDBdtSrc.Columns
                        Select Case oColMainCategoryXX.ColumnName.ToString.ToUpper
                            Case tColMerMainCategoryType.ToUpper
                                tTexValidateMainCategoryType = tTexValidateMainCategoryType & oDataRowColMainCategoryType.Item(oColMainCategoryXX.ColumnName.ToString)
                            Case Else
                                '...Nothing 
                        End Select

                    Next

                Next

                '...validate column merchandiser team
                If tTexValidateMainCategoryType <> "" Then

                    Dim tMerChanMainCategoryType As List(Of String) = oDBdtSrc.AsEnumerable() _
                                                                  .Select(Function(r) r.Field(Of String)(tColMerMainCategoryType)) _
                                                                  .Distinct() _
                                                                  .ToList()

                    If Not tMerChanMainCategoryType Is Nothing And tMerChanMainCategoryType.Count > 0 Then
                        '...ถ้ามีบางรายการ ของแถวข้อมูล สำหรับ คอลัมน์ Main Category Type เป็นค่าว่าง หรือ blank
                        Dim oDBdtMerMainCategoryType As System.Data.DataTable

                        tSql = ""
                        tSql = "SELECT A.FTMainCategoryCode"
                        tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TMERMMainCategoryType AS A (NOLOCK)"
                        tSql &= Environment.NewLine & "ORDER BY A.FTMainCategoryCode ASC;"

                        oDBdtMerMainCategoryType = HI.Conn.SQLConn.GetDataTable(tSql, HI.Conn.DB.DataBaseName.DB_MASTER)

                        If Not oDBdtMerMainCategoryType Is Nothing And oDBdtMerMainCategoryType.Rows.Count > 0 Then

                            Dim bFoundMerMainCategoryType As Boolean = True

                            For Each s As String In tMerChanMainCategoryType

                                If Not s Is Nothing Then

                                    If s.Trim() <> "" Then

                                        Dim oDataRow As DataRow()

                                        Try
                                            oDataRow = oDBdtMerMainCategoryType.Select("FTMainCategoryCode = '" & HI.UL.ULF.rpQuoted(s.ToString().Trim()) & "'")
                                            If oDataRow.Length > 0 Then
                                                '...valid team merchandiser
                                                If oDataRow(0)("FTMainCategoryCode") <> "" Then
                                                    'MsgBox("Merchan Team : " & s.ToString())
                                                    'bFoundMerMainCategoryType = True
                                                Else
                                                    '...do nothing
                                                End If

                                            Else

                                                Select Case HI.ST.Lang.Language
                                                    Case HI.ST.Lang.eLang.TH
                                                        MsgBox("ไม่พบข้อมูลประเภทผลิตภัณฑ์ในระบบข้อมูลหลัก" & Environment.NewLine & "รายการประเภทผลิตภัณฑ์ : " & s.ToString() & Environment.NewLine & "หรือยังไม่มีการกำหนดข้อมูลหลักรายการประเภทผลิตภัณฑ์ !!!" & Environment.NewLine & "{FB:Football, BB:Basketball}" & Environment.NewLine & "กรุณาทำการตรวจสอบอีกครั้ง", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
                                                    Case Else
                                                        MsgBox("Invalid Main Category Type " & Environment.NewLine & "Or Not provide for main category type master file !!!" & Environment.NewLine & "Main Category Type : " & s.ToString() & Environment.NewLine & "{FB:Football, BB:Basketball}", MsgBoxStyle.OkOnly + MsgBoxStyle.Exclamation, My.Application.Info.Title)
                                                End Select

                                                bFoundMerMainCategoryType = False

                                                Exit For

                                            End If

                                        Catch ex As Exception
                                        End Try

                                    Else

                                        Select Case HI.ST.Lang.Language
                                            Case HI.ST.Lang.eLang.TH
                                                MsgBox("ไฟล์นำเข้ารายการใบสั่งผลิตอัตโนมัติยังไม่มีการกำหนดรายการข้อมูลประเภทผลิตภัณฑ์ หรือ กำหนดข้อมูลไม่ครบถ้วนกรุณาตรวจสอบอีกครั้ง !!!" & Environment.NewLine & "{FB:Football, BB:Basketball}", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
                                            Case Else
                                                MsgBox("Source file row item not complete or not yet specify main category type code" & Environment.NewLine & "{FB:Football, BB:Basketball} !!!", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
                                        End Select

                                        bFoundMerMainCategoryType = False

                                        Exit For

                                    End If

                                Else

                                    Select Case HI.ST.Lang.Language
                                        Case HI.ST.Lang.eLang.TH
                                            MsgBox("ไม่พบรายการข้อมูลประเภทผลิตภัณฑ์ !!!" & Environment.NewLine & "{FB:Football, BB:Basketball}", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
                                        Case Else
                                            MsgBox("Invalid data main category type code !!!" & Environment.NewLine & "{FB:Football, BB:Basketball}", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
                                    End Select

                                    bFoundMerMainCategoryType = False

                                    Exit For

                                End If

                            Next

                            If bFoundMerMainCategoryType = True Then
                                bRet = True
                            End If

                        End If

                        If Not oDBdtMerMainCategoryType Is Nothing Then oDBdtMerMainCategoryType.Dispose()

                    Else
                        MsgBox("รายการคอลัมน์นี้กำหนดให้เป็นข้อมูลประเภทผลิตภัณฑ์ เช่น ฟุตบอล/บาสเก็ตบอล" & Environment.NewLine & "ตัวอย่าง เช่น FB:Football, BB:Basketball !!!", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, My.Application.Info.Title)
                    End If

                Else
                    MsgBox("รายการคอลัมน์นี้กำหนดให้เป็นข้อมูลประเภทผลิตภัณฑ์ เช่น ฟุตบอล/บาสเก็ตบอล" & Environment.NewLine & "ตัวอย่าง เช่น FB:Football, BB:Basketball !!!", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, My.Application.Info.Title)
                End If

            Else
                '...do nothing
            End If

        Catch ex As Exception
           
        End Try

        Return bRet

    End Function

    Private Function W_PRCbValidateMatchMerchanTeam(ByVal oDBdt As System.Data.DataTable) As Boolean
        Dim bRet As Boolean = False

        Try
            If Not oDBdt Is Nothing And oDBdt.Rows.Count > 0 Then

                Dim oDBdtSrc As System.Data.DataTable

                oDBdtSrc = oDBdt.Copy()

                Dim tColMerchanTeam As String = "F5"

                Dim tTexValidateMerchandiserTeam As String

                tTexValidateMerchandiserTeam = ""

                For Each oDataRowColMerchanTeam As System.Data.DataRow In oDBdtSrc.Rows
                    For Each oColMerchanTeamXX As System.Data.DataColumn In oDBdtSrc.Columns
                        Select Case oColMerchanTeamXX.ColumnName.ToString.ToUpper
                            Case tColMerchanTeam.ToUpper
                                tTexValidateMerchandiserTeam = tTexValidateMerchandiserTeam & oDataRowColMerchanTeam.Item(oColMerchanTeamXX.ColumnName.ToString)
                            Case Else
                                '...Nothing 
                        End Select

                    Next

                Next

                '...validate column merchandiser team
                If tTexValidateMerchandiserTeam <> "" Then

                    Dim tMerChanTeam As List(Of String) = oDBdtSrc.AsEnumerable() _
                                                                  .Select(Function(r) r.Field(Of String)(tColMerchanTeam)) _
                                                                  .Distinct() _
                                                                  .ToList()

                    If Not tMerChanTeam Is Nothing And tMerChanTeam.Count > 0 Then
                        '...ถ้ามีบางรายการ ของแถวข้อมูล สำหรับ คอลัมน์ Merchandiser Team เป็นค่าว่าง หรือ blank
                        Dim oDBdtMerchanTeam As System.Data.DataTable

                        tSql = ""
                        tSql = "SELECT A.FTMerTeamCode"
                        tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMerTeam] AS A WITH(NOLOCK)"
                        tSql &= Environment.NewLine & "ORDER BY A.FNHSysMerTeamId ASC;"

                        oDBdtMerchanTeam = HI.Conn.SQLConn.GetDataTable(tSql, HI.Conn.DB.DataBaseName.DB_MASTER)

                        If Not oDBdtMerchanTeam Is Nothing And oDBdtMerchanTeam.Rows.Count > 0 Then
                            Dim bFoundMerTeam As Boolean = True

                            For Each s As String In tMerChanTeam

                                If Not s Is Nothing Then

                                    If s.Trim() <> "" Then

                                        Dim oDataRow As DataRow()

                                        Try
                                            oDataRow = oDBdtMerchanTeam.Select("FTMerTeamCode = '" & HI.UL.ULF.rpQuoted(s.ToString().Trim()) & "'")
                                            If oDataRow.Length > 0 Then
                                                '...valid team merchandiser
                                                If oDataRow(0)("FTMerTeamCode") <> "" Then
                                                    'MsgBox("Merchan Team : " & s.ToString())
                                                    'bFoundMerTeam = True
                                                Else
                                                    '...do nothing
                                                End If

                                            Else

                                                Select Case HI.ST.Lang.Language
                                                    Case HI.ST.Lang.eLang.TH
                                                        MsgBox("ไม่พบข้อมูลทีมเมอร์แชนไดเซอร์ในระบบข้อมูลหลัก" & Environment.NewLine & "รายการรหัสทีม : " & s.ToString() & Environment.NewLine & "หรือยังไม่มีการกำหนดข้อมูลหลักรายการทีมเมอร์แชนไดเซอร์ !!!" & Environment.NewLine & "กรุณาทำการตรวจสอบอีกครั้ง", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
                                                    Case Else
                                                        MsgBox("Invalid Merchandiser Team " & Environment.NewLine & "Or Not provide for merchandiser team master file !!!" & Environment.NewLine & "Merchandiser Team : " & s.ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Exclamation, My.Application.Info.Title)
                                                End Select

                                                bFoundMerTeam = False

                                                Exit For

                                            End If

                                        Catch ex As Exception
                                        End Try

                                    Else

                                        Select Case HI.ST.Lang.Language
                                            Case HI.ST.Lang.eLang.TH
                                                MsgBox("ไฟล์นำเข้ารายการใบสั่งผลิตอัตโนมัติยังไม่มีการกำหนดรายการข้อมูลทีมเมอร์แชนไดเซอร์ หรือ กำหนดข้อมูลไม่ครบถ้วนกรุณาตรวจสอบอีกครั้ง !!!", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
                                            Case Else
                                                MsgBox("Source file row item not complete or not yet specify merchandiser team code !!!", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
                                        End Select

                                        bFoundMerTeam = False

                                        Exit For

                                    End If

                                Else

                                    Select Case HI.ST.Lang.Language
                                        Case HI.ST.Lang.eLang.TH
                                            MsgBox("ไม่พบรายการข้อมูลทีมเมอร์แชนไดเซอร์ !!!", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
                                        Case Else
                                            MsgBox("Invalid data merchandiser team code !!!", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
                                    End Select

                                    bFoundMerTeam = False

                                    Exit For

                                End If

                            Next

                            If bFoundMerTeam = True Then
                                bRet = True
                            End If

                        End If

                        If Not oDBdtMerchanTeam Is Nothing Then oDBdtMerchanTeam.Dispose()

                    Else
                        MsgBox("รายการคอลัมน์นี้กำหนดให้เป็นข้อมูลทีมเมอร์แชนไดเซอร์" & Environment.NewLine & "ซึ่งท่านจะต้องทำการกำหนดข้อมูลรายการทีมเมอร์แชนไดเซอร์ !!!", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, My.Application.Info.Title)
                    End If

                Else
                    MsgBox("รายการคอลัมน์นี้กำหนดให้เป็นข้อมูลทีมเมอร์แชนไดเซอร์" & Environment.NewLine & "ซึ่งท่านจะต้องทำการกำหนดข้อมูลรายการทีมเมอร์แชนไดเซอร์ !!!", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, My.Application.Info.Title)
                End If

            Else
                '...do nothing
            End If

        Catch ex As Exception
           
        End Try

        Return bRet

    End Function

    Private Sub W_PRCxValidateMaster(ByVal oDBdt As System.Data.DataTable)

       
        '========================================= Validate Map Size ============================================
        Dim tmpDTNotMapSize As New System.Data.DataTable

        Dim oColFTSizeCodeNotExists As System.Data.DataColumn
        oColFTSizeCodeNotExists = New System.Data.DataColumn("FTSizeCodeNotExists", System.Type.GetType("System.String"))
        oColFTSizeCodeNotExists.Caption = "FTSizeCodeNotExists"

        tmpDTNotMapSize.Columns.Add(oColFTSizeCodeNotExists.ColumnName, oColFTSizeCodeNotExists.DataType)

        Dim oColMapSizeExtend As System.Data.DataColumn
        oColMapSizeExtend = New System.Data.DataColumn("FTMapSizeExtend", System.Type.GetType("System.String"))
        oColMapSizeExtend.Caption = "FTMapSizeExtend"

        tmpDTNotMapSize.Columns.Add(oColMapSizeExtend.ColumnName, oColMapSizeExtend.DataType)

        If Not tmpDTNotMapSize Is Nothing AndAlso tmpDTNotMapSize.Rows.Count > 0 Then
            _oSplash.TopMost = False
            _oSplash.Refresh()

            _wMapSizeImportOrder = New wMapSizeImportOrder(tmpDTNotMapSize)

            HI.TL.HandlerControl.AddHandlerObj(_wMapSizeImportOrder)

            Dim oSysLang As New HI.ST.SysLanguage

            Try
                Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleID, _wMapSizeImportOrder.Name.ToString.Trim, _wMapSizeImportOrder)

                Call HI.ST.Lang.SP_SETxLanguage(_wMapSizeImportOrder)

                _wMapSizeImportOrder.TopMost = True
                _wMapSizeImportOrder.Refresh()

                _oDBdtUserImportMapSizeManual = Nothing

                With _wMapSizeImportOrder
                    .DTUserImportMapSize = Nothing
                    If .ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                        _oDBdtUserImportMapSizeManual = .DTUserImportMapSize.Copy()
                    Else

                        If Not DBNull.Value.Equals(tmpDTNotMapSize) And tmpDTNotMapSize.Rows.Count > 0 Then

                            For Each oDataRowNotMapSize As System.Data.DataRow In tmpDTNotMapSize.Rows
                                '...add new FNHSysMatSizeId:FTMatSizeCode in System TMERMMatSize [HITECH_MASTER]
                                Dim nFNHSysMatSizeId As Integer

                                nFNHSysMatSizeId = Val(HI.TL.RunID.GetRunNoID("TMERMMatSize", "FNHSysMatSizeId", HI.Conn.DB.DataBaseName.DB_MASTER).ToString())

                                tSql = ""
                                tSql = "DECLARE @FNMatSizeSeqMax AS INT;"
                                tSql &= Environment.NewLine & "SELECT @FNMatSizeSeqMax = MAX(A.FNMatSizeSeq)"
                                tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMatSize] AS A WITH(NOLOCK)"
                                tSql &= Environment.NewLine & "GROUP BY A.FNHSysMatSizeId;"
                                tSql &= Environment.NewLine & "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMatSize] ([FTInsUser],[FDInsDate],[FTInsTime]"
                                tSql &= Environment.NewLine & "                                                                                                    ,[FTUpdUser],[FDUpdDate],[FTUpdTime]"
                                tSql &= Environment.NewLine & "                                                                                                    ,[FNHSysMatSizeId],[FTMatSizeCode],[FNMatSizeSeq]"
                                tSql &= Environment.NewLine & "							                                                                           ,[FTMatSizeNameTH],[FTMatSizeNameEN],[FTRemark],[FTStateActive])"
                                tSql &= Environment.NewLine & "VALUES(NULL, NULL, NULL"
                                tSql &= ",NULL, NULL, NULL"
                                tSql &= ", " & nFNHSysMatSizeId & ", N'" & HI.UL.ULF.rpQuoted(oDataRowNotMapSize!FTSizeCodeNotExists.ToString) & "', (ISNULL(@FNMatSizeSeqMax, 0) + 1)"
                                tSql &= ", N'" & HI.UL.ULF.rpQuoted(oDataRowNotMapSize!FTSizeCodeNotExists.ToString) & "', N'" & HI.UL.ULF.rpQuoted(oDataRowNotMapSize!FTSizeCodeNotExists.ToString) & "', '', '1');"

                                If HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MASTER) = True Then
                                    

                                End If

                            Next

                        End If

                    End If

                End With

                _wMapSizeImportOrder.TopMost = False
                _wMapSizeImportOrder.Refresh()

            Catch ex As Exception
               
            End Try

            _oSplash.TopMost = True
            _oSplash.Refresh()

        End If

        If Not tmpDTNotMapSize Is Nothing Then tmpDTNotMapSize.Dispose()
        '========================================================================================================

        Call W_PRCbValidateExistsMasterProductType(oDBdt) '******************************************************

    End Sub

    Private Function W_PRCbValidateExistsMasterProductType(ByVal oDBdt As System.Data.DataTable) As Boolean
        Dim bRet As Boolean = False

        Try
            If Not oDBdt Is Nothing And oDBdt.Rows.Count > 0 Then

                Dim tColCategoryCode As String = "F37" '...AK : FB:Footbal, BB:Basketball
                Dim tColCategoryExtend As String = "F36" '...AJ : CMO_Sponsor_Bus_Org
                Dim tColSubCategoryExtend As String = "F38" '...AL : CMO_Sponsor_Consumer_Purpose

                '---------------------------------------------------------------------------------------------------------------------------------
                Dim DTMatProductTypeTemp As System.Data.DataTable

                DTMatProductTypeTemp = New System.Data.DataTable

                Dim oColFTProdTypeCode As System.Data.DataColumn
                oColFTProdTypeCode = New System.Data.DataColumn("FTProdTypeCode", System.Type.GetType("System.String"))
                oColFTProdTypeCode.Caption = "FTProdTypeCode"
                DTMatProductTypeTemp.Columns.Add(oColFTProdTypeCode.ColumnName, oColFTProdTypeCode.DataType)

                Dim oColFTProdTypeDesc As System.Data.DataColumn
                oColFTProdTypeDesc = New System.Data.DataColumn("FTProdTypeDesc", System.Type.GetType("System.String"))
                oColFTProdTypeDesc.Caption = "FTProdTypeDesc"

                DTMatProductTypeTemp.Columns.Add(oColFTProdTypeDesc.ColumnName, oColFTProdTypeDesc.DataType)

                Dim oColFTProdTypeRemark As System.Data.DataColumn
                oColFTProdTypeRemark = New System.Data.DataColumn("FTProdTypeRemark", System.Type.GetType("System.String"))
                oColFTProdTypeRemark.Caption = "FTProdTypeRemark"

                DTMatProductTypeTemp.Columns.Add(oColFTProdTypeRemark.ColumnName, oColFTProdTypeRemark.DataType)

                '---------------------------------------------------------------------------------------------------------------------------------

                Dim tTextCategoryCode As String
                Dim tTextCategoryExtend As String
                Dim tTextSubCategoryExtend As String

                For Each oRowProdType As System.Data.DataRow In oDBdt.Rows

                    tTextCategoryCode = "" : tTextCategoryExtend = "" : tTextSubCategoryExtend = ""

                    If Not DBNull.Value.Equals(oRowProdType.Item(tColCategoryCode)) Then
                        tTextCategoryCode = oRowProdType.Item(tColCategoryCode).ToString.Trim

                        If Not DBNull.Value.Equals(oRowProdType.Item(tColCategoryExtend)) Then
                            tTextCategoryExtend = oRowProdType.Item(tColCategoryExtend).ToString.Trim
                        End If

                        If Not DBNull.Value.Equals(oRowProdType.Item(tColSubCategoryExtend)) Then
                            tTextSubCategoryExtend = oRowProdType.Item(tColSubCategoryExtend).ToString.Trim
                        End If

                    End If

                    If tTextCategoryCode <> "" And tTextCategoryExtend <> "" And tTextSubCategoryExtend <> "" Then

                        Dim oRowAppendProdType As System.Data.DataRow

                        If Not DTMatProductTypeTemp Is Nothing And DTMatProductTypeTemp.Rows.Count > 0 Then
                            Dim oRowProdTypeNotExists As System.Data.DataRow()

                            oRowProdTypeNotExists = DTMatProductTypeTemp.Select("FTProdTypeCode = '" & HI.UL.ULF.rpQuoted(tTextCategoryCode) & "' AND FTProdTypeDesc = '" & HI.UL.ULF.rpQuoted(tTextCategoryExtend) & "' AND FTProdTypeRemark = '" & HI.UL.ULF.rpQuoted(tTextSubCategoryExtend) & "'")

                            If oRowProdTypeNotExists.Length > 0 Then
                                '...already exists record
                            Else
                                oRowAppendProdType = DTMatProductTypeTemp.NewRow()
                                oRowAppendProdType.Item("FTProdTypeCode") = tTextCategoryCode
                                oRowAppendProdType.Item("FTProdTypeDesc") = tTextCategoryExtend
                                oRowAppendProdType.Item("FTProdTypeRemark") = tTextSubCategoryExtend
                                DTMatProductTypeTemp.Rows.Add(oRowAppendProdType)
                            End If
                        Else
                            oRowAppendProdType = DTMatProductTypeTemp.NewRow()
                            oRowAppendProdType.Item("FTProdTypeCode") = tTextCategoryCode
                            oRowAppendProdType.Item("FTProdTypeDesc") = tTextCategoryExtend
                            oRowAppendProdType.Item("FTProdTypeRemark") = tTextSubCategoryExtend
                            DTMatProductTypeTemp.Rows.Add(oRowAppendProdType)
                        End If

                    End If

                Next

                If Not DTMatProductTypeTemp Is Nothing Then DTMatProductTypeTemp.AcceptChanges()

                If Not DTMatProductTypeTemp Is Nothing AndAlso DTMatProductTypeTemp.Rows.Count > 0 Then

                    Dim tCategoryCode As String = ""
                    Dim tCategoryExtend As String = ""
                    Dim tSubCategoryExtend As String = ""

                    Dim oStrBuilder As New System.Text.StringBuilder()

                    For Each oRowMatProdType As System.Data.DataRow In DTMatProductTypeTemp.Rows

                        tCategoryCode = "" : tCategoryExtend = "" : tSubCategoryExtend = ""

                        If Not DBNull.Value.Equals(oRowMatProdType.Item("FTProdTypeCode")) Then
                            tCategoryCode = oRowMatProdType.Item("FTProdTypeCode").ToString.Trim

                            If tCategoryCode <> "" AndAlso Not DBNull.Value.Equals(oRowMatProdType.Item("FTProdTypeDesc")) Then
                                tCategoryExtend = oRowMatProdType.Item("FTProdTypeDesc").ToString.Trim

                                If tCategoryExtend <> "" AndAlso Not DBNull.Value.Equals(oRowMatProdType.Item("FTProdTypeRemark")) Then
                                    tSubCategoryExtend = oRowMatProdType.Item("FTProdTypeRemark").ToString.Trim

                                    If tSubCategoryExtend <> "" Then

                                        Dim tProductTypeCode As String
                                        tProductTypeCode = tCategoryCode.Trim() & Mid$(tCategoryExtend.Trim(), 1, 1) & Mid$(tSubCategoryExtend.Trim(), 1, 1)

                                        Dim tProductTypeDesc As String
                                        tProductTypeDesc = tCategoryExtend.Trim()

                                        Dim tProductTypeRemark As String
                                        tProductTypeRemark = tCategoryExtend.Trim() & "-" & tSubCategoryExtend.Trim()

                                        Dim nFNHSysProdTypeId As Integer
                                        nFNHSysProdTypeId = Val(HI.TL.RunID.GetRunNoID("TMERMProductType", "FNHSysProdTypeId", Conn.DB.DataBaseName.DB_MASTER).ToString())
                                        '======================================================================================================================================
                                        'BB + BASKETBALL                + SPORT                       + 01        ==> (BB + B) + S + 01 ==> BBBS01:BASKETBALL-SPORT
                                        'BB + BASKETBALL                + SPORT TEAM                  + 02        ==> (BB + B) + S + 02 ==> BBBS01:BASKETBALL-SPORT TEAM
                                        'BB + BASKETBALL                + PERFORMANCE                 + 03        ==> (BB + B) + P + 01 ==> BBBS01:BASKETBALL-PERFORMANCE
                                        '======================================================================================================================================
                                        'BB + TRAINING                  + FOUNDATIONAL TRAINING       + 01        ==> BB + T + S + 01 ==> BBTS01:TRAINING-FOUNDATIONAL TRAINING
                                        'BB + JORDAN PERF BBALL         + JORDAN CORE                 + 01        ==> BB + J + J + 01 ==> BBJJ01:JORDAN PERF BBALL-JORDAN CORE
                                        'FB + FOOTBALL/SOCCER TEAMSPORT + TEAMSPORT                   + 01        ==> FB + F + T + 01 ==> FOOTBALL/SOCCER TEAMSPORT-TEAMSPORT

                                        oStrBuilder.Remove(0, oStrBuilder.Length)

                                        oStrBuilder.AppendLine("SELECT TOP 1 A.FTProdTypeCode")
                                        oStrBuilder.AppendLine("FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TMERMProductType AS A WITH(NOLOCK)")
                                        'oStrBuilder.AppendLine("WHERE (A.FTProdTypeNameTH = N'" & HI.UL.ULF.rpQuoted(tProductTypeDesc) & "' OR A.FTProdTypeNameEN = N'" & HI.UL.ULF.rpQuoted(tProductTypeDesc) & "');")
                                        oStrBuilder.AppendLine("WHERE A.FTProdTypeCode = N'" & HI.UL.ULF.rpQuoted(tProductTypeCode) & "';")

                                        If oStrBuilder.Length > 0 Then
                                            tSql = ""
                                            tSql = oStrBuilder.ToString()

                                            If HI.Conn.SQLConn.GetField(tSql, HI.Conn.DB.DataBaseName.DB_MASTER, "") = "" Then
                                                oStrBuilder.Remove(0, oStrBuilder.Length)

                                                oStrBuilder.AppendLine("INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMProductType]")
                                                oStrBuilder.AppendLine("           ([FTInsUser]")
                                                oStrBuilder.AppendLine("           ,[FDInsDate]")
                                                oStrBuilder.AppendLine("           ,[FTInsTime]")
                                                oStrBuilder.AppendLine("           ,[FTUpdUser]")
                                                oStrBuilder.AppendLine("           ,[FDUpdDate]")
                                                oStrBuilder.AppendLine("           ,[FTUpdTime]")
                                                oStrBuilder.AppendLine("           ,[FNHSysProdTypeId]")
                                                oStrBuilder.AppendLine("           ,[FTProdTypeCode]")
                                                oStrBuilder.AppendLine("           ,[FTProdTypeNameTH]")
                                                oStrBuilder.AppendLine("           ,[FTProdTypeNameEN]")
                                                oStrBuilder.AppendLine("           ,[FTRemark]")
                                                oStrBuilder.AppendLine("           ,[FTStateActive])")
                                                oStrBuilder.AppendLine(" VALUES")
                                                oStrBuilder.AppendLine(String.Format("       ({0}", "'Admin'"))
                                                oStrBuilder.AppendLine(String.Format("       ,{0}", "'2015/03/27'"))
                                                oStrBuilder.AppendLine(String.Format("       ,{0}", "'00:13:00'"))
                                                oStrBuilder.AppendLine(String.Format("       ,{0}", "NULL"))
                                                oStrBuilder.AppendLine(String.Format("       ,{0}", "NULL"))
                                                oStrBuilder.AppendLine(String.Format("       ,{0}", "NULL"))
                                                oStrBuilder.AppendLine(String.Format("       ,{0}", nFNHSysProdTypeId))
                                                oStrBuilder.AppendLine(String.Format("       ,{0}", "N'" & HI.UL.ULF.rpQuoted(tProductTypeCode) & "'"))
                                                oStrBuilder.AppendLine(String.Format("       ,{0}", "N'" & HI.UL.ULF.rpQuoted(tProductTypeDesc) & "'"))
                                                oStrBuilder.AppendLine(String.Format("       ,{0}", "N'" & HI.UL.ULF.rpQuoted(tProductTypeDesc) & "'"))
                                                oStrBuilder.AppendLine(String.Format("       ,{0}", "N'" & HI.UL.ULF.rpQuoted(tProductTypeRemark) & "'"))
                                                oStrBuilder.AppendLine(String.Format("       ,{0})", "N'1'"))

                                                If oStrBuilder.Length > 0 Then
                                                    tSql = ""
                                                    tSql = oStrBuilder.ToString()

                                                    If HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MASTER) = True Then
                                                        'MsgBox("Execute data complete...", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
                                                    Else
                                                        'MsgBox("Execute data not complete !!!", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
                                                    End If

                                                End If

                                            End If

                                        End If

                                    End If

                                End If

                            End If

                        End If

                    Next

                End If

                If Not DTMatProductTypeTemp Is Nothing Then DTMatProductTypeTemp.Dispose()

                bRet = True

            End If

        Catch ex As Exception
           
        End Try

        Return bRet

    End Function

    Private Function W_GETtMerTeamCode() As String
        Dim tMerTeamCode As String = ""
        Try
            Dim oStrBuilder As New System.Text.StringBuilder()

            oStrBuilder.Remove(0, oStrBuilder.Length)

            oStrBuilder.AppendLine("DECLARE @FTMerTeamCodeMax AS NVARCHAR(30);")
            oStrBuilder.AppendLine("DECLARE @FTMerTeamCodeRet AS NVARCHAR(30);")
            oStrBuilder.AppendLine("SELECT @FTMerTeamCodeMax = MAX(A.FTMerTeamCode)")
            oStrBuilder.AppendLine("FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMerTeam] AS A WITH(NOLOCK)")
            oStrBuilder.AppendLine("WHERE SUBSTRING(A.FTMerTeamCode, 1, 1) = N'M'")
            oStrBuilder.AppendLine("      AND ISNUMERIC(SUBSTRING(A.FTMerTeamCode, 2, LEN(A.FTMerTeamCode)-1)) = 1")
            oStrBuilder.AppendLine("GROUP BY A.FTMerTeamCode;")
            oStrBuilder.AppendLine("IF (@@ROWCOUNT > 0)")
            oStrBuilder.AppendLine("	BEGIN")
            oStrBuilder.AppendLine("		PRINT 'FTMerTeamCode Max is : ' + @FTMerTeamCodeMax;")
            oStrBuilder.AppendLine("		DECLARE @nCntMerTeam AS INT;")
            oStrBuilder.AppendLine("		SELECT @nCntMerTeam = COUNT(L1.FTMerTeamCode)")
            oStrBuilder.AppendLine("		FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMerTeam] AS L1 WITH(NOLOCK)")
            oStrBuilder.AppendLine("		WHERE SUBSTRING(L1.FTMerTeamCode, 1, 1) = N'M'")
            oStrBuilder.AppendLine("		      AND ISNUMERIC(SUBSTRING(L1.FTMerTeamCode, 2, LEN(L1.FTMerTeamCode)-1)) = 1")
            oStrBuilder.AppendLine("		IF (@@ROWCOUNT > 0)")
            oStrBuilder.AppendLine("			BEGIN")
            oStrBuilder.AppendLine("			  PRINT '@nCntMerTeam : ' + CONVERT(VARCHAR(10), (@nCntMerTeam + 1));")
            oStrBuilder.AppendLine("			  PRINT 'FTMerTeamCodeRet : ' + N'M' + CONVERT(VARCHAR(10), (@nCntMerTeam + 1));")
            oStrBuilder.AppendLine("			  SET @FTMerTeamCodeRet =  N'M' + CONVERT(NVARCHAR(29), (@nCntMerTeam + 1))")
            oStrBuilder.AppendLine("			END")
            oStrBuilder.AppendLine("		ELSE")
            oStrBuilder.AppendLine("		    BEGIN")
            oStrBuilder.AppendLine("			  PRINT 'FTMerTeamCodeRet : ' + N'M1';")
            oStrBuilder.AppendLine("			  SET @FTMerTeamCodeRet = N'M1'")
            oStrBuilder.AppendLine("		    END")
            oStrBuilder.AppendLine("	END")
            oStrBuilder.AppendLine("ELSE")
            oStrBuilder.AppendLine("	BEGIN")
            oStrBuilder.AppendLine("		SET @FTMerTeamCodeRet = N'M1'")
            oStrBuilder.AppendLine("		PRINT 'FTMerTeamCodeRet : ' + @FTMerTeamCodeRet")
            oStrBuilder.AppendLine("	END;")
            oStrBuilder.AppendLine("SELECT @FTMerTeamCodeRet AS FTMerTeamCode;	")

            tSql = ""
            tSql = oStrBuilder.ToString()

            'tMerTeamCode = HI.Conn.SQLConn.GetField(tSql, "").ToString()

            Dim oDBdt As System.Data.DataTable

            oDBdt = HI.Conn.SQLConn.GetDataTable(tSql, HI.Conn.DB.DataBaseName.DB_MASTER)

            If Not oDBdt Is Nothing And oDBdt.Rows.Count > 0 Then
                If Not DBNull.Value.Equals(oDBdt.Rows(0).Item(0)) Then
                    If oDBdt.Rows(0).Item(0).ToString() <> "" Then
                        tMerTeamCode = oDBdt.Rows(0).Item(0).ToString()
                    End If

                End If

            End If

        Catch ex As Exception

            MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)

        End Try

        Return tMerTeamCode

    End Function

    Private Function W_PRCbLoadMapSize() As System.Data.DataTable
        Dim tmpDTMapSizeMaster As System.Data.DataTable
        Try
            tSql = ""
            tSql = "SELECT A.FTMapSize, A.FTMapSizeExtension"
            tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERMMapSize] AS A WITH(NOLOCK)"
            tSql &= Environment.NewLine & "ORDER BY A.FTMapSize ASC;"

            tmpDTMapSizeMaster = HI.Conn.SQLConn.GetDataTable(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN)

        Catch ex As Exception
            Throw New Exception(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString())
            tmpDTMapSizeMaster = Nothing
        End Try

        oDBdtMapSize = tmpDTMapSizeMaster.Copy()

        If Not tmpDTMapSizeMaster Is Nothing Then tmpDTMapSizeMaster.Dispose()

        Return oDBdtMapSize

    End Function


    Private Function W_PRCbImportFactoryOrderPMC(_Spls) As Boolean
        '...last modify 2014/12/19 when drop field Amount, Qty TMERTOrder_BreakDown, TMERTOrder, TMERTOrderSub

        Dim _bImportComplete As Boolean = False

        Dim tMsgSplash As String = ""

        Try
            Dim _FNHSysMerTeamId As Integer = -1
            Dim nFNHSysCustId As Integer
            Dim nFNHSysCmpRunId As Integer
            Dim nFNHSysBuyId As Integer
            Dim nFNHSysVenderPramId As Integer
            Dim FNQtySpecialType As Integer = 0
            Dim FNQtySpecialTypeQty As Double = 0
            Dim _dtper As DataTable
            Dim _Qry As String = ""

        
            _FNHSysMerTeamId = Me.SysMerTeamId

            tSql = "SELECT TOP 1 FNQtySpecialType, FNQtySpecial "
            tSql &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMBuyGrp AS M WITH(NOLOCK) "
            tSql &= vbCrLf & " WHERE FNHSysBuyGrpId=" & Integer.Parse(Val(FNHSysBuyGrpId.Properties.Tag.ToString)) & ""
            _dtper = HI.Conn.SQLConn.GetDataTable(tSql, Conn.DB.DataBaseName.DB_MERCHAN)

            For Each R As DataRow In _dtper.Rows

                FNQtySpecialType = Integer.Parse(Val(R!FNQtySpecialType.ToString()))
                FNQtySpecialTypeQty = Double.Parse(Val(R!FNQtySpecial.ToString()))

                Exit For
            Next

            nFNHSysCustId = Val(Me.FNHSysCustId.Properties.Tag) : nFNHSysCmpRunId = Val(Me.FNHSysCmpRunId.Properties.Tag) : nFNHSysBuyId = Val(Me.FNHSysBuyId.Properties.Tag)

            tSql = ""
            tSql = "SELECT TOP 1 A.FNHSysVenderPramId FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMVenderPram] AS A WITH(NOLOCK) WHERE A.FNHSysVenderPramId = " & Val(Me.FNHSysVenderPramId.Properties.Tag)

            nFNHSysVenderPramId = Val(HI.Conn.SQLConn.GetField(tSql, HI.Conn.DB.DataBaseName.DB_MASTER, "0"))

            If nFNHSysVenderPramId <= 0 Then Return False

            Dim oDBdtImport As System.Data.DataTable

            tSql = "  SELECT  " & _FNHSysMerTeamId & " AS FNHSysMerTeamId, A.FTPONo, '' AS FTPOTrading, '' AS FTPOCreateDate, A.FTOrderDate AS FTOrderDate"
            tSql &= Environment.NewLine & "         , " & nFNHSysBuyId & " AS FNHSysBuyId, " & nFNHSysCmpRunId & " AS FNHSysCmpRunId, " & nFNHSysCustId & " AS FNHSysCustId"
            tSql &= Environment.NewLine & "    ,N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'  AS FTOrderBy, 17 AS FNOrderType, 0 AS FNJobState"
            tSql &= Environment.NewLine & "    ,A.FTStyle AS FTStyle, '' AS FDShipDate, '' AS FDShipDateOrginal, A.FNHSysBuyGrpId,A.FNHSysGenderId, A.FNHSysProdTypeId"
            tSql &= Environment.NewLine & "    ,A.FNHSysPlantId,'' AS FTYear, A.FNHSysMainCategoryId,A.FNHSysSeasonId AS FTPlanningSeason,A.FNHSysCountryId,A.FNHSysBuyerId"
            tSql &= Environment.NewLine & "   FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTTempImportOrderTargetPMC] AS A "
            tSql &= Environment.NewLine & "       WHERE A.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            tSql &= Environment.NewLine & "    AND A.FTPONo IN (SELECT MAX(L1.FTPONo)"
            tSql &= Environment.NewLine & "     FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTTempImportOrderTargetPMC AS L1 WITH(NOLOCK)"
            tSql &= Environment.NewLine & "     WHERE L1.FTUserLogin = A.FTUserLogin"
            tSql &= Environment.NewLine & "        AND L1.FTPONo = A.FTPONo"
            tSql &= Environment.NewLine & "    GROUP BY L1.FTPONo)"
            tSql &= Environment.NewLine & "   ORDER BY  A.FTStyle ASC;"

            oDBdtImport = HI.Conn.SQLConn.GetDataTable(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN)

            If Not oDBdtImport Is Nothing AndAlso oDBdtImport.Rows.Count > 0 Then
                '...generate facotry order no.
                HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_MERCHAN)
                HI.Conn.SQLConn.SqlConnectionOpen()
                HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
                HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

                tSql = ""
                tSql = "DECLARE @DBName NVARCHAR(30);"
                tSql &= Environment.NewLine & "DECLARE @TblName NVARCHAR(30);"
                tSql &= Environment.NewLine & "DECLARE @DocType NVARCHAR(30);"
                tSql &= Environment.NewLine & "DECLARE @GetFotmat NVARCHAR(30);"
                tSql &= Environment.NewLine & "DECLARE @AddPrefix NVARCHAR(30);"
                tSql &= Environment.NewLine & "SET @DBName = N'" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "';"
                tSql &= Environment.NewLine & "SET @TblName = N'TMERTOrder';"
                tSql &= Environment.NewLine & "SET @DocType = '17';"
                tSql &= Environment.NewLine & "SET @GetFotmat = '';"
                tSql &= Environment.NewLine & "SET @AddPrefix = N'" & HI.TL.CboList.GetListRefer("FNOrderType", 17) & HI.UL.ULF.rpQuoted(Me.FNHSysCmpRunId.Text.Trim()) & "';"
                tSql &= Environment.NewLine & "DECLARE @tblSrcConfigDoc AS TABLE(FTRunNo NVARCHAR(30), FTRunStr NVARCHAR(30), FNRunning INT, FNRunningNoMax INT);"
                tSql &= Environment.NewLine & "INSERT INTO @tblSrcConfigDoc(FTRunNo, FTRunStr, FNRunning, FNRunningNoMax)"
                tSql &= Environment.NewLine & "EXEC SP_GET_FACTORY_ORDERNO_MAX @DBName, @TblName, @DocType, @GetFotmat,@AddPrefix;"
                tSql &= Environment.NewLine & "DECLARE @FTRunNo        AS NVARCHAR(30);"
                tSql &= Environment.NewLine & "DECLARE @FTRunStr       AS NVARCHAR(30);"
                tSql &= Environment.NewLine & "DECLARE @FNRunning      AS INT;"
                tSql &= Environment.NewLine & "DECLARE @FNRunningNoMax AS INT;"
                tSql &= Environment.NewLine & "SELECT @FTRunNo = A.FTRunNo, @FTRunStr = A.FTRunStr, @FNRunning = A.FNRunning, @FNRunningNoMax = A.FNRunningNoMax"
                tSql &= Environment.NewLine & "FROM @tblSrcConfigDoc AS A;"
                tSql &= Environment.NewLine & "--SET @FNRunningNoMax = @FNRunningNoMax + 1;"
                tSql &= Environment.NewLine & " create table #Tab("
                tSql &= Environment.NewLine & " [FTInsUser] [nvarchar](50) NULL,"
                tSql &= Environment.NewLine & " [FDInsDate] [varchar](10) NULL,"
                tSql &= Environment.NewLine & " [FTInsTime] [varchar](8) NULL,"
                tSql &= Environment.NewLine & " [FTUpdUser] [nvarchar](50) NULL,"
                tSql &= Environment.NewLine & " [FDUpdDate] [varchar](10) NULL,"
                tSql &= Environment.NewLine & " [FTUpdTime] [varchar](8) NULL,"
                tSql &= Environment.NewLine & " [FTOrderNo] [nvarchar](30) NOT NULL,"
                tSql &= Environment.NewLine & " [FDOrderDate] [nvarchar](10) NULL,"
                tSql &= Environment.NewLine & " [FTOrderBy] [nvarchar](50) NULL,"
                tSql &= Environment.NewLine & " [FNOrderType] [int] NULL,"
                tSql &= Environment.NewLine & " [FNHSysCmpId] [int] NULL,"
                tSql &= Environment.NewLine & " [FNHSysCmpRunId] [int] NULL,"
                tSql &= Environment.NewLine & " [FNHSysStyleId] [int] NULL,"
                tSql &= Environment.NewLine & " [FTPORef] [nvarchar](50) NULL,"
                tSql &= Environment.NewLine & " [FNHSysCustId] [int] NULL,"
                tSql &= Environment.NewLine & " [FNHSysAgencyId] [int] NULL,"
                tSql &= Environment.NewLine & " [FNHSysProdTypeId] [int] NULL,"
                tSql &= Environment.NewLine & " [FNHSysBuyerId] [int] NULL,"
                tSql &= Environment.NewLine & " [FTMainMaterial] [nvarchar](500) NULL,"
                tSql &= Environment.NewLine & " [FTCombination] [nvarchar](500) NULL,"
                tSql &= Environment.NewLine & " [FTRemark] [nvarchar](1000) NULL,"
                tSql &= Environment.NewLine & " [FTStateOrderApp] [nvarchar](1) NULL,"
                tSql &= Environment.NewLine & " [FTAppBy] [nvarchar](50) NULL,"
                tSql &= Environment.NewLine & " [FDAppDate] [varchar](10) NULL,"
                tSql &= Environment.NewLine & " [FTAppTime] [varchar](8) NULL,"
                tSql &= Environment.NewLine & " [FNJobState] [int] NULL,"
                tSql &= Environment.NewLine & " [FTStateBy] [nvarchar](50) NULL,"
                tSql &= Environment.NewLine & " [FDStateDate] [varchar](10) NULL,"
                tSql &= Environment.NewLine & " [FTStateTime] [varchar](8) NULL,"
                tSql &= Environment.NewLine & " [FTImage1] [nvarchar](100) NULL,"
                tSql &= Environment.NewLine & " [FTImage2] [nvarchar](100) NULL,"
                tSql &= Environment.NewLine & " [FTImage3] [nvarchar](100) NULL,"
                tSql &= Environment.NewLine & " [FTImage4] [nvarchar](100) NULL,"
                tSql &= Environment.NewLine & " [FNHSysBrandId] [int] NULL,"
                tSql &= Environment.NewLine & " [FNHSysBuyId] [int] NULL,"
                tSql &= Environment.NewLine & " [FTCancelAppBy] [nvarchar](50) NULL,"
                tSql &= Environment.NewLine & " [FDCancelAppDate] [varchar](10) NULL,"
                tSql &= Environment.NewLine & " [FDCancelAppTime] [varchar](8) NULL,"
                tSql &= Environment.NewLine & " [FTCancelAppRemark] [nvarchar](500) NULL,"
                tSql &= Environment.NewLine & " [FTPOTradingCo] [nvarchar](30) NULL,"
                tSql &= Environment.NewLine & " [FTPOItem] [nvarchar](30) NULL,"
                tSql &= Environment.NewLine & " [FTPOCreateDate] [varchar](10) NULL,"
                tSql &= Environment.NewLine & " [FNHSysMerTeamId] [int] NULL,"
                tSql &= Environment.NewLine & " [FNHSysPlantId] [int] NULL,"
                tSql &= Environment.NewLine & " [FNHSysBuyGrpId] [int] NULL,"
                tSql &= Environment.NewLine & " [FNHSysMainCategoryId] [int] NULL,"
                tSql &= Environment.NewLine & " [FNHSysVenderPramId] [int] NULL,"
                tSql &= Environment.NewLine & " [FTOrderCreateStatus] [nvarchar](1) NULL,"
                tSql &= Environment.NewLine & " [FTImportUser] [nvarchar](50) NULL,"
                tSql &= Environment.NewLine & " [FDImportDate] [nvarchar](10) NULL,"
                tSql &= Environment.NewLine & " [FTImportTime] [nvarchar](8) NULL,"
                tSql &= Environment.NewLine & " [FNRowImport] [Int],"
                tSql &= Environment.NewLine & " [FTStyle] [nvarchar](30) NULL,"
                tSql &= Environment.NewLine & " [FNHSysSeasonId] [Int] NULL"
                tSql &= Environment.NewLine & "  )"
                tSql &= Environment.NewLine & " INSERT INTO  #Tab"
                tSql &= Environment.NewLine & "                              ([FTInsUser],[FDInsDate],[FTInsTime]"
                tSql &= Environment.NewLine & "                              ,[FTUpdUser],[FDUpdDate],[FTUpdTime]"
                tSql &= Environment.NewLine & "                              ,[FTOrderNo],[FDOrderDate],[FTOrderBy],[FNOrderType]"
                tSql &= Environment.NewLine & "                              ,[FNHSysCmpId],[FNHSysCmpRunId],[FNHSysStyleId],[FTPORef]"
                tSql &= Environment.NewLine & "                              ,[FNHSysCustId],[FNHSysAgencyId],[FNHSysProdTypeId],[FNHSysBuyerId]"
                tSql &= Environment.NewLine & "                              ,[FTMainMaterial],[FTCombination],[FTRemark]"
                tSql &= Environment.NewLine & "                              ,[FTStateOrderApp],[FTAppBy],[FDAppDate],[FTAppTime]"
                tSql &= Environment.NewLine & "                              ,[FNJobState],[FTStateBy],[FDStateDate],[FTStateTime]"
                tSql &= Environment.NewLine & "                              ,[FTImage1],[FTImage2],[FTImage3],[FTImage4]"
                tSql &= Environment.NewLine & "                              ,[FNHSysBrandId],[FNHSysBuyId],[FTCancelAppBy],[FDCancelAppDate]"
                tSql &= Environment.NewLine & "                              ,[FDCancelAppTime],[FTCancelAppRemark],[FTPOTradingCo],[FTPOItem]"
                tSql &= Environment.NewLine & "                              ,[FTPOCreateDate],[FNHSysMerTeamId],[FNHSysPlantId],[FNHSysBuyGrpId]"
                tSql &= Environment.NewLine & "                              ,[FNHSysMainCategoryId],[FNHSysVenderPramId],[FTOrderCreateStatus],[FTImportUser],[FDImportDate],[FTImportTime],[FNRowImport],[FTStyle],[FNHSysSeasonId])"
                tSql &= Environment.NewLine & " SELECT NULL AS FTInsUser, CONVERT(VARCHAR(10),GETDATE(),111) AS FDInsDate, CONVERT(VARCHAR(8),GETDATE(),114) AS FTInsTime"
                tSql &= Environment.NewLine & "     , NULL AS FTUpdUser, NULL AS FDUpdDate, NULL AS FTUpdTime"
                tSql &= Environment.NewLine & "     , @FTRunNo + (RIGHT(@FTRunStr, @FNRunning - LEN(CONVERT(VARCHAR(30) , (ROW_NUMBER() OVER( ORDER BY B.FTMerTeamCode ASC,A.FTStyle ASC,A.FTPONO ASC) + @FNRunningNoMax)))) + CONVERT(VARCHAR(30) , (ROW_NUMBER() OVER(ORDER BY B.FTMerTeamCode ASC,A.FTStyle ASC,A.FTPONO ASC) + @FNRunningNoMax))) AS FTOrderNo"
                tSql &= Environment.NewLine & "     ,  A.FTOrderDate , NULL AS FTOrderBy, 17 AS FNOrderType"
                tSql &= Environment.NewLine & "     , NULL AS FNHSysCmpId, " & nFNHSysCmpRunId & " AS FNHSysCmpRunId"
                tSql &= Environment.NewLine & "     , A.FNHSysStyleId"
                tSql &= Environment.NewLine & "     , A.FTPONo, " & nFNHSysCustId & " AS FNHSysCustId"
                tSql &= Environment.NewLine & "     , ISNULL((SELECT L1.FNHSysAgencyId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCNMAgency] AS L1 WITH(NOLOCK) WHERE L1.FTAgencyCode = N'-'),NULL) AS FNHSysAgencyId"
                'tSql &= Environment.NewLine & "     , A.FNHSysProdTypeId"
                tSql &= Environment.NewLine & "     , ISNULL((SELECT L1.FNHSysProdTypeId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMProductType] AS L1 WITH(NOLOCK) WHERE L1.FTProdTypeCode = N'-'),NULL) AS FNHSysProdTypeId"
                tSql &= Environment.NewLine & " , -1 AS FNHSysBuyerId, NULL AS FTMainMaterial, NULL AS FTCombination, NULL AS FTRemark"
                tSql &= Environment.NewLine & "     , NULL AS FTStateOrderApp, NULL AS FTAppBy, NULL AS FDAppDate, NULL AS FTAppTime"
                tSql &= Environment.NewLine & "     , 0 AS FNJobState, NULL AS FTStateBy, NULL AS FDStateDate, NULL AS FTStateTime"
                tSql &= Environment.NewLine & "     , NULL AS FTImage1, NULL AS FTImage2, NULL AS FTImage3, NULL AS FTImage4"
                tSql &= Environment.NewLine & "     , ISNULL((SELECT L1.FNHSysBrandId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMBrand] AS L1 WITH(NOLOCK) WHERE L1.FTBrandCode = N'-'),NULL) AS FNHSysBrandId"
                tSql &= Environment.NewLine & "     , " & nFNHSysBuyId & " AS FNHSysBuyId"
                tSql &= Environment.NewLine & "     , NULL AS FTCancelAppBy, NULL AS FDCancelAppDate, NULL AS FDCancelAppTime, NULL AS FTCancelAppRemark"
                tSql &= Environment.NewLine & "     , '' AS FTPOTrading, NULL AS FTPOItem, '' AS FTPOCreateDate"
                tSql &= Environment.NewLine & "     ," & _FNHSysMerTeamId & " AS FNHSysMerTeamId"
                tSql &= Environment.NewLine & "     , ISNULL((SELECT L1.FNHSysPlantId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMPlant] AS L1 WITH(NOLOCK) WHERE L1.FTPlantCode = N'-'),NULL) AS FNHSysPlantId"
                tSql &= Environment.NewLine & "   , A.FNHSysBuyGrpId"
                tSql &= Environment.NewLine & "     , A.FNHSysMainCategoryId, " & nFNHSysVenderPramId & " AS FNHSysVenderPramId"
                tSql &= Environment.NewLine & "     , N'Y' AS FTOrderCreateStatus, N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' AS FTImportUser , CONVERT(VARCHAR(10),GETDATE(),111) AS FDImportDate, CONVERT(VARCHAR(8),GETDATE(),114) AS FTImportTime,0 AS FNRowImport,A.FTStyle," & Integer.Parse(Val(FNHSysSeasonId.Properties.Tag.ToString)) & " "
                tSql &= Environment.NewLine & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTTempImportOrderTargetPMC] AS A LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMerTeam] AS B WITH(NOLOCK) ON A.FNHSysMerTeamId = B.FNHSysMerTeamId"
                tSql &= Environment.NewLine & " WHERE A.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                tSql &= Environment.NewLine & "      AND A.FNGrpNo IN (SELECT MAX(L1.FNGrpNo)"
                tSql &= Environment.NewLine & "                           FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTTempImportOrderTargetPMC] AS L1 WITH(NOLOCK)"
                tSql &= Environment.NewLine & "                           WHERE L1.FTUserLogin = A.FTUserLogin"
                tSql &= Environment.NewLine & "                                 AND L1.FTPONo = A.FTPONO"
                tSql &= Environment.NewLine & "                                 AND L1.FTStyle = A.FTStyle"
                tSql &= Environment.NewLine & "                           GROUP BY L1.FTPONo)"
                tSql &= Environment.NewLine & " ORDER BY B.FTMerTeamCode ASC,A.FTStyle ASC,A.FTPONO ASC;"
                tSql &= Environment.NewLine & " UPDATE A"
                tSql &= Environment.NewLine & " SET FTGenerateOrderNo = ISNULL((SELECT TOP 1 ISNULL(FTOrderNo,'')"
                tSql &= Environment.NewLine & "                                FROM #Tab"
                tSql &= Environment.NewLine & "                                WHERE FTPORef = A.FTPONO AND FTStyle = A.FTStyle), '')"
                tSql &= Environment.NewLine & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTTempImportOrderTargetPMC] AS A LEFT OUTER JOIN [HITECH_MASTER]..[TMERMMerTeam] AS B WITH(NOLOCK) ON A.FNHSysMerTeamId = B.FNHSysMerTeamId"
                tSql &= Environment.NewLine & " WHERE A.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                tSql &= Environment.NewLine & "      AND A.FTPONo IN ( "
                tSql &= Environment.NewLine & "                              SELECT MAX(L1.FTPONo) "
                tSql &= Environment.NewLine & "                              FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTTempImportOrderTargetPMC] AS L1 WITH(NOLOCK) "
                tSql &= Environment.NewLine & "                              WHERE L1.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                tSql &= Environment.NewLine & "                              GROUP BY L1.FTPONo,L1.FTStyle "
                tSql &= Environment.NewLine & " 	                       )"
                tSql &= Environment.NewLine & " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTOrder]"
                tSql &= Environment.NewLine & "                              ([FTInsUser],[FDInsDate],[FTInsTime]"
                tSql &= Environment.NewLine & "                              ,[FTUpdUser],[FDUpdDate],[FTUpdTime]"
                tSql &= Environment.NewLine & "                              ,[FTOrderNo],[FDOrderDate],[FTOrderBy],[FNOrderType]"
                tSql &= Environment.NewLine & "                              ,[FNHSysCmpId],[FNHSysCmpRunId],[FNHSysStyleId],[FTPORef]"
                tSql &= Environment.NewLine & "                              ,[FNHSysCustId],[FNHSysAgencyId],[FNHSysProdTypeId],[FNHSysBuyerId]"
                tSql &= Environment.NewLine & "                              ,[FTMainMaterial],[FTCombination],[FTRemark]"
                tSql &= Environment.NewLine & "                              ,[FTStateOrderApp],[FTAppBy],[FDAppDate],[FTAppTime]"
                tSql &= Environment.NewLine & "                              ,[FNJobState],[FTStateBy],[FDStateDate],[FTStateTime]"
                tSql &= Environment.NewLine & "                              ,[FTImage1],[FTImage2],[FTImage3],[FTImage4]"
                tSql &= Environment.NewLine & "                              ,[FNHSysBrandId],[FNHSysBuyId],[FTCancelAppBy],[FDCancelAppDate]"
                tSql &= Environment.NewLine & "                              ,[FDCancelAppTime],[FTCancelAppRemark],[FTPOTradingCo],[FTPOItem]"
                tSql &= Environment.NewLine & "                              ,[FTPOCreateDate],[FNHSysMerTeamId],[FNHSysPlantId],[FNHSysBuyGrpId]"
                tSql &= Environment.NewLine & "                              ,[FNHSysMainCategoryId],[FNHSysVenderPramId],[FTOrderCreateStatus],[FTImportUser],[FDImportDate],[FTImportTime],[FNHSysSeasonId],[FNHSysCmpIdCreate])"
                tSql &= Environment.NewLine & " SELECT [FTInsUser],[FDInsDate],[FTInsTime]"
                tSql &= Environment.NewLine & "       ,[FTUpdUser],[FDUpdDate],[FTUpdTime]"
                tSql &= Environment.NewLine & "       ,[FTOrderNo],[FDOrderDate],[FTOrderBy],[FNOrderType]"
                tSql &= Environment.NewLine & "       ,[FNHSysCmpId],[FNHSysCmpRunId],[FNHSysStyleId],[FTPORef]"
                tSql &= Environment.NewLine & "       ,[FNHSysCustId],[FNHSysAgencyId],[FNHSysProdTypeId],[FNHSysBuyerId]"
                tSql &= Environment.NewLine & "       ,[FTMainMaterial],[FTCombination],[FTRemark]"
                tSql &= Environment.NewLine & "       ,[FTStateOrderApp],[FTAppBy],[FDAppDate],[FTAppTime]"
                tSql &= Environment.NewLine & "       ,[FNJobState],[FTStateBy],[FDStateDate],[FTStateTime]"
                tSql &= Environment.NewLine & "       ,[FTImage1],[FTImage2],[FTImage3],[FTImage4]"
                tSql &= Environment.NewLine & "       ,[FNHSysBrandId],[FNHSysBuyId],[FTCancelAppBy],[FDCancelAppDate]"
                tSql &= Environment.NewLine & "       ,[FDCancelAppTime],[FTCancelAppRemark],[FTPOTradingCo],[FTPOItem]"
                tSql &= Environment.NewLine & "       ,[FTPOCreateDate],[FNHSysMerTeamId],[FNHSysPlantId],[FNHSysBuyGrpId]"
                tSql &= Environment.NewLine & "       ,[FNHSysMainCategoryId],[FNHSysVenderPramId],[FTOrderCreateStatus],[FTImportUser],[FDImportDate],[FTImportTime],[FNHSysSeasonId]," & Val(HI.ST.SysInfo.CmpID) & ""
                tSql &= Environment.NewLine & " FROM #Tab"
                tSql &= Environment.NewLine & " DROP TABLE #Tab;"

                '...Merge Transaction TMERTOrderSub And TMERTOrderSub_BreakDown
                '========================================================================================================================================================================
                tSql &= Environment.NewLine & "      create table #TabSub ("
                tSql &= Environment.NewLine & "[FTInsUser] [nvarchar](50) NULL,"
                tSql &= Environment.NewLine & "[FDInsDate] [varchar](10) NULL,"
                tSql &= Environment.NewLine & "[FTInsTime] [varchar](8) NULL,"
                tSql &= Environment.NewLine & "[FTUpdUser] [nvarchar](50) NULL,"
                tSql &= Environment.NewLine & "[FDUpdDate] [varchar](10) NULL,"
                tSql &= Environment.NewLine & "[FTUpdTime] [varchar](8) NULL,"
                tSql &= Environment.NewLine & "[FTOrderNo] [nvarchar](30) NOT NULL,"
                tSql &= Environment.NewLine & "[FTSubOrderNo] [nvarchar](30) NOT NULL,"
                tSql &= Environment.NewLine & "[FDSubOrderDate] [nvarchar](10) NULL,"
                tSql &= Environment.NewLine & "[FTSubOrderBy] [nvarchar](50) NULL,"
                tSql &= Environment.NewLine & "[FDProDate] [nvarchar](10) NULL,"
                tSql &= Environment.NewLine & "[FDShipDate] [nvarchar](10) NULL,"
                tSql &= Environment.NewLine & "[FNHSysBuyId] [int] NULL,"
                tSql &= Environment.NewLine & "[FNHSysContinentId] [int] NULL,"
                tSql &= Environment.NewLine & "[FNHSysCountryId] [int] NULL,"
                tSql &= Environment.NewLine & "[FNHSysProvinceId] [int] NULL,"
                tSql &= Environment.NewLine & "[FNHSysShipModeId] [int] NULL,"
                tSql &= Environment.NewLine & "[FNHSysCurId] [int] NULL,"
                tSql &= Environment.NewLine & "[FNHSysGenderId] [int] NULL,"
                tSql &= Environment.NewLine & "[FNHSysUnitId] [int] NULL,"
                tSql &= Environment.NewLine & "[FNSubOrderState] [int] NULL,"
                tSql &= Environment.NewLine & "[FTStateEmb] [nvarchar](1) NULL,"
                tSql &= Environment.NewLine & "[FTStatePrint] [nvarchar](1) NULL,"
                tSql &= Environment.NewLine & "[FTStateHeat] [nvarchar](1) NULL,"
                tSql &= Environment.NewLine & "[FTStateLaser] [nvarchar](1) NULL,"
                tSql &= Environment.NewLine & "[FTStateWindows] [nvarchar](1) NULL,"
                tSql &= Environment.NewLine & "[FTStateOther1] [nvarchar](1) NULL,"
                tSql &= Environment.NewLine & "[FTOther1Note] [nvarchar](50) NULL,"
                tSql &= Environment.NewLine & "[FTStateOther2] [nvarchar](1) NULL,"
                tSql &= Environment.NewLine & "[FTOther2Note] [nvarchar](50) NULL,"
                tSql &= Environment.NewLine & "[FTStateOther3] [nvarchar](1) NULL,"
                tSql &= Environment.NewLine & "[FTOther3Note1] [nvarchar](50) NULL,"
                tSql &= Environment.NewLine & "[FTRemark] [nvarchar](1000) NULL,"
                tSql &= Environment.NewLine & "[FNHSysShipPortId] [int] NULL,"
                tSql &= Environment.NewLine & "[FDShipDateOrginal] [nvarchar](10) NULL,"
                tSql &= Environment.NewLine & "[FTCustRef] [nvarchar](200) NULL,"
                tSql &= Environment.NewLine & "[FNGrpNo] int NULL)"
                tSql &= Environment.NewLine & " INSERT INTO #TabSub"
                tSql &= Environment.NewLine & " ([FTInsUser],[FDInsDate],[FTInsTime],[FTUpdUser]"
                tSql &= Environment.NewLine & " ,[FDUpdDate],[FTUpdTime],[FTOrderNo],[FTSubOrderNo]"
                tSql &= Environment.NewLine & " ,[FDSubOrderDate],[FTSubOrderBy],[FDProDate],[FDShipDate]"
                tSql &= Environment.NewLine & " ,[FNHSysBuyId],[FNHSysContinentId],[FNHSysCountryId],[FNHSysProvinceId]"
                tSql &= Environment.NewLine & " ,[FNHSysShipModeId],[FNHSysCurId],[FNHSysGenderId],[FNHSysUnitId]"
                tSql &= Environment.NewLine & " ,[FNSubOrderState],[FTStateEmb],[FTStatePrint],[FTStateHeat]"
                tSql &= Environment.NewLine & " ,[FTStateLaser],[FTStateWindows],[FTStateOther1],[FTOther1Note]"
                tSql &= Environment.NewLine & " ,[FTStateOther2],[FTOther2Note],[FTStateOther3],[FTOther3Note1]"
                tSql &= Environment.NewLine & " ,[FTRemark]"
                tSql &= Environment.NewLine & " ,[FNHSysShipPortId]"
                tSql &= Environment.NewLine & " ,[FDShipDateOrginal],[FTCustRef],[FNGrpNo])"
                tSql &= Environment.NewLine & " SELECT DISTINCT  NULL AS FTInsUser , CONVERT(VARCHAR(10),GETDATE(),111) AS FDInsDate, CONVERT(VARCHAR(8),GETDATE(),114) AS FTInsTime"
                tSql &= Environment.NewLine & "     , NULL AS FTUpdUser , NULL AS FDUpdDate, NULL AS FTUpdTime"
                tSql &= Environment.NewLine & "     , A.FTOrderNo"
                tSql &= Environment.NewLine & "     , (A.FTOrderNo + '-' + CHAR(64 + ROW_NUMBER() OVER (PARTITION BY A.FTOrderNo ORDER By A.FTOrderNo,A.FDShipDate,A.FNGrpNo))) AS FTSubOrderNo"     '...แยก sub order no. ตาม destination (country)
                tSql &= Environment.NewLine & "     , A.FDOrderDate AS FDSubOrderDate"
                tSql &= Environment.NewLine & "     , N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' AS FTSubOrderBy"
                tSql &= Environment.NewLine & "     , ISNULL((SELECT  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.[FN_GET_PRODDATE_IMPORT] (A.FDShipDate, " & nFNHSysCustId & ", NULL)), NULL) AS FDProdDate"
                tSql &= Environment.NewLine & "     , A.FDShipDate AS FDShipDate"
                tSql &= Environment.NewLine & "     , NULL AS FNHSysBuyId"
                tSql &= Environment.NewLine & "     , -1 FNHSysContinentId"
                tSql &= Environment.NewLine & "     , -1 AS FNHSysCountryId"
                tSql &= Environment.NewLine & "     , -1 AS FNHSysProvinceId"
                tSql &= Environment.NewLine & "     , -1 AS FNHSysShipModeId"
                tSql &= Environment.NewLine & "     , A.FNHSysCurId AS FNHSysCurId"
                tSql &= Environment.NewLine & "     , -1 FNHSysGenderId"
                tSql &= Environment.NewLine & "     ," & Integer.Parse(Val(FNHSysUnitId.Properties.Tag.ToString())) & "  AS FNHSysUnitId"
                tSql &= Environment.NewLine & "     , NULL AS FNSubOrderState"
                tSql &= Environment.NewLine & "     , C.FTStateEmb AS FTStateEmb"
                tSql &= Environment.NewLine & "     , C.FTStatePrint AS FTStatePrint"
                tSql &= Environment.NewLine & "     , C.FTStateHeat AS FTStateHeat"
                tSql &= Environment.NewLine & "     , C.FTStateLaser AS FTStateLaser"
                tSql &= Environment.NewLine & "     , C.FTStateWindows AS FTStateWindows"
                tSql &= Environment.NewLine & "     , NULL AS FTOther1Note, NULL AS FTOther1Note, NULL AS FTStateOther2, NULL AS FTOther2Note, NULL AS FTStateOther3, NULL AS FTOther3Note1, NULL AS FTRemark"
                tSql &= Environment.NewLine & "     , ISNULL((SELECT TOP 1 L1.FNHSysShipPortId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCNMShipPort] AS L1 WITH(NOLOCK) WHERE L1.FTShipPortCode = N'-'),NULL) AS FNHSysShipPortId"
                tSql &= Environment.NewLine & "     , A.FDShipDateOrginal AS FDShipDateOrginal"
                tSql &= Environment.NewLine & "     , dbo.FN_GetCustomer_Refer(A.FNHSysPlantId, A.FNHSysBuyerId) AS FTCustRef,A.FNGrpNo"
                tSql &= Environment.NewLine & "FROM(SELECT A.FTPONo, C.FDOrderDate, '' AS FTPOTrading, '' AS FTPOItem, 0 AS FNRowImport, A.FTStyle AS FTStyle, C.FTOrderNo"
                tSql &= Environment.NewLine & "     ,A.FDShipDate AS FDShipDate"
                tSql &= Environment.NewLine & "      ,A.FDShipDate AS FDShipDateOrginal,A.FNGrpNo "
                tSql &= Environment.NewLine & "     , ISNULL((SELECT TOP 1 L1.FNHSysPlantId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMPlant] AS L1 WITH(NOLOCK) WHERE L1.FTPlantCode = N'-'),NULL) AS FNHSysPlantId"
                tSql &= Environment.NewLine & "     , ISNULL((SELECT TOP 1 L1.FNHSysBuyerId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMBuyer] AS L1 WITH(NOLOCK) WHERE L1.FTBuyerCode = N'-'),NULL) AS FNHSysBuyerId"
                tSql &= Environment.NewLine & "          , ISNULL((SELECT TOP 1 L1.FNHSysCurId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCNMCustomer] AS L1 WITH(NOLOCK) WHERE L1.FNHSysCustId = C.FNHSysCustId), NULL) AS FNHSysCurId"
                tSql &= Environment.NewLine & "     FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTTempImportOrderTargetPMC] AS A LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMerTeam] AS B WITH(NOLOCK) ON A.FNHSysMerTeamId = B.FNHSysMerTeamId"
                tSql &= Environment.NewLine & "          INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrder] AS C WITH(NOLOCK) ON A.FTGenerateOrderNo = C.FTOrderNo"
                tSql &= Environment.NewLine & "     WHERE A.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                tSql &= Environment.NewLine & "           AND NOT EXISTS (SELECT 'T'"
                tSql &= Environment.NewLine & "                           FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub] AS L2 WITH(NOLOCK)"
                tSql &= Environment.NewLine & "                           WHERE C.FTOrderNo = L2.FTOrderNo)"
                tSql &= Environment.NewLine & "           AND A.FTPONo IN (SELECT MAX(L1.FTPONo)"
                tSql &= Environment.NewLine & "                                FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTTempImportOrderTargetPMC] AS L1 WITH(NOLOCK)"
                tSql &= Environment.NewLine & "                                WHERE L1.FTUserLogin =N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                tSql &= Environment.NewLine & "                                      AND L1.FTPONo = A.FTPONO"
                tSql &= Environment.NewLine & "                                GROUP BY L1.FTPONo,FTStyle)) AS A"
                tSql &= Environment.NewLine & "       LEFT JOIN (SELECT L4.FNHSysStyleId, L4.FTStyleCode, ISNULL(MAX(L3.FTStateEmb),0) AS FTStateEmb, ISNULL(MAX(L3.FTStatePrint),0) AS FTStatePrint, ISNULL(MAX(L3.FTStateHeat),0) AS FTStateHeat, ISNULL(MAX(L3.FTStateLaser),0) AS FTStateLaser, ISNULL(MAX(L3.FTStateWindows),0) AS FTStateWindows"
                tSql &= Environment.NewLine & "                  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTStyle_Part] AS L3 WITH(NOLOCK) INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMStyle] AS L4 WITH(NOLOCK) ON L3.FNHSysStyleId = L4.FNHSysStyleId"
                tSql &= Environment.NewLine & "                  GROUP BY L4.FNHSysStyleId, L4.FTStyleCode ) AS C ON A.FTStyle = C.FTStyleCode"
                tSql &= Environment.NewLine & "ORDER BY A.FTOrderNo ASC;"



                tSql &= Environment.NewLine & "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTOrderSub]"
                tSql &= Environment.NewLine & "         ([FTInsUser],[FDInsDate],[FTInsTime],[FTUpdUser]"
                tSql &= Environment.NewLine & "         ,[FDUpdDate],[FTUpdTime],[FTOrderNo],[FTSubOrderNo]"
                tSql &= Environment.NewLine & "         ,[FDSubOrderDate],[FTSubOrderBy],[FDProDate],[FDShipDate]"
                tSql &= Environment.NewLine & "         ,[FNHSysBuyId],[FNHSysContinentId],[FNHSysCountryId],[FNHSysProvinceId]"
                tSql &= Environment.NewLine & "         ,[FNHSysShipModeId],[FNHSysCurId],[FNHSysGenderId],[FNHSysUnitId]"
                tSql &= Environment.NewLine & "         ,[FNSubOrderState],[FTStateEmb],[FTStatePrint],[FTStateHeat]"
                tSql &= Environment.NewLine & "         ,[FTStateLaser],[FTStateWindows],[FTStateOther1],[FTOther1Note]"
                tSql &= Environment.NewLine & "         ,[FTStateOther2],[FTOther2Note],[FTStateOther3],[FTOther3Note1]"
                tSql &= Environment.NewLine & "         ,[FTRemark]"
                tSql &= Environment.NewLine & "         ,[FNHSysShipPortId]"
                tSql &= Environment.NewLine & "         ,[FDShipDateOrginal],[FTCustRef])"
                tSql &= Environment.NewLine & "   SELECT [FTInsUser],[FDInsDate],[FTInsTime],[FTUpdUser]"
                tSql &= Environment.NewLine & "    ,[FDUpdDate],[FTUpdTime],[FTOrderNo],[FTSubOrderNo]"
                tSql &= Environment.NewLine & "    ,[FDSubOrderDate],[FTSubOrderBy],[FDProDate],[FDShipDate]"
                tSql &= Environment.NewLine & "    ,[FNHSysBuyId],[FNHSysContinentId],[FNHSysCountryId],[FNHSysProvinceId]"
                tSql &= Environment.NewLine & "    ,[FNHSysShipModeId],[FNHSysCurId],[FNHSysGenderId],[FNHSysUnitId]"
                tSql &= Environment.NewLine & "    ,[FNSubOrderState],[FTStateEmb],[FTStatePrint],[FTStateHeat]"
                tSql &= Environment.NewLine & "   ,[FTStateLaser],[FTStateWindows],[FTStateOther1],[FTOther1Note]"
                tSql &= Environment.NewLine & "   ,[FTStateOther2],[FTOther2Note],[FTStateOther3],[FTOther3Note1]"
                tSql &= Environment.NewLine & "    ,[FTRemark]"
                tSql &= Environment.NewLine & "    ,[FNHSysShipPortId]"
                tSql &= Environment.NewLine & "    ,[FDShipDateOrginal],[FTCustRef] "
                tSql &= Environment.NewLine & "   FROM #TabSub"
                tSql &= Environment.NewLine & "    Update A"
                tSql &= Environment.NewLine & "  SET   FTGenerateSubOrderNo = B.FTSubOrderNo"
                tSql &= Environment.NewLine & "   FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTTempImportOrderTargetPMC] AS A INNER JOIN #TabSub AS B  ON A.FTGenerateOrderNo = B.FTOrderNo AND A.FNGrpNo=B.FNGrpNo"
                tSql &= Environment.NewLine & "    WHERE A.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                tSql &= Environment.NewLine & "   DROP TABLE #TabSub"
                tSql &= Environment.NewLine & "DECLARE @TMERTOrderSub_BreakDown_Import AS TABLE ([FTInsUser] [nvarchar](50), [FDInsDate] [varchar](10) NULL, [FTInsTime] [varchar](8) NULL,"
                tSql &= Environment.NewLine & "                              [FTUpdUser] [nvarchar](50) NULL, [FDUpdDate] [varchar](10) NULL, [FTUpdTime] [varchar](8) NULL,"
                tSql &= Environment.NewLine & "                              [FTOrderNo] [nvarchar](30) NOT NULL, [FTSubOrderNo] [nvarchar](30) NOT NULL,"
                tSql &= Environment.NewLine & "                              [FTColorway] [nvarchar](30) NOT NULL, [FTSizeBreakDown] [nvarchar](30) NOT NULL,"
                tSql &= Environment.NewLine & "                              [FNPrice] [numeric](18, 5) NULL, [FNQuantity] [int] NULL, [FNAmt] [numeric](18, 5) NULL,"
                tSql &= Environment.NewLine & "                              [FNHSysMatColorId] [int] NULL, [FNHSysMatSizeId] [int] NULL,"
                tSql &= Environment.NewLine & "                              [FNExtraQty] [numeric](18, 5) NULL, [FNQuantityExtra] [numeric](18, 5) NULL,"
                tSql &= Environment.NewLine & "                              [FNGrandQuantity] [numeric](18, 5) NULL, [FNAmntExtra] [numeric](18, 5) NULL,"
                tSql &= Environment.NewLine & "                              [FNGrandAmnt] [numeric](18, 5) NULL, [FNGarmentQtyTest] [int] NULL,[FNAmntQtyTest] [numeric](18, 5) NULL)"
                tSql &= Environment.NewLine & "INSERT INTO @TMERTOrderSub_BreakDown_Import ([FTInsUser], [FDInsDate], [FTInsTime],"
                tSql &= Environment.NewLine & "				    [FTUpdUser], [FDUpdDate], [FTUpdTime],"
                tSql &= Environment.NewLine & "				    [FTOrderNo], [FTSubOrderNo], [FTColorway],"
                tSql &= Environment.NewLine & "				    [FTSizeBreakDown], [FNPrice],"
                tSql &= Environment.NewLine & "				    [FNQuantity], [FNAmt], [FNHSysMatColorId], [FNHSysMatSizeId],"
                tSql &= Environment.NewLine & "				    [FNExtraQty], [FNQuantityExtra], [FNGrandQuantity],"
                tSql &= Environment.NewLine & "				    [FNAmntExtra], [FNGrandAmnt])"
                tSql &= Environment.NewLine & "SELECT ordImport.FTInsUser, ordImport.FDInsDate, ordImport.FTInsTime,"
                tSql &= Environment.NewLine & "       ordImport.FTUpdUser, ordImport.FDUpdDate, ordImport.FTUpdTime,"
                tSql &= Environment.NewLine & "       ordImport.FTOrderNo, ordImport.FTSubOrderNo, ordImport.FTColorway,"
                tSql &= Environment.NewLine & "       ordImport.FTSizeBreakDown, ordImport.FNPrice, ordImport.FNQuantity,"
                tSql &= Environment.NewLine & "       ordImport.FNAmt, ordImport.FNHSysMatColorId, ordImport.FNHSysMatSizeId,"

                'Select Case FNQtySpecialType
                '    Case 1
                '        tSql &= Environment.NewLine & "            0 AS FNExtraQty"
                '        tSql &= Environment.NewLine & "           ,  " & Integer.Parse(Val(FNQtySpecialTypeQty)) & " AS FNQuantityExtra"
                '    Case 2
                '        tSql &= Environment.NewLine & "            " & FNQtySpecialTypeQty & " AS FNExtraQty"
                '        tSql &= Environment.NewLine & "           , CEILING((ordImport.FNQuantity * " & FNQtySpecialTypeQty & ")/100.00) AS FNQuantityExtra"
                '    Case Else
                tSql &= Environment.NewLine & "       0 AS FNExtraQty,0 AS FNQuantityExtra"
                '  End Select

                tSql &= Environment.NewLine & " , ordImport.FNGrandQuantity,"
                tSql &= Environment.NewLine & "       ordImport.FNAmntExtra, ordImport.FNGrandAmnt"
                tSql &= Environment.NewLine & "FROM (SELECT NULL AS FTInsUser, CONVERT(VARCHAR(10),GETDATE(),111) AS FDInsDate, CONVERT(VARCHAR(8),GETDATE(),114) AS FTInsTime"
                tSql &= Environment.NewLine & "           , NULL AS FTUpdUser, NULL AS FDUpdDate, NULL AS FTUpdTime"
                tSql &= Environment.NewLine & "           , AA.FTGenerateOrderNo AS FTOrderNo"
                tSql &= Environment.NewLine & "           , (MM3.FTSubOrderNo) AS FTSubOrderNo"
                tSql &= Environment.NewLine & "           , A.FTMapColor AS FTColorway"
                tSql &= Environment.NewLine & "           , A.FTMapSize AS FTSizeBreakDown"
                tSql &= Environment.NewLine & "           , ISNULL(MAX(AA.FNFOBPrice),0) AS FNPrice"
                tSql &= Environment.NewLine & "           , ISNULL(SUM(A.FNQuantity), 0) AS FNQuantity"
                tSql &= Environment.NewLine & "           , ISNULL((SUM(A.FNQuantity) * MAX(AA.FNFOBPrice)), 0) AS FNAmt"
                tSql &= Environment.NewLine & "           , C.FNHSysMatColorId AS FNHSysMatColorId"
                tSql &= Environment.NewLine & "           , D.FNHSysMatSizeId AS FNHSysMatSizeId"
                tSql &= Environment.NewLine & "           , 0 AS FNExtraQty"
                tSql &= Environment.NewLine & "           , 0 AS FNQuantityExtra"
                tSql &= Environment.NewLine & "           , ISNULL(SUM(0), 0) AS FNGrandQuantity"
                tSql &= Environment.NewLine & "           , 0 AS FNAmntExtra"
                tSql &= Environment.NewLine & "           , ISNULL((SUM(A.FNQuantity) * MAX(AA.FNFOBPrice)), NULL) AS FNGrandAmnt"
                tSql &= Environment.NewLine & "      FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTTempImportOrderTargetPMC] AS AA WITH(NOLOCK)"
                tSql &= Environment.NewLine & "           INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub] AS MM3 WITH(NOLOCK) ON AA.FTGenerateOrderNo = MM3.FTOrderNo AND AA.FTGenerateSubOrderNo = MM3.FTSubOrderNo "
                tSql &= Environment.NewLine & "           INNER JOIN (SELECT A.*"
                tSql &= Environment.NewLine & "      ,    -1 AS FNHSysCountryId"
                tSql &= Environment.NewLine & "           FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTTempImportOrderTargetPMC_D] AS A WITH(NOLOCK)"
                tSql &= Environment.NewLine & "           WHERE FTUserLogin='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                tSql &= Environment.NewLine & "    ) AS A  ON  AA.FTUserLogin = A.FTUserLogin AND AA.FTPONo = A.FTPONo  AND AA.FTStyle = A.FTStyle AND AA.FNGrpNo = A.FNGrpNo  AND MM3.FNHSysCountryId = A.FNHSysCountryId"
                tSql &= Environment.NewLine & "           INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrder] AS B WITH(NOLOCK) ON AA.FTGenerateOrderNo = B.FTOrderNo"
                tSql &= Environment.NewLine & "           INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMatColor] AS C (NOLOCK) ON A.FTMapColor = C.FTMatColorCode"
                tSql &= Environment.NewLine & "           INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMatSize] AS D (NOLOCK) ON A.FTMapSize = D.FTMatSizeCode"
                tSql &= Environment.NewLine & "      WHERE A.FNQuantity > 0"
                tSql &= Environment.NewLine & "      GROUP BY AA.FTGenerateOrderNo, A.FTPONo,  A.FTStyle, MM3.FTSubOrderNo, A.FTMapColor, A.FTMapSize, C.FNHSysMatColorId, D.FNHSysMatSizeId"
                tSql &= Environment.NewLine & "      ) AS ordImport"
                tSql &= Environment.NewLine & "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTOrderSub_BreakDown] ([FTInsUser],[FDInsDate],[FTInsTime]"
                tSql &= Environment.NewLine & "                 ,[FTUpdUser],[FDUpdDate],[FTUpdTime]"
                tSql &= Environment.NewLine & "                 ,[FTOrderNo],[FTSubOrderNo]"
                tSql &= Environment.NewLine & "                 ,[FTColorway],[FTSizeBreakDown]"
                tSql &= Environment.NewLine & "                 ,[FNPrice],[FNQuantity],[FNAmt]"
                tSql &= Environment.NewLine & "                 ,[FNHSysMatColorId],[FNHSysMatSizeId]"
                tSql &= Environment.NewLine & "                 ,[FNExtraQty],[FNQuantityExtra]"
                tSql &= Environment.NewLine & "                 ,[FNGrandQuantity]"
                tSql &= Environment.NewLine & "                 ,[FNAmntExtra]"
                tSql &= Environment.NewLine & "                 ,[FNGrandAmnt]"
                tSql &= Environment.NewLine & "                 ,[FNGarmentQtyTest]"
                tSql &= Environment.NewLine & "                 ,[FNAmntQtyTest],[FNPriceOrg])"
                tSql &= Environment.NewLine & "SELECT aa.[FTInsUser], aa.[FDInsDate], aa.[FTInsTime],"
                tSql &= Environment.NewLine & "       aa.[FTUpdUser], aa.[FDUpdDate], aa.[FTUpdTime],"
                tSql &= Environment.NewLine & "       aa.[FTOrderNo], aa.[FTSubOrderNo], aa.[FTColorway],"
                tSql &= Environment.NewLine & "       aa.[FTSizeBreakDown], aa.[FNPrice],"
                tSql &= Environment.NewLine & "       aa.[FNQuantity], aa.[FNAmt], aa.[FNHSysMatColorId], aa.[FNHSysMatSizeId],"
                tSql &= Environment.NewLine & "       aa.[FNExtraQty], aa.[FNQuantityExtra]"
                tSql &= Environment.NewLine & "      ,(aa.[FNQuantity] + aa.[FNQuantityExtra]) AS  [FNGrandQuantity]"
                'tSql &= Environment.NewLine & "      , aa.[FNGrandQuantity]"
                tSql &= Environment.NewLine & "      , aa.[FNAmntExtra], aa.[FNGrandAmnt],0 AS FNGarmentQtyTest ,0 AS FNAmntQtyTest, aa.[FNPrice]"
                tSql &= Environment.NewLine & "FROM @TMERTOrderSub_BreakDown_Import AS aa"
                tSql &= Environment.NewLine & "WHERE NOT EXISTS (SELECT 'T'"
                tSql &= Environment.NewLine & "                  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub_BreakDown AS bb (NOLOCK)"
                tSql &= Environment.NewLine & "                  WHERE bb.FTOrderNo = aa.FTOrderNo"
                tSql &= Environment.NewLine & "                        AND bb.FTSubOrderNo = aa.FTSubOrderNo"
                tSql &= Environment.NewLine & "                        AND bb.FTColorway = aa.FTColorway"
                tSql &= Environment.NewLine & "                        AND bb.FTSizeBreakDown = aa.FTSizeBreakDown"
                tSql &= Environment.NewLine & "                  );"
                '========================================================================================================================================================================

                If HI.Conn.SQLConn.Execute_Tran(tSql, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    'MsgBox("Step Append New Order No !!!")
                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    _bImportComplete = False
                Else
                    HI.Conn.SQLConn.Tran.Commit()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    _bImportComplete = True
                End If

            Else
                'MsgBox("พบปัญหาในการบันทึกรายการนำเข้าข้อมูลใบสั่งผลิต !!!", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, My.Application.Info.Title)
            End If



        Catch ex As Exception



        End Try

        Return _bImportComplete

    End Function

    Private Function W_PRCbImportFactoryOrderPMCPDF(_Spls) As Boolean
        '...last modify 2014/12/19 when drop field Amount, Qty TMERTOrder_BreakDown, TMERTOrder, TMERTOrderSub

        Dim _bImportComplete As Boolean = False

        Dim tMsgSplash As String = ""

        Try
            Dim _FNHSysMerTeamId As Integer = -1
            Dim nFNHSysCustId As Integer
            Dim nFNHSysCmpRunId As Integer
            Dim nFNHSysBuyId As Integer
            Dim nFNHSysVenderPramId As Integer
            Dim FNQtySpecialType As Integer = 0
            Dim FNQtySpecialTypeQty As Double = 0
            Dim _dtper As DataTable
            Dim _Qry As String = ""


            _FNHSysMerTeamId = Me.SysMerTeamId

            tSql = "SELECT TOP 1 FNQtySpecialType, FNQtySpecial "
            tSql &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMBuyGrp AS M WITH(NOLOCK) "
            tSql &= vbCrLf & " WHERE FNHSysBuyGrpId=" & Integer.Parse(Val(FNHSysBuyGrpId.Properties.Tag.ToString)) & ""
            _dtper = HI.Conn.SQLConn.GetDataTable(tSql, Conn.DB.DataBaseName.DB_MERCHAN)

            For Each R As DataRow In _dtper.Rows

                FNQtySpecialType = Integer.Parse(Val(R!FNQtySpecialType.ToString()))
                FNQtySpecialTypeQty = Double.Parse(Val(R!FNQtySpecial.ToString()))

                Exit For
            Next

            nFNHSysCustId = Val(Me.FNHSysCustId.Properties.Tag) : nFNHSysCmpRunId = Val(Me.FNHSysCmpRunId.Properties.Tag) : nFNHSysBuyId = Val(Me.FNHSysBuyId.Properties.Tag)

            tSql = ""
            tSql = "SELECT TOP 1 A.FNHSysVenderPramId FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMVenderPram] AS A WITH(NOLOCK) WHERE A.FNHSysVenderPramId = " & Val(Me.FNHSysVenderPramId.Properties.Tag)

            nFNHSysVenderPramId = Val(HI.Conn.SQLConn.GetField(tSql, HI.Conn.DB.DataBaseName.DB_MASTER, "0"))

            If nFNHSysVenderPramId <= 0 Then Return False

            Dim oDBdtImport As System.Data.DataTable

            tSql = "  SELECT  " & _FNHSysMerTeamId & " AS FNHSysMerTeamId, A.FTPONo, '' AS FTPOTrading, '' AS FTPOCreateDate, A.FTOrderDate AS FTOrderDate"
            tSql &= Environment.NewLine & "         , " & nFNHSysBuyId & " AS FNHSysBuyId, " & nFNHSysCmpRunId & " AS FNHSysCmpRunId, " & nFNHSysCustId & " AS FNHSysCustId"
            tSql &= Environment.NewLine & "    ,N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'  AS FTOrderBy, 17 AS FNOrderType, 0 AS FNJobState"
            tSql &= Environment.NewLine & "    ,A.FTStyle AS FTStyle, '' AS FDShipDate, '' AS FDShipDateOrginal, A.FNHSysBuyGrpId,A.FNHSysGenderId, A.FNHSysProdTypeId"
            tSql &= Environment.NewLine & "    ,A.FNHSysPlantId,'' AS FTYear, A.FNHSysMainCategoryId,A.FNHSysSeasonId AS FTPlanningSeason,A.FNHSysCountryId,A.FNHSysBuyerId"
            tSql &= Environment.NewLine & "   FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTTempImportOrderTargetPMC] AS A "
            tSql &= Environment.NewLine & "       WHERE A.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            tSql &= Environment.NewLine & "    AND A.FTPONo IN (SELECT MAX(L1.FTPONo)"
            tSql &= Environment.NewLine & "     FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTTempImportOrderTargetPMC AS L1 WITH(NOLOCK)"
            tSql &= Environment.NewLine & "     WHERE L1.FTUserLogin = A.FTUserLogin"
            tSql &= Environment.NewLine & "        AND L1.FTPONo = A.FTPONo"
            tSql &= Environment.NewLine & "    GROUP BY L1.FTPONo)"
            tSql &= Environment.NewLine & "   ORDER BY  A.FTStyle ASC;"

            oDBdtImport = HI.Conn.SQLConn.GetDataTable(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN)

            If Not oDBdtImport Is Nothing AndAlso oDBdtImport.Rows.Count > 0 Then
                '...generate facotry order no.
                HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_MERCHAN)
                HI.Conn.SQLConn.SqlConnectionOpen()
                HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
                HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

                tSql = ""
                tSql = "DECLARE @DBName NVARCHAR(30);"
                tSql &= Environment.NewLine & "DECLARE @TblName NVARCHAR(30);"
                tSql &= Environment.NewLine & "DECLARE @DocType NVARCHAR(30);"
                tSql &= Environment.NewLine & "DECLARE @GetFotmat NVARCHAR(30);"
                tSql &= Environment.NewLine & "DECLARE @AddPrefix NVARCHAR(30);"
                tSql &= Environment.NewLine & "SET @DBName = N'" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "';"
                tSql &= Environment.NewLine & "SET @TblName = N'TMERTOrder';"
                tSql &= Environment.NewLine & "SET @DocType = '17';"
                tSql &= Environment.NewLine & "SET @GetFotmat = '';"
                tSql &= Environment.NewLine & "SET @AddPrefix = N'" & HI.TL.CboList.GetListRefer("FNOrderType", 17) & HI.UL.ULF.rpQuoted(Me.FNHSysCmpRunId.Text.Trim()) & "';"
                tSql &= Environment.NewLine & "DECLARE @tblSrcConfigDoc AS TABLE(FTRunNo NVARCHAR(30), FTRunStr NVARCHAR(30), FNRunning INT, FNRunningNoMax INT);"
                tSql &= Environment.NewLine & "INSERT INTO @tblSrcConfigDoc(FTRunNo, FTRunStr, FNRunning, FNRunningNoMax)"
                tSql &= Environment.NewLine & "EXEC SP_GET_FACTORY_ORDERNO_MAX @DBName, @TblName, @DocType, @GetFotmat,@AddPrefix;"
                tSql &= Environment.NewLine & "DECLARE @FTRunNo        AS NVARCHAR(30);"
                tSql &= Environment.NewLine & "DECLARE @FTRunStr       AS NVARCHAR(30);"
                tSql &= Environment.NewLine & "DECLARE @FNRunning      AS INT;"
                tSql &= Environment.NewLine & "DECLARE @FNRunningNoMax AS INT;"
                tSql &= Environment.NewLine & "SELECT @FTRunNo = A.FTRunNo, @FTRunStr = A.FTRunStr, @FNRunning = A.FNRunning, @FNRunningNoMax = A.FNRunningNoMax"
                tSql &= Environment.NewLine & "FROM @tblSrcConfigDoc AS A;"
                tSql &= Environment.NewLine & "--SET @FNRunningNoMax = @FNRunningNoMax + 1;"
                tSql &= Environment.NewLine & " create table #Tab("
                tSql &= Environment.NewLine & " [FTInsUser] [nvarchar](50) NULL,"
                tSql &= Environment.NewLine & " [FDInsDate] [varchar](10) NULL,"
                tSql &= Environment.NewLine & " [FTInsTime] [varchar](8) NULL,"
                tSql &= Environment.NewLine & " [FTUpdUser] [nvarchar](50) NULL,"
                tSql &= Environment.NewLine & " [FDUpdDate] [varchar](10) NULL,"
                tSql &= Environment.NewLine & " [FTUpdTime] [varchar](8) NULL,"
                tSql &= Environment.NewLine & " [FTOrderNo] [nvarchar](30) NOT NULL,"
                tSql &= Environment.NewLine & " [FDOrderDate] [nvarchar](10) NULL,"
                tSql &= Environment.NewLine & " [FTOrderBy] [nvarchar](50) NULL,"
                tSql &= Environment.NewLine & " [FNOrderType] [int] NULL,"
                tSql &= Environment.NewLine & " [FNHSysCmpId] [int] NULL,"
                tSql &= Environment.NewLine & " [FNHSysCmpRunId] [int] NULL,"
                tSql &= Environment.NewLine & " [FNHSysStyleId] [int] NULL,"
                tSql &= Environment.NewLine & " [FTPORef] [nvarchar](50) NULL,"
                tSql &= Environment.NewLine & " [FNHSysCustId] [int] NULL,"
                tSql &= Environment.NewLine & " [FNHSysAgencyId] [int] NULL,"
                tSql &= Environment.NewLine & " [FNHSysProdTypeId] [int] NULL,"
                tSql &= Environment.NewLine & " [FNHSysBuyerId] [int] NULL,"
                tSql &= Environment.NewLine & " [FTMainMaterial] [nvarchar](500) NULL,"
                tSql &= Environment.NewLine & " [FTCombination] [nvarchar](500) NULL,"
                tSql &= Environment.NewLine & " [FTRemark] [nvarchar](1000) NULL,"
                tSql &= Environment.NewLine & " [FTStateOrderApp] [nvarchar](1) NULL,"
                tSql &= Environment.NewLine & " [FTAppBy] [nvarchar](50) NULL,"
                tSql &= Environment.NewLine & " [FDAppDate] [varchar](10) NULL,"
                tSql &= Environment.NewLine & " [FTAppTime] [varchar](8) NULL,"
                tSql &= Environment.NewLine & " [FNJobState] [int] NULL,"
                tSql &= Environment.NewLine & " [FTStateBy] [nvarchar](50) NULL,"
                tSql &= Environment.NewLine & " [FDStateDate] [varchar](10) NULL,"
                tSql &= Environment.NewLine & " [FTStateTime] [varchar](8) NULL,"
                tSql &= Environment.NewLine & " [FTImage1] [nvarchar](100) NULL,"
                tSql &= Environment.NewLine & " [FTImage2] [nvarchar](100) NULL,"
                tSql &= Environment.NewLine & " [FTImage3] [nvarchar](100) NULL,"
                tSql &= Environment.NewLine & " [FTImage4] [nvarchar](100) NULL,"
                tSql &= Environment.NewLine & " [FNHSysBrandId] [int] NULL,"
                tSql &= Environment.NewLine & " [FNHSysBuyId] [int] NULL,"
                tSql &= Environment.NewLine & " [FTCancelAppBy] [nvarchar](50) NULL,"
                tSql &= Environment.NewLine & " [FDCancelAppDate] [varchar](10) NULL,"
                tSql &= Environment.NewLine & " [FDCancelAppTime] [varchar](8) NULL,"
                tSql &= Environment.NewLine & " [FTCancelAppRemark] [nvarchar](500) NULL,"
                tSql &= Environment.NewLine & " [FTPOTradingCo] [nvarchar](30) NULL,"
                tSql &= Environment.NewLine & " [FTPOItem] [nvarchar](30) NULL,"
                tSql &= Environment.NewLine & " [FTPOCreateDate] [varchar](10) NULL,"
                tSql &= Environment.NewLine & " [FNHSysMerTeamId] [int] NULL,"
                tSql &= Environment.NewLine & " [FNHSysPlantId] [int] NULL,"
                tSql &= Environment.NewLine & " [FNHSysBuyGrpId] [int] NULL,"
                tSql &= Environment.NewLine & " [FNHSysMainCategoryId] [int] NULL,"
                tSql &= Environment.NewLine & " [FNHSysVenderPramId] [int] NULL,"
                tSql &= Environment.NewLine & " [FTOrderCreateStatus] [nvarchar](1) NULL,"
                tSql &= Environment.NewLine & " [FTImportUser] [nvarchar](50) NULL,"
                tSql &= Environment.NewLine & " [FDImportDate] [nvarchar](10) NULL,"
                tSql &= Environment.NewLine & " [FTImportTime] [nvarchar](8) NULL,"
                tSql &= Environment.NewLine & " [FNRowImport] [Int],"
                tSql &= Environment.NewLine & " [FTStyle] [nvarchar](30) NULL,"
                tSql &= Environment.NewLine & " [FNHSysSeasonId] [Int] NULL"
                tSql &= Environment.NewLine & "  )"
                tSql &= Environment.NewLine & " INSERT INTO  #Tab"
                tSql &= Environment.NewLine & "                              ([FTInsUser],[FDInsDate],[FTInsTime]"
                tSql &= Environment.NewLine & "                              ,[FTUpdUser],[FDUpdDate],[FTUpdTime]"
                tSql &= Environment.NewLine & "                              ,[FTOrderNo],[FDOrderDate],[FTOrderBy],[FNOrderType]"
                tSql &= Environment.NewLine & "                              ,[FNHSysCmpId],[FNHSysCmpRunId],[FNHSysStyleId],[FTPORef]"
                tSql &= Environment.NewLine & "                              ,[FNHSysCustId],[FNHSysAgencyId],[FNHSysProdTypeId],[FNHSysBuyerId]"
                tSql &= Environment.NewLine & "                              ,[FTMainMaterial],[FTCombination],[FTRemark]"
                tSql &= Environment.NewLine & "                              ,[FTStateOrderApp],[FTAppBy],[FDAppDate],[FTAppTime]"
                tSql &= Environment.NewLine & "                              ,[FNJobState],[FTStateBy],[FDStateDate],[FTStateTime]"
                tSql &= Environment.NewLine & "                              ,[FTImage1],[FTImage2],[FTImage3],[FTImage4]"
                tSql &= Environment.NewLine & "                              ,[FNHSysBrandId],[FNHSysBuyId],[FTCancelAppBy],[FDCancelAppDate]"
                tSql &= Environment.NewLine & "                              ,[FDCancelAppTime],[FTCancelAppRemark],[FTPOTradingCo],[FTPOItem]"
                tSql &= Environment.NewLine & "                              ,[FTPOCreateDate],[FNHSysMerTeamId],[FNHSysPlantId],[FNHSysBuyGrpId]"
                tSql &= Environment.NewLine & "                              ,[FNHSysMainCategoryId],[FNHSysVenderPramId],[FTOrderCreateStatus],[FTImportUser],[FDImportDate],[FTImportTime],[FNRowImport],[FTStyle],[FNHSysSeasonId])"
                tSql &= Environment.NewLine & " SELECT NULL AS FTInsUser, CONVERT(VARCHAR(10),GETDATE(),111) AS FDInsDate, CONVERT(VARCHAR(8),GETDATE(),114) AS FTInsTime"
                tSql &= Environment.NewLine & "     , NULL AS FTUpdUser, NULL AS FDUpdDate, NULL AS FTUpdTime"
                tSql &= Environment.NewLine & "     , @FTRunNo + (RIGHT(@FTRunStr, @FNRunning - LEN(CONVERT(VARCHAR(30) , (ROW_NUMBER() OVER( ORDER BY B.FTMerTeamCode ASC,A.FTStyle ASC,A.FTPONO ASC) + @FNRunningNoMax)))) + CONVERT(VARCHAR(30) , (ROW_NUMBER() OVER(ORDER BY B.FTMerTeamCode ASC,A.FTStyle ASC,A.FTPONO ASC) + @FNRunningNoMax))) AS FTOrderNo"
                tSql &= Environment.NewLine & "     ,  A.FTOrderDate , NULL AS FTOrderBy, 17 AS FNOrderType"
                tSql &= Environment.NewLine & "     , NULL AS FNHSysCmpId, " & nFNHSysCmpRunId & " AS FNHSysCmpRunId"
                tSql &= Environment.NewLine & "     , A.FNHSysStyleId"
                tSql &= Environment.NewLine & "     , A.FTPONo, " & nFNHSysCustId & " AS FNHSysCustId"
                tSql &= Environment.NewLine & "     , ISNULL((SELECT L1.FNHSysAgencyId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCNMAgency] AS L1 WITH(NOLOCK) WHERE L1.FTAgencyCode = N'-'),NULL) AS FNHSysAgencyId"
                'tSql &= Environment.NewLine & "     , A.FNHSysProdTypeId"
                tSql &= Environment.NewLine & "     , ISNULL((SELECT L1.FNHSysProdTypeId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMProductType] AS L1 WITH(NOLOCK) WHERE L1.FTProdTypeCode = N'-'),NULL) AS FNHSysProdTypeId"
                tSql &= Environment.NewLine & " , -1 AS FNHSysBuyerId, NULL AS FTMainMaterial, NULL AS FTCombination, NULL AS FTRemark"
                tSql &= Environment.NewLine & "     , NULL AS FTStateOrderApp, NULL AS FTAppBy, NULL AS FDAppDate, NULL AS FTAppTime"
                tSql &= Environment.NewLine & "     , 0 AS FNJobState, NULL AS FTStateBy, NULL AS FDStateDate, NULL AS FTStateTime"
                tSql &= Environment.NewLine & "     , NULL AS FTImage1, NULL AS FTImage2, NULL AS FTImage3, NULL AS FTImage4"
                tSql &= Environment.NewLine & "     , ISNULL((SELECT L1.FNHSysBrandId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMBrand] AS L1 WITH(NOLOCK) WHERE L1.FTBrandCode = N'-'),NULL) AS FNHSysBrandId"
                tSql &= Environment.NewLine & "     , " & nFNHSysBuyId & " AS FNHSysBuyId"
                tSql &= Environment.NewLine & "     , NULL AS FTCancelAppBy, NULL AS FDCancelAppDate, NULL AS FDCancelAppTime, NULL AS FTCancelAppRemark"
                tSql &= Environment.NewLine & "     , '' AS FTPOTrading, NULL AS FTPOItem, '' AS FTPOCreateDate"
                tSql &= Environment.NewLine & "     ," & _FNHSysMerTeamId & " AS FNHSysMerTeamId"
                tSql &= Environment.NewLine & "     , ISNULL((SELECT L1.FNHSysPlantId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMPlant] AS L1 WITH(NOLOCK) WHERE L1.FTPlantCode = N'-'),NULL) AS FNHSysPlantId"
                tSql &= Environment.NewLine & "   , A.FNHSysBuyGrpId"
                tSql &= Environment.NewLine & "     , A.FNHSysMainCategoryId, " & nFNHSysVenderPramId & " AS FNHSysVenderPramId"
                tSql &= Environment.NewLine & "     , N'Y' AS FTOrderCreateStatus, N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' AS FTImportUser , CONVERT(VARCHAR(10),GETDATE(),111) AS FDImportDate, CONVERT(VARCHAR(8),GETDATE(),114) AS FTImportTime,0 AS FNRowImport,A.FTStyle," & Integer.Parse(Val(FNHSysSeasonId.Properties.Tag.ToString)) & " "
                tSql &= Environment.NewLine & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTTempImportOrderTargetPMC] AS A LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMerTeam] AS B WITH(NOLOCK) ON A.FNHSysMerTeamId = B.FNHSysMerTeamId"
                tSql &= Environment.NewLine & " WHERE A.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                tSql &= Environment.NewLine & "      AND A.FNGrpNo IN (SELECT MAX(L1.FNGrpNo)"
                tSql &= Environment.NewLine & "                           FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTTempImportOrderTargetPMC] AS L1 WITH(NOLOCK)"
                tSql &= Environment.NewLine & "                           WHERE L1.FTUserLogin = A.FTUserLogin"
                tSql &= Environment.NewLine & "                                 AND L1.FTPONo = A.FTPONO"
                tSql &= Environment.NewLine & "                                 AND L1.FTStyle = A.FTStyle"
                tSql &= Environment.NewLine & "                           GROUP BY L1.FTPONo)"
                tSql &= Environment.NewLine & " ORDER BY B.FTMerTeamCode ASC,A.FTStyle ASC,A.FTPONO ASC;"
                tSql &= Environment.NewLine & " UPDATE A"
                tSql &= Environment.NewLine & " SET FTGenerateOrderNo = ISNULL((SELECT TOP 1 ISNULL(FTOrderNo,'')"
                tSql &= Environment.NewLine & "                                FROM #Tab"
                tSql &= Environment.NewLine & "                                WHERE FTPORef = A.FTPONO AND FTStyle = A.FTStyle), '')"
                tSql &= Environment.NewLine & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTTempImportOrderTargetPMC] AS A LEFT OUTER JOIN [HITECH_MASTER]..[TMERMMerTeam] AS B WITH(NOLOCK) ON A.FNHSysMerTeamId = B.FNHSysMerTeamId"
                tSql &= Environment.NewLine & " WHERE A.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                tSql &= Environment.NewLine & "      AND A.FTPONo IN ( "
                tSql &= Environment.NewLine & "                              SELECT MAX(L1.FTPONo) "
                tSql &= Environment.NewLine & "                              FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTTempImportOrderTargetPMC] AS L1 WITH(NOLOCK) "
                tSql &= Environment.NewLine & "                              WHERE L1.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                tSql &= Environment.NewLine & "                              GROUP BY L1.FTPONo,L1.FTStyle "
                tSql &= Environment.NewLine & " 	                       )"
                tSql &= Environment.NewLine & " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTOrder]"
                tSql &= Environment.NewLine & "                              ([FTInsUser],[FDInsDate],[FTInsTime]"
                tSql &= Environment.NewLine & "                              ,[FTUpdUser],[FDUpdDate],[FTUpdTime]"
                tSql &= Environment.NewLine & "                              ,[FTOrderNo],[FDOrderDate],[FTOrderBy],[FNOrderType]"
                tSql &= Environment.NewLine & "                              ,[FNHSysCmpId],[FNHSysCmpRunId],[FNHSysStyleId],[FTPORef]"
                tSql &= Environment.NewLine & "                              ,[FNHSysCustId],[FNHSysAgencyId],[FNHSysProdTypeId],[FNHSysBuyerId]"
                tSql &= Environment.NewLine & "                              ,[FTMainMaterial],[FTCombination],[FTRemark]"
                tSql &= Environment.NewLine & "                              ,[FTStateOrderApp],[FTAppBy],[FDAppDate],[FTAppTime]"
                tSql &= Environment.NewLine & "                              ,[FNJobState],[FTStateBy],[FDStateDate],[FTStateTime]"
                tSql &= Environment.NewLine & "                              ,[FTImage1],[FTImage2],[FTImage3],[FTImage4]"
                tSql &= Environment.NewLine & "                              ,[FNHSysBrandId],[FNHSysBuyId],[FTCancelAppBy],[FDCancelAppDate]"
                tSql &= Environment.NewLine & "                              ,[FDCancelAppTime],[FTCancelAppRemark],[FTPOTradingCo],[FTPOItem]"
                tSql &= Environment.NewLine & "                              ,[FTPOCreateDate],[FNHSysMerTeamId],[FNHSysPlantId],[FNHSysBuyGrpId]"
                tSql &= Environment.NewLine & "                              ,[FNHSysMainCategoryId],[FNHSysVenderPramId],[FTOrderCreateStatus],[FTImportUser],[FDImportDate],[FTImportTime],[FNHSysSeasonId],[FNHSysCmpIdCreate])"
                tSql &= Environment.NewLine & " SELECT [FTInsUser],[FDInsDate],[FTInsTime]"
                tSql &= Environment.NewLine & "       ,[FTUpdUser],[FDUpdDate],[FTUpdTime]"
                tSql &= Environment.NewLine & "       ,[FTOrderNo],[FDOrderDate],[FTOrderBy],[FNOrderType]"
                tSql &= Environment.NewLine & "       ,[FNHSysCmpId],[FNHSysCmpRunId],[FNHSysStyleId],[FTPORef]"
                tSql &= Environment.NewLine & "       ,[FNHSysCustId],[FNHSysAgencyId],[FNHSysProdTypeId],[FNHSysBuyerId]"
                tSql &= Environment.NewLine & "       ,[FTMainMaterial],[FTCombination],[FTRemark]"
                tSql &= Environment.NewLine & "       ,[FTStateOrderApp],[FTAppBy],[FDAppDate],[FTAppTime]"
                tSql &= Environment.NewLine & "       ,[FNJobState],[FTStateBy],[FDStateDate],[FTStateTime]"
                tSql &= Environment.NewLine & "       ,[FTImage1],[FTImage2],[FTImage3],[FTImage4]"
                tSql &= Environment.NewLine & "       ,[FNHSysBrandId],[FNHSysBuyId],[FTCancelAppBy],[FDCancelAppDate]"
                tSql &= Environment.NewLine & "       ,[FDCancelAppTime],[FTCancelAppRemark],[FTPOTradingCo],[FTPOItem]"
                tSql &= Environment.NewLine & "       ,[FTPOCreateDate],[FNHSysMerTeamId],[FNHSysPlantId],[FNHSysBuyGrpId]"
                tSql &= Environment.NewLine & "       ,[FNHSysMainCategoryId],[FNHSysVenderPramId],[FTOrderCreateStatus],[FTImportUser],[FDImportDate],[FTImportTime],[FNHSysSeasonId]," & Val(HI.ST.SysInfo.CmpID) & ""
                tSql &= Environment.NewLine & " FROM #Tab"
                tSql &= Environment.NewLine & " DROP TABLE #Tab;"

                '...Merge Transaction TMERTOrderSub And TMERTOrderSub_BreakDown
                '========================================================================================================================================================================
                tSql &= Environment.NewLine & "      create table #TabSub ("
                tSql &= Environment.NewLine & "[FTInsUser] [nvarchar](50) NULL,"
                tSql &= Environment.NewLine & "[FDInsDate] [varchar](10) NULL,"
                tSql &= Environment.NewLine & "[FTInsTime] [varchar](8) NULL,"
                tSql &= Environment.NewLine & "[FTUpdUser] [nvarchar](50) NULL,"
                tSql &= Environment.NewLine & "[FDUpdDate] [varchar](10) NULL,"
                tSql &= Environment.NewLine & "[FTUpdTime] [varchar](8) NULL,"
                tSql &= Environment.NewLine & "[FTOrderNo] [nvarchar](30) NOT NULL,"
                tSql &= Environment.NewLine & "[FTSubOrderNo] [nvarchar](30) NOT NULL,"
                tSql &= Environment.NewLine & "[FDSubOrderDate] [nvarchar](10) NULL,"
                tSql &= Environment.NewLine & "[FTSubOrderBy] [nvarchar](50) NULL,"
                tSql &= Environment.NewLine & "[FDProDate] [nvarchar](10) NULL,"
                tSql &= Environment.NewLine & "[FDShipDate] [nvarchar](10) NULL,"
                tSql &= Environment.NewLine & "[FNHSysBuyId] [int] NULL,"
                tSql &= Environment.NewLine & "[FNHSysContinentId] [int] NULL,"
                tSql &= Environment.NewLine & "[FNHSysCountryId] [int] NULL,"
                tSql &= Environment.NewLine & "[FNHSysProvinceId] [int] NULL,"
                tSql &= Environment.NewLine & "[FNHSysShipModeId] [int] NULL,"
                tSql &= Environment.NewLine & "[FNHSysCurId] [int] NULL,"
                tSql &= Environment.NewLine & "[FNHSysGenderId] [int] NULL,"
                tSql &= Environment.NewLine & "[FNHSysUnitId] [int] NULL,"
                tSql &= Environment.NewLine & "[FNSubOrderState] [int] NULL,"
                tSql &= Environment.NewLine & "[FTStateEmb] [nvarchar](1) NULL,"
                tSql &= Environment.NewLine & "[FTStatePrint] [nvarchar](1) NULL,"
                tSql &= Environment.NewLine & "[FTStateHeat] [nvarchar](1) NULL,"
                tSql &= Environment.NewLine & "[FTStateLaser] [nvarchar](1) NULL,"
                tSql &= Environment.NewLine & "[FTStateWindows] [nvarchar](1) NULL,"
                tSql &= Environment.NewLine & "[FTStateOther1] [nvarchar](1) NULL,"
                tSql &= Environment.NewLine & "[FTOther1Note] [nvarchar](50) NULL,"
                tSql &= Environment.NewLine & "[FTStateOther2] [nvarchar](1) NULL,"
                tSql &= Environment.NewLine & "[FTOther2Note] [nvarchar](50) NULL,"
                tSql &= Environment.NewLine & "[FTStateOther3] [nvarchar](1) NULL,"
                tSql &= Environment.NewLine & "[FTOther3Note1] [nvarchar](50) NULL,"
                tSql &= Environment.NewLine & "[FTRemark] [nvarchar](1000) NULL,"
                tSql &= Environment.NewLine & "[FNHSysShipPortId] [int] NULL,"
                tSql &= Environment.NewLine & "[FDShipDateOrginal] [nvarchar](10) NULL,"
                tSql &= Environment.NewLine & "[FTCustRef] [nvarchar](200) NULL,"
                tSql &= Environment.NewLine & "[FNGrpNo] int NULL)"
                tSql &= Environment.NewLine & " INSERT INTO #TabSub"
                tSql &= Environment.NewLine & " ([FTInsUser],[FDInsDate],[FTInsTime],[FTUpdUser]"
                tSql &= Environment.NewLine & " ,[FDUpdDate],[FTUpdTime],[FTOrderNo],[FTSubOrderNo]"
                tSql &= Environment.NewLine & " ,[FDSubOrderDate],[FTSubOrderBy],[FDProDate],[FDShipDate]"
                tSql &= Environment.NewLine & " ,[FNHSysBuyId],[FNHSysContinentId],[FNHSysCountryId],[FNHSysProvinceId]"
                tSql &= Environment.NewLine & " ,[FNHSysShipModeId],[FNHSysCurId],[FNHSysGenderId],[FNHSysUnitId]"
                tSql &= Environment.NewLine & " ,[FNSubOrderState],[FTStateEmb],[FTStatePrint],[FTStateHeat]"
                tSql &= Environment.NewLine & " ,[FTStateLaser],[FTStateWindows],[FTStateOther1],[FTOther1Note]"
                tSql &= Environment.NewLine & " ,[FTStateOther2],[FTOther2Note],[FTStateOther3],[FTOther3Note1]"
                tSql &= Environment.NewLine & " ,[FTRemark]"
                tSql &= Environment.NewLine & " ,[FNHSysShipPortId]"
                tSql &= Environment.NewLine & " ,[FDShipDateOrginal],[FTCustRef],[FNGrpNo])"
                tSql &= Environment.NewLine & " SELECT DISTINCT  NULL AS FTInsUser , CONVERT(VARCHAR(10),GETDATE(),111) AS FDInsDate, CONVERT(VARCHAR(8),GETDATE(),114) AS FTInsTime"
                tSql &= Environment.NewLine & "     , NULL AS FTUpdUser , NULL AS FDUpdDate, NULL AS FTUpdTime"
                tSql &= Environment.NewLine & "     , A.FTOrderNo"
                tSql &= Environment.NewLine & "     , (A.FTOrderNo + '-' + CHAR(64 + ROW_NUMBER() OVER (PARTITION BY A.FTOrderNo ORDER By A.FTOrderNo,A.FDShipDate,A.FNGrpNo))) AS FTSubOrderNo"     '...แยก sub order no. ตาม destination (country)
                tSql &= Environment.NewLine & "     , A.FDOrderDate AS FDSubOrderDate"
                tSql &= Environment.NewLine & "     , N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' AS FTSubOrderBy"
                tSql &= Environment.NewLine & "     , ISNULL((SELECT  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.[FN_GET_PRODDATE_IMPORT] (A.FDShipDate, " & nFNHSysCustId & ", NULL)), NULL) AS FDProdDate"
                tSql &= Environment.NewLine & "     , A.FDShipDate AS FDShipDate"
                tSql &= Environment.NewLine & "     , NULL AS FNHSysBuyId"
                tSql &= Environment.NewLine & "     , -1 FNHSysContinentId"
                tSql &= Environment.NewLine & "     , -1 AS FNHSysCountryId"
                tSql &= Environment.NewLine & "     , -1 AS FNHSysProvinceId"
                tSql &= Environment.NewLine & "     , -1 AS FNHSysShipModeId"
                tSql &= Environment.NewLine & "     , A.FNHSysCurId AS FNHSysCurId"
                tSql &= Environment.NewLine & "     , -1 FNHSysGenderId"
                tSql &= Environment.NewLine & "     ," & Integer.Parse(Val(FNHSysUnitId.Properties.Tag.ToString())) & "  AS FNHSysUnitId"
                tSql &= Environment.NewLine & "     , NULL AS FNSubOrderState"
                tSql &= Environment.NewLine & "     , C.FTStateEmb AS FTStateEmb"
                tSql &= Environment.NewLine & "     , C.FTStatePrint AS FTStatePrint"
                tSql &= Environment.NewLine & "     , C.FTStateHeat AS FTStateHeat"
                tSql &= Environment.NewLine & "     , C.FTStateLaser AS FTStateLaser"
                tSql &= Environment.NewLine & "     , C.FTStateWindows AS FTStateWindows"
                tSql &= Environment.NewLine & "     , NULL AS FTOther1Note, NULL AS FTOther1Note, NULL AS FTStateOther2, NULL AS FTOther2Note, NULL AS FTStateOther3, NULL AS FTOther3Note1, NULL AS FTRemark"
                tSql &= Environment.NewLine & "     , ISNULL((SELECT TOP 1 L1.FNHSysShipPortId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCNMShipPort] AS L1 WITH(NOLOCK) WHERE L1.FTShipPortCode = N'-'),NULL) AS FNHSysShipPortId"
                tSql &= Environment.NewLine & "     , A.FDShipDateOrginal AS FDShipDateOrginal"
                tSql &= Environment.NewLine & "     , dbo.FN_GetCustomer_Refer(A.FNHSysPlantId, A.FNHSysBuyerId) AS FTCustRef,A.FNGrpNo"
                tSql &= Environment.NewLine & "FROM(SELECT A.FTPONo, C.FDOrderDate, '' AS FTPOTrading, '' AS FTPOItem, 0 AS FNRowImport, A.FTStyle AS FTStyle, C.FTOrderNo"
                tSql &= Environment.NewLine & "     ,A.FDShipDate AS FDShipDate"
                tSql &= Environment.NewLine & "      ,A.FDShipDate AS FDShipDateOrginal,A.FNGrpNo "
                tSql &= Environment.NewLine & "     , ISNULL((SELECT TOP 1 L1.FNHSysPlantId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMPlant] AS L1 WITH(NOLOCK) WHERE L1.FTPlantCode = N'-'),NULL) AS FNHSysPlantId"
                tSql &= Environment.NewLine & "     , ISNULL((SELECT TOP 1 L1.FNHSysBuyerId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMBuyer] AS L1 WITH(NOLOCK) WHERE L1.FTBuyerCode = N'-'),NULL) AS FNHSysBuyerId"
                tSql &= Environment.NewLine & "          , ISNULL((SELECT TOP 1 L1.FNHSysCurId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCNMCustomer] AS L1 WITH(NOLOCK) WHERE L1.FNHSysCustId = C.FNHSysCustId), NULL) AS FNHSysCurId"
                tSql &= Environment.NewLine & "     FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTTempImportOrderTargetPMC] AS A LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMerTeam] AS B WITH(NOLOCK) ON A.FNHSysMerTeamId = B.FNHSysMerTeamId"
                tSql &= Environment.NewLine & "          INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrder] AS C WITH(NOLOCK) ON A.FTGenerateOrderNo = C.FTOrderNo"
                tSql &= Environment.NewLine & "     WHERE A.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                tSql &= Environment.NewLine & "           AND NOT EXISTS (SELECT 'T'"
                tSql &= Environment.NewLine & "                           FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub] AS L2 WITH(NOLOCK)"
                tSql &= Environment.NewLine & "                           WHERE C.FTOrderNo = L2.FTOrderNo)"
                tSql &= Environment.NewLine & "           AND A.FTPONo IN (SELECT MAX(L1.FTPONo)"
                tSql &= Environment.NewLine & "                                FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTTempImportOrderTargetPMC] AS L1 WITH(NOLOCK)"
                tSql &= Environment.NewLine & "                                WHERE L1.FTUserLogin =N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                tSql &= Environment.NewLine & "                                      AND L1.FTPONo = A.FTPONO"
                tSql &= Environment.NewLine & "                                GROUP BY L1.FTPONo,FTStyle)) AS A"
                tSql &= Environment.NewLine & "       LEFT JOIN (SELECT L4.FNHSysStyleId, L4.FTStyleCode, ISNULL(MAX(L3.FTStateEmb),0) AS FTStateEmb, ISNULL(MAX(L3.FTStatePrint),0) AS FTStatePrint, ISNULL(MAX(L3.FTStateHeat),0) AS FTStateHeat, ISNULL(MAX(L3.FTStateLaser),0) AS FTStateLaser, ISNULL(MAX(L3.FTStateWindows),0) AS FTStateWindows"
                tSql &= Environment.NewLine & "                  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTStyle_Part] AS L3 WITH(NOLOCK) INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMStyle] AS L4 WITH(NOLOCK) ON L3.FNHSysStyleId = L4.FNHSysStyleId"
                tSql &= Environment.NewLine & "                  GROUP BY L4.FNHSysStyleId, L4.FTStyleCode ) AS C ON A.FTStyle = C.FTStyleCode"
                tSql &= Environment.NewLine & "ORDER BY A.FTOrderNo ASC;"



                tSql &= Environment.NewLine & "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTOrderSub]"
                tSql &= Environment.NewLine & "         ([FTInsUser],[FDInsDate],[FTInsTime],[FTUpdUser]"
                tSql &= Environment.NewLine & "         ,[FDUpdDate],[FTUpdTime],[FTOrderNo],[FTSubOrderNo]"
                tSql &= Environment.NewLine & "         ,[FDSubOrderDate],[FTSubOrderBy],[FDProDate],[FDShipDate]"
                tSql &= Environment.NewLine & "         ,[FNHSysBuyId],[FNHSysContinentId],[FNHSysCountryId],[FNHSysProvinceId]"
                tSql &= Environment.NewLine & "         ,[FNHSysShipModeId],[FNHSysCurId],[FNHSysGenderId],[FNHSysUnitId]"
                tSql &= Environment.NewLine & "         ,[FNSubOrderState],[FTStateEmb],[FTStatePrint],[FTStateHeat]"
                tSql &= Environment.NewLine & "         ,[FTStateLaser],[FTStateWindows],[FTStateOther1],[FTOther1Note]"
                tSql &= Environment.NewLine & "         ,[FTStateOther2],[FTOther2Note],[FTStateOther3],[FTOther3Note1]"
                tSql &= Environment.NewLine & "         ,[FTRemark]"
                tSql &= Environment.NewLine & "         ,[FNHSysShipPortId]"
                tSql &= Environment.NewLine & "         ,[FDShipDateOrginal],[FTCustRef])"
                tSql &= Environment.NewLine & "   SELECT [FTInsUser],[FDInsDate],[FTInsTime],[FTUpdUser]"
                tSql &= Environment.NewLine & "    ,[FDUpdDate],[FTUpdTime],[FTOrderNo],[FTSubOrderNo]"
                tSql &= Environment.NewLine & "    ,[FDSubOrderDate],[FTSubOrderBy],[FDProDate],[FDShipDate]"
                tSql &= Environment.NewLine & "    ,[FNHSysBuyId],[FNHSysContinentId],[FNHSysCountryId],[FNHSysProvinceId]"
                tSql &= Environment.NewLine & "    ,[FNHSysShipModeId],[FNHSysCurId],[FNHSysGenderId],[FNHSysUnitId]"
                tSql &= Environment.NewLine & "    ,[FNSubOrderState],[FTStateEmb],[FTStatePrint],[FTStateHeat]"
                tSql &= Environment.NewLine & "   ,[FTStateLaser],[FTStateWindows],[FTStateOther1],[FTOther1Note]"
                tSql &= Environment.NewLine & "   ,[FTStateOther2],[FTOther2Note],[FTStateOther3],[FTOther3Note1]"
                tSql &= Environment.NewLine & "    ,[FTRemark]"
                tSql &= Environment.NewLine & "    ,[FNHSysShipPortId]"
                tSql &= Environment.NewLine & "    ,[FDShipDateOrginal],[FTCustRef] "
                tSql &= Environment.NewLine & "   FROM #TabSub"
                tSql &= Environment.NewLine & "    Update A"
                tSql &= Environment.NewLine & "  SET   FTGenerateSubOrderNo = B.FTSubOrderNo"
                tSql &= Environment.NewLine & "   FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTTempImportOrderTargetPMC] AS A INNER JOIN #TabSub AS B  ON A.FTGenerateOrderNo = B.FTOrderNo AND A.FNGrpNo=B.FNGrpNo"
                tSql &= Environment.NewLine & "    WHERE A.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                tSql &= Environment.NewLine & "   DROP TABLE #TabSub"
                tSql &= Environment.NewLine & "DECLARE @TMERTOrderSub_BreakDown_Import AS TABLE ([FTInsUser] [nvarchar](50), [FDInsDate] [varchar](10) NULL, [FTInsTime] [varchar](8) NULL,"
                tSql &= Environment.NewLine & "                              [FTUpdUser] [nvarchar](50) NULL, [FDUpdDate] [varchar](10) NULL, [FTUpdTime] [varchar](8) NULL,"
                tSql &= Environment.NewLine & "                              [FTOrderNo] [nvarchar](30) NOT NULL, [FTSubOrderNo] [nvarchar](30) NOT NULL,"
                tSql &= Environment.NewLine & "                              [FTColorway] [nvarchar](30) NOT NULL, [FTSizeBreakDown] [nvarchar](30) NOT NULL,"
                tSql &= Environment.NewLine & "                              [FNPrice] [numeric](18, 5) NULL, [FNQuantity] [int] NULL, [FNAmt] [numeric](18, 5) NULL,"
                tSql &= Environment.NewLine & "                              [FNHSysMatColorId] [int] NULL, [FNHSysMatSizeId] [int] NULL,"
                tSql &= Environment.NewLine & "                              [FNExtraQty] [numeric](18, 5) NULL, [FNQuantityExtra] [numeric](18, 5) NULL,"
                tSql &= Environment.NewLine & "                              [FNGrandQuantity] [numeric](18, 5) NULL, [FNAmntExtra] [numeric](18, 5) NULL,"
                tSql &= Environment.NewLine & "                              [FNGrandAmnt] [numeric](18, 5) NULL, [FNGarmentQtyTest] [int] NULL,[FNAmntQtyTest] [numeric](18, 5) NULL)"
                tSql &= Environment.NewLine & "INSERT INTO @TMERTOrderSub_BreakDown_Import ([FTInsUser], [FDInsDate], [FTInsTime],"
                tSql &= Environment.NewLine & "				    [FTUpdUser], [FDUpdDate], [FTUpdTime],"
                tSql &= Environment.NewLine & "				    [FTOrderNo], [FTSubOrderNo], [FTColorway],"
                tSql &= Environment.NewLine & "				    [FTSizeBreakDown], [FNPrice],"
                tSql &= Environment.NewLine & "				    [FNQuantity], [FNAmt], [FNHSysMatColorId], [FNHSysMatSizeId],"
                tSql &= Environment.NewLine & "				    [FNExtraQty], [FNQuantityExtra], [FNGrandQuantity],"
                tSql &= Environment.NewLine & "				    [FNAmntExtra], [FNGrandAmnt])"
                tSql &= Environment.NewLine & "SELECT ordImport.FTInsUser, ordImport.FDInsDate, ordImport.FTInsTime,"
                tSql &= Environment.NewLine & "       ordImport.FTUpdUser, ordImport.FDUpdDate, ordImport.FTUpdTime,"
                tSql &= Environment.NewLine & "       ordImport.FTOrderNo, ordImport.FTSubOrderNo, ordImport.FTColorway,"
                tSql &= Environment.NewLine & "       ordImport.FTSizeBreakDown, ordImport.FNPrice, ordImport.FNQuantity,"
                tSql &= Environment.NewLine & "       ordImport.FNAmt, ordImport.FNHSysMatColorId, ordImport.FNHSysMatSizeId,"

                'Select Case FNQtySpecialType
                '    Case 1
                '        tSql &= Environment.NewLine & "            0 AS FNExtraQty"
                '        tSql &= Environment.NewLine & "           ,  " & Integer.Parse(Val(FNQtySpecialTypeQty)) & " AS FNQuantityExtra"
                '    Case 2
                '        tSql &= Environment.NewLine & "            " & FNQtySpecialTypeQty & " AS FNExtraQty"
                '        tSql &= Environment.NewLine & "           , CEILING((ordImport.FNQuantity * " & FNQtySpecialTypeQty & ")/100.00) AS FNQuantityExtra"
                '    Case Else
                tSql &= Environment.NewLine & "       0 AS FNExtraQty,0 AS FNQuantityExtra"
                '  End Select

                tSql &= Environment.NewLine & " , ordImport.FNGrandQuantity,"
                tSql &= Environment.NewLine & "       ordImport.FNAmntExtra, ordImport.FNGrandAmnt"
                tSql &= Environment.NewLine & "FROM (SELECT NULL AS FTInsUser, CONVERT(VARCHAR(10),GETDATE(),111) AS FDInsDate, CONVERT(VARCHAR(8),GETDATE(),114) AS FTInsTime"
                tSql &= Environment.NewLine & "           , NULL AS FTUpdUser, NULL AS FDUpdDate, NULL AS FTUpdTime"
                tSql &= Environment.NewLine & "           , AA.FTGenerateOrderNo AS FTOrderNo"
                tSql &= Environment.NewLine & "           , (MM3.FTSubOrderNo) AS FTSubOrderNo"
                tSql &= Environment.NewLine & "           , A.FTMapColor AS FTColorway"
                tSql &= Environment.NewLine & "           , A.FTMapSize AS FTSizeBreakDown"
                tSql &= Environment.NewLine & "           , ISNULL(MAX(A.FNFOBPrice),0) AS FNPrice"
                tSql &= Environment.NewLine & "           , ISNULL(SUM(A.FNQuantity), 0) AS FNQuantity"
                tSql &= Environment.NewLine & "           , ISNULL((SUM(A.FNQuantity) * MAX(A.FNFOBPrice)), 0) AS FNAmt"
                tSql &= Environment.NewLine & "           , C.FNHSysMatColorId AS FNHSysMatColorId"
                tSql &= Environment.NewLine & "           , D.FNHSysMatSizeId AS FNHSysMatSizeId"
                tSql &= Environment.NewLine & "           , 0 AS FNExtraQty"
                tSql &= Environment.NewLine & "           , 0 AS FNQuantityExtra"
                tSql &= Environment.NewLine & "           , ISNULL(SUM(0), 0) AS FNGrandQuantity"
                tSql &= Environment.NewLine & "           , 0 AS FNAmntExtra"
                tSql &= Environment.NewLine & "           , ISNULL((SUM(A.FNQuantity) * MAX(A.FNFOBPrice)), NULL) AS FNGrandAmnt"
                tSql &= Environment.NewLine & "      FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTTempImportOrderTargetPMC] AS AA WITH(NOLOCK)"
                tSql &= Environment.NewLine & "           INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub] AS MM3 WITH(NOLOCK) ON AA.FTGenerateOrderNo = MM3.FTOrderNo AND AA.FTGenerateSubOrderNo = MM3.FTSubOrderNo "
                tSql &= Environment.NewLine & "           INNER JOIN (SELECT A.*"
                tSql &= Environment.NewLine & "      ,    -1 AS FNHSysCountryId"
                tSql &= Environment.NewLine & "           FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTTempImportOrderTargetPMC_D] AS A WITH(NOLOCK)"
                tSql &= Environment.NewLine & "           WHERE FTUserLogin='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                tSql &= Environment.NewLine & "    ) AS A  ON  AA.FTUserLogin = A.FTUserLogin AND AA.FTPONo = A.FTPONo  AND AA.FTStyle = A.FTStyle AND AA.FNGrpNo = A.FNGrpNo  AND MM3.FNHSysCountryId = A.FNHSysCountryId"
                tSql &= Environment.NewLine & "           INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrder] AS B WITH(NOLOCK) ON AA.FTGenerateOrderNo = B.FTOrderNo"
                tSql &= Environment.NewLine & "           INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMatColor] AS C (NOLOCK) ON A.FTMapColor = C.FTMatColorCode"
                tSql &= Environment.NewLine & "           INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMatSize] AS D (NOLOCK) ON A.FTMapSize = D.FTMatSizeCode"
                tSql &= Environment.NewLine & "      WHERE A.FNQuantity > 0"
                tSql &= Environment.NewLine & "      GROUP BY AA.FTGenerateOrderNo, A.FTPONo,  A.FTStyle, MM3.FTSubOrderNo, A.FTMapColor, A.FTMapSize, C.FNHSysMatColorId, D.FNHSysMatSizeId"
                tSql &= Environment.NewLine & "      ) AS ordImport"
                tSql &= Environment.NewLine & "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTOrderSub_BreakDown] ([FTInsUser],[FDInsDate],[FTInsTime]"
                tSql &= Environment.NewLine & "                 ,[FTUpdUser],[FDUpdDate],[FTUpdTime]"
                tSql &= Environment.NewLine & "                 ,[FTOrderNo],[FTSubOrderNo]"
                tSql &= Environment.NewLine & "                 ,[FTColorway],[FTSizeBreakDown]"
                tSql &= Environment.NewLine & "                 ,[FNPrice],[FNQuantity],[FNAmt]"
                tSql &= Environment.NewLine & "                 ,[FNHSysMatColorId],[FNHSysMatSizeId]"
                tSql &= Environment.NewLine & "                 ,[FNExtraQty],[FNQuantityExtra]"
                tSql &= Environment.NewLine & "                 ,[FNGrandQuantity]"
                tSql &= Environment.NewLine & "                 ,[FNAmntExtra]"
                tSql &= Environment.NewLine & "                 ,[FNGrandAmnt]"
                tSql &= Environment.NewLine & "                 ,[FNGarmentQtyTest]"
                tSql &= Environment.NewLine & "                 ,[FNAmntQtyTest],[FNPriceOrg])"
                tSql &= Environment.NewLine & "SELECT aa.[FTInsUser], aa.[FDInsDate], aa.[FTInsTime],"
                tSql &= Environment.NewLine & "       aa.[FTUpdUser], aa.[FDUpdDate], aa.[FTUpdTime],"
                tSql &= Environment.NewLine & "       aa.[FTOrderNo], aa.[FTSubOrderNo], aa.[FTColorway],"
                tSql &= Environment.NewLine & "       aa.[FTSizeBreakDown], aa.[FNPrice],"
                tSql &= Environment.NewLine & "       aa.[FNQuantity], aa.[FNAmt], aa.[FNHSysMatColorId], aa.[FNHSysMatSizeId],"
                tSql &= Environment.NewLine & "       aa.[FNExtraQty], aa.[FNQuantityExtra]"
                tSql &= Environment.NewLine & "      ,(aa.[FNQuantity] + aa.[FNQuantityExtra]) AS  [FNGrandQuantity]"
                'tSql &= Environment.NewLine & "      , aa.[FNGrandQuantity]"
                tSql &= Environment.NewLine & "      , aa.[FNAmntExtra], aa.[FNGrandAmnt],0 AS FNGarmentQtyTest ,0 AS FNAmntQtyTest, aa.[FNPrice]"
                tSql &= Environment.NewLine & "FROM @TMERTOrderSub_BreakDown_Import AS aa"
                tSql &= Environment.NewLine & "WHERE NOT EXISTS (SELECT 'T'"
                tSql &= Environment.NewLine & "                  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub_BreakDown AS bb (NOLOCK)"
                tSql &= Environment.NewLine & "                  WHERE bb.FTOrderNo = aa.FTOrderNo"
                tSql &= Environment.NewLine & "                        AND bb.FTSubOrderNo = aa.FTSubOrderNo"
                tSql &= Environment.NewLine & "                        AND bb.FTColorway = aa.FTColorway"
                tSql &= Environment.NewLine & "                        AND bb.FTSizeBreakDown = aa.FTSizeBreakDown"
                tSql &= Environment.NewLine & "                  );"
                '========================================================================================================================================================================

                If HI.Conn.SQLConn.Execute_Tran(tSql, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    'MsgBox("Step Append New Order No !!!")
                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    _bImportComplete = False
                Else
                    HI.Conn.SQLConn.Tran.Commit()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    _bImportComplete = True
                End If

            Else
                'MsgBox("พบปัญหาในการบันทึกรายการนำเข้าข้อมูลใบสั่งผลิต !!!", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, My.Application.Info.Title)
            End If



        Catch ex As Exception



        End Try

        Return _bImportComplete

    End Function

    Private Function W_PRCbImportFactoryOrderCMS(_Spls) As Boolean
        '...last modify 2014/12/19 when drop field Amount, Qty TMERTOrder_BreakDown, TMERTOrder, TMERTOrderSub

        Dim _bImportComplete As Boolean = False

        Dim tMsgSplash As String = ""

        Try
            Dim _FNHSysMerTeamId As Integer = -1
            Dim nFNHSysCustId As Integer
            Dim nFNHSysCmpRunId As Integer
            Dim nFNHSysBuyId As Integer
            Dim nFNHSysVenderPramId As Integer
            Dim FNQtySpecialType As Integer = 0
            Dim FNQtySpecialTypeQty As Double = 0
            Dim _dtper As DataTable
            Dim _Qry As String = ""


            _FNHSysMerTeamId = Me.SysMerTeamId

            tSql = "SELECT TOP 1 FNQtySpecialType, FNQtySpecial "
            tSql &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMBuyGrp AS M WITH(NOLOCK) "
            tSql &= vbCrLf & " WHERE FNHSysBuyGrpId=" & Integer.Parse(Val(FNHSysBuyGrpId.Properties.Tag.ToString)) & ""
            _dtper = HI.Conn.SQLConn.GetDataTable(tSql, Conn.DB.DataBaseName.DB_MERCHAN)

            For Each R As DataRow In _dtper.Rows

                FNQtySpecialType = Integer.Parse(Val(R!FNQtySpecialType.ToString()))
                FNQtySpecialTypeQty = Double.Parse(Val(R!FNQtySpecial.ToString()))

                Exit For
            Next

            nFNHSysCustId = Val(Me.FNHSysCustId.Properties.Tag) : nFNHSysCmpRunId = Val(Me.FNHSysCmpRunId.Properties.Tag) : nFNHSysBuyId = Val(Me.FNHSysBuyId.Properties.Tag)

            tSql = ""
            tSql = "SELECT TOP 1 A.FNHSysVenderPramId FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMVenderPram] AS A WITH(NOLOCK) WHERE A.FNHSysVenderPramId = " & Val(Me.FNHSysVenderPramId.Properties.Tag)

            nFNHSysVenderPramId = Val(HI.Conn.SQLConn.GetField(tSql, HI.Conn.DB.DataBaseName.DB_MASTER, "0"))

            If nFNHSysVenderPramId <= 0 Then Return False

            Dim oDBdtImport As System.Data.DataTable

            tSql = "  SELECT  " & _FNHSysMerTeamId & " AS FNHSysMerTeamId, '' AS FTPONo, '' AS FTPOTrading, '' AS FTPOCreateDate, A.FTOrderDate AS FTOrderDate"
            tSql &= Environment.NewLine & "         , " & nFNHSysBuyId & " AS FNHSysBuyId, " & nFNHSysCmpRunId & " AS FNHSysCmpRunId, " & nFNHSysCustId & " AS FNHSysCustId"
            tSql &= Environment.NewLine & "    ,N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'  AS FTOrderBy, 0 AS FNOrderType, 0 AS FNJobState"
            tSql &= Environment.NewLine & "    ,A.FTStyle AS FTStyle, '' AS FDShipDate, '' AS FDShipDateOrginal, A.FNHSysBuyGrpId,A.FNHSysGenderId, A.FNHSysProdTypeId"
            tSql &= Environment.NewLine & "    ,A.FNHSysPlantId,'' AS FTYear, A.FNHSysMainCategoryId,A.FNHSysSeasonId AS FTPlanningSeason,A.FNHSysCountryId,A.FNHSysBuyerId"
            tSql &= Environment.NewLine & "   FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTTempImportOrderTargetCMS] AS A "
            tSql &= Environment.NewLine & "       WHERE A.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            tSql &= Environment.NewLine & "   ORDER BY  A.FTStyle ASC;"

            oDBdtImport = HI.Conn.SQLConn.GetDataTable(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN)

            If Not oDBdtImport Is Nothing AndAlso oDBdtImport.Rows.Count > 0 Then
                '...generate facotry order no.
                HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_MERCHAN)
                HI.Conn.SQLConn.SqlConnectionOpen()
                HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
                HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

                tSql = ""
                tSql = "DECLARE @DBName NVARCHAR(30);"
                tSql &= Environment.NewLine & "DECLARE @TblName NVARCHAR(30);"
                tSql &= Environment.NewLine & "DECLARE @DocType NVARCHAR(30);"
                tSql &= Environment.NewLine & "DECLARE @GetFotmat NVARCHAR(30);"
                tSql &= Environment.NewLine & "DECLARE @AddPrefix NVARCHAR(30);"
                tSql &= Environment.NewLine & "SET @DBName = N'" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "';"
                tSql &= Environment.NewLine & "SET @TblName = N'TMERTOrder';"
                tSql &= Environment.NewLine & "SET @DocType = '0';"
                tSql &= Environment.NewLine & "SET @GetFotmat = '';"
                tSql &= Environment.NewLine & "SET @AddPrefix = N'" & HI.TL.CboList.GetListRefer("FNOrderType", 0) & HI.UL.ULF.rpQuoted(Me.FNHSysCmpRunId.Text.Trim()) & "';"
                tSql &= Environment.NewLine & "DECLARE @tblSrcConfigDoc AS TABLE(FTRunNo NVARCHAR(30), FTRunStr NVARCHAR(30), FNRunning INT, FNRunningNoMax INT);"
                tSql &= Environment.NewLine & "INSERT INTO @tblSrcConfigDoc(FTRunNo, FTRunStr, FNRunning, FNRunningNoMax)"
                tSql &= Environment.NewLine & "EXEC SP_GET_FACTORY_ORDERNO_MAX @DBName, @TblName, @DocType, @GetFotmat,@AddPrefix;"
                tSql &= Environment.NewLine & "DECLARE @FTRunNo        AS NVARCHAR(30);"
                tSql &= Environment.NewLine & "DECLARE @FTRunStr       AS NVARCHAR(30);"
                tSql &= Environment.NewLine & "DECLARE @FNRunning      AS INT;"
                tSql &= Environment.NewLine & "DECLARE @FNRunningNoMax AS INT;"
                tSql &= Environment.NewLine & "SELECT @FTRunNo = A.FTRunNo, @FTRunStr = A.FTRunStr, @FNRunning = A.FNRunning, @FNRunningNoMax = A.FNRunningNoMax"
                tSql &= Environment.NewLine & "FROM @tblSrcConfigDoc AS A;"
                tSql &= Environment.NewLine & "--SET @FNRunningNoMax = @FNRunningNoMax + 1;"
                tSql &= Environment.NewLine & " create table #Tab("
                tSql &= Environment.NewLine & " [FTInsUser] [nvarchar](50) NULL,"
                tSql &= Environment.NewLine & " [FDInsDate] [varchar](10) NULL,"
                tSql &= Environment.NewLine & " [FTInsTime] [varchar](8) NULL,"
                tSql &= Environment.NewLine & " [FTUpdUser] [nvarchar](50) NULL,"
                tSql &= Environment.NewLine & " [FDUpdDate] [varchar](10) NULL,"
                tSql &= Environment.NewLine & " [FTUpdTime] [varchar](8) NULL,"
                tSql &= Environment.NewLine & " [FTOrderNo] [nvarchar](30) NOT NULL,"
                tSql &= Environment.NewLine & " [FDOrderDate] [nvarchar](10) NULL,"
                tSql &= Environment.NewLine & " [FTOrderBy] [nvarchar](50) NULL,"
                tSql &= Environment.NewLine & " [FNOrderType] [int] NULL,"
                tSql &= Environment.NewLine & " [FNHSysCmpId] [int] NULL,"
                tSql &= Environment.NewLine & " [FNHSysCmpRunId] [int] NULL,"
                tSql &= Environment.NewLine & " [FNHSysStyleId] [int] NULL,"
                tSql &= Environment.NewLine & " [FTPORef] [nvarchar](50) NULL,"
                tSql &= Environment.NewLine & " [FNHSysCustId] [int] NULL,"
                tSql &= Environment.NewLine & " [FNHSysAgencyId] [int] NULL,"
                tSql &= Environment.NewLine & " [FNHSysProdTypeId] [int] NULL,"
                tSql &= Environment.NewLine & " [FNHSysBuyerId] [int] NULL,"
                tSql &= Environment.NewLine & " [FTMainMaterial] [nvarchar](500) NULL,"
                tSql &= Environment.NewLine & " [FTCombination] [nvarchar](500) NULL,"
                tSql &= Environment.NewLine & " [FTRemark] [nvarchar](1000) NULL,"
                tSql &= Environment.NewLine & " [FTStateOrderApp] [nvarchar](1) NULL,"
                tSql &= Environment.NewLine & " [FTAppBy] [nvarchar](50) NULL,"
                tSql &= Environment.NewLine & " [FDAppDate] [varchar](10) NULL,"
                tSql &= Environment.NewLine & " [FTAppTime] [varchar](8) NULL,"
                tSql &= Environment.NewLine & " [FNJobState] [int] NULL,"
                tSql &= Environment.NewLine & " [FTStateBy] [nvarchar](50) NULL,"
                tSql &= Environment.NewLine & " [FDStateDate] [varchar](10) NULL,"
                tSql &= Environment.NewLine & " [FTStateTime] [varchar](8) NULL,"
                tSql &= Environment.NewLine & " [FTImage1] [nvarchar](100) NULL,"
                tSql &= Environment.NewLine & " [FTImage2] [nvarchar](100) NULL,"
                tSql &= Environment.NewLine & " [FTImage3] [nvarchar](100) NULL,"
                tSql &= Environment.NewLine & " [FTImage4] [nvarchar](100) NULL,"
                tSql &= Environment.NewLine & " [FNHSysBrandId] [int] NULL,"
                tSql &= Environment.NewLine & " [FNHSysBuyId] [int] NULL,"
                tSql &= Environment.NewLine & " [FTCancelAppBy] [nvarchar](50) NULL,"
                tSql &= Environment.NewLine & " [FDCancelAppDate] [varchar](10) NULL,"
                tSql &= Environment.NewLine & " [FDCancelAppTime] [varchar](8) NULL,"
                tSql &= Environment.NewLine & " [FTCancelAppRemark] [nvarchar](500) NULL,"
                tSql &= Environment.NewLine & " [FTPOTradingCo] [nvarchar](30) NULL,"
                tSql &= Environment.NewLine & " [FTPOItem] [nvarchar](30) NULL,"
                tSql &= Environment.NewLine & " [FTPOCreateDate] [varchar](10) NULL,"
                tSql &= Environment.NewLine & " [FNHSysMerTeamId] [int] NULL,"
                tSql &= Environment.NewLine & " [FNHSysPlantId] [int] NULL,"
                tSql &= Environment.NewLine & " [FNHSysBuyGrpId] [int] NULL,"
                tSql &= Environment.NewLine & " [FNHSysMainCategoryId] [int] NULL,"
                tSql &= Environment.NewLine & " [FNHSysVenderPramId] [int] NULL,"
                tSql &= Environment.NewLine & " [FTOrderCreateStatus] [nvarchar](1) NULL,"
                tSql &= Environment.NewLine & " [FTImportUser] [nvarchar](50) NULL,"
                tSql &= Environment.NewLine & " [FDImportDate] [nvarchar](10) NULL,"
                tSql &= Environment.NewLine & " [FTImportTime] [nvarchar](8) NULL,"
                tSql &= Environment.NewLine & " [FNRowImport] [Int],"
                tSql &= Environment.NewLine & " [FTStyle] [nvarchar](30) NULL,"
                tSql &= Environment.NewLine & " [FNHSysSeasonId] [Int] NULL,"
                tSql &= Environment.NewLine & " [FTOrderNoRef] [nvarchar](30) NULL"
                tSql &= Environment.NewLine & "  )"
                tSql &= Environment.NewLine & " INSERT INTO  #Tab"
                tSql &= Environment.NewLine & "                              ([FTInsUser],[FDInsDate],[FTInsTime]"
                tSql &= Environment.NewLine & "                              ,[FTUpdUser],[FDUpdDate],[FTUpdTime]"
                tSql &= Environment.NewLine & "                              ,[FTOrderNo],[FDOrderDate],[FTOrderBy],[FNOrderType]"
                tSql &= Environment.NewLine & "                              ,[FNHSysCmpId],[FNHSysCmpRunId],[FNHSysStyleId],[FTPORef]"
                tSql &= Environment.NewLine & "                              ,[FNHSysCustId],[FNHSysAgencyId],[FNHSysProdTypeId],[FNHSysBuyerId]"
                tSql &= Environment.NewLine & "                              ,[FTMainMaterial],[FTCombination],[FTRemark]"
                tSql &= Environment.NewLine & "                              ,[FTStateOrderApp],[FTAppBy],[FDAppDate],[FTAppTime]"
                tSql &= Environment.NewLine & "                              ,[FNJobState],[FTStateBy],[FDStateDate],[FTStateTime]"
                tSql &= Environment.NewLine & "                              ,[FTImage1],[FTImage2],[FTImage3],[FTImage4]"
                tSql &= Environment.NewLine & "                              ,[FNHSysBrandId],[FNHSysBuyId],[FTCancelAppBy],[FDCancelAppDate]"
                tSql &= Environment.NewLine & "                              ,[FDCancelAppTime],[FTCancelAppRemark],[FTPOTradingCo],[FTPOItem]"
                tSql &= Environment.NewLine & "                              ,[FTPOCreateDate],[FNHSysMerTeamId],[FNHSysPlantId],[FNHSysBuyGrpId]"
                tSql &= Environment.NewLine & "                              ,[FNHSysMainCategoryId],[FNHSysVenderPramId],[FTOrderCreateStatus],[FTImportUser],[FDImportDate],[FTImportTime],[FNRowImport],[FTStyle],[FNHSysSeasonId],[FTOrderNoRef])"
                tSql &= Environment.NewLine & " SELECT NULL AS FTInsUser, CONVERT(VARCHAR(10),GETDATE(),111) AS FDInsDate, CONVERT(VARCHAR(8),GETDATE(),114) AS FTInsTime"
                tSql &= Environment.NewLine & "     , NULL AS FTUpdUser, NULL AS FDUpdDate, NULL AS FTUpdTime"
                tSql &= Environment.NewLine & "     , @FTRunNo + (RIGHT(@FTRunStr, @FNRunning - LEN(CONVERT(VARCHAR(30) , (ROW_NUMBER() OVER( ORDER BY B.FTMerTeamCode ASC,A.FTStyle ASC,A.FTPONO ASC) + @FNRunningNoMax)))) + CONVERT(VARCHAR(30) , (ROW_NUMBER() OVER(ORDER BY B.FTMerTeamCode ASC,A.FTStyle ASC,A.FTPONO ASC) + @FNRunningNoMax))) AS FTOrderNo"
                tSql &= Environment.NewLine & "     ,  A.FTOrderDate , NULL AS FTOrderBy, 0 AS FNOrderType"
                tSql &= Environment.NewLine & "     , NULL AS FNHSysCmpId, " & nFNHSysCmpRunId & " AS FNHSysCmpRunId"
                tSql &= Environment.NewLine & "     , A.FNHSysStyleId"
                tSql &= Environment.NewLine & "     , A.FTPONo, " & nFNHSysCustId & " AS FNHSysCustId"
                tSql &= Environment.NewLine & "     , ISNULL((SELECT L1.FNHSysAgencyId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCNMAgency] AS L1 WITH(NOLOCK) WHERE L1.FTAgencyCode = N'-'),NULL) AS FNHSysAgencyId"
                'tSql &= Environment.NewLine & "     , A.FNHSysProdTypeId"
                tSql &= Environment.NewLine & "     , ISNULL((SELECT L1.FNHSysProdTypeId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMProductType] AS L1 WITH(NOLOCK) WHERE L1.FTProdTypeCode = N'-'),NULL) AS FNHSysProdTypeId"
                tSql &= Environment.NewLine & " , -1 AS FNHSysBuyerId, NULL AS FTMainMaterial, NULL AS FTCombination, NULL AS FTRemark"
                tSql &= Environment.NewLine & "     , NULL AS FTStateOrderApp, NULL AS FTAppBy, NULL AS FDAppDate, NULL AS FTAppTime"
                tSql &= Environment.NewLine & "     , 0 AS FNJobState, NULL AS FTStateBy, NULL AS FDStateDate, NULL AS FTStateTime"
                tSql &= Environment.NewLine & "     , NULL AS FTImage1, NULL AS FTImage2, NULL AS FTImage3, NULL AS FTImage4"
                tSql &= Environment.NewLine & "     , ISNULL((SELECT L1.FNHSysBrandId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMBrand] AS L1 WITH(NOLOCK) WHERE L1.FTBrandCode = N'-'),NULL) AS FNHSysBrandId"
                tSql &= Environment.NewLine & "     , " & nFNHSysBuyId & " AS FNHSysBuyId"
                tSql &= Environment.NewLine & "     , NULL AS FTCancelAppBy, NULL AS FDCancelAppDate, NULL AS FDCancelAppTime, NULL AS FTCancelAppRemark"
                tSql &= Environment.NewLine & "     , '' AS FTPOTrading, NULL AS FTPOItem, '' AS FTPOCreateDate"
                tSql &= Environment.NewLine & "     ," & _FNHSysMerTeamId & " AS FNHSysMerTeamId"
                tSql &= Environment.NewLine & "     , ISNULL((SELECT L1.FNHSysPlantId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMPlant] AS L1 WITH(NOLOCK) WHERE L1.FTPlantCode = N'-'),NULL) AS FNHSysPlantId"
                tSql &= Environment.NewLine & "   , A.FNHSysBuyGrpId"
                tSql &= Environment.NewLine & "     , A.FNHSysMainCategoryId, " & nFNHSysVenderPramId & " AS FNHSysVenderPramId"
                tSql &= Environment.NewLine & "     , N'Y' AS FTOrderCreateStatus, N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' AS FTImportUser , CONVERT(VARCHAR(10),GETDATE(),111) AS FDImportDate, CONVERT(VARCHAR(8),GETDATE(),114) AS FTImportTime,0 AS FNRowImport,A.FTStyle," & Integer.Parse(Val(FNHSysSeasonId.Properties.Tag.ToString)) & ",FTOrderNoRef "
                tSql &= Environment.NewLine & " FROM ( "

                tSql &= Environment.NewLine & "   SELECT D.FTPORef AS FTPONO, A.FTStyle, A.FTOrderDate, A.FNHSysPlantId, A.FNHSysGenderId, A.FNHSysMerTeamId, A.FNHSysProdTypeId, A.FNHSysCountryId, A.FNHSysBuyGrpId, A.FNHSysBuyerId, A.FNHSysStyleId, "
                tSql &= Environment.NewLine & "  A.FNHSysSeasonId, A.FNHSysMainCategoryId,A.FTOrderNoRef"
                tSql &= Environment.NewLine & "  FROM     [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTTempImportOrderTargetCMS AS A INNER JOIN"
                tSql &= Environment.NewLine & "    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTTempImportOrderTargetCMS_D AS D ON A.FTUserLogin = D.FTUserLogin AND A.FTColor = D.FTColor AND A.FTItem = D.FTItem"
                tSql &= Environment.NewLine & "  WHERE  (A.FTUserLogin =N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "')"
                tSql &= Environment.NewLine & "  GROUP BY D.FTPORef, A.FTStyle, A.FTOrderDate, A.FNHSysPlantId, A.FNHSysGenderId, A.FNHSysMerTeamId, A.FNHSysProdTypeId, A.FNHSysCountryId, A.FNHSysBuyGrpId, A.FNHSysBuyerId, A.FNHSysStyleId, "
                tSql &= Environment.NewLine & "     A.FNHSysSeasonId, A.FNHSysMainCategoryId,A.FTOrderNoRef ) AS A"
                tSql &= Environment.NewLine & "   LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMerTeam] AS B WITH(NOLOCK) ON A.FNHSysMerTeamId = B.FNHSysMerTeamId"
                tSql &= Environment.NewLine & " ORDER BY B.FTMerTeamCode ASC,A.FTStyle ASC,A.FTPONO ASC;"
                tSql &= Environment.NewLine & " UPDATE D "
                tSql &= Environment.NewLine & " SET FTGenerateOrderNo = ISNULL((SELECT TOP 1 ISNULL(FTOrderNo,'')"
                tSql &= Environment.NewLine & "                                FROM #Tab"
                tSql &= Environment.NewLine & "                                WHERE FTPORef = D.FTPORef AND FTStyle = A.FTStyle), '')"
                tSql &= Environment.NewLine & "  FROM     [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTTempImportOrderTargetCMS AS A INNER JOIN"
                tSql &= Environment.NewLine & "    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTTempImportOrderTargetCMS_D AS D ON A.FTUserLogin = D.FTUserLogin AND A.FTColor = D.FTColor AND A.FTItem = D.FTItem"
                tSql &= Environment.NewLine & " WHERE D.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "


                tSql &= Environment.NewLine & " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTOrder]"
                tSql &= Environment.NewLine & "                              ([FTInsUser],[FDInsDate],[FTInsTime]"
                tSql &= Environment.NewLine & "                              ,[FTUpdUser],[FDUpdDate],[FTUpdTime]"
                tSql &= Environment.NewLine & "                              ,[FTOrderNo],[FDOrderDate],[FTOrderBy],[FNOrderType]"
                tSql &= Environment.NewLine & "                              ,[FNHSysCmpId],[FNHSysCmpRunId],[FNHSysStyleId],[FTPORef]"
                tSql &= Environment.NewLine & "                              ,[FNHSysCustId],[FNHSysAgencyId],[FNHSysProdTypeId],[FNHSysBuyerId]"
                tSql &= Environment.NewLine & "                              ,[FTMainMaterial],[FTCombination],[FTRemark]"
                tSql &= Environment.NewLine & "                              ,[FTStateOrderApp],[FTAppBy],[FDAppDate],[FTAppTime]"
                tSql &= Environment.NewLine & "                              ,[FNJobState],[FTStateBy],[FDStateDate],[FTStateTime]"
                tSql &= Environment.NewLine & "                              ,[FTImage1],[FTImage2],[FTImage3],[FTImage4]"
                tSql &= Environment.NewLine & "                              ,[FNHSysBrandId],[FNHSysBuyId],[FTCancelAppBy],[FDCancelAppDate]"
                tSql &= Environment.NewLine & "                              ,[FDCancelAppTime],[FTCancelAppRemark],[FTPOTradingCo],[FTPOItem]"
                tSql &= Environment.NewLine & "                              ,[FTPOCreateDate],[FNHSysMerTeamId],[FNHSysPlantId],[FNHSysBuyGrpId]"
                tSql &= Environment.NewLine & "                              ,[FNHSysMainCategoryId],[FNHSysVenderPramId],[FTOrderCreateStatus],[FTImportUser],[FDImportDate],[FTImportTime],[FNHSysSeasonId],[FTOrderNoRef],[FNHSysCmpIdCreate])"
                tSql &= Environment.NewLine & " SELECT [FTInsUser],[FDInsDate],[FTInsTime]"
                tSql &= Environment.NewLine & "       ,[FTUpdUser],[FDUpdDate],[FTUpdTime]"
                tSql &= Environment.NewLine & "       ,[FTOrderNo],[FDOrderDate],[FTOrderBy],[FNOrderType]"
                tSql &= Environment.NewLine & "       ,[FNHSysCmpId],[FNHSysCmpRunId],[FNHSysStyleId],[FTPORef]"
                tSql &= Environment.NewLine & "       ,[FNHSysCustId],[FNHSysAgencyId],[FNHSysProdTypeId],[FNHSysBuyerId]"
                tSql &= Environment.NewLine & "       ,[FTMainMaterial],[FTCombination],[FTRemark]"
                tSql &= Environment.NewLine & "       ,[FTStateOrderApp],[FTAppBy],[FDAppDate],[FTAppTime]"
                tSql &= Environment.NewLine & "       ,[FNJobState],[FTStateBy],[FDStateDate],[FTStateTime]"
                tSql &= Environment.NewLine & "       ,[FTImage1],[FTImage2],[FTImage3],[FTImage4]"
                tSql &= Environment.NewLine & "       ,[FNHSysBrandId],[FNHSysBuyId],[FTCancelAppBy],[FDCancelAppDate]"
                tSql &= Environment.NewLine & "       ,[FDCancelAppTime],[FTCancelAppRemark],[FTPOTradingCo],[FTPOItem]"
                tSql &= Environment.NewLine & "       ,[FTPOCreateDate],[FNHSysMerTeamId],[FNHSysPlantId],[FNHSysBuyGrpId]"
                tSql &= Environment.NewLine & "       ,[FNHSysMainCategoryId],[FNHSysVenderPramId],[FTOrderCreateStatus],[FTImportUser],[FDImportDate],[FTImportTime],[FNHSysSeasonId],[FTOrderNoRef]," & Val(HI.ST.SysInfo.CmpID) & ""
                tSql &= Environment.NewLine & " FROM #Tab"
                tSql &= Environment.NewLine & " DROP TABLE #Tab;"

                '...Merge Transaction TMERTOrderSub And TMERTOrderSub_BreakDown
                '========================================================================================================================================================================
                tSql &= Environment.NewLine & "      create table #TabSub ("
                tSql &= Environment.NewLine & "[FTInsUser] [nvarchar](50) NULL,"
                tSql &= Environment.NewLine & "[FDInsDate] [varchar](10) NULL,"
                tSql &= Environment.NewLine & "[FTInsTime] [varchar](8) NULL,"
                tSql &= Environment.NewLine & "[FTUpdUser] [nvarchar](50) NULL,"
                tSql &= Environment.NewLine & "[FDUpdDate] [varchar](10) NULL,"
                tSql &= Environment.NewLine & "[FTUpdTime] [varchar](8) NULL,"
                tSql &= Environment.NewLine & "[FTOrderNo] [nvarchar](30) NOT NULL,"
                tSql &= Environment.NewLine & "[FTSubOrderNo] [nvarchar](30) NOT NULL,"
                tSql &= Environment.NewLine & "[FDSubOrderDate] [nvarchar](10) NULL,"
                tSql &= Environment.NewLine & "[FTSubOrderBy] [nvarchar](50) NULL,"
                tSql &= Environment.NewLine & "[FDProDate] [nvarchar](10) NULL,"
                tSql &= Environment.NewLine & "[FDShipDate] [nvarchar](10) NULL,"
                tSql &= Environment.NewLine & "[FNHSysBuyId] [int] NULL,"
                tSql &= Environment.NewLine & "[FNHSysContinentId] [int] NULL,"
                tSql &= Environment.NewLine & "[FNHSysCountryId] [int] NULL,"
                tSql &= Environment.NewLine & "[FNHSysProvinceId] [int] NULL,"
                tSql &= Environment.NewLine & "[FNHSysShipModeId] [int] NULL,"
                tSql &= Environment.NewLine & "[FNHSysCurId] [int] NULL,"
                tSql &= Environment.NewLine & "[FNHSysGenderId] [int] NULL,"
                tSql &= Environment.NewLine & "[FNHSysUnitId] [int] NULL,"
                tSql &= Environment.NewLine & "[FNSubOrderState] [int] NULL,"
                tSql &= Environment.NewLine & "[FTStateEmb] [nvarchar](1) NULL,"
                tSql &= Environment.NewLine & "[FTStatePrint] [nvarchar](1) NULL,"
                tSql &= Environment.NewLine & "[FTStateHeat] [nvarchar](1) NULL,"
                tSql &= Environment.NewLine & "[FTStateLaser] [nvarchar](1) NULL,"
                tSql &= Environment.NewLine & "[FTStateWindows] [nvarchar](1) NULL,"
                tSql &= Environment.NewLine & "[FTStateOther1] [nvarchar](1) NULL,"
                tSql &= Environment.NewLine & "[FTOther1Note] [nvarchar](50) NULL,"
                tSql &= Environment.NewLine & "[FTStateOther2] [nvarchar](1) NULL,"
                tSql &= Environment.NewLine & "[FTOther2Note] [nvarchar](50) NULL,"
                tSql &= Environment.NewLine & "[FTStateOther3] [nvarchar](1) NULL,"
                tSql &= Environment.NewLine & "[FTOther3Note1] [nvarchar](50) NULL,"
                tSql &= Environment.NewLine & "[FTRemark] [nvarchar](1000) NULL,"
                tSql &= Environment.NewLine & "[FNHSysShipPortId] [int] NULL,"
                tSql &= Environment.NewLine & "[FDShipDateOrginal] [nvarchar](10) NULL,"
                tSql &= Environment.NewLine & "[FTCustRef] [nvarchar](200) NULL,"
                tSql &= Environment.NewLine & "[FNGrpNo] int NULL)"
                tSql &= Environment.NewLine & " INSERT INTO #TabSub"
                tSql &= Environment.NewLine & " ([FTInsUser],[FDInsDate],[FTInsTime],[FTUpdUser]"
                tSql &= Environment.NewLine & " ,[FDUpdDate],[FTUpdTime],[FTOrderNo],[FTSubOrderNo]"
                tSql &= Environment.NewLine & " ,[FDSubOrderDate],[FTSubOrderBy],[FDProDate],[FDShipDate]"
                tSql &= Environment.NewLine & " ,[FNHSysBuyId],[FNHSysContinentId],[FNHSysCountryId],[FNHSysProvinceId]"
                tSql &= Environment.NewLine & " ,[FNHSysShipModeId],[FNHSysCurId],[FNHSysGenderId],[FNHSysUnitId]"
                tSql &= Environment.NewLine & " ,[FNSubOrderState],[FTStateEmb],[FTStatePrint],[FTStateHeat]"
                tSql &= Environment.NewLine & " ,[FTStateLaser],[FTStateWindows],[FTStateOther1],[FTOther1Note]"
                tSql &= Environment.NewLine & " ,[FTStateOther2],[FTOther2Note],[FTStateOther3],[FTOther3Note1]"
                tSql &= Environment.NewLine & " ,[FTRemark]"
                tSql &= Environment.NewLine & " ,[FNHSysShipPortId]"
                tSql &= Environment.NewLine & " ,[FDShipDateOrginal],[FTCustRef],[FNGrpNo])"
                tSql &= Environment.NewLine & " SELECT DISTINCT  NULL AS FTInsUser , CONVERT(VARCHAR(10),GETDATE(),111) AS FDInsDate, CONVERT(VARCHAR(8),GETDATE(),114) AS FTInsTime"
                tSql &= Environment.NewLine & "     , NULL AS FTUpdUser , NULL AS FDUpdDate, NULL AS FTUpdTime"
                tSql &= Environment.NewLine & "     , A.FTOrderNo"
                tSql &= Environment.NewLine & "     , (A.FTOrderNo + '-' + CHAR(64 + ROW_NUMBER() OVER (PARTITION BY A.FTOrderNo ORDER By A.FTOrderNo,A.FDShipDate,A.FNGrpNo))) AS FTSubOrderNo"     '...แยก sub order no. ตาม destination (country)
                tSql &= Environment.NewLine & "     , A.FDOrderDate AS FDSubOrderDate"
                tSql &= Environment.NewLine & "     , N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' AS FTSubOrderBy"
                tSql &= Environment.NewLine & "     , ISNULL((SELECT  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.[FN_GET_PRODDATE_IMPORT] (A.FDShipDate, " & nFNHSysCustId & ", NULL)), NULL) AS FDProdDate"
                tSql &= Environment.NewLine & "     , A.FDShipDate AS FDShipDate"
                tSql &= Environment.NewLine & "     , NULL AS FNHSysBuyId"
                tSql &= Environment.NewLine & "     , -1 FNHSysContinentId"
                tSql &= Environment.NewLine & "     , -1 AS FNHSysCountryId"
                tSql &= Environment.NewLine & "     , -1 AS FNHSysProvinceId"
                tSql &= Environment.NewLine & "     , -1 AS FNHSysShipModeId"
                tSql &= Environment.NewLine & "     , A.FNHSysCurId AS FNHSysCurId"
                tSql &= Environment.NewLine & "     , -1 FNHSysGenderId"
                tSql &= Environment.NewLine & "     ," & Integer.Parse(Val(FNHSysUnitId.Properties.Tag.ToString())) & "  AS FNHSysUnitId"
                tSql &= Environment.NewLine & "     , NULL AS FNSubOrderState"
                tSql &= Environment.NewLine & "     , C.FTStateEmb AS FTStateEmb"
                tSql &= Environment.NewLine & "     , C.FTStatePrint AS FTStatePrint"
                tSql &= Environment.NewLine & "     , C.FTStateHeat AS FTStateHeat"
                tSql &= Environment.NewLine & "     , C.FTStateLaser AS FTStateLaser"
                tSql &= Environment.NewLine & "     , C.FTStateWindows AS FTStateWindows"
                tSql &= Environment.NewLine & "     , NULL AS FTOther1Note, NULL AS FTOther1Note, NULL AS FTStateOther2, NULL AS FTOther2Note, NULL AS FTStateOther3, NULL AS FTOther3Note1, NULL AS FTRemark"
                tSql &= Environment.NewLine & "     , ISNULL((SELECT TOP 1 L1.FNHSysShipPortId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCNMShipPort] AS L1 WITH(NOLOCK) WHERE L1.FTShipPortCode = N'-'),NULL) AS FNHSysShipPortId"
                tSql &= Environment.NewLine & "     , A.FDShipDateOrginal AS FDShipDateOrginal"
                tSql &= Environment.NewLine & "     , dbo.FN_GetCustomer_Refer(A.FNHSysPlantId, A.FNHSysBuyerId) AS FTCustRef,A.FNGrpNo"
                tSql &= Environment.NewLine & "FROM(SELECT A.FTPONo, C.FDOrderDate, '' AS FTPOTrading, '' AS FTPOItem, 0 AS FNRowImport, A.FTStyle AS FTStyle, C.FTOrderNo"
                tSql &= Environment.NewLine & "     ,A.FDShipDate AS FDShipDate"
                tSql &= Environment.NewLine & "      ,A.FDShipDate AS FDShipDateOrginal,0 AS FNGrpNo "
                tSql &= Environment.NewLine & "     , ISNULL((SELECT TOP 1 L1.FNHSysPlantId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMPlant] AS L1 WITH(NOLOCK) WHERE L1.FTPlantCode = N'-'),NULL) AS FNHSysPlantId"
                tSql &= Environment.NewLine & "     , ISNULL((SELECT TOP 1 L1.FNHSysBuyerId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMBuyer] AS L1 WITH(NOLOCK) WHERE L1.FTBuyerCode = N'-'),NULL) AS FNHSysBuyerId"
                tSql &= Environment.NewLine & "          , ISNULL((SELECT TOP 1 L1.FNHSysCurId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCNMCustomer] AS L1 WITH(NOLOCK) WHERE L1.FNHSysCustId = C.FNHSysCustId), NULL) AS FNHSysCurId"
                tSql &= Environment.NewLine & "     FROM ("

                tSql &= Environment.NewLine & "   SELECT D.FTPORef AS FTPONO, A.FTStyle, A.FTOrderDate, A.FNHSysPlantId, A.FNHSysGenderId, A.FNHSysMerTeamId, A.FNHSysProdTypeId, A.FNHSysCountryId, A.FNHSysBuyGrpId, A.FNHSysBuyerId, A.FNHSysStyleId, "
                tSql &= Environment.NewLine & "     A.FNHSysSeasonId, A.FNHSysMainCategoryId, D.FDShipDate, D.FNHSysCountryId AS FNHSysCountryId_D, D.FTCountry,D.FTGenerateOrderNo"
                tSql &= Environment.NewLine & "   FROM     [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTTempImportOrderTargetCMS AS A INNER JOIN"
                tSql &= Environment.NewLine & "            [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTTempImportOrderTargetCMS_D AS D ON A.FTUserLogin = D.FTUserLogin AND A.FTColor = D.FTColor AND A.FTItem = D.FTItem"
                tSql &= Environment.NewLine & "   WHERE  (A.FTUserLogin =N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "')"
                tSql &= Environment.NewLine & "   GROUP BY D.FTPORef, A.FTStyle, A.FTOrderDate, A.FNHSysPlantId, A.FNHSysGenderId, A.FNHSysMerTeamId, A.FNHSysProdTypeId, A.FNHSysCountryId, A.FNHSysBuyGrpId, A.FNHSysBuyerId, A.FNHSysStyleId, "
                tSql &= Environment.NewLine & "  A.FNHSysSeasonId, A.FNHSysMainCategoryId, D.FDShipDate, D.FNHSysCountryId, D.FTCountry, D.FTGenerateOrderNo"


                tSql &= Environment.NewLine & "   ) AS A LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMerTeam] AS B WITH(NOLOCK) ON A.FNHSysMerTeamId = B.FNHSysMerTeamId"
                tSql &= Environment.NewLine & "          INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrder] AS C WITH(NOLOCK) ON A.FTGenerateOrderNo = C.FTOrderNo"
                tSql &= Environment.NewLine & "     WHERE  NOT EXISTS (SELECT 'T'"
                tSql &= Environment.NewLine & "                           FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub] AS L2 WITH(NOLOCK)"
                tSql &= Environment.NewLine & "                           WHERE C.FTOrderNo = L2.FTOrderNo)"
                tSql &= Environment.NewLine & "           ) AS A"
                tSql &= Environment.NewLine & "       LEFT JOIN (SELECT L4.FNHSysStyleId, L4.FTStyleCode, ISNULL(MAX(L3.FTStateEmb),0) AS FTStateEmb, ISNULL(MAX(L3.FTStatePrint),0) AS FTStatePrint, ISNULL(MAX(L3.FTStateHeat),0) AS FTStateHeat, ISNULL(MAX(L3.FTStateLaser),0) AS FTStateLaser, ISNULL(MAX(L3.FTStateWindows),0) AS FTStateWindows"
                tSql &= Environment.NewLine & "                  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTStyle_Part] AS L3 WITH(NOLOCK) INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMStyle] AS L4 WITH(NOLOCK) ON L3.FNHSysStyleId = L4.FNHSysStyleId"
                tSql &= Environment.NewLine & "                  GROUP BY L4.FNHSysStyleId, L4.FTStyleCode ) AS C ON A.FTStyle = C.FTStyleCode"
                tSql &= Environment.NewLine & "ORDER BY A.FTOrderNo ASC;"



                tSql &= Environment.NewLine & "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTOrderSub]"
                tSql &= Environment.NewLine & "         ([FTInsUser],[FDInsDate],[FTInsTime],[FTUpdUser]"
                tSql &= Environment.NewLine & "         ,[FDUpdDate],[FTUpdTime],[FTOrderNo],[FTSubOrderNo]"
                tSql &= Environment.NewLine & "         ,[FDSubOrderDate],[FTSubOrderBy],[FDProDate],[FDShipDate]"
                tSql &= Environment.NewLine & "         ,[FNHSysBuyId],[FNHSysContinentId],[FNHSysCountryId],[FNHSysProvinceId]"
                tSql &= Environment.NewLine & "         ,[FNHSysShipModeId],[FNHSysCurId],[FNHSysGenderId],[FNHSysUnitId]"
                tSql &= Environment.NewLine & "         ,[FNSubOrderState],[FTStateEmb],[FTStatePrint],[FTStateHeat]"
                tSql &= Environment.NewLine & "         ,[FTStateLaser],[FTStateWindows],[FTStateOther1],[FTOther1Note]"
                tSql &= Environment.NewLine & "         ,[FTStateOther2],[FTOther2Note],[FTStateOther3],[FTOther3Note1]"
                tSql &= Environment.NewLine & "         ,[FTRemark]"
                tSql &= Environment.NewLine & "         ,[FNHSysShipPortId]"
                tSql &= Environment.NewLine & "         ,[FDShipDateOrginal],[FTCustRef])"
                tSql &= Environment.NewLine & "   SELECT [FTInsUser],[FDInsDate],[FTInsTime],[FTUpdUser]"
                tSql &= Environment.NewLine & "    ,[FDUpdDate],[FTUpdTime],[FTOrderNo],[FTSubOrderNo]"
                tSql &= Environment.NewLine & "    ,[FDSubOrderDate],[FTSubOrderBy],[FDProDate],[FDShipDate]"
                tSql &= Environment.NewLine & "    ,[FNHSysBuyId],[FNHSysContinentId],[FNHSysCountryId],[FNHSysProvinceId]"
                tSql &= Environment.NewLine & "    ,[FNHSysShipModeId],[FNHSysCurId],[FNHSysGenderId],[FNHSysUnitId]"
                tSql &= Environment.NewLine & "    ,[FNSubOrderState],[FTStateEmb],[FTStatePrint],[FTStateHeat]"
                tSql &= Environment.NewLine & "   ,[FTStateLaser],[FTStateWindows],[FTStateOther1],[FTOther1Note]"
                tSql &= Environment.NewLine & "   ,[FTStateOther2],[FTOther2Note],[FTStateOther3],[FTOther3Note1]"
                tSql &= Environment.NewLine & "    ,[FTRemark]"
                tSql &= Environment.NewLine & "    ,[FNHSysShipPortId]"
                tSql &= Environment.NewLine & "    ,[FDShipDateOrginal],[FTCustRef] "
                tSql &= Environment.NewLine & "   FROM #TabSub"
                tSql &= Environment.NewLine & "    Update A"
                tSql &= Environment.NewLine & "  SET   FTGenerateSubOrderNo = B.FTSubOrderNo"
                tSql &= Environment.NewLine & "   FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTTempImportOrderTargetCMS_D] AS A"
                tSql &= Environment.NewLine & "  INNER JOIN #TabSub AS B  ON A.FTGenerateOrderNo = B.FTOrderNo AND A.FDShipDate=B.FDShipDate"
                tSql &= Environment.NewLine & "    WHERE A.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                tSql &= Environment.NewLine & "   DROP TABLE #TabSub"
                tSql &= Environment.NewLine & "DECLARE @TMERTOrderSub_BreakDown_Import AS TABLE ([FTInsUser] [nvarchar](50), [FDInsDate] [varchar](10) NULL, [FTInsTime] [varchar](8) NULL,"
                tSql &= Environment.NewLine & "                              [FTUpdUser] [nvarchar](50) NULL, [FDUpdDate] [varchar](10) NULL, [FTUpdTime] [varchar](8) NULL,"
                tSql &= Environment.NewLine & "                              [FTOrderNo] [nvarchar](30) NOT NULL, [FTSubOrderNo] [nvarchar](30) NOT NULL,"
                tSql &= Environment.NewLine & "                              [FTColorway] [nvarchar](30) NOT NULL, [FTSizeBreakDown] [nvarchar](30) NOT NULL,"
                tSql &= Environment.NewLine & "                              [FNPrice] [numeric](18, 5) NULL, [FNQuantity] [int] NULL, [FNAmt] [numeric](18, 5) NULL,"
                tSql &= Environment.NewLine & "                              [FNHSysMatColorId] [int] NULL, [FNHSysMatSizeId] [int] NULL,"
                tSql &= Environment.NewLine & "                              [FNExtraQty] [numeric](18, 5) NULL, [FNQuantityExtra] [numeric](18, 5) NULL,"
                tSql &= Environment.NewLine & "                              [FNGrandQuantity] [numeric](18, 5) NULL, [FNAmntExtra] [numeric](18, 5) NULL,"
                tSql &= Environment.NewLine & "                              [FNGrandAmnt] [numeric](18, 5) NULL, [FNGarmentQtyTest] [int] NULL,[FNAmntQtyTest] [numeric](18, 5) NULL)"
                tSql &= Environment.NewLine & "INSERT INTO @TMERTOrderSub_BreakDown_Import ([FTInsUser], [FDInsDate], [FTInsTime],"
                tSql &= Environment.NewLine & "				    [FTUpdUser], [FDUpdDate], [FTUpdTime],"
                tSql &= Environment.NewLine & "				    [FTOrderNo], [FTSubOrderNo], [FTColorway],"
                tSql &= Environment.NewLine & "				    [FTSizeBreakDown], [FNPrice],"
                tSql &= Environment.NewLine & "				    [FNQuantity], [FNAmt], [FNHSysMatColorId], [FNHSysMatSizeId],"
                tSql &= Environment.NewLine & "				    [FNExtraQty], [FNQuantityExtra], [FNGrandQuantity],"
                tSql &= Environment.NewLine & "				    [FNAmntExtra], [FNGrandAmnt])"
                tSql &= Environment.NewLine & "SELECT ordImport.FTInsUser, ordImport.FDInsDate, ordImport.FTInsTime,"
                tSql &= Environment.NewLine & "       ordImport.FTUpdUser, ordImport.FDUpdDate, ordImport.FTUpdTime,"
                tSql &= Environment.NewLine & "       ordImport.FTOrderNo, ordImport.FTSubOrderNo, ordImport.FTColorway,"
                tSql &= Environment.NewLine & "       ordImport.FTSizeBreakDown, ordImport.FNPrice, ordImport.FNQuantity,"
                tSql &= Environment.NewLine & "       ordImport.FNAmt, ordImport.FNHSysMatColorId, ordImport.FNHSysMatSizeId,"

                'Select Case FNQtySpecialType
                '    Case 1
                '        tSql &= Environment.NewLine & "            0 AS FNExtraQty"
                '        tSql &= Environment.NewLine & "           ,  " & Integer.Parse(Val(FNQtySpecialTypeQty)) & " AS FNQuantityExtra"
                '    Case 2
                '        tSql &= Environment.NewLine & "            " & FNQtySpecialTypeQty & " AS FNExtraQty"
                '        tSql &= Environment.NewLine & "           , CEILING((ordImport.FNQuantity * " & FNQtySpecialTypeQty & ")/100.00) AS FNQuantityExtra"
                '    Case Else
                tSql &= Environment.NewLine & "       0 AS FNExtraQty,0 AS FNQuantityExtra"
                '  End Select

                tSql &= Environment.NewLine & " , ordImport.FNGrandQuantity,"
                tSql &= Environment.NewLine & "       ordImport.FNAmntExtra, ordImport.FNGrandAmnt"
                tSql &= Environment.NewLine & "FROM ("

                tSql &= Environment.NewLine & "    SELECT NULL AS FTInsUser, CONVERT(VARCHAR(10),GETDATE(),111) AS FDInsDate, CONVERT(VARCHAR(8),GETDATE(),114) AS FTInsTime"
                tSql &= Environment.NewLine & "   , NULL AS FTUpdUser, NULL AS FDUpdDate, NULL AS FTUpdTime"
                tSql &= Environment.NewLine & "    , AA.FTGenerateOrderNo AS FTOrderNo"
                tSql &= Environment.NewLine & "    , (MM3.FTSubOrderNo) AS FTSubOrderNo"
                tSql &= Environment.NewLine & "    , BB.FTMapColor AS FTColorway"
                tSql &= Environment.NewLine & "   , BB.FTMapSize AS FTSizeBreakDown"
                tSql &= Environment.NewLine & "    , ISNULL(MAX(BB.FNFOBPrice),0) AS FNPrice"
                tSql &= Environment.NewLine & "    , ISNULL(SUM(AA.FNQuantity), 0) AS FNQuantity"
                tSql &= Environment.NewLine & "   , ISNULL((SUM(AA.FNQuantity) * MAX(BB.FNFOBPrice)), 0) AS FNAmt"
                tSql &= Environment.NewLine & "    , C.FNHSysMatColorId AS FNHSysMatColorId"
                tSql &= Environment.NewLine & "    , D.FNHSysMatSizeId AS FNHSysMatSizeId"
                tSql &= Environment.NewLine & "    , 0 AS FNExtraQty"
                tSql &= Environment.NewLine & "    , 0 AS FNQuantityExtra"
                tSql &= Environment.NewLine & "    , ISNULL(SUM(0), 0) AS FNGrandQuantity"
                tSql &= Environment.NewLine & "     , 0 AS FNAmntExtra"
                tSql &= Environment.NewLine & "   , ISNULL((SUM(AA.FNQuantity) * MAX(BB.FNFOBPrice)), NULL) AS FNGrandAmnt"
                tSql &= Environment.NewLine & "   FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTTempImportOrderTargetCMS_D] AS AA WITH(NOLOCK) INNER JOIN"
                tSql &= Environment.NewLine & "        [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTTempImportOrderTargetCMS] AS BB WITH(NOLOCK) ON AA.FTUserLogin = BB.FTUserLogin AND AA.FTColor = BB.FTColor AND AA.FTItem = BB.FTItem"
                tSql &= Environment.NewLine & "     INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTOrderSub] AS MM3 WITH(NOLOCK) ON AA.FTGenerateOrderNo = MM3.FTOrderNo AND AA.FTGenerateSubOrderNo = MM3.FTSubOrderNo "

                tSql &= Environment.NewLine & "    INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTOrder] AS B WITH(NOLOCK) ON AA.FTGenerateOrderNo = B.FTOrderNo"
                tSql &= Environment.NewLine & "    INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].[dbo].[TMERMMatColor] AS C (NOLOCK) ON BB.FTMapColor = C.FTMatColorCode"
                tSql &= Environment.NewLine & "    INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].[dbo].[TMERMMatSize] AS D (NOLOCK) ON BB.FTMapSize = D.FTMatSizeCode"
                tSql &= Environment.NewLine & "   WHERE  AA.FTUserLogin= N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'  "
                tSql &= Environment.NewLine & "   GROUP BY AA.FTGenerateOrderNo, AA.FTPORef ,  BB.FTStyle, MM3.FTSubOrderNo"
                tSql &= Environment.NewLine & "  , BB.FTMapColor,BB.FTMapSize, C.FNHSysMatColorId, D.FNHSysMatSizeId"

                tSql &= Environment.NewLine & "      ) AS ordImport"
                tSql &= Environment.NewLine & "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTOrderSub_BreakDown] ([FTInsUser],[FDInsDate],[FTInsTime]"
                tSql &= Environment.NewLine & "                 ,[FTUpdUser],[FDUpdDate],[FTUpdTime]"
                tSql &= Environment.NewLine & "                 ,[FTOrderNo],[FTSubOrderNo]"
                tSql &= Environment.NewLine & "                 ,[FTColorway],[FTSizeBreakDown]"
                tSql &= Environment.NewLine & "                 ,[FNPrice],[FNQuantity],[FNAmt]"
                tSql &= Environment.NewLine & "                 ,[FNHSysMatColorId],[FNHSysMatSizeId]"
                tSql &= Environment.NewLine & "                 ,[FNExtraQty],[FNQuantityExtra]"
                tSql &= Environment.NewLine & "                 ,[FNGrandQuantity]"
                tSql &= Environment.NewLine & "                 ,[FNAmntExtra]"
                tSql &= Environment.NewLine & "                 ,[FNGrandAmnt]"
                tSql &= Environment.NewLine & "                 ,[FNGarmentQtyTest]"
                tSql &= Environment.NewLine & "                 ,[FNAmntQtyTest],[FNPriceOrg])"
                tSql &= Environment.NewLine & "SELECT aa.[FTInsUser], aa.[FDInsDate], aa.[FTInsTime],"
                tSql &= Environment.NewLine & "       aa.[FTUpdUser], aa.[FDUpdDate], aa.[FTUpdTime],"
                tSql &= Environment.NewLine & "       aa.[FTOrderNo], aa.[FTSubOrderNo], aa.[FTColorway],"
                tSql &= Environment.NewLine & "       aa.[FTSizeBreakDown], aa.[FNPrice],"
                tSql &= Environment.NewLine & "       aa.[FNQuantity], aa.[FNAmt], aa.[FNHSysMatColorId], aa.[FNHSysMatSizeId],"
                tSql &= Environment.NewLine & "       aa.[FNExtraQty], aa.[FNQuantityExtra]"
                tSql &= Environment.NewLine & "      ,(aa.[FNQuantity] + aa.[FNQuantityExtra]) AS  [FNGrandQuantity]"
                'tSql &= Environment.NewLine & "      , aa.[FNGrandQuantity]"
                tSql &= Environment.NewLine & "      , aa.[FNAmntExtra], aa.[FNGrandAmnt],0 AS FNGarmentQtyTest ,0 AS FNAmntQtyTest, aa.[FNPrice]"
                tSql &= Environment.NewLine & "FROM @TMERTOrderSub_BreakDown_Import AS aa"
                tSql &= Environment.NewLine & "WHERE NOT EXISTS (SELECT 'T'"
                tSql &= Environment.NewLine & "                  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub_BreakDown AS bb (NOLOCK)"
                tSql &= Environment.NewLine & "                  WHERE bb.FTOrderNo = aa.FTOrderNo"
                tSql &= Environment.NewLine & "                        AND bb.FTSubOrderNo = aa.FTSubOrderNo"
                tSql &= Environment.NewLine & "                        AND bb.FTColorway = aa.FTColorway"
                tSql &= Environment.NewLine & "                        AND bb.FTSizeBreakDown = aa.FTSizeBreakDown"
                tSql &= Environment.NewLine & "                  );"
                '========================================================================================================================================================================

                If HI.Conn.SQLConn.Execute_Tran(tSql, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    'MsgBox("Step Append New Order No !!!")
                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    _bImportComplete = False
                Else
                    HI.Conn.SQLConn.Tran.Commit()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    _bImportComplete = True
                End If

            Else
                'MsgBox("พบปัญหาในการบันทึกรายการนำเข้าข้อมูลใบสั่งผลิต !!!", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, My.Application.Info.Title)
            End If



        Catch ex As Exception



        End Try

        Return _bImportComplete

    End Function

#End Region

    Private Sub FTFilePath_ButtonClick(sender As Object, e As XtraEditors.Controls.ButtonPressedEventArgs) Handles FTFilePath.ButtonClick

        If W_PRCbValidateProgrammeVendor() = True Then
            '...default when user click button select file format excel
            Select Case e.Button.Index
                Case 0
                    Try
                        Dim opFileDialog As New System.Windows.Forms.OpenFileDialog
                        Select Case Me.FNImportExcelTargetType.SelectedIndex
                            Case 0, 1
                                opFileDialog.Filter = "Excel Worksheets (*.xls, *.xlsx)|*.xls;*.xlsx|csv files (*.csv)|*.csv"
                            Case 2
                                opFileDialog.Filter = "PDF Files(*.PDF)|*.PDF"
                        End Select
                    
                        Me.FTFilePath.Text = ""

                        Dim oDialogResult As DialogResult = opFileDialog.ShowDialog()

                        If oDialogResult = System.Windows.Forms.DialogResult.OK Then
                            Try
                                If opFileDialog.FileName <> "" Then
                                    Me.FTFilePath.Text = opFileDialog.FileName.ToString()
                                End If
                            Catch ex As Exception
                            End Try
                        End If

                    Catch ex As Exception
                      
                    End Try

                Case Else
                    '...do nothing
            End Select

            Call Me.ocmReadExcel.PerformClick()

        End If

    End Sub

    Private Sub wImportSalesman_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        Me.FNHSysCustId.Focus()
    End Sub

    Private Sub wImportSalesman_Load(sender As Object, e As EventArgs) Handles Me.Load
        Timer1.Enabled = True

        ' Me.otcImportOrderNo.TabPages.Item(1).PageVisible = False

        oGridViewConfirmImport = New DevExpress.XtraGrid.Views.Grid.GridView()
        oGridViewConfirmImport = ogdConfirmImport.Views(0)

        ogdConfirmImport.ViewCollection.Add(oGridViewConfirmImport)
        ogdConfirmImport.MainView = oGridViewConfirmImport

        oGridViewConfirmImport.GridControl = ogdConfirmImport

        Me.ogdConfirmImport.DataSource = Nothing
        Me.ogdConfirmImport.Refresh()

        oGridViewConfirmImport.OptionsView.ShowAutoFilterRow = True
        oGridViewConfirmImport.OptionsBehavior.AllowAddRows = DefaultBoolean.False
        oGridViewConfirmImport.OptionsBehavior.AllowDeleteRows = DefaultBoolean.False
        oGridViewConfirmImport.OptionsBehavior.Editable = False

        Me.oGridViewConfirmImport.RefreshData()
        Me.oGridViewConfirmImport.BestFitColumns()

        Me.oGridViewConfirmImport.OptionsView.ColumnAutoWidth = False
        Me.oGridViewConfirmImport.RefreshData()

        Me.FNHSysUnitId.Text = "PCS"
        Me.FNHSysCurId.Text = "US"

    End Sub

    Private Sub ReadPMCFilePDF()
        Try
            If Me.FNHSysVenderPramId.Text <= "" Or Me.FNHSysVenderPramId.Properties.Tag.ToString = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FNHSysVenderPramId_lbl.Text)
                Exit Sub
            End If

            If Me.FNHSysSeasonId.Text <= "" Or Me.FNHSysSeasonId.Properties.Tag.ToString = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FNHSysSeasonId_lbl.Text)
                Exit Sub
            End If

            If Me.FNHSysUnitId.Text <= "" Or Me.FNHSysUnitId.Properties.Tag.ToString = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FNHSysUnitId_lbl.Text)
                Exit Sub
            End If

            If Me.FNHSysCurId.Text <= "" Or Me.FNHSysCurId.Properties.Tag.ToString = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FNHSysCurId_lbl.Text)
                Exit Sub
            End If

            If Me.FDOrderDate.Text = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, Me.FDOrderDate_lbl.Text)
                Exit Sub
            End If

            Dim _CheckOrderQtyZero As Boolean = False

            Dim oDBdtExcel As System.Data.DataTable

            oDBdtExcel = New System.Data.DataTable

            If Me.FTFilePath.Text.Trim() <> "" Then

                If IO.File.Exists(Me.FTFilePath.Text.Trim().ToString()) Then

                    _bCatchSrcFile = False

                    Dim _FilePath As String
                    _FilePath = Me.FTFilePath.Text.Trim()

                    Dim tFileExtension As String

                    tFileExtension = IO.Path.GetExtension(_FilePath)

                    '=====================================================================================================================================================================================================================
                    '------------------------------------------------------------------------------------------------------------------------
            
                    '=====================================================================================================================================================================================================================

                    Select Case HI.ST.Lang.Language

                        Case HI.ST.Lang.eLang.TH
                            _tSplashText = "กรุณารอสักครู่.....กำลังดำเนินการอ่านข้อมูล"
                        Case Else
                            _tSplashText = "Read data.....please wait"
                    End Select



                    _oSplash = New HI.TL.SplashScreen(_tSplashText, "", True)


                    Application.DoEvents()

                    Dim _FoundPrm As Boolean = False '...กำหนดโครงการ Program HIT/HIG/HIP...

                    _oSplash.UpdateInformation(_tSplashText & Environment.NewLine & "Read Data From PDF File.......")

                    Call W_PRCbLoadMapSize()

                    opshet.LoadDocument(_FilePath)

                    Dim ListDataFromPDFHeader As New List(Of DataTable)
                    ListDataFromPDFHeader = HI.UL.ReadPDFToDataTable.BytescoutReadPdfFile(_FilePath, HI.ST.UserInfo.UserName)

                    Dim _Program As String = ""
                    Dim _Season As String = ""
                    Dim _CommitNo As String = ""
                    Dim _TotalQuantity As Integer = 0

                    Dim RowIdx As Integer = 0
                   
                    For Each DataTemp As DataTable In ListDataFromPDFHeader

                        For Each R As DataRow In DataTemp.Rows


                            Select Case RowIdx
                                Case 0, 1, 2, 4, 6, 7, 10, 11, 12
                                Case 3
                                    Try
                                        If R!F4.ToString.Trim() = "" Then
                                            _Program = R!F5.ToString.Trim()
                                        Else
                                            _Program = R!F4.ToString.Trim()
                                        End If

                                    Catch ex As Exception
                                    End Try
                                Case 5
                                    Try
                                        If R!F4.ToString.Trim() = "" Then
                                            _Season = R!F5.ToString.Trim()
                                        Else
                                            _Season = R!F4.ToString.Trim()
                                        End If

                                    Catch ex As Exception
                                    End Try
                                Case 8


                                    Try
                                        If R!F4.ToString.Trim() = "" Then
                                            _CommitNo = R!F5.ToString.Trim()
                                        Else
                                            _CommitNo = R!F4.ToString.Trim()
                                        End If

                                    Catch ex As Exception
                                    End Try


                                Case 9
                                    Try
                                        If R!F4.ToString.Trim() = "" Then
                                            _TotalQuantity = Integer.Parse(R!F5.ToString.Trim())
                                        Else
                                            _TotalQuantity = Integer.Parse(R!F4.ToString.Trim())
                                        End If

                                    Catch ex As Exception
                                        _TotalQuantity = 0
                                    End Try
                                Case 13

                                Case Else

                            End Select

                            RowIdx = RowIdx + 1
                        Next


                        Exit For
                    Next

                    ListDataFromPDFHeader = Nothing

                    Dim ListDataFromPDF As New List(Of DataTable)
                    ListDataFromPDF = HI.UL.ReadPDFToDataTable.SautinReadPdfFile(_FilePath, HI.ST.UserInfo.UserName)

                    If ListDataFromPDF Is Nothing Then
                        _oSplash.Close()
                        HI.MG.ShowMsg.mInfo("ข้อมูล File ไม่ถูกต้องกรุณาทำการตรวจสอบ !!!", 1504130001, Me.Text, , MessageBoxIcon.Warning)
                        Exit Sub
                    End If

                    If ListDataFromPDF.Count <= 0 Then
                        _oSplash.Close()
                        HI.MG.ShowMsg.mInfo("ข้อมูล File ไม่ถูกต้องกรุณาทำการตรวจสอบ !!!", 1504130001, Me.Text, , MessageBoxIcon.Warning)
                        Exit Sub
                    End If

                    oDBdtExcel = Nothing
                    Dim DTSrcExcel As System.Data.DataTable = Nothing
                    Dim RowIndx As Integer = 0
                    Dim DataTempRead As DataTable
                    For Each DataTemp As DataTable In ListDataFromPDF

                        DataTempRead = DataTemp.Copy
                      
                        DataTempRead.Columns.Add("FTStateDelete", GetType(String))
                        DataTempRead.BeginInit()
                        RowIndx = 0
                        For Each R As DataRow In DataTempRead.Rows
                            RowIndx = RowIndx + 1
                            If R!F1.ToString.ToLower() = "Item Description".ToLower() Then
                                Exit For
                            Else
                                Try
                                    R!FTStateDelete = "1"
                                Catch ex As Exception

                                End Try

                            End If
                        Next
                        DataTempRead.EndInit()

                        DataTempRead.BeginInit()
                        For Each R As DataRow In DataTempRead.Select("FTStateDelete='1'")
                            R.Delete()
                        Next
                        DataTempRead.EndInit()

                        DataTempRead.BeginInit()

                        For I As Integer = DataTempRead.Columns.Count - 1 To 0 Step -1
                            If DataTempRead.Select(" " & DataTempRead.Columns(I).ColumnName & "<>''").Length <= 0 Then
                                DataTempRead.Columns.RemoveAt(I)
                            End If
                        Next
                     
                        DataTempRead.EndInit()
                        Dim ColNo As Integer = 1
                        If oDBdtExcel Is Nothing Then
                            oDBdtExcel = New DataTable
                            ColNo = 1
                            DataTempRead.BeginInit()
                            For Each Collection As DataColumn In DataTempRead.Columns

                                oDBdtExcel.Columns.Add("F" & ColNo.ToString, GetType(String))
                              
                                ColNo = ColNo + 1

                            Next
                            DataTempRead.EndInit()
                        End If

                        ColNo = 1
                        DataTempRead.BeginInit()
                        For Each Collection As DataColumn In DataTempRead.Columns
                            DataTempRead.Columns(ColNo - 1).ColumnName = "F" & ColNo.ToString

                            ColNo = ColNo + 1
                        Next

                        DataTempRead.Rows.RemoveAt(0)

                        DataTempRead.EndInit()
                        oDBdtExcel.Merge(DataTempRead.Copy)

                    Next

                    DTSrcExcel = oDBdtExcel.Copy()

                    DTSrcExcel.BeginInit()
                    For Each R As DataRow In DTSrcExcel.Select("F1='' AND F4=''")
                        R.Delete()
                    Next
                    DTSrcExcel.EndInit()

                    If DTSrcExcel.Rows.Count > 0 And _Season <> "" And _CommitNo <> "" And DTSrcExcel.Columns.Count >= 12 And DTSrcExcel.Columns.Count <= 25 Then


                        Call ImportFilePMCPDFToTemp(DTSrcExcel, _Program, _Season, _CommitNo, _TotalQuantity)

                        Dim _StateCheckPass As Boolean = True
                        If CheckNewStyle() = False Then
                            _StateCheckPass = False
                        End If

                        If _StateCheckPass = True And CheckColorWay() = False Then
                            _StateCheckPass = False
                        End If

                        If _StateCheckPass = True And CheckSize() = False Then
                            _StateCheckPass = False
                        End If


                        If (_StateCheckPass) Then

                            'Dim _Qry As String = ""
                            'Dim _dt As DataTable
                            '_Qry = "SELECT FNGrpNo,FTSizePer  FROM (SELECT FNGrpNo, SUM(FTSizePer) AS FTSizePer"
                            '_Qry &= vbCrLf & "   FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempImportOrderTargetPMC_D AS A WITH(NOLOCK)"
                            '_Qry &= vbCrLf & "   WHERE  (FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "')"
                            '_Qry &= vbCrLf & "   GROUP BY FNGrpNo ) AS A  WHERE FTSizePer <> 100  "
                            '_dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

                            'If _dt.Rows.Count > 0 Then
                            '    _Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempImportOrderTargetPMC WHERE FTUserLogin='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                            '    HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

                            '    _Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempImportOrderTargetPMC_D WHERE FTUserLogin='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                            '    HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

                            'Else

                            '    _Qry = "SELECT FNGrpNo,FTColorPer  FROM (SELECT FNGrpNo, SUM(FTColorPer) AS FTColorPer"
                            '    _Qry &= vbCrLf & " FROM ( SELECT FNGrpNo, FTColor, FTColorPer "
                            '    _Qry &= vbCrLf & "   FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempImportOrderTargetPMC_D AS A WITH(NOLOCK)"
                            '    _Qry &= vbCrLf & "   WHERE  (FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "')"
                            '    _Qry &= vbCrLf & "   GROUP BY FNGrpNo, FTColor, FTColorPer ) AS X"
                            '    _Qry &= vbCrLf & "   GROUP BY FNGrpNo ) AS A  WHERE FTColorPer <> 100  "
                            '    _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)
                            '    If _dt.Rows.Count > 0 Then

                            '        _Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempImportOrderTargetPMC WHERE FTUserLogin='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                            '        HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

                            '        _Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempImportOrderTargetPMC_D WHERE FTUserLogin='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                            '        HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

                            '    Else

                            '        _Qry = " CREATE TABLE #TabData("
                            '        _Qry &= vbCrLf & " [FTUserLogin] [nvarchar](30) NOT NULL,"
                            '        _Qry &= vbCrLf & " [FTPONo] [nvarchar](30) NOT NULL,"
                            '        _Qry &= vbCrLf & " [FTSeason] [nvarchar](30) NOT NULL,"
                            '        _Qry &= vbCrLf & " [FTStyle] [nvarchar](30) NOT NULL,"
                            '        _Qry &= vbCrLf & " [FDShipDate] [nvarchar](10) NOT NULL,"
                            '        _Qry &= vbCrLf & " [FTProdTypeDescription] [nvarchar](200) NOT NULL,"
                            '        _Qry &= vbCrLf & " [FNGrpNo] [int] NOT NULL,"
                            '        _Qry &= vbCrLf & " [FTColor] [nvarchar](50) NOT NULL,"
                            '        _Qry &= vbCrLf & " [FTColorPer] [numeric](18, 2) NULL,"
                            '        _Qry &= vbCrLf & " [FTSize] [nvarchar](50) NOT NULL,"
                            '        _Qry &= vbCrLf & " [FTSizePer] [numeric](18, 2) NULL,"
                            '        _Qry &= vbCrLf & " [FTMapColor] [nvarchar](50) NULL,"
                            '        _Qry &= vbCrLf & " [FTMapSize] [nvarchar](50) NULL,"
                            '        _Qry &= vbCrLf & " [FNQuantity] [numeric](18, 0) NULL)"
                            '        _Qry &= vbCrLf & " INSERT INTO #TabData(FTUserLogin, FTPONo, FTSeason, FTStyle, FDShipDate, FTProdTypeDescription"
                            '        _Qry &= vbCrLf & ", FNGrpNo, FTColor, FTColorPer, FTSize, FTSizePer, FTMapColor, FTMapSize, FNQuantity)"
                            '        _Qry &= vbCrLf & " SELECT XA.FTUserLogin, XA.FTPONo, XA.FTSeason, XA.FTStyle, XA.FDShipDate"
                            '        _Qry &= vbCrLf & ", XA.FTProdTypeDescription, XA.FNGrpNo, XA.FTColor, XA.FTColorPer,B.FTSize, B.FTSizePer,XA.FTMapColor, B.FTMapSize"
                            '        _Qry &= vbCrLf & "	,CONVERT(numeric(18, 0), ((XA.FNColorQty *  CONVERT(numeric(18, 2),  B.FTSizePer)) / 100 ))AS FTSizePerQty"
                            '        _Qry &= vbCrLf & "  FROM     (SELECT FTUserLogin, FTPONo, FTSeason, FTStyle, FDShipDate, FTProdTypeDescription, FNGrpNo, FTColor, FTColorPer, FTMapColor, (FNColorQty * CONVERT(numeric(18, 2), FTColorPer))  / 100 AS FNColorQty"
                            '        _Qry &= vbCrLf & "   FROM      (SELECT FTUserLogin, FTPONo, FTSeason, FTStyle, FDShipDate, FTProdTypeDescription, FNGrpNo, FTColor, FTColorPer, FTMapColor, ISNULL"
                            '        _Qry &= vbCrLf & "   ((SELECT TOP 1 FNGrandQuantity"
                            '        _Qry &= vbCrLf & "    FROM      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempImportOrderTargetPMC AS X WITH(NOLOCK)"
                            '        _Qry &= vbCrLf & "   WHERE   (FTUserLogin = A.FTUserLogin) AND (FNGrpNo = A.FNGrpNo)), 0) AS FNColorQty"
                            '        _Qry &= vbCrLf & "   FROM      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempImportOrderTargetPMC_D AS A WITH(NOLOCK) "
                            '        _Qry &= vbCrLf & "   WHERE   (FTUserLogin ='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "')"
                            '        _Qry &= vbCrLf & "  GROUP BY FTUserLogin, FTPONo, FTSeason, FTStyle, FDShipDate, FTProdTypeDescription, FNGrpNo, FTColor, FTColorPer, FTMapColor) AS A) AS XA INNER JOIN (SELECT FNGrpNo, FTSize, FTSizePer, FTMapSize"
                            '        _Qry &= vbCrLf & "	FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempImportOrderTargetPMC_D WITH(NOLOCK)"
                            '        _Qry &= vbCrLf & "	WHERE  (FTUserLogin ='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "')"
                            '        _Qry &= vbCrLf & "	GROUP BY FNGrpNo, FTSize, FTSizePer, FTMapSize) AS B ON XA.FNGrpNo = B.FNGrpNo"
                            '        _Qry &= vbCrLf & " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempImportOrderTargetPMC_D WHERE (FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "')"
                            '        _Qry &= vbCrLf & " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempImportOrderTargetPMC_D(FTUserLogin, FTPONo, FTSeason, FTStyle, FDShipDate, FTProdTypeDescription"
                            '        _Qry &= vbCrLf & " , FNGrpNo, FTColor, FTColorPer, FTSize, FTSizePer, FTMapColor, FTMapSize, FNQuantity)"
                            '        _Qry &= vbCrLf & " SELECT FTUserLogin, FTPONo, FTSeason, FTStyle, FDShipDate, FTProdTypeDescription, FNGrpNo, FTColor, FTColorPer, FTSize, FTSizePer, FTMapColor, FTMapSize, FNQuantity"
                            '        _Qry &= vbCrLf & " FROM #TabData"
                            '        _Qry &= vbCrLf & " DROP TABLE #TabData"

                            '        HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

                            '    End If

                            'End If

                            Call ShowPMCPDFData()
                            _oSplash.Close()

                            'If _dt.Rows.Count > 0 Then
                            '    HI.MG.ShowMsg.mInfo("ข้อมูล File ไม่ถูกต้อง พบข้อมูลบางรายการ จำนวนรวม ไม่เท่ากับ 100 % กรุณาทำการตรวจสอบ File !!!", 1584935501, Me.Text, , MessageBoxIcon.Warning)
                            'End If

                            '_dt.Dispose()
                        Else

                            Dim _Qry As String = ""
                            _Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempImportOrderTargetPMC WHERE FTUserLogin='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                            HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

                            _Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempImportOrderTargetPMC_D WHERE FTUserLogin='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                            HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

                            _oSplash.Close()
                            HI.MG.ShowMsg.mInfo("ข้อมูล File ไม่ถูกต้องกรุณาทำการตรวจสอบ !!!", 1504130001, Me.Text, , MessageBoxIcon.Warning)
                        End If

                    Else
                        _oSplash.Close()
                        HI.MG.ShowMsg.mInfo("ข้อมูล File ไม่ถูกต้องกรุณาทำการตรวจสอบ !!!", 1504130001, Me.Text, , MessageBoxIcon.Warning)
                        Exit Sub
                    End If

                    _oSplash.Close()

                End If

                _oSplash.Close()


            Else
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.FTFilePath_lbl.Text)

                Me.FTFilePath.Focus()
            End If

        Catch ex As Exception
            _oSplash.Close()
        End Try

    End Sub
    Private Sub ReadPMCFile()
        Try
            If Me.FNHSysVenderPramId.Text <= "" Or Me.FNHSysVenderPramId.Properties.Tag.ToString = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FNHSysVenderPramId_lbl.Text)
                Exit Sub
            End If

            If Me.FNHSysSeasonId.Text <= "" Or Me.FNHSysSeasonId.Properties.Tag.ToString = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FNHSysSeasonId_lbl.Text)
                Exit Sub
            End If

            If Me.FNHSysUnitId.Text <= "" Or Me.FNHSysUnitId.Properties.Tag.ToString = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FNHSysUnitId_lbl.Text)
                Exit Sub
            End If

            If Me.FNHSysCurId.Text <= "" Or Me.FNHSysCurId.Properties.Tag.ToString = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FNHSysCurId_lbl.Text)
                Exit Sub
            End If

            If Me.FDOrderDate.Text = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, Me.FDOrderDate_lbl.Text)
                Exit Sub
            End If

            Dim _CheckOrderQtyZero As Boolean = False

            Dim oDBdtExcel As System.Data.DataTable

            oDBdtExcel = New System.Data.DataTable

            If Me.FTFilePath.Text.Trim() <> "" Then

                If IO.File.Exists(Me.FTFilePath.Text.Trim().ToString()) Then

                    _bCatchSrcFile = False

                    Dim _FilePath As String
                    _FilePath = Me.FTFilePath.Text.Trim()

                    Dim tFileExtension As String

                    tFileExtension = IO.Path.GetExtension(_FilePath)

                    '=====================================================================================================================================================================================================================
                    '------------------------------------------------------------------------------------------------------------------------
                    Dim oSpreadSheetCtrl As DevExpress.XtraSpreadsheet.SpreadsheetControl = New DevExpress.XtraSpreadsheet.SpreadsheetControl()
                    Dim oWorkSheetItem As DevExpress.Spreadsheet.WorksheetCollection = Nothing

                    oSpreadSheetCtrl.LoadDocument(_FilePath)

                    If Not oSpreadSheetCtrl Is Nothing Then

                        oWorkSheetItem = oSpreadSheetCtrl.Document.Worksheets

                        Dim bValidateSheetName As Boolean = False

                        For Each oListWorkSheet As DevExpress.Spreadsheet.Worksheet In oWorkSheetItem
                            '...default sheet1 for automatic import factory order no.
                            If oListWorkSheet.Name.ToString.ToUpper = "Sheet1".ToUpper Then
                                bValidateSheetName = True
                                Exit For
                            Else


                            End If

                        Next

                        If Not bValidateSheetName Then
                            HI.MG.ShowMsg.mInfo("ไม่พบรายการข้อมูลแผ่นงาน {Sheet1} " & Microsoft.VisualBasic.vbCrLf & "กรุณาระบุรายการแผ่นงานที่จะนำเข้ารายการใบสั่งผลิตอัตโนมัติให้ถูกตอ้ง", 1501310001, Me.Text, "Work sheet name !!!", MessageBoxIcon.Warning)
                            Exit Sub
                        Else


                        End If

                    Else
                        HI.MG.ShowMsg.mInfo("ไม่พบรายการข้อมูลแผ่นงาน {Sheet1} " & Microsoft.VisualBasic.vbCrLf & "กรุณาระบุรายการแผ่นงานที่จะนำเข้ารายการใบสั่งผลิตอัตโนมัติให้ถูกตอ้ง", 1501310001, Me.Text, "Work sheet name !!!", MessageBoxIcon.Warning)
                        Exit Sub
                    End If
                    '=====================================================================================================================================================================================================================

                    Select Case HI.ST.Lang.Language

                        Case HI.ST.Lang.eLang.TH
                            _tSplashText = "กรุณารอสักครู่.....กำลังดำเนินการอ่านข้อมูล"
                        Case Else
                            _tSplashText = "Read data.....please wait"
                    End Select



                    _oSplash = New HI.TL.SplashScreen(_tSplashText, "", True)


                    Application.DoEvents()

                    Dim _FoundPrm As Boolean = False '...กำหนดโครงการ Program HIT/HIG/HIP...

                    _oSplash.UpdateInformation(_tSplashText & Environment.NewLine & "Read Data From Excel File.......")

                    Call W_PRCbLoadMapSize()

                    opshet.LoadDocument(_FilePath)

                    oDBdtExcel = HI.UL.ReadExcel.Read(_FilePath, "Sheet1", -1)

                    Dim DTSrcExcel As System.Data.DataTable

                    DTSrcExcel = oDBdtExcel.Copy()

                    DTSrcExcel.BeginInit()
                    For Each R As DataRow In DTSrcExcel.Select("F1='' AND F5=''")
                        R.Delete()
                    Next
                    DTSrcExcel.EndInit()

                    If DTSrcExcel.Rows.Count > 0 And DTSrcExcel.Columns.Count >= 6 And DTSrcExcel.Columns.Count <= 8 Then
                        Call ImportFileToTemp(DTSrcExcel)

                        Dim _StateCheckPass As Boolean = True
                        If CheckNewStyle() = False Then    
                            _StateCheckPass = False
                        End If

                        If _StateCheckPass = True And CheckColorWay() = False Then
                            _StateCheckPass = False
                        End If

                        If _StateCheckPass = True And CheckSize() = False Then
                            _StateCheckPass = False
                        End If


                        If (_StateCheckPass) Then

                            Dim _Qry As String = ""
                            Dim _dt As DataTable
                            _Qry = "SELECT FNGrpNo,FTSizePer  FROM (SELECT FNGrpNo, SUM(FTSizePer) AS FTSizePer"
                            _Qry &= vbCrLf & "   FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempImportOrderTargetPMC_D AS A WITH(NOLOCK)"
                            _Qry &= vbCrLf & "   WHERE  (FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "')"
                            _Qry &= vbCrLf & "   GROUP BY FNGrpNo ) AS A  WHERE FTSizePer <> 100  "
                            _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

                            If _dt.Rows.Count > 0 Then
                                _Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempImportOrderTargetPMC WHERE FTUserLogin='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                                HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

                                _Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempImportOrderTargetPMC_D WHERE FTUserLogin='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                                HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

                            Else

                                _Qry = "SELECT FNGrpNo,FTColorPer  FROM (SELECT FNGrpNo, SUM(FTColorPer) AS FTColorPer"
                                _Qry &= vbCrLf & " FROM ( SELECT FNGrpNo, FTColor, FTColorPer "
                                _Qry &= vbCrLf & "   FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempImportOrderTargetPMC_D AS A WITH(NOLOCK)"
                                _Qry &= vbCrLf & "   WHERE  (FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "')"
                                _Qry &= vbCrLf & "   GROUP BY FNGrpNo, FTColor, FTColorPer ) AS X"
                                _Qry &= vbCrLf & "   GROUP BY FNGrpNo ) AS A  WHERE FTColorPer <> 100  "
                                _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)
                                If _dt.Rows.Count > 0 Then

                                    _Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempImportOrderTargetPMC WHERE FTUserLogin='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                                    HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

                                    _Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempImportOrderTargetPMC_D WHERE FTUserLogin='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                                    HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

                                Else

                                    _Qry = " CREATE TABLE #TabData("
                                    _Qry &= vbCrLf & " [FTUserLogin] [nvarchar](30) NOT NULL,"
                                    _Qry &= vbCrLf & " [FTPONo] [nvarchar](30) NOT NULL,"
                                    _Qry &= vbCrLf & " [FTSeason] [nvarchar](30) NOT NULL,"
                                    _Qry &= vbCrLf & " [FTStyle] [nvarchar](30) NOT NULL,"
                                    _Qry &= vbCrLf & " [FDShipDate] [nvarchar](10) NOT NULL,"
                                    _Qry &= vbCrLf & " [FTProdTypeDescription] [nvarchar](200) NOT NULL,"
                                    _Qry &= vbCrLf & " [FNGrpNo] [int] NOT NULL,"
                                    _Qry &= vbCrLf & " [FTColor] [nvarchar](50) NOT NULL,"
                                    _Qry &= vbCrLf & " [FTColorPer] [numeric](18, 2) NULL,"
                                    _Qry &= vbCrLf & " [FTSize] [nvarchar](50) NOT NULL,"
                                    _Qry &= vbCrLf & " [FTSizePer] [numeric](18, 2) NULL,"
                                    _Qry &= vbCrLf & " [FTMapColor] [nvarchar](50) NULL,"
                                    _Qry &= vbCrLf & " [FTMapSize] [nvarchar](50) NULL,"
                                    _Qry &= vbCrLf & " [FNQuantity] [numeric](18, 0) NULL)"
                                    _Qry &= vbCrLf & " INSERT INTO #TabData(FTUserLogin, FTPONo, FTSeason, FTStyle, FDShipDate, FTProdTypeDescription"
                                    _Qry &= vbCrLf & ", FNGrpNo, FTColor, FTColorPer, FTSize, FTSizePer, FTMapColor, FTMapSize, FNQuantity)"
                                    _Qry &= vbCrLf & " SELECT XA.FTUserLogin, XA.FTPONo, XA.FTSeason, XA.FTStyle, XA.FDShipDate"
                                    _Qry &= vbCrLf & ", XA.FTProdTypeDescription, XA.FNGrpNo, XA.FTColor, XA.FTColorPer,B.FTSize, B.FTSizePer,XA.FTMapColor, B.FTMapSize"
                                    _Qry &= vbCrLf & "	,CONVERT(numeric(18, 0), ((XA.FNColorQty *  CONVERT(numeric(18, 2),  B.FTSizePer)) / 100 ))AS FTSizePerQty"
                                    _Qry &= vbCrLf & "  FROM     (SELECT FTUserLogin, FTPONo, FTSeason, FTStyle, FDShipDate, FTProdTypeDescription, FNGrpNo, FTColor, FTColorPer, FTMapColor, (FNColorQty * CONVERT(numeric(18, 2), FTColorPer))  / 100 AS FNColorQty"
                                    _Qry &= vbCrLf & "   FROM      (SELECT FTUserLogin, FTPONo, FTSeason, FTStyle, FDShipDate, FTProdTypeDescription, FNGrpNo, FTColor, FTColorPer, FTMapColor, ISNULL"
                                    _Qry &= vbCrLf & "   ((SELECT TOP 1 FNGrandQuantity"
                                    _Qry &= vbCrLf & "    FROM      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempImportOrderTargetPMC AS X WITH(NOLOCK)"
                                    _Qry &= vbCrLf & "   WHERE   (FTUserLogin = A.FTUserLogin) AND (FNGrpNo = A.FNGrpNo)), 0) AS FNColorQty"
                                    _Qry &= vbCrLf & "   FROM      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempImportOrderTargetPMC_D AS A WITH(NOLOCK) "
                                    _Qry &= vbCrLf & "   WHERE   (FTUserLogin ='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "')"
                                    _Qry &= vbCrLf & "  GROUP BY FTUserLogin, FTPONo, FTSeason, FTStyle, FDShipDate, FTProdTypeDescription, FNGrpNo, FTColor, FTColorPer, FTMapColor) AS A) AS XA INNER JOIN (SELECT FNGrpNo, FTSize, FTSizePer, FTMapSize"
                                    _Qry &= vbCrLf & "	FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempImportOrderTargetPMC_D WITH(NOLOCK)"
                                    _Qry &= vbCrLf & "	WHERE  (FTUserLogin ='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "')"
                                    _Qry &= vbCrLf & "	GROUP BY FNGrpNo, FTSize, FTSizePer, FTMapSize) AS B ON XA.FNGrpNo = B.FNGrpNo"
                                    _Qry &= vbCrLf & " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempImportOrderTargetPMC_D WHERE (FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "')"
                                    _Qry &= vbCrLf & " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempImportOrderTargetPMC_D(FTUserLogin, FTPONo, FTSeason, FTStyle, FDShipDate, FTProdTypeDescription"
                                    _Qry &= vbCrLf & " , FNGrpNo, FTColor, FTColorPer, FTSize, FTSizePer, FTMapColor, FTMapSize, FNQuantity)"
                                    _Qry &= vbCrLf & " SELECT FTUserLogin, FTPONo, FTSeason, FTStyle, FDShipDate, FTProdTypeDescription, FNGrpNo, FTColor, FTColorPer, FTSize, FTSizePer, FTMapColor, FTMapSize, FNQuantity"
                                    _Qry &= vbCrLf & " FROM #TabData"
                                    _Qry &= vbCrLf & " DROP TABLE #TabData"

                                    HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

                                End If

                            End If

                            Call ShowPMCData()
                            _oSplash.Close()

                            If _dt.Rows.Count > 0 Then
                                HI.MG.ShowMsg.mInfo("ข้อมูล File ไม่ถูกต้อง พบข้อมูลบางรายการ จำนวนรวม ไม่เท่ากับ 100 % กรุณาทำการตรวจสอบ File !!!", 1584935501, Me.Text, , MessageBoxIcon.Warning)
                            End If

                            _dt.Dispose()
                        Else

                            Dim _Qry As String = ""
                            _Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempImportOrderTargetPMC WHERE FTUserLogin='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                            HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

                            _Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempImportOrderTargetPMC_D WHERE FTUserLogin='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                            HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

                            _oSplash.Close()
                            HI.MG.ShowMsg.mInfo("ข้อมูล File ไม่ถูกต้องกรุณาทำการตรวจสอบ !!!", 1504130001, Me.Text, , MessageBoxIcon.Warning)
                        End If

                    Else
                        _oSplash.Close()
                        HI.MG.ShowMsg.mInfo("ข้อมูล File ไม่ถูกต้องกรุณาทำการตรวจสอบ !!!", 1504130001, Me.Text, , MessageBoxIcon.Warning)
                        Exit Sub
                    End If

                    _oSplash.Close()
                    
                    End If

                    _oSplash.Close()


            Else
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.FTFilePath_lbl.Text)

                Me.FTFilePath.Focus()
            End If

        Catch ex As Exception
            _oSplash.Close()
        End Try

    End Sub

    Private Sub ReadCMSFile()
        Try
            If Me.FNHSysVenderPramId.Text <= "" Or Me.FNHSysVenderPramId.Properties.Tag.ToString = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FNHSysVenderPramId_lbl.Text)
                Exit Sub
            End If

            If Me.FNHSysSeasonId.Text <= "" Or Me.FNHSysSeasonId.Properties.Tag.ToString = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FNHSysSeasonId_lbl.Text)
                Exit Sub
            End If

            If Me.FNHSysUnitId.Text <= "" Or Me.FNHSysUnitId.Properties.Tag.ToString = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FNHSysUnitId_lbl.Text)
                Exit Sub
            End If

            If Me.FNHSysCurId.Text <= "" Or Me.FNHSysCurId.Properties.Tag.ToString = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FNHSysCurId_lbl.Text)
                Exit Sub
            End If

            If Me.FDOrderDate.Text = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, Me.FDOrderDate_lbl.Text)
                Exit Sub
            End If

            Dim _CheckOrderQtyZero As Boolean = False

            Dim oDBdtExcel As System.Data.DataTable

            oDBdtExcel = New System.Data.DataTable

            If Me.FTFilePath.Text.Trim() <> "" Then

                If IO.File.Exists(Me.FTFilePath.Text.Trim().ToString()) Then

                    _bCatchSrcFile = False

                    Dim _FilePath As String
                    _FilePath = Me.FTFilePath.Text.Trim()

                    Dim tFileExtension As String

                    tFileExtension = IO.Path.GetExtension(_FilePath)

                    '=====================================================================================================================================================================================================================
                    '------------------------------------------------------------------------------------------------------------------------
                    Dim oSpreadSheetCtrl As DevExpress.XtraSpreadsheet.SpreadsheetControl = New DevExpress.XtraSpreadsheet.SpreadsheetControl()
                    Dim oWorkSheetItem As DevExpress.Spreadsheet.WorksheetCollection = Nothing

                    oSpreadSheetCtrl.LoadDocument(_FilePath)

                    If Not oSpreadSheetCtrl Is Nothing Then

                        oWorkSheetItem = oSpreadSheetCtrl.Document.Worksheets

                        Dim bValidateSheetName As Boolean = False

                        For Each oListWorkSheet As DevExpress.Spreadsheet.Worksheet In oWorkSheetItem
                            '...default sheet1 for automatic import factory order no.
                            If oListWorkSheet.Name.ToString.ToUpper = "Sheet1".ToUpper Then
                                bValidateSheetName = True
                                Exit For
                            Else

                            End If
                        Next

                        If Not bValidateSheetName Then
                            HI.MG.ShowMsg.mInfo("ไม่พบรายการข้อมูลแผ่นงาน {Sheet1} " & Microsoft.VisualBasic.vbCrLf & "กรุณาระบุรายการแผ่นงานที่จะนำเข้ารายการใบสั่งผลิตอัตโนมัติให้ถูกตอ้ง", 1501310001, Me.Text, "Work sheet name !!!", MessageBoxIcon.Warning)
                            Exit Sub
                        Else


                        End If

                    Else
                        HI.MG.ShowMsg.mInfo("ไม่พบรายการข้อมูลแผ่นงาน {Sheet1} " & Microsoft.VisualBasic.vbCrLf & "กรุณาระบุรายการแผ่นงานที่จะนำเข้ารายการใบสั่งผลิตอัตโนมัติให้ถูกตอ้ง", 1501310001, Me.Text, "Work sheet name !!!", MessageBoxIcon.Warning)
                        Exit Sub
                    End If
                    '=====================================================================================================================================================================================================================

                    Select Case HI.ST.Lang.Language

                        Case HI.ST.Lang.eLang.TH
                            _tSplashText = "กรุณารอสักครู่.....กำลังดำเนินการอ่านข้อมูล"
                        Case Else
                            _tSplashText = "Read data.....please wait"
                    End Select



                    _oSplash = New HI.TL.SplashScreen(_tSplashText, "", True)


                    Application.DoEvents()

                    Dim _FoundPrm As Boolean = False '...กำหนดโครงการ Program HIT/HIG/HIP...

                    _oSplash.UpdateInformation(_tSplashText & Environment.NewLine & "Read Data From Excel File.......")

                    Call W_PRCbLoadMapSize()

                    opshet.LoadDocument(_FilePath)

                    oDBdtExcel = HI.UL.ReadExcel.Read(_FilePath, "Sheet1", -1)

                    Dim DTSrcExcel As System.Data.DataTable

                    DTSrcExcel = oDBdtExcel.Copy()

                    'DTSrcExcel.BeginInit()
                    'For Each R As DataRow In DTSrcExcel.Select("F1='' AND F5=''")
                    '    R.Delete()
                    'Next
                    'DTSrcExcel.EndInit()

                    If DTSrcExcel.Rows.Count > 0 And DTSrcExcel.Columns.Count > 8 Then
                        Dim _CommitNo As String = ""
                        Dim _Qry As String = ""
                        If DTSrcExcel.Rows(3)!F1.ToString.Trim().ToLower = "Commit:".ToLower Then


                            _CommitNo = DTSrcExcel.Rows(3)!F2.ToString.Trim()

                            _Qry = "SELECT TOP 1 FTOrderNo FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH(NOLOCK) WHERE FTPORef='" & HI.UL.ULF.rpQuoted(_CommitNo) & "' AND FNOrderType=17"

                            If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MERCHAN, "") <> "" Then
                                Call ImportFileCMSToTemp(DTSrcExcel)

                                Dim _StateCheckPass As Boolean = True
                                If CheckNewStyleCMS() = False Then
                                    _StateCheckPass = False
                                End If
                                _oSplash.Close()
                                If _StateCheckPass = True And CheckMappingColorWayAndSize() = False Then
                                    _StateCheckPass = False
                                End If

                                _oSplash = New HI.TL.SplashScreen(_tSplashText, "", True)
                                _oSplash.UpdateInformation(_tSplashText & Environment.NewLine & "Read Data From Excel File.......")

                                If (_StateCheckPass) Then
                                    Call ShowCMSData()
                                    _oSplash.Close()
                                Else

                                    _Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempImportOrderTargetCMS WHERE FTUserLogin='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                                    HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

                                    _Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempImportOrderTargetCMS_D WHERE FTUserLogin='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                                    HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

                                    _oSplash.Close()
                                    HI.MG.ShowMsg.mInfo("ข้อมูล File ไม่ถูกต้องกรุณาทำการตรวจสอบ !!!", 1504130001, Me.Text, , MessageBoxIcon.Warning)

                                End If
                            Else
                                _oSplash.Close()
                                HI.MG.ShowMsg.mInfo("ไม่พบข้อมูล Order Booking Commit No นี้ !!!", 1574138977, Me.Text, , MessageBoxIcon.Warning)
                                Exit Sub
                            End If

                        Else
                            _oSplash.Close()
                            HI.MG.ShowMsg.mInfo("ข้อมูล File ไม่ถูกต้องกรุณาทำการตรวจสอบ ไม่พบเลข Commit No : !!!", 1584134981, Me.Text, _CommitNo, MessageBoxIcon.Warning)
                            Exit Sub
                        End If
                        
                    Else
                        _oSplash.Close()
                        HI.MG.ShowMsg.mInfo("ข้อมูล File ไม่ถูกต้องกรุณาทำการตรวจสอบ !!!", 1504130001, Me.Text, , MessageBoxIcon.Warning)
                        Exit Sub
                    End If

                    _oSplash.Close()

                End If

                _oSplash.Close()


            Else
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.FTFilePath_lbl.Text)

                Me.FTFilePath.Focus()
            End If

        Catch ex As Exception
            _oSplash.Close()
        End Try

    End Sub

    Private Sub ocmReadExcel_Click(sender As Object, e As EventArgs) Handles ocmReadExcel.Click
        Dim _Qry As String = ""
        _Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempImportOrderTargetPMC WHERE FTUserLogin='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
        HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

        _Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempImportOrderTargetPMC_D WHERE FTUserLogin='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
        HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

        _Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempImportOrderTargetCMS WHERE FTUserLogin='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
        HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

        _Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempImportOrderTargetCMS_D WHERE FTUserLogin='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
        HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

        Dim _FNHSysMerTeamId As Integer = 0
        _Qry = "SELECT TOP 1 FNHSysMerTeamId "
        _Qry &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEUserLogin AS A WITH(NOLOCK)"
        _Qry &= vbCrLf & " WHERE FTUserName='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
        _FNHSysMerTeamId = Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_SECURITY)))

        If _FNHSysMerTeamId > 0 Or HI.ST.SysInfo.Admin Then
            SysMerTeamId = _FNHSysMerTeamId
            Select Case FNImportExcelTargetType.SelectedIndex
                Case 0
                    Call ReadPMCFile()
                Case 1
                    Call ReadCMSFile()
                Case 2
                    Call ReadPMCFilePDF()
            End Select
        Else
            HI.MG.ShowMsg.mInfo("ไม่พบข้อมูล Merchandise Team ของ ผู้ใช้งาน ไม่สามารถดำเนินการต่อได้ !!!", 15051407854, Me.Text, , MessageBoxIcon.Warning)

        End If
       
    End Sub

    Private Sub ocmclearclsr_Click(sender As Object, e As EventArgs) Handles ocmclearclsr.Click
        Call W_PRCbClsr()     
    End Sub

    Private Sub ocmExit_Click(sender As Object, e As EventArgs) Handles ocmExit.Click
        Me.Close()
    End Sub

 
    Private Sub ogvImportOrder_RowCellStyle(sender As Object, e As RowCellStyleEventArgs)
        Try
            If e.RowHandle > -1 And e.RowHandle < 3 Then
                e.Appearance.BackColor = Color.YellowGreen
                e.Appearance.ForeColor = Color.DarkBlue
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString())
        End Try
    End Sub

    Private Sub FNHSysBuyId_KeyPress(sender As Object, e As KeyPressEventArgs) Handles FNHSysBuyId.KeyPress

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Try
            'Dim span As TimeSpan = DateTime.Now.Subtract(startTime)

            If Not (_oSplash Is Nothing) And (_oSplash.IsDisposed = False) Then
                Application.DoEvents()
                _oSplash.Updatetime(Format(Now, "HH:mm:ss"))
                Application.DoEvents()
            End If
        Catch ex As Exception
        End Try

    End Sub


    Private Sub FNHSysVenderPramId_EditValueChanged(sender As Object, e As EventArgs) Handles FNHSysVenderPramId.EditValueChanged
        FTFilePath.Text = ""
        ogdConfirmImport.DataSource = Nothing
    End Sub

    Private Sub FNHSysCustId_EditValueChanged(sender As Object, e As EventArgs) Handles FNHSysCustId.EditValueChanged
        Try

            If Me.InvokeRequired Then
                Me.Invoke(New HI.Delegate.Dele.FNHSysEmpTypeId_ValueChanged(AddressOf FNHSysCustId_EditValueChanged), New Object() {sender, e})
            Else

                Dim sSql As String
                sSql = ""
                sSql = "SELECT TOP 1 A.FTCurCode"
                sSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TFINMCurrency AS A (NOLOCK)"
                sSql &= Environment.NewLine & "WHERE A.FNHSysCurId IN (SELECT TOP 1 L1.FNHSysCurId"
                sSql &= Environment.NewLine & "                       FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMCustomer AS L1 (NOLOCK)"
                sSql &= Environment.NewLine & "                       WHERE L1.FTCustCode  = '" & HI.UL.ULF.rpQuoted(FNHSysCustId.Text) & "')"

                Me.FNHSysCurId.Text = HI.Conn.SQLConn.GetField(sSql, HI.Conn.DB.DataBaseName.DB_MASTER, "")

            End If

            ' If Me.FNHSysCustId.Text.Trim <> "" AndAlso Val(Me.FNHSysCustId.Properties.Tag) > 0 Then
          

            'Else
            ' Me.FNHSysCurId.Text = ""
            ' End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub ocmImportOrder_Click(sender As Object, e As EventArgs) Handles ocmImportOrder.Click
        Dim _Qry As String = ""
       

        Try
            If (Not (Me.ogdConfirmImport.DataSource Is Nothing)) Or (Not (Me.ogccms.DataSource Is Nothing)) Then
                Dim _FNHSysMerTeamId As Integer = 0
                _Qry = "SELECT TOP 1 FNHSysMerTeamId "
                _Qry &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEUserLogin AS A WITH(NOLOCK)"
                _Qry &= vbCrLf & " WHERE FTUserName='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _FNHSysMerTeamId = Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_SECURITY)))

                If _FNHSysMerTeamId <= 0 And Not (HI.ST.SysInfo.Admin) Then
                    HI.MG.ShowMsg.mInfo("ไม่พบข้อมูล Merchandise Team ของ ผู้ใช้งาน ไม่สามารถดำเนินการต่อได้ !!!", 15051407854, Me.Text, , MessageBoxIcon.Warning)
                    Exit Sub
                End If

                Dim _dtImport As DataTable = Nothing

                Select Case Me.FNImportExcelTargetType.SelectedIndex
                    Case 0, 2
                        With CType(Me.ogdConfirmImport.DataSource, DataTable)
                            .AcceptChanges()
                            _dtImport = .Copy
                        End With
                    Case 1
                        With CType(Me.ogccms.DataSource, DataTable)
                            .AcceptChanges()
                            _dtImport = .Copy
                        End With
                End Select

                If _dtImport Is Nothing Then
                    HI.MG.ShowMsg.mInfo("ไม่พบข้อมูลในการการเข้าระบบ กรุณาทำการตรวจสอบ !!!", 1504301799, Me.Text, , MessageBoxIcon.Warning)
                    Exit Sub
                Else
                    If _dtImport.Rows.Count <= 0 Then
                        HI.MG.ShowMsg.mInfo("ไม่พบข้อมูลในการการเข้าระบบ กรุณาทำการตรวจสอบ !!!", 1504301799, Me.Text, , MessageBoxIcon.Warning)
                        Exit Sub
                    End If
                End If


                Select Case Me.FNImportExcelTargetType.SelectedIndex
                    Case 1
                        If _dtImport.Select("FTOrderNoRef=''").Length > 0 Then
                            HI.MG.ShowMsg.mInfo("ไม่พบข้อมูล Order Booking ในบางรายการ กรุณาทำการตรวจสอบ !!!", 1504398799, Me.Text, , MessageBoxIcon.Warning)
                            Exit Sub
                        End If

                        If _dtImport.Select("FTSTateColor='1'").Length > 0 Then
                            HI.MG.ShowMsg.mInfo("ไม่พบข้อมูล Color And Size Breakdown ใน Order Booking บางรายการ กรุณาทำการตรวจสอบ !!!", 1504398801, Me.Text, , MessageBoxIcon.Warning)
                            Exit Sub
                        End If

                        If _dtImport.Select("FTSTatePrice='1'").Length > 0 Then
                            HI.MG.ShowMsg.mInfo("ราคา Color And Size Breakdown ไม่ตรงกับ Order Booking บางรายการ กรุณาทำการตรวจสอบ !!!", 1504398801, Me.Text, , MessageBoxIcon.Warning)
                            Exit Sub
                        End If

                End Select

                If W_PRCbValidateConfirmGenerateFactoryOrder() = True Then

                    If HI.MG.ShowMsg.mConfirmProcess("-----", 1404240002, "Confirm") = True Then

                        Dim _Spls As New HI.TL.SplashScreen("Generate Factory Orders Target .....Please Wait...")

                        Dim _StateImport As Boolean = False
                        Select Case Me.FNImportExcelTargetType.SelectedIndex
                            Case 0
                                _StateImport = W_PRCbImportFactoryOrderPMC(_Spls)
                            Case 1
                                _StateImport = W_PRCbImportFactoryOrderCMS(_Spls)
                            Case 2
                                _StateImport = W_PRCbImportFactoryOrderPMCPDF(_Spls)
                        End Select
                        If _StateImport = True Then

                            '...clear temp after process import orders salesman  complete
                            '---------------------------------------------------------------------------------------------------------------------------------------------------------
                            Application.DoEvents()

                            _Spls.UpdateInformation("Finishing Generate Orders Salesman .....Please Wait")

                            Select Case Me.FNImportExcelTargetType.SelectedIndex

                                Case 0, 2
                                    Try
                                        tSql = ""
                                        tSql = "UPDATE A"
                                        tSql &= Environment.NewLine & " SET A.FPOrderImage1 = C.FPStyleImage1,"
                                        tSql &= Environment.NewLine & "    A.FPOrderImage2 = C.FPStyleImage2,"
                                        tSql &= Environment.NewLine & "    A.FPOrderImage3 = C.FPStyleImage3,"
                                        tSql &= Environment.NewLine & "    A.FPOrderImage4 = C.FPStyleImage4"
                                        tSql &= Environment.NewLine & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrder AS A LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTTempImportOrderTargetPMC AS B ON A.FTOrderNo = B.FTGenerateOrderNo"
                                        tSql &= Environment.NewLine & "                          LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TMERMStyle AS C ON A.FNHSysStyleId = C.FNHSysStyleId"
                                        tSql &= Environment.NewLine & " WHERE B.FTUserLogIn = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "';"

                                        If HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN) = True Then
                                        Else
                                        End If

                                    Catch ex As Exception
                                    End Try
                                Case 1
                                    Try
                                        tSql = ""
                                        tSql = "UPDATE A"
                                        tSql &= Environment.NewLine & " SET A.FPOrderImage1 = C.FPStyleImage1,"
                                        tSql &= Environment.NewLine & "    A.FPOrderImage2 = C.FPStyleImage2,"
                                        tSql &= Environment.NewLine & "    A.FPOrderImage3 = C.FPStyleImage3,"
                                        tSql &= Environment.NewLine & "    A.FPOrderImage4 = C.FPStyleImage4"
                                        tSql &= Environment.NewLine & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrder AS A LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTTempImportOrderTargetCMS AS B ON A.FTOrderNo = B.FTGenerateOrderNo"
                                        tSql &= Environment.NewLine & "                          LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TMERMStyle AS C ON A.FNHSysStyleId = C.FNHSysStyleId"
                                        tSql &= Environment.NewLine & " WHERE B.FTUserLogIn = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "';"

                                        If HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN) = True Then
                                        Else
                                        End If

                                    Catch ex As Exception
                                    End Try
                            End Select

                            Dim Cmdstring As String = ""
                            Dim dtstss As DataTable

                            Select Case Me.FNImportExcelTargetType.SelectedIndex

                                Case 0, 2
                                    Try
                                        Cmdstring = "  SELECT  DISTINCT C.FTOrderNo  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTTempImportOrderTargetPMC] AS A "
                                        Cmdstring &= Environment.NewLine & "          INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrder] AS C WITH(NOLOCK) ON A.FTGenerateOrderNo = C.FTOrderNo"
                                        Cmdstring &= Environment.NewLine & "     WHERE A.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"

                                        dtstss = HI.Conn.SQLConn.GetDataTable(Cmdstring, Conn.DB.DataBaseName.DB_MERCHAN)

                                        For Each R As DataRow In dtstss.Rows

                                            Cmdstring = "EXEC [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.USP_GEN_BREAKDOWN_SHIPDESINATION '" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString()) & "'"
                                            HI.Conn.SQLConn.ExecuteOnly(Cmdstring, Conn.DB.DataBaseName.DB_MERCHAN)

                                        Next

                                    Catch ex As Exception
                                    End Try
                                Case 1
                                    Try
                                        Cmdstring = "  SELECT  DISTINCT C.FTOrderNo  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTTempImportOrderTargetCMS] AS A "
                                        Cmdstring &= Environment.NewLine & "          INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrder] AS C WITH(NOLOCK) ON A.FTGenerateOrderNo = C.FTOrderNo"
                                        Cmdstring &= Environment.NewLine & "     WHERE A.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"

                                        dtstss = HI.Conn.SQLConn.GetDataTable(Cmdstring, Conn.DB.DataBaseName.DB_MERCHAN)

                                        For Each R As DataRow In dtstss.Rows

                                            Cmdstring = "EXEC [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.USP_GEN_BREAKDOWN_SHIPDESINATION '" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString()) & "'"
                                            HI.Conn.SQLConn.ExecuteOnly(Cmdstring, Conn.DB.DataBaseName.DB_MERCHAN)

                                        Next

                                    Catch ex As Exception
                                    End Try
                            End Select





                            _Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempImportOrderTargetPMC WHERE FTUserLogin='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                            HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

                            _Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempImportOrderTargetPMC_D WHERE FTUserLogin='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                            HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

                            _Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempImportOrderTargetCMS WHERE FTUserLogin='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                            HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

                            _Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempImportOrderTargetCMS_D WHERE FTUserLogin='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                            HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

                            '---------------------------------------------------------------------------------------------------------------------------------------------------------

                            _bCatchSrcFile = False

                            Select Case Me.FNImportExcelTargetType.SelectedIndex
                                Case 0, 2
                                    Call ShowPMCData()
                                Case 1
                                    Call ShowCMSData()
                            End Select

                            _Spls.Close()

                            HI.MG.ShowMsg.mInfo("Import Factory Orders Target. Complete...", 1504307918, Me.Text, , MessageBoxIcon.Information)

                        Else

                            _Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempImportOrderTargetPMC WHERE FTUserLogin='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                            HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

                            _Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempImportOrderTargetPMC_D WHERE FTUserLogin='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                            HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

                            _Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempImportOrderTargetCMS WHERE FTUserLogin='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                            HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

                            _Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempImportOrderTargetCMS_D WHERE FTUserLogin='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                            HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)
                            _Spls.Close()

                            HI.MG.ShowMsg.mInfo("พบปัญหาในการบันทึกรายการนำเข้าข้อมูลใบสั่งผลิตอัตโนมัติ", 1504300918, Me.Text, , MessageBoxIcon.Warning)
                        End If

                    Else
                        '...do nothing
                    End If

                Else
                    '...do nothing
                End If

            Else
                HI.MG.ShowMsg.mInfo("Cannot Import to Factory Orders Salesman. , Please validate source file for import data !!!", 1504300018, Me.Text, , MessageBoxIcon.Warning)
            End If

        Catch ex As Exception


            _Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempImportOrderTargetPMC WHERE FTUserLogin='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
            HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

            _Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempImportOrderTargetPMC_D WHERE FTUserLogin='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
            HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

            _Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempImportOrderTargetCMS WHERE FTUserLogin='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
            HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

            _Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempImportOrderTargetCMS_D WHERE FTUserLogin='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
            HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)


        End Try

    End Sub

    Private Sub FNImportExcelTargetType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles FNImportExcelTargetType.SelectedIndexChanged
        Call InitGridPMC(Nothing, Nothing)

        Me.otppmc.PageVisible = (FNImportExcelTargetType.SelectedIndex = 0 Or FNImportExcelTargetType.SelectedIndex = 2)
        Me.otpcms.PageVisible = (FNImportExcelTargetType.SelectedIndex = 1)

        'FNHSysSeasonId_lbl.Visible = (FNImportExcelTargetType.SelectedIndex = 0)
        'FNHSysSeasonId.Visible = (FNImportExcelTargetType.SelectedIndex = 0)
        'FNHSysSeasonId_None.Visible = (FNImportExcelTargetType.SelectedIndex = 0)

        'FNHSysUnitId_lbl.Visible = (FNImportExcelTargetType.SelectedIndex = 0)
        'FNHSysUnitId.Visible = (FNImportExcelTargetType.SelectedIndex = 0)
        'FNHSysUnitId_None.Visible = (FNImportExcelTargetType.SelectedIndex = 0)
        Me.FTFilePath.Text = ""
        Me.ogdConfirmImport.DataSource = Nothing
        Me.ogccms.DataSource = Nothing
    End Sub

    Private Sub FNHSysBuyId_EditValueChanged(sender As Object, e As EventArgs) Handles FNHSysBuyId.EditValueChanged

    End Sub

    Private Sub FTFilePath_EditValueChanged(sender As Object, e As EventArgs) Handles FTFilePath.EditValueChanged

    End Sub
End Class
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

Public Class wCheckFileImportOrder

#Region "Variable Declaration"

    Private Const _DBEnum As Integer = HI.Conn.DB.DataBaseName.DB_MERCHAN
    Private Const _nRowHeader As Integer = 3
    Private Const _nStartRowImportExcel As Integer = 3
    Private Const _nStartColImportSizeValExcel As Integer = 49
    Private Const _nStartRowImportCSV As Integer = 3
    Private Const _nStartColImporSizeValCSV As Integer = 49
    Private Const _tTextMatchSizeTerminate As String = "Result"
    Private Const _tTextRowDelimeter As String = "Overall Result|Result"
    Private Const _tHyphenSign As String = "-"
    Private Const _tNotAssigned As String = "Not assigned"
    Private Const _tNA As String = "#"
    Private Const _tUnitImport As String = "UOM"
    Private _tCurtImport As Integer = 0

    '...form dialog confirm map size not exists in system master file
    Private _wMapSizeImportOrder As wMapSizeImportOrder

    Private _bInitialGridBandView As Boolean
    Private _bLoadFirstTime As Boolean = True

    Private tSql As String
    Private _SystemFilePath As String = Application.StartupPath & IIf(Microsoft.VisualBasic.Right(Application.StartupPath, 1) = "\", "", "\") & "Order"

    Private oDBdtImport As System.Data.DataTable
    Private oDBdtSrcOrder As System.Data.DataTable
    Private oDBdtMapSize As System.Data.DataTable
    Private oDBdtStyle As System.Data.DataTable
    Private _oDBdtUserImportMapSizeManual As System.Data.DataTable

    Private _bCatchSrcFile As Boolean

    Private startTime As DateTime

    Private _oSplash As HI.TL.SplashScreen = Nothing
    Private _tSplashText As String

    Dim stopWatch As Stopwatch = New Stopwatch()
    Dim days As System.Int64
    Dim Hours As System.Int64
    Dim Minutes As System.Int64
    Dim Seconds As System.Int64

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

    End Sub

    Private Function W_PRCbValidateProgrammeVendor() As Boolean
        Dim _bRetProgrammeVendor As Boolean = False

        Try
            If Me.FNHSysVenderPramId.Text.Trim() <> "" Then
                _bRetProgrammeVendor = True
            Else
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.FNHSysVenderPramId_lbl.Text)
                Me.FNHSysVenderPramId.Focus()
            End If
        Catch ex As Exception
            If System.Diagnostics.Debugger.IsAttached = True Then
                MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), My.Application.Info.Title)
            End If
        End Try

        Return _bRetProgrammeVendor

    End Function

    Private Function W_PRCbValidateConfirmGenerateFactoryOrder() As Boolean
        Dim _bRet As Boolean = False
        Try

            If Me.FNHSysVenderPramId.Text.Trim() <> "" Then

                If W_PRCbAddNewStyleImport() = True Then
                    _bRet = True
                End If

            Else
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.FNHSysVenderPramId_lbl.Text)
                Me.FNHSysVenderPramId.Focus()
            End If
           
        Catch ex As Exception
            If System.Diagnostics.Debugger.IsAttached = True Then
                MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), My.Application.Info.Title)
            End If
        End Try

        Return _bRet

    End Function

    'Private Function W_PRCbRemoveGridViewColumn(ByVal pGridView As DevExpress.XtraGrid.Views.Grid.GridView) As DevExpress.XtraGrid.Views.Grid.GridView
    '    Try
    '        With pGridView
    '            For nLoopColGridView As Integer = .Columns.Count - 1 To 0 Step -1
    '                Select Case .Columns.Item(nLoopColGridView).Name.ToString.ToUpper
    '                    Case "FTMatColorName".ToString.ToUpper, "FTMatColorCode".ToString.ToUpper
    '                        '...do nothing
    '                    Case "FNSeq".ToString.ToUpper, "FTSizeSpecDesc".ToString.ToUpper
    '                        '...do nothing
    '                        'Case "oColFNRowImport".ToString.ToUpper, "oColFTPONo".ToString.ToUpper, "oColFTPOTrading".ToString.ToUpper, "oColFTPOItem".ToUpper.ToUpper, "oColFNHSysMerTeamId".ToUpper.ToUpper & _
    '                        '     "oColFTMerTeamCode".ToString.ToUpper, "oColFTMerTeamDesc".ToUpper.ToUpper, "oColFTPOCreateDate".ToString.ToUpper, "oColFTOrderDate".ToString.ToUpper, "oColFNHSysPlantId".ToString.ToUpper & _
    '                        '     "oColFTPlantDesc".ToString.ToUpper, "oColFNHSysBuyGrpId".ToString.ToUpper, " oColFTBuyGrpCode".ToString.ToUpper, "oColFTBuyGrpDesc".ToString.ToUpper, "oColFTStyle".ToString.ToUpper & _
    '                        '     "oColFTStyleDesc".ToString.ToUpper, "oColFNHSysMainCategoryId".ToString.ToUpper, "oColFTMainCategoryCode".ToString.ToUpper, "oColFTMainCategoryDesc".ToString.ToUpper, "oColFTProdTypeDesc".ToString.ToUpper & _
    '                        '     "oColFTMaterial".ToString.ToUpper, "oColFTMaterialDesc".ToString.ToUpper, "oColFTPlanningSeason".ToString.ToUpper, "oColFTYear".ToString.ToUpper, " oColFNHSysBuyerId".ToString.ToUpper & _
    '                        '     "oColFTBuyerCode".ToString.ToUpper, "oColFTBuyerDesc".ToString.ToUpper, "oColFNHSysCountryId".ToString.ToUpper, "oColFNHSysCountryCode".ToString.ToUpper, "oColFTCountryDesc".ToString.ToUpper & _
    '                        '     "oColFNHSysGenderId".ToString.ToUpper, "oColFTGenderCode".ToString.ToUpper, "oColFTGenderDesc".ToString.ToUpper, "oColFDShipmentDate".ToString.ToUpper, "oColFDShipmentDateOriginal".ToString.ToUpper & _
    '                        '     "oColFNHSysMatColorId".ToString.ToUpper, "oColFTColorwayCode".ToString.ToUpper, "oColFTColorwayDesc".ToString.ToUpper, "oColFNHSysShipModeId".ToString.ToUpper, " oColFTShipModeDesc".ToString.ToUpper & _
    '                        '     "oColFNHSysUnitId".ToString.ToUpper, "oColFTUnitDesc".ToString.ToUpper
    '                        '    '...do nothing
    '                    Case "oColFNRowImport".ToString.ToUpper, "oColFTPONo".ToString.ToUpper, "oColFTPOTrading".ToString.ToUpper, "oColFTPOItem".ToString.ToUpper
    '                        '...do nothing
    '                    Case "oColFNHSysMerTeamId".ToString.ToUpper, "ColFTMerTeamCode".ToString.ToUpper, "oColFTMerTeamDesc".ToString.ToUpper, "oColFTPOCreateDate".ToString.ToUpper
    '                        '...do nothing
    '                    Case "oColFTOrderDate".ToString.ToUpper, "oColFNHSysPlantId".ToString.ToUpper, "oColFTPlantDesc".ToString.ToUpper, "oColFNHSysBuyGrpId".ToString.ToUpper
    '                        '...do nothing
    '                    Case "oColFTBuyGrpCode".ToString.ToUpper, "oColFTBuyGrpDesc".ToString.ToUpper, "oColFTStyle".ToString.ToUpper, "oColFTStyleDesc".ToString.ToUpper, "oColFNHSysMainCategoryId".ToString.ToUpper
    '                        '...do nothing
    '                    Case "oColFTMainCategoryCode".ToString.ToUpper, "oColFTMainCategoryDesc".ToString.ToUpper, "oColFTProdTypeDesc".ToString.ToUpper, "oColFTMaterial".ToString.ToUpper
    '                        '...do nothing
    '                    Case "oColFTMaterialDesc".ToString.ToUpper, "oColFTPlanningSeason".ToString.ToUpper, "oColFTYear".ToString.ToUpper, "oColFNHSysBuyerId".ToString.ToUpper
    '                        '...do nothing
    '                    Case "oColFTBuyerCode".ToString.ToUpper, "oColFTBuyerDesc".ToString.ToUpper, "oColFNHSysCountryId".ToString.ToUpper, "oColFNHSysCountryCode".ToString.ToUpper
    '                        '...do nothing
    '                    Case "oColFTCountryDesc".ToString.ToUpper, "oColFNHSysGenderId".ToString.ToUpper, "oColFTGenderCode".ToString.ToUpper, "oColFTGenderDesc".ToString.ToUpper
    '                        '...do nothing
    '                    Case "oColFDShipmentDate".ToString.ToUpper, "oColFDShipmentDateOriginal".ToString.ToUpper, "oColFNHSysMatColorId".ToString.ToUpper, "oColFTColorwayCode".ToString.ToUpper
    '                        '...do nothing
    '                    Case "oColFTColorwayDesc".ToUpper.ToUpper, "oColFNHSysShipModeId".ToString.ToUpper, "oColFTShipModeDesc".ToString.ToUpper, "oColFNHSysUnitId".ToString.ToUpper, "oColFTUnitDesc".ToString.ToUpper
    '                        '...do noghing
    '                        'MsgBox("Grid View Name : " & pGridView.Name & Environment.NewLine & "Column Name : " & CStr(pGridView.Columns.Item(nLoopColGridView).Name.ToString.ToUpper), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
    '                    Case Else
    '                        .Columns.Remove(.Columns.Item(nLoopColGridView))
    '                End Select

    '            Next

    '        End With

    '    Catch ex As Exception
    '    End Try

    '    Return pGridView

    'End Function

    Private Sub W_PRCxInitialGridBandView(oGridBandedView As DevExpress.XtraGrid.Views.Grid.GridView)
        Dim bRet As Boolean = _bInitialGridBandView

        Try

            REM 2014/06/24
            'oGridBandedView.ClearGrouping()
            oGridBandedView.ClearDocument()

            'If Not (_bInitialGridBandView) Then
            '    oGridBandedView.ClearGrouping()
            '    oGridBandedView.ClearDocument()
            'End If

            REM oGridBandedView.Columns("FTMerTeamCode").SortIndex = 0
            REM oGridBandedView.Columns("FTMerTeamCode").SortOrder = DevExpress.Data.ColumnSortOrder.Ascending
            REM oGridBandedView.Columns("FTPONo").SortIndex = 1
            REM oGridBandedView.Columns("FTPONo").SortOrder = DevExpress.Data.ColumnSortOrder.Ascending

            ' Make the group footers always visible.
            oGridBandedView.GroupFooterShowMode = GroupFooterShowMode.VisibleAlways

            oGridBandedView.Columns("FTMerTeamCode").Group()
            oGridBandedView.Columns("FTMerTeamCode").SortIndex = 0
            oGridBandedView.Columns("FTMerTeamCode").SortOrder = DevExpress.Data.ColumnSortOrder.Ascending
            REM oGridBandedView.Columns("FTMerTeamCode").Caption = "Merchandiser Team"
            oGridBandedView.Columns("FTPONo").Group()
            oGridBandedView.Columns("FTPONo").SortIndex = 1
            oGridBandedView.Columns("FTPONo").SortOrder = DevExpress.Data.ColumnSortOrder.Ascending
            REM oGridBandedView.Columns("FTPONo").Caption = "PO No."


            Dim oItemSummary As GridGroupSummaryItem = New GridGroupSummaryItem
            oItemSummary.FieldName = "FNQuantity"
            oItemSummary.SummaryType = DevExpress.Data.SummaryItemType.Sum
            oItemSummary.DisplayFormat = "{0:n0}"
            oItemSummary.ShowInGroupColumnFooter = oGridBandedView.Columns("FNQuantity")
            oGridBandedView.GroupSummary.Add(oItemSummary)

            Try
                Dim oItemSummary2 As GridGroupSummaryItem = New GridGroupSummaryItem
                oItemSummary2.FieldName = "FNGrandQuantity"
                oItemSummary2.SummaryType = DevExpress.Data.SummaryItemType.Sum
                oItemSummary2.DisplayFormat = "{0:n0}"
                oItemSummary2.ShowInGroupColumnFooter = oGridBandedView.Columns("FNGrandQuantity")
                oGridBandedView.GroupSummary.Add(oItemSummary2)
            Catch ex As Exception

            End Try
           

            'If Not oDBdtMatSize Is Nothing AndAlso oDBdtMatSize.Rows.Count > 0 Then
            '    Dim nLoopMatSize As Integer

            '    For nLoopMatSize = 0 To oDBdtMatSize.Rows.Count - 1
            '        Dim oItemSummary As GridGroupSummaryItem = New GridGroupSummaryItem
            '        oItemSummary.FieldName = "FNQuantity" & oDBdtMatSize.Rows(nLoopMatSize).Item("FTMatSizeCode").ToString()
            '        oItemSummary.SummaryType = DevExpress.Data.SummaryItemType.Sum
            '        oItemSummary.DisplayFormat = "{0:n0}"
            '        oItemSummary.ShowInGroupColumnFooter = oGridBandedView.Columns("FNQuantity" & oDBdtMatSize.Rows(nLoopMatSize).Item("FTMatSizeCode").ToString())
            '        oGridBandedView.GroupSummary.Add(oItemSummary)

            '    Next nLoopMatSize

            'End If

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

            Me.ogdConfirmImport.DataSource = Nothing
            Me.ogdConfirmImport.Refresh()

            ogdOrderchange.DataSource = Nothing
            ogdOrderchange.Refresh()

            ogdOrderRemove.DataSource = Nothing
            ogdOrderRemove.Refresh()

            'Call W_PRCbRemoveGridViewColumn(Me.ogvConfirmImport)
            'Call W_PRCxInitialGridBandView()

            _bCatchSrcFile = False

            HI.TL.HandlerControl.ClearControl(Me)


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

    Private Function W_PRCbAddNewStyleImport() As Boolean
        Dim _bRet As Boolean = False
        Try
            _bRet = True
            If Not oDBdtStyle Is Nothing And oDBdtStyle.Rows.Count > 0 Then
                Dim nFNHSysStyleId As Integer
                Dim nFNHSysCustId As Integer
                Dim tStyleImport As String
                Dim tStyleImportDesc As String

                nFNHSysCustId = 0

         

                Dim oStrBuilder As New System.Text.StringBuilder()

                Dim nLoopStyle As Integer

                For nLoopStyle = 0 To oDBdtStyle.Rows.Count - 1
                    oStrBuilder.Remove(0, oStrBuilder.Length)

                    tStyleImport = ""
                    tStyleImport = oDBdtStyle.Rows(nLoopStyle).Item("FTStyleCode")

                    tStyleImportDesc = ""
                    tStyleImportDesc = oDBdtStyle.Rows(nLoopStyle).Item("FTStyleName")

                    tSql = "SELECT TOP 1 A.FTStyleCode FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMStyle] AS A WITH(NOLOCK) WHERE A.FTStyleCode = N'" & HI.UL.ULF.rpQuoted(tStyleImport) & "'"

                    If HI.Conn.SQLConn.GetField(tSql, HI.Conn.DB.DataBaseName.DB_MASTER, "") = "" Then
                        nFNHSysStyleId = Val(HI.TL.RunID.GetRunNoID("TMERMStyle", "FNHSysStyleId", Conn.DB.DataBaseName.DB_MASTER).ToString())

                        oStrBuilder.AppendLine("INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMStyle]([FTInsUser]")
                        oStrBuilder.AppendLine("											  ,[FDInsDate]")
                        oStrBuilder.AppendLine("											  ,[FTInsTime]")
                        oStrBuilder.AppendLine("											  ,[FTUpdUser]")
                        oStrBuilder.AppendLine("											  ,[FDUpdDate]")
                        oStrBuilder.AppendLine("											  ,[FTUpdTime]")
                        oStrBuilder.AppendLine("											  ,[FNHSysStyleId]")
                        oStrBuilder.AppendLine("											  ,[FTStyleCode]")
                        oStrBuilder.AppendLine("											  ,[FTStyleNameTH]")
                        oStrBuilder.AppendLine("											  ,[FTStyleNameEN]")
                        oStrBuilder.AppendLine("											  ,[FTRemark]")
                        oStrBuilder.AppendLine("											  ,[FTStateActive],[FNCMDisPer]")
                        oStrBuilder.AppendLine("											  ,[FNHSysCustId])")
                        oStrBuilder.AppendLine("VALUES(NULL")
                        oStrBuilder.AppendLine("      ,CONVERT(VARCHAR(10),GETDATE(),111)")
                        oStrBuilder.AppendLine("      ,CONVERT(VARCHAR(8),GETDATE(),114)")
                        oStrBuilder.AppendLine("      ,NULL")
                        oStrBuilder.AppendLine("      ,NULL")
                        oStrBuilder.AppendLine("      ,NULL")
                        oStrBuilder.AppendLine(String.Format("      ,{0}", nFNHSysStyleId))
                        oStrBuilder.AppendLine("      ,N'" & HI.UL.ULF.rpQuoted(tStyleImport) & "'")
                        oStrBuilder.AppendLine("      ,N'" & HI.UL.ULF.rpQuoted(tStyleImportDesc) & "'")
                        oStrBuilder.AppendLine("      ,N'" & HI.UL.ULF.rpQuoted(tStyleImportDesc) & "'")
                        oStrBuilder.AppendLine("      ,''")
                        oStrBuilder.AppendLine("      ,'1',2.0")
                        REM 2014/05/24 oStrBuilder.AppendLine("      ,COALESCE((SELECT TOP 1 A.FNHSysCustId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCNMCustomer] AS A WITH(NOLOCK) WHERE A.FTCustCode = N'" & HI.UL.ULF.rpQuoted(tCustCodeStyle) & "'), NULL))")
                        oStrBuilder.AppendLine(String.Format("       ,{0})", nFNHSysCustId))

                        tSql = ""
                        tSql = oStrBuilder.ToString()

                        If HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MASTER) Then
                            'MsgBox("Execute data complete...", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
                        End If

                    End If

                Next nLoopStyle

            Else
                '...do nothing
            End If

        Catch ex As Exception
            If System.Diagnostics.Debugger.IsAttached = True Then
                Throw New Exception(ex.Message().ToString() & ex.StackTrace.ToString())
            End If
            _bRet = False
        End Try

        Return _bRet

    End Function

    Private Function W_PRCbValidateMatchPlant(ByVal oDBdt As System.Data.DataTable) As Boolean
        Dim bRet As Boolean = False
        Try
            If Not oDBdt Is Nothing And oDBdt.Rows.Count > 0 And oDBdt.Rows.Count > _nRowHeader Then
                Dim oDBdtSrc As System.Data.DataTable

                oDBdtSrc = oDBdt.Clone()

                Dim nLoop As Integer
                Dim nStartRow As Integer = _nRowHeader

                Try
                    'For nLoop = _nRowHeader To oDBdt.Rows.Count - 1
                    '    'Application.DoEvents()
                    '    oDBdtSrc.ImportRow(oDBdt.Rows(nLoop))
                    'Next nLoop
                    For nLoop = _nRowHeader To oDBdt.Rows.Count - 1
                        'Application.DoEvents()
                        oDBdtSrc.ImportRow(oDBdt.Rows(nLoop))
                    Next nLoop

                    oDBdtSrc.AcceptChanges()

                Catch ex As Exception
                    'MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
                End Try

                Dim tColPlant As String = "F31"

                Dim tPlant As List(Of String) = oDBdtSrc.AsEnumerable() _
                                     .Select(Function(r) r.Field(Of String)(tColPlant)) _
                                     .Distinct() _
                                     .ToList()

                If Not tPlant Is Nothing And tPlant.Count > 0 Then
                    Dim oDBdtPlant As System.Data.DataTable

                    tSql = ""
                    tSql = "SELECT A.FTPlantCode"
                    tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TMERMPlant AS A WITH(NOLOCK)"
                    tSql &= Environment.NewLine & "ORDER BY A.FNHSysPlantId ASC;"

                    oDBdtPlant = HI.Conn.SQLConn.GetDataTable(tSql, HI.Conn.DB.DataBaseName.DB_MASTER)

                    If Not oDBdtPlant Is Nothing And oDBdtPlant.Rows.Count > 0 Then
                        Dim bFoundPlant As Boolean = True

                        For Each s As String In tPlant
                            If Not s Is Nothing Then
                                If s.Trim() <> "" Then
                                    Dim oDataRow As DataRow()
                                    Try
                                        oDataRow = oDBdtPlant.Select("FTPlantCode = '" & HI.UL.ULF.rpQuoted(s.ToString().Trim()) & "'")
                                        If oDataRow.Length > 0 Then
                                            '...valid team buy group
                                            If oDataRow(0)("FTPlantCode") <> "" Then
                                                'bFoundBuyGrp = True
                                            Else
                                                '...do nothing
                                            End If
                                        Else

                                            Select Case HI.ST.Lang.Language

                                                Case HI.ST.Lang.eLang.TH
                                                    MsgBox("ไม่พบรายการข้อมูลโรงงานลูกค้าในระบบข้อมูลหลัก" & Environment.NewLine & "รายการรหัสโรงงานลูกค้า : " & s.ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)

                                                Case Else
                                                    MsgBox("Plant code not exists in system master plant " & Environment.NewLine & "Plant code is : " & s.ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
                                            End Select

                                            bFoundPlant = False

                                            Exit For

                                        End If

                                    Catch ex As Exception
                                    End Try

                                Else
                                    MsgBox("Source file row item not yet specify plant code (plant code is ) :" & s.ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
                                    bFoundPlant = False
                                    Exit For
                                End If

                            Else

                                Select Case HI.ST.Lang.Language

                                    Case HI.ST.Lang.eLang.TH
                                        MsgBox("ไม่พบรายการข้อมูลโรงงานลูกค้า !!!", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)

                                    Case Else
                                        MsgBox("Invalid data plant !!!", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
                                End Select

                                bFoundPlant = False

                                Exit For

                            End If

                        Next

                        If bFoundPlant = True Then
                            bRet = True
                        End If

                    End If

                End If

            Else
                '...do nothing
            End If

        Catch ex As Exception
            If System.Diagnostics.Debugger.IsAttached = True Then
                MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
            End If
        End Try

        Return bRet

    End Function

    Private Function W_PRCbValidateMatchCurrency(ByVal oDBdt As System.Data.DataTable) As Boolean
        Dim bRet As Boolean = True

        If _tCurtImport <= 0 Then
            Return True
        End If

        Dim _Qry As String = ""
        Dim _CheckCur As String = ""
        Dim _dtCur As New DataTable
        _dtCur.Columns.Add("FTCurrency", GetType(String))
        Try
            For Each R As DataRow In oDBdt.Select("F" & _tCurtImport.ToString & "<>''")

                If _dtCur.Select("FTCurrency='" & HI.UL.ULF.rpQuoted(R.Item("F" & _tCurtImport.ToString).ToString) & "'").Length <= 0 Then
                    _dtCur.BeginInit()
                    _dtCur.Rows.Add(R.Item("F" & _tCurtImport.ToString).ToString)
                    _dtCur.EndInit()
                End If
            Next

            If _dtCur.Rows.Count > 0 Then
                For Each R As DataRow In _dtCur.Rows
                    _Qry = "SELECT TOP 1  FTCurCode"
                    _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TFINMCurrency AS A WITH(NOLOCK)"
                    _Qry &= vbCrLf & "WHERE FTCurCode='" & HI.UL.ULF.rpQuoted(R!FTCurrency.ToString) & "'"

                    If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MASTER, "") = "" Then
                        If _CheckCur = "" Then
                            _CheckCur = R!FTCurrency.ToString
                        Else
                            _CheckCur = _CheckCur & "," & R!FTCurrency.ToString
                        End If
                    End If

                Next
            End If

            If _CheckCur <> "" Then
                HI.MG.ShowMsg.mInfo("ข้อมูลสกุลเงิน ไม่ถูกต้อง กรุณาทำการตรวจสอบ !!!", 1504020085, Me.Text, _CheckCur, MessageBoxIcon.Warning)
                bRet = False
            End If
        Catch ex As Exception

        End Try
        Return bRet

    End Function

    Private Function W_PRCbValidateMatchBuyGroup(ByVal oDBdt As System.Data.DataTable) As Boolean
        Dim bRet As Boolean = False
        Try
            If Not oDBdt Is Nothing And oDBdt.Rows.Count > 0 And oDBdt.Rows.Count > _nRowHeader Then
                Dim oDBdtSrc As New System.Data.DataTable

                oDBdtSrc = oDBdt.Clone
                Dim nLoop As Integer
                Dim nStartRow As Integer = _nRowHeader

                Try
                    REM 2014/06/30
                    'For nLoop = _nRowHeader To oDBdt.Rows.Count - 1
                    '    Application.DoEvents()
                    '    oDBdtSrc.ImportRow(oDBdt.Rows(nLoop))
                    'Next nLoop

                    For nLoop = _nRowHeader To oDBdt.Rows.Count - 1
                        Application.DoEvents()
                        oDBdtSrc.ImportRow(oDBdt.Rows(nLoop))
                    Next nLoop

                    oDBdtSrc.AcceptChanges()

                Catch ex As Exception
                    'MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
                End Try


                Dim tColBuyGrp As String = "F43"

                Dim tTexValidateBuyGrp As String


                tTexValidateBuyGrp = ""

                For Each oDataRowColBuyGrp As System.Data.DataRow In oDBdtSrc.Rows
                    For Each oColBuyGrp As System.Data.DataColumn In oDBdtSrc.Columns
                        Select Case oColBuyGrp.ColumnName.ToString.ToUpper
                            Case tColBuyGrp.ToUpper
                                tTexValidateBuyGrp = tTexValidateBuyGrp & oDataRowColBuyGrp.Item(oColBuyGrp.ColumnName.ToString)
                            Case Else
                                '...Nothing 
                        End Select
                    Next

                Next

                If tTexValidateBuyGrp <> "" Then
                    Dim tBuyGrp As List(Of String) = oDBdtSrc.AsEnumerable() _
                                     .Select(Function(r) r.Field(Of String)(tColBuyGrp)) _
                                     .Distinct() _
                                     .ToList()

                    If Not tBuyGrp Is Nothing And tBuyGrp.Count > 0 Then
                        Dim oDBdtBuyGrp As System.Data.DataTable

                        tSql = ""
                        tSql = "SELECT A.FTBuyGrpCode"
                        tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMBuyGrp] AS A WITH(NOLOCK)"
                        tSql &= Environment.NewLine & "ORDER BY A.FNHSysBuyGrpId ASC;"

                        oDBdtBuyGrp = HI.Conn.SQLConn.GetDataTable(tSql, HI.Conn.DB.DataBaseName.DB_MASTER)

                        If Not oDBdtBuyGrp Is Nothing And oDBdtBuyGrp.Rows.Count > 0 Then
                            Dim bFoundBuyGrp As Boolean = True

                            For Each s As String In tBuyGrp

                                If Not s Is Nothing Then

                                    If s.Trim() <> "" Then

                                        Dim oDataRow As DataRow()
                                        Try
                                            oDataRow = oDBdtBuyGrp.Select("FTBuyGrpCode = '" & HI.UL.ULF.rpQuoted(s.ToString().Trim()) & "'")
                                            If oDataRow.Length > 0 Then
                                                '...valid team buy group
                                                If oDataRow(0)("FTBuyGrpCode") <> "" Then
                                                    'bFoundBuyGrp = True
                                                Else
                                                    '...do nothing
                                                End If
                                            Else

                                                Select Case HI.ST.Lang.Language

                                                    Case HI.ST.Lang.eLang.TH
                                                        MsgBox("ไม่พบรายการข้อมูลกลุ่มการซื้อในระบบข้อมูลหลัก" & Environment.NewLine & "รายการรหัสกลุ่มการซื้อ : " & s.ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
                                                    Case Else
                                                        MsgBox("Buy group code not exists in system master buy group " & Environment.NewLine & "Buy group code is : " & s.ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
                                                End Select

                                                bFoundBuyGrp = False

                                                Exit For

                                            End If

                                        Catch ex As Exception
                                        End Try

                                    Else
                                        MsgBox("Source file row item not yet specify buy group code (buy group code is ) :" & s.ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
                                        bFoundBuyGrp = False
                                        Exit For
                                    End If

                                Else

                                    Select Case HI.ST.Lang.Language

                                        Case HI.ST.Lang.eLang.TH
                                            MsgBox("ไม่พบรายการข้อมูลกลุ่มการซื้อ !!!", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)


                                        Case Else
                                            MsgBox("Invalid data buy group !!!", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
                                    End Select

                                    bFoundBuyGrp = False

                                    Exit For

                                End If

                            Next

                            If bFoundBuyGrp = True Then
                                bRet = True
                            End If

                        End If

                    Else
                        MsgBox("รายการคอลัมน์นี้กำหนดให้เป็นข้อมูล Buy Group" & Environment.NewLine & "ซึ่งท่านจะต้องทำการกำหนดข้อมูล Buy Group !!!", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, My.Application.Info.Title)
                    End If

                Else
                    MsgBox("รายการคอลัมน์นี้กำหนดให้เป็นข้อมูล Buy Group" & Environment.NewLine & "ซึ่งท่านจะต้องทำการกำหนดข้อมูลรายการ Buy Group !!!", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, My.Application.Info.Title)
                End If

            Else
                '...do nothing
            End If

        Catch ex As Exception
            If System.Diagnostics.Debugger.IsAttached = True Then
                MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace.ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
            End If
        End Try

        Return bRet

    End Function

    Private Function W_PRCbValidateMatchMerchanTeam(ByVal oDBdt As System.Data.DataTable) As Boolean
        Dim bRet As Boolean = False
        Try
            If Not oDBdt Is Nothing And oDBdt.Rows.Count > 0 And oDBdt.Rows.Count > _nRowHeader Then

                Dim oDBdtSrc As System.Data.DataTable

                oDBdtSrc = oDBdt.Clone()

                Dim nLoop As Integer
                Dim nStartRow As Integer = _nRowHeader

                Try
                    REM 2014/06/30
                    'For nLoop = _nRowHeader To oDBdt.Rows.Count - 1
                    '    'Application.DoEvents()
                    '    oDBdtSrc.ImportRow(oDBdt.Rows(nLoop))
                    'Next nLoop

                    For nLoop = _nRowHeader To oDBdt.Rows.Count - 1
                        'Application.DoEvents()
                        oDBdtSrc.ImportRow(oDBdt.Rows(nLoop))
                    Next nLoop

                    oDBdtSrc.AcceptChanges()

                Catch ex As Exception
                    'MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
                End Try

                Dim tColMerchanTeam As String = "F14"

                'Dim tmpDTColMerchanTeam As New System.Data.DataTable

                'Dim oColMerChanTeamValidate As System.Data.DataColumn
                'oColMerChanTeamValidate = New System.Data.DataColumn(tColMerchanTeam.ToString, System.Type.GetType("System.String"))
                'oColMerChanTeamValidate.Caption = tColMerchanTeam.ToString

                'tmpDTColMerchanTeam.Columns.Add(oColMerChanTeamValidate.ColumnName, oColMerChanTeamValidate.DataType)

                Dim tTexValidateMerchandiserTeam As String


                tTexValidateMerchandiserTeam = ""

                For Each oDataRowColMerchanTeam As System.Data.DataRow In oDBdtSrc.Rows
                    For Each oColMerchanTeamXX As System.Data.DataColumn In oDBdtSrc.Columns
                        Select Case oColMerchanTeamXX.ColumnName.ToString.ToUpper
                            Case tColMerchanTeam.ToUpper
                                'Dim oNewRowMerchanTeam As System.Data.DataRow

                                'oNewRowMerchanTeam = tmpDTColMerchanTeam.NewRow()

                                'oNewRowMerchanTeam.Item(oColMerChanTeamValidate.ColumnName.ToString) = oDataRowColMerchanTeam!tColMerchanTeam.ToString

                                'tmpDTColMerchanTeam.Rows.Add(oNewRowMerchanTeam)

                                'tTexValidateMerchandiserTeam = tTexValidateMerchandiserTeam & oDataRowColMerchanTeam!oColMerchanTeamXX.ColumnName.ToString

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
                                                REM MsgBox("Merchandiser Team code not exists in system master merchandiser team" & Environment.NewLine & "Merchandiser Team code is : " & s.ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)

                                                'Select Case HI.ST.Lang.Language

                                                '    Case HI.ST.Lang.eLang.TH
                                                '        MsgBox("ไม่พบข้อมูลทีมเมอร์แชนไดเซอร์ในระบบข้อมูลหลัก" & Environment.NewLine & "รายการรหัสทีม : " & s.ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
                                                '    Case Else
                                                '        MsgBox("Merchandiser Team code not exists in system master merchandiser team" & Environment.NewLine & "Merchandiser Team code is : " & s.ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
                                                'End Select

                                                Select Case HI.ST.Lang.Language
                                                    Case HI.ST.Lang.eLang.TH
                                                        MsgBox("ไม่พบข้อมูลทีมเมอร์แชนไดเซอร์ในระบบข้อมูลหลัก" & Environment.NewLine & "รายการรหัสทีม : " & s.ToString() & Environment.NewLine & "หรือยังไม่มีการกำหนดข้อมูลหลักรายการทีมเมอร์แชนไดเซอร์ !!!" & Environment.NewLine & "กรุณาทำการตรวจสอบอีกครั้ง", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
                                                        'MsgBox("การกำหนดข้อมูลทีมเมอร์แชนไดเซอร์บางรายการของไฟล์ต้นฉบับไม่ถูกต้อง " & Environment.NewLine & "หรือยังไม่มีการกำหนดข้อมูลหลักรายการทีมเมอร์แชนไดเซอร์ !!!" & Environment.NewLine & "กรุณาทำการตรวจสอบอีกครั้ง", MsgBoxStyle.OkOnly + MsgBoxStyle.Exclamation, My.Application.Info.Title)
                                                    Case Else
                                                        MsgBox("Invalid Merchandiser Team " & Environment.NewLine & "Or Not provide for merchandiser team master file !!!" & Environment.NewLine & "Merchandiser Team : " & s.ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Exclamation, My.Application.Info.Title)
                                                End Select

                                                bFoundMerTeam = False

                                                Exit For

                                            End If

                                        Catch ex As Exception
                                        End Try

                                    Else
                                        REM MsgBox("Source file row item not yet specify merchandiser team code (merchandiser team code is ) :" & s.ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)

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
                                    REM MsgBox("Invalid data merchandiser team code !!!", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)

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
            If System.Diagnostics.Debugger.IsAttached = True Then
                MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
            End If
        End Try

        Return bRet

    End Function

    Private Sub W_PRCxValidateMaster(ByVal oDBdt As System.Data.DataTable)
        Call W_PRCbValidateExistsMasterSeason(oDBdt)
        Call W_PRCbValidateExistsMasterStyle(oDBdt)
        Call W_PRCbValidateExistsMatchColor(oDBdt)
        Call W_PRCbValidateExistsMasterGender(oDBdt)
        Call W_PRCbValidateExistsMasterBuyer(oDBdt)
        Call W_PRCbValidateExistsMasterShipMode(oDBdt)
        Call W_PRCbValidateExistsMasterMerchanUnit(oDBdt)
        Call W_PRCbValidateExistsMasterCountry(oDBdt)   '...validate country/continent

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

        Call W_PRCbValidateExistsMatchSize(oDBdt, tmpDTNotMapSize)

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
                                    If System.Diagnostics.Debugger.IsAttached = True Then
                                        System.Diagnostics.Debug.Write("Append new size code to system data base TMERMMatSize [HITECH_MASTER] : " & oDataRowNotMapSize!FTSizeCodeNotExists.ToString & " " & Environment.NewLine)
                                    End If

                                End If

                            Next

                        End If

                    End If

                End With

                _wMapSizeImportOrder.TopMost = False
                _wMapSizeImportOrder.Refresh()

            Catch ex As Exception
                If System.Diagnostics.Debugger.IsAttached = True Then
                    MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString, MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title.ToString)
                End If
            End Try

            _oSplash.TopMost = True
            _oSplash.Refresh()

        End If

        If Not tmpDTNotMapSize Is Nothing Then tmpDTNotMapSize.Dispose()
        '========================================================================================================

        Call W_PRCbValidateExistsMasterProductType(oDBdt)

    End Sub

    Private Function W_PRCbValidateExistsMasterProductType(ByVal oDBdt As System.Data.DataTable) As Boolean
        Dim bRet As Boolean = False
        Try
            If Not oDBdt Is Nothing And oDBdt.Rows.Count > 0 And oDBdt.Rows.Count > _nRowHeader Then
                Dim tColCategoryCode As String = "F10"
                Dim tColCategoryExtend As String = "F11"
                Dim tColSubCategoryExtend As String = "F12"

                Dim oStrBuilder As New System.Text.StringBuilder()

                Dim nLoopCategory As Integer

                For nLoopCategory = _nStartRowImportExcel To oDBdt.Rows.Count - 1

                    '_oSplash.UpdateInformation(_tSplashText & Environment.NewLine & "กำลังดำเนินการตรวจสอบข้อมูลประเภทผลิตภัณฑ์...")

                    Dim tCategoryCode As String = ""
                    Dim tCategoryExtend As String = ""
                    Dim tSubCategoryExtend As String = ""

                    If Not DBNull.Value.Equals(oDBdt.Rows(nLoopCategory).Item(tColCategoryCode)) Then
                        tCategoryCode = oDBdt.Rows(nLoopCategory).Item(tColCategoryCode)

                        If tCategoryCode <> "" AndAlso Not DBNull.Value.Equals(oDBdt.Rows(nLoopCategory).Item(tColCategoryExtend)) Then
                            tCategoryExtend = oDBdt.Rows(nLoopCategory).Item(tColCategoryExtend)

                            If tCategoryExtend <> "" AndAlso Not DBNull.Value.Equals(oDBdt.Rows(nLoopCategory).Item(tColSubCategoryExtend)) Then
                                tSubCategoryExtend = oDBdt.Rows(nLoopCategory).Item(tColSubCategoryExtend)

                                If tSubCategoryExtend <> "" Then
                                    '...validate product type exists in system merchan (Category)

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
                                    oStrBuilder.AppendLine("WHERE (A.FTProdTypeNameTH = N'" & HI.UL.ULF.rpQuoted(tProductTypeDesc) & "' OR A.FTProdTypeNameEN = N'" & HI.UL.ULF.rpQuoted(tProductTypeDesc) & "');")

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
                                            oStrBuilder.AppendLine(String.Format("       ({0}", "NULL"))
                                            oStrBuilder.AppendLine(String.Format("       ,{0}", "NULL"))
                                            oStrBuilder.AppendLine(String.Format("       ,{0}", "NULL"))
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

                    'Application.DoEvents()

                Next nLoopCategory

                bRet = True

            End If

        Catch ex As Exception
            ' If System.Diagnostics.Debugger.IsAttached = True Then
            MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
            'End If
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
            'If System.Diagnostics.Debugger.IsAttached = True Then
            MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
            ' End If
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

        tmpDTMapSizeMaster.Dispose()

        Return oDBdtMapSize

    End Function

    Private Function W_PRCbValidateExistsMasterSeason(ByVal oDBdt As System.Data.DataTable) As Boolean
        Dim bRet As Boolean = False
        Try
            If Not oDBdt Is Nothing And oDBdt.Rows.Count > 0 And oDBdt.Rows.Count > _nRowHeader Then
                Dim tColPlanningSeason As String = "F25"
                Dim tColYear As String = "F26"
                Dim nLoopSeason As Integer
                'For nLoopSeason = _nStartRowImportExcel To oDBdt.Rows.Count - 1
                For nLoopSeason = _nStartRowImportExcel To oDBdt.Rows.Count - 1

                    If Not DBNull.Value.Equals(oDBdt.Rows(nLoopSeason).Item(tColPlanningSeason)) Then
                        If oDBdt.Rows(nLoopSeason).Item(tColPlanningSeason).ToString() <> "" Then
                            Dim tSeason As String, tYear As String
                            tSeason = ""
                            tYear = ""

                            If Not DBNull.Value.Equals(oDBdt.Rows(nLoopSeason).Item(tColYear)) Then
                                If oDBdt.Rows(nLoopSeason).Item(tColYear).ToString() <> "" Then
                                    tYear = Microsoft.VisualBasic.Mid$(oDBdt.Rows(nLoopSeason).Item(tColYear).ToString(), 3, 4)
                                End If
                            End If

                            tSeason = oDBdt.Rows(nLoopSeason).Item(tColPlanningSeason).ToString() & tYear

                            tSql = ""
                            tSql = "SELECT TOP 1 A.FTSeasonCode FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMSeason] AS A WITH(NOLOCK) WHERE A.FTSeasonCode = N'" & HI.UL.ULF.rpQuoted(tSeason) & "';"

                            If HI.Conn.SQLConn.GetField(tSql, HI.Conn.DB.DataBaseName.DB_MASTER, "") = "" Then
                                Dim nFNHSysSeasonId As Integer

                                nFNHSysSeasonId = Val(HI.TL.RunID.GetRunNoID("TMERMSeason", "FNHSysSeasonId", HI.Conn.DB.DataBaseName.DB_MASTER).ToString())

                                Dim oStrBuilder As New System.Text.StringBuilder()

                                oStrBuilder.Remove(0, oStrBuilder.Length)

                                oStrBuilder.AppendLine("INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMSeason]([FTInsUser]")
                                oStrBuilder.AppendLine("											   ,[FDInsDate]")
                                oStrBuilder.AppendLine("											   ,[FTInsTime]")
                                oStrBuilder.AppendLine("											   ,[FTUpdUser]")
                                oStrBuilder.AppendLine("											   ,[FDUpdDate]")
                                oStrBuilder.AppendLine("											   ,[FTUpdTime]")
                                oStrBuilder.AppendLine("											   ,[FNHSysSeasonId]")
                                oStrBuilder.AppendLine("											   ,[FTSeasonCode]")
                                oStrBuilder.AppendLine("											   ,[FTSeasonNameTH]")
                                oStrBuilder.AppendLine("											   ,[FTSeasonNameEN]")
                                oStrBuilder.AppendLine("											   ,[FTRemark]")
                                oStrBuilder.AppendLine("											   ,[FTStateActive])")
                                oStrBuilder.AppendLine("VALUES(NULL")
                                oStrBuilder.AppendLine("      ,NULL")
                                oStrBuilder.AppendLine("      ,NULL")
                                oStrBuilder.AppendLine("      ,NULL")
                                oStrBuilder.AppendLine("      ,NULL")
                                oStrBuilder.AppendLine("      ,NULL")
                                oStrBuilder.AppendLine(String.Format("      ,{0}", nFNHSysSeasonId))
                                oStrBuilder.AppendLine("      ,N'" & HI.UL.ULF.rpQuoted(tSeason) & "'")
                                oStrBuilder.AppendLine("      ,N'" & HI.UL.ULF.rpQuoted(tSeason) & "'")
                                oStrBuilder.AppendLine("      ,N'" & HI.UL.ULF.rpQuoted(tSeason) & "'")
                                oStrBuilder.AppendLine("      ,''")
                                oStrBuilder.AppendLine("      ,'1')")

                                tSql = ""
                                tSql = oStrBuilder.ToString()

                                If HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MASTER) Then
                                    'MsgBox("Execute data complete...", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
                                End If

                            End If

                        End If

                    End If

                    'Application.DoEvents()

                Next nLoopSeason

                bRet = True
            End If

        Catch ex As Exception
            'If System.Diagnostics.Debugger.IsAttached = True Then
            MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
            ' End If
        End Try

        Return bRet

    End Function

    Private Function W_PRCbValidateExistsMasterBuyGroup(ByVal oDBdt As System.Data.DataTable) As Boolean
        Dim bRet As Boolean
        Try
            If Not oDBdt Is Nothing And oDBdt.Rows.Count > 0 And oDBdt.Rows.Count > _nRowHeader Then
                Dim tColBuyGrp As String = "F43"
                Dim tColBuyGrpDesc As String = "F44"

                Dim nLoopBuyGrp As Integer

                For nLoopBuyGrp = _nStartRowImportExcel To oDBdt.Rows.Count - 1

                    If Not DBNull.Value.Equals(oDBdt.Rows(nLoopBuyGrp).Item(tColBuyGrp)) Then
                        If oDBdt.Rows(nLoopBuyGrp).Item(tColBuyGrp).ToString() <> "" Then
                            Dim tBuyGrpCode As String
                            tBuyGrpCode = ""

                            tBuyGrpCode = oDBdt.Rows(nLoopBuyGrp).Item(tColBuyGrp).ToString()

                            If Not DBNull.Value.Equals(oDBdt.Rows(nLoopBuyGrp).Item(tColBuyGrpDesc)) Then
                                If oDBdt.Rows(nLoopBuyGrp).Item(tColBuyGrpDesc).ToString() <> "" Then
                                    Dim tBuyGrpDesc As String
                                    tBuyGrpDesc = ""

                                    If Not DBNull.Value.Equals(oDBdt.Rows(nLoopBuyGrp).Item(tColBuyGrpDesc)) Then
                                        tBuyGrpDesc = oDBdt.Rows(nLoopBuyGrp).Item(tColBuyGrpDesc).ToString().Trim()
                                    End If

                                    tSql = ""
                                    tSql = "SELECT TOP 1 A.FTBuyGrpCode FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMBuyGrp] AS A WITH(NOLOCK) WHERE A.FTBuyGrpCode = N'" & HI.UL.ULF.rpQuoted(tBuyGrpCode) & "';"

                                    If HI.Conn.SQLConn.GetField(tSql, HI.Conn.DB.DataBaseName.DB_MASTER, "") = "" Then
                                        Dim nFNHSysBuyGrpId As Integer

                                        nFNHSysBuyGrpId = Val(HI.TL.RunID.GetRunNoID("TMERMBuyGrp", "FNHSysBuyGrpId", Conn.DB.DataBaseName.DB_MASTER).ToString())

                                        Dim oStrBuilder As New System.Text.StringBuilder()

                                        oStrBuilder.Remove(0, oStrBuilder.Length)

                                        oStrBuilder.AppendLine("INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMBuyGrp]([FTInsUser]")
                                        oStrBuilder.AppendLine("											   ,[FDInsDate]")
                                        oStrBuilder.AppendLine("											   ,[FTInsTime]")
                                        oStrBuilder.AppendLine("											   ,[FTUpdUser]")
                                        oStrBuilder.AppendLine("											   ,[FDUpdDate]")
                                        oStrBuilder.AppendLine("											   ,[FTUpdTime]")
                                        oStrBuilder.AppendLine("											   ,[FNHSysBuyGrpId]")
                                        oStrBuilder.AppendLine("											   ,[FTBuyGrpCode]")
                                        oStrBuilder.AppendLine("											   ,[FTBuyGrpNameTH]")
                                        oStrBuilder.AppendLine("											   ,[FTBuyGrpNameEN]")
                                        oStrBuilder.AppendLine("											   ,[FNQtySpecialType]")
                                        oStrBuilder.AppendLine("											   ,[FNQtySpecial]")
                                        oStrBuilder.AppendLine("											   ,[FTRemark]")
                                        oStrBuilder.AppendLine("											   ,[FTStateActive])")
                                        oStrBuilder.AppendLine("VALUES")
                                        oStrBuilder.AppendLine("   (NULL")
                                        oStrBuilder.AppendLine("   ,NULL")
                                        oStrBuilder.AppendLine("   ,NULL")
                                        oStrBuilder.AppendLine("   ,NULL")
                                        oStrBuilder.AppendLine("   ,NULL")
                                        oStrBuilder.AppendLine("   ,NULL")
                                        oStrBuilder.AppendLine(String.Format("   ,{0}", nFNHSysBuyGrpId))
                                        oStrBuilder.AppendLine("   ,N'" & HI.UL.ULF.rpQuoted(tBuyGrpCode) & "'")
                                        oStrBuilder.AppendLine("   ,N'" & HI.UL.ULF.rpQuoted(tBuyGrpDesc) & "'")
                                        oStrBuilder.AppendLine("   ,N'" & HI.UL.ULF.rpQuoted(tBuyGrpDesc) & "'")
                                        oStrBuilder.AppendLine("   ,NULL")
                                        oStrBuilder.AppendLine("   ,NULL")
                                        oStrBuilder.AppendLine("   ,''")
                                        oStrBuilder.AppendLine("   ,'1')")
                                        oStrBuilder.AppendLine("")

                                        tSql = ""
                                        tSql = oStrBuilder.ToString()

                                        If HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MASTER) Then
                                            'MsgBox("Execute data complelte...", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
                                        End If

                                    End If

                                End If

                            End If

                        End If

                    End If

                    'Application.DoEvents()

                Next nLoopBuyGrp

                bRet = True
            End If

        Catch ex As Exception
            'If System.Diagnostics.Debugger.IsAttached = True Then
            MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
            'End If
        End Try

        Return bRet

    End Function

    Private Function W_PRCbValidateExistsMasterPlant(ByVal oDBdt As System.Data.DataTable) As Boolean
        '...default FNQtySpecialType : 2 {%}
        '...default FNQtySpecial : 1
        Dim bRet As Boolean = False
        Try
            If Not oDBdt Is Nothing And oDBdt.Rows.Count > 0 And oDBdt.Rows.Count > _nRowHeader Then
                Dim tColPlant As String = "F31"
                Dim tColPlantDesc As String = "F32"

                Dim nLoopPlant As Integer

                For nLoopPlant = _nStartRowImportExcel To oDBdt.Rows.Count - 1

                    If Not DBNull.Value.Equals(oDBdt.Rows(nLoopPlant).Item(tColPlant)) Then
                        If oDBdt.Rows(nLoopPlant).Item(tColPlant).ToString() <> "" Then
                            Dim tPlantCode As String = ""

                            tPlantCode = oDBdt.Rows(nLoopPlant).Item(tColPlant).ToString()

                            If Not DBNull.Value.Equals(oDBdt.Rows(nLoopPlant).Item(tColPlantDesc)) Then
                                If oDBdt.Rows(nLoopPlant).Item(tColPlantDesc).ToString() <> "" Then
                                    Dim tPlantDesc As String = ""

                                    tPlantDesc = oDBdt.Rows(nLoopPlant).Item(tColPlantDesc).ToString()

                                    tSql = ""
                                    tSql = "SELECT TOP 1 A.FTPlantCode FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMPlant] AS A WITH(NOLOCK) WHERE A.FTPlantCode = N'" & HI.UL.ULF.rpQuoted(tPlantCode) & "';"

                                    If HI.Conn.SQLConn.GetField(tSql, HI.Conn.DB.DataBaseName.DB_MASTER, "") = "" Then
                                        Dim nFNHSysPlantId As Integer

                                        nFNHSysPlantId = Val(HI.TL.RunID.GetRunNoID("TMERMPlant", "FNHSysPlantId", Conn.DB.DataBaseName.DB_MASTER).ToString())

                                        Dim oStrBuilder As New System.Text.StringBuilder()

                                        oStrBuilder.Remove(0, oStrBuilder.Length)

                                        oStrBuilder.AppendLine("INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMPlant]([FTInsUser]")
                                        oStrBuilder.AppendLine("											  ,[FDInsDate]")
                                        oStrBuilder.AppendLine("											  ,[FTInsTime]")
                                        oStrBuilder.AppendLine("											  ,[FTUpdUser]")
                                        oStrBuilder.AppendLine("											  ,[FDUpdDate]")
                                        oStrBuilder.AppendLine("											  ,[FTUpdTime]")
                                        oStrBuilder.AppendLine("											  ,[FNHSysPlantId]")
                                        oStrBuilder.AppendLine("											  ,[FTPlantCode]")
                                        oStrBuilder.AppendLine("											  ,[FTPlantNameTH]")
                                        oStrBuilder.AppendLine("											  ,[FTPlantNameEN]")
                                        oStrBuilder.AppendLine("											  ,[FNQtySpecialType]")
                                        oStrBuilder.AppendLine("                                              ,[FNQtySpecial]")
                                        oStrBuilder.AppendLine("											  ,[FTRemark]")
                                        oStrBuilder.AppendLine("											  ,[FTStateActive])")
                                        oStrBuilder.AppendLine("VALUES(NULL")
                                        oStrBuilder.AppendLine("      ,NULL")
                                        oStrBuilder.AppendLine("      ,NULL")
                                        oStrBuilder.AppendLine("      ,NULL")
                                        oStrBuilder.AppendLine("      ,NULL")
                                        oStrBuilder.AppendLine("      ,NULL")
                                        oStrBuilder.AppendLine(String.Format("      ,{0}", nFNHSysPlantId))
                                        oStrBuilder.AppendLine("      ,N'" & HI.UL.ULF.rpQuoted(tPlantCode) & "'")
                                        oStrBuilder.AppendLine("      ,N'" & HI.UL.ULF.rpQuoted(tPlantDesc) & "'")
                                        oStrBuilder.AppendLine("      ,N'" & HI.UL.ULF.rpQuoted(tPlantDesc) & "'")
                                        oStrBuilder.AppendLine("      ,2")
                                        oStrBuilder.AppendLine("      ,1")
                                        oStrBuilder.AppendLine("      ,''")
                                        oStrBuilder.AppendLine("      ,'1')")

                                        tSql = ""
                                        tSql = oStrBuilder.ToString()

                                        If HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MASTER) = True Then
                                            'MsgBox("Execute data complete...", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
                                        End If

                                    End If

                                End If

                            End If

                        End If

                    End If

                    'Application.DoEvents()

                Next nLoopPlant

                bRet = True
            End If

        Catch ex As Exception
            'If System.Diagnostics.Debugger.IsAttached = True Then
            MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
            'End If
        End Try

        Return bRet

    End Function

    Private Function W_PRCbValidateExistsMasterCountry(ByVal oDBdt As System.Data.DataTable) As Boolean
        Dim bRet As Boolean
        Try
            If Not oDBdt Is Nothing And oDBdt.Rows.Count > 0 And oDBdt.Rows.Count > _nRowHeader Then
                Dim tColCountry As String = "F39"

                Dim nLoopCountry As Integer

                For nLoopCountry = _nStartRowImportExcel To oDBdt.Rows.Count - 1

                    '_oSplash.UpdateInformation(_tSplashText & Environment.NewLine & "กำลังดำเนินการตรวจสอบข้อมูลประเทศ...")

                    If Not DBNull.Value.Equals(oDBdt.Rows(nLoopCountry).Item(tColCountry)) Then
                        If oDBdt.Rows(nLoopCountry).Item(tColCountry).ToString() <> "" Then
                            Dim tCountry As String
                            tCountry = ""

                            tCountry = oDBdt.Rows(nLoopCountry).Item(tColCountry).ToString()
                            tCountry = tCountry.Trim()

                            tSql = ""
                            'tSql = "SELECT TOP 1 A.FTCountryNameEN FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCNMCountry] AS A WITH(NOLOCK) WHERE A.FTCountryNameEN = N'" & HI.UL.ULF.rpQuoted(tCountry) & "';"
                            tSql = "SELECT TOP 1 A.FTCountryNameEN FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCNMCountry] AS A WITH(NOLOCK) WHERE (A.FTCountryNameEN LIKE '%" & HI.UL.ULF.rpQuoted(tCountry) & "%' OR A.FTCountryCode LIKE '%" & HI.UL.ULF.rpQuoted(tCountry) & "');"

                            If HI.Conn.SQLConn.GetField(tSql, HI.Conn.DB.DataBaseName.DB_MASTER, "") = "" Then
                                Dim nFNHSysCountryId As Integer

                                nFNHSysCountryId = Val(HI.TL.RunID.GetRunNoID("TCNMCountry", "FNHSysCountryId", Conn.DB.DataBaseName.DB_MASTER).ToString())

                                Dim oStrBuilder As New System.Text.StringBuilder()

                                oStrBuilder.Remove(0, oStrBuilder.Length)

                                oStrBuilder.AppendLine("INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCNMCountry]([FTInsUser]")
                                oStrBuilder.AppendLine("                                               ,[FDInsDate]")
                                oStrBuilder.AppendLine("                                               ,[FTInsTime]")
                                oStrBuilder.AppendLine("                                               ,[FTUpdUser]")
                                oStrBuilder.AppendLine("                                               ,[FDUpdDate]")
                                oStrBuilder.AppendLine("                                               ,[FTUpdTime]")
                                oStrBuilder.AppendLine("											   ,[FNHSysCountryId]")
                                oStrBuilder.AppendLine("											   ,[FNHSysContinentId]")
                                oStrBuilder.AppendLine("											   ,[FTCountryCode]")
                                oStrBuilder.AppendLine("											   ,[FTCountryNameTH]")
                                oStrBuilder.AppendLine("											   ,[FTCountryNameEN]")
                                oStrBuilder.AppendLine("											   ,[FTRemark]")
                                oStrBuilder.AppendLine("											   ,[FTStateActive])")
                                oStrBuilder.AppendLine("VALUES(NULL")
                                oStrBuilder.AppendLine("      ,NULL")
                                oStrBuilder.AppendLine("      ,NULL")
                                oStrBuilder.AppendLine("      ,NULL")
                                oStrBuilder.AppendLine("      ,NULL")
                                oStrBuilder.AppendLine("      ,NULL")
                                oStrBuilder.AppendLine(String.Format("      ,{0}", nFNHSysCountryId))
                                oStrBuilder.AppendLine("      ,NULL")
                                oStrBuilder.AppendLine("      ,N'" & HI.UL.ULF.rpQuoted(tCountry) & "'")
                                oStrBuilder.AppendLine("      ,N'" & HI.UL.ULF.rpQuoted(tCountry) & "'")
                                oStrBuilder.AppendLine("      ,N'" & HI.UL.ULF.rpQuoted(tCountry) & "'")
                                oStrBuilder.AppendLine("      ,''")
                                oStrBuilder.AppendLine("      ,'1');")

                                tSql = ""
                                tSql = oStrBuilder.ToString()

                                If HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MASTER) Then
                                    '...validate new country not map continent
                                    Dim nFNHSysContinentId As Integer

                                    nFNHSysContinentId = Val(HI.TL.RunID.GetRunNoID("TCNMContinent", "FNHSysContinentId", Conn.DB.DataBaseName.DB_MASTER).ToString())

                                    oStrBuilder.Remove(0, oStrBuilder.Length)

                                    oStrBuilder.AppendLine("DECLARE @FNHSysContinentId AS INT;")
                                    oStrBuilder.AppendLine("DECLARE @FNHSysCountryId AS INT;")
                                    oStrBuilder.AppendLine(String.Format("SET @FNHSysContinentId = {0};", nFNHSysContinentId))
                                    oStrBuilder.AppendLine(String.Format("SET @FNHSysCountryId = {0};", nFNHSysCountryId))
                                    oStrBuilder.AppendLine("INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCNMContinent] ([FTInsUser],[FDInsDate],[FTInsTime],[FTUpdUser],[FDUpdDate],[FTUpdTime]")
                                    oStrBuilder.AppendLine("											 ,[FNHSysContinentId]")
                                    oStrBuilder.AppendLine("										     ,[FTContinentCode]")
                                    oStrBuilder.AppendLine("										     ,[FTContinentNameTH]")
                                    oStrBuilder.AppendLine("										     ,[FTContinentNameEN]")
                                    oStrBuilder.AppendLine("											 ,[FTRemark]")
                                    oStrBuilder.AppendLine("										     ,[FTStateActive])")
                                    oStrBuilder.AppendLine("SELECT NULL, NULL, NULL, NULL, NULL, NULL, @FNHSysContinentId, A.FTCountryCode, A.FTCountryCode, A.FTCountryCode,'', N'1'")
                                    oStrBuilder.AppendLine("FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCNMCountry] AS A WITH(NOLOCK)")
                                    oStrBuilder.AppendLine("WHERE A.FNHSysCountryId = @FNHSysCountryId;")

                                    If oStrBuilder.Length > 0 Then
                                        tSql = ""
                                        tSql = oStrBuilder.ToString()
                                        If HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MASTER) = True Then
                                            '...map country zone continent
                                            tSql = ""
                                            tSql = "UPDATE A"
                                            tSql &= Environment.NewLine & "SET  A.FNHSysContinentId = " & nFNHSysContinentId
                                            tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCNMCountry] AS A"
                                            tSql &= Environment.NewLine & "WHERE A.FNHSysCountryId = " & nFNHSysCountryId & ";"

                                            HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MASTER)

                                        End If

                                    End If

                                End If

                            End If

                        End If

                    End If

                    'Application.DoEvents()

                Next nLoopCountry

                bRet = True

            End If

        Catch ex As Exception
            'If System.Diagnostics.Debugger.IsAttached = True Then
            MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
            'End If
        End Try

        Return bRet

    End Function

    Private Function W_PRCbValidateExistsMasterMerchanUnit(ByVal oDBDt As System.Data.DataTable) As Boolean
        Dim bRet As Boolean = True
        'Try
        '    If Not oDBDt Is Nothing And oDBDt.Rows.Count > 0 And oDBDt.Rows.Count > _nRowHeader Then
        '        Dim tColUom As String = "F47"

        '        Dim nLoopUOM As Integer

        '        For nLoopUOM = _nStartRowImportExcel To oDBDt.Rows.Count - 1

        '            If Not DBNull.Value.Equals(oDBDt.Rows(nLoopUOM).Item(tColUom)) Then
        '                If oDBDt.Rows(nLoopUOM).Item(tColUom).ToString() <> "" Then
        '                    Dim tUOM As String
        '                    tUOM = ""

        '                    tUOM = oDBDt.Rows(nLoopUOM).Item(tColUom).ToString()

        '                    If UCase(tUOM) <> _tUnitImport Then
        '                        tSql = ""
        '                        tSql = "SELECT TOP 1 A.FTUnitCode FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCNMUnit] AS A WITH(NOLOCK) WHERE A.FTUnitCode = N'" & HI.UL.ULF.rpQuoted(tUOM) & "' AND A.FTStateUnitSale = '1';"

        '                        If HI.Conn.SQLConn.GetField(tSql, HI.Conn.DB.DataBaseName.DB_MASTER, "") = "" Then
        '                            Dim nFNHSysUnitId As Integer

        '                            nFNHSysUnitId = Val(HI.TL.RunID.GetRunNoID("TCNMUnit", "FNHSysUnitId", Conn.DB.DataBaseName.DB_MASTER).ToString())

        '                            Dim oStrBuilder As New System.Text.StringBuilder()

        '                            oStrBuilder.Remove(0, oStrBuilder.Length)

        '                            oStrBuilder.AppendLine("INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCNMUnit]([FTInsUser]")
        '                            oStrBuilder.AppendLine("									   ,[FDInsDate]")
        '                            oStrBuilder.AppendLine("									   ,[FTInsTime]")
        '                            oStrBuilder.AppendLine("									   ,[FTUpdUser]")
        '                            oStrBuilder.AppendLine("									   ,[FDUpdDate]")
        '                            oStrBuilder.AppendLine("									   ,[FTUpdTime]")
        '                            oStrBuilder.AppendLine("									   ,[FNHSysUnitId]")
        '                            oStrBuilder.AppendLine("									   ,[FTUnitCode]")
        '                            oStrBuilder.AppendLine("									   ,[FTUnitNameTH]")
        '                            oStrBuilder.AppendLine("									   ,[FTUnitNameEN]")
        '                            oStrBuilder.AppendLine("									   ,[FTRemark]")
        '                            oStrBuilder.AppendLine("                                       ,[FTStateUnitSale]")
        '                            oStrBuilder.AppendLine("									   ,[FTStateUnitPurchase]")
        '                            oStrBuilder.AppendLine("									   ,[FTStateUnitStock]")
        '                            oStrBuilder.AppendLine("									   ,[FTStateActive])")
        '                            oStrBuilder.AppendLine("VALUES(NULL")
        '                            oStrBuilder.AppendLine("      ,NULL")
        '                            oStrBuilder.AppendLine("      ,NULL")
        '                            oStrBuilder.AppendLine("      ,NULL")
        '                            oStrBuilder.AppendLine("      ,NULL")
        '                            oStrBuilder.AppendLine("      ,NULL")
        '                            oStrBuilder.AppendLine(String.Format("      ,{0}", nFNHSysUnitId))
        '                            oStrBuilder.AppendLine("      ,N'" & HI.UL.ULF.rpQuoted(tUOM) & "'")
        '                            oStrBuilder.AppendLine("      ,N'" & HI.UL.ULF.rpQuoted(tUOM) & "'")
        '                            oStrBuilder.AppendLine("      ,N'" & HI.UL.ULF.rpQuoted(tUOM) & "'")
        '                            oStrBuilder.AppendLine("      ,''")
        '                            oStrBuilder.AppendLine("      ,'1'")
        '                            oStrBuilder.AppendLine("      ,NULL")
        '                            oStrBuilder.AppendLine("      ,NULL")
        '                            oStrBuilder.AppendLine("      ,'1')")

        '                            tSql = ""
        '                            tSql = oStrBuilder.ToString()

        '                            If HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MASTER) Then
        '                                'MsgBox("Execute data complete ...", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
        '                            End If

        '                        End If

        '                    End If

        '                End If

        '            End If

        '            'Application.DoEvents()

        '        Next nLoopUOM

        '        bRet = True

        '    End If

        'Catch ex As Exception
        '    'If System.Diagnostics.Debugger.IsAttached = True Then
        '    MsgBox(ex.Message().ToString() & ex.StackTrace().ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
        '    ' End If
        'End Try

        Return bRet

    End Function

    Private Function W_PRCbValidateExistsMasterShipMode(ByVal oDBdt As System.Data.DataTable) As Boolean
        Dim bRet As Boolean = False
        Try
            If Not oDBdt Is Nothing And oDBdt.Rows.Count > 0 And oDBdt.Rows.Count > _nRowHeader Then
                Dim tColShipMode As String = "F46"

                Dim nLoopShipMode As Integer

                For nLoopShipMode = _nStartRowImportExcel To oDBdt.Rows.Count - 1

                    '_oSplash.UpdateInformation(_tSplashText & Environment.NewLine & "กำลังดำเนินการตรวจสอบข้อมูลประเภทการจัดส่งสินค้า..")

                    If Not DBNull.Value.Equals(oDBdt.Rows(nLoopShipMode).Item(tColShipMode)) Then
                        If oDBdt.Rows(nLoopShipMode).Item(tColShipMode).ToString() <> "" Then
                            Dim tShipMode As String
                            tShipMode = ""

                            tShipMode = oDBdt.Rows(nLoopShipMode).Item(tColShipMode).ToString()
                            tShipMode = tShipMode.Trim()

                            tSql = ""
                            tSql = "SELECT TOP 1 A.FTShipModeCode FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCNMShipMode] AS A WITH(NOLOCK) WHERE A.FTShipModeCode = N'" & HI.UL.ULF.rpQuoted(tShipMode) & "';"

                            If HI.Conn.SQLConn.GetField(tSql, HI.Conn.DB.DataBaseName.DB_MASTER, "") = "" Then
                                Dim nFNHSysShipModeId As Integer

                                nFNHSysShipModeId = Val(HI.TL.RunID.GetRunNoID("TCNMShipMode", "FNHSysShipModeId", Conn.DB.DataBaseName.DB_MASTER).ToString())

                                Dim oStrBuilder As New System.Text.StringBuilder()

                                oStrBuilder.Remove(0, oStrBuilder.Length)

                                oStrBuilder.AppendLine("INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCNMShipMode]")
                                oStrBuilder.AppendLine("           ([FTInsUser]")
                                oStrBuilder.AppendLine("           ,[FDInsDate]")
                                oStrBuilder.AppendLine("           ,[FTInsTime]")
                                oStrBuilder.AppendLine("           ,[FTUpdUser]")
                                oStrBuilder.AppendLine("           ,[FDUpdDate]")
                                oStrBuilder.AppendLine("           ,[FTUpdTime]")
                                oStrBuilder.AppendLine("           ,[FNHSysShipModeId]")
                                oStrBuilder.AppendLine("           ,[FTShipModeCode]")
                                oStrBuilder.AppendLine("           ,[FTShipModenNameTH]")
                                oStrBuilder.AppendLine("           ,[FTShipModeNameEN]")
                                oStrBuilder.AppendLine("           ,[FTRemark]")
                                oStrBuilder.AppendLine("           ,[FTStateActive])")
                                oStrBuilder.AppendLine("VALUES (NULL")
                                oStrBuilder.AppendLine("       ,NULL")
                                oStrBuilder.AppendLine("       ,NULL")
                                oStrBuilder.AppendLine("       ,NULL")
                                oStrBuilder.AppendLine("       ,NULL")
                                oStrBuilder.AppendLine("       ,NULL")
                                oStrBuilder.AppendLine(String.Format("       ,{0}", nFNHSysShipModeId))
                                oStrBuilder.AppendLine("       ,N'" & HI.UL.ULF.rpQuoted(tShipMode) & "'")
                                oStrBuilder.AppendLine("       ,N'" & HI.UL.ULF.rpQuoted(tShipMode) & "'")
                                oStrBuilder.AppendLine("       ,N'" & HI.UL.ULF.rpQuoted(tShipMode) & "'")
                                oStrBuilder.AppendLine("       ,''")
                                oStrBuilder.AppendLine("       ,'1')")

                                tSql = ""
                                tSql = oStrBuilder.ToString()

                                If HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MASTER) = True Then
                                    'MsgBox("Execute data complete ...", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
                                End If

                            End If

                        End If

                    End If

                    'Application.DoEvents()

                Next nLoopShipMode

                bRet = True

            End If

        Catch ex As Exception
            'If System.Diagnostics.Debugger.IsAttached = True Then
            MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
            'End If
        End Try

        Return bRet

    End Function

    Private Function W_PRCbValidateExistsMasterBuyer(ByVal oDBdt As System.Data.DataTable) As Boolean
        Dim bRet As Boolean
        Try
            If Not oDBdt Is Nothing And oDBdt.Rows.Count > 0 And oDBdt.Rows.Count > _nRowHeader Then
                Dim tColBuyer As String = "F36"
                Dim tColBuyerDesc As String = "F37"

                Dim nLoopBuyer As Integer
                'For nLoopBuyer = _nStartRowImportExcel To oDBdt.Rows.Count - 1
                For nLoopBuyer = _nStartRowImportExcel To oDBdt.Rows.Count - 1

                    '_oSplash.UpdateInformation(_tSplashText & Environment.NewLine & "กำลังดำเนินการตรวจสอบข้อมูลผู้ซื้อ...")

                    If Not DBNull.Value.Equals(oDBdt.Rows(nLoopBuyer).Item(tColBuyer)) Then
                        Dim tBuyerCode As String = ""

                        tBuyerCode = oDBdt.Rows(nLoopBuyer).Item(tColBuyer).ToString()
                        tBuyerCode = tBuyerCode.Trim()

                        If tBuyerCode <> "" And UCase(tBuyerCode) <> UCase(_tNotAssigned) And UCase(tBuyerCode) <> UCase(_tNA) Then

                            If Not DBNull.Value.Equals(oDBdt.Rows(nLoopBuyer).Item(tColBuyerDesc)) Then
                                Dim tBuyerDesc As String = ""

                                tBuyerDesc = oDBdt.Rows(nLoopBuyer).Item(tColBuyerDesc).ToString()
                                tBuyerDesc = tBuyerDesc.Trim()

                                If tBuyerDesc <> "" And UCase(tBuyerDesc) <> UCase(_tNotAssigned) And UCase(tBuyerDesc) <> UCase(_tNA) Then

                                    tSql = ""
                                    tSql = "SELECT TOP 1 A.FTBuyerCode  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMBuyer] AS A WITH(NOLOCK) WHERE A.FTBuyerCode = N'" & HI.UL.ULF.rpQuoted(tBuyerCode) & "';"

                                    If HI.Conn.SQLConn.GetField(tSql, HI.Conn.DB.DataBaseName.DB_MASTER, "") = "" Then
                                        Dim nFNHSysBuyerId As Integer

                                        nFNHSysBuyerId = Val(HI.TL.RunID.GetRunNoID("TMERMBuyer", "FNHSysBuyerId", Conn.DB.DataBaseName.DB_MASTER).ToString())

                                        Dim oStrBuilder As New System.Text.StringBuilder()

                                        oStrBuilder.Remove(0, oStrBuilder.Length)

                                        oStrBuilder.AppendLine("INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMBuyer]")
                                        oStrBuilder.AppendLine("           ([FTInsUser]")
                                        oStrBuilder.AppendLine("           ,[FDInsDate]")
                                        oStrBuilder.AppendLine("           ,[FTInsTime]")
                                        oStrBuilder.AppendLine("           ,[FTUpdUser]")
                                        oStrBuilder.AppendLine("           ,[FDUpdDate]")
                                        oStrBuilder.AppendLine("           ,[FTUpdTime]")
                                        oStrBuilder.AppendLine("           ,[FNHSysBuyerId]")
                                        oStrBuilder.AppendLine("           ,[FTBuyerCode]")
                                        oStrBuilder.AppendLine("           ,[FTBuyerNameTH]")
                                        oStrBuilder.AppendLine("           ,[FTBuyerNameEN]")
                                        oStrBuilder.AppendLine("           ,[FTRemark]")
                                        oStrBuilder.AppendLine("           ,[FTStateActive])")
                                        oStrBuilder.AppendLine("VALUES")
                                        oStrBuilder.AppendLine(String.Format("   ({0}", "NULL"))
                                        oStrBuilder.AppendLine(String.Format("   ,{0}", "NULL"))
                                        oStrBuilder.AppendLine(String.Format("   ,{0}", "NULL"))
                                        oStrBuilder.AppendLine(String.Format("   ,{0}", "NULL"))
                                        oStrBuilder.AppendLine(String.Format("   ,{0}", "NULL"))
                                        oStrBuilder.AppendLine(String.Format("   ,{0}", "NULL"))
                                        oStrBuilder.AppendLine(String.Format("   ,{0}", nFNHSysBuyerId))
                                        oStrBuilder.AppendLine(String.Format("   ,{0}", "N'" & HI.UL.ULF.rpQuoted(tBuyerCode) & "'"))
                                        oStrBuilder.AppendLine(String.Format("   ,{0}", "N'" & HI.UL.ULF.rpQuoted(tBuyerDesc) & "'"))
                                        oStrBuilder.AppendLine(String.Format("   ,{0}", "N'" & HI.UL.ULF.rpQuoted(tBuyerDesc) & "'"))
                                        oStrBuilder.AppendLine(String.Format("   ,{0}", "''"))
                                        oStrBuilder.AppendLine(String.Format("   ,{0})", "N'1'"))

                                        tSql = ""
                                        tSql = oStrBuilder.ToString()

                                        If HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MASTER) = True Then
                                            'MsgBox("Execuete data complete !!!", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
                                        Else
                                            MsgBox("Execute data not complete !!!", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
                                        End If

                                    End If

                                End If

                            End If

                        End If

                    End If

                    'Application.DoEvents()

                Next nLoopBuyer

                bRet = True

            End If

        Catch ex As Exception
            'If System.Diagnostics.Debugger.IsAttached = True Then
            MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
            'End If
        End Try

        Return bRet

    End Function

    Private Function W_PRCbValidateExistsMasterMerchanTeam(ByVal oDBdt As System.Data.DataTable) As Boolean
        Dim bRet As Boolean = False
        Try
            If Not oDBdt Is Nothing And oDBdt.Rows.Count > 0 And oDBdt.Rows.Count > _nRowHeader Then
                Dim tColMerTeam As String = "F14"

                Dim nLoopMasterMerchanTeam As Integer

                For nLoopMasterMerchanTeam = _nStartRowImportExcel To oDBdt.Rows.Count - 1
                    Dim tMerchanTeam As String
                    tMerchanTeam = ""

                    If Not DBNull.Value.Equals(oDBdt.Rows(nLoopMasterMerchanTeam).Item(tColMerTeam)) Then
                        If oDBdt.Rows(nLoopMasterMerchanTeam).Item(tColMerTeam).ToString() <> "" Then
                            tMerchanTeam = oDBdt.Rows(nLoopMasterMerchanTeam).Item(tColMerTeam).ToString()
                            tMerchanTeam = tMerchanTeam.Trim()

                            tSql = ""
                            tSql = "SELECT TOP 1 A.FTMerTeamNameEN FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMerTeam] AS A WITH(NOLOCK) WHERE A.FTMerTeamNameEN = N'" & HI.UL.ULF.rpQuoted(tMerchanTeam) & "';"

                            If HI.Conn.SQLConn.GetField(tSql, HI.Conn.DB.DataBaseName.DB_MASTER, "") = "" Then
                                Dim nFNHSysMerTeamId As Integer

                                nFNHSysMerTeamId = Val(HI.TL.RunID.GetRunNoID("TMERMMerTeam", "FNHSysMerTeamId", Conn.DB.DataBaseName.DB_MASTER).ToString())

                                Dim tMerchanTeamCode As String

                                tMerchanTeamCode = W_GETtMerTeamCode()

                                If tMerchanTeamCode <> "" Then
                                    tSql = ""
                                    tSql = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMerTeam]([FTInsUser],[FDInsDate],[FTInsTime]"
                                    tSql &= Environment.NewLine & "                         ,[FTUpdUser],[FDUpdDate],[FTUpdTime]"
                                    tSql &= Environment.NewLine & "                         ,[FNHSysMerTeamId],[FTMerTeamCode],[FTMerTeamNameTH]"
                                    tSql &= Environment.NewLine & "                         ,[FTMerTeamNameEN],[FTRemark],[FTStateActive])"
                                    tSql &= Environment.NewLine & "VALUES(NULL, NULL, NULL"
                                    tSql &= "                            ,NULL, NULL, NULL"
                                    tSql &= "," & nFNHSysMerTeamId & ", N'" & HI.UL.ULF.rpQuoted(tMerchanTeamCode) & "', N'" & HI.UL.ULF.rpQuoted(tMerchanTeam) & "'"
                                    tSql &= ",N'" & HI.UL.ULF.rpQuoted(tMerchanTeam) & "', '', '1')"

                                    If HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MASTER) Then
                                        'MsgBox("Execuete data complete !!!", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
                                    End If

                                End If

                            End If

                        End If

                    End If

                    'Application.DoEvents()

                Next nLoopMasterMerchanTeam

                bRet = True

            End If

        Catch ex As Exception
            'If System.Diagnostics.Debugger.IsAttached = True Then
            MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
            'End If
        End Try

        Return bRet

    End Function

    Private Function W_PRCbValidateExistsMasterGender(ByVal oDBdt As System.Data.DataTable) As Boolean
        Dim bRet As Boolean = False
        Try
            If Not oDBdt Is Nothing And oDBdt.Rows.Count > 0 And oDBdt.Rows.Count > _nRowHeader Then
                Dim tColGender As String = "F18"

                Dim nLoopMasterGender As Integer
                'For nLoopMasterGender = _nStartRowImportExcel To oDBdt.Rows.Count - 1
                For nLoopMasterGender = _nStartRowImportExcel To oDBdt.Rows.Count - 1

                    '_oSplash.UpdateInformation(_tSplashText & Environment.NewLine & "กำลังดำเนินการตรวจสอบข้อมูลเพศผลิตภัณฑ์...")

                    Dim tMatchGender As String
                    tMatchGender = ""

                    If Not DBNull.Value.Equals(oDBdt.Rows(nLoopMasterGender).Item(tColGender)) Then
                        If oDBdt.Rows(nLoopMasterGender).Item(tColGender).ToString() <> "" Then
                            tMatchGender = oDBdt.Rows(nLoopMasterGender).Item(tColGender).ToString()
                            tMatchGender = tMatchGender.Trim()

                            tSql = ""
                            tSql = "SELECT TOP 1 A.FTGenderNameEN FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMGender] AS A  WITH(NOLOCK) WHERE A.FTGenderNameEN = N'" & HI.UL.ULF.rpQuoted(tMatchGender) & "';"

                            If HI.Conn.SQLConn.GetField(tSql, HI.Conn.DB.DataBaseName.DB_MASTER, "") = "" Then
                                Dim nFNHSysGenderId As Integer

                                nFNHSysGenderId = Val(HI.TL.RunID.GetRunNoID("TMERMGender", "FNHSysGenderId", Conn.DB.DataBaseName.DB_MASTER).ToString())

                                tSql = ""
                                tSql = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMGender]([FTInsUser],[FDInsDate],[FTInsTime]"
                                tSql &= Environment.NewLine & "                        ,[FTUpdUser],[FDUpdDate],[FTUpdTime]"
                                tSql &= Environment.NewLine & "                        ,[FNHSysGenderId],[FTGenderCode],[FTGenderNameTH]"
                                tSql &= Environment.NewLine & "                        ,[FTGenderNameEN],[FTRemark],[FTStateActive])"
                                tSql &= Environment.NewLine & "VALUES(NULL, NULL, NULL"
                                tSql &= "                            ,NULL, NULL , NULL"
                                tSql &= "                            ," & nFNHSysGenderId & ", N'" & HI.UL.ULF.rpQuoted(tMatchGender) & "', N'" & HI.UL.ULF.rpQuoted(tMatchGender) & "'"
                                tSql &= "                            ,N'" & HI.UL.ULF.rpQuoted(tMatchGender) & "', '', '1')"

                                If HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MASTER) = True Then
                                    'MsgBox("Execuete data complete !!!", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
                                End If

                            End If

                        End If

                    End If

                    'Application.DoEvents()

                Next nLoopMasterGender

                bRet = True

            End If

        Catch ex As Exception
            'If System.Diagnostics.Debugger.IsAttached = True Then
            MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
            ' End If
        End Try

        Return bRet

    End Function

    Private Function W_PRCbValidateExistsMatchColor(ByVal oDBdt As System.Data.DataTable) As Boolean
        Dim bRet As Boolean = False

        Try
            If Not oDBdt Is Nothing And oDBdt.Rows.Count > 0 And oDBdt.Rows.Count > _nRowHeader Then
                Dim oDBdtMatColorTemp As System.Data.DataTable

                oDBdtMatColorTemp = New System.Data.DataTable

                Dim oColFTMatColorCode As DataColumn
                oColFTMatColorCode = New DataColumn("FTMatColorCode", System.Type.GetType("System.String"))
                oColFTMatColorCode.Caption = "FTMatColorCode"

                oDBdtMatColorTemp.Columns.Add(oColFTMatColorCode.ColumnName, oColFTMatColorCode.DataType)

                Dim oColFTMatColorDesc As DataColumn
                oColFTMatColorDesc = New DataColumn("FTMatColorDesc", System.Type.GetType("System.String"))
                oColFTMatColorDesc.Caption = "FTMatColorDesc"

                oDBdtMatColorTemp.Columns.Add(oColFTMatColorDesc.ColumnName, oColFTMatColorDesc.DataType)

                Dim tColMatColor As String = "F16"
                Dim tColMatColorDesc As String = "F23"

                Dim nLoopRowMatchColor As Integer
                'For nLoopRowMatchColor = _nStartRowImportExcel To oDBdt.Rows.Count - 1
                For nLoopRowMatchColor = _nStartRowImportExcel To oDBdt.Rows.Count - 1

                    '_oSplash.UpdateInformation(_tSplashText & Environment.NewLine & "กำลังดำเนินการตรวจสอบข้อมูลสีผลิตภัณฑ์...")

                    Dim tMatchColor As String = ""
                    tMatchColor = ""

                    If Not DBNull.Value.Equals(oDBdt.Rows(nLoopRowMatchColor).Item(tColMatColor)) Then
                        If oDBdt.Rows(nLoopRowMatchColor).Item(tColMatColor).ToString() <> "" Then
                            tMatchColor = oDBdt.Rows(nLoopRowMatchColor).Item(tColMatColor)

                            Dim nPosHyphen As Integer

                            nPosHyphen = InStr(tMatchColor, _tHyphenSign)

                            If nPosHyphen > 0 Then
                                tMatchColor = Microsoft.VisualBasic.Mid$(tMatchColor, nPosHyphen + 1, Len(tMatchColor))

                                If Not oDBdtMatColorTemp Is Nothing And oDBdtMatColorTemp.Rows.Count > 0 Then
                                    Dim oDataRowValidate As DataRow()

                                    oDataRowValidate = oDBdtMatColorTemp.Select("FTMatColorCode = '" & HI.UL.ULF.rpQuoted(tMatchColor) & "'")

                                    If Not oDataRowValidate Is Nothing And oDataRowValidate.Length <= 0 Then
                                        tSql = ""
                                        tSql = "SELECT TOP 1 A.FTMatColorCode FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMatColor] AS A WITH(NOLOCK) WHERE A.FTMatColorCode  = N'" & HI.UL.ULF.rpQuoted(tMatchColor) & "';"

                                        If HI.Conn.SQLConn.GetField(tSql, HI.Conn.DB.DataBaseName.DB_MASTER, "") = "" Then

                                            Dim tMatchColorDesc As String = ""
                                            tMatchColorDesc = oDBdt.Rows(nLoopRowMatchColor).Item(tColMatColorDesc).ToString().Trim()

                                            Dim oRowItemMatColor As DataRow
                                            '...กรณีเป็นรายการ รหัส สีที่ยังไม่มีอยู่ในระบบ
                                            oRowItemMatColor = oDBdtMatColorTemp.NewRow()
                                            oRowItemMatColor.Item("FTMatColorCode") = tMatchColor
                                            oRowItemMatColor.Item("FTMatColorDesc") = tMatchColorDesc
                                            oDBdtMatColorTemp.Rows.Add(oRowItemMatColor)

                                        End If

                                    End If

                                ElseIf Not oDBdtMatColorTemp Is Nothing And oDBdtMatColorTemp.Rows.Count = 0 Then
                                    tSql = ""
                                    tSql = "SELECT TOP 1 A.FTMatColorCode FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMatColor] AS A WITH(NOLOCK) WHERE A.FTMatColorCode  = N'" & HI.UL.ULF.rpQuoted(tMatchColor) & "';"

                                    If HI.Conn.SQLConn.GetField(tSql, HI.Conn.DB.DataBaseName.DB_MASTER, "") = "" Then

                                        Dim tMatchColorDesc As String = ""
                                        tMatchColorDesc = oDBdt.Rows(nLoopRowMatchColor).Item(tColMatColorDesc).ToString().Trim()

                                        Dim oRowItemMatColor As DataRow
                                        '...กรณีเป็นรายการ รหัส สีที่ยังไม่มีอยู่ในระบบ
                                        oRowItemMatColor = oDBdtMatColorTemp.NewRow()
                                        oRowItemMatColor.Item("FTMatColorCode") = tMatchColor
                                        oRowItemMatColor.Item("FTMatColorDesc") = tMatchColorDesc
                                        oDBdtMatColorTemp.Rows.Add(oRowItemMatColor)

                                    End If

                                End If

                            End If

                        End If

                    End If

                    'Application.DoEvents()

                Next nLoopRowMatchColor

                If Not oDBdtMatColorTemp Is Nothing And oDBdtMatColorTemp.Rows.Count > 0 Then
                    oDBdtMatColorTemp.AcceptChanges()

                    Dim oDBdvNotExistsMatColor As System.Data.DataView = oDBdtMatColorTemp.DefaultView
                    oDBdvNotExistsMatColor.Sort = "FTMatColorCode"
                    Dim oDBdtNotExistsMatColor As System.Data.DataTable = oDBdvNotExistsMatColor.ToTable()
                    If Not oDBdtNotExistsMatColor Is Nothing And oDBdtNotExistsMatColor.Rows.Count > 0 Then
                        Dim nLoopNotExitsMatColor As Integer
                        Dim tMatColorCodeNotExists As String, tMatColorDescNotExists As String

                        For nLoopNotExitsMatColor = 0 To oDBdtNotExistsMatColor.Rows.Count - 1
                            tMatColorCodeNotExists = oDBdtNotExistsMatColor.Rows(nLoopNotExitsMatColor).Item("FTMatColorCode")
                            tMatColorDescNotExists = oDBdtNotExistsMatColor.Rows(nLoopNotExitsMatColor).Item("FTMatColorDesc")

                            tSql = ""
                            tSql = "SELECT TOP 1 A.FTMatColorCode FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMatColor] AS A WITH(NOLOCK) WHERE A.FTMatColorCode  = N'" & HI.UL.ULF.rpQuoted(tMatColorCodeNotExists) & "';"

                            If HI.Conn.SQLConn.GetField(tSql, HI.Conn.DB.DataBaseName.DB_MASTER, "") = "" Then
                                Dim nFNHSysMatColorId As Integer

                                nFNHSysMatColorId = Val(HI.TL.RunID.GetRunNoID("TMERMMatColor", "FNHSysMatColorId", Conn.DB.DataBaseName.DB_MASTER).ToString())

                                tSql = ""
                                tSql = "DECLARE @nFNMatColorId AS INT;"
                                tSql &= Environment.NewLine & "DECLARE @tFTMatColorCode AS NVARCHAR(30);"
                                tSql &= Environment.NewLine & "DECLARE @tFTMatColorDesc AS NVARCHAR(100);"
                                tSql &= Environment.NewLine & "SET @nFNMatColorId = " & nFNHSysMatColorId & ";"
                                tSql &= Environment.NewLine & "SET @tFTMatColorCode = N'" & HI.UL.ULF.rpQuoted(tMatColorCodeNotExists) & "';"
                                tSql &= Environment.NewLine & "SET @tFTMatColorDesc = N'" & HI.UL.ULF.rpQuoted(tMatColorDescNotExists) & "';"
                                tSql &= Environment.NewLine & "DECLARE @FNMatColorSeq AS INT;"
                                tSql &= Environment.NewLine & "SELECT @FNMatColorSeq = ISNULL(MAX(A.FNMatColorSeq),0)"
                                tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMatColor] AS A"
                                tSql &= Environment.NewLine & "GROUP BY A.FNMatColorSeq;"
                                tSql &= Environment.NewLine & "--PRINT 'FNMatColorSeq Current : ' + CONVERT(VARCHAR(10), @FNMatColorSeq);"
                                tSql &= Environment.NewLine & "--PRINT 'FNMatColorSeq Next : ' + CONVERT(VARCHAR(10), (@FNMatColorSeq + 1));"
                                tSql &= Environment.NewLine & "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].[dbo].[TMERMMatColor]([FTInsUser],[FDInsDate],[FTInsTime]"
                                tSql &= Environment.NewLine & "                                                 ,[FTUpdUser],[FDUpdDate],[FTUpdTime]"
                                tSql &= Environment.NewLine & "                                                 ,[FNHSysMatColorId],[FTMatColorCode],[FNMatColorSeq]"
                                tSql &= Environment.NewLine & "                                                 ,[FTMatColorNameTH],[FTMatColorNameEN],[FTRemark],[FTStateActive])"
                                tSql &= Environment.NewLine & "VALUES(NULL, NULL, NULL"
                                tSql &= Environment.NewLine & ",NULL, NULL, NULL"
                                tSql &= Environment.NewLine & ",@nFNMatColorId, @tFTMatColorCode, (ISNULL(@FNMatColorSeq, 0) + 1)"
                                tSql &= Environment.NewLine & ",@tFTMatColorDesc, @tFTMatColorDesc, '', '1');"

                                If HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MASTER) = True Then
                                    'MsgBox("Execuete data complete !!!", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
                                End If

                            End If

                        Next nLoopNotExitsMatColor

                    End If

                Else
                    '...do nothing
                End If

                bRet = True

            End If

        Catch ex As Exception
            'If System.Diagnostics.Debugger.IsAttached = True Then
            MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), My.Application.Info.Title)
            'End If
        End Try

        Return bRet

    End Function

    Private Function W_PRCbValidateExistsMatchSize(ByVal oDBdt As System.Data.DataTable, ByRef paramDTNotMapSize As System.Data.DataTable) As Boolean
        Dim bRet As Boolean = False
        Try
            If Not oDBdt Is Nothing And oDBdt.Rows.Count > 0 And oDBdt.Rows.Count > _nRowHeader And oDBdt.Columns.Count > _nStartColImportSizeValExcel Then
                Dim nCntColMatchSize As Integer
                Dim nLoopColMatchSize As Integer
                Dim tMatchSize As String

                nCntColMatchSize = oDBdt.Columns.Count
                nLoopColMatchSize = _nStartColImportSizeValExcel

                tMatchSize = oDBdt.Rows(_nRowHeader - 2).Item(_nStartColImportSizeValExcel).ToString()

                If Not oDBdtMapSize Is Nothing AndAlso oDBdtMapSize.Rows.Count > 0 Then
                    Dim tMapSizeExtension As String = ""
                    Dim oDataRow As DataRow()
                    Try
                        oDataRow = oDBdtMapSize.Select("FTMapSize = '" & HI.UL.ULF.rpQuoted(tMatchSize) & "'")
                        If oDataRow.Length > 0 Then '...XXL ==> 2XL
                            If oDataRow(0)("FTMapSizeExtension") <> "" Then
                                tMatchSize = oDataRow(0)("FTMapSizeExtension").ToString()
                            End If

                        End If

                    Catch ex As Exception
                    End Try

                End If

                Do While tMatchSize <> "" And UCase(tMatchSize) <> UCase(_tTextMatchSizeTerminate) And nLoopColMatchSize < nCntColMatchSize

                    '_oSplash.UpdateInformation(_tSplashText & Environment.NewLine & "กำลังดำเนินการตรวจสอบข้อมูลขนาดผลิตภัณฑ์...")

                    tSql = ""
                    tSql = "SELECT TOP 1 A.FTMatSizeCode FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMatSize] AS A WITH(NOLOCK) WHERE A.FTMatSizeCode = N'" & HI.UL.ULF.rpQuoted(tMatchSize) & "';"
                    '...Mat Size Code not exists in system
                    If HI.Conn.SQLConn.GetField(tSql, HI.Conn.DB.DataBaseName.DB_MASTER, "") = "" Then
                        '...not match size code in system database TMERMMatSize [MASTER]

                        REM 2014/11/27
                        '====================================================================================================================================================================================================
                        'Dim nFNHSysMatSizeId As Integer

                        'nFNHSysMatSizeId = Val(HI.TL.RunID.GetRunNoID("TMERMMatSize", "FNHSysMatSizeId", Conn.DB.DataBaseName.DB_MASTER).ToString())

                        'tSql = ""
                        'tSql = "DECLARE @FNMatSizeSeqMax AS INT;"
                        'tSql &= Environment.NewLine & "SELECT @FNMatSizeSeqMax = MAX(A.FNMatSizeSeq)"
                        'tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMatSize] AS A WITH(NOLOCK)"
                        'tSql &= Environment.NewLine & "GROUP BY A.FNHSysMatSizeId;"
                        'tSql &= Environment.NewLine & "--PRINT 'FNMatSizeSeqMax Current : ' + CONVERT(VARCHAR(10),ISNULL(@FNMatSizeSeqMax, 1));"
                        'tSql &= Environment.NewLine & "--PRINT 'FNMatSizeSeq Max Next : ' + CONVERT(VARCHAR(10),(ISNULL(@FNMatSizeSeqMax, 1) + 1));"
                        'tSql &= Environment.NewLine & "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMatSize] ([FTInsUser],[FDInsDate],[FTInsTime]"
                        'tSql &= Environment.NewLine & "                                                                                                    ,[FTUpdUser],[FDUpdDate],[FTUpdTime]"
                        'tSql &= Environment.NewLine & "                                                                                                    ,[FNHSysMatSizeId],[FTMatSizeCode],[FNMatSizeSeq]"
                        'tSql &= Environment.NewLine & "							                                                                           ,[FTMatSizeNameTH],[FTMatSizeNameEN],[FTRemark],[FTStateActive])"
                        'tSql &= Environment.NewLine & "VALUES(NULL, NULL, NULL"
                        'tSql &= ",NULL, NULL, NULL"
                        'tSql &= ", " & nFNHSysMatSizeId & ", N'" & HI.UL.ULF.rpQuoted(tMatchSize) & "', (ISNULL(@FNMatSizeSeqMax, 0) + 1)"
                        'tSql &= ", N'" & HI.UL.ULF.rpQuoted(tMatchSize) & "', N'" & HI.UL.ULF.rpQuoted(tMatchSize) & "', '', '1');"

                        'If HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MASTER) = True Then
                        '    'MsgBox("Execute Data Complete !!!", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
                        'End If
                        '====================================================================================================================================================================================================

                        If paramDTNotMapSize.Select("FTSizeCodeNotExists = '" & HI.UL.ULF.rpQuoted(tMatchSize) & "'").Length <= 0 Then
                            Dim oDataRow As System.Data.DataRow

                            oDataRow = paramDTNotMapSize.NewRow()

                            oDataRow.Item("FTSizeCodeNotExists") = tMatchSize
                            oDataRow.Item("FTMapSizeExtend") = ""

                            paramDTNotMapSize.Rows.Add(oDataRow)
                        End If

                    End If

                    nLoopColMatchSize = nLoopColMatchSize + 5

                    tMatchSize = oDBdt.Rows(_nRowHeader - 2).Item(nLoopColMatchSize).ToString()

                    If Not oDBdtMapSize Is Nothing AndAlso oDBdtMapSize.Rows.Count > 0 Then
                        Dim tMapSizeExtension As String = ""
                        Dim oDataRow As DataRow()
                        Try
                            oDataRow = oDBdtMapSize.Select("FTMapSize = '" & HI.UL.ULF.rpQuoted(tMatchSize) & "'")

                            If oDataRow.Length > 0 Then
                                If oDataRow(0)("FTMapSizeExtension") <> "" Then
                                    tMatchSize = oDataRow(0)("FTMapSizeExtension").ToString()
                                End If

                            Else
                                '...mat size id not exists in system database ???
                            End If

                        Catch ex As Exception
                        End Try

                    End If

                    'Application.DoEvents()

                Loop

                If Not paramDTNotMapSize Is Nothing AndAlso paramDTNotMapSize.Rows.Count > 0 Then paramDTNotMapSize.AcceptChanges()

                bRet = True

            End If

        Catch ex As Exception
            'If System.Diagnostics.Debugger.IsAttached = True Then
            MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.OkOnly, My.Application.Info.Title)
            'End If
        End Try

        Return bRet

    End Function

    Private Function W_PRCbValidateExistsMasterStyle(ByVal oDBdt As System.Data.DataTable) As Boolean
        Dim bRet As Boolean = False
        Try
            If Not oDBdt Is Nothing And oDBdt.Rows.Count > 0 And oDBdt.Rows.Count > _nRowHeader Then

                '...represent style master when import new style
                '-------------------------------------------------------------------------------
                oDBdtStyle = Nothing
                oDBdtStyle = New System.Data.DataTable

                Dim oColFTStyleCode As DataColumn
                oColFTStyleCode = New DataColumn("FTStyleCode", System.Type.GetType("System.String"))

                oDBdtStyle.Columns.Add(oColFTStyleCode.ColumnName, oColFTStyleCode.DataType)

                Dim oColFTStyleName As DataColumn
                oColFTStyleName = New DataColumn("FTStyleName", System.Type.GetType("System.String"))

                oDBdtStyle.Columns.Add(oColFTStyleName.ColumnName, oColFTStyleName.DataType)
                '-------------------------------------------------------------------------------

                Dim tColStyle As String = "F15"
                Dim tColMaterialStyle As String = "F17"
                Dim tColPlanSeasonStyle As String = "F25"
                Dim tColYearStyle As String = "F26"
                Dim tColCustStyle As String = "F36"

                Dim nFNHSysCustId As Integer
                nFNHSysCustId = 0

                Dim nLoopStyle As Integer
                'For nLoopStyle = _nStartRowImportExcel To oDBdt.Rows.Count - 1
                For nLoopStyle = _nStartRowImportExcel To oDBdt.Rows.Count - 1

                    '_oSplash.UpdateInformation(_tSplashText & Environment.NewLine & "กำลังดำเนินการตรวจสอบข้อมูลแบบผลิตภัณฑ์...")

                    If Not DBNull.Value.Equals(oDBdt.Rows(nLoopStyle).Item(tColStyle)) Then
                        If oDBdt.Rows(nLoopStyle).Item(tColStyle).ToString() <> "" Then
                            Dim tStyleCode As String
                            tStyleCode = ""

                            tStyleCode = oDBdt.Rows(nLoopStyle).Item(tColStyle).ToString()
                            tStyleCode = tStyleCode.Trim()

                            If Not DBNull.Value.Equals(oDBdt.Rows(nLoopStyle).Item(tColPlanSeasonStyle)) Then
                                If oDBdt.Rows(nLoopStyle).Item(tColPlanSeasonStyle).ToString() <> "" Then
                                    Dim tPlanningSeasonStyle As String
                                    tPlanningSeasonStyle = ""

                                    tPlanningSeasonStyle = oDBdt.Rows(nLoopStyle).Item(tColPlanSeasonStyle).ToString()
                                    tPlanningSeasonStyle.Trim()

                                    If Not DBNull.Value.Equals(oDBdt.Rows(nLoopStyle).Item(tColYearStyle)) Then
                                        If oDBdt.Rows(nLoopStyle).Item(tColYearStyle).ToString() <> "" Then
                                            Dim tYearStyle As String
                                            tYearStyle = ""

                                            tYearStyle = oDBdt.Rows(nLoopStyle).Item(tColYearStyle).ToString()
                                            tYearStyle = tYearStyle.Trim()

                                            If Len(tYearStyle) > 2 Then
                                                tYearStyle = Microsoft.VisualBasic.Right$(tYearStyle, 2)
                                            End If

                                            Dim tStyleImport As String = ""


                                            Select Case FNHSysVenderPramId.Text.ToUpper
                                                Case "HSC"
                                                    'tStyleImport = tStyleCode & tPlanningSeasonStyle & tYearStyle & "HSC"
                                                    tStyleImport = tStyleCode & "HSC"
                                                Case "HIP/DP"
                                                    tStyleImport = tStyleCode & "/HIP"
                                                Case "HIP/MS"
                                                    'tStyleImport = tStyleCode & tPlanningSeasonStyle & tYearStyle & "MS"
                                                    tStyleImport = tStyleCode & "MS"
                                                Case Else
                                                    'tStyleImport = tStyleCode & tPlanningSeasonStyle & tYearStyle
                                                    tStyleImport = tStyleCode
                                            End Select

                                            tSql = ""
                                            tSql = "SELECT TOP 1 A.FTStyleCode FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMStyle] AS A  WITH(NOLOCK) WHERE A.FTStyleCode = '" & HI.UL.ULF.rpQuoted(tStyleImport) & "';"

                                            If HI.Conn.SQLConn.GetField(tSql, HI.Conn.DB.DataBaseName.DB_MASTER, "") = "" Then
                                                '...validate new style import order
                                                '-------------------------------------------------------------------------------------------------------------------------------------
                                                If Not DBNull.Value.Equals(oDBdt.Rows(nLoopStyle).Item(tColMaterialStyle)) Then
                                                    If oDBdt.Rows(nLoopStyle).Item(tColMaterialStyle).ToString() <> "" Then
                                                        Dim tMaterialStyle As String = ""
                                                        tMaterialStyle = oDBdt.Rows(nLoopStyle).Item(tColMaterialStyle).ToString()
                                                        tMaterialStyle = tMaterialStyle.Trim()

                                                        Dim tStyleImportDesc As String = ""
                                                        'tStyleImportDesc = tMaterialStyle & "-" & tPlanningSeasonStyle & "-" & tYearStyle
                                                        tStyleImportDesc = tMaterialStyle

                                                        Dim tCustCodeStyle As String = ""
                                                        tCustCodeStyle = oDBdt.Rows(nLoopStyle).Item(tColCustStyle).ToString()
                                                        tCustCodeStyle = tCustCodeStyle.Trim()

                                                        Dim oRow As DataRow
                                                        If Not oDBdtStyle Is Nothing And oDBdtStyle.Rows.Count > 0 Then
                                                            Dim oDataRowExists As DataRow()
                                                            oDataRowExists = oDBdtStyle.Select("FTStyleCode = '" & HI.UL.ULF.rpQuoted(tStyleImport) & "'")
                                                            If oDataRowExists.Length > 0 Then
                                                                '...already exists
                                                            Else
                                                                oRow = oDBdtStyle.NewRow()
                                                                oRow.Item("FTStyleCode") = tStyleImport
                                                                oRow.Item("FTStyleName") = tStyleImportDesc
                                                                oDBdtStyle.Rows.Add(oRow)
                                                            End If

                                                        Else
                                                            oRow = oDBdtStyle.NewRow()
                                                            oRow.Item("FTStyleCode") = tStyleImport
                                                            oRow.Item("FTStyleName") = tStyleImportDesc
                                                            oDBdtStyle.Rows.Add(oRow)
                                                        End If

                                                    End If
                                                    '-------------------------------------------------------------------------------------------------------------------------------------

                                                End If

                                            End If

                                            tSql = ""
                                            tSql &= "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTStyle"
                                            tSql &= " (FTInsUser, FDInsDate, FTInsTime, FNHSysStyleId, FTStyleCode, FTStyleNameTH, FTStyleNameEN, FNHSysCustId, FNHSysSeasonId, FTStateActive) "
                                            tSql &= Environment.NewLine & "SELECT TOP 1  FTInsUser, FDInsDate, FTInsTime, FNHSysStyleId, FTStyleCode"
                                            tSql &= Environment.NewLine & "                                  , FTStyleNameTH, FTStyleNameEN, FNHSysCustId"
                                            tSql &= Environment.NewLine & "                                  , ISNULL((SELECT TOP 1 FNHSysSeasonId "
                                            tSql &= Environment.NewLine & "                                            FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMSeason] AS A  WITH(NOLOCK)"
                                            tSql &= Environment.NewLine & "                                            WHERE A.FTSeasonCode = N'" & HI.UL.ULF.rpQuoted(tPlanningSeasonStyle & tYearStyle) & "'"
                                            tSql &= Environment.NewLine & "                                            ), 0) AS  FNHSysSeasonId"
                                            tSql &= Environment.NewLine & "                                  , FTStateActive"
                                            tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMStyle] AS A  WITH(NOLOCK)"
                                            tSql &= Environment.NewLine & "WHERE A.FTStyleCode = N'" & HI.UL.ULF.rpQuoted(tStyleImport) & "'"
                                            tSql &= Environment.NewLine & "      AND NOT (A.FTStyleCode IN (SELECT FTStyleCode FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTStyle) )"

                                            HI.Conn.SQLConn.ExecuteNonQuery(tSql, Conn.DB.DataBaseName.DB_MERCHAN)

                                        End If

                                    End If

                                End If

                            End If

                        End If

                    End If

                    'Application.DoEvents()

                Next nLoopStyle

                If Not oDBdtStyle Is Nothing And oDBdtStyle.Rows.Count > 0 Then
                    oDBdtStyle.AcceptChanges()
                End If

                bRet = True

            End If

        Catch ex As Exception
            'If System.Diagnostics.Debugger.IsAttached = True Then
            MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
            'End If
        End Try

        Return bRet

    End Function

    Private Sub CheckCurrencyImport(ByVal oDBdtSrc As System.Data.DataTable)
        _tCurtImport = 0

        If Not oDBdtSrc Is Nothing And oDBdtSrc.Rows.Count > 0 And oDBdtSrc.Rows.Count > _nRowHeader Then
            Dim _TCol As Integer = 0
            Dim FoundColumn As Boolean = False
            For Each Col As DataColumn In oDBdtSrc.Columns
                _TCol = _TCol + 1

                If oDBdtSrc.Rows(0).Item(Col).ToString.Trim.ToLower = "Overall Result".ToLower() And oDBdtSrc.Rows(2).Item(Col).ToString.Trim.ToLower = "Trading Co Gross Unit Price".ToLower() Then
                    FoundColumn = True
                    Exit For
                End If
            Next

            If FoundColumn = True Then
                If oDBdtSrc.Columns.Count > _TCol Then
                    _tCurtImport = _TCol + 1
                End If

            End If
        End If
    End Sub

    Private Function W_PRCbSaveListOrderToTemp(ByVal oDBdtSrc As System.Data.DataTable) As Boolean
        '...catch raw data before confirm import factory order no.
        Dim bAppendSrcHeader As Boolean = True
        Dim bAppendSrcDetial As Boolean = True
        Dim bRet As Boolean = False
        Dim _FTCurrency As String = ""
        Try
            If Not oDBdtSrc Is Nothing And oDBdtSrc.Rows.Count > 0 And oDBdtSrc.Rows.Count > _nRowHeader Then

                '_oSplash.UpdateInformation(_tSplashText & Environment.NewLine & "กำลังดำเนินการเตรียมข้อมูลเพื่อนำเข้ารายการใบสั่งผลิต...")

                '...clear soure before import order temp
                tSql = ""
                tSql = "DELETE A"
                tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTImportOrderTemp] AS A"
                tSql &= Environment.NewLine & "WHERE A.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "';"

                If HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN) = True Then
                    'MsgBox("Delete data complete...", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
                End If

                tSql = ""
                tSql = "DELETE A"
                tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTImportOrderSizeBreakdownTemp] AS A"
                tSql &= Environment.NewLine & "WHERE A.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "';"

                If HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN) = True Then
                    'MsgBox("Delete data complete...", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
                End If

                Dim nLoopOrder As Integer
                Dim oStrBuilder As New System.Text.StringBuilder()

                If bAppendSrcHeader = True Then

                    For nLoopOrder = _nStartRowImportExcel To oDBdtSrc.Rows.Count - 1
                        Application.DoEvents()

                        oStrBuilder.Remove(0, oStrBuilder.Length)

                        Dim tStyle As String
                        Dim tMaterialStyle As String
                        Dim tPlanSeasonStyle As String
                        Dim tYearStyle As String
                        Dim tFTCountry As String
                        Dim tFTBuyer As String

                        tFTCountry = ""
                        'If Not DBNull.Value.Equals(oDBdtSrc.Rows(nLoopOrder).Item("F39")) Then
                        '    If oDBdtSrc.Rows(nLoopOrder).Item("F39").ToString() <> "" Then
                        '        tFTCountry = oDBdtSrc.Rows(nLoopOrder).Item("F39").ToString().Trim()
                        '    End If
                        'End If

                        If Not DBNull.Value.Equals(oDBdtSrc.Rows(nLoopOrder).Item("F36")) Then
                            If oDBdtSrc.Rows(nLoopOrder).Item("F36").ToString() <> "" Then
                                tFTCountry = oDBdtSrc.Rows(nLoopOrder).Item("F36").ToString().Trim()
                            End If
                        End If

                        tFTBuyer = ""
                        If Not DBNull.Value.Equals(oDBdtSrc.Rows(nLoopOrder).Item("F36")) Then
                            If oDBdtSrc.Rows(nLoopOrder).Item("F36").ToString() <> "" Then
                                tFTBuyer = oDBdtSrc.Rows(nLoopOrder).Item("F36").ToString().Trim()
                            End If
                        End If

                        tStyle = oDBdtSrc.Rows(nLoopOrder).Item("F15")
                        tMaterialStyle = oDBdtSrc.Rows(nLoopOrder).Item("F17")
                        tPlanSeasonStyle = oDBdtSrc.Rows(nLoopOrder).Item("F25")

                        If Not DBNull.Value.Equals(oDBdtSrc.Rows(nLoopOrder).Item("F26")) Then
                            If Len(oDBdtSrc.Rows(nLoopOrder).Item("F26")) = 4 Then
                                tYearStyle = Microsoft.VisualBasic.Right(oDBdtSrc.Rows(nLoopOrder).Item("F26"), 2)
                            ElseIf Len(oDBdtSrc.Rows(nLoopOrder).Item("F26")) = 2 Then
                                tYearStyle = oDBdtSrc.Rows(nLoopOrder).Item("F26")
                            End If
                        Else
                            tYearStyle = CStr(Now.Year)
                        End If

                        tStyle = tStyle.Trim() : tMaterialStyle = tMaterialStyle.Trim() : tPlanSeasonStyle = tPlanSeasonStyle.Trim() : tYearStyle = tYearStyle.Trim()

                        Dim tStyleCode As String

                        Select Case FNHSysVenderPramId.Text.ToUpper
                            Case "HSC"
                                'tStyleCode = tStyle & tPlanSeasonStyle & tYearStyle & "HSC"
                                tStyleCode = tStyle & "HSC"
                            Case "HIP/DP"
                                tStyleCode = tStyle & "/HIP"
                            Case "HIP/MS"
                                'tStyleCode = tStyle & tPlanSeasonStyle & tYearStyle & "MS"
                                tStyleCode = tStyle & "MS"
                            Case Else
                                'tStyleCode = tStyle & tPlanSeasonStyle & tYearStyle
                                tStyleCode = tStyle
                        End Select

                        Dim tStyleDesc As String
                        tStyleDesc = tMaterialStyle & "-" & tPlanSeasonStyle & "-" & tYearStyle

                        Dim objCulture As System.Globalization.CultureInfo = New System.Globalization.CultureInfo("en-US", True)

                        objCulture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy"
                        objCulture.DateTimeFormat.ShortTimePattern = "HH:mm:ss"

                        System.Threading.Thread.CurrentThread.CurrentCulture = objCulture

                        'Dim oDateF8 As DateTime
                        Dim tDateF8 As String

                        tDateF8 = CStr(oDBdtSrc.Rows(nLoopOrder).Item("F8"))

                        'If Not DBNull.Value.Equals(oDBdtSrc.Rows(nLoopOrder).Item("F8")) And CStr(oDBdtSrc.Rows(nLoopOrder).Item("F8")) <> "" Then
                        '    oDateF8 = DateTime.Parse(oDBdtSrc.Rows(nLoopOrder).Item("F8") & " 00:00:00 AM", CultureInfo.InvariantCulture)
                        '    tDateF8 = Format$(oDateF8, "dd/MM/yyyy")
                        '    'oDateF8 = DateTime.Parse(oDBdtSrc.Rows(nLoopOrder).Item("F8") & " 00:00:00 AM", System.Threading.Thread.CurrentThread.CurrentCulture)
                        '    'oDateF8 = DateTime.ParseExact(oDBdtSrc.Rows(nLoopOrder).Item("F8"), objCulture.DateTimeFormat.ShortDatePattern, System.Threading.Thread.CurrentThread.CurrentCulture)
                        '    'tDateF8 = HI.UL.ULDate.CheckDate(oDateF8)
                        'Else
                        '    tDateF8 = ""
                        'End If

                        'Dim oDateF9 As DateTime
                        Dim tDateF9 As String

                        tDateF9 = CStr(oDBdtSrc.Rows(nLoopOrder).Item("F9"))

                        'If Not DBNull.Value.Equals(oDBdtSrc.Rows(nLoopOrder).Item("F9")) And CStr(oDBdtSrc.Rows(nLoopOrder).Item("F9")) <> "" Then
                        '    oDateF9 = DateTime.Parse(oDBdtSrc.Rows(nLoopOrder).Item("F9") & " 00:00:00 AM", CultureInfo.InvariantCulture)
                        '    tDateF9 = Format$(oDateF9, "dd/MM/yyyy")
                        '    'oDateF9 = DateTime.Parse(oDBdtSrc.Rows(nLoopOrder).Item("F9") & " 00:00:00 AM", System.Threading.Thread.CurrentThread.CurrentCulture)
                        '    'oDateF9 = DateTime.ParseExact(oDBdtSrc.Rows(nLoopOrder).Item("F9"), objCulture.DateTimeFormat.ShortDatePattern, System.Threading.Thread.CurrentThread.CurrentCulture)
                        '    'tDateF9 = HI.UL.ULDate.CheckDate(oDateF9)
                        'Else
                        '    tDateF9 = ""
                        'End If

                        'Dim oDateF41 As DateTime
                        Dim tDateF41 As String

                        tDateF41 = CStr(oDBdtSrc.Rows(nLoopOrder).Item("F41"))

                        'If Not DBNull.Value.Equals(oDBdtSrc.Rows(nLoopOrder).Item("F41")) And CStr(oDBdtSrc.Rows(nLoopOrder).Item("F41")) <> "" Then
                        '    oDateF41 = DateTime.Parse(oDBdtSrc.Rows(nLoopOrder).Item("F41") & " 00:00:00 AM", CultureInfo.InvariantCulture)
                        '    tDateF41 = Format$(oDateF41, "dd/MM/yyyy")
                        '    'oDateF41 = DateTime.Parse(oDBdtSrc.Rows(nLoopOrder).Item("F41") & " 00:00:00 AM", System.Threading.Thread.CurrentThread.CurrentCulture)
                        '    'oDateF41 = DateTime.ParseExact(oDBdtSrc.Rows(nLoopOrder).Item("F41"), objCulture.DateTimeFormat.ShortDatePattern, System.Threading.Thread.CurrentThread.CurrentCulture)
                        '    'tDateF41 = HI.UL.ULDate.CheckDate(oDateF41)
                        'Else
                        '    tDateF41 = ""
                        'End If

                        'Dim oDateF42 As DateTime
                        Dim tDateF42 As String

                        tDateF42 = CStr(oDBdtSrc.Rows(nLoopOrder).Item("F42"))

                        If _tCurtImport > 0 Then
                            _FTCurrency = oDBdtSrc.Rows(nLoopOrder).Item("F" & _tCurtImport.ToString & "").ToString.Trim()
                        Else
                            _FTCurrency = ""
                        End If

                        '...record import order to temp
                        oStrBuilder.AppendLine("INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTImportOrderTemp] ([FTUserLogin]")
                        oStrBuilder.AppendLine("	                                                     ,[FNRowImport]")
                        oStrBuilder.AppendLine("                                                         ,[FTPONo]")
                        oStrBuilder.AppendLine("						                                 ,[FTPOTrading]")
                        oStrBuilder.AppendLine("			                                             ,[FTPOItem]")
                        oStrBuilder.AppendLine("							                             ,[FTPOCreateDate]")
                        oStrBuilder.AppendLine("							                             ,[FTOrderDate]")
                        oStrBuilder.AppendLine("							                             ,[FTStyle]")
                        oStrBuilder.AppendLine("						                                 ,[FTStyleDesc]")
                        oStrBuilder.AppendLine("							                             ,[FNHSysMainCategoryId]")
                        oStrBuilder.AppendLine("							                             ,[FTMainCategoryDesc]")
                        oStrBuilder.AppendLine("							                             ,[FNHSysPlantId]")
                        oStrBuilder.AppendLine("							                             ,[FTPlantDesc]")
                        oStrBuilder.AppendLine("							                             ,[FNHSysBuyGrpId]")
                        oStrBuilder.AppendLine("							                             ,[FTBuyGrpNameDesc]")
                        oStrBuilder.AppendLine("							                             ,[FTMaterial]")
                        oStrBuilder.AppendLine("							                             ,[FTMaterialDesc]")
                        oStrBuilder.AppendLine("							                             ,[FNHSysGenderId]")
                        oStrBuilder.AppendLine("						                                 ,[FTGenderNameDesc]")
                        oStrBuilder.AppendLine("			                                             ,[FTPlanningSeason]")
                        oStrBuilder.AppendLine("							                             ,[FTYear]")
                        oStrBuilder.AppendLine("                                                         ,[FNHSysBuyerId]")
                        oStrBuilder.AppendLine("			                                             ,[FTCustCode]")
                        oStrBuilder.AppendLine("							                             ,[FTCustDesc]")
                        oStrBuilder.AppendLine("                                                         ,[FNHSysCountryId]")
                        oStrBuilder.AppendLine("				                                         ,[FTCountry]")
                        oStrBuilder.AppendLine("						                                 ,[FDShipDateOrginal]")
                        oStrBuilder.AppendLine("							                             ,[FDShipDate]")
                        oStrBuilder.AppendLine("                                                         ,[FNHSysMerTeamId]")
                        oStrBuilder.AppendLine("                                                         ,[FNHSysProdTypeId],[FTCurrency],[FNHSysProvinceId])")
                        oStrBuilder.AppendLine(String.Format("VALUES({0}", "'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"))
                        oStrBuilder.AppendLine(String.Format("      ,{0}", nLoopOrder))
                        oStrBuilder.AppendLine(String.Format("      ,{0}", "'" & HI.UL.ULF.rpQuoted(oDBdtSrc.Rows(nLoopOrder).Item("F5")) & "'"))
                        oStrBuilder.AppendLine(String.Format("      ,{0}", "'" & HI.UL.ULF.rpQuoted(oDBdtSrc.Rows(nLoopOrder).Item("F6")) & "'"))
                        oStrBuilder.AppendLine(String.Format("      ,{0}", "'" & HI.UL.ULF.rpQuoted(oDBdtSrc.Rows(nLoopOrder).Item("F7")) & "'"))
                      

                        If tDateF8 <> "" Then
                            oStrBuilder.AppendLine(String.Format("      ,'{0}'", HI.UL.ULDate.ConvertEnDB(tDateF8)))
                        Else
                            oStrBuilder.AppendLine(String.Format("      ,'{0}'", ""))
                        End If

                        If tDateF9 <> "" Then
                            oStrBuilder.AppendLine(String.Format("      ,'{0}'", HI.UL.ULDate.ConvertEnDB(tDateF9)))
                        Else
                            oStrBuilder.AppendLine(String.Format("      ,'{0}'", ""))
                        End If

                        oStrBuilder.AppendLine(String.Format("      ,{0}", "'" & HI.UL.ULF.rpQuoted(tStyleCode) & "'"))
                        oStrBuilder.AppendLine(String.Format("      ,{0}", "'" & HI.UL.ULF.rpQuoted(tStyleDesc) & "'"))
                        oStrBuilder.AppendLine(String.Format("      ,{0}", "(SELECT TOP 1 L1.FNHSysMainCategoryId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMainCategoryType] AS L1 WITH(NOLOCK) WHERE L1.FTMainCategoryCode = N'" & HI.UL.ULF.rpQuoted(IIf(IsDBNull(oDBdtSrc.Rows(nLoopOrder).Item("F10")), "", oDBdtSrc.Rows(nLoopOrder).Item("F10"))) & "')"))
                        oStrBuilder.AppendLine(String.Format("      ,{0}", "'" & HI.UL.ULF.rpQuoted(oDBdtSrc.Rows(nLoopOrder).Item("F10")) & "'"))
                        oStrBuilder.AppendLine(String.Format("      ,{0}", "(SELECT TOP 1 A.FNHSysPlantId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMPlant] AS A WITH(NOLOCK) WHERE A.FTPlantCode = N'" & HI.UL.ULF.rpQuoted(oDBdtSrc.Rows(nLoopOrder).Item("F31")) & "')"))
                        oStrBuilder.AppendLine(String.Format("      ,{0}", "'" & HI.UL.ULF.rpQuoted(oDBdtSrc.Rows(nLoopOrder).Item("F32")) & "'"))
                        oStrBuilder.AppendLine(String.Format("      ,{0}", "(SELECT TOP 1 A.FNHSysBuyGrpId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMBuyGrp] AS A WITH(NOLOCK) WHERE A.FTBuyGrpCode = N'" & oDBdtSrc.Rows(nLoopOrder).Item("F43") & "')"))
                        oStrBuilder.AppendLine(String.Format("      ,{0}", "'" & HI.UL.ULF.rpQuoted(oDBdtSrc.Rows(nLoopOrder).Item("F44")) & "'"))
                        oStrBuilder.AppendLine(String.Format("      ,{0}", "'" & HI.UL.ULF.rpQuoted(oDBdtSrc.Rows(nLoopOrder).Item("F16")) & "'"))
                        oStrBuilder.AppendLine(String.Format("      ,{0}", "'" & HI.UL.ULF.rpQuoted(oDBdtSrc.Rows(nLoopOrder).Item("F17")) & "'"))
                        oStrBuilder.AppendLine(String.Format("      ,{0}", "(SELECT TOP 1 FNHSysGenderId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMGender] AS A  WITH(NOLOCK) WHERE A.FTGenderCode = N'" & HI.UL.ULF.rpQuoted(oDBdtSrc.Rows(nLoopOrder).Item("F18")) & "' OR A.FTGenderNameEN = N'" & HI.UL.ULF.rpQuoted(oDBdtSrc.Rows(nLoopOrder).Item("F18")) & "' OR A.FTGenderNameTH = N'" & HI.UL.ULF.rpQuoted(oDBdtSrc.Rows(nLoopOrder).Item("F18")) & "')"))
                        oStrBuilder.AppendLine(String.Format("      ,{0}", "'" & HI.UL.ULF.rpQuoted(oDBdtSrc.Rows(nLoopOrder).Item("F18")) & "'"))
                        oStrBuilder.AppendLine(String.Format("      ,{0}", "'" & HI.UL.ULF.rpQuoted(oDBdtSrc.Rows(nLoopOrder).Item("F25")) & "'"))
                        oStrBuilder.AppendLine(String.Format("      ,{0}", "'" & HI.UL.ULF.rpQuoted(oDBdtSrc.Rows(nLoopOrder).Item("F26")) & "'"))
                        oStrBuilder.AppendLine(String.Format("      ,{0}", "(SELECT ISNULL((SELECT A.FNHSysBuyerId " & _
                                                                                           "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMBuyer] AS A WITH(NOLOCK) " & _
                                                                                           "WHERE A.FTBuyerCode = N'" & HI.UL.ULF.rpQuoted(tFTBuyer) & "'), ISNULL((SELECT L1.FNHSysBuyerId " & _
                                                                                           "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMBuyer] AS L1 WITH(NOLOCK) " & _
                                                                                           "WHERE L1.FTBuyerCode = N'-'), NULL)))"))
                        oStrBuilder.AppendLine(String.Format("      ,{0}", "'" & HI.UL.ULF.rpQuoted(IIf(IsDBNull(oDBdtSrc.Rows(nLoopOrder).Item("F36")), "", oDBdtSrc.Rows(nLoopOrder).Item("F36"))) & "'"))
                        oStrBuilder.AppendLine(String.Format("      ,{0}", "'" & HI.UL.ULF.rpQuoted(IIf(IsDBNull(oDBdtSrc.Rows(nLoopOrder).Item("F37")), "", oDBdtSrc.Rows(nLoopOrder).Item("F37"))) & "'"))

                        oStrBuilder.AppendLine(String.Format("      ,{0}", "CASE WHEN N'" & HI.UL.ULF.rpQuoted(tFTCountry.Trim()) & "'  ='#' THEN (SELECT TOP 1 A.FNHSysCountryId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMPlant] AS A WITH(NOLOCK) WHERE A.FTPlantCode = N'" & HI.UL.ULF.rpQuoted(oDBdtSrc.Rows(nLoopOrder).Item("F31")) & "')  ELSE (SELECT ISNULL((SELECT TOP 1 L1.FNHSysCountryId " & _
                                                                                           "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCMMProvince] AS L1 WITH(NOLOCK) " & _
                                                                                           "WHERE  L1.FTProvinceCode =  N'" & HI.UL.ULF.rpQuoted(tFTCountry) & "'),NULL)) END"))

                        oStrBuilder.AppendLine(String.Format("      ,{0}", "'" & HI.UL.ULF.rpQuoted(IIf(IsDBNull(oDBdtSrc.Rows(nLoopOrder).Item("F39")), "", oDBdtSrc.Rows(nLoopOrder).Item("F39"))) & "'"))

                        If tDateF41 <> "" Then
                            oStrBuilder.AppendLine(String.Format("     ,'{0}'", HI.UL.ULDate.ConvertEnDB(tDateF41)))
                        Else
                            oStrBuilder.AppendLine(String.Format("     ,'{0}'", ""))
                        End If

                        If tDateF42 <> "" Then
                            oStrBuilder.AppendLine(String.Format("      ,'{0}'", HI.UL.ULDate.ConvertEnDB(tDateF42)))
                        Else
                            oStrBuilder.AppendLine(String.Format("      ,'{0}'", ""))
                        End If

                        oStrBuilder.AppendLine(String.Format("      ,{0}", "(SELECT ISNULL((SELECT TOP 1 A.FNHSysMerTeamId " & _
                                                                                           "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMerTeam] AS A WITH(NOLOCK)" & _
                                                                                           "WHERE A.FTMerTeamCode = N'" & HI.UL.ULF.rpQuoted(IIf(IsDBNull(oDBdtSrc.Rows(nLoopOrder).Item("F14")), "", oDBdtSrc.Rows(nLoopOrder).Item("F14"))) & "'),NULL))"))
                        oStrBuilder.AppendLine(String.Format("      ,{0}", "(SELECT ISNULL((SELECT TOP 1 A.FNHSysProdTypeId " & _
                                                                            "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMProductType] AS A WITH(NOLOCK)" & _
                                                                            "WHERE (A.FTProdTypeNameEN = N'" & HI.UL.ULF.rpQuoted(IIf(IsDBNull(oDBdtSrc.Rows(nLoopOrder).Item("F11")), "", oDBdtSrc.Rows(nLoopOrder).Item("F11"))) & "' OR A.FTProdTypeNameTH = N'" & HI.UL.ULF.rpQuoted(IIf(IsDBNull(oDBdtSrc.Rows(nLoopOrder).Item("F11")), "", oDBdtSrc.Rows(nLoopOrder).Item("F11"))) & "')),NULL))" & ""))
                        oStrBuilder.AppendLine(String.Format("      ,'{0}'", _FTCurrency) & "")
                        oStrBuilder.AppendLine(String.Format("      ,{0}", "CASE WHEN N'" & HI.UL.ULF.rpQuoted(tFTCountry.Trim()) & "'  ='#' THEN (SELECT TOP 1 A.FNHSysProvinceId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMPlant] AS A WITH(NOLOCK) WHERE A.FTPlantCode = N'" & HI.UL.ULF.rpQuoted(oDBdtSrc.Rows(nLoopOrder).Item("F31")) & "')  ELSE (SELECT ISNULL((SELECT TOP 1 L1.FNHSysProvinceId " & _
                                                                                         "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCMMProvince] AS L1 WITH(NOLOCK) " & _
                                                                                         "WHERE  L1.FTProvinceCode =  N'" & HI.UL.ULF.rpQuoted(tFTCountry) & "'),NULL)) END )"))
                        tSql = ""
                        tSql = oStrBuilder.ToString()

                        If HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN) = True Then
                            'MsgBox("Execute data complete...", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
                        Else
                            'MsgBox("Execute batch file not complete !!!", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
                        End If

                    Next nLoopOrder

                End If

                '...represent record colorway size breakdown
                If bAppendSrcDetial = True Then
                    '...add row by colorway / size breakdown
                    Dim tColPONo As String = "F5"
                    Dim tColPOTrading As String = "F6"
                    Dim tColPOItem As String = "F7"

                    Dim tColMatColor As String = "F16"
                    Dim tColMatColorDesc As String = "F23"
                    Dim tColShipMode As String = "F46"
                    Dim tColUom As String = "F47"

                    Dim nLoopSizeBreakdown As Integer

                    For nLoopSizeBreakdown = _nStartRowImportExcel To oDBdtSrc.Rows.Count - 1
                        'Application.DoEvents()

                        '...Column Extension Match Size Breakdown
                        '...col 1 : Total Item Qty
                        '...col 2 : NET Unit Price
                        '...col 3 : Trading Co Net Unit Price
                        '...col 4 : Gross Unit Price
                        '...col 5 : Tranding Co Gross Unit Price

                        Dim tDateF42 As String
                        tDateF42 = CStr(oDBdtSrc.Rows(nLoopSizeBreakdown).Item("F42"))

                        Dim tMatColor As String
                        tMatColor = oDBdtSrc.Rows(nLoopSizeBreakdown).Item(tColMatColor)

                        Dim nPosHyphen As Integer
                        nPosHyphen = InStr(tMatColor, _tHyphenSign)

                        If nPosHyphen > 0 Then
                            tMatColor = Microsoft.VisualBasic.Mid$(tMatColor, nPosHyphen + 1, Len(tMatColor))

                            Dim nCntColMatchSize As Integer
                            Dim nLoopColMatchSize As Integer
                            Dim tMatchSize As String

                            nCntColMatchSize = oDBdtSrc.Columns.Count
                            nLoopColMatchSize = _nStartColImportSizeValExcel

                            tMatchSize = oDBdtSrc.Rows(_nRowHeader - 2).Item(_nStartColImportSizeValExcel).ToString()
                            tMatchSize = tMatchSize.Trim()

                            '...validate map size ex. XXL ==> 2XL, XXXL ==> 3XL, XXXXL ==> 4XL
                            If Not oDBdtMapSize Is Nothing AndAlso oDBdtMapSize.Rows.Count > 0 Then
                                Dim tMapSizeExtension As String = ""
                                Dim oDataRow As DataRow()
                                Try
                                    oDataRow = oDBdtMapSize.Select("FTMapSize = '" & HI.UL.ULF.rpQuoted(tMatchSize) & "'")
                                    If oDataRow.Length > 0 Then

                                        If oDataRow(0)("FTMapSizeExtension") <> "" Then
                                            tMatchSize = oDataRow(0)("FTMapSizeExtension").ToString()
                                        End If

                                    Else
                                        '/* ถ้าไม่มีการ Map Size ไว้ใน TMERMMapSize [MERCHAN] ให้ทำการตรวจสอบว่่า มีการ Map ด้วยวิธี Manual ผ่าน หน้าจอ โปรแกรม ก่อนการ Import Order หรือไม่ */
                                        If Not _oDBdtUserImportMapSizeManual Is Nothing AndAlso _oDBdtUserImportMapSizeManual.Rows.Count > 0 Then
                                            Dim oDataRowMapSizeManual As System.Data.DataRow()
                                            Try
                                                oDataRowMapSizeManual = _oDBdtUserImportMapSizeManual.Select("FTSizeCodeNotExists = '" & HI.UL.ULF.rpQuoted(tMatchSize) & "'")
                                                If oDataRowMapSizeManual.Length > 0 Then

                                                    If oDataRowMapSizeManual(0)("FTMapSizeExtend") <> "" Then
                                                        tMatchSize = oDataRowMapSizeManual(0)("FTMapSizeExtend").ToString()
                                                    End If

                                                End If

                                            Catch ex As Exception
                                            End Try

                                        End If

                                    End If

                                Catch ex As Exception
                                End Try

                            Else
                                '/* ถ้าไม่มีการ Map Size ไว้ใน TMERMMapSize [MERCHAN] ให้ทำการตรวจสอบว่่า มีการ Map ด้วยวิธี Manual ผ่าน หน้าจอ โปรแกรม ก่อนการ Import Order หรือไม่ */
                                If Not _oDBdtUserImportMapSizeManual Is Nothing AndAlso _oDBdtUserImportMapSizeManual.Rows.Count > 0 Then
                                    Dim oDataRowMapSizeManual As System.Data.DataRow()

                                    Try

                                        oDataRowMapSizeManual = _oDBdtUserImportMapSizeManual.Select("FTSizeCodeNotExists = '" & HI.UL.ULF.rpQuoted(tMatchSize) & "'")

                                        If oDataRowMapSizeManual.Length > 0 Then

                                            If oDataRowMapSizeManual(0)("FTMapSizeExtend") <> "" Then

                                                tMatchSize = oDataRowMapSizeManual(0)("FTMapSizeExtend").ToString()

                                            End If

                                        End If

                                    Catch ex As Exception
                                    End Try

                                End If

                            End If

                            Do While tMatchSize <> "" And UCase(tMatchSize) <> UCase(_tTextMatchSizeTerminate) And (nLoopColMatchSize < nCntColMatchSize)
                                Dim tStyle As String
                                Dim tMaterialStyle As String
                                Dim tPlanSeasonStyle As String
                                Dim tYearStyle As String

                                tStyle = oDBdtSrc.Rows(nLoopSizeBreakdown).Item("F15")
                                tMaterialStyle = oDBdtSrc.Rows(nLoopSizeBreakdown).Item("F17")
                                tPlanSeasonStyle = oDBdtSrc.Rows(nLoopSizeBreakdown).Item("F25")

                                tYearStyle = Microsoft.VisualBasic.Right$(CStr(Now.Year), 4)
                                If Not DBNull.Value.Equals(oDBdtSrc.Rows(nLoopSizeBreakdown).Item("F26")) Then
                                    If Len(oDBdtSrc.Rows(nLoopSizeBreakdown).Item("F26")) = 4 Then
                                        tYearStyle = Microsoft.VisualBasic.Right(oDBdtSrc.Rows(nLoopSizeBreakdown).Item("F26"), 2)
                                    ElseIf Len(oDBdtSrc.Rows(nLoopSizeBreakdown).Item("F26")) = 2 Then
                                        tYearStyle = oDBdtSrc.Rows(nLoopSizeBreakdown).Item("F26")
                                    End If
                                Else
                                    tYearStyle = Microsoft.VisualBasic.Right$(CStr(Now.Year), 2)
                                End If

                                Dim tStyleCode As String
                                ' tStyleCode = tStyle & tPlanSeasonStyle & tYearStyle

                                Select Case FNHSysVenderPramId.Text.ToUpper
                                    Case "HSC"
                                        'tStyleCode = tStyle & tPlanSeasonStyle & tYearStyle & "HSC"
                                        tStyleCode = tStyle & "HSC"
                                    Case "HIP/DP"
                                        tStyleCode = tStyle & "/HIP"
                                    Case "HIP/MS"
                                        'tStyleCode = tStyle & tPlanSeasonStyle & tYearStyle & "MS"
                                        tStyleCode = tStyle & "MS"
                                    Case Else
                                        'tStyleCode = tStyle & tPlanSeasonStyle & tYearStyle
                                        tStyleCode = tStyle
                                End Select

                                Dim nFNQuantity As Double                  '...Col ที่ 1 : ของไซส์
                                Dim tFTNetUnitPrice As String               '...Col ที่ 2 : ของไซส์
                                Dim tFTTradingCoNetUnitPrice As String      '...Col ที่ 3 : ของไซส์
                                Dim tFTGrossUnitPrice As String             '...Col ที่ 4 : ของไซส์
                                Dim tFTTradingCoGrossUnitPrice As String    '...Col ที่ 5 : ของไซส์

                                Dim nColIncMatSize As Integer
                                nColIncMatSize = nLoopColMatchSize

                                If Not DBNull.Value.Equals(oDBdtSrc.Rows(nLoopSizeBreakdown).Item(nColIncMatSize)) Then

                                    If CStr(oDBdtSrc.Rows(nLoopSizeBreakdown).Item(nColIncMatSize)).Trim() <> "" Then
                                        nFNQuantity = Double.Parse(oDBdtSrc.Rows(nLoopSizeBreakdown).Item(nColIncMatSize), NumberStyles.Currency)
                                    Else
                                        nFNQuantity = 0
                                    End If
                                Else
                                    nFNQuantity = 0
                                End If

                                If Not DBNull.Value.Equals(oDBdtSrc.Rows(nLoopSizeBreakdown).Item(nColIncMatSize + 1)) Then
                                    tFTNetUnitPrice = oDBdtSrc.Rows(nLoopSizeBreakdown).Item(nColIncMatSize + 1)

                                    Dim _tmp As String = tFTNetUnitPrice
                                    tFTNetUnitPrice = ""
                                    For Each C As Char In _tmp.ToCharArray

                                        If IsNumeric(C) Or C = "." Then
                                            tFTNetUnitPrice = tFTNetUnitPrice & C
                                        End If
                                    Next

                                    tFTNetUnitPrice = tFTNetUnitPrice.Trim()

                                    If IsNumeric(tFTNetUnitPrice) = False Then
                                        tFTNetUnitPrice = ""
                                    End If

                                Else
                                    tFTNetUnitPrice = ""
                                End If

                                If Not DBNull.Value.Equals(oDBdtSrc.Rows(nLoopSizeBreakdown).Item(nColIncMatSize + 2)) Then
                                    tFTTradingCoNetUnitPrice = oDBdtSrc.Rows(nLoopSizeBreakdown).Item(nColIncMatSize + 2)

                                    Dim _tmp As String = tFTTradingCoNetUnitPrice
                                    tFTTradingCoNetUnitPrice = ""
                                    For Each C As Char In _tmp.ToCharArray

                                        If IsNumeric(C) Or C = "." Then
                                            tFTTradingCoNetUnitPrice = tFTTradingCoNetUnitPrice & C
                                        End If
                                    Next

                                    tFTTradingCoNetUnitPrice = tFTTradingCoNetUnitPrice.Trim()

                                    If IsNumeric(tFTTradingCoNetUnitPrice) = False Then
                                        tFTTradingCoNetUnitPrice = ""
                                    End If

                                Else
                                    tFTTradingCoNetUnitPrice = ""
                                End If

                                If Not DBNull.Value.Equals(oDBdtSrc.Rows(nLoopSizeBreakdown).Item(nColIncMatSize + 3)) Then
                                    tFTGrossUnitPrice = oDBdtSrc.Rows(nLoopSizeBreakdown).Item(nColIncMatSize + 3)

                                    Dim _tmp As String = tFTGrossUnitPrice
                                    tFTGrossUnitPrice = ""
                                    For Each C As Char In _tmp.ToCharArray

                                        If IsNumeric(C) Or C = "." Then
                                            tFTGrossUnitPrice = tFTGrossUnitPrice & C
                                        End If
                                    Next

                                    tFTGrossUnitPrice = tFTGrossUnitPrice.Trim()

                                    If IsNumeric(tFTGrossUnitPrice) = False Then
                                        tFTGrossUnitPrice = ""
                                    End If

                                Else
                                    tFTGrossUnitPrice = ""
                                End If

                                If Not DBNull.Value.Equals(oDBdtSrc.Rows(nLoopSizeBreakdown).Item(nColIncMatSize + 4)) Then
                                    tFTTradingCoGrossUnitPrice = oDBdtSrc.Rows(nLoopSizeBreakdown).Item(nColIncMatSize + 4)

                                    Dim _tmp As String = tFTTradingCoGrossUnitPrice
                                    tFTTradingCoGrossUnitPrice = ""
                                    For Each C As Char In _tmp.ToCharArray

                                        If IsNumeric(C) Or C = "." Then
                                            tFTTradingCoGrossUnitPrice = tFTTradingCoGrossUnitPrice & C
                                        End If
                                    Next

                                    tFTTradingCoGrossUnitPrice = tFTTradingCoGrossUnitPrice.Trim()

                                    If IsNumeric(tFTTradingCoGrossUnitPrice) = False Then
                                        tFTTradingCoGrossUnitPrice = ""
                                    End If

                                Else
                                    tFTTradingCoGrossUnitPrice = ""
                                End If

                                Application.DoEvents()

                                tSql = ""
                                tSql = "SELECT TOP 1 A.FTSizeBreakdownCode"
                                tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTImportOrderSizeBreakdownTemp] AS A WITH(NOLOCK)"
                                tSql &= Environment.NewLine & "WHERE A.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                                tSql &= Environment.NewLine & "      AND A.FNRowImport = " & nLoopSizeBreakdown
                                tSql &= Environment.NewLine & "      AND A.FTPONo = N'" & HI.UL.ULF.rpQuoted(oDBdtSrc.Rows(nLoopSizeBreakdown).Item(tColPONo)) & "'"
                                tSql &= Environment.NewLine & "      AND A.FTPOTrading = N'" & HI.UL.ULF.rpQuoted(oDBdtSrc.Rows(nLoopSizeBreakdown).Item(tColPOTrading)) & "'"
                                tSql &= Environment.NewLine & "      AND A.FTPOItem = N'" & HI.UL.ULF.rpQuoted(oDBdtSrc.Rows(nLoopSizeBreakdown).Item(tColPOItem)) & "'"
                                tSql &= Environment.NewLine & "      AND A.FTStyle = N'" & HI.UL.ULF.rpQuoted(tStyleCode) & "'"
                                tSql &= Environment.NewLine & "      AND A.FTColorwayCode = N'" & HI.UL.ULF.rpQuoted(tMatColor) & "'"
                                tSql &= Environment.NewLine & "      AND A.FTShipDate = N'" & HI.UL.ULDate.ConvertEnDB(tDateF42) & "'"
                                tSql &= Environment.NewLine & "      AND A.FTSizeBreakdownCode = N'" & HI.UL.ULF.rpQuoted(tMatchSize) & "';"
                                If HI.Conn.SQLConn.GetField(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN, "") <> "" Then
                                    '...exists in system import order temp and quantity greater than zero / update record
                                    oStrBuilder.Remove(0, oStrBuilder.Length)

                                    If nFNQuantity > 0 Then

                                        oStrBuilder.AppendLine("UPDATE A")
                                        oStrBuilder.AppendLine(String.Format("SET A.[FNQuantity] = {0}", nFNQuantity))
                                        oStrBuilder.AppendLine(String.Format("   ,A.[FTNetUnitPrice] = {0}", "N'" & HI.UL.ULF.rpQuoted(tFTNetUnitPrice) & "'"))
                                        oStrBuilder.AppendLine(String.Format("   ,A.[FTTradingCoNetUnitPrice] = {0}", "N'" & HI.UL.ULF.rpQuoted(tFTTradingCoNetUnitPrice) & "'"))
                                        oStrBuilder.AppendLine(String.Format("   ,A.[FTGrossUnitPrice] = {0}", "N'" & HI.UL.ULF.rpQuoted(tFTGrossUnitPrice) & "'"))
                                        oStrBuilder.AppendLine(String.Format("   ,A.[FTTradingCoGrossUnitPrice] = {0}", "N'" & HI.UL.ULF.rpQuoted(tFTTradingCoGrossUnitPrice) & "'"))
                                        oStrBuilder.AppendLine("FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTImportOrderSizeBreakdownTemp] AS A")
                                        oStrBuilder.AppendLine(String.Format("WHERE A.[FTUserLogin] = {0}", "N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"))
                                        oStrBuilder.AppendLine(String.Format("      AND A.[FNRowImport] = {0}", nLoopSizeBreakdown))
                                        oStrBuilder.AppendLine(String.Format("      AND A.[FTPONo] = {0}", "N'" & HI.UL.ULF.rpQuoted(oDBdtSrc.Rows(nLoopSizeBreakdown).Item(tColPONo)) & "'"))
                                        oStrBuilder.AppendLine(String.Format("      AND A.[FTPOTrading] = {0}", "N'" & HI.UL.ULF.rpQuoted(oDBdtSrc.Rows(nLoopSizeBreakdown).Item(tColPOTrading)) & "'"))
                                        oStrBuilder.AppendLine(String.Format("      AND A.[FTPOItem] = {0}", "N'" & HI.UL.ULF.rpQuoted(oDBdtSrc.Rows(nLoopSizeBreakdown).Item(tColPOItem)) & "'"))
                                        oStrBuilder.AppendLine(String.Format("      AND A.[FTStyle] = {0}", "N'" & HI.UL.ULF.rpQuoted(tStyleCode) & "'"))
                                        oStrBuilder.AppendLine(String.Format("      AND A.[FTColorwayCode] = {0}", "N'" & HI.UL.ULF.rpQuoted(tMatColor) & "'"))
                                        oStrBuilder.AppendLine(String.Format("      AND A.[FTShipDate] = {0}", "N'" & HI.UL.ULDate.ConvertEnDB(tDateF42) & "'"))
                                        oStrBuilder.AppendLine(String.Format("      AND A.[FTSizeBreakdownCode] = {0}", "N'" & HI.UL.ULF.rpQuoted(tMatchSize) & "'"))

                                    End If

                                Else
                                    oStrBuilder.Remove(0, oStrBuilder.Length)

                                    oStrBuilder.AppendLine("INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTImportOrderSizeBreakdownTemp]")
                                    oStrBuilder.AppendLine("           ([FTUserLogin]")
                                    oStrBuilder.AppendLine("           ,[FNRowImport]")
                                    oStrBuilder.AppendLine("           ,[FTPONo]")
                                    oStrBuilder.AppendLine("           ,[FTPOTrading]")
                                    oStrBuilder.AppendLine("           ,[FTPOItem]")
                                    oStrBuilder.AppendLine("           ,[FTStyle]")
                                    oStrBuilder.AppendLine("           ,[FTColorwayCode]")
                                    oStrBuilder.AppendLine("           ,[FTColorwayDesc]")
                                    oStrBuilder.AppendLine("           ,[FTSizeBreakdownCode]")
                                    oStrBuilder.AppendLine("           ,[FTSizeBreakdownDesc]")
                                    oStrBuilder.AppendLine("           ,[FTMode]")
                                    oStrBuilder.AppendLine("           ,[FTUom]")
                                    oStrBuilder.AppendLine("           ,[FNQuantity]")
                                    oStrBuilder.AppendLine("           ,[FTNetUnitPrice]")
                                    oStrBuilder.AppendLine("           ,[FTTradingCoNetUnitPrice]")
                                    oStrBuilder.AppendLine("           ,[FTGrossUnitPrice]")
                                    oStrBuilder.AppendLine("           ,[FTTradingCoGrossUnitPrice],[FTShipDate])")
                                    oStrBuilder.AppendLine("     VALUES(")
                                    oStrBuilder.AppendLine(String.Format("            {0}", "N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"))
                                    oStrBuilder.AppendLine(String.Format("           ,{0}", nLoopSizeBreakdown))
                                    oStrBuilder.AppendLine(String.Format("           ,{0}", "N'" & HI.UL.ULF.rpQuoted(oDBdtSrc.Rows(nLoopSizeBreakdown).Item(tColPONo)) & "'"))
                                    oStrBuilder.AppendLine(String.Format("           ,{0}", "N'" & HI.UL.ULF.rpQuoted(oDBdtSrc.Rows(nLoopSizeBreakdown).Item(tColPOTrading)) & "'"))
                                    oStrBuilder.AppendLine(String.Format("           ,{0}", "N'" & HI.UL.ULF.rpQuoted(oDBdtSrc.Rows(nLoopSizeBreakdown).Item(tColPOItem)) & "'"))
                                    oStrBuilder.AppendLine(String.Format("           ,{0}", "N'" & HI.UL.ULF.rpQuoted(tStyleCode) & "'"))
                                    oStrBuilder.AppendLine(String.Format("           ,{0}", "N'" & tMatColor & "'"))
                                    oStrBuilder.AppendLine(String.Format("           ,{0}", "N'" & HI.UL.ULF.rpQuoted(oDBdtSrc.Rows(nLoopSizeBreakdown).Item(tColMatColorDesc)) & "'"))
                                    oStrBuilder.AppendLine(String.Format("           ,{0}", "N'" & HI.UL.ULF.rpQuoted(tMatchSize) & "'"))
                                    oStrBuilder.AppendLine(String.Format("           ,{0}", "N'" & HI.UL.ULF.rpQuoted(tMatchSize) & "'"))
                                    oStrBuilder.AppendLine(String.Format("           ,{0}", "N'" & HI.UL.ULF.rpQuoted(oDBdtSrc.Rows(nLoopSizeBreakdown).Item(tColShipMode)) & "'"))
                                    oStrBuilder.AppendLine(String.Format("           ,{0}", "N'" & HI.UL.ULF.rpQuoted("PCS") & "'"))
                                    'oStrBuilder.AppendLine(String.Format("           ,{0}", "N'" & HI.UL.ULF.rpQuoted(oDBdtSrc.Rows(nLoopSizeBreakdown).Item(tColUom)) & "'"))
                                    oStrBuilder.AppendLine(String.Format("           ,{0}", nFNQuantity))
                                    oStrBuilder.AppendLine(String.Format("           ,{0}", "N'" & HI.UL.ULF.rpQuoted(IIf(IsDBNull(tFTNetUnitPrice), "", tFTNetUnitPrice)) & "'"))
                                    oStrBuilder.AppendLine(String.Format("           ,{0}", "N'" & HI.UL.ULF.rpQuoted(IIf(IsDBNull(tFTTradingCoNetUnitPrice), "", tFTTradingCoNetUnitPrice)) & "'"))
                                    oStrBuilder.AppendLine(String.Format("           ,{0}", "N'" & HI.UL.ULF.rpQuoted(IIf(IsDBNull(tFTGrossUnitPrice), "", tFTGrossUnitPrice)) & "'"))
                                    oStrBuilder.AppendLine(String.Format("           ,{0}", "N'" & HI.UL.ULF.rpQuoted(IIf(IsDBNull(tFTTradingCoGrossUnitPrice), "", tFTTradingCoGrossUnitPrice)) & "'"))
                                    oStrBuilder.AppendLine(String.Format("           ,{0})", "N'" & HI.UL.ULDate.ConvertEnDB(tDateF42) & "'"))

                                End If

                                If oStrBuilder.Length > 0 Then
                                    tSql = ""
                                    tSql = oStrBuilder.ToString()

                                    If HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN) = True Then
                                        'MsgBox("Execute data complete ...", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
                                    Else
                                        'MsgBox("Execute batch file not complete !!!", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
                                    End If

                                Else
                                    'MsgBox("Do nothing zzz !!!")
                                End If

                                '...increment col match size
                                nLoopColMatchSize = nLoopColMatchSize + 5

                                tMatchSize = oDBdtSrc.Rows(_nRowHeader - 2).Item(nLoopColMatchSize).ToString()

                                If Not oDBdtMapSize Is Nothing AndAlso oDBdtMapSize.Rows.Count > 0 Then
                                    Dim tMapSizeExtension As String = ""
                                    Dim oDataRow As DataRow()
                                    Try
                                        oDataRow = oDBdtMapSize.Select("FTMapSize = '" & HI.UL.ULF.rpQuoted(tMatchSize) & "'")
                                        If oDataRow.Length > 0 Then
                                            If oDataRow(0)("FTMapSizeExtension") <> "" Then
                                                tMatchSize = oDataRow(0)("FTMapSizeExtension").ToString()
                                            End If

                                        Else
                                            '/* ถ้าไม่มีการ Map Size ไว้ใน TMERMMapSize [MERCHAN] ให้ทำการตรวจสอบว่่า มีการ Map ด้วยวิธี Manual ผ่าน หน้าจอ โปรแกรม ก่อนการ Import Order หรือไม่ */
                                            If Not _oDBdtUserImportMapSizeManual Is Nothing AndAlso _oDBdtUserImportMapSizeManual.Rows.Count > 0 Then
                                                Dim oDataRowMapSizeManual As System.Data.DataRow()

                                                Try
                                                    oDataRowMapSizeManual = _oDBdtUserImportMapSizeManual.Select("FTSizeCodeNotExists = '" & HI.UL.ULF.rpQuoted(tMatchSize) & "'")
                                                    If oDataRowMapSizeManual.Length > 0 Then

                                                        If System.Diagnostics.Debugger.IsAttached = True Then
                                                            System.Diagnostics.Debug.Write("Map Size Code : " & tMatchSize & " To ==> " & "^_^" & " ")

                                                            If oDataRowMapSizeManual(0)("FTMapSizeExtend") <> "" Then
                                                                tMatchSize = oDataRowMapSizeManual(0)("FTMapSizeExtend").ToString()
                                                            End If

                                                            System.Diagnostics.Debug.Write(" " & tMatchSize & " " & Environment.NewLine)
                                                        Else
                                                            If oDataRowMapSizeManual(0)("FTMapSizeExtend") <> "" Then
                                                                tMatchSize = oDataRowMapSizeManual(0)("FTMapSizeExtend").ToString()
                                                            End If

                                                        End If

                                                    End If

                                                Catch ex As Exception
                                                End Try

                                            End If

                                        End If

                                    Catch ex As Exception
                                    End Try

                                Else
                                    '/* ถ้าไม่มีการ Map Size ไว้ใน TMERMMapSize [MERCHAN] ให้ทำการตรวจสอบว่่า มีการ Map ด้วยวิธี Manual ผ่าน หน้าจอ โปรแกรม ก่อนการ Import Order หรือไม่ */
                                    If Not _oDBdtUserImportMapSizeManual Is Nothing AndAlso _oDBdtUserImportMapSizeManual.Rows.Count > 0 Then
                                        Dim oDataRowMapSizeManual As System.Data.DataRow()

                                        Try
                                            oDataRowMapSizeManual = _oDBdtUserImportMapSizeManual.Select("FTSizeCodeNotExists = '" & HI.UL.ULF.rpQuoted(tMatchSize) & "'")
                                            If oDataRowMapSizeManual.Length > 0 Then

                                                If oDataRowMapSizeManual(0)("FTMapSizeExtend") <> "" Then
                                                    tMatchSize = oDataRowMapSizeManual(0)("FTMapSizeExtend").ToString()
                                                End If

                                            End If

                                        Catch ex As Exception
                                        End Try

                                    End If

                                End If

                                'Application.DoEvents()

                            Loop

                        End If

                    Next nLoopSizeBreakdown

                End If

                bRet = True

            End If

        Catch ex As Exception
            'If System.Diagnostics.Debugger.IsAttached = True Then
            MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
            'End If
            REM MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
        End Try

        Return bRet

    End Function

    Private Function W_PRCbShowBrowseDataImportFactoryOrder() As Boolean
        Dim bRet As Boolean = False

        'Me.ogdConfirmImport.DataSource = Nothing
        'Me.ogdConfirmImport.Refresh()
        'Call W_PRCbRemoveGridViewColumn(Me.ogvConfirmImport)
        'ogvConfirmImport.OptionsView.ColumnAutoWidth = False

        Try
            Dim oDBdtStatic As System.Data.DataTable
            Dim oDBdtDynamic As System.Data.DataTable
            Dim oDBdtBrowse As System.Data.DataTable
            Dim oDBdtMatSize As System.Data.DataTable

            Dim _dt As DataTable
            Dim _Qry As String = ""


            tSql = ""
            tSql = "SELECT A.FNRowImport, A.FTPONo, A.FTPOTrading, A.FTPOItem"
            tSql &= Environment.NewLine & "      , A.FNHSysMerTeamId"
            tSql &= Environment.NewLine & "      , (SELECT TOP 1 L1.FTMerTeamCode FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMerTeam] AS L1 WITH(NOLOCK) WHERE L1.FNHSysMerTeamId = A.FNHSysMerTeamId) AS FTMerTeamCode"
            tSql &= Environment.NewLine & "      , (SELECT TOP 1 L1.FTMerTeamNameEN FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMerTeam] AS L1 WITH(NOLOCK) WHERE L1.FNHSysMerTeamId = A.FNHSysMerTeamId) AS FTMerTeamDesc"
            tSql &= Environment.NewLine & "      , CASE WHEN ISDATE(A.FTPOCreateDate) = 1 THEN CONVERT(VARCHAR(10), CAST(A.FTPOCreateDate AS DATE), 103) ELSE '' END AS FTPOCreateDate"
            tSql &= Environment.NewLine & "      , CASE WHEN ISDATE(A.FTOrderDate) = 1 THEN CONVERT(VARCHAR(10), CAST(A.FTOrderDate AS DATE), 103) ELSE '' END AS FTOrderDate"
            tSql &= Environment.NewLine & "      , A.FNHSysPlantId"
            tSql &= Environment.NewLine & "      , (SELECT TOP 1 L1.FTPlantNameEN FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMPlant] AS L1 WITH(NOLOCK) WHERE L1.FNHSysPlantId = A.FNHSysPlantId) AS FTPlantDesc"
            tSql &= Environment.NewLine & "      , A.FNHSysBuyGrpId  AS FNHSysBuyGrpId"
            tSql &= Environment.NewLine & "      , (SELECT TOP 1 L1.FTBuyGrpCode FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMBuyGrp] AS L1 WITH(NOLOCK) WHERE L1.FNHSysBuyGrpId = A.FNHSysBuyGrpId)  AS FTBuyGrpCode"
            tSql &= Environment.NewLine & "      , (SELECT TOP 1 L1.FTBuyGrpNameEN FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMBuyGrp] AS L1 WITH(NOLOCK) WHERE L1.FNHSysBuyGrpId = A.FNHSysBuyGrpId) AS FTBuyGrpDesc"
            tSql &= Environment.NewLine & "      , A.FTStyle, A.FTStyleDesc"
            tSql &= Environment.NewLine & "      , A.FNHSysMainCategoryId  AS FNHSysMainCategoryId"
            tSql &= Environment.NewLine & "      , (SELECT TOP 1 L1.FTMainCategoryCode FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMainCategoryType] AS L1 WITH(NOLOCK) WHERE L1.FNHSysMainCategoryId = A.FNHSysMainCategoryId) AS FTMainCategoryCode"
            tSql &= Environment.NewLine & "      , (SELECT TOP 1 L1.FTMainCategoryDescEN FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMainCategoryType] AS L1 WHERE L1.FNHSysMainCategoryId = A.FNHSysMainCategoryId) AS FTMainCategoryDesc"
            tSql &= Environment.NewLine & "      , (SELECT TOP 1 L1.FTProdTypeNameEN FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMProductType] AS L1 WITH(NOLOCK) WHERE L1.FNHSysProdTypeId = A.FNHSysProdTypeId) AS FTProdTypeDesc"
            tSql &= Environment.NewLine & "      , A.FTMaterial, A.FTMaterial, A.FTPlanningSeason, A.FTYear"
            tSql &= Environment.NewLine & "      , (SELECT TOP 1 L1.FNHSysBuyerId  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMBuyer] AS L1 WITH(NOLOCK) WHERE L1.FTBuyerCode = A.FTCustCode)  AS FNHSysBuyerId"
            tSql &= Environment.NewLine & "      , A.FTCustCode AS FTBuyerCode"
            tSql &= Environment.NewLine & "      , ISNULL((SELECT TOP 1 L1.FTBuyerNameEN FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMBuyer] AS L1 WITH(NOLOCK) WHERE L1.FTBuyerCode = A.FTCustCode), '') AS FTBuyerDesc"
            tSql &= Environment.NewLine & "      , A.FNHSysCountryId AS FNHSysCountryId"
            tSql &= Environment.NewLine & "      , D.FTCountryCode AS FNHSysCountryCode"
            tSql &= Environment.NewLine & "      , D.FTCountryNameEN AS FTCountryDesc"
            tSql &= Environment.NewLine & "      , A.FNHSysGenderId   AS FNHSysGenderId"
            tSql &= Environment.NewLine & "      , (SELECT TOP 1 L1.FTGenderCode  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMGender] AS L1 WITH(NOLOCK) WHERE L1.FNHSysGenderId = A.FNHSysGenderId) AS FTGenderCode"
            tSql &= Environment.NewLine & "      , (SELECT TOP 1 L1.FTGenderNameEN FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMGender] AS L1 WITH(NOLOCK) WHERE L1.FNHSysGenderId = A.FNHSysGenderId) AS FTGenderDesc"
            tSql &= Environment.NewLine & "      , CASE WHEN ISDATE(A.FDShipDate) = 1 THEN CONVERT(VARCHAR(10) , CAST(A.FDShipDate AS DATE), 103) ELSE '' END AS FDShipmentDate"
            tSql &= Environment.NewLine & "      , CASE WHEN ISDATE(A.FDShipDateOrginal) = 1 THEN CONVERT(VARCHAR(10) , CAST(A.FDShipDateOrginal AS DATE), 103) ELSE '' END AS FDShipmentDateOriginal"
            tSql &= Environment.NewLine & "      , (SELECT TOP 1 L1.FNHSysMatColorId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMatColor] AS L1 WITH(NOLOCK) WHERE L1.FTMatColorCode = B.FTColorwayCode) AS FNHSysMatColorId"
            tSql &= Environment.NewLine & "      , B.FTColorwayCode"
            tSql &= Environment.NewLine & "      , B.FTColorwayDesc"
            tSql &= Environment.NewLine & "      , (SELECT TOP 1 L1.FNHSysShipModeId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCNMShipMode] AS L1 WITH(NOLOCK) WHERE L1.FTShipModeCode = B.FTMode) AS FNHSysShipModeId"
            tSql &= Environment.NewLine & "      , B.FTMode AS FTShipModeDesc"
            tSql &= Environment.NewLine & "      , (SELECT TOP 1 L1.FNHSysUnitId  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCNMUnit] AS L1 WITH(NOLOCK) WHERE L1.FTUnitCode = B.FTUom) AS FNHSysUnitId"
            tSql &= Environment.NewLine & "      , B.FTUom AS FTUnitDesc"
            tSql &= Environment.NewLine & "      , CASE WHEN ISDATE(B.FTShipDate) = 1 THEN Convert(Datetime,B.FTShipDate) ELSE NULL END AS FTShipDate "
            tSql &= Environment.NewLine & "      , B.FTSizeBreakdownCode "
            tSql &= Environment.NewLine & "      , B.FNQuantity"
            tSql &= Environment.NewLine & "      , B.FNPrice,0 AS FNGrandQuantity"

            tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTImportOrderTemp] AS A INNER JOIN ("
            tSql &= Environment.NewLine & "  SELECT DISTINCT A.FNRowImport, A.FTPONo, A.FTPOTrading, A.FTPOItem"
            tSql &= Environment.NewLine & "        , A.FTStyle, A.FTColorwayCode, A.FTColorwayDesc, A.FTMode, A.FTUom"
            tSql &= Environment.NewLine & "        ,A.FTShipDate,A.FTSizeBreakdownCode,FNQuantity,FNPrice "
            tSql &= Environment.NewLine & "         FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTImportOrderSizeBreakdownTemp] AS A WITH(NOLOCK) INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMatColor] AS B WITH(NOLOCK) ON A.FTColorwayCode = B.FTMatColorCode"
            tSql &= Environment.NewLine & "         WHERE A.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            tSql &= Environment.NewLine & "         AND A.FNQuantity >0 "
            tSql &= Environment.NewLine & "       ) AS B ON A.FNRowImport = B.FNRowImport"
            tSql &= Environment.NewLine & "      AND A.FTPONo = B.FTPONo"
            tSql &= Environment.NewLine & "      AND A.FTPOTrading = B.FTPOTrading"
            tSql &= Environment.NewLine & "      AND A.FTPOItem = B.FTPOItem"
            tSql &= Environment.NewLine & "      AND A.FTStyle = B.FTStyle"
            tSql &= Environment.NewLine & "      LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMatColor] AS C WITH(NOLOCK) ON B.FTColorwayCode = C.FTMatColorCode"
            tSql &= Environment.NewLine & "      LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCNMCountry] AS D WITH(NOLOCK) ON A.FNHSysCountryId = D.FNHSysCountryId"
            tSql &= Environment.NewLine & "      LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMatSize] AS MS WITH(NOLOCK) ON B.FTSizeBreakdownCode = MS.FTMatSizeCode  "
            tSql &= Environment.NewLine & "      	  LEFT OUTER JOIN (SELECT O.FTOrderNo, OSB.FTPOref, OSB.FTNikePOLineItem, OSB.FTColorway"
            tSql &= Environment.NewLine & " , OSB.FTSizeBreakDown, OSB.FNGrandQuantity, OSB.FDShipDate"
            tSql &= Environment.NewLine & " , OSB.FNPrice, ST.FNHSysStyleId, ST.FTStyleCode, OSB.FTSubOrderNo,OSB.FTSubOrderNoRef"
            tSql &= Environment.NewLine & "  FROM     [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..V_OrderSub_BreakDown_ShipDestination AS OSB INNER JOIN"
            tSql &= Environment.NewLine & "           [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrder AS O WITH(NOLOCK) ON OSB.FTOrderNo = O.FTOrderNo INNER JOIN"
            tSql &= Environment.NewLine & "           [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTStyle AS ST  WITH(NOLOCK) ON O.FNHSysStyleId = ST.FNHSysStyleId ) AS OX ON "
            tSql &= Environment.NewLine & "   A.FTPONo = OX.FTPOref"
            tSql &= Environment.NewLine & " AND A.FTPOItem = OX.FTNikePOLineItem"
            tSql &= Environment.NewLine & " AND A.FTStyle = LEFT(OX.FTStyleCode,6)"
            tSql &= Environment.NewLine & " AND C.FTMatColorCode=OX.FTColorway"
            tSql &= Environment.NewLine & "  AND MS.FTMatSizeCode=OX.FTSizeBreakDown"

            tSql &= Environment.NewLine & "WHERE A.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            tSql &= Environment.NewLine & "  AND OX.FTOrderNo IS NULL"
            'tSql &= Environment.NewLine & "      AND A.FTPONo = B.FTPONo"
            'tSql &= Environment.NewLine & "      AND A.FTPOItem = B.FTPOItem"
            'tSql &= Environment.NewLine & "      AND A.FTStyle = B.FTStyle"
            tSql &= Environment.NewLine & "ORDER BY A.FNRowImport ASC, A.FTPONo ASC, A.FTPOItem ASC, C.FNMatColorSeq ASC,MS.FNMatSizeSeq ;"

            oDBdtStatic = HI.Conn.SQLConn.GetDataTable(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN)

            Me.ogdConfirmImport.DataSource = oDBdtStatic
            Me.ogdConfirmImport.Refresh()
            Me.ogvConfirmImport.RefreshData()
            Me.ogvConfirmImport.BestFitColumns()
            ogvConfirmImport.ClearSorting()

            'If Not _bInitialGridBandView Then
            '    Call W_PRCxInitialGridBandView()
            'End If

            Call W_PRCxInitialGridBandView(ogvConfirmImport)

        Catch ex As Exception
           
        End Try

        Return bRet

    End Function

    Private Function W_PRCbShowBrowseDataImportFactoryOrderChange() As Boolean
        Dim bRet As Boolean = False

        'Me.ogdConfirmImport.DataSource = Nothing
        'Me.ogdConfirmImport.Refresh()
        'Call W_PRCbRemoveGridViewColumn(Me.ogvConfirmImport)
        'ogvConfirmImport.OptionsView.ColumnAutoWidth = False

        Try
            Dim oDBdtStatic As System.Data.DataTable
            Dim oDBdtDynamic As System.Data.DataTable
            Dim oDBdtBrowse As System.Data.DataTable
            Dim oDBdtMatSize As System.Data.DataTable

            ogdOrderchange.DataSource = Nothing


            Dim _dt As DataTable
            Dim _Qry As String = ""


            tSql = ""
            tSql = "SELECT A.FNRowImport, A.FTPONo, A.FTPOTrading, A.FTPOItem"
            tSql &= Environment.NewLine & "      , A.FNHSysMerTeamId"
            tSql &= Environment.NewLine & "      , (SELECT TOP 1 L1.FTMerTeamCode FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMerTeam] AS L1 WITH(NOLOCK) WHERE L1.FNHSysMerTeamId = A.FNHSysMerTeamId) AS FTMerTeamCode"
            tSql &= Environment.NewLine & "      , (SELECT TOP 1 L1.FTMerTeamNameEN FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMerTeam] AS L1 WITH(NOLOCK) WHERE L1.FNHSysMerTeamId = A.FNHSysMerTeamId) AS FTMerTeamDesc"
            tSql &= Environment.NewLine & "      , CASE WHEN ISDATE(A.FTPOCreateDate) = 1 THEN CONVERT(VARCHAR(10), CAST(A.FTPOCreateDate AS DATE), 103) ELSE '' END AS FTPOCreateDate"
            tSql &= Environment.NewLine & "      , CASE WHEN ISDATE(A.FTOrderDate) = 1 THEN CONVERT(VARCHAR(10), CAST(A.FTOrderDate AS DATE), 103) ELSE '' END AS FTOrderDate"
            tSql &= Environment.NewLine & "      , A.FNHSysPlantId"
            tSql &= Environment.NewLine & "      , (SELECT TOP 1 L1.FTPlantNameEN FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMPlant] AS L1 WITH(NOLOCK) WHERE L1.FNHSysPlantId = A.FNHSysPlantId) AS FTPlantDesc"
            tSql &= Environment.NewLine & "      , A.FNHSysBuyGrpId  AS FNHSysBuyGrpId"
            tSql &= Environment.NewLine & "      , (SELECT TOP 1 L1.FTBuyGrpCode FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMBuyGrp] AS L1 WITH(NOLOCK) WHERE L1.FNHSysBuyGrpId = A.FNHSysBuyGrpId)  AS FTBuyGrpCode"
            tSql &= Environment.NewLine & "      , (SELECT TOP 1 L1.FTBuyGrpNameEN FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMBuyGrp] AS L1 WITH(NOLOCK) WHERE L1.FNHSysBuyGrpId = A.FNHSysBuyGrpId) AS FTBuyGrpDesc"
            tSql &= Environment.NewLine & "      , A.FTStyle, A.FTStyleDesc"
            tSql &= Environment.NewLine & "      , A.FNHSysMainCategoryId  AS FNHSysMainCategoryId"
            tSql &= Environment.NewLine & "      , (SELECT TOP 1 L1.FTMainCategoryCode FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMainCategoryType] AS L1 WITH(NOLOCK) WHERE L1.FNHSysMainCategoryId = A.FNHSysMainCategoryId) AS FTMainCategoryCode"
            tSql &= Environment.NewLine & "      , (SELECT TOP 1 L1.FTMainCategoryDescEN FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMainCategoryType] AS L1 WHERE L1.FNHSysMainCategoryId = A.FNHSysMainCategoryId) AS FTMainCategoryDesc"
            tSql &= Environment.NewLine & "      , (SELECT TOP 1 L1.FTProdTypeNameEN FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMProductType] AS L1 WITH(NOLOCK) WHERE L1.FNHSysProdTypeId = A.FNHSysProdTypeId) AS FTProdTypeDesc"
            tSql &= Environment.NewLine & "      , A.FTMaterial, A.FTMaterial, A.FTPlanningSeason, A.FTYear"
            tSql &= Environment.NewLine & "      , (SELECT TOP 1 L1.FNHSysBuyerId  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMBuyer] AS L1 WITH(NOLOCK) WHERE L1.FTBuyerCode = A.FTCustCode)  AS FNHSysBuyerId"
            tSql &= Environment.NewLine & "      , A.FTCustCode AS FTBuyerCode"
            tSql &= Environment.NewLine & "      , ISNULL((SELECT TOP 1 L1.FTBuyerNameEN FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMBuyer] AS L1 WITH(NOLOCK) WHERE L1.FTBuyerCode = A.FTCustCode), '') AS FTBuyerDesc"
            tSql &= Environment.NewLine & "      , A.FNHSysCountryId AS FNHSysCountryId"
            tSql &= Environment.NewLine & "      , D.FTCountryCode AS FNHSysCountryCode"
            tSql &= Environment.NewLine & "      , D.FTCountryNameEN AS FTCountryDesc"
            tSql &= Environment.NewLine & "      , A.FNHSysGenderId   AS FNHSysGenderId"
            tSql &= Environment.NewLine & "      , (SELECT TOP 1 L1.FTGenderCode  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMGender] AS L1 WITH(NOLOCK) WHERE L1.FNHSysGenderId = A.FNHSysGenderId) AS FTGenderCode"
            tSql &= Environment.NewLine & "      , (SELECT TOP 1 L1.FTGenderNameEN FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMGender] AS L1 WITH(NOLOCK) WHERE L1.FNHSysGenderId = A.FNHSysGenderId) AS FTGenderDesc"
            tSql &= Environment.NewLine & "      , CASE WHEN ISDATE(A.FDShipDate) = 1 THEN CONVERT(VARCHAR(10) , CAST(A.FDShipDate AS DATE), 103) ELSE '' END AS FDShipmentDate"
            tSql &= Environment.NewLine & "      , CASE WHEN ISDATE(A.FDShipDateOrginal) = 1 THEN CONVERT(VARCHAR(10) , CAST(A.FDShipDateOrginal AS DATE), 103) ELSE '' END AS FDShipmentDateOriginal"
            tSql &= Environment.NewLine & "      , (SELECT TOP 1 L1.FNHSysMatColorId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMatColor] AS L1 WITH(NOLOCK) WHERE L1.FTMatColorCode = B.FTColorwayCode) AS FNHSysMatColorId"
            tSql &= Environment.NewLine & "      , B.FTColorwayCode"
            tSql &= Environment.NewLine & "      , B.FTColorwayDesc"
            tSql &= Environment.NewLine & "      , (SELECT TOP 1 L1.FNHSysShipModeId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCNMShipMode] AS L1 WITH(NOLOCK) WHERE L1.FTShipModeCode = B.FTMode) AS FNHSysShipModeId"
            tSql &= Environment.NewLine & "      , B.FTMode AS FTShipModeDesc"
            tSql &= Environment.NewLine & "      , (SELECT TOP 1 L1.FNHSysUnitId  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCNMUnit] AS L1 WITH(NOLOCK) WHERE L1.FTUnitCode = B.FTUom) AS FNHSysUnitId"
            tSql &= Environment.NewLine & "      , B.FTUom AS FTUnitDesc"
            tSql &= Environment.NewLine & "      , CASE WHEN ISDATE(B.FTShipDate) = 1 THEN Convert(Datetime,B.FTShipDate) ELSE NULL END AS FTShipDate "
            tSql &= Environment.NewLine & "      , B.FTSizeBreakdownCode "
            tSql &= Environment.NewLine & "      , B.FNQuantity"
            tSql &= Environment.NewLine & "      , B.FNPrice"
            tSql &= Environment.NewLine & "      , OX.FNGrandQuantity"
            tSql &= Environment.NewLine & "      , OX.FNPrice"
            tSql &= Environment.NewLine & "      , OX.FTOrderNo"
            tSql &= Environment.NewLine & "      , OX.FTSubOrderNoRef"
            tSql &= Environment.NewLine & "  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTImportOrderTemp] AS A INNER JOIN ("
            tSql &= Environment.NewLine & "  SELECT DISTINCT A.FNRowImport, A.FTPONo, A.FTPOTrading, A.FTPOItem"
            tSql &= Environment.NewLine & "        , A.FTStyle, A.FTColorwayCode, A.FTColorwayDesc, A.FTMode, A.FTUom"
            tSql &= Environment.NewLine & "        ,A.FTShipDate,A.FTSizeBreakdownCode,FNQuantity,FNPrice "
            tSql &= Environment.NewLine & "         FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTImportOrderSizeBreakdownTemp] AS A WITH(NOLOCK) INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMatColor] AS B WITH(NOLOCK) ON A.FTColorwayCode = B.FTMatColorCode"
            tSql &= Environment.NewLine & "         WHERE A.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            tSql &= Environment.NewLine & "         AND A.FNQuantity >0 "
            tSql &= Environment.NewLine & "       ) AS B ON A.FNRowImport = B.FNRowImport"
            tSql &= Environment.NewLine & "      AND A.FTPONo = B.FTPONo"
            tSql &= Environment.NewLine & "      AND A.FTPOTrading = B.FTPOTrading"
            tSql &= Environment.NewLine & "      AND A.FTPOItem = B.FTPOItem"
            tSql &= Environment.NewLine & "      AND A.FTStyle = B.FTStyle"
            tSql &= Environment.NewLine & "      LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMatColor] AS C WITH(NOLOCK) ON B.FTColorwayCode = C.FTMatColorCode"
            tSql &= Environment.NewLine & "      LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCNMCountry] AS D WITH(NOLOCK) ON A.FNHSysCountryId = D.FNHSysCountryId"
            tSql &= Environment.NewLine & "      LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMatSize] AS MS WITH(NOLOCK) ON B.FTSizeBreakdownCode = MS.FTMatSizeCode  "
            tSql &= Environment.NewLine & "      INNER JOIN (SELECT O.FTOrderNo, OSB.FTPOref, OSB.FTNikePOLineItem, OSB.FTColorway"
            tSql &= Environment.NewLine & " , OSB.FTSizeBreakDown, OSB.FNGrandQuantity, OSB.FDShipDate"
            tSql &= Environment.NewLine & " , OSB.FNPrice, ST.FNHSysStyleId, ST.FTStyleCode, OSB.FTSubOrderNo,OSB.FTSubOrderNoRef"
            tSql &= Environment.NewLine & "  FROM     [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..V_OrderSub_BreakDown_ShipDestination AS OSB INNER JOIN"
            tSql &= Environment.NewLine & "           [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrder AS O WITH(NOLOCK) ON OSB.FTOrderNo = O.FTOrderNo INNER JOIN"
            tSql &= Environment.NewLine & "           [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTStyle AS ST  WITH(NOLOCK) ON O.FNHSysStyleId = ST.FNHSysStyleId ) AS OX ON "
            tSql &= Environment.NewLine & "   A.FTPONo = OX.FTPOref"
            tSql &= Environment.NewLine & " AND A.FTPOItem = OX.FTNikePOLineItem"
            tSql &= Environment.NewLine & " AND A.FTStyle = LEFT(OX.FTStyleCode,6)"
            tSql &= Environment.NewLine & " AND C.FTMatColorCode=OX.FTColorway"
            tSql &= Environment.NewLine & "  AND MS.FTMatSizeCode=OX.FTSizeBreakDown"

            tSql &= Environment.NewLine & "WHERE A.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            tSql &= Environment.NewLine & "      AND B.FNQuantity <> OX.FNGrandQuantity"
            'tSql &= Environment.NewLine & "      AND A.FTPONo = B.FTPONo"
            'tSql &= Environment.NewLine & "      AND A.FTPOItem = B.FTPOItem"
            'tSql &= Environment.NewLine & "      AND A.FTStyle = B.FTStyle"
            tSql &= Environment.NewLine & "ORDER BY A.FNRowImport ASC, A.FTPONo ASC, A.FTPOItem ASC, C.FNMatColorSeq ASC,MS.FNMatSizeSeq ;"

            oDBdtStatic = HI.Conn.SQLConn.GetDataTable(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN)

            Me.ogdOrderchange.DataSource = oDBdtStatic
            Me.ogdOrderchange.Refresh()
            Me.ogvOrderchange.RefreshData()
            Me.ogvOrderchange.BestFitColumns()
            ogvOrderchange.ClearSorting()

            'If Not _bInitialGridBandView Then
            '    Call W_PRCxInitialGridBandView()
            'End If

            Call W_PRCxInitialGridBandView(ogvOrderchange)

        Catch ex As Exception

        End Try

        Return bRet

    End Function

    Private Function W_PRCbShowBrowseDataImportFactoryOrderRemove() As Boolean
        Dim bRet As Boolean = False

        'Me.ogdConfirmImport.DataSource = Nothing
        'Me.ogdConfirmImport.Refresh()
        'Call W_PRCbRemoveGridViewColumn(Me.ogvConfirmImport)
        'ogvConfirmImport.OptionsView.ColumnAutoWidth = False

        Try
            Dim oDBdtStatic As System.Data.DataTable
            Dim oDBdtDynamic As System.Data.DataTable
            Dim oDBdtBrowse As System.Data.DataTable
            Dim oDBdtMatSize As System.Data.DataTable

            ogdOrderRemove.DataSource = Nothing

            Dim _dt As DataTable
            Dim _Qry As String = ""


            tSql = ""
            tSql = "SELECT A.FNRowImport, A.FTPONo, A.FTPOTrading, A.FTPOItem"
            tSql &= Environment.NewLine & "      , A.FNHSysMerTeamId"
            tSql &= Environment.NewLine & "      , (SELECT TOP 1 L1.FTMerTeamCode FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMerTeam] AS L1 WITH(NOLOCK) WHERE L1.FNHSysMerTeamId = A.FNHSysMerTeamId) AS FTMerTeamCode"
            tSql &= Environment.NewLine & "      , (SELECT TOP 1 L1.FTMerTeamNameEN FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMerTeam] AS L1 WITH(NOLOCK) WHERE L1.FNHSysMerTeamId = A.FNHSysMerTeamId) AS FTMerTeamDesc"
            tSql &= Environment.NewLine & "      , CASE WHEN ISDATE(A.FTPOCreateDate) = 1 THEN CONVERT(VARCHAR(10), CAST(A.FTPOCreateDate AS DATE), 103) ELSE '' END AS FTPOCreateDate"
            tSql &= Environment.NewLine & "      , CASE WHEN ISDATE(A.FTOrderDate) = 1 THEN CONVERT(VARCHAR(10), CAST(A.FTOrderDate AS DATE), 103) ELSE '' END AS FTOrderDate"
            tSql &= Environment.NewLine & "      , A.FNHSysPlantId"
            tSql &= Environment.NewLine & "      , (SELECT TOP 1 L1.FTPlantNameEN FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMPlant] AS L1 WITH(NOLOCK) WHERE L1.FNHSysPlantId = A.FNHSysPlantId) AS FTPlantDesc"
            tSql &= Environment.NewLine & "      , A.FNHSysBuyGrpId  AS FNHSysBuyGrpId"
            tSql &= Environment.NewLine & "      , (SELECT TOP 1 L1.FTBuyGrpCode FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMBuyGrp] AS L1 WITH(NOLOCK) WHERE L1.FNHSysBuyGrpId = A.FNHSysBuyGrpId)  AS FTBuyGrpCode"
            tSql &= Environment.NewLine & "      , (SELECT TOP 1 L1.FTBuyGrpNameEN FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMBuyGrp] AS L1 WITH(NOLOCK) WHERE L1.FNHSysBuyGrpId = A.FNHSysBuyGrpId) AS FTBuyGrpDesc"
            tSql &= Environment.NewLine & "      , A.FTStyle, A.FTStyleDesc"
            tSql &= Environment.NewLine & "      , A.FNHSysMainCategoryId  AS FNHSysMainCategoryId"
            tSql &= Environment.NewLine & "      , (SELECT TOP 1 L1.FTMainCategoryCode FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMainCategoryType] AS L1 WITH(NOLOCK) WHERE L1.FNHSysMainCategoryId = A.FNHSysMainCategoryId) AS FTMainCategoryCode"
            tSql &= Environment.NewLine & "      , (SELECT TOP 1 L1.FTMainCategoryDescEN FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMainCategoryType] AS L1 WHERE L1.FNHSysMainCategoryId = A.FNHSysMainCategoryId) AS FTMainCategoryDesc"
            tSql &= Environment.NewLine & "      , (SELECT TOP 1 L1.FTProdTypeNameEN FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMProductType] AS L1 WITH(NOLOCK) WHERE L1.FNHSysProdTypeId = A.FNHSysProdTypeId) AS FTProdTypeDesc"
            tSql &= Environment.NewLine & "      , A.FTMaterial, A.FTMaterial, A.FTPlanningSeason, A.FTYear"
            tSql &= Environment.NewLine & "      , (SELECT TOP 1 L1.FNHSysBuyerId  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMBuyer] AS L1 WITH(NOLOCK) WHERE L1.FTBuyerCode = A.FTCustCode)  AS FNHSysBuyerId"
            tSql &= Environment.NewLine & "      , A.FTCustCode AS FTBuyerCode"
            tSql &= Environment.NewLine & "      , ISNULL((SELECT TOP 1 L1.FTBuyerNameEN FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMBuyer] AS L1 WITH(NOLOCK) WHERE L1.FTBuyerCode = A.FTCustCode), '') AS FTBuyerDesc"
            tSql &= Environment.NewLine & "      , A.FNHSysCountryId AS FNHSysCountryId"
            tSql &= Environment.NewLine & "      , D.FTCountryCode AS FNHSysCountryCode"
            tSql &= Environment.NewLine & "      , D.FTCountryNameEN AS FTCountryDesc"
            tSql &= Environment.NewLine & "      , A.FNHSysGenderId   AS FNHSysGenderId"
            tSql &= Environment.NewLine & "      , (SELECT TOP 1 L1.FTGenderCode  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMGender] AS L1 WITH(NOLOCK) WHERE L1.FNHSysGenderId = A.FNHSysGenderId) AS FTGenderCode"
            tSql &= Environment.NewLine & "      , (SELECT TOP 1 L1.FTGenderNameEN FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMGender] AS L1 WITH(NOLOCK) WHERE L1.FNHSysGenderId = A.FNHSysGenderId) AS FTGenderDesc"
            tSql &= Environment.NewLine & "      , CASE WHEN ISDATE(A.FDShipDate) = 1 THEN CONVERT(VARCHAR(10) , CAST(A.FDShipDate AS DATE), 103) ELSE '' END AS FDShipmentDate"
            tSql &= Environment.NewLine & "      , CASE WHEN ISDATE(A.FDShipDateOrginal) = 1 THEN CONVERT(VARCHAR(10) , CAST(A.FDShipDateOrginal AS DATE), 103) ELSE '' END AS FDShipmentDateOriginal"
            tSql &= Environment.NewLine & "      , (SELECT TOP 1 L1.FNHSysMatColorId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMatColor] AS L1 WITH(NOLOCK) WHERE L1.FTMatColorCode = B.FTColorwayCode) AS FNHSysMatColorId"
            tSql &= Environment.NewLine & "      , B.FTColorwayCode"
            tSql &= Environment.NewLine & "      , B.FTColorwayDesc"
            tSql &= Environment.NewLine & "      , (SELECT TOP 1 L1.FNHSysShipModeId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCNMShipMode] AS L1 WITH(NOLOCK) WHERE L1.FTShipModeCode = B.FTMode) AS FNHSysShipModeId"
            tSql &= Environment.NewLine & "      , B.FTMode AS FTShipModeDesc"
            tSql &= Environment.NewLine & "      , (SELECT TOP 1 L1.FNHSysUnitId  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCNMUnit] AS L1 WITH(NOLOCK) WHERE L1.FTUnitCode = B.FTUom) AS FNHSysUnitId"
            tSql &= Environment.NewLine & "      , B.FTUom AS FTUnitDesc"
            tSql &= Environment.NewLine & "      , CASE WHEN ISDATE(B.FTShipDate) = 1 THEN Convert(Datetime,B.FTShipDate) ELSE NULL END AS FTShipDate "
            tSql &= Environment.NewLine & "      , B.FTSizeBreakdownCode "
            tSql &= Environment.NewLine & "      , B.FNQuantity"
            tSql &= Environment.NewLine & "      , B.FNPrice"
            tSql &= Environment.NewLine & "      , OX.FNGrandQuantity"
            tSql &= Environment.NewLine & "      , OX.FNPrice"
            tSql &= Environment.NewLine & "      , OX.FTOrderNo"
            tSql &= Environment.NewLine & "      , OX.FTSubOrderNoRef,OX.FTNikePOLineItem"
            tSql &= Environment.NewLine & "  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTImportOrderTemp] AS A INNER JOIN ("
            tSql &= Environment.NewLine & "  SELECT DISTINCT A.FNRowImport, A.FTPONo, A.FTPOTrading, A.FTPOItem"
            tSql &= Environment.NewLine & "        , A.FTStyle, A.FTColorwayCode, A.FTColorwayDesc, A.FTMode, A.FTUom"
            tSql &= Environment.NewLine & "        ,A.FTShipDate,A.FTSizeBreakdownCode,FNQuantity,FNPrice "
            tSql &= Environment.NewLine & "         FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTImportOrderSizeBreakdownTemp] AS A WITH(NOLOCK) INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMatColor] AS B WITH(NOLOCK) ON A.FTColorwayCode = B.FTMatColorCode"
            tSql &= Environment.NewLine & "         WHERE A.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            tSql &= Environment.NewLine & "         AND A.FNQuantity >0 "
            tSql &= Environment.NewLine & "       ) AS B ON A.FNRowImport = B.FNRowImport"
            tSql &= Environment.NewLine & "      AND A.FTPONo = B.FTPONo"
            tSql &= Environment.NewLine & "      AND A.FTPOTrading = B.FTPOTrading"
            tSql &= Environment.NewLine & "      AND A.FTPOItem = B.FTPOItem"
            tSql &= Environment.NewLine & "      AND A.FTStyle = B.FTStyle"
            tSql &= Environment.NewLine & "      LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMatColor] AS C WITH(NOLOCK) ON B.FTColorwayCode = C.FTMatColorCode"
            tSql &= Environment.NewLine & "      LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCNMCountry] AS D WITH(NOLOCK) ON A.FNHSysCountryId = D.FNHSysCountryId"
            tSql &= Environment.NewLine & "      LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMatSize] AS MS WITH(NOLOCK) ON B.FTSizeBreakdownCode = MS.FTMatSizeCode  "
            tSql &= Environment.NewLine & "      INNER JOIN (SELECT O.FTOrderNo, OSB.FTPOref, OSB.FTNikePOLineItem, OSB.FTColorway"
            tSql &= Environment.NewLine & " , OSB.FTSizeBreakDown, OSB.FNGrandQuantity, OSB.FDShipDate"
            tSql &= Environment.NewLine & " , OSB.FNPrice, ST.FNHSysStyleId, ST.FTStyleCode, OSB.FTSubOrderNo,OSB.FTSubOrderNoRef"
            tSql &= Environment.NewLine & "  FROM     [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..V_OrderSub_BreakDown_ShipDestination AS OSB INNER JOIN"
            tSql &= Environment.NewLine & "           [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrder AS O WITH(NOLOCK) ON OSB.FTOrderNo = O.FTOrderNo INNER JOIN"
            tSql &= Environment.NewLine & "           [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTStyle AS ST  WITH(NOLOCK) ON O.FNHSysStyleId = ST.FNHSysStyleId ) AS OX ON "
            tSql &= Environment.NewLine & "   A.FTPONo = OX.FTPOref"
            tSql &= Environment.NewLine & " AND A.FTPOItem <> OX.FTNikePOLineItem"
            tSql &= Environment.NewLine & " AND A.FTStyle = LEFT(OX.FTStyleCode,6)"
            tSql &= Environment.NewLine & " AND C.FTMatColorCode=OX.FTColorway"
            tSql &= Environment.NewLine & "  AND MS.FTMatSizeCode=OX.FTSizeBreakDown"

            tSql &= Environment.NewLine & "WHERE A.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"

            'tSql &= Environment.NewLine & "      AND A.FTPONo = B.FTPONo"
            'tSql &= Environment.NewLine & "      AND A.FTPOItem = B.FTPOItem"
            'tSql &= Environment.NewLine & "      AND A.FTStyle = B.FTStyle"
            tSql &= Environment.NewLine & "ORDER BY A.FNRowImport ASC, A.FTPONo ASC, A.FTPOItem ASC, C.FNMatColorSeq ASC,MS.FNMatSizeSeq ;"

            oDBdtStatic = HI.Conn.SQLConn.GetDataTable(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN)

            Me.ogdOrderRemove.DataSource = oDBdtStatic
            Me.ogdOrderRemove.Refresh()
            Me.ogvOrderRemove.RefreshData()
            Me.ogvOrderRemove.BestFitColumns()
            ogvOrderRemove.ClearSorting()

            'If Not _bInitialGridBandView Then
            '    Call W_PRCxInitialGridBandView()
            'End If

            Call W_PRCxInitialGridBandView(ogvOrderRemove)

        Catch ex As Exception

        End Try

        Return bRet

    End Function

    Private Function W_GETdtReadExcelData(ptFilePath As String, _extension As String) As System.Data.DataTable
        Dim oDBdtExcel As New System.Data.DataTable()
        Dim hasHeaders As Boolean = False
        Dim HDR As String = If(hasHeaders, "Yes", "No")
        Dim strConn As String

        Try
            If ptFilePath.Substring(ptFilePath.LastIndexOf("."c)).ToLower() = ".xlsx" Then
                strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & ptFilePath & ";Extended Properties=""Excel 12.0;HDR=" & HDR & ";IMEX=1"""
            Else
                'strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & ptFilePath & ";Extended Properties=""Excel 8.0;HDR=" & HDR & ";IMEX=1"""
                strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & ptFilePath & ";Extended Properties=""Excel 8.0;HDR=" & HDR & ";IMEX=1"""
            End If

            MsgBox("Step before connected excel file...", , MsgBoxStyle.OkOnly)

            Dim conn As New OleDbConnection(strConn)

            Try
                conn.Open()

                MsgBox("Connected excel file complete...", , MsgBoxStyle.OkOnly)

            Catch ex As Exception
                MsgBox("Cannot connected excel file !!!", , MsgBoxStyle.OkOnly)
            End Try


            MsgBox("Step after connected excel file complete...", , MsgBoxStyle.OkOnly)

            Dim schemaTable As DataTable = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, New Object() {Nothing, Nothing, Nothing, "TABLE"})

            'Looping Total Sheet of Xl File
            'foreach (DataRow schemaRow in schemaTable.Rows)
            '                {
            '                }

            'Looping a first Sheet of Xl File
            Dim schemaRow As DataRow = schemaTable.Rows(0)

            Dim sheet As String = "Sheet1" REM 2014/06/05 schemaRow("TABLE_NAME").ToString()

            If Not sheet.EndsWith("_") Then
                MsgBox("Step Adapter fill rocord set to datatable", MsgBoxStyle.OkOnly)

                Dim query As String = "SELECT  *  FROM [" & sheet & "$]"
                Dim daexcel As New OleDbDataAdapter(query, conn)
                'oDBdtExcel.Locale = CultureInfo.CurrentCulture
                daexcel.Fill(oDBdtExcel)
            End If

            conn.Close()

        Catch ex As Exception
        End Try

        If oDBdtExcel.Rows.Count > 0 Then
            MsgBox("Record Count : " & oDBdtExcel.Rows.Count, , MsgBoxStyle.OkOnly)
        Else
            MsgBox("Not have record !!!", , MsgBoxStyle.OkOnly)
        End If

        Return oDBdtExcel

    End Function

    Private Function W_PRCdtReadExcelIntoDataTable(ByVal ptFileName As String, ByVal ptSheetName As String) As DataTable
        Dim RetVal As New DataTable

        Dim tConnString As String

        tConnString = "Driver={Microsoft Excel Driver (*.xls, *.xlsx, *.xlsm, *.xlsb)};DBQ=" & ptFileName & ";Extended Properties=""IMEX=1"""

        Dim tSQL As String

        tSQL = "SELECT * FROM [" & ptSheetName & "$]"

        Dim oAdapter As New System.Data.Odbc.OdbcDataAdapter(tSQL, tConnString)

        oAdapter.Fill(RetVal)

        Return RetVal

    End Function

    Private Function W_PRCdtReadCSV(ByVal ptPath As String) As System.Data.DataTable
        Try
            Dim sr As New StreamReader(ptPath)

            Dim fullFileStr As String = sr.ReadToEnd()

            sr.Close()
            sr.Dispose()

            Dim lines As String() = fullFileStr.Split(ControlChars.Lf)
            Dim recs As New DataTable()
            Dim sArr As String() = lines(0).Split(","c)

            For Each s As String In sArr
                recs.Columns.Add(New DataColumn())
            Next

            Dim row As DataRow
            Dim finalLine As String = ""

            For Each line As String In lines
                row = recs.NewRow()
                finalLine = line.Replace(Convert.ToString(ControlChars.Cr), "")
                row.ItemArray = finalLine.Split(","c)
                recs.Rows.Add(row)
            Next

            Return recs

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Private Function W_PRCdtRead_CSV(ByVal ptFilePath As String) As DataTable
        '----------------------------------------------------
        'Reads a csv file into a datatable, with the first row as column headers

        REM Dim myFile As String = FilePath & "\" & FileName
        Dim myFile As String = ptFilePath
        'Dim myTable As DataTable = New DataTable("MyTable")
        Dim myTable As DataTable = New System.Data.DataTable
        Dim i As Integer
        Dim myRow As DataRow
        Dim myColumn As DataColumn
        Dim MyType As String
        Dim fieldValues As String()

        Dim ColumnNames As String()
        Dim ColumnTypes As String()
        Dim myReader As New StreamReader(myFile)
        Try

            'Open file and read first two lines
            ColumnNames = myReader.ReadLine().Split(",")
            ColumnTypes = myReader.ReadLine().Split(",")

            'Create data columns named according to first line of data, with type according to second line
            For i = 0 To ColumnNames.Length() - 1
                myColumn = New DataColumn()
                MyType = "System." & ColumnTypes(i)
                myColumn.DataType = System.Type.GetType(MyType)

                myColumn.ColumnName = ColumnNames(i)
                myTable.Columns.Add(myColumn)
            Next

            'Read the body of the data to data table
            While myReader.Peek() <> -1
                fieldValues = myReader.ReadLine().Split(",")
                myRow = myTable.NewRow
                For i = 0 To fieldValues.Length() - 1
                    myRow.Item(i) = fieldValues(i).ToString
                Next
                myTable.Rows.Add(myRow)
            End While
        Catch ex As Exception
            MsgBox("Error building datatable: " & ex.Message)
            Return New DataTable("Empty")
        Finally
            myReader.Close()
        End Try

        Return myTable

    End Function

    Private Sub Resolve_Duplicate_Names(ByRef source() As String)
        ' Resolves the possibility of duplicated names by appending "Duplicate Name" and a number at the end of any duplicates
        Dim i, n, dnum As Integer
        dnum = 1
        For n = 0 To source.Length - 1
            For i = n + 1 To source.Length - 1
                If source(i) = source(n) Then
                    source(i) = source(i) & "Duplicate Name " & dnum
                    dnum += 1
                End If
            Next
        Next

        Return

    End Sub

    Private Function AddValuesToTable(ByRef source() As String, ByVal destination As DataTable, Optional ByVal HeaderFlag As Boolean = False) As Boolean
        'Ensures a datatable can hold an array of values and then adds a new row 
        Try
            Dim existing As Integer = destination.Columns.Count

            If HeaderFlag Then

                Resolve_Duplicate_Names(source)

                For i As Integer = 0 To source.Length - existing - 1
                    destination.Columns.Add(source(i).ToString, GetType(String))
                Next i

                Return True

            End If

            For i As Integer = 0 To source.Length - existing - 1
                REM destination.Columns.Add("Column" & (existing + 1 + i).ToString, GetType(String))
                destination.Columns.Add("F" & (existing + 1 + i).ToString, GetType(String))
            Next

            destination.Rows.Add(source)

            Return True

        Catch ex As Exception
            Console.WriteLine(ex.ToString())
            Return False
        End Try

    End Function

    Private Function W_GETdtCSVToDataTable(ByVal filePathName As String, Optional ByVal hasHeader As Boolean = False) As DataTable

        REM If Not W_PRCbValidateFileIsOpen(filePathName) Then Return Nothing

        ' Parses a csv into a datatable.
        Try
            Dim oDBdt As New DataTable
            If System.IO.File.Exists(filePathName) Then
                Dim parser As New Microsoft.VisualBasic.FileIO.TextFieldParser(filePathName)
                parser.Delimiters = New String() {","}
                parser.HasFieldsEnclosedInQuotes = True 'use if data may contain delimiters 
                parser.TextFieldType = FileIO.FieldType.Delimited
                parser.TrimWhiteSpace = True

                Dim HeaderFlag As Boolean
                If hasHeader Then HeaderFlag = True

                While Not parser.EndOfData
                    If AddValuesToTable(parser.ReadFields, oDBdt, HeaderFlag) Then
                        HeaderFlag = False
                    Else
                        parser.Close()
                        Return Nothing
                    End If

                End While

                parser.Close()

                Return oDBdt

            Else : Return Nothing
            End If

        Catch ex As Exception
            Console.WriteLine(ex.ToString())
            Return Nothing
        End Try

    End Function

#End Region

    Private Sub FTFilePath_ButtonClick(sender As Object, e As XtraEditors.Controls.ButtonPressedEventArgs) Handles FTFilePath.ButtonClick

        If W_PRCbValidateProgrammeVendor() = True Then
            Me.otcImportOrderNo.SelectedTabPageIndex = 0
            '...default when user click button select file format excel
            Select Case e.Button.Index
                Case 0
                    Try
                        Dim opFileDialog As New System.Windows.Forms.OpenFileDialog
                        REM 2014/04/29 opFileDialog.Filter = "Excel Worksheets (*.xls, *.xlsx)|*.xls;*.xlsx"
                        opFileDialog.Filter = "Excel Worksheets (*.xls, *.xlsx)|*.xls;*.xlsx|csv files (*.csv)|*.csv"
                        REM opFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)

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
                        'If System.Diagnostics.Debugger.IsAttached = True Then
                        Throw New Exception(ex.Message().ToString() & Environment.NewLine & ex.StackTrace.ToString())
                        'End If
                    End Try

                Case Else
                    '...do nothing
            End Select

            Call Me.ocmReadExcel.PerformClick()

        End If

    End Sub

    Private Sub wImportOrder_Activated(sender As Object, e As EventArgs) Handles Me.Activated

    End Sub

    Private Sub wImportOrder_Load(sender As Object, e As EventArgs) Handles Me.Load
        Timer1.Enabled = True

        Me.ogdConfirmImport.DataSource = Nothing
       
        ogvConfirmImport.OptionsView.ShowAutoFilterRow = True
        ogvConfirmImport.OptionsBehavior.AllowAddRows = DefaultBoolean.False
        ogvConfirmImport.OptionsBehavior.AllowDeleteRows = DefaultBoolean.False
        ogvConfirmImport.OptionsBehavior.Editable = False

    End Sub

    Private Sub ocmReadExcel_Click(sender As Object, e As EventArgs) Handles ocmReadExcel.Click
        Try
    
            If Me.FNHSysVenderPramId.Text <= "" Or Me.FNHSysVenderPramId.Properties.Tag.ToString = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, FNHSysVenderPramId_lbl.Text)
                Exit Sub
            End If

            Me.otcImportOrderNo.SelectedTabPageIndex = 0

            Dim _CheckOrderQtyZero As Boolean = False

            Dim oDBdtExcel As New System.Data.DataTable
            If Me.FTFilePath.Text.Trim() <> "" Then

                If IO.File.Exists(Me.FTFilePath.Text.Trim().ToString()) Then

                    _bCatchSrcFile = False

                    Dim _FilePath As String
                    _FilePath = Me.FTFilePath.Text.Trim()

                    Dim tFileExtension As String

                    tFileExtension = IO.Path.GetExtension(_FilePath)

                    '=====================================================================================================================================================================================================================
                    'DEVEXPRESS SPREAD SHEET XXX Modify 2015/01/31
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
                                'If System.Diagnostics.Debugger.IsAttached = True Then
                                '    MsgBox("Sheet Name : " + oListWorkSheet.Name.ToString, MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
                                'End If

                            End If

                        Next

                        If Not bValidateSheetName Then
                            HI.MG.ShowMsg.mInfo("ไม่พบรายการข้อมูลแผ่นงาน {Sheet1} " & Microsoft.VisualBasic.vbCrLf & "กรุณาระบุรายการแผ่นงานที่จะนำเข้ารายการใบสั่งผลิตอัตโนมัติให้ถูกตอ้ง", 1501310001, Me.Text, "Work sheet name !!!", MessageBoxIcon.Warning)
                            Exit Sub
                        Else
                            'If System.Diagnostics.Debugger.IsAttached = True Then
                            '    MsgBox("Work Sheet Name for automatic import factory order is : {Sheet1}", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
                            'End If

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

                    'With ogvConfirmImport
                    '    For I As Integer = .Columns.Count - 1 To 0 Step -1
                    '        Select Case Microsoft.VisualBasic.Left(.Columns(I).Name.ToString, 4).ToUpper
                    '            Case "oCol".ToUpper
                    '            Case Else
                    '                ' MsgBox("Column Field Name : " + .Columns(I).FieldName & "Column Name : " & .Columns(I).Name, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, My.Application.Info.Title)
                    '                .Columns.Remove(.Columns(I))
                    '        End Select

                    '    Next

                    'End With

                    Call W_PRCbLoadMapSize()

                    oDBdtExcel = HI.UL.ReadExcel.Read(_FilePath, "Sheet1", -1)

                    '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                    oDBdtExcel.BeginInit()
                    For Each Rx As DataRow In oDBdtExcel.Select("F1 = 'Overall Result' ")
                        Rx.Delete()
                    Next
                    oDBdtExcel.EndInit()

                    'Overall Result'
                    If oDBdtExcel.Select("F1='" & HI.UL.ULF.rpQuoted(Me.FNHSysVenderPramId.Text) & "'").Length > 0 Then
                        _FoundPrm = True

                        If oDBdtExcel.Rows.Count > 3 Then
                            oDBdtExcel.Rows(0)!F1 = Me.FNHSysVenderPramId.Text
                            oDBdtExcel.Rows(1)!F1 = Me.FNHSysVenderPramId.Text
                            oDBdtExcel.Rows(2)!F1 = Me.FNHSysVenderPramId.Text
                        End If

                        oDBdtExcel.BeginInit()
                        For Each Rx As DataRow In oDBdtExcel.Select("F1<>'" & HI.UL.ULF.rpQuoted(Me.FNHSysVenderPramId.Text) & "'")
                            Rx.Delete()
                        Next
                        oDBdtExcel.EndInit()

                        Dim tRowDelimeterOverAllResult As String = ""

                        If Not DBNull.Value.Equals(oDBdtExcel.Rows(oDBdtExcel.Rows.Count - 1).Item(0)) Then
                            tRowDelimeterOverAllResult = oDBdtExcel.Rows(oDBdtExcel.Rows.Count - 1).Item(0).ToString.Trim()
                        End If

                        If tRowDelimeterOverAllResult Like _tTextRowDelimeter.Split("|")(0) Or tRowDelimeterOverAllResult Like _tTextRowDelimeter.Split("|")(1) Then
                            oDBdtExcel.Rows.RemoveAt(oDBdtExcel.Rows.Count - 1)
                        End If

                        '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                        If Not oDBdtExcel Is Nothing And oDBdtExcel.Rows.Count > 0 Then

                            Application.DoEvents()

                            _oSplash.UpdateInformation(_tSplashText & Environment.NewLine & "กำลังดำเนินการตรวจสอบข้อมูลเกี่ยวกับ Merchandiser Team, Buy Group And Plant ")
                            Call CheckCurrencyImport(oDBdtExcel)
                            If Not W_PRCbValidateMatchMerchanTeam(oDBdtExcel) Then

                            ElseIf Not W_PRCbValidateMatchBuyGroup(oDBdtExcel) Then


                            ElseIf Not W_PRCbValidateMatchPlant(oDBdtExcel) Then
                                '...รายการแฟ้มข้อมูลโรงงานลูกค้า
                                HI.MG.ShowMsg.mInfo("การกำหนดข้อมูลหลักรายการโรงงานลูกค้าของไฟล์ต้นฉบับไม่ถูกต้อง หรือยังไม่มีการกำหนดข้อมูลหลักรายการโรงงานลูกค้า !!!", 1601012578, Me.Text, , MessageBoxIcon.Warning)
                               
                            ElseIf Not W_PRCbValidateMatchCurrency(oDBdtExcel) Then
                            Else

                                _bCatchSrcFile = True

                                Application.DoEvents()

                                _oSplash.UpdateInformation(_tSplashText & Environment.NewLine & "กำลังดำเนินการตรวจสอบข้อมูล Master กรุณารอสักครู่...")

                                Call W_PRCxValidateMaster(oDBdtExcel)

                                Application.DoEvents()

                                _oSplash.UpdateInformation(_tSplashText & Environment.NewLine & "Load Data To Temp ")

                                Application.DoEvents()

                                If W_PRCbSaveListOrderToTemp(oDBdtExcel) = True Then

                                    '...modify by Tid 2015/03/31
                                    Dim oStrBuilder As New System.Text.StringBuilder()

                                    tSql = " UPDATE XA SET FNGrpSeq = XX.FNGrpSeq"
                                    tSql &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTImportOrderTemp AS XA INNER JOIN"
                                    tSql &= vbCrLf & " 	(SELECT A.FTPONo"
                                    tSql &= vbCrLf & "  , A.FTPOItem"
                                    tSql &= vbCrLf & "   , A.FDShipDate"
                                    tSql &= vbCrLf & "   ,A.FTStyle "
                                    tSql &= vbCrLf & "   , B.FTColorwayCode"
                                    tSql &= vbCrLf & "   ,ROW_NUMBER() Over (PARTITION BY A.FTPONo,A.FTStyle ,A.FDShipDate,B.FTColorwayCode ORDER BY A.FTPOItem) AS FNGrpSeq"
                                    tSql &= vbCrLf & "  FROM     [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTImportOrderTemp AS A WITH (NOLOCK) INNER JOIN"
                                    tSql &= vbCrLf & " 	(SELECT  FTPONo,FTStyle ,FTShipDate,FTColorwayCode,FTPOItem "
                                    tSql &= vbCrLf & "      FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTImportOrderSizeBreakdownTemp WITH (NOLOCK) "
                                    tSql &= vbCrLf & " WHERE FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                                    tSql &= vbCrLf & " GROUP BY FTPONo,FTStyle ,FTShipDate,FTColorwayCode,FTPOItem "
                                    tSql &= vbCrLf & " 	)AS B ON  A.FTPONo = B.FTPONo AND A.FTPOItem = B.FTPOItem AND "
                                    tSql &= vbCrLf & "     A.FDShipDate = B.FTShipDate And A.FTStyle = B.FTStyle"
                                    tSql &= vbCrLf & " WHERE A.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                                    tSql &= vbCrLf & "  ) AS XX ON XA.FTPONo = XX.FTPONo  "
                                    tSql &= vbCrLf & " AND  XA.FTPOItem = XX.FTPOItem "
                                    tSql &= vbCrLf & " AND XA.FDShipDate = XX.FDShipDate "
                                    tSql &= vbCrLf & "  AND XA.FTStyle = XX.FTStyle"
                                    tSql &= vbCrLf & " WHERE XA.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                                    HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN)

                                    tSql = "UPDATE A SET FNPrice = CASE WHEN ISNUMERIC(FTTradingCoGrossUnitPrice) = 1 THEN CONVERT(NUMERIC(18, 4),FTTradingCoGrossUnitPrice)  "
                                    tSql &= vbCrLf & "                  WHEN ISNUMERIC(FTGrossUnitPrice) = 1 THEN CONVERT(NUMERIC(18, 4),FTGrossUnitPrice)  "
                                    tSql &= vbCrLf & "                  WHEN ISNUMERIC(FTTradingCoNetUnitPrice) = 1 THEN CONVERT(NUMERIC(18, 4),FTTradingCoNetUnitPrice)  "
                                    tSql &= vbCrLf & "                  WHEN ISNUMERIC(FTNetUnitPrice) = 1 THEN CONVERT(NUMERIC(18, 4),FTNetUnitPrice)  ELSE 0 END "
                                    tSql &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTImportOrderSizeBreakdownTemp AS A "
                                    tSql &= vbCrLf & " WHERE A.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                                    tSql &= vbCrLf & "       AND A.FNQuantity > 0 "

                                    If HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN) = True Then
                                        'If System.Diagnostics.Debugger.IsAttached = True Then
                                        '    MsgBox("EXECUTE DATA COMPLETE ...", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
                                        'End If
                                    Else
                                        'If System.Diagnostics.Debugger.IsAttached = True Then
                                        '    MsgBox("EXECUTE DATA NOT COMPLETE ...", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
                                        'End If
                                    End If

                                    Application.DoEvents()

                                    If CheckOrderQtyZero() = False Then
                                        Call W_PRCbShowBrowseDataImportFactoryOrder()
                                        Call W_PRCbShowBrowseDataImportFactoryOrderChange()
                                        Call W_PRCbShowBrowseDataImportFactoryOrderRemove()


                                    Else
                                        '...clear source before import order temp
                                        tSql = ""
                                        tSql = "DELETE A"
                                        tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTImportOrderTemp] AS A"
                                        tSql &= Environment.NewLine & "WHERE A.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "';"

                                        If HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN) = True Then
                                        End If

                                        tSql = ""
                                        tSql = "DELETE A"
                                        tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTImportOrderSizeBreakdownTemp] AS A"
                                        tSql &= Environment.NewLine & "WHERE A.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "';"

                                        If HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN) = True Then
                                        End If

                                        _oSplash.Close()

                                        HI.MG.ShowMsg.mInfo("พบข้อมูลจำนวน Order เป็น 0 ไม่สามารถทำการ โหลด File ได้ กรูณาทำการตรวจสอบยอด Order !!!", 1407010001, Me.Text, FNHSysVenderPramId_lbl.Text & "  " & Me.FNHSysVenderPramId.Text, MessageBoxIcon.Warning)

                                        Exit Sub

                                    End If

                                Else
                                    MsgBox("พบข้อผิดพลาดในการตรวจสอบข้อมูลก่อนนำเข้่ารายการใบสั่งผลิต !!!", MsgBoxStyle.OkOnly + MsgBoxStyle.Critical, My.Application.Info.Title)
                                End If

                            End If

                        Else
                            Select Case HI.ST.Lang.Language

                                Case HI.ST.Lang.eLang.TH
                                    MsgBox("ไม่พบข้อมูลสำหรับการนำเข้ารายการใบสั่งผลิตอัตโนมัติ...worksheet 1 !!!", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
                                Case Else
                                    MsgBox("Data not found, for import factory order...worksheet 1 !!!", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
                            End Select
                        End If

           

                End If

                    _oSplash.Close()

                    If Not (_FoundPrm) Then
                        HI.MG.ShowMsg.mInfo("ไม่พบข้อมูล File Program ตามที่ระบุ กรุณาทำการตรวจสอบ File !!!", 140630001, Me.Text, FNHSysVenderPramId_lbl.Text & "  " & Me.FNHSysVenderPramId.Text, MessageBoxIcon.Warning)
                        FNHSysVenderPramId.Focus()
                    End If

                Else
                    MsgBox("Unable to locate source file : " & Me.FTFilePath.Text.ToString(), vbOKOnly + MsgBoxStyle.Exclamation, "Warning")
                  
                End If

            Else
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.FTFilePath_lbl.Text)
             
                Me.FTFilePath.Focus()
            End If

        Catch ex As Exception

            _oSplash.Close()

          
        End Try

    End Sub

    Private Sub ocmclearclsr_Click(sender As Object, e As EventArgs) Handles ocmclearclsr.Click
        Call W_PRCbClsr()
        'Dim nFNCntImport As Integer = PRCnUpdateImportTime(1)
        'MsgBox(String.Format("Import time's : {0}", nFNCntImport), MsgBoxStyle.Information + MsgBoxStyle.OkOnly, My.Application.Info.Title)
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

    Private Sub FNHSysBuyId_KeyPress(sender As Object, e As KeyPressEventArgs)
        Try
            If Asc(e.KeyChar) = 13 Then
                Me.FTFilePath.Focus()
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Try
            'Dim span As TimeSpan = DateTime.Now.Subtract(startTime)

            If Not (_oSplash Is Nothing) And (_oSplash.IsDisposed = False) Then
                _oSplash.Updatetime(Format(Now, "HH:mm:ss"))
                ';  _oSplash = New HI.TL.SplashScreen(_tSplashText)
            End If

            ''_oSplash.UpdateInformation(_tSplashText & " " & String.Format("{0:00}", span.Minutes.ToString) & ":" & String.Format("{0:00}", span.Seconds.ToString) & ":" & String.Format("{0:00}", span.Milliseconds.ToString))

            'Dim TotalSeconds As System.Int64
            'TotalSeconds = TimeSpan.FromMilliseconds(stopWatch.ElapsedMilliseconds).TotalSeconds
            'Seconds = TotalSeconds Mod 60
            'If (TotalSeconds >= 60) Then
            '    Minutes = (TotalSeconds / 60) Mod 60
            'End If
            'If (TotalSeconds >= 60 * 60) Then
            '    Hours = (TotalSeconds / (60 * 60) Mod 24)
            'End If

            'If (TotalSeconds >= 24 * 60 * 60) Then
            '    days = (TotalSeconds / (24 * 60 * 60) Mod 365)
            'End If

            ''Label1.Text = (" days " & days & " Hours " & Hours & " Minutes " & Minutes & " Seconds " & Seconds)

            '_oSplash.UpdateInformation(_tSplashText & " " & " days " & days & " Hours " & Hours & " Minutes " & Minutes & " Seconds " & Seconds)

            REM Label1.Text = Now.TimeOfDay.ToString()

        Catch ex As Exception
        End Try

    End Sub

    Private Sub FTFilePath_EditValueChanged(sender As Object, e As EventArgs) Handles FTFilePath.EditValueChanged
        Try
            Me.otcImportOrderNo.SelectedTabPageIndex = 0

            Me.ogdConfirmImport.DataSource = Nothing
            Me.ogdConfirmImport.Refresh()

            ogdOrderchange.DataSource = Nothing
            ogdOrderchange.Refresh()

            ogdOrderRemove.DataSource = Nothing
            ogdOrderRemove.Refresh()
        Catch ex As Exception

        End Try
       

    End Sub

    Private Sub FNHSysVenderPramId_EditValueChanged(sender As Object, e As EventArgs) Handles FNHSysVenderPramId.EditValueChanged
        FTFilePath.Text = ""
        ogdConfirmImport.DataSource = Nothing
    End Sub


    Private Function CheckOrderQtyZero() As Boolean
        Dim _Qry As String = ""

        _Qry = " SELECT  FTPONo FROM (SELECT  FTPONo, SUM(ISNULL(FNQuantity,0)) AS FNQuantity"
        _Qry &= vbCrLf & "   FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTImportOrderSizeBreakdownTemp WITH (NOLOCK) "
        _Qry &= vbCrLf & "  WHERE  (FTUserLogin ='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "') "
        _Qry &= vbCrLf & "   GROUP BY  FTPONo ) AS A WHERE FNQuantity <= 0 "

        Return (HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MERCHAN, "") <> "")

    End Function

    Private Sub ogvConfirmImport_CellMerge(sender As Object, e As CellMergeEventArgs) Handles ogvConfirmImport.CellMerge

        Try
            With Me.ogvConfirmImport

                Select Case e.Column.FieldName.ToString
                    Case "FTPOItem"
                        If ("" & .GetRowCellValue(e.RowHandle1, "FTPONo").ToString = "" & .GetRowCellValue(e.RowHandle2, "FTPONo").ToString) Then
                            e.Merge = (e.CellValue1.ToString = e.CellValue2.ToString)
                            e.Handled = True
                            e.Column.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
                        Else
                            e.Merge = False
                            e.Handled = True
                        End If
                    Case Else

                        If ("" & .GetRowCellValue(e.RowHandle1, "FTPONo").ToString = "" & .GetRowCellValue(e.RowHandle2, "FTPONo").ToString) _
                         And ("" & .GetRowCellValue(e.RowHandle1, "FTPOItem").ToString = "" & .GetRowCellValue(e.RowHandle2, "FTPOItem").ToString) Then

                            e.Merge = (e.CellValue1.ToString = e.CellValue2.ToString)
                            e.Handled = True
                            e.Column.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
                        Else
                            e.Merge = False
                            e.Handled = True
                        End If
                End Select
               
            End With
        Catch ex As Exception

        End Try

    End Sub

    Private Sub ogvOrderchange_CellMerge(sender As Object, e As CellMergeEventArgs) Handles ogvOrderchange.CellMerge
        Try
            With Me.ogvOrderchange

                Select Case e.Column.FieldName.ToString
                    Case "FTPOItem"
                        If ("" & .GetRowCellValue(e.RowHandle1, "FTPONo").ToString = "" & .GetRowCellValue(e.RowHandle2, "FTPONo").ToString) Then
                            e.Merge = (e.CellValue1.ToString = e.CellValue2.ToString)
                            e.Handled = True
                            e.Column.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
                        Else
                            e.Merge = False
                            e.Handled = True
                        End If
                    Case Else

                        If ("" & .GetRowCellValue(e.RowHandle1, "FTPONo").ToString = "" & .GetRowCellValue(e.RowHandle2, "FTPONo").ToString) _
                         And ("" & .GetRowCellValue(e.RowHandle1, "FTPOItem").ToString = "" & .GetRowCellValue(e.RowHandle2, "FTPOItem").ToString) Then

                            e.Merge = (e.CellValue1.ToString = e.CellValue2.ToString)
                            e.Handled = True
                            e.Column.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
                        Else
                            e.Merge = False
                            e.Handled = True
                        End If
                End Select

            End With
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ogvOrderRemove_CellMerge(sender As Object, e As CellMergeEventArgs) Handles ogvOrderRemove.CellMerge
        Try
            With Me.ogvOrderRemove

                Select Case e.Column.FieldName.ToString
                    Case "FTPOItem"
                        If ("" & .GetRowCellValue(e.RowHandle1, "FTPONo").ToString = "" & .GetRowCellValue(e.RowHandle2, "FTPONo").ToString) Then
                            e.Merge = (e.CellValue1.ToString = e.CellValue2.ToString)
                            e.Handled = True
                            e.Column.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
                        Else
                            e.Merge = False
                            e.Handled = True
                        End If
                    Case Else

                        If ("" & .GetRowCellValue(e.RowHandle1, "FTPONo").ToString = "" & .GetRowCellValue(e.RowHandle2, "FTPONo").ToString) _
                         And ("" & .GetRowCellValue(e.RowHandle1, "FTPOItem").ToString = "" & .GetRowCellValue(e.RowHandle2, "FTPOItem").ToString) Then

                            e.Merge = (e.CellValue1.ToString = e.CellValue2.ToString)
                            e.Handled = True
                            e.Column.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
                        Else
                            e.Merge = False
                            e.Handled = True
                        End If
                End Select

            End With
        Catch ex As Exception

        End Try
    End Sub
End Class
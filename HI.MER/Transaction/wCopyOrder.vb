Imports System
Imports System.Data
Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Windows.Forms
Imports System.Windows.Forms.Control
Imports System.Drawing
Imports System.IO
Imports Microsoft.VisualBasic

Public Class wCopyOrder

#Region "Variable Declaration"
    Private Const _nTotalFactorySubOrderNo As Integer = 26
    Private _tSysDBName As String
    Private _tSysTableName As String
    Private __SystemFilePath As String = Application.StartupPath & IIf(Microsoft.VisualBasic.Right(Application.StartupPath, 1) = "\", "", "\") & "Order"
    Private _tFTOrderNo As String       '...เลขที่ใบสั่งผลิต
    Private _nFNHSysCmpId As Integer
    Private _nFNHSysCmpRunId As Integer
    Private _nFNHSysStyleId As Integer
    Private _tFNHSysCmpId As String     '...รหัสโรงงาน/บริษัท สาขา : Code
    Private _tFNHSysCmpRunId As String  '...รหัสเลข run เอกสาร : Code
    Private _tFNHSysStyleId As String   '...รหัสสไตล์ : Code
    Private tSql As String

    Private _wListCompleteCopyOrder As wListCompleteCopyOrder

    Private _oImage1 As System.Drawing.Image
    Private _oImage2 As System.Drawing.Image
    Private _oImage3 As System.Drawing.Image
    Private _oImage4 As System.Drawing.Image

    Private _FTImage1 As String
    Private _FTImage2 As String
    Private _FTImage3 As String
    Private _FTImage4 As String

#End Region

#Region "Property"
    Private Shared _DTImageRefOrderNo As System.Data.DataTable = Nothing
    Private Shared ReadOnly Property _LoadImageRefOrderNo(ByVal paramOrderNo As String) As System.Data.DataTable
        Get
            If _DTImageRefOrderNo Is Nothing Then
                Dim sSQL As String
                sSQL = ""
                sSQL = "SELECT A.FTOrderNo, A.FTImage1, A.FTImage2, A.FTImage3, A.FTImage4"
                sSQL &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrder AS A (NOLOCK)"
                sSQL &= Environment.NewLine & "WHERE A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(paramOrderNo) & "';"

                _DTImageRefOrderNo = HI.Conn.SQLConn.GetDataTable(sSQL, HI.Conn.DB.DataBaseName.DB_MERCHAN)

            End If

            Return _DTImageRefOrderNo

        End Get

    End Property

#End Region

#Region "Procedure And Function"

    Public Sub New(ByVal ptFTOrderNo As String, ByVal ptSysDBName As String, ByVal ptSysTableName As String)
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.

        _tSysDBName = ptSysDBName
        _tSysTableName = ptSysTableName

        _tFTOrderNo = ptFTOrderNo

        If Not DBNull.Value.Equals(ptFTOrderNo) And ptFTOrderNo <> "" Then
            Call W_PRCbLoadMasterFactoryOrderNo(_tFTOrderNo)

            Me.FTOrderNo.Text = _tFTOrderNo
            Me.FNCopyOrderNo.Value = 1
            Me.FNCopySubOrderNo.Value = 1

            '===================================================== _wListCompleteCopyOrder =======================================================
            _wListCompleteCopyOrder = New wListCompleteCopyOrder(Nothing)

            HI.TL.HandlerControl.AddHandlerObj(_wListCompleteCopyOrder)

            Dim oSysLang As New HI.ST.SysLanguage

            Try
                Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleID, _wListCompleteCopyOrder.Name.ToString.Trim, _wListCompleteCopyOrder)
            Catch ex As Exception
                '...Nothing 
            End Try
            '==================================================================================================================================

        End If

    End Sub

    Private Function W_PRCbCopyOrderNo_BACKUP_20150119() As Boolean

        If W_PRCbValidateBF_CopyOrderNo() = False Then Return False

        Dim oDBdt As DataTable
        Dim oDBdtOrder As DataTable
        Dim oDBdtOrderSub As DataTable
        Dim oDBdtOrderSub_Breakdown As DataTable
        Dim oDBdtOrderSub_Sew As DataTable
        Dim oDBdtOrderSub_Pack As DataTable
        Dim oDBdtOrderSub_SizeSpec As DataTable

        Dim _Spls As HI.TL.SplashScreen

        Dim _FNHSysCmpId As Integer, _FNHSysCmpRunId As Integer, _FNHSysStyleId As Integer, _FNHSysCustId As Integer
        Dim tFDInsDate As String, tFTInsTime As String, tFTInsUser As String
        Dim _tFTOrderNoSrc As String, _tFTOrderNoDest As String, _tFTSubOrderNoSrc As String, _tFTSubOrderNoDest As String
        Dim _tFTMainMaterial As String, _tFTCombination As String
        Dim nFNCopyOrderNo As Integer, nFNCopySubOrderNo As Integer, nLoopCopyOrder As Integer

        Dim _bCopyOrderComplete As Boolean = False

        Try

            _tFTOrderNoSrc = Me.FTOrderNo.Text.Trim()
            _tFTSubOrderNoSrc = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTSubOrderNo FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub] AS A WITH(NOLOCK) WHERE A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(_tFTOrderNoSrc) & "'", Conn.DB.DataBaseName.DB_MERCHAN, "") '_tFTOrderNoSrc & "-A"

            _FNHSysCmpId = Val(Me.FNHSysCmpId.Properties.Tag.ToString())
            _FNHSysCmpRunId = Val(Me.FNHSysCmpRunId.Properties.Tag.ToString())
            _FNHSysStyleId = Val(Me.FNHSysStyleId.Properties.Tag.ToString())

            If Not HI.MG.ShowMsg.mConfirmProcess("", 1403110004, Me.FTOrderNo.Text.Trim()) = True Then Return False

            If Not System.Diagnostics.Debugger.IsAttached = True Then
                _Spls = New HI.TL.SplashScreen("Copy Data...Please Wait ")
            End If

            REM 2014/12/09
            'Dim tmpDTOrderNoImage As System.Data.DataTable

            'tmpDTOrderNoImage = _LoadImageRefOrderNo(_tFTOrderNoSrc)

            'For Each oDataRow As System.Data.DataRow In tmpDTOrderNoImage.Rows
            '    _FTImage1 = oDataRow!FTImage1.ToString
            '    _FTImage2 = oDataRow!FTImage2.ToString
            '    _FTImage3 = oDataRow!FTImage3.ToString
            '    _FTImage4 = oDataRow!FTImage4.ToString

            '    Exit For

            'Next

            'tmpDTOrderNoImage.Dispose()

            nFNCopyOrderNo = Me.FNCopyOrderNo.Value
            nFNCopySubOrderNo = Me.FNCopySubOrderNo.Value

            tSql = "DECLARE @FDInsDate AS VARCHAR(10);"
            tSql &= Environment.NewLine & "DECLARE @FTInsTime AS VARCHAR(8);"
            tSql &= Environment.NewLine & "SELECT @FDInsDate = CONVERT(VARCHAR(10), GETDATE(), 111), @FTInsTime = CONVERT(VARCHAR(10), GETDATE(), 114);"
            tSql &= Environment.NewLine & "SELECT ISNULL(@FDInsDate,'') AS FDInsDate, ISNULL(@FTInsTime, '') AS FTInsTime;"

            oDBdt = HI.Conn.SQLConn.GetDataTable(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN)

            If oDBdt.Rows.Count > 0 Then
                tFDInsDate = "'" & oDBdt.Rows(0).Item("FDInsDate") & "'"
                tFTInsTime = "'" & oDBdt.Rows(0).Item("FTInsTime") & "'"
            Else
                tFDInsDate = HI.UL.ULDate.FormatDateDB
                tFTInsTime = HI.UL.ULDate.FormatTimeDB
            End If

            tFTInsUser = HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName)

            '...Customer System Id by Style Id
            tSql = ""
            tSql = "SELECT A.FNHSysCustId"
            tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMStyle]  AS A WITH(NOLOCK) INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.[TCNMCustomer] AS B WITH(NOLOCK) ON A.FNHSysCustId = B.FNHSysCustId"
            tSql &= Environment.NewLine & "WHERE A.FNHSysStyleId = " & _FNHSysStyleId

            _FNHSysCustId = Val(HI.Conn.SQLConn.GetField(tSql, HI.Conn.DB.DataBaseName.DB_MASTER, "0"))

            _bCopyOrderComplete = True
            nLoopCopyOrder = 1

            Do While (_bCopyOrderComplete) AndAlso (nLoopCopyOrder <= nFNCopyOrderNo)

                _tFTOrderNoDest = HI.TL.Document.GetDocumentNo(_tSysDBName, _tSysTableName, "", False, Me.FNHSysCmpRunId.Text.Trim().ToString()).ToString()

                HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_MERCHAN)
                HI.Conn.SQLConn.SqlConnectionOpen()
                HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
                HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

                Try
                    REM 2014/12/09
                    'Me._oImage1 = HI.UL.ULImage.LoadImage("" & __SystemFilePath & "\" & _FTImage1)
                    'Me._oImage2 = HI.UL.ULImage.LoadImage("" & __SystemFilePath & "\" & _FTImage2)
                    'Me._oImage3 = HI.UL.ULImage.LoadImage("" & __SystemFilePath & "\" & _FTImage3)
                    'Me._oImage4 = HI.UL.ULImage.LoadImage("" & __SystemFilePath & "\" & _FTImage4)

                    tSql = ""
                    REM 2014/12/09
                    'tSql = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrder] (FTInsUser, FDInsDate, FTInsTime, FTUpdUser, FDUpdDate, FTUpdTime"
                    'tSql &= vbCrLf & ", FTOrderNo, FDOrderDate, FTOrderBy, FNOrderType, FNHSysCmpId, FNHSysCmpRunId, FNHSysStyleId, FTPORef, FNHSysCustId, "
                    'tSql &= vbCrLf & " FNHSysAgencyId, FNHSysProdTypeId, FNHSysBuyerId, FTMainMaterial, FTCombination, FTRemark, FNOrderQty, FNExtraQty, FNOrderAmt, FNGrandQty, FNGrandAmt, FNHSysBrandId, FNHSysBuyId, "
                    'tSql &= vbCrLf & "     FNJobState, FNHSysPlantId, FNHSysBuyGrpId, FNHSysMerTeamId, FTImage1, FTImage2, FTImage3, FTImage4)"
                    'tSql &= vbCrLf & "SELECT TOP 1 '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    'tSql &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ",'" & HI.UL.ULF.rpQuoted(_tFTOrderNoDest) & "'"
                    'tSql &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    'tSql &= vbCrLf & ", FNOrderType," & _FNHSysCmpId & "," & _FNHSysCmpRunId & ", " & _FNHSysStyleId & ", FTPORef," & _FNHSysCustId & " "
                    'tSql &= vbCrLf & ", FNHSysAgencyId, FNHSysProdTypeId, FNHSysBuyerId, FTMainMaterial, FTCombination, FTRemark, 0 AS  FNOrderQty"
                    'tSql &= vbCrLf & ", 0 AS  FNExtraQty, 0 AS  FNOrderAmt, 0 AS  FNGrandQty, 0 AS  FNGrandAmt, FNHSysBrandId, FNHSysBuyId"
                    'tSql &= vbCrLf & ", 0 AS FNJobState,  FNHSysPlantId, FNHSysBuyGrpId, FNHSysMerTeamId"
                    'tSql &= vbCrLf & ", '" & HI.UL.ULImage.SaveImage(Me._oImage1, _tFTOrderNoDest & "_FTImage1".ToString, __SystemFilePath) & "'"
                    'tSql &= vbCrLf & ", '" & HI.UL.ULImage.SaveImage(Me._oImage2, _tFTOrderNoDest & "_FTImage2".ToString, __SystemFilePath) & "'"
                    'tSql &= vbCrLf & ", '" & HI.UL.ULImage.SaveImage(Me._oImage3, _tFTOrderNoDest & "_FTImage3".ToString, __SystemFilePath) & "'"
                    'tSql &= vbCrLf & ", '" & HI.UL.ULImage.SaveImage(Me._oImage4, _tFTOrderNoDest & "_FTImage4".ToString, __SystemFilePath) & "'"
                    'tSql &= vbCrLf & "FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrder] WITH(NOLOCK)"
                    'tSql &= vbCrLf & "WHERE FTOrderNo = '" & HI.UL.ULF.rpQuoted(_tFTOrderNoSrc) & "'"

                    REM 2014/12/18
                    'tSql = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrder] (FTInsUser, FDInsDate, FTInsTime, FTUpdUser, FDUpdDate, FTUpdTime"
                    'tSql &= vbCrLf & ", FTOrderNo, FDOrderDate, FTOrderBy, FNOrderType, FNHSysCmpId, FNHSysCmpRunId, FNHSysStyleId, FTPORef, FNHSysCustId, "
                    'tSql &= vbCrLf & " FNHSysAgencyId, FNHSysProdTypeId, FNHSysBuyerId, FTMainMaterial, FTCombination, FTRemark, FNOrderQty, FNExtraQty, FNOrderAmt, FNGrandQty, FNGrandAmt, FNHSysBrandId, FNHSysBuyId, "
                    'tSql &= vbCrLf & "     FNJobState, FNHSysPlantId, FNHSysBuyGrpId, FNHSysMerTeamId, FPOrderImage1, FPOrderImage2, FPOrderImage3, FPOrderImage4)"
                    'tSql &= vbCrLf & "SELECT TOP 1 '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    'tSql &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ",'" & HI.UL.ULF.rpQuoted(_tFTOrderNoDest) & "'"
                    'tSql &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    'tSql &= vbCrLf & ", FNOrderType," & _FNHSysCmpId & "," & _FNHSysCmpRunId & ", " & _FNHSysStyleId & ", FTPORef," & _FNHSysCustId & " "
                    'tSql &= vbCrLf & ", FNHSysAgencyId, FNHSysProdTypeId, FNHSysBuyerId, FTMainMaterial, FTCombination, FTRemark, 0 AS  FNOrderQty"
                    'tSql &= vbCrLf & ", 0 AS  FNExtraQty, 0 AS  FNOrderAmt, 0 AS  FNGrandQty, 0 AS  FNGrandAmt, FNHSysBrandId, FNHSysBuyId"
                    'tSql &= vbCrLf & ", 0 AS FNJobState,  FNHSysPlantId, FNHSysBuyGrpId, FNHSysMerTeamId, FPOrderImage1, FPOrderImage2, FPOrderImage3, FPOrderImage4"
                    'tSql &= vbCrLf & "FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrder] WITH(NOLOCK)"
                    'tSql &= vbCrLf & "WHERE FTOrderNo = '" & HI.UL.ULF.rpQuoted(_tFTOrderNoSrc) & "'"

                    tSql = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrder] (FTInsUser, FDInsDate, FTInsTime, FTUpdUser, FDUpdDate, FTUpdTime"
                    tSql &= vbCrLf & ", FTOrderNo, FDOrderDate, FTOrderBy, FNOrderType, FNHSysCmpId, FNHSysCmpRunId, FNHSysStyleId, FTPORef, FNHSysCustId, "
                    tSql &= vbCrLf & " FNHSysAgencyId, FNHSysProdTypeId, FNHSysBuyerId, FTMainMaterial, FTCombination, FTRemark, FNHSysBrandId, FNHSysBuyId, "
                    tSql &= vbCrLf & "     FNJobState, FNHSysPlantId, FNHSysBuyGrpId, FNHSysMerTeamId, FPOrderImage1, FPOrderImage2, FPOrderImage3, FPOrderImage4)"
                    tSql &= vbCrLf & "SELECT TOP 1 '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    tSql &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ",'" & HI.UL.ULF.rpQuoted(_tFTOrderNoDest) & "'"
                    tSql &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    tSql &= vbCrLf & ", FNOrderType," & _FNHSysCmpId & "," & _FNHSysCmpRunId & ", " & _FNHSysStyleId & ", FTPORef," & _FNHSysCustId & " "
                    tSql &= vbCrLf & ", FNHSysAgencyId, FNHSysProdTypeId, FNHSysBuyerId, FTMainMaterial, FTCombination, FTRemark, FNHSysBrandId, FNHSysBuyId"
                    tSql &= vbCrLf & ", 0 AS FNJobState,  FNHSysPlantId, FNHSysBuyGrpId, FNHSysMerTeamId, FPOrderImage1, FPOrderImage2, FPOrderImage3, FPOrderImage4"
                    tSql &= vbCrLf & "FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrder] WITH(NOLOCK)"
                    tSql &= vbCrLf & "WHERE FTOrderNo = '" & HI.UL.ULF.rpQuoted(_tFTOrderNoSrc) & "'"

                    If HI.Conn.SQLConn.Execute_Tran(tSql, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        _bCopyOrderComplete = False

                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    End If

                    If _bCopyOrderComplete = True Then
                        HI.Conn.SQLConn.Tran.Commit()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                        '...update FPOrderImageXXX
                        'If System.Diagnostics.Debugger.IsAttached = True Then
                        '    '...Modify Save Factory Order No ImageXXX : represent image to database merchan TMERTOrder
                        '    '================================================================================================================
                        '    HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(HI.Conn.DB.DataBaseName.DB_MERCHAN)
                        '    HI.Conn.SQLConn.SqlConnectionOpen()

                        '    tSql = ""
                        '    tSql = "UPDATE A"
                        '    tSql &= Environment.NewLine & "SET A.[FPOrderImage1] = @FPOrderImage1,"
                        '    tSql &= Environment.NewLine & "    A.[FPOrderImage2] = @FPOrderImage2,"
                        '    tSql &= Environment.NewLine & "    A.[FPOrderImage3] = @FPOrderImage3,"
                        '    tSql &= Environment.NewLine & "    A.[FPOrderImage4] = @FPOrderImage4"
                        '    tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrder] AS A"
                        '    tSql &= Environment.NewLine & "WHERE  A.FTOrderNo = @FTOrderNo"

                        '    Dim cmd As New SqlCommand(tSql, HI.Conn.SQLConn.Cnn)

                        '    cmd.Parameters.AddWithValue("FTOrderNo", _tFTOrderNoDest)

                        '    '...FPOrderImage1
                        '    '---------------------------------------------------------------------------------------------------------------------------------------
                        '    Dim msFPOrderImage1 As New MemoryStream()
                        '    Dim data As Byte() = Nothing

                        '    For Each Obj As Object In Me.Controls.Find("FTImage1", True)
                        '        Select Case HI.ENM.Control.GeTypeControl(Obj)
                        '            Case ENM.Control.ControlType.PictureEdit
                        '                Obj.Image.Save(msFPOrderImage1, System.Drawing.Imaging.ImageFormat.Jpeg)
                        '                data = msFPOrderImage1.GetBuffer()
                        '        End Select
                        '    Next

                        '    If Not DBNull.Value.Equals(data) Then
                        '        cmd.Parameters.AddWithValue("FPOrderImage1", data)
                        '    Else
                        '        cmd.Parameters.AddWithValue("FPOrderImage1", DBNull.Value)
                        '    End If
                        '    '----------------------------------------------------------------------------------------------------------------------------------------

                        '    '...FPOrderImage2
                        '    '----------------------------------------------------------------------------------------------------------------------------------------
                        '    Dim msFPOrderImage2 As New MemoryStream()
                        '    Dim data2 As Byte() = Nothing

                        '    For Each Obj As Object In Me.Controls.Find("FTImage2", True)
                        '        Select Case HI.ENM.Control.GeTypeControl(Obj)
                        '            Case ENM.Control.ControlType.PictureEdit
                        '                'data2 = HI.UL.ULImage.ConvertImageToByteArray(CType(Obj, DevExpress.XtraEditors.PictureEdit).Image, UL.ULImage.PicType.Nothing)
                        '                Obj.Image.Save(msFPOrderImage2, System.Drawing.Imaging.ImageFormat.Jpeg)
                        '                data2 = msFPOrderImage2.GetBuffer()
                        '        End Select
                        '    Next

                        '    If Not DBNull.Value.Equals(data) Then
                        '        cmd.Parameters.AddWithValue("FPOrderImage2", data2)
                        '    Else
                        '        cmd.Parameters.AddWithValue("FPOrderImage2", DBNull.Value)
                        '    End If
                        '    '----------------------------------------------------------------------------------------------------------------------------------------

                        '    '...@FPOrderImage3
                        '    '----------------------------------------------------------------------------------------------------------------------------------------
                        '    Dim msFPOrderImage3 As New MemoryStream()
                        '    Dim data3 As Byte() = Nothing

                        '    For Each Obj As Object In Me.Controls.Find("FTImage3", True)
                        '        Select Case HI.ENM.Control.GeTypeControl(Obj)
                        '            Case ENM.Control.ControlType.PictureEdit
                        '                Obj.Image.Save(msFPOrderImage3, System.Drawing.Imaging.ImageFormat.Jpeg)
                        '                data3 = msFPOrderImage3.GetBuffer()
                        '        End Select
                        '    Next

                        '    If Not DBNull.Value.Equals(data) Then
                        '        cmd.Parameters.AddWithValue("FPOrderImage3", data3)
                        '    Else
                        '        cmd.Parameters.AddWithValue("FPOrderImage3", DBNull.Value)
                        '    End If
                        '    '----------------------------------------------------------------------------------------------------------------------------------------

                        '    '...@FPOrderImage4
                        '    '----------------------------------------------------------------------------------------------------------------------------------------
                        '    Dim msFPOrderImage4 As New MemoryStream()
                        '    Dim data4 As Byte() = Nothing

                        '    For Each Obj As Object In Me.Controls.Find("FTImage4", True)
                        '        Select Case HI.ENM.Control.GeTypeControl(Obj)
                        '            Case ENM.Control.ControlType.PictureEdit
                        '                Obj.Image.Save(msFPOrderImage4, System.Drawing.Imaging.ImageFormat.Jpeg)
                        '                data4 = msFPOrderImage4.GetBuffer()
                        '        End Select
                        '    Next

                        '    If Not DBNull.Value.Equals(data) Then
                        '        cmd.Parameters.AddWithValue("FPOrderImage4", data4)
                        '    Else
                        '        cmd.Parameters.AddWithValue("FPOrderImage4", DBNull.Value)
                        '    End If
                        '    '----------------------------------------------------------------------------------------------------------------------------------------
                        '    cmd.CommandType = CommandType.Text

                        '    cmd.ExecuteNonQuery()

                        '    cmd.Parameters.Clear()

                        '    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cnn)

                        'End If

                    End If

                Catch ex As Exception
                    _bCopyOrderComplete = False

                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                End Try

                If _bCopyOrderComplete = True Then

                    ''...TMERTOrder_BreakDown
                    'tSql = ""
                    'tSql = "DELETE A"
                    'tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.[TMERTOrder_BreakDown] AS A"
                    'tSql &= Environment.NewLine & "WHERE  A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(_tFTOrderNoDest) & "'"

                    'HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN)

                    'tSql = ""
                    'tSql = "DELETE A"
                    'tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub] AS A"
                    'tSql &= Environment.NewLine & "WHERE A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(_tFTOrderNoDest) & "'"
                    ''  tSql &= Environment.NewLine & "      AND A.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(_tFTSubOrderNoDest) & "'"

                    'HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN)

                    'tSql = ""
                    'tSql = "DELETE A"
                    'tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub_BreakDown] AS A"
                    'tSql &= Environment.NewLine & "WHERE A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(_tFTOrderNoDest) & "'"
                    '' tSql &= Environment.NewLine & "      AND A.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(_tFTSubOrderNoDest) & "'"

                    'HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN)

                    '...Atleast One Copy Factory Sub Order No.
                    If nFNCopySubOrderNo > 0 Then

                        Dim nLoopCopySubOrder As Integer
                        For nLoopCopySubOrder = 1 To nFNCopySubOrderNo
                            tSql = ""
                            tSql = "EXEC SP_GEN_CHARACTER_SubOrderNo '" & HI.UL.ULF.rpQuoted(_tFTOrderNoDest) & "'"

                            _tFTSubOrderNoDest = HI.Conn.SQLConn.GetField(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN, "")

                            REM 2014/12/18 drop field amount, qty TMERTOrderSub,
                            'tSql = ""
                            'tSql = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTOrderSub] (FTInsUser, FDInsDate, FTInsTime, FTUpdUser, FDUpdDate, FTUpdTime"
                            'tSql &= vbCrLf & ", FTOrderNo, FTSubOrderNo, FDSubOrderDate, FTSubOrderBy, FDProDate, FDShipDate, FNHSysBuyId, FNHSysContinentId"
                            'tSql &= vbCrLf & ", FNHSysCountryId, FNHSysProvinceId, FNHSysShipModeId, FNHSysCurId, FNHSysGenderId, FNHSysUnitId, FNSubOrderState"
                            'tSql &= vbCrLf & ", FTStateEmb, FTStatePrint, FTStateHeat, FTStateLaser, FTStateWindows,FTStateOther1"
                            'tSql &= vbCrLf & ", FTOther1Note, FTStateOther2, FTOther2Note, FTStateOther3, FTOther3Note1, FTRemark, FNSubOrderQty, FNSubOrderAmt, FNHSysShipPortId"
                            'tSql &= vbCrLf & ", FNSubOrderExtraQty, FNSubOrderExtraAmt,FDShipDateOrginal, FTCustRef, FNSubOrderGarmentTestQty, FNSubOrderGarmentTestAmnt)"
                            'tSql &= vbCrLf & "SELECT TOP 1 '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                            'tSql &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ",'" & HI.UL.ULF.rpQuoted(_tFTOrderNoDest) & "','" & HI.UL.ULF.rpQuoted(_tFTSubOrderNoDest) & "'"
                            'tSql &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                            'tSql &= vbCrLf & " , FDProDate, FDShipDate, FNHSysBuyId, FNHSysContinentId, "
                            'tSql &= vbCrLf & " FNHSysCountryId, FNHSysProvinceId, FNHSysShipModeId, FNHSysCurId, FNHSysGenderId, FNHSysUnitId, FNSubOrderState, FTStateEmb, FTStatePrint, FTStateHeat, FTStateLaser, FTStateWindows,"
                            'tSql &= vbCrLf & " FTStateOther1, FTOther1Note, FTStateOther2, FTOther2Note, FTStateOther3, FTOther3Note1, FTRemark,0 AS  FNSubOrderQty,0 AS  FNSubOrderAmt, FNHSysShipPortId,0 AS  FNSubOrderExtraQty,0 AS  FNSubOrderExtraAmt,"
                            'tSql &= vbCrLf & " FDShipDateOrginal, FTCustRef,0 AS  FNSubOrderGarmentTestQty,0 AS  FNSubOrderGarmentTestAmnt"
                            'tSql &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTOrderSub] WITH(NOLOCK)"
                            'tSql &= vbCrLf & "WHERE FTOrderNo = '" & HI.UL.ULF.rpQuoted(_tFTOrderNoSrc) & "'"

                            tSql = ""
                            tSql = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTOrderSub] (FTInsUser, FDInsDate, FTInsTime, FTUpdUser, FDUpdDate, FTUpdTime"
                            tSql &= vbCrLf & ", FTOrderNo, FTSubOrderNo, FDSubOrderDate, FTSubOrderBy, FDProDate, FDShipDate, FNHSysBuyId, FNHSysContinentId"
                            tSql &= vbCrLf & ", FNHSysCountryId, FNHSysProvinceId, FNHSysShipModeId, FNHSysCurId, FNHSysGenderId, FNHSysUnitId, FNSubOrderState"
                            tSql &= vbCrLf & ", FTStateEmb, FTStatePrint, FTStateHeat, FTStateLaser, FTStateWindows,FTStateOther1"
                            tSql &= vbCrLf & ", FTOther1Note, FTStateOther2, FTOther2Note, FTStateOther3, FTOther3Note1, FTRemark, FNHSysShipPortId"
                            tSql &= vbCrLf & ", FDShipDateOrginal, FTCustRef)"
                            tSql &= vbCrLf & "SELECT TOP 1 '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                            tSql &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ",'" & HI.UL.ULF.rpQuoted(_tFTOrderNoDest) & "','" & HI.UL.ULF.rpQuoted(_tFTSubOrderNoDest) & "'"
                            tSql &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                            tSql &= vbCrLf & " , FDProDate, FDShipDate, FNHSysBuyId, FNHSysContinentId, "
                            tSql &= vbCrLf & " FNHSysCountryId, FNHSysProvinceId, FNHSysShipModeId, FNHSysCurId, FNHSysGenderId, FNHSysUnitId, FNSubOrderState, FTStateEmb, FTStatePrint, FTStateHeat, FTStateLaser, FTStateWindows,"
                            tSql &= vbCrLf & " FTStateOther1, FTOther1Note, FTStateOther2, FTOther2Note, FTStateOther3, FTOther3Note1, FTRemark, FNHSysShipPortId,"
                            tSql &= vbCrLf & " FDShipDateOrginal, FTCustRef"
                            tSql &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTOrderSub] WITH(NOLOCK)"
                            tSql &= vbCrLf & "WHERE FTOrderNo = '" & HI.UL.ULF.rpQuoted(_tFTOrderNoSrc) & "'"

                            HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN)

                            '...TMERTOrderSub_Breakdown
                            tSql = ""
                            tSql = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub_BreakDown]"
                            tSql &= vbCrLf & " ("
                            tSql &= vbCrLf & " FTInsUser, FDInsDate, FTInsTime, FTUpdUser, FDUpdDate, FTUpdTime"
                            tSql &= vbCrLf & " , FTOrderNo, FTSubOrderNo, FTColorway, FTSizeBreakDown, FNPrice, FNQuantity, FNAmt, FNHSysMatColorId,"
                            tSql &= vbCrLf & "  FNHSysMatSizeId, FNExtraQty, FNQuantityExtra, FNGrandQuantity, FNAmntExtra, FNGrandAmnt, FNGarmentQtyTest, FNAmntQtyTest"
                            tSql &= vbCrLf & " )"
                            tSql &= vbCrLf & "SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ""
                            tSql &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ""
                            tSql &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_tFTOrderNoDest) & "','" & HI.UL.ULF.rpQuoted(_tFTSubOrderNoDest) & "'"
                            tSql &= vbCrLf & ",FTColorway, FTSizeBreakDown, FNPrice,0 AS  FNQuantity,0 AS  FNAmt, FNHSysMatColorId, "
                            tSql &= vbCrLf & "  FNHSysMatSizeId, 0 AS  FNExtraQty, 0 AS  FNQuantityExtra,0 AS   FNGrandQuantity,0 AS  FNAmntExtra,0 AS  FNGrandAmnt,0 AS  FNGarmentQtyTest, 0 AS  FNAmntQtyTest"
                            tSql &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub_BreakDown] WITH(NOLOCK) "
                            tSql &= vbCrLf & "WHERE FTSubOrderNo = '" & HI.UL.ULF.rpQuoted(_tFTSubOrderNoSrc) & "' "

                            HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN)

                            '...TMERTOrderSub_Sew
                            tSql = ""
                            tSql = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub_Sew]"
                            tSql &= vbCrLf & "  ( "
                            tSql &= vbCrLf & " FTInsUser, FDInsDate, FTInsTime, FTUpdUser, FDUpdDate, FTUpdTime, FTOrderNo, FTSubOrderNo, FNSewSeq, FTSewDescription, FTSewNote, FTImage"
                            tSql &= vbCrLf & "  )"
                            tSql &= vbCrLf & "SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ""
                            tSql &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ""
                            tSql &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_tFTOrderNoDest) & "','" & HI.UL.ULF.rpQuoted(_tFTSubOrderNoDest) & "', FNSewSeq, FTSewDescription, FTSewNote, FTImage"
                            tSql &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub_Sew] WITH(NOLOCK) "
                            tSql &= vbCrLf & "WHERE FTSubOrderNo = '" & HI.UL.ULF.rpQuoted(_tFTSubOrderNoSrc) & "' "
                            HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN)

                            tSql = ""
                            tSql = "SELECT  A.FTOrderNo, A.FTSubOrderNo, A.FNSewSeq, A.FTImage"
                            tSql &= Environment.NewLine & "FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub_Sew AS A (NOLOCK)"
                            tSql &= Environment.NewLine & "WHERE  (A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(_tFTOrderNoDest) & "') AND (A.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(_tFTSubOrderNoDest) & "')"
                            tSql &= Environment.NewLine & "ORDER BY A.FNSewSeq ASC;"

                            Dim tmpDTSew As System.Data.DataTable

                            tmpDTSew = HI.Conn.SQLConn.GetDataTable(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN)

                            For Each oDataRowSew As System.Data.DataRow In tmpDTSew.Rows
                                Dim oImageSew As System.Drawing.Image
                                Dim FTImageSew As String
                                Dim FNSewSeq As Integer

                                FTImageSew = oDataRowSew!FTImage.ToString
                                FNSewSeq = Val(oDataRowSew!FNSewSeq.ToString)

                                oImageSew = HI.UL.ULImage.LoadImage("" & __SystemFilePath & "\OrderNo\SubOrderNo\Sewing\" & FTImageSew)

                                If Not oImageSew Is Nothing Then
                                    FTImageSew = _tFTSubOrderNoDest & "_" & FNSewSeq.ToString
                                    FTImageSew = Microsoft.VisualBasic.Replace(FTImageSew, "-", "_")

                                    tSql = ""
                                    tSql = "UPDATE A"
                                    tSql &= Environment.NewLine & "SET A.FTImage = '" & HI.UL.ULImage.SaveImage(oImageSew, FTImageSew, "" & __SystemFilePath & "\OrderNo\SubOrderNo\Sewing\") & "'"
                                    tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub_Sew AS A"
                                    tSql &= Environment.NewLine & "WHERE A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(_tFTOrderNoDest) & "'"
                                    tSql &= Environment.NewLine & "      AND A.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(_tFTSubOrderNoDest) & "'"
                                    tSql &= Environment.NewLine & "      AND A.FNSewSeq = " & FNSewSeq & ";"

                                    HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN)

                                End If

                            Next

                            tmpDTSew.Dispose()

                            '...TMERTOrderSub_Pack
                            tSql = ""
                            tSql = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub_Pack]"
                            tSql &= vbCrLf & "  ( "
                            tSql &= vbCrLf & " FTInsUser, FDInsDate, FTInsTime, FTUpdUser, FDUpdDate, FTUpdTime, FTOrderNo, FTSubOrderNo, FNPackSeq, FTPackDescription, FTPackNote, FTImage "
                            tSql &= vbCrLf & "  )"
                            tSql &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ""
                            tSql &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ""
                            tSql &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_tFTOrderNoDest) & "','" & HI.UL.ULF.rpQuoted(_tFTSubOrderNoDest) & "', FNPackSeq, FTPackDescription, FTPackNote, FTImage "
                            tSql &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub_Pack] WITH(NOLOCK) "
                            tSql &= vbCrLf & "  WHERE FTSubOrderNo='" & HI.UL.ULF.rpQuoted(_tFTSubOrderNoSrc) & "' "
                            HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN)

                            tSql = ""
                            tSql = "SELECT  A.FTOrderNo, A.FTSubOrderNo, A.FNPackSeq, A.FTImage"
                            tSql &= Environment.NewLine & "FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub_Pack AS A (NOLOCK)"
                            tSql &= Environment.NewLine & "WHERE  (A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(_tFTOrderNoDest) & "') AND (A.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(_tFTSubOrderNoDest) & "')"
                            tSql &= Environment.NewLine & "ORDER BY A.FNPackSeq ASC;"

                            Dim tmpDTPack As System.Data.DataTable

                            tmpDTPack = HI.Conn.SQLConn.GetDataTable(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN)

                            For Each oDataRowPack As System.Data.DataRow In tmpDTPack.Rows
                                Dim oImagePack As System.Drawing.Image
                                Dim FTImagePack As String
                                Dim FNPackSeq As Integer

                                FTImagePack = oDataRowPack!FTImage.ToString
                                FNPackSeq = Val(oDataRowPack!FNPackSeq.ToString)
                                oImagePack = HI.UL.ULImage.LoadImage("" & __SystemFilePath & "\OrderNo\SubOrderNo\Packing\" & FTImagePack)

                                If Not oImagePack Is Nothing Then
                                    FTImagePack = _tFTSubOrderNoDest & "_" & FNPackSeq.ToString
                                    FTImagePack = Microsoft.VisualBasic.Replace(FTImagePack, "-", "_")

                                    tSql = ""
                                    tSql = "UPDATE A"
                                    tSql &= Environment.NewLine & "SET A.FTImage = '" & HI.UL.ULImage.SaveImage(oImagePack, FTImagePack, "" & __SystemFilePath & "\OrderNo\SubOrderNo\Packing\") & "'"
                                    tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub_Pack AS A "
                                    tSql &= Environment.NewLine & "WHERE A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(_tFTOrderNoDest) & "'"
                                    tSql &= Environment.NewLine & "      AND A.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(_tFTSubOrderNoDest) & "'"
                                    tSql &= Environment.NewLine & "      AND A.FNPackSeq = " & FNPackSeq & ";"

                                    HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN)

                                End If

                            Next

                            tmpDTPack.Dispose()

                            '..TMERTOrderSub_Bundle
                            tSql = ""
                            tSql = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTOrderSub_Bundle]([FTInsUser], [FDInsDate], [FTInsTime], [FTOrderNo], [FTSubOrderNo], [FTColorway], [FTSizeBreakDown], [FNQuantity])"
                            tSql &= Environment.NewLine & "SELECT N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' AS FTInsUser, " & HI.UL.ULDate.FormatDateDB & " AS FDInsDate, " & HI.UL.ULDate.FormatTimeDB & " AS FTInsTime, N'" & HI.UL.ULF.rpQuoted(_tFTOrderNoDest) & "' AS FTOrderNo, N'" & HI.UL.ULF.rpQuoted(_tFTSubOrderNoDest) & "' AS FTSubOrderNo, A.[FTColorway], A.[FTSizeBreakDown], 0 AS FNQuantity"
                            tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTOrderSub_Bundle] AS A (NOLOCK)"
                            tSql &= Environment.NewLine & "WHERE A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(_tFTOrderNoSrc) & "'"
                            tSql &= Environment.NewLine & "      AND A.FTSubOrderNO = N'" & HI.UL.ULF.rpQuoted(_tFTSubOrderNoSrc) & "';"



                            '...TMERTOrderSub_SizeSpec
                            tSql = ""
                            tSql = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub_SizeSpec]"
                            tSql &= vbCrLf & "  ( "
                            tSql &= vbCrLf & " FTInsUser, FDInsDate, FTInsTime, FTUpdUser, FDUpdDate, FTUpdTime, FTOrderNo, FTSubOrderNo, FNSeq, FNHSysMatSizeId, FTSizeSpecDesc, FTSizeSpecExtension "
                            tSql &= vbCrLf & "  )"
                            tSql &= vbCrLf & "SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ""
                            tSql &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ""
                            tSql &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_tFTOrderNoDest) & "','" & HI.UL.ULF.rpQuoted(_tFTSubOrderNoDest) & "', FNSeq, FNHSysMatSizeId, FTSizeSpecDesc, FTSizeSpecExtension "
                            tSql &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub_SizeSpec] WITH(NOLOCK) "
                            tSql &= vbCrLf & "WHERE FTSubOrderNo = '" & HI.UL.ULF.rpQuoted(_tFTSubOrderNoSrc) & "' "

                            HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN)

                            '...TMERTOrderSub_Component
                            tSql = ""
                            tSql = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub_Component]"
                            tSql &= vbCrLf & "  ( "
                            tSql &= vbCrLf & " FTInsUser, FDInsDate, FTInsTime, FTUpdUser, FDUpdDate, FTUpdTime, FTOrderNo, FTSubOrderNo, FNHSysMerMatId, FNPart, FTComponent, FTRemark, FNConSmp,FNSeq "
                            tSql &= vbCrLf & "  )"
                            tSql &= vbCrLf & "SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ""
                            tSql &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ""
                            tSql &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_tFTOrderNoDest) & "','" & HI.UL.ULF.rpQuoted(_tFTSubOrderNoDest) & "', FNHSysMerMatId, FNPart, FTComponent, FTRemark, FNConSmp,FNSeq "
                            tSql &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub_Component] WITH(NOLOCK) "
                            tSql &= vbCrLf & "WHERE FTSubOrderNo = '" & HI.UL.ULF.rpQuoted(_tFTSubOrderNoSrc) & "' "

                            HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN)


                        Next nLoopCopySubOrder

                    End If

                    REM 2014/12/16 HITECH_MERCHAN..TMERTOrder_BreakDown NOT USE
                    '=============================================================================================================================================================
                    'tSql = ""
                    'tSql = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrder_BreakDown]"
                    'tSql &= vbCrLf & " ("
                    'tSql &= vbCrLf & " FTInsUser, FDInsDate, FTInsTime, FTUpdUser, FDUpdDate, FTUpdTime"
                    'tSql &= vbCrLf & " , FTOrderNo, FTColorway, FTSizeBreakDown, FNPrice, FNQuantity, FNAmt, FNExtraQuantity, FNHSysMatColorId "
                    'tSql &= vbCrLf & " ,FNHSysMatSizeId, FNGarmentQtyTest, FNAmntQtyTest, FNGrandQuantity, FNAmntExtra, FNGrandAmnt"
                    'tSql &= vbCrLf & " )"
                    'tSql &= vbCrLf & "SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ""
                    'tSql &= vbCrLf & ", '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ""
                    'tSql &= vbCrLf & ", '" & HI.UL.ULF.rpQuoted(_tFTOrderNoDest) & "'"
                    'tSql &= vbCrLf & ", FTColorway, FTSizeBreakDown, FNPrice,0 AS  FNQuantity,0 AS  FNAmt,0 AS  FNExtraQuantity,FNHSysMatColorId "
                    'tSql &= vbCrLf & ", FNHSysMatSizeId,0 AS  FNGarmentQtyTest,0 AS  FNAmntQtyTest,0 AS  FNGrandQuantity,0 AS  FNAmntExtra,0 AS  FNGrandAmnt"
                    'tSql &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrder_BreakDown] WITH(NOLOCK) "
                    'tSql &= vbCrLf & "WHERE FTOrderNo = '" & HI.UL.ULF.rpQuoted(_tFTOrderNoSrc) & "' "

                    'HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN)

                    '=============================================================================================================================================================


                End If

                nLoopCopyOrder = nLoopCopyOrder + 1

                Application.DoEvents()

            Loop

            If Not System.Diagnostics.Debugger.IsAttached = True Then
                _Spls.Close()
            End If

            If (_bCopyOrderComplete = True) And ((nLoopCopyOrder - 1) = nFNCopyOrderNo) Then
                'Select Case HI.ST.Lang.Language
                '    Case HI.ST.Lang.Lang.TH
                '        HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, "คัดลอกรายการใบสั่งผลิตเรียบร้อยแล้ว...")
                '    Case Else
                '        HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, "Copy Factory Order No. complete...")
                'End Select

                Select Case HI.ST.Lang.Language
                    Case HI.ST.Lang.eLang.TH
                        HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, "คัดลอกรายการใบสั่งผลิตเรียบร้อยแล้ว...")
                    Case HI.ST.Lang.eLang.EN
                        HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, "Copy Factory Order No. complete...")
                    Case Else

                End Select

            End If

        Catch ex As Exception

            If Not System.Diagnostics.Debugger.IsAttached = True Then
                _Spls.Close()
            Else
                MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.OkOnly, My.Application.Info.Title)
            End If

        End Try

        Return _bCopyOrderComplete

    End Function

    Private Function W_PRCbCopyOrderNo() As Boolean

        If W_PRCbValidateBF_CopyOrderNo() = False Then Return False

        Dim oDBdt As System.Data.DataTable
        Dim oDBdtOrder As System.Data.DataTable
        Dim oDBdtOrderSub As System.Data.DataTable
        Dim oDBdtOrderSub_Breakdown As System.Data.DataTable
        Dim oDBdtOrderSub_Sew As System.Data.DataTable
        Dim oDBdtOrderSub_Pack As System.Data.DataTable
        Dim oDBdtOrderSub_SizeSpec As System.Data.DataTable

        Dim tmpDTListCopyOrderComplete As System.Data.DataTable

        Dim _Spls As HI.TL.SplashScreen

        Dim _FNHSysCmpId As Integer, _FNHSysCmpRunId As Integer, _FNHSysStyleId As Integer, _FNHSysCustId As Integer
        Dim tFDInsDate As String, tFTInsTime As String, tFTInsUser As String
        Dim _tFTOrderNoSrc As String, _tFTOrderNoDest As String, _tFTSubOrderNoSrc As String, _tFTSubOrderNoDest As String
        Dim _tFTMainMaterial As String, _tFTCombination As String
        Dim nFNCopyOrderNo As Integer, nFNCopySubOrderNo As Integer, nLoopCopyOrder As Integer

        Dim _bCopyOrderComplete As Boolean = False

        Try

            _tFTOrderNoSrc = Me.FTOrderNo.Text.Trim()
            _tFTSubOrderNoSrc = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTSubOrderNo FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub] AS A WITH(NOLOCK) WHERE A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(_tFTOrderNoSrc) & "'", Conn.DB.DataBaseName.DB_MERCHAN, "") '_tFTOrderNoSrc & "-A"

            _FNHSysCmpId = Val(Me.FNHSysCmpId.Properties.Tag.ToString())
            _FNHSysCmpRunId = Val(Me.FNHSysCmpRunId.Properties.Tag.ToString())
            _FNHSysStyleId = Val(Me.FNHSysStyleId.Properties.Tag.ToString())

            If Not HI.MG.ShowMsg.mConfirmProcess("", 1403110004, Me.FTOrderNo.Text.Trim()) = True Then Return False

            If Not System.Diagnostics.Debugger.IsAttached = True Then
                _Spls = New HI.TL.SplashScreen("Copy Data...Please Wait ")
            End If

            nFNCopyOrderNo = Me.FNCopyOrderNo.Value
            nFNCopySubOrderNo = Me.FNCopySubOrderNo.Value

            tSql = "DECLARE @FDInsDate AS VARCHAR(10);"
            tSql &= Environment.NewLine & "DECLARE @FTInsTime AS VARCHAR(8);"
            tSql &= Environment.NewLine & "SELECT @FDInsDate = CONVERT(VARCHAR(10), GETDATE(), 111), @FTInsTime = CONVERT(VARCHAR(10), GETDATE(), 114);"
            tSql &= Environment.NewLine & "SELECT ISNULL(@FDInsDate,'') AS FDInsDate, ISNULL(@FTInsTime, '') AS FTInsTime;"

            oDBdt = HI.Conn.SQLConn.GetDataTable(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN)

            If oDBdt.Rows.Count > 0 Then
                tFDInsDate = "'" & oDBdt.Rows(0).Item("FDInsDate") & "'"
                tFTInsTime = "'" & oDBdt.Rows(0).Item("FTInsTime") & "'"
            Else
                tFDInsDate = HI.UL.ULDate.FormatDateDB
                tFTInsTime = HI.UL.ULDate.FormatTimeDB
            End If

            tFTInsUser = HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName)

            '...Customer System Id by Style Id
            tSql = ""
            tSql = "SELECT A.FNHSysCustId"
            tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMStyle]  AS A WITH(NOLOCK) INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.[TCNMCustomer] AS B WITH(NOLOCK) ON A.FNHSysCustId = B.FNHSysCustId"
            tSql &= Environment.NewLine & "WHERE A.FNHSysStyleId = " & _FNHSysStyleId

            _FNHSysCustId = Val(HI.Conn.SQLConn.GetField(tSql, HI.Conn.DB.DataBaseName.DB_MASTER, "0"))

            tmpDTListCopyOrderComplete = New System.Data.DataTable

            Dim oColFTOrderNoCopy As System.Data.DataColumn
            oColFTOrderNoCopy = New System.Data.DataColumn("FTOrderNoCopy", System.Type.GetType("System.String"))
            oColFTOrderNoCopy.Caption = "FTOrderNoCopy"
            tmpDTListCopyOrderComplete.Columns.Add(oColFTOrderNoCopy.ColumnName, oColFTOrderNoCopy.DataType)

            Dim oColFTOrderNoSubCopy As System.Data.DataColumn
            oColFTOrderNoSubCopy = New System.Data.DataColumn("FTOrderNoSubCopy", System.Type.GetType("System.String"))
            oColFTOrderNoSubCopy.Caption = "FTOrderNoSubCopy"
            tmpDTListCopyOrderComplete.Columns.Add(oColFTOrderNoSubCopy.ColumnName, oColFTOrderNoSubCopy.DataType)

            _bCopyOrderComplete = True
            nLoopCopyOrder = 1

            Dim pListValue As String = HI.TL.CboList.GetListValue(FNOrderType.Properties.Tag.ToString, FNOrderType.SelectedIndex)

            Do While (_bCopyOrderComplete) AndAlso (nLoopCopyOrder <= nFNCopyOrderNo)

                _tFTOrderNoDest = HI.TL.Document.GetDocumentNo(_tSysDBName, _tSysTableName, pListValue, False, HI.TL.CboList.GetListRefer(FNOrderType.Properties.Tag.ToString, FNOrderType.SelectedIndex) & Me.FNHSysCmpRunId.Text.Trim().ToString()).ToString()

                HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_MERCHAN)
                HI.Conn.SQLConn.SqlConnectionOpen()
                HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
                HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

                Try
                    tSql = ""
                    tSql = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrder] (FTInsUser, FDInsDate, FTInsTime, FTUpdUser, FDUpdDate, FTUpdTime"
                    tSql &= vbCrLf & ", FTOrderNo, FDOrderDate, FTOrderBy, FNOrderType, FNHSysCmpId, FNHSysCmpRunId, FNHSysStyleId, FTPORef, FNHSysCustId, "
                    tSql &= vbCrLf & " FNHSysAgencyId, FNHSysProdTypeId, FNHSysBuyerId, FTMainMaterial, FTCombination, FTRemark, FNHSysBrandId, FNHSysBuyId, "
                    tSql &= vbCrLf & "     FNJobState, FNHSysPlantId, FNHSysBuyGrpId, FNHSysMerTeamId, FPOrderImage1, FPOrderImage2, FPOrderImage3, FPOrderImage4,FNHSysSeasonId,FNHSysVenderPramId,FNHSysCmpIdCreate)"
                    tSql &= vbCrLf & "SELECT TOP 1 '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    tSql &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ",'" & HI.UL.ULF.rpQuoted(_tFTOrderNoDest) & "'"
                    tSql &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    tSql &= vbCrLf & ", " & pListValue & "," & _FNHSysCmpId & "," & _FNHSysCmpRunId & ", " & _FNHSysStyleId & ", FTPORef," & _FNHSysCustId & " "
                    tSql &= vbCrLf & ", FNHSysAgencyId, FNHSysProdTypeId, FNHSysBuyerId, FTMainMaterial, FTCombination, FTRemark, FNHSysBrandId, FNHSysBuyId"
                    tSql &= vbCrLf & ", 0 AS FNJobState,  FNHSysPlantId, FNHSysBuyGrpId, FNHSysMerTeamId, FPOrderImage1, FPOrderImage2, FPOrderImage3, FPOrderImage4,FNHSysSeasonId,FNHSysVenderPramId," & Val(HI.ST.SysInfo.CmpID) & ""
                    tSql &= vbCrLf & "FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrder] WITH(NOLOCK)"
                    tSql &= vbCrLf & "WHERE FTOrderNo = '" & HI.UL.ULF.rpQuoted(_tFTOrderNoSrc) & "'"

                    If HI.Conn.SQLConn.Execute_Tran(tSql, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        _bCopyOrderComplete = False

                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    End If

                    If _bCopyOrderComplete = True Then
                        HI.Conn.SQLConn.Tran.Commit()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                        Dim oNewRowOrderNoCopy As System.Data.DataRow
                        oNewRowOrderNoCopy = tmpDTListCopyOrderComplete.NewRow()
                        oNewRowOrderNoCopy.Item("FTOrderNoCopy") = _tFTOrderNoDest
                        tmpDTListCopyOrderComplete.Rows.Add(oNewRowOrderNoCopy)
                        tmpDTListCopyOrderComplete.AcceptChanges()

                    End If

                Catch ex As Exception
                    _bCopyOrderComplete = False

                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                End Try

                If _bCopyOrderComplete = True Then

                    '...Atleast One Copy Factory Sub Order No.
                    If nFNCopySubOrderNo > 0 Then

                        Dim tTextOrderNoSubCopy As String

                        tTextOrderNoSubCopy = ""

                        Dim nLoopCopySubOrder As Integer
                        For nLoopCopySubOrder = 1 To nFNCopySubOrderNo
                            tSql = ""
                            tSql = "EXEC SP_GEN_CHARACTER_SubOrderNo '" & HI.UL.ULF.rpQuoted(_tFTOrderNoDest) & "'"

                            _tFTSubOrderNoDest = HI.Conn.SQLConn.GetField(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN, "")

                            tSql = ""
                            tSql = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTOrderSub] (FTInsUser, FDInsDate, FTInsTime, FTUpdUser, FDUpdDate, FTUpdTime"
                            tSql &= vbCrLf & ", FTOrderNo, FTSubOrderNo, FDSubOrderDate, FTSubOrderBy, FDProDate, FDShipDate, FNHSysBuyId, FNHSysContinentId"
                            tSql &= vbCrLf & ", FNHSysCountryId, FNHSysProvinceId, FNHSysShipModeId, FNHSysCurId, FNHSysGenderId, FNHSysUnitId, FNSubOrderState"
                            tSql &= vbCrLf & ", FTStateEmb, FTStatePrint, FTStateHeat, FTStateLaser, FTStateWindows,FTStateOther1"
                            tSql &= vbCrLf & ", FTOther1Note, FTStateOther2, FTOther2Note, FTStateOther3, FTOther3Note1, FTRemark, FNHSysShipPortId"
                            tSql &= vbCrLf & ", FDShipDateOrginal, FTCustRef, FNPackCartonSubType, FNPackPerCarton)"
                            ' Add Column FNPackCartonSubType, FNPackPerCarton By Chet
                            tSql &= vbCrLf & "SELECT TOP 1 '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                            tSql &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ",'" & HI.UL.ULF.rpQuoted(_tFTOrderNoDest) & "','" & HI.UL.ULF.rpQuoted(_tFTSubOrderNoDest) & "'"
                            tSql &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                            tSql &= vbCrLf & " , FDProDate, FDShipDate, FNHSysBuyId, FNHSysContinentId, "
                            tSql &= vbCrLf & " FNHSysCountryId, FNHSysProvinceId, FNHSysShipModeId, FNHSysCurId, FNHSysGenderId, FNHSysUnitId, FNSubOrderState, FTStateEmb, FTStatePrint, FTStateHeat, FTStateLaser, FTStateWindows,"
                            tSql &= vbCrLf & " FTStateOther1, FTOther1Note, FTStateOther2, FTOther2Note, FTStateOther3, FTOther3Note1, FTRemark, FNHSysShipPortId,"
                            tSql &= vbCrLf & " FDShipDateOrginal, FTCustRef, FNPackCartonSubType, FNPackPerCarton"
                            ' Add Column FNPackCartonSubType, FNPackPerCarton By Chet
                            tSql &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTOrderSub] WITH(NOLOCK)"
                            tSql &= vbCrLf & "WHERE FTOrderNo = '" & HI.UL.ULF.rpQuoted(_tFTOrderNoSrc) & "'"

                            If HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN) = True Then
                                '...add new record factory sub order no. complete...
                                If tTextOrderNoSubCopy = "" Then
                                    tTextOrderNoSubCopy = _tFTSubOrderNoDest
                                Else
                                    tTextOrderNoSubCopy = tTextOrderNoSubCopy & "|" & _tFTSubOrderNoDest
                                End If

                            End If

                            '...TMERTOrderSub_Breakdown
                            tSql = ""
                            tSql = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub_BreakDown]"
                            tSql &= vbCrLf & " ("
                            tSql &= vbCrLf & " FTInsUser, FDInsDate, FTInsTime, FTUpdUser, FDUpdDate, FTUpdTime"
                            tSql &= vbCrLf & " , FTOrderNo, FTSubOrderNo, FTColorway, FTSizeBreakDown, FNPrice, FNQuantity, FNAmt, FNHSysMatColorId,"
                            tSql &= vbCrLf & "  FNHSysMatSizeId, FNExtraQty, FNQuantityExtra, FNGrandQuantity, FNAmntExtra, FNGrandAmnt, FNGarmentQtyTest, FNAmntQtyTest,FTNikePOLineItem"
                            tSql &= vbCrLf & " )"
                            tSql &= vbCrLf & "SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ""
                            tSql &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ""
                            tSql &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_tFTOrderNoDest) & "','" & HI.UL.ULF.rpQuoted(_tFTSubOrderNoDest) & "'"
                            tSql &= vbCrLf & ",FTColorway, FTSizeBreakDown, FNPrice,0 AS  FNQuantity,0 AS  FNAmt, FNHSysMatColorId, "
                            tSql &= vbCrLf & "  FNHSysMatSizeId, 0 AS  FNExtraQty, 0 AS  FNQuantityExtra,0 AS   FNGrandQuantity,0 AS  FNAmntExtra,0 AS  FNGrandAmnt,0 AS  FNGarmentQtyTest, 0 AS  FNAmntQtyTest,'0'"
                            tSql &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub_BreakDown] WITH(NOLOCK) "
                            tSql &= vbCrLf & "WHERE FTSubOrderNo = '" & HI.UL.ULF.rpQuoted(_tFTSubOrderNoSrc) & "' "

                            HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN)

                            '...TMERTOrderSub_Sew
                            tSql = ""
                            tSql = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub_Sew]"
                            tSql &= vbCrLf & "  ( "
                            tSql &= vbCrLf & " FTInsUser, FDInsDate, FTInsTime, FTUpdUser, FDUpdDate, FTUpdTime, FTOrderNo, FTSubOrderNo, FNSewSeq, FTSewDescription, FTSewNote, FTImage"
                            tSql &= vbCrLf & "  )"
                            tSql &= vbCrLf & "SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ""
                            tSql &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ""
                            tSql &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_tFTOrderNoDest) & "','" & HI.UL.ULF.rpQuoted(_tFTSubOrderNoDest) & "', FNSewSeq, FTSewDescription, FTSewNote, FTImage"
                            tSql &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub_Sew] WITH(NOLOCK) "
                            tSql &= vbCrLf & "WHERE FTSubOrderNo = '" & HI.UL.ULF.rpQuoted(_tFTSubOrderNoSrc) & "' "
                            HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN)

                            tSql = ""
                            tSql = "SELECT  A.FTOrderNo, A.FTSubOrderNo, A.FNSewSeq, A.FTImage"
                            tSql &= Environment.NewLine & "FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub_Sew AS A (NOLOCK)"
                            tSql &= Environment.NewLine & "WHERE  (A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(_tFTOrderNoDest) & "') AND (A.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(_tFTSubOrderNoDest) & "')"
                            tSql &= Environment.NewLine & "ORDER BY A.FNSewSeq ASC;"

                            Dim tmpDTSew As System.Data.DataTable

                            tmpDTSew = HI.Conn.SQLConn.GetDataTable(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN)

                            For Each oDataRowSew As System.Data.DataRow In tmpDTSew.Rows
                                Dim oImageSew As System.Drawing.Image
                                Dim FTImageSew As String
                                Dim FNSewSeq As Integer

                                FTImageSew = oDataRowSew!FTImage.ToString
                                FNSewSeq = Val(oDataRowSew!FNSewSeq.ToString)

                                oImageSew = HI.UL.ULImage.LoadImage("" & __SystemFilePath & "\OrderNo\SubOrderNo\Sewing\" & FTImageSew)

                                If Not oImageSew Is Nothing Then
                                    FTImageSew = _tFTSubOrderNoDest & "_" & FNSewSeq.ToString
                                    FTImageSew = Microsoft.VisualBasic.Replace(FTImageSew, "-", "_")

                                    tSql = ""
                                    tSql = "UPDATE A"
                                    tSql &= Environment.NewLine & "SET A.FTImage = '" & HI.UL.ULImage.SaveImage(oImageSew, FTImageSew, "" & __SystemFilePath & "\OrderNo\SubOrderNo\Sewing\") & "'"
                                    tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub_Sew AS A"
                                    tSql &= Environment.NewLine & "WHERE A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(_tFTOrderNoDest) & "'"
                                    tSql &= Environment.NewLine & "      AND A.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(_tFTSubOrderNoDest) & "'"
                                    tSql &= Environment.NewLine & "      AND A.FNSewSeq = " & FNSewSeq & ";"

                                    HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN)

                                End If

                            Next

                            tmpDTSew.Dispose()

                            '...TMERTOrderSub_Pack
                            tSql = ""
                            tSql = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub_Pack]"
                            tSql &= vbCrLf & "  ( "
                            tSql &= vbCrLf & " FTInsUser, FDInsDate, FTInsTime, FTUpdUser, FDUpdDate, FTUpdTime, FTOrderNo, FTSubOrderNo, FNPackSeq, FTPackDescription, FTPackNote, FTImage "
                            tSql &= vbCrLf & "  )"
                            tSql &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ""
                            tSql &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ""
                            tSql &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_tFTOrderNoDest) & "','" & HI.UL.ULF.rpQuoted(_tFTSubOrderNoDest) & "', FNPackSeq, FTPackDescription, FTPackNote, FTImage "
                            tSql &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub_Pack] WITH(NOLOCK) "
                            tSql &= vbCrLf & "  WHERE FTSubOrderNo='" & HI.UL.ULF.rpQuoted(_tFTSubOrderNoSrc) & "' "
                            HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN)

                            tSql = ""
                            tSql = "SELECT  A.FTOrderNo, A.FTSubOrderNo, A.FNPackSeq, A.FTImage"
                            tSql &= Environment.NewLine & "FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub_Pack AS A (NOLOCK)"
                            tSql &= Environment.NewLine & "WHERE  (A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(_tFTOrderNoDest) & "') AND (A.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(_tFTSubOrderNoDest) & "')"
                            tSql &= Environment.NewLine & "ORDER BY A.FNPackSeq ASC;"

                            Dim tmpDTPack As System.Data.DataTable

                            tmpDTPack = HI.Conn.SQLConn.GetDataTable(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN)

                            For Each oDataRowPack As System.Data.DataRow In tmpDTPack.Rows
                                Dim oImagePack As System.Drawing.Image
                                Dim FTImagePack As String
                                Dim FNPackSeq As Integer

                                FTImagePack = oDataRowPack!FTImage.ToString
                                FNPackSeq = Val(oDataRowPack!FNPackSeq.ToString)
                                oImagePack = HI.UL.ULImage.LoadImage("" & __SystemFilePath & "\OrderNo\SubOrderNo\Packing\" & FTImagePack)

                                If Not oImagePack Is Nothing Then
                                    FTImagePack = _tFTSubOrderNoDest & "_" & FNPackSeq.ToString
                                    FTImagePack = Microsoft.VisualBasic.Replace(FTImagePack, "-", "_")

                                    tSql = ""
                                    tSql = "UPDATE A"
                                    tSql &= Environment.NewLine & "SET A.FTImage = '" & HI.UL.ULImage.SaveImage(oImagePack, FTImagePack, "" & __SystemFilePath & "\OrderNo\SubOrderNo\Packing\") & "'"
                                    tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub_Pack AS A "
                                    tSql &= Environment.NewLine & "WHERE A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(_tFTOrderNoDest) & "'"
                                    tSql &= Environment.NewLine & "      AND A.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(_tFTSubOrderNoDest) & "'"
                                    tSql &= Environment.NewLine & "      AND A.FNPackSeq = " & FNPackSeq & ";"

                                    HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN)

                                End If

                            Next

                            tmpDTPack.Dispose()

                            '..TMERTOrderSub_Bundle
                            tSql = ""
                            tSql = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTOrderSub_Bundle]([FTInsUser], [FDInsDate], [FTInsTime], [FTOrderNo], [FTSubOrderNo], [FTColorway], [FTSizeBreakDown], [FNQuantity])"
                            tSql &= Environment.NewLine & "SELECT N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' AS FTInsUser, " & HI.UL.ULDate.FormatDateDB & " AS FDInsDate, " & HI.UL.ULDate.FormatTimeDB & " AS FTInsTime, N'" & HI.UL.ULF.rpQuoted(_tFTOrderNoDest) & "' AS FTOrderNo, N'" & HI.UL.ULF.rpQuoted(_tFTSubOrderNoDest) & "' AS FTSubOrderNo, A.[FTColorway], A.[FTSizeBreakDown], 0 AS FNQuantity"
                            tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTOrderSub_Bundle] AS A (NOLOCK)"
                            tSql &= Environment.NewLine & "WHERE A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(_tFTOrderNoSrc) & "'"
                            tSql &= Environment.NewLine & "      AND A.FTSubOrderNO = N'" & HI.UL.ULF.rpQuoted(_tFTSubOrderNoSrc) & "';"



                            '...TMERTOrderSub_SizeSpec
                            tSql = ""
                            tSql = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub_SizeSpec]"
                            tSql &= vbCrLf & "  ( "
                            tSql &= vbCrLf & " FTInsUser, FDInsDate, FTInsTime, FTUpdUser, FDUpdDate, FTUpdTime, FTOrderNo, FTSubOrderNo, FNSeq, FNHSysMatSizeId, FTSizeSpecDesc, FTSizeSpecExtension "
                            tSql &= vbCrLf & "  )"
                            tSql &= vbCrLf & "SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ""
                            tSql &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ""
                            tSql &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_tFTOrderNoDest) & "','" & HI.UL.ULF.rpQuoted(_tFTSubOrderNoDest) & "', FNSeq, FNHSysMatSizeId, FTSizeSpecDesc, FTSizeSpecExtension "
                            tSql &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub_SizeSpec] WITH(NOLOCK) "
                            tSql &= vbCrLf & "WHERE FTSubOrderNo = '" & HI.UL.ULF.rpQuoted(_tFTSubOrderNoSrc) & "' "

                            HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN)

                            '...TMERTOrderSub_Component
                            tSql = ""
                            tSql = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub_Component]"
                            tSql &= vbCrLf & "  ( "
                            tSql &= vbCrLf & " FTInsUser, FDInsDate, FTInsTime, FTUpdUser, FDUpdDate, FTUpdTime, FTOrderNo, FTSubOrderNo, FNHSysMerMatId, FNPart, FTComponent, FTRemark, FNConSmp,FNSeq,FNDataSeq "
                            tSql &= vbCrLf & "  )"
                            tSql &= vbCrLf & "SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ""
                            tSql &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ""
                            tSql &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_tFTOrderNoDest) & "','" & HI.UL.ULF.rpQuoted(_tFTSubOrderNoDest) & "', FNHSysMerMatId, FNPart, FTComponent, FTRemark, FNConSmp,FNSeq,FNDataSeq "
                            tSql &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub_Component] WITH(NOLOCK) "
                            tSql &= vbCrLf & "WHERE FTSubOrderNo = '" & HI.UL.ULF.rpQuoted(_tFTSubOrderNoSrc) & "' "

                            HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN)


                        Next nLoopCopySubOrder

                        If tTextOrderNoSubCopy <> "" Then
                            '...รายการใบสั่งผลิตย่อยที่ทำการ Generate Copy Complete
                            For Each oDataRowOrderNoCopy As DataRow In tmpDTListCopyOrderComplete.Select("FTOrderNoCopy = '" & HI.UL.ULF.rpQuoted(_tFTOrderNoDest) & "'")
                                oDataRowOrderNoCopy.Item("FTOrderNoSubCopy") = tTextOrderNoSubCopy

                                Exit For

                            Next

                            tmpDTListCopyOrderComplete.AcceptChanges()

                        End If

                    End If

                End If

                nLoopCopyOrder = nLoopCopyOrder + 1

                Application.DoEvents()

            Loop

            If Not System.Diagnostics.Debugger.IsAttached = True Then
                _Spls.Close()
            End If

            If (_bCopyOrderComplete = True) And ((nLoopCopyOrder - 1) = nFNCopyOrderNo) Then

                Select Case HI.ST.Lang.Language
                    Case HI.ST.Lang.eLang.TH
                        HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, "คัดลอกรายการใบสั่งผลิตเรียบร้อยแล้ว...")
                    Case HI.ST.Lang.eLang.EN
                        HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, "Copy Factory Order No. complete...")
                    Case Else

                End Select

                If Not DBNull.Value.Equals(tmpDTListCopyOrderComplete) AndAlso tmpDTListCopyOrderComplete.Rows.Count > 0 Then
                    _wListCompleteCopyOrder = New wListCompleteCopyOrder(tmpDTListCopyOrderComplete)

                    HI.TL.HandlerControl.AddHandlerObj(_wListCompleteCopyOrder)

                    Dim oSysLang As New HI.ST.SysLanguage

                    Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleID, _wListCompleteCopyOrder.Name.ToString.Trim, _wListCompleteCopyOrder)

                    Call HI.ST.Lang.SP_SETxLanguage(_wListCompleteCopyOrder)

                    Try
                        _wListCompleteCopyOrder.ShowDialog()
                    Catch ex As Exception
                        If System.Diagnostics.Debugger.IsAttached = True Then
                            MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.OkOnly, Me.Text)
                        End If
                    End Try

                End If

            End If

        Catch ex As Exception

            If Not System.Diagnostics.Debugger.IsAttached = True Then
                _Spls.Close()
            Else
                MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.OkOnly, My.Application.Info.Title)
            End If

        End Try

        Return _bCopyOrderComplete

    End Function

    Private Function W_PRCbLoadMasterFactoryOrderNo(ByVal ptFTOrderNo As String)
        Dim oDBdt As DataTable
        Dim _bPass As Boolean = False
        Try
            tSql = ""
            tSql = "SELECT A.FTOrderNo, A.FNHSysCmpId, B.FTCmpCode, A.FNHSysCmpRunId, C.FTCmpRunCode, A.FNHSysStyleId, D.FTStyleCode"
            tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrder] AS A (NOLOCK) INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCNMCmp] AS B WITH(NOLOCK) ON A.FNHSysCmpId = B.FNHSysCmpId"
            tSql &= Environment.NewLine & "     INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMCmpRun AS C WITH(NOLOCK) ON A.FNHSysCmpRunId = C.FNHSysCmpRunId"
            tSql &= Environment.NewLine & "     INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TMERMStyle AS D (NOLOCK) ON A.FNHSysStyleId = D.FNHSysStyleId"
            tSql &= Environment.NewLine & "WHERE A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(_tFTOrderNo) & "';"

            oDBdt = HI.Conn.SQLConn.GetDataTable(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN)

            If oDBdt.Rows.Count > 0 Then
                _nFNHSysCmpId = oDBdt.Rows(0).Item("FNHSysCmpId")
                _tFNHSysCmpId = oDBdt.Rows(0).Item("FTCmpCode").ToString()
                _nFNHSysCmpRunId = oDBdt.Rows(0).Item("FNHSysCmpRunId")
                _tFNHSysCmpRunId = oDBdt.Rows(0).Item("FTCmpRunCode").ToString()
                _nFNHSysStyleId = oDBdt.Rows(0).Item("FNHSysStyleId")
                _tFNHSysStyleId = oDBdt.Rows(0).Item("FTStyleCode").ToString()
                _bPass = True
            Else
                _bPass = False
            End If

            Return _bPass
        Catch ex As Exception
            If System.Diagnostics.Debugger.IsAttached = True Then
                MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace.ToString())
            End If
            Return _bPass
        End Try

    End Function

    Private Function W_PRCbValidateBF_CopyOrderNo() As Boolean
        Dim _bPass As Boolean = False
        Try
            If Me.FNHSysCmpId.Text.Trim() <> "" Then
                If Me.FNHSysCmpRunId.Text.Trim() <> "" Then
                    If Me.FNHSysStyleId.Text.Trim() <> "" Then
                        If Me.FNCopyOrderNo.Value > 0 Then
                            If Me.FNCopySubOrderNo.Value > 1 Then
                                '...validate total item factory sub order no.
                                If Me.FNCopySubOrderNo.Value <= _nTotalFactorySubOrderNo Then
                                    _bPass = True
                                Else
                                    MessageBox.Show(String.Format("ปริมาณเลขที่ใบสั่งผลิตย่อยจะต้องไม่เกินจำนวน {0}", _nTotalFactorySubOrderNo) & " รายการ !!!", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                    Me.FNCopySubOrderNo.Focus()
                                End If
                            Else
                                _bPass = True
                            End If
                        Else
                            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, Me.FNCopyOrderNo_lbl.Text)
                            Me.FNCopyOrderNo.Focus()
                        End If
                    Else
                        HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, Me.FNHSysStyleId_lbl.Text)
                    End If
                Else
                    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, Me.FNHSysCmpRunId_lbl.Text)
                End If
            Else
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, Me.FNHSysCmpId_lbl.Text)
            End If

            Return _bPass
        Catch ex As Exception
            If System.Diagnostics.Debugger.IsAttached = True Then
                MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace.ToString())
            End If
            Return _bPass
        End Try

    End Function

#End Region

#Region "Event Handle"

    Private Sub ocmcancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmcancel.Click
        Me.Close()
    End Sub

    Private Sub ocmok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmok.Click
        If W_PRCbCopyOrderNo() = True Then
            Me.Close()
        End If
    End Sub

    Private Sub FNHSysCmpId_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles FNHSysCmpId.EditValueChanged
        If Me.FNHSysCmpId.Text.Trim() <> "" Then
            tSql = ""
            tSql = "SELECT TOP 1 A.FNHSysCmpId"
            tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMCmp AS A WITH(NOLOCK)"
            tSql &= Environment.NewLine & " WHERE A.FTCmpCode = N'" & HI.UL.ULF.rpQuoted(Me.FNHSysCmpId.Text.Trim()) & "';"
            Me.FNHSysCmpId.Properties.Tag = HI.Conn.SQLConn.GetField(tSql, HI.Conn.DB.DataBaseName.DB_MASTER, "")
        End If
    End Sub

    Private Sub FNHSysCmpRunId_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles FNHSysCmpRunId.EditValueChanged
        If Me.FNHSysCmpRunId.Text.Trim() <> "" Then
            tSql = ""
            tSql = "SELECT TOP 1 A.FNHSysCmpRunId"
            tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMCmpRun AS A WITH(NOLOCK)"
            tSql &= Environment.NewLine & "WHERE A.FTCmpRunCode = N'" & HI.UL.ULF.rpQuoted(Me.FNHSysCmpRunId.Text.Trim()) & "';"
            Me.FNHSysCmpRunId.Properties.Tag = HI.Conn.SQLConn.GetField(tSql, HI.Conn.DB.DataBaseName.DB_MASTER, "")
        End If
    End Sub

    Private Sub FNHSysStyleId_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles FNHSysStyleId.EditValueChanged
        If Me.FNHSysStyleId.Text.Trim() <> "" Then
            tSql = ""
            tSql = "SELECT TOP 1 A.FNHSysStyleId"
            tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TMERMStyle AS A WITH(NOLOCK)"
            tSql &= Environment.NewLine & "WHERE A.FTStyleCode = N'" & HI.UL.ULF.rpQuoted(Me.FNHSysStyleId.Text.Trim()) & "';"
            Me.FNHSysStyleId.Properties.Tag = HI.Conn.SQLConn.GetField(tSql, HI.Conn.DB.DataBaseName.DB_MASTER, "")
        End If
    End Sub

    Private Sub wCopyOrder_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not DBNull.Value.Equals(_tFTOrderNo) And _tFTOrderNo <> "" Then
            Me.FNHSysCmpId.Text = _tFNHSysCmpId
            Me.FNHSysCmpRunId.Text = _tFNHSysCmpRunId
            Me.FNHSysStyleId.Text = _tFNHSysStyleId
        End If
    End Sub

    Private Sub FNCopySubOrderNo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles FNCopySubOrderNo.KeyPress
        If Asc(e.KeyChar) = 13 Then
            Me.ocmok.PerformClick()
        End If
    End Sub

#End Region

End Class
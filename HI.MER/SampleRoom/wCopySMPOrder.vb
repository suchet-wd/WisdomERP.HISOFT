Imports System
Imports System.Data
Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Windows.Forms
Imports System.Windows.Forms.Control
Imports System.Drawing
Imports System.IO
Imports Microsoft.VisualBasic

Public Class wCopySMPOrder

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
                sSQL &= Environment.NewLine & "FROM " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.TMERTOrder AS A (NOLOCK)"
                sSQL &= Environment.NewLine & "WHERE A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(paramOrderNo) & "';"

                _DTImageRefOrderNo = HI.Conn.SQLConn.GetDataTable(sSQL, HI.Conn.DB.DataBaseName.DB_MERCHAN)

            End If

            Return _DTImageRefOrderNo

        End Get

    End Property

    Private _newDoc As String = ""
    Public Property newDoc As String
        Get
            Return _newDoc
        End Get
        Set(value As String)
            _newDoc = value
        End Set
    End Property

    Private _StyleCode As String = ""
    Public Property StyleCode As String
        Get
            Return _StyleCode
        End Get
        Set(value As String)
            _StyleCode = value
        End Set
    End Property
#End Region

#Region "Procedure And Function"

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.

    End Sub



    Private Function SaveCopy() As Boolean
        Try
            ' If W_PRCbValidateBF_CopyOrderNo() = False Then Return False
            Dim _Cmd As String = ""
            _newDoc = ""
            Dim _Spls As HI.TL.SplashScreen
            _Spls = New HI.TL.SplashScreen("Copy Data...Please Wait ")


            Dim RevisedNo As Integer = 0
            Dim _tFTOrderNoDest As String = ""


            Try
                Dim _CmpH As String = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTDocRun FROM " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TCNMCmp WHERE FNHSysCmpId=" & Val(FNHSysCmpId.Properties.Tag.ToString()) & " ", Conn.DB.DataBaseName.DB_SYSTEM, "")


                _tFTOrderNoDest = HI.TL.Document.GetDocumentNo(HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE), "TSMPOrder", HI.TL.CboList.GetListValue(FNSMPOrderType.Properties.Tag.ToString, FNSMPOrderType.SelectedIndex), False, _CmpH).ToString
                If _tFTOrderNoDest = "" Then
                    _tFTOrderNoDest = HI.TL.Document.GetDocumentNo(HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE), "TSMPOrder", "", False, _CmpH).ToString

                End If
                HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_MERCHAN)
                HI.Conn.SQLConn.SqlConnectionOpen()
                HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
                HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

                _Cmd = "Insert into  " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & ".dbo.[TSMPOrder] (FTInsUser, FDInsDate, FTInsTime"
                _Cmd &= vbCrLf & " ,FTSMPOrderNo, FDSMPOrderDate, FTSMPOrderBy, FNSMPOrderType, FNSMPPrototypeNo, FNHSysCmpId, FNHSysCmpRunId, FNHSysStyleId, "
                _Cmd &= vbCrLf & "  FNHSysSeasonId, FNHSysCustId, FNHSysMerTeamId, FTRemark, FTStateSendToSMP, FTSendToSMPBy, FDSendToSMPDate, FTSendToSMPTime, FPOrderImage1, FPOrderImage2, FPOrderImage3, FPOrderImage4, FTStateEmb, "
                _Cmd &= vbCrLf & "   FTStatePrint, FTStateHeat, FTStateLaser, FTStateWindows, FTFabticA, FTFabticB, FTFabticC, FTFabticD, FTBlock, FTPattern  "
                _Cmd &= vbCrLf & "   , FNHSysCmpIdCreate, FNRevisedNo, "
                _Cmd &= vbCrLf & "   FNOrderSampleType, FTCustomerTeam, FNHSysGenderId, FTFabticE, FTFabticF, FTFabticG, FTFabticH, "
                _Cmd &= vbCrLf & "  FTFabticI, FTMatA, FTMatNameA, FTMatColorA, FTMatColorNameA, FTMatSizeA, FNMatQuantityA, FTMatB, FTMatNameB, FTMatColorB, FTMatColorNameB, FTMatSizeB, FNMatQuantityB, FTMatC, FTMatNameC, FTMatColorC, "
                _Cmd &= vbCrLf & "   FTMatColorNameC, FTMatSizeC, FNMatQuantityC, FTMatD, FTMatNameD, FTMatColorD, FTMatColorNameD, FTMatSizeD, FNMatQuantityD, FTMatE, FTMatNameE, FTMatColorE, FTMatColorNameE, FTMatSizeE, FNMatQuantityE, FTMatF, "
                _Cmd &= vbCrLf & "   FTMatNameF, FTMatColorF, FTMatColorNameF, FTMatSizeF, FNMatQuantityF, FTMatG, FTMatNameG, FTMatColorG, FTMatColorNameG, FTMatSizeG, FNMatQuantityG, FTMatH, FTMatNameH, FTMatColorH, FTMatColorNameH, "
                _Cmd &= vbCrLf & "  FTMatSizeH, FNMatQuantityH, FTMatI, FTMatNameI, FTMatColorI, FTMatColorNameI, FTMatSizeI, FNMatQuantityI, FNMatUnitIdA, FNMatUnitIdB, FNMatUnitIdC, FNMatUnitIdD, FNMatUnitIdE, FNMatUnitIdF, FNMatUnitIdG, FNMatUnitIdH, "
                _Cmd &= vbCrLf & "  FNMatUnitIdI, FNSMPPriceType,FNSMPOrderStatus "

                _Cmd &= vbCrLf & "  , FTStatePatternHard, FTStatePatternEPT, FTStatePatternGrandenest, FTStatePatternGradelogo, FTStatePattern3D"
                _Cmd &= vbCrLf & "  , FTStatePatternOptiplan, FTStatePatternOthers, FTPatternOthersNote, FNHSysSuplIdA, FNHSysSuplIdB, FNHSysSuplIdC, FNHSysSuplIdD, FNHSysSuplIdE, FNHSysSuplIdF, FNHSysSuplIdG, FNHSysSuplIdH, FNHSysSuplIdI"

                _Cmd &= vbCrLf & "  ,FNHSysRawmatIdA, FNHSysRawmatIdB, FNHSysRawmatIdC, FNHSysRawmatIdD, FNHSysRawmatIdE, FNHSysRawmatIdF, FNHSysRawmatIdG, FNHSysRawmatIdH, FNHSysRawmatIdI, FTPgmName "
                _Cmd &= vbCrLf & "  )"
                _Cmd &= vbCrLf & "SELECT TOP 1 '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ""
                _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_tFTOrderNoDest) & "'"
                _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Cmd &= vbCrLf & "," & HI.TL.CboList.GetListValue(FNSMPOrderType.Properties.Tag.ToString, FNSMPOrderType.SelectedIndex) & "  AS FNSMPOrderType," & FNSMPPrototypeNo.Value & " AS  FNSMPPrototypeNo, " & Val(FNHSysCmpId.Properties.Tag.ToString()) & " AS FNHSysCmpId, FNHSysCmpRunId, FNHSysStyleId,  "
                _Cmd &= vbCrLf & "      FNHSysSeasonId, FNHSysCustId, FNHSysMerTeamId, FTRemark, FTStateSendToSMP, FTSendToSMPBy, FDSendToSMPDate, FTSendToSMPTime, FPOrderImage1, FPOrderImage2, FPOrderImage3, FPOrderImage4, "
                _Cmd &= vbCrLf & "     FTStateEmb, FTStatePrint, FTStateHeat, FTStateLaser, FTStateWindows, FTFabticA, FTFabticB, FTFabticC, FTFabticD, FTBlock, FTPattern "
                _Cmd &= vbCrLf & "     ," & HI.ST.SysInfo.CmpID & " FNHSysCmpIdCreate,0  FNRevisedNo "
                _Cmd &= vbCrLf & "   , " & FNOrderSampleType.SelectedIndex & " AS FNOrderSampleType, FTCustomerTeam, FNHSysGenderId, FTFabticE, FTFabticF, FTFabticG, "
                _Cmd &= vbCrLf & "    FTFabticH, FTFabticI, FTMatA, FTMatNameA, FTMatColorA, FTMatColorNameA, FTMatSizeA, FNMatQuantityA, FTMatB, FTMatNameB, FTMatColorB, FTMatColorNameB, FTMatSizeB, FNMatQuantityB, FTMatC, FTMatNameC, "
                _Cmd &= vbCrLf & "   FTMatColorC, FTMatColorNameC, FTMatSizeC, FNMatQuantityC, FTMatD, FTMatNameD, FTMatColorD, FTMatColorNameD, FTMatSizeD, FNMatQuantityD, FTMatE, FTMatNameE, FTMatColorE, FTMatColorNameE, FTMatSizeE, "
                _Cmd &= vbCrLf & "  FNMatQuantityE, FTMatF, FTMatNameF, FTMatColorF, FTMatColorNameF, FTMatSizeF, FNMatQuantityF, FTMatG, FTMatNameG, FTMatColorG, FTMatColorNameG, FTMatSizeG, FNMatQuantityG, FTMatH, FTMatNameH, "
                _Cmd &= vbCrLf & "  FTMatColorH, FTMatColorNameH, FTMatSizeH, FNMatQuantityH, FTMatI, FTMatNameI, FTMatColorI, FTMatColorNameI, FTMatSizeI, FNMatQuantityI, FNMatUnitIdA, FNMatUnitIdB, FNMatUnitIdC, FNMatUnitIdD, FNMatUnitIdE, "
                _Cmd &= vbCrLf & "  FNMatUnitIdF, FNMatUnitIdG, FNMatUnitIdH, FNMatUnitIdI, FNSMPPriceType,0 AS  FNSMPOrderStatus"

                _Cmd &= vbCrLf & "  , FTStatePatternHard, FTStatePatternEPT, FTStatePatternGrandenest, FTStatePatternGradelogo, FTStatePattern3D"
                _Cmd &= vbCrLf & "  , FTStatePatternOptiplan, FTStatePatternOthers, FTPatternOthersNote, FNHSysSuplIdA, FNHSysSuplIdB, FNHSysSuplIdC, FNHSysSuplIdD, FNHSysSuplIdE, FNHSysSuplIdF, FNHSysSuplIdG, FNHSysSuplIdH, FNHSysSuplIdI"
                _Cmd &= vbCrLf & "  ,FNHSysRawmatIdA, FNHSysRawmatIdB, FNHSysRawmatIdC, FNHSysRawmatIdD, FNHSysRawmatIdE, FNHSysRawmatIdF, FNHSysRawmatIdG, FNHSysRawmatIdH, FNHSysRawmatIdI, FTPgmName "
                _Cmd &= vbCrLf & " From " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & ".dbo.[TSMPOrder] with(nolock)"
                _Cmd &= vbCrLf & "WHERE FTSMPOrderNo = '" & HI.UL.ULF.rpQuoted(FTOrderNo.Text.Trim()) & "'"

                If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    _Spls.Close()
                    Return False
                End If

                _Cmd = "Insert into  " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & ".dbo.[TSMPOrder_Breakdown] ("
                _Cmd &= vbCrLf & "   FTInsUser, FDInsDate,FTInsTime, FTSMPOrderNo, FTSizeBreakDown, FNSeq, FTColorway, FNQuantity, FTDeliveryDate, FTRemark, FTPatternDate, FTFabricDate, FTAccessoryDate, FNPrice, FNAmt, FNFreeQuantity, FNDebitQuantity "
                _Cmd &= vbCrLf & " )"
                _Cmd &= vbCrLf & " SELECT  '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ""
                _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_tFTOrderNoDest) & "'"
                _Cmd &= vbCrLf & ", FTSizeBreakDown, FNSeq, FTColorway, FNQuantity, FTDeliveryDate, FTRemark, FTPatternDate, FTFabricDate, FTAccessoryDate, FNPrice, FNAmt, FNFreeQuantity, FNDebitQuantity "
                _Cmd &= vbCrLf & " From " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & ".dbo.[TSMPOrder_Breakdown] with(nolock)"
                _Cmd &= vbCrLf & " WHERE FTSMPOrderNo = '" & HI.UL.ULF.rpQuoted(FTOrderNo.Text.Trim()) & "'"
                _Cmd &= vbCrLf & " Insert into  " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & ".dbo.[TSMPOrder_File] ("
                _Cmd &= vbCrLf & "   FTInsUser, FDInsDate, FTInsTime, FTSMPOrderNo, FNFileSeq, FTFileName, FTFileType, FBFile "
                _Cmd &= vbCrLf & " )"

                _Cmd &= vbCrLf & "SELECT  '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ""
                _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_tFTOrderNoDest) & "'"
                _Cmd &= vbCrLf & ",FNFileSeq, FTFileName, FTFileType, FBFile "
                _Cmd &= vbCrLf & " From " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & ".dbo.[TSMPOrder_File] with(nolock)"
                _Cmd &= vbCrLf & "WHERE FTSMPOrderNo = '" & HI.UL.ULF.rpQuoted(FTOrderNo.Text.Trim()) & "'"

                _Cmd &= vbCrLf & " Insert into  " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & ".dbo.[TSMPOrder_MatList] ("
                _Cmd &= vbCrLf & "   FTInsUser, FDInsDate, FTInsTime,  FTSMPOrderNo, FNMatSeq, FTMat, FTMatName, FTMatColor, FTMatColorName, FTMatSize, FNMatQuantity, FNHSysUnitId, FTRemark, FNHSysSuplId, FTStateFree,FNHSysRawmatId "
                _Cmd &= vbCrLf & " )"
                _Cmd &= vbCrLf & "SELECT  '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ""
                _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_tFTOrderNoDest) & "'"
                _Cmd &= vbCrLf & ", FNMatSeq, FTMat, FTMatName, FTMatColor, FTMatColorName, FTMatSize, FNMatQuantity, FNHSysUnitId, FTRemark, FNHSysSuplId, FTStateFree,FNHSysRawmatId "
                _Cmd &= vbCrLf & " From " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & ".dbo.[TSMPOrder_MatList] with(nolock)"
                _Cmd &= vbCrLf & "WHERE FTSMPOrderNo = '" & HI.UL.ULF.rpQuoted(FTOrderNo.Text.Trim()) & "'"

                _Cmd &= vbCrLf & " Insert into  " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & ".dbo.[TSMPOrder_MatPart] ("
                _Cmd &= vbCrLf & "  FTInsUser, FDInsDate, FTInsTime,  FTSMPOrderNo, FNMatSeq, FNMat, FNHSysPartId"
                _Cmd &= vbCrLf & " )"
                _Cmd &= vbCrLf & "SELECT  '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ""
                _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_tFTOrderNoDest) & "'"
                _Cmd &= vbCrLf & ", FNMatSeq, FNMat, FNHSysPartId "
                _Cmd &= vbCrLf & " From " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & ".dbo.[TSMPOrder_MatPart] with(nolock)"
                _Cmd &= vbCrLf & "WHERE FTSMPOrderNo = '" & HI.UL.ULF.rpQuoted(FTOrderNo.Text.Trim()) & "'"

                _Cmd &= vbCrLf & " Insert into  " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & ".dbo.[TSMPOrder_SetPart] ("
                _Cmd &= vbCrLf & " FTInsUser, FDInsDate, FTInsTime, FTSMPOrderNo, FNHSysPartId, FTStateEmb, FTStatePrint, FTStateHeat, FTStateLaser, FTStateWindows, FTEmbNote, FTPrintNote, FTHeatNote,  FTLaserNote, FTWindowsNote"
                _Cmd &= vbCrLf & " )"
                _Cmd &= vbCrLf & "SELECT  '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ""
                _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_tFTOrderNoDest) & "'"
                _Cmd &= vbCrLf & ",  FNHSysPartId, FTStateEmb, FTStatePrint, FTStateHeat, FTStateLaser, FTStateWindows, FTEmbNote, FTPrintNote, FTHeatNote,  FTLaserNote, FTWindowsNote "
                _Cmd &= vbCrLf & " From " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & ".dbo.[TSMPOrder_SetPart] with(nolock)"
                _Cmd &= vbCrLf & "WHERE FTSMPOrderNo = '" & HI.UL.ULF.rpQuoted(FTOrderNo.Text.Trim()) & "'"

                _Cmd &= vbCrLf & " Insert into  " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & ".dbo.[TSMPOrder_FabricMatList] ("
                _Cmd &= vbCrLf & "   FTInsUser, FDInsDate, FTInsTime, FTSMPOrderNo, FNMatSeq, FTMat, FTMatName, FTMatColor, FTMatColorName, FTMatSize, FNMatQuantity, FNHSysUnitId, FTRemark, FNHSysSuplId, FTStateFree, FTStateFreeUpdUser, FDStateFreeUpdDate, FTStateFreeUpdTime, FNHSysRawmatId "
                _Cmd &= vbCrLf & " )"
                _Cmd &= vbCrLf & "SELECT  '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ""
                _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_tFTOrderNoDest) & "'"
                _Cmd &= vbCrLf & ",  FNMatSeq, FTMat, FTMatName, FTMatColor, FTMatColorName, FTMatSize, FNMatQuantity, FNHSysUnitId, FTRemark, FNHSysSuplId, FTStateFree, FTStateFreeUpdUser, FDStateFreeUpdDate, FTStateFreeUpdTime, FNHSysRawmatId "
                _Cmd &= vbCrLf & " From " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & ".dbo.[TSMPOrder_FabricMatList] with(nolock)"
                _Cmd &= vbCrLf & "WHERE FTSMPOrderNo = '" & HI.UL.ULF.rpQuoted(FTOrderNo.Text.Trim()) & "'"

                HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

                HI.Conn.SQLConn.Tran.Commit()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                _newDoc = HI.UL.ULF.rpQuoted(_tFTOrderNoDest)
                _Spls.Close()
            Catch ex As Exception
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                _newDoc = ""
                _Spls.Close()
                Return False
            End Try

            Return True

        Catch ex As Exception

            Return False
        End Try
    End Function



#End Region

#Region "Event Handle"

    Private Sub ocmcancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmcancel.Click
        _newDoc = ""
        Me.Close()
    End Sub

    Private Sub ocmok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmok.Click
        _newDoc = ""
        If FNHSysCmpId.Text <> "" Then
            If SaveCopy() = True Then
                HI.MG.ShowMsg.mInfo("Copy Data Complete ...", 1907017745, Me.Text, _newDoc)
                Me.Close()
            End If

        End If

    End Sub

    Private Sub FNHSysCmpId_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles FNHSysCmpId.EditValueChanged

    End Sub



    Private Sub wCopyOrder_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        FNHSysCmpId.Text = ""
        FNHSysCmpId_None.Text = ""
        FNSMPOrderType.SelectedIndex = 0
        FNOrderSampleType.SelectedIndex = 0
        FNSMPPrototypeNo.Value = 0
    End Sub



#End Region

End Class
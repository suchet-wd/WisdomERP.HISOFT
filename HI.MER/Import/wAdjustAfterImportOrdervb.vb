Imports System
Imports System.Data
Imports System.Windows.Forms
Imports System.Windows.Forms.Control
Imports System.Linq
Imports System.Collections.Generic
Imports DevExpress
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid.GridView
Imports DevExpress.XtraGrid.Views.BandedGrid
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views.Base.BaseView
Imports DevExpress.XtraEditors.Repository
Imports Microsoft.VisualBasic
Imports System.Data.OleDb
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraGrid.Views.Card
Imports System.Data.SqlClient
Imports System.Reflection
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.Data.Filtering
Imports DevExpress.XtraGrid.Columns

Public Class wAdjustAfterImportOrder

    Private PDataTableTmp As New DataTable
    Private FilerData As Boolean = False

#Region "Variable Declaration"
    Private tSql As String
    Private Const _tTableFactoryOrderNo As String = "FactoryOrderNo"
    Private Const _tTableFactorySubOrderNo As String = "FactorySubOrderNo"
    Private Const _tTableFactorySubOrderNoBreakdown As String = "FactorySubOrderNoBreakdown"
    Private Const _tRelationName As String = "FONo"

    Private oDBdtOrderNo As System.Data.DataTable
    Private oDBdtSubOrderNo As System.Data.DataTable
    Private oGridViewAdjustFONo As DevExpress.XtraGrid.Views.Grid.GridView

    Private oDBds As DataSet
    Private oDBdtFactoryOrderNo As System.Data.DataTable
    Private oDBdtFactorySubOrderNo As System.Data.DataTable
    Private oDBdtFactorySubOrderNoBreakdown As System.Data.DataTable

    Private _bLoadFONoBreakdown As Boolean = False

    Private Delegate Sub FooterCellCustomDrawEventHandler(ByVal sender As Object, ByVal e As FooterCellCustomDrawEventArgs)

    Private Enum eTabIndexs As Integer
        oTabFactoryOrderNo = 0
        oTabFactorySubOrderNo = 1
        oTabFactorySubOrderNoBreakdown = 2
    End Enum

#End Region

#Region "Property"

    Private _CallMenuName As String = ""
    Public Property CallMenuName As String
        Get
            Return _CallMenuName
        End Get
        Set(ByVal value As String)
            _CallMenuName = value
        End Set
    End Property

    Private _CallMethodName As String = ""
    Public Property CallMethodName As String
        Get
            Return _CallMethodName
        End Get
        Set(ByVal value As String)
            _CallMethodName = value
        End Set
    End Property

    Private _CallMethodParm As String = ""
    Public Property CallMethodParm As String
        Get
            Return _CallMethodParm
        End Get
        Set(ByVal value As String)
            _CallMethodParm = value
        End Set
    End Property

#End Region

#Region "MAIN PROC"

    Private Sub PROC_SAVE(sender As Object, e As EventArgs) Handles ocmSave.Click

        Try


            PDataTableTmp = New DataTable
            With PDataTableTmp.Columns
                .Add("FNHSysStyleId", GetType(Integer))
            End With

            Dim tConfirmSaveData As String = ""
            Dim _Qry As String = ""

            CType(ogdAdjustFONo.DataSource, DataTable).AcceptChanges()

            Dim _FoundTransferWh As Boolean = False

            Select Case Me.otcFactoryOrderNo.SelectedTabPageIndex
                Case eTabIndexs.oTabFactoryOrderNo

                    If FNHSysCmpId.Text = "" Then
                        HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FNHSysCmpId_lbl.Text)
                        Exit Sub
                    End If

                    If FNHSysBuyId.Text = "" Then
                        HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FNHSysBuyId_lbl.Text)
                        Exit Sub
                    End If

                    '...save all data factory order no/ factory sub order no/ factory sub order no breakdown
                    If Not W_PRCbValidateBFSaveData() Then Exit Sub
                    Dim _dt As DataTable
                    With CType(ogdAdjustFONo.DataSource, DataTable)
                        .AcceptChanges()
                        _dt = .Copy
                        'If .Select("FTChk='1' AND FNHSysCmpIdOrg<>0 AND FNHSysCmpIdOrg<> " & Val(FNHSysCmpId.Properties.Tag.ToString()) & " ").Length > 0 Then
                        '    HI.MG.ShowMsg.mInfo("พบข้อมูลการโอนระหว่างคลังแล้ว กรุณาทำการแจ้งสต๊อก เรื่องการเปลี่ยนสถานที่ผลิต  !!!", 1506297893, Me.Text, , MessageBoxIcon.Warning)
                        'End If
                        _dt.BeginInit()
                        For Each R As DataRow In _dt.Select("FTChk='1' AND FNHSysCmpIdOrg<>0 AND FNHSysCmpIdOrg<> " & Val(FNHSysCmpId.Properties.Tag.ToString()) & " ")

                            _Qry = " SELECT TOP 1  H.FTTransferWHNo "
                            _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENTransferWH AS H WITH(NOLOCK) INNER JOIN"
                            _Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT AS D WITH(NOLOCK)  ON H.FNHSysWHId = D.FNHSysWHId"
                            _Qry &= vbCrLf & "    WHERE  D.FTOrderNo='" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "' AND H.FTStateApprove='1' "

                            If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_INVEN, "") <> "" Then
                                R!FTChk = "0"
                                _FoundTransferWh = True
                            End If

                        Next

                        _dt.EndInit()
                    End With

                    If _FoundTransferWh Then
                        HI.MG.ShowMsg.mInfo("พบข้อมูลการโอนระหว่างคลังแล้วในบางรายการจะไม่สามารถทำการเปลี่ยนสถานที่ผลิตได้ !!!", 15062897894, Me.Text, , MessageBoxIcon.Warning)
                    End If

                    If Not HI.MG.ShowMsg.mConfirmProcess(MG.ShowMsg.ProcessType.mSave, Me.Text) Then Exit Sub

                    If W_PRCbSaveAdjustFONo() = True Then

                        For Each R As DataRow In _dt.Select("FTChk='1' AND FNHSysCmpIdOrg<>0 AND FNHSysCmpIdOrg<> " & Val(FNHSysCmpId.Properties.Tag.ToString()) & "  AND FNHSysBuyId_Hide=" & Val(FNHSysBuyId.Properties.Tag.ToString()) & " ")


                            _Qry = "UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder"
                            _Qry &= vbCrLf & " SET FTStateSendDirectorApp='0'"
                            _Qry &= vbCrLf & " ,FTStateSendDirectorBy=''"
                            _Qry &= vbCrLf & " ,FTStateDirectorApp='0'"
                            _Qry &= vbCrLf & " ,FTStateDirectorAppBy=''"
                            _Qry &= vbCrLf & " ,FTStateDirectorReject='0'"
                            _Qry &= vbCrLf & " ,FTStateDirectorRejectBy=''"
                            _Qry &= vbCrLf & " WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "'"

                            HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

                            Dim Cmdstring As String = ""
                            Cmdstring = "EXEC [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.USP_GEN_BREAKDOWN_SHIPDESINATION '" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString()) & "'"
                            HI.Conn.SQLConn.ExecuteOnly(Cmdstring, Conn.DB.DataBaseName.DB_MERCHAN)

                        Next


                        HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                        ocmRefresh.PerformClick()
                    Else
                        HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                    End If

                Case eTabIndexs.oTabFactorySubOrderNo
                    '...save data factory sub order no/ factory sub order no breakdown

                    If Me.ogvAdjustSubFONo.RowCount <= 0 Then
                        HI.MG.ShowMsg.mInfo("ไม่พบรายการข้อมูลใบสั่งผลิตย่อย !!!", 1406260718, Me.Text, , MessageBoxIcon.Warning)
                        Exit Sub
                    End If
                    If Not HI.MG.ShowMsg.mConfirmProcess(MG.ShowMsg.ProcessType.mSave, Me.Text) Then Exit Sub

                    If W_PRCbSaveAdjustFONoSub() = True Then
                        HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                        ocmRefresh.PerformClick()
                    Else
                        HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                    End If

                Case eTabIndexs.oTabFactorySubOrderNoBreakdown
                    '...save only data factory sub order no breakdown
                    If Me.ogvAdjustSubFONoBreakdown.RowCount <= 0 Then
                        HI.MG.ShowMsg.mInfo("ไม่พบรายการข้อมูลใบสั่งผลิตย่อย !!!", 1406260719, Me.Text, , MessageBoxIcon.Warning)
                        Exit Sub
                    End If
                    If Not HI.MG.ShowMsg.mConfirmProcess(MG.ShowMsg.ProcessType.mSave, Me.Text) Then Exit Sub
                    If W_PRCBSaveAdjustFONoSubBreakdown() = True Then
                        HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.otcFactoryOrderNo.SelectedTabPage.Name)
                        ocmRefresh.PerformClick()
                    Else
                        HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.otcFactoryOrderNo.SelectedTabPage.Name)
                    End If

            End Select

            Call UpDateMasterStyle()

        Catch ex As Exception
            If System.Diagnostics.Debugger.IsAttached = True Then
                Throw New Exception(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString())
            End If
        End Try
    End Sub

    Private Function UpDateMasterStyle() As Boolean
        Try
            Dim _Cmd As String = "" : Dim _oDt As DataTable
            For Each x As DataRow In PDataTableTmp.Rows
                _Cmd = "Select Max(B.FNCMDisAmt) AS FNCMDisAmt From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub_BreakDown AS B WITH(NOLOCK) "
                _Cmd &= vbCrLf & "INNER JON   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrder AS O WITH(NOLOCK) ON B.FTOrderNo = O.FTOrderNo"
                _Cmd &= vbCrLf & "WHERE O.FNHSysStyleId=" & CInt("0" & x!FNHSysStyleId.ToString)
                _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_MERCHAN)
                For Each R As DataRow In _oDt.Rows
                    _Cmd = "Update [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TMERMStyle"
                    _Cmd &= vbCrLf & "Set FNCMDisAmt=" & CDbl("0" & R!FNCMDisAmt.ToString)
                    _Cmd &= vbCrLf & ", FNNetCM = Isnull(FNCM,0)-" & CDbl("0" & R!FNCMDisAmt.ToString)
                    _Cmd &= vbCrLf & "WHERE FNHSysStyleId=" & CInt("0" & R!FNHSysStyleId.ToString)
                    HI.Conn.SQLConn.ExecuteNonQuery(_Cmd, Conn.DB.DataBaseName.DB_MASTER)
                Next
            Next
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub PROC_DELETE(sender As Object, e As EventArgs) Handles ocmDelete.Click
        'If System.Diagnostics.Debugger.IsAttached = False Then Exit Sub

        Try
            If Me.ogvAdjustFONo.RowCount < 1 Then Exit Sub
            CType(ogdAdjustFONo.DataSource, DataTable).AcceptChanges()



            If FNHSysBuyId.Text = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FNHSysBuyId_lbl.Text)
                Exit Sub
            End If

            Dim tComfirmDelete As String
            Select Case HI.ST.Lang.Language

                Case HI.ST.Lang.eLang.TH
                    tComfirmDelete = "ท่านแน่ใจแล้วใช่หรือไม่ ที่จะทำการลบข้อมูล"
                Case Else
                    tComfirmDelete = "Are you sure, do you want to delete data"
            End Select

            If Not HI.MG.ShowMsg.mConfirmProcess(MG.ShowMsg.ProcessType.mDelete, "") = True Then Exit Sub

            If W_PRCbDeleteFONo() = True Then
                HI.MG.ShowMsg.mProcessComplete(HI.MG.ShowMsg.ProcessType.mDelete, Me.Text)
                Me.ocmRefresh.PerformClick()
            Else
                HI.MG.ShowMsg.mProcessNotComplete(HI.MG.ShowMsg.ProcessType.mDelete, Me.Text)
            End If

        Catch ex As Exception
            If System.Diagnostics.Debugger.IsAttached = True Then
                MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
            End If
        End Try

    End Sub

    Private Sub PROC_REFRESH(sender As Object, e As EventArgs) Handles ocmRefresh.Click
        'If System.Diagnostics.Debugger.IsAttached = False Then Exit Sub

        Try

            Dim _Spls As New HI.TL.SplashScreen("Loading... Please Wait....")

            Call W_PRCbLoadFONo()
            Call W_PRCbLoadFONoSub()
            Call W_PRCbLoadFONoSubBreakdown()

            ogvAdjustFONo.ClearColumnsFilter()
            ogvAdjustFONo.ActiveFilter.Clear()

            ogvAdjustSubFONo.ClearColumnsFilter()
            ogvAdjustSubFONo.ActiveFilter.Clear()

            ogvAdjustSubFONoBreakdown.ClearColumnsFilter()
            ogvAdjustSubFONoBreakdown.ActiveFilter.Clear()

            Me.otcFactoryOrderNo.SelectedTabPageIndex = eTabIndexs.oTabFactoryOrderNo

            _Spls.Close()

            'For Each oColGrdView As DevExpress.XtraGrid.Columns.GridColumn In DirectCast(Me.ogdAdjustFONo.Views(0), DevExpress.XtraGrid.Views.Grid.GridView).Columns
            '    With oColGrdView
            '        .OptionsColumn.AllowEdit = True
            '    End With
            'Next

            'If Not HI.ST.SysInfo.Admin Then

            '    For Each oColGrdView As DevExpress.XtraGrid.Columns.GridColumn In DirectCast(Me.ogdAdjustFONo.Views(0), DevExpress.XtraGrid.Views.Grid.GridView).Columns
            '        With oColGrdView
            '            .OptionsColumn.AllowEdit = False
            '        End With
            '    Next
            'End If

            'Call W_PRCxExpandViewDetail(False)

        Catch ex As Exception
            If System.Diagnostics.Debugger.IsAttached = True Then
                MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
            End If
        End Try

        Me.FNHSysCmpId.Focus()

    End Sub

    Private Sub PROC_REFRESH_REM_20140611(sender As Object, e As EventArgs)
        REM If System.Diagnostics.Debugger.IsAttached = False Then Exit Sub
        REM 2014/05/27 Call W_PRCbShowBrowseData()

        Try
            'Dim oRepositoryFTCmpCode As DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit = New DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit()
            ' oRepositoryFTCmpCode.Buttons(0).Tag = 11
            'Me.ogvAdjustFONo.Columns("FTCmpCode").ColumnEdit = oRepositoryFTCmpCode

            'Dim oRepositoryFTShipModeCode As DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit = New DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit()
            'oRepositoryFTShipModeCode.Buttons(0).Tag = 91
            'Me.ogvAdjustImportSubOrder.Columns("FTShipModeCode").ColumnEdit = oRepositoryFTShipModeCode

            '=================================================================================================================
            Dim lstRegionName As List(Of System.String) = New List(Of System.String)()
            Dim i As Integer
            For i = 0 To 5 - 1 Step i + 1
                'lstRegionName.Add(i.ToString())
                Select Case i
                    Case 0 : lstRegionName.Add("Bat")
                    Case 1 : lstRegionName.Add("Cat")
                    Case 2 : lstRegionName.Add("Rat")
                    Case 3 : lstRegionName.Add("Pegion")
                    Case 4 : lstRegionName.Add("Zebra")
                End Select
            Next

            'Dim myLookup As RepositoryItemLookUpEdit = New RepositoryItemLookUpEdit()

            'myLookup.DisplayMember = "Key"
            'myLookup.ValueMember = "Value"

            'myLookup.Columns.Add(New LookUpColumnInfo("Key", 0, "Test Key"))
            'myLookup.DataSource = lst

            'ogvAdjustImportSubOrder.Columns("FTShipPortCode").ColumnEdit = myLookup

            REM 2014/06/11
            'Dim RegionNameEditor As RepositoryItemComboBox = New RepositoryItemComboBox()
            'RegionNameEditor.Items.AddRange(lstRegionName)
            'Me.ogdAdjustFONo.RepositoryItems.Add(RegionNameEditor)
            'Me.ogvAdjustImportSubOrder.Columns("FTShipPortCode").ColumnEdit = RegionNameEditor
            '=================================================================================================================

            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_MERCHAN)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand

            tSql = ""
            tSql = "--DECLARE @FTOrderNo AS NVARCHAR(30);"
            tSql &= Environment.NewLine & "DECLARE @FTLang AS NVARCHAR(30);"
            tSql &= Environment.NewLine & "--SET @FTOrderNo = N'N140500010';"

            Select Case HI.ST.Lang.Language

                Case HI.ST.Lang.eLang.TH
                    tSql &= Environment.NewLine & "SET @FTLang = N'" & HI.ST.Lang.Language.ToString() & "';"
                Case Else
                    tSql &= Environment.NewLine & "SET @FTLang = N'" & HI.ST.Lang.Language.ToString() & "';"
            End Select

            tSql &= Environment.NewLine & "SELECT '0' AS FTChk,"
            tSql &= Environment.NewLine & "       A.FNHSysCmpId AS FNHSysCmpId_Hide, B.FTCmpCode AS FNHSysCmpId ,CASE WHEN @FTLang = N'EN' THEN B.FTCmpNameEN ELSE B.FTCmpNameTH END AS FNHSysCmpId_None,"
            tSql &= Environment.NewLine & "       A.FNHSysCmpRunId AS FNHSysCmpRunId, C.FTCmpRunCode AS FTCmpRunCode, CASE WHEN @FTLang = N'EN' THEN C.FTCmpRunNameEN ELSE C.FTCmpRunNameTH END AS FTCmpRunName,"
            tSql &= Environment.NewLine & "       A.FNHSysStyleId AS FNHSysStyleId, D.FTStyleCode AS FTStyleCode, CASE WHEN @FTLang = N'EN' THEN D.FTStyleNameEN ELSE D.FTStyleNameTH END AS FTStyleName,"
            tSql &= Environment.NewLine & "       A.FTOrderNo AS FTOrderNo, CONVERT(VARCHAR(10), CAST(A.FDOrderDate AS DATE), 103) AS FDOrderDate,"
            tSql &= Environment.NewLine & "       E.FNListIndex AS FNOrderType,"
            tSql &= Environment.NewLine & "       CASE WHEN @FTLang = N'EN' THEN E.FTNameEN ELSE E.FTNameTH END AS FTOrderType,"
            tSql &= Environment.NewLine & "       A.FTPORef AS FTPORef,"
            tSql &= Environment.NewLine & "       A.FNHSysCustId AS FNHSysCustId,"
            tSql &= Environment.NewLine & "       F.FTCustCode AS FTCustCode, CASE WHEN @FTLang = N'EN' THEN F.FTCustNameEN ELSE F.FTCustNameTH END AS FTCustName,"
            tSql &= Environment.NewLine & "       A.FNHSysAgencyId AS FNHSysAgencyId_Hide, G.FTAgencyCode AS FNHSysAgencyId, CASE WHEN @FTLang = N'EN' THEN G.FTAgencyNameEN ELSE G.FTAgencyNameTH END AS FNHSysAgencyId_None,"
            tSql &= Environment.NewLine & "       A.FNHSysProdTypeId AS FNHSysProdTypeId,"
            tSql &= Environment.NewLine & "       H.FTProdTypeCode AS FTProdTypeCode, CASE WHEN @FTLang = N'EN' THEN H.FTProdTypeNameEN ELSE H.FTProdTypeNameTH END AS FTProdTypeName,"
            tSql &= Environment.NewLine & "       A.FNHSysBrandId AS FNHSysBrandId,"
            tSql &= Environment.NewLine & "       I.FTBrandCode AS FTBrandCode, CASE WHEN @FTLang = N'EN' THEN I.FTBrandNameEN ELSE I.FTBrandNameTH END AS FTBrandName,"
            tSql &= Environment.NewLine & "       A.FNHSysBuyerId AS FNHSysBuyerId, J.FTBuyerCode AS FTBuyerCode, CASE WHEN @FTLang = N'EN' THEN J.FTBuyerNameEN ELSE J.FTBuyerNameTH END AS FTBuyerName,"
            tSql &= Environment.NewLine & "       A.FNHSysBuyId AS FNHSysBuyId, K.FTBuyCode AS FTBuyCode, CASE WHEN @FTLang = N'EN' THEN K.FTBuyNameEN ELSE K.FTBuyNameTH END AS FTBuyName,"
            tSql &= Environment.NewLine & "       A.FNHSysPlantId AS FNHSysPlantId, L.FTPlantCode AS FTPlantCode, CASE WHEN @FTLang = N'EN' THEN L.FTPlantNameEN ELSE L.FTPlantNameTH END AS FTPlantName,"
            tSql &= Environment.NewLine & "       A.FNHSysBuyGrpId AS FNHSysBuyGrpId, M.FTBuyGrpCode AS FTBuyGrpCode, CASE WHEN @FTLang = N'EN' THEN M.FTBuyGrpNameEN ELSE M.FTBuyGrpNameTH END AS FTBuyGrpName,"
            tSql &= Environment.NewLine & "       A.FNHSysMerTeamId AS FNHSysMerTeamId, N.FTMerTeamCode AS FTMerTeamCode, CASE WHEN @FTLang = N'EN' THEN N.FTMerTeamNameEN ELSE N.FTMerTeamNameTH END AS FTMerTeamName,"
            tSql &= Environment.NewLine & "       A.FNHSysVenderPramId AS FNHSysVenderPramId, O.FTVenderPramCode AS FTVenderPramCode, CASE WHEN @FTLang = N'EN' THEN O.FTVenderPramNameEN ELSE O.FTVenderPramNameTH END AS FTVenderPramName"
            tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrder] AS A WITH(NOLOCK) LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCNMCmp] AS B ON A.FNHSysCmpId = B.FNHSysCmpId"
            tSql &= Environment.NewLine & "     INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCNMCmpRun] AS C WITH(NOLOCK) ON A.FNHSysCmpRunId = C.FNHSysCmpRunId"
            tSql &= Environment.NewLine & "     INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMStyle] AS D WITH(NOLOCK) ON A.FNHSysStyleId = D.FNHSysStyleId"
            tSql &= Environment.NewLine & "     INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SYSTEM) & "]..[HSysListData] AS E WITH(NOLOCK) ON A.FNOrderType = E.FNListIndex"
            tSql &= Environment.NewLine & "     INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCNMCustomer] AS F WITH(NOLOCK) ON A.FNHSysCustId = F.FNHSysCustId"
            tSql &= Environment.NewLine & "     LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCNMAgency] AS G WITH(NOLOCK) ON A.FNHSysAgencyId = G.FNHSysAgencyId"
            tSql &= Environment.NewLine & "     INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMProductType] AS H WITH(NOLOCK) ON A.FNHSysProdTypeId = H.FNHSysProdTypeId"
            tSql &= Environment.NewLine & "     LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMBrand] AS I WITH(NOLOCK) ON A.FNHSysBrandId = I.FNHSysBrandId"
            tSql &= Environment.NewLine & "     LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMBuyer] AS J WITH(NOLOCK) ON A.FNHSysBuyerId = J.FNHSysBuyerId"
            tSql &= Environment.NewLine & "     INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMBuy] AS K WITH(NOLOCK) ON A.FNHSysBuyId = K.FNHSysBuyId"
            tSql &= Environment.NewLine & "     LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMPlant] AS L WITH(NOLOCK) ON A.FNHSysPlantId = L.FNHSysPlantId"
            tSql &= Environment.NewLine & "     LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMBuyGrp] AS M WITH(NOLOCK) ON A.FNHSysBuyGrpId = M.FNHSysBuyGrpId"
            tSql &= Environment.NewLine & "     LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMerTeam] AS N WITH(NOLOCK) ON A.FNHSysMerTeamId = N.FNHSysMerTeamId"
            tSql &= Environment.NewLine & "     LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMVenderPram] AS O WITH(NOLOCK) ON A.FNHSysVenderPramId = O.FNHSysVenderPramId"
            tSql &= Environment.NewLine & "WHERE E.FTListName = N'FNOrderType'"

            'tSql &= Environment.NewLine & "--AND A.FTOrderCreateStatus = N'Y'"
            'tSql &= Environment.NewLine & "--AND A.FTInsUser IS NULL"
            'tSql &= Environment.NewLine & "--AND A.FTUpdUser IS NULL"
            'tSql &= Environment.NewLine & "--AND A.FNHSysCmpId IS NULL;"

            tSql &= Environment.NewLine & "      AND (A.FTStateOrderApp IS NULL OR A.FTStateOrderApp = N'0')"

            REM 2014/06/06
            'If UCase(HI.ST.UserInfo.UserName) <> "ADMIN" Then
            '    tSql &= Environment.NewLine & "      AND A.FNHSysMerTeamId = (SELECT A.FNHSysMerTeamId AS FNHSysMerTeamId"
            '    tSql &= Environment.NewLine & "                               FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SECURITY) & "]..[TSEUserLogin] AS A WITH(NOLOCK)"
            '    tSql &= Environment.NewLine & "                               WHERE A.FTUserName = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "')"
            'End If

            If Not (HI.ST.SysInfo.Admin) Then
                tSql &= Environment.NewLine & "      AND A.FNHSysMerTeamId = (SELECT A.FNHSysMerTeamId AS FNHSysMerTeamId"
                tSql &= Environment.NewLine & "                               FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SECURITY) & "]..[TSEUserLogin] AS A WITH(NOLOCK)"
                tSql &= Environment.NewLine & "                               WHERE A.FTUserName = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "')"
            End If

            Dim oCmdOrderNo As New SqlCommand

            'oCmdOrderNo.Connection = HI.Conn.SQLConn.Cnn
            'oCmdOrderNo.CommandType = CommandType.Text
            'oCmdOrderNo.CommandText = tSql

            'Dim sqlDA As New SqlDataAdapter(oCmdOrderNo.CommandText, HI.Conn.SQLConn._ConnString)

            REM Dim oDBds As New DataSet()
            oDBds = New DataSet()

            Dim sqlCmd As New SqlCommand

            '...กรองเฉพาะรายการที่เป็นรายการตรงกับรหัสทีมเมอร์แชนไดเซอร์ที่ตนเองสังกัดอยู่เท่านั้น และ พนง.ที่อยู่ภายในทีมเดียวกันนั้นสามารถที่จะทำการแก้ไขรายการทุกรายการได้ถ้าเป็นรายการของทีมนั้นๆ

            oCmdOrderNo.Connection = HI.Conn.SQLConn.Cnn
            oCmdOrderNo.CommandType = CommandType.StoredProcedure
            oCmdOrderNo.CommandText = "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[SP_GET_LIST_FACTORY_ORDER_INFO_AFTER_IMPORT]"
            oCmdOrderNo.Parameters.AddWithValue("@FTUserLogin", HI.ST.UserInfo.UserName)
            oCmdOrderNo.Parameters.AddWithValue("@FTLang", HI.ST.Lang.Language.ToString())

            Dim sqlDA As New SqlDataAdapter(oCmdOrderNo.CommandText, HI.Conn.SQLConn._ConnString)

            sqlDA.SelectCommand = oCmdOrderNo

            REM Dim oDBdtFactoryOrderNo As New System.Data.DataTable
            oDBdtFactoryOrderNo = New System.Data.DataTable
            oDBdtFactoryOrderNo.TableName = _tTableFactoryOrderNo '"FactoryOrderNo"

            sqlDA.Fill(oDBds, oDBdtFactoryOrderNo.TableName.ToString())

            tSql = ""
            tSql = "--DECLARE @FTOrderNo AS NVARCHAR(30);"
            tSql &= Environment.NewLine & "DECLARE @FTLang AS NVARCHAR(30);"
            tSql &= Environment.NewLine & "--SET @FTOrderNo = N'N140500010';"

            Select Case HI.ST.Lang.Language

                Case HI.ST.Lang.eLang.TH
                    tSql &= Environment.NewLine & "SET @FTLang = N'" & HI.ST.Lang.Language.ToString() & "';"
                Case Else
                    tSql &= Environment.NewLine & "SET @FTLang = N'" & HI.ST.Lang.Language.ToString() & "';"
            End Select

            tSql &= Environment.NewLine & "SELECT A.FTOrderNo, A.FTSubOrderNo, CONVERT(VARCHAR(10), CAST(A.FDSubOrderDate AS DATE), 103) AS FDSubOrderDate,"
            tSql &= Environment.NewLine & "       CONVERT(VARCHAR(10), CAST(A.FDProDate AS DATE), 103) AS FDProDate, CONVERT(VARCHAR(10), CAST(A.FDShipDate AS DATE), 103) AS FDShipDate,"
            tSql &= Environment.NewLine & "       A.FNHSysContinentId, B.FTContinentCode, CASE WHEN @FTLang = N'EN' THEN B.FTContinentNameEN ELSE B.FTContinentNameTH END AS FTContinentName,"
            tSql &= Environment.NewLine & "       A.FNHSysCountryId, C.FTCountryCode, CASE WHEN @FTLang = N'EN' THEN C.FTCountryNameEN ELSE C.FTCountryNameTH END AS FTCountryName,"
            tSql &= Environment.NewLine & "       A.FNHSysProvinceId, D.FTProvinceCode, CASE WHEN @FTLang = N'EN' THEN D.FTProvinceNameEN ELSE D.FTProvinceNameTH END AS FTProvinceName,"
            tSql &= Environment.NewLine & "       A.FNHSysShipModeId, E.FTShipModeCode, CASE WHEN @FTLang = N'EN' THEN E.FTShipModeNameEN ELSE E.FTShipModenNameTH END AS FTShipModeName,"
            tSql &= Environment.NewLine & "       A.FNHSysShipPortId, F.FTShipPortCode, CASE WHEN @FTLang = N'EN' THEN F.FTShipPortNameEN ELSE F.FTShipPortNameTH END AS FTShipPortName,"
            tSql &= Environment.NewLine & "       A.FNHSysCurId, G.FTCurCode, CASE WHEN @FTLang = N'EN' THEN G.FTCurDescEN ELSE G.FTCurDescTH END AS FTCurDesc,"
            tSql &= Environment.NewLine & "       A.FNHSysGenderId, H.FTGenderCode, CASE WHEN @FTLang = N'EN' THEN H.FTGenderNameEN ELSE H.FTGenderNameTH END AS FTGenderName,"
            tSql &= Environment.NewLine & "       A.FNHSysUnitId, I.FTUnitCode, CASE WHEN @FTLang = N'EN' THEN I.FTUnitNameEN ELSE I.FTUnitNameTH END AS FTUnitName,"
            tSql &= Environment.NewLine & "       ISNULL(A.FTStateEmb, 0) AS FTStateEmb, ISNULL(A.FTStatePrint, 0) AS FTStatePrint, ISNULL(A.FTStateHeat, 0) AS FTStateHeat, ISNULL(A.FTStateLaser, 0) AS FTStateLaser, ISNULL(A.FTStateWindows, 0) AS FTStateWindows"
            tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrder] AS M1 WITH(NOLOCK) INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub] AS A WITH(NOLOCK) ON M1.FTOrderNo = A.FTOrderNo"
            tSql &= Environment.NewLine & "     LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCNMContinent] AS B WITH(NOLOCK) ON A.FNHSysContinentId = B.FNHSysContinentId"
            tSql &= Environment.NewLine & "     LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCNMCountry] AS C WITH(NOLOCK) ON A.FNHSysCountryId = C.FNHSysCountryId"
            tSql &= Environment.NewLine & "     LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCMMProvince] AS D WITH(NOLOCK) ON A.FNHSysProvinceId = D.FNHSysProvinceId"
            tSql &= Environment.NewLine & "     LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCNMShipMode] AS E WITH(NOLOCK) ON A.FNHSysShipModeId = E.FNHSysShipModeId"
            tSql &= Environment.NewLine & "     LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCNMShipPort] AS F WITH(NOLOCK) ON A.FNHSysShipPortId = F.FNHSysShipPortId"
            tSql &= Environment.NewLine & "     INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TFINMCurrency] AS G WITH(NOLOCK) ON A.FNHSysCurId = G.FNHSysCurId"
            tSql &= Environment.NewLine & "     LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMGender] AS H WITH(NOLOCK) ON A.FNHSysGenderId = H.FNHSysGenderId"
            tSql &= Environment.NewLine & "     LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCNMUnit] AS I WITH(NOLOCK) ON A.FNHSysUnitId = I.FNHSysUnitId"
            tSql &= Environment.NewLine & "--WHERE A.FTOrderNo = @FTOrderNo"
            REM tSql &= Environment.NewLine & "WHERE (M1.FTStateOrderApp IS NULL OR M1.FTStateOrderApp = '0')"
            tSql &= Environment.NewLine & "ORDER BY A.FTSubOrderNo ASC;"

            Dim oCmdSubOrderNo As New SqlCommand

            REM 2014/06/06
            'oCmdSubOrderNo.Connection = HI.Conn.SQLConn.Cnn
            'oCmdSubOrderNo.CommandType = CommandType.Text
            'oCmdSubOrderNo.CommandText = tSql

            'Dim sqlDA_SubOrderNo As New SqlDataAdapter(oCmdSubOrderNo.CommandText, HI.Conn.SQLConn._ConnString)

            'sqlDA_SubOrderNo.Fill(oDBds, "FactorySubOrderNo")

            oCmdSubOrderNo.Connection = HI.Conn.SQLConn.Cnn
            oCmdSubOrderNo.CommandType = CommandType.StoredProcedure
            oCmdSubOrderNo.CommandText = "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[SP_GET_LIST_FACTORY_SUBORDER_INFO_AFTER_IMPORT]"
            oCmdSubOrderNo.Parameters.AddWithValue("@FTUserLogin", HI.ST.UserInfo.UserName)
            oCmdSubOrderNo.Parameters.AddWithValue("@FTLang", HI.ST.Lang.Language.ToString())

            Dim sqlDA_SubOrderNo As New SqlDataAdapter(oCmdSubOrderNo.CommandText, HI.Conn.SQLConn._ConnString)

            sqlDA_SubOrderNo.SelectCommand = oCmdSubOrderNo

            REM Dim oDBdtFactorySubOrderNo As New System.Data.DataTable
            oDBdtFactorySubOrderNo = New System.Data.DataTable
            oDBdtFactorySubOrderNo.TableName = _tTableFactorySubOrderNo

            sqlDA_SubOrderNo.Fill(oDBds, oDBdtFactorySubOrderNo.TableName.ToString())

            Try
                Dim oRelation As DataRelation
                Dim FTPrimaryKeyColumn As DataColumn
                Dim FTForeignKeyColumn As DataColumn

                FTPrimaryKeyColumn = oDBds.Tables(_tTableFactoryOrderNo).Columns("FTOrderNo")
                FTForeignKeyColumn = oDBds.Tables(_tTableFactorySubOrderNo).Columns("FTOrderNo")

                oRelation = New DataRelation(_tRelationName, FTPrimaryKeyColumn, FTForeignKeyColumn, False)

                oDBds.Relations.Add(oRelation)

            Catch ex As Exception
                If System.Diagnostics.Debugger.IsAttached = True Then
                    Throw New Exception(ex.Message().ToString())
                End If
            End Try

            Me.ogdAdjustFONo.DataSource = oDBds.Tables(_tTableFactoryOrderNo)
            Me.ogdAdjustFONo.Refresh()
            Me.ogdAdjustFONo.ForceInitialize()

            REM Me.ogvAdjustFONo.OptionsBehavior.AutoExpandAllGroups = False
            REM Me.ogvAdjustFONo.ExpandAllGroups()
            Me.ogvAdjustFONo.OptionsView.ShowGroupPanel = False
            Me.ogvAdjustFONo.OptionsView.ColumnAutoWidth = False
            Me.ogvAdjustFONo.RefreshData()

            '============================================================================================================
            'Dim oGridViewSubOrderNo As New DevExpress.XtraGrid.Views.Grid.GridView(Me.ogdAdjustFONo)
            ''Dim oGridViewOrderNo As New DevExpress.XtraGrid.Views.Grid.GridView(Me.ogdAdjustFONo)

            'oGridViewSubOrderNo.OptionsView.ShowGroupPanel = False
            'oGridViewSubOrderNo.OptionsBehavior.AutoExpandAllGroups = True
            'oGridViewSubOrderNo.ExpandAllGroups()
            'oGridViewSubOrderNo.OptionsView.ColumnAutoWidth = False
            'oGridViewSubOrderNo.RefreshData()
            '=============================================================================================================

            REM 2014/06/11
            'ogdAdjustFONo.LevelTree.Nodes.Add(_tRelationName, Me.ogvAdjustImportSubOrder)

            'Me.ogvAdjustImportSubOrder.OptionsView.ShowGroupPanel = False
            'REM Me.ogvAdjustImportSubOrder.OptionsBehavior.AutoExpandAllGroups = True
            'REM Me.ogvAdjustImportSubOrder.ExpandAllGroups()
            'Me.ogvAdjustImportSubOrder.OptionsView.ColumnAutoWidth = False
            'Me.ogvAdjustImportSubOrder.RefreshData()

            'Me.ogvAdjustImportSubOrder.OptionsBehavior.Editable = True

            'CType(Me.ogdAdjustFONo.MainView, DevExpress.XtraGrid.Views.Grid.GridView).Columns(0).ColumnEdit = RepositoryItemFTAgencyCode


            'Me.ogvAdjustImportSubOrder.PopulateColumns(oDBds.Tables(_tTableFactorySubOrderNo))

            'Dim oRepositoryItemDateEdit As RepositoryItemDateEdit = New RepositoryItemDateEdit()

            'Me.ogvAdjustImportSubOrder.Columns("FDSubOrderDate").ColumnEdit = oRepositoryItemDateEdit
            'Me.ogvAdjustImportSubOrder.Columns("FDProDate").ColumnEdit = oRepositoryItemDateEdit
            'Me.ogvAdjustImportSubOrder.Columns("FDShipDate").ColumnEdit = oRepositoryItemDateEdit

            REM 2014/06/11
            'Select Case HI.ST.Lang.Language
            '    Case HI.ST.Lang.eLang.EN
            '        Me.ogvAdjustImportSubOrder.ViewCaption = "Factory Sub Order No."
            '    Case HI.ST.Lang.eLang.TH
            '        Me.ogvAdjustImportSubOrder.ViewCaption = "รายการเลขที่ใบสั่งผลิตย่อย"
            '    Case HI.ST.Lang.eLang.KM
            '        Me.ogvAdjustImportSubOrder.ViewCaption = "Factory Sub Order No."
            '    Case HI.ST.Lang.eLang.VT
            '        Me.ogvAdjustImportSubOrder.ViewCaption = "Factory Sub Order No."
            'End Select

            'Dim oDBdtFactoryImpotSubOrderNo As System.Data.DataTable = CType(ogvAdjustImportSubOrder.DataSource, System.Data.DataTable)

            'Me.ogvAdjustImportSubOrder.RefreshData()
            'Me.ogvAdjustImportSubOrder.OptionsView.ColumnAutoWidth = False

            'oGridViewOrderNo.OptionsView.ShowFilterPanelMode = Views.Base.ShowFilterPanelMode.ShowAlways
            'oGridViewOrderNo.OptionsView.ShowGroupPanel = False

            'oGridViewOrderNo.OptionsView.ColumnAutoWidth = False
            'oGridViewOrderNo.RefreshData()

            ' Collapse all the details opened for the master rows in the main view.
            'CType(Me.ogdAdjustFONo.MainView, DevExpress.XtraGrid.Views.Grid.GridView).ExpandGroupLevel(1)

            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            'For Each oColGrdView As DevExpress.XtraGrid.Columns.GridColumn In DirectCast(Me.ogdAdjustFONo.Views(0), DevExpress.XtraGrid.Views.Grid.GridView).Columns
            '    With oColGrdView
            '        .OptionsColumn.AllowEdit = True
            '    End With
            'Next

            'If Not HI.ST.SysInfo.Admin Then

            '    For Each oColGrdView As DevExpress.XtraGrid.Columns.GridColumn In DirectCast(Me.ogdAdjustFONo.Views(0), DevExpress.XtraGrid.Views.Grid.GridView).Columns
            '        With oColGrdView
            '            .OptionsColumn.AllowEdit = False
            '        End With
            '    Next
            'End If

            Call W_PRCxExpandViewDetail(False)

        Catch ex As Exception
            If System.Diagnostics.Debugger.IsAttached = True Then
                MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
            End If
        End Try

    End Sub

    Private Sub PROC_CLEAR(send As Object, e As EventArgs) Handles ocmclearclsr.Click
        REM If System.Diagnostics.Debugger.IsAttached = False Then Exit Sub

        Me.ogdAdjustFONo.DataSource = Nothing
        Me.ogdAdjustFONo.Refresh()
        Me.ogdAdjustSubFONo.DataSource = Nothing
        Me.ogdAdjustSubFONo.Refresh()
        Me.ogdAdjustSubFONoBreakdown.DataSource = Nothing
        Me.ogdAdjustSubFONoBreakdown.Refresh()

        ogvAdjustFONo.ClearColumnsFilter()
        ogvAdjustFONo.ActiveFilter.Clear()

        ogvAdjustSubFONo.ClearColumnsFilter()
        ogvAdjustSubFONo.ActiveFilter.Clear()

        ogvAdjustSubFONoBreakdown.ClearColumnsFilter()
        ogvAdjustSubFONoBreakdown.ActiveFilter.Clear()

        HI.TL.HandlerControl.ClearControl(Me)

        Me.otcFactoryOrderNo.SelectedTabPageIndex = eTabIndexs.oTabFactoryOrderNo

        Me.ockAll.Checked = False
        Me.FNHSysCmpId.Text = ""
        Me.FNHSysCmpId.Focus()

    End Sub

    Private Sub PROC_EXIT(sender As Object, e As EventArgs) Handles ocmExit.Click
        Me.Close()
    End Sub

#End Region

#Region "Sub Procedure And Sub Function"

    Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.

        '...LookupEdit browse for factory order no.
        '-------------------------------------------------------------
        RepositoryItemFTBuyerCode.Buttons(0).Tag = 86
        RepositoryItemFTBuyCode.Buttons(0).Tag = 113
        RepositoryItemFTProdTypeCode.Buttons(0).Tag = 88
        RepositoryItemFTBrandCode.Buttons(0).Tag = 8
        RepositoryItemFTMerTeamCode.Buttons(0).Tag = 153
        '-------------------------------------------------------------

        '...LookupEdit browse for factory sub order no.
        '-------------------------------------------------------------
        RepositoryItemFNHSysContinentId.Buttons(0).Tag = 172 '2
        RepositoryItemFNHSysCountryId.Buttons(0).Tag = 171 '3
        RepositoryItemFNHSysProvinceId.Buttons(0).Tag = 82
        RepositoryItemFNHSysShipModeId.Buttons(0).Tag = 91
        RepositoryItemFNHSysShipPortId.Buttons(0).Tag = 41
        RepositoryItemFNHSysCurId.Buttons(0).Tag = 16
        RepositoryItemFNHSysGenderId.Buttons(0).Tag = 90
        RepositoryItemFNHSysUnitId.Buttons(0).Tag = 5
        ReposFTBuyGrpCode.Buttons(0).Tag = 167
        '-------------------------------------------------------------

        'With RepositoryItemFTContinentCode
        '    RemoveHandler .Click, AddressOf HI.TL.HandlerControl.DynamicResponButtone_Gotocus
        '    RemoveHandler .ButtonClick, AddressOf HI.TL.HandlerControl.DynamicResponButtone_ButtonClick
        '    RemoveHandler .EditValueChanged, AddressOf HI.TL.HandlerControl.DynamicResponButtonedit_EditValueChanged
        '    RemoveHandler .Leave, AddressOf HI.TL.HandlerControl.DynamicResponButtonedit_Leave
        'End With

        REM 2014/06/16 ContinentId, CountryId
        'With RepositoryItemFNHSysContinentId
        '    RemoveHandler .ButtonClick, AddressOf HI.TL.HandlerControl.DynamicResponButtoneSysHide_ButtonClick
        '    RemoveHandler .EditValueChanged, AddressOf HI.TL.HandlerControl.DynamicResponButtoneditSysHide_EditValueChanged
        '    RemoveHandler .Leave, AddressOf HI.TL.HandlerControl.DynamicResponButtoneditSysHide_Leave

        '    AddHandler .ButtonClick, AddressOf HI.TL.HandlerControl.DynamicResponButtone_ButtonClick
        '    AddHandler .EditValueChanged, AddressOf HI.TL.HandlerControl.DynamicResponButtonedit_EditValueChanged
        '    AddHandler .Leave, AddressOf HI.TL.HandlerControl.DynamicResponButtonedit_Leave
        'End With

        'With RepositoryItemFNHSysCountryId
        '    RemoveHandler .ButtonClick, AddressOf HI.TL.HandlerControl.DynamicResponButtoneSysHide_ButtonClick
        '    RemoveHandler .EditValueChanged, AddressOf HI.TL.HandlerControl.DynamicResponButtoneditSysHide_EditValueChanged
        '    RemoveHandler .Leave, AddressOf HI.TL.HandlerControl.DynamicResponButtoneditSysHide_Leave

        '    AddHandler .ButtonClick, AddressOf HI.TL.HandlerControl.DynamicResponButtone_ButtonClick
        '    AddHandler .EditValueChanged, AddressOf HI.TL.HandlerControl.DynamicResponButtonedit_EditValueChanged
        '    AddHandler .Leave, AddressOf HI.TL.HandlerControl.DynamicResponButtonedit_Leave
        'End With

        REM 2014/06/16
        'HI.TL.HandlerControl.AddHandlerObj(RepositoryItemFDSubOrderDate)
        'HI.TL.HandlerControl.AddHandlerObj(RepositoryItemFDProDate)
        'HI.TL.HandlerControl.AddHandlerObj(RepositoryItemFDShipDate)

        'With RepositoryItemFNHSysContinentId
        '    AddHandler .Click, AddressOf HI.TL.HandlerControl.DynamicResponButtone_Gotocus
        '    AddHandler .ButtonClick, AddressOf HI.TL.HandlerControl.DynamicResponButtone_ButtonClick
        '    AddHandler .EditValueChanged, AddressOf HI.TL.HandlerControl.DynamicResponButtonedit_EditValueChanged
        '    AddHandler .Leave, AddressOf HI.TL.HandlerControl.DynamicResponButtonedit_Leave
        'End With

        'With RepositoryItemFNHSysCountryId
        '    AddHandler .Click, AddressOf HI.TL.HandlerControl.DynamicResponButtone_Gotocus
        '    AddHandler .ButtonClick, AddressOf HI.TL.HandlerControl.DynamicResponButtone_ButtonClick
        '    AddHandler .EditValueChanged, AddressOf HI.TL.HandlerControl.DynamicResponButtonedit_EditValueChanged
        '    AddHandler .Leave, AddressOf HI.TL.HandlerControl.DynamicResponButtonedit_Leave
        'End With

        'With RepositoryItemFNHSysProvinceId
        '    AddHandler .Click, AddressOf HI.TL.HandlerControl.DynamicResponButtone_Gotocus
        '    AddHandler .ButtonClick, AddressOf HI.TL.HandlerControl.DynamicResponButtone_ButtonClick
        '    AddHandler .EditValueChanged, AddressOf HI.TL.HandlerControl.DynamicResponButtonedit_EditValueChanged
        '    AddHandler .Leave, AddressOf HI.TL.HandlerControl.DynamicResponButtonedit_Leave
        'End With

        'With RepositoryItemFNHSysShipModeId
        '    AddHandler .Click, AddressOf HI.TL.HandlerControl.DynamicResponButtone_Gotocus
        '    AddHandler .ButtonClick, AddressOf HI.TL.HandlerControl.DynamicResponButtone_ButtonClick
        '    AddHandler .EditValueChanged, AddressOf HI.TL.HandlerControl.DynamicResponButtonedit_EditValueChanged
        '    AddHandler .Leave, AddressOf HI.TL.HandlerControl.DynamicResponButtonedit_Leave
        'End With

        'With RepositoryItemFNHSysShipPortId
        '    AddHandler .Click, AddressOf HI.TL.HandlerControl.DynamicResponButtone_Gotocus
        '    AddHandler .ButtonClick, AddressOf HI.TL.HandlerControl.DynamicResponButtone_ButtonClick
        '    AddHandler .EditValueChanged, AddressOf HI.TL.HandlerControl.DynamicResponButtonedit_EditValueChanged
        '    AddHandler .Leave, AddressOf HI.TL.HandlerControl.DynamicResponButtonedit_Leave
        'End With

        'With RepositoryItemFNHSysCurId
        '    AddHandler .Click, AddressOf HI.TL.HandlerControl.DynamicResponButtone_Gotocus
        '    AddHandler .ButtonClick, AddressOf HI.TL.HandlerControl.DynamicResponButtone_ButtonClick
        '    AddHandler .EditValueChanged, AddressOf HI.TL.HandlerControl.DynamicResponButtonedit_EditValueChanged
        '    AddHandler .Leave, AddressOf HI.TL.HandlerControl.DynamicResponButtonedit_Leave
        'End With

        'With RepositoryItemFNHSysGenderId
        '    AddHandler .Click, AddressOf HI.TL.HandlerControl.DynamicResponButtone_Gotocus
        '    AddHandler .ButtonClick, AddressOf HI.TL.HandlerControl.DynamicResponButtone_ButtonClick
        '    AddHandler .EditValueChanged, AddressOf HI.TL.HandlerControl.DynamicResponButtonedit_EditValueChanged
        '    AddHandler .Leave, AddressOf HI.TL.HandlerControl.DynamicResponButtonedit_Leave
        'End With

        'With RepositoryItemFNHSysUnitId
        '    AddHandler .Click, AddressOf HI.TL.HandlerControl.DynamicResponButtone_Gotocus
        '    AddHandler .ButtonClick, AddressOf HI.TL.HandlerControl.DynamicResponButtone_ButtonClick
        '    AddHandler .EditValueChanged, AddressOf HI.TL.HandlerControl.DynamicResponButtonedit_EditValueChanged
        '    AddHandler .Leave, AddressOf HI.TL.HandlerControl.DynamicResponButtonedit_Leave
        'End With

    End Sub

    Private Function W_PRCdvGETViewFilterData(ByVal pGridView As DevExpress.XtraGrid.Views.Grid.GridView) As System.Data.DataView
        Try
            If Not pGridView Is Nothing Then
                If pGridView.ActiveFilter Is Nothing Or pGridView.ActiveFilterEnabled = False Or pGridView.ActiveFilter.Expression = "" Then
                    Return CType(pGridView.DataSource, System.Data.DataView)
                Else
                    Dim oDBdt As System.Data.DataTable = CType(pGridView.GridControl.DataSource, System.Data.DataTable)
                    Dim oDataViewFilterData As New System.Data.DataView(oDBdt)
                    oDataViewFilterData.RowFilter = DevExpress.Data.Filtering.CriteriaToWhereClauseHelper.GetDataSetWhere(pGridView.ActiveFilterCriteria)
                    Return oDataViewFilterData
                End If

            Else
                Return Nothing
            End If

        Catch ex As Exception
            If System.Diagnostics.Debugger.IsAttached = True Then
                MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
            End If

            Return Nothing
        End Try

    End Function

    Private Sub W_PRCxExpandViewDetail(ByVal pbExpandAll As Boolean, Optional ByVal pnRowDefault As Integer = 0)
        Try
            If pbExpandAll = True Then
                Dim nLoop As Integer
                For nLoop = 0 To Me.ogvAdjustFONo.RowCount - 1
                    Me.ogvAdjustFONo.SetMasterRowExpanded(nLoop, _tRelationName, True)
                Next nLoop
            Else
                If Me.ogvAdjustFONo.RowCount > 0 Then
                    Me.ogvAdjustFONo.SetMasterRowExpanded(pnRowDefault, _tRelationName, True)
                End If
            End If
        Catch ex As Exception
            If System.Diagnostics.Debugger.IsAttached = True Then
                MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
            End If
        End Try
    End Sub

    Private Function GridViewGetBrowseRespon(ControlObject As Object, GridView As DevExpress.XtraGrid.Views.Grid.GridView, BrowseID As Integer, ByRef _dtret As DataTable, _
                                                ByRef _BrowseCmd As String, _
                                                ByRef _BrowseSortCmd As String, _
                                                ByRef _BrowseWhereCmd As String, _
                                                ByRef _FTBrwCmdField As String, _
                                                ByRef _FTBrwCmdFieldOptional As String, _
                                                ByRef FTBrwCmdGroupBy As String, _
                                                ByRef _Command As String, _
                                                 ByRef _ConFiled As String, _
                                                _Data As String, _
                                                Optional _Editvalue As Boolean = False) As String




        Dim _Qrysql As String
        Dim _ConFieldName As String = ""
        Dim _dtbrw As DataTable
        _Qrysql = " SELECT  TOP 1    BrwID, "

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Qrysql &= vbCrLf & " FTBrwCmdTH AS FTBrwCmd "
        Else
            _Qrysql &= vbCrLf & " FTBrwCmdEN AS FTBrwCmd "
        End If

        _Qrysql &= vbCrLf & ", BrwRetID,FTConField,FTBrwCmdSort,FTBrwCmdWhere,FTBrwCmdField,FTBrwCmdFieldOptional,FTBrwCmdENGroupBy,FTBrwCmdTHGroupBy "
        _Qrysql &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysBrowse  With (NOLOCK) "
        _Qrysql &= vbCrLf & " WHERE BrwID=" & BrowseID & " "

        _dtbrw = HI.Conn.SQLConn.GetDataTable(_Qrysql, Conn.DB.DataBaseName.DB_SYSTEM)
        _Qrysql = ""
        For Each Row As DataRow In _dtbrw.Rows
            _Qrysql = " SELECT  BrwRetID, FNSeq, FTBrwField, FTRetField,Convert(nvarchar(500),'') AS ValuesRet,FTStatePropertyTag "
            _Qrysql &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysBrowseRet With (NOLOCK) "
            _Qrysql &= vbCrLf & " WHERE BrwRetID=" & Val(Row!BrwRetID.ToString) & " "

            _dtret = New DataTable
            _dtret = HI.Conn.SQLConn.GetDataTable(_Qrysql, Conn.DB.DataBaseName.DB_SYSTEM)

            _BrowseCmd = Row!FTBrwCmd.ToString
            _BrowseSortCmd = Row!FTBrwCmdSort.ToString
            _BrowseWhereCmd = Row!FTBrwCmdWhere.ToString

            _FTBrwCmdField = Row!FTBrwCmdField.ToString
            _FTBrwCmdFieldOptional = Row!FTBrwCmdFieldOptional.ToString

            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                FTBrwCmdGroupBy = Row!FTBrwCmdTHGroupBy.ToString
            Else
                FTBrwCmdGroupBy = Row!FTBrwCmdENGroupBy.ToString
            End If

            _Command = Row!FTBrwCmd.ToString

            If (_Editvalue) Then
                _ConFieldName = Row!FTConField.ToString
                _ConFiled = _ConFieldName
            End If

        Next


        Dim _Where As String = ""
        Dim _DataCon As String = ""

        Dim I As Integer = 0
        If _ConFieldName <> "" Then
            For Each _QryCon As String In _ConFieldName.Split(",")

                I = I + 1

                If I = 1 Then

                    If _Where = "" Then
                        _Where &= "     " & _QryCon & " ='" & _Data & "'  "
                    Else
                        _Where &= "   AND  " & _QryCon & " ='" & _Data & "'  "
                    End If

                Else

                    _DataCon = ""

                    With GridView
                        If Not (.Columns.ColumnByFieldName(_QryCon) Is Nothing) Then
                            _DataCon = "" & .GetRowCellValue(.FocusedRowHandle, _QryCon).ToString

                            If _Where = "" Then
                                _Where &= "     " & _QryCon & " ='" & _DataCon & "'  "
                            Else
                                _Where &= "   AND  " & _QryCon & " ='" & _DataCon & "'  "
                            End If

                        End If
                    End With

                End If
            Next
        End If

        '------------Browse Where Require Field---------------
        If _FTBrwCmdField <> "" Then
            For Each _QryCon As String In _FTBrwCmdField.Split(",")
                _DataCon = ""
                With GridView
                    If Not (.Columns.ColumnByFieldName(_QryCon.Trim()) Is Nothing) Then
                        Select Case .Columns.ColumnByFieldName(_QryCon.Trim()).ColumnType.FullName.ToString.ToUpper
                            Case Else
                                _DataCon = "" & .GetRowCellValue(.FocusedRowHandle, _QryCon.Trim())
                        End Select

                        If _Where = "" Then
                            _Where &= "     " & _QryCon & " ='" & _DataCon & "'  "
                        Else
                            _Where &= "   AND  " & _QryCon & " ='" & _DataCon & "'  "
                        End If
                    End If
                End With

            Next

        End If

        '------------Browse Where Require Field---------------

        '------------Browse Where Optional Field---------------
        If _FTBrwCmdFieldOptional <> "" Then
            For Each _QryCon As String In _FTBrwCmdFieldOptional.Split(",")
                _DataCon = ""
                With GridView
                    If Not (.Columns.ColumnByFieldName(_QryCon.Trim()) Is Nothing) Then
                        Select Case .Columns.ColumnByFieldName(_QryCon.Trim()).ColumnType.FullName.ToString.ToUpper
                            Case Else
                                _DataCon = "" & .GetRowCellValue(.FocusedRowHandle, _QryCon.Trim())
                        End Select

                        If _DataCon <> "" Then
                            If _Where = "" Then
                                _Where &= "     " & _QryCon & " ='" & _DataCon & "'  "
                            Else
                                _Where &= "   AND  " & _QryCon & " ='" & _DataCon & "'  "
                            End If
                        End If
                    End If
                End With
            Next
        End If

        '------------Browse Where Optional Field---------------
        If _Where <> "" Then
            If _BrowseWhereCmd = "" Then
                _Where = "   WHERE  " & _Where
            Else
                _Where = "   AND  " & _Where
            End If
        End If


        If Not (HI.ST.SysInfo.Admin) Then
            If Microsoft.VisualBasic.Left(ControlObject.name.ToString.ToUpper, 11) = "FNTSysEmpID".ToUpper Then
                _Where = HI.ST.Security.PermissionEmpData(_Where)
            ElseIf Microsoft.VisualBasic.Left(ControlObject.name.ToString.ToUpper, 15) = "FNTSysEmpTypeId".ToUpper Then
                _Where = HI.ST.Security.PermissionEmpType(_Where)
            End If
        End If

        _dtbrw.Dispose()

        Return _Where

    End Function

    Private Function W_PRCbValidateBFSaveData() As Boolean
        Dim _bRet As Boolean = False
        Try

            If Me.ogvAdjustFONo.RowCount > 0 Then
                REM 2014/06/27
                'If Me.FNHSysCmpId.Text.Trim <> "" Then

                '    For nLoopAdjustFONo As Integer = 0 To Me.ogvAdjustFONo.DataRowCount - 1

                '        Dim oFONoRow As DataRow = Me.ogvAdjustFONo.GetDataRow(nLoopAdjustFONo)

                '        If Not oFONoRow Is Nothing And oFONoRow.ItemArray.Count > 0 Then

                '            If oFONoRow.Item("FTChk").ToString() = "1" Then
                '                '...atleast one or more item check
                '                _bRet = True
                '                Exit For
                '            End If

                '        End If

                '    Next nLoopAdjustFONo

                '    If _bRet = False Then
                '        Dim tMsgInfoValidateListFONo As String = ""

                '        Select Case HI.ST.Lang.Language
                '            Case HI.ST.Lang.eLang.EN, HI.ST.Lang.eLang.KM, HI.ST.Lang.eLang.VT
                '                MsgBox("Please select list item factory order no to save data !!!", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
                '            Case HI.ST.Lang.eLang.TH
                '                MsgBox("กรุณาเลือกรายการใบสั่งผลิดเพื่อทำการบันทึกข้อมูล !!!", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
                '        End Select
                '    End If

                'Else
                '    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.FNHSysCmpId_lbl.Text)
                '    Me.FNHSysCmpId.Focus()
                'End If

                _bRet = True

            Else

                Select Case HI.ST.Lang.Language
                    Case HI.ST.Lang.eLang.TH
                        HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, "ปรับปรุงข้อมูลรายการใบสั่งผลิต", "ไม่พบรายการข้อมูลใบสั่งผลิต !!!")
                    Case Else
                        HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, "Adjust Factory Order No.", "Data not found !!!")

                        'Case HI.ST.Lang.eLang.KM
                        '    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, "Adjust Factory Order No.", "Data not found !!!")
                        'Case HI.ST.Lang.eLang.VT
                        '    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, "Adjust Factory Order No.", "Data not found !!!")
                End Select

            End If

        Catch ex As Exception
            If System.Diagnostics.Debugger.IsAttached = True Then
                MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
            End If
        End Try

        Return _bRet

    End Function

    Private Function W_PRCbShowBrowseData() As Boolean
        Dim _bRet As Boolean = False
        Try
            '...Header Order No.
            tSql = ""
            tSql = "--DECLARE @FTOrderNo AS NVARCHAR(30);"
            tSql &= Environment.NewLine & "DECLARE @FTLang AS NVARCHAR(30);"
            tSql &= Environment.NewLine & "--SET @FTOrderNo = N'N140500010';"

            Select Case HI.ST.Lang.Language

                Case HI.ST.Lang.eLang.TH
                    tSql &= Environment.NewLine & "SET @FTLang = N'TH';"
             
                Case Else
                    tSql &= Environment.NewLine & "SET @FTLang = N'VT';"
            End Select

            tSql &= Environment.NewLine & "SELECT A.FNHSysCmpId as FNHSysCmpId_Hide, B.FTCmpCode FNHSysCmpId , CASE WHEN @FTLang = N'EN' THEN B.FTCmpNameEN ELSE B.FTCmpNameTH END AS FTCmpName,"
            tSql &= Environment.NewLine & "       A.FNHSysCmpRunId, C.FTCmpRunCode, CASE WHEN @FTLang = N'EN' THEN C.FTCmpRunNameEN ELSE C.FTCmpRunNameTH END AS FTCmpRunName,"
            tSql &= Environment.NewLine & "       A.FNHSysStyleId, D.FTStyleCode, CASE WHEN @FTLang = N'EN' THEN D.FTStyleNameEN ELSE D.FTStyleNameTH END AS FTStyleName,"
            tSql &= Environment.NewLine & "       A.FTOrderNo, A.FDOrderDate,"
            tSql &= Environment.NewLine & "       E.FNListIndex AS FNOrderType,"
            tSql &= Environment.NewLine & "       CASE WHEN @FTLang = N'EN' THEN E.FTNameEN ELSE E.FTNameTH END AS FTOrderType,"
            tSql &= Environment.NewLine & "       A.FTPORef,"
            tSql &= Environment.NewLine & "       A.FNHSysCustId,"
            tSql &= Environment.NewLine & "       F.FTCustCode, CASE WHEN @FTLang = N'EN' THEN F.FTCustNameEN ELSE F.FTCustNameTH END AS FTCustName,"
            tSql &= Environment.NewLine & "       A.FNHSysAgencyId, G.FTAgencyCode, CASE WHEN @FTLang = N'EN' THEN G.FTAgencyNameEN ELSE G.FTAgencyNameTH END AS FTAgencyName,"
            tSql &= Environment.NewLine & "       A.FNHSysProdTypeId,"
            tSql &= Environment.NewLine & "       H.FTProdTypeCode, CASE WHEN @FTLang = N'EN' THEN H.FTProdTypeNameEN ELSE H.FTProdTypeNameTH END AS FTProdTypeName,"
            tSql &= Environment.NewLine & "       A.FNHSysBrandId,"
            tSql &= Environment.NewLine & "       I.FTBrandCode, CASE WHEN @FTLang = N'EN' THEN I.FTBrandNameEN ELSE I.FTBrandNameTH END AS FTBrandName,"
            tSql &= Environment.NewLine & "       A.FNHSysBuyerId, J.FTBuyerCode, CASE WHEN @FTLang = N'EN' THEN J.FTBuyerNameEN ELSE J.FTBuyerNameTH END AS FTBuyerName,"
            tSql &= Environment.NewLine & "       A.FNHSysBuyId, K.FTBuyCode, CASE WHEN @FTLang = N'EN' THEN K.FTBuyNameEN ELSE K.FTBuyNameTH END AS FTBuyName,"
            tSql &= Environment.NewLine & "       A.FNHSysPlantId, L.FTPlantCode, CASE WHEN @FTLang = N'EN' THEN L.FTPlantNameEN ELSE L.FTPlantNameTH END AS FTPlantName,"
            tSql &= Environment.NewLine & "       A.FNHSysBuyGrpId, M.FTBuyGrpCode, CASE WHEN @FTLang = N'EN' THEN M.FTBuyGrpNameEN ELSE M.FTBuyGrpNameTH END AS FTBuyGrpName,"
            tSql &= Environment.NewLine & "       A.FNHSysMerTeamId, N.FTMerTeamCode, CASE WHEN @FTLang = N'EN' THEN N.FTMerTeamNameEN ELSE N.FTMerTeamNameTH END AS FTMerTeamName,"
            tSql &= Environment.NewLine & "       A.FNHSysVenderPramId, O.FTVenderPramCode, CASE WHEN @FTLang = N'EN' THEN O.FTVenderPramNameEN ELSE O.FTVenderPramNameTH END AS FTVenderPramName"
            tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrder] AS A WITH(NOLOCK) LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCNMCmp] AS B ON A.FNHSysCmpId = B.FNHSysCmpId"
            tSql &= Environment.NewLine & "     INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCNMCmpRun] AS C WITH(NOLOCK) ON A.FNHSysCmpRunId = C.FNHSysCmpRunId"
            tSql &= Environment.NewLine & "     INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMStyle] AS D WITH(NOLOCK) ON A.FNHSysStyleId = D.FNHSysStyleId"
            tSql &= Environment.NewLine & "     INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SYSTEM) & "]..[HSysListData] AS E WITH(NOLOCK) ON A.FNOrderType = E.FNListIndex"
            tSql &= Environment.NewLine & "     INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCNMCustomer] AS F WITH(NOLOCK) ON A.FNHSysCustId = F.FNHSysCustId"
            tSql &= Environment.NewLine & "     LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCNMAgency] AS G WITH(NOLOCK) ON A.FNHSysAgencyId = G.FNHSysAgencyId"
            tSql &= Environment.NewLine & "     INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMProductType] AS H WITH(NOLOCK) ON A.FNHSysProdTypeId = H.FNHSysProdTypeId"
            tSql &= Environment.NewLine & "     LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMBrand] AS I WITH(NOLOCK) ON A.FNHSysBrandId = I.FNHSysBrandId"
            tSql &= Environment.NewLine & "     LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMBuyer] AS J WITH(NOLOCK) ON A.FNHSysBuyerId = J.FNHSysBuyerId"
            tSql &= Environment.NewLine & "     INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMBuy] AS K WITH(NOLOCK) ON A.FNHSysBuyId = K.FNHSysBuyId"
            tSql &= Environment.NewLine & "     LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMPlant] AS L WITH(NOLOCK) ON A.FNHSysPlantId = L.FNHSysPlantId"
            tSql &= Environment.NewLine & "     LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMBuyGrp] AS M WITH(NOLOCK) ON A.FNHSysBuyGrpId = M.FNHSysBuyGrpId"
            tSql &= Environment.NewLine & "     INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMerTeam] AS N WITH(NOLOCK) ON A.FNHSysMerTeamId = N.FNHSysMerTeamId"
            tSql &= Environment.NewLine & "     INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMVenderPram] AS O WITH(NOLOCK) ON A.FNHSysVenderPramId = O.FNHSysVenderPramId"
            tSql &= Environment.NewLine & "WHERE E.FTListName = N'FNOrderType'"
            tSql &= Environment.NewLine & "--AND A.FTOrderCreateStatus = N'Y'"
            tSql &= Environment.NewLine & "--AND A.FTInsUser IS NULL"
            tSql &= Environment.NewLine & "--AND A.FTUpdUser IS NULL"
            tSql &= Environment.NewLine & "--AND A.FNHSysCmpId IS NULL;"
            tSql &= Environment.NewLine & "      AND (A.FTStateOrderApp IS NULL OR A.FTStateOrderApp = N'0')"

            oDBdtOrderNo = HI.Conn.SQLConn.GetDataTable(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN)

            '...Detail Sub Order No.
            tSql = ""
            tSql = "--DECLARE @FTOrderNo AS NVARCHAR(30);"
            tSql &= Environment.NewLine & "DECLARE @FTLang AS NVARCHAR(30);"
            tSql &= Environment.NewLine & "--SET @FTOrderNo = N'N140500010';"

            Select Case HI.ST.Lang.Language
                Case HI.ST.Lang.eLang.TH
                    tSql &= Environment.NewLine & "SET @FTLang = N'TH';"
                Case Else
                    tSql &= Environment.NewLine & "SET @FTLang = N'EN';"
            End Select

            tSql &= Environment.NewLine & "SELECT A.FTOrderNo, A.FTSubOrderNo, A.FDSubOrderDate, A.FDProDate, A.FDShipDate,"
            tSql &= Environment.NewLine & "       A.FNHSysContinentId, B.FTContinentCode, CASE WHEN @FTLang = N'EN' THEN B.FTContinentNameEN ELSE B.FTContinentNameTH END AS FTContinentName,"
            tSql &= Environment.NewLine & "       A.FNHSysCountryId, C.FTCountryCode, CASE WHEN @FTLang = N'EN' THEN C.FTCountryNameEN ELSE C.FTCountryNameTH END AS FTCountryName,"
            tSql &= Environment.NewLine & "       A.FNHSysProvinceId, D.FTProvinceCode, CASE WHEN @FTLang = N'EN' THEN D.FTProvinceNameEN ELSE D.FTProvinceNameTH END AS FTProvinceName,"
            tSql &= Environment.NewLine & "       A.FNHSysShipModeId, E.FTShipModeCode, CASE WHEN @FTLang = N'EN' THEN E.FTShipModeNameEN ELSE E.FTShipModenNameTH END AS FTShipModeName,"
            tSql &= Environment.NewLine & "       A.FNHSysShipPortId, F.FTShipPortCode, CASE WHEN @FTLang = N'EN' THEN F.FTShipPortNameEN ELSE F.FTShipPortNameTH END AS FTShipPortName,"
            tSql &= Environment.NewLine & "       A.FNHSysCurId, G.FTCurCode, CASE WHEN @FTLang = N'EN' THEN G.FTCurDescEN ELSE G.FTCurDescTH END AS FTCurDesc,"
            tSql &= Environment.NewLine & "       A.FNHSysGenderId, H.FTGenderCode, CASE WHEN @FTLang = N'EN' THEN H.FTGenderNameEN ELSE H.FTGenderNameTH END AS FTGenderName,"
            tSql &= Environment.NewLine & "       A.FNHSysUnitId, I.FTUnitCode, CASE WHEN @FTLang = N'EN' THEN I.FTUnitNameEN ELSE I.FTUnitNameTH END AS FTUnitName,"
            tSql &= Environment.NewLine & "       ISNULL(A.FTStateEmb, 0) AS FTStateEmb, ISNULL(A.FTStatePrint, 0) AS FTStatePrint, ISNULL(A.FTStateHeat, 0) AS FTStateHeat, ISNULL(A.FTStateLaser, 0) AS FTStateLaser, ISNULL(A.FTStateWindows, 0) AS FTStateWindows"
            tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrder] AS M1 WITH(NOLOCK) INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub] AS A WITH(NOLOCK) ON M1.FTOrderNo = A.FTOrderNo"
            tSql &= Environment.NewLine & "     LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCNMContinent] AS B WITH(NOLOCK) ON A.FNHSysContinentId = B.FNHSysContinentId"
            tSql &= Environment.NewLine & "     LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCNMCountry] AS C WITH(NOLOCK) ON A.FNHSysCountryId = C.FNHSysCountryId"
            tSql &= Environment.NewLine & "     LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCMMProvince] AS D WITH(NOLOCK) ON A.FNHSysProvinceId = D.FNHSysProvinceId"
            tSql &= Environment.NewLine & "     LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCNMShipMode] AS E WITH(NOLOCK) ON A.FNHSysShipModeId = E.FNHSysShipModeId"
            tSql &= Environment.NewLine & "     LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCNMShipPort] AS F WITH(NOLOCK) ON A.FNHSysShipPortId = F.FNHSysShipPortId"
            tSql &= Environment.NewLine & "     INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TFINMCurrency] AS G WITH(NOLOCK) ON A.FNHSysCurId = G.FNHSysCurId"
            tSql &= Environment.NewLine & "     LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMGender] AS H WITH(NOLOCK) ON A.FNHSysGenderId = H.FNHSysGenderId"
            tSql &= Environment.NewLine & "     LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCNMUnit] AS I WITH(NOLOCK) ON A.FNHSysUnitId = I.FNHSysUnitId"
            tSql &= Environment.NewLine & "--WHERE A.FTOrderNo = @FTOrderNo"
            tSql &= Environment.NewLine & "WHERE (M1.FTStateOrderApp IS NULL OR M1.FTStateOrderApp = '0')"
            tSql &= Environment.NewLine & "ORDER BY A.FTSubOrderNo ASC;"

            oDBdtSubOrderNo = HI.Conn.SQLConn.GetDataTable(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN)

            Me.ogdAdjustFONo.DataSource = oDBdtOrderNo
            Me.ogdAdjustFONo.Refresh()
            'Me.ogdAdjustImportOrder.OptionsView.ColumnAutoWidth = False

            'Dim ogvBandedView As BandedGridView = New BandedGridView(Me.ogdAdjustFONo)
            'Me.ogdAdjustFONo.MainView = Me.ogdAdjustFONo.Views(0) 'ogvBandedView
            ''Add one band and one column to the view 
            'Dim band As GridBand = ogvBandedView.Bands.AddBand("Factory Sub Order No.")
            'Dim column As BandedGridColumn = ogvBandedView.Columns.AddField("FTSubOrderNo")
            'column.OwnerBand = band
            'column.Visible = True

            '...banded view
            '---------------------------------------------------------------------------------------------
            Dim oGridBandedView As DevExpress.XtraGrid.Views.Grid.GridView = Me.ogdAdjustFONo.Views(0)
            oGridBandedView.ClearGrouping()
            oGridBandedView.ClearDocument()

            oGridBandedView.OptionsBehavior.AllowAddRows = Utils.DefaultBoolean.False
            oGridBandedView.OptionsBehavior.AllowDeleteRows = Utils.DefaultBoolean.False
            oGridBandedView.OptionsBehavior.AutoExpandAllGroups = True

            oGridBandedView.Columns("FTOrderNo").SortIndex = 0
            oGridBandedView.Columns("FTOrderNo").SortOrder = DevExpress.Data.ColumnSortOrder.Ascending

            ' Make the group footers always visible.
            oGridBandedView.GroupFooterShowMode = Views.Grid.GroupFooterShowMode.VisibleAlways
            oGridBandedView.Columns("FTOrderNo").Group()
            oGridBandedView.Columns("FTOrderNo").Caption = "Factory Order No."

            oGridBandedView.GroupFooterShowMode = Views.Grid.GroupFooterShowMode.VisibleIfExpanded
            oGridBandedView.ExpandAllGroups()
            oGridBandedView.OptionsView.ColumnAutoWidth = False
            oGridBandedView.RefreshData()
            '---------------------------------------------------------------------------------------------

            '...Level Tree
            'Me.ogdAdjustFONo.LevelTree.Nodes(0).RelationName = "Factory Sub Order No."
            'Me.ogdAdjustFONo.ShowOnlyPredefinedDetails = True
            'Me.ogdAdjustFONo.DataSource = oDBdtSubOrderNo

            _bRet = True


        Catch ex As Exception
            MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
        End Try

        Return _bRet

    End Function

    Private Function W_PRCbLoadFONo() As Boolean
        Dim bRetLoadFONo As Boolean = False

        Try
            'HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_MERCHAN)
            'HI.Conn.SQLConn.SqlConnectionOpen()
            'HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand

            'Dim oCmdOrderNo As New SqlCommand

            ''...กรองเฉพาะรายการที่เป็นรายการตรงกับรหัสทีมเมอร์แชนไดเซอร์ที่ตนเองสังกัดอยู่เท่านั้น และ พนง.ที่อยู่ภายในทีมเดียวกันนั้นสามารถที่จะทำการแก้ไขรายการทุกรายการได้ถ้าเป็นรายการของทีมนั้นๆ
            'oCmdOrderNo.Connection = HI.Conn.SQLConn.Cnn
            'oCmdOrderNo.CommandType = CommandType.StoredProcedure
            'oCmdOrderNo.CommandText = "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[SP_GET_LIST_FACTORY_ORDER_INFO_AFTER_IMPORT]"
            'oCmdOrderNo.Parameters.AddWithValue("@FTUserLogin", HI.ST.UserInfo.UserName)
            'oCmdOrderNo.Parameters.AddWithValue("@FTLang", HI.ST.Lang.Language.ToString())
            'oCmdOrderNo.Parameters.AddWithValue("@SysBuyId", Val(FNHSysBuyId.Properties.Tag.ToString))

            'Dim sqlDA As New SqlDataAdapter(oCmdOrderNo.CommandText, HI.Conn.SQLConn._ConnString)

            'sqlDA.SelectCommand = oCmdOrderNo

            oDBdtFactoryOrderNo = New System.Data.DataTable
            oDBdtFactoryOrderNo.TableName = _tTableFactoryOrderNo

            'sqlDA.Fill(oDBdtFactoryOrderNo)

            Dim cmdstring As String = " EXEC [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[SP_GET_LIST_FACTORY_ORDER_INFO_AFTER_IMPORT] @FTUserLogin='" & HI.ST.UserInfo.UserName & "',@FTLang='" & HI.ST.Lang.Language.ToString() & "',@SysBuyId=" & Val(FNHSysBuyId.Properties.Tag.ToString) & ""
            oDBdtFactoryOrderNo = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_MERCHAN)
            Me.ogvAdjustFONo.ActiveFilter.Clear()
            Me.ogvAdjustFONo.OptionsView.ShowGroupPanel = False

            Me.ogdAdjustFONo.DataSource = oDBdtFactoryOrderNo
            'Me.ogvAdjustFONo.OptionsView.ColumnAutoWidth = False
            Me.ogdAdjustFONo.Refresh()
            Me.ogvAdjustFONo.RefreshData()
            Me.ogvAdjustFONo.BestFitColumns()

            Me.ogvAdjustFONo.OptionsView.ColumnAutoWidth = False
            Me.ogvAdjustFONo.RefreshData()

            'Me.ogdAdjustFONo.ForceInitialize()

            ' HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

        Catch ex As Exception
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            If System.Diagnostics.Debugger.IsAttached = True Then
                MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
            End If
        End Try

        Return bRetLoadFONo

    End Function

    Private Function W_PRCbLoadFONoSub(Optional ByVal ptFilterCriteria As String = "") As Boolean
        Dim bRetLoadFONoSub As Boolean = False

        Try
            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_MERCHAN)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand

            Dim oCmdSubOrderNo As New SqlCommand

            oCmdSubOrderNo.Connection = HI.Conn.SQLConn.Cnn
            oCmdSubOrderNo.CommandType = CommandType.StoredProcedure
            oCmdSubOrderNo.CommandText = "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[SP_GET_LIST_FACTORY_SUBORDER_INFO_AFTER_IMPORT]"
            oCmdSubOrderNo.Parameters.AddWithValue("@FTUserLogin", HI.ST.UserInfo.UserName)
            oCmdSubOrderNo.Parameters.AddWithValue("@FTLang", HI.ST.Lang.Language.ToString())
            oCmdSubOrderNo.Parameters.AddWithValue("@SysBuyId", Val(FNHSysBuyId.Properties.Tag.ToString))

            If ptFilterCriteria <> "" Then
                oCmdSubOrderNo.Parameters.AddWithValue("@FTFilterOrderNo", ptFilterCriteria)
            End If

            Dim sqlDA_SubOrderNo As New SqlDataAdapter(oCmdSubOrderNo.CommandText, HI.Conn.SQLConn._ConnString)

            sqlDA_SubOrderNo.SelectCommand = oCmdSubOrderNo

            oDBdtFactorySubOrderNo = New System.Data.DataTable
            oDBdtFactorySubOrderNo.TableName = _tTableFactorySubOrderNo

            sqlDA_SubOrderNo.Fill(oDBdtFactorySubOrderNo)

            Me.ogvAdjustSubFONo.ActiveFilter.Clear()
            Me.ogvAdjustSubFONo.OptionsView.ShowGroupPanel = False

            Me.ogdAdjustSubFONo.DataSource = oDBdtFactorySubOrderNo
            'Me.ogvAdjustSubFONo.OptionsView.ColumnAutoWidth = False
            Me.ogdAdjustSubFONo.Refresh()
            Me.ogvAdjustSubFONo.RefreshData()
            Me.ogvAdjustSubFONo.BestFitColumns()

            Me.ogvAdjustSubFONo.OptionsView.ColumnAutoWidth = False
            Me.ogvAdjustSubFONo.RefreshData()

            'Me.ogdAdjustSubFONo.ForceInitialize()

            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

        Catch ex As Exception
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            If System.Diagnostics.Debugger.IsAttached = True Then
                MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
            End If
        End Try

        Return bRetLoadFONoSub

    End Function

    Private Function W_PRCbLoadFONoSubBreakdown(Optional ByVal ptFilterFONoCriteria As String = "", Optional ByVal ptFilterFONoSubCriteria As String = "") As Boolean

        _bLoadFONoBreakdown = True

        Dim bRetLoadFONoSubBreakdown As Boolean = False
        Try
            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_MERCHAN)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand

            Dim oCmdSubOrderNoBreakdown As New SqlCommand

            oCmdSubOrderNoBreakdown.Connection = HI.Conn.SQLConn.Cnn
            oCmdSubOrderNoBreakdown.CommandType = CommandType.StoredProcedure
            oCmdSubOrderNoBreakdown.CommandText = "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[SP_GET_LIST_FACTORY_SUBORDER_BREAKDOWN_INFO_AFTER_IMPORT]"
            oCmdSubOrderNoBreakdown.Parameters.AddWithValue("@FTUserLogin", HI.ST.UserInfo.UserName)
            oCmdSubOrderNoBreakdown.Parameters.AddWithValue("@FTLang", HI.ST.Lang.Language.ToString())
            oCmdSubOrderNoBreakdown.Parameters.AddWithValue("@SysBuyId", Val(FNHSysBuyId.Properties.Tag.ToString))


            If ptFilterFONoCriteria <> "" Then
                oCmdSubOrderNoBreakdown.Parameters.AddWithValue("@FTFilterOrderNo", ptFilterFONoCriteria)
            End If

            If ptFilterFONoSubCriteria <> "" Then
                oCmdSubOrderNoBreakdown.Parameters.AddWithValue("@FTFilterSubOrderNo", ptFilterFONoSubCriteria)
            End If

            Dim sqlDA_SubOrderNoBreakdown As New SqlDataAdapter(oCmdSubOrderNoBreakdown.CommandText, HI.Conn.SQLConn._ConnString)

            sqlDA_SubOrderNoBreakdown.SelectCommand = oCmdSubOrderNoBreakdown

            oDBdtFactorySubOrderNoBreakdown = New System.Data.DataTable
            oDBdtFactorySubOrderNoBreakdown.TableName = _tTableFactorySubOrderNoBreakdown

            sqlDA_SubOrderNoBreakdown.Fill(oDBdtFactorySubOrderNoBreakdown)

            Me.ogvAdjustSubFONoBreakdown.ActiveFilter.Clear()
            Me.ogvAdjustSubFONoBreakdown.OptionsView.ShowGroupPanel = False

            Me.ogdAdjustSubFONoBreakdown.DataSource = oDBdtFactorySubOrderNoBreakdown
            'Me.ogvAdjustSubFONoBreakdown.OptionsView.ColumnAutoWidth = False
            Me.ogdAdjustSubFONoBreakdown.Refresh()
            Me.ogvAdjustSubFONoBreakdown.RefreshData()
            Me.ogvAdjustSubFONoBreakdown.BestFitColumns()

            Me.ogvAdjustSubFONoBreakdown.OptionsView.ColumnAutoWidth = False
            Me.ogvAdjustSubFONoBreakdown.RefreshData()

            'Me.ogdAdjustSubFONoBreakdown.ForceInitialize()

            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

        Catch ex As Exception
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            If System.Diagnostics.Debugger.IsAttached = True Then
                MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
            End If
        End Try

        _bLoadFONoBreakdown = False

        Return bRetLoadFONoSubBreakdown

    End Function

    Private Function W_PRCnGETRowHandleByColumnValue(ByVal pGridView As DevExpress.XtraGrid.Views.Grid.GridView, ByVal ptColFieldName As String, ByVal pVal As Object) As Integer
        Dim nResult As Integer = DevExpress.XtraGrid.GridControl.InvalidRowHandle

        Dim nLoop As Integer
        For nLoop = 0 To pGridView.DataRowCount - 1
            If pGridView.GetDataRow(nLoop)(ptColFieldName).Equals(pVal) Then
                nResult = nLoop
                Exit For
            End If

        Next nLoop

        Return nResult

    End Function

    Private Function W_PRCbSaveAdjustFONo_REM_20140612_PM1537() As Boolean
        'If System.Diagnostics.Debugger.IsAttached = False Then Return False

        Dim bSaveData As Boolean = False

        Dim _Spls As New HI.TL.SplashScreen("Save data   Please Wait...")

        Try
            Dim oStrBuilder As New System.Text.StringBuilder()

            oStrBuilder.Remove(0, oStrBuilder.Length)

            For nLoopAdjustFONo As Integer = 0 To Me.ogvAdjustFONo.DataRowCount - 1
                'If Me.ogvAdjustFONo.GetRowCellValue(nLoopAdjustFONo, "ColumnName").ToString() = "A" Then
                '    ' Your code here
                'End If

                'For Each oColGrdViewAdjFONo As DevExpress.XtraGrid.Columns.GridColumn In DirectCast(Me.ogdAdjustFONo.Views(0), DevExpress.XtraGrid.Views.Grid.GridView).Columns
                '    With oColGrdViewAdjFONo

                '    End With
                'Next

                'Dim oDBdt As System.Data.DataTable = CType(ogdAdjustFONo.DataSource, System.Data.DataTable)

                'If Not oDBdt Is Nothing AndAlso oDBdt.Rows.Count > 0 Then
                '    For Each r As DataRow In oDBdt.Rows
                '        r!FTChk = IIf(Me.ockAll.EditValue = True, 1, 0)
                '    Next

                'End If

                'If CType(Me.ogvAdjustFONo.GetRowCellValue(nLoopAdjustFONo, oColFTChk), DevExpress.XtraEditors.CheckEdit).CheckState = CheckState.Checked Then
                '    MsgBox("Factory Order No. : " & Me.ogvAdjustFONo.GetRowCellValue(nLoopAdjustFONo, oColFTOrderNo_HD.FieldName.ToString()).ToString())
                'End If

                'If CType(Me.ogvAdjustFONo.GetRowCellValue(nLoopAdjustFONo, oColFTChk), DevExpress.XtraEditors.CheckEdit).CheckState = CheckState.Checked Then
                '    MsgBox("Factory Order No. : " & Me.ogvAdjustFONo.GetRowCellValue(nLoopAdjustFONo, oColFTOrderNo_HD.FieldName.ToString()).ToString())
                'End If

                'If Me.ogvAdjustFONo.GetRowCellValue(nLoopAdjustFONo, oColFTChk).ToString() = "1" Then
                '    MsgBox("Factory Order No. : " & Me.ogvAdjustFONo.GetRowCellValue(nLoopAdjustFONo, oColFTOrderNo_HD.FieldName.ToString()).ToString())
                'Else
                '    MsgBox("Factory Order No. Not Selected : " & Me.ogvAdjustFONo.GetRowCellValue(nLoopAdjustFONo, oColFTOrderNo_HD.FieldName.ToString()).ToString() & " !!!")
                'End If

            Next nLoopAdjustFONo

            For nLoopAdjustSubFONo As Integer = 0 To Me.ogvAdjustSubFONo.DataRowCount - 1
                '...look for FTOrderNo in gridview ogvAdjustFONo is column check is true '1'

                Dim tFTOrderNo As String, tFTSubOrderNo As String
                'tFTSubOrderNo = Me.ogvAdjustSubFONo.GetDataRow(nLoopAdjustSubFONo)("_oColFTSubOrderNo").Equals("N140600006")
                'oDataRow = Me.ogvAdjustSubFONo.GetDataRow(nLoopAdjustSubFONo)("_oColFTSubOrderNo").Equals("N140600006")

                'tFTOrderNo = Me.ogvAdjustSubFONo.GetRowCellValue(nLoopAdjustSubFONo, "FTOrderNo")
                'tFTSubOrderNo = Me.ogvAdjustSubFONo.GetRowCellValue(nLoopAdjustSubFONo, "FTSubOrderNo")

                'If tFTSubOrderNo = "N140600015-A" Or tFTSubOrderNo = "N140600016-A" Then

                '    Try
                '        Dim oDataRow As DataRow = Me.ogvAdjustSubFONo.GetDataRow(nLoopAdjustSubFONo)

                '        If Not oDataRow Is Nothing Then

                '            Dim nValidateFONoRow As Integer
                '            nValidateFONoRow = W_PRCnGETRowHandleByColumnValue(Me.ogvAdjustFONo, "FTOrderNo", tFTOrderNo)

                '            If nValidateFONoRow >= 0 Then
                '                Dim oFONoRow As DataRow = Me.ogvAdjustFONo.GetDataRow(nValidateFONoRow)
                '                If Not oFONoRow Is Nothing Then
                '                    If oFONoRow.Item("FTChk") = "1" Then
                '                        MsgBox("Factory Order No :" & tFTOrderNo & Environment.NewLine & "Factory Sub Order No. :" & tFTSubOrderNo & Environment.NewLine & "Gender Code : " & oDataRow.Item("FNHSysGenderId") & Environment.NewLine & "Gender Description : " & oDataRow.Item("FNHSysGenderId_None").ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
                '                    End If

                '                End If

                '            End If

                '        End If

                '    Catch ex As Exception
                '        Throw New Exception(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString())
                '    End Try

                'End If

                Try
                    Dim oDataRow As DataRow = Me.ogvAdjustSubFONo.GetDataRow(nLoopAdjustSubFONo)

                    If Not oDataRow Is Nothing Then

                        tFTOrderNo = Me.ogvAdjustSubFONo.GetRowCellValue(nLoopAdjustSubFONo, "FTOrderNo")
                        tFTSubOrderNo = Me.ogvAdjustSubFONo.GetRowCellValue(nLoopAdjustSubFONo, "FTSubOrderNo")

                        Dim nValidateFONoRow As Integer
                        nValidateFONoRow = W_PRCnGETRowHandleByColumnValue(Me.ogvAdjustFONo, "FTOrderNo", tFTOrderNo)
                        'nValidateFONoRow = W_PRCnGETRowHandleByColumnValue(Me.ogvAdjustFONo, "FTOrderNo", tFTSubOrderNo)

                        If nValidateFONoRow >= 0 Then
                            Dim oFONoRow As DataRow = Me.ogvAdjustFONo.GetDataRow(nValidateFONoRow)
                            If Not oFONoRow Is Nothing Then
                                If oFONoRow.Item("FTChk") = "1" Then
                                    MsgBox("Factory Order No :" & tFTOrderNo & Environment.NewLine & "Factory Sub Order No. :" & tFTSubOrderNo & Environment.NewLine & "Gender Code : " & oDataRow.Item("FNHSysGenderId") & Environment.NewLine & "Gender Description : " & oDataRow.Item("FNHSysGenderId_None").ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
                                End If

                            End If

                        End If

                    End If

                Catch ex As Exception
                    If System.Diagnostics.Debugger.IsAttached = True Then
                        Throw New Exception(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString())
                    End If
                End Try

            Next nLoopAdjustSubFONo

            Return False

            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_MERCHAN)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            tSql = ""
            tSql = oStrBuilder.ToString()

            If HI.Conn.SQLConn.Execute_Tran(tSql, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                bSaveData = False
            Else
                HI.Conn.SQLConn.Tran.Commit()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                bSaveData = True
            End If

        Catch ex As Exception
            MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
        End Try

        _Spls.Close()

        Return bSaveData

    End Function

    Private Function W_PRCBSaveAdjustFONoSubBreakdown() As Boolean
        Dim bSaveFONoSubBreakdown As Boolean = False

        Dim _Spls As HI.TL.SplashScreen

        If Not System.Diagnostics.Debugger.IsAttached = True Then
            _Spls = New HI.TL.SplashScreen("Save data  Please Wait...")
        End If

        Try
            Dim oStrBuilder As New System.Text.StringBuilder()
            Dim dtOrder As New DataTable
            dtOrder.Columns.Add("FTOrderNo", GetType(String))

            oStrBuilder.Remove(0, oStrBuilder.Length)

            Dim nFNHSysMatColorId As Integer, nFNHSysMatSizeId As Integer
            Dim nFNPrice As Double, nFNAmt As Double, nFNExtraQty As Double, nFNAmntExtra As Double, nFNGrandAmnt As Double, nFNGarmentQtyTest As Double, nFNGarmentQtyExTest As Double
            Dim nFNQuantity As Integer, nFNQuantityExtra As Integer, nFNGrandQuantity As Integer

            Dim tFTOrderNoBreakdownPrv As String, tFTSubOrderNoBreakdownPrv As String

            tFTOrderNoBreakdownPrv = Me.ogvAdjustSubFONoBreakdown.GetRowCellValue(0, "FTOrderNo").ToString()
            tFTSubOrderNoBreakdownPrv = Me.ogvAdjustSubFONoBreakdown.GetRowCellValue(0, "FTSubOrderNo").ToString()

            oStrBuilder.Remove(0, oStrBuilder.Length)

            oStrBuilder.AppendLine("DECLARE @FTUpdUser AS NVARCHAR(30);")
            oStrBuilder.AppendLine("DECLARE @FDUpdDate AS NVARCHAR(50);")
            oStrBuilder.AppendLine("DECLARE @FTUpdTime AS NVARCHAR(50);")
            oStrBuilder.AppendLine(String.Format("SET @FTUpdUser = N'{0}';", HI.ST.UserInfo.UserName))
            oStrBuilder.AppendLine("SELECT @FDUpdDate = CONVERT(VARCHAR(10),GETDATE(),111);")
            oStrBuilder.AppendLine("SELECT @FTUpdTime = CONVERT(VARCHAR(8),GETDATE(),114);")

            For nLoopAdjustBreakdown As Integer = 0 To Me.ogvAdjustSubFONoBreakdown.DataRowCount - 1

                Dim tFTOrderNoBreakdown As String, tFTSubOrderNoBreakdown As String

                ' If Val(Me.ogvAdjustSubFONoBreakdown.GetRowCellValue(nLoopAdjustBreakdown, "FNHSysBuyId_Hide").ToString()) = Val(FNHSysBuyId.Properties.Tag.ToString()) Then
                Try
                    Dim oDataRowBreakdown As DataRow = Me.ogvAdjustSubFONoBreakdown.GetDataRow(nLoopAdjustBreakdown)

                    If Not oDataRowBreakdown Is Nothing Then

                        tFTOrderNoBreakdown = Me.ogvAdjustSubFONoBreakdown.GetRowCellValue(nLoopAdjustBreakdown, "FTOrderNo").ToString()
                        tFTSubOrderNoBreakdown = Me.ogvAdjustSubFONoBreakdown.GetRowCellValue(nLoopAdjustBreakdown, "FTSubOrderNo").ToString()


                        If dtOrder.Select("FTOrderNo='" & HI.UL.ULF.rpQuoted(tFTOrderNoBreakdown) & "'").Length <= 0 Then
                            dtOrder.Rows.Add(tFTOrderNoBreakdown)
                        End If

                        If tFTOrderNoBreakdown <> tFTOrderNoBreakdownPrv And tFTSubOrderNoBreakdown <> tFTSubOrderNoBreakdownPrv Then

                            REM 2014/12/18
                            'oStrBuilder.AppendLine("UPDATE A")
                            'oStrBuilder.AppendLine("SET A.FNSubOrderQty = B.FNQuantity")
                            'oStrBuilder.AppendLine("   ,A.FNSubOrderAmt = B.FNAmt")
                            'oStrBuilder.AppendLine("   ,A.FNSubOrderExtraQty = B.FNSubOrderExtraQty")
                            'oStrBuilder.AppendLine("   ,A.FNSubOrderExtraAmt = B.FNSubOrderExtraAmt")
                            'oStrBuilder.AppendLine("   ,A.FTUpdUser =N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'")
                            'oStrBuilder.AppendLine(String.Format("   ,A.FDUpdDate = {0}", HI.UL.ULDate.FormatDateDB))
                            'oStrBuilder.AppendLine(String.Format("   ,A.FTUpdTime = {0}", HI.UL.ULDate.FormatTimeDB))
                            'oStrBuilder.AppendLine("FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.[TMERTOrderSub] AS A LEFT JOIN (SELECT M.FTOrderNo, M.FTSubOrderNo, ISNULL(SUM(M.FNQuantity),0) AS FNQuantity, ISNULL(SUM(M.FNAmt),0) AS FNAmt")
                            'oStrBuilder.AppendLine("                                                                                                                    , ISNULL(SUM(M.FNQuantityExtra),0) AS FNSubOrderExtraQty")
                            'oStrBuilder.AppendLine("                                                                                                                    , ISNULL(SUM(M.FNAmntExtra),0) AS FNSubOrderExtraAmt")
                            'oStrBuilder.AppendLine("                                                                                                                    , ISNULL(SUM(M.FNQuantityExtra),0) AS FNQuantityExtra")
                            'oStrBuilder.AppendLine("                                                                                                                    , ISNULL(SUM(M.FNAmntExtra),0) AS FNAmntExtra")
                            'oStrBuilder.AppendLine("                                                                                                               FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub_BreakDown] AS M WITH(NOLOCK)")
                            'oStrBuilder.AppendLine("													                                                           WHERE M.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(tFTOrderNoBreakdownPrv) & "'")
                            'oStrBuilder.AppendLine("															                                                         AND M.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(tFTSubOrderNoBreakdownPrv) & "'")
                            'oStrBuilder.AppendLine("													                                                           GROUP BY M.FTOrderNo, M.FTSubOrderNo) AS B ON A.FTOrderNo = B.FTOrderNo")
                            'oStrBuilder.AppendLine("WHERE A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(tFTOrderNoBreakdownPrv) & "'")
                            'oStrBuilder.AppendLine("      AND A.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(tFTSubOrderNoBreakdownPrv) & "'")
                            'oStrBuilder.AppendLine("      AND A.FTSubOrderNo = B.FTSubOrderNo;")

                            tFTOrderNoBreakdownPrv = tFTOrderNoBreakdown
                            tFTSubOrderNoBreakdownPrv = tFTSubOrderNoBreakdown

                        End If

                        nFNHSysMatColorId = Val(oDataRowBreakdown.Item("FNHSysMatColorId"))
                        nFNHSysMatSizeId = Val(oDataRowBreakdown.Item("FNHSysMatSizeId"))

                        nFNPrice = 0 : nFNAmt = 0 : nFNExtraQty = 0 : nFNAmntExtra = 0 : nFNGrandAmnt = 0
                        nFNQuantity = 0 : nFNQuantityExtra = 0 : nFNGrandQuantity = 0 : nFNGarmentQtyTest = 0

                        If Not DBNull.Value.Equals(oDataRowBreakdown.Item("FNQuantity")) Then
                            nFNQuantity = Val(oDataRowBreakdown.Item("FNQuantity"))
                        Else
                            nFNQuantity = 0
                        End If

                        If Not DBNull.Value.Equals(oDataRowBreakdown.Item("FNPrice")) Then
                            nFNPrice = Val(oDataRowBreakdown.Item("FNPrice"))
                        Else
                            nFNPrice = 0
                        End If

                        If Not DBNull.Value.Equals(oDataRowBreakdown.Item("FNAmt")) Then
                            nFNAmt = Val(oDataRowBreakdown.Item("FNAmt"))
                        Else
                            nFNAmt = 0
                        End If

                        If Not DBNull.Value.Equals(oDataRowBreakdown.Item("FNExtraQty")) Then
                            nFNExtraQty = Val(oDataRowBreakdown.Item("FNExtraQty"))
                        Else
                            nFNExtraQty = 0
                        End If

                        If Not DBNull.Value.Equals(oDataRowBreakdown.Item("FNQuantityExtra")) Then
                            nFNQuantityExtra = Val(oDataRowBreakdown.Item("FNQuantityExtra"))
                        Else
                            nFNQuantityExtra = 0
                        End If

                        If Not DBNull.Value.Equals(oDataRowBreakdown.Item("FNGrandQuantity")) Then
                            nFNGrandQuantity = Val(oDataRowBreakdown.Item("FNGrandQuantity"))
                        Else
                            nFNGrandQuantity = 0
                        End If

                        If Not DBNull.Value.Equals(oDataRowBreakdown.Item("FNAmntExtra")) Then
                            nFNAmntExtra = Val(oDataRowBreakdown.Item("FNAmntExtra"))
                        Else
                            nFNAmntExtra = 0
                        End If

                        If Not DBNull.Value.Equals(oDataRowBreakdown.Item("FNGrandAmnt")) Then
                            nFNGrandAmnt = Val(oDataRowBreakdown.Item("FNGrandAmnt"))
                        Else
                            nFNGrandAmnt = 0
                        End If

                        If Not DBNull.Value.Equals(oDataRowBreakdown.Item("FNGarmentQtyTest")) Then
                            nFNGarmentQtyTest = Val(oDataRowBreakdown.Item("FNGarmentQtyTest"))
                        Else
                            nFNGarmentQtyTest = 0
                        End If


                        If Not DBNull.Value.Equals(oDataRowBreakdown.Item("FNExternalQtyTest")) Then
                            nFNGarmentQtyExTest = Val(oDataRowBreakdown.Item("FNExternalQtyTest"))
                        Else
                            nFNGarmentQtyExTest = 0
                        End If

                        nFNGrandQuantity = nFNQuantity + nFNGarmentQtyTest + nFNGarmentQtyExTest + nFNQuantityExtra
                        nFNGrandAmnt = Double.Parse(Format(Val(nFNGrandQuantity) * Val(nFNPrice), "0.00"))

                        Dim _FNAmntQtyTest As Double = 0
                        Try
                            _FNAmntQtyTest = Double.Parse(Format(Val(nFNGarmentQtyTest) * Val(nFNPrice), "0.00"))
                        Catch ex As Exception
                        End Try



                        Dim _FNAmntQtyExTest As Double = 0
                        Try
                            _FNAmntQtyExTest = Double.Parse(Format(Val(nFNGarmentQtyExTest) * Val(nFNPrice), "0.00"))
                        Catch ex As Exception
                        End Try


                        Dim _CMDisPer As Double = GetCMDisPer(tFTOrderNoBreakdown)
                        oStrBuilder.AppendLine("UPDATE A")
                        oStrBuilder.AppendLine("SET A.FTUpdUser = @FTUpdUser,")
                        oStrBuilder.AppendLine("    A.FDUpdDate = @FDUpdDate,")
                        oStrBuilder.AppendLine("    A.FTUpdTime = @FTUpdTime,")
                        oStrBuilder.AppendLine(String.Format("    A.FNPrice = {0},", nFNPrice))
                        oStrBuilder.AppendLine(String.Format("    A.FNQuantity = {0},", nFNQuantity))
                        oStrBuilder.AppendLine(String.Format("    A.FNAmt = {0},", nFNAmt))
                        oStrBuilder.AppendLine(String.Format("    A.FNExtraQty = {0},", nFNExtraQty))
                        oStrBuilder.AppendLine(String.Format("    A.FNQuantityExtra = {0},", nFNQuantityExtra))
                        oStrBuilder.AppendLine(String.Format("    A.FNAmntExtra = {0},", nFNAmntExtra))
                        oStrBuilder.AppendLine(String.Format("    A.FNGrandQuantity = {0},", nFNGrandQuantity))
                        oStrBuilder.AppendLine(String.Format("    A.FNGrandAmnt = {0}", nFNGrandAmnt))
                        oStrBuilder.AppendLine(String.Format("  ,  A.FNGarmentQtyTest = {0}", nFNGarmentQtyTest))
                        oStrBuilder.AppendLine(String.Format("  ,  A.FNAmntQtyTest = {0}", _FNAmntQtyTest))

                        oStrBuilder.AppendLine(String.Format("  ,  A.FNExternalQtyTest = {0}", nFNGarmentQtyExTest))
                        oStrBuilder.AppendLine(String.Format("  ,  A.FNExternalAmntQtyTest = {0}", _FNAmntQtyExTest))


                        oStrBuilder.AppendLine(String.Format("  ,  A.FNPriceOrg = {0}", nFNPrice))
                        oStrBuilder.AppendLine(String.Format("  ,  A.FNCMDisPer = {0}", _CMDisPer))
                        oStrBuilder.AppendLine(String.Format("  ,  A.FNCMDisAmt = {0}", (nFNPrice * _CMDisPer) / 100))
                        oStrBuilder.AppendLine("FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub_BreakDown] AS A")
                        oStrBuilder.AppendLine(String.Format("WHERE A.FTOrderNo = N'{0}'", tFTOrderNoBreakdown))
                        oStrBuilder.AppendLine(String.Format("      AND A.FTSubOrderNo = N'{0}'", tFTSubOrderNoBreakdown))
                        oStrBuilder.AppendLine(String.Format("      AND A.FNHSysMatColorId = {0}", nFNHSysMatColorId))
                        oStrBuilder.AppendLine(String.Format("      AND A.FNHSysMatSizeId = {0};", nFNHSysMatSizeId))

                        Call SaveCMPrice(tFTOrderNoBreakdown)
                    End If

                Catch ex As Exception
                        If System.Diagnostics.Debugger.IsAttached = True Then
                            Throw New Exception(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString())
                        End If
                    End Try
                ' End If



            Next nLoopAdjustBreakdown

            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(HI.Conn.DB.DataBaseName.DB_MERCHAN)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            REM 2011/12/18 drop field TMERTOrderSub
            'oStrBuilder.AppendLine("UPDATE A")
            'oStrBuilder.AppendLine("SET A.FNSubOrderQty = B.FNQuantity")
            'oStrBuilder.AppendLine("   ,A.FNSubOrderAmt = B.FNAmt")
            'oStrBuilder.AppendLine("   ,A.FNSubOrderExtraQty = B.FNSubOrderExtraQty")
            'oStrBuilder.AppendLine("   ,A.FNSubOrderExtraAmt = B.FNSubOrderExtraAmt")
            'oStrBuilder.AppendLine("   ,A.FTUpdUser =N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'")
            'oStrBuilder.AppendLine(String.Format("   ,A.FDUpdDate = {0}", HI.UL.ULDate.FormatDateDB))
            'oStrBuilder.AppendLine(String.Format("   ,A.FTUpdTime = {0}", HI.UL.ULDate.FormatTimeDB))
            'oStrBuilder.AppendLine("FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.[TMERTOrderSub] AS A LEFT JOIN (SELECT M.FTOrderNo, M.FTSubOrderNo, ISNULL(SUM(M.FNQuantity),0) AS FNQuantity, ISNULL(SUM(M.FNAmt),0) AS FNAmt")
            'oStrBuilder.AppendLine("                                                                                                                    , ISNULL(SUM(M.FNQuantityExtra),0) AS FNSubOrderExtraQty")
            'oStrBuilder.AppendLine("                                                                                                                    , ISNULL(SUM(M.FNAmntExtra),0) AS FNSubOrderExtraAmt")
            'oStrBuilder.AppendLine("                                                                                                                    , ISNULL(SUM(M.FNQuantityExtra),0) AS FNQuantityExtra")
            'oStrBuilder.AppendLine("                                                                                                                    , ISNULL(SUM(M.FNAmntExtra),0) AS FNAmntExtra")
            'oStrBuilder.AppendLine("                                                                                                               FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub_BreakDown] AS M WITH(NOLOCK)")
            'oStrBuilder.AppendLine("													                                                           WHERE M.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(tFTOrderNoBreakdownPrv) & "'")
            'oStrBuilder.AppendLine("															                                                         AND M.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(tFTSubOrderNoBreakdownPrv) & "'")
            'oStrBuilder.AppendLine("													                                                           GROUP BY M.FTOrderNo, M.FTSubOrderNo) AS B ON A.FTOrderNo = B.FTOrderNo")
            'oStrBuilder.AppendLine("WHERE A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(tFTOrderNoBreakdownPrv) & "'")
            'oStrBuilder.AppendLine("      AND A.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(tFTSubOrderNoBreakdownPrv) & "'")
            'oStrBuilder.AppendLine("      AND A.FTSubOrderNo = B.FTSubOrderNo;")

            tSql = ""
            tSql = oStrBuilder.ToString()

            If HI.Conn.SQLConn.Execute_Tran(tSql, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                If Not System.Diagnostics.Debugger.IsAttached = True Then
                    MsgBox("Step failed at update factory sub order no breakdown !!!", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
                End If

                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                bSaveFONoSubBreakdown = False
            Else
                HI.Conn.SQLConn.Tran.Commit()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)



                For Each R As DataRow In dtOrder.Rows

                    Dim Cmdstring As String = ""
                    Cmdstring = "EXEC [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.USP_GEN_BREAKDOWN_SHIPDESINATION '" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "'"
                    HI.Conn.SQLConn.ExecuteOnly(Cmdstring, Conn.DB.DataBaseName.DB_MERCHAN)
                Next


                bSaveFONoSubBreakdown = True
            End If

        Catch ex As Exception
            If System.Diagnostics.Debugger.IsAttached = True Then
                MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
            End If
        End Try

        If Not System.Diagnostics.Debugger.IsAttached = True Then
            _Spls.Close()
        End If

        Return bSaveFONoSubBreakdown

    End Function

    Private Function W_PRCbSaveAdjustFONoSub() As Boolean
        Dim bSaveFONoSub As Boolean = False

        Dim _Spls As HI.TL.SplashScreen

        If Not System.Diagnostics.Debugger.IsAttached = True Then
            _Spls = New HI.TL.SplashScreen("Save data  Please Wait...")
        End If
        Dim dtOrder As New DataTable
        dtOrder.Columns.Add("FTOrderNo", GetType(String))

        Try
            Dim tFDSubOrderDate As String, tFDProDate As String, tFDShipDate As String
            Dim nFNHSysContinentId As Integer, nFNHSysCountryId As Integer, nFNHSysProvinceId As Integer, nFNHSysShipModeId As Integer, nFNHSysShipPortId As Integer, nFNHSysCurId As Integer, nFNHSysGenderId As Integer, nFNHSysUnitId As Integer
            Dim tFTStateEmb As String, tFTStatePrint As String, tFTStateHeat As String, tFTStateLaser As String, tFTStateWindows As String

            Dim oStrBuilder As New System.Text.StringBuilder()

            oStrBuilder.Remove(0, oStrBuilder.Length)

            oStrBuilder.AppendLine("DECLARE @FTUpdUser AS NVARCHAR(30);")
            oStrBuilder.AppendLine("DECLARE @FDUpdDate AS NVARCHAR(50);")
            oStrBuilder.AppendLine("DECLARE @FTUpdTime AS NVARCHAR(50);")
            oStrBuilder.AppendLine(String.Format("SET @FTUpdUser = N'{0}';", HI.ST.UserInfo.UserName))
            oStrBuilder.AppendLine("SELECT @FDUpdDate = CONVERT(VARCHAR(10),GETDATE(),111);")
            oStrBuilder.AppendLine("SELECT @FTUpdTime = CONVERT(VARCHAR(8),GETDATE(),114);")

            For nLoopAdjustSubFONo As Integer = 0 To Me.ogvAdjustSubFONo.DataRowCount - 1

                Dim tFTOrderNoDT As String, tFTSubOrderNo As String

                ' If Val(Me.ogvAdjustSubFONo.GetRowCellValue(nLoopAdjustSubFONo, "FNHSysBuyId_Hide").ToString()) = Val(FNHSysBuyId.Properties.Tag.ToString()) Then
                Try
                        Dim oDataRow As DataRow = Me.ogvAdjustSubFONo.GetDataRow(nLoopAdjustSubFONo)

                        If Not oDataRow Is Nothing Then

                            tFTOrderNoDT = Me.ogvAdjustSubFONo.GetRowCellValue(nLoopAdjustSubFONo, "FTOrderNo").ToString()
                            tFTSubOrderNo = Me.ogvAdjustSubFONo.GetRowCellValue(nLoopAdjustSubFONo, "FTSubOrderNo").ToString()


                        If dtOrder.Select("FTOrderNo='" & HI.UL.ULF.rpQuoted(tFTOrderNoDT) & "'").Length <= 0 Then
                            dtOrder.Rows.Add(tFTOrderNoDT)
                        End If


                        tFDSubOrderDate = CStr(oDataRow.Item("FDSubOrderDate"))
                            tFDProDate = CStr(oDataRow.Item("FDProDate"))
                            tFDShipDate = CStr(oDataRow.Item("FDShipDate"))

                            If Not DBNull.Value.Equals(oDataRow.Item("FNHSysContinentId")) Then
                                nFNHSysContinentId = Val(oDataRow.Item("FNHSysContinentId"))
                            Else
                                nFNHSysContinentId = -1
                            End If

                            If Not DBNull.Value.Equals(oDataRow.Item("FNHSysCountryId")) Then
                                nFNHSysCountryId = Val(oDataRow.Item("FNHSysCountryId"))
                            Else
                                nFNHSysCountryId = -1
                            End If

                            If Not DBNull.Value.Equals(oDataRow.Item("FNHSysProvinceId_Hide")) Then
                                nFNHSysProvinceId = Val(oDataRow.Item("FNHSysProvinceId_Hide"))
                            Else
                                nFNHSysProvinceId = -1
                            End If

                            If Not DBNull.Value.Equals(oDataRow.Item("FNHSysShipModeId_Hide")) Then
                                nFNHSysShipModeId = Val(oDataRow.Item("FNHSysShipModeId_Hide"))
                            Else
                                nFNHSysShipModeId = 0
                            End If

                            If Not DBNull.Value.Equals(oDataRow.Item("FNHSysShipPortId_Hide")) Then
                                nFNHSysShipPortId = Val(oDataRow.Item("FNHSysShipPortId_Hide"))
                            Else
                                nFNHSysShipPortId = 0
                            End If

                            If Not DBNull.Value.Equals(oDataRow.Item("FNHSysCurId_Hide")) Then
                                nFNHSysCurId = Val(oDataRow.Item("FNHSysCurId_Hide"))
                            Else
                                nFNHSysCurId = 0
                            End If

                            If Not DBNull.Value.Equals(oDataRow.Item("FNHSysGenderId_Hide")) Then
                                nFNHSysGenderId = Val(oDataRow.Item("FNHSysGenderId_Hide"))
                            Else
                                nFNHSysGenderId = 0
                            End If

                            If Not DBNull.Value.Equals(oDataRow.Item("FNHSysUnitId_Hide")) Then
                                nFNHSysUnitId = Val(oDataRow.Item("FNHSysUnitId_Hide"))
                            Else
                                nFNHSysUnitId = 0
                            End If

                            tFTStateEmb = oDataRow.Item("FTStateEmb").ToString()
                            tFTStatePrint = oDataRow.Item("FTStatePrint").ToString()
                            tFTStateHeat = oDataRow.Item("FTStateHeat").ToString()
                            tFTStateLaser = oDataRow.Item("FTStateLaser").ToString()
                            tFTStateWindows = oDataRow.Item("FTStateWindows").ToString()

                            oStrBuilder.AppendLine("UPDATE A")
                            oStrBuilder.AppendLine("SET A.FTUpdUser = @FTUpdUser,")
                            oStrBuilder.AppendLine("    A.FDUpdDate = @FDUpdDate,")
                            oStrBuilder.AppendLine("    A.FTUpdTime = @FTUpdTime,")
                            oStrBuilder.AppendLine(String.Format("    A.FDSubOrderDate = '{0}',", HI.UL.ULDate.ConvertEnDB(tFDSubOrderDate)))
                            oStrBuilder.AppendLine(String.Format("    A.FDProDate = '{0}',", HI.UL.ULDate.ConvertEnDB(tFDProDate)))
                            oStrBuilder.AppendLine(String.Format("    A.FDShipDate = '{0}',", HI.UL.ULDate.ConvertEnDB(tFDShipDate)))
                            oStrBuilder.AppendLine(String.Format("    A.FNHSysContinentId = {0},", IIf(nFNHSysContinentId = 0, "NULL", nFNHSysContinentId)))
                            oStrBuilder.AppendLine(String.Format("    A.FNHSysCountryId = {0},", IIf(nFNHSysCountryId = 0, "NULL", nFNHSysCountryId)))
                            oStrBuilder.AppendLine(String.Format("    A.FNHSysProvinceId = {0},", IIf(nFNHSysProvinceId = 0, "NULL", nFNHSysProvinceId)))
                            oStrBuilder.AppendLine(String.Format("    A.FNHSysShipModeId = {0},", IIf(nFNHSysShipModeId = 0, "NULL", nFNHSysShipModeId)))
                            oStrBuilder.AppendLine(String.Format("    A.FNHSysShipPortId = {0},", IIf(nFNHSysShipPortId = 0, "NULL", nFNHSysShipPortId)))
                            oStrBuilder.AppendLine(String.Format("    A.FNHSysCurId = {0},", IIf(nFNHSysCurId = 0, "NULL", nFNHSysCurId)))
                            oStrBuilder.AppendLine(String.Format("    A.FNHSysGenderId = {0},", IIf(nFNHSysGenderId = 0, "NULL", nFNHSysGenderId)))
                            oStrBuilder.AppendLine(String.Format("    A.FNHSysUnitId = {0},", IIf(nFNHSysUnitId = 0, "NULL", nFNHSysUnitId)))
                            oStrBuilder.AppendLine(String.Format("    A.FTStateEmb = N'{0}',", tFTStateEmb))
                            oStrBuilder.AppendLine(String.Format("    A.FTStatePrint = N'{0}',", tFTStatePrint))
                            oStrBuilder.AppendLine(String.Format("    A.FTStateHeat = N'{0}',", tFTStateHeat))
                            oStrBuilder.AppendLine(String.Format("    A.FTStateLaser = N'{0}',", tFTStateLaser))
                            oStrBuilder.AppendLine(String.Format("    A.FTStateWindows = N'{0}'", tFTStateWindows))
                            oStrBuilder.AppendLine("FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub] AS A")
                            oStrBuilder.AppendLine(String.Format("WHERE A.FTOrderNo = N'{0}'", HI.UL.ULF.rpQuoted(tFTOrderNoDT)))
                            oStrBuilder.AppendLine(String.Format("      AND A.FTSubOrderNo = N'{0}';", tFTSubOrderNo))

                            '...Syncronized Bom Sheet Mer Material Component
                            'oStrBuilder.AppendLine(String.Format("MERGE INTO [{0}]..[TMERTOrderSub_Component] AS target", HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN)))
                            'oStrBuilder.AppendLine("USING (SELECT A.FNHSysMerMatId, A.FTComponent, A.FTOrderNo, A.FTSubOrderNo")
                            'oStrBuilder.AppendLine(String.Format("       FROM [{0}]..[TMERTStyle_Mat] AS A WITH(NOLOCK)", HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN)))
                            'oStrBuilder.AppendLine("       WHERE A.FTStateActive = N'1'")
                            'oStrBuilder.AppendLine("             AND A.FTStateMainMaterial = N'1'")
                            'oStrBuilder.AppendLine(String.Format("             AND A.FNHSysStyleId = (SELECT TOP 1 L1.FNHSysStyleId FROM [{0}]..[TMERTOrder] AS L1 WITH(NOLOCK) WHERE L1.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(tFTOrderNoDT) & "')", HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN)))
                            'oStrBuilder.AppendLine("             AND ((A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(tFTOrderNoDT) & "' AND A.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(tFTSubOrderNo) & "') OR (A.FTOrderNo = N'-1' AND A.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(tFTSubOrderNo) & "')")
                            'oStrBuilder.AppendLine("                                                                                OR (A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(tFTOrderNoDT) & "' AND A.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(tFTSubOrderNo) & "')")
                            'oStrBuilder.AppendLine("                                                                                OR (A.FTOrderNo = N'-1' AND A.FTSubOrderNo = N'-1'))) AS source")
                            'oStrBuilder.AppendLine("ON (target.FNHSysMerMatId = source.FNHSysMerMatId)")
                            'oStrBuilder.AppendLine(String.Format("   AND (target.FTOrderNo = N'{0}')", HI.UL.ULF.rpQuoted(tFTOrderNoDT)))
                            'oStrBuilder.AppendLine(String.Format("   AND (target.FTSubOrderNo = N'{0}')", HI.UL.ULF.rpQuoted(tFTSubOrderNo)))
                            'oStrBuilder.AppendLine("WHEN MATCHED AND (target.FNHSysMerMatId = source.FNHSysMerMatId) THEN")
                            'oStrBuilder.AppendLine("     UPDATE SET target.FTComponent  =  source.FTComponent,")
                            'oStrBuilder.AppendLine("                target.FTUpdUser = @FTUpdUser,")
                            'oStrBuilder.AppendLine("                target.FDUpdDate = @FDUpdDate,")
                            'oStrBuilder.AppendLine("                target.FTUpdTime = @FTUpdTime")
                            'oStrBuilder.AppendLine("WHEN NOT MATCHED BY target THEN")
                            'oStrBuilder.AppendLine("     INSERT ([FTInsUser],[FDInsDate],[FTInsTime],FTUpdUser, FDUpdDate, FTUpdTime,")
                            'oStrBuilder.AppendLine("             [FTOrderNo],[FTSubOrderNo],[FNHSysMerMatId] ,[FTComponent],[FTRemark])")
                            'oStrBuilder.AppendLine("     VALUES (@FTUpdUser, @FDUpdDate, @FTUpdTime,")
                            'oStrBuilder.AppendLine("             NULL, NULL, NULL, N'" & HI.UL.ULF.rpQuoted(tFTOrderNoDT) & "', N'" & HI.UL.ULF.rpQuoted(tFTSubOrderNo) & "', FNHSysMerMatId, FTComponent, NULL)")
                            'oStrBuilder.AppendLine("WHEN NOT MATCHED BY source AND (target.FTOrderNo  = N'" & HI.UL.ULF.rpQuoted(tFTOrderNoDT) & "') AND (target.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(tFTSubOrderNo) & "') THEN")
                            'oStrBuilder.AppendLine("     DELETE;")

                        End If

                    Catch ex As Exception
                        If System.Diagnostics.Debugger.IsAttached = True Then
                            Throw New Exception(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString())
                        End If
                    End Try
                'End If


            Next nLoopAdjustSubFONo

            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(HI.Conn.DB.DataBaseName.DB_MERCHAN)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            tSql = ""
            tSql = oStrBuilder.ToString()

            If HI.Conn.SQLConn.Execute_Tran(tSql, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                If Not System.Diagnostics.Debugger.IsAttached = True Then
                    MsgBox("Step failed at update factory sub order no !!!", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
                End If

                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                bSaveFONoSub = False
            Else
                'HI.Conn.SQLConn.Tran.Commit()
                'HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                'HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                bSaveFONoSub = True
            End If

            If bSaveFONoSub = True Then



                If Me.ogvAdjustSubFONoBreakdown.DataRowCount > 0 Then

                    Dim nFNHSysMatColorId As Integer, nFNHSysMatSizeId As Integer
                    Dim nFNPrice As Double, nFNAmt As Double, nFNExtraQty As Double, nFNAmntExtra As Double, nFNGrandAmnt As Double
                    Dim nFNQuantity As Integer, nFNQuantityExtra As Integer, nFNGrandQuantity As Integer
                    Dim nFNGarmentQtyTest As Double = 0

                    Dim tFTOrderNoBreakdownPrv As String, tFTSubOrderNoBreakdownPrv As String

                    tFTOrderNoBreakdownPrv = Me.ogvAdjustSubFONoBreakdown.GetRowCellValue(0, "FTOrderNo").ToString()
                    tFTSubOrderNoBreakdownPrv = Me.ogvAdjustSubFONoBreakdown.GetRowCellValue(0, "FTSubOrderNo").ToString()

                    oStrBuilder.Remove(0, oStrBuilder.Length)

                    oStrBuilder.AppendLine("DECLARE @FTUpdUser AS NVARCHAR(30);")
                    oStrBuilder.AppendLine("DECLARE @FDUpdDate AS NVARCHAR(50);")
                    oStrBuilder.AppendLine("DECLARE @FTUpdTime AS NVARCHAR(50);")
                    oStrBuilder.AppendLine(String.Format("SET @FTUpdUser = N'{0}';", HI.ST.UserInfo.UserName))
                    oStrBuilder.AppendLine("SELECT @FDUpdDate = CONVERT(VARCHAR(10),GETDATE(),111);")
                    oStrBuilder.AppendLine("SELECT @FTUpdTime = CONVERT(VARCHAR(8),GETDATE(),114);")

                    For nLoopAdjustBreakdown As Integer = 0 To Me.ogvAdjustSubFONoBreakdown.DataRowCount - 1

                        Dim tFTOrderNoBreakdown As String, tFTSubOrderNoBreakdown As String

                        Try
                            Dim oDataRowBreakdown As DataRow = Me.ogvAdjustSubFONoBreakdown.GetDataRow(nLoopAdjustBreakdown)

                            If Not oDataRowBreakdown Is Nothing Then

                                tFTOrderNoBreakdown = Me.ogvAdjustSubFONoBreakdown.GetRowCellValue(nLoopAdjustBreakdown, "FTOrderNo").ToString()
                                tFTSubOrderNoBreakdown = Me.ogvAdjustSubFONoBreakdown.GetRowCellValue(nLoopAdjustBreakdown, "FTSubOrderNo").ToString()

                                If tFTOrderNoBreakdown <> tFTOrderNoBreakdownPrv And tFTSubOrderNoBreakdown <> tFTSubOrderNoBreakdownPrv Then

                                    REM 2014/12/18 drop field Amount, Qty
                                    'oStrBuilder.AppendLine("UPDATE A")
                                    'oStrBuilder.AppendLine("SET A.FNSubOrderQty = B.FNQuantity")
                                    'oStrBuilder.AppendLine("   ,A.FNSubOrderAmt = B.FNAmt")
                                    'oStrBuilder.AppendLine("   ,A.FNSubOrderExtraQty = B.FNSubOrderExtraQty")
                                    'oStrBuilder.AppendLine("   ,A.FNSubOrderExtraAmt = B.FNSubOrderExtraAmt")
                                    'oStrBuilder.AppendLine("   ,A.FTUpdUser =N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'")
                                    'oStrBuilder.AppendLine(String.Format("   ,A.FDUpdDate = {0}", HI.UL.ULDate.FormatDateDB))
                                    'oStrBuilder.AppendLine(String.Format("   ,A.FTUpdTime = {0}", HI.UL.ULDate.FormatTimeDB))
                                    'oStrBuilder.AppendLine("FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.[TMERTOrderSub] AS A LEFT JOIN (SELECT M.FTOrderNo, M.FTSubOrderNo, ISNULL(SUM(M.FNQuantity),0) AS FNQuantity, ISNULL(SUM(M.FNAmt),0) AS FNAmt")
                                    'oStrBuilder.AppendLine("                                                                                                                    , ISNULL(SUM(M.FNQuantityExtra),0) AS FNSubOrderExtraQty")
                                    'oStrBuilder.AppendLine("                                                                                                                    , ISNULL(SUM(M.FNAmntExtra),0) AS FNSubOrderExtraAmt")
                                    'oStrBuilder.AppendLine("                                                                                                                    , ISNULL(SUM(M.FNQuantityExtra),0) AS FNQuantityExtra")
                                    'oStrBuilder.AppendLine("                                                                                                                    , ISNULL(SUM(M.FNAmntExtra),0) AS FNAmntExtra")
                                    'oStrBuilder.AppendLine("                                                                                                               FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub_BreakDown] AS M WITH(NOLOCK)")
                                    'oStrBuilder.AppendLine("													                                                           WHERE M.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(tFTOrderNoBreakdownPrv) & "'")
                                    'oStrBuilder.AppendLine("															                                                         AND M.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(tFTSubOrderNoBreakdownPrv) & "'")
                                    'oStrBuilder.AppendLine("													                                                           GROUP BY M.FTOrderNo, M.FTSubOrderNo) AS B ON A.FTOrderNo = B.FTOrderNo")
                                    'oStrBuilder.AppendLine("WHERE A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(tFTOrderNoBreakdownPrv) & "'")
                                    'oStrBuilder.AppendLine("      AND A.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(tFTSubOrderNoBreakdownPrv) & "'")
                                    'oStrBuilder.AppendLine("      AND A.FTSubOrderNo = B.FTSubOrderNo;")

                                    tFTOrderNoBreakdownPrv = tFTOrderNoBreakdown
                                    tFTSubOrderNoBreakdownPrv = tFTSubOrderNoBreakdown

                                End If

                                nFNHSysMatColorId = Val(oDataRowBreakdown.Item("FNHSysMatColorId"))
                                nFNHSysMatSizeId = Val(oDataRowBreakdown.Item("FNHSysMatSizeId"))

                                nFNPrice = 0 : nFNAmt = 0 : nFNExtraQty = 0 : nFNAmntExtra = 0 : nFNGrandAmnt = 0
                                nFNQuantity = 0 : nFNQuantityExtra = 0
                                nFNGarmentQtyTest = 0
                                If Not DBNull.Value.Equals(oDataRowBreakdown.Item("FNQuantity")) Then
                                    nFNQuantity = Val(oDataRowBreakdown.Item("FNQuantity"))
                                End If

                                If Not DBNull.Value.Equals(oDataRowBreakdown.Item("FNPrice")) Then
                                    nFNPrice = Val(oDataRowBreakdown.Item("FNPrice"))
                                End If

                                If Not DBNull.Value.Equals(oDataRowBreakdown.Item("FNAmt")) Then
                                    nFNAmt = Val(oDataRowBreakdown.Item("FNAmt"))
                                End If

                                If Not DBNull.Value.Equals(oDataRowBreakdown.Item("FNExtraQty")) Then
                                    nFNExtraQty = Val(oDataRowBreakdown.Item("FNExtraQty"))
                                End If

                                If Not DBNull.Value.Equals(oDataRowBreakdown.Item("FNQuantityExtra")) Then
                                    nFNQuantityExtra = Val(oDataRowBreakdown.Item("FNQuantityExtra"))
                                End If

                                If Not DBNull.Value.Equals(oDataRowBreakdown.Item("FNGrandQuantity")) Then
                                    nFNGrandQuantity = Val(oDataRowBreakdown.Item("FNGrandQuantity"))
                                End If

                                If Not DBNull.Value.Equals(oDataRowBreakdown.Item("FNAmntExtra")) Then
                                    nFNAmntExtra = Val(oDataRowBreakdown.Item("FNAmntExtra"))
                                End If

                                If Not DBNull.Value.Equals(oDataRowBreakdown.Item("FNGrandAmnt")) Then
                                    nFNGrandAmnt = Val(oDataRowBreakdown.Item("FNGrandAmnt"))
                                End If

                                If Not DBNull.Value.Equals(oDataRowBreakdown.Item("FNGarmentQtyTest")) Then
                                    nFNGarmentQtyTest = Val(oDataRowBreakdown.Item("FNGarmentQtyTest"))
                                Else
                                    nFNGarmentQtyTest = 0
                                End If

                                nFNGrandQuantity = nFNQuantity + nFNGarmentQtyTest + nFNQuantityExtra
                                nFNGrandAmnt = Double.Parse(Format(Val(nFNGrandQuantity) * Val(nFNPrice), "0.00"))

                                Dim _FNAmntQtyTest As Double = 0
                                Try
                                    _FNAmntQtyTest = Double.Parse(Format(Val(nFNGarmentQtyTest) * Val(nFNPrice), "0.00"))
                                Catch ex As Exception
                                End Try

                                Dim _CMDisPer As Double = GetCMDisPer(tFTOrderNoBreakdown)
                                oStrBuilder.AppendLine("UPDATE A")
                                oStrBuilder.AppendLine("SET A.FTUpdUser = @FTUpdUser,")
                                oStrBuilder.AppendLine("    A.FDUpdDate = @FDUpdDate,")
                                oStrBuilder.AppendLine("    A.FTUpdTime = @FTUpdTime,")
                                oStrBuilder.AppendLine(String.Format("    A.FNPrice = {0},", nFNPrice))
                                oStrBuilder.AppendLine(String.Format("    A.FNQuantity = {0},", nFNQuantity))
                                oStrBuilder.AppendLine(String.Format("    A.FNAmt = {0},", nFNAmt))
                                oStrBuilder.AppendLine(String.Format("    A.FNExtraQty = {0},", nFNExtraQty))
                                oStrBuilder.AppendLine(String.Format("    A.FNQuantityExtra = {0},", nFNQuantityExtra))
                                oStrBuilder.AppendLine(String.Format("    A.FNAmntExtra = {0},", nFNAmntExtra))
                                oStrBuilder.AppendLine(String.Format("    A.FNGrandQuantity = {0},", nFNGrandQuantity))
                                oStrBuilder.AppendLine(String.Format("    A.FNGrandAmnt = {0}", nFNGrandAmnt))
                                oStrBuilder.AppendLine(String.Format("  ,  A.FNGarmentQtyTest = {0}", nFNGarmentQtyTest))
                                oStrBuilder.AppendLine(String.Format("  ,  A.FNAmntQtyTest = {0}", _FNAmntQtyTest))
                                oStrBuilder.AppendLine(String.Format("  ,  A.FNPriceOrg = {0}", nFNPrice))
                                oStrBuilder.AppendLine(String.Format("  ,  A.FNCMDisPer = {0}", _CMDisPer))
                                oStrBuilder.AppendLine(String.Format("  ,  A.FNCMDisAmt = {0}", (nFNPrice * _CMDisPer) / 100))
                                oStrBuilder.AppendLine("FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub_BreakDown] AS A")
                                oStrBuilder.AppendLine(String.Format("WHERE A.FTOrderNo = N'{0}'", tFTOrderNoBreakdown))
                                oStrBuilder.AppendLine(String.Format("      AND A.FTSubOrderNo = N'{0}'", tFTSubOrderNoBreakdown))
                                oStrBuilder.AppendLine(String.Format("      AND A.FNHSysMatColorId = {0}", nFNHSysMatColorId))
                                oStrBuilder.AppendLine(String.Format("      AND A.FNHSysMatSizeId = {0};", nFNHSysMatSizeId))

                                Call SaveCMPrice(tFTOrderNoBreakdown)

                                'oStrBuilder.AppendLine("UPDATE A")
                                'oStrBuilder.AppendLine("SET A.FTUpdUser = @FTUpdUser,")
                                'oStrBuilder.AppendLine("    A.FDUpdDate = @FDUpdDate,")
                                'oStrBuilder.AppendLine("    A.FTUpdTime = @FTUpdTime,")
                                'oStrBuilder.AppendLine(String.Format("    A.FNPrice = {0},", nFNPrice))
                                'oStrBuilder.AppendLine(String.Format("    A.FNQuantity = {0},", nFNQuantity))
                                'oStrBuilder.AppendLine(String.Format("    A.FNExtraQty = {0},", nFNExtraQty))
                                'oStrBuilder.AppendLine(String.Format("    A.FNQuantityExtra = {0},", nFNQuantityExtra))
                                'oStrBuilder.AppendLine(String.Format("    A.FNAmntExtra = {0},", nFNQuantityExtra))
                                'oStrBuilder.AppendLine(String.Format("    A.FNGrandAmnt = {0}", nFNGrandAmnt))
                                'oStrBuilder.AppendLine("FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub_BreakDown] AS A")
                                'oStrBuilder.AppendLine(String.Format("WHERE A.FTOrderNo = N'{0}'", tFTOrderNoBreakdown))
                                'oStrBuilder.AppendLine(String.Format("      AND A.FTSubOrderNo = N'{0}'", tFTSubOrderNoBreakdown))
                                'oStrBuilder.AppendLine(String.Format("      AND A.FNHSysMatColorId = {0}", nFNHSysMatColorId))
                                'oStrBuilder.AppendLine(String.Format("      AND A.FNHSysMatSizeId = {0};", nFNHSysMatSizeId))

                            End If

                        Catch ex As Exception
                            If System.Diagnostics.Debugger.IsAttached = True Then
                                Throw New Exception(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString())
                            End If
                        End Try

                    Next nLoopAdjustBreakdown

                    'oStrBuilder.AppendLine("UPDATE A")
                    'oStrBuilder.AppendLine("SET A.FNSubOrderQty = B.FNQuantity")
                    'oStrBuilder.AppendLine("   ,A.FNSubOrderAmt = B.FNAmt")
                    'oStrBuilder.AppendLine("   ,A.FNSubOrderExtraQty = B.FNSubOrderExtraQty")
                    'oStrBuilder.AppendLine("   ,A.FNSubOrderExtraAmt = B.FNSubOrderExtraAmt")
                    'oStrBuilder.AppendLine("   ,A.FTUpdUser =N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'")
                    'oStrBuilder.AppendLine(String.Format("   ,A.FDUpdDate = {0}", HI.UL.ULDate.FormatDateDB))
                    'oStrBuilder.AppendLine(String.Format("   ,A.FTUpdTime = {0}", HI.UL.ULDate.FormatTimeDB))
                    'oStrBuilder.AppendLine("FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.[TMERTOrderSub] AS A LEFT JOIN (SELECT M.FTOrderNo, M.FTSubOrderNo, ISNULL(SUM(M.FNQuantity),0) AS FNQuantity, ISNULL(SUM(M.FNAmt),0) AS FNAmt")
                    'oStrBuilder.AppendLine("                                                                                                                    , ISNULL(SUM(M.FNQuantityExtra),0) AS FNSubOrderExtraQty")
                    'oStrBuilder.AppendLine("                                                                                                                    , ISNULL(SUM(M.FNAmntExtra),0) AS FNSubOrderExtraAmt")
                    'oStrBuilder.AppendLine("                                                                                                                    , ISNULL(SUM(M.FNQuantityExtra),0) AS FNQuantityExtra")
                    'oStrBuilder.AppendLine("                                                                                                                    , ISNULL(SUM(M.FNAmntExtra),0) AS FNAmntExtra")
                    'oStrBuilder.AppendLine("                                                                                                               FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub_BreakDown] AS M WITH(NOLOCK)")
                    'oStrBuilder.AppendLine("													                                                           WHERE M.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(tFTOrderNoBreakdownPrv) & "'")
                    'oStrBuilder.AppendLine("															                                                         AND M.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(tFTSubOrderNoBreakdownPrv) & "'")
                    'oStrBuilder.AppendLine("													                                                           GROUP BY M.FTOrderNo, M.FTSubOrderNo) AS B ON A.FTOrderNo = B.FTOrderNo")
                    'oStrBuilder.AppendLine("WHERE A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(tFTOrderNoBreakdownPrv) & "'")
                    'oStrBuilder.AppendLine("      AND A.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(tFTSubOrderNoBreakdownPrv) & "'")
                    'oStrBuilder.AppendLine("      AND A.FTSubOrderNo = B.FTSubOrderNo;")

                    tSql = ""
                    tSql = oStrBuilder.ToString()

                    If HI.Conn.SQLConn.Execute_Tran(tSql, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                        If Not System.Diagnostics.Debugger.IsAttached = True Then
                            MsgBox("Step failed at update factory sub order no breakdown !!!", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
                        End If

                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        bSaveFONoSub = False
                    Else
                        HI.Conn.SQLConn.Tran.Commit()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)



                        For Each R As DataRow In dtOrder.Rows

                            Dim Cmdstring As String = ""
                            Cmdstring = "EXEC [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.USP_GEN_BREAKDOWN_SHIPDESINATION '" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "'"
                            HI.Conn.SQLConn.ExecuteOnly(Cmdstring, Conn.DB.DataBaseName.DB_MERCHAN)
                        Next



                        bSaveFONoSub = True
                    End If

                End If

            End If

        Catch ex As Exception
            If System.Diagnostics.Debugger.IsAttached = True Then
                MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
            End If
        End Try

        If Not System.Diagnostics.Debugger.IsAttached = True Then
            _Spls.Close()
        End If

        Return bSaveFONoSub

    End Function

    Private Function W_PRCbSaveAdjustFONo_REM_20140627() As Boolean

        Dim bSaveData As Boolean = False
        Dim bPromtSaveFONo As Boolean = False
        Dim bPromtSaveFONoSub As Boolean = False
        Dim bPromtSaveFONoSubBreakdown As Boolean = False

        Dim _Spls As HI.TL.SplashScreen

        If Not System.Diagnostics.Debugger.IsAttached = True Then
            _Spls = New HI.TL.SplashScreen("Save data  Please Wait...")
        End If

        Try
            Dim tFTOrderNo As String
            Dim tFDOrderDate As String
            Dim nFNHSysBuyId As Integer, nFNHSysProdTypeId As Integer, nFNHSysBrandId As Integer, nFNHSysBuyerId As Integer

            Dim oStrBuilder As New System.Text.StringBuilder()

            oStrBuilder.Remove(0, oStrBuilder.Length)

            oStrBuilder.AppendLine("DECLARE @FNHSysCmpId AS INT;")
            oStrBuilder.AppendLine("DECLARE @FTInsUser   AS NVARCHAR(50), @FTUpdUser AS NVARCHAR(30), @FTOrderBy AS NVARCHAR(30);")
            oStrBuilder.AppendLine("DECLARE @FDInsDate   AS NVARCHAR(50), @FDUpdDate AS NVARCHAR(50);")
            oStrBuilder.AppendLine("DECLARE @FTInsTime   AS NVARCHAR(50), @FTUpdTime AS NVARCHAR(50);")
            oStrBuilder.AppendLine(String.Format("SET @FNHSysCmpId = {0};", Val(Me.FNHSysCmpId.Properties.Tag)))
            oStrBuilder.AppendLine(String.Format("SET @FTInsUser = N'{0}';", HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName)))
            oStrBuilder.AppendLine(String.Format("SET @FTUpdUser = N'{0}';", HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName)))
            oStrBuilder.AppendLine(String.Format("SET @FTOrderBy = N'{0}';", HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName)))
            oStrBuilder.AppendLine("SELECT @FDInsDate = CONVERT(VARCHAR(10),GETDATE(),111);")
            oStrBuilder.AppendLine("SELECT @FTInsTime = CONVERT(VARCHAR(8),GETDATE(),114);")
            oStrBuilder.AppendLine("SELECT @FDUpdDate = CONVERT(VARCHAR(10),GETDATE(),111);")
            oStrBuilder.AppendLine("SELECT @FTUpdTime = CONVERT(VARCHAR(8),GETDATE(),114);")

            For nLoopAdjustFONo As Integer = 0 To Me.ogvAdjustFONo.DataRowCount - 1

                Dim oFONoRow As DataRow = Me.ogvAdjustFONo.GetDataRow(nLoopAdjustFONo)

                If Not oFONoRow Is Nothing And oFONoRow.ItemArray.Count > 0 Then

                    If oFONoRow.Item("FTChk").ToString() = "1" Then

                        tFTOrderNo = ""
                        tFTOrderNo = oFONoRow.Item("FTOrderNo").ToString()

                        If Not tFTOrderNo Is Nothing And tFTOrderNo <> "" Then

                            nFNHSysBuyId = 0 : nFNHSysProdTypeId = 0 : nFNHSysBrandId = 0 : nFNHSysBuyerId = 0

                            tFDOrderDate = CStr(oFONoRow.Item("FDOrderDate"))

                            If Not DBNull.Value.Equals(oFONoRow.Item("FNHSysBuyId_Hide")) Then
                                nFNHSysBuyId = Val(oFONoRow.Item("FNHSysBuyId_Hide"))
                            End If

                            If Not DBNull.Value.Equals(oFONoRow.Item("FNHSysBuyerId_Hide")) Then
                                nFNHSysBuyerId = Val(oFONoRow.Item("FNHSysBuyerId_Hide"))
                            End If

                            If Not DBNull.Value.Equals(oFONoRow.Item("FNHSysProdTypeId_Hide")) Then
                                nFNHSysProdTypeId = Val(oFONoRow.Item("FNHSysProdTypeId_Hide"))
                            End If

                            If Not DBNull.Value.Equals(oFONoRow.Item("FNHSysBrandId_Hide")) Then
                                nFNHSysBrandId = Val(oFONoRow.Item("FNHSysBrandId_Hide"))
                            End If

                            oStrBuilder.AppendLine(String.Format("IF ((SELECT A.FNHSysCmpId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrder] AS A WITH(NOLOCK) WHERE A.FTOrderNo = N'{0}') IS NULL)", tFTOrderNo))
                            oStrBuilder.AppendLine("   BEGIN")
                            oStrBuilder.AppendLine("		UPDATE A")
                            oStrBuilder.AppendLine("		SET A.FTInsUser = @FTInsUser,")
                            oStrBuilder.AppendLine("			A.FDInsDate = @FDInsDate,")
                            oStrBuilder.AppendLine("			A.FTInsTime = @FTInsTime,")
                            oStrBuilder.AppendLine("            A.FTUpdUser = @FTUpdUser,")
                            oStrBuilder.AppendLine("            A.FDUpdDate = @FDUpdDate,")
                            oStrBuilder.AppendLine("            A.FTUpdTime = @FTUpdTime,")
                            oStrBuilder.AppendLine("            A.FTOrderBy = @FTOrderBy,")
                            oStrBuilder.AppendLine("			A.FNHSysCmpId = @FNHSysCmpId,")
                            oStrBuilder.AppendLine(String.Format("			A.FDOrderDate = N'{0}',", HI.UL.ULDate.ConvertEnDB(tFDOrderDate)))
                            oStrBuilder.AppendLine(String.Format("			A.FNHSysBuyId = {0},", IIf(nFNHSysBuyId = 0, "NULL", nFNHSysBuyId)))
                            oStrBuilder.AppendLine(String.Format("			A.FNHSysProdTypeId = {0},", IIf(nFNHSysProdTypeId = 0, "NULL", nFNHSysProdTypeId)))
                            oStrBuilder.AppendLine(String.Format("			A.FNHSysBrandId = {0},", IIf(nFNHSysBrandId = 0, "NULL", nFNHSysBrandId)))
                            oStrBuilder.AppendLine(String.Format("			A.FNHSysBuyerId = {0}", IIf(nFNHSysBuyerId = 0, "NULL", nFNHSysBuyerId)))
                            oStrBuilder.AppendLine("		FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrder] AS A")
                            oStrBuilder.AppendLine(String.Format("		WHERE A.FTOrderNo = N'{0}';", HI.UL.ULF.rpQuoted(tFTOrderNo)))
                            'oStrBuilder.AppendLine("		UPDATE A")
                            'oStrBuilder.AppendLine("		SET A.FTInsUser = @FTInsUser,")
                            'oStrBuilder.AppendLine("			A.FDInsDate = @FDInsDate,")
                            'oStrBuilder.AppendLine("			A.FTInsTime = @FTInsTime,")
                            'oStrBuilder.AppendLine("            A.FTUpdUser = @FTUpdUser,")
                            'oStrBuilder.AppendLine("            A.FDUpdDate = @FDUpdDate,")
                            'oStrBuilder.AppendLine("            A.FTUpdTime = @FTUpdTime")
                            'oStrBuilder.AppendLine("		FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrder_BreakDown] AS A")
                            'oStrBuilder.AppendLine(String.Format("		WHERE A.FTOrderNo = N'{0}';", HI.UL.ULF.rpQuoted(tFTOrderNo)))
                            oStrBuilder.AppendLine("		UPDATE A")
                            oStrBuilder.AppendLine("		SET A.FTInsUser = @FTInsUser,")
                            oStrBuilder.AppendLine("			A.FDInsDate = @FDInsDate,")
                            oStrBuilder.AppendLine("			A.FTInsTime = @FTInsTime,")
                            oStrBuilder.AppendLine("            A.FTUpdUser = @FTUpdUser,")
                            oStrBuilder.AppendLine("            A.FDUpdDate = @FDUpdDate,")
                            oStrBuilder.AppendLine("            A.FTUpdTime = @FTUpdTime")
                            oStrBuilder.AppendLine("		FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub] AS A")
                            oStrBuilder.AppendLine(String.Format("		WHERE A.FTOrderNo = N'{0}';", HI.UL.ULF.rpQuoted(tFTOrderNo)))
                            oStrBuilder.AppendLine("		UPDATE A")
                            oStrBuilder.AppendLine("		SET A.FTInsUser = @FTInsUser,")
                            oStrBuilder.AppendLine("			A.FDInsDate = @FDInsDate,")
                            oStrBuilder.AppendLine("			A.FTInsTime = @FTInsTime,")
                            oStrBuilder.AppendLine("            A.FTUpdUser = @FTUpdUser,")
                            oStrBuilder.AppendLine("            A.FDUpdDate = @FDUpdDate,")
                            oStrBuilder.AppendLine("            A.FTUpdTime = @FTUpdTime")
                            oStrBuilder.AppendLine("		FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub_BreakDown] AS A")
                            oStrBuilder.AppendLine(String.Format("		WHERE A.FTOrderNo = N'{0}';", HI.UL.ULF.rpQuoted(tFTOrderNo)))

                            '...modify 2014/06/19
                            '-------------------------------------------------------------------------------------------------------------------------
                            oStrBuilder.AppendLine(String.Format("EXEC SP_INSERT_MAINMATERIAL_FACTORY_ORDER N'{0}'", HI.UL.ULF.rpQuoted(tFTOrderNo)))
                            oStrBuilder.AppendLine(String.Format("EXEC SP_INSERT_COMBINATION_FACTORY_ORDER N'{0}'", HI.UL.ULF.rpQuoted(tFTOrderNo)))
                            '-------------------------------------------------------------------------------------------------------------------------

                            oStrBuilder.AppendLine("   END")
                            oStrBuilder.AppendLine("ELSE")
                            oStrBuilder.AppendLine("   BEGIN")
                            oStrBuilder.AppendLine("		UPDATE A")
                            oStrBuilder.AppendLine("		SET A.FTUpdUser = @FTUpdUser,")
                            oStrBuilder.AppendLine("			A.FDUpdDate = @FDUpdDate,")
                            oStrBuilder.AppendLine("			A.FTUpdTime = @FTUpdTime,")
                            oStrBuilder.AppendLine("			A.FNHSysCmpId = @FNHSysCmpId,")
                            oStrBuilder.AppendLine(String.Format("			A.FDOrderDate = N'{0}',", HI.UL.ULDate.ConvertEnDB(tFDOrderDate)))
                            oStrBuilder.AppendLine(String.Format("			A.FNHSysBuyId = {0},", nFNHSysBuyId))
                            oStrBuilder.AppendLine(String.Format("			A.FNHSysProdTypeId = {0},", nFNHSysProdTypeId))
                            oStrBuilder.AppendLine(String.Format("			A.FNHSysBrandId = {0},", nFNHSysBrandId))
                            oStrBuilder.AppendLine(String.Format("			A.FNHSysBuyerId = {0}", nFNHSysBuyerId))
                            oStrBuilder.AppendLine("		FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrder] AS A")
                            oStrBuilder.AppendLine(String.Format("		WHERE A.FTOrderNo = N'{0}';", HI.UL.ULF.rpQuoted(tFTOrderNo)))

                            '...modify 2014/06/19
                            '-------------------------------------------------------------------------------------------------------------------------
                            oStrBuilder.AppendLine(String.Format("EXEC SP_INSERT_MAINMATERIAL_FACTORY_ORDER N'{0}'", HI.UL.ULF.rpQuoted(tFTOrderNo)))
                            oStrBuilder.AppendLine(String.Format("EXEC SP_INSERT_COMBINATION_FACTORY_ORDER N'{0}'", HI.UL.ULF.rpQuoted(tFTOrderNo)))
                            '-------------------------------------------------------------------------------------------------------------------------

                            oStrBuilder.AppendLine("   END;")

                        End If

                    End If

                End If

            Next nLoopAdjustFONo

            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(HI.Conn.DB.DataBaseName.DB_MERCHAN)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            tSql = ""
            tSql = oStrBuilder.ToString()

            If HI.Conn.SQLConn.Execute_Tran(tSql, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                If Not System.Diagnostics.Debugger.IsAttached = True Then
                    MsgBox("Step failed at update factory order no !!!", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
                End If

                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                bSaveData = False

            Else

                'HI.Conn.SQLConn.Tran.Commit()
                'HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                'HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                bSaveData = True

            End If

            If bSaveData = True Then
                Dim tFDSubOrderDate As String, tFDProDate As String, tFDShipDate As String
                Dim nFNHSysContinentId As Integer, nFNHSysCountryId As Integer, nFNHSysProvinceId As Integer, nFNHSysShipModeId As Integer, nFNHSysShipPortId As Integer, nFNHSysCurId As Integer, nFNHSysGenderId As Integer, nFNHSysUnitId As Integer
                Dim tFTStateEmb As String, tFTStatePrint As String, tFTStateHeat As String, tFTStateLaser As String, tFTStateWindows As String

                oStrBuilder.Remove(0, oStrBuilder.Length)
                oStrBuilder.AppendLine("DECLARE @FTUpdUser AS NVARCHAR(30);")
                oStrBuilder.AppendLine("DECLARE @FDUpdDate AS NVARCHAR(50);")
                oStrBuilder.AppendLine("DECLARE @FTUpdTime AS NVARCHAR(50);")
                oStrBuilder.AppendLine(String.Format("SET @FTUpdUser = N'{0}';", HI.ST.UserInfo.UserName))
                oStrBuilder.AppendLine("SELECT @FDUpdDate = CONVERT(VARCHAR(10),GETDATE(),111);")
                oStrBuilder.AppendLine("SELECT @FTUpdTime = CONVERT(VARCHAR(8),GETDATE(),114);")

                For nLoopAdjustSubFONo As Integer = 0 To Me.ogvAdjustSubFONo.DataRowCount - 1

                    Dim tFTOrderNoDT As String, tFTSubOrderNo As String

                    Try
                        Dim oDataRow As DataRow = Me.ogvAdjustSubFONo.GetDataRow(nLoopAdjustSubFONo)

                        If Not oDataRow Is Nothing Then

                            tFTOrderNoDT = Me.ogvAdjustSubFONo.GetRowCellValue(nLoopAdjustSubFONo, "FTOrderNo").ToString()
                            tFTSubOrderNo = Me.ogvAdjustSubFONo.GetRowCellValue(nLoopAdjustSubFONo, "FTSubOrderNo").ToString()

                            tFDSubOrderDate = CStr(oDataRow.Item("FDSubOrderDate"))
                            tFDProDate = CStr(oDataRow.Item("FDProDate"))
                            tFDShipDate = CStr(oDataRow.Item("FDShipDate"))

                            If Not DBNull.Value.Equals(oDataRow.Item("FNHSysContinentId")) Then
                                nFNHSysContinentId = Val(oDataRow.Item("FNHSysContinentId"))
                            Else
                                nFNHSysCountryId = 0
                            End If

                            If Not DBNull.Value.Equals(oDataRow.Item("FNHSysCountryId")) Then
                                nFNHSysCountryId = Val(oDataRow.Item("FNHSysCountryId"))
                            Else
                                nFNHSysCountryId = 0
                            End If

                            If Not DBNull.Value.Equals(oDataRow.Item("FNHSysProvinceId_Hide")) Then
                                nFNHSysProvinceId = Val(oDataRow.Item("FNHSysProvinceId_Hide"))
                            Else
                                nFNHSysProvinceId = 0
                            End If

                            If Not DBNull.Value.Equals(oDataRow.Item("FNHSysShipModeId_Hide")) Then
                                nFNHSysShipModeId = Val(oDataRow.Item("FNHSysShipModeId_Hide"))
                            Else
                                nFNHSysShipModeId = 0
                            End If

                            If Not DBNull.Value.Equals(oDataRow.Item("FNHSysShipPortId_Hide")) Then
                                nFNHSysShipPortId = Val(oDataRow.Item("FNHSysShipPortId_Hide"))
                            Else
                                nFNHSysShipPortId = 0
                            End If

                            If Not DBNull.Value.Equals(oDataRow.Item("FNHSysCurId_Hide")) Then
                                nFNHSysCurId = Val(oDataRow.Item("FNHSysCurId_Hide"))
                            Else
                                nFNHSysCurId = 0
                            End If

                            If Not DBNull.Value.Equals(oDataRow.Item("FNHSysGenderId_Hide")) Then
                                nFNHSysGenderId = Val(oDataRow.Item("FNHSysGenderId_Hide"))
                            Else
                                nFNHSysGenderId = 0
                            End If

                            If Not DBNull.Value.Equals(oDataRow.Item("FNHSysUnitId_Hide")) Then
                                nFNHSysUnitId = Val(oDataRow.Item("FNHSysUnitId_Hide"))
                            Else
                                nFNHSysUnitId = 0
                            End If

                            tFTStateEmb = oDataRow.Item("FTStateEmb").ToString()
                            tFTStatePrint = oDataRow.Item("FTStatePrint").ToString()
                            tFTStateHeat = oDataRow.Item("FTStateHeat").ToString()
                            tFTStateLaser = oDataRow.Item("FTStateLaser").ToString()
                            tFTStateWindows = oDataRow.Item("FTStateWindows").ToString()

                            Dim nValidateFONoRow As Integer
                            nValidateFONoRow = W_PRCnGETRowHandleByColumnValue(Me.ogvAdjustFONo, "FTOrderNo", tFTOrderNoDT)
                            'nValidateFONoRow = W_PRCnGETRowHandleByColumnValue(Me.ogvAdjustFONo, "FTOrderNo", tFTSubOrderNo)

                            If nValidateFONoRow >= 0 Then

                                Dim oFONoRow As DataRow = Me.ogvAdjustFONo.GetDataRow(nValidateFONoRow)

                                If Not oFONoRow Is Nothing And oFONoRow.ItemArray.Count > 0 Then
                                    '...look for FTOrderNo in gridview ogvAdjustFONo is column check is true '1'
                                    If oFONoRow.Item("FTChk").ToString() = "1" Then

                                        oStrBuilder.AppendLine("UPDATE A")
                                        oStrBuilder.AppendLine("SET A.FTUpdUser = @FTUpdUser,")
                                        oStrBuilder.AppendLine("    A.FDUpdDate = @FDUpdDate,")
                                        oStrBuilder.AppendLine("    A.FTUpdTime = @FTUpdTime,")
                                        oStrBuilder.AppendLine(String.Format("    A.FDSubOrderDate = '{0}',", HI.UL.ULDate.ConvertEnDB(tFDSubOrderDate)))
                                        oStrBuilder.AppendLine(String.Format("    A.FDProDate = '{0}',", HI.UL.ULDate.ConvertEnDB(tFDProDate)))
                                        oStrBuilder.AppendLine(String.Format("    A.FDShipDate = '{0}',", HI.UL.ULDate.ConvertEnDB(tFDShipDate)))
                                        oStrBuilder.AppendLine(String.Format("    A.FNHSysContinentId = {0},", IIf(nFNHSysContinentId = 0, "NULL", nFNHSysContinentId)))
                                        oStrBuilder.AppendLine(String.Format("    A.FNHSysCountryId = {0},", IIf(nFNHSysCountryId = 0, "NULL", nFNHSysCountryId)))
                                        oStrBuilder.AppendLine(String.Format("    A.FNHSysProvinceId = {0},", IIf(nFNHSysProvinceId = 0, "NULL", nFNHSysProvinceId)))
                                        oStrBuilder.AppendLine(String.Format("    A.FNHSysShipModeId = {0},", IIf(nFNHSysShipModeId = 0, "NULL", nFNHSysShipModeId)))
                                        oStrBuilder.AppendLine(String.Format("    A.FNHSysShipPortId = {0},", IIf(nFNHSysShipPortId = 0, "NULL", nFNHSysShipPortId)))
                                        oStrBuilder.AppendLine(String.Format("    A.FNHSysCurId = {0},", IIf(nFNHSysCurId = 0, "NULL", nFNHSysCurId)))
                                        oStrBuilder.AppendLine(String.Format("    A.FNHSysGenderId = {0},", IIf(nFNHSysGenderId = 0, "NULL", nFNHSysGenderId)))
                                        oStrBuilder.AppendLine(String.Format("    A.FNHSysUnitId = {0},", IIf(nFNHSysUnitId = 0, "NULL", nFNHSysUnitId)))
                                        oStrBuilder.AppendLine(String.Format("    A.FTStateEmb = N'{0}',", tFTStateEmb))
                                        oStrBuilder.AppendLine(String.Format("    A.FTStatePrint = N'{0}',", tFTStatePrint))
                                        oStrBuilder.AppendLine(String.Format("    A.FTStateHeat = N'{0}',", tFTStateHeat))
                                        oStrBuilder.AppendLine(String.Format("    A.FTStateLaser = N'{0}',", tFTStateLaser))
                                        oStrBuilder.AppendLine(String.Format("    A.FTStateWindows = N'{0}'", tFTStateWindows))
                                        oStrBuilder.AppendLine("FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub] AS A")
                                        oStrBuilder.AppendLine(String.Format("WHERE A.FTOrderNo = N'{0}'", HI.UL.ULF.rpQuoted(tFTOrderNoDT)))
                                        oStrBuilder.AppendLine(String.Format("      AND A.FTSubOrderNo = N'{0}';", HI.UL.ULF.rpQuoted(tFTSubOrderNo)))

                                        '...Syncronized Bom Sheet Mer Material Component
                                        oStrBuilder.AppendLine(String.Format("MERGE INTO [{0}]..[TMERTOrderSub_Component] AS target", HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN)))
                                        'oStrBuilder.AppendLine("USING (SELECT A.FNHSysMerMatId, A.FTComponent, A.FTOrderNo, A.FTSubOrderNo")
                                        'oStrBuilder.AppendLine(String.Format("       FROM [{0}]..[TMERTStyle_Mat] AS A WITH(NOLOCK)", HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN)))
                                        'oStrBuilder.AppendLine("       WHERE A.FTStateActive = N'1'")
                                        'oStrBuilder.AppendLine("             AND A.FTStateMainMaterial = N'1'")
                                        'oStrBuilder.AppendLine(String.Format("             AND A.FNHSysStyleId = (SELECT TOP 1 L1.FNHSysStyleId FROM [{0}]..[TMERTOrder] AS L1 WITH(NOLOCK) WHERE L1.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(tFTOrderNoDT) & "')", HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN)))
                                        'oStrBuilder.AppendLine("             AND ((A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(tFTOrderNoDT) & "' AND A.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(tFTSubOrderNo) & "') OR (A.FTOrderNo = N'-1' AND A.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(tFTSubOrderNo) & "')")
                                        'oStrBuilder.AppendLine("                                                                                OR (A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(tFTOrderNoDT) & "' AND A.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(tFTSubOrderNo) & "')")
                                        'oStrBuilder.AppendLine("                                                                                OR (A.FTOrderNo = N'-1' AND A.FTSubOrderNo = N'-1'))) AS source")
                                        'oStrBuilder.AppendLine("ON (target.FNHSysMerMatId = source.FNHSysMerMatId)")
                                        'oStrBuilder.AppendLine(String.Format("   AND (target.FTOrderNo = N'{0}')", HI.UL.ULF.rpQuoted(tFTOrderNoDT)))
                                        'oStrBuilder.AppendLine(String.Format("   AND (target.FTSubOrderNo = N'{0}')", HI.UL.ULF.rpQuoted(tFTSubOrderNo)))
                                        'oStrBuilder.AppendLine("WHEN MATCHED AND (target.FNHSysMerMatId = source.FNHSysMerMatId) THEN")
                                        'oStrBuilder.AppendLine("     UPDATE SET target.FTComponent  =  source.FTComponent,")
                                        'oStrBuilder.AppendLine("                target.FTUpdUser = @FTUpdUser,")
                                        'oStrBuilder.AppendLine("                target.FDUpdDate = @FDUpdDate,")
                                        'oStrBuilder.AppendLine("                target.FTUpdTime = @FTUpdTime")
                                        'oStrBuilder.AppendLine("WHEN NOT MATCHED BY target THEN")
                                        'oStrBuilder.AppendLine("     INSERT ([FTInsUser],[FDInsDate],[FTInsTime],FTUpdUser, FDUpdDate, FTUpdTime,")
                                        'oStrBuilder.AppendLine("             [FTOrderNo],[FTSubOrderNo],[FNHSysMerMatId] ,[FTComponent],[FTRemark])")
                                        'oStrBuilder.AppendLine("     VALUES (@FTUpdUser, @FDUpdDate, @FTUpdTime,")
                                        'oStrBuilder.AppendLine("             NULL, NULL, NULL, N'" & HI.UL.ULF.rpQuoted(tFTOrderNoDT) & "', N'" & HI.UL.ULF.rpQuoted(tFTSubOrderNo) & "', FNHSysMerMatId, FTComponent, NULL)")
                                        'oStrBuilder.AppendLine("WHEN NOT MATCHED BY source AND (target.FTOrderNo  = N'" & HI.UL.ULF.rpQuoted(tFTOrderNoDT) & "') AND (target.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(tFTSubOrderNo) & "') THEN")
                                        'oStrBuilder.AppendLine("     DELETE;")

                                    End If

                                End If

                            End If

                        End If

                    Catch ex As Exception
                        If System.Diagnostics.Debugger.IsAttached = True Then
                            Throw New Exception(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString())
                        End If
                    End Try

                Next nLoopAdjustSubFONo

                tSql = ""
                tSql = oStrBuilder.ToString()

                If HI.Conn.SQLConn.Execute_Tran(tSql, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                    If Not System.Diagnostics.Debugger.IsAttached = True Then
                        MsgBox("Step failed at update factory sub order no !!!", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
                    End If

                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    bSaveData = False
                Else
                    'HI.Conn.SQLConn.Tran.Commit()
                    'HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    'HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    bSaveData = True
                End If

                If bSaveData = True Then
                    Dim nFNHSysMatColorId As Integer, nFNHSysMatSizeId As Integer
                    Dim nFNPrice As Double, nFNAmt As Double, nFNExtraQty As Double, nFNAmntExtra As Double, nFNGrandAmnt As Double
                    Dim nFNQuantity As Integer, nFNQuantityExtra As Integer, nFNGrandQuantity As Integer

                    oStrBuilder.Remove(0, oStrBuilder.Length)

                    oStrBuilder.AppendLine("DECLARE @FTUpdUser AS NVARCHAR(30);")
                    oStrBuilder.AppendLine("DECLARE @FDUpdDate AS NVARCHAR(50);")
                    oStrBuilder.AppendLine("DECLARE @FTUpdTime AS NVARCHAR(50);")
                    oStrBuilder.AppendLine(String.Format("SET @FTUpdUser = N'{0}';", HI.ST.UserInfo.UserName))
                    oStrBuilder.AppendLine("SELECT @FDUpdDate = CONVERT(VARCHAR(10),GETDATE(),111);")
                    oStrBuilder.AppendLine("SELECT @FTUpdTime = CONVERT(VARCHAR(8),GETDATE(),114);")

                    For nLoopAdjustBreakdown As Integer = 0 To Me.ogvAdjustSubFONoBreakdown.DataRowCount - 1
                        Dim tFTOrderNoBreakdown As String, tFTSubOrderNoBreakdown As String

                        Try
                            Dim oDataRowBreakdown As DataRow = Me.ogvAdjustSubFONoBreakdown.GetDataRow(nLoopAdjustBreakdown)

                            If Not oDataRowBreakdown Is Nothing Then

                                tFTOrderNoBreakdown = Me.ogvAdjustSubFONoBreakdown.GetRowCellValue(nLoopAdjustBreakdown, "FTOrderNo").ToString()
                                tFTSubOrderNoBreakdown = Me.ogvAdjustSubFONoBreakdown.GetRowCellValue(nLoopAdjustBreakdown, "FTSubOrderNo").ToString()

                                Dim nValidateFONoBreakdownRow As Integer
                                nValidateFONoBreakdownRow = W_PRCnGETRowHandleByColumnValue(Me.ogvAdjustFONo, "FTOrderNo", tFTOrderNoBreakdown)

                                If nValidateFONoBreakdownRow >= 0 Then

                                    Dim oFONoRowBreakdown As DataRow = Me.ogvAdjustFONo.GetDataRow(nValidateFONoBreakdownRow)

                                    If Not oFONoRowBreakdown Is Nothing And oFONoRowBreakdown.ItemArray.Count > 0 Then
                                        '...look for FTOrderNo in gridview ogvAdjustFONo is column check is true '1'
                                        If oFONoRowBreakdown.Item("FTChk").ToString() = "1" Then

                                            nFNHSysMatColorId = Val(oDataRowBreakdown.Item("FNHSysMatColorId"))
                                            nFNHSysMatSizeId = Val(oDataRowBreakdown.Item("FNHSysMatSizeId"))

                                            nFNPrice = 0 : nFNAmt = 0 : nFNExtraQty = 0 : nFNAmntExtra = 0 : nFNGrandAmnt = 0
                                            nFNQuantity = 0 : nFNQuantityExtra = 0

                                            If Not DBNull.Value.Equals(oDataRowBreakdown.Item("FNQuantity")) Then
                                                nFNQuantity = Val(oDataRowBreakdown.Item("FNQuantity"))
                                            End If

                                            If Not DBNull.Value.Equals(oDataRowBreakdown.Item("FNPrice")) Then
                                                nFNPrice = Val(oDataRowBreakdown.Item("FNPrice"))
                                            End If

                                            If Not DBNull.Value.Equals(oDataRowBreakdown.Item("FNAmt")) Then
                                                nFNAmt = Val(oDataRowBreakdown.Item("FNAmt"))
                                            End If

                                            If Not DBNull.Value.Equals(oDataRowBreakdown.Item("FNExtraQty")) Then
                                                nFNExtraQty = Val(oDataRowBreakdown.Item("FNExtraQty"))
                                            End If

                                            If Not DBNull.Value.Equals(oDataRowBreakdown.Item("FNQuantityExtra")) Then
                                                nFNQuantityExtra = Val(oDataRowBreakdown.Item("FNQuantityExtra"))
                                            End If

                                            If Not DBNull.Value.Equals(oDataRowBreakdown.Item("FNGrandQuantity")) Then
                                                nFNGrandQuantity = Val(oDataRowBreakdown.Item("FNGrandQuantity"))
                                            End If

                                            If Not DBNull.Value.Equals(oDataRowBreakdown.Item("FNAmntExtra")) Then
                                                nFNAmntExtra = Val(oDataRowBreakdown.Item("FNAmntExtra"))
                                            End If

                                            If Not DBNull.Value.Equals(oDataRowBreakdown.Item("FNGrandAmnt")) Then
                                                nFNGrandAmnt = Val(oDataRowBreakdown.Item("FNGrandAmnt"))
                                            End If

                                            oStrBuilder.AppendLine(" UPDATE A")
                                            oStrBuilder.AppendLine(" SET A.FTUpdUser = @FTUpdUser,")
                                            oStrBuilder.AppendLine("    A.FDUpdDate = @FDUpdDate,")
                                            oStrBuilder.AppendLine("    A.FTUpdTime = @FTUpdTime,")
                                            oStrBuilder.AppendLine(String.Format("    A.FNPrice = {0},", nFNPrice))
                                            oStrBuilder.AppendLine(String.Format("    A.FNQuantity = {0},", nFNQuantity))
                                            oStrBuilder.AppendLine(String.Format("    A.FNExtraQty = {0},", nFNExtraQty))
                                            oStrBuilder.AppendLine(String.Format("    A.FNQuantityExtra = {0},", nFNQuantityExtra))
                                            oStrBuilder.AppendLine(String.Format("    A.FNAmntExtra = {0},", nFNQuantityExtra))
                                            oStrBuilder.AppendLine(String.Format("    A.FNGrandAmnt = {0}", nFNGrandAmnt))
                                            oStrBuilder.AppendLine(" FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub_BreakDown] AS A")
                                            oStrBuilder.AppendLine(String.Format("WHERE A.FTOrderNo = N'{0}'", tFTOrderNoBreakdown))
                                            oStrBuilder.AppendLine(String.Format("      AND A.FTSubOrderNo = N'{0}'", tFTSubOrderNoBreakdown))
                                            oStrBuilder.AppendLine(String.Format("      AND A.FNHSysMatColorId = {0}", nFNHSysMatColorId))
                                            oStrBuilder.AppendLine(String.Format("      AND A.FNHSysMatSizeId = {0};", nFNHSysMatSizeId))

                                        End If

                                    End If

                                End If

                            End If

                        Catch ex As Exception
                            If System.Diagnostics.Debugger.IsAttached = True Then
                                Throw New Exception(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString())
                            End If
                        End Try

                    Next nLoopAdjustBreakdown

                    tSql = ""
                    tSql = oStrBuilder.ToString()

                    If HI.Conn.SQLConn.Execute_Tran(tSql, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                        If Not System.Diagnostics.Debugger.IsAttached = True Then
                            MsgBox("Step failed at update factory sub order no breakdown !!!", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
                        End If

                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        bSaveData = False
                    Else
                        HI.Conn.SQLConn.Tran.Commit()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        bSaveData = True
                    End If

                End If

            End If

        Catch ex As Exception
            If System.Diagnostics.Debugger.IsAttached = True Then
                MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
            End If
        End Try

        If Not System.Diagnostics.Debugger.IsAttached = True Then
            _Spls.Close()
        End If

        Return bSaveData

    End Function

    Private Function W_PRCbSaveAdjustFONo() As Boolean

        Dim bSaveData As Boolean = False
        Dim bPromtSaveFONo As Boolean = False
        Dim bPromtSaveFONoSub As Boolean = False
        Dim bPromtSaveFONoSubBreakdown As Boolean = False

        Dim _Spls As HI.TL.SplashScreen

        If Not System.Diagnostics.Debugger.IsAttached = True Then
            _Spls = New HI.TL.SplashScreen("Save data  Please Wait...")
        End If

        Try
            Dim tFTOrderNo As String
            Dim tFDOrderDate As String
            Dim nFNHSysBuyId As Integer, nFNHSysProdTypeId As Integer, nFNHSysBrandId As Integer, nFNHSysBuyerId As Integer
            Dim _FNHSysBuyGrpId As Integer = 0

            Dim oStrBuilder As New System.Text.StringBuilder()

            oStrBuilder.Remove(0, oStrBuilder.Length)

            REM 2014/06/27 oStrBuilder.AppendLine("DECLARE @FNHSysCmpId AS INT;")
            oStrBuilder.AppendLine("DECLARE @FTInsUser   AS NVARCHAR(50), @FTUpdUser AS NVARCHAR(30), @FTOrderBy AS NVARCHAR(30);")
            oStrBuilder.AppendLine("DECLARE @FDInsDate   AS NVARCHAR(50), @FDUpdDate AS NVARCHAR(50);")
            oStrBuilder.AppendLine("DECLARE @FTInsTime   AS NVARCHAR(50), @FTUpdTime AS NVARCHAR(50);")
            REM 2014/06/27 oStrBuilder.AppendLine(String.Format("SET @FNHSysCmpId = {0};", Val(Me.FNHSysCmpId.Properties.Tag)))
            oStrBuilder.AppendLine(String.Format("SET @FTInsUser = N'{0}';", HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName)))
            oStrBuilder.AppendLine(String.Format("SET @FTUpdUser = N'{0}';", HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName)))
            oStrBuilder.AppendLine(String.Format("SET @FTOrderBy = N'{0}';", HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName)))
            oStrBuilder.AppendLine("SELECT @FDInsDate = CONVERT(VARCHAR(10),GETDATE(),111);")
            oStrBuilder.AppendLine("SELECT @FTInsTime = CONVERT(VARCHAR(8),GETDATE(),114);")
            oStrBuilder.AppendLine("SELECT @FDUpdDate = CONVERT(VARCHAR(10),GETDATE(),111);")
            oStrBuilder.AppendLine("SELECT @FTUpdTime = CONVERT(VARCHAR(8),GETDATE(),114);")


            For Each oFONoRow As DataRow In CType(ogdAdjustFONo.DataSource, DataTable).Select("FTChk='1' AND FNHSysBuyId_Hide=" & Val(FNHSysBuyId.Properties.Tag.ToString()) & " ")
                tFTOrderNo = ""
                tFTOrderNo = oFONoRow.Item("FTOrderNo").ToString()

                If Not tFTOrderNo Is Nothing And tFTOrderNo <> "" Then

                    nFNHSysBuyId = 0 : nFNHSysProdTypeId = 0 : nFNHSysBrandId = 0 : nFNHSysBuyerId = 0
                    _FNHSysBuyGrpId = 0
                    _FNHSysBuyGrpId = Val(oFONoRow.Item("FNHSysBuyGrpId_Hide").ToString)

                    tFDOrderDate = CStr(oFONoRow.Item("FDOrderDate"))

                    If Not DBNull.Value.Equals(oFONoRow.Item("FNHSysBuyId_Hide")) Then
                        nFNHSysBuyId = Val(oFONoRow.Item("FNHSysBuyId_Hide"))
                    End If

                    If Not DBNull.Value.Equals(oFONoRow.Item("FNHSysBuyerId_Hide")) Then
                        nFNHSysBuyerId = Val(oFONoRow.Item("FNHSysBuyerId_Hide"))
                    End If

                    If Not DBNull.Value.Equals(oFONoRow.Item("FNHSysProdTypeId_Hide")) Then
                        nFNHSysProdTypeId = Val(oFONoRow.Item("FNHSysProdTypeId_Hide"))
                    End If

                    If Not DBNull.Value.Equals(oFONoRow.Item("FNHSysBrandId_Hide")) Then
                        nFNHSysBrandId = Val(oFONoRow.Item("FNHSysBrandId_Hide"))
                    End If

                    oStrBuilder.AppendLine(String.Format("IF ((SELECT A.FNHSysCmpId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrder] AS A WITH(NOLOCK) WHERE A.FTOrderNo = N'{0}') IS NULL)", tFTOrderNo))
                    oStrBuilder.AppendLine("   BEGIN")
                    oStrBuilder.AppendLine("		UPDATE A")
                    oStrBuilder.AppendLine("		SET A.FTInsUser = @FTInsUser,")
                    oStrBuilder.AppendLine("			A.FDInsDate = @FDInsDate,")
                    oStrBuilder.AppendLine("			A.FTInsTime = @FTInsTime,")
                    oStrBuilder.AppendLine("            A.FTUpdUser = @FTUpdUser,")
                    oStrBuilder.AppendLine("            A.FDUpdDate = @FDUpdDate,")
                    oStrBuilder.AppendLine("            A.FTUpdTime = @FTUpdTime,")
                    oStrBuilder.AppendLine("            A.FTOrderBy = @FTOrderBy,")
                    oStrBuilder.AppendLine(String.Format("			A.FNHSysBuyGrpId = {0},", _FNHSysBuyGrpId))
                    If oFONoRow.Item("FTChk").ToString() = "1" Then

                        If Val(Me.FNHSysCmpId.Properties.Tag) > 0 Then
                            oStrBuilder.AppendLine(String.Format("			A.FNHSysCmpId = {0},", Val(Me.FNHSysCmpId.Properties.Tag)))
                            oStrBuilder.AppendLine("            A.FNHSysCmpIdTo = ISNULL(Cmpc.FNHSysCmpPOId,0),")

                            'Else
                            '    oStrBuilder.AppendLine(String.Format("			A.FNHSysCmpId = {0},", "NULL"))
                        End If

                    End If

                    oStrBuilder.AppendLine(String.Format("			A.FDOrderDate = N'{0}',", HI.UL.ULDate.ConvertEnDB(tFDOrderDate)))
                    oStrBuilder.AppendLine(String.Format("			A.FNHSysBuyId = {0},", IIf(nFNHSysBuyId = 0, "NULL", nFNHSysBuyId)))
                    oStrBuilder.AppendLine(String.Format("			A.FNHSysProdTypeId = {0},", IIf(nFNHSysProdTypeId = 0, "NULL", nFNHSysProdTypeId)))
                    oStrBuilder.AppendLine(String.Format("			A.FNHSysBrandId = {0},", IIf(nFNHSysBrandId = 0, "NULL", nFNHSysBrandId)))
                    oStrBuilder.AppendLine(String.Format("			A.FNHSysBuyerId = {0}", IIf(nFNHSysBuyerId = 0, "NULL", nFNHSysBuyerId)))
                    oStrBuilder.AppendLine("		FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrder] AS A")


                    oStrBuilder.AppendLine("  outer apply (select top 1 Cmpc.FNHSysCmpPOId from [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCNMCmp] As Cmpc With(NOLOCK) where Cmpc.FNHSysCmpId = " & Val(Me.FNHSysCmpId.Properties.Tag) & " ) AS Cmpc ")



                    oStrBuilder.AppendLine(String.Format("		WHERE A.FTOrderNo = N'{0}';", HI.UL.ULF.rpQuoted(tFTOrderNo)))
                    'oStrBuilder.AppendLine("		UPDATE A")
                    'oStrBuilder.AppendLine("		SET A.FTInsUser = @FTInsUser,")
                    'oStrBuilder.AppendLine("			A.FDInsDate = @FDInsDate,")
                    'oStrBuilder.AppendLine("			A.FTInsTime = @FTInsTime,")
                    'oStrBuilder.AppendLine("            A.FTUpdUser = @FTUpdUser,")
                    'oStrBuilder.AppendLine("            A.FDUpdDate = @FDUpdDate,")
                    'oStrBuilder.AppendLine("            A.FTUpdTime = @FTUpdTime")
                    'oStrBuilder.AppendLine("		FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrder_BreakDown] AS A")
                    'oStrBuilder.AppendLine(String.Format("		WHERE A.FTOrderNo = N'{0}';", HI.UL.ULF.rpQuoted(tFTOrderNo)))
                    oStrBuilder.AppendLine("		UPDATE A")
                    oStrBuilder.AppendLine("		SET A.FTInsUser = @FTInsUser,")
                    oStrBuilder.AppendLine("			A.FDInsDate = @FDInsDate,")
                    oStrBuilder.AppendLine("			A.FTInsTime = @FTInsTime,")
                    oStrBuilder.AppendLine("            A.FTUpdUser = @FTUpdUser,")
                    oStrBuilder.AppendLine("            A.FDUpdDate = @FDUpdDate,")
                    oStrBuilder.AppendLine("            A.FTUpdTime = @FTUpdTime")
                    oStrBuilder.AppendLine("		FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub] AS A")
                    oStrBuilder.AppendLine(String.Format("		WHERE A.FTOrderNo = N'{0}';", HI.UL.ULF.rpQuoted(tFTOrderNo)))
                    oStrBuilder.AppendLine("		UPDATE A")
                    oStrBuilder.AppendLine("		SET A.FTInsUser = @FTInsUser,")
                    oStrBuilder.AppendLine("			A.FDInsDate = @FDInsDate,")
                    oStrBuilder.AppendLine("			A.FTInsTime = @FTInsTime,")
                    oStrBuilder.AppendLine("            A.FTUpdUser = @FTUpdUser,")
                    oStrBuilder.AppendLine("            A.FDUpdDate = @FDUpdDate,")
                    oStrBuilder.AppendLine("            A.FTUpdTime = @FTUpdTime")
                    oStrBuilder.AppendLine("		FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub_BreakDown] AS A")
                    oStrBuilder.AppendLine(String.Format("		WHERE A.FTOrderNo = N'{0}';", HI.UL.ULF.rpQuoted(tFTOrderNo)))

                    '...modify 2014/06/19
                    '-------------------------------------------------------------------------------------------------------------------------
                    oStrBuilder.AppendLine(String.Format("EXEC SP_INSERT_MAINMATERIAL_FACTORY_ORDER N'{0}'", HI.UL.ULF.rpQuoted(tFTOrderNo)))
                    oStrBuilder.AppendLine(String.Format("EXEC SP_INSERT_COMBINATION_FACTORY_ORDER N'{0}'", HI.UL.ULF.rpQuoted(tFTOrderNo)))
                    '-------------------------------------------------------------------------------------------------------------------------

                    oStrBuilder.AppendLine("   END")
                    oStrBuilder.AppendLine("ELSE")
                    oStrBuilder.AppendLine("   BEGIN")
                    oStrBuilder.AppendLine("		UPDATE A")
                    oStrBuilder.AppendLine("		SET A.FTUpdUser = @FTUpdUser,")
                    oStrBuilder.AppendLine("			A.FDUpdDate = @FDUpdDate,")
                    oStrBuilder.AppendLine("			A.FTUpdTime = @FTUpdTime,")
                    oStrBuilder.AppendLine("			A.FTOrderBy = CASE WHEN ISNULL(A.FTOrderBy,'') ='' THEN @FTOrderBy ELSE ISNULL(A.FTOrderBy,'') END,")
                    oStrBuilder.AppendLine(String.Format("			A.FNHSysBuyGrpId = {0},", _FNHSysBuyGrpId))
                    If oFONoRow.Item("FTChk").ToString() = "1" Then
                        If Val(Me.FNHSysCmpId.Properties.Tag) > 0 Then
                            oStrBuilder.AppendLine(String.Format("			A.FNHSysCmpId = {0},", Val(Me.FNHSysCmpId.Properties.Tag)))
                            oStrBuilder.AppendLine("            A.FNHSysCmpIdTo = ISNULL(Cmpc.FNHSysCmpPOId,0),")
                            'Else
                            '    oStrBuilder.AppendLine(String.Format("			A.FNHSysCmpId = {0},", "NULL"))
                        End If
                    End If

                    oStrBuilder.AppendLine(String.Format("			A.FDOrderDate = N'{0}',", HI.UL.ULDate.ConvertEnDB(tFDOrderDate)))
                    oStrBuilder.AppendLine(String.Format("			A.FNHSysBuyId = {0},", nFNHSysBuyId))
                    oStrBuilder.AppendLine(String.Format("			A.FNHSysProdTypeId = {0},", nFNHSysProdTypeId))
                    oStrBuilder.AppendLine(String.Format("			A.FNHSysBrandId = {0},", nFNHSysBrandId))
                    oStrBuilder.AppendLine(String.Format("			A.FNHSysBuyerId = {0}", nFNHSysBuyerId))
                    oStrBuilder.AppendLine("		FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrder] AS A")
                    oStrBuilder.AppendLine("  outer apply (select top 1 Cmpc.FNHSysCmpPOId from [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCNMCmp] As Cmpc With(NOLOCK) where Cmpc.FNHSysCmpId = " & Val(Me.FNHSysCmpId.Properties.Tag) & " ) AS Cmpc ")

                    oStrBuilder.AppendLine(String.Format("		WHERE A.FTOrderNo = N'{0}';", HI.UL.ULF.rpQuoted(tFTOrderNo)))

                    '...modify 2014/06/19
                    '-------------------------------------------------------------------------------------------------------------------------
                    oStrBuilder.AppendLine(String.Format("EXEC SP_INSERT_MAINMATERIAL_FACTORY_ORDER N'{0}'", HI.UL.ULF.rpQuoted(tFTOrderNo)))
                    oStrBuilder.AppendLine(String.Format("EXEC SP_INSERT_COMBINATION_FACTORY_ORDER N'{0}'", HI.UL.ULF.rpQuoted(tFTOrderNo)))
                    '-------------------------------------------------------------------------------------------------------------------------

                    oStrBuilder.AppendLine("   END;")

                End If
            Next



            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(HI.Conn.DB.DataBaseName.DB_MERCHAN)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            tSql = ""
            tSql = oStrBuilder.ToString()

            If HI.Conn.SQLConn.Execute_Tran(tSql, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                If Not System.Diagnostics.Debugger.IsAttached = True Then
                    MsgBox("Step failed at update factory order no !!!", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
                End If

                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                bSaveData = False

            Else

                'HI.Conn.SQLConn.Tran.Commit()
                'HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                'HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                bSaveData = True

            End If

            If bSaveData = True Then
                Dim tFDSubOrderDate As String, tFDProDate As String, tFDShipDate As String
                Dim nFNHSysContinentId As Integer, nFNHSysCountryId As Integer, nFNHSysProvinceId As Integer, nFNHSysShipModeId As Integer, nFNHSysShipPortId As Integer, nFNHSysCurId As Integer, nFNHSysGenderId As Integer, nFNHSysUnitId As Integer
                Dim tFTStateEmb As String, tFTStatePrint As String, tFTStateHeat As String, tFTStateLaser As String, tFTStateWindows As String

                oStrBuilder.Remove(0, oStrBuilder.Length)
                oStrBuilder.AppendLine("DECLARE @FTUpdUser AS NVARCHAR(30);")
                oStrBuilder.AppendLine("DECLARE @FDUpdDate AS NVARCHAR(50);")
                oStrBuilder.AppendLine("DECLARE @FTUpdTime AS NVARCHAR(50);")
                oStrBuilder.AppendLine(String.Format("SET @FTUpdUser = N'{0}';", HI.ST.UserInfo.UserName))
                oStrBuilder.AppendLine("SELECT @FDUpdDate = CONVERT(VARCHAR(10),GETDATE(),111);")
                oStrBuilder.AppendLine("SELECT @FTUpdTime = CONVERT(VARCHAR(8),GETDATE(),114);")

                For nLoopAdjustSubFONo As Integer = 0 To Me.ogvAdjustSubFONo.DataRowCount - 1

                    ' If Val(Me.ogvAdjustSubFONo.GetRowCellValue(nLoopAdjustSubFONo, "FNHSysBuyId_Hide").ToString()) = Val(FNHSysBuyId.Properties.Tag.ToString()) Then
                    Dim tFTOrderNoDT As String, tFTSubOrderNo As String

                        Try
                            Dim oDataRow As DataRow = Me.ogvAdjustSubFONo.GetDataRow(nLoopAdjustSubFONo)

                            If Not oDataRow Is Nothing Then

                                tFTOrderNoDT = Me.ogvAdjustSubFONo.GetRowCellValue(nLoopAdjustSubFONo, "FTOrderNo").ToString()
                                tFTSubOrderNo = Me.ogvAdjustSubFONo.GetRowCellValue(nLoopAdjustSubFONo, "FTSubOrderNo").ToString()

                                tFDSubOrderDate = CStr(oDataRow.Item("FDSubOrderDate"))
                                tFDProDate = CStr(oDataRow.Item("FDProDate"))
                                tFDShipDate = CStr(oDataRow.Item("FDShipDate"))

                                If Not DBNull.Value.Equals(oDataRow.Item("FNHSysContinentId")) Then
                                    nFNHSysContinentId = Val(oDataRow.Item("FNHSysContinentId"))
                                Else
                                    nFNHSysCountryId = 0
                                End If

                                If Not DBNull.Value.Equals(oDataRow.Item("FNHSysCountryId")) Then
                                    nFNHSysCountryId = Val(oDataRow.Item("FNHSysCountryId"))
                                Else
                                    nFNHSysCountryId = 0
                                End If

                                If Not DBNull.Value.Equals(oDataRow.Item("FNHSysProvinceId_Hide")) Then
                                    nFNHSysProvinceId = Val(oDataRow.Item("FNHSysProvinceId_Hide"))
                                Else
                                    nFNHSysProvinceId = 0
                                End If

                                If Not DBNull.Value.Equals(oDataRow.Item("FNHSysShipModeId_Hide")) Then
                                    nFNHSysShipModeId = Val(oDataRow.Item("FNHSysShipModeId_Hide"))
                                Else
                                    nFNHSysShipModeId = 0
                                End If

                                If Not DBNull.Value.Equals(oDataRow.Item("FNHSysShipPortId_Hide")) Then
                                    nFNHSysShipPortId = Val(oDataRow.Item("FNHSysShipPortId_Hide"))
                                Else
                                    nFNHSysShipPortId = 0
                                End If

                                If Not DBNull.Value.Equals(oDataRow.Item("FNHSysCurId_Hide")) Then
                                    nFNHSysCurId = Val(oDataRow.Item("FNHSysCurId_Hide"))
                                Else
                                    nFNHSysCurId = 0
                                End If

                                If Not DBNull.Value.Equals(oDataRow.Item("FNHSysGenderId_Hide")) Then
                                    nFNHSysGenderId = Val(oDataRow.Item("FNHSysGenderId_Hide"))
                                Else
                                    nFNHSysGenderId = 0
                                End If

                                If Not DBNull.Value.Equals(oDataRow.Item("FNHSysUnitId_Hide")) Then
                                    nFNHSysUnitId = Val(oDataRow.Item("FNHSysUnitId_Hide"))
                                Else
                                    nFNHSysUnitId = 0
                                End If

                                tFTStateEmb = oDataRow.Item("FTStateEmb").ToString()
                                tFTStatePrint = oDataRow.Item("FTStatePrint").ToString()
                                tFTStateHeat = oDataRow.Item("FTStateHeat").ToString()
                                tFTStateLaser = oDataRow.Item("FTStateLaser").ToString()
                                tFTStateWindows = oDataRow.Item("FTStateWindows").ToString()

                                Dim nValidateFONoRow As Integer
                                nValidateFONoRow = W_PRCnGETRowHandleByColumnValue(Me.ogvAdjustFONo, "FTOrderNo", tFTOrderNoDT)
                                'nValidateFONoRow = W_PRCnGETRowHandleByColumnValue(Me.ogvAdjustFONo, "FTOrderNo", tFTSubOrderNo)

                                If nValidateFONoRow >= 0 Then

                                    Dim oFONoRow As DataRow = Me.ogvAdjustFONo.GetDataRow(nValidateFONoRow)

                                    If Not oFONoRow Is Nothing And oFONoRow.ItemArray.Count > 0 Then
                                        oStrBuilder.AppendLine("UPDATE A")
                                        oStrBuilder.AppendLine("SET A.FTUpdUser = @FTUpdUser,")
                                        oStrBuilder.AppendLine("    A.FDUpdDate = @FDUpdDate,")
                                        oStrBuilder.AppendLine("    A.FTUpdTime = @FTUpdTime,")
                                        oStrBuilder.AppendLine(String.Format("    A.FDSubOrderDate = '{0}',", HI.UL.ULDate.ConvertEnDB(tFDSubOrderDate)))
                                        oStrBuilder.AppendLine(String.Format("    A.FDProDate = '{0}',", HI.UL.ULDate.ConvertEnDB(tFDProDate)))
                                        oStrBuilder.AppendLine(String.Format("    A.FDShipDate = '{0}',", HI.UL.ULDate.ConvertEnDB(tFDShipDate)))
                                        oStrBuilder.AppendLine(String.Format("    A.FNHSysContinentId = {0},", IIf(nFNHSysContinentId = 0, "NULL", nFNHSysContinentId)))
                                        oStrBuilder.AppendLine(String.Format("    A.FNHSysCountryId = {0},", IIf(nFNHSysCountryId = 0, "NULL", nFNHSysCountryId)))
                                        oStrBuilder.AppendLine(String.Format("    A.FNHSysProvinceId = {0},", IIf(nFNHSysProvinceId = 0, "NULL", nFNHSysProvinceId)))
                                        oStrBuilder.AppendLine(String.Format("    A.FNHSysShipModeId = {0},", IIf(nFNHSysShipModeId = 0, "NULL", nFNHSysShipModeId)))
                                        oStrBuilder.AppendLine(String.Format("    A.FNHSysShipPortId = {0},", IIf(nFNHSysShipPortId = 0, "NULL", nFNHSysShipPortId)))
                                        oStrBuilder.AppendLine(String.Format("    A.FNHSysCurId = {0},", IIf(nFNHSysCurId = 0, "NULL", nFNHSysCurId)))
                                        oStrBuilder.AppendLine(String.Format("    A.FNHSysGenderId = {0},", IIf(nFNHSysGenderId = 0, "NULL", nFNHSysGenderId)))
                                        oStrBuilder.AppendLine(String.Format("    A.FNHSysUnitId = {0},", IIf(nFNHSysUnitId = 0, "NULL", nFNHSysUnitId)))
                                        oStrBuilder.AppendLine(String.Format("    A.FTStateEmb = N'{0}',", tFTStateEmb))
                                        oStrBuilder.AppendLine(String.Format("    A.FTStatePrint = N'{0}',", tFTStatePrint))
                                        oStrBuilder.AppendLine(String.Format("    A.FTStateHeat = N'{0}',", tFTStateHeat))
                                        oStrBuilder.AppendLine(String.Format("    A.FTStateLaser = N'{0}',", tFTStateLaser))
                                        oStrBuilder.AppendLine(String.Format("    A.FTStateWindows = N'{0}'", tFTStateWindows))
                                        oStrBuilder.AppendLine("FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub] AS A")
                                        oStrBuilder.AppendLine(String.Format("WHERE A.FTOrderNo = N'{0}'", HI.UL.ULF.rpQuoted(tFTOrderNoDT)))
                                        oStrBuilder.AppendLine(String.Format("      AND A.FTSubOrderNo = N'{0}';", HI.UL.ULF.rpQuoted(tFTSubOrderNo)))

                                        '...Syncronized Bom Sheet Mer Material Component
                                        'oStrBuilder.AppendLine(String.Format("MERGE INTO [{0}]..[TMERTOrderSub_Component] AS target", HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN)))
                                        'oStrBuilder.AppendLine("USING (SELECT A.FNHSysMerMatId, A.FTComponent, A.FTOrderNo, A.FTSubOrderNo")
                                        'oStrBuilder.AppendLine(String.Format("       FROM [{0}]..[TMERTStyle_Mat] AS A WITH(NOLOCK)", HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN)))
                                        'oStrBuilder.AppendLine("       WHERE A.FTStateActive = N'1'")
                                        'oStrBuilder.AppendLine("             AND A.FTStateMainMaterial = N'1'")
                                        'oStrBuilder.AppendLine(String.Format("             AND A.FNHSysStyleId = (SELECT TOP 1 L1.FNHSysStyleId FROM [{0}]..[TMERTOrder] AS L1 WITH(NOLOCK) WHERE L1.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(tFTOrderNoDT) & "')", HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN)))
                                        'oStrBuilder.AppendLine("             AND ((A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(tFTOrderNoDT) & "' AND A.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(tFTSubOrderNo) & "') OR (A.FTOrderNo = N'-1' AND A.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(tFTSubOrderNo) & "')")
                                        'oStrBuilder.AppendLine("                                                                                OR (A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(tFTOrderNoDT) & "' AND A.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(tFTSubOrderNo) & "')")
                                        'oStrBuilder.AppendLine("                                                                                OR (A.FTOrderNo = N'-1' AND A.FTSubOrderNo = N'-1'))) AS source")
                                        'oStrBuilder.AppendLine("ON (target.FNHSysMerMatId = source.FNHSysMerMatId)")
                                        'oStrBuilder.AppendLine(String.Format("   AND (target.FTOrderNo = N'{0}')", HI.UL.ULF.rpQuoted(tFTOrderNoDT)))
                                        'oStrBuilder.AppendLine(String.Format("   AND (target.FTSubOrderNo = N'{0}')", HI.UL.ULF.rpQuoted(tFTSubOrderNo)))
                                        'oStrBuilder.AppendLine("WHEN MATCHED AND (target.FNHSysMerMatId = source.FNHSysMerMatId) THEN")
                                        'oStrBuilder.AppendLine("     UPDATE SET target.FTComponent  =  source.FTComponent,")
                                        'oStrBuilder.AppendLine("                target.FTUpdUser = @FTUpdUser,")
                                        'oStrBuilder.AppendLine("                target.FDUpdDate = @FDUpdDate,")
                                        'oStrBuilder.AppendLine("                target.FTUpdTime = @FTUpdTime")
                                        'oStrBuilder.AppendLine("WHEN NOT MATCHED BY target THEN")
                                        'oStrBuilder.AppendLine("     INSERT ([FTInsUser],[FDInsDate],[FTInsTime],FTUpdUser, FDUpdDate, FTUpdTime,")
                                        'oStrBuilder.AppendLine("             [FTOrderNo],[FTSubOrderNo],[FNHSysMerMatId] ,[FTComponent],[FTRemark])")
                                        'oStrBuilder.AppendLine("     VALUES (@FTUpdUser, @FDUpdDate, @FTUpdTime,")
                                        'oStrBuilder.AppendLine("             NULL, NULL, NULL, N'" & HI.UL.ULF.rpQuoted(tFTOrderNoDT) & "', N'" & HI.UL.ULF.rpQuoted(tFTSubOrderNo) & "', FNHSysMerMatId, FTComponent, NULL)")
                                        'oStrBuilder.AppendLine("WHEN NOT MATCHED BY source AND (target.FTOrderNo  = N'" & HI.UL.ULF.rpQuoted(tFTOrderNoDT) & "') AND (target.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(tFTSubOrderNo) & "') THEN")
                                        'oStrBuilder.AppendLine("     DELETE;")

                                    End If

                                End If

                            End If

                        Catch ex As Exception
                            If System.Diagnostics.Debugger.IsAttached = True Then
                                Throw New Exception(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString())
                            End If
                        End Try
                    ' End If

                Next nLoopAdjustSubFONo

                tSql = ""
                tSql = oStrBuilder.ToString()

                If HI.Conn.SQLConn.Execute_Tran(tSql, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                    If Not System.Diagnostics.Debugger.IsAttached = True Then
                        MsgBox("Step failed at update factory sub order no !!!", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
                    End If

                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    bSaveData = False
                Else
                    'HI.Conn.SQLConn.Tran.Commit()
                    'HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    'HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    bSaveData = True
                End If

                If bSaveData = True Then
                    Dim nFNHSysMatColorId As Integer, nFNHSysMatSizeId As Integer
                    Dim nFNPrice As Double, nFNAmt As Double, nFNExtraQty As Double, nFNAmntExtra As Double, nFNGrandAmnt As Double
                    Dim nFNQuantity As Integer, nFNQuantityExtra As Integer, nFNGrandQuantity As Integer
                    Dim nFNGarmentQtyTest As Double
                    Dim nFNGarmentQtyExTest As Double
                    oStrBuilder.Remove(0, oStrBuilder.Length)

                    oStrBuilder.AppendLine("DECLARE @FTUpdUser AS NVARCHAR(30);")
                    oStrBuilder.AppendLine("DECLARE @FDUpdDate AS NVARCHAR(50);")
                    oStrBuilder.AppendLine("DECLARE @FTUpdTime AS NVARCHAR(50);")
                    oStrBuilder.AppendLine(String.Format("SET @FTUpdUser = N'{0}';", HI.ST.UserInfo.UserName))
                    oStrBuilder.AppendLine("SELECT @FDUpdDate = CONVERT(VARCHAR(10),GETDATE(),111);")
                    oStrBuilder.AppendLine("SELECT @FTUpdTime = CONVERT(VARCHAR(8),GETDATE(),114);")

                    For nLoopAdjustBreakdown As Integer = 0 To Me.ogvAdjustSubFONoBreakdown.DataRowCount - 1
                        Dim tFTOrderNoBreakdown As String, tFTSubOrderNoBreakdown As String

                        ' If Val(Me.ogvAdjustSubFONoBreakdown.GetRowCellValue(nLoopAdjustBreakdown, "FNHSysBuyId_Hide").ToString()) = Val(FNHSysBuyId.Properties.Tag.ToString()) Then
                        Try
                                Dim oDataRowBreakdown As DataRow = Me.ogvAdjustSubFONoBreakdown.GetDataRow(nLoopAdjustBreakdown)

                            If Not oDataRowBreakdown Is Nothing Then

                                tFTOrderNoBreakdown = Me.ogvAdjustSubFONoBreakdown.GetRowCellValue(nLoopAdjustBreakdown, "FTOrderNo").ToString()
                                tFTSubOrderNoBreakdown = Me.ogvAdjustSubFONoBreakdown.GetRowCellValue(nLoopAdjustBreakdown, "FTSubOrderNo").ToString()

                                Dim nValidateFONoBreakdownRow As Integer
                                nValidateFONoBreakdownRow = W_PRCnGETRowHandleByColumnValue(Me.ogvAdjustFONo, "FTOrderNo", tFTOrderNoBreakdown)

                                If nValidateFONoBreakdownRow >= 0 Then

                                    Dim oFONoRowBreakdown As DataRow = Me.ogvAdjustFONo.GetDataRow(nValidateFONoBreakdownRow)

                                    If Not oFONoRowBreakdown Is Nothing And oFONoRowBreakdown.ItemArray.Count > 0 Then

                                        nFNHSysMatColorId = Val(oDataRowBreakdown.Item("FNHSysMatColorId"))
                                        nFNHSysMatSizeId = Val(oDataRowBreakdown.Item("FNHSysMatSizeId"))

                                        nFNPrice = 0 : nFNAmt = 0 : nFNExtraQty = 0 : nFNAmntExtra = 0 : nFNGrandAmnt = 0
                                        nFNQuantity = 0 : nFNQuantityExtra = 0
                                        nFNGarmentQtyTest = 0

                                        If Not DBNull.Value.Equals(oDataRowBreakdown.Item("FNQuantity")) Then
                                            nFNQuantity = Val(oDataRowBreakdown.Item("FNQuantity"))
                                        End If

                                        If Not DBNull.Value.Equals(oDataRowBreakdown.Item("FNPrice")) Then
                                            nFNPrice = Val(oDataRowBreakdown.Item("FNPrice"))
                                        End If

                                        If Not DBNull.Value.Equals(oDataRowBreakdown.Item("FNAmt")) Then
                                            nFNAmt = Val(oDataRowBreakdown.Item("FNAmt"))
                                        End If

                                        If Not DBNull.Value.Equals(oDataRowBreakdown.Item("FNExtraQty")) Then
                                            nFNExtraQty = Val(oDataRowBreakdown.Item("FNExtraQty"))
                                        End If

                                        If Not DBNull.Value.Equals(oDataRowBreakdown.Item("FNQuantityExtra")) Then
                                            nFNQuantityExtra = Val(oDataRowBreakdown.Item("FNQuantityExtra"))
                                        End If

                                        If Not DBNull.Value.Equals(oDataRowBreakdown.Item("FNGrandQuantity")) Then
                                            nFNGrandQuantity = Val(oDataRowBreakdown.Item("FNGrandQuantity"))
                                        End If

                                        If Not DBNull.Value.Equals(oDataRowBreakdown.Item("FNAmntExtra")) Then
                                            nFNAmntExtra = Val(oDataRowBreakdown.Item("FNAmntExtra"))
                                        End If

                                        If Not DBNull.Value.Equals(oDataRowBreakdown.Item("FNGrandAmnt")) Then
                                            nFNGrandAmnt = Val(oDataRowBreakdown.Item("FNGrandAmnt"))
                                        End If

                                        If Not DBNull.Value.Equals(oDataRowBreakdown.Item("FNGarmentQtyTest")) Then
                                            nFNGarmentQtyTest = Val(oDataRowBreakdown.Item("FNGarmentQtyTest"))
                                        Else
                                            nFNGarmentQtyTest = 0
                                        End If

                                        If Not DBNull.Value.Equals(oDataRowBreakdown.Item("FNExternalQtyTest")) Then
                                            nFNGarmentQtyExTest = Val(oDataRowBreakdown.Item("FNExternalQtyTest"))
                                        Else
                                            nFNGarmentQtyExTest = 0
                                        End If

                                        nFNGrandQuantity = nFNQuantity + nFNGarmentQtyTest + nFNQuantityExtra + nFNGarmentQtyExTest
                                        nFNGrandAmnt = Double.Parse(Format(Val(nFNGrandQuantity) * Val(nFNPrice), "0.00"))

                                        Dim _FNAmntQtyTest As Double = 0
                                        Try
                                            _FNAmntQtyTest = Double.Parse(Format(Val(nFNGarmentQtyTest) * Val(nFNPrice), "0.00"))
                                        Catch ex As Exception
                                        End Try

                                        Dim _FNAmntQtyExTest As Double = 0
                                        Try
                                            _FNAmntQtyExTest = Double.Parse(Format(Val(nFNGarmentQtyExTest) * Val(nFNPrice), "0.00"))
                                        Catch ex As Exception
                                        End Try


                                        Dim _CMDisPer As Double = GetCMDisPer(tFTOrderNoBreakdown)
                                        oStrBuilder.AppendLine("UPDATE A")
                                        oStrBuilder.AppendLine("SET A.FTUpdUser = @FTUpdUser,")
                                        oStrBuilder.AppendLine("    A.FDUpdDate = @FDUpdDate,")
                                        oStrBuilder.AppendLine("    A.FTUpdTime = @FTUpdTime,")
                                        oStrBuilder.AppendLine(String.Format("    A.FNPrice = {0},", nFNPrice))
                                        oStrBuilder.AppendLine(String.Format("    A.FNQuantity = {0},", nFNQuantity))
                                        oStrBuilder.AppendLine(String.Format("    A.FNAmt = {0},", nFNAmt))
                                        oStrBuilder.AppendLine(String.Format("    A.FNExtraQty = {0},", nFNExtraQty))
                                        oStrBuilder.AppendLine(String.Format("    A.FNQuantityExtra = {0},", nFNQuantityExtra))
                                        oStrBuilder.AppendLine(String.Format("    A.FNAmntExtra = {0},", nFNAmntExtra))
                                        oStrBuilder.AppendLine(String.Format("    A.FNGrandQuantity = {0},", nFNGrandQuantity))
                                        oStrBuilder.AppendLine(String.Format("    A.FNGrandAmnt = {0}", nFNGrandAmnt))
                                        oStrBuilder.AppendLine(String.Format("  ,  A.FNGarmentQtyTest = {0}", nFNGarmentQtyTest))
                                        oStrBuilder.AppendLine(String.Format("  ,  A.FNAmntQtyTest = {0}", _FNAmntQtyTest))

                                        oStrBuilder.AppendLine(String.Format("  ,  A.FNExternalQtyTest = {0}", nFNGarmentQtyExTest))
                                        oStrBuilder.AppendLine(String.Format("  ,  A.FNExternalAmntQtyTest = {0}", _FNAmntQtyExTest))

                                        oStrBuilder.AppendLine(String.Format("  ,  A.FNPriceOrg = {0}", nFNPrice))
                                        oStrBuilder.AppendLine(String.Format("  ,  A.FNCMDisPer = {0}", _CMDisPer))
                                        oStrBuilder.AppendLine(String.Format("  ,  A.FNCMDisAmt = {0}", (nFNPrice * _CMDisPer) / 100))
                                        oStrBuilder.AppendLine("FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub_BreakDown] AS A")
                                        oStrBuilder.AppendLine(String.Format("WHERE A.FTOrderNo = N'{0}'", tFTOrderNoBreakdown))
                                        oStrBuilder.AppendLine(String.Format("      AND A.FTSubOrderNo = N'{0}'", tFTSubOrderNoBreakdown))
                                        oStrBuilder.AppendLine(String.Format("      AND A.FNHSysMatColorId = {0}", nFNHSysMatColorId))
                                        oStrBuilder.AppendLine(String.Format("      AND A.FNHSysMatSizeId = {0};", nFNHSysMatSizeId))

                                        Call SaveCMPrice(tFTOrderNoBreakdown)


                                    End If

                                End If

                            End If

                        Catch ex As Exception
                                If System.Diagnostics.Debugger.IsAttached = True Then
                                    Throw New Exception(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString())
                                End If
                            End Try
                        'End If




                    Next nLoopAdjustBreakdown

                    tSql = ""
                    tSql = oStrBuilder.ToString()

                    If HI.Conn.SQLConn.Execute_Tran(tSql, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                        If Not System.Diagnostics.Debugger.IsAttached = True Then
                            MsgBox("Step failed at update factory sub order no breakdown !!!", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
                        End If

                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        bSaveData = False
                    Else
                        HI.Conn.SQLConn.Tran.Commit()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        bSaveData = True
                    End If

                End If

            End If

        Catch ex As Exception
            If System.Diagnostics.Debugger.IsAttached = True Then
                MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
            End If
        End Try

        If Not System.Diagnostics.Debugger.IsAttached = True Then
            _Spls.Close()
        End If

        Return bSaveData

    End Function

    Private Sub SaveCMPrice(tFTOrderNoBreakdown As String)
        Try
            Dim _StyleId As Integer = CInt(GetStyleIdByOrderNo(tFTOrderNoBreakdown))
            If PDataTableTmp.Select("FNHSysStyleId=" & _StyleId).Count <= 0 Then
                PDataTableTmp.Rows.Add(_StyleId)
            End If
        Catch ex As Exception
        End Try

    End Sub

    Private Function GetStyleIdByOrderNo(_OrderNo As String) As String
        Try
            Dim _Cmd As String = ""
            _Cmd = "Select Top 1 FNHSysStyleId From   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrder  WITH(NOLOCK) "
            _Cmd &= vbCrLf & "WHERE  FTOrderNo='" & HI.UL.ULF.rpQuoted(_OrderNo) & "'"
            Return HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_MERCHAN, "")
        Catch ex As Exception
        End Try
    End Function

    Private Function GetCMDisPer(_OrderNo As String) As Double
        Try
            Dim _Cmd As String = ""
            _Cmd = "Select Top 1 S.FNCMDisPer From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TMERMStyle AS S WITH(NOLOCK) INNER JOIN "
            _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrder AS O WITH(NOLOCK) ON S.FNHSysStyleId = O.FNHSysStyleId "
            _Cmd &= vbCrLf & "WHERE O.FTOrderNo='" & HI.UL.ULF.rpQuoted(_OrderNo) & "'"
            Return HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_MERCHAN, "0")
        Catch ex As Exception
        End Try
        Return 0
    End Function

    Private Function W_PRCbDeleteFONo() As Boolean
        Dim bRetDelete As Boolean = False

        Dim _Spls As New HI.TL.SplashScreen("Delete data  Please Wait...")

        Try
            tSql = ""

            Dim tFTOrderNo As String
            bRetDelete = False

            For Each oFONoRow As DataRow In CType(ogdAdjustFONo.DataSource, DataTable).Select("FTChk='1' AND FNHSysBuyId_Hide=" & Val(FNHSysBuyId.Properties.Tag.ToString()) & "")
                tFTOrderNo = ""
                tFTOrderNo = oFONoRow.Item("FTOrderNo").ToString()

                If Not tFTOrderNo Is Nothing And tFTOrderNo <> "" Then
                    tSql = "DELETE A"
                    tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub_SizeSpec] AS A"
                    tSql &= Environment.NewLine & "WHERE A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(tFTOrderNo) & "';"
                    tSql &= Environment.NewLine & "DELETE A"
                    tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub_Bundle AS A"
                    tSql &= Environment.NewLine & "WHERE A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(tFTOrderNo) & "'"
                    tSql &= Environment.NewLine & "DELETE A"
                    tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub_Pack] AS A"
                    tSql &= Environment.NewLine & "WHERE A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(tFTOrderNo) & "';"
                    tSql &= Environment.NewLine & "DELETE A"
                    tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub_Sew] AS A"
                    tSql &= Environment.NewLine & "WHERE A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(tFTOrderNo) & "';"
                    tSql &= Environment.NewLine & "DELETE A"
                    tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub_Component] AS A"
                    tSql &= Environment.NewLine & "WHERE A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(tFTOrderNo) & "';"
                    tSql &= Environment.NewLine & "DELETE A"
                    tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub_BreakDown] AS A"
                    tSql &= Environment.NewLine & "WHERE A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(tFTOrderNo) & "';"
                    tSql &= Environment.NewLine & "DELETE A"
                    tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub] AS A"
                    tSql &= Environment.NewLine & "WHERE A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(tFTOrderNo) & "';"
                    'tSql &= Environment.NewLine & "DELETE A"
                    'tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrder_BreakDown] AS A"
                    'tSql &= Environment.NewLine & "WHERE A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(tFTOrderNo) & "';"
                    tSql &= Environment.NewLine & "DELETE A"
                    tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrder] AS A"
                    tSql &= Environment.NewLine & "WHERE A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(tFTOrderNo) & "';" & Environment.NewLine

                    If HI.Conn.SQLConn.ExecuteNonQuery(tSql, Conn.DB.DataBaseName.DB_MERCHAN) Then
                        bRetDelete = True
                        HI.Auditor.CreateLog.CreateLogDelete(HI.ST.SysInfo.MenuName, Me.Name, "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(tFTOrderNo) & "'")

                        Dim cmdstring As String = "EXEC [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.USP_GEN_BREAKDOWN_SHIPDESINATION '" & HI.UL.ULF.rpQuoted(tFTOrderNo) & "'"
                        HI.Conn.SQLConn.ExecuteOnly(cmdstring, Conn.DB.DataBaseName.DB_MERCHAN)
                    End If

                End If

            Next


        Catch ex As Exception
            If System.Diagnostics.Debugger.IsAttached = True Then
                MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
            End If
        End Try

        _Spls.Close()

        Return bRetDelete

    End Function

    Private Sub W_PRCxRecalSubOrderBreakdownExtraQty(ByVal pnRowHandle As Integer)
        Try
            Dim nFNQuantity As Integer, nFNQuantityExtra As Integer, nFNGrandQuantity As Integer
            Dim nFNPrice As Double, nFNExtraQty As Double, nFNAmt As Double, nFNAmntExtra As Double, nFNGrandAmnt As Double

            nFNQuantity = 0
            nFNPrice = 0
            nFNAmt = 0
            nFNExtraQty = 0

            If Not DBNull.Value.Equals(Me.ogvAdjustSubFONoBreakdown.GetRowCellValue(pnRowHandle, "FNQuantity")) Then
                nFNQuantity = Val(Me.ogvAdjustSubFONoBreakdown.GetRowCellValue(pnRowHandle, "FNQuantity"))
            End If

            If Not DBNull.Value.Equals(Me.ogvAdjustSubFONoBreakdown.GetRowCellValue(pnRowHandle, "FNPrice")) Then
                nFNPrice = Val(Me.ogvAdjustSubFONoBreakdown.GetRowCellValue(pnRowHandle, "FNPrice"))
            End If

            If Not DBNull.Value.Equals(Me.ogvAdjustSubFONoBreakdown.GetRowCellValue(pnRowHandle, "FNAmt")) Then
                nFNAmt = Val(Me.ogvAdjustSubFONoBreakdown.GetRowCellValue(pnRowHandle, oColFNAmt))
            End If

            If Not DBNull.Value.Equals(Me.ogvAdjustSubFONoBreakdown.GetRowCellValue(pnRowHandle, "FNExtraQty")) Then
                nFNExtraQty = Val(Me.ogvAdjustSubFONoBreakdown.GetRowCellValue(pnRowHandle, "FNExtraQty"))
            End If


            If nFNQuantity = 0 Then
                nFNQuantityExtra = 0
            Else
                If nFNExtraQty = 0 Then
                    nFNQuantityExtra = 0

                Else
                    Dim nFractionNumber#
                    nFractionNumber = nFNQuantity * (nFNExtraQty / 100)

                    Dim tRetExtraQty$ = CStr(nFractionNumber)

                    Dim nPosDigit% = Microsoft.VisualBasic.InStr(tRetExtraQty, ".")

                    If nPosDigit = 0 Then
                        nFNQuantityExtra = CDbl(nFractionNumber)
                    Else
                        Dim tFractionInt$ = Microsoft.VisualBasic.Mid$(tRetExtraQty, 1, nPosDigit - 1)
                        Dim tFractionDecimal$ = Microsoft.VisualBasic.Mid$(tRetExtraQty, nPosDigit + 1, Len(tRetExtraQty) - nPosDigit)

                        Dim nReal#
                        Double.TryParse(tFractionDecimal, nReal)

                        If nReal > 0 Then
                            Dim nFractionInt#
                            Double.TryParse(tFractionInt, nFractionInt)
                            nFNQuantityExtra = CDbl(IIf(nFractionInt < 0, 0, nFractionInt) + 1)
                        Else
                            Dim nFractionInt#
                            Double.TryParse(tFractionInt, nFractionInt)

                            nFNQuantityExtra = CDbl(IIf(nFractionInt < 0, 0, nFractionInt))
                        End If

                    End If

                End If

            End If

            '...update FNQuantityExtra
            Me.ogvAdjustSubFONoBreakdown.SetRowCellValue(pnRowHandle, oColFNQuantityExtra, nFNQuantityExtra)
            Me.ogvAdjustSubFONoBreakdown.RefreshRowCell(pnRowHandle, oColFNQuantityExtra)
            '...update FNGrandQuantity
            nFNGrandQuantity = nFNQuantity + nFNQuantityExtra
            Me.ogvAdjustSubFONoBreakdown.SetRowCellValue(pnRowHandle, oColFNGrandQuantity, nFNGrandQuantity)
            Me.ogvAdjustSubFONoBreakdown.RefreshRowCell(pnRowHandle, oColFNGrandQuantity)
            '...update FNAmntExtra
            nFNAmntExtra = nFNPrice * nFNQuantityExtra
            Me.ogvAdjustSubFONoBreakdown.SetRowCellValue(pnRowHandle, oColFNAmntExtra, nFNAmntExtra)
            Me.ogvAdjustSubFONoBreakdown.RefreshRowCell(pnRowHandle, oColFNAmntExtra)
            '...update FNGrandAmnt
            nFNGrandAmnt = nFNAmt + nFNAmntExtra
            Me.ogvAdjustSubFONoBreakdown.SetRowCellValue(pnRowHandle, oColFNGrandAmnt, nFNGrandAmnt)
            Me.ogvAdjustSubFONoBreakdown.RefreshRowCell(pnRowHandle, oColFNGrandAmnt)

        Catch ex As Exception
            If System.Diagnostics.Debugger.IsAttached = True Then
                MsgBox(ex.Message().ToString() & ControlChars.CrLf & ex.StackTrace().ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
            End If
        End Try

    End Sub

#End Region

    Private Sub ockAll_CheckedChanged(sender As Object, e As EventArgs) Handles ockAll.CheckedChanged
        Try
            Dim _State As String = "0"
            If Me.ockAll.Checked Then
                _State = "1"
            End If

            With ogdAdjustFONo
                If Not (.DataSource Is Nothing) And ogvAdjustFONo.RowCount > 0 Then

                    With ogvAdjustFONo
                        For I As Integer = 0 To .RowCount - 1

                            'Select Case .GetRowCellValue(I, "FTVenderPramCode").ToString()
                            '    Case "HIC", "HTV"
                            '    Case Else
                            .SetRowCellValue(I, .Columns.ColumnByFieldName("FTChk"), _State)
                            'End Select

                        Next
                    End With

                    CType(.DataSource, DataTable).AcceptChanges()
                End If

            End With

        Catch ex As Exception
            If System.Diagnostics.Debugger.IsAttached = True Then
                MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
            End If
        End Try

    End Sub

    Private Sub ogvAdjustFONo_ColumnFilterChanged(sender As Object, e As EventArgs) Handles ogvAdjustFONo.ColumnFilterChanged
        Try
            'Dim oDBdv As System.Data.DataView
            'oDBdv = W_PRCdvGETViewFilterData(Me.ogvAdjustFONo)
            'If Not oDBdv Is Nothing Then
            '    '...refresh datasource for gridview sub order no and sub order no breakdown
            '    Dim tFTFilterFONoCriteria As String = ""
            '    For Each oRowView As DataRowView In oDBdv
            '        Dim oDataRow As DataRow = oRowView.Row
            '        tFTFilterFONoCriteria = tFTFilterFONoCriteria & oDataRow.Item("FTOrderNo").ToString() & "|"
            '    Next

            '    If tFTFilterFONoCriteria <> "" Then
            '        tFTFilterFONoCriteria = Microsoft.VisualBasic.Mid$(tFTFilterFONoCriteria, 1, Len(tFTFilterFONoCriteria) - 1)
            '        Call W_PRCbLoadFONoSub(tFTFilterFONoCriteria)
            '        Call W_PRCbLoadFONoSubBreakdown(tFTFilterFONoCriteria)
            '    End If

            'End If

            If FilerData = False Then

                FilerData = True

                For Each GridCol As DevExpress.XtraGrid.Columns.GridColumn In ogvAdjustFONo.Columns
                    Select Case GridCol.FieldName.ToString
                        Case "FTOrderNo"

                            Try
                                Dim K As String = GridCol.FilterInfo.Value.ToString

                                If K <> "" Then
                                    ogvAdjustSubFONo.Columns.ColumnByFieldName(GridCol.FieldName).FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[" & GridCol.FieldName & "] Like '%" & K & "%'", K, K, ColumnFilterType.Value)
                                    ogvAdjustSubFONoBreakdown.Columns.ColumnByFieldName(GridCol.FieldName).FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[" & GridCol.FieldName & "] Like '%" & K & "%'", K, K, ColumnFilterType.Value)

                                Else
                                    ogvAdjustSubFONo.Columns.ColumnByFieldName(GridCol.FieldName).ClearFilter()
                                    ogvAdjustSubFONoBreakdown.Columns.ColumnByFieldName(GridCol.FieldName).ClearFilter()
                                End If

                            Catch ex As Exception
                                ogvAdjustSubFONo.Columns.ColumnByFieldName(GridCol.FieldName).ClearFilter()
                                ogvAdjustSubFONoBreakdown.Columns.ColumnByFieldName(GridCol.FieldName).ClearFilter()
                            End Try

                        Case "FTStyleCode"

                            REM 2015/01/29
                            'Dim tTextFilterStyleCode As String = GridCol.FilterInfo.Value.ToString

                            'If tTextFilterStyleCode <> "" Then
                            '    Try
                            '        Dim oDBdv As System.Data.DataView
                            '        oDBdv = W_PRCdvGETViewFilterData(Me.ogvAdjustFONo)
                            '        If Not oDBdv Is Nothing AndAlso oDBdv.Count > 0 Then

                            '            '...refresh datasource for gridview sub order no and sub order no breakdown
                            '            Dim tFTFilterFONoCriteria As String = ""

                            '            For Each oRowView As DataRowView In oDBdv
                            '                Dim oDataRow As DataRow = oRowView.Row

                            '                If tFTFilterFONoCriteria = "" Then
                            '                    tFTFilterFONoCriteria = "[FTOrderNo] = '" & oDataRow.Item(11).ToString() & "'"
                            '                Else
                            '                    tFTFilterFONoCriteria = tFTFilterFONoCriteria & " OR [FTOrderNo] = '" & oDataRow.Item(11).ToString() & "'"
                            '                End If

                            '            Next

                            '            If tFTFilterFONoCriteria <> "" Then
                            '                ogvAdjustSubFONo.Columns.ColumnByFieldName("FTOrderNo").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo(DevExpress.XtraGrid.Columns.ColumnFilterType.Custom, tFTFilterFONoCriteria)
                            '                ogvAdjustSubFONoBreakdown.Columns.ColumnByFieldName("FTOrderNo").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo(DevExpress.XtraGrid.Columns.ColumnFilterType.Custom, tFTFilterFONoCriteria)
                            '            Else
                            '                ogvAdjustSubFONo.Columns.ColumnByFieldName("FTOrderNo").ClearFilter()
                            '                ogvAdjustSubFONoBreakdown.Columns.ColumnByFieldName("FTOrderNo").ClearFilter()
                            '            End If

                            '        Else
                            '            ogvAdjustSubFONo.Columns.ColumnByFieldName("FTOrderNo").ClearFilter()
                            '            ogvAdjustSubFONoBreakdown.Columns.ColumnByFieldName("FTOrderNo").ClearFilter()
                            '        End If

                            '    Catch ex As Exception
                            '        ogvAdjustSubFONo.Columns.ColumnByFieldName("FTOrderNo").ClearFilter()
                            '        ogvAdjustSubFONoBreakdown.Columns.ColumnByFieldName("FTOrderNo").ClearFilter()
                            '    End Try

                            'Else
                            '    ogvAdjustSubFONo.Columns.ColumnByFieldName("FTOrderNo").ClearFilter()
                            '    ogvAdjustSubFONoBreakdown.Columns.ColumnByFieldName("FTOrderNo").ClearFilter()

                            'End If

                            Try
                                Dim tTextFilterStyleCode As String = GridCol.FilterInfo.Value.ToString

                                If tTextFilterStyleCode <> "" Then
                                    ogvAdjustSubFONo.Columns.ColumnByFieldName(GridCol.FieldName).FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[" & GridCol.FieldName & "] LIKE '%" & tTextFilterStyleCode & "%'", tTextFilterStyleCode, tTextFilterStyleCode, ColumnFilterType.Value)
                                    ogvAdjustSubFONoBreakdown.Columns.ColumnByFieldName(GridCol.FieldName).FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[" & GridCol.FieldName & "] LIKE '%" & tTextFilterStyleCode & "%'", tTextFilterStyleCode, tTextFilterStyleCode, ColumnFilterType.Value)
                                Else
                                    ogvAdjustSubFONo.Columns.ColumnByFieldName(GridCol.FieldName).ClearFilter()
                                    ogvAdjustSubFONoBreakdown.Columns.ColumnByFieldName(GridCol.FieldName).ClearFilter()
                                End If

                            Catch ex As Exception
                                ogvAdjustSubFONo.Columns.ColumnByFieldName(GridCol.FieldName).ClearFilter()
                                'ogvAdjustSubFONoBreakdown.Columns.ColumnByFieldName(GridCol.FieldName).ClearFilter()
                            End Try

                        Case "FNHSysBuyId"

                            '...FTBuyCode
                            Try
                                Dim tTextFilterBuyCode As String = GridCol.FilterInfo.Value.ToString

                                If tTextFilterBuyCode <> "" Then
                                    ogvAdjustSubFONo.Columns.ColumnByFieldName("FNHSysBuyId").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[FNHSysBuyId] LIKE '%" & tTextFilterBuyCode & "%'", tTextFilterBuyCode, tTextFilterBuyCode, ColumnFilterType.Value)
                                    ogvAdjustSubFONoBreakdown.Columns.ColumnByFieldName("FNHSysBuyId").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[FNHSysBuyId] LIKE '%" & tTextFilterBuyCode & "%'", tTextFilterBuyCode, tTextFilterBuyCode, ColumnFilterType.Value)


                                    'If CType(Me.ogdAdjustSubFONo.DataSource, System.Data.DataTable).Select("FNHSysBuyId = '" & HI.UL.ULF.rpQuoted(tTextFilterBuyCode) & "'").Length > 0 Then

                                    '    MsgBox("FTBuyCode {FNHSysBuyId} : " & tTextFilterBuyCode & " has exists record...", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, My.Application.Info.Title)

                                    'End If

                                Else
                                    ogvAdjustSubFONo.Columns.ColumnByFieldName("FNHSysBuyId").ClearFilter()
                                    ogvAdjustSubFONoBreakdown.Columns.ColumnByFieldName("FNHSysBuyId").ClearFilter()
                                End If

                            Catch ex As Exception

                                'If System.Diagnostics.Debugger.IsAttached = True Then
                                '    MsgBox(ex.Message().ToString & Environment.NewLine & ex.StackTrace().ToString, MsgBoxStyle.Information + MsgBoxStyle.OkCancel, My.Application.Info.Title)
                                'End If

                                ogvAdjustSubFONo.Columns.ColumnByFieldName("FNHSysBuyId").ClearFilter()
                                ogvAdjustSubFONoBreakdown.Columns.ColumnByFieldName("FNHSysBuyId").ClearFilter()

                            End Try

                        Case Else

                    End Select

                Next

                FilerData = False

            End If

        Catch ex As Exception
            'If System.Diagnostics.Debugger.IsAttached = True Then
            '    MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace.ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
            'End If
            FilerData = False
        End Try

    End Sub

    Private Sub ogvAdjustSubFONo_ColumnFilterChanged(sender As Object, e As EventArgs) Handles ogvAdjustSubFONo.ColumnFilterChanged
        'Try
        '    Dim oDBdv As System.Data.DataView
        '    oDBdv = W_PRCdvGETViewFilterData(Me.ogvAdjustSubFONo)
        '    If Not oDBdv Is Nothing Then
        '        '...refresh datasource for gridview sub order no and sub order no breakdown
        '        Dim tFTFilterFONoSubCriteria As String = ""
        '        For Each oRowView As DataRowView In oDBdv
        '            Dim oDataRow As DataRow = oRowView.Row
        '            REM 2014/06/14 tFTFilterFONoCriteria = tFTFilterFONoCriteria & oDataRow.Item("FTOrderNo").ToString() & "|"
        '            tFTFilterFONoSubCriteria = tFTFilterFONoSubCriteria & oDataRow.Item("FTSubOrderNo").ToString() & "|"
        '        Next

        '        If tFTFilterFONoSubCriteria <> "" Then
        '            tFTFilterFONoSubCriteria = Microsoft.VisualBasic.Mid$(tFTFilterFONoSubCriteria, 1, Len(tFTFilterFONoSubCriteria) - 1)
        '            Call W_PRCbLoadFONoSubBreakdown("", tFTFilterFONoSubCriteria)
        '        End If

        '    End If


        'Catch ex As Exception
        '    If System.Diagnostics.Debugger.IsAttached = True Then
        '        MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace.ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
        '    End If
        'End Try
        Try

            If FilerData = False Then

                FilerData = True

                For Each GridCol As DevExpress.XtraGrid.Columns.GridColumn In ogvAdjustSubFONo.Columns

                    Select Case GridCol.FieldName.ToString
                        Case "FTOrderNo"
                            Try
                                Dim K As String = GridCol.FilterInfo.Value.ToString

                                If K <> "" Then
                                    ogvAdjustFONo.Columns.ColumnByFieldName(GridCol.FieldName).FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[" & GridCol.FieldName & "] Like '%" & K & "%'", K, K, ColumnFilterType.Value)
                                    ogvAdjustSubFONoBreakdown.Columns.ColumnByFieldName(GridCol.FieldName).FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[" & GridCol.FieldName & "] Like '%" & K & "%'", K, K, ColumnFilterType.Value)

                                Else
                                    ogvAdjustFONo.Columns.ColumnByFieldName(GridCol.FieldName).ClearFilter()
                                    ogvAdjustSubFONoBreakdown.Columns.ColumnByFieldName(GridCol.FieldName).ClearFilter()
                                End If
                            Catch ex As Exception
                                ogvAdjustFONo.Columns.ColumnByFieldName(GridCol.FieldName).ClearFilter()
                                ogvAdjustSubFONoBreakdown.Columns.ColumnByFieldName(GridCol.FieldName).ClearFilter()
                            End Try

                        Case "FTSubOrderNo"
                            Try
                                Dim K As String = GridCol.FilterInfo.Value.ToString
                                If K <> "" Then
                                    ogvAdjustSubFONoBreakdown.Columns.ColumnByFieldName(GridCol.FieldName).FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[" & GridCol.FieldName & "] Like '%" & K & "%'", K, K, ColumnFilterType.Value)
                                Else
                                    ogvAdjustSubFONoBreakdown.Columns.ColumnByFieldName(GridCol.FieldName).ClearFilter()
                                End If
                            Catch ex As Exception
                                ogvAdjustSubFONoBreakdown.Columns.ColumnByFieldName(GridCol.FieldName).ClearFilter()
                            End Try

                        Case "FTStyleCode"
                            '...hidden
                        Case "FTBuyCode"
                            '...hidden
                        Case Else
                            '...do nothing
                    End Select

                Next
                FilerData = False
            End If


        Catch ex As Exception
            If System.Diagnostics.Debugger.IsAttached = True Then
                MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace.ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
            End If
            FilerData = False
        End Try

    End Sub

    Private Sub ogvAdjustFONo_CustomDrawFooterCell(sender As Object, e As Views.Grid.FooterCellCustomDrawEventArgs) Handles ogvAdjustFONo.CustomDrawFooterCell

        Try
            If e.Column.FieldName = "FTStyleCode" Then

                If e.Info.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count Then

                    If Me.ogvAdjustFONo.DataRowCount > 0 Then

                        If Me.ogvAdjustFONo.ActiveFilter Is Nothing Or Me.ogvAdjustFONo.ActiveFilterEnabled = False Or Me.ogvAdjustFONo.ActiveFilter.Expression = "" Then

                            Dim oDBdvFONoView As New System.Data.DataView
                            oDBdvFONoView = CType(Me.ogvAdjustFONo.DataSource, System.Data.DataView)

                            If oDBdvFONoView.Count > 0 Then
                                Dim oDBdtFONo As System.Data.DataTable
                                oDBdtFONo = oDBdvFONoView.ToTable()

                                If Not oDBdtFONo Is Nothing And oDBdtFONo.Rows.Count > 0 Then
                                    Dim oListFTStyleCode As List(Of String) = oDBdtFONo.AsEnumerable() _
                                                                             .Select(Function(r) r.Field(Of String)("FTStyleCode")) _
                                                                             .Distinct() _
                                                                             .ToList()

                                    If Not oListFTStyleCode Is Nothing And oListFTStyleCode.Count > 0 Then
                                        e.Info.DisplayText = oListFTStyleCode.Count
                                    Else
                                        e.Info.DisplayText = ""
                                    End If
                                Else
                                    e.Info.DisplayText = ""
                                End If
                            Else
                                e.Info.DisplayText = ""
                            End If

                        Else
                            '...count distinct style code by row filter criteria
                            Dim oDBdtFONoCriteria As System.Data.DataTable = CType(Me.ogvAdjustFONo.GridControl.DataSource, System.Data.DataTable)
                            Dim oDataViewFilterData As New System.Data.DataView(oDBdtFONoCriteria)

                            Try
                                oDataViewFilterData.RowFilter = DevExpress.Data.Filtering.CriteriaToWhereClauseHelper.GetDataSetWhere(Me.ogvAdjustFONo.ActiveFilterCriteria)

                                If Not oDataViewFilterData Is Nothing And oDataViewFilterData.Count > 0 Then
                                    Dim oDBdtFONoRowFilter As System.Data.DataTable
                                    oDBdtFONoRowFilter = oDataViewFilterData.ToTable()

                                    If Not oDBdtFONoRowFilter Is Nothing And oDBdtFONoRowFilter.Rows.Count > 0 Then
                                        Dim oListFTStyleCodeRowFilter As List(Of String) = oDBdtFONoRowFilter.AsEnumerable() _
                                                                                 .Select(Function(r) r.Field(Of String)("FTStyleCode")) _
                                                                                 .Distinct() _
                                                                                 .ToList()

                                        If Not oListFTStyleCodeRowFilter Is Nothing And oListFTStyleCodeRowFilter.Count > 0 Then
                                            e.Info.DisplayText = oListFTStyleCodeRowFilter.Count
                                        Else
                                            e.Info.DisplayText = ""
                                        End If

                                    Else
                                        e.Info.DisplayText = ""
                                    End If

                                Else
                                    e.Info.DisplayText = ""
                                End If
                            Catch ex As Exception

                            End Try


                        End If

                    Else
                        e.Info.DisplayText = ""
                    End If

                    'e.Handled = True

                End If

            ElseIf e.Column.FieldName.Equals("FTOrderNo") Then
                'e.Info.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count
            End If

        Catch ex As Exception
            If System.Diagnostics.Debugger.IsAttached = True Then
                MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
            End If
        End Try

    End Sub

    Private Sub wAdjustAfterImportOrder_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            With Me.ogvAdjustSubFONo
                '...don't create application instance call by name zzz
                RemoveHandler .DoubleClick, AddressOf HI.TL.HandlerControl.GridView_DoubleClick
            End With

            With Me.ogvAdjustSubFONoBreakdown
                '...don't create application instance call by name zzz
                RemoveHandler .DoubleClick, AddressOf HI.TL.HandlerControl.GridView_DoubleClick
            End With

            With RepositoryItemFNHSysContinentId
                '...remove handle column edit for repository button edit : by cause gridview tag : 3
                RemoveHandler .ButtonClick, AddressOf HI.TL.HandlerControl.DynamicResponButtoneSysHide_ButtonClick
                RemoveHandler .EditValueChanged, AddressOf HI.TL.HandlerControl.DynamicResponButtoneditSysHide_EditValueChanged
                RemoveHandler .Leave, AddressOf HI.TL.HandlerControl.DynamicResponButtoneditSysHide_Leave

                '...add hadle column edit for repository button edit
                AddHandler .ButtonClick, AddressOf HI.TL.HandlerControl.DynamicResponButtone_ButtonClick
                AddHandler .EditValueChanged, AddressOf HI.TL.HandlerControl.DynamicResponButtonedit_EditValueChanged
                AddHandler .Leave, AddressOf HI.TL.HandlerControl.DynamicResponButtonedit_Leave
            End With

            With RepositoryItemFNHSysCountryId
                '...remove handle column edit for repository repository button edit : by cause gridview tag : 3
                RemoveHandler .ButtonClick, AddressOf HI.TL.HandlerControl.DynamicResponButtoneSysHide_ButtonClick
                RemoveHandler .EditValueChanged, AddressOf HI.TL.HandlerControl.DynamicResponButtoneditSysHide_EditValueChanged
                RemoveHandler .Leave, AddressOf HI.TL.HandlerControl.DynamicResponButtoneditSysHide_Leave

                '...add handle column edit for repository button edit
                AddHandler .ButtonClick, AddressOf HI.TL.HandlerControl.DynamicResponButtone_ButtonClick
                AddHandler .EditValueChanged, AddressOf HI.TL.HandlerControl.DynamicResponButtonedit_EditValueChanged
                AddHandler .Leave, AddressOf HI.TL.HandlerControl.DynamicResponButtonedit_Leave
            End With

            Me.ogdAdjustFONo.DataSource = Nothing
            'Me.ogvAdjustFONo.OptionsView.ColumnAutoWidth = False
            Me.ogdAdjustFONo.Refresh()
            Me.ogvAdjustFONo.RefreshData()
            Me.ogvAdjustFONo.BestFitColumns()

            Me.ogvAdjustFONo.OptionsView.ColumnAutoWidth = False
            Me.ogvAdjustFONo.RefreshData()

            Me.ogdAdjustSubFONo.DataSource = Nothing
            'Me.ogvAdjustSubFONo.OptionsView.ColumnAutoWidth = False
            Me.ogdAdjustSubFONo.Refresh()
            Me.ogvAdjustSubFONo.RefreshData()
            Me.ogvAdjustSubFONo.BestFitColumns()

            Me.ogvAdjustSubFONo.OptionsView.ColumnAutoWidth = False
            Me.ogvAdjustSubFONo.RefreshData()

            Me.ogdAdjustSubFONoBreakdown.DataSource = Nothing
            'Me.ogvAdjustSubFONoBreakdown.OptionsView.ColumnAutoWidth = False
            Me.ogdAdjustSubFONoBreakdown.Refresh()
            Me.ogvAdjustSubFONoBreakdown.RefreshData()
            Me.ogvAdjustSubFONoBreakdown.BestFitColumns()

            Me.ogvAdjustSubFONoBreakdown.OptionsView.ColumnAutoWidth = False
            Me.ogvAdjustSubFONoBreakdown.RefreshData()

        Catch ex As Exception
            If System.Diagnostics.Debugger.IsAttached = True Then
                Throw New Exception(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString())
            End If
        End Try

    End Sub

    Private Sub ogvAdjustSubFONoBreakdown_CellValueChanged(sender As Object, e As Views.Base.CellValueChangedEventArgs) Handles ogvAdjustSubFONoBreakdown.CellValueChanged

        If _bLoadFONoBreakdown = True Then Exit Sub

        Try
            Dim nRowHandle As Integer
            nRowHandle = e.RowHandle

            Dim bReCalculateColValChange As Boolean = False

            If Not bReCalculateColValChange Then

                bReCalculateColValChange = True

                Select Case e.Column.FieldName
                    Case "FNQuantity"
                        If nRowHandle >= 0 Then
                            Dim nFNQuantity As Integer = 0
                            Dim nFNQuantityExtra As Integer = 0
                            Dim nFNGrandQuantity As Integer = 0
                            Dim nFNExtraQty As Double = 0
                            Dim nFNPrice As Double = 0
                            Dim nFNAmt As Double = 0
                            Dim nFNAmntExtra As Double = 0
                            Dim nFNGrandAmnt As Double = 0

                            If Not DBNull.Value.Equals(Me.ogvAdjustSubFONoBreakdown.GetRowCellValue(nRowHandle, "FNQuantity")) Then
                                nFNQuantity = Val(Me.ogvAdjustSubFONoBreakdown.GetRowCellValue(nRowHandle, "FNQuantity"))
                            End If

                            If Not DBNull.Value.Equals(Me.ogvAdjustSubFONoBreakdown.GetRowCellValue(nRowHandle, "FNPrice")) Then
                                nFNPrice = Val(Me.ogvAdjustSubFONoBreakdown.GetRowCellValue(nRowHandle, "FNPrice"))
                            End If

                            nFNAmt = nFNQuantity * nFNPrice

                            If Not DBNull.Value.Equals(Me.ogvAdjustSubFONoBreakdown.GetRowCellValue(nRowHandle, "FNExtraQty")) Then
                                nFNExtraQty = Val(Me.ogvAdjustSubFONoBreakdown.GetRowCellValue(nRowHandle, "FNExtraQty"))
                            End If

                            '...re-calculate quantity extra
                            If nFNQuantity = 0 Then
                                nFNQuantityExtra = 0
                            Else
                                If nFNExtraQty = 0 Then
                                    nFNQuantityExtra = 0
                                Else
                                    Dim nFractionNumber#
                                    nFractionNumber = nFNQuantity * (nFNExtraQty / 100)

                                    Dim tRetExtraQty$ = CStr(nFractionNumber)

                                    Dim nPosDigit% = Microsoft.VisualBasic.InStr(tRetExtraQty, ".")

                                    If nPosDigit = 0 Then
                                        nFNQuantityExtra = CDbl(nFractionNumber)
                                    Else
                                        Dim tFractionInt$ = Microsoft.VisualBasic.Mid$(tRetExtraQty, 1, nPosDigit - 1)
                                        Dim tFractionDecimal$ = Microsoft.VisualBasic.Mid$(tRetExtraQty, nPosDigit + 1, Len(tRetExtraQty) - nPosDigit)

                                        Dim nReal#
                                        Double.TryParse(tFractionDecimal, nReal)

                                        If nReal > 0 Then
                                            Dim nFractionInt#
                                            Double.TryParse(tFractionInt, nFractionInt)
                                            nFNQuantityExtra = CDbl(IIf(nFractionInt < 0, 0, nFractionInt) + 1)
                                        Else
                                            Dim nFractionInt#
                                            Double.TryParse(tFractionInt, nFractionInt)

                                            nFNQuantityExtra = CDbl(IIf(nFractionInt < 0, 0, nFractionInt))
                                        End If

                                    End If

                                End If

                            End If

                            '...re-calculate grand quantity
                            nFNGrandQuantity = nFNQuantity + nFNQuantityExtra

                            '...re-calculate amount from extra quantity
                            nFNAmntExtra = nFNPrice * nFNQuantityExtra

                            '...re-calculate grand amount
                            nFNGrandAmnt = nFNAmt + nFNAmntExtra

                            '...update row cell value
                            '------------------------------------------------------------------------
                            '...update FNAmt
                            Me.ogvAdjustSubFONoBreakdown.SetRowCellValue(nRowHandle, "FNAmt", nFNAmt)
                            Me.ogvAdjustSubFONoBreakdown.RefreshRowCell(nRowHandle, oColFNAmt)
                            '...update FNQuantityExtra
                            Me.ogvAdjustSubFONoBreakdown.SetRowCellValue(nRowHandle, oColFNQuantityExtra, nFNQuantityExtra)
                            Me.ogvAdjustSubFONoBreakdown.RefreshRowCell(nRowHandle, oColFNQuantityExtra)
                            '...update FNGrandQuantity
                            nFNGrandQuantity = nFNQuantity + nFNQuantityExtra
                            Me.ogvAdjustSubFONoBreakdown.SetRowCellValue(nRowHandle, oColFNGrandQuantity, nFNGrandQuantity)
                            Me.ogvAdjustSubFONoBreakdown.RefreshRowCell(nRowHandle, oColFNGrandQuantity)
                            '...update FNAmntExtra
                            nFNAmntExtra = nFNPrice * nFNQuantityExtra
                            Me.ogvAdjustSubFONoBreakdown.SetRowCellValue(nRowHandle, oColFNAmntExtra, nFNAmntExtra)
                            Me.ogvAdjustSubFONoBreakdown.RefreshRowCell(nRowHandle, oColFNAmntExtra)
                            '...update FNGrandAmnt
                            nFNGrandAmnt = nFNAmt + nFNAmntExtra
                            Me.ogvAdjustSubFONoBreakdown.SetRowCellValue(nRowHandle, oColFNGrandAmnt, nFNGrandAmnt)
                            Me.ogvAdjustSubFONoBreakdown.RefreshRowCell(nRowHandle, oColFNGrandAmnt)
                            '--------------------------------------------------------------------------

                        End If

                    Case "FNPrice"
                        If nRowHandle >= 0 Then
                            Dim nFNQuantity As Integer = 0
                            Dim nFNQuantityExtra As Integer = 0
                            Dim nFNGrandQuantity As Integer = 0
                            Dim nFNExtraQty As Double = 0
                            Dim nFNPrice As Double = 0
                            Dim nFNAmt As Double = 0
                            Dim nFNAmntExtra As Double = 0
                            Dim nFNGrandAmnt As Double = 0

                            If Not DBNull.Value.Equals(Me.ogvAdjustSubFONoBreakdown.GetRowCellValue(nRowHandle, "FNQuantity")) Then
                                nFNQuantity = Val(Me.ogvAdjustSubFONoBreakdown.GetRowCellValue(nRowHandle, "FNQuantity"))
                            End If

                            If Not DBNull.Value.Equals(Me.ogvAdjustSubFONoBreakdown.GetRowCellValue(nRowHandle, "FNPrice")) Then
                                nFNPrice = Val(Me.ogvAdjustSubFONoBreakdown.GetRowCellValue(nRowHandle, "FNPrice"))
                            End If

                            nFNAmt = nFNQuantity * nFNPrice

                            If Not DBNull.Value.Equals(Me.ogvAdjustSubFONoBreakdown.GetRowCellValue(nRowHandle, "FNQuantityExtra")) Then
                                nFNQuantityExtra = Val(Me.ogvAdjustSubFONoBreakdown.GetRowCellValue(nRowHandle, "FNQuantityExtra"))
                            End If

                            '...re-calculate amount from extra quantity
                            nFNAmntExtra = nFNPrice * nFNQuantityExtra

                            '...re-calculate grand amount
                            nFNGrandAmnt = nFNAmt + nFNAmntExtra

                            '...update row cell value
                            '------------------------------------------------------------------------
                            '...update FNAmt
                            Me.ogvAdjustSubFONoBreakdown.SetRowCellValue(nRowHandle, "FNAmt", nFNAmt)
                            Me.ogvAdjustSubFONoBreakdown.RefreshRowCell(nRowHandle, oColFNAmt)

                            '...update FNAmntExtra
                            nFNAmntExtra = nFNPrice * nFNQuantityExtra
                            Me.ogvAdjustSubFONoBreakdown.SetRowCellValue(nRowHandle, oColFNAmntExtra, nFNAmntExtra)
                            Me.ogvAdjustSubFONoBreakdown.RefreshRowCell(nRowHandle, oColFNAmntExtra)

                            '...update FNGrandAmnt
                            nFNGrandAmnt = nFNAmt + nFNAmntExtra
                            Me.ogvAdjustSubFONoBreakdown.SetRowCellValue(nRowHandle, oColFNGrandAmnt, nFNGrandAmnt)
                            Me.ogvAdjustSubFONoBreakdown.RefreshRowCell(nRowHandle, oColFNGrandAmnt)
                            '--------------------------------------------------------------------------

                        End If

                    Case "FNExtraQty"

                        If nRowHandle >= 0 Then
                            Dim nFNQuantity As Integer = 0
                            Dim nFNQuantityExtra As Integer = 0
                            Dim nFNGrandQuantity As Integer = 0
                            Dim nFNExtraQty As Double = 0
                            Dim nFNPrice As Double = 0
                            Dim nFNAmt As Double = 0
                            Dim nFNAmntExtra As Double = 0
                            Dim nFNGrandAmnt As Double = 0

                            If Not DBNull.Value.Equals(Me.ogvAdjustSubFONoBreakdown.GetRowCellValue(nRowHandle, "FNQuantity")) Then
                                nFNQuantity = Val(Me.ogvAdjustSubFONoBreakdown.GetRowCellValue(nRowHandle, "FNQuantity"))
                            End If

                            If Not DBNull.Value.Equals(Me.ogvAdjustSubFONoBreakdown.GetRowCellValue(nRowHandle, "FNPrice")) Then
                                nFNPrice = Val(Me.ogvAdjustSubFONoBreakdown.GetRowCellValue(nRowHandle, "FNPrice"))
                            End If

                            nFNAmt = nFNQuantity * nFNPrice

                            If Not DBNull.Value.Equals(Me.ogvAdjustSubFONoBreakdown.GetRowCellValue(nRowHandle, "FNExtraQty")) Then
                                nFNExtraQty = Val(Me.ogvAdjustSubFONoBreakdown.GetRowCellValue(nRowHandle, "FNExtraQty"))
                            End If

                            '...re-calculate quantity extra
                            If nFNQuantity = 0 Then
                                nFNQuantityExtra = 0
                            Else
                                If nFNExtraQty = 0 Then
                                    nFNQuantityExtra = 0
                                Else
                                    Dim nFractionNumber#
                                    nFractionNumber = nFNQuantity * (nFNExtraQty / 100)

                                    Dim tRetExtraQty$ = CStr(nFractionNumber)

                                    Dim nPosDigit% = Microsoft.VisualBasic.InStr(tRetExtraQty, ".")

                                    If nPosDigit = 0 Then
                                        nFNQuantityExtra = CDbl(nFractionNumber)
                                    Else
                                        Dim tFractionInt$ = Microsoft.VisualBasic.Mid$(tRetExtraQty, 1, nPosDigit - 1)
                                        Dim tFractionDecimal$ = Microsoft.VisualBasic.Mid$(tRetExtraQty, nPosDigit + 1, Len(tRetExtraQty) - nPosDigit)

                                        Dim nReal#
                                        Double.TryParse(tFractionDecimal, nReal)

                                        If nReal > 0 Then
                                            Dim nFractionInt#
                                            Double.TryParse(tFractionInt, nFractionInt)
                                            nFNQuantityExtra = CDbl(IIf(nFractionInt < 0, 0, nFractionInt) + 1)
                                        Else
                                            Dim nFractionInt#
                                            Double.TryParse(tFractionInt, nFractionInt)

                                            nFNQuantityExtra = CDbl(IIf(nFractionInt < 0, 0, nFractionInt))
                                        End If

                                    End If

                                End If

                            End If

                            '...re-calculate grand quantity
                            nFNGrandQuantity = nFNQuantity + nFNQuantityExtra

                            '...re-calculate amount from extra quantity
                            nFNAmntExtra = nFNPrice * nFNQuantityExtra

                            '...re-calculate grand amount
                            nFNGrandAmnt = nFNAmt + nFNAmntExtra

                            '...update row cell value
                            '------------------------------------------------------------------------
                            '...update FNQuantityExtra
                            Me.ogvAdjustSubFONoBreakdown.SetRowCellValue(nRowHandle, oColFNQuantityExtra, nFNQuantityExtra)
                            Me.ogvAdjustSubFONoBreakdown.RefreshRowCell(nRowHandle, oColFNQuantityExtra)

                            '...update FNGrandQuantity
                            nFNGrandQuantity = nFNQuantity + nFNQuantityExtra
                            Me.ogvAdjustSubFONoBreakdown.SetRowCellValue(nRowHandle, oColFNGrandQuantity, nFNGrandQuantity)
                            Me.ogvAdjustSubFONoBreakdown.RefreshRowCell(nRowHandle, oColFNGrandQuantity)

                            '...update FNAmntExtra
                            nFNAmntExtra = nFNPrice * nFNQuantityExtra
                            Me.ogvAdjustSubFONoBreakdown.SetRowCellValue(nRowHandle, oColFNAmntExtra, nFNAmntExtra)
                            Me.ogvAdjustSubFONoBreakdown.RefreshRowCell(nRowHandle, oColFNAmntExtra)

                            '...update FNGrandAmnt
                            nFNGrandAmnt = nFNAmt + nFNAmntExtra
                            Me.ogvAdjustSubFONoBreakdown.SetRowCellValue(nRowHandle, oColFNGrandAmnt, nFNGrandAmnt)
                            Me.ogvAdjustSubFONoBreakdown.RefreshRowCell(nRowHandle, oColFNGrandAmnt)
                            '--------------------------------------------------------------------------

                        End If

                End Select

                bReCalculateColValChange = False

            End If

        Catch ex As Exception
            If System.Diagnostics.Debugger.IsAttached = True Then
                MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
            End If
        End Try

    End Sub

    Private Sub ogvAdjustSubFONoBreakdown_ColumnFilterChanged(sender As Object, e As EventArgs) Handles ogvAdjustSubFONoBreakdown.ColumnFilterChanged

        Try

            If FilerData = False Then

                FilerData = True

                For Each GridCol As DevExpress.XtraGrid.Columns.GridColumn In ogvAdjustSubFONoBreakdown.Columns
                    Select Case GridCol.FieldName.ToString
                        Case "FTOrderNo"
                            Try

                                Dim K As String = "" & GridCol.FilterInfo.Value.ToString

                                If K <> "" Then
                                    ogvAdjustFONo.Columns.ColumnByFieldName(GridCol.FieldName).FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[" & GridCol.FieldName & "] LIKE '%" & K & "%'", K, K, ColumnFilterType.Value)
                                    ogvAdjustSubFONo.Columns.ColumnByFieldName(GridCol.FieldName).FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[" & GridCol.FieldName & "] LIKE '%" & K & "%'", K, K, ColumnFilterType.Value)
                                Else
                                    ogvAdjustFONo.Columns.ColumnByFieldName(GridCol.FieldName).ClearFilter()
                                    ogvAdjustSubFONo.Columns.ColumnByFieldName(GridCol.FieldName).ClearFilter()
                                End If

                            Catch ex As Exception
                                ogvAdjustFONo.Columns.ColumnByFieldName(GridCol.FieldName).ClearFilter()
                                ogvAdjustSubFONo.Columns.ColumnByFieldName(GridCol.FieldName).ClearFilter()
                            End Try
                        Case "FTSubOrderNo"
                            Try
                                Dim K As String = GridCol.FilterInfo.Value.ToString

                                If K <> "" Then
                                    ogvAdjustSubFONo.Columns.ColumnByFieldName(GridCol.FieldName).FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[" & GridCol.FieldName & "] LIKE '%" & K & "%'", K, K, ColumnFilterType.Value)
                                Else
                                    ogvAdjustSubFONo.Columns.ColumnByFieldName(GridCol.FieldName).ClearFilter()
                                End If

                            Catch ex As Exception
                                ogvAdjustSubFONo.Columns.ColumnByFieldName(GridCol.FieldName).ClearFilter()
                            End Try

                        Case "FTStyleCode"
                            '...hidden
                        Case "FTBuyCode"
                            '...hidden
                        Case Else
                            '...do nothing
                    End Select

                Next

                FilerData = False

            End If

        Catch ex As Exception
            FilerData = False
        End Try
       
    End Sub

    Private Sub ogvAdjustSubFONoBreakdown_CustomDrawFooterCell(sender As Object, e As FooterCellCustomDrawEventArgs) Handles ogvAdjustSubFONoBreakdown.CustomDrawFooterCell
        Try
            If e.Column.FieldName = "FTOrderNo" Then

                If e.Info.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count Then

                    If Me.ogvAdjustSubFONoBreakdown.DataRowCount > 0 Then

                        If Me.ogvAdjustSubFONoBreakdown.ActiveFilter Is Nothing Or Me.ogvAdjustSubFONoBreakdown.ActiveFilterEnabled = False Or Me.ogvAdjustSubFONoBreakdown.ActiveFilter.Expression = "" Then

                            Dim oDBdvFONoView As New System.Data.DataView
                            oDBdvFONoView = CType(Me.ogvAdjustSubFONoBreakdown.DataSource, System.Data.DataView)

                            If oDBdvFONoView.Count > 0 Then
                                Dim oDBdtFONo As System.Data.DataTable
                                oDBdtFONo = oDBdvFONoView.ToTable()

                                If Not oDBdtFONo Is Nothing And oDBdtFONo.Rows.Count > 0 Then
                                    Dim oListFTOrderNo As List(Of String) = oDBdtFONo.AsEnumerable() _
                                                                             .Select(Function(r) r.Field(Of String)("FTOrderNo")) _
                                                                             .Distinct() _
                                                                             .ToList()

                                    If Not oListFTOrderNo Is Nothing And oListFTOrderNo.Count > 0 Then
                                        e.Info.DisplayText = oListFTOrderNo.Count
                                    Else
                                        e.Info.DisplayText = ""
                                    End If
                                Else
                                    e.Info.DisplayText = ""
                                End If
                            Else
                                e.Info.DisplayText = ""
                            End If

                        Else
                            '...count distinct Factory Order No. by row filter criteria
                            Dim oDBdtFONoCriteria As System.Data.DataTable = CType(Me.ogvAdjustSubFONoBreakdown.GridControl.DataSource, System.Data.DataTable)
                            Dim oDataViewFilterData As New System.Data.DataView(oDBdtFONoCriteria)

                            oDataViewFilterData.RowFilter = DevExpress.Data.Filtering.CriteriaToWhereClauseHelper.GetDataSetWhere(Me.ogvAdjustSubFONoBreakdown.ActiveFilterCriteria)

                            If Not oDataViewFilterData Is Nothing And oDataViewFilterData.Count > 0 Then
                                Dim oDBdtFONoRowFilter As System.Data.DataTable
                                oDBdtFONoRowFilter = oDataViewFilterData.ToTable()

                                If Not oDBdtFONoRowFilter Is Nothing And oDBdtFONoRowFilter.Rows.Count > 0 Then
                                    Dim oListFTOrderNoRowFilter As List(Of String) = oDBdtFONoRowFilter.AsEnumerable() _
                                                                             .Select(Function(r) r.Field(Of String)("FTOrderNo")) _
                                                                             .Distinct() _
                                                                             .ToList()

                                    If Not oListFTOrderNoRowFilter Is Nothing And oListFTOrderNoRowFilter.Count > 0 Then
                                        e.Info.DisplayText = oListFTOrderNoRowFilter.Count
                                    Else
                                        e.Info.DisplayText = ""
                                    End If

                                Else
                                    e.Info.DisplayText = ""
                                End If

                            Else
                                e.Info.DisplayText = ""
                            End If

                        End If

                    Else
                        e.Info.DisplayText = ""
                    End If

                    'e.Handled = True

                End If

            ElseIf e.Column.FieldName.Equals("FTSubOrderNo") Then

                If e.Info.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count Then

                    If Me.ogvAdjustSubFONoBreakdown.DataRowCount > 0 Then

                        If Me.ogvAdjustSubFONoBreakdown.ActiveFilter Is Nothing Or Me.ogvAdjustSubFONoBreakdown.ActiveFilterEnabled = False Or Me.ogvAdjustSubFONoBreakdown.ActiveFilter.Expression = "" Then

                            Dim oDBdvFONoView As New System.Data.DataView
                            oDBdvFONoView = CType(Me.ogvAdjustSubFONoBreakdown.DataSource, System.Data.DataView)

                            If oDBdvFONoView.Count > 0 Then
                                Dim oDBdtFONo As System.Data.DataTable
                                oDBdtFONo = oDBdvFONoView.ToTable()

                                If Not oDBdtFONo Is Nothing And oDBdtFONo.Rows.Count > 0 Then
                                    Dim oListFTSubOrderNo As List(Of String) = oDBdtFONo.AsEnumerable() _
                                                                             .Select(Function(r) r.Field(Of String)("FTSubOrderNo")) _
                                                                             .Distinct() _
                                                                             .ToList()

                                    If Not oListFTSubOrderNo Is Nothing And oListFTSubOrderNo.Count > 0 Then
                                        e.Info.DisplayText = oListFTSubOrderNo.Count
                                    Else
                                        e.Info.DisplayText = ""
                                    End If
                                Else
                                    e.Info.DisplayText = ""
                                End If
                            Else
                                e.Info.DisplayText = ""
                            End If

                        Else
                            '...count distinct Factory Order No. by row filter criteria
                            Dim oDBdtFONoCriteria As System.Data.DataTable = CType(Me.ogvAdjustSubFONoBreakdown.GridControl.DataSource, System.Data.DataTable)
                            Dim oDataViewFilterData As New System.Data.DataView(oDBdtFONoCriteria)

                            oDataViewFilterData.RowFilter = DevExpress.Data.Filtering.CriteriaToWhereClauseHelper.GetDataSetWhere(Me.ogvAdjustSubFONoBreakdown.ActiveFilterCriteria)

                            If Not oDataViewFilterData Is Nothing And oDataViewFilterData.Count > 0 Then
                                Dim oDBdtFONoRowFilter As System.Data.DataTable
                                oDBdtFONoRowFilter = oDataViewFilterData.ToTable()

                                If Not oDBdtFONoRowFilter Is Nothing And oDBdtFONoRowFilter.Rows.Count > 0 Then
                                    Dim oListFTSubOrderNoRowFilter As List(Of String) = oDBdtFONoRowFilter.AsEnumerable() _
                                                                             .Select(Function(r) r.Field(Of String)("FTSubOrderNo")) _
                                                                             .Distinct() _
                                                                             .ToList()

                                    If Not oListFTSubOrderNoRowFilter Is Nothing And oListFTSubOrderNoRowFilter.Count > 0 Then
                                        e.Info.DisplayText = oListFTSubOrderNoRowFilter.Count
                                    Else
                                        e.Info.DisplayText = ""
                                    End If

                                Else
                                    e.Info.DisplayText = ""
                                End If

                            Else
                                e.Info.DisplayText = ""
                            End If

                        End If

                    Else
                        e.Info.DisplayText = ""
                    End If

                    'e.Handled = True

                End If

            End If

        Catch ex As Exception
            If System.Diagnostics.Debugger.IsAttached = True Then
                MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
            End If
        End Try

    End Sub

    Private Sub ogvAdjustSubFONoBreakdown_CustomRowFilter(sender As Object, e As Views.Base.RowFilterEventArgs) Handles ogvAdjustSubFONoBreakdown.CustomRowFilter

    End Sub

    'Private Function FooterCellCustomDrawEventArgs() As Object
    '    Throw New NotImplementedException
    'End Function

    Private Sub ogvAdjustSubFONoBreakdown_MouseDown(sender As Object, e As MouseEventArgs) Handles ogvAdjustSubFONoBreakdown.MouseDown
        Try

            Dim hitInfo = Me.ogvAdjustSubFONoBreakdown.CalcHitInfo(e.Location)

            If hitInfo.InRowCell = True Then
                Dim rowHandle As Integer = hitInfo.RowHandle
                Dim column As DevExpress.XtraGrid.Columns.GridColumn = hitInfo.Column

            Else

            End If

        Catch ex As Exception
        End Try

    End Sub

    Private Sub ogdAdjustFONo_Click(sender As Object, e As EventArgs) Handles ogdAdjustFONo.Click

    End Sub

    Private Sub FNHSysCmpId_EditValueChanged(sender As Object, e As EventArgs) Handles FNHSysCmpId.EditValueChanged

    End Sub

    Private Sub otcFactoryOrderNo_SelectedPageChanging(sender As Object, e As XtraTab.TabPageChangingEventArgs) Handles otcFactoryOrderNo.SelectedPageChanging
        'Select Case e.Page.Name
        '    Case otbSubOrderNo.Name
        '        Try
        '            Dim oDBdv As System.Data.DataView
        '            oDBdv = W_PRCdvGETViewFilterData(Me.ogvAdjustFONo)
        '            If Not oDBdv Is Nothing Then
        '                '...refresh datasource for gridview sub order no and sub order no breakdown
        '                Dim tFTFilterFONoCriteria As String = ""
        '                For Each oRowView As DataRowView In oDBdv
        '                    Dim oDataRow As DataRow = oRowView.Row
        '                    tFTFilterFONoCriteria = tFTFilterFONoCriteria & oDataRow.Item("FTOrderNo").ToString() & "|"
        '                Next

        '                If tFTFilterFONoCriteria <> "" Then
        '                    tFTFilterFONoCriteria = Microsoft.VisualBasic.Mid$(tFTFilterFONoCriteria, 1, Len(tFTFilterFONoCriteria) - 1)
        '                    Call W_PRCbLoadFONoSub(tFTFilterFONoCriteria)
        '                    'Call W_PRCbLoadFONoSubBreakdown(tFTFilterFONoCriteria)
        '                End If

        '            End If
        '        Catch ex As Exception
        '            If System.Diagnostics.Debugger.IsAttached = True Then
        '                MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace.ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
        '            End If
        '        End Try
        '    Case otpSubOrderNoBreakdown.Name

        '        Try
        '            Dim oDBdv As System.Data.DataView
        '            oDBdv = W_PRCdvGETViewFilterData(Me.ogvAdjustSubFONo)
        '            If Not oDBdv Is Nothing Then
        '                '...refresh datasource for gridview sub order no and sub order no breakdown
        '                Dim tFTFilterFONoSubCriteria As String = ""
        '                For Each oRowView As DataRowView In oDBdv
        '                    Dim oDataRow As DataRow = oRowView.Row
        '                    REM 2014/06/14 tFTFilterFONoCriteria = tFTFilterFONoCriteria & oDataRow.Item("FTOrderNo").ToString() & "|"
        '                    tFTFilterFONoSubCriteria = tFTFilterFONoSubCriteria & oDataRow.Item("FTSubOrderNo").ToString() & "|"
        '                Next

        '                If tFTFilterFONoSubCriteria <> "" Then
        '                    tFTFilterFONoSubCriteria = Microsoft.VisualBasic.Mid$(tFTFilterFONoSubCriteria, 1, Len(tFTFilterFONoSubCriteria) - 1)
        '                    Call W_PRCbLoadFONoSubBreakdown("", tFTFilterFONoSubCriteria)
        '                End If

        '            End If
        '        Catch ex As Exception
        '            If System.Diagnostics.Debugger.IsAttached = True Then
        '                MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace.ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
        '            End If
        '        End Try

        'End Select
    End Sub

    Private Sub ogvAdjustSubFONo_CustomDrawFooterCell(sender As Object, e As FooterCellCustomDrawEventArgs) Handles ogvAdjustSubFONo.CustomDrawFooterCell
        Try
            If e.Column.FieldName = "FTOrderNo" Then

                If e.Info.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count Then

                    If Me.ogvAdjustSubFONo.DataRowCount > 0 Then

                        If Me.ogvAdjustSubFONo.ActiveFilter Is Nothing Or Me.ogvAdjustSubFONo.ActiveFilterEnabled = False Or Me.ogvAdjustSubFONo.ActiveFilter.Expression = "" Then

                            Dim oDBdvFONoView As New System.Data.DataView
                            oDBdvFONoView = CType(Me.ogvAdjustSubFONo.DataSource, System.Data.DataView)

                            If oDBdvFONoView.Count > 0 Then
                                Dim oDBdtFONo As System.Data.DataTable
                                oDBdtFONo = oDBdvFONoView.ToTable()

                                If Not oDBdtFONo Is Nothing And oDBdtFONo.Rows.Count > 0 Then
                                    Dim oListFTOrderNo As List(Of String) = oDBdtFONo.AsEnumerable() _
                                                                             .Select(Function(r) r.Field(Of String)("FTOrderNo")) _
                                                                             .Distinct() _
                                                                             .ToList()

                                    If Not oListFTOrderNo Is Nothing And oListFTOrderNo.Count > 0 Then
                                        e.Info.DisplayText = oListFTOrderNo.Count
                                    Else
                                        e.Info.DisplayText = ""
                                    End If
                                Else
                                    e.Info.DisplayText = ""
                                End If
                            Else
                                e.Info.DisplayText = ""
                            End If

                        Else
                            '...count distinct Factory Order No. by row filter criteria
                            Dim oDBdtFONoCriteria As System.Data.DataTable = CType(Me.ogvAdjustSubFONo.GridControl.DataSource, System.Data.DataTable)
                            Dim oDataViewFilterData As New System.Data.DataView(oDBdtFONoCriteria)

                            oDataViewFilterData.RowFilter = DevExpress.Data.Filtering.CriteriaToWhereClauseHelper.GetDataSetWhere(Me.ogvAdjustSubFONo.ActiveFilterCriteria)

                            If Not oDataViewFilterData Is Nothing And oDataViewFilterData.Count > 0 Then
                                Dim oDBdtFONoRowFilter As System.Data.DataTable
                                oDBdtFONoRowFilter = oDataViewFilterData.ToTable()

                                If Not oDBdtFONoRowFilter Is Nothing And oDBdtFONoRowFilter.Rows.Count > 0 Then
                                    Dim oListFTOrderNoRowFilter As List(Of String) = oDBdtFONoRowFilter.AsEnumerable() _
                                                                             .Select(Function(r) r.Field(Of String)("FTOrderNo")) _
                                                                             .Distinct() _
                                                                             .ToList()

                                    If Not oListFTOrderNoRowFilter Is Nothing And oListFTOrderNoRowFilter.Count > 0 Then
                                        e.Info.DisplayText = oListFTOrderNoRowFilter.Count
                                    Else
                                        e.Info.DisplayText = ""
                                    End If

                                Else
                                    e.Info.DisplayText = ""
                                End If

                            Else
                                e.Info.DisplayText = ""
                            End If

                        End If

                    Else
                        e.Info.DisplayText = ""
                    End If

                    'e.Handled = True

                End If

            ElseIf e.Column.FieldName.Equals("FTSubOrderNo") Then
                'e.Info.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count
            End If

        Catch ex As Exception
            If System.Diagnostics.Debugger.IsAttached = True Then
                MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
            End If
        End Try

    End Sub

    Private Sub ogvAdjustSubFONo_ShowingEditor(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles ogvAdjustSubFONo.ShowingEditor
        Try
            Dim view = CType(sender, DevExpress.XtraGrid.Views.Grid.GridView)

            If view.FocusedRowHandle < 0 Then Exit Sub

            'If view.FocusedColumn.FieldName = "FTOrderNo" And view.GetRowCellValue(view.FocusedRowHandle, "FTOrderNo").ToString = "NI1501166" Then
            '    e.Cancel = True
            'End If

            If view.GetRowCellValue(view.FocusedRowHandle, "FTOrderNo").ToString = "NI1501166" Then
                e.Cancel = True
            Else
                e.Cancel = False
            End If

        Catch ex As Exception
            If System.Diagnostics.Debugger.IsAttached = True Then
                MsgBox(ex.Message().ToString & Environment.NewLine & ex.StackTrace().ToString, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, My.Application.Info.Title)
            End If
        End Try
    End Sub

    Private Sub RepositoryItemCheckEdit1_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles RepositoryItemCheckEdit1.EditValueChanging
        Try

            With ogvAdjustFONo

                'Select Case .GetFocusedRowCellValue("FTVenderPramCode").ToString()
                '    Case "HIC", "HTV"
                '        e.Cancel = True
                '    Case Else
                '        e.Cancel = False
                'End Select

            End With

        Catch ex As Exception
            e.Cancel = True
        End Try
    End Sub

    Private Sub FNHSysBuyId_EditValueChanged(sender As Object, e As EventArgs) Handles FNHSysBuyId.EditValueChanged

    End Sub
End Class
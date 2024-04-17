Imports DevExpress.Data
Imports DevExpress.Utils
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraGrid.Views.Grid
Imports Microsoft.Office.Interop
Imports System.ComponentModel
Imports System.Data.SqlClient
Imports System.IO
Imports System.Windows.Forms

Public Class wPurchaseTrackingPI

    Private StateCal As Boolean = False
    Private _RowDataChange As Boolean
    Private GridDataBefore As String = ""
    Private GridDataInvoiceBefore As String = ""
    Private GridDataNoteBefore As String = ""

    Private wAddPI As wPurchaseTrackingPIAddPI
    Private wAddPIPayment As wPurchaseTrackingPIAddPayment
    Private wAddtracking As wPurchaseTrackingPIAddTracking
    Private wAddPaid As wPurchaseTrackingPIAddPaid
    Private wAddPIPreview As wPurchaseTrackingPIPreview
    Private wListPINo As wPurchaseTrackingPIListPI
    Private _FormLoad As Boolean = True
    Private pCountMaxApp As Integer = 0
    Private ds As DataSet

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        wAddPI = New wPurchaseTrackingPIAddPI
        HI.TL.HandlerControl.AddHandlerObj(wAddPI)

        Dim oSysLang As New ST.SysLanguage
        Try
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, wAddPI.Name.ToString.Trim, wAddPI)
        Catch ex As Exception
        Finally
        End Try

        wAddPIPayment = New wPurchaseTrackingPIAddPayment
        HI.TL.HandlerControl.AddHandlerObj(wAddPIPayment)

        Try
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, wAddPIPayment.Name.ToString.Trim, wAddPIPayment)
        Catch ex As Exception
        Finally
        End Try

        wAddtracking = New wPurchaseTrackingPIAddTracking
        HI.TL.HandlerControl.AddHandlerObj(wAddtracking)

        Try
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, wAddtracking.Name.ToString.Trim, wAddtracking)
        Catch ex As Exception
        Finally
        End Try

        wAddPaid = New wPurchaseTrackingPIAddPaid
        HI.TL.HandlerControl.AddHandlerObj(wAddPaid)

        Try
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, wAddPaid.Name.ToString.Trim, wAddPaid)
        Catch ex As Exception
        Finally
        End Try

        wAddPIPreview = New wPurchaseTrackingPIPreview
        HI.TL.HandlerControl.AddHandlerObj(wAddPIPreview)

        Try
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, wAddPIPreview.Name.ToString.Trim, wAddPIPreview)
        Catch ex As Exception
        Finally
        End Try

        wListPINo = New wPurchaseTrackingPIListPI
        '  HI.TL.HandlerControl.AddHandlerObj(wListPINo)

        Try
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, wListPINo.Name.ToString.Trim, wListPINo)
        Catch ex As Exception
        Finally
        End Try

        With RepositoryItemDateEdit1

            RemoveHandler .Leave, AddressOf HI.TL.HandlerControl.RepositoryItemDate_Leave
            RemoveHandler .Click, AddressOf HI.TL.HandlerControl.RepositoryItemDate_GotFocus
            AddHandler .Leave, AddressOf ItemDate_Leave
            AddHandler .Click, AddressOf ItemDate_GotFocus

        End With

        Call InitGrid()

        Dim cmd As String = "Select top 1  FTCfgData from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSESystemConfig  WHERE  (FTCfgName = N'popdfmaxapp') "
        pCountMaxApp = Val(HI.Conn.SQLConn.GetField(cmd, Conn.DB.DataBaseName.DB_SECURITY, "0"))

    End Sub

#Region "Initial Grid"
    Private Sub InitGrid()

        '------Start Add Summary Grid-------------
        'Dim sFieldCount As String = ""
        'Dim sFieldSum As String = "FNPOQuantity|FNRcvQuantity|FNRTsQuantity|FNPOBalQuantity"

        'Dim sFieldGrpCount As String = ""
        'Dim sFieldGrpSum As String = "FNPOQuantity|FNRcvQuantity|FNRTsQuantity|FNPOBalQuantity"

        'Dim sFieldCustomSum As String = ""
        'Dim sFieldCustomGrpSum As String = ""

        'With ogvtime
        '    .ClearGrouping()
        '    .ClearDocument()
        '    '.Columns("FTDateTrans").Group()

        '    For Each Str As String In sFieldCount.Split("|")
        '        If Str <> "" Then
        '            .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Count, Str)
        '            .Columns(Str).SummaryItem.DisplayFormat = "{0:n0}"
        '        End If
        '    Next

        '    For Each Str As String In sFieldCustomSum.Split("|")
        '        If Str <> "" Then
        '            .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Custom, Str)
        '            .Columns(Str).SummaryItem.DisplayFormat = "{0:n0}"
        '        End If
        '    Next

        '    'For Each Str As String In sFieldSum.Split("|")
        '    '    If Str <> "" Then
        '    '        .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Sum, Str)
        '    '        .Columns(Str).SummaryItem.DisplayFormat = "{0:n" & HI.ST.Config.QtyDigit.ToString & "}"
        '    '    End If
        '    'Next

        '    For Each Str As String In sFieldGrpCount.Split("|")
        '        If Str <> "" Then
        '            .GroupSummary.Add(DevExpress.Data.SummaryItemType.Count, Str, Nothing, "(Count by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n0})")
        '        End If
        '    Next

        '    For Each Str As String In sFieldCustomGrpSum.Split("|")
        '        If Str <> "" Then
        '            .GroupSummary.Add(DevExpress.Data.SummaryItemType.Custom, Str, Nothing, "(Sum by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n0})")
        '        End If
        '    Next

        '    For Each Str As String In sFieldGrpSum.Split("|")
        '        If Str <> "" Then
        '            .GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, Str, Nothing, "(Sum by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n" & HI.ST.Config.QtyDigit.ToString & "})")
        '        End If
        '    Next

        '    .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        '    .OptionsView.ShowFooter = True
        '    .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleIfExpanded

        '    .ExpandAllGroups()
        '    .RefreshData()

        'End With
        '------End Add Summary Grid-------------

    End Sub
#End Region

#Region "Custom summaries"

    Private totalSum As Integer = 0
    Private GrpSum As Integer = 0

    Private Sub InitStartValue()
        totalSum = 0
        GrpSum = 0
    End Sub

    'Private Sub ogv_CustomDrawGroupRow(sender As Object, e As DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventArgs) Handles ogv.CustomDrawGroupRow

    '    Dim info As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridGroupRowInfo = e.Info
    '    Dim Handle As Integer = ogv.GetDataRowHandleByGroupRowHandle(e.RowHandle)

    '    'Select Case info.Column.FieldName.ToString.ToUpper
    '    '    Case "FNWorkingDay"

    '    Dim GrpDisplayText As String = ogv.GetGroupSummaryText(e.RowHandle)  'ogv.GetGroupRowValue(e.RowHandle, info.Column)
    '    Dim GrpDisplayTextReplace As String = Nothing
    '    Dim GrpDisplayTextReplaceNew As String = Nothing
    '    GrpDisplayTextReplace = GrpDisplayText.Split(")")(1)

    '    If GrpDisplayTextReplace <> "" Then
    '        If GrpDisplayTextReplace.Split("=").Length >= 2 Then
    '            Dim Title1 As String = GrpDisplayTextReplace.Split("=")(0)
    '            Dim Title2 As String = GrpDisplayTextReplace.Split("=")(1)

    '            If IsNumeric(Title2) = False Then
    '                Title2 = "0"
    '            End If
    '            Dim _Sum As Integer = CDbl(Title2)
    '            Dim NetDisplay As String = ""
    '            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
    '                NetDisplay = Format((_Sum \ 480), "00") & " วัน : " & Format(((_Sum Mod 480) \ 60), "00") & " ชั่วโมง : " & Format(((_Sum Mod 480) Mod 60), "00") & " นาที"
    '            Else
    '                NetDisplay = Format((_Sum \ 480), "00") & " Day : " & Format(((_Sum Mod 480) \ 60), "00") & " Hour : " & Format(((_Sum Mod 480) Mod 60), "00") & " Minute"
    '            End If

    '            GrpDisplayTextReplaceNew = Title1 & "=" & NetDisplay
    '            GrpDisplayText = GrpDisplayText.Replace(GrpDisplayTextReplace, GrpDisplayTextReplaceNew)
    '        End If


    '    info.GroupText = info.Column.Caption + ":" + info.GroupValueText + ""
    '    info.GroupText += "" + GrpDisplayText + ""

    '    'End Select

    'End Sub


    Private Sub Gridview_CustomSummaryCalculate(ByVal sender As Object, ByVal e As DevExpress.Data.CustomSummaryEventArgs) Handles ogvtime.CustomSummaryCalculate
        If e.SummaryProcess = CustomSummaryProcess.Start Then
            InitStartValue()
        End If

        Select Case CType(e.Item, DevExpress.XtraGrid.GridSummaryItem).FieldName.ToString
            Case "FNTime", "FNOT1", "FNOT1_5", "FNOT2", "FNOT3", "FNOT4"
                If e.SummaryProcess = CustomSummaryProcess.Calculate Then

                    If e.FieldValue IsNot Nothing AndAlso e.FieldValue IsNot DBNull.Value Then
                        If e.IsGroupSummary Then
                            Dim Seq As Integer = 1
                            For Each Str As String In e.FieldValue.ToString.Split(":")
                                Select Case Seq
                                    Case 1
                                        GrpSum = GrpSum + (Integer.Parse(Val(Str)) * 60)
                                    Case Else
                                        GrpSum = GrpSum + Integer.Parse(Val(Str))
                                End Select
                                Seq = Seq + 1
                            Next
                        End If

                        If e.IsTotalSummary Then
                            Dim Seq As Integer = 1
                            For Each Str As String In e.FieldValue.ToString.Split(":")
                                Select Case Seq
                                    Case 1
                                        totalSum = totalSum + (Integer.Parse(Val(Str)) * 60)
                                    Case Else
                                        totalSum = totalSum + Integer.Parse(Val(Str))
                                End Select

                                Seq = Seq + 1
                            Next
                        End If

                    End If

                    If e.IsGroupSummary Then
                        Dim GrpDisplay As String = ""
                        GrpDisplay = Format(((GrpSum) \ 60), "00") & " : " & Format(((GrpSum) Mod 60), "00")
                        e.TotalValue = GrpSum
                    End If

                    If e.IsTotalSummary Then
                        Dim NetDisplay As String = ""

                        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                            NetDisplay = Format(((totalSum) \ 60), "00") & " : " & Format(((totalSum) Mod 60), "00")
                        Else
                            NetDisplay = Format(((totalSum) \ 60), "00") & " : " & Format(((totalSum) Mod 60), "00")
                        End If

                        e.TotalValue = NetDisplay ' totalSum 'NetDisplay

                    End If
                End If
        End Select
    End Sub


#End Region

#Region "Property"

    Private _CallMenuName As String = ""
    Public Property CallMenuName As String
        Get
            Return _CallMenuName
        End Get
        Set(value As String)
            _CallMenuName = value
        End Set
    End Property

    Private _CallMethodName As String = ""
    Public Property CallMethodName As String
        Get
            Return _CallMethodName
        End Get
        Set(value As String)
            _CallMethodName = value
        End Set
    End Property

    Private _CallMethodParm As String = ""
    Public Property CallMethodParm As String
        Get
            Return _CallMethodParm
        End Get
        Set(value As String)
            _CallMethodParm = value
        End Set
    End Property

    Private _ActualDate As String = ""
    ReadOnly Property ActualDate As String
        Get
            Return _ActualDate
        End Get
    End Property

    Private _ActualNextDate As String = ""
    ReadOnly Property ActualNextDate As String
        Get
            Return _ActualNextDate
        End Get
    End Property


#End Region

#Region "Procedure"

    Private Sub LoadData()
        Dim _Qry As String = ""
        'Dim _dt As DataTable


        StateCal = False


        Dim _Spls As New HI.TL.SplashScreen("Loading...   Please Wait   ")

        ds = New DataSet()
        Try
            _Qry = " EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.USP_GETDATAPO_TRACKING '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & FNListDocumentTrackPIData.SelectedIndex.ToString() & ",'" & HI.UL.ULDate.ConvertEnDB(FTStartPurchaseDate.Text) & "','" & HI.UL.ULDate.ConvertEnDB(FTEndPurchaseDate.Text) & "','" & HI.UL.ULDate.ConvertEnDB(FTStartDelivery.Text) & "','" & HI.UL.ULDate.ConvertEnDB(FTEndDelivery.Text) & "','',''," & Val(FNHSysSuplId.Properties.Tag.ToString) & ",'" & HI.UL.ULDate.ConvertEnDB(FTStartPayment.Text) & "','" & HI.UL.ULDate.ConvertEnDB(FTEndPayment.Text) & "','" & HI.UL.ULF.rpQuoted(chknewupload.EditValue.ToString) & "',1"

            ' _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PUR)

            HI.Conn.SQLConn.GetDataSet(_Qry, Conn.DB.DataBaseName.DB_PUR, ds)

            ds.Relations.Add("Item_PoNo", ds.Tables(0).Columns("FTPurchaseNo"), ds.Tables(1).Columns("FTPurchaseNo"))

            Me.ogdtime.DataSource = ds.Tables(0)

        Catch ex As Exception
            Me.ogdtime.DataSource = Nothing
        End Try

        ' _dt.Dispose()
        _Spls.Close()

        _RowDataChange = False

    End Sub

    Private Function VerifyData() As Boolean
        Dim _Pass As Boolean = True

        Return _Pass
    End Function

#End Region

#Region "General"

    Private Sub ogv_MasterRowGetRelationDisplayCaption(sender As Object, e As MasterRowGetRelationNameEventArgs) Handles ogvtime.MasterRowGetRelationDisplayCaption
        Dim view As GridView = TryCast(sender, GridView)
        Dim companyName = CStr(view.GetRowCellValue(e.RowHandle, "FTPurchaseNo"))
        If e.RelationIndex = 0 Then e.RelationName = $"{companyName}: P/I Detail "
    End Sub

    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            _FormLoad = False
            HI.UL.AppRegistry.LoadLayoutGridFromRegistry(Me, Me.ogvtime)

            Dim StateShowSelect As Boolean = (ocmsendmail.Enabled OrElse ocmsavepi.Enabled OrElse ocmsavepipayment.Enabled OrElse ocmsavetrack.Enabled OrElse ocmsavepaid.Enabled)
            ochkselectall.Visible = StateShowSelect


            With Me.ogvtime
                .Columns.ColumnByFieldName("FTSelect").Visible = StateShowSelect
                .Columns.ColumnByFieldName("FTSelect").OptionsColumn.ShowInCustomizationForm = StateShowSelect


                If ocmsendmail.Enabled = False Then
                    .Columns.ColumnByFieldName("FTStateSendMail").AppearanceCell.BackColor = Nothing
                End If

                .Columns.ColumnByFieldName("FNPOGrandAmt").Visible = ocmshowamount.Enabled
                .Columns.ColumnByFieldName("FNPOGrandAmt").OptionsColumn.ShowInCustomizationForm = ocmshowamount.Enabled
                .Columns.ColumnByFieldName("FNPIPONetAmt").Visible = ocmshowamount.Enabled
                .Columns.ColumnByFieldName("FNPIPONetAmt").OptionsColumn.ShowInCustomizationForm = ocmshowamount.Enabled

                .Columns.ColumnByFieldName("FNPIGrandNetAmt").Visible = ocmshowamount.Enabled
                .Columns.ColumnByFieldName("FNPIGrandNetAmt").OptionsColumn.ShowInCustomizationForm = ocmshowamount.Enabled

                .Columns.ColumnByFieldName("FNCNAmt").Visible = ocmshowamount.Enabled
                .Columns.ColumnByFieldName("FNCNAmt").OptionsColumn.ShowInCustomizationForm = ocmshowamount.Enabled

                .Columns.ColumnByFieldName("FNDNAmt").Visible = ocmshowamount.Enabled
                .Columns.ColumnByFieldName("FNDNAmt").OptionsColumn.ShowInCustomizationForm = ocmshowamount.Enabled

                .Columns.ColumnByFieldName("FNSurchargeAmt").Visible = ocmshowamount.Enabled
                .Columns.ColumnByFieldName("FNSurchargeAmt").OptionsColumn.ShowInCustomizationForm = ocmshowamount.Enabled

                .Columns.ColumnByFieldName("FNPIGrandTotalAmt").Visible = ocmshowamount.Enabled
                .Columns.ColumnByFieldName("FNPIGrandTotalAmt").OptionsColumn.ShowInCustomizationForm = ocmshowamount.Enabled

                .Columns.ColumnByFieldName("FTStateSendPIToAcc").OptionsColumn.AllowEdit = (Me.ocmFTStateSendPIToAcc.Enabled)

                If Me.ocmFTStateSendPIToAcc.Enabled = False Then
                    .Columns.ColumnByFieldName("FTStateSendPIToAcc").AppearanceCell.BackColor = Nothing
                End If

                .Columns.ColumnByFieldName("FTStateFinishPO").OptionsColumn.AllowEdit = (Me.ocmFTStateFinishPO.Enabled)

                If Me.ocmFTStateFinishPO.Enabled = False Then
                    .Columns.ColumnByFieldName("FTStateFinishPO").AppearanceCell.BackColor = Nothing
                End If


                .Columns.ColumnByFieldName("FTFacCheckCFMDeliveryDate1").OptionsColumn.AllowEdit = (Me.ocmFTFacCheckCFMDeliveryDate1.Enabled)

                If Me.ocmFTFacCheckCFMDeliveryDate1.Enabled = False Then
                    .Columns.ColumnByFieldName("FTFacCheckCFMDeliveryDate1").AppearanceCell.BackColor = Nothing
                End If

                .Columns.ColumnByFieldName("FTFacCheckCFMDeliveryDate2").OptionsColumn.AllowEdit = (Me.ocmFTFacCheckCFMDeliveryDate2.Enabled)

                If Me.ocmFTFacCheckCFMDeliveryDate2.Enabled = False Then
                    .Columns.ColumnByFieldName("FTFacCheckCFMDeliveryDate2").AppearanceCell.BackColor = Nothing
                End If


                .Columns.ColumnByFieldName("FTFacCheckCFMDeliveryDateFinal").OptionsColumn.AllowEdit = (Me.ocmFTFacCheckCFMDeliveryDateFinal.Enabled)

                If Me.ocmFTFacCheckCFMDeliveryDateFinal.Enabled = False Then
                    .Columns.ColumnByFieldName("FTFacCheckCFMDeliveryDateFinal").AppearanceCell.BackColor = Nothing
                End If



                .Columns.ColumnByFieldName("FTWarehouseDate").OptionsColumn.AllowEdit = (Me.ocmFTWarehouseDate.Enabled)

                If Me.ocmFTWarehouseDate.Enabled = False Then
                    .Columns.ColumnByFieldName("FTWarehouseDate").AppearanceCell.BackColor = Nothing
                End If


                .Columns.ColumnByFieldName("FTImpactedGacDate").OptionsColumn.AllowEdit = (Me.ocmFTImpactedGacDate.Enabled)

                If Me.ocmFTImpactedGacDate.Enabled = False Then
                    .Columns.ColumnByFieldName("FTImpactedGacDate").AppearanceCell.BackColor = Nothing
                End If


                .Columns.ColumnByFieldName("FTBaseOnLeadTimeDeliveryDate").OptionsColumn.AllowEdit = (Me.ocmFTBaseOnLeadTimeDeliveryDate.Enabled)

                If Me.ocmFTBaseOnLeadTimeDeliveryDate.Enabled = False Then
                    .Columns.ColumnByFieldName("FTBaseOnLeadTimeDeliveryDate").AppearanceCell.BackColor = Nothing
                End If


                .Columns.ColumnByFieldName("FTNote").OptionsColumn.AllowEdit = (Me.ocmFTNote.Enabled)

                If Me.ocmFTNote.Enabled = False Then
                    .Columns.ColumnByFieldName("FTNote").AppearanceCell.BackColor = Nothing
                End If


                .Columns.ColumnByFieldName("FTInvoiceNo").OptionsColumn.AllowEdit = (Me.ocmFTInvoiceNo.Enabled)

                If Me.ocmFTInvoiceNo.Enabled = False Then
                    .Columns.ColumnByFieldName("FTInvoiceNo").AppearanceCell.BackColor = Nothing
                End If



                .Columns.ColumnByFieldName("FTETD").OptionsColumn.AllowEdit = (Me.ocmFTETD.Enabled)

                If Me.ocmFTETD.Enabled = False Then
                    .Columns.ColumnByFieldName("FTETD").AppearanceCell.BackColor = Nothing
                End If


                .Columns.ColumnByFieldName("FTETA").OptionsColumn.AllowEdit = (Me.ocmFTInvoiceNo.Enabled)

                If Me.ocmFTInvoiceNo.Enabled = False Then
                    .Columns.ColumnByFieldName("FTETA").AppearanceCell.BackColor = Nothing
                End If



                .Columns.ColumnByFieldName("FTStatePaid").OptionsColumn.AllowEdit = (Me.ocmFTStatePaid.Enabled)
                .Columns.ColumnByFieldName("FTPaidNote").OptionsColumn.AllowEdit = (Me.ocmFTStatePaid.Enabled)

                If Me.ocmFTStatePaid.Enabled = False Then
                    .Columns.ColumnByFieldName("FTStatePaid").AppearanceCell.BackColor = Nothing
                    .Columns.ColumnByFieldName("FTPaidNote").AppearanceCell.BackColor = Nothing
                End If



                .OptionsSelection.MultiSelectMode = GridMultiSelectMode.RowSelect
                .OptionsSelection.MultiSelect = False

            End With

            StateCal = False



            Dim Indx As Integer = 0
            Try
                Indx = Val(HI.UL.AppRegistry.ReadRegistry("ListDoc" & Me.Name))
            Catch ex As Exception
            End Try


            FNListDocumentTrackPIData.SelectedIndex = Indx


            Try
                With ogvlist
                    For Each GridCol As DevExpress.XtraGrid.Columns.GridColumn In .Columns
                        GridCol.OptionsColumn.AllowMerge = DefaultBoolean.False
                        GridCol.OptionsColumn.AllowEdit = False
                        GridCol.OptionsColumn.AllowMove = False
                        GridCol.OptionsColumn.ReadOnly = True
                        GridCol.OptionsColumn.AllowSort = DefaultBoolean.False
                    Next

                    .OptionsView.ShowFooter = False

                End With
            Catch ex As Exception

            End Try

        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmload.Click
        If VerifyData() Then
            ogdtime.DataSource = Nothing
            ochkselectall.Checked = False

            Call LoadData()
        End If
    End Sub

    Private Sub ocmclear_Click(sender As System.Object, e As System.EventArgs) Handles ocmclear.Click
        ogdtime.DataSource = Nothing
        ochkselectall.Checked = False

        HI.TL.HandlerControl.ClearControl(Me)
    End Sub

#End Region

    Private Sub ocmsavelayout_Click(sender As Object, e As EventArgs) Handles ocmsavelayout.Click
        HI.UL.AppRegistry.SaveLayoutGridToRegistry(Me, Me.ogvtime)
        HI.MG.ShowMsg.mInfo("Save Layout Grid Complete...", 1404240001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Information)
    End Sub

    Private Sub ogbheader_Click(sender As Object, e As EventArgs) Handles ogbheader.Click

    End Sub

    Private Sub ocmpreview_Click(sender As Object, e As EventArgs)
        With Me.ogvtime
            If .RowCount <= 0 Then Exit Sub
            If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub

            Dim _PoNo As String = "" & .GetFocusedRowCellValue("FTPurchaseNo").ToString
            Dim _FNPoState As Integer = 0

            Dim _Qry As String = ""

            _Qry = "Select TOP 1 FNPoState   "
            _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase AS A WITH(NOLOCK)"
            _Qry &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(_PoNo) & "' "

            _FNPoState = Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PUR, "0")))

            With New HI.RP.Report

                Dim _tmplang As HI.ST.Lang.eLang = HI.ST.Lang.Language

                If _FNPoState = 0 Then
                    HI.ST.Lang.Language = ST.Lang.eLang.TH
                Else
                    HI.ST.Lang.Language = ST.Lang.eLang.EN
                End If

                .FormTitle = Me.Text
                .ReportFolderName = "PurchaseOrder\"
                .ReportName = "PurchaseOrder.rpt"
                .AddParameter("Draft", "DRAFT")
                .Formular = "{TPURTPurchase.FTPurchaseNo}='" & HI.UL.ULF.rpQuoted(_PoNo) & "'"
                .Preview()

                HI.ST.Lang.Language = _tmplang
            End With

        End With
    End Sub

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub hideContainerTop_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub ocmmail_Click(sender As Object, e As EventArgs) Handles ocmsendmail.Click


        'With New wPurchaseTrackingPIMail
        '    .ShowDialog()
        'End With
        Dim _CheckPath As String = "C:\WISDOMPOPDF"

        Try
            If Directory.Exists(_CheckPath) = False Then
                Directory.CreateDirectory(_CheckPath)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
            Exit Sub
        End Try


        Dim pPathPDF As String = ""
        Dim cmdstring As String = ""

        cmdstring = "select top 1 FTCfgData   "
        cmdstring &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSESystemConfig With(NOLOCK) "
        cmdstring &= vbCrLf & "  Where (FTCfgName = N'POPDF')"

        pPathPDF = HI.Conn.SQLConn.GetField(cmdstring, Conn.DB.DataBaseName.DB_SECURITY, "")

        Try

            Dim dtpo As DataTable
            Dim dtpoList As DataTable


            With CType(Me.ogdtime.DataSource, DataTable)
                .AcceptChanges()

                dtpo = .Copy()

            End With


            If pCountMaxApp > 0 Then
                Select Case True
                    Case pCountMaxApp = 1

                        If dtpo.Select("FTSelect='1' AND FNDocType=0  AND FTStateSendApp='1'   ").Length <= 0 Then

                            Exit Sub
                        End If

                        dtpoList = dtpo.Select("FTSelect='1' AND FNDocType=0  AND FTStateSendApp='1'  ").CopyToDataTable

                    Case pCountMaxApp = 2

                        If dtpo.Select("FTSelect='1' AND FNDocType=0  AND  FTStateSuperVisorApp='1' ").Length <= 0 Then

                            Exit Sub
                        End If

                        dtpoList = dtpo.Select("FTSelect='1' AND FNDocType=0  AND FTStateSuperVisorApp='1' ").CopyToDataTable

                    Case Else

                        If dtpo.Select("FTSelect='1' AND FNDocType=0  AND  FTStateManagerApp='1'  ").Length <= 0 Then

                            Exit Sub
                        End If

                        dtpoList = dtpo.Select("FTSelect='1' AND FNDocType=0  AND  FTStateManagerApp='1'   ").CopyToDataTable

                End Select

            Else

                If dtpo.Select("FTSelect='1' AND FTStateSuperVisorApp='1'").Length <= 0 Then

                    Exit Sub
                End If

                dtpoList = dtpo.Select("FTSelect='1'  AND FTStateSuperVisorApp='1' ").CopyToDataTable

            End If



            Dim _FTMail As String = ""
            Dim _FTMailCC As String = ""
            Dim TemplateMail As String = ""
            Dim _Sql As String = ""
            Dim PoNo As String = ""
            Dim PoAllNo As String = ""
            Dim PoState As Integer = 0
            Dim StateFoundPDF As Boolean = False
            Dim Str_Doc_Name As String = ""
            Dim StatePDF As Boolean = False

            Dim grp As List(Of Integer) = (dtpoList.Select("FNHSysSuplId>0", "FNHSysSuplId").CopyToDataTable).AsEnumerable() _
                                                          .Select(Function(r) r.Field(Of Integer)("FNHSysSuplId")) _
                                                          .Distinct() _
                                                          .ToList()


            For Each Ind As Integer In grp
                _FTMail = ""
                _FTMailCC = ""
                TemplateMail = ""
                PoAllNo = ""
                Dim dtsupl As DataTable
                _Sql = "Select TOP 1 FTPOMailTo,FTPOMailCC,FRTemplateMail "
                _Sql &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier With(NOLOCK) "
                _Sql &= vbCrLf & " WHERE FNHSysSuplId=" & Integer.Parse(Val(Ind)) & ""

                dtsupl = HI.Conn.SQLConn.GetDataTable(_Sql, Conn.DB.DataBaseName.DB_MASTER)

                For Each Rmail As DataRow In dtsupl.Rows
                    _FTMail = Rmail!FTPOMailTo.ToString
                    _FTMailCC = Rmail!FTPOMailCC.ToString
                    TemplateMail = Rmail!FRTemplateMail.ToString

                Next
                dtsupl.Dispose()
                cmdstring = ""

                Dim _Spls As New HI.TL.SplashScreen("Creating....Mail Please Wait.")
                Try

                    Dim OutlookMessage As Outlook.MailItem
                    Dim AppOutlook As New Outlook.Application
                    Dim objNS As Outlook._NameSpace = AppOutlook.Session
                    Dim objFolder As Outlook.MAPIFolder

                    Dim oInsp As Outlook.Inspector
                    Dim mySignature As String

                    objFolder = objNS.GetDefaultFolder(Outlook.OlDefaultFolders.olFolderDrafts)

                    Try
                        OutlookMessage = AppOutlook.CreateItem(Outlook.OlItemType.olMailItem)

                        With OutlookMessage


                            Dim _tmplang As HI.ST.Lang.eLang = HI.ST.Lang.Language
                            Try


                                For Each R As DataRow In dtpoList.Select("FNHSysSuplId = " & Val(Ind) & "")
                                    PoNo = R!FTPurchaseNo.ToString
                                    PoState = Val(R!FNPoState.ToString)

                                    If PoAllNo = "" Then

                                        PoAllNo = PoNo
                                    Else
                                        PoAllNo = PoAllNo & "," & PoNo
                                    End If

                                    StateFoundPDF = False
                                    Str_Doc_Name = ""
                                    StatePDF = False

                                    _Sql = "Select TOP 1 '1' AS FTStatePDF "
                                    _Sql &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase With(NOLOCK) "
                                    _Sql &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(PoNo) & "' AND FTStateManagerApp='1' AND FTStatePDF='1'"
                                    StatePDF = HI.Conn.SQLConn.GetField(_Sql, Conn.DB.DataBaseName.DB_PUR, "") = "1"

                                    If pPathPDF <> "" And StatePDF Then

                                        Str_Doc_Name = pPathPDF & "\" & R!FTPurchaseBy.ToString & "\" & PoNo & ".pdf"
                                        If File.Exists(Str_Doc_Name) = True Then
                                            StateFoundPDF = True
                                        Else
                                            Str_Doc_Name = ""
                                        End If

                                    End If

                                    If StateFoundPDF = False Then
                                        With New HI.RP.Report
                                            .FormTitle = "Convert To " & PoNo & ".pdf"
                                            .ReportFolderName = "PurchaseOrder\"  '"Purchase Report\" '
                                            .ReportName = "PurchaseOrder.rpt"
                                            .AddParameter("Draft", "")
                                            .Formular = "{TPURTPurchase.FTPurchaseNo}='" & HI.UL.ULF.rpQuoted(PoNo) & "'"

                                            ' ตรวจสอบ โฟร์เดอร์ก่อน


                                            .PathExport = _CheckPath & ""
                                            '.PathExport = "\\hisoft_svr\HI SOFT SYSTEM\PO PDF\" & Temp_FTPurchaseBy & "\"
                                            .ExportName = PoNo
                                            .ExportFile = HI.RP.Report.ExFile.PDF

                                            ' กรณีหาไฟล์ไม่เจอ  ????
                                            .PrevieNoSplash(PoState)

                                            Dim _FIleExportPDFName As String = .ExportFileSuccessName

                                            Str_Doc_Name = _CheckPath & "\" & PoNo & ".pdf"
                                        End With


                                    End If

                                    Try

                                        If Str_Doc_Name <> "" Then
                                            If File.Exists(Str_Doc_Name) = True Then

                                                If StatePDF = False Then
                                                    HI.PO.POPDFToDB.SaveFilePDF(PoNo, Str_Doc_Name)
                                                End If

                                                .Attachments.Add(Str_Doc_Name)
                                            End If

                                        End If

                                    Catch ex As Exception

                                    End Try



                                Next



                            Catch ex As Exception
                            End Try

                            '.To = _FTMail
                            '.CC = _FTMailCC
                            '.Subject = PoAllNo

                            'If TemplateMail <> "" Then
                            '    .HTMLBody = TemplateMail.Replace("{0}", PoAllNo)
                            'Else
                            '    .Body = "PO Ref No.  " & PoAllNo
                            'End If


                            .Display()
                            .To = _FTMail
                            .CC = _FTMailCC
                            .Subject = PoAllNo

                            oInsp = .GetInspector
                            mySignature = .HTMLBody

                            If TemplateMail <> "" Then

                                Try
                                    .HTMLBody = TemplateMail.Replace("{0}", PoAllNo) & mySignature
                                Catch ex As Exception

                                    .HTMLBody = "<p>" & "PO Ref No.  " & PoAllNo & "</p>" & mySignature
                                    '.Body = "PO Ref No.  " & PoAllNo & .Body
                                End Try


                            Else
                                '.Body = "PO Ref No.  " & PoAllNo & .Body
                                .HTMLBody = "<p>" & "PO Ref No.  " & PoAllNo & "</p>" & mySignature
                            End If



                            _Spls.Close()
                            ' .Display(True)


                            Try
                                ' .Send()


                                cmdstring = "Update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase SET FTStateSendMail='1'"
                                cmdstring &= vbCrLf & ",FTSendMailBy= CASE WHEN ISNULL(FTSendMailBy,'') ='' THEN  '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'  ELSE FTSendMailBy END"
                                cmdstring &= vbCrLf & ",FTSendMailDate=CASE WHEN ISNULL(FTSendMailBy,'') ='' THEN  " & HI.UL.ULDate.FormatDateDB & "  ELSE FTSendMailDate END "
                                cmdstring &= vbCrLf & ",FTSendMailTime=CASE WHEN ISNULL(FTSendMailBy,'') ='' THEN  " & HI.UL.ULDate.FormatTimeDB & "  ELSE FTSendMailTime END "
                                cmdstring &= vbCrLf & ",FTLastMailBy= CASE WHEN ISNULL(FTSendMailBy,'') <>'' THEN  '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' ELSE FTLastMailBy  END "
                                cmdstring &= vbCrLf & ",FTLastMailDate=CASE WHEN ISNULL(FTSendMailBy,'') <>'' THEN   " & HI.UL.ULDate.FormatDateDB & "  ELSE FTLastMailDate  END  "
                                cmdstring &= vbCrLf & ",FTLastMailTime=CASE WHEN ISNULL(FTSendMailBy,'') <>'' THEN   " & HI.UL.ULDate.FormatTimeDB & "  ELSE FTLastMailTime  END  "
                                cmdstring &= vbCrLf & ",FTSystemSendMailBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                                cmdstring &= vbCrLf & ",FTSystemSendMailDate=" & HI.UL.ULDate.FormatDateDB & " "
                                cmdstring &= vbCrLf & ",FTSystemSendMailTime=" & HI.UL.ULDate.FormatTimeDB & " "
                                cmdstring &= vbCrLf & "  ,FTStateHold='0' "
                                cmdstring &= vbCrLf & "  ,FNHSysPOHoldId=0 "
                                cmdstring &= vbCrLf & "  WHERE FTPurchaseNo IN ('" & PoAllNo.Replace(",", "','") & "')"
                                cmdstring &= vbCrLf & " select FTPurchaseNo,FTStateSendMail,FTSendMailBy,CASE WHEN ISDATE(FTSendMailDate) = 1 THEN Convert(varchar(10),Convert(datetime,FTSendMailDate),103) ELSE '' END AS  FTSendMailDate,FTSendMailTime "
                                cmdstring &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase   "
                                cmdstring &= vbCrLf & "  WHERE FTPurchaseNo IN ('" & PoAllNo.Replace(",", "','") & "')"

                                Dim dt As DataTable = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_PUR)

                                For Each R As DataRow In dt.Rows

                                    'ogvtime.SetRowCellValue(ogvtime.FocusedRowHandle, "FTStateSendMail", R!FTStateSendMail.ToString)
                                    'ogvtime.SetRowCellValue(ogvtime.FocusedRowHandle, "FTSendMailBy", R!FTSendMailBy.ToString)
                                    'ogvtime.SetRowCellValue(ogvtime.FocusedRowHandle, "FTSendMailDate", R!FTSendMailDate.ToString)
                                    'ogvtime.SetRowCellValue(ogvtime.FocusedRowHandle, "FTSendMailTime", R!FTSendMailTime.ToString)



                                    With CType(Me.ogdtime.DataSource, DataTable)
                                        .AcceptChanges()

                                        For Each Rxp As DataRow In .Select("FTPurchaseNo='" & HI.UL.ULF.rpQuoted(R!FTPurchaseNo.ToString) & "'")
                                            Rxp!FTSelect = "0"

                                            Rxp!FTStateSendMail = R!FTStateSendMail.ToString
                                            Rxp!FTSendMailBy = R!FTSendMailBy.ToString
                                            Rxp!FTSendMailDate = R!FTSendMailDate.ToString
                                            Rxp!FTSendMailTime = R!FTSendMailTime.ToString
                                        Next

                                        .AcceptChanges()

                                    End With

                                    Try
                                        cmdstring = " EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.USP_SENDDATAPO_FORVENDER '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(R!FTPurchaseNo.ToString) & "'"
                                        HI.Conn.SQLConn.ExecuteOnly(cmdstring, Conn.DB.DataBaseName.DB_PUR)

                                    Catch ex As Exception

                                    End Try
                                Next

                                dt.Dispose()




                            Catch ex As Exception

                            Finally

                            End Try

                        End With





                    Catch ex As Exception
                        _Spls.Close()
                        HI.MG.ShowMsg.mInfo("เนื่องจากพบข้อผิดพลาดบางประการ ระบบจึงไม่สามารถทำการส่งเมลล์ได้ !!!", 1408280001, Me.Text, , MessageBoxIcon.Warning)
                    Finally
                        OutlookMessage = Nothing
                        AppOutlook = Nothing
                    End Try



                Catch ex As Exception
                    _Spls.Close()
                    HI.MG.ShowMsg.mInfo("ไม่พบ Microsoft Outlook ไม่สามารถทำการส่งเมลล์ได้ !!!", 1408280002, Me.Text, , MessageBoxIcon.Warning)
                End Try


                For Each R As DataRow In dtpoList.Select("FNHSysSuplId = " & Val(Ind) & "")
                    PoNo = R!FTPurchaseNo.ToString

                    Try
                        If File.Exists(_CheckPath & "\" & PoNo & ".pdf") = True Then
                            File.Delete(_CheckPath & "\" & PoNo & ".pdf")
                        End If
                    Catch ex As Exception
                    End Try

                Next

            Next

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub ItemDate_Leave(sender As Object, e As System.EventArgs)
        Try
            With CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView)

                Dim _TDate As String

                Try

                    _TDate = HI.UL.ULDate.ConvertEnDB(CType(sender, DevExpress.XtraEditors.DateEdit).DateTime)

                    If _TDate = "0001/01/01" Then
                        _TDate = ""
                    End If

                Catch ex As Exception
                    _TDate = ""
                End Try

                CType(sender, DevExpress.XtraEditors.DateEdit).Text = _TDate

                Try
                    If _TDate <> "" Then
                        CType(sender, DevExpress.XtraEditors.DateEdit).DateTime = _TDate
                    Else
                        CType(sender, DevExpress.XtraEditors.DateEdit).DateTime = Nothing
                    End If

                Catch ex As Exception
                End Try

                If _TDate = "" Then
                    .SetRowCellValue(.FocusedRowHandle, .FocusedColumn.FieldName.ToString, "")
                Else
                    .SetRowCellValue(.FocusedRowHandle, .FocusedColumn.FieldName.ToString, HI.UL.ULDate.ConvertEN(_TDate))
                End If

                Dim NewData As String = HI.UL.ULDate.ConvertEN(_TDate)
                If NewData <> GridDataBefore Then
                    Dim OrderNo As String = .GetRowCellValue(.FocusedRowHandle, "FTPurchaseNo").ToString()

                    Dim FieldName As String = .FocusedColumn.FieldName.ToString

                    Dim cmdstring As String = ""

                    cmdstring = "update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase  Set "

                    Select Case FieldName
                        Case "FTFacCheckCFMDeliveryDate1"

                            cmdstring &= vbCrLf & " FTFacCheckCFMDeliveryDate1='" & HI.UL.ULDate.ConvertEnDB(NewData) & "'"
                            cmdstring &= vbCrLf & ",FTFacCheckCFMDeliveryDate1By='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                            cmdstring &= vbCrLf & ",FTFacCheckCFMDeliveryDate1Date=" & HI.UL.ULDate.FormatDateDB & ""
                            cmdstring &= vbCrLf & ",FTFacCheckCFMDeliveryDate1Time=" & HI.UL.ULDate.FormatTimeDB & " "
                        Case "FTFacCheckCFMDeliveryDate2"

                            cmdstring &= vbCrLf & " FTFacCheckCFMDeliveryDate2='" & HI.UL.ULDate.ConvertEnDB(NewData) & "'"
                            cmdstring &= vbCrLf & ",FTFacCheckCFMDeliveryDate2By='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                            cmdstring &= vbCrLf & ",FTFacCheckCFMDeliveryDate2Date=" & HI.UL.ULDate.FormatDateDB & ""
                            cmdstring &= vbCrLf & ",FTFacCheckCFMDeliveryDate2Time=" & HI.UL.ULDate.FormatTimeDB & " "
                        Case "FTFacCheckCFMDeliveryDateFinal"

                            cmdstring &= vbCrLf & " FTFacCheckCFMDeliveryDateFinal='" & HI.UL.ULDate.ConvertEnDB(NewData) & "'"
                            cmdstring &= vbCrLf & ",FTFacCheckCFMDeliveryDateFinalBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                            cmdstring &= vbCrLf & ",FTFacCheckCFMDeliveryDateFinalDate=" & HI.UL.ULDate.FormatDateDB & ""
                            cmdstring &= vbCrLf & ",FTFacCheckCFMDeliveryDateFinalTime=" & HI.UL.ULDate.FormatTimeDB & " "
                        Case "FTWarehouseDate"

                            cmdstring &= vbCrLf & " FTWarehouseDate='" & HI.UL.ULDate.ConvertEnDB(NewData) & "'"
                            cmdstring &= vbCrLf & ",FTWarehouseDateBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                            cmdstring &= vbCrLf & ",FTWarehouseDateDate=" & HI.UL.ULDate.FormatDateDB & ""
                            cmdstring &= vbCrLf & ",FTWarehouseDateTime=" & HI.UL.ULDate.FormatTimeDB & " "
                        Case "FTImpactedGacDate"

                            cmdstring &= vbCrLf & " FTImpactedGacDate='" & HI.UL.ULDate.ConvertEnDB(NewData) & "'"
                            cmdstring &= vbCrLf & ",FTImpactedGacDatey='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                            cmdstring &= vbCrLf & ",FTImpactedGacDateDate=" & HI.UL.ULDate.FormatDateDB & ""
                            cmdstring &= vbCrLf & ",FTImpactedGacDateTime=" & HI.UL.ULDate.FormatTimeDB & " "

                        Case "FTBaseOnLeadTimeDeliveryDate"

                            cmdstring &= vbCrLf & " FTBaseOnLeadTimeDeliveryDate='" & HI.UL.ULDate.ConvertEnDB(NewData) & "'"
                            cmdstring &= vbCrLf & ",FTBaseOnLeadTimeDeliveryDateBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                            cmdstring &= vbCrLf & ",FTBaseOnLeadTimeDeliveryDateDate=" & HI.UL.ULDate.FormatDateDB & ""
                            cmdstring &= vbCrLf & ",FTBaseOnLeadTimeDeliveryDateTime=" & HI.UL.ULDate.FormatTimeDB & " "

                        Case "FTETD"

                            cmdstring &= vbCrLf & " FTETD='" & HI.UL.ULDate.ConvertEnDB(NewData) & "'"
                            cmdstring &= vbCrLf & ",FTETDBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                            cmdstring &= vbCrLf & ",FTETDDate=" & HI.UL.ULDate.FormatDateDB & ""
                            cmdstring &= vbCrLf & ",FTETDTime=" & HI.UL.ULDate.FormatTimeDB & " "
                        Case "FTETA"

                            cmdstring &= vbCrLf & " FTETA='" & HI.UL.ULDate.ConvertEnDB(NewData) & "'"
                            cmdstring &= vbCrLf & ",FTETABy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                            cmdstring &= vbCrLf & ",FTETADate=" & HI.UL.ULDate.FormatDateDB & ""
                            cmdstring &= vbCrLf & ",FTETATime=" & HI.UL.ULDate.FormatTimeDB & " "




                    End Select


                    cmdstring &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(OrderNo) & "'  "

                    If HI.Conn.SQLConn.ExecuteNonQuery(cmdstring, Conn.DB.DataBaseName.DB_PUR) Then
                        'Select Case FieldName
                        '    Case "FTFacCheckCFMDeliveryDate1"

                        '        cmdstring &= vbCrLf & " FTFacCheckCFMDeliveryDate1='" & HI.UL.ULDate.ConvertEnDB(NewData) & "'"
                        '        cmdstring &= vbCrLf & ",FTFacCheckCFMDeliveryDate1By='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        '        cmdstring &= vbCrLf & ",FTFacCheckCFMDeliveryDate1Date=" & HI.UL.ULDate.FormatDateDB & ""
                        '        cmdstring &= vbCrLf & ",FTFacCheckCFMDeliveryDate1Time=" & HI.UL.ULDate.FormatTimeDB & " "

                        '        .SetRowCellValue(.FocusedRowHandle, "FTFacCheckCFMDeliveryDate1", HI.UL.ULDate.ConvertEN(_TDate))

                        '    Case "FTFacCheckCFMDeliveryDate2"

                        '        cmdstring &= vbCrLf & " FTFacCheckCFMDeliveryDate2='" & HI.UL.ULDate.ConvertEnDB(NewData) & "'"
                        '        cmdstring &= vbCrLf & ",FTFacCheckCFMDeliveryDate2By='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        '        cmdstring &= vbCrLf & ",FTFacCheckCFMDeliveryDate2Date=" & HI.UL.ULDate.FormatDateDB & ""
                        '        cmdstring &= vbCrLf & ",FTFacCheckCFMDeliveryDate2Time=" & HI.UL.ULDate.FormatTimeDB & " "
                        '    Case "FTFacCheckCFMDeliveryDateFinal"

                        '        cmdstring &= vbCrLf & " FTFacCheckCFMDeliveryDateFinal='" & HI.UL.ULDate.ConvertEnDB(NewData) & "'"
                        '        cmdstring &= vbCrLf & ",FTFacCheckCFMDeliveryDateFinalBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        '        cmdstring &= vbCrLf & ",FTFacCheckCFMDeliveryDateFinalDate=" & HI.UL.ULDate.FormatDateDB & ""
                        '        cmdstring &= vbCrLf & ",FTFacCheckCFMDeliveryDateFinalTime=" & HI.UL.ULDate.FormatTimeDB & " "
                        '    Case "FTWarehouseDate"

                        '        cmdstring &= vbCrLf & " FTWarehouseDate='" & HI.UL.ULDate.ConvertEnDB(NewData) & "'"
                        '        cmdstring &= vbCrLf & ",FTWarehouseDateBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        '        cmdstring &= vbCrLf & ",FTWarehouseDateDate=" & HI.UL.ULDate.FormatDateDB & ""
                        '        cmdstring &= vbCrLf & ",FTWarehouseDateTime=" & HI.UL.ULDate.FormatTimeDB & " "
                        '    Case "FTImpactedGacDate"

                        '        cmdstring &= vbCrLf & " FTImpactedGacDate='" & HI.UL.ULDate.ConvertEnDB(NewData) & "'"
                        '        cmdstring &= vbCrLf & ",FTImpactedGacDatey='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        '        cmdstring &= vbCrLf & ",FTImpactedGacDateDate=" & HI.UL.ULDate.FormatDateDB & ""
                        '        cmdstring &= vbCrLf & ",FTImpactedGacDateTime=" & HI.UL.ULDate.FormatTimeDB & " "

                        '    Case "FTBaseOnLeadTimeDeliveryDate"

                        '        cmdstring &= vbCrLf & " FTBaseOnLeadTimeDeliveryDate='" & HI.UL.ULDate.ConvertEnDB(NewData) & "'"
                        '        cmdstring &= vbCrLf & ",FTBaseOnLeadTimeDeliveryDateBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        '        cmdstring &= vbCrLf & ",FTBaseOnLeadTimeDeliveryDateDate=" & HI.UL.ULDate.FormatDateDB & ""
                        '        cmdstring &= vbCrLf & ",FTBaseOnLeadTimeDeliveryDateTime=" & HI.UL.ULDate.FormatTimeDB & " "


                        'End Select
                    End If

                End If



            End With

        Catch ex As Exception
        End Try
    End Sub

    Private Sub ItemDate_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            With CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView)

                Dim _TDate As String = HI.UL.ULDate.ConvertEnDB(.GetFocusedRowCellValue(.FocusedColumn))
                If _TDate = "" Then
                    Beep()
                End If
                Try
                    CType(sender, DevExpress.XtraEditors.DateEdit).DateTime = _TDate
                Catch ex As Exception
                    CType(sender, DevExpress.XtraEditors.DateEdit).Text = ""
                End Try

                GridDataBefore = _TDate
            End With



        Catch ex As Exception

        End Try
    End Sub

    Private Sub RepCheckEdit_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles RepCheckEdit.EditValueChanging
        Select Case ogvtime.FocusedColumn.FieldName.ToString
            Case "FTStateFinishPO"

                Dim PoNo As String = ogvtime.GetRowCellValue(ogvtime.FocusedRowHandle, "FTPurchaseNo").ToString()

                Dim FieldName As String = ogvtime.FocusedColumn.FieldName.ToString
                Dim State As String = "0"
                If e.NewValue.ToString = "1" Then
                    State = "1"
                End If
                ogvtime.SetRowCellValue(ogvtime.FocusedRowHandle, ogvtime.FocusedColumn.FieldName.ToString, State)


                Dim cmdstring As String = ""

                cmdstring = "update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase  Set "
                cmdstring &= vbCrLf & " FTStateFinishPO='" & HI.UL.ULF.rpQuoted(State) & "'"
                cmdstring &= vbCrLf & ",FTSendFinishPOBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                cmdstring &= vbCrLf & ",FTSendFinishPODate=" & HI.UL.ULDate.FormatDateDB & ""
                cmdstring &= vbCrLf & ",FTSendFinishPOTime=" & HI.UL.ULDate.FormatTimeDB & " "
                cmdstring &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(PoNo) & "'  "
                cmdstring &= vbCrLf & " select top 1 FTStateFinishPO,FTSendFinishPOBy,CASE WHEN ISDATE(FTSendFinishPODate) = 1 THEN Convert(varchar(10),Convert(datetime,FTSendFinishPODate),103) ELSE '' END AS  FTSendFinishPODate,FTSendFinishPOTime "
                cmdstring &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase   "
                cmdstring &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(PoNo) & "'  "

                Dim dt As DataTable = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_PUR)

                For Each R As DataRow In dt.Rows

                    ogvtime.SetRowCellValue(ogvtime.FocusedRowHandle, "FTStateFinishPO", R!FTStateFinishPO.ToString)
                    ogvtime.SetRowCellValue(ogvtime.FocusedRowHandle, "FTSendFinishPOBy", R!FTSendFinishPOBy.ToString)
                    ogvtime.SetRowCellValue(ogvtime.FocusedRowHandle, "FTSendFinishPODate", R!FTSendFinishPODate.ToString)
                    ogvtime.SetRowCellValue(ogvtime.FocusedRowHandle, "FTSendFinishPOTime", R!FTSendFinishPOTime.ToString)

                    Exit For
                Next

                dt.Dispose()

            Case "FTStateSendPIToAcc"


                Dim PoNo As String = ogvtime.GetRowCellValue(ogvtime.FocusedRowHandle, "FTPurchaseNo").ToString()

                Dim FieldName As String = ogvtime.FocusedColumn.FieldName.ToString
                Dim State As String = "0"
                If e.NewValue.ToString = "1" Then
                    State = "1"
                End If
                ogvtime.SetRowCellValue(ogvtime.FocusedRowHandle, ogvtime.FocusedColumn.FieldName.ToString, State)


                Dim cmdstring As String = ""

                cmdstring = "update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase  Set "
                cmdstring &= vbCrLf & " FTStateSendPIToAcc='" & HI.UL.ULF.rpQuoted(State) & "'"
                cmdstring &= vbCrLf & ",FTSendPIToAccBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                cmdstring &= vbCrLf & ",FTSendPIToAccDate=" & HI.UL.ULDate.FormatDateDB & ""
                cmdstring &= vbCrLf & ",FTSendPIToAccTime=" & HI.UL.ULDate.FormatTimeDB & " "
                cmdstring &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(PoNo) & "'  "
                cmdstring &= vbCrLf & " select top 1 FTStateSendPIToAcc,FTSendPIToAccBy,CASE WHEN ISDATE(FTSendPIToAccDate) = 1 THEN Convert(varchar(10),Convert(datetime,FTSendPIToAccDate),103) ELSE '' END AS FTSendPIToAccDate,FTSendPIToAccTime "
                cmdstring &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase   "
                cmdstring &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(PoNo) & "'  "

                Dim dt As DataTable = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_PUR)

                For Each R As DataRow In dt.Rows

                    ogvtime.SetRowCellValue(ogvtime.FocusedRowHandle, "FTStateSendPIToAcc", R!FTStateSendPIToAcc.ToString)
                    ogvtime.SetRowCellValue(ogvtime.FocusedRowHandle, "FTSendPIToAccBy", R!FTSendPIToAccBy.ToString)
                    ogvtime.SetRowCellValue(ogvtime.FocusedRowHandle, "FTSendPIToAccDate", R!FTSendPIToAccDate.ToString)
                    ogvtime.SetRowCellValue(ogvtime.FocusedRowHandle, "FTSendPIToAccTime", R!FTSendPIToAccTime.ToString)

                    Exit For
                Next

                dt.Dispose()

            Case "FTStateSendMail"


                Dim PoNo As String = ogvtime.GetRowCellValue(ogvtime.FocusedRowHandle, "FTPurchaseNo").ToString()

                Dim FieldName As String = ogvtime.FocusedColumn.FieldName.ToString
                Dim State As String = "0"
                If e.NewValue.ToString = "1" Then
                    State = "1"
                End If

                ogvtime.SetRowCellValue(ogvtime.FocusedRowHandle, ogvtime.FocusedColumn.FieldName.ToString, State)


                Dim cmdstring As String = ""



                cmdstring = "update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase  Set "
                cmdstring &= vbCrLf & ",FTSendMailBy= CASE WHEN ISNULL(FTSendMailBy,'') ='' THEN  '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'  ELSE FTSendMailBy END"
                cmdstring &= vbCrLf & ",FTSendMailDate=CASE WHEN ISNULL(FTSendMailBy,'') ='' THEN  " & HI.UL.ULDate.FormatDateDB & "  ELSE FTSendMailDate END "
                cmdstring &= vbCrLf & ",FTSendMailTime=CASE WHEN ISNULL(FTSendMailBy,'') ='' THEN  " & HI.UL.ULDate.FormatTimeDB & "  ELSE FTSendMailTime END "
                cmdstring &= vbCrLf & ",FTLastMailBy= CASE WHEN ISNULL(FTSendMailBy,'') <>'' THEN  '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' ELSE FTLastMailBy  END "
                cmdstring &= vbCrLf & ",FTLastMailDate=CASE WHEN ISNULL(FTSendMailBy,'') <>'' THEN   " & HI.UL.ULDate.FormatDateDB & "  ELSE FTLastMailDate  END  "
                cmdstring &= vbCrLf & ",FTLastMailTime=CASE WHEN ISNULL(FTSendMailBy,'') <>'' THEN   " & HI.UL.ULDate.FormatTimeDB & "  ELSE FTLastMailTime  END  "
                cmdstring &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(PoNo) & "'  "
                cmdstring &= vbCrLf & " select top 1 FTStateSendMail,FTSendMailBy,CASE WHEN ISDATE(FTSendMailDate) = 1 THEN Convert(varchar(10),Convert(datetime,FTSendMailDate),103) ELSE '' END AS FTSendMailDate,FTSendMailTime "
                cmdstring &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase   "
                cmdstring &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(PoNo) & "'  "

                Dim dt As DataTable = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_PUR)

                For Each R As DataRow In dt.Rows

                    ogvtime.SetRowCellValue(ogvtime.FocusedRowHandle, "FTStateSendMail", R!FTStateSendMail.ToString)
                    ogvtime.SetRowCellValue(ogvtime.FocusedRowHandle, "FTSendMailBy", R!FTSendMailBy.ToString)
                    ogvtime.SetRowCellValue(ogvtime.FocusedRowHandle, "FTSendMailDate", R!FTSendMailDate.ToString)
                    ogvtime.SetRowCellValue(ogvtime.FocusedRowHandle, "FTSendMailTime", R!FTSendMailTime.ToString)

                    Exit For
                Next

                dt.Dispose()

                If State = "1" Then
                    cmdstring = " EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.USP_SENDDATAPO_FORVENDER '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(PoNo) & "'"
                    HI.Conn.SQLConn.ExecuteOnly(cmdstring, Conn.DB.DataBaseName.DB_PUR)
                End If



        End Select
    End Sub

    Private Sub ocmsavepi_Click(sender As Object, e As EventArgs) Handles ocmsavepi.Click
        Dim dtpo As DataTable
        Dim dtpoList As DataTable
        Dim dtpoListPI As DataTable

        With CType(Me.ogdtime.DataSource, DataTable)
            .AcceptChanges()

            dtpo = .Copy()

        End With

        'If dtpo.Select("FTSelect='1' AND FTStateSuperVisorApp='1' AND FTStateManagerApp='1'").Length <= 0 Then

        '    Exit Sub
        'End If

        'dtpoList = dtpo.Select("FTSelect='1'  AND FTStateSuperVisorApp='1'  AND FTStateManagerApp='1' ").CopyToDataTable

        If dtpo.Select("FTSelect='1' AND FNPOBalGrandAmt >0").Length <= 0 Then

            Exit Sub
        End If


        dtpoList = dtpo.Select("FTSelect='1'  AND FTStateSuperVisorApp='1' ").CopyToDataTable



        Dim grp As List(Of Integer) = (dtpoList.Select("FNHSysSuplId>0", "FNHSysSuplId").CopyToDataTable).AsEnumerable() _
                                                      .Select(Function(r) r.Field(Of Integer)("FNHSysSuplId")) _
                                                      .Distinct() _
                                                      .ToList()

        Dim mSuplID As Integer = 0

        For Each Ind As Integer In grp
            mSuplID = Ind

            Exit For
        Next

        dtpoList = dtpo.Select("FTSelect='1' AND FNPOBalGrandAmt >0  AND FNHSysSuplId=" & mSuplID & " ").CopyToDataTable


        'Dim _FTMail As String = ""
        'Dim _Sql As String = ""
        'Dim PoNo As String = ""
        'Dim PoAllNo As String = ""
        'Dim PoState As Integer = 0

        'Dim grp As List(Of String) = (dtpoList.Select("FNHSysSuplId>0", "FNHSysSuplId").CopyToDataTable).AsEnumerable() _
        '                                              .Select(Function(r) r.Field(Of String)("FNHSysSuplId")) _
        '                                              .Distinct() _
        '                                              .ToList()


        'For Each Ind As String In grp

        '    PoAllNo = ""

        '    _Sql = "Select TOP 1 FTMail "
        '    _Sql &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier With(NOLOCK) "
        '    _Sql &= vbCrLf & " WHERE FNHSysSuplId=" & Integer.Parse(Val(Ind)) & ""
        '    _FTMail = HI.Conn.SQLConn.GetField(_Sql, Conn.DB.DataBaseName.DB_MASTER, "")


        '    Dim _Spls As New HI.TL.SplashScreen("Creating....Mail Please Wait.")

        '    For Each R As DataRow In dtpoList.Select("FNHSysSuplId = " & Val(Ind) & "")
        '        PoNo = R!FTPurchaseNo.ToString
        '        PoState = Val(R!FNPoState.ToString)


        '        If PoAllNo = "" Then

        '            PoAllNo = PoNo
        '        Else
        '            PoAllNo = PoAllNo & "," & PoNo
        '        End If


        '    Next


        '    _Spls.Close()


        'Next

        Dim SuplCode As String = ""
        Dim CurCode As String = ""

        For Each R As DataRow In dtpoList.Rows
            SuplCode = R!FTSuplCode.ToString
            CurCode = R!FTCurCode.ToString

            Exit For
        Next

        If mSuplID = 0 Or SuplCode = "" Then
            Exit Sub
        End If

        HI.TL.HandlerControl.ClearControl(wAddPI)
        With wAddPI
            .PINO = ""
            .FNPIDocType.SelectedIndex = 0
            .AddMat = False
            .FTPINo.Enabled = True
            .FTPINo.ReadOnly = False
            .ocmadd.Enabled = True
            .ocmcancel.Enabled = True
            .FNHSysSuplId.Text = SuplCode
            .FNHSysCurId.Text = CurCode
            .ocmviewpdf.Enabled = True
            .ocmviewpdf.Visible = False
            .ocmattachpoaccepted.Enabled = True
            .ocmviewpiacceptedpdf.Enabled = True
            .ocmviewpiacceptedpdf.Visible = False
            .ocmreject.Visible = False
            .ocmreject.Enabled = False
            .StateDelete = False
            ' .ogcpo.DataSource = dtpoList.Copy
            .LoadDataPI("")

            For Each R As DataRow In dtpoList.Rows
                .AddPo(R!FTPurchaseNo.ToString, R!FNPOQuantity, R!FNPOGrandAmt, R!FNPOBalQuantity, R!FNPOBalGrandAmt, R!FNPOBalQuantity, R!FNPOBalGrandAmt, R!FTUnitCode.ToString)

            Next

            .SumGridAmount()
            .ShowDialog()

            If .AddMat Then
                dtpoList = CType(.ogcpo.DataSource, DataTable).Copy
                Dim dtpocndn As DataTable = CType(.ogcCNDN.DataSource, DataTable).Copy
                Dim dtsurcharge As DataTable = CType(.ogcsurcharge.DataSource, DataTable).Copy

                Dim cmdstring As String

                Dim pNote As String = .FTRemark.Text.Trim
                Dim pPINo As String = .FTPINo.Text.Trim()
                Dim pPIDate As String = .FTPIDate.Text
                Dim pPIRcvDate As String = .FTRcvPIDate.Text
                Dim pPICFMDate As String = .FTPISuplCFMDeliveryDate.Text

                Dim PIQty As Double = .FNPIGrandQuantity.Value
                Dim PIAmt As Double = .FNPIGrandNetAmt.Value

                Dim PICNAmt As Double = .FNCNAmt.Value
                Dim PIDNAmt As Double = .FNDNAmt.Value
                Dim PISurchargeAmt As Double = .FNSurchargeAmt.Value
                Dim PIGTotalAmt As Double = .FNPIGrandTotalAmt.Value

                Dim SuplID As Integer = Val(.FNHSysSuplId.Properties.Tag.ToString)
                Dim CurID As Integer = Val(.FNHSysCurId.Properties.Tag.ToString)
                Dim DocType As Integer = .FNPIDocType.SelectedIndex
                Dim MatType As String = .FTPIMatType.Text.Trim


                Dim BLNo As String = .FTBLNo.Text.Trim
                Dim BLDate As String = .FTBLDate.Text

                Dim _Spls As New HI.TL.SplashScreen("Saving...   Please Wait   ")
                For Each R As DataRow In dtpoList.Rows

                    cmdstring = "Update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase SET "
                    cmdstring &= vbCrLf & " FTPINoBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                    cmdstring &= vbCrLf & ",FTPINoDate=" & HI.UL.ULDate.FormatDateDB & " "
                    cmdstring &= vbCrLf & ",FTPINoTime=" & HI.UL.ULDate.FormatTimeDB & " "
                    cmdstring &= vbCrLf & ",FTPINo='" & HI.UL.ULF.rpQuoted(pPINo) & "' "
                    cmdstring &= vbCrLf & ",FTPIDate='" & HI.UL.ULDate.ConvertEnDB(pPIDate) & "' "
                    cmdstring &= vbCrLf & ",FTRcvPIDate='" & HI.UL.ULDate.ConvertEnDB(pPIRcvDate) & "' "
                    cmdstring &= vbCrLf & ",FTPISuplCFMDeliveryDate='" & HI.UL.ULDate.ConvertEnDB(pPICFMDate) & "' "
                    cmdstring &= vbCrLf & " ,FTPIRemark='" & HI.UL.ULF.rpQuoted(pNote) & "' "
                    cmdstring &= vbCrLf & " ,FNPIQuantity=" & Val(R!FNPIPOQuantity.ToString) & " "
                    cmdstring &= vbCrLf & " ,FNPINetAmt=" & Val(R!FNPIPONetAmt.ToString) & ""
                    cmdstring &= vbCrLf & "  WHERE FTPurchaseNo ='" & HI.UL.ULF.rpQuoted(R!FTPurchaseNo.ToString) & "'"
                    cmdstring &= vbCrLf & "  DECLARE @CountData int = 0 "
                    cmdstring &= vbCrLf & " UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_PI "
                    cmdstring &= vbCrLf & " Set FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                    cmdstring &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB & " "
                    cmdstring &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & " "
                    'cmdstring &= vbCrLf & " FTPINoBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                    'cmdstring &= vbCrLf & ",FTPINoDate=" & HI.UL.ULDate.FormatDateDB & " "
                    'cmdstring &= vbCrLf & ",FTPINoTime=" & HI.UL.ULDate.FormatTimeDB & " "
                    cmdstring &= vbCrLf & ",FTPINo='" & HI.UL.ULF.rpQuoted(pPINo) & "' "
                    cmdstring &= vbCrLf & ",FTPIDate='" & HI.UL.ULDate.ConvertEnDB(pPIDate) & "' "
                    cmdstring &= vbCrLf & ",FTRcvPIDate='" & HI.UL.ULDate.ConvertEnDB(pPIRcvDate) & "' "
                    cmdstring &= vbCrLf & ",FTPISuplCFMDeliveryDate='" & HI.UL.ULDate.ConvertEnDB(pPICFMDate) & "' "
                    cmdstring &= vbCrLf & ",FTPIRemark='" & HI.UL.ULF.rpQuoted(pNote) & "' "
                    cmdstring &= vbCrLf & " ,FNPOQuantity=" & Val(R!FNPOQuantity.ToString) & " "
                    cmdstring &= vbCrLf & " ,FNPONetAmt=" & Val(R!FNPONetAmt.ToString) & ""
                    cmdstring &= vbCrLf & " ,FNPIPOQuantity=" & Val(R!FNPIPOQuantity.ToString) & " "
                    cmdstring &= vbCrLf & " ,FNPIPONetAmt=" & Val(R!FNPIPONetAmt.ToString) & ""
                    cmdstring &= vbCrLf & " ,FNPIGrandQuantity=" & PIQty & " "
                    cmdstring &= vbCrLf & " ,FNPIGrandNetAmt=" & PIAmt & ""
                    cmdstring &= vbCrLf & " ,FNPOBalQuantity=" & Val(R!FNPOBalQuantity.ToString) & " "
                    cmdstring &= vbCrLf & " ,FNPOBalGrandAmt=" & Val(R!FNPOBalGrandAmt.ToString) & ""
                    cmdstring &= vbCrLf & " ,FNCNAmt=" & PICNAmt & " "
                    cmdstring &= vbCrLf & " ,FNDNAmt=" & PIDNAmt & ""
                    cmdstring &= vbCrLf & " ,FNSurchargeAmt=" & PISurchargeAmt & " "
                    cmdstring &= vbCrLf & " ,FNPIGrandTotalAmt=" & PIGTotalAmt & ""
                    cmdstring &= vbCrLf & " ,FNHSysSuplId=" & SuplID & ""
                    cmdstring &= vbCrLf & " ,FNHSysCurId=" & CurID & " "
                    cmdstring &= vbCrLf & " ,FNPIDocType=" & DocType & ""
                    cmdstring &= vbCrLf & " ,FTPIMatType='" & HI.UL.ULF.rpQuoted(MatType) & "'"
                    cmdstring &= vbCrLf & " ,FTUnitCode='" & HI.UL.ULF.rpQuoted(R!FTUnitCode.ToString) & "'"
                    cmdstring &= vbCrLf & " ,FTBLNo='" & HI.UL.ULF.rpQuoted(BLNo) & "'"
                    cmdstring &= vbCrLf & " ,FTBLDate='" & HI.UL.ULDate.ConvertEnDB(BLDate) & "'"
                    cmdstring &= vbCrLf & "  WHERE FTPurchaseNo ='" & HI.UL.ULF.rpQuoted(R!FTPurchaseNo.ToString) & "'"
                    cmdstring &= vbCrLf & "  AND FTPINo ='" & HI.UL.ULF.rpQuoted(pPINo) & "'"
                    cmdstring &= vbCrLf & " SET @CountData = @@ROWCOUNT  "
                    cmdstring &= vbCrLf & " IF @CountData <=0 "
                    cmdstring &= vbCrLf & "   BEGIN "
                    cmdstring &= vbCrLf & "        INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_PI(FTInsUser, FDInsDate, FTInsTime"
                    cmdstring &= vbCrLf & ", FTPINo, FTPurchaseNo, FTPIDate, FTRcvPIDate, FTPISuplCFMDeliveryDate"
                    cmdstring &= vbCrLf & ",  FTPIRemark, FNPOQuantity, FNPONetAmt,  FNPIPOQuantity, FNPIPONetAmt, FNPIGrandQuantity, FNPIGrandNetAmt,FNPOBalQuantity,FNPOBalGrandAmt,FNCNAmt,FNDNAmt,FNSurchargeAmt,FNPIGrandTotalAmt,FNHSysSuplId,FNHSysCurId,FNPIDocType,FTPIMatType,FTUnitCode,FTBLNo,FTBLDate)  "
                    cmdstring &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                    cmdstring &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & " "
                    cmdstring &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & " "
                    cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(pPINo) & "' "
                    cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!FTPurchaseNo.ToString) & "' "
                    cmdstring &= vbCrLf & ",'" & HI.UL.ULDate.ConvertEnDB(pPIDate) & "' "
                    cmdstring &= vbCrLf & ",'" & HI.UL.ULDate.ConvertEnDB(pPIRcvDate) & "' "
                    cmdstring &= vbCrLf & ",'" & HI.UL.ULDate.ConvertEnDB(pPICFMDate) & "' "
                    cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(pNote) & "' "
                    cmdstring &= vbCrLf & " ," & Val(R!FNPOQuantity.ToString) & " "
                    cmdstring &= vbCrLf & " ," & Val(R!FNPONetAmt.ToString) & ""
                    cmdstring &= vbCrLf & " ," & Val(R!FNPIPOQuantity.ToString) & " "
                    cmdstring &= vbCrLf & " ," & Val(R!FNPIPONetAmt.ToString) & ""
                    cmdstring &= vbCrLf & " ," & PIQty & " "
                    cmdstring &= vbCrLf & " ," & PIAmt & ""
                    cmdstring &= vbCrLf & " ," & Val(R!FNPOBalQuantity.ToString) & " "
                    cmdstring &= vbCrLf & " ," & Val(R!FNPOBalGrandAmt.ToString) & ""
                    cmdstring &= vbCrLf & " ," & PICNAmt & " "
                    cmdstring &= vbCrLf & " ," & PIDNAmt & ""
                    cmdstring &= vbCrLf & " ," & PISurchargeAmt & " "
                    cmdstring &= vbCrLf & " ," & PIGTotalAmt & ""
                    cmdstring &= vbCrLf & " ," & SuplID & ""
                    cmdstring &= vbCrLf & " ," & CurID & " "
                    cmdstring &= vbCrLf & " ," & DocType & ""
                    cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(MatType) & "' "
                    cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!FTUnitCode.ToString) & "'"
                    cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(BLNo) & "'"
                    cmdstring &= vbCrLf & " ,'" & HI.UL.ULDate.ConvertEnDB(BLDate) & "'"
                    cmdstring &= vbCrLf & "  SET @CountData = @@ROWCOUNT  "
                    cmdstring &= vbCrLf & "   END  "
                    cmdstring &= vbCrLf & "       delete from  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_PI_Surcharge where FTPINo='" & HI.UL.ULF.rpQuoted(pPINo) & "' AND FNHSysSuplId=" & SuplID & ""
                    cmdstring &= vbCrLf & "       delete from  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_PI_CNDN where FTPINo='" & HI.UL.ULF.rpQuoted(pPINo) & "' AND FNHSysSuplId=" & SuplID & ""

                    Dim Rseq As Integer = 0

                    For Each Rxm As DataRow In dtsurcharge.Select("FTDescription<>''", "FNSeq")
                        Rseq = Rseq + 1

                        cmdstring &= vbCrLf & "        INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_PI_Surcharge(FTInsUser, FDInsDate, FTInsTime"
                        cmdstring &= vbCrLf & ", FTPINo, FNSeq, FTDescription, FNSurchargeAmt, FTRemark,FNHSysSuplId)  "
                        cmdstring &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                        cmdstring &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & " "
                        cmdstring &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & " "
                        cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(pPINo) & "' "
                        cmdstring &= vbCrLf & " ," & Rseq & " "
                        cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(Rxm!FTDescription.ToString) & "' "
                        cmdstring &= vbCrLf & " ," & Val(Rxm!FNSurchargeAmt.ToString) & " "
                        cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(Rxm!FTRemark.ToString) & "' "
                        cmdstring &= vbCrLf & " ," & SuplID & " "

                    Next

                    Rseq = 0

                    For Each Rxm As DataRow In dtpocndn.Select("FTDocRefNo<>''", "FNSeq")

                        Rseq = Rseq + 1

                        cmdstring &= vbCrLf & "        INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_PI_CNDN(FTInsUser, FDInsDate, FTInsTime"
                        cmdstring &= vbCrLf & ", FTPINo, FNSeq, FTDocRefNo, FTDocRefDate, FTDocType, FNDocAmt, FTDocRemark,FNHSysSuplId)  "
                        cmdstring &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                        cmdstring &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & " "
                        cmdstring &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & " "
                        cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(pPINo) & "' "
                        cmdstring &= vbCrLf & " ," & Rseq & " "
                        cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(Rxm!FTDocRefNo.ToString) & "' "
                        cmdstring &= vbCrLf & " ,'' "
                        cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(Rxm!FTDocType.ToString) & "' "
                        cmdstring &= vbCrLf & " ," & Val(Rxm!FNDocAmt.ToString) & " "
                        cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(Rxm!FTDocRemark.ToString) & "' "
                        cmdstring &= vbCrLf & " ," & SuplID & " "

                    Next

                    cmdstring &= vbCrLf & " SELECT  @CountData"

                    HI.Conn.SQLConn.ExecuteOnly(cmdstring, Conn.DB.DataBaseName.DB_PUR)

                    cmdstring = " EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.USP_GETDATAPO_TRACKING '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & FNListDocumentTrackPIData.SelectedIndex.ToString() & ",'','','','','" & HI.UL.ULF.rpQuoted(R!FTPurchaseNo.ToString) & "','" & HI.UL.ULF.rpQuoted(R!FTPurchaseNo.ToString) & "',0"

                    Dim mdt As DataTable
                    mdt = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_PUR)

                    For Each Rxp As DataRow In mdt.Rows

                        With CType(Me.ogdtime.DataSource, DataTable)
                            .AcceptChanges()

                            For Each Rxd As DataRow In .Select("FTPurchaseNo='" & HI.UL.ULF.rpQuoted(R!FTPurchaseNo.ToString) & "'")
                                For Each C1 As DataColumn In mdt.Columns

                                    Try
                                        Rxd.Item(C1.ColumnName) = Rxp.Item(C1.ColumnName)
                                    Catch ex As Exception

                                    End Try


                                Next
                            Next

                            .AcceptChanges()
                        End With


                    Next

                Next


                cmdstring = " EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.USP_SENDDATAPAYMENT_FORVENDER '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(pPINo) & "'"
                HI.Conn.SQLConn.ExecuteOnly(cmdstring, Conn.DB.DataBaseName.DB_PUR)

                _Spls.Close()
                ' Call LoadData()

            End If

        End With

        dtpoList.Dispose()
        dtpo.Dispose()

    End Sub

    Private Sub ocmsavetrack_Click(sender As Object, e As EventArgs) Handles ocmsavetrack.Click
        Dim dtpo As DataTable
        Dim dtpoList As DataTable

        With CType(Me.ogdtime.DataSource, DataTable)
            .AcceptChanges()

            dtpo = .Copy()

        End With

        If dtpo.Select("FTSelect='1' ").Length <= 0 Then

            Exit Sub
        End If

        dtpoList = dtpo.Select("FTSelect='1'  ").CopyToDataTable
        HI.TL.HandlerControl.ClearControl(wAddtracking)
        With wAddtracking
            .AddMat = False
            .ocmadd.Enabled = True
            .ocmcancel.Enabled = True
            .ShowDialog()

            If .AddMat Then

                Dim pNote As String = .FTRemark.Text.Trim
                Dim pConTactName As String = .FTContactName.Text.Trim()
                Dim pContactDate As String = .FTTrackDate.Text
                Dim cmd As String = ""


                Dim _Spls As New HI.TL.SplashScreen("Saving...   Please Wait   ")
                For Each R As DataRow In dtpoList.Rows
                    cmd = " INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_Tracking ( FTInsUser, FDInsDate, FTInsTime, FTPurchaseNo, FNTrackSeq, FTTrackBy, FTTrackDate, FTTrackTime, FTContactName, FTNote) "

                    cmd &= vbCrLf & "  SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                    cmd &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                    cmd &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ""
                    cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTPurchaseNo.ToString) & "' "
                    cmd &= vbCrLf & ", ISNULL((SELECt MAX(FNTrackSeq) AS FNTrackSeq FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_Tracking WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(R!FTPurchaseNo.ToString) & "' ),0) +1  As FNTrackSeq  "
                    cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                    cmd &= vbCrLf & ",'" & HI.UL.ULDate.ConvertEnDB(pContactDate) & "'  "
                    cmd &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ""
                    cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(pConTactName) & "' "
                    cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(pNote) & "' "
                    cmd &= vbCrLf & "  Select  Top 1 TR.FNTrackSeq,Case When ISDATE(TR.FTTrackDate) = 1 Then Convert(varchar(10),Convert(datetime,TR.FTTrackDate) ,103) Else '' END As FTTrackDate,TR.FTTrackBy,TR.FTContactName,TR.FTNote "
                    cmd &= vbCrLf & "  From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_Tracking AS TR  WITH(NOLOCK)  "
                    cmd &= vbCrLf & "   Where TR.FTPurchaseNo = '" & HI.UL.ULF.rpQuoted(R!FTPurchaseNo.ToString) & "' "
                    cmd &= vbCrLf & "   Order By TR.FNTrackSeq DESC "

                    Dim mdt As DataTable
                    mdt = HI.Conn.SQLConn.GetDataTable(cmd, Conn.DB.DataBaseName.DB_PUR)

                    For Each Rxp As DataRow In mdt.Rows

                        With CType(Me.ogdtime.DataSource, DataTable)
                            .AcceptChanges()

                            For Each Rxd As DataRow In .Select("FTPurchaseNo='" & HI.UL.ULF.rpQuoted(R!FTPurchaseNo.ToString) & "'")

                                Rxd!FNTrackSeq = Rxp!FNTrackSeq
                                Rxd!FTTrackDate = Rxp!FTTrackDate
                                Rxd!FTTrackBy = Rxp!FTTrackBy
                                Rxd!FTContactName = Rxp!FTContactName
                                Rxd!FTNote = Rxp!FTNote


                            Next

                            .AcceptChanges()
                        End With


                    Next

                Next

                _Spls.Close()
                '  Call LoadData()

            End If

        End With
        dtpoList.Dispose()
        dtpo.Dispose()
    End Sub

    Private Sub ocmsavepaid_Click(sender As Object, e As EventArgs) Handles ocmsavepaid.Click
        Dim dtpo As DataTable
        Dim dtpoList As DataTable


        With CType(Me.ogdtime.DataSource, DataTable)
            .AcceptChanges()

            dtpo = .Copy()

        End With

        If dtpo.Select("FTSelect='1' AND FTStateSuperVisorApp='1' AND FTStateManagerApp='1' AND FTPINo<>''").Length <= 0 Then

            Exit Sub
        End If

        dtpoList = dtpo.Select("FTSelect='1'  AND FTStateSuperVisorApp='1'  AND FTStateManagerApp='1'  AND FTPINo<>'' ").CopyToDataTable

        HI.TL.HandlerControl.ClearControl(wAddPaid)
        With wAddPaid
            .AddMat = False
            .ocmadd.Enabled = True
            .ocmcancel.Enabled = True
            .ShowDialog()

            If .AddMat Then

                Dim pNote As String = .FTPaidNote.Text.Trim
                Dim pContactDate As String = .FTPaidDate.Text
                Dim pLCNo As String = .FTLCNo.Text.Trim

                Dim cmd As String = ""
                Dim FileID As Integer = 0

                If .FTFilePath.Text <> "" Then

                    Dim pFilePath As String = .FTFilePath.Text
                    Dim pFileName As String = .FTPDFName.Text

                    cmd = "Declare @FileId int =0 "
                    cmd &= vbCrLf & " Select TOP 1  @FileId= ISNULL(MAX(A.FNFileID),0) +1  "
                    cmd &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_PI_File As A With(NOLOCK)"
                    cmd &= vbCrLf & "  IF @FileId  > 0 "
                    cmd &= vbCrLf & "          BEGIN "
                    cmd &= vbCrLf & "                INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_PI_File(FTInsUser, FDInsDate, FTInsTime, FNFileID, FTFileName)"
                    cmd &= vbCrLf & "                Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    cmd &= vbCrLf & "                       ," & HI.UL.ULDate.FormatDateDB
                    cmd &= vbCrLf & "                       ," & HI.UL.ULDate.FormatTimeDB
                    cmd &= vbCrLf & "                      ,@FileId"
                    cmd &= vbCrLf & "                      ,'" & HI.UL.ULF.rpQuoted(pFileName) & "'"
                    cmd &= vbCrLf & "          END "
                    cmd &= vbCrLf & "   SELECT @FileId "

                    FileID = Val(HI.Conn.SQLConn.GetField(cmd, Conn.DB.DataBaseName.DB_PUR, "0"))

                    If FileID > 0 Then

                        Dim data As Byte() = System.IO.File.ReadAllBytes(pFilePath)

                        HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(HI.Conn.DB.DataBaseName.DB_ACCOUNT)
                        HI.Conn.SQLConn.SqlConnectionOpen()

                        cmd = "UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_PI_File"
                        cmd &= " Set  FPFile=@FPFile"
                        cmd &= "  Where FNFileID=@FNFileID"

                        Dim scmd As New SqlCommand(cmd, HI.Conn.SQLConn.Cnn)
                        Dim p6 As New SqlParameter("@FPFile", SqlDbType.VarBinary)
                        p6.Value = data

                        Dim p8 As New SqlParameter("@FNFileID", SqlDbType.NVarChar)
                        p8.Value = FileID

                        scmd.Parameters.Add(p6)
                        scmd.Parameters.Add(p8)

                        scmd.ExecuteNonQuery()

                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cnn)

                    End If

                End If

                Dim _Spls As New HI.TL.SplashScreen("Saving...   Please Wait   ")
                For Each R As DataRow In dtpoList.Rows

                    cmd = "Update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase SET FTStatePaid='1'"
                    cmd &= vbCrLf & ",FTStatePaidBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                    cmd &= vbCrLf & ",FTStatePaidDate=" & HI.UL.ULDate.FormatDateDB & " "
                    cmd &= vbCrLf & ",FTStatePaidTime=" & HI.UL.ULDate.FormatTimeDB & " "
                    cmd &= vbCrLf & ",FTPaidDate='" & HI.UL.ULDate.ConvertEnDB(pContactDate) & "' "
                    cmd &= vbCrLf & ",FTPaidNote='" & HI.UL.ULF.rpQuoted(pNote) & "' "
                    cmd &= vbCrLf & "  WHERE FTPurchaseNo ='" & HI.UL.ULF.rpQuoted(R!FTPurchaseNo.ToString) & "'"


                    cmd &= vbCrLf & " UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_PI SET FTStatePaid='1'"
                    cmd &= vbCrLf & ",FTStatePaidBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                    cmd &= vbCrLf & ",FTStatePaidDate=" & HI.UL.ULDate.FormatDateDB & " "
                    cmd &= vbCrLf & ",FTStatePaidTime=" & HI.UL.ULDate.FormatTimeDB & " "
                    cmd &= vbCrLf & ",FTPaidDate=CASE WHEN ISNULL(FTLCNo,'') ='' THEN '" & HI.UL.ULDate.ConvertEnDB(pContactDate) & "' ELSE ISNULL(FTPaidDate,'') END "
                    cmd &= vbCrLf & ",FTLCNo= CASE WHEN ISNULL(FTLCNo,'') ='' THEN '" & HI.UL.ULF.rpQuoted(pLCNo) & "' ELSE ISNULL(FTLCNo,'') END "
                    cmd &= vbCrLf & ",FTPaidNote=CASE WHEN ISNULL(FTLCNo,'') ='' THEN '" & HI.UL.ULF.rpQuoted(pNote) & "'   ELSE FTPaidNote END"
                    cmd &= vbCrLf & ",FNFileID= CASE WHEN ISNULL(FNFileID,0) =0 THEN   " & FileID & " ELSE ISNULL(FNFileID,0)  END "

                    cmd &= vbCrLf & "  WHERE FTPINo ='" & HI.UL.ULF.rpQuoted(R!FTPINo.ToString) & "'  "

                    HI.Conn.SQLConn.ExecuteNonQuery(cmd, Conn.DB.DataBaseName.DB_PUR)

                    cmd = " EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.USP_GETDATAPO_TRACKING '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & FNListDocumentTrackPIData.SelectedIndex.ToString() & ",'','','','','" & HI.UL.ULF.rpQuoted(R!FTPurchaseNo.ToString) & "','" & HI.UL.ULF.rpQuoted(R!FTPurchaseNo.ToString) & "',0"

                    Dim mdt As DataTable
                    mdt = HI.Conn.SQLConn.GetDataTable(cmd, Conn.DB.DataBaseName.DB_PUR)

                    For Each Rxp As DataRow In mdt.Rows

                        With CType(Me.ogdtime.DataSource, DataTable)
                            .AcceptChanges()

                            For Each Rxd As DataRow In .Select("FTPurchaseNo='" & HI.UL.ULF.rpQuoted(R!FTPurchaseNo.ToString) & "'")
                                For Each C1 As DataColumn In mdt.Columns

                                    Try
                                        Rxd.Item(C1.ColumnName) = Rxp.Item(C1.ColumnName)
                                    Catch ex As Exception

                                    End Try


                                Next
                            Next

                            .AcceptChanges()
                        End With


                    Next

                Next
                Try
                    Dim grp As List(Of String) = (dtpoList.Select("FTPINo<>''", "FTPINo").CopyToDataTable).AsEnumerable() _
                                                        .Select(Function(r) r.Field(Of String)("FTPINo")) _
                                                        .Distinct() _
                                                        .ToList()


                    For Each ppi As String In grp

                        If ppi <> "" Then

                            cmd = " EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.USP_SENDDATAPAYMENT_FORVENDER '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(ppi) & "'"
                            HI.Conn.SQLConn.ExecuteOnly(cmd, Conn.DB.DataBaseName.DB_PUR)


                        End If

                    Next
                Catch ex As Exception

                End Try


                _Spls.Close()
                '  Call LoadData()

            End If

        End With
        dtpoList.Dispose()
        dtpo.Dispose()
    End Sub

    Private Sub ocmsavepipayment_Click(sender As Object, e As EventArgs) Handles ocmsavepipayment.Click
        Dim dtpo As DataTable
        Dim dtpoList As DataTable


        With CType(Me.ogdtime.DataSource, DataTable)
            .AcceptChanges()

            dtpo = .Copy()

        End With

        If dtpo.Select("FTSelect='1' AND FTStateSuperVisorApp='1' AND FTStateManagerApp='1' AND FTPINo<>''").Length <= 0 Then

            Exit Sub
        End If

        dtpoList = dtpo.Select("FTSelect='1'  AND FTStateSuperVisorApp='1'  AND FTStateManagerApp='1'  AND FTPINo<>'' ").CopyToDataTable

        HI.TL.HandlerControl.ClearControl(wAddPIPayment)
        With wAddPIPayment
            .AddMat = False
            .ocmadd.Enabled = True
            .ocmcancel.Enabled = True
            .ShowDialog()

            If .AddMat Then

                Dim pFNPIPayType As Integer = .FNPIPayType.SelectedIndex
                Dim pNote As String = .FTPIPayTypeRemark.Text.Trim
                Dim pContactDate As String = .FTPIPayDate.Text
                Dim VenderAccNo As String = .FTVenderAccNo.Text.Trim
                Dim cmd As String = ""

                Dim _Spls As New HI.TL.SplashScreen("Saving...   Please Wait   ")
                For Each R As DataRow In dtpoList.Rows

                    cmd = "Update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase SET FNPIPayType=" & pFNPIPayType & ""
                    cmd &= vbCrLf & ",FTPIPayTypeBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                    cmd &= vbCrLf & ",FTPIPayTypeDate=" & HI.UL.ULDate.FormatDateDB & " "
                    cmd &= vbCrLf & ",FTPIPayTypeTime=" & HI.UL.ULDate.FormatTimeDB & " "
                    cmd &= vbCrLf & ",FTPIPayDate='" & HI.UL.ULDate.ConvertEnDB(pContactDate) & "' "
                    cmd &= vbCrLf & ",FTPIPayTypeRemark='" & HI.UL.ULF.rpQuoted(pNote) & "' "
                    cmd &= vbCrLf & "  WHERE FTPurchaseNo ='" & HI.UL.ULF.rpQuoted(R!FTPurchaseNo.ToString) & "'"

                    cmd &= vbCrLf & " UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_PI SET FNPIPayType=" & pFNPIPayType & ""
                    cmd &= vbCrLf & ",FTPIPayTypeBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                    cmd &= vbCrLf & ",FTPIPayTypeDate=" & HI.UL.ULDate.FormatDateDB & " "
                    cmd &= vbCrLf & ",FTPIPayTypeTime=" & HI.UL.ULDate.FormatTimeDB & " "
                    cmd &= vbCrLf & ",FTPIPayDate='" & HI.UL.ULDate.ConvertEnDB(pContactDate) & "' "
                    cmd &= vbCrLf & ",FTPIPayTypeRemark='" & HI.UL.ULF.rpQuoted(pNote) & "' "
                    cmd &= vbCrLf & ",FTVenderAccNo='" & HI.UL.ULF.rpQuoted(VenderAccNo) & "' "
                    cmd &= vbCrLf & "  WHERE FTPINo ='" & HI.UL.ULF.rpQuoted(R!FTPINo.ToString) & "'  AND ISNULL(FTStatePaid,'') <>'1' "

                    HI.Conn.SQLConn.ExecuteNonQuery(cmd, Conn.DB.DataBaseName.DB_PUR)

                    cmd = " EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.USP_GETDATAPO_TRACKING '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & FNListDocumentTrackPIData.SelectedIndex.ToString() & ",'','','','','" & HI.UL.ULF.rpQuoted(R!FTPurchaseNo.ToString) & "','" & HI.UL.ULF.rpQuoted(R!FTPurchaseNo.ToString) & "',0"

                    Dim mdt As DataTable
                    mdt = HI.Conn.SQLConn.GetDataTable(cmd, Conn.DB.DataBaseName.DB_PUR)

                    For Each Rxp As DataRow In mdt.Rows

                        With CType(Me.ogdtime.DataSource, DataTable)
                            .AcceptChanges()

                            For Each Rxd As DataRow In .Select("FTPurchaseNo='" & HI.UL.ULF.rpQuoted(R!FTPurchaseNo.ToString) & "'")
                                For Each C1 As DataColumn In mdt.Columns

                                    Try
                                        Rxd.Item(C1.ColumnName) = Rxp.Item(C1.ColumnName)
                                    Catch ex As Exception

                                    End Try


                                Next
                            Next

                            .AcceptChanges()
                        End With


                    Next

                Next


                Dim grp As List(Of String) = (dtpoList.Select("FTPINo<>''", "FTPINo").CopyToDataTable).AsEnumerable() _
                                                          .Select(Function(r) r.Field(Of String)("FTPINo")) _
                                                          .Distinct() _
                                                          .ToList()


                For Each ppi As String In grp

                    If ppi <> "" Then

                        cmd = " EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.USP_SENDDATAPAYMENT_FORVENDER '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(ppi) & "'"
                        HI.Conn.SQLConn.ExecuteOnly(cmd, Conn.DB.DataBaseName.DB_PUR)


                    End If

                Next

                _Spls.Close()
                ' Call LoadData()

            End If

        End With

        dtpoList.Dispose()
        dtpo.Dispose()
    End Sub

    Private Sub RepositoryItemTextFTInvoiceNo_Leave(sender As Object, e As EventArgs) Handles RepositoryItemTextFTInvoiceNo.Leave
        Try
            With CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView)

                Dim _TData As String

                Try

                    _TData = CType(sender, DevExpress.XtraEditors.TextEdit).Text.Trim



                Catch ex As Exception
                    _TData = ""
                End Try


                .SetRowCellValue(.FocusedRowHandle, .FocusedColumn.FieldName.ToString, _TData)


                Dim NewData As String = _TData
                If NewData <> GridDataInvoiceBefore Then
                    Dim OrderNo As String = .GetRowCellValue(.FocusedRowHandle, "FTPurchaseNo").ToString()

                    Dim FieldName As String = .FocusedColumn.FieldName.ToString

                    Dim cmdstring As String = ""

                    cmdstring = "update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase  Set "
                    cmdstring &= vbCrLf & " FTInvoiceNo='" & HI.UL.ULF.rpQuoted(NewData) & "'"
                    cmdstring &= vbCrLf & ",FTInvoiceNoBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    cmdstring &= vbCrLf & ",FTInvoiceNoDate=" & HI.UL.ULDate.FormatDateDB & ""
                    cmdstring &= vbCrLf & ",FTInvoiceNoTime=" & HI.UL.ULDate.FormatTimeDB & " "
                    cmdstring &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(OrderNo) & "'  "

                    If HI.Conn.SQLConn.ExecuteNonQuery(cmdstring, Conn.DB.DataBaseName.DB_PUR) Then

                    End If

                End If



            End With

        Catch ex As Exception
        End Try
    End Sub

    Private Sub RepositoryItemMemoExFTNote_Leave(sender As Object, e As EventArgs) Handles RepositoryItemMemoExFTNote.Leave
        Try
            With CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView)

                Dim _TData As String

                Try

                    _TData = CType(sender, DevExpress.XtraEditors.MemoExEdit).Text.Trim



                Catch ex As Exception
                    _TData = ""
                End Try


                .SetRowCellValue(.FocusedRowHandle, .FocusedColumn.FieldName.ToString, _TData)


                Dim NewData As String = _TData
                If NewData <> GridDataNoteBefore Then
                    Dim OrderNo As String = .GetRowCellValue(.FocusedRowHandle, "FTPurchaseNo").ToString()

                    Dim FieldName As String = .FocusedColumn.FieldName.ToString

                    Dim cmdstring As String = ""

                    cmdstring = "update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase  Set "
                    cmdstring &= vbCrLf & " FTNote='" & HI.UL.ULF.rpQuoted(NewData) & "'"
                    cmdstring &= vbCrLf & ",FTNoteBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    cmdstring &= vbCrLf & ",FTNoteDate=" & HI.UL.ULDate.FormatDateDB & ""
                    cmdstring &= vbCrLf & ",FTNoteTime=" & HI.UL.ULDate.FormatTimeDB & " "
                    cmdstring &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(OrderNo) & "'  "

                    If HI.Conn.SQLConn.ExecuteNonQuery(cmdstring, Conn.DB.DataBaseName.DB_PUR) Then

                    End If

                End If



            End With

        Catch ex As Exception
        End Try
    End Sub

    Private Sub ogvtime_ShowingEditor(sender As Object, e As CancelEventArgs) Handles ogvtime.ShowingEditor
        Select Case ogvtime.FocusedColumn.FieldName.ToString
            Case "FTInvoiceNo"

                GridDataInvoiceBefore = ogvtime.GetFocusedRowCellValue(ogvtime.FocusedColumn.FieldName.ToString).ToString
            Case "FTNote"

                GridDataNoteBefore = ogvtime.GetFocusedRowCellValue(ogvtime.FocusedColumn.FieldName.ToString).ToString
            Case "PDF"
                If ogvtime.GetFocusedRowCellValue("FTStateFile").ToString = "1" Then
                    e.Cancel = False
                Else
                    e.Cancel = True
                End If
            Case "PIPDF"
                If ogvtime.GetFocusedRowCellValue("FTStatePIPDFFile").ToString = "1" Then
                    e.Cancel = False
                Else
                    e.Cancel = True
                End If
        End Select
    End Sub

    Private Sub ochkselectall_CheckedChanged(sender As Object, e As EventArgs) Handles ochkselectall.CheckedChanged
        Try

            Dim _State As String = "0"
            If Me.ochkselectall.Checked Then
                _State = "1"
            End If

            With ogdtime
                If Not (.DataSource Is Nothing) And ogvtime.RowCount > 0 Then

                    With ogvtime
                        For I As Integer = 0 To .RowCount - 1
                            .SetRowCellValue(I, .Columns.ColumnByFieldName("FTSelect"), _State)
                        Next
                    End With

                    CType(.DataSource, DataTable).AcceptChanges()
                End If

            End With
        Catch ex As Exception

        End Try
    End Sub

    Private Sub FNListDocumentTrackPIData_SelectedIndexChanged(sender As Object, e As EventArgs) Handles FNListDocumentTrackPIData.SelectedIndexChanged
        If _FormLoad = False Then
            Call HI.UL.AppRegistry.WriteRegistry("ListDoc" & Me.Name, FNListDocumentTrackPIData.SelectedIndex.ToString)
        End If
    End Sub

    Private Sub ocmeditpi_Click(sender As Object, e As EventArgs) Handles ocmeditpi.Click
        With ogvtime
            If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub



        End With

        Dim mPINO As String = ""
        Dim cmdstring As String = ""
        Dim PoNo As String = ogvtime.GetRowCellValue(ogvtime.FocusedRowHandle, "FTPurchaseNo").ToString()


        Dim SuplCode As String = ogvtime.GetRowCellValue(ogvtime.FocusedRowHandle, "FTSuplCode").ToString()
        Dim CurCode As String = ogvtime.GetRowCellValue(ogvtime.FocusedRowHandle, "FTCurCode").ToString()

        Dim dtpoList As DataTable
        Dim dtpi As DataTable

        cmdstring = " select   FTPINo from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_PI WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(PoNo) & "' AND ISNULL(FTStatePaid,'') <>'1'  ORDER BY FTPINo "
        dtpi = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_PUR)
        If dtpi.Rows.Count = 0 Then

            Exit Sub
        Else
            If dtpi.Rows.Count = 1 Then
                mPINO = dtpi.Rows(0)!FTPINo.ToString
            Else
                With wListPINo
                    .PINo = ""
                    .ogcdetail.DataSource = dtpi.Copy
                    .ShowDialog()

                    If .PINo <> "" Then
                        mPINO = .PINo
                    End If

                End With

            End If
        End If

        dtpi.Dispose()

        If mPINO = "" Then
            Exit Sub
        End If

        Dim dtpo As DataTable
        Dim FounNone As Boolean = False

        With CType(Me.ogdtime.DataSource, DataTable)
            .AcceptChanges()

            dtpo = .Copy()

        End With

        If dtpo.Select("FTSelect='1'  AND FTStateSuperVisorApp='1'  AND  FTPINo='' ").Length > 0 Then
            dtpoList = dtpo.Select("FTSelect='1'  AND FTStateSuperVisorApp='1'  AND  FTPINo='' ").CopyToDataTable

            FounNone = True
        End If


        HI.TL.HandlerControl.ClearControl(wAddPI)
        With wAddPI
            .PINO = mPINO
            .FNPIDocType.SelectedIndex = 0
            .AddMat = False
            .FTPINo.Enabled = False
            .FTPINo.ReadOnly = True
            .ocmadd.Enabled = True
            .ocmcancel.Enabled = True
            ' .ogcpo.DataSource = dtpoList.Copy
            .FNHSysSuplId.Text = SuplCode
            .FNHSysCurId.Text = CurCode
            .FTPINo.Text = mPINO
            .ocmviewpdf.Enabled = True
            .ocmviewpdf.Visible = False
            .ocmattachpoaccepted.Enabled = True
            .ocmviewpiacceptedpdf.Enabled = True
            .ocmviewpiacceptedpdf.Visible = False
            .ocmreject.Visible = True
            .ocmreject.Enabled = True
            .StateDelete = False
            .LoadDataPI(mPINO)

            Try
                If FounNone Then
                    For Each R As DataRow In dtpoList.Rows
                        .AddPo(R!FTPurchaseNo.ToString, R!FNPOQuantity, R!FNPOGrandAmt, R!FNPOBalQuantity, R!FNPOBalGrandAmt, R!FNPOBalQuantity, R!FNPOBalGrandAmt, R!FTUnitCode.ToString)

                    Next
                End If


            Catch ex As Exception

            End Try


            .SumGridAmount()

            .ShowDialog()

            If .AddMat Then
                dtpoList = CType(.ogcpo.DataSource, DataTable).Copy

                dtpoList = CType(.ogcpo.DataSource, DataTable).Copy
                Dim dtpocndn As DataTable = CType(.ogcCNDN.DataSource, DataTable).Copy
                Dim dtsurcharge As DataTable = CType(.ogcsurcharge.DataSource, DataTable).Copy

                Dim pNote As String = .FTRemark.Text.Trim
                Dim pPINo As String = .FTPINo.Text.Trim()
                Dim pPIDate As String = .FTPIDate.Text
                Dim pPIRcvDate As String = .FTRcvPIDate.Text
                Dim pPICFMDate As String = .FTPISuplCFMDeliveryDate.Text

                Dim PIQty As Double = .FNPIGrandQuantity.Value
                Dim PIAmt As Double = .FNPIGrandNetAmt.Value

                Dim PICNAmt As Double = .FNCNAmt.Value
                Dim PIDNAmt As Double = .FNDNAmt.Value
                Dim PISurchargeAmt As Double = .FNSurchargeAmt.Value
                Dim PIGTotalAmt As Double = .FNPIGrandTotalAmt.Value

                Dim SuplID As Integer = Val(.FNHSysSuplId.Properties.Tag.ToString)
                Dim CurID As Integer = Val(.FNHSysCurId.Properties.Tag.ToString)
                Dim DocType As Integer = .FNPIDocType.SelectedIndex
                Dim MatType As String = .FTPIMatType.Text.Trim


                Dim BLNo As String = .FTBLNo.Text.Trim
                Dim BLDate As String = .FTBLDate.Text

                Dim pStatedelete As Boolean = .StateDelete
                Dim _Spls As New HI.TL.SplashScreen("Saving...   Please Wait   ")
                For Each R As DataRow In dtpoList.Rows

                    cmdstring = " "
                    cmdstring &= vbCrLf & "  DECLARE @CountData int = 0 "

                    If pStatedelete = False Then

                        cmdstring &= vbCrLf & "  Update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase Set "
                        cmdstring &= vbCrLf & " FTPINoBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                        cmdstring &= vbCrLf & ",FTPINoDate=" & HI.UL.ULDate.FormatDateDB & " "
                        cmdstring &= vbCrLf & ",FTPINoTime=" & HI.UL.ULDate.FormatTimeDB & " "

                        cmdstring &= vbCrLf & ",FTPINo='" & HI.UL.ULF.rpQuoted(pPINo) & "' "
                        cmdstring &= vbCrLf & ",FTPIDate='" & HI.UL.ULDate.ConvertEnDB(pPIDate) & "' "
                        cmdstring &= vbCrLf & ",FTRcvPIDate='" & HI.UL.ULDate.ConvertEnDB(pPIRcvDate) & "' "
                        cmdstring &= vbCrLf & ",FTPISuplCFMDeliveryDate='" & HI.UL.ULDate.ConvertEnDB(pPICFMDate) & "' "
                        cmdstring &= vbCrLf & " ,FTPIRemark='" & HI.UL.ULF.rpQuoted(pNote) & "' "
                        cmdstring &= vbCrLf & " ,FNPIQuantity=" & Val(R!FNPIPOQuantity.ToString) & " "
                        cmdstring &= vbCrLf & " ,FNPINetAmt=" & Val(R!FNPIPONetAmt.ToString) & ""
                        cmdstring &= vbCrLf & "  WHERE FTPurchaseNo ='" & HI.UL.ULF.rpQuoted(R!FTPurchaseNo.ToString) & "'"

                        cmdstring &= vbCrLf & " UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_PI "
                        cmdstring &= vbCrLf & " Set FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                        cmdstring &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB & " "
                        cmdstring &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & " "
                        'cmdstring &= vbCrLf & " FTPINoBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                        'cmdstring &= vbCrLf & ",FTPINoDate=" & HI.UL.ULDate.FormatDateDB & " "
                        'cmdstring &= vbCrLf & ",FTPINoTime=" & HI.UL.ULDate.FormatTimeDB & " "
                        cmdstring &= vbCrLf & ",FTPINo='" & HI.UL.ULF.rpQuoted(pPINo) & "' "
                        cmdstring &= vbCrLf & ",FTPIDate='" & HI.UL.ULDate.ConvertEnDB(pPIDate) & "' "
                        cmdstring &= vbCrLf & ",FTRcvPIDate='" & HI.UL.ULDate.ConvertEnDB(pPIRcvDate) & "' "
                        cmdstring &= vbCrLf & ",FTPISuplCFMDeliveryDate='" & HI.UL.ULDate.ConvertEnDB(pPICFMDate) & "' "
                        cmdstring &= vbCrLf & ",FTPIRemark='" & HI.UL.ULF.rpQuoted(pNote) & "' "

                        cmdstring &= vbCrLf & " ,FNPOQuantity=" & Val(R!FNPOQuantity.ToString) & " "
                        cmdstring &= vbCrLf & " ,FNPONetAmt=" & Val(R!FNPONetAmt.ToString) & ""
                        cmdstring &= vbCrLf & " ,FNPIPOQuantity=" & Val(R!FNPIPOQuantity.ToString) & " "
                        cmdstring &= vbCrLf & " ,FNPIPONetAmt=" & Val(R!FNPIPONetAmt.ToString) & ""
                        cmdstring &= vbCrLf & " ,FNPIGrandQuantity=" & PIQty & " "
                        cmdstring &= vbCrLf & " ,FNPIGrandNetAmt=" & PIAmt & ""
                        cmdstring &= vbCrLf & " ,FNPOBalQuantity=" & Val(R!FNPOBalQuantity.ToString) & " "
                        cmdstring &= vbCrLf & " ,FNPOBalGrandAmt=" & Val(R!FNPOBalGrandAmt.ToString) & ""

                        cmdstring &= vbCrLf & " ,FNCNAmt=" & PICNAmt & " "
                        cmdstring &= vbCrLf & " ,FNDNAmt=" & PIDNAmt & ""
                        cmdstring &= vbCrLf & " ,FNSurchargeAmt=" & PISurchargeAmt & " "
                        cmdstring &= vbCrLf & " ,FNPIGrandTotalAmt=" & PIGTotalAmt & ""

                        cmdstring &= vbCrLf & " ,FNHSysSuplId=" & SuplID & ""
                        cmdstring &= vbCrLf & " ,FNHSysCurId=" & CurID & " "
                        cmdstring &= vbCrLf & " ,FNPIDocType=" & DocType & ""
                        cmdstring &= vbCrLf & " ,FTPIMatType='" & HI.UL.ULF.rpQuoted(MatType) & "'"
                        cmdstring &= vbCrLf & " ,FTUnitCode='" & HI.UL.ULF.rpQuoted(R!FTUnitCode.ToString) & "'"
                        cmdstring &= vbCrLf & " ,FTBLNo='" & HI.UL.ULF.rpQuoted(BLNo) & "'"
                        cmdstring &= vbCrLf & " ,FTBLDate='" & HI.UL.ULDate.ConvertEnDB(BLDate) & "'"

                        cmdstring &= vbCrLf & "  WHERE FTPurchaseNo ='" & HI.UL.ULF.rpQuoted(R!FTPurchaseNo.ToString) & "'"
                        cmdstring &= vbCrLf & "  AND FTPINo ='" & HI.UL.ULF.rpQuoted(mPINO) & "'"


                        cmdstring &= vbCrLf & " SET @CountData = @@ROWCOUNT  "
                        cmdstring &= vbCrLf & " IF @CountData <=0 "
                        cmdstring &= vbCrLf & "   BEGIN "

                        cmdstring &= vbCrLf & "        INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_PI(FTInsUser, FDInsDate, FTInsTime"
                        cmdstring &= vbCrLf & ", FTPINo, FTPurchaseNo, FTPIDate, FTRcvPIDate, FTPISuplCFMDeliveryDate"
                        cmdstring &= vbCrLf & ",  FTPIRemark, FNPOQuantity, FNPONetAmt,  FNPIPOQuantity, FNPIPONetAmt, FNPIGrandQuantity, FNPIGrandNetAmt,FNPOBalQuantity,FNPOBalGrandAmt,FNCNAmt,FNDNAmt,FNSurchargeAmt,FNPIGrandTotalAmt,FNHSysSuplId,FNHSysCurId,FNPIDocType,FTPIMatType,FTUnitCode,FTBLNo,FTBLDate)  "
                        cmdstring &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                        cmdstring &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & " "
                        cmdstring &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & " "
                        cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(pPINo) & "' "
                        cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!FTPurchaseNo.ToString) & "' "

                        cmdstring &= vbCrLf & ",'" & HI.UL.ULDate.ConvertEnDB(pPIDate) & "' "
                        cmdstring &= vbCrLf & ",'" & HI.UL.ULDate.ConvertEnDB(pPIRcvDate) & "' "
                        cmdstring &= vbCrLf & ",'" & HI.UL.ULDate.ConvertEnDB(pPICFMDate) & "' "
                        cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(pNote) & "' "

                        cmdstring &= vbCrLf & " ," & Val(R!FNPOQuantity.ToString) & " "
                        cmdstring &= vbCrLf & " ," & Val(R!FNPONetAmt.ToString) & ""
                        cmdstring &= vbCrLf & " ," & Val(R!FNPIPOQuantity.ToString) & " "
                        cmdstring &= vbCrLf & " ," & Val(R!FNPIPONetAmt.ToString) & ""
                        cmdstring &= vbCrLf & " ," & PIQty & " "
                        cmdstring &= vbCrLf & " ," & PIAmt & ""
                        cmdstring &= vbCrLf & " ," & Val(R!FNPOBalQuantity.ToString) & " "
                        cmdstring &= vbCrLf & " ," & Val(R!FNPOBalGrandAmt.ToString) & ""
                        cmdstring &= vbCrLf & " ," & PICNAmt & " "
                        cmdstring &= vbCrLf & " ," & PIDNAmt & ""
                        cmdstring &= vbCrLf & " ," & PISurchargeAmt & " "
                        cmdstring &= vbCrLf & " ," & PIGTotalAmt & ""
                        cmdstring &= vbCrLf & " ," & SuplID & ""
                        cmdstring &= vbCrLf & " ," & CurID & " "
                        cmdstring &= vbCrLf & " ," & DocType & ""
                        cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(MatType) & "' "
                        cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!FTUnitCode.ToString) & "'"
                        cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(BLNo) & "'"
                        cmdstring &= vbCrLf & " ,'" & HI.UL.ULDate.ConvertEnDB(BLDate) & "'"

                        cmdstring &= vbCrLf & " SET @CountData = @@ROWCOUNT  "

                        cmdstring &= vbCrLf & "   END "


                    End If

                    cmdstring &= vbCrLf & " delete from  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_PI_Surcharge where FTPINo='" & HI.UL.ULF.rpQuoted(mPINO) & "' AND FNHSysSuplId=" & SuplID & ""
                    cmdstring &= vbCrLf & " delete from  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_PI_CNDN where FTPINo='" & HI.UL.ULF.rpQuoted(mPINO) & "' AND FNHSysSuplId=" & SuplID & ""

                    If pStatedelete = False Then

                        Dim Rseq As Integer = 0

                        For Each Rxm As DataRow In dtsurcharge.Select("FTDescription<>''", "FNSeq")
                            Rseq = Rseq + 1

                            cmdstring &= vbCrLf & "        INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_PI_Surcharge(FTInsUser, FDInsDate, FTInsTime"
                            cmdstring &= vbCrLf & ", FTPINo, FNSeq, FTDescription, FNSurchargeAmt, FTRemark,FNHSysSuplId)  "
                            cmdstring &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                            cmdstring &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & " "
                            cmdstring &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & " "
                            cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(pPINo) & "' "
                            cmdstring &= vbCrLf & " ," & Rseq & " "
                            cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(Rxm!FTDescription.ToString) & "' "
                            cmdstring &= vbCrLf & " ," & Val(Rxm!FNSurchargeAmt.ToString) & " "
                            cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(Rxm!FTRemark.ToString) & "' "
                            cmdstring &= vbCrLf & " ," & SuplID & " "
                        Next

                        Rseq = 0

                        For Each Rxm As DataRow In dtpocndn.Select("FTDocRefNo<>''", "FNSeq")
                            Rseq = Rseq + 1

                            cmdstring &= vbCrLf & "        INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_PI_CNDN(FTInsUser, FDInsDate, FTInsTime"
                            cmdstring &= vbCrLf & ", FTPINo, FNSeq, FTDocRefNo, FTDocRefDate, FTDocType, FNDocAmt, FTDocRemark,FNHSysSuplId)  "
                            cmdstring &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                            cmdstring &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & " "
                            cmdstring &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & " "
                            cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(pPINo) & "' "
                            cmdstring &= vbCrLf & " ," & Rseq & " "
                            cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(Rxm!FTDocRefNo.ToString) & "' "
                            cmdstring &= vbCrLf & " ,'' "
                            cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(Rxm!FTDocType.ToString) & "' "
                            cmdstring &= vbCrLf & " ," & Val(Rxm!FNDocAmt.ToString) & " "
                            cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(Rxm!FTDocRemark.ToString) & "' "
                            cmdstring &= vbCrLf & " ," & SuplID & " "
                        Next


                    End If

                    cmdstring &= vbCrLf & " SELECT  @CountData"

                    HI.Conn.SQLConn.ExecuteOnly(cmdstring, Conn.DB.DataBaseName.DB_PUR)

                    cmdstring = " EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.USP_GETDATAPO_TRACKING '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & FNListDocumentTrackPIData.SelectedIndex.ToString() & ",'','','','','" & HI.UL.ULF.rpQuoted(R!FTPurchaseNo.ToString) & "','" & HI.UL.ULF.rpQuoted(R!FTPurchaseNo.ToString) & "',0"

                    Dim mdt As DataTable
                    mdt = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_PUR)

                    For Each Rxp As DataRow In mdt.Rows

                        With CType(Me.ogdtime.DataSource, DataTable)
                            .AcceptChanges()

                            For Each Rxd As DataRow In .Select("FTPurchaseNo='" & HI.UL.ULF.rpQuoted(R!FTPurchaseNo.ToString) & "'")
                                For Each C1 As DataColumn In mdt.Columns

                                    Try
                                        Rxd.Item(C1.ColumnName) = Rxp.Item(C1.ColumnName)
                                    Catch ex As Exception

                                    End Try


                                Next
                            Next


                            .AcceptChanges()

                        End With


                    Next

                Next


                cmdstring = " EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.USP_SENDDATAPAYMENT_FORVENDER '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(pPINo) & "'"
                HI.Conn.SQLConn.ExecuteOnly(cmdstring, Conn.DB.DataBaseName.DB_PUR)

                _Spls.Close()
                ' Call LoadData()

            End If

        End With


    End Sub

    Private Sub ocmpreviewpisummary_Click(sender As Object, e As EventArgs) Handles ocmpreviewpisummary.Click



        With wAddPIPreview
            .SFTDateTrans.Text = ""
            .EFTDateTrans.Text = ""
            .ocmpreview.Enabled = True
            .ocmcancel.Enabled = True
            .ShowDialog()
        End With
    End Sub

    Private Sub ocmFTStatePaid_Click(sender As Object, e As EventArgs) Handles ocmFTStatePaid.Click

    End Sub

    Private Sub RepositoryItemPopupContainerEdit0_QueryPopUp(sender As Object, e As CancelEventArgs) Handles RepositoryItemPopupContainerPDF.QueryPopUp
        Try
            If Me.ogdtime.DataSource Is Nothing Then Exit Sub


            Try
                FilePdfViewer.CloseDocument()
            Catch ex As Exception

            End Try

            Dim cmdstring As String = ""

            Select Case ogvtime.FocusedColumn.FieldName
                Case "PIPDF"

                    Dim PINO As String = Me.ogvtime.GetFocusedRowCellValue("FTPINo").ToString().Split(",")(0)
                    cmdstring = " EXEC   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.USP_GETFILEPIPDF '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(PINO) & "'"

                Case Else

                    Dim pPDFSeq As Integer = Val(Me.ogvtime.GetFocusedRowCellValue("FNFileID").ToString)

                    cmdstring = "Select TOP 1 FPFile "
                    cmdstring &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_PI_File AS A With(NOLOCK) "
                    cmdstring &= vbCrLf & " WHERE  A.FNFileID =" & pPDFSeq & "  "

            End Select

            Dim dt As DataTable = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_PUR)

            If dt.Rows.Count > 0 Then


                Try
                    Dim br As Byte() = dt.Rows(0)!FPFile
                    FilePdfViewer.LoadDocument(New MemoryStream(br))
                Catch ex As Exception

                End Try

            Else


            End If


            dt.Dispose()

        Catch ex As Exception

        End Try
    End Sub


    Private Sub RepositoryItemPopupContainerEdit1_QueryPopUp(sender As Object, e As CancelEventArgs) Handles RepositoryItemPopupContainerPDFPI.QueryPopUp
        Try
            If Me.ogdtime.DataSource Is Nothing Then Exit Sub


            Dim masterView As GridView = Me.ogvtime
            Dim detailView As GridView = TryCast(masterView.GetDetailView(masterView.FocusedRowHandle, 0), GridView)
            detailView.Focus()

            Try
                FilePdfViewerPI.CloseDocument()
            Catch ex As Exception

            End Try

            Dim cmdstring As String = ""

            Select Case detailView.FocusedColumn.FieldName
                Case "PIPDF"

                    Dim PINO As String = detailView.GetFocusedRowCellValue("FTPINo").ToString()
                    cmdstring = " EXEC   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.USP_GETFILEPIPDF '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(PINO) & "'"

                Case Else

                    Dim pPDFSeq As Integer = Val(detailView.GetFocusedRowCellValue("FNFileID").ToString)

                    cmdstring = "Select TOP 1 FPFile "
                    cmdstring &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_PI_File AS A With(NOLOCK) "
                    cmdstring &= vbCrLf & " WHERE  A.FNFileID =" & pPDFSeq & "  "

            End Select

            Dim dt As DataTable = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_PUR)

            If dt.Rows.Count > 0 Then


                Try
                    Dim br As Byte() = dt.Rows(0)!FPFile
                    FilePdfViewerPI.LoadDocument(New MemoryStream(br))
                Catch ex As Exception

                End Try

            Else


            End If


            dt.Dispose()

        Catch ex As Exception
            Dim msg As String = ex.Message

        End Try
    End Sub

    Private Sub ogvlist_ShowingEditor(sender As Object, e As CancelEventArgs) Handles ogvlist.ShowingEditor
        Try

            Dim masterView As GridView = Me.ogvtime
            Dim detailView As GridView = TryCast(masterView.GetDetailView(masterView.FocusedRowHandle, 0), GridView)
            detailView.Focus()

            Select Case detailView.FocusedColumn.FieldName
                Case "PIPDF"
                    If detailView.GetFocusedRowCellValue("FTStatePIPDFFile").ToString = "1" Then
                        e.Cancel = False

                    Else
                        e.Cancel = True
                    End If
                Case "PDF"

                    If detailView.GetFocusedRowCellValue("FTStateFile").ToString = "1" Then
                        e.Cancel = False

                    Else
                        e.Cancel = True
                    End If

            End Select
        Catch ex As Exception

        End Try
    End Sub
End Class
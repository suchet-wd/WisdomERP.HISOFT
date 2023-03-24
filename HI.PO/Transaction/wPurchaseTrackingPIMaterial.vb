Imports DevExpress.Data
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraGrid.Views.Grid
Imports Microsoft.Office.Interop
Imports System.ComponentModel
Imports System.IO
Imports System.Windows.Forms

Public Class wPurchaseTrackingPIMaterial

    Private StateCal As Boolean = False
    Private _RowDataChange As Boolean
    Private GridDataBefore As String = ""
    Private GridDataInvoiceBefore As String = ""
    Private GridDataNoteBefore As String = ""

    Private _FormLoad As Boolean = True
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.





        With RepositoryItemDateEdit1

            RemoveHandler .Leave, AddressOf HI.TL.HandlerControl.RepositoryItemDate_Leave
            RemoveHandler .Click, AddressOf HI.TL.HandlerControl.RepositoryItemDate_GotFocus
            AddHandler .Leave, AddressOf ItemDate_Leave
            AddHandler .Click, AddressOf ItemDate_GotFocus

        End With

        Call InitGrid()

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
        Dim _dt As DataTable


        StateCal = False





        Dim _Spls As New HI.TL.SplashScreen("Loading...   Please Wait   ")

        _Qry = " EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.USP_GETDATAPO_MATERIAL_TRACKING '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & FNListDocumentTrackPIData.SelectedIndex.ToString() & ",'" & HI.UL.ULDate.ConvertEnDB(FTStartPurchaseDate.Text) & "','" & HI.UL.ULDate.ConvertEnDB(FTEndPurchaseDate.Text) & "','" & HI.UL.ULDate.ConvertEnDB(FTStartDelivery.Text) & "','" & HI.UL.ULDate.ConvertEnDB(FTEndDelivery.Text) & "','',''," & Val(FNHSysSuplId.Properties.Tag.ToString) & ""


        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PUR)

        Me.ogdtime.DataSource = _dt.Copy
        _dt.Dispose()
        _Spls.Close()

        _RowDataChange = False

    End Sub

    Private Function VerifyData() As Boolean
        Dim _Pass As Boolean = True

        Return _Pass
    End Function
#End Region

#Region "General"

    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            _FormLoad = False
            HI.UL.AppRegistry.LoadLayoutGridFromRegistry(Me, Me.ogvtime)




            With Me.ogvtime



                .Columns.ColumnByFieldName("FTStateSendMail").AppearanceCell.BackColor = Nothing





                .Columns.ColumnByFieldName("FTStateSendPIToAcc").OptionsColumn.AllowEdit = False
                .Columns.ColumnByFieldName("FTStateSendPIToAcc").AppearanceCell.BackColor = Nothing


                .Columns.ColumnByFieldName("FTStateFinishPO").OptionsColumn.AllowEdit = False


                .Columns.ColumnByFieldName("FTStateFinishPO").AppearanceCell.BackColor = Nothing


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



                .Columns.ColumnByFieldName("FTStatePaid").OptionsColumn.AllowEdit = False
                .Columns.ColumnByFieldName("FTPaidNote").OptionsColumn.AllowEdit = False


                .Columns.ColumnByFieldName("FTStatePaid").AppearanceCell.BackColor = Nothing
                    .Columns.ColumnByFieldName("FTPaidNote").AppearanceCell.BackColor = Nothing




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


        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmload.Click
        If VerifyData() Then
            ogdtime.DataSource = Nothing


            Call LoadData()
        End If
    End Sub

    Private Sub ocmclear_Click(sender As System.Object, e As System.EventArgs) Handles ocmclear.Click
        ogdtime.DataSource = Nothing


        HI.TL.HandlerControl.ClearControl(Me)
    End Sub

#End Region

    Private Sub ocmsavelayout_Click(sender As Object, e As EventArgs) Handles ocmsavelayout.Click
        HI.UL.AppRegistry.SaveLayoutGridToRegistry(Me, Me.ogvtime)
        HI.MG.ShowMsg.mInfo("Save Layout Grid Complete...", 1404240001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Information)
    End Sub

    Private Sub ogbheader_Click(sender As Object, e As EventArgs) Handles ogbheader.Click

    End Sub


    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub hideContainerTop_Click(sender As Object, e As EventArgs)

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
                    Dim MatId As Integer = Val(.GetRowCellValue(.FocusedRowHandle, "FNHSysRawMatId").ToString())

                    Dim FieldName As String = .FocusedColumn.FieldName.ToString

                    Dim cmdstring As String = ""

                    cmdstring = "  DECLARE @CountData int = 0 "
                    cmdstring &= vbCrLf & " update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_PI_Material_Tracking  Set "
                    cmdstring &= vbCrLf & "FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    cmdstring &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB & ""
                    cmdstring &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & ", "

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
                    cmdstring &= vbCrLf & " AND FNHSysRawMatId =" & MatId & "  "
                    cmdstring &= vbCrLf & " SET @CountData = @@ROWCOUNT  "
                    cmdstring &= vbCrLf & " IF @CountData <=0 "
                    cmdstring &= vbCrLf & "   BEGIN "

                    cmdstring &= vbCrLf & "        INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_PI_Material_Tracking(FTInsUser, FDInsDate, FTInsTime"
                    cmdstring &= vbCrLf & ", FTPurchaseNo, FNHSysRawMatId ,"

                    Select Case FieldName
                        Case "FTFacCheckCFMDeliveryDate1"

                            cmdstring &= vbCrLf & " FTFacCheckCFMDeliveryDate1"
                            cmdstring &= vbCrLf & ",FTFacCheckCFMDeliveryDate1By"
                            cmdstring &= vbCrLf & ",FTFacCheckCFMDeliveryDate1Date"
                            cmdstring &= vbCrLf & ",FTFacCheckCFMDeliveryDate1Time"
                        Case "FTFacCheckCFMDeliveryDate2"

                            cmdstring &= vbCrLf & " FTFacCheckCFMDeliveryDate2"
                            cmdstring &= vbCrLf & ",FTFacCheckCFMDeliveryDate2By"
                            cmdstring &= vbCrLf & ",FTFacCheckCFMDeliveryDate2Date"
                            cmdstring &= vbCrLf & ",FTFacCheckCFMDeliveryDate2Time"
                        Case "FTFacCheckCFMDeliveryDateFinal"

                            cmdstring &= vbCrLf & " FTFacCheckCFMDeliveryDateFinal"
                            cmdstring &= vbCrLf & ",FTFacCheckCFMDeliveryDateFinalBy"
                            cmdstring &= vbCrLf & ",FTFacCheckCFMDeliveryDateFinalDate"
                            cmdstring &= vbCrLf & ",FTFacCheckCFMDeliveryDateFinalTime "
                        Case "FTWarehouseDate"

                            cmdstring &= vbCrLf & " FTWarehouseDate"
                            cmdstring &= vbCrLf & ",FTWarehouseDateBy"
                            cmdstring &= vbCrLf & ",FTWarehouseDateDate"
                            cmdstring &= vbCrLf & ",FTWarehouseDateTime"
                        Case "FTImpactedGacDate"

                            cmdstring &= vbCrLf & " FTImpactedGacDate"
                            cmdstring &= vbCrLf & ",FTImpactedGacDatey"
                            cmdstring &= vbCrLf & ",FTImpactedGacDateDate"
                            cmdstring &= vbCrLf & ",FTImpactedGacDateTime"

                        Case "FTBaseOnLeadTimeDeliveryDate"

                            cmdstring &= vbCrLf & " FTBaseOnLeadTimeDeliveryDate"
                            cmdstring &= vbCrLf & ",FTBaseOnLeadTimeDeliveryDateBy"
                            cmdstring &= vbCrLf & ",FTBaseOnLeadTimeDeliveryDateDate"
                            cmdstring &= vbCrLf & ",FTBaseOnLeadTimeDeliveryDateTime"

                        Case "FTETD"

                            cmdstring &= vbCrLf & " FTETD"
                            cmdstring &= vbCrLf & ",FTETDBy"
                            cmdstring &= vbCrLf & ",FTETDDate"
                            cmdstring &= vbCrLf & ",FTETDTime "
                        Case "FTETA"

                            cmdstring &= vbCrLf & " FTETA"
                            cmdstring &= vbCrLf & ",FTETABy"
                            cmdstring &= vbCrLf & ",FTETADate"
                            cmdstring &= vbCrLf & ",FTETATime "
                    End Select

                    cmdstring &= vbCrLf & " )  "
                    cmdstring &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                    cmdstring &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & " "
                    cmdstring &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & " "
                    cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(OrderNo) & "' "
                    cmdstring &= vbCrLf & "," & MatId & ",  "

                    Select Case FieldName
                        Case "FTFacCheckCFMDeliveryDate1"

                            cmdstring &= vbCrLf & " '" & HI.UL.ULDate.ConvertEnDB(NewData) & "'"
                            cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                            cmdstring &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                            cmdstring &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & " "
                        Case "FTFacCheckCFMDeliveryDate2"

                            cmdstring &= vbCrLf & " '" & HI.UL.ULDate.ConvertEnDB(NewData) & "'"
                            cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                            cmdstring &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                            cmdstring &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & " "
                        Case "FTFacCheckCFMDeliveryDateFinal"

                            cmdstring &= vbCrLf & " '" & HI.UL.ULDate.ConvertEnDB(NewData) & "'"
                            cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                            cmdstring &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                            cmdstring &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & " "
                        Case "FTWarehouseDate"

                            cmdstring &= vbCrLf & " '" & HI.UL.ULDate.ConvertEnDB(NewData) & "'"
                            cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                            cmdstring &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                            cmdstring &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & " "
                        Case "FTImpactedGacDate"

                            cmdstring &= vbCrLf & " '" & HI.UL.ULDate.ConvertEnDB(NewData) & "'"
                            cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                            cmdstring &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                            cmdstring &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & " "

                        Case "FTBaseOnLeadTimeDeliveryDate"

                            cmdstring &= vbCrLf & " '" & HI.UL.ULDate.ConvertEnDB(NewData) & "'"
                            cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                            cmdstring &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                            cmdstring &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & " "

                        Case "FTETD"

                            cmdstring &= vbCrLf & " '" & HI.UL.ULDate.ConvertEnDB(NewData) & "'"
                            cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                            cmdstring &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                            cmdstring &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & " "
                        Case "FTETA"

                            cmdstring &= vbCrLf & " '" & HI.UL.ULDate.ConvertEnDB(NewData) & "'"
                            cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                            cmdstring &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                            cmdstring &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & " "

                    End Select

                    cmdstring &= vbCrLf & " SET @CountData = @@ROWCOUNT  "
                    cmdstring &= vbCrLf & "   END "

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
        'Select Case ogvtime.FocusedColumn.FieldName.ToString
        '    Case "FTStateFinishPO"

        '        Dim PoNo As String = ogvtime.GetRowCellValue(ogvtime.FocusedRowHandle, "FTPurchaseNo").ToString()

        '        Dim FieldName As String = ogvtime.FocusedColumn.FieldName.ToString
        '        Dim State As String = "0"
        '        If e.NewValue.ToString = "1" Then
        '            State = "1"
        '        End If
        '        ogvtime.SetRowCellValue(ogvtime.FocusedRowHandle, ogvtime.FocusedColumn.FieldName.ToString, State)


        '        Dim cmdstring As String = ""

        '        cmdstring = "update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase  Set "
        '        cmdstring &= vbCrLf & " FTStateFinishPO='" & HI.UL.ULF.rpQuoted(State) & "'"
        '        cmdstring &= vbCrLf & ",FTSendFinishPOBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
        '        cmdstring &= vbCrLf & ",FTSendFinishPODate=" & HI.UL.ULDate.FormatDateDB & ""
        '        cmdstring &= vbCrLf & ",FTSendFinishPOTime=" & HI.UL.ULDate.FormatTimeDB & " "
        '        cmdstring &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(PoNo) & "'  "
        '        cmdstring &= vbCrLf & " select top 1 FTStateFinishPO,FTSendFinishPOBy,CASE WHEN ISDATE(FTSendFinishPODate) = 1 THEN Convert(varchar(10),Convert(datetime,FTSendFinishPODate),103) ELSE '' END AS  FTSendFinishPODate,FTSendFinishPOTime "
        '        cmdstring &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase   "
        '        cmdstring &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(PoNo) & "'  "

        '        Dim dt As DataTable = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_PUR)

        '        For Each R As DataRow In dt.Rows

        '            ogvtime.SetRowCellValue(ogvtime.FocusedRowHandle, "FTStateFinishPO", R!FTStateFinishPO.ToString)
        '            ogvtime.SetRowCellValue(ogvtime.FocusedRowHandle, "FTSendFinishPOBy", R!FTSendFinishPOBy.ToString)
        '            ogvtime.SetRowCellValue(ogvtime.FocusedRowHandle, "FTSendFinishPODate", R!FTSendFinishPODate.ToString)
        '            ogvtime.SetRowCellValue(ogvtime.FocusedRowHandle, "FTSendFinishPOTime", R!FTSendFinishPOTime.ToString)

        '            Exit For
        '        Next

        '        dt.Dispose()

        '    Case "FTStateSendPIToAcc"


        '        Dim PoNo As String = ogvtime.GetRowCellValue(ogvtime.FocusedRowHandle, "FTPurchaseNo").ToString()

        '        Dim FieldName As String = ogvtime.FocusedColumn.FieldName.ToString
        '        Dim State As String = "0"
        '        If e.NewValue.ToString = "1" Then
        '            State = "1"
        '        End If
        '        ogvtime.SetRowCellValue(ogvtime.FocusedRowHandle, ogvtime.FocusedColumn.FieldName.ToString, State)


        '        Dim cmdstring As String = ""

        '        cmdstring = "update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase  Set "
        '        cmdstring &= vbCrLf & " FTStateSendPIToAcc='" & HI.UL.ULF.rpQuoted(State) & "'"
        '        cmdstring &= vbCrLf & ",FTSendPIToAccBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
        '        cmdstring &= vbCrLf & ",FTSendPIToAccDate=" & HI.UL.ULDate.FormatDateDB & ""
        '        cmdstring &= vbCrLf & ",FTSendPIToAccTime=" & HI.UL.ULDate.FormatTimeDB & " "
        '        cmdstring &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(PoNo) & "'  "
        '        cmdstring &= vbCrLf & " select top 1 FTStateSendPIToAcc,FTSendPIToAccBy,CASE WHEN ISDATE(FTSendPIToAccDate) = 1 THEN Convert(varchar(10),Convert(datetime,FTSendPIToAccDate),103) ELSE '' END AS FTSendPIToAccDate,FTSendPIToAccTime "
        '        cmdstring &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase   "
        '        cmdstring &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(PoNo) & "'  "

        '        Dim dt As DataTable = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_PUR)

        '        For Each R As DataRow In dt.Rows

        '            ogvtime.SetRowCellValue(ogvtime.FocusedRowHandle, "FTStateSendPIToAcc", R!FTStateSendPIToAcc.ToString)
        '            ogvtime.SetRowCellValue(ogvtime.FocusedRowHandle, "FTSendPIToAccBy", R!FTSendPIToAccBy.ToString)
        '            ogvtime.SetRowCellValue(ogvtime.FocusedRowHandle, "FTSendPIToAccDate", R!FTSendPIToAccDate.ToString)
        '            ogvtime.SetRowCellValue(ogvtime.FocusedRowHandle, "FTSendPIToAccTime", R!FTSendPIToAccTime.ToString)

        '            Exit For
        '        Next

        '        dt.Dispose()

        '    Case "FTStateSendMail"


        '        Dim PoNo As String = ogvtime.GetRowCellValue(ogvtime.FocusedRowHandle, "FTPurchaseNo").ToString()

        '        Dim FieldName As String = ogvtime.FocusedColumn.FieldName.ToString
        '        Dim State As String = "0"
        '        If e.NewValue.ToString = "1" Then
        '            State = "1"
        '        End If

        '        ogvtime.SetRowCellValue(ogvtime.FocusedRowHandle, ogvtime.FocusedColumn.FieldName.ToString, State)


        '        Dim cmdstring As String = ""



        '        cmdstring = "update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase  Set "
        '        cmdstring &= vbCrLf & " FTStateSendMail='" & HI.UL.ULF.rpQuoted(State) & "'"
        '        cmdstring &= vbCrLf & ",FTSendMailBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
        '        cmdstring &= vbCrLf & ",FTSendMailDate=" & HI.UL.ULDate.FormatDateDB & ""
        '        cmdstring &= vbCrLf & ",FTSendMailTime=" & HI.UL.ULDate.FormatTimeDB & " "
        '        cmdstring &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(PoNo) & "'  "
        '        cmdstring &= vbCrLf & " select top 1 FTStateSendMail,FTSendMailBy,CASE WHEN ISDATE(FTSendMailDate) = 1 THEN Convert(varchar(10),Convert(datetime,FTSendMailDate),103) ELSE '' END AS FTSendMailDate,FTSendMailTime "
        '        cmdstring &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase   "
        '        cmdstring &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(PoNo) & "'  "

        '        Dim dt As DataTable = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_PUR)

        '        For Each R As DataRow In dt.Rows

        '            ogvtime.SetRowCellValue(ogvtime.FocusedRowHandle, "FTStateSendMail", R!FTStateSendMail.ToString)
        '            ogvtime.SetRowCellValue(ogvtime.FocusedRowHandle, "FTSendMailBy", R!FTSendMailBy.ToString)
        '            ogvtime.SetRowCellValue(ogvtime.FocusedRowHandle, "FTSendMailDate", R!FTSendMailDate.ToString)
        '            ogvtime.SetRowCellValue(ogvtime.FocusedRowHandle, "FTSendMailTime", R!FTSendMailTime.ToString)

        '            Exit For
        '        Next

        '        dt.Dispose()

        'End Select
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
                    Dim MatId As String = .GetRowCellValue(.FocusedRowHandle, "FNHSysRawMatId").ToString()

                    Dim FieldName As String = .FocusedColumn.FieldName.ToString

                    Dim cmdstring As String = ""

                    cmdstring = "  DECLARE @CountData int = 0 "
                    cmdstring &= vbCrLf & " update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_PI_Material_Tracking  Set "
                    cmdstring &= vbCrLf & " FTInvoiceNo='" & HI.UL.ULF.rpQuoted(NewData) & "'"
                    cmdstring &= vbCrLf & ",FTInvoiceNoBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    cmdstring &= vbCrLf & ",FTInvoiceNoDate=" & HI.UL.ULDate.FormatDateDB & ""
                    cmdstring &= vbCrLf & ",FTInvoiceNoTime=" & HI.UL.ULDate.FormatTimeDB & " "
                    cmdstring &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(OrderNo) & "'  "
                    cmdstring &= vbCrLf & " AND FNHSysRawMatId=" & MatId & "  "
                    cmdstring &= vbCrLf & " SET @CountData = @@ROWCOUNT  "
                    cmdstring &= vbCrLf & " IF @CountData <=0 "
                    cmdstring &= vbCrLf & "   BEGIN "

                    cmdstring &= vbCrLf & "        INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_PI_Material_Tracking(FTInsUser, FDInsDate, FTInsTime"
                    cmdstring &= vbCrLf & ", FTPurchaseNo, FNHSysRawMatId ,"
                    cmdstring &= vbCrLf & " FTInvoiceNo"
                    cmdstring &= vbCrLf & ",FTInvoiceNoBy"
                    cmdstring &= vbCrLf & ",FTInvoiceNoDate"
                    cmdstring &= vbCrLf & ",FTInvoiceNoTime"
                    cmdstring &= vbCrLf & " )  "
                    cmdstring &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                    cmdstring &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & " "
                    cmdstring &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & " "
                    cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(OrderNo) & "' "
                    cmdstring &= vbCrLf & "," & MatId & "  "
                    cmdstring &= vbCrLf & ", '" & HI.UL.ULF.rpQuoted(NewData) & "'"
                    cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    cmdstring &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                    cmdstring &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & " "


                    cmdstring &= vbCrLf & " SET @CountData = @@ROWCOUNT  "
                    cmdstring &= vbCrLf & "   END "

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
        End Select
    End Sub


    Private Sub FNListDocumentTrackPIData_SelectedIndexChanged(sender As Object, e As EventArgs) Handles FNListDocumentTrackPIData.SelectedIndexChanged
        If _FormLoad = False Then
            Call HI.UL.AppRegistry.WriteRegistry("ListDoc" & Me.Name, FNListDocumentTrackPIData.SelectedIndex.ToString)
        End If
    End Sub


End Class
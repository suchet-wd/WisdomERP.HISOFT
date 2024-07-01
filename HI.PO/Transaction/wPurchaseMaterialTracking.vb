Imports DevExpress.Data
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraGrid.Views.Grid
Imports Microsoft.Office.Interop
Imports System.ComponentModel
Imports System.IO
Imports System.Windows.Forms

Public Class wPurchaseMaterialTracking

    Private StateCal As Boolean = False
    Private _RowDataChange As Boolean
    Private GridDataBefore As String = ""
    Private GridDataInvoiceBefore As String = ""
    Private GridDataNoteBefore As String = ""

    Private GridDataQtyBefore As Double = 0

    Private wAddtracking As wPurchaseTrackingPIAddTracking
    Private _FormLoad As Boolean = True

    Private pCountMaxApp As Integer = 0

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        wAddtracking = New wPurchaseTrackingPIAddTracking
        HI.TL.HandlerControl.AddHandlerObj(wAddtracking)

        Dim oSysLang As New ST.SysLanguage
        Try
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, wAddtracking.Name.ToString.Trim, wAddtracking)
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
        Dim _dt As DataTable
        StateCal = False

        Me.ogdtime.DataSource = Nothing
        Dim _Spls As New HI.TL.SplashScreen("Loading...   Please Wait   ")

        Try
            If FNDataType.SelectedIndex <> 2 Then
                _Qry = " EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.USP_GETDATAPO_MATERIAL_DELAY_PROCESS '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) &
                "'," & FNListDocumentTrackPIData.SelectedIndex.ToString() & ",'" & HI.UL.ULDate.ConvertEnDB(FTStartPurchaseDate.Text) & "','" & HI.UL.ULDate.ConvertEnDB(FTEndPurchaseDate.Text) &
                "','" & HI.UL.ULDate.ConvertEnDB(FTStartDelivery.Text) & "','" & HI.UL.ULDate.ConvertEnDB(FTEndDelivery.Text) & "','',''," & Val(FNHSysSuplId.Properties.Tag.ToString) & "," &
                Val(FNHSysBuyId.Properties.Tag.ToString) & "," & FNDataType.SelectedIndex & ",'" & HI.UL.ULDate.ConvertEnDB(FTStartOrderDate.Text) & "','" &
                HI.UL.ULDate.ConvertEnDB(FTEndOrderDate.Text) & "'"

            Else
                ''_Qry = " EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.USP_GETDATAPO_MATERIAL_DELAY_PROCESS_SAMPLE '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & FNListDocumentTrackPIData.SelectedIndex.ToString() & ",'" & HI.UL.ULDate.ConvertEnDB(FTStartPurchaseDate.Text) & "','" & HI.UL.ULDate.ConvertEnDB(FTEndPurchaseDate.Text) & "','" & HI.UL.ULDate.ConvertEnDB(FTStartDelivery.Text) & "','" & HI.UL.ULDate.ConvertEnDB(FTEndDelivery.Text) & "','',''," & Val(FNHSysSuplId.Properties.Tag.ToString) & "," & Val(FNHSysBuyId.Properties.Tag.ToString) & "," & FNDataType.SelectedIndex & ",'" & HI.UL.ULDate.ConvertEnDB(FTStartOrderDate.Text) & "','" & HI.UL.ULDate.ConvertEnDB(FTEndOrderDate.Text) & "'"
                '_Qry = "EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.USP_GETDATAPO_MATERIAL_DELAY_PROCESS_SAMPLE " & vbCrLf
                '_Qry &= "@UserName = '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "', @FNDataType = " & FNListDocumentTrackPIData.SelectedIndex.ToString() & vbCrLf
                '_Qry &= ", @FNSSupl = " & Val(FNHSysSuplId.Properties.Tag.ToString) & ", @FNBuy = " & Val(FNHSysBuyId.Properties.Tag.ToString) & ", @DocType = 2" & vbCrLf
                '_Qry &= ", @SDate = '" & HI.UL.ULDate.ConvertEnDB(FTStartPurchaseDate.Text) & "' , @EDate = '" & HI.UL.ULDate.ConvertEnDB(FTEndPurchaseDate.Text) & "'" & vbCrLf
                '_Qry &= ", @SDeliDate= '" & HI.UL.ULDate.ConvertEnDB(FTStartDelivery.Text) & "', @EDeliDate = '" & HI.UL.ULDate.ConvertEnDB(FTEndDelivery.Text) & "' " & vbCrLf
                '_Qry &= ", @OrderSDate = '" & HI.UL.ULDate.ConvertEnDB(FTStartOrderDate.Text) & "', @OrderEDate = '" & HI.UL.ULDate.ConvertEnDB(FTEndOrderDate.Text) & "'" & vbCrLf
                ''_Qry &= ", @DevConfirmSDate = '" & HI.UL.ULDate.ConvertEnDB(FTStartDevCFMDate.Text) & "', @DevConfirmEDate = '" & HI.UL.ULDate.ConvertEnDB(FTEndDevCFMDate.Text) & "'"
                ''_Qry &= ", @SRConfirmSDate = '" & HI.UL.ULDate.ConvertEnDB(FTStartSRConfirmDate.Text) & "', @SRConfirmEDate = '" & HI.UL.ULDate.ConvertEnDB(FTEndSRConfirmDate.Text) & "'"
                '', @SPO = '', @EPO = ''

                _Qry = "EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.USP_GETDATAPO_MATERIAL_DELAY_PROCESS_SAMPLE " & vbCrLf
                _Qry &= "@UserName = '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "', @FNDataType = " & FNListDocumentTrackPIData.SelectedIndex.ToString() & vbCrLf
                _Qry &= ", @FNSSupl = " & Val(FNHSysSuplId.Properties.Tag.ToString) & ", @FNBuy = " & Val(FNHSysBuyId.Properties.Tag.ToString) & ", @DocType = 2" & vbCrLf
                If HI.UL.ULDate.ConvertEnDB(FTStartPurchaseDate.Text) <> "" And HI.UL.ULDate.ConvertEnDB(FTEndPurchaseDate.Text) <> "" Then
                    _Qry &= ", @SDate = '" & HI.UL.ULDate.ConvertEnDB(FTStartPurchaseDate.Text) & "' , @EDate = '" & HI.UL.ULDate.ConvertEnDB(FTEndPurchaseDate.Text) & "'" & vbCrLf
                End If
                If HI.UL.ULDate.ConvertEnDB(FTStartDelivery.Text) <> "" And HI.UL.ULDate.ConvertEnDB(FTEndDelivery.Text) <> "" Then
                    _Qry &= ", @SDeliDate= '" & HI.UL.ULDate.ConvertEnDB(FTStartDelivery.Text) & "', @EDeliDate = '" & HI.UL.ULDate.ConvertEnDB(FTEndDelivery.Text) & "' " & vbCrLf

                End If
                If HI.UL.ULDate.ConvertEnDB(FTStartOrderDate.Text) <> "" And HI.UL.ULDate.ConvertEnDB(FTEndOrderDate.Text) <> "" Then
                    _Qry &= ", @OrderSDate = '" & HI.UL.ULDate.ConvertEnDB(FTStartOrderDate.Text) & "', @OrderEDate = '" & HI.UL.ULDate.ConvertEnDB(FTEndOrderDate.Text) & "'" & vbCrLf
                End If
                If HI.UL.ULDate.ConvertEnDB(FTStartDevCFMDate.Text) <> "" And HI.UL.ULDate.ConvertEnDB(FTEndDevCFMDate.Text) <> "" Then
                    _Qry &= ", @DevConfirmSDate = '" & HI.UL.ULDate.ConvertEnDB(FTStartDevCFMDate.Text) & "', @DevConfirmEDate = '" & HI.UL.ULDate.ConvertEnDB(FTEndDevCFMDate.Text) & "'"
                End If

            End If
            _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PUR)

            Me.ogdtime.DataSource = _dt.Copy
            _dt.Dispose()

        Catch ex As Exception
            Me.ogdtime.DataSource = Nothing
        End Try

        _Spls.Close()

        _RowDataChange = False

    End Sub

    'Private Function BindToStoredProcedure() As SqlDataSource
    '    Try

    '        Dim pSerVerName As String = ""
    '        Dim pUId As String = ""
    '        Dim pUpws As String = ""
    '        Dim pDataBase As String = ""

    '        HI.Conn.DB.UsedDB(Conn.DB.DataBaseName.DB_PUR)

    '        pSerVerName = HI.Conn.DB.SerVerName
    '        pDataBase = HI.Conn.DB.BaseName
    '        pUId = HI.Conn.DB.UIDName
    '        pUpws = HI.Conn.DB.PWDName

    '        Dim ds As SqlDataSource = New SqlDataSource()

    '        ds.ConnectionName = "DataBaseConnectionName"
    '        ds.ConnectionParameters = New MsSqlConnectionParameters(pSerVerName, pDataBase, pUId, pUpws, MsSqlAuthorizationType.SqlServer)

    '        Dim spQuery As DevExpress.DataAccess.Sql.StoredProcQuery = New StoredProcQuery("spQueryStore", "USP_GETDATAPO_MATERIAL_DELAY_PROCESS")

    '        spQuery.Parameters.Add(New QueryParameter(
    '                               name:="@User",
    '                               type:=GetType(String),
    '                               value:=HI.ST.UserInfo.UserName))

    '        spQuery.Parameters.Add(New QueryParameter(
    '                               name:="@FNDataType",
    '                               type:=GetType(Integer),
    '                               value:=Val(FNListDocumentTrackPIData.SelectedIndex.ToString())))

    '        spQuery.Parameters.Add(New QueryParameter(
    '                               name:="@SDate",
    '                               type:=GetType(String),
    '                               value:=HI.UL.ULDate.ConvertEnDB(FTStartPurchaseDate.Text)))

    '        spQuery.Parameters.Add(New QueryParameter(
    '                               name:="@EDate",
    '                               type:=GetType(String),
    '                               value:=HI.UL.ULDate.ConvertEnDB(FTEndPurchaseDate.Text)))

    '        spQuery.Parameters.Add(New QueryParameter(
    '                               name:="@SDeliDate",
    '                               type:=GetType(String),
    '                               value:=HI.UL.ULDate.ConvertEnDB(FTStartDelivery.Text)))

    '        spQuery.Parameters.Add(New QueryParameter(
    '                               name:="@EDeliDate",
    '                               type:=GetType(String),
    '                               value:=HI.UL.ULDate.ConvertEnDB(FTEndDelivery.Text)))

    '        spQuery.Parameters.Add(New QueryParameter(
    '                               name:="@SPO",
    '                               type:=GetType(String),
    '                               value:=""))

    '        spQuery.Parameters.Add(New QueryParameter(
    '                               name:="@EPO",
    '                               type:=GetType(String),
    '                               value:=""))

    '        spQuery.Parameters.Add(New QueryParameter(
    '                               name:="@FNSSupl",
    '                               type:=GetType(Integer),
    '                               value:=Val(FNHSysSuplId.Properties.Tag.ToString)))


    '        spQuery.Parameters.Add(New QueryParameter(
    '                               name:="@FNBuy",
    '                               type:=GetType(Integer),
    '                               value:=Val(FNHSysBuyId.Properties.Tag.ToString)))

    '        spQuery.Parameters.Add(New QueryParameter(
    '                               name:="@DocType",
    '                               type:=GetType(Integer),
    '                               value:=FNDataType.SelectedIndex))

    '        spQuery.Parameters.Add(New QueryParameter(
    '                               name:="@OrderSDate",
    '                               type:=GetType(String),
    '                               value:=HI.UL.ULDate.ConvertEnDB(FTStartOrderDate.Text)))

    '        spQuery.Parameters.Add(New QueryParameter(
    '                               name:="@OrderEDate",
    '                               type:=GetType(String),
    '                               value:=HI.UL.ULDate.ConvertEnDB(FTEndOrderDate.Text)))

    '        ds.Queries.Add(spQuery)
    '        ds.Fill()

    '        Return ds
    '    Catch ex As Exception
    '        Return Nothing
    '    End Try

    'End Function

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

            Dim StateShowSelect As Boolean = (ocmsendmail.Enabled OrElse ocmsavetrack.Enabled)
            ochkselectall.Visible = StateShowSelect


            With Me.ogvtime
                .Columns.ColumnByFieldName("FTSelect").Visible = StateShowSelect
                .Columns.ColumnByFieldName("FTSelect").OptionsColumn.ShowInCustomizationForm = StateShowSelect


                .Columns.ColumnByFieldName("FTStateSendMail").OptionsColumn.AllowEdit = (Me.ocmsendmail.Enabled)

                If ocmsendmail.Enabled = False Then
                    .Columns.ColumnByFieldName("FTStateSendMail").AppearanceCell.BackColor = Nothing
                End If



                .Columns.ColumnByFieldName("FTORGOETCDate").OptionsColumn.AllowEdit = (Me.ocmFTORGOETCDate.Enabled)

                If Me.ocmFTORGOETCDate.Enabled = False Then
                    .Columns.ColumnByFieldName("FTORGOETCDate").AppearanceCell.BackColor = Nothing
                End If


                .Columns.ColumnByFieldName("FTFinalOETCDate").OptionsColumn.AllowEdit = (Me.ocmFTFinalOETCDate.Enabled)

                If Me.ocmFTFinalOETCDate.Enabled = False Then
                    .Columns.ColumnByFieldName("FTFinalOETCDate").AppearanceCell.BackColor = Nothing
                End If

                .Columns.ColumnByFieldName("FNPOCFMQuantity").OptionsColumn.AllowEdit = (Me.ocmpurchase.Enabled)
                .Columns.ColumnByFieldName("FTPOCFMNote").OptionsColumn.AllowEdit = (Me.ocmpurchase.Enabled)
                .Columns.ColumnByFieldName("FTInvoiceNo").OptionsColumn.AllowEdit = (Me.ocmpurchase.Enabled)
                .Columns.ColumnByFieldName("FTInvoiceNote").OptionsColumn.AllowEdit = (Me.ocmpurchase.Enabled)
                .Columns.ColumnByFieldName("FTSendSMPDate").OptionsColumn.AllowEdit = (Me.ocmpurchase.Enabled)
                .Columns.ColumnByFieldName("FTSendSMPStatus").OptionsColumn.AllowEdit = (Me.ocmpurchase.Enabled)
                .Columns.ColumnByFieldName("FTSendSMPRemark").OptionsColumn.AllowEdit = (Me.ocmpurchase.Enabled)
                .Columns.ColumnByFieldName("FTSendSMPAWB").OptionsColumn.AllowEdit = (Me.ocmpurchase.Enabled)
                .Columns.ColumnByFieldName("FTSendSMPPayType").OptionsColumn.AllowEdit = (Me.ocmpurchase.Enabled)


                If Me.ocmpurchase.Enabled = False Then
                    .Columns.ColumnByFieldName("FNPOCFMQuantity").AppearanceCell.BackColor = Nothing
                    .Columns.ColumnByFieldName("FTPOCFMNote").AppearanceCell.BackColor = Nothing

                    .Columns.ColumnByFieldName("FTInvoiceNo").AppearanceCell.BackColor = Nothing
                    .Columns.ColumnByFieldName("FTInvoiceNote").AppearanceCell.BackColor = Nothing
                    .Columns.ColumnByFieldName("FTSendSMPDate").AppearanceCell.BackColor = Nothing
                    .Columns.ColumnByFieldName("FTSendSMPStatus").AppearanceCell.BackColor = Nothing
                    .Columns.ColumnByFieldName("FTSendSMPRemark").AppearanceCell.BackColor = Nothing
                    .Columns.ColumnByFieldName("FTSendSMPAWB").AppearanceCell.BackColor = Nothing
                    .Columns.ColumnByFieldName("FTSendSMPPayType").AppearanceCell.BackColor = Nothing
                End If


                .Columns.ColumnByFieldName("FNPROCFMQuantity").OptionsColumn.AllowEdit = (Me.ocmproduction.Enabled)
                .Columns.ColumnByFieldName("FTPROCFMNote").OptionsColumn.AllowEdit = (Me.ocmproduction.Enabled)

                If Me.ocmproduction.Enabled = False Then
                    .Columns.ColumnByFieldName("FNPROCFMQuantity").AppearanceCell.BackColor = Nothing
                    .Columns.ColumnByFieldName("FTPROCFMNote").AppearanceCell.BackColor = Nothing
                End If


                .Columns.ColumnByFieldName("FTDelayReasonsCode").OptionsColumn.AllowEdit = (Me.ocmDelayReasons.Enabled)

                If Me.ocmDelayReasons.Enabled = False Then
                    .Columns.ColumnByFieldName("FTDelayReasonsCode").AppearanceCell.BackColor = Nothing
                End If



                .Columns.ColumnByFieldName("FTFurtherDelayReasonCode").OptionsColumn.AllowEdit = (Me.ocmFurtherDelayReason.Enabled)

                If Me.ocmFurtherDelayReason.Enabled = False Then
                    .Columns.ColumnByFieldName("FTFurtherDelayReasonCode").AppearanceCell.BackColor = Nothing
                End If
                .Columns.ColumnByFieldName("FTTrackNote").AppearanceCell.BackColor = Nothing

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


            Indx = 0

            Try
                Indx = Val(HI.UL.AppRegistry.ReadRegistry("ListDDataType" & Me.Name))
            Catch ex As Exception
            End Try

            FNDataType.SelectedIndex = Indx

            RepositoryItemGridLookUpEditFTDelayReasonsCode.View.OptionsView.ShowAutoFilterRow = True
            RepositoryItemGridLookUpEditFTFurtherDelayReasonCode.View.OptionsView.ShowAutoFilterRow = True

            LoadMaster()

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

        LoadMaster()

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

    Private Sub ocmmail_Click(sender As Object, e As EventArgs) Handles ocmsendmail.Click


        'With New wPurchaseTrackingPIMail
        '    .ShowDialog()
        'End With
        Dim _Sql As String = ""

        Dim _CheckPath As String = "C:\WISDOMPOPDF"


        Dim pPathPDF As String = ""


        _Sql = "select top 1 FTCfgData   "
        _Sql &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSESystemConfig With(NOLOCK) "
        _Sql &= vbCrLf & "  Where (FTCfgName = N'POPDF')"

        pPathPDF = HI.Conn.SQLConn.GetField(_Sql, Conn.DB.DataBaseName.DB_SECURITY, "")

        Try

            If Directory.Exists(_CheckPath) = False Then
                Directory.CreateDirectory(_CheckPath)
            End If

        Catch ex As Exception

            MsgBox(ex.Message)

            Exit Sub
        End Try


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

                If dtpo.Select("FTSelect='1' AND FNDocType=0  AND ( FTStateSuperVisorApp='1' OR FTStatePDFWaitPrice='1' ) ").Length <= 0 Then

                    Exit Sub
                End If

                dtpoList = dtpo.Select("FTSelect='1' AND FNDocType=0  AND ( FTStateSuperVisorApp='1' OR FTStatePDFWaitPrice='1' ) ").CopyToDataTable

            End If


            Dim _FTMail As String = ""
            Dim _FTMailCC As String = ""
            Dim TemplateMail As String = ""

            Dim PoNo As String = ""
            Dim PoAllNo As String = ""
            Dim UpdatePoAllNo As String = ""
            Dim PoState As Integer = 0

            Dim grp As List(Of Integer) = (dtpoList.Select("FNHSysSuplId>0 AND FNDocType=0", "FNHSysSuplId").CopyToDataTable).AsEnumerable() _
                                                      .Select(Function(r) r.Field(Of Integer)("FNHSysSuplId")) _
                                                      .Distinct() _
                                                      .ToList()


            For Each Ind As Integer In grp

                PoAllNo = ""
                UpdatePoAllNo = ""

                _FTMail = ""
                _FTMailCC = ""
                TemplateMail = ""

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

                Dim _Spls As New HI.TL.SplashScreen("Creating....Mail Please Wait.")
                Try

                    Dim OutlookMessage As Outlook.MailItem
                    Dim AppOutlook As New Outlook.Application
                    Dim objNS As Outlook._NameSpace = AppOutlook.Session
                    Dim objFolder As Outlook.MAPIFolder
                    Dim oInsp As Outlook.Inspector
                    Dim mySignature As String
                    Dim StateFoundPDF As Boolean = False
                    Dim Str_Doc_Name As String
                    Dim StatePDF As Boolean = False
                    Dim cmdstring As String = ""

                    objFolder = objNS.GetDefaultFolder(Outlook.OlDefaultFolders.olFolderDrafts)

                    Try
                        OutlookMessage = AppOutlook.CreateItem(Outlook.OlItemType.olMailItem)

                        Try
                            With OutlookMessage


                                Dim _tmplang As HI.ST.Lang.eLang = HI.ST.Lang.Language
                                Try



                                    Dim grplistpo As List(Of String) = (dtpoList.Select("FNHSysSuplId=" & Val(Ind) & " AND FNDocType=0 ", "FTPurchaseNo").CopyToDataTable).AsEnumerable() _
                                                          .Select(Function(r) r.Field(Of String)("FTPurchaseNo")) _
                                                          .Distinct() _
                                                          .ToList()

                                    For Each POInd As String In grplistpo

                                        For Each R As DataRow In dtpoList.Select("FNHSysSuplId = " & Val(Ind) & " AND FTPurchaseNo='" & POInd & "' AND FNDocType=0 ")

                                            PoNo = R!FTPurchaseNo.ToString
                                            PoState = Val(R!FNPoState.ToString)



                                            If PoAllNo = "" Then

                                                PoAllNo = PoNo
                                                UpdatePoAllNo = PoNo
                                            Else
                                                PoAllNo = PoAllNo & "," & PoNo
                                                UpdatePoAllNo = UpdatePoAllNo & "','" & PoNo
                                            End If

                                            StateFoundPDF = False
                                            Str_Doc_Name = ""

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

                                                End With

                                                Str_Doc_Name = _CheckPath & "\" & PoNo & ".pdf"

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



                                            Exit For
                                        Next

                                    Next

                                Catch ex As Exception
                                End Try

                                .Display()
                                .To = _FTMail
                                .CC = _FTMailCC
                                .Subject = PoAllNo

                                Try
                                    oInsp = .GetInspector
                                Catch ex As Exception

                                End Try

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


                            End With
                        Catch ex As Exception
                            _Spls.Close()
                        End Try

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
                            cmdstring &= vbCrLf & "  WHERE FTPurchaseNo IN ('" & UpdatePoAllNo & "')"
                            cmdstring &= vbCrLf & " select  FTPurchaseNo,FTStateSendMail,FTSendMailBy,CASE WHEN ISDATE(FTSendMailDate) = 1 THEN Convert(varchar(10),Convert(datetime,FTSendMailDate),103) ELSE '' END AS  FTSendMailDate,FTSendMailTime "
                            cmdstring &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase   "
                            cmdstring &= vbCrLf & "  WHERE FTPurchaseNo IN ('" & UpdatePoAllNo & "')"

                            Dim dt As DataTable = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_PUR)

                            For Each R As DataRow In dt.Rows


                                With CType(Me.ogdtime.DataSource, DataTable)
                                    .AcceptChanges()

                                    For Each Rxp As DataRow In .Select("FTPurchaseNo='" & HI.UL.ULF.rpQuoted(R!FTPurchaseNo.ToString) & "'")
                                        Rxp!FTSelect = "0"

                                        Rxp!FTStateSendMail = R!FTStateSendMail.ToString
                                        Rxp!FTSendMailBy = R!FTSendMailBy.ToString
                                        Rxp!FTSendMailDate = R!FTSendMailDate.ToString
                                        Rxp!FTSendMailTime = R!FTSendMailTime.ToString

                                        Rxp!FTStateHold = "0"
                                        Rxp!FTHoldReason = ""

                                        If Val(Rxp!FNLeadTime.ToString) > 0 Then

                                            Rxp!FTOETCDate = HI.UL.ULDate.AddDay(R!FTSendMailDate.ToString, Val(Rxp!FNLeadTime.ToString))

                                        End If

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
                            MsgBox(ex.Message)
                        Finally

                        End Try

                    Catch ex As Exception
                        _Spls.Close()
                        HI.MG.ShowMsg.mInfo("เนื่องจากพบข้อผิดพลาดบางประการ ระบบจึงไม่สามารถทำการส่งเมลล์ได้ !!!", 1408280001, Me.Text, ex.Message, MessageBoxIcon.Warning)
                    Finally
                        OutlookMessage = Nothing
                        AppOutlook = Nothing
                    End Try


                Catch ex As Exception
                    _Spls.Close()
                    HI.MG.ShowMsg.mInfo("ไม่พบ Microsoft Outlook ไม่สามารถทำการส่งเมลล์ได้ !!!", 1408280002, Me.Text, ex.Message, MessageBoxIcon.Warning)
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

                Select Case ogvtime.FocusedColumn.FieldName.ToString
                    Case "FTORGOETCDate", "FTFinalOETCDate", "FTSendSMPDate"

                    Case Else
                        Exit Sub
                End Select

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
                If NewData <> GridDataBefore And NewData <> "" Then

                    Dim FieldName As String = .FocusedColumn.FieldName.ToString

                    Dim OrderNo As String = .GetRowCellValue(.FocusedRowHandle, "FTDocKey").ToString()
                    Dim MatId As Integer = Integer.Parse(Val("0" & .GetRowCellValue(.FocusedRowHandle, "FNHSysRawMatId").ToString()))
                    Dim LeadTime As Integer = Integer.Parse(Val("0" & .GetRowCellValue(.FocusedRowHandle, "FNLeadTime").ToString()))
                    Dim OETCDate As String = .GetRowCellValue(.FocusedRowHandle, "FTOETCDate").ToString()
                    Dim Doctype As Integer = Val(.GetRowCellValue(.FocusedRowHandle, "FNDocType").ToString())
                    Dim cmdstring As String = ""

                    cmdstring = "  Declare @CountData int = 0 "
                    cmdstring &= vbCrLf & " update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_T1T2  Set "


                    Select Case ogvtime.FocusedColumn.FieldName.ToString
                        Case "FTORGOETCDate"

                            cmdstring &= vbCrLf & " FTORGOETCDate='" & HI.UL.ULDate.ConvertEnDB(NewData) & "'"
                            cmdstring &= vbCrLf & ", FNORGLeadTime= DATEDIFF(DAY,'" & HI.UL.ULDate.ConvertEnDB(OETCDate) & "','" & HI.UL.ULDate.ConvertEnDB(NewData) & "')"
                            cmdstring &= vbCrLf & ",FTORGOETCInputBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                            cmdstring &= vbCrLf & ",FTORGOETCInputDate=" & HI.UL.ULDate.FormatDateDB & ""
                            cmdstring &= vbCrLf & ",FTORGOETCInputTime=" & HI.UL.ULDate.FormatTimeDB & ""

                        Case "FTFinalOETCDate"

                            cmdstring &= vbCrLf & " FTFinalOETCDate='" & HI.UL.ULDate.ConvertEnDB(NewData) & "'"
                            cmdstring &= vbCrLf & " ,FNFinalLeadTime=DATEDIFF(DAY,'" & HI.UL.ULDate.ConvertEnDB(OETCDate) & "','" & HI.UL.ULDate.ConvertEnDB(NewData) & "')"
                            cmdstring &= vbCrLf & ",FTFinalOETCInputBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                            cmdstring &= vbCrLf & ",FTFinalOETCInputDate=" & HI.UL.ULDate.FormatDateDB & ""
                            cmdstring &= vbCrLf & ",FTFinalOETCInputTime=" & HI.UL.ULDate.FormatTimeDB & ""
                        Case "FTSendSMPDate"

                            cmdstring &= vbCrLf & " FTSendSMPDate='" & HI.UL.ULDate.ConvertEnDB(NewData) & "'"
                            cmdstring &= vbCrLf & ",FTSendSMPDateInputBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                            cmdstring &= vbCrLf & ",FTSendSMPDateInputDate=" & HI.UL.ULDate.FormatDateDB & ""
                            cmdstring &= vbCrLf & ",FTSendSMPDateInputTime=" & HI.UL.ULDate.FormatTimeDB & ""

                    End Select

                    cmdstring &= vbCrLf & ",FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                    cmdstring &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB & ""
                    cmdstring &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & ""

                    cmdstring &= vbCrLf & ",FTFirstInputBy= CASE WHEN ISNULL(FTFirstInputBy,'') ='' THEN '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' ELSE FTFirstInputBy END "
                    cmdstring &= vbCrLf & ",FTFirstInputDate=CASE WHEN ISNULL(FTFirstInputBy,'') ='' THEN " & HI.UL.ULDate.FormatDateDB & " ELSE FTFirstInputDate END "
                    cmdstring &= vbCrLf & ",FTFirstInputTime=CASE WHEN ISNULL(FTFirstInputBy,'') ='' THEN " & HI.UL.ULDate.FormatTimeDB & " ELSE FTFirstInputTime END "

                    cmdstring &= vbCrLf & ",FTLastInputBy=CASE WHEN ISNULL(FTFirstInputBy,'') <>'' THEN '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'  ELSE FTLastInputBy END "
                    cmdstring &= vbCrLf & ",FTLastInputDate=CASE WHEN ISNULL(FTFirstInputBy,'') <>'' THEN " & HI.UL.ULDate.FormatDateDB & "  ELSE FTLastInputDate END "
                    cmdstring &= vbCrLf & ",FTLastInputTime=CASE WHEN ISNULL(FTFirstInputBy,'') <>'' THEN " & HI.UL.ULDate.FormatTimeDB & "  ELSE FTLastInputTime END "
                    cmdstring &= vbCrLf & " ,FNLeadTime=" & LeadTime & ""
                    cmdstring &= vbCrLf & " ,FTOETCDate='" & HI.UL.ULDate.ConvertEnDB(OETCDate) & "'"

                    cmdstring &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(OrderNo) & "'  "
                    cmdstring &= vbCrLf & " AND  FNHSysRawMatId=" & MatId & "  "
                    cmdstring &= vbCrLf & " SET @CountData = @@ROWCOUNT  "
                    cmdstring &= vbCrLf & " IF @CountData <=0 "
                    cmdstring &= vbCrLf & "   BEGIN "

                    cmdstring &= vbCrLf & "        INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_T1T2(FTInsUser, FDInsDate, FTInsTime, FTPurchaseNo, FNDocType ,FNHSysRawMatId, FTFirstInputBy, FTFirstInputDate,FTFirstInputTime,FNLeadTime,FTOETCDate "

                    Select Case ogvtime.FocusedColumn.FieldName.ToString
                        Case "FTORGOETCDate"

                            cmdstring &= vbCrLf & ", FTORGOETCDate"
                            cmdstring &= vbCrLf & ", FNORGLeadTime"
                            cmdstring &= vbCrLf & ",FTORGOETCInputBy "
                            cmdstring &= vbCrLf & ",FTORGOETCInputDate"
                            cmdstring &= vbCrLf & ",FTORGOETCInputTime"

                        Case "FTFinalOETCDate"

                            cmdstring &= vbCrLf & ", FTFinalOETCDate"
                            cmdstring &= vbCrLf & " ,FNFinalLeadTime"
                            cmdstring &= vbCrLf & ",FTFinalOETCInputBy"
                            cmdstring &= vbCrLf & ",FTFinalOETCInputDate"
                            cmdstring &= vbCrLf & ",FTFinalOETCInputTime"
                        Case "FTSendSMPDate"

                            cmdstring &= vbCrLf & ", FTSendSMPDate"
                            cmdstring &= vbCrLf & ",FTSendSMPDateInputBy"
                            cmdstring &= vbCrLf & ",FTSendSMPDateInputDate"
                            cmdstring &= vbCrLf & ",FTORGOETCInputTime"

                    End Select

                    cmdstring &= vbCrLf & ")  "
                    cmdstring &= vbCrLf & "  SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                    cmdstring &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                    cmdstring &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ""
                    cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(OrderNo) & "' "
                    cmdstring &= vbCrLf & "," & Doctype & " "
                    cmdstring &= vbCrLf & "," & MatId & " "
                    cmdstring &= vbCrLf & ", '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                    cmdstring &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                    cmdstring &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ""
                    cmdstring &= vbCrLf & " ," & LeadTime & ""
                    cmdstring &= vbCrLf & " ,'" & HI.UL.ULDate.ConvertEnDB(OETCDate) & "'"

                    Select Case ogvtime.FocusedColumn.FieldName.ToString
                        Case "FTORGOETCDate"
                            cmdstring &= vbCrLf & ",'" & HI.UL.ULDate.ConvertEnDB(NewData) & "'"
                            cmdstring &= vbCrLf & ",DATEDIFF(DAY,'" & HI.UL.ULDate.ConvertEnDB(OETCDate) & "','" & HI.UL.ULDate.ConvertEnDB(NewData) & "')"
                            cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                            cmdstring &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                            cmdstring &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ""

                        Case "FTFinalOETCDate"
                            cmdstring &= vbCrLf & " ,'" & HI.UL.ULDate.ConvertEnDB(NewData) & "'"
                            cmdstring &= vbCrLf & " ,DATEDIFF(DAY,'" & HI.UL.ULDate.ConvertEnDB(OETCDate) & "','" & HI.UL.ULDate.ConvertEnDB(NewData) & "')"
                            cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                            cmdstring &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                            cmdstring &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ""

                        Case "FTSendSMPDate"
                            cmdstring &= vbCrLf & " ,'" & HI.UL.ULDate.ConvertEnDB(NewData) & "'"
                            cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                            cmdstring &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                            cmdstring &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ""


                    End Select

                    cmdstring &= vbCrLf & " SET @CountData = @@ROWCOUNT  "

                    cmdstring &= vbCrLf & "   END "

                    cmdstring &= vbCrLf & " update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_T1T2  Set "
                    cmdstring &= vbCrLf & "  FTDelayLangth = CASE WHEN  ISDATE(FTFinalOETCDate)= 1   THEN    "

                    cmdstring &= vbCrLf & " CASE WHEN FNFinalLeadTime <=14 THEN '1-14 DAYS' "
                    cmdstring &= vbCrLf & "      WHEN FNFinalLeadTime >=29 THEN '>29 DAYS' "
                    cmdstring &= vbCrLf & "    Else  '15-28 DAYS' End "

                    cmdstring &= vbCrLf & "  ELSE "
                    cmdstring &= vbCrLf & " CASE WHEN   ISDATE(FTORGOETCDate) =1 THEN  "

                    cmdstring &= vbCrLf & " CASE WHEN FNORGLeadTime <=14 THEN '1-14 DAYS' "
                    cmdstring &= vbCrLf & "      WHEN FNORGLeadTime >=29 THEN '>29 DAYS' "
                    cmdstring &= vbCrLf & "    Else  '15-28 DAYS' End "

                    cmdstring &= vbCrLf & "  ELSE '' END "
                    cmdstring &= vbCrLf & " END "

                    cmdstring &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(OrderNo) & "'  "
                    cmdstring &= vbCrLf & " AND  FNHSysRawMatId=" & MatId & "  "

                    cmdstring &= vbCrLf & "  Select  Top 1 Case When ISDATE(TR.FTFirstInputDate) = 1 Then Convert(varchar(10),Convert(datetime,TR.FTFirstInputDate) ,103) Else '' END As FTFirstInputDate,TR.FTFirstInputBy  "
                    cmdstring &= vbCrLf & " ,Case When ISDATE(TR.FTLastInputDate) = 1 Then Convert(varchar(10),Convert(datetime,TR.FTLastInputDate) ,103) Else '' END As FTLastInputDate,TR.FTLastInputBy  "
                    cmdstring &= vbCrLf & " ,Case When ISDATE(TR.FTORGOETCDate) = 1 Then Convert(varchar(10),Convert(datetime,TR.FTORGOETCDate) ,103) Else '' END As FTORGOETCDate "
                    cmdstring &= vbCrLf & " ,Case When ISDATE(TR.FTFinalOETCDate) = 1 Then Convert(varchar(10),Convert(datetime,TR.FTFinalOETCDate) ,103) Else '' END As FTFinalOETCDate "

                    cmdstring &= vbCrLf & " ,FNORGLeadTime "
                    cmdstring &= vbCrLf & " ,FNFinalLeadTime"
                    cmdstring &= vbCrLf & " ,FTDelayLangth"

                    cmdstring &= vbCrLf & " ,'' AS FTSamplePPC"

                    cmdstring &= vbCrLf & "  From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_T1T2 AS TR  WITH(NOLOCK)  "
                    cmdstring &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(OrderNo) & "'  "
                    cmdstring &= vbCrLf & " AND  FNHSysRawMatId=" & MatId & "  "

                    Dim mdt As DataTable
                    mdt = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_PUR)


                    For Each Rxp As DataRow In mdt.Rows

                        With CType(Me.ogdtime.DataSource, DataTable)
                            .AcceptChanges()

                            For Each Rxd As DataRow In .Select("FTDocKey='" & HI.UL.ULF.rpQuoted(OrderNo) & "'  AND FNHSysRawMatId=" & MatId & " ")

                                Rxd!FTFirstInputDate = Rxp!FTFirstInputDate
                                Rxd!FTFirstInputBy = Rxp!FTFirstInputBy
                                Rxd!FTLastInputDate = Rxp!FTLastInputDate
                                Rxd!FTLastInputBy = Rxp!FTLastInputBy

                                Rxd!FTORGOETCDate = Rxp!FTORGOETCDate
                                Rxd!FTFinalOETCDate = Rxp!FTFinalOETCDate
                                Rxd!FNORGLeadTime = Rxp!FNORGLeadTime
                                Rxd!FNFinalLeadTime = Rxp!FNFinalLeadTime
                                Rxd!FTDelayLangth = Rxp!FTDelayLangth

                            Next

                            .AcceptChanges()
                        End With

                    Next

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

        If ogvtime.GetRowCellValue(ogvtime.FocusedRowHandle, "FTStateCancel").ToString() <> "1" Then
            Select Case ogvtime.FocusedColumn.FieldName.ToString

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
                    cmdstring &= vbCrLf & " FTStateSendMail='" & HI.UL.ULF.rpQuoted(State) & "'"
                    cmdstring &= vbCrLf & ",FTSendMailBy= CASE WHEN ISNULL(FTSendMailBy,'') ='' THEN  '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'  ELSE FTSendMailBy END"
                    cmdstring &= vbCrLf & ",FTSendMailDate=CASE WHEN ISNULL(FTSendMailBy,'') ='' THEN  " & HI.UL.ULDate.FormatDateDB & "  ELSE FTSendMailDate END "
                    cmdstring &= vbCrLf & ",FTSendMailTime=CASE WHEN ISNULL(FTSendMailBy,'') ='' THEN  " & HI.UL.ULDate.FormatTimeDB & "  ELSE FTSendMailTime END "
                    cmdstring &= vbCrLf & ",FTLastMailBy= CASE WHEN ISNULL(FTSendMailBy,'') <>'' THEN  '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' ELSE FTLastMailBy  END "
                    cmdstring &= vbCrLf & ",FTLastMailDate=CASE WHEN ISNULL(FTSendMailBy,'') <>'' THEN   " & HI.UL.ULDate.FormatDateDB & "  ELSE FTLastMailDate  END  "
                    cmdstring &= vbCrLf & ",FTLastMailTime=CASE WHEN ISNULL(FTSendMailBy,'') <>'' THEN   " & HI.UL.ULDate.FormatTimeDB & "  ELSE FTLastMailTime  END  "
                    cmdstring &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(PoNo) & "'  "
                    cmdstring &= vbCrLf & " select top 1 FTStateSendMail,FTSendMailBy,CASE WHEN ISDATE(FTSendMailDate) = 1 THEN Convert(varchar(10),Convert(datetime,FTSendMailDate),103) ELSE '' END AS FTSendMailDate,FTSendMailTime,FTPurchaseNo "
                    cmdstring &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase   "
                    cmdstring &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(PoNo) & "'  "

                    Dim dt As DataTable = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_PUR)

                    For Each R As DataRow In dt.Rows


                        With CType(Me.ogdtime.DataSource, DataTable)
                            .AcceptChanges()

                            For Each Rxd As DataRow In .Select("FTPurchaseNo='" & HI.UL.ULF.rpQuoted(PoNo) & "'")

                                Rxd!FTStateSendMail = R!FTStateSendMail
                                Rxd!FTSendMailBy = R!FTSendMailBy
                                Rxd!FTSendMailDate = R!FTSendMailDate
                                Rxd!FTSendMailTime = R!FTSendMailTime


                                If Val(Rxd!FNLeadTime.ToString) > 0 Then

                                    Rxd!FTOETCDate = HI.UL.ULDate.AddDay(R!FTSendMailDate.ToString, Val(Rxd!FNLeadTime.ToString))

                                End If

                            Next

                            .AcceptChanges()

                        End With

                        Exit For
                    Next

                    dt.Dispose()

                    If State = "1" Then
                        Try
                            cmdstring = " EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.USP_SENDDATAPO_FORVENDER '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(PoNo) & "'"
                            HI.Conn.SQLConn.ExecuteOnly(cmdstring, Conn.DB.DataBaseName.DB_PUR)
                        Catch ex As Exception

                        End Try

                    End If

            End Select

            e.Cancel = False

        Else
            e.Cancel = True
        End If


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


        Dim grplistpo As List(Of String) = (dtpoList.Select("FTDocKey<>''", "FTDocKey").CopyToDataTable).AsEnumerable() _
                                                      .Select(Function(r) r.Field(Of String)("FTDocKey")) _
                                                      .Distinct() _
                                                      .ToList()


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
                Dim RPoNo As String = ""
                Dim RMId As Integer = 0
                For Each R As DataRow In dtpoList.Rows
                    RPoNo = R!FTDocKey.ToString
                    RMId = Val(R!FNHSysRawMatId.ToString)


                    cmd = " INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_T1T2_Tracking ( FTInsUser, FDInsDate, FTInsTime, FTPurchaseNo,FNHSysRawMatId, FNTrackSeq, FTTrackBy, FTTrackDate, FTTrackTime, FTContactName, FTNote) "

                    cmd &= vbCrLf & "  SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                    cmd &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                    cmd &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ""
                    cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(RPoNo) & "' "
                    cmd &= vbCrLf & "," & RMId & ""
                    cmd &= vbCrLf & ", ISNULL((SELECt MAX(FNTrackSeq) AS FNTrackSeq FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_T1T2_Tracking WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(RPoNo) & "' AND FNHSysRawMatId =" & RMId & " ),0) +1  As FNTrackSeq  "
                    cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                    cmd &= vbCrLf & ",'" & HI.UL.ULDate.ConvertEnDB(pContactDate) & "'  "
                    cmd &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ""
                    cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(pConTactName) & "' "
                    cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(pNote) & "' "

                    cmd &= vbCrLf & "  Select  Top 1 TR.FNTrackSeq,Case When ISDATE(TR.FTTrackDate) = 1 Then Convert(varchar(10),Convert(datetime,TR.FTTrackDate) ,103) Else '' END As FTTrackDate,TR.FTTrackBy,TR.FTContactName,TR.FTNote "
                    cmd &= vbCrLf & "  From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_T1T2_Tracking AS TR  WITH(NOLOCK)  "
                    cmd &= vbCrLf & "   Where TR.FTPurchaseNo = '" & HI.UL.ULF.rpQuoted(RPoNo) & "' AND TR.FNHSysRawMatId =" & RMId & "  "
                    cmd &= vbCrLf & "   Order By TR.FNTrackSeq DESC "

                    Dim mdt As DataTable
                    mdt = HI.Conn.SQLConn.GetDataTable(cmd, Conn.DB.DataBaseName.DB_PUR)

                    Try
                        For Each Rxp As DataRow In mdt.Rows

                            With CType(Me.ogdtime.DataSource, DataTable)
                                .AcceptChanges()

                                For Each Rxd As DataRow In .Select("FTDocKey='" & HI.UL.ULF.rpQuoted(RPoNo) & "' AND FNHSysRawMatId=" & RMId & "")

                                    Rxd!FNTrackSeq = Rxp!FNTrackSeq
                                    Rxd!FTTrackDate = Rxp!FTTrackDate
                                    Rxd!FTTrackBy = Rxp!FTTrackBy
                                    Rxd!FTContactName = Rxp!FTContactName
                                    Rxd!FTTrackNote = Rxp!FTNote

                                Next

                                .AcceptChanges()
                            End With


                        Next
                    Catch ex As Exception

                    End Try

                Next

                _Spls.Close()
                '  Call LoadData()

            End If

        End With
        dtpoList.Dispose()
        dtpo.Dispose()
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

                    Dim FieldName As String = .FocusedColumn.FieldName.ToString

                    Dim OrderNo As String = .GetRowCellValue(.FocusedRowHandle, "FTDocKey").ToString()
                    Dim MatId As Integer = Integer.Parse(Val("0" & .GetRowCellValue(.FocusedRowHandle, "FNHSysRawMatId").ToString()))
                    Dim LeadTime As Integer = Integer.Parse(Val("0" & .GetRowCellValue(.FocusedRowHandle, "FNLeadTime").ToString()))
                    Dim OETCDate As String = .GetRowCellValue(.FocusedRowHandle, "FTOETCDate").ToString()
                    Dim Doctype As Integer = Val(.GetRowCellValue(.FocusedRowHandle, "FNDocType").ToString())

                    Dim cmdstring As String = ""

                    cmdstring = "  DECLARE @CountData int = 0 "
                    cmdstring &= vbCrLf & " update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_T1T2  Set "


                    Select Case ogvtime.FocusedColumn.FieldName.ToString
                        Case "FTPROCFMNote"
                            cmdstring &= vbCrLf & " FTPROCFMNote='" & HI.UL.ULF.rpQuoted(NewData) & "'"
                        Case "FTPOCFMNote"
                            cmdstring &= vbCrLf & " FTPOCFMNote='" & HI.UL.ULF.rpQuoted(NewData) & "'"
                        Case "FTInvoiceNote"
                            cmdstring &= vbCrLf & " FTInvoiceNote='" & HI.UL.ULF.rpQuoted(NewData) & "'"
                        Case "FTSendSMPRemark"
                            cmdstring &= vbCrLf & " FTSendSMPRemark='" & HI.UL.ULF.rpQuoted(NewData) & "'"
                    End Select

                    cmdstring &= vbCrLf & ",FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                    cmdstring &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB & ""
                    cmdstring &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & ""
                    cmdstring &= vbCrLf & ",FTFirstInputBy= CASE WHEN ISNULL(FTFirstInputBy,'') ='' THEN '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' ELSE FTFirstInputBy END "
                    cmdstring &= vbCrLf & ",FTFirstInputDate=CASE WHEN ISNULL(FTFirstInputBy,'') ='' THEN " & HI.UL.ULDate.FormatDateDB & " ELSE FTFirstInputDate END "
                    cmdstring &= vbCrLf & ",FTFirstInputTime=CASE WHEN ISNULL(FTFirstInputBy,'') ='' THEN " & HI.UL.ULDate.FormatTimeDB & " ELSE FTFirstInputTime END "
                    cmdstring &= vbCrLf & ",FTLastInputBy=CASE WHEN ISNULL(FTFirstInputBy,'') <>'' THEN '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'  ELSE FTLastInputBy END "
                    cmdstring &= vbCrLf & ",FTLastInputDate=CASE WHEN ISNULL(FTFirstInputBy,'') <>'' THEN " & HI.UL.ULDate.FormatDateDB & "  ELSE FTLastInputDate END "
                    cmdstring &= vbCrLf & ",FTLastInputTime=CASE WHEN ISNULL(FTFirstInputBy,'') <>'' THEN " & HI.UL.ULDate.FormatTimeDB & "  ELSE FTLastInputTime END "
                    cmdstring &= vbCrLf & " ,FNLeadTime=" & LeadTime & ""
                    cmdstring &= vbCrLf & " ,FTOETCDate='" & HI.UL.ULDate.ConvertEnDB(OETCDate) & "'"
                    cmdstring &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(OrderNo) & "'  "
                    cmdstring &= vbCrLf & " AND  FNHSysRawMatId=" & MatId & "  "
                    cmdstring &= vbCrLf & " SET @CountData = @@ROWCOUNT  "
                    cmdstring &= vbCrLf & " IF @CountData <=0 "
                    cmdstring &= vbCrLf & "   BEGIN "

                    cmdstring &= vbCrLf & "        INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_T1T2(FTInsUser, FDInsDate, FTInsTime, FTPurchaseNo,FNDocType, FNHSysRawMatId, FTFirstInputBy, FTFirstInputDate,FTFirstInputTime,FNLeadTime,FTOETCDate "

                    Select Case ogvtime.FocusedColumn.FieldName.ToString
                        Case "FTPROCFMNote"
                            cmdstring &= vbCrLf & " ,FTPROCFMNote"
                        Case "FTPOCFMNote"
                            cmdstring &= vbCrLf & " ,FTPOCFMNote"
                        Case "FTInvoiceNote"
                            cmdstring &= vbCrLf & ", FTInvoiceNote"
                        Case "FTSendSMPRemark"
                            cmdstring &= vbCrLf & ", FTSendSMPRemark"

                    End Select

                    cmdstring &= vbCrLf & ")  "
                    cmdstring &= vbCrLf & "  SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                    cmdstring &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                    cmdstring &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ""
                    cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(OrderNo) & "' "
                    cmdstring &= vbCrLf & "," & Doctype & " "
                    cmdstring &= vbCrLf & "," & MatId & " "
                    cmdstring &= vbCrLf & ", '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                    cmdstring &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                    cmdstring &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ""
                    cmdstring &= vbCrLf & " ," & LeadTime & ""
                    cmdstring &= vbCrLf & " ,'" & HI.UL.ULDate.ConvertEnDB(OETCDate) & "'"

                    cmdstring &= vbCrLf & ", '" & HI.UL.ULF.rpQuoted(NewData) & "' "

                    cmdstring &= vbCrLf & " SET @CountData = @@ROWCOUNT  "

                    cmdstring &= vbCrLf & "   END "

                    cmdstring &= vbCrLf & "  Select  Top 1 Case When ISDATE(TR.FTFirstInputDate) = 1 Then Convert(varchar(10),Convert(datetime,TR.FTFirstInputDate) ,103) Else '' END As FTFirstInputDate,TR.FTFirstInputBy  "
                    cmdstring &= vbCrLf & " ,Case When ISDATE(TR.FTLastInputDate) = 1 Then Convert(varchar(10),Convert(datetime,TR.FTLastInputDate) ,103) Else '' END As FTLastInputDate,TR.FTLastInputBy  "

                    cmdstring &= vbCrLf & " ,'' AS FTSamplePPC"
                    cmdstring &= vbCrLf & "  From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_T1T2 AS TR  WITH(NOLOCK)  "
                    cmdstring &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(OrderNo) & "'  "
                    cmdstring &= vbCrLf & " AND  FNHSysRawMatId=" & MatId & "  "

                    Dim mdt As DataTable
                    mdt = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_PUR)


                    For Each Rxp As DataRow In mdt.Rows

                        With CType(Me.ogdtime.DataSource, DataTable)
                            .AcceptChanges()

                            For Each Rxd As DataRow In .Select("FTDocKey='" & HI.UL.ULF.rpQuoted(OrderNo) & "'  AND FNHSysRawMatId=" & MatId & " ")

                                Rxd!FTFirstInputDate = Rxp!FTFirstInputDate
                                Rxd!FTFirstInputBy = Rxp!FTFirstInputBy
                                Rxd!FTLastInputDate = Rxp!FTLastInputDate
                                Rxd!FTLastInputBy = Rxp!FTLastInputBy


                            Next

                            .AcceptChanges()
                        End With


                    Next

                End If

            End With

        Catch ex As Exception
        End Try
    End Sub

    Private Sub ogvtime_ShowingEditor(sender As Object, e As CancelEventArgs) Handles ogvtime.ShowingEditor

        Try

            If FNDataType.SelectedIndex = 2 Then
                e.Cancel = True
            Else
                If ogvtime.GetFocusedRowCellValue("FTStateCancel").ToString = "1" Then
                    e.Cancel = True
                Else
                    GridDataNoteBefore = ""
                    If ogvtime.GetFocusedRowCellValue("FNDocType").ToString = "1" Then
                        Select Case ogvtime.FocusedColumn.FieldName
                            Case "FTSelect", "FTTrackNote"
                                e.Cancel = False
                            Case "FTInvoiceNo", "FTInvoiceNote", "FTSendSMPDate", "FTSendSMPStatus", "FTSendSMPRemark", "FTSendSMPAWB", "FTSendSMPPayType"
                                e.Cancel = False
                                GridDataNoteBefore = ogvtime.GetFocusedRowCellValue(ogvtime.FocusedColumn.FieldName.ToString).ToString
                            Case Else

                                e.Cancel = True
                        End Select
                    Else
                        Select Case ogvtime.FocusedColumn.FieldName
                            Case "FTSelect", "FTStateSendMail", "FTTrackNote"
                                e.Cancel = False
                            Case "FTInvoiceNo", "FTInvoiceNote", "FTSendSMPDate", "FTSendSMPStatus", "FTSendSMPRemark", "FTSendSMPAWB", "FTSendSMPPayType"
                                e.Cancel = False
                                GridDataNoteBefore = ogvtime.GetFocusedRowCellValue(ogvtime.FocusedColumn.FieldName.ToString).ToString
                            Case Else
                                If ogvtime.GetFocusedRowCellValue("FTStateSendMail").ToString = "1" Then

                                    Select Case ogvtime.FocusedColumn.FieldName.ToString
                                        Case "FTORGOETCDate", "FTFinalOETCDate", "FNPOCFMQuantity", "FNPROCFMQuantity", "FTDelayReasonsCode", "FTFurtherDelayReasonCode", "FTPOCFMNote", "FTPROCFMNote"

                                            If ogvtime.GetFocusedRowCellValue("FTOETCDate").ToString <> "" Then
                                                e.Cancel = False

                                                Select Case ogvtime.FocusedColumn.FieldName.ToString

                                                    Case "FNPOCFMQuantity", "FNPROCFMQuantity"
                                                        GridDataQtyBefore = Val(ogvtime.GetFocusedRowCellValue(ogvtime.FocusedColumn.FieldName.ToString).ToString)

                                                    Case "FTPOCFMNote", "FTPROCFMNote"

                                                        GridDataNoteBefore = ogvtime.GetFocusedRowCellValue(ogvtime.FocusedColumn.FieldName.ToString).ToString
                                                End Select

                                            Else
                                                e.Cancel = True
                                            End If


                                        Case "FNLeadTime"
                                            e.Cancel = False

                                            If ogvtime.GetFocusedRowCellValue(ogvtime.FocusedColumn.FieldName.ToString) Is Nothing Then
                                                GridDataQtyBefore = -1

                                            Else

                                                GridDataQtyBefore = Val(ogvtime.GetFocusedRowCellValue(ogvtime.FocusedColumn.FieldName.ToString).ToString)
                                            End If


                                        Case Else
                                            e.Cancel = False

                                    End Select

                                Else
                                    e.Cancel = True
                                End If


                        End Select
                    End If
                End If


            End If

        Catch ex As Exception
            e.Cancel = True
        End Try

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

                            If .GetRowCellValue(I, "FTStateCancel").ToString <> "1" Then
                                .SetRowCellValue(I, .Columns.ColumnByFieldName("FTSelect"), _State)
                            End If

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

        Select Case FNListDocumentTrackPIData.SelectedIndex.ToString
            Case "0"
                FTStartPurchaseDate.Properties.MinValue = DateAndTime.DateValue(Today.AddDays(-90))
                FTStartPurchaseDate.Properties.MaxValue = Today()
                FTEndPurchaseDate.Properties.MinValue = DateAndTime.DateValue(Today.AddDays(-90))
                FTEndPurchaseDate.Properties.MaxValue = Today()

                FTStartDelivery.Properties.MinValue = DateAndTime.DateValue(Today.AddDays(-90))
                FTStartDelivery.Properties.MaxValue = Today()
                FTEndDelivery.Properties.MinValue = DateAndTime.DateValue(Today.AddDays(-90))
                FTEndDelivery.Properties.MaxValue = Today()

                FTStartOrderDate.Properties.MinValue = DateAndTime.DateValue(Today.AddDays(-90))
                FTStartOrderDate.Properties.MaxValue = Today()
                FTEndOrderDate.Properties.MinValue = DateAndTime.DateValue(Today.AddDays(-90))
                FTEndOrderDate.Properties.MaxValue = Today()


            Case "1"
                FTStartPurchaseDate.Properties.MinValue = DateAndTime.DateValue(Today.AddDays(-180))
                FTStartPurchaseDate.Properties.MaxValue = Today()
                FTEndPurchaseDate.Properties.MinValue = DateAndTime.DateValue(Today.AddDays(-180))
                FTEndPurchaseDate.Properties.MaxValue = Today()

                FTStartDelivery.Properties.MinValue = DateAndTime.DateValue(Today.AddDays(-180))
                FTStartDelivery.Properties.MaxValue = Today()
                FTEndDelivery.Properties.MinValue = DateAndTime.DateValue(Today.AddDays(-180))
                FTEndDelivery.Properties.MaxValue = Today()

                FTStartOrderDate.Properties.MinValue = DateAndTime.DateValue(Today.AddDays(-180))
                FTStartOrderDate.Properties.MaxValue = Today()
                FTEndOrderDate.Properties.MinValue = DateAndTime.DateValue(Today.AddDays(-180))
                FTEndOrderDate.Properties.MaxValue = Today()

            Case "2"
                FTStartPurchaseDate.Properties.MinValue = DateAndTime.DateValue(Today.AddDays(-365))
                FTStartPurchaseDate.Properties.MaxValue = Today()
                FTEndPurchaseDate.Properties.MinValue = DateAndTime.DateValue(Today.AddDays(-365))
                FTEndPurchaseDate.Properties.MaxValue = Today()

                FTStartDelivery.Properties.MinValue = DateAndTime.DateValue(Today.AddDays(-365))
                FTStartDelivery.Properties.MaxValue = Today()
                FTEndDelivery.Properties.MinValue = DateAndTime.DateValue(Today.AddDays(-365))
                FTEndDelivery.Properties.MaxValue = Today()

                FTStartOrderDate.Properties.MinValue = DateAndTime.DateValue(Today.AddDays(-365))
                FTStartOrderDate.Properties.MaxValue = Today()
                FTEndOrderDate.Properties.MinValue = DateAndTime.DateValue(Today.AddDays(-365))
                FTEndOrderDate.Properties.MaxValue = Today()

            Case "3"
                FTStartPurchaseDate.Properties.MinValue = DateAndTime.DateValue(Today.AddDays(-730))
                FTStartPurchaseDate.Properties.MaxValue = Today()
                FTEndPurchaseDate.Properties.MinValue = DateAndTime.DateValue(Today.AddDays(-730))
                FTEndPurchaseDate.Properties.MaxValue = Today()

                FTStartDelivery.Properties.MinValue = DateAndTime.DateValue(Today.AddDays(-730))
                FTStartDelivery.Properties.MaxValue = Today()
                FTEndDelivery.Properties.MinValue = DateAndTime.DateValue(Today.AddDays(-730))
                FTEndDelivery.Properties.MaxValue = Today()

                FTStartOrderDate.Properties.MinValue = DateAndTime.DateValue(Today.AddDays(-730))
                FTStartOrderDate.Properties.MaxValue = Today()
                FTEndOrderDate.Properties.MinValue = DateAndTime.DateValue(Today.AddDays(-730))
                FTEndOrderDate.Properties.MaxValue = Today()

        End Select
    End Sub

    Private Sub RepositoryItemCalcQty_Leave(sender As Object, e As EventArgs) Handles RepositoryItemCalcQty.Leave
        Try
            With Me.ogvtime

                Select Case ogvtime.FocusedColumn.FieldName.ToString
                    Case "FNPOCFMQuantity", "FNPROCFMQuantity"

                    Case Else
                        Exit Sub
                End Select

                Dim NewQty As Double
                Dim DataString As String = .GetFocusedRowCellValue(ogvtime.FocusedColumn.FieldName.ToString).ToString
                NewQty = Val(DataString) ' CType(sender, DevExpress.XtraEditors.CalcEdit).Value

                '  .SetRowCellValue(.FocusedRowHandle, .FocusedColumn.FieldName.ToString, NewQty)



                If (NewQty <> GridDataQtyBefore) Or (DataString = "" And GridDataQtyBefore > 0) Then

                    Dim FieldName As String = .FocusedColumn.FieldName.ToString

                    Dim OrderNo As String = .GetRowCellValue(.FocusedRowHandle, "FTDocKey").ToString()
                    Dim MatId As Integer = Integer.Parse(Val("0" & .GetRowCellValue(.FocusedRowHandle, "FNHSysRawMatId").ToString()))
                    Dim LeadTime As Integer = Integer.Parse(Val("0" & .GetRowCellValue(.FocusedRowHandle, "FNLeadTime").ToString()))
                    Dim OETCDate As String = .GetRowCellValue(.FocusedRowHandle, "FTOETCDate").ToString()
                    Dim POQty As Double = Val(.GetRowCellValue(.FocusedRowHandle, "FNPOQuantity").ToString())
                    Dim Doctype As Integer = Val(.GetRowCellValue(.FocusedRowHandle, "FNDocType").ToString())
                    Dim cmdstring As String = ""


                    cmdstring = "  Declare @CountData int = 0 "
                    cmdstring &= vbCrLf & " update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_T1T2  Set "


                    Select Case ogvtime.FocusedColumn.FieldName.ToString
                        Case "FNPOCFMQuantity"

                            If DataString = "" Then
                                cmdstring &= vbCrLf & " FNPOCFMQuantity=NULL"
                            Else
                                cmdstring &= vbCrLf & " FNPOCFMQuantity=" & NewQty & ""
                            End If

                            cmdstring &= vbCrLf & ",FTPOCFMQuantityInputBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                            cmdstring &= vbCrLf & ",FTPOCFMQuantityInputDate=" & HI.UL.ULDate.FormatDateDB & ""
                            cmdstring &= vbCrLf & ",FTPOCFMQuantityInputTime=" & HI.UL.ULDate.FormatTimeDB & ""

                        Case "FNPROCFMQuantity"

                            If DataString = "" Then
                                cmdstring &= vbCrLf & " FNPROCFMQuantity=NULL"
                            Else
                                cmdstring &= vbCrLf & " FNPROCFMQuantity=" & NewQty & ""
                            End If

                            cmdstring &= vbCrLf & ",FTPROCFMQuantityInputBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                            cmdstring &= vbCrLf & ",FTPROCFMQuantityInputDate=" & HI.UL.ULDate.FormatDateDB & ""
                            cmdstring &= vbCrLf & ",FTPROCFMQuantityInputTime=" & HI.UL.ULDate.FormatTimeDB & ""

                    End Select

                    cmdstring &= vbCrLf & ",FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                    cmdstring &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB & ""
                    cmdstring &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & ""

                    cmdstring &= vbCrLf & ",FTFirstInputBy= CASE WHEN ISNULL(FTFirstInputBy,'') ='' THEN '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' ELSE FTFirstInputBy END "
                    cmdstring &= vbCrLf & ",FTFirstInputDate=CASE WHEN ISNULL(FTFirstInputBy,'') ='' THEN " & HI.UL.ULDate.FormatDateDB & " ELSE FTFirstInputDate END "
                    cmdstring &= vbCrLf & ",FTFirstInputTime=CASE WHEN ISNULL(FTFirstInputBy,'') ='' THEN " & HI.UL.ULDate.FormatTimeDB & " ELSE FTFirstInputTime END "

                    cmdstring &= vbCrLf & ",FTLastInputBy=CASE WHEN ISNULL(FTFirstInputBy,'') <>'' THEN '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'  ELSE FTLastInputBy END "
                    cmdstring &= vbCrLf & ",FTLastInputDate=CASE WHEN ISNULL(FTFirstInputBy,'') <>'' THEN " & HI.UL.ULDate.FormatDateDB & "  ELSE FTLastInputDate END "
                    cmdstring &= vbCrLf & ",FTLastInputTime=CASE WHEN ISNULL(FTFirstInputBy,'') <>'' THEN " & HI.UL.ULDate.FormatTimeDB & "  ELSE FTLastInputTime END "
                    cmdstring &= vbCrLf & " ,FNLeadTime=" & LeadTime & ""
                    cmdstring &= vbCrLf & " ,FTOETCDate='" & HI.UL.ULDate.ConvertEnDB(OETCDate) & "'"
                    cmdstring &= vbCrLf & " ,FNPOQuantity=" & POQty & ""

                    cmdstring &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(OrderNo) & "'  "
                    cmdstring &= vbCrLf & " AND  FNHSysRawMatId=" & MatId & "  "

                    cmdstring &= vbCrLf & " SET @CountData = @@ROWCOUNT  "
                    cmdstring &= vbCrLf & " IF @CountData <=0 "
                    cmdstring &= vbCrLf & "   BEGIN "

                    cmdstring &= vbCrLf & "        INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_T1T2(FTInsUser, FDInsDate, FTInsTime, FTPurchaseNo,FNDocType, FNHSysRawMatId, FTFirstInputBy, FTFirstInputDate,FTFirstInputTime,FNLeadTime,FTOETCDate,FNPOQuantity "

                    Select Case ogvtime.FocusedColumn.FieldName.ToString
                        Case "FNPOCFMQuantity"

                            cmdstring &= vbCrLf & ", FNPOCFMQuantity"
                            cmdstring &= vbCrLf & ",FTPOCFMQuantityInputBy "
                            cmdstring &= vbCrLf & ",FTPOCFMQuantityInputDate"
                            cmdstring &= vbCrLf & ",FTPOCFMQuantityInputTime"

                        Case "FNPROCFMQuantity"

                            cmdstring &= vbCrLf & ", FNPROCFMQuantity"
                            cmdstring &= vbCrLf & ",FTPROCFMQuantityInputBy"
                            cmdstring &= vbCrLf & ",FTPROCFMQuantityInputDate"
                            cmdstring &= vbCrLf & ",FTPROCFMQuantityInputTime"

                    End Select

                    cmdstring &= vbCrLf & ")  "
                    cmdstring &= vbCrLf & "  SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                    cmdstring &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                    cmdstring &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ""
                    cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(OrderNo) & "' "
                    cmdstring &= vbCrLf & "," & Doctype & " "
                    cmdstring &= vbCrLf & "," & MatId & " "
                    cmdstring &= vbCrLf & ", '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                    cmdstring &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                    cmdstring &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ""
                    cmdstring &= vbCrLf & " ," & LeadTime & ""
                    cmdstring &= vbCrLf & " ,'" & HI.UL.ULDate.ConvertEnDB(OETCDate) & "'"
                    cmdstring &= vbCrLf & " ," & POQty & ""

                    Select Case ogvtime.FocusedColumn.FieldName.ToString
                        Case "FNPOCFMQuantity"

                            If DataString = "" Then
                                cmdstring &= vbCrLf & ",NULL"
                            Else
                                cmdstring &= vbCrLf & "," & NewQty & ""
                            End If


                            cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                            cmdstring &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                            cmdstring &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ""

                        Case "FNPROCFMQuantity"

                            If DataString = "" Then
                                cmdstring &= vbCrLf & ",NULL"
                            Else
                                cmdstring &= vbCrLf & "," & NewQty & ""
                            End If

                            cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                            cmdstring &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                            cmdstring &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ""

                    End Select

                    cmdstring &= vbCrLf & " SET @CountData = @@ROWCOUNT  "
                    cmdstring &= vbCrLf & "   END "
                    cmdstring &= vbCrLf & " update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_T1T2  Set "
                    cmdstring &= vbCrLf & "  FNPOBALQuantity = CASE WHEN  FNPROCFMQuantity IS NULL    THEN  FNPOQuantity- ISNULL(FNPOCFMQuantity,0)   ELSE  FNPOQuantity- ISNULL(FNPROCFMQuantity,0)   END   "
                    cmdstring &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(OrderNo) & "'  "
                    cmdstring &= vbCrLf & " AND  FNHSysRawMatId=" & MatId & "  "
                    cmdstring &= vbCrLf & "  Select  Top 1 Case When ISDATE(TR.FTFirstInputDate) = 1 Then Convert(varchar(10),Convert(datetime,TR.FTFirstInputDate) ,103) Else '' END As FTFirstInputDate,TR.FTFirstInputBy  "
                    cmdstring &= vbCrLf & " ,Case When ISDATE(TR.FTLastInputDate) = 1 Then Convert(varchar(10),Convert(datetime,TR.FTLastInputDate) ,103) Else '' END As FTLastInputDate,TR.FTLastInputBy  "
                    cmdstring &= vbCrLf & " ,Case When ISDATE(TR.FTORGOETCDate) = 1 Then Convert(varchar(10),Convert(datetime,TR.FTORGOETCDate) ,103) Else '' END As FTORGOETCDate "
                    cmdstring &= vbCrLf & " ,Case When ISDATE(TR.FTFinalOETCDate) = 1 Then Convert(varchar(10),Convert(datetime,TR.FTFinalOETCDate) ,103) Else '' END As FTFinalOETCDate "
                    cmdstring &= vbCrLf & " ,FNPOBALQuantity "

                    cmdstring &= vbCrLf & " ,'' AS 



"
                    cmdstring &= vbCrLf & "  From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_T1T2 AS TR  WITH(NOLOCK)  "
                    cmdstring &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(OrderNo) & "'  "
                    cmdstring &= vbCrLf & " AND  FNHSysRawMatId=" & MatId & "  "

                    Dim mdt As DataTable
                    mdt = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_PUR)


                    For Each Rxp As DataRow In mdt.Rows

                        With CType(Me.ogdtime.DataSource, DataTable)
                            .AcceptChanges()

                            For Each Rxd As DataRow In .Select("FTDocKey='" & HI.UL.ULF.rpQuoted(OrderNo) & "'  AND FNHSysRawMatId=" & MatId & " ")

                                Rxd!FTFirstInputDate = Rxp!FTFirstInputDate
                                Rxd!FTFirstInputBy = Rxp!FTFirstInputBy
                                Rxd!FTLastInputDate = Rxp!FTLastInputDate
                                Rxd!FTLastInputBy = Rxp!FTLastInputBy

                                Rxd!FNPOBALQuantity = Rxp!FNPOBALQuantity



                            Next

                            .AcceptChanges()
                        End With

                    Next

                End If

            End With

        Catch ex As Exception
        End Try
    End Sub

    Private Sub RepositoryItemGridLookUpEditFTDelayReasonsCode_EditValueChanged(sender As Object, e As EventArgs) Handles RepositoryItemGridLookUpEditFTDelayReasonsCode.EditValueChanged
        Try
            With Me.ogvtime


                If .FocusedRowHandle < 0 Then Exit Sub

                Dim obj As DevExpress.XtraEditors.GridLookUpEdit = DirectCast(sender, DevExpress.XtraEditors.GridLookUpEdit)

                Dim DelayReasonsCode As String = obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTDelayReasonsCode").ToString
                Dim DelayReasonsName As String = obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTDelayReasonsName").ToString


                .SetRowCellValue(.FocusedRowHandle, .FocusedColumn.FieldName.ToString, DelayReasonsCode)
                .SetRowCellValue(.FocusedRowHandle, "FTDelayReasonsName", DelayReasonsName)


                Dim FieldName As String = .FocusedColumn.FieldName.ToString

                Dim OrderNo As String = .GetRowCellValue(.FocusedRowHandle, "FTDocKey").ToString()
                Dim MatId As Integer = Integer.Parse(Val("0" & .GetRowCellValue(.FocusedRowHandle, "FNHSysRawMatId").ToString()))
                Dim LeadTime As Integer = Integer.Parse(Val("0" & .GetRowCellValue(.FocusedRowHandle, "FNLeadTime").ToString()))
                Dim OETCDate As String = .GetRowCellValue(.FocusedRowHandle, "FTOETCDate").ToString()
                Dim Doctype As Integer = Val(.GetRowCellValue(.FocusedRowHandle, "FNDocType").ToString())

                Dim cmdstring As String = ""

                cmdstring = "  DECLARE @CountData int = 0 "
                cmdstring &= vbCrLf & " update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_T1T2  Set "




                cmdstring &= vbCrLf & " FTDelayReasonsCode='" & HI.UL.ULF.rpQuoted(DelayReasonsCode) & "'"


                cmdstring &= vbCrLf & ",FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                cmdstring &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB & ""
                cmdstring &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & ""

                cmdstring &= vbCrLf & ",FTFirstInputBy= CASE WHEN ISNULL(FTFirstInputBy,'') ='' THEN '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' ELSE FTFirstInputBy END "
                cmdstring &= vbCrLf & ",FTFirstInputDate=CASE WHEN ISNULL(FTFirstInputBy,'') ='' THEN " & HI.UL.ULDate.FormatDateDB & " ELSE FTFirstInputDate END "
                cmdstring &= vbCrLf & ",FTFirstInputTime=CASE WHEN ISNULL(FTFirstInputBy,'') ='' THEN " & HI.UL.ULDate.FormatTimeDB & " ELSE FTFirstInputTime END "

                cmdstring &= vbCrLf & ",FTLastInputBy=CASE WHEN ISNULL(FTFirstInputBy,'') <>'' THEN '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'  ELSE FTLastInputBy END "
                cmdstring &= vbCrLf & ",FTLastInputDate=CASE WHEN ISNULL(FTFirstInputBy,'') <>'' THEN " & HI.UL.ULDate.FormatDateDB & "  ELSE FTLastInputDate END "
                cmdstring &= vbCrLf & ",FTLastInputTime=CASE WHEN ISNULL(FTFirstInputBy,'') <>'' THEN " & HI.UL.ULDate.FormatTimeDB & "  ELSE FTLastInputTime END "


                cmdstring &= vbCrLf & " ,FNLeadTime=" & LeadTime & ""
                cmdstring &= vbCrLf & " ,FTOETCDate='" & HI.UL.ULDate.ConvertEnDB(OETCDate) & "'"

                cmdstring &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(OrderNo) & "'  "
                cmdstring &= vbCrLf & " AND  FNHSysRawMatId=" & MatId & "  "
                cmdstring &= vbCrLf & " SET @CountData = @@ROWCOUNT  "
                cmdstring &= vbCrLf & " IF @CountData <=0 "
                cmdstring &= vbCrLf & "   BEGIN "

                cmdstring &= vbCrLf & "        INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_T1T2(FTInsUser, FDInsDate, FTInsTime, FTPurchaseNo,FNDocType, FNHSysRawMatId, FTFirstInputBy, FTFirstInputDate,FTFirstInputTime,FNLeadTime,FTOETCDate "



                cmdstring &= vbCrLf & " ,FTDelayReasonsCode"



                cmdstring &= vbCrLf & ")  "
                cmdstring &= vbCrLf & "  SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                cmdstring &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                cmdstring &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ""
                cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(OrderNo) & "' "
                cmdstring &= vbCrLf & "," & Doctype & " "
                cmdstring &= vbCrLf & "," & MatId & " "
                cmdstring &= vbCrLf & ", '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                cmdstring &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                cmdstring &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ""
                cmdstring &= vbCrLf & " ," & LeadTime & ""
                cmdstring &= vbCrLf & " ,'" & HI.UL.ULDate.ConvertEnDB(OETCDate) & "'"

                cmdstring &= vbCrLf & ", '" & HI.UL.ULF.rpQuoted(DelayReasonsCode) & "' "

                cmdstring &= vbCrLf & " SET @CountData = @@ROWCOUNT  "

                cmdstring &= vbCrLf & "   END "

                cmdstring &= vbCrLf & "  Select  Top 1 Case When ISDATE(TR.FTFirstInputDate) = 1 Then Convert(varchar(10),Convert(datetime,TR.FTFirstInputDate) ,103) Else '' END As FTFirstInputDate,TR.FTFirstInputBy  "
                cmdstring &= vbCrLf & " ,Case When ISDATE(TR.FTLastInputDate) = 1 Then Convert(varchar(10),Convert(datetime,TR.FTLastInputDate) ,103) Else '' END As FTLastInputDate,TR.FTLastInputBy  "

                cmdstring &= vbCrLf & " ,'' AS FTSamplePPC"
                cmdstring &= vbCrLf & "  From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_T1T2 AS TR  WITH(NOLOCK)  "
                cmdstring &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(OrderNo) & "'  "
                cmdstring &= vbCrLf & " AND  FNHSysRawMatId=" & MatId & "  "

                Dim mdt As DataTable
                mdt = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_PUR)


                For Each Rxp As DataRow In mdt.Rows

                    With CType(Me.ogdtime.DataSource, DataTable)
                        .AcceptChanges()

                        For Each Rxd As DataRow In .Select("FTDocKey='" & HI.UL.ULF.rpQuoted(OrderNo) & "'  AND FNHSysRawMatId=" & MatId & " ")

                            Rxd!FTFirstInputDate = Rxp!FTFirstInputDate
                            Rxd!FTFirstInputBy = Rxp!FTFirstInputBy
                            Rxd!FTLastInputDate = Rxp!FTLastInputDate
                            Rxd!FTLastInputBy = Rxp!FTLastInputBy


                        Next

                        .AcceptChanges()
                    End With


                Next


            End With

        Catch ex As Exception
        End Try
    End Sub

    Private Sub RepositoryItemGridLookUpEditFTFurtherDelayReasonCode_EditValueChanged(sender As Object, e As EventArgs) Handles RepositoryItemGridLookUpEditFTFurtherDelayReasonCode.EditValueChanged
        Try
            With Me.ogvtime


                If .FocusedRowHandle < 0 Then Exit Sub

                Dim obj As DevExpress.XtraEditors.GridLookUpEdit = DirectCast(sender, DevExpress.XtraEditors.GridLookUpEdit)

                Dim DelayReasonsCode As String = obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTFurtherDelayReasonCode").ToString
                Dim DelayReasonsName As String = obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTFurtherDelayReasonName").ToString


                .SetRowCellValue(.FocusedRowHandle, .FocusedColumn.FieldName.ToString, DelayReasonsCode)
                .SetRowCellValue(.FocusedRowHandle, "FTFurtherDelayReasonName", DelayReasonsName)


                Dim FieldName As String = .FocusedColumn.FieldName.ToString

                Dim OrderNo As String = .GetRowCellValue(.FocusedRowHandle, "FTDocKey").ToString()
                Dim MatId As Integer = Integer.Parse(Val("0" & .GetRowCellValue(.FocusedRowHandle, "FNHSysRawMatId").ToString()))
                Dim LeadTime As Integer = Integer.Parse(Val("0" & .GetRowCellValue(.FocusedRowHandle, "FNLeadTime").ToString()))
                Dim OETCDate As String = .GetRowCellValue(.FocusedRowHandle, "FTOETCDate").ToString()
                Dim Doctype As Integer = Val(.GetRowCellValue(.FocusedRowHandle, "FNDocType").ToString())

                Dim cmdstring As String = ""

                cmdstring = "  DECLARE @CountData int = 0 "
                cmdstring &= vbCrLf & " update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_T1T2  Set "




                cmdstring &= vbCrLf & " FTFurtherDelayReasonCode='" & HI.UL.ULF.rpQuoted(DelayReasonsCode) & "'"


                cmdstring &= vbCrLf & ",FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                cmdstring &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB & ""
                cmdstring &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & ""

                cmdstring &= vbCrLf & ",FTFirstInputBy= CASE WHEN ISNULL(FTFirstInputBy,'') ='' THEN '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' ELSE FTFirstInputBy END "
                cmdstring &= vbCrLf & ",FTFirstInputDate=CASE WHEN ISNULL(FTFirstInputBy,'') ='' THEN " & HI.UL.ULDate.FormatDateDB & " ELSE FTFirstInputDate END "
                cmdstring &= vbCrLf & ",FTFirstInputTime=CASE WHEN ISNULL(FTFirstInputBy,'') ='' THEN " & HI.UL.ULDate.FormatTimeDB & " ELSE FTFirstInputTime END "

                cmdstring &= vbCrLf & ",FTLastInputBy=CASE WHEN ISNULL(FTFirstInputBy,'') <>'' THEN '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'  ELSE FTLastInputBy END "
                cmdstring &= vbCrLf & ",FTLastInputDate=CASE WHEN ISNULL(FTFirstInputBy,'') <>'' THEN " & HI.UL.ULDate.FormatDateDB & "  ELSE FTLastInputDate END "
                cmdstring &= vbCrLf & ",FTLastInputTime=CASE WHEN ISNULL(FTFirstInputBy,'') <>'' THEN " & HI.UL.ULDate.FormatTimeDB & "  ELSE FTLastInputTime END "
                cmdstring &= vbCrLf & " ,FNLeadTime=" & LeadTime & ""
                cmdstring &= vbCrLf & " ,FTOETCDate='" & HI.UL.ULDate.ConvertEnDB(OETCDate) & "'"

                cmdstring &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(OrderNo) & "'  "
                cmdstring &= vbCrLf & " AND  FNHSysRawMatId=" & MatId & "  "
                cmdstring &= vbCrLf & " SET @CountData = @@ROWCOUNT  "
                cmdstring &= vbCrLf & " IF @CountData <=0 "
                cmdstring &= vbCrLf & "   BEGIN "

                cmdstring &= vbCrLf & "        INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_T1T2(FTInsUser, FDInsDate, FTInsTime, FTPurchaseNo, FNDocType,FNHSysRawMatId, FTFirstInputBy, FTFirstInputDate,FTFirstInputTime,FNLeadTime,FTOETCDate "



                cmdstring &= vbCrLf & " ,FTFurtherDelayReasonCode"



                cmdstring &= vbCrLf & ")  "
                cmdstring &= vbCrLf & "  SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                cmdstring &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                cmdstring &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ""
                cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(OrderNo) & "' "
                cmdstring &= vbCrLf & "," & Doctype & " "
                cmdstring &= vbCrLf & "," & MatId & " "
                cmdstring &= vbCrLf & ", '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                cmdstring &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                cmdstring &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ""
                cmdstring &= vbCrLf & " ," & LeadTime & ""
                cmdstring &= vbCrLf & " ,'" & HI.UL.ULDate.ConvertEnDB(OETCDate) & "'"

                cmdstring &= vbCrLf & ", '" & HI.UL.ULF.rpQuoted(DelayReasonsCode) & "' "

                cmdstring &= vbCrLf & " SET @CountData = @@ROWCOUNT  "

                cmdstring &= vbCrLf & "   END "

                cmdstring &= vbCrLf & "  Select  Top 1 Case When ISDATE(TR.FTFirstInputDate) = 1 Then Convert(varchar(10),Convert(datetime,TR.FTFirstInputDate) ,103) Else '' END As FTFirstInputDate,TR.FTFirstInputBy  "
                cmdstring &= vbCrLf & " ,Case When ISDATE(TR.FTLastInputDate) = 1 Then Convert(varchar(10),Convert(datetime,TR.FTLastInputDate) ,103) Else '' END As FTLastInputDate,TR.FTLastInputBy  "

                cmdstring &= vbCrLf & " ,'' AS FTSamplePPC"
                cmdstring &= vbCrLf & "  From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_T1T2 AS TR  WITH(NOLOCK)  "
                cmdstring &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(OrderNo) & "'  "
                cmdstring &= vbCrLf & " AND  FNHSysRawMatId=" & MatId & "  "

                Dim mdt As DataTable
                mdt = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_PUR)


                For Each Rxp As DataRow In mdt.Rows

                    With CType(Me.ogdtime.DataSource, DataTable)
                        .AcceptChanges()

                        For Each Rxd As DataRow In .Select("FTDocKey='" & HI.UL.ULF.rpQuoted(OrderNo) & "'  AND FNHSysRawMatId=" & MatId & " ")

                            Rxd!FTFirstInputDate = Rxp!FTFirstInputDate
                            Rxd!FTFirstInputBy = Rxp!FTFirstInputBy
                            Rxd!FTLastInputDate = Rxp!FTLastInputDate
                            Rxd!FTLastInputBy = Rxp!FTLastInputBy


                        Next

                        .AcceptChanges()
                    End With


                Next


            End With

        Catch ex As Exception
        End Try
    End Sub


    Private Sub LoadMaster()
        Dim cmd As String = ""
        Dim dt As DataTable



        cmd = "SELECT   FTDelayReasonsCode, FTDelayReasonsNameEN AS FTDelayReasonsName  from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDelayReason WITH(NOLOCK) WHERE FTStateActive='1'  Order by FTDelayReasonsCode "

        dt = HI.Conn.SQLConn.GetDataTable(cmd, Conn.DB.DataBaseName.DB_MASTER)

        RepositoryItemGridLookUpEditFTDelayReasonsCode.DataSource = dt.Copy



        cmd = "SELECT   FTDelayReasonsCode AS FTFurtherDelayReasonCode, FTDelayReasonsNameEN AS FTFurtherDelayReasonNamee from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDelayReason WITH(NOLOCK) WHERE FTStateActive='1'  Order  by FTDelayReasonsCode "

        dt = HI.Conn.SQLConn.GetDataTable(cmd, Conn.DB.DataBaseName.DB_MASTER)

        RepositoryItemGridLookUpEditFTFurtherDelayReasonCode.DataSource = dt.Copy




        dt.Dispose()

    End Sub

    Private Sub ocmFTFacCheckCFMDeliveryDate2_Click(sender As Object, e As EventArgs) Handles ocmFTORGOETCDate.Click

    End Sub

    Private Sub RepositoryItemCalcEditFNLeadTime_Leave(sender As Object, e As EventArgs) Handles RepositoryItemCalcEditFNLeadTime.Leave
        Try
            With Me.ogvtime

                Select Case ogvtime.FocusedColumn.FieldName.ToString
                    Case "FNLeadTime"

                    Case Else
                        Exit Sub
                End Select

                Dim NewQty As Double
                NewQty = Val(.GetFocusedRowCellValue("FNLeadTime").ToString)   ' CType(sender, DevExpress.XtraEditors.CalcEdit).Value

                '  .SetRowCellValue(.FocusedRowHandle, .FocusedColumn.FieldName.ToString, NewQty)



                If NewQty <> GridDataQtyBefore Then

                    Dim FieldName As String = .FocusedColumn.FieldName.ToString

                    Dim OrderNo As String = .GetRowCellValue(.FocusedRowHandle, "FTDocKey").ToString()
                    Dim MatId As Integer = Integer.Parse(Val("0" & .GetRowCellValue(.FocusedRowHandle, "FNHSysRawMatId").ToString()))
                    Dim SendMailDate As String = .GetRowCellValue(.FocusedRowHandle, "FTSendMailDate").ToString()
                    Dim LeadTime As Integer = Integer.Parse(Val("0" & .GetRowCellValue(.FocusedRowHandle, "FNLeadTime").ToString()))
                    Dim OETCDate As String = .GetRowCellValue(.FocusedRowHandle, "FTOETCDate").ToString()
                    Dim POQty As Double = Val(.GetRowCellValue(.FocusedRowHandle, "FNPOQuantity").ToString())
                    Dim Doctype As Integer = Val(.GetRowCellValue(.FocusedRowHandle, "FNDocType").ToString())
                    Dim cmdstring As String = ""


                    cmdstring = "  Declare @CountData int = 0 "
                    cmdstring &= vbCrLf & " update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_T1T2  Set "


                    cmdstring &= vbCrLf & " FNLeadTime=" & NewQty & ""
                    cmdstring &= vbCrLf & ",FTOETCDate=Convert(varchar(10),DATEADD(DAY," & NewQty & ",'" & HI.UL.ULDate.ConvertEnDB(SendMailDate) & "'),111)"

                    cmdstring &= vbCrLf & ", FNORGLeadTime=  CASE WHEN ISDATE(FTORGOETCDate) = 1 THEN   DATEDIFF(DAY,Convert(varchar(10),DATEADD(DAY," & NewQty & ",'" & HI.UL.ULDate.ConvertEnDB(SendMailDate) & "'),111),FTORGOETCDate)   ELSE NULL END"

                    cmdstring &= vbCrLf & " ,FNFinalLeadTime=CASE WHEN ISDATE(FTFinalOETCDate) = 1 THEN   DATEDIFF(DAY,Convert(varchar(10),DATEADD(DAY," & NewQty & ",'" & HI.UL.ULDate.ConvertEnDB(SendMailDate) & "'),111),FTFinalOETCDate)   ELSE NULL END"

                    cmdstring &= vbCrLf & ",FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                    cmdstring &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB & ""
                    cmdstring &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & ""

                    cmdstring &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(OrderNo) & "'  "
                    cmdstring &= vbCrLf & " AND  FNHSysRawMatId=" & MatId & "  "
                    cmdstring &= vbCrLf & " SET @CountData = @@ROWCOUNT  "
                    cmdstring &= vbCrLf & " IF @CountData <=0 "
                    cmdstring &= vbCrLf & "   BEGIN "

                    cmdstring &= vbCrLf & "        INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_T1T2(FTInsUser, FDInsDate, FTInsTime, FTPurchaseNo,FNDocType, FNHSysRawMatId, FNLeadTime,FTOETCDate "



                    cmdstring &= vbCrLf & ")  "
                    cmdstring &= vbCrLf & "  SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                    cmdstring &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                    cmdstring &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ""
                    cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(OrderNo) & "' "
                    cmdstring &= vbCrLf & "," & Doctype & " "
                    cmdstring &= vbCrLf & "," & MatId & " "

                    cmdstring &= vbCrLf & "," & NewQty & ""
                    cmdstring &= vbCrLf & ",Convert(varchar(10),DATEADD(DAY," & NewQty & ",'" & HI.UL.ULDate.ConvertEnDB(SendMailDate) & "'),111)"


                    cmdstring &= vbCrLf & " SET @CountData = @@ROWCOUNT  "

                    cmdstring &= vbCrLf & "   END "


                    cmdstring &= vbCrLf & " update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_T1T2  Set "
                    cmdstring &= vbCrLf & "  FTDelayLangth = CASE WHEN  ISDATE(FTFinalOETCDate)= 1   THEN    "

                    cmdstring &= vbCrLf & " CASE WHEN FNFinalLeadTime <=14 THEN '1-14 DAYS' "
                    cmdstring &= vbCrLf & "      WHEN FNFinalLeadTime >=29 THEN '>29 DAYS' "
                    cmdstring &= vbCrLf & "    Else  '15-28 DAYS' End "

                    cmdstring &= vbCrLf & "  ELSE "
                    cmdstring &= vbCrLf & " CASE WHEN   ISDATE(FTORGOETCDate) =1 THEN  "

                    cmdstring &= vbCrLf & " CASE WHEN FNORGLeadTime <=14 THEN '1-14 DAYS' "
                    cmdstring &= vbCrLf & "      WHEN FNORGLeadTime >=29 THEN '>29 DAYS' "
                    cmdstring &= vbCrLf & "    Else  '15-28 DAYS' End "

                    cmdstring &= vbCrLf & "  ELSE '' END "
                    cmdstring &= vbCrLf & " END "

                    cmdstring &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(OrderNo) & "'  "
                    cmdstring &= vbCrLf & " AND  FNHSysRawMatId=" & MatId & "  "



                    cmdstring &= vbCrLf & "  Select  Top 1 Case When ISDATE(TR.FTFirstInputDate) = 1 Then Convert(varchar(10),Convert(datetime,TR.FTFirstInputDate) ,103) Else '' END As FTFirstInputDate,TR.FTFirstInputBy  "
                    cmdstring &= vbCrLf & " ,Case When ISDATE(TR.FTLastInputDate) = 1 Then Convert(varchar(10),Convert(datetime,TR.FTLastInputDate) ,103) Else '' END As FTLastInputDate,TR.FTLastInputBy  "
                    cmdstring &= vbCrLf & " ,Case When ISDATE(TR.FTOETCDate) = 1 Then Convert(varchar(10),Convert(datetime,TR.FTOETCDate) ,103) Else '' END As FTOETCDate "
                    cmdstring &= vbCrLf & ",FNORGLeadTime,FNFinalLeadTime,FTDelayLangth "
                    cmdstring &= vbCrLf & " ,'' AS FTSamplePPC"
                    cmdstring &= vbCrLf & "  From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_T1T2 AS TR  WITH(NOLOCK)  "
                    cmdstring &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(OrderNo) & "'  "
                    cmdstring &= vbCrLf & " AND  FNHSysRawMatId=" & MatId & "  "

                    Dim mdt As DataTable
                    mdt = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_PUR)


                    For Each Rxp As DataRow In mdt.Rows

                        With CType(Me.ogdtime.DataSource, DataTable)
                            .AcceptChanges()

                            For Each Rxd As DataRow In .Select("FTDocKey='" & HI.UL.ULF.rpQuoted(OrderNo) & "'  AND FNHSysRawMatId=" & MatId & " ")

                                Rxd!FTFirstInputDate = Rxp!FTFirstInputDate
                                Rxd!FTFirstInputBy = Rxp!FTFirstInputBy
                                Rxd!FTLastInputDate = Rxp!FTLastInputDate
                                Rxd!FTLastInputBy = Rxp!FTLastInputBy

                                Rxd!FTOETCDate = Rxp!FTOETCDate
                                Rxd!FNORGLeadTime = Rxp!FNORGLeadTime
                                Rxd!FNFinalLeadTime = Rxp!FNFinalLeadTime
                                Rxd!FTDelayLangth = Rxp!FTDelayLangth

                            Next

                            .AcceptChanges()
                        End With

                    Next

                End If

            End With

        Catch ex As Exception
        End Try
    End Sub

    Private Sub RepositoryItemCalcQty_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles RepositoryItemCalcQty.EditValueChanging
        Try
            Try
                If Val(e.NewValue.ToString) >= 0 Then

                    Dim FNPOQty As Double = Val(ogvtime.GetFocusedRowCellValue("FNPOQuantity"))


                    If FNPOQty < Val(e.NewValue.ToString) Then
                        e.Cancel = True
                    Else
                        e.Cancel = False

                        ogvtime.SetFocusedRowCellValue(ogvtime.FocusedColumn.FieldName, Val(e.NewValue.ToString))
                    End If


                Else
                    e.Cancel = True

                End If
            Catch ex As Exception

            End Try
        Catch ex As Exception

        End Try
    End Sub

    Private Sub RepositoryItemCalcEditFNLeadTime_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles RepositoryItemCalcEditFNLeadTime.EditValueChanging
        Try
            If Val(e.NewValue.ToString) >= 0 Then
                e.Cancel = False

                Dim pLT As Integer = Val(e.NewValue.ToString)

                ogvtime.SetFocusedRowCellValue(ogvtime.FocusedColumn.FieldName, Val(e.NewValue.ToString))

            Else
                e.Cancel = True

            End If
        Catch ex As Exception

        End Try
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
                If NewData <> GridDataNoteBefore Then

                    Dim FieldName As String = .FocusedColumn.FieldName.ToString

                    Dim OrderNo As String = .GetRowCellValue(.FocusedRowHandle, "FTDocKey").ToString()
                    Dim MatId As Integer = Integer.Parse(Val("0" & .GetRowCellValue(.FocusedRowHandle, "FNHSysRawMatId").ToString()))
                    Dim LeadTime As Integer = Integer.Parse(Val("0" & .GetRowCellValue(.FocusedRowHandle, "FNLeadTime").ToString()))
                    Dim OETCDate As String = .GetRowCellValue(.FocusedRowHandle, "FTOETCDate").ToString()
                    Dim Doctype As Integer = Val(.GetRowCellValue(.FocusedRowHandle, "FNDocType").ToString())

                    Dim cmdstring As String = ""

                    cmdstring = "  DECLARE @CountData int = 0 "
                    cmdstring &= vbCrLf & " update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_T1T2  Set "



                    cmdstring &= vbCrLf & " FTInvoiceNo='" & HI.UL.ULF.rpQuoted(NewData) & "'"
                    cmdstring &= vbCrLf & ",FTInvoiceNoInputBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                    cmdstring &= vbCrLf & ",FTInvoiceNoInputDate=" & HI.UL.ULDate.FormatDateDB & ""
                    cmdstring &= vbCrLf & ",FTInvoiceNoInputTime=" & HI.UL.ULDate.FormatTimeDB & ""

                    cmdstring &= vbCrLf & ",FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                    cmdstring &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB & ""
                    cmdstring &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & ""

                    cmdstring &= vbCrLf & ",FTFirstInputBy= CASE WHEN ISNULL(FTFirstInputBy,'') ='' THEN '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' ELSE FTFirstInputBy END "
                    cmdstring &= vbCrLf & ",FTFirstInputDate=CASE WHEN ISNULL(FTFirstInputBy,'') ='' THEN " & HI.UL.ULDate.FormatDateDB & " ELSE FTFirstInputDate END "
                    cmdstring &= vbCrLf & ",FTFirstInputTime=CASE WHEN ISNULL(FTFirstInputBy,'') ='' THEN " & HI.UL.ULDate.FormatTimeDB & " ELSE FTFirstInputTime END "

                    cmdstring &= vbCrLf & ",FTLastInputBy=CASE WHEN ISNULL(FTFirstInputBy,'') <>'' THEN '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'  ELSE FTLastInputBy END "
                    cmdstring &= vbCrLf & ",FTLastInputDate=CASE WHEN ISNULL(FTFirstInputBy,'') <>'' THEN " & HI.UL.ULDate.FormatDateDB & "  ELSE FTLastInputDate END "
                    cmdstring &= vbCrLf & ",FTLastInputTime=CASE WHEN ISNULL(FTFirstInputBy,'') <>'' THEN " & HI.UL.ULDate.FormatTimeDB & "  ELSE FTLastInputTime END "
                    cmdstring &= vbCrLf & " ,FNLeadTime=" & LeadTime & ""
                    cmdstring &= vbCrLf & " ,FTOETCDate='" & HI.UL.ULDate.ConvertEnDB(OETCDate) & "'"

                    cmdstring &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(OrderNo) & "'  "
                    cmdstring &= vbCrLf & " AND  FNHSysRawMatId=" & MatId & "  "
                    cmdstring &= vbCrLf & " SET @CountData = @@ROWCOUNT  "
                    cmdstring &= vbCrLf & " IF @CountData <=0 "
                    cmdstring &= vbCrLf & "   BEGIN "

                    cmdstring &= vbCrLf & "        INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_T1T2(FTInsUser, FDInsDate, FTInsTime, FTPurchaseNo,FNDocType, FNHSysRawMatId, FTFirstInputBy, FTFirstInputDate,FTFirstInputTime,FNLeadTime,FTOETCDate "


                    cmdstring &= vbCrLf & ", FTInvoiceNo"
                    cmdstring &= vbCrLf & ",FTInvoiceNoInputBy "
                    cmdstring &= vbCrLf & ",FTInvoiceNoInputDate"
                    cmdstring &= vbCrLf & ",FTInvoiceNoInputTime"

                    cmdstring &= vbCrLf & ")  "
                    cmdstring &= vbCrLf & "  SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                    cmdstring &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                    cmdstring &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ""
                    cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(OrderNo) & "' "
                    cmdstring &= vbCrLf & "," & Doctype & " "
                    cmdstring &= vbCrLf & "," & MatId & " "
                    cmdstring &= vbCrLf & ", '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                    cmdstring &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                    cmdstring &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ""
                    cmdstring &= vbCrLf & " ," & LeadTime & ""
                    cmdstring &= vbCrLf & " ,'" & HI.UL.ULDate.ConvertEnDB(OETCDate) & "'"

                    cmdstring &= vbCrLf & ", '" & HI.UL.ULF.rpQuoted(NewData) & "' "
                    cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                    cmdstring &= vbCrLf & ",=" & HI.UL.ULDate.FormatDateDB & ""
                    cmdstring &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ""
                    cmdstring &= vbCrLf & " SET @CountData = @@ROWCOUNT  "

                    cmdstring &= vbCrLf & "   END "

                    cmdstring &= vbCrLf & "  Select  Top 1 Case When ISDATE(TR.FTFirstInputDate) = 1 Then Convert(varchar(10),Convert(datetime,TR.FTFirstInputDate) ,103) Else '' END As FTFirstInputDate,TR.FTFirstInputBy  "
                    cmdstring &= vbCrLf & " ,Case When ISDATE(TR.FTLastInputDate) = 1 Then Convert(varchar(10),Convert(datetime,TR.FTLastInputDate) ,103) Else '' END As FTLastInputDate,TR.FTLastInputBy  "

                    cmdstring &= vbCrLf & " ,'' AS FTSamplePPC"
                    cmdstring &= vbCrLf & "  From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_T1T2 AS TR  WITH(NOLOCK)  "
                    cmdstring &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(OrderNo) & "'  "
                    cmdstring &= vbCrLf & " AND  FNHSysRawMatId=" & MatId & "  "

                    Dim mdt As DataTable
                    mdt = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_PUR)


                    For Each Rxp As DataRow In mdt.Rows

                        With CType(Me.ogdtime.DataSource, DataTable)
                            .AcceptChanges()

                            For Each Rxd As DataRow In .Select("FTDocKey='" & HI.UL.ULF.rpQuoted(OrderNo) & "'  AND FNHSysRawMatId=" & MatId & " ")

                                Rxd!FTFirstInputDate = Rxp!FTFirstInputDate
                                Rxd!FTFirstInputBy = Rxp!FTFirstInputBy
                                Rxd!FTLastInputDate = Rxp!FTLastInputDate
                                Rxd!FTLastInputBy = Rxp!FTLastInputBy


                            Next

                            .AcceptChanges()
                        End With


                    Next


                End If

            End With

        Catch ex As Exception
        End Try
    End Sub

    Private Sub RepositoryItemTextEditFTSendSMPStatus_Leave(sender As Object, e As EventArgs) Handles RepositoryItemTextEditFTSendSMPStatus.Leave
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
                If NewData <> GridDataNoteBefore Then

                    Dim FieldName As String = .FocusedColumn.FieldName.ToString

                    Dim OrderNo As String = .GetRowCellValue(.FocusedRowHandle, "FTDocKey").ToString()
                    Dim MatId As Integer = Integer.Parse(Val("0" & .GetRowCellValue(.FocusedRowHandle, "FNHSysRawMatId").ToString()))
                    Dim LeadTime As Integer = Integer.Parse(Val("0" & .GetRowCellValue(.FocusedRowHandle, "FNLeadTime").ToString()))
                    Dim OETCDate As String = .GetRowCellValue(.FocusedRowHandle, "FTOETCDate").ToString()
                    Dim Doctype As Integer = Val(.GetRowCellValue(.FocusedRowHandle, "FNDocType").ToString())

                    Dim cmdstring As String = ""

                    cmdstring = "  DECLARE @CountData int = 0 "
                    cmdstring &= vbCrLf & " update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_T1T2  Set "


                    Select Case ogvtime.FocusedColumn.FieldName.ToString
                        Case "FTSendSMPStatus"

                            cmdstring &= vbCrLf & " FTSendSMPStatus='" & HI.UL.ULF.rpQuoted(NewData) & "'"
                            cmdstring &= vbCrLf & ",FTSendSMPStatusInputBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                            cmdstring &= vbCrLf & ",FTSendSMPStatusInputDate=" & HI.UL.ULDate.FormatDateDB & ""
                            cmdstring &= vbCrLf & ",FTSendSMPStatusInputTime=" & HI.UL.ULDate.FormatTimeDB & ""

                        Case "FTSendSMPAWB"

                            cmdstring &= vbCrLf & " FTSendSMPAWB='" & HI.UL.ULF.rpQuoted(NewData) & "'"
                            cmdstring &= vbCrLf & ",FTSendSMPAWBInputBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                            cmdstring &= vbCrLf & ",FTSendSMPAWBInputDate=" & HI.UL.ULDate.FormatDateDB & ""
                            cmdstring &= vbCrLf & ",FTSendSMPAWBInputTime=" & HI.UL.ULDate.FormatTimeDB & ""
                        Case "FTSendSMPPayType"
                            cmdstring &= vbCrLf & " FTSendSMPPayType='" & HI.UL.ULF.rpQuoted(NewData) & "'"
                            cmdstring &= vbCrLf & ",FTSendSMPPayTypeInputBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                            cmdstring &= vbCrLf & ",FTSendSMPPayTypeInputDate=" & HI.UL.ULDate.FormatDateDB & ""
                            cmdstring &= vbCrLf & ",FTSendSMPPayTypeInputTime=" & HI.UL.ULDate.FormatTimeDB & ""

                    End Select

                    cmdstring &= vbCrLf & ",FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                    cmdstring &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB & ""
                    cmdstring &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & ""

                    cmdstring &= vbCrLf & ",FTFirstInputBy= CASE WHEN ISNULL(FTFirstInputBy,'') ='' THEN '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' ELSE FTFirstInputBy END "
                    cmdstring &= vbCrLf & ",FTFirstInputDate=CASE WHEN ISNULL(FTFirstInputBy,'') ='' THEN " & HI.UL.ULDate.FormatDateDB & " ELSE FTFirstInputDate END "
                    cmdstring &= vbCrLf & ",FTFirstInputTime=CASE WHEN ISNULL(FTFirstInputBy,'') ='' THEN " & HI.UL.ULDate.FormatTimeDB & " ELSE FTFirstInputTime END "

                    cmdstring &= vbCrLf & ",FTLastInputBy=CASE WHEN ISNULL(FTFirstInputBy,'') <>'' THEN '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'  ELSE FTLastInputBy END "
                    cmdstring &= vbCrLf & ",FTLastInputDate=CASE WHEN ISNULL(FTFirstInputBy,'') <>'' THEN " & HI.UL.ULDate.FormatDateDB & "  ELSE FTLastInputDate END "
                    cmdstring &= vbCrLf & ",FTLastInputTime=CASE WHEN ISNULL(FTFirstInputBy,'') <>'' THEN " & HI.UL.ULDate.FormatTimeDB & "  ELSE FTLastInputTime END "
                    cmdstring &= vbCrLf & " ,FNLeadTime=" & LeadTime & ""
                    cmdstring &= vbCrLf & " ,FTOETCDate='" & HI.UL.ULDate.ConvertEnDB(OETCDate) & "'"

                    cmdstring &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(OrderNo) & "'  "
                    cmdstring &= vbCrLf & " AND  FNHSysRawMatId=" & MatId & "  "
                    cmdstring &= vbCrLf & " SET @CountData = @@ROWCOUNT  "
                    cmdstring &= vbCrLf & " IF @CountData <=0 "
                    cmdstring &= vbCrLf & "   BEGIN "

                    cmdstring &= vbCrLf & "        INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_T1T2(FTInsUser, FDInsDate, FTInsTime, FTPurchaseNo,FNDocType, FNHSysRawMatId, FTFirstInputBy, FTFirstInputDate,FTFirstInputTime,FNLeadTime,FTOETCDate "

                    Select Case ogvtime.FocusedColumn.FieldName.ToString
                        Case "FTSendSMPStatus"

                            cmdstring &= vbCrLf & ", FTSendSMPStatus"
                            cmdstring &= vbCrLf & ",FTSendSMPStatusInputBy "
                            cmdstring &= vbCrLf & ",FTSendSMPStatusInputDate"
                            cmdstring &= vbCrLf & ",FTSendSMPStatusInputTime"

                        Case "FTSendSMPAWB"

                            cmdstring &= vbCrLf & ", FTSendSMPAWB"
                            cmdstring &= vbCrLf & ",FTSendSMPAWBInputBy "
                            cmdstring &= vbCrLf & ",FTSendSMPAWBInputDate"
                            cmdstring &= vbCrLf & ",FTSendSMPAWBInputTime"
                        Case "FTSendSMPPayType"
                            cmdstring &= vbCrLf & ", FTSendSMPPayType"
                            cmdstring &= vbCrLf & ",FTSendSMPPayTypeInputBy "
                            cmdstring &= vbCrLf & ",FTSendSMPPayTypeInputDate"
                            cmdstring &= vbCrLf & ",FTSendSMPPayTypeInputTime"

                    End Select

                    cmdstring &= vbCrLf & ")  "
                    cmdstring &= vbCrLf & "  SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                    cmdstring &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                    cmdstring &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ""
                    cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(OrderNo) & "' "
                    cmdstring &= vbCrLf & "," & Doctype & " "
                    cmdstring &= vbCrLf & "," & MatId & " "
                    cmdstring &= vbCrLf & ", '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                    cmdstring &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                    cmdstring &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ""
                    cmdstring &= vbCrLf & " ," & LeadTime & ""
                    cmdstring &= vbCrLf & " ,'" & HI.UL.ULDate.ConvertEnDB(OETCDate) & "'"

                    Select Case ogvtime.FocusedColumn.FieldName.ToString
                        Case "FTSendSMPStatus"

                            cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(NewData) & "'"
                            cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                            cmdstring &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                            cmdstring &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ""

                        Case "FTSendSMPAWB"

                            cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(NewData) & "'"
                            cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                            cmdstring &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                            cmdstring &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ""
                        Case "FTSendSMPPayType"
                            cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(NewData) & "'"
                            cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                            cmdstring &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                            cmdstring &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ""

                    End Select

                    cmdstring &= vbCrLf & " SET @CountData = @@ROWCOUNT  "

                    cmdstring &= vbCrLf & "   END "

                    cmdstring &= vbCrLf & "  Select  Top 1 Case When ISDATE(TR.FTFirstInputDate) = 1 Then Convert(varchar(10),Convert(datetime,TR.FTFirstInputDate) ,103) Else '' END As FTFirstInputDate,TR.FTFirstInputBy  "
                    cmdstring &= vbCrLf & " ,Case When ISDATE(TR.FTLastInputDate) = 1 Then Convert(varchar(10),Convert(datetime,TR.FTLastInputDate) ,103) Else '' END As FTLastInputDate,TR.FTLastInputBy  "

                    cmdstring &= vbCrLf & " ,'' AS FTSamplePPC "
                    cmdstring &= vbCrLf & "  From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_T1T2 AS TR  WITH(NOLOCK)  "
                    cmdstring &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(OrderNo) & "'  "
                    cmdstring &= vbCrLf & " AND  FNHSysRawMatId=" & MatId & "  "

                    Dim mdt As DataTable
                    mdt = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_PUR)


                    For Each Rxp As DataRow In mdt.Rows

                        With CType(Me.ogdtime.DataSource, DataTable)
                            .AcceptChanges()

                            For Each Rxd As DataRow In .Select("FTDocKey='" & HI.UL.ULF.rpQuoted(OrderNo) & "'  AND FNHSysRawMatId=" & MatId & " ")

                                Rxd!FTFirstInputDate = Rxp!FTFirstInputDate
                                Rxd!FTFirstInputBy = Rxp!FTFirstInputBy
                                Rxd!FTLastInputDate = Rxp!FTLastInputDate
                                Rxd!FTLastInputBy = Rxp!FTLastInputBy


                            Next

                            .AcceptChanges()
                        End With


                    Next


                End If

            End With

        Catch ex As Exception
        End Try
    End Sub


    Private Sub RepositoryItemPopupContainerTrackNote_QueryPopUp(sender As Object, e As CancelEventArgs) Handles RepositoryItemPopupContainerTrackNote.QueryPopUp
        Try

            With Me.ogvtime

                Dim OrderNo As String = .GetRowCellValue(.FocusedRowHandle, "FTDocKey").ToString()
                Dim MatId As Integer = Integer.Parse(Val("0" & .GetRowCellValue(.FocusedRowHandle, "FNHSysRawMatId").ToString()))


                Dim cmdstring As String = ""


                Dim TrackNote As String = ""


                cmdstring = "SELECT TR.FNTrackSeq , TR.FTTrackBy  ,TR.FTTrackDate  ,TR.FTContactName  ,TR.FTContactName  ,TR.FTNote "
                cmdstring &= vbCrLf & " FROM	[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_T1T2_Tracking AS TR  WITH(NOLOCK)  "
                cmdstring &= vbCrLf & " 	  WHERE TR.FTPurchaseNo ='" & HI.UL.ULF.rpQuoted(OrderNo) & "' AND FNHSysRawMatId=" & MatId & "  "
                cmdstring &= vbCrLf & "  ORDER BY  TR.FNTrackSeq "

                Dim dt As DataTable = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_PUR)

                For Each R As DataRow In dt.Rows

                    TrackNote &= "No . " & R!FNTrackSeq.ToString & Environment.NewLine
                    TrackNote &= "Track By : " & R!FTTrackBy.ToString & Environment.NewLine
                    TrackNote &= "Track Date : " & HI.UL.ULDate.ConvertEN(R!FTTrackDate.ToString) & Environment.NewLine
                    TrackNote &= "Contact Name : " & R!FTContactName.ToString & Environment.NewLine
                    TrackNote &= "Detail : " & R!FTNote.ToString & Environment.NewLine & Environment.NewLine


                Next

                FTTRNote.Text = TrackNote

            End With

        Catch ex As Exception

        End Try
    End Sub


    Private Sub FNDataType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles FNDataType.SelectedIndexChanged

        Try
            If _FormLoad = False Then
                Call HI.UL.AppRegistry.WriteRegistry("ListDDataType" & Me.Name, FNDataType.SelectedIndex.ToString)
            End If

            If FNDataType.SelectedIndex <> 2 Then

                With Me.ogvtime
                    '.Columns.ColumnByFieldName("FTDevConfirmDate").Visible = False
                    .Columns.ColumnByFieldName("FTPartCode").Visible = False
                    .Columns.ColumnByFieldName("FTPartDetail").Visible = False
                    '.Columns.ColumnByFieldName("FTOrderDate").Visible = False
                    .Columns.ColumnByFieldName("FNUsedQuantity").Visible = False
                    .Columns.ColumnByFieldName("FTUsedUnit").Visible = False
                    '.Columns.ColumnByFieldName("FTSuplCalCode").Visible = False
                    '.Columns.ColumnByFieldName("FDSupCFMDelDate").Visible = False
                End With
            Else
                With Me.ogvtime
                    '.Columns.ColumnByFieldName("FTDevConfirmDate").Visible = True
                    .Columns.ColumnByFieldName("FTPartCode").Visible = True
                    .Columns.ColumnByFieldName("FTPartDetail").Visible = True
                    '.Columns.ColumnByFieldName("FTOrderDate").Visible = True
                    .Columns.ColumnByFieldName("FNUsedQuantity").Visible = True
                    .Columns.ColumnByFieldName("FTUsedUnit").Visible = True
                    '.Columns.ColumnByFieldName("FTSuplCalCode").Visible = True
                    '.Columns.ColumnByFieldName("FDSupCFMDelDate").Visible = True
                End With
            End If

            ogdtime.DataSource = Nothing
        Catch ex As Exception

        End Try

    End Sub

    Private Sub ogvtime_RowStyle(sender As Object, e As RowStyleEventArgs) Handles ogvtime.RowStyle
        Try
            With Me.ogvtime
                If "" & .GetRowCellValue(e.RowHandle, "FTStateCancel").ToString = "1" Then

                    e.Appearance.BackColor = System.Drawing.Color.Pink
                    e.Appearance.ForeColor = System.Drawing.Color.Red

                End If

            End With
        Catch ex As Exception

        End Try

    End Sub

    Private Sub ocmholdpurchase_Click(sender As Object, e As EventArgs) Handles ocmholdpurchase.Click
        Try
            Dim dtpo As DataTable
            Dim dtpoList As DataTable

            With CType(Me.ogdtime.DataSource, DataTable)
                .AcceptChanges()

                dtpo = .Copy()

            End With

            Dim tReason As String = ""
            Dim pHoldId As Integer = 0
            Dim pHoldCode As String = ""
            Dim pHoldName As String = ""

            If dtpo.Select("FTSelect='1' AND FNDocType=0  AND  FTStateSendMail='0' ").Length <= 0 Then

                Exit Sub

            Else

                With New wInputHoldReason
                    .bW_Confirm = False
                    .ShowDialog()

                    If .bW_Confirm Then

                        tReason = .otbCancelReason.Text.Trim
                        pHoldId = Val(.FNHSysPOHoldId.Properties.Tag.ToString)
                        pHoldCode = .FNHSysPOHoldId.Text
                        pHoldName = .FNHSysPOHoldId_None.Text.Trim
                    End If

                End With

                If pHoldId = 0 Then
                    Exit Sub

                End If
            End If

            dtpoList = dtpo.Select("FTSelect='1' AND FNDocType=0  AND  FTStateSendMail='0' ").CopyToDataTable

            Dim _FTMail As String = ""
            Dim _FTMailCC As String = ""
            Dim TemplateMail As String = ""

            Dim PoNo As String = ""
            Dim PoAllNo As String = ""
            Dim UpdatePoAllNo As String = ""
            Dim PoState As Integer = 0

            Dim grp As List(Of String) = (dtpoList.Select("FNHSysSuplId>0 AND FNDocType=0", "FTPurchaseNo").CopyToDataTable).AsEnumerable() _
                                                      .Select(Function(r) r.Field(Of String)("FTPurchaseNo")) _
                                                      .Distinct() _
                                                      .ToList()


            For Each Ind As String In grp

                PoNo = ""

                Dim cmdstring As String = ""

                cmdstring = " UPDATE  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase  SET FTStateHold='1',FNHSysPOHoldId=" & pHoldId & ",FTHoldNote='" & HI.UL.ULF.rpQuoted(tReason) & "',FTHoldBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "',FTHoldDate=" & HI.UL.ULDate.FormatDateDB & ",FTHoldTime=" & HI.UL.ULDate.FormatTimeDB & ""
                cmdstring &= vbCrLf & "  WHERE FTPurchaseNo ='" & HI.UL.ULF.rpQuoted(Ind) & "' "

                If HI.Conn.SQLConn.ExecuteNonQuery(cmdstring, Conn.DB.DataBaseName.DB_PUR) Then

                    With CType(Me.ogdtime.DataSource, DataTable)
                        .AcceptChanges()

                        For Each Rxp As DataRow In .Select("FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Ind) & "'")
                            Rxp!FTStateHold = "1"
                            Rxp!FTHoldReason = pHoldName


                        Next

                        .AcceptChanges()

                    End With


                End If

            Next

        Catch ex As Exception
            MsgBox(ex.Message)

        End Try


    End Sub

    Private Sub FTStartPurchaseDate_EditValueChanged(sender As Object, e As EventArgs) Handles FTStartPurchaseDate.EditValueChanged
        If FTStartPurchaseDate.Text.ToString <> "" Then
            FTEndPurchaseDate.Properties.MinValue = FTStartPurchaseDate.EditValue
        End If
    End Sub

    Private Sub FTStartDelivery_EditValueChanged(sender As Object, e As EventArgs) Handles FTStartDelivery.EditValueChanged
        If FTStartDelivery.Text.ToString <> "" Then
            FTEndDelivery.Properties.MinValue = FTStartDelivery.EditValue
        End If
    End Sub

    Private Sub FTStartOrderDate_EditValueChanged(sender As Object, e As EventArgs) Handles FTStartOrderDate.EditValueChanged
        If FTStartOrderDate.Text.ToString <> "" Then
            FTEndOrderDate.Properties.MinValue = FTStartOrderDate.EditValue
        End If
    End Sub
End Class
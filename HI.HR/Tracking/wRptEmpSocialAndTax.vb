Imports DevExpress.Data
Imports System.IO
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraGrid
Imports System.Windows.Forms
Imports DevExpress.XtraGrid.Views.Grid

Public Class wRptEmpSocialAndTax
    Private StateCal As Boolean = False
    Private _RowDataChange As Boolean

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        Call InitialGridMergCell()
        Call InitGrid()

    End Sub



#Region "Initial Grid"

    Private Property FNHSysSuplId As Object

    Private Sub InitGrid()
        '------Start Add Summary Grid-------------
        Dim sFieldCount As String = ""
        Dim sFieldSum As String = "FNSalary|FCOTBaht|FNIncentiveAmt|OtherAdd|FNTotalIncome|FNSocial|FNTax|FNNetpay|AvgSalary|EmpTotal|OtherExpense|FNSocialPack|FNTaxPack"

        Dim sFieldGrpCount As String = ""
        Dim sFieldGrpSum As String = "FNSalary|FCOTBaht|FNIncentiveAmt|OtherAdd|FNTotalIncome|FNSocial|FNTax|FNNetpay|AvgSalary|EmpTotal|OtherExpense|FNSocialPack|FNTaxPack"



        '_Qry = "SELECT       FNDisAmt, FNQuantity, FNNetAmt, FTRawMatCode,  FTRawMatColorCode, FTRawMatSizeCode, "


        '_Qry &= vbCrLf & "   FTUnitCode,  FTReceiveNo, FNRCVQty, CASE WHEN ISDATE(FDReceiveDate) = 1 THEN Convert(varchar(10),Convert(datetime,FDReceiveDate) ,103) ELSE '' END AS FDReceiveDate, FTInvoiceNo, TotalRCV, FTOrderNo, FTTransferWHNo,FTRTSNo,"
        '_Qry &= vbCrLf & "   FNTWQty, FTIssueNo, FNISSQty, FTSaleAndTerminateNo, FNSNTQty, FTTransferCenterNo, FNRTSQty, FTReturnSuplNo, RTSISQty, BAL, TCQty, FDPurchaseDate, FDDeliveryDate,FTSuplCode, RCVTotal"



        Dim sFieldCustomSum As String = ""
        Dim sFieldCustomGrpSum As String = ""

        With ogvtime
            .ClearGrouping()
            .ClearDocument()


            For Each Str As String In sFieldCount.Split("|")
                If Str <> "" Then
                    .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Count, Str)
                    .Columns(Str).SummaryItem.DisplayFormat = "{0:n0}"
                End If
            Next

            For Each Str As String In sFieldCustomSum.Split("|")
                If Str <> "" Then
                    .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Custom, Str)
                    .Columns(Str).SummaryItem.DisplayFormat = "{0:n0}"
                End If
            Next

            For Each Str As String In sFieldSum.Split("|")
                If Str <> "" Then
                    .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Sum, Str)
                    .Columns(Str).SummaryItem.DisplayFormat = "{0:n2}" '" & HI.ST.Config.QtyDigit.ToString & "
                End If
            Next

            For Each Str As String In sFieldGrpCount.Split("|")
                If Str <> "" Then
                    .GroupSummary.Add(DevExpress.Data.SummaryItemType.Count, Str, Nothing, "(Count by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n2})")
                End If
            Next


            For Each Str As String In sFieldCustomGrpSum.Split("|")
                If Str <> "" Then


                    .GroupSummary.Add(DevExpress.Data.SummaryItemType.Custom, Str, Nothing, "(Sum by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n2})")
                End If
            Next

            For Each Str As String In sFieldGrpSum.Split("|")
                If Str <> "" Then
                    .GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, Str, Nothing, "(Sum by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n2})")
                End If
            Next

            'For Each Str As String In sFieldCustomGrpSum.Split("|")
            '    If Str <> "" Then

            '        .GroupSummary.Add(DevExpress.Data.SummaryItemType.Custom, Str, Nothing, "(Sum by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n2})")
            '    End If
            'Next



            .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
            .OptionsView.ShowFooter = True

            .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleIfExpanded


            .ExpandAllGroups()
            .RefreshData()


        End With

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
    '            If HI.ST.Lang.Language = ST.Lang.Lang.TH Then
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


    Private Sub Gridview1_CustomSummaryCalculate(ByVal sender As Object, ByVal e As DevExpress.Data.CustomSummaryEventArgs) Handles ogvtime.CustomSummaryCalculate
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
    Private Sub Gridview_CustomSummaryCalculate(ByVal sender As Object, ByVal e As DevExpress.Data.CustomSummaryEventArgs)
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
        Dim _oDt As DataTable
        Try
            StateCal = False

            Dim _Spls As New HI.TL.SplashScreen("Loading...   Please Wait   ")





            _Qry = "SELECT P.FTPayYear,P.FTPayTerm,sum(isnull(P.FCBaht,0)) AS FNSalary, sum(isnull(P.FCOt1_Baht+P.FCOt15_Baht+P.FCOt2_Baht+P.FCOt3_Baht+P.FCOt4_Baht,0)) AS FCOTBaht,"
            _Qry &= vbCrLf & "sum(isnull(FNIncentiveAmt,0)) As FNIncentiveAmt, count(*) As EmpTotal, sum(isnull(P.FNTotalAddOther,0)) As OtherAdd,Sum(isnull(P.FNTotalExpenseOther,0)) As OtherExpense,"
            _Qry &= vbCrLf & "sum(isnull(P.FNTotalIncome,0)) AS FNTotalIncome,sum(isnull(P.FNSocial,0)) AS FNSocial,sum(isnull(P.FNTax,0)) AS FNTax,(SUBSTRING(V.FDCalDateBegin, 0, 3) + '-' + V.FDCalDateEnd) AS FDCalDate,"
            _Qry &= vbCrLf & "sum(isnull(P.FNNetpay,0)) As FNNetpay,"
            _Qry &= vbCrLf & "            Case WHEN V.FTTermOfMonth = 2 THEN DD.FNSocial ELSE 0.00 END AS FNSocialPack,"
            _Qry &= vbCrLf & "Case WHEN V.FTTermOfMonth = 2 THEN DD.FNTax ELSE 0.00 END AS FNTaxPack,"
            _Qry &= vbCrLf & "DD.FNMonth,V.FTTermOfMonth"
            _Qry &= vbCrLf & "FROM "
            _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTPayRoll AS P LEFT OUTER JOIN"
            _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.V_SysBrowse_237 AS V ON P.FTPayTerm = V.FTPayTerm And P.FTPayYear = V.FTPayYear LEFT OUTER JOIN"
            _Qry &= vbCrLf & "(SELECT D.FTPayYear,V.FNMonth,sum(D.FNSocial) AS FNSocial,sum(D.FNTax) AS FNTax FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.V_RptDailyWage AS D LEFT OUTER JOIN"
            _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.V_SysBrowse_237 AS V ON D.FTPayYear = V.FTPayYear And D.FTPayTerm = V.FTPayTerm"
            _Qry &= vbCrLf & "GROUP BY D.FTPayYear,V.FNMonth) AS DD ON P.FTPayYear = DD.FTPayYear AND V.FNMonth = DD.FNMonth"


            _Qry &= vbCrLf & "WHERE P.FTPayYear Is not null"


            If Me.FTPayYear.Text <> "" Then
                _Qry &= vbCrLf & " AND P.FTPayYear='" & HI.UL.ULF.rpQuoted(Me.FTPayYear.Text) & "'"
            End If
            If Me.FTPayTerm.Text <> "" Then
                _Qry &= vbCrLf & " AND P.FTPayTerm='" & HI.UL.ULF.rpQuoted(Me.FTPayTerm.Text) & "'"
            End If

            'EndEmpType

            _Qry &= vbCrLf & "GROUP BY P.FTPayYear, P.FTPayTerm, V.FDCalDateBegin, V.FDCalDateEnd, DD.FNSocial, DD.FNTax, DD.FNMonth, V.FTTermOfMonth"
            _Qry &= vbCrLf & "ORDER BY P.FTPayTerm"

            _oDt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_INVEN)

            Me.ogdtime.DataSource = _oDt.Copy
            Me.ogvtime.ExpandAllGroups()

            _oDt.Dispose()
            _Spls.Close()

            _RowDataChange = False

        Catch ex As Exception

        End Try



    End Sub

    Private Function VerifyData() As Boolean
        Dim _Pass As Boolean = False

        If Me.FTPayYear.Text <> "" Then
            _Pass = True
        End If

        If Me.FTPayTerm.Text <> "" Then
            _Pass = True
        End If

        If Not (_Pass) Then
            HI.MG.ShowMsg.mInfo("กรุณาทำการเลือกเงื่อไข อย่างน้อย 1 รายการ !!!", 1406170001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
        End If

        Return _Pass
    End Function
#End Region

#Region "General"

    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Me.FNHSysCmpId.Text = "1"
            Me.FNHSysCmpId.Text = HI.ST.SysInfo.CmpCode
            Me.FNHSysCmpId.Properties.ReadOnly = True
            Me.FNHSysCmpId.Properties.Buttons(0).Visible = False
            Me.FNHSysCmpId.Properties.Buttons(0).Enabled = False





            HI.UL.AppRegistry.LoadLayoutGridFromRegistry(Me, Me.ogvtime)
            StateCal = False
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmload.Click
        If VerifyData() Then
            Call LoadData()


        End If
    End Sub

    Private Sub ocmclear_Click(sender As System.Object, e As System.EventArgs) Handles ocmclear.Click
        HI.TL.HandlerControl.ClearControl(Me)
    End Sub

#End Region

    Private Sub ocmsavelayout_Click(sender As Object, e As EventArgs) Handles ocmsavelayout.Click
        HI.UL.AppRegistry.SaveLayoutGridToRegistry(Me, Me.ogvtime)
        HI.MG.ShowMsg.mInfo("Save Layout Grid Complete...", 1404240001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Information)
    End Sub



    Private Sub ocmpreview_Click(sender As Object, e As EventArgs) Handles ocmpreview.Click
        'With Me.ogvtime
        '    If .RowCount <= 0 Then Exit Sub
        '    If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub


        '    Dim _Qry As String = ""




        '    With New HI.RP.Report

        '        .FormTitle = Me.Text
        '        .ReportFolderName = "Inventrory\"
        '        .ReportName = "ReceiveHistory.rpt"


        '        .Formular = _Qry
        '        .Preview()

        '        ''HI.ST.Lang.Language = _tmplang
        '    End With

        'End With
    End Sub

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub


    Private Sub InitialGridMergCell()

        For Each c As GridColumn In ogvtime.Columns

            Select Case c.FieldName.ToString
                'Case "FTPurchaseNo", "FTRawMatCode", "FTRawMatName", "FTRawMatColorCode", "FTRawMatSizeCode", "FTSuplCode", "FTSuplName", "FTUnitCode", "FTReceiveNo", "FTInvoiceNo", "FTUnitCodeRCV", _
                '    "FTSaleAndTerminateNo", "FTOrderNo", "FTTransferWHNo", "FTIssueNo", "FDReceiveDate", "FNQuantity", "FTUnitNameRCV", "FTTransferCenterNo", "FTReturnSuplNo", "FTRTSNo"
                '    c.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True
                '    c.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
                'Case Else
                Case Else
                    c.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False
            End Select
        Next
    End Sub


    Private Sub ogvtime_CellMerge(sender As Object, e As DevExpress.XtraGrid.Views.Grid.CellMergeEventArgs) Handles ogvtime.CellMerge
        Try
            With Me.ogvtime


                If .GetRowCellValue(e.RowHandle1, "FTPurchaseNo").ToString = .GetRowCellValue(e.RowHandle2, "FTPurchaseNo").ToString Then


                    If e.Column.FieldName = "FTRawMatSizeCode" Then
                        If .GetRowCellValue(e.RowHandle1, "FTRawMatCode").ToString = .GetRowCellValue(e.RowHandle2, "FTRawMatCode").ToString And
                            .GetRowCellValue(e.RowHandle1, "FTRawMatColorCode").ToString = .GetRowCellValue(e.RowHandle2, "FTRawMatColorCode").ToString Then

                            e.Merge = (e.CellValue1.ToString = e.CellValue2.ToString)
                            e.Handled = True

                        Else

                            e.Merge = False
                            e.Handled = True
                        End If
                        'e.Merge = False
                        'e.Handled = True
                    Else

                        'e.Merge = (e.CellValue1.ToString = e.CellValue2.ToString)
                        'e.Handled = True

                    End If


                    If e.Column.FieldName = "FNQuantity" Then
                        'If .GetRowCellValue(e.RowHandle1, "FTRawMatCode").ToString = .GetRowCellValue(e.RowHandle2, "FTRawMatCode").ToString And _
                        '    .GetRowCellValue(e.RowHandle1, "FTRawMatColorCode").ToString = .GetRowCellValue(e.RowHandle2, "FTRawMatColorCode").ToString And _
                        '    .GetRowCellValue(e.RowHandle1, "FTRawMatSizeCode").ToString = .GetRowCellValue(e.RowHandle2, "FTRawMatSizeCode").ToString And _
                        '    .GetRowCellValue(e.RowHandle1, "FTOrderNo").ToString = .GetRowCellValue(e.RowHandle2, "FTOrderNo").ToString Then
                        If .GetRowCellValue(e.RowHandle1, "FTOrderNo").ToString = .GetRowCellValue(e.RowHandle2, "FTOrderNo").ToString Then


                            e.Merge = (e.CellValue1.ToString = e.CellValue2.ToString)
                            e.Handled = True

                            'If e.Column.FieldName = "FTOrderNo" Then
                            '    If .GetRowCellValue(e.RowHandle1, "FTReceiveNo").ToString = .GetRowCellValue(e.RowHandle2, "FTReceiveNo").ToString Then
                            '        e.Merge = (e.CellValue1.ToString = e.CellValue2.ToString)
                            '        e.Handled = True

                            '    Else
                            '        'e.Merge = False
                            '        'e.Handled = True
                            '        e.Merge = (e.CellValue1.ToString = e.CellValue2.ToString)
                            '        e.Handled = True
                            '    End If
                            'Else
                            '    'e.Merge = (e.CellValue1.ToString = e.CellValue2.ToString)
                            '    'e.Handled = True
                            'End If


                        Else
                            e.Merge = False
                            e.Handled = True

                        End If
                    Else
                        'e.Merge = (e.CellValue1.ToString = e.CellValue2.ToString)
                        'e.Handled = True

                    End If
                    'Else

                    '    'e.Merge = False
                    '    'e.Handled = True
                    'End If
                    '    'e.Merge = False
                    '    'e.Handled = True
                    'Else

                    '    e.Merge = (e.CellValue1.ToString = e.CellValue2.ToString)
                    '    e.Handled = True

                    'End If





                Else
                    e.Merge = False
                    e.Handled = True
                End If



            End With

        Catch ex As Exception

        End Try
    End Sub


    Private m_DbDtEmployeeType As New DataTable
    ReadOnly Property DbDtEmployeeType As DataTable
        Get
            Return m_DbDtEmployeeType
        End Get
    End Property










    Private Sub hideContainerTop_Click(sender As Object, e As EventArgs) Handles hideContainerTop.Click

    End Sub

    Private Sub ogdtime_Click(sender As Object, e As EventArgs) Handles ogdtime.Click

    End Sub
End Class
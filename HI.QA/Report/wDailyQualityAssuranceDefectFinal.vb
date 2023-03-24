Imports DevExpress.XtraCharts
Imports DevExpress.XtraEditors.Controls
Imports System.Drawing

Public Class wDailyQualityAssuranceDefectFinal

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        pivotGridControl.OptionsChartDataSource.ProvideDataByColumns = ceChartDataVertical.Checked
        pivotGridControl.OptionsChartDataSource.SelectionOnly = ceSelectionOnly.Checked
        pivotGridControl.OptionsChartDataSource.ProvideColumnGrandTotals = ceShowColumnGrandTotals.Checked
        pivotGridControl.OptionsChartDataSource.ProvideRowGrandTotals = ceShowRowGrandTotals.Checked

        opggrpdefectcode.OptionsChartDataSource.ProvideDataByColumns = ceChartDataVertical.Checked
        opggrpdefectcode.OptionsChartDataSource.SelectionOnly = ceSelectionOnly.Checked
        opggrpdefectcode.OptionsChartDataSource.ProvideColumnGrandTotals = ceShowColumnGrandTotals.Checked
        opggrpdefectcode.OptionsChartDataSource.ProvideRowGrandTotals = ceShowRowGrandTotals.Checked


        chartControl.CrosshairOptions.ShowArgumentLine = False
        chartControlgrpdefect.CrosshairOptions.ShowArgumentLine = False

        Dim restrictedTypes() As ViewType = {ViewType.PolarArea, ViewType.PolarLine, ViewType.SideBySideGantt, ViewType.Bubble, ViewType.SideBySideRangeBar, ViewType.RangeBar, ViewType.Gantt, ViewType.PolarPoint, ViewType.Stock, ViewType.CandleStick, ViewType.SideBySideFullStackedBar, ViewType.SideBySideFullStackedBar3D, ViewType.SideBySideStackedBar, ViewType.SideBySideStackedBar3D}
        For Each type As ViewType In System.Enum.GetValues(GetType(ViewType))

            If Array.IndexOf(Of ViewType)(restrictedTypes, type) >= 0 Then
                Continue For
            End If
            comboChartType.Properties.Items.Add(type)

        Next type

        comboChartType.SelectedItem = ViewType.Bar
        chartControl.DataSource = pivotGridControl
        chartControlgrpdefect.DataSource = opggrpdefectcode


        chartControl.SeriesTemplate.LabelsVisibility = If(True, DevExpress.Utils.DefaultBoolean.True, DevExpress.Utils.DefaultBoolean.False)
        chartControl.CrosshairEnabled = If(True, DevExpress.Utils.DefaultBoolean.False, DevExpress.Utils.DefaultBoolean.True)
        CType(chartControl.SeriesTemplate.Label, BarSeriesLabel).Position = BarSeriesLabelPosition.Top


        chartControlgrpdefect.SeriesTemplate.LabelsVisibility = If(True, DevExpress.Utils.DefaultBoolean.True, DevExpress.Utils.DefaultBoolean.False)
        chartControlgrpdefect.CrosshairEnabled = If(True, DevExpress.Utils.DefaultBoolean.False, DevExpress.Utils.DefaultBoolean.True)

        CType(chartControlgrpdefect.SeriesTemplate.Label, BarSeriesLabel).Position = BarSeriesLabelPosition.Top

        chartControlgrpdefect.SeriesTemplate.SeriesPointsSorting = SortingMode.Descending
        chartControlgrpdefect.SeriesTemplate.SeriesPointsSortingKey = SeriesPointKey.Value_1

        Call InitGrid()

    End Sub

#Region "Chart"
    '<comboChartType>
    Private Sub comboBoxEdit2_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles comboChartType.SelectedIndexChanged
        chartControl.SeriesTemplate.ChangeView(CType(comboChartType.SelectedItem, ViewType))
        chartControlgrpdefect.SeriesTemplate.ChangeView(CType(comboChartType.SelectedItem, ViewType))

        If chartControl.SeriesTemplate.Label IsNot Nothing Then
            chartControl.SeriesTemplate.LabelsVisibility = If(checkShowPointLabels.Checked, DevExpress.Utils.DefaultBoolean.True, DevExpress.Utils.DefaultBoolean.False)
            chartControl.CrosshairEnabled = If(checkShowPointLabels.Checked, DevExpress.Utils.DefaultBoolean.False, DevExpress.Utils.DefaultBoolean.True)

            chartControlgrpdefect.SeriesTemplate.LabelsVisibility = If(checkShowPointLabels.Checked, DevExpress.Utils.DefaultBoolean.True, DevExpress.Utils.DefaultBoolean.False)
            chartControlgrpdefect.CrosshairEnabled = If(checkShowPointLabels.Checked, DevExpress.Utils.DefaultBoolean.False, DevExpress.Utils.DefaultBoolean.True)

            checkShowPointLabels.Enabled = True
        Else
            checkShowPointLabels.Enabled = False
        End If

        chartControl.Legend.Visible = False
        chartControlgrpdefect.Legend.Visible = False

        'If (TryCast(chartControl.SeriesTemplate.View, SimpleDiagramSeriesViewBase)) Is Nothing Then
        '    chartControl.Legend.Visible = True
        'End If

        If TypeOf chartControl.Diagram Is Diagram3D Then
            Dim diagram As Diagram3D = CType(chartControl.Diagram, Diagram3D)
            diagram.RuntimeRotation = True
            diagram.RuntimeZooming = True
            diagram.RuntimeScrolling = True


            Dim diagram2 As Diagram3D = CType(chartControlgrpdefect.Diagram, Diagram3D)
            diagram2.RuntimeRotation = True
            diagram2.RuntimeZooming = True
            diagram2.RuntimeScrolling = True

        End If

        For Each series As Series In chartControl.Series
            Dim supportTransparency As ISupportTransparency = TryCast(series.View, ISupportTransparency)
            If supportTransparency IsNot Nothing Then
                If (TypeOf series.View Is AreaSeriesView) OrElse (TypeOf series.View Is Area3DSeriesView) OrElse (TypeOf series.View Is RadarAreaSeriesView) OrElse (TypeOf series.View Is Bar3DSeriesView) Then
                    supportTransparency.Transparency = 135
                Else
                    supportTransparency.Transparency = 0
                End If
            End If


        Next series

        For Each series As Series In chartControlgrpdefect.Series
            Dim supportTransparency As ISupportTransparency = TryCast(series.View, ISupportTransparency)
            If supportTransparency IsNot Nothing Then
                If (TypeOf series.View Is AreaSeriesView) OrElse (TypeOf series.View Is Area3DSeriesView) OrElse (TypeOf series.View Is RadarAreaSeriesView) OrElse (TypeOf series.View Is Bar3DSeriesView) Then
                    supportTransparency.Transparency = 135
                Else
                    supportTransparency.Transparency = 0
                End If
            End If



        Next series

    End Sub
    '</comboChartType>

    '<checkShowPointLabels>
    Private Sub checkEdit1_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles checkShowPointLabels.CheckedChanged
        chartControl.SeriesTemplate.LabelsVisibility = If(checkShowPointLabels.Checked, DevExpress.Utils.DefaultBoolean.True, DevExpress.Utils.DefaultBoolean.False)
        chartControl.CrosshairEnabled = If(checkShowPointLabels.Checked, DevExpress.Utils.DefaultBoolean.False, DevExpress.Utils.DefaultBoolean.True)

        chartControlgrpdefect.SeriesTemplate.LabelsVisibility = If(checkShowPointLabels.Checked, DevExpress.Utils.DefaultBoolean.True, DevExpress.Utils.DefaultBoolean.False)
        chartControlgrpdefect.CrosshairEnabled = If(checkShowPointLabels.Checked, DevExpress.Utils.DefaultBoolean.False, DevExpress.Utils.DefaultBoolean.True)

    End Sub


    '</checkShowPointLabels>

    '<ceChartDataVertical>
    Private Sub ceChartDataVertical_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ceChartDataVertical.CheckedChanged
        pivotGridControl.OptionsChartDataSource.ProvideDataByColumns = ceChartDataVertical.Checked

        opggrpdefectcode.OptionsChartDataSource.ProvideDataByColumns = ceChartDataVertical.Checked
    End Sub
    '</ceChartDataVertical>
    '<ceSelectionOnly>
    Private Sub ceSelectionOnly_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ceSelectionOnly.CheckedChanged
        pivotGridControl.OptionsChartDataSource.SelectionOnly = ceSelectionOnly.Checked
        opggrpdefectcode.OptionsChartDataSource.ProvideDataByColumns = ceChartDataVertical.Checked

        seUpdateDelay.Enabled = ceSelectionOnly.Checked
        lblUpdateDelay.Enabled = seUpdateDelay.Enabled
    End Sub
    '</ceSelectionOnly>
    '<ceShowColumnGrandTotals>
    Private Sub ceShowColumnGrandTotals_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ceShowColumnGrandTotals.CheckedChanged
        pivotGridControl.OptionsChartDataSource.ProvideColumnGrandTotals = ceShowColumnGrandTotals.Checked
        opggrpdefectcode.OptionsChartDataSource.ProvideColumnGrandTotals = ceShowColumnGrandTotals.Checked
    End Sub
    '</ceShowColumnGrandTotals>

    '<ceShowRowGrandTotals>
    Private Sub ceShowRowGrandTotals_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ceShowRowGrandTotals.CheckedChanged
        pivotGridControl.OptionsChartDataSource.ProvideRowGrandTotals = ceShowRowGrandTotals.Checked
        opggrpdefectcode.OptionsChartDataSource.ProvideRowGrandTotals = ceShowRowGrandTotals.Checked
    End Sub
    '</ceShowRowGrandTotals>

    '<seUpdateDelay>
    Private Sub seUpdateDelay_EditValueChanged(ByVal sender As Object, ByVal e As EventArgs) Handles seUpdateDelay.EditValueChanged
        pivotGridControl.OptionsChartDataSource.UpdateDelay = CInt(Fix(seUpdateDelay.Value))
        opggrpdefectcode.OptionsChartDataSource.UpdateDelay = CInt(Fix(seUpdateDelay.Value))
    End Sub
    '</seUpdateDelay>

#End Region

#Region "Procedure"
    Private Sub LoadData()
        Dim _Qry As String = ""
        Dim dt As New DataTable
        Dim dtchart As New DataTable
        Dim OutputQty As Integer = 0
        Dim QAQty As Integer = 0
        Dim DefectQty As Integer = 0
        Dim DerfectPoint As Integer = 0
        Dim _Spls As New HI.TL.SplashScreen("Loading Data... Please Wait... ")

        Try
            _Qry = "EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.SP_GETDATA_QADAILY_DEFECT_PREFINAL " & Integer.Parse(Val(Me.FNHSysCmpId.Properties.Tag.ToString())) & ",'" & HI.UL.ULDate.ConvertEnDB(Me.FTSDate.Text) & "','" & HI.UL.ULDate.ConvertEnDB(Me.FTEDate.Text) & "'"
            dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

            dtchart = dt.Clone



            For Each R As DataRow In dt.Select("FTQACode<>''")
                ' dtchart.ImportRow(R)

                If dtchart.Select("FTDefectGrp='" & HI.UL.ULF.rpQuoted(R!FTDefectGrp.ToString) & "'").Length <= 0 Then

                    dtchart.ImportRow(R)

                Else

                    For Each Rx As DataRow In dtchart.Select("FTDefectGrp='" & HI.UL.ULF.rpQuoted(R!FTDefectGrp.ToString) & "'")

                        Rx!FNQty = Val(Rx!FNQty) + Val(R!FNQty.ToString)
                        Rx!FNDfectPerByQA = Val(Rx!FNDfectPerByQA) + Val(R!FNDfectPerByQA.ToString)
                        Rx!FNDfectPerByDefect = Val(Rx!FNDfectPerByDefect) + Val(R!FNDfectPerByDefect.ToString)
                        Rx!FNDfectPerByDefectSummary = Val(Rx!FNDfectPerByDefectSummary) + Val(R!FNDfectPerByDefectSummary.ToString)

                    Next

                End If

                OutputQty = Val(R!FNTotalIn.ToString)
                QAQty = Val(R!FNTotalQA.ToString)
                DefectQty = Val(R!FNTotalDefect.ToString)
                DerfectPoint = Val(R!FNTotalDefectPoint.ToString)

            Next

            Me.FNTotalIn.Value = OutputQty
            Me.FNTotalQA.Value = QAQty
            Me.FNTotalQADefect.Value = DefectQty
            Me.FNTotalQADefectPoint.Value = DerfectPoint

            Me.ogcdefect.DataSource = dt.Copy
            Me.ogcdefectgrp.DataSource = dtchart.Copy

            pivotGridControl.DataSource = dt.Copy
            chartControl.RefreshData()

            opggrpdefectcode.DataSource = dtchart.Copy

            opggrpdefectcode.RefreshData()

            _Spls.Close()
        Catch ex As Exception
            _Spls.Close()
        End Try

        dt.Dispose()

    End Sub

#End Region

#Region "Grid Control"

    Private Sub InitGrid()

        '------Start Add Summary Grid-------------
        Dim sFieldCount As String = "FNSeq"
        Dim sFieldSumQty As String = "FNQty"
        Dim sFieldSum As String = "FNDfectPerByQA|FNDfectPerByDefect"
        Dim sFieldGrpCount As String = ""
        Dim sFieldGrpSum As String = ""

        'T.FNLateNormalMin, T.FNLateNormalCut
        Dim sFieldCustomSum As String = ""
        Dim sFieldCustomGrpSum As String = ""

        With ogvdefect

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

            For Each Str As String In sFieldSumQty.Split("|")
                If Str <> "" Then
                    .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Sum, Str)
                    .Columns(Str).SummaryItem.DisplayFormat = "{0:n0}"
                End If
            Next


            For Each Str As String In sFieldSum.Split("|")
                If Str <> "" Then
                    .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Sum, Str)
                    .Columns(Str).SummaryItem.DisplayFormat = "{0:n2}"
                End If
            Next

            For Each Str As String In sFieldGrpCount.Split("|")
                If Str <> "" Then
                    .GroupSummary.Add(DevExpress.Data.SummaryItemType.Count, Str, Nothing, "(Count by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n0})")
                End If
            Next

            For Each Str As String In sFieldCustomGrpSum.Split("|")
                If Str <> "" Then
                    .GroupSummary.Add(DevExpress.Data.SummaryItemType.Custom, Str, Nothing, "(Sum by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n0})")
                End If
            Next

            For Each Str As String In sFieldGrpSum.Split("|")
                If Str <> "" Then
                    .GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, Str, Nothing, "(Sum by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n2})")
                End If
            Next

            .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
            .OptionsView.ShowFooter = True
            .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleIfExpanded
            .OptionsView.ShowGroupPanel = False
            .ExpandAllGroups()
            .RefreshData()

        End With

        sFieldCount = ""
        sFieldSumQty = "FNQty"
        sFieldSum = "FNDfectPerByQA"
        sFieldGrpCount = ""
        sFieldGrpSum = ""

        'T.FNLateNormalMin, T.FNLateNormalCut
        sFieldCustomSum = ""
        sFieldCustomGrpSum = ""


        With ogvdefectgrp

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

            For Each Str As String In sFieldSumQty.Split("|")
                If Str <> "" Then
                    .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Sum, Str)
                    .Columns(Str).SummaryItem.DisplayFormat = "{0:n0}"
                End If
            Next

            For Each Str As String In sFieldSum.Split("|")
                If Str <> "" Then
                    .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Sum, Str)
                    .Columns(Str).SummaryItem.DisplayFormat = "{0:n2}"
                End If
            Next

            For Each Str As String In sFieldGrpCount.Split("|")
                If Str <> "" Then
                    .GroupSummary.Add(DevExpress.Data.SummaryItemType.Count, Str, Nothing, "(Count by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n0})")
                End If
            Next

            For Each Str As String In sFieldCustomGrpSum.Split("|")
                If Str <> "" Then
                    .GroupSummary.Add(DevExpress.Data.SummaryItemType.Custom, Str, Nothing, "(Sum by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n0})")
                End If
            Next

            For Each Str As String In sFieldGrpSum.Split("|")
                If Str <> "" Then
                    .GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, Str, Nothing, "(Sum by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n2})")
                End If
            Next

            .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
            .OptionsView.ShowFooter = True
            .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleIfExpanded
            .OptionsView.ShowGroupPanel = False
            .ExpandAllGroups()

            .RefreshData()

        End With
        '------End Add Summary Grid-------------
    End Sub
#End Region

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ocmload_Click(sender As Object, e As EventArgs) Handles ocmload.Click
        If Me.FTSDate.Text <> "" And Me.FTEDate.Text <> "" Then

            Call LoadData()



        Else
            HI.MG.ShowMsg.mInfo("กรุณาทำการระบุวันที่ที่ต้องการดูข้อมูล !!!", 1406138871, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub wHRBIWageSummary_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.FNHSysCmpId.Text = HI.ST.SysInfo.CmpCode

        ogvdefect.OptionsView.ShowAutoFilterRow = False
        ogvdefectgrp.OptionsView.ShowAutoFilterRow = False

    End Sub

    Private Sub chartControl_CustomDrawSeriesPoint(sender As Object, e As CustomDrawSeriesPointEventArgs) Handles chartControl.CustomDrawSeriesPoint
        ' These changes will be applied to Bar Series only.
        Dim drawOptions = CType(e.SeriesDrawOptions, BarDrawOptions)
        If drawOptions Is Nothing Then
            Return
        End If

        ' Get the fill options for the series point.
        drawOptions.FillStyle.FillMode = FillMode.Gradient
        Dim options = CType(drawOptions.FillStyle.Options, RectangleGradientFillOptions)
        If options Is Nothing Then
            Return
        End If


        options.Color2 = Color.FromArgb(154, 196, 84)
        drawOptions.Color = Color.FromArgb(81, 137, 3)
        drawOptions.Border.Color = Color.FromArgb(100, 39, 91, 1)

        '' Get the value at the current series point.
        'Dim val As Double = e.SeriesPoint(0)


        '' If the value is less than 2.5, then fill the bar with green colors.
        'If val < 2.5 Then
        '    options.Color2 = Color.FromArgb(154, 196, 84)
        '    drawOptions.Color = Color.FromArgb(81, 137, 3)
        '    drawOptions.Border.Color = Color.FromArgb(100, 39, 91, 1)
        '    ' ... if the value is less than 5.5, then fill the bar with yellow colors.
        'Else
        '    If val < 5.5 Then
        '        options.Color2 = Color.FromArgb(254, 233, 124)
        '        drawOptions.Color = Color.FromArgb(249, 170, 15)
        '        drawOptions.Border.Color = Color.FromArgb(60, 165, 73, 5)
        '        ' ... if the value is greater, then fill the bar with red colors.
        '    Else
        '        options.Color2 = Color.FromArgb(242, 143, 112)
        '        drawOptions.Color = Color.FromArgb(199, 57, 12)
        '        drawOptions.Border.Color = Color.FromArgb(100, 155, 26, 0)
        '    End If
        'End If
    End Sub

    Private Sub chartControlgrpdefect_CustomDrawSeriesPoint(sender As Object, e As CustomDrawSeriesPointEventArgs) Handles chartControlgrpdefect.CustomDrawSeriesPoint
        ' These changes will be applied to Bar Series only.
        Dim drawOptions = CType(e.SeriesDrawOptions, BarDrawOptions)
        If drawOptions Is Nothing Then
            Return
        End If

        ' Get the fill options for the series point.
        drawOptions.FillStyle.FillMode = FillMode.Gradient
        Dim options = CType(drawOptions.FillStyle.Options, RectangleGradientFillOptions)
        If options Is Nothing Then
            Return
        End If

        options.Color2 = Color.FromArgb(154, 196, 84)
        drawOptions.Color = Color.FromArgb(81, 137, 3)
        drawOptions.Border.Color = Color.FromArgb(100, 39, 91, 1)

        '' Get the value at the current series point.
        'Dim val As Double = e.SeriesPoint(0)


        '' If the value is less than 2.5, then fill the bar with green colors.
        'If val < 2.5 Then
        '    options.Color2 = Color.FromArgb(154, 196, 84)
        '    drawOptions.Color = Color.FromArgb(81, 137, 3)
        '    drawOptions.Border.Color = Color.FromArgb(100, 39, 91, 1)
        '    ' ... if the value is less than 5.5, then fill the bar with yellow colors.
        'Else
        '    If val < 5.5 Then
        '        options.Color2 = Color.FromArgb(254, 233, 124)
        '        drawOptions.Color = Color.FromArgb(249, 170, 15)
        '        drawOptions.Border.Color = Color.FromArgb(60, 165, 73, 5)
        '        ' ... if the value is greater, then fill the bar with red colors.
        '    Else
        '        options.Color2 = Color.FromArgb(242, 143, 112)
        '        drawOptions.Color = Color.FromArgb(199, 57, 12)
        '        drawOptions.Border.Color = Color.FromArgb(100, 155, 26, 0)
        '    End If
        'End If
    End Sub
End Class
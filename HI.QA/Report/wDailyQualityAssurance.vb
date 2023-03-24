Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.XtraCharts
Imports DevExpress.XtraEditors.Controls

Public Class wDailyQualityAssurance

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        pivotGridControl.OptionsChartDataSource.ProvideDataByColumns = ceChartDataVertical.Checked
        pivotGridControl.OptionsChartDataSource.SelectionOnly = ceSelectionOnly.Checked
        pivotGridControl.OptionsChartDataSource.ProvideColumnGrandTotals = ceShowColumnGrandTotals.Checked
        pivotGridControl.OptionsChartDataSource.ProvideRowGrandTotals = ceShowRowGrandTotals.Checked

        chartControl.CrosshairOptions.ShowArgumentLine = False

        Dim restrictedTypes() As ViewType = {ViewType.PolarArea, ViewType.PolarLine, ViewType.SideBySideGantt, ViewType.Bubble, ViewType.SideBySideRangeBar, ViewType.RangeBar, ViewType.Gantt, ViewType.PolarPoint, ViewType.Stock, ViewType.CandleStick, ViewType.SideBySideFullStackedBar, ViewType.SideBySideFullStackedBar3D, ViewType.SideBySideStackedBar, ViewType.SideBySideStackedBar3D}
        For Each type As ViewType In System.Enum.GetValues(GetType(ViewType))
            If Array.IndexOf(Of ViewType)(restrictedTypes, type) >= 0 Then
                Continue For
            End If
            comboChartType.Properties.Items.Add(type)
        Next type
        comboChartType.SelectedItem = ViewType.Bar
        chartControl.DataSource = pivotGridControl


    End Sub

#Region "Chart"
    '<comboChartType>
    Private Sub comboBoxEdit2_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles comboChartType.SelectedIndexChanged
        chartControl.SeriesTemplate.ChangeView(CType(comboChartType.SelectedItem, ViewType))
        If chartControl.SeriesTemplate.Label IsNot Nothing Then
            chartControl.SeriesTemplate.LabelsVisibility = If(checkShowPointLabels.Checked, DevExpress.Utils.DefaultBoolean.True, DevExpress.Utils.DefaultBoolean.False)
            chartControl.CrosshairEnabled = If(checkShowPointLabels.Checked, DevExpress.Utils.DefaultBoolean.False, DevExpress.Utils.DefaultBoolean.True)
            checkShowPointLabels.Enabled = True
        Else
            checkShowPointLabels.Enabled = False
        End If

        chartControl.Legend.Visible = False
        'If (TryCast(chartControl.SeriesTemplate.View, SimpleDiagramSeriesViewBase)) Is Nothing Then
        '    chartControl.Legend.Visible = True
        'End If

        If TypeOf chartControl.Diagram Is Diagram3D Then
            Dim diagram As Diagram3D = CType(chartControl.Diagram, Diagram3D)
            diagram.RuntimeRotation = True
            diagram.RuntimeZooming = True
            diagram.RuntimeScrolling = True
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
    End Sub
    '</comboChartType>

    '<checkShowPointLabels>
    Private Sub checkEdit1_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles checkShowPointLabels.CheckedChanged
        chartControl.SeriesTemplate.LabelsVisibility = If(checkShowPointLabels.Checked, DevExpress.Utils.DefaultBoolean.True, DevExpress.Utils.DefaultBoolean.False)
        chartControl.CrosshairEnabled = If(checkShowPointLabels.Checked, DevExpress.Utils.DefaultBoolean.False, DevExpress.Utils.DefaultBoolean.True)
    End Sub
    '</checkShowPointLabels>

    '<ceChartDataVertical>
    Private Sub ceChartDataVertical_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ceChartDataVertical.CheckedChanged
        pivotGridControl.OptionsChartDataSource.ProvideDataByColumns = ceChartDataVertical.Checked
    End Sub
    '</ceChartDataVertical>
    '<ceSelectionOnly>
    Private Sub ceSelectionOnly_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ceSelectionOnly.CheckedChanged
        pivotGridControl.OptionsChartDataSource.SelectionOnly = ceSelectionOnly.Checked
        seUpdateDelay.Enabled = ceSelectionOnly.Checked
        lblUpdateDelay.Enabled = seUpdateDelay.Enabled
    End Sub
    '</ceSelectionOnly>
    '<ceShowColumnGrandTotals>
    Private Sub ceShowColumnGrandTotals_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ceShowColumnGrandTotals.CheckedChanged
        pivotGridControl.OptionsChartDataSource.ProvideColumnGrandTotals = ceShowColumnGrandTotals.Checked
    End Sub
    '</ceShowColumnGrandTotals>

    '<ceShowRowGrandTotals>
    Private Sub ceShowRowGrandTotals_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ceShowRowGrandTotals.CheckedChanged
        pivotGridControl.OptionsChartDataSource.ProvideRowGrandTotals = ceShowRowGrandTotals.Checked
    End Sub
    '</ceShowRowGrandTotals>

    '<seUpdateDelay>
    Private Sub seUpdateDelay_EditValueChanged(ByVal sender As Object, ByVal e As EventArgs) Handles seUpdateDelay.EditValueChanged
        pivotGridControl.OptionsChartDataSource.UpdateDelay = CInt(Fix(seUpdateDelay.Value))
    End Sub
    '</seUpdateDelay>


#End Region

#Region "Procedure"
    Private Sub LoadData()
        Dim _Qry As String = ""
        Dim dt As New DataTable
        Dim dtchart As New DataTable
        Dim _Spls As New HI.TL.SplashScreen("Loading Data... Please Wait... ")

        Try
            _Qry = "EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.SP_GETDATA_QADAILY " & Integer.Parse(Val(Me.FNHSysCmpId.Properties.Tag.ToString())) & ",'" & HI.UL.ULDate.ConvertEnDB(Me.FTDate.Text) & "'"
            dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

            dtchart = dt.Clone

            dtchart.Columns("FTData").DataType = GetType(Integer)
            'dtchart.Columns.Add("FTData2", GetType(Double))
            For Each R As DataRow In dt.Select("FTState='03'")
                dtchart.ImportRow(R)
            Next



            LoadChart(dt.Select("FTState='04' ").CopyToDataTable, dt.Select("FTState='03' ").CopyToDataTable)

            For Each R As DataRow In dt.Rows
                If R!FTState = "04" Then
                    R!FTData = R!FTData.ToString & " %"
                End If

            Next

            For Each R As DataRow In dt.Rows
                If R!FTState.ToString.Length > 2 Then
                    R!FTState = Microsoft.VisualBasic.Right(R!FTState.ToString, 1)
                Else
                    R!FTState = ""
                End If
            Next

            Me.pivotGriddata.DataSource = dt.Copy
            pivotGriddata.BestFit()
            pivotGriddata.ExpandAll()

            pivotGridControl.DataSource = dtchart.Copy
            chartControl.RefreshData()

            _Spls.Close()
        Catch ex As Exception
            _Spls.Close()
        End Try

        dt.Dispose()

    End Sub

#End Region

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ocmload_Click(sender As Object, e As EventArgs) Handles ocmload.Click
        If Me.FTDate.Text <> "" Then
            Call LoadData()
        Else
            HI.MG.ShowMsg.mInfo("กรุณาทำการระบุวันที่ที่ต้องการดูข้อมูล !!!", 1406138871, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
        End If
    End Sub

    Dim oChart As New DevExpress.XtraCharts.ChartControl
    Private Sub LoadChart(ByVal _oDt As DataTable, ByVal _oDtd As DataTable)
        Try

            'oChart.ClearSelection()
            oChart = New DevExpress.XtraCharts.ChartControl

            ' Create the first side-by-side bar series and add points to it.
            Dim _Date As Date = Date.Now.ToString("d")
            Dim series1 As New Series("", ViewType.Bar)
            ' Dim series2 As New Series("", ViewType.Line)

            Dim pointSeriesLabel1 As New DevExpress.XtraCharts.PointSeriesLabel()
            Dim pointSeriesLabel2 As New DevExpress.XtraCharts.PointSeriesLabel()
            Dim _UnitSectCodeBefore As String = ""
            Dim _Where As String = ""
            For Each R As DataRow In _oDtd.Rows
                series1.Points.Add(New SeriesPoint(R!FTUnitSectCode.ToString, New Double() {CDbl("0" & R!FTData.ToString)}))
            Next
            'For Each R As DataRow In _oDt.Rows
            '    series2.Points.Add(New SeriesPoint(R!FTUnitSectCode.ToString, New Double() {CDbl("0" & R!FTData.ToString)}))
            'Next

            oChart.Series.Add(series1)
            ' oChart.Series.Add(series2)
            'series1.Label.TextPattern = "{VP}"
            ' series2.Label.TextPattern = "{V:f2}"

            '  oChart.Series(1).View.Color = Color.Green
            For Each series As Series In oChart.Series
                If series.Label IsNot Nothing Then
                    series.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True

                End If
            Next series
            Dim LineSeriesView4 As DevExpress.XtraCharts.LineSeriesView = New DevExpress.XtraCharts.LineSeriesView()
            LineSeriesView4.LineMarkerOptions.Size = 8
            LineSeriesView4.Color = System.Drawing.Color.Blue
            LineSeriesView4.MarkerVisibility = DevExpress.Utils.DefaultBoolean.[True]
            ' series2.View = LineSeriesView4

            Dim label As SideBySideBarSeriesLabel = TryCast(series1.Label, SideBySideBarSeriesLabel)
            label.Position = BarSeriesLabelPosition.Top

            oChart.CrosshairEnabled = DevExpress.Utils.DefaultBoolean.False
            oChart.ToolTipEnabled = DevExpress.Utils.DefaultBoolean.False
            ' CType(oChart.SeriesTemplate.View, LineSeriesView).MarkerVisibility = DevExpress.Utils.DefaultBoolean.[True]

            '   CType(oChart.SeriesTemplate.Label, BarSeriesLabel).Position = BarSeriesLabelPosition.Top
            Try
                If TypeOf oChart.Diagram Is Diagram3D Then
                    Dim diagram As Diagram3D = CType(oChart.Diagram, Diagram3D)
                    diagram.RuntimeRotation = True
                    diagram.RuntimeZooming = True
                    diagram.RuntimeScrolling = True

                End If
            Catch ex As Exception
            End Try


            Dim chartTitle1 As New ChartTitle()


            '   chartTitle1.Text = Me.FTTitleChart.Text
            'oChart.Titles.Add(chartTitle1)
            oChart.Dock = DockStyle.Fill



            Me.otpchart.Controls.Clear()
            Me.otpchart.Controls.Add(oChart)
        Catch ex As Exception

        End Try
    End Sub



    Private Sub wHRBIWageSummary_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.FNHSysCmpId.Text = HI.ST.SysInfo.CmpCode
    End Sub

End Class
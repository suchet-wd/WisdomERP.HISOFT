Imports DevExpress.XtraCharts
Imports DevExpress.XtraEditors.Controls

Public Class wBIFGSale

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        pivotGridControlQty.OptionsChartDataSource.ProvideDataByColumns = ceChartDataVerticalQty.Checked
        pivotGridControlQty.OptionsChartDataSource.SelectionOnly = ceSelectionOnlyQty.Checked
        pivotGridControlQty.OptionsChartDataSource.ProvideColumnGrandTotals = ceShowColumnGrandTotalsQty.Checked
        pivotGridControlQty.OptionsChartDataSource.ProvideRowGrandTotals = ceShowRowGrandTotalsQty.Checked

        PivotGridControlAmt.OptionsChartDataSource.ProvideDataByColumns = ceChartDataVerticalAmt.Checked
        PivotGridControlAmt.OptionsChartDataSource.SelectionOnly = ceSelectionOnlyAmt.Checked
        PivotGridControlAmt.OptionsChartDataSource.ProvideColumnGrandTotals = ceShowColumnGrandTotalsAmt.Checked
        PivotGridControlAmt.OptionsChartDataSource.ProvideRowGrandTotals = ceShowRowGrandTotalsAmt.Checked

        chartControlAmt.CrosshairOptions.ShowArgumentLine = False
        chartControlQty.CrosshairOptions.ShowArgumentLine = False

        Dim restrictedTypes() As ViewType = {ViewType.PolarArea, ViewType.PolarLine, ViewType.SideBySideGantt, ViewType.Bubble, ViewType.SideBySideRangeBar, ViewType.RangeBar, ViewType.Gantt, ViewType.PolarPoint, ViewType.Stock, ViewType.CandleStick, ViewType.SideBySideFullStackedBar, ViewType.SideBySideFullStackedBar3D, ViewType.SideBySideStackedBar, ViewType.SideBySideStackedBar3D}
        For Each type As ViewType In System.Enum.GetValues(GetType(ViewType))
            If Array.IndexOf(Of ViewType)(restrictedTypes, type) >= 0 Then
                Continue For
            End If
            comboChartTypeAmt.Properties.Items.Add(type)
            comboChartTypeQty.Properties.Items.Add(type)
        Next type
        comboChartTypeQty.SelectedItem = ViewType.Bar
        comboChartTypeAmt.SelectedItem = ViewType.Bar

        chartControlAmt.DataSource = PivotGridControlAmt
        chartControlQty.DataSource = pivotGridControlQty
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
        If (TryCast(chartControl.SeriesTemplate.View, SimpleDiagramSeriesViewBase)) Is Nothing Then
            chartControl.Legend.Visible = True
        End If
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
        pivotGridControlQty.OptionsChartDataSource.ProvideDataByColumns = ceChartDataVertical.Checked
    End Sub
    '</ceChartDataVertical>
    '<ceSelectionOnly>
    Private Sub ceSelectionOnly_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ceSelectionOnly.CheckedChanged
        pivotGridControlQty.OptionsChartDataSource.SelectionOnly = ceSelectionOnly.Checked
        seUpdateDelay.Enabled = ceSelectionOnly.Checked
        lblUpdateDelay.Enabled = seUpdateDelay.Enabled
    End Sub
    '</ceSelectionOnly>
    '<ceShowColumnGrandTotals>
    Private Sub ceShowColumnGrandTotals_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ceShowColumnGrandTotals.CheckedChanged
        pivotGridControlQty.OptionsChartDataSource.ProvideColumnGrandTotals = ceShowColumnGrandTotals.Checked
    End Sub
    '</ceShowColumnGrandTotals>

    '<ceShowRowGrandTotals>
    Private Sub ceShowRowGrandTotals_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ceShowRowGrandTotals.CheckedChanged
        pivotGridControlQty.OptionsChartDataSource.ProvideRowGrandTotals = ceShowRowGrandTotals.Checked
    End Sub
    '</ceShowRowGrandTotals>

    '<seUpdateDelay>
    Private Sub seUpdateDelay_EditValueChanged(ByVal sender As Object, ByVal e As EventArgs) Handles seUpdateDelay.EditValueChanged
        pivotGridControlQty.OptionsChartDataSource.UpdateDelay = CInt(Fix(seUpdateDelay.Value))
    End Sub
    '</seUpdateDelay>

   

#End Region

#Region "Chart Qty"

    Private Sub comboChartTypeQty_SelectedIndexChanged(sender As Object, e As EventArgs) Handles comboChartTypeQty.SelectedIndexChanged
        chartControlQty.SeriesTemplate.ChangeView(CType(comboChartTypeQty.SelectedItem, ViewType))
        If chartControlQty.SeriesTemplate.Label IsNot Nothing Then
            chartControlQty.SeriesTemplate.LabelsVisibility = If(checkShowPointLabelsQty.Checked, DevExpress.Utils.DefaultBoolean.True, DevExpress.Utils.DefaultBoolean.False)
            chartControlQty.CrosshairEnabled = If(checkShowPointLabelsQty.Checked, DevExpress.Utils.DefaultBoolean.False, DevExpress.Utils.DefaultBoolean.True)
            checkShowPointLabelsQty.Enabled = True
        Else
            checkShowPointLabelsQty.Enabled = False
        End If
        If (TryCast(chartControlQty.SeriesTemplate.View, SimpleDiagramSeriesViewBase)) Is Nothing Then
            chartControlQty.Legend.Visible = True
        End If
        If TypeOf chartControlQty.Diagram Is Diagram3D Then
            Dim diagram As Diagram3D = CType(chartControlQty.Diagram, Diagram3D)
            diagram.RuntimeRotation = True
            diagram.RuntimeZooming = True
            diagram.RuntimeScrolling = True
        End If
        For Each series As Series In chartControlQty.Series
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
    Private Sub checkShowPointLabelsQty_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles checkShowPointLabelsQty.CheckedChanged
        chartControlQty.SeriesTemplate.LabelsVisibility = If(checkShowPointLabelsQty.Checked, DevExpress.Utils.DefaultBoolean.True, DevExpress.Utils.DefaultBoolean.False)
        chartControlQty.CrosshairEnabled = If(checkShowPointLabelsQty.Checked, DevExpress.Utils.DefaultBoolean.False, DevExpress.Utils.DefaultBoolean.True)
    End Sub
    '</checkShowPointLabels>

    '<ceChartDataVertical>
    Private Sub ceChartDataVertical_ceChartDataVerticalQty(ByVal sender As Object, ByVal e As EventArgs) Handles ceChartDataVerticalQty.CheckedChanged
        pivotGridControlQty.OptionsChartDataSource.ProvideDataByColumns = ceChartDataVerticalQty.Checked
    End Sub
    '</ceChartDataVertical>
    '<ceSelectionOnly>
    Private Sub ceSelectionOnlyQty_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ceSelectionOnlyQty.CheckedChanged
        pivotGridControlQty.OptionsChartDataSource.SelectionOnly = ceSelectionOnlyQty.Checked
        seUpdateDelayQty.Enabled = ceSelectionOnlyQty.Checked
        lblUpdateDelay.Enabled = seUpdateDelayQty.Enabled
    End Sub
    '</ceSelectionOnly>
    '<ceShowColumnGrandTotals>
    Private Sub ceShowColumnGrandTotalsQty_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ceShowColumnGrandTotalsQty.CheckedChanged
        pivotGridControlQty.OptionsChartDataSource.ProvideColumnGrandTotals = ceShowColumnGrandTotalsQty.Checked
    End Sub
    '</ceShowColumnGrandTotals>

    '<ceShowRowGrandTotals>
    Private Sub ceShowRowGrandTotalsQty_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ceShowRowGrandTotalsQty.CheckedChanged
        pivotGridControlQty.OptionsChartDataSource.ProvideRowGrandTotals = ceShowRowGrandTotalsQty.Checked
    End Sub
    '</ceShowRowGrandTotals>

    '<seUpdateDelay>
    Private Sub seUpdateDelayQty_EditValueChanged(ByVal sender As Object, ByVal e As EventArgs) Handles seUpdateDelayQty.EditValueChanged
        pivotGridControlQty.OptionsChartDataSource.UpdateDelay = CInt(Fix(seUpdateDelayQty.Value))
    End Sub
    '</seUpdateDelay>


#End Region

#Region "Chart Amt"

    Private Sub comboChartTypeAmt_SelectedIndexChanged(sender As Object, e As EventArgs) Handles comboChartTypeAmt.SelectedIndexChanged
        chartControlAmt.SeriesTemplate.ChangeView(CType(comboChartTypeAmt.SelectedItem, ViewType))
        If chartControlAmt.SeriesTemplate.Label IsNot Nothing Then
            chartControlAmt.SeriesTemplate.LabelsVisibility = If(checkShowPointLabelsAmt.Checked, DevExpress.Utils.DefaultBoolean.True, DevExpress.Utils.DefaultBoolean.False)
            chartControlAmt.CrosshairEnabled = If(checkShowPointLabelsAmt.Checked, DevExpress.Utils.DefaultBoolean.False, DevExpress.Utils.DefaultBoolean.True)
            checkShowPointLabelsAmt.Enabled = True
        Else
            checkShowPointLabelsAmt.Enabled = False
        End If
        If (TryCast(chartControlAmt.SeriesTemplate.View, SimpleDiagramSeriesViewBase)) Is Nothing Then
            chartControlAmt.Legend.Visible = True
        End If
        If TypeOf chartControlAmt.Diagram Is Diagram3D Then
            Dim diagram As Diagram3D = CType(chartControlAmt.Diagram, Diagram3D)
            diagram.RuntimeRotation = True
            diagram.RuntimeZooming = True
            diagram.RuntimeScrolling = True
        End If
        For Each series As Series In chartControlAmt.Series
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
    Private Sub checkShowPointLabelsAmt_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles checkShowPointLabelsAmt.CheckedChanged
        chartControlAmt.SeriesTemplate.LabelsVisibility = If(checkShowPointLabelsAmt.Checked, DevExpress.Utils.DefaultBoolean.True, DevExpress.Utils.DefaultBoolean.False)
        chartControlAmt.CrosshairEnabled = If(checkShowPointLabelsAmt.Checked, DevExpress.Utils.DefaultBoolean.False, DevExpress.Utils.DefaultBoolean.True)
    End Sub
    '</checkShowPointLabels>

    '<ceChartDataVertical>
    Private Sub ceChartDataVerticalAmt_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ceChartDataVerticalAmt.CheckedChanged
        PivotGridControlAmt.OptionsChartDataSource.ProvideDataByColumns = ceChartDataVerticalAmt.Checked
    End Sub
    '</ceChartDataVertical>
    '<ceSelectionOnly>
    Private Sub ceSelectionOnly_ceSelectionOnlyQty(ByVal sender As Object, ByVal e As EventArgs) Handles ceSelectionOnlyAmt.CheckedChanged
        PivotGridControlAmt.OptionsChartDataSource.SelectionOnly = ceSelectionOnlyAmt.Checked
        seUpdateDelayAmt.Enabled = ceSelectionOnlyAmt.Checked
        lblUpdateDelay.Enabled = seUpdateDelayAmt.Enabled
    End Sub
    '</ceSelectionOnly>
    '<ceShowColumnGrandTotals>
    Private Sub ceShowColumnGrandTotalsAmt_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ceShowColumnGrandTotalsAmt.CheckedChanged
        PivotGridControlAmt.OptionsChartDataSource.ProvideColumnGrandTotals = ceShowColumnGrandTotalsAmt.Checked
    End Sub
    '</ceShowColumnGrandTotals>

    '<ceShowRowGrandTotals>
    Private Sub ceShowRowGrandTotalsAmt_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ceShowRowGrandTotalsAmt.CheckedChanged
        PivotGridControlAmt.OptionsChartDataSource.ProvideRowGrandTotals = ceShowRowGrandTotalsAmt.Checked
    End Sub
    '</ceShowRowGrandTotals>

    '<seUpdateDelay>
    Private Sub seUpdateDelayAmt_EditValueChanged(ByVal sender As Object, ByVal e As EventArgs) Handles seUpdateDelayAmt.EditValueChanged
        PivotGridControlAmt.OptionsChartDataSource.UpdateDelay = CInt(Fix(seUpdateDelayAmt.Value))
    End Sub
    '</seUpdateDelay>


#End Region
#Region "Procedure"
    Private Sub LoadData()
        Dim _Qry As String = ""
        Dim dt As New DataTable

        Dim _Spls As New HI.TL.SplashScreen("Loading Data... Please Wait... ")

        Try

            _Qry = "SELECT  Top 10  LEFT(S.FDInvoiceDate, 4) AS FTYear, RIGHT(LEFT(S.FDInvoiceDate, 7), 2) AS FTMonth, D.FTOrderNo"
            _Qry &= vbCrLf & ", D.FTColorway, D.FTSizeBreakDown, D.FNQuantity, D.FNPrice, D.FTBarcodeCustNo, T.FNHSysStyleId "
            _Qry &= vbCrLf & ", T.FTStyleCode , (D.FNQuantity * D.FNPrice) as  FNNetAmt"
            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Qry &= vbCrLf & ", T.FTStyleNameTH as FTStyleName"
            Else
                _Qry &= vbCrLf & ", T.FTStyleNameEN as FTStyleName"
            End If
            _Qry &= vbCrLf & "FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTSale AS S WITH (NOLOCK) LEFT OUTER JOIN"
            _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTSale_Detail AS D WITH (NOLOCK) ON S.FTInvoiceNo = D.FTInvoiceNo LEFT OUTER JOIN"
            _Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH (NOLOCK) ON D.FTOrderNo = O.FTOrderNo LEFT OUTER JOIN"
            _Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS T WITH (NOLOCK) ON O.FNHSysStyleId = T.FNHSysStyleId"
            _Qry &= vbCrLf & "WHERE LEFT(S.FDInvoiceDate, 7) >='" & Microsoft.VisualBasic.Left(HI.UL.ULDate.ConvertEnDB(Me.FTYear.Text), 7) & "'"
            _Qry &= vbCrLf & " and LEFT(S.FDInvoiceDate, 7) <='" & Microsoft.VisualBasic.Left(HI.UL.ULDate.ConvertEnDB(Me.FTYearTo.Text), 7) & "'"
            _Qry &= vbCrLf & "Order by D.FNQuantity Desc "

            dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)
            Me.pivotGridControlQty.DataSource = dt.Copy



            _Qry = "SELECT   Top 10     LEFT(S.FDInvoiceDate, 4) AS FTYear, RIGHT(LEFT(S.FDInvoiceDate, 7), 2) AS FTMonth, D.FTOrderNo"
            _Qry &= vbCrLf & ", D.FTColorway, D.FTSizeBreakDown, D.FNQuantity, D.FNPrice, D.FTBarcodeCustNo, T.FNHSysStyleId "
            _Qry &= vbCrLf & ", T.FTStyleCode , (D.FNQuantity * D.FNPrice) as  FNNetAmt"
            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Qry &= vbCrLf & ", T.FTStyleNameTH as FTStyleName"
            Else
                _Qry &= vbCrLf & ", T.FTStyleNameEN as FTStyleName"
            End If
            _Qry &= vbCrLf & "FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTSale AS S WITH (NOLOCK) LEFT OUTER JOIN"
            _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTSale_Detail AS D WITH (NOLOCK) ON S.FTInvoiceNo = D.FTInvoiceNo LEFT OUTER JOIN"
            _Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH (NOLOCK) ON D.FTOrderNo = O.FTOrderNo LEFT OUTER JOIN"
            _Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS T WITH (NOLOCK) ON O.FNHSysStyleId = T.FNHSysStyleId"
            _Qry &= vbCrLf & "WHERE LEFT(S.FDInvoiceDate, 7) >='" & Microsoft.VisualBasic.Left(HI.UL.ULDate.ConvertEnDB(Me.FTYear.Text), 7) & "'"
            _Qry &= vbCrLf & " and LEFT(S.FDInvoiceDate, 7) <='" & Microsoft.VisualBasic.Left(HI.UL.ULDate.ConvertEnDB(Me.FTYearTo.Text), 7) & "'"
            _Qry &= vbCrLf & "Order by  (D.FNQuantity * D.FNPrice) Desc "
            dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

            Me.PivotGridControlAmt.DataSource = dt.Copy
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
        If Me.FTYear.Text <> "" And Me.FTYearTo.Text <> "" Then
            Call LoadData()
        Else
            HI.MG.ShowMsg.mInfo("กรุณาทำการเลือกช่วงข้อมูลที่ต้องการดูข้อมูล !!!", 1496130001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub Form_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Me.FTYear.Text = Date.Now
            Me.FTYearTo.Text = Date.Now
            Me.otb.SelectedTabPageIndex = 0
        Catch ex As Exception
        End Try
    End Sub

End Class
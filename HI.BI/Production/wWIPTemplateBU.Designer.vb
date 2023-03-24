<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wWIPTemplateBU
    Inherits DevExpress.XtraEditors.XtraForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim XyDiagram2 As DevExpress.XtraCharts.XYDiagram = New DevExpress.XtraCharts.XYDiagram()
        Dim SideBySideBarSeriesLabel2 As DevExpress.XtraCharts.SideBySideBarSeriesLabel = New DevExpress.XtraCharts.SideBySideBarSeriesLabel()
        Dim XyDiagram1 As DevExpress.XtraCharts.XYDiagram = New DevExpress.XtraCharts.XYDiagram()
        Dim SideBySideBarSeriesLabel1 As DevExpress.XtraCharts.SideBySideBarSeriesLabel = New DevExpress.XtraCharts.SideBySideBarSeriesLabel()
        Me.oDockManager = New DevExpress.XtraBars.Docking.DockManager(Me.components)
        Me.pivotGridControl = New DevExpress.XtraPivotGrid.PivotGridControl()
        Me.C1FNSeq = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.CFTData = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.CFNQuantity = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.CFTState = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.CFTData2 = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.chartControl = New DevExpress.XtraCharts.ChartControl()
        Me.ogbmainprocbutton = New DevExpress.XtraEditors.PanelControl()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.otmtime = New System.Windows.Forms.Timer(Me.components)
        Me.olbtitle = New DevExpress.XtraEditors.LabelControl()
        Me.chartControl2 = New DevExpress.XtraCharts.ChartControl()
        Me.Timer2 = New System.Windows.Forms.Timer(Me.components)
        Me.pivotGridControl2 = New DevExpress.XtraPivotGrid.PivotGridControl()
        Me.C2FNSeq = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.C2FTData = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.C2FNQuantity = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.C2FTState = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.C2FTData2 = New DevExpress.XtraPivotGrid.PivotGridField()
        CType(Me.oDockManager, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pivotGridControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chartControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(XyDiagram2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(SideBySideBarSeriesLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbmainprocbutton.SuspendLayout()
        CType(Me.chartControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(XyDiagram1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(SideBySideBarSeriesLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pivotGridControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'oDockManager
        '
        Me.oDockManager.Form = Me
        Me.oDockManager.TopZIndexControls.AddRange(New String() {"DevExpress.XtraBars.BarDockControl", "DevExpress.XtraBars.StandaloneBarDockControl", "System.Windows.Forms.StatusBar", "DevExpress.XtraBars.Ribbon.RibbonStatusBar", "DevExpress.XtraBars.Ribbon.RibbonControl"})
        '
        'pivotGridControl
        '
        Me.pivotGridControl.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.pivotGridControl.Cursor = System.Windows.Forms.Cursors.Default
        Me.pivotGridControl.Fields.AddRange(New DevExpress.XtraPivotGrid.PivotGridField() {Me.C1FNSeq, Me.CFTData, Me.CFNQuantity, Me.CFTState, Me.CFTData2})
        Me.pivotGridControl.Location = New System.Drawing.Point(131, 136)
        Me.pivotGridControl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.pivotGridControl.Name = "pivotGridControl"
        Me.pivotGridControl.OptionsChartDataSource.DataProvideMode = DevExpress.XtraPivotGrid.PivotChartDataProvideMode.UseCustomSettings
        Me.pivotGridControl.OptionsChartDataSource.ProvideDataByColumns = False
        Me.pivotGridControl.OptionsChartDataSource.UpdateDelay = 500
        Me.pivotGridControl.Size = New System.Drawing.Size(1420, 255)
        Me.pivotGridControl.TabIndex = 2
        Me.pivotGridControl.Visible = False
        '
        'C1FNSeq
        '
        Me.C1FNSeq.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea
        Me.C1FNSeq.AreaIndex = 0
        Me.C1FNSeq.FieldName = "FNSeq"
        Me.C1FNSeq.Name = "C1FNSeq"
        Me.C1FNSeq.SortMode = DevExpress.XtraPivotGrid.PivotSortMode.Value
        Me.C1FNSeq.Visible = False
        '
        'CFTData
        '
        Me.CFTData.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea
        Me.CFTData.AreaIndex = 0
        Me.CFTData.FieldName = "FTData"
        Me.CFTData.Name = "CFTData"
        '
        'CFNQuantity
        '
        Me.CFNQuantity.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
        Me.CFNQuantity.AreaIndex = 0
        Me.CFNQuantity.CellFormat.FormatString = "{0:n0}"
        Me.CFNQuantity.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CFNQuantity.FieldName = "FNQuantity"
        Me.CFNQuantity.Name = "CFNQuantity"
        '
        'CFTState
        '
        Me.CFTState.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea
        Me.CFTState.AreaIndex = 0
        Me.CFTState.Caption = "FTState"
        Me.CFTState.FieldName = "FTState"
        Me.CFTState.Name = "CFTState"
        '
        'CFTData2
        '
        Me.CFTData2.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea
        Me.CFTData2.AreaIndex = 1
        Me.CFTData2.Caption = "FTData2"
        Me.CFTData2.FieldName = "FTData2"
        Me.CFTData2.Name = "CFTData2"
        '
        'chartControl
        '
        Me.chartControl.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chartControl.BackColor = System.Drawing.Color.Transparent
        XyDiagram2.AxisX.Label.Staggered = True
        XyDiagram2.AxisX.VisibleInPanesSerializable = "-1"
        XyDiagram2.AxisY.VisibleInPanesSerializable = "-1"
        XyDiagram2.DefaultPane.BackColor = System.Drawing.Color.Transparent
        XyDiagram2.PaneDistance = 2
        Me.chartControl.Diagram = XyDiagram2
        Me.chartControl.Legend.AlignmentHorizontal = DevExpress.XtraCharts.LegendAlignmentHorizontal.Left
        Me.chartControl.Legend.AlignmentVertical = DevExpress.XtraCharts.LegendAlignmentVertical.TopOutside
        Me.chartControl.Legend.MaxHorizontalPercentage = 30.0R
        Me.chartControl.Location = New System.Drawing.Point(5, 54)
        Me.chartControl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.chartControl.Name = "chartControl"
        Me.chartControl.SeriesDataMember = "Series"
        Me.chartControl.SeriesSerializable = New DevExpress.XtraCharts.Series(-1) {}
        Me.chartControl.SeriesTemplate.ArgumentDataMember = "Arguments"
        Me.chartControl.SeriesTemplate.CrosshairLabelPattern = "{S} : {V:c}"
        SideBySideBarSeriesLabel2.Position = DevExpress.XtraCharts.BarSeriesLabelPosition.TopInside
        Me.chartControl.SeriesTemplate.Label = SideBySideBarSeriesLabel2
        Me.chartControl.SeriesTemplate.LabelsVisibility = DevExpress.Utils.DefaultBoolean.[True]
        Me.chartControl.SeriesTemplate.ShowInLegend = False
        Me.chartControl.SeriesTemplate.ValueDataMembersSerializable = "Values"
        Me.chartControl.Size = New System.Drawing.Size(1600, 377)
        Me.chartControl.TabIndex = 3
        '
        'ogbmainprocbutton
        '
        Me.ogbmainprocbutton.Controls.Add(Me.ocmexit)
        Me.ogbmainprocbutton.Location = New System.Drawing.Point(523, 364)
        Me.ogbmainprocbutton.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbmainprocbutton.Name = "ogbmainprocbutton"
        Me.ogbmainprocbutton.Size = New System.Drawing.Size(736, 42)
        Me.ogbmainprocbutton.TabIndex = 392
        Me.ogbmainprocbutton.Visible = False
        '
        'ocmexit
        '
        Me.ocmexit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmexit.Location = New System.Drawing.Point(551, 9)
        Me.ocmexit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmexit.Name = "ocmexit"
        Me.ocmexit.Size = New System.Drawing.Size(111, 31)
        Me.ocmexit.TabIndex = 96
        Me.ocmexit.TabStop = False
        Me.ocmexit.Tag = "2|"
        Me.ocmexit.Text = "EXIT"
        '
        'otmtime
        '
        Me.otmtime.Interval = 30000
        '
        'olbtitle
        '
        Me.olbtitle.Appearance.Font = New System.Drawing.Font("Tahoma", 20.0!, System.Drawing.FontStyle.Bold)
        Me.olbtitle.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.olbtitle.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.olbtitle.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.olbtitle.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.olbtitle.Dock = System.Windows.Forms.DockStyle.Top
        Me.olbtitle.Location = New System.Drawing.Point(0, 0)
        Me.olbtitle.Name = "olbtitle"
        Me.olbtitle.Size = New System.Drawing.Size(1610, 45)
        Me.olbtitle.TabIndex = 393
        Me.olbtitle.Text = "Cutting"
        '
        'chartControl2
        '
        Me.chartControl2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chartControl2.BackColor = System.Drawing.Color.Transparent
        XyDiagram1.AxisX.Label.Staggered = True
        XyDiagram1.AxisX.VisibleInPanesSerializable = "-1"
        XyDiagram1.AxisY.VisibleInPanesSerializable = "-1"
        XyDiagram1.DefaultPane.BackColor = System.Drawing.Color.Transparent
        XyDiagram1.PaneDistance = 2
        Me.chartControl2.Diagram = XyDiagram1
        Me.chartControl2.Legend.AlignmentHorizontal = DevExpress.XtraCharts.LegendAlignmentHorizontal.Left
        Me.chartControl2.Legend.AlignmentVertical = DevExpress.XtraCharts.LegendAlignmentVertical.TopOutside
        Me.chartControl2.Legend.MaxHorizontalPercentage = 30.0R
        Me.chartControl2.Legend.Visible = False
        Me.chartControl2.Location = New System.Drawing.Point(4, 439)
        Me.chartControl2.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.chartControl2.Name = "chartControl2"
        Me.chartControl2.SeriesDataMember = "Series"
        Me.chartControl2.SeriesSerializable = New DevExpress.XtraCharts.Series(-1) {}
        Me.chartControl2.SeriesTemplate.ArgumentDataMember = "Arguments"
        Me.chartControl2.SeriesTemplate.CrosshairLabelPattern = "{S} : {V:c}"
        SideBySideBarSeriesLabel1.Position = DevExpress.XtraCharts.BarSeriesLabelPosition.TopInside
        Me.chartControl2.SeriesTemplate.Label = SideBySideBarSeriesLabel1
        Me.chartControl2.SeriesTemplate.LabelsVisibility = DevExpress.Utils.DefaultBoolean.[True]
        Me.chartControl2.SeriesTemplate.ValueDataMembersSerializable = "Values"
        Me.chartControl2.Size = New System.Drawing.Size(1601, 459)
        Me.chartControl2.TabIndex = 394
        '
        'Timer2
        '
        Me.Timer2.Interval = 30000
        '
        'pivotGridControl2
        '
        Me.pivotGridControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.pivotGridControl2.Cursor = System.Windows.Forms.Cursors.Default
        Me.pivotGridControl2.Fields.AddRange(New DevExpress.XtraPivotGrid.PivotGridField() {Me.C2FNSeq, Me.C2FTData, Me.C2FNQuantity, Me.C2FTState, Me.C2FTData2})
        Me.pivotGridControl2.Location = New System.Drawing.Point(182, 600)
        Me.pivotGridControl2.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.pivotGridControl2.Name = "pivotGridControl2"
        Me.pivotGridControl2.OptionsChartDataSource.DataProvideMode = DevExpress.XtraPivotGrid.PivotChartDataProvideMode.UseCustomSettings
        Me.pivotGridControl2.OptionsChartDataSource.ProvideDataByColumns = False
        Me.pivotGridControl2.OptionsChartDataSource.UpdateDelay = 500
        Me.pivotGridControl2.Size = New System.Drawing.Size(1420, 255)
        Me.pivotGridControl2.TabIndex = 395
        Me.pivotGridControl2.Visible = False
        '
        'C2FNSeq
        '
        Me.C2FNSeq.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea
        Me.C2FNSeq.AreaIndex = 0
        Me.C2FNSeq.FieldName = "FNSeq"
        Me.C2FNSeq.Name = "C2FNSeq"
        Me.C2FNSeq.SortMode = DevExpress.XtraPivotGrid.PivotSortMode.Value
        '
        'C2FTData
        '
        Me.C2FTData.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea
        Me.C2FTData.AreaIndex = 0
        Me.C2FTData.FieldName = "FTData"
        Me.C2FTData.Name = "C2FTData"
        '
        'C2FNQuantity
        '
        Me.C2FNQuantity.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
        Me.C2FNQuantity.AreaIndex = 0
        Me.C2FNQuantity.CellFormat.FormatString = "{0:n0}"
        Me.C2FNQuantity.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.C2FNQuantity.FieldName = "FNQuantity"
        Me.C2FNQuantity.Name = "C2FNQuantity"
        '
        'C2FTState
        '
        Me.C2FTState.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea
        Me.C2FTState.AreaIndex = 1
        Me.C2FTState.Caption = "FTState"
        Me.C2FTState.FieldName = "FTState"
        Me.C2FTState.Name = "C2FTState"
        '
        'C2FTData2
        '
        Me.C2FTData2.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea
        Me.C2FTData2.AreaIndex = 2
        Me.C2FTData2.Caption = "FTData2"
        Me.C2FTData2.FieldName = "FTData2"
        Me.C2FTData2.Name = "C2FTData2"
        '
        'wWIPTemplateBU
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(1610, 928)
        Me.Controls.Add(Me.pivotGridControl)
        Me.Controls.Add(Me.pivotGridControl2)
        Me.Controls.Add(Me.chartControl2)
        Me.Controls.Add(Me.olbtitle)
        Me.Controls.Add(Me.ogbmainprocbutton)
        Me.Controls.Add(Me.chartControl)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "wWIPTemplateBU"
        Me.Text = "WIP Template BU"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.oDockManager, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pivotGridControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(XyDiagram2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(SideBySideBarSeriesLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chartControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbmainprocbutton.ResumeLayout(False)
        CType(XyDiagram1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(SideBySideBarSeriesLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chartControl2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pivotGridControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents oDockManager As DevExpress.XtraBars.Docking.DockManager
    Private WithEvents pivotGridControl As DevExpress.XtraPivotGrid.PivotGridControl
    Private WithEvents chartControl As DevExpress.XtraCharts.ChartControl
    Friend WithEvents C1FNSeq As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents CFTData As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents CFNQuantity As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents ogbmainprocbutton As DevExpress.XtraEditors.PanelControl
    Friend WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents otmtime As System.Windows.Forms.Timer
    Friend WithEvents olbtitle As DevExpress.XtraEditors.LabelControl
    Private WithEvents chartControl2 As DevExpress.XtraCharts.ChartControl
    Friend WithEvents Timer2 As System.Windows.Forms.Timer
    Friend WithEvents CFTState As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents CFTData2 As DevExpress.XtraPivotGrid.PivotGridField
    Private WithEvents pivotGridControl2 As DevExpress.XtraPivotGrid.PivotGridControl
    Friend WithEvents C2FNSeq As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents C2FTData As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents C2FNQuantity As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents C2FTState As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents C2FTData2 As DevExpress.XtraPivotGrid.PivotGridField
End Class

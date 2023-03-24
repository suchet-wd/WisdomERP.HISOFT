Imports DevExpress.XtraEditors.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wBIFGSale
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(wBIFGSale))
        Dim XyDiagram1 As DevExpress.XtraCharts.XYDiagram = New DevExpress.XtraCharts.XYDiagram()
        Dim LineSeriesView1 As DevExpress.XtraCharts.LineSeriesView = New DevExpress.XtraCharts.LineSeriesView()
        Dim XyDiagram2 As DevExpress.XtraCharts.XYDiagram = New DevExpress.XtraCharts.XYDiagram()
        Dim LineSeriesView2 As DevExpress.XtraCharts.LineSeriesView = New DevExpress.XtraCharts.LineSeriesView()
        Dim XyDiagram3 As DevExpress.XtraCharts.XYDiagram = New DevExpress.XtraCharts.XYDiagram()
        Dim LineSeriesView3 As DevExpress.XtraCharts.LineSeriesView = New DevExpress.XtraCharts.LineSeriesView()
        Me.ocmdoc = New DevExpress.XtraBars.Docking.DockManager()
        Me.ogbheader = New DevExpress.XtraBars.Docking.DockPanel()
        Me.DockPanel1_Container = New DevExpress.XtraBars.Docking.ControlContainer()
        Me.FTYearTo = New DevExpress.XtraEditors.DateEdit()
        Me.FTYearTo_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTYear = New DevExpress.XtraEditors.DateEdit()
        Me.FTYear_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.panelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.seUpdateDelay = New DevExpress.XtraEditors.SpinEdit()
        Me.lblUpdateDelay = New DevExpress.XtraEditors.LabelControl()
        Me.ceShowRowGrandTotals = New DevExpress.XtraEditors.CheckEdit()
        Me.ceShowColumnGrandTotals = New DevExpress.XtraEditors.CheckEdit()
        Me.ceSelectionOnly = New DevExpress.XtraEditors.CheckEdit()
        Me.ceChartDataVertical = New DevExpress.XtraEditors.CheckEdit()
        Me.checkShowPointLabels = New DevExpress.XtraEditors.CheckEdit()
        Me.comboChartType = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.pivotGridControlQty = New DevExpress.XtraPivotGrid.PivotGridControl()
        Me.CFNQuantity = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.CFFNNetAmt = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.CFTYear = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.CFTMonth = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.CFTStyleCode = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.CFTStyleName = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.chartControl = New DevExpress.XtraCharts.ChartControl()
        Me.ogbmainprocbutton = New DevExpress.XtraEditors.PanelControl()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmload = New DevExpress.XtraEditors.SimpleButton()
        Me.otb = New DevExpress.XtraTab.XtraTabControl()
        Me.otpchart = New DevExpress.XtraTab.XtraTabPage()
        Me.otpdata = New DevExpress.XtraTab.XtraTabPage()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.chartControlQty = New DevExpress.XtraCharts.ChartControl()
        Me.PanelControl2 = New DevExpress.XtraEditors.PanelControl()
        Me.seUpdateDelayQty = New DevExpress.XtraEditors.SpinEdit()
        Me.seUpdateDelayQty_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.ceShowRowGrandTotalsQty = New DevExpress.XtraEditors.CheckEdit()
        Me.ceShowColumnGrandTotalsQty = New DevExpress.XtraEditors.CheckEdit()
        Me.ceSelectionOnlyQty = New DevExpress.XtraEditors.CheckEdit()
        Me.ceChartDataVerticalQty = New DevExpress.XtraEditors.CheckEdit()
        Me.checkShowPointLabelsQty = New DevExpress.XtraEditors.CheckEdit()
        Me.comboChartTypeQty = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.comboChartTypeQty_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.otpAmount = New DevExpress.XtraTab.XtraTabPage()
        Me.GroupControl2 = New DevExpress.XtraEditors.GroupControl()
        Me.chartControlAmt = New DevExpress.XtraCharts.ChartControl()
        Me.PanelControl3 = New DevExpress.XtraEditors.PanelControl()
        Me.seUpdateDelayAmt = New DevExpress.XtraEditors.SpinEdit()
        Me.seUpdateDelayAmt_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.ceShowRowGrandTotalsAmt = New DevExpress.XtraEditors.CheckEdit()
        Me.ceShowColumnGrandTotalsAmt = New DevExpress.XtraEditors.CheckEdit()
        Me.ceSelectionOnlyAmt = New DevExpress.XtraEditors.CheckEdit()
        Me.ceChartDataVerticalAmt = New DevExpress.XtraEditors.CheckEdit()
        Me.checkShowPointLabelsAmt = New DevExpress.XtraEditors.CheckEdit()
        Me.comboChartTypeAmt = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.comboChartTypeAmt_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.PivotGridControlAmt = New DevExpress.XtraPivotGrid.PivotGridControl()
        Me.PivotGridField1 = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.PivotGridField2 = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.PivotGridField3 = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.PivotGridField4 = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.PivotGridField5 = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.PivotGridField6 = New DevExpress.XtraPivotGrid.PivotGridField()
        CType(Me.ocmdoc, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbheader.SuspendLayout()
        Me.DockPanel1_Container.SuspendLayout()
        CType(Me.FTYearTo.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTYearTo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTYear.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTYear.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.panelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.panelControl1.SuspendLayout()
        CType(Me.seUpdateDelay.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ceShowRowGrandTotals.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ceShowColumnGrandTotals.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ceSelectionOnly.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ceChartDataVertical.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.checkShowPointLabels.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.comboChartType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pivotGridControlQty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chartControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(XyDiagram1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(LineSeriesView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbmainprocbutton.SuspendLayout()
        CType(Me.otb, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.otb.SuspendLayout()
        Me.otpchart.SuspendLayout()
        Me.otpdata.SuspendLayout()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.chartControlQty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(XyDiagram2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(LineSeriesView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl2.SuspendLayout()
        CType(Me.seUpdateDelayQty.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ceShowRowGrandTotalsQty.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ceShowColumnGrandTotalsQty.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ceSelectionOnlyQty.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ceChartDataVerticalQty.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.checkShowPointLabelsQty.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.comboChartTypeQty.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.otpAmount.SuspendLayout()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl2.SuspendLayout()
        CType(Me.chartControlAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(XyDiagram3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(LineSeriesView3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl3.SuspendLayout()
        CType(Me.seUpdateDelayAmt.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ceShowRowGrandTotalsAmt.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ceShowColumnGrandTotalsAmt.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ceSelectionOnlyAmt.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ceChartDataVerticalAmt.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.checkShowPointLabelsAmt.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.comboChartTypeAmt.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PivotGridControlAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ocmdoc
        '
        Me.ocmdoc.Form = Me
        Me.ocmdoc.RootPanels.AddRange(New DevExpress.XtraBars.Docking.DockPanel() {Me.ogbheader})
        Me.ocmdoc.TopZIndexControls.AddRange(New String() {"DevExpress.XtraBars.BarDockControl", "DevExpress.XtraBars.StandaloneBarDockControl", "System.Windows.Forms.StatusBar", "System.Windows.Forms.MenuStrip", "System.Windows.Forms.StatusStrip", "DevExpress.XtraBars.Ribbon.RibbonStatusBar", "DevExpress.XtraBars.Ribbon.RibbonControl"})
        '
        'ogbheader
        '
        Me.ogbheader.Controls.Add(Me.DockPanel1_Container)
        Me.ogbheader.Dock = DevExpress.XtraBars.Docking.DockingStyle.Top
        Me.ogbheader.ID = New System.Guid("6a838d1f-4f6f-4734-9f6d-4c809fcfd587")
        Me.ogbheader.Image = CType(resources.GetObject("ogbheader.Image"), System.Drawing.Image)
        Me.ogbheader.Location = New System.Drawing.Point(0, 0)
        Me.ogbheader.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbheader.Name = "ogbheader"
        Me.ogbheader.Options.AllowDockBottom = False
        Me.ogbheader.Options.AllowDockFill = False
        Me.ogbheader.Options.AllowDockLeft = False
        Me.ogbheader.Options.AllowDockRight = False
        Me.ogbheader.Options.AllowFloating = False
        Me.ogbheader.Options.FloatOnDblClick = False
        Me.ogbheader.Options.ShowCloseButton = False
        Me.ogbheader.Options.ShowMaximizeButton = False
        Me.ogbheader.OriginalSize = New System.Drawing.Size(200, 73)
        Me.ogbheader.Size = New System.Drawing.Size(1014, 73)
        Me.ogbheader.Text = "Criteria"
        '
        'DockPanel1_Container
        '
        Me.DockPanel1_Container.Controls.Add(Me.FTYearTo)
        Me.DockPanel1_Container.Controls.Add(Me.FTYearTo_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FTYear)
        Me.DockPanel1_Container.Controls.Add(Me.FTYear_lbl)
        Me.DockPanel1_Container.Location = New System.Drawing.Point(5, 27)
        Me.DockPanel1_Container.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.DockPanel1_Container.Name = "DockPanel1_Container"
        Me.DockPanel1_Container.Size = New System.Drawing.Size(1004, 39)
        Me.DockPanel1_Container.TabIndex = 0
        '
        'FTYearTo
        '
        Me.FTYearTo.EditValue = Nothing
        Me.FTYearTo.EnterMoveNextControl = True
        Me.FTYearTo.Location = New System.Drawing.Point(576, 6)
        Me.FTYearTo.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTYearTo.Name = "FTYearTo"
        Me.FTYearTo.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTYearTo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FTYearTo.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.FTYearTo.Properties.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.TouchUI
        Me.FTYearTo.Properties.DisplayFormat.FormatString = "MM/yyyy"
        Me.FTYearTo.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTYearTo.Properties.EditFormat.FormatString = "MM/yyyy"
        Me.FTYearTo.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTYearTo.Properties.Mask.EditMask = "MM/yyyy"
        Me.FTYearTo.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.FTYearTo.Properties.NullDate = ""
        Me.FTYearTo.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTYearTo.Size = New System.Drawing.Size(131, 22)
        Me.FTYearTo.TabIndex = 478
        Me.FTYearTo.Tag = "2|"
        '
        'FTYearTo_lbl
        '
        Me.FTYearTo_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTYearTo_lbl.Appearance.Options.UseForeColor = True
        Me.FTYearTo_lbl.Appearance.Options.UseTextOptions = True
        Me.FTYearTo_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTYearTo_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTYearTo_lbl.Location = New System.Drawing.Point(451, 5)
        Me.FTYearTo_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTYearTo_lbl.Name = "FTYearTo_lbl"
        Me.FTYearTo_lbl.Size = New System.Drawing.Size(122, 23)
        Me.FTYearTo_lbl.TabIndex = 480
        Me.FTYearTo_lbl.Tag = "2|"
        Me.FTYearTo_lbl.Text = "To :"
        '
        'FTYear
        '
        Me.FTYear.EditValue = Nothing
        Me.FTYear.EnterMoveNextControl = True
        Me.FTYear.Location = New System.Drawing.Point(154, 6)
        Me.FTYear.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTYear.Name = "FTYear"
        Me.FTYear.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTYear.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FTYear.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.FTYear.Properties.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.TouchUI
        Me.FTYear.Properties.DisplayFormat.FormatString = "MM/yyyy"
        Me.FTYear.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTYear.Properties.EditFormat.FormatString = "MM/yyyy"
        Me.FTYear.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTYear.Properties.Mask.EditMask = "MM/yyyy"
        Me.FTYear.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.FTYear.Properties.NullDate = ""
        Me.FTYear.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTYear.Size = New System.Drawing.Size(131, 22)
        Me.FTYear.TabIndex = 477
        Me.FTYear.Tag = "2|"
        '
        'FTYear_lbl
        '
        Me.FTYear_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTYear_lbl.Appearance.Options.UseForeColor = True
        Me.FTYear_lbl.Appearance.Options.UseTextOptions = True
        Me.FTYear_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTYear_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTYear_lbl.Location = New System.Drawing.Point(28, 5)
        Me.FTYear_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTYear_lbl.Name = "FTYear_lbl"
        Me.FTYear_lbl.Size = New System.Drawing.Size(122, 23)
        Me.FTYear_lbl.TabIndex = 479
        Me.FTYear_lbl.Tag = "2|"
        Me.FTYear_lbl.Text = "Start :"
        '
        'panelControl1
        '
        Me.panelControl1.Controls.Add(Me.seUpdateDelay)
        Me.panelControl1.Controls.Add(Me.lblUpdateDelay)
        Me.panelControl1.Controls.Add(Me.ceShowRowGrandTotals)
        Me.panelControl1.Controls.Add(Me.ceShowColumnGrandTotals)
        Me.panelControl1.Controls.Add(Me.ceSelectionOnly)
        Me.panelControl1.Controls.Add(Me.ceChartDataVertical)
        Me.panelControl1.Controls.Add(Me.checkShowPointLabels)
        Me.panelControl1.Controls.Add(Me.comboChartType)
        Me.panelControl1.Controls.Add(Me.LabelControl1)
        Me.panelControl1.Dock = System.Windows.Forms.DockStyle.Top
        Me.panelControl1.Location = New System.Drawing.Point(0, 0)
        Me.panelControl1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.panelControl1.Name = "panelControl1"
        Me.panelControl1.Padding = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.panelControl1.Size = New System.Drawing.Size(1007, 70)
        Me.panelControl1.TabIndex = 3
        '
        'seUpdateDelay
        '
        Me.seUpdateDelay.EditValue = New Decimal(New Integer() {500, 0, 0, 0})
        Me.seUpdateDelay.Location = New System.Drawing.Point(293, 39)
        Me.seUpdateDelay.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.seUpdateDelay.Name = "seUpdateDelay"
        Me.seUpdateDelay.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.seUpdateDelay.Properties.Increment = New Decimal(New Integer() {100, 0, 0, 0})
        Me.seUpdateDelay.Properties.IsFloatValue = False
        Me.seUpdateDelay.Properties.Mask.EditMask = "N00"
        Me.seUpdateDelay.Properties.MaxValue = New Decimal(New Integer() {5000, 0, 0, 0})
        Me.seUpdateDelay.Size = New System.Drawing.Size(56, 22)
        Me.seUpdateDelay.TabIndex = 10
        Me.seUpdateDelay.ToolTip = "Sets the Chart update delay when PivotGrid selection changes."
        '
        'lblUpdateDelay
        '
        Me.lblUpdateDelay.Location = New System.Drawing.Point(203, 43)
        Me.lblUpdateDelay.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.lblUpdateDelay.Name = "lblUpdateDelay"
        Me.lblUpdateDelay.Size = New System.Drawing.Size(80, 16)
        Me.lblUpdateDelay.TabIndex = 13
        Me.lblUpdateDelay.Text = "Update Delay:"
        '
        'ceShowRowGrandTotals
        '
        Me.ceShowRowGrandTotals.EditValue = True
        Me.ceShowRowGrandTotals.Location = New System.Drawing.Point(559, 12)
        Me.ceShowRowGrandTotals.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ceShowRowGrandTotals.Name = "ceShowRowGrandTotals"
        Me.ceShowRowGrandTotals.Properties.AutoWidth = True
        Me.ceShowRowGrandTotals.Properties.Caption = "Show Row Grand Totals"
        Me.ceShowRowGrandTotals.Size = New System.Drawing.Size(160, 20)
        Me.ceShowRowGrandTotals.TabIndex = 7
        Me.ceShowRowGrandTotals.ToolTip = "Toggles whether row grand total values are shown in the Chart control"
        '
        'ceShowColumnGrandTotals
        '
        Me.ceShowColumnGrandTotals.Location = New System.Drawing.Point(559, 39)
        Me.ceShowColumnGrandTotals.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ceShowColumnGrandTotals.Name = "ceShowColumnGrandTotals"
        Me.ceShowColumnGrandTotals.Properties.AutoWidth = True
        Me.ceShowColumnGrandTotals.Properties.Caption = "Show Column Grand Totals"
        Me.ceShowColumnGrandTotals.Size = New System.Drawing.Size(178, 20)
        Me.ceShowColumnGrandTotals.TabIndex = 13
        Me.ceShowColumnGrandTotals.ToolTip = "Toggles whether column grand total values are shown in the Chart control"
        '
        'ceSelectionOnly
        '
        Me.ceSelectionOnly.Location = New System.Drawing.Point(12, 39)
        Me.ceSelectionOnly.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ceSelectionOnly.Name = "ceSelectionOnly"
        Me.ceSelectionOnly.Properties.AutoWidth = True
        Me.ceSelectionOnly.Properties.Caption = "Selection Only"
        Me.ceSelectionOnly.Size = New System.Drawing.Size(103, 20)
        Me.ceSelectionOnly.TabIndex = 9
        Me.ceSelectionOnly.ToolTip = "Toggles whether all PivotGrid cells or selected cells only should be represented " & _
    "in the Chart"
        '
        'ceChartDataVertical
        '
        Me.ceChartDataVertical.Location = New System.Drawing.Point(356, 39)
        Me.ceChartDataVertical.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ceChartDataVertical.Name = "ceChartDataVertical"
        Me.ceChartDataVertical.Properties.AutoWidth = True
        Me.ceChartDataVertical.Properties.Caption = "Generate Series from Columns"
        Me.ceChartDataVertical.Size = New System.Drawing.Size(198, 20)
        Me.ceChartDataVertical.TabIndex = 12
        Me.ceChartDataVertical.ToolTip = "Toggles whether series in a chart control are created based on PivotGrid columns " & _
    "or rows"
        '
        'checkShowPointLabels
        '
        Me.checkShowPointLabels.Location = New System.Drawing.Point(356, 12)
        Me.checkShowPointLabels.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.checkShowPointLabels.Name = "checkShowPointLabels"
        Me.checkShowPointLabels.Properties.AutoWidth = True
        Me.checkShowPointLabels.Properties.Caption = "Show Point Labels"
        Me.checkShowPointLabels.Size = New System.Drawing.Size(126, 20)
        Me.checkShowPointLabels.TabIndex = 4
        Me.checkShowPointLabels.ToolTip = "Toggles whether value labels are shown in the Chart control"
        '
        'comboChartType
        '
        Me.comboChartType.EditValue = "Line"
        Me.comboChartType.Location = New System.Drawing.Point(169, 12)
        Me.comboChartType.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.comboChartType.Name = "comboChartType"
        Me.comboChartType.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.comboChartType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.comboChartType.Size = New System.Drawing.Size(180, 22)
        Me.comboChartType.TabIndex = 3
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Options.UseTextOptions = True
        Me.LabelControl1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LabelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LabelControl1.Location = New System.Drawing.Point(16, 15)
        Me.LabelControl1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(146, 21)
        Me.LabelControl1.TabIndex = 2
        Me.LabelControl1.Text = "Chart Type:"
        '
        'pivotGridControlQty
        '
        Me.pivotGridControlQty.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.pivotGridControlQty.Cursor = System.Windows.Forms.Cursors.Default
        Me.pivotGridControlQty.Dock = System.Windows.Forms.DockStyle.Top
        Me.pivotGridControlQty.Fields.AddRange(New DevExpress.XtraPivotGrid.PivotGridField() {Me.CFNQuantity, Me.CFFNNetAmt, Me.CFTYear, Me.CFTMonth, Me.CFTStyleCode, Me.CFTStyleName})
        Me.pivotGridControlQty.Location = New System.Drawing.Point(0, 0)
        Me.pivotGridControlQty.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.pivotGridControlQty.Name = "pivotGridControlQty"
        Me.pivotGridControlQty.OptionsChartDataSource.DataProvideMode = DevExpress.XtraPivotGrid.PivotChartDataProvideMode.UseCustomSettings
        Me.pivotGridControlQty.OptionsChartDataSource.ProvideDataByColumns = False
        Me.pivotGridControlQty.OptionsChartDataSource.UpdateDelay = 500
        Me.pivotGridControlQty.Size = New System.Drawing.Size(1007, 274)
        Me.pivotGridControlQty.TabIndex = 2
        '
        'CFNQuantity
        '
        Me.CFNQuantity.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
        Me.CFNQuantity.AreaIndex = 0
        Me.CFNQuantity.Caption = "FNQuantity"
        Me.CFNQuantity.CellFormat.FormatString = "{0:n0}"
        Me.CFNQuantity.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CFNQuantity.FieldName = "FNQuantity"
        Me.CFNQuantity.Name = "CFNQuantity"
        Me.CFNQuantity.Width = 86
        '
        'CFFNNetAmt
        '
        Me.CFFNNetAmt.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
        Me.CFFNNetAmt.AreaIndex = 1
        Me.CFFNNetAmt.Caption = "FNNetAmt"
        Me.CFFNNetAmt.CellFormat.FormatString = "{0:n2}"
        Me.CFFNNetAmt.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CFFNNetAmt.FieldName = "FNNetAmt"
        Me.CFFNNetAmt.Name = "CFFNNetAmt"
        Me.CFFNNetAmt.Visible = False
        Me.CFFNNetAmt.Width = 78
        '
        'CFTYear
        '
        Me.CFTYear.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea
        Me.CFTYear.AreaIndex = 0
        Me.CFTYear.Caption = "FTYear"
        Me.CFTYear.FieldName = "FTYear"
        Me.CFTYear.Name = "CFTYear"
        Me.CFTYear.UnboundFieldName = "CFTPayYear"
        '
        'CFTMonth
        '
        Me.CFTMonth.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea
        Me.CFTMonth.AreaIndex = 1
        Me.CFTMonth.Caption = "FTMonth"
        Me.CFTMonth.FieldName = "FTMonth"
        Me.CFTMonth.Name = "CFTMonth"
        Me.CFTMonth.UnboundFieldName = "CFNMonth"
        '
        'CFTStyleCode
        '
        Me.CFTStyleCode.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea
        Me.CFTStyleCode.AreaIndex = 0
        Me.CFTStyleCode.Caption = "FTStyleCode"
        Me.CFTStyleCode.FieldName = "FTStyleCode"
        Me.CFTStyleCode.Name = "CFTStyleCode"
        Me.CFTStyleCode.SortMode = DevExpress.XtraPivotGrid.PivotSortMode.None
        '
        'CFTStyleName
        '
        Me.CFTStyleName.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea
        Me.CFTStyleName.AreaIndex = 1
        Me.CFTStyleName.Caption = "FTStyleName"
        Me.CFTStyleName.FieldName = "FTStyleName"
        Me.CFTStyleName.Name = "CFTStyleName"
        '
        'chartControl
        '
        Me.chartControl.DataBindings = Nothing
        XyDiagram1.AxisX.Label.Staggered = True
        XyDiagram1.AxisX.VisibleInPanesSerializable = "-1"
        XyDiagram1.AxisY.VisibleInPanesSerializable = "-1"
        Me.chartControl.Diagram = XyDiagram1
        Me.chartControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.chartControl.Legend.MaxHorizontalPercentage = 30.0R
        Me.chartControl.Legend.Name = "Default Legend"
        Me.chartControl.Location = New System.Drawing.Point(0, 70)
        Me.chartControl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.chartControl.Name = "chartControl"
        Me.chartControl.SeriesDataMember = "Series"
        Me.chartControl.SeriesSerializable = New DevExpress.XtraCharts.Series(-1) {}
        Me.chartControl.SeriesTemplate.ArgumentDataMember = "Arguments"
        Me.chartControl.SeriesTemplate.CrosshairLabelPattern = "{S} : {V:c}"
        Me.chartControl.SeriesTemplate.LabelsVisibility = DevExpress.Utils.DefaultBoolean.[False]
        Me.chartControl.SeriesTemplate.ValueDataMembersSerializable = "Values"
        Me.chartControl.SeriesTemplate.View = LineSeriesView1
        Me.chartControl.Size = New System.Drawing.Size(1007, 526)
        Me.chartControl.TabIndex = 3
        '
        'ogbmainprocbutton
        '
        Me.ogbmainprocbutton.Controls.Add(Me.ocmexit)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmload)
        Me.ogbmainprocbutton.Location = New System.Drawing.Point(84, 4)
        Me.ogbmainprocbutton.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbmainprocbutton.Name = "ogbmainprocbutton"
        Me.ogbmainprocbutton.Size = New System.Drawing.Size(736, 42)
        Me.ogbmainprocbutton.TabIndex = 391
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
        'ocmload
        '
        Me.ocmload.Location = New System.Drawing.Point(20, 6)
        Me.ocmload.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmload.Name = "ocmload"
        Me.ocmload.Size = New System.Drawing.Size(111, 31)
        Me.ocmload.TabIndex = 93
        Me.ocmload.TabStop = False
        Me.ocmload.Tag = "2|"
        Me.ocmload.Text = "LoadData"
        '
        'otb
        '
        Me.otb.Dock = System.Windows.Forms.DockStyle.Fill
        Me.otb.Location = New System.Drawing.Point(0, 73)
        Me.otb.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.otb.Name = "otb"
        Me.otb.SelectedTabPage = Me.otpchart
        Me.otb.Size = New System.Drawing.Size(1014, 630)
        Me.otb.TabIndex = 394
        Me.otb.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.otpdata, Me.otpchart, Me.otpAmount})
        '
        'otpchart
        '
        Me.otpchart.Controls.Add(Me.chartControl)
        Me.otpchart.Controls.Add(Me.panelControl1)
        Me.otpchart.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.otpchart.Name = "otpchart"
        Me.otpchart.PageVisible = False
        Me.otpchart.Size = New System.Drawing.Size(1007, 596)
        Me.otpchart.Text = "Chart"
        '
        'otpdata
        '
        Me.otpdata.Controls.Add(Me.GroupControl1)
        Me.otpdata.Controls.Add(Me.pivotGridControlQty)
        Me.otpdata.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.otpdata.Name = "otpdata"
        Me.otpdata.Size = New System.Drawing.Size(1007, 596)
        Me.otpdata.Text = "Quantity"
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Me.chartControlQty)
        Me.GroupControl1.Controls.Add(Me.PanelControl2)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl1.Location = New System.Drawing.Point(0, 274)
        Me.GroupControl1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(1007, 322)
        Me.GroupControl1.TabIndex = 3
        Me.GroupControl1.Text = "Chart"
        '
        'chartControlQty
        '
        Me.chartControlQty.DataBindings = Nothing
        XyDiagram2.AxisX.Label.Staggered = True
        XyDiagram2.AxisX.VisibleInPanesSerializable = "-1"
        XyDiagram2.AxisY.VisibleInPanesSerializable = "-1"
        Me.chartControlQty.Diagram = XyDiagram2
        Me.chartControlQty.Dock = System.Windows.Forms.DockStyle.Fill
        Me.chartControlQty.Legend.MaxHorizontalPercentage = 30.0R
        Me.chartControlQty.Legend.Name = "Default Legend"
        Me.chartControlQty.Location = New System.Drawing.Point(2, 95)
        Me.chartControlQty.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.chartControlQty.Name = "chartControlQty"
        Me.chartControlQty.SeriesDataMember = "Series"
        Me.chartControlQty.SeriesSerializable = New DevExpress.XtraCharts.Series(-1) {}
        Me.chartControlQty.SeriesTemplate.ArgumentDataMember = "Arguments"
        Me.chartControlQty.SeriesTemplate.CrosshairLabelPattern = "{S} : {V:c}"
        Me.chartControlQty.SeriesTemplate.LabelsVisibility = DevExpress.Utils.DefaultBoolean.[False]
        Me.chartControlQty.SeriesTemplate.ValueDataMembersSerializable = "Values"
        Me.chartControlQty.SeriesTemplate.View = LineSeriesView2
        Me.chartControlQty.Size = New System.Drawing.Size(1003, 225)
        Me.chartControlQty.TabIndex = 5
        '
        'PanelControl2
        '
        Me.PanelControl2.Controls.Add(Me.seUpdateDelayQty)
        Me.PanelControl2.Controls.Add(Me.seUpdateDelayQty_lbl)
        Me.PanelControl2.Controls.Add(Me.ceShowRowGrandTotalsQty)
        Me.PanelControl2.Controls.Add(Me.ceShowColumnGrandTotalsQty)
        Me.PanelControl2.Controls.Add(Me.ceSelectionOnlyQty)
        Me.PanelControl2.Controls.Add(Me.ceChartDataVerticalQty)
        Me.PanelControl2.Controls.Add(Me.checkShowPointLabelsQty)
        Me.PanelControl2.Controls.Add(Me.comboChartTypeQty)
        Me.PanelControl2.Controls.Add(Me.comboChartTypeQty_lbl)
        Me.PanelControl2.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelControl2.Location = New System.Drawing.Point(2, 25)
        Me.PanelControl2.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.PanelControl2.Name = "PanelControl2"
        Me.PanelControl2.Padding = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.PanelControl2.Size = New System.Drawing.Size(1003, 70)
        Me.PanelControl2.TabIndex = 4
        '
        'seUpdateDelayQty
        '
        Me.seUpdateDelayQty.EditValue = New Decimal(New Integer() {500, 0, 0, 0})
        Me.seUpdateDelayQty.Location = New System.Drawing.Point(293, 39)
        Me.seUpdateDelayQty.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.seUpdateDelayQty.Name = "seUpdateDelayQty"
        Me.seUpdateDelayQty.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.seUpdateDelayQty.Properties.Increment = New Decimal(New Integer() {100, 0, 0, 0})
        Me.seUpdateDelayQty.Properties.IsFloatValue = False
        Me.seUpdateDelayQty.Properties.Mask.EditMask = "N00"
        Me.seUpdateDelayQty.Properties.MaxValue = New Decimal(New Integer() {5000, 0, 0, 0})
        Me.seUpdateDelayQty.Size = New System.Drawing.Size(56, 22)
        Me.seUpdateDelayQty.TabIndex = 10
        Me.seUpdateDelayQty.ToolTip = "Sets the Chart update delay when PivotGrid selection changes."
        '
        'seUpdateDelayQty_lbl
        '
        Me.seUpdateDelayQty_lbl.Location = New System.Drawing.Point(203, 43)
        Me.seUpdateDelayQty_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.seUpdateDelayQty_lbl.Name = "seUpdateDelayQty_lbl"
        Me.seUpdateDelayQty_lbl.Size = New System.Drawing.Size(80, 16)
        Me.seUpdateDelayQty_lbl.TabIndex = 13
        Me.seUpdateDelayQty_lbl.Text = "Update Delay:"
        '
        'ceShowRowGrandTotalsQty
        '
        Me.ceShowRowGrandTotalsQty.Location = New System.Drawing.Point(559, 12)
        Me.ceShowRowGrandTotalsQty.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ceShowRowGrandTotalsQty.Name = "ceShowRowGrandTotalsQty"
        Me.ceShowRowGrandTotalsQty.Properties.AutoWidth = True
        Me.ceShowRowGrandTotalsQty.Properties.Caption = "Show Row Grand Totals"
        Me.ceShowRowGrandTotalsQty.Size = New System.Drawing.Size(160, 20)
        Me.ceShowRowGrandTotalsQty.TabIndex = 7
        Me.ceShowRowGrandTotalsQty.ToolTip = "Toggles whether row grand total values are shown in the Chart control"
        '
        'ceShowColumnGrandTotalsQty
        '
        Me.ceShowColumnGrandTotalsQty.Location = New System.Drawing.Point(559, 39)
        Me.ceShowColumnGrandTotalsQty.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ceShowColumnGrandTotalsQty.Name = "ceShowColumnGrandTotalsQty"
        Me.ceShowColumnGrandTotalsQty.Properties.AutoWidth = True
        Me.ceShowColumnGrandTotalsQty.Properties.Caption = "Show Column Grand Totals"
        Me.ceShowColumnGrandTotalsQty.Size = New System.Drawing.Size(178, 20)
        Me.ceShowColumnGrandTotalsQty.TabIndex = 13
        Me.ceShowColumnGrandTotalsQty.ToolTip = "Toggles whether column grand total values are shown in the Chart control"
        '
        'ceSelectionOnlyQty
        '
        Me.ceSelectionOnlyQty.Location = New System.Drawing.Point(12, 39)
        Me.ceSelectionOnlyQty.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ceSelectionOnlyQty.Name = "ceSelectionOnlyQty"
        Me.ceSelectionOnlyQty.Properties.AutoWidth = True
        Me.ceSelectionOnlyQty.Properties.Caption = "Selection Only"
        Me.ceSelectionOnlyQty.Size = New System.Drawing.Size(103, 20)
        Me.ceSelectionOnlyQty.TabIndex = 9
        Me.ceSelectionOnlyQty.ToolTip = "Toggles whether all PivotGrid cells or selected cells only should be represented " & _
    "in the Chart"
        '
        'ceChartDataVerticalQty
        '
        Me.ceChartDataVerticalQty.Location = New System.Drawing.Point(356, 39)
        Me.ceChartDataVerticalQty.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ceChartDataVerticalQty.Name = "ceChartDataVerticalQty"
        Me.ceChartDataVerticalQty.Properties.AutoWidth = True
        Me.ceChartDataVerticalQty.Properties.Caption = "Generate Series from Columns"
        Me.ceChartDataVerticalQty.Size = New System.Drawing.Size(198, 20)
        Me.ceChartDataVerticalQty.TabIndex = 12
        Me.ceChartDataVerticalQty.ToolTip = "Toggles whether series in a chart control are created based on PivotGrid columns " & _
    "or rows"
        '
        'checkShowPointLabelsQty
        '
        Me.checkShowPointLabelsQty.Location = New System.Drawing.Point(356, 12)
        Me.checkShowPointLabelsQty.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.checkShowPointLabelsQty.Name = "checkShowPointLabelsQty"
        Me.checkShowPointLabelsQty.Properties.AutoWidth = True
        Me.checkShowPointLabelsQty.Properties.Caption = "Show Point Labels"
        Me.checkShowPointLabelsQty.Size = New System.Drawing.Size(126, 20)
        Me.checkShowPointLabelsQty.TabIndex = 4
        Me.checkShowPointLabelsQty.ToolTip = "Toggles whether value labels are shown in the Chart control"
        '
        'comboChartTypeQty
        '
        Me.comboChartTypeQty.EditValue = "Line"
        Me.comboChartTypeQty.Location = New System.Drawing.Point(169, 12)
        Me.comboChartTypeQty.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.comboChartTypeQty.Name = "comboChartTypeQty"
        Me.comboChartTypeQty.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.comboChartTypeQty.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.comboChartTypeQty.Size = New System.Drawing.Size(180, 22)
        Me.comboChartTypeQty.TabIndex = 3
        '
        'comboChartTypeQty_lbl
        '
        Me.comboChartTypeQty_lbl.Appearance.Options.UseTextOptions = True
        Me.comboChartTypeQty_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.comboChartTypeQty_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.comboChartTypeQty_lbl.Location = New System.Drawing.Point(16, 15)
        Me.comboChartTypeQty_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.comboChartTypeQty_lbl.Name = "comboChartTypeQty_lbl"
        Me.comboChartTypeQty_lbl.Size = New System.Drawing.Size(146, 21)
        Me.comboChartTypeQty_lbl.TabIndex = 2
        Me.comboChartTypeQty_lbl.Text = "Chart Type:"
        '
        'otpAmount
        '
        Me.otpAmount.Controls.Add(Me.GroupControl2)
        Me.otpAmount.Controls.Add(Me.PivotGridControlAmt)
        Me.otpAmount.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.otpAmount.Name = "otpAmount"
        Me.otpAmount.Size = New System.Drawing.Size(1007, 596)
        Me.otpAmount.Text = "Amount"
        '
        'GroupControl2
        '
        Me.GroupControl2.Controls.Add(Me.chartControlAmt)
        Me.GroupControl2.Controls.Add(Me.PanelControl3)
        Me.GroupControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl2.Location = New System.Drawing.Point(0, 274)
        Me.GroupControl2.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupControl2.Name = "GroupControl2"
        Me.GroupControl2.Size = New System.Drawing.Size(1007, 322)
        Me.GroupControl2.TabIndex = 5
        Me.GroupControl2.Text = "Chart"
        '
        'chartControlAmt
        '
        Me.chartControlAmt.DataBindings = Nothing
        XyDiagram3.AxisX.Label.Staggered = True
        XyDiagram3.AxisX.VisibleInPanesSerializable = "-1"
        XyDiagram3.AxisY.VisibleInPanesSerializable = "-1"
        Me.chartControlAmt.Diagram = XyDiagram3
        Me.chartControlAmt.Dock = System.Windows.Forms.DockStyle.Fill
        Me.chartControlAmt.Legend.MaxHorizontalPercentage = 30.0R
        Me.chartControlAmt.Legend.Name = "Default Legend"
        Me.chartControlAmt.Location = New System.Drawing.Point(2, 95)
        Me.chartControlAmt.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.chartControlAmt.Name = "chartControlAmt"
        Me.chartControlAmt.SeriesDataMember = "Series"
        Me.chartControlAmt.SeriesSerializable = New DevExpress.XtraCharts.Series(-1) {}
        Me.chartControlAmt.SeriesTemplate.ArgumentDataMember = "Arguments"
        Me.chartControlAmt.SeriesTemplate.CrosshairLabelPattern = "{S} : {V:c}"
        Me.chartControlAmt.SeriesTemplate.LabelsVisibility = DevExpress.Utils.DefaultBoolean.[False]
        Me.chartControlAmt.SeriesTemplate.ValueDataMembersSerializable = "Values"
        Me.chartControlAmt.SeriesTemplate.View = LineSeriesView3
        Me.chartControlAmt.Size = New System.Drawing.Size(1003, 225)
        Me.chartControlAmt.TabIndex = 5
        '
        'PanelControl3
        '
        Me.PanelControl3.Controls.Add(Me.seUpdateDelayAmt)
        Me.PanelControl3.Controls.Add(Me.seUpdateDelayAmt_lbl)
        Me.PanelControl3.Controls.Add(Me.ceShowRowGrandTotalsAmt)
        Me.PanelControl3.Controls.Add(Me.ceShowColumnGrandTotalsAmt)
        Me.PanelControl3.Controls.Add(Me.ceSelectionOnlyAmt)
        Me.PanelControl3.Controls.Add(Me.ceChartDataVerticalAmt)
        Me.PanelControl3.Controls.Add(Me.checkShowPointLabelsAmt)
        Me.PanelControl3.Controls.Add(Me.comboChartTypeAmt)
        Me.PanelControl3.Controls.Add(Me.comboChartTypeAmt_lbl)
        Me.PanelControl3.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelControl3.Location = New System.Drawing.Point(2, 25)
        Me.PanelControl3.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.PanelControl3.Name = "PanelControl3"
        Me.PanelControl3.Padding = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.PanelControl3.Size = New System.Drawing.Size(1003, 70)
        Me.PanelControl3.TabIndex = 4
        '
        'seUpdateDelayAmt
        '
        Me.seUpdateDelayAmt.EditValue = New Decimal(New Integer() {500, 0, 0, 0})
        Me.seUpdateDelayAmt.Location = New System.Drawing.Point(293, 39)
        Me.seUpdateDelayAmt.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.seUpdateDelayAmt.Name = "seUpdateDelayAmt"
        Me.seUpdateDelayAmt.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.seUpdateDelayAmt.Properties.Increment = New Decimal(New Integer() {100, 0, 0, 0})
        Me.seUpdateDelayAmt.Properties.IsFloatValue = False
        Me.seUpdateDelayAmt.Properties.Mask.EditMask = "N00"
        Me.seUpdateDelayAmt.Properties.MaxValue = New Decimal(New Integer() {5000, 0, 0, 0})
        Me.seUpdateDelayAmt.Size = New System.Drawing.Size(56, 22)
        Me.seUpdateDelayAmt.TabIndex = 10
        Me.seUpdateDelayAmt.ToolTip = "Sets the Chart update delay when PivotGrid selection changes."
        '
        'seUpdateDelayAmt_lbl
        '
        Me.seUpdateDelayAmt_lbl.Location = New System.Drawing.Point(203, 43)
        Me.seUpdateDelayAmt_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.seUpdateDelayAmt_lbl.Name = "seUpdateDelayAmt_lbl"
        Me.seUpdateDelayAmt_lbl.Size = New System.Drawing.Size(80, 16)
        Me.seUpdateDelayAmt_lbl.TabIndex = 13
        Me.seUpdateDelayAmt_lbl.Text = "Update Delay:"
        '
        'ceShowRowGrandTotalsAmt
        '
        Me.ceShowRowGrandTotalsAmt.Location = New System.Drawing.Point(559, 12)
        Me.ceShowRowGrandTotalsAmt.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ceShowRowGrandTotalsAmt.Name = "ceShowRowGrandTotalsAmt"
        Me.ceShowRowGrandTotalsAmt.Properties.AutoWidth = True
        Me.ceShowRowGrandTotalsAmt.Properties.Caption = "Show Row Grand Totals"
        Me.ceShowRowGrandTotalsAmt.Size = New System.Drawing.Size(160, 20)
        Me.ceShowRowGrandTotalsAmt.TabIndex = 7
        Me.ceShowRowGrandTotalsAmt.ToolTip = "Toggles whether row grand total values are shown in the Chart control"
        '
        'ceShowColumnGrandTotalsAmt
        '
        Me.ceShowColumnGrandTotalsAmt.Location = New System.Drawing.Point(559, 39)
        Me.ceShowColumnGrandTotalsAmt.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ceShowColumnGrandTotalsAmt.Name = "ceShowColumnGrandTotalsAmt"
        Me.ceShowColumnGrandTotalsAmt.Properties.AutoWidth = True
        Me.ceShowColumnGrandTotalsAmt.Properties.Caption = "Show Column Grand Totals"
        Me.ceShowColumnGrandTotalsAmt.Size = New System.Drawing.Size(178, 20)
        Me.ceShowColumnGrandTotalsAmt.TabIndex = 13
        Me.ceShowColumnGrandTotalsAmt.ToolTip = "Toggles whether column grand total values are shown in the Chart control"
        '
        'ceSelectionOnlyAmt
        '
        Me.ceSelectionOnlyAmt.Location = New System.Drawing.Point(12, 39)
        Me.ceSelectionOnlyAmt.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ceSelectionOnlyAmt.Name = "ceSelectionOnlyAmt"
        Me.ceSelectionOnlyAmt.Properties.AutoWidth = True
        Me.ceSelectionOnlyAmt.Properties.Caption = "Selection Only"
        Me.ceSelectionOnlyAmt.Size = New System.Drawing.Size(103, 20)
        Me.ceSelectionOnlyAmt.TabIndex = 9
        Me.ceSelectionOnlyAmt.ToolTip = "Toggles whether all PivotGrid cells or selected cells only should be represented " & _
    "in the Chart"
        '
        'ceChartDataVerticalAmt
        '
        Me.ceChartDataVerticalAmt.Location = New System.Drawing.Point(356, 39)
        Me.ceChartDataVerticalAmt.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ceChartDataVerticalAmt.Name = "ceChartDataVerticalAmt"
        Me.ceChartDataVerticalAmt.Properties.AutoWidth = True
        Me.ceChartDataVerticalAmt.Properties.Caption = "Generate Series from Columns"
        Me.ceChartDataVerticalAmt.Size = New System.Drawing.Size(198, 20)
        Me.ceChartDataVerticalAmt.TabIndex = 12
        Me.ceChartDataVerticalAmt.ToolTip = "Toggles whether series in a chart control are created based on PivotGrid columns " & _
    "or rows"
        '
        'checkShowPointLabelsAmt
        '
        Me.checkShowPointLabelsAmt.Location = New System.Drawing.Point(356, 12)
        Me.checkShowPointLabelsAmt.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.checkShowPointLabelsAmt.Name = "checkShowPointLabelsAmt"
        Me.checkShowPointLabelsAmt.Properties.AutoWidth = True
        Me.checkShowPointLabelsAmt.Properties.Caption = "Show Point Labels"
        Me.checkShowPointLabelsAmt.Size = New System.Drawing.Size(126, 20)
        Me.checkShowPointLabelsAmt.TabIndex = 4
        Me.checkShowPointLabelsAmt.ToolTip = "Toggles whether value labels are shown in the Chart control"
        '
        'comboChartTypeAmt
        '
        Me.comboChartTypeAmt.EditValue = "Line"
        Me.comboChartTypeAmt.Location = New System.Drawing.Point(169, 12)
        Me.comboChartTypeAmt.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.comboChartTypeAmt.Name = "comboChartTypeAmt"
        Me.comboChartTypeAmt.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.comboChartTypeAmt.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.comboChartTypeAmt.Size = New System.Drawing.Size(180, 22)
        Me.comboChartTypeAmt.TabIndex = 3
        '
        'comboChartTypeAmt_lbl
        '
        Me.comboChartTypeAmt_lbl.Appearance.Options.UseTextOptions = True
        Me.comboChartTypeAmt_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.comboChartTypeAmt_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.comboChartTypeAmt_lbl.Location = New System.Drawing.Point(16, 15)
        Me.comboChartTypeAmt_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.comboChartTypeAmt_lbl.Name = "comboChartTypeAmt_lbl"
        Me.comboChartTypeAmt_lbl.Size = New System.Drawing.Size(146, 21)
        Me.comboChartTypeAmt_lbl.TabIndex = 2
        Me.comboChartTypeAmt_lbl.Text = "Chart Type:"
        '
        'PivotGridControlAmt
        '
        Me.PivotGridControlAmt.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.PivotGridControlAmt.Cursor = System.Windows.Forms.Cursors.Default
        Me.PivotGridControlAmt.Dock = System.Windows.Forms.DockStyle.Top
        Me.PivotGridControlAmt.Fields.AddRange(New DevExpress.XtraPivotGrid.PivotGridField() {Me.PivotGridField1, Me.PivotGridField2, Me.PivotGridField3, Me.PivotGridField4, Me.PivotGridField5, Me.PivotGridField6})
        Me.PivotGridControlAmt.Location = New System.Drawing.Point(0, 0)
        Me.PivotGridControlAmt.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.PivotGridControlAmt.Name = "PivotGridControlAmt"
        Me.PivotGridControlAmt.OptionsChartDataSource.DataProvideMode = DevExpress.XtraPivotGrid.PivotChartDataProvideMode.UseCustomSettings
        Me.PivotGridControlAmt.OptionsChartDataSource.ProvideDataByColumns = False
        Me.PivotGridControlAmt.OptionsChartDataSource.UpdateDelay = 500
        Me.PivotGridControlAmt.Size = New System.Drawing.Size(1007, 274)
        Me.PivotGridControlAmt.TabIndex = 4
        '
        'PivotGridField1
        '
        Me.PivotGridField1.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
        Me.PivotGridField1.AreaIndex = 0
        Me.PivotGridField1.Caption = "FNQuantity"
        Me.PivotGridField1.CellFormat.FormatString = "{0:n0}"
        Me.PivotGridField1.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.PivotGridField1.FieldName = "FNQuantity"
        Me.PivotGridField1.Name = "PivotGridField1"
        Me.PivotGridField1.Visible = False
        Me.PivotGridField1.Width = 86
        '
        'PivotGridField2
        '
        Me.PivotGridField2.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
        Me.PivotGridField2.AreaIndex = 0
        Me.PivotGridField2.Caption = "FNNetAmt"
        Me.PivotGridField2.CellFormat.FormatString = "{0:n2}"
        Me.PivotGridField2.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.PivotGridField2.FieldName = "FNNetAmt"
        Me.PivotGridField2.Name = "PivotGridField2"
        Me.PivotGridField2.Width = 78
        '
        'PivotGridField3
        '
        Me.PivotGridField3.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea
        Me.PivotGridField3.AreaIndex = 0
        Me.PivotGridField3.Caption = "FTYear"
        Me.PivotGridField3.FieldName = "FTYear"
        Me.PivotGridField3.Name = "PivotGridField3"
        Me.PivotGridField3.UnboundFieldName = "CFTPayYear"
        '
        'PivotGridField4
        '
        Me.PivotGridField4.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea
        Me.PivotGridField4.AreaIndex = 1
        Me.PivotGridField4.Caption = "FTMonth"
        Me.PivotGridField4.FieldName = "FTMonth"
        Me.PivotGridField4.Name = "PivotGridField4"
        Me.PivotGridField4.UnboundFieldName = "CFNMonth"
        '
        'PivotGridField5
        '
        Me.PivotGridField5.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea
        Me.PivotGridField5.AreaIndex = 0
        Me.PivotGridField5.Caption = "FTStyleCode"
        Me.PivotGridField5.FieldName = "FTStyleCode"
        Me.PivotGridField5.Name = "PivotGridField5"
        '
        'PivotGridField6
        '
        Me.PivotGridField6.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea
        Me.PivotGridField6.AreaIndex = 1
        Me.PivotGridField6.Caption = "FTStyleName"
        Me.PivotGridField6.FieldName = "FTStyleName"
        Me.PivotGridField6.Name = "PivotGridField6"
        '
        'wBIFGSale
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1014, 703)
        Me.Controls.Add(Me.otb)
        Me.Controls.Add(Me.ogbmainprocbutton)
        Me.Controls.Add(Me.ogbheader)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "wBIFGSale"
        Me.Text = "Purchaase Order Summary"
        CType(Me.ocmdoc, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbheader.ResumeLayout(False)
        Me.DockPanel1_Container.ResumeLayout(False)
        CType(Me.FTYearTo.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTYearTo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTYear.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTYear.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.panelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.panelControl1.ResumeLayout(False)
        Me.panelControl1.PerformLayout()
        CType(Me.seUpdateDelay.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ceShowRowGrandTotals.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ceShowColumnGrandTotals.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ceSelectionOnly.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ceChartDataVertical.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.checkShowPointLabels.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.comboChartType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pivotGridControlQty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(XyDiagram1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(LineSeriesView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chartControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbmainprocbutton.ResumeLayout(False)
        CType(Me.otb, System.ComponentModel.ISupportInitialize).EndInit()
        Me.otb.ResumeLayout(False)
        Me.otpchart.ResumeLayout(False)
        Me.otpdata.ResumeLayout(False)
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        CType(XyDiagram2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(LineSeriesView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chartControlQty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl2.ResumeLayout(False)
        Me.PanelControl2.PerformLayout()
        CType(Me.seUpdateDelayQty.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ceShowRowGrandTotalsQty.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ceShowColumnGrandTotalsQty.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ceSelectionOnlyQty.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ceChartDataVerticalQty.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.checkShowPointLabelsQty.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.comboChartTypeQty.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.otpAmount.ResumeLayout(False)
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl2.ResumeLayout(False)
        CType(XyDiagram3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(LineSeriesView3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chartControlAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl3.ResumeLayout(False)
        Me.PanelControl3.PerformLayout()
        CType(Me.seUpdateDelayAmt.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ceShowRowGrandTotalsAmt.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ceShowColumnGrandTotalsAmt.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ceSelectionOnlyAmt.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ceChartDataVerticalAmt.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.checkShowPointLabelsAmt.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.comboChartTypeAmt.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PivotGridControlAmt, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ocmdoc As DevExpress.XtraBars.Docking.DockManager
    Friend WithEvents ogbheader As DevExpress.XtraBars.Docking.DockPanel
    Friend WithEvents DockPanel1_Container As DevExpress.XtraBars.Docking.ControlContainer
    Friend WithEvents FTYearTo As DevExpress.XtraEditors.DateEdit
    Friend WithEvents FTYearTo_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTYear As DevExpress.XtraEditors.DateEdit
    Friend WithEvents FTYear_lbl As DevExpress.XtraEditors.LabelControl
    Private WithEvents pivotGridControlQty As DevExpress.XtraPivotGrid.PivotGridControl
    Private WithEvents CFFNNetAmt As DevExpress.XtraPivotGrid.PivotGridField
    Private WithEvents CFTMonth As DevExpress.XtraPivotGrid.PivotGridField
    Private WithEvents CFTYear As DevExpress.XtraPivotGrid.PivotGridField
    Private WithEvents chartControl As DevExpress.XtraCharts.ChartControl
    Private WithEvents panelControl1 As DevExpress.XtraEditors.PanelControl
    Private WithEvents seUpdateDelay As DevExpress.XtraEditors.SpinEdit
    Private WithEvents lblUpdateDelay As DevExpress.XtraEditors.LabelControl
    Private WithEvents ceShowRowGrandTotals As DevExpress.XtraEditors.CheckEdit
    Private WithEvents ceShowColumnGrandTotals As DevExpress.XtraEditors.CheckEdit
    Private WithEvents ceSelectionOnly As DevExpress.XtraEditors.CheckEdit
    Private WithEvents ceChartDataVertical As DevExpress.XtraEditors.CheckEdit
    Private WithEvents checkShowPointLabels As DevExpress.XtraEditors.CheckEdit
    Private WithEvents comboChartType As DevExpress.XtraEditors.ComboBoxEdit
    Private WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ogbmainprocbutton As DevExpress.XtraEditors.PanelControl
    Friend WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmload As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents otb As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents otpdata As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents otpchart As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents CFNQuantity As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents CFTStyleCode As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents CFTStyleName As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents otpAmount As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents GroupControl2 As DevExpress.XtraEditors.GroupControl
    Private WithEvents chartControlAmt As DevExpress.XtraCharts.ChartControl
    Private WithEvents PanelControl3 As DevExpress.XtraEditors.PanelControl
    Private WithEvents seUpdateDelayAmt As DevExpress.XtraEditors.SpinEdit
    Private WithEvents seUpdateDelayAmt_lbl As DevExpress.XtraEditors.LabelControl
    Private WithEvents ceShowRowGrandTotalsAmt As DevExpress.XtraEditors.CheckEdit
    Private WithEvents ceShowColumnGrandTotalsAmt As DevExpress.XtraEditors.CheckEdit
    Private WithEvents ceSelectionOnlyAmt As DevExpress.XtraEditors.CheckEdit
    Private WithEvents ceChartDataVerticalAmt As DevExpress.XtraEditors.CheckEdit
    Private WithEvents checkShowPointLabelsAmt As DevExpress.XtraEditors.CheckEdit
    Private WithEvents comboChartTypeAmt As DevExpress.XtraEditors.ComboBoxEdit
    Private WithEvents comboChartTypeAmt_lbl As DevExpress.XtraEditors.LabelControl
    Private WithEvents PivotGridControlAmt As DevExpress.XtraPivotGrid.PivotGridControl
    Friend WithEvents PivotGridField1 As DevExpress.XtraPivotGrid.PivotGridField
    Private WithEvents PivotGridField2 As DevExpress.XtraPivotGrid.PivotGridField
    Private WithEvents PivotGridField3 As DevExpress.XtraPivotGrid.PivotGridField
    Private WithEvents PivotGridField4 As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents PivotGridField5 As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents PivotGridField6 As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Private WithEvents chartControlQty As DevExpress.XtraCharts.ChartControl
    Private WithEvents PanelControl2 As DevExpress.XtraEditors.PanelControl
    Private WithEvents seUpdateDelayQty As DevExpress.XtraEditors.SpinEdit
    Private WithEvents seUpdateDelayQty_lbl As DevExpress.XtraEditors.LabelControl
    Private WithEvents ceShowRowGrandTotalsQty As DevExpress.XtraEditors.CheckEdit
    Private WithEvents ceShowColumnGrandTotalsQty As DevExpress.XtraEditors.CheckEdit
    Private WithEvents ceSelectionOnlyQty As DevExpress.XtraEditors.CheckEdit
    Private WithEvents ceChartDataVerticalQty As DevExpress.XtraEditors.CheckEdit
    Private WithEvents checkShowPointLabelsQty As DevExpress.XtraEditors.CheckEdit
    Private WithEvents comboChartTypeQty As DevExpress.XtraEditors.ComboBoxEdit
    Private WithEvents comboChartTypeQty_lbl As DevExpress.XtraEditors.LabelControl
End Class

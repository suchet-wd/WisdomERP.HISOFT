Imports DevExpress.XtraEditors.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wBIReceiveSummary
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(wBIReceiveSummary))
        Dim XyDiagram1 As DevExpress.XtraCharts.XYDiagram = New DevExpress.XtraCharts.XYDiagram()
        Dim LineSeriesView1 As DevExpress.XtraCharts.LineSeriesView = New DevExpress.XtraCharts.LineSeriesView()
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
        Me.pivotGridControl = New DevExpress.XtraPivotGrid.PivotGridControl()
        Me.CFTSuplCode = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.CFTSuplName = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.CFTRawMatCode = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.CFNRcvQuantity = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.CFNRcvAmt = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.CFNRtsQuantity = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.CFNRtsAmt = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.CFTYear = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.CFTMonth = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.CFTCmpCode = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.CFTCmpName = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.chartControl = New DevExpress.XtraCharts.ChartControl()
        Me.ogbmainprocbutton = New DevExpress.XtraEditors.PanelControl()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmload = New DevExpress.XtraEditors.SimpleButton()
        Me.otb = New DevExpress.XtraTab.XtraTabControl()
        Me.otpdata = New DevExpress.XtraTab.XtraTabPage()
        Me.otpchart = New DevExpress.XtraTab.XtraTabPage()
        Me.PFTWHCode = New DevExpress.XtraPivotGrid.PivotGridField()
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
        CType(Me.pivotGridControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chartControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(XyDiagram1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(LineSeriesView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbmainprocbutton.SuspendLayout()
        CType(Me.otb, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.otb.SuspendLayout()
        Me.otpdata.SuspendLayout()
        Me.otpchart.SuspendLayout()
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
        Me.DockPanel1_Container.Location = New System.Drawing.Point(4, 24)
        Me.DockPanel1_Container.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.DockPanel1_Container.Name = "DockPanel1_Container"
        Me.DockPanel1_Container.Size = New System.Drawing.Size(1006, 45)
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
        Me.FTYearTo.Properties.DisplayFormat.FormatString = "MM/yyyy"
        Me.FTYearTo.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTYearTo.Properties.EditFormat.FormatString = "MM/yyyy"
        Me.FTYearTo.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTYearTo.Properties.Mask.EditMask = "MM/yyyy"
        Me.FTYearTo.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.FTYearTo.Properties.NullDate = ""
        Me.FTYearTo.Size = New System.Drawing.Size(131, 22)
        Me.FTYearTo.TabIndex = 478
        Me.FTYearTo.Tag = "2|"
        '
        'FTYearTo_lbl
        '
        Me.FTYearTo_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
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
        Me.FTYear.Properties.DisplayFormat.FormatString = "MM/yyyy"
        Me.FTYear.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTYear.Properties.EditFormat.FormatString = "MM/yyyy"
        Me.FTYear.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTYear.Properties.Mask.EditMask = "MM/yyyy"
        Me.FTYear.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.FTYear.Properties.NullDate = ""
        Me.FTYear.Size = New System.Drawing.Size(131, 22)
        Me.FTYear.TabIndex = 477
        Me.FTYear.Tag = "2|"
        '
        'FTYear_lbl
        '
        Me.FTYear_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
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
        Me.panelControl1.Padding = New System.Windows.Forms.Padding(6)
        Me.panelControl1.Size = New System.Drawing.Size(1008, 70)
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
        Me.LabelControl1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LabelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LabelControl1.Location = New System.Drawing.Point(16, 15)
        Me.LabelControl1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(146, 21)
        Me.LabelControl1.TabIndex = 2
        Me.LabelControl1.Text = "Chart Type:"
        '
        'pivotGridControl
        '
        Me.pivotGridControl.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.pivotGridControl.Cursor = System.Windows.Forms.Cursors.Default
        Me.pivotGridControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pivotGridControl.Fields.AddRange(New DevExpress.XtraPivotGrid.PivotGridField() {Me.CFTSuplCode, Me.CFTSuplName, Me.CFTRawMatCode, Me.CFNRcvQuantity, Me.CFNRcvAmt, Me.CFNRtsQuantity, Me.CFNRtsAmt, Me.CFTYear, Me.CFTMonth, Me.CFTCmpCode, Me.CFTCmpName, Me.PFTWHCode})
        Me.pivotGridControl.Location = New System.Drawing.Point(0, 0)
        Me.pivotGridControl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.pivotGridControl.Name = "pivotGridControl"
        Me.pivotGridControl.OptionsChartDataSource.DataProvideMode = DevExpress.XtraPivotGrid.PivotChartDataProvideMode.UseCustomSettings
        Me.pivotGridControl.OptionsChartDataSource.ProvideDataByColumns = False
        Me.pivotGridControl.OptionsChartDataSource.UpdateDelay = 500
        Me.pivotGridControl.Size = New System.Drawing.Size(1008, 599)
        Me.pivotGridControl.TabIndex = 2
        '
        'CFTSuplCode
        '
        Me.CFTSuplCode.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea
        Me.CFTSuplCode.AreaIndex = 2
        Me.CFTSuplCode.Caption = "FTSuplCode"
        Me.CFTSuplCode.FieldName = "FTSuplCode"
        Me.CFTSuplCode.Name = "CFTSuplCode"
        '
        'CFTSuplName
        '
        Me.CFTSuplName.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea
        Me.CFTSuplName.AreaIndex = 3
        Me.CFTSuplName.Caption = "FTSuplName"
        Me.CFTSuplName.FieldName = "FTSuplName"
        Me.CFTSuplName.Name = "CFTSuplName"
        Me.CFTSuplName.Width = 150
        '
        'CFTRawMatCode
        '
        Me.CFTRawMatCode.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea
        Me.CFTRawMatCode.AreaIndex = 4
        Me.CFTRawMatCode.Caption = "FTRawMatCode"
        Me.CFTRawMatCode.FieldName = "FTRawMatCode"
        Me.CFTRawMatCode.Name = "CFTRawMatCode"
        '
        'CFNRcvQuantity
        '
        Me.CFNRcvQuantity.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
        Me.CFNRcvQuantity.AreaIndex = 0
        Me.CFNRcvQuantity.Caption = "FNRcvQuantity"
        Me.CFNRcvQuantity.CellFormat.FormatString = "{0:n4}"
        Me.CFNRcvQuantity.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CFNRcvQuantity.FieldName = "FNRcvQuantity"
        Me.CFNRcvQuantity.Name = "CFNRcvQuantity"
        Me.CFNRcvQuantity.Width = 88
        '
        'CFNRcvAmt
        '
        Me.CFNRcvAmt.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
        Me.CFNRcvAmt.AreaIndex = 1
        Me.CFNRcvAmt.Caption = "FNRcvAmt"
        Me.CFNRcvAmt.CellFormat.FormatString = "{0:n2}"
        Me.CFNRcvAmt.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CFNRcvAmt.FieldName = "FNRcvAmt"
        Me.CFNRcvAmt.Name = "CFNRcvAmt"
        Me.CFNRcvAmt.Width = 80
        '
        'CFNRtsQuantity
        '
        Me.CFNRtsQuantity.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
        Me.CFNRtsQuantity.AreaIndex = 2
        Me.CFNRtsQuantity.Caption = "FNRtsQuantity"
        Me.CFNRtsQuantity.CellFormat.FormatString = "{0:n4}"
        Me.CFNRtsQuantity.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CFNRtsQuantity.FieldName = "FNRtsQuantity"
        Me.CFNRtsQuantity.Name = "CFNRtsQuantity"
        Me.CFNRtsQuantity.Width = 90
        '
        'CFNRtsAmt
        '
        Me.CFNRtsAmt.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
        Me.CFNRtsAmt.AreaIndex = 3
        Me.CFNRtsAmt.Caption = "FNRtsAmt"
        Me.CFNRtsAmt.CellFormat.FormatString = "{0:n2}"
        Me.CFNRtsAmt.CellFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.CFNRtsAmt.FieldName = "FNRtsAmt"
        Me.CFNRtsAmt.Name = "CFNRtsAmt"
        Me.CFNRtsAmt.Width = 80
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
        'CFTCmpCode
        '
        Me.CFTCmpCode.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea
        Me.CFTCmpCode.AreaIndex = 0
        Me.CFTCmpCode.Caption = "FTCmpCode"
        Me.CFTCmpCode.FieldName = "FTCmpCode"
        Me.CFTCmpCode.Name = "CFTCmpCode"
        '
        'CFTCmpName
        '
        Me.CFTCmpName.AreaIndex = 0
        Me.CFTCmpName.Caption = "FTCmpName"
        Me.CFTCmpName.FieldName = "FTCmpName"
        Me.CFTCmpName.Name = "CFTCmpName"
        '
        'chartControl
        '
        XyDiagram1.AxisX.Label.Staggered = True
        XyDiagram1.AxisX.VisibleInPanesSerializable = "-1"
        XyDiagram1.AxisY.VisibleInPanesSerializable = "-1"
        Me.chartControl.Diagram = XyDiagram1
        Me.chartControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.chartControl.Legend.MaxHorizontalPercentage = 30.0R
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
        Me.chartControl.Size = New System.Drawing.Size(1008, 529)
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
        Me.otb.SelectedTabPage = Me.otpdata
        Me.otb.Size = New System.Drawing.Size(1014, 630)
        Me.otb.TabIndex = 394
        Me.otb.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.otpdata, Me.otpchart})
        '
        'otpdata
        '
        Me.otpdata.Controls.Add(Me.pivotGridControl)
        Me.otpdata.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.otpdata.Name = "otpdata"
        Me.otpdata.Size = New System.Drawing.Size(1008, 599)
        Me.otpdata.Text = "Data"
        '
        'otpchart
        '
        Me.otpchart.Controls.Add(Me.chartControl)
        Me.otpchart.Controls.Add(Me.panelControl1)
        Me.otpchart.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.otpchart.Name = "otpchart"
        Me.otpchart.PageVisible = False
        Me.otpchart.Size = New System.Drawing.Size(1008, 599)
        Me.otpchart.Text = "Chart"
        '
        'PFTWHCode
        '
        Me.PFTWHCode.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea
        Me.PFTWHCode.AreaIndex = 1
        Me.PFTWHCode.Caption = "คลัง"
        Me.PFTWHCode.FieldName = "FTWHCode"
        Me.PFTWHCode.Name = "PFTWHCode"
        '
        'wBIReceiveSummary
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1014, 703)
        Me.Controls.Add(Me.otb)
        Me.Controls.Add(Me.ogbmainprocbutton)
        Me.Controls.Add(Me.ogbheader)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "wBIReceiveSummary"
        Me.Text = "Receive Summary"
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
        CType(Me.pivotGridControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(XyDiagram1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(LineSeriesView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chartControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbmainprocbutton.ResumeLayout(False)
        CType(Me.otb, System.ComponentModel.ISupportInitialize).EndInit()
        Me.otb.ResumeLayout(False)
        Me.otpdata.ResumeLayout(False)
        Me.otpchart.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ocmdoc As DevExpress.XtraBars.Docking.DockManager
    Friend WithEvents ogbheader As DevExpress.XtraBars.Docking.DockPanel
    Friend WithEvents DockPanel1_Container As DevExpress.XtraBars.Docking.ControlContainer
    Friend WithEvents FTYearTo As DevExpress.XtraEditors.DateEdit
    Friend WithEvents FTYearTo_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTYear As DevExpress.XtraEditors.DateEdit
    Friend WithEvents FTYear_lbl As DevExpress.XtraEditors.LabelControl
    Private WithEvents pivotGridControl As DevExpress.XtraPivotGrid.PivotGridControl
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
    Friend WithEvents CFTSuplCode As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents CFTSuplName As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents ogbmainprocbutton As DevExpress.XtraEditors.PanelControl
    Friend WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmload As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents otb As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents otpdata As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents otpchart As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents CFNRcvQuantity As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents CFNRcvAmt As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents CFNRtsQuantity As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents CFNRtsAmt As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents CFTRawMatCode As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents CFTCmpCode As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents CFTCmpName As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents PFTWHCode As DevExpress.XtraPivotGrid.PivotGridField
End Class

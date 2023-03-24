Imports DevExpress.XtraEditors.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wDailyQualityAssuranceDefectFinal
    Inherits DevExpress.XtraEditors.XtraForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(wDailyQualityAssuranceDefectFinal))
        Dim SerializableAppearanceObject5 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim XyDiagram1 As DevExpress.XtraCharts.XYDiagram = New DevExpress.XtraCharts.XYDiagram()
        Dim LineSeriesView1 As DevExpress.XtraCharts.LineSeriesView = New DevExpress.XtraCharts.LineSeriesView()
        Dim XyDiagram2 As DevExpress.XtraCharts.XYDiagram = New DevExpress.XtraCharts.XYDiagram()
        Dim LineSeriesView2 As DevExpress.XtraCharts.LineSeriesView = New DevExpress.XtraCharts.LineSeriesView()
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject4 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim GridLevelNode1 As DevExpress.XtraGrid.GridLevelNode = New DevExpress.XtraGrid.GridLevelNode()
        Me.ocmdoc = New DevExpress.XtraBars.Docking.DockManager()
        Me.ogbheader = New DevExpress.XtraBars.Docking.DockPanel()
        Me.DockPanel1_Container = New DevExpress.XtraBars.Docking.ControlContainer()
        Me.FTEDate = New DevExpress.XtraEditors.DateEdit()
        Me.FTEDate_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTSDate = New DevExpress.XtraEditors.DateEdit()
        Me.FNHSysCmpId_None = New DevExpress.XtraEditors.TextEdit()
        Me.FNHSysCmpId_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTSDate_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysCmpId = New DevExpress.XtraEditors.ButtonEdit()
        Me.ogbmainprocbutton = New DevExpress.XtraEditors.PanelControl()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmload = New DevExpress.XtraEditors.SimpleButton()
        Me.otb = New DevExpress.XtraTab.XtraTabControl()
        Me.otpchart = New DevExpress.XtraTab.XtraTabPage()
        Me.chartControl = New DevExpress.XtraCharts.ChartControl()
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
        Me.otpgrpdefect = New DevExpress.XtraTab.XtraTabPage()
        Me.chartControlgrpdefect = New DevExpress.XtraCharts.ChartControl()
        Me.otpdata = New DevExpress.XtraTab.XtraTabPage()
        Me.opggrpdefectcode = New DevExpress.XtraPivotGrid.PivotGridControl()
        Me.PivotGridField1 = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.PivotGridField2 = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.PivotGridField3 = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.pivotGridControl = New DevExpress.XtraPivotGrid.PivotGridControl()
        Me.CFNSeq = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.CCFTUnitSectCode = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.CCFTStateData = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.CCFTData = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.opndata = New DevExpress.XtraEditors.PanelControl()
        Me.FNTotalQADefectPoint = New DevExpress.XtraEditors.CalcEdit()
        Me.FNTotalQADefectPoint_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNTotalQADefect = New DevExpress.XtraEditors.CalcEdit()
        Me.FNTotalQADefect_Lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNTotalQA = New DevExpress.XtraEditors.CalcEdit()
        Me.FNTotalQA_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNTotalIn = New DevExpress.XtraEditors.CalcEdit()
        Me.FNTotalIn_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.ogcdefectgrp = New DevExpress.XtraGrid.GridControl()
        Me.ogvdefectgrp = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.C2FTDefectGrp = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.C2FNQty = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.C2FNDfectPerByQA = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ogcdefect = New DevExpress.XtraGrid.GridControl()
        Me.ogvdefect = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.CCFNSeq = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFTQACode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFTDefectGrp = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFTDefectName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFNQty = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFNDfectPerByQA = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFNDfectPerByDefect = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFNDfectPerByDefectSummary = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.ocmdoc, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbheader.SuspendLayout()
        Me.DockPanel1_Container.SuspendLayout()
        CType(Me.FTEDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTEDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTSDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTSDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysCmpId_None.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysCmpId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbmainprocbutton.SuspendLayout()
        CType(Me.otb, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.otb.SuspendLayout()
        Me.otpchart.SuspendLayout()
        CType(Me.chartControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(XyDiagram1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(LineSeriesView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.panelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.panelControl1.SuspendLayout()
        CType(Me.seUpdateDelay.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ceShowRowGrandTotals.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ceShowColumnGrandTotals.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ceSelectionOnly.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ceChartDataVertical.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.checkShowPointLabels.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.comboChartType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.otpgrpdefect.SuspendLayout()
        CType(Me.chartControlgrpdefect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(XyDiagram2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(LineSeriesView2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.otpdata.SuspendLayout()
        CType(Me.opggrpdefectcode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pivotGridControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.opndata, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.opndata.SuspendLayout()
        CType(Me.FNTotalQADefectPoint.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNTotalQADefect.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNTotalQA.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNTotalIn.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogcdefectgrp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvdefectgrp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogcdefect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvdefect, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.ogbheader.Name = "ogbheader"
        Me.ogbheader.Options.AllowDockBottom = False
        Me.ogbheader.Options.AllowDockFill = False
        Me.ogbheader.Options.AllowDockLeft = False
        Me.ogbheader.Options.AllowDockRight = False
        Me.ogbheader.Options.AllowFloating = False
        Me.ogbheader.Options.FloatOnDblClick = False
        Me.ogbheader.Options.ShowCloseButton = False
        Me.ogbheader.Options.ShowMaximizeButton = False
        Me.ogbheader.OriginalSize = New System.Drawing.Size(200, 88)
        Me.ogbheader.Size = New System.Drawing.Size(892, 88)
        Me.ogbheader.Text = "Criteria"
        '
        'DockPanel1_Container
        '
        Me.DockPanel1_Container.Controls.Add(Me.FTEDate)
        Me.DockPanel1_Container.Controls.Add(Me.FTEDate_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FTSDate)
        Me.DockPanel1_Container.Controls.Add(Me.FNHSysCmpId_None)
        Me.DockPanel1_Container.Controls.Add(Me.FNHSysCmpId_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FTSDate_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FNHSysCmpId)
        Me.DockPanel1_Container.Location = New System.Drawing.Point(2, 28)
        Me.DockPanel1_Container.Name = "DockPanel1_Container"
        Me.DockPanel1_Container.Size = New System.Drawing.Size(888, 57)
        Me.DockPanel1_Container.TabIndex = 0
        '
        'FTEDate
        '
        Me.FTEDate.EditValue = Nothing
        Me.FTEDate.EnterMoveNextControl = True
        Me.FTEDate.Location = New System.Drawing.Point(373, 25)
        Me.FTEDate.Name = "FTEDate"
        Me.FTEDate.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTEDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FTEDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.FTEDate.Properties.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.FTEDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.FTEDate.Properties.EditFormat.FormatString = "dd/MM/yyyy"
        Me.FTEDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.FTEDate.Properties.Mask.EditMask = "dd/MM/yyyy"
        Me.FTEDate.Properties.NullDate = ""
        Me.FTEDate.Size = New System.Drawing.Size(113, 20)
        Me.FTEDate.TabIndex = 480
        Me.FTEDate.Tag = "2|"
        '
        'FTEDate_lbl
        '
        Me.FTEDate_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTEDate_lbl.Appearance.Options.UseForeColor = True
        Me.FTEDate_lbl.Appearance.Options.UseTextOptions = True
        Me.FTEDate_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTEDate_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTEDate_lbl.Location = New System.Drawing.Point(249, 24)
        Me.FTEDate_lbl.Name = "FTEDate_lbl"
        Me.FTEDate_lbl.Size = New System.Drawing.Size(121, 19)
        Me.FTEDate_lbl.TabIndex = 481
        Me.FTEDate_lbl.Tag = "2|"
        Me.FTEDate_lbl.Text = "To Date :"
        '
        'FTSDate
        '
        Me.FTSDate.EditValue = Nothing
        Me.FTSDate.EnterMoveNextControl = True
        Me.FTSDate.Location = New System.Drawing.Point(132, 25)
        Me.FTSDate.Name = "FTSDate"
        Me.FTSDate.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTSDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FTSDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.FTSDate.Properties.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.FTSDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.FTSDate.Properties.EditFormat.FormatString = "dd/MM/yyyy"
        Me.FTSDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.FTSDate.Properties.Mask.EditMask = "dd/MM/yyyy"
        Me.FTSDate.Properties.NullDate = ""
        Me.FTSDate.Size = New System.Drawing.Size(113, 20)
        Me.FTSDate.TabIndex = 395
        Me.FTSDate.Tag = "2|"
        '
        'FNHSysCmpId_None
        '
        Me.FNHSysCmpId_None.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FNHSysCmpId_None.Location = New System.Drawing.Point(246, 3)
        Me.FNHSysCmpId_None.Name = "FNHSysCmpId_None"
        Me.FNHSysCmpId_None.Properties.Appearance.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysCmpId_None.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysCmpId_None.Properties.ReadOnly = True
        Me.FNHSysCmpId_None.Size = New System.Drawing.Size(487, 20)
        Me.FNHSysCmpId_None.TabIndex = 505
        Me.FNHSysCmpId_None.Tag = ""
        '
        'FNHSysCmpId_lbl
        '
        Me.FNHSysCmpId_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysCmpId_lbl.Appearance.Options.UseForeColor = True
        Me.FNHSysCmpId_lbl.Appearance.Options.UseTextOptions = True
        Me.FNHSysCmpId_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysCmpId_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysCmpId_lbl.Location = New System.Drawing.Point(6, 3)
        Me.FNHSysCmpId_lbl.Name = "FNHSysCmpId_lbl"
        Me.FNHSysCmpId_lbl.Size = New System.Drawing.Size(123, 17)
        Me.FNHSysCmpId_lbl.TabIndex = 504
        Me.FNHSysCmpId_lbl.Tag = "2|"
        Me.FNHSysCmpId_lbl.Text = "Company :"
        '
        'FTSDate_lbl
        '
        Me.FTSDate_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTSDate_lbl.Appearance.Options.UseForeColor = True
        Me.FTSDate_lbl.Appearance.Options.UseTextOptions = True
        Me.FTSDate_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTSDate_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTSDate_lbl.Location = New System.Drawing.Point(8, 24)
        Me.FTSDate_lbl.Name = "FTSDate_lbl"
        Me.FTSDate_lbl.Size = New System.Drawing.Size(121, 19)
        Me.FTSDate_lbl.TabIndex = 479
        Me.FTSDate_lbl.Tag = "2|"
        Me.FTSDate_lbl.Text = "Date :"
        '
        'FNHSysCmpId
        '
        Me.FNHSysCmpId.Location = New System.Drawing.Point(132, 3)
        Me.FNHSysCmpId.Name = "FNHSysCmpId"
        Me.FNHSysCmpId.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysCmpId.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysCmpId.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNHSysCmpId.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNHSysCmpId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject5, "", "11", Nothing, True)})
        Me.FNHSysCmpId.Properties.Tag = ""
        Me.FNHSysCmpId.Size = New System.Drawing.Size(113, 20)
        Me.FNHSysCmpId.TabIndex = 503
        Me.FNHSysCmpId.Tag = ""
        '
        'ogbmainprocbutton
        '
        Me.ogbmainprocbutton.Controls.Add(Me.ocmexit)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmload)
        Me.ogbmainprocbutton.Location = New System.Drawing.Point(205, 3)
        Me.ogbmainprocbutton.Name = "ogbmainprocbutton"
        Me.ogbmainprocbutton.Size = New System.Drawing.Size(498, 34)
        Me.ogbmainprocbutton.TabIndex = 391
        '
        'ocmexit
        '
        Me.ocmexit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmexit.Location = New System.Drawing.Point(339, 7)
        Me.ocmexit.Name = "ocmexit"
        Me.ocmexit.Size = New System.Drawing.Size(95, 25)
        Me.ocmexit.TabIndex = 96
        Me.ocmexit.TabStop = False
        Me.ocmexit.Tag = "2|"
        Me.ocmexit.Text = "EXIT"
        '
        'ocmload
        '
        Me.ocmload.Location = New System.Drawing.Point(17, 5)
        Me.ocmload.Name = "ocmload"
        Me.ocmload.Size = New System.Drawing.Size(95, 25)
        Me.ocmload.TabIndex = 93
        Me.ocmload.TabStop = False
        Me.ocmload.Tag = "2|"
        Me.ocmload.Text = "LoadData"
        '
        'otb
        '
        Me.otb.Dock = System.Windows.Forms.DockStyle.Fill
        Me.otb.Location = New System.Drawing.Point(0, 88)
        Me.otb.Name = "otb"
        Me.otb.SelectedTabPage = Me.otpchart
        Me.otb.Size = New System.Drawing.Size(892, 215)
        Me.otb.TabIndex = 396
        Me.otb.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.otpchart, Me.otpgrpdefect, Me.otpdata})
        '
        'otpchart
        '
        Me.otpchart.Controls.Add(Me.chartControl)
        Me.otpchart.Controls.Add(Me.panelControl1)
        Me.otpchart.Name = "otpchart"
        Me.otpchart.Size = New System.Drawing.Size(884, 185)
        Me.otpchart.Text = "Chart Defect"
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
        Me.chartControl.Location = New System.Drawing.Point(0, 32)
        Me.chartControl.Name = "chartControl"
        Me.chartControl.SeriesDataMember = "Series"
        Me.chartControl.SeriesSerializable = New DevExpress.XtraCharts.Series(-1) {}
        Me.chartControl.SeriesTemplate.ArgumentDataMember = "Arguments"
        Me.chartControl.SeriesTemplate.CrosshairLabelPattern = "{S} : {V:c}"
        Me.chartControl.SeriesTemplate.LabelsVisibility = DevExpress.Utils.DefaultBoolean.[False]
        Me.chartControl.SeriesTemplate.LegendName = "Default Legend"
        Me.chartControl.SeriesTemplate.LegendTextPattern = "{V:0.00%}"
        Me.chartControl.SeriesTemplate.ValueDataMembersSerializable = "Values"
        Me.chartControl.SeriesTemplate.View = LineSeriesView1
        Me.chartControl.Size = New System.Drawing.Size(884, 153)
        Me.chartControl.TabIndex = 3
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
        Me.panelControl1.Name = "panelControl1"
        Me.panelControl1.Padding = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.panelControl1.Size = New System.Drawing.Size(884, 32)
        Me.panelControl1.TabIndex = 3
        Me.panelControl1.Visible = False
        '
        'seUpdateDelay
        '
        Me.seUpdateDelay.EditValue = New Decimal(New Integer() {500, 0, 0, 0})
        Me.seUpdateDelay.Location = New System.Drawing.Point(697, 3)
        Me.seUpdateDelay.Name = "seUpdateDelay"
        Me.seUpdateDelay.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.seUpdateDelay.Properties.Increment = New Decimal(New Integer() {100, 0, 0, 0})
        Me.seUpdateDelay.Properties.IsFloatValue = False
        Me.seUpdateDelay.Properties.Mask.EditMask = "N00"
        Me.seUpdateDelay.Properties.MaxValue = New Decimal(New Integer() {5000, 0, 0, 0})
        Me.seUpdateDelay.Size = New System.Drawing.Size(48, 20)
        Me.seUpdateDelay.TabIndex = 10
        Me.seUpdateDelay.ToolTip = "Sets the Chart update delay when PivotGrid selection changes."
        Me.seUpdateDelay.Visible = False
        '
        'lblUpdateDelay
        '
        Me.lblUpdateDelay.Location = New System.Drawing.Point(605, 11)
        Me.lblUpdateDelay.Name = "lblUpdateDelay"
        Me.lblUpdateDelay.Size = New System.Drawing.Size(69, 13)
        Me.lblUpdateDelay.TabIndex = 13
        Me.lblUpdateDelay.Text = "Update Delay:"
        Me.lblUpdateDelay.Visible = False
        '
        'ceShowRowGrandTotals
        '
        Me.ceShowRowGrandTotals.Location = New System.Drawing.Point(479, 10)
        Me.ceShowRowGrandTotals.Name = "ceShowRowGrandTotals"
        Me.ceShowRowGrandTotals.Properties.AutoWidth = True
        Me.ceShowRowGrandTotals.Properties.Caption = "Show Row Grand Totals"
        Me.ceShowRowGrandTotals.Size = New System.Drawing.Size(137, 20)
        Me.ceShowRowGrandTotals.TabIndex = 7
        Me.ceShowRowGrandTotals.ToolTip = "Toggles whether row grand total values are shown in the Chart control"
        Me.ceShowRowGrandTotals.Visible = False
        '
        'ceShowColumnGrandTotals
        '
        Me.ceShowColumnGrandTotals.Location = New System.Drawing.Point(697, 8)
        Me.ceShowColumnGrandTotals.Name = "ceShowColumnGrandTotals"
        Me.ceShowColumnGrandTotals.Properties.AutoWidth = True
        Me.ceShowColumnGrandTotals.Properties.Caption = "Show Column Grand Totals"
        Me.ceShowColumnGrandTotals.Size = New System.Drawing.Size(151, 20)
        Me.ceShowColumnGrandTotals.TabIndex = 13
        Me.ceShowColumnGrandTotals.ToolTip = "Toggles whether column grand total values are shown in the Chart control"
        Me.ceShowColumnGrandTotals.Visible = False
        '
        'ceSelectionOnly
        '
        Me.ceSelectionOnly.Location = New System.Drawing.Point(621, 2)
        Me.ceSelectionOnly.Name = "ceSelectionOnly"
        Me.ceSelectionOnly.Properties.AutoWidth = True
        Me.ceSelectionOnly.Properties.Caption = "Selection Only"
        Me.ceSelectionOnly.Size = New System.Drawing.Size(91, 20)
        Me.ceSelectionOnly.TabIndex = 9
        Me.ceSelectionOnly.ToolTip = "Toggles whether all PivotGrid cells or selected cells only should be represented " &
    "in the Chart"
        Me.ceSelectionOnly.Visible = False
        '
        'ceChartDataVertical
        '
        Me.ceChartDataVertical.Location = New System.Drawing.Point(430, 4)
        Me.ceChartDataVertical.Name = "ceChartDataVertical"
        Me.ceChartDataVertical.Properties.AutoWidth = True
        Me.ceChartDataVertical.Properties.Caption = "Generate Series from Columns"
        Me.ceChartDataVertical.Size = New System.Drawing.Size(168, 20)
        Me.ceChartDataVertical.TabIndex = 12
        Me.ceChartDataVertical.ToolTip = "Toggles whether series in a chart control are created based on PivotGrid columns " &
    "or rows"
        Me.ceChartDataVertical.Visible = False
        '
        'checkShowPointLabels
        '
        Me.checkShowPointLabels.Location = New System.Drawing.Point(305, 10)
        Me.checkShowPointLabels.Name = "checkShowPointLabels"
        Me.checkShowPointLabels.Properties.AutoWidth = True
        Me.checkShowPointLabels.Properties.Caption = "Show Point Labels"
        Me.checkShowPointLabels.Size = New System.Drawing.Size(109, 20)
        Me.checkShowPointLabels.TabIndex = 4
        Me.checkShowPointLabels.ToolTip = "Toggles whether value labels are shown in the Chart control"
        Me.checkShowPointLabels.Visible = False
        '
        'comboChartType
        '
        Me.comboChartType.EditValue = "Line"
        Me.comboChartType.Location = New System.Drawing.Point(145, 6)
        Me.comboChartType.Name = "comboChartType"
        Me.comboChartType.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.comboChartType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.comboChartType.Size = New System.Drawing.Size(154, 20)
        Me.comboChartType.TabIndex = 3
        Me.comboChartType.Visible = False
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Options.UseTextOptions = True
        Me.LabelControl1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LabelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LabelControl1.Location = New System.Drawing.Point(14, 6)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(125, 17)
        Me.LabelControl1.TabIndex = 2
        Me.LabelControl1.Text = "Chart Type:"
        Me.LabelControl1.Visible = False
        '
        'otpgrpdefect
        '
        Me.otpgrpdefect.Controls.Add(Me.chartControlgrpdefect)
        Me.otpgrpdefect.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.otpgrpdefect.Name = "otpgrpdefect"
        Me.otpgrpdefect.Size = New System.Drawing.Size(886, 204)
        Me.otpgrpdefect.Text = "Chart Group Defect"
        '
        'chartControlgrpdefect
        '
        Me.chartControlgrpdefect.DataBindings = Nothing
        XyDiagram2.AxisX.Label.Staggered = True
        XyDiagram2.AxisX.VisibleInPanesSerializable = "-1"
        XyDiagram2.AxisY.VisibleInPanesSerializable = "-1"
        Me.chartControlgrpdefect.Diagram = XyDiagram2
        Me.chartControlgrpdefect.Dock = System.Windows.Forms.DockStyle.Fill
        Me.chartControlgrpdefect.Legend.MaxHorizontalPercentage = 30.0R
        Me.chartControlgrpdefect.Legend.Name = "Default Legend"
        Me.chartControlgrpdefect.Location = New System.Drawing.Point(0, 0)
        Me.chartControlgrpdefect.Name = "chartControlgrpdefect"
        Me.chartControlgrpdefect.SeriesDataMember = "Series"
        Me.chartControlgrpdefect.SeriesSerializable = New DevExpress.XtraCharts.Series(-1) {}
        Me.chartControlgrpdefect.SeriesTemplate.ArgumentDataMember = "Arguments"
        Me.chartControlgrpdefect.SeriesTemplate.CrosshairLabelPattern = "{S} : {V:c}"
        Me.chartControlgrpdefect.SeriesTemplate.LabelsVisibility = DevExpress.Utils.DefaultBoolean.[False]
        Me.chartControlgrpdefect.SeriesTemplate.ValueDataMembersSerializable = "Values"
        Me.chartControlgrpdefect.SeriesTemplate.View = LineSeriesView2
        Me.chartControlgrpdefect.Size = New System.Drawing.Size(886, 204)
        Me.chartControlgrpdefect.TabIndex = 4
        '
        'otpdata
        '
        Me.otpdata.Controls.Add(Me.opggrpdefectcode)
        Me.otpdata.Controls.Add(Me.pivotGridControl)
        Me.otpdata.Name = "otpdata"
        Me.otpdata.PageVisible = False
        Me.otpdata.Size = New System.Drawing.Size(886, 204)
        Me.otpdata.Text = "Data"
        '
        'opggrpdefectcode
        '
        Me.opggrpdefectcode.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.opggrpdefectcode.Cursor = System.Windows.Forms.Cursors.Default
        Me.opggrpdefectcode.Fields.AddRange(New DevExpress.XtraPivotGrid.PivotGridField() {Me.PivotGridField1, Me.PivotGridField2, Me.PivotGridField3})
        Me.opggrpdefectcode.Location = New System.Drawing.Point(583, 3)
        Me.opggrpdefectcode.Name = "opggrpdefectcode"
        Me.opggrpdefectcode.OptionsChartDataSource.DataProvideMode = DevExpress.XtraPivotGrid.PivotChartDataProvideMode.UseCustomSettings
        Me.opggrpdefectcode.OptionsChartDataSource.ProvideDataByColumns = False
        Me.opggrpdefectcode.OptionsChartDataSource.UpdateDelay = 500
        Me.opggrpdefectcode.Size = New System.Drawing.Size(293, 206)
        Me.opggrpdefectcode.TabIndex = 3
        '
        'PivotGridField1
        '
        Me.PivotGridField1.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea
        Me.PivotGridField1.AreaIndex = 0
        Me.PivotGridField1.Caption = "Code"
        Me.PivotGridField1.FieldName = "FTDefectGrp"
        Me.PivotGridField1.Name = "PivotGridField1"
        '
        'PivotGridField2
        '
        Me.PivotGridField2.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea
        Me.PivotGridField2.AreaIndex = 0
        Me.PivotGridField2.FieldName = "FTStateData"
        Me.PivotGridField2.Name = "PivotGridField2"
        '
        'PivotGridField3
        '
        Me.PivotGridField3.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
        Me.PivotGridField3.AreaIndex = 0
        Me.PivotGridField3.CellFormat.FormatString = "{0:n4}"
        Me.PivotGridField3.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.PivotGridField3.FieldName = "FNDfectPerByQA"
        Me.PivotGridField3.Name = "PivotGridField3"
        Me.PivotGridField3.SortMode = DevExpress.XtraPivotGrid.PivotSortMode.Value
        Me.PivotGridField3.SortOrder = DevExpress.XtraPivotGrid.PivotSortOrder.Descending
        '
        'pivotGridControl
        '
        Me.pivotGridControl.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.pivotGridControl.Cursor = System.Windows.Forms.Cursors.Default
        Me.pivotGridControl.Fields.AddRange(New DevExpress.XtraPivotGrid.PivotGridField() {Me.CFNSeq, Me.CCFTUnitSectCode, Me.CCFTStateData, Me.CCFTData})
        Me.pivotGridControl.Location = New System.Drawing.Point(0, 0)
        Me.pivotGridControl.Name = "pivotGridControl"
        Me.pivotGridControl.OptionsChartDataSource.DataProvideMode = DevExpress.XtraPivotGrid.PivotChartDataProvideMode.UseCustomSettings
        Me.pivotGridControl.OptionsChartDataSource.ProvideDataByColumns = False
        Me.pivotGridControl.OptionsChartDataSource.UpdateDelay = 500
        Me.pivotGridControl.Size = New System.Drawing.Size(306, 212)
        Me.pivotGridControl.TabIndex = 2
        '
        'CFNSeq
        '
        Me.CFNSeq.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea
        Me.CFNSeq.AreaIndex = 0
        Me.CFNSeq.Caption = "Seq"
        Me.CFNSeq.FieldName = "FNSeq"
        Me.CFNSeq.Name = "CFNSeq"
        '
        'CCFTUnitSectCode
        '
        Me.CCFTUnitSectCode.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea
        Me.CCFTUnitSectCode.AreaIndex = 1
        Me.CCFTUnitSectCode.Caption = "Code"
        Me.CCFTUnitSectCode.FieldName = "FTQACode"
        Me.CCFTUnitSectCode.Name = "CCFTUnitSectCode"
        '
        'CCFTStateData
        '
        Me.CCFTStateData.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea
        Me.CCFTStateData.AreaIndex = 0
        Me.CCFTStateData.FieldName = "FTStateData"
        Me.CCFTStateData.Name = "CCFTStateData"
        '
        'CCFTData
        '
        Me.CCFTData.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
        Me.CCFTData.AreaIndex = 0
        Me.CCFTData.CellFormat.FormatString = "{0:n4}"
        Me.CCFTData.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CCFTData.FieldName = "FNDfectPerByQA"
        Me.CCFTData.Name = "CCFTData"
        '
        'opndata
        '
        Me.opndata.Controls.Add(Me.FNTotalQADefectPoint)
        Me.opndata.Controls.Add(Me.FNTotalQADefectPoint_lbl)
        Me.opndata.Controls.Add(Me.FNTotalQADefect)
        Me.opndata.Controls.Add(Me.FNTotalQADefect_Lbl)
        Me.opndata.Controls.Add(Me.FNTotalQA)
        Me.opndata.Controls.Add(Me.FNTotalQA_lbl)
        Me.opndata.Controls.Add(Me.FNTotalIn)
        Me.opndata.Controls.Add(Me.FNTotalIn_lbl)
        Me.opndata.Controls.Add(Me.ogcdefectgrp)
        Me.opndata.Controls.Add(Me.ogcdefect)
        Me.opndata.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.opndata.Location = New System.Drawing.Point(0, 303)
        Me.opndata.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.opndata.Name = "opndata"
        Me.opndata.Size = New System.Drawing.Size(892, 268)
        Me.opndata.TabIndex = 398
        '
        'FNTotalQADefectPoint
        '
        Me.FNTotalQADefectPoint.EnterMoveNextControl = True
        Me.FNTotalQADefectPoint.Location = New System.Drawing.Point(726, 3)
        Me.FNTotalQADefectPoint.Name = "FNTotalQADefectPoint"
        Me.FNTotalQADefectPoint.Properties.Appearance.Options.UseTextOptions = True
        Me.FNTotalQADefectPoint.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNTotalQADefectPoint.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNTotalQADefectPoint.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNTotalQADefectPoint.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNTotalQADefectPoint.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNTotalQADefectPoint.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNTotalQADefectPoint.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNTotalQADefectPoint.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNTotalQADefectPoint.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNTotalQADefectPoint.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, True, False, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", Nothing, Nothing, True)})
        Me.FNTotalQADefectPoint.Properties.DisplayFormat.FormatString = "{0:n0}"
        Me.FNTotalQADefectPoint.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNTotalQADefectPoint.Properties.EditFormat.FormatString = "{0:n0}"
        Me.FNTotalQADefectPoint.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNTotalQADefectPoint.Properties.Precision = 0
        Me.FNTotalQADefectPoint.Properties.ReadOnly = True
        Me.FNTotalQADefectPoint.Size = New System.Drawing.Size(92, 20)
        Me.FNTotalQADefectPoint.TabIndex = 487
        Me.FNTotalQADefectPoint.Tag = "2|"
        '
        'FNTotalQADefectPoint_lbl
        '
        Me.FNTotalQADefectPoint_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNTotalQADefectPoint_lbl.Appearance.Options.UseForeColor = True
        Me.FNTotalQADefectPoint_lbl.Appearance.Options.UseTextOptions = True
        Me.FNTotalQADefectPoint_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNTotalQADefectPoint_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNTotalQADefectPoint_lbl.Location = New System.Drawing.Point(621, 2)
        Me.FNTotalQADefectPoint_lbl.Name = "FNTotalQADefectPoint_lbl"
        Me.FNTotalQADefectPoint_lbl.Size = New System.Drawing.Size(100, 19)
        Me.FNTotalQADefectPoint_lbl.TabIndex = 486
        Me.FNTotalQADefectPoint_lbl.Tag = "2|"
        Me.FNTotalQADefectPoint_lbl.Text = "Defect Point :"
        '
        'FNTotalQADefect
        '
        Me.FNTotalQADefect.EnterMoveNextControl = True
        Me.FNTotalQADefect.Location = New System.Drawing.Point(525, 3)
        Me.FNTotalQADefect.Name = "FNTotalQADefect"
        Me.FNTotalQADefect.Properties.Appearance.Options.UseTextOptions = True
        Me.FNTotalQADefect.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNTotalQADefect.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNTotalQADefect.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNTotalQADefect.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNTotalQADefect.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNTotalQADefect.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNTotalQADefect.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNTotalQADefect.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNTotalQADefect.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNTotalQADefect.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, True, False, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject2, "", Nothing, Nothing, True)})
        Me.FNTotalQADefect.Properties.DisplayFormat.FormatString = "{0:n0}"
        Me.FNTotalQADefect.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNTotalQADefect.Properties.EditFormat.FormatString = "{0:n0}"
        Me.FNTotalQADefect.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNTotalQADefect.Properties.Precision = 0
        Me.FNTotalQADefect.Properties.ReadOnly = True
        Me.FNTotalQADefect.Size = New System.Drawing.Size(92, 20)
        Me.FNTotalQADefect.TabIndex = 485
        Me.FNTotalQADefect.Tag = "2|"
        '
        'FNTotalQADefect_Lbl
        '
        Me.FNTotalQADefect_Lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNTotalQADefect_Lbl.Appearance.Options.UseForeColor = True
        Me.FNTotalQADefect_Lbl.Appearance.Options.UseTextOptions = True
        Me.FNTotalQADefect_Lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNTotalQADefect_Lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNTotalQADefect_Lbl.Location = New System.Drawing.Point(421, 2)
        Me.FNTotalQADefect_Lbl.Name = "FNTotalQADefect_Lbl"
        Me.FNTotalQADefect_Lbl.Size = New System.Drawing.Size(100, 19)
        Me.FNTotalQADefect_Lbl.TabIndex = 484
        Me.FNTotalQADefect_Lbl.Tag = "2|"
        Me.FNTotalQADefect_Lbl.Text = "Defect Qty :"
        '
        'FNTotalQA
        '
        Me.FNTotalQA.EnterMoveNextControl = True
        Me.FNTotalQA.Location = New System.Drawing.Point(326, 4)
        Me.FNTotalQA.Name = "FNTotalQA"
        Me.FNTotalQA.Properties.Appearance.Options.UseTextOptions = True
        Me.FNTotalQA.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNTotalQA.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNTotalQA.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNTotalQA.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNTotalQA.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNTotalQA.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNTotalQA.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNTotalQA.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNTotalQA.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNTotalQA.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, True, False, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject3, "", Nothing, Nothing, True)})
        Me.FNTotalQA.Properties.DisplayFormat.FormatString = "{0:n0}"
        Me.FNTotalQA.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNTotalQA.Properties.EditFormat.FormatString = "{0:n0}"
        Me.FNTotalQA.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNTotalQA.Properties.Precision = 0
        Me.FNTotalQA.Properties.ReadOnly = True
        Me.FNTotalQA.Size = New System.Drawing.Size(92, 20)
        Me.FNTotalQA.TabIndex = 483
        Me.FNTotalQA.Tag = "2|"
        '
        'FNTotalQA_lbl
        '
        Me.FNTotalQA_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNTotalQA_lbl.Appearance.Options.UseForeColor = True
        Me.FNTotalQA_lbl.Appearance.Options.UseTextOptions = True
        Me.FNTotalQA_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNTotalQA_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNTotalQA_lbl.Location = New System.Drawing.Point(221, 3)
        Me.FNTotalQA_lbl.Name = "FNTotalQA_lbl"
        Me.FNTotalQA_lbl.Size = New System.Drawing.Size(100, 19)
        Me.FNTotalQA_lbl.TabIndex = 482
        Me.FNTotalQA_lbl.Tag = "2|"
        Me.FNTotalQA_lbl.Text = "QC Qty :"
        '
        'FNTotalIn
        '
        Me.FNTotalIn.EnterMoveNextControl = True
        Me.FNTotalIn.Location = New System.Drawing.Point(125, 3)
        Me.FNTotalIn.Name = "FNTotalIn"
        Me.FNTotalIn.Properties.Appearance.Options.UseTextOptions = True
        Me.FNTotalIn.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNTotalIn.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNTotalIn.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNTotalIn.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNTotalIn.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNTotalIn.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNTotalIn.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNTotalIn.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNTotalIn.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNTotalIn.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, True, False, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject4, "", Nothing, Nothing, True)})
        Me.FNTotalIn.Properties.DisplayFormat.FormatString = "{0:n0}"
        Me.FNTotalIn.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNTotalIn.Properties.EditFormat.FormatString = "{0:n0}"
        Me.FNTotalIn.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNTotalIn.Properties.Precision = 0
        Me.FNTotalIn.Properties.ReadOnly = True
        Me.FNTotalIn.Size = New System.Drawing.Size(92, 20)
        Me.FNTotalIn.TabIndex = 481
        Me.FNTotalIn.Tag = "2|"
        '
        'FNTotalIn_lbl
        '
        Me.FNTotalIn_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNTotalIn_lbl.Appearance.Options.UseForeColor = True
        Me.FNTotalIn_lbl.Appearance.Options.UseTextOptions = True
        Me.FNTotalIn_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNTotalIn_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNTotalIn_lbl.Location = New System.Drawing.Point(0, 2)
        Me.FNTotalIn_lbl.Name = "FNTotalIn_lbl"
        Me.FNTotalIn_lbl.Size = New System.Drawing.Size(121, 19)
        Me.FNTotalIn_lbl.TabIndex = 480
        Me.FNTotalIn_lbl.Tag = "2|"
        Me.FNTotalIn_lbl.Text = "Output Qty :"
        '
        'ogcdefectgrp
        '
        Me.ogcdefectgrp.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogcdefectgrp.Location = New System.Drawing.Point(553, 28)
        Me.ogcdefectgrp.MainView = Me.ogvdefectgrp
        Me.ogcdefectgrp.Name = "ogcdefectgrp"
        Me.ogcdefectgrp.Size = New System.Drawing.Size(334, 238)
        Me.ogcdefectgrp.TabIndex = 3
        Me.ogcdefectgrp.TabStop = False
        Me.ogcdefectgrp.Tag = "2|"
        Me.ogcdefectgrp.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvdefectgrp})
        '
        'ogvdefectgrp
        '
        Me.ogvdefectgrp.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.C2FTDefectGrp, Me.C2FNQty, Me.C2FNDfectPerByQA})
        Me.ogvdefectgrp.GridControl = Me.ogcdefectgrp
        Me.ogvdefectgrp.Name = "ogvdefectgrp"
        Me.ogvdefectgrp.OptionsCustomization.AllowGroup = False
        Me.ogvdefectgrp.OptionsCustomization.AllowQuickHideColumns = False
        Me.ogvdefectgrp.OptionsView.AllowCellMerge = True
        Me.ogvdefectgrp.OptionsView.ColumnAutoWidth = False
        Me.ogvdefectgrp.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.ogvdefectgrp.OptionsView.ShowGroupPanel = False
        Me.ogvdefectgrp.SortInfo.AddRange(New DevExpress.XtraGrid.Columns.GridColumnSortInfo() {New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.C2FNQty, DevExpress.Data.ColumnSortOrder.Descending)})
        Me.ogvdefectgrp.Tag = "2|"
        '
        'C2FTDefectGrp
        '
        Me.C2FTDefectGrp.Caption = "Group Code"
        Me.C2FTDefectGrp.FieldName = "FTDefectGrp"
        Me.C2FTDefectGrp.Name = "C2FTDefectGrp"
        Me.C2FTDefectGrp.OptionsColumn.AllowEdit = False
        Me.C2FTDefectGrp.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.C2FTDefectGrp.OptionsColumn.AllowMove = False
        Me.C2FTDefectGrp.OptionsColumn.AllowShowHide = False
        Me.C2FTDefectGrp.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.C2FTDefectGrp.OptionsColumn.FixedWidth = True
        Me.C2FTDefectGrp.OptionsColumn.ReadOnly = True
        Me.C2FTDefectGrp.Visible = True
        Me.C2FTDefectGrp.VisibleIndex = 0
        Me.C2FTDefectGrp.Width = 119
        '
        'C2FNQty
        '
        Me.C2FNQty.Caption = "Quantity"
        Me.C2FNQty.DisplayFormat.FormatString = "{0:n0}"
        Me.C2FNQty.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.C2FNQty.FieldName = "FNQty"
        Me.C2FNQty.Name = "C2FNQty"
        Me.C2FNQty.OptionsColumn.AllowEdit = False
        Me.C2FNQty.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.C2FNQty.OptionsColumn.AllowMove = False
        Me.C2FNQty.OptionsColumn.AllowShowHide = False
        Me.C2FNQty.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.C2FNQty.OptionsColumn.FixedWidth = True
        Me.C2FNQty.OptionsColumn.ReadOnly = True
        Me.C2FNQty.Visible = True
        Me.C2FNQty.VisibleIndex = 1
        Me.C2FNQty.Width = 102
        '
        'C2FNDfectPerByQA
        '
        Me.C2FNDfectPerByQA.Caption = "%"
        Me.C2FNDfectPerByQA.FieldName = "FNDfectPerByQA"
        Me.C2FNDfectPerByQA.Name = "C2FNDfectPerByQA"
        Me.C2FNDfectPerByQA.OptionsColumn.AllowEdit = False
        Me.C2FNDfectPerByQA.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.C2FNDfectPerByQA.OptionsColumn.AllowMove = False
        Me.C2FNDfectPerByQA.OptionsColumn.AllowShowHide = False
        Me.C2FNDfectPerByQA.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.C2FNDfectPerByQA.OptionsColumn.FixedWidth = True
        Me.C2FNDfectPerByQA.OptionsColumn.ReadOnly = True
        Me.C2FNDfectPerByQA.Visible = True
        Me.C2FNDfectPerByQA.VisibleIndex = 2
        Me.C2FNDfectPerByQA.Width = 84
        '
        'ogcdefect
        '
        Me.ogcdefect.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        GridLevelNode1.RelationName = "Level1"
        Me.ogcdefect.LevelTree.Nodes.AddRange(New DevExpress.XtraGrid.GridLevelNode() {GridLevelNode1})
        Me.ogcdefect.Location = New System.Drawing.Point(0, 28)
        Me.ogcdefect.MainView = Me.ogvdefect
        Me.ogcdefect.Name = "ogcdefect"
        Me.ogcdefect.Size = New System.Drawing.Size(548, 238)
        Me.ogcdefect.TabIndex = 2
        Me.ogcdefect.TabStop = False
        Me.ogcdefect.Tag = "2|"
        Me.ogcdefect.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvdefect})
        '
        'ogvdefect
        '
        Me.ogvdefect.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.CCFNSeq, Me.CFTQACode, Me.CFTDefectGrp, Me.CFTDefectName, Me.CFNQty, Me.CFNDfectPerByQA, Me.CFNDfectPerByDefect, Me.CFNDfectPerByDefectSummary})
        Me.ogvdefect.GridControl = Me.ogcdefect
        Me.ogvdefect.Name = "ogvdefect"
        Me.ogvdefect.OptionsCustomization.AllowGroup = False
        Me.ogvdefect.OptionsCustomization.AllowQuickHideColumns = False
        Me.ogvdefect.OptionsView.AllowCellMerge = True
        Me.ogvdefect.OptionsView.ColumnAutoWidth = False
        Me.ogvdefect.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.ogvdefect.OptionsView.ShowGroupPanel = False
        Me.ogvdefect.Tag = "2|"
        '
        'CCFNSeq
        '
        Me.CCFNSeq.Caption = "No."
        Me.CCFNSeq.DisplayFormat.FormatString = "{0:n0}"
        Me.CCFNSeq.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CCFNSeq.FieldName = "FNSeq"
        Me.CCFNSeq.Name = "CCFNSeq"
        Me.CCFNSeq.OptionsColumn.AllowEdit = False
        Me.CCFNSeq.OptionsColumn.FixedWidth = True
        Me.CCFNSeq.OptionsColumn.ReadOnly = True
        Me.CCFNSeq.Visible = True
        Me.CCFNSeq.VisibleIndex = 0
        '
        'CFTQACode
        '
        Me.CFTQACode.Caption = "Code"
        Me.CFTQACode.FieldName = "FTQACode"
        Me.CFTQACode.Name = "CFTQACode"
        Me.CFTQACode.OptionsColumn.AllowEdit = False
        Me.CFTQACode.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFTQACode.OptionsColumn.AllowMove = False
        Me.CFTQACode.OptionsColumn.AllowShowHide = False
        Me.CFTQACode.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFTQACode.OptionsColumn.FixedWidth = True
        Me.CFTQACode.OptionsColumn.ReadOnly = True
        Me.CFTQACode.Visible = True
        Me.CFTQACode.VisibleIndex = 1
        Me.CFTQACode.Width = 124
        '
        'CFTDefectGrp
        '
        Me.CFTDefectGrp.Caption = "Grp."
        Me.CFTDefectGrp.FieldName = "FTDefectGrp"
        Me.CFTDefectGrp.Name = "CFTDefectGrp"
        Me.CFTDefectGrp.OptionsColumn.AllowEdit = False
        Me.CFTDefectGrp.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFTDefectGrp.OptionsColumn.AllowMove = False
        Me.CFTDefectGrp.OptionsColumn.AllowShowHide = False
        Me.CFTDefectGrp.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFTDefectGrp.OptionsColumn.FixedWidth = True
        Me.CFTDefectGrp.OptionsColumn.ReadOnly = True
        Me.CFTDefectGrp.Visible = True
        Me.CFTDefectGrp.VisibleIndex = 2
        '
        'CFTDefectName
        '
        Me.CFTDefectName.Caption = "Defect Name"
        Me.CFTDefectName.FieldName = "FTDefectName"
        Me.CFTDefectName.Name = "CFTDefectName"
        Me.CFTDefectName.OptionsColumn.AllowEdit = False
        Me.CFTDefectName.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFTDefectName.OptionsColumn.AllowMove = False
        Me.CFTDefectName.OptionsColumn.AllowShowHide = False
        Me.CFTDefectName.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFTDefectName.OptionsColumn.ReadOnly = True
        Me.CFTDefectName.Visible = True
        Me.CFTDefectName.VisibleIndex = 3
        Me.CFTDefectName.Width = 183
        '
        'CFNQty
        '
        Me.CFNQty.Caption = "Quantity"
        Me.CFNQty.DisplayFormat.FormatString = "{0:n0}"
        Me.CFNQty.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CFNQty.FieldName = "FNQty"
        Me.CFNQty.Name = "CFNQty"
        Me.CFNQty.OptionsColumn.AllowEdit = False
        Me.CFNQty.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFNQty.OptionsColumn.AllowMove = False
        Me.CFNQty.OptionsColumn.AllowShowHide = False
        Me.CFNQty.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFNQty.OptionsColumn.ReadOnly = True
        Me.CFNQty.Visible = True
        Me.CFNQty.VisibleIndex = 4
        '
        'CFNDfectPerByQA
        '
        Me.CFNDfectPerByQA.Caption = "% Defect"
        Me.CFNDfectPerByQA.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CFNDfectPerByQA.FieldName = "FNDfectPerByQA"
        Me.CFNDfectPerByQA.Name = "CFNDfectPerByQA"
        Me.CFNDfectPerByQA.OptionsColumn.AllowEdit = False
        Me.CFNDfectPerByQA.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFNDfectPerByQA.OptionsColumn.AllowMove = False
        Me.CFNDfectPerByQA.OptionsColumn.AllowShowHide = False
        Me.CFNDfectPerByQA.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFNDfectPerByQA.OptionsColumn.ReadOnly = True
        Me.CFNDfectPerByQA.Visible = True
        Me.CFNDfectPerByQA.VisibleIndex = 5
        Me.CFNDfectPerByQA.Width = 86
        '
        'CFNDfectPerByDefect
        '
        Me.CFNDfectPerByDefect.Caption = "% By Defect"
        Me.CFNDfectPerByDefect.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CFNDfectPerByDefect.FieldName = "FNDfectPerByDefect"
        Me.CFNDfectPerByDefect.Name = "CFNDfectPerByDefect"
        Me.CFNDfectPerByDefect.OptionsColumn.AllowEdit = False
        Me.CFNDfectPerByDefect.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFNDfectPerByDefect.OptionsColumn.AllowMove = False
        Me.CFNDfectPerByDefect.OptionsColumn.AllowShowHide = False
        Me.CFNDfectPerByDefect.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFNDfectPerByDefect.OptionsColumn.ReadOnly = True
        Me.CFNDfectPerByDefect.Visible = True
        Me.CFNDfectPerByDefect.VisibleIndex = 6
        Me.CFNDfectPerByDefect.Width = 106
        '
        'CFNDfectPerByDefectSummary
        '
        Me.CFNDfectPerByDefectSummary.Caption = "% Sum Defect"
        Me.CFNDfectPerByDefectSummary.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CFNDfectPerByDefectSummary.FieldName = "FNDfectPerByDefectSummary"
        Me.CFNDfectPerByDefectSummary.Name = "CFNDfectPerByDefectSummary"
        Me.CFNDfectPerByDefectSummary.OptionsColumn.AllowEdit = False
        Me.CFNDfectPerByDefectSummary.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFNDfectPerByDefectSummary.OptionsColumn.AllowMove = False
        Me.CFNDfectPerByDefectSummary.OptionsColumn.AllowShowHide = False
        Me.CFNDfectPerByDefectSummary.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFNDfectPerByDefectSummary.OptionsColumn.ReadOnly = True
        Me.CFNDfectPerByDefectSummary.Visible = True
        Me.CFNDfectPerByDefectSummary.VisibleIndex = 7
        Me.CFNDfectPerByDefectSummary.Width = 111
        '
        'wDailyQualityAssuranceDefectFinal
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(892, 571)
        Me.Controls.Add(Me.otb)
        Me.Controls.Add(Me.opndata)
        Me.Controls.Add(Me.ogbmainprocbutton)
        Me.Controls.Add(Me.ogbheader)
        Me.Name = "wDailyQualityAssuranceDefectFinal"
        Me.Text = "Daily Quality Assurance Defect Final"
        CType(Me.ocmdoc, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbheader.ResumeLayout(False)
        Me.DockPanel1_Container.ResumeLayout(False)
        CType(Me.FTEDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTEDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTSDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTSDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysCmpId_None.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysCmpId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbmainprocbutton.ResumeLayout(False)
        CType(Me.otb, System.ComponentModel.ISupportInitialize).EndInit()
        Me.otb.ResumeLayout(False)
        Me.otpchart.ResumeLayout(False)
        CType(XyDiagram1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(LineSeriesView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chartControl, System.ComponentModel.ISupportInitialize).EndInit()
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
        Me.otpgrpdefect.ResumeLayout(False)
        CType(XyDiagram2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(LineSeriesView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chartControlgrpdefect, System.ComponentModel.ISupportInitialize).EndInit()
        Me.otpdata.ResumeLayout(False)
        CType(Me.opggrpdefectcode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pivotGridControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.opndata, System.ComponentModel.ISupportInitialize).EndInit()
        Me.opndata.ResumeLayout(False)
        CType(Me.FNTotalQADefectPoint.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNTotalQADefect.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNTotalQA.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNTotalIn.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogcdefectgrp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvdefectgrp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogcdefect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvdefect, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ocmdoc As DevExpress.XtraBars.Docking.DockManager
    Friend WithEvents ogbheader As DevExpress.XtraBars.Docking.DockPanel
    Friend WithEvents DockPanel1_Container As DevExpress.XtraBars.Docking.ControlContainer
    Friend WithEvents FNHSysCmpId_None As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FNHSysCmpId_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTSDate_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysCmpId As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents ogbmainprocbutton As DevExpress.XtraEditors.PanelControl
    Friend WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmload As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FTSDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents otb As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents otpdata As DevExpress.XtraTab.XtraTabPage
    Private WithEvents pivotGridControl As DevExpress.XtraPivotGrid.PivotGridControl
    Friend WithEvents CCFTUnitSectCode As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents CCFTStateData As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents CCFTData As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents otpchart As DevExpress.XtraTab.XtraTabPage
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
    Friend WithEvents FTEDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents FTEDate_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents otpgrpdefect As DevExpress.XtraTab.XtraTabPage
    Private WithEvents chartControlgrpdefect As DevExpress.XtraCharts.ChartControl
    Private WithEvents opggrpdefectcode As DevExpress.XtraPivotGrid.PivotGridControl
    Friend WithEvents PivotGridField1 As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents PivotGridField2 As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents PivotGridField3 As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents opndata As DevExpress.XtraEditors.PanelControl
    Friend WithEvents ogcdefectgrp As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvdefectgrp As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents C2FTDefectGrp As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents C2FNQty As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents C2FNDfectPerByQA As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ogcdefect As DevExpress.XtraGrid.GridControl
    Friend WithEvents CFNSeq As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents ogvdefect As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents CFTQACode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFTDefectGrp As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFTDefectName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFNQty As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFNDfectPerByQA As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFNDfectPerByDefect As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFNDfectPerByDefectSummary As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CCFNSeq As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNTotalIn_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNTotalQADefectPoint As DevExpress.XtraEditors.CalcEdit
    Friend WithEvents FNTotalQADefectPoint_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNTotalQADefect As DevExpress.XtraEditors.CalcEdit
    Friend WithEvents FNTotalQADefect_Lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNTotalQA As DevExpress.XtraEditors.CalcEdit
    Friend WithEvents FNTotalQA_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNTotalIn As DevExpress.XtraEditors.CalcEdit
End Class

Imports DevExpress.XtraEditors.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wHRBIManpower
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(wHRBIManpower))
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim XyDiagram1 As DevExpress.XtraCharts.XYDiagram = New DevExpress.XtraCharts.XYDiagram()
        Dim LineSeriesView1 As DevExpress.XtraCharts.LineSeriesView = New DevExpress.XtraCharts.LineSeriesView()
        Me.ocmdoc = New DevExpress.XtraBars.Docking.DockManager()
        Me.hideContainerTop = New DevExpress.XtraBars.Docking.AutoHideContainer()
        Me.ogbheader = New DevExpress.XtraBars.Docking.DockPanel()
        Me.DockPanel1_Container = New DevExpress.XtraBars.Docking.ControlContainer()
        Me.FNBIManpowerState = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.FNBIManpowerState_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTYearTo = New DevExpress.XtraEditors.DateEdit()
        Me.FTYear = New DevExpress.XtraEditors.DateEdit()
        Me.FNHSysCmpId_None = New DevExpress.XtraEditors.TextEdit()
        Me.FTYearTo_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysCmpId_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTYear_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysCmpId = New DevExpress.XtraEditors.ButtonEdit()
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
        Me.CFTCmpName = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.CFTEmpTypeName = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.CFTDeptName = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.CFTDivisonName = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.CFTSectName = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.CFTUnitSectName = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.CFTEmpTotal = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.CFTPayYear = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.CFNMonth = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.CFTDate = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.CFTEmp = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.CFTEmpNew = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.CFTEmpResign = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.CFTEmpM = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.CFTEmpF = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.chartControl = New DevExpress.XtraCharts.ChartControl()
        Me.ogbmainprocbutton = New DevExpress.XtraEditors.PanelControl()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmload = New DevExpress.XtraEditors.SimpleButton()
        Me.otb = New DevExpress.XtraTab.XtraTabControl()
        Me.otpdata = New DevExpress.XtraTab.XtraTabPage()
        Me.otpchart = New DevExpress.XtraTab.XtraTabPage()
        CType(Me.ocmdoc, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.hideContainerTop.SuspendLayout()
        Me.ogbheader.SuspendLayout()
        Me.DockPanel1_Container.SuspendLayout()
        CType(Me.FNBIManpowerState.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTYearTo.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTYearTo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTYear.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTYear.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysCmpId_None.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysCmpId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.ocmdoc.AutoHideContainers.AddRange(New DevExpress.XtraBars.Docking.AutoHideContainer() {Me.hideContainerTop})
        Me.ocmdoc.Form = Me
        Me.ocmdoc.TopZIndexControls.AddRange(New String() {"DevExpress.XtraBars.BarDockControl", "DevExpress.XtraBars.StandaloneBarDockControl", "System.Windows.Forms.StatusBar", "System.Windows.Forms.MenuStrip", "System.Windows.Forms.StatusStrip", "DevExpress.XtraBars.Ribbon.RibbonStatusBar", "DevExpress.XtraBars.Ribbon.RibbonControl"})
        '
        'hideContainerTop
        '
        Me.hideContainerTop.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.hideContainerTop.Controls.Add(Me.ogbheader)
        Me.hideContainerTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.hideContainerTop.Location = New System.Drawing.Point(0, 0)
        Me.hideContainerTop.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.hideContainerTop.Name = "hideContainerTop"
        Me.hideContainerTop.Size = New System.Drawing.Size(1014, 40)
        '
        'ogbheader
        '
        Me.ogbheader.Controls.Add(Me.DockPanel1_Container)
        Me.ogbheader.Dock = DevExpress.XtraBars.Docking.DockingStyle.Top
        Me.ogbheader.ID = New System.Guid("6a838d1f-4f6f-4734-9f6d-4c809fcfd587")
        Me.ogbheader.Image = CType(resources.GetObject("ogbheader.Image"), System.Drawing.Image)
        Me.ogbheader.Location = New System.Drawing.Point(0, 40)
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
        Me.ogbheader.OriginalSize = New System.Drawing.Size(200, 98)
        Me.ogbheader.SavedDock = DevExpress.XtraBars.Docking.DockingStyle.Top
        Me.ogbheader.SavedIndex = 0
        Me.ogbheader.Size = New System.Drawing.Size(1014, 98)
        Me.ogbheader.Text = "Criteria"
        Me.ogbheader.Visibility = DevExpress.XtraBars.Docking.DockVisibility.AutoHide
        '
        'DockPanel1_Container
        '
        Me.DockPanel1_Container.Controls.Add(Me.FNBIManpowerState)
        Me.DockPanel1_Container.Controls.Add(Me.FNBIManpowerState_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FTYearTo)
        Me.DockPanel1_Container.Controls.Add(Me.FTYear)
        Me.DockPanel1_Container.Controls.Add(Me.FNHSysCmpId_None)
        Me.DockPanel1_Container.Controls.Add(Me.FTYearTo_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FNHSysCmpId_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FTYear_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FNHSysCmpId)
        Me.DockPanel1_Container.Location = New System.Drawing.Point(5, 27)
        Me.DockPanel1_Container.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.DockPanel1_Container.Name = "DockPanel1_Container"
        Me.DockPanel1_Container.Size = New System.Drawing.Size(1004, 64)
        Me.DockPanel1_Container.TabIndex = 0
        '
        'FNBIManpowerState
        '
        Me.FNBIManpowerState.EditValue = ""
        Me.FNBIManpowerState.EnterMoveNextControl = True
        Me.FNBIManpowerState.Location = New System.Drawing.Point(154, 58)
        Me.FNBIManpowerState.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNBIManpowerState.Name = "FNBIManpowerState"
        Me.FNBIManpowerState.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FNBIManpowerState.Properties.Appearance.Options.UseBackColor = True
        Me.FNBIManpowerState.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FNBIManpowerState.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FNBIManpowerState.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FNBIManpowerState.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FNBIManpowerState.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNBIManpowerState.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNBIManpowerState.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNBIManpowerState.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNBIManpowerState.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNBIManpowerState.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNBIManpowerState.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNBIManpowerState.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNBIManpowerState.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FNBIManpowerState.Properties.Tag = "FNBIManpowerState"
        Me.FNBIManpowerState.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.FNBIManpowerState.Size = New System.Drawing.Size(209, 22)
        Me.FNBIManpowerState.TabIndex = 395
        Me.FNBIManpowerState.Tag = "2|"
        '
        'FNBIManpowerState_lbl
        '
        Me.FNBIManpowerState_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNBIManpowerState_lbl.Appearance.Options.UseForeColor = True
        Me.FNBIManpowerState_lbl.Appearance.Options.UseTextOptions = True
        Me.FNBIManpowerState_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNBIManpowerState_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNBIManpowerState_lbl.Location = New System.Drawing.Point(9, 60)
        Me.FNBIManpowerState_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNBIManpowerState_lbl.Name = "FNBIManpowerState_lbl"
        Me.FNBIManpowerState_lbl.Size = New System.Drawing.Size(141, 23)
        Me.FNBIManpowerState_lbl.TabIndex = 480
        Me.FNBIManpowerState_lbl.Tag = "2|"
        Me.FNBIManpowerState_lbl.Text = "รูปแบบการวิเคราะห์ :"
        '
        'FTYearTo
        '
        Me.FTYearTo.EditValue = Nothing
        Me.FTYearTo.EnterMoveNextControl = True
        Me.FTYearTo.Location = New System.Drawing.Point(581, 31)
        Me.FTYearTo.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTYearTo.Name = "FTYearTo"
        Me.FTYearTo.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTYearTo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FTYearTo.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.FTYearTo.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTYearTo.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTYearTo.Properties.NullDate = ""
        Me.FTYearTo.Size = New System.Drawing.Size(132, 22)
        Me.FTYearTo.TabIndex = 396
        Me.FTYearTo.Tag = "2|"
        '
        'FTYear
        '
        Me.FTYear.EditValue = Nothing
        Me.FTYear.EnterMoveNextControl = True
        Me.FTYear.Location = New System.Drawing.Point(154, 31)
        Me.FTYear.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTYear.Name = "FTYear"
        Me.FTYear.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTYear.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FTYear.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.FTYear.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTYear.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTYear.Properties.NullDate = ""
        Me.FTYear.Size = New System.Drawing.Size(132, 22)
        Me.FTYear.TabIndex = 395
        Me.FTYear.Tag = "2|"
        '
        'FNHSysCmpId_None
        '
        Me.FNHSysCmpId_None.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FNHSysCmpId_None.Location = New System.Drawing.Point(287, 4)
        Me.FNHSysCmpId_None.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysCmpId_None.Name = "FNHSysCmpId_None"
        Me.FNHSysCmpId_None.Properties.Appearance.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysCmpId_None.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysCmpId_None.Properties.ReadOnly = True
        Me.FNHSysCmpId_None.Size = New System.Drawing.Size(708, 22)
        Me.FNHSysCmpId_None.TabIndex = 505
        Me.FNHSysCmpId_None.Tag = ""
        '
        'FTYearTo_lbl
        '
        Me.FTYearTo_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTYearTo_lbl.Appearance.Options.UseForeColor = True
        Me.FTYearTo_lbl.Appearance.Options.UseTextOptions = True
        Me.FTYearTo_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTYearTo_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTYearTo_lbl.Location = New System.Drawing.Point(451, 30)
        Me.FTYearTo_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTYearTo_lbl.Name = "FTYearTo_lbl"
        Me.FTYearTo_lbl.Size = New System.Drawing.Size(122, 23)
        Me.FTYearTo_lbl.TabIndex = 480
        Me.FTYearTo_lbl.Tag = "2|"
        Me.FTYearTo_lbl.Text = "To :"
        '
        'FNHSysCmpId_lbl
        '
        Me.FNHSysCmpId_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysCmpId_lbl.Appearance.Options.UseForeColor = True
        Me.FNHSysCmpId_lbl.Appearance.Options.UseTextOptions = True
        Me.FNHSysCmpId_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysCmpId_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysCmpId_lbl.Location = New System.Drawing.Point(7, 4)
        Me.FNHSysCmpId_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysCmpId_lbl.Name = "FNHSysCmpId_lbl"
        Me.FNHSysCmpId_lbl.Size = New System.Drawing.Size(143, 21)
        Me.FNHSysCmpId_lbl.TabIndex = 504
        Me.FNHSysCmpId_lbl.Tag = "2|"
        Me.FNHSysCmpId_lbl.Text = "Company :"
        '
        'FTYear_lbl
        '
        Me.FTYear_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTYear_lbl.Appearance.Options.UseForeColor = True
        Me.FTYear_lbl.Appearance.Options.UseTextOptions = True
        Me.FTYear_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTYear_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTYear_lbl.Location = New System.Drawing.Point(9, 30)
        Me.FTYear_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTYear_lbl.Name = "FTYear_lbl"
        Me.FTYear_lbl.Size = New System.Drawing.Size(141, 23)
        Me.FTYear_lbl.TabIndex = 479
        Me.FTYear_lbl.Tag = "2|"
        Me.FTYear_lbl.Text = "Start :"
        '
        'FNHSysCmpId
        '
        Me.FNHSysCmpId.Location = New System.Drawing.Point(154, 4)
        Me.FNHSysCmpId.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysCmpId.Name = "FNHSysCmpId"
        Me.FNHSysCmpId.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysCmpId.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysCmpId.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNHSysCmpId.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNHSysCmpId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, False, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", "11", Nothing, True)})
        Me.FNHSysCmpId.Properties.ReadOnly = True
        Me.FNHSysCmpId.Properties.Tag = ""
        Me.FNHSysCmpId.Size = New System.Drawing.Size(132, 22)
        Me.FNHSysCmpId.TabIndex = 503
        Me.FNHSysCmpId.Tag = ""
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
        Me.ceSelectionOnly.ToolTip = "Toggles whether all PivotGrid cells or selected cells only should be represented " &
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
        Me.ceChartDataVertical.ToolTip = "Toggles whether series in a chart control are created based on PivotGrid columns " &
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
        'pivotGridControl
        '
        Me.pivotGridControl.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.pivotGridControl.Cursor = System.Windows.Forms.Cursors.Default
        Me.pivotGridControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pivotGridControl.Fields.AddRange(New DevExpress.XtraPivotGrid.PivotGridField() {Me.CFTCmpName, Me.CFTEmpTypeName, Me.CFTDeptName, Me.CFTDivisonName, Me.CFTSectName, Me.CFTUnitSectName, Me.CFTEmpTotal, Me.CFTPayYear, Me.CFNMonth, Me.CFTDate, Me.CFTEmp, Me.CFTEmpNew, Me.CFTEmpResign, Me.CFTEmpM, Me.CFTEmpF})
        Me.pivotGridControl.Location = New System.Drawing.Point(0, 0)
        Me.pivotGridControl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.pivotGridControl.Name = "pivotGridControl"
        Me.pivotGridControl.OptionsChartDataSource.DataProvideMode = DevExpress.XtraPivotGrid.PivotChartDataProvideMode.UseCustomSettings
        Me.pivotGridControl.OptionsChartDataSource.ProvideDataByColumns = False
        Me.pivotGridControl.OptionsChartDataSource.UpdateDelay = 500
        Me.pivotGridControl.OptionsView.ShowColumnGrandTotalHeader = False
        Me.pivotGridControl.OptionsView.ShowColumnGrandTotals = False
        Me.pivotGridControl.OptionsView.ShowColumnTotals = False
        Me.pivotGridControl.Size = New System.Drawing.Size(1007, 629)
        Me.pivotGridControl.TabIndex = 2
        '
        'CFTCmpName
        '
        Me.CFTCmpName.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea
        Me.CFTCmpName.AreaIndex = 0
        Me.CFTCmpName.Caption = "FTCmpName"
        Me.CFTCmpName.FieldName = "FTCmpName"
        Me.CFTCmpName.Name = "CFTCmpName"
        Me.CFTCmpName.Width = 155
        '
        'CFTEmpTypeName
        '
        Me.CFTEmpTypeName.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea
        Me.CFTEmpTypeName.AreaIndex = 1
        Me.CFTEmpTypeName.Caption = "FTEmpTypeName"
        Me.CFTEmpTypeName.FieldName = "FTEmpTypeName"
        Me.CFTEmpTypeName.Name = "CFTEmpTypeName"
        '
        'CFTDeptName
        '
        Me.CFTDeptName.AreaIndex = 0
        Me.CFTDeptName.Caption = "FTDeptName"
        Me.CFTDeptName.FieldName = "FTDeptName"
        Me.CFTDeptName.Name = "CFTDeptName"
        '
        'CFTDivisonName
        '
        Me.CFTDivisonName.AreaIndex = 1
        Me.CFTDivisonName.Caption = "FTDivisonName"
        Me.CFTDivisonName.FieldName = "FTDivisonName"
        Me.CFTDivisonName.Name = "CFTDivisonName"
        '
        'CFTSectName
        '
        Me.CFTSectName.AreaIndex = 2
        Me.CFTSectName.Caption = "FTSectName"
        Me.CFTSectName.FieldName = "FTSectName"
        Me.CFTSectName.Name = "CFTSectName"
        '
        'CFTUnitSectName
        '
        Me.CFTUnitSectName.AreaIndex = 3
        Me.CFTUnitSectName.Caption = "FTUnitSectName"
        Me.CFTUnitSectName.FieldName = "FTUnitSectName"
        Me.CFTUnitSectName.Name = "CFTUnitSectName"
        '
        'CFTEmpTotal
        '
        Me.CFTEmpTotal.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
        Me.CFTEmpTotal.AreaIndex = 3
        Me.CFTEmpTotal.CellFormat.FormatString = "{0:n0}"
        Me.CFTEmpTotal.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CFTEmpTotal.FieldName = "FTEmpTotal"
        Me.CFTEmpTotal.Name = "CFTEmpTotal"
        Me.CFTEmpTotal.Width = 80
        '
        'CFTPayYear
        '
        Me.CFTPayYear.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea
        Me.CFTPayYear.AreaIndex = 0
        Me.CFTPayYear.Caption = "FTYear"
        Me.CFTPayYear.FieldName = "FTYear"
        Me.CFTPayYear.Name = "CFTPayYear"
        Me.CFTPayYear.UnboundFieldName = "CFTPayYear"
        '
        'CFNMonth
        '
        Me.CFNMonth.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea
        Me.CFNMonth.AreaIndex = 1
        Me.CFNMonth.Caption = "FNMonth"
        Me.CFNMonth.FieldName = "FNMonth"
        Me.CFNMonth.Name = "CFNMonth"
        Me.CFNMonth.UnboundFieldName = "CFNMonth"
        '
        'CFTDate
        '
        Me.CFTDate.AreaIndex = 4
        Me.CFTDate.Caption = "FTDate"
        Me.CFTDate.FieldName = "FTDate"
        Me.CFTDate.Name = "CFTDate"
        '
        'CFTEmp
        '
        Me.CFTEmp.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
        Me.CFTEmp.AreaIndex = 0
        Me.CFTEmp.CellFormat.FormatString = "{0:n0}"
        Me.CFTEmp.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CFTEmp.FieldName = "FTEmp"
        Me.CFTEmp.Name = "CFTEmp"
        Me.CFTEmp.Width = 66
        '
        'CFTEmpNew
        '
        Me.CFTEmpNew.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
        Me.CFTEmpNew.AreaIndex = 1
        Me.CFTEmpNew.Caption = "FTEmpNew"
        Me.CFTEmpNew.CellFormat.FormatString = "{0:n0}"
        Me.CFTEmpNew.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CFTEmpNew.FieldName = "FTEmpNew"
        Me.CFTEmpNew.Name = "CFTEmpNew"
        Me.CFTEmpNew.Width = 67
        '
        'CFTEmpResign
        '
        Me.CFTEmpResign.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
        Me.CFTEmpResign.AreaIndex = 2
        Me.CFTEmpResign.Caption = "FTEmpResign"
        Me.CFTEmpResign.CellFormat.FormatString = "{0:n0}"
        Me.CFTEmpResign.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CFTEmpResign.FieldName = "FTEmpResign"
        Me.CFTEmpResign.Name = "CFTEmpResign"
        Me.CFTEmpResign.Width = 75
        '
        'CFTEmpM
        '
        Me.CFTEmpM.AreaIndex = 6
        Me.CFTEmpM.Caption = "FTEmpM"
        Me.CFTEmpM.CellFormat.FormatString = "{0:n0}"
        Me.CFTEmpM.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CFTEmpM.FieldName = "FTEmpM"
        Me.CFTEmpM.Name = "CFTEmpM"
        '
        'CFTEmpF
        '
        Me.CFTEmpF.AreaIndex = 5
        Me.CFTEmpF.Caption = "FTEmpF"
        Me.CFTEmpF.CellFormat.FormatString = "{0:n0}"
        Me.CFTEmpF.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CFTEmpF.FieldName = "FTEmpF"
        Me.CFTEmpF.Name = "CFTEmpF"
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
        Me.chartControl.Size = New System.Drawing.Size(1007, 561)
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
        Me.otb.Location = New System.Drawing.Point(0, 40)
        Me.otb.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.otb.Name = "otb"
        Me.otb.SelectedTabPage = Me.otpdata
        Me.otb.Size = New System.Drawing.Size(1014, 663)
        Me.otb.TabIndex = 394
        Me.otb.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.otpdata, Me.otpchart})
        '
        'otpdata
        '
        Me.otpdata.Controls.Add(Me.pivotGridControl)
        Me.otpdata.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.otpdata.Name = "otpdata"
        Me.otpdata.Size = New System.Drawing.Size(1007, 629)
        Me.otpdata.Text = "Data"
        '
        'otpchart
        '
        Me.otpchart.Controls.Add(Me.chartControl)
        Me.otpchart.Controls.Add(Me.panelControl1)
        Me.otpchart.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.otpchart.Name = "otpchart"
        Me.otpchart.Size = New System.Drawing.Size(1007, 631)
        Me.otpchart.Text = "Chart"
        '
        'wHRBIManpower
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1014, 703)
        Me.Controls.Add(Me.otb)
        Me.Controls.Add(Me.ogbmainprocbutton)
        Me.Controls.Add(Me.hideContainerTop)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "wHRBIManpower"
        Me.Text = "HR BI"
        CType(Me.ocmdoc, System.ComponentModel.ISupportInitialize).EndInit()
        Me.hideContainerTop.ResumeLayout(False)
        Me.ogbheader.ResumeLayout(False)
        Me.DockPanel1_Container.ResumeLayout(False)
        CType(Me.FNBIManpowerState.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTYearTo.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTYearTo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTYear.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTYear.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysCmpId_None.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysCmpId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents FNHSysCmpId_None As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FTYearTo_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysCmpId_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTYear_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysCmpId As DevExpress.XtraEditors.ButtonEdit
    Private WithEvents pivotGridControl As DevExpress.XtraPivotGrid.PivotGridControl
    Private WithEvents CFTCmpName As DevExpress.XtraPivotGrid.PivotGridField
    Private WithEvents CFTEmpTotal As DevExpress.XtraPivotGrid.PivotGridField
    Private WithEvents CFNMonth As DevExpress.XtraPivotGrid.PivotGridField
    Private WithEvents CFTPayYear As DevExpress.XtraPivotGrid.PivotGridField
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
    Friend WithEvents CFTEmpTypeName As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents CFTDeptName As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents CFTDivisonName As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents CFTSectName As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents CFTUnitSectName As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents ogbmainprocbutton As DevExpress.XtraEditors.PanelControl
    Friend WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmload As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents otb As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents otpdata As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents otpchart As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents FTYearTo As DevExpress.XtraEditors.DateEdit
    Friend WithEvents FTYear As DevExpress.XtraEditors.DateEdit
    Friend WithEvents CFTDate As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents CFTEmp As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents CFTEmpNew As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents CFTEmpResign As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents CFTEmpM As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents CFTEmpF As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents FNBIManpowerState_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNBIManpowerState As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents hideContainerTop As DevExpress.XtraBars.Docking.AutoHideContainer
End Class

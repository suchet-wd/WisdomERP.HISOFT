Imports DevExpress.XtraEditors.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wMERBIOrderSummary
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(wMERBIOrderSummary))
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim XyDiagram1 As DevExpress.XtraCharts.XYDiagram = New DevExpress.XtraCharts.XYDiagram()
        Dim LineSeriesView1 As DevExpress.XtraCharts.LineSeriesView = New DevExpress.XtraCharts.LineSeriesView()
        Me.ocmdoc = New DevExpress.XtraBars.Docking.DockManager()
        Me.ogbheader = New DevExpress.XtraBars.Docking.DockPanel()
        Me.DockPanel1_Container = New DevExpress.XtraBars.Docking.ControlContainer()
        Me.FTEndCreateDate = New DevExpress.XtraEditors.DateEdit()
        Me.FNHSysCustId_None = New DevExpress.XtraEditors.TextEdit()
        Me.FTEndCreateDate_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTStartCreateDate = New DevExpress.XtraEditors.DateEdit()
        Me.FTEndOrderDate = New DevExpress.XtraEditors.DateEdit()
        Me.FTStartCreateDate_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysCustId_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysCmpId_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysCustId = New DevExpress.XtraEditors.ButtonEdit()
        Me.FTEndOrderDate_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTStartOrderDate = New DevExpress.XtraEditors.DateEdit()
        Me.FTStartOrderDate_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTEndShipment = New DevExpress.XtraEditors.DateEdit()
        Me.FTEndShipment_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTStartShipment = New DevExpress.XtraEditors.DateEdit()
        Me.FNHSysCmpId = New DevExpress.XtraEditors.ButtonEdit()
        Me.FTStartShipment_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysCmpId_None = New DevExpress.XtraEditors.TextEdit()
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
        Me.CFTCmpCode = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.CFTCmpName = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.CFTCustCode = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.CFTCustName = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.CFTCountryCode = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.CFTCountryName = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.CFTPORef = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.CFTStyleCode = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.CFTStyleName = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.CFTCurCode = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.CFDShipDate = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.CFNPrice = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.CFNQuantity = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.CFNOrderAmt = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.CFNQuantityExtra = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.CFNOrderExtraAmt = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.CFNGarmentQtyTest = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.CFNOrderTestAmt = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.CFNOrderGrandQuantity = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.CFNOrderGrandAmt = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.CFDOrderDate = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.CFTInsUser = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.CFDOrderYear = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.CFNYearTerm = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.CFDOrderMonth = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.CFDOrderShipYear = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.CFDOrderShiptMonth = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.CFTPlantCode = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.CFTPlantName = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.C3FTSeasonCode = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.C3FTOrderTypeName = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.PXFTNikePOLineItem = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.chartControl = New DevExpress.XtraCharts.ChartControl()
        Me.otb = New DevExpress.XtraTab.XtraTabControl()
        Me.otpsummary = New DevExpress.XtraTab.XtraTabPage()
        Me.ogc = New DevExpress.XtraGrid.GridControl()
        Me.ogv = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.FTOrderNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CCFTSeasonCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CCFTOrderTypeName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTCmpCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTCmpName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTCustCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTCustName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTCountryCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTCountryName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTPlantCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTPlantName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTPORef = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFTNikePOLineItem = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTStyleCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTStyleName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FDShipDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTCurCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNPrice = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNOrderAmt = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNQuantityExtra = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNOrderExtraAmt = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNGarmentQtyTest = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNOrderTestAmt = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNOrderGrandQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNOrderGrandAmt = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FDOrderDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTInsUser = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FDOrderYear = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FDOrderMonth = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FDOrderShipYear = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FDOrderShiptMonth = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNYearTerm = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.otpdata = New DevExpress.XtraTab.XtraTabPage()
        Me.otpchart = New DevExpress.XtraTab.XtraTabPage()
        Me.ogbmainprocbutton = New DevExpress.XtraEditors.PanelControl()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmload = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.ocmdoc, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbheader.SuspendLayout()
        Me.DockPanel1_Container.SuspendLayout()
        CType(Me.FTEndCreateDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTEndCreateDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysCustId_None.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTStartCreateDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTStartCreateDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTEndOrderDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTEndOrderDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysCustId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTStartOrderDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTStartOrderDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTEndShipment.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTEndShipment.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTStartShipment.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTStartShipment.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysCmpId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysCmpId_None.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
        CType(Me.otb, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.otb.SuspendLayout()
        Me.otpsummary.SuspendLayout()
        CType(Me.ogc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogv, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.otpdata.SuspendLayout()
        Me.otpchart.SuspendLayout()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbmainprocbutton.SuspendLayout()
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
        Me.ogbheader.OriginalSize = New System.Drawing.Size(200, 143)
        Me.ogbheader.Size = New System.Drawing.Size(1100, 143)
        Me.ogbheader.Text = "Criteria"
        '
        'DockPanel1_Container
        '
        Me.DockPanel1_Container.Controls.Add(Me.FTEndCreateDate)
        Me.DockPanel1_Container.Controls.Add(Me.FNHSysCustId_None)
        Me.DockPanel1_Container.Controls.Add(Me.FTEndCreateDate_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FTStartCreateDate)
        Me.DockPanel1_Container.Controls.Add(Me.FTEndOrderDate)
        Me.DockPanel1_Container.Controls.Add(Me.FTStartCreateDate_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FNHSysCustId_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FNHSysCmpId_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FNHSysCustId)
        Me.DockPanel1_Container.Controls.Add(Me.FTEndOrderDate_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FTStartOrderDate)
        Me.DockPanel1_Container.Controls.Add(Me.FTStartOrderDate_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FTEndShipment)
        Me.DockPanel1_Container.Controls.Add(Me.FTEndShipment_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FTStartShipment)
        Me.DockPanel1_Container.Controls.Add(Me.FNHSysCmpId)
        Me.DockPanel1_Container.Controls.Add(Me.FTStartShipment_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FNHSysCmpId_None)
        Me.DockPanel1_Container.Location = New System.Drawing.Point(4, 23)
        Me.DockPanel1_Container.Name = "DockPanel1_Container"
        Me.DockPanel1_Container.Size = New System.Drawing.Size(1092, 115)
        Me.DockPanel1_Container.TabIndex = 0
        '
        'FTEndCreateDate
        '
        Me.FTEndCreateDate.EditValue = Nothing
        Me.FTEndCreateDate.EnterMoveNextControl = True
        Me.FTEndCreateDate.Location = New System.Drawing.Point(701, 67)
        Me.FTEndCreateDate.Name = "FTEndCreateDate"
        Me.FTEndCreateDate.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTEndCreateDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FTEndCreateDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.FTEndCreateDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTEndCreateDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTEndCreateDate.Properties.NullDate = ""
        Me.FTEndCreateDate.Size = New System.Drawing.Size(130, 20)
        Me.FTEndCreateDate.TabIndex = 419
        Me.FTEndCreateDate.Tag = "2|"
        '
        'FNHSysCustId_None
        '
        Me.FNHSysCustId_None.Location = New System.Drawing.Point(834, 2)
        Me.FNHSysCustId_None.Name = "FNHSysCustId_None"
        Me.FNHSysCustId_None.Properties.Appearance.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysCustId_None.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysCustId_None.Properties.ReadOnly = True
        Me.FNHSysCustId_None.Size = New System.Drawing.Size(237, 20)
        Me.FNHSysCustId_None.TabIndex = 401
        Me.FNHSysCustId_None.Tag = "2|"
        '
        'FTEndCreateDate_lbl
        '
        Me.FTEndCreateDate_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTEndCreateDate_lbl.Appearance.Options.UseForeColor = True
        Me.FTEndCreateDate_lbl.Appearance.Options.UseTextOptions = True
        Me.FTEndCreateDate_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTEndCreateDate_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTEndCreateDate_lbl.Location = New System.Drawing.Point(559, 66)
        Me.FTEndCreateDate_lbl.Name = "FTEndCreateDate_lbl"
        Me.FTEndCreateDate_lbl.Size = New System.Drawing.Size(140, 19)
        Me.FTEndCreateDate_lbl.TabIndex = 420
        Me.FTEndCreateDate_lbl.Tag = "2|"
        Me.FTEndCreateDate_lbl.Text = "End Create Date :"
        '
        'FTStartCreateDate
        '
        Me.FTStartCreateDate.EditValue = Nothing
        Me.FTStartCreateDate.EnterMoveNextControl = True
        Me.FTStartCreateDate.Location = New System.Drawing.Point(166, 67)
        Me.FTStartCreateDate.Name = "FTStartCreateDate"
        Me.FTStartCreateDate.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTStartCreateDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FTStartCreateDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.FTStartCreateDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTStartCreateDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTStartCreateDate.Properties.NullDate = ""
        Me.FTStartCreateDate.Size = New System.Drawing.Size(130, 20)
        Me.FTStartCreateDate.TabIndex = 417
        Me.FTStartCreateDate.Tag = "2|"
        '
        'FTEndOrderDate
        '
        Me.FTEndOrderDate.EditValue = Nothing
        Me.FTEndOrderDate.EnterMoveNextControl = True
        Me.FTEndOrderDate.Location = New System.Drawing.Point(701, 24)
        Me.FTEndOrderDate.Name = "FTEndOrderDate"
        Me.FTEndOrderDate.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTEndOrderDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FTEndOrderDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.FTEndOrderDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTEndOrderDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTEndOrderDate.Properties.NullDate = ""
        Me.FTEndOrderDate.Size = New System.Drawing.Size(130, 20)
        Me.FTEndOrderDate.TabIndex = 419
        Me.FTEndOrderDate.Tag = "2|"
        '
        'FTStartCreateDate_lbl
        '
        Me.FTStartCreateDate_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTStartCreateDate_lbl.Appearance.Options.UseForeColor = True
        Me.FTStartCreateDate_lbl.Appearance.Options.UseTextOptions = True
        Me.FTStartCreateDate_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTStartCreateDate_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTStartCreateDate_lbl.Location = New System.Drawing.Point(21, 66)
        Me.FTStartCreateDate_lbl.Name = "FTStartCreateDate_lbl"
        Me.FTStartCreateDate_lbl.Size = New System.Drawing.Size(139, 19)
        Me.FTStartCreateDate_lbl.TabIndex = 418
        Me.FTStartCreateDate_lbl.Tag = "2|"
        Me.FTStartCreateDate_lbl.Text = "Start Create Date :"
        '
        'FNHSysCustId_lbl
        '
        Me.FNHSysCustId_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FNHSysCustId_lbl.Appearance.Options.UseForeColor = True
        Me.FNHSysCustId_lbl.Appearance.Options.UseTextOptions = True
        Me.FNHSysCustId_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysCustId_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysCustId_lbl.Location = New System.Drawing.Point(539, 2)
        Me.FNHSysCustId_lbl.Name = "FNHSysCustId_lbl"
        Me.FNHSysCustId_lbl.Size = New System.Drawing.Size(156, 20)
        Me.FNHSysCustId_lbl.TabIndex = 399
        Me.FNHSysCustId_lbl.Tag = "2|"
        Me.FNHSysCustId_lbl.Text = "Customer :"
        '
        'FNHSysCmpId_lbl
        '
        Me.FNHSysCmpId_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FNHSysCmpId_lbl.Appearance.Options.UseForeColor = True
        Me.FNHSysCmpId_lbl.Appearance.Options.UseTextOptions = True
        Me.FNHSysCmpId_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysCmpId_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysCmpId_lbl.Location = New System.Drawing.Point(4, 3)
        Me.FNHSysCmpId_lbl.Name = "FNHSysCmpId_lbl"
        Me.FNHSysCmpId_lbl.Size = New System.Drawing.Size(156, 20)
        Me.FNHSysCmpId_lbl.TabIndex = 399
        Me.FNHSysCmpId_lbl.Tag = "2|"
        Me.FNHSysCmpId_lbl.Text = "Company :"
        '
        'FNHSysCustId
        '
        Me.FNHSysCustId.Location = New System.Drawing.Point(701, 2)
        Me.FNHSysCustId.Name = "FNHSysCustId"
        Me.FNHSysCustId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", "83", Nothing, True)})
        Me.FNHSysCustId.Properties.Tag = ""
        Me.FNHSysCustId.Size = New System.Drawing.Size(130, 20)
        Me.FNHSysCustId.TabIndex = 400
        Me.FNHSysCustId.Tag = "2|"
        '
        'FTEndOrderDate_lbl
        '
        Me.FTEndOrderDate_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTEndOrderDate_lbl.Appearance.Options.UseForeColor = True
        Me.FTEndOrderDate_lbl.Appearance.Options.UseTextOptions = True
        Me.FTEndOrderDate_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTEndOrderDate_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTEndOrderDate_lbl.Location = New System.Drawing.Point(559, 23)
        Me.FTEndOrderDate_lbl.Name = "FTEndOrderDate_lbl"
        Me.FTEndOrderDate_lbl.Size = New System.Drawing.Size(140, 19)
        Me.FTEndOrderDate_lbl.TabIndex = 420
        Me.FTEndOrderDate_lbl.Tag = "2|"
        Me.FTEndOrderDate_lbl.Text = "End Order Date:"
        '
        'FTStartOrderDate
        '
        Me.FTStartOrderDate.EditValue = Nothing
        Me.FTStartOrderDate.EnterMoveNextControl = True
        Me.FTStartOrderDate.Location = New System.Drawing.Point(166, 24)
        Me.FTStartOrderDate.Name = "FTStartOrderDate"
        Me.FTStartOrderDate.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTStartOrderDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FTStartOrderDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.FTStartOrderDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTStartOrderDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTStartOrderDate.Properties.NullDate = ""
        Me.FTStartOrderDate.Size = New System.Drawing.Size(130, 20)
        Me.FTStartOrderDate.TabIndex = 417
        Me.FTStartOrderDate.Tag = "2|"
        '
        'FTStartOrderDate_lbl
        '
        Me.FTStartOrderDate_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTStartOrderDate_lbl.Appearance.Options.UseForeColor = True
        Me.FTStartOrderDate_lbl.Appearance.Options.UseTextOptions = True
        Me.FTStartOrderDate_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTStartOrderDate_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTStartOrderDate_lbl.Location = New System.Drawing.Point(21, 23)
        Me.FTStartOrderDate_lbl.Name = "FTStartOrderDate_lbl"
        Me.FTStartOrderDate_lbl.Size = New System.Drawing.Size(139, 19)
        Me.FTStartOrderDate_lbl.TabIndex = 418
        Me.FTStartOrderDate_lbl.Tag = "2|"
        Me.FTStartOrderDate_lbl.Text = "Start OrderDate :"
        '
        'FTEndShipment
        '
        Me.FTEndShipment.EditValue = Nothing
        Me.FTEndShipment.EnterMoveNextControl = True
        Me.FTEndShipment.Location = New System.Drawing.Point(701, 44)
        Me.FTEndShipment.Name = "FTEndShipment"
        Me.FTEndShipment.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTEndShipment.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FTEndShipment.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.FTEndShipment.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTEndShipment.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTEndShipment.Properties.NullDate = ""
        Me.FTEndShipment.Size = New System.Drawing.Size(130, 20)
        Me.FTEndShipment.TabIndex = 415
        Me.FTEndShipment.Tag = "2|"
        '
        'FTEndShipment_lbl
        '
        Me.FTEndShipment_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTEndShipment_lbl.Appearance.Options.UseForeColor = True
        Me.FTEndShipment_lbl.Appearance.Options.UseTextOptions = True
        Me.FTEndShipment_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTEndShipment_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTEndShipment_lbl.Location = New System.Drawing.Point(559, 43)
        Me.FTEndShipment_lbl.Name = "FTEndShipment_lbl"
        Me.FTEndShipment_lbl.Size = New System.Drawing.Size(140, 19)
        Me.FTEndShipment_lbl.TabIndex = 416
        Me.FTEndShipment_lbl.Tag = "2|"
        Me.FTEndShipment_lbl.Text = "End Shipment:"
        '
        'FTStartShipment
        '
        Me.FTStartShipment.EditValue = Nothing
        Me.FTStartShipment.EnterMoveNextControl = True
        Me.FTStartShipment.Location = New System.Drawing.Point(166, 44)
        Me.FTStartShipment.Name = "FTStartShipment"
        Me.FTStartShipment.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTStartShipment.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FTStartShipment.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.FTStartShipment.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTStartShipment.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTStartShipment.Properties.NullDate = ""
        Me.FTStartShipment.Size = New System.Drawing.Size(130, 20)
        Me.FTStartShipment.TabIndex = 413
        Me.FTStartShipment.Tag = "2|"
        '
        'FNHSysCmpId
        '
        Me.FNHSysCmpId.Location = New System.Drawing.Point(166, 3)
        Me.FNHSysCmpId.Name = "FNHSysCmpId"
        Me.FNHSysCmpId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject2, "", "11", Nothing, True)})
        Me.FNHSysCmpId.Properties.Tag = ""
        Me.FNHSysCmpId.Size = New System.Drawing.Size(130, 20)
        Me.FNHSysCmpId.TabIndex = 400
        Me.FNHSysCmpId.Tag = "2|"
        '
        'FTStartShipment_lbl
        '
        Me.FTStartShipment_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTStartShipment_lbl.Appearance.Options.UseForeColor = True
        Me.FTStartShipment_lbl.Appearance.Options.UseTextOptions = True
        Me.FTStartShipment_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTStartShipment_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTStartShipment_lbl.Location = New System.Drawing.Point(21, 43)
        Me.FTStartShipment_lbl.Name = "FTStartShipment_lbl"
        Me.FTStartShipment_lbl.Size = New System.Drawing.Size(139, 19)
        Me.FTStartShipment_lbl.TabIndex = 414
        Me.FTStartShipment_lbl.Tag = "2|"
        Me.FTStartShipment_lbl.Text = "Start Shipment:"
        '
        'FNHSysCmpId_None
        '
        Me.FNHSysCmpId_None.Location = New System.Drawing.Point(298, 3)
        Me.FNHSysCmpId_None.Name = "FNHSysCmpId_None"
        Me.FNHSysCmpId_None.Properties.Appearance.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysCmpId_None.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysCmpId_None.Properties.ReadOnly = True
        Me.FNHSysCmpId_None.Size = New System.Drawing.Size(222, 20)
        Me.FNHSysCmpId_None.TabIndex = 401
        Me.FNHSysCmpId_None.Tag = "2|"
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
        Me.panelControl1.Padding = New System.Windows.Forms.Padding(5)
        Me.panelControl1.Size = New System.Drawing.Size(1094, 57)
        Me.panelControl1.TabIndex = 3
        '
        'seUpdateDelay
        '
        Me.seUpdateDelay.EditValue = New Decimal(New Integer() {500, 0, 0, 0})
        Me.seUpdateDelay.Location = New System.Drawing.Point(251, 32)
        Me.seUpdateDelay.Name = "seUpdateDelay"
        Me.seUpdateDelay.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.seUpdateDelay.Properties.Increment = New Decimal(New Integer() {100, 0, 0, 0})
        Me.seUpdateDelay.Properties.IsFloatValue = False
        Me.seUpdateDelay.Properties.Mask.EditMask = "N00"
        Me.seUpdateDelay.Properties.MaxValue = New Decimal(New Integer() {5000, 0, 0, 0})
        Me.seUpdateDelay.Size = New System.Drawing.Size(48, 20)
        Me.seUpdateDelay.TabIndex = 10
        Me.seUpdateDelay.ToolTip = "Sets the Chart update delay when PivotGrid selection changes."
        '
        'lblUpdateDelay
        '
        Me.lblUpdateDelay.Location = New System.Drawing.Point(174, 35)
        Me.lblUpdateDelay.Name = "lblUpdateDelay"
        Me.lblUpdateDelay.Size = New System.Drawing.Size(69, 13)
        Me.lblUpdateDelay.TabIndex = 13
        Me.lblUpdateDelay.Text = "Update Delay:"
        '
        'ceShowRowGrandTotals
        '
        Me.ceShowRowGrandTotals.EditValue = True
        Me.ceShowRowGrandTotals.Location = New System.Drawing.Point(479, 10)
        Me.ceShowRowGrandTotals.Name = "ceShowRowGrandTotals"
        Me.ceShowRowGrandTotals.Properties.AutoWidth = True
        Me.ceShowRowGrandTotals.Properties.Caption = "Show Row Grand Totals"
        Me.ceShowRowGrandTotals.Size = New System.Drawing.Size(136, 19)
        Me.ceShowRowGrandTotals.TabIndex = 7
        Me.ceShowRowGrandTotals.ToolTip = "Toggles whether row grand total values are shown in the Chart control"
        '
        'ceShowColumnGrandTotals
        '
        Me.ceShowColumnGrandTotals.Location = New System.Drawing.Point(479, 32)
        Me.ceShowColumnGrandTotals.Name = "ceShowColumnGrandTotals"
        Me.ceShowColumnGrandTotals.Properties.AutoWidth = True
        Me.ceShowColumnGrandTotals.Properties.Caption = "Show Column Grand Totals"
        Me.ceShowColumnGrandTotals.Size = New System.Drawing.Size(150, 19)
        Me.ceShowColumnGrandTotals.TabIndex = 13
        Me.ceShowColumnGrandTotals.ToolTip = "Toggles whether column grand total values are shown in the Chart control"
        '
        'ceSelectionOnly
        '
        Me.ceSelectionOnly.Location = New System.Drawing.Point(10, 32)
        Me.ceSelectionOnly.Name = "ceSelectionOnly"
        Me.ceSelectionOnly.Properties.AutoWidth = True
        Me.ceSelectionOnly.Properties.Caption = "Selection Only"
        Me.ceSelectionOnly.Size = New System.Drawing.Size(90, 19)
        Me.ceSelectionOnly.TabIndex = 9
        Me.ceSelectionOnly.ToolTip = "Toggles whether all PivotGrid cells or selected cells only should be represented " &
    "in the Chart"
        '
        'ceChartDataVertical
        '
        Me.ceChartDataVertical.Location = New System.Drawing.Point(305, 32)
        Me.ceChartDataVertical.Name = "ceChartDataVertical"
        Me.ceChartDataVertical.Properties.AutoWidth = True
        Me.ceChartDataVertical.Properties.Caption = "Generate Series from Columns"
        Me.ceChartDataVertical.Size = New System.Drawing.Size(167, 19)
        Me.ceChartDataVertical.TabIndex = 12
        Me.ceChartDataVertical.ToolTip = "Toggles whether series in a chart control are created based on PivotGrid columns " &
    "or rows"
        '
        'checkShowPointLabels
        '
        Me.checkShowPointLabels.Location = New System.Drawing.Point(305, 10)
        Me.checkShowPointLabels.Name = "checkShowPointLabels"
        Me.checkShowPointLabels.Properties.AutoWidth = True
        Me.checkShowPointLabels.Properties.Caption = "Show Point Labels"
        Me.checkShowPointLabels.Size = New System.Drawing.Size(108, 19)
        Me.checkShowPointLabels.TabIndex = 4
        Me.checkShowPointLabels.ToolTip = "Toggles whether value labels are shown in the Chart control"
        '
        'comboChartType
        '
        Me.comboChartType.EditValue = "Line"
        Me.comboChartType.Location = New System.Drawing.Point(145, 10)
        Me.comboChartType.Name = "comboChartType"
        Me.comboChartType.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.comboChartType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.comboChartType.Size = New System.Drawing.Size(154, 20)
        Me.comboChartType.TabIndex = 3
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Options.UseTextOptions = True
        Me.LabelControl1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LabelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LabelControl1.Location = New System.Drawing.Point(14, 12)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(125, 17)
        Me.LabelControl1.TabIndex = 2
        Me.LabelControl1.Text = "Chart Type:"
        '
        'pivotGridControl
        '
        Me.pivotGridControl.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.pivotGridControl.Cursor = System.Windows.Forms.Cursors.Default
        Me.pivotGridControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pivotGridControl.Fields.AddRange(New DevExpress.XtraPivotGrid.PivotGridField() {Me.CFTCmpCode, Me.CFTCmpName, Me.CFTCustCode, Me.CFTCustName, Me.CFTCountryCode, Me.CFTCountryName, Me.CFTPORef, Me.CFTStyleCode, Me.CFTStyleName, Me.CFTCurCode, Me.CFDShipDate, Me.CFNPrice, Me.CFNQuantity, Me.CFNOrderAmt, Me.CFNQuantityExtra, Me.CFNOrderExtraAmt, Me.CFNGarmentQtyTest, Me.CFNOrderTestAmt, Me.CFNOrderGrandQuantity, Me.CFNOrderGrandAmt, Me.CFDOrderDate, Me.CFTInsUser, Me.CFDOrderYear, Me.CFNYearTerm, Me.CFDOrderMonth, Me.CFDOrderShipYear, Me.CFDOrderShiptMonth, Me.CFTPlantCode, Me.CFTPlantName, Me.C3FTSeasonCode, Me.C3FTOrderTypeName, Me.PXFTNikePOLineItem})
        Me.pivotGridControl.Location = New System.Drawing.Point(0, 0)
        Me.pivotGridControl.Name = "pivotGridControl"
        Me.pivotGridControl.OptionsChartDataSource.DataProvideMode = DevExpress.XtraPivotGrid.PivotChartDataProvideMode.UseCustomSettings
        Me.pivotGridControl.OptionsChartDataSource.ProvideDataByColumns = False
        Me.pivotGridControl.OptionsChartDataSource.UpdateDelay = 500
        Me.pivotGridControl.OptionsView.ShowColumnGrandTotalHeader = False
        Me.pivotGridControl.OptionsView.ShowColumnGrandTotals = False
        Me.pivotGridControl.OptionsView.ShowColumnTotals = False
        Me.pivotGridControl.Size = New System.Drawing.Size(1094, 400)
        Me.pivotGridControl.TabIndex = 2
        '
        'CFTCmpCode
        '
        Me.CFTCmpCode.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea
        Me.CFTCmpCode.AreaIndex = 0
        Me.CFTCmpCode.Caption = "Cmp Code"
        Me.CFTCmpCode.FieldName = "FTCmpCode"
        Me.CFTCmpCode.Name = "CFTCmpCode"
        '
        'CFTCmpName
        '
        Me.CFTCmpName.AreaIndex = 1
        Me.CFTCmpName.Caption = "Cmp Name"
        Me.CFTCmpName.FieldName = "FTCmpName"
        Me.CFTCmpName.Name = "CFTCmpName"
        Me.CFTCmpName.Width = 155
        '
        'CFTCustCode
        '
        Me.CFTCustCode.AreaIndex = 0
        Me.CFTCustCode.Caption = "Customer Code"
        Me.CFTCustCode.FieldName = "FTCustCode"
        Me.CFTCustCode.Name = "CFTCustCode"
        '
        'CFTCustName
        '
        Me.CFTCustName.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea
        Me.CFTCustName.AreaIndex = 1
        Me.CFTCustName.Caption = "Customer Name"
        Me.CFTCustName.FieldName = "FTCustName"
        Me.CFTCustName.Name = "CFTCustName"
        '
        'CFTCountryCode
        '
        Me.CFTCountryCode.AreaIndex = 11
        Me.CFTCountryCode.Caption = "Country Code"
        Me.CFTCountryCode.FieldName = "FTCountryCode"
        Me.CFTCountryCode.Name = "CFTCountryCode"
        '
        'CFTCountryName
        '
        Me.CFTCountryName.AreaIndex = 2
        Me.CFTCountryName.Caption = "Country Name"
        Me.CFTCountryName.FieldName = "FTCountryName"
        Me.CFTCountryName.Name = "CFTCountryName"
        '
        'CFTPORef
        '
        Me.CFTPORef.AreaIndex = 3
        Me.CFTPORef.Caption = "PO Ref."
        Me.CFTPORef.FieldName = "FTPORef"
        Me.CFTPORef.Name = "CFTPORef"
        '
        'CFTStyleCode
        '
        Me.CFTStyleCode.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea
        Me.CFTStyleCode.AreaIndex = 2
        Me.CFTStyleCode.Caption = "Style Code"
        Me.CFTStyleCode.FieldName = "FTStyleCode"
        Me.CFTStyleCode.Name = "CFTStyleCode"
        '
        'CFTStyleName
        '
        Me.CFTStyleName.AreaIndex = 4
        Me.CFTStyleName.Caption = "Style Name"
        Me.CFTStyleName.FieldName = "FTStyleName"
        Me.CFTStyleName.Name = "CFTStyleName"
        '
        'CFTCurCode
        '
        Me.CFTCurCode.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea
        Me.CFTCurCode.AreaIndex = 3
        Me.CFTCurCode.Caption = "Currrency"
        Me.CFTCurCode.FieldName = "FTCurCode"
        Me.CFTCurCode.Name = "CFTCurCode"
        '
        'CFDShipDate
        '
        Me.CFDShipDate.AreaIndex = 6
        Me.CFDShipDate.Caption = "Shipment Date"
        Me.CFDShipDate.FieldName = "FDShipDate"
        Me.CFDShipDate.Name = "CFDShipDate"
        '
        'CFNPrice
        '
        Me.CFNPrice.AreaIndex = 5
        Me.CFNPrice.Caption = "Price"
        Me.CFNPrice.CellFormat.FormatString = "{0:n4}"
        Me.CFNPrice.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CFNPrice.FieldName = "FNPrice"
        Me.CFNPrice.Name = "CFNPrice"
        '
        'CFNQuantity
        '
        Me.CFNQuantity.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
        Me.CFNQuantity.AreaIndex = 0
        Me.CFNQuantity.Caption = "จำนวน Order"
        Me.CFNQuantity.CellFormat.FormatString = "{0:n0}"
        Me.CFNQuantity.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CFNQuantity.FieldName = "FNQuantity"
        Me.CFNQuantity.Name = "CFNQuantity"
        '
        'CFNOrderAmt
        '
        Me.CFNOrderAmt.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
        Me.CFNOrderAmt.AreaIndex = 1
        Me.CFNOrderAmt.Caption = "มูลค่า Order"
        Me.CFNOrderAmt.CellFormat.FormatString = "{0:n2}"
        Me.CFNOrderAmt.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CFNOrderAmt.FieldName = "FNOrderAmt"
        Me.CFNOrderAmt.Name = "CFNOrderAmt"
        '
        'CFNQuantityExtra
        '
        Me.CFNQuantityExtra.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
        Me.CFNQuantityExtra.AreaIndex = 2
        Me.CFNQuantityExtra.Caption = "เผื่อ"
        Me.CFNQuantityExtra.CellFormat.FormatString = "{0:n0}"
        Me.CFNQuantityExtra.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CFNQuantityExtra.FieldName = "FNQuantityExtra"
        Me.CFNQuantityExtra.Name = "CFNQuantityExtra"
        '
        'CFNOrderExtraAmt
        '
        Me.CFNOrderExtraAmt.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
        Me.CFNOrderExtraAmt.AreaIndex = 3
        Me.CFNOrderExtraAmt.Caption = "มูลค่าเผื่อ"
        Me.CFNOrderExtraAmt.CellFormat.FormatString = "{0:n2}"
        Me.CFNOrderExtraAmt.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CFNOrderExtraAmt.FieldName = "FNOrderExtraAmt"
        Me.CFNOrderExtraAmt.Name = "CFNOrderExtraAmt"
        '
        'CFNGarmentQtyTest
        '
        Me.CFNGarmentQtyTest.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
        Me.CFNGarmentQtyTest.AreaIndex = 4
        Me.CFNGarmentQtyTest.Caption = "เทส"
        Me.CFNGarmentQtyTest.CellFormat.FormatString = "{0:n0}"
        Me.CFNGarmentQtyTest.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CFNGarmentQtyTest.FieldName = "FNGarmentQtyTest"
        Me.CFNGarmentQtyTest.Name = "CFNGarmentQtyTest"
        '
        'CFNOrderTestAmt
        '
        Me.CFNOrderTestAmt.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
        Me.CFNOrderTestAmt.AreaIndex = 5
        Me.CFNOrderTestAmt.Caption = "มูลค่าเทส"
        Me.CFNOrderTestAmt.CellFormat.FormatString = "{0:n2}"
        Me.CFNOrderTestAmt.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CFNOrderTestAmt.FieldName = "FNOrderTestAmt"
        Me.CFNOrderTestAmt.Name = "CFNOrderTestAmt"
        '
        'CFNOrderGrandQuantity
        '
        Me.CFNOrderGrandQuantity.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
        Me.CFNOrderGrandQuantity.AreaIndex = 6
        Me.CFNOrderGrandQuantity.Caption = "Grand Quantity"
        Me.CFNOrderGrandQuantity.CellFormat.FormatString = "{0:n0}"
        Me.CFNOrderGrandQuantity.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CFNOrderGrandQuantity.FieldName = "FNOrderGrandQuantity"
        Me.CFNOrderGrandQuantity.Name = "CFNOrderGrandQuantity"
        '
        'CFNOrderGrandAmt
        '
        Me.CFNOrderGrandAmt.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
        Me.CFNOrderGrandAmt.AreaIndex = 7
        Me.CFNOrderGrandAmt.Caption = "Grand Amt"
        Me.CFNOrderGrandAmt.CellFormat.FormatString = "{0:n0}"
        Me.CFNOrderGrandAmt.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CFNOrderGrandAmt.FieldName = "FNOrderGrandAmt"
        Me.CFNOrderGrandAmt.Name = "CFNOrderGrandAmt"
        '
        'CFDOrderDate
        '
        Me.CFDOrderDate.AreaIndex = 7
        Me.CFDOrderDate.Caption = "OrderDate"
        Me.CFDOrderDate.FieldName = "FDOrderDate"
        Me.CFDOrderDate.Name = "CFDOrderDate"
        '
        'CFTInsUser
        '
        Me.CFTInsUser.AreaIndex = 8
        Me.CFTInsUser.Caption = "Mer"
        Me.CFTInsUser.FieldName = "FTInsUser"
        Me.CFTInsUser.Name = "CFTInsUser"
        '
        'CFDOrderYear
        '
        Me.CFDOrderYear.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea
        Me.CFDOrderYear.AreaIndex = 0
        Me.CFDOrderYear.Caption = "Order Year"
        Me.CFDOrderYear.FieldName = "FDOrderYear"
        Me.CFDOrderYear.Name = "CFDOrderYear"
        '
        'CFNYearTerm
        '
        Me.CFNYearTerm.AreaIndex = 12
        Me.CFNYearTerm.Caption = "ไตรมาส"
        Me.CFNYearTerm.FieldName = "FNYearTerm"
        Me.CFNYearTerm.Name = "CFNYearTerm"
        '
        'CFDOrderMonth
        '
        Me.CFDOrderMonth.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea
        Me.CFDOrderMonth.AreaIndex = 1
        Me.CFDOrderMonth.Caption = "Order Month"
        Me.CFDOrderMonth.FieldName = "FDOrderMonth"
        Me.CFDOrderMonth.Name = "CFDOrderMonth"
        '
        'CFDOrderShipYear
        '
        Me.CFDOrderShipYear.AreaIndex = 9
        Me.CFDOrderShipYear.Caption = "OrderShipYear"
        Me.CFDOrderShipYear.FieldName = "FDOrderShipYear"
        Me.CFDOrderShipYear.Name = "CFDOrderShipYear"
        '
        'CFDOrderShiptMonth
        '
        Me.CFDOrderShiptMonth.AreaIndex = 10
        Me.CFDOrderShiptMonth.Caption = "OrderShiptMonth"
        Me.CFDOrderShiptMonth.FieldName = "FDOrderShiptMonth"
        Me.CFDOrderShiptMonth.Name = "CFDOrderShiptMonth"
        '
        'CFTPlantCode
        '
        Me.CFTPlantCode.AreaIndex = 13
        Me.CFTPlantCode.Caption = "Plant"
        Me.CFTPlantCode.FieldName = "FTPlantCode"
        Me.CFTPlantCode.Name = "CFTPlantCode"
        '
        'CFTPlantName
        '
        Me.CFTPlantName.AreaIndex = 14
        Me.CFTPlantName.Caption = "Plant Name"
        Me.CFTPlantName.FieldName = "FTPlantName"
        Me.CFTPlantName.Name = "CFTPlantName"
        '
        'C3FTSeasonCode
        '
        Me.C3FTSeasonCode.AreaIndex = 15
        Me.C3FTSeasonCode.Caption = "Season"
        Me.C3FTSeasonCode.FieldName = "FTSeasonCode"
        Me.C3FTSeasonCode.Name = "C3FTSeasonCode"
        '
        'C3FTOrderTypeName
        '
        Me.C3FTOrderTypeName.AreaIndex = 16
        Me.C3FTOrderTypeName.Caption = "Order Type"
        Me.C3FTOrderTypeName.FieldName = "FTOrderTypeName"
        Me.C3FTOrderTypeName.Name = "C3FTOrderTypeName"
        '
        'PXFTNikePOLineItem
        '
        Me.PXFTNikePOLineItem.AreaIndex = 17
        Me.PXFTNikePOLineItem.Caption = "PO Line."
        Me.PXFTNikePOLineItem.FieldName = "FTNikePOLineItem"
        Me.PXFTNikePOLineItem.Name = "PXFTNikePOLineItem"
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
        Me.chartControl.Location = New System.Drawing.Point(0, 57)
        Me.chartControl.Name = "chartControl"
        Me.chartControl.SeriesDataMember = "Series"
        Me.chartControl.SeriesSerializable = New DevExpress.XtraCharts.Series(-1) {}
        Me.chartControl.SeriesTemplate.ArgumentDataMember = "Arguments"
        Me.chartControl.SeriesTemplate.CrosshairLabelPattern = "{S} : {V:c}"
        Me.chartControl.SeriesTemplate.LabelsVisibility = DevExpress.Utils.DefaultBoolean.[False]
        Me.chartControl.SeriesTemplate.ValueDataMembersSerializable = "Values"
        Me.chartControl.SeriesTemplate.View = LineSeriesView1
        Me.chartControl.Size = New System.Drawing.Size(1094, 343)
        Me.chartControl.TabIndex = 3
        '
        'otb
        '
        Me.otb.Dock = System.Windows.Forms.DockStyle.Fill
        Me.otb.Location = New System.Drawing.Point(0, 143)
        Me.otb.Name = "otb"
        Me.otb.SelectedTabPage = Me.otpsummary
        Me.otb.Size = New System.Drawing.Size(1100, 428)
        Me.otb.TabIndex = 394
        Me.otb.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.otpsummary, Me.otpdata, Me.otpchart})
        '
        'otpsummary
        '
        Me.otpsummary.Controls.Add(Me.ogc)
        Me.otpsummary.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.otpsummary.Name = "otpsummary"
        Me.otpsummary.Size = New System.Drawing.Size(1094, 400)
        Me.otpsummary.Text = "Summary"
        '
        'ogc
        '
        Me.ogc.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogc.Location = New System.Drawing.Point(0, 0)
        Me.ogc.MainView = Me.ogv
        Me.ogc.Name = "ogc"
        Me.ogc.Size = New System.Drawing.Size(1094, 400)
        Me.ogc.TabIndex = 1
        Me.ogc.TabStop = False
        Me.ogc.Tag = "2|"
        Me.ogc.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogv})
        '
        'ogv
        '
        Me.ogv.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.FTOrderNo, Me.CCFTSeasonCode, Me.CCFTOrderTypeName, Me.FTCmpCode, Me.FTCmpName, Me.FTCustCode, Me.FTCustName, Me.FTCountryCode, Me.FTCountryName, Me.FTPlantCode, Me.FTPlantName, Me.FTPORef, Me.CFTNikePOLineItem, Me.FTStyleCode, Me.FTStyleName, Me.FDShipDate, Me.FTCurCode, Me.FNPrice, Me.FNQuantity, Me.FNOrderAmt, Me.FNQuantityExtra, Me.FNOrderExtraAmt, Me.FNGarmentQtyTest, Me.FNOrderTestAmt, Me.FNOrderGrandQuantity, Me.FNOrderGrandAmt, Me.FDOrderDate, Me.FTInsUser, Me.FDOrderYear, Me.FDOrderMonth, Me.FDOrderShipYear, Me.FDOrderShiptMonth, Me.FNYearTerm})
        Me.ogv.GridControl = Me.ogc
        Me.ogv.Name = "ogv"
        Me.ogv.OptionsCustomization.AllowQuickHideColumns = False
        Me.ogv.OptionsView.ColumnAutoWidth = False
        Me.ogv.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.ogv.OptionsView.ShowGroupPanel = False
        Me.ogv.Tag = "2|"
        '
        'FTOrderNo
        '
        Me.FTOrderNo.Caption = "FTOrderNo"
        Me.FTOrderNo.FieldName = "FTOrderNo"
        Me.FTOrderNo.Name = "FTOrderNo"
        Me.FTOrderNo.OptionsColumn.AllowEdit = False
        Me.FTOrderNo.OptionsColumn.ReadOnly = True
        Me.FTOrderNo.Visible = True
        Me.FTOrderNo.VisibleIndex = 0
        Me.FTOrderNo.Width = 120
        '
        'CCFTSeasonCode
        '
        Me.CCFTSeasonCode.Caption = "Season"
        Me.CCFTSeasonCode.FieldName = "FTSeasonCode"
        Me.CCFTSeasonCode.Name = "CCFTSeasonCode"
        Me.CCFTSeasonCode.OptionsColumn.AllowEdit = False
        Me.CCFTSeasonCode.OptionsColumn.ReadOnly = True
        Me.CCFTSeasonCode.Visible = True
        Me.CCFTSeasonCode.VisibleIndex = 1
        '
        'CCFTOrderTypeName
        '
        Me.CCFTOrderTypeName.Caption = "Order Type"
        Me.CCFTOrderTypeName.FieldName = "FTOrderTypeName"
        Me.CCFTOrderTypeName.Name = "CCFTOrderTypeName"
        Me.CCFTOrderTypeName.OptionsColumn.AllowEdit = False
        Me.CCFTOrderTypeName.OptionsColumn.ReadOnly = True
        Me.CCFTOrderTypeName.Visible = True
        Me.CCFTOrderTypeName.VisibleIndex = 2
        '
        'FTCmpCode
        '
        Me.FTCmpCode.Caption = "FTCmpCode"
        Me.FTCmpCode.FieldName = "FTCmpCode"
        Me.FTCmpCode.Name = "FTCmpCode"
        Me.FTCmpCode.OptionsColumn.AllowEdit = False
        Me.FTCmpCode.OptionsColumn.ReadOnly = True
        Me.FTCmpCode.Visible = True
        Me.FTCmpCode.VisibleIndex = 3
        Me.FTCmpCode.Width = 100
        '
        'FTCmpName
        '
        Me.FTCmpName.Caption = "FTCmpName"
        Me.FTCmpName.FieldName = "FTCmpName"
        Me.FTCmpName.Name = "FTCmpName"
        Me.FTCmpName.OptionsColumn.AllowEdit = False
        Me.FTCmpName.OptionsColumn.ReadOnly = True
        Me.FTCmpName.Visible = True
        Me.FTCmpName.VisibleIndex = 4
        Me.FTCmpName.Width = 150
        '
        'FTCustCode
        '
        Me.FTCustCode.Caption = "Customer Code"
        Me.FTCustCode.FieldName = "FTCustCode"
        Me.FTCustCode.Name = "FTCustCode"
        Me.FTCustCode.OptionsColumn.AllowEdit = False
        Me.FTCustCode.OptionsColumn.ReadOnly = True
        Me.FTCustCode.Visible = True
        Me.FTCustCode.VisibleIndex = 5
        Me.FTCustCode.Width = 100
        '
        'FTCustName
        '
        Me.FTCustName.Caption = "Customer Name"
        Me.FTCustName.FieldName = "FTCustName"
        Me.FTCustName.Name = "FTCustName"
        Me.FTCustName.OptionsColumn.AllowEdit = False
        Me.FTCustName.OptionsColumn.ReadOnly = True
        Me.FTCustName.Visible = True
        Me.FTCustName.VisibleIndex = 6
        Me.FTCustName.Width = 150
        '
        'FTCountryCode
        '
        Me.FTCountryCode.Caption = "FTCountryCode"
        Me.FTCountryCode.FieldName = "FTCountryCode"
        Me.FTCountryCode.Name = "FTCountryCode"
        Me.FTCountryCode.OptionsColumn.AllowEdit = False
        Me.FTCountryCode.OptionsColumn.ReadOnly = True
        Me.FTCountryCode.Visible = True
        Me.FTCountryCode.VisibleIndex = 7
        Me.FTCountryCode.Width = 100
        '
        'FTCountryName
        '
        Me.FTCountryName.Caption = "FTCountryName"
        Me.FTCountryName.FieldName = "FTCountryName"
        Me.FTCountryName.Name = "FTCountryName"
        Me.FTCountryName.OptionsColumn.AllowEdit = False
        Me.FTCountryName.OptionsColumn.ReadOnly = True
        Me.FTCountryName.Visible = True
        Me.FTCountryName.VisibleIndex = 8
        Me.FTCountryName.Width = 150
        '
        'FTPlantCode
        '
        Me.FTPlantCode.Caption = "Plant"
        Me.FTPlantCode.FieldName = "FTPlantCode"
        Me.FTPlantCode.Name = "FTPlantCode"
        Me.FTPlantCode.OptionsColumn.AllowEdit = False
        Me.FTPlantCode.OptionsColumn.ReadOnly = True
        Me.FTPlantCode.Visible = True
        Me.FTPlantCode.VisibleIndex = 9
        Me.FTPlantCode.Width = 92
        '
        'FTPlantName
        '
        Me.FTPlantName.Caption = "Plant Name"
        Me.FTPlantName.FieldName = "FTPlantName"
        Me.FTPlantName.Name = "FTPlantName"
        Me.FTPlantName.OptionsColumn.AllowEdit = False
        Me.FTPlantName.OptionsColumn.ReadOnly = True
        Me.FTPlantName.Visible = True
        Me.FTPlantName.VisibleIndex = 10
        Me.FTPlantName.Width = 120
        '
        'FTPORef
        '
        Me.FTPORef.Caption = "FTPORef"
        Me.FTPORef.FieldName = "FTPORef"
        Me.FTPORef.Name = "FTPORef"
        Me.FTPORef.OptionsColumn.AllowEdit = False
        Me.FTPORef.OptionsColumn.ReadOnly = True
        Me.FTPORef.Visible = True
        Me.FTPORef.VisibleIndex = 11
        Me.FTPORef.Width = 120
        '
        'CFTNikePOLineItem
        '
        Me.CFTNikePOLineItem.Caption = "PO Line."
        Me.CFTNikePOLineItem.FieldName = "FTNikePOLineItem"
        Me.CFTNikePOLineItem.Name = "CFTNikePOLineItem"
        Me.CFTNikePOLineItem.OptionsColumn.AllowEdit = False
        Me.CFTNikePOLineItem.OptionsColumn.ReadOnly = True
        Me.CFTNikePOLineItem.Visible = True
        Me.CFTNikePOLineItem.VisibleIndex = 12
        '
        'FTStyleCode
        '
        Me.FTStyleCode.Caption = "FTStyleCode"
        Me.FTStyleCode.FieldName = "FTStyleCode"
        Me.FTStyleCode.Name = "FTStyleCode"
        Me.FTStyleCode.OptionsColumn.AllowEdit = False
        Me.FTStyleCode.OptionsColumn.ReadOnly = True
        Me.FTStyleCode.Visible = True
        Me.FTStyleCode.VisibleIndex = 13
        Me.FTStyleCode.Width = 100
        '
        'FTStyleName
        '
        Me.FTStyleName.Caption = "FTStyleName"
        Me.FTStyleName.FieldName = "FTStyleName"
        Me.FTStyleName.Name = "FTStyleName"
        Me.FTStyleName.OptionsColumn.AllowEdit = False
        Me.FTStyleName.OptionsColumn.ReadOnly = True
        Me.FTStyleName.Visible = True
        Me.FTStyleName.VisibleIndex = 14
        Me.FTStyleName.Width = 150
        '
        'FDShipDate
        '
        Me.FDShipDate.Caption = "Shipment Date"
        Me.FDShipDate.FieldName = "FDShipDate"
        Me.FDShipDate.Name = "FDShipDate"
        Me.FDShipDate.OptionsColumn.AllowEdit = False
        Me.FDShipDate.OptionsColumn.ReadOnly = True
        Me.FDShipDate.Visible = True
        Me.FDShipDate.VisibleIndex = 15
        Me.FDShipDate.Width = 100
        '
        'FTCurCode
        '
        Me.FTCurCode.Caption = "FTCurCode"
        Me.FTCurCode.FieldName = "FTCurCode"
        Me.FTCurCode.Name = "FTCurCode"
        Me.FTCurCode.OptionsColumn.AllowEdit = False
        Me.FTCurCode.OptionsColumn.ReadOnly = True
        Me.FTCurCode.Visible = True
        Me.FTCurCode.VisibleIndex = 16
        Me.FTCurCode.Width = 80
        '
        'FNPrice
        '
        Me.FNPrice.Caption = "FNPrice"
        Me.FNPrice.DisplayFormat.FormatString = "{0:n4}"
        Me.FNPrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNPrice.FieldName = "FNPrice"
        Me.FNPrice.Name = "FNPrice"
        Me.FNPrice.OptionsColumn.AllowEdit = False
        Me.FNPrice.OptionsColumn.ReadOnly = True
        Me.FNPrice.Visible = True
        Me.FNPrice.VisibleIndex = 17
        Me.FNPrice.Width = 100
        '
        'FNQuantity
        '
        Me.FNQuantity.Caption = "FNQuantity"
        Me.FNQuantity.DisplayFormat.FormatString = "{0:n0}"
        Me.FNQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNQuantity.FieldName = "FNQuantity"
        Me.FNQuantity.Name = "FNQuantity"
        Me.FNQuantity.OptionsColumn.AllowEdit = False
        Me.FNQuantity.OptionsColumn.ReadOnly = True
        Me.FNQuantity.Visible = True
        Me.FNQuantity.VisibleIndex = 18
        Me.FNQuantity.Width = 100
        '
        'FNOrderAmt
        '
        Me.FNOrderAmt.Caption = "FNOrderAmt"
        Me.FNOrderAmt.DisplayFormat.FormatString = "{0:n2}"
        Me.FNOrderAmt.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNOrderAmt.FieldName = "FNOrderAmt"
        Me.FNOrderAmt.Name = "FNOrderAmt"
        Me.FNOrderAmt.OptionsColumn.AllowEdit = False
        Me.FNOrderAmt.OptionsColumn.ReadOnly = True
        Me.FNOrderAmt.Visible = True
        Me.FNOrderAmt.VisibleIndex = 19
        Me.FNOrderAmt.Width = 100
        '
        'FNQuantityExtra
        '
        Me.FNQuantityExtra.Caption = "FNQuantityExtra"
        Me.FNQuantityExtra.DisplayFormat.FormatString = "{0:n0}"
        Me.FNQuantityExtra.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNQuantityExtra.FieldName = "FNQuantityExtra"
        Me.FNQuantityExtra.Name = "FNQuantityExtra"
        Me.FNQuantityExtra.OptionsColumn.AllowEdit = False
        Me.FNQuantityExtra.OptionsColumn.ReadOnly = True
        Me.FNQuantityExtra.Visible = True
        Me.FNQuantityExtra.VisibleIndex = 20
        Me.FNQuantityExtra.Width = 100
        '
        'FNOrderExtraAmt
        '
        Me.FNOrderExtraAmt.Caption = "FNOrderExtraAmt"
        Me.FNOrderExtraAmt.DisplayFormat.FormatString = "{0:n2}"
        Me.FNOrderExtraAmt.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNOrderExtraAmt.FieldName = "FNOrderExtraAmt"
        Me.FNOrderExtraAmt.Name = "FNOrderExtraAmt"
        Me.FNOrderExtraAmt.OptionsColumn.AllowEdit = False
        Me.FNOrderExtraAmt.OptionsColumn.ReadOnly = True
        Me.FNOrderExtraAmt.Visible = True
        Me.FNOrderExtraAmt.VisibleIndex = 21
        Me.FNOrderExtraAmt.Width = 100
        '
        'FNGarmentQtyTest
        '
        Me.FNGarmentQtyTest.Caption = "FNGarmentQtyTest"
        Me.FNGarmentQtyTest.DisplayFormat.FormatString = "{0:n0}"
        Me.FNGarmentQtyTest.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNGarmentQtyTest.FieldName = "FNGarmentQtyTest"
        Me.FNGarmentQtyTest.Name = "FNGarmentQtyTest"
        Me.FNGarmentQtyTest.OptionsColumn.AllowEdit = False
        Me.FNGarmentQtyTest.OptionsColumn.ReadOnly = True
        Me.FNGarmentQtyTest.Visible = True
        Me.FNGarmentQtyTest.VisibleIndex = 22
        Me.FNGarmentQtyTest.Width = 100
        '
        'FNOrderTestAmt
        '
        Me.FNOrderTestAmt.Caption = "FNOrderTestAmt"
        Me.FNOrderTestAmt.DisplayFormat.FormatString = "{0:n2}"
        Me.FNOrderTestAmt.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNOrderTestAmt.FieldName = "FNOrderTestAmt"
        Me.FNOrderTestAmt.Name = "FNOrderTestAmt"
        Me.FNOrderTestAmt.OptionsColumn.AllowEdit = False
        Me.FNOrderTestAmt.OptionsColumn.ReadOnly = True
        Me.FNOrderTestAmt.Visible = True
        Me.FNOrderTestAmt.VisibleIndex = 23
        Me.FNOrderTestAmt.Width = 100
        '
        'FNOrderGrandQuantity
        '
        Me.FNOrderGrandQuantity.Caption = "FNOrderGrandQuantity"
        Me.FNOrderGrandQuantity.DisplayFormat.FormatString = "{0:n0}"
        Me.FNOrderGrandQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNOrderGrandQuantity.FieldName = "FNOrderGrandQuantity"
        Me.FNOrderGrandQuantity.Name = "FNOrderGrandQuantity"
        Me.FNOrderGrandQuantity.OptionsColumn.AllowEdit = False
        Me.FNOrderGrandQuantity.OptionsColumn.ReadOnly = True
        Me.FNOrderGrandQuantity.Visible = True
        Me.FNOrderGrandQuantity.VisibleIndex = 24
        Me.FNOrderGrandQuantity.Width = 100
        '
        'FNOrderGrandAmt
        '
        Me.FNOrderGrandAmt.Caption = "FNOrderGrandAmt"
        Me.FNOrderGrandAmt.DisplayFormat.FormatString = "{0:n2}"
        Me.FNOrderGrandAmt.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNOrderGrandAmt.FieldName = "FNOrderGrandAmt"
        Me.FNOrderGrandAmt.Name = "FNOrderGrandAmt"
        Me.FNOrderGrandAmt.OptionsColumn.AllowEdit = False
        Me.FNOrderGrandAmt.OptionsColumn.ReadOnly = True
        Me.FNOrderGrandAmt.Visible = True
        Me.FNOrderGrandAmt.VisibleIndex = 25
        Me.FNOrderGrandAmt.Width = 100
        '
        'FDOrderDate
        '
        Me.FDOrderDate.Caption = "Order Date"
        Me.FDOrderDate.FieldName = "FDOrderDate"
        Me.FDOrderDate.Name = "FDOrderDate"
        Me.FDOrderDate.OptionsColumn.AllowEdit = False
        Me.FDOrderDate.OptionsColumn.ReadOnly = True
        Me.FDOrderDate.Visible = True
        Me.FDOrderDate.VisibleIndex = 26
        Me.FDOrderDate.Width = 100
        '
        'FTInsUser
        '
        Me.FTInsUser.Caption = "Mer"
        Me.FTInsUser.FieldName = "FTInsUser"
        Me.FTInsUser.Name = "FTInsUser"
        Me.FTInsUser.OptionsColumn.AllowEdit = False
        Me.FTInsUser.OptionsColumn.ReadOnly = True
        Me.FTInsUser.Visible = True
        Me.FTInsUser.VisibleIndex = 27
        Me.FTInsUser.Width = 100
        '
        'FDOrderYear
        '
        Me.FDOrderYear.Caption = "FDOrderYear"
        Me.FDOrderYear.FieldName = "FDOrderYear"
        Me.FDOrderYear.Name = "FDOrderYear"
        '
        'FDOrderMonth
        '
        Me.FDOrderMonth.Caption = "FDOrderMonth"
        Me.FDOrderMonth.FieldName = "FDOrderMonth"
        Me.FDOrderMonth.Name = "FDOrderMonth"
        '
        'FDOrderShipYear
        '
        Me.FDOrderShipYear.Caption = "FDOrderShipYear"
        Me.FDOrderShipYear.FieldName = "FDOrderShipYear"
        Me.FDOrderShipYear.Name = "FDOrderShipYear"
        '
        'FDOrderShiptMonth
        '
        Me.FDOrderShiptMonth.Caption = "FDOrderShiptMonth"
        Me.FDOrderShiptMonth.FieldName = "FDOrderShiptMonth"
        Me.FDOrderShiptMonth.Name = "FDOrderShiptMonth"
        '
        'FNYearTerm
        '
        Me.FNYearTerm.Caption = "FNYearTerm"
        Me.FNYearTerm.FieldName = "FNYearTerm"
        Me.FNYearTerm.Name = "FNYearTerm"
        '
        'otpdata
        '
        Me.otpdata.Controls.Add(Me.pivotGridControl)
        Me.otpdata.Name = "otpdata"
        Me.otpdata.Size = New System.Drawing.Size(1094, 400)
        Me.otpdata.Text = "BI Data"
        '
        'otpchart
        '
        Me.otpchart.Controls.Add(Me.chartControl)
        Me.otpchart.Controls.Add(Me.panelControl1)
        Me.otpchart.Name = "otpchart"
        Me.otpchart.Size = New System.Drawing.Size(1094, 400)
        Me.otpchart.Text = "BI Chart"
        '
        'ogbmainprocbutton
        '
        Me.ogbmainprocbutton.Controls.Add(Me.ocmexit)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmload)
        Me.ogbmainprocbutton.Location = New System.Drawing.Point(119, 268)
        Me.ogbmainprocbutton.Name = "ogbmainprocbutton"
        Me.ogbmainprocbutton.Size = New System.Drawing.Size(631, 34)
        Me.ogbmainprocbutton.TabIndex = 397
        '
        'ocmexit
        '
        Me.ocmexit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmexit.Location = New System.Drawing.Point(472, 7)
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
        'wMERBIOrderSummary
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1100, 571)
        Me.Controls.Add(Me.ogbmainprocbutton)
        Me.Controls.Add(Me.otb)
        Me.Controls.Add(Me.ogbheader)
        Me.Name = "wMERBIOrderSummary"
        Me.Text = "Order Summary"
        CType(Me.ocmdoc, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbheader.ResumeLayout(False)
        Me.DockPanel1_Container.ResumeLayout(False)
        CType(Me.FTEndCreateDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTEndCreateDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysCustId_None.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTStartCreateDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTStartCreateDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTEndOrderDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTEndOrderDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysCustId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTStartOrderDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTStartOrderDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTEndShipment.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTEndShipment.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTStartShipment.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTStartShipment.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysCmpId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysCmpId_None.Properties, System.ComponentModel.ISupportInitialize).EndInit()
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
        CType(Me.otb, System.ComponentModel.ISupportInitialize).EndInit()
        Me.otb.ResumeLayout(False)
        Me.otpsummary.ResumeLayout(False)
        CType(Me.ogc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.otpdata.ResumeLayout(False)
        Me.otpchart.ResumeLayout(False)
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbmainprocbutton.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ocmdoc As DevExpress.XtraBars.Docking.DockManager
    Friend WithEvents ogbheader As DevExpress.XtraBars.Docking.DockPanel
    Friend WithEvents DockPanel1_Container As DevExpress.XtraBars.Docking.ControlContainer
    Private WithEvents pivotGridControl As DevExpress.XtraPivotGrid.PivotGridControl
    Private WithEvents CFTCmpName As DevExpress.XtraPivotGrid.PivotGridField
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
    Friend WithEvents otb As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents otpdata As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents otpchart As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents otpsummary As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents ogbmainprocbutton As DevExpress.XtraEditors.PanelControl
    Friend WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmload As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FTEndOrderDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents FNHSysCmpId_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTEndOrderDate_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTStartOrderDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents FTStartOrderDate_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTEndShipment As DevExpress.XtraEditors.DateEdit
    Friend WithEvents FTEndShipment_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTStartShipment As DevExpress.XtraEditors.DateEdit
    Friend WithEvents FNHSysCmpId As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FTStartShipment_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysCmpId_None As DevExpress.XtraEditors.TextEdit
    Friend WithEvents ogc As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogv As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents CFTCmpCode As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents CFTCustCode As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents CFTCustName As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents CFTCountryCode As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents CFTCountryName As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents CFTPORef As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents CFTStyleCode As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents CFTStyleName As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents CFTCurCode As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents CFDShipDate As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents CFNPrice As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents CFNQuantity As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents CFNOrderAmt As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents CFNQuantityExtra As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents CFNOrderExtraAmt As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents CFNGarmentQtyTest As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents CFNOrderTestAmt As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents CFNOrderGrandQuantity As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents CFNOrderGrandAmt As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents CFDOrderDate As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents CFTInsUser As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents CFDOrderYear As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents CFNYearTerm As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents CFDOrderMonth As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents CFDOrderShipYear As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents CFDOrderShiptMonth As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents FTOrderNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTCmpCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTCmpName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTCountryCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTCountryName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTPORef As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTStyleCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTStyleName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FDShipDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTCurCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNPrice As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNOrderAmt As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNQuantityExtra As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNOrderExtraAmt As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNGarmentQtyTest As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNOrderTestAmt As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNOrderGrandQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNOrderGrandAmt As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FDOrderDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTInsUser As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FDOrderYear As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FDOrderMonth As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FDOrderShipYear As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FDOrderShiptMonth As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNYearTerm As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNHSysCustId_None As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FNHSysCustId_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysCustId As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FTCustCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTCustName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTPlantCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTPlantName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFTPlantCode As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents CFTPlantName As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents FTEndCreateDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents FTEndCreateDate_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTStartCreateDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents FTStartCreateDate_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents CCFTSeasonCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CCFTOrderTypeName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents C3FTSeasonCode As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents C3FTOrderTypeName As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents CFTNikePOLineItem As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents PXFTNikePOLineItem As DevExpress.XtraPivotGrid.PivotGridField
End Class

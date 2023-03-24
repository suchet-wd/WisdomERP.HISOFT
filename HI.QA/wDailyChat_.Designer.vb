Imports DevExpress.XtraEditors.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wDailyChat_
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(wDailyChat_))
        Dim EditorButtonImageOptions1 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject4 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim XyDiagram1 As DevExpress.XtraCharts.XYDiagram = New DevExpress.XtraCharts.XYDiagram()
        Dim Series1 As DevExpress.XtraCharts.Series = New DevExpress.XtraCharts.Series()
        Dim GridLevelNode1 As DevExpress.XtraGrid.GridLevelNode = New DevExpress.XtraGrid.GridLevelNode()
        Me.ocmdoc = New DevExpress.XtraBars.Docking.DockManager(Me.components)
        Me.hideContainerTop = New DevExpress.XtraBars.Docking.AutoHideContainer()
        Me.ogbheader = New DevExpress.XtraBars.Docking.DockPanel()
        Me.DockPanel1_Container = New DevExpress.XtraBars.Docking.ControlContainer()
        Me.EFTDateTrans = New DevExpress.XtraEditors.DateEdit()
        Me.FNHSysCmpId_None = New DevExpress.XtraEditors.TextEdit()
        Me.EFTDateTrans_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.SFTDateTrans = New DevExpress.XtraEditors.DateEdit()
        Me.FNHSysCmpId_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.SFTDateTrans_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysCmpId = New DevExpress.XtraEditors.ButtonEdit()
        Me.hideContainerLeft = New DevExpress.XtraBars.Docking.AutoHideContainer()
        Me.oDockPanelDetail = New DevExpress.XtraBars.Docking.DockPanel()
        Me.ControlContainer2 = New DevExpress.XtraBars.Docking.ControlContainer()
        Me.ogrpSubChart = New DevExpress.XtraEditors.GroupControl()
        Me.ochartSubDefect = New DevExpress.XtraCharts.ChartControl()
        Me.ogcDefectDetail = New DevExpress.XtraGrid.GridControl()
        Me.ogvDefectDetail = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.cdFNHSysStyleId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cdFNHSysUnitSectId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cdFTOrderNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cdFDQADate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cdFTPORef = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cdFTStyleCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cdFNQAInQty = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cdFNQAAqlQty = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cdFNQAActualQty = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cdFNTotalDefect = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ogrpGrird = New DevExpress.XtraEditors.GroupControl()
        Me.ogbmainprocbutton = New DevExpress.XtraEditors.PanelControl()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmload = New DevExpress.XtraEditors.SimpleButton()
        Me.ogcMain = New DevExpress.XtraGrid.GridControl()
        Me.ogvMain = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.cFNHSysUnitSectId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTUnitSectCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNQAInQty = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNQAAqlQty = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNQAActualQty = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNMajorQty = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNMinorQty = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNAndon = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.dockDetail = New DevExpress.XtraBars.Docking.DockPanel()
        Me.ControlContainer1 = New DevExpress.XtraBars.Docking.ControlContainer()
        Me.ogcDetailMain = New DevExpress.XtraGrid.GridControl()
        Me.ogvDetailMain = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.ocViewType = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.ocViewType_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTSeriesName = New DevExpress.XtraEditors.LabelControl()
        Me.FTTitleChart = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.comboChartType = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.checkShowPointLabels = New DevExpress.XtraEditors.CheckEdit()
        Me.ceChartDataVertical = New DevExpress.XtraEditors.CheckEdit()
        Me.ceSelectionOnly = New DevExpress.XtraEditors.CheckEdit()
        Me.ceShowColumnGrandTotals = New DevExpress.XtraEditors.CheckEdit()
        Me.ceShowRowGrandTotals = New DevExpress.XtraEditors.CheckEdit()
        Me.lblUpdateDelay = New DevExpress.XtraEditors.LabelControl()
        Me.seUpdateDelay = New DevExpress.XtraEditors.SpinEdit()
        Me.ogrpChart = New DevExpress.XtraEditors.GroupControl()
        Me.ComboBoxEdit1 = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.oGrpTopData = New DevExpress.XtraEditors.GroupControl()
        Me.ogcTopDefect = New DevExpress.XtraGrid.GridControl()
        Me.ogvTopDefect = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.ctFNHSysQADetailId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ctFTQADetailName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ctQty = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ct = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ocViewTypePie = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.ocViewTypePie_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.oGrpTopChart = New DevExpress.XtraEditors.GroupControl()
        Me.ogrpMChart = New DevExpress.XtraEditors.GroupControl()
        Me.oGrpMTopChart = New DevExpress.XtraEditors.GroupControl()
        CType(Me.ocmdoc, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.hideContainerTop.SuspendLayout()
        Me.ogbheader.SuspendLayout()
        Me.DockPanel1_Container.SuspendLayout()
        CType(Me.EFTDateTrans.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EFTDateTrans.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysCmpId_None.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SFTDateTrans.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SFTDateTrans.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysCmpId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.hideContainerLeft.SuspendLayout()
        Me.oDockPanelDetail.SuspendLayout()
        Me.ControlContainer2.SuspendLayout()
        CType(Me.ogrpSubChart, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogrpSubChart.SuspendLayout()
        CType(Me.ochartSubDefect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(XyDiagram1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Series1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogcDefectDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvDefectDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogrpGrird, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogrpGrird.SuspendLayout()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbmainprocbutton.SuspendLayout()
        CType(Me.ogcMain, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.dockDetail.SuspendLayout()
        Me.ControlContainer1.SuspendLayout()
        CType(Me.ogcDetailMain, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvDetailMain, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ocViewType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.comboChartType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.checkShowPointLabels.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ceChartDataVertical.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ceSelectionOnly.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ceShowColumnGrandTotals.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ceShowRowGrandTotals.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.seUpdateDelay.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogrpChart, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ComboBoxEdit1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.oGrpTopData, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.oGrpTopData.SuspendLayout()
        CType(Me.ogcTopDefect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvTopDefect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ocViewTypePie.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.oGrpTopChart, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogrpMChart, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogrpMChart.SuspendLayout()
        CType(Me.oGrpMTopChart, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.oGrpMTopChart.SuspendLayout()
        Me.SuspendLayout()
        '
        'ocmdoc
        '
        Me.ocmdoc.AutoHideContainers.AddRange(New DevExpress.XtraBars.Docking.AutoHideContainer() {Me.hideContainerTop, Me.hideContainerLeft})
        Me.ocmdoc.Form = Me
        Me.ocmdoc.HiddenPanels.AddRange(New DevExpress.XtraBars.Docking.DockPanel() {Me.dockDetail})
        Me.ocmdoc.TopZIndexControls.AddRange(New String() {"DevExpress.XtraBars.BarDockControl", "DevExpress.XtraBars.StandaloneBarDockControl", "System.Windows.Forms.StatusBar", "System.Windows.Forms.MenuStrip", "System.Windows.Forms.StatusStrip", "DevExpress.XtraBars.Ribbon.RibbonStatusBar", "DevExpress.XtraBars.Ribbon.RibbonControl"})
        '
        'hideContainerTop
        '
        Me.hideContainerTop.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.hideContainerTop.Controls.Add(Me.ogbheader)
        Me.hideContainerTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.hideContainerTop.Location = New System.Drawing.Point(21, 0)
        Me.hideContainerTop.Name = "hideContainerTop"
        Me.hideContainerTop.Size = New System.Drawing.Size(1258, 40)
        '
        'ogbheader
        '
        Me.ogbheader.Controls.Add(Me.DockPanel1_Container)
        Me.ogbheader.Dock = DevExpress.XtraBars.Docking.DockingStyle.Top
        Me.ogbheader.ID = New System.Guid("6a838d1f-4f6f-4734-9f6d-4c809fcfd587")
        Me.ogbheader.ImageOptions.Image = CType(resources.GetObject("ogbheader.ImageOptions.Image"), System.Drawing.Image)
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
        Me.ogbheader.OriginalSize = New System.Drawing.Size(200, 84)
        Me.ogbheader.SavedDock = DevExpress.XtraBars.Docking.DockingStyle.Top
        Me.ogbheader.SavedIndex = 0
        Me.ogbheader.Size = New System.Drawing.Size(1226, 84)
        Me.ogbheader.Text = "Criteria"
        Me.ogbheader.Visibility = DevExpress.XtraBars.Docking.DockVisibility.AutoHide
        '
        'DockPanel1_Container
        '
        Me.DockPanel1_Container.Controls.Add(Me.EFTDateTrans)
        Me.DockPanel1_Container.Controls.Add(Me.FNHSysCmpId_None)
        Me.DockPanel1_Container.Controls.Add(Me.EFTDateTrans_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.SFTDateTrans)
        Me.DockPanel1_Container.Controls.Add(Me.FNHSysCmpId_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.SFTDateTrans_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FNHSysCmpId)
        Me.DockPanel1_Container.Location = New System.Drawing.Point(3, 19)
        Me.DockPanel1_Container.Name = "DockPanel1_Container"
        Me.DockPanel1_Container.Size = New System.Drawing.Size(1044, 46)
        Me.DockPanel1_Container.TabIndex = 0
        '
        'EFTDateTrans
        '
        Me.EFTDateTrans.EditValue = Nothing
        Me.EFTDateTrans.EnterMoveNextControl = True
        Me.EFTDateTrans.Location = New System.Drawing.Point(397, 22)
        Me.EFTDateTrans.Name = "EFTDateTrans"
        Me.EFTDateTrans.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.EFTDateTrans.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.EFTDateTrans.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.EFTDateTrans.Properties.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.EFTDateTrans.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.EFTDateTrans.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.EFTDateTrans.Properties.NullDate = ""
        Me.EFTDateTrans.Size = New System.Drawing.Size(120, 20)
        Me.EFTDateTrans.TabIndex = 397
        Me.EFTDateTrans.Tag = "2|"
        '
        'FNHSysCmpId_None
        '
        Me.FNHSysCmpId_None.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FNHSysCmpId_None.Location = New System.Drawing.Point(246, 0)
        Me.FNHSysCmpId_None.Name = "FNHSysCmpId_None"
        Me.FNHSysCmpId_None.Properties.Appearance.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysCmpId_None.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysCmpId_None.Properties.ReadOnly = True
        Me.FNHSysCmpId_None.Size = New System.Drawing.Size(965, 20)
        Me.FNHSysCmpId_None.TabIndex = 505
        Me.FNHSysCmpId_None.Tag = ""
        '
        'EFTDateTrans_lbl
        '
        Me.EFTDateTrans_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.EFTDateTrans_lbl.Appearance.Options.UseForeColor = True
        Me.EFTDateTrans_lbl.Appearance.Options.UseTextOptions = True
        Me.EFTDateTrans_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.EFTDateTrans_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.EFTDateTrans_lbl.Location = New System.Drawing.Point(284, 22)
        Me.EFTDateTrans_lbl.Name = "EFTDateTrans_lbl"
        Me.EFTDateTrans_lbl.Size = New System.Drawing.Size(110, 20)
        Me.EFTDateTrans_lbl.TabIndex = 398
        Me.EFTDateTrans_lbl.Tag = "2|"
        Me.EFTDateTrans_lbl.Text = "ถึงวันที่ :"
        '
        'SFTDateTrans
        '
        Me.SFTDateTrans.EditValue = Nothing
        Me.SFTDateTrans.EnterMoveNextControl = True
        Me.SFTDateTrans.Location = New System.Drawing.Point(133, 22)
        Me.SFTDateTrans.Name = "SFTDateTrans"
        Me.SFTDateTrans.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.SFTDateTrans.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.SFTDateTrans.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.SFTDateTrans.Properties.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.SFTDateTrans.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.SFTDateTrans.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.SFTDateTrans.Properties.NullDate = ""
        Me.SFTDateTrans.Size = New System.Drawing.Size(120, 20)
        Me.SFTDateTrans.TabIndex = 395
        Me.SFTDateTrans.Tag = "2|"
        '
        'FNHSysCmpId_lbl
        '
        Me.FNHSysCmpId_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysCmpId_lbl.Appearance.Options.UseForeColor = True
        Me.FNHSysCmpId_lbl.Appearance.Options.UseTextOptions = True
        Me.FNHSysCmpId_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysCmpId_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysCmpId_lbl.Location = New System.Drawing.Point(6, 0)
        Me.FNHSysCmpId_lbl.Name = "FNHSysCmpId_lbl"
        Me.FNHSysCmpId_lbl.Size = New System.Drawing.Size(123, 17)
        Me.FNHSysCmpId_lbl.TabIndex = 504
        Me.FNHSysCmpId_lbl.Tag = "2|"
        Me.FNHSysCmpId_lbl.Text = "Company :"
        '
        'SFTDateTrans_lbl
        '
        Me.SFTDateTrans_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.SFTDateTrans_lbl.Appearance.Options.UseForeColor = True
        Me.SFTDateTrans_lbl.Appearance.Options.UseTextOptions = True
        Me.SFTDateTrans_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.SFTDateTrans_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.SFTDateTrans_lbl.Location = New System.Drawing.Point(20, 22)
        Me.SFTDateTrans_lbl.Name = "SFTDateTrans_lbl"
        Me.SFTDateTrans_lbl.Size = New System.Drawing.Size(110, 20)
        Me.SFTDateTrans_lbl.TabIndex = 396
        Me.SFTDateTrans_lbl.Tag = "2|"
        Me.SFTDateTrans_lbl.Text = "วันที่ :"
        '
        'FNHSysCmpId
        '
        Me.FNHSysCmpId.Location = New System.Drawing.Point(132, 0)
        Me.FNHSysCmpId.Name = "FNHSysCmpId"
        Me.FNHSysCmpId.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysCmpId.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysCmpId.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNHSysCmpId.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNHSysCmpId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, False, False, EditorButtonImageOptions1, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, SerializableAppearanceObject2, SerializableAppearanceObject3, SerializableAppearanceObject4, "", "11", Nothing, DevExpress.Utils.ToolTipAnchor.[Default])})
        Me.FNHSysCmpId.Properties.ReadOnly = True
        Me.FNHSysCmpId.Properties.Tag = ""
        Me.FNHSysCmpId.Size = New System.Drawing.Size(113, 20)
        Me.FNHSysCmpId.TabIndex = 503
        Me.FNHSysCmpId.Tag = ""
        '
        'hideContainerLeft
        '
        Me.hideContainerLeft.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.hideContainerLeft.Controls.Add(Me.oDockPanelDetail)
        Me.hideContainerLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.hideContainerLeft.Location = New System.Drawing.Point(0, 0)
        Me.hideContainerLeft.Name = "hideContainerLeft"
        Me.hideContainerLeft.Size = New System.Drawing.Size(21, 980)
        '
        'oDockPanelDetail
        '
        Me.oDockPanelDetail.Controls.Add(Me.ControlContainer2)
        Me.oDockPanelDetail.Dock = DevExpress.XtraBars.Docking.DockingStyle.Left
        Me.oDockPanelDetail.FloatSize = New System.Drawing.Size(502, 609)
        Me.oDockPanelDetail.ID = New System.Guid("d9fcf8de-6519-4cdc-9695-0fe0abe22e3e")
        Me.oDockPanelDetail.Location = New System.Drawing.Point(0, 0)
        Me.oDockPanelDetail.Name = "oDockPanelDetail"
        Me.oDockPanelDetail.OriginalSize = New System.Drawing.Size(500, 200)
        Me.oDockPanelDetail.SavedDock = DevExpress.XtraBars.Docking.DockingStyle.Left
        Me.oDockPanelDetail.SavedIndex = 0
        Me.oDockPanelDetail.Size = New System.Drawing.Size(500, 942)
        Me.oDockPanelDetail.TabsPosition = DevExpress.XtraBars.Docking.TabsPosition.Left
        Me.oDockPanelDetail.TabsScroll = True
        Me.oDockPanelDetail.Text = "DockPanel1"
        Me.oDockPanelDetail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.AutoHide
        '
        'ControlContainer2
        '
        Me.ControlContainer2.Controls.Add(Me.ogrpSubChart)
        Me.ControlContainer2.Controls.Add(Me.ogcDefectDetail)
        Me.ControlContainer2.Controls.Add(Me.ogrpGrird)
        Me.ControlContainer2.Location = New System.Drawing.Point(3, 19)
        Me.ControlContainer2.Name = "ControlContainer2"
        Me.ControlContainer2.Size = New System.Drawing.Size(422, 743)
        Me.ControlContainer2.TabIndex = 0
        '
        'ogrpSubChart
        '
        Me.ogrpSubChart.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogrpSubChart.Controls.Add(Me.ochartSubDefect)
        Me.ogrpSubChart.Location = New System.Drawing.Point(4, 272)
        Me.ogrpSubChart.Name = "ogrpSubChart"
        Me.ogrpSubChart.Size = New System.Drawing.Size(485, 740)
        Me.ogrpSubChart.TabIndex = 1
        Me.ogrpSubChart.Text = "Sub Chart"
        '
        'ochartSubDefect
        '
        XyDiagram1.AxisX.VisibleInPanesSerializable = "-1"
        XyDiagram1.AxisY.VisibleInPanesSerializable = "-1"
        Me.ochartSubDefect.Diagram = XyDiagram1
        Me.ochartSubDefect.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ochartSubDefect.Legend.Name = "Default Legend"
        Me.ochartSubDefect.Location = New System.Drawing.Point(2, 23)
        Me.ochartSubDefect.Name = "ochartSubDefect"
        Series1.Name = "Series 1"
        Me.ochartSubDefect.SeriesSerializable = New DevExpress.XtraCharts.Series() {Series1}
        Me.ochartSubDefect.Size = New System.Drawing.Size(481, 715)
        Me.ochartSubDefect.TabIndex = 0
        '
        'ogcDefectDetail
        '
        Me.ogcDefectDetail.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        GridLevelNode1.RelationName = "Level1"
        Me.ogcDefectDetail.LevelTree.Nodes.AddRange(New DevExpress.XtraGrid.GridLevelNode() {GridLevelNode1})
        Me.ogcDefectDetail.Location = New System.Drawing.Point(2, 0)
        Me.ogcDefectDetail.MainView = Me.ogvDefectDetail
        Me.ogcDefectDetail.Name = "ogcDefectDetail"
        Me.ogcDefectDetail.Size = New System.Drawing.Size(486, 266)
        Me.ogcDefectDetail.TabIndex = 0
        Me.ogcDefectDetail.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvDefectDetail})
        '
        'ogvDefectDetail
        '
        Me.ogvDefectDetail.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.cdFNHSysStyleId, Me.cdFNHSysUnitSectId, Me.cdFTOrderNo, Me.cdFDQADate, Me.cdFTPORef, Me.cdFTStyleCode, Me.cdFNQAInQty, Me.cdFNQAAqlQty, Me.cdFNQAActualQty, Me.cdFNTotalDefect})
        Me.ogvDefectDetail.DetailHeight = 284
        Me.ogvDefectDetail.GridControl = Me.ogcDefectDetail
        Me.ogvDefectDetail.Name = "ogvDefectDetail"
        Me.ogvDefectDetail.OptionsView.ColumnAutoWidth = False
        Me.ogvDefectDetail.OptionsView.ShowGroupPanel = False
        '
        'cdFNHSysStyleId
        '
        Me.cdFNHSysStyleId.Caption = "FNHSysStyleId"
        Me.cdFNHSysStyleId.FieldName = "FNHSysStyleId"
        Me.cdFNHSysStyleId.MinWidth = 17
        Me.cdFNHSysStyleId.Name = "cdFNHSysStyleId"
        Me.cdFNHSysStyleId.Width = 64
        '
        'cdFNHSysUnitSectId
        '
        Me.cdFNHSysUnitSectId.Caption = "FNHSysUnitSectId"
        Me.cdFNHSysUnitSectId.FieldName = "FNHSysUnitSectId"
        Me.cdFNHSysUnitSectId.MinWidth = 17
        Me.cdFNHSysUnitSectId.Name = "cdFNHSysUnitSectId"
        Me.cdFNHSysUnitSectId.Width = 64
        '
        'cdFTOrderNo
        '
        Me.cdFTOrderNo.Caption = "FTOrderNo"
        Me.cdFTOrderNo.FieldName = "FTOrderNo"
        Me.cdFTOrderNo.MinWidth = 17
        Me.cdFTOrderNo.Name = "cdFTOrderNo"
        Me.cdFTOrderNo.Visible = True
        Me.cdFTOrderNo.VisibleIndex = 2
        Me.cdFTOrderNo.Width = 93
        '
        'cdFDQADate
        '
        Me.cdFDQADate.Caption = "FDQADate"
        Me.cdFDQADate.FieldName = "FDQADate"
        Me.cdFDQADate.MinWidth = 17
        Me.cdFDQADate.Name = "cdFDQADate"
        Me.cdFDQADate.Visible = True
        Me.cdFDQADate.VisibleIndex = 0
        Me.cdFDQADate.Width = 81
        '
        'cdFTPORef
        '
        Me.cdFTPORef.Caption = "FTPORef"
        Me.cdFTPORef.FieldName = "FTPORef"
        Me.cdFTPORef.MinWidth = 17
        Me.cdFTPORef.Name = "cdFTPORef"
        Me.cdFTPORef.Visible = True
        Me.cdFTPORef.VisibleIndex = 3
        Me.cdFTPORef.Width = 114
        '
        'cdFTStyleCode
        '
        Me.cdFTStyleCode.Caption = "FTStyleCode"
        Me.cdFTStyleCode.FieldName = "FTStyleCode"
        Me.cdFTStyleCode.MinWidth = 17
        Me.cdFTStyleCode.Name = "cdFTStyleCode"
        Me.cdFTStyleCode.Visible = True
        Me.cdFTStyleCode.VisibleIndex = 1
        Me.cdFTStyleCode.Width = 90
        '
        'cdFNQAInQty
        '
        Me.cdFNQAInQty.Caption = "FNQAInQty"
        Me.cdFNQAInQty.FieldName = "FNQAInQty"
        Me.cdFNQAInQty.MinWidth = 17
        Me.cdFNQAInQty.Name = "cdFNQAInQty"
        Me.cdFNQAInQty.Visible = True
        Me.cdFNQAInQty.VisibleIndex = 4
        Me.cdFNQAInQty.Width = 88
        '
        'cdFNQAAqlQty
        '
        Me.cdFNQAAqlQty.Caption = "FNQAAqlQty"
        Me.cdFNQAAqlQty.FieldName = "FNQAAqlQty"
        Me.cdFNQAAqlQty.MinWidth = 17
        Me.cdFNQAAqlQty.Name = "cdFNQAAqlQty"
        Me.cdFNQAAqlQty.Width = 64
        '
        'cdFNQAActualQty
        '
        Me.cdFNQAActualQty.Caption = "FNQAActualQty"
        Me.cdFNQAActualQty.FieldName = "FNQAActualQty"
        Me.cdFNQAActualQty.MinWidth = 17
        Me.cdFNQAActualQty.Name = "cdFNQAActualQty"
        Me.cdFNQAActualQty.Width = 64
        '
        'cdFNTotalDefect
        '
        Me.cdFNTotalDefect.Caption = "FNTotalDefect"
        Me.cdFNTotalDefect.FieldName = "FNTotalDefect"
        Me.cdFNTotalDefect.MinWidth = 17
        Me.cdFNTotalDefect.Name = "cdFNTotalDefect"
        Me.cdFNTotalDefect.Visible = True
        Me.cdFNTotalDefect.VisibleIndex = 5
        Me.cdFNTotalDefect.Width = 92
        '
        'ogrpGrird
        '
        Me.ogrpGrird.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogrpGrird.Controls.Add(Me.ogbmainprocbutton)
        Me.ogrpGrird.Controls.Add(Me.ogcMain)
        Me.ogrpGrird.Location = New System.Drawing.Point(27, 18)
        Me.ogrpGrird.Name = "ogrpGrird"
        Me.ogrpGrird.ShowCaption = False
        Me.ogrpGrird.Size = New System.Drawing.Size(1105, 199)
        Me.ogrpGrird.TabIndex = 397
        Me.ogrpGrird.Text = "GroupControl1"
        '
        'ogbmainprocbutton
        '
        Me.ogbmainprocbutton.Controls.Add(Me.ocmexit)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmload)
        Me.ogbmainprocbutton.Location = New System.Drawing.Point(138, 136)
        Me.ogbmainprocbutton.Name = "ogbmainprocbutton"
        Me.ogbmainprocbutton.Size = New System.Drawing.Size(334, 28)
        Me.ogbmainprocbutton.TabIndex = 391
        '
        'ocmexit
        '
        Me.ocmexit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmexit.Location = New System.Drawing.Point(175, 7)
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
        'ogcMain
        '
        Me.ogcMain.Location = New System.Drawing.Point(109, 2)
        Me.ogcMain.MainView = Me.ogvMain
        Me.ogcMain.Name = "ogcMain"
        Me.ogcMain.Size = New System.Drawing.Size(1478, 195)
        Me.ogcMain.TabIndex = 1
        Me.ogcMain.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvMain})
        '
        'ogvMain
        '
        Me.ogvMain.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.cFNHSysUnitSectId, Me.cFTUnitSectCode, Me.cFNQAInQty, Me.cFNQAAqlQty, Me.cFNQAActualQty, Me.cFNMajorQty, Me.cFNMinorQty, Me.cFNAndon})
        Me.ogvMain.DetailHeight = 284
        Me.ogvMain.GridControl = Me.ogcMain
        Me.ogvMain.Name = "ogvMain"
        Me.ogvMain.OptionsView.ColumnAutoWidth = False
        Me.ogvMain.OptionsView.ShowGroupPanel = False
        '
        'cFNHSysUnitSectId
        '
        Me.cFNHSysUnitSectId.Caption = "FNHSysUnitSectId"
        Me.cFNHSysUnitSectId.FieldName = "FNHSysUnitSectId"
        Me.cFNHSysUnitSectId.MinWidth = 17
        Me.cFNHSysUnitSectId.Name = "cFNHSysUnitSectId"
        Me.cFNHSysUnitSectId.OptionsColumn.AllowEdit = False
        Me.cFNHSysUnitSectId.Width = 90
        '
        'cFTUnitSectCode
        '
        Me.cFTUnitSectCode.Caption = "FTUnitSectCode"
        Me.cFTUnitSectCode.FieldName = "FTUnitSectCode"
        Me.cFTUnitSectCode.MinWidth = 17
        Me.cFTUnitSectCode.Name = "cFTUnitSectCode"
        Me.cFTUnitSectCode.OptionsColumn.AllowEdit = False
        Me.cFTUnitSectCode.Visible = True
        Me.cFTUnitSectCode.VisibleIndex = 0
        Me.cFTUnitSectCode.Width = 133
        '
        'cFNQAInQty
        '
        Me.cFNQAInQty.Caption = "FNQAInQty"
        Me.cFNQAInQty.FieldName = "FNQAInQty"
        Me.cFNQAInQty.MinWidth = 17
        Me.cFNQAInQty.Name = "cFNQAInQty"
        Me.cFNQAInQty.OptionsColumn.AllowEdit = False
        Me.cFNQAInQty.Visible = True
        Me.cFNQAInQty.VisibleIndex = 1
        Me.cFNQAInQty.Width = 105
        '
        'cFNQAAqlQty
        '
        Me.cFNQAAqlQty.Caption = "FNQAAqlQty"
        Me.cFNQAAqlQty.FieldName = "FNQAAqlQty"
        Me.cFNQAAqlQty.MinWidth = 17
        Me.cFNQAAqlQty.Name = "cFNQAAqlQty"
        Me.cFNQAAqlQty.OptionsColumn.AllowEdit = False
        Me.cFNQAAqlQty.Visible = True
        Me.cFNQAAqlQty.VisibleIndex = 2
        Me.cFNQAAqlQty.Width = 91
        '
        'cFNQAActualQty
        '
        Me.cFNQAActualQty.Caption = "FNQAActualQty"
        Me.cFNQAActualQty.FieldName = "FNQAActualQty"
        Me.cFNQAActualQty.MinWidth = 17
        Me.cFNQAActualQty.Name = "cFNQAActualQty"
        Me.cFNQAActualQty.OptionsColumn.AllowEdit = False
        Me.cFNQAActualQty.Visible = True
        Me.cFNQAActualQty.VisibleIndex = 3
        Me.cFNQAActualQty.Width = 95
        '
        'cFNMajorQty
        '
        Me.cFNMajorQty.Caption = "FNMajorQty"
        Me.cFNMajorQty.FieldName = "FNMajorQty"
        Me.cFNMajorQty.MinWidth = 17
        Me.cFNMajorQty.Name = "cFNMajorQty"
        Me.cFNMajorQty.OptionsColumn.AllowEdit = False
        Me.cFNMajorQty.Visible = True
        Me.cFNMajorQty.VisibleIndex = 4
        Me.cFNMajorQty.Width = 85
        '
        'cFNMinorQty
        '
        Me.cFNMinorQty.Caption = "FNMinorQty"
        Me.cFNMinorQty.FieldName = "FNMinorQty"
        Me.cFNMinorQty.MinWidth = 17
        Me.cFNMinorQty.Name = "cFNMinorQty"
        Me.cFNMinorQty.OptionsColumn.AllowEdit = False
        Me.cFNMinorQty.Visible = True
        Me.cFNMinorQty.VisibleIndex = 5
        '
        'cFNAndon
        '
        Me.cFNAndon.Caption = "FNAndon"
        Me.cFNAndon.FieldName = "FNAndon"
        Me.cFNAndon.MinWidth = 17
        Me.cFNAndon.Name = "cFNAndon"
        Me.cFNAndon.OptionsColumn.AllowEdit = False
        Me.cFNAndon.Visible = True
        Me.cFNAndon.VisibleIndex = 6
        '
        'dockDetail
        '
        Me.dockDetail.Controls.Add(Me.ControlContainer1)
        Me.dockDetail.Dock = DevExpress.XtraBars.Docking.DockingStyle.Left
        Me.dockDetail.ID = New System.Guid("4e5cb227-0643-4f12-b0eb-4ff836533f93")
        Me.dockDetail.Location = New System.Drawing.Point(0, 0)
        Me.dockDetail.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.dockDetail.Name = "dockDetail"
        Me.dockDetail.OriginalSize = New System.Drawing.Size(500, 200)
        Me.dockDetail.SavedDock = DevExpress.XtraBars.Docking.DockingStyle.Left
        Me.dockDetail.SavedIndex = 0
        Me.dockDetail.Size = New System.Drawing.Size(429, 570)
        Me.dockDetail.Text = "Detail"
        Me.dockDetail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden
        '
        'ControlContainer1
        '
        Me.ControlContainer1.Controls.Add(Me.ogcDetailMain)
        Me.ControlContainer1.Location = New System.Drawing.Point(3, 19)
        Me.ControlContainer1.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.ControlContainer1.Name = "ControlContainer1"
        Me.ControlContainer1.Size = New System.Drawing.Size(422, 548)
        Me.ControlContainer1.TabIndex = 0
        '
        'ogcDetailMain
        '
        Me.ogcDetailMain.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogcDetailMain.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.ogcDetailMain.Location = New System.Drawing.Point(0, 1)
        Me.ogcDetailMain.MainView = Me.ogvDetailMain
        Me.ogcDetailMain.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.ogcDetailMain.Name = "ogcDetailMain"
        Me.ogcDetailMain.Size = New System.Drawing.Size(422, 288)
        Me.ogcDetailMain.TabIndex = 0
        Me.ogcDetailMain.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvDetailMain})
        '
        'ogvDetailMain
        '
        Me.ogvDetailMain.DetailHeight = 284
        Me.ogvDetailMain.GridControl = Me.ogcDetailMain
        Me.ogvDetailMain.Name = "ogvDetailMain"
        '
        'ocViewType
        '
        Me.ocViewType.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocViewType.EditValue = "Line"
        Me.ocViewType.Location = New System.Drawing.Point(214, -2)
        Me.ocViewType.Name = "ocViewType"
        Me.ocViewType.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ocViewType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.ocViewType.Size = New System.Drawing.Size(154, 20)
        Me.ocViewType.TabIndex = 5
        '
        'ocViewType_lbl
        '
        Me.ocViewType_lbl.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocViewType_lbl.Appearance.Options.UseTextOptions = True
        Me.ocViewType_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.ocViewType_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.ocViewType_lbl.Location = New System.Drawing.Point(83, 0)
        Me.ocViewType_lbl.Name = "ocViewType_lbl"
        Me.ocViewType_lbl.Size = New System.Drawing.Size(125, 17)
        Me.ocViewType_lbl.TabIndex = 4
        Me.ocViewType_lbl.Text = "Chart Type:"
        '
        'FTSeriesName
        '
        Me.FTSeriesName.Location = New System.Drawing.Point(138, 57)
        Me.FTSeriesName.Name = "FTSeriesName"
        Me.FTSeriesName.Size = New System.Drawing.Size(53, 13)
        Me.FTSeriesName.TabIndex = 0
        Me.FTSeriesName.Text = "Defect Qty"
        Me.FTSeriesName.Visible = False
        '
        'FTTitleChart
        '
        Me.FTTitleChart.Location = New System.Drawing.Point(138, 38)
        Me.FTTitleChart.Name = "FTTitleChart"
        Me.FTTitleChart.Size = New System.Drawing.Size(88, 13)
        Me.FTTitleChart.TabIndex = 0
        Me.FTTitleChart.Text = "Defect Chart Daily"
        Me.FTTitleChart.Visible = False
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
        '
        'comboChartType
        '
        Me.comboChartType.Location = New System.Drawing.Point(0, 0)
        Me.comboChartType.Name = "comboChartType"
        Me.comboChartType.Size = New System.Drawing.Size(100, 20)
        Me.comboChartType.TabIndex = 0
        '
        'checkShowPointLabels
        '
        Me.checkShowPointLabels.Location = New System.Drawing.Point(356, 12)
        Me.checkShowPointLabels.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.checkShowPointLabels.Name = "checkShowPointLabels"
        Me.checkShowPointLabels.Properties.AutoWidth = True
        Me.checkShowPointLabels.Properties.Caption = "Show Point Labels"
        Me.checkShowPointLabels.Size = New System.Drawing.Size(109, 19)
        Me.checkShowPointLabels.TabIndex = 4
        Me.checkShowPointLabels.ToolTip = "Toggles whether value labels are shown in the Chart control"
        '
        'ceChartDataVertical
        '
        Me.ceChartDataVertical.Location = New System.Drawing.Point(356, 39)
        Me.ceChartDataVertical.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ceChartDataVertical.Name = "ceChartDataVertical"
        Me.ceChartDataVertical.Properties.AutoWidth = True
        Me.ceChartDataVertical.Properties.Caption = "Generate Series from Columns"
        Me.ceChartDataVertical.Size = New System.Drawing.Size(168, 19)
        Me.ceChartDataVertical.TabIndex = 12
        Me.ceChartDataVertical.ToolTip = "Toggles whether series in a chart control are created based on PivotGrid columns " &
    "or rows"
        '
        'ceSelectionOnly
        '
        Me.ceSelectionOnly.Location = New System.Drawing.Point(12, 39)
        Me.ceSelectionOnly.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ceSelectionOnly.Name = "ceSelectionOnly"
        Me.ceSelectionOnly.Properties.AutoWidth = True
        Me.ceSelectionOnly.Properties.Caption = "Selection Only"
        Me.ceSelectionOnly.Size = New System.Drawing.Size(91, 19)
        Me.ceSelectionOnly.TabIndex = 9
        Me.ceSelectionOnly.ToolTip = "Toggles whether all PivotGrid cells or selected cells only should be represented " &
    "in the Chart"
        '
        'ceShowColumnGrandTotals
        '
        Me.ceShowColumnGrandTotals.Location = New System.Drawing.Point(559, 39)
        Me.ceShowColumnGrandTotals.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ceShowColumnGrandTotals.Name = "ceShowColumnGrandTotals"
        Me.ceShowColumnGrandTotals.Properties.AutoWidth = True
        Me.ceShowColumnGrandTotals.Properties.Caption = "Show Column Grand Totals"
        Me.ceShowColumnGrandTotals.Size = New System.Drawing.Size(151, 19)
        Me.ceShowColumnGrandTotals.TabIndex = 13
        Me.ceShowColumnGrandTotals.ToolTip = "Toggles whether column grand total values are shown in the Chart control"
        '
        'ceShowRowGrandTotals
        '
        Me.ceShowRowGrandTotals.EditValue = True
        Me.ceShowRowGrandTotals.Location = New System.Drawing.Point(559, 12)
        Me.ceShowRowGrandTotals.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ceShowRowGrandTotals.Name = "ceShowRowGrandTotals"
        Me.ceShowRowGrandTotals.Properties.AutoWidth = True
        Me.ceShowRowGrandTotals.Properties.Caption = "Show Row Grand Totals"
        Me.ceShowRowGrandTotals.Size = New System.Drawing.Size(137, 19)
        Me.ceShowRowGrandTotals.TabIndex = 7
        Me.ceShowRowGrandTotals.ToolTip = "Toggles whether row grand total values are shown in the Chart control"
        '
        'lblUpdateDelay
        '
        Me.lblUpdateDelay.Location = New System.Drawing.Point(203, 43)
        Me.lblUpdateDelay.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.lblUpdateDelay.Name = "lblUpdateDelay"
        Me.lblUpdateDelay.Size = New System.Drawing.Size(80, 16)
        Me.lblUpdateDelay.TabIndex = 13
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
        'ogrpChart
        '
        Me.ogrpChart.Location = New System.Drawing.Point(5, 24)
        Me.ogrpChart.Name = "ogrpChart"
        Me.ogrpChart.ShowCaption = False
        Me.ogrpChart.Size = New System.Drawing.Size(888, 452)
        Me.ogrpChart.TabIndex = 396
        Me.ogrpChart.Text = "Chart"
        '
        'ComboBoxEdit1
        '
        Me.ComboBoxEdit1.EditValue = "Line"
        Me.ComboBoxEdit1.Location = New System.Drawing.Point(145, 10)
        Me.ComboBoxEdit1.Name = "ComboBoxEdit1"
        Me.ComboBoxEdit1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ComboBoxEdit1.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.ComboBoxEdit1.Size = New System.Drawing.Size(154, 20)
        Me.ComboBoxEdit1.TabIndex = 3
        '
        'LabelControl2
        '
        Me.LabelControl2.Appearance.Options.UseTextOptions = True
        Me.LabelControl2.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LabelControl2.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LabelControl2.Location = New System.Drawing.Point(14, 12)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(125, 17)
        Me.LabelControl2.TabIndex = 2
        Me.LabelControl2.Text = "Chart Type:"
        '
        'oGrpTopData
        '
        Me.oGrpTopData.Controls.Add(Me.ogcTopDefect)
        Me.oGrpTopData.Location = New System.Drawing.Point(97, 371)
        Me.oGrpTopData.Name = "oGrpTopData"
        Me.oGrpTopData.Size = New System.Drawing.Size(350, 378)
        Me.oGrpTopData.TabIndex = 399
        Me.oGrpTopData.Text = "Top Defect"
        '
        'ogcTopDefect
        '
        Me.ogcTopDefect.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogcTopDefect.Location = New System.Drawing.Point(2, 23)
        Me.ogcTopDefect.MainView = Me.ogvTopDefect
        Me.ogcTopDefect.Name = "ogcTopDefect"
        Me.ogcTopDefect.Size = New System.Drawing.Size(346, 353)
        Me.ogcTopDefect.TabIndex = 0
        Me.ogcTopDefect.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvTopDefect})
        '
        'ogvTopDefect
        '
        Me.ogvTopDefect.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.ctFNHSysQADetailId, Me.ctFTQADetailName, Me.ctQty, Me.ct})
        Me.ogvTopDefect.DetailHeight = 284
        Me.ogvTopDefect.GridControl = Me.ogcTopDefect
        Me.ogvTopDefect.Name = "ogvTopDefect"
        Me.ogvTopDefect.OptionsView.ColumnAutoWidth = False
        Me.ogvTopDefect.OptionsView.ShowGroupPanel = False
        '
        'ctFNHSysQADetailId
        '
        Me.ctFNHSysQADetailId.Caption = "FNHSysQADetailId"
        Me.ctFNHSysQADetailId.FieldName = "FNHSysQADetailId"
        Me.ctFNHSysQADetailId.MinWidth = 17
        Me.ctFNHSysQADetailId.Name = "ctFNHSysQADetailId"
        Me.ctFNHSysQADetailId.Width = 64
        '
        'ctFTQADetailName
        '
        Me.ctFTQADetailName.Caption = "FTQADetailName"
        Me.ctFTQADetailName.FieldName = "FTQADetailName"
        Me.ctFTQADetailName.MinWidth = 17
        Me.ctFTQADetailName.Name = "ctFTQADetailName"
        Me.ctFTQADetailName.OptionsColumn.AllowEdit = False
        Me.ctFTQADetailName.Visible = True
        Me.ctFTQADetailName.VisibleIndex = 0
        Me.ctFTQADetailName.Width = 64
        '
        'ctQty
        '
        Me.ctQty.Caption = "Qty"
        Me.ctQty.FieldName = "Qty"
        Me.ctQty.MinWidth = 17
        Me.ctQty.Name = "ctQty"
        Me.ctQty.OptionsColumn.AllowEdit = False
        Me.ctQty.Visible = True
        Me.ctQty.VisibleIndex = 1
        Me.ctQty.Width = 87
        '
        'ct
        '
        Me.ct.Caption = "DefectPer"
        Me.ct.FieldName = "DefectPer"
        Me.ct.MinWidth = 17
        Me.ct.Name = "ct"
        Me.ct.OptionsColumn.AllowEdit = False
        Me.ct.Visible = True
        Me.ct.VisibleIndex = 2
        Me.ct.Width = 107
        '
        'ocViewTypePie
        '
        Me.ocViewTypePie.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocViewTypePie.EditValue = "Line"
        Me.ocViewTypePie.Location = New System.Drawing.Point(387, 0)
        Me.ocViewTypePie.Name = "ocViewTypePie"
        Me.ocViewTypePie.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ocViewTypePie.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.ocViewTypePie.Size = New System.Drawing.Size(154, 20)
        Me.ocViewTypePie.TabIndex = 7
        '
        'ocViewTypePie_lbl
        '
        Me.ocViewTypePie_lbl.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocViewTypePie_lbl.Appearance.Options.UseTextOptions = True
        Me.ocViewTypePie_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.ocViewTypePie_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.ocViewTypePie_lbl.Location = New System.Drawing.Point(256, 2)
        Me.ocViewTypePie_lbl.Name = "ocViewTypePie_lbl"
        Me.ocViewTypePie_lbl.Size = New System.Drawing.Size(125, 17)
        Me.ocViewTypePie_lbl.TabIndex = 6
        Me.ocViewTypePie_lbl.Text = "Chart Type Pie:"
        '
        'oGrpTopChart
        '
        Me.oGrpTopChart.Dock = System.Windows.Forms.DockStyle.Fill
        Me.oGrpTopChart.Location = New System.Drawing.Point(2, 23)
        Me.oGrpTopChart.Name = "oGrpTopChart"
        Me.oGrpTopChart.ShowCaption = False
        Me.oGrpTopChart.Size = New System.Drawing.Size(537, 584)
        Me.oGrpTopChart.TabIndex = 0
        Me.oGrpTopChart.Text = "Top Defect "
        '
        'ogrpMChart
        '
        Me.ogrpMChart.Controls.Add(Me.ocViewType)
        Me.ogrpMChart.Controls.Add(Me.ogrpChart)
        Me.ogrpMChart.Controls.Add(Me.ocViewType_lbl)
        Me.ogrpMChart.Location = New System.Drawing.Point(122, 97)
        Me.ogrpMChart.Name = "ogrpMChart"
        Me.ogrpMChart.Size = New System.Drawing.Size(568, 223)
        Me.ogrpMChart.TabIndex = 401
        Me.ogrpMChart.Text = "Chart"
        '
        'oGrpMTopChart
        '
        Me.oGrpMTopChart.Controls.Add(Me.ocViewTypePie)
        Me.oGrpMTopChart.Controls.Add(Me.ocViewTypePie_lbl)
        Me.oGrpMTopChart.Controls.Add(Me.oGrpTopChart)
        Me.oGrpMTopChart.Location = New System.Drawing.Point(517, 371)
        Me.oGrpMTopChart.Name = "oGrpMTopChart"
        Me.oGrpMTopChart.Size = New System.Drawing.Size(541, 609)
        Me.oGrpMTopChart.TabIndex = 403
        Me.oGrpMTopChart.Text = "Top Defect"
        '
        'wDailyChat_
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(1296, 733)
        Me.Controls.Add(Me.oGrpMTopChart)
        Me.Controls.Add(Me.oGrpTopData)
        Me.Controls.Add(Me.FTSeriesName)
        Me.Controls.Add(Me.FTTitleChart)
        Me.Controls.Add(Me.ogrpMChart)
        Me.Controls.Add(Me.hideContainerTop)
        Me.Controls.Add(Me.hideContainerLeft)
        Me.Name = "wDailyChat_"
        Me.Text = "QA Daily Chart"
        CType(Me.ocmdoc, System.ComponentModel.ISupportInitialize).EndInit()
        Me.hideContainerTop.ResumeLayout(False)
        Me.ogbheader.ResumeLayout(False)
        Me.DockPanel1_Container.ResumeLayout(False)
        CType(Me.EFTDateTrans.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EFTDateTrans.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysCmpId_None.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SFTDateTrans.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SFTDateTrans.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysCmpId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.hideContainerLeft.ResumeLayout(False)
        Me.oDockPanelDetail.ResumeLayout(False)
        Me.ControlContainer2.ResumeLayout(False)
        CType(Me.ogrpSubChart, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogrpSubChart.ResumeLayout(False)
        CType(XyDiagram1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Series1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ochartSubDefect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogcDefectDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvDefectDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogrpGrird, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogrpGrird.ResumeLayout(False)
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbmainprocbutton.ResumeLayout(False)
        CType(Me.ogcMain, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.dockDetail.ResumeLayout(False)
        Me.ControlContainer1.ResumeLayout(False)
        CType(Me.ogcDetailMain, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvDetailMain, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ocViewType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.comboChartType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.checkShowPointLabels.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ceChartDataVertical.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ceSelectionOnly.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ceShowColumnGrandTotals.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ceShowRowGrandTotals.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.seUpdateDelay.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogrpChart, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ComboBoxEdit1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.oGrpTopData, System.ComponentModel.ISupportInitialize).EndInit()
        Me.oGrpTopData.ResumeLayout(False)
        CType(Me.ogcTopDefect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvTopDefect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ocViewTypePie.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.oGrpTopChart, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogrpMChart, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogrpMChart.ResumeLayout(False)
        CType(Me.oGrpMTopChart, System.ComponentModel.ISupportInitialize).EndInit()
        Me.oGrpMTopChart.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ocmdoc As DevExpress.XtraBars.Docking.DockManager
    Friend WithEvents ogbheader As DevExpress.XtraBars.Docking.DockPanel
    Friend WithEvents DockPanel1_Container As DevExpress.XtraBars.Docking.ControlContainer
    Friend WithEvents FNHSysCmpId_None As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FNHSysCmpId_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysCmpId As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents ogbmainprocbutton As DevExpress.XtraEditors.PanelControl
    Friend WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmload As DevExpress.XtraEditors.SimpleButton
    Private WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Private WithEvents comboChartType As DevExpress.XtraEditors.ComboBoxEdit
    Private WithEvents checkShowPointLabels As DevExpress.XtraEditors.CheckEdit
    Private WithEvents ceChartDataVertical As DevExpress.XtraEditors.CheckEdit
    Private WithEvents ceSelectionOnly As DevExpress.XtraEditors.CheckEdit
    Private WithEvents ceShowColumnGrandTotals As DevExpress.XtraEditors.CheckEdit
    Private WithEvents ceShowRowGrandTotals As DevExpress.XtraEditors.CheckEdit
    Private WithEvents lblUpdateDelay As DevExpress.XtraEditors.LabelControl
    Private WithEvents seUpdateDelay As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents EFTDateTrans As DevExpress.XtraEditors.DateEdit
    Friend WithEvents EFTDateTrans_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents SFTDateTrans As DevExpress.XtraEditors.DateEdit
    Friend WithEvents SFTDateTrans_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTTitleChart As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTSeriesName As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ogrpChart As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ogrpGrird As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ogcMain As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvMain As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents cFNHSysUnitSectId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTUnitSectCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNQAInQty As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNQAAqlQty As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNQAActualQty As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNMajorQty As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNMinorQty As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNAndon As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents hideContainerTop As DevExpress.XtraBars.Docking.AutoHideContainer
    Private WithEvents ComboBoxEdit1 As DevExpress.XtraEditors.ComboBoxEdit
    Private WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Private WithEvents ocViewType As DevExpress.XtraEditors.ComboBoxEdit
    Private WithEvents ocViewType_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents oGrpTopChart As DevExpress.XtraEditors.GroupControl
    Friend WithEvents oGrpTopData As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ogcTopDefect As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvTopDefect As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents ctFNHSysQADetailId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ctFTQADetailName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ctQty As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ct As DevExpress.XtraGrid.Columns.GridColumn
    Private WithEvents ocViewTypePie As DevExpress.XtraEditors.ComboBoxEdit
    Private WithEvents ocViewTypePie_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents oGrpMTopChart As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ogrpMChart As DevExpress.XtraEditors.GroupControl
    Friend WithEvents dockDetail As DevExpress.XtraBars.Docking.DockPanel
    Friend WithEvents ControlContainer1 As DevExpress.XtraBars.Docking.ControlContainer
    Friend WithEvents ogcDetailMain As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvDetailMain As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents oDockPanelDetail As DevExpress.XtraBars.Docking.DockPanel
    Friend WithEvents ControlContainer2 As DevExpress.XtraBars.Docking.ControlContainer
    Friend WithEvents ogrpSubChart As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ochartSubDefect As DevExpress.XtraCharts.ChartControl
    Friend WithEvents ogcDefectDetail As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvDefectDetail As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents cdFNHSysStyleId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cdFNHSysUnitSectId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cdFTOrderNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cdFDQADate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cdFTPORef As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cdFTStyleCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cdFNQAInQty As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cdFNQAAqlQty As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cdFNQAActualQty As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cdFNTotalDefect As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents hideContainerLeft As DevExpress.XtraBars.Docking.AutoHideContainer
End Class

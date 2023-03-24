Imports DevExpress.XtraEditors.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wHRBIEmpLate
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(wHRBIEmpLate))
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim XyDiagram1 As DevExpress.XtraCharts.XYDiagram = New DevExpress.XtraCharts.XYDiagram()
        Dim SideBySideBarSeriesView1 As DevExpress.XtraCharts.SideBySideBarSeriesView = New DevExpress.XtraCharts.SideBySideBarSeriesView()
        Me.ocmdoc = New DevExpress.XtraBars.Docking.DockManager()
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
        Me.pivotGridControl = New DevExpress.XtraPivotGrid.PivotGridControl()
        Me.CFTState = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.CFTUnitSectName = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.CTotal = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.ogbmainprocbutton = New DevExpress.XtraEditors.PanelControl()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmload = New DevExpress.XtraEditors.SimpleButton()
        Me.otb = New DevExpress.XtraTab.XtraTabControl()
        Me.otpdata = New DevExpress.XtraTab.XtraTabPage()
        Me.chartControl = New DevExpress.XtraCharts.ChartControl()
        Me.otpchart = New DevExpress.XtraTab.XtraTabPage()
        Me.panelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.comboChartType = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.checkShowPointLabels = New DevExpress.XtraEditors.CheckEdit()
        Me.ceChartDataVertical = New DevExpress.XtraEditors.CheckEdit()
        Me.ceSelectionOnly = New DevExpress.XtraEditors.CheckEdit()
        Me.ceShowColumnGrandTotals = New DevExpress.XtraEditors.CheckEdit()
        Me.ceShowRowGrandTotals = New DevExpress.XtraEditors.CheckEdit()
        Me.lblUpdateDelay = New DevExpress.XtraEditors.LabelControl()
        Me.seUpdateDelay = New DevExpress.XtraEditors.SpinEdit()
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
        CType(Me.pivotGridControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbmainprocbutton.SuspendLayout()
        CType(Me.otb, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.otb.SuspendLayout()
        Me.otpdata.SuspendLayout()
        CType(Me.chartControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(XyDiagram1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(SideBySideBarSeriesView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.otpchart.SuspendLayout()
        CType(Me.panelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.comboChartType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.checkShowPointLabels.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ceChartDataVertical.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ceSelectionOnly.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ceShowColumnGrandTotals.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ceShowRowGrandTotals.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.seUpdateDelay.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.hideContainerTop.Name = "hideContainerTop"
        Me.hideContainerTop.Size = New System.Drawing.Size(1014, 40)
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
        Me.ogbheader.OriginalSize = New System.Drawing.Size(200, 84)
        Me.ogbheader.SavedDock = DevExpress.XtraBars.Docking.DockingStyle.Top
        Me.ogbheader.SavedIndex = 0
        Me.ogbheader.Size = New System.Drawing.Size(1014, 84)
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
        Me.DockPanel1_Container.Location = New System.Drawing.Point(5, 27)
        Me.DockPanel1_Container.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.DockPanel1_Container.Name = "DockPanel1_Container"
        Me.DockPanel1_Container.Size = New System.Drawing.Size(1004, 50)
        Me.DockPanel1_Container.TabIndex = 0
        '
        'EFTDateTrans
        '
        Me.EFTDateTrans.EditValue = Nothing
        Me.EFTDateTrans.EnterMoveNextControl = True
        Me.EFTDateTrans.Location = New System.Drawing.Point(463, 29)
        Me.EFTDateTrans.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.EFTDateTrans.Name = "EFTDateTrans"
        Me.EFTDateTrans.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.EFTDateTrans.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.EFTDateTrans.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.EFTDateTrans.Properties.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.EFTDateTrans.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.EFTDateTrans.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.EFTDateTrans.Properties.NullDate = ""
        Me.EFTDateTrans.Size = New System.Drawing.Size(140, 22)
        Me.EFTDateTrans.TabIndex = 397
        Me.EFTDateTrans.Tag = "2|"
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
        'EFTDateTrans_lbl
        '
        Me.EFTDateTrans_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.EFTDateTrans_lbl.Appearance.Options.UseForeColor = True
        Me.EFTDateTrans_lbl.Appearance.Options.UseTextOptions = True
        Me.EFTDateTrans_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.EFTDateTrans_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.EFTDateTrans_lbl.Location = New System.Drawing.Point(331, 29)
        Me.EFTDateTrans_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.EFTDateTrans_lbl.Name = "EFTDateTrans_lbl"
        Me.EFTDateTrans_lbl.Size = New System.Drawing.Size(128, 25)
        Me.EFTDateTrans_lbl.TabIndex = 398
        Me.EFTDateTrans_lbl.Tag = "2|"
        Me.EFTDateTrans_lbl.Text = "ถึงวันที่ :"
        '
        'SFTDateTrans
        '
        Me.SFTDateTrans.EditValue = Nothing
        Me.SFTDateTrans.EnterMoveNextControl = True
        Me.SFTDateTrans.Location = New System.Drawing.Point(155, 29)
        Me.SFTDateTrans.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.SFTDateTrans.Name = "SFTDateTrans"
        Me.SFTDateTrans.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.SFTDateTrans.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.SFTDateTrans.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.SFTDateTrans.Properties.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.SFTDateTrans.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.SFTDateTrans.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.SFTDateTrans.Properties.NullDate = ""
        Me.SFTDateTrans.Size = New System.Drawing.Size(140, 22)
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
        Me.FNHSysCmpId_lbl.Location = New System.Drawing.Point(7, 4)
        Me.FNHSysCmpId_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysCmpId_lbl.Name = "FNHSysCmpId_lbl"
        Me.FNHSysCmpId_lbl.Size = New System.Drawing.Size(143, 21)
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
        Me.SFTDateTrans_lbl.Location = New System.Drawing.Point(23, 29)
        Me.SFTDateTrans_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.SFTDateTrans_lbl.Name = "SFTDateTrans_lbl"
        Me.SFTDateTrans_lbl.Size = New System.Drawing.Size(128, 25)
        Me.SFTDateTrans_lbl.TabIndex = 396
        Me.SFTDateTrans_lbl.Tag = "2|"
        Me.SFTDateTrans_lbl.Text = "วันที่ :"
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
        'pivotGridControl
        '
        Me.pivotGridControl.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.pivotGridControl.Cursor = System.Windows.Forms.Cursors.Default
        Me.pivotGridControl.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pivotGridControl.Fields.AddRange(New DevExpress.XtraPivotGrid.PivotGridField() {Me.CFTState, Me.CFTUnitSectName, Me.CTotal})
        Me.pivotGridControl.Location = New System.Drawing.Point(0, 425)
        Me.pivotGridControl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.pivotGridControl.Name = "pivotGridControl"
        Me.pivotGridControl.OptionsChartDataSource.DataProvideMode = DevExpress.XtraPivotGrid.PivotChartDataProvideMode.UseCustomSettings
        Me.pivotGridControl.OptionsChartDataSource.ProvideDataByColumns = False
        Me.pivotGridControl.OptionsChartDataSource.UpdateDelay = 500
        Me.pivotGridControl.OptionsCustomization.AllowDrag = False
        Me.pivotGridControl.OptionsCustomization.AllowEdit = False
        Me.pivotGridControl.OptionsCustomization.AllowFilter = False
        Me.pivotGridControl.OptionsCustomization.AllowFilterBySummary = False
        Me.pivotGridControl.OptionsCustomization.AllowSort = False
        Me.pivotGridControl.OptionsView.ShowRowGrandTotals = False
        Me.pivotGridControl.OptionsView.ShowRowTotals = False
        Me.pivotGridControl.Size = New System.Drawing.Size(1007, 204)
        Me.pivotGridControl.TabIndex = 2
        '
        'CFTState
        '
        Me.CFTState.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea
        Me.CFTState.AreaIndex = 0
        Me.CFTState.Caption = "FTState"
        Me.CFTState.FieldName = "FTState"
        Me.CFTState.Name = "CFTState"
        Me.CFTState.Width = 160
        '
        'CFTUnitSectName
        '
        Me.CFTUnitSectName.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea
        Me.CFTUnitSectName.AreaIndex = 0
        Me.CFTUnitSectName.Caption = "FTUnitSectName"
        Me.CFTUnitSectName.FieldName = "FTUnitSectName"
        Me.CFTUnitSectName.Name = "CFTUnitSectName"
        '
        'CTotal
        '
        Me.CTotal.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
        Me.CTotal.AreaIndex = 0
        Me.CTotal.CellFormat.FormatString = "{0:n2}"
        Me.CTotal.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CTotal.FieldName = "Total"
        Me.CTotal.Name = "CTotal"
        '
        'ogbmainprocbutton
        '
        Me.ogbmainprocbutton.Controls.Add(Me.ocmexit)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmload)
        Me.ogbmainprocbutton.Location = New System.Drawing.Point(110, 4)
        Me.ogbmainprocbutton.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbmainprocbutton.Name = "ogbmainprocbutton"
        Me.ogbmainprocbutton.Size = New System.Drawing.Size(710, 34)
        Me.ogbmainprocbutton.TabIndex = 391
        '
        'ocmexit
        '
        Me.ocmexit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmexit.Location = New System.Drawing.Point(525, 9)
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
        Me.otpdata.Controls.Add(Me.chartControl)
        Me.otpdata.Controls.Add(Me.pivotGridControl)
        Me.otpdata.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.otpdata.Name = "otpdata"
        Me.otpdata.Size = New System.Drawing.Size(1007, 629)
        Me.otpdata.Text = "Data"
        '
        'chartControl
        '
        Me.chartControl.DataBindings = Nothing
        XyDiagram1.AxisX.Label.Font = New System.Drawing.Font("Tahoma", 7.0!)
        XyDiagram1.AxisX.Label.Staggered = True
        XyDiagram1.AxisX.VisibleInPanesSerializable = "-1"
        XyDiagram1.AxisY.VisibleInPanesSerializable = "-1"
        XyDiagram1.EnableAxisXScrolling = True
        Me.chartControl.Diagram = XyDiagram1
        Me.chartControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.chartControl.Legend.MaxHorizontalPercentage = 30.0R
        Me.chartControl.Legend.Name = "Default Legend"
        Me.chartControl.Location = New System.Drawing.Point(0, 0)
        Me.chartControl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.chartControl.Name = "chartControl"
        Me.chartControl.SeriesDataMember = "Series"
        Me.chartControl.SeriesSerializable = New DevExpress.XtraCharts.Series(-1) {}
        Me.chartControl.SeriesTemplate.ArgumentDataMember = "Arguments"
        Me.chartControl.SeriesTemplate.CrosshairLabelPattern = "{S} : {V:c}"
        Me.chartControl.SeriesTemplate.LabelsVisibility = DevExpress.Utils.DefaultBoolean.[False]
        Me.chartControl.SeriesTemplate.ValueDataMembersSerializable = "Values"
        SideBySideBarSeriesView1.BarWidth = 0.5R
        Me.chartControl.SeriesTemplate.View = SideBySideBarSeriesView1
        Me.chartControl.SideBySideBarDistanceFixed = 2
        Me.chartControl.SideBySideBarDistanceVariable = 1.0R
        Me.chartControl.SideBySideEqualBarWidth = False
        Me.chartControl.Size = New System.Drawing.Size(1007, 425)
        Me.chartControl.TabIndex = 4
        '
        'otpchart
        '
        Me.otpchart.Controls.Add(Me.panelControl1)
        Me.otpchart.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.otpchart.Name = "otpchart"
        Me.otpchart.PageVisible = False
        Me.otpchart.Size = New System.Drawing.Size(1007, 629)
        Me.otpchart.Text = "Chart"
        '
        'panelControl1
        '
        Me.panelControl1.Dock = System.Windows.Forms.DockStyle.Top
        Me.panelControl1.Location = New System.Drawing.Point(0, 0)
        Me.panelControl1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.panelControl1.Name = "panelControl1"
        Me.panelControl1.Padding = New System.Windows.Forms.Padding(6)
        Me.panelControl1.Size = New System.Drawing.Size(1007, 223)
        Me.panelControl1.TabIndex = 3
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
        Me.comboChartType.EditValue = "Line"
        Me.comboChartType.Location = New System.Drawing.Point(169, 12)
        Me.comboChartType.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.comboChartType.Name = "comboChartType"
        Me.comboChartType.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.comboChartType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.comboChartType.Size = New System.Drawing.Size(180, 22)
        Me.comboChartType.TabIndex = 3
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
        'wHRBIEmpLate
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1014, 703)
        Me.Controls.Add(Me.otb)
        Me.Controls.Add(Me.ogbmainprocbutton)
        Me.Controls.Add(Me.hideContainerTop)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "wHRBIEmpLate"
        Me.Text = "Emp Late"
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
        CType(Me.pivotGridControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbmainprocbutton.ResumeLayout(False)
        CType(Me.otb, System.ComponentModel.ISupportInitialize).EndInit()
        Me.otb.ResumeLayout(False)
        Me.otpdata.ResumeLayout(False)
        CType(XyDiagram1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(SideBySideBarSeriesView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chartControl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.otpchart.ResumeLayout(False)
        CType(Me.panelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.comboChartType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.checkShowPointLabels.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ceChartDataVertical.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ceSelectionOnly.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ceShowColumnGrandTotals.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ceShowRowGrandTotals.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.seUpdateDelay.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ocmdoc As DevExpress.XtraBars.Docking.DockManager
    Friend WithEvents ogbheader As DevExpress.XtraBars.Docking.DockPanel
    Friend WithEvents DockPanel1_Container As DevExpress.XtraBars.Docking.ControlContainer
    Friend WithEvents FNHSysCmpId_None As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FNHSysCmpId_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysCmpId As DevExpress.XtraEditors.ButtonEdit
    Private WithEvents pivotGridControl As DevExpress.XtraPivotGrid.PivotGridControl
    Private WithEvents CTotal As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents CFTState As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents CFTUnitSectName As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents ogbmainprocbutton As DevExpress.XtraEditors.PanelControl
    Friend WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmload As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents otb As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents otpdata As DevExpress.XtraTab.XtraTabPage
    Private WithEvents chartControl As DevExpress.XtraCharts.ChartControl
    Friend WithEvents otpchart As DevExpress.XtraTab.XtraTabPage
    Private WithEvents panelControl1 As DevExpress.XtraEditors.PanelControl
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
    Friend WithEvents hideContainerTop As DevExpress.XtraBars.Docking.AutoHideContainer
End Class

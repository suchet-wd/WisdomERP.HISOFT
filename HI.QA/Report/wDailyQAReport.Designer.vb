Imports DevExpress.XtraEditors.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wDailyQAReport
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(wDailyQAReport))
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.ocmdoc = New DevExpress.XtraBars.Docking.DockManager(Me.components)
        Me.hideContainerTop = New DevExpress.XtraBars.Docking.AutoHideContainer()
        Me.ogbheader = New DevExpress.XtraBars.Docking.DockPanel()
        Me.DockPanel1_Container = New DevExpress.XtraBars.Docking.ControlContainer()
        Me.FNHSysUnitSectId = New DevExpress.XtraEditors.ButtonEdit()
        Me.FNHSysUnitSectId_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FDEDate = New DevExpress.XtraEditors.DateEdit()
        Me.FNHSysStyleId = New DevExpress.XtraEditors.ButtonEdit()
        Me.FTOrderNo = New DevExpress.XtraEditors.ButtonEdit()
        Me.FNHSysStyleId_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FDSDate = New DevExpress.XtraEditors.DateEdit()
        Me.FNHSysStyleId_None = New DevExpress.XtraEditors.TextEdit()
        Me.FNHSysUnitSectId_None = New DevExpress.XtraEditors.TextEdit()
        Me.FDEDate_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FDSDate_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTOrderNo_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.dockDetail = New DevExpress.XtraBars.Docking.DockPanel()
        Me.ControlContainer1 = New DevExpress.XtraBars.Docking.ControlContainer()
        Me.ogcDetailMain = New DevExpress.XtraGrid.GridControl()
        Me.ogvDetailMain = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.comboChartType = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.checkShowPointLabels = New DevExpress.XtraEditors.CheckEdit()
        Me.ceChartDataVertical = New DevExpress.XtraEditors.CheckEdit()
        Me.ceSelectionOnly = New DevExpress.XtraEditors.CheckEdit()
        Me.ceShowColumnGrandTotals = New DevExpress.XtraEditors.CheckEdit()
        Me.ceShowRowGrandTotals = New DevExpress.XtraEditors.CheckEdit()
        Me.lblUpdateDelay = New DevExpress.XtraEditors.LabelControl()
        Me.seUpdateDelay = New DevExpress.XtraEditors.SpinEdit()
        Me.ComboBoxEdit1 = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.oPanalControlDetail = New DevExpress.XtraEditors.PanelControl()
        Me.XtraScrollableControl1 = New DevExpress.XtraEditors.XtraScrollableControl()
        Me.ogrpDetail = New DevExpress.XtraEditors.GroupControl()
        Me.FTSeriesTopName = New DevExpress.XtraEditors.LabelControl()
        Me.FTTitleTopChart = New DevExpress.XtraEditors.LabelControl()
        Me.FTSeriesDetailName = New DevExpress.XtraEditors.LabelControl()
        Me.FTTitleDetailChart = New DevExpress.XtraEditors.LabelControl()
        Me.ogbmainprocbutton = New DevExpress.XtraEditors.PanelControl()
        Me.ocmpreview = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmload = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.ogcDetailDaily = New DevExpress.XtraGrid.GridControl()
        Me.ogvDetailDaily = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.cNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cDefect = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cTime1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cTime2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cTime3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cTime4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cTime5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cTime6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cTime7 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cTime8 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cTime9 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cTime10 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cTime11 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cTime12 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cTotal = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.oChartDefaul = New DevExpress.XtraCharts.ChartControl()
        Me.imageRpt = New DevExpress.XtraEditors.PictureEdit()
        Me.FTImage = New DevExpress.XtraEditors.PictureEdit()
        Me.imageRpt2 = New DevExpress.XtraEditors.PictureEdit()
        Me.FTImage2 = New DevExpress.XtraEditors.PictureEdit()
        Me.ogrpQualityRate = New DevExpress.XtraEditors.GroupControl()
        Me.FNQualityRate = New DevExpress.XtraEditors.LabelControl()
        Me.ogrpSummaryDefect = New DevExpress.XtraEditors.GroupControl()
        Me.FNPercentDefect_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNPercentDefect = New DevExpress.XtraEditors.LabelControl()
        Me.FNDefectQty = New DevExpress.XtraEditors.LabelControl()
        Me.FNDefectQty_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.ogrpDocument = New DevExpress.XtraEditors.GroupControl()
        Me.oFDQADateTo_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.oFDQADateE = New DevExpress.XtraEditors.TextEdit()
        Me.oFDQADate = New DevExpress.XtraEditors.TextEdit()
        Me.oFTColorway = New DevExpress.XtraEditors.TextEdit()
        Me.oFTOrderNo = New DevExpress.XtraEditors.TextEdit()
        Me.oFNHSysUnitSectId_None = New DevExpress.XtraEditors.TextEdit()
        Me.oFNHSysStyleId_None = New DevExpress.XtraEditors.TextEdit()
        Me.oFNHSysStyleId = New DevExpress.XtraEditors.TextEdit()
        Me.oFNHSysUnitSectId = New DevExpress.XtraEditors.TextEdit()
        Me.oFDQADate_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.oFNHSysStyleId_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.oFTColorway_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.oFNHSysUnitSectId_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.oFTOrderNo_lbl = New DevExpress.XtraEditors.LabelControl()
        CType(Me.ocmdoc, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.hideContainerTop.SuspendLayout()
        Me.ogbheader.SuspendLayout()
        Me.DockPanel1_Container.SuspendLayout()
        CType(Me.FNHSysUnitSectId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FDEDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FDEDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysStyleId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTOrderNo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FDSDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FDSDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysStyleId_None.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysUnitSectId_None.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.dockDetail.SuspendLayout()
        Me.ControlContainer1.SuspendLayout()
        CType(Me.ogcDetailMain, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvDetailMain, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.comboChartType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.checkShowPointLabels.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ceChartDataVertical.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ceSelectionOnly.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ceShowColumnGrandTotals.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ceShowRowGrandTotals.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.seUpdateDelay.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ComboBoxEdit1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.oPanalControlDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.oPanalControlDetail.SuspendLayout()
        Me.XtraScrollableControl1.SuspendLayout()
        CType(Me.ogrpDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogrpDetail.SuspendLayout()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbmainprocbutton.SuspendLayout()
        CType(Me.ogcDetailDaily, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvDetailDaily, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.oChartDefaul, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.imageRpt.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTImage.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.imageRpt2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTImage2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogrpQualityRate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogrpQualityRate.SuspendLayout()
        CType(Me.ogrpSummaryDefect, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogrpSummaryDefect.SuspendLayout()
        CType(Me.ogrpDocument, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogrpDocument.SuspendLayout()
        CType(Me.oFDQADateE.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.oFDQADate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.oFTColorway.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.oFTOrderNo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.oFNHSysUnitSectId_None.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.oFNHSysStyleId_None.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.oFNHSysStyleId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.oFNHSysUnitSectId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ocmdoc
        '
        Me.ocmdoc.AutoHideContainers.AddRange(New DevExpress.XtraBars.Docking.AutoHideContainer() {Me.hideContainerTop})
        Me.ocmdoc.Form = Me
        Me.ocmdoc.HiddenPanels.AddRange(New DevExpress.XtraBars.Docking.DockPanel() {Me.dockDetail})
        Me.ocmdoc.TopZIndexControls.AddRange(New String() {"DevExpress.XtraBars.BarDockControl", "DevExpress.XtraBars.StandaloneBarDockControl", " System.Windows.Forms.StatusBar", " System.Windows.Forms.MenuStrip", " System.Windows.Forms.StatusStrip", "DevExpress.XtraBars.Ribbon.RibbonStatusBar", "DevExpress.XtraBars.Ribbon.RibbonControl"})
        '
        'hideContainerTop
        '
        Me.hideContainerTop.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.hideContainerTop.Controls.Add(Me.ogbheader)
        Me.hideContainerTop.Dock =  System.Windows.Forms.DockStyle.Top
        Me.hideContainerTop.Location = New System.Drawing.Point(0, 0)
        Me.hideContainerTop.Name = "hideContainerTop"
        Me.hideContainerTop.Size = New System.Drawing.Size(1864, 40)
        '
        'ogbheader
        '
        Me.ogbheader.Controls.Add(Me.DockPanel1_Container)
        Me.ogbheader.Dock = DevExpress.XtraBars.Docking.DockingStyle.Top
        Me.ogbheader.ID = New System.Guid("6a838d1f-4f6f-4734-9f6d-4c809fcfd587")
        Me.ogbheader.Image = CType(resources.GetObject("ogbheader.Image"), System.Drawing.Image)
        Me.ogbheader.Location = New System.Drawing.Point(0, 40)
        Me.ogbheader.Margin = New  System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbheader.Name = "ogbheader"
        Me.ogbheader.Options.AllowDockBottom = False
        Me.ogbheader.Options.AllowDockFill = False
        Me.ogbheader.Options.AllowDockLeft = False
        Me.ogbheader.Options.AllowDockRight = False
        Me.ogbheader.Options.AllowFloating = False
        Me.ogbheader.Options.FloatOnDblClick = False
        Me.ogbheader.Options.ShowCloseButton = False
        Me.ogbheader.Options.ShowMaximizeButton = False
        Me.ogbheader.OriginalSize = New System.Drawing.Size(200, 117)
        Me.ogbheader.SavedDock = DevExpress.XtraBars.Docking.DockingStyle.Top
        Me.ogbheader.SavedIndex = 0
        Me.ogbheader.Size = New System.Drawing.Size(1864, 117)
        Me.ogbheader.Text = "Criteria"
        Me.ogbheader.Visibility = DevExpress.XtraBars.Docking.DockVisibility.AutoHide
        '
        'DockPanel1_Container
        '
        Me.DockPanel1_Container.Controls.Add(Me.FNHSysUnitSectId)
        Me.DockPanel1_Container.Controls.Add(Me.FNHSysUnitSectId_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FDEDate)
        Me.DockPanel1_Container.Controls.Add(Me.FNHSysStyleId)
        Me.DockPanel1_Container.Controls.Add(Me.FTOrderNo)
        Me.DockPanel1_Container.Controls.Add(Me.FNHSysStyleId_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FDSDate)
        Me.DockPanel1_Container.Controls.Add(Me.FNHSysStyleId_None)
        Me.DockPanel1_Container.Controls.Add(Me.FNHSysUnitSectId_None)
        Me.DockPanel1_Container.Controls.Add(Me.FDEDate_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FDSDate_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FTOrderNo_lbl)
        Me.DockPanel1_Container.Location = New System.Drawing.Point(5, 27)
        Me.DockPanel1_Container.Margin = New  System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.DockPanel1_Container.Name = "DockPanel1_Container"
        Me.DockPanel1_Container.Size = New System.Drawing.Size(1854, 83)
        Me.DockPanel1_Container.TabIndex = 0
        '
        'FNHSysUnitSectId
        '
        Me.FNHSysUnitSectId.Location = New System.Drawing.Point(169, 2)
        Me.FNHSysUnitSectId.Margin = New  System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysUnitSectId.Name = "FNHSysUnitSectId"
        Me.FNHSysUnitSectId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut( System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", "248", Nothing, True)})
        Me.FNHSysUnitSectId.Size = New System.Drawing.Size(152, 22)
        Me.FNHSysUnitSectId.TabIndex = 0
        '
        'FNHSysUnitSectId_lbl
        '
        Me.FNHSysUnitSectId_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysUnitSectId_lbl.Appearance.Options.UseForeColor = True
        Me.FNHSysUnitSectId_lbl.Appearance.Options.UseTextOptions = True
        Me.FNHSysUnitSectId_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysUnitSectId_lbl.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top
        Me.FNHSysUnitSectId_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysUnitSectId_lbl.Location = New System.Drawing.Point(9, 4)
        Me.FNHSysUnitSectId_lbl.Margin = New  System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysUnitSectId_lbl.Name = "FNHSysUnitSectId_lbl"
        Me.FNHSysUnitSectId_lbl.Size = New System.Drawing.Size(153, 22)
        Me.FNHSysUnitSectId_lbl.TabIndex = 474
        Me.FNHSysUnitSectId_lbl.Tag = "2|"
        Me.FNHSysUnitSectId_lbl.Text = "FNHSysUnitSectId"
        '
        'FDEDate
        '
        Me.FDEDate.EditValue = Nothing
        Me.FDEDate.EnterMoveNextControl = True
        Me.FDEDate.Location = New System.Drawing.Point(427, 33)
        Me.FDEDate.Margin = New  System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FDEDate.Name = "FDEDate"
        Me.FDEDate.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.FDEDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FDEDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.FDEDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FDEDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FDEDate.Properties.NullDate = ""
        Me.FDEDate.Size = New System.Drawing.Size(152, 22)
        Me.FDEDate.TabIndex = 4
        Me.FDEDate.Tag = "2|"
        '
        'FNHSysStyleId
        '
        Me.FNHSysStyleId.Location = New System.Drawing.Point(169, 32)
        Me.FNHSysStyleId.Margin = New  System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysStyleId.Name = "FNHSysStyleId"
        Me.FNHSysStyleId.Properties.AutoHeight = False
        Me.FNHSysStyleId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut( System.Windows.Forms.Keys.None), SerializableAppearanceObject2, "", "225", Nothing, True)})
        Me.FNHSysStyleId.Properties.CharacterCasing =  System.Windows.Forms.CharacterCasing.Upper
        Me.FNHSysStyleId.Properties.Tag = ""
        Me.FNHSysStyleId.Size = New System.Drawing.Size(152, 20)
        Me.FNHSysStyleId.TabIndex = 1
        Me.FNHSysStyleId.Tag = ""
        Me.FNHSysStyleId.Visible = False
        '
        'FTOrderNo
        '
        Me.FTOrderNo.Location = New System.Drawing.Point(169, 62)
        Me.FTOrderNo.Margin = New  System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTOrderNo.Name = "FTOrderNo"
        Me.FTOrderNo.Properties.AutoHeight = False
        Me.FTOrderNo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut( System.Windows.Forms.Keys.None), SerializableAppearanceObject3, "", "141", Nothing, True)})
        Me.FTOrderNo.Size = New System.Drawing.Size(152, 20)
        Me.FTOrderNo.TabIndex = 2
        Me.FTOrderNo.Visible = False
        '
        'FNHSysStyleId_lbl
        '
        Me.FNHSysStyleId_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysStyleId_lbl.Appearance.Options.UseForeColor = True
        Me.FNHSysStyleId_lbl.Appearance.Options.UseTextOptions = True
        Me.FNHSysStyleId_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysStyleId_lbl.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top
        Me.FNHSysStyleId_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysStyleId_lbl.Location = New System.Drawing.Point(15, 33)
        Me.FNHSysStyleId_lbl.Margin = New  System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysStyleId_lbl.Name = "FNHSysStyleId_lbl"
        Me.FNHSysStyleId_lbl.Size = New System.Drawing.Size(147, 21)
        Me.FNHSysStyleId_lbl.TabIndex = 473
        Me.FNHSysStyleId_lbl.Tag = "2|"
        Me.FNHSysStyleId_lbl.Text = "FNHSysStyleId :"
        Me.FNHSysStyleId_lbl.Visible = False
        '
        'FDSDate
        '
        Me.FDSDate.EditValue = Nothing
        Me.FDSDate.EnterMoveNextControl = True
        Me.FDSDate.Location = New System.Drawing.Point(169, 33)
        Me.FDSDate.Margin = New  System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FDSDate.Name = "FDSDate"
        Me.FDSDate.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.FDSDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FDSDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.FDSDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FDSDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FDSDate.Properties.NullDate = ""
        Me.FDSDate.Size = New System.Drawing.Size(149, 22)
        Me.FDSDate.TabIndex = 3
        Me.FDSDate.Tag = "2|"
        '
        'FNHSysStyleId_None
        '
        Me.FNHSysStyleId_None.Anchor = CType((( System.Windows.Forms.AnchorStyles.Top Or  System.Windows.Forms.AnchorStyles.Left) _
            Or  System.Windows.Forms.AnchorStyles.Right),  System.Windows.Forms.AnchorStyles)
        Me.FNHSysStyleId_None.Location = New System.Drawing.Point(324, 32)
        Me.FNHSysStyleId_None.Margin = New  System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysStyleId_None.Name = "FNHSysStyleId_None"
        Me.FNHSysStyleId_None.Properties.Appearance.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysStyleId_None.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysStyleId_None.Properties.AutoHeight = False
        Me.FNHSysStyleId_None.Properties.ReadOnly = True
        Me.FNHSysStyleId_None.Size = New System.Drawing.Size(1684, 20)
        Me.FNHSysStyleId_None.TabIndex = 476
        Me.FNHSysStyleId_None.Tag = ""
        Me.FNHSysStyleId_None.Visible = False
        '
        'FNHSysUnitSectId_None
        '
        Me.FNHSysUnitSectId_None.Anchor = CType((( System.Windows.Forms.AnchorStyles.Top Or  System.Windows.Forms.AnchorStyles.Left) _
            Or  System.Windows.Forms.AnchorStyles.Right),  System.Windows.Forms.AnchorStyles)
        Me.FNHSysUnitSectId_None.Location = New System.Drawing.Point(324, 2)
        Me.FNHSysUnitSectId_None.Margin = New  System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysUnitSectId_None.Name = "FNHSysUnitSectId_None"
        Me.FNHSysUnitSectId_None.Properties.Appearance.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysUnitSectId_None.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysUnitSectId_None.Properties.AutoHeight = False
        Me.FNHSysUnitSectId_None.Properties.ReadOnly = True
        Me.FNHSysUnitSectId_None.Size = New System.Drawing.Size(1684, 20)
        Me.FNHSysUnitSectId_None.TabIndex = 475
        Me.FNHSysUnitSectId_None.Tag = ""
        '
        'FDEDate_lbl
        '
        Me.FDEDate_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FDEDate_lbl.Appearance.Options.UseForeColor = True
        Me.FDEDate_lbl.Appearance.Options.UseTextOptions = True
        Me.FDEDate_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FDEDate_lbl.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top
        Me.FDEDate_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FDEDate_lbl.Location = New System.Drawing.Point(325, 33)
        Me.FDEDate_lbl.Margin = New  System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FDEDate_lbl.Name = "FDEDate_lbl"
        Me.FDEDate_lbl.Size = New System.Drawing.Size(96, 25)
        Me.FDEDate_lbl.TabIndex = 478
        Me.FDEDate_lbl.Tag = "2|"
        Me.FDEDate_lbl.Text = "To :"
        '
        'FDSDate_lbl
        '
        Me.FDSDate_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FDSDate_lbl.Appearance.Options.UseForeColor = True
        Me.FDSDate_lbl.Appearance.Options.UseTextOptions = True
        Me.FDSDate_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FDSDate_lbl.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top
        Me.FDSDate_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FDSDate_lbl.Location = New System.Drawing.Point(3, 36)
        Me.FDSDate_lbl.Margin = New  System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FDSDate_lbl.Name = "FDSDate_lbl"
        Me.FDSDate_lbl.Size = New System.Drawing.Size(156, 21)
        Me.FDSDate_lbl.TabIndex = 478
        Me.FDSDate_lbl.Tag = "2|"
        Me.FDSDate_lbl.Text = "Date :"
        '
        'FTOrderNo_lbl
        '
        Me.FTOrderNo_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTOrderNo_lbl.Appearance.Options.UseForeColor = True
        Me.FTOrderNo_lbl.Appearance.Options.UseTextOptions = True
        Me.FTOrderNo_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTOrderNo_lbl.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top
        Me.FTOrderNo_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTOrderNo_lbl.Location = New System.Drawing.Point(6, 62)
        Me.FTOrderNo_lbl.Margin = New  System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTOrderNo_lbl.Name = "FTOrderNo_lbl"
        Me.FTOrderNo_lbl.Size = New System.Drawing.Size(156, 14)
        Me.FTOrderNo_lbl.TabIndex = 478
        Me.FTOrderNo_lbl.Tag = "2|"
        Me.FTOrderNo_lbl.Text = "FTOrderNo :"
        Me.FTOrderNo_lbl.Visible = False
        '
        'dockDetail
        '
        Me.dockDetail.Controls.Add(Me.ControlContainer1)
        Me.dockDetail.Dock = DevExpress.XtraBars.Docking.DockingStyle.Left
        Me.dockDetail.ID = New System.Guid("4e5cb227-0643-4f12-b0eb-4ff836533f93")
        Me.dockDetail.Location = New System.Drawing.Point(0, 0)
        Me.dockDetail.Name = "dockDetail"
        Me.dockDetail.OriginalSize = New System.Drawing.Size(500, 200)
        Me.dockDetail.SavedDock = DevExpress.XtraBars.Docking.DockingStyle.Left
        Me.dockDetail.SavedIndex = 0
        Me.dockDetail.Size = New System.Drawing.Size(500, 702)
        Me.dockDetail.Text = "Detail"
        Me.dockDetail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden
        '
        'ControlContainer1
        '
        Me.ControlContainer1.Controls.Add(Me.ogcDetailMain)
        Me.ControlContainer1.Location = New System.Drawing.Point(4, 23)
        Me.ControlContainer1.Name = "ControlContainer1"
        Me.ControlContainer1.Size = New System.Drawing.Size(492, 675)
        Me.ControlContainer1.TabIndex = 0
        '
        'ogcDetailMain
        '
        Me.ogcDetailMain.Anchor = CType((( System.Windows.Forms.AnchorStyles.Top Or  System.Windows.Forms.AnchorStyles.Left) _
            Or  System.Windows.Forms.AnchorStyles.Right),  System.Windows.Forms.AnchorStyles)
        Me.ogcDetailMain.Location = New System.Drawing.Point(0, 1)
        Me.ogcDetailMain.MainView = Me.ogvDetailMain
        Me.ogcDetailMain.Name = "ogcDetailMain"
        Me.ogcDetailMain.Size = New System.Drawing.Size(492, 354)
        Me.ogcDetailMain.TabIndex = 0
        Me.ogcDetailMain.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvDetailMain})
        '
        'ogvDetailMain
        '
        Me.ogvDetailMain.GridControl = Me.ogcDetailMain
        Me.ogvDetailMain.Name = "ogvDetailMain"
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Options.UseTextOptions = True
        Me.LabelControl1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LabelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LabelControl1.Location = New System.Drawing.Point(16, 15)
        Me.LabelControl1.Margin = New  System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(146, 21)
        Me.LabelControl1.TabIndex = 2
        '
        'comboChartType
        '
        Me.comboChartType.Location = New System.Drawing.Point(0, 0)
        Me.comboChartType.Name = "comboChartType"
        Me.comboChartType.Size = New System.Drawing.Size(100, 22)
        Me.comboChartType.TabIndex = 0
        '
        'checkShowPointLabels
        '
        Me.checkShowPointLabels.Location = New System.Drawing.Point(356, 12)
        Me.checkShowPointLabels.Margin = New  System.Windows.Forms.Padding(3, 4, 3, 4)
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
        Me.ceChartDataVertical.Margin = New  System.Windows.Forms.Padding(3, 4, 3, 4)
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
        Me.ceSelectionOnly.Margin = New  System.Windows.Forms.Padding(3, 4, 3, 4)
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
        Me.ceShowColumnGrandTotals.Margin = New  System.Windows.Forms.Padding(3, 4, 3, 4)
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
        Me.ceShowRowGrandTotals.Margin = New  System.Windows.Forms.Padding(3, 4, 3, 4)
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
        Me.lblUpdateDelay.Margin = New  System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.lblUpdateDelay.Name = "lblUpdateDelay"
        Me.lblUpdateDelay.Size = New System.Drawing.Size(80, 16)
        Me.lblUpdateDelay.TabIndex = 13
        '
        'seUpdateDelay
        '
        Me.seUpdateDelay.EditValue = New Decimal(New Integer() {500, 0, 0, 0})
        Me.seUpdateDelay.Location = New System.Drawing.Point(293, 39)
        Me.seUpdateDelay.Margin = New  System.Windows.Forms.Padding(3, 4, 3, 4)
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
        'ComboBoxEdit1
        '
        Me.ComboBoxEdit1.EditValue = "Line"
        Me.ComboBoxEdit1.Location = New System.Drawing.Point(145, 10)
        Me.ComboBoxEdit1.Name = "ComboBoxEdit1"
        Me.ComboBoxEdit1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ComboBoxEdit1.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.ComboBoxEdit1.Size = New System.Drawing.Size(154, 22)
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
        'oPanalControlDetail
        '
        Me.oPanalControlDetail.AllowTouchScroll = True
        Me.oPanalControlDetail.AutoSizeMode =  System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.oPanalControlDetail.Controls.Add(Me.XtraScrollableControl1)
        Me.oPanalControlDetail.Dock =  System.Windows.Forms.DockStyle.Fill
        Me.oPanalControlDetail.Location = New System.Drawing.Point(0, 40)
        Me.oPanalControlDetail.Margin = New  System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.oPanalControlDetail.Name = "oPanalControlDetail"
        Me.oPanalControlDetail.Size = New System.Drawing.Size(1864, 930)
        Me.oPanalControlDetail.TabIndex = 0
        '
        'XtraScrollableControl1
        '
        Me.XtraScrollableControl1.Controls.Add(Me.ogrpDetail)
        Me.XtraScrollableControl1.Controls.Add(Me.PanelControl1)
        Me.XtraScrollableControl1.Dock =  System.Windows.Forms.DockStyle.Fill
        Me.XtraScrollableControl1.Location = New System.Drawing.Point(2, 2)
        Me.XtraScrollableControl1.Margin = New  System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.XtraScrollableControl1.Name = "XtraScrollableControl1"
        Me.XtraScrollableControl1.Size = New System.Drawing.Size(1860, 926)
        Me.XtraScrollableControl1.TabIndex = 407
        '
        'ogrpDetail
        '
        Me.ogrpDetail.AppearanceCaption.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.ogrpDetail.AppearanceCaption.Options.UseFont = True
        Me.ogrpDetail.AppearanceCaption.Options.UseTextOptions = True
        Me.ogrpDetail.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ogrpDetail.Controls.Add(Me.FTSeriesTopName)
        Me.ogrpDetail.Controls.Add(Me.FTTitleTopChart)
        Me.ogrpDetail.Controls.Add(Me.FTSeriesDetailName)
        Me.ogrpDetail.Controls.Add(Me.FTTitleDetailChart)
        Me.ogrpDetail.Controls.Add(Me.ogbmainprocbutton)
        Me.ogrpDetail.Controls.Add(Me.ogcDetailDaily)
        Me.ogrpDetail.Dock =  System.Windows.Forms.DockStyle.Fill
        Me.ogrpDetail.Location = New System.Drawing.Point(0, 481)
        Me.ogrpDetail.Margin = New  System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogrpDetail.Name = "ogrpDetail"
        Me.ogrpDetail.RightToLeft =  System.Windows.Forms.RightToLeft.No
        Me.ogrpDetail.Size = New System.Drawing.Size(1860, 445)
        Me.ogrpDetail.TabIndex = 1
        Me.ogrpDetail.Text = "Daily Quality Report"
        '
        'FTSeriesTopName
        '
        Me.FTSeriesTopName.Location = New System.Drawing.Point(680, 176)
        Me.FTSeriesTopName.Margin = New  System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTSeriesTopName.Name = "FTSeriesTopName"
        Me.FTSeriesTopName.Size = New System.Drawing.Size(78, 16)
        Me.FTSeriesTopName.TabIndex = 476
        Me.FTSeriesTopName.Text = "LabelControl1"
        Me.FTSeriesTopName.Visible = False
        '
        'FTTitleTopChart
        '
        Me.FTTitleTopChart.Location = New System.Drawing.Point(790, 176)
        Me.FTTitleTopChart.Margin = New  System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTTitleTopChart.Name = "FTTitleTopChart"
        Me.FTTitleTopChart.Size = New System.Drawing.Size(78, 16)
        Me.FTTitleTopChart.TabIndex = 477
        Me.FTTitleTopChart.Text = "LabelControl1"
        Me.FTTitleTopChart.Visible = False
        '
        'FTSeriesDetailName
        '
        Me.FTSeriesDetailName.Location = New System.Drawing.Point(915, 176)
        Me.FTSeriesDetailName.Margin = New  System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTSeriesDetailName.Name = "FTSeriesDetailName"
        Me.FTSeriesDetailName.Size = New System.Drawing.Size(78, 16)
        Me.FTSeriesDetailName.TabIndex = 478
        Me.FTSeriesDetailName.Text = "LabelControl1"
        Me.FTSeriesDetailName.Visible = False
        '
        'FTTitleDetailChart
        '
        Me.FTTitleDetailChart.Location = New System.Drawing.Point(1102, 176)
        Me.FTTitleDetailChart.Margin = New  System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTTitleDetailChart.Name = "FTTitleDetailChart"
        Me.FTTitleDetailChart.Size = New System.Drawing.Size(78, 16)
        Me.FTTitleDetailChart.TabIndex = 479
        Me.FTTitleDetailChart.Text = "LabelControl1"
        Me.FTTitleDetailChart.Visible = False
        '
        'ogbmainprocbutton
        '
        Me.ogbmainprocbutton.Anchor = CType(( System.Windows.Forms.AnchorStyles.Top Or  System.Windows.Forms.AnchorStyles.Right),  System.Windows.Forms.AnchorStyles)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmpreview)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmload)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmexit)
        Me.ogbmainprocbutton.Location = New System.Drawing.Point(1312, 7)
        Me.ogbmainprocbutton.Margin = New  System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbmainprocbutton.Name = "ogbmainprocbutton"
        Me.ogbmainprocbutton.Size = New System.Drawing.Size(419, 58)
        Me.ogbmainprocbutton.TabIndex = 475
        Me.ogbmainprocbutton.Tag = "2|"
        '
        'ocmpreview
        '
        Me.ocmpreview.Location = New System.Drawing.Point(121, 6)
        Me.ocmpreview.Margin = New  System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmpreview.Name = "ocmpreview"
        Me.ocmpreview.Size = New System.Drawing.Size(124, 31)
        Me.ocmpreview.TabIndex = 334
        Me.ocmpreview.TabStop = False
        Me.ocmpreview.Tag = "2|"
        Me.ocmpreview.Text = "Preview"
        '
        'ocmload
        '
        Me.ocmload.Location = New System.Drawing.Point(6, 6)
        Me.ocmload.Margin = New  System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmload.Name = "ocmload"
        Me.ocmload.Size = New System.Drawing.Size(108, 36)
        Me.ocmload.TabIndex = 97
        Me.ocmload.Text = "Load"
        '
        'ocmexit
        '
        Me.ocmexit.Location = New System.Drawing.Point(391, 172)
        Me.ocmexit.Margin = New  System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmexit.Name = "ocmexit"
        Me.ocmexit.Size = New System.Drawing.Size(111, 31)
        Me.ocmexit.TabIndex = 96
        Me.ocmexit.TabStop = False
        Me.ocmexit.Tag = "2|"
        Me.ocmexit.Text = "EXIT"
        '
        'ogcDetailDaily
        '
        Me.ogcDetailDaily.Dock =  System.Windows.Forms.DockStyle.Fill
        Me.ogcDetailDaily.EmbeddedNavigator.Margin = New  System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcDetailDaily.Location = New System.Drawing.Point(2, 26)
        Me.ogcDetailDaily.MainView = Me.ogvDetailDaily
        Me.ogcDetailDaily.Margin = New  System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcDetailDaily.Name = "ogcDetailDaily"
        Me.ogcDetailDaily.Size = New System.Drawing.Size(1856, 417)
        Me.ogcDetailDaily.TabIndex = 3
        Me.ogcDetailDaily.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvDetailDaily})
        '
        'ogvDetailDaily
        '
        Me.ogvDetailDaily.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.cNo, Me.cDefect, Me.cTime1, Me.cTime2, Me.cTime3, Me.cTime4, Me.cTime5, Me.cTime6, Me.cTime7, Me.cTime8, Me.cTime9, Me.cTime10, Me.cTime11, Me.cTime12, Me.cTotal})
        Me.ogvDetailDaily.GridControl = Me.ogcDetailDaily
        Me.ogvDetailDaily.Name = "ogvDetailDaily"
        Me.ogvDetailDaily.OptionsCustomization.AllowFilter = False
        Me.ogvDetailDaily.OptionsFilter.AllowFilterEditor = False
        Me.ogvDetailDaily.OptionsFind.AllowFindPanel = False
        Me.ogvDetailDaily.OptionsView.ColumnAutoWidth = False
        Me.ogvDetailDaily.OptionsView.ShowFooter = True
        Me.ogvDetailDaily.OptionsView.ShowGroupPanel = False
        '
        'cNo
        '
        Me.cNo.Caption = "No"
        Me.cNo.FieldName = "FNSeq"
        Me.cNo.Name = "cNo"
        Me.cNo.OptionsColumn.AllowEdit = False
        Me.cNo.OptionsFilter.AllowAutoFilter = False
        Me.cNo.Visible = True
        Me.cNo.VisibleIndex = 0
        Me.cNo.Width = 50
        '
        'cDefect
        '
        Me.cDefect.Caption = "Defect"
        Me.cDefect.DisplayFormat.FormatString = "{0:n2}"
        Me.cDefect.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cDefect.FieldName = "FTQADetailName"
        Me.cDefect.Name = "cDefect"
        Me.cDefect.OptionsColumn.AllowEdit = False
        Me.cDefect.OptionsFilter.AllowAutoFilter = False
        Me.cDefect.Visible = True
        Me.cDefect.VisibleIndex = 1
        Me.cDefect.Width = 217
        '
        'cTime1
        '
        Me.cTime1.Caption = "08.00-09.00"
        Me.cTime1.DisplayFormat.FormatString = "{0:n0}"
        Me.cTime1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cTime1.FieldName = "1"
        Me.cTime1.Name = "cTime1"
        Me.cTime1.OptionsColumn.AllowEdit = False
        Me.cTime1.OptionsFilter.AllowAutoFilter = False
        Me.cTime1.Visible = True
        Me.cTime1.VisibleIndex = 2
        Me.cTime1.Width = 71
        '
        'cTime2
        '
        Me.cTime2.Caption = "09.01-10.00"
        Me.cTime2.DisplayFormat.FormatString = "{0:n0}"
        Me.cTime2.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cTime2.FieldName = "2"
        Me.cTime2.Name = "cTime2"
        Me.cTime2.OptionsColumn.AllowEdit = False
        Me.cTime2.OptionsFilter.AllowAutoFilter = False
        Me.cTime2.Visible = True
        Me.cTime2.VisibleIndex = 3
        Me.cTime2.Width = 70
        '
        'cTime3
        '
        Me.cTime3.Caption = "10.01-11.00"
        Me.cTime3.DisplayFormat.FormatString = "{0:n0}"
        Me.cTime3.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cTime3.FieldName = "3"
        Me.cTime3.Name = "cTime3"
        Me.cTime3.OptionsColumn.AllowEdit = False
        Me.cTime3.OptionsFilter.AllowAutoFilter = False
        Me.cTime3.Visible = True
        Me.cTime3.VisibleIndex = 4
        Me.cTime3.Width = 68
        '
        'cTime4
        '
        Me.cTime4.Caption = "11.01-12.00"
        Me.cTime4.DisplayFormat.FormatString = "{0:n0}"
        Me.cTime4.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cTime4.FieldName = "4"
        Me.cTime4.Name = "cTime4"
        Me.cTime4.OptionsColumn.AllowEdit = False
        Me.cTime4.OptionsFilter.AllowAutoFilter = False
        Me.cTime4.Visible = True
        Me.cTime4.VisibleIndex = 5
        Me.cTime4.Width = 72
        '
        'cTime5
        '
        Me.cTime5.Caption = "13.00-14.00"
        Me.cTime5.DisplayFormat.FormatString = "{0:n0}"
        Me.cTime5.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cTime5.FieldName = "5"
        Me.cTime5.Name = "cTime5"
        Me.cTime5.OptionsColumn.AllowEdit = False
        Me.cTime5.OptionsFilter.AllowAutoFilter = False
        Me.cTime5.Visible = True
        Me.cTime5.VisibleIndex = 6
        Me.cTime5.Width = 70
        '
        'cTime6
        '
        Me.cTime6.Caption = "14.01-15.00"
        Me.cTime6.DisplayFormat.FormatString = "{0:n0}"
        Me.cTime6.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cTime6.FieldName = "6"
        Me.cTime6.Name = "cTime6"
        Me.cTime6.OptionsColumn.AllowEdit = False
        Me.cTime6.OptionsFilter.AllowAutoFilter = False
        Me.cTime6.Visible = True
        Me.cTime6.VisibleIndex = 7
        Me.cTime6.Width = 72
        '
        'cTime7
        '
        Me.cTime7.Caption = "15.01-16.00"
        Me.cTime7.DisplayFormat.FormatString = "{0:n0}"
        Me.cTime7.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cTime7.FieldName = "7"
        Me.cTime7.Name = "cTime7"
        Me.cTime7.OptionsColumn.AllowEdit = False
        Me.cTime7.OptionsFilter.AllowAutoFilter = False
        Me.cTime7.Visible = True
        Me.cTime7.VisibleIndex = 8
        Me.cTime7.Width = 73
        '
        'cTime8
        '
        Me.cTime8.Caption = "16.01-17.00"
        Me.cTime8.DisplayFormat.FormatString = "{0:n0}"
        Me.cTime8.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cTime8.FieldName = "8"
        Me.cTime8.Name = "cTime8"
        Me.cTime8.OptionsColumn.AllowEdit = False
        Me.cTime8.OptionsFilter.AllowAutoFilter = False
        Me.cTime8.Visible = True
        Me.cTime8.VisibleIndex = 9
        Me.cTime8.Width = 72
        '
        'cTime9
        '
        Me.cTime9.Caption = "17.30-18.30"
        Me.cTime9.DisplayFormat.FormatString = "{0:n0}"
        Me.cTime9.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cTime9.FieldName = "9"
        Me.cTime9.Name = "cTime9"
        Me.cTime9.OptionsColumn.AllowEdit = False
        Me.cTime9.OptionsFilter.AllowAutoFilter = False
        Me.cTime9.Visible = True
        Me.cTime9.VisibleIndex = 10
        Me.cTime9.Width = 70
        '
        'cTime10
        '
        Me.cTime10.Caption = "18.30-19.30"
        Me.cTime10.DisplayFormat.FormatString = "{0:n0}"
        Me.cTime10.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cTime10.FieldName = "10"
        Me.cTime10.Name = "cTime10"
        Me.cTime10.OptionsColumn.AllowEdit = False
        Me.cTime10.OptionsFilter.AllowAutoFilter = False
        Me.cTime10.Visible = True
        Me.cTime10.VisibleIndex = 11
        Me.cTime10.Width = 69
        '
        'cTime11
        '
        Me.cTime11.Caption = "19.30-20.30"
        Me.cTime11.DisplayFormat.FormatString = "{0:n0}"
        Me.cTime11.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cTime11.FieldName = "11"
        Me.cTime11.Name = "cTime11"
        Me.cTime11.OptionsColumn.AllowEdit = False
        Me.cTime11.OptionsFilter.AllowAutoFilter = False
        Me.cTime11.Visible = True
        Me.cTime11.VisibleIndex = 12
        Me.cTime11.Width = 74
        '
        'cTime12
        '
        Me.cTime12.Caption = "20.30-21.30"
        Me.cTime12.DisplayFormat.FormatString = "{0:n0}"
        Me.cTime12.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cTime12.FieldName = "12"
        Me.cTime12.Name = "cTime12"
        Me.cTime12.OptionsColumn.AllowEdit = False
        Me.cTime12.OptionsFilter.AllowAutoFilter = False
        Me.cTime12.Visible = True
        Me.cTime12.VisibleIndex = 13
        Me.cTime12.Width = 69
        '
        'cTotal
        '
        Me.cTotal.Caption = "Total"
        Me.cTotal.DisplayFormat.FormatString = "{0:n0}"
        Me.cTotal.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cTotal.FieldName = "Total"
        Me.cTotal.Name = "cTotal"
        Me.cTotal.OptionsColumn.AllowEdit = False
        Me.cTotal.OptionsFilter.AllowAutoFilter = False
        Me.cTotal.Visible = True
        Me.cTotal.VisibleIndex = 14
        Me.cTotal.Width = 157
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.oChartDefaul)
        Me.PanelControl1.Controls.Add(Me.imageRpt)
        Me.PanelControl1.Controls.Add(Me.FTImage)
        Me.PanelControl1.Controls.Add(Me.imageRpt2)
        Me.PanelControl1.Controls.Add(Me.FTImage2)
        Me.PanelControl1.Controls.Add(Me.ogrpQualityRate)
        Me.PanelControl1.Controls.Add(Me.ogrpSummaryDefect)
        Me.PanelControl1.Controls.Add(Me.ogrpDocument)
        Me.PanelControl1.Dock =  System.Windows.Forms.DockStyle.Top
        Me.PanelControl1.Location = New System.Drawing.Point(0, 0)
        Me.PanelControl1.Margin = New  System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(1860, 481)
        Me.PanelControl1.TabIndex = 0
        '
        'oChartDefaul
        '
        Me.oChartDefaul.DataBindings = Nothing
        Me.oChartDefaul.Legend.Name = "Default Legend"
        Me.oChartDefaul.Location = New System.Drawing.Point(790, 5)
        Me.oChartDefaul.Name = "oChartDefaul"
        Me.oChartDefaul.SeriesSerializable = New DevExpress.XtraCharts.Series(-1) {}
        Me.oChartDefaul.Size = New System.Drawing.Size(613, 469)
        Me.oChartDefaul.TabIndex = 479
        '
        'imageRpt
        '
        Me.imageRpt.AllowDrop = True
        Me.imageRpt.Cursor =  System.Windows.Forms.Cursors.Default
        Me.imageRpt.Location = New System.Drawing.Point(1, 13)
        Me.imageRpt.Margin = New  System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.imageRpt.Name = "imageRpt"
        Me.imageRpt.Properties.ShowMenu = False
        Me.imageRpt.Properties.ShowZoomSubMenu = DevExpress.Utils.DefaultBoolean.[False]
        Me.imageRpt.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch
        Me.imageRpt.Properties.ZoomAccelerationFactor = 1.0R
        Me.imageRpt.ShowToolTips = False
        Me.imageRpt.Size = New System.Drawing.Size(389, 468)
        Me.imageRpt.TabIndex = 474
        Me.imageRpt.Tag = ""
        Me.imageRpt.Visible = False
        '
        'FTImage
        '
        Me.FTImage.AllowDrop = True
        Me.FTImage.Cursor =  System.Windows.Forms.Cursors.Default
        Me.FTImage.Location = New System.Drawing.Point(0, 5)
        Me.FTImage.Margin = New  System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTImage.Name = "FTImage"
        Me.FTImage.Properties.ShowMenu = False
        Me.FTImage.Properties.ShowZoomSubMenu = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTImage.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch
        Me.FTImage.Properties.ZoomAccelerationFactor = 1.0R
        Me.FTImage.ShowToolTips = False
        Me.FTImage.Size = New System.Drawing.Size(389, 468)
        Me.FTImage.TabIndex = 474
        Me.FTImage.Tag = ""
        '
        'imageRpt2
        '
        Me.imageRpt2.AllowDrop = True
        Me.imageRpt2.Cursor =  System.Windows.Forms.Cursors.Default
        Me.imageRpt2.Location = New System.Drawing.Point(396, 13)
        Me.imageRpt2.Margin = New  System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.imageRpt2.Name = "imageRpt2"
        Me.imageRpt2.Properties.ShowMenu = False
        Me.imageRpt2.Properties.ShowZoomSubMenu = DevExpress.Utils.DefaultBoolean.[False]
        Me.imageRpt2.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch
        Me.imageRpt2.Properties.ZoomAccelerationFactor = 1.0R
        Me.imageRpt2.ShowToolTips = False
        Me.imageRpt2.Size = New System.Drawing.Size(389, 468)
        Me.imageRpt2.TabIndex = 473
        Me.imageRpt2.Tag = ""
        Me.imageRpt2.Visible = False
        '
        'FTImage2
        '
        Me.FTImage2.AllowDrop = True
        Me.FTImage2.Cursor =  System.Windows.Forms.Cursors.Default
        Me.FTImage2.Location = New System.Drawing.Point(396, 5)
        Me.FTImage2.Margin = New  System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTImage2.Name = "FTImage2"
        Me.FTImage2.Properties.ShowMenu = False
        Me.FTImage2.Properties.ShowZoomSubMenu = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTImage2.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch
        Me.FTImage2.Properties.ZoomAccelerationFactor = 1.0R
        Me.FTImage2.ShowToolTips = False
        Me.FTImage2.Size = New System.Drawing.Size(389, 468)
        Me.FTImage2.TabIndex = 473
        Me.FTImage2.Tag = ""
        '
        'ogrpQualityRate
        '
        Me.ogrpQualityRate.Anchor = CType((( System.Windows.Forms.AnchorStyles.Top Or  System.Windows.Forms.AnchorStyles.Left) _
            Or  System.Windows.Forms.AnchorStyles.Right),  System.Windows.Forms.AnchorStyles)
        Me.ogrpQualityRate.AppearanceCaption.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.ogrpQualityRate.AppearanceCaption.Options.UseFont = True
        Me.ogrpQualityRate.Controls.Add(Me.FNQualityRate)
        Me.ogrpQualityRate.Location = New System.Drawing.Point(1409, 331)
        Me.ogrpQualityRate.Margin = New  System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogrpQualityRate.Name = "ogrpQualityRate"
        Me.ogrpQualityRate.Size = New System.Drawing.Size(451, 144)
        Me.ogrpQualityRate.TabIndex = 478
        Me.ogrpQualityRate.Text = "Quanlity Rate"
        '
        'FNQualityRate
        '
        Me.FNQualityRate.Anchor = CType((( System.Windows.Forms.AnchorStyles.Top Or  System.Windows.Forms.AnchorStyles.Left) _
            Or  System.Windows.Forms.AnchorStyles.Right),  System.Windows.Forms.AnchorStyles)
        Me.FNQualityRate.Appearance.Font = New System.Drawing.Font("Tahoma", 30.0!, System.Drawing.FontStyle.Bold)
        Me.FNQualityRate.Appearance.Options.UseFont = True
        Me.FNQualityRate.Appearance.Options.UseTextOptions = True
        Me.FNQualityRate.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FNQualityRate.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNQualityRate.Location = New System.Drawing.Point(6, 33)
        Me.FNQualityRate.Margin = New  System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNQualityRate.Name = "FNQualityRate"
        Me.FNQualityRate.Size = New System.Drawing.Size(440, 105)
        Me.FNQualityRate.TabIndex = 0
        Me.FNQualityRate.Text = "0.00%"
        '
        'ogrpSummaryDefect
        '
        Me.ogrpSummaryDefect.Anchor = CType((( System.Windows.Forms.AnchorStyles.Top Or  System.Windows.Forms.AnchorStyles.Left) _
            Or  System.Windows.Forms.AnchorStyles.Right),  System.Windows.Forms.AnchorStyles)
        Me.ogrpSummaryDefect.Controls.Add(Me.FNPercentDefect_lbl)
        Me.ogrpSummaryDefect.Controls.Add(Me.FNPercentDefect)
        Me.ogrpSummaryDefect.Controls.Add(Me.FNDefectQty)
        Me.ogrpSummaryDefect.Controls.Add(Me.FNDefectQty_lbl)
        Me.ogrpSummaryDefect.Location = New System.Drawing.Point(1409, 204)
        Me.ogrpSummaryDefect.Margin = New  System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogrpSummaryDefect.Name = "ogrpSummaryDefect"
        Me.ogrpSummaryDefect.Size = New System.Drawing.Size(449, 119)
        Me.ogrpSummaryDefect.TabIndex = 477
        Me.ogrpSummaryDefect.Text = "Defect Summary"
        '
        'FNPercentDefect_lbl
        '
        Me.FNPercentDefect_lbl.Appearance.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.FNPercentDefect_lbl.Appearance.Options.UseFont = True
        Me.FNPercentDefect_lbl.Appearance.Options.UseTextOptions = True
        Me.FNPercentDefect_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNPercentDefect_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNPercentDefect_lbl.Location = New System.Drawing.Point(6, 64)
        Me.FNPercentDefect_lbl.Margin = New  System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNPercentDefect_lbl.Name = "FNPercentDefect_lbl"
        Me.FNPercentDefect_lbl.Size = New System.Drawing.Size(159, 27)
        Me.FNPercentDefect_lbl.TabIndex = 0
        Me.FNPercentDefect_lbl.Text = "Percent Defect Qty :"
        '
        'FNPercentDefect
        '
        Me.FNPercentDefect.Appearance.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.FNPercentDefect.Appearance.Options.UseFont = True
        Me.FNPercentDefect.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNPercentDefect.Location = New System.Drawing.Point(171, 64)
        Me.FNPercentDefect.Margin = New  System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNPercentDefect.Name = "FNPercentDefect"
        Me.FNPercentDefect.Size = New System.Drawing.Size(176, 27)
        Me.FNPercentDefect.TabIndex = 0
        Me.FNPercentDefect.Text = "0.00%"
        '
        'FNDefectQty
        '
        Me.FNDefectQty.Appearance.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.FNDefectQty.Appearance.Options.UseFont = True
        Me.FNDefectQty.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNDefectQty.Location = New System.Drawing.Point(171, 30)
        Me.FNDefectQty.Margin = New  System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNDefectQty.Name = "FNDefectQty"
        Me.FNDefectQty.Size = New System.Drawing.Size(176, 27)
        Me.FNDefectQty.TabIndex = 0
        Me.FNDefectQty.Text = "0"
        '
        'FNDefectQty_lbl
        '
        Me.FNDefectQty_lbl.Appearance.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.FNDefectQty_lbl.Appearance.Options.UseFont = True
        Me.FNDefectQty_lbl.Appearance.Options.UseTextOptions = True
        Me.FNDefectQty_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNDefectQty_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNDefectQty_lbl.Location = New System.Drawing.Point(6, 30)
        Me.FNDefectQty_lbl.Margin = New  System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNDefectQty_lbl.Name = "FNDefectQty_lbl"
        Me.FNDefectQty_lbl.Size = New System.Drawing.Size(159, 27)
        Me.FNDefectQty_lbl.TabIndex = 0
        Me.FNDefectQty_lbl.Text = "Defect Qty :"
        '
        'ogrpDocument
        '
        Me.ogrpDocument.Anchor = CType((( System.Windows.Forms.AnchorStyles.Top Or  System.Windows.Forms.AnchorStyles.Left) _
            Or  System.Windows.Forms.AnchorStyles.Right),  System.Windows.Forms.AnchorStyles)
        Me.ogrpDocument.Controls.Add(Me.oFDQADateTo_lbl)
        Me.ogrpDocument.Controls.Add(Me.oFDQADateE)
        Me.ogrpDocument.Controls.Add(Me.oFDQADate)
        Me.ogrpDocument.Controls.Add(Me.oFTColorway)
        Me.ogrpDocument.Controls.Add(Me.oFTOrderNo)
        Me.ogrpDocument.Controls.Add(Me.oFNHSysUnitSectId_None)
        Me.ogrpDocument.Controls.Add(Me.oFNHSysStyleId_None)
        Me.ogrpDocument.Controls.Add(Me.oFNHSysStyleId)
        Me.ogrpDocument.Controls.Add(Me.oFNHSysUnitSectId)
        Me.ogrpDocument.Controls.Add(Me.oFDQADate_lbl)
        Me.ogrpDocument.Controls.Add(Me.oFNHSysStyleId_lbl)
        Me.ogrpDocument.Controls.Add(Me.oFTColorway_lbl)
        Me.ogrpDocument.Controls.Add(Me.oFNHSysUnitSectId_lbl)
        Me.ogrpDocument.Controls.Add(Me.oFTOrderNo_lbl)
        Me.ogrpDocument.Location = New System.Drawing.Point(1409, 6)
        Me.ogrpDocument.Margin = New  System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogrpDocument.Name = "ogrpDocument"
        Me.ogrpDocument.Size = New System.Drawing.Size(451, 191)
        Me.ogrpDocument.TabIndex = 476
        Me.ogrpDocument.Text = "Document"
        '
        'oFDQADateTo_lbl
        '
        Me.oFDQADateTo_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.oFDQADateTo_lbl.Appearance.Options.UseForeColor = True
        Me.oFDQADateTo_lbl.Appearance.Options.UseTextOptions = True
        Me.oFDQADateTo_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.oFDQADateTo_lbl.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top
        Me.oFDQADateTo_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.oFDQADateTo_lbl.Location = New System.Drawing.Point(82, 185)
        Me.oFDQADateTo_lbl.Margin = New  System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.oFDQADateTo_lbl.Name = "oFDQADateTo_lbl"
        Me.oFDQADateTo_lbl.Size = New System.Drawing.Size(58, 25)
        Me.oFDQADateTo_lbl.TabIndex = 479
        Me.oFDQADateTo_lbl.Tag = "2|"
        Me.oFDQADateTo_lbl.Text = "To :"
        Me.oFDQADateTo_lbl.Visible = False
        '
        'oFDQADateE
        '
        Me.oFDQADateE.Anchor = CType((( System.Windows.Forms.AnchorStyles.Top Or  System.Windows.Forms.AnchorStyles.Left) _
            Or  System.Windows.Forms.AnchorStyles.Right),  System.Windows.Forms.AnchorStyles)
        Me.oFDQADateE.Location = New System.Drawing.Point(213, 159)
        Me.oFDQADateE.Margin = New  System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.oFDQADateE.Name = "oFDQADateE"
        Me.oFDQADateE.Properties.ReadOnly = True
        Me.oFDQADateE.Size = New System.Drawing.Size(233, 22)
        Me.oFDQADateE.TabIndex = 472
        '
        'oFDQADate
        '
        Me.oFDQADate.Location = New System.Drawing.Point(120, 159)
        Me.oFDQADate.Margin = New  System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.oFDQADate.Name = "oFDQADate"
        Me.oFDQADate.Properties.ReadOnly = True
        Me.oFDQADate.Size = New System.Drawing.Size(87, 22)
        Me.oFDQADate.TabIndex = 472
        '
        'oFTColorway
        '
        Me.oFTColorway.Anchor = CType((( System.Windows.Forms.AnchorStyles.Top Or  System.Windows.Forms.AnchorStyles.Left) _
            Or  System.Windows.Forms.AnchorStyles.Right),  System.Windows.Forms.AnchorStyles)
        Me.oFTColorway.Location = New System.Drawing.Point(120, 132)
        Me.oFTColorway.Margin = New  System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.oFTColorway.Name = "oFTColorway"
        Me.oFTColorway.Properties.ReadOnly = True
        Me.oFTColorway.Size = New System.Drawing.Size(326, 22)
        Me.oFTColorway.TabIndex = 472
        '
        'oFTOrderNo
        '
        Me.oFTOrderNo.Anchor = CType((( System.Windows.Forms.AnchorStyles.Top Or  System.Windows.Forms.AnchorStyles.Left) _
            Or  System.Windows.Forms.AnchorStyles.Right),  System.Windows.Forms.AnchorStyles)
        Me.oFTOrderNo.Location = New System.Drawing.Point(120, 105)
        Me.oFTOrderNo.Margin = New  System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.oFTOrderNo.Name = "oFTOrderNo"
        Me.oFTOrderNo.Properties.ReadOnly = True
        Me.oFTOrderNo.Size = New System.Drawing.Size(326, 22)
        Me.oFTOrderNo.TabIndex = 472
        '
        'oFNHSysUnitSectId_None
        '
        Me.oFNHSysUnitSectId_None.Anchor = CType((( System.Windows.Forms.AnchorStyles.Top Or  System.Windows.Forms.AnchorStyles.Left) _
            Or  System.Windows.Forms.AnchorStyles.Right),  System.Windows.Forms.AnchorStyles)
        Me.oFNHSysUnitSectId_None.Location = New System.Drawing.Point(120, 51)
        Me.oFNHSysUnitSectId_None.Margin = New  System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.oFNHSysUnitSectId_None.Name = "oFNHSysUnitSectId_None"
        Me.oFNHSysUnitSectId_None.Properties.ReadOnly = True
        Me.oFNHSysUnitSectId_None.Size = New System.Drawing.Size(326, 22)
        Me.oFNHSysUnitSectId_None.TabIndex = 472
        '
        'oFNHSysStyleId_None
        '
        Me.oFNHSysStyleId_None.Anchor = CType((( System.Windows.Forms.AnchorStyles.Top Or  System.Windows.Forms.AnchorStyles.Left) _
            Or  System.Windows.Forms.AnchorStyles.Right),  System.Windows.Forms.AnchorStyles)
        Me.oFNHSysStyleId_None.Location = New System.Drawing.Point(398, 80)
        Me.oFNHSysStyleId_None.Margin = New  System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.oFNHSysStyleId_None.Name = "oFNHSysStyleId_None"
        Me.oFNHSysStyleId_None.Properties.ReadOnly = True
        Me.oFNHSysStyleId_None.Size = New System.Drawing.Size(48, 22)
        Me.oFNHSysStyleId_None.TabIndex = 472
        Me.oFNHSysStyleId_None.Visible = False
        '
        'oFNHSysStyleId
        '
        Me.oFNHSysStyleId.Anchor = CType((( System.Windows.Forms.AnchorStyles.Top Or  System.Windows.Forms.AnchorStyles.Left) _
            Or  System.Windows.Forms.AnchorStyles.Right),  System.Windows.Forms.AnchorStyles)
        Me.oFNHSysStyleId.Location = New System.Drawing.Point(120, 78)
        Me.oFNHSysStyleId.Margin = New  System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.oFNHSysStyleId.Name = "oFNHSysStyleId"
        Me.oFNHSysStyleId.Properties.ReadOnly = True
        Me.oFNHSysStyleId.Size = New System.Drawing.Size(326, 22)
        Me.oFNHSysStyleId.TabIndex = 472
        '
        'oFNHSysUnitSectId
        '
        Me.oFNHSysUnitSectId.Anchor = CType((( System.Windows.Forms.AnchorStyles.Top Or  System.Windows.Forms.AnchorStyles.Left) _
            Or  System.Windows.Forms.AnchorStyles.Right),  System.Windows.Forms.AnchorStyles)
        Me.oFNHSysUnitSectId.Location = New System.Drawing.Point(120, 26)
        Me.oFNHSysUnitSectId.Margin = New  System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.oFNHSysUnitSectId.Name = "oFNHSysUnitSectId"
        Me.oFNHSysUnitSectId.Properties.ReadOnly = True
        Me.oFNHSysUnitSectId.Size = New System.Drawing.Size(326, 22)
        Me.oFNHSysUnitSectId.TabIndex = 472
        '
        'oFDQADate_lbl
        '
        Me.oFDQADate_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.oFDQADate_lbl.Appearance.Options.UseForeColor = True
        Me.oFDQADate_lbl.Appearance.Options.UseTextOptions = True
        Me.oFDQADate_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.oFDQADate_lbl.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top
        Me.oFDQADate_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.oFDQADate_lbl.Location = New System.Drawing.Point(6, 160)
        Me.oFDQADate_lbl.Margin = New  System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.oFDQADate_lbl.Name = "oFDQADate_lbl"
        Me.oFDQADate_lbl.Size = New System.Drawing.Size(108, 26)
        Me.oFDQADate_lbl.TabIndex = 471
        Me.oFDQADate_lbl.Tag = "2|"
        Me.oFDQADate_lbl.Text = "FDQADate :"
        '
        'oFNHSysStyleId_lbl
        '
        Me.oFNHSysStyleId_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.oFNHSysStyleId_lbl.Appearance.Options.UseForeColor = True
        Me.oFNHSysStyleId_lbl.Appearance.Options.UseTextOptions = True
        Me.oFNHSysStyleId_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.oFNHSysStyleId_lbl.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top
        Me.oFNHSysStyleId_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.oFNHSysStyleId_lbl.Location = New System.Drawing.Point(6, 80)
        Me.oFNHSysStyleId_lbl.Margin = New  System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.oFNHSysStyleId_lbl.Name = "oFNHSysStyleId_lbl"
        Me.oFNHSysStyleId_lbl.Size = New System.Drawing.Size(108, 25)
        Me.oFNHSysStyleId_lbl.TabIndex = 469
        Me.oFNHSysStyleId_lbl.Tag = "2|"
        Me.oFNHSysStyleId_lbl.Text = "FNHSysStyleId :"
        '
        'oFTColorway_lbl
        '
        Me.oFTColorway_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.oFTColorway_lbl.Appearance.Options.UseForeColor = True
        Me.oFTColorway_lbl.Appearance.Options.UseTextOptions = True
        Me.oFTColorway_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.oFTColorway_lbl.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top
        Me.oFTColorway_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.oFTColorway_lbl.Location = New System.Drawing.Point(6, 136)
        Me.oFTColorway_lbl.Margin = New  System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.oFTColorway_lbl.Name = "oFTColorway_lbl"
        Me.oFTColorway_lbl.Size = New System.Drawing.Size(108, 26)
        Me.oFTColorway_lbl.TabIndex = 471
        Me.oFTColorway_lbl.Tag = "2|"
        Me.oFTColorway_lbl.Text = "FTColorWay :"
        '
        'oFNHSysUnitSectId_lbl
        '
        Me.oFNHSysUnitSectId_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.oFNHSysUnitSectId_lbl.Appearance.Options.UseForeColor = True
        Me.oFNHSysUnitSectId_lbl.Appearance.Options.UseTextOptions = True
        Me.oFNHSysUnitSectId_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.oFNHSysUnitSectId_lbl.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top
        Me.oFNHSysUnitSectId_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.oFNHSysUnitSectId_lbl.Location = New System.Drawing.Point(6, 26)
        Me.oFNHSysUnitSectId_lbl.Margin = New  System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.oFNHSysUnitSectId_lbl.Name = "oFNHSysUnitSectId_lbl"
        Me.oFNHSysUnitSectId_lbl.Size = New System.Drawing.Size(108, 23)
        Me.oFNHSysUnitSectId_lbl.TabIndex = 470
        Me.oFNHSysUnitSectId_lbl.Tag = "2|"
        Me.oFNHSysUnitSectId_lbl.Text = "FNHSysUnitSectId"
        '
        'oFTOrderNo_lbl
        '
        Me.oFTOrderNo_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.oFTOrderNo_lbl.Appearance.Options.UseForeColor = True
        Me.oFTOrderNo_lbl.Appearance.Options.UseTextOptions = True
        Me.oFTOrderNo_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.oFTOrderNo_lbl.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top
        Me.oFTOrderNo_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.oFTOrderNo_lbl.Location = New System.Drawing.Point(6, 109)
        Me.oFTOrderNo_lbl.Margin = New  System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.oFTOrderNo_lbl.Name = "oFTOrderNo_lbl"
        Me.oFTOrderNo_lbl.Size = New System.Drawing.Size(108, 26)
        Me.oFTOrderNo_lbl.TabIndex = 471
        Me.oFTOrderNo_lbl.Tag = "2|"
        Me.oFTOrderNo_lbl.Text = "FTOrderNo :"
        '
        'wDailyQAReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode =  System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.AutoValidate =  System.Windows.Forms.AutoValidate.EnablePreventFocusChange
        Me.ClientSize = New System.Drawing.Size(1864, 970)
        Me.Controls.Add(Me.oPanalControlDetail)
        Me.Controls.Add(Me.hideContainerTop)
        Me.Margin = New  System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(1085, 726)
        Me.Name = "wDailyQAReport"
        Me.Text = "QA Daily Chart"
        CType(Me.ocmdoc, System.ComponentModel.ISupportInitialize).EndInit()
        Me.hideContainerTop.ResumeLayout(False)
        Me.ogbheader.ResumeLayout(False)
        Me.DockPanel1_Container.ResumeLayout(False)
        CType(Me.FNHSysUnitSectId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FDEDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FDEDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysStyleId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTOrderNo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FDSDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FDSDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysStyleId_None.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysUnitSectId_None.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.dockDetail.ResumeLayout(False)
        Me.ControlContainer1.ResumeLayout(False)
        CType(Me.ogcDetailMain, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvDetailMain, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.comboChartType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.checkShowPointLabels.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ceChartDataVertical.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ceSelectionOnly.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ceShowColumnGrandTotals.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ceShowRowGrandTotals.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.seUpdateDelay.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ComboBoxEdit1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.oPanalControlDetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.oPanalControlDetail.ResumeLayout(False)
        Me.XtraScrollableControl1.ResumeLayout(False)
        CType(Me.ogrpDetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogrpDetail.ResumeLayout(False)
        Me.ogrpDetail.PerformLayout()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbmainprocbutton.ResumeLayout(False)
        CType(Me.ogcDetailDaily, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvDetailDaily, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        CType(Me.oChartDefaul, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.imageRpt.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTImage.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.imageRpt2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTImage2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogrpQualityRate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogrpQualityRate.ResumeLayout(False)
        CType(Me.ogrpSummaryDefect, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogrpSummaryDefect.ResumeLayout(False)
        CType(Me.ogrpDocument, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogrpDocument.ResumeLayout(False)
        CType(Me.oFDQADateE.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.oFDQADate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.oFTColorway.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.oFTOrderNo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.oFNHSysUnitSectId_None.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.oFNHSysStyleId_None.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.oFNHSysStyleId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.oFNHSysUnitSectId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ocmdoc As DevExpress.XtraBars.Docking.DockManager
    Friend WithEvents ogbheader As DevExpress.XtraBars.Docking.DockPanel
    Friend WithEvents DockPanel1_Container As DevExpress.XtraBars.Docking.ControlContainer
    Private WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Private WithEvents comboChartType As DevExpress.XtraEditors.ComboBoxEdit
    Private WithEvents checkShowPointLabels As DevExpress.XtraEditors.CheckEdit
    Private WithEvents ceChartDataVertical As DevExpress.XtraEditors.CheckEdit
    Private WithEvents ceSelectionOnly As DevExpress.XtraEditors.CheckEdit
    Private WithEvents ceShowColumnGrandTotals As DevExpress.XtraEditors.CheckEdit
    Private WithEvents ceShowRowGrandTotals As DevExpress.XtraEditors.CheckEdit
    Private WithEvents lblUpdateDelay As DevExpress.XtraEditors.LabelControl
    Private WithEvents seUpdateDelay As DevExpress.XtraEditors.SpinEdit
    Private WithEvents ComboBoxEdit1 As DevExpress.XtraEditors.ComboBoxEdit
    Private WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents dockDetail As DevExpress.XtraBars.Docking.DockPanel
    Friend WithEvents ControlContainer1 As DevExpress.XtraBars.Docking.ControlContainer
    Friend WithEvents ogcDetailMain As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvDetailMain As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents oPanalControlDetail As DevExpress.XtraEditors.PanelControl
    Friend WithEvents XtraScrollableControl1 As DevExpress.XtraEditors.XtraScrollableControl
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents ogbmainprocbutton As DevExpress.XtraEditors.PanelControl
    Friend WithEvents ocmload As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ogrpDocument As DevExpress.XtraEditors.GroupControl
    Friend WithEvents oFDQADate As DevExpress.XtraEditors.TextEdit
    Friend WithEvents oFTColorway As DevExpress.XtraEditors.TextEdit
    Friend WithEvents oFTOrderNo As DevExpress.XtraEditors.TextEdit
    Friend WithEvents oFNHSysUnitSectId_None As DevExpress.XtraEditors.TextEdit
    Friend WithEvents oFNHSysStyleId_None As DevExpress.XtraEditors.TextEdit
    Friend WithEvents oFNHSysStyleId As DevExpress.XtraEditors.TextEdit
    Friend WithEvents oFNHSysUnitSectId As DevExpress.XtraEditors.TextEdit
    Friend WithEvents oFDQADate_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents oFNHSysStyleId_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents oFTColorway_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents oFNHSysUnitSectId_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents oFTOrderNo_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTImage2 As DevExpress.XtraEditors.PictureEdit
    Friend WithEvents FTImage As DevExpress.XtraEditors.PictureEdit
    Friend WithEvents ogrpDetail As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ogcDetailDaily As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvDetailDaily As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents cNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cDefect As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cTime1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cTime2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cTime3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cTime4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cTime5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cTime6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cTime7 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cTime8 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cTime9 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cTime10 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cTime11 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cTime12 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cTotal As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNHSysUnitSectId As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FNHSysUnitSectId_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FDEDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents FNHSysStyleId As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FTOrderNo As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FNHSysStyleId_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FDSDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents FNHSysStyleId_None As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FNHSysUnitSectId_None As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FTOrderNo_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ogrpSummaryDefect As DevExpress.XtraEditors.GroupControl
    Friend WithEvents FNDefectQty_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNPercentDefect_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNPercentDefect As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNDefectQty As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ogrpQualityRate As DevExpress.XtraEditors.GroupControl
    Friend WithEvents FNQualityRate As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ocmpreview As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FDEDate_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FDSDate_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents oFDQADateE As DevExpress.XtraEditors.TextEdit
    Friend WithEvents oFDQADateTo_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents oChartDefaul As DevExpress.XtraCharts.ChartControl
    Friend WithEvents FTSeriesTopName As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTTitleTopChart As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTSeriesDetailName As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTTitleDetailChart As DevExpress.XtraEditors.LabelControl
    Friend WithEvents hideContainerTop As DevExpress.XtraBars.Docking.AutoHideContainer
    Friend WithEvents imageRpt As DevExpress.XtraEditors.PictureEdit
    Friend WithEvents imageRpt2 As DevExpress.XtraEditors.PictureEdit
End Class

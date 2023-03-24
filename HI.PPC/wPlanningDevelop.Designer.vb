<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wPlanningDevelop
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
        Dim EditorButtonImageOptions1 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject4 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(wPlanningDevelop))
        Me.DockManager1 = New DevExpress.XtraBars.Docking.DockManager(Me.components)
        Me.ogbheader = New DevExpress.XtraBars.Docking.DockPanel()
        Me.DockPanel1_Container = New DevExpress.XtraBars.Docking.ControlContainer()
        Me.FNHSysCmpId_None = New DevExpress.XtraEditors.TextEdit()
        Me.FNHSysCmpId_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysCmpId = New DevExpress.XtraEditors.ButtonEdit()
        Me.ogbplanning = New DevExpress.XtraEditors.GroupControl()
        Me.lblpositionButtom = New DevExpress.XtraEditors.LabelControl()
        Me.lblpositiontop = New DevExpress.XtraEditors.LabelControl()
        Me.olbgraphmove = New DevExpress.XtraEditors.PanelControl()
        Me.olbgraphmoveimage = New DevExpress.XtraEditors.PictureEdit()
        Me.olbgraphmovedesc = New DevExpress.XtraEditors.LabelControl()
        Me.ogvplaning = New C1.Win.C1FlexGrid.Classic.C1FlexGridClassic()
        Me.ogbmainprocbutton = New DevExpress.XtraEditors.PanelControl()
        Me.ocmedit = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmsave = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmdelete = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmadd = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmload = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.ImgBarbg = New System.Windows.Forms.ImageList(Me.components)
        Me.ImgBarbgAlert = New System.Windows.Forms.ImageList(Me.components)
        Me.C1STooltip = New C1.Win.C1SuperTooltip.C1SuperTooltip(Me.components)
        Me.mnuplanpopup = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.cnuRefreshLine = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator12 = New System.Windows.Forms.ToolStripSeparator()
        Me.cnuDelJob = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator13 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuSkillByStyle = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuLearnningGroup = New System.Windows.Forms.ToolStripMenuItem()
        CType(Me.DockManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbheader.SuspendLayout()
        Me.DockPanel1_Container.SuspendLayout()
        CType(Me.FNHSysCmpId_None.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysCmpId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogbplanning, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbplanning.SuspendLayout()
        CType(Me.olbgraphmove, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.olbgraphmove.SuspendLayout()
        CType(Me.olbgraphmoveimage.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvplaning, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbmainprocbutton.SuspendLayout()
        Me.mnuplanpopup.SuspendLayout()
        Me.SuspendLayout()
        '
        'DockManager1
        '
        Me.DockManager1.Form = Me
        Me.DockManager1.RootPanels.AddRange(New DevExpress.XtraBars.Docking.DockPanel() {Me.ogbheader})
        Me.DockManager1.TopZIndexControls.AddRange(New String() {"DevExpress.XtraBars.BarDockControl", "DevExpress.XtraBars.StandaloneBarDockControl", "System.Windows.Forms.StatusBar", "System.Windows.Forms.MenuStrip", "System.Windows.Forms.StatusStrip", "DevExpress.XtraBars.Ribbon.RibbonStatusBar", "DevExpress.XtraBars.Ribbon.RibbonControl"})
        '
        'ogbheader
        '
        Me.ogbheader.Controls.Add(Me.DockPanel1_Container)
        Me.ogbheader.Dock = DevExpress.XtraBars.Docking.DockingStyle.Top
        Me.ogbheader.ID = New System.Guid("9d240089-1fd1-47c1-9b32-db829b2594d1")
        Me.ogbheader.Location = New System.Drawing.Point(0, 0)
        Me.ogbheader.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.ogbheader.Name = "ogbheader"
        Me.ogbheader.Options.AllowDockAsTabbedDocument = False
        Me.ogbheader.Options.AllowDockBottom = False
        Me.ogbheader.Options.AllowDockFill = False
        Me.ogbheader.Options.AllowDockLeft = False
        Me.ogbheader.Options.AllowDockRight = False
        Me.ogbheader.Options.AllowFloating = False
        Me.ogbheader.Options.ShowCloseButton = False
        Me.ogbheader.Options.ShowMaximizeButton = False
        Me.ogbheader.OriginalSize = New System.Drawing.Size(200, 73)
        Me.ogbheader.Size = New System.Drawing.Size(1074, 59)
        Me.ogbheader.Text = "DockPanel1"
        '
        'DockPanel1_Container
        '
        Me.DockPanel1_Container.Controls.Add(Me.FNHSysCmpId_None)
        Me.DockPanel1_Container.Controls.Add(Me.FNHSysCmpId_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FNHSysCmpId)
        Me.DockPanel1_Container.Location = New System.Drawing.Point(3, 26)
        Me.DockPanel1_Container.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.DockPanel1_Container.Name = "DockPanel1_Container"
        Me.DockPanel1_Container.Size = New System.Drawing.Size(1068, 29)
        Me.DockPanel1_Container.TabIndex = 0
        '
        'FNHSysCmpId_None
        '
        Me.FNHSysCmpId_None.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FNHSysCmpId_None.Location = New System.Drawing.Point(297, 7)
        Me.FNHSysCmpId_None.Name = "FNHSysCmpId_None"
        Me.FNHSysCmpId_None.Properties.Appearance.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysCmpId_None.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysCmpId_None.Properties.ReadOnly = True
        Me.FNHSysCmpId_None.Size = New System.Drawing.Size(518, 20)
        Me.FNHSysCmpId_None.TabIndex = 508
        Me.FNHSysCmpId_None.Tag = ""
        '
        'FNHSysCmpId_lbl
        '
        Me.FNHSysCmpId_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysCmpId_lbl.Appearance.Options.UseForeColor = True
        Me.FNHSysCmpId_lbl.Appearance.Options.UseTextOptions = True
        Me.FNHSysCmpId_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysCmpId_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysCmpId_lbl.Location = New System.Drawing.Point(45, 7)
        Me.FNHSysCmpId_lbl.Name = "FNHSysCmpId_lbl"
        Me.FNHSysCmpId_lbl.Size = New System.Drawing.Size(123, 17)
        Me.FNHSysCmpId_lbl.TabIndex = 507
        Me.FNHSysCmpId_lbl.Tag = "2|"
        Me.FNHSysCmpId_lbl.Text = "Company :"
        '
        'FNHSysCmpId
        '
        Me.FNHSysCmpId.Location = New System.Drawing.Point(171, 7)
        Me.FNHSysCmpId.Name = "FNHSysCmpId"
        Me.FNHSysCmpId.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysCmpId.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysCmpId.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNHSysCmpId.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNHSysCmpId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, EditorButtonImageOptions1, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, SerializableAppearanceObject2, SerializableAppearanceObject3, SerializableAppearanceObject4, "", "328", Nothing, DevExpress.Utils.ToolTipAnchor.[Default])})
        Me.FNHSysCmpId.Properties.ReadOnly = True
        Me.FNHSysCmpId.Properties.Tag = ""
        Me.FNHSysCmpId.Size = New System.Drawing.Size(121, 20)
        Me.FNHSysCmpId.TabIndex = 506
        Me.FNHSysCmpId.Tag = ""
        '
        'ogbplanning
        '
        Me.ogbplanning.Controls.Add(Me.lblpositionButtom)
        Me.ogbplanning.Controls.Add(Me.lblpositiontop)
        Me.ogbplanning.Controls.Add(Me.olbgraphmove)
        Me.ogbplanning.Controls.Add(Me.ogvplaning)
        Me.ogbplanning.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogbplanning.Location = New System.Drawing.Point(0, 59)
        Me.ogbplanning.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.ogbplanning.Name = "ogbplanning"
        Me.ogbplanning.Size = New System.Drawing.Size(1074, 535)
        Me.ogbplanning.TabIndex = 143
        Me.ogbplanning.Text = "Planning Detail"
        '
        'lblpositionButtom
        '
        Me.lblpositionButtom.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblpositionButtom.Location = New System.Drawing.Point(937, 470)
        Me.lblpositionButtom.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.lblpositionButtom.Name = "lblpositionButtom"
        Me.lblpositionButtom.Size = New System.Drawing.Size(37, 13)
        Me.lblpositionButtom.TabIndex = 7
        Me.lblpositionButtom.Text = "Position"
        Me.lblpositionButtom.Visible = False
        '
        'lblpositiontop
        '
        Me.lblpositiontop.Location = New System.Drawing.Point(265, 156)
        Me.lblpositiontop.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.lblpositiontop.Name = "lblpositiontop"
        Me.lblpositiontop.Size = New System.Drawing.Size(58, 13)
        Me.lblpositiontop.TabIndex = 5
        Me.lblpositiontop.Text = "Position Top"
        Me.lblpositiontop.Visible = False
        '
        'olbgraphmove
        '
        Me.olbgraphmove.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.olbgraphmove.Appearance.Options.UseBackColor = True
        Me.olbgraphmove.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.olbgraphmove.Controls.Add(Me.olbgraphmoveimage)
        Me.olbgraphmove.Controls.Add(Me.olbgraphmovedesc)
        Me.olbgraphmove.Location = New System.Drawing.Point(479, 75)
        Me.olbgraphmove.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat
        Me.olbgraphmove.LookAndFeel.UseDefaultLookAndFeel = False
        Me.olbgraphmove.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.olbgraphmove.Name = "olbgraphmove"
        Me.olbgraphmove.Size = New System.Drawing.Size(238, 17)
        Me.olbgraphmove.TabIndex = 4
        Me.olbgraphmove.Visible = False
        '
        'olbgraphmoveimage
        '
        Me.olbgraphmoveimage.Cursor = System.Windows.Forms.Cursors.Default
        Me.olbgraphmoveimage.Dock = System.Windows.Forms.DockStyle.Fill
        Me.olbgraphmoveimage.Location = New System.Drawing.Point(71, 0)
        Me.olbgraphmoveimage.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.olbgraphmoveimage.Name = "olbgraphmoveimage"
        Me.olbgraphmoveimage.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.olbgraphmoveimage.Properties.ReadOnly = True
        Me.olbgraphmoveimage.Properties.ShowMenu = False
        Me.olbgraphmoveimage.Properties.ShowZoomSubMenu = DevExpress.Utils.DefaultBoolean.[False]
        Me.olbgraphmoveimage.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch
        Me.olbgraphmoveimage.Size = New System.Drawing.Size(167, 17)
        Me.olbgraphmoveimage.TabIndex = 1
        '
        'olbgraphmovedesc
        '
        Me.olbgraphmovedesc.Appearance.Font = New System.Drawing.Font("Tahoma", 7.8!, System.Drawing.FontStyle.Bold)
        Me.olbgraphmovedesc.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.olbgraphmovedesc.Appearance.Options.UseFont = True
        Me.olbgraphmovedesc.Appearance.Options.UseForeColor = True
        Me.olbgraphmovedesc.Appearance.Options.UseTextOptions = True
        Me.olbgraphmovedesc.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.olbgraphmovedesc.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.olbgraphmovedesc.Dock = System.Windows.Forms.DockStyle.Left
        Me.olbgraphmovedesc.Location = New System.Drawing.Point(0, 0)
        Me.olbgraphmovedesc.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.olbgraphmovedesc.Name = "olbgraphmovedesc"
        Me.olbgraphmovedesc.Size = New System.Drawing.Size(71, 12)
        Me.olbgraphmovedesc.TabIndex = 0
        Me.olbgraphmovedesc.Text = "LabelControl1"
        '
        'ogvplaning
        '
        Me.ogvplaning.AllowBigSelection = False
        Me.ogvplaning.AllowSelection = False
        Me.ogvplaning.AllowUserResizing = C1.Win.C1FlexGrid.Classic.AllowUserResizeSettings.flexResizeNone
        Me.ogvplaning.AutoGenerateColumns = False
        Me.ogvplaning.BackColorSel = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.ogvplaning.Cols = 2
        Me.ogvplaning.ColumnInfo = "2,2,0,0,0,110,Columns:"
        Me.ogvplaning.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ogvplaning.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogvplaning.DropMode = C1.Win.C1FlexGrid.DropModeEnum.Manual
        Me.ogvplaning.FixedCols = 2
        Me.ogvplaning.FixedRows = 5
        Me.ogvplaning.FocusRect = C1.Win.C1FlexGrid.FocusRectEnum.None
        Me.ogvplaning.Font = New System.Drawing.Font("Tahoma", 7.8!)
        Me.ogvplaning.GridColorFixed = System.Drawing.SystemColors.ControlDark
        Me.ogvplaning.HighLight = C1.Win.C1FlexGrid.HighLightEnum.Never
        Me.ogvplaning.Location = New System.Drawing.Point(2, 23)
        Me.ogvplaning.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.ogvplaning.Name = "ogvplaning"
        Me.ogvplaning.NodeClosedPicture = Nothing
        Me.ogvplaning.NodeOpenPicture = Nothing
        Me.ogvplaning.OutlineCol = -1
        Me.ogvplaning.Rows = 5
        Me.ogvplaning.Size = New System.Drawing.Size(1070, 510)
        Me.ogvplaning.StyleInfo = resources.GetString("ogvplaning.StyleInfo")
        Me.ogvplaning.TabIndex = 1
        Me.ogvplaning.TreeColor = System.Drawing.Color.DarkGray
        Me.ogvplaning.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Office2010Blue
        '
        'ogbmainprocbutton
        '
        Me.ogbmainprocbutton.Controls.Add(Me.ocmedit)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmsave)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmdelete)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmadd)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmload)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmexit)
        Me.ogbmainprocbutton.Location = New System.Drawing.Point(110, 252)
        Me.ogbmainprocbutton.Name = "ogbmainprocbutton"
        Me.ogbmainprocbutton.Size = New System.Drawing.Size(855, 90)
        Me.ogbmainprocbutton.TabIndex = 398
        '
        'ocmedit
        '
        Me.ocmedit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmedit.Location = New System.Drawing.Point(382, 34)
        Me.ocmedit.Name = "ocmedit"
        Me.ocmedit.Size = New System.Drawing.Size(90, 22)
        Me.ocmedit.TabIndex = 112
        Me.ocmedit.TabStop = False
        Me.ocmedit.Tag = "2|"
        Me.ocmedit.Text = "Edit"
        Me.ocmedit.Visible = False
        '
        'ocmsave
        '
        Me.ocmsave.Location = New System.Drawing.Point(311, 15)
        Me.ocmsave.Name = "ocmsave"
        Me.ocmsave.Size = New System.Drawing.Size(95, 25)
        Me.ocmsave.TabIndex = 111
        Me.ocmsave.TabStop = False
        Me.ocmsave.Tag = "2|"
        Me.ocmsave.Text = "SAVE"
        '
        'ocmdelete
        '
        Me.ocmdelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmdelete.Location = New System.Drawing.Point(216, 15)
        Me.ocmdelete.Name = "ocmdelete"
        Me.ocmdelete.Size = New System.Drawing.Size(90, 22)
        Me.ocmdelete.TabIndex = 98
        Me.ocmdelete.TabStop = False
        Me.ocmdelete.Tag = "2|"
        Me.ocmdelete.Text = "Delete"
        Me.ocmdelete.Visible = False
        '
        'ocmadd
        '
        Me.ocmadd.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmadd.Location = New System.Drawing.Point(128, 16)
        Me.ocmadd.Name = "ocmadd"
        Me.ocmadd.Size = New System.Drawing.Size(83, 22)
        Me.ocmadd.TabIndex = 99
        Me.ocmadd.TabStop = False
        Me.ocmadd.Tag = "2|"
        Me.ocmadd.Text = "ADD"
        '
        'ocmload
        '
        Me.ocmload.Location = New System.Drawing.Point(27, 15)
        Me.ocmload.Name = "ocmload"
        Me.ocmload.Size = New System.Drawing.Size(95, 25)
        Me.ocmload.TabIndex = 97
        Me.ocmload.TabStop = False
        Me.ocmload.Tag = "2|"
        Me.ocmload.Text = "LoadData"
        '
        'ocmexit
        '
        Me.ocmexit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmexit.Location = New System.Drawing.Point(696, 7)
        Me.ocmexit.Name = "ocmexit"
        Me.ocmexit.Size = New System.Drawing.Size(95, 25)
        Me.ocmexit.TabIndex = 96
        Me.ocmexit.TabStop = False
        Me.ocmexit.Tag = "2|"
        Me.ocmexit.Text = "EXIT"
        '
        'ImgBarbg
        '
        Me.ImgBarbg.ImageStream = CType(resources.GetObject("ImgBarbg.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImgBarbg.TransparentColor = System.Drawing.Color.Transparent
        Me.ImgBarbg.Images.SetKeyName(0, "01Sky.png")
        Me.ImgBarbg.Images.SetKeyName(1, "02Greenlight.png")
        Me.ImgBarbg.Images.SetKeyName(2, "03Bule.png")
        Me.ImgBarbg.Images.SetKeyName(3, "04Green.png")
        Me.ImgBarbg.Images.SetKeyName(4, "05Yellow.png")
        Me.ImgBarbg.Images.SetKeyName(5, "06Orange.png")
        Me.ImgBarbg.Images.SetKeyName(6, "07Gray.png")
        Me.ImgBarbg.Images.SetKeyName(7, "08BlueBlack.png")
        Me.ImgBarbg.Images.SetKeyName(8, "09Red.png")
        '
        'ImgBarbgAlert
        '
        Me.ImgBarbgAlert.ImageStream = CType(resources.GetObject("ImgBarbgAlert.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImgBarbgAlert.TransparentColor = System.Drawing.Color.Transparent
        Me.ImgBarbgAlert.Images.SetKeyName(0, "11SkyRed.png")
        Me.ImgBarbgAlert.Images.SetKeyName(1, "12GreenlightRed.png")
        Me.ImgBarbgAlert.Images.SetKeyName(2, "13BuleRed.png")
        Me.ImgBarbgAlert.Images.SetKeyName(3, "14GreenRed.png")
        Me.ImgBarbgAlert.Images.SetKeyName(4, "15YellowRed.png")
        Me.ImgBarbgAlert.Images.SetKeyName(5, "16OrangeRed.png")
        Me.ImgBarbgAlert.Images.SetKeyName(6, "17GrayRed.png")
        Me.ImgBarbgAlert.Images.SetKeyName(7, "18BlueBlackRed.png")
        Me.ImgBarbgAlert.Images.SetKeyName(8, "19Red.png")
        '
        'C1STooltip
        '
        Me.C1STooltip.Font = New System.Drawing.Font("Tahoma", 8.0!)
        '
        'mnuplanpopup
        '
        Me.mnuplanpopup.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.mnuplanpopup.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cnuRefreshLine, Me.ToolStripSeparator12, Me.cnuDelJob, Me.ToolStripSeparator13, Me.mnuSkillByStyle, Me.mnuLearnningGroup})
        Me.mnuplanpopup.Name = "ContextMenuStrip1"
        Me.mnuplanpopup.Size = New System.Drawing.Size(182, 104)
        '
        'cnuRefreshLine
        '
        Me.cnuRefreshLine.Name = "cnuRefreshLine"
        Me.cnuRefreshLine.Size = New System.Drawing.Size(181, 22)
        Me.cnuRefreshLine.Text = "Refresh Line"
        '
        'ToolStripSeparator12
        '
        Me.ToolStripSeparator12.Name = "ToolStripSeparator12"
        Me.ToolStripSeparator12.Size = New System.Drawing.Size(178, 6)
        '
        'cnuDelJob
        '
        Me.cnuDelJob.Name = "cnuDelJob"
        Me.cnuDelJob.Size = New System.Drawing.Size(181, 22)
        Me.cnuDelJob.Text = "Delete Job"
        '
        'ToolStripSeparator13
        '
        Me.ToolStripSeparator13.Name = "ToolStripSeparator13"
        Me.ToolStripSeparator13.Size = New System.Drawing.Size(178, 6)
        '
        'mnuSkillByStyle
        '
        Me.mnuSkillByStyle.Name = "mnuSkillByStyle"
        Me.mnuSkillByStyle.Size = New System.Drawing.Size(181, 22)
        Me.mnuSkillByStyle.Text = "Edit Skill By Style"
        '
        'mnuLearnningGroup
        '
        Me.mnuLearnningGroup.Name = "mnuLearnningGroup"
        Me.mnuLearnningGroup.Size = New System.Drawing.Size(181, 22)
        Me.mnuLearnningGroup.Text = "Add Learning Group"
        '
        'wPlanningDevelop
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1074, 594)
        Me.Controls.Add(Me.ogbmainprocbutton)
        Me.Controls.Add(Me.ogbplanning)
        Me.Controls.Add(Me.ogbheader)
        Me.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Name = "wPlanningDevelop"
        Me.Text = "wPlanningDevelop"
        CType(Me.DockManager1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbheader.ResumeLayout(False)
        Me.DockPanel1_Container.ResumeLayout(False)
        CType(Me.FNHSysCmpId_None.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysCmpId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogbplanning, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbplanning.ResumeLayout(False)
        Me.ogbplanning.PerformLayout()
        CType(Me.olbgraphmove, System.ComponentModel.ISupportInitialize).EndInit()
        Me.olbgraphmove.ResumeLayout(False)
        Me.olbgraphmove.PerformLayout()
        CType(Me.olbgraphmoveimage.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvplaning, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbmainprocbutton.ResumeLayout(False)
        Me.mnuplanpopup.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents DockManager1 As DevExpress.XtraBars.Docking.DockManager
    Friend WithEvents ogbheader As DevExpress.XtraBars.Docking.DockPanel
    Friend WithEvents DockPanel1_Container As DevExpress.XtraBars.Docking.ControlContainer
    Friend WithEvents ogbplanning As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ogvplaning As C1.Win.C1FlexGrid.Classic.C1FlexGridClassic
    Friend WithEvents ogbmainprocbutton As DevExpress.XtraEditors.PanelControl
    Friend WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ImgBarbg As System.Windows.Forms.ImageList
    Friend WithEvents olbgraphmove As DevExpress.XtraEditors.PanelControl
    Friend WithEvents olbgraphmoveimage As DevExpress.XtraEditors.PictureEdit
    Friend WithEvents olbgraphmovedesc As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ImgBarbgAlert As System.Windows.Forms.ImageList
    Friend WithEvents lblpositionButtom As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblpositiontop As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysCmpId_None As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FNHSysCmpId_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysCmpId As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents ocmload As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmdelete As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmadd As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmsave As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents C1STooltip As C1.Win.C1SuperTooltip.C1SuperTooltip
    Friend WithEvents mnuplanpopup As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents cnuRefreshLine As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator12 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cnuDelJob As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator13 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuSkillByStyle As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuLearnningGroup As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ocmedit As DevExpress.XtraEditors.SimpleButton
End Class

Imports System.ComponentModel

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Main
    Inherits DevExpress.XtraBars.Ribbon.RibbonForm

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Main))
        Me.MainRibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.MnuPainStyle = New DevExpress.XtraBars.BarSubItem()
        Me.mnusysabout = New DevExpress.XtraBars.BarButtonItem()
        Me.FTUserLogINIP = New DevExpress.XtraBars.BarStaticItem()
        Me.olbdate = New DevExpress.XtraBars.BarStaticItem()
        Me.olbtime = New DevExpress.XtraBars.BarStaticItem()
        Me.olbcmp = New DevExpress.XtraBars.BarStaticItem()
        Me.olbcopyright = New DevExpress.XtraBars.BarStaticItem()
        Me.RibbonGalleryBarItem1 = New DevExpress.XtraBars.RibbonGalleryBarItem()
        Me.RepFNlang = New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox()
        Me.RibbonStatusBar = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.imagemenuList = New System.Windows.Forms.ImageList(Me.components)
        Me.otmtime = New System.Windows.Forms.Timer(Me.components)
        Me.MdiManager = New DevExpress.XtraTabbedMdi.XtraTabbedMdiManager(Me.components)
        Me.MainDefaultLookAndFeel = New DevExpress.LookAndFeel.DefaultLookAndFeel(Me.components)
        Me.onvbar = New DevExpress.XtraNavBar.NavBarControl()
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.FNFont = New DevExpress.XtraEditors.ImageComboBoxEdit()
        Me.ImgFont = New System.Windows.Forms.ImageList(Me.components)
        Me.opmmail = New DevExpress.XtraEditors.PictureEdit()
        Me.FNLang = New DevExpress.XtraEditors.ImageComboBoxEdit()
        Me.ImgLang = New System.Windows.Forms.ImageList(Me.components)
        Me.FTUsername = New DevExpress.XtraEditors.LabelControl()
        Me.FPUserImage = New DevExpress.XtraEditors.PictureEdit()
        Me.oDockManager = New DevExpress.XtraBars.Docking.DockManager(Me.components)
        Me.odocpanal = New DevExpress.XtraBars.Docking.DockPanel()
        Me.DockPanel1_Container = New DevExpress.XtraBars.Docking.ControlContainer()
        Me.StandaloneBarDockControl = New DevExpress.XtraBars.StandaloneBarDockControl()
        Me.otmchkuserlogin = New System.Windows.Forms.Timer(Me.components)
        Me.ocmcheckapp = New System.Windows.Forms.Timer(Me.components)
        Me.otmpdf = New System.Windows.Forms.Timer(Me.components)
        Me.otbcheckmail = New System.Windows.Forms.Timer(Me.components)
        Me.ocmcheckappdirector = New System.Windows.Forms.Timer(Me.components)
        Me.ocmcheckmanagerfactoryapp = New System.Windows.Forms.Timer(Me.components)
        Me.ocmcheckwhappcm = New System.Windows.Forms.Timer(Me.components)
        Me.otmcheckmerapptvw = New System.Windows.Forms.Timer(Me.components)
        Me.otmcheckinvoicecharge = New System.Windows.Forms.Timer(Me.components)
        Me.otmsewinglineleader = New System.Windows.Forms.Timer(Me.components)
        Me.otmqafinalleader = New System.Windows.Forms.Timer(Me.components)
        Me.otmdctimer = New System.Windows.Forms.Timer(Me.components)
        Me.otmcheckappr = New System.Windows.Forms.Timer(Me.components)
        Me.otmChkOrderCostApp = New System.Windows.Forms.Timer(Me.components)
        Me.otmChkEmpLeaveApp = New System.Windows.Forms.Timer(Me.components)
        Me.otmqastyleriskcritical = New System.Windows.Forms.Timer(Me.components)
        Me.otmcheckAssetPR = New System.Windows.Forms.Timer(Me.components)
        Me.otmDirectorAssetPO = New System.Windows.Forms.Timer(Me.components)
        Me.otmDirectorAssetPR = New System.Windows.Forms.Timer(Me.components)
        Me.otmAssetPo = New System.Windows.Forms.Timer(Me.components)
        Me.otmwisdomservice = New System.Windows.Forms.Timer(Me.components)
        CType(Me.MainRibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepFNlang, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MdiManager, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.onvbar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.FNFont.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.opmmail.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNLang.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FPUserImage.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.oDockManager, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.odocpanal.SuspendLayout()
        Me.DockPanel1_Container.SuspendLayout()
        Me.SuspendLayout()
        '
        'MainRibbonControl
        '
        Me.MainRibbonControl.AllowKeyTips = False
        Me.MainRibbonControl.AllowMdiChildButtons = False
        Me.MainRibbonControl.AllowMinimizeRibbon = False
        Me.MainRibbonControl.ApplicationButtonImageOptions.Image = CType(resources.GetObject("MainRibbonControl.ApplicationButtonImageOptions.Image"), System.Drawing.Image)
        Me.MainRibbonControl.ApplicationButtonText = Nothing
        Me.MainRibbonControl.ExpandCollapseItem.Id = 0
        Me.MainRibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.MainRibbonControl.ExpandCollapseItem, Me.MainRibbonControl.SearchEditItem, Me.MnuPainStyle, Me.mnusysabout, Me.FTUserLogINIP, Me.olbdate, Me.olbtime, Me.olbcmp, Me.olbcopyright, Me.RibbonGalleryBarItem1})
        Me.MainRibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.MainRibbonControl.Margin = New System.Windows.Forms.Padding(6, 5, 6, 5)
        Me.MainRibbonControl.MaxItemId = 17
        Me.MainRibbonControl.Name = "MainRibbonControl"
        Me.MainRibbonControl.OptionsPageCategories.Alignment = DevExpress.XtraBars.Ribbon.RibbonPageCategoryAlignment.Left
        Me.MainRibbonControl.QuickToolbarItemLinks.Add(Me.MnuPainStyle)
        Me.MainRibbonControl.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepFNlang})
        Me.MainRibbonControl.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.MainRibbonControl.ShowDisplayOptionsMenuButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.MainRibbonControl.ShowExpandCollapseButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.MainRibbonControl.ShowMoreCommandsButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.MainRibbonControl.ShowPageHeadersInFormCaption = DevExpress.Utils.DefaultBoolean.[False]
        Me.MainRibbonControl.ShowPageHeadersMode = DevExpress.XtraBars.Ribbon.ShowPageHeadersMode.ShowOnMultiplePages
        Me.MainRibbonControl.ShowQatLocationSelector = False
        Me.MainRibbonControl.ShowToolbarCustomizeItem = False
        Me.MainRibbonControl.Size = New System.Drawing.Size(1756, 32)
        Me.MainRibbonControl.StatusBar = Me.RibbonStatusBar
        Me.MainRibbonControl.Toolbar.ShowCustomizeItem = False
        '
        'MnuPainStyle
        '
        Me.MnuPainStyle.Id = 1
        Me.MnuPainStyle.ImageOptions.Image = CType(resources.GetObject("MnuPainStyle.ImageOptions.Image"), System.Drawing.Image)
        Me.MnuPainStyle.Name = "MnuPainStyle"
        '
        'mnusysabout
        '
        Me.mnusysabout.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right
        Me.mnusysabout.Caption = "About"
        Me.mnusysabout.Id = 2
        Me.mnusysabout.Name = "mnusysabout"
        '
        'FTUserLogINIP
        '
        Me.FTUserLogINIP.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Left
        Me.FTUserLogINIP.AutoSize = DevExpress.XtraBars.BarStaticItemSize.None
        Me.FTUserLogINIP.Id = 3
        Me.FTUserLogINIP.Name = "FTUserLogINIP"
        Me.FTUserLogINIP.TextAlignment = System.Drawing.StringAlignment.Center
        Me.FTUserLogINIP.Width = 150
        '
        'olbdate
        '
        Me.olbdate.Id = 8
        Me.olbdate.Name = "olbdate"
        '
        'olbtime
        '
        Me.olbtime.Id = 9
        Me.olbtime.Name = "olbtime"
        '
        'olbcmp
        '
        Me.olbcmp.Id = 16
        Me.olbcmp.Name = "olbcmp"
        Me.olbcmp.TextAlignment = System.Drawing.StringAlignment.Center
        '
        'olbcopyright
        '
        Me.olbcopyright.AutoSize = DevExpress.XtraBars.BarStaticItemSize.Spring
        Me.olbcopyright.Id = 10
        Me.olbcopyright.Name = "olbcopyright"
        Me.olbcopyright.TextAlignment = System.Drawing.StringAlignment.Center
        '
        'RibbonGalleryBarItem1
        '
        Me.RibbonGalleryBarItem1.Caption = "InplaceGallery1"
        Me.RibbonGalleryBarItem1.Id = 13
        Me.RibbonGalleryBarItem1.Name = "RibbonGalleryBarItem1"
        '
        'RepFNlang
        '
        Me.RepFNlang.AutoHeight = False
        Me.RepFNlang.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepFNlang.Name = "RepFNlang"
        '
        'RibbonStatusBar
        '
        Me.RibbonStatusBar.ItemLinks.Add(Me.olbcmp, True)
        Me.RibbonStatusBar.ItemLinks.Add(Me.FTUserLogINIP, True)
        Me.RibbonStatusBar.ItemLinks.Add(Me.olbdate, True)
        Me.RibbonStatusBar.ItemLinks.Add(Me.olbtime)
        Me.RibbonStatusBar.ItemLinks.Add(Me.olbcopyright)
        Me.RibbonStatusBar.ItemLinks.Add(Me.mnusysabout)
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 960)
        Me.RibbonStatusBar.Margin = New System.Windows.Forms.Padding(6, 5, 6, 5)
        Me.RibbonStatusBar.Name = "RibbonStatusBar"
        Me.RibbonStatusBar.Ribbon = Me.MainRibbonControl
        Me.RibbonStatusBar.Size = New System.Drawing.Size(1756, 24)
        '
        'imagemenuList
        '
        Me.imagemenuList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit
        Me.imagemenuList.ImageSize = New System.Drawing.Size(16, 16)
        Me.imagemenuList.TransparentColor = System.Drawing.Color.Magenta
        '
        'otmtime
        '
        Me.otmtime.Enabled = True
        Me.otmtime.Interval = 1000
        '
        'MdiManager
        '
        Me.MdiManager.HeaderButtons = CType((DevExpress.XtraTab.TabButtons.Prev Or DevExpress.XtraTab.TabButtons.[Next]), DevExpress.XtraTab.TabButtons)
        Me.MdiManager.MdiParent = Nothing
        '
        'MainDefaultLookAndFeel
        '
        Me.MainDefaultLookAndFeel.LookAndFeel.SkinName = "McSkin"
        '
        'onvbar
        '
        Me.onvbar.Dock = System.Windows.Forms.DockStyle.Fill
        Me.onvbar.DragDropFlags = DevExpress.XtraNavBar.NavBarDragDrop.None
        Me.onvbar.Location = New System.Drawing.Point(0, 107)
        Me.onvbar.Margin = New System.Windows.Forms.Padding(6, 5, 6, 5)
        Me.onvbar.Name = "onvbar"
        Me.onvbar.OptionsNavPane.ExpandedWidth = 310
        Me.onvbar.PaintStyleKind = DevExpress.XtraNavBar.NavBarViewKind.ExplorerBar
        Me.onvbar.Size = New System.Drawing.Size(310, 792)
        Me.onvbar.TabIndex = 1
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.FNFont)
        Me.PanelControl1.Controls.Add(Me.opmmail)
        Me.PanelControl1.Controls.Add(Me.FNLang)
        Me.PanelControl1.Controls.Add(Me.FTUsername)
        Me.PanelControl1.Controls.Add(Me.FPUserImage)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelControl1.Location = New System.Drawing.Point(0, 0)
        Me.PanelControl1.Margin = New System.Windows.Forms.Padding(6, 5, 6, 5)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(310, 107)
        Me.PanelControl1.TabIndex = 0
        '
        'FNFont
        '
        Me.FNFont.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FNFont.Location = New System.Drawing.Point(98, 74)
        Me.FNFont.Margin = New System.Windows.Forms.Padding(4)
        Me.FNFont.MenuManager = Me.MainRibbonControl
        Me.FNFont.Name = "FNFont"
        Me.FNFont.Properties.AllowMouseWheel = False
        Me.FNFont.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FNFont.Properties.LargeImages = Me.ImgFont
        Me.FNFont.Size = New System.Drawing.Size(205, 20)
        Me.FNFont.TabIndex = 9
        '
        'ImgFont
        '
        Me.ImgFont.ImageStream = CType(resources.GetObject("ImgFont.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImgFont.TransparentColor = System.Drawing.Color.Transparent
        Me.ImgFont.Images.SetKeyName(0, "Font.jpg")
        '
        'opmmail
        '
        Me.opmmail.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.opmmail.Cursor = System.Windows.Forms.Cursors.Hand
        Me.opmmail.EditValue = CType(resources.GetObject("opmmail.EditValue"), Object)
        Me.opmmail.Location = New System.Drawing.Point(256, 5)
        Me.opmmail.Margin = New System.Windows.Forms.Padding(6, 5, 6, 5)
        Me.opmmail.Name = "opmmail"
        Me.opmmail.Properties.ReadOnly = True
        Me.opmmail.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch
        Me.opmmail.Size = New System.Drawing.Size(51, 37)
        Me.opmmail.TabIndex = 8
        Me.opmmail.Visible = False
        '
        'FNLang
        '
        Me.FNLang.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FNLang.Location = New System.Drawing.Point(98, 47)
        Me.FNLang.Margin = New System.Windows.Forms.Padding(4)
        Me.FNLang.MenuManager = Me.MainRibbonControl
        Me.FNLang.Name = "FNLang"
        Me.FNLang.Properties.AllowMouseWheel = False
        Me.FNLang.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FNLang.Properties.LargeImages = Me.ImgLang
        Me.FNLang.Size = New System.Drawing.Size(205, 20)
        Me.FNLang.TabIndex = 7
        '
        'ImgLang
        '
        Me.ImgLang.ImageStream = CType(resources.GetObject("ImgLang.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImgLang.TransparentColor = System.Drawing.Color.Transparent
        Me.ImgLang.Images.SetKeyName(0, "English.jpg")
        Me.ImgLang.Images.SetKeyName(1, "Thai.jpg")
        Me.ImgLang.Images.SetKeyName(2, "Vitenam.gif")
        Me.ImgLang.Images.SetKeyName(3, "KM.jpg")
        Me.ImgLang.Images.SetKeyName(4, "Myanmar.jpg")
        Me.ImgLang.Images.SetKeyName(5, "laos.jpg")
        Me.ImgLang.Images.SetKeyName(6, "China.jpg")
        '
        'FTUsername
        '
        Me.FTUsername.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FTUsername.Appearance.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.FTUsername.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTUsername.Appearance.Options.UseFont = True
        Me.FTUsername.Appearance.Options.UseForeColor = True
        Me.FTUsername.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTUsername.Location = New System.Drawing.Point(98, 10)
        Me.FTUsername.Margin = New System.Windows.Forms.Padding(6, 5, 6, 5)
        Me.FTUsername.Name = "FTUsername"
        Me.FTUsername.Size = New System.Drawing.Size(151, 29)
        Me.FTUsername.TabIndex = 1
        Me.FTUsername.Text = "Wisdom System Admin"
        '
        'FPUserImage
        '
        Me.FPUserImage.Cursor = System.Windows.Forms.Cursors.Hand
        Me.FPUserImage.EditValue = CType(resources.GetObject("FPUserImage.EditValue"), Object)
        Me.FPUserImage.Location = New System.Drawing.Point(2, 5)
        Me.FPUserImage.Margin = New System.Windows.Forms.Padding(6, 5, 6, 5)
        Me.FPUserImage.Name = "FPUserImage"
        Me.FPUserImage.Properties.ReadOnly = True
        Me.FPUserImage.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch
        Me.FPUserImage.Size = New System.Drawing.Size(87, 91)
        Me.FPUserImage.TabIndex = 0
        '
        'oDockManager
        '
        Me.oDockManager.Form = Me
        Me.oDockManager.RootPanels.AddRange(New DevExpress.XtraBars.Docking.DockPanel() {Me.odocpanal})
        Me.oDockManager.TopZIndexControls.AddRange(New String() {"DevExpress.XtraBars.BarDockControl", "DevExpress.XtraBars.StandaloneBarDockControl", "System.Windows.Forms.StatusBar", "DevExpress.XtraBars.Ribbon.RibbonStatusBar", "DevExpress.XtraBars.Ribbon.RibbonControl"})
        '
        'odocpanal
        '
        Me.odocpanal.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.odocpanal.Appearance.Image = CType(resources.GetObject("odocpanal.Appearance.Image"), System.Drawing.Image)
        Me.odocpanal.Appearance.Options.UseForeColor = True
        Me.odocpanal.Appearance.Options.UseImage = True
        Me.odocpanal.Appearance.Options.UseTextOptions = True
        Me.odocpanal.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.odocpanal.Controls.Add(Me.DockPanel1_Container)
        Me.odocpanal.Cursor = System.Windows.Forms.Cursors.Hand
        Me.odocpanal.Dock = DevExpress.XtraBars.Docking.DockingStyle.Left
        Me.odocpanal.DockVertical = DevExpress.Utils.DefaultBoolean.[False]
        Me.odocpanal.ID = New System.Guid("77b9346d-8d15-4323-af1e-af82afa9902a")
        Me.odocpanal.ImageOptions.Image = CType(resources.GetObject("odocpanal.ImageOptions.Image"), System.Drawing.Image)
        Me.odocpanal.Location = New System.Drawing.Point(0, 32)
        Me.odocpanal.Margin = New System.Windows.Forms.Padding(4)
        Me.odocpanal.Name = "odocpanal"
        Me.odocpanal.Options.AllowDockAsTabbedDocument = False
        Me.odocpanal.Options.AllowDockBottom = False
        Me.odocpanal.Options.AllowDockFill = False
        Me.odocpanal.Options.AllowDockTop = False
        Me.odocpanal.Options.AllowFloating = False
        Me.odocpanal.Options.FloatOnDblClick = False
        Me.odocpanal.Options.ShowCloseButton = False
        Me.odocpanal.Options.ShowMaximizeButton = False
        Me.odocpanal.OriginalSize = New System.Drawing.Size(317, 901)
        Me.odocpanal.Size = New System.Drawing.Size(317, 928)
        Me.odocpanal.TabStop = False
        Me.odocpanal.TabText = "MENU"
        Me.odocpanal.Text = "MENU"
        '
        'DockPanel1_Container
        '
        Me.DockPanel1_Container.Controls.Add(Me.onvbar)
        Me.DockPanel1_Container.Controls.Add(Me.PanelControl1)
        Me.DockPanel1_Container.Location = New System.Drawing.Point(3, 26)
        Me.DockPanel1_Container.Margin = New System.Windows.Forms.Padding(4)
        Me.DockPanel1_Container.Name = "DockPanel1_Container"
        Me.DockPanel1_Container.Size = New System.Drawing.Size(310, 899)
        Me.DockPanel1_Container.TabIndex = 0
        '
        'StandaloneBarDockControl
        '
        Me.StandaloneBarDockControl.AutoSize = True
        Me.StandaloneBarDockControl.CausesValidation = False
        Me.StandaloneBarDockControl.Dock = System.Windows.Forms.DockStyle.Top
        Me.StandaloneBarDockControl.Location = New System.Drawing.Point(0, 32)
        Me.StandaloneBarDockControl.Manager = Nothing
        Me.StandaloneBarDockControl.Margin = New System.Windows.Forms.Padding(4)
        Me.StandaloneBarDockControl.Name = "StandaloneBarDockControl"
        Me.StandaloneBarDockControl.Size = New System.Drawing.Size(1756, 0)
        Me.StandaloneBarDockControl.Text = "StandaloneBarDockControl1"
        '
        'otmchkuserlogin
        '
        Me.otmchkuserlogin.Interval = 600000
        '
        'ocmcheckapp
        '
        Me.ocmcheckapp.Interval = 120000
        '
        'otmpdf
        '
        Me.otmpdf.Interval = 180000
        '
        'otbcheckmail
        '
        Me.otbcheckmail.Interval = 300000
        '
        'ocmcheckappdirector
        '
        Me.ocmcheckappdirector.Interval = 60000
        '
        'ocmcheckmanagerfactoryapp
        '
        Me.ocmcheckmanagerfactoryapp.Interval = 60000
        '
        'ocmcheckwhappcm
        '
        Me.ocmcheckwhappcm.Interval = 60000
        '
        'otmcheckmerapptvw
        '
        Me.otmcheckmerapptvw.Interval = 60000
        '
        'otmcheckinvoicecharge
        '
        Me.otmcheckinvoicecharge.Interval = 600000
        '
        'otmsewinglineleader
        '
        Me.otmsewinglineleader.Interval = 60000
        '
        'otmqafinalleader
        '
        Me.otmqafinalleader.Interval = 60000
        '
        'otmdctimer
        '
        Me.otmdctimer.Interval = 60000
        '
        'otmcheckappr
        '
        Me.otmcheckappr.Interval = 60000
        '
        'otmChkOrderCostApp
        '
        Me.otmChkOrderCostApp.Interval = 60000
        '
        'otmChkEmpLeaveApp
        '
        Me.otmChkEmpLeaveApp.Interval = 60000
        '
        'otmqastyleriskcritical
        '
        Me.otmqastyleriskcritical.Interval = 60000
        '
        'otmcheckAssetPR
        '
        Me.otmcheckAssetPR.Interval = 120000
        '
        'otmDirectorAssetPO
        '
        Me.otmDirectorAssetPO.Interval = 120000
        '
        'otmDirectorAssetPR
        '
        Me.otmDirectorAssetPR.Interval = 120000
        '
        'otmAssetPo
        '
        Me.otmAssetPo.Interval = 120000
        '
        'otmwisdomservice
        '
        Me.otmwisdomservice.Interval = 600000
        '
        'Main
        '
        Me.Appearance.Options.UseFont = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1756, 984)
        Me.Controls.Add(Me.odocpanal)
        Me.Controls.Add(Me.StandaloneBarDockControl)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.MainRibbonControl)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.IconOptions.Icon = CType(resources.GetObject("Main.IconOptions.Icon"), System.Drawing.Icon)
        Me.IsMdiContainer = True
        Me.Margin = New System.Windows.Forms.Padding(6, 5, 6, 5)
        Me.MinimumSize = New System.Drawing.Size(800, 600)
        Me.Name = "Main"
        Me.Ribbon = Me.MainRibbonControl
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.StatusBar = Me.RibbonStatusBar
        Me.Tag = "|WISDOM SYSTEM|WISDOM SYSTEM|WISDOM SYSTEM|WISDOM SYSTEM|WISDOM SYSTEM|WISDOM SYS" &
    "TEM|WISDOM SYSTEM"
        Me.Text = "WISDOM SYSTEM"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.MainRibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepFNlang, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MdiManager, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.onvbar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        CType(Me.FNFont.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.opmmail.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNLang.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FPUserImage.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.oDockManager, System.ComponentModel.ISupportInitialize).EndInit()
        Me.odocpanal.ResumeLayout(False)
        Me.DockPanel1_Container.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents RibbonStatusBar As DevExpress.XtraBars.Ribbon.RibbonStatusBar
    Friend WithEvents MnuPainStyle As DevExpress.XtraBars.BarSubItem
    Private WithEvents imagemenuList As System.Windows.Forms.ImageList
    Friend WithEvents otmtime As System.Windows.Forms.Timer
    Friend WithEvents MdiManager As DevExpress.XtraTabbedMdi.XtraTabbedMdiManager
    Friend WithEvents MainDefaultLookAndFeel As DevExpress.LookAndFeel.DefaultLookAndFeel
    Friend WithEvents mnusysabout As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents onvbar As DevExpress.XtraNavBar.NavBarControl
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents FTUsername As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FPUserImage As DevExpress.XtraEditors.PictureEdit
    Friend WithEvents oDockManager As DevExpress.XtraBars.Docking.DockManager
    Friend WithEvents odocpanal As DevExpress.XtraBars.Docking.DockPanel
    Friend WithEvents DockPanel1_Container As DevExpress.XtraBars.Docking.ControlContainer
    Friend WithEvents StandaloneBarDockControl As DevExpress.XtraBars.StandaloneBarDockControl
    Friend WithEvents FTUserLogINIP As DevExpress.XtraBars.BarStaticItem
    Friend WithEvents otmchkuserlogin As System.Windows.Forms.Timer
    Friend WithEvents ImgLang As System.Windows.Forms.ImageList
    Public WithEvents MainRibbonControl As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents olbdate As DevExpress.XtraBars.BarStaticItem
    Friend WithEvents olbtime As DevExpress.XtraBars.BarStaticItem
    Friend WithEvents olbcmp As DevExpress.XtraBars.BarStaticItem
    Friend WithEvents olbcopyright As DevExpress.XtraBars.BarStaticItem
    Friend WithEvents RibbonGalleryBarItem1 As DevExpress.XtraBars.RibbonGalleryBarItem
    Friend WithEvents RepFNlang As DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
    Friend WithEvents FNLang As DevExpress.XtraEditors.ImageComboBoxEdit
    Friend WithEvents ocmcheckapp As System.Windows.Forms.Timer
    Friend WithEvents otmpdf As System.Windows.Forms.Timer
    Friend WithEvents opmmail As DevExpress.XtraEditors.PictureEdit
    Friend WithEvents otbcheckmail As System.Windows.Forms.Timer
    Friend WithEvents ocmcheckappdirector As System.Windows.Forms.Timer
    Friend WithEvents ocmcheckmanagerfactoryapp As System.Windows.Forms.Timer
    Friend WithEvents ocmcheckwhappcm As System.Windows.Forms.Timer
    Friend WithEvents otmcheckmerapptvw As System.Windows.Forms.Timer
    Friend WithEvents otmcheckinvoicecharge As System.Windows.Forms.Timer
    Friend WithEvents otmsewinglineleader As System.Windows.Forms.Timer
    Friend WithEvents otmqafinalleader As System.Windows.Forms.Timer
    Friend WithEvents otmdctimer As System.Windows.Forms.Timer
    Friend WithEvents otmcheckappr As System.Windows.Forms.Timer
    Friend WithEvents otmChkOrderCostApp As System.Windows.Forms.Timer
    Friend WithEvents FNFont As DevExpress.XtraEditors.ImageComboBoxEdit
    Friend WithEvents ImgFont As System.Windows.Forms.ImageList
    Friend WithEvents otmChkEmpLeaveApp As System.Windows.Forms.Timer
    Friend WithEvents otmqastyleriskcritical As System.Windows.Forms.Timer
    Friend WithEvents otmcheckAssetPR As System.Windows.Forms.Timer
    Friend WithEvents otmDirectorAssetPO As System.Windows.Forms.Timer
    Friend WithEvents otmDirectorAssetPR As System.Windows.Forms.Timer
    Friend WithEvents otmAssetPo As System.Windows.Forms.Timer
    Friend WithEvents otmwisdomservice As System.Windows.Forms.Timer
End Class

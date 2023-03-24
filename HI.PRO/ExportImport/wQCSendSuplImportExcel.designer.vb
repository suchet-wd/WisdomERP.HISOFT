<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wQCSendSuplImportExcel
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(wQCSendSuplImportExcel))
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject4 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject5 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject6 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.DockManager = New DevExpress.XtraBars.Docking.DockManager(Me.components)
        Me.oCriteria = New DevExpress.XtraBars.Docking.DockPanel()
        Me.DockPanel1_Container = New DevExpress.XtraBars.Docking.ControlContainer()
        Me.FNHSysSuplId = New DevExpress.XtraEditors.ButtonEdit()
        Me.FTEndSendSupl = New DevExpress.XtraEditors.DateEdit()
        Me.FNHSysCmpId_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysSuplId_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysCmpId_None = New DevExpress.XtraEditors.TextEdit()
        Me.FTEndSendSupl_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysCmpId = New DevExpress.XtraEditors.ButtonEdit()
        Me.FNHSysSuplId_None = New DevExpress.XtraEditors.TextEdit()
        Me.FTStartSendSupl = New DevExpress.XtraEditors.DateEdit()
        Me.FTStartSendSupl_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTOrderNoTo_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysBuyId = New DevExpress.XtraEditors.ButtonEdit()
        Me.FNHSysStyleId_None = New DevExpress.XtraEditors.TextEdit()
        Me.FNHSysBuyId_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTOrderNoTo = New DevExpress.XtraEditors.ButtonEdit()
        Me.FNHSysStyleId = New DevExpress.XtraEditors.ButtonEdit()
        Me.FTOrderNo_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysStyleId_lbl2 = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysBuyId_None = New DevExpress.XtraEditors.TextEdit()
        Me.FTOrderNo = New DevExpress.XtraEditors.ButtonEdit()
        Me.ogbmainprocbutton = New DevExpress.XtraEditors.PanelControl()
        Me.ocmsave = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmReadExcel = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.otabDetail = New DevExpress.XtraTab.XtraTabControl()
        Me.otabpageEm = New DevExpress.XtraTab.XtraTabPage()
        Me.ogcdetail = New DevExpress.XtraGrid.GridControl()
        Me.otbdynmic = New DevExpress.XtraTab.XtraTabPage()
        Me.otbdynamicdetails = New DevExpress.XtraTab.XtraTabControl()
        Me.GridBand1 = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.GridBand2 = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.GridBand3 = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.GridBand4 = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.GridBand5 = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.hideContainerTop = New DevExpress.XtraBars.Docking.AutoHideContainer()
        Me.ogvdetail = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.ogvGDetailgbFDSendSuplDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ogvGDetailgbFNBunbleSeq = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ogvGDetailgbFNFacDefectQty = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ogvGDetailgbFNQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ogvGDetailgbFNTableNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ogvGDetailgbFTCheckBy = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ogvGDetailgbFTColorway = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ogvGDetailgbFTOrderNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ogvGDetailgbFTPORef = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ogvGDetailgbFTPartName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ogvGDetailgbFTSendSuplNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ogvGDetailgbFTSizeBreakDown = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ogvGDetailgbFTStyleCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ogvGDetailgbFTSuplCode = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.DockManager, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.oCriteria.SuspendLayout()
        Me.DockPanel1_Container.SuspendLayout()
        CType(Me.FNHSysSuplId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTEndSendSupl.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTEndSendSupl.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysCmpId_None.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysCmpId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysSuplId_None.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTStartSendSupl.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTStartSendSupl.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysBuyId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysStyleId_None.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTOrderNoTo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysStyleId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysBuyId_None.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTOrderNo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbmainprocbutton.SuspendLayout()
        CType(Me.otabDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.otabDetail.SuspendLayout()
        Me.otabpageEm.SuspendLayout()
        CType(Me.ogcdetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.otbdynmic.SuspendLayout()
        CType(Me.otbdynamicdetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvdetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DockManager
        '
        Me.DockManager.Form = Me
        Me.DockManager.HiddenPanels.AddRange(New DevExpress.XtraBars.Docking.DockPanel() {Me.oCriteria})
        Me.DockManager.TopZIndexControls.AddRange(New String() {"DevExpress.XtraBars.BarDockControl", "DevExpress.XtraBars.StandaloneBarDockControl", "System.Windows.Forms.StatusBar", "System.Windows.Forms.MenuStrip", "System.Windows.Forms.StatusStrip", "DevExpress.XtraBars.Ribbon.RibbonStatusBar", "DevExpress.XtraBars.Ribbon.RibbonControl"})
        '
        'oCriteria
        '
        Me.oCriteria.Controls.Add(Me.DockPanel1_Container)
        Me.oCriteria.Dock = DevExpress.XtraBars.Docking.DockingStyle.Top
        Me.oCriteria.ID = New System.Guid("aa291f06-3140-48e7-b9cf-391318277a0d")
        Me.oCriteria.Image = CType(resources.GetObject("oCriteria.Image"), System.Drawing.Image)
        Me.oCriteria.Location = New System.Drawing.Point(0, -185)
        Me.oCriteria.Name = "oCriteria"
        Me.oCriteria.Options.AllowDockBottom = False
        Me.oCriteria.Options.AllowDockFill = False
        Me.oCriteria.Options.FloatOnDblClick = False
        Me.oCriteria.Options.ShowCloseButton = False
        Me.oCriteria.OriginalSize = New System.Drawing.Size(200, 185)
        Me.oCriteria.SavedDock = DevExpress.XtraBars.Docking.DockingStyle.Top
        Me.oCriteria.SavedIndex = 0
        Me.oCriteria.Size = New System.Drawing.Size(1090, 185)
        Me.oCriteria.Text = "Criteria"
        Me.oCriteria.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden
        '
        'DockPanel1_Container
        '
        Me.DockPanel1_Container.Controls.Add(Me.FNHSysSuplId)
        Me.DockPanel1_Container.Controls.Add(Me.FTEndSendSupl)
        Me.DockPanel1_Container.Controls.Add(Me.FNHSysCmpId_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FNHSysSuplId_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FNHSysCmpId_None)
        Me.DockPanel1_Container.Controls.Add(Me.FTEndSendSupl_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FNHSysCmpId)
        Me.DockPanel1_Container.Controls.Add(Me.FNHSysSuplId_None)
        Me.DockPanel1_Container.Controls.Add(Me.FTStartSendSupl)
        Me.DockPanel1_Container.Controls.Add(Me.FTStartSendSupl_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FTOrderNoTo_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FNHSysBuyId)
        Me.DockPanel1_Container.Controls.Add(Me.FNHSysStyleId_None)
        Me.DockPanel1_Container.Controls.Add(Me.FNHSysBuyId_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FTOrderNoTo)
        Me.DockPanel1_Container.Controls.Add(Me.FNHSysStyleId)
        Me.DockPanel1_Container.Controls.Add(Me.FTOrderNo_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FNHSysStyleId_lbl2)
        Me.DockPanel1_Container.Controls.Add(Me.FNHSysBuyId_None)
        Me.DockPanel1_Container.Controls.Add(Me.FTOrderNo)
        Me.DockPanel1_Container.Location = New System.Drawing.Point(4, 23)
        Me.DockPanel1_Container.Name = "DockPanel1_Container"
        Me.DockPanel1_Container.Size = New System.Drawing.Size(1082, 158)
        Me.DockPanel1_Container.TabIndex = 0
        '
        'FNHSysSuplId
        '
        Me.FNHSysSuplId.EnterMoveNextControl = True
        Me.FNHSysSuplId.Location = New System.Drawing.Point(156, 90)
        Me.FNHSysSuplId.Name = "FNHSysSuplId"
        Me.FNHSysSuplId.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FNHSysSuplId.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysSuplId.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysSuplId.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysSuplId.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FNHSysSuplId.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FNHSysSuplId.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNHSysSuplId.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysSuplId.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNHSysSuplId.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNHSysSuplId.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysSuplId.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysSuplId.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNHSysSuplId.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNHSysSuplId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", 15, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", "177", Nothing, True)})
        Me.FNHSysSuplId.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.FNHSysSuplId.Properties.MaxLength = 30
        Me.FNHSysSuplId.Size = New System.Drawing.Size(131, 20)
        Me.FNHSysSuplId.TabIndex = 414
        Me.FNHSysSuplId.Tag = "2|"
        '
        'FTEndSendSupl
        '
        Me.FTEndSendSupl.EditValue = Nothing
        Me.FTEndSendSupl.EnterMoveNextControl = True
        Me.FTEndSendSupl.Location = New System.Drawing.Point(475, 69)
        Me.FTEndSendSupl.Name = "FTEndSendSupl"
        Me.FTEndSendSupl.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTEndSendSupl.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FTEndSendSupl.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.FTEndSendSupl.Properties.DisplayFormat.FormatString = "d"
        Me.FTEndSendSupl.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTEndSendSupl.Properties.EditFormat.FormatString = "d"
        Me.FTEndSendSupl.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTEndSendSupl.Properties.NullDate = ""
        Me.FTEndSendSupl.Size = New System.Drawing.Size(130, 20)
        Me.FTEndSendSupl.TabIndex = 402
        Me.FTEndSendSupl.Tag = "2|"
        '
        'FNHSysCmpId_lbl
        '
        Me.FNHSysCmpId_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysCmpId_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysCmpId_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysCmpId_lbl.Location = New System.Drawing.Point(8, 3)
        Me.FNHSysCmpId_lbl.Name = "FNHSysCmpId_lbl"
        Me.FNHSysCmpId_lbl.Size = New System.Drawing.Size(145, 17)
        Me.FNHSysCmpId_lbl.TabIndex = 511
        Me.FNHSysCmpId_lbl.Tag = "2|"
        Me.FNHSysCmpId_lbl.Text = "Company :"
        '
        'FNHSysSuplId_lbl
        '
        Me.FNHSysSuplId_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FNHSysSuplId_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysSuplId_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysSuplId_lbl.Location = New System.Drawing.Point(0, 90)
        Me.FNHSysSuplId_lbl.Name = "FNHSysSuplId_lbl"
        Me.FNHSysSuplId_lbl.Size = New System.Drawing.Size(153, 19)
        Me.FNHSysSuplId_lbl.TabIndex = 415
        Me.FNHSysSuplId_lbl.Tag = "2|"
        Me.FNHSysSuplId_lbl.Text = "Supplier :"
        '
        'FNHSysCmpId_None
        '
        Me.FNHSysCmpId_None.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FNHSysCmpId_None.Location = New System.Drawing.Point(288, 3)
        Me.FNHSysCmpId_None.Name = "FNHSysCmpId_None"
        Me.FNHSysCmpId_None.Properties.Appearance.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysCmpId_None.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysCmpId_None.Properties.ReadOnly = True
        Me.FNHSysCmpId_None.Size = New System.Drawing.Size(714, 20)
        Me.FNHSysCmpId_None.TabIndex = 512
        Me.FNHSysCmpId_None.Tag = ""
        '
        'FTEndSendSupl_lbl
        '
        Me.FTEndSendSupl_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTEndSendSupl_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTEndSendSupl_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTEndSendSupl_lbl.Location = New System.Drawing.Point(288, 68)
        Me.FTEndSendSupl_lbl.Name = "FTEndSendSupl_lbl"
        Me.FTEndSendSupl_lbl.Size = New System.Drawing.Size(181, 19)
        Me.FTEndSendSupl_lbl.TabIndex = 403
        Me.FTEndSendSupl_lbl.Tag = "2|"
        Me.FTEndSendSupl_lbl.Text = "End SendSupl :"
        '
        'FNHSysCmpId
        '
        Me.FNHSysCmpId.Location = New System.Drawing.Point(156, 3)
        Me.FNHSysCmpId.Name = "FNHSysCmpId"
        Me.FNHSysCmpId.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysCmpId.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysCmpId.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNHSysCmpId.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNHSysCmpId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, False, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject2, "", "11", Nothing, True)})
        Me.FNHSysCmpId.Properties.ReadOnly = True
        Me.FNHSysCmpId.Properties.Tag = ""
        Me.FNHSysCmpId.Size = New System.Drawing.Size(131, 20)
        Me.FNHSysCmpId.TabIndex = 510
        Me.FNHSysCmpId.Tag = ""
        '
        'FNHSysSuplId_None
        '
        Me.FNHSysSuplId_None.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FNHSysSuplId_None.EnterMoveNextControl = True
        Me.FNHSysSuplId_None.Location = New System.Drawing.Point(289, 90)
        Me.FNHSysSuplId_None.Name = "FNHSysSuplId_None"
        Me.FNHSysSuplId_None.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FNHSysSuplId_None.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FNHSysSuplId_None.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysSuplId_None.Properties.Appearance.Options.UseForeColor = True
        Me.FNHSysSuplId_None.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysSuplId_None.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysSuplId_None.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FNHSysSuplId_None.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FNHSysSuplId_None.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNHSysSuplId_None.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysSuplId_None.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNHSysSuplId_None.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNHSysSuplId_None.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysSuplId_None.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysSuplId_None.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNHSysSuplId_None.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNHSysSuplId_None.Properties.ReadOnly = True
        Me.FNHSysSuplId_None.Size = New System.Drawing.Size(713, 20)
        Me.FNHSysSuplId_None.TabIndex = 416
        Me.FNHSysSuplId_None.TabStop = False
        Me.FNHSysSuplId_None.Tag = "2|"
        '
        'FTStartSendSupl
        '
        Me.FTStartSendSupl.EditValue = Nothing
        Me.FTStartSendSupl.EnterMoveNextControl = True
        Me.FTStartSendSupl.Location = New System.Drawing.Point(156, 69)
        Me.FTStartSendSupl.Name = "FTStartSendSupl"
        Me.FTStartSendSupl.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTStartSendSupl.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FTStartSendSupl.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.FTStartSendSupl.Properties.DisplayFormat.FormatString = "d"
        Me.FTStartSendSupl.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTStartSendSupl.Properties.EditFormat.FormatString = "d"
        Me.FTStartSendSupl.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTStartSendSupl.Properties.NullDate = ""
        Me.FTStartSendSupl.Size = New System.Drawing.Size(131, 20)
        Me.FTStartSendSupl.TabIndex = 400
        Me.FTStartSendSupl.Tag = "2|"
        '
        'FTStartSendSupl_lbl
        '
        Me.FTStartSendSupl_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTStartSendSupl_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTStartSendSupl_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTStartSendSupl_lbl.Location = New System.Drawing.Point(9, 68)
        Me.FTStartSendSupl_lbl.Name = "FTStartSendSupl_lbl"
        Me.FTStartSendSupl_lbl.Size = New System.Drawing.Size(144, 19)
        Me.FTStartSendSupl_lbl.TabIndex = 401
        Me.FTStartSendSupl_lbl.Tag = "2|"
        Me.FTStartSendSupl_lbl.Text = "Start SendSupl :"
        '
        'FTOrderNoTo_lbl
        '
        Me.FTOrderNoTo_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTOrderNoTo_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTOrderNoTo_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTOrderNoTo_lbl.Location = New System.Drawing.Point(293, 48)
        Me.FTOrderNoTo_lbl.Name = "FTOrderNoTo_lbl"
        Me.FTOrderNoTo_lbl.Size = New System.Drawing.Size(176, 19)
        Me.FTOrderNoTo_lbl.TabIndex = 407
        Me.FTOrderNoTo_lbl.Tag = "2|"
        Me.FTOrderNoTo_lbl.Text = "To Order No :"
        '
        'FNHSysBuyId
        '
        Me.FNHSysBuyId.Location = New System.Drawing.Point(156, 26)
        Me.FNHSysBuyId.Name = "FNHSysBuyId"
        Me.FNHSysBuyId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject3, "", "113", Nothing, True)})
        Me.FNHSysBuyId.Properties.Tag = ""
        Me.FNHSysBuyId.Size = New System.Drawing.Size(131, 20)
        Me.FNHSysBuyId.TabIndex = 408
        Me.FNHSysBuyId.Tag = "2|"
        Me.FNHSysBuyId.Visible = False
        '
        'FNHSysStyleId_None
        '
        Me.FNHSysStyleId_None.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FNHSysStyleId_None.Location = New System.Drawing.Point(288, 27)
        Me.FNHSysStyleId_None.Name = "FNHSysStyleId_None"
        Me.FNHSysStyleId_None.Properties.Appearance.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysStyleId_None.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysStyleId_None.Properties.ReadOnly = True
        Me.FNHSysStyleId_None.Size = New System.Drawing.Size(714, 20)
        Me.FNHSysStyleId_None.TabIndex = 411
        Me.FNHSysStyleId_None.Tag = "2|"
        '
        'FNHSysBuyId_lbl
        '
        Me.FNHSysBuyId_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FNHSysBuyId_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysBuyId_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysBuyId_lbl.Location = New System.Drawing.Point(3, 26)
        Me.FNHSysBuyId_lbl.Name = "FNHSysBuyId_lbl"
        Me.FNHSysBuyId_lbl.Size = New System.Drawing.Size(35, 20)
        Me.FNHSysBuyId_lbl.TabIndex = 412
        Me.FNHSysBuyId_lbl.Tag = "2|"
        Me.FNHSysBuyId_lbl.Text = "Buy :"
        Me.FNHSysBuyId_lbl.Visible = False
        '
        'FTOrderNoTo
        '
        Me.FTOrderNoTo.EnterMoveNextControl = True
        Me.FTOrderNoTo.Location = New System.Drawing.Point(475, 48)
        Me.FTOrderNoTo.Name = "FTOrderNoTo"
        Me.FTOrderNoTo.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FTOrderNoTo.Properties.Appearance.Options.UseBackColor = True
        Me.FTOrderNoTo.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FTOrderNoTo.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FTOrderNoTo.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FTOrderNoTo.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FTOrderNoTo.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FTOrderNoTo.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FTOrderNoTo.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FTOrderNoTo.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FTOrderNoTo.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FTOrderNoTo.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FTOrderNoTo.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FTOrderNoTo.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FTOrderNoTo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", 15, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject4, "", "219", Nothing, True)})
        Me.FTOrderNoTo.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.FTOrderNoTo.Properties.MaxLength = 30
        Me.FTOrderNoTo.Size = New System.Drawing.Size(130, 20)
        Me.FTOrderNoTo.TabIndex = 406
        Me.FTOrderNoTo.Tag = "2|"
        '
        'FNHSysStyleId
        '
        Me.FNHSysStyleId.Location = New System.Drawing.Point(156, 27)
        Me.FNHSysStyleId.Name = "FNHSysStyleId"
        Me.FNHSysStyleId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject5, "", "89", Nothing, True)})
        Me.FNHSysStyleId.Properties.Tag = ""
        Me.FNHSysStyleId.Size = New System.Drawing.Size(131, 20)
        Me.FNHSysStyleId.TabIndex = 410
        Me.FNHSysStyleId.Tag = "2|"
        '
        'FTOrderNo_lbl
        '
        Me.FTOrderNo_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTOrderNo_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTOrderNo_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTOrderNo_lbl.Location = New System.Drawing.Point(9, 47)
        Me.FTOrderNo_lbl.Name = "FTOrderNo_lbl"
        Me.FTOrderNo_lbl.Size = New System.Drawing.Size(144, 19)
        Me.FTOrderNo_lbl.TabIndex = 405
        Me.FTOrderNo_lbl.Tag = "2|"
        Me.FTOrderNo_lbl.Text = "From Order No :"
        '
        'FNHSysStyleId_lbl2
        '
        Me.FNHSysStyleId_lbl2.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FNHSysStyleId_lbl2.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysStyleId_lbl2.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysStyleId_lbl2.Location = New System.Drawing.Point(9, 27)
        Me.FNHSysStyleId_lbl2.Name = "FNHSysStyleId_lbl2"
        Me.FNHSysStyleId_lbl2.Size = New System.Drawing.Size(144, 20)
        Me.FNHSysStyleId_lbl2.TabIndex = 413
        Me.FNHSysStyleId_lbl2.Tag = "2|"
        Me.FNHSysStyleId_lbl2.Text = "Style No :"
        '
        'FNHSysBuyId_None
        '
        Me.FNHSysBuyId_None.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FNHSysBuyId_None.Location = New System.Drawing.Point(288, 26)
        Me.FNHSysBuyId_None.Name = "FNHSysBuyId_None"
        Me.FNHSysBuyId_None.Properties.Appearance.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysBuyId_None.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysBuyId_None.Properties.ReadOnly = True
        Me.FNHSysBuyId_None.Size = New System.Drawing.Size(714, 20)
        Me.FNHSysBuyId_None.TabIndex = 409
        Me.FNHSysBuyId_None.Tag = "2|"
        Me.FNHSysBuyId_None.Visible = False
        '
        'FTOrderNo
        '
        Me.FTOrderNo.EnterMoveNextControl = True
        Me.FTOrderNo.Location = New System.Drawing.Point(156, 48)
        Me.FTOrderNo.Name = "FTOrderNo"
        Me.FTOrderNo.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FTOrderNo.Properties.Appearance.Options.UseBackColor = True
        Me.FTOrderNo.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FTOrderNo.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FTOrderNo.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FTOrderNo.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FTOrderNo.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FTOrderNo.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FTOrderNo.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FTOrderNo.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FTOrderNo.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FTOrderNo.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FTOrderNo.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FTOrderNo.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FTOrderNo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", 15, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject6, "", "218", Nothing, True)})
        Me.FTOrderNo.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.FTOrderNo.Properties.MaxLength = 30
        Me.FTOrderNo.Size = New System.Drawing.Size(131, 20)
        Me.FTOrderNo.TabIndex = 404
        Me.FTOrderNo.Tag = "2|"
        '
        'ogbmainprocbutton
        '
        Me.ogbmainprocbutton.Controls.Add(Me.ocmsave)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmReadExcel)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmexit)
        Me.ogbmainprocbutton.Location = New System.Drawing.Point(156, 6)
        Me.ogbmainprocbutton.Name = "ogbmainprocbutton"
        Me.ogbmainprocbutton.Size = New System.Drawing.Size(602, 34)
        Me.ogbmainprocbutton.TabIndex = 392
        '
        'ocmsave
        '
        Me.ocmsave.Location = New System.Drawing.Point(14, 4)
        Me.ocmsave.Name = "ocmsave"
        Me.ocmsave.Size = New System.Drawing.Size(95, 25)
        Me.ocmsave.TabIndex = 100
        Me.ocmsave.TabStop = False
        Me.ocmsave.Tag = "2|"
        Me.ocmsave.Text = "SAVE"
        '
        'ocmReadExcel
        '
        Me.ocmReadExcel.Location = New System.Drawing.Point(132, 4)
        Me.ocmReadExcel.Name = "ocmReadExcel"
        Me.ocmReadExcel.Size = New System.Drawing.Size(95, 25)
        Me.ocmReadExcel.TabIndex = 99
        Me.ocmReadExcel.TabStop = False
        Me.ocmReadExcel.Tag = "2|"
        Me.ocmReadExcel.Text = "READ EXCEL FILE"
        '
        'ocmexit
        '
        Me.ocmexit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmexit.Location = New System.Drawing.Point(443, 7)
        Me.ocmexit.Name = "ocmexit"
        Me.ocmexit.Size = New System.Drawing.Size(95, 25)
        Me.ocmexit.TabIndex = 96
        Me.ocmexit.TabStop = False
        Me.ocmexit.Tag = "2|"
        Me.ocmexit.Text = "EXIT"
        '
        'otabDetail
        '
        Me.otabDetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.otabDetail.Location = New System.Drawing.Point(0, 0)
        Me.otabDetail.Name = "otabDetail"
        Me.otabDetail.SelectedTabPage = Me.otabpageEm
        Me.otabDetail.Size = New System.Drawing.Size(1090, 572)
        Me.otabDetail.TabIndex = 398
        Me.otabDetail.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.otabpageEm, Me.otbdynmic})
        '
        'otabpageEm
        '
        Me.otabpageEm.Controls.Add(Me.ogcdetail)
        Me.otabpageEm.Name = "otabpageEm"
        Me.otabpageEm.PageVisible = False
        Me.otabpageEm.Size = New System.Drawing.Size(1084, 544)
        Me.otabpageEm.Text = "Embroidery"
        '
        'ogcdetail
        '
        Me.ogcdetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogcdetail.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.ogcdetail.Location = New System.Drawing.Point(0, 0)
        Me.ogcdetail.MainView = Me.ogvdetail
        Me.ogcdetail.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.ogcdetail.Name = "ogcdetail"
        Me.ogcdetail.Size = New System.Drawing.Size(1084, 544)
        Me.ogcdetail.TabIndex = 397
        Me.ogcdetail.TabStop = False
        Me.ogcdetail.Tag = "2|"
        Me.ogcdetail.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvdetail})
        '
        'otbdynmic
        '
        Me.otbdynmic.Controls.Add(Me.otbdynamicdetails)
        Me.otbdynmic.Name = "otbdynmic"
        Me.otbdynmic.Size = New System.Drawing.Size(1084, 544)
        Me.otbdynmic.Text = "Detail"
        '
        'otbdynamicdetails
        '
        Me.otbdynamicdetails.Dock = System.Windows.Forms.DockStyle.Fill
        Me.otbdynamicdetails.Location = New System.Drawing.Point(0, 0)
        Me.otbdynamicdetails.Name = "otbdynamicdetails"
        Me.otbdynamicdetails.Size = New System.Drawing.Size(1084, 544)
        Me.otbdynamicdetails.TabIndex = 0
        '
        'GridBand1
        '
        Me.GridBand1.Caption = "GridBand1"
        Me.GridBand1.Name = "GridBand1"
        Me.GridBand1.VisibleIndex = -1
        '
        'GridBand2
        '
        Me.GridBand2.Caption = "GridBand2"
        Me.GridBand2.Name = "GridBand2"
        Me.GridBand2.VisibleIndex = -1
        '
        'GridBand3
        '
        Me.GridBand3.Caption = "GridBand3"
        Me.GridBand3.Name = "GridBand3"
        Me.GridBand3.VisibleIndex = -1
        '
        'GridBand4
        '
        Me.GridBand4.Caption = "GridBand4"
        Me.GridBand4.Name = "GridBand4"
        Me.GridBand4.VisibleIndex = -1
        '
        'GridBand5
        '
        Me.GridBand5.Caption = "GridBand5"
        Me.GridBand5.Name = "GridBand5"
        Me.GridBand5.VisibleIndex = -1
        '
        'hideContainerTop
        '
        Me.hideContainerTop.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.hideContainerTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.hideContainerTop.Location = New System.Drawing.Point(0, 0)
        Me.hideContainerTop.Name = "hideContainerTop"
        Me.hideContainerTop.Size = New System.Drawing.Size(1090, 38)
        '
        'ogvdetail
        '
        Me.ogvdetail.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.ogvGDetailgbFDSendSuplDate, Me.ogvGDetailgbFNBunbleSeq, Me.ogvGDetailgbFNFacDefectQty, Me.ogvGDetailgbFNQuantity, Me.ogvGDetailgbFNTableNo, Me.ogvGDetailgbFTCheckBy, Me.ogvGDetailgbFTColorway, Me.ogvGDetailgbFTOrderNo, Me.ogvGDetailgbFTPORef, Me.ogvGDetailgbFTPartName, Me.ogvGDetailgbFTSendSuplNo, Me.ogvGDetailgbFTSizeBreakDown, Me.ogvGDetailgbFTStyleCode, Me.ogvGDetailgbFTSuplCode})
        Me.ogvdetail.GridControl = Me.ogcdetail
        Me.ogvdetail.Name = "ogvdetail"
        Me.ogvdetail.OptionsCustomization.AllowQuickHideColumns = False
        Me.ogvdetail.OptionsPrint.PrintHeader = False
        Me.ogvdetail.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.ogvdetail.OptionsView.ShowGroupPanel = False
        '
        'ogvGDetailgbFDSendSuplDate
        '
        Me.ogvGDetailgbFDSendSuplDate.Caption = "BandedGridColumn1"
        Me.ogvGDetailgbFDSendSuplDate.FieldName = "FDSendSuplDate"
        Me.ogvGDetailgbFDSendSuplDate.Name = "ogvGDetailgbFDSendSuplDate"
        Me.ogvGDetailgbFDSendSuplDate.Visible = True
        Me.ogvGDetailgbFDSendSuplDate.VisibleIndex = 0
        '
        'ogvGDetailgbFNBunbleSeq
        '
        Me.ogvGDetailgbFNBunbleSeq.Caption = "BandedGridColumn2"
        Me.ogvGDetailgbFNBunbleSeq.FieldName = "FNBunbleSeq"
        Me.ogvGDetailgbFNBunbleSeq.Name = "ogvGDetailgbFNBunbleSeq"
        Me.ogvGDetailgbFNBunbleSeq.Visible = True
        Me.ogvGDetailgbFNBunbleSeq.VisibleIndex = 1
        '
        'ogvGDetailgbFNFacDefectQty
        '
        Me.ogvGDetailgbFNFacDefectQty.Caption = "BandedGridColumn3"
        Me.ogvGDetailgbFNFacDefectQty.FieldName = "FNFacDefectQty"
        Me.ogvGDetailgbFNFacDefectQty.Name = "ogvGDetailgbFNFacDefectQty"
        Me.ogvGDetailgbFNFacDefectQty.Visible = True
        Me.ogvGDetailgbFNFacDefectQty.VisibleIndex = 2
        '
        'ogvGDetailgbFNQuantity
        '
        Me.ogvGDetailgbFNQuantity.Caption = "BandedGridColumn4"
        Me.ogvGDetailgbFNQuantity.FieldName = "FNQuantity"
        Me.ogvGDetailgbFNQuantity.Name = "ogvGDetailgbFNQuantity"
        Me.ogvGDetailgbFNQuantity.Visible = True
        Me.ogvGDetailgbFNQuantity.VisibleIndex = 3
        '
        'ogvGDetailgbFNTableNo
        '
        Me.ogvGDetailgbFNTableNo.Caption = "BandedGridColumn5"
        Me.ogvGDetailgbFNTableNo.FieldName = "FNTableNo"
        Me.ogvGDetailgbFNTableNo.Name = "ogvGDetailgbFNTableNo"
        Me.ogvGDetailgbFNTableNo.Visible = True
        Me.ogvGDetailgbFNTableNo.VisibleIndex = 4
        '
        'ogvGDetailgbFTCheckBy
        '
        Me.ogvGDetailgbFTCheckBy.Caption = "BandedGridColumn6"
        Me.ogvGDetailgbFTCheckBy.FieldName = "FTCheckBy"
        Me.ogvGDetailgbFTCheckBy.Name = "ogvGDetailgbFTCheckBy"
        Me.ogvGDetailgbFTCheckBy.Visible = True
        Me.ogvGDetailgbFTCheckBy.VisibleIndex = 5
        '
        'ogvGDetailgbFTColorway
        '
        Me.ogvGDetailgbFTColorway.Caption = "BandedGridColumn7"
        Me.ogvGDetailgbFTColorway.FieldName = "FTColorway"
        Me.ogvGDetailgbFTColorway.Name = "ogvGDetailgbFTColorway"
        Me.ogvGDetailgbFTColorway.Visible = True
        Me.ogvGDetailgbFTColorway.VisibleIndex = 6
        '
        'ogvGDetailgbFTOrderNo
        '
        Me.ogvGDetailgbFTOrderNo.Caption = "BandedGridColumn8"
        Me.ogvGDetailgbFTOrderNo.FieldName = "FTOrderNo"
        Me.ogvGDetailgbFTOrderNo.Name = "ogvGDetailgbFTOrderNo"
        Me.ogvGDetailgbFTOrderNo.Visible = True
        Me.ogvGDetailgbFTOrderNo.VisibleIndex = 7
        '
        'ogvGDetailgbFTPORef
        '
        Me.ogvGDetailgbFTPORef.Caption = "BandedGridColumn1"
        Me.ogvGDetailgbFTPORef.FieldName = "FTPORef"
        Me.ogvGDetailgbFTPORef.Name = "ogvGDetailgbFTPORef"
        Me.ogvGDetailgbFTPORef.Visible = True
        Me.ogvGDetailgbFTPORef.VisibleIndex = 8
        '
        'ogvGDetailgbFTPartName
        '
        Me.ogvGDetailgbFTPartName.Caption = "BandedGridColumn9"
        Me.ogvGDetailgbFTPartName.FieldName = "FTPartName"
        Me.ogvGDetailgbFTPartName.Name = "ogvGDetailgbFTPartName"
        Me.ogvGDetailgbFTPartName.Visible = True
        Me.ogvGDetailgbFTPartName.VisibleIndex = 9
        '
        'ogvGDetailgbFTSendSuplNo
        '
        Me.ogvGDetailgbFTSendSuplNo.Caption = "BandedGridColumn1"
        Me.ogvGDetailgbFTSendSuplNo.FieldName = "FTSendSuplNo"
        Me.ogvGDetailgbFTSendSuplNo.Name = "ogvGDetailgbFTSendSuplNo"
        Me.ogvGDetailgbFTSendSuplNo.Visible = True
        Me.ogvGDetailgbFTSendSuplNo.VisibleIndex = 10
        '
        'ogvGDetailgbFTSizeBreakDown
        '
        Me.ogvGDetailgbFTSizeBreakDown.Caption = "BandedGridColumn1"
        Me.ogvGDetailgbFTSizeBreakDown.FieldName = "FTSizeBreakDown"
        Me.ogvGDetailgbFTSizeBreakDown.Name = "ogvGDetailgbFTSizeBreakDown"
        Me.ogvGDetailgbFTSizeBreakDown.Visible = True
        Me.ogvGDetailgbFTSizeBreakDown.VisibleIndex = 11
        '
        'ogvGDetailgbFTStyleCode
        '
        Me.ogvGDetailgbFTStyleCode.Caption = "BandedGridColumn1"
        Me.ogvGDetailgbFTStyleCode.FieldName = "FTStyleCode"
        Me.ogvGDetailgbFTStyleCode.Name = "ogvGDetailgbFTStyleCode"
        Me.ogvGDetailgbFTStyleCode.Visible = True
        Me.ogvGDetailgbFTStyleCode.VisibleIndex = 12
        '
        'ogvGDetailgbFTSuplCode
        '
        Me.ogvGDetailgbFTSuplCode.Caption = "BandedGridColumn2"
        Me.ogvGDetailgbFTSuplCode.FieldName = "FTSuplCode"
        Me.ogvGDetailgbFTSuplCode.Name = "ogvGDetailgbFTSuplCode"
        Me.ogvGDetailgbFTSuplCode.Visible = True
        Me.ogvGDetailgbFTSuplCode.VisibleIndex = 13
        '
        'wQCSendSuplImportExcel
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1090, 572)
        Me.Controls.Add(Me.ogbmainprocbutton)
        Me.Controls.Add(Me.otabDetail)
        Me.Name = "wQCSendSuplImportExcel"
        Me.Text = "wQCSendSuplImportExcel"
        CType(Me.DockManager, System.ComponentModel.ISupportInitialize).EndInit()
        Me.oCriteria.ResumeLayout(False)
        Me.DockPanel1_Container.ResumeLayout(False)
        CType(Me.FNHSysSuplId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTEndSendSupl.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTEndSendSupl.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysCmpId_None.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysCmpId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysSuplId_None.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTStartSendSupl.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTStartSendSupl.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysBuyId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysStyleId_None.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTOrderNoTo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysStyleId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysBuyId_None.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTOrderNo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbmainprocbutton.ResumeLayout(False)
        CType(Me.otabDetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.otabDetail.ResumeLayout(False)
        Me.otabpageEm.ResumeLayout(False)
        CType(Me.ogcdetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.otbdynmic.ResumeLayout(False)
        CType(Me.otbdynamicdetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvdetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents DockManager As DevExpress.XtraBars.Docking.DockManager
    Friend WithEvents oCriteria As DevExpress.XtraBars.Docking.DockPanel
    Friend WithEvents DockPanel1_Container As DevExpress.XtraBars.Docking.ControlContainer
    Friend WithEvents FNHSysCmpId_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysCmpId_None As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FNHSysCmpId As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents ogbmainprocbutton As DevExpress.XtraEditors.PanelControl
    Friend WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents otabDetail As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents otabpageEm As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents GridBand1 As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents GridBand2 As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents GridBand3 As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents GridBand4 As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents GridBand5 As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents ogcdetail As DevExpress.XtraGrid.GridControl
    Friend WithEvents FNHSysSuplId As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FTEndSendSupl As DevExpress.XtraEditors.DateEdit
    Friend WithEvents FNHSysSuplId_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTEndSendSupl_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysSuplId_None As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FTStartSendSupl As DevExpress.XtraEditors.DateEdit
    Friend WithEvents FTStartSendSupl_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTOrderNoTo_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysBuyId As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FNHSysStyleId_None As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FNHSysBuyId_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTOrderNoTo As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FNHSysStyleId As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FTOrderNo_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysStyleId_lbl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysBuyId_None As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FTOrderNo As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents otbdynmic As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents otbdynamicdetails As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents hideContainerTop As DevExpress.XtraBars.Docking.AutoHideContainer
    Friend WithEvents ocmReadExcel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmsave As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ogvdetail As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents ogvGDetailgbFDSendSuplDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ogvGDetailgbFNBunbleSeq As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ogvGDetailgbFNFacDefectQty As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ogvGDetailgbFNQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ogvGDetailgbFNTableNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ogvGDetailgbFTCheckBy As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ogvGDetailgbFTColorway As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ogvGDetailgbFTOrderNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ogvGDetailgbFTPORef As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ogvGDetailgbFTPartName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ogvGDetailgbFTSendSuplNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ogvGDetailgbFTSizeBreakDown As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ogvGDetailgbFTStyleCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ogvGDetailgbFTSuplCode As DevExpress.XtraGrid.Columns.GridColumn
End Class

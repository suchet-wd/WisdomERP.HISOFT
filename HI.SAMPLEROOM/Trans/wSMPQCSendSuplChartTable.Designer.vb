<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wSMPQCSendSuplChartTable
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(wSMPQCSendSuplChartTable))
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject4 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.DockManager = New DevExpress.XtraBars.Docking.DockManager()
        Me.oCriteria = New DevExpress.XtraBars.Docking.DockPanel()
        Me.DockPanel1_Container = New DevExpress.XtraBars.Docking.ControlContainer()
        Me.FTOrderNoTo_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTOrderNoTo = New DevExpress.XtraEditors.ButtonEdit()
        Me.FTOrderNo_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTEndSendSupl = New DevExpress.XtraEditors.DateEdit()
        Me.FTOrderNo = New DevExpress.XtraEditors.ButtonEdit()
        Me.FNHSysCmpId_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysSuplId_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysCmpId_None = New DevExpress.XtraEditors.TextEdit()
        Me.FTEndSendSupl_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysCmpId = New DevExpress.XtraEditors.ButtonEdit()
        Me.FNHSysSuplId_None = New DevExpress.XtraEditors.TextEdit()
        Me.FTStartSendSupl = New DevExpress.XtraEditors.DateEdit()
        Me.FTStartSendSupl_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.ogbmainprocbutton = New DevExpress.XtraEditors.PanelControl()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmload = New DevExpress.XtraEditors.SimpleButton()
        Me.otabDetail = New DevExpress.XtraTab.XtraTabControl()
        Me.XtraTabPage1 = New DevExpress.XtraTab.XtraTabPage()
        Me.FNSendSuplDefectQty_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNSendSuplQty_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.XtraTabPage2 = New DevExpress.XtraTab.XtraTabPage()
        Me.GridControl1 = New DevExpress.XtraGrid.GridControl()
        Me.ogvdetail = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.ogvGFTName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ogvGFNQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ogvGFNQuantityPercent = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNHSysSuplId = New DevExpress.XtraEditors.ButtonEdit()
        CType(Me.DockManager, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.oCriteria.SuspendLayout()
        Me.DockPanel1_Container.SuspendLayout()
        CType(Me.FTOrderNoTo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTEndSendSupl.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTEndSendSupl.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTOrderNo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysCmpId_None.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysCmpId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysSuplId_None.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTStartSendSupl.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTStartSendSupl.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbmainprocbutton.SuspendLayout()
        CType(Me.otabDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.otabDetail.SuspendLayout()
        Me.XtraTabPage1.SuspendLayout()
        Me.XtraTabPage2.SuspendLayout()
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvdetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysSuplId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DockManager
        '
        Me.DockManager.Form = Me
        Me.DockManager.RootPanels.AddRange(New DevExpress.XtraBars.Docking.DockPanel() {Me.oCriteria})
        Me.DockManager.TopZIndexControls.AddRange(New String() {"DevExpress.XtraBars.BarDockControl", "DevExpress.XtraBars.StandaloneBarDockControl", "System.Windows.Forms.StatusBar", "System.Windows.Forms.MenuStrip", "System.Windows.Forms.StatusStrip", "DevExpress.XtraBars.Ribbon.RibbonStatusBar", "DevExpress.XtraBars.Ribbon.RibbonControl"})
        '
        'oCriteria
        '
        Me.oCriteria.Controls.Add(Me.DockPanel1_Container)
        Me.oCriteria.Dock = DevExpress.XtraBars.Docking.DockingStyle.Top
        Me.oCriteria.ID = New System.Guid("91ad7047-0443-4c14-af97-ecf3f818a924")
        Me.oCriteria.Image = CType(resources.GetObject("oCriteria.Image"), System.Drawing.Image)
        Me.oCriteria.Location = New System.Drawing.Point(0, 0)
        Me.oCriteria.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.oCriteria.Name = "oCriteria"
        Me.oCriteria.Options.AllowDockBottom = False
        Me.oCriteria.Options.AllowDockFill = False
        Me.oCriteria.Options.AllowDockLeft = False
        Me.oCriteria.Options.AllowDockRight = False
        Me.oCriteria.Options.ShowCloseButton = False
        Me.oCriteria.OriginalSize = New System.Drawing.Size(200, 160)
        Me.oCriteria.Size = New System.Drawing.Size(1286, 160)
        Me.oCriteria.Text = "Criteria"
        '
        'DockPanel1_Container
        '
        Me.DockPanel1_Container.Controls.Add(Me.FNHSysSuplId)
        Me.DockPanel1_Container.Controls.Add(Me.FTOrderNoTo_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FTOrderNoTo)
        Me.DockPanel1_Container.Controls.Add(Me.FTOrderNo_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FTEndSendSupl)
        Me.DockPanel1_Container.Controls.Add(Me.FTOrderNo)
        Me.DockPanel1_Container.Controls.Add(Me.FNHSysCmpId_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FNHSysSuplId_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FNHSysCmpId_None)
        Me.DockPanel1_Container.Controls.Add(Me.FTEndSendSupl_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FNHSysCmpId)
        Me.DockPanel1_Container.Controls.Add(Me.FNHSysSuplId_None)
        Me.DockPanel1_Container.Controls.Add(Me.FTStartSendSupl)
        Me.DockPanel1_Container.Controls.Add(Me.FTStartSendSupl_lbl)
        Me.DockPanel1_Container.Location = New System.Drawing.Point(5, 27)
        Me.DockPanel1_Container.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.DockPanel1_Container.Name = "DockPanel1_Container"
        Me.DockPanel1_Container.Size = New System.Drawing.Size(1276, 126)
        Me.DockPanel1_Container.TabIndex = 0
        '
        'FTOrderNoTo_lbl
        '
        Me.FTOrderNoTo_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTOrderNoTo_lbl.Appearance.Options.UseForeColor = True
        Me.FTOrderNoTo_lbl.Appearance.Options.UseTextOptions = True
        Me.FTOrderNoTo_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTOrderNoTo_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTOrderNoTo_lbl.Location = New System.Drawing.Point(444, 66)
        Me.FTOrderNoTo_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTOrderNoTo_lbl.Name = "FTOrderNoTo_lbl"
        Me.FTOrderNoTo_lbl.Size = New System.Drawing.Size(118, 23)
        Me.FTOrderNoTo_lbl.TabIndex = 398
        Me.FTOrderNoTo_lbl.Tag = "2|"
        Me.FTOrderNoTo_lbl.Text = "To Order No :"
        '
        'FTOrderNoTo
        '
        Me.FTOrderNoTo.EnterMoveNextControl = True
        Me.FTOrderNoTo.Location = New System.Drawing.Point(567, 66)
        Me.FTOrderNoTo.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
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
        Me.FTOrderNoTo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", 15, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject2, "", "219", Nothing, True)})
        Me.FTOrderNoTo.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.FTOrderNoTo.Properties.MaxLength = 30
        Me.FTOrderNoTo.Size = New System.Drawing.Size(152, 22)
        Me.FTOrderNoTo.TabIndex = 396
        Me.FTOrderNoTo.Tag = "2|"
        '
        'FTOrderNo_lbl
        '
        Me.FTOrderNo_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTOrderNo_lbl.Appearance.Options.UseForeColor = True
        Me.FTOrderNo_lbl.Appearance.Options.UseTextOptions = True
        Me.FTOrderNo_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTOrderNo_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTOrderNo_lbl.Location = New System.Drawing.Point(61, 65)
        Me.FTOrderNo_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTOrderNo_lbl.Name = "FTOrderNo_lbl"
        Me.FTOrderNo_lbl.Size = New System.Drawing.Size(133, 23)
        Me.FTOrderNo_lbl.TabIndex = 397
        Me.FTOrderNo_lbl.Tag = "2|"
        Me.FTOrderNo_lbl.Text = "From Order No :"
        '
        'FTEndSendSupl
        '
        Me.FTEndSendSupl.EditValue = Nothing
        Me.FTEndSendSupl.EnterMoveNextControl = True
        Me.FTEndSendSupl.Location = New System.Drawing.Point(567, 37)
        Me.FTEndSendSupl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTEndSendSupl.Name = "FTEndSendSupl"
        Me.FTEndSendSupl.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTEndSendSupl.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FTEndSendSupl.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.FTEndSendSupl.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTEndSendSupl.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTEndSendSupl.Properties.NullDate = ""
        Me.FTEndSendSupl.Size = New System.Drawing.Size(152, 22)
        Me.FTEndSendSupl.TabIndex = 515
        Me.FTEndSendSupl.Tag = "2|"
        '
        'FTOrderNo
        '
        Me.FTOrderNo.EnterMoveNextControl = True
        Me.FTOrderNo.Location = New System.Drawing.Point(196, 66)
        Me.FTOrderNo.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
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
        Me.FTOrderNo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", 15, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject3, "", "218", Nothing, True)})
        Me.FTOrderNo.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.FTOrderNo.Properties.MaxLength = 30
        Me.FTOrderNo.Size = New System.Drawing.Size(152, 22)
        Me.FTOrderNo.TabIndex = 395
        Me.FTOrderNo.Tag = "2|"
        '
        'FNHSysCmpId_lbl
        '
        Me.FNHSysCmpId_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysCmpId_lbl.Appearance.Options.UseForeColor = True
        Me.FNHSysCmpId_lbl.Appearance.Options.UseTextOptions = True
        Me.FNHSysCmpId_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysCmpId_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysCmpId_lbl.Location = New System.Drawing.Point(22, 7)
        Me.FNHSysCmpId_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysCmpId_lbl.Name = "FNHSysCmpId_lbl"
        Me.FNHSysCmpId_lbl.Size = New System.Drawing.Size(169, 21)
        Me.FNHSysCmpId_lbl.TabIndex = 530
        Me.FNHSysCmpId_lbl.Tag = "2|"
        Me.FNHSysCmpId_lbl.Text = "Company :"
        '
        'FNHSysSuplId_lbl
        '
        Me.FNHSysSuplId_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FNHSysSuplId_lbl.Appearance.Options.UseForeColor = True
        Me.FNHSysSuplId_lbl.Appearance.Options.UseTextOptions = True
        Me.FNHSysSuplId_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysSuplId_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysSuplId_lbl.Location = New System.Drawing.Point(13, 96)
        Me.FNHSysSuplId_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysSuplId_lbl.Name = "FNHSysSuplId_lbl"
        Me.FNHSysSuplId_lbl.Size = New System.Drawing.Size(178, 23)
        Me.FNHSysSuplId_lbl.TabIndex = 527
        Me.FNHSysSuplId_lbl.Tag = "2|"
        Me.FNHSysSuplId_lbl.Text = "Supplier :"
        '
        'FNHSysCmpId_None
        '
        Me.FNHSysCmpId_None.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FNHSysCmpId_None.Location = New System.Drawing.Point(349, 7)
        Me.FNHSysCmpId_None.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysCmpId_None.Name = "FNHSysCmpId_None"
        Me.FNHSysCmpId_None.Properties.Appearance.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysCmpId_None.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysCmpId_None.Properties.ReadOnly = True
        Me.FNHSysCmpId_None.Size = New System.Drawing.Size(918, 22)
        Me.FNHSysCmpId_None.TabIndex = 531
        Me.FNHSysCmpId_None.Tag = ""
        '
        'FTEndSendSupl_lbl
        '
        Me.FTEndSendSupl_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTEndSendSupl_lbl.Appearance.Options.UseForeColor = True
        Me.FTEndSendSupl_lbl.Appearance.Options.UseTextOptions = True
        Me.FTEndSendSupl_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTEndSendSupl_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTEndSendSupl_lbl.Location = New System.Drawing.Point(350, 36)
        Me.FTEndSendSupl_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTEndSendSupl_lbl.Name = "FTEndSendSupl_lbl"
        Me.FTEndSendSupl_lbl.Size = New System.Drawing.Size(211, 23)
        Me.FTEndSendSupl_lbl.TabIndex = 516
        Me.FTEndSendSupl_lbl.Tag = "2|"
        Me.FTEndSendSupl_lbl.Text = "End SendSupl :"
        '
        'FNHSysCmpId
        '
        Me.FNHSysCmpId.Location = New System.Drawing.Point(195, 7)
        Me.FNHSysCmpId.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysCmpId.Name = "FNHSysCmpId"
        Me.FNHSysCmpId.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysCmpId.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysCmpId.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNHSysCmpId.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNHSysCmpId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, False, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject4, "", "11", Nothing, True)})
        Me.FNHSysCmpId.Properties.ReadOnly = True
        Me.FNHSysCmpId.Properties.Tag = ""
        Me.FNHSysCmpId.Size = New System.Drawing.Size(153, 22)
        Me.FNHSysCmpId.TabIndex = 529
        Me.FNHSysCmpId.Tag = ""
        '
        'FNHSysSuplId_None
        '
        Me.FNHSysSuplId_None.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FNHSysSuplId_None.EnterMoveNextControl = True
        Me.FNHSysSuplId_None.Location = New System.Drawing.Point(350, 96)
        Me.FNHSysSuplId_None.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
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
        Me.FNHSysSuplId_None.Size = New System.Drawing.Size(917, 22)
        Me.FNHSysSuplId_None.TabIndex = 528
        Me.FNHSysSuplId_None.TabStop = False
        Me.FNHSysSuplId_None.Tag = "2|"
        '
        'FTStartSendSupl
        '
        Me.FTStartSendSupl.EditValue = Nothing
        Me.FTStartSendSupl.EnterMoveNextControl = True
        Me.FTStartSendSupl.Location = New System.Drawing.Point(195, 37)
        Me.FTStartSendSupl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTStartSendSupl.Name = "FTStartSendSupl"
        Me.FTStartSendSupl.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTStartSendSupl.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FTStartSendSupl.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.FTStartSendSupl.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTStartSendSupl.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTStartSendSupl.Properties.NullDate = ""
        Me.FTStartSendSupl.Size = New System.Drawing.Size(153, 22)
        Me.FTStartSendSupl.TabIndex = 513
        Me.FTStartSendSupl.Tag = "2|"
        '
        'FTStartSendSupl_lbl
        '
        Me.FTStartSendSupl_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTStartSendSupl_lbl.Appearance.Options.UseForeColor = True
        Me.FTStartSendSupl_lbl.Appearance.Options.UseTextOptions = True
        Me.FTStartSendSupl_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTStartSendSupl_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTStartSendSupl_lbl.Location = New System.Drawing.Point(23, 36)
        Me.FTStartSendSupl_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTStartSendSupl_lbl.Name = "FTStartSendSupl_lbl"
        Me.FTStartSendSupl_lbl.Size = New System.Drawing.Size(168, 23)
        Me.FTStartSendSupl_lbl.TabIndex = 514
        Me.FTStartSendSupl_lbl.Tag = "2|"
        Me.FTStartSendSupl_lbl.Text = "Start SendSupl :"
        '
        'ogbmainprocbutton
        '
        Me.ogbmainprocbutton.Controls.Add(Me.ocmexit)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmload)
        Me.ogbmainprocbutton.Location = New System.Drawing.Point(292, 6)
        Me.ogbmainprocbutton.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbmainprocbutton.Name = "ogbmainprocbutton"
        Me.ogbmainprocbutton.Size = New System.Drawing.Size(702, 42)
        Me.ogbmainprocbutton.TabIndex = 393
        '
        'ocmexit
        '
        Me.ocmexit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmexit.Location = New System.Drawing.Point(517, 9)
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
        'otabDetail
        '
        Me.otabDetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.otabDetail.Location = New System.Drawing.Point(0, 160)
        Me.otabDetail.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.otabDetail.Name = "otabDetail"
        Me.otabDetail.SelectedTabPage = Me.XtraTabPage1
        Me.otabDetail.Size = New System.Drawing.Size(1286, 596)
        Me.otabDetail.TabIndex = 394
        Me.otabDetail.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.XtraTabPage1, Me.XtraTabPage2})
        '
        'XtraTabPage1
        '
        Me.XtraTabPage1.Controls.Add(Me.FNSendSuplDefectQty_lbl)
        Me.XtraTabPage1.Controls.Add(Me.FNSendSuplQty_lbl)
        Me.XtraTabPage1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.XtraTabPage1.Name = "XtraTabPage1"
        Me.XtraTabPage1.Size = New System.Drawing.Size(1279, 562)
        Me.XtraTabPage1.Text = "XtraTabPage1"
        '
        'FNSendSuplDefectQty_lbl
        '
        Me.FNSendSuplDefectQty_lbl.Location = New System.Drawing.Point(195, 27)
        Me.FNSendSuplDefectQty_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNSendSuplDefectQty_lbl.Name = "FNSendSuplDefectQty_lbl"
        Me.FNSendSuplDefectQty_lbl.Size = New System.Drawing.Size(128, 17)
        Me.FNSendSuplDefectQty_lbl.TabIndex = 0
        Me.FNSendSuplDefectQty_lbl.Tag = "2|"
        Me.FNSendSuplDefectQty_lbl.Text = "จำนวนงานเสียทั้งหมด"
        Me.FNSendSuplDefectQty_lbl.Visible = False
        '
        'FNSendSuplQty_lbl
        '
        Me.FNSendSuplQty_lbl.Location = New System.Drawing.Point(195, 4)
        Me.FNSendSuplQty_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNSendSuplQty_lbl.Name = "FNSendSuplQty_lbl"
        Me.FNSendSuplQty_lbl.Size = New System.Drawing.Size(106, 17)
        Me.FNSendSuplQty_lbl.TabIndex = 0
        Me.FNSendSuplQty_lbl.Tag = "2|"
        Me.FNSendSuplQty_lbl.Text = "จำนวนงานทั้งหมด"
        Me.FNSendSuplQty_lbl.Visible = False
        '
        'XtraTabPage2
        '
        Me.XtraTabPage2.Controls.Add(Me.GridControl1)
        Me.XtraTabPage2.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.XtraTabPage2.Name = "XtraTabPage2"
        Me.XtraTabPage2.PageVisible = False
        Me.XtraTabPage2.Size = New System.Drawing.Size(1279, 682)
        Me.XtraTabPage2.Text = "XtraTabPage2"
        '
        'GridControl1
        '
        Me.GridControl1.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GridControl1.Location = New System.Drawing.Point(437, 91)
        Me.GridControl1.MainView = Me.ogvdetail
        Me.GridControl1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GridControl1.Name = "GridControl1"
        Me.GridControl1.Size = New System.Drawing.Size(467, 246)
        Me.GridControl1.TabIndex = 0
        Me.GridControl1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvdetail})
        '
        'ogvdetail
        '
        Me.ogvdetail.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.ogvGFTName, Me.ogvGFNQuantity, Me.ogvGFNQuantityPercent})
        Me.ogvdetail.GridControl = Me.GridControl1
        Me.ogvdetail.Name = "ogvdetail"
        '
        'ogvGFTName
        '
        Me.ogvGFTName.Caption = "FTName"
        Me.ogvGFTName.FieldName = "FTName"
        Me.ogvGFTName.Name = "ogvGFTName"
        Me.ogvGFTName.Visible = True
        Me.ogvGFTName.VisibleIndex = 0
        '
        'ogvGFNQuantity
        '
        Me.ogvGFNQuantity.Caption = "FNQuantity"
        Me.ogvGFNQuantity.FieldName = "FNQuantity"
        Me.ogvGFNQuantity.Name = "ogvGFNQuantity"
        Me.ogvGFNQuantity.Visible = True
        Me.ogvGFNQuantity.VisibleIndex = 1
        '
        'ogvGFNQuantityPercent
        '
        Me.ogvGFNQuantityPercent.Caption = "FNQuantityPercent"
        Me.ogvGFNQuantityPercent.FieldName = "FNQuantityPercent"
        Me.ogvGFNQuantityPercent.Name = "ogvGFNQuantityPercent"
        Me.ogvGFNQuantityPercent.Visible = True
        Me.ogvGFNQuantityPercent.VisibleIndex = 2
        '
        'FNHSysSuplId
        '
        Me.FNHSysSuplId.EnterMoveNextControl = True
        Me.FNHSysSuplId.Location = New System.Drawing.Point(195, 97)
        Me.FNHSysSuplId.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
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
        Me.FNHSysSuplId.Size = New System.Drawing.Size(153, 22)
        Me.FNHSysSuplId.TabIndex = 532
        Me.FNHSysSuplId.Tag = "2|"
        '
        'wSMPQCSendSuplChartTable
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1286, 756)
        Me.Controls.Add(Me.otabDetail)
        Me.Controls.Add(Me.ogbmainprocbutton)
        Me.Controls.Add(Me.oCriteria)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "wSMPQCSendSuplChartTable"
        Me.Text = "wSMPQCSendSuplChartTable"
        CType(Me.DockManager, System.ComponentModel.ISupportInitialize).EndInit()
        Me.oCriteria.ResumeLayout(False)
        Me.DockPanel1_Container.ResumeLayout(False)
        CType(Me.FTOrderNoTo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTEndSendSupl.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTEndSendSupl.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTOrderNo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysCmpId_None.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysCmpId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysSuplId_None.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTStartSendSupl.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTStartSendSupl.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbmainprocbutton.ResumeLayout(False)
        CType(Me.otabDetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.otabDetail.ResumeLayout(False)
        Me.XtraTabPage1.ResumeLayout(False)
        Me.XtraTabPage1.PerformLayout()
        Me.XtraTabPage2.ResumeLayout(False)
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvdetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysSuplId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents DockManager As DevExpress.XtraBars.Docking.DockManager
    Friend WithEvents oCriteria As DevExpress.XtraBars.Docking.DockPanel
    Friend WithEvents DockPanel1_Container As DevExpress.XtraBars.Docking.ControlContainer
    Friend WithEvents FTEndSendSupl As DevExpress.XtraEditors.DateEdit
    Friend WithEvents FNHSysCmpId_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysSuplId_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysCmpId_None As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FTEndSendSupl_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysCmpId As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FNHSysSuplId_None As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FTStartSendSupl As DevExpress.XtraEditors.DateEdit
    Friend WithEvents FTStartSendSupl_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ogbmainprocbutton As DevExpress.XtraEditors.PanelControl
    Friend WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmload As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents otabDetail As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents XtraTabPage1 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents FNSendSuplDefectQty_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNSendSuplQty_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents XtraTabPage2 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents GridControl1 As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvdetail As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents ogvGFTName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ogvGFNQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ogvGFNQuantityPercent As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTOrderNoTo_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTOrderNoTo As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FTOrderNo_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTOrderNo As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FNHSysSuplId As DevExpress.XtraEditors.ButtonEdit
End Class

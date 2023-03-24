Imports DevExpress.XtraEditors.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wQCRollFabricTracking
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(wQCRollFabricTracking))
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.ocmdoc = New DevExpress.XtraBars.Docking.DockManager()
        Me.ogbheader = New DevExpress.XtraBars.Docking.DockPanel()
        Me.DockPanel1_Container = New DevExpress.XtraBars.Docking.ControlContainer()
        Me.FNHSysSuplIdTo = New DevExpress.XtraEditors.ButtonEdit()
        Me.FNHSysSuplId = New DevExpress.XtraEditors.ButtonEdit()
        Me.ogborg = New DevExpress.XtraEditors.GroupControl()
        Me.NHSysSuplIdTo_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTEndDate = New DevExpress.XtraEditors.DateEdit()
        Me.NHSysSuplIdTo_None = New DevExpress.XtraEditors.TextEdit()
        Me.FTEndDate_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysSuplId_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTStartDate = New DevExpress.XtraEditors.DateEdit()
        Me.FNHSysSuplId_None = New DevExpress.XtraEditors.TextEdit()
        Me.FTStartDate_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.ogbmainprocbutton = New DevExpress.XtraEditors.PanelControl()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmload = New DevExpress.XtraEditors.SimpleButton()
        Me.otb = New DevExpress.XtraTab.XtraTabControl()
        Me.otpdata = New DevExpress.XtraTab.XtraTabPage()
        CType(Me.ocmdoc, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbheader.SuspendLayout()
        Me.DockPanel1_Container.SuspendLayout()
        CType(Me.FNHSysSuplIdTo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysSuplId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogborg, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTEndDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTEndDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NHSysSuplIdTo_None.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTStartDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTStartDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysSuplId_None.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbmainprocbutton.SuspendLayout()
        CType(Me.otb, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.otb.SuspendLayout()
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
        Me.ogbheader.OriginalSize = New System.Drawing.Size(200, 99)
        Me.ogbheader.Size = New System.Drawing.Size(1519, 99)
        Me.ogbheader.Text = "Criteria"
        '
        'DockPanel1_Container
        '
        Me.DockPanel1_Container.Controls.Add(Me.FNHSysSuplIdTo)
        Me.DockPanel1_Container.Controls.Add(Me.FNHSysSuplId)
        Me.DockPanel1_Container.Controls.Add(Me.ogborg)
        Me.DockPanel1_Container.Controls.Add(Me.NHSysSuplIdTo_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FTEndDate)
        Me.DockPanel1_Container.Controls.Add(Me.NHSysSuplIdTo_None)
        Me.DockPanel1_Container.Controls.Add(Me.FTEndDate_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FNHSysSuplId_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FTStartDate)
        Me.DockPanel1_Container.Controls.Add(Me.FNHSysSuplId_None)
        Me.DockPanel1_Container.Controls.Add(Me.FTStartDate_lbl)
        Me.DockPanel1_Container.Location = New System.Drawing.Point(2, 28)
        Me.DockPanel1_Container.Name = "DockPanel1_Container"
        Me.DockPanel1_Container.Size = New System.Drawing.Size(1515, 68)
        Me.DockPanel1_Container.TabIndex = 0
        '
        'FNHSysSuplIdTo
        '
        Me.FNHSysSuplIdTo.EnterMoveNextControl = True
        Me.FNHSysSuplIdTo.Location = New System.Drawing.Point(556, 27)
        Me.FNHSysSuplIdTo.Name = "FNHSysSuplIdTo"
        Me.FNHSysSuplIdTo.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FNHSysSuplIdTo.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysSuplIdTo.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysSuplIdTo.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysSuplIdTo.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FNHSysSuplIdTo.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FNHSysSuplIdTo.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNHSysSuplIdTo.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysSuplIdTo.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNHSysSuplIdTo.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNHSysSuplIdTo.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysSuplIdTo.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysSuplIdTo.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNHSysSuplIdTo.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNHSysSuplIdTo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", 15, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", "175", Nothing, True)})
        Me.FNHSysSuplIdTo.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.FNHSysSuplIdTo.Properties.MaxLength = 30
        Me.FNHSysSuplIdTo.Size = New System.Drawing.Size(100, 20)
        Me.FNHSysSuplIdTo.TabIndex = 399
        Me.FNHSysSuplIdTo.Tag = "2|"
        '
        'FNHSysSuplId
        '
        Me.FNHSysSuplId.EnterMoveNextControl = True
        Me.FNHSysSuplId.Location = New System.Drawing.Point(158, 27)
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
        Me.FNHSysSuplId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", 15, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject2, "", "99", Nothing, True)})
        Me.FNHSysSuplId.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.FNHSysSuplId.Properties.MaxLength = 30
        Me.FNHSysSuplId.Size = New System.Drawing.Size(100, 20)
        Me.FNHSysSuplId.TabIndex = 396
        Me.FNHSysSuplId.Tag = "2|"
        '
        'ogborg
        '
        Me.ogborg.Dock = System.Windows.Forms.DockStyle.Right
        Me.ogborg.Location = New System.Drawing.Point(1281, 0)
        Me.ogborg.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.ogborg.Name = "ogborg"
        Me.ogborg.Size = New System.Drawing.Size(234, 68)
        Me.ogborg.TabIndex = 481
        Me.ogborg.Text = "GroupControl1"
        Me.ogborg.Visible = False
        '
        'NHSysSuplIdTo_lbl
        '
        Me.NHSysSuplIdTo_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.NHSysSuplIdTo_lbl.Appearance.Options.UseForeColor = True
        Me.NHSysSuplIdTo_lbl.Appearance.Options.UseTextOptions = True
        Me.NHSysSuplIdTo_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.NHSysSuplIdTo_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.NHSysSuplIdTo_lbl.Location = New System.Drawing.Point(439, 27)
        Me.NHSysSuplIdTo_lbl.Name = "NHSysSuplIdTo_lbl"
        Me.NHSysSuplIdTo_lbl.Size = New System.Drawing.Size(116, 19)
        Me.NHSysSuplIdTo_lbl.TabIndex = 400
        Me.NHSysSuplIdTo_lbl.Tag = "2|"
        Me.NHSysSuplIdTo_lbl.Text = "Supplier :"
        '
        'FTEndDate
        '
        Me.FTEndDate.EditValue = Nothing
        Me.FTEndDate.EnterMoveNextControl = True
        Me.FTEndDate.Location = New System.Drawing.Point(556, 3)
        Me.FTEndDate.Name = "FTEndDate"
        Me.FTEndDate.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTEndDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FTEndDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.FTEndDate.Properties.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.FTEndDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTEndDate.Properties.EditFormat.FormatString = "dd/MM/yyyy"
        Me.FTEndDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTEndDate.Properties.Mask.EditMask = "dd/MM/yyyy"
        Me.FTEndDate.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.FTEndDate.Properties.NullDate = ""
        Me.FTEndDate.Size = New System.Drawing.Size(112, 20)
        Me.FTEndDate.TabIndex = 478
        Me.FTEndDate.Tag = "2|"
        '
        'NHSysSuplIdTo_None
        '
        Me.NHSysSuplIdTo_None.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.NHSysSuplIdTo_None.EnterMoveNextControl = True
        Me.NHSysSuplIdTo_None.Location = New System.Drawing.Point(657, 27)
        Me.NHSysSuplIdTo_None.Name = "NHSysSuplIdTo_None"
        Me.NHSysSuplIdTo_None.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.NHSysSuplIdTo_None.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.NHSysSuplIdTo_None.Properties.Appearance.Options.UseBackColor = True
        Me.NHSysSuplIdTo_None.Properties.Appearance.Options.UseForeColor = True
        Me.NHSysSuplIdTo_None.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.NHSysSuplIdTo_None.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.NHSysSuplIdTo_None.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.NHSysSuplIdTo_None.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.NHSysSuplIdTo_None.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.NHSysSuplIdTo_None.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.NHSysSuplIdTo_None.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.NHSysSuplIdTo_None.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.NHSysSuplIdTo_None.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.NHSysSuplIdTo_None.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.NHSysSuplIdTo_None.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.NHSysSuplIdTo_None.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.NHSysSuplIdTo_None.Properties.ReadOnly = True
        Me.NHSysSuplIdTo_None.Size = New System.Drawing.Size(215, 20)
        Me.NHSysSuplIdTo_None.TabIndex = 401
        Me.NHSysSuplIdTo_None.TabStop = False
        Me.NHSysSuplIdTo_None.Tag = "2|"
        '
        'FTEndDate_lbl
        '
        Me.FTEndDate_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTEndDate_lbl.Appearance.Options.UseForeColor = True
        Me.FTEndDate_lbl.Appearance.Options.UseTextOptions = True
        Me.FTEndDate_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTEndDate_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTEndDate_lbl.Location = New System.Drawing.Point(449, 2)
        Me.FTEndDate_lbl.Name = "FTEndDate_lbl"
        Me.FTEndDate_lbl.Size = New System.Drawing.Size(105, 19)
        Me.FTEndDate_lbl.TabIndex = 480
        Me.FTEndDate_lbl.Tag = "2|"
        Me.FTEndDate_lbl.Text = "ถึงวันที่ :"
        '
        'FNHSysSuplId_lbl
        '
        Me.FNHSysSuplId_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FNHSysSuplId_lbl.Appearance.Options.UseForeColor = True
        Me.FNHSysSuplId_lbl.Appearance.Options.UseTextOptions = True
        Me.FNHSysSuplId_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysSuplId_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysSuplId_lbl.Location = New System.Drawing.Point(8, 27)
        Me.FNHSysSuplId_lbl.Name = "FNHSysSuplId_lbl"
        Me.FNHSysSuplId_lbl.Size = New System.Drawing.Size(149, 19)
        Me.FNHSysSuplId_lbl.TabIndex = 397
        Me.FNHSysSuplId_lbl.Tag = "2|"
        Me.FNHSysSuplId_lbl.Text = "Supplier :"
        '
        'FTStartDate
        '
        Me.FTStartDate.EditValue = Nothing
        Me.FTStartDate.EnterMoveNextControl = True
        Me.FTStartDate.Location = New System.Drawing.Point(157, 3)
        Me.FTStartDate.Name = "FTStartDate"
        Me.FTStartDate.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTStartDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FTStartDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.FTStartDate.Properties.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.FTStartDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTStartDate.Properties.EditFormat.FormatString = "dd/MM/yyyy"
        Me.FTStartDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTStartDate.Properties.Mask.EditMask = "dd/MM/yyyy"
        Me.FTStartDate.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.FTStartDate.Properties.NullDate = ""
        Me.FTStartDate.Size = New System.Drawing.Size(112, 20)
        Me.FTStartDate.TabIndex = 477
        Me.FTStartDate.Tag = "2|"
        '
        'FNHSysSuplId_None
        '
        Me.FNHSysSuplId_None.EnterMoveNextControl = True
        Me.FNHSysSuplId_None.Location = New System.Drawing.Point(259, 27)
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
        Me.FNHSysSuplId_None.Size = New System.Drawing.Size(177, 20)
        Me.FNHSysSuplId_None.TabIndex = 398
        Me.FNHSysSuplId_None.TabStop = False
        Me.FNHSysSuplId_None.Tag = "2|"
        '
        'FTStartDate_lbl
        '
        Me.FTStartDate_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTStartDate_lbl.Appearance.Options.UseForeColor = True
        Me.FTStartDate_lbl.Appearance.Options.UseTextOptions = True
        Me.FTStartDate_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTStartDate_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTStartDate_lbl.Location = New System.Drawing.Point(49, 2)
        Me.FTStartDate_lbl.Name = "FTStartDate_lbl"
        Me.FTStartDate_lbl.Size = New System.Drawing.Size(105, 19)
        Me.FTStartDate_lbl.TabIndex = 479
        Me.FTStartDate_lbl.Tag = "2|"
        Me.FTStartDate_lbl.Text = "วันที่เริ่มต้น :"
        '
        'ogbmainprocbutton
        '
        Me.ogbmainprocbutton.Controls.Add(Me.ocmexit)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmload)
        Me.ogbmainprocbutton.Location = New System.Drawing.Point(312, 3)
        Me.ogbmainprocbutton.Name = "ogbmainprocbutton"
        Me.ogbmainprocbutton.Size = New System.Drawing.Size(663, 50)
        Me.ogbmainprocbutton.TabIndex = 391
        '
        'ocmexit
        '
        Me.ocmexit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmexit.Location = New System.Drawing.Point(505, 7)
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
        Me.otb.Location = New System.Drawing.Point(0, 99)
        Me.otb.Name = "otb"
        Me.otb.SelectedTabPage = Me.otpdata
        Me.otb.Size = New System.Drawing.Size(1519, 472)
        Me.otb.TabIndex = 394
        Me.otb.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.otpdata})
        '
        'otpdata
        '
        Me.otpdata.Name = "otpdata"
        Me.otpdata.Size = New System.Drawing.Size(1511, 442)
        Me.otpdata.Text = "Data"
        '
        'wQCRollFabricTracking
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1519, 571)
        Me.Controls.Add(Me.otb)
        Me.Controls.Add(Me.ogbmainprocbutton)
        Me.Controls.Add(Me.ogbheader)
        Me.Name = "wQCRollFabricTracking"
        Me.Text = "ตรวจสอบการ QC ผ้า"
        CType(Me.ocmdoc, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbheader.ResumeLayout(False)
        Me.DockPanel1_Container.ResumeLayout(False)
        CType(Me.FNHSysSuplIdTo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysSuplId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogborg, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTEndDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTEndDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NHSysSuplIdTo_None.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTStartDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTStartDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysSuplId_None.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbmainprocbutton.ResumeLayout(False)
        CType(Me.otb, System.ComponentModel.ISupportInitialize).EndInit()
        Me.otb.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ocmdoc As DevExpress.XtraBars.Docking.DockManager
    Friend WithEvents ogbheader As DevExpress.XtraBars.Docking.DockPanel
    Friend WithEvents DockPanel1_Container As DevExpress.XtraBars.Docking.ControlContainer
    Friend WithEvents FTEndDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents FTEndDate_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTStartDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents FTStartDate_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ogbmainprocbutton As DevExpress.XtraEditors.PanelControl
    Friend WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmload As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents otb As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents otpdata As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents ogborg As DevExpress.XtraEditors.GroupControl
    Friend WithEvents FNHSysSuplIdTo As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FNHSysSuplId As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents NHSysSuplIdTo_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents NHSysSuplIdTo_None As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FNHSysSuplId_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysSuplId_None As DevExpress.XtraEditors.TextEdit
End Class

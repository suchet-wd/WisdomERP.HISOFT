<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wQCFabricInspecRoll
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
        Dim SerializableAppearanceObject4 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject5 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.ogrpSubmenu = New DevExpress.XtraEditors.GroupControl()
        Me.TileControl = New DevExpress.XtraEditors.TileControl()
        Me.ocmclose = New DevExpress.XtraEditors.SimpleButton()
        Me.FNActQuantity_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNActQuantity = New DevExpress.XtraEditors.CalcEdit()
        Me.FNQuantity_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNQuantity = New DevExpress.XtraEditors.CalcEdit()
        Me.FTFabricFrontSize_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTFabricFrontSize = New DevExpress.XtraEditors.CalcEdit()
        Me.FTActFabricFrontSize_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTActFabricFrontSize = New DevExpress.XtraEditors.CalcEdit()
        Me.FTRollNo_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTShades = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.FTShades_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNQCFabricRollStatus_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNQCFabricRollStatus = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.FTRollNo = New DevExpress.XtraEditors.TextEdit()
        CType(Me.ogrpSubmenu, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogrpSubmenu.SuspendLayout()
        CType(Me.FNActQuantity.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNQuantity.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTFabricFrontSize.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTActFabricFrontSize.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTShades.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNQCFabricRollStatus.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTRollNo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ogrpSubmenu
        '
        Me.ogrpSubmenu.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogrpSubmenu.Controls.Add(Me.TileControl)
        Me.ogrpSubmenu.Location = New System.Drawing.Point(0, 81)
        Me.ogrpSubmenu.Name = "ogrpSubmenu"
        Me.ogrpSubmenu.ShowCaption = False
        Me.ogrpSubmenu.Size = New System.Drawing.Size(1243, 500)
        Me.ogrpSubmenu.TabIndex = 3
        Me.ogrpSubmenu.Text = "Sub Menu"
        '
        'TileControl
        '
        Me.TileControl.BackColor = System.Drawing.Color.DarkGray
        Me.TileControl.Cursor = System.Windows.Forms.Cursors.Default
        Me.TileControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TileControl.DragSize = New System.Drawing.Size(0, 0)
        Me.TileControl.ItemSize = 70
        Me.TileControl.Location = New System.Drawing.Point(2, 2)
        Me.TileControl.MaxId = 25
        Me.TileControl.Name = "TileControl"
        Me.TileControl.Orientation = System.Windows.Forms.Orientation.Vertical
        Me.TileControl.ScrollMode = DevExpress.XtraEditors.TileControlScrollMode.ScrollBar
        Me.TileControl.ShowGroupText = True
        Me.TileControl.Size = New System.Drawing.Size(1239, 496)
        Me.TileControl.TabIndex = 0
        Me.TileControl.Text = "TileControlSubmenu"
        '
        'ocmclose
        '
        Me.ocmclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmclose.Appearance.Font = New System.Drawing.Font("Tahoma", 14.0!, System.Drawing.FontStyle.Bold)
        Me.ocmclose.Appearance.Options.UseFont = True
        Me.ocmclose.Location = New System.Drawing.Point(1121, 11)
        Me.ocmclose.Name = "ocmclose"
        Me.ocmclose.Size = New System.Drawing.Size(115, 54)
        Me.ocmclose.TabIndex = 97
        Me.ocmclose.TabStop = False
        Me.ocmclose.Tag = "2|"
        Me.ocmclose.Text = "CLOSE"
        '
        'FNActQuantity_lbl
        '
        Me.FNActQuantity_lbl.Appearance.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.FNActQuantity_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNActQuantity_lbl.Appearance.Options.UseFont = True
        Me.FNActQuantity_lbl.Appearance.Options.UseForeColor = True
        Me.FNActQuantity_lbl.Appearance.Options.UseTextOptions = True
        Me.FNActQuantity_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNActQuantity_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNActQuantity_lbl.Location = New System.Drawing.Point(247, 43)
        Me.FNActQuantity_lbl.Name = "FNActQuantity_lbl"
        Me.FNActQuantity_lbl.Size = New System.Drawing.Size(120, 18)
        Me.FNActQuantity_lbl.TabIndex = 460
        Me.FNActQuantity_lbl.Tag = "2|"
        Me.FNActQuantity_lbl.Text = "Actual  :"
        '
        'FNActQuantity
        '
        Me.FNActQuantity.Location = New System.Drawing.Point(369, 40)
        Me.FNActQuantity.Name = "FNActQuantity"
        Me.FNActQuantity.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!)
        Me.FNActQuantity.Properties.Appearance.Options.UseFont = True
        Me.FNActQuantity.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNActQuantity.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNActQuantity.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNActQuantity.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNActQuantity.Properties.AutoHeight = False
        Me.FNActQuantity.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, True, False, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject4, "", Nothing, Nothing, True)})
        Me.FNActQuantity.Properties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Style3D
        Me.FNActQuantity.Properties.PopupResizeMode = DevExpress.XtraEditors.Controls.ResizeMode.LiveResize
        Me.FNActQuantity.Properties.Precision = 0
        Me.FNActQuantity.Size = New System.Drawing.Size(105, 31)
        Me.FNActQuantity.TabIndex = 459
        '
        'FNQuantity_lbl
        '
        Me.FNQuantity_lbl.Appearance.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.FNQuantity_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNQuantity_lbl.Appearance.Options.UseFont = True
        Me.FNQuantity_lbl.Appearance.Options.UseForeColor = True
        Me.FNQuantity_lbl.Appearance.Options.UseTextOptions = True
        Me.FNQuantity_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNQuantity_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNQuantity_lbl.Location = New System.Drawing.Point(14, 43)
        Me.FNQuantity_lbl.Name = "FNQuantity_lbl"
        Me.FNQuantity_lbl.Size = New System.Drawing.Size(120, 18)
        Me.FNQuantity_lbl.TabIndex = 462
        Me.FNQuantity_lbl.Tag = "2|"
        Me.FNQuantity_lbl.Text = "Ticketed  :"
        '
        'FNQuantity
        '
        Me.FNQuantity.Location = New System.Drawing.Point(136, 40)
        Me.FNQuantity.Name = "FNQuantity"
        Me.FNQuantity.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!)
        Me.FNQuantity.Properties.Appearance.Options.UseFont = True
        Me.FNQuantity.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNQuantity.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNQuantity.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNQuantity.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNQuantity.Properties.AutoHeight = False
        Me.FNQuantity.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, True, False, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject5, "", Nothing, Nothing, True)})
        Me.FNQuantity.Properties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Style3D
        Me.FNQuantity.Properties.PopupResizeMode = DevExpress.XtraEditors.Controls.ResizeMode.LiveResize
        Me.FNQuantity.Properties.Precision = 0
        Me.FNQuantity.Properties.ReadOnly = True
        Me.FNQuantity.Size = New System.Drawing.Size(105, 31)
        Me.FNQuantity.TabIndex = 461
        '
        'FTFabricFrontSize_lbl
        '
        Me.FTFabricFrontSize_lbl.Appearance.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.FTFabricFrontSize_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTFabricFrontSize_lbl.Appearance.Options.UseFont = True
        Me.FTFabricFrontSize_lbl.Appearance.Options.UseForeColor = True
        Me.FTFabricFrontSize_lbl.Appearance.Options.UseTextOptions = True
        Me.FTFabricFrontSize_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTFabricFrontSize_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTFabricFrontSize_lbl.Location = New System.Drawing.Point(487, 42)
        Me.FTFabricFrontSize_lbl.Name = "FTFabricFrontSize_lbl"
        Me.FTFabricFrontSize_lbl.Size = New System.Drawing.Size(120, 18)
        Me.FTFabricFrontSize_lbl.TabIndex = 466
        Me.FTFabricFrontSize_lbl.Tag = "2|"
        Me.FTFabricFrontSize_lbl.Text = "หน้าผ้า  :"
        '
        'FTFabricFrontSize
        '
        Me.FTFabricFrontSize.Location = New System.Drawing.Point(609, 39)
        Me.FTFabricFrontSize.Name = "FTFabricFrontSize"
        Me.FTFabricFrontSize.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!)
        Me.FTFabricFrontSize.Properties.Appearance.Options.UseFont = True
        Me.FTFabricFrontSize.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FTFabricFrontSize.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FTFabricFrontSize.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FTFabricFrontSize.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FTFabricFrontSize.Properties.AutoHeight = False
        Me.FTFabricFrontSize.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, True, False, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", Nothing, Nothing, True)})
        Me.FTFabricFrontSize.Properties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Style3D
        Me.FTFabricFrontSize.Properties.PopupResizeMode = DevExpress.XtraEditors.Controls.ResizeMode.LiveResize
        Me.FTFabricFrontSize.Properties.Precision = 0
        Me.FTFabricFrontSize.Properties.ReadOnly = True
        Me.FTFabricFrontSize.Size = New System.Drawing.Size(105, 31)
        Me.FTFabricFrontSize.TabIndex = 465
        '
        'FTActFabricFrontSize_lbl
        '
        Me.FTActFabricFrontSize_lbl.Appearance.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.FTActFabricFrontSize_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTActFabricFrontSize_lbl.Appearance.Options.UseFont = True
        Me.FTActFabricFrontSize_lbl.Appearance.Options.UseForeColor = True
        Me.FTActFabricFrontSize_lbl.Appearance.Options.UseTextOptions = True
        Me.FTActFabricFrontSize_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTActFabricFrontSize_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTActFabricFrontSize_lbl.Location = New System.Drawing.Point(720, 42)
        Me.FTActFabricFrontSize_lbl.Name = "FTActFabricFrontSize_lbl"
        Me.FTActFabricFrontSize_lbl.Size = New System.Drawing.Size(120, 18)
        Me.FTActFabricFrontSize_lbl.TabIndex = 464
        Me.FTActFabricFrontSize_lbl.Tag = "2|"
        Me.FTActFabricFrontSize_lbl.Text = "หน้าผ้าวัดจริง  :"
        '
        'FTActFabricFrontSize
        '
        Me.FTActFabricFrontSize.Location = New System.Drawing.Point(843, 39)
        Me.FTActFabricFrontSize.Name = "FTActFabricFrontSize"
        Me.FTActFabricFrontSize.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!)
        Me.FTActFabricFrontSize.Properties.Appearance.Options.UseFont = True
        Me.FTActFabricFrontSize.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FTActFabricFrontSize.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FTActFabricFrontSize.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FTActFabricFrontSize.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FTActFabricFrontSize.Properties.AutoHeight = False
        Me.FTActFabricFrontSize.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, True, False, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject2, "", Nothing, Nothing, True)})
        Me.FTActFabricFrontSize.Properties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Style3D
        Me.FTActFabricFrontSize.Properties.PopupResizeMode = DevExpress.XtraEditors.Controls.ResizeMode.LiveResize
        Me.FTActFabricFrontSize.Properties.Precision = 0
        Me.FTActFabricFrontSize.Size = New System.Drawing.Size(105, 31)
        Me.FTActFabricFrontSize.TabIndex = 463
        '
        'FTRollNo_lbl
        '
        Me.FTRollNo_lbl.Appearance.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.FTRollNo_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTRollNo_lbl.Appearance.Options.UseFont = True
        Me.FTRollNo_lbl.Appearance.Options.UseForeColor = True
        Me.FTRollNo_lbl.Appearance.Options.UseTextOptions = True
        Me.FTRollNo_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTRollNo_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTRollNo_lbl.Location = New System.Drawing.Point(14, 14)
        Me.FTRollNo_lbl.Name = "FTRollNo_lbl"
        Me.FTRollNo_lbl.Size = New System.Drawing.Size(120, 18)
        Me.FTRollNo_lbl.TabIndex = 468
        Me.FTRollNo_lbl.Tag = "2|"
        Me.FTRollNo_lbl.Text = "ROLL NO  :"
        '
        'FTShades
        '
        Me.FTShades.Location = New System.Drawing.Point(370, 10)
        Me.FTShades.Name = "FTShades"
        Me.FTShades.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FTShades.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!)
        Me.FTShades.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTShades.Properties.Appearance.Options.UseBackColor = True
        Me.FTShades.Properties.Appearance.Options.UseFont = True
        Me.FTShades.Properties.Appearance.Options.UseForeColor = True
        Me.FTShades.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FTShades.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FTShades.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FTShades.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FTShades.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FTShades.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FTShades.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FTShades.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FTShades.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FTShades.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FTShades.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FTShades.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FTShades.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FTShades.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.FTShades.Properties.MaxLength = 50
        Me.FTShades.Properties.Tag = "FNShades"
        Me.FTShades.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.FTShades.Size = New System.Drawing.Size(237, 26)
        Me.FTShades.TabIndex = 469
        Me.FTShades.TabStop = False
        Me.FTShades.Tag = "2|"
        '
        'FTShades_lbl
        '
        Me.FTShades_lbl.Appearance.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.FTShades_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTShades_lbl.Appearance.Options.UseFont = True
        Me.FTShades_lbl.Appearance.Options.UseForeColor = True
        Me.FTShades_lbl.Appearance.Options.UseTextOptions = True
        Me.FTShades_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTShades_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTShades_lbl.Location = New System.Drawing.Point(247, 15)
        Me.FTShades_lbl.Name = "FTShades_lbl"
        Me.FTShades_lbl.Size = New System.Drawing.Size(120, 18)
        Me.FTShades_lbl.TabIndex = 470
        Me.FTShades_lbl.Tag = "2|"
        Me.FTShades_lbl.Text = "Shades :"
        '
        'FNQCFabricRollStatus_lbl
        '
        Me.FNQCFabricRollStatus_lbl.Appearance.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.FNQCFabricRollStatus_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNQCFabricRollStatus_lbl.Appearance.Options.UseFont = True
        Me.FNQCFabricRollStatus_lbl.Appearance.Options.UseForeColor = True
        Me.FNQCFabricRollStatus_lbl.Appearance.Options.UseTextOptions = True
        Me.FNQCFabricRollStatus_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNQCFabricRollStatus_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNQCFabricRollStatus_lbl.Location = New System.Drawing.Point(719, 15)
        Me.FNQCFabricRollStatus_lbl.Name = "FNQCFabricRollStatus_lbl"
        Me.FNQCFabricRollStatus_lbl.Size = New System.Drawing.Size(120, 18)
        Me.FNQCFabricRollStatus_lbl.TabIndex = 472
        Me.FNQCFabricRollStatus_lbl.Tag = "2|"
        Me.FNQCFabricRollStatus_lbl.Text = "Status :"
        '
        'FNQCFabricRollStatus
        '
        Me.FNQCFabricRollStatus.Location = New System.Drawing.Point(843, 10)
        Me.FNQCFabricRollStatus.Name = "FNQCFabricRollStatus"
        Me.FNQCFabricRollStatus.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FNQCFabricRollStatus.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!)
        Me.FNQCFabricRollStatus.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FNQCFabricRollStatus.Properties.Appearance.Options.UseBackColor = True
        Me.FNQCFabricRollStatus.Properties.Appearance.Options.UseFont = True
        Me.FNQCFabricRollStatus.Properties.Appearance.Options.UseForeColor = True
        Me.FNQCFabricRollStatus.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FNQCFabricRollStatus.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FNQCFabricRollStatus.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FNQCFabricRollStatus.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FNQCFabricRollStatus.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNQCFabricRollStatus.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNQCFabricRollStatus.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNQCFabricRollStatus.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNQCFabricRollStatus.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNQCFabricRollStatus.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNQCFabricRollStatus.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNQCFabricRollStatus.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNQCFabricRollStatus.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FNQCFabricRollStatus.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.FNQCFabricRollStatus.Properties.MaxLength = 50
        Me.FNQCFabricRollStatus.Properties.Tag = "FNQCFabricRollStatus"
        Me.FNQCFabricRollStatus.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.FNQCFabricRollStatus.Size = New System.Drawing.Size(169, 26)
        Me.FNQCFabricRollStatus.TabIndex = 471
        Me.FNQCFabricRollStatus.TabStop = False
        Me.FNQCFabricRollStatus.Tag = "2|"
        '
        'FTRollNo
        '
        Me.FTRollNo.Location = New System.Drawing.Point(136, 11)
        Me.FTRollNo.Name = "FTRollNo"
        Me.FTRollNo.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!)
        Me.FTRollNo.Properties.Appearance.Options.UseFont = True
        Me.FTRollNo.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FTRollNo.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FTRollNo.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FTRollNo.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FTRollNo.Properties.AutoHeight = False
        Me.FTRollNo.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.FTRollNo.Properties.ReadOnly = True
        Me.FTRollNo.Size = New System.Drawing.Size(105, 31)
        Me.FTRollNo.TabIndex = 467
        '
        'wQCFabricInspecRoll
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1246, 584)
        Me.Controls.Add(Me.FNQCFabricRollStatus_lbl)
        Me.Controls.Add(Me.FNQCFabricRollStatus)
        Me.Controls.Add(Me.FTShades_lbl)
        Me.Controls.Add(Me.FTShades)
        Me.Controls.Add(Me.FTRollNo_lbl)
        Me.Controls.Add(Me.FTFabricFrontSize_lbl)
        Me.Controls.Add(Me.FTFabricFrontSize)
        Me.Controls.Add(Me.FTActFabricFrontSize_lbl)
        Me.Controls.Add(Me.FTActFabricFrontSize)
        Me.Controls.Add(Me.FNQuantity_lbl)
        Me.Controls.Add(Me.FNQuantity)
        Me.Controls.Add(Me.FNActQuantity_lbl)
        Me.Controls.Add(Me.FNActQuantity)
        Me.Controls.Add(Me.ocmclose)
        Me.Controls.Add(Me.ogrpSubmenu)
        Me.Controls.Add(Me.FTRollNo)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "wQCFabricInspecRoll"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "QC Fabric Detail"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.ogrpSubmenu, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogrpSubmenu.ResumeLayout(False)
        CType(Me.FNActQuantity.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNQuantity.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTFabricFrontSize.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTActFabricFrontSize.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTShades.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNQCFabricRollStatus.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTRollNo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogrpSubmenu As DevExpress.XtraEditors.GroupControl
    Friend WithEvents TileControl As DevExpress.XtraEditors.TileControl
    Friend WithEvents ocmclose As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FNActQuantity_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNActQuantity As DevExpress.XtraEditors.CalcEdit
    Friend WithEvents FNQuantity_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNQuantity As DevExpress.XtraEditors.CalcEdit
    Friend WithEvents FTFabricFrontSize_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTFabricFrontSize As DevExpress.XtraEditors.CalcEdit
    Friend WithEvents FTActFabricFrontSize_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTActFabricFrontSize As DevExpress.XtraEditors.CalcEdit
    Friend WithEvents FTRollNo_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTShades As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents FTShades_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNQCFabricRollStatus_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNQCFabricRollStatus As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents FTRollNo As DevExpress.XtraEditors.TextEdit
End Class

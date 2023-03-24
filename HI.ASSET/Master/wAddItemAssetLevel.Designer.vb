<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wAddItemAssetLevel
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
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.ocmclear = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.ogbbutton = New DevExpress.XtraEditors.PanelControl()
        Me.ocmaddnew = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmedit = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmdelete = New DevExpress.XtraEditors.SimpleButton()
        Me.FNHSysConvAssetId = New DevExpress.XtraEditors.TextEdit()
        Me.FNFixedAssetType = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.FNFixedAssetType_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNEndQty = New DevExpress.XtraEditors.CalcEdit()
        Me.FNEndQty_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTUserName_None = New DevExpress.XtraEditors.TextEdit()
        Me.FTUserName = New DevExpress.XtraEditors.ButtonEdit()
        Me.FTUserName_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNStartQty = New DevExpress.XtraEditors.CalcEdit()
        Me.FNStartQty_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysCmpId_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysCmpId_None = New DevExpress.XtraEditors.TextEdit()
        Me.FNHSysCmpId = New DevExpress.XtraEditors.ButtonEdit()
        Me.FTStateDirector = New DevExpress.XtraEditors.CheckEdit()
        Me.FTStateFactory = New DevExpress.XtraEditors.CheckEdit()
        CType(Me.ogbbutton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbbutton.SuspendLayout()
        CType(Me.FNHSysConvAssetId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNFixedAssetType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNEndQty.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTUserName_None.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTUserName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNStartQty.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysCmpId_None.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysCmpId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTStateDirector.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTStateFactory.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ocmclear
        '
        Me.ocmclear.Location = New System.Drawing.Point(300, 17)
        Me.ocmclear.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmclear.Name = "ocmclear"
        Me.ocmclear.Size = New System.Drawing.Size(87, 28)
        Me.ocmclear.TabIndex = 300
        Me.ocmclear.Text = "Clear"
        '
        'ocmexit
        '
        Me.ocmexit.Location = New System.Drawing.Point(393, 17)
        Me.ocmexit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmexit.Name = "ocmexit"
        Me.ocmexit.Size = New System.Drawing.Size(87, 28)
        Me.ocmexit.TabIndex = 301
        Me.ocmexit.Text = "Exit"
        '
        'ogbbutton
        '
        Me.ogbbutton.Controls.Add(Me.ocmaddnew)
        Me.ogbbutton.Controls.Add(Me.ocmedit)
        Me.ogbbutton.Controls.Add(Me.ocmdelete)
        Me.ogbbutton.Controls.Add(Me.ocmexit)
        Me.ogbbutton.Controls.Add(Me.ocmclear)
        Me.ogbbutton.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ogbbutton.Location = New System.Drawing.Point(0, 177)
        Me.ogbbutton.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbbutton.Name = "ogbbutton"
        Me.ogbbutton.Size = New System.Drawing.Size(777, 54)
        Me.ogbbutton.TabIndex = 302
        '
        'ocmaddnew
        '
        Me.ocmaddnew.Location = New System.Drawing.Point(21, 17)
        Me.ocmaddnew.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmaddnew.Name = "ocmaddnew"
        Me.ocmaddnew.Size = New System.Drawing.Size(87, 27)
        Me.ocmaddnew.TabIndex = 424
        Me.ocmaddnew.TabStop = False
        Me.ocmaddnew.Tag = "2|"
        Me.ocmaddnew.Text = "NEW"
        '
        'ocmedit
        '
        Me.ocmedit.Location = New System.Drawing.Point(114, 17)
        Me.ocmedit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmedit.Name = "ocmedit"
        Me.ocmedit.Size = New System.Drawing.Size(87, 28)
        Me.ocmedit.TabIndex = 306
        Me.ocmedit.Text = "ocmedit"
        '
        'ocmdelete
        '
        Me.ocmdelete.Location = New System.Drawing.Point(207, 17)
        Me.ocmdelete.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmdelete.Name = "ocmdelete"
        Me.ocmdelete.Size = New System.Drawing.Size(87, 28)
        Me.ocmdelete.TabIndex = 305
        Me.ocmdelete.TabStop = False
        Me.ocmdelete.Tag = "2|"
        Me.ocmdelete.Text = "DELETE"
        '
        'FNHSysConvAssetId
        '
        Me.FNHSysConvAssetId.Location = New System.Drawing.Point(520, 236)
        Me.FNHSysConvAssetId.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysConvAssetId.Name = "FNHSysConvAssetId"
        Me.FNHSysConvAssetId.Size = New System.Drawing.Size(117, 23)
        Me.FNHSysConvAssetId.TabIndex = 305
        Me.FNHSysConvAssetId.Visible = False
        '
        'FNFixedAssetType
        '
        Me.FNFixedAssetType.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FNFixedAssetType.EditValue = ""
        Me.FNFixedAssetType.EnterMoveNextControl = True
        Me.FNFixedAssetType.Location = New System.Drawing.Point(181, 48)
        Me.FNFixedAssetType.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNFixedAssetType.Name = "FNFixedAssetType"
        Me.FNFixedAssetType.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FNFixedAssetType.Properties.Appearance.Options.UseBackColor = True
        Me.FNFixedAssetType.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FNFixedAssetType.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FNFixedAssetType.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FNFixedAssetType.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FNFixedAssetType.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNFixedAssetType.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNFixedAssetType.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNFixedAssetType.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNFixedAssetType.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNFixedAssetType.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNFixedAssetType.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNFixedAssetType.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNFixedAssetType.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FNFixedAssetType.Properties.Tag = "FNFixedAssetType"
        Me.FNFixedAssetType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.FNFixedAssetType.Size = New System.Drawing.Size(182, 23)
        Me.FNFixedAssetType.TabIndex = 307
        Me.FNFixedAssetType.Tag = "2|"
        '
        'FNFixedAssetType_lbl
        '
        Me.FNFixedAssetType_lbl.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FNFixedAssetType_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNFixedAssetType_lbl.Appearance.Options.UseForeColor = True
        Me.FNFixedAssetType_lbl.Appearance.Options.UseTextOptions = True
        Me.FNFixedAssetType_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNFixedAssetType_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNFixedAssetType_lbl.Location = New System.Drawing.Point(27, 46)
        Me.FNFixedAssetType_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNFixedAssetType_lbl.Name = "FNFixedAssetType_lbl"
        Me.FNFixedAssetType_lbl.Size = New System.Drawing.Size(148, 23)
        Me.FNFixedAssetType_lbl.TabIndex = 306
        Me.FNFixedAssetType_lbl.Tag = "2|"
        Me.FNFixedAssetType_lbl.Text = "Asset Type :"
        '
        'FNEndQty
        '
        Me.FNEndQty.Location = New System.Drawing.Point(567, 110)
        Me.FNEndQty.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNEndQty.Name = "FNEndQty"
        Me.FNEndQty.Properties.AllowMouseWheel = False
        Me.FNEndQty.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNEndQty.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNEndQty.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNEndQty.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FNEndQty.Properties.DisplayFormat.FormatString = "{0:n0}"
        Me.FNEndQty.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNEndQty.Properties.Precision = 0
        Me.FNEndQty.Size = New System.Drawing.Size(182, 23)
        Me.FNEndQty.TabIndex = 423
        Me.FNEndQty.Tag = ""
        '
        'FNEndQty_lbl
        '
        Me.FNEndQty_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNEndQty_lbl.Appearance.Options.UseForeColor = True
        Me.FNEndQty_lbl.Appearance.Options.UseTextOptions = True
        Me.FNEndQty_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNEndQty_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNEndQty_lbl.Location = New System.Drawing.Point(397, 109)
        Me.FNEndQty_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNEndQty_lbl.Name = "FNEndQty_lbl"
        Me.FNEndQty_lbl.Size = New System.Drawing.Size(162, 23)
        Me.FNEndQty_lbl.TabIndex = 422
        Me.FNEndQty_lbl.Tag = "2|"
        Me.FNEndQty_lbl.Text = "FNEndQty :"
        '
        'FTUserName_None
        '
        Me.FTUserName_None.Location = New System.Drawing.Point(369, 79)
        Me.FTUserName_None.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTUserName_None.Name = "FTUserName_None"
        Me.FTUserName_None.Properties.Appearance.BackColor = System.Drawing.Color.LightCyan
        Me.FTUserName_None.Properties.Appearance.Options.UseBackColor = True
        Me.FTUserName_None.Properties.ReadOnly = True
        Me.FTUserName_None.Size = New System.Drawing.Size(380, 23)
        Me.FTUserName_None.TabIndex = 430
        Me.FTUserName_None.Tag = "2|"
        '
        'FTUserName
        '
        Me.FTUserName.EditValue = "                    "
        Me.FTUserName.EnterMoveNextControl = True
        Me.FTUserName.Location = New System.Drawing.Point(181, 79)
        Me.FTUserName.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTUserName.Name = "FTUserName"
        Me.FTUserName.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FTUserName.Properties.Appearance.Options.UseBackColor = True
        Me.FTUserName.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FTUserName.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FTUserName.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FTUserName.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FTUserName.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FTUserName.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FTUserName.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FTUserName.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FTUserName.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FTUserName.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FTUserName.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FTUserName.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FTUserName.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", 15, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", "471", Nothing, True)})
        Me.FTUserName.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.FTUserName.Properties.MaxLength = 30
        Me.FTUserName.Size = New System.Drawing.Size(182, 23)
        Me.FTUserName.TabIndex = 429
        Me.FTUserName.Tag = "2|"
        '
        'FTUserName_lbl
        '
        Me.FTUserName_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTUserName_lbl.Appearance.Options.UseForeColor = True
        Me.FTUserName_lbl.Appearance.Options.UseTextOptions = True
        Me.FTUserName_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTUserName_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTUserName_lbl.Location = New System.Drawing.Point(13, 78)
        Me.FTUserName_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTUserName_lbl.Name = "FTUserName_lbl"
        Me.FTUserName_lbl.Size = New System.Drawing.Size(162, 23)
        Me.FTUserName_lbl.TabIndex = 428
        Me.FTUserName_lbl.Tag = "2|"
        Me.FTUserName_lbl.Text = "FTUserName :"
        '
        'FNStartQty
        '
        Me.FNStartQty.Location = New System.Drawing.Point(181, 110)
        Me.FNStartQty.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNStartQty.Name = "FNStartQty"
        Me.FNStartQty.Properties.AllowMouseWheel = False
        Me.FNStartQty.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNStartQty.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNStartQty.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNStartQty.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FNStartQty.Properties.DisplayFormat.FormatString = "{0:n0}"
        Me.FNStartQty.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNStartQty.Properties.Precision = 0
        Me.FNStartQty.Size = New System.Drawing.Size(182, 23)
        Me.FNStartQty.TabIndex = 432
        Me.FNStartQty.Tag = ""
        '
        'FNStartQty_lbl
        '
        Me.FNStartQty_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNStartQty_lbl.Appearance.Options.UseForeColor = True
        Me.FNStartQty_lbl.Appearance.Options.UseTextOptions = True
        Me.FNStartQty_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNStartQty_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNStartQty_lbl.Location = New System.Drawing.Point(11, 109)
        Me.FNStartQty_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNStartQty_lbl.Name = "FNStartQty_lbl"
        Me.FNStartQty_lbl.Size = New System.Drawing.Size(162, 23)
        Me.FNStartQty_lbl.TabIndex = 431
        Me.FNStartQty_lbl.Tag = "2|"
        Me.FNStartQty_lbl.Text = "FNStartQty :"
        '
        'FNHSysCmpId_lbl
        '
        Me.FNHSysCmpId_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysCmpId_lbl.Appearance.Options.UseForeColor = True
        Me.FNHSysCmpId_lbl.Appearance.Options.UseTextOptions = True
        Me.FNHSysCmpId_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysCmpId_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysCmpId_lbl.Location = New System.Drawing.Point(27, 17)
        Me.FNHSysCmpId_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysCmpId_lbl.Name = "FNHSysCmpId_lbl"
        Me.FNHSysCmpId_lbl.Size = New System.Drawing.Size(148, 21)
        Me.FNHSysCmpId_lbl.TabIndex = 448
        Me.FNHSysCmpId_lbl.Tag = "2|"
        Me.FNHSysCmpId_lbl.Text = "Company :"
        '
        'FNHSysCmpId_None
        '
        Me.FNHSysCmpId_None.Location = New System.Drawing.Point(369, 17)
        Me.FNHSysCmpId_None.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysCmpId_None.Name = "FNHSysCmpId_None"
        Me.FNHSysCmpId_None.Properties.Appearance.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysCmpId_None.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysCmpId_None.Properties.ReadOnly = True
        Me.FNHSysCmpId_None.Size = New System.Drawing.Size(380, 23)
        Me.FNHSysCmpId_None.TabIndex = 450
        Me.FNHSysCmpId_None.Tag = ""
        '
        'FNHSysCmpId
        '
        Me.FNHSysCmpId.Location = New System.Drawing.Point(181, 17)
        Me.FNHSysCmpId.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysCmpId.Name = "FNHSysCmpId"
        Me.FNHSysCmpId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject2, "", "11", Nothing, True)})
        Me.FNHSysCmpId.Properties.Tag = ""
        Me.FNHSysCmpId.Size = New System.Drawing.Size(182, 23)
        Me.FNHSysCmpId.TabIndex = 449
        Me.FNHSysCmpId.Tag = ""
        '
        'FTStateDirector
        '
        Me.FTStateDirector.Location = New System.Drawing.Point(427, 149)
        Me.FTStateDirector.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTStateDirector.Name = "FTStateDirector"
        Me.FTStateDirector.Properties.Caption = "FTStateDirector"
        Me.FTStateDirector.Properties.ValueChecked = "1"
        Me.FTStateDirector.Properties.ValueUnchecked = "0"
        Me.FTStateDirector.Size = New System.Drawing.Size(166, 20)
        Me.FTStateDirector.TabIndex = 425
        Me.FTStateDirector.TabStop = False
        Me.FTStateDirector.Tag = "2|"
        '
        'FTStateFactory
        '
        Me.FTStateFactory.Location = New System.Drawing.Point(181, 149)
        Me.FTStateFactory.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTStateFactory.Name = "FTStateFactory"
        Me.FTStateFactory.Properties.Caption = "FTStateFactory"
        Me.FTStateFactory.Properties.ValueChecked = "1"
        Me.FTStateFactory.Properties.ValueUnchecked = "0"
        Me.FTStateFactory.Size = New System.Drawing.Size(166, 20)
        Me.FTStateFactory.TabIndex = 451
        Me.FTStateFactory.TabStop = False
        Me.FTStateFactory.Tag = "2|"
        '
        'wAddItemAssetLevel
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(777, 231)
        Me.Controls.Add(Me.FTStateFactory)
        Me.Controls.Add(Me.FTStateDirector)
        Me.Controls.Add(Me.FNHSysCmpId_lbl)
        Me.Controls.Add(Me.FNHSysCmpId_None)
        Me.Controls.Add(Me.FNHSysCmpId)
        Me.Controls.Add(Me.FNStartQty)
        Me.Controls.Add(Me.FNStartQty_lbl)
        Me.Controls.Add(Me.FTUserName_None)
        Me.Controls.Add(Me.FTUserName)
        Me.Controls.Add(Me.FTUserName_lbl)
        Me.Controls.Add(Me.FNEndQty)
        Me.Controls.Add(Me.FNEndQty_lbl)
        Me.Controls.Add(Me.FNFixedAssetType)
        Me.Controls.Add(Me.FNFixedAssetType_lbl)
        Me.Controls.Add(Me.ogbbutton)
        Me.Controls.Add(Me.FNHSysConvAssetId)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "wAddItemAssetLevel"
        Me.Tag = "2|"
        Me.Text = "AssetAdditemLevel"
        CType(Me.ogbbutton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbbutton.ResumeLayout(False)
        CType(Me.FNHSysConvAssetId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNFixedAssetType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNEndQty.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTUserName_None.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTUserName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNStartQty.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysCmpId_None.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysCmpId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTStateDirector.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTStateFactory.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents ocmclear As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ogbbutton As DevExpress.XtraEditors.PanelControl
    Friend WithEvents ocmdelete As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FNHSysConvAssetId As DevExpress.XtraEditors.TextEdit
    Friend WithEvents ocmedit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FNFixedAssetType As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents FNFixedAssetType_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNEndQty As DevExpress.XtraEditors.CalcEdit
    Friend WithEvents FNEndQty_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ocmaddnew As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FTUserName_None As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FTUserName As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FTUserName_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNStartQty As DevExpress.XtraEditors.CalcEdit
    Friend WithEvents FNStartQty_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysCmpId_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysCmpId_None As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FNHSysCmpId As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FTStateDirector As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents FTStateFactory As DevExpress.XtraEditors.CheckEdit
End Class

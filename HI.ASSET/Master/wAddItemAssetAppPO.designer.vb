<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wAddItemAssetAppPO
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
        Me.FNHSysDirectorGrpId_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTManagerName_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTManagerUserName = New DevExpress.XtraEditors.ButtonEdit()
        Me.FNHSysDirectorGrpId = New DevExpress.XtraEditors.ButtonEdit()
        Me.FNHSysDirectorGrpId_None = New DevExpress.XtraEditors.TextEdit()
        Me.FTManagerUserName_None = New DevExpress.XtraEditors.TextEdit()
        CType(Me.ogbbutton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbbutton.SuspendLayout()
        CType(Me.FNHSysConvAssetId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNFixedAssetType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTManagerUserName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysDirectorGrpId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysDirectorGrpId_None.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTManagerUserName_None.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.ogbbutton.Location = New System.Drawing.Point(0, 139)
        Me.ogbbutton.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbbutton.Name = "ogbbutton"
        Me.ogbbutton.Size = New System.Drawing.Size(685, 54)
        Me.ogbbutton.TabIndex = 302
        '
        'ocmaddnew
        '
        Me.ocmaddnew.Location = New System.Drawing.Point(21, 21)
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
        Me.FNHSysConvAssetId.Location = New System.Drawing.Point(230, 169)
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
        Me.FNFixedAssetType.Location = New System.Drawing.Point(180, 23)
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
        Me.FNFixedAssetType_lbl.Location = New System.Drawing.Point(26, 21)
        Me.FNFixedAssetType_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNFixedAssetType_lbl.Name = "FNFixedAssetType_lbl"
        Me.FNFixedAssetType_lbl.Size = New System.Drawing.Size(148, 23)
        Me.FNFixedAssetType_lbl.TabIndex = 306
        Me.FNFixedAssetType_lbl.Tag = "2|"
        Me.FNFixedAssetType_lbl.Text = "Asset Type :"
        '
        'FNHSysDirectorGrpId_lbl
        '
        Me.FNHSysDirectorGrpId_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysDirectorGrpId_lbl.Appearance.Options.UseForeColor = True
        Me.FNHSysDirectorGrpId_lbl.Appearance.Options.UseTextOptions = True
        Me.FNHSysDirectorGrpId_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysDirectorGrpId_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysDirectorGrpId_lbl.Location = New System.Drawing.Point(12, 88)
        Me.FNHSysDirectorGrpId_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysDirectorGrpId_lbl.Name = "FNHSysDirectorGrpId_lbl"
        Me.FNHSysDirectorGrpId_lbl.Size = New System.Drawing.Size(162, 23)
        Me.FNHSysDirectorGrpId_lbl.TabIndex = 420
        Me.FNHSysDirectorGrpId_lbl.Tag = "2|"
        Me.FNHSysDirectorGrpId_lbl.Text = "FNHSysDirectorGrpId :"
        '
        'FTManagerName_lbl
        '
        Me.FTManagerName_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTManagerName_lbl.Appearance.Options.UseForeColor = True
        Me.FTManagerName_lbl.Appearance.Options.UseTextOptions = True
        Me.FTManagerName_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTManagerName_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTManagerName_lbl.Location = New System.Drawing.Point(12, 55)
        Me.FTManagerName_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTManagerName_lbl.Name = "FTManagerName_lbl"
        Me.FTManagerName_lbl.Size = New System.Drawing.Size(162, 23)
        Me.FTManagerName_lbl.TabIndex = 422
        Me.FTManagerName_lbl.Tag = "2|"
        Me.FTManagerName_lbl.Text = "FTManagerName :"
        '
        'FTManagerUserName
        '
        Me.FTManagerUserName.EnterMoveNextControl = True
        Me.FTManagerUserName.Location = New System.Drawing.Point(180, 56)
        Me.FTManagerUserName.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTManagerUserName.Name = "FTManagerUserName"
        Me.FTManagerUserName.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FTManagerUserName.Properties.Appearance.Options.UseBackColor = True
        Me.FTManagerUserName.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FTManagerUserName.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FTManagerUserName.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FTManagerUserName.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FTManagerUserName.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FTManagerUserName.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FTManagerUserName.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FTManagerUserName.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FTManagerUserName.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FTManagerUserName.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FTManagerUserName.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FTManagerUserName.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FTManagerUserName.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", 15, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", "467", Nothing, True)})
        Me.FTManagerUserName.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.FTManagerUserName.Properties.MaxLength = 30
        Me.FTManagerUserName.Size = New System.Drawing.Size(182, 23)
        Me.FTManagerUserName.TabIndex = 425
        Me.FTManagerUserName.Tag = "2|"
        '
        'FNHSysDirectorGrpId
        '
        Me.FNHSysDirectorGrpId.EditValue = "                    "
        Me.FNHSysDirectorGrpId.EnterMoveNextControl = True
        Me.FNHSysDirectorGrpId.Location = New System.Drawing.Point(180, 89)
        Me.FNHSysDirectorGrpId.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysDirectorGrpId.Name = "FNHSysDirectorGrpId"
        Me.FNHSysDirectorGrpId.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FNHSysDirectorGrpId.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysDirectorGrpId.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysDirectorGrpId.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysDirectorGrpId.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FNHSysDirectorGrpId.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FNHSysDirectorGrpId.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNHSysDirectorGrpId.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysDirectorGrpId.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNHSysDirectorGrpId.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNHSysDirectorGrpId.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysDirectorGrpId.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysDirectorGrpId.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNHSysDirectorGrpId.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNHSysDirectorGrpId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", 15, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject2, "", "227", Nothing, True)})
        Me.FNHSysDirectorGrpId.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.FNHSysDirectorGrpId.Properties.MaxLength = 30
        Me.FNHSysDirectorGrpId.Size = New System.Drawing.Size(182, 23)
        Me.FNHSysDirectorGrpId.TabIndex = 426
        Me.FNHSysDirectorGrpId.Tag = "2|"
        '
        'FNHSysDirectorGrpId_None
        '
        Me.FNHSysDirectorGrpId_None.Location = New System.Drawing.Point(368, 89)
        Me.FNHSysDirectorGrpId_None.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysDirectorGrpId_None.Name = "FNHSysDirectorGrpId_None"
        Me.FNHSysDirectorGrpId_None.Properties.Appearance.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysDirectorGrpId_None.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysDirectorGrpId_None.Properties.ReadOnly = True
        Me.FNHSysDirectorGrpId_None.Size = New System.Drawing.Size(250, 23)
        Me.FNHSysDirectorGrpId_None.TabIndex = 427
        Me.FNHSysDirectorGrpId_None.Tag = "2|"
        '
        'FTManagerUserName_None
        '
        Me.FTManagerUserName_None.Location = New System.Drawing.Point(368, 56)
        Me.FTManagerUserName_None.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTManagerUserName_None.Name = "FTManagerUserName_None"
        Me.FTManagerUserName_None.Properties.Appearance.BackColor = System.Drawing.Color.LightCyan
        Me.FTManagerUserName_None.Properties.Appearance.Options.UseBackColor = True
        Me.FTManagerUserName_None.Properties.ReadOnly = True
        Me.FTManagerUserName_None.Size = New System.Drawing.Size(250, 23)
        Me.FTManagerUserName_None.TabIndex = 428
        Me.FTManagerUserName_None.Tag = "2|"
        '
        'wAddItemAssetAppPO
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(685, 193)
        Me.Controls.Add(Me.FTManagerUserName_None)
        Me.Controls.Add(Me.FNHSysDirectorGrpId_None)
        Me.Controls.Add(Me.FNHSysDirectorGrpId)
        Me.Controls.Add(Me.FTManagerUserName)
        Me.Controls.Add(Me.FTManagerName_lbl)
        Me.Controls.Add(Me.FNHSysDirectorGrpId_lbl)
        Me.Controls.Add(Me.FNFixedAssetType)
        Me.Controls.Add(Me.FNFixedAssetType_lbl)
        Me.Controls.Add(Me.ogbbutton)
        Me.Controls.Add(Me.FNHSysConvAssetId)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "wAddItemAssetAppPO"
        Me.Tag = "2|"
        Me.Text = "AssetAdditemLAppPO"
        CType(Me.ogbbutton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbbutton.ResumeLayout(False)
        CType(Me.FNHSysConvAssetId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNFixedAssetType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTManagerUserName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysDirectorGrpId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysDirectorGrpId_None.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTManagerUserName_None.Properties, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents FNHSysDirectorGrpId_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTManagerName_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ocmaddnew As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FTManagerUserName As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FNHSysDirectorGrpId As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FNHSysDirectorGrpId_None As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FTManagerUserName_None As DevExpress.XtraEditors.TextEdit
End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wAutoGenerateBarcodeCust
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
        Dim EditorButtonImageOptions1 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject4 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.ocmcancel = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmcreate = New DevExpress.XtraEditors.SimpleButton()
        Me.FNGenerateBarcodeCustType_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNGenerateBarcodeCustType = New DevExpress.XtraEditors.ComboBoxEdit()
        CType(Me.FNGenerateBarcodeCustType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ocmcancel
        '
        Me.ocmcancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmcancel.Location = New System.Drawing.Point(384, 91)
        Me.ocmcancel.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmcancel.Name = "ocmcancel"
        Me.ocmcancel.Size = New System.Drawing.Size(180, 31)
        Me.ocmcancel.TabIndex = 311
        Me.ocmcancel.TabStop = False
        Me.ocmcancel.Tag = "2|"
        Me.ocmcancel.Text = "CANCEL"
        '
        'ocmcreate
        '
        Me.ocmcreate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmcreate.Location = New System.Drawing.Point(92, 91)
        Me.ocmcreate.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmcreate.Name = "ocmcreate"
        Me.ocmcreate.Size = New System.Drawing.Size(180, 31)
        Me.ocmcreate.TabIndex = 310
        Me.ocmcreate.TabStop = False
        Me.ocmcreate.Tag = "2|"
        Me.ocmcreate.Text = "CREATE"
        '
        'FNGenerateBarcodeCustType_lbl
        '
        Me.FNGenerateBarcodeCustType_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNGenerateBarcodeCustType_lbl.Appearance.Options.UseForeColor = True
        Me.FNGenerateBarcodeCustType_lbl.Appearance.Options.UseTextOptions = True
        Me.FNGenerateBarcodeCustType_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNGenerateBarcodeCustType_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNGenerateBarcodeCustType_lbl.Location = New System.Drawing.Point(5, 28)
        Me.FNGenerateBarcodeCustType_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNGenerateBarcodeCustType_lbl.Name = "FNGenerateBarcodeCustType_lbl"
        Me.FNGenerateBarcodeCustType_lbl.Size = New System.Drawing.Size(142, 23)
        Me.FNGenerateBarcodeCustType_lbl.TabIndex = 313
        Me.FNGenerateBarcodeCustType_lbl.Tag = "2|"
        Me.FNGenerateBarcodeCustType_lbl.Text = "Generate Type :"
        '
        'FNGenerateBarcodeCustType
        '
        Me.FNGenerateBarcodeCustType.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FNGenerateBarcodeCustType.EditValue = ""
        Me.FNGenerateBarcodeCustType.EnterMoveNextControl = True
        Me.FNGenerateBarcodeCustType.Location = New System.Drawing.Point(151, 29)
        Me.FNGenerateBarcodeCustType.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNGenerateBarcodeCustType.Name = "FNGenerateBarcodeCustType"
        Me.FNGenerateBarcodeCustType.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FNGenerateBarcodeCustType.Properties.Appearance.Options.UseBackColor = True
        Me.FNGenerateBarcodeCustType.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FNGenerateBarcodeCustType.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FNGenerateBarcodeCustType.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FNGenerateBarcodeCustType.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FNGenerateBarcodeCustType.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNGenerateBarcodeCustType.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNGenerateBarcodeCustType.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNGenerateBarcodeCustType.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNGenerateBarcodeCustType.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNGenerateBarcodeCustType.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNGenerateBarcodeCustType.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNGenerateBarcodeCustType.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNGenerateBarcodeCustType.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, True, True, False, EditorButtonImageOptions1, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, SerializableAppearanceObject2, SerializableAppearanceObject3, SerializableAppearanceObject4, "", "FNPackCartonSubType", Nothing, DevExpress.Utils.ToolTipAnchor.[Default])})
        Me.FNGenerateBarcodeCustType.Properties.Tag = "FNGenerateBarcodeCustType"
        Me.FNGenerateBarcodeCustType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.FNGenerateBarcodeCustType.Size = New System.Drawing.Size(413, 22)
        Me.FNGenerateBarcodeCustType.TabIndex = 312
        Me.FNGenerateBarcodeCustType.Tag = "2|"
        '
        'wAutoGenerateBarcodeCust
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(644, 156)
        Me.ControlBox = False
        Me.Controls.Add(Me.FNGenerateBarcodeCustType_lbl)
        Me.Controls.Add(Me.FNGenerateBarcodeCustType)
        Me.Controls.Add(Me.ocmcancel)
        Me.Controls.Add(Me.ocmcreate)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "wAutoGenerateBarcodeCust"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "wAutoGenerateBarcodeCust"
        CType(Me.FNGenerateBarcodeCustType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ocmcancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmcreate As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FNGenerateBarcodeCustType_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNGenerateBarcodeCustType As DevExpress.XtraEditors.ComboBoxEdit
End Class

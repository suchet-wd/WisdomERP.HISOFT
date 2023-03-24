<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ucUserLogin
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ucUserLogin))
        Me.FNHSysCmpId = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.FNHSysCmpId_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.FNLang = New DevExpress.XtraEditors.ImageComboBoxEdit()
        Me.otbPassword = New DevExpress.XtraEditors.TextEdit()
        Me.otbLogin = New DevExpress.XtraEditors.TextEdit()
        CType(Me.FNHSysCmpId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNLang.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.otbPassword.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.otbLogin.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'FNHSysCmpId
        '
        Me.FNHSysCmpId.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FNHSysCmpId.EditValue = ""
        Me.FNHSysCmpId.EnterMoveNextControl = True
        Me.FNHSysCmpId.Location = New System.Drawing.Point(263, 123)
        Me.FNHSysCmpId.Name = "FNHSysCmpId"
        Me.FNHSysCmpId.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FNHSysCmpId.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysCmpId.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysCmpId.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysCmpId.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FNHSysCmpId.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FNHSysCmpId.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNHSysCmpId.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysCmpId.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNHSysCmpId.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNHSysCmpId.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysCmpId.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysCmpId.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNHSysCmpId.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNHSysCmpId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FNHSysCmpId.Properties.Tag = "FNPoState"
        Me.FNHSysCmpId.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.FNHSysCmpId.Size = New System.Drawing.Size(172, 20)
        Me.FNHSysCmpId.TabIndex = 21
        Me.FNHSysCmpId.Tag = "2|"
        '
        'FNHSysCmpId_lbl
        '
        Me.FNHSysCmpId_lbl.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.FNHSysCmpId_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysCmpId_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysCmpId_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysCmpId_lbl.Location = New System.Drawing.Point(85, 126)
        Me.FNHSysCmpId_lbl.Name = "FNHSysCmpId_lbl"
        Me.FNHSysCmpId_lbl.Size = New System.Drawing.Size(175, 17)
        Me.FNHSysCmpId_lbl.TabIndex = 20
        Me.FNHSysCmpId_lbl.Text = "Company :"
        '
        'LabelControl3
        '
        Me.LabelControl3.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.LabelControl3.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.LabelControl3.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LabelControl3.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LabelControl3.Location = New System.Drawing.Point(81, 149)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(177, 17)
        Me.LabelControl3.TabIndex = 19
        Me.LabelControl3.Text = "Language :"
        '
        'LabelControl2
        '
        Me.LabelControl2.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.LabelControl2.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.LabelControl2.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LabelControl2.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LabelControl2.Location = New System.Drawing.Point(85, 101)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(176, 17)
        Me.LabelControl2.TabIndex = 18
        Me.LabelControl2.Text = "Password :"
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.LabelControl1.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.LabelControl1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LabelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LabelControl1.Location = New System.Drawing.Point(78, 77)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(183, 17)
        Me.LabelControl1.TabIndex = 17
        Me.LabelControl1.Text = "User Name :"
        '
        'FNLang
        '
        Me.FNLang.Location = New System.Drawing.Point(263, 146)
        Me.FNLang.Name = "FNLang"
        Me.FNLang.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FNLang.Size = New System.Drawing.Size(172, 20)
        Me.FNLang.TabIndex = 16
        '
        'otbPassword
        '
        Me.otbPassword.EditValue = ""
        Me.otbPassword.Location = New System.Drawing.Point(263, 99)
        Me.otbPassword.Name = "otbPassword"
        Me.otbPassword.Properties.MaxLength = 20
        Me.otbPassword.Properties.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.otbPassword.Size = New System.Drawing.Size(172, 20)
        Me.otbPassword.TabIndex = 15
        '
        'otbLogin
        '
        Me.otbLogin.EditValue = ""
        Me.otbLogin.Location = New System.Drawing.Point(263, 75)
        Me.otbLogin.Name = "otbLogin"
        Me.otbLogin.Properties.MaxLength = 50
        Me.otbLogin.Size = New System.Drawing.Size(172, 20)
        Me.otbLogin.TabIndex = 14
        '
        'ucUserLogin
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Controls.Add(Me.FNHSysCmpId)
        Me.Controls.Add(Me.FNHSysCmpId_lbl)
        Me.Controls.Add(Me.LabelControl3)
        Me.Controls.Add(Me.LabelControl2)
        Me.Controls.Add(Me.LabelControl1)
        Me.Controls.Add(Me.FNLang)
        Me.Controls.Add(Me.otbPassword)
        Me.Controls.Add(Me.otbLogin)
        Me.DoubleBuffered = True
        Me.Name = "ucUserLogin"
        Me.Size = New System.Drawing.Size(476, 205)
        CType(Me.FNHSysCmpId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNLang.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.otbPassword.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.otbLogin.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents FNHSysCmpId As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents FNHSysCmpId_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNLang As DevExpress.XtraEditors.ImageComboBoxEdit
    Friend WithEvents otbPassword As DevExpress.XtraEditors.TextEdit
    Friend WithEvents otbLogin As DevExpress.XtraEditors.TextEdit

End Class

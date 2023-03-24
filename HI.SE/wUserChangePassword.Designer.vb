<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wUserChangePassword
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
        Me.ogbbutton = New DevExpress.XtraEditors.GroupControl()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmsave = New DevExpress.XtraEditors.SimpleButton()
        Me.ogbinfo = New DevExpress.XtraEditors.GroupControl()
        Me.otbpldpassword = New DevExpress.XtraEditors.TextEdit()
        Me.otbpldpassword_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTPasswordRe = New DevExpress.XtraEditors.TextEdit()
        Me.FTPasswordRe_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTPassword = New DevExpress.XtraEditors.TextEdit()
        Me.FTPassword_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTNickName = New DevExpress.XtraEditors.TextEdit()
        Me.FTNickName_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTTel = New DevExpress.XtraEditors.TextEdit()
        Me.FTTel_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTEmail = New DevExpress.XtraEditors.TextEdit()
        Me.FTEmail_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTFax = New DevExpress.XtraEditors.TextEdit()
        Me.FTFax_lbl = New DevExpress.XtraEditors.LabelControl()
        CType(Me.ogbbutton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbbutton.SuspendLayout()
        CType(Me.ogbinfo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbinfo.SuspendLayout()
        CType(Me.otbpldpassword.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTPasswordRe.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTPassword.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTNickName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTTel.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTEmail.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTFax.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ogbbutton
        '
        Me.ogbbutton.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbbutton.Controls.Add(Me.ocmexit)
        Me.ogbbutton.Controls.Add(Me.ocmsave)
        Me.ogbbutton.Location = New System.Drawing.Point(2, 308)
        Me.ogbbutton.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbbutton.Name = "ogbbutton"
        Me.ogbbutton.ShowCaption = False
        Me.ogbbutton.Size = New System.Drawing.Size(646, 52)
        Me.ogbbutton.TabIndex = 19
        Me.ogbbutton.Text = "GroupControl9"
        '
        'ocmexit
        '
        Me.ocmexit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmexit.Location = New System.Drawing.Point(348, 11)
        Me.ocmexit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmexit.Name = "ocmexit"
        Me.ocmexit.Size = New System.Drawing.Size(160, 31)
        Me.ocmexit.TabIndex = 103
        Me.ocmexit.TabStop = False
        Me.ocmexit.Tag = "2|"
        Me.ocmexit.Text = "EXIT"
        '
        'ocmsave
        '
        Me.ocmsave.Location = New System.Drawing.Point(124, 11)
        Me.ocmsave.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmsave.Name = "ocmsave"
        Me.ocmsave.Size = New System.Drawing.Size(160, 31)
        Me.ocmsave.TabIndex = 102
        Me.ocmsave.TabStop = False
        Me.ocmsave.Tag = "2|"
        Me.ocmsave.Text = "SAVE"
        '
        'ogbinfo
        '
        Me.ogbinfo.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbinfo.Controls.Add(Me.FTFax)
        Me.ogbinfo.Controls.Add(Me.FTFax_lbl)
        Me.ogbinfo.Controls.Add(Me.FTEmail)
        Me.ogbinfo.Controls.Add(Me.FTEmail_lbl)
        Me.ogbinfo.Controls.Add(Me.FTTel)
        Me.ogbinfo.Controls.Add(Me.FTTel_lbl)
        Me.ogbinfo.Controls.Add(Me.FTNickName)
        Me.ogbinfo.Controls.Add(Me.FTNickName_lbl)
        Me.ogbinfo.Controls.Add(Me.otbpldpassword)
        Me.ogbinfo.Controls.Add(Me.otbpldpassword_lbl)
        Me.ogbinfo.Controls.Add(Me.FTPasswordRe)
        Me.ogbinfo.Controls.Add(Me.FTPasswordRe_lbl)
        Me.ogbinfo.Controls.Add(Me.FTPassword)
        Me.ogbinfo.Controls.Add(Me.FTPassword_lbl)
        Me.ogbinfo.Location = New System.Drawing.Point(6, 4)
        Me.ogbinfo.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbinfo.Name = "ogbinfo"
        Me.ogbinfo.Size = New System.Drawing.Size(643, 297)
        Me.ogbinfo.TabIndex = 18
        Me.ogbinfo.Text = "User Infomation"
        '
        'otbpldpassword
        '
        Me.otbpldpassword.Location = New System.Drawing.Point(218, 58)
        Me.otbpldpassword.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.otbpldpassword.Name = "otbpldpassword"
        Me.otbpldpassword.Properties.MaxLength = 16
        Me.otbpldpassword.Properties.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.otbpldpassword.Size = New System.Drawing.Size(307, 22)
        Me.otbpldpassword.TabIndex = 0
        Me.otbpldpassword.Tag = "2|"
        '
        'otbpldpassword_lbl
        '
        Me.otbpldpassword_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.otbpldpassword_lbl.Appearance.Options.UseForeColor = True
        Me.otbpldpassword_lbl.Appearance.Options.UseTextOptions = True
        Me.otbpldpassword_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.otbpldpassword_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.otbpldpassword_lbl.Location = New System.Drawing.Point(26, 55)
        Me.otbpldpassword_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.otbpldpassword_lbl.Name = "otbpldpassword_lbl"
        Me.otbpldpassword_lbl.Size = New System.Drawing.Size(189, 23)
        Me.otbpldpassword_lbl.TabIndex = 391
        Me.otbpldpassword_lbl.Tag = "2|"
        Me.otbpldpassword_lbl.Text = "Old Password :"
        '
        'FTPasswordRe
        '
        Me.FTPasswordRe.Location = New System.Drawing.Point(218, 114)
        Me.FTPasswordRe.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTPasswordRe.Name = "FTPasswordRe"
        Me.FTPasswordRe.Properties.MaxLength = 16
        Me.FTPasswordRe.Properties.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.FTPasswordRe.Size = New System.Drawing.Size(307, 22)
        Me.FTPasswordRe.TabIndex = 2
        Me.FTPasswordRe.Tag = "2|"
        '
        'FTPasswordRe_lbl
        '
        Me.FTPasswordRe_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTPasswordRe_lbl.Appearance.Options.UseForeColor = True
        Me.FTPasswordRe_lbl.Appearance.Options.UseTextOptions = True
        Me.FTPasswordRe_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTPasswordRe_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTPasswordRe_lbl.Location = New System.Drawing.Point(26, 114)
        Me.FTPasswordRe_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTPasswordRe_lbl.Name = "FTPasswordRe_lbl"
        Me.FTPasswordRe_lbl.Size = New System.Drawing.Size(189, 23)
        Me.FTPasswordRe_lbl.TabIndex = 286
        Me.FTPasswordRe_lbl.Tag = "2|"
        Me.FTPasswordRe_lbl.Text = "Re New Password :"
        '
        'FTPassword
        '
        Me.FTPassword.Location = New System.Drawing.Point(218, 87)
        Me.FTPassword.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTPassword.Name = "FTPassword"
        Me.FTPassword.Properties.MaxLength = 16
        Me.FTPassword.Properties.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.FTPassword.Size = New System.Drawing.Size(307, 22)
        Me.FTPassword.TabIndex = 1
        Me.FTPassword.Tag = "2|"
        '
        'FTPassword_lbl
        '
        Me.FTPassword_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTPassword_lbl.Appearance.Options.UseForeColor = True
        Me.FTPassword_lbl.Appearance.Options.UseTextOptions = True
        Me.FTPassword_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTPassword_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTPassword_lbl.Location = New System.Drawing.Point(26, 87)
        Me.FTPassword_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTPassword_lbl.Name = "FTPassword_lbl"
        Me.FTPassword_lbl.Size = New System.Drawing.Size(189, 23)
        Me.FTPassword_lbl.TabIndex = 284
        Me.FTPassword_lbl.Tag = "2|"
        Me.FTPassword_lbl.Text = "New Password :"
        '
        'FTNickName
        '
        Me.FTNickName.Location = New System.Drawing.Point(218, 174)
        Me.FTNickName.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTNickName.Name = "FTNickName"
        Me.FTNickName.Properties.MaxLength = 50
        Me.FTNickName.Size = New System.Drawing.Size(307, 22)
        Me.FTNickName.TabIndex = 392
        Me.FTNickName.Tag = "2|"
        '
        'FTNickName_lbl
        '
        Me.FTNickName_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTNickName_lbl.Appearance.Options.UseForeColor = True
        Me.FTNickName_lbl.Appearance.Options.UseTextOptions = True
        Me.FTNickName_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTNickName_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTNickName_lbl.Location = New System.Drawing.Point(27, 174)
        Me.FTNickName_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTNickName_lbl.Name = "FTNickName_lbl"
        Me.FTNickName_lbl.Size = New System.Drawing.Size(189, 23)
        Me.FTNickName_lbl.TabIndex = 393
        Me.FTNickName_lbl.Tag = "2|"
        Me.FTNickName_lbl.Text = "Nick Name :"
        '
        'FTTel
        '
        Me.FTTel.Location = New System.Drawing.Point(218, 204)
        Me.FTTel.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTTel.Name = "FTTel"
        Me.FTTel.Properties.MaxLength = 50
        Me.FTTel.Size = New System.Drawing.Size(307, 22)
        Me.FTTel.TabIndex = 394
        Me.FTTel.Tag = "2|"
        '
        'FTTel_lbl
        '
        Me.FTTel_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTTel_lbl.Appearance.Options.UseForeColor = True
        Me.FTTel_lbl.Appearance.Options.UseTextOptions = True
        Me.FTTel_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTTel_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTTel_lbl.Location = New System.Drawing.Point(27, 204)
        Me.FTTel_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTTel_lbl.Name = "FTTel_lbl"
        Me.FTTel_lbl.Size = New System.Drawing.Size(189, 23)
        Me.FTTel_lbl.TabIndex = 395
        Me.FTTel_lbl.Tag = "2|"
        Me.FTTel_lbl.Text = "Tel. :"
        '
        'FTEmail
        '
        Me.FTEmail.Location = New System.Drawing.Point(218, 234)
        Me.FTEmail.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTEmail.Name = "FTEmail"
        Me.FTEmail.Properties.MaxLength = 50
        Me.FTEmail.Size = New System.Drawing.Size(307, 22)
        Me.FTEmail.TabIndex = 396
        Me.FTEmail.Tag = "2|"
        '
        'FTEmail_lbl
        '
        Me.FTEmail_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTEmail_lbl.Appearance.Options.UseForeColor = True
        Me.FTEmail_lbl.Appearance.Options.UseTextOptions = True
        Me.FTEmail_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTEmail_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTEmail_lbl.Location = New System.Drawing.Point(27, 233)
        Me.FTEmail_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTEmail_lbl.Name = "FTEmail_lbl"
        Me.FTEmail_lbl.Size = New System.Drawing.Size(189, 23)
        Me.FTEmail_lbl.TabIndex = 397
        Me.FTEmail_lbl.Tag = "2|"
        Me.FTEmail_lbl.Text = "E-Mail :"
        '
        'FTFax
        '
        Me.FTFax.Location = New System.Drawing.Point(218, 264)
        Me.FTFax.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTFax.Name = "FTFax"
        Me.FTFax.Properties.MaxLength = 50
        Me.FTFax.Size = New System.Drawing.Size(307, 22)
        Me.FTFax.TabIndex = 398
        Me.FTFax.Tag = "2|"
        '
        'FTFax_lbl
        '
        Me.FTFax_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTFax_lbl.Appearance.Options.UseForeColor = True
        Me.FTFax_lbl.Appearance.Options.UseTextOptions = True
        Me.FTFax_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTFax_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTFax_lbl.Location = New System.Drawing.Point(27, 264)
        Me.FTFax_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTFax_lbl.Name = "FTFax_lbl"
        Me.FTFax_lbl.Size = New System.Drawing.Size(189, 23)
        Me.FTFax_lbl.TabIndex = 399
        Me.FTFax_lbl.Tag = "2|"
        Me.FTFax_lbl.Text = "Tel. :"
        '
        'wUserChangePassword
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(651, 365)
        Me.Controls.Add(Me.ogbbutton)
        Me.Controls.Add(Me.ogbinfo)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "wUserChangePassword"
        Me.Text = "wUserChangePassword"
        CType(Me.ogbbutton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbbutton.ResumeLayout(False)
        CType(Me.ogbinfo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbinfo.ResumeLayout(False)
        CType(Me.otbpldpassword.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTPasswordRe.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTPassword.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTNickName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTTel.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTEmail.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTFax.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogbbutton As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmsave As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ogbinfo As DevExpress.XtraEditors.GroupControl
    Friend WithEvents otbpldpassword As DevExpress.XtraEditors.TextEdit
    Friend WithEvents otbpldpassword_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTPasswordRe As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FTPasswordRe_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTPassword As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FTPassword_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTFax As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FTFax_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTEmail As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FTEmail_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTTel As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FTTel_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTNickName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FTNickName_lbl As DevExpress.XtraEditors.LabelControl
End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class CopyPermission
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
        Me.ogbbutton = New DevExpress.XtraEditors.GroupControl()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmsave = New DevExpress.XtraEditors.SimpleButton()
        Me.ogbinfo = New DevExpress.XtraEditors.GroupControl()
        Me.FTPermissionNameEN = New DevExpress.XtraEditors.TextEdit()
        Me.FTPermissionNameEN_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTPermissionNameTH = New DevExpress.XtraEditors.TextEdit()
        Me.FTPermissionNameTH_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTPermissionCode = New DevExpress.XtraEditors.TextEdit()
        Me.FTPermissionCode_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.otboldpermissionname = New DevExpress.XtraEditors.TextEdit()
        Me.otbpldpassword_lbl = New DevExpress.XtraEditors.LabelControl()
        CType(Me.ogbbutton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbbutton.SuspendLayout()
        CType(Me.ogbinfo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbinfo.SuspendLayout()
        CType(Me.FTPermissionNameEN.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTPermissionNameTH.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTPermissionCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.otboldpermissionname.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ogbbutton
        '
        Me.ogbbutton.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbbutton.Controls.Add(Me.ocmexit)
        Me.ogbbutton.Controls.Add(Me.ocmsave)
        Me.ogbbutton.Location = New System.Drawing.Point(2, 170)
        Me.ogbbutton.Name = "ogbbutton"
        Me.ogbbutton.ShowCaption = False
        Me.ogbbutton.Size = New System.Drawing.Size(554, 42)
        Me.ogbbutton.TabIndex = 19
        Me.ogbbutton.Text = "GroupControl9"
        '
        'ocmexit
        '
        Me.ocmexit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmexit.Location = New System.Drawing.Point(298, 9)
        Me.ocmexit.Name = "ocmexit"
        Me.ocmexit.Size = New System.Drawing.Size(137, 25)
        Me.ocmexit.TabIndex = 103
        Me.ocmexit.TabStop = False
        Me.ocmexit.Tag = "2|"
        Me.ocmexit.Text = "EXIT"
        '
        'ocmsave
        '
        Me.ocmsave.Location = New System.Drawing.Point(106, 9)
        Me.ocmsave.Name = "ocmsave"
        Me.ocmsave.Size = New System.Drawing.Size(137, 25)
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
        Me.ogbinfo.Controls.Add(Me.FTPermissionNameEN)
        Me.ogbinfo.Controls.Add(Me.FTPermissionNameEN_lbl)
        Me.ogbinfo.Controls.Add(Me.FTPermissionNameTH)
        Me.ogbinfo.Controls.Add(Me.FTPermissionNameTH_lbl)
        Me.ogbinfo.Controls.Add(Me.FTPermissionCode)
        Me.ogbinfo.Controls.Add(Me.FTPermissionCode_lbl)
        Me.ogbinfo.Controls.Add(Me.otboldpermissionname)
        Me.ogbinfo.Controls.Add(Me.otbpldpassword_lbl)
        Me.ogbinfo.Location = New System.Drawing.Point(5, 3)
        Me.ogbinfo.Name = "ogbinfo"
        Me.ogbinfo.ShowCaption = False
        Me.ogbinfo.Size = New System.Drawing.Size(551, 161)
        Me.ogbinfo.TabIndex = 18
        Me.ogbinfo.Text = "User Infomation"
        '
        'FTPermissionNameEN
        '
        Me.FTPermissionNameEN.Location = New System.Drawing.Point(187, 102)
        Me.FTPermissionNameEN.Name = "FTPermissionNameEN"
        Me.FTPermissionNameEN.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FTPermissionNameEN.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FTPermissionNameEN.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FTPermissionNameEN.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FTPermissionNameEN.Properties.MaxLength = 200
        Me.FTPermissionNameEN.Size = New System.Drawing.Size(263, 20)
        Me.FTPermissionNameEN.TabIndex = 396
        Me.FTPermissionNameEN.Tag = "2|"
        '
        'FTPermissionNameEN_lbl
        '
        Me.FTPermissionNameEN_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTPermissionNameEN_lbl.Appearance.Options.UseForeColor = True
        Me.FTPermissionNameEN_lbl.Appearance.Options.UseTextOptions = True
        Me.FTPermissionNameEN_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTPermissionNameEN_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTPermissionNameEN_lbl.Location = New System.Drawing.Point(23, 102)
        Me.FTPermissionNameEN_lbl.Name = "FTPermissionNameEN_lbl"
        Me.FTPermissionNameEN_lbl.Size = New System.Drawing.Size(162, 19)
        Me.FTPermissionNameEN_lbl.TabIndex = 397
        Me.FTPermissionNameEN_lbl.Tag = "2|"
        Me.FTPermissionNameEN_lbl.Text = "Role Description EN :"
        '
        'FTPermissionNameTH
        '
        Me.FTPermissionNameTH.Location = New System.Drawing.Point(187, 80)
        Me.FTPermissionNameTH.Name = "FTPermissionNameTH"
        Me.FTPermissionNameTH.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FTPermissionNameTH.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FTPermissionNameTH.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FTPermissionNameTH.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FTPermissionNameTH.Properties.MaxLength = 200
        Me.FTPermissionNameTH.Size = New System.Drawing.Size(263, 20)
        Me.FTPermissionNameTH.TabIndex = 394
        Me.FTPermissionNameTH.Tag = "2|"
        '
        'FTPermissionNameTH_lbl
        '
        Me.FTPermissionNameTH_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTPermissionNameTH_lbl.Appearance.Options.UseForeColor = True
        Me.FTPermissionNameTH_lbl.Appearance.Options.UseTextOptions = True
        Me.FTPermissionNameTH_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTPermissionNameTH_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTPermissionNameTH_lbl.Location = New System.Drawing.Point(24, 80)
        Me.FTPermissionNameTH_lbl.Name = "FTPermissionNameTH_lbl"
        Me.FTPermissionNameTH_lbl.Size = New System.Drawing.Size(162, 19)
        Me.FTPermissionNameTH_lbl.TabIndex = 395
        Me.FTPermissionNameTH_lbl.Tag = "2|"
        Me.FTPermissionNameTH_lbl.Text = "Role Description TH :"
        '
        'FTPermissionCode
        '
        Me.FTPermissionCode.Location = New System.Drawing.Point(187, 57)
        Me.FTPermissionCode.Name = "FTPermissionCode"
        Me.FTPermissionCode.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FTPermissionCode.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FTPermissionCode.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FTPermissionCode.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FTPermissionCode.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.FTPermissionCode.Properties.MaxLength = 30
        Me.FTPermissionCode.Size = New System.Drawing.Size(196, 20)
        Me.FTPermissionCode.TabIndex = 392
        Me.FTPermissionCode.Tag = "2|"
        '
        'FTPermissionCode_lbl
        '
        Me.FTPermissionCode_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTPermissionCode_lbl.Appearance.Options.UseForeColor = True
        Me.FTPermissionCode_lbl.Appearance.Options.UseTextOptions = True
        Me.FTPermissionCode_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTPermissionCode_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTPermissionCode_lbl.Location = New System.Drawing.Point(55, 57)
        Me.FTPermissionCode_lbl.Name = "FTPermissionCode_lbl"
        Me.FTPermissionCode_lbl.Size = New System.Drawing.Size(130, 19)
        Me.FTPermissionCode_lbl.TabIndex = 393
        Me.FTPermissionCode_lbl.Tag = "2|"
        Me.FTPermissionCode_lbl.Text = "To Role Code :"
        '
        'otboldpermissionname
        '
        Me.otboldpermissionname.Location = New System.Drawing.Point(187, 19)
        Me.otboldpermissionname.Name = "otboldpermissionname"
        Me.otboldpermissionname.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.otboldpermissionname.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.otboldpermissionname.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.otboldpermissionname.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.otboldpermissionname.Properties.MaxLength = 16
        Me.otboldpermissionname.Properties.ReadOnly = True
        Me.otboldpermissionname.Size = New System.Drawing.Size(196, 20)
        Me.otboldpermissionname.TabIndex = 0
        Me.otboldpermissionname.Tag = "2|"
        '
        'otbpldpassword_lbl
        '
        Me.otbpldpassword_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.otbpldpassword_lbl.Appearance.Options.UseForeColor = True
        Me.otbpldpassword_lbl.Appearance.Options.UseTextOptions = True
        Me.otbpldpassword_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.otbpldpassword_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.otbpldpassword_lbl.Location = New System.Drawing.Point(22, 18)
        Me.otbpldpassword_lbl.Name = "otbpldpassword_lbl"
        Me.otbpldpassword_lbl.Size = New System.Drawing.Size(162, 19)
        Me.otbpldpassword_lbl.TabIndex = 391
        Me.otbpldpassword_lbl.Tag = "2|"
        Me.otbpldpassword_lbl.Text = "From Role Code :"
        '
        'CopyPermission
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(558, 216)
        Me.Controls.Add(Me.ogbbutton)
        Me.Controls.Add(Me.ogbinfo)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "CopyPermission"
        Me.Text = "Copy Permission"
        CType(Me.ogbbutton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbbutton.ResumeLayout(False)
        CType(Me.ogbinfo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbinfo.ResumeLayout(False)
        CType(Me.FTPermissionNameEN.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTPermissionNameTH.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTPermissionCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.otboldpermissionname.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogbbutton As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmsave As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ogbinfo As DevExpress.XtraEditors.GroupControl
    Friend WithEvents otboldpermissionname As DevExpress.XtraEditors.TextEdit
    Friend WithEvents otbpldpassword_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTPermissionNameEN As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FTPermissionNameEN_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTPermissionNameTH As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FTPermissionNameTH_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTPermissionCode As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FTPermissionCode_lbl As DevExpress.XtraEditors.LabelControl
End Class

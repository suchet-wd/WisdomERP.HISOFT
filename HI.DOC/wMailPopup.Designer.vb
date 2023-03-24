<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wMailPopup
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
        Me.ogrpmail = New DevExpress.XtraEditors.GroupControl()
        Me.ocmsend = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmcancel = New DevExpress.XtraEditors.SimpleButton()
        Me.FTMessange = New DevExpress.XtraEditors.MemoEdit()
        Me.FTSubJect = New DevExpress.XtraEditors.TextEdit()
        Me.FTMessange_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTSubJect_lbl = New DevExpress.XtraEditors.LabelControl()
        CType(Me.ogrpmail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogrpmail.SuspendLayout()
        CType(Me.FTMessange.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTSubJect.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ogrpmail
        '
        Me.ogrpmail.Controls.Add(Me.ocmsend)
        Me.ogrpmail.Controls.Add(Me.ocmcancel)
        Me.ogrpmail.Controls.Add(Me.FTMessange)
        Me.ogrpmail.Controls.Add(Me.FTSubJect)
        Me.ogrpmail.Controls.Add(Me.FTMessange_lbl)
        Me.ogrpmail.Controls.Add(Me.FTSubJect_lbl)
        Me.ogrpmail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogrpmail.Location = New System.Drawing.Point(0, 0)
        Me.ogrpmail.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogrpmail.Name = "ogrpmail"
        Me.ogrpmail.Size = New System.Drawing.Size(946, 484)
        Me.ogrpmail.TabIndex = 0
        Me.ogrpmail.Text = "Mail"
        '
        'ocmsend
        '
        Me.ocmsend.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmsend.Location = New System.Drawing.Point(750, 441)
        Me.ocmsend.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmsend.Name = "ocmsend"
        Me.ocmsend.Size = New System.Drawing.Size(87, 28)
        Me.ocmsend.TabIndex = 2
        Me.ocmsend.Text = "Send"
        '
        'ocmcancel
        '
        Me.ocmcancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmcancel.Location = New System.Drawing.Point(845, 441)
        Me.ocmcancel.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmcancel.Name = "ocmcancel"
        Me.ocmcancel.Size = New System.Drawing.Size(87, 28)
        Me.ocmcancel.TabIndex = 2
        Me.ocmcancel.Text = "Cancel"
        '
        'FTMessange
        '
        Me.FTMessange.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FTMessange.Location = New System.Drawing.Point(185, 63)
        Me.FTMessange.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTMessange.Name = "FTMessange"
        Me.FTMessange.Properties.MaxLength = 8000
        Me.FTMessange.Size = New System.Drawing.Size(747, 370)
        Me.FTMessange.TabIndex = 1
        Me.FTMessange.Tag = "2|"
        '
        'FTSubJect
        '
        Me.FTSubJect.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FTSubJect.Location = New System.Drawing.Point(185, 31)
        Me.FTSubJect.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTSubJect.Name = "FTSubJect"
        Me.FTSubJect.Properties.MaxLength = 500
        Me.FTSubJect.Size = New System.Drawing.Size(747, 23)
        Me.FTSubJect.TabIndex = 0
        Me.FTSubJect.Tag = "2|"
        '
        'FTMessange_lbl
        '
        Me.FTMessange_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTMessange_lbl.Appearance.Options.UseForeColor = True
        Me.FTMessange_lbl.Appearance.Options.UseTextOptions = True
        Me.FTMessange_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTMessange_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTMessange_lbl.LineColor = System.Drawing.Color.Blue
        Me.FTMessange_lbl.Location = New System.Drawing.Point(15, 55)
        Me.FTMessange_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTMessange_lbl.Name = "FTMessange_lbl"
        Me.FTMessange_lbl.Size = New System.Drawing.Size(163, 30)
        Me.FTMessange_lbl.TabIndex = 0
        Me.FTMessange_lbl.Text = "Messange :"
        '
        'FTSubJect_lbl
        '
        Me.FTSubJect_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTSubJect_lbl.Appearance.Options.UseForeColor = True
        Me.FTSubJect_lbl.Appearance.Options.UseTextOptions = True
        Me.FTSubJect_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTSubJect_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTSubJect_lbl.LineColor = System.Drawing.Color.Blue
        Me.FTSubJect_lbl.Location = New System.Drawing.Point(14, 30)
        Me.FTSubJect_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTSubJect_lbl.Name = "FTSubJect_lbl"
        Me.FTSubJect_lbl.Size = New System.Drawing.Size(163, 30)
        Me.FTSubJect_lbl.TabIndex = 0
        Me.FTSubJect_lbl.Text = "Subject :"
        '
        'wMailPopup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(946, 484)
        Me.Controls.Add(Me.ogrpmail)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "wMailPopup"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "wMailPopup"
        CType(Me.ogrpmail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogrpmail.ResumeLayout(False)
        CType(Me.FTMessange.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTSubJect.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogrpmail As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ocmsend As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmcancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FTMessange As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents FTSubJect As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FTMessange_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTSubJect_lbl As DevExpress.XtraEditors.LabelControl
End Class

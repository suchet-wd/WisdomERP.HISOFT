<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wInputUserAndPassword
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
        Me.ogcControl = New DevExpress.XtraEditors.GroupControl()
        Me.obtCancel = New DevExpress.XtraEditors.SimpleButton()
        Me.obtOk = New DevExpress.XtraEditors.SimpleButton()
        Me.txtmail_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.txtmail = New DevExpress.XtraEditors.TextEdit()
        Me.txtpassword = New DevExpress.XtraEditors.TextEdit()
        Me.txtpassword_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTStateReserve = New DevExpress.XtraEditors.CheckEdit()
        CType(Me.ogcControl, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogcControl.SuspendLayout()
        CType(Me.txtmail.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtpassword.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTStateReserve.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ogcControl
        '
        Me.ogcControl.Controls.Add(Me.obtCancel)
        Me.ogcControl.Controls.Add(Me.obtOk)
        Me.ogcControl.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ogcControl.Location = New System.Drawing.Point(0, 112)
        Me.ogcControl.Name = "ogcControl"
        Me.ogcControl.ShowCaption = False
        Me.ogcControl.Size = New System.Drawing.Size(491, 30)
        Me.ogcControl.TabIndex = 218
        '
        'obtCancel
        '
        Me.obtCancel.Location = New System.Drawing.Point(374, 3)
        Me.obtCancel.Name = "obtCancel"
        Me.obtCancel.Size = New System.Drawing.Size(102, 25)
        Me.obtCancel.TabIndex = 1
        Me.obtCancel.Tag = "1|Cancel|ยกเลิก"
        Me.obtCancel.Text = "Cancel"
        '
        'obtOk
        '
        Me.obtOk.Location = New System.Drawing.Point(265, 3)
        Me.obtOk.Name = "obtOk"
        Me.obtOk.Size = New System.Drawing.Size(102, 25)
        Me.obtOk.TabIndex = 0
        Me.obtOk.Tag = "1|Ok|ตกลง"
        Me.obtOk.Text = "Ok"
        '
        'txtmail_lbl
        '
        Me.txtmail_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.txtmail_lbl.Appearance.Options.UseForeColor = True
        Me.txtmail_lbl.Appearance.Options.UseTextOptions = True
        Me.txtmail_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.txtmail_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.txtmail_lbl.Location = New System.Drawing.Point(25, 12)
        Me.txtmail_lbl.Name = "txtmail_lbl"
        Me.txtmail_lbl.Size = New System.Drawing.Size(144, 17)
        Me.txtmail_lbl.TabIndex = 220
        Me.txtmail_lbl.Text = "Email Connect Nike :"
        '
        'txtmail
        '
        Me.txtmail.Location = New System.Drawing.Point(176, 11)
        Me.txtmail.Name = "txtmail"
        Me.txtmail.Size = New System.Drawing.Size(247, 20)
        Me.txtmail.TabIndex = 219
        '
        'txtpassword
        '
        Me.txtpassword.EditValue = ""
        Me.txtpassword.Location = New System.Drawing.Point(176, 37)
        Me.txtpassword.Name = "txtpassword"
        Me.txtpassword.Properties.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtpassword.Size = New System.Drawing.Size(247, 20)
        Me.txtpassword.TabIndex = 221
        '
        'txtpassword_lbl
        '
        Me.txtpassword_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.txtpassword_lbl.Appearance.Options.UseForeColor = True
        Me.txtpassword_lbl.Appearance.Options.UseTextOptions = True
        Me.txtpassword_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.txtpassword_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.txtpassword_lbl.Location = New System.Drawing.Point(26, 38)
        Me.txtpassword_lbl.Name = "txtpassword_lbl"
        Me.txtpassword_lbl.Size = New System.Drawing.Size(144, 17)
        Me.txtpassword_lbl.TabIndex = 222
        Me.txtpassword_lbl.Text = "Password :"
        '
        'FTStateReserve
        '
        Me.FTStateReserve.EditValue = "0"
        Me.FTStateReserve.Location = New System.Drawing.Point(176, 61)
        Me.FTStateReserve.Name = "FTStateReserve"
        Me.FTStateReserve.Properties.Caption = "Show Password"
        Me.FTStateReserve.Properties.ReadOnly = True
        Me.FTStateReserve.Properties.Tag = "FTStateLaser"
        Me.FTStateReserve.Properties.ValueChecked = "1"
        Me.FTStateReserve.Properties.ValueUnchecked = "0"
        Me.FTStateReserve.Size = New System.Drawing.Size(247, 21)
        Me.FTStateReserve.TabIndex = 476
        Me.FTStateReserve.Tag = "2|"
        '
        'wInputUserAndPassword
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(491, 142)
        Me.Controls.Add(Me.FTStateReserve)
        Me.Controls.Add(Me.txtpassword_lbl)
        Me.Controls.Add(Me.txtpassword)
        Me.Controls.Add(Me.txtmail_lbl)
        Me.Controls.Add(Me.txtmail)
        Me.Controls.Add(Me.ogcControl)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "wInputUserAndPassword"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Tag = "1|User && Password|User && Password"
        Me.Text = "User && Password"
        CType(Me.ogcControl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogcControl.ResumeLayout(False)
        CType(Me.txtmail.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtpassword.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTStateReserve.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogcControl As DevExpress.XtraEditors.GroupControl
    Friend WithEvents obtCancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents obtOk As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents txtmail_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtmail As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtpassword As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtpassword_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTStateReserve As DevExpress.XtraEditors.CheckEdit
End Class

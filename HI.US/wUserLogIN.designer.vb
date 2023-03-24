<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wUserLogIN
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
        Me.otbLogin = New DevExpress.XtraEditors.TextEdit()
        Me.otbPassword = New DevExpress.XtraEditors.TextEdit()
        CType(Me.otbLogin.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.otbPassword.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'otbLogin
        '
        Me.otbLogin.EditValue = ""
        Me.otbLogin.EnterMoveNextControl = True
        Me.otbLogin.Location = New System.Drawing.Point(162, 97)
        Me.otbLogin.Name = "otbLogin"
        Me.otbLogin.Properties.MaxLength = 50
        Me.otbLogin.Size = New System.Drawing.Size(150, 20)
        Me.otbLogin.TabIndex = 0
        '
        'otbPassword
        '
        Me.otbPassword.EditValue = ""
        Me.otbPassword.Location = New System.Drawing.Point(162, 121)
        Me.otbPassword.Name = "otbPassword"
        Me.otbPassword.Properties.MaxLength = 20
        Me.otbPassword.Properties.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.otbPassword.Size = New System.Drawing.Size(150, 20)
        Me.otbPassword.TabIndex = 1
        '
        'wUserLogIN
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(406, 190)
        Me.Controls.Add(Me.otbPassword)
        Me.Controls.Add(Me.otbLogin)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "wUserLogIN"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Login"
        CType(Me.otbLogin.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.otbPassword.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents otbLogin As DevExpress.XtraEditors.TextEdit
    Friend WithEvents otbPassword As DevExpress.XtraEditors.TextEdit
End Class

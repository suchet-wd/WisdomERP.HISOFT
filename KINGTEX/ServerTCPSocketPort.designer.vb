<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ServerTCPSocketPort
    Inherits System.Windows.Forms.Form

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
        Me.TReceive = New System.Windows.Forms.RichTextBox()
        Me.Timer1 = New System.Windows.Forms.Timer()
        Me.SuspendLayout()
        '
        'TReceive
        '
        Me.TReceive.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TReceive.BackColor = System.Drawing.Color.LightCyan
        Me.TReceive.ForeColor = System.Drawing.Color.Blue
        Me.TReceive.Location = New System.Drawing.Point(0, 0)
        Me.TReceive.Margin = New System.Windows.Forms.Padding(4)
        Me.TReceive.Name = "TReceive"
        Me.TReceive.ReadOnly = True
        Me.TReceive.Size = New System.Drawing.Size(872, 654)
        Me.TReceive.TabIndex = 0
        Me.TReceive.Text = ""
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        Me.Timer1.Interval = 1000
        '
        'ServerTCPSocketPort
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Black
        Me.ClientSize = New System.Drawing.Size(874, 654)
        Me.Controls.Add(Me.TReceive)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "ServerTCPSocketPort"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Receive Data From TCP"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TReceive As System.Windows.Forms.RichTextBox
    Friend WithEvents Timer1 As System.Windows.Forms.Timer

End Class

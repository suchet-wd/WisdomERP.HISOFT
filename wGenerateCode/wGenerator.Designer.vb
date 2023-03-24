<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wGenerator
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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
        Me.olb1 = New System.Windows.Forms.Label()
        Me.olb2 = New System.Windows.Forms.Label()
        Me.otb1 = New System.Windows.Forms.TextBox()
        Me.otb2 = New System.Windows.Forms.TextBox()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'olb1
        '
        Me.olb1.Location = New System.Drawing.Point(49, 81)
        Me.olb1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.olb1.Name = "olb1"
        Me.olb1.Size = New System.Drawing.Size(259, 23)
        Me.olb1.TabIndex = 0
        Me.olb1.Text = "ข้อมูลที่ต้องการเข้ารหัส"
        Me.olb1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'olb2
        '
        Me.olb2.Location = New System.Drawing.Point(49, 121)
        Me.olb2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.olb2.Name = "olb2"
        Me.olb2.Size = New System.Drawing.Size(259, 23)
        Me.olb2.TabIndex = 1
        Me.olb2.Text = "ข้อมูลที่ต้องการเข้ารหัสเรียบร้อยแล้ว"
        Me.olb2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'otb1
        '
        Me.otb1.Location = New System.Drawing.Point(316, 84)
        Me.otb1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.otb1.Name = "otb1"
        Me.otb1.Size = New System.Drawing.Size(324, 22)
        Me.otb1.TabIndex = 2
        '
        'otb2
        '
        Me.otb2.Location = New System.Drawing.Point(316, 119)
        Me.otb2.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.otb2.Name = "otb2"
        Me.otb2.Size = New System.Drawing.Size(324, 22)
        Me.otb2.TabIndex = 3
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(316, 42)
        Me.TextBox1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.TextBox1.Size = New System.Drawing.Size(324, 22)
        Me.TextBox1.TabIndex = 4
        '
        'wGenerator
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(727, 192)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.otb2)
        Me.Controls.Add(Me.otb1)
        Me.Controls.Add(Me.olb2)
        Me.Controls.Add(Me.olb1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "wGenerator"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Generator"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents olb1 As System.Windows.Forms.Label
    Friend WithEvents olb2 As System.Windows.Forms.Label
    Friend WithEvents otb1 As System.Windows.Forms.TextBox
    Friend WithEvents otb2 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox

End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ConvertPDF
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
        Me.otmcheckpo = New System.Windows.Forms.Timer()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.olbtotal = New System.Windows.Forms.Label()
        Me.olbcurrent = New System.Windows.Forms.Label()
        Me.olbpono = New System.Windows.Forms.Label()
        Me.olbBy = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'otmcheckpo
        '
        Me.otmcheckpo.Interval = 180000
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Blue
        Me.Label1.Location = New System.Drawing.Point(12, 49)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(98, 25)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Total PO"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Blue
        Me.Label2.Location = New System.Drawing.Point(12, 93)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(84, 25)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Current"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Blue
        Me.Label3.Location = New System.Drawing.Point(12, 141)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(81, 25)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "PO NO"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Blue
        Me.Label4.Location = New System.Drawing.Point(12, 189)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(37, 25)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "By"
        '
        'olbtotal
        '
        Me.olbtotal.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.olbtotal.ForeColor = System.Drawing.Color.DarkBlue
        Me.olbtotal.Location = New System.Drawing.Point(179, 49)
        Me.olbtotal.Name = "olbtotal"
        Me.olbtotal.Size = New System.Drawing.Size(268, 25)
        Me.olbtotal.TabIndex = 4
        Me.olbtotal.Text = "XX"
        '
        'olbcurrent
        '
        Me.olbcurrent.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.olbcurrent.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.olbcurrent.Location = New System.Drawing.Point(179, 93)
        Me.olbcurrent.Name = "olbcurrent"
        Me.olbcurrent.Size = New System.Drawing.Size(268, 25)
        Me.olbcurrent.TabIndex = 5
        Me.olbcurrent.Text = "XX"
        '
        'olbpono
        '
        Me.olbpono.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.olbpono.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.olbpono.Location = New System.Drawing.Point(179, 141)
        Me.olbpono.Name = "olbpono"
        Me.olbpono.Size = New System.Drawing.Size(268, 25)
        Me.olbpono.TabIndex = 6
        Me.olbpono.Text = "XX"
        '
        'olbBy
        '
        Me.olbBy.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.olbBy.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.olbBy.Location = New System.Drawing.Point(179, 189)
        Me.olbBy.Name = "olbBy"
        Me.olbBy.Size = New System.Drawing.Size(268, 25)
        Me.olbBy.TabIndex = 7
        Me.olbBy.Text = "XX"
        '
        'ConvertPDF
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(496, 290)
        Me.Controls.Add(Me.olbBy)
        Me.Controls.Add(Me.olbpono)
        Me.Controls.Add(Me.olbcurrent)
        Me.Controls.Add(Me.olbtotal)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.MaximumSize = New System.Drawing.Size(514, 337)
        Me.MinimumSize = New System.Drawing.Size(514, 337)
        Me.Name = "ConvertPDF"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Convert PO To PDF"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents otmcheckpo As Timer
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents olbtotal As Label
    Friend WithEvents olbcurrent As Label
    Friend WithEvents olbpono As Label
    Friend WithEvents olbBy As Label
End Class

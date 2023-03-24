<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wBarcodeAPI
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
        Me.ocmsave = New DevExpress.XtraEditors.SimpleButton()
        Me.SuspendLayout()
        '
        'ocmsave
        '
        Me.ocmsave.Location = New System.Drawing.Point(390, 315)
        Me.ocmsave.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmsave.Name = "ocmsave"
        Me.ocmsave.Size = New System.Drawing.Size(111, 31)
        Me.ocmsave.TabIndex = 94
        Me.ocmsave.TabStop = False
        Me.ocmsave.Tag = "2|"
        Me.ocmsave.Text = "SAVE"
        '
        'wBarcodeAPI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(890, 661)
        Me.Controls.Add(Me.ocmsave)
        Me.Name = "wBarcodeAPI"
        Me.Text = "wBarcodeAPI"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents ocmsave As DevExpress.XtraEditors.SimpleButton
End Class

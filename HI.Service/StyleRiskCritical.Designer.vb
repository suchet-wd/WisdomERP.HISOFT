<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class StyleRiskCritical
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
        Me.otpcritical = New DevExpress.XtraTab.XtraTabControl()
        CType(Me.otpcritical, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'otpcritical
        '
        Me.otpcritical.Dock = System.Windows.Forms.DockStyle.Fill
        Me.otpcritical.Location = New System.Drawing.Point(0, 0)
        Me.otpcritical.Name = "otpcritical"
        Me.otpcritical.Size = New System.Drawing.Size(393, 506)
        Me.otpcritical.TabIndex = 0
        '
        'StyleRiskCritical
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(393, 506)
        Me.Controls.Add(Me.otpcritical)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "StyleRiskCritical"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "StyleRiskCritical"
        CType(Me.otpcritical, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents otpcritical As DevExpress.XtraTab.XtraTabControl
End Class

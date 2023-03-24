<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wFormScreen
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
        Me.olbcmp = New DevExpress.XtraEditors.LabelControl()
        Me.olbhisoft = New DevExpress.XtraEditors.LabelControl()
        Me.SuspendLayout()
        '
        'olbcmp
        '
        Me.olbcmp.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.olbcmp.Appearance.Font = New System.Drawing.Font("Tahoma", 22.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.olbcmp.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.olbcmp.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.olbcmp.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.olbcmp.Location = New System.Drawing.Point(-3, 471)
        Me.olbcmp.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.olbcmp.Name = "olbcmp"
        Me.olbcmp.Size = New System.Drawing.Size(1179, 44)
        Me.olbcmp.TabIndex = 1
        Me.olbcmp.Text = "WISDOM SYSTEM"
        Me.olbcmp.Visible = False
        '
        'olbhisoft
        '
        Me.olbhisoft.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.olbhisoft.Appearance.Font = New System.Drawing.Font("AngsanaUPC", 50.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.olbhisoft.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.olbhisoft.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.olbhisoft.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.olbhisoft.Location = New System.Drawing.Point(-3, 356)
        Me.olbhisoft.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.olbhisoft.Name = "olbhisoft"
        Me.olbhisoft.Size = New System.Drawing.Size(1179, 108)
        Me.olbhisoft.TabIndex = 0
        Me.olbhisoft.Text = "WISDOM System"
        Me.olbhisoft.Visible = False
        '
        'wFormScreen
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1174, 650)
        Me.ControlBox = False
        Me.Controls.Add(Me.olbcmp)
        Me.Controls.Add(Me.olbhisoft)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "wFormScreen"
        Me.Text = "wFormScreen"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents olbcmp As DevExpress.XtraEditors.LabelControl
    Friend WithEvents olbhisoft As DevExpress.XtraEditors.LabelControl
End Class

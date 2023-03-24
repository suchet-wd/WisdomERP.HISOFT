<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class EfkaDisplayDataInfo
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
        Me.xtrascroll = New DevExpress.XtraEditors.XtraScrollableControl()
        Me.olbtop = New DevExpress.XtraEditors.LabelControl()
        Me.SuspendLayout()
        '
        'xtrascroll
        '
        Me.xtrascroll.Dock = System.Windows.Forms.DockStyle.Fill
        Me.xtrascroll.Location = New System.Drawing.Point(0, 41)
        Me.xtrascroll.Name = "xtrascroll"
        Me.xtrascroll.Size = New System.Drawing.Size(1277, 759)
        Me.xtrascroll.TabIndex = 1
        '
        'olbtop
        '
        Me.olbtop.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.olbtop.Appearance.Font = New System.Drawing.Font("Tahoma", 16.0!, System.Drawing.FontStyle.Bold)
        Me.olbtop.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.olbtop.Appearance.Options.UseBackColor = True
        Me.olbtop.Appearance.Options.UseFont = True
        Me.olbtop.Appearance.Options.UseForeColor = True
        Me.olbtop.Appearance.Options.UseTextOptions = True
        Me.olbtop.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.olbtop.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.olbtop.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.olbtop.Dock = System.Windows.Forms.DockStyle.Top
        Me.olbtop.Location = New System.Drawing.Point(0, 0)
        Me.olbtop.Name = "olbtop"
        Me.olbtop.Size = New System.Drawing.Size(1277, 41)
        Me.olbtop.TabIndex = 20
        Me.olbtop.Text = "Efka Data Info"
        '
        'EfkaDisplayDataInfo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1277, 800)
        Me.ControlBox = False
        Me.Controls.Add(Me.xtrascroll)
        Me.Controls.Add(Me.olbtop)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Name = "EfkaDisplayDataInfo"
        Me.Text = "Efka Data Info"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents xtrascroll As DevExpress.XtraEditors.XtraScrollableControl
    Friend WithEvents olbtop As DevExpress.XtraEditors.LabelControl
End Class

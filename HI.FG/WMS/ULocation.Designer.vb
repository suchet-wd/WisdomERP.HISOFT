<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ULocation
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Me.opn = New DevExpress.XtraEditors.PanelControl()
        Me.olbdesc = New DevExpress.XtraEditors.LabelControl()
        Me.olbloc = New DevExpress.XtraEditors.LabelControl()
        CType(Me.opn, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.opn.SuspendLayout()
        Me.SuspendLayout()
        '
        'opn
        '
        Me.opn.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.opn.Appearance.Options.UseBackColor = True
        Me.opn.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.opn.Controls.Add(Me.olbdesc)
        Me.opn.Controls.Add(Me.olbloc)
        Me.opn.Dock = System.Windows.Forms.DockStyle.Fill
        Me.opn.Location = New System.Drawing.Point(0, 0)
        Me.opn.Name = "opn"
        Me.opn.Size = New System.Drawing.Size(158, 55)
        Me.opn.TabIndex = 0
        '
        'olbdesc
        '
        Me.olbdesc.Appearance.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.olbdesc.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.olbdesc.Appearance.Options.UseFont = True
        Me.olbdesc.Appearance.Options.UseForeColor = True
        Me.olbdesc.Appearance.Options.UseTextOptions = True
        Me.olbdesc.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.olbdesc.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.olbdesc.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.olbdesc.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.olbdesc.Dock = System.Windows.Forms.DockStyle.Fill
        Me.olbdesc.Location = New System.Drawing.Point(0, 13)
        Me.olbdesc.Name = "olbdesc"
        Me.olbdesc.Size = New System.Drawing.Size(158, 42)
        Me.olbdesc.TabIndex = 1
        '
        'olbloc
        '
        Me.olbloc.Appearance.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Bold)
        Me.olbloc.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.olbloc.Appearance.Options.UseFont = True
        Me.olbloc.Appearance.Options.UseForeColor = True
        Me.olbloc.Appearance.Options.UseTextOptions = True
        Me.olbloc.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.olbloc.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.olbloc.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap
        Me.olbloc.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.olbloc.Dock = System.Windows.Forms.DockStyle.Top
        Me.olbloc.Location = New System.Drawing.Point(0, 0)
        Me.olbloc.Name = "olbloc"
        Me.olbloc.Size = New System.Drawing.Size(158, 13)
        Me.olbloc.TabIndex = 0
        Me.olbloc.Text = "A-010001"
        '
        'ULocation
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Controls.Add(Me.opn)
        Me.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Name = "ULocation"
        Me.Size = New System.Drawing.Size(158, 55)
        CType(Me.opn, System.ComponentModel.ISupportInitialize).EndInit()
        Me.opn.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Public WithEvents opn As DevExpress.XtraEditors.PanelControl
    Public WithEvents olbloc As DevExpress.XtraEditors.LabelControl
    Public WithEvents olbdesc As DevExpress.XtraEditors.LabelControl
End Class

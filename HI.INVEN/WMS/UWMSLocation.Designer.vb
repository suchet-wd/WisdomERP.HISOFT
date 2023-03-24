<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class UWMSLocation
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.opnheader = New DevExpress.XtraEditors.PanelControl()
        Me.olbdatestate = New DevExpress.XtraEditors.LabelControl()
        Me.olbheadloc = New DevExpress.XtraEditors.LabelControl()
        CType(Me.opnheader, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.opnheader.SuspendLayout()
        Me.SuspendLayout()
        '
        'opnheader
        '
        Me.opnheader.Appearance.BackColor = System.Drawing.Color.Teal
        Me.opnheader.Appearance.Options.UseBackColor = True
        Me.opnheader.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.opnheader.Controls.Add(Me.olbdatestate)
        Me.opnheader.Controls.Add(Me.olbheadloc)
        Me.opnheader.Dock = System.Windows.Forms.DockStyle.Top
        Me.opnheader.Location = New System.Drawing.Point(0, 0)
        Me.opnheader.Name = "opnheader"
        Me.opnheader.Size = New System.Drawing.Size(193, 48)
        Me.opnheader.TabIndex = 0
        '
        'olbdatestate
        '
        Me.olbdatestate.Appearance.BackColor = System.Drawing.Color.Blue
        Me.olbdatestate.Appearance.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Bold)
        Me.olbdatestate.Appearance.ForeColor = System.Drawing.Color.Yellow
        Me.olbdatestate.Appearance.Options.UseBackColor = True
        Me.olbdatestate.Appearance.Options.UseFont = True
        Me.olbdatestate.Appearance.Options.UseForeColor = True
        Me.olbdatestate.Appearance.Options.UseTextOptions = True
        Me.olbdatestate.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.olbdatestate.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.olbdatestate.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.olbdatestate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.olbdatestate.Location = New System.Drawing.Point(0, 32)
        Me.olbdatestate.Name = "olbdatestate"
        Me.olbdatestate.Size = New System.Drawing.Size(193, 16)
        Me.olbdatestate.TabIndex = 2
        Me.olbdatestate.Text = "Press for Start"
        '
        'olbheadloc
        '
        Me.olbheadloc.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold)
        Me.olbheadloc.Appearance.ForeColor = System.Drawing.Color.Yellow
        Me.olbheadloc.Appearance.Options.UseFont = True
        Me.olbheadloc.Appearance.Options.UseForeColor = True
        Me.olbheadloc.Appearance.Options.UseTextOptions = True
        Me.olbheadloc.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.olbheadloc.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.olbheadloc.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap
        Me.olbheadloc.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.olbheadloc.Dock = System.Windows.Forms.DockStyle.Top
        Me.olbheadloc.Location = New System.Drawing.Point(0, 0)
        Me.olbheadloc.Name = "olbheadloc"
        Me.olbheadloc.Size = New System.Drawing.Size(193, 32)
        Me.olbheadloc.TabIndex = 1
        Me.olbheadloc.Text = "A-010001"
        '
        'UWMSLocation
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Controls.Add(Me.opnheader)
        Me.Name = "UWMSLocation"
        Me.Size = New System.Drawing.Size(193, 732)
        CType(Me.opnheader, System.ComponentModel.ISupportInitialize).EndInit()
        Me.opnheader.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Public WithEvents opnheader As DevExpress.XtraEditors.PanelControl
    Public WithEvents olbheadloc As DevExpress.XtraEditors.LabelControl
    Friend WithEvents olbdatestate As DevExpress.XtraEditors.LabelControl
End Class

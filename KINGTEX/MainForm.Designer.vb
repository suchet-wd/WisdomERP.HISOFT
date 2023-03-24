<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainForm
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
        Me.ocmcomport = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmtcpip = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmtcpipsocket = New DevExpress.XtraEditors.SimpleButton()
        Me.SuspendLayout()
        '
        'ocmcomport
        '
        Me.ocmcomport.Appearance.Font = New System.Drawing.Font("Tahoma", 16.0!, System.Drawing.FontStyle.Bold)
        Me.ocmcomport.Appearance.Options.UseFont = True
        Me.ocmcomport.Location = New System.Drawing.Point(46, 31)
        Me.ocmcomport.Name = "ocmcomport"
        Me.ocmcomport.Size = New System.Drawing.Size(368, 80)
        Me.ocmcomport.TabIndex = 0
        Me.ocmcomport.Text = "Comport"
        '
        'ocmtcpip
        '
        Me.ocmtcpip.Appearance.Font = New System.Drawing.Font("Tahoma", 16.0!, System.Drawing.FontStyle.Bold)
        Me.ocmtcpip.Appearance.Options.UseFont = True
        Me.ocmtcpip.Location = New System.Drawing.Point(46, 158)
        Me.ocmtcpip.Name = "ocmtcpip"
        Me.ocmtcpip.Size = New System.Drawing.Size(368, 80)
        Me.ocmtcpip.TabIndex = 1
        Me.ocmtcpip.Text = "TCP/IP"
        '
        'ocmexit
        '
        Me.ocmexit.Appearance.Font = New System.Drawing.Font("Tahoma", 16.0!, System.Drawing.FontStyle.Bold)
        Me.ocmexit.Appearance.Options.UseFont = True
        Me.ocmexit.Location = New System.Drawing.Point(46, 400)
        Me.ocmexit.Name = "ocmexit"
        Me.ocmexit.Size = New System.Drawing.Size(368, 80)
        Me.ocmexit.TabIndex = 2
        Me.ocmexit.Text = "Exit"
        '
        'ocmtcpipsocket
        '
        Me.ocmtcpipsocket.Appearance.Font = New System.Drawing.Font("Tahoma", 16.0!, System.Drawing.FontStyle.Bold)
        Me.ocmtcpipsocket.Appearance.Options.UseFont = True
        Me.ocmtcpipsocket.Location = New System.Drawing.Point(46, 281)
        Me.ocmtcpipsocket.Name = "ocmtcpipsocket"
        Me.ocmtcpipsocket.Size = New System.Drawing.Size(368, 80)
        Me.ocmtcpipsocket.TabIndex = 3
        Me.ocmtcpipsocket.Text = "TCP/IP SOCKET"
        '
        'MainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(462, 589)
        Me.ControlBox = False
        Me.Controls.Add(Me.ocmtcpipsocket)
        Me.Controls.Add(Me.ocmexit)
        Me.Controls.Add(Me.ocmtcpip)
        Me.Controls.Add(Me.ocmcomport)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "MainForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "MainForm"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents ocmcomport As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmtcpip As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmtcpipsocket As DevExpress.XtraEditors.SimpleButton
End Class

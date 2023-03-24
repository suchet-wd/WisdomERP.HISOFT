<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wQAPreFinalCheckPoint
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
        Me.ogrpSubmenu = New DevExpress.XtraEditors.GroupControl()
        Me.TileControl = New DevExpress.XtraEditors.TileControl()
        Me.ocmclose = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.ogrpSubmenu, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogrpSubmenu.SuspendLayout()
        Me.SuspendLayout()
        '
        'ogrpSubmenu
        '
        Me.ogrpSubmenu.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogrpSubmenu.Controls.Add(Me.TileControl)
        Me.ogrpSubmenu.Location = New System.Drawing.Point(0, 48)
        Me.ogrpSubmenu.Name = "ogrpSubmenu"
        Me.ogrpSubmenu.ShowCaption = False
        Me.ogrpSubmenu.Size = New System.Drawing.Size(941, 525)
        Me.ogrpSubmenu.TabIndex = 3
        Me.ogrpSubmenu.Text = "Sub Menu"
        '
        'TileControl
        '
        Me.TileControl.BackColor = System.Drawing.Color.DarkGray
        Me.TileControl.Cursor = System.Windows.Forms.Cursors.Default
        Me.TileControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TileControl.ItemSize = 70
        Me.TileControl.Location = New System.Drawing.Point(2, 2)
        Me.TileControl.MaxId = 25
        Me.TileControl.Name = "TileControl"
        Me.TileControl.Orientation = System.Windows.Forms.Orientation.Vertical
        Me.TileControl.ScrollMode = DevExpress.XtraEditors.TileControlScrollMode.ScrollBar
        Me.TileControl.Size = New System.Drawing.Size(937, 521)
        Me.TileControl.TabIndex = 0
        Me.TileControl.Text = "TileControlSubmenu"
        '
        'ocmclose
        '
        Me.ocmclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmclose.Location = New System.Drawing.Point(750, 7)
        Me.ocmclose.Name = "ocmclose"
        Me.ocmclose.Size = New System.Drawing.Size(185, 33)
        Me.ocmclose.TabIndex = 97
        Me.ocmclose.TabStop = False
        Me.ocmclose.Tag = "2|"
        Me.ocmclose.Text = "CLOSE"
        '
        'wQAPreFinalCheckPoint
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(945, 575)
        Me.Controls.Add(Me.ocmclose)
        Me.Controls.Add(Me.ogrpSubmenu)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "wQAPreFinalCheckPoint"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "QA Pre-Final CheckPoint"
        CType(Me.ogrpSubmenu, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogrpSubmenu.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogrpSubmenu As DevExpress.XtraEditors.GroupControl
    Friend WithEvents TileControl As DevExpress.XtraEditors.TileControl
    Friend WithEvents ocmclose As DevExpress.XtraEditors.SimpleButton
End Class

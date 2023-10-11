<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wQAPreFinalSamplePopup
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
        Me.ogrpInline = New DevExpress.XtraEditors.GroupControl()
        Me.ogrpSubmenu = New DevExpress.XtraEditors.GroupControl()
        Me.TileControl = New DevExpress.XtraEditors.TileControl()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.ogrpDefect = New DevExpress.XtraEditors.GroupControl()
        Me.obtClose = New DevExpress.XtraEditors.SimpleButton()
        Me.obtSelect = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.ogrpInline, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogrpInline.SuspendLayout()
        CType(Me.ogrpSubmenu, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogrpSubmenu.SuspendLayout()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogrpDefect, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogrpDefect.SuspendLayout()
        Me.SuspendLayout()
        '
        'ogrpInline
        '
        Me.ogrpInline.Controls.Add(Me.ogrpSubmenu)
        Me.ogrpInline.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogrpInline.Location = New System.Drawing.Point(0, 0)
        Me.ogrpInline.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogrpInline.Name = "ogrpInline"
        Me.ogrpInline.Size = New System.Drawing.Size(1614, 792)
        Me.ogrpInline.TabIndex = 2
        Me.ogrpInline.Text = "Qty Check Per PCS"
        '
        'ogrpSubmenu
        '
        Me.ogrpSubmenu.Controls.Add(Me.TileControl)
        Me.ogrpSubmenu.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogrpSubmenu.Location = New System.Drawing.Point(2, 27)
        Me.ogrpSubmenu.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogrpSubmenu.Name = "ogrpSubmenu"
        Me.ogrpSubmenu.ShowCaption = False
        Me.ogrpSubmenu.Size = New System.Drawing.Size(1610, 763)
        Me.ogrpSubmenu.TabIndex = 3
        Me.ogrpSubmenu.Text = "Sub Menu"
        '
        'TileControl
        '
        Me.TileControl.BackColor = System.Drawing.Color.DarkGray
        Me.TileControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TileControl.Location = New System.Drawing.Point(2, 2)
        Me.TileControl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.TileControl.Name = "TileControl"
        Me.TileControl.Padding = New System.Windows.Forms.Padding(21, 22, 21, 22)
        Me.TileControl.ScrollMode = DevExpress.XtraEditors.TileControlScrollMode.ScrollBar
        Me.TileControl.Size = New System.Drawing.Size(1606, 759)
        Me.TileControl.TabIndex = 1
        Me.TileControl.Text = "TileControl1"
        '
        'GridView1
        '
        Me.GridView1.Name = "GridView1"
        '
        'ogrpDefect
        '
        Me.ogrpDefect.Controls.Add(Me.obtClose)
        Me.ogrpDefect.Controls.Add(Me.obtSelect)
        Me.ogrpDefect.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ogrpDefect.Location = New System.Drawing.Point(0, 792)
        Me.ogrpDefect.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogrpDefect.Name = "ogrpDefect"
        Me.ogrpDefect.ShowCaption = False
        Me.ogrpDefect.Size = New System.Drawing.Size(1614, 86)
        Me.ogrpDefect.TabIndex = 1
        Me.ogrpDefect.Text = "Defect"
        '
        'obtClose
        '
        Me.obtClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.obtClose.Location = New System.Drawing.Point(1504, 29)
        Me.obtClose.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.obtClose.Name = "obtClose"
        Me.obtClose.Size = New System.Drawing.Size(87, 28)
        Me.obtClose.TabIndex = 2
        Me.obtClose.Text = "CLOSE"
        '
        'obtSelect
        '
        Me.obtSelect.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.obtSelect.Location = New System.Drawing.Point(1410, 29)
        Me.obtSelect.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.obtSelect.Name = "obtSelect"
        Me.obtSelect.Size = New System.Drawing.Size(87, 28)
        Me.obtSelect.TabIndex = 3
        Me.obtSelect.Text = "Select"
        Me.obtSelect.Visible = False
        '
        'wQAPreFinalSamplePopup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1614, 878)
        Me.Controls.Add(Me.ogrpInline)
        Me.Controls.Add(Me.ogrpDefect)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "wQAPreFinalSamplePopup"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = " "
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.ogrpInline, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogrpInline.ResumeLayout(False)
        CType(Me.ogrpSubmenu, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogrpSubmenu.ResumeLayout(False)
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogrpDefect, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogrpDefect.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogrpInline As DevExpress.XtraEditors.GroupControl
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents ogrpDefect As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ogrpSubmenu As DevExpress.XtraEditors.GroupControl
    Friend WithEvents TileControl As DevExpress.XtraEditors.TileControl
    Friend WithEvents obtClose As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents obtSelect As DevExpress.XtraEditors.SimpleButton
End Class

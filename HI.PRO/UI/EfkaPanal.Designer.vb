<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class EfkaPanal
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
        Me.components = New System.ComponentModel.Container()
        Me.Timchecker = New System.Windows.Forms.Timer(Me.components)
        Me.uXtraScrollable = New DevExpress.XtraEditors.XtraScrollableControl()
        Me.pn1 = New DevExpress.XtraEditors.PanelControl()
        Me.pn2 = New DevExpress.XtraEditors.PanelControl()
        Me.pn3 = New DevExpress.XtraEditors.PanelControl()
        Me.pn4 = New DevExpress.XtraEditors.PanelControl()
        Me.pn5 = New DevExpress.XtraEditors.PanelControl()
        Me.pn6 = New DevExpress.XtraEditors.PanelControl()
        Me.uXtraScrollable.SuspendLayout()
        CType(Me.pn1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pn2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pn3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pn4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pn5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pn6, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Timchecker
        '
        Me.Timchecker.Interval = 1000
        '
        'uXtraScrollable
        '
        Me.uXtraScrollable.Controls.Add(Me.pn6)
        Me.uXtraScrollable.Controls.Add(Me.pn5)
        Me.uXtraScrollable.Controls.Add(Me.pn4)
        Me.uXtraScrollable.Controls.Add(Me.pn3)
        Me.uXtraScrollable.Controls.Add(Me.pn2)
        Me.uXtraScrollable.Controls.Add(Me.pn1)
        Me.uXtraScrollable.Dock = System.Windows.Forms.DockStyle.Fill
        Me.uXtraScrollable.Location = New System.Drawing.Point(0, 0)
        Me.uXtraScrollable.Name = "uXtraScrollable"
        Me.uXtraScrollable.Size = New System.Drawing.Size(1999, 367)
        Me.uXtraScrollable.TabIndex = 0
        '
        'pn1
        '
        Me.pn1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.pn1.Dock = System.Windows.Forms.DockStyle.Left
        Me.pn1.Location = New System.Drawing.Point(0, 0)
        Me.pn1.Name = "pn1"
        Me.pn1.Size = New System.Drawing.Size(338, 346)
        Me.pn1.TabIndex = 0
        '
        'pn2
        '
        Me.pn2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.pn2.Dock = System.Windows.Forms.DockStyle.Left
        Me.pn2.Location = New System.Drawing.Point(338, 0)
        Me.pn2.Name = "pn2"
        Me.pn2.Size = New System.Drawing.Size(338, 346)
        Me.pn2.TabIndex = 1
        '
        'pn3
        '
        Me.pn3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.pn3.Dock = System.Windows.Forms.DockStyle.Left
        Me.pn3.Location = New System.Drawing.Point(676, 0)
        Me.pn3.Name = "pn3"
        Me.pn3.Size = New System.Drawing.Size(338, 346)
        Me.pn3.TabIndex = 2
        '
        'pn4
        '
        Me.pn4.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.pn4.Dock = System.Windows.Forms.DockStyle.Left
        Me.pn4.Location = New System.Drawing.Point(1014, 0)
        Me.pn4.Name = "pn4"
        Me.pn4.Size = New System.Drawing.Size(338, 346)
        Me.pn4.TabIndex = 3
        '
        'pn5
        '
        Me.pn5.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.pn5.Dock = System.Windows.Forms.DockStyle.Left
        Me.pn5.Location = New System.Drawing.Point(1352, 0)
        Me.pn5.Name = "pn5"
        Me.pn5.Size = New System.Drawing.Size(338, 346)
        Me.pn5.TabIndex = 4
        '
        'pn6
        '
        Me.pn6.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.pn6.Dock = System.Windows.Forms.DockStyle.Left
        Me.pn6.Location = New System.Drawing.Point(1690, 0)
        Me.pn6.Name = "pn6"
        Me.pn6.Size = New System.Drawing.Size(338, 346)
        Me.pn6.TabIndex = 5
        '
        'EfkaPanal
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.uXtraScrollable)
        Me.Name = "EfkaPanal"
        Me.Size = New System.Drawing.Size(1999, 367)
        Me.uXtraScrollable.ResumeLayout(False)
        CType(Me.pn1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pn2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pn3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pn4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pn5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pn6, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Timchecker As System.Windows.Forms.Timer
    Friend WithEvents uXtraScrollable As DevExpress.XtraEditors.XtraScrollableControl
    Friend WithEvents pn1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents pn6 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents pn5 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents pn4 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents pn3 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents pn2 As DevExpress.XtraEditors.PanelControl
End Class

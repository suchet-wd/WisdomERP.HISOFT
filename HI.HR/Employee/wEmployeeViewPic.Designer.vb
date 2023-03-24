<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wEmployeeViewPic
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
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.FTEmpPicName = New DevExpress.XtraEditors.PictureEdit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.FTEmpPicName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupControl1
        '
        Me.GroupControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupControl1.Controls.Add(Me.FTEmpPicName)
        Me.GroupControl1.Location = New System.Drawing.Point(3, 4)
        Me.GroupControl1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(633, 519)
        Me.GroupControl1.TabIndex = 1
        '
        'FTEmpPicName
        '
        Me.FTEmpPicName.Cursor = System.Windows.Forms.Cursors.Default
        Me.FTEmpPicName.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FTEmpPicName.Location = New System.Drawing.Point(2, 25)
        Me.FTEmpPicName.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTEmpPicName.Name = "FTEmpPicName"
        Me.FTEmpPicName.Properties.ErrorImage = Nothing
        Me.FTEmpPicName.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch
        Me.FTEmpPicName.Properties.Tag = "Employee\"
        Me.FTEmpPicName.Properties.ZoomAccelerationFactor = 1.0R
        Me.FTEmpPicName.Size = New System.Drawing.Size(629, 492)
        Me.FTEmpPicName.TabIndex = 314
        Me.FTEmpPicName.Tag = "2|"
        '
        'wEmployeeViewPic
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(639, 526)
        Me.Controls.Add(Me.GroupControl1)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "wEmployeeViewPic"
        Me.Text = "Employee View Picture"
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        CType(Me.FTEmpPicName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents FTEmpPicName As DevExpress.XtraEditors.PictureEdit
End Class

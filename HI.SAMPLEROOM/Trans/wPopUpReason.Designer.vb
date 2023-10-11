<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wPopUpReason
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
        Me.ogbStyleHeader = New DevExpress.XtraEditors.GroupControl()
        Me.FTRemark = New DevExpress.XtraEditors.MemoEdit()
        Me.otbsave = New DevExpress.XtraEditors.SimpleButton()
        Me.otbclose = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.ogbStyleHeader, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbStyleHeader.SuspendLayout()
        CType(Me.FTRemark.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ogbStyleHeader
        '
        Me.ogbStyleHeader.Controls.Add(Me.FTRemark)
        Me.ogbStyleHeader.Controls.Add(Me.otbsave)
        Me.ogbStyleHeader.Controls.Add(Me.otbclose)
        Me.ogbStyleHeader.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogbStyleHeader.Location = New System.Drawing.Point(0, 0)
        Me.ogbStyleHeader.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbStyleHeader.Name = "ogbStyleHeader"
        Me.ogbStyleHeader.ShowCaption = False
        Me.ogbStyleHeader.Size = New System.Drawing.Size(722, 352)
        Me.ogbStyleHeader.TabIndex = 2
        Me.ogbStyleHeader.Text = "Style Info"
        '
        'FTRemark
        '
        Me.FTRemark.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FTRemark.Location = New System.Drawing.Point(5, 5)
        Me.FTRemark.Name = "FTRemark"
        Me.FTRemark.Properties.MaxLength = 500
        Me.FTRemark.Size = New System.Drawing.Size(712, 297)
        Me.FTRemark.TabIndex = 114
        '
        'otbsave
        '
        Me.otbsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.otbsave.Location = New System.Drawing.Point(476, 309)
        Me.otbsave.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.otbsave.Name = "otbsave"
        Me.otbsave.Size = New System.Drawing.Size(114, 30)
        Me.otbsave.TabIndex = 113
        Me.otbsave.Text = "Save"
        '
        'otbclose
        '
        Me.otbclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.otbclose.Location = New System.Drawing.Point(596, 309)
        Me.otbclose.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.otbclose.Name = "otbclose"
        Me.otbclose.Size = New System.Drawing.Size(111, 31)
        Me.otbclose.TabIndex = 96
        Me.otbclose.TabStop = False
        Me.otbclose.Tag = "2|"
        Me.otbclose.Text = "EXIT"
        '
        'wPopUpReason
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(722, 352)
        Me.Controls.Add(Me.ogbStyleHeader)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "wPopUpReason"
        Me.Text = "wPopUpReason"
        CType(Me.ogbStyleHeader, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbStyleHeader.ResumeLayout(False)
        CType(Me.FTRemark.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogbStyleHeader As DevExpress.XtraEditors.GroupControl
    Friend WithEvents otbclose As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents otbsave As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FTRemark As DevExpress.XtraEditors.MemoEdit
End Class

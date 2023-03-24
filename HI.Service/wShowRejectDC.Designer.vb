<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wShowRejectDC
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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
        Me.MedReason = New DevExpress.XtraEditors.MemoEdit()
        Me.SBtnOK = New DevExpress.XtraEditors.SimpleButton()
        Me.SBtnExit = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.MedReason.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'MedReason
        '
        Me.MedReason.Location = New System.Drawing.Point(12, 13)
        Me.MedReason.Name = "MedReason"
        Me.MedReason.Size = New System.Drawing.Size(474, 187)
        Me.MedReason.TabIndex = 2
        Me.MedReason.UseOptimizedRendering = True
        '
        'SBtnOK
        '
        Me.SBtnOK.Location = New System.Drawing.Point(127, 214)
        Me.SBtnOK.Name = "SBtnOK"
        Me.SBtnOK.Size = New System.Drawing.Size(72, 27)
        Me.SBtnOK.TabIndex = 3
        Me.SBtnOK.Text = "OK"
        '
        'SBtnExit
        '
        Me.SBtnExit.Location = New System.Drawing.Point(276, 214)
        Me.SBtnExit.Name = "SBtnExit"
        Me.SBtnExit.Size = New System.Drawing.Size(72, 27)
        Me.SBtnExit.TabIndex = 4
        Me.SBtnExit.Text = "Exit"
        '
        'wShowRejectDC
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(498, 257)
        Me.Controls.Add(Me.SBtnExit)
        Me.Controls.Add(Me.SBtnOK)
        Me.Controls.Add(Me.MedReason)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "wShowRejectDC"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Reason "
        CType(Me.MedReason.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents MedReason As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents SBtnOK As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents SBtnExit As DevExpress.XtraEditors.SimpleButton
End Class

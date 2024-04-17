<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wInputReason
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
        Me.ogcControl = New DevExpress.XtraEditors.GroupControl()
        Me.obtCancel = New DevExpress.XtraEditors.SimpleButton()
        Me.obtOk = New DevExpress.XtraEditors.SimpleButton()
        Me.otbCancelReason = New DevExpress.XtraEditors.MemoEdit()
        CType(Me.ogcControl, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogcControl.SuspendLayout()
        CType(Me.otbCancelReason.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ogcControl
        '
        Me.ogcControl.Controls.Add(Me.obtCancel)
        Me.ogcControl.Controls.Add(Me.obtOk)
        Me.ogcControl.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ogcControl.Location = New System.Drawing.Point(0, 204)
        Me.ogcControl.Name = "ogcControl"
        Me.ogcControl.ShowCaption = False
        Me.ogcControl.Size = New System.Drawing.Size(448, 30)
        Me.ogcControl.TabIndex = 218
        '
        'obtCancel
        '
        Me.obtCancel.Location = New System.Drawing.Point(327, 3)
        Me.obtCancel.Name = "obtCancel"
        Me.obtCancel.Size = New System.Drawing.Size(102, 25)
        Me.obtCancel.TabIndex = 1
        Me.obtCancel.Tag = "1|Cancel|ยกเลิก"
        Me.obtCancel.Text = "Cancel"
        '
        'obtOk
        '
        Me.obtOk.Location = New System.Drawing.Point(218, 3)
        Me.obtOk.Name = "obtOk"
        Me.obtOk.Size = New System.Drawing.Size(102, 25)
        Me.obtOk.TabIndex = 0
        Me.obtOk.Tag = "1|Ok|ตกลง"
        Me.obtOk.Text = "Ok"
        '
        'otbCancelReason
        '
        Me.otbCancelReason.Dock = System.Windows.Forms.DockStyle.Fill
        Me.otbCancelReason.Location = New System.Drawing.Point(0, 0)
        Me.otbCancelReason.Name = "otbCancelReason"
        Me.otbCancelReason.Size = New System.Drawing.Size(448, 204)
        Me.otbCancelReason.TabIndex = 0
        '
        'wInputReason
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(448, 234)
        Me.Controls.Add(Me.otbCancelReason)
        Me.Controls.Add(Me.ogcControl)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "wInputReason"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Tag = "1|Reason|Reason"
        Me.Text = "Reason"
        CType(Me.ogcControl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogcControl.ResumeLayout(False)
        CType(Me.otbCancelReason.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogcControl As DevExpress.XtraEditors.GroupControl
    Friend WithEvents obtCancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents obtOk As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents otbCancelReason As DevExpress.XtraEditors.MemoEdit
End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wFormSet_FileName
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
        Me.description_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTPath_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTPathName = New DevExpress.XtraEditors.ButtonEdit()
        Me.obtcancel = New DevExpress.XtraEditors.SimpleButton()
        Me.obtnext = New DevExpress.XtraEditors.SimpleButton()
        Me.OpenFileDialog = New System.Windows.Forms.OpenFileDialog()
        CType(Me.FTPathName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'description_lbl
        '
        Me.description_lbl.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.description_lbl.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top
        Me.description_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.description_lbl.Location = New System.Drawing.Point(22, 12)
        Me.description_lbl.Name = "description_lbl"
        Me.description_lbl.Size = New System.Drawing.Size(786, 175)
        Me.description_lbl.TabIndex = 0
        Me.description_lbl.Text = "โปรดตรวจสอบไฟล์  หรือเลือกไฟล์ใหม่  ในกรณีที่ไฟล์ ที่จะ Backup ไม่ถูกต้อง" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "กดปุ่ม" & _
    " Next เพื่อทำการ Sync Data ต่อไป  หรือ  กดปุ่ม Cancel  เพื่อยกเลิก การ Sync Data" & _
    "."
        '
        'FTPath_lbl
        '
        Me.FTPath_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTPath_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTPath_lbl.Location = New System.Drawing.Point(33, 194)
        Me.FTPath_lbl.Name = "FTPath_lbl"
        Me.FTPath_lbl.Size = New System.Drawing.Size(185, 13)
        Me.FTPath_lbl.TabIndex = 1
        Me.FTPath_lbl.Text = "From File Name :"
        '
        'FTPathName
        '
        Me.FTPathName.Location = New System.Drawing.Point(225, 193)
        Me.FTPathName.Name = "FTPathName"
        Me.FTPathName.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.FTPathName.Size = New System.Drawing.Size(502, 20)
        Me.FTPathName.TabIndex = 2
        '
        'obtcancel
        '
        Me.obtcancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.obtcancel.Location = New System.Drawing.Point(652, 221)
        Me.obtcancel.Name = "obtcancel"
        Me.obtcancel.Size = New System.Drawing.Size(75, 23)
        Me.obtcancel.TabIndex = 3
        Me.obtcancel.Text = "Cancel"
        '
        'obtnext
        '
        Me.obtnext.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.obtnext.Location = New System.Drawing.Point(733, 221)
        Me.obtnext.Name = "obtnext"
        Me.obtnext.Size = New System.Drawing.Size(75, 23)
        Me.obtnext.TabIndex = 3
        Me.obtnext.Text = "Next"
        '
        'OpenFileDialog
        '
        Me.OpenFileDialog.FileName = "OpenFileDialog"
        '
        'wFormSet_FileName
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(824, 256)
        Me.Controls.Add(Me.obtnext)
        Me.Controls.Add(Me.obtcancel)
        Me.Controls.Add(Me.FTPathName)
        Me.Controls.Add(Me.FTPath_lbl)
        Me.Controls.Add(Me.description_lbl)
        Me.FormBorderEffect = DevExpress.XtraEditors.FormBorderEffect.Shadow
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "wFormSet_FileName"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "d"
        CType(Me.FTPathName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents description_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTPath_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTPathName As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents obtcancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents obtnext As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents OpenFileDialog As System.Windows.Forms.OpenFileDialog
End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wFormSetPathFile
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
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
        Me.FTPathName = New DevExpress.XtraEditors.ButtonEdit()
        Me.obtcancel = New DevExpress.XtraEditors.SimpleButton()
        Me.obtnext = New DevExpress.XtraEditors.SimpleButton()
        Me.FTPathNameTo_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTPathNameTo = New DevExpress.XtraEditors.ButtonEdit()
        CType(Me.FTPathName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTPathNameTo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.description_lbl.Text = "เลือกที่เก็บไฟล์ Backup Database   " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "กดปุ่ม Next เพื่อทำการ Sync Data ต่อไป  หรือ" & _
    "  กดปุ่ม Cancel  เพื่อยกเลิก การ Sync Data."
        '
        'FTPath_lbl
        '
        Me.FTPath_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTPath_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTPath_lbl.Location = New System.Drawing.Point(33, 194)
        Me.FTPath_lbl.Name = "FTPath_lbl"
        Me.FTPath_lbl.Size = New System.Drawing.Size(185, 13)
        Me.FTPath_lbl.TabIndex = 1
        Me.FTPath_lbl.Text = "Path Name :"
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
        Me.obtcancel.Location = New System.Drawing.Point(652, 259)
        Me.obtcancel.Name = "obtcancel"
        Me.obtcancel.Size = New System.Drawing.Size(75, 23)
        Me.obtcancel.TabIndex = 3
        Me.obtcancel.Text = "Cancel"
        '
        'obtnext
        '
        Me.obtnext.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.obtnext.Location = New System.Drawing.Point(733, 259)
        Me.obtnext.Name = "obtnext"
        Me.obtnext.Size = New System.Drawing.Size(75, 23)
        Me.obtnext.TabIndex = 3
        Me.obtnext.Text = "Next"
        '
        'FTPathNameTo_lbl
        '
        Me.FTPathNameTo_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTPathNameTo_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTPathNameTo_lbl.Location = New System.Drawing.Point(33, 220)
        Me.FTPathNameTo_lbl.Name = "FTPathNameTo_lbl"
        Me.FTPathNameTo_lbl.Size = New System.Drawing.Size(185, 13)
        Me.FTPathNameTo_lbl.TabIndex = 1
        Me.FTPathNameTo_lbl.Text = "Path Name To :"
        '
        'FTPathNameTo
        '
        Me.FTPathNameTo.Location = New System.Drawing.Point(225, 219)
        Me.FTPathNameTo.Name = "FTPathNameTo"
        Me.FTPathNameTo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.FTPathNameTo.Size = New System.Drawing.Size(502, 20)
        Me.FTPathNameTo.TabIndex = 2
        '
        'wFormSetPathFile
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(824, 294)
        Me.Controls.Add(Me.obtnext)
        Me.Controls.Add(Me.obtcancel)
        Me.Controls.Add(Me.FTPathNameTo)
        Me.Controls.Add(Me.FTPathNameTo_lbl)
        Me.Controls.Add(Me.FTPathName)
        Me.Controls.Add(Me.FTPath_lbl)
        Me.Controls.Add(Me.description_lbl)
        Me.FormBorderEffect = DevExpress.XtraEditors.FormBorderEffect.Shadow
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "wFormSetPathFile"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "d"
        CType(Me.FTPathName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTPathNameTo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents description_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTPath_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FolderBrowserDialog1 As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents FTPathName As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents obtcancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents obtnext As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FTPathNameTo_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTPathNameTo As DevExpress.XtraEditors.ButtonEdit
End Class

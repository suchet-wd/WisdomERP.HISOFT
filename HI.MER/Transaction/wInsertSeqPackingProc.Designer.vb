<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wInsertSeqPackingProc
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
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.ogbPackingProc = New DevExpress.XtraEditors.GroupControl()
        Me.FTInsertImagePacking = New DevExpress.XtraEditors.PictureEdit()
        Me.FTInsertPackNote = New DevExpress.XtraEditors.MemoEdit()
        Me.FTInsertPackNote_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTInsertPackDescription = New DevExpress.XtraEditors.MemoEdit()
        Me.FTInsertPackDescription_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNInsertPackSeq = New DevExpress.XtraEditors.CalcEdit()
        Me.FNInsertPackSeq_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.ogbConfirmInsertSeqPack = New DevExpress.XtraEditors.GroupControl()
        Me.ocmCancelPackProc = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmInsertPackProc = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.ogbPackingProc, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbPackingProc.SuspendLayout()
        CType(Me.FTInsertImagePacking.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTInsertPackNote.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTInsertPackDescription.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNInsertPackSeq.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogbConfirmInsertSeqPack, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbConfirmInsertSeqPack.SuspendLayout()
        Me.SuspendLayout()
        '
        'ogbPackingProc
        '
        Me.ogbPackingProc.Controls.Add(Me.FTInsertImagePacking)
        Me.ogbPackingProc.Controls.Add(Me.FTInsertPackNote)
        Me.ogbPackingProc.Controls.Add(Me.FTInsertPackNote_lbl)
        Me.ogbPackingProc.Controls.Add(Me.FTInsertPackDescription)
        Me.ogbPackingProc.Controls.Add(Me.FTInsertPackDescription_lbl)
        Me.ogbPackingProc.Controls.Add(Me.FNInsertPackSeq)
        Me.ogbPackingProc.Controls.Add(Me.FNInsertPackSeq_lbl)
        Me.ogbPackingProc.Dock = System.Windows.Forms.DockStyle.Top
        Me.ogbPackingProc.Location = New System.Drawing.Point(0, 0)
        Me.ogbPackingProc.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbPackingProc.Name = "ogbPackingProc"
        Me.ogbPackingProc.Size = New System.Drawing.Size(979, 311)
        Me.ogbPackingProc.TabIndex = 1
        '
        'FTInsertImagePacking
        '
        Me.FTInsertImagePacking.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FTInsertImagePacking.Location = New System.Drawing.Point(810, 74)
        Me.FTInsertImagePacking.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTInsertImagePacking.Name = "FTInsertImagePacking"
        Me.FTInsertImagePacking.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch
        Me.FTInsertImagePacking.Size = New System.Drawing.Size(155, 229)
        Me.FTInsertImagePacking.TabIndex = 538
        '
        'FTInsertPackNote
        '
        Me.FTInsertPackNote.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FTInsertPackNote.EditValue = ""
        Me.FTInsertPackNote.Location = New System.Drawing.Point(188, 192)
        Me.FTInsertPackNote.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTInsertPackNote.Name = "FTInsertPackNote"
        Me.FTInsertPackNote.Properties.MaxLength = 500
        Me.FTInsertPackNote.Size = New System.Drawing.Size(615, 111)
        Me.FTInsertPackNote.TabIndex = 534
        Me.FTInsertPackNote.Tag = "2|"
        Me.FTInsertPackNote.UseOptimizedRendering = True
        '
        'FTInsertPackNote_lbl
        '
        Me.FTInsertPackNote_lbl.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(31, Byte), Integer), CType(CType(53, Byte), Integer))
        Me.FTInsertPackNote_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTInsertPackNote_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTInsertPackNote_lbl.Location = New System.Drawing.Point(37, 234)
        Me.FTInsertPackNote_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTInsertPackNote_lbl.Name = "FTInsertPackNote_lbl"
        Me.FTInsertPackNote_lbl.Size = New System.Drawing.Size(143, 23)
        Me.FTInsertPackNote_lbl.TabIndex = 533
        Me.FTInsertPackNote_lbl.Tag = "2|"
        Me.FTInsertPackNote_lbl.Text = "หมายเหตุ :"
        '
        'FTInsertPackDescription
        '
        Me.FTInsertPackDescription.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FTInsertPackDescription.EditValue = ""
        Me.FTInsertPackDescription.Location = New System.Drawing.Point(188, 74)
        Me.FTInsertPackDescription.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTInsertPackDescription.Name = "FTInsertPackDescription"
        Me.FTInsertPackDescription.Properties.MaxLength = 500
        Me.FTInsertPackDescription.Size = New System.Drawing.Size(615, 111)
        Me.FTInsertPackDescription.TabIndex = 532
        Me.FTInsertPackDescription.Tag = "2|"
        Me.FTInsertPackDescription.UseOptimizedRendering = True
        '
        'FTInsertPackDescription_lbl
        '
        Me.FTInsertPackDescription_lbl.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(31, Byte), Integer), CType(CType(53, Byte), Integer))
        Me.FTInsertPackDescription_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTInsertPackDescription_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTInsertPackDescription_lbl.Location = New System.Drawing.Point(10, 118)
        Me.FTInsertPackDescription_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTInsertPackDescription_lbl.Name = "FTInsertPackDescription_lbl"
        Me.FTInsertPackDescription_lbl.Size = New System.Drawing.Size(170, 23)
        Me.FTInsertPackDescription_lbl.TabIndex = 531
        Me.FTInsertPackDescription_lbl.Tag = "2|"
        Me.FTInsertPackDescription_lbl.Text = "รายละเอียดขั้นตอนการแพ็ค  :"
        '
        'FNInsertPackSeq
        '
        Me.FNInsertPackSeq.EnterMoveNextControl = True
        Me.FNInsertPackSeq.Location = New System.Drawing.Point(188, 42)
        Me.FNInsertPackSeq.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNInsertPackSeq.Name = "FNInsertPackSeq"
        Me.FNInsertPackSeq.Properties.Appearance.Options.UseTextOptions = True
        Me.FNInsertPackSeq.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNInsertPackSeq.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNInsertPackSeq.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNInsertPackSeq.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNInsertPackSeq.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNInsertPackSeq.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNInsertPackSeq.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNInsertPackSeq.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNInsertPackSeq.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNInsertPackSeq.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, True, False, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", Nothing, Nothing, True)})
        Me.FNInsertPackSeq.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNInsertPackSeq.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNInsertPackSeq.Properties.MaxLength = 5
        Me.FNInsertPackSeq.Properties.Precision = 2
        Me.FNInsertPackSeq.Size = New System.Drawing.Size(104, 22)
        Me.FNInsertPackSeq.TabIndex = 529
        Me.FNInsertPackSeq.Tag = "2|"
        '
        'FNInsertPackSeq_lbl
        '
        Me.FNInsertPackSeq_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNInsertPackSeq_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNInsertPackSeq_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNInsertPackSeq_lbl.Location = New System.Drawing.Point(41, 42)
        Me.FNInsertPackSeq_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNInsertPackSeq_lbl.Name = "FNInsertPackSeq_lbl"
        Me.FNInsertPackSeq_lbl.Size = New System.Drawing.Size(140, 23)
        Me.FNInsertPackSeq_lbl.TabIndex = 528
        Me.FNInsertPackSeq_lbl.Tag = "2|"
        Me.FNInsertPackSeq_lbl.Text = "ลำดับที่ขั้นตอนแพ็ค  :"
        '
        'ogbConfirmInsertSeqPack
        '
        Me.ogbConfirmInsertSeqPack.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbConfirmInsertSeqPack.Controls.Add(Me.ocmCancelPackProc)
        Me.ogbConfirmInsertSeqPack.Controls.Add(Me.ocmInsertPackProc)
        Me.ogbConfirmInsertSeqPack.Location = New System.Drawing.Point(0, 315)
        Me.ogbConfirmInsertSeqPack.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbConfirmInsertSeqPack.Name = "ogbConfirmInsertSeqPack"
        Me.ogbConfirmInsertSeqPack.ShowCaption = False
        Me.ogbConfirmInsertSeqPack.Size = New System.Drawing.Size(979, 48)
        Me.ogbConfirmInsertSeqPack.TabIndex = 2
        '
        'ocmCancelPackProc
        '
        Me.ocmCancelPackProc.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ocmCancelPackProc.Location = New System.Drawing.Point(615, 7)
        Me.ocmCancelPackProc.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmCancelPackProc.Name = "ocmCancelPackProc"
        Me.ocmCancelPackProc.Size = New System.Drawing.Size(167, 33)
        Me.ocmCancelPackProc.TabIndex = 8
        Me.ocmCancelPackProc.Text = "ยกเลิก"
        '
        'ocmInsertPackProc
        '
        Me.ocmInsertPackProc.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ocmInsertPackProc.Location = New System.Drawing.Point(188, 7)
        Me.ocmInsertPackProc.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmInsertPackProc.Name = "ocmInsertPackProc"
        Me.ocmInsertPackProc.Size = New System.Drawing.Size(167, 33)
        Me.ocmInsertPackProc.TabIndex = 7
        Me.ocmInsertPackProc.Text = "ตกลง"
        '
        'wInsertSeqPackingProc
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(979, 361)
        Me.Controls.Add(Me.ogbConfirmInsertSeqPack)
        Me.Controls.Add(Me.ogbPackingProc)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "wInsertSeqPackingProc"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "wInsertSeqPackingProc"
        CType(Me.ogbPackingProc, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbPackingProc.ResumeLayout(False)
        CType(Me.FTInsertImagePacking.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTInsertPackNote.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTInsertPackDescription.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNInsertPackSeq.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogbConfirmInsertSeqPack, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbConfirmInsertSeqPack.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogbPackingProc As DevExpress.XtraEditors.GroupControl
    Friend WithEvents FTInsertImagePacking As DevExpress.XtraEditors.PictureEdit
    Friend WithEvents FTInsertPackNote As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents FTInsertPackNote_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTInsertPackDescription As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents FTInsertPackDescription_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNInsertPackSeq As DevExpress.XtraEditors.CalcEdit
    Friend WithEvents FNInsertPackSeq_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ogbConfirmInsertSeqPack As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ocmCancelPackProc As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmInsertPackProc As DevExpress.XtraEditors.SimpleButton
End Class

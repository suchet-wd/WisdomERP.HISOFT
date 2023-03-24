<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wInsertSeqSewingProc
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
        Me.ogbSewingProc = New DevExpress.XtraEditors.GroupControl()
        Me.FTInsertImageSewing = New DevExpress.XtraEditors.PictureEdit()
        Me.FTInsertSewNote = New DevExpress.XtraEditors.MemoEdit()
        Me.FTInsertSewNote_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTInsertSewDescription = New DevExpress.XtraEditors.MemoEdit()
        Me.FTInsertSewDescription_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNInsertSewSeq = New DevExpress.XtraEditors.CalcEdit()
        Me.FNInsertSewSeq_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.ogbConfirmInsertSeqSew = New DevExpress.XtraEditors.GroupControl()
        Me.ocmCancelSewProc = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmInsertSewProc = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.ogbSewingProc, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbSewingProc.SuspendLayout()
        CType(Me.FTInsertImageSewing.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTInsertSewNote.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTInsertSewDescription.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNInsertSewSeq.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogbConfirmInsertSeqSew, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbConfirmInsertSeqSew.SuspendLayout()
        Me.SuspendLayout()
        '
        'ogbSewingProc
        '
        Me.ogbSewingProc.Controls.Add(Me.FTInsertImageSewing)
        Me.ogbSewingProc.Controls.Add(Me.FTInsertSewNote)
        Me.ogbSewingProc.Controls.Add(Me.FTInsertSewNote_lbl)
        Me.ogbSewingProc.Controls.Add(Me.FTInsertSewDescription)
        Me.ogbSewingProc.Controls.Add(Me.FTInsertSewDescription_lbl)
        Me.ogbSewingProc.Controls.Add(Me.FNInsertSewSeq)
        Me.ogbSewingProc.Controls.Add(Me.FNInsertSewSeq_lbl)
        Me.ogbSewingProc.Dock = System.Windows.Forms.DockStyle.Top
        Me.ogbSewingProc.Location = New System.Drawing.Point(0, 0)
        Me.ogbSewingProc.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbSewingProc.Name = "ogbSewingProc"
        Me.ogbSewingProc.Size = New System.Drawing.Size(979, 311)
        Me.ogbSewingProc.TabIndex = 0
        '
        'FTInsertImageSewing
        '
        Me.FTInsertImageSewing.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FTInsertImageSewing.Location = New System.Drawing.Point(810, 74)
        Me.FTInsertImageSewing.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTInsertImageSewing.Name = "FTInsertImageSewing"
        Me.FTInsertImageSewing.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch
        Me.FTInsertImageSewing.Size = New System.Drawing.Size(155, 229)
        Me.FTInsertImageSewing.TabIndex = 538
        '
        'FTInsertSewNote
        '
        Me.FTInsertSewNote.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FTInsertSewNote.EditValue = ""
        Me.FTInsertSewNote.Location = New System.Drawing.Point(188, 192)
        Me.FTInsertSewNote.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTInsertSewNote.Name = "FTInsertSewNote"
        Me.FTInsertSewNote.Properties.MaxLength = 500
        Me.FTInsertSewNote.Size = New System.Drawing.Size(615, 111)
        Me.FTInsertSewNote.TabIndex = 534
        Me.FTInsertSewNote.Tag = "2|"
        Me.FTInsertSewNote.UseOptimizedRendering = True
        '
        'FTInsertSewNote_lbl
        '
        Me.FTInsertSewNote_lbl.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(31, Byte), Integer), CType(CType(53, Byte), Integer))
        Me.FTInsertSewNote_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTInsertSewNote_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTInsertSewNote_lbl.Location = New System.Drawing.Point(37, 234)
        Me.FTInsertSewNote_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTInsertSewNote_lbl.Name = "FTInsertSewNote_lbl"
        Me.FTInsertSewNote_lbl.Size = New System.Drawing.Size(143, 23)
        Me.FTInsertSewNote_lbl.TabIndex = 533
        Me.FTInsertSewNote_lbl.Tag = "2|"
        Me.FTInsertSewNote_lbl.Text = "หมายเหตุ :"
        '
        'FTInsertSewDescription
        '
        Me.FTInsertSewDescription.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FTInsertSewDescription.EditValue = ""
        Me.FTInsertSewDescription.Location = New System.Drawing.Point(188, 74)
        Me.FTInsertSewDescription.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTInsertSewDescription.Name = "FTInsertSewDescription"
        Me.FTInsertSewDescription.Properties.MaxLength = 500
        Me.FTInsertSewDescription.Size = New System.Drawing.Size(615, 111)
        Me.FTInsertSewDescription.TabIndex = 532
        Me.FTInsertSewDescription.Tag = "2|"
        Me.FTInsertSewDescription.UseOptimizedRendering = True
        '
        'FTInsertSewDescription_lbl
        '
        Me.FTInsertSewDescription_lbl.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(31, Byte), Integer), CType(CType(53, Byte), Integer))
        Me.FTInsertSewDescription_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTInsertSewDescription_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTInsertSewDescription_lbl.Location = New System.Drawing.Point(10, 118)
        Me.FTInsertSewDescription_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTInsertSewDescription_lbl.Name = "FTInsertSewDescription_lbl"
        Me.FTInsertSewDescription_lbl.Size = New System.Drawing.Size(170, 23)
        Me.FTInsertSewDescription_lbl.TabIndex = 531
        Me.FTInsertSewDescription_lbl.Tag = "2|"
        Me.FTInsertSewDescription_lbl.Text = "รายละเอียดขั้นตอนการเย็บ  :"
        '
        'FNInsertSewSeq
        '
        Me.FNInsertSewSeq.EnterMoveNextControl = True
        Me.FNInsertSewSeq.Location = New System.Drawing.Point(188, 42)
        Me.FNInsertSewSeq.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNInsertSewSeq.Name = "FNInsertSewSeq"
        Me.FNInsertSewSeq.Properties.Appearance.Options.UseTextOptions = True
        Me.FNInsertSewSeq.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNInsertSewSeq.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNInsertSewSeq.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNInsertSewSeq.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNInsertSewSeq.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNInsertSewSeq.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNInsertSewSeq.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNInsertSewSeq.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNInsertSewSeq.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNInsertSewSeq.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, True, False, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", Nothing, Nothing, True)})
        Me.FNInsertSewSeq.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNInsertSewSeq.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNInsertSewSeq.Properties.MaxLength = 5
        Me.FNInsertSewSeq.Properties.Precision = 2
        Me.FNInsertSewSeq.Size = New System.Drawing.Size(104, 22)
        Me.FNInsertSewSeq.TabIndex = 529
        Me.FNInsertSewSeq.Tag = "2|"
        '
        'FNInsertSewSeq_lbl
        '
        Me.FNInsertSewSeq_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNInsertSewSeq_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNInsertSewSeq_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNInsertSewSeq_lbl.Location = New System.Drawing.Point(41, 42)
        Me.FNInsertSewSeq_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNInsertSewSeq_lbl.Name = "FNInsertSewSeq_lbl"
        Me.FNInsertSewSeq_lbl.Size = New System.Drawing.Size(140, 23)
        Me.FNInsertSewSeq_lbl.TabIndex = 528
        Me.FNInsertSewSeq_lbl.Tag = "2|"
        Me.FNInsertSewSeq_lbl.Text = "ลำดับที่ขั้นตอนเย็บ  :"
        '
        'ogbConfirmInsertSeqSew
        '
        Me.ogbConfirmInsertSeqSew.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbConfirmInsertSeqSew.Controls.Add(Me.ocmCancelSewProc)
        Me.ogbConfirmInsertSeqSew.Controls.Add(Me.ocmInsertSewProc)
        Me.ogbConfirmInsertSeqSew.Location = New System.Drawing.Point(0, 315)
        Me.ogbConfirmInsertSeqSew.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbConfirmInsertSeqSew.Name = "ogbConfirmInsertSeqSew"
        Me.ogbConfirmInsertSeqSew.ShowCaption = False
        Me.ogbConfirmInsertSeqSew.Size = New System.Drawing.Size(979, 48)
        Me.ogbConfirmInsertSeqSew.TabIndex = 1
        '
        'ocmCancelSewProc
        '
        Me.ocmCancelSewProc.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ocmCancelSewProc.Location = New System.Drawing.Point(615, 7)
        Me.ocmCancelSewProc.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmCancelSewProc.Name = "ocmCancelSewProc"
        Me.ocmCancelSewProc.Size = New System.Drawing.Size(167, 33)
        Me.ocmCancelSewProc.TabIndex = 8
        Me.ocmCancelSewProc.Text = "ยกเลิก"
        '
        'ocmInsertSewProc
        '
        Me.ocmInsertSewProc.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ocmInsertSewProc.Location = New System.Drawing.Point(188, 7)
        Me.ocmInsertSewProc.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmInsertSewProc.Name = "ocmInsertSewProc"
        Me.ocmInsertSewProc.Size = New System.Drawing.Size(167, 33)
        Me.ocmInsertSewProc.TabIndex = 7
        Me.ocmInsertSewProc.Text = "ตกลง"
        '
        'wInsertSeqSewingProc
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(979, 364)
        Me.Controls.Add(Me.ogbConfirmInsertSeqSew)
        Me.Controls.Add(Me.ogbSewingProc)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "wInsertSeqSewingProc"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "wInsertSeqSewingProc"
        CType(Me.ogbSewingProc, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbSewingProc.ResumeLayout(False)
        CType(Me.FTInsertImageSewing.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTInsertSewNote.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTInsertSewDescription.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNInsertSewSeq.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogbConfirmInsertSeqSew, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbConfirmInsertSeqSew.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogbSewingProc As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ogbConfirmInsertSeqSew As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ocmCancelSewProc As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmInsertSewProc As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FNInsertSewSeq As DevExpress.XtraEditors.CalcEdit
    Friend WithEvents FNInsertSewSeq_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTInsertSewDescription_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTInsertSewDescription As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents FTInsertSewNote_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTInsertSewNote As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents FTInsertImageSewing As DevExpress.XtraEditors.PictureEdit
End Class

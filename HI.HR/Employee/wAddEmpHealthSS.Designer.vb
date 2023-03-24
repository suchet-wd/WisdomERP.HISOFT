<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wAddEmpHealthSS

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
        Me.ogbbutton = New DevExpress.XtraEditors.GroupControl()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmsave = New DevExpress.XtraEditors.SimpleButton()
        Me.ogbemployee = New DevExpress.XtraEditors.GroupControl()
        Me.FTCauseOfInjury_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTCauseOfInjury = New DevExpress.XtraEditors.TextEdit()
        Me.FTInjuredBody_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTInjuredBody = New DevExpress.XtraEditors.TextEdit()
        Me.FDWorkDate = New DevExpress.XtraEditors.DateEdit()
        Me.FDWorkDate_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl7 = New DevExpress.XtraEditors.LabelControl()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.GroupControl2 = New DevExpress.XtraEditors.GroupControl()
        Me.GroupControl3 = New DevExpress.XtraEditors.GroupControl()
        Me.GroupControl4 = New DevExpress.XtraEditors.GroupControl()
        Me.FTDisability = New DevExpress.XtraEditors.CheckEdit()
        Me.FTLossOfSomeOrgans = New DevExpress.XtraEditors.CheckEdit()
        Me.FTLeaveWorkMore = New DevExpress.XtraEditors.CheckEdit()
        Me.FTLeaveWorkNiMore = New DevExpress.XtraEditors.CheckEdit()
        CType(Me.ogbbutton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbbutton.SuspendLayout()
        CType(Me.ogbemployee, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbemployee.SuspendLayout()
        CType(Me.FTCauseOfInjury.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTInjuredBody.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FDWorkDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FDWorkDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl2.SuspendLayout()
        CType(Me.GroupControl3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl3.SuspendLayout()
        CType(Me.GroupControl4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl4.SuspendLayout()
        CType(Me.FTDisability.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTLossOfSomeOrgans.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTLeaveWorkMore.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTLeaveWorkNiMore.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ogbbutton
        '
        Me.ogbbutton.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbbutton.Controls.Add(Me.ocmexit)
        Me.ogbbutton.Controls.Add(Me.ocmsave)
        Me.ogbbutton.Location = New System.Drawing.Point(3, 193)
        Me.ogbbutton.Name = "ogbbutton"
        Me.ogbbutton.ShowCaption = False
        Me.ogbbutton.Size = New System.Drawing.Size(767, 44)
        Me.ogbbutton.TabIndex = 17
        Me.ogbbutton.Text = "GroupControl9"
        '
        'ocmexit
        '
        Me.ocmexit.Location = New System.Drawing.Point(576, 9)
        Me.ocmexit.Name = "ocmexit"
        Me.ocmexit.Size = New System.Drawing.Size(155, 25)
        Me.ocmexit.TabIndex = 1
        Me.ocmexit.TabStop = False
        Me.ocmexit.Tag = "2|"
        Me.ocmexit.Text = "EXIT"
        '
        'ocmsave
        '
        Me.ocmsave.Location = New System.Drawing.Point(122, 8)
        Me.ocmsave.Name = "ocmsave"
        Me.ocmsave.Size = New System.Drawing.Size(155, 25)
        Me.ocmsave.TabIndex = 0
        Me.ocmsave.TabStop = False
        Me.ocmsave.Tag = "2|"
        Me.ocmsave.Text = "ADD"
        '
        'ogbemployee
        '
        Me.ogbemployee.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbemployee.AppearanceCaption.Options.UseTextOptions = True
        Me.ogbemployee.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ogbemployee.Controls.Add(Me.FTCauseOfInjury_lbl)
        Me.ogbemployee.Controls.Add(Me.FTCauseOfInjury)
        Me.ogbemployee.Controls.Add(Me.FTInjuredBody_lbl)
        Me.ogbemployee.Controls.Add(Me.FTInjuredBody)
        Me.ogbemployee.Controls.Add(Me.FDWorkDate)
        Me.ogbemployee.Controls.Add(Me.FDWorkDate_lbl)
        Me.ogbemployee.Location = New System.Drawing.Point(3, 4)
        Me.ogbemployee.Name = "ogbemployee"
        Me.ogbemployee.ShowCaption = False
        Me.ogbemployee.Size = New System.Drawing.Size(766, 86)
        Me.ogbemployee.TabIndex = 18
        Me.ogbemployee.Text = "Leave Detail"
        '
        'FTCauseOfInjury_lbl
        '
        Me.FTCauseOfInjury_lbl.Appearance.Options.UseTextOptions = True
        Me.FTCauseOfInjury_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTCauseOfInjury_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTCauseOfInjury_lbl.Location = New System.Drawing.Point(5, 54)
        Me.FTCauseOfInjury_lbl.Name = "FTCauseOfInjury_lbl"
        Me.FTCauseOfInjury_lbl.Size = New System.Drawing.Size(198, 20)
        Me.FTCauseOfInjury_lbl.TabIndex = 335
        Me.FTCauseOfInjury_lbl.Tag = "2|"
        Me.FTCauseOfInjury_lbl.Text = "สาเหตุของการบาดเจ็บหรือการเจ็บป่วย :"
        '
        'FTCauseOfInjury
        '
        Me.FTCauseOfInjury.Location = New System.Drawing.Point(209, 55)
        Me.FTCauseOfInjury.Name = "FTCauseOfInjury"
        Me.FTCauseOfInjury.Properties.MaxLength = 100
        Me.FTCauseOfInjury.Size = New System.Drawing.Size(545, 20)
        Me.FTCauseOfInjury.TabIndex = 334
        Me.FTCauseOfInjury.Tag = "2|"
        '
        'FTInjuredBody_lbl
        '
        Me.FTInjuredBody_lbl.Appearance.Options.UseTextOptions = True
        Me.FTInjuredBody_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTInjuredBody_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTInjuredBody_lbl.Location = New System.Drawing.Point(5, 28)
        Me.FTInjuredBody_lbl.Name = "FTInjuredBody_lbl"
        Me.FTInjuredBody_lbl.Size = New System.Drawing.Size(198, 20)
        Me.FTInjuredBody_lbl.TabIndex = 333
        Me.FTInjuredBody_lbl.Tag = "2|"
        Me.FTInjuredBody_lbl.Text = "ส่วนของร่างกายที่บาดเจ็บหรือการเจ็บป่วย :"
        '
        'FTInjuredBody
        '
        Me.FTInjuredBody.Location = New System.Drawing.Point(209, 29)
        Me.FTInjuredBody.Name = "FTInjuredBody"
        Me.FTInjuredBody.Properties.MaxLength = 100
        Me.FTInjuredBody.Size = New System.Drawing.Size(545, 20)
        Me.FTInjuredBody.TabIndex = 2
        Me.FTInjuredBody.Tag = "2|"
        '
        'FDWorkDate
        '
        Me.FDWorkDate.EditValue = Nothing
        Me.FDWorkDate.EnterMoveNextControl = True
        Me.FDWorkDate.Location = New System.Drawing.Point(209, 6)
        Me.FDWorkDate.Name = "FDWorkDate"
        Me.FDWorkDate.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.FDWorkDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FDWorkDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.FDWorkDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FDWorkDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FDWorkDate.Properties.NullDate = ""
        Me.FDWorkDate.Size = New System.Drawing.Size(180, 20)
        Me.FDWorkDate.TabIndex = 0
        Me.FDWorkDate.Tag = "2|"
        '
        'FDWorkDate_lbl
        '
        Me.FDWorkDate_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FDWorkDate_lbl.Appearance.Options.UseForeColor = True
        Me.FDWorkDate_lbl.Appearance.Options.UseTextOptions = True
        Me.FDWorkDate_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FDWorkDate_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FDWorkDate_lbl.Location = New System.Drawing.Point(87, 6)
        Me.FDWorkDate_lbl.Name = "FDWorkDate_lbl"
        Me.FDWorkDate_lbl.Size = New System.Drawing.Size(116, 19)
        Me.FDWorkDate_lbl.TabIndex = 282
        Me.FDWorkDate_lbl.Tag = "2|"
        Me.FDWorkDate_lbl.Text = "วันที่ :"
        '
        'LabelControl4
        '
        Me.LabelControl4.Appearance.Options.UseTextOptions = True
        Me.LabelControl4.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.LabelControl4.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LabelControl4.Location = New System.Drawing.Point(136, 6)
        Me.LabelControl4.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Size = New System.Drawing.Size(126, 20)
        Me.LabelControl4.TabIndex = 341
        Me.LabelControl4.Tag = "2|"
        Me.LabelControl4.Text = "ทำงานไม่ได้ชั่วคราว"
        '
        'LabelControl7
        '
        Me.LabelControl7.Appearance.Options.UseTextOptions = True
        Me.LabelControl7.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.LabelControl7.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LabelControl7.Location = New System.Drawing.Point(305, 6)
        Me.LabelControl7.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LabelControl7.Name = "LabelControl7"
        Me.LabelControl7.Size = New System.Drawing.Size(96, 20)
        Me.LabelControl7.TabIndex = 338
        Me.LabelControl7.Tag = "2|"
        Me.LabelControl7.Text = "ระดับความรุนแรง"
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Me.GroupControl2)
        Me.GroupControl1.Controls.Add(Me.LabelControl7)
        Me.GroupControl1.Location = New System.Drawing.Point(3, 91)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.ShowCaption = False
        Me.GroupControl1.Size = New System.Drawing.Size(767, 100)
        Me.GroupControl1.TabIndex = 18
        Me.GroupControl1.Text = "GroupControl9"
        '
        'GroupControl2
        '
        Me.GroupControl2.Controls.Add(Me.FTLossOfSomeOrgans)
        Me.GroupControl2.Controls.Add(Me.FTDisability)
        Me.GroupControl2.Controls.Add(Me.GroupControl3)
        Me.GroupControl2.Location = New System.Drawing.Point(0, 36)
        Me.GroupControl2.Name = "GroupControl2"
        Me.GroupControl2.ShowCaption = False
        Me.GroupControl2.Size = New System.Drawing.Size(766, 62)
        Me.GroupControl2.TabIndex = 339
        Me.GroupControl2.Text = "GroupControl9"
        '
        'GroupControl3
        '
        Me.GroupControl3.Controls.Add(Me.GroupControl4)
        Me.GroupControl3.Controls.Add(Me.LabelControl4)
        Me.GroupControl3.Location = New System.Drawing.Point(332, 0)
        Me.GroupControl3.Name = "GroupControl3"
        Me.GroupControl3.ShowCaption = False
        Me.GroupControl3.Size = New System.Drawing.Size(435, 64)
        Me.GroupControl3.TabIndex = 340
        Me.GroupControl3.Text = "GroupControl9"
        '
        'GroupControl4
        '
        Me.GroupControl4.Controls.Add(Me.FTLeaveWorkNiMore)
        Me.GroupControl4.Controls.Add(Me.FTLeaveWorkMore)
        Me.GroupControl4.Location = New System.Drawing.Point(1, 33)
        Me.GroupControl4.Name = "GroupControl4"
        Me.GroupControl4.ShowCaption = False
        Me.GroupControl4.Size = New System.Drawing.Size(433, 29)
        Me.GroupControl4.TabIndex = 342
        Me.GroupControl4.Text = "GroupControl9"
        '
        'FTDisability
        '
        Me.FTDisability.EditValue = "0"
        Me.FTDisability.Location = New System.Drawing.Point(32, 36)
        Me.FTDisability.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTDisability.Name = "FTDisability"
        Me.FTDisability.Properties.Caption = "ทุพพลภาพ"
        Me.FTDisability.Properties.ValueChecked = "1"
        Me.FTDisability.Properties.ValueUnchecked = "0"
        Me.FTDisability.Size = New System.Drawing.Size(93, 20)
        Me.FTDisability.TabIndex = 389
        Me.FTDisability.Tag = "2|"
        '
        'FTLossOfSomeOrgans
        '
        Me.FTLossOfSomeOrgans.EditValue = "0"
        Me.FTLossOfSomeOrgans.Location = New System.Drawing.Point(188, 36)
        Me.FTLossOfSomeOrgans.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTLossOfSomeOrgans.Name = "FTLossOfSomeOrgans"
        Me.FTLossOfSomeOrgans.Properties.Caption = "สูญเสียอวัยวะบางส่วน"
        Me.FTLossOfSomeOrgans.Properties.ValueChecked = "1"
        Me.FTLossOfSomeOrgans.Properties.ValueUnchecked = "0"
        Me.FTLossOfSomeOrgans.Size = New System.Drawing.Size(138, 20)
        Me.FTLossOfSomeOrgans.TabIndex = 390
        Me.FTLossOfSomeOrgans.Tag = "2|"
        '
        'FTLeaveWorkMore
        '
        Me.FTLeaveWorkMore.EditValue = "0"
        Me.FTLeaveWorkMore.Location = New System.Drawing.Point(37, 5)
        Me.FTLeaveWorkMore.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTLeaveWorkMore.Name = "FTLeaveWorkMore"
        Me.FTLeaveWorkMore.Properties.Caption = "หยุดงานเกิน 3 วัน"
        Me.FTLeaveWorkMore.Properties.ValueChecked = "1"
        Me.FTLeaveWorkMore.Properties.ValueUnchecked = "0"
        Me.FTLeaveWorkMore.Size = New System.Drawing.Size(138, 20)
        Me.FTLeaveWorkMore.TabIndex = 391
        Me.FTLeaveWorkMore.Tag = "2|"
        '
        'FTLeaveWorkNiMore
        '
        Me.FTLeaveWorkNiMore.EditValue = "0"
        Me.FTLeaveWorkNiMore.Location = New System.Drawing.Point(218, 6)
        Me.FTLeaveWorkNiMore.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTLeaveWorkNiMore.Name = "FTLeaveWorkNiMore"
        Me.FTLeaveWorkNiMore.Properties.Caption = "หยุดงานไม่เกิน 3 วัน"
        Me.FTLeaveWorkNiMore.Properties.ValueChecked = "1"
        Me.FTLeaveWorkNiMore.Properties.ValueUnchecked = "0"
        Me.FTLeaveWorkNiMore.Size = New System.Drawing.Size(138, 20)
        Me.FTLeaveWorkNiMore.TabIndex = 392
        Me.FTLeaveWorkNiMore.Tag = "2|"
        '
        'wAddEmpHealthSS
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(769, 239)
        Me.ControlBox = False
        Me.Controls.Add(Me.GroupControl1)
        Me.Controls.Add(Me.ogbemployee)
        Me.Controls.Add(Me.ogbbutton)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "wAddEmpHealthSS"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "wAddEmpHealthSS"
        CType(Me.ogbbutton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbbutton.ResumeLayout(False)
        CType(Me.ogbemployee, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbemployee.ResumeLayout(False)
        CType(Me.FTCauseOfInjury.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTInjuredBody.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FDWorkDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FDWorkDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl2.ResumeLayout(False)
        CType(Me.GroupControl3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl3.ResumeLayout(False)
        CType(Me.GroupControl4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl4.ResumeLayout(False)
        CType(Me.FTDisability.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTLossOfSomeOrgans.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTLeaveWorkMore.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTLeaveWorkNiMore.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogbbutton As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmsave As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ogbemployee As DevExpress.XtraEditors.GroupControl
    Friend WithEvents FDWorkDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents FDWorkDate_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTInjuredBody_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTInjuredBody As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FTCauseOfInjury_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTCauseOfInjury As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl7 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents GroupControl2 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents GroupControl3 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents GroupControl4 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents FTLossOfSomeOrgans As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents FTDisability As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents FTLeaveWorkNiMore As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents FTLeaveWorkMore As DevExpress.XtraEditors.CheckEdit
End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wCreateBomDev
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
        Me.FTStyleDetail = New DevExpress.XtraEditors.TextEdit()
        Me.FTStyle = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.FNBomDevType_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNBomDevType = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.FTSeason = New DevExpress.XtraEditors.TextEdit()
        Me.FTStateMergeData = New DevExpress.XtraEditors.CheckEdit()
        Me.GroupControl2 = New DevExpress.XtraEditors.GroupControl()
        Me.ocmClear = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmcancel = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmok = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.FTStyleDetail.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTStyle.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNBomDevType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTSeason.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTStateMergeData.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl2.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupControl1
        '
        Me.GroupControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupControl1.Controls.Add(Me.FTStyleDetail)
        Me.GroupControl1.Controls.Add(Me.FTStyle)
        Me.GroupControl1.Controls.Add(Me.LabelControl2)
        Me.GroupControl1.Controls.Add(Me.FNBomDevType_lbl)
        Me.GroupControl1.Controls.Add(Me.FNBomDevType)
        Me.GroupControl1.Controls.Add(Me.LabelControl1)
        Me.GroupControl1.Controls.Add(Me.FTSeason)
        Me.GroupControl1.Controls.Add(Me.FTStateMergeData)
        Me.GroupControl1.Location = New System.Drawing.Point(3, 6)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(435, 105)
        Me.GroupControl1.TabIndex = 287
        Me.GroupControl1.Text = "Style"
        '
        'FTStyleDetail
        '
        Me.FTStyleDetail.Location = New System.Drawing.Point(201, 29)
        Me.FTStyleDetail.Name = "FTStyleDetail"
        Me.FTStyleDetail.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FTStyleDetail.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FTStyleDetail.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FTStyleDetail.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FTStyleDetail.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.FTStyleDetail.Size = New System.Drawing.Size(210, 20)
        Me.FTStyleDetail.TabIndex = 2
        Me.FTStyleDetail.Tag = "2|"
        '
        'FTStyle
        '
        Me.FTStyle.Location = New System.Drawing.Point(97, 29)
        Me.FTStyle.Name = "FTStyle"
        Me.FTStyle.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FTStyle.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FTStyle.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FTStyle.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FTStyle.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.FTStyle.Size = New System.Drawing.Size(98, 20)
        Me.FTStyle.TabIndex = 1
        Me.FTStyle.Tag = "2|"
        '
        'LabelControl2
        '
        Me.LabelControl2.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.LabelControl2.Appearance.Options.UseForeColor = True
        Me.LabelControl2.Appearance.Options.UseTextOptions = True
        Me.LabelControl2.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LabelControl2.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LabelControl2.Location = New System.Drawing.Point(9, 28)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(82, 20)
        Me.LabelControl2.TabIndex = 507
        Me.LabelControl2.Tag = "2|"
        Me.LabelControl2.Text = "Style No :"
        '
        'FNBomDevType_lbl
        '
        Me.FNBomDevType_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNBomDevType_lbl.Appearance.Options.UseForeColor = True
        Me.FNBomDevType_lbl.Appearance.Options.UseTextOptions = True
        Me.FNBomDevType_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNBomDevType_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNBomDevType_lbl.Location = New System.Drawing.Point(232, 56)
        Me.FNBomDevType_lbl.Name = "FNBomDevType_lbl"
        Me.FNBomDevType_lbl.Size = New System.Drawing.Size(75, 17)
        Me.FNBomDevType_lbl.TabIndex = 501
        Me.FNBomDevType_lbl.Tag = "2|"
        Me.FNBomDevType_lbl.Text = "BOM Type :"
        '
        'FNBomDevType
        '
        Me.FNBomDevType.EditValue = ""
        Me.FNBomDevType.EnterMoveNextControl = True
        Me.FNBomDevType.Location = New System.Drawing.Point(313, 55)
        Me.FNBomDevType.Name = "FNBomDevType"
        Me.FNBomDevType.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FNBomDevType.Properties.Appearance.Options.UseBackColor = True
        Me.FNBomDevType.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FNBomDevType.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FNBomDevType.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FNBomDevType.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FNBomDevType.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNBomDevType.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNBomDevType.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNBomDevType.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNBomDevType.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNBomDevType.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNBomDevType.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNBomDevType.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNBomDevType.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FNBomDevType.Properties.Tag = "FNBomDevType"
        Me.FNBomDevType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.FNBomDevType.Size = New System.Drawing.Size(98, 20)
        Me.FNBomDevType.TabIndex = 4
        Me.FNBomDevType.Tag = "2|"
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.LabelControl1.Appearance.Options.UseForeColor = True
        Me.LabelControl1.Appearance.Options.UseTextOptions = True
        Me.LabelControl1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LabelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LabelControl1.Location = New System.Drawing.Point(0, 56)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(91, 17)
        Me.LabelControl1.TabIndex = 499
        Me.LabelControl1.Tag = "2|"
        Me.LabelControl1.Text = "Season :"
        '
        'FTSeason
        '
        Me.FTSeason.Location = New System.Drawing.Point(97, 55)
        Me.FTSeason.Name = "FTSeason"
        Me.FTSeason.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FTSeason.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FTSeason.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FTSeason.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FTSeason.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.FTSeason.Size = New System.Drawing.Size(98, 20)
        Me.FTSeason.TabIndex = 3
        Me.FTSeason.Tag = "2|"
        '
        'FTStateMergeData
        '
        Me.FTStateMergeData.EditValue = "0"
        Me.FTStateMergeData.Location = New System.Drawing.Point(12, 81)
        Me.FTStateMergeData.Name = "FTStateMergeData"
        Me.FTStateMergeData.Properties.Appearance.Options.UseTextOptions = True
        Me.FTStateMergeData.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.FTStateMergeData.Properties.Caption = "Merge Data In Style Destination"
        Me.FTStateMergeData.Properties.ValueChecked = "1"
        Me.FTStateMergeData.Properties.ValueUnchecked = "0"
        Me.FTStateMergeData.Size = New System.Drawing.Size(418, 20)
        Me.FTStateMergeData.TabIndex = 394
        Me.FTStateMergeData.Visible = False
        '
        'GroupControl2
        '
        Me.GroupControl2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupControl2.Controls.Add(Me.ocmClear)
        Me.GroupControl2.Controls.Add(Me.ocmcancel)
        Me.GroupControl2.Controls.Add(Me.ocmok)
        Me.GroupControl2.Location = New System.Drawing.Point(3, 114)
        Me.GroupControl2.Name = "GroupControl2"
        Me.GroupControl2.ShowCaption = False
        Me.GroupControl2.Size = New System.Drawing.Size(435, 41)
        Me.GroupControl2.TabIndex = 288
        Me.GroupControl2.Text = "GroupControl2"
        '
        'ocmClear
        '
        Me.ocmClear.Location = New System.Drawing.Point(148, 9)
        Me.ocmClear.Name = "ocmClear"
        Me.ocmClear.Size = New System.Drawing.Size(128, 25)
        Me.ocmClear.TabIndex = 108
        Me.ocmClear.TabStop = False
        Me.ocmClear.Tag = "2|"
        Me.ocmClear.Text = "Clear"
        '
        'ocmcancel
        '
        Me.ocmcancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmcancel.Location = New System.Drawing.Point(282, 9)
        Me.ocmcancel.Name = "ocmcancel"
        Me.ocmcancel.Size = New System.Drawing.Size(145, 25)
        Me.ocmcancel.TabIndex = 107
        Me.ocmcancel.TabStop = False
        Me.ocmcancel.Tag = "2|"
        Me.ocmcancel.Text = "Cancel"
        '
        'ocmok
        '
        Me.ocmok.Location = New System.Drawing.Point(9, 9)
        Me.ocmok.Name = "ocmok"
        Me.ocmok.Size = New System.Drawing.Size(133, 25)
        Me.ocmok.TabIndex = 106
        Me.ocmok.TabStop = False
        Me.ocmok.Tag = "2|"
        Me.ocmok.Text = "OK"
        '
        'wCreateBomDev
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(442, 161)
        Me.ControlBox = False
        Me.Controls.Add(Me.GroupControl2)
        Me.Controls.Add(Me.GroupControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "wCreateBomDev"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Create BOM Develop Style"
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        CType(Me.FTStyleDetail.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTStyle.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNBomDevType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTSeason.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTStateMergeData.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents GroupControl2 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ocmok As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmcancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FNBomDevType_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNBomDevType As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTSeason As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTStateMergeData As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents FTStyle As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FTStyleDetail As DevExpress.XtraEditors.TextEdit
    Friend WithEvents ocmClear As DevExpress.XtraEditors.SimpleButton
End Class

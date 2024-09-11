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
        Me.GroupControl2 = New DevExpress.XtraEditors.GroupControl()
        Me.ocmcancel = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmok = New DevExpress.XtraEditors.SimpleButton()
        Me.FTVersion = New DevExpress.XtraEditors.TextEdit()
        Me.FTVersion_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNBomDevType_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNBomDevType = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.FTSeason = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.FTStateMergeData = New DevExpress.XtraEditors.CheckEdit()
        Me.FTStyle = New DevExpress.XtraEditors.TextEdit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl2.SuspendLayout()
        CType(Me.FTVersion.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNBomDevType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTSeason.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTStateMergeData.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTStyle.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupControl1
        '
        Me.GroupControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupControl1.Controls.Add(Me.FTStyle)
        Me.GroupControl1.Controls.Add(Me.LabelControl2)
        Me.GroupControl1.Controls.Add(Me.FTVersion)
        Me.GroupControl1.Controls.Add(Me.FTVersion_lbl)
        Me.GroupControl1.Controls.Add(Me.FNBomDevType_lbl)
        Me.GroupControl1.Controls.Add(Me.FNBomDevType)
        Me.GroupControl1.Controls.Add(Me.LabelControl1)
        Me.GroupControl1.Controls.Add(Me.FTSeason)
        Me.GroupControl1.Controls.Add(Me.FTStateMergeData)
        Me.GroupControl1.Location = New System.Drawing.Point(3, 6)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(435, 146)
        Me.GroupControl1.TabIndex = 287
        Me.GroupControl1.Text = "Style"
        '
        'GroupControl2
        '
        Me.GroupControl2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupControl2.Controls.Add(Me.ocmcancel)
        Me.GroupControl2.Controls.Add(Me.ocmok)
        Me.GroupControl2.Location = New System.Drawing.Point(3, 155)
        Me.GroupControl2.Name = "GroupControl2"
        Me.GroupControl2.ShowCaption = False
        Me.GroupControl2.Size = New System.Drawing.Size(435, 41)
        Me.GroupControl2.TabIndex = 288
        Me.GroupControl2.Text = "GroupControl2"
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
        'FTVersion
        '
        Me.FTVersion.EditValue = ""
        Me.FTVersion.Location = New System.Drawing.Point(307, 55)
        Me.FTVersion.Name = "FTVersion"
        Me.FTVersion.Properties.Appearance.BackColor = System.Drawing.Color.LightCyan
        Me.FTVersion.Properties.Appearance.Options.UseBackColor = True
        Me.FTVersion.Properties.ReadOnly = True
        Me.FTVersion.Size = New System.Drawing.Size(98, 20)
        Me.FTVersion.TabIndex = 502
        Me.FTVersion.Tag = "2|"
        '
        'FTVersion_lbl
        '
        Me.FTVersion_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTVersion_lbl.Appearance.Options.UseForeColor = True
        Me.FTVersion_lbl.Appearance.Options.UseTextOptions = True
        Me.FTVersion_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTVersion_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTVersion_lbl.Location = New System.Drawing.Point(247, 56)
        Me.FTVersion_lbl.Name = "FTVersion_lbl"
        Me.FTVersion_lbl.Size = New System.Drawing.Size(54, 17)
        Me.FTVersion_lbl.TabIndex = 503
        Me.FTVersion_lbl.Tag = "2|"
        Me.FTVersion_lbl.Text = "Version :"
        '
        'FNBomDevType_lbl
        '
        Me.FNBomDevType_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNBomDevType_lbl.Appearance.Options.UseForeColor = True
        Me.FNBomDevType_lbl.Appearance.Options.UseTextOptions = True
        Me.FNBomDevType_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNBomDevType_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNBomDevType_lbl.Location = New System.Drawing.Point(16, 56)
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
        Me.FNBomDevType.Location = New System.Drawing.Point(97, 55)
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
        Me.FNBomDevType.TabIndex = 500
        Me.FNBomDevType.Tag = "2|"
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.LabelControl1.Appearance.Options.UseForeColor = True
        Me.LabelControl1.Appearance.Options.UseTextOptions = True
        Me.LabelControl1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LabelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LabelControl1.Location = New System.Drawing.Point(210, 30)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(91, 17)
        Me.LabelControl1.TabIndex = 499
        Me.LabelControl1.Tag = "2|"
        Me.LabelControl1.Text = "Season :"
        '
        'FTSeason
        '
        Me.FTSeason.Location = New System.Drawing.Point(307, 29)
        Me.FTSeason.Name = "FTSeason"
        Me.FTSeason.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FTSeason.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FTSeason.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FTSeason.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FTSeason.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.FTSeason.Size = New System.Drawing.Size(98, 20)
        Me.FTSeason.TabIndex = 498
        Me.FTSeason.Tag = "2|"
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
        'FTStateMergeData
        '
        Me.FTStateMergeData.EditValue = "0"
        Me.FTStateMergeData.Location = New System.Drawing.Point(9, 81)
        Me.FTStateMergeData.Name = "FTStateMergeData"
        Me.FTStateMergeData.Properties.Appearance.Options.UseTextOptions = True
        Me.FTStateMergeData.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.FTStateMergeData.Properties.Caption = "Merge Data In Style Destination"
        Me.FTStateMergeData.Properties.ValueChecked = "1"
        Me.FTStateMergeData.Properties.ValueUnchecked = "0"
        Me.FTStateMergeData.Size = New System.Drawing.Size(418, 20)
        Me.FTStateMergeData.TabIndex = 394
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
        Me.FTStyle.TabIndex = 509
        Me.FTStyle.Tag = "2|"
        '
        'wCreateBomDev
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(442, 202)
        Me.ControlBox = False
        Me.Controls.Add(Me.GroupControl2)
        Me.Controls.Add(Me.GroupControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "wCreateBomDev"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Create BOM Develop Style"
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl2.ResumeLayout(False)
        CType(Me.FTVersion.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNBomDevType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTSeason.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTStateMergeData.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTStyle.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents GroupControl2 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ocmok As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmcancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FTVersion As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FTVersion_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNBomDevType_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNBomDevType As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTSeason As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTStateMergeData As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents FTStyle As DevExpress.XtraEditors.TextEdit
End Class

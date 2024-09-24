<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wCreateBomDevByBomOriginal
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
        Dim EditorButtonImageOptions2 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim SerializableAppearanceObject5 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject6 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject7 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject8 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.FNBomDevType_Hide = New DevExpress.XtraEditors.TextEdit()
        Me.FNBomDevType = New DevExpress.XtraEditors.TextEdit()
        Me.FNHSysStyleDevId_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTStyle = New DevExpress.XtraEditors.TextEdit()
        Me.oFTStyle_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.oFNBomDevType_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.oFTSeason_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTSeason = New DevExpress.XtraEditors.TextEdit()
        Me.FTVersion_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNVersion = New DevExpress.XtraEditors.TextEdit()
        Me.FNHSysStyleDevId_Hide = New DevExpress.XtraEditors.TextEdit()
        Me.FNHSysStyleDevId_None = New DevExpress.XtraEditors.TextEdit()
        Me.FTStateMergeData = New DevExpress.XtraEditors.CheckEdit()
        Me.FNHSysStyleDevId = New DevExpress.XtraEditors.ButtonEdit()
        Me.GroupControl2 = New DevExpress.XtraEditors.GroupControl()
        Me.ocmClear = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmcancel = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmok = New DevExpress.XtraEditors.SimpleButton()
        Me.FNHSysStyleDevId_Target = New DevExpress.XtraEditors.TextEdit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.FNBomDevType_Hide.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNBomDevType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTStyle.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTSeason.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNVersion.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysStyleDevId_Hide.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysStyleDevId_None.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTStateMergeData.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysStyleDevId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl2.SuspendLayout()
        CType(Me.FNHSysStyleDevId_Target.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupControl1
        '
        Me.GroupControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupControl1.Controls.Add(Me.FNHSysStyleDevId_Target)
        Me.GroupControl1.Controls.Add(Me.FNBomDevType_Hide)
        Me.GroupControl1.Controls.Add(Me.FNBomDevType)
        Me.GroupControl1.Controls.Add(Me.FNHSysStyleDevId_lbl)
        Me.GroupControl1.Controls.Add(Me.FTStyle)
        Me.GroupControl1.Controls.Add(Me.oFTStyle_lbl)
        Me.GroupControl1.Controls.Add(Me.oFNBomDevType_lbl)
        Me.GroupControl1.Controls.Add(Me.oFTSeason_lbl)
        Me.GroupControl1.Controls.Add(Me.FTSeason)
        Me.GroupControl1.Controls.Add(Me.FTVersion_lbl)
        Me.GroupControl1.Controls.Add(Me.FNVersion)
        Me.GroupControl1.Controls.Add(Me.FNHSysStyleDevId_Hide)
        Me.GroupControl1.Controls.Add(Me.FNHSysStyleDevId_None)
        Me.GroupControl1.Controls.Add(Me.FTStateMergeData)
        Me.GroupControl1.Controls.Add(Me.FNHSysStyleDevId)
        Me.GroupControl1.Location = New System.Drawing.Point(3, 6)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(481, 116)
        Me.GroupControl1.TabIndex = 287
        Me.GroupControl1.Text = "BOM Original"
        '
        'FNBomDevType_Hide
        '
        Me.FNBomDevType_Hide.Location = New System.Drawing.Point(198, 83)
        Me.FNBomDevType_Hide.Name = "FNBomDevType_Hide"
        Me.FNBomDevType_Hide.Properties.Appearance.BackColor = System.Drawing.Color.Gold
        Me.FNBomDevType_Hide.Properties.Appearance.Options.UseBackColor = True
        Me.FNBomDevType_Hide.Size = New System.Drawing.Size(53, 20)
        Me.FNBomDevType_Hide.TabIndex = 516
        Me.FNBomDevType_Hide.Visible = False
        '
        'FNBomDevType
        '
        Me.FNBomDevType.EditValue = ""
        Me.FNBomDevType.Enabled = False
        Me.FNBomDevType.Location = New System.Drawing.Point(122, 83)
        Me.FNBomDevType.Name = "FNBomDevType"
        Me.FNBomDevType.Properties.Appearance.BackColor = System.Drawing.Color.LightCyan
        Me.FNBomDevType.Properties.Appearance.Options.UseBackColor = True
        Me.FNBomDevType.Properties.ReadOnly = True
        Me.FNBomDevType.Size = New System.Drawing.Size(70, 20)
        Me.FNBomDevType.TabIndex = 515
        Me.FNBomDevType.Tag = "2|"
        '
        'FNHSysStyleDevId_lbl
        '
        Me.FNHSysStyleDevId_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysStyleDevId_lbl.Appearance.Options.UseForeColor = True
        Me.FNHSysStyleDevId_lbl.Appearance.Options.UseTextOptions = True
        Me.FNHSysStyleDevId_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysStyleDevId_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysStyleDevId_lbl.Location = New System.Drawing.Point(9, 30)
        Me.FNHSysStyleDevId_lbl.Name = "FNHSysStyleDevId_lbl"
        Me.FNHSysStyleDevId_lbl.Size = New System.Drawing.Size(101, 20)
        Me.FNHSysStyleDevId_lbl.TabIndex = 514
        Me.FNHSysStyleDevId_lbl.Tag = "2|"
        Me.FNHSysStyleDevId_lbl.Text = "BOM Original :"
        '
        'FTStyle
        '
        Me.FTStyle.Enabled = False
        Me.FTStyle.Location = New System.Drawing.Point(122, 57)
        Me.FTStyle.Name = "FTStyle"
        Me.FTStyle.Properties.Appearance.BackColor = System.Drawing.Color.LightCyan
        Me.FTStyle.Properties.Appearance.Options.UseBackColor = True
        Me.FTStyle.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FTStyle.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FTStyle.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FTStyle.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FTStyle.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.FTStyle.Size = New System.Drawing.Size(129, 20)
        Me.FTStyle.TabIndex = 508
        Me.FTStyle.Tag = "2|"
        '
        'oFTStyle_lbl
        '
        Me.oFTStyle_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.oFTStyle_lbl.Appearance.Options.UseForeColor = True
        Me.oFTStyle_lbl.Appearance.Options.UseTextOptions = True
        Me.oFTStyle_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.oFTStyle_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.oFTStyle_lbl.Location = New System.Drawing.Point(9, 57)
        Me.oFTStyle_lbl.Name = "oFTStyle_lbl"
        Me.oFTStyle_lbl.Size = New System.Drawing.Size(101, 20)
        Me.oFTStyle_lbl.TabIndex = 513
        Me.oFTStyle_lbl.Tag = "2|"
        Me.oFTStyle_lbl.Text = "Style No :"
        '
        'oFNBomDevType_lbl
        '
        Me.oFNBomDevType_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.oFNBomDevType_lbl.Appearance.Options.UseForeColor = True
        Me.oFNBomDevType_lbl.Appearance.Options.UseTextOptions = True
        Me.oFNBomDevType_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.oFNBomDevType_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.oFNBomDevType_lbl.Location = New System.Drawing.Point(9, 84)
        Me.oFNBomDevType_lbl.Name = "oFNBomDevType_lbl"
        Me.oFNBomDevType_lbl.Size = New System.Drawing.Size(101, 17)
        Me.oFNBomDevType_lbl.TabIndex = 512
        Me.oFNBomDevType_lbl.Tag = "2|"
        Me.oFNBomDevType_lbl.Text = "BOM Type :"
        '
        'oFTSeason_lbl
        '
        Me.oFTSeason_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.oFTSeason_lbl.Appearance.Options.UseForeColor = True
        Me.oFTSeason_lbl.Appearance.Options.UseTextOptions = True
        Me.oFTSeason_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.oFTSeason_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.oFTSeason_lbl.Location = New System.Drawing.Point(257, 58)
        Me.oFTSeason_lbl.Name = "oFTSeason_lbl"
        Me.oFTSeason_lbl.Size = New System.Drawing.Size(101, 17)
        Me.oFTSeason_lbl.TabIndex = 511
        Me.oFTSeason_lbl.Tag = "2|"
        Me.oFTSeason_lbl.Text = "Season :"
        '
        'FTSeason
        '
        Me.FTSeason.Enabled = False
        Me.FTSeason.Location = New System.Drawing.Point(364, 57)
        Me.FTSeason.Name = "FTSeason"
        Me.FTSeason.Properties.Appearance.BackColor = System.Drawing.Color.LightCyan
        Me.FTSeason.Properties.Appearance.Options.UseBackColor = True
        Me.FTSeason.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FTSeason.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FTSeason.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FTSeason.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FTSeason.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.FTSeason.Size = New System.Drawing.Size(105, 20)
        Me.FTSeason.TabIndex = 509
        Me.FTSeason.Tag = "2|"
        '
        'FTVersion_lbl
        '
        Me.FTVersion_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTVersion_lbl.Appearance.Options.UseForeColor = True
        Me.FTVersion_lbl.Appearance.Options.UseTextOptions = True
        Me.FTVersion_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTVersion_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTVersion_lbl.Location = New System.Drawing.Point(304, 84)
        Me.FTVersion_lbl.Name = "FTVersion_lbl"
        Me.FTVersion_lbl.Size = New System.Drawing.Size(54, 17)
        Me.FTVersion_lbl.TabIndex = 503
        Me.FTVersion_lbl.Tag = "2|"
        Me.FTVersion_lbl.Text = "Version :"
        '
        'FNVersion
        '
        Me.FNVersion.EditValue = ""
        Me.FNVersion.Enabled = False
        Me.FNVersion.Location = New System.Drawing.Point(364, 83)
        Me.FNVersion.Name = "FNVersion"
        Me.FNVersion.Properties.Appearance.BackColor = System.Drawing.Color.LightCyan
        Me.FNVersion.Properties.Appearance.Options.UseBackColor = True
        Me.FNVersion.Properties.ReadOnly = True
        Me.FNVersion.Size = New System.Drawing.Size(47, 20)
        Me.FNVersion.TabIndex = 504
        Me.FNVersion.Tag = "2|"
        '
        'FNHSysStyleDevId_Hide
        '
        Me.FNHSysStyleDevId_Hide.EditValue = "Source"
        Me.FNHSysStyleDevId_Hide.Location = New System.Drawing.Point(427, 1)
        Me.FNHSysStyleDevId_Hide.Name = "FNHSysStyleDevId_Hide"
        Me.FNHSysStyleDevId_Hide.Properties.Appearance.BackColor = System.Drawing.Color.Gold
        Me.FNHSysStyleDevId_Hide.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysStyleDevId_Hide.Properties.ReadOnly = True
        Me.FNHSysStyleDevId_Hide.Size = New System.Drawing.Size(46, 20)
        Me.FNHSysStyleDevId_Hide.TabIndex = 506
        Me.FNHSysStyleDevId_Hide.Tag = "2|"
        Me.FNHSysStyleDevId_Hide.Visible = False
        '
        'FNHSysStyleDevId_None
        '
        Me.FNHSysStyleDevId_None.Enabled = False
        Me.FNHSysStyleDevId_None.Location = New System.Drawing.Point(257, 31)
        Me.FNHSysStyleDevId_None.Name = "FNHSysStyleDevId_None"
        Me.FNHSysStyleDevId_None.Properties.Appearance.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysStyleDevId_None.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysStyleDevId_None.Properties.ReadOnly = True
        Me.FNHSysStyleDevId_None.Size = New System.Drawing.Size(212, 20)
        Me.FNHSysStyleDevId_None.TabIndex = 501
        Me.FNHSysStyleDevId_None.Tag = "2|"
        '
        'FTStateMergeData
        '
        Me.FTStateMergeData.EditValue = "0"
        Me.FTStateMergeData.Location = New System.Drawing.Point(9, 109)
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
        'FNHSysStyleDevId
        '
        Me.FNHSysStyleDevId.Location = New System.Drawing.Point(122, 31)
        Me.FNHSysStyleDevId.Name = "FNHSysStyleDevId"
        Me.FNHSysStyleDevId.Properties.Appearance.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysStyleDevId.Properties.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysStyleDevId.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysStyleDevId.Properties.Appearance.Options.UseForeColor = True
        Me.FNHSysStyleDevId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, EditorButtonImageOptions2, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject5, SerializableAppearanceObject6, SerializableAppearanceObject7, SerializableAppearanceObject8, "", "709", Nothing, DevExpress.Utils.ToolTipAnchor.[Default])})
        Me.FNHSysStyleDevId.Size = New System.Drawing.Size(129, 20)
        Me.FNHSysStyleDevId.TabIndex = 502
        Me.FNHSysStyleDevId.Tag = "2|"
        '
        'GroupControl2
        '
        Me.GroupControl2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupControl2.Controls.Add(Me.ocmClear)
        Me.GroupControl2.Controls.Add(Me.ocmcancel)
        Me.GroupControl2.Controls.Add(Me.ocmok)
        Me.GroupControl2.Location = New System.Drawing.Point(3, 125)
        Me.GroupControl2.Name = "GroupControl2"
        Me.GroupControl2.ShowCaption = False
        Me.GroupControl2.Size = New System.Drawing.Size(481, 41)
        Me.GroupControl2.TabIndex = 288
        Me.GroupControl2.Text = "GroupControl2"
        '
        'ocmClear
        '
        Me.ocmClear.Location = New System.Drawing.Point(9, 8)
        Me.ocmClear.Name = "ocmClear"
        Me.ocmClear.Size = New System.Drawing.Size(90, 25)
        Me.ocmClear.TabIndex = 108
        Me.ocmClear.TabStop = False
        Me.ocmClear.Tag = "2|"
        Me.ocmClear.Text = "Clear"
        '
        'ocmcancel
        '
        Me.ocmcancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmcancel.Location = New System.Drawing.Point(383, 8)
        Me.ocmcancel.Name = "ocmcancel"
        Me.ocmcancel.Size = New System.Drawing.Size(90, 25)
        Me.ocmcancel.TabIndex = 107
        Me.ocmcancel.TabStop = False
        Me.ocmcancel.Tag = "2|"
        Me.ocmcancel.Text = "Cancel"
        '
        'ocmok
        '
        Me.ocmok.Location = New System.Drawing.Point(287, 8)
        Me.ocmok.Name = "ocmok"
        Me.ocmok.Size = New System.Drawing.Size(90, 25)
        Me.ocmok.TabIndex = 106
        Me.ocmok.TabStop = False
        Me.ocmok.Tag = "2|"
        Me.ocmok.Text = "OK"
        '
        'FNHSysStyleDevId_Target
        '
        Me.FNHSysStyleDevId_Target.EditValue = "Target"
        Me.FNHSysStyleDevId_Target.Location = New System.Drawing.Point(379, 1)
        Me.FNHSysStyleDevId_Target.Name = "FNHSysStyleDevId_Target"
        Me.FNHSysStyleDevId_Target.Properties.Appearance.BackColor = System.Drawing.Color.Gold
        Me.FNHSysStyleDevId_Target.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysStyleDevId_Target.Size = New System.Drawing.Size(42, 20)
        Me.FNHSysStyleDevId_Target.TabIndex = 517
        Me.FNHSysStyleDevId_Target.Visible = False
        '
        'wCreateBomDevByBomOriginal
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(488, 172)
        Me.ControlBox = False
        Me.Controls.Add(Me.GroupControl2)
        Me.Controls.Add(Me.GroupControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "wCreateBomDevByBomOriginal"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Create BOM Develop Style by Import BOM Original"
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        CType(Me.FNBomDevType_Hide.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNBomDevType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTStyle.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTSeason.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNVersion.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysStyleDevId_Hide.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysStyleDevId_None.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTStateMergeData.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysStyleDevId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl2.ResumeLayout(False)
        CType(Me.FNHSysStyleDevId_Target.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents GroupControl2 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ocmok As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmcancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FTStateMergeData As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents ocmClear As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FNHSysStyleDevId_None As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FNVersion As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FTVersion_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysStyleDevId_Hide As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FNHSysStyleDevId As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FNHSysStyleDevId_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTStyle As DevExpress.XtraEditors.TextEdit
    Friend WithEvents oFTStyle_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents oFNBomDevType_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents oFTSeason_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTSeason As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FNBomDevType As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FNBomDevType_Hide As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FNHSysStyleDevId_Target As DevExpress.XtraEditors.TextEdit
End Class

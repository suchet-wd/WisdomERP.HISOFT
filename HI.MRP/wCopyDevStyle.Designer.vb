<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wCopyDevStyle
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
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.FNHSysStyleIdF_None = New DevExpress.XtraEditors.TextEdit()
        Me.FNHSysStyleIdF_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.FTStateMergeData = New DevExpress.XtraEditors.CheckEdit()
        Me.FNHSysSeasonIdF_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysSeasonIdF = New DevExpress.XtraEditors.TextEdit()
        Me.GroupControl2 = New DevExpress.XtraEditors.GroupControl()
        Me.ocmcancel = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmok = New DevExpress.XtraEditors.SimpleButton()
        Me.FNHSysStyleDevId = New DevExpress.XtraEditors.ButtonEdit()
        Me.FNHSysStyleDevId_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTSeason = New DevExpress.XtraEditors.TextEdit()
        Me.FTSeason_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysStyleIdF = New DevExpress.XtraEditors.TextEdit()
        Me.FNVersion = New DevExpress.XtraEditors.CalcEdit()
        CType(Me.FNHSysStyleIdF_None.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.FTStateMergeData.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysSeasonIdF.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl2.SuspendLayout()
        CType(Me.FNHSysStyleDevId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTSeason.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysStyleIdF.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNVersion.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'FNHSysStyleIdF_None
        '
        Me.FNHSysStyleIdF_None.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FNHSysStyleIdF_None.Location = New System.Drawing.Point(273, 34)
        Me.FNHSysStyleIdF_None.Name = "FNHSysStyleIdF_None"
        Me.FNHSysStyleIdF_None.Properties.Appearance.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysStyleIdF_None.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysStyleIdF_None.Properties.ReadOnly = True
        Me.FNHSysStyleIdF_None.Size = New System.Drawing.Size(342, 20)
        Me.FNHSysStyleIdF_None.TabIndex = 283
        Me.FNHSysStyleIdF_None.Tag = "2|"
        '
        'FNHSysStyleIdF_lbl
        '
        Me.FNHSysStyleIdF_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysStyleIdF_lbl.Appearance.Options.UseForeColor = True
        Me.FNHSysStyleIdF_lbl.Appearance.Options.UseTextOptions = True
        Me.FNHSysStyleIdF_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysStyleIdF_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysStyleIdF_lbl.Location = New System.Drawing.Point(31, 34)
        Me.FNHSysStyleIdF_lbl.Name = "FNHSysStyleIdF_lbl"
        Me.FNHSysStyleIdF_lbl.Size = New System.Drawing.Size(107, 17)
        Me.FNHSysStyleIdF_lbl.TabIndex = 282
        Me.FNHSysStyleIdF_lbl.Tag = "2|"
        Me.FNHSysStyleIdF_lbl.Text = "From Style No :"
        '
        'GroupControl1
        '
        Me.GroupControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupControl1.Controls.Add(Me.FNVersion)
        Me.GroupControl1.Controls.Add(Me.FTSeason)
        Me.GroupControl1.Controls.Add(Me.FTSeason_lbl)
        Me.GroupControl1.Controls.Add(Me.FNHSysStyleDevId)
        Me.GroupControl1.Controls.Add(Me.FNHSysStyleDevId_lbl)
        Me.GroupControl1.Controls.Add(Me.FTStateMergeData)
        Me.GroupControl1.Controls.Add(Me.FNHSysSeasonIdF_lbl)
        Me.GroupControl1.Controls.Add(Me.FNHSysSeasonIdF)
        Me.GroupControl1.Controls.Add(Me.FNHSysStyleIdF_None)
        Me.GroupControl1.Controls.Add(Me.FNHSysStyleIdF_lbl)
        Me.GroupControl1.Controls.Add(Me.FNHSysStyleIdF)
        Me.GroupControl1.Location = New System.Drawing.Point(3, 6)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(644, 176)
        Me.GroupControl1.TabIndex = 287
        Me.GroupControl1.Text = "Style"
        '
        'FTStateMergeData
        '
        Me.FTStateMergeData.EditValue = "0"
        Me.FTStateMergeData.Location = New System.Drawing.Point(138, 151)
        Me.FTStateMergeData.Name = "FTStateMergeData"
        Me.FTStateMergeData.Properties.Appearance.Options.UseTextOptions = True
        Me.FTStateMergeData.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.FTStateMergeData.Properties.Caption = "Merge Data In Style Destination"
        Me.FTStateMergeData.Properties.ValueChecked = "1"
        Me.FTStateMergeData.Properties.ValueUnchecked = "0"
        Me.FTStateMergeData.Size = New System.Drawing.Size(477, 19)
        Me.FTStateMergeData.TabIndex = 394
        '
        'FNHSysSeasonIdF_lbl
        '
        Me.FNHSysSeasonIdF_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysSeasonIdF_lbl.Appearance.Options.UseForeColor = True
        Me.FNHSysSeasonIdF_lbl.Appearance.Options.UseTextOptions = True
        Me.FNHSysSeasonIdF_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysSeasonIdF_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysSeasonIdF_lbl.Location = New System.Drawing.Point(11, 59)
        Me.FNHSysSeasonIdF_lbl.Name = "FNHSysSeasonIdF_lbl"
        Me.FNHSysSeasonIdF_lbl.Size = New System.Drawing.Size(127, 15)
        Me.FNHSysSeasonIdF_lbl.TabIndex = 302
        Me.FNHSysSeasonIdF_lbl.Tag = "2|"
        Me.FNHSysSeasonIdF_lbl.Text = "Season :"
        '
        'FNHSysSeasonIdF
        '
        Me.FNHSysSeasonIdF.Location = New System.Drawing.Point(139, 58)
        Me.FNHSysSeasonIdF.Name = "FNHSysSeasonIdF"
        Me.FNHSysSeasonIdF.Properties.Appearance.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysSeasonIdF.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysSeasonIdF.Properties.ReadOnly = True
        Me.FNHSysSeasonIdF.Size = New System.Drawing.Size(133, 20)
        Me.FNHSysSeasonIdF.TabIndex = 300
        Me.FNHSysSeasonIdF.Tag = "2|"
        '
        'GroupControl2
        '
        Me.GroupControl2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupControl2.Controls.Add(Me.ocmcancel)
        Me.GroupControl2.Controls.Add(Me.ocmok)
        Me.GroupControl2.Location = New System.Drawing.Point(3, 185)
        Me.GroupControl2.Name = "GroupControl2"
        Me.GroupControl2.ShowCaption = False
        Me.GroupControl2.Size = New System.Drawing.Size(644, 41)
        Me.GroupControl2.TabIndex = 288
        Me.GroupControl2.Text = "GroupControl2"
        '
        'ocmcancel
        '
        Me.ocmcancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmcancel.Location = New System.Drawing.Point(373, 9)
        Me.ocmcancel.Name = "ocmcancel"
        Me.ocmcancel.Size = New System.Drawing.Size(145, 25)
        Me.ocmcancel.TabIndex = 107
        Me.ocmcancel.TabStop = False
        Me.ocmcancel.Tag = "2|"
        Me.ocmcancel.Text = "Cancel"
        '
        'ocmok
        '
        Me.ocmok.Location = New System.Drawing.Point(139, 9)
        Me.ocmok.Name = "ocmok"
        Me.ocmok.Size = New System.Drawing.Size(133, 25)
        Me.ocmok.TabIndex = 106
        Me.ocmok.TabStop = False
        Me.ocmok.Tag = "2|"
        Me.ocmok.Text = "OK"
        '
        'FNHSysStyleDevId
        '
        Me.FNHSysStyleDevId.Location = New System.Drawing.Point(139, 98)
        Me.FNHSysStyleDevId.Name = "FNHSysStyleDevId"
        Me.FNHSysStyleDevId.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysStyleDevId.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysStyleDevId.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNHSysStyleDevId.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNHSysStyleDevId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject2, "", "277", Nothing, True)})
        Me.FNHSysStyleDevId.Properties.ReadOnly = True
        Me.FNHSysStyleDevId.Properties.Tag = ""
        Me.FNHSysStyleDevId.Size = New System.Drawing.Size(133, 20)
        Me.FNHSysStyleDevId.TabIndex = 395
        Me.FNHSysStyleDevId.Tag = "2|"
        '
        'FNHSysStyleDevId_lbl
        '
        Me.FNHSysStyleDevId_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysStyleDevId_lbl.Appearance.Options.UseForeColor = True
        Me.FNHSysStyleDevId_lbl.Appearance.Options.UseTextOptions = True
        Me.FNHSysStyleDevId_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysStyleDevId_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysStyleDevId_lbl.Location = New System.Drawing.Point(5, 97)
        Me.FNHSysStyleDevId_lbl.Name = "FNHSysStyleDevId_lbl"
        Me.FNHSysStyleDevId_lbl.Size = New System.Drawing.Size(130, 20)
        Me.FNHSysStyleDevId_lbl.TabIndex = 396
        Me.FNHSysStyleDevId_lbl.Tag = "2|"
        Me.FNHSysStyleDevId_lbl.Text = "Copy To Style No :"
        '
        'FTSeason
        '
        Me.FTSeason.Location = New System.Drawing.Point(408, 95)
        Me.FTSeason.Name = "FTSeason"
        Me.FTSeason.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FTSeason.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FTSeason.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FTSeason.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FTSeason.Properties.ReadOnly = True
        Me.FTSeason.Size = New System.Drawing.Size(133, 20)
        Me.FTSeason.TabIndex = 398
        Me.FTSeason.Tag = "2|"
        '
        'FTSeason_lbl
        '
        Me.FTSeason_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTSeason_lbl.Appearance.Options.UseForeColor = True
        Me.FTSeason_lbl.Appearance.Options.UseTextOptions = True
        Me.FTSeason_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTSeason_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTSeason_lbl.Location = New System.Drawing.Point(278, 99)
        Me.FTSeason_lbl.Name = "FTSeason_lbl"
        Me.FTSeason_lbl.Size = New System.Drawing.Size(125, 15)
        Me.FTSeason_lbl.TabIndex = 399
        Me.FTSeason_lbl.Tag = "2|"
        Me.FTSeason_lbl.Text = "Season :"
        '
        'FNHSysStyleIdF
        '
        Me.FNHSysStyleIdF.Location = New System.Drawing.Point(139, 34)
        Me.FNHSysStyleIdF.Name = "FNHSysStyleIdF"
        Me.FNHSysStyleIdF.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysStyleIdF.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysStyleIdF.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNHSysStyleIdF.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNHSysStyleIdF.Properties.ReadOnly = True
        Me.FNHSysStyleIdF.Properties.Tag = ""
        Me.FNHSysStyleIdF.Size = New System.Drawing.Size(133, 20)
        Me.FNHSysStyleIdF.TabIndex = 281
        Me.FNHSysStyleIdF.Tag = "2|"
        '
        'FNVersion
        '
        Me.FNVersion.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FNVersion.EnterMoveNextControl = True
        Me.FNVersion.Location = New System.Drawing.Point(547, 94)
        Me.FNVersion.Name = "FNVersion"
        Me.FNVersion.Properties.Appearance.Options.UseTextOptions = True
        Me.FNVersion.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNVersion.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNVersion.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNVersion.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNVersion.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNVersion.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNVersion.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNVersion.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNVersion.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNVersion.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, True, False, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", Nothing, Nothing, True)})
        Me.FNVersion.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNVersion.Properties.Precision = 0
        Me.FNVersion.Properties.ReadOnly = True
        Me.FNVersion.Size = New System.Drawing.Size(35, 20)
        Me.FNVersion.TabIndex = 400
        Me.FNVersion.Tag = "2|"
        '
        'wCopyDevStyle
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(651, 232)
        Me.ControlBox = False
        Me.Controls.Add(Me.GroupControl2)
        Me.Controls.Add(Me.GroupControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "wCopyDevStyle"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Copy Develop Style"
        CType(Me.FNHSysStyleIdF_None.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        CType(Me.FTStateMergeData.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysSeasonIdF.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl2.ResumeLayout(False)
        CType(Me.FNHSysStyleDevId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTSeason.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysStyleIdF.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNVersion.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents FNHSysStyleIdF_None As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FNHSysStyleIdF_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents GroupControl2 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ocmok As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmcancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FNHSysSeasonIdF_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysSeasonIdF As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FTStateMergeData As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents FNHSysStyleDevId As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FNHSysStyleDevId_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTSeason As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FTSeason_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysStyleIdF As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FNVersion As DevExpress.XtraEditors.CalcEdit
End Class

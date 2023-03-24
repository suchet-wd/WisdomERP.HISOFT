<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wImportOptiplan
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
        Dim EditorButtonImageOptions1 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject4 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.ogbselectfile = New DevExpress.XtraEditors.GroupControl()
        Me.FTStateDeletebefore = New DevExpress.XtraEditors.CheckEdit()
        Me.FNHSysUnitId_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysUnitId = New DevExpress.XtraEditors.ButtonEdit()
        Me.FTFilePath = New DevExpress.XtraEditors.ButtonEdit()
        Me.ogbdetail = New DevExpress.XtraEditors.GroupControl()
        Me.opshet = New DevExpress.XtraSpreadsheet.SpreadsheetControl()
        Me.ogbmainprocbutton = New DevExpress.XtraEditors.PanelControl()
        Me.ocmdeleteoptiplan = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmimportoptiplan = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmclear = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmimportoptiplanallinfolder = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.ogbselectfile, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbselectfile.SuspendLayout()
        CType(Me.FTStateDeletebefore.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysUnitId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTFilePath.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogbdetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbdetail.SuspendLayout()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbmainprocbutton.SuspendLayout()
        Me.SuspendLayout()
        '
        'ogbselectfile
        '
        Me.ogbselectfile.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbselectfile.Controls.Add(Me.FTStateDeletebefore)
        Me.ogbselectfile.Controls.Add(Me.FNHSysUnitId_lbl)
        Me.ogbselectfile.Controls.Add(Me.FNHSysUnitId)
        Me.ogbselectfile.Controls.Add(Me.FTFilePath)
        Me.ogbselectfile.Location = New System.Drawing.Point(2, 2)
        Me.ogbselectfile.Name = "ogbselectfile"
        Me.ogbselectfile.Size = New System.Drawing.Size(1021, 76)
        Me.ogbselectfile.TabIndex = 0
        Me.ogbselectfile.Text = "Select File"
        '
        'FTStateDeletebefore
        '
        Me.FTStateDeletebefore.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FTStateDeletebefore.EditValue = "0"
        Me.FTStateDeletebefore.Location = New System.Drawing.Point(264, 51)
        Me.FTStateDeletebefore.Name = "FTStateDeletebefore"
        Me.FTStateDeletebefore.Properties.Caption = "ลบข้อมูล ตาม ใบสั่งผลิต และ Mark ตาม File ก่อน Import"
        Me.FTStateDeletebefore.Properties.ValueChecked = "1"
        Me.FTStateDeletebefore.Properties.ValueUnchecked = "0"
        Me.FTStateDeletebefore.Size = New System.Drawing.Size(747, 20)
        Me.FTStateDeletebefore.TabIndex = 269
        Me.FTStateDeletebefore.Tag = "2|"
        '
        'FNHSysUnitId_lbl
        '
        Me.FNHSysUnitId_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysUnitId_lbl.Appearance.Options.UseForeColor = True
        Me.FNHSysUnitId_lbl.Appearance.Options.UseTextOptions = True
        Me.FNHSysUnitId_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysUnitId_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysUnitId_lbl.Location = New System.Drawing.Point(13, 50)
        Me.FNHSysUnitId_lbl.Name = "FNHSysUnitId_lbl"
        Me.FNHSysUnitId_lbl.Size = New System.Drawing.Size(92, 19)
        Me.FNHSysUnitId_lbl.TabIndex = 268
        Me.FNHSysUnitId_lbl.Tag = "2|"
        Me.FNHSysUnitId_lbl.Text = "Unit :"
        '
        'FNHSysUnitId
        '
        Me.FNHSysUnitId.EnterMoveNextControl = True
        Me.FNHSysUnitId.Location = New System.Drawing.Point(107, 50)
        Me.FNHSysUnitId.Name = "FNHSysUnitId"
        Me.FNHSysUnitId.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FNHSysUnitId.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysUnitId.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysUnitId.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysUnitId.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FNHSysUnitId.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FNHSysUnitId.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNHSysUnitId.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysUnitId.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNHSysUnitId.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNHSysUnitId.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysUnitId.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysUnitId.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNHSysUnitId.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNHSysUnitId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", 15, True, True, False, EditorButtonImageOptions1, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, SerializableAppearanceObject2, SerializableAppearanceObject3, SerializableAppearanceObject4, "", "118", Nothing, DevExpress.Utils.ToolTipAnchor.[Default])})
        Me.FNHSysUnitId.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.FNHSysUnitId.Properties.MaxLength = 30
        Me.FNHSysUnitId.Properties.ReadOnly = True
        Me.FNHSysUnitId.Size = New System.Drawing.Size(143, 20)
        Me.FNHSysUnitId.TabIndex = 267
        Me.FNHSysUnitId.Tag = "2|"
        '
        'FTFilePath
        '
        Me.FTFilePath.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FTFilePath.Location = New System.Drawing.Point(10, 26)
        Me.FTFilePath.Name = "FTFilePath"
        Me.FTFilePath.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FTFilePath.Properties.Appearance.Options.UseBackColor = True
        Me.FTFilePath.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FTFilePath.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FTFilePath.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FTFilePath.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FTFilePath.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FTFilePath.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FTFilePath.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FTFilePath.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FTFilePath.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FTFilePath.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FTFilePath.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FTFilePath.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FTFilePath.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.FTFilePath.Properties.ReadOnly = True
        Me.FTFilePath.Size = New System.Drawing.Size(1000, 20)
        Me.FTFilePath.TabIndex = 1
        Me.FTFilePath.Tag = "2|"
        '
        'ogbdetail
        '
        Me.ogbdetail.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbdetail.Controls.Add(Me.opshet)
        Me.ogbdetail.Location = New System.Drawing.Point(2, 84)
        Me.ogbdetail.Name = "ogbdetail"
        Me.ogbdetail.Size = New System.Drawing.Size(1021, 487)
        Me.ogbdetail.TabIndex = 1
        Me.ogbdetail.Text = "File Detail"
        '
        'opshet
        '
        Me.opshet.Dock = System.Windows.Forms.DockStyle.Fill
        Me.opshet.Location = New System.Drawing.Point(2, 23)
        Me.opshet.Name = "opshet"
        Me.opshet.Options.Behavior.Column.Resize = DevExpress.XtraSpreadsheet.DocumentCapability.Enabled
        Me.opshet.ReadOnly = True
        Me.opshet.Size = New System.Drawing.Size(1017, 462)
        Me.opshet.TabIndex = 0
        '
        'ogbmainprocbutton
        '
        Me.ogbmainprocbutton.Controls.Add(Me.ocmimportoptiplanallinfolder)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmdeleteoptiplan)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmexit)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmimportoptiplan)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmclear)
        Me.ogbmainprocbutton.Location = New System.Drawing.Point(82, 247)
        Me.ogbmainprocbutton.Name = "ogbmainprocbutton"
        Me.ogbmainprocbutton.Size = New System.Drawing.Size(818, 98)
        Me.ogbmainprocbutton.TabIndex = 391
        '
        'ocmdeleteoptiplan
        '
        Me.ocmdeleteoptiplan.Location = New System.Drawing.Point(268, 26)
        Me.ocmdeleteoptiplan.Name = "ocmdeleteoptiplan"
        Me.ocmdeleteoptiplan.Size = New System.Drawing.Size(95, 25)
        Me.ocmdeleteoptiplan.TabIndex = 97
        Me.ocmdeleteoptiplan.TabStop = False
        Me.ocmdeleteoptiplan.Tag = "2|"
        Me.ocmdeleteoptiplan.Text = "delete optiplan"
        '
        'ocmexit
        '
        Me.ocmexit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmexit.Location = New System.Drawing.Point(609, 42)
        Me.ocmexit.Name = "ocmexit"
        Me.ocmexit.Size = New System.Drawing.Size(95, 25)
        Me.ocmexit.TabIndex = 96
        Me.ocmexit.TabStop = False
        Me.ocmexit.Tag = "2|"
        Me.ocmexit.Text = "EXIT"
        '
        'ocmimportoptiplan
        '
        Me.ocmimportoptiplan.Location = New System.Drawing.Point(5, 11)
        Me.ocmimportoptiplan.Name = "ocmimportoptiplan"
        Me.ocmimportoptiplan.Size = New System.Drawing.Size(95, 25)
        Me.ocmimportoptiplan.TabIndex = 93
        Me.ocmimportoptiplan.TabStop = False
        Me.ocmimportoptiplan.Tag = "2|"
        Me.ocmimportoptiplan.Text = "Importoptiplan"
        '
        'ocmclear
        '
        Me.ocmclear.Location = New System.Drawing.Point(106, 11)
        Me.ocmclear.Name = "ocmclear"
        Me.ocmclear.Size = New System.Drawing.Size(95, 25)
        Me.ocmclear.TabIndex = 95
        Me.ocmclear.TabStop = False
        Me.ocmclear.Tag = "2|"
        Me.ocmclear.Text = "CLEAR"
        '
        'ocmimportoptiplanallinfolder
        '
        Me.ocmimportoptiplanallinfolder.Location = New System.Drawing.Point(394, 42)
        Me.ocmimportoptiplanallinfolder.Name = "ocmimportoptiplanallinfolder"
        Me.ocmimportoptiplanallinfolder.Size = New System.Drawing.Size(160, 25)
        Me.ocmimportoptiplanallinfolder.TabIndex = 98
        Me.ocmimportoptiplanallinfolder.TabStop = False
        Me.ocmimportoptiplanallinfolder.Tag = "2|"
        Me.ocmimportoptiplanallinfolder.Text = "ImportoptiplanAllinfolder"
        '
        'wImportOptiplan
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1024, 571)
        Me.Controls.Add(Me.ogbmainprocbutton)
        Me.Controls.Add(Me.ogbdetail)
        Me.Controls.Add(Me.ogbselectfile)
        Me.Name = "wImportOptiplan"
        Me.Text = "wImportOptiplan"
        CType(Me.ogbselectfile, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbselectfile.ResumeLayout(False)
        CType(Me.FTStateDeletebefore.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysUnitId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTFilePath.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogbdetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbdetail.ResumeLayout(False)
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbmainprocbutton.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogbselectfile As DevExpress.XtraEditors.GroupControl
    Friend WithEvents FTFilePath As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents ogbdetail As DevExpress.XtraEditors.GroupControl
    Friend WithEvents opshet As DevExpress.XtraSpreadsheet.SpreadsheetControl
    Friend WithEvents ogbmainprocbutton As DevExpress.XtraEditors.PanelControl
    Friend WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmimportoptiplan As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmclear As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FNHSysUnitId_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysUnitId As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents ocmdeleteoptiplan As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FTStateDeletebefore As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents ocmimportoptiplanallinfolder As DevExpress.XtraEditors.SimpleButton
End Class

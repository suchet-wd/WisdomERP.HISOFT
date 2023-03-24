<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wImportExcelStyle
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
        Me.ogbdetail = New DevExpress.XtraEditors.GroupControl()
        Me.FNStyleType = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.FNStyleType_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTMapStyleName = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.FTMapStyleName_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTMapStyleCode = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.FTMapStyleCode_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTFilePath = New DevExpress.XtraEditors.ButtonEdit()
        Me.opshet = New DevExpress.XtraSpreadsheet.SpreadsheetControl()
        Me.ogbmainprocbutton = New DevExpress.XtraEditors.PanelControl()
        Me.ocmimportimmage = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmImportnetprice = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.ogbselectfile = New DevExpress.XtraEditors.GroupControl()
        CType(Me.ogbdetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbdetail.SuspendLayout()
        CType(Me.FNStyleType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTMapStyleName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTMapStyleCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTFilePath.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbmainprocbutton.SuspendLayout()
        CType(Me.ogbselectfile, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbselectfile.SuspendLayout()
        Me.SuspendLayout()
        '
        'ogbdetail
        '
        Me.ogbdetail.Controls.Add(Me.FNStyleType)
        Me.ogbdetail.Controls.Add(Me.FNStyleType_lbl)
        Me.ogbdetail.Controls.Add(Me.FTMapStyleName)
        Me.ogbdetail.Controls.Add(Me.FTMapStyleName_lbl)
        Me.ogbdetail.Controls.Add(Me.FTMapStyleCode)
        Me.ogbdetail.Controls.Add(Me.FTMapStyleCode_lbl)
        Me.ogbdetail.Controls.Add(Me.FTFilePath)
        Me.ogbdetail.Dock = System.Windows.Forms.DockStyle.Top
        Me.ogbdetail.Location = New System.Drawing.Point(0, 0)
        Me.ogbdetail.Name = "ogbdetail"
        Me.ogbdetail.Size = New System.Drawing.Size(975, 102)
        Me.ogbdetail.TabIndex = 143
        Me.ogbdetail.Text = "Select File Excel"
        '
        'FNStyleType
        '
        Me.FNStyleType.EditValue = ""
        Me.FNStyleType.EnterMoveNextControl = True
        Me.FNStyleType.Location = New System.Drawing.Point(211, 74)
        Me.FNStyleType.Name = "FNStyleType"
        Me.FNStyleType.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FNStyleType.Properties.Appearance.Options.UseBackColor = True
        Me.FNStyleType.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FNStyleType.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FNStyleType.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FNStyleType.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FNStyleType.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNStyleType.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNStyleType.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNStyleType.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNStyleType.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNStyleType.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNStyleType.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNStyleType.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNStyleType.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FNStyleType.Properties.Tag = "FNStyleType"
        Me.FNStyleType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.FNStyleType.Size = New System.Drawing.Size(149, 20)
        Me.FNStyleType.TabIndex = 258
        Me.FNStyleType.Tag = "2|"
        '
        'FNStyleType_lbl
        '
        Me.FNStyleType_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNStyleType_lbl.Appearance.Options.UseForeColor = True
        Me.FNStyleType_lbl.Appearance.Options.UseTextOptions = True
        Me.FNStyleType_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNStyleType_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNStyleType_lbl.Location = New System.Drawing.Point(18, 74)
        Me.FNStyleType_lbl.Name = "FNStyleType_lbl"
        Me.FNStyleType_lbl.Size = New System.Drawing.Size(187, 19)
        Me.FNStyleType_lbl.TabIndex = 259
        Me.FNStyleType_lbl.Tag = "2|"
        Me.FNStyleType_lbl.Text = "Style Type :"
        '
        'FTMapStyleName
        '
        Me.FTMapStyleName.EditValue = ""
        Me.FTMapStyleName.EnterMoveNextControl = True
        Me.FTMapStyleName.Location = New System.Drawing.Point(558, 49)
        Me.FTMapStyleName.Name = "FTMapStyleName"
        Me.FTMapStyleName.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FTMapStyleName.Properties.Appearance.Options.UseBackColor = True
        Me.FTMapStyleName.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FTMapStyleName.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FTMapStyleName.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FTMapStyleName.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FTMapStyleName.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FTMapStyleName.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FTMapStyleName.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FTMapStyleName.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FTMapStyleName.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FTMapStyleName.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FTMapStyleName.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FTMapStyleName.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FTMapStyleName.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FTMapStyleName.Properties.Tag = ""
        Me.FTMapStyleName.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.FTMapStyleName.Size = New System.Drawing.Size(149, 20)
        Me.FTMapStyleName.TabIndex = 256
        Me.FTMapStyleName.Tag = "2|"
        '
        'FTMapStyleName_lbl
        '
        Me.FTMapStyleName_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTMapStyleName_lbl.Appearance.Options.UseForeColor = True
        Me.FTMapStyleName_lbl.Appearance.Options.UseTextOptions = True
        Me.FTMapStyleName_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTMapStyleName_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTMapStyleName_lbl.Location = New System.Drawing.Point(365, 49)
        Me.FTMapStyleName_lbl.Name = "FTMapStyleName_lbl"
        Me.FTMapStyleName_lbl.Size = New System.Drawing.Size(187, 19)
        Me.FTMapStyleName_lbl.TabIndex = 257
        Me.FTMapStyleName_lbl.Tag = "2|"
        Me.FTMapStyleName_lbl.Text = "Mapping Field Style Name :"
        '
        'FTMapStyleCode
        '
        Me.FTMapStyleCode.EditValue = ""
        Me.FTMapStyleCode.EnterMoveNextControl = True
        Me.FTMapStyleCode.Location = New System.Drawing.Point(211, 49)
        Me.FTMapStyleCode.Name = "FTMapStyleCode"
        Me.FTMapStyleCode.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FTMapStyleCode.Properties.Appearance.Options.UseBackColor = True
        Me.FTMapStyleCode.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FTMapStyleCode.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FTMapStyleCode.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FTMapStyleCode.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FTMapStyleCode.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FTMapStyleCode.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FTMapStyleCode.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FTMapStyleCode.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FTMapStyleCode.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FTMapStyleCode.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FTMapStyleCode.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FTMapStyleCode.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FTMapStyleCode.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FTMapStyleCode.Properties.Tag = ""
        Me.FTMapStyleCode.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.FTMapStyleCode.Size = New System.Drawing.Size(149, 20)
        Me.FTMapStyleCode.TabIndex = 254
        Me.FTMapStyleCode.Tag = "2|"
        '
        'FTMapStyleCode_lbl
        '
        Me.FTMapStyleCode_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTMapStyleCode_lbl.Appearance.Options.UseForeColor = True
        Me.FTMapStyleCode_lbl.Appearance.Options.UseTextOptions = True
        Me.FTMapStyleCode_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTMapStyleCode_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTMapStyleCode_lbl.Location = New System.Drawing.Point(18, 49)
        Me.FTMapStyleCode_lbl.Name = "FTMapStyleCode_lbl"
        Me.FTMapStyleCode_lbl.Size = New System.Drawing.Size(187, 19)
        Me.FTMapStyleCode_lbl.TabIndex = 255
        Me.FTMapStyleCode_lbl.Tag = "2|"
        Me.FTMapStyleCode_lbl.Text = "Mapping Field Style Code :"
        '
        'FTFilePath
        '
        Me.FTFilePath.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FTFilePath.Location = New System.Drawing.Point(18, 23)
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
        Me.FTFilePath.Size = New System.Drawing.Size(945, 20)
        Me.FTFilePath.TabIndex = 1
        Me.FTFilePath.Tag = "2|"
        '
        'opshet
        '
        Me.opshet.Dock = System.Windows.Forms.DockStyle.Fill
        Me.opshet.Location = New System.Drawing.Point(2, 23)
        Me.opshet.Name = "opshet"
        Me.opshet.Options.Behavior.Column.Resize = DevExpress.XtraSpreadsheet.DocumentCapability.Enabled
        Me.opshet.ReadOnly = True
        Me.opshet.Size = New System.Drawing.Size(971, 515)
        Me.opshet.TabIndex = 1
        '
        'ogbmainprocbutton
        '
        Me.ogbmainprocbutton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmimportimmage)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmImportnetprice)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmexit)
        Me.ogbmainprocbutton.Location = New System.Drawing.Point(123, 196)
        Me.ogbmainprocbutton.Name = "ogbmainprocbutton"
        Me.ogbmainprocbutton.Size = New System.Drawing.Size(767, 149)
        Me.ogbmainprocbutton.TabIndex = 144
        Me.ogbmainprocbutton.Tag = "2|"
        '
        'ocmimportimmage
        '
        Me.ocmimportimmage.Location = New System.Drawing.Point(233, 22)
        Me.ocmimportimmage.Name = "ocmimportimmage"
        Me.ocmimportimmage.Size = New System.Drawing.Size(165, 25)
        Me.ocmimportimmage.TabIndex = 101
        Me.ocmimportimmage.TabStop = False
        Me.ocmimportimmage.Tag = "2|"
        Me.ocmimportimmage.Text = "IMPORT ImageStyle"
        '
        'ocmImportnetprice
        '
        Me.ocmImportnetprice.Location = New System.Drawing.Point(37, 22)
        Me.ocmImportnetprice.Name = "ocmImportnetprice"
        Me.ocmImportnetprice.Size = New System.Drawing.Size(165, 25)
        Me.ocmImportnetprice.TabIndex = 100
        Me.ocmImportnetprice.TabStop = False
        Me.ocmImportnetprice.Tag = "2|"
        Me.ocmImportnetprice.Text = "IMPORT ORDER NET PRICE."
        '
        'ocmexit
        '
        Me.ocmexit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmexit.Location = New System.Drawing.Point(654, 11)
        Me.ocmexit.Name = "ocmexit"
        Me.ocmexit.Size = New System.Drawing.Size(95, 25)
        Me.ocmexit.TabIndex = 96
        Me.ocmexit.TabStop = False
        Me.ocmexit.Tag = "2|"
        Me.ocmexit.Text = "EXIT"
        '
        'ogbselectfile
        '
        Me.ogbselectfile.Controls.Add(Me.opshet)
        Me.ogbselectfile.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogbselectfile.Location = New System.Drawing.Point(0, 102)
        Me.ogbselectfile.Name = "ogbselectfile"
        Me.ogbselectfile.Size = New System.Drawing.Size(975, 540)
        Me.ogbselectfile.TabIndex = 145
        Me.ogbselectfile.Text = "Data Import Info"
        '
        'wImportExcelStyle
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(975, 642)
        Me.Controls.Add(Me.ogbmainprocbutton)
        Me.Controls.Add(Me.ogbselectfile)
        Me.Controls.Add(Me.ogbdetail)
        Me.Name = "wImportExcelStyle"
        Me.Text = "Import Excel File Style"
        CType(Me.ogbdetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbdetail.ResumeLayout(False)
        CType(Me.FNStyleType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTMapStyleName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTMapStyleCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTFilePath.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbmainprocbutton.ResumeLayout(False)
        CType(Me.ogbselectfile, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbselectfile.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogbdetail As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ogbmainprocbutton As DevExpress.XtraEditors.PanelControl
    Friend WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ogbselectfile As DevExpress.XtraEditors.GroupControl
    Friend WithEvents FTFilePath As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents opshet As DevExpress.XtraSpreadsheet.SpreadsheetControl
    Friend WithEvents ocmImportnetprice As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FTMapStyleCode As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents FTMapStyleCode_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTMapStyleName As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents FTMapStyleName_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNStyleType As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents FNStyleType_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ocmimportimmage As DevExpress.XtraEditors.SimpleButton
End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wImportCostSheet
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
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject4 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject5 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.ogbselectfile = New DevExpress.XtraEditors.GroupControl()
        Me.FTFilePath = New DevExpress.XtraEditors.ButtonEdit()
        Me.ogbdetail = New DevExpress.XtraEditors.GroupControl()
        Me.opshet = New DevExpress.XtraSpreadsheet.SpreadsheetControl()
        Me.ogbmainprocbutton = New DevExpress.XtraEditors.PanelControl()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmImportOrder = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmclear = New DevExpress.XtraEditors.SimpleButton()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.FNCmp_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNCmp = New DevExpress.XtraEditors.CalcEdit()
        Me.FTUserImporttime = New DevExpress.XtraEditors.TextEdit()
        Me.FTUserImporttime_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTUserImport = New DevExpress.XtraEditors.TextEdit()
        Me.FTUserImport_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNAccImportCost_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNAccImportCost = New DevExpress.XtraEditors.CalcEdit()
        Me.FNFabricImportCost_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNFabricImportCost = New DevExpress.XtraEditors.CalcEdit()
        Me.FNAccCost_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNAccCost = New DevExpress.XtraEditors.CalcEdit()
        Me.FNFabricCost_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTSeasonCode = New DevExpress.XtraEditors.TextEdit()
        Me.FTSeasonCode_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTStyleCode = New DevExpress.XtraEditors.TextEdit()
        Me.FTStyleCode_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNFabricCost = New DevExpress.XtraEditors.CalcEdit()
        CType(Me.ogbselectfile, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbselectfile.SuspendLayout()
        CType(Me.FTFilePath.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogbdetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbdetail.SuspendLayout()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbmainprocbutton.SuspendLayout()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.FNCmp.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTUserImporttime.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTUserImport.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNAccImportCost.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNFabricImportCost.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNAccCost.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTSeasonCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTStyleCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNFabricCost.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ogbselectfile
        '
        Me.ogbselectfile.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbselectfile.Controls.Add(Me.FTFilePath)
        Me.ogbselectfile.Location = New System.Drawing.Point(2, 2)
        Me.ogbselectfile.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbselectfile.Name = "ogbselectfile"
        Me.ogbselectfile.Size = New System.Drawing.Size(922, 62)
        Me.ogbselectfile.TabIndex = 0
        Me.ogbselectfile.Text = "Select File"
        '
        'FTFilePath
        '
        Me.FTFilePath.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FTFilePath.Location = New System.Drawing.Point(12, 32)
        Me.FTFilePath.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
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
        Me.FTFilePath.Size = New System.Drawing.Size(898, 22)
        Me.FTFilePath.TabIndex = 1
        Me.FTFilePath.Tag = "2|"
        '
        'ogbdetail
        '
        Me.ogbdetail.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbdetail.Controls.Add(Me.opshet)
        Me.ogbdetail.Location = New System.Drawing.Point(2, 200)
        Me.ogbdetail.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbdetail.Name = "ogbdetail"
        Me.ogbdetail.Size = New System.Drawing.Size(922, 503)
        Me.ogbdetail.TabIndex = 1
        Me.ogbdetail.Text = "File Detail"
        '
        'opshet
        '
        Me.opshet.Dock = System.Windows.Forms.DockStyle.Fill
        Me.opshet.Location = New System.Drawing.Point(2, 24)
        Me.opshet.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.opshet.Name = "opshet"
        Me.opshet.Options.Behavior.Column.Resize = DevExpress.XtraSpreadsheet.DocumentCapability.Enabled
        Me.opshet.ReadOnly = True
        Me.opshet.Size = New System.Drawing.Size(918, 477)
        Me.opshet.TabIndex = 0
        '
        'ogbmainprocbutton
        '
        Me.ogbmainprocbutton.Controls.Add(Me.ocmexit)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmImportOrder)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmclear)
        Me.ogbmainprocbutton.Location = New System.Drawing.Point(96, 304)
        Me.ogbmainprocbutton.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbmainprocbutton.Name = "ogbmainprocbutton"
        Me.ogbmainprocbutton.Size = New System.Drawing.Size(736, 95)
        Me.ogbmainprocbutton.TabIndex = 391
        '
        'ocmexit
        '
        Me.ocmexit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmexit.Location = New System.Drawing.Point(492, 52)
        Me.ocmexit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmexit.Name = "ocmexit"
        Me.ocmexit.Size = New System.Drawing.Size(111, 31)
        Me.ocmexit.TabIndex = 96
        Me.ocmexit.TabStop = False
        Me.ocmexit.Tag = "2|"
        Me.ocmexit.Text = "EXIT"
        '
        'ocmImportOrder
        '
        Me.ocmImportOrder.Location = New System.Drawing.Point(6, 14)
        Me.ocmImportOrder.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmImportOrder.Name = "ocmImportOrder"
        Me.ocmImportOrder.Size = New System.Drawing.Size(111, 31)
        Me.ocmImportOrder.TabIndex = 93
        Me.ocmImportOrder.TabStop = False
        Me.ocmImportOrder.Tag = "2|"
        Me.ocmImportOrder.Text = "Import"
        '
        'ocmclear
        '
        Me.ocmclear.Location = New System.Drawing.Point(124, 14)
        Me.ocmclear.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmclear.Name = "ocmclear"
        Me.ocmclear.Size = New System.Drawing.Size(111, 31)
        Me.ocmclear.TabIndex = 95
        Me.ocmclear.TabStop = False
        Me.ocmclear.Tag = "2|"
        Me.ocmclear.Text = "CLEAR"
        '
        'GroupControl1
        '
        Me.GroupControl1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupControl1.Controls.Add(Me.FNCmp_lbl)
        Me.GroupControl1.Controls.Add(Me.FNCmp)
        Me.GroupControl1.Controls.Add(Me.FTUserImporttime)
        Me.GroupControl1.Controls.Add(Me.FTUserImporttime_lbl)
        Me.GroupControl1.Controls.Add(Me.FTUserImport)
        Me.GroupControl1.Controls.Add(Me.FTUserImport_lbl)
        Me.GroupControl1.Controls.Add(Me.FNAccImportCost_lbl)
        Me.GroupControl1.Controls.Add(Me.FNAccImportCost)
        Me.GroupControl1.Controls.Add(Me.FNFabricImportCost_lbl)
        Me.GroupControl1.Controls.Add(Me.FNFabricImportCost)
        Me.GroupControl1.Controls.Add(Me.FNAccCost_lbl)
        Me.GroupControl1.Controls.Add(Me.FNAccCost)
        Me.GroupControl1.Controls.Add(Me.FNFabricCost_lbl)
        Me.GroupControl1.Controls.Add(Me.FTSeasonCode)
        Me.GroupControl1.Controls.Add(Me.FTSeasonCode_lbl)
        Me.GroupControl1.Controls.Add(Me.FTStyleCode)
        Me.GroupControl1.Controls.Add(Me.FTStyleCode_lbl)
        Me.GroupControl1.Controls.Add(Me.FNFabricCost)
        Me.GroupControl1.Location = New System.Drawing.Point(4, 72)
        Me.GroupControl1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(918, 120)
        Me.GroupControl1.TabIndex = 392
        Me.GroupControl1.Text = "Select File"
        '
        'FNCmp_lbl
        '
        Me.FNCmp_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNCmp_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNCmp_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNCmp_lbl.Location = New System.Drawing.Point(599, 28)
        Me.FNCmp_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNCmp_lbl.Name = "FNCmp_lbl"
        Me.FNCmp_lbl.Size = New System.Drawing.Size(159, 22)
        Me.FNCmp_lbl.TabIndex = 457
        Me.FNCmp_lbl.Tag = "2|"
        Me.FNCmp_lbl.Text = "CMP :"
        '
        'FNCmp
        '
        Me.FNCmp.Location = New System.Drawing.Point(763, 28)
        Me.FNCmp.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNCmp.Name = "FNCmp"
        Me.FNCmp.Properties.Appearance.BackColor = System.Drawing.Color.LightCyan
        Me.FNCmp.Properties.Appearance.Options.UseBackColor = True
        Me.FNCmp.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, True, False, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", Nothing, Nothing, True)})
        Me.FNCmp.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None
        Me.FNCmp.Properties.Precision = 4
        Me.FNCmp.Properties.ReadOnly = True
        Me.FNCmp.Size = New System.Drawing.Size(123, 22)
        Me.FNCmp.TabIndex = 458
        Me.FNCmp.Tag = ""
        '
        'FTUserImporttime
        '
        Me.FTUserImporttime.Location = New System.Drawing.Point(763, 80)
        Me.FTUserImporttime.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTUserImporttime.Name = "FTUserImporttime"
        Me.FTUserImporttime.Properties.Appearance.BackColor = System.Drawing.Color.LightCyan
        Me.FTUserImporttime.Properties.Appearance.Options.UseBackColor = True
        Me.FTUserImporttime.Properties.ReadOnly = True
        Me.FTUserImporttime.Size = New System.Drawing.Size(145, 22)
        Me.FTUserImporttime.TabIndex = 456
        Me.FTUserImporttime.Tag = ""
        '
        'FTUserImporttime_lbl
        '
        Me.FTUserImporttime_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTUserImporttime_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTUserImporttime_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTUserImporttime_lbl.Location = New System.Drawing.Point(600, 79)
        Me.FTUserImporttime_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTUserImporttime_lbl.Name = "FTUserImporttime_lbl"
        Me.FTUserImporttime_lbl.Size = New System.Drawing.Size(158, 22)
        Me.FTUserImporttime_lbl.TabIndex = 455
        Me.FTUserImporttime_lbl.Tag = "2|"
        Me.FTUserImporttime_lbl.Text = "Time :"
        '
        'FTUserImport
        '
        Me.FTUserImport.Location = New System.Drawing.Point(763, 54)
        Me.FTUserImport.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTUserImport.Name = "FTUserImport"
        Me.FTUserImport.Properties.Appearance.BackColor = System.Drawing.Color.LightCyan
        Me.FTUserImport.Properties.Appearance.Options.UseBackColor = True
        Me.FTUserImport.Properties.ReadOnly = True
        Me.FTUserImport.Size = New System.Drawing.Size(145, 22)
        Me.FTUserImport.TabIndex = 454
        Me.FTUserImport.Tag = ""
        '
        'FTUserImport_lbl
        '
        Me.FTUserImport_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTUserImport_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTUserImport_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTUserImport_lbl.Location = New System.Drawing.Point(599, 56)
        Me.FTUserImport_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTUserImport_lbl.Name = "FTUserImport_lbl"
        Me.FTUserImport_lbl.Size = New System.Drawing.Size(158, 22)
        Me.FTUserImport_lbl.TabIndex = 453
        Me.FTUserImport_lbl.Tag = "2|"
        Me.FTUserImport_lbl.Text = "User Import :"
        '
        'FNAccImportCost_lbl
        '
        Me.FNAccImportCost_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNAccImportCost_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNAccImportCost_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNAccImportCost_lbl.Location = New System.Drawing.Point(301, 81)
        Me.FNAccImportCost_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNAccImportCost_lbl.Name = "FNAccImportCost_lbl"
        Me.FNAccImportCost_lbl.Size = New System.Drawing.Size(159, 22)
        Me.FNAccImportCost_lbl.TabIndex = 451
        Me.FNAccImportCost_lbl.Tag = "2|"
        Me.FNAccImportCost_lbl.Text = "Accessory Import Cost :"
        '
        'FNAccImportCost
        '
        Me.FNAccImportCost.Location = New System.Drawing.Point(465, 81)
        Me.FNAccImportCost.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNAccImportCost.Name = "FNAccImportCost"
        Me.FNAccImportCost.Properties.Appearance.BackColor = System.Drawing.Color.LightCyan
        Me.FNAccImportCost.Properties.Appearance.Options.UseBackColor = True
        Me.FNAccImportCost.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, True, False, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject2, "", Nothing, Nothing, True)})
        Me.FNAccImportCost.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None
        Me.FNAccImportCost.Properties.Precision = 4
        Me.FNAccImportCost.Properties.ReadOnly = True
        Me.FNAccImportCost.Size = New System.Drawing.Size(123, 22)
        Me.FNAccImportCost.TabIndex = 452
        Me.FNAccImportCost.Tag = ""
        '
        'FNFabricImportCost_lbl
        '
        Me.FNFabricImportCost_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNFabricImportCost_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNFabricImportCost_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNFabricImportCost_lbl.Location = New System.Drawing.Point(8, 82)
        Me.FNFabricImportCost_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNFabricImportCost_lbl.Name = "FNFabricImportCost_lbl"
        Me.FNFabricImportCost_lbl.Size = New System.Drawing.Size(158, 22)
        Me.FNFabricImportCost_lbl.TabIndex = 449
        Me.FNFabricImportCost_lbl.Tag = "2|"
        Me.FNFabricImportCost_lbl.Text = "Fabric Import Cost :"
        '
        'FNFabricImportCost
        '
        Me.FNFabricImportCost.Location = New System.Drawing.Point(172, 82)
        Me.FNFabricImportCost.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNFabricImportCost.Name = "FNFabricImportCost"
        Me.FNFabricImportCost.Properties.Appearance.BackColor = System.Drawing.Color.LightCyan
        Me.FNFabricImportCost.Properties.Appearance.Options.UseBackColor = True
        Me.FNFabricImportCost.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, True, False, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject3, "", Nothing, Nothing, True)})
        Me.FNFabricImportCost.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None
        Me.FNFabricImportCost.Properties.Precision = 4
        Me.FNFabricImportCost.Properties.ReadOnly = True
        Me.FNFabricImportCost.Size = New System.Drawing.Size(125, 22)
        Me.FNFabricImportCost.TabIndex = 450
        Me.FNFabricImportCost.Tag = ""
        '
        'FNAccCost_lbl
        '
        Me.FNAccCost_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNAccCost_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNAccCost_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNAccCost_lbl.Location = New System.Drawing.Point(301, 54)
        Me.FNAccCost_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNAccCost_lbl.Name = "FNAccCost_lbl"
        Me.FNAccCost_lbl.Size = New System.Drawing.Size(159, 22)
        Me.FNAccCost_lbl.TabIndex = 447
        Me.FNAccCost_lbl.Tag = "2|"
        Me.FNAccCost_lbl.Text = "Accessory Cost :"
        '
        'FNAccCost
        '
        Me.FNAccCost.Location = New System.Drawing.Point(465, 54)
        Me.FNAccCost.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNAccCost.Name = "FNAccCost"
        Me.FNAccCost.Properties.Appearance.BackColor = System.Drawing.Color.LightCyan
        Me.FNAccCost.Properties.Appearance.Options.UseBackColor = True
        Me.FNAccCost.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, True, False, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject4, "", Nothing, Nothing, True)})
        Me.FNAccCost.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None
        Me.FNAccCost.Properties.Precision = 4
        Me.FNAccCost.Properties.ReadOnly = True
        Me.FNAccCost.Size = New System.Drawing.Size(123, 22)
        Me.FNAccCost.TabIndex = 448
        Me.FNAccCost.Tag = ""
        '
        'FNFabricCost_lbl
        '
        Me.FNFabricCost_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNFabricCost_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNFabricCost_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNFabricCost_lbl.Location = New System.Drawing.Point(8, 55)
        Me.FNFabricCost_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNFabricCost_lbl.Name = "FNFabricCost_lbl"
        Me.FNFabricCost_lbl.Size = New System.Drawing.Size(158, 22)
        Me.FNFabricCost_lbl.TabIndex = 445
        Me.FNFabricCost_lbl.Tag = "2|"
        Me.FNFabricCost_lbl.Text = "Fabric Cost :"
        '
        'FTSeasonCode
        '
        Me.FTSeasonCode.Location = New System.Drawing.Point(465, 28)
        Me.FTSeasonCode.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTSeasonCode.Name = "FTSeasonCode"
        Me.FTSeasonCode.Properties.Appearance.BackColor = System.Drawing.Color.LightCyan
        Me.FTSeasonCode.Properties.Appearance.Options.UseBackColor = True
        Me.FTSeasonCode.Properties.ReadOnly = True
        Me.FTSeasonCode.Size = New System.Drawing.Size(123, 22)
        Me.FTSeasonCode.TabIndex = 444
        Me.FTSeasonCode.Tag = ""
        '
        'FTSeasonCode_lbl
        '
        Me.FTSeasonCode_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTSeasonCode_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTSeasonCode_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTSeasonCode_lbl.Location = New System.Drawing.Point(301, 28)
        Me.FTSeasonCode_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTSeasonCode_lbl.Name = "FTSeasonCode_lbl"
        Me.FTSeasonCode_lbl.Size = New System.Drawing.Size(159, 22)
        Me.FTSeasonCode_lbl.TabIndex = 443
        Me.FTSeasonCode_lbl.Tag = "2|"
        Me.FTSeasonCode_lbl.Text = "Season :"
        '
        'FTStyleCode
        '
        Me.FTStyleCode.Location = New System.Drawing.Point(172, 28)
        Me.FTStyleCode.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTStyleCode.Name = "FTStyleCode"
        Me.FTStyleCode.Properties.Appearance.BackColor = System.Drawing.Color.LightCyan
        Me.FTStyleCode.Properties.Appearance.Options.UseBackColor = True
        Me.FTStyleCode.Properties.ReadOnly = True
        Me.FTStyleCode.Size = New System.Drawing.Size(125, 22)
        Me.FTStyleCode.TabIndex = 442
        Me.FTStyleCode.Tag = ""
        '
        'FTStyleCode_lbl
        '
        Me.FTStyleCode_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTStyleCode_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTStyleCode_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTStyleCode_lbl.Location = New System.Drawing.Point(8, 28)
        Me.FTStyleCode_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTStyleCode_lbl.Name = "FTStyleCode_lbl"
        Me.FTStyleCode_lbl.Size = New System.Drawing.Size(158, 22)
        Me.FTStyleCode_lbl.TabIndex = 440
        Me.FTStyleCode_lbl.Tag = "2|"
        Me.FTStyleCode_lbl.Text = "Style Code :"
        '
        'FNFabricCost
        '
        Me.FNFabricCost.Location = New System.Drawing.Point(172, 55)
        Me.FNFabricCost.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNFabricCost.Name = "FNFabricCost"
        Me.FNFabricCost.Properties.Appearance.BackColor = System.Drawing.Color.LightCyan
        Me.FNFabricCost.Properties.Appearance.Options.UseBackColor = True
        Me.FNFabricCost.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, True, False, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject5, "", Nothing, Nothing, True)})
        Me.FNFabricCost.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None
        Me.FNFabricCost.Properties.Precision = 4
        Me.FNFabricCost.Properties.ReadOnly = True
        Me.FNFabricCost.Size = New System.Drawing.Size(125, 22)
        Me.FNFabricCost.TabIndex = 446
        Me.FNFabricCost.Tag = ""
        '
        'wImportCostSheet
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(926, 703)
        Me.Controls.Add(Me.GroupControl1)
        Me.Controls.Add(Me.ogbmainprocbutton)
        Me.Controls.Add(Me.ogbdetail)
        Me.Controls.Add(Me.ogbselectfile)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "wImportCostSheet"
        Me.Text = "Import Cost Sheet"
        CType(Me.ogbselectfile, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbselectfile.ResumeLayout(False)
        CType(Me.FTFilePath.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogbdetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbdetail.ResumeLayout(False)
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbmainprocbutton.ResumeLayout(False)
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        CType(Me.FNCmp.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTUserImporttime.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTUserImport.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNAccImportCost.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNFabricImportCost.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNAccCost.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTSeasonCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTStyleCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNFabricCost.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogbselectfile As DevExpress.XtraEditors.GroupControl
    Friend WithEvents FTFilePath As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents ogbdetail As DevExpress.XtraEditors.GroupControl
    Friend WithEvents opshet As DevExpress.XtraSpreadsheet.SpreadsheetControl
    Friend WithEvents ogbmainprocbutton As DevExpress.XtraEditors.PanelControl
    Friend WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmImportOrder As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmclear As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents FTStyleCode_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNAccImportCost_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNAccImportCost As DevExpress.XtraEditors.CalcEdit
    Friend WithEvents FNFabricImportCost_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNFabricImportCost As DevExpress.XtraEditors.CalcEdit
    Friend WithEvents FNAccCost_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNAccCost As DevExpress.XtraEditors.CalcEdit
    Friend WithEvents FNFabricCost_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTSeasonCode As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FTSeasonCode_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTStyleCode As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FNFabricCost As DevExpress.XtraEditors.CalcEdit
    Friend WithEvents FTUserImporttime As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FTUserImporttime_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTUserImport As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FTUserImport_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNCmp_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNCmp As DevExpress.XtraEditors.CalcEdit
End Class

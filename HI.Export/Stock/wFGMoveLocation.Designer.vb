<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wFGMoveLocation
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
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.ogbmainprocbutton = New DevExpress.XtraEditors.PanelControl()
        Me.ocmpreview = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmremove = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmToWHFG = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmload = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmsaveweightpack = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmclear = New DevExpress.XtraEditors.SimpleButton()
        Me.ogrpbarcode = New DevExpress.XtraEditors.GroupControl()
        Me.FNMoveType = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.ogrpbarcodeInfo = New DevExpress.XtraEditors.GroupControl()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.FTProductBarcodeNo = New DevExpress.XtraEditors.TextEdit()
        Me.FTBarcodeLocation = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysWHLocId_None = New DevExpress.XtraEditors.TextEdit()
        Me.FNHSysWHLocId = New DevExpress.XtraEditors.ButtonEdit()
        Me.FTStateMoveType_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysWHLocId_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysWHFGId_None = New DevExpress.XtraEditors.TextEdit()
        Me.FNHSysWHFGId = New DevExpress.XtraEditors.ButtonEdit()
        Me.FNHSysWHFGId_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.ogrpfinishgoods = New DevExpress.XtraEditors.GroupControl()
        Me.ogcwarehouse = New DevExpress.XtraGrid.GridControl()
        Me.ogvwarehouse = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GFTSelect = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryGFTSelect = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.GFTBarCodeCarton = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GFTOrderNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GFTStyleCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GFTColorWay = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GFNQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GFTPORef = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GFTSubOrderNo = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbmainprocbutton.SuspendLayout()
        CType(Me.ogrpbarcode, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogrpbarcode.SuspendLayout()
        CType(Me.FNMoveType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogrpbarcodeInfo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogrpbarcodeInfo.SuspendLayout()
        CType(Me.FTProductBarcodeNo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTBarcodeLocation.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysWHLocId_None.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysWHLocId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysWHFGId_None.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysWHFGId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogrpfinishgoods, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogrpfinishgoods.SuspendLayout()
        CType(Me.ogcwarehouse, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvwarehouse, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryGFTSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ogbmainprocbutton
        '
        Me.ogbmainprocbutton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmpreview)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmremove)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmToWHFG)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmload)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmsaveweightpack)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmexit)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmclear)
        Me.ogbmainprocbutton.Location = New System.Drawing.Point(318, 111)
        Me.ogbmainprocbutton.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbmainprocbutton.Name = "ogbmainprocbutton"
        Me.ogbmainprocbutton.Size = New System.Drawing.Size(568, 142)
        Me.ogbmainprocbutton.TabIndex = 393
        Me.ogbmainprocbutton.Tag = "2|"
        '
        'ocmpreview
        '
        Me.ocmpreview.Location = New System.Drawing.Point(250, 55)
        Me.ocmpreview.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmpreview.Name = "ocmpreview"
        Me.ocmpreview.Size = New System.Drawing.Size(68, 31)
        Me.ocmpreview.TabIndex = 331
        Me.ocmpreview.TabStop = False
        Me.ocmpreview.Tag = "2|"
        Me.ocmpreview.Text = "PREVIEW"
        '
        'ocmremove
        '
        Me.ocmremove.Location = New System.Drawing.Point(27, 90)
        Me.ocmremove.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmremove.Name = "ocmremove"
        Me.ocmremove.Size = New System.Drawing.Size(136, 28)
        Me.ocmremove.TabIndex = 330
        Me.ocmremove.Text = "Delete"
        '
        'ocmToWHFG
        '
        Me.ocmToWHFG.Location = New System.Drawing.Point(246, 90)
        Me.ocmToWHFG.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmToWHFG.Name = "ocmToWHFG"
        Me.ocmToWHFG.Size = New System.Drawing.Size(136, 28)
        Me.ocmToWHFG.TabIndex = 330
        Me.ocmToWHFG.Text = "In FG.Stock"
        '
        'ocmload
        '
        Me.ocmload.Location = New System.Drawing.Point(27, 54)
        Me.ocmload.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmload.Name = "ocmload"
        Me.ocmload.Size = New System.Drawing.Size(136, 28)
        Me.ocmload.TabIndex = 330
        Me.ocmload.Text = "Load Data"
        '
        'ocmsaveweightpack
        '
        Me.ocmsaveweightpack.Location = New System.Drawing.Point(246, 16)
        Me.ocmsaveweightpack.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmsaveweightpack.Name = "ocmsaveweightpack"
        Me.ocmsaveweightpack.Size = New System.Drawing.Size(97, 31)
        Me.ocmsaveweightpack.TabIndex = 105
        Me.ocmsaveweightpack.TabStop = False
        Me.ocmsaveweightpack.Tag = "2|"
        Me.ocmsaveweightpack.Text = "saveweight"
        Me.ocmsaveweightpack.Visible = False
        '
        'ocmexit
        '
        Me.ocmexit.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.ocmexit.Location = New System.Drawing.Point(52, 16)
        Me.ocmexit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmexit.Name = "ocmexit"
        Me.ocmexit.Size = New System.Drawing.Size(111, 31)
        Me.ocmexit.TabIndex = 96
        Me.ocmexit.TabStop = False
        Me.ocmexit.Tag = "2|"
        Me.ocmexit.Text = "EXIT"
        '
        'ocmclear
        '
        Me.ocmclear.Location = New System.Drawing.Point(176, 16)
        Me.ocmclear.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmclear.Name = "ocmclear"
        Me.ocmclear.Size = New System.Drawing.Size(63, 31)
        Me.ocmclear.TabIndex = 95
        Me.ocmclear.TabStop = False
        Me.ocmclear.Tag = "2|"
        Me.ocmclear.Text = "CLEAR"
        '
        'ogrpbarcode
        '
        Me.ogrpbarcode.Controls.Add(Me.FNMoveType)
        Me.ogrpbarcode.Controls.Add(Me.ogrpbarcodeInfo)
        Me.ogrpbarcode.Controls.Add(Me.FNHSysWHLocId_None)
        Me.ogrpbarcode.Controls.Add(Me.FNHSysWHLocId)
        Me.ogrpbarcode.Controls.Add(Me.FTStateMoveType_lbl)
        Me.ogrpbarcode.Controls.Add(Me.FNHSysWHLocId_lbl)
        Me.ogrpbarcode.Controls.Add(Me.FNHSysWHFGId_None)
        Me.ogrpbarcode.Controls.Add(Me.FNHSysWHFGId)
        Me.ogrpbarcode.Controls.Add(Me.FNHSysWHFGId_lbl)
        Me.ogrpbarcode.Dock = System.Windows.Forms.DockStyle.Top
        Me.ogrpbarcode.Location = New System.Drawing.Point(0, 0)
        Me.ogrpbarcode.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogrpbarcode.Name = "ogrpbarcode"
        Me.ogrpbarcode.Size = New System.Drawing.Size(1277, 302)
        Me.ogrpbarcode.TabIndex = 0
        Me.ogrpbarcode.Text = "Scan Barcode In Warehouse Finish Goods"
        '
        'FNMoveType
        '
        Me.FNMoveType.Location = New System.Drawing.Point(164, 94)
        Me.FNMoveType.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.FNMoveType.Name = "FNMoveType"
        Me.FNMoveType.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FNMoveType.Properties.Tag = "FNMoveType"
        Me.FNMoveType.Size = New System.Drawing.Size(146, 23)
        Me.FNMoveType.TabIndex = 404
        '
        'ogrpbarcodeInfo
        '
        Me.ogrpbarcodeInfo.Controls.Add(Me.LabelControl1)
        Me.ogrpbarcodeInfo.Controls.Add(Me.FTProductBarcodeNo)
        Me.ogrpbarcodeInfo.Controls.Add(Me.FTBarcodeLocation)
        Me.ogrpbarcodeInfo.Controls.Add(Me.LabelControl2)
        Me.ogrpbarcodeInfo.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ogrpbarcodeInfo.Location = New System.Drawing.Point(2, 119)
        Me.ogrpbarcodeInfo.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.ogrpbarcodeInfo.Name = "ogrpbarcodeInfo"
        Me.ogrpbarcodeInfo.Size = New System.Drawing.Size(1273, 181)
        Me.ogrpbarcodeInfo.TabIndex = 403
        Me.ogrpbarcodeInfo.Text = "Barcode Input"
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.LabelControl1.Appearance.Options.UseForeColor = True
        Me.LabelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LabelControl1.Location = New System.Drawing.Point(5, 30)
        Me.LabelControl1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(148, 23)
        Me.LabelControl1.TabIndex = 400
        Me.LabelControl1.Tag = "2|"
        Me.LabelControl1.Text = "Barcode Location :"
        '
        'FTProductBarcodeNo
        '
        Me.FTProductBarcodeNo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FTProductBarcodeNo.EditValue = ""
        Me.FTProductBarcodeNo.Location = New System.Drawing.Point(651, 57)
        Me.FTProductBarcodeNo.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTProductBarcodeNo.Name = "FTProductBarcodeNo"
        Me.FTProductBarcodeNo.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 26.0!, System.Drawing.FontStyle.Bold)
        Me.FTProductBarcodeNo.Properties.Appearance.Options.UseFont = True
        Me.FTProductBarcodeNo.Properties.AutoHeight = False
        Me.FTProductBarcodeNo.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.FTProductBarcodeNo.Size = New System.Drawing.Size(612, 96)
        Me.FTProductBarcodeNo.TabIndex = 1
        Me.FTProductBarcodeNo.Tag = "2|"
        '
        'FTBarcodeLocation
        '
        Me.FTBarcodeLocation.EditValue = ""
        Me.FTBarcodeLocation.Location = New System.Drawing.Point(5, 57)
        Me.FTBarcodeLocation.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTBarcodeLocation.Name = "FTBarcodeLocation"
        Me.FTBarcodeLocation.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 26.0!, System.Drawing.FontStyle.Bold)
        Me.FTBarcodeLocation.Properties.Appearance.Options.UseFont = True
        Me.FTBarcodeLocation.Properties.AutoHeight = False
        Me.FTBarcodeLocation.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.FTBarcodeLocation.Size = New System.Drawing.Size(579, 96)
        Me.FTBarcodeLocation.TabIndex = 1
        Me.FTBarcodeLocation.Tag = "2|"
        '
        'LabelControl2
        '
        Me.LabelControl2.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.LabelControl2.Appearance.Options.UseForeColor = True
        Me.LabelControl2.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LabelControl2.Location = New System.Drawing.Point(651, 30)
        Me.LabelControl2.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(148, 23)
        Me.LabelControl2.TabIndex = 400
        Me.LabelControl2.Tag = "2|"
        Me.LabelControl2.Text = "Barcode Pallet:"
        '
        'FNHSysWHLocId_None
        '
        Me.FNHSysWHLocId_None.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FNHSysWHLocId_None.EnterMoveNextControl = True
        Me.FNHSysWHLocId_None.Location = New System.Drawing.Point(316, 64)
        Me.FNHSysWHLocId_None.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysWHLocId_None.Name = "FNHSysWHLocId_None"
        Me.FNHSysWHLocId_None.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FNHSysWHLocId_None.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FNHSysWHLocId_None.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysWHLocId_None.Properties.Appearance.Options.UseForeColor = True
        Me.FNHSysWHLocId_None.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysWHLocId_None.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysWHLocId_None.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FNHSysWHLocId_None.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FNHSysWHLocId_None.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNHSysWHLocId_None.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysWHLocId_None.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNHSysWHLocId_None.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNHSysWHLocId_None.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysWHLocId_None.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysWHLocId_None.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNHSysWHLocId_None.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNHSysWHLocId_None.Properties.ReadOnly = True
        Me.FNHSysWHLocId_None.Size = New System.Drawing.Size(934, 23)
        Me.FNHSysWHLocId_None.TabIndex = 402
        Me.FNHSysWHLocId_None.TabStop = False
        Me.FNHSysWHLocId_None.Tag = "2|"
        '
        'FNHSysWHLocId
        '
        Me.FNHSysWHLocId.Location = New System.Drawing.Point(164, 64)
        Me.FNHSysWHLocId.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysWHLocId.Name = "FNHSysWHLocId"
        Me.FNHSysWHLocId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", "492", Nothing, True)})
        Me.FNHSysWHLocId.Size = New System.Drawing.Size(146, 23)
        Me.FNHSysWHLocId.TabIndex = 401
        Me.FNHSysWHLocId.Tag = "2|"
        '
        'FTStateMoveType_lbl
        '
        Me.FTStateMoveType_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTStateMoveType_lbl.Appearance.Options.UseForeColor = True
        Me.FTStateMoveType_lbl.Appearance.Options.UseTextOptions = True
        Me.FTStateMoveType_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTStateMoveType_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTStateMoveType_lbl.Location = New System.Drawing.Point(12, 90)
        Me.FTStateMoveType_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTStateMoveType_lbl.Name = "FTStateMoveType_lbl"
        Me.FTStateMoveType_lbl.Size = New System.Drawing.Size(148, 23)
        Me.FTStateMoveType_lbl.TabIndex = 400
        Me.FTStateMoveType_lbl.Tag = "2|"
        Me.FTStateMoveType_lbl.Text = "Move Type :"
        '
        'FNHSysWHLocId_lbl
        '
        Me.FNHSysWHLocId_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysWHLocId_lbl.Appearance.Options.UseForeColor = True
        Me.FNHSysWHLocId_lbl.Appearance.Options.UseTextOptions = True
        Me.FNHSysWHLocId_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysWHLocId_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysWHLocId_lbl.Location = New System.Drawing.Point(10, 59)
        Me.FNHSysWHLocId_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysWHLocId_lbl.Name = "FNHSysWHLocId_lbl"
        Me.FNHSysWHLocId_lbl.Size = New System.Drawing.Size(148, 23)
        Me.FNHSysWHLocId_lbl.TabIndex = 400
        Me.FNHSysWHLocId_lbl.Tag = "2|"
        Me.FNHSysWHLocId_lbl.Text = "FNHSysWHLocFGId :"
        '
        'FNHSysWHFGId_None
        '
        Me.FNHSysWHFGId_None.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FNHSysWHFGId_None.EnterMoveNextControl = True
        Me.FNHSysWHFGId_None.Location = New System.Drawing.Point(316, 34)
        Me.FNHSysWHFGId_None.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysWHFGId_None.Name = "FNHSysWHFGId_None"
        Me.FNHSysWHFGId_None.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FNHSysWHFGId_None.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FNHSysWHFGId_None.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysWHFGId_None.Properties.Appearance.Options.UseForeColor = True
        Me.FNHSysWHFGId_None.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysWHFGId_None.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysWHFGId_None.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FNHSysWHFGId_None.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FNHSysWHFGId_None.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNHSysWHFGId_None.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysWHFGId_None.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNHSysWHFGId_None.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNHSysWHFGId_None.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysWHFGId_None.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysWHFGId_None.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNHSysWHFGId_None.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNHSysWHFGId_None.Properties.ReadOnly = True
        Me.FNHSysWHFGId_None.Size = New System.Drawing.Size(934, 23)
        Me.FNHSysWHFGId_None.TabIndex = 303
        Me.FNHSysWHFGId_None.TabStop = False
        Me.FNHSysWHFGId_None.Tag = "2|"
        '
        'FNHSysWHFGId
        '
        Me.FNHSysWHFGId.Location = New System.Drawing.Point(164, 34)
        Me.FNHSysWHFGId.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysWHFGId.Name = "FNHSysWHFGId"
        Me.FNHSysWHFGId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject2, "", "282", Nothing, True)})
        Me.FNHSysWHFGId.Size = New System.Drawing.Size(146, 23)
        Me.FNHSysWHFGId.TabIndex = 302
        Me.FNHSysWHFGId.Tag = "2|"
        '
        'FNHSysWHFGId_lbl
        '
        Me.FNHSysWHFGId_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysWHFGId_lbl.Appearance.Options.UseForeColor = True
        Me.FNHSysWHFGId_lbl.Appearance.Options.UseTextOptions = True
        Me.FNHSysWHFGId_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysWHFGId_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysWHFGId_lbl.Location = New System.Drawing.Point(10, 33)
        Me.FNHSysWHFGId_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysWHFGId_lbl.Name = "FNHSysWHFGId_lbl"
        Me.FNHSysWHFGId_lbl.Size = New System.Drawing.Size(148, 23)
        Me.FNHSysWHFGId_lbl.TabIndex = 301
        Me.FNHSysWHFGId_lbl.Tag = "2|"
        Me.FNHSysWHFGId_lbl.Text = "FNHSysWHFGId :"
        '
        'ogrpfinishgoods
        '
        Me.ogrpfinishgoods.AppearanceCaption.Options.UseTextOptions = True
        Me.ogrpfinishgoods.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ogrpfinishgoods.Controls.Add(Me.ogbmainprocbutton)
        Me.ogrpfinishgoods.Controls.Add(Me.ogcwarehouse)
        Me.ogrpfinishgoods.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogrpfinishgoods.Location = New System.Drawing.Point(0, 302)
        Me.ogrpfinishgoods.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogrpfinishgoods.Name = "ogrpfinishgoods"
        Me.ogrpfinishgoods.Size = New System.Drawing.Size(1277, 444)
        Me.ogrpfinishgoods.TabIndex = 0
        Me.ogrpfinishgoods.Text = "Finish Goods In Warehouse To Day"
        '
        'ogcwarehouse
        '
        Me.ogcwarehouse.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogcwarehouse.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcwarehouse.Location = New System.Drawing.Point(2, 25)
        Me.ogcwarehouse.MainView = Me.ogvwarehouse
        Me.ogcwarehouse.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcwarehouse.Name = "ogcwarehouse"
        Me.ogcwarehouse.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryGFTSelect})
        Me.ogcwarehouse.Size = New System.Drawing.Size(1273, 417)
        Me.ogcwarehouse.TabIndex = 0
        Me.ogcwarehouse.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvwarehouse})
        '
        'ogvwarehouse
        '
        Me.ogvwarehouse.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GFTSelect, Me.GFTBarCodeCarton, Me.GFTOrderNo, Me.GFTStyleCode, Me.GFTColorWay, Me.GFNQuantity, Me.GFTPORef, Me.GFTSubOrderNo})
        Me.ogvwarehouse.GridControl = Me.ogcwarehouse
        Me.ogvwarehouse.Name = "ogvwarehouse"
        Me.ogvwarehouse.OptionsView.ColumnAutoWidth = False
        Me.ogvwarehouse.OptionsView.ShowGroupPanel = False
        '
        'GFTSelect
        '
        Me.GFTSelect.Caption = "FTSelect"
        Me.GFTSelect.ColumnEdit = Me.RepositoryGFTSelect
        Me.GFTSelect.FieldName = "FTSelect"
        Me.GFTSelect.Name = "GFTSelect"
        Me.GFTSelect.Width = 52
        '
        'RepositoryGFTSelect
        '
        Me.RepositoryGFTSelect.AutoHeight = False
        Me.RepositoryGFTSelect.Caption = "Check"
        Me.RepositoryGFTSelect.Name = "RepositoryGFTSelect"
        Me.RepositoryGFTSelect.ValueChecked = "1"
        Me.RepositoryGFTSelect.ValueUnchecked = "0"
        '
        'GFTBarCodeCarton
        '
        Me.GFTBarCodeCarton.Caption = "FTBarCodeCarton"
        Me.GFTBarCodeCarton.FieldName = "FTBarCodeCarton"
        Me.GFTBarCodeCarton.Name = "GFTBarCodeCarton"
        Me.GFTBarCodeCarton.OptionsColumn.AllowEdit = False
        Me.GFTBarCodeCarton.Visible = True
        Me.GFTBarCodeCarton.VisibleIndex = 0
        Me.GFTBarCodeCarton.Width = 250
        '
        'GFTOrderNo
        '
        Me.GFTOrderNo.Caption = "FTOrderNo"
        Me.GFTOrderNo.FieldName = "FTOrderNo"
        Me.GFTOrderNo.Name = "GFTOrderNo"
        Me.GFTOrderNo.OptionsColumn.AllowEdit = False
        Me.GFTOrderNo.Visible = True
        Me.GFTOrderNo.VisibleIndex = 2
        Me.GFTOrderNo.Width = 136
        '
        'GFTStyleCode
        '
        Me.GFTStyleCode.Caption = "FTStyleCode"
        Me.GFTStyleCode.FieldName = "FTStyleCode"
        Me.GFTStyleCode.Name = "GFTStyleCode"
        Me.GFTStyleCode.OptionsColumn.AllowEdit = False
        Me.GFTStyleCode.Visible = True
        Me.GFTStyleCode.VisibleIndex = 4
        Me.GFTStyleCode.Width = 107
        '
        'GFTColorWay
        '
        Me.GFTColorWay.Caption = "FTColorWay"
        Me.GFTColorWay.FieldName = "FTColorWay"
        Me.GFTColorWay.Name = "GFTColorWay"
        Me.GFTColorWay.OptionsColumn.AllowEdit = False
        Me.GFTColorWay.Visible = True
        Me.GFTColorWay.VisibleIndex = 5
        Me.GFTColorWay.Width = 144
        '
        'GFNQuantity
        '
        Me.GFNQuantity.Caption = "FNQuantity"
        Me.GFNQuantity.FieldName = "FNQuantity"
        Me.GFNQuantity.Name = "GFNQuantity"
        Me.GFNQuantity.OptionsColumn.AllowEdit = False
        Me.GFNQuantity.Visible = True
        Me.GFNQuantity.VisibleIndex = 6
        Me.GFNQuantity.Width = 112
        '
        'GFTPORef
        '
        Me.GFTPORef.Caption = "FTPORef"
        Me.GFTPORef.FieldName = "FTPORef"
        Me.GFTPORef.Name = "GFTPORef"
        Me.GFTPORef.OptionsColumn.AllowEdit = False
        Me.GFTPORef.Visible = True
        Me.GFTPORef.VisibleIndex = 1
        Me.GFTPORef.Width = 98
        '
        'GFTSubOrderNo
        '
        Me.GFTSubOrderNo.Caption = "FTSubOrderNo"
        Me.GFTSubOrderNo.FieldName = "FTSubOrderNo"
        Me.GFTSubOrderNo.Name = "GFTSubOrderNo"
        Me.GFTSubOrderNo.OptionsColumn.AllowEdit = False
        Me.GFTSubOrderNo.Visible = True
        Me.GFTSubOrderNo.VisibleIndex = 3
        Me.GFTSubOrderNo.Width = 135
        '
        'wFGMoveLocation
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1277, 746)
        Me.Controls.Add(Me.ogrpfinishgoods)
        Me.Controls.Add(Me.ogrpbarcode)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "wFGMoveLocation"
        Me.Text = "wFGMoveLocation"
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbmainprocbutton.ResumeLayout(False)
        CType(Me.ogrpbarcode, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogrpbarcode.ResumeLayout(False)
        CType(Me.FNMoveType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogrpbarcodeInfo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogrpbarcodeInfo.ResumeLayout(False)
        CType(Me.FTProductBarcodeNo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTBarcodeLocation.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysWHLocId_None.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysWHLocId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysWHFGId_None.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysWHFGId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogrpfinishgoods, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogrpfinishgoods.ResumeLayout(False)
        CType(Me.ogcwarehouse, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvwarehouse, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryGFTSelect, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogrpbarcode As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ogrpfinishgoods As DevExpress.XtraEditors.GroupControl
    Friend WithEvents FTProductBarcodeNo As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FNHSysWHFGId_None As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FNHSysWHFGId As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FNHSysWHFGId_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ogbmainprocbutton As DevExpress.XtraEditors.PanelControl
    Friend WithEvents ocmsaveweightpack As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmclear As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmload As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ogcwarehouse As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvwarehouse As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GFTBarCodeCarton As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GFTOrderNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GFTColorWay As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GFNQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ocmremove As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents GFTSelect As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryGFTSelect As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents GFTStyleCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GFTPORef As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GFTSubOrderNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ocmpreview As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmToWHFG As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FTBarcodeLocation As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FNHSysWHLocId_None As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FNHSysWHLocId As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FNHSysWHLocId_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ogrpbarcodeInfo As DevExpress.XtraEditors.GroupControl
    Friend WithEvents FTStateMoveType_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNMoveType As DevExpress.XtraEditors.ComboBoxEdit
End Class

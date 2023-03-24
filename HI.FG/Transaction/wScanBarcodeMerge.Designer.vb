<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wScanBarcodeMerge
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
        Dim Code128Generator1 As DevExpress.XtraPrinting.BarCode.Code128Generator = New DevExpress.XtraPrinting.BarCode.Code128Generator()
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.ogbmainprocbutton = New DevExpress.XtraEditors.PanelControl()
        Me.ocmpreview = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmremove = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmload = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmsaveweightpack = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmclear = New DevExpress.XtraEditors.SimpleButton()
        Me.ogrpbarcode = New DevExpress.XtraEditors.GroupControl()
        Me.FTBarCodePallet = New DevExpress.XtraEditors.BarCodeControl()
        Me.FNHSysWHLocId_None = New DevExpress.XtraEditors.TextEdit()
        Me.FNHSysWHLocId = New DevExpress.XtraEditors.ButtonEdit()
        Me.FNHSysWHFGId_None = New DevExpress.XtraEditors.TextEdit()
        Me.FNHSysWHLocId_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysWHFGId = New DevExpress.XtraEditors.ButtonEdit()
        Me.FNHSysWHFGId_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTStateDeleteBarcode = New DevExpress.XtraEditors.CheckEdit()
        Me.FTProductBarcodeNo = New DevExpress.XtraEditors.TextEdit()
        Me.ogrpfinishgoods = New DevExpress.XtraEditors.GroupControl()
        Me.oGFTSelectAll = New DevExpress.XtraEditors.CheckEdit()
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
        Me.ogpPalletInfo = New DevExpress.XtraEditors.GroupControl()
        Me.ogcBalCarton = New DevExpress.XtraGrid.GridControl()
        Me.ogvBalCarton = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.FNHSysCartonId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTCartonCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNCartonBal = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNCartonTotal = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNCBM = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCheckEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.ogpcartontotal = New DevExpress.XtraEditors.GroupControl()
        Me.lblQtyScan = New DevExpress.XtraEditors.LabelControl()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbmainprocbutton.SuspendLayout()
        CType(Me.ogrpbarcode, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogrpbarcode.SuspendLayout()
        CType(Me.FNHSysWHLocId_None.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysWHLocId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysWHFGId_None.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysWHFGId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTStateDeleteBarcode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTProductBarcodeNo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogrpfinishgoods, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogrpfinishgoods.SuspendLayout()
        CType(Me.oGFTSelectAll.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogcwarehouse, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvwarehouse, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryGFTSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogpPalletInfo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogpPalletInfo.SuspendLayout()
        CType(Me.ogcBalCarton, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvBalCarton, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogpcartontotal, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogpcartontotal.SuspendLayout()
        Me.SuspendLayout()
        '
        'ogbmainprocbutton
        '
        Me.ogbmainprocbutton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmpreview)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmremove)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmload)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmsaveweightpack)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmexit)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmclear)
        Me.ogbmainprocbutton.Location = New System.Drawing.Point(318, 111)
        Me.ogbmainprocbutton.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbmainprocbutton.Name = "ogbmainprocbutton"
        Me.ogbmainprocbutton.Size = New System.Drawing.Size(568, 141)
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
        Me.ogrpbarcode.Controls.Add(Me.FTBarCodePallet)
        Me.ogrpbarcode.Controls.Add(Me.FNHSysWHLocId_None)
        Me.ogrpbarcode.Controls.Add(Me.FNHSysWHLocId)
        Me.ogrpbarcode.Controls.Add(Me.FNHSysWHFGId_None)
        Me.ogrpbarcode.Controls.Add(Me.FNHSysWHLocId_lbl)
        Me.ogrpbarcode.Controls.Add(Me.FNHSysWHFGId)
        Me.ogrpbarcode.Controls.Add(Me.FNHSysWHFGId_lbl)
        Me.ogrpbarcode.Controls.Add(Me.FTStateDeleteBarcode)
        Me.ogrpbarcode.Controls.Add(Me.FTProductBarcodeNo)
        Me.ogrpbarcode.Dock = System.Windows.Forms.DockStyle.Top
        Me.ogrpbarcode.Location = New System.Drawing.Point(0, 0)
        Me.ogrpbarcode.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogrpbarcode.Name = "ogrpbarcode"
        Me.ogrpbarcode.Size = New System.Drawing.Size(1277, 211)
        Me.ogrpbarcode.TabIndex = 0
        Me.ogrpbarcode.Text = "Scan Barcode In Warehouse Finish Goods"
        '
        'FTBarCodePallet
        '
        Me.FTBarCodePallet.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FTBarCodePallet.Location = New System.Drawing.Point(651, 98)
        Me.FTBarCodePallet.Name = "FTBarCodePallet"
        Me.FTBarCodePallet.Padding = New System.Windows.Forms.Padding(10, 2, 10, 0)
        Me.FTBarCodePallet.Size = New System.Drawing.Size(611, 83)
        Me.FTBarCodePallet.Symbology = Code128Generator1
        Me.FTBarCodePallet.TabIndex = 394
        Me.FTBarCodePallet.Tag = "2|"
        '
        'FNHSysWHLocId_None
        '
        Me.FNHSysWHLocId_None.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FNHSysWHLocId_None.EnterMoveNextControl = True
        Me.FNHSysWHLocId_None.Location = New System.Drawing.Point(371, 54)
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
        Me.FNHSysWHLocId_None.Size = New System.Drawing.Size(891, 22)
        Me.FNHSysWHLocId_None.TabIndex = 303
        Me.FNHSysWHLocId_None.TabStop = False
        Me.FNHSysWHLocId_None.Tag = "2|"
        '
        'FNHSysWHLocId
        '
        Me.FNHSysWHLocId.Location = New System.Drawing.Point(222, 54)
        Me.FNHSysWHLocId.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysWHLocId.Name = "FNHSysWHLocId"
        Me.FNHSysWHLocId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", "492", Nothing, True)})
        Me.FNHSysWHLocId.Size = New System.Drawing.Size(146, 22)
        Me.FNHSysWHLocId.TabIndex = 302
        Me.FNHSysWHLocId.Tag = "2|"
        '
        'FNHSysWHFGId_None
        '
        Me.FNHSysWHFGId_None.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FNHSysWHFGId_None.EnterMoveNextControl = True
        Me.FNHSysWHFGId_None.Location = New System.Drawing.Point(371, 28)
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
        Me.FNHSysWHFGId_None.Size = New System.Drawing.Size(891, 22)
        Me.FNHSysWHFGId_None.TabIndex = 303
        Me.FNHSysWHFGId_None.TabStop = False
        Me.FNHSysWHFGId_None.Tag = "2|"
        '
        'FNHSysWHLocId_lbl
        '
        Me.FNHSysWHLocId_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysWHLocId_lbl.Appearance.Options.UseForeColor = True
        Me.FNHSysWHLocId_lbl.Appearance.Options.UseTextOptions = True
        Me.FNHSysWHLocId_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysWHLocId_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysWHLocId_lbl.Location = New System.Drawing.Point(10, 54)
        Me.FNHSysWHLocId_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysWHLocId_lbl.Name = "FNHSysWHLocId_lbl"
        Me.FNHSysWHLocId_lbl.Size = New System.Drawing.Size(204, 23)
        Me.FNHSysWHLocId_lbl.TabIndex = 301
        Me.FNHSysWHLocId_lbl.Tag = "2|"
        Me.FNHSysWHLocId_lbl.Text = "FNHSysWHLocFGId :"
        '
        'FNHSysWHFGId
        '
        Me.FNHSysWHFGId.Location = New System.Drawing.Point(222, 28)
        Me.FNHSysWHFGId.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysWHFGId.Name = "FNHSysWHFGId"
        Me.FNHSysWHFGId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject2, "", "282", Nothing, True)})
        Me.FNHSysWHFGId.Size = New System.Drawing.Size(146, 22)
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
        Me.FNHSysWHFGId_lbl.Location = New System.Drawing.Point(10, 28)
        Me.FNHSysWHFGId_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysWHFGId_lbl.Name = "FNHSysWHFGId_lbl"
        Me.FNHSysWHFGId_lbl.Size = New System.Drawing.Size(204, 23)
        Me.FNHSysWHFGId_lbl.TabIndex = 301
        Me.FNHSysWHFGId_lbl.Tag = "2|"
        Me.FNHSysWHFGId_lbl.Text = "FNHSysWHFGId :"
        '
        'FTStateDeleteBarcode
        '
        Me.FTStateDeleteBarcode.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FTStateDeleteBarcode.Location = New System.Drawing.Point(7, 185)
        Me.FTStateDeleteBarcode.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTStateDeleteBarcode.Name = "FTStateDeleteBarcode"
        Me.FTStateDeleteBarcode.Properties.Caption = "Delete Barcode"
        Me.FTStateDeleteBarcode.Size = New System.Drawing.Size(610, 20)
        Me.FTStateDeleteBarcode.TabIndex = 2
        Me.FTStateDeleteBarcode.Tag = "2|"
        '
        'FTProductBarcodeNo
        '
        Me.FTProductBarcodeNo.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FTProductBarcodeNo.EditValue = ""
        Me.FTProductBarcodeNo.Location = New System.Drawing.Point(7, 85)
        Me.FTProductBarcodeNo.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTProductBarcodeNo.Name = "FTProductBarcodeNo"
        Me.FTProductBarcodeNo.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 26.0!, System.Drawing.FontStyle.Bold)
        Me.FTProductBarcodeNo.Properties.Appearance.Options.UseFont = True
        Me.FTProductBarcodeNo.Properties.AutoHeight = False
        Me.FTProductBarcodeNo.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.FTProductBarcodeNo.Size = New System.Drawing.Size(638, 96)
        Me.FTProductBarcodeNo.TabIndex = 1
        Me.FTProductBarcodeNo.Tag = "2|"
        '
        'ogrpfinishgoods
        '
        Me.ogrpfinishgoods.AppearanceCaption.Options.UseTextOptions = True
        Me.ogrpfinishgoods.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ogrpfinishgoods.Controls.Add(Me.oGFTSelectAll)
        Me.ogrpfinishgoods.Controls.Add(Me.ogbmainprocbutton)
        Me.ogrpfinishgoods.Controls.Add(Me.ogcwarehouse)
        Me.ogrpfinishgoods.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogrpfinishgoods.Location = New System.Drawing.Point(0, 454)
        Me.ogrpfinishgoods.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogrpfinishgoods.Name = "ogrpfinishgoods"
        Me.ogrpfinishgoods.Size = New System.Drawing.Size(1277, 292)
        Me.ogrpfinishgoods.TabIndex = 0
        Me.ogrpfinishgoods.Text = "Finish Goods In Warehouse To Day"
        '
        'oGFTSelectAll
        '
        Me.oGFTSelectAll.Location = New System.Drawing.Point(7, 2)
        Me.oGFTSelectAll.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.oGFTSelectAll.Name = "oGFTSelectAll"
        Me.oGFTSelectAll.Properties.Caption = "Select All"
        Me.oGFTSelectAll.Size = New System.Drawing.Size(173, 20)
        Me.oGFTSelectAll.TabIndex = 1
        Me.oGFTSelectAll.Visible = False
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
        Me.ogcwarehouse.Size = New System.Drawing.Size(1273, 265)
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
        'ogpPalletInfo
        '
        Me.ogpPalletInfo.Controls.Add(Me.ogcBalCarton)
        Me.ogpPalletInfo.Controls.Add(Me.ogpcartontotal)
        Me.ogpPalletInfo.Dock = System.Windows.Forms.DockStyle.Top
        Me.ogpPalletInfo.Location = New System.Drawing.Point(0, 211)
        Me.ogpPalletInfo.Name = "ogpPalletInfo"
        Me.ogpPalletInfo.Size = New System.Drawing.Size(1277, 243)
        Me.ogpPalletInfo.TabIndex = 1
        Me.ogpPalletInfo.Text = "Pallet Info"
        '
        'ogcBalCarton
        '
        Me.ogcBalCarton.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogcBalCarton.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcBalCarton.Location = New System.Drawing.Point(2, 25)
        Me.ogcBalCarton.MainView = Me.ogvBalCarton
        Me.ogcBalCarton.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcBalCarton.Name = "ogcBalCarton"
        Me.ogcBalCarton.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCheckEdit1})
        Me.ogcBalCarton.Size = New System.Drawing.Size(994, 216)
        Me.ogcBalCarton.TabIndex = 1
        Me.ogcBalCarton.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvBalCarton})
        '
        'ogvBalCarton
        '
        Me.ogvBalCarton.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.FNHSysCartonId, Me.FTCartonCode, Me.FNCartonBal, Me.FNCartonTotal, Me.FNCBM, Me.GridColumn1})
        Me.ogvBalCarton.GridControl = Me.ogcBalCarton
        Me.ogvBalCarton.Name = "ogvBalCarton"
        Me.ogvBalCarton.OptionsView.ColumnAutoWidth = False
        Me.ogvBalCarton.OptionsView.ShowFooter = True
        Me.ogvBalCarton.OptionsView.ShowGroupPanel = False
        '
        'FNHSysCartonId
        '
        Me.FNHSysCartonId.Caption = "FNHSysCartonId"
        Me.FNHSysCartonId.FieldName = "FNHSysCartonId"
        Me.FNHSysCartonId.Name = "FNHSysCartonId"
        '
        'FTCartonCode
        '
        Me.FTCartonCode.Caption = "Carton Code"
        Me.FTCartonCode.FieldName = "FTCartonCode"
        Me.FTCartonCode.Name = "FTCartonCode"
        Me.FTCartonCode.OptionsColumn.AllowEdit = False
        Me.FTCartonCode.Visible = True
        Me.FTCartonCode.VisibleIndex = 0
        Me.FTCartonCode.Width = 80
        '
        'FNCartonBal
        '
        Me.FNCartonBal.Caption = "Caton Bal"
        Me.FNCartonBal.DisplayFormat.FormatString = "N0"
        Me.FNCartonBal.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNCartonBal.FieldName = "FNCartonBal"
        Me.FNCartonBal.Name = "FNCartonBal"
        Me.FNCartonBal.OptionsColumn.AllowEdit = False
        Me.FNCartonBal.Visible = True
        Me.FNCartonBal.VisibleIndex = 1
        Me.FNCartonBal.Width = 77
        '
        'FNCartonTotal
        '
        Me.FNCartonTotal.Caption = "Caton Total"
        Me.FNCartonTotal.DisplayFormat.FormatString = "N0"
        Me.FNCartonTotal.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNCartonTotal.FieldName = "FNCartonTotal"
        Me.FNCartonTotal.Name = "FNCartonTotal"
        Me.FNCartonTotal.OptionsColumn.AllowEdit = False
        Me.FNCartonTotal.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FNCartonTotal", "{0:N0}")})
        Me.FNCartonTotal.Visible = True
        Me.FNCartonTotal.VisibleIndex = 2
        Me.FNCartonTotal.Width = 84
        '
        'FNCBM
        '
        Me.FNCBM.Caption = "CBM"
        Me.FNCBM.DisplayFormat.FormatString = "N2"
        Me.FNCBM.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNCBM.FieldName = "FNCBM"
        Me.FNCBM.Name = "FNCBM"
        Me.FNCBM.OptionsColumn.AllowEdit = False
        Me.FNCBM.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FNCBM", "{0:N2}")})
        Me.FNCBM.Visible = True
        Me.FNCBM.VisibleIndex = 3
        Me.FNCBM.Width = 79
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "GridColumn1"
        Me.GridColumn1.FieldName = "Carton"
        Me.GridColumn1.Name = "GridColumn1"
        '
        'RepositoryItemCheckEdit1
        '
        Me.RepositoryItemCheckEdit1.AutoHeight = False
        Me.RepositoryItemCheckEdit1.Caption = "Check"
        Me.RepositoryItemCheckEdit1.Name = "RepositoryItemCheckEdit1"
        Me.RepositoryItemCheckEdit1.ValueChecked = "1"
        Me.RepositoryItemCheckEdit1.ValueUnchecked = "0"
        '
        'ogpcartontotal
        '
        Me.ogpcartontotal.Controls.Add(Me.lblQtyScan)
        Me.ogpcartontotal.Dock = System.Windows.Forms.DockStyle.Right
        Me.ogpcartontotal.Location = New System.Drawing.Point(996, 25)
        Me.ogpcartontotal.Name = "ogpcartontotal"
        Me.ogpcartontotal.Size = New System.Drawing.Size(279, 216)
        Me.ogpcartontotal.TabIndex = 0
        Me.ogpcartontotal.Text = "Total Carton"
        '
        'lblQtyScan
        '
        Me.lblQtyScan.Appearance.Font = New System.Drawing.Font("Tahoma", 66.0!, System.Drawing.FontStyle.Bold)
        Me.lblQtyScan.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.lblQtyScan.Appearance.Options.UseFont = True
        Me.lblQtyScan.Appearance.Options.UseForeColor = True
        Me.lblQtyScan.Appearance.Options.UseTextOptions = True
        Me.lblQtyScan.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lblQtyScan.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.lblQtyScan.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblQtyScan.Location = New System.Drawing.Point(2, 25)
        Me.lblQtyScan.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.lblQtyScan.Name = "lblQtyScan"
        Me.lblQtyScan.Size = New System.Drawing.Size(275, 189)
        Me.lblQtyScan.TabIndex = 310
        Me.lblQtyScan.Tag = "2|"
        Me.lblQtyScan.Text = "000"
        '
        'wScanBarcodeMerge
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1277, 746)
        Me.Controls.Add(Me.ogrpfinishgoods)
        Me.Controls.Add(Me.ogpPalletInfo)
        Me.Controls.Add(Me.ogrpbarcode)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "wScanBarcodeMerge"
        Me.Text = "wScanBarcodeMerge"
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbmainprocbutton.ResumeLayout(False)
        CType(Me.ogrpbarcode, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogrpbarcode.ResumeLayout(False)
        CType(Me.FNHSysWHLocId_None.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysWHLocId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysWHFGId_None.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysWHFGId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTStateDeleteBarcode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTProductBarcodeNo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogrpfinishgoods, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogrpfinishgoods.ResumeLayout(False)
        CType(Me.oGFTSelectAll.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogcwarehouse, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvwarehouse, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryGFTSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogpPalletInfo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogpPalletInfo.ResumeLayout(False)
        CType(Me.ogcBalCarton, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvBalCarton, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogpcartontotal, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogpcartontotal.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogrpbarcode As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ogrpfinishgoods As DevExpress.XtraEditors.GroupControl
    Friend WithEvents FTProductBarcodeNo As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FTStateDeleteBarcode As DevExpress.XtraEditors.CheckEdit
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
    Friend WithEvents oGFTSelectAll As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents GFTStyleCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GFTPORef As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GFTSubOrderNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNHSysWHLocId_None As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FNHSysWHLocId As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FNHSysWHLocId_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ogpPalletInfo As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ogpcartontotal As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ogcBalCarton As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvBalCarton As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents FNHSysCartonId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTCartonCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNCartonBal As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNCartonTotal As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNCBM As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemCheckEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents lblQtyScan As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTBarCodePallet As DevExpress.XtraEditors.BarCodeControl
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ocmpreview As DevExpress.XtraEditors.SimpleButton
End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wSearchBarcodeAsset
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
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.ocmfind = New DevExpress.XtraEditors.SimpleButton()
        Me.ogbdetail = New DevExpress.XtraEditors.GroupControl()
        Me.ogcdetail = New DevExpress.XtraGrid.GridControl()
        Me.ogvdetail = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.CFTSelect = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ReposFTSelect = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.cFTBarcodeNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTAssetCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTAssetName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNHSysFixedAssetId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTDocumentNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTWHAssetCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTWHAssetName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTUnitAssetCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNQuantityIN = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNQuantityOUT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ogbmainprocbutton = New DevExpress.XtraEditors.PanelControl()
        Me.ocmpreview = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmclear = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.ogbsearch = New DevExpress.XtraEditors.GroupControl()
        Me.otpsearchbybarcode = New DevExpress.XtraTab.XtraTabPage()
        Me.FTBarcodeNo = New DevExpress.XtraEditors.TextEdit()
        Me.FTBarcodeNo_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.otpsearch = New DevExpress.XtraTab.XtraTabPage()
        Me.FTDocumentNo = New DevExpress.XtraEditors.ButtonEdit()
        Me.FTOrderNo_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysWHAssetId_None = New DevExpress.XtraEditors.TextEdit()
        Me.FNHSysWHId_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysWHAssetId = New DevExpress.XtraEditors.ButtonEdit()
        Me.otbmainsearch = New DevExpress.XtraTab.XtraTabControl()
        CType(Me.ogbdetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbdetail.SuspendLayout()
        CType(Me.ogcdetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvdetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ReposFTSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbmainprocbutton.SuspendLayout()
        CType(Me.ogbsearch, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbsearch.SuspendLayout()
        Me.otpsearchbybarcode.SuspendLayout()
        CType(Me.FTBarcodeNo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.otpsearch.SuspendLayout()
        CType(Me.FTDocumentNo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysWHAssetId_None.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysWHAssetId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.otbmainsearch, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.otbmainsearch.SuspendLayout()
        Me.SuspendLayout()
        '
        'ocmfind
        '
        Me.ocmfind.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmfind.Location = New System.Drawing.Point(17, 14)
        Me.ocmfind.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmfind.Name = "ocmfind"
        Me.ocmfind.Size = New System.Drawing.Size(117, 28)
        Me.ocmfind.TabIndex = 293
        Me.ocmfind.Text = "Search"
        '
        'ogbdetail
        '
        Me.ogbdetail.Controls.Add(Me.ogcdetail)
        Me.ogbdetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogbdetail.Location = New System.Drawing.Point(0, 151)
        Me.ogbdetail.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbdetail.Name = "ogbdetail"
        Me.ogbdetail.Size = New System.Drawing.Size(1552, 554)
        Me.ogbdetail.TabIndex = 1
        Me.ogbdetail.Text = "Barcode Detail"
        '
        'ogcdetail
        '
        Me.ogcdetail.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogcdetail.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcdetail.Location = New System.Drawing.Point(6, 30)
        Me.ogcdetail.MainView = Me.ogvdetail
        Me.ogcdetail.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcdetail.Name = "ogcdetail"
        Me.ogcdetail.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.ReposFTSelect})
        Me.ogcdetail.Size = New System.Drawing.Size(1541, 518)
        Me.ogcdetail.TabIndex = 3
        Me.ogcdetail.TabStop = False
        Me.ogcdetail.Tag = "2|"
        Me.ogcdetail.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvdetail})
        '
        'ogvdetail
        '
        Me.ogvdetail.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.CFTSelect, Me.cFTBarcodeNo, Me.FTAssetCode, Me.FTAssetName, Me.FNHSysFixedAssetId, Me.cFTDocumentNo, Me.FTWHAssetCode, Me.FTWHAssetName, Me.FTUnitAssetCode, Me.FNQuantity, Me.FNQuantityIN, Me.FNQuantityOUT})
        Me.ogvdetail.GridControl = Me.ogcdetail
        Me.ogvdetail.Name = "ogvdetail"
        Me.ogvdetail.OptionsCustomization.AllowGroup = False
        Me.ogvdetail.OptionsCustomization.AllowQuickHideColumns = False
        Me.ogvdetail.OptionsView.ColumnAutoWidth = False
        Me.ogvdetail.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.ogvdetail.OptionsView.ShowAutoFilterRow = True
        Me.ogvdetail.OptionsView.ShowFooter = True
        Me.ogvdetail.OptionsView.ShowGroupPanel = False
        Me.ogvdetail.Tag = "2|"
        '
        'CFTSelect
        '
        Me.CFTSelect.Caption = " "
        Me.CFTSelect.ColumnEdit = Me.ReposFTSelect
        Me.CFTSelect.FieldName = "FTSelect"
        Me.CFTSelect.Name = "CFTSelect"
        Me.CFTSelect.OptionsColumn.AllowEdit = False
        Me.CFTSelect.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFTSelect.OptionsColumn.AllowMove = False
        Me.CFTSelect.OptionsColumn.AllowShowHide = False
        Me.CFTSelect.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFTSelect.OptionsColumn.FixedWidth = True
        Me.CFTSelect.OptionsColumn.ReadOnly = True
        Me.CFTSelect.Visible = True
        Me.CFTSelect.VisibleIndex = 0
        Me.CFTSelect.Width = 40
        '
        'ReposFTSelect
        '
        Me.ReposFTSelect.AutoHeight = False
        Me.ReposFTSelect.Name = "ReposFTSelect"
        Me.ReposFTSelect.ValueChecked = "1"
        Me.ReposFTSelect.ValueUnchecked = "0"
        '
        'cFTBarcodeNo
        '
        Me.cFTBarcodeNo.Caption = "FTBarcodeNo"
        Me.cFTBarcodeNo.FieldName = "FTBarcodeNo"
        Me.cFTBarcodeNo.Name = "cFTBarcodeNo"
        Me.cFTBarcodeNo.OptionsColumn.ReadOnly = True
        Me.cFTBarcodeNo.Visible = True
        Me.cFTBarcodeNo.VisibleIndex = 1
        Me.cFTBarcodeNo.Width = 100
        '
        'FTAssetCode
        '
        Me.FTAssetCode.Caption = "FTAssetCode"
        Me.FTAssetCode.FieldName = "FTAssetCode"
        Me.FTAssetCode.Name = "FTAssetCode"
        Me.FTAssetCode.OptionsColumn.AllowEdit = False
        Me.FTAssetCode.OptionsColumn.ReadOnly = True
        Me.FTAssetCode.Visible = True
        Me.FTAssetCode.VisibleIndex = 2
        '
        'FTAssetName
        '
        Me.FTAssetName.Caption = "FTAssetName"
        Me.FTAssetName.FieldName = "FTAssetName"
        Me.FTAssetName.Name = "FTAssetName"
        Me.FTAssetName.OptionsColumn.AllowEdit = False
        Me.FTAssetName.OptionsColumn.ReadOnly = True
        Me.FTAssetName.Visible = True
        Me.FTAssetName.VisibleIndex = 3
        Me.FTAssetName.Width = 100
        '
        'FNHSysFixedAssetId
        '
        Me.FNHSysFixedAssetId.Caption = "FNHSysFixedAssetId"
        Me.FNHSysFixedAssetId.FieldName = "FNHSysFixedAssetId"
        Me.FNHSysFixedAssetId.Name = "FNHSysFixedAssetId"
        Me.FNHSysFixedAssetId.OptionsColumn.AllowEdit = False
        Me.FNHSysFixedAssetId.OptionsColumn.ReadOnly = True
        Me.FNHSysFixedAssetId.Visible = True
        Me.FNHSysFixedAssetId.VisibleIndex = 4
        '
        'cFTDocumentNo
        '
        Me.cFTDocumentNo.Caption = "FTDocumentNo"
        Me.cFTDocumentNo.FieldName = "FTDocumentNo"
        Me.cFTDocumentNo.Name = "cFTDocumentNo"
        Me.cFTDocumentNo.OptionsColumn.AllowEdit = False
        Me.cFTDocumentNo.OptionsColumn.ReadOnly = True
        Me.cFTDocumentNo.Width = 150
        '
        'FTWHAssetCode
        '
        Me.FTWHAssetCode.Caption = "FTWHAssetCode"
        Me.FTWHAssetCode.FieldName = "FTWHAssetCode"
        Me.FTWHAssetCode.Name = "FTWHAssetCode"
        Me.FTWHAssetCode.OptionsColumn.AllowEdit = False
        Me.FTWHAssetCode.OptionsColumn.ReadOnly = True
        Me.FTWHAssetCode.Visible = True
        Me.FTWHAssetCode.VisibleIndex = 5
        '
        'FTWHAssetName
        '
        Me.FTWHAssetName.Caption = "FTWHAssetName"
        Me.FTWHAssetName.FieldName = "FTWHAssetName"
        Me.FTWHAssetName.Name = "FTWHAssetName"
        Me.FTWHAssetName.OptionsColumn.AllowEdit = False
        Me.FTWHAssetName.OptionsColumn.ReadOnly = True
        Me.FTWHAssetName.Visible = True
        Me.FTWHAssetName.VisibleIndex = 6
        Me.FTWHAssetName.Width = 100
        '
        'FTUnitAssetCode
        '
        Me.FTUnitAssetCode.Caption = "FTUnitAssetCode"
        Me.FTUnitAssetCode.FieldName = "FTUnitAssetCode"
        Me.FTUnitAssetCode.Name = "FTUnitAssetCode"
        Me.FTUnitAssetCode.OptionsColumn.AllowEdit = False
        Me.FTUnitAssetCode.OptionsColumn.ReadOnly = True
        Me.FTUnitAssetCode.Visible = True
        Me.FTUnitAssetCode.VisibleIndex = 7
        '
        'FNQuantity
        '
        Me.FNQuantity.Caption = "FNQuantity"
        Me.FNQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNQuantity.FieldName = "FNQuantity"
        Me.FNQuantity.Name = "FNQuantity"
        Me.FNQuantity.OptionsColumn.AllowEdit = False
        Me.FNQuantity.OptionsColumn.ReadOnly = True
        Me.FNQuantity.Visible = True
        Me.FNQuantity.VisibleIndex = 8
        '
        'FNQuantityIN
        '
        Me.FNQuantityIN.Caption = "FNQuantityIN"
        Me.FNQuantityIN.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNQuantityIN.FieldName = "FNQuantityIN"
        Me.FNQuantityIN.Name = "FNQuantityIN"
        Me.FNQuantityIN.OptionsColumn.AllowEdit = False
        Me.FNQuantityIN.OptionsColumn.ReadOnly = True
        Me.FNQuantityIN.Visible = True
        Me.FNQuantityIN.VisibleIndex = 9
        '
        'FNQuantityOUT
        '
        Me.FNQuantityOUT.Caption = "FNQuantityOUT"
        Me.FNQuantityOUT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNQuantityOUT.FieldName = "FNQuantityOUT"
        Me.FNQuantityOUT.Name = "FNQuantityOUT"
        Me.FNQuantityOUT.OptionsColumn.AllowEdit = False
        Me.FNQuantityOUT.OptionsColumn.ReadOnly = True
        Me.FNQuantityOUT.Visible = True
        Me.FNQuantityOUT.VisibleIndex = 10
        '
        'ogbmainprocbutton
        '
        Me.ogbmainprocbutton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmpreview)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmclear)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmfind)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmexit)
        Me.ogbmainprocbutton.Location = New System.Drawing.Point(426, 263)
        Me.ogbmainprocbutton.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbmainprocbutton.Name = "ogbmainprocbutton"
        Me.ogbmainprocbutton.Size = New System.Drawing.Size(1120, 58)
        Me.ogbmainprocbutton.TabIndex = 144
        Me.ogbmainprocbutton.Tag = "2|"
        '
        'ocmpreview
        '
        Me.ocmpreview.Location = New System.Drawing.Point(505, 14)
        Me.ocmpreview.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmpreview.Name = "ocmpreview"
        Me.ocmpreview.Size = New System.Drawing.Size(111, 31)
        Me.ocmpreview.TabIndex = 295
        Me.ocmpreview.TabStop = False
        Me.ocmpreview.Tag = "2|"
        Me.ocmpreview.Text = "PREVIEW"
        '
        'ocmclear
        '
        Me.ocmclear.Location = New System.Drawing.Point(142, 11)
        Me.ocmclear.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmclear.Name = "ocmclear"
        Me.ocmclear.Size = New System.Drawing.Size(111, 31)
        Me.ocmclear.TabIndex = 294
        Me.ocmclear.TabStop = False
        Me.ocmclear.Tag = "2|"
        Me.ocmclear.Text = "CLEAR"
        '
        'ocmexit
        '
        Me.ocmexit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmexit.Location = New System.Drawing.Point(988, 14)
        Me.ocmexit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmexit.Name = "ocmexit"
        Me.ocmexit.Size = New System.Drawing.Size(111, 31)
        Me.ocmexit.TabIndex = 96
        Me.ocmexit.TabStop = False
        Me.ocmexit.Tag = "2|"
        Me.ocmexit.Text = "EXIT"
        '
        'ogbsearch
        '
        Me.ogbsearch.Controls.Add(Me.otbmainsearch)
        Me.ogbsearch.Dock = System.Windows.Forms.DockStyle.Top
        Me.ogbsearch.Location = New System.Drawing.Point(0, 0)
        Me.ogbsearch.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbsearch.Name = "ogbsearch"
        Me.ogbsearch.Size = New System.Drawing.Size(1552, 151)
        Me.ogbsearch.TabIndex = 0
        Me.ogbsearch.Text = "Search Detail"
        '
        'otpsearchbybarcode
        '
        Me.otpsearchbybarcode.Controls.Add(Me.FTBarcodeNo_lbl)
        Me.otpsearchbybarcode.Controls.Add(Me.FTBarcodeNo)
        Me.otpsearchbybarcode.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.otpsearchbybarcode.Name = "otpsearchbybarcode"
        Me.otpsearchbybarcode.Size = New System.Drawing.Size(1541, 90)
        Me.otpsearchbybarcode.Text = "Search By Barcode"
        '
        'FTBarcodeNo
        '
        Me.FTBarcodeNo.Location = New System.Drawing.Point(195, 33)
        Me.FTBarcodeNo.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTBarcodeNo.Name = "FTBarcodeNo"
        Me.FTBarcodeNo.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FTBarcodeNo.Properties.Appearance.Options.UseBackColor = True
        Me.FTBarcodeNo.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FTBarcodeNo.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FTBarcodeNo.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FTBarcodeNo.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FTBarcodeNo.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FTBarcodeNo.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FTBarcodeNo.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FTBarcodeNo.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FTBarcodeNo.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FTBarcodeNo.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FTBarcodeNo.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FTBarcodeNo.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FTBarcodeNo.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.FTBarcodeNo.Properties.MaxLength = 30
        Me.FTBarcodeNo.Size = New System.Drawing.Size(201, 22)
        Me.FTBarcodeNo.TabIndex = 288
        Me.FTBarcodeNo.Tag = "2|"
        '
        'FTBarcodeNo_lbl
        '
        Me.FTBarcodeNo_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTBarcodeNo_lbl.Appearance.Options.UseForeColor = True
        Me.FTBarcodeNo_lbl.Appearance.Options.UseTextOptions = True
        Me.FTBarcodeNo_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTBarcodeNo_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTBarcodeNo_lbl.Location = New System.Drawing.Point(45, 33)
        Me.FTBarcodeNo_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTBarcodeNo_lbl.Name = "FTBarcodeNo_lbl"
        Me.FTBarcodeNo_lbl.Size = New System.Drawing.Size(146, 23)
        Me.FTBarcodeNo_lbl.TabIndex = 289
        Me.FTBarcodeNo_lbl.Tag = "2|"
        Me.FTBarcodeNo_lbl.Text = "Barcode No :"
        '
        'otpsearch
        '
        Me.otpsearch.Controls.Add(Me.FNHSysWHAssetId)
        Me.otpsearch.Controls.Add(Me.FNHSysWHId_lbl)
        Me.otpsearch.Controls.Add(Me.FNHSysWHAssetId_None)
        Me.otpsearch.Controls.Add(Me.FTOrderNo_lbl)
        Me.otpsearch.Controls.Add(Me.FTDocumentNo)
        Me.otpsearch.Name = "otpsearch"
        Me.otpsearch.Size = New System.Drawing.Size(1541, 90)
        Me.otpsearch.Text = "otpsearch"
        '
        'FTDocumentNo
        '
        Me.FTDocumentNo.EnterMoveNextControl = True
        Me.FTDocumentNo.Location = New System.Drawing.Point(192, 34)
        Me.FTDocumentNo.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTDocumentNo.Name = "FTDocumentNo"
        Me.FTDocumentNo.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FTDocumentNo.Properties.Appearance.Options.UseBackColor = True
        Me.FTDocumentNo.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FTDocumentNo.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FTDocumentNo.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FTDocumentNo.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FTDocumentNo.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FTDocumentNo.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FTDocumentNo.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FTDocumentNo.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FTDocumentNo.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FTDocumentNo.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FTDocumentNo.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FTDocumentNo.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FTDocumentNo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", 15, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject3, "", "569", Nothing, True)})
        Me.FTDocumentNo.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.FTDocumentNo.Properties.MaxLength = 30
        Me.FTDocumentNo.Size = New System.Drawing.Size(167, 22)
        Me.FTDocumentNo.TabIndex = 287
        Me.FTDocumentNo.Tag = "2|"
        '
        'FTOrderNo_lbl
        '
        Me.FTOrderNo_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTOrderNo_lbl.Appearance.Options.UseForeColor = True
        Me.FTOrderNo_lbl.Appearance.Options.UseTextOptions = True
        Me.FTOrderNo_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTOrderNo_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTOrderNo_lbl.Location = New System.Drawing.Point(22, 34)
        Me.FTOrderNo_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTOrderNo_lbl.Name = "FTOrderNo_lbl"
        Me.FTOrderNo_lbl.Size = New System.Drawing.Size(163, 23)
        Me.FTOrderNo_lbl.TabIndex = 288
        Me.FTOrderNo_lbl.Tag = "2|"
        Me.FTOrderNo_lbl.Text = "Document No :"
        '
        'FNHSysWHAssetId_None
        '
        Me.FNHSysWHAssetId_None.EnterMoveNextControl = True
        Me.FNHSysWHAssetId_None.Location = New System.Drawing.Point(661, 34)
        Me.FNHSysWHAssetId_None.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysWHAssetId_None.Name = "FNHSysWHAssetId_None"
        Me.FNHSysWHAssetId_None.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FNHSysWHAssetId_None.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FNHSysWHAssetId_None.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysWHAssetId_None.Properties.Appearance.Options.UseForeColor = True
        Me.FNHSysWHAssetId_None.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysWHAssetId_None.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysWHAssetId_None.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FNHSysWHAssetId_None.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FNHSysWHAssetId_None.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNHSysWHAssetId_None.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysWHAssetId_None.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNHSysWHAssetId_None.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNHSysWHAssetId_None.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysWHAssetId_None.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysWHAssetId_None.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNHSysWHAssetId_None.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNHSysWHAssetId_None.Properties.ReadOnly = True
        Me.FNHSysWHAssetId_None.Size = New System.Drawing.Size(152, 22)
        Me.FNHSysWHAssetId_None.TabIndex = 291
        Me.FNHSysWHAssetId_None.TabStop = False
        Me.FNHSysWHAssetId_None.Tag = "2|"
        '
        'FNHSysWHId_lbl
        '
        Me.FNHSysWHId_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FNHSysWHId_lbl.Appearance.Options.UseForeColor = True
        Me.FNHSysWHId_lbl.Appearance.Options.UseTextOptions = True
        Me.FNHSysWHId_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysWHId_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysWHId_lbl.Location = New System.Drawing.Point(404, 34)
        Me.FNHSysWHId_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysWHId_lbl.Name = "FNHSysWHId_lbl"
        Me.FNHSysWHId_lbl.Size = New System.Drawing.Size(134, 23)
        Me.FNHSysWHId_lbl.TabIndex = 290
        Me.FNHSysWHId_lbl.Tag = "2|"
        Me.FNHSysWHId_lbl.Text = "Warehouse No :"
        '
        'FNHSysWHAssetId
        '
        Me.FNHSysWHAssetId.EnterMoveNextControl = True
        Me.FNHSysWHAssetId.Location = New System.Drawing.Point(545, 34)
        Me.FNHSysWHAssetId.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysWHAssetId.Name = "FNHSysWHAssetId"
        Me.FNHSysWHAssetId.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FNHSysWHAssetId.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysWHAssetId.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysWHAssetId.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysWHAssetId.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FNHSysWHAssetId.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FNHSysWHAssetId.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNHSysWHAssetId.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysWHAssetId.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNHSysWHAssetId.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNHSysWHAssetId.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysWHAssetId.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysWHAssetId.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNHSysWHAssetId.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNHSysWHAssetId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", 15, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject2, "", "425", Nothing, True)})
        Me.FNHSysWHAssetId.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.FNHSysWHAssetId.Properties.MaxLength = 30
        Me.FNHSysWHAssetId.Size = New System.Drawing.Size(112, 22)
        Me.FNHSysWHAssetId.TabIndex = 289
        Me.FNHSysWHAssetId.Tag = "2|"
        '
        'otbmainsearch
        '
        Me.otbmainsearch.AppearancePage.Header.Options.UseTextOptions = True
        Me.otbmainsearch.AppearancePage.Header.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.otbmainsearch.Dock = System.Windows.Forms.DockStyle.Fill
        Me.otbmainsearch.Location = New System.Drawing.Point(2, 25)
        Me.otbmainsearch.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.otbmainsearch.Name = "otbmainsearch"
        Me.otbmainsearch.SelectedTabPage = Me.otpsearchbybarcode
        Me.otbmainsearch.Size = New System.Drawing.Size(1548, 124)
        Me.otbmainsearch.TabIndex = 0
        Me.otbmainsearch.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.otpsearch, Me.otpsearchbybarcode})
        Me.otbmainsearch.TabPageWidth = 150
        '
        'wSearchBarcodeAsset
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1552, 705)
        Me.Controls.Add(Me.ogbmainprocbutton)
        Me.Controls.Add(Me.ogbdetail)
        Me.Controls.Add(Me.ogbsearch)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "wSearchBarcodeAsset"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Search Barcode"
        CType(Me.ogbdetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbdetail.ResumeLayout(False)
        CType(Me.ogcdetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvdetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ReposFTSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbmainprocbutton.ResumeLayout(False)
        CType(Me.ogbsearch, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbsearch.ResumeLayout(False)
        Me.otpsearchbybarcode.ResumeLayout(False)
        CType(Me.FTBarcodeNo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.otpsearch.ResumeLayout(False)
        CType(Me.FTDocumentNo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysWHAssetId_None.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysWHAssetId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.otbmainsearch, System.ComponentModel.ISupportInitialize).EndInit()
        Me.otbmainsearch.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogbdetail As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ogcdetail As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvdetail As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents ocmfind As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ogbmainprocbutton As DevExpress.XtraEditors.PanelControl
    Friend WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmclear As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmpreview As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents CFTSelect As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ReposFTSelect As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents FTAssetCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTAssetName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNHSysFixedAssetId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTBarcodeNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTDocumentNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTWHAssetCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTWHAssetName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTUnitAssetCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNQuantityIN As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNQuantityOUT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ogbsearch As DevExpress.XtraEditors.GroupControl
    Friend WithEvents otbmainsearch As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents otpsearchbybarcode As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents FTBarcodeNo_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTBarcodeNo As DevExpress.XtraEditors.TextEdit
    Friend WithEvents otpsearch As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents FNHSysWHAssetId As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FNHSysWHId_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysWHAssetId_None As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FTOrderNo_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTDocumentNo As DevExpress.XtraEditors.ButtonEdit
End Class

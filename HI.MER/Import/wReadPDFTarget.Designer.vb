<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wReadPDFTarget
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
        Me.ogbselectfile = New DevExpress.XtraEditors.GroupControl()
        Me.FNReadPDFType = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.FNReadPDFType_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTFilePath = New DevExpress.XtraEditors.ButtonEdit()
        Me.ogcdetail = New DevExpress.XtraGrid.GridControl()
        Me.ogvdetail = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.ITEM = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.QUANTITY = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.UNITPRICE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.BUYERITEMNO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.BUYERNUMBER = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.VENDERSTYLE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GTINUCC = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.PRODUCT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.VENDERCOLOR = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.VENDERSIZE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.UNITPRICE2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.PACK = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INNERPACK = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.COMMODITYCODE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.COUNTRY = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ReposFTSelect = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.otb = New DevExpress.XtraTab.XtraTabControl()
        Me.otpnormal = New DevExpress.XtraTab.XtraTabPage()
        Me.otpset = New DevExpress.XtraTab.XtraTabPage()
        Me.ogcdetailset = New DevExpress.XtraGrid.GridControl()
        Me.ogvdetailset = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.CITEM = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CQUANTITY = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CUNITPRICE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CBUYERNUMBER = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CGTINUCC = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CITEMASSIGNED = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFNINCLUDED = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CUNITPRICE2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CSUBBUYERNUMBER = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CVENDERSTYLE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CGTINUCC2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CPRODUCT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CVENDERCOLOR = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CVENDERSIZE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CCOMMODITYCODE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CCOUNTRY = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CPACK = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTStatePriceDiff = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCheckEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.ogbmainprocbutton = New DevExpress.XtraEditors.PanelControl()
        Me.ocmsavelayout = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmrefresh = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmexporttoexcel = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmloaddata = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmclear = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.ogbselectfile, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbselectfile.SuspendLayout()
        CType(Me.FNReadPDFType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTFilePath.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogcdetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvdetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ReposFTSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.otb, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.otb.SuspendLayout()
        Me.otpnormal.SuspendLayout()
        Me.otpset.SuspendLayout()
        CType(Me.ogcdetailset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvdetailset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbmainprocbutton.SuspendLayout()
        Me.SuspendLayout()
        '
        'ogbselectfile
        '
        Me.ogbselectfile.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbselectfile.Controls.Add(Me.FNReadPDFType)
        Me.ogbselectfile.Controls.Add(Me.FNReadPDFType_lbl)
        Me.ogbselectfile.Controls.Add(Me.FTFilePath)
        Me.ogbselectfile.Location = New System.Drawing.Point(3, 4)
        Me.ogbselectfile.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbselectfile.Name = "ogbselectfile"
        Me.ogbselectfile.Size = New System.Drawing.Size(1034, 90)
        Me.ogbselectfile.TabIndex = 2
        Me.ogbselectfile.Text = "Select File"
        '
        'FNReadPDFType
        '
        Me.FNReadPDFType.EditValue = ""
        Me.FNReadPDFType.EnterMoveNextControl = True
        Me.FNReadPDFType.Location = New System.Drawing.Point(187, 33)
        Me.FNReadPDFType.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNReadPDFType.Name = "FNReadPDFType"
        Me.FNReadPDFType.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FNReadPDFType.Properties.Appearance.Options.UseBackColor = True
        Me.FNReadPDFType.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FNReadPDFType.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FNReadPDFType.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FNReadPDFType.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FNReadPDFType.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNReadPDFType.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNReadPDFType.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNReadPDFType.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNReadPDFType.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNReadPDFType.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNReadPDFType.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNReadPDFType.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNReadPDFType.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FNReadPDFType.Properties.Tag = "FNReadPDFType"
        Me.FNReadPDFType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.FNReadPDFType.Size = New System.Drawing.Size(253, 22)
        Me.FNReadPDFType.TabIndex = 293
        Me.FNReadPDFType.TabStop = False
        Me.FNReadPDFType.Tag = "2|"
        '
        'FNReadPDFType_lbl
        '
        Me.FNReadPDFType_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNReadPDFType_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNReadPDFType_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNReadPDFType_lbl.Location = New System.Drawing.Point(43, 33)
        Me.FNReadPDFType_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNReadPDFType_lbl.Name = "FNReadPDFType_lbl"
        Me.FNReadPDFType_lbl.Size = New System.Drawing.Size(138, 23)
        Me.FNReadPDFType_lbl.TabIndex = 292
        Me.FNReadPDFType_lbl.Tag = "2|"
        Me.FNReadPDFType_lbl.Text = "PDF Type :"
        '
        'FTFilePath
        '
        Me.FTFilePath.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FTFilePath.Location = New System.Drawing.Point(43, 60)
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
        Me.FTFilePath.Size = New System.Drawing.Size(975, 22)
        Me.FTFilePath.TabIndex = 1
        Me.FTFilePath.Tag = "2|"
        '
        'ogcdetail
        '
        Me.ogcdetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogcdetail.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcdetail.Location = New System.Drawing.Point(0, 0)
        Me.ogcdetail.MainView = Me.ogvdetail
        Me.ogcdetail.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcdetail.Name = "ogcdetail"
        Me.ogcdetail.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.ReposFTSelect})
        Me.ogcdetail.Size = New System.Drawing.Size(1028, 568)
        Me.ogcdetail.TabIndex = 3
        Me.ogcdetail.TabStop = False
        Me.ogcdetail.Tag = "2|"
        Me.ogcdetail.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvdetail})
        '
        'ogvdetail
        '
        Me.ogvdetail.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.ITEM, Me.QUANTITY, Me.UNITPRICE, Me.BUYERITEMNO, Me.BUYERNUMBER, Me.VENDERSTYLE, Me.GTINUCC, Me.PRODUCT, Me.VENDERCOLOR, Me.VENDERSIZE, Me.UNITPRICE2, Me.PACK, Me.INNERPACK, Me.COMMODITYCODE, Me.COUNTRY})
        Me.ogvdetail.GridControl = Me.ogcdetail
        Me.ogvdetail.Name = "ogvdetail"
        Me.ogvdetail.OptionsCustomization.AllowGroup = False
        Me.ogvdetail.OptionsCustomization.AllowQuickHideColumns = False
        Me.ogvdetail.OptionsView.ColumnAutoWidth = False
        Me.ogvdetail.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.ogvdetail.OptionsView.ShowGroupPanel = False
        Me.ogvdetail.Tag = "2|"
        '
        'ITEM
        '
        Me.ITEM.Caption = "ITEM"
        Me.ITEM.FieldName = "ITEM"
        Me.ITEM.Name = "ITEM"
        Me.ITEM.OptionsColumn.AllowEdit = False
        Me.ITEM.OptionsColumn.ReadOnly = True
        Me.ITEM.Visible = True
        Me.ITEM.VisibleIndex = 0
        Me.ITEM.Width = 100
        '
        'QUANTITY
        '
        Me.QUANTITY.AppearanceCell.Options.UseTextOptions = True
        Me.QUANTITY.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.QUANTITY.Caption = "QUANTITY"
        Me.QUANTITY.DisplayFormat.FormatString = "{0:n0}"
        Me.QUANTITY.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.QUANTITY.FieldName = "QUANTITY"
        Me.QUANTITY.Name = "QUANTITY"
        Me.QUANTITY.OptionsColumn.AllowEdit = False
        Me.QUANTITY.OptionsColumn.ReadOnly = True
        Me.QUANTITY.Visible = True
        Me.QUANTITY.VisibleIndex = 1
        Me.QUANTITY.Width = 80
        '
        'UNITPRICE
        '
        Me.UNITPRICE.AppearanceCell.Options.UseTextOptions = True
        Me.UNITPRICE.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.UNITPRICE.Caption = "UNIT PRICE"
        Me.UNITPRICE.DisplayFormat.FormatString = "{0:n3}"
        Me.UNITPRICE.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.UNITPRICE.FieldName = "UNITPRICE"
        Me.UNITPRICE.Name = "UNITPRICE"
        Me.UNITPRICE.OptionsColumn.AllowEdit = False
        Me.UNITPRICE.OptionsColumn.ReadOnly = True
        Me.UNITPRICE.Visible = True
        Me.UNITPRICE.VisibleIndex = 2
        Me.UNITPRICE.Width = 80
        '
        'BUYERITEMNO
        '
        Me.BUYERITEMNO.Caption = "Buyer's Item Number"
        Me.BUYERITEMNO.FieldName = "BUYERITEMNO"
        Me.BUYERITEMNO.Name = "BUYERITEMNO"
        Me.BUYERITEMNO.OptionsColumn.AllowEdit = False
        Me.BUYERITEMNO.OptionsColumn.ReadOnly = True
        Me.BUYERITEMNO.Visible = True
        Me.BUYERITEMNO.VisibleIndex = 3
        Me.BUYERITEMNO.Width = 100
        '
        'BUYERNUMBER
        '
        Me.BUYERNUMBER.Caption = "Buyer's Catalog Number"
        Me.BUYERNUMBER.FieldName = "BUYERNUMBER"
        Me.BUYERNUMBER.Name = "BUYERNUMBER"
        Me.BUYERNUMBER.OptionsColumn.AllowEdit = False
        Me.BUYERNUMBER.OptionsColumn.ReadOnly = True
        Me.BUYERNUMBER.Visible = True
        Me.BUYERNUMBER.VisibleIndex = 4
        Me.BUYERNUMBER.Width = 100
        '
        'VENDERSTYLE
        '
        Me.VENDERSTYLE.Caption = "Vendor's Style Number"
        Me.VENDERSTYLE.FieldName = "VENDERSTYLE"
        Me.VENDERSTYLE.Name = "VENDERSTYLE"
        Me.VENDERSTYLE.OptionsColumn.AllowEdit = False
        Me.VENDERSTYLE.OptionsColumn.ReadOnly = True
        Me.VENDERSTYLE.Visible = True
        Me.VENDERSTYLE.VisibleIndex = 5
        Me.VENDERSTYLE.Width = 120
        '
        'GTINUCC
        '
        Me.GTINUCC.Caption = "GTIN UCC"
        Me.GTINUCC.FieldName = "GTINUCC"
        Me.GTINUCC.Name = "GTINUCC"
        Me.GTINUCC.OptionsColumn.AllowEdit = False
        Me.GTINUCC.OptionsColumn.ReadOnly = True
        Me.GTINUCC.Visible = True
        Me.GTINUCC.VisibleIndex = 6
        Me.GTINUCC.Width = 100
        '
        'PRODUCT
        '
        Me.PRODUCT.Caption = "PRODUCT"
        Me.PRODUCT.FieldName = "PRODUCT"
        Me.PRODUCT.Name = "PRODUCT"
        Me.PRODUCT.OptionsColumn.AllowEdit = False
        Me.PRODUCT.OptionsColumn.ReadOnly = True
        Me.PRODUCT.Visible = True
        Me.PRODUCT.VisibleIndex = 7
        Me.PRODUCT.Width = 120
        '
        'VENDERCOLOR
        '
        Me.VENDERCOLOR.Caption = "Vendor color"
        Me.VENDERCOLOR.FieldName = "VENDERCOLOR"
        Me.VENDERCOLOR.Name = "VENDERCOLOR"
        Me.VENDERCOLOR.OptionsColumn.AllowEdit = False
        Me.VENDERCOLOR.OptionsColumn.ReadOnly = True
        Me.VENDERCOLOR.Visible = True
        Me.VENDERCOLOR.VisibleIndex = 8
        Me.VENDERCOLOR.Width = 100
        '
        'VENDERSIZE
        '
        Me.VENDERSIZE.Caption = "Vendor size"
        Me.VENDERSIZE.FieldName = "VENDERSIZE"
        Me.VENDERSIZE.Name = "VENDERSIZE"
        Me.VENDERSIZE.OptionsColumn.AllowEdit = False
        Me.VENDERSIZE.OptionsColumn.ReadOnly = True
        Me.VENDERSIZE.Visible = True
        Me.VENDERSIZE.VisibleIndex = 9
        Me.VENDERSIZE.Width = 80
        '
        'UNITPRICE2
        '
        Me.UNITPRICE2.AppearanceCell.Options.UseTextOptions = True
        Me.UNITPRICE2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.UNITPRICE2.Caption = "UNIT PRICE STORE"
        Me.UNITPRICE2.DisplayFormat.FormatString = "{0:n3}"
        Me.UNITPRICE2.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.UNITPRICE2.FieldName = "UNITPRICE2"
        Me.UNITPRICE2.Name = "UNITPRICE2"
        Me.UNITPRICE2.OptionsColumn.AllowEdit = False
        Me.UNITPRICE2.OptionsColumn.ReadOnly = True
        Me.UNITPRICE2.Visible = True
        Me.UNITPRICE2.VisibleIndex = 10
        Me.UNITPRICE2.Width = 80
        '
        'PACK
        '
        Me.PACK.AppearanceCell.Options.UseTextOptions = True
        Me.PACK.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.PACK.Caption = "PACK"
        Me.PACK.DisplayFormat.FormatString = "{0:n0}"
        Me.PACK.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.PACK.FieldName = "PACK"
        Me.PACK.Name = "PACK"
        Me.PACK.OptionsColumn.AllowEdit = False
        Me.PACK.OptionsColumn.ReadOnly = True
        Me.PACK.Visible = True
        Me.PACK.VisibleIndex = 11
        Me.PACK.Width = 80
        '
        'INNERPACK
        '
        Me.INNERPACK.AppearanceCell.Options.UseTextOptions = True
        Me.INNERPACK.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INNERPACK.Caption = "INNER PACK"
        Me.INNERPACK.DisplayFormat.FormatString = "{0:n0}"
        Me.INNERPACK.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INNERPACK.FieldName = "INNERPACK"
        Me.INNERPACK.Name = "INNERPACK"
        Me.INNERPACK.OptionsColumn.AllowEdit = False
        Me.INNERPACK.OptionsColumn.ReadOnly = True
        Me.INNERPACK.Visible = True
        Me.INNERPACK.VisibleIndex = 12
        Me.INNERPACK.Width = 80
        '
        'COMMODITYCODE
        '
        Me.COMMODITYCODE.Caption = "COMMODITY CODE "
        Me.COMMODITYCODE.FieldName = "COMMODITYCODE"
        Me.COMMODITYCODE.Name = "COMMODITYCODE"
        Me.COMMODITYCODE.OptionsColumn.AllowEdit = False
        Me.COMMODITYCODE.OptionsColumn.ReadOnly = True
        Me.COMMODITYCODE.Visible = True
        Me.COMMODITYCODE.VisibleIndex = 13
        Me.COMMODITYCODE.Width = 120
        '
        'COUNTRY
        '
        Me.COUNTRY.Caption = "Country of Origin"
        Me.COUNTRY.FieldName = "COUNTRY"
        Me.COUNTRY.Name = "COUNTRY"
        Me.COUNTRY.OptionsColumn.AllowEdit = False
        Me.COUNTRY.OptionsColumn.ReadOnly = True
        Me.COUNTRY.Visible = True
        Me.COUNTRY.VisibleIndex = 14
        Me.COUNTRY.Width = 120
        '
        'ReposFTSelect
        '
        Me.ReposFTSelect.AutoHeight = False
        Me.ReposFTSelect.Caption = "Check"
        Me.ReposFTSelect.Name = "ReposFTSelect"
        Me.ReposFTSelect.ValueChecked = "1"
        Me.ReposFTSelect.ValueUnchecked = "0"
        '
        'otb
        '
        Me.otb.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.otb.Location = New System.Drawing.Point(3, 101)
        Me.otb.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.otb.Name = "otb"
        Me.otb.SelectedTabPage = Me.otpnormal
        Me.otb.Size = New System.Drawing.Size(1034, 599)
        Me.otb.TabIndex = 392
        Me.otb.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.otpnormal, Me.otpset})
        '
        'otpnormal
        '
        Me.otpnormal.Controls.Add(Me.ogcdetail)
        Me.otpnormal.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.otpnormal.Name = "otpnormal"
        Me.otpnormal.Size = New System.Drawing.Size(1028, 568)
        Me.otpnormal.Text = "PO Normal"
        '
        'otpset
        '
        Me.otpset.Controls.Add(Me.ogcdetailset)
        Me.otpset.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.otpset.Name = "otpset"
        Me.otpset.Size = New System.Drawing.Size(1028, 568)
        Me.otpset.Text = "PO Set"
        '
        'ogcdetailset
        '
        Me.ogcdetailset.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogcdetailset.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcdetailset.Location = New System.Drawing.Point(0, 0)
        Me.ogcdetailset.MainView = Me.ogvdetailset
        Me.ogcdetailset.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcdetailset.Name = "ogcdetailset"
        Me.ogcdetailset.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCheckEdit1})
        Me.ogcdetailset.Size = New System.Drawing.Size(1028, 568)
        Me.ogcdetailset.TabIndex = 4
        Me.ogcdetailset.TabStop = False
        Me.ogcdetailset.Tag = "2|"
        Me.ogcdetailset.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvdetailset})
        '
        'ogvdetailset
        '
        Me.ogvdetailset.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.CITEM, Me.CQUANTITY, Me.CUNITPRICE, Me.CBUYERNUMBER, Me.CGTINUCC, Me.CITEMASSIGNED, Me.CFNINCLUDED, Me.CUNITPRICE2, Me.CSUBBUYERNUMBER, Me.CVENDERSTYLE, Me.CGTINUCC2, Me.CPRODUCT, Me.CVENDERCOLOR, Me.CVENDERSIZE, Me.CCOMMODITYCODE, Me.CCOUNTRY, Me.CPACK, Me.FTStatePriceDiff})
        Me.ogvdetailset.GridControl = Me.ogcdetailset
        Me.ogvdetailset.Name = "ogvdetailset"
        Me.ogvdetailset.OptionsCustomization.AllowGroup = False
        Me.ogvdetailset.OptionsCustomization.AllowQuickHideColumns = False
        Me.ogvdetailset.OptionsView.AllowCellMerge = True
        Me.ogvdetailset.OptionsView.ColumnAutoWidth = False
        Me.ogvdetailset.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.ogvdetailset.OptionsView.ShowGroupPanel = False
        Me.ogvdetailset.Tag = "2|"
        '
        'CITEM
        '
        Me.CITEM.AppearanceHeader.Options.UseTextOptions = True
        Me.CITEM.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.CITEM.Caption = "ITEM"
        Me.CITEM.FieldName = "ITEM"
        Me.CITEM.Name = "CITEM"
        Me.CITEM.OptionsColumn.AllowEdit = False
        Me.CITEM.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.CITEM.OptionsColumn.ReadOnly = True
        Me.CITEM.Visible = True
        Me.CITEM.VisibleIndex = 0
        Me.CITEM.Width = 100
        '
        'CQUANTITY
        '
        Me.CQUANTITY.AppearanceHeader.Options.UseTextOptions = True
        Me.CQUANTITY.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.CQUANTITY.Caption = "QUANTITY"
        Me.CQUANTITY.DisplayFormat.FormatString = "{0:n0}"
        Me.CQUANTITY.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CQUANTITY.FieldName = "QUANTITY"
        Me.CQUANTITY.Name = "CQUANTITY"
        Me.CQUANTITY.OptionsColumn.AllowEdit = False
        Me.CQUANTITY.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.CQUANTITY.OptionsColumn.ReadOnly = True
        Me.CQUANTITY.Visible = True
        Me.CQUANTITY.VisibleIndex = 1
        Me.CQUANTITY.Width = 100
        '
        'CUNITPRICE
        '
        Me.CUNITPRICE.AppearanceHeader.Options.UseTextOptions = True
        Me.CUNITPRICE.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.CUNITPRICE.Caption = "UNIT PRICE"
        Me.CUNITPRICE.DisplayFormat.FormatString = "{0:n3}"
        Me.CUNITPRICE.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CUNITPRICE.FieldName = "UNITPRICE"
        Me.CUNITPRICE.Name = "CUNITPRICE"
        Me.CUNITPRICE.OptionsColumn.AllowEdit = False
        Me.CUNITPRICE.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.CUNITPRICE.OptionsColumn.ReadOnly = True
        Me.CUNITPRICE.Visible = True
        Me.CUNITPRICE.VisibleIndex = 2
        Me.CUNITPRICE.Width = 100
        '
        'CBUYERNUMBER
        '
        Me.CBUYERNUMBER.AppearanceHeader.Options.UseTextOptions = True
        Me.CBUYERNUMBER.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.CBUYERNUMBER.Caption = "Buyer's Catalog Number"
        Me.CBUYERNUMBER.FieldName = "BUYERNUMBER"
        Me.CBUYERNUMBER.Name = "CBUYERNUMBER"
        Me.CBUYERNUMBER.OptionsColumn.AllowEdit = False
        Me.CBUYERNUMBER.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.CBUYERNUMBER.OptionsColumn.ReadOnly = True
        Me.CBUYERNUMBER.Visible = True
        Me.CBUYERNUMBER.VisibleIndex = 3
        Me.CBUYERNUMBER.Width = 120
        '
        'CGTINUCC
        '
        Me.CGTINUCC.AppearanceHeader.Options.UseTextOptions = True
        Me.CGTINUCC.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.CGTINUCC.Caption = "GTIN UCC"
        Me.CGTINUCC.FieldName = "GTINUCC"
        Me.CGTINUCC.Name = "CGTINUCC"
        Me.CGTINUCC.OptionsColumn.AllowEdit = False
        Me.CGTINUCC.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.CGTINUCC.OptionsColumn.ReadOnly = True
        Me.CGTINUCC.Visible = True
        Me.CGTINUCC.VisibleIndex = 4
        Me.CGTINUCC.Width = 100
        '
        'CITEMASSIGNED
        '
        Me.CITEMASSIGNED.AppearanceHeader.Options.UseTextOptions = True
        Me.CITEMASSIGNED.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.CITEMASSIGNED.Caption = "ASSIGNED IDENTIFICATION"
        Me.CITEMASSIGNED.FieldName = "ITEMASSIGNED"
        Me.CITEMASSIGNED.Name = "CITEMASSIGNED"
        Me.CITEMASSIGNED.OptionsColumn.AllowEdit = False
        Me.CITEMASSIGNED.OptionsColumn.ReadOnly = True
        Me.CITEMASSIGNED.Visible = True
        Me.CITEMASSIGNED.VisibleIndex = 5
        Me.CITEMASSIGNED.Width = 120
        '
        'CFNINCLUDED
        '
        Me.CFNINCLUDED.AppearanceCell.Options.UseTextOptions = True
        Me.CFNINCLUDED.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.CFNINCLUDED.AppearanceHeader.Options.UseTextOptions = True
        Me.CFNINCLUDED.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.CFNINCLUDED.Caption = "Included"
        Me.CFNINCLUDED.DisplayFormat.FormatString = "{0:n0}"
        Me.CFNINCLUDED.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CFNINCLUDED.FieldName = "FNINCLUDED"
        Me.CFNINCLUDED.Name = "CFNINCLUDED"
        Me.CFNINCLUDED.OptionsColumn.AllowEdit = False
        Me.CFNINCLUDED.OptionsColumn.ReadOnly = True
        Me.CFNINCLUDED.Visible = True
        Me.CFNINCLUDED.VisibleIndex = 6
        Me.CFNINCLUDED.Width = 100
        '
        'CUNITPRICE2
        '
        Me.CUNITPRICE2.AppearanceCell.Options.UseTextOptions = True
        Me.CUNITPRICE2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.CUNITPRICE2.AppearanceHeader.Options.UseTextOptions = True
        Me.CUNITPRICE2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.CUNITPRICE2.Caption = "UNIT PRICE"
        Me.CUNITPRICE2.DisplayFormat.FormatString = "{0:n3}"
        Me.CUNITPRICE2.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CUNITPRICE2.FieldName = "UNITPRICE2"
        Me.CUNITPRICE2.Name = "CUNITPRICE2"
        Me.CUNITPRICE2.OptionsColumn.AllowEdit = False
        Me.CUNITPRICE2.OptionsColumn.ReadOnly = True
        Me.CUNITPRICE2.Visible = True
        Me.CUNITPRICE2.VisibleIndex = 7
        Me.CUNITPRICE2.Width = 100
        '
        'CSUBBUYERNUMBER
        '
        Me.CSUBBUYERNUMBER.AppearanceHeader.Options.UseTextOptions = True
        Me.CSUBBUYERNUMBER.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.CSUBBUYERNUMBER.Caption = "Buyer's Catalog Number"
        Me.CSUBBUYERNUMBER.FieldName = "SUBBUYERNUMBER"
        Me.CSUBBUYERNUMBER.Name = "CSUBBUYERNUMBER"
        Me.CSUBBUYERNUMBER.OptionsColumn.AllowEdit = False
        Me.CSUBBUYERNUMBER.OptionsColumn.ReadOnly = True
        Me.CSUBBUYERNUMBER.Visible = True
        Me.CSUBBUYERNUMBER.VisibleIndex = 8
        Me.CSUBBUYERNUMBER.Width = 120
        '
        'CVENDERSTYLE
        '
        Me.CVENDERSTYLE.AppearanceHeader.Options.UseTextOptions = True
        Me.CVENDERSTYLE.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.CVENDERSTYLE.Caption = "Vendor's Style Number"
        Me.CVENDERSTYLE.FieldName = "VENDERSTYLE"
        Me.CVENDERSTYLE.Name = "CVENDERSTYLE"
        Me.CVENDERSTYLE.OptionsColumn.AllowEdit = False
        Me.CVENDERSTYLE.OptionsColumn.ReadOnly = True
        Me.CVENDERSTYLE.Visible = True
        Me.CVENDERSTYLE.VisibleIndex = 9
        Me.CVENDERSTYLE.Width = 120
        '
        'CGTINUCC2
        '
        Me.CGTINUCC2.AppearanceHeader.Options.UseTextOptions = True
        Me.CGTINUCC2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.CGTINUCC2.Caption = "GTIN UCC"
        Me.CGTINUCC2.FieldName = "GTINUCC2"
        Me.CGTINUCC2.Name = "CGTINUCC2"
        Me.CGTINUCC2.OptionsColumn.AllowEdit = False
        Me.CGTINUCC2.OptionsColumn.ReadOnly = True
        Me.CGTINUCC2.Visible = True
        Me.CGTINUCC2.VisibleIndex = 10
        Me.CGTINUCC2.Width = 100
        '
        'CPRODUCT
        '
        Me.CPRODUCT.AppearanceHeader.Options.UseTextOptions = True
        Me.CPRODUCT.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.CPRODUCT.Caption = "PRODUCT"
        Me.CPRODUCT.FieldName = "PRODUCT"
        Me.CPRODUCT.Name = "CPRODUCT"
        Me.CPRODUCT.OptionsColumn.AllowEdit = False
        Me.CPRODUCT.OptionsColumn.ReadOnly = True
        Me.CPRODUCT.Visible = True
        Me.CPRODUCT.VisibleIndex = 11
        Me.CPRODUCT.Width = 120
        '
        'CVENDERCOLOR
        '
        Me.CVENDERCOLOR.AppearanceHeader.Options.UseTextOptions = True
        Me.CVENDERCOLOR.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.CVENDERCOLOR.Caption = "Vendor color"
        Me.CVENDERCOLOR.FieldName = "VENDERCOLOR"
        Me.CVENDERCOLOR.Name = "CVENDERCOLOR"
        Me.CVENDERCOLOR.OptionsColumn.AllowEdit = False
        Me.CVENDERCOLOR.OptionsColumn.ReadOnly = True
        Me.CVENDERCOLOR.Width = 100
        '
        'CVENDERSIZE
        '
        Me.CVENDERSIZE.AppearanceHeader.Options.UseTextOptions = True
        Me.CVENDERSIZE.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.CVENDERSIZE.Caption = "Vendor size"
        Me.CVENDERSIZE.FieldName = "VENDERSIZE"
        Me.CVENDERSIZE.Name = "CVENDERSIZE"
        Me.CVENDERSIZE.OptionsColumn.AllowEdit = False
        Me.CVENDERSIZE.OptionsColumn.ReadOnly = True
        Me.CVENDERSIZE.Width = 80
        '
        'CCOMMODITYCODE
        '
        Me.CCOMMODITYCODE.AppearanceHeader.Options.UseTextOptions = True
        Me.CCOMMODITYCODE.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.CCOMMODITYCODE.Caption = "COMMODITY CODE "
        Me.CCOMMODITYCODE.FieldName = "COMMODITYCODE"
        Me.CCOMMODITYCODE.Name = "CCOMMODITYCODE"
        Me.CCOMMODITYCODE.OptionsColumn.AllowEdit = False
        Me.CCOMMODITYCODE.OptionsColumn.ReadOnly = True
        Me.CCOMMODITYCODE.Visible = True
        Me.CCOMMODITYCODE.VisibleIndex = 12
        Me.CCOMMODITYCODE.Width = 120
        '
        'CCOUNTRY
        '
        Me.CCOUNTRY.AppearanceHeader.Options.UseTextOptions = True
        Me.CCOUNTRY.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.CCOUNTRY.Caption = "Country of Origin"
        Me.CCOUNTRY.FieldName = "COUNTRY"
        Me.CCOUNTRY.Name = "CCOUNTRY"
        Me.CCOUNTRY.OptionsColumn.AllowEdit = False
        Me.CCOUNTRY.OptionsColumn.ReadOnly = True
        Me.CCOUNTRY.Visible = True
        Me.CCOUNTRY.VisibleIndex = 13
        Me.CCOUNTRY.Width = 120
        '
        'CPACK
        '
        Me.CPACK.AppearanceCell.Options.UseTextOptions = True
        Me.CPACK.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.CPACK.AppearanceHeader.Options.UseTextOptions = True
        Me.CPACK.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.CPACK.Caption = "DPCI"
        Me.CPACK.DisplayFormat.FormatString = "{0:n0}"
        Me.CPACK.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CPACK.FieldName = "FNPACK"
        Me.CPACK.Name = "CPACK"
        Me.CPACK.OptionsColumn.AllowEdit = False
        Me.CPACK.OptionsColumn.ReadOnly = True
        Me.CPACK.Visible = True
        Me.CPACK.VisibleIndex = 14
        Me.CPACK.Width = 100
        '
        'FTStatePriceDiff
        '
        Me.FTStatePriceDiff.AppearanceCell.Options.UseTextOptions = True
        Me.FTStatePriceDiff.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTStatePriceDiff.AppearanceHeader.Options.UseTextOptions = True
        Me.FTStatePriceDiff.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTStatePriceDiff.Caption = "FTStatePriceDiff"
        Me.FTStatePriceDiff.FieldName = "FTStatePriceDiff"
        Me.FTStatePriceDiff.Name = "FTStatePriceDiff"
        Me.FTStatePriceDiff.OptionsColumn.AllowEdit = False
        Me.FTStatePriceDiff.OptionsColumn.ReadOnly = True
        Me.FTStatePriceDiff.Width = 80
        '
        'RepositoryItemCheckEdit1
        '
        Me.RepositoryItemCheckEdit1.AutoHeight = False
        Me.RepositoryItemCheckEdit1.Caption = "Check"
        Me.RepositoryItemCheckEdit1.Name = "RepositoryItemCheckEdit1"
        Me.RepositoryItemCheckEdit1.ValueChecked = "1"
        Me.RepositoryItemCheckEdit1.ValueUnchecked = "0"
        '
        'ogbmainprocbutton
        '
        Me.ogbmainprocbutton.Controls.Add(Me.ocmsavelayout)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmrefresh)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmexporttoexcel)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmexit)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmloaddata)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmclear)
        Me.ogbmainprocbutton.Location = New System.Drawing.Point(90, 330)
        Me.ogbmainprocbutton.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbmainprocbutton.Name = "ogbmainprocbutton"
        Me.ogbmainprocbutton.Size = New System.Drawing.Size(859, 43)
        Me.ogbmainprocbutton.TabIndex = 393
        '
        'ocmsavelayout
        '
        Me.ocmsavelayout.Location = New System.Drawing.Point(436, 7)
        Me.ocmsavelayout.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmsavelayout.Name = "ocmsavelayout"
        Me.ocmsavelayout.Size = New System.Drawing.Size(136, 28)
        Me.ocmsavelayout.TabIndex = 335
        Me.ocmsavelayout.Text = "savelayoutgrid"
        '
        'ocmrefresh
        '
        Me.ocmrefresh.Location = New System.Drawing.Point(329, 5)
        Me.ocmrefresh.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmrefresh.Name = "ocmrefresh"
        Me.ocmrefresh.Size = New System.Drawing.Size(85, 31)
        Me.ocmrefresh.TabIndex = 334
        Me.ocmrefresh.TabStop = False
        Me.ocmrefresh.Tag = "2|"
        Me.ocmrefresh.Text = "Refresh"
        '
        'ocmexporttoexcel
        '
        Me.ocmexporttoexcel.Location = New System.Drawing.Point(190, 7)
        Me.ocmexporttoexcel.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmexporttoexcel.Name = "ocmexporttoexcel"
        Me.ocmexporttoexcel.Size = New System.Drawing.Size(133, 28)
        Me.ocmexporttoexcel.TabIndex = 333
        Me.ocmexporttoexcel.Text = "Export to Optiplan"
        '
        'ocmexit
        '
        Me.ocmexit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmexit.Location = New System.Drawing.Point(785, 4)
        Me.ocmexit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmexit.Name = "ocmexit"
        Me.ocmexit.Size = New System.Drawing.Size(64, 31)
        Me.ocmexit.TabIndex = 96
        Me.ocmexit.TabStop = False
        Me.ocmexit.Tag = "2|"
        Me.ocmexit.Text = "EXIT"
        '
        'ocmloaddata
        '
        Me.ocmloaddata.Location = New System.Drawing.Point(6, 6)
        Me.ocmloaddata.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmloaddata.Name = "ocmloaddata"
        Me.ocmloaddata.Size = New System.Drawing.Size(92, 31)
        Me.ocmloaddata.TabIndex = 93
        Me.ocmloaddata.TabStop = False
        Me.ocmloaddata.Tag = "2|"
        Me.ocmloaddata.Text = "LoadData"
        '
        'ocmclear
        '
        Me.ocmclear.Location = New System.Drawing.Point(100, 6)
        Me.ocmclear.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmclear.Name = "ocmclear"
        Me.ocmclear.Size = New System.Drawing.Size(84, 31)
        Me.ocmclear.TabIndex = 95
        Me.ocmclear.TabStop = False
        Me.ocmclear.Tag = "2|"
        Me.ocmclear.Text = "CLEAR"
        '
        'wReadPDFTarget
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1038, 703)
        Me.Controls.Add(Me.ogbmainprocbutton)
        Me.Controls.Add(Me.otb)
        Me.Controls.Add(Me.ogbselectfile)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "wReadPDFTarget"
        Me.Text = "wReadPDF"
        CType(Me.ogbselectfile, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbselectfile.ResumeLayout(False)
        CType(Me.FNReadPDFType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTFilePath.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogcdetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvdetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ReposFTSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.otb, System.ComponentModel.ISupportInitialize).EndInit()
        Me.otb.ResumeLayout(False)
        Me.otpnormal.ResumeLayout(False)
        Me.otpset.ResumeLayout(False)
        CType(Me.ogcdetailset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvdetailset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbmainprocbutton.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogbselectfile As DevExpress.XtraEditors.GroupControl
    Friend WithEvents FTFilePath As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents ogcdetail As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvdetail As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents ReposFTSelect As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents ITEM As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents QUANTITY As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents UNITPRICE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents BUYERNUMBER As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents VENDERSTYLE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GTINUCC As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents PRODUCT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents VENDERCOLOR As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents VENDERSIZE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents UNITPRICE2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents PACK As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INNERPACK As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents COMMODITYCODE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents COUNTRY As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents otb As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents otpnormal As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents otpset As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents ogbmainprocbutton As DevExpress.XtraEditors.PanelControl
    Friend WithEvents ocmsavelayout As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmrefresh As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmexporttoexcel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmloaddata As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmclear As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ogcdetailset As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvdetailset As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents CITEMASSIGNED As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFNINCLUDED As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CUNITPRICE2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CSUBBUYERNUMBER As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CVENDERSTYLE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CGTINUCC2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CPRODUCT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CVENDERCOLOR As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CVENDERSIZE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CPACK As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTStatePriceDiff As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CCOMMODITYCODE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CCOUNTRY As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemCheckEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents FNReadPDFType As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents FNReadPDFType_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents CITEM As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CQUANTITY As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CUNITPRICE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CBUYERNUMBER As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CGTINUCC As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents BUYERITEMNO As DevExpress.XtraGrid.Columns.GridColumn
End Class

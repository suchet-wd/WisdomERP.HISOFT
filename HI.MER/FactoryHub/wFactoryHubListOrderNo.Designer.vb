<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wFactoryHubListOrderNo
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
        Me.ogbFactoryOrderCopy = New DevExpress.XtraEditors.GroupControl()
        Me.ogdmain = New DevExpress.XtraGrid.GridControl()
        Me.ogvmain = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.CFIXFTOrderNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFIXFTSelect = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepFTSelect = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.CFIXFactory_Vendor_Code = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFIXVendor_Location_Code_MCO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFIXFTNikePO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFIXPO_Doc_Date = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFIXPO_Number = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFIXPO_Ref = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFIXPO_Group = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFIXCurrency_Type = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFIXShip_Via_Instructions = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFIXBUY_SEASON = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFIXBUY_YEAR = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFIXBUY_GROUP = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFIXPO_Item = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFIXMaterial_Number = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFIXMaterial_Description = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFIXPlant = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFIXNike_Division_Code = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFIXUOM = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFIXMode_Code = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFIXMode_Code_Description = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFIXOGAC_Date = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFIXGAC_Date = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFIXGAC_Reason_Code = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFIXMaterial_Dev_Code = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFIXSilhhouette_Code = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFIXGender_Age_Code = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFIXSO_NUMBER = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFIXSO_ITEM = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFIXColor_Combo_Name = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFIXColor_Combo_ShortName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFIXMSRP_US = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFIXQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFIXUCC_NAME1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFIXShip_To_Account = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.ogbFactoryOrderCopy, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbFactoryOrderCopy.SuspendLayout()
        CType(Me.ogdmain, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvmain, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepFTSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ogbFactoryOrderCopy
        '
        Me.ogbFactoryOrderCopy.Controls.Add(Me.ogdmain)
        Me.ogbFactoryOrderCopy.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogbFactoryOrderCopy.Location = New System.Drawing.Point(0, 0)
        Me.ogbFactoryOrderCopy.Name = "ogbFactoryOrderCopy"
        Me.ogbFactoryOrderCopy.ShowCaption = False
        Me.ogbFactoryOrderCopy.Size = New System.Drawing.Size(1362, 531)
        Me.ogbFactoryOrderCopy.TabIndex = 0
        Me.ogbFactoryOrderCopy.Text = "List Factory Order No. Copy"
        '
        'ogdmain
        '
        Me.ogdmain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogdmain.Location = New System.Drawing.Point(2, 2)
        Me.ogdmain.MainView = Me.ogvmain
        Me.ogdmain.Name = "ogdmain"
        Me.ogdmain.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepFTSelect})
        Me.ogdmain.Size = New System.Drawing.Size(1358, 527)
        Me.ogdmain.TabIndex = 399
        Me.ogdmain.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvmain})
        '
        'ogvmain
        '
        Me.ogvmain.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.CFIXFTOrderNo, Me.CFIXFTSelect, Me.CFIXFactory_Vendor_Code, Me.CFIXVendor_Location_Code_MCO, Me.CFIXFTNikePO, Me.CFIXPO_Doc_Date, Me.CFIXPO_Number, Me.CFIXPO_Ref, Me.CFIXPO_Group, Me.CFIXCurrency_Type, Me.CFIXShip_Via_Instructions, Me.CFIXBUY_SEASON, Me.CFIXBUY_YEAR, Me.CFIXBUY_GROUP, Me.CFIXPO_Item, Me.CFIXMaterial_Number, Me.CFIXMaterial_Description, Me.CFIXPlant, Me.CFIXShip_To_Account, Me.CFIXUCC_NAME1, Me.CFIXNike_Division_Code, Me.CFIXUOM, Me.CFIXMode_Code, Me.CFIXMode_Code_Description, Me.CFIXOGAC_Date, Me.CFIXGAC_Date, Me.CFIXGAC_Reason_Code, Me.CFIXMaterial_Dev_Code, Me.CFIXSilhhouette_Code, Me.CFIXGender_Age_Code, Me.CFIXSO_NUMBER, Me.CFIXSO_ITEM, Me.CFIXColor_Combo_Name, Me.CFIXColor_Combo_ShortName, Me.CFIXMSRP_US, Me.CFIXQuantity})
        Me.ogvmain.GridControl = Me.ogdmain
        Me.ogvmain.Name = "ogvmain"
        Me.ogvmain.OptionsView.ColumnAutoWidth = False
        Me.ogvmain.OptionsView.ShowFooter = True
        Me.ogvmain.OptionsView.ShowGroupPanel = False
        '
        'CFIXFTOrderNo
        '
        Me.CFIXFTOrderNo.Caption = "Order No"
        Me.CFIXFTOrderNo.FieldName = "FTOrderNo"
        Me.CFIXFTOrderNo.Name = "CFIXFTOrderNo"
        Me.CFIXFTOrderNo.OptionsColumn.AllowEdit = False
        Me.CFIXFTOrderNo.OptionsColumn.ReadOnly = True
        Me.CFIXFTOrderNo.Visible = True
        Me.CFIXFTOrderNo.VisibleIndex = 0
        '
        'CFIXFTSelect
        '
        Me.CFIXFTSelect.Caption = "Select"
        Me.CFIXFTSelect.ColumnEdit = Me.RepFTSelect
        Me.CFIXFTSelect.FieldName = "FTSelect"
        Me.CFIXFTSelect.Name = "CFIXFTSelect"
        Me.CFIXFTSelect.OptionsColumn.AllowEdit = False
        Me.CFIXFTSelect.OptionsColumn.ReadOnly = True
        '
        'RepFTSelect
        '
        Me.RepFTSelect.AutoHeight = False
        Me.RepFTSelect.Name = "RepFTSelect"
        Me.RepFTSelect.ValueChecked = "1"
        Me.RepFTSelect.ValueUnchecked = "0"
        '
        'CFIXFactory_Vendor_Code
        '
        Me.CFIXFactory_Vendor_Code.Caption = "Factory"
        Me.CFIXFactory_Vendor_Code.FieldName = "Factory_Vendor_Code"
        Me.CFIXFactory_Vendor_Code.Name = "CFIXFactory_Vendor_Code"
        Me.CFIXFactory_Vendor_Code.OptionsColumn.AllowEdit = False
        Me.CFIXFactory_Vendor_Code.OptionsColumn.ReadOnly = True
        Me.CFIXFactory_Vendor_Code.Visible = True
        Me.CFIXFactory_Vendor_Code.VisibleIndex = 11
        '
        'CFIXVendor_Location_Code_MCO
        '
        Me.CFIXVendor_Location_Code_MCO.Caption = "Vendor Location"
        Me.CFIXVendor_Location_Code_MCO.FieldName = "Vendor_Location_Code_MCO"
        Me.CFIXVendor_Location_Code_MCO.Name = "CFIXVendor_Location_Code_MCO"
        Me.CFIXVendor_Location_Code_MCO.OptionsColumn.AllowEdit = False
        Me.CFIXVendor_Location_Code_MCO.OptionsColumn.ReadOnly = True
        Me.CFIXVendor_Location_Code_MCO.Visible = True
        Me.CFIXVendor_Location_Code_MCO.VisibleIndex = 12
        '
        'CFIXFTNikePO
        '
        Me.CFIXFTNikePO.Caption = "FTNikePO"
        Me.CFIXFTNikePO.FieldName = "FTNikePO"
        Me.CFIXFTNikePO.Name = "CFIXFTNikePO"
        Me.CFIXFTNikePO.OptionsColumn.AllowEdit = False
        Me.CFIXFTNikePO.OptionsColumn.ReadOnly = True
        Me.CFIXFTNikePO.Visible = True
        Me.CFIXFTNikePO.VisibleIndex = 1
        '
        'CFIXPO_Doc_Date
        '
        Me.CFIXPO_Doc_Date.Caption = "PO Doc Date"
        Me.CFIXPO_Doc_Date.FieldName = "PO_Doc_Date"
        Me.CFIXPO_Doc_Date.Name = "CFIXPO_Doc_Date"
        Me.CFIXPO_Doc_Date.OptionsColumn.AllowEdit = False
        Me.CFIXPO_Doc_Date.OptionsColumn.ReadOnly = True
        Me.CFIXPO_Doc_Date.Visible = True
        Me.CFIXPO_Doc_Date.VisibleIndex = 2
        '
        'CFIXPO_Number
        '
        Me.CFIXPO_Number.Caption = "PO Number"
        Me.CFIXPO_Number.FieldName = "PO_Number"
        Me.CFIXPO_Number.Name = "CFIXPO_Number"
        Me.CFIXPO_Number.OptionsColumn.AllowEdit = False
        Me.CFIXPO_Number.OptionsColumn.ReadOnly = True
        Me.CFIXPO_Number.Visible = True
        Me.CFIXPO_Number.VisibleIndex = 3
        '
        'CFIXPO_Ref
        '
        Me.CFIXPO_Ref.Caption = "PO Ref"
        Me.CFIXPO_Ref.FieldName = "PO_Ref"
        Me.CFIXPO_Ref.Name = "CFIXPO_Ref"
        Me.CFIXPO_Ref.OptionsColumn.AllowEdit = False
        Me.CFIXPO_Ref.OptionsColumn.ReadOnly = True
        Me.CFIXPO_Ref.Visible = True
        Me.CFIXPO_Ref.VisibleIndex = 4
        '
        'CFIXPO_Group
        '
        Me.CFIXPO_Group.Caption = "PO Group"
        Me.CFIXPO_Group.FieldName = "PO_Group"
        Me.CFIXPO_Group.Name = "CFIXPO_Group"
        Me.CFIXPO_Group.OptionsColumn.AllowEdit = False
        Me.CFIXPO_Group.OptionsColumn.ReadOnly = True
        Me.CFIXPO_Group.Visible = True
        Me.CFIXPO_Group.VisibleIndex = 5
        '
        'CFIXCurrency_Type
        '
        Me.CFIXCurrency_Type.Caption = "Currency"
        Me.CFIXCurrency_Type.FieldName = "Currency_Type"
        Me.CFIXCurrency_Type.Name = "CFIXCurrency_Type"
        Me.CFIXCurrency_Type.OptionsColumn.AllowEdit = False
        Me.CFIXCurrency_Type.OptionsColumn.ReadOnly = True
        Me.CFIXCurrency_Type.Visible = True
        Me.CFIXCurrency_Type.VisibleIndex = 6
        '
        'CFIXShip_Via_Instructions
        '
        Me.CFIXShip_Via_Instructions.Caption = "Ship Via"
        Me.CFIXShip_Via_Instructions.FieldName = "Ship_Via_Instructions"
        Me.CFIXShip_Via_Instructions.Name = "CFIXShip_Via_Instructions"
        Me.CFIXShip_Via_Instructions.OptionsColumn.AllowEdit = False
        Me.CFIXShip_Via_Instructions.OptionsColumn.ReadOnly = True
        Me.CFIXShip_Via_Instructions.Visible = True
        Me.CFIXShip_Via_Instructions.VisibleIndex = 7
        '
        'CFIXBUY_SEASON
        '
        Me.CFIXBUY_SEASON.Caption = "Season"
        Me.CFIXBUY_SEASON.FieldName = "BUY_SEASON"
        Me.CFIXBUY_SEASON.Name = "CFIXBUY_SEASON"
        Me.CFIXBUY_SEASON.OptionsColumn.AllowEdit = False
        Me.CFIXBUY_SEASON.OptionsColumn.ReadOnly = True
        Me.CFIXBUY_SEASON.Visible = True
        Me.CFIXBUY_SEASON.VisibleIndex = 8
        '
        'CFIXBUY_YEAR
        '
        Me.CFIXBUY_YEAR.Caption = "YEAR"
        Me.CFIXBUY_YEAR.FieldName = "BUY_YEAR"
        Me.CFIXBUY_YEAR.Name = "CFIXBUY_YEAR"
        Me.CFIXBUY_YEAR.OptionsColumn.AllowEdit = False
        Me.CFIXBUY_YEAR.OptionsColumn.ReadOnly = True
        Me.CFIXBUY_YEAR.Visible = True
        Me.CFIXBUY_YEAR.VisibleIndex = 9
        '
        'CFIXBUY_GROUP
        '
        Me.CFIXBUY_GROUP.Caption = "Buy Grp"
        Me.CFIXBUY_GROUP.FieldName = "BUY_GROUP"
        Me.CFIXBUY_GROUP.Name = "CFIXBUY_GROUP"
        Me.CFIXBUY_GROUP.OptionsColumn.AllowEdit = False
        Me.CFIXBUY_GROUP.OptionsColumn.ReadOnly = True
        Me.CFIXBUY_GROUP.Visible = True
        Me.CFIXBUY_GROUP.VisibleIndex = 10
        '
        'CFIXPO_Item
        '
        Me.CFIXPO_Item.Caption = "PO Item"
        Me.CFIXPO_Item.FieldName = "PO_Item"
        Me.CFIXPO_Item.Name = "CFIXPO_Item"
        Me.CFIXPO_Item.OptionsColumn.AllowEdit = False
        Me.CFIXPO_Item.OptionsColumn.ReadOnly = True
        Me.CFIXPO_Item.Visible = True
        Me.CFIXPO_Item.VisibleIndex = 13
        '
        'CFIXMaterial_Number
        '
        Me.CFIXMaterial_Number.Caption = "Material Number"
        Me.CFIXMaterial_Number.FieldName = "Material_Number"
        Me.CFIXMaterial_Number.Name = "CFIXMaterial_Number"
        Me.CFIXMaterial_Number.OptionsColumn.AllowEdit = False
        Me.CFIXMaterial_Number.OptionsColumn.ReadOnly = True
        Me.CFIXMaterial_Number.Visible = True
        Me.CFIXMaterial_Number.VisibleIndex = 14
        '
        'CFIXMaterial_Description
        '
        Me.CFIXMaterial_Description.Caption = "Material Description"
        Me.CFIXMaterial_Description.FieldName = "Material_Description"
        Me.CFIXMaterial_Description.Name = "CFIXMaterial_Description"
        Me.CFIXMaterial_Description.OptionsColumn.AllowEdit = False
        Me.CFIXMaterial_Description.OptionsColumn.ReadOnly = True
        Me.CFIXMaterial_Description.Visible = True
        Me.CFIXMaterial_Description.VisibleIndex = 15
        '
        'CFIXPlant
        '
        Me.CFIXPlant.Caption = "CPlant"
        Me.CFIXPlant.FieldName = "Plant"
        Me.CFIXPlant.Name = "CFIXPlant"
        Me.CFIXPlant.OptionsColumn.AllowEdit = False
        Me.CFIXPlant.OptionsColumn.ReadOnly = True
        Me.CFIXPlant.Visible = True
        Me.CFIXPlant.VisibleIndex = 16
        '
        'CFIXNike_Division_Code
        '
        Me.CFIXNike_Division_Code.Caption = "Nike Division Code"
        Me.CFIXNike_Division_Code.FieldName = "Nike_Division_Code"
        Me.CFIXNike_Division_Code.Name = "CFIXNike_Division_Code"
        Me.CFIXNike_Division_Code.OptionsColumn.AllowEdit = False
        Me.CFIXNike_Division_Code.OptionsColumn.ReadOnly = True
        Me.CFIXNike_Division_Code.Visible = True
        Me.CFIXNike_Division_Code.VisibleIndex = 19
        '
        'CFIXUOM
        '
        Me.CFIXUOM.Caption = "UOM"
        Me.CFIXUOM.FieldName = "UOM"
        Me.CFIXUOM.Name = "CFIXUOM"
        Me.CFIXUOM.OptionsColumn.AllowEdit = False
        Me.CFIXUOM.OptionsColumn.ReadOnly = True
        Me.CFIXUOM.Visible = True
        Me.CFIXUOM.VisibleIndex = 20
        '
        'CFIXMode_Code
        '
        Me.CFIXMode_Code.Caption = "Mode Code"
        Me.CFIXMode_Code.FieldName = "Mode_Code"
        Me.CFIXMode_Code.Name = "CFIXMode_Code"
        Me.CFIXMode_Code.OptionsColumn.AllowEdit = False
        Me.CFIXMode_Code.OptionsColumn.ReadOnly = True
        Me.CFIXMode_Code.Visible = True
        Me.CFIXMode_Code.VisibleIndex = 21
        '
        'CFIXMode_Code_Description
        '
        Me.CFIXMode_Code_Description.Caption = "Mode Description"
        Me.CFIXMode_Code_Description.FieldName = "Mode_Code_Description"
        Me.CFIXMode_Code_Description.Name = "CFIXMode_Code_Description"
        Me.CFIXMode_Code_Description.OptionsColumn.AllowEdit = False
        Me.CFIXMode_Code_Description.OptionsColumn.ReadOnly = True
        Me.CFIXMode_Code_Description.Visible = True
        Me.CFIXMode_Code_Description.VisibleIndex = 22
        '
        'CFIXOGAC_Date
        '
        Me.CFIXOGAC_Date.Caption = "O GAC Date"
        Me.CFIXOGAC_Date.FieldName = "OGAC_Date"
        Me.CFIXOGAC_Date.Name = "CFIXOGAC_Date"
        Me.CFIXOGAC_Date.OptionsColumn.AllowEdit = False
        Me.CFIXOGAC_Date.OptionsColumn.ReadOnly = True
        Me.CFIXOGAC_Date.Visible = True
        Me.CFIXOGAC_Date.VisibleIndex = 23
        '
        'CFIXGAC_Date
        '
        Me.CFIXGAC_Date.Caption = "GAC Date"
        Me.CFIXGAC_Date.FieldName = "GAC_Date"
        Me.CFIXGAC_Date.Name = "CFIXGAC_Date"
        Me.CFIXGAC_Date.OptionsColumn.AllowEdit = False
        Me.CFIXGAC_Date.OptionsColumn.ReadOnly = True
        Me.CFIXGAC_Date.Visible = True
        Me.CFIXGAC_Date.VisibleIndex = 24
        '
        'CFIXGAC_Reason_Code
        '
        Me.CFIXGAC_Reason_Code.Caption = "GAC Reason"
        Me.CFIXGAC_Reason_Code.FieldName = "GAC_Reason_Code"
        Me.CFIXGAC_Reason_Code.Name = "CFIXGAC_Reason_Code"
        Me.CFIXGAC_Reason_Code.OptionsColumn.AllowEdit = False
        Me.CFIXGAC_Reason_Code.OptionsColumn.ReadOnly = True
        Me.CFIXGAC_Reason_Code.Visible = True
        Me.CFIXGAC_Reason_Code.VisibleIndex = 25
        '
        'CFIXMaterial_Dev_Code
        '
        Me.CFIXMaterial_Dev_Code.Caption = "Material Dev"
        Me.CFIXMaterial_Dev_Code.FieldName = "Material_Dev_Code"
        Me.CFIXMaterial_Dev_Code.Name = "CFIXMaterial_Dev_Code"
        Me.CFIXMaterial_Dev_Code.OptionsColumn.AllowEdit = False
        Me.CFIXMaterial_Dev_Code.OptionsColumn.ReadOnly = True
        Me.CFIXMaterial_Dev_Code.Visible = True
        Me.CFIXMaterial_Dev_Code.VisibleIndex = 26
        '
        'CFIXSilhhouette_Code
        '
        Me.CFIXSilhhouette_Code.Caption = "Silhhouette"
        Me.CFIXSilhhouette_Code.FieldName = "Silhhouette_Code"
        Me.CFIXSilhhouette_Code.Name = "CFIXSilhhouette_Code"
        Me.CFIXSilhhouette_Code.OptionsColumn.AllowEdit = False
        Me.CFIXSilhhouette_Code.OptionsColumn.ReadOnly = True
        Me.CFIXSilhhouette_Code.Visible = True
        Me.CFIXSilhhouette_Code.VisibleIndex = 27
        '
        'CFIXGender_Age_Code
        '
        Me.CFIXGender_Age_Code.Caption = "Gender"
        Me.CFIXGender_Age_Code.FieldName = "Gender_Age_Code"
        Me.CFIXGender_Age_Code.Name = "CFIXGender_Age_Code"
        Me.CFIXGender_Age_Code.OptionsColumn.AllowEdit = False
        Me.CFIXGender_Age_Code.OptionsColumn.ReadOnly = True
        Me.CFIXGender_Age_Code.Visible = True
        Me.CFIXGender_Age_Code.VisibleIndex = 28
        '
        'CFIXSO_NUMBER
        '
        Me.CFIXSO_NUMBER.Caption = "SO NUMBER"
        Me.CFIXSO_NUMBER.FieldName = "SO_NUMBER"
        Me.CFIXSO_NUMBER.Name = "CFIXSO_NUMBER"
        Me.CFIXSO_NUMBER.OptionsColumn.AllowEdit = False
        Me.CFIXSO_NUMBER.OptionsColumn.ReadOnly = True
        Me.CFIXSO_NUMBER.Visible = True
        Me.CFIXSO_NUMBER.VisibleIndex = 29
        '
        'CFIXSO_ITEM
        '
        Me.CFIXSO_ITEM.Caption = "SO ITEM"
        Me.CFIXSO_ITEM.FieldName = "SO_ITEM"
        Me.CFIXSO_ITEM.Name = "CFIXSO_ITEM"
        Me.CFIXSO_ITEM.OptionsColumn.AllowEdit = False
        Me.CFIXSO_ITEM.OptionsColumn.ReadOnly = True
        Me.CFIXSO_ITEM.Visible = True
        Me.CFIXSO_ITEM.VisibleIndex = 30
        '
        'CFIXColor_Combo_Name
        '
        Me.CFIXColor_Combo_Name.Caption = "Color Combo Name"
        Me.CFIXColor_Combo_Name.FieldName = "Color_Combo_Name"
        Me.CFIXColor_Combo_Name.Name = "CFIXColor_Combo_Name"
        Me.CFIXColor_Combo_Name.OptionsColumn.AllowEdit = False
        Me.CFIXColor_Combo_Name.OptionsColumn.ReadOnly = True
        Me.CFIXColor_Combo_Name.Visible = True
        Me.CFIXColor_Combo_Name.VisibleIndex = 31
        '
        'CFIXColor_Combo_ShortName
        '
        Me.CFIXColor_Combo_ShortName.Caption = "Color Combo ShortName"
        Me.CFIXColor_Combo_ShortName.FieldName = "Color_Combo_ShortName"
        Me.CFIXColor_Combo_ShortName.Name = "CFIXColor_Combo_ShortName"
        Me.CFIXColor_Combo_ShortName.OptionsColumn.AllowEdit = False
        Me.CFIXColor_Combo_ShortName.OptionsColumn.ReadOnly = True
        Me.CFIXColor_Combo_ShortName.Visible = True
        Me.CFIXColor_Combo_ShortName.VisibleIndex = 32
        '
        'CFIXMSRP_US
        '
        Me.CFIXMSRP_US.Caption = "MSRP_US"
        Me.CFIXMSRP_US.DisplayFormat.FormatString = "{0:n2}"
        Me.CFIXMSRP_US.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CFIXMSRP_US.FieldName = "MSRP_US"
        Me.CFIXMSRP_US.Name = "CFIXMSRP_US"
        Me.CFIXMSRP_US.OptionsColumn.AllowEdit = False
        Me.CFIXMSRP_US.OptionsColumn.ReadOnly = True
        Me.CFIXMSRP_US.Visible = True
        Me.CFIXMSRP_US.VisibleIndex = 33
        '
        'CFIXQuantity
        '
        Me.CFIXQuantity.Caption = "Quantity"
        Me.CFIXQuantity.DisplayFormat.FormatString = "{0:n0}"
        Me.CFIXQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CFIXQuantity.FieldName = "Quantity"
        Me.CFIXQuantity.Name = "CFIXQuantity"
        Me.CFIXQuantity.OptionsColumn.AllowEdit = False
        Me.CFIXQuantity.OptionsColumn.ReadOnly = True
        Me.CFIXQuantity.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Quantity", "{0:n0}")})
        Me.CFIXQuantity.Visible = True
        Me.CFIXQuantity.VisibleIndex = 34
        '
        'CFIXUCC_NAME1
        '
        Me.CFIXUCC_NAME1.Caption = "UCC_NAME1"
        Me.CFIXUCC_NAME1.FieldName = "UCC_NAME1"
        Me.CFIXUCC_NAME1.Name = "CFIXUCC_NAME1"
        Me.CFIXUCC_NAME1.OptionsColumn.AllowEdit = False
        Me.CFIXUCC_NAME1.OptionsColumn.ReadOnly = True
        Me.CFIXUCC_NAME1.Visible = True
        Me.CFIXUCC_NAME1.VisibleIndex = 18
        Me.CFIXUCC_NAME1.Width = 150
        '
        'CFIXShip_To_Account
        '
        Me.CFIXShip_To_Account.Caption = "Ship_To_Account"
        Me.CFIXShip_To_Account.FieldName = "cxShip_To_Account"
        Me.CFIXShip_To_Account.Name = "CFIXShip_To_Account"
        Me.CFIXShip_To_Account.OptionsColumn.AllowEdit = False
        Me.CFIXShip_To_Account.OptionsColumn.ReadOnly = True
        Me.CFIXShip_To_Account.Visible = True
        Me.CFIXShip_To_Account.VisibleIndex = 17
        Me.CFIXShip_To_Account.Width = 80
        '
        'wFactoryHubListOrderNo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1362, 531)
        Me.Controls.Add(Me.ogbFactoryOrderCopy)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "wFactoryHubListOrderNo"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "List Order Import"
        CType(Me.ogbFactoryOrderCopy, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbFactoryOrderCopy.ResumeLayout(False)
        CType(Me.ogdmain, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvmain, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepFTSelect, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogbFactoryOrderCopy As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ogdmain As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvmain As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents CFIXFTOrderNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFIXFTSelect As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepFTSelect As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents CFIXFactory_Vendor_Code As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFIXVendor_Location_Code_MCO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFIXFTNikePO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFIXPO_Doc_Date As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFIXPO_Number As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFIXPO_Ref As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFIXPO_Group As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFIXCurrency_Type As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFIXShip_Via_Instructions As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFIXBUY_SEASON As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFIXBUY_YEAR As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFIXBUY_GROUP As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFIXPO_Item As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFIXMaterial_Number As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFIXMaterial_Description As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFIXPlant As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFIXNike_Division_Code As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFIXUOM As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFIXMode_Code As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFIXMode_Code_Description As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFIXOGAC_Date As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFIXGAC_Date As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFIXGAC_Reason_Code As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFIXMaterial_Dev_Code As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFIXSilhhouette_Code As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFIXGender_Age_Code As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFIXSO_NUMBER As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFIXSO_ITEM As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFIXColor_Combo_Name As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFIXColor_Combo_ShortName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFIXMSRP_US As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFIXQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFIXShip_To_Account As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFIXUCC_NAME1 As DevExpress.XtraGrid.Columns.GridColumn
End Class

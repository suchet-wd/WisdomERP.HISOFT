<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wReserveAXItemPopup
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
        Me.ogcbarcode = New DevExpress.XtraGrid.GridControl()
        Me.ogvbarcode = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.FTSelect = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ReposFTSelect = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.BFTRawMatCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.BFTRawMatColorCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTRawMatColorName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.BFTRawMatSizeCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTBarcodeNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFTBatchNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.BFTOrderNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.xStockUnit = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.BFNQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.xFNOnReserve = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.xQtyAvaiable = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNReserveQty = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ReposFNReserveQty = New DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit()
        Me.FTPurchaseNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.xxFTOrderNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.xxFNHSysRawMatId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.xxSeason = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.xxSite = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.xxWarehouse = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.xxLocation = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.xxInventoryStatus = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.xxDataAreaId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.xxFTSeason = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.xxFTCmp = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.xxToSeason = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.xxToSite = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ogb = New DevExpress.XtraEditors.GroupControl()
        Me.FTStaReceiveAll = New DevExpress.XtraEditors.CheckEdit()
        Me.ocmcancel = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmok = New DevExpress.XtraEditors.SimpleButton()
        Me.FNHSysMainMatId_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysMainMatId = New DevExpress.XtraEditors.ButtonEdit()
        Me.ocmsearch = New DevExpress.XtraEditors.SimpleButton()
        Me.FTStateCheckMRP = New DevExpress.XtraEditors.CheckEdit()
        CType(Me.ogcbarcode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvbarcode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ReposFTSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ReposFNReserveQty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogb, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogb.SuspendLayout()
        CType(Me.FTStaReceiveAll.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysMainMatId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTStateCheckMRP.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ogcbarcode
        '
        Me.ogcbarcode.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogcbarcode.Location = New System.Drawing.Point(5, 24)
        Me.ogcbarcode.MainView = Me.ogvbarcode
        Me.ogcbarcode.Name = "ogcbarcode"
        Me.ogcbarcode.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.ReposFTSelect, Me.ReposFNReserveQty})
        Me.ogcbarcode.Size = New System.Drawing.Size(1202, 365)
        Me.ogcbarcode.TabIndex = 3
        Me.ogcbarcode.TabStop = False
        Me.ogcbarcode.Tag = "2|"
        Me.ogcbarcode.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvbarcode})
        '
        'ogvbarcode
        '
        Me.ogvbarcode.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.FTSelect, Me.BFTRawMatCode, Me.BFTRawMatColorCode, Me.FTRawMatColorName, Me.BFTRawMatSizeCode, Me.FTBarcodeNo, Me.CFTBatchNo, Me.BFTOrderNo, Me.xStockUnit, Me.BFNQuantity, Me.xFNOnReserve, Me.xQtyAvaiable, Me.FNReserveQty, Me.FTPurchaseNo, Me.xxFTOrderNo, Me.xxFNHSysRawMatId, Me.xxSeason, Me.xxSite, Me.xxWarehouse, Me.xxLocation, Me.xxInventoryStatus, Me.xxDataAreaId, Me.xxFTSeason, Me.xxFTCmp, Me.xxToSeason, Me.xxToSite})
        Me.ogvbarcode.DetailHeight = 284
        Me.ogvbarcode.GridControl = Me.ogcbarcode
        Me.ogvbarcode.Name = "ogvbarcode"
        Me.ogvbarcode.OptionsCustomization.AllowGroup = False
        Me.ogvbarcode.OptionsCustomization.AllowQuickHideColumns = False
        Me.ogvbarcode.OptionsView.ColumnAutoWidth = False
        Me.ogvbarcode.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.ogvbarcode.OptionsView.ShowFooter = True
        Me.ogvbarcode.OptionsView.ShowGroupPanel = False
        Me.ogvbarcode.Tag = "2|"
        '
        'FTSelect
        '
        Me.FTSelect.AppearanceCell.BackColor = System.Drawing.Color.LightCyan
        Me.FTSelect.AppearanceCell.Options.UseBackColor = True
        Me.FTSelect.AppearanceHeader.Options.UseTextOptions = True
        Me.FTSelect.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTSelect.Caption = " "
        Me.FTSelect.ColumnEdit = Me.ReposFTSelect
        Me.FTSelect.FieldName = "FTSelect"
        Me.FTSelect.MinWidth = 17
        Me.FTSelect.Name = "FTSelect"
        Me.FTSelect.OptionsColumn.AllowMove = False
        Me.FTSelect.Visible = True
        Me.FTSelect.VisibleIndex = 0
        Me.FTSelect.Width = 28
        '
        'ReposFTSelect
        '
        Me.ReposFTSelect.AutoHeight = False
        Me.ReposFTSelect.Caption = "Check"
        Me.ReposFTSelect.Name = "ReposFTSelect"
        Me.ReposFTSelect.ValueChecked = "1"
        Me.ReposFTSelect.ValueUnchecked = "0"
        '
        'BFTRawMatCode
        '
        Me.BFTRawMatCode.AppearanceHeader.Options.UseTextOptions = True
        Me.BFTRawMatCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.BFTRawMatCode.Caption = "FTRawMatCode"
        Me.BFTRawMatCode.FieldName = "FTRawMatCode"
        Me.BFTRawMatCode.MinWidth = 17
        Me.BFTRawMatCode.Name = "BFTRawMatCode"
        Me.BFTRawMatCode.OptionsColumn.AllowEdit = False
        Me.BFTRawMatCode.OptionsColumn.AllowMove = False
        Me.BFTRawMatCode.OptionsColumn.ReadOnly = True
        Me.BFTRawMatCode.Visible = True
        Me.BFTRawMatCode.VisibleIndex = 1
        Me.BFTRawMatCode.Width = 105
        '
        'BFTRawMatColorCode
        '
        Me.BFTRawMatColorCode.AppearanceHeader.Options.UseTextOptions = True
        Me.BFTRawMatColorCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.BFTRawMatColorCode.Caption = "FTRawMatColorCode"
        Me.BFTRawMatColorCode.FieldName = "FTRawMatColorCode"
        Me.BFTRawMatColorCode.MinWidth = 17
        Me.BFTRawMatColorCode.Name = "BFTRawMatColorCode"
        Me.BFTRawMatColorCode.OptionsColumn.AllowEdit = False
        Me.BFTRawMatColorCode.OptionsColumn.AllowMove = False
        Me.BFTRawMatColorCode.OptionsColumn.FixedWidth = True
        Me.BFTRawMatColorCode.OptionsColumn.ReadOnly = True
        Me.BFTRawMatColorCode.Visible = True
        Me.BFTRawMatColorCode.VisibleIndex = 2
        Me.BFTRawMatColorCode.Width = 69
        '
        'FTRawMatColorName
        '
        Me.FTRawMatColorName.Caption = "Color Name"
        Me.FTRawMatColorName.FieldName = "ColorName"
        Me.FTRawMatColorName.MinWidth = 17
        Me.FTRawMatColorName.Name = "FTRawMatColorName"
        Me.FTRawMatColorName.OptionsColumn.AllowEdit = False
        Me.FTRawMatColorName.OptionsColumn.FixedWidth = True
        Me.FTRawMatColorName.OptionsColumn.ReadOnly = True
        Me.FTRawMatColorName.Visible = True
        Me.FTRawMatColorName.VisibleIndex = 3
        Me.FTRawMatColorName.Width = 77
        '
        'BFTRawMatSizeCode
        '
        Me.BFTRawMatSizeCode.AppearanceHeader.Options.UseTextOptions = True
        Me.BFTRawMatSizeCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.BFTRawMatSizeCode.Caption = "FTRawMatSizeCode"
        Me.BFTRawMatSizeCode.FieldName = "SizeId"
        Me.BFTRawMatSizeCode.MinWidth = 17
        Me.BFTRawMatSizeCode.Name = "BFTRawMatSizeCode"
        Me.BFTRawMatSizeCode.OptionsColumn.AllowEdit = False
        Me.BFTRawMatSizeCode.OptionsColumn.AllowMove = False
        Me.BFTRawMatSizeCode.OptionsColumn.FixedWidth = True
        Me.BFTRawMatSizeCode.OptionsColumn.ReadOnly = True
        Me.BFTRawMatSizeCode.Visible = True
        Me.BFTRawMatSizeCode.VisibleIndex = 4
        Me.BFTRawMatSizeCode.Width = 69
        '
        'FTBarcodeNo
        '
        Me.FTBarcodeNo.AppearanceHeader.Options.UseTextOptions = True
        Me.FTBarcodeNo.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTBarcodeNo.Caption = "Licence Plate Id"
        Me.FTBarcodeNo.FieldName = "LicencePlateId"
        Me.FTBarcodeNo.MinWidth = 17
        Me.FTBarcodeNo.Name = "FTBarcodeNo"
        Me.FTBarcodeNo.OptionsColumn.AllowEdit = False
        Me.FTBarcodeNo.OptionsColumn.AllowMove = False
        Me.FTBarcodeNo.OptionsColumn.ReadOnly = True
        Me.FTBarcodeNo.Visible = True
        Me.FTBarcodeNo.VisibleIndex = 5
        Me.FTBarcodeNo.Width = 99
        '
        'CFTBatchNo
        '
        Me.CFTBatchNo.Caption = "Batch No"
        Me.CFTBatchNo.FieldName = "BatchNumber"
        Me.CFTBatchNo.MinWidth = 17
        Me.CFTBatchNo.Name = "CFTBatchNo"
        Me.CFTBatchNo.OptionsColumn.AllowEdit = False
        Me.CFTBatchNo.OptionsColumn.ReadOnly = True
        Me.CFTBatchNo.Visible = True
        Me.CFTBatchNo.VisibleIndex = 6
        Me.CFTBatchNo.Width = 64
        '
        'BFTOrderNo
        '
        Me.BFTOrderNo.AppearanceHeader.Options.UseTextOptions = True
        Me.BFTOrderNo.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.BFTOrderNo.Caption = "FTOrderNo"
        Me.BFTOrderNo.FieldName = "JobNumber"
        Me.BFTOrderNo.MinWidth = 17
        Me.BFTOrderNo.Name = "BFTOrderNo"
        Me.BFTOrderNo.OptionsColumn.AllowEdit = False
        Me.BFTOrderNo.OptionsColumn.AllowMove = False
        Me.BFTOrderNo.OptionsColumn.FixedWidth = True
        Me.BFTOrderNo.OptionsColumn.ReadOnly = True
        Me.BFTOrderNo.Visible = True
        Me.BFTOrderNo.VisibleIndex = 8
        Me.BFTOrderNo.Width = 77
        '
        'xStockUnit
        '
        Me.xStockUnit.Caption = "Unit"
        Me.xStockUnit.FieldName = "StockUnit"
        Me.xStockUnit.Name = "xStockUnit"
        Me.xStockUnit.OptionsColumn.AllowEdit = False
        Me.xStockUnit.OptionsColumn.ReadOnly = True
        Me.xStockUnit.Visible = True
        Me.xStockUnit.VisibleIndex = 9
        Me.xStockUnit.Width = 64
        '
        'BFNQuantity
        '
        Me.BFNQuantity.AppearanceCell.Options.UseTextOptions = True
        Me.BFNQuantity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.BFNQuantity.AppearanceHeader.Options.UseTextOptions = True
        Me.BFNQuantity.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.BFNQuantity.Caption = "Bal. Quantity"
        Me.BFNQuantity.DisplayFormat.FormatString = "{0:n4}"
        Me.BFNQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.BFNQuantity.FieldName = "PhysicalQty"
        Me.BFNQuantity.MinWidth = 17
        Me.BFNQuantity.Name = "BFNQuantity"
        Me.BFNQuantity.OptionsColumn.AllowEdit = False
        Me.BFNQuantity.OptionsColumn.AllowMove = False
        Me.BFNQuantity.OptionsColumn.ReadOnly = True
        Me.BFNQuantity.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "PhysicalQty", "{0:n4}")})
        Me.BFNQuantity.Visible = True
        Me.BFNQuantity.VisibleIndex = 10
        Me.BFNQuantity.Width = 87
        '
        'xFNOnReserve
        '
        Me.xFNOnReserve.Caption = "On Reserve"
        Me.xFNOnReserve.FieldName = "FNOnReserve"
        Me.xFNOnReserve.Name = "xFNOnReserve"
        Me.xFNOnReserve.OptionsColumn.AllowEdit = False
        Me.xFNOnReserve.OptionsColumn.ReadOnly = True
        Me.xFNOnReserve.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FNOnReserve", "{0:n4}")})
        Me.xFNOnReserve.Visible = True
        Me.xFNOnReserve.VisibleIndex = 11
        Me.xFNOnReserve.Width = 64
        '
        'xQtyAvaiable
        '
        Me.xQtyAvaiable.Caption = "Qty Avaiable"
        Me.xQtyAvaiable.DisplayFormat.FormatString = "{0:n4}"
        Me.xQtyAvaiable.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.xQtyAvaiable.FieldName = "QtyAvaiable"
        Me.xQtyAvaiable.Name = "xQtyAvaiable"
        Me.xQtyAvaiable.OptionsColumn.AllowEdit = False
        Me.xQtyAvaiable.OptionsColumn.ReadOnly = True
        Me.xQtyAvaiable.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "QtyAvaiable", "{0:n4}")})
        Me.xQtyAvaiable.Visible = True
        Me.xQtyAvaiable.VisibleIndex = 12
        Me.xQtyAvaiable.Width = 64
        '
        'FNReserveQty
        '
        Me.FNReserveQty.AppearanceCell.BackColor = System.Drawing.Color.LightCyan
        Me.FNReserveQty.AppearanceCell.Options.UseBackColor = True
        Me.FNReserveQty.AppearanceHeader.Options.UseTextOptions = True
        Me.FNReserveQty.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FNReserveQty.Caption = "Reserve Qty"
        Me.FNReserveQty.ColumnEdit = Me.ReposFNReserveQty
        Me.FNReserveQty.DisplayFormat.FormatString = "{0:n4}"
        Me.FNReserveQty.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNReserveQty.FieldName = "FNDocQuantity"
        Me.FNReserveQty.MinWidth = 17
        Me.FNReserveQty.Name = "FNReserveQty"
        Me.FNReserveQty.OptionsColumn.AllowMove = False
        Me.FNReserveQty.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FNDocQuantity", "{0:n4}")})
        Me.FNReserveQty.Visible = True
        Me.FNReserveQty.VisibleIndex = 13
        Me.FNReserveQty.Width = 104
        '
        'ReposFNReserveQty
        '
        Me.ReposFNReserveQty.AutoHeight = False
        Me.ReposFNReserveQty.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ReposFNReserveQty.DisplayFormat.FormatString = "N4"
        Me.ReposFNReserveQty.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.ReposFNReserveQty.EditFormat.FormatString = "N4"
        Me.ReposFNReserveQty.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.ReposFNReserveQty.Name = "ReposFNReserveQty"
        '
        'FTPurchaseNo
        '
        Me.FTPurchaseNo.Caption = "Purchase No"
        Me.FTPurchaseNo.FieldName = "PONumber"
        Me.FTPurchaseNo.MinWidth = 17
        Me.FTPurchaseNo.Name = "FTPurchaseNo"
        Me.FTPurchaseNo.OptionsColumn.AllowEdit = False
        Me.FTPurchaseNo.OptionsColumn.ReadOnly = True
        Me.FTPurchaseNo.Visible = True
        Me.FTPurchaseNo.VisibleIndex = 7
        Me.FTPurchaseNo.Width = 103
        '
        'xxFTOrderNo
        '
        Me.xxFTOrderNo.Caption = "FTOrderNo"
        Me.xxFTOrderNo.FieldName = "FTOrderNo"
        Me.xxFTOrderNo.Name = "xxFTOrderNo"
        Me.xxFTOrderNo.OptionsColumn.AllowEdit = False
        Me.xxFTOrderNo.OptionsColumn.ReadOnly = True
        Me.xxFTOrderNo.Width = 64
        '
        'xxFNHSysRawMatId
        '
        Me.xxFNHSysRawMatId.Caption = "FNHSysRawMatId"
        Me.xxFNHSysRawMatId.FieldName = "FNHSysRawMatId"
        Me.xxFNHSysRawMatId.Name = "xxFNHSysRawMatId"
        Me.xxFNHSysRawMatId.OptionsColumn.AllowEdit = False
        Me.xxFNHSysRawMatId.OptionsColumn.ReadOnly = True
        Me.xxFNHSysRawMatId.Width = 64
        '
        'xxSeason
        '
        Me.xxSeason.Caption = "Season"
        Me.xxSeason.FieldName = "Season"
        Me.xxSeason.Name = "xxSeason"
        Me.xxSeason.OptionsColumn.AllowEdit = False
        Me.xxSeason.OptionsColumn.ReadOnly = True
        Me.xxSeason.Width = 64
        '
        'xxSite
        '
        Me.xxSite.Caption = "[Site]"
        Me.xxSite.FieldName = "Site"
        Me.xxSite.Name = "xxSite"
        Me.xxSite.OptionsColumn.AllowEdit = False
        Me.xxSite.OptionsColumn.ReadOnly = True
        Me.xxSite.Width = 64
        '
        'xxWarehouse
        '
        Me.xxWarehouse.Caption = "Warehouse"
        Me.xxWarehouse.FieldName = "Warehouse"
        Me.xxWarehouse.Name = "xxWarehouse"
        Me.xxWarehouse.OptionsColumn.AllowEdit = False
        Me.xxWarehouse.OptionsColumn.ReadOnly = True
        Me.xxWarehouse.Width = 64
        '
        'xxLocation
        '
        Me.xxLocation.Caption = "Location"
        Me.xxLocation.FieldName = "Location"
        Me.xxLocation.Name = "xxLocation"
        Me.xxLocation.OptionsColumn.AllowEdit = False
        Me.xxLocation.OptionsColumn.ReadOnly = True
        Me.xxLocation.Width = 64
        '
        'xxInventoryStatus
        '
        Me.xxInventoryStatus.Caption = "InventoryStatus"
        Me.xxInventoryStatus.FieldName = "InventoryStatus"
        Me.xxInventoryStatus.Name = "xxInventoryStatus"
        Me.xxInventoryStatus.OptionsColumn.AllowEdit = False
        Me.xxInventoryStatus.OptionsColumn.ReadOnly = True
        Me.xxInventoryStatus.Width = 64
        '
        'xxDataAreaId
        '
        Me.xxDataAreaId.Caption = "DataAreaId"
        Me.xxDataAreaId.FieldName = "DataAreaId"
        Me.xxDataAreaId.Name = "xxDataAreaId"
        Me.xxDataAreaId.OptionsColumn.AllowEdit = False
        Me.xxDataAreaId.OptionsColumn.ReadOnly = True
        Me.xxDataAreaId.Width = 64
        '
        'xxFTSeason
        '
        Me.xxFTSeason.Caption = "FTSeason"
        Me.xxFTSeason.FieldName = "FTSeason"
        Me.xxFTSeason.Name = "xxFTSeason"
        Me.xxFTSeason.OptionsColumn.AllowEdit = False
        Me.xxFTSeason.OptionsColumn.ReadOnly = True
        Me.xxFTSeason.Width = 64
        '
        'xxFTCmp
        '
        Me.xxFTCmp.Caption = "FTCmp"
        Me.xxFTCmp.FieldName = "FTCmp"
        Me.xxFTCmp.Name = "xxFTCmp"
        Me.xxFTCmp.OptionsColumn.AllowEdit = False
        Me.xxFTCmp.OptionsColumn.ReadOnly = True
        Me.xxFTCmp.Width = 64
        '
        'xxToSeason
        '
        Me.xxToSeason.Caption = "ToSeason"
        Me.xxToSeason.FieldName = "ToSeason"
        Me.xxToSeason.Name = "xxToSeason"
        '
        'xxToSite
        '
        Me.xxToSite.Caption = "ToSite"
        Me.xxToSite.FieldName = "ToSite"
        Me.xxToSite.Name = "xxToSite"
        '
        'ogb
        '
        Me.ogb.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogb.Controls.Add(Me.FTStaReceiveAll)
        Me.ogb.Controls.Add(Me.ocmcancel)
        Me.ogb.Controls.Add(Me.ocmok)
        Me.ogb.Controls.Add(Me.ogcbarcode)
        Me.ogb.Location = New System.Drawing.Point(1, 42)
        Me.ogb.Name = "ogb"
        Me.ogb.Size = New System.Drawing.Size(1208, 393)
        Me.ogb.TabIndex = 4
        Me.ogb.Text = "Barcode Detail"
        '
        'FTStaReceiveAll
        '
        Me.FTStaReceiveAll.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FTStaReceiveAll.Location = New System.Drawing.Point(695, 1)
        Me.FTStaReceiveAll.Name = "FTStaReceiveAll"
        Me.FTStaReceiveAll.Properties.Appearance.Options.UseTextOptions = True
        Me.FTStaReceiveAll.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTStaReceiveAll.Properties.Caption = "Receive All"
        Me.FTStaReceiveAll.Properties.GlyphAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTStaReceiveAll.Properties.ValueChecked = "1"
        Me.FTStaReceiveAll.Properties.ValueUnchecked = "0"
        Me.FTStaReceiveAll.Size = New System.Drawing.Size(151, 20)
        Me.FTStaReceiveAll.TabIndex = 105
        Me.FTStaReceiveAll.Tag = "2|"
        Me.FTStaReceiveAll.Visible = False
        '
        'ocmcancel
        '
        Me.ocmcancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmcancel.Location = New System.Drawing.Point(1033, 1)
        Me.ocmcancel.Name = "ocmcancel"
        Me.ocmcancel.Size = New System.Drawing.Size(160, 20)
        Me.ocmcancel.TabIndex = 104
        Me.ocmcancel.TabStop = False
        Me.ocmcancel.Tag = "2|"
        Me.ocmcancel.Text = "CANCEL"
        '
        'ocmok
        '
        Me.ocmok.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmok.Location = New System.Drawing.Point(858, 1)
        Me.ocmok.Name = "ocmok"
        Me.ocmok.Size = New System.Drawing.Size(160, 20)
        Me.ocmok.TabIndex = 103
        Me.ocmok.TabStop = False
        Me.ocmok.Tag = "2|"
        Me.ocmok.Text = "OK"
        '
        'FNHSysMainMatId_lbl
        '
        Me.FNHSysMainMatId_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FNHSysMainMatId_lbl.Appearance.Options.UseForeColor = True
        Me.FNHSysMainMatId_lbl.Appearance.Options.UseTextOptions = True
        Me.FNHSysMainMatId_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysMainMatId_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysMainMatId_lbl.Location = New System.Drawing.Point(75, 13)
        Me.FNHSysMainMatId_lbl.Name = "FNHSysMainMatId_lbl"
        Me.FNHSysMainMatId_lbl.Size = New System.Drawing.Size(142, 19)
        Me.FNHSysMainMatId_lbl.TabIndex = 292
        Me.FNHSysMainMatId_lbl.Tag = "2|"
        Me.FNHSysMainMatId_lbl.Text = "Material Code :"
        '
        'FNHSysMainMatId
        '
        Me.FNHSysMainMatId.EnterMoveNextControl = True
        Me.FNHSysMainMatId.Location = New System.Drawing.Point(221, 12)
        Me.FNHSysMainMatId.Name = "FNHSysMainMatId"
        Me.FNHSysMainMatId.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FNHSysMainMatId.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysMainMatId.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysMainMatId.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysMainMatId.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FNHSysMainMatId.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FNHSysMainMatId.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNHSysMainMatId.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysMainMatId.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNHSysMainMatId.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNHSysMainMatId.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysMainMatId.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysMainMatId.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNHSysMainMatId.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNHSysMainMatId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", 15, True, True, False, EditorButtonImageOptions1, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, SerializableAppearanceObject2, SerializableAppearanceObject3, SerializableAppearanceObject4, "", "274", Nothing, DevExpress.Utils.ToolTipAnchor.[Default])})
        Me.FNHSysMainMatId.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.FNHSysMainMatId.Properties.MaxLength = 30
        Me.FNHSysMainMatId.Size = New System.Drawing.Size(268, 20)
        Me.FNHSysMainMatId.TabIndex = 291
        Me.FNHSysMainMatId.Tag = "2|"
        '
        'ocmsearch
        '
        Me.ocmsearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmsearch.Location = New System.Drawing.Point(495, 12)
        Me.ocmsearch.Name = "ocmsearch"
        Me.ocmsearch.Size = New System.Drawing.Size(160, 20)
        Me.ocmsearch.TabIndex = 293
        Me.ocmsearch.TabStop = False
        Me.ocmsearch.Tag = "2|"
        Me.ocmsearch.Text = "Search Data Barcode"
        '
        'FTStateCheckMRP
        '
        Me.FTStateCheckMRP.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FTStateCheckMRP.EditValue = "1"
        Me.FTStateCheckMRP.Location = New System.Drawing.Point(716, 12)
        Me.FTStateCheckMRP.Name = "FTStateCheckMRP"
        Me.FTStateCheckMRP.Properties.Caption = "Check MRP"
        Me.FTStateCheckMRP.Properties.ValueChecked = "1"
        Me.FTStateCheckMRP.Properties.ValueUnchecked = "0"
        Me.FTStateCheckMRP.Size = New System.Drawing.Size(174, 20)
        Me.FTStateCheckMRP.TabIndex = 294
        Me.FTStateCheckMRP.Tag = "2|"
        '
        'wReserveAXItemPopup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1211, 437)
        Me.ControlBox = False
        Me.Controls.Add(Me.FTStateCheckMRP)
        Me.Controls.Add(Me.ocmsearch)
        Me.Controls.Add(Me.FNHSysMainMatId_lbl)
        Me.Controls.Add(Me.FNHSysMainMatId)
        Me.Controls.Add(Me.ogb)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "wReserveAXItemPopup"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Reserve Item"
        CType(Me.ogcbarcode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvbarcode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ReposFTSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ReposFNReserveQty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogb, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogb.ResumeLayout(False)
        CType(Me.FTStaReceiveAll.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysMainMatId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTStateCheckMRP.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogcbarcode As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvbarcode As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents FTSelect As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ReposFTSelect As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents FTBarcodeNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents BFTRawMatCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents BFTRawMatColorCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents BFTRawMatSizeCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents BFTOrderNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents BFNQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNReserveQty As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ReposFNReserveQty As DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit
    Friend WithEvents ogb As DevExpress.XtraEditors.GroupControl
    Friend WithEvents FTStaReceiveAll As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents ocmcancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmok As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FTPurchaseNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTRawMatColorName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFTBatchNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents xStockUnit As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents xFNOnReserve As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents xQtyAvaiable As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents xxFTOrderNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents xxFNHSysRawMatId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents xxSeason As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents xxSite As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents xxWarehouse As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents xxLocation As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents xxInventoryStatus As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents xxDataAreaId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents xxFTSeason As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents xxFTCmp As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents xxToSeason As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents xxToSite As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNHSysMainMatId_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysMainMatId As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents ocmsearch As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FTStateCheckMRP As DevExpress.XtraEditors.CheckEdit
End Class

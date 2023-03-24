<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wReceiveMultipleAsset
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
        Me.ogcrcv = New DevExpress.XtraGrid.GridControl()
        Me.ogvrcv = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.FNHSysRawMatId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTRawMatCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTMatDesc = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTRawMatColorCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTRawMatSizeCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTFabricFrontSize = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ReposFTFabricFrontSize = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.FNTSysUnitId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTUnitCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNPrice = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNDisPer = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNDisAmt = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNNetPrice = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ResFNQuantity = New DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit()
        Me.FTProductNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FDShipDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNUsedQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNNetAmt = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNPOQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNTCQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNRTSQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNOrderRcvQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ResFNRcvQty = New DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit()
        Me.FNConvRatio = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNPricePerStock = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNHSysUnitIdStock = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCalcEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit()
        Me.ResFNPOBalQty = New DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit()
        Me.ResFNRcvHisQty = New DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit()
        Me.ResFTStateSelect = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.ogb = New DevExpress.XtraEditors.GroupControl()
        Me.CFNQuantity = New DevExpress.XtraEditors.CalcEdit()
        Me.FNQuantity_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysFixedAssetId = New DevExpress.XtraEditors.TextEdit()
        Me.FNHSysFixedAssetId_None_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTAssetModelCode_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysFixedAssetId_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTAssetModelCode = New DevExpress.XtraEditors.TextEdit()
        Me.FNHSysFixedAssetId_None = New DevExpress.XtraEditors.MemoEdit()
        Me.ocmcancel = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmreceive = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.ogcrcv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvrcv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ReposFTFabricFrontSize, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ResFNQuantity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ResFNRcvQty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCalcEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ResFNPOBalQty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ResFNRcvHisQty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ResFTStateSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogb, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogb.SuspendLayout()
        CType(Me.CFNQuantity.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysFixedAssetId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTAssetModelCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysFixedAssetId_None.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ogcrcv
        '
        Me.ogcrcv.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogcrcv.Location = New System.Drawing.Point(5, 134)
        Me.ogcrcv.MainView = Me.ogvrcv
        Me.ogcrcv.Name = "ogcrcv"
        Me.ogcrcv.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCalcEdit1, Me.ResFNPOBalQty, Me.ResFNQuantity, Me.ResFNRcvHisQty, Me.ResFNRcvQty, Me.ResFTStateSelect, Me.ReposFTFabricFrontSize})
        Me.ogcrcv.Size = New System.Drawing.Size(853, 241)
        Me.ogcrcv.TabIndex = 3
        Me.ogcrcv.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvrcv})
        '
        'ogvrcv
        '
        Me.ogvrcv.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.FNHSysRawMatId, Me.FTRawMatCode, Me.FTMatDesc, Me.FTRawMatColorCode, Me.FTRawMatSizeCode, Me.FTFabricFrontSize, Me.FNTSysUnitId, Me.FTUnitCode, Me.FNPrice, Me.FNDisPer, Me.FNDisAmt, Me.FNNetPrice, Me.FNQuantity, Me.FTProductNo, Me.FDShipDate, Me.FNUsedQuantity, Me.FNNetAmt, Me.FNPOQuantity, Me.FNTCQuantity, Me.FNRTSQuantity, Me.FNOrderRcvQuantity, Me.FNConvRatio, Me.FNPricePerStock, Me.FNHSysUnitIdStock})
        Me.ogvrcv.GridControl = Me.ogcrcv
        Me.ogvrcv.Name = "ogvrcv"
        Me.ogvrcv.OptionsCustomization.AllowGroup = False
        Me.ogvrcv.OptionsCustomization.AllowQuickHideColumns = False
        Me.ogvrcv.OptionsView.ColumnAutoWidth = False
        Me.ogvrcv.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.ogvrcv.OptionsView.ShowAutoFilterRow = True
        Me.ogvrcv.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
        Me.ogvrcv.OptionsView.ShowGroupPanel = False
        '
        'FNHSysRawMatId
        '
        Me.FNHSysRawMatId.Caption = "FNHSysRawMatId"
        Me.FNHSysRawMatId.FieldName = "FNHSysRawMatId"
        Me.FNHSysRawMatId.Name = "FNHSysRawMatId"
        Me.FNHSysRawMatId.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        '
        'FTRawMatCode
        '
        Me.FTRawMatCode.AppearanceHeader.Options.UseTextOptions = True
        Me.FTRawMatCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTRawMatCode.Caption = "FTRawMatCode"
        Me.FTRawMatCode.FieldName = "FTRawMatCode"
        Me.FTRawMatCode.Name = "FTRawMatCode"
        Me.FTRawMatCode.OptionsColumn.AllowEdit = False
        Me.FTRawMatCode.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.FTRawMatCode.OptionsColumn.AllowMove = False
        Me.FTRawMatCode.OptionsColumn.ReadOnly = True
        Me.FTRawMatCode.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        Me.FTRawMatCode.Width = 100
        '
        'FTMatDesc
        '
        Me.FTMatDesc.AppearanceHeader.Options.UseTextOptions = True
        Me.FTMatDesc.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTMatDesc.Caption = "FTMatDesc"
        Me.FTMatDesc.FieldName = "FTMatDesc"
        Me.FTMatDesc.Name = "FTMatDesc"
        Me.FTMatDesc.OptionsColumn.AllowEdit = False
        Me.FTMatDesc.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.FTMatDesc.OptionsColumn.AllowMove = False
        Me.FTMatDesc.OptionsColumn.ReadOnly = True
        Me.FTMatDesc.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        Me.FTMatDesc.Width = 150
        '
        'FTRawMatColorCode
        '
        Me.FTRawMatColorCode.AppearanceHeader.Options.UseTextOptions = True
        Me.FTRawMatColorCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTRawMatColorCode.Caption = "FTRawMatColorCode"
        Me.FTRawMatColorCode.FieldName = "FTRawMatColorCode"
        Me.FTRawMatColorCode.Name = "FTRawMatColorCode"
        Me.FTRawMatColorCode.OptionsColumn.AllowEdit = False
        Me.FTRawMatColorCode.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.FTRawMatColorCode.OptionsColumn.AllowMove = False
        Me.FTRawMatColorCode.OptionsColumn.ReadOnly = True
        Me.FTRawMatColorCode.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        Me.FTRawMatColorCode.Width = 106
        '
        'FTRawMatSizeCode
        '
        Me.FTRawMatSizeCode.AppearanceHeader.Options.UseTextOptions = True
        Me.FTRawMatSizeCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTRawMatSizeCode.Caption = "FTRawMatSizeCode"
        Me.FTRawMatSizeCode.FieldName = "FTRawMatSizeCode"
        Me.FTRawMatSizeCode.Name = "FTRawMatSizeCode"
        Me.FTRawMatSizeCode.OptionsColumn.AllowEdit = False
        Me.FTRawMatSizeCode.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.FTRawMatSizeCode.OptionsColumn.AllowMove = False
        Me.FTRawMatSizeCode.OptionsColumn.ReadOnly = True
        Me.FTRawMatSizeCode.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        '
        'FTFabricFrontSize
        '
        Me.FTFabricFrontSize.AppearanceHeader.Options.UseTextOptions = True
        Me.FTFabricFrontSize.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTFabricFrontSize.Caption = "FTFabricFrontSize"
        Me.FTFabricFrontSize.ColumnEdit = Me.ReposFTFabricFrontSize
        Me.FTFabricFrontSize.FieldName = "FTFabricFrontSize"
        Me.FTFabricFrontSize.Name = "FTFabricFrontSize"
        Me.FTFabricFrontSize.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.FTFabricFrontSize.OptionsColumn.AllowMove = False
        Me.FTFabricFrontSize.Width = 87
        '
        'ReposFTFabricFrontSize
        '
        Me.ReposFTFabricFrontSize.AutoHeight = False
        Me.ReposFTFabricFrontSize.MaxLength = 50
        Me.ReposFTFabricFrontSize.Name = "ReposFTFabricFrontSize"
        '
        'FNTSysUnitId
        '
        Me.FNTSysUnitId.Caption = "FNTSysUnitId"
        Me.FNTSysUnitId.FieldName = "FNTSysUnitId"
        Me.FNTSysUnitId.Name = "FNTSysUnitId"
        Me.FNTSysUnitId.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        '
        'FTUnitCode
        '
        Me.FTUnitCode.AppearanceCell.Options.UseTextOptions = True
        Me.FTUnitCode.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTUnitCode.AppearanceHeader.Options.UseTextOptions = True
        Me.FTUnitCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTUnitCode.Caption = "FTUnitCode"
        Me.FTUnitCode.FieldName = "FTUnitCode"
        Me.FTUnitCode.Name = "FTUnitCode"
        Me.FTUnitCode.OptionsColumn.AllowEdit = False
        Me.FTUnitCode.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.FTUnitCode.OptionsColumn.AllowMove = False
        Me.FTUnitCode.OptionsColumn.ReadOnly = True
        Me.FTUnitCode.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        Me.FTUnitCode.Width = 60
        '
        'FNPrice
        '
        Me.FNPrice.Caption = "FNPrice"
        Me.FNPrice.FieldName = "FNPrice"
        Me.FNPrice.Name = "FNPrice"
        Me.FNPrice.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        '
        'FNDisPer
        '
        Me.FNDisPer.Caption = "FNDisPer"
        Me.FNDisPer.FieldName = "FNDisPer"
        Me.FNDisPer.Name = "FNDisPer"
        Me.FNDisPer.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        '
        'FNDisAmt
        '
        Me.FNDisAmt.Caption = "FNDisAmt"
        Me.FNDisAmt.FieldName = "FNDisAmt"
        Me.FNDisAmt.Name = "FNDisAmt"
        Me.FNDisAmt.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        '
        'FNNetPrice
        '
        Me.FNNetPrice.Caption = "FNNetPrice"
        Me.FNNetPrice.FieldName = "FNNetPrice"
        Me.FNNetPrice.Name = "FNNetPrice"
        Me.FNNetPrice.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        '
        'FNQuantity
        '
        Me.FNQuantity.AppearanceCell.Options.UseTextOptions = True
        Me.FNQuantity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNQuantity.AppearanceHeader.Options.UseTextOptions = True
        Me.FNQuantity.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FNQuantity.Caption = "FNQuantity"
        Me.FNQuantity.ColumnEdit = Me.ResFNQuantity
        Me.FNQuantity.DisplayFormat.FormatString = "{0:n4}"
        Me.FNQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNQuantity.FieldName = "FNQuantity"
        Me.FNQuantity.Name = "FNQuantity"
        Me.FNQuantity.OptionsColumn.AllowEdit = False
        Me.FNQuantity.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.FNQuantity.OptionsColumn.AllowMove = False
        Me.FNQuantity.OptionsColumn.ReadOnly = True
        Me.FNQuantity.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        Me.FNQuantity.Width = 103
        '
        'ResFNQuantity
        '
        Me.ResFNQuantity.AutoHeight = False
        Me.ResFNQuantity.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ResFNQuantity.Name = "ResFNQuantity"
        '
        'FTProductNo
        '
        Me.FTProductNo.Caption = "FTProductNo"
        Me.FTProductNo.FieldName = "FTProductNo"
        Me.FTProductNo.Name = "FTProductNo"
        Me.FTProductNo.OptionsColumn.AllowEdit = False
        Me.FTProductNo.OptionsColumn.ReadOnly = True
        Me.FTProductNo.Visible = True
        Me.FTProductNo.VisibleIndex = 0
        Me.FTProductNo.Width = 112
        '
        'FDShipDate
        '
        Me.FDShipDate.AppearanceCell.Options.UseTextOptions = True
        Me.FDShipDate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FDShipDate.Caption = "Shipment Date"
        Me.FDShipDate.FieldName = "FDShipDate"
        Me.FDShipDate.Name = "FDShipDate"
        Me.FDShipDate.OptionsColumn.AllowEdit = False
        Me.FDShipDate.OptionsColumn.ReadOnly = True
        Me.FDShipDate.Visible = True
        Me.FDShipDate.VisibleIndex = 1
        Me.FDShipDate.Width = 109
        '
        'FNUsedQuantity
        '
        Me.FNUsedQuantity.Caption = "Used Qty"
        Me.FNUsedQuantity.DisplayFormat.FormatString = "{0:n4}"
        Me.FNUsedQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNUsedQuantity.FieldName = "FNUsedQuantity"
        Me.FNUsedQuantity.Name = "FNUsedQuantity"
        Me.FNUsedQuantity.OptionsColumn.AllowEdit = False
        Me.FNUsedQuantity.OptionsColumn.ReadOnly = True
        Me.FNUsedQuantity.Visible = True
        Me.FNUsedQuantity.VisibleIndex = 2
        Me.FNUsedQuantity.Width = 93
        '
        'FNNetAmt
        '
        Me.FNNetAmt.Caption = "FNNetAmt"
        Me.FNNetAmt.FieldName = "FNNetAmt"
        Me.FNNetAmt.Name = "FNNetAmt"
        Me.FNNetAmt.OptionsColumn.AllowEdit = False
        Me.FNNetAmt.OptionsColumn.ReadOnly = True
        '
        'FNPOQuantity
        '
        Me.FNPOQuantity.AppearanceCell.Options.UseTextOptions = True
        Me.FNPOQuantity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNPOQuantity.AppearanceHeader.Options.UseTextOptions = True
        Me.FNPOQuantity.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FNPOQuantity.Caption = "PO Qty"
        Me.FNPOQuantity.DisplayFormat.FormatString = "{0:n4}"
        Me.FNPOQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNPOQuantity.FieldName = "FNPOQuantity"
        Me.FNPOQuantity.Name = "FNPOQuantity"
        Me.FNPOQuantity.OptionsColumn.AllowEdit = False
        Me.FNPOQuantity.OptionsColumn.AllowMove = False
        Me.FNPOQuantity.OptionsColumn.ReadOnly = True
        Me.FNPOQuantity.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        Me.FNPOQuantity.Visible = True
        Me.FNPOQuantity.VisibleIndex = 3
        Me.FNPOQuantity.Width = 99
        '
        'FNTCQuantity
        '
        Me.FNTCQuantity.AppearanceCell.Options.UseTextOptions = True
        Me.FNTCQuantity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNTCQuantity.AppearanceHeader.Options.UseTextOptions = True
        Me.FNTCQuantity.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FNTCQuantity.Caption = "Rev Qty BF."
        Me.FNTCQuantity.DisplayFormat.FormatString = "{0:n4}"
        Me.FNTCQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNTCQuantity.FieldName = "FNTCQuantity"
        Me.FNTCQuantity.Name = "FNTCQuantity"
        Me.FNTCQuantity.OptionsColumn.AllowEdit = False
        Me.FNTCQuantity.OptionsColumn.AllowMove = False
        Me.FNTCQuantity.OptionsColumn.ReadOnly = True
        Me.FNTCQuantity.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        Me.FNTCQuantity.Visible = True
        Me.FNTCQuantity.VisibleIndex = 4
        Me.FNTCQuantity.Width = 120
        '
        'FNRTSQuantity
        '
        Me.FNRTSQuantity.AppearanceCell.Options.UseTextOptions = True
        Me.FNRTSQuantity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNRTSQuantity.Caption = "RTS Qty."
        Me.FNRTSQuantity.DisplayFormat.FormatString = "{0:n4}"
        Me.FNRTSQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNRTSQuantity.FieldName = "FNRTSQuantity"
        Me.FNRTSQuantity.Name = "FNRTSQuantity"
        Me.FNRTSQuantity.OptionsColumn.AllowEdit = False
        Me.FNRTSQuantity.OptionsColumn.ReadOnly = True
        Me.FNRTSQuantity.Visible = True
        Me.FNRTSQuantity.VisibleIndex = 5
        Me.FNRTSQuantity.Width = 106
        '
        'FNOrderRcvQuantity
        '
        Me.FNOrderRcvQuantity.AppearanceCell.Options.UseTextOptions = True
        Me.FNOrderRcvQuantity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNOrderRcvQuantity.AppearanceHeader.Options.UseTextOptions = True
        Me.FNOrderRcvQuantity.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FNOrderRcvQuantity.Caption = "RCV Qty."
        Me.FNOrderRcvQuantity.ColumnEdit = Me.ResFNRcvQty
        Me.FNOrderRcvQuantity.DisplayFormat.FormatString = "{0:n4}"
        Me.FNOrderRcvQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNOrderRcvQuantity.FieldName = "FNOrderRcvQuantity"
        Me.FNOrderRcvQuantity.Name = "FNOrderRcvQuantity"
        Me.FNOrderRcvQuantity.OptionsColumn.AllowMove = False
        Me.FNOrderRcvQuantity.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        Me.FNOrderRcvQuantity.Visible = True
        Me.FNOrderRcvQuantity.VisibleIndex = 6
        Me.FNOrderRcvQuantity.Width = 121
        '
        'ResFNRcvQty
        '
        Me.ResFNRcvQty.AutoHeight = False
        Me.ResFNRcvQty.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ResFNRcvQty.Name = "ResFNRcvQty"
        '
        'FNConvRatio
        '
        Me.FNConvRatio.Caption = "FNConvRatio"
        Me.FNConvRatio.FieldName = "FNConvRatio"
        Me.FNConvRatio.Name = "FNConvRatio"
        Me.FNConvRatio.OptionsColumn.AllowEdit = False
        Me.FNConvRatio.OptionsColumn.ReadOnly = True
        Me.FNConvRatio.OptionsColumn.ShowInCustomizationForm = False
        '
        'FNPricePerStock
        '
        Me.FNPricePerStock.Caption = "FNPricePerStock"
        Me.FNPricePerStock.FieldName = "FNPricePerStock"
        Me.FNPricePerStock.Name = "FNPricePerStock"
        Me.FNPricePerStock.OptionsColumn.AllowEdit = False
        Me.FNPricePerStock.OptionsColumn.ReadOnly = True
        Me.FNPricePerStock.OptionsColumn.ShowInCustomizationForm = False
        '
        'FNHSysUnitIdStock
        '
        Me.FNHSysUnitIdStock.Caption = "FNHSysUnitIdStock"
        Me.FNHSysUnitIdStock.FieldName = "FNHSysUnitIdStock"
        Me.FNHSysUnitIdStock.Name = "FNHSysUnitIdStock"
        Me.FNHSysUnitIdStock.OptionsColumn.AllowEdit = False
        Me.FNHSysUnitIdStock.OptionsColumn.ReadOnly = True
        '
        'RepositoryItemCalcEdit1
        '
        Me.RepositoryItemCalcEdit1.AutoHeight = False
        Me.RepositoryItemCalcEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemCalcEdit1.Name = "RepositoryItemCalcEdit1"
        Me.RepositoryItemCalcEdit1.Precision = 4
        '
        'ResFNPOBalQty
        '
        Me.ResFNPOBalQty.AutoHeight = False
        Me.ResFNPOBalQty.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ResFNPOBalQty.Name = "ResFNPOBalQty"
        '
        'ResFNRcvHisQty
        '
        Me.ResFNRcvHisQty.AutoHeight = False
        Me.ResFNRcvHisQty.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ResFNRcvHisQty.Name = "ResFNRcvHisQty"
        '
        'ResFTStateSelect
        '
        Me.ResFTStateSelect.AutoHeight = False
        Me.ResFTStateSelect.Caption = "Check"
        Me.ResFTStateSelect.Name = "ResFTStateSelect"
        Me.ResFTStateSelect.ValueChecked = "1"
        Me.ResFTStateSelect.ValueUnchecked = "0"
        '
        'ogb
        '
        Me.ogb.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogb.Controls.Add(Me.CFNQuantity)
        Me.ogb.Controls.Add(Me.FNQuantity_lbl)
        Me.ogb.Controls.Add(Me.FNHSysFixedAssetId)
        Me.ogb.Controls.Add(Me.FNHSysFixedAssetId_None_lbl)
        Me.ogb.Controls.Add(Me.FTAssetModelCode_lbl)
        Me.ogb.Controls.Add(Me.FNHSysFixedAssetId_lbl)
        Me.ogb.Controls.Add(Me.FTAssetModelCode)
        Me.ogb.Controls.Add(Me.FNHSysFixedAssetId_None)
        Me.ogb.Controls.Add(Me.ocmcancel)
        Me.ogb.Controls.Add(Me.ocmreceive)
        Me.ogb.Controls.Add(Me.ogcrcv)
        Me.ogb.Location = New System.Drawing.Point(2, 2)
        Me.ogb.Name = "ogb"
        Me.ogb.Size = New System.Drawing.Size(863, 380)
        Me.ogb.TabIndex = 6
        Me.ogb.Tag = "2|"
        Me.ogb.Text = "Multiple Detail"
        '
        'CFNQuantity
        '
        Me.CFNQuantity.EnterMoveNextControl = True
        Me.CFNQuantity.Location = New System.Drawing.Point(675, 108)
        Me.CFNQuantity.Name = "CFNQuantity"
        Me.CFNQuantity.Properties.Appearance.Options.UseTextOptions = True
        Me.CFNQuantity.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.CFNQuantity.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.CFNQuantity.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.CFNQuantity.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.CFNQuantity.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.CFNQuantity.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.CFNQuantity.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.CFNQuantity.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.CFNQuantity.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.CFNQuantity.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, True, False, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", Nothing, Nothing, True)})
        Me.CFNQuantity.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CFNQuantity.Properties.Precision = 4
        Me.CFNQuantity.Properties.ReadOnly = True
        Me.CFNQuantity.Size = New System.Drawing.Size(144, 20)
        Me.CFNQuantity.TabIndex = 296
        Me.CFNQuantity.Tag = "2|"
        '
        'FNQuantity_lbl
        '
        Me.FNQuantity_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNQuantity_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNQuantity_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNQuantity_lbl.Location = New System.Drawing.Point(577, 107)
        Me.FNQuantity_lbl.Name = "FNQuantity_lbl"
        Me.FNQuantity_lbl.Size = New System.Drawing.Size(97, 19)
        Me.FNQuantity_lbl.TabIndex = 297
        Me.FNQuantity_lbl.Tag = "2|"
        Me.FNQuantity_lbl.Text = "Quantity :"
        '
        'FNHSysFixedAssetId
        '
        Me.FNHSysFixedAssetId.EnterMoveNextControl = True
        Me.FNHSysFixedAssetId.Location = New System.Drawing.Point(137, 26)
        Me.FNHSysFixedAssetId.Name = "FNHSysFixedAssetId"
        Me.FNHSysFixedAssetId.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FNHSysFixedAssetId.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FNHSysFixedAssetId.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysFixedAssetId.Properties.Appearance.Options.UseForeColor = True
        Me.FNHSysFixedAssetId.Properties.Appearance.Options.UseTextOptions = True
        Me.FNHSysFixedAssetId.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FNHSysFixedAssetId.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysFixedAssetId.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysFixedAssetId.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FNHSysFixedAssetId.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FNHSysFixedAssetId.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNHSysFixedAssetId.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysFixedAssetId.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNHSysFixedAssetId.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNHSysFixedAssetId.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysFixedAssetId.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysFixedAssetId.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNHSysFixedAssetId.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNHSysFixedAssetId.Properties.ReadOnly = True
        Me.FNHSysFixedAssetId.Size = New System.Drawing.Size(151, 20)
        Me.FNHSysFixedAssetId.TabIndex = 295
        Me.FNHSysFixedAssetId.TabStop = False
        Me.FNHSysFixedAssetId.Tag = "2|"
        '
        'FNHSysFixedAssetId_None_lbl
        '
        Me.FNHSysFixedAssetId_None_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FNHSysFixedAssetId_None_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysFixedAssetId_None_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysFixedAssetId_None_lbl.Location = New System.Drawing.Point(12, 50)
        Me.FNHSysFixedAssetId_None_lbl.Name = "FNHSysFixedAssetId_None_lbl"
        Me.FNHSysFixedAssetId_None_lbl.Size = New System.Drawing.Size(121, 19)
        Me.FNHSysFixedAssetId_None_lbl.TabIndex = 293
        Me.FNHSysFixedAssetId_None_lbl.Tag = "2|"
        Me.FNHSysFixedAssetId_None_lbl.Text = "Asset Code :"
        '
        'FTAssetModelCode_lbl
        '
        Me.FTAssetModelCode_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTAssetModelCode_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTAssetModelCode_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTAssetModelCode_lbl.Location = New System.Drawing.Point(28, 113)
        Me.FTAssetModelCode_lbl.Name = "FTAssetModelCode_lbl"
        Me.FTAssetModelCode_lbl.Size = New System.Drawing.Size(107, 19)
        Me.FTAssetModelCode_lbl.TabIndex = 291
        Me.FTAssetModelCode_lbl.Tag = "2|"
        Me.FTAssetModelCode_lbl.Text = "Model Code :"
        '
        'FNHSysFixedAssetId_lbl
        '
        Me.FNHSysFixedAssetId_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysFixedAssetId_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysFixedAssetId_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysFixedAssetId_lbl.Location = New System.Drawing.Point(12, 28)
        Me.FNHSysFixedAssetId_lbl.Name = "FNHSysFixedAssetId_lbl"
        Me.FNHSysFixedAssetId_lbl.Size = New System.Drawing.Size(121, 19)
        Me.FNHSysFixedAssetId_lbl.TabIndex = 290
        Me.FNHSysFixedAssetId_lbl.Tag = "2|"
        Me.FNHSysFixedAssetId_lbl.Text = "Asset Code :"
        '
        'FTAssetModelCode
        '
        Me.FTAssetModelCode.EnterMoveNextControl = True
        Me.FTAssetModelCode.Location = New System.Drawing.Point(137, 112)
        Me.FTAssetModelCode.Name = "FTAssetModelCode"
        Me.FTAssetModelCode.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FTAssetModelCode.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTAssetModelCode.Properties.Appearance.Options.UseBackColor = True
        Me.FTAssetModelCode.Properties.Appearance.Options.UseForeColor = True
        Me.FTAssetModelCode.Properties.Appearance.Options.UseTextOptions = True
        Me.FTAssetModelCode.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTAssetModelCode.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FTAssetModelCode.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FTAssetModelCode.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FTAssetModelCode.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FTAssetModelCode.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FTAssetModelCode.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FTAssetModelCode.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FTAssetModelCode.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FTAssetModelCode.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FTAssetModelCode.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FTAssetModelCode.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FTAssetModelCode.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FTAssetModelCode.Properties.ReadOnly = True
        Me.FTAssetModelCode.Size = New System.Drawing.Size(109, 20)
        Me.FTAssetModelCode.TabIndex = 286
        Me.FTAssetModelCode.TabStop = False
        Me.FTAssetModelCode.Tag = "2|"
        '
        'FNHSysFixedAssetId_None
        '
        Me.FNHSysFixedAssetId_None.EditValue = ""
        Me.FNHSysFixedAssetId_None.Location = New System.Drawing.Point(137, 50)
        Me.FNHSysFixedAssetId_None.Name = "FNHSysFixedAssetId_None"
        Me.FNHSysFixedAssetId_None.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysFixedAssetId_None.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysFixedAssetId_None.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNHSysFixedAssetId_None.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNHSysFixedAssetId_None.Properties.ReadOnly = True
        Me.FNHSysFixedAssetId_None.Size = New System.Drawing.Size(682, 56)
        Me.FNHSysFixedAssetId_None.TabIndex = 289
        Me.FNHSysFixedAssetId_None.Tag = "2|"
        Me.FNHSysFixedAssetId_None.UseOptimizedRendering = True
        '
        'ocmcancel
        '
        Me.ocmcancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmcancel.Location = New System.Drawing.Point(696, 1)
        Me.ocmcancel.Name = "ocmcancel"
        Me.ocmcancel.Size = New System.Drawing.Size(160, 20)
        Me.ocmcancel.TabIndex = 101
        Me.ocmcancel.TabStop = False
        Me.ocmcancel.Tag = "2|"
        Me.ocmcancel.Text = "CANCEL"
        '
        'ocmreceive
        '
        Me.ocmreceive.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmreceive.Location = New System.Drawing.Point(528, 1)
        Me.ocmreceive.Name = "ocmreceive"
        Me.ocmreceive.Size = New System.Drawing.Size(160, 20)
        Me.ocmreceive.TabIndex = 100
        Me.ocmreceive.TabStop = False
        Me.ocmreceive.Tag = "2|"
        Me.ocmreceive.Text = "SAVE"
        '
        'wReceiveMultiple
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(866, 385)
        Me.ControlBox = False
        Me.Controls.Add(Me.ogb)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "wReceiveMultiple"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "wReceive Multiple"
        CType(Me.ogcrcv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvrcv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ReposFTFabricFrontSize, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ResFNQuantity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ResFNRcvQty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCalcEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ResFNPOBalQty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ResFNRcvHisQty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ResFTStateSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogb, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogb.ResumeLayout(False)
        CType(Me.CFNQuantity.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysFixedAssetId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTAssetModelCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysFixedAssetId_None.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogcrcv As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvrcv As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents FNHSysRawMatId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTRawMatCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTMatDesc As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTRawMatColorCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTRawMatSizeCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNTSysUnitId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTUnitCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNPrice As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNDisPer As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNDisAmt As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNNetPrice As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ResFNPOBalQty As DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit
    Friend WithEvents FNTCQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNPOQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNOrderRcvQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemCalcEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit
    Friend WithEvents ogb As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ocmcancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmreceive As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ResFNQuantity As DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit
    Friend WithEvents ResFNRcvHisQty As DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit
    Friend WithEvents ResFNRcvQty As DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit
    Friend WithEvents ResFTStateSelect As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents FTFabricFrontSize As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ReposFTFabricFrontSize As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents FTProductNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FDShipDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNUsedQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNNetAmt As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNRTSQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNConvRatio As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNPricePerStock As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNHSysFixedAssetId As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FNHSysFixedAssetId_None_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTAssetModelCode_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysFixedAssetId_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTAssetModelCode As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FNHSysFixedAssetId_None As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents CFNQuantity As DevExpress.XtraEditors.CalcEdit
    Friend WithEvents FNQuantity_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysUnitIdStock As DevExpress.XtraGrid.Columns.GridColumn
End Class

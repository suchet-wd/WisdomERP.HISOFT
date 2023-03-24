<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wReceiveMultiple
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
        Me.FTOrderNo = New DevExpress.XtraGrid.Columns.GridColumn()
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
        Me.TFNHSysRawMatId = New DevExpress.XtraEditors.TextEdit()
        Me.FTFabricFrontSize_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.TFTFabricFrontSize = New DevExpress.XtraEditors.TextEdit()
        Me.FNTSysMatId_None_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTRawMatSizeCode_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTRawMatColorCode_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysRawMatId_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.TFTRawMatSizeCode = New DevExpress.XtraEditors.TextEdit()
        Me.TFTRawMatColorCode = New DevExpress.XtraEditors.TextEdit()
        Me.FNHSysRawMatId_None = New DevExpress.XtraEditors.MemoEdit()
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
        CType(Me.TFNHSysRawMatId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TFTFabricFrontSize.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TFTRawMatSizeCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TFTRawMatColorCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysRawMatId_None.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ogcrcv
        '
        Me.ogcrcv.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogcrcv.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcrcv.Location = New System.Drawing.Point(6, 165)
        Me.ogcrcv.MainView = Me.ogvrcv
        Me.ogcrcv.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcrcv.Name = "ogcrcv"
        Me.ogcrcv.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCalcEdit1, Me.ResFNPOBalQty, Me.ResFNQuantity, Me.ResFNRcvHisQty, Me.ResFNRcvQty, Me.ResFTStateSelect, Me.ReposFTFabricFrontSize})
        Me.ogcrcv.Size = New System.Drawing.Size(995, 297)
        Me.ogcrcv.TabIndex = 3
        Me.ogcrcv.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvrcv})
        '
        'ogvrcv
        '
        Me.ogvrcv.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.FNHSysRawMatId, Me.FTRawMatCode, Me.FTMatDesc, Me.FTRawMatColorCode, Me.FTRawMatSizeCode, Me.FTFabricFrontSize, Me.FNTSysUnitId, Me.FTUnitCode, Me.FNPrice, Me.FNDisPer, Me.FNDisAmt, Me.FNNetPrice, Me.FNQuantity, Me.FTOrderNo, Me.FDShipDate, Me.FNUsedQuantity, Me.FNNetAmt, Me.FNPOQuantity, Me.FNTCQuantity, Me.FNRTSQuantity, Me.FNOrderRcvQuantity, Me.FNConvRatio, Me.FNPricePerStock, Me.FNHSysUnitIdStock})
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
        'FTOrderNo
        '
        Me.FTOrderNo.Caption = "FO No."
        Me.FTOrderNo.FieldName = "FTOrderNo"
        Me.FTOrderNo.Name = "FTOrderNo"
        Me.FTOrderNo.OptionsColumn.AllowEdit = False
        Me.FTOrderNo.OptionsColumn.ReadOnly = True
        Me.FTOrderNo.Visible = True
        Me.FTOrderNo.VisibleIndex = 0
        Me.FTOrderNo.Width = 112
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
        Me.ogb.Controls.Add(Me.TFNHSysRawMatId)
        Me.ogb.Controls.Add(Me.FTFabricFrontSize_lbl)
        Me.ogb.Controls.Add(Me.TFTFabricFrontSize)
        Me.ogb.Controls.Add(Me.FNTSysMatId_None_lbl)
        Me.ogb.Controls.Add(Me.FTRawMatSizeCode_lbl)
        Me.ogb.Controls.Add(Me.FTRawMatColorCode_lbl)
        Me.ogb.Controls.Add(Me.FNHSysRawMatId_lbl)
        Me.ogb.Controls.Add(Me.TFTRawMatSizeCode)
        Me.ogb.Controls.Add(Me.TFTRawMatColorCode)
        Me.ogb.Controls.Add(Me.FNHSysRawMatId_None)
        Me.ogb.Controls.Add(Me.ocmcancel)
        Me.ogb.Controls.Add(Me.ocmreceive)
        Me.ogb.Controls.Add(Me.ogcrcv)
        Me.ogb.Location = New System.Drawing.Point(2, 2)
        Me.ogb.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogb.Name = "ogb"
        Me.ogb.Size = New System.Drawing.Size(1007, 468)
        Me.ogb.TabIndex = 6
        Me.ogb.Tag = "2|"
        Me.ogb.Text = "Multiple Detail"
        '
        'CFNQuantity
        '
        Me.CFNQuantity.EnterMoveNextControl = True
        Me.CFNQuantity.Location = New System.Drawing.Point(787, 133)
        Me.CFNQuantity.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
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
        Me.CFNQuantity.Size = New System.Drawing.Size(168, 22)
        Me.CFNQuantity.TabIndex = 296
        Me.CFNQuantity.Tag = "2|"
        '
        'FNQuantity_lbl
        '
        Me.FNQuantity_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNQuantity_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNQuantity_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNQuantity_lbl.Location = New System.Drawing.Point(673, 132)
        Me.FNQuantity_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNQuantity_lbl.Name = "FNQuantity_lbl"
        Me.FNQuantity_lbl.Size = New System.Drawing.Size(113, 23)
        Me.FNQuantity_lbl.TabIndex = 297
        Me.FNQuantity_lbl.Tag = "2|"
        Me.FNQuantity_lbl.Text = "Quantity :"
        '
        'TFNHSysRawMatId
        '
        Me.TFNHSysRawMatId.EnterMoveNextControl = True
        Me.TFNHSysRawMatId.Location = New System.Drawing.Point(160, 32)
        Me.TFNHSysRawMatId.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.TFNHSysRawMatId.Name = "TFNHSysRawMatId"
        Me.TFNHSysRawMatId.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.TFNHSysRawMatId.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.TFNHSysRawMatId.Properties.Appearance.Options.UseBackColor = True
        Me.TFNHSysRawMatId.Properties.Appearance.Options.UseForeColor = True
        Me.TFNHSysRawMatId.Properties.Appearance.Options.UseTextOptions = True
        Me.TFNHSysRawMatId.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.TFNHSysRawMatId.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.TFNHSysRawMatId.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.TFNHSysRawMatId.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.TFNHSysRawMatId.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.TFNHSysRawMatId.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.TFNHSysRawMatId.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.TFNHSysRawMatId.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.TFNHSysRawMatId.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.TFNHSysRawMatId.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.TFNHSysRawMatId.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.TFNHSysRawMatId.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.TFNHSysRawMatId.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.TFNHSysRawMatId.Properties.ReadOnly = True
        Me.TFNHSysRawMatId.Size = New System.Drawing.Size(176, 22)
        Me.TFNHSysRawMatId.TabIndex = 295
        Me.TFNHSysRawMatId.TabStop = False
        Me.TFNHSysRawMatId.Tag = "2|"
        '
        'FTFabricFrontSize_lbl
        '
        Me.FTFabricFrontSize_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTFabricFrontSize_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTFabricFrontSize_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTFabricFrontSize_lbl.Location = New System.Drawing.Point(16, 133)
        Me.FTFabricFrontSize_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTFabricFrontSize_lbl.Name = "FTFabricFrontSize_lbl"
        Me.FTFabricFrontSize_lbl.Size = New System.Drawing.Size(141, 23)
        Me.FTFabricFrontSize_lbl.TabIndex = 294
        Me.FTFabricFrontSize_lbl.Tag = "2|"
        Me.FTFabricFrontSize_lbl.Text = "Fabric Front Size :"
        '
        'TFTFabricFrontSize
        '
        Me.TFTFabricFrontSize.EnterMoveNextControl = True
        Me.TFTFabricFrontSize.Location = New System.Drawing.Point(160, 133)
        Me.TFTFabricFrontSize.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.TFTFabricFrontSize.Name = "TFTFabricFrontSize"
        Me.TFTFabricFrontSize.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.TFTFabricFrontSize.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.TFTFabricFrontSize.Properties.Appearance.Options.UseBackColor = True
        Me.TFTFabricFrontSize.Properties.Appearance.Options.UseForeColor = True
        Me.TFTFabricFrontSize.Properties.Appearance.Options.UseTextOptions = True
        Me.TFTFabricFrontSize.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.TFTFabricFrontSize.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.TFTFabricFrontSize.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.TFTFabricFrontSize.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.TFTFabricFrontSize.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.TFTFabricFrontSize.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.TFTFabricFrontSize.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.TFTFabricFrontSize.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.TFTFabricFrontSize.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.TFTFabricFrontSize.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.TFTFabricFrontSize.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.TFTFabricFrontSize.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.TFTFabricFrontSize.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.TFTFabricFrontSize.Properties.MaxLength = 50
        Me.TFTFabricFrontSize.Properties.ReadOnly = True
        Me.TFTFabricFrontSize.Size = New System.Drawing.Size(336, 22)
        Me.TFTFabricFrontSize.TabIndex = 287
        Me.TFTFabricFrontSize.TabStop = False
        Me.TFTFabricFrontSize.Tag = "2|"
        '
        'FNTSysMatId_None_lbl
        '
        Me.FNTSysMatId_None_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FNTSysMatId_None_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNTSysMatId_None_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNTSysMatId_None_lbl.Location = New System.Drawing.Point(14, 62)
        Me.FNTSysMatId_None_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNTSysMatId_None_lbl.Name = "FNTSysMatId_None_lbl"
        Me.FNTSysMatId_None_lbl.Size = New System.Drawing.Size(141, 23)
        Me.FNTSysMatId_None_lbl.TabIndex = 293
        Me.FNTSysMatId_None_lbl.Tag = "2|"
        Me.FNTSysMatId_None_lbl.Text = "Material Code :"
        '
        'FTRawMatSizeCode_lbl
        '
        Me.FTRawMatSizeCode_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTRawMatSizeCode_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTRawMatSizeCode_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTRawMatSizeCode_lbl.Location = New System.Drawing.Point(693, 33)
        Me.FTRawMatSizeCode_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTRawMatSizeCode_lbl.Name = "FTRawMatSizeCode_lbl"
        Me.FTRawMatSizeCode_lbl.Size = New System.Drawing.Size(125, 23)
        Me.FTRawMatSizeCode_lbl.TabIndex = 292
        Me.FTRawMatSizeCode_lbl.Tag = "2|"
        Me.FTRawMatSizeCode_lbl.Text = "Size Code :"
        '
        'FTRawMatColorCode_lbl
        '
        Me.FTRawMatColorCode_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTRawMatColorCode_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTRawMatColorCode_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTRawMatColorCode_lbl.Location = New System.Drawing.Point(421, 34)
        Me.FTRawMatColorCode_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTRawMatColorCode_lbl.Name = "FTRawMatColorCode_lbl"
        Me.FTRawMatColorCode_lbl.Size = New System.Drawing.Size(125, 23)
        Me.FTRawMatColorCode_lbl.TabIndex = 291
        Me.FTRawMatColorCode_lbl.Tag = "2|"
        Me.FTRawMatColorCode_lbl.Text = "Color Code :"
        '
        'FNHSysRawMatId_lbl
        '
        Me.FNHSysRawMatId_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysRawMatId_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysRawMatId_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysRawMatId_lbl.Location = New System.Drawing.Point(14, 34)
        Me.FNHSysRawMatId_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysRawMatId_lbl.Name = "FNHSysRawMatId_lbl"
        Me.FNHSysRawMatId_lbl.Size = New System.Drawing.Size(141, 23)
        Me.FNHSysRawMatId_lbl.TabIndex = 290
        Me.FNHSysRawMatId_lbl.Tag = "2|"
        Me.FNHSysRawMatId_lbl.Text = "Material Code :"
        '
        'TFTRawMatSizeCode
        '
        Me.TFTRawMatSizeCode.EnterMoveNextControl = True
        Me.TFTRawMatSizeCode.Location = New System.Drawing.Point(825, 32)
        Me.TFTRawMatSizeCode.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.TFTRawMatSizeCode.Name = "TFTRawMatSizeCode"
        Me.TFTRawMatSizeCode.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.TFTRawMatSizeCode.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.TFTRawMatSizeCode.Properties.Appearance.Options.UseBackColor = True
        Me.TFTRawMatSizeCode.Properties.Appearance.Options.UseForeColor = True
        Me.TFTRawMatSizeCode.Properties.Appearance.Options.UseTextOptions = True
        Me.TFTRawMatSizeCode.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.TFTRawMatSizeCode.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.TFTRawMatSizeCode.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.TFTRawMatSizeCode.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.TFTRawMatSizeCode.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.TFTRawMatSizeCode.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.TFTRawMatSizeCode.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.TFTRawMatSizeCode.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.TFTRawMatSizeCode.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.TFTRawMatSizeCode.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.TFTRawMatSizeCode.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.TFTRawMatSizeCode.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.TFTRawMatSizeCode.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.TFTRawMatSizeCode.Properties.ReadOnly = True
        Me.TFTRawMatSizeCode.Size = New System.Drawing.Size(131, 22)
        Me.TFTRawMatSizeCode.TabIndex = 288
        Me.TFTRawMatSizeCode.TabStop = False
        Me.TFTRawMatSizeCode.Tag = "2|"
        '
        'TFTRawMatColorCode
        '
        Me.TFTRawMatColorCode.EnterMoveNextControl = True
        Me.TFTRawMatColorCode.Location = New System.Drawing.Point(548, 33)
        Me.TFTRawMatColorCode.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.TFTRawMatColorCode.Name = "TFTRawMatColorCode"
        Me.TFTRawMatColorCode.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.TFTRawMatColorCode.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.TFTRawMatColorCode.Properties.Appearance.Options.UseBackColor = True
        Me.TFTRawMatColorCode.Properties.Appearance.Options.UseForeColor = True
        Me.TFTRawMatColorCode.Properties.Appearance.Options.UseTextOptions = True
        Me.TFTRawMatColorCode.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.TFTRawMatColorCode.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.TFTRawMatColorCode.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.TFTRawMatColorCode.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.TFTRawMatColorCode.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.TFTRawMatColorCode.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.TFTRawMatColorCode.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.TFTRawMatColorCode.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.TFTRawMatColorCode.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.TFTRawMatColorCode.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.TFTRawMatColorCode.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.TFTRawMatColorCode.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.TFTRawMatColorCode.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.TFTRawMatColorCode.Properties.ReadOnly = True
        Me.TFTRawMatColorCode.Size = New System.Drawing.Size(127, 22)
        Me.TFTRawMatColorCode.TabIndex = 286
        Me.TFTRawMatColorCode.TabStop = False
        Me.TFTRawMatColorCode.Tag = "2|"
        '
        'FNHSysRawMatId_None
        '
        Me.FNHSysRawMatId_None.EditValue = ""
        Me.FNHSysRawMatId_None.Location = New System.Drawing.Point(160, 62)
        Me.FNHSysRawMatId_None.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysRawMatId_None.Name = "FNHSysRawMatId_None"
        Me.FNHSysRawMatId_None.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysRawMatId_None.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysRawMatId_None.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNHSysRawMatId_None.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNHSysRawMatId_None.Properties.ReadOnly = True
        Me.FNHSysRawMatId_None.Size = New System.Drawing.Size(796, 69)
        Me.FNHSysRawMatId_None.TabIndex = 289
        Me.FNHSysRawMatId_None.Tag = "2|"
        Me.FNHSysRawMatId_None.UseOptimizedRendering = True
        '
        'ocmcancel
        '
        Me.ocmcancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmcancel.Location = New System.Drawing.Point(812, 1)
        Me.ocmcancel.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmcancel.Name = "ocmcancel"
        Me.ocmcancel.Size = New System.Drawing.Size(187, 25)
        Me.ocmcancel.TabIndex = 101
        Me.ocmcancel.TabStop = False
        Me.ocmcancel.Tag = "2|"
        Me.ocmcancel.Text = "CANCEL"
        '
        'ocmreceive
        '
        Me.ocmreceive.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmreceive.Location = New System.Drawing.Point(616, 1)
        Me.ocmreceive.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmreceive.Name = "ocmreceive"
        Me.ocmreceive.Size = New System.Drawing.Size(187, 25)
        Me.ocmreceive.TabIndex = 100
        Me.ocmreceive.TabStop = False
        Me.ocmreceive.Tag = "2|"
        Me.ocmreceive.Text = "SAVE"
        '
        'wReceiveMultiple
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1010, 474)
        Me.ControlBox = False
        Me.Controls.Add(Me.ogb)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
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
        CType(Me.TFNHSysRawMatId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TFTFabricFrontSize.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TFTRawMatSizeCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TFTRawMatColorCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysRawMatId_None.Properties, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents FTOrderNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FDShipDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNUsedQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNNetAmt As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNRTSQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNConvRatio As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNPricePerStock As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents TFNHSysRawMatId As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FTFabricFrontSize_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents TFTFabricFrontSize As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FNTSysMatId_None_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTRawMatSizeCode_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTRawMatColorCode_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysRawMatId_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents TFTRawMatSizeCode As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TFTRawMatColorCode As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FNHSysRawMatId_None As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents CFNQuantity As DevExpress.XtraEditors.CalcEdit
    Friend WithEvents FNQuantity_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysUnitIdStock As DevExpress.XtraGrid.Columns.GridColumn
End Class

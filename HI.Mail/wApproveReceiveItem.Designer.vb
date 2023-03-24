<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wApproveReceiveItem
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
        Me.ogcrcv = New DevExpress.XtraGrid.GridControl()
        Me.ogvrcv = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.FTStateSelect = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ResFTStateSelect = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.FNHSysRawMatId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTRawMatCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTMatDesc = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTRawMatColorCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTRawMatSizeCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTFabricFrontSize = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ReposFTFabricFrontSize = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.FTUnitCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNRcvHisQty = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNPOBalQty = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNRcvQtyOver = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNTotalRcvQty = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNRcvQty = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ResFNRcvQty = New DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit()
        Me.CFTStateApprove = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNHSysMailAppId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTReceiveBy = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTReceiveNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTPurchaseNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCalcEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit()
        Me.ResFNPOBalQty = New DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit()
        Me.ResFNQuantity = New DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit()
        Me.ResFNRcvHisQty = New DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit()
        Me.ogb = New DevExpress.XtraEditors.GroupControl()
        Me.ocmreject = New DevExpress.XtraEditors.SimpleButton()
        Me.FTSelectAll = New DevExpress.XtraEditors.CheckEdit()
        Me.ocmcancel = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmapprove = New DevExpress.XtraEditors.SimpleButton()
        Me.CFNHSysWHId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFTStateImport = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.ogcrcv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvrcv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ResFTStateSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ReposFTFabricFrontSize, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ResFNRcvQty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCalcEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ResFNPOBalQty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ResFNQuantity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ResFNRcvHisQty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogb, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogb.SuspendLayout()
        CType(Me.FTSelectAll.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ogcrcv
        '
        Me.ogcrcv.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogcrcv.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcrcv.Location = New System.Drawing.Point(6, 30)
        Me.ogcrcv.MainView = Me.ogvrcv
        Me.ogcrcv.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcrcv.Name = "ogcrcv"
        Me.ogcrcv.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCalcEdit1, Me.ResFNPOBalQty, Me.ResFNQuantity, Me.ResFNRcvHisQty, Me.ResFNRcvQty, Me.ResFTStateSelect, Me.ReposFTFabricFrontSize})
        Me.ogcrcv.Size = New System.Drawing.Size(1231, 459)
        Me.ogcrcv.TabIndex = 3
        Me.ogcrcv.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvrcv})
        '
        'ogvrcv
        '
        Me.ogvrcv.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.FTStateSelect, Me.FNHSysRawMatId, Me.FTRawMatCode, Me.FTMatDesc, Me.FTRawMatColorCode, Me.FTRawMatSizeCode, Me.FTFabricFrontSize, Me.FTUnitCode, Me.FNQuantity, Me.FNRcvHisQty, Me.FNPOBalQty, Me.FNRcvQtyOver, Me.FNTotalRcvQty, Me.FNRcvQty, Me.CFTStateApprove, Me.FNHSysMailAppId, Me.FTReceiveBy, Me.FTReceiveNo, Me.FTPurchaseNo, Me.CFNHSysWHId, Me.CFTStateImport})
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
        'FTStateSelect
        '
        Me.FTStateSelect.AppearanceCell.Options.UseTextOptions = True
        Me.FTStateSelect.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTStateSelect.AppearanceHeader.Options.UseTextOptions = True
        Me.FTStateSelect.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTStateSelect.Caption = " "
        Me.FTStateSelect.ColumnEdit = Me.ResFTStateSelect
        Me.FTStateSelect.FieldName = "FTSelect"
        Me.FTStateSelect.Name = "FTStateSelect"
        Me.FTStateSelect.OptionsColumn.AllowMove = False
        Me.FTStateSelect.OptionsColumn.ShowCaption = False
        Me.FTStateSelect.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        Me.FTStateSelect.Visible = True
        Me.FTStateSelect.VisibleIndex = 0
        Me.FTStateSelect.Width = 30
        '
        'ResFTStateSelect
        '
        Me.ResFTStateSelect.AutoHeight = False
        Me.ResFTStateSelect.Caption = "Check"
        Me.ResFTStateSelect.Name = "ResFTStateSelect"
        Me.ResFTStateSelect.ValueChecked = "1"
        Me.ResFTStateSelect.ValueUnchecked = "0"
        '
        'FNHSysRawMatId
        '
        Me.FNHSysRawMatId.Caption = "FNHSysRawMatId"
        Me.FNHSysRawMatId.FieldName = "FNHSysRawMatId"
        Me.FNHSysRawMatId.Name = "FNHSysRawMatId"
        '
        'FTRawMatCode
        '
        Me.FTRawMatCode.AppearanceHeader.Options.UseTextOptions = True
        Me.FTRawMatCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTRawMatCode.Caption = "FTRawMatCode"
        Me.FTRawMatCode.FieldName = "FTRawMatCode"
        Me.FTRawMatCode.Name = "FTRawMatCode"
        Me.FTRawMatCode.OptionsColumn.AllowEdit = False
        Me.FTRawMatCode.OptionsColumn.AllowMove = False
        Me.FTRawMatCode.OptionsColumn.ReadOnly = True
        Me.FTRawMatCode.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        Me.FTRawMatCode.Visible = True
        Me.FTRawMatCode.VisibleIndex = 1
        Me.FTRawMatCode.Width = 96
        '
        'FTMatDesc
        '
        Me.FTMatDesc.AppearanceHeader.Options.UseTextOptions = True
        Me.FTMatDesc.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTMatDesc.Caption = "FTMatDesc"
        Me.FTMatDesc.FieldName = "FTRawMatName"
        Me.FTMatDesc.Name = "FTMatDesc"
        Me.FTMatDesc.OptionsColumn.AllowEdit = False
        Me.FTMatDesc.OptionsColumn.AllowMove = False
        Me.FTMatDesc.OptionsColumn.ReadOnly = True
        Me.FTMatDesc.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        Me.FTMatDesc.Visible = True
        Me.FTMatDesc.VisibleIndex = 2
        Me.FTMatDesc.Width = 141
        '
        'FTRawMatColorCode
        '
        Me.FTRawMatColorCode.AppearanceHeader.Options.UseTextOptions = True
        Me.FTRawMatColorCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTRawMatColorCode.Caption = "FTRawMatColorCode"
        Me.FTRawMatColorCode.FieldName = "FTRawMatColorCode"
        Me.FTRawMatColorCode.Name = "FTRawMatColorCode"
        Me.FTRawMatColorCode.OptionsColumn.AllowEdit = False
        Me.FTRawMatColorCode.OptionsColumn.AllowMove = False
        Me.FTRawMatColorCode.OptionsColumn.ReadOnly = True
        Me.FTRawMatColorCode.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        Me.FTRawMatColorCode.Visible = True
        Me.FTRawMatColorCode.VisibleIndex = 3
        Me.FTRawMatColorCode.Width = 77
        '
        'FTRawMatSizeCode
        '
        Me.FTRawMatSizeCode.AppearanceHeader.Options.UseTextOptions = True
        Me.FTRawMatSizeCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTRawMatSizeCode.Caption = "FTRawMatSizeCode"
        Me.FTRawMatSizeCode.FieldName = "FTRawMatSizeCode"
        Me.FTRawMatSizeCode.Name = "FTRawMatSizeCode"
        Me.FTRawMatSizeCode.OptionsColumn.AllowEdit = False
        Me.FTRawMatSizeCode.OptionsColumn.AllowMove = False
        Me.FTRawMatSizeCode.OptionsColumn.ReadOnly = True
        Me.FTRawMatSizeCode.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        Me.FTRawMatSizeCode.Visible = True
        Me.FTRawMatSizeCode.VisibleIndex = 4
        '
        'FTFabricFrontSize
        '
        Me.FTFabricFrontSize.AppearanceHeader.Options.UseTextOptions = True
        Me.FTFabricFrontSize.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTFabricFrontSize.Caption = "FTFabricFrontSize"
        Me.FTFabricFrontSize.ColumnEdit = Me.ReposFTFabricFrontSize
        Me.FTFabricFrontSize.FieldName = "FTFabricFrontSize"
        Me.FTFabricFrontSize.Name = "FTFabricFrontSize"
        Me.FTFabricFrontSize.OptionsColumn.AllowEdit = False
        Me.FTFabricFrontSize.OptionsColumn.AllowMove = False
        Me.FTFabricFrontSize.OptionsColumn.ReadOnly = True
        Me.FTFabricFrontSize.Visible = True
        Me.FTFabricFrontSize.VisibleIndex = 5
        Me.FTFabricFrontSize.Width = 87
        '
        'ReposFTFabricFrontSize
        '
        Me.ReposFTFabricFrontSize.AutoHeight = False
        Me.ReposFTFabricFrontSize.MaxLength = 50
        Me.ReposFTFabricFrontSize.Name = "ReposFTFabricFrontSize"
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
        Me.FTUnitCode.OptionsColumn.AllowMove = False
        Me.FTUnitCode.OptionsColumn.ReadOnly = True
        Me.FTUnitCode.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        Me.FTUnitCode.Visible = True
        Me.FTUnitCode.VisibleIndex = 6
        Me.FTUnitCode.Width = 60
        '
        'FNQuantity
        '
        Me.FNQuantity.AppearanceCell.Options.UseTextOptions = True
        Me.FNQuantity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNQuantity.AppearanceHeader.Options.UseTextOptions = True
        Me.FNQuantity.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FNQuantity.Caption = "จำนวนสั่งซื้อ"
        Me.FNQuantity.DisplayFormat.FormatString = "{0:n4}"
        Me.FNQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNQuantity.FieldName = "FNPOQuantity"
        Me.FNQuantity.Name = "FNQuantity"
        Me.FNQuantity.OptionsColumn.AllowEdit = False
        Me.FNQuantity.OptionsColumn.AllowMove = False
        Me.FNQuantity.OptionsColumn.ReadOnly = True
        Me.FNQuantity.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        Me.FNQuantity.Visible = True
        Me.FNQuantity.VisibleIndex = 7
        Me.FNQuantity.Width = 82
        '
        'FNRcvHisQty
        '
        Me.FNRcvHisQty.AppearanceCell.Options.UseTextOptions = True
        Me.FNRcvHisQty.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNRcvHisQty.AppearanceHeader.Options.UseTextOptions = True
        Me.FNRcvHisQty.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FNRcvHisQty.Caption = "จำนวนรับก่อนหน้า"
        Me.FNRcvHisQty.DisplayFormat.FormatString = "{0:n4}"
        Me.FNRcvHisQty.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNRcvHisQty.FieldName = "FNRcvHisQuantity"
        Me.FNRcvHisQty.Name = "FNRcvHisQty"
        Me.FNRcvHisQty.OptionsColumn.AllowEdit = False
        Me.FNRcvHisQty.OptionsColumn.AllowMove = False
        Me.FNRcvHisQty.OptionsColumn.ReadOnly = True
        Me.FNRcvHisQty.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        Me.FNRcvHisQty.Visible = True
        Me.FNRcvHisQty.VisibleIndex = 8
        Me.FNRcvHisQty.Width = 88
        '
        'FNPOBalQty
        '
        Me.FNPOBalQty.AppearanceCell.Options.UseTextOptions = True
        Me.FNPOBalQty.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNPOBalQty.AppearanceHeader.Options.UseTextOptions = True
        Me.FNPOBalQty.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FNPOBalQty.Caption = "รับได้"
        Me.FNPOBalQty.DisplayFormat.FormatString = "{0:n4}"
        Me.FNPOBalQty.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNPOBalQty.FieldName = "FNRcvQtyPass"
        Me.FNPOBalQty.Name = "FNPOBalQty"
        Me.FNPOBalQty.OptionsColumn.AllowEdit = False
        Me.FNPOBalQty.OptionsColumn.AllowMove = False
        Me.FNPOBalQty.OptionsColumn.ReadOnly = True
        Me.FNPOBalQty.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        Me.FNPOBalQty.Visible = True
        Me.FNPOBalQty.VisibleIndex = 9
        Me.FNPOBalQty.Width = 80
        '
        'FNRcvQtyOver
        '
        Me.FNRcvQtyOver.AppearanceCell.Options.UseTextOptions = True
        Me.FNRcvQtyOver.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNRcvQtyOver.Caption = "รับเกิน"
        Me.FNRcvQtyOver.DisplayFormat.FormatString = "{0:n4}"
        Me.FNRcvQtyOver.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNRcvQtyOver.FieldName = "FNRcvQtyOver"
        Me.FNRcvQtyOver.Name = "FNRcvQtyOver"
        Me.FNRcvQtyOver.OptionsColumn.AllowEdit = False
        Me.FNRcvQtyOver.OptionsColumn.AllowMove = False
        Me.FNRcvQtyOver.OptionsColumn.ReadOnly = True
        Me.FNRcvQtyOver.Visible = True
        Me.FNRcvQtyOver.VisibleIndex = 10
        Me.FNRcvQtyOver.Width = 76
        '
        'FNTotalRcvQty
        '
        Me.FNTotalRcvQty.AppearanceCell.Options.UseTextOptions = True
        Me.FNTotalRcvQty.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNTotalRcvQty.Caption = "รวมรับ"
        Me.FNTotalRcvQty.DisplayFormat.FormatString = "{0:n4}"
        Me.FNTotalRcvQty.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNTotalRcvQty.FieldName = "FNTotalRcvQty"
        Me.FNTotalRcvQty.Name = "FNTotalRcvQty"
        Me.FNTotalRcvQty.OptionsColumn.AllowEdit = False
        Me.FNTotalRcvQty.OptionsColumn.AllowMove = False
        Me.FNTotalRcvQty.OptionsColumn.ReadOnly = True
        Me.FNTotalRcvQty.Visible = True
        Me.FNTotalRcvQty.VisibleIndex = 11
        Me.FNTotalRcvQty.Width = 79
        '
        'FNRcvQty
        '
        Me.FNRcvQty.AppearanceCell.Options.UseTextOptions = True
        Me.FNRcvQty.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNRcvQty.AppearanceHeader.Options.UseTextOptions = True
        Me.FNRcvQty.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FNRcvQty.Caption = "อนุมัติ"
        Me.FNRcvQty.ColumnEdit = Me.ResFNRcvQty
        Me.FNRcvQty.DisplayFormat.FormatString = "{0:n4}"
        Me.FNRcvQty.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNRcvQty.FieldName = "FNApproveRcvQty"
        Me.FNRcvQty.Name = "FNRcvQty"
        Me.FNRcvQty.OptionsColumn.AllowMove = False
        Me.FNRcvQty.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        Me.FNRcvQty.Visible = True
        Me.FNRcvQty.VisibleIndex = 12
        Me.FNRcvQty.Width = 80
        '
        'ResFNRcvQty
        '
        Me.ResFNRcvQty.AutoHeight = False
        Me.ResFNRcvQty.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ResFNRcvQty.Name = "ResFNRcvQty"
        '
        'CFTStateApprove
        '
        Me.CFTStateApprove.Caption = "FTStateApprove"
        Me.CFTStateApprove.FieldName = "FTStateApprove"
        Me.CFTStateApprove.Name = "CFTStateApprove"
        '
        'FNHSysMailAppId
        '
        Me.FNHSysMailAppId.Caption = "FNHSysMailAppId"
        Me.FNHSysMailAppId.FieldName = "FNHSysMailAppId"
        Me.FNHSysMailAppId.Name = "FNHSysMailAppId"
        '
        'FTReceiveBy
        '
        Me.FTReceiveBy.Caption = "FTReceiveBy"
        Me.FTReceiveBy.FieldName = "FTReceiveBy"
        Me.FTReceiveBy.Name = "FTReceiveBy"
        '
        'FTReceiveNo
        '
        Me.FTReceiveNo.Caption = "FTReceiveNo"
        Me.FTReceiveNo.FieldName = "FTReceiveNo"
        Me.FTReceiveNo.Name = "FTReceiveNo"
        '
        'FTPurchaseNo
        '
        Me.FTPurchaseNo.Caption = "FTPurchaseNo"
        Me.FTPurchaseNo.FieldName = "FTPurchaseNo"
        Me.FTPurchaseNo.Name = "FTPurchaseNo"
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
        'ResFNQuantity
        '
        Me.ResFNQuantity.AutoHeight = False
        Me.ResFNQuantity.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ResFNQuantity.Name = "ResFNQuantity"
        '
        'ResFNRcvHisQty
        '
        Me.ResFNRcvHisQty.AutoHeight = False
        Me.ResFNRcvHisQty.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ResFNRcvHisQty.Name = "ResFNRcvHisQty"
        '
        'ogb
        '
        Me.ogb.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogb.Controls.Add(Me.ocmreject)
        Me.ogb.Controls.Add(Me.FTSelectAll)
        Me.ogb.Controls.Add(Me.ocmcancel)
        Me.ogb.Controls.Add(Me.ocmapprove)
        Me.ogb.Controls.Add(Me.ogcrcv)
        Me.ogb.Location = New System.Drawing.Point(2, 2)
        Me.ogb.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogb.Name = "ogb"
        Me.ogb.Size = New System.Drawing.Size(1243, 495)
        Me.ogb.TabIndex = 6
        Me.ogb.Tag = "2|"
        Me.ogb.Text = "Purchase Detail"
        '
        'ocmreject
        '
        Me.ocmreject.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmreject.Location = New System.Drawing.Point(848, 0)
        Me.ocmreject.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmreject.Name = "ocmreject"
        Me.ocmreject.Size = New System.Drawing.Size(187, 25)
        Me.ocmreject.TabIndex = 103
        Me.ocmreject.TabStop = False
        Me.ocmreject.Tag = "2|"
        Me.ocmreject.Text = "Reject"
        '
        'FTSelectAll
        '
        Me.FTSelectAll.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FTSelectAll.Location = New System.Drawing.Point(397, 3)
        Me.FTSelectAll.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTSelectAll.Name = "FTSelectAll"
        Me.FTSelectAll.Properties.Appearance.Options.UseTextOptions = True
        Me.FTSelectAll.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTSelectAll.Properties.Caption = "Select All"
        Me.FTSelectAll.Properties.GlyphAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTSelectAll.Properties.ValueChecked = "1"
        Me.FTSelectAll.Properties.ValueUnchecked = "0"
        Me.FTSelectAll.Size = New System.Drawing.Size(176, 20)
        Me.FTSelectAll.TabIndex = 102
        Me.FTSelectAll.Tag = "2|"
        '
        'ocmcancel
        '
        Me.ocmcancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmcancel.Location = New System.Drawing.Point(1048, 1)
        Me.ocmcancel.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmcancel.Name = "ocmcancel"
        Me.ocmcancel.Size = New System.Drawing.Size(187, 25)
        Me.ocmcancel.TabIndex = 101
        Me.ocmcancel.TabStop = False
        Me.ocmcancel.Tag = "2|"
        Me.ocmcancel.Text = "CANCEL"
        '
        'ocmapprove
        '
        Me.ocmapprove.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmapprove.Location = New System.Drawing.Point(647, 1)
        Me.ocmapprove.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmapprove.Name = "ocmapprove"
        Me.ocmapprove.Size = New System.Drawing.Size(187, 25)
        Me.ocmapprove.TabIndex = 100
        Me.ocmapprove.TabStop = False
        Me.ocmapprove.Tag = "2|"
        Me.ocmapprove.Text = "Approve"
        '
        'CFNHSysWHId
        '
        Me.CFNHSysWHId.Caption = "FNHSysWHId"
        Me.CFNHSysWHId.FieldName = "FNHSysWHId"
        Me.CFNHSysWHId.Name = "CFNHSysWHId"
        Me.CFNHSysWHId.OptionsColumn.AllowEdit = False
        Me.CFNHSysWHId.OptionsColumn.ReadOnly = True
        '
        'CFTStateImport
        '
        Me.CFTStateImport.Caption = "FTStateImport"
        Me.CFTStateImport.FieldName = "FTStateImport"
        Me.CFTStateImport.Name = "CFTStateImport"
        '
        'wApproveReceiveItem
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1247, 501)
        Me.ControlBox = False
        Me.Controls.Add(Me.ogb)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "wApproveReceiveItem"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Approve Receive"
        CType(Me.ogcrcv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvrcv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ResFTStateSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ReposFTFabricFrontSize, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ResFNRcvQty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCalcEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ResFNPOBalQty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ResFNQuantity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ResFNRcvHisQty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogb, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogb.ResumeLayout(False)
        CType(Me.FTSelectAll.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogcrcv As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvrcv As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents FNHSysRawMatId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTRawMatCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTMatDesc As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTRawMatColorCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTRawMatSizeCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTUnitCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ResFNPOBalQty As DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit
    Friend WithEvents FNRcvHisQty As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNPOBalQty As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNRcvQty As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemCalcEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit
    Friend WithEvents ogb As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ocmcancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmapprove As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ResFNQuantity As DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit
    Friend WithEvents ResFNRcvHisQty As DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit
    Friend WithEvents ResFNRcvQty As DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit
    Friend WithEvents FTSelectAll As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents FTStateSelect As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ResFTStateSelect As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents FTFabricFrontSize As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ReposFTFabricFrontSize As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents ocmreject As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FNRcvQtyOver As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNTotalRcvQty As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFTStateApprove As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNHSysMailAppId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTReceiveBy As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTReceiveNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTPurchaseNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFNHSysWHId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFTStateImport As DevExpress.XtraGrid.Columns.GridColumn
End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wSMPReturnStockItem
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
        Me.ogcrcv = New DevExpress.XtraGrid.GridControl()
        Me.ogvrcv = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.CDataType = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTReceiveNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFTMatCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFTMatColorCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFTMatSizeCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTDescription = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNPrice = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFNAmount = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTPurchaseNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFNBalQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ResFNPOBalQty = New DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit()
        Me.CFNIssueQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ResFNRcvQty = New DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit()
        Me.CFTReceiveNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFNHSysWHId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCalcEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit()
        Me.ResFNQuantity = New DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit()
        Me.ResFNRcvHisQty = New DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit()
        Me.ResFTStateSelect = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.ReposFTFabricFrontSize = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.ogb = New DevExpress.XtraEditors.GroupControl()
        Me.ocmcancel = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmok = New DevExpress.XtraEditors.SimpleButton()
        Me.CFTUnitCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFNHSysUnitId = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.ogcrcv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvrcv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ResFNPOBalQty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ResFNRcvQty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCalcEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ResFNQuantity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ResFNRcvHisQty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ResFTStateSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ReposFTFabricFrontSize, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogb, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogb.SuspendLayout()
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
        Me.ogcrcv.Size = New System.Drawing.Size(1263, 459)
        Me.ogcrcv.TabIndex = 3
        Me.ogcrcv.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvrcv})
        '
        'ogvrcv
        '
        Me.ogvrcv.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.CDataType, Me.FTReceiveNo, Me.CFTMatCode, Me.CFTMatColorCode, Me.CFTMatSizeCode, Me.FTDescription, Me.FNPrice, Me.CFNAmount, Me.FTPurchaseNo, Me.CFTUnitCode, Me.CFNBalQuantity, Me.CFNIssueQuantity, Me.CFTReceiveNo, Me.CFNHSysWHId, Me.CFNHSysUnitId})
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
        'CDataType
        '
        Me.CDataType.AppearanceCell.Options.UseTextOptions = True
        Me.CDataType.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.CDataType.AppearanceHeader.Options.UseTextOptions = True
        Me.CDataType.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.CDataType.Caption = " DataType"
        Me.CDataType.FieldName = "DataType"
        Me.CDataType.Name = "CDataType"
        Me.CDataType.OptionsColumn.AllowEdit = False
        Me.CDataType.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.CDataType.OptionsColumn.AllowMove = False
        Me.CDataType.OptionsColumn.ReadOnly = True
        Me.CDataType.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        Me.CDataType.Width = 30
        '
        'FTReceiveNo
        '
        Me.FTReceiveNo.Caption = "Document Ref."
        Me.FTReceiveNo.FieldName = "FTReceiveNo"
        Me.FTReceiveNo.Name = "FTReceiveNo"
        Me.FTReceiveNo.OptionsColumn.AllowEdit = False
        Me.FTReceiveNo.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTReceiveNo.OptionsColumn.AllowMove = False
        Me.FTReceiveNo.OptionsColumn.ReadOnly = True
        Me.FTReceiveNo.Visible = True
        Me.FTReceiveNo.VisibleIndex = 7
        Me.FTReceiveNo.Width = 186
        '
        'CFTMatCode
        '
        Me.CFTMatCode.Caption = "FTMatCode"
        Me.CFTMatCode.FieldName = "FTMatCode"
        Me.CFTMatCode.Name = "CFTMatCode"
        Me.CFTMatCode.OptionsColumn.AllowEdit = False
        Me.CFTMatCode.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFTMatCode.OptionsColumn.AllowMove = False
        Me.CFTMatCode.OptionsColumn.ReadOnly = True
        Me.CFTMatCode.Visible = True
        Me.CFTMatCode.VisibleIndex = 0
        Me.CFTMatCode.Width = 132
        '
        'CFTMatColorCode
        '
        Me.CFTMatColorCode.Caption = "FTMatColorCode"
        Me.CFTMatColorCode.FieldName = "FTMatColorCode"
        Me.CFTMatColorCode.Name = "CFTMatColorCode"
        Me.CFTMatColorCode.OptionsColumn.AllowEdit = False
        Me.CFTMatColorCode.OptionsColumn.ReadOnly = True
        Me.CFTMatColorCode.Visible = True
        Me.CFTMatColorCode.VisibleIndex = 1
        Me.CFTMatColorCode.Width = 124
        '
        'CFTMatSizeCode
        '
        Me.CFTMatSizeCode.Caption = "FTMatSizeCode"
        Me.CFTMatSizeCode.FieldName = "FTMatSizeCode"
        Me.CFTMatSizeCode.Name = "CFTMatSizeCode"
        Me.CFTMatSizeCode.OptionsColumn.AllowEdit = False
        Me.CFTMatSizeCode.OptionsColumn.ReadOnly = True
        Me.CFTMatSizeCode.Visible = True
        Me.CFTMatSizeCode.VisibleIndex = 2
        Me.CFTMatSizeCode.Width = 131
        '
        'FTDescription
        '
        Me.FTDescription.AppearanceHeader.Options.UseTextOptions = True
        Me.FTDescription.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTDescription.Caption = "FTDescription"
        Me.FTDescription.FieldName = "FTDescription"
        Me.FTDescription.Name = "FTDescription"
        Me.FTDescription.OptionsColumn.AllowEdit = False
        Me.FTDescription.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTDescription.OptionsColumn.AllowMove = False
        Me.FTDescription.OptionsColumn.ReadOnly = True
        Me.FTDescription.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        Me.FTDescription.Visible = True
        Me.FTDescription.VisibleIndex = 3
        Me.FTDescription.Width = 467
        '
        'FNPrice
        '
        Me.FNPrice.Caption = "FNPrice"
        Me.FNPrice.FieldName = "FNPrice"
        Me.FNPrice.Name = "FNPrice"
        Me.FNPrice.OptionsColumn.AllowEdit = False
        Me.FNPrice.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNPrice.OptionsColumn.AllowMove = False
        Me.FNPrice.OptionsColumn.ReadOnly = True
        Me.FNPrice.Width = 100
        '
        'CFNAmount
        '
        Me.CFNAmount.AppearanceCell.Options.UseTextOptions = True
        Me.CFNAmount.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.CFNAmount.AppearanceHeader.Options.UseTextOptions = True
        Me.CFNAmount.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.CFNAmount.Caption = "FNAmount"
        Me.CFNAmount.DisplayFormat.FormatString = "{0:n4}"
        Me.CFNAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CFNAmount.FieldName = "FNAmount"
        Me.CFNAmount.Name = "CFNAmount"
        Me.CFNAmount.OptionsColumn.AllowEdit = False
        Me.CFNAmount.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFNAmount.OptionsColumn.AllowMove = False
        Me.CFNAmount.OptionsColumn.ReadOnly = True
        Me.CFNAmount.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        Me.CFNAmount.Width = 97
        '
        'FTPurchaseNo
        '
        Me.FTPurchaseNo.Caption = "FTPurchaseNo"
        Me.FTPurchaseNo.FieldName = "FTPurchaseNo"
        Me.FTPurchaseNo.Name = "FTPurchaseNo"
        Me.FTPurchaseNo.OptionsColumn.AllowEdit = False
        Me.FTPurchaseNo.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTPurchaseNo.OptionsColumn.AllowMove = False
        Me.FTPurchaseNo.OptionsColumn.ReadOnly = True
        Me.FTPurchaseNo.Visible = True
        Me.FTPurchaseNo.VisibleIndex = 4
        Me.FTPurchaseNo.Width = 113
        '
        'CFNBalQuantity
        '
        Me.CFNBalQuantity.AppearanceCell.Options.UseTextOptions = True
        Me.CFNBalQuantity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.CFNBalQuantity.AppearanceHeader.Options.UseTextOptions = True
        Me.CFNBalQuantity.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.CFNBalQuantity.Caption = "FNPOBalQty"
        Me.CFNBalQuantity.ColumnEdit = Me.ResFNPOBalQty
        Me.CFNBalQuantity.DisplayFormat.FormatString = "{0:n4}"
        Me.CFNBalQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CFNBalQuantity.FieldName = "FNBalQuantity"
        Me.CFNBalQuantity.Name = "CFNBalQuantity"
        Me.CFNBalQuantity.OptionsColumn.AllowEdit = False
        Me.CFNBalQuantity.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFNBalQuantity.OptionsColumn.AllowMove = False
        Me.CFNBalQuantity.OptionsColumn.ReadOnly = True
        Me.CFNBalQuantity.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        Me.CFNBalQuantity.Visible = True
        Me.CFNBalQuantity.VisibleIndex = 5
        Me.CFNBalQuantity.Width = 151
        '
        'ResFNPOBalQty
        '
        Me.ResFNPOBalQty.AutoHeight = False
        Me.ResFNPOBalQty.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ResFNPOBalQty.Name = "ResFNPOBalQty"
        '
        'CFNIssueQuantity
        '
        Me.CFNIssueQuantity.AppearanceCell.Options.UseTextOptions = True
        Me.CFNIssueQuantity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.CFNIssueQuantity.AppearanceHeader.Options.UseTextOptions = True
        Me.CFNIssueQuantity.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.CFNIssueQuantity.Caption = "FNIssueQuantity"
        Me.CFNIssueQuantity.ColumnEdit = Me.ResFNRcvQty
        Me.CFNIssueQuantity.DisplayFormat.FormatString = "{0:n4}"
        Me.CFNIssueQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CFNIssueQuantity.FieldName = "FNIssueQuantity"
        Me.CFNIssueQuantity.Name = "CFNIssueQuantity"
        Me.CFNIssueQuantity.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFNIssueQuantity.OptionsColumn.AllowMove = False
        Me.CFNIssueQuantity.OptionsColumn.AllowShowHide = False
        Me.CFNIssueQuantity.OptionsColumn.ShowInCustomizationForm = False
        Me.CFNIssueQuantity.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        Me.CFNIssueQuantity.Visible = True
        Me.CFNIssueQuantity.VisibleIndex = 6
        Me.CFNIssueQuantity.Width = 130
        '
        'ResFNRcvQty
        '
        Me.ResFNRcvQty.AutoHeight = False
        Me.ResFNRcvQty.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ResFNRcvQty.Name = "ResFNRcvQty"
        '
        'CFTReceiveNo
        '
        Me.CFTReceiveNo.Caption = "FTReceiveNo"
        Me.CFTReceiveNo.FieldName = "FTReceiveNo"
        Me.CFTReceiveNo.Name = "CFTReceiveNo"
        Me.CFTReceiveNo.OptionsColumn.AllowEdit = False
        Me.CFTReceiveNo.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFTReceiveNo.OptionsColumn.AllowMove = False
        Me.CFTReceiveNo.OptionsColumn.ReadOnly = True
        '
        'CFNHSysWHId
        '
        Me.CFNHSysWHId.Caption = "FNHSysWHId"
        Me.CFNHSysWHId.FieldName = "FNHSysWHId"
        Me.CFNHSysWHId.Name = "CFNHSysWHId"
        Me.CFNHSysWHId.OptionsColumn.AllowEdit = False
        Me.CFNHSysWHId.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFNHSysWHId.OptionsColumn.AllowMove = False
        Me.CFNHSysWHId.OptionsColumn.ReadOnly = True
        '
        'RepositoryItemCalcEdit1
        '
        Me.RepositoryItemCalcEdit1.AutoHeight = False
        Me.RepositoryItemCalcEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemCalcEdit1.Name = "RepositoryItemCalcEdit1"
        Me.RepositoryItemCalcEdit1.Precision = 4
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
        'ResFTStateSelect
        '
        Me.ResFTStateSelect.AutoHeight = False
        Me.ResFTStateSelect.Caption = "Check"
        Me.ResFTStateSelect.Name = "ResFTStateSelect"
        Me.ResFTStateSelect.ValueChecked = "1"
        Me.ResFTStateSelect.ValueUnchecked = "0"
        '
        'ReposFTFabricFrontSize
        '
        Me.ReposFTFabricFrontSize.AutoHeight = False
        Me.ReposFTFabricFrontSize.MaxLength = 50
        Me.ReposFTFabricFrontSize.Name = "ReposFTFabricFrontSize"
        '
        'ogb
        '
        Me.ogb.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogb.Controls.Add(Me.ocmcancel)
        Me.ogb.Controls.Add(Me.ocmok)
        Me.ogb.Controls.Add(Me.ogcrcv)
        Me.ogb.Location = New System.Drawing.Point(2, 2)
        Me.ogb.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogb.Name = "ogb"
        Me.ogb.Size = New System.Drawing.Size(1275, 495)
        Me.ogb.TabIndex = 6
        Me.ogb.Tag = "2|"
        Me.ogb.Text = "Purchase Detail"
        '
        'ocmcancel
        '
        Me.ocmcancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmcancel.Location = New System.Drawing.Point(1080, 1)
        Me.ocmcancel.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmcancel.Name = "ocmcancel"
        Me.ocmcancel.Size = New System.Drawing.Size(187, 25)
        Me.ocmcancel.TabIndex = 101
        Me.ocmcancel.TabStop = False
        Me.ocmcancel.Tag = "2|"
        Me.ocmcancel.Text = "CANCEL"
        '
        'ocmok
        '
        Me.ocmok.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmok.Location = New System.Drawing.Point(884, 1)
        Me.ocmok.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmok.Name = "ocmok"
        Me.ocmok.Size = New System.Drawing.Size(187, 25)
        Me.ocmok.TabIndex = 100
        Me.ocmok.TabStop = False
        Me.ocmok.Tag = "2|"
        Me.ocmok.Text = "OK"
        '
        'CFTUnitCode
        '
        Me.CFTUnitCode.Caption = "FTUnitCode"
        Me.CFTUnitCode.FieldName = "FTUnitCode"
        Me.CFTUnitCode.Name = "CFTUnitCode"
        Me.CFTUnitCode.OptionsColumn.AllowEdit = False
        Me.CFTUnitCode.OptionsColumn.ReadOnly = True
        Me.CFTUnitCode.Visible = True
        Me.CFTUnitCode.VisibleIndex = 8
        '
        'CFNHSysUnitId
        '
        Me.CFNHSysUnitId.Caption = "FNHSysUnitId"
        Me.CFNHSysUnitId.FieldName = "FNHSysUnitId"
        Me.CFNHSysUnitId.Name = "CFNHSysUnitId"
        '
        'wSMPReturnStockItem
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1279, 501)
        Me.ControlBox = False
        Me.Controls.Add(Me.ogb)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "wSMPReturnStockItem"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Sample Room Return Item"
        CType(Me.ogcrcv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvrcv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ResFNPOBalQty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ResFNRcvQty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCalcEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ResFNQuantity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ResFNRcvHisQty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ResFTStateSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ReposFTFabricFrontSize, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogb, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogb.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogcrcv As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvrcv As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents FTDescription As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNPrice As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFNAmount As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ResFNPOBalQty As DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit
    Friend WithEvents CFNBalQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFNIssueQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemCalcEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit
    Friend WithEvents ogb As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ocmcancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmok As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ResFNQuantity As DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit
    Friend WithEvents ResFNRcvHisQty As DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit
    Friend WithEvents ResFNRcvQty As DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit
    Friend WithEvents CDataType As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ResFTStateSelect As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents ReposFTFabricFrontSize As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents FTReceiveNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFTReceiveNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFTMatCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFNHSysWHId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTPurchaseNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFTMatColorCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFTMatSizeCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFTUnitCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFNHSysUnitId As DevExpress.XtraGrid.Columns.GridColumn
End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wGenerateBarcodeInven
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
        Me.ogcdetail = New DevExpress.XtraGrid.GridControl()
        Me.ogvdetail = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.FTRawMatCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTRawMatColorCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTRawMatSizeCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTFabricFrontSize = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTOrderNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTPurchaseNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNHSysRawMatId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNHSysWHId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNHSysUnitId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNQuantityStock = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNPricePerStock = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTUnitCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNBarCodeQty = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNBarcodeBalance = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNQtyBarcode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ReposFNQtyBarcode = New DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit()
        Me.FNGenBarcodeQty = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ReposFNGenBarcodeQty = New DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit()
        Me.FTBatchNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ReposFTBatchNo = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.FTGrade = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ReposFTGrade = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.FTRollNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepFTRollNo = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.FTOrderNoRef = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryFTOrderNoRef = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.CFNSeqRef = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmgenbarcode = New DevExpress.XtraEditors.SimpleButton()
        Me.ogbdetail = New DevExpress.XtraEditors.GroupControl()
        Me.FTStaLastAll = New DevExpress.XtraEditors.CheckEdit()
        CType(Me.ogcdetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvdetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ReposFNQtyBarcode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ReposFNGenBarcodeQty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ReposFTBatchNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ReposFTGrade, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepFTRollNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryFTOrderNoRef, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogbdetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbdetail.SuspendLayout()
        CType(Me.FTStaLastAll.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
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
        Me.ogcdetail.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.ReposFNQtyBarcode, Me.ReposFNGenBarcodeQty, Me.ReposFTBatchNo, Me.ReposFTGrade, Me.RepFTRollNo, Me.RepositoryFTOrderNoRef})
        Me.ogcdetail.Size = New System.Drawing.Size(1258, 485)
        Me.ogcdetail.TabIndex = 3
        Me.ogcdetail.TabStop = False
        Me.ogcdetail.Tag = "2|"
        Me.ogcdetail.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvdetail})
        '
        'ogvdetail
        '
        Me.ogvdetail.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.FTRawMatCode, Me.FTRawMatColorCode, Me.FTRawMatSizeCode, Me.FTFabricFrontSize, Me.FTOrderNo, Me.FTPurchaseNo, Me.FNHSysRawMatId, Me.FNHSysWHId, Me.FNHSysUnitId, Me.FNQuantityStock, Me.FNPricePerStock, Me.FTUnitCode, Me.FNBarCodeQty, Me.FNBarcodeBalance, Me.FNQtyBarcode, Me.FNGenBarcodeQty, Me.FTBatchNo, Me.FTGrade, Me.FTRollNo, Me.FTOrderNoRef, Me.CFNSeqRef})
        Me.ogvdetail.GridControl = Me.ogcdetail
        Me.ogvdetail.Name = "ogvdetail"
        Me.ogvdetail.OptionsCustomization.AllowGroup = False
        Me.ogvdetail.OptionsCustomization.AllowQuickHideColumns = False
        Me.ogvdetail.OptionsView.ColumnAutoWidth = False
        Me.ogvdetail.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.ogvdetail.OptionsView.ShowGroupPanel = False
        Me.ogvdetail.Tag = "2|"
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
        Me.FTRawMatCode.Visible = True
        Me.FTRawMatCode.VisibleIndex = 0
        Me.FTRawMatCode.Width = 102
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
        Me.FTRawMatColorCode.Visible = True
        Me.FTRawMatColorCode.VisibleIndex = 1
        Me.FTRawMatColorCode.Width = 86
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
        Me.FTRawMatSizeCode.Visible = True
        Me.FTRawMatSizeCode.VisibleIndex = 2
        Me.FTRawMatSizeCode.Width = 82
        '
        'FTFabricFrontSize
        '
        Me.FTFabricFrontSize.AppearanceHeader.Options.UseTextOptions = True
        Me.FTFabricFrontSize.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTFabricFrontSize.Caption = "FTFabricFrontSize"
        Me.FTFabricFrontSize.FieldName = "FTFabricFrontSize"
        Me.FTFabricFrontSize.Name = "FTFabricFrontSize"
        Me.FTFabricFrontSize.OptionsColumn.AllowEdit = False
        Me.FTFabricFrontSize.OptionsColumn.AllowMove = False
        Me.FTFabricFrontSize.OptionsColumn.ReadOnly = True
        Me.FTFabricFrontSize.Visible = True
        Me.FTFabricFrontSize.VisibleIndex = 3
        Me.FTFabricFrontSize.Width = 72
        '
        'FTOrderNo
        '
        Me.FTOrderNo.AppearanceHeader.Options.UseTextOptions = True
        Me.FTOrderNo.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTOrderNo.Caption = "FTOrderNo"
        Me.FTOrderNo.FieldName = "FTOrderNo"
        Me.FTOrderNo.Name = "FTOrderNo"
        Me.FTOrderNo.OptionsColumn.AllowEdit = False
        Me.FTOrderNo.OptionsColumn.AllowMove = False
        Me.FTOrderNo.OptionsColumn.ReadOnly = True
        Me.FTOrderNo.Visible = True
        Me.FTOrderNo.VisibleIndex = 4
        Me.FTOrderNo.Width = 88
        '
        'FTPurchaseNo
        '
        Me.FTPurchaseNo.AppearanceHeader.Options.UseTextOptions = True
        Me.FTPurchaseNo.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTPurchaseNo.Caption = "FTPurchaseNo"
        Me.FTPurchaseNo.FieldName = "FTPurchaseNo"
        Me.FTPurchaseNo.Name = "FTPurchaseNo"
        Me.FTPurchaseNo.OptionsColumn.AllowEdit = False
        Me.FTPurchaseNo.OptionsColumn.AllowMove = False
        Me.FTPurchaseNo.OptionsColumn.ReadOnly = True
        Me.FTPurchaseNo.OptionsColumn.ShowInCustomizationForm = False
        Me.FTPurchaseNo.Visible = True
        Me.FTPurchaseNo.VisibleIndex = 5
        Me.FTPurchaseNo.Width = 100
        '
        'FNHSysRawMatId
        '
        Me.FNHSysRawMatId.AppearanceHeader.Options.UseTextOptions = True
        Me.FNHSysRawMatId.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FNHSysRawMatId.Caption = "FNHSysRawMatId"
        Me.FNHSysRawMatId.FieldName = "FNHSysRawMatId"
        Me.FNHSysRawMatId.Name = "FNHSysRawMatId"
        Me.FNHSysRawMatId.OptionsColumn.AllowEdit = False
        Me.FNHSysRawMatId.OptionsColumn.AllowMove = False
        Me.FNHSysRawMatId.OptionsColumn.ReadOnly = True
        Me.FNHSysRawMatId.OptionsColumn.ShowInCustomizationForm = False
        '
        'FNHSysWHId
        '
        Me.FNHSysWHId.AppearanceHeader.Options.UseTextOptions = True
        Me.FNHSysWHId.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FNHSysWHId.Caption = "FNHSysWHId"
        Me.FNHSysWHId.FieldName = "FNHSysWHId"
        Me.FNHSysWHId.Name = "FNHSysWHId"
        Me.FNHSysWHId.OptionsColumn.AllowEdit = False
        Me.FNHSysWHId.OptionsColumn.AllowMove = False
        Me.FNHSysWHId.OptionsColumn.ReadOnly = True
        Me.FNHSysWHId.OptionsColumn.ShowInCustomizationForm = False
        '
        'FNHSysUnitId
        '
        Me.FNHSysUnitId.Caption = "FNHSysUnitId"
        Me.FNHSysUnitId.FieldName = "FNHSysUnitId"
        Me.FNHSysUnitId.Name = "FNHSysUnitId"
        '
        'FNQuantityStock
        '
        Me.FNQuantityStock.AppearanceHeader.Options.UseTextOptions = True
        Me.FNQuantityStock.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FNQuantityStock.Caption = "FNQuantityStock"
        Me.FNQuantityStock.FieldName = "FNQuantityStock"
        Me.FNQuantityStock.Name = "FNQuantityStock"
        Me.FNQuantityStock.OptionsColumn.AllowEdit = False
        Me.FNQuantityStock.OptionsColumn.AllowMove = False
        Me.FNQuantityStock.OptionsColumn.ReadOnly = True
        Me.FNQuantityStock.OptionsColumn.ShowInCustomizationForm = False
        Me.FNQuantityStock.Width = 101
        '
        'FNPricePerStock
        '
        Me.FNPricePerStock.AppearanceHeader.Options.UseTextOptions = True
        Me.FNPricePerStock.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FNPricePerStock.Caption = "FNPricePerStock"
        Me.FNPricePerStock.FieldName = "FNPricePerStock"
        Me.FNPricePerStock.Name = "FNPricePerStock"
        Me.FNPricePerStock.OptionsColumn.AllowEdit = False
        Me.FNPricePerStock.OptionsColumn.AllowMove = False
        Me.FNPricePerStock.OptionsColumn.ReadOnly = True
        Me.FNPricePerStock.OptionsColumn.ShowInCustomizationForm = False
        '
        'FTUnitCode
        '
        Me.FTUnitCode.AppearanceHeader.Options.UseTextOptions = True
        Me.FTUnitCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTUnitCode.Caption = "FTUnitCode"
        Me.FTUnitCode.FieldName = "FTUnitCode"
        Me.FTUnitCode.Name = "FTUnitCode"
        Me.FTUnitCode.OptionsColumn.AllowEdit = False
        Me.FTUnitCode.OptionsColumn.AllowMove = False
        Me.FTUnitCode.OptionsColumn.ReadOnly = True
        Me.FTUnitCode.Visible = True
        Me.FTUnitCode.VisibleIndex = 6
        Me.FTUnitCode.Width = 60
        '
        'FNBarCodeQty
        '
        Me.FNBarCodeQty.AppearanceHeader.Options.UseTextOptions = True
        Me.FNBarCodeQty.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FNBarCodeQty.Caption = "FNBarCodeQty"
        Me.FNBarCodeQty.FieldName = "FNBarCodeQty"
        Me.FNBarCodeQty.Name = "FNBarCodeQty"
        Me.FNBarCodeQty.OptionsColumn.AllowEdit = False
        Me.FNBarCodeQty.OptionsColumn.AllowMove = False
        Me.FNBarCodeQty.OptionsColumn.ReadOnly = True
        Me.FNBarCodeQty.OptionsColumn.ShowInCustomizationForm = False
        Me.FNBarCodeQty.Width = 91
        '
        'FNBarcodeBalance
        '
        Me.FNBarcodeBalance.AppearanceCell.Options.UseTextOptions = True
        Me.FNBarcodeBalance.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNBarcodeBalance.AppearanceHeader.Options.UseTextOptions = True
        Me.FNBarcodeBalance.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FNBarcodeBalance.Caption = "FNBarcodeBalance"
        Me.FNBarcodeBalance.DisplayFormat.FormatString = "N4"
        Me.FNBarcodeBalance.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNBarcodeBalance.FieldName = "FNBarcodeBalance"
        Me.FNBarcodeBalance.Name = "FNBarcodeBalance"
        Me.FNBarcodeBalance.OptionsColumn.AllowEdit = False
        Me.FNBarcodeBalance.OptionsColumn.AllowMove = False
        Me.FNBarcodeBalance.OptionsColumn.ReadOnly = True
        Me.FNBarcodeBalance.Visible = True
        Me.FNBarcodeBalance.VisibleIndex = 7
        Me.FNBarcodeBalance.Width = 93
        '
        'FNQtyBarcode
        '
        Me.FNQtyBarcode.AppearanceCell.BackColor = System.Drawing.Color.LightCyan
        Me.FNQtyBarcode.AppearanceCell.Options.UseBackColor = True
        Me.FNQtyBarcode.AppearanceHeader.Options.UseTextOptions = True
        Me.FNQtyBarcode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FNQtyBarcode.Caption = "FNQtyBarcode"
        Me.FNQtyBarcode.ColumnEdit = Me.ReposFNQtyBarcode
        Me.FNQtyBarcode.FieldName = "FNQtyBarcode"
        Me.FNQtyBarcode.Name = "FNQtyBarcode"
        Me.FNQtyBarcode.OptionsColumn.AllowMove = False
        Me.FNQtyBarcode.OptionsColumn.AllowShowHide = False
        Me.FNQtyBarcode.OptionsColumn.ShowInCustomizationForm = False
        Me.FNQtyBarcode.Visible = True
        Me.FNQtyBarcode.VisibleIndex = 8
        Me.FNQtyBarcode.Width = 84
        '
        'ReposFNQtyBarcode
        '
        Me.ReposFNQtyBarcode.AutoHeight = False
        Me.ReposFNQtyBarcode.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ReposFNQtyBarcode.DisplayFormat.FormatString = "N0"
        Me.ReposFNQtyBarcode.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.ReposFNQtyBarcode.EditFormat.FormatString = "N0"
        Me.ReposFNQtyBarcode.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.ReposFNQtyBarcode.Name = "ReposFNQtyBarcode"
        Me.ReposFNQtyBarcode.Precision = 0
        '
        'FNGenBarcodeQty
        '
        Me.FNGenBarcodeQty.AppearanceCell.BackColor = System.Drawing.Color.LightCyan
        Me.FNGenBarcodeQty.AppearanceCell.Options.UseBackColor = True
        Me.FNGenBarcodeQty.AppearanceHeader.Options.UseTextOptions = True
        Me.FNGenBarcodeQty.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FNGenBarcodeQty.Caption = "FNGenBarcodeQty"
        Me.FNGenBarcodeQty.ColumnEdit = Me.ReposFNGenBarcodeQty
        Me.FNGenBarcodeQty.FieldName = "FNGenBarcodeQty"
        Me.FNGenBarcodeQty.Name = "FNGenBarcodeQty"
        Me.FNGenBarcodeQty.OptionsColumn.AllowMove = False
        Me.FNGenBarcodeQty.OptionsColumn.AllowShowHide = False
        Me.FNGenBarcodeQty.OptionsColumn.ShowInCustomizationForm = False
        Me.FNGenBarcodeQty.Visible = True
        Me.FNGenBarcodeQty.VisibleIndex = 9
        Me.FNGenBarcodeQty.Width = 91
        '
        'ReposFNGenBarcodeQty
        '
        Me.ReposFNGenBarcodeQty.AutoHeight = False
        Me.ReposFNGenBarcodeQty.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ReposFNGenBarcodeQty.DisplayFormat.FormatString = "N4"
        Me.ReposFNGenBarcodeQty.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.ReposFNGenBarcodeQty.EditFormat.FormatString = "N4"
        Me.ReposFNGenBarcodeQty.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.ReposFNGenBarcodeQty.Name = "ReposFNGenBarcodeQty"
        Me.ReposFNGenBarcodeQty.Precision = 4
        '
        'FTBatchNo
        '
        Me.FTBatchNo.AppearanceCell.BackColor = System.Drawing.Color.LightCyan
        Me.FTBatchNo.AppearanceCell.Options.UseBackColor = True
        Me.FTBatchNo.AppearanceHeader.Options.UseTextOptions = True
        Me.FTBatchNo.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTBatchNo.Caption = "FTBatchNo"
        Me.FTBatchNo.ColumnEdit = Me.ReposFTBatchNo
        Me.FTBatchNo.FieldName = "FTBatchNo"
        Me.FTBatchNo.Name = "FTBatchNo"
        Me.FTBatchNo.OptionsColumn.AllowMove = False
        Me.FTBatchNo.OptionsColumn.AllowShowHide = False
        Me.FTBatchNo.OptionsColumn.ShowInCustomizationForm = False
        Me.FTBatchNo.Visible = True
        Me.FTBatchNo.VisibleIndex = 11
        Me.FTBatchNo.Width = 147
        '
        'ReposFTBatchNo
        '
        Me.ReposFTBatchNo.AutoHeight = False
        Me.ReposFTBatchNo.MaxLength = 50
        Me.ReposFTBatchNo.Name = "ReposFTBatchNo"
        '
        'FTGrade
        '
        Me.FTGrade.AppearanceCell.BackColor = System.Drawing.Color.LightCyan
        Me.FTGrade.AppearanceCell.Options.UseBackColor = True
        Me.FTGrade.AppearanceHeader.Options.UseTextOptions = True
        Me.FTGrade.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTGrade.Caption = "FTGrade"
        Me.FTGrade.ColumnEdit = Me.ReposFTGrade
        Me.FTGrade.FieldName = "FTGrade"
        Me.FTGrade.Name = "FTGrade"
        Me.FTGrade.OptionsColumn.AllowMove = False
        Me.FTGrade.OptionsColumn.AllowShowHide = False
        Me.FTGrade.OptionsColumn.ShowInCustomizationForm = False
        Me.FTGrade.Visible = True
        Me.FTGrade.VisibleIndex = 10
        Me.FTGrade.Width = 127
        '
        'ReposFTGrade
        '
        Me.ReposFTGrade.AutoHeight = False
        Me.ReposFTGrade.MaxLength = 50
        Me.ReposFTGrade.Name = "ReposFTGrade"
        '
        'FTRollNo
        '
        Me.FTRollNo.AppearanceCell.BackColor = System.Drawing.Color.LightCyan
        Me.FTRollNo.AppearanceCell.Options.UseBackColor = True
        Me.FTRollNo.AppearanceHeader.Options.UseTextOptions = True
        Me.FTRollNo.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTRollNo.Caption = "พับที่"
        Me.FTRollNo.ColumnEdit = Me.RepFTRollNo
        Me.FTRollNo.FieldName = "FTRollNo"
        Me.FTRollNo.Name = "FTRollNo"
        Me.FTRollNo.OptionsColumn.AllowMove = False
        Me.FTRollNo.OptionsColumn.AllowShowHide = False
        Me.FTRollNo.OptionsColumn.ShowInCustomizationForm = False
        Me.FTRollNo.Visible = True
        Me.FTRollNo.VisibleIndex = 12
        Me.FTRollNo.Width = 100
        '
        'RepFTRollNo
        '
        Me.RepFTRollNo.AutoHeight = False
        Me.RepFTRollNo.MaxLength = 50
        Me.RepFTRollNo.Name = "RepFTRollNo"
        '
        'FTOrderNoRef
        '
        Me.FTOrderNoRef.Caption = "FTOrderNoRef"
        Me.FTOrderNoRef.ColumnEdit = Me.RepositoryFTOrderNoRef
        Me.FTOrderNoRef.FieldName = "FTOrderNoRef"
        Me.FTOrderNoRef.Name = "FTOrderNoRef"
        Me.FTOrderNoRef.OptionsColumn.AllowEdit = False
        Me.FTOrderNoRef.OptionsColumn.ReadOnly = True
        Me.FTOrderNoRef.Visible = True
        Me.FTOrderNoRef.VisibleIndex = 13
        Me.FTOrderNoRef.Width = 100
        '
        'RepositoryFTOrderNoRef
        '
        Me.RepositoryFTOrderNoRef.AutoHeight = False
        Me.RepositoryFTOrderNoRef.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.RepositoryFTOrderNoRef.MaxLength = 30
        Me.RepositoryFTOrderNoRef.Name = "RepositoryFTOrderNoRef"
        '
        'CFNSeqRef
        '
        Me.CFNSeqRef.Caption = "FNSeqRef"
        Me.CFNSeqRef.FieldName = "FNSeqRef"
        Me.CFNSeqRef.Name = "CFNSeqRef"
        '
        'ocmexit
        '
        Me.ocmexit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmexit.Location = New System.Drawing.Point(1036, 1)
        Me.ocmexit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmexit.Name = "ocmexit"
        Me.ocmexit.Size = New System.Drawing.Size(206, 25)
        Me.ocmexit.TabIndex = 95
        Me.ocmexit.TabStop = False
        Me.ocmexit.Tag = "2|"
        Me.ocmexit.Text = "EXIT"
        '
        'ocmgenbarcode
        '
        Me.ocmgenbarcode.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmgenbarcode.Location = New System.Drawing.Point(819, 1)
        Me.ocmgenbarcode.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmgenbarcode.Name = "ocmgenbarcode"
        Me.ocmgenbarcode.Size = New System.Drawing.Size(206, 25)
        Me.ocmgenbarcode.TabIndex = 94
        Me.ocmgenbarcode.TabStop = False
        Me.ocmgenbarcode.Tag = "2|"
        Me.ocmgenbarcode.Text = "GENERATE BARCODE"
        '
        'ogbdetail
        '
        Me.ogbdetail.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbdetail.Controls.Add(Me.FTStaLastAll)
        Me.ogbdetail.Controls.Add(Me.ocmexit)
        Me.ogbdetail.Controls.Add(Me.ogcdetail)
        Me.ogbdetail.Controls.Add(Me.ocmgenbarcode)
        Me.ogbdetail.Location = New System.Drawing.Point(5, 6)
        Me.ogbdetail.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbdetail.Name = "ogbdetail"
        Me.ogbdetail.Size = New System.Drawing.Size(1269, 521)
        Me.ogbdetail.TabIndex = 5
        Me.ogbdetail.Text = "Detail"
        '
        'FTStaLastAll
        '
        Me.FTStaLastAll.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FTStaLastAll.Location = New System.Drawing.Point(307, 2)
        Me.FTStaLastAll.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTStaLastAll.Name = "FTStaLastAll"
        Me.FTStaLastAll.Properties.Appearance.Options.UseTextOptions = True
        Me.FTStaLastAll.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTStaLastAll.Properties.Caption = "เศษที่เหลือลงดวงสุดท้าย"
        Me.FTStaLastAll.Properties.GlyphAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTStaLastAll.Properties.ValueChecked = "1"
        Me.FTStaLastAll.Properties.ValueUnchecked = "0"
        Me.FTStaLastAll.Size = New System.Drawing.Size(476, 21)
        Me.FTStaLastAll.TabIndex = 103
        Me.FTStaLastAll.Tag = "2|"
        '
        'wGenerateBarcodeInven
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1277, 529)
        Me.ControlBox = False
        Me.Controls.Add(Me.ogbdetail)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "wGenerateBarcodeInven"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "wGenerateBarcode"
        CType(Me.ogcdetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvdetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ReposFNQtyBarcode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ReposFNGenBarcodeQty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ReposFTBatchNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ReposFTGrade, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepFTRollNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryFTOrderNoRef, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogbdetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbdetail.ResumeLayout(False)
        CType(Me.FTStaLastAll.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogcdetail As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvdetail As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmgenbarcode As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FTRawMatCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTRawMatColorCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTRawMatSizeCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTFabricFrontSize As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTPurchaseNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTOrderNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNHSysRawMatId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNHSysWHId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNQuantityStock As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNPricePerStock As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTUnitCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNBarCodeQty As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNBarcodeBalance As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNQtyBarcode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ReposFNQtyBarcode As DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit
    Friend WithEvents FNGenBarcodeQty As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ReposFNGenBarcodeQty As DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit
    Friend WithEvents FTBatchNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ReposFTBatchNo As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents FTGrade As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ReposFTGrade As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents ogbdetail As DevExpress.XtraEditors.GroupControl
    Friend WithEvents FTStaLastAll As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents FNHSysUnitId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTRollNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepFTRollNo As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents FTOrderNoRef As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryFTOrderNoRef As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents CFNSeqRef As DevExpress.XtraGrid.Columns.GridColumn
End Class

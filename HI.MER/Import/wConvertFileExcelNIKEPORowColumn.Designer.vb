<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wConvertFileExcelNIKEPORowColumn
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
        Me.ogbselectfile = New DevExpress.XtraEditors.GroupControl()
        Me.ocmselectfileexcel = New DevExpress.XtraEditors.SimpleButton()
        Me.ogc = New DevExpress.XtraGrid.GridControl()
        Me.ogv = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.FTStateComplete = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepFTSelect = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.CFTName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.xFTErrorMessage = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemImageComboBox1 = New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox()
        Me.otb = New DevExpress.XtraTab.XtraTabControl()
        Me.otpnormal = New DevExpress.XtraTab.XtraTabPage()
        Me.opshet = New DevExpress.XtraSpreadsheet.SpreadsheetControl()
        Me.otpgrid1 = New DevExpress.XtraTab.XtraTabPage()
        Me.ogcdt1 = New DevExpress.XtraGrid.GridControl()
        Me.ogvdt1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.CFIXFTUserLogIn = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFIXFNRowSeq = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFIXPurchaseOrderNumber = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFIXTradingCoPONumber = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFIXPOLineItemNumber = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFIXVendorCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFIXVendorName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFIXPMODECcode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFIXPMODECName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFIXDPOMLineItemStatus = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFIXDocType = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFIXDocTypecripDestion = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFIXDocumentDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFIXOGAC = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFIXGAC = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFIXlanningSeasonCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFIXProductCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFIXModeofTransportation = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFIXPlantCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFIXPlantName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFIXShipToCustomerNumber = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFIXShipToCustomerName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFIXCustomerPO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFIXTotalItemQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFIXGenderAge = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFIXGenderAgeDescription = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFIXStyleNumber = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFIXTradingCompanyCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFIXPlanningSeasonYear = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFIXScheduleLineItemNumber = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFIXDestinationCountryCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFIXestinationCountryName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ogbmainprocbutton = New DevExpress.XtraEditors.PanelControl()
        Me.ocmReadExcel = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmclear = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmsavelayout = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.ogbselectfile, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbselectfile.SuspendLayout()
        CType(Me.ogc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepFTSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemImageComboBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.otb, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.otb.SuspendLayout()
        Me.otpnormal.SuspendLayout()
        Me.otpgrid1.SuspendLayout()
        CType(Me.ogcdt1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvdt1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbmainprocbutton.SuspendLayout()
        Me.SuspendLayout()
        '
        'ogbselectfile
        '
        Me.ogbselectfile.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbselectfile.Controls.Add(Me.ocmselectfileexcel)
        Me.ogbselectfile.Controls.Add(Me.ogc)
        Me.ogbselectfile.Location = New System.Drawing.Point(3, 3)
        Me.ogbselectfile.Name = "ogbselectfile"
        Me.ogbselectfile.ShowCaption = False
        Me.ogbselectfile.Size = New System.Drawing.Size(1150, 136)
        Me.ogbselectfile.TabIndex = 2
        Me.ogbselectfile.Text = "Select File"
        '
        'ocmselectfileexcel
        '
        Me.ocmselectfileexcel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmselectfileexcel.Location = New System.Drawing.Point(1003, 10)
        Me.ocmselectfileexcel.Name = "ocmselectfileexcel"
        Me.ocmselectfileexcel.Size = New System.Drawing.Size(131, 25)
        Me.ocmselectfileexcel.TabIndex = 313
        Me.ocmselectfileexcel.TabStop = False
        Me.ocmselectfileexcel.Tag = "2|"
        Me.ocmselectfileexcel.Text = "EXCEL FILE"
        '
        'ogc
        '
        Me.ogc.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogc.Location = New System.Drawing.Point(2, 2)
        Me.ogc.MainView = Me.ogv
        Me.ogc.Name = "ogc"
        Me.ogc.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepFTSelect, Me.RepositoryItemImageComboBox1})
        Me.ogc.Size = New System.Drawing.Size(983, 129)
        Me.ogc.TabIndex = 312
        Me.ogc.TabStop = False
        Me.ogc.Tag = "3|"
        Me.ogc.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogv})
        '
        'ogv
        '
        Me.ogv.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.FTStateComplete, Me.CFTName, Me.xFTErrorMessage})
        Me.ogv.GridControl = Me.ogc
        Me.ogv.Name = "ogv"
        Me.ogv.OptionsCustomization.AllowGroup = False
        Me.ogv.OptionsCustomization.AllowQuickHideColumns = False
        Me.ogv.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect
        Me.ogv.OptionsView.ColumnAutoWidth = False
        Me.ogv.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.ogv.OptionsView.ShowGroupPanel = False
        Me.ogv.Tag = "2|"
        '
        'FTStateComplete
        '
        Me.FTStateComplete.AppearanceHeader.Options.UseTextOptions = True
        Me.FTStateComplete.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTStateComplete.Caption = "Read Complete"
        Me.FTStateComplete.ColumnEdit = Me.RepFTSelect
        Me.FTStateComplete.FieldName = "FTStateComplete"
        Me.FTStateComplete.Name = "FTStateComplete"
        Me.FTStateComplete.OptionsColumn.AllowEdit = False
        Me.FTStateComplete.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTStateComplete.OptionsColumn.AllowShowHide = False
        Me.FTStateComplete.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTStateComplete.OptionsColumn.ReadOnly = True
        Me.FTStateComplete.OptionsColumn.ShowInCustomizationForm = False
        Me.FTStateComplete.Visible = True
        Me.FTStateComplete.VisibleIndex = 1
        Me.FTStateComplete.Width = 115
        '
        'RepFTSelect
        '
        Me.RepFTSelect.AutoHeight = False
        Me.RepFTSelect.Caption = "Check"
        Me.RepFTSelect.Name = "RepFTSelect"
        Me.RepFTSelect.ValueChecked = "1"
        Me.RepFTSelect.ValueUnchecked = "0"
        '
        'CFTName
        '
        Me.CFTName.AppearanceHeader.Options.UseTextOptions = True
        Me.CFTName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.CFTName.Caption = "FileName"
        Me.CFTName.FieldName = "FTFileName"
        Me.CFTName.Name = "CFTName"
        Me.CFTName.OptionsColumn.AllowEdit = False
        Me.CFTName.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFTName.OptionsColumn.AllowShowHide = False
        Me.CFTName.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFTName.OptionsColumn.ReadOnly = True
        Me.CFTName.Visible = True
        Me.CFTName.VisibleIndex = 0
        Me.CFTName.Width = 415
        '
        'xFTErrorMessage
        '
        Me.xFTErrorMessage.Caption = "Error Message"
        Me.xFTErrorMessage.FieldName = "FTErrorMessage"
        Me.xFTErrorMessage.Name = "xFTErrorMessage"
        Me.xFTErrorMessage.OptionsColumn.AllowEdit = False
        Me.xFTErrorMessage.OptionsColumn.AllowMove = False
        Me.xFTErrorMessage.OptionsColumn.ReadOnly = True
        Me.xFTErrorMessage.Visible = True
        Me.xFTErrorMessage.VisibleIndex = 2
        Me.xFTErrorMessage.Width = 382
        '
        'RepositoryItemImageComboBox1
        '
        Me.RepositoryItemImageComboBox1.AutoHeight = False
        Me.RepositoryItemImageComboBox1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemImageComboBox1.Name = "RepositoryItemImageComboBox1"
        '
        'otb
        '
        Me.otb.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.otb.Location = New System.Drawing.Point(3, 144)
        Me.otb.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.otb.Name = "otb"
        Me.otb.SelectedTabPage = Me.otpnormal
        Me.otb.Size = New System.Drawing.Size(1150, 430)
        Me.otb.TabIndex = 392
        Me.otb.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.otpgrid1, Me.otpnormal})
        '
        'otpnormal
        '
        Me.otpnormal.Controls.Add(Me.opshet)
        Me.otpnormal.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.otpnormal.Name = "otpnormal"
        Me.otpnormal.PageVisible = False
        Me.otpnormal.Size = New System.Drawing.Size(1128, 405)
        Me.otpnormal.Text = "Excel Detail"
        '
        'opshet
        '
        Me.opshet.Dock = System.Windows.Forms.DockStyle.Fill
        Me.opshet.Location = New System.Drawing.Point(0, 0)
        Me.opshet.Name = "opshet"
        Me.opshet.Options.Behavior.Column.Resize = DevExpress.XtraSpreadsheet.DocumentCapability.Enabled
        Me.opshet.ReadOnly = True
        Me.opshet.Size = New System.Drawing.Size(1128, 405)
        Me.opshet.TabIndex = 2
        '
        'otpgrid1
        '
        Me.otpgrid1.Controls.Add(Me.ogcdt1)
        Me.otpgrid1.Name = "otpgrid1"
        Me.otpgrid1.Size = New System.Drawing.Size(1148, 405)
        Me.otpgrid1.Text = "NIKE PO Detail"
        '
        'ogcdt1
        '
        Me.ogcdt1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogcdt1.Location = New System.Drawing.Point(0, 0)
        Me.ogcdt1.MainView = Me.ogvdt1
        Me.ogcdt1.Name = "ogcdt1"
        Me.ogcdt1.Size = New System.Drawing.Size(1148, 405)
        Me.ogcdt1.TabIndex = 398
        Me.ogcdt1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvdt1})
        '
        'ogvdt1
        '
        Me.ogvdt1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.CFIXFTUserLogIn, Me.CFIXFNRowSeq, Me.CFIXPurchaseOrderNumber, Me.CFIXTradingCoPONumber, Me.CFIXPOLineItemNumber, Me.CFIXVendorCode, Me.CFIXVendorName, Me.CFIXPMODECcode, Me.CFIXPMODECName, Me.CFIXDPOMLineItemStatus, Me.CFIXDocType, Me.CFIXDocTypecripDestion, Me.CFIXDocumentDate, Me.CFIXOGAC, Me.CFIXGAC, Me.CFIXlanningSeasonCode, Me.CFIXProductCode, Me.CFIXModeofTransportation, Me.CFIXPlantCode, Me.CFIXPlantName, Me.CFIXShipToCustomerNumber, Me.CFIXShipToCustomerName, Me.CFIXCustomerPO, Me.CFIXGenderAge, Me.CFIXGenderAgeDescription, Me.CFIXStyleNumber, Me.CFIXTradingCompanyCode, Me.CFIXPlanningSeasonYear, Me.CFIXScheduleLineItemNumber, Me.CFIXDestinationCountryCode, Me.CFIXestinationCountryName, Me.CFIXTotalItemQuantity})
        Me.ogvdt1.GridControl = Me.ogcdt1
        Me.ogvdt1.Name = "ogvdt1"
        Me.ogvdt1.OptionsBehavior.ReadOnly = True
        Me.ogvdt1.OptionsView.ColumnAutoWidth = False
        Me.ogvdt1.OptionsView.ShowGroupPanel = False
        '
        'CFIXFTUserLogIn
        '
        Me.CFIXFTUserLogIn.Caption = "User Log In"
        Me.CFIXFTUserLogIn.FieldName = "FTUserLogIn"
        Me.CFIXFTUserLogIn.Name = "CFIXFTUserLogIn"
        Me.CFIXFTUserLogIn.OptionsColumn.AllowEdit = False
        Me.CFIXFTUserLogIn.OptionsColumn.ReadOnly = True
        '
        'CFIXFNRowSeq
        '
        Me.CFIXFNRowSeq.Caption = "FNRowSeq"
        Me.CFIXFNRowSeq.FieldName = "FNRowSeq"
        Me.CFIXFNRowSeq.Name = "CFIXFNRowSeq"
        Me.CFIXFNRowSeq.OptionsColumn.AllowEdit = False
        Me.CFIXFNRowSeq.OptionsColumn.ReadOnly = True
        '
        'CFIXPurchaseOrderNumber
        '
        Me.CFIXPurchaseOrderNumber.Caption = "Purchase Order Number"
        Me.CFIXPurchaseOrderNumber.FieldName = "Purchase Order Number"
        Me.CFIXPurchaseOrderNumber.Name = "CFIXPurchaseOrderNumber"
        Me.CFIXPurchaseOrderNumber.OptionsColumn.AllowEdit = False
        Me.CFIXPurchaseOrderNumber.OptionsColumn.ReadOnly = True
        Me.CFIXPurchaseOrderNumber.Visible = True
        Me.CFIXPurchaseOrderNumber.VisibleIndex = 0
        '
        'CFIXTradingCoPONumber
        '
        Me.CFIXTradingCoPONumber.Caption = "Trading Co PO Number"
        Me.CFIXTradingCoPONumber.FieldName = "Trading Co PO Number"
        Me.CFIXTradingCoPONumber.Name = "CFIXTradingCoPONumber"
        Me.CFIXTradingCoPONumber.OptionsColumn.AllowEdit = False
        Me.CFIXTradingCoPONumber.OptionsColumn.ReadOnly = True
        Me.CFIXTradingCoPONumber.Visible = True
        Me.CFIXTradingCoPONumber.VisibleIndex = 1
        '
        'CFIXPOLineItemNumber
        '
        Me.CFIXPOLineItemNumber.Caption = "PO Line Item Number"
        Me.CFIXPOLineItemNumber.FieldName = "PO Line Item Number"
        Me.CFIXPOLineItemNumber.Name = "CFIXPOLineItemNumber"
        Me.CFIXPOLineItemNumber.OptionsColumn.AllowEdit = False
        Me.CFIXPOLineItemNumber.OptionsColumn.ReadOnly = True
        Me.CFIXPOLineItemNumber.Visible = True
        Me.CFIXPOLineItemNumber.VisibleIndex = 2
        '
        'CFIXVendorCode
        '
        Me.CFIXVendorCode.Caption = "Vendor Code"
        Me.CFIXVendorCode.FieldName = "Vendor Code"
        Me.CFIXVendorCode.Name = "CFIXVendorCode"
        Me.CFIXVendorCode.OptionsColumn.AllowEdit = False
        Me.CFIXVendorCode.OptionsColumn.ReadOnly = True
        Me.CFIXVendorCode.Visible = True
        Me.CFIXVendorCode.VisibleIndex = 3
        '
        'CFIXVendorName
        '
        Me.CFIXVendorName.Caption = "Vendor Name"
        Me.CFIXVendorName.FieldName = "Vendor Name"
        Me.CFIXVendorName.Name = "CFIXVendorName"
        Me.CFIXVendorName.OptionsColumn.AllowEdit = False
        Me.CFIXVendorName.OptionsColumn.ReadOnly = True
        Me.CFIXVendorName.Visible = True
        Me.CFIXVendorName.VisibleIndex = 4
        '
        'CFIXPMODECcode
        '
        Me.CFIXPMODECcode.Caption = "PMO/DEC code"
        Me.CFIXPMODECcode.FieldName = "PMO/DEC code"
        Me.CFIXPMODECcode.Name = "CFIXPMODECcode"
        Me.CFIXPMODECcode.OptionsColumn.AllowEdit = False
        Me.CFIXPMODECcode.OptionsColumn.ReadOnly = True
        Me.CFIXPMODECcode.Visible = True
        Me.CFIXPMODECcode.VisibleIndex = 5
        '
        'CFIXPMODECName
        '
        Me.CFIXPMODECName.Caption = "PMO/DEC Name"
        Me.CFIXPMODECName.FieldName = "PMO/DEC Name"
        Me.CFIXPMODECName.Name = "CFIXPMODECName"
        Me.CFIXPMODECName.OptionsColumn.AllowEdit = False
        Me.CFIXPMODECName.OptionsColumn.ReadOnly = True
        Me.CFIXPMODECName.Visible = True
        Me.CFIXPMODECName.VisibleIndex = 6
        '
        'CFIXDPOMLineItemStatus
        '
        Me.CFIXDPOMLineItemStatus.Caption = "DPOM Line Item Status"
        Me.CFIXDPOMLineItemStatus.FieldName = "DPOM Line Item Status"
        Me.CFIXDPOMLineItemStatus.Name = "CFIXDPOMLineItemStatus"
        Me.CFIXDPOMLineItemStatus.OptionsColumn.AllowEdit = False
        Me.CFIXDPOMLineItemStatus.OptionsColumn.ReadOnly = True
        Me.CFIXDPOMLineItemStatus.Visible = True
        Me.CFIXDPOMLineItemStatus.VisibleIndex = 7
        '
        'CFIXDocType
        '
        Me.CFIXDocType.Caption = "Doc Type"
        Me.CFIXDocType.FieldName = "Doc Type"
        Me.CFIXDocType.Name = "CFIXDocType"
        Me.CFIXDocType.OptionsColumn.AllowEdit = False
        Me.CFIXDocType.OptionsColumn.ReadOnly = True
        Me.CFIXDocType.Visible = True
        Me.CFIXDocType.VisibleIndex = 8
        '
        'CFIXDocTypecripDestion
        '
        Me.CFIXDocTypecripDestion.Caption = "Doc Type cripDestion"
        Me.CFIXDocTypecripDestion.FieldName = "Doc Type cripDestion"
        Me.CFIXDocTypecripDestion.Name = "CFIXDocTypecripDestion"
        Me.CFIXDocTypecripDestion.OptionsColumn.AllowEdit = False
        Me.CFIXDocTypecripDestion.OptionsColumn.ReadOnly = True
        Me.CFIXDocTypecripDestion.Visible = True
        Me.CFIXDocTypecripDestion.VisibleIndex = 9
        '
        'CFIXDocumentDate
        '
        Me.CFIXDocumentDate.Caption = "Document Date"
        Me.CFIXDocumentDate.FieldName = "Document Date"
        Me.CFIXDocumentDate.Name = "CFIXDocumentDate"
        Me.CFIXDocumentDate.OptionsColumn.AllowEdit = False
        Me.CFIXDocumentDate.OptionsColumn.ReadOnly = True
        Me.CFIXDocumentDate.Visible = True
        Me.CFIXDocumentDate.VisibleIndex = 10
        '
        'CFIXOGAC
        '
        Me.CFIXOGAC.Caption = "OGAC"
        Me.CFIXOGAC.FieldName = "OGAC"
        Me.CFIXOGAC.Name = "CFIXOGAC"
        Me.CFIXOGAC.OptionsColumn.AllowEdit = False
        Me.CFIXOGAC.OptionsColumn.ReadOnly = True
        Me.CFIXOGAC.Visible = True
        Me.CFIXOGAC.VisibleIndex = 11
        '
        'CFIXGAC
        '
        Me.CFIXGAC.Caption = "GAC"
        Me.CFIXGAC.FieldName = "GAC"
        Me.CFIXGAC.Name = "CFIXGAC"
        Me.CFIXGAC.OptionsColumn.AllowEdit = False
        Me.CFIXGAC.OptionsColumn.ReadOnly = True
        Me.CFIXGAC.Visible = True
        Me.CFIXGAC.VisibleIndex = 12
        '
        'CFIXlanningSeasonCode
        '
        Me.CFIXlanningSeasonCode.Caption = "Planning Season Code"
        Me.CFIXlanningSeasonCode.FieldName = "Planning Season Code"
        Me.CFIXlanningSeasonCode.Name = "CFIXlanningSeasonCode"
        Me.CFIXlanningSeasonCode.OptionsColumn.AllowEdit = False
        Me.CFIXlanningSeasonCode.OptionsColumn.ReadOnly = True
        Me.CFIXlanningSeasonCode.Visible = True
        Me.CFIXlanningSeasonCode.VisibleIndex = 13
        '
        'CFIXProductCode
        '
        Me.CFIXProductCode.Caption = "Product Code"
        Me.CFIXProductCode.FieldName = "Product Code"
        Me.CFIXProductCode.Name = "CFIXProductCode"
        Me.CFIXProductCode.OptionsColumn.AllowEdit = False
        Me.CFIXProductCode.OptionsColumn.ReadOnly = True
        Me.CFIXProductCode.Visible = True
        Me.CFIXProductCode.VisibleIndex = 14
        '
        'CFIXModeofTransportation
        '
        Me.CFIXModeofTransportation.Caption = "Mode of Transportation"
        Me.CFIXModeofTransportation.FieldName = "Mode of Transportation"
        Me.CFIXModeofTransportation.Name = "CFIXModeofTransportation"
        Me.CFIXModeofTransportation.OptionsColumn.AllowEdit = False
        Me.CFIXModeofTransportation.OptionsColumn.ReadOnly = True
        Me.CFIXModeofTransportation.Visible = True
        Me.CFIXModeofTransportation.VisibleIndex = 15
        '
        'CFIXPlantCode
        '
        Me.CFIXPlantCode.Caption = "Plant Code"
        Me.CFIXPlantCode.FieldName = "Plant Code"
        Me.CFIXPlantCode.Name = "CFIXPlantCode"
        Me.CFIXPlantCode.OptionsColumn.AllowEdit = False
        Me.CFIXPlantCode.OptionsColumn.ReadOnly = True
        Me.CFIXPlantCode.Visible = True
        Me.CFIXPlantCode.VisibleIndex = 16
        '
        'CFIXPlantName
        '
        Me.CFIXPlantName.Caption = "Plant Name"
        Me.CFIXPlantName.FieldName = "Plant Name"
        Me.CFIXPlantName.Name = "CFIXPlantName"
        Me.CFIXPlantName.OptionsColumn.AllowEdit = False
        Me.CFIXPlantName.OptionsColumn.ReadOnly = True
        Me.CFIXPlantName.Visible = True
        Me.CFIXPlantName.VisibleIndex = 17
        '
        'CFIXShipToCustomerNumber
        '
        Me.CFIXShipToCustomerNumber.Caption = "Ship To Customer Number"
        Me.CFIXShipToCustomerNumber.FieldName = "Ship To Customer Number"
        Me.CFIXShipToCustomerNumber.Name = "CFIXShipToCustomerNumber"
        Me.CFIXShipToCustomerNumber.OptionsColumn.AllowEdit = False
        Me.CFIXShipToCustomerNumber.OptionsColumn.ReadOnly = True
        Me.CFIXShipToCustomerNumber.Visible = True
        Me.CFIXShipToCustomerNumber.VisibleIndex = 18
        '
        'CFIXShipToCustomerName
        '
        Me.CFIXShipToCustomerName.Caption = "Ship To Customer Name"
        Me.CFIXShipToCustomerName.FieldName = "Ship To Customer Name"
        Me.CFIXShipToCustomerName.Name = "CFIXShipToCustomerName"
        Me.CFIXShipToCustomerName.OptionsColumn.AllowEdit = False
        Me.CFIXShipToCustomerName.OptionsColumn.ReadOnly = True
        Me.CFIXShipToCustomerName.Visible = True
        Me.CFIXShipToCustomerName.VisibleIndex = 19
        '
        'CFIXCustomerPO
        '
        Me.CFIXCustomerPO.Caption = "Customer PO"
        Me.CFIXCustomerPO.FieldName = "Customer PO"
        Me.CFIXCustomerPO.Name = "CFIXCustomerPO"
        Me.CFIXCustomerPO.OptionsColumn.AllowEdit = False
        Me.CFIXCustomerPO.OptionsColumn.ReadOnly = True
        Me.CFIXCustomerPO.Visible = True
        Me.CFIXCustomerPO.VisibleIndex = 20
        '
        'CFIXTotalItemQuantity
        '
        Me.CFIXTotalItemQuantity.Caption = "Total Item Quantity"
        Me.CFIXTotalItemQuantity.FieldName = "Total Item Quantity"
        Me.CFIXTotalItemQuantity.Name = "CFIXTotalItemQuantity"
        Me.CFIXTotalItemQuantity.OptionsColumn.AllowEdit = False
        Me.CFIXTotalItemQuantity.OptionsColumn.ReadOnly = True
        Me.CFIXTotalItemQuantity.Visible = True
        Me.CFIXTotalItemQuantity.VisibleIndex = 29
        '
        'CFIXGenderAge
        '
        Me.CFIXGenderAge.Caption = "Gender Age"
        Me.CFIXGenderAge.FieldName = "Gender Age"
        Me.CFIXGenderAge.Name = "CFIXGenderAge"
        Me.CFIXGenderAge.OptionsColumn.AllowEdit = False
        Me.CFIXGenderAge.OptionsColumn.ReadOnly = True
        Me.CFIXGenderAge.Visible = True
        Me.CFIXGenderAge.VisibleIndex = 21
        '
        'CFIXGenderAgeDescription
        '
        Me.CFIXGenderAgeDescription.Caption = "Gender Age Description"
        Me.CFIXGenderAgeDescription.FieldName = "Gender Age Description"
        Me.CFIXGenderAgeDescription.Name = "CFIXGenderAgeDescription"
        Me.CFIXGenderAgeDescription.OptionsColumn.AllowEdit = False
        Me.CFIXGenderAgeDescription.OptionsColumn.ReadOnly = True
        Me.CFIXGenderAgeDescription.Visible = True
        Me.CFIXGenderAgeDescription.VisibleIndex = 22
        '
        'CFIXStyleNumber
        '
        Me.CFIXStyleNumber.Caption = "Style Number"
        Me.CFIXStyleNumber.FieldName = "Style Number"
        Me.CFIXStyleNumber.Name = "CFIXStyleNumber"
        Me.CFIXStyleNumber.OptionsColumn.AllowEdit = False
        Me.CFIXStyleNumber.OptionsColumn.ReadOnly = True
        Me.CFIXStyleNumber.Visible = True
        Me.CFIXStyleNumber.VisibleIndex = 23
        '
        'CFIXTradingCompanyCode
        '
        Me.CFIXTradingCompanyCode.Caption = "Trading Company Code"
        Me.CFIXTradingCompanyCode.FieldName = "Trading Company Code"
        Me.CFIXTradingCompanyCode.Name = "CFIXTradingCompanyCode"
        Me.CFIXTradingCompanyCode.OptionsColumn.AllowEdit = False
        Me.CFIXTradingCompanyCode.OptionsColumn.ReadOnly = True
        Me.CFIXTradingCompanyCode.Visible = True
        Me.CFIXTradingCompanyCode.VisibleIndex = 24
        '
        'CFIXPlanningSeasonYear
        '
        Me.CFIXPlanningSeasonYear.Caption = "Planning Season Year"
        Me.CFIXPlanningSeasonYear.FieldName = "Planning Season Year"
        Me.CFIXPlanningSeasonYear.Name = "CFIXPlanningSeasonYear"
        Me.CFIXPlanningSeasonYear.OptionsColumn.AllowEdit = False
        Me.CFIXPlanningSeasonYear.OptionsColumn.ReadOnly = True
        Me.CFIXPlanningSeasonYear.Visible = True
        Me.CFIXPlanningSeasonYear.VisibleIndex = 25
        '
        'CFIXScheduleLineItemNumber
        '
        Me.CFIXScheduleLineItemNumber.Caption = "Schedule Line Item Number"
        Me.CFIXScheduleLineItemNumber.FieldName = "Schedule Line Item Number"
        Me.CFIXScheduleLineItemNumber.Name = "CFIXScheduleLineItemNumber"
        Me.CFIXScheduleLineItemNumber.OptionsColumn.AllowEdit = False
        Me.CFIXScheduleLineItemNumber.OptionsColumn.ReadOnly = True
        Me.CFIXScheduleLineItemNumber.Visible = True
        Me.CFIXScheduleLineItemNumber.VisibleIndex = 26
        '
        'CFIXDestinationCountryCode
        '
        Me.CFIXDestinationCountryCode.Caption = "Destination Country Code"
        Me.CFIXDestinationCountryCode.FieldName = "Destination Country Code"
        Me.CFIXDestinationCountryCode.Name = "CFIXDestinationCountryCode"
        Me.CFIXDestinationCountryCode.OptionsColumn.AllowEdit = False
        Me.CFIXDestinationCountryCode.OptionsColumn.ReadOnly = True
        Me.CFIXDestinationCountryCode.Visible = True
        Me.CFIXDestinationCountryCode.VisibleIndex = 27
        Me.CFIXDestinationCountryCode.Width = 100
        '
        'CFIXestinationCountryName
        '
        Me.CFIXestinationCountryName.Caption = "estination Country Name"
        Me.CFIXestinationCountryName.FieldName = "estination Country Name"
        Me.CFIXestinationCountryName.Name = "CFIXestinationCountryName"
        Me.CFIXestinationCountryName.OptionsColumn.AllowEdit = False
        Me.CFIXestinationCountryName.OptionsColumn.ReadOnly = True
        Me.CFIXestinationCountryName.Visible = True
        Me.CFIXestinationCountryName.VisibleIndex = 28
        Me.CFIXestinationCountryName.Width = 200
        '
        'ogbmainprocbutton
        '
        Me.ogbmainprocbutton.Controls.Add(Me.ocmsavelayout)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmReadExcel)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmexit)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmclear)
        Me.ogbmainprocbutton.Location = New System.Drawing.Point(77, 268)
        Me.ogbmainprocbutton.Name = "ogbmainprocbutton"
        Me.ogbmainprocbutton.Size = New System.Drawing.Size(736, 236)
        Me.ogbmainprocbutton.TabIndex = 393
        '
        'ocmReadExcel
        '
        Me.ocmReadExcel.Location = New System.Drawing.Point(321, 106)
        Me.ocmReadExcel.Name = "ocmReadExcel"
        Me.ocmReadExcel.Size = New System.Drawing.Size(95, 25)
        Me.ocmReadExcel.TabIndex = 99
        Me.ocmReadExcel.TabStop = False
        Me.ocmReadExcel.Tag = "2|"
        Me.ocmReadExcel.Text = "READ EXCEL FILE"
        '
        'ocmexit
        '
        Me.ocmexit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmexit.Location = New System.Drawing.Point(673, 3)
        Me.ocmexit.Name = "ocmexit"
        Me.ocmexit.Size = New System.Drawing.Size(55, 25)
        Me.ocmexit.TabIndex = 96
        Me.ocmexit.TabStop = False
        Me.ocmexit.Tag = "2|"
        Me.ocmexit.Text = "EXIT"
        '
        'ocmclear
        '
        Me.ocmclear.Location = New System.Drawing.Point(41, 45)
        Me.ocmclear.Name = "ocmclear"
        Me.ocmclear.Size = New System.Drawing.Size(113, 25)
        Me.ocmclear.TabIndex = 95
        Me.ocmclear.TabStop = False
        Me.ocmclear.Tag = "2|"
        Me.ocmclear.Text = "CLEAR"
        '
        'ocmsavelayout
        '
        Me.ocmsavelayout.Location = New System.Drawing.Point(299, 161)
        Me.ocmsavelayout.Name = "ocmsavelayout"
        Me.ocmsavelayout.Size = New System.Drawing.Size(117, 23)
        Me.ocmsavelayout.TabIndex = 338
        Me.ocmsavelayout.Text = "savelayoutgrid"
        '
        'wConvertFileExcelNIKEPORowColumn
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1153, 576)
        Me.Controls.Add(Me.ogbmainprocbutton)
        Me.Controls.Add(Me.otb)
        Me.Controls.Add(Me.ogbselectfile)
        Me.Name = "wConvertFileExcelNIKEPORowColumn"
        Me.Text = "Convert File Excel NIKE PO แนวตั้ง To แนวนอน"
        CType(Me.ogbselectfile, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbselectfile.ResumeLayout(False)
        CType(Me.ogc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepFTSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemImageComboBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.otb, System.ComponentModel.ISupportInitialize).EndInit()
        Me.otb.ResumeLayout(False)
        Me.otpnormal.ResumeLayout(False)
        Me.otpgrid1.ResumeLayout(False)
        CType(Me.ogcdt1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvdt1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbmainprocbutton.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogbselectfile As DevExpress.XtraEditors.GroupControl
    Friend WithEvents otb As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents otpnormal As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents ogbmainprocbutton As DevExpress.XtraEditors.PanelControl
    Friend WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmclear As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents opshet As DevExpress.XtraSpreadsheet.SpreadsheetControl
    Friend WithEvents otpgrid1 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents ogcdt1 As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvdt1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents CFIXFTUserLogIn As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFIXFNRowSeq As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFIXPurchaseOrderNumber As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFIXTradingCoPONumber As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFIXPOLineItemNumber As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFIXVendorCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFIXVendorName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFIXPMODECcode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFIXPMODECName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFIXDPOMLineItemStatus As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFIXDocType As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFIXDocTypecripDestion As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFIXDocumentDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFIXOGAC As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFIXGAC As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFIXlanningSeasonCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFIXProductCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFIXModeofTransportation As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFIXPlantCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFIXPlantName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFIXShipToCustomerNumber As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFIXShipToCustomerName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFIXCustomerPO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFIXTotalItemQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFIXGenderAge As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFIXGenderAgeDescription As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFIXStyleNumber As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFIXTradingCompanyCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFIXPlanningSeasonYear As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ocmReadExcel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmselectfileexcel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ogc As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogv As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents FTStateComplete As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepFTSelect As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents CFTName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents xFTErrorMessage As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFIXScheduleLineItemNumber As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFIXDestinationCountryCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFIXestinationCountryName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemImageComboBox1 As DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
    Friend WithEvents ocmsavelayout As DevExpress.XtraEditors.SimpleButton
End Class

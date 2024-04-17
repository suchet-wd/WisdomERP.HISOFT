Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraGrid.Views.Grid

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wFabricflowsTrackig
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
        Me.ogbdetail = New DevExpress.XtraEditors.GroupControl()
        Me.FTSupl = New DevExpress.XtraEditors.TextEdit()
        Me.Supplier_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNFabricFlowsListType_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNFabricFlowsListType = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.RepositoryItemPopupSupplier = New DevExpress.XtraEditors.PopupContainerEdit()
        Me.PopupOrderNo = New DevExpress.XtraEditors.PopupContainerControl()
        Me.ockselectorderall = New DevExpress.XtraEditors.CheckEdit()
        Me.ogdlistsupplier = New DevExpress.XtraGrid.GridControl()
        Me.ogvlistsupplier = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.xOrderFTSelect = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemSelectsupl = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.xFTSupplierCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.xxx3FTSupplierCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ogbmainprocbutton = New DevExpress.XtraEditors.PanelControl()
        Me.ocmdownloadpopdf = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmsavelayout = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmclear = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmload = New DevExpress.XtraEditors.SimpleButton()
        Me.ogdpo = New DevExpress.XtraGrid.GridControl()
        Me.ogvpo = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.xFTSelect = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepCheckEdit = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.StateFlagNew = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.VenderCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.VendorName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.VendorLocation = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FactoryCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.PONo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.PODate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.Shipto = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GarmentShipmentDestination = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.MatrClass = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.POItemCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.MatrCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.UPCCOMBOIM = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ContentCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CareCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.Color = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColorName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GCW = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.Size = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.SizeMatrix = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.Currency = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.Price = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.QtyUnit = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.DeliveryDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.Season = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.Custporef = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.Buy = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.BuyNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.Category = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.Program = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.SubProgram = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.StyleNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.StyleName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.PORefType = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.POMatching1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.POMatching2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.POMatching3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.POMatching4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.POMatching5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ItemMatching1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ItemMatching2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ItemMatching3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ItemMatching4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ItemMatching5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.Position = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.Type = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.PaymentTerm = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.Remarkfrommer = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RemarkForPurchase = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CompanyName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.address1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.address2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.address3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.address4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.sysowner = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.sysownername = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.sysownermail = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ZeroInspection = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GarmentShip = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.OGACDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.HITLink = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.NIKECustomerPONo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.QRS = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.PromoQty = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.MOQ = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ActualQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.Quantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.POUploadDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.POUploadTime = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.POUploadBy = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CountryOfOrigin = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.SaleOrderSaleOrderLine = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.NikeSAPPOPONIKEPOLine = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.NikematerialStyleColorway = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.Modifire1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.Modifire2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.Modifire3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.MPOHZ = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ItemDescription = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.BulkQRSSample = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CCTotalpage = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.Surcharge = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ChinaInsertCard = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.P1pc2in1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.WovenLabelSizeLength = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ArgentinaImportNumber = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.DownFill = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.SYS_ID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ChinasizeMatrixtype = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.EAGERPItemNumber = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CompoundColororCCIMfor2in1CCIM = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CPO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.BhasaIndonesiaProductBrand = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.SAFCODE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.Youthorder = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.NFCproduct = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.NeckneckopeningX2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ChestbodywidthX2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CenterBackbodylength = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.WaistwaistrelaxedInseam = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.PackQuantityQTYPERSELLINGUNIT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.Fabriccode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.PRODUCTDESCRIPTION = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.PRODUCTCODEDESCRIPTION = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GARMENTSTYLEFIELD = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INSEAMSTYLE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.EndCustomer = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.oDockManager = New DevExpress.XtraBars.Docking.DockManager()
        Me.otbmain = New DevExpress.XtraTab.XtraTabControl()
        Me.otpnew = New DevExpress.XtraTab.XtraTabPage()
        Me.otpworking = New DevExpress.XtraTab.XtraTabPage()
        Me.ogdwpo = New DevExpress.XtraGrid.GridControl()
        Me.ogvwpo = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.dFTSelect = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCheckEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.PIStateEdit = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.PIStateAccept = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.xVendorLocation = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.xVendorName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.sFactoryCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.sPONo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.sPODate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.sBuy = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.sBuyNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.sSeason = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.sPORefType = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.sGarmentShipmentDestination = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.sMatrCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.sPOItemCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.sStyleNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.sColor = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.sColorName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.sQRS = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.sPromoQty = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.sMOQ = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.sActualQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.sQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.sQtyUnit = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.sPrice = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.sSurcharge = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.sSubProgram = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.T2_Confirm_OrderNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.T2_Confirm_PO_Date = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.Estimatedeldate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.T2_Confirm_Price = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.T2_Confirm_Quantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.T2_Confirm_MOQQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNPIQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.Doc_Surcharge_Amount = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNPINetAmt = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.T2_Confirm_Ship_Date = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.T2_Confirm_ShipQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.Actualdeldate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.T2_Confirm_Ship_Date2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.T2_Confirm_ShipQuantity2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.Actualdeldate2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.POBalanceQty = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.InvoiceNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTAWBNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.T2_Confirm_By = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.T2_Confirm_Note = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTStateHasFile = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.sCategory = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.sRemarkForPurchase = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.sShipto = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.xMatrClass = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.sGCW = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.sSize = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ssCurrency = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.sDeliveryDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.sOGACDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.sRcvQty = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.sRcvDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RcvBalanceQty = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.AcknowledgeBy = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.AcknowledgeDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.AcknowledgeTime = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.StateAcknowledgeLock = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.sVenderCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.sEndCustomer = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ssysownername = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ssysownermail = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.StateRejectedBuy = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.sRejectedBuyBy = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RejectedBuyDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RejectedBuyTime = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RejectedBuyNote = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.otppayment = New DevExpress.XtraTab.XtraTabPage()
        Me.ogdpopayment = New DevExpress.XtraGrid.GridControl()
        Me.ogvpopayment = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.aPONo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.PayType = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.sPaymentTerm = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.aPaymentDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.LCNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.aPINo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.aPIDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.aRcvPIDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.aPISuplCFMDeliveryDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.aInvoiceNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.aInvoiceDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.aPurchaseDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.aPurchaseBy = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.aSupplierCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.aSupplierName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.aCurrency = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.aDeliveryDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.aCompany = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.aBuy = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.aPOUnit = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.aPOQty = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.aPOAmount = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.aPOOutstandingQty = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.aSentDocToAccDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.aFinishbalancePaymentDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.aPIQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.aPINetAmt = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.aPIDocCNAmt = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.aPIDocDNAmt = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.aPIDocSurchargeAmt = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.aPIDocNetAmt = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.aNote = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.aFTStateHasFile = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCheckEdit2 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        CType(Me.ogbdetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbdetail.SuspendLayout()
        CType(Me.FTSupl.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNFabricFlowsListType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupSupplier.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PopupOrderNo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PopupOrderNo.SuspendLayout()
        CType(Me.ockselectorderall.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogdlistsupplier, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvlistsupplier, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemSelectsupl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbmainprocbutton.SuspendLayout()
        CType(Me.ogdpo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvpo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepCheckEdit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.oDockManager, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.otbmain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.otbmain.SuspendLayout()
        Me.otpnew.SuspendLayout()
        Me.otpworking.SuspendLayout()
        CType(Me.ogdwpo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvwpo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.otppayment.SuspendLayout()
        CType(Me.ogdpopayment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvpopayment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ogbdetail
        '
        Me.ogbdetail.Controls.Add(Me.FTSupl)
        Me.ogbdetail.Controls.Add(Me.Supplier_lbl)
        Me.ogbdetail.Controls.Add(Me.FNFabricFlowsListType_lbl)
        Me.ogbdetail.Controls.Add(Me.FNFabricFlowsListType)
        Me.ogbdetail.Controls.Add(Me.RepositoryItemPopupSupplier)
        Me.ogbdetail.Dock = System.Windows.Forms.DockStyle.Top
        Me.ogbdetail.Location = New System.Drawing.Point(0, 0)
        Me.ogbdetail.Name = "ogbdetail"
        Me.ogbdetail.ShowCaption = False
        Me.ogbdetail.Size = New System.Drawing.Size(1137, 75)
        Me.ogbdetail.TabIndex = 0
        '
        'FTSupl
        '
        Me.FTSupl.EnterMoveNextControl = True
        Me.FTSupl.Location = New System.Drawing.Point(163, 33)
        Me.FTSupl.Name = "FTSupl"
        Me.FTSupl.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FTSupl.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTSupl.Properties.Appearance.Options.UseBackColor = True
        Me.FTSupl.Properties.Appearance.Options.UseForeColor = True
        Me.FTSupl.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FTSupl.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FTSupl.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FTSupl.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FTSupl.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FTSupl.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FTSupl.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FTSupl.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FTSupl.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FTSupl.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FTSupl.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FTSupl.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FTSupl.Properties.ReadOnly = True
        Me.FTSupl.Size = New System.Drawing.Size(423, 21)
        Me.FTSupl.TabIndex = 572
        Me.FTSupl.TabStop = False
        Me.FTSupl.Tag = "2|"
        '
        'Supplier_lbl
        '
        Me.Supplier_lbl.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(31, Byte), Integer), CType(CType(53, Byte), Integer))
        Me.Supplier_lbl.Appearance.Options.UseForeColor = True
        Me.Supplier_lbl.Appearance.Options.UseTextOptions = True
        Me.Supplier_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.Supplier_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.Supplier_lbl.Location = New System.Drawing.Point(35, 33)
        Me.Supplier_lbl.Name = "Supplier_lbl"
        Me.Supplier_lbl.Size = New System.Drawing.Size(123, 18)
        Me.Supplier_lbl.TabIndex = 570
        Me.Supplier_lbl.Tag = "2|"
        Me.Supplier_lbl.Text = "Supplier :"
        '
        'FNFabricFlowsListType_lbl
        '
        Me.FNFabricFlowsListType_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FNFabricFlowsListType_lbl.Appearance.Options.UseForeColor = True
        Me.FNFabricFlowsListType_lbl.Appearance.Options.UseTextOptions = True
        Me.FNFabricFlowsListType_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNFabricFlowsListType_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNFabricFlowsListType_lbl.Location = New System.Drawing.Point(60, 14)
        Me.FNFabricFlowsListType_lbl.Name = "FNFabricFlowsListType_lbl"
        Me.FNFabricFlowsListType_lbl.Size = New System.Drawing.Size(98, 13)
        Me.FNFabricFlowsListType_lbl.TabIndex = 569
        Me.FNFabricFlowsListType_lbl.Text = "Data Status :"
        '
        'FNFabricFlowsListType
        '
        Me.FNFabricFlowsListType.EditValue = ""
        Me.FNFabricFlowsListType.EnterMoveNextControl = True
        Me.FNFabricFlowsListType.Location = New System.Drawing.Point(163, 11)
        Me.FNFabricFlowsListType.Name = "FNFabricFlowsListType"
        Me.FNFabricFlowsListType.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FNFabricFlowsListType.Properties.Appearance.Options.UseBackColor = True
        Me.FNFabricFlowsListType.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FNFabricFlowsListType.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FNFabricFlowsListType.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FNFabricFlowsListType.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FNFabricFlowsListType.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNFabricFlowsListType.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNFabricFlowsListType.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNFabricFlowsListType.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNFabricFlowsListType.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNFabricFlowsListType.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNFabricFlowsListType.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNFabricFlowsListType.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNFabricFlowsListType.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FNFabricFlowsListType.Properties.Items.AddRange(New Object() {"New && Update Purchase", "Workig Purchase", "Purchase Payment"})
        Me.FNFabricFlowsListType.Properties.Tag = ""
        Me.FNFabricFlowsListType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.FNFabricFlowsListType.Size = New System.Drawing.Size(148, 21)
        Me.FNFabricFlowsListType.TabIndex = 568
        Me.FNFabricFlowsListType.Tag = "2|"
        '
        'RepositoryItemPopupSupplier
        '
        Me.RepositoryItemPopupSupplier.EditValue = ""
        Me.RepositoryItemPopupSupplier.Location = New System.Drawing.Point(586, 33)
        Me.RepositoryItemPopupSupplier.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.RepositoryItemPopupSupplier.Name = "RepositoryItemPopupSupplier"
        Me.RepositoryItemPopupSupplier.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[True]
        Me.RepositoryItemPopupSupplier.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupSupplier.Properties.PopupControl = Me.PopupOrderNo
        Me.RepositoryItemPopupSupplier.Size = New System.Drawing.Size(21, 21)
        Me.RepositoryItemPopupSupplier.TabIndex = 571
        '
        'PopupOrderNo
        '
        Me.PopupOrderNo.Controls.Add(Me.ockselectorderall)
        Me.PopupOrderNo.Controls.Add(Me.ogdlistsupplier)
        Me.PopupOrderNo.Location = New System.Drawing.Point(351, 67)
        Me.PopupOrderNo.Name = "PopupOrderNo"
        Me.PopupOrderNo.Size = New System.Drawing.Size(467, 363)
        Me.PopupOrderNo.TabIndex = 388
        '
        'ockselectorderall
        '
        Me.ockselectorderall.EditValue = "0"
        Me.ockselectorderall.Location = New System.Drawing.Point(27, 6)
        Me.ockselectorderall.Name = "ockselectorderall"
        Me.ockselectorderall.Properties.Caption = "Select All"
        Me.ockselectorderall.Properties.Tag = ""
        Me.ockselectorderall.Properties.ValueChecked = "1"
        Me.ockselectorderall.Properties.ValueUnchecked = "0"
        Me.ockselectorderall.Size = New System.Drawing.Size(122, 20)
        Me.ockselectorderall.TabIndex = 549
        Me.ockselectorderall.Tag = "2|"
        '
        'ogdlistsupplier
        '
        Me.ogdlistsupplier.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogdlistsupplier.Location = New System.Drawing.Point(4, 26)
        Me.ogdlistsupplier.MainView = Me.ogvlistsupplier
        Me.ogdlistsupplier.Name = "ogdlistsupplier"
        Me.ogdlistsupplier.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemSelectsupl})
        Me.ogdlistsupplier.Size = New System.Drawing.Size(460, 332)
        Me.ogdlistsupplier.TabIndex = 308
        Me.ogdlistsupplier.TabStop = False
        Me.ogdlistsupplier.Tag = "3|"
        Me.ogdlistsupplier.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvlistsupplier})
        '
        'ogvlistsupplier
        '
        Me.ogvlistsupplier.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.xOrderFTSelect, Me.xFTSupplierCode, Me.xxx3FTSupplierCode})
        Me.ogvlistsupplier.GridControl = Me.ogdlistsupplier
        Me.ogvlistsupplier.Name = "ogvlistsupplier"
        Me.ogvlistsupplier.OptionsCustomization.AllowGroup = False
        Me.ogvlistsupplier.OptionsCustomization.AllowQuickHideColumns = False
        Me.ogvlistsupplier.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect
        Me.ogvlistsupplier.OptionsView.ColumnAutoWidth = False
        Me.ogvlistsupplier.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.ogvlistsupplier.OptionsView.ShowGroupPanel = False
        Me.ogvlistsupplier.Tag = "2|"
        '
        'xOrderFTSelect
        '
        Me.xOrderFTSelect.AppearanceHeader.Options.UseTextOptions = True
        Me.xOrderFTSelect.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.xOrderFTSelect.Caption = "FTSelect"
        Me.xOrderFTSelect.ColumnEdit = Me.RepositoryItemSelectsupl
        Me.xOrderFTSelect.FieldName = "FTSelect"
        Me.xOrderFTSelect.Name = "xOrderFTSelect"
        Me.xOrderFTSelect.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.xOrderFTSelect.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.xOrderFTSelect.OptionsColumn.AllowMove = False
        Me.xOrderFTSelect.OptionsColumn.AllowShowHide = False
        Me.xOrderFTSelect.Visible = True
        Me.xOrderFTSelect.VisibleIndex = 0
        Me.xOrderFTSelect.Width = 41
        '
        'RepositoryItemSelectsupl
        '
        Me.RepositoryItemSelectsupl.AutoHeight = False
        Me.RepositoryItemSelectsupl.Name = "RepositoryItemSelectsupl"
        Me.RepositoryItemSelectsupl.ValueChecked = "1"
        Me.RepositoryItemSelectsupl.ValueUnchecked = "0"
        '
        'xFTSupplierCode
        '
        Me.xFTSupplierCode.AppearanceHeader.Options.UseTextOptions = True
        Me.xFTSupplierCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.xFTSupplierCode.Caption = "Supplier Code"
        Me.xFTSupplierCode.FieldName = "FTSupplierCode"
        Me.xFTSupplierCode.Name = "xFTSupplierCode"
        Me.xFTSupplierCode.OptionsColumn.AllowEdit = False
        Me.xFTSupplierCode.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.xFTSupplierCode.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.xFTSupplierCode.OptionsColumn.AllowMove = False
        Me.xFTSupplierCode.OptionsColumn.AllowShowHide = False
        Me.xFTSupplierCode.OptionsColumn.ReadOnly = True
        Me.xFTSupplierCode.Visible = True
        Me.xFTSupplierCode.VisibleIndex = 1
        Me.xFTSupplierCode.Width = 103
        '
        'xxx3FTSupplierCode
        '
        Me.xxx3FTSupplierCode.Caption = "Supplier Name"
        Me.xxx3FTSupplierCode.FieldName = "FTSupplierName"
        Me.xxx3FTSupplierCode.Name = "xxx3FTSupplierCode"
        Me.xxx3FTSupplierCode.OptionsColumn.AllowEdit = False
        Me.xxx3FTSupplierCode.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.xxx3FTSupplierCode.OptionsColumn.AllowMove = False
        Me.xxx3FTSupplierCode.OptionsColumn.ReadOnly = True
        Me.xxx3FTSupplierCode.Visible = True
        Me.xxx3FTSupplierCode.VisibleIndex = 2
        Me.xxx3FTSupplierCode.Width = 257
        '
        'ogbmainprocbutton
        '
        Me.ogbmainprocbutton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmdownloadpopdf)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmsavelayout)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmexit)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmclear)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmload)
        Me.ogbmainprocbutton.Location = New System.Drawing.Point(149, 296)
        Me.ogbmainprocbutton.Name = "ogbmainprocbutton"
        Me.ogbmainprocbutton.Size = New System.Drawing.Size(607, 99)
        Me.ogbmainprocbutton.TabIndex = 386
        Me.ogbmainprocbutton.Tag = "2|"
        '
        'ocmdownloadpopdf
        '
        Me.ocmdownloadpopdf.Location = New System.Drawing.Point(55, 50)
        Me.ocmdownloadpopdf.Name = "ocmdownloadpopdf"
        Me.ocmdownloadpopdf.Size = New System.Drawing.Size(117, 23)
        Me.ocmdownloadpopdf.TabIndex = 331
        Me.ocmdownloadpopdf.Text = "Download PO PDF"
        '
        'ocmsavelayout
        '
        Me.ocmsavelayout.Location = New System.Drawing.Point(245, 38)
        Me.ocmsavelayout.Name = "ocmsavelayout"
        Me.ocmsavelayout.Size = New System.Drawing.Size(117, 23)
        Me.ocmsavelayout.TabIndex = 330
        Me.ocmsavelayout.Text = "Save Layout"
        '
        'ocmexit
        '
        Me.ocmexit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmexit.Location = New System.Drawing.Point(494, 11)
        Me.ocmexit.Name = "ocmexit"
        Me.ocmexit.Size = New System.Drawing.Size(95, 25)
        Me.ocmexit.TabIndex = 96
        Me.ocmexit.TabStop = False
        Me.ocmexit.Tag = "2|"
        Me.ocmexit.Text = "EXIT"
        '
        'ocmclear
        '
        Me.ocmclear.Location = New System.Drawing.Point(14, 10)
        Me.ocmclear.Name = "ocmclear"
        Me.ocmclear.Size = New System.Drawing.Size(95, 25)
        Me.ocmclear.TabIndex = 95
        Me.ocmclear.TabStop = False
        Me.ocmclear.Tag = "2|"
        Me.ocmclear.Text = "CLEAR"
        '
        'ocmload
        '
        Me.ocmload.Location = New System.Drawing.Point(115, 12)
        Me.ocmload.Name = "ocmload"
        Me.ocmload.Size = New System.Drawing.Size(117, 23)
        Me.ocmload.TabIndex = 329
        Me.ocmload.Text = "Load Data"
        '
        'ogdpo
        '
        Me.ogdpo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogdpo.Location = New System.Drawing.Point(0, 0)
        Me.ogdpo.MainView = Me.ogvpo
        Me.ogdpo.Name = "ogdpo"
        Me.ogdpo.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepCheckEdit})
        Me.ogdpo.Size = New System.Drawing.Size(1129, 344)
        Me.ogdpo.TabIndex = 0
        Me.ogdpo.TabStop = False
        Me.ogdpo.Tag = "2|"
        Me.ogdpo.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvpo})
        '
        'ogvpo
        '
        Me.ogvpo.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.xFTSelect, Me.StateFlagNew, Me.VenderCode, Me.VendorName, Me.VendorLocation, Me.FactoryCode, Me.PONo, Me.PODate, Me.Shipto, Me.GarmentShipmentDestination, Me.MatrClass, Me.POItemCode, Me.MatrCode, Me.UPCCOMBOIM, Me.ContentCode, Me.CareCode, Me.Color, Me.ColorName, Me.GCW, Me.Size, Me.SizeMatrix, Me.Currency, Me.Price, Me.QtyUnit, Me.DeliveryDate, Me.Season, Me.Custporef, Me.Buy, Me.BuyNo, Me.Category, Me.Program, Me.SubProgram, Me.StyleNo, Me.StyleName, Me.PORefType, Me.POMatching1, Me.POMatching2, Me.POMatching3, Me.POMatching4, Me.POMatching5, Me.ItemMatching1, Me.ItemMatching2, Me.ItemMatching3, Me.ItemMatching4, Me.ItemMatching5, Me.Position, Me.Type, Me.PaymentTerm, Me.Remarkfrommer, Me.RemarkForPurchase, Me.CompanyName, Me.address1, Me.address2, Me.address3, Me.address4, Me.sysowner, Me.sysownername, Me.sysownermail, Me.ZeroInspection, Me.GarmentShip, Me.OGACDate, Me.HITLink, Me.NIKECustomerPONo, Me.QRS, Me.PromoQty, Me.MOQ, Me.ActualQuantity, Me.Quantity, Me.POUploadDate, Me.POUploadTime, Me.POUploadBy, Me.CountryOfOrigin, Me.SaleOrderSaleOrderLine, Me.NikeSAPPOPONIKEPOLine, Me.NikematerialStyleColorway, Me.Modifire1, Me.Modifire2, Me.Modifire3, Me.MPOHZ, Me.ItemDescription, Me.BulkQRSSample, Me.CCTotalpage, Me.Surcharge, Me.ChinaInsertCard, Me.P1pc2in1, Me.WovenLabelSizeLength, Me.ArgentinaImportNumber, Me.DownFill, Me.SYS_ID, Me.ChinasizeMatrixtype, Me.EAGERPItemNumber, Me.CompoundColororCCIMfor2in1CCIM, Me.CPO, Me.BhasaIndonesiaProductBrand, Me.SAFCODE, Me.Youthorder, Me.NFCproduct, Me.NeckneckopeningX2, Me.ChestbodywidthX2, Me.CenterBackbodylength, Me.WaistwaistrelaxedInseam, Me.PackQuantityQTYPERSELLINGUNIT, Me.Fabriccode, Me.PRODUCTDESCRIPTION, Me.PRODUCTCODEDESCRIPTION, Me.GARMENTSTYLEFIELD, Me.INSEAMSTYLE, Me.EndCustomer})
        Me.ogvpo.GridControl = Me.ogdpo
        Me.ogvpo.Name = "ogvpo"
        Me.ogvpo.OptionsCustomization.AllowQuickHideColumns = False
        Me.ogvpo.OptionsView.ColumnAutoWidth = False
        Me.ogvpo.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.ogvpo.OptionsView.ShowGroupPanel = False
        Me.ogvpo.Tag = "2|"
        '
        'xFTSelect
        '
        Me.xFTSelect.Caption = "select"
        Me.xFTSelect.ColumnEdit = Me.RepCheckEdit
        Me.xFTSelect.FieldName = "FTSelect"
        Me.xFTSelect.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
        Me.xFTSelect.MinWidth = 22
        Me.xFTSelect.Name = "xFTSelect"
        Me.xFTSelect.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.xFTSelect.Visible = True
        Me.xFTSelect.VisibleIndex = 0
        Me.xFTSelect.Width = 50
        '
        'RepCheckEdit
        '
        Me.RepCheckEdit.AutoHeight = False
        Me.RepCheckEdit.Caption = "Check"
        Me.RepCheckEdit.Name = "RepCheckEdit"
        Me.RepCheckEdit.ValueChecked = "1"
        Me.RepCheckEdit.ValueUnchecked = "0"
        '
        'StateFlagNew
        '
        Me.StateFlagNew.Caption = "Status"
        Me.StateFlagNew.FieldName = "StateFlagNew"
        Me.StateFlagNew.MinWidth = 21
        Me.StateFlagNew.Name = "StateFlagNew"
        Me.StateFlagNew.OptionsColumn.AllowEdit = False
        Me.StateFlagNew.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.StateFlagNew.OptionsColumn.ReadOnly = True
        Me.StateFlagNew.Visible = True
        Me.StateFlagNew.VisibleIndex = 1
        Me.StateFlagNew.Width = 51
        '
        'VenderCode
        '
        Me.VenderCode.Caption = "Vendor Code"
        Me.VenderCode.FieldName = "VenderCode"
        Me.VenderCode.MinWidth = 21
        Me.VenderCode.Name = "VenderCode"
        Me.VenderCode.OptionsColumn.AllowEdit = False
        Me.VenderCode.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.VenderCode.OptionsColumn.ReadOnly = True
        Me.VenderCode.Visible = True
        Me.VenderCode.VisibleIndex = 2
        Me.VenderCode.Width = 103
        '
        'VendorName
        '
        Me.VendorName.Caption = "Vendor Name"
        Me.VendorName.FieldName = "VendorName"
        Me.VendorName.MinWidth = 21
        Me.VendorName.Name = "VendorName"
        Me.VendorName.OptionsColumn.AllowEdit = False
        Me.VendorName.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.VendorName.OptionsColumn.ReadOnly = True
        Me.VendorName.Visible = True
        Me.VendorName.VisibleIndex = 3
        Me.VendorName.Width = 214
        '
        'VendorLocation
        '
        Me.VendorLocation.Caption = "Vendor Location"
        Me.VendorLocation.FieldName = "VendorLocation"
        Me.VendorLocation.MinWidth = 21
        Me.VendorLocation.Name = "VendorLocation"
        Me.VendorLocation.OptionsColumn.AllowEdit = False
        Me.VendorLocation.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.VendorLocation.OptionsColumn.ReadOnly = True
        Me.VendorLocation.Visible = True
        Me.VendorLocation.VisibleIndex = 4
        Me.VendorLocation.Width = 77
        '
        'FactoryCode
        '
        Me.FactoryCode.Caption = "Factory Code"
        Me.FactoryCode.FieldName = "FactoryCode"
        Me.FactoryCode.MinWidth = 21
        Me.FactoryCode.Name = "FactoryCode"
        Me.FactoryCode.OptionsColumn.AllowEdit = False
        Me.FactoryCode.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FactoryCode.OptionsColumn.ReadOnly = True
        Me.FactoryCode.Visible = True
        Me.FactoryCode.VisibleIndex = 5
        Me.FactoryCode.Width = 77
        '
        'PONo
        '
        Me.PONo.Caption = "PO Number"
        Me.PONo.FieldName = "PONo"
        Me.PONo.MinWidth = 21
        Me.PONo.Name = "PONo"
        Me.PONo.OptionsColumn.AllowEdit = False
        Me.PONo.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.PONo.OptionsColumn.ReadOnly = True
        Me.PONo.Visible = True
        Me.PONo.VisibleIndex = 6
        Me.PONo.Width = 129
        '
        'PODate
        '
        Me.PODate.Caption = "PO Date"
        Me.PODate.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.PODate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.PODate.FieldName = "PODate"
        Me.PODate.MinWidth = 21
        Me.PODate.Name = "PODate"
        Me.PODate.OptionsColumn.AllowEdit = False
        Me.PODate.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.PODate.OptionsColumn.ReadOnly = True
        Me.PODate.Visible = True
        Me.PODate.VisibleIndex = 7
        Me.PODate.Width = 77
        '
        'Shipto
        '
        Me.Shipto.Caption = "Ship to"
        Me.Shipto.FieldName = "Shipto"
        Me.Shipto.MinWidth = 21
        Me.Shipto.Name = "Shipto"
        Me.Shipto.OptionsColumn.AllowEdit = False
        Me.Shipto.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.Shipto.OptionsColumn.ReadOnly = True
        Me.Shipto.Visible = True
        Me.Shipto.VisibleIndex = 8
        Me.Shipto.Width = 77
        '
        'GarmentShipmentDestination
        '
        Me.GarmentShipmentDestination.Caption = "Garment Shipment Destination"
        Me.GarmentShipmentDestination.FieldName = "GarmentShipmentDestination"
        Me.GarmentShipmentDestination.MinWidth = 21
        Me.GarmentShipmentDestination.Name = "GarmentShipmentDestination"
        Me.GarmentShipmentDestination.OptionsColumn.AllowEdit = False
        Me.GarmentShipmentDestination.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.GarmentShipmentDestination.OptionsColumn.ReadOnly = True
        Me.GarmentShipmentDestination.Visible = True
        Me.GarmentShipmentDestination.VisibleIndex = 9
        Me.GarmentShipmentDestination.Width = 77
        '
        'MatrClass
        '
        Me.MatrClass.Caption = "Material Type"
        Me.MatrClass.FieldName = "MatrClass"
        Me.MatrClass.MinWidth = 21
        Me.MatrClass.Name = "MatrClass"
        Me.MatrClass.OptionsColumn.AllowEdit = False
        Me.MatrClass.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.MatrClass.OptionsColumn.ReadOnly = True
        Me.MatrClass.Visible = True
        Me.MatrClass.VisibleIndex = 10
        Me.MatrClass.Width = 77
        '
        'POItemCode
        '
        Me.POItemCode.Caption = "PO Item Code"
        Me.POItemCode.FieldName = "POItemCode"
        Me.POItemCode.MinWidth = 21
        Me.POItemCode.Name = "POItemCode"
        Me.POItemCode.OptionsColumn.AllowEdit = False
        Me.POItemCode.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.POItemCode.OptionsColumn.ReadOnly = True
        Me.POItemCode.Visible = True
        Me.POItemCode.VisibleIndex = 11
        Me.POItemCode.Width = 103
        '
        'MatrCode
        '
        Me.MatrCode.Caption = "Material Code"
        Me.MatrCode.FieldName = "MatrCode"
        Me.MatrCode.MinWidth = 21
        Me.MatrCode.Name = "MatrCode"
        Me.MatrCode.OptionsColumn.AllowEdit = False
        Me.MatrCode.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.MatrCode.OptionsColumn.ReadOnly = True
        Me.MatrCode.Visible = True
        Me.MatrCode.VisibleIndex = 12
        Me.MatrCode.Width = 103
        '
        'UPCCOMBOIM
        '
        Me.UPCCOMBOIM.Caption = "UPC(COMBO IM#)"
        Me.UPCCOMBOIM.FieldName = "UPCCOMBOIM"
        Me.UPCCOMBOIM.MinWidth = 21
        Me.UPCCOMBOIM.Name = "UPCCOMBOIM"
        Me.UPCCOMBOIM.OptionsColumn.AllowEdit = False
        Me.UPCCOMBOIM.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.UPCCOMBOIM.OptionsColumn.ReadOnly = True
        Me.UPCCOMBOIM.Width = 77
        '
        'ContentCode
        '
        Me.ContentCode.Caption = "Content Code"
        Me.ContentCode.FieldName = "ContentCode"
        Me.ContentCode.MinWidth = 21
        Me.ContentCode.Name = "ContentCode"
        Me.ContentCode.OptionsColumn.AllowEdit = False
        Me.ContentCode.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.ContentCode.OptionsColumn.ReadOnly = True
        Me.ContentCode.Width = 77
        '
        'CareCode
        '
        Me.CareCode.Caption = "Care Code"
        Me.CareCode.FieldName = "CareCode"
        Me.CareCode.MinWidth = 21
        Me.CareCode.Name = "CareCode"
        Me.CareCode.OptionsColumn.AllowEdit = False
        Me.CareCode.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.CareCode.OptionsColumn.ReadOnly = True
        Me.CareCode.Width = 77
        '
        'Color
        '
        Me.Color.Caption = "Color Code"
        Me.Color.FieldName = "Color"
        Me.Color.MinWidth = 21
        Me.Color.Name = "Color"
        Me.Color.OptionsColumn.AllowEdit = False
        Me.Color.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.Color.OptionsColumn.ReadOnly = True
        Me.Color.Visible = True
        Me.Color.VisibleIndex = 13
        Me.Color.Width = 86
        '
        'ColorName
        '
        Me.ColorName.Caption = "Color Name"
        Me.ColorName.FieldName = "ColorName"
        Me.ColorName.MinWidth = 21
        Me.ColorName.Name = "ColorName"
        Me.ColorName.OptionsColumn.AllowEdit = False
        Me.ColorName.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColorName.OptionsColumn.ReadOnly = True
        Me.ColorName.Visible = True
        Me.ColorName.VisibleIndex = 14
        Me.ColorName.Width = 171
        '
        'GCW
        '
        Me.GCW.Caption = "GCW"
        Me.GCW.FieldName = "GCW"
        Me.GCW.MinWidth = 21
        Me.GCW.Name = "GCW"
        Me.GCW.OptionsColumn.AllowEdit = False
        Me.GCW.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.GCW.OptionsColumn.ReadOnly = True
        Me.GCW.Visible = True
        Me.GCW.VisibleIndex = 15
        Me.GCW.Width = 77
        '
        'Size
        '
        Me.Size.Caption = "Size"
        Me.Size.FieldName = "Size"
        Me.Size.MinWidth = 21
        Me.Size.Name = "Size"
        Me.Size.OptionsColumn.AllowEdit = False
        Me.Size.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.Size.OptionsColumn.ReadOnly = True
        Me.Size.Visible = True
        Me.Size.VisibleIndex = 16
        Me.Size.Width = 77
        '
        'SizeMatrix
        '
        Me.SizeMatrix.Caption = "Size Matrix"
        Me.SizeMatrix.FieldName = "SizeMatrix"
        Me.SizeMatrix.MinWidth = 21
        Me.SizeMatrix.Name = "SizeMatrix"
        Me.SizeMatrix.OptionsColumn.AllowEdit = False
        Me.SizeMatrix.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.SizeMatrix.OptionsColumn.ReadOnly = True
        Me.SizeMatrix.Width = 77
        '
        'Currency
        '
        Me.Currency.Caption = "Currency"
        Me.Currency.FieldName = "Currency"
        Me.Currency.MinWidth = 21
        Me.Currency.Name = "Currency"
        Me.Currency.OptionsColumn.AllowEdit = False
        Me.Currency.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.Currency.OptionsColumn.ReadOnly = True
        Me.Currency.Visible = True
        Me.Currency.VisibleIndex = 17
        Me.Currency.Width = 77
        '
        'Price
        '
        Me.Price.Caption = "Price"
        Me.Price.DisplayFormat.FormatString = "{0:n5}"
        Me.Price.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.Price.FieldName = "Price"
        Me.Price.MinWidth = 21
        Me.Price.Name = "Price"
        Me.Price.OptionsColumn.AllowEdit = False
        Me.Price.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.Price.OptionsColumn.ReadOnly = True
        Me.Price.Visible = True
        Me.Price.VisibleIndex = 18
        Me.Price.Width = 77
        '
        'QtyUnit
        '
        Me.QtyUnit.Caption = "Unit"
        Me.QtyUnit.FieldName = "QtyUnit"
        Me.QtyUnit.MinWidth = 21
        Me.QtyUnit.Name = "QtyUnit"
        Me.QtyUnit.OptionsColumn.AllowEdit = False
        Me.QtyUnit.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.QtyUnit.OptionsColumn.ReadOnly = True
        Me.QtyUnit.Visible = True
        Me.QtyUnit.VisibleIndex = 19
        Me.QtyUnit.Width = 77
        '
        'DeliveryDate
        '
        Me.DeliveryDate.Caption = "FCTY Need Date"
        Me.DeliveryDate.FieldName = "DeliveryDate"
        Me.DeliveryDate.MinWidth = 21
        Me.DeliveryDate.Name = "DeliveryDate"
        Me.DeliveryDate.OptionsColumn.AllowEdit = False
        Me.DeliveryDate.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.DeliveryDate.OptionsColumn.ReadOnly = True
        Me.DeliveryDate.Visible = True
        Me.DeliveryDate.VisibleIndex = 20
        Me.DeliveryDate.Width = 77
        '
        'Season
        '
        Me.Season.Caption = "Season"
        Me.Season.FieldName = "Season"
        Me.Season.MinWidth = 21
        Me.Season.Name = "Season"
        Me.Season.OptionsColumn.AllowEdit = False
        Me.Season.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.Season.OptionsColumn.ReadOnly = True
        Me.Season.Visible = True
        Me.Season.VisibleIndex = 21
        Me.Season.Width = 77
        '
        'Custporef
        '
        Me.Custporef.Caption = "End Customer PO"
        Me.Custporef.FieldName = "Custporef"
        Me.Custporef.MinWidth = 21
        Me.Custporef.Name = "Custporef"
        Me.Custporef.OptionsColumn.AllowEdit = False
        Me.Custporef.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.Custporef.OptionsColumn.ReadOnly = True
        Me.Custporef.Visible = True
        Me.Custporef.VisibleIndex = 22
        Me.Custporef.Width = 77
        '
        'Buy
        '
        Me.Buy.Caption = "Buy Month"
        Me.Buy.FieldName = "Buy"
        Me.Buy.MinWidth = 21
        Me.Buy.Name = "Buy"
        Me.Buy.OptionsColumn.AllowEdit = False
        Me.Buy.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.Buy.OptionsColumn.ReadOnly = True
        Me.Buy.Visible = True
        Me.Buy.VisibleIndex = 23
        Me.Buy.Width = 77
        '
        'BuyNo
        '
        Me.BuyNo.Caption = "Buy Code"
        Me.BuyNo.FieldName = "BuyNo"
        Me.BuyNo.MinWidth = 21
        Me.BuyNo.Name = "BuyNo"
        Me.BuyNo.OptionsColumn.AllowEdit = False
        Me.BuyNo.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.BuyNo.OptionsColumn.ReadOnly = True
        Me.BuyNo.Visible = True
        Me.BuyNo.VisibleIndex = 24
        Me.BuyNo.Width = 77
        '
        'Category
        '
        Me.Category.Caption = "Category"
        Me.Category.FieldName = "Category"
        Me.Category.MinWidth = 21
        Me.Category.Name = "Category"
        Me.Category.OptionsColumn.AllowEdit = False
        Me.Category.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.Category.OptionsColumn.ReadOnly = True
        Me.Category.Visible = True
        Me.Category.VisibleIndex = 25
        Me.Category.Width = 77
        '
        'Program
        '
        Me.Program.Caption = "Buy Group"
        Me.Program.FieldName = "Program"
        Me.Program.MinWidth = 21
        Me.Program.Name = "Program"
        Me.Program.OptionsColumn.AllowEdit = False
        Me.Program.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.Program.OptionsColumn.ReadOnly = True
        Me.Program.Visible = True
        Me.Program.VisibleIndex = 26
        Me.Program.Width = 77
        '
        'SubProgram
        '
        Me.SubProgram.Caption = "Sub Program"
        Me.SubProgram.FieldName = "SubProgram"
        Me.SubProgram.MinWidth = 21
        Me.SubProgram.Name = "SubProgram"
        Me.SubProgram.OptionsColumn.AllowEdit = False
        Me.SubProgram.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.SubProgram.OptionsColumn.ReadOnly = True
        Me.SubProgram.Visible = True
        Me.SubProgram.VisibleIndex = 27
        Me.SubProgram.Width = 77
        '
        'StyleNo
        '
        Me.StyleNo.Caption = "Style No"
        Me.StyleNo.FieldName = "StyleNo"
        Me.StyleNo.MinWidth = 21
        Me.StyleNo.Name = "StyleNo"
        Me.StyleNo.OptionsColumn.AllowEdit = False
        Me.StyleNo.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.StyleNo.OptionsColumn.ReadOnly = True
        Me.StyleNo.Visible = True
        Me.StyleNo.VisibleIndex = 28
        Me.StyleNo.Width = 77
        '
        'StyleName
        '
        Me.StyleName.Caption = "Style Name"
        Me.StyleName.FieldName = "StyleName"
        Me.StyleName.MinWidth = 21
        Me.StyleName.Name = "StyleName"
        Me.StyleName.OptionsColumn.AllowEdit = False
        Me.StyleName.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.StyleName.OptionsColumn.ReadOnly = True
        Me.StyleName.Visible = True
        Me.StyleName.VisibleIndex = 29
        Me.StyleName.Width = 77
        '
        'PORefType
        '
        Me.PORefType.Caption = "Ref Order Type"
        Me.PORefType.FieldName = "PORefType"
        Me.PORefType.MinWidth = 21
        Me.PORefType.Name = "PORefType"
        Me.PORefType.OptionsColumn.AllowEdit = False
        Me.PORefType.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.PORefType.OptionsColumn.ReadOnly = True
        Me.PORefType.Visible = True
        Me.PORefType.VisibleIndex = 30
        Me.PORefType.Width = 77
        '
        'POMatching1
        '
        Me.POMatching1.Caption = "PO Matching 1"
        Me.POMatching1.FieldName = "POMatching1"
        Me.POMatching1.MinWidth = 21
        Me.POMatching1.Name = "POMatching1"
        Me.POMatching1.OptionsColumn.AllowEdit = False
        Me.POMatching1.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.POMatching1.OptionsColumn.ReadOnly = True
        Me.POMatching1.Visible = True
        Me.POMatching1.VisibleIndex = 31
        Me.POMatching1.Width = 77
        '
        'POMatching2
        '
        Me.POMatching2.Caption = "PO Matching 2"
        Me.POMatching2.FieldName = "POMatching2"
        Me.POMatching2.MinWidth = 21
        Me.POMatching2.Name = "POMatching2"
        Me.POMatching2.OptionsColumn.AllowEdit = False
        Me.POMatching2.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.POMatching2.OptionsColumn.ReadOnly = True
        Me.POMatching2.Visible = True
        Me.POMatching2.VisibleIndex = 32
        Me.POMatching2.Width = 77
        '
        'POMatching3
        '
        Me.POMatching3.Caption = "PO Matching 3"
        Me.POMatching3.FieldName = "POMatching3"
        Me.POMatching3.MinWidth = 21
        Me.POMatching3.Name = "POMatching3"
        Me.POMatching3.OptionsColumn.AllowEdit = False
        Me.POMatching3.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.POMatching3.OptionsColumn.ReadOnly = True
        Me.POMatching3.Visible = True
        Me.POMatching3.VisibleIndex = 33
        Me.POMatching3.Width = 77
        '
        'POMatching4
        '
        Me.POMatching4.Caption = "PO Matching 4"
        Me.POMatching4.FieldName = "POMatching4"
        Me.POMatching4.MinWidth = 21
        Me.POMatching4.Name = "POMatching4"
        Me.POMatching4.OptionsColumn.AllowEdit = False
        Me.POMatching4.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.POMatching4.OptionsColumn.ReadOnly = True
        Me.POMatching4.Visible = True
        Me.POMatching4.VisibleIndex = 34
        Me.POMatching4.Width = 77
        '
        'POMatching5
        '
        Me.POMatching5.Caption = "PO Matching 5"
        Me.POMatching5.FieldName = "POMatching5"
        Me.POMatching5.MinWidth = 21
        Me.POMatching5.Name = "POMatching5"
        Me.POMatching5.OptionsColumn.AllowEdit = False
        Me.POMatching5.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.POMatching5.OptionsColumn.ReadOnly = True
        Me.POMatching5.Visible = True
        Me.POMatching5.VisibleIndex = 35
        Me.POMatching5.Width = 77
        '
        'ItemMatching1
        '
        Me.ItemMatching1.Caption = "Item Matching 1"
        Me.ItemMatching1.FieldName = "ItemMatching1"
        Me.ItemMatching1.MinWidth = 21
        Me.ItemMatching1.Name = "ItemMatching1"
        Me.ItemMatching1.OptionsColumn.AllowEdit = False
        Me.ItemMatching1.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.ItemMatching1.OptionsColumn.ReadOnly = True
        Me.ItemMatching1.Visible = True
        Me.ItemMatching1.VisibleIndex = 36
        Me.ItemMatching1.Width = 77
        '
        'ItemMatching2
        '
        Me.ItemMatching2.Caption = "Item Matching 2"
        Me.ItemMatching2.FieldName = "ItemMatching2"
        Me.ItemMatching2.MinWidth = 21
        Me.ItemMatching2.Name = "ItemMatching2"
        Me.ItemMatching2.OptionsColumn.AllowEdit = False
        Me.ItemMatching2.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.ItemMatching2.OptionsColumn.ReadOnly = True
        Me.ItemMatching2.Visible = True
        Me.ItemMatching2.VisibleIndex = 37
        Me.ItemMatching2.Width = 77
        '
        'ItemMatching3
        '
        Me.ItemMatching3.Caption = "Item Matching 3"
        Me.ItemMatching3.FieldName = "ItemMatching3"
        Me.ItemMatching3.MinWidth = 21
        Me.ItemMatching3.Name = "ItemMatching3"
        Me.ItemMatching3.OptionsColumn.AllowEdit = False
        Me.ItemMatching3.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.ItemMatching3.OptionsColumn.ReadOnly = True
        Me.ItemMatching3.Visible = True
        Me.ItemMatching3.VisibleIndex = 38
        Me.ItemMatching3.Width = 77
        '
        'ItemMatching4
        '
        Me.ItemMatching4.Caption = "Item Matching 4"
        Me.ItemMatching4.FieldName = "ItemMatching4"
        Me.ItemMatching4.MinWidth = 21
        Me.ItemMatching4.Name = "ItemMatching4"
        Me.ItemMatching4.OptionsColumn.AllowEdit = False
        Me.ItemMatching4.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.ItemMatching4.OptionsColumn.ReadOnly = True
        Me.ItemMatching4.Visible = True
        Me.ItemMatching4.VisibleIndex = 39
        Me.ItemMatching4.Width = 77
        '
        'ItemMatching5
        '
        Me.ItemMatching5.Caption = "Item Matching 5"
        Me.ItemMatching5.FieldName = "ItemMatching5"
        Me.ItemMatching5.MinWidth = 21
        Me.ItemMatching5.Name = "ItemMatching5"
        Me.ItemMatching5.OptionsColumn.AllowEdit = False
        Me.ItemMatching5.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.ItemMatching5.OptionsColumn.ReadOnly = True
        Me.ItemMatching5.Visible = True
        Me.ItemMatching5.VisibleIndex = 40
        Me.ItemMatching5.Width = 77
        '
        'Position
        '
        Me.Position.Caption = "Position"
        Me.Position.FieldName = "Position"
        Me.Position.MinWidth = 21
        Me.Position.Name = "Position"
        Me.Position.OptionsColumn.AllowEdit = False
        Me.Position.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.Position.OptionsColumn.ReadOnly = True
        Me.Position.Visible = True
        Me.Position.VisibleIndex = 41
        Me.Position.Width = 77
        '
        'Type
        '
        Me.Type.Caption = "Type"
        Me.Type.FieldName = "Type"
        Me.Type.MinWidth = 21
        Me.Type.Name = "Type"
        Me.Type.OptionsColumn.AllowEdit = False
        Me.Type.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.Type.OptionsColumn.ReadOnly = True
        Me.Type.Visible = True
        Me.Type.VisibleIndex = 42
        Me.Type.Width = 77
        '
        'PaymentTerm
        '
        Me.PaymentTerm.Caption = "Payment Term"
        Me.PaymentTerm.FieldName = "PaymentTerm"
        Me.PaymentTerm.MinWidth = 21
        Me.PaymentTerm.Name = "PaymentTerm"
        Me.PaymentTerm.OptionsColumn.AllowEdit = False
        Me.PaymentTerm.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.PaymentTerm.OptionsColumn.ReadOnly = True
        Me.PaymentTerm.Visible = True
        Me.PaymentTerm.VisibleIndex = 43
        Me.PaymentTerm.Width = 77
        '
        'Remarkfrommer
        '
        Me.Remarkfrommer.Caption = "Remark From Mer"
        Me.Remarkfrommer.FieldName = "Remarkfrommer"
        Me.Remarkfrommer.MinWidth = 21
        Me.Remarkfrommer.Name = "Remarkfrommer"
        Me.Remarkfrommer.OptionsColumn.AllowEdit = False
        Me.Remarkfrommer.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.Remarkfrommer.OptionsColumn.ReadOnly = True
        Me.Remarkfrommer.Visible = True
        Me.Remarkfrommer.VisibleIndex = 44
        Me.Remarkfrommer.Width = 77
        '
        'RemarkForPurchase
        '
        Me.RemarkForPurchase.Caption = "Remark For Purchase"
        Me.RemarkForPurchase.FieldName = "RemarkForPurchase"
        Me.RemarkForPurchase.MinWidth = 21
        Me.RemarkForPurchase.Name = "RemarkForPurchase"
        Me.RemarkForPurchase.OptionsColumn.AllowEdit = False
        Me.RemarkForPurchase.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.RemarkForPurchase.OptionsColumn.ReadOnly = True
        Me.RemarkForPurchase.Visible = True
        Me.RemarkForPurchase.VisibleIndex = 45
        Me.RemarkForPurchase.Width = 77
        '
        'CompanyName
        '
        Me.CompanyName.Caption = "Company Name"
        Me.CompanyName.FieldName = "CompanyName"
        Me.CompanyName.MinWidth = 21
        Me.CompanyName.Name = "CompanyName"
        Me.CompanyName.OptionsColumn.AllowEdit = False
        Me.CompanyName.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.CompanyName.OptionsColumn.ReadOnly = True
        Me.CompanyName.Visible = True
        Me.CompanyName.VisibleIndex = 46
        Me.CompanyName.Width = 77
        '
        'address1
        '
        Me.address1.Caption = "Address 1"
        Me.address1.FieldName = "address1"
        Me.address1.MinWidth = 21
        Me.address1.Name = "address1"
        Me.address1.OptionsColumn.AllowEdit = False
        Me.address1.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.address1.OptionsColumn.ReadOnly = True
        Me.address1.Visible = True
        Me.address1.VisibleIndex = 47
        Me.address1.Width = 77
        '
        'address2
        '
        Me.address2.Caption = "Address 2"
        Me.address2.FieldName = "address2"
        Me.address2.MinWidth = 21
        Me.address2.Name = "address2"
        Me.address2.OptionsColumn.AllowEdit = False
        Me.address2.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.address2.OptionsColumn.ReadOnly = True
        Me.address2.Visible = True
        Me.address2.VisibleIndex = 48
        Me.address2.Width = 77
        '
        'address3
        '
        Me.address3.Caption = "Address 3"
        Me.address3.FieldName = "address3"
        Me.address3.MinWidth = 21
        Me.address3.Name = "address3"
        Me.address3.OptionsColumn.AllowEdit = False
        Me.address3.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.address3.OptionsColumn.ReadOnly = True
        Me.address3.Visible = True
        Me.address3.VisibleIndex = 49
        Me.address3.Width = 77
        '
        'address4
        '
        Me.address4.Caption = "Address 4"
        Me.address4.FieldName = "address4"
        Me.address4.MinWidth = 21
        Me.address4.Name = "address4"
        Me.address4.OptionsColumn.AllowEdit = False
        Me.address4.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.address4.OptionsColumn.ReadOnly = True
        Me.address4.Visible = True
        Me.address4.VisibleIndex = 50
        Me.address4.Width = 77
        '
        'sysowner
        '
        Me.sysowner.Caption = "Purchaser"
        Me.sysowner.FieldName = "sysowner"
        Me.sysowner.MinWidth = 21
        Me.sysowner.Name = "sysowner"
        Me.sysowner.OptionsColumn.AllowEdit = False
        Me.sysowner.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.sysowner.OptionsColumn.ReadOnly = True
        Me.sysowner.Visible = True
        Me.sysowner.VisibleIndex = 51
        Me.sysowner.Width = 77
        '
        'sysownername
        '
        Me.sysownername.Caption = "Purchaser Name"
        Me.sysownername.FieldName = "sysownername"
        Me.sysownername.MinWidth = 21
        Me.sysownername.Name = "sysownername"
        Me.sysownername.OptionsColumn.AllowEdit = False
        Me.sysownername.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.sysownername.OptionsColumn.ReadOnly = True
        Me.sysownername.Visible = True
        Me.sysownername.VisibleIndex = 52
        Me.sysownername.Width = 77
        '
        'sysownermail
        '
        Me.sysownermail.Caption = "Purchaser E-Mail"
        Me.sysownermail.FieldName = "sysownermail"
        Me.sysownermail.MinWidth = 21
        Me.sysownermail.Name = "sysownermail"
        Me.sysownermail.OptionsColumn.AllowEdit = False
        Me.sysownermail.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.sysownermail.OptionsColumn.ReadOnly = True
        Me.sysownermail.Visible = True
        Me.sysownermail.VisibleIndex = 53
        Me.sysownermail.Width = 77
        '
        'ZeroInspection
        '
        Me.ZeroInspection.Caption = "Zero Inspection"
        Me.ZeroInspection.FieldName = "ZeroInspection"
        Me.ZeroInspection.MinWidth = 21
        Me.ZeroInspection.Name = "ZeroInspection"
        Me.ZeroInspection.OptionsColumn.AllowEdit = False
        Me.ZeroInspection.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.ZeroInspection.OptionsColumn.ReadOnly = True
        Me.ZeroInspection.Visible = True
        Me.ZeroInspection.VisibleIndex = 54
        Me.ZeroInspection.Width = 77
        '
        'GarmentShip
        '
        Me.GarmentShip.Caption = "GAC Date"
        Me.GarmentShip.FieldName = "GarmentShip"
        Me.GarmentShip.MinWidth = 21
        Me.GarmentShip.Name = "GarmentShip"
        Me.GarmentShip.OptionsColumn.AllowEdit = False
        Me.GarmentShip.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.GarmentShip.OptionsColumn.ReadOnly = True
        Me.GarmentShip.Visible = True
        Me.GarmentShip.VisibleIndex = 55
        Me.GarmentShip.Width = 77
        '
        'OGACDate
        '
        Me.OGACDate.Caption = "OGAC Date(MM/YYYY)"
        Me.OGACDate.FieldName = "OGACDate"
        Me.OGACDate.MinWidth = 21
        Me.OGACDate.Name = "OGACDate"
        Me.OGACDate.OptionsColumn.AllowEdit = False
        Me.OGACDate.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.OGACDate.OptionsColumn.ReadOnly = True
        Me.OGACDate.Width = 77
        '
        'HITLink
        '
        Me.HITLink.Caption = "HIT Job No"
        Me.HITLink.FieldName = "HITLink"
        Me.HITLink.MinWidth = 21
        Me.HITLink.Name = "HITLink"
        Me.HITLink.OptionsColumn.AllowEdit = False
        Me.HITLink.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.HITLink.OptionsColumn.ReadOnly = True
        Me.HITLink.Visible = True
        Me.HITLink.VisibleIndex = 56
        Me.HITLink.Width = 77
        '
        'NIKECustomerPONo
        '
        Me.NIKECustomerPONo.Caption = "NIKE Sales Order No"
        Me.NIKECustomerPONo.FieldName = "NIKECustomerPONo"
        Me.NIKECustomerPONo.MinWidth = 21
        Me.NIKECustomerPONo.Name = "NIKECustomerPONo"
        Me.NIKECustomerPONo.OptionsColumn.AllowEdit = False
        Me.NIKECustomerPONo.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.NIKECustomerPONo.OptionsColumn.ReadOnly = True
        Me.NIKECustomerPONo.Visible = True
        Me.NIKECustomerPONo.VisibleIndex = 57
        Me.NIKECustomerPONo.Width = 77
        '
        'QRS
        '
        Me.QRS.Caption = "QPP/QRS Quantity"
        Me.QRS.DisplayFormat.FormatString = "{0:n4}"
        Me.QRS.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.QRS.FieldName = "QRS"
        Me.QRS.MinWidth = 21
        Me.QRS.Name = "QRS"
        Me.QRS.OptionsColumn.AllowEdit = False
        Me.QRS.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.QRS.OptionsColumn.ReadOnly = True
        Me.QRS.Visible = True
        Me.QRS.VisibleIndex = 58
        Me.QRS.Width = 77
        '
        'PromoQty
        '
        Me.PromoQty.Caption = "Promo Quantity"
        Me.PromoQty.DisplayFormat.FormatString = "{0:n4}"
        Me.PromoQty.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.PromoQty.FieldName = "PromoQty"
        Me.PromoQty.MinWidth = 21
        Me.PromoQty.Name = "PromoQty"
        Me.PromoQty.OptionsColumn.AllowEdit = False
        Me.PromoQty.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.PromoQty.OptionsColumn.ReadOnly = True
        Me.PromoQty.Visible = True
        Me.PromoQty.VisibleIndex = 59
        Me.PromoQty.Width = 77
        '
        'MOQ
        '
        Me.MOQ.Caption = "MOQ Quantity"
        Me.MOQ.DisplayFormat.FormatString = "{0:n4}"
        Me.MOQ.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.MOQ.FieldName = "MOQ"
        Me.MOQ.MinWidth = 21
        Me.MOQ.Name = "MOQ"
        Me.MOQ.OptionsColumn.AllowEdit = False
        Me.MOQ.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.MOQ.OptionsColumn.ReadOnly = True
        Me.MOQ.Visible = True
        Me.MOQ.VisibleIndex = 60
        Me.MOQ.Width = 77
        '
        'ActualQuantity
        '
        Me.ActualQuantity.Caption = "Bulk Quantity"
        Me.ActualQuantity.DisplayFormat.FormatString = "{0:n4}"
        Me.ActualQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.ActualQuantity.FieldName = "ActualQuantity"
        Me.ActualQuantity.MinWidth = 21
        Me.ActualQuantity.Name = "ActualQuantity"
        Me.ActualQuantity.OptionsColumn.AllowEdit = False
        Me.ActualQuantity.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.ActualQuantity.OptionsColumn.ReadOnly = True
        Me.ActualQuantity.Visible = True
        Me.ActualQuantity.VisibleIndex = 61
        Me.ActualQuantity.Width = 77
        '
        'Quantity
        '
        Me.Quantity.Caption = "Total PO Quantity"
        Me.Quantity.DisplayFormat.FormatString = "{0:n4}"
        Me.Quantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.Quantity.FieldName = "Quantity"
        Me.Quantity.MinWidth = 21
        Me.Quantity.Name = "Quantity"
        Me.Quantity.OptionsColumn.AllowEdit = False
        Me.Quantity.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.Quantity.OptionsColumn.ReadOnly = True
        Me.Quantity.Visible = True
        Me.Quantity.VisibleIndex = 62
        Me.Quantity.Width = 77
        '
        'POUploadDate
        '
        Me.POUploadDate.Caption = "PO Upload Date"
        Me.POUploadDate.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.POUploadDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.POUploadDate.FieldName = "POUploadDate"
        Me.POUploadDate.MinWidth = 21
        Me.POUploadDate.Name = "POUploadDate"
        Me.POUploadDate.OptionsColumn.AllowEdit = False
        Me.POUploadDate.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.POUploadDate.OptionsColumn.ReadOnly = True
        Me.POUploadDate.Visible = True
        Me.POUploadDate.VisibleIndex = 63
        Me.POUploadDate.Width = 77
        '
        'POUploadTime
        '
        Me.POUploadTime.Caption = "Upload Time"
        Me.POUploadTime.FieldName = "POUploadTime"
        Me.POUploadTime.MinWidth = 21
        Me.POUploadTime.Name = "POUploadTime"
        Me.POUploadTime.OptionsColumn.AllowEdit = False
        Me.POUploadTime.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.POUploadTime.OptionsColumn.ReadOnly = True
        Me.POUploadTime.Visible = True
        Me.POUploadTime.VisibleIndex = 64
        Me.POUploadTime.Width = 77
        '
        'POUploadBy
        '
        Me.POUploadBy.Caption = "Upload By"
        Me.POUploadBy.FieldName = "POUploadBy"
        Me.POUploadBy.MinWidth = 21
        Me.POUploadBy.Name = "POUploadBy"
        Me.POUploadBy.OptionsColumn.AllowEdit = False
        Me.POUploadBy.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.POUploadBy.OptionsColumn.ReadOnly = True
        Me.POUploadBy.Visible = True
        Me.POUploadBy.VisibleIndex = 65
        Me.POUploadBy.Width = 77
        '
        'CountryOfOrigin
        '
        Me.CountryOfOrigin.Caption = "Country Of Origin"
        Me.CountryOfOrigin.FieldName = "CountryOfOrigin"
        Me.CountryOfOrigin.MinWidth = 21
        Me.CountryOfOrigin.Name = "CountryOfOrigin"
        Me.CountryOfOrigin.OptionsColumn.AllowEdit = False
        Me.CountryOfOrigin.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.CountryOfOrigin.OptionsColumn.ReadOnly = True
        Me.CountryOfOrigin.Width = 77
        '
        'SaleOrderSaleOrderLine
        '
        Me.SaleOrderSaleOrderLine.Caption = "Sale Order - Sale Order Line"
        Me.SaleOrderSaleOrderLine.FieldName = "SaleOrderSaleOrderLine"
        Me.SaleOrderSaleOrderLine.MinWidth = 21
        Me.SaleOrderSaleOrderLine.Name = "SaleOrderSaleOrderLine"
        Me.SaleOrderSaleOrderLine.OptionsColumn.AllowEdit = False
        Me.SaleOrderSaleOrderLine.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.SaleOrderSaleOrderLine.OptionsColumn.ReadOnly = True
        Me.SaleOrderSaleOrderLine.Width = 77
        '
        'NikeSAPPOPONIKEPOLine
        '
        Me.NikeSAPPOPONIKEPOLine.Caption = "Nike SAPPO PONIKE PO-Line"
        Me.NikeSAPPOPONIKEPOLine.FieldName = "NikeSAPPOPONIKEPOLine"
        Me.NikeSAPPOPONIKEPOLine.MinWidth = 21
        Me.NikeSAPPOPONIKEPOLine.Name = "NikeSAPPOPONIKEPOLine"
        Me.NikeSAPPOPONIKEPOLine.OptionsColumn.AllowEdit = False
        Me.NikeSAPPOPONIKEPOLine.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.NikeSAPPOPONIKEPOLine.OptionsColumn.ReadOnly = True
        Me.NikeSAPPOPONIKEPOLine.Width = 77
        '
        'NikematerialStyleColorway
        '
        Me.NikematerialStyleColorway.Caption = "Style-Colorway"
        Me.NikematerialStyleColorway.FieldName = "NikematerialStyleColorway"
        Me.NikematerialStyleColorway.MinWidth = 21
        Me.NikematerialStyleColorway.Name = "NikematerialStyleColorway"
        Me.NikematerialStyleColorway.OptionsColumn.AllowEdit = False
        Me.NikematerialStyleColorway.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.NikematerialStyleColorway.OptionsColumn.ReadOnly = True
        Me.NikematerialStyleColorway.Width = 77
        '
        'Modifire1
        '
        Me.Modifire1.Caption = "Modifire 1"
        Me.Modifire1.FieldName = "Modifire1"
        Me.Modifire1.MinWidth = 21
        Me.Modifire1.Name = "Modifire1"
        Me.Modifire1.OptionsColumn.AllowEdit = False
        Me.Modifire1.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.Modifire1.OptionsColumn.ReadOnly = True
        Me.Modifire1.Width = 77
        '
        'Modifire2
        '
        Me.Modifire2.Caption = "Modifire 2"
        Me.Modifire2.FieldName = "Modifire2"
        Me.Modifire2.MinWidth = 21
        Me.Modifire2.Name = "Modifire2"
        Me.Modifire2.OptionsColumn.AllowEdit = False
        Me.Modifire2.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.Modifire2.OptionsColumn.ReadOnly = True
        Me.Modifire2.Width = 77
        '
        'Modifire3
        '
        Me.Modifire3.Caption = "Modifire 3"
        Me.Modifire3.FieldName = "Modifire3"
        Me.Modifire3.MinWidth = 21
        Me.Modifire3.Name = "Modifire3"
        Me.Modifire3.OptionsColumn.AllowEdit = False
        Me.Modifire3.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.Modifire3.OptionsColumn.ReadOnly = True
        Me.Modifire3.Width = 77
        '
        'MPOHZ
        '
        Me.MPOHZ.Caption = "MPOHZ"
        Me.MPOHZ.FieldName = "MPOHZ"
        Me.MPOHZ.MinWidth = 21
        Me.MPOHZ.Name = "MPOHZ"
        Me.MPOHZ.OptionsColumn.AllowEdit = False
        Me.MPOHZ.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.MPOHZ.OptionsColumn.ReadOnly = True
        Me.MPOHZ.Width = 77
        '
        'ItemDescription
        '
        Me.ItemDescription.Caption = "Item Description"
        Me.ItemDescription.FieldName = "ItemDescription"
        Me.ItemDescription.MinWidth = 21
        Me.ItemDescription.Name = "ItemDescription"
        Me.ItemDescription.OptionsColumn.AllowEdit = False
        Me.ItemDescription.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.ItemDescription.OptionsColumn.ReadOnly = True
        Me.ItemDescription.Visible = True
        Me.ItemDescription.VisibleIndex = 66
        Me.ItemDescription.Width = 77
        '
        'BulkQRSSample
        '
        Me.BulkQRSSample.Caption = "Bulk/Sample (Care Label)"
        Me.BulkQRSSample.FieldName = "BulkQRSSample"
        Me.BulkQRSSample.MinWidth = 21
        Me.BulkQRSSample.Name = "BulkQRSSample"
        Me.BulkQRSSample.OptionsColumn.AllowEdit = False
        Me.BulkQRSSample.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.BulkQRSSample.OptionsColumn.ReadOnly = True
        Me.BulkQRSSample.Width = 77
        '
        'CCTotalpage
        '
        Me.CCTotalpage.Caption = "CC Total page"""
        Me.CCTotalpage.FieldName = "CCTotalpage"
        Me.CCTotalpage.MinWidth = 21
        Me.CCTotalpage.Name = "CCTotalpage"
        Me.CCTotalpage.OptionsColumn.AllowEdit = False
        Me.CCTotalpage.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.CCTotalpage.OptionsColumn.ReadOnly = True
        Me.CCTotalpage.Width = 77
        '
        'Surcharge
        '
        Me.Surcharge.Caption = "Surcharge"
        Me.Surcharge.DisplayFormat.FormatString = "{0:n5}"
        Me.Surcharge.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.Surcharge.FieldName = "Surcharge"
        Me.Surcharge.MinWidth = 21
        Me.Surcharge.Name = "Surcharge"
        Me.Surcharge.OptionsColumn.AllowEdit = False
        Me.Surcharge.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.Surcharge.OptionsColumn.ReadOnly = True
        Me.Surcharge.Visible = True
        Me.Surcharge.VisibleIndex = 67
        Me.Surcharge.Width = 77
        '
        'ChinaInsertCard
        '
        Me.ChinaInsertCard.Caption = "China Insert Card (Trimco)"
        Me.ChinaInsertCard.FieldName = "ChinaInsertCard"
        Me.ChinaInsertCard.MinWidth = 21
        Me.ChinaInsertCard.Name = "ChinaInsertCard"
        Me.ChinaInsertCard.OptionsColumn.AllowEdit = False
        Me.ChinaInsertCard.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.ChinaInsertCard.OptionsColumn.ReadOnly = True
        Me.ChinaInsertCard.Width = 77
        '
        'P1pc2in1
        '
        Me.P1pc2in1.Caption = "P1PC 2in1"
        Me.P1pc2in1.FieldName = "P1pc2in1"
        Me.P1pc2in1.MinWidth = 21
        Me.P1pc2in1.Name = "P1pc2in1"
        Me.P1pc2in1.OptionsColumn.AllowEdit = False
        Me.P1pc2in1.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.P1pc2in1.OptionsColumn.ReadOnly = True
        Me.P1pc2in1.Width = 77
        '
        'WovenLabelSizeLength
        '
        Me.WovenLabelSizeLength.Caption = "Woven Label Size Length"
        Me.WovenLabelSizeLength.FieldName = "WovenLabelSizeLength"
        Me.WovenLabelSizeLength.MinWidth = 21
        Me.WovenLabelSizeLength.Name = "WovenLabelSizeLength"
        Me.WovenLabelSizeLength.OptionsColumn.AllowEdit = False
        Me.WovenLabelSizeLength.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.WovenLabelSizeLength.OptionsColumn.ReadOnly = True
        Me.WovenLabelSizeLength.Width = 77
        '
        'ArgentinaImportNumber
        '
        Me.ArgentinaImportNumber.Caption = "Argentina Import Number"
        Me.ArgentinaImportNumber.FieldName = "ArgentinaImportNumber"
        Me.ArgentinaImportNumber.MinWidth = 21
        Me.ArgentinaImportNumber.Name = "ArgentinaImportNumber"
        Me.ArgentinaImportNumber.OptionsColumn.AllowEdit = False
        Me.ArgentinaImportNumber.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.ArgentinaImportNumber.OptionsColumn.ReadOnly = True
        Me.ArgentinaImportNumber.Width = 77
        '
        'DownFill
        '
        Me.DownFill.Caption = "Down Fill"
        Me.DownFill.FieldName = "DownFill"
        Me.DownFill.MinWidth = 21
        Me.DownFill.Name = "DownFill"
        Me.DownFill.OptionsColumn.AllowEdit = False
        Me.DownFill.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.DownFill.OptionsColumn.ReadOnly = True
        Me.DownFill.Visible = True
        Me.DownFill.VisibleIndex = 68
        Me.DownFill.Width = 77
        '
        'SYS_ID
        '
        Me.SYS_ID.Caption = "SYS_ID"
        Me.SYS_ID.FieldName = "SYS_ID"
        Me.SYS_ID.MinWidth = 21
        Me.SYS_ID.Name = "SYS_ID"
        Me.SYS_ID.OptionsColumn.AllowEdit = False
        Me.SYS_ID.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.SYS_ID.OptionsColumn.ReadOnly = True
        Me.SYS_ID.Width = 77
        '
        'ChinasizeMatrixtype
        '
        Me.ChinasizeMatrixtype.Caption = "China size Matrix type"
        Me.ChinasizeMatrixtype.FieldName = "ChinasizeMatrixtype"
        Me.ChinasizeMatrixtype.MinWidth = 21
        Me.ChinasizeMatrixtype.Name = "ChinasizeMatrixtype"
        Me.ChinasizeMatrixtype.OptionsColumn.AllowEdit = False
        Me.ChinasizeMatrixtype.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.ChinasizeMatrixtype.OptionsColumn.ReadOnly = True
        Me.ChinasizeMatrixtype.Width = 77
        '
        'EAGERPItemNumber
        '
        Me.EAGERPItemNumber.Caption = "EAGERP Item Number"
        Me.EAGERPItemNumber.FieldName = "EAGERPItemNumber"
        Me.EAGERPItemNumber.MinWidth = 21
        Me.EAGERPItemNumber.Name = "EAGERPItemNumber"
        Me.EAGERPItemNumber.OptionsColumn.AllowEdit = False
        Me.EAGERPItemNumber.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.EAGERPItemNumber.OptionsColumn.ReadOnly = True
        Me.EAGERPItemNumber.Width = 77
        '
        'CompoundColororCCIMfor2in1CCIM
        '
        Me.CompoundColororCCIMfor2in1CCIM.Caption = "Compound Coloror CCI Mfor2in1CCIM"""
        Me.CompoundColororCCIMfor2in1CCIM.FieldName = "CompoundColororCCIMfor2in1CCIM"
        Me.CompoundColororCCIMfor2in1CCIM.MinWidth = 21
        Me.CompoundColororCCIMfor2in1CCIM.Name = "CompoundColororCCIMfor2in1CCIM"
        Me.CompoundColororCCIMfor2in1CCIM.OptionsColumn.AllowEdit = False
        Me.CompoundColororCCIMfor2in1CCIM.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.CompoundColororCCIMfor2in1CCIM.OptionsColumn.ReadOnly = True
        Me.CompoundColororCCIMfor2in1CCIM.Width = 77
        '
        'CPO
        '
        Me.CPO.Caption = "CPO"
        Me.CPO.FieldName = "CPO"
        Me.CPO.MinWidth = 21
        Me.CPO.Name = "CPO"
        Me.CPO.OptionsColumn.AllowEdit = False
        Me.CPO.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.CPO.OptionsColumn.ReadOnly = True
        Me.CPO.Width = 77
        '
        'BhasaIndonesiaProductBrand
        '
        Me.BhasaIndonesiaProductBrand.Caption = "Bhasa Indonesia Product Brand"
        Me.BhasaIndonesiaProductBrand.FieldName = "BhasaIndonesiaProductBrand"
        Me.BhasaIndonesiaProductBrand.MinWidth = 21
        Me.BhasaIndonesiaProductBrand.Name = "BhasaIndonesiaProductBrand"
        Me.BhasaIndonesiaProductBrand.OptionsColumn.AllowEdit = False
        Me.BhasaIndonesiaProductBrand.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.BhasaIndonesiaProductBrand.OptionsColumn.ReadOnly = True
        Me.BhasaIndonesiaProductBrand.Width = 77
        '
        'SAFCODE
        '
        Me.SAFCODE.Caption = "SAFCODE"
        Me.SAFCODE.FieldName = "SAFCODE"
        Me.SAFCODE.MinWidth = 21
        Me.SAFCODE.Name = "SAFCODE"
        Me.SAFCODE.OptionsColumn.AllowEdit = False
        Me.SAFCODE.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.SAFCODE.OptionsColumn.ReadOnly = True
        Me.SAFCODE.Width = 77
        '
        'Youthorder
        '
        Me.Youthorder.Caption = "Youth order"
        Me.Youthorder.FieldName = "Youthorder"
        Me.Youthorder.MinWidth = 21
        Me.Youthorder.Name = "Youthorder"
        Me.Youthorder.OptionsColumn.AllowEdit = False
        Me.Youthorder.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.Youthorder.OptionsColumn.ReadOnly = True
        Me.Youthorder.Width = 77
        '
        'NFCproduct
        '
        Me.NFCproduct.Caption = "NFC Product"
        Me.NFCproduct.FieldName = "NFCproduct"
        Me.NFCproduct.MinWidth = 21
        Me.NFCproduct.Name = "NFCproduct"
        Me.NFCproduct.OptionsColumn.AllowEdit = False
        Me.NFCproduct.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.NFCproduct.OptionsColumn.ReadOnly = True
        Me.NFCproduct.Width = 77
        '
        'NeckneckopeningX2
        '
        Me.NeckneckopeningX2.Caption = "Neck neck opening X2"
        Me.NeckneckopeningX2.FieldName = "NeckneckopeningX2"
        Me.NeckneckopeningX2.MinWidth = 21
        Me.NeckneckopeningX2.Name = "NeckneckopeningX2"
        Me.NeckneckopeningX2.OptionsColumn.AllowEdit = False
        Me.NeckneckopeningX2.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.NeckneckopeningX2.OptionsColumn.ReadOnly = True
        Me.NeckneckopeningX2.Width = 77
        '
        'ChestbodywidthX2
        '
        Me.ChestbodywidthX2.Caption = "Chest body width X2"
        Me.ChestbodywidthX2.FieldName = "ChestbodywidthX2"
        Me.ChestbodywidthX2.MinWidth = 21
        Me.ChestbodywidthX2.Name = "ChestbodywidthX2"
        Me.ChestbodywidthX2.OptionsColumn.AllowEdit = False
        Me.ChestbodywidthX2.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.ChestbodywidthX2.OptionsColumn.ReadOnly = True
        Me.ChestbodywidthX2.Visible = True
        Me.ChestbodywidthX2.VisibleIndex = 69
        Me.ChestbodywidthX2.Width = 77
        '
        'CenterBackbodylength
        '
        Me.CenterBackbodylength.Caption = "Center Back body length"
        Me.CenterBackbodylength.FieldName = "CenterBackbodylength"
        Me.CenterBackbodylength.MinWidth = 21
        Me.CenterBackbodylength.Name = "CenterBackbodylength"
        Me.CenterBackbodylength.OptionsColumn.AllowEdit = False
        Me.CenterBackbodylength.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.CenterBackbodylength.OptionsColumn.ReadOnly = True
        Me.CenterBackbodylength.Width = 77
        '
        'WaistwaistrelaxedInseam
        '
        Me.WaistwaistrelaxedInseam.Caption = "Waist waistrelaxed Inseam"
        Me.WaistwaistrelaxedInseam.FieldName = "WaistwaistrelaxedInseam"
        Me.WaistwaistrelaxedInseam.MinWidth = 21
        Me.WaistwaistrelaxedInseam.Name = "WaistwaistrelaxedInseam"
        Me.WaistwaistrelaxedInseam.OptionsColumn.AllowEdit = False
        Me.WaistwaistrelaxedInseam.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.WaistwaistrelaxedInseam.OptionsColumn.ReadOnly = True
        Me.WaistwaistrelaxedInseam.Width = 77
        '
        'PackQuantityQTYPERSELLINGUNIT
        '
        Me.PackQuantityQTYPERSELLINGUNIT.Caption = "Pack Quantity QTYPERSELLINGUNIT"
        Me.PackQuantityQTYPERSELLINGUNIT.FieldName = "PackQuantityQTYPERSELLINGUNIT"
        Me.PackQuantityQTYPERSELLINGUNIT.MinWidth = 21
        Me.PackQuantityQTYPERSELLINGUNIT.Name = "PackQuantityQTYPERSELLINGUNIT"
        Me.PackQuantityQTYPERSELLINGUNIT.OptionsColumn.AllowEdit = False
        Me.PackQuantityQTYPERSELLINGUNIT.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.PackQuantityQTYPERSELLINGUNIT.OptionsColumn.ReadOnly = True
        Me.PackQuantityQTYPERSELLINGUNIT.Width = 77
        '
        'Fabriccode
        '
        Me.Fabriccode.Caption = "Fabric code"
        Me.Fabriccode.FieldName = "Fabriccode"
        Me.Fabriccode.MinWidth = 21
        Me.Fabriccode.Name = "Fabriccode"
        Me.Fabriccode.OptionsColumn.AllowEdit = False
        Me.Fabriccode.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.Fabriccode.OptionsColumn.ReadOnly = True
        Me.Fabriccode.Width = 77
        '
        'PRODUCTDESCRIPTION
        '
        Me.PRODUCTDESCRIPTION.Caption = "PRODUCT DESCRIPTION"
        Me.PRODUCTDESCRIPTION.FieldName = "PRODUCTDESCRIPTION"
        Me.PRODUCTDESCRIPTION.MinWidth = 21
        Me.PRODUCTDESCRIPTION.Name = "PRODUCTDESCRIPTION"
        Me.PRODUCTDESCRIPTION.OptionsColumn.AllowEdit = False
        Me.PRODUCTDESCRIPTION.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.PRODUCTDESCRIPTION.OptionsColumn.ReadOnly = True
        Me.PRODUCTDESCRIPTION.Width = 77
        '
        'PRODUCTCODEDESCRIPTION
        '
        Me.PRODUCTCODEDESCRIPTION.Caption = "Compound Coloror CCI Mfor2in1CCIM"
        Me.PRODUCTCODEDESCRIPTION.FieldName = "PRODUCTCODEDESCRIPTION"
        Me.PRODUCTCODEDESCRIPTION.MinWidth = 21
        Me.PRODUCTCODEDESCRIPTION.Name = "PRODUCTCODEDESCRIPTION"
        Me.PRODUCTCODEDESCRIPTION.OptionsColumn.AllowEdit = False
        Me.PRODUCTCODEDESCRIPTION.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.PRODUCTCODEDESCRIPTION.OptionsColumn.ReadOnly = True
        Me.PRODUCTCODEDESCRIPTION.Width = 77
        '
        'GARMENTSTYLEFIELD
        '
        Me.GARMENTSTYLEFIELD.Caption = "GARMENT STYLE FIELD"
        Me.GARMENTSTYLEFIELD.FieldName = "GARMENTSTYLEFIELD"
        Me.GARMENTSTYLEFIELD.MinWidth = 21
        Me.GARMENTSTYLEFIELD.Name = "GARMENTSTYLEFIELD"
        Me.GARMENTSTYLEFIELD.OptionsColumn.AllowEdit = False
        Me.GARMENTSTYLEFIELD.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.GARMENTSTYLEFIELD.OptionsColumn.ReadOnly = True
        Me.GARMENTSTYLEFIELD.Width = 77
        '
        'INSEAMSTYLE
        '
        Me.INSEAMSTYLE.Caption = "INSEAM STYLE"
        Me.INSEAMSTYLE.FieldName = "INSEAMSTYLE"
        Me.INSEAMSTYLE.MinWidth = 21
        Me.INSEAMSTYLE.Name = "INSEAMSTYLE"
        Me.INSEAMSTYLE.OptionsColumn.AllowEdit = False
        Me.INSEAMSTYLE.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.INSEAMSTYLE.OptionsColumn.ReadOnly = True
        Me.INSEAMSTYLE.Width = 77
        '
        'EndCustomer
        '
        Me.EndCustomer.Caption = "End Customer"
        Me.EndCustomer.FieldName = "EndCustomer"
        Me.EndCustomer.MinWidth = 21
        Me.EndCustomer.Name = "EndCustomer"
        Me.EndCustomer.OptionsColumn.AllowEdit = False
        Me.EndCustomer.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.EndCustomer.OptionsColumn.ReadOnly = True
        Me.EndCustomer.Visible = True
        Me.EndCustomer.VisibleIndex = 70
        Me.EndCustomer.Width = 77
        '
        'oDockManager
        '
        Me.oDockManager.Form = Me
        Me.oDockManager.TopZIndexControls.AddRange(New String() {"DevExpress.XtraBars.BarDockControl", "DevExpress.XtraBars.StandaloneBarDockControl", "System.Windows.Forms.StatusBar", "DevExpress.XtraBars.Ribbon.RibbonStatusBar", "DevExpress.XtraBars.Ribbon.RibbonControl"})
        '
        'otbmain
        '
        Me.otbmain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.otbmain.Location = New System.Drawing.Point(0, 75)
        Me.otbmain.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.otbmain.Name = "otbmain"
        Me.otbmain.SelectedTabPage = Me.otpnew
        Me.otbmain.Size = New System.Drawing.Size(1137, 375)
        Me.otbmain.TabIndex = 387
        Me.otbmain.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.otpnew, Me.otpworking, Me.otppayment})
        '
        'otpnew
        '
        Me.otpnew.Controls.Add(Me.ogdpo)
        Me.otpnew.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.otpnew.Name = "otpnew"
        Me.otpnew.Size = New System.Drawing.Size(1129, 344)
        Me.otpnew.Text = "New && Update Purchase"
        '
        'otpworking
        '
        Me.otpworking.Controls.Add(Me.ogdwpo)
        Me.otpworking.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.otpworking.Name = "otpworking"
        Me.otpworking.PageVisible = False
        Me.otpworking.Size = New System.Drawing.Size(1135, 342)
        Me.otpworking.Text = "Workig Purchase"
        '
        'ogdwpo
        '
        Me.ogdwpo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogdwpo.Location = New System.Drawing.Point(0, 0)
        Me.ogdwpo.MainView = Me.ogvwpo
        Me.ogdwpo.Name = "ogdwpo"
        Me.ogdwpo.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCheckEdit1})
        Me.ogdwpo.Size = New System.Drawing.Size(1135, 342)
        Me.ogdwpo.TabIndex = 1
        Me.ogdwpo.TabStop = False
        Me.ogdwpo.Tag = "2|"
        Me.ogdwpo.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvwpo})
        '
        'ogvwpo
        '
        Me.ogvwpo.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.dFTSelect, Me.PIStateEdit, Me.PIStateAccept, Me.xVendorLocation, Me.xVendorName, Me.sFactoryCode, Me.sPONo, Me.sPODate, Me.sBuy, Me.sBuyNo, Me.sSeason, Me.sPORefType, Me.sGarmentShipmentDestination, Me.sMatrCode, Me.sPOItemCode, Me.sStyleNo, Me.sColor, Me.sColorName, Me.sQRS, Me.sPromoQty, Me.sMOQ, Me.sActualQuantity, Me.sQuantity, Me.sQtyUnit, Me.sPrice, Me.sSurcharge, Me.sSubProgram, Me.T2_Confirm_OrderNo, Me.T2_Confirm_PO_Date, Me.Estimatedeldate, Me.T2_Confirm_Price, Me.T2_Confirm_Quantity, Me.T2_Confirm_MOQQuantity, Me.FNPIQuantity, Me.Doc_Surcharge_Amount, Me.FNPINetAmt, Me.T2_Confirm_Ship_Date, Me.T2_Confirm_ShipQuantity, Me.Actualdeldate, Me.T2_Confirm_Ship_Date2, Me.T2_Confirm_ShipQuantity2, Me.Actualdeldate2, Me.POBalanceQty, Me.InvoiceNo, Me.FTAWBNo, Me.T2_Confirm_By, Me.T2_Confirm_Note, Me.FTStateHasFile, Me.sCategory, Me.sRemarkForPurchase, Me.sShipto, Me.xMatrClass, Me.sGCW, Me.sSize, Me.ssCurrency, Me.sDeliveryDate, Me.sOGACDate, Me.sRcvQty, Me.sRcvDate, Me.RcvBalanceQty, Me.AcknowledgeBy, Me.AcknowledgeDate, Me.AcknowledgeTime, Me.StateAcknowledgeLock, Me.sVenderCode, Me.sEndCustomer, Me.ssysownername, Me.ssysownermail, Me.StateRejectedBuy, Me.sRejectedBuyBy, Me.RejectedBuyDate, Me.RejectedBuyTime, Me.RejectedBuyNote})
        Me.ogvwpo.GridControl = Me.ogdwpo
        Me.ogvwpo.Name = "ogvwpo"
        Me.ogvwpo.OptionsCustomization.AllowQuickHideColumns = False
        Me.ogvwpo.OptionsView.ColumnAutoWidth = False
        Me.ogvwpo.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.ogvwpo.OptionsView.ShowGroupPanel = False
        Me.ogvwpo.Tag = "2|"
        '
        'dFTSelect
        '
        Me.dFTSelect.Caption = "select"
        Me.dFTSelect.ColumnEdit = Me.RepositoryItemCheckEdit1
        Me.dFTSelect.FieldName = "FTSelect"
        Me.dFTSelect.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
        Me.dFTSelect.MinWidth = 22
        Me.dFTSelect.Name = "dFTSelect"
        Me.dFTSelect.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.dFTSelect.Visible = True
        Me.dFTSelect.VisibleIndex = 0
        Me.dFTSelect.Width = 50
        '
        'RepositoryItemCheckEdit1
        '
        Me.RepositoryItemCheckEdit1.AutoHeight = False
        Me.RepositoryItemCheckEdit1.Caption = "Check"
        Me.RepositoryItemCheckEdit1.Name = "RepositoryItemCheckEdit1"
        Me.RepositoryItemCheckEdit1.ValueChecked = "1"
        Me.RepositoryItemCheckEdit1.ValueUnchecked = "0"
        '
        'PIStateEdit
        '
        Me.PIStateEdit.Caption = "PI Edit"
        Me.PIStateEdit.ColumnEdit = Me.RepositoryItemCheckEdit1
        Me.PIStateEdit.FieldName = "PIStateEdit"
        Me.PIStateEdit.MinWidth = 21
        Me.PIStateEdit.Name = "PIStateEdit"
        Me.PIStateEdit.OptionsColumn.AllowEdit = False
        Me.PIStateEdit.OptionsColumn.ReadOnly = True
        Me.PIStateEdit.Visible = True
        Me.PIStateEdit.VisibleIndex = 1
        Me.PIStateEdit.Width = 55
        '
        'PIStateAccept
        '
        Me.PIStateAccept.Caption = "PI Accepted"
        Me.PIStateAccept.ColumnEdit = Me.RepositoryItemCheckEdit1
        Me.PIStateAccept.FieldName = "PIStateAccept"
        Me.PIStateAccept.MinWidth = 21
        Me.PIStateAccept.Name = "PIStateAccept"
        Me.PIStateAccept.OptionsColumn.AllowEdit = False
        Me.PIStateAccept.OptionsColumn.ReadOnly = True
        Me.PIStateAccept.Visible = True
        Me.PIStateAccept.VisibleIndex = 2
        Me.PIStateAccept.Width = 66
        '
        'xVendorLocation
        '
        Me.xVendorLocation.Caption = "Vendor Location"
        Me.xVendorLocation.FieldName = "VendorLocation"
        Me.xVendorLocation.MinWidth = 21
        Me.xVendorLocation.Name = "xVendorLocation"
        Me.xVendorLocation.OptionsColumn.AllowEdit = False
        Me.xVendorLocation.OptionsColumn.ReadOnly = True
        Me.xVendorLocation.Visible = True
        Me.xVendorLocation.VisibleIndex = 3
        Me.xVendorLocation.Width = 96
        '
        'xVendorName
        '
        Me.xVendorName.Caption = "Vendor Name"
        Me.xVendorName.FieldName = "VendorName"
        Me.xVendorName.MinWidth = 21
        Me.xVendorName.Name = "xVendorName"
        Me.xVendorName.OptionsColumn.AllowEdit = False
        Me.xVendorName.OptionsColumn.ReadOnly = True
        Me.xVendorName.Visible = True
        Me.xVendorName.VisibleIndex = 4
        Me.xVendorName.Width = 177
        '
        'sFactoryCode
        '
        Me.sFactoryCode.Caption = "Factory Code"
        Me.sFactoryCode.FieldName = "FactoryCode"
        Me.sFactoryCode.MinWidth = 21
        Me.sFactoryCode.Name = "sFactoryCode"
        Me.sFactoryCode.OptionsColumn.AllowEdit = False
        Me.sFactoryCode.OptionsColumn.ReadOnly = True
        Me.sFactoryCode.Visible = True
        Me.sFactoryCode.VisibleIndex = 5
        Me.sFactoryCode.Width = 77
        '
        'sPONo
        '
        Me.sPONo.Caption = "PO Number"
        Me.sPONo.FieldName = "PONo"
        Me.sPONo.MinWidth = 21
        Me.sPONo.Name = "sPONo"
        Me.sPONo.OptionsColumn.AllowEdit = False
        Me.sPONo.OptionsColumn.ReadOnly = True
        Me.sPONo.Visible = True
        Me.sPONo.VisibleIndex = 6
        Me.sPONo.Width = 139
        '
        'sPODate
        '
        Me.sPODate.Caption = "PO Date"
        Me.sPODate.FieldName = "PODate"
        Me.sPODate.MinWidth = 21
        Me.sPODate.Name = "sPODate"
        Me.sPODate.OptionsColumn.AllowEdit = False
        Me.sPODate.OptionsColumn.ReadOnly = True
        Me.sPODate.Visible = True
        Me.sPODate.VisibleIndex = 7
        Me.sPODate.Width = 113
        '
        'sBuy
        '
        Me.sBuy.Caption = "Buy Month"
        Me.sBuy.FieldName = "Buy"
        Me.sBuy.MinWidth = 21
        Me.sBuy.Name = "sBuy"
        Me.sBuy.OptionsColumn.AllowEdit = False
        Me.sBuy.OptionsColumn.ReadOnly = True
        Me.sBuy.Visible = True
        Me.sBuy.VisibleIndex = 8
        Me.sBuy.Width = 77
        '
        'sBuyNo
        '
        Me.sBuyNo.Caption = "Buy Code"
        Me.sBuyNo.FieldName = "BuyNo"
        Me.sBuyNo.MinWidth = 21
        Me.sBuyNo.Name = "sBuyNo"
        Me.sBuyNo.OptionsColumn.AllowEdit = False
        Me.sBuyNo.OptionsColumn.ReadOnly = True
        Me.sBuyNo.Visible = True
        Me.sBuyNo.VisibleIndex = 9
        Me.sBuyNo.Width = 77
        '
        'sSeason
        '
        Me.sSeason.Caption = "Season"
        Me.sSeason.FieldName = "Season"
        Me.sSeason.MinWidth = 21
        Me.sSeason.Name = "sSeason"
        Me.sSeason.OptionsColumn.AllowEdit = False
        Me.sSeason.OptionsColumn.ReadOnly = True
        Me.sSeason.Visible = True
        Me.sSeason.VisibleIndex = 10
        Me.sSeason.Width = 77
        '
        'sPORefType
        '
        Me.sPORefType.Caption = "Ref Order Type"
        Me.sPORefType.FieldName = "PORefType"
        Me.sPORefType.MinWidth = 21
        Me.sPORefType.Name = "sPORefType"
        Me.sPORefType.OptionsColumn.AllowEdit = False
        Me.sPORefType.OptionsColumn.ReadOnly = True
        Me.sPORefType.Visible = True
        Me.sPORefType.VisibleIndex = 11
        Me.sPORefType.Width = 77
        '
        'sGarmentShipmentDestination
        '
        Me.sGarmentShipmentDestination.Caption = "Garment Shipment Destination"
        Me.sGarmentShipmentDestination.FieldName = "GarmentShipmentDestination"
        Me.sGarmentShipmentDestination.MinWidth = 21
        Me.sGarmentShipmentDestination.Name = "sGarmentShipmentDestination"
        Me.sGarmentShipmentDestination.OptionsColumn.AllowEdit = False
        Me.sGarmentShipmentDestination.OptionsColumn.ReadOnly = True
        Me.sGarmentShipmentDestination.Visible = True
        Me.sGarmentShipmentDestination.VisibleIndex = 12
        Me.sGarmentShipmentDestination.Width = 77
        '
        'sMatrCode
        '
        Me.sMatrCode.Caption = "Material Code"
        Me.sMatrCode.FieldName = "MatrCode"
        Me.sMatrCode.MinWidth = 21
        Me.sMatrCode.Name = "sMatrCode"
        Me.sMatrCode.OptionsColumn.AllowEdit = False
        Me.sMatrCode.OptionsColumn.ReadOnly = True
        Me.sMatrCode.Visible = True
        Me.sMatrCode.VisibleIndex = 13
        Me.sMatrCode.Width = 77
        '
        'sPOItemCode
        '
        Me.sPOItemCode.Caption = "PO Item Code"
        Me.sPOItemCode.FieldName = "POItemCode"
        Me.sPOItemCode.MinWidth = 21
        Me.sPOItemCode.Name = "sPOItemCode"
        Me.sPOItemCode.OptionsColumn.AllowEdit = False
        Me.sPOItemCode.OptionsColumn.ReadOnly = True
        Me.sPOItemCode.Visible = True
        Me.sPOItemCode.VisibleIndex = 14
        Me.sPOItemCode.Width = 77
        '
        'sStyleNo
        '
        Me.sStyleNo.Caption = "Style No"
        Me.sStyleNo.FieldName = "StyleNo"
        Me.sStyleNo.MinWidth = 21
        Me.sStyleNo.Name = "sStyleNo"
        Me.sStyleNo.OptionsColumn.AllowEdit = False
        Me.sStyleNo.OptionsColumn.ReadOnly = True
        Me.sStyleNo.Visible = True
        Me.sStyleNo.VisibleIndex = 15
        Me.sStyleNo.Width = 77
        '
        'sColor
        '
        Me.sColor.Caption = "Color Code"
        Me.sColor.FieldName = "Color"
        Me.sColor.MinWidth = 21
        Me.sColor.Name = "sColor"
        Me.sColor.OptionsColumn.AllowEdit = False
        Me.sColor.OptionsColumn.ReadOnly = True
        Me.sColor.Visible = True
        Me.sColor.VisibleIndex = 16
        Me.sColor.Width = 77
        '
        'sColorName
        '
        Me.sColorName.Caption = "Color Name"
        Me.sColorName.FieldName = "ColorName"
        Me.sColorName.MinWidth = 21
        Me.sColorName.Name = "sColorName"
        Me.sColorName.OptionsColumn.AllowEdit = False
        Me.sColorName.OptionsColumn.ReadOnly = True
        Me.sColorName.Visible = True
        Me.sColorName.VisibleIndex = 17
        Me.sColorName.Width = 77
        '
        'sQRS
        '
        Me.sQRS.Caption = "QPP/QRS Quantity"
        Me.sQRS.DisplayFormat.FormatString = "{0:n4}"
        Me.sQRS.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.sQRS.FieldName = "QRS"
        Me.sQRS.MinWidth = 21
        Me.sQRS.Name = "sQRS"
        Me.sQRS.OptionsColumn.AllowEdit = False
        Me.sQRS.OptionsColumn.ReadOnly = True
        Me.sQRS.Visible = True
        Me.sQRS.VisibleIndex = 18
        Me.sQRS.Width = 77
        '
        'sPromoQty
        '
        Me.sPromoQty.Caption = "Promo Quantity"
        Me.sPromoQty.DisplayFormat.FormatString = "{0:n4}"
        Me.sPromoQty.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.sPromoQty.FieldName = "PromoQty"
        Me.sPromoQty.MinWidth = 21
        Me.sPromoQty.Name = "sPromoQty"
        Me.sPromoQty.OptionsColumn.AllowEdit = False
        Me.sPromoQty.OptionsColumn.ReadOnly = True
        Me.sPromoQty.Visible = True
        Me.sPromoQty.VisibleIndex = 19
        Me.sPromoQty.Width = 77
        '
        'sMOQ
        '
        Me.sMOQ.Caption = "MOQ Quantity"
        Me.sMOQ.DisplayFormat.FormatString = "{0:n4}"
        Me.sMOQ.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.sMOQ.FieldName = "MOQ"
        Me.sMOQ.MinWidth = 21
        Me.sMOQ.Name = "sMOQ"
        Me.sMOQ.OptionsColumn.AllowEdit = False
        Me.sMOQ.OptionsColumn.ReadOnly = True
        Me.sMOQ.Visible = True
        Me.sMOQ.VisibleIndex = 20
        Me.sMOQ.Width = 77
        '
        'sActualQuantity
        '
        Me.sActualQuantity.Caption = "Bulk Quantity"
        Me.sActualQuantity.DisplayFormat.FormatString = "{0:n4}"
        Me.sActualQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.sActualQuantity.FieldName = "ActualQuantity"
        Me.sActualQuantity.MinWidth = 21
        Me.sActualQuantity.Name = "sActualQuantity"
        Me.sActualQuantity.OptionsColumn.AllowEdit = False
        Me.sActualQuantity.OptionsColumn.ReadOnly = True
        Me.sActualQuantity.Visible = True
        Me.sActualQuantity.VisibleIndex = 21
        Me.sActualQuantity.Width = 77
        '
        'sQuantity
        '
        Me.sQuantity.Caption = "Total PO Quantity"
        Me.sQuantity.DisplayFormat.FormatString = "{0:n4}"
        Me.sQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.sQuantity.FieldName = "Quantity"
        Me.sQuantity.MinWidth = 21
        Me.sQuantity.Name = "sQuantity"
        Me.sQuantity.OptionsColumn.AllowEdit = False
        Me.sQuantity.OptionsColumn.ReadOnly = True
        Me.sQuantity.Visible = True
        Me.sQuantity.VisibleIndex = 22
        Me.sQuantity.Width = 77
        '
        'sQtyUnit
        '
        Me.sQtyUnit.Caption = "Unit"
        Me.sQtyUnit.FieldName = "QtyUnit"
        Me.sQtyUnit.MinWidth = 21
        Me.sQtyUnit.Name = "sQtyUnit"
        Me.sQtyUnit.OptionsColumn.AllowEdit = False
        Me.sQtyUnit.OptionsColumn.ReadOnly = True
        Me.sQtyUnit.Visible = True
        Me.sQtyUnit.VisibleIndex = 23
        Me.sQtyUnit.Width = 77
        '
        'sPrice
        '
        Me.sPrice.Caption = "Price"
        Me.sPrice.DisplayFormat.FormatString = "{0:n5}"
        Me.sPrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.sPrice.FieldName = "Price"
        Me.sPrice.MinWidth = 21
        Me.sPrice.Name = "sPrice"
        Me.sPrice.OptionsColumn.AllowEdit = False
        Me.sPrice.OptionsColumn.ReadOnly = True
        Me.sPrice.Visible = True
        Me.sPrice.VisibleIndex = 24
        Me.sPrice.Width = 77
        '
        'sSurcharge
        '
        Me.sSurcharge.Caption = "Surcharge"
        Me.sSurcharge.DisplayFormat.FormatString = "{0:n5}"
        Me.sSurcharge.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.sSurcharge.FieldName = "Surcharge"
        Me.sSurcharge.MinWidth = 21
        Me.sSurcharge.Name = "sSurcharge"
        Me.sSurcharge.OptionsColumn.AllowEdit = False
        Me.sSurcharge.OptionsColumn.ReadOnly = True
        Me.sSurcharge.Visible = True
        Me.sSurcharge.VisibleIndex = 25
        Me.sSurcharge.Width = 77
        '
        'sSubProgram
        '
        Me.sSubProgram.Caption = "Sub Program"
        Me.sSubProgram.FieldName = "SubProgram"
        Me.sSubProgram.MinWidth = 21
        Me.sSubProgram.Name = "sSubProgram"
        Me.sSubProgram.OptionsColumn.AllowEdit = False
        Me.sSubProgram.OptionsColumn.ReadOnly = True
        Me.sSubProgram.Visible = True
        Me.sSubProgram.VisibleIndex = 26
        Me.sSubProgram.Width = 77
        '
        'T2_Confirm_OrderNo
        '
        Me.T2_Confirm_OrderNo.Caption = "PI Number"
        Me.T2_Confirm_OrderNo.FieldName = "T2_Confirm_OrderNo"
        Me.T2_Confirm_OrderNo.MinWidth = 21
        Me.T2_Confirm_OrderNo.Name = "T2_Confirm_OrderNo"
        Me.T2_Confirm_OrderNo.OptionsColumn.AllowEdit = False
        Me.T2_Confirm_OrderNo.OptionsColumn.ReadOnly = True
        Me.T2_Confirm_OrderNo.Visible = True
        Me.T2_Confirm_OrderNo.VisibleIndex = 27
        Me.T2_Confirm_OrderNo.Width = 129
        '
        'T2_Confirm_PO_Date
        '
        Me.T2_Confirm_PO_Date.Caption = "PI Date"
        Me.T2_Confirm_PO_Date.FieldName = "T2_Confirm_PO_Date"
        Me.T2_Confirm_PO_Date.MinWidth = 21
        Me.T2_Confirm_PO_Date.Name = "T2_Confirm_PO_Date"
        Me.T2_Confirm_PO_Date.OptionsColumn.AllowEdit = False
        Me.T2_Confirm_PO_Date.OptionsColumn.ReadOnly = True
        Me.T2_Confirm_PO_Date.Visible = True
        Me.T2_Confirm_PO_Date.VisibleIndex = 28
        Me.T2_Confirm_PO_Date.Width = 103
        '
        'Estimatedeldate
        '
        Me.Estimatedeldate.Caption = "PI Confirm Delivery Date"
        Me.Estimatedeldate.FieldName = "Estimatedeldate"
        Me.Estimatedeldate.MinWidth = 21
        Me.Estimatedeldate.Name = "Estimatedeldate"
        Me.Estimatedeldate.OptionsColumn.AllowEdit = False
        Me.Estimatedeldate.OptionsColumn.ReadOnly = True
        Me.Estimatedeldate.Visible = True
        Me.Estimatedeldate.VisibleIndex = 29
        Me.Estimatedeldate.Width = 103
        '
        'T2_Confirm_Price
        '
        Me.T2_Confirm_Price.Caption = "Confirm Price"
        Me.T2_Confirm_Price.DisplayFormat.FormatString = "{0:n5}"
        Me.T2_Confirm_Price.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.T2_Confirm_Price.FieldName = "T2_Confirm_Price"
        Me.T2_Confirm_Price.MinWidth = 21
        Me.T2_Confirm_Price.Name = "T2_Confirm_Price"
        Me.T2_Confirm_Price.OptionsColumn.AllowEdit = False
        Me.T2_Confirm_Price.OptionsColumn.ReadOnly = True
        Me.T2_Confirm_Price.Visible = True
        Me.T2_Confirm_Price.VisibleIndex = 30
        Me.T2_Confirm_Price.Width = 77
        '
        'T2_Confirm_Quantity
        '
        Me.T2_Confirm_Quantity.Caption = "Confirm Quantity"
        Me.T2_Confirm_Quantity.DisplayFormat.FormatString = "{0:n4}"
        Me.T2_Confirm_Quantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.T2_Confirm_Quantity.FieldName = "T2_Confirm_Quantity"
        Me.T2_Confirm_Quantity.MinWidth = 21
        Me.T2_Confirm_Quantity.Name = "T2_Confirm_Quantity"
        Me.T2_Confirm_Quantity.OptionsColumn.AllowEdit = False
        Me.T2_Confirm_Quantity.OptionsColumn.ReadOnly = True
        Me.T2_Confirm_Quantity.Visible = True
        Me.T2_Confirm_Quantity.VisibleIndex = 31
        Me.T2_Confirm_Quantity.Width = 77
        '
        'T2_Confirm_MOQQuantity
        '
        Me.T2_Confirm_MOQQuantity.Caption = "Confirm MOQ"
        Me.T2_Confirm_MOQQuantity.DisplayFormat.FormatString = "{0:n4}"
        Me.T2_Confirm_MOQQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.T2_Confirm_MOQQuantity.FieldName = "T2_Confirm_MOQQuantity"
        Me.T2_Confirm_MOQQuantity.MinWidth = 21
        Me.T2_Confirm_MOQQuantity.Name = "T2_Confirm_MOQQuantity"
        Me.T2_Confirm_MOQQuantity.OptionsColumn.AllowEdit = False
        Me.T2_Confirm_MOQQuantity.OptionsColumn.ReadOnly = True
        Me.T2_Confirm_MOQQuantity.Visible = True
        Me.T2_Confirm_MOQQuantity.VisibleIndex = 32
        Me.T2_Confirm_MOQQuantity.Width = 77
        '
        'FNPIQuantity
        '
        Me.FNPIQuantity.Caption = "Total PI QTY"
        Me.FNPIQuantity.DisplayFormat.FormatString = "{0:n4}"
        Me.FNPIQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNPIQuantity.FieldName = "FNPIQuantity"
        Me.FNPIQuantity.MinWidth = 21
        Me.FNPIQuantity.Name = "FNPIQuantity"
        Me.FNPIQuantity.OptionsColumn.AllowEdit = False
        Me.FNPIQuantity.OptionsColumn.ReadOnly = True
        Me.FNPIQuantity.Visible = True
        Me.FNPIQuantity.VisibleIndex = 33
        Me.FNPIQuantity.Width = 77
        '
        'Doc_Surcharge_Amount
        '
        Me.Doc_Surcharge_Amount.Caption = "PI Surcharge"
        Me.Doc_Surcharge_Amount.DisplayFormat.FormatString = "{0:n2}"
        Me.Doc_Surcharge_Amount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.Doc_Surcharge_Amount.FieldName = "Doc_Surcharge_Amount"
        Me.Doc_Surcharge_Amount.MinWidth = 21
        Me.Doc_Surcharge_Amount.Name = "Doc_Surcharge_Amount"
        Me.Doc_Surcharge_Amount.OptionsColumn.AllowEdit = False
        Me.Doc_Surcharge_Amount.OptionsColumn.ReadOnly = True
        Me.Doc_Surcharge_Amount.Visible = True
        Me.Doc_Surcharge_Amount.VisibleIndex = 34
        Me.Doc_Surcharge_Amount.Width = 77
        '
        'FNPINetAmt
        '
        Me.FNPINetAmt.Caption = "Total PI Amount"
        Me.FNPINetAmt.DisplayFormat.FormatString = "{0:n2}"
        Me.FNPINetAmt.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNPINetAmt.FieldName = "FNPINetAmt"
        Me.FNPINetAmt.MinWidth = 21
        Me.FNPINetAmt.Name = "FNPINetAmt"
        Me.FNPINetAmt.OptionsColumn.AllowEdit = False
        Me.FNPINetAmt.OptionsColumn.ReadOnly = True
        Me.FNPINetAmt.Visible = True
        Me.FNPINetAmt.VisibleIndex = 35
        Me.FNPINetAmt.Width = 77
        '
        'T2_Confirm_Ship_Date
        '
        Me.T2_Confirm_Ship_Date.Caption = "Confirm Ship Date"
        Me.T2_Confirm_Ship_Date.FieldName = "T2_Confirm_Ship_Date"
        Me.T2_Confirm_Ship_Date.MinWidth = 21
        Me.T2_Confirm_Ship_Date.Name = "T2_Confirm_Ship_Date"
        Me.T2_Confirm_Ship_Date.OptionsColumn.AllowEdit = False
        Me.T2_Confirm_Ship_Date.OptionsColumn.ReadOnly = True
        Me.T2_Confirm_Ship_Date.Visible = True
        Me.T2_Confirm_Ship_Date.VisibleIndex = 36
        Me.T2_Confirm_Ship_Date.Width = 77
        '
        'T2_Confirm_ShipQuantity
        '
        Me.T2_Confirm_ShipQuantity.Caption = "Confirm Ship Quantity"
        Me.T2_Confirm_ShipQuantity.DisplayFormat.FormatString = "{0:n4}"
        Me.T2_Confirm_ShipQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.T2_Confirm_ShipQuantity.FieldName = "T2_Confirm_ShipQuantity"
        Me.T2_Confirm_ShipQuantity.MinWidth = 21
        Me.T2_Confirm_ShipQuantity.Name = "T2_Confirm_ShipQuantity"
        Me.T2_Confirm_ShipQuantity.OptionsColumn.AllowEdit = False
        Me.T2_Confirm_ShipQuantity.OptionsColumn.ReadOnly = True
        Me.T2_Confirm_ShipQuantity.Visible = True
        Me.T2_Confirm_ShipQuantity.VisibleIndex = 37
        Me.T2_Confirm_ShipQuantity.Width = 77
        '
        'Actualdeldate
        '
        Me.Actualdeldate.Caption = "Actual Delivery Date"
        Me.Actualdeldate.FieldName = "Actualdeldate"
        Me.Actualdeldate.MinWidth = 21
        Me.Actualdeldate.Name = "Actualdeldate"
        Me.Actualdeldate.OptionsColumn.AllowEdit = False
        Me.Actualdeldate.OptionsColumn.ReadOnly = True
        Me.Actualdeldate.Visible = True
        Me.Actualdeldate.VisibleIndex = 38
        Me.Actualdeldate.Width = 77
        '
        'T2_Confirm_Ship_Date2
        '
        Me.T2_Confirm_Ship_Date2.Caption = "Confirm Ship Date(2nd)"
        Me.T2_Confirm_Ship_Date2.FieldName = "T2_Confirm_Ship_Date2"
        Me.T2_Confirm_Ship_Date2.MinWidth = 21
        Me.T2_Confirm_Ship_Date2.Name = "T2_Confirm_Ship_Date2"
        Me.T2_Confirm_Ship_Date2.OptionsColumn.AllowEdit = False
        Me.T2_Confirm_Ship_Date2.OptionsColumn.ReadOnly = True
        Me.T2_Confirm_Ship_Date2.Visible = True
        Me.T2_Confirm_Ship_Date2.VisibleIndex = 39
        Me.T2_Confirm_Ship_Date2.Width = 77
        '
        'T2_Confirm_ShipQuantity2
        '
        Me.T2_Confirm_ShipQuantity2.Caption = "Confirm Ship Quantity(2nd)"
        Me.T2_Confirm_ShipQuantity2.DisplayFormat.FormatString = "{0:n4}"
        Me.T2_Confirm_ShipQuantity2.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.T2_Confirm_ShipQuantity2.FieldName = "T2_Confirm_ShipQuantity2"
        Me.T2_Confirm_ShipQuantity2.MinWidth = 21
        Me.T2_Confirm_ShipQuantity2.Name = "T2_Confirm_ShipQuantity2"
        Me.T2_Confirm_ShipQuantity2.OptionsColumn.AllowEdit = False
        Me.T2_Confirm_ShipQuantity2.OptionsColumn.ReadOnly = True
        Me.T2_Confirm_ShipQuantity2.Visible = True
        Me.T2_Confirm_ShipQuantity2.VisibleIndex = 40
        Me.T2_Confirm_ShipQuantity2.Width = 77
        '
        'Actualdeldate2
        '
        Me.Actualdeldate2.Caption = "Actual Delivery Date(2nd)"
        Me.Actualdeldate2.FieldName = "Actualdeldate2"
        Me.Actualdeldate2.MinWidth = 21
        Me.Actualdeldate2.Name = "Actualdeldate2"
        Me.Actualdeldate2.OptionsColumn.AllowEdit = False
        Me.Actualdeldate2.OptionsColumn.ReadOnly = True
        Me.Actualdeldate2.Visible = True
        Me.Actualdeldate2.VisibleIndex = 41
        Me.Actualdeldate2.Width = 77
        '
        'POBalanceQty
        '
        Me.POBalanceQty.Caption = "Balance Ship Quantity"
        Me.POBalanceQty.DisplayFormat.FormatString = "{0:n4}"
        Me.POBalanceQty.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.POBalanceQty.FieldName = "POBalanceQty"
        Me.POBalanceQty.MinWidth = 21
        Me.POBalanceQty.Name = "POBalanceQty"
        Me.POBalanceQty.OptionsColumn.AllowEdit = False
        Me.POBalanceQty.OptionsColumn.ReadOnly = True
        Me.POBalanceQty.Visible = True
        Me.POBalanceQty.VisibleIndex = 42
        Me.POBalanceQty.Width = 77
        '
        'InvoiceNo
        '
        Me.InvoiceNo.Caption = "Invoice No"
        Me.InvoiceNo.FieldName = "InvoiceNo"
        Me.InvoiceNo.MinWidth = 21
        Me.InvoiceNo.Name = "InvoiceNo"
        Me.InvoiceNo.OptionsColumn.AllowEdit = False
        Me.InvoiceNo.OptionsColumn.ReadOnly = True
        Me.InvoiceNo.Visible = True
        Me.InvoiceNo.VisibleIndex = 43
        Me.InvoiceNo.Width = 77
        '
        'FTAWBNo
        '
        Me.FTAWBNo.Caption = "AWB No"
        Me.FTAWBNo.FieldName = "FTAWBNo"
        Me.FTAWBNo.MinWidth = 21
        Me.FTAWBNo.Name = "FTAWBNo"
        Me.FTAWBNo.OptionsColumn.AllowEdit = False
        Me.FTAWBNo.OptionsColumn.ReadOnly = True
        Me.FTAWBNo.Visible = True
        Me.FTAWBNo.VisibleIndex = 44
        Me.FTAWBNo.Width = 77
        '
        'T2_Confirm_By
        '
        Me.T2_Confirm_By.Caption = "Confirm By"
        Me.T2_Confirm_By.FieldName = "T2_Confirm_By"
        Me.T2_Confirm_By.MinWidth = 21
        Me.T2_Confirm_By.Name = "T2_Confirm_By"
        Me.T2_Confirm_By.OptionsColumn.AllowEdit = False
        Me.T2_Confirm_By.OptionsColumn.ReadOnly = True
        Me.T2_Confirm_By.Visible = True
        Me.T2_Confirm_By.VisibleIndex = 45
        Me.T2_Confirm_By.Width = 77
        '
        'T2_Confirm_Note
        '
        Me.T2_Confirm_Note.Caption = "Remark"
        Me.T2_Confirm_Note.FieldName = "T2_Confirm_Note"
        Me.T2_Confirm_Note.MinWidth = 21
        Me.T2_Confirm_Note.Name = "T2_Confirm_Note"
        Me.T2_Confirm_Note.OptionsColumn.AllowEdit = False
        Me.T2_Confirm_Note.OptionsColumn.ReadOnly = True
        Me.T2_Confirm_Note.Visible = True
        Me.T2_Confirm_Note.VisibleIndex = 46
        Me.T2_Confirm_Note.Width = 77
        '
        'FTStateHasFile
        '
        Me.FTStateHasFile.Caption = "PDF"
        Me.FTStateHasFile.ColumnEdit = Me.RepositoryItemCheckEdit1
        Me.FTStateHasFile.FieldName = "FTStateHasFile"
        Me.FTStateHasFile.MinWidth = 21
        Me.FTStateHasFile.Name = "FTStateHasFile"
        Me.FTStateHasFile.OptionsColumn.AllowEdit = False
        Me.FTStateHasFile.OptionsColumn.ReadOnly = True
        Me.FTStateHasFile.Visible = True
        Me.FTStateHasFile.VisibleIndex = 47
        Me.FTStateHasFile.Width = 77
        '
        'sCategory
        '
        Me.sCategory.Caption = "Category"
        Me.sCategory.FieldName = "Category"
        Me.sCategory.MinWidth = 21
        Me.sCategory.Name = "sCategory"
        Me.sCategory.OptionsColumn.AllowEdit = False
        Me.sCategory.OptionsColumn.ReadOnly = True
        Me.sCategory.Visible = True
        Me.sCategory.VisibleIndex = 48
        Me.sCategory.Width = 77
        '
        'sRemarkForPurchase
        '
        Me.sRemarkForPurchase.Caption = "Remark For Purchase"
        Me.sRemarkForPurchase.FieldName = "RemarkForPurchase"
        Me.sRemarkForPurchase.MinWidth = 21
        Me.sRemarkForPurchase.Name = "sRemarkForPurchase"
        Me.sRemarkForPurchase.OptionsColumn.AllowEdit = False
        Me.sRemarkForPurchase.OptionsColumn.ReadOnly = True
        Me.sRemarkForPurchase.Visible = True
        Me.sRemarkForPurchase.VisibleIndex = 49
        Me.sRemarkForPurchase.Width = 77
        '
        'sShipto
        '
        Me.sShipto.Caption = "Ship To"
        Me.sShipto.FieldName = "Shipto"
        Me.sShipto.MinWidth = 21
        Me.sShipto.Name = "sShipto"
        Me.sShipto.OptionsColumn.AllowEdit = False
        Me.sShipto.OptionsColumn.ReadOnly = True
        Me.sShipto.Visible = True
        Me.sShipto.VisibleIndex = 50
        Me.sShipto.Width = 77
        '
        'xMatrClass
        '
        Me.xMatrClass.Caption = "Material Type"
        Me.xMatrClass.FieldName = "MatrClass"
        Me.xMatrClass.MinWidth = 21
        Me.xMatrClass.Name = "xMatrClass"
        Me.xMatrClass.OptionsColumn.AllowEdit = False
        Me.xMatrClass.OptionsColumn.ReadOnly = True
        Me.xMatrClass.Visible = True
        Me.xMatrClass.VisibleIndex = 51
        Me.xMatrClass.Width = 77
        '
        'sGCW
        '
        Me.sGCW.Caption = "GCW"
        Me.sGCW.FieldName = "GCW"
        Me.sGCW.MinWidth = 21
        Me.sGCW.Name = "sGCW"
        Me.sGCW.OptionsColumn.AllowEdit = False
        Me.sGCW.OptionsColumn.ReadOnly = True
        Me.sGCW.Visible = True
        Me.sGCW.VisibleIndex = 52
        Me.sGCW.Width = 77
        '
        'sSize
        '
        Me.sSize.Caption = "Size"
        Me.sSize.FieldName = "Size"
        Me.sSize.MinWidth = 21
        Me.sSize.Name = "sSize"
        Me.sSize.OptionsColumn.AllowEdit = False
        Me.sSize.OptionsColumn.ReadOnly = True
        Me.sSize.Visible = True
        Me.sSize.VisibleIndex = 53
        Me.sSize.Width = 77
        '
        'ssCurrency
        '
        Me.ssCurrency.Caption = "Currency"
        Me.ssCurrency.FieldName = "Currency"
        Me.ssCurrency.MinWidth = 21
        Me.ssCurrency.Name = "ssCurrency"
        Me.ssCurrency.OptionsColumn.AllowEdit = False
        Me.ssCurrency.OptionsColumn.ReadOnly = True
        Me.ssCurrency.Visible = True
        Me.ssCurrency.VisibleIndex = 54
        Me.ssCurrency.Width = 77
        '
        'sDeliveryDate
        '
        Me.sDeliveryDate.Caption = "FCTY Need Date"
        Me.sDeliveryDate.FieldName = "DeliveryDate"
        Me.sDeliveryDate.MinWidth = 21
        Me.sDeliveryDate.Name = "sDeliveryDate"
        Me.sDeliveryDate.OptionsColumn.AllowEdit = False
        Me.sDeliveryDate.OptionsColumn.ReadOnly = True
        Me.sDeliveryDate.Visible = True
        Me.sDeliveryDate.VisibleIndex = 55
        Me.sDeliveryDate.Width = 77
        '
        'sOGACDate
        '
        Me.sOGACDate.Caption = "OGAC Date"
        Me.sOGACDate.FieldName = "OGACDate"
        Me.sOGACDate.MinWidth = 21
        Me.sOGACDate.Name = "sOGACDate"
        Me.sOGACDate.OptionsColumn.AllowEdit = False
        Me.sOGACDate.OptionsColumn.ReadOnly = True
        Me.sOGACDate.Width = 77
        '
        'sRcvQty
        '
        Me.sRcvQty.Caption = "Receive Quantity"
        Me.sRcvQty.DisplayFormat.FormatString = "{0:n2}"
        Me.sRcvQty.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.sRcvQty.FieldName = "RcvQty"
        Me.sRcvQty.MinWidth = 21
        Me.sRcvQty.Name = "sRcvQty"
        Me.sRcvQty.OptionsColumn.AllowEdit = False
        Me.sRcvQty.OptionsColumn.ReadOnly = True
        Me.sRcvQty.Visible = True
        Me.sRcvQty.VisibleIndex = 56
        Me.sRcvQty.Width = 77
        '
        'sRcvDate
        '
        Me.sRcvDate.Caption = "Receive Date"
        Me.sRcvDate.FieldName = "RcvDate"
        Me.sRcvDate.MinWidth = 21
        Me.sRcvDate.Name = "sRcvDate"
        Me.sRcvDate.OptionsColumn.AllowEdit = False
        Me.sRcvDate.OptionsColumn.ReadOnly = True
        Me.sRcvDate.Visible = True
        Me.sRcvDate.VisibleIndex = 57
        Me.sRcvDate.Width = 77
        '
        'RcvBalanceQty
        '
        Me.RcvBalanceQty.Caption = "Balance Receive Quantity"
        Me.RcvBalanceQty.DisplayFormat.FormatString = "{0:n4}"
        Me.RcvBalanceQty.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.RcvBalanceQty.FieldName = "RcvBalanceQty"
        Me.RcvBalanceQty.MinWidth = 21
        Me.RcvBalanceQty.Name = "RcvBalanceQty"
        Me.RcvBalanceQty.OptionsColumn.AllowEdit = False
        Me.RcvBalanceQty.OptionsColumn.ReadOnly = True
        Me.RcvBalanceQty.Visible = True
        Me.RcvBalanceQty.VisibleIndex = 58
        Me.RcvBalanceQty.Width = 77
        '
        'AcknowledgeBy
        '
        Me.AcknowledgeBy.Caption = "Acknowledge By"
        Me.AcknowledgeBy.FieldName = "AcknowledgeBy"
        Me.AcknowledgeBy.MinWidth = 21
        Me.AcknowledgeBy.Name = "AcknowledgeBy"
        Me.AcknowledgeBy.OptionsColumn.AllowEdit = False
        Me.AcknowledgeBy.OptionsColumn.ReadOnly = True
        Me.AcknowledgeBy.Visible = True
        Me.AcknowledgeBy.VisibleIndex = 59
        Me.AcknowledgeBy.Width = 77
        '
        'AcknowledgeDate
        '
        Me.AcknowledgeDate.Caption = "Acknowledge Date"
        Me.AcknowledgeDate.FieldName = "AcknowledgeDate"
        Me.AcknowledgeDate.MinWidth = 21
        Me.AcknowledgeDate.Name = "AcknowledgeDate"
        Me.AcknowledgeDate.OptionsColumn.AllowEdit = False
        Me.AcknowledgeDate.OptionsColumn.ReadOnly = True
        Me.AcknowledgeDate.Visible = True
        Me.AcknowledgeDate.VisibleIndex = 60
        Me.AcknowledgeDate.Width = 77
        '
        'AcknowledgeTime
        '
        Me.AcknowledgeTime.Caption = "Acknowledge Time"
        Me.AcknowledgeTime.FieldName = "AcknowledgeTime"
        Me.AcknowledgeTime.MinWidth = 21
        Me.AcknowledgeTime.Name = "AcknowledgeTime"
        Me.AcknowledgeTime.OptionsColumn.AllowEdit = False
        Me.AcknowledgeTime.OptionsColumn.ReadOnly = True
        Me.AcknowledgeTime.Visible = True
        Me.AcknowledgeTime.VisibleIndex = 61
        Me.AcknowledgeTime.Width = 77
        '
        'StateAcknowledgeLock
        '
        Me.StateAcknowledgeLock.Caption = "Locked"
        Me.StateAcknowledgeLock.FieldName = "StateAcknowledgeLock"
        Me.StateAcknowledgeLock.MinWidth = 21
        Me.StateAcknowledgeLock.Name = "StateAcknowledgeLock"
        Me.StateAcknowledgeLock.OptionsColumn.AllowEdit = False
        Me.StateAcknowledgeLock.OptionsColumn.ReadOnly = True
        Me.StateAcknowledgeLock.Visible = True
        Me.StateAcknowledgeLock.VisibleIndex = 62
        Me.StateAcknowledgeLock.Width = 77
        '
        'sVenderCode
        '
        Me.sVenderCode.Caption = "Vendor Code"
        Me.sVenderCode.FieldName = "VenderCode"
        Me.sVenderCode.MinWidth = 21
        Me.sVenderCode.Name = "sVenderCode"
        Me.sVenderCode.OptionsColumn.AllowEdit = False
        Me.sVenderCode.OptionsColumn.ReadOnly = True
        Me.sVenderCode.Visible = True
        Me.sVenderCode.VisibleIndex = 63
        Me.sVenderCode.Width = 77
        '
        'sEndCustomer
        '
        Me.sEndCustomer.Caption = "End Customer"
        Me.sEndCustomer.FieldName = "EndCustomer"
        Me.sEndCustomer.MinWidth = 21
        Me.sEndCustomer.Name = "sEndCustomer"
        Me.sEndCustomer.OptionsColumn.AllowEdit = False
        Me.sEndCustomer.OptionsColumn.ReadOnly = True
        Me.sEndCustomer.Visible = True
        Me.sEndCustomer.VisibleIndex = 64
        Me.sEndCustomer.Width = 77
        '
        'ssysownername
        '
        Me.ssysownername.Caption = "Purchaser Name"
        Me.ssysownername.FieldName = "sysownername"
        Me.ssysownername.MinWidth = 21
        Me.ssysownername.Name = "ssysownername"
        Me.ssysownername.OptionsColumn.AllowEdit = False
        Me.ssysownername.OptionsColumn.ReadOnly = True
        Me.ssysownername.Visible = True
        Me.ssysownername.VisibleIndex = 65
        Me.ssysownername.Width = 77
        '
        'ssysownermail
        '
        Me.ssysownermail.Caption = "Purchaser E-Mail"
        Me.ssysownermail.FieldName = "sysownermail"
        Me.ssysownermail.MinWidth = 21
        Me.ssysownermail.Name = "ssysownermail"
        Me.ssysownermail.OptionsColumn.AllowEdit = False
        Me.ssysownermail.OptionsColumn.ReadOnly = True
        Me.ssysownermail.Visible = True
        Me.ssysownermail.VisibleIndex = 66
        Me.ssysownermail.Width = 77
        '
        'StateRejectedBuy
        '
        Me.StateRejectedBuy.Caption = "Rejected Buy"
        Me.StateRejectedBuy.ColumnEdit = Me.RepositoryItemCheckEdit1
        Me.StateRejectedBuy.FieldName = "StateRejectedBuy"
        Me.StateRejectedBuy.MinWidth = 21
        Me.StateRejectedBuy.Name = "StateRejectedBuy"
        Me.StateRejectedBuy.OptionsColumn.AllowEdit = False
        Me.StateRejectedBuy.OptionsColumn.ReadOnly = True
        Me.StateRejectedBuy.Visible = True
        Me.StateRejectedBuy.VisibleIndex = 67
        Me.StateRejectedBuy.Width = 77
        '
        'sRejectedBuyBy
        '
        Me.sRejectedBuyBy.Caption = "Rejected Buy By"
        Me.sRejectedBuyBy.FieldName = "RejectedBuyBy"
        Me.sRejectedBuyBy.MinWidth = 21
        Me.sRejectedBuyBy.Name = "sRejectedBuyBy"
        Me.sRejectedBuyBy.OptionsColumn.AllowEdit = False
        Me.sRejectedBuyBy.OptionsColumn.ReadOnly = True
        Me.sRejectedBuyBy.Visible = True
        Me.sRejectedBuyBy.VisibleIndex = 68
        Me.sRejectedBuyBy.Width = 77
        '
        'RejectedBuyDate
        '
        Me.RejectedBuyDate.Caption = "Rejected Buy Date"
        Me.RejectedBuyDate.FieldName = "RejectedBuyDate"
        Me.RejectedBuyDate.MinWidth = 21
        Me.RejectedBuyDate.Name = "RejectedBuyDate"
        Me.RejectedBuyDate.OptionsColumn.AllowEdit = False
        Me.RejectedBuyDate.OptionsColumn.ReadOnly = True
        Me.RejectedBuyDate.Visible = True
        Me.RejectedBuyDate.VisibleIndex = 69
        Me.RejectedBuyDate.Width = 77
        '
        'RejectedBuyTime
        '
        Me.RejectedBuyTime.Caption = "Rejected Buy Time"
        Me.RejectedBuyTime.FieldName = "RejectedBuyTime"
        Me.RejectedBuyTime.MinWidth = 21
        Me.RejectedBuyTime.Name = "RejectedBuyTime"
        Me.RejectedBuyTime.OptionsColumn.AllowEdit = False
        Me.RejectedBuyTime.OptionsColumn.ReadOnly = True
        Me.RejectedBuyTime.Visible = True
        Me.RejectedBuyTime.VisibleIndex = 70
        Me.RejectedBuyTime.Width = 77
        '
        'RejectedBuyNote
        '
        Me.RejectedBuyNote.Caption = "Rejected Buy Note"
        Me.RejectedBuyNote.FieldName = "RejectedBuyNote"
        Me.RejectedBuyNote.MinWidth = 21
        Me.RejectedBuyNote.Name = "RejectedBuyNote"
        Me.RejectedBuyNote.OptionsColumn.AllowEdit = False
        Me.RejectedBuyNote.OptionsColumn.ReadOnly = True
        Me.RejectedBuyNote.Visible = True
        Me.RejectedBuyNote.VisibleIndex = 71
        Me.RejectedBuyNote.Width = 214
        '
        'otppayment
        '
        Me.otppayment.Controls.Add(Me.ogdpopayment)
        Me.otppayment.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.otppayment.Name = "otppayment"
        Me.otppayment.PageVisible = False
        Me.otppayment.Size = New System.Drawing.Size(1135, 342)
        Me.otppayment.Text = "Purchase Payment"
        '
        'ogdpopayment
        '
        Me.ogdpopayment.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogdpopayment.Location = New System.Drawing.Point(0, 0)
        Me.ogdpopayment.MainView = Me.ogvpopayment
        Me.ogdpopayment.Name = "ogdpopayment"
        Me.ogdpopayment.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCheckEdit2})
        Me.ogdpopayment.Size = New System.Drawing.Size(1135, 342)
        Me.ogdpopayment.TabIndex = 1
        Me.ogdpopayment.TabStop = False
        Me.ogdpopayment.Tag = "2|"
        Me.ogdpopayment.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvpopayment})
        '
        'ogvpopayment
        '
        Me.ogvpopayment.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.aPONo, Me.PayType, Me.sPaymentTerm, Me.aPaymentDate, Me.LCNo, Me.aPINo, Me.aPIDate, Me.aRcvPIDate, Me.aPISuplCFMDeliveryDate, Me.aInvoiceNo, Me.aInvoiceDate, Me.aPurchaseDate, Me.aPurchaseBy, Me.aSupplierCode, Me.aSupplierName, Me.aCurrency, Me.aDeliveryDate, Me.aCompany, Me.aBuy, Me.aPOUnit, Me.aPOQty, Me.aPOAmount, Me.aPOOutstandingQty, Me.aSentDocToAccDate, Me.aFinishbalancePaymentDate, Me.aPIQuantity, Me.aPINetAmt, Me.aPIDocCNAmt, Me.aPIDocDNAmt, Me.aPIDocSurchargeAmt, Me.aPIDocNetAmt, Me.aNote, Me.aFTStateHasFile})
        Me.ogvpopayment.GridControl = Me.ogdpopayment
        Me.ogvpopayment.Name = "ogvpopayment"
        Me.ogvpopayment.OptionsCustomization.AllowQuickHideColumns = False
        Me.ogvpopayment.OptionsView.ColumnAutoWidth = False
        Me.ogvpopayment.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.ogvpopayment.OptionsView.ShowGroupPanel = False
        Me.ogvpopayment.Tag = "2|"
        '
        'aPONo
        '
        Me.aPONo.Caption = "PO Number"
        Me.aPONo.FieldName = "PONo"
        Me.aPONo.MinWidth = 21
        Me.aPONo.Name = "aPONo"
        Me.aPONo.OptionsColumn.AllowEdit = False
        Me.aPONo.OptionsColumn.ReadOnly = True
        Me.aPONo.Visible = True
        Me.aPONo.VisibleIndex = 0
        Me.aPONo.Width = 129
        '
        'PayType
        '
        Me.PayType.Caption = "Pay Type"
        Me.PayType.FieldName = "PayType"
        Me.PayType.MinWidth = 21
        Me.PayType.Name = "PayType"
        Me.PayType.OptionsColumn.AllowEdit = False
        Me.PayType.OptionsColumn.ReadOnly = True
        Me.PayType.Visible = True
        Me.PayType.VisibleIndex = 1
        Me.PayType.Width = 129
        '
        'sPaymentTerm
        '
        Me.sPaymentTerm.Caption = "Payment Term"
        Me.sPaymentTerm.FieldName = "PaymentTerm"
        Me.sPaymentTerm.MinWidth = 21
        Me.sPaymentTerm.Name = "sPaymentTerm"
        Me.sPaymentTerm.OptionsColumn.AllowEdit = False
        Me.sPaymentTerm.OptionsColumn.ReadOnly = True
        Me.sPaymentTerm.Visible = True
        Me.sPaymentTerm.VisibleIndex = 2
        Me.sPaymentTerm.Width = 129
        '
        'aPaymentDate
        '
        Me.aPaymentDate.Caption = "Payment Date"
        Me.aPaymentDate.FieldName = "PaymentDate"
        Me.aPaymentDate.MinWidth = 21
        Me.aPaymentDate.Name = "aPaymentDate"
        Me.aPaymentDate.OptionsColumn.AllowEdit = False
        Me.aPaymentDate.OptionsColumn.ReadOnly = True
        Me.aPaymentDate.Visible = True
        Me.aPaymentDate.VisibleIndex = 3
        Me.aPaymentDate.Width = 86
        '
        'LCNo
        '
        Me.LCNo.Caption = "LC Number"
        Me.LCNo.FieldName = "LCNo"
        Me.LCNo.MinWidth = 21
        Me.LCNo.Name = "LCNo"
        Me.LCNo.OptionsColumn.AllowEdit = False
        Me.LCNo.OptionsColumn.ReadOnly = True
        Me.LCNo.Visible = True
        Me.LCNo.VisibleIndex = 4
        Me.LCNo.Width = 129
        '
        'aPINo
        '
        Me.aPINo.Caption = "PI Number"
        Me.aPINo.FieldName = "PINo"
        Me.aPINo.MinWidth = 21
        Me.aPINo.Name = "aPINo"
        Me.aPINo.OptionsColumn.AllowEdit = False
        Me.aPINo.OptionsColumn.ReadOnly = True
        Me.aPINo.Visible = True
        Me.aPINo.VisibleIndex = 5
        Me.aPINo.Width = 129
        '
        'aPIDate
        '
        Me.aPIDate.Caption = "PI Date"
        Me.aPIDate.FieldName = "PIDate"
        Me.aPIDate.MinWidth = 21
        Me.aPIDate.Name = "aPIDate"
        Me.aPIDate.OptionsColumn.AllowEdit = False
        Me.aPIDate.OptionsColumn.ReadOnly = True
        Me.aPIDate.Visible = True
        Me.aPIDate.VisibleIndex = 6
        Me.aPIDate.Width = 77
        '
        'aRcvPIDate
        '
        Me.aRcvPIDate.Caption = "Receive PI Date"
        Me.aRcvPIDate.FieldName = "RcvPIDate"
        Me.aRcvPIDate.MinWidth = 21
        Me.aRcvPIDate.Name = "aRcvPIDate"
        Me.aRcvPIDate.OptionsColumn.AllowEdit = False
        Me.aRcvPIDate.OptionsColumn.ReadOnly = True
        Me.aRcvPIDate.Visible = True
        Me.aRcvPIDate.VisibleIndex = 7
        Me.aRcvPIDate.Width = 77
        '
        'aPISuplCFMDeliveryDate
        '
        Me.aPISuplCFMDeliveryDate.Caption = "PI Confirm Delivery Date"
        Me.aPISuplCFMDeliveryDate.FieldName = "PISuplCFMDeliveryDate"
        Me.aPISuplCFMDeliveryDate.MinWidth = 21
        Me.aPISuplCFMDeliveryDate.Name = "aPISuplCFMDeliveryDate"
        Me.aPISuplCFMDeliveryDate.OptionsColumn.AllowEdit = False
        Me.aPISuplCFMDeliveryDate.OptionsColumn.ReadOnly = True
        Me.aPISuplCFMDeliveryDate.Visible = True
        Me.aPISuplCFMDeliveryDate.VisibleIndex = 8
        Me.aPISuplCFMDeliveryDate.Width = 77
        '
        'aInvoiceNo
        '
        Me.aInvoiceNo.Caption = "Invoice No"
        Me.aInvoiceNo.FieldName = "InvoiceNo"
        Me.aInvoiceNo.MinWidth = 21
        Me.aInvoiceNo.Name = "aInvoiceNo"
        Me.aInvoiceNo.OptionsColumn.AllowEdit = False
        Me.aInvoiceNo.OptionsColumn.ReadOnly = True
        Me.aInvoiceNo.Visible = True
        Me.aInvoiceNo.VisibleIndex = 9
        Me.aInvoiceNo.Width = 129
        '
        'aInvoiceDate
        '
        Me.aInvoiceDate.Caption = "PI Date/Invoice Date"
        Me.aInvoiceDate.FieldName = "aInvoiceDate"
        Me.aInvoiceDate.MinWidth = 21
        Me.aInvoiceDate.Name = "aInvoiceDate"
        Me.aInvoiceDate.OptionsColumn.AllowEdit = False
        Me.aInvoiceDate.OptionsColumn.ReadOnly = True
        Me.aInvoiceDate.Visible = True
        Me.aInvoiceDate.VisibleIndex = 10
        Me.aInvoiceDate.Width = 77
        '
        'aPurchaseDate
        '
        Me.aPurchaseDate.Caption = "Purchase Date"
        Me.aPurchaseDate.FieldName = "PurchaseDate"
        Me.aPurchaseDate.MinWidth = 21
        Me.aPurchaseDate.Name = "aPurchaseDate"
        Me.aPurchaseDate.OptionsColumn.AllowEdit = False
        Me.aPurchaseDate.OptionsColumn.ReadOnly = True
        Me.aPurchaseDate.Visible = True
        Me.aPurchaseDate.VisibleIndex = 11
        Me.aPurchaseDate.Width = 77
        '
        'aPurchaseBy
        '
        Me.aPurchaseBy.Caption = "Purchase By"
        Me.aPurchaseBy.FieldName = "PurchaseBy"
        Me.aPurchaseBy.MinWidth = 21
        Me.aPurchaseBy.Name = "aPurchaseBy"
        Me.aPurchaseBy.OptionsColumn.AllowEdit = False
        Me.aPurchaseBy.OptionsColumn.ReadOnly = True
        Me.aPurchaseBy.Visible = True
        Me.aPurchaseBy.VisibleIndex = 12
        Me.aPurchaseBy.Width = 129
        '
        'aSupplierCode
        '
        Me.aSupplierCode.Caption = "Vendor Code"
        Me.aSupplierCode.FieldName = "SupplierCode"
        Me.aSupplierCode.MinWidth = 21
        Me.aSupplierCode.Name = "aSupplierCode"
        Me.aSupplierCode.OptionsColumn.AllowEdit = False
        Me.aSupplierCode.OptionsColumn.ReadOnly = True
        Me.aSupplierCode.Visible = True
        Me.aSupplierCode.VisibleIndex = 13
        Me.aSupplierCode.Width = 103
        '
        'aSupplierName
        '
        Me.aSupplierName.Caption = "Vendor Name"
        Me.aSupplierName.FieldName = "SupplierName"
        Me.aSupplierName.MinWidth = 21
        Me.aSupplierName.Name = "aSupplierName"
        Me.aSupplierName.OptionsColumn.AllowEdit = False
        Me.aSupplierName.OptionsColumn.ReadOnly = True
        Me.aSupplierName.Visible = True
        Me.aSupplierName.VisibleIndex = 14
        Me.aSupplierName.Width = 214
        '
        'aCurrency
        '
        Me.aCurrency.Caption = "Currency"
        Me.aCurrency.FieldName = "Currency"
        Me.aCurrency.MinWidth = 21
        Me.aCurrency.Name = "aCurrency"
        Me.aCurrency.OptionsColumn.AllowEdit = False
        Me.aCurrency.OptionsColumn.ReadOnly = True
        Me.aCurrency.Visible = True
        Me.aCurrency.VisibleIndex = 15
        Me.aCurrency.Width = 77
        '
        'aDeliveryDate
        '
        Me.aDeliveryDate.Caption = "Delivery Date"
        Me.aDeliveryDate.FieldName = "DeliveryDate"
        Me.aDeliveryDate.MinWidth = 21
        Me.aDeliveryDate.Name = "aDeliveryDate"
        Me.aDeliveryDate.OptionsColumn.AllowEdit = False
        Me.aDeliveryDate.OptionsColumn.ReadOnly = True
        Me.aDeliveryDate.Visible = True
        Me.aDeliveryDate.VisibleIndex = 16
        Me.aDeliveryDate.Width = 77
        '
        'aCompany
        '
        Me.aCompany.Caption = "Company Name"
        Me.aCompany.FieldName = "Company"
        Me.aCompany.MinWidth = 21
        Me.aCompany.Name = "aCompany"
        Me.aCompany.OptionsColumn.AllowEdit = False
        Me.aCompany.OptionsColumn.ReadOnly = True
        Me.aCompany.Visible = True
        Me.aCompany.VisibleIndex = 17
        Me.aCompany.Width = 77
        '
        'aBuy
        '
        Me.aBuy.Caption = "Buy Month"
        Me.aBuy.FieldName = "Buy"
        Me.aBuy.MinWidth = 21
        Me.aBuy.Name = "aBuy"
        Me.aBuy.OptionsColumn.AllowEdit = False
        Me.aBuy.OptionsColumn.ReadOnly = True
        Me.aBuy.Visible = True
        Me.aBuy.VisibleIndex = 18
        Me.aBuy.Width = 77
        '
        'aPOUnit
        '
        Me.aPOUnit.Caption = "Unit"
        Me.aPOUnit.FieldName = "POUnit"
        Me.aPOUnit.MinWidth = 21
        Me.aPOUnit.Name = "aPOUnit"
        Me.aPOUnit.OptionsColumn.AllowEdit = False
        Me.aPOUnit.OptionsColumn.ReadOnly = True
        Me.aPOUnit.Visible = True
        Me.aPOUnit.VisibleIndex = 19
        Me.aPOUnit.Width = 77
        '
        'aPOQty
        '
        Me.aPOQty.Caption = "Total PO Quantity"
        Me.aPOQty.DisplayFormat.FormatString = "{0:n4}"
        Me.aPOQty.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.aPOQty.FieldName = "POQty"
        Me.aPOQty.MinWidth = 21
        Me.aPOQty.Name = "aPOQty"
        Me.aPOQty.OptionsColumn.AllowEdit = False
        Me.aPOQty.OptionsColumn.ReadOnly = True
        Me.aPOQty.Visible = True
        Me.aPOQty.VisibleIndex = 20
        Me.aPOQty.Width = 86
        '
        'aPOAmount
        '
        Me.aPOAmount.Caption = "Total Amount"
        Me.aPOAmount.DisplayFormat.FormatString = "{0:n2}"
        Me.aPOAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.aPOAmount.FieldName = "POAmount"
        Me.aPOAmount.MinWidth = 21
        Me.aPOAmount.Name = "aPOAmount"
        Me.aPOAmount.OptionsColumn.AllowEdit = False
        Me.aPOAmount.OptionsColumn.ReadOnly = True
        Me.aPOAmount.Visible = True
        Me.aPOAmount.VisibleIndex = 21
        Me.aPOAmount.Width = 86
        '
        'aPOOutstandingQty
        '
        Me.aPOOutstandingQty.Caption = "PO Outstanding Qty"
        Me.aPOOutstandingQty.DisplayFormat.FormatString = "{0:n4}"
        Me.aPOOutstandingQty.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.aPOOutstandingQty.FieldName = "POOutstandingQty"
        Me.aPOOutstandingQty.MinWidth = 21
        Me.aPOOutstandingQty.Name = "aPOOutstandingQty"
        Me.aPOOutstandingQty.OptionsColumn.AllowEdit = False
        Me.aPOOutstandingQty.OptionsColumn.ReadOnly = True
        Me.aPOOutstandingQty.Visible = True
        Me.aPOOutstandingQty.VisibleIndex = 22
        Me.aPOOutstandingQty.Width = 86
        '
        'aSentDocToAccDate
        '
        Me.aSentDocToAccDate.Caption = "Submit Document Date"
        Me.aSentDocToAccDate.FieldName = "SentDocToAccDate"
        Me.aSentDocToAccDate.MinWidth = 21
        Me.aSentDocToAccDate.Name = "aSentDocToAccDate"
        Me.aSentDocToAccDate.OptionsColumn.AllowEdit = False
        Me.aSentDocToAccDate.OptionsColumn.ReadOnly = True
        Me.aSentDocToAccDate.Visible = True
        Me.aSentDocToAccDate.VisibleIndex = 23
        Me.aSentDocToAccDate.Width = 77
        '
        'aFinishbalancePaymentDate
        '
        Me.aFinishbalancePaymentDate.Caption = "Complete Payment Date"
        Me.aFinishbalancePaymentDate.FieldName = "FinishbalancePaymentDate"
        Me.aFinishbalancePaymentDate.MinWidth = 21
        Me.aFinishbalancePaymentDate.Name = "aFinishbalancePaymentDate"
        Me.aFinishbalancePaymentDate.OptionsColumn.AllowEdit = False
        Me.aFinishbalancePaymentDate.OptionsColumn.ReadOnly = True
        Me.aFinishbalancePaymentDate.Visible = True
        Me.aFinishbalancePaymentDate.VisibleIndex = 24
        Me.aFinishbalancePaymentDate.Width = 77
        '
        'aPIQuantity
        '
        Me.aPIQuantity.Caption = "PI Quantity"
        Me.aPIQuantity.DisplayFormat.FormatString = "{0:n4}"
        Me.aPIQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.aPIQuantity.FieldName = "PIQuantity"
        Me.aPIQuantity.MinWidth = 21
        Me.aPIQuantity.Name = "aPIQuantity"
        Me.aPIQuantity.OptionsColumn.AllowEdit = False
        Me.aPIQuantity.OptionsColumn.ReadOnly = True
        Me.aPIQuantity.Visible = True
        Me.aPIQuantity.VisibleIndex = 25
        Me.aPIQuantity.Width = 86
        '
        'aPINetAmt
        '
        Me.aPINetAmt.Caption = "Total PI Amount"
        Me.aPINetAmt.DisplayFormat.FormatString = "{0:n2}"
        Me.aPINetAmt.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.aPINetAmt.FieldName = "PINetAmt"
        Me.aPINetAmt.MinWidth = 21
        Me.aPINetAmt.Name = "aPINetAmt"
        Me.aPINetAmt.OptionsColumn.AllowEdit = False
        Me.aPINetAmt.OptionsColumn.ReadOnly = True
        Me.aPINetAmt.Visible = True
        Me.aPINetAmt.VisibleIndex = 26
        Me.aPINetAmt.Width = 86
        '
        'aPIDocCNAmt
        '
        Me.aPIDocCNAmt.Caption = "Credit Note Amount"
        Me.aPIDocCNAmt.DisplayFormat.FormatString = "{0:n2}"
        Me.aPIDocCNAmt.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.aPIDocCNAmt.FieldName = "PIDocCNAmt"
        Me.aPIDocCNAmt.MinWidth = 21
        Me.aPIDocCNAmt.Name = "aPIDocCNAmt"
        Me.aPIDocCNAmt.OptionsColumn.AllowEdit = False
        Me.aPIDocCNAmt.OptionsColumn.ReadOnly = True
        Me.aPIDocCNAmt.Visible = True
        Me.aPIDocCNAmt.VisibleIndex = 27
        Me.aPIDocCNAmt.Width = 86
        '
        'aPIDocDNAmt
        '
        Me.aPIDocDNAmt.Caption = "Debit Note Amount"
        Me.aPIDocDNAmt.DisplayFormat.FormatString = "{0:n2}"
        Me.aPIDocDNAmt.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.aPIDocDNAmt.FieldName = "PIDocDNAmt"
        Me.aPIDocDNAmt.MinWidth = 21
        Me.aPIDocDNAmt.Name = "aPIDocDNAmt"
        Me.aPIDocDNAmt.OptionsColumn.AllowEdit = False
        Me.aPIDocDNAmt.OptionsColumn.ReadOnly = True
        Me.aPIDocDNAmt.Visible = True
        Me.aPIDocDNAmt.VisibleIndex = 28
        Me.aPIDocDNAmt.Width = 86
        '
        'aPIDocSurchargeAmt
        '
        Me.aPIDocSurchargeAmt.Caption = "Surcharge Amount"
        Me.aPIDocSurchargeAmt.DisplayFormat.FormatString = "{0:n2}"
        Me.aPIDocSurchargeAmt.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.aPIDocSurchargeAmt.FieldName = "PIDocSurchargeAmt"
        Me.aPIDocSurchargeAmt.MinWidth = 21
        Me.aPIDocSurchargeAmt.Name = "aPIDocSurchargeAmt"
        Me.aPIDocSurchargeAmt.OptionsColumn.AllowEdit = False
        Me.aPIDocSurchargeAmt.OptionsColumn.ReadOnly = True
        Me.aPIDocSurchargeAmt.Visible = True
        Me.aPIDocSurchargeAmt.VisibleIndex = 29
        Me.aPIDocSurchargeAmt.Width = 86
        '
        'aPIDocNetAmt
        '
        Me.aPIDocNetAmt.Caption = "Net Amount"
        Me.aPIDocNetAmt.DisplayFormat.FormatString = "{0:n2}"
        Me.aPIDocNetAmt.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.aPIDocNetAmt.FieldName = "PIDocNetAmt"
        Me.aPIDocNetAmt.MinWidth = 21
        Me.aPIDocNetAmt.Name = "aPIDocNetAmt"
        Me.aPIDocNetAmt.OptionsColumn.AllowEdit = False
        Me.aPIDocNetAmt.OptionsColumn.ReadOnly = True
        Me.aPIDocNetAmt.Visible = True
        Me.aPIDocNetAmt.VisibleIndex = 30
        Me.aPIDocNetAmt.Width = 86
        '
        'aNote
        '
        Me.aNote.Caption = "Remark"
        Me.aNote.FieldName = "Note"
        Me.aNote.MinWidth = 21
        Me.aNote.Name = "aNote"
        Me.aNote.OptionsColumn.AllowEdit = False
        Me.aNote.OptionsColumn.ReadOnly = True
        Me.aNote.Visible = True
        Me.aNote.VisibleIndex = 31
        Me.aNote.Width = 214
        '
        'aFTStateHasFile
        '
        Me.aFTStateHasFile.Caption = "PDF File"
        Me.aFTStateHasFile.ColumnEdit = Me.RepositoryItemCheckEdit2
        Me.aFTStateHasFile.FieldName = "FTStateHasFile"
        Me.aFTStateHasFile.MinWidth = 21
        Me.aFTStateHasFile.Name = "aFTStateHasFile"
        Me.aFTStateHasFile.OptionsColumn.AllowEdit = False
        Me.aFTStateHasFile.OptionsColumn.ReadOnly = True
        Me.aFTStateHasFile.Visible = True
        Me.aFTStateHasFile.VisibleIndex = 32
        Me.aFTStateHasFile.Width = 77
        '
        'RepositoryItemCheckEdit2
        '
        Me.RepositoryItemCheckEdit2.AutoHeight = False
        Me.RepositoryItemCheckEdit2.Caption = "Check"
        Me.RepositoryItemCheckEdit2.Name = "RepositoryItemCheckEdit2"
        Me.RepositoryItemCheckEdit2.ValueChecked = "1"
        Me.RepositoryItemCheckEdit2.ValueUnchecked = "0"
        '
        'wFabricflowsTrackig
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1137, 450)
        Me.Controls.Add(Me.PopupOrderNo)
        Me.Controls.Add(Me.ogbmainprocbutton)
        Me.Controls.Add(Me.otbmain)
        Me.Controls.Add(Me.ogbdetail)
        Me.Name = "wFabricflowsTrackig"
        Me.Text = "Fabric flows Tracking"
        CType(Me.ogbdetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbdetail.ResumeLayout(False)
        CType(Me.FTSupl.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNFabricFlowsListType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupSupplier.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PopupOrderNo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PopupOrderNo.ResumeLayout(False)
        CType(Me.ockselectorderall.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogdlistsupplier, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvlistsupplier, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemSelectsupl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbmainprocbutton.ResumeLayout(False)
        CType(Me.ogdpo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvpo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepCheckEdit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.oDockManager, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.otbmain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.otbmain.ResumeLayout(False)
        Me.otpnew.ResumeLayout(False)
        Me.otpworking.ResumeLayout(False)
        CType(Me.ogdwpo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvwpo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.otppayment.ResumeLayout(False)
        CType(Me.ogdpopayment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvpopayment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogbdetail As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ogdpo As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvpo As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents ogbmainprocbutton As DevExpress.XtraEditors.PanelControl
    Friend WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmclear As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmload As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents oDockManager As DevExpress.XtraBars.Docking.DockManager
    Friend WithEvents RepCheckEdit As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents FNFabricFlowsListType_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNFabricFlowsListType As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents otbmain As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents otpnew As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents otpworking As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents ogdwpo As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvwpo As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents RepositoryItemCheckEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents otppayment As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents ogdpopayment As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvpopayment As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents RepositoryItemCheckEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents ocmsavelayout As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents StateFlagNew As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents VenderCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents VendorName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents VendorLocation As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FactoryCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents PONo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents PODate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents Shipto As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GarmentShipmentDestination As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents MatrClass As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents POItemCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents MatrCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents UPCCOMBOIM As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ContentCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CareCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents Color As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColorName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCW As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents Size As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents SizeMatrix As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents Currency As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents Price As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents QtyUnit As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents DeliveryDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents Season As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents Custporef As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents Buy As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents BuyNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents Category As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents Program As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents SubProgram As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents StyleNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents StyleName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents PORefType As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents POMatching1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents POMatching2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents POMatching3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents POMatching4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents POMatching5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ItemMatching1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ItemMatching2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ItemMatching3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ItemMatching4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ItemMatching5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents Position As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents Type As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents PaymentTerm As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents Remarkfrommer As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RemarkForPurchase As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CompanyName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents address1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents address2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents address3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents address4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents sysowner As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents sysownername As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents sysownermail As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ZeroInspection As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GarmentShip As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents OGACDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents HITLink As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents NIKECustomerPONo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents QRS As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents PromoQty As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents MOQ As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ActualQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents Quantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents POUploadDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents POUploadTime As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents POUploadBy As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CountryOfOrigin As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents SaleOrderSaleOrderLine As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents NikeSAPPOPONIKEPOLine As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents NikematerialStyleColorway As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents Modifire1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents Modifire2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents Modifire3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents MPOHZ As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ItemDescription As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents BulkQRSSample As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CCTotalpage As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents Surcharge As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ChinaInsertCard As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents P1pc2in1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents WovenLabelSizeLength As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ArgentinaImportNumber As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents DownFill As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents SYS_ID As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ChinasizeMatrixtype As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents EAGERPItemNumber As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CompoundColororCCIMfor2in1CCIM As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CPO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents BhasaIndonesiaProductBrand As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents SAFCODE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents Youthorder As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents NFCproduct As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents NeckneckopeningX2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ChestbodywidthX2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CenterBackbodylength As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents WaistwaistrelaxedInseam As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents PackQuantityQTYPERSELLINGUNIT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents Fabriccode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents PRODUCTDESCRIPTION As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents PRODUCTCODEDESCRIPTION As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GARMENTSTYLEFIELD As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INSEAMSTYLE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents EndCustomer As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents PIStateEdit As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents PIStateAccept As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents xVendorLocation As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents xVendorName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents sFactoryCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents sPONo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents sPODate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents sBuy As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents sBuyNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents sSeason As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents sPORefType As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents sGarmentShipmentDestination As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents sMatrCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents sPOItemCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents sStyleNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents sColor As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents sColorName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents sQRS As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents sPromoQty As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents sMOQ As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents sActualQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents sQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents sQtyUnit As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents sPrice As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents sSurcharge As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents sSubProgram As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents T2_Confirm_OrderNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents T2_Confirm_PO_Date As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents Estimatedeldate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents T2_Confirm_Price As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents T2_Confirm_Quantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents T2_Confirm_MOQQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNPIQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents Doc_Surcharge_Amount As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNPINetAmt As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents T2_Confirm_Ship_Date As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents T2_Confirm_ShipQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents Actualdeldate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents T2_Confirm_Ship_Date2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents T2_Confirm_ShipQuantity2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents Actualdeldate2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents POBalanceQty As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents InvoiceNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTAWBNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents T2_Confirm_By As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents T2_Confirm_Note As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTStateHasFile As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents sCategory As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents sRemarkForPurchase As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents sShipto As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents xMatrClass As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents sGCW As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents sSize As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ssCurrency As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents sDeliveryDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents sOGACDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents sRcvQty As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents sRcvDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RcvBalanceQty As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents AcknowledgeBy As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents AcknowledgeDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents AcknowledgeTime As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents StateAcknowledgeLock As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents sVenderCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents sEndCustomer As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ssysownername As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ssysownermail As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents StateRejectedBuy As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents sRejectedBuyBy As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RejectedBuyDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RejectedBuyTime As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RejectedBuyNote As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents aPONo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents PayType As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents sPaymentTerm As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents aPaymentDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents LCNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents aPINo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents aPIDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents aRcvPIDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents aPISuplCFMDeliveryDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents aInvoiceNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents aInvoiceDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents aPurchaseDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents aPurchaseBy As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents aSupplierCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents aSupplierName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents aCurrency As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents aDeliveryDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents aCompany As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents aBuy As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents aPOUnit As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents aPOQty As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents aPOAmount As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents aPOOutstandingQty As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents aSentDocToAccDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents aFinishbalancePaymentDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents aPIQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents aPINetAmt As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents aPIDocCNAmt As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents aPIDocDNAmt As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents aPIDocSurchargeAmt As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents aPIDocNetAmt As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents aNote As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents aFTStateHasFile As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents Supplier_lbl As LabelControl
    Friend WithEvents RepositoryItemPopupSupplier As PopupContainerEdit
    Friend WithEvents PopupOrderNo As PopupContainerControl
    Friend WithEvents ockselectorderall As CheckEdit
    Friend WithEvents ogdlistsupplier As GridControl
    Friend WithEvents ogvlistsupplier As GridView
    Friend WithEvents xOrderFTSelect As GridColumn
    Friend WithEvents xFTSupplierCode As GridColumn
    Friend WithEvents xxx3FTSupplierCode As GridColumn
    Friend WithEvents RepositoryItemSelectsupl As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents FTSupl As TextEdit
    Friend WithEvents xFTSelect As GridColumn
    Friend WithEvents dFTSelect As GridColumn
    Friend WithEvents ocmdownloadpopdf As SimpleButton
End Class

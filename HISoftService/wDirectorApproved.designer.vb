<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wDirectorApproved
    Inherits DevExpress.XtraBars.Ribbon.RibbonForm

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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(wDirectorApproved))
        Me.MainRibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.mnusysabout = New DevExpress.XtraBars.BarButtonItem()
        Me.FTUserLogINIP = New DevExpress.XtraBars.BarStaticItem()
        Me.RepositoryItemComboBox1 = New DevExpress.XtraEditors.Repository.RepositoryItemComboBox()
        Me.RibbonStatusBar = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.MainDefaultLookAndFeel = New DevExpress.LookAndFeel.DefaultLookAndFeel(Me.components)
        Me.oDockManager = New DevExpress.XtraBars.Docking.DockManager(Me.components)
        Me.StandaloneBarDockControl = New DevExpress.XtraBars.StandaloneBarDockControl()
        Me.ogApprovedMail = New DevExpress.XtraEditors.GroupControl()
        Me.ochkselectall = New DevExpress.XtraEditors.CheckEdit()
        Me.ogDirectorApproved = New DevExpress.XtraGrid.GridControl()
        Me.ogvDirectorApproved = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.ColFTStateApproved = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCheckEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.ColFTPurchaseBy = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColFTSuperVisorName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColFTPurchaseNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColFDPurchaseDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColFTPurchaseState = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColFTCmpRunCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColFTCmpRunName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColFTSuplCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColFTSuplName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColFDDeliveryDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColFTCrTermCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColFTCrTermDesc = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColFNCreditDay = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColFTTermOfPMCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColFTTermOfPMName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColFTDeliveryCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColFTDeliveryDesc = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTCurCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColFNExchangeRate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColFTContactPerson = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColFNDisCountAmt = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColFNSurcharge = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColFNVatAmt = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColFNPOGrandAmt = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTQuantityinfo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColFTRemark = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColFNDisCountPer = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColFNPONetAmt = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColFNVatPer = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColFTTeamGrpCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColFTTeamGrpName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColFTPurGrpCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColFTPurGrpName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTPoTypeState = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTStateFree = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.otmchkpo = New System.Windows.Forms.Timer(Me.components)
        Me.otbmain = New DevExpress.XtraTab.XtraTabControl()
        Me.otpordercost = New DevExpress.XtraTab.XtraTabPage()
        Me.ogrpApprovedordercost = New DevExpress.XtraEditors.GroupControl()
        Me.ogcordercost = New DevExpress.XtraGrid.GridControl()
        Me.ogvordercost = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridView()
        Me.gridBand3 = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.BandedGridColumn2 = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.RepositoryItemCheckEdit1FTSelect = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.gCmpName = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.FTCmpName = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.gFTInvoice = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.FTInvoiceNo = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.gInvoiceDate = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.FDInvoiceDate = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.gFTOrderNo = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.FTOrderNo = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.gFTPORef = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.FTPORef = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.gFTStyle = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.FTStyleCode = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.gFNExportQuantity = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.FNExportQuantity = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.gFNExportAmt = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.FNExportAmtTHB = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.gFNExportAmtus = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.FNExportAmt = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.gCenter = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.gCenter1 = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.FNFabricCostPer = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.gCenter2 = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.FNFabricCost = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.gCenter3 = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.FNAccessroryCostPer = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.gCenter4 = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.BandedGridColumn1 = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.gFNFabricAccMinCostPer = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.FNFabricAccMinCostPer = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.gFabricAccMin = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.FNFabricAccMinCost = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.gCenter5 = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.FNAccFabStockCostPer = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.gCenter6 = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.FNAccFabStockCost = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.cFNFabricAccStockOtherCostPer = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.FNFabricAccStockOtherCostPer = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.gOtherStock = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.FNFabricAccStockOtherCost = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.gFNOtherServiceCostPer = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.FNOtherServiceCostPer = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.gOtherServiceCost = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.FNOtherServiceCost = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.gFNIncomeAfterRawmaterialPer = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.FNIncomeAfterRawmaterialPer = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.gFNIncomeAferRawmaterial = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.FNIncomeAfterRawmaterial = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.RepFNIncomeAfterRawmaterial = New DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit()
        Me.gCenter7 = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.FNConductedCostPer = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.gCenter8 = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.FNConductedCost = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.gCenter9 = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.FNOtherCostPer = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.gCenter10 = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.FNOtherCost = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.gCenter11 = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.FNEmbFacCostPer = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.gCenter12 = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.FNEmbFacCost = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.gCenter13 = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.FNEmpPrintBranchPer = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.gCenter14 = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.FNEmpPrintBranch = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.gFNWagePullPer = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.FNWagePullPer = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.gFNWagePull = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.FNWagePull = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.gBtanch = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.gBtanch1 = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.FNExportAmtBFPer = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.gBtanch2 = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.FNExportAmtBF = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.gBtanch3 = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.FNWageCostPer = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.gBtanch4 = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.FNWageCost = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.RepFNWageCost = New DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit()
        Me.gFNSewCostPer = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.FNSewCostPer = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.gSewCost = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.FNSewCost = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.gEmbroideryCostPer = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.FNEmbroideryCostPer = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.gEmbroidCost = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.FNEmbroideryCost = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.gFNPrintCostPer = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.FNPrintCostPer = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.gPrintCost = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.FNPrintCost = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.gBtanch5 = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.FNEmpPrintSubPer = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.gBtanch6 = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.FNEmpPrintSub = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.gFNImportCostPer = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.FNImportCostPer = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.gImportCost = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.FNImportCost = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.gBtanch7 = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.FNImportExportCostPer = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.gBtanch8 = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.FNImportExportCost = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.gBtanch9 = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.FNProdCostPer = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.gBtanch10 = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.FNProdCost = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.RepFNProdCost = New DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit()
        Me.gBtanch11 = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.FNCommissionCostPer = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.gBtanch12 = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.FNCommissionCost = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.gBtanch13 = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.FNTransportAirCostPer = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.gBtanch14 = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.FNTransportAirCost = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.gEtcCostper = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.FNEtcCostPer = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.gEtcCost = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.FNEtcCost = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.gProfit = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.gProfit1 = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.FNNetProfitPer = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.gProfit2 = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.FNNetProfit = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.gFNNetProfitRcv = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.FNNetProfitRcv = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.gFNOrderQuantity = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.FNOrderQuantity = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.gFNExport = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.gridBand1 = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.FNExportQuantityTo = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.gridBand2 = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.FNExportQuantityOtherMonth = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.รวม = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.FNTotalExport = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.gdiff = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.FNOrderQuantityBal = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.gridBand4 = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.FNPrice = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.gdiffper = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.FNLostPer = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.CFNStateRow = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.FTSelectAllOrderCost = New DevExpress.XtraEditors.CheckEdit()
        Me.otpapppo = New DevExpress.XtraTab.XtraTabPage()
        Me.otpappfactory = New DevExpress.XtraTab.XtraTabPage()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.ogcdirector = New DevExpress.XtraGrid.GridControl()
        Me.ogvdirector = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.C2FNHSysCmpId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTSelect = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositorySelect = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.C2FTCmpCodeTo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.C2FTCmpCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.C2FTCompName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.C2FTPORef = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.C2FNHSysStyleId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.C2FTStyleCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.C2FTOrderNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.C2FNHSysCustId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.C2FTCustCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.C2FTCustName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.C2FNHSysBuyId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.C2FTBuyCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.C2FNHSysSeasonId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.C2FTSeasonCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.C2FNHSysProdTypeId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.C2FTProdTypeCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.C2FTProdTypeName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.C2FDShipDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.C2FTCurCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.C2FNQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemTextEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.C2FNAmt = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.C2FNExtraQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.C2FNAmntExtra = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFNGarmentQtyTest = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNAmntQtyTest = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.C2FNTotalQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNGrandAmnt = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.C2FTStateEmb = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.C2FTStatePrint = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.C2FTStateHeat = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.C2FTStateLaser = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.C2FTStateWindows = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.C2FTStateOrderApp = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.C2FTAppBy = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.C2FDAppDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.C2FTAppTime = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTStateSendDirectorApp = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTStateSendDirectorBy = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FDStateSendDirectorDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTStateSendDirectorTime = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTStateDirectorApp = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTStateDirectorAppBy = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FDStateDirectorAppDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTStateDirectorAppTime = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTStateDirectorReject = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTStateDirectorRejectBy = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FDStateDirectorRejectDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTStateDirectorRejectTime = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.C33FTColorway = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.C33FTNikePOLineItem = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.C33FTSizeBreakDown = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNCM = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNGrandAmtCM = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ockdirectorselectall = New DevExpress.XtraEditors.CheckEdit()
        Me.otpappdocument = New DevExpress.XtraTab.XtraTabPage()
        Me.ogbdocument = New DevExpress.XtraEditors.GroupControl()
        Me.ogcdocument = New DevExpress.XtraGrid.GridControl()
        Me.ogvdocument = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.cFTSelect = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryFTSelect = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.cFTDocumentNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFDDocumentDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTDocumentBy = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTCmpCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTCmpName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTDocumentTitle = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTNote = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTBenefit = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNOperActivity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTOperActivityName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTDocName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTDocRefCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTDocTypeName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTFileTypeName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFBDocument = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNDocType = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNHSysCmpId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryFTSandApprove = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.FTSelectAllDC = New DevExpress.XtraEditors.CheckEdit()
        Me.otpleaveapprove = New DevExpress.XtraTab.XtraTabPage()
        Me.ogbapproveleave = New DevExpress.XtraEditors.GroupControl()
        Me.ogcleaveapp = New DevExpress.XtraGrid.GridControl()
        Me.ogvapproveleave = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.FNHSysEmpID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemFTSelect = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.cFTEmpName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColFTStartDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColFTEndDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNSumTotalLeaveDay = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTLeaveType = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTLeaveTypeName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTStaLeaveDayName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColFTHoliday = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepColFTHoliday = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.FTLeavePay = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepFTLeavePay = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.FTLeaveStartTime = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTLeaveEndTime = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNLeaveTotalTime = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNLeaveTotalDay = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTStaCalSSO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepFTStaCalSSO = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.FTStaLeaveDay = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTLeaveNote = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColFTStateMedicalCertificate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColFTMedicalCertificateName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFTInsUser = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFTInsDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFTInsTime = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFTStateDeductVacation = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepFTApproveState = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.cFTStateType = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNLeaveTotalTimeMin = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTMngApproveState = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCFTMngApproveState = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.FTMngApproveBy = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTMngApproveTime = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FDMngApproveDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTApproveState = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.SickLeave = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.BusinessLeave = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.VacationLeave = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTEmpTypeName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTSectName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTUnitSectName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCheckEdit2 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.RepositoryFTApproveState = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.RepColFTLeavePay = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.FTSelectAllleave = New DevExpress.XtraEditors.CheckEdit()
        Me.ocmapprove = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmreject = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmpreview = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmsavelayout = New DevExpress.XtraEditors.SimpleButton()
        Me.ogbmainprocbutton = New DevExpress.XtraEditors.PanelControl()
        CType(Me.MainRibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemComboBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.oDockManager, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogApprovedMail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogApprovedMail.SuspendLayout()
        CType(Me.ochkselectall.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogDirectorApproved, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvDirectorApproved, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.otbmain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.otbmain.SuspendLayout()
        Me.otpordercost.SuspendLayout()
        CType(Me.ogrpApprovedordercost, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogrpApprovedordercost.SuspendLayout()
        CType(Me.ogcordercost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvordercost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit1FTSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepFNIncomeAfterRawmaterial, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepFNWageCost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepFNProdCost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTSelectAllOrderCost.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.otpapppo.SuspendLayout()
        Me.otpappfactory.SuspendLayout()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.ogcdirector, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvdirector, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositorySelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ockdirectorselectall.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.otpappdocument.SuspendLayout()
        CType(Me.ogbdocument, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbdocument.SuspendLayout()
        CType(Me.ogcdocument, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvdocument, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryFTSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryFTSandApprove, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTSelectAllDC.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.otpleaveapprove.SuspendLayout()
        CType(Me.ogbapproveleave, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbapproveleave.SuspendLayout()
        CType(Me.ogcleaveapp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvapproveleave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemFTSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepColFTHoliday, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepFTLeavePay, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepFTStaCalSSO, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepFTApproveState, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCFTMngApproveState, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryFTApproveState, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepColFTLeavePay, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTSelectAllleave.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbmainprocbutton.SuspendLayout()
        Me.SuspendLayout()
        '
        'MainRibbonControl
        '
        Me.MainRibbonControl.ApplicationButtonText = Nothing
        Me.MainRibbonControl.ExpandCollapseItem.Id = 0
        Me.MainRibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.MainRibbonControl.ExpandCollapseItem, Me.mnusysabout, Me.FTUserLogINIP, Me.MainRibbonControl.SearchEditItem})
        Me.MainRibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.MainRibbonControl.Margin = New System.Windows.Forms.Padding(6, 5, 6, 5)
        Me.MainRibbonControl.MaxItemId = 5
        Me.MainRibbonControl.Name = "MainRibbonControl"
        Me.MainRibbonControl.OptionsPageCategories.Alignment = DevExpress.XtraBars.Ribbon.RibbonPageCategoryAlignment.Left
        Me.MainRibbonControl.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemComboBox1})
        Me.MainRibbonControl.ShowExpandCollapseButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.MainRibbonControl.ShowToolbarCustomizeItem = False
        Me.MainRibbonControl.Size = New System.Drawing.Size(1687, 54)
        Me.MainRibbonControl.StatusBar = Me.RibbonStatusBar
        Me.MainRibbonControl.Toolbar.ShowCustomizeItem = False
        '
        'mnusysabout
        '
        Me.mnusysabout.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right
        Me.mnusysabout.Caption = "About"
        Me.mnusysabout.Id = 2
        Me.mnusysabout.Name = "mnusysabout"
        '
        'FTUserLogINIP
        '
        Me.FTUserLogINIP.AutoSize = DevExpress.XtraBars.BarStaticItemSize.None
        Me.FTUserLogINIP.Id = 3
        Me.FTUserLogINIP.Name = "FTUserLogINIP"
        Me.FTUserLogINIP.TextAlignment = System.Drawing.StringAlignment.Center
        Me.FTUserLogINIP.Width = 150
        '
        'RepositoryItemComboBox1
        '
        Me.RepositoryItemComboBox1.AutoHeight = False
        Me.RepositoryItemComboBox1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemComboBox1.Name = "RepositoryItemComboBox1"
        '
        'RibbonStatusBar
        '
        Me.RibbonStatusBar.ItemLinks.Add(Me.mnusysabout)
        Me.RibbonStatusBar.ItemLinks.Add(Me.FTUserLogINIP)
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 881)
        Me.RibbonStatusBar.Margin = New System.Windows.Forms.Padding(6, 5, 6, 5)
        Me.RibbonStatusBar.Name = "RibbonStatusBar"
        Me.RibbonStatusBar.Ribbon = Me.MainRibbonControl
        Me.RibbonStatusBar.Size = New System.Drawing.Size(1687, 27)
        '
        'MainDefaultLookAndFeel
        '
        Me.MainDefaultLookAndFeel.LookAndFeel.SkinName = "McSkin"
        '
        'oDockManager
        '
        Me.oDockManager.Form = Me
        Me.oDockManager.TopZIndexControls.AddRange(New String() {"DevExpress.XtraBars.BarDockControl", "DevExpress.XtraBars.StandaloneBarDockControl", "System.Windows.Forms.StatusBar", "DevExpress.XtraBars.Ribbon.RibbonStatusBar", "DevExpress.XtraBars.Ribbon.RibbonControl"})
        '
        'StandaloneBarDockControl
        '
        Me.StandaloneBarDockControl.AutoSize = True
        Me.StandaloneBarDockControl.CausesValidation = False
        Me.StandaloneBarDockControl.Dock = System.Windows.Forms.DockStyle.Top
        Me.StandaloneBarDockControl.Location = New System.Drawing.Point(0, 54)
        Me.StandaloneBarDockControl.Manager = Nothing
        Me.StandaloneBarDockControl.Margin = New System.Windows.Forms.Padding(4)
        Me.StandaloneBarDockControl.Name = "StandaloneBarDockControl"
        Me.StandaloneBarDockControl.Size = New System.Drawing.Size(1687, 0)
        Me.StandaloneBarDockControl.Text = "StandaloneBarDockControl1"
        '
        'ogApprovedMail
        '
        Me.ogApprovedMail.AppearanceCaption.Options.UseTextOptions = True
        Me.ogApprovedMail.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ogApprovedMail.Controls.Add(Me.ochkselectall)
        Me.ogApprovedMail.Controls.Add(Me.ogDirectorApproved)
        Me.ogApprovedMail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogApprovedMail.Location = New System.Drawing.Point(0, 0)
        Me.ogApprovedMail.Margin = New System.Windows.Forms.Padding(4)
        Me.ogApprovedMail.Name = "ogApprovedMail"
        Me.ogApprovedMail.Size = New System.Drawing.Size(1679, 797)
        Me.ogApprovedMail.TabIndex = 9
        Me.ogApprovedMail.Text = "Approved  Purchase"
        '
        'ochkselectall
        '
        Me.ochkselectall.Location = New System.Drawing.Point(28, 1)
        Me.ochkselectall.Margin = New System.Windows.Forms.Padding(4)
        Me.ochkselectall.Name = "ochkselectall"
        Me.ochkselectall.Properties.Caption = "Select All"
        Me.ochkselectall.Size = New System.Drawing.Size(230, 20)
        Me.ochkselectall.TabIndex = 1
        '
        'ogDirectorApproved
        '
        Me.ogDirectorApproved.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogDirectorApproved.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4)
        Me.ogDirectorApproved.Location = New System.Drawing.Point(0, 32)
        Me.ogDirectorApproved.MainView = Me.ogvDirectorApproved
        Me.ogDirectorApproved.Margin = New System.Windows.Forms.Padding(4)
        Me.ogDirectorApproved.Name = "ogDirectorApproved"
        Me.ogDirectorApproved.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCheckEdit1})
        Me.ogDirectorApproved.Size = New System.Drawing.Size(1674, 757)
        Me.ogDirectorApproved.TabIndex = 0
        Me.ogDirectorApproved.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvDirectorApproved})
        '
        'ogvDirectorApproved
        '
        Me.ogvDirectorApproved.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.ColFTStateApproved, Me.ColFTPurchaseBy, Me.ColFTSuperVisorName, Me.ColFTPurchaseNo, Me.ColFDPurchaseDate, Me.ColFTPurchaseState, Me.ColFTCmpRunCode, Me.ColFTCmpRunName, Me.ColFTSuplCode, Me.ColFTSuplName, Me.ColFDDeliveryDate, Me.ColFTCrTermCode, Me.ColFTCrTermDesc, Me.ColFNCreditDay, Me.ColFTTermOfPMCode, Me.ColFTTermOfPMName, Me.ColFTDeliveryCode, Me.ColFTDeliveryDesc, Me.FTCurCode, Me.ColFNExchangeRate, Me.ColFTContactPerson, Me.ColFNDisCountAmt, Me.ColFNSurcharge, Me.ColFNVatAmt, Me.ColFNPOGrandAmt, Me.FTQuantityinfo, Me.ColFTRemark, Me.ColFNDisCountPer, Me.ColFNPONetAmt, Me.ColFNVatPer, Me.ColFTTeamGrpCode, Me.ColFTTeamGrpName, Me.ColFTPurGrpCode, Me.ColFTPurGrpName, Me.FTPoTypeState, Me.FTStateFree})
        Me.ogvDirectorApproved.GridControl = Me.ogDirectorApproved
        Me.ogvDirectorApproved.Name = "ogvDirectorApproved"
        Me.ogvDirectorApproved.OptionsView.ColumnAutoWidth = False
        Me.ogvDirectorApproved.OptionsView.ShowGroupPanel = False
        '
        'ColFTStateApproved
        '
        Me.ColFTStateApproved.Caption = "FTStateApproved"
        Me.ColFTStateApproved.ColumnEdit = Me.RepositoryItemCheckEdit1
        Me.ColFTStateApproved.FieldName = "FTStateApproved"
        Me.ColFTStateApproved.Name = "ColFTStateApproved"
        Me.ColFTStateApproved.Visible = True
        Me.ColFTStateApproved.VisibleIndex = 0
        Me.ColFTStateApproved.Width = 50
        '
        'RepositoryItemCheckEdit1
        '
        Me.RepositoryItemCheckEdit1.AutoHeight = False
        Me.RepositoryItemCheckEdit1.Caption = "Check"
        Me.RepositoryItemCheckEdit1.Name = "RepositoryItemCheckEdit1"
        Me.RepositoryItemCheckEdit1.ValueChecked = "1"
        Me.RepositoryItemCheckEdit1.ValueUnchecked = "0"
        '
        'ColFTPurchaseBy
        '
        Me.ColFTPurchaseBy.Caption = "FTPurchaseBy"
        Me.ColFTPurchaseBy.FieldName = "FTPurchaseBy"
        Me.ColFTPurchaseBy.Name = "ColFTPurchaseBy"
        Me.ColFTPurchaseBy.OptionsColumn.AllowEdit = False
        Me.ColFTPurchaseBy.OptionsColumn.ReadOnly = True
        Me.ColFTPurchaseBy.Visible = True
        Me.ColFTPurchaseBy.VisibleIndex = 1
        '
        'ColFTSuperVisorName
        '
        Me.ColFTSuperVisorName.Caption = "FTSuperVisorName"
        Me.ColFTSuperVisorName.FieldName = "FTSuperVisorName"
        Me.ColFTSuperVisorName.Name = "ColFTSuperVisorName"
        Me.ColFTSuperVisorName.OptionsColumn.AllowEdit = False
        Me.ColFTSuperVisorName.OptionsColumn.ReadOnly = True
        Me.ColFTSuperVisorName.Visible = True
        Me.ColFTSuperVisorName.VisibleIndex = 2
        '
        'ColFTPurchaseNo
        '
        Me.ColFTPurchaseNo.Caption = "FTPurchaseNo"
        Me.ColFTPurchaseNo.FieldName = "FTPurchaseNo"
        Me.ColFTPurchaseNo.Name = "ColFTPurchaseNo"
        Me.ColFTPurchaseNo.OptionsColumn.AllowEdit = False
        Me.ColFTPurchaseNo.OptionsColumn.ReadOnly = True
        Me.ColFTPurchaseNo.Visible = True
        Me.ColFTPurchaseNo.VisibleIndex = 3
        Me.ColFTPurchaseNo.Width = 100
        '
        'ColFDPurchaseDate
        '
        Me.ColFDPurchaseDate.Caption = "FDPurchaseDate"
        Me.ColFDPurchaseDate.FieldName = "FDPurchaseDate"
        Me.ColFDPurchaseDate.Name = "ColFDPurchaseDate"
        Me.ColFDPurchaseDate.OptionsColumn.AllowEdit = False
        Me.ColFDPurchaseDate.OptionsColumn.ReadOnly = True
        Me.ColFDPurchaseDate.Visible = True
        Me.ColFDPurchaseDate.VisibleIndex = 4
        Me.ColFDPurchaseDate.Width = 70
        '
        'ColFTPurchaseState
        '
        Me.ColFTPurchaseState.Caption = "FTPurchaseState"
        Me.ColFTPurchaseState.FieldName = "FTPurchaseState"
        Me.ColFTPurchaseState.Name = "ColFTPurchaseState"
        Me.ColFTPurchaseState.OptionsColumn.AllowEdit = False
        Me.ColFTPurchaseState.OptionsColumn.ReadOnly = True
        Me.ColFTPurchaseState.Width = 200
        '
        'ColFTCmpRunCode
        '
        Me.ColFTCmpRunCode.Caption = "FTCmpRunCode"
        Me.ColFTCmpRunCode.FieldName = "FTCmpRunCode"
        Me.ColFTCmpRunCode.Name = "ColFTCmpRunCode"
        Me.ColFTCmpRunCode.OptionsColumn.AllowEdit = False
        Me.ColFTCmpRunCode.OptionsColumn.ReadOnly = True
        Me.ColFTCmpRunCode.Width = 50
        '
        'ColFTCmpRunName
        '
        Me.ColFTCmpRunName.Caption = "FTCmpRunName"
        Me.ColFTCmpRunName.FieldName = "FTCmpRunName"
        Me.ColFTCmpRunName.Name = "ColFTCmpRunName"
        Me.ColFTCmpRunName.OptionsColumn.AllowEdit = False
        Me.ColFTCmpRunName.OptionsColumn.ReadOnly = True
        Me.ColFTCmpRunName.Width = 200
        '
        'ColFTSuplCode
        '
        Me.ColFTSuplCode.Caption = "FTSuplCode"
        Me.ColFTSuplCode.FieldName = "FTSuplCode"
        Me.ColFTSuplCode.Name = "ColFTSuplCode"
        Me.ColFTSuplCode.OptionsColumn.AllowEdit = False
        Me.ColFTSuplCode.OptionsColumn.ReadOnly = True
        Me.ColFTSuplCode.Visible = True
        Me.ColFTSuplCode.VisibleIndex = 5
        '
        'ColFTSuplName
        '
        Me.ColFTSuplName.Caption = "FTSuplName"
        Me.ColFTSuplName.FieldName = "FTSuplName"
        Me.ColFTSuplName.Name = "ColFTSuplName"
        Me.ColFTSuplName.OptionsColumn.AllowEdit = False
        Me.ColFTSuplName.OptionsColumn.ReadOnly = True
        Me.ColFTSuplName.Visible = True
        Me.ColFTSuplName.VisibleIndex = 6
        Me.ColFTSuplName.Width = 150
        '
        'ColFDDeliveryDate
        '
        Me.ColFDDeliveryDate.Caption = "FDDeliveryDate"
        Me.ColFDDeliveryDate.FieldName = "FDDeliveryDate"
        Me.ColFDDeliveryDate.Name = "ColFDDeliveryDate"
        Me.ColFDDeliveryDate.OptionsColumn.AllowEdit = False
        Me.ColFDDeliveryDate.OptionsColumn.ReadOnly = True
        Me.ColFDDeliveryDate.Width = 100
        '
        'ColFTCrTermCode
        '
        Me.ColFTCrTermCode.Caption = "FTCrTermCode"
        Me.ColFTCrTermCode.FieldName = "FTCrTermCode"
        Me.ColFTCrTermCode.Name = "ColFTCrTermCode"
        Me.ColFTCrTermCode.OptionsColumn.AllowEdit = False
        Me.ColFTCrTermCode.OptionsColumn.ReadOnly = True
        Me.ColFTCrTermCode.Width = 100
        '
        'ColFTCrTermDesc
        '
        Me.ColFTCrTermDesc.Caption = "FTCrTermDesc"
        Me.ColFTCrTermDesc.FieldName = "FTCrTermDesc"
        Me.ColFTCrTermDesc.Name = "ColFTCrTermDesc"
        Me.ColFTCrTermDesc.OptionsColumn.AllowEdit = False
        Me.ColFTCrTermDesc.OptionsColumn.ReadOnly = True
        Me.ColFTCrTermDesc.Width = 100
        '
        'ColFNCreditDay
        '
        Me.ColFNCreditDay.Caption = "FNCreditDay"
        Me.ColFNCreditDay.FieldName = "FNCreditDay"
        Me.ColFNCreditDay.Name = "ColFNCreditDay"
        Me.ColFNCreditDay.OptionsColumn.AllowEdit = False
        Me.ColFNCreditDay.OptionsColumn.ReadOnly = True
        Me.ColFNCreditDay.Width = 100
        '
        'ColFTTermOfPMCode
        '
        Me.ColFTTermOfPMCode.Caption = "FTTermOfPMCode"
        Me.ColFTTermOfPMCode.FieldName = "FTTermOfPMCode"
        Me.ColFTTermOfPMCode.Name = "ColFTTermOfPMCode"
        Me.ColFTTermOfPMCode.OptionsColumn.AllowEdit = False
        Me.ColFTTermOfPMCode.OptionsColumn.ReadOnly = True
        Me.ColFTTermOfPMCode.Width = 100
        '
        'ColFTTermOfPMName
        '
        Me.ColFTTermOfPMName.Caption = "FTTermOfPMName"
        Me.ColFTTermOfPMName.FieldName = "FTTermOfPMName"
        Me.ColFTTermOfPMName.Name = "ColFTTermOfPMName"
        Me.ColFTTermOfPMName.OptionsColumn.AllowEdit = False
        Me.ColFTTermOfPMName.OptionsColumn.ReadOnly = True
        Me.ColFTTermOfPMName.Width = 100
        '
        'ColFTDeliveryCode
        '
        Me.ColFTDeliveryCode.Caption = "FTDeliveryCode"
        Me.ColFTDeliveryCode.FieldName = "FTDeliveryCode"
        Me.ColFTDeliveryCode.Name = "ColFTDeliveryCode"
        Me.ColFTDeliveryCode.OptionsColumn.AllowEdit = False
        Me.ColFTDeliveryCode.OptionsColumn.ReadOnly = True
        Me.ColFTDeliveryCode.Visible = True
        Me.ColFTDeliveryCode.VisibleIndex = 7
        '
        'ColFTDeliveryDesc
        '
        Me.ColFTDeliveryDesc.Caption = "FTDeliveryDesc"
        Me.ColFTDeliveryDesc.FieldName = "FTDeliveryDesc"
        Me.ColFTDeliveryDesc.Name = "ColFTDeliveryDesc"
        Me.ColFTDeliveryDesc.OptionsColumn.AllowEdit = False
        Me.ColFTDeliveryDesc.OptionsColumn.ReadOnly = True
        Me.ColFTDeliveryDesc.Visible = True
        Me.ColFTDeliveryDesc.VisibleIndex = 8
        Me.ColFTDeliveryDesc.Width = 100
        '
        'FTCurCode
        '
        Me.FTCurCode.Caption = "สกุลเงิน"
        Me.FTCurCode.FieldName = "FTCurCode"
        Me.FTCurCode.Name = "FTCurCode"
        Me.FTCurCode.OptionsColumn.AllowEdit = False
        Me.FTCurCode.OptionsColumn.ReadOnly = True
        Me.FTCurCode.Visible = True
        Me.FTCurCode.VisibleIndex = 9
        Me.FTCurCode.Width = 80
        '
        'ColFNExchangeRate
        '
        Me.ColFNExchangeRate.Caption = "FNExchangeRate"
        Me.ColFNExchangeRate.DisplayFormat.FormatString = "{0:n4}"
        Me.ColFNExchangeRate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.ColFNExchangeRate.FieldName = "FNExchangeRate"
        Me.ColFNExchangeRate.Name = "ColFNExchangeRate"
        Me.ColFNExchangeRate.OptionsColumn.AllowEdit = False
        Me.ColFNExchangeRate.OptionsColumn.ReadOnly = True
        Me.ColFNExchangeRate.Visible = True
        Me.ColFNExchangeRate.VisibleIndex = 10
        Me.ColFNExchangeRate.Width = 85
        '
        'ColFTContactPerson
        '
        Me.ColFTContactPerson.Caption = "FTContactPerson"
        Me.ColFTContactPerson.FieldName = "FTContactPerson"
        Me.ColFTContactPerson.Name = "ColFTContactPerson"
        Me.ColFTContactPerson.OptionsColumn.AllowEdit = False
        Me.ColFTContactPerson.OptionsColumn.ReadOnly = True
        Me.ColFTContactPerson.Width = 100
        '
        'ColFNDisCountAmt
        '
        Me.ColFNDisCountAmt.Caption = "FNDisCountAmt"
        Me.ColFNDisCountAmt.DisplayFormat.FormatString = "{0:n2}"
        Me.ColFNDisCountAmt.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.ColFNDisCountAmt.FieldName = "FNDisCountAmt"
        Me.ColFNDisCountAmt.Name = "ColFNDisCountAmt"
        Me.ColFNDisCountAmt.OptionsColumn.AllowEdit = False
        Me.ColFNDisCountAmt.OptionsColumn.ReadOnly = True
        Me.ColFNDisCountAmt.Visible = True
        Me.ColFNDisCountAmt.VisibleIndex = 11
        '
        'ColFNSurcharge
        '
        Me.ColFNSurcharge.Caption = "FNSurcharge"
        Me.ColFNSurcharge.DisplayFormat.FormatString = "{0:n2}"
        Me.ColFNSurcharge.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.ColFNSurcharge.FieldName = "FNSurcharge"
        Me.ColFNSurcharge.Name = "ColFNSurcharge"
        Me.ColFNSurcharge.OptionsColumn.AllowEdit = False
        Me.ColFNSurcharge.OptionsColumn.ReadOnly = True
        Me.ColFNSurcharge.Visible = True
        Me.ColFNSurcharge.VisibleIndex = 12
        '
        'ColFNVatAmt
        '
        Me.ColFNVatAmt.Caption = "FNVatAmt"
        Me.ColFNVatAmt.DisplayFormat.FormatString = "{0:n2}"
        Me.ColFNVatAmt.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.ColFNVatAmt.FieldName = "FNVatAmt"
        Me.ColFNVatAmt.Name = "ColFNVatAmt"
        Me.ColFNVatAmt.OptionsColumn.AllowEdit = False
        Me.ColFNVatAmt.OptionsColumn.ReadOnly = True
        Me.ColFNVatAmt.Visible = True
        Me.ColFNVatAmt.VisibleIndex = 13
        Me.ColFNVatAmt.Width = 110
        '
        'ColFNPOGrandAmt
        '
        Me.ColFNPOGrandAmt.Caption = "FNPOGrandAmt"
        Me.ColFNPOGrandAmt.DisplayFormat.FormatString = "{0:n2}"
        Me.ColFNPOGrandAmt.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.ColFNPOGrandAmt.FieldName = "FNPOGrandAmt"
        Me.ColFNPOGrandAmt.Name = "ColFNPOGrandAmt"
        Me.ColFNPOGrandAmt.OptionsColumn.AllowEdit = False
        Me.ColFNPOGrandAmt.OptionsColumn.ReadOnly = True
        Me.ColFNPOGrandAmt.Visible = True
        Me.ColFNPOGrandAmt.VisibleIndex = 14
        Me.ColFNPOGrandAmt.Width = 110
        '
        'FTQuantityinfo
        '
        Me.FTQuantityinfo.AppearanceCell.Options.UseTextOptions = True
        Me.FTQuantityinfo.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTQuantityinfo.Caption = "Quantity Info"
        Me.FTQuantityinfo.FieldName = "FTQuantityinfo"
        Me.FTQuantityinfo.Name = "FTQuantityinfo"
        Me.FTQuantityinfo.OptionsColumn.AllowEdit = False
        Me.FTQuantityinfo.OptionsColumn.ReadOnly = True
        Me.FTQuantityinfo.Visible = True
        Me.FTQuantityinfo.VisibleIndex = 15
        Me.FTQuantityinfo.Width = 120
        '
        'ColFTRemark
        '
        Me.ColFTRemark.Caption = "FTRemark"
        Me.ColFTRemark.FieldName = "FTRemark"
        Me.ColFTRemark.Name = "ColFTRemark"
        Me.ColFTRemark.OptionsColumn.AllowEdit = False
        Me.ColFTRemark.OptionsColumn.ReadOnly = True
        Me.ColFTRemark.Visible = True
        Me.ColFTRemark.VisibleIndex = 16
        Me.ColFTRemark.Width = 100
        '
        'ColFNDisCountPer
        '
        Me.ColFNDisCountPer.Caption = "FNDisCountPer"
        Me.ColFNDisCountPer.DisplayFormat.FormatString = "{0:n2}"
        Me.ColFNDisCountPer.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.ColFNDisCountPer.FieldName = "FNDisCountPer"
        Me.ColFNDisCountPer.Name = "ColFNDisCountPer"
        Me.ColFNDisCountPer.OptionsColumn.AllowEdit = False
        Me.ColFNDisCountPer.OptionsColumn.ReadOnly = True
        Me.ColFNDisCountPer.Width = 100
        '
        'ColFNPONetAmt
        '
        Me.ColFNPONetAmt.Caption = "FNPONetAmt"
        Me.ColFNPONetAmt.DisplayFormat.FormatString = "{0:n4}"
        Me.ColFNPONetAmt.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.ColFNPONetAmt.FieldName = "FNPONetAmt"
        Me.ColFNPONetAmt.Name = "ColFNPONetAmt"
        Me.ColFNPONetAmt.OptionsColumn.AllowEdit = False
        Me.ColFNPONetAmt.OptionsColumn.ReadOnly = True
        Me.ColFNPONetAmt.Width = 120
        '
        'ColFNVatPer
        '
        Me.ColFNVatPer.Caption = "FNVatPer"
        Me.ColFNVatPer.DisplayFormat.FormatString = "{0:n2}"
        Me.ColFNVatPer.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.ColFNVatPer.FieldName = "FNVatPer"
        Me.ColFNVatPer.Name = "ColFNVatPer"
        Me.ColFNVatPer.OptionsColumn.AllowEdit = False
        Me.ColFNVatPer.OptionsColumn.ReadOnly = True
        Me.ColFNVatPer.Width = 100
        '
        'ColFTTeamGrpCode
        '
        Me.ColFTTeamGrpCode.Caption = "FTTeamGrpCode"
        Me.ColFTTeamGrpCode.FieldName = "FTTeamGrpCode"
        Me.ColFTTeamGrpCode.Name = "ColFTTeamGrpCode"
        Me.ColFTTeamGrpCode.OptionsColumn.AllowEdit = False
        Me.ColFTTeamGrpCode.OptionsColumn.ReadOnly = True
        Me.ColFTTeamGrpCode.Width = 100
        '
        'ColFTTeamGrpName
        '
        Me.ColFTTeamGrpName.Caption = "FTTeamGrpName"
        Me.ColFTTeamGrpName.FieldName = "FTTeamGrpName"
        Me.ColFTTeamGrpName.Name = "ColFTTeamGrpName"
        Me.ColFTTeamGrpName.OptionsColumn.AllowEdit = False
        Me.ColFTTeamGrpName.OptionsColumn.ReadOnly = True
        Me.ColFTTeamGrpName.Width = 100
        '
        'ColFTPurGrpCode
        '
        Me.ColFTPurGrpCode.Caption = "FTPurGrpCode"
        Me.ColFTPurGrpCode.FieldName = "FTPurGrpCode"
        Me.ColFTPurGrpCode.Name = "ColFTPurGrpCode"
        Me.ColFTPurGrpCode.OptionsColumn.AllowEdit = False
        Me.ColFTPurGrpCode.OptionsColumn.ReadOnly = True
        Me.ColFTPurGrpCode.Width = 100
        '
        'ColFTPurGrpName
        '
        Me.ColFTPurGrpName.Caption = "FTPurGrpName"
        Me.ColFTPurGrpName.FieldName = "FTPurGrpName"
        Me.ColFTPurGrpName.Name = "ColFTPurGrpName"
        Me.ColFTPurGrpName.OptionsColumn.AllowEdit = False
        Me.ColFTPurGrpName.OptionsColumn.ReadOnly = True
        Me.ColFTPurGrpName.Width = 100
        '
        'FTPoTypeState
        '
        Me.FTPoTypeState.Caption = "FTPoTypeState"
        Me.FTPoTypeState.FieldName = "FTPoTypeState"
        Me.FTPoTypeState.Name = "FTPoTypeState"
        '
        'FTStateFree
        '
        Me.FTStateFree.Caption = "FTStateFree"
        Me.FTStateFree.FieldName = "FTStateFree"
        Me.FTStateFree.Name = "FTStateFree"
        '
        'otmchkpo
        '
        Me.otmchkpo.Interval = 60000
        '
        'otbmain
        '
        Me.otbmain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.otbmain.Location = New System.Drawing.Point(0, 54)
        Me.otbmain.Name = "otbmain"
        Me.otbmain.SelectedTabPage = Me.otpordercost
        Me.otbmain.Size = New System.Drawing.Size(1687, 827)
        Me.otbmain.TabIndex = 14
        Me.otbmain.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.otpapppo, Me.otpappfactory, Me.otpappdocument, Me.otpordercost, Me.otpleaveapprove})
        '
        'otpordercost
        '
        Me.otpordercost.Controls.Add(Me.ogrpApprovedordercost)
        Me.otpordercost.Margin = New System.Windows.Forms.Padding(4)
        Me.otpordercost.Name = "otpordercost"
        Me.otpordercost.PageVisible = False
        Me.otpordercost.Size = New System.Drawing.Size(1679, 797)
        Me.otpordercost.Text = "Approved Order Costing"
        '
        'ogrpApprovedordercost
        '
        Me.ogrpApprovedordercost.AppearanceCaption.Options.UseTextOptions = True
        Me.ogrpApprovedordercost.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ogrpApprovedordercost.Controls.Add(Me.ogcordercost)
        Me.ogrpApprovedordercost.Controls.Add(Me.FTSelectAllOrderCost)
        Me.ogrpApprovedordercost.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogrpApprovedordercost.Location = New System.Drawing.Point(0, 0)
        Me.ogrpApprovedordercost.Margin = New System.Windows.Forms.Padding(4)
        Me.ogrpApprovedordercost.Name = "ogrpApprovedordercost"
        Me.ogrpApprovedordercost.Size = New System.Drawing.Size(1679, 797)
        Me.ogrpApprovedordercost.TabIndex = 13
        Me.ogrpApprovedordercost.Text = "Approved  Order Costing"
        '
        'ogcordercost
        '
        Me.ogcordercost.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogcordercost.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcordercost.Location = New System.Drawing.Point(2, 22)
        Me.ogcordercost.MainView = Me.ogvordercost
        Me.ogcordercost.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcordercost.Name = "ogcordercost"
        Me.ogcordercost.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepFNIncomeAfterRawmaterial, Me.RepFNProdCost, Me.RepFNWageCost, Me.RepositoryItemCheckEdit1FTSelect})
        Me.ogcordercost.Size = New System.Drawing.Size(1675, 773)
        Me.ogcordercost.TabIndex = 396
        Me.ogcordercost.TabStop = False
        Me.ogcordercost.Tag = "2|"
        Me.ogcordercost.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvordercost})
        '
        'ogvordercost
        '
        Me.ogvordercost.Bands.AddRange(New DevExpress.XtraGrid.Views.BandedGrid.GridBand() {Me.gridBand3, Me.gCmpName, Me.gFTInvoice, Me.gInvoiceDate, Me.gFTOrderNo, Me.gFTPORef, Me.gFTStyle, Me.gFNExportQuantity, Me.gFNExportAmt, Me.gFNExportAmtus, Me.gCenter, Me.gBtanch, Me.gProfit, Me.gFNOrderQuantity, Me.gFNExport, Me.gdiff, Me.gridBand4, Me.gdiffper})
        Me.ogvordercost.Columns.AddRange(New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn() {Me.BandedGridColumn2, Me.FTInvoiceNo, Me.FTOrderNo, Me.FTPORef, Me.FTStyleCode, Me.FNExportQuantity, Me.FNExportAmtTHB, Me.FNExportAmt, Me.FNFabricCostPer, Me.FNWagePullPer, Me.FNWagePull, Me.FNNetProfitRcv, Me.FNFabricCost, Me.FNAccessroryCostPer, Me.BandedGridColumn1, Me.FNAccFabStockCostPer, Me.FNAccFabStockCost, Me.FNConductedCostPer, Me.FNConductedCost, Me.FNOtherCostPer, Me.FNOtherCost, Me.FNEmbFacCostPer, Me.FNEmbFacCost, Me.FNEmpPrintBranchPer, Me.FNEmpPrintBranch, Me.FNExportAmtBFPer, Me.FNExportAmtBF, Me.FNWageCostPer, Me.FNWageCost, Me.FNEmpPrintSubPer, Me.FNEmpPrintSub, Me.FNImportExportCostPer, Me.FNImportExportCost, Me.FNProdCostPer, Me.FNProdCost, Me.FNCommissionCostPer, Me.FNCommissionCost, Me.FNTransportAirCostPer, Me.FNTransportAirCost, Me.FNNetProfitPer, Me.FNNetProfit, Me.FNOrderQuantity, Me.FNExportQuantityTo, Me.FNExportQuantityOtherMonth, Me.FNTotalExport, Me.FNOrderQuantityBal, Me.FNLostPer, Me.FNFabricAccMinCost, Me.FNFabricAccStockOtherCost, Me.FNOtherServiceCost, Me.FNSewCost, Me.FNEmbroideryCost, Me.FNPrintCost, Me.FNImportCost, Me.FNEtcCost, Me.FNIncomeAfterRawmaterial, Me.FNFabricAccMinCostPer, Me.FNFabricAccStockOtherCostPer, Me.FNOtherServiceCostPer, Me.FNSewCostPer, Me.FNEmbroideryCostPer, Me.FNPrintCostPer, Me.FNImportCostPer, Me.FNEtcCostPer, Me.FNIncomeAfterRawmaterialPer, Me.CFNStateRow, Me.FNPrice, Me.FDInvoiceDate, Me.FTCmpName})
        Me.ogvordercost.GridControl = Me.ogcordercost
        Me.ogvordercost.Name = "ogvordercost"
        Me.ogvordercost.OptionsCustomization.AllowQuickHideColumns = False
        Me.ogvordercost.OptionsPrint.PrintHeader = False
        Me.ogvordercost.OptionsSelection.MultiSelect = True
        Me.ogvordercost.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect
        Me.ogvordercost.OptionsView.ColumnAutoWidth = False
        Me.ogvordercost.OptionsView.ShowColumnHeaders = False
        Me.ogvordercost.OptionsView.ShowGroupPanel = False
        '
        'gridBand3
        '
        Me.gridBand3.Columns.Add(Me.BandedGridColumn2)
        Me.gridBand3.Name = "gridBand3"
        Me.gridBand3.VisibleIndex = 0
        Me.gridBand3.Width = 50
        '
        'BandedGridColumn2
        '
        Me.BandedGridColumn2.Caption = "BandedGridColumn2"
        Me.BandedGridColumn2.ColumnEdit = Me.RepositoryItemCheckEdit1FTSelect
        Me.BandedGridColumn2.FieldName = "FTSelect"
        Me.BandedGridColumn2.Name = "BandedGridColumn2"
        Me.BandedGridColumn2.Visible = True
        Me.BandedGridColumn2.Width = 50
        '
        'RepositoryItemCheckEdit1FTSelect
        '
        Me.RepositoryItemCheckEdit1FTSelect.AutoHeight = False
        Me.RepositoryItemCheckEdit1FTSelect.Name = "RepositoryItemCheckEdit1FTSelect"
        Me.RepositoryItemCheckEdit1FTSelect.ValueChecked = "1"
        Me.RepositoryItemCheckEdit1FTSelect.ValueUnchecked = "0"
        '
        'gCmpName
        '
        Me.gCmpName.AppearanceHeader.Options.UseTextOptions = True
        Me.gCmpName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gCmpName.Caption = "Company"
        Me.gCmpName.Columns.Add(Me.FTCmpName)
        Me.gCmpName.Name = "gCmpName"
        Me.gCmpName.VisibleIndex = 1
        Me.gCmpName.Width = 140
        '
        'FTCmpName
        '
        Me.FTCmpName.Caption = "FTCmpName"
        Me.FTCmpName.FieldName = "FTCmpName"
        Me.FTCmpName.Name = "FTCmpName"
        Me.FTCmpName.OptionsColumn.AllowEdit = False
        Me.FTCmpName.Visible = True
        Me.FTCmpName.Width = 140
        '
        'gFTInvoice
        '
        Me.gFTInvoice.AppearanceHeader.Options.UseTextOptions = True
        Me.gFTInvoice.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gFTInvoice.Caption = "INVOICE"
        Me.gFTInvoice.Columns.Add(Me.FTInvoiceNo)
        Me.gFTInvoice.Name = "gFTInvoice"
        Me.gFTInvoice.RowCount = 2
        Me.gFTInvoice.Visible = False
        Me.gFTInvoice.VisibleIndex = -1
        Me.gFTInvoice.Width = 120
        '
        'FTInvoiceNo
        '
        Me.FTInvoiceNo.Caption = "FTInvoiceNo"
        Me.FTInvoiceNo.FieldName = "FTInvoiceNo"
        Me.FTInvoiceNo.Name = "FTInvoiceNo"
        Me.FTInvoiceNo.OptionsColumn.AllowEdit = False
        Me.FTInvoiceNo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTInvoiceNo.OptionsColumn.ReadOnly = True
        Me.FTInvoiceNo.Visible = True
        Me.FTInvoiceNo.Width = 120
        '
        'gInvoiceDate
        '
        Me.gInvoiceDate.AppearanceHeader.Options.UseTextOptions = True
        Me.gInvoiceDate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gInvoiceDate.Caption = "InvoiceDate"
        Me.gInvoiceDate.Columns.Add(Me.FDInvoiceDate)
        Me.gInvoiceDate.Name = "gInvoiceDate"
        Me.gInvoiceDate.VisibleIndex = 2
        Me.gInvoiceDate.Width = 131
        '
        'FDInvoiceDate
        '
        Me.FDInvoiceDate.Caption = "FDInvoiceDate"
        Me.FDInvoiceDate.FieldName = "FDInvoiceDate"
        Me.FDInvoiceDate.Name = "FDInvoiceDate"
        Me.FDInvoiceDate.OptionsColumn.AllowEdit = False
        Me.FDInvoiceDate.Visible = True
        Me.FDInvoiceDate.Width = 131
        '
        'gFTOrderNo
        '
        Me.gFTOrderNo.AppearanceHeader.Options.UseTextOptions = True
        Me.gFTOrderNo.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gFTOrderNo.Caption = "FO. No."
        Me.gFTOrderNo.Columns.Add(Me.FTOrderNo)
        Me.gFTOrderNo.Name = "gFTOrderNo"
        Me.gFTOrderNo.RowCount = 2
        Me.gFTOrderNo.Visible = False
        Me.gFTOrderNo.VisibleIndex = -1
        Me.gFTOrderNo.Width = 120
        '
        'FTOrderNo
        '
        Me.FTOrderNo.Caption = "FTOrderNo"
        Me.FTOrderNo.FieldName = "FTOrderNo"
        Me.FTOrderNo.Name = "FTOrderNo"
        Me.FTOrderNo.OptionsColumn.AllowEdit = False
        Me.FTOrderNo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTOrderNo.OptionsColumn.ReadOnly = True
        Me.FTOrderNo.Visible = True
        Me.FTOrderNo.Width = 120
        '
        'gFTPORef
        '
        Me.gFTPORef.AppearanceHeader.Options.UseTextOptions = True
        Me.gFTPORef.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gFTPORef.Caption = "PO"
        Me.gFTPORef.Columns.Add(Me.FTPORef)
        Me.gFTPORef.Name = "gFTPORef"
        Me.gFTPORef.RowCount = 2
        Me.gFTPORef.Visible = False
        Me.gFTPORef.VisibleIndex = -1
        Me.gFTPORef.Width = 120
        '
        'FTPORef
        '
        Me.FTPORef.Caption = "FTPORef"
        Me.FTPORef.FieldName = "FTPORef"
        Me.FTPORef.Name = "FTPORef"
        Me.FTPORef.OptionsColumn.AllowEdit = False
        Me.FTPORef.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTPORef.OptionsColumn.ReadOnly = True
        Me.FTPORef.Visible = True
        Me.FTPORef.Width = 120
        '
        'gFTStyle
        '
        Me.gFTStyle.AppearanceHeader.Options.UseTextOptions = True
        Me.gFTStyle.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gFTStyle.Caption = "Style"
        Me.gFTStyle.Columns.Add(Me.FTStyleCode)
        Me.gFTStyle.Name = "gFTStyle"
        Me.gFTStyle.RowCount = 2
        Me.gFTStyle.Visible = False
        Me.gFTStyle.VisibleIndex = -1
        Me.gFTStyle.Width = 120
        '
        'FTStyleCode
        '
        Me.FTStyleCode.Caption = "FTStyleCode"
        Me.FTStyleCode.FieldName = "FTStyleCode"
        Me.FTStyleCode.Name = "FTStyleCode"
        Me.FTStyleCode.OptionsColumn.AllowEdit = False
        Me.FTStyleCode.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTStyleCode.OptionsColumn.ReadOnly = True
        Me.FTStyleCode.Visible = True
        Me.FTStyleCode.Width = 120
        '
        'gFNExportQuantity
        '
        Me.gFNExportQuantity.AppearanceHeader.Options.UseTextOptions = True
        Me.gFNExportQuantity.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gFNExportQuantity.Caption = "ส่งออก(ตัว)"
        Me.gFNExportQuantity.Columns.Add(Me.FNExportQuantity)
        Me.gFNExportQuantity.Name = "gFNExportQuantity"
        Me.gFNExportQuantity.RowCount = 2
        Me.gFNExportQuantity.VisibleIndex = 3
        Me.gFNExportQuantity.Width = 140
        '
        'FNExportQuantity
        '
        Me.FNExportQuantity.AppearanceCell.Options.UseTextOptions = True
        Me.FNExportQuantity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNExportQuantity.Caption = "FNExportQuantity"
        Me.FNExportQuantity.DisplayFormat.FormatString = "{0:n0}"
        Me.FNExportQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNExportQuantity.FieldName = "FNExportQuantity"
        Me.FNExportQuantity.Name = "FNExportQuantity"
        Me.FNExportQuantity.OptionsColumn.AllowEdit = False
        Me.FNExportQuantity.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNExportQuantity.OptionsColumn.ReadOnly = True
        Me.FNExportQuantity.Visible = True
        Me.FNExportQuantity.Width = 140
        '
        'gFNExportAmt
        '
        Me.gFNExportAmt.AppearanceHeader.Options.UseTextOptions = True
        Me.gFNExportAmt.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gFNExportAmt.Caption = "มูลค่าส่งออก"
        Me.gFNExportAmt.Columns.Add(Me.FNExportAmtTHB)
        Me.gFNExportAmt.Name = "gFNExportAmt"
        Me.gFNExportAmt.RowCount = 2
        Me.gFNExportAmt.VisibleIndex = 4
        Me.gFNExportAmt.Width = 146
        '
        'FNExportAmtTHB
        '
        Me.FNExportAmtTHB.AppearanceCell.Options.UseTextOptions = True
        Me.FNExportAmtTHB.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNExportAmtTHB.Caption = "FNExportAmtTHB"
        Me.FNExportAmtTHB.DisplayFormat.FormatString = "{0:n2}"
        Me.FNExportAmtTHB.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNExportAmtTHB.FieldName = "FNExportAmtTHB"
        Me.FNExportAmtTHB.Name = "FNExportAmtTHB"
        Me.FNExportAmtTHB.OptionsColumn.AllowEdit = False
        Me.FNExportAmtTHB.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNExportAmtTHB.OptionsColumn.ReadOnly = True
        Me.FNExportAmtTHB.Visible = True
        Me.FNExportAmtTHB.Width = 146
        '
        'gFNExportAmtus
        '
        Me.gFNExportAmtus.Caption = "มูลค่าส่งออก (us)"
        Me.gFNExportAmtus.Columns.Add(Me.FNExportAmt)
        Me.gFNExportAmtus.Name = "gFNExportAmtus"
        Me.gFNExportAmtus.Visible = False
        Me.gFNExportAmtus.VisibleIndex = -1
        Me.gFNExportAmtus.Width = 86
        '
        'FNExportAmt
        '
        Me.FNExportAmt.Caption = "FNExportAmt"
        Me.FNExportAmt.DisplayFormat.FormatString = "{0:n2}"
        Me.FNExportAmt.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNExportAmt.FieldName = "FNExportAmt"
        Me.FNExportAmt.Name = "FNExportAmt"
        Me.FNExportAmt.OptionsColumn.AllowEdit = False
        Me.FNExportAmt.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNExportAmt.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNExportAmt.OptionsColumn.ReadOnly = True
        Me.FNExportAmt.Visible = True
        Me.FNExportAmt.Width = 86
        '
        'gCenter
        '
        Me.gCenter.AppearanceHeader.Options.UseTextOptions = True
        Me.gCenter.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gCenter.Caption = "ส่วนกลาง"
        Me.gCenter.Children.AddRange(New DevExpress.XtraGrid.Views.BandedGrid.GridBand() {Me.gCenter1, Me.gCenter2, Me.gCenter3, Me.gCenter4, Me.gFNFabricAccMinCostPer, Me.gFabricAccMin, Me.gCenter5, Me.gCenter6, Me.cFNFabricAccStockOtherCostPer, Me.gOtherStock, Me.gFNOtherServiceCostPer, Me.gOtherServiceCost, Me.gFNIncomeAfterRawmaterialPer, Me.gFNIncomeAferRawmaterial, Me.gCenter7, Me.gCenter8, Me.gCenter9, Me.gCenter10, Me.gCenter11, Me.gCenter12, Me.gCenter13, Me.gCenter14, Me.gFNWagePullPer, Me.gFNWagePull})
        Me.gCenter.Name = "gCenter"
        Me.gCenter.VisibleIndex = 5
        Me.gCenter.Width = 335
        '
        'gCenter1
        '
        Me.gCenter1.AppearanceHeader.Options.UseTextOptions = True
        Me.gCenter1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gCenter1.Caption = "%"
        Me.gCenter1.Columns.Add(Me.FNFabricCostPer)
        Me.gCenter1.Name = "gCenter1"
        Me.gCenter1.VisibleIndex = 0
        Me.gCenter1.Width = 50
        '
        'FNFabricCostPer
        '
        Me.FNFabricCostPer.AppearanceCell.Options.UseTextOptions = True
        Me.FNFabricCostPer.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNFabricCostPer.Caption = "FNFabricCostPer"
        Me.FNFabricCostPer.DisplayFormat.FormatString = "{0:n2}"
        Me.FNFabricCostPer.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNFabricCostPer.FieldName = "FNFabricCostPer"
        Me.FNFabricCostPer.Name = "FNFabricCostPer"
        Me.FNFabricCostPer.OptionsColumn.AllowEdit = False
        Me.FNFabricCostPer.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNFabricCostPer.OptionsColumn.ReadOnly = True
        Me.FNFabricCostPer.Visible = True
        Me.FNFabricCostPer.Width = 50
        '
        'gCenter2
        '
        Me.gCenter2.AppearanceHeader.Options.UseTextOptions = True
        Me.gCenter2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gCenter2.Caption = "ผ้า"
        Me.gCenter2.Columns.Add(Me.FNFabricCost)
        Me.gCenter2.Name = "gCenter2"
        Me.gCenter2.VisibleIndex = 1
        Me.gCenter2.Width = 106
        '
        'FNFabricCost
        '
        Me.FNFabricCost.AppearanceCell.Options.UseTextOptions = True
        Me.FNFabricCost.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNFabricCost.Caption = "FNFabricCost"
        Me.FNFabricCost.DisplayFormat.FormatString = "{0:n2}"
        Me.FNFabricCost.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNFabricCost.FieldName = "FNFabricCost"
        Me.FNFabricCost.Name = "FNFabricCost"
        Me.FNFabricCost.OptionsColumn.AllowEdit = False
        Me.FNFabricCost.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNFabricCost.OptionsColumn.ReadOnly = True
        Me.FNFabricCost.Visible = True
        Me.FNFabricCost.Width = 106
        '
        'gCenter3
        '
        Me.gCenter3.AppearanceHeader.Options.UseTextOptions = True
        Me.gCenter3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gCenter3.Caption = "%"
        Me.gCenter3.Columns.Add(Me.FNAccessroryCostPer)
        Me.gCenter3.Name = "gCenter3"
        Me.gCenter3.Visible = False
        Me.gCenter3.VisibleIndex = -1
        Me.gCenter3.Width = 50
        '
        'FNAccessroryCostPer
        '
        Me.FNAccessroryCostPer.AppearanceCell.Options.UseTextOptions = True
        Me.FNAccessroryCostPer.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNAccessroryCostPer.Caption = "FNAccessroryCostPer"
        Me.FNAccessroryCostPer.DisplayFormat.FormatString = "{0:n2}"
        Me.FNAccessroryCostPer.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNAccessroryCostPer.FieldName = "FNAccessroryCostPer"
        Me.FNAccessroryCostPer.Name = "FNAccessroryCostPer"
        Me.FNAccessroryCostPer.OptionsColumn.AllowEdit = False
        Me.FNAccessroryCostPer.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNAccessroryCostPer.OptionsColumn.ReadOnly = True
        Me.FNAccessroryCostPer.Visible = True
        Me.FNAccessroryCostPer.Width = 50
        '
        'gCenter4
        '
        Me.gCenter4.AppearanceHeader.Options.UseTextOptions = True
        Me.gCenter4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gCenter4.Caption = "วัสดุ"
        Me.gCenter4.Columns.Add(Me.BandedGridColumn1)
        Me.gCenter4.Name = "gCenter4"
        Me.gCenter4.Visible = False
        Me.gCenter4.VisibleIndex = -1
        Me.gCenter4.Width = 112
        '
        'BandedGridColumn1
        '
        Me.BandedGridColumn1.AppearanceCell.Options.UseTextOptions = True
        Me.BandedGridColumn1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.BandedGridColumn1.Caption = "FNAccessroryCost"
        Me.BandedGridColumn1.DisplayFormat.FormatString = "{0:n2}"
        Me.BandedGridColumn1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.BandedGridColumn1.FieldName = "FNAccessroryCost"
        Me.BandedGridColumn1.Name = "BandedGridColumn1"
        Me.BandedGridColumn1.OptionsColumn.AllowEdit = False
        Me.BandedGridColumn1.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.BandedGridColumn1.OptionsColumn.ReadOnly = True
        Me.BandedGridColumn1.Visible = True
        Me.BandedGridColumn1.Width = 112
        '
        'gFNFabricAccMinCostPer
        '
        Me.gFNFabricAccMinCostPer.Caption = "%"
        Me.gFNFabricAccMinCostPer.Columns.Add(Me.FNFabricAccMinCostPer)
        Me.gFNFabricAccMinCostPer.Name = "gFNFabricAccMinCostPer"
        Me.gFNFabricAccMinCostPer.VisibleIndex = 2
        Me.gFNFabricAccMinCostPer.Width = 42
        '
        'FNFabricAccMinCostPer
        '
        Me.FNFabricAccMinCostPer.Caption = "FNFabricAccMinCostPer"
        Me.FNFabricAccMinCostPer.DisplayFormat.FormatString = "{0:n2}"
        Me.FNFabricAccMinCostPer.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNFabricAccMinCostPer.FieldName = "FNFabricAccMinCostPer"
        Me.FNFabricAccMinCostPer.Name = "FNFabricAccMinCostPer"
        Me.FNFabricAccMinCostPer.OptionsColumn.AllowEdit = False
        Me.FNFabricAccMinCostPer.Visible = True
        Me.FNFabricAccMinCostPer.Width = 42
        '
        'gFabricAccMin
        '
        Me.gFabricAccMin.AppearanceHeader.Options.UseTextOptions = True
        Me.gFabricAccMin.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gFabricAccMin.Caption = "Fabrice + Acc Min"
        Me.gFabricAccMin.Columns.Add(Me.FNFabricAccMinCost)
        Me.gFabricAccMin.Name = "gFabricAccMin"
        Me.gFabricAccMin.VisibleIndex = 3
        Me.gFabricAccMin.Width = 137
        '
        'FNFabricAccMinCost
        '
        Me.FNFabricAccMinCost.Caption = "FNFabricAccMinCost"
        Me.FNFabricAccMinCost.DisplayFormat.FormatString = "{0:n2}"
        Me.FNFabricAccMinCost.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNFabricAccMinCost.FieldName = "FNFabricAccMinCost"
        Me.FNFabricAccMinCost.Name = "FNFabricAccMinCost"
        Me.FNFabricAccMinCost.OptionsColumn.AllowEdit = False
        Me.FNFabricAccMinCost.Visible = True
        Me.FNFabricAccMinCost.Width = 137
        '
        'gCenter5
        '
        Me.gCenter5.AppearanceHeader.Options.UseTextOptions = True
        Me.gCenter5.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gCenter5.Caption = "%"
        Me.gCenter5.Columns.Add(Me.FNAccFabStockCostPer)
        Me.gCenter5.Name = "gCenter5"
        Me.gCenter5.Visible = False
        Me.gCenter5.VisibleIndex = -1
        Me.gCenter5.Width = 50
        '
        'FNAccFabStockCostPer
        '
        Me.FNAccFabStockCostPer.AppearanceCell.Options.UseTextOptions = True
        Me.FNAccFabStockCostPer.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNAccFabStockCostPer.Caption = "FNAccFabStockCostPer"
        Me.FNAccFabStockCostPer.DisplayFormat.FormatString = "{0:n2}"
        Me.FNAccFabStockCostPer.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNAccFabStockCostPer.FieldName = "FNAccFabStockCostPer"
        Me.FNAccFabStockCostPer.Name = "FNAccFabStockCostPer"
        Me.FNAccFabStockCostPer.OptionsColumn.AllowEdit = False
        Me.FNAccFabStockCostPer.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNAccFabStockCostPer.OptionsColumn.ReadOnly = True
        Me.FNAccFabStockCostPer.Visible = True
        Me.FNAccFabStockCostPer.Width = 50
        '
        'gCenter6
        '
        Me.gCenter6.AppearanceHeader.Options.UseTextOptions = True
        Me.gCenter6.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gCenter6.Caption = "STOCK"
        Me.gCenter6.Columns.Add(Me.FNAccFabStockCost)
        Me.gCenter6.Name = "gCenter6"
        Me.gCenter6.Visible = False
        Me.gCenter6.VisibleIndex = -1
        Me.gCenter6.Width = 232
        '
        'FNAccFabStockCost
        '
        Me.FNAccFabStockCost.AppearanceCell.Options.UseTextOptions = True
        Me.FNAccFabStockCost.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNAccFabStockCost.Caption = "FNAccFabStockCost"
        Me.FNAccFabStockCost.DisplayFormat.FormatString = "{0:n2}"
        Me.FNAccFabStockCost.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNAccFabStockCost.FieldName = "FNAccFabStockCost"
        Me.FNAccFabStockCost.Name = "FNAccFabStockCost"
        Me.FNAccFabStockCost.OptionsColumn.AllowEdit = False
        Me.FNAccFabStockCost.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNAccFabStockCost.OptionsColumn.ReadOnly = True
        Me.FNAccFabStockCost.Width = 232
        '
        'cFNFabricAccStockOtherCostPer
        '
        Me.cFNFabricAccStockOtherCostPer.Caption = "%"
        Me.cFNFabricAccStockOtherCostPer.Columns.Add(Me.FNFabricAccStockOtherCostPer)
        Me.cFNFabricAccStockOtherCostPer.Name = "cFNFabricAccStockOtherCostPer"
        Me.cFNFabricAccStockOtherCostPer.Visible = False
        Me.cFNFabricAccStockOtherCostPer.VisibleIndex = -1
        Me.cFNFabricAccStockOtherCostPer.Width = 41
        '
        'FNFabricAccStockOtherCostPer
        '
        Me.FNFabricAccStockOtherCostPer.Caption = "FNFabricAccStockOtherCostPer"
        Me.FNFabricAccStockOtherCostPer.DisplayFormat.FormatString = "{0:n2}"
        Me.FNFabricAccStockOtherCostPer.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNFabricAccStockOtherCostPer.FieldName = "FNFabricAccStockOtherCostPer"
        Me.FNFabricAccStockOtherCostPer.Name = "FNFabricAccStockOtherCostPer"
        Me.FNFabricAccStockOtherCostPer.OptionsColumn.AllowEdit = False
        Me.FNFabricAccStockOtherCostPer.Visible = True
        Me.FNFabricAccStockOtherCostPer.Width = 41
        '
        'gOtherStock
        '
        Me.gOtherStock.AppearanceHeader.Options.UseTextOptions = True
        Me.gOtherStock.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gOtherStock.Caption = "OtherStock"
        Me.gOtherStock.Columns.Add(Me.FNFabricAccStockOtherCost)
        Me.gOtherStock.Name = "gOtherStock"
        Me.gOtherStock.Visible = False
        Me.gOtherStock.VisibleIndex = -1
        Me.gOtherStock.Width = 183
        '
        'FNFabricAccStockOtherCost
        '
        Me.FNFabricAccStockOtherCost.Caption = "FNFabricAccStockOtherCost"
        Me.FNFabricAccStockOtherCost.DisplayFormat.FormatString = "{0:n2}"
        Me.FNFabricAccStockOtherCost.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNFabricAccStockOtherCost.FieldName = "FNFabricAccStockOtherCost"
        Me.FNFabricAccStockOtherCost.Name = "FNFabricAccStockOtherCost"
        Me.FNFabricAccStockOtherCost.OptionsColumn.AllowEdit = False
        Me.FNFabricAccStockOtherCost.Width = 183
        '
        'gFNOtherServiceCostPer
        '
        Me.gFNOtherServiceCostPer.Caption = "%"
        Me.gFNOtherServiceCostPer.Columns.Add(Me.FNOtherServiceCostPer)
        Me.gFNOtherServiceCostPer.Name = "gFNOtherServiceCostPer"
        Me.gFNOtherServiceCostPer.Visible = False
        Me.gFNOtherServiceCostPer.VisibleIndex = -1
        Me.gFNOtherServiceCostPer.Width = 45
        '
        'FNOtherServiceCostPer
        '
        Me.FNOtherServiceCostPer.Caption = "FNOtherServiceCostPer"
        Me.FNOtherServiceCostPer.DisplayFormat.FormatString = "{0:n2}"
        Me.FNOtherServiceCostPer.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNOtherServiceCostPer.FieldName = "FNOtherServiceCostPer"
        Me.FNOtherServiceCostPer.Name = "FNOtherServiceCostPer"
        Me.FNOtherServiceCostPer.OptionsColumn.AllowEdit = False
        Me.FNOtherServiceCostPer.Visible = True
        Me.FNOtherServiceCostPer.Width = 45
        '
        'gOtherServiceCost
        '
        Me.gOtherServiceCost.AppearanceHeader.Options.UseTextOptions = True
        Me.gOtherServiceCost.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gOtherServiceCost.Caption = "OtherServiceCost"
        Me.gOtherServiceCost.Columns.Add(Me.FNOtherServiceCost)
        Me.gOtherServiceCost.Name = "gOtherServiceCost"
        Me.gOtherServiceCost.Visible = False
        Me.gOtherServiceCost.VisibleIndex = -1
        Me.gOtherServiceCost.Width = 82
        '
        'FNOtherServiceCost
        '
        Me.FNOtherServiceCost.Caption = "FNOtherServiceCost"
        Me.FNOtherServiceCost.DisplayFormat.FormatString = "{0:n2}"
        Me.FNOtherServiceCost.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNOtherServiceCost.FieldName = "FNOtherServiceCost"
        Me.FNOtherServiceCost.Name = "FNOtherServiceCost"
        Me.FNOtherServiceCost.OptionsColumn.AllowEdit = False
        Me.FNOtherServiceCost.Visible = True
        Me.FNOtherServiceCost.Width = 82
        '
        'gFNIncomeAfterRawmaterialPer
        '
        Me.gFNIncomeAfterRawmaterialPer.Caption = "%"
        Me.gFNIncomeAfterRawmaterialPer.Columns.Add(Me.FNIncomeAfterRawmaterialPer)
        Me.gFNIncomeAfterRawmaterialPer.Name = "gFNIncomeAfterRawmaterialPer"
        Me.gFNIncomeAfterRawmaterialPer.Visible = False
        Me.gFNIncomeAfterRawmaterialPer.VisibleIndex = -1
        Me.gFNIncomeAfterRawmaterialPer.Width = 36
        '
        'FNIncomeAfterRawmaterialPer
        '
        Me.FNIncomeAfterRawmaterialPer.Caption = "FNIncomeAfterRawmaterialPer"
        Me.FNIncomeAfterRawmaterialPer.DisplayFormat.FormatString = "{0:n2}"
        Me.FNIncomeAfterRawmaterialPer.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNIncomeAfterRawmaterialPer.FieldName = "FNIncomeAfterRawmaterialPer"
        Me.FNIncomeAfterRawmaterialPer.Name = "FNIncomeAfterRawmaterialPer"
        Me.FNIncomeAfterRawmaterialPer.OptionsColumn.AllowEdit = False
        Me.FNIncomeAfterRawmaterialPer.Visible = True
        Me.FNIncomeAfterRawmaterialPer.Width = 36
        '
        'gFNIncomeAferRawmaterial
        '
        Me.gFNIncomeAferRawmaterial.Caption = "FNIncomeAferRawmaterial"
        Me.gFNIncomeAferRawmaterial.Columns.Add(Me.FNIncomeAfterRawmaterial)
        Me.gFNIncomeAferRawmaterial.Name = "gFNIncomeAferRawmaterial"
        Me.gFNIncomeAferRawmaterial.Visible = False
        Me.gFNIncomeAferRawmaterial.VisibleIndex = -1
        Me.gFNIncomeAferRawmaterial.Width = 178
        '
        'FNIncomeAfterRawmaterial
        '
        Me.FNIncomeAfterRawmaterial.Caption = "FNIncomeAfterRawmaterial"
        Me.FNIncomeAfterRawmaterial.ColumnEdit = Me.RepFNIncomeAfterRawmaterial
        Me.FNIncomeAfterRawmaterial.DisplayFormat.FormatString = "{0:n2}"
        Me.FNIncomeAfterRawmaterial.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNIncomeAfterRawmaterial.FieldName = "FNIncomeAfterRawmaterial"
        Me.FNIncomeAfterRawmaterial.Name = "FNIncomeAfterRawmaterial"
        Me.FNIncomeAfterRawmaterial.OptionsColumn.AllowEdit = False
        Me.FNIncomeAfterRawmaterial.Visible = True
        Me.FNIncomeAfterRawmaterial.Width = 178
        '
        'RepFNIncomeAfterRawmaterial
        '
        Me.RepFNIncomeAfterRawmaterial.AutoHeight = False
        Me.RepFNIncomeAfterRawmaterial.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepFNIncomeAfterRawmaterial.DisplayFormat.FormatString = "{0:n2}"
        Me.RepFNIncomeAfterRawmaterial.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.RepFNIncomeAfterRawmaterial.EditFormat.FormatString = "{0:n2}"
        Me.RepFNIncomeAfterRawmaterial.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.RepFNIncomeAfterRawmaterial.Name = "RepFNIncomeAfterRawmaterial"
        Me.RepFNIncomeAfterRawmaterial.Precision = 2
        '
        'gCenter7
        '
        Me.gCenter7.AppearanceHeader.Options.UseTextOptions = True
        Me.gCenter7.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gCenter7.Caption = "%"
        Me.gCenter7.Columns.Add(Me.FNConductedCostPer)
        Me.gCenter7.Name = "gCenter7"
        Me.gCenter7.Visible = False
        Me.gCenter7.VisibleIndex = -1
        Me.gCenter7.Width = 50
        '
        'FNConductedCostPer
        '
        Me.FNConductedCostPer.AppearanceCell.Options.UseTextOptions = True
        Me.FNConductedCostPer.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNConductedCostPer.Caption = "FNConductedCostPer"
        Me.FNConductedCostPer.DisplayFormat.FormatString = "{0:n2}"
        Me.FNConductedCostPer.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNConductedCostPer.FieldName = "FNConductedCostPer"
        Me.FNConductedCostPer.Name = "FNConductedCostPer"
        Me.FNConductedCostPer.OptionsColumn.AllowEdit = False
        Me.FNConductedCostPer.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNConductedCostPer.OptionsColumn.ReadOnly = True
        Me.FNConductedCostPer.Visible = True
        Me.FNConductedCostPer.Width = 50
        '
        'gCenter8
        '
        Me.gCenter8.AppearanceHeader.Options.UseTextOptions = True
        Me.gCenter8.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gCenter8.Caption = "ค่าดำเนินการ"
        Me.gCenter8.Columns.Add(Me.FNConductedCost)
        Me.gCenter8.Name = "gCenter8"
        Me.gCenter8.Visible = False
        Me.gCenter8.VisibleIndex = -1
        Me.gCenter8.Width = 100
        '
        'FNConductedCost
        '
        Me.FNConductedCost.AppearanceCell.Options.UseTextOptions = True
        Me.FNConductedCost.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNConductedCost.Caption = "FNConductedCost"
        Me.FNConductedCost.DisplayFormat.FormatString = "{0:n2}"
        Me.FNConductedCost.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNConductedCost.FieldName = "FNConductedCost"
        Me.FNConductedCost.Name = "FNConductedCost"
        Me.FNConductedCost.OptionsColumn.AllowEdit = False
        Me.FNConductedCost.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNConductedCost.OptionsColumn.ReadOnly = True
        Me.FNConductedCost.Visible = True
        Me.FNConductedCost.Width = 100
        '
        'gCenter9
        '
        Me.gCenter9.AppearanceHeader.Options.UseTextOptions = True
        Me.gCenter9.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gCenter9.Caption = "%"
        Me.gCenter9.Columns.Add(Me.FNOtherCostPer)
        Me.gCenter9.Name = "gCenter9"
        Me.gCenter9.Visible = False
        Me.gCenter9.VisibleIndex = -1
        Me.gCenter9.Width = 50
        '
        'FNOtherCostPer
        '
        Me.FNOtherCostPer.AppearanceCell.Options.UseTextOptions = True
        Me.FNOtherCostPer.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNOtherCostPer.Caption = "FNOtherCostPer"
        Me.FNOtherCostPer.DisplayFormat.FormatString = "{0:n2}"
        Me.FNOtherCostPer.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNOtherCostPer.FieldName = "FNOtherCostPer"
        Me.FNOtherCostPer.Name = "FNOtherCostPer"
        Me.FNOtherCostPer.OptionsColumn.AllowEdit = False
        Me.FNOtherCostPer.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNOtherCostPer.OptionsColumn.ReadOnly = True
        Me.FNOtherCostPer.Width = 50
        '
        'gCenter10
        '
        Me.gCenter10.AppearanceHeader.Options.UseTextOptions = True
        Me.gCenter10.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gCenter10.Caption = "ค่าเคลียร์"
        Me.gCenter10.Columns.Add(Me.FNOtherCost)
        Me.gCenter10.Name = "gCenter10"
        Me.gCenter10.Visible = False
        Me.gCenter10.VisibleIndex = -1
        Me.gCenter10.Width = 100
        '
        'FNOtherCost
        '
        Me.FNOtherCost.AppearanceCell.Options.UseTextOptions = True
        Me.FNOtherCost.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNOtherCost.Caption = "FNOtherCost"
        Me.FNOtherCost.DisplayFormat.FormatString = "{0:n2}"
        Me.FNOtherCost.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNOtherCost.FieldName = "FNOtherCost"
        Me.FNOtherCost.Name = "FNOtherCost"
        Me.FNOtherCost.OptionsColumn.AllowEdit = False
        Me.FNOtherCost.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNOtherCost.OptionsColumn.ReadOnly = True
        Me.FNOtherCost.Width = 100
        '
        'gCenter11
        '
        Me.gCenter11.AppearanceHeader.Options.UseTextOptions = True
        Me.gCenter11.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gCenter11.Caption = "%"
        Me.gCenter11.Columns.Add(Me.FNEmbFacCostPer)
        Me.gCenter11.Name = "gCenter11"
        Me.gCenter11.Visible = False
        Me.gCenter11.VisibleIndex = -1
        Me.gCenter11.Width = 50
        '
        'FNEmbFacCostPer
        '
        Me.FNEmbFacCostPer.AppearanceCell.Options.UseTextOptions = True
        Me.FNEmbFacCostPer.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNEmbFacCostPer.Caption = "FNEmbFacCostPer"
        Me.FNEmbFacCostPer.DisplayFormat.FormatString = "{0:n2}"
        Me.FNEmbFacCostPer.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNEmbFacCostPer.FieldName = "FNEmbFacCostPer"
        Me.FNEmbFacCostPer.Name = "FNEmbFacCostPer"
        Me.FNEmbFacCostPer.OptionsColumn.AllowEdit = False
        Me.FNEmbFacCostPer.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNEmbFacCostPer.OptionsColumn.ReadOnly = True
        Me.FNEmbFacCostPer.Visible = True
        Me.FNEmbFacCostPer.Width = 50
        '
        'gCenter12
        '
        Me.gCenter12.AppearanceHeader.Options.UseTextOptions = True
        Me.gCenter12.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gCenter12.Caption = "ปัก 91"
        Me.gCenter12.Columns.Add(Me.FNEmbFacCost)
        Me.gCenter12.Name = "gCenter12"
        Me.gCenter12.Visible = False
        Me.gCenter12.VisibleIndex = -1
        Me.gCenter12.Width = 164
        '
        'FNEmbFacCost
        '
        Me.FNEmbFacCost.AppearanceCell.Options.UseTextOptions = True
        Me.FNEmbFacCost.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNEmbFacCost.Caption = "FNEmbFacCost"
        Me.FNEmbFacCost.DisplayFormat.FormatString = "{0:n2}"
        Me.FNEmbFacCost.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNEmbFacCost.FieldName = "FNEmbFacCost"
        Me.FNEmbFacCost.Name = "FNEmbFacCost"
        Me.FNEmbFacCost.OptionsColumn.AllowEdit = False
        Me.FNEmbFacCost.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNEmbFacCost.OptionsColumn.ReadOnly = True
        Me.FNEmbFacCost.Visible = True
        Me.FNEmbFacCost.Width = 164
        '
        'gCenter13
        '
        Me.gCenter13.AppearanceHeader.Options.UseTextOptions = True
        Me.gCenter13.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gCenter13.Caption = "%"
        Me.gCenter13.Columns.Add(Me.FNEmpPrintBranchPer)
        Me.gCenter13.Name = "gCenter13"
        Me.gCenter13.Visible = False
        Me.gCenter13.VisibleIndex = -1
        Me.gCenter13.Width = 50
        '
        'FNEmpPrintBranchPer
        '
        Me.FNEmpPrintBranchPer.AppearanceCell.Options.UseTextOptions = True
        Me.FNEmpPrintBranchPer.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNEmpPrintBranchPer.Caption = "FNEmpPrintBranchPer"
        Me.FNEmpPrintBranchPer.DisplayFormat.FormatString = "{0:n2}"
        Me.FNEmpPrintBranchPer.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNEmpPrintBranchPer.FieldName = "FNEmpPrintBranchPer"
        Me.FNEmpPrintBranchPer.Name = "FNEmpPrintBranchPer"
        Me.FNEmpPrintBranchPer.OptionsColumn.AllowEdit = False
        Me.FNEmpPrintBranchPer.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNEmpPrintBranchPer.OptionsColumn.ReadOnly = True
        Me.FNEmpPrintBranchPer.Visible = True
        Me.FNEmpPrintBranchPer.Width = 50
        '
        'gCenter14
        '
        Me.gCenter14.Caption = "ปักพิมพ์"
        Me.gCenter14.Columns.Add(Me.FNEmpPrintBranch)
        Me.gCenter14.Name = "gCenter14"
        Me.gCenter14.Visible = False
        Me.gCenter14.VisibleIndex = -1
        Me.gCenter14.Width = 145
        '
        'FNEmpPrintBranch
        '
        Me.FNEmpPrintBranch.AppearanceCell.Options.UseTextOptions = True
        Me.FNEmpPrintBranch.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNEmpPrintBranch.Caption = "FNEmpPrintBranch"
        Me.FNEmpPrintBranch.DisplayFormat.FormatString = "{0:n2}"
        Me.FNEmpPrintBranch.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNEmpPrintBranch.FieldName = "FNEmpPrintBranch"
        Me.FNEmpPrintBranch.Name = "FNEmpPrintBranch"
        Me.FNEmpPrintBranch.OptionsColumn.AllowEdit = False
        Me.FNEmpPrintBranch.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNEmpPrintBranch.OptionsColumn.ReadOnly = True
        Me.FNEmpPrintBranch.Visible = True
        Me.FNEmpPrintBranch.Width = 145
        '
        'gFNWagePullPer
        '
        Me.gFNWagePullPer.AppearanceHeader.Options.UseTextOptions = True
        Me.gFNWagePullPer.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gFNWagePullPer.Caption = "%"
        Me.gFNWagePullPer.Columns.Add(Me.FNWagePullPer)
        Me.gFNWagePullPer.Name = "gFNWagePullPer"
        Me.gFNWagePullPer.Visible = False
        Me.gFNWagePullPer.VisibleIndex = -1
        Me.gFNWagePullPer.Width = 75
        '
        'FNWagePullPer
        '
        Me.FNWagePullPer.Caption = "FNWagePullPer"
        Me.FNWagePullPer.DisplayFormat.FormatString = "{n:n2}"
        Me.FNWagePullPer.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNWagePullPer.FieldName = "FNWagePullPer"
        Me.FNWagePullPer.Name = "FNWagePullPer"
        Me.FNWagePullPer.OptionsColumn.AllowEdit = False
        Me.FNWagePullPer.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNWagePullPer.OptionsColumn.ReadOnly = True
        Me.FNWagePullPer.Visible = True
        '
        'gFNWagePull
        '
        Me.gFNWagePull.Caption = "FNWagePull"
        Me.gFNWagePull.Columns.Add(Me.FNWagePull)
        Me.gFNWagePull.Name = "gFNWagePull"
        Me.gFNWagePull.Visible = False
        Me.gFNWagePull.VisibleIndex = -1
        Me.gFNWagePull.Width = 75
        '
        'FNWagePull
        '
        Me.FNWagePull.Caption = "FNWagePull"
        Me.FNWagePull.DisplayFormat.FormatString = "{n:n2}"
        Me.FNWagePull.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNWagePull.FieldName = "FNWagePull"
        Me.FNWagePull.Name = "FNWagePull"
        Me.FNWagePull.OptionsColumn.AllowEdit = False
        Me.FNWagePull.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNWagePull.OptionsColumn.ReadOnly = True
        Me.FNWagePull.Visible = True
        '
        'gBtanch
        '
        Me.gBtanch.AppearanceHeader.Options.UseTextOptions = True
        Me.gBtanch.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gBtanch.Caption = "สาขา"
        Me.gBtanch.Children.AddRange(New DevExpress.XtraGrid.Views.BandedGrid.GridBand() {Me.gBtanch1, Me.gBtanch2, Me.gBtanch3, Me.gBtanch4, Me.gFNSewCostPer, Me.gSewCost, Me.gEmbroideryCostPer, Me.gEmbroidCost, Me.gFNPrintCostPer, Me.gPrintCost, Me.gBtanch5, Me.gBtanch6, Me.gFNImportCostPer, Me.gImportCost, Me.gBtanch7, Me.gBtanch8, Me.gBtanch9, Me.gBtanch10, Me.gBtanch11, Me.gBtanch12, Me.gBtanch13, Me.gBtanch14, Me.gEtcCostper, Me.gEtcCost})
        Me.gBtanch.Name = "gBtanch"
        Me.gBtanch.VisibleIndex = 6
        Me.gBtanch.Width = 301
        '
        'gBtanch1
        '
        Me.gBtanch1.AppearanceHeader.Options.UseTextOptions = True
        Me.gBtanch1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gBtanch1.Caption = "%"
        Me.gBtanch1.Columns.Add(Me.FNExportAmtBFPer)
        Me.gBtanch1.Name = "gBtanch1"
        Me.gBtanch1.Visible = False
        Me.gBtanch1.VisibleIndex = -1
        Me.gBtanch1.Width = 50
        '
        'FNExportAmtBFPer
        '
        Me.FNExportAmtBFPer.AppearanceCell.Options.UseTextOptions = True
        Me.FNExportAmtBFPer.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNExportAmtBFPer.Caption = "FNExportAmtBFPer"
        Me.FNExportAmtBFPer.DisplayFormat.FormatString = "{0:n2}"
        Me.FNExportAmtBFPer.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNExportAmtBFPer.FieldName = "FNExportAmtBFPer"
        Me.FNExportAmtBFPer.Name = "FNExportAmtBFPer"
        Me.FNExportAmtBFPer.OptionsColumn.AllowEdit = False
        Me.FNExportAmtBFPer.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNExportAmtBFPer.OptionsColumn.ReadOnly = True
        Me.FNExportAmtBFPer.Visible = True
        Me.FNExportAmtBFPer.Width = 50
        '
        'gBtanch2
        '
        Me.gBtanch2.AppearanceHeader.Options.UseTextOptions = True
        Me.gBtanch2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gBtanch2.Caption = "รายได้"
        Me.gBtanch2.Columns.Add(Me.FNExportAmtBF)
        Me.gBtanch2.Name = "gBtanch2"
        Me.gBtanch2.Visible = False
        Me.gBtanch2.VisibleIndex = -1
        Me.gBtanch2.Width = 100
        '
        'FNExportAmtBF
        '
        Me.FNExportAmtBF.AppearanceCell.Options.UseTextOptions = True
        Me.FNExportAmtBF.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNExportAmtBF.Caption = "FNExportAmtBF"
        Me.FNExportAmtBF.DisplayFormat.FormatString = "{0:n2}"
        Me.FNExportAmtBF.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNExportAmtBF.FieldName = "FNExportAmtBF"
        Me.FNExportAmtBF.Name = "FNExportAmtBF"
        Me.FNExportAmtBF.OptionsColumn.AllowEdit = False
        Me.FNExportAmtBF.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNExportAmtBF.OptionsColumn.ReadOnly = True
        Me.FNExportAmtBF.Visible = True
        Me.FNExportAmtBF.Width = 100
        '
        'gBtanch3
        '
        Me.gBtanch3.AppearanceHeader.Options.UseTextOptions = True
        Me.gBtanch3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gBtanch3.Caption = "%"
        Me.gBtanch3.Columns.Add(Me.FNWageCostPer)
        Me.gBtanch3.Name = "gBtanch3"
        Me.gBtanch3.VisibleIndex = 0
        Me.gBtanch3.Width = 50
        '
        'FNWageCostPer
        '
        Me.FNWageCostPer.AppearanceCell.Options.UseTextOptions = True
        Me.FNWageCostPer.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNWageCostPer.Caption = "FNWageCostPer"
        Me.FNWageCostPer.DisplayFormat.FormatString = "{0:n2}"
        Me.FNWageCostPer.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNWageCostPer.FieldName = "FNWageCostPer"
        Me.FNWageCostPer.Name = "FNWageCostPer"
        Me.FNWageCostPer.OptionsColumn.AllowEdit = False
        Me.FNWageCostPer.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNWageCostPer.OptionsColumn.ReadOnly = True
        Me.FNWageCostPer.Visible = True
        Me.FNWageCostPer.Width = 50
        '
        'gBtanch4
        '
        Me.gBtanch4.AppearanceHeader.Options.UseTextOptions = True
        Me.gBtanch4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gBtanch4.Caption = "ค่าแรง"
        Me.gBtanch4.Columns.Add(Me.FNWageCost)
        Me.gBtanch4.Name = "gBtanch4"
        Me.gBtanch4.VisibleIndex = 1
        Me.gBtanch4.Width = 100
        '
        'FNWageCost
        '
        Me.FNWageCost.AppearanceCell.Options.UseTextOptions = True
        Me.FNWageCost.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNWageCost.Caption = "FNWageCost"
        Me.FNWageCost.ColumnEdit = Me.RepFNWageCost
        Me.FNWageCost.DisplayFormat.FormatString = "{0:n2}"
        Me.FNWageCost.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNWageCost.FieldName = "FNWageCost"
        Me.FNWageCost.Name = "FNWageCost"
        Me.FNWageCost.OptionsColumn.AllowEdit = False
        Me.FNWageCost.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNWageCost.OptionsColumn.ReadOnly = True
        Me.FNWageCost.Visible = True
        Me.FNWageCost.Width = 100
        '
        'RepFNWageCost
        '
        Me.RepFNWageCost.AutoHeight = False
        Me.RepFNWageCost.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepFNWageCost.DisplayFormat.FormatString = "{0:n2}"
        Me.RepFNWageCost.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.RepFNWageCost.EditFormat.FormatString = "{0:n2}"
        Me.RepFNWageCost.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.RepFNWageCost.Name = "RepFNWageCost"
        Me.RepFNWageCost.Precision = 2
        '
        'gFNSewCostPer
        '
        Me.gFNSewCostPer.Caption = "%"
        Me.gFNSewCostPer.Columns.Add(Me.FNSewCostPer)
        Me.gFNSewCostPer.Name = "gFNSewCostPer"
        Me.gFNSewCostPer.Visible = False
        Me.gFNSewCostPer.VisibleIndex = -1
        Me.gFNSewCostPer.Width = 44
        '
        'FNSewCostPer
        '
        Me.FNSewCostPer.Caption = "FNSewCostPer"
        Me.FNSewCostPer.DisplayFormat.FormatString = "{0:n2}"
        Me.FNSewCostPer.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNSewCostPer.FieldName = "FNSewCostPer"
        Me.FNSewCostPer.Name = "FNSewCostPer"
        Me.FNSewCostPer.OptionsColumn.AllowEdit = False
        Me.FNSewCostPer.Visible = True
        Me.FNSewCostPer.Width = 44
        '
        'gSewCost
        '
        Me.gSewCost.AppearanceHeader.Options.UseTextOptions = True
        Me.gSewCost.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gSewCost.Caption = "Sew Cost"
        Me.gSewCost.Columns.Add(Me.FNSewCost)
        Me.gSewCost.Name = "gSewCost"
        Me.gSewCost.Visible = False
        Me.gSewCost.VisibleIndex = -1
        Me.gSewCost.Width = 75
        '
        'FNSewCost
        '
        Me.FNSewCost.Caption = "FNSewCost"
        Me.FNSewCost.DisplayFormat.FormatString = "{0:n2}"
        Me.FNSewCost.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNSewCost.FieldName = "FNSewCost"
        Me.FNSewCost.Name = "FNSewCost"
        Me.FNSewCost.OptionsColumn.AllowEdit = False
        Me.FNSewCost.Visible = True
        '
        'gEmbroideryCostPer
        '
        Me.gEmbroideryCostPer.Caption = "%"
        Me.gEmbroideryCostPer.Columns.Add(Me.FNEmbroideryCostPer)
        Me.gEmbroideryCostPer.Name = "gEmbroideryCostPer"
        Me.gEmbroideryCostPer.Visible = False
        Me.gEmbroideryCostPer.VisibleIndex = -1
        Me.gEmbroideryCostPer.Width = 53
        '
        'FNEmbroideryCostPer
        '
        Me.FNEmbroideryCostPer.Caption = "FNEmbroideryCostPer"
        Me.FNEmbroideryCostPer.DisplayFormat.FormatString = "{0:n2}"
        Me.FNEmbroideryCostPer.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNEmbroideryCostPer.FieldName = "FNEmbroideryCostPer"
        Me.FNEmbroideryCostPer.Name = "FNEmbroideryCostPer"
        Me.FNEmbroideryCostPer.OptionsColumn.AllowEdit = False
        Me.FNEmbroideryCostPer.Visible = True
        Me.FNEmbroideryCostPer.Width = 53
        '
        'gEmbroidCost
        '
        Me.gEmbroidCost.AppearanceHeader.Options.UseTextOptions = True
        Me.gEmbroidCost.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gEmbroidCost.Caption = "EmbroidCost"
        Me.gEmbroidCost.Columns.Add(Me.FNEmbroideryCost)
        Me.gEmbroidCost.Name = "gEmbroidCost"
        Me.gEmbroidCost.Visible = False
        Me.gEmbroidCost.VisibleIndex = -1
        Me.gEmbroidCost.Width = 125
        '
        'FNEmbroideryCost
        '
        Me.FNEmbroideryCost.Caption = "FNEmbroideryCost"
        Me.FNEmbroideryCost.DisplayFormat.FormatString = "{0:n2}"
        Me.FNEmbroideryCost.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNEmbroideryCost.FieldName = "FNEmbroideryCost"
        Me.FNEmbroideryCost.Name = "FNEmbroideryCost"
        Me.FNEmbroideryCost.OptionsColumn.AllowEdit = False
        Me.FNEmbroideryCost.Visible = True
        Me.FNEmbroideryCost.Width = 125
        '
        'gFNPrintCostPer
        '
        Me.gFNPrintCostPer.Caption = "%"
        Me.gFNPrintCostPer.Columns.Add(Me.FNPrintCostPer)
        Me.gFNPrintCostPer.Name = "gFNPrintCostPer"
        Me.gFNPrintCostPer.Visible = False
        Me.gFNPrintCostPer.VisibleIndex = -1
        Me.gFNPrintCostPer.Width = 47
        '
        'FNPrintCostPer
        '
        Me.FNPrintCostPer.Caption = "FNPrintCostPer"
        Me.FNPrintCostPer.DisplayFormat.FormatString = "{0:n2}"
        Me.FNPrintCostPer.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNPrintCostPer.FieldName = "FNPrintCostPer"
        Me.FNPrintCostPer.Name = "FNPrintCostPer"
        Me.FNPrintCostPer.OptionsColumn.AllowEdit = False
        Me.FNPrintCostPer.Visible = True
        Me.FNPrintCostPer.Width = 47
        '
        'gPrintCost
        '
        Me.gPrintCost.AppearanceHeader.Options.UseTextOptions = True
        Me.gPrintCost.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gPrintCost.Caption = "PrintCost"
        Me.gPrintCost.Columns.Add(Me.FNPrintCost)
        Me.gPrintCost.Name = "gPrintCost"
        Me.gPrintCost.Visible = False
        Me.gPrintCost.VisibleIndex = -1
        Me.gPrintCost.Width = 100
        '
        'FNPrintCost
        '
        Me.FNPrintCost.Caption = "FNPrintCost"
        Me.FNPrintCost.DisplayFormat.FormatString = "{0:n2}"
        Me.FNPrintCost.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNPrintCost.FieldName = "FNPrintCost"
        Me.FNPrintCost.Name = "FNPrintCost"
        Me.FNPrintCost.OptionsColumn.AllowEdit = False
        Me.FNPrintCost.Visible = True
        Me.FNPrintCost.Width = 100
        '
        'gBtanch5
        '
        Me.gBtanch5.AppearanceHeader.Options.UseTextOptions = True
        Me.gBtanch5.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gBtanch5.Caption = "%"
        Me.gBtanch5.Columns.Add(Me.FNEmpPrintSubPer)
        Me.gBtanch5.Name = "gBtanch5"
        Me.gBtanch5.Visible = False
        Me.gBtanch5.VisibleIndex = -1
        Me.gBtanch5.Width = 50
        '
        'FNEmpPrintSubPer
        '
        Me.FNEmpPrintSubPer.AppearanceCell.Options.UseTextOptions = True
        Me.FNEmpPrintSubPer.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNEmpPrintSubPer.Caption = "FNEmpPrintSubPer"
        Me.FNEmpPrintSubPer.DisplayFormat.FormatString = "{0:n2}"
        Me.FNEmpPrintSubPer.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNEmpPrintSubPer.FieldName = "FNEmpPrintSubPer"
        Me.FNEmpPrintSubPer.Name = "FNEmpPrintSubPer"
        Me.FNEmpPrintSubPer.OptionsColumn.AllowEdit = False
        Me.FNEmpPrintSubPer.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNEmpPrintSubPer.OptionsColumn.ReadOnly = True
        Me.FNEmpPrintSubPer.Visible = True
        Me.FNEmpPrintSubPer.Width = 50
        '
        'gBtanch6
        '
        Me.gBtanch6.AppearanceHeader.Options.UseTextOptions = True
        Me.gBtanch6.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gBtanch6.Caption = "Sub นอก"
        Me.gBtanch6.Columns.Add(Me.FNEmpPrintSub)
        Me.gBtanch6.Name = "gBtanch6"
        Me.gBtanch6.Visible = False
        Me.gBtanch6.VisibleIndex = -1
        Me.gBtanch6.Width = 100
        '
        'FNEmpPrintSub
        '
        Me.FNEmpPrintSub.AppearanceCell.Options.UseTextOptions = True
        Me.FNEmpPrintSub.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNEmpPrintSub.Caption = "FNEmpPrintSub"
        Me.FNEmpPrintSub.DisplayFormat.FormatString = "{0:n2}"
        Me.FNEmpPrintSub.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNEmpPrintSub.FieldName = "FNEmpPrintSub"
        Me.FNEmpPrintSub.Name = "FNEmpPrintSub"
        Me.FNEmpPrintSub.OptionsColumn.AllowEdit = False
        Me.FNEmpPrintSub.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNEmpPrintSub.OptionsColumn.ReadOnly = True
        Me.FNEmpPrintSub.Visible = True
        Me.FNEmpPrintSub.Width = 100
        '
        'gFNImportCostPer
        '
        Me.gFNImportCostPer.Caption = "%"
        Me.gFNImportCostPer.Columns.Add(Me.FNImportCostPer)
        Me.gFNImportCostPer.Name = "gFNImportCostPer"
        Me.gFNImportCostPer.Visible = False
        Me.gFNImportCostPer.VisibleIndex = -1
        Me.gFNImportCostPer.Width = 49
        '
        'FNImportCostPer
        '
        Me.FNImportCostPer.Caption = "FNImportCostPer"
        Me.FNImportCostPer.DisplayFormat.FormatString = "{0:n2}"
        Me.FNImportCostPer.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNImportCostPer.FieldName = "FNImportCostPer"
        Me.FNImportCostPer.Name = "FNImportCostPer"
        Me.FNImportCostPer.OptionsColumn.AllowEdit = False
        Me.FNImportCostPer.Visible = True
        Me.FNImportCostPer.Width = 49
        '
        'gImportCost
        '
        Me.gImportCost.AppearanceHeader.Options.UseTextOptions = True
        Me.gImportCost.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gImportCost.Caption = "ImportCost"
        Me.gImportCost.Columns.Add(Me.FNImportCost)
        Me.gImportCost.Name = "gImportCost"
        Me.gImportCost.Visible = False
        Me.gImportCost.VisibleIndex = -1
        Me.gImportCost.Width = 75
        '
        'FNImportCost
        '
        Me.FNImportCost.Caption = "FNImportCost"
        Me.FNImportCost.DisplayFormat.FormatString = "{0:n2}"
        Me.FNImportCost.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNImportCost.FieldName = "FNImportCost"
        Me.FNImportCost.Name = "FNImportCost"
        Me.FNImportCost.OptionsColumn.AllowEdit = False
        Me.FNImportCost.Visible = True
        '
        'gBtanch7
        '
        Me.gBtanch7.AppearanceHeader.Options.UseTextOptions = True
        Me.gBtanch7.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gBtanch7.Caption = "%"
        Me.gBtanch7.Columns.Add(Me.FNImportExportCostPer)
        Me.gBtanch7.Name = "gBtanch7"
        Me.gBtanch7.Visible = False
        Me.gBtanch7.VisibleIndex = -1
        Me.gBtanch7.Width = 49
        '
        'FNImportExportCostPer
        '
        Me.FNImportExportCostPer.AppearanceCell.Options.UseTextOptions = True
        Me.FNImportExportCostPer.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNImportExportCostPer.Caption = "FNImportExportCostPer"
        Me.FNImportExportCostPer.DisplayFormat.FormatString = "{0:n2}"
        Me.FNImportExportCostPer.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNImportExportCostPer.FieldName = "FNImportExportCostPer"
        Me.FNImportExportCostPer.Name = "FNImportExportCostPer"
        Me.FNImportExportCostPer.OptionsColumn.AllowEdit = False
        Me.FNImportExportCostPer.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNImportExportCostPer.OptionsColumn.ReadOnly = True
        Me.FNImportExportCostPer.Visible = True
        Me.FNImportExportCostPer.Width = 49
        '
        'gBtanch8
        '
        Me.gBtanch8.AppearanceHeader.Options.UseTextOptions = True
        Me.gBtanch8.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gBtanch8.Caption = "คชจ. นำเข้า+ส่งออก"
        Me.gBtanch8.Columns.Add(Me.FNImportExportCost)
        Me.gBtanch8.Name = "gBtanch8"
        Me.gBtanch8.Visible = False
        Me.gBtanch8.VisibleIndex = -1
        Me.gBtanch8.Width = 125
        '
        'FNImportExportCost
        '
        Me.FNImportExportCost.AppearanceCell.Options.UseTextOptions = True
        Me.FNImportExportCost.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNImportExportCost.Caption = "FNImportExportCost"
        Me.FNImportExportCost.DisplayFormat.FormatString = "{0:n2}"
        Me.FNImportExportCost.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNImportExportCost.FieldName = "FNImportExportCost"
        Me.FNImportExportCost.Name = "FNImportExportCost"
        Me.FNImportExportCost.OptionsColumn.AllowEdit = False
        Me.FNImportExportCost.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNImportExportCost.OptionsColumn.ReadOnly = True
        Me.FNImportExportCost.Visible = True
        Me.FNImportExportCost.Width = 125
        '
        'gBtanch9
        '
        Me.gBtanch9.AppearanceHeader.Options.UseTextOptions = True
        Me.gBtanch9.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gBtanch9.Caption = "%"
        Me.gBtanch9.Columns.Add(Me.FNProdCostPer)
        Me.gBtanch9.Name = "gBtanch9"
        Me.gBtanch9.VisibleIndex = 2
        Me.gBtanch9.Width = 51
        '
        'FNProdCostPer
        '
        Me.FNProdCostPer.AppearanceCell.Options.UseTextOptions = True
        Me.FNProdCostPer.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNProdCostPer.Caption = "FNProdCostPer"
        Me.FNProdCostPer.DisplayFormat.FormatString = "{0:n2}"
        Me.FNProdCostPer.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNProdCostPer.FieldName = "FNProdCostPer"
        Me.FNProdCostPer.Name = "FNProdCostPer"
        Me.FNProdCostPer.OptionsColumn.AllowEdit = False
        Me.FNProdCostPer.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNProdCostPer.OptionsColumn.ReadOnly = True
        Me.FNProdCostPer.Visible = True
        Me.FNProdCostPer.Width = 51
        '
        'gBtanch10
        '
        Me.gBtanch10.AppearanceHeader.Options.UseTextOptions = True
        Me.gBtanch10.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gBtanch10.Caption = "คชจ. ผลิต"
        Me.gBtanch10.Columns.Add(Me.FNProdCost)
        Me.gBtanch10.Name = "gBtanch10"
        Me.gBtanch10.VisibleIndex = 3
        Me.gBtanch10.Width = 100
        '
        'FNProdCost
        '
        Me.FNProdCost.AppearanceCell.Options.UseTextOptions = True
        Me.FNProdCost.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNProdCost.Caption = "FNProdCost"
        Me.FNProdCost.ColumnEdit = Me.RepFNProdCost
        Me.FNProdCost.DisplayFormat.FormatString = "{0:n2}"
        Me.FNProdCost.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNProdCost.FieldName = "FNProdCost"
        Me.FNProdCost.Name = "FNProdCost"
        Me.FNProdCost.OptionsColumn.AllowEdit = False
        Me.FNProdCost.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNProdCost.OptionsColumn.ReadOnly = True
        Me.FNProdCost.Visible = True
        Me.FNProdCost.Width = 100
        '
        'RepFNProdCost
        '
        Me.RepFNProdCost.AutoHeight = False
        Me.RepFNProdCost.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepFNProdCost.DisplayFormat.FormatString = "{0:n2}"
        Me.RepFNProdCost.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.RepFNProdCost.EditFormat.FormatString = "{0:n2}"
        Me.RepFNProdCost.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.RepFNProdCost.Name = "RepFNProdCost"
        Me.RepFNProdCost.Precision = 2
        '
        'gBtanch11
        '
        Me.gBtanch11.AppearanceHeader.Options.UseTextOptions = True
        Me.gBtanch11.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.gBtanch11.Caption = "%"
        Me.gBtanch11.Columns.Add(Me.FNCommissionCostPer)
        Me.gBtanch11.Name = "gBtanch11"
        Me.gBtanch11.Visible = False
        Me.gBtanch11.VisibleIndex = -1
        Me.gBtanch11.Width = 50
        '
        'FNCommissionCostPer
        '
        Me.FNCommissionCostPer.AppearanceCell.Options.UseTextOptions = True
        Me.FNCommissionCostPer.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNCommissionCostPer.Caption = "FNCommissionCostPer"
        Me.FNCommissionCostPer.DisplayFormat.FormatString = "{0:n2}"
        Me.FNCommissionCostPer.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNCommissionCostPer.FieldName = "FNCommissionCostPer"
        Me.FNCommissionCostPer.Name = "FNCommissionCostPer"
        Me.FNCommissionCostPer.OptionsColumn.AllowEdit = False
        Me.FNCommissionCostPer.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNCommissionCostPer.OptionsColumn.ReadOnly = True
        Me.FNCommissionCostPer.Visible = True
        Me.FNCommissionCostPer.Width = 50
        '
        'gBtanch12
        '
        Me.gBtanch12.AppearanceHeader.Options.UseTextOptions = True
        Me.gBtanch12.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gBtanch12.Caption = "ค่านายหน้า"
        Me.gBtanch12.Columns.Add(Me.FNCommissionCost)
        Me.gBtanch12.Name = "gBtanch12"
        Me.gBtanch12.Visible = False
        Me.gBtanch12.VisibleIndex = -1
        Me.gBtanch12.Width = 100
        '
        'FNCommissionCost
        '
        Me.FNCommissionCost.AppearanceCell.Options.UseTextOptions = True
        Me.FNCommissionCost.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNCommissionCost.Caption = "FNCommissionCost"
        Me.FNCommissionCost.DisplayFormat.FormatString = "{0:n2}"
        Me.FNCommissionCost.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNCommissionCost.FieldName = "FNCommissionCost"
        Me.FNCommissionCost.Name = "FNCommissionCost"
        Me.FNCommissionCost.OptionsColumn.AllowEdit = False
        Me.FNCommissionCost.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNCommissionCost.OptionsColumn.ReadOnly = True
        Me.FNCommissionCost.Visible = True
        Me.FNCommissionCost.Width = 100
        '
        'gBtanch13
        '
        Me.gBtanch13.AppearanceHeader.Options.UseTextOptions = True
        Me.gBtanch13.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gBtanch13.Caption = "%"
        Me.gBtanch13.Columns.Add(Me.FNTransportAirCostPer)
        Me.gBtanch13.Name = "gBtanch13"
        Me.gBtanch13.Visible = False
        Me.gBtanch13.VisibleIndex = -1
        Me.gBtanch13.Width = 50
        '
        'FNTransportAirCostPer
        '
        Me.FNTransportAirCostPer.AppearanceCell.Options.UseTextOptions = True
        Me.FNTransportAirCostPer.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNTransportAirCostPer.Caption = "FNTransportAirCostPer"
        Me.FNTransportAirCostPer.DisplayFormat.FormatString = "{0:n2}"
        Me.FNTransportAirCostPer.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNTransportAirCostPer.FieldName = "FNTransportAirCostPer"
        Me.FNTransportAirCostPer.Name = "FNTransportAirCostPer"
        Me.FNTransportAirCostPer.OptionsColumn.AllowEdit = False
        Me.FNTransportAirCostPer.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNTransportAirCostPer.OptionsColumn.ReadOnly = True
        Me.FNTransportAirCostPer.Visible = True
        Me.FNTransportAirCostPer.Width = 50
        '
        'gBtanch14
        '
        Me.gBtanch14.AppearanceHeader.Options.UseTextOptions = True
        Me.gBtanch14.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gBtanch14.Caption = "แอร์"
        Me.gBtanch14.Columns.Add(Me.FNTransportAirCost)
        Me.gBtanch14.Name = "gBtanch14"
        Me.gBtanch14.Visible = False
        Me.gBtanch14.VisibleIndex = -1
        Me.gBtanch14.Width = 100
        '
        'FNTransportAirCost
        '
        Me.FNTransportAirCost.AppearanceCell.Options.UseTextOptions = True
        Me.FNTransportAirCost.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNTransportAirCost.Caption = "FNTransportAirCost"
        Me.FNTransportAirCost.DisplayFormat.FormatString = "{0:n2}"
        Me.FNTransportAirCost.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNTransportAirCost.FieldName = "FNTransportAirCost"
        Me.FNTransportAirCost.Name = "FNTransportAirCost"
        Me.FNTransportAirCost.OptionsColumn.AllowEdit = False
        Me.FNTransportAirCost.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNTransportAirCost.OptionsColumn.ReadOnly = True
        Me.FNTransportAirCost.Visible = True
        Me.FNTransportAirCost.Width = 100
        '
        'gEtcCostper
        '
        Me.gEtcCostper.Caption = "%"
        Me.gEtcCostper.Columns.Add(Me.FNEtcCostPer)
        Me.gEtcCostper.Name = "gEtcCostper"
        Me.gEtcCostper.Visible = False
        Me.gEtcCostper.VisibleIndex = -1
        Me.gEtcCostper.Width = 54
        '
        'FNEtcCostPer
        '
        Me.FNEtcCostPer.Caption = "FNEtcCostPer"
        Me.FNEtcCostPer.DisplayFormat.FormatString = "{0:n2}"
        Me.FNEtcCostPer.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNEtcCostPer.FieldName = "FNEtcCostPer"
        Me.FNEtcCostPer.Name = "FNEtcCostPer"
        Me.FNEtcCostPer.OptionsColumn.AllowEdit = False
        Me.FNEtcCostPer.Visible = True
        Me.FNEtcCostPer.Width = 54
        '
        'gEtcCost
        '
        Me.gEtcCost.AppearanceHeader.Options.UseTextOptions = True
        Me.gEtcCost.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gEtcCost.Caption = "EtcCost"
        Me.gEtcCost.Columns.Add(Me.FNEtcCost)
        Me.gEtcCost.Name = "gEtcCost"
        Me.gEtcCost.Visible = False
        Me.gEtcCost.VisibleIndex = -1
        Me.gEtcCost.Width = 75
        '
        'FNEtcCost
        '
        Me.FNEtcCost.Caption = "FNEtcCost"
        Me.FNEtcCost.DisplayFormat.FormatString = "{0:n2}"
        Me.FNEtcCost.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNEtcCost.FieldName = "FNEtcCost"
        Me.FNEtcCost.Name = "FNEtcCost"
        Me.FNEtcCost.OptionsColumn.AllowEdit = False
        Me.FNEtcCost.Visible = True
        '
        'gProfit
        '
        Me.gProfit.AppearanceHeader.Options.UseTextOptions = True
        Me.gProfit.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gProfit.Caption = "กำไรขาดทุน"
        Me.gProfit.Children.AddRange(New DevExpress.XtraGrid.Views.BandedGrid.GridBand() {Me.gProfit1, Me.gProfit2, Me.gFNNetProfitRcv})
        Me.gProfit.Name = "gProfit"
        Me.gProfit.VisibleIndex = 7
        Me.gProfit.Width = 310
        '
        'gProfit1
        '
        Me.gProfit1.AppearanceHeader.Options.UseTextOptions = True
        Me.gProfit1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gProfit1.Caption = "%"
        Me.gProfit1.Columns.Add(Me.FNNetProfitPer)
        Me.gProfit1.Name = "gProfit1"
        Me.gProfit1.VisibleIndex = 0
        Me.gProfit1.Width = 45
        '
        'FNNetProfitPer
        '
        Me.FNNetProfitPer.AppearanceCell.Options.UseTextOptions = True
        Me.FNNetProfitPer.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNNetProfitPer.Caption = "FNNetProfitPer"
        Me.FNNetProfitPer.FieldName = "FNNetProfitPer"
        Me.FNNetProfitPer.Name = "FNNetProfitPer"
        Me.FNNetProfitPer.OptionsColumn.AllowEdit = False
        Me.FNNetProfitPer.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNNetProfitPer.OptionsColumn.ReadOnly = True
        Me.FNNetProfitPer.Visible = True
        Me.FNNetProfitPer.Width = 45
        '
        'gProfit2
        '
        Me.gProfit2.AppearanceHeader.Options.UseTextOptions = True
        Me.gProfit2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gProfit2.Caption = "มูลค่า"
        Me.gProfit2.Columns.Add(Me.FNNetProfit)
        Me.gProfit2.Name = "gProfit2"
        Me.gProfit2.Visible = False
        Me.gProfit2.VisibleIndex = -1
        Me.gProfit2.Width = 202
        '
        'FNNetProfit
        '
        Me.FNNetProfit.AppearanceCell.Options.UseTextOptions = True
        Me.FNNetProfit.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNNetProfit.Caption = "FNNetProfit"
        Me.FNNetProfit.DisplayFormat.FormatString = "{0:n2}"
        Me.FNNetProfit.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNNetProfit.FieldName = "FNNetProfit"
        Me.FNNetProfit.Name = "FNNetProfit"
        Me.FNNetProfit.OptionsColumn.AllowEdit = False
        Me.FNNetProfit.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNNetProfit.OptionsColumn.ReadOnly = True
        Me.FNNetProfit.Width = 202
        '
        'gFNNetProfitRcv
        '
        Me.gFNNetProfitRcv.Caption = "FNNetProfitRcv"
        Me.gFNNetProfitRcv.Columns.Add(Me.FNNetProfitRcv)
        Me.gFNNetProfitRcv.Name = "gFNNetProfitRcv"
        Me.gFNNetProfitRcv.VisibleIndex = 1
        Me.gFNNetProfitRcv.Width = 265
        '
        'FNNetProfitRcv
        '
        Me.FNNetProfitRcv.Caption = "FNNetProfitRcv"
        Me.FNNetProfitRcv.DisplayFormat.FormatString = "{0:n2}"
        Me.FNNetProfitRcv.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNNetProfitRcv.FieldName = "FNNetProfitRcv"
        Me.FNNetProfitRcv.Name = "FNNetProfitRcv"
        Me.FNNetProfitRcv.OptionsColumn.AllowEdit = False
        Me.FNNetProfitRcv.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNNetProfitRcv.OptionsColumn.ReadOnly = True
        Me.FNNetProfitRcv.Visible = True
        Me.FNNetProfitRcv.Width = 265
        '
        'gFNOrderQuantity
        '
        Me.gFNOrderQuantity.AppearanceHeader.Options.UseTextOptions = True
        Me.gFNOrderQuantity.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gFNOrderQuantity.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.gFNOrderQuantity.Caption = "ยอดสั่งซื้อ"
        Me.gFNOrderQuantity.Columns.Add(Me.FNOrderQuantity)
        Me.gFNOrderQuantity.Name = "gFNOrderQuantity"
        Me.gFNOrderQuantity.RowCount = 2
        Me.gFNOrderQuantity.Visible = False
        Me.gFNOrderQuantity.VisibleIndex = -1
        Me.gFNOrderQuantity.Width = 100
        '
        'FNOrderQuantity
        '
        Me.FNOrderQuantity.AppearanceCell.Options.UseTextOptions = True
        Me.FNOrderQuantity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNOrderQuantity.Caption = "FNOrderQuantity"
        Me.FNOrderQuantity.DisplayFormat.FormatString = "{0:n0}"
        Me.FNOrderQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNOrderQuantity.FieldName = "FNOrderQuantity"
        Me.FNOrderQuantity.Name = "FNOrderQuantity"
        Me.FNOrderQuantity.OptionsColumn.AllowEdit = False
        Me.FNOrderQuantity.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNOrderQuantity.OptionsColumn.ReadOnly = True
        Me.FNOrderQuantity.Visible = True
        Me.FNOrderQuantity.Width = 100
        '
        'gFNExport
        '
        Me.gFNExport.AppearanceHeader.Options.UseTextOptions = True
        Me.gFNExport.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gFNExport.Caption = "ส่งออก"
        Me.gFNExport.Children.AddRange(New DevExpress.XtraGrid.Views.BandedGrid.GridBand() {Me.gridBand1, Me.gridBand2, Me.รวม})
        Me.gFNExport.Name = "gFNExport"
        Me.gFNExport.Visible = False
        Me.gFNExport.VisibleIndex = -1
        Me.gFNExport.Width = 100
        '
        'gridBand1
        '
        Me.gridBand1.AppearanceHeader.Options.UseTextOptions = True
        Me.gridBand1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gridBand1.Caption = "ตัว"
        Me.gridBand1.Columns.Add(Me.FNExportQuantityTo)
        Me.gridBand1.Name = "gridBand1"
        Me.gridBand1.Visible = False
        Me.gridBand1.VisibleIndex = -1
        Me.gridBand1.Width = 117
        '
        'FNExportQuantityTo
        '
        Me.FNExportQuantityTo.AppearanceCell.Options.UseTextOptions = True
        Me.FNExportQuantityTo.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNExportQuantityTo.Caption = "FNExportQuantityTo"
        Me.FNExportQuantityTo.DisplayFormat.FormatString = "{0:n0}"
        Me.FNExportQuantityTo.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNExportQuantityTo.FieldName = "FNExportQuantityTo"
        Me.FNExportQuantityTo.Name = "FNExportQuantityTo"
        Me.FNExportQuantityTo.OptionsColumn.AllowEdit = False
        Me.FNExportQuantityTo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNExportQuantityTo.OptionsColumn.ReadOnly = True
        Me.FNExportQuantityTo.Visible = True
        Me.FNExportQuantityTo.Width = 117
        '
        'gridBand2
        '
        Me.gridBand2.AppearanceHeader.Options.UseTextOptions = True
        Me.gridBand2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gridBand2.Caption = "เดือนอื่นๆ"
        Me.gridBand2.Columns.Add(Me.FNExportQuantityOtherMonth)
        Me.gridBand2.Name = "gridBand2"
        Me.gridBand2.Visible = False
        Me.gridBand2.VisibleIndex = -1
        Me.gridBand2.Width = 154
        '
        'FNExportQuantityOtherMonth
        '
        Me.FNExportQuantityOtherMonth.AppearanceCell.Options.UseTextOptions = True
        Me.FNExportQuantityOtherMonth.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNExportQuantityOtherMonth.Caption = "FNExportQuantityOtherMonth"
        Me.FNExportQuantityOtherMonth.DisplayFormat.FormatString = "{0:n0}"
        Me.FNExportQuantityOtherMonth.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNExportQuantityOtherMonth.FieldName = "FNExportQuantityOtherMonth"
        Me.FNExportQuantityOtherMonth.Name = "FNExportQuantityOtherMonth"
        Me.FNExportQuantityOtherMonth.OptionsColumn.AllowEdit = False
        Me.FNExportQuantityOtherMonth.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNExportQuantityOtherMonth.OptionsColumn.ReadOnly = True
        Me.FNExportQuantityOtherMonth.Visible = True
        Me.FNExportQuantityOtherMonth.Width = 154
        '
        'รวม
        '
        Me.รวม.AppearanceHeader.Options.UseTextOptions = True
        Me.รวม.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.รวม.Caption = "รวม"
        Me.รวม.Columns.Add(Me.FNTotalExport)
        Me.รวม.Name = "รวม"
        Me.รวม.Visible = False
        Me.รวม.VisibleIndex = -1
        Me.รวม.Width = 100
        '
        'FNTotalExport
        '
        Me.FNTotalExport.AppearanceCell.Options.UseTextOptions = True
        Me.FNTotalExport.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNTotalExport.Caption = "FNTotalExport"
        Me.FNTotalExport.DisplayFormat.FormatString = "{0:n0}"
        Me.FNTotalExport.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNTotalExport.FieldName = "FNTotalExport"
        Me.FNTotalExport.Name = "FNTotalExport"
        Me.FNTotalExport.OptionsColumn.AllowEdit = False
        Me.FNTotalExport.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNTotalExport.OptionsColumn.ReadOnly = True
        Me.FNTotalExport.Visible = True
        Me.FNTotalExport.Width = 100
        '
        'gdiff
        '
        Me.gdiff.AppearanceHeader.Options.UseTextOptions = True
        Me.gdiff.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gdiff.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.gdiff.Caption = "ผลต่าง"
        Me.gdiff.Columns.Add(Me.FNOrderQuantityBal)
        Me.gdiff.Name = "gdiff"
        Me.gdiff.RowCount = 2
        Me.gdiff.Visible = False
        Me.gdiff.VisibleIndex = -1
        Me.gdiff.Width = 100
        '
        'FNOrderQuantityBal
        '
        Me.FNOrderQuantityBal.AppearanceCell.Options.UseTextOptions = True
        Me.FNOrderQuantityBal.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNOrderQuantityBal.Caption = "FNOrderQuantityBal"
        Me.FNOrderQuantityBal.DisplayFormat.FormatString = "{0:n0}"
        Me.FNOrderQuantityBal.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNOrderQuantityBal.FieldName = "FNOrderQuantityBal"
        Me.FNOrderQuantityBal.Name = "FNOrderQuantityBal"
        Me.FNOrderQuantityBal.OptionsColumn.AllowEdit = False
        Me.FNOrderQuantityBal.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNOrderQuantityBal.OptionsColumn.ReadOnly = True
        Me.FNOrderQuantityBal.Visible = True
        Me.FNOrderQuantityBal.Width = 100
        '
        'gridBand4
        '
        Me.gridBand4.Caption = "gridBand4"
        Me.gridBand4.Columns.Add(Me.FNPrice)
        Me.gridBand4.Name = "gridBand4"
        Me.gridBand4.Visible = False
        Me.gridBand4.VisibleIndex = -1
        Me.gridBand4.Width = 75
        '
        'FNPrice
        '
        Me.FNPrice.Caption = "FNPrice"
        Me.FNPrice.FieldName = "FNPrice"
        Me.FNPrice.Name = "FNPrice"
        Me.FNPrice.OptionsColumn.AllowEdit = False
        Me.FNPrice.Visible = True
        '
        'gdiffper
        '
        Me.gdiffper.AppearanceHeader.Options.UseTextOptions = True
        Me.gdiffper.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gdiffper.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.gdiffper.Caption = "% สูญเสีย"
        Me.gdiffper.Columns.Add(Me.FNLostPer)
        Me.gdiffper.Name = "gdiffper"
        Me.gdiffper.RowCount = 2
        Me.gdiffper.Visible = False
        Me.gdiffper.VisibleIndex = -1
        Me.gdiffper.Width = 50
        '
        'FNLostPer
        '
        Me.FNLostPer.AppearanceCell.Options.UseTextOptions = True
        Me.FNLostPer.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNLostPer.Caption = "FNLostPer"
        Me.FNLostPer.DisplayFormat.FormatString = "{0:n2}"
        Me.FNLostPer.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNLostPer.FieldName = "FNLostPer"
        Me.FNLostPer.Name = "FNLostPer"
        Me.FNLostPer.OptionsColumn.AllowEdit = False
        Me.FNLostPer.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNLostPer.OptionsColumn.ReadOnly = True
        Me.FNLostPer.Visible = True
        Me.FNLostPer.Width = 50
        '
        'CFNStateRow
        '
        Me.CFNStateRow.Caption = "FNStateRow"
        Me.CFNStateRow.FieldName = "FNStateRow"
        Me.CFNStateRow.Name = "CFNStateRow"
        '
        'FTSelectAllOrderCost
        '
        Me.FTSelectAllOrderCost.Location = New System.Drawing.Point(28, 1)
        Me.FTSelectAllOrderCost.Margin = New System.Windows.Forms.Padding(4)
        Me.FTSelectAllOrderCost.Name = "FTSelectAllOrderCost"
        Me.FTSelectAllOrderCost.Properties.Caption = "Select All"
        Me.FTSelectAllOrderCost.Size = New System.Drawing.Size(230, 20)
        Me.FTSelectAllOrderCost.TabIndex = 1
        Me.FTSelectAllOrderCost.Tag = "2|"
        '
        'otpapppo
        '
        Me.otpapppo.Controls.Add(Me.ogApprovedMail)
        Me.otpapppo.Name = "otpapppo"
        Me.otpapppo.PageVisible = False
        Me.otpapppo.Size = New System.Drawing.Size(1679, 797)
        Me.otpapppo.Text = "Approve Purchase"
        '
        'otpappfactory
        '
        Me.otpappfactory.Controls.Add(Me.GroupControl1)
        Me.otpappfactory.Name = "otpappfactory"
        Me.otpappfactory.PageVisible = False
        Me.otpappfactory.Size = New System.Drawing.Size(1679, 774)
        Me.otpappfactory.Text = "Approve Factory"
        '
        'GroupControl1
        '
        Me.GroupControl1.AppearanceCaption.Options.UseTextOptions = True
        Me.GroupControl1.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GroupControl1.Controls.Add(Me.ogcdirector)
        Me.GroupControl1.Controls.Add(Me.ockdirectorselectall)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl1.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl1.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(1679, 774)
        Me.GroupControl1.TabIndex = 10
        Me.GroupControl1.Text = "Approved  Purchase"
        '
        'ogcdirector
        '
        Me.ogcdirector.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogcdirector.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcdirector.Location = New System.Drawing.Point(2, 22)
        Me.ogcdirector.MainView = Me.ogvdirector
        Me.ogcdirector.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcdirector.Name = "ogcdirector"
        Me.ogcdirector.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemTextEdit1, Me.RepositorySelect})
        Me.ogcdirector.Size = New System.Drawing.Size(1675, 750)
        Me.ogcdirector.TabIndex = 20
        Me.ogcdirector.TabStop = False
        Me.ogcdirector.Tag = "2|"
        Me.ogcdirector.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvdirector})
        '
        'ogvdirector
        '
        Me.ogvdirector.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.C2FNHSysCmpId, Me.FTSelect, Me.C2FTCmpCodeTo, Me.C2FTCmpCode, Me.C2FTCompName, Me.C2FTPORef, Me.C2FNHSysStyleId, Me.C2FTStyleCode, Me.C2FTOrderNo, Me.C2FNHSysCustId, Me.C2FTCustCode, Me.C2FTCustName, Me.C2FNHSysBuyId, Me.C2FTBuyCode, Me.C2FNHSysSeasonId, Me.C2FTSeasonCode, Me.C2FNHSysProdTypeId, Me.C2FTProdTypeCode, Me.C2FTProdTypeName, Me.C2FDShipDate, Me.C2FTCurCode, Me.C2FNQuantity, Me.C2FNAmt, Me.C2FNExtraQuantity, Me.C2FNAmntExtra, Me.CFNGarmentQtyTest, Me.FNAmntQtyTest, Me.C2FNTotalQuantity, Me.FNGrandAmnt, Me.C2FTStateEmb, Me.C2FTStatePrint, Me.C2FTStateHeat, Me.C2FTStateLaser, Me.C2FTStateWindows, Me.C2FTStateOrderApp, Me.C2FTAppBy, Me.C2FDAppDate, Me.C2FTAppTime, Me.FTStateSendDirectorApp, Me.FTStateSendDirectorBy, Me.FDStateSendDirectorDate, Me.FTStateSendDirectorTime, Me.FTStateDirectorApp, Me.FTStateDirectorAppBy, Me.FDStateDirectorAppDate, Me.FTStateDirectorAppTime, Me.FTStateDirectorReject, Me.FTStateDirectorRejectBy, Me.FDStateDirectorRejectDate, Me.FTStateDirectorRejectTime, Me.C33FTColorway, Me.C33FTNikePOLineItem, Me.C33FTSizeBreakDown, Me.FNCM, Me.FNGrandAmtCM})
        Me.ogvdirector.CustomizationFormBounds = New System.Drawing.Rectangle(758, 512, 216, 178)
        Me.ogvdirector.GridControl = Me.ogcdirector
        Me.ogvdirector.GroupCount = 1
        Me.ogvdirector.GroupSummary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FNQuantity", Me.C2FNQuantity, "{0:n0}"), New DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FNExtraQuantity", Me.C2FNExtraQuantity, "{0:n0}"), New DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FNTotalQuantity", Me.C2FNTotalQuantity, "{0:n0}")})
        Me.ogvdirector.Name = "ogvdirector"
        Me.ogvdirector.OptionsBehavior.AutoExpandAllGroups = True
        Me.ogvdirector.OptionsCustomization.AllowQuickHideColumns = False
        Me.ogvdirector.OptionsNavigation.EnterMoveNextColumn = True
        Me.ogvdirector.OptionsView.ColumnAutoWidth = False
        Me.ogvdirector.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.ogvdirector.OptionsView.ShowAutoFilterRow = True
        Me.ogvdirector.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
        Me.ogvdirector.OptionsView.ShowFooter = True
        Me.ogvdirector.OptionsView.ShowGroupPanel = False
        Me.ogvdirector.SortInfo.AddRange(New DevExpress.XtraGrid.Columns.GridColumnSortInfo() {New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.C2FTCmpCode, DevExpress.Data.ColumnSortOrder.Ascending)})
        Me.ogvdirector.Tag = "2|"
        '
        'C2FNHSysCmpId
        '
        Me.C2FNHSysCmpId.Caption = "FNHSysCmpId"
        Me.C2FNHSysCmpId.FieldName = "FNHSysCmpId"
        Me.C2FNHSysCmpId.Name = "C2FNHSysCmpId"
        Me.C2FNHSysCmpId.OptionsColumn.AllowEdit = False
        Me.C2FNHSysCmpId.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.C2FNHSysCmpId.OptionsColumn.ReadOnly = True
        '
        'FTSelect
        '
        Me.FTSelect.Caption = "  "
        Me.FTSelect.ColumnEdit = Me.RepositorySelect
        Me.FTSelect.FieldName = "FTSelect"
        Me.FTSelect.Name = "FTSelect"
        Me.FTSelect.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTSelect.Visible = True
        Me.FTSelect.VisibleIndex = 0
        Me.FTSelect.Width = 47
        '
        'RepositorySelect
        '
        Me.RepositorySelect.AutoHeight = False
        Me.RepositorySelect.Caption = "Check"
        Me.RepositorySelect.Name = "RepositorySelect"
        Me.RepositorySelect.ValueChecked = "1"
        Me.RepositorySelect.ValueUnchecked = "0"
        '
        'C2FTCmpCodeTo
        '
        Me.C2FTCmpCodeTo.Caption = "โรงงาน"
        Me.C2FTCmpCodeTo.FieldName = "FTCmpCodeTo"
        Me.C2FTCmpCodeTo.Name = "C2FTCmpCodeTo"
        Me.C2FTCmpCodeTo.OptionsColumn.AllowEdit = False
        Me.C2FTCmpCodeTo.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.C2FTCmpCodeTo.OptionsColumn.ReadOnly = True
        Me.C2FTCmpCodeTo.Visible = True
        Me.C2FTCmpCodeTo.VisibleIndex = 1
        '
        'C2FTCmpCode
        '
        Me.C2FTCmpCode.Caption = "FTCmpCode"
        Me.C2FTCmpCode.FieldName = "FTCmpCode"
        Me.C2FTCmpCode.GroupInterval = DevExpress.XtraGrid.ColumnGroupInterval.Value
        Me.C2FTCmpCode.Name = "C2FTCmpCode"
        Me.C2FTCmpCode.OptionsColumn.AllowEdit = False
        Me.C2FTCmpCode.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.C2FTCmpCode.OptionsColumn.ReadOnly = True
        Me.C2FTCmpCode.Visible = True
        Me.C2FTCmpCode.VisibleIndex = 2
        '
        'C2FTCompName
        '
        Me.C2FTCompName.Caption = "FTCompName"
        Me.C2FTCompName.FieldName = "FTCompName"
        Me.C2FTCompName.Name = "C2FTCompName"
        Me.C2FTCompName.OptionsColumn.AllowEdit = False
        Me.C2FTCompName.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.C2FTCompName.OptionsColumn.ReadOnly = True
        Me.C2FTCompName.Visible = True
        Me.C2FTCompName.VisibleIndex = 2
        Me.C2FTCompName.Width = 103
        '
        'C2FTPORef
        '
        Me.C2FTPORef.Caption = "FTPORef"
        Me.C2FTPORef.FieldName = "FTPORef"
        Me.C2FTPORef.Name = "C2FTPORef"
        Me.C2FTPORef.OptionsColumn.AllowEdit = False
        Me.C2FTPORef.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.C2FTPORef.OptionsColumn.ReadOnly = True
        Me.C2FTPORef.Visible = True
        Me.C2FTPORef.VisibleIndex = 3
        '
        'C2FNHSysStyleId
        '
        Me.C2FNHSysStyleId.Caption = "FNHSysStyleId"
        Me.C2FNHSysStyleId.FieldName = "FNHSysStyleId"
        Me.C2FNHSysStyleId.Name = "C2FNHSysStyleId"
        Me.C2FNHSysStyleId.OptionsColumn.AllowEdit = False
        Me.C2FNHSysStyleId.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[True]
        Me.C2FNHSysStyleId.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.C2FNHSysStyleId.OptionsColumn.ReadOnly = True
        '
        'C2FTStyleCode
        '
        Me.C2FTStyleCode.Caption = "FTStyleCode"
        Me.C2FTStyleCode.FieldName = "FTStyleCode"
        Me.C2FTStyleCode.Name = "C2FTStyleCode"
        Me.C2FTStyleCode.OptionsColumn.AllowEdit = False
        Me.C2FTStyleCode.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.C2FTStyleCode.OptionsColumn.ReadOnly = True
        Me.C2FTStyleCode.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        Me.C2FTStyleCode.Visible = True
        Me.C2FTStyleCode.VisibleIndex = 4
        '
        'C2FTOrderNo
        '
        Me.C2FTOrderNo.Caption = "FTOrderNo"
        Me.C2FTOrderNo.FieldName = "FTOrderNo"
        Me.C2FTOrderNo.Name = "C2FTOrderNo"
        Me.C2FTOrderNo.OptionsColumn.AllowEdit = False
        Me.C2FTOrderNo.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.C2FTOrderNo.OptionsColumn.ReadOnly = True
        Me.C2FTOrderNo.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        Me.C2FTOrderNo.Visible = True
        Me.C2FTOrderNo.VisibleIndex = 5
        '
        'C2FNHSysCustId
        '
        Me.C2FNHSysCustId.Caption = "FNHSysCustId"
        Me.C2FNHSysCustId.FieldName = "FNHSysCustId"
        Me.C2FNHSysCustId.Name = "C2FNHSysCustId"
        Me.C2FNHSysCustId.OptionsColumn.AllowEdit = False
        Me.C2FNHSysCustId.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.C2FNHSysCustId.OptionsColumn.ReadOnly = True
        '
        'C2FTCustCode
        '
        Me.C2FTCustCode.Caption = "FTCustCode"
        Me.C2FTCustCode.FieldName = "FTCustCode"
        Me.C2FTCustCode.Name = "C2FTCustCode"
        Me.C2FTCustCode.OptionsColumn.AllowEdit = False
        Me.C2FTCustCode.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.C2FTCustCode.OptionsColumn.ReadOnly = True
        '
        'C2FTCustName
        '
        Me.C2FTCustName.Caption = "FTCustName"
        Me.C2FTCustName.FieldName = "FTCustName"
        Me.C2FTCustName.Name = "C2FTCustName"
        Me.C2FTCustName.OptionsColumn.AllowEdit = False
        Me.C2FTCustName.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.C2FTCustName.OptionsColumn.ReadOnly = True
        Me.C2FTCustName.Visible = True
        Me.C2FTCustName.VisibleIndex = 6
        '
        'C2FNHSysBuyId
        '
        Me.C2FNHSysBuyId.Caption = "FNHSysBuyId"
        Me.C2FNHSysBuyId.FieldName = "FNHSysBuyId"
        Me.C2FNHSysBuyId.Name = "C2FNHSysBuyId"
        Me.C2FNHSysBuyId.OptionsColumn.AllowEdit = False
        Me.C2FNHSysBuyId.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.C2FNHSysBuyId.OptionsColumn.ReadOnly = True
        '
        'C2FTBuyCode
        '
        Me.C2FTBuyCode.Caption = "FTBuyCode"
        Me.C2FTBuyCode.FieldName = "FTBuyCode"
        Me.C2FTBuyCode.Name = "C2FTBuyCode"
        Me.C2FTBuyCode.OptionsColumn.AllowEdit = False
        Me.C2FTBuyCode.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.C2FTBuyCode.OptionsColumn.ReadOnly = True
        Me.C2FTBuyCode.Visible = True
        Me.C2FTBuyCode.VisibleIndex = 7
        '
        'C2FNHSysSeasonId
        '
        Me.C2FNHSysSeasonId.Caption = "FNHSysSeasonId"
        Me.C2FNHSysSeasonId.FieldName = "FNHSysSeasonId"
        Me.C2FNHSysSeasonId.Name = "C2FNHSysSeasonId"
        Me.C2FNHSysSeasonId.OptionsColumn.AllowEdit = False
        Me.C2FNHSysSeasonId.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.C2FNHSysSeasonId.OptionsColumn.ReadOnly = True
        '
        'C2FTSeasonCode
        '
        Me.C2FTSeasonCode.Caption = "FTSeasonCode"
        Me.C2FTSeasonCode.FieldName = "FTSeasonCode"
        Me.C2FTSeasonCode.Name = "C2FTSeasonCode"
        Me.C2FTSeasonCode.OptionsColumn.AllowEdit = False
        Me.C2FTSeasonCode.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.C2FTSeasonCode.OptionsColumn.ReadOnly = True
        Me.C2FTSeasonCode.Visible = True
        Me.C2FTSeasonCode.VisibleIndex = 8
        '
        'C2FNHSysProdTypeId
        '
        Me.C2FNHSysProdTypeId.Caption = "FNHSysProdTypeId"
        Me.C2FNHSysProdTypeId.FieldName = "FNHSysProdTypeId"
        Me.C2FNHSysProdTypeId.Name = "C2FNHSysProdTypeId"
        Me.C2FNHSysProdTypeId.OptionsColumn.AllowEdit = False
        Me.C2FNHSysProdTypeId.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.C2FNHSysProdTypeId.OptionsColumn.ReadOnly = True
        '
        'C2FTProdTypeCode
        '
        Me.C2FTProdTypeCode.Caption = "FTProdTypeCode"
        Me.C2FTProdTypeCode.FieldName = "FTProdTypeCode"
        Me.C2FTProdTypeCode.Name = "C2FTProdTypeCode"
        Me.C2FTProdTypeCode.OptionsColumn.AllowEdit = False
        Me.C2FTProdTypeCode.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.C2FTProdTypeCode.OptionsColumn.ReadOnly = True
        '
        'C2FTProdTypeName
        '
        Me.C2FTProdTypeName.Caption = "FTProdTypeName"
        Me.C2FTProdTypeName.FieldName = "FTProdTypeName"
        Me.C2FTProdTypeName.Name = "C2FTProdTypeName"
        Me.C2FTProdTypeName.OptionsColumn.AllowEdit = False
        Me.C2FTProdTypeName.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.C2FTProdTypeName.OptionsColumn.ReadOnly = True
        '
        'C2FDShipDate
        '
        Me.C2FDShipDate.Caption = "FDShipDate"
        Me.C2FDShipDate.FieldName = "FDShipDate"
        Me.C2FDShipDate.Name = "C2FDShipDate"
        Me.C2FDShipDate.OptionsColumn.AllowEdit = False
        Me.C2FDShipDate.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.C2FDShipDate.OptionsColumn.ReadOnly = True
        Me.C2FDShipDate.Visible = True
        Me.C2FDShipDate.VisibleIndex = 9
        '
        'C2FTCurCode
        '
        Me.C2FTCurCode.Caption = "สกุลเงิน"
        Me.C2FTCurCode.FieldName = "FTCurCode"
        Me.C2FTCurCode.Name = "C2FTCurCode"
        Me.C2FTCurCode.OptionsColumn.AllowEdit = False
        Me.C2FTCurCode.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.C2FTCurCode.OptionsColumn.ReadOnly = True
        Me.C2FTCurCode.Visible = True
        Me.C2FTCurCode.VisibleIndex = 10
        '
        'C2FNQuantity
        '
        Me.C2FNQuantity.Caption = "FNQuantity"
        Me.C2FNQuantity.ColumnEdit = Me.RepositoryItemTextEdit1
        Me.C2FNQuantity.DisplayFormat.FormatString = "{0:n0}"
        Me.C2FNQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.C2FNQuantity.FieldName = "FNQuantity"
        Me.C2FNQuantity.Name = "C2FNQuantity"
        Me.C2FNQuantity.OptionsColumn.AllowEdit = False
        Me.C2FNQuantity.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.C2FNQuantity.OptionsColumn.ReadOnly = True
        Me.C2FNQuantity.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        Me.C2FNQuantity.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FNQuantity", "{0:n0}")})
        '
        'RepositoryItemTextEdit1
        '
        Me.RepositoryItemTextEdit1.AutoHeight = False
        Me.RepositoryItemTextEdit1.DisplayFormat.FormatString = "N0"
        Me.RepositoryItemTextEdit1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.RepositoryItemTextEdit1.EditFormat.FormatString = "N0"
        Me.RepositoryItemTextEdit1.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.RepositoryItemTextEdit1.Name = "RepositoryItemTextEdit1"
        '
        'C2FNAmt
        '
        Me.C2FNAmt.Caption = "Amount"
        Me.C2FNAmt.DisplayFormat.FormatString = "{0:n2}"
        Me.C2FNAmt.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.C2FNAmt.FieldName = "FNAmt"
        Me.C2FNAmt.Name = "C2FNAmt"
        Me.C2FNAmt.OptionsColumn.AllowEdit = False
        Me.C2FNAmt.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.C2FNAmt.OptionsColumn.ReadOnly = True
        Me.C2FNAmt.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FNAmt", "{0:n2}")})
        '
        'C2FNExtraQuantity
        '
        Me.C2FNExtraQuantity.Caption = "FNExtraQuantity"
        Me.C2FNExtraQuantity.ColumnEdit = Me.RepositoryItemTextEdit1
        Me.C2FNExtraQuantity.DisplayFormat.FormatString = "{0:n0}"
        Me.C2FNExtraQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.C2FNExtraQuantity.FieldName = "FNExtraQuantity"
        Me.C2FNExtraQuantity.Name = "C2FNExtraQuantity"
        Me.C2FNExtraQuantity.OptionsColumn.AllowEdit = False
        Me.C2FNExtraQuantity.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.C2FNExtraQuantity.OptionsColumn.ReadOnly = True
        Me.C2FNExtraQuantity.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        Me.C2FNExtraQuantity.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FNExtraQuantity", "{0:n0}")})
        '
        'C2FNAmntExtra
        '
        Me.C2FNAmntExtra.Caption = "Extra Amount"
        Me.C2FNAmntExtra.DisplayFormat.FormatString = "{0:n2}"
        Me.C2FNAmntExtra.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.C2FNAmntExtra.FieldName = "FNAmntExtra"
        Me.C2FNAmntExtra.Name = "C2FNAmntExtra"
        Me.C2FNAmntExtra.OptionsColumn.AllowEdit = False
        Me.C2FNAmntExtra.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.C2FNAmntExtra.OptionsColumn.ReadOnly = True
        Me.C2FNAmntExtra.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FNAmntExtra", "{0:n2}")})
        '
        'CFNGarmentQtyTest
        '
        Me.CFNGarmentQtyTest.Caption = "Garment Qty Test"
        Me.CFNGarmentQtyTest.DisplayFormat.FormatString = "{0:n0}"
        Me.CFNGarmentQtyTest.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CFNGarmentQtyTest.FieldName = "FNGarmentQtyTest"
        Me.CFNGarmentQtyTest.Name = "CFNGarmentQtyTest"
        Me.CFNGarmentQtyTest.OptionsColumn.AllowEdit = False
        Me.CFNGarmentQtyTest.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.CFNGarmentQtyTest.OptionsColumn.ReadOnly = True
        Me.CFNGarmentQtyTest.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FNGarmentQtyTest", "{0:n0}")})
        '
        'FNAmntQtyTest
        '
        Me.FNAmntQtyTest.Caption = "Test Amount"
        Me.FNAmntQtyTest.DisplayFormat.FormatString = "{0:n2}"
        Me.FNAmntQtyTest.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNAmntQtyTest.FieldName = "FNAmntQtyTest"
        Me.FNAmntQtyTest.Name = "FNAmntQtyTest"
        Me.FNAmntQtyTest.OptionsColumn.AllowEdit = False
        Me.FNAmntQtyTest.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.FNAmntQtyTest.OptionsColumn.ReadOnly = True
        Me.FNAmntQtyTest.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FNAmntQtyTest", "{0:n2}")})
        '
        'C2FNTotalQuantity
        '
        Me.C2FNTotalQuantity.Caption = "FNTotalQuantity"
        Me.C2FNTotalQuantity.ColumnEdit = Me.RepositoryItemTextEdit1
        Me.C2FNTotalQuantity.DisplayFormat.FormatString = "{0:n0}"
        Me.C2FNTotalQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.C2FNTotalQuantity.FieldName = "FNTotalQuantity"
        Me.C2FNTotalQuantity.Name = "C2FNTotalQuantity"
        Me.C2FNTotalQuantity.OptionsColumn.AllowEdit = False
        Me.C2FNTotalQuantity.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.C2FNTotalQuantity.OptionsColumn.ReadOnly = True
        Me.C2FNTotalQuantity.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FNTotalQuantity", "{0:n0}")})
        Me.C2FNTotalQuantity.Visible = True
        Me.C2FNTotalQuantity.VisibleIndex = 11
        '
        'FNGrandAmnt
        '
        Me.FNGrandAmnt.Caption = "FNGrandAmnt"
        Me.FNGrandAmnt.DisplayFormat.FormatString = "{0:n2}"
        Me.FNGrandAmnt.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNGrandAmnt.FieldName = "FNGrandAmnt"
        Me.FNGrandAmnt.Name = "FNGrandAmnt"
        Me.FNGrandAmnt.OptionsColumn.AllowEdit = False
        Me.FNGrandAmnt.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.FNGrandAmnt.OptionsColumn.ReadOnly = True
        Me.FNGrandAmnt.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FNGrandAmnt", "{0:n2}")})
        Me.FNGrandAmnt.Visible = True
        Me.FNGrandAmnt.VisibleIndex = 12
        '
        'C2FTStateEmb
        '
        Me.C2FTStateEmb.Caption = "FTStateEmb"
        Me.C2FTStateEmb.ColumnEdit = Me.RepositorySelect
        Me.C2FTStateEmb.FieldName = "FTStateEmb"
        Me.C2FTStateEmb.Name = "C2FTStateEmb"
        Me.C2FTStateEmb.OptionsColumn.AllowEdit = False
        Me.C2FTStateEmb.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.C2FTStateEmb.OptionsColumn.ReadOnly = True
        '
        'C2FTStatePrint
        '
        Me.C2FTStatePrint.Caption = "FTStatePrint"
        Me.C2FTStatePrint.ColumnEdit = Me.RepositorySelect
        Me.C2FTStatePrint.FieldName = "FTStatePrint"
        Me.C2FTStatePrint.Name = "C2FTStatePrint"
        Me.C2FTStatePrint.OptionsColumn.AllowEdit = False
        Me.C2FTStatePrint.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.C2FTStatePrint.OptionsColumn.ReadOnly = True
        '
        'C2FTStateHeat
        '
        Me.C2FTStateHeat.Caption = "FTStateHeat"
        Me.C2FTStateHeat.ColumnEdit = Me.RepositorySelect
        Me.C2FTStateHeat.FieldName = "FTStateHeat"
        Me.C2FTStateHeat.Name = "C2FTStateHeat"
        Me.C2FTStateHeat.OptionsColumn.AllowEdit = False
        Me.C2FTStateHeat.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.C2FTStateHeat.OptionsColumn.ReadOnly = True
        '
        'C2FTStateLaser
        '
        Me.C2FTStateLaser.Caption = "FTStateLaser"
        Me.C2FTStateLaser.ColumnEdit = Me.RepositorySelect
        Me.C2FTStateLaser.FieldName = "FTStateLaser"
        Me.C2FTStateLaser.Name = "C2FTStateLaser"
        Me.C2FTStateLaser.OptionsColumn.AllowEdit = False
        Me.C2FTStateLaser.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.C2FTStateLaser.OptionsColumn.ReadOnly = True
        '
        'C2FTStateWindows
        '
        Me.C2FTStateWindows.Caption = "FTStateWindows"
        Me.C2FTStateWindows.ColumnEdit = Me.RepositorySelect
        Me.C2FTStateWindows.FieldName = "FTStateWindows"
        Me.C2FTStateWindows.Name = "C2FTStateWindows"
        Me.C2FTStateWindows.OptionsColumn.AllowEdit = False
        Me.C2FTStateWindows.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.C2FTStateWindows.OptionsColumn.ReadOnly = True
        '
        'C2FTStateOrderApp
        '
        Me.C2FTStateOrderApp.Caption = "FTStateOrderApp"
        Me.C2FTStateOrderApp.ColumnEdit = Me.RepositorySelect
        Me.C2FTStateOrderApp.FieldName = "FTStateOrderApp"
        Me.C2FTStateOrderApp.Name = "C2FTStateOrderApp"
        Me.C2FTStateOrderApp.OptionsColumn.AllowEdit = False
        Me.C2FTStateOrderApp.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.C2FTStateOrderApp.OptionsColumn.ReadOnly = True
        Me.C2FTStateOrderApp.Width = 65
        '
        'C2FTAppBy
        '
        Me.C2FTAppBy.Caption = "FTAppBy"
        Me.C2FTAppBy.FieldName = "FTAppBy"
        Me.C2FTAppBy.Name = "C2FTAppBy"
        Me.C2FTAppBy.OptionsColumn.AllowEdit = False
        Me.C2FTAppBy.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.C2FTAppBy.OptionsColumn.ReadOnly = True
        Me.C2FTAppBy.Width = 86
        '
        'C2FDAppDate
        '
        Me.C2FDAppDate.Caption = "FDAppDate"
        Me.C2FDAppDate.FieldName = "FDAppDate"
        Me.C2FDAppDate.Name = "C2FDAppDate"
        Me.C2FDAppDate.OptionsColumn.AllowEdit = False
        Me.C2FDAppDate.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.C2FDAppDate.OptionsColumn.ReadOnly = True
        '
        'C2FTAppTime
        '
        Me.C2FTAppTime.Caption = "FTAppTime"
        Me.C2FTAppTime.FieldName = "FTAppTime"
        Me.C2FTAppTime.Name = "C2FTAppTime"
        Me.C2FTAppTime.OptionsColumn.AllowEdit = False
        Me.C2FTAppTime.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.C2FTAppTime.OptionsColumn.ReadOnly = True
        Me.C2FTAppTime.Width = 94
        '
        'FTStateSendDirectorApp
        '
        Me.FTStateSendDirectorApp.Caption = "Send App"
        Me.FTStateSendDirectorApp.ColumnEdit = Me.RepositorySelect
        Me.FTStateSendDirectorApp.FieldName = "FTStateSendDirectorApp"
        Me.FTStateSendDirectorApp.Name = "FTStateSendDirectorApp"
        Me.FTStateSendDirectorApp.OptionsColumn.AllowEdit = False
        Me.FTStateSendDirectorApp.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.FTStateSendDirectorApp.OptionsColumn.ReadOnly = True
        '
        'FTStateSendDirectorBy
        '
        Me.FTStateSendDirectorBy.Caption = "Send App By"
        Me.FTStateSendDirectorBy.FieldName = "FTStateSendDirectorBy"
        Me.FTStateSendDirectorBy.Name = "FTStateSendDirectorBy"
        Me.FTStateSendDirectorBy.OptionsColumn.AllowEdit = False
        Me.FTStateSendDirectorBy.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.FTStateSendDirectorBy.OptionsColumn.ReadOnly = True
        Me.FTStateSendDirectorBy.Visible = True
        Me.FTStateSendDirectorBy.VisibleIndex = 18
        '
        'FDStateSendDirectorDate
        '
        Me.FDStateSendDirectorDate.Caption = "Send App Date"
        Me.FDStateSendDirectorDate.FieldName = "FDStateSendDirectorDate"
        Me.FDStateSendDirectorDate.Name = "FDStateSendDirectorDate"
        Me.FDStateSendDirectorDate.OptionsColumn.AllowEdit = False
        Me.FDStateSendDirectorDate.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.FDStateSendDirectorDate.OptionsColumn.ReadOnly = True
        Me.FDStateSendDirectorDate.Visible = True
        Me.FDStateSendDirectorDate.VisibleIndex = 19
        '
        'FTStateSendDirectorTime
        '
        Me.FTStateSendDirectorTime.Caption = "Send App Time"
        Me.FTStateSendDirectorTime.FieldName = "FTStateSendDirectorTime"
        Me.FTStateSendDirectorTime.Name = "FTStateSendDirectorTime"
        Me.FTStateSendDirectorTime.OptionsColumn.AllowEdit = False
        Me.FTStateSendDirectorTime.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.FTStateSendDirectorTime.OptionsColumn.ReadOnly = True
        '
        'FTStateDirectorApp
        '
        Me.FTStateDirectorApp.Caption = "Approved"
        Me.FTStateDirectorApp.ColumnEdit = Me.RepositorySelect
        Me.FTStateDirectorApp.FieldName = "FTStateDirectorApp"
        Me.FTStateDirectorApp.Name = "FTStateDirectorApp"
        Me.FTStateDirectorApp.OptionsColumn.AllowEdit = False
        Me.FTStateDirectorApp.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.FTStateDirectorApp.OptionsColumn.ReadOnly = True
        '
        'FTStateDirectorAppBy
        '
        Me.FTStateDirectorAppBy.Caption = "Approved By"
        Me.FTStateDirectorAppBy.FieldName = "FTStateDirectorAppBy"
        Me.FTStateDirectorAppBy.Name = "FTStateDirectorAppBy"
        Me.FTStateDirectorAppBy.OptionsColumn.AllowEdit = False
        Me.FTStateDirectorAppBy.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.FTStateDirectorAppBy.OptionsColumn.ReadOnly = True
        '
        'FDStateDirectorAppDate
        '
        Me.FDStateDirectorAppDate.Caption = "Approved Date"
        Me.FDStateDirectorAppDate.FieldName = "FDStateDirectorAppDate"
        Me.FDStateDirectorAppDate.Name = "FDStateDirectorAppDate"
        Me.FDStateDirectorAppDate.OptionsColumn.AllowEdit = False
        Me.FDStateDirectorAppDate.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.FDStateDirectorAppDate.OptionsColumn.ReadOnly = True
        '
        'FTStateDirectorAppTime
        '
        Me.FTStateDirectorAppTime.Caption = "Approved Time"
        Me.FTStateDirectorAppTime.Name = "FTStateDirectorAppTime"
        Me.FTStateDirectorAppTime.OptionsColumn.AllowEdit = False
        Me.FTStateDirectorAppTime.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.FTStateDirectorAppTime.OptionsColumn.ReadOnly = True
        '
        'FTStateDirectorReject
        '
        Me.FTStateDirectorReject.Caption = "Rejected"
        Me.FTStateDirectorReject.ColumnEdit = Me.RepositorySelect
        Me.FTStateDirectorReject.FieldName = "FTStateDirectorReject"
        Me.FTStateDirectorReject.Name = "FTStateDirectorReject"
        Me.FTStateDirectorReject.OptionsColumn.AllowEdit = False
        Me.FTStateDirectorReject.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.FTStateDirectorReject.OptionsColumn.ReadOnly = True
        '
        'FTStateDirectorRejectBy
        '
        Me.FTStateDirectorRejectBy.Caption = "Rejected By"
        Me.FTStateDirectorRejectBy.FieldName = "FTStateDirectorRejectBy"
        Me.FTStateDirectorRejectBy.Name = "FTStateDirectorRejectBy"
        Me.FTStateDirectorRejectBy.OptionsColumn.AllowEdit = False
        Me.FTStateDirectorRejectBy.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.FTStateDirectorRejectBy.OptionsColumn.ReadOnly = True
        '
        'FDStateDirectorRejectDate
        '
        Me.FDStateDirectorRejectDate.Caption = "Rejected Date"
        Me.FDStateDirectorRejectDate.Name = "FDStateDirectorRejectDate"
        Me.FDStateDirectorRejectDate.OptionsColumn.AllowEdit = False
        Me.FDStateDirectorRejectDate.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.FDStateDirectorRejectDate.OptionsColumn.ReadOnly = True
        '
        'FTStateDirectorRejectTime
        '
        Me.FTStateDirectorRejectTime.Caption = "Rejected Time"
        Me.FTStateDirectorRejectTime.FieldName = "FTStateDirectorRejectTime"
        Me.FTStateDirectorRejectTime.Name = "FTStateDirectorRejectTime"
        Me.FTStateDirectorRejectTime.OptionsColumn.AllowEdit = False
        Me.FTStateDirectorRejectTime.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.FTStateDirectorRejectTime.OptionsColumn.ReadOnly = True
        '
        'C33FTColorway
        '
        Me.C33FTColorway.Caption = "Colorway"
        Me.C33FTColorway.FieldName = "FTColorway"
        Me.C33FTColorway.Name = "C33FTColorway"
        Me.C33FTColorway.OptionsColumn.AllowEdit = False
        Me.C33FTColorway.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.C33FTColorway.OptionsColumn.ReadOnly = True
        Me.C33FTColorway.Visible = True
        Me.C33FTColorway.VisibleIndex = 13
        '
        'C33FTNikePOLineItem
        '
        Me.C33FTNikePOLineItem.Caption = "PO Line"
        Me.C33FTNikePOLineItem.FieldName = "FTNikePOLineItem"
        Me.C33FTNikePOLineItem.Name = "C33FTNikePOLineItem"
        Me.C33FTNikePOLineItem.OptionsColumn.AllowEdit = False
        Me.C33FTNikePOLineItem.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.C33FTNikePOLineItem.OptionsColumn.ReadOnly = True
        Me.C33FTNikePOLineItem.Visible = True
        Me.C33FTNikePOLineItem.VisibleIndex = 14
        '
        'C33FTSizeBreakDown
        '
        Me.C33FTSizeBreakDown.Caption = "SizeBreakDown"
        Me.C33FTSizeBreakDown.FieldName = "FTSizeBreakDown"
        Me.C33FTSizeBreakDown.Name = "C33FTSizeBreakDown"
        Me.C33FTSizeBreakDown.OptionsColumn.AllowEdit = False
        Me.C33FTSizeBreakDown.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.C33FTSizeBreakDown.OptionsColumn.ReadOnly = True
        Me.C33FTSizeBreakDown.Visible = True
        Me.C33FTSizeBreakDown.VisibleIndex = 15
        '
        'FNCM
        '
        Me.FNCM.Caption = "CM Price"
        Me.FNCM.DisplayFormat.FormatString = "{0:n4}"
        Me.FNCM.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNCM.FieldName = "FNCM"
        Me.FNCM.Name = "FNCM"
        Me.FNCM.OptionsColumn.AllowEdit = False
        Me.FNCM.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNCM.OptionsColumn.ReadOnly = True
        Me.FNCM.Visible = True
        Me.FNCM.VisibleIndex = 16
        Me.FNCM.Width = 100
        '
        'FNGrandAmtCM
        '
        Me.FNGrandAmtCM.Caption = "Amount By CM"
        Me.FNGrandAmtCM.DisplayFormat.FormatString = "{0:n2}"
        Me.FNGrandAmtCM.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNGrandAmtCM.FieldName = "FNGrandAmtCM"
        Me.FNGrandAmtCM.Name = "FNGrandAmtCM"
        Me.FNGrandAmtCM.OptionsColumn.AllowEdit = False
        Me.FNGrandAmtCM.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNGrandAmtCM.OptionsColumn.ReadOnly = True
        Me.FNGrandAmtCM.Visible = True
        Me.FNGrandAmtCM.VisibleIndex = 17
        Me.FNGrandAmtCM.Width = 100
        '
        'ockdirectorselectall
        '
        Me.ockdirectorselectall.Location = New System.Drawing.Point(28, 1)
        Me.ockdirectorselectall.Margin = New System.Windows.Forms.Padding(4)
        Me.ockdirectorselectall.Name = "ockdirectorselectall"
        Me.ockdirectorselectall.Properties.Caption = "Select All"
        Me.ockdirectorselectall.Size = New System.Drawing.Size(230, 20)
        Me.ockdirectorselectall.TabIndex = 1
        '
        'otpappdocument
        '
        Me.otpappdocument.Controls.Add(Me.ogbdocument)
        Me.otpappdocument.Name = "otpappdocument"
        Me.otpappdocument.PageVisible = False
        Me.otpappdocument.Size = New System.Drawing.Size(1679, 774)
        Me.otpappdocument.Text = "Approve Document Control"
        '
        'ogbdocument
        '
        Me.ogbdocument.AppearanceCaption.Options.UseTextOptions = True
        Me.ogbdocument.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ogbdocument.Controls.Add(Me.ogcdocument)
        Me.ogbdocument.Controls.Add(Me.FTSelectAllDC)
        Me.ogbdocument.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogbdocument.Location = New System.Drawing.Point(0, 0)
        Me.ogbdocument.Margin = New System.Windows.Forms.Padding(4)
        Me.ogbdocument.Name = "ogbdocument"
        Me.ogbdocument.Size = New System.Drawing.Size(1679, 774)
        Me.ogbdocument.TabIndex = 13
        Me.ogbdocument.Text = "Approved  Documentation"
        '
        'ogcdocument
        '
        Me.ogcdocument.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogcdocument.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4)
        Me.ogcdocument.Location = New System.Drawing.Point(2, 22)
        Me.ogcdocument.MainView = Me.ogvdocument
        Me.ogcdocument.Margin = New System.Windows.Forms.Padding(4)
        Me.ogcdocument.MenuManager = Me.MainRibbonControl
        Me.ogcdocument.Name = "ogcdocument"
        Me.ogcdocument.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryFTSelect, Me.RepositoryFTSandApprove})
        Me.ogcdocument.Size = New System.Drawing.Size(1675, 750)
        Me.ogcdocument.TabIndex = 2
        Me.ogcdocument.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvdocument})
        '
        'ogvdocument
        '
        Me.ogvdocument.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.cFTSelect, Me.cFTDocumentNo, Me.cFDDocumentDate, Me.cFTDocumentBy, Me.cFTCmpCode, Me.cFTCmpName, Me.cFTDocumentTitle, Me.cFTNote, Me.cFTBenefit, Me.cFNOperActivity, Me.cFTOperActivityName, Me.cFTDocName, Me.cFTDocRefCode, Me.cFTDocTypeName, Me.cFTFileTypeName, Me.cFBDocument, Me.cFNDocType, Me.cFNHSysCmpId})
        Me.ogvdocument.GridControl = Me.ogcdocument
        Me.ogvdocument.Name = "ogvdocument"
        Me.ogvdocument.OptionsEditForm.ShowUpdateCancelPanel = DevExpress.Utils.DefaultBoolean.[True]
        Me.ogvdocument.OptionsView.ColumnAutoWidth = False
        Me.ogvdocument.OptionsView.ShowGroupPanel = False
        '
        'cFTSelect
        '
        Me.cFTSelect.Caption = "FTSelect"
        Me.cFTSelect.ColumnEdit = Me.RepositoryFTSelect
        Me.cFTSelect.FieldName = "FTSelect"
        Me.cFTSelect.Name = "cFTSelect"
        Me.cFTSelect.Visible = True
        Me.cFTSelect.VisibleIndex = 0
        Me.cFTSelect.Width = 36
        '
        'RepositoryFTSelect
        '
        Me.RepositoryFTSelect.AutoHeight = False
        Me.RepositoryFTSelect.Caption = "Check"
        Me.RepositoryFTSelect.Name = "RepositoryFTSelect"
        Me.RepositoryFTSelect.ValueChecked = "1"
        Me.RepositoryFTSelect.ValueUnchecked = "0"
        '
        'cFTDocumentNo
        '
        Me.cFTDocumentNo.Caption = "FTDocumentNo"
        Me.cFTDocumentNo.FieldName = "FTDocumentNo"
        Me.cFTDocumentNo.Name = "cFTDocumentNo"
        Me.cFTDocumentNo.OptionsColumn.AllowEdit = False
        Me.cFTDocumentNo.Visible = True
        Me.cFTDocumentNo.VisibleIndex = 3
        Me.cFTDocumentNo.Width = 149
        '
        'cFDDocumentDate
        '
        Me.cFDDocumentDate.Caption = "FDDocumentDate"
        Me.cFDDocumentDate.FieldName = "FDDocumentDate"
        Me.cFDDocumentDate.Name = "cFDDocumentDate"
        Me.cFDDocumentDate.OptionsColumn.AllowEdit = False
        Me.cFDDocumentDate.Visible = True
        Me.cFDDocumentDate.VisibleIndex = 4
        Me.cFDDocumentDate.Width = 107
        '
        'cFTDocumentBy
        '
        Me.cFTDocumentBy.Caption = "FTDocumentBy"
        Me.cFTDocumentBy.FieldName = "FTDocumentBy"
        Me.cFTDocumentBy.Name = "cFTDocumentBy"
        Me.cFTDocumentBy.OptionsColumn.AllowEdit = False
        Me.cFTDocumentBy.Visible = True
        Me.cFTDocumentBy.VisibleIndex = 5
        Me.cFTDocumentBy.Width = 104
        '
        'cFTCmpCode
        '
        Me.cFTCmpCode.Caption = "FTCmpCode"
        Me.cFTCmpCode.FieldName = "FTCmpCode"
        Me.cFTCmpCode.Name = "cFTCmpCode"
        Me.cFTCmpCode.OptionsColumn.AllowEdit = False
        Me.cFTCmpCode.Visible = True
        Me.cFTCmpCode.VisibleIndex = 1
        Me.cFTCmpCode.Width = 88
        '
        'cFTCmpName
        '
        Me.cFTCmpName.Caption = "FTCmpName"
        Me.cFTCmpName.FieldName = "FTCmpName"
        Me.cFTCmpName.Name = "cFTCmpName"
        Me.cFTCmpName.OptionsColumn.AllowEdit = False
        Me.cFTCmpName.Visible = True
        Me.cFTCmpName.VisibleIndex = 2
        Me.cFTCmpName.Width = 149
        '
        'cFTDocumentTitle
        '
        Me.cFTDocumentTitle.Caption = "FTDocumentTitle"
        Me.cFTDocumentTitle.FieldName = "FTDocumentTitle"
        Me.cFTDocumentTitle.Name = "cFTDocumentTitle"
        Me.cFTDocumentTitle.OptionsColumn.AllowEdit = False
        Me.cFTDocumentTitle.Visible = True
        Me.cFTDocumentTitle.VisibleIndex = 7
        Me.cFTDocumentTitle.Width = 133
        '
        'cFTNote
        '
        Me.cFTNote.Caption = "FTNote"
        Me.cFTNote.FieldName = "FTNote"
        Me.cFTNote.Name = "cFTNote"
        Me.cFTNote.OptionsColumn.AllowEdit = False
        Me.cFTNote.Visible = True
        Me.cFTNote.VisibleIndex = 14
        Me.cFTNote.Width = 181
        '
        'cFTBenefit
        '
        Me.cFTBenefit.Caption = "FTBenefit"
        Me.cFTBenefit.FieldName = "FTBenefit"
        Me.cFTBenefit.Name = "cFTBenefit"
        Me.cFTBenefit.OptionsColumn.AllowEdit = False
        Me.cFTBenefit.Visible = True
        Me.cFTBenefit.VisibleIndex = 10
        Me.cFTBenefit.Width = 261
        '
        'cFNOperActivity
        '
        Me.cFNOperActivity.Caption = "FNOperActivity"
        Me.cFNOperActivity.FieldName = "FNOperActivity"
        Me.cFNOperActivity.Name = "cFNOperActivity"
        Me.cFNOperActivity.OptionsColumn.AllowEdit = False
        Me.cFNOperActivity.Visible = True
        Me.cFNOperActivity.VisibleIndex = 6
        Me.cFNOperActivity.Width = 128
        '
        'cFTOperActivityName
        '
        Me.cFTOperActivityName.Caption = "FTOperActivityName"
        Me.cFTOperActivityName.FieldName = "FTOperActivityName"
        Me.cFTOperActivityName.Name = "cFTOperActivityName"
        Me.cFTOperActivityName.OptionsColumn.AllowEdit = False
        Me.cFTOperActivityName.Visible = True
        Me.cFTOperActivityName.VisibleIndex = 11
        Me.cFTOperActivityName.Width = 172
        '
        'cFTDocName
        '
        Me.cFTDocName.Caption = "FTDocName"
        Me.cFTDocName.FieldName = "FTDocName"
        Me.cFTDocName.Name = "cFTDocName"
        Me.cFTDocName.OptionsColumn.AllowEdit = False
        Me.cFTDocName.Visible = True
        Me.cFTDocName.VisibleIndex = 8
        Me.cFTDocName.Width = 194
        '
        'cFTDocRefCode
        '
        Me.cFTDocRefCode.Caption = "FTDocRefCode"
        Me.cFTDocRefCode.FieldName = "FTDocRefCode"
        Me.cFTDocRefCode.Name = "cFTDocRefCode"
        Me.cFTDocRefCode.OptionsColumn.AllowEdit = False
        Me.cFTDocRefCode.Visible = True
        Me.cFTDocRefCode.VisibleIndex = 9
        Me.cFTDocRefCode.Width = 139
        '
        'cFTDocTypeName
        '
        Me.cFTDocTypeName.Caption = "FTDocTypeName"
        Me.cFTDocTypeName.FieldName = "FTDocTypeName"
        Me.cFTDocTypeName.Name = "cFTDocTypeName"
        Me.cFTDocTypeName.OptionsColumn.AllowEdit = False
        Me.cFTDocTypeName.Visible = True
        Me.cFTDocTypeName.VisibleIndex = 12
        Me.cFTDocTypeName.Width = 100
        '
        'cFTFileTypeName
        '
        Me.cFTFileTypeName.Caption = "FTFileTypeName"
        Me.cFTFileTypeName.FieldName = "FTFileTypeName"
        Me.cFTFileTypeName.Name = "cFTFileTypeName"
        Me.cFTFileTypeName.OptionsColumn.AllowEdit = False
        Me.cFTFileTypeName.Visible = True
        Me.cFTFileTypeName.VisibleIndex = 13
        Me.cFTFileTypeName.Width = 65
        '
        'cFBDocument
        '
        Me.cFBDocument.Caption = "GridColumn1"
        Me.cFBDocument.FieldName = "FBDocument"
        Me.cFBDocument.Name = "cFBDocument"
        Me.cFBDocument.OptionsColumn.AllowEdit = False
        '
        'cFNDocType
        '
        Me.cFNDocType.Caption = "FNDocType"
        Me.cFNDocType.FieldName = "FNDocType"
        Me.cFNDocType.Name = "cFNDocType"
        Me.cFNDocType.OptionsColumn.AllowEdit = False
        '
        'cFNHSysCmpId
        '
        Me.cFNHSysCmpId.Caption = "FNHSysCmpId"
        Me.cFNHSysCmpId.FieldName = "FNHSysCmpId"
        Me.cFNHSysCmpId.Name = "cFNHSysCmpId"
        '
        'RepositoryFTSandApprove
        '
        Me.RepositoryFTSandApprove.AutoHeight = False
        Me.RepositoryFTSandApprove.Caption = "Check"
        Me.RepositoryFTSandApprove.Name = "RepositoryFTSandApprove"
        Me.RepositoryFTSandApprove.ValueChecked = "1"
        Me.RepositoryFTSandApprove.ValueUnchecked = "0"
        '
        'FTSelectAllDC
        '
        Me.FTSelectAllDC.Location = New System.Drawing.Point(28, 1)
        Me.FTSelectAllDC.Margin = New System.Windows.Forms.Padding(4)
        Me.FTSelectAllDC.Name = "FTSelectAllDC"
        Me.FTSelectAllDC.Properties.Caption = "Select All"
        Me.FTSelectAllDC.Size = New System.Drawing.Size(230, 20)
        Me.FTSelectAllDC.TabIndex = 1
        Me.FTSelectAllDC.Tag = "2|"
        '
        'otpleaveapprove
        '
        Me.otpleaveapprove.Controls.Add(Me.ogbapproveleave)
        Me.otpleaveapprove.Name = "otpleaveapprove"
        Me.otpleaveapprove.PageVisible = False
        Me.otpleaveapprove.Size = New System.Drawing.Size(1679, 774)
        Me.otpleaveapprove.Text = "Approve Leave"
        '
        'ogbapproveleave
        '
        Me.ogbapproveleave.AppearanceCaption.Options.UseTextOptions = True
        Me.ogbapproveleave.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ogbapproveleave.Controls.Add(Me.ogcleaveapp)
        Me.ogbapproveleave.Controls.Add(Me.FTSelectAllleave)
        Me.ogbapproveleave.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogbapproveleave.Location = New System.Drawing.Point(0, 0)
        Me.ogbapproveleave.Margin = New System.Windows.Forms.Padding(4)
        Me.ogbapproveleave.Name = "ogbapproveleave"
        Me.ogbapproveleave.Size = New System.Drawing.Size(1679, 774)
        Me.ogbapproveleave.TabIndex = 13
        Me.ogbapproveleave.Text = "Approved  Leave"
        '
        'ogcleaveapp
        '
        Me.ogcleaveapp.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogcleaveapp.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcleaveapp.Location = New System.Drawing.Point(2, 22)
        Me.ogcleaveapp.MainView = Me.ogvapproveleave
        Me.ogcleaveapp.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcleaveapp.Name = "ogcleaveapp"
        Me.ogcleaveapp.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCheckEdit2, Me.RepositoryFTApproveState, Me.RepColFTLeavePay, Me.RepColFTHoliday, Me.RepFTStaCalSSO, Me.RepFTApproveState, Me.RepFTLeavePay, Me.RepositoryItemFTSelect, Me.RepositoryItemCFTMngApproveState})
        Me.ogcleaveapp.Size = New System.Drawing.Size(1675, 750)
        Me.ogcleaveapp.TabIndex = 4
        Me.ogcleaveapp.TabStop = False
        Me.ogcleaveapp.Tag = "2|"
        Me.ogcleaveapp.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvapproveleave, Me.GridView1})
        '
        'ogvapproveleave
        '
        Me.ogvapproveleave.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.FNHSysEmpID, Me.GridColumn1, Me.cFTEmpName, Me.ColFTStartDate, Me.ColFTEndDate, Me.cFNSumTotalLeaveDay, Me.FTLeaveType, Me.FTLeaveTypeName, Me.FTStaLeaveDayName, Me.ColFTHoliday, Me.FTLeavePay, Me.FTLeaveStartTime, Me.FTLeaveEndTime, Me.FNLeaveTotalTime, Me.FNLeaveTotalDay, Me.FTStaCalSSO, Me.FTStaLeaveDay, Me.FTLeaveNote, Me.ColFTStateMedicalCertificate, Me.ColFTMedicalCertificateName, Me.CFTInsUser, Me.CFTInsDate, Me.CFTInsTime, Me.CFTStateDeductVacation, Me.cFTStateType, Me.cFNLeaveTotalTimeMin, Me.FTMngApproveState, Me.FTMngApproveBy, Me.FTMngApproveTime, Me.FDMngApproveDate, Me.FTApproveState, Me.SickLeave, Me.BusinessLeave, Me.VacationLeave, Me.FTEmpTypeName, Me.FTSectName, Me.FTUnitSectName})
        Me.ogvapproveleave.GridControl = Me.ogcleaveapp
        Me.ogvapproveleave.Name = "ogvapproveleave"
        Me.ogvapproveleave.OptionsCustomization.AllowGroup = False
        Me.ogvapproveleave.OptionsCustomization.AllowQuickHideColumns = False
        Me.ogvapproveleave.OptionsView.ColumnAutoWidth = False
        Me.ogvapproveleave.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.ogvapproveleave.OptionsView.ShowGroupPanel = False
        Me.ogvapproveleave.Tag = "2|"
        '
        'FNHSysEmpID
        '
        Me.FNHSysEmpID.Caption = "FNHSysEmpID"
        Me.FNHSysEmpID.FieldName = "FNHSysEmpID"
        Me.FNHSysEmpID.Name = "FNHSysEmpID"
        Me.FNHSysEmpID.OptionsColumn.AllowEdit = False
        Me.FNHSysEmpID.Width = 104
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "FTSelect"
        Me.GridColumn1.ColumnEdit = Me.RepositoryItemFTSelect
        Me.GridColumn1.FieldName = "FTSelect"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        Me.GridColumn1.Width = 40
        '
        'RepositoryItemFTSelect
        '
        Me.RepositoryItemFTSelect.AutoHeight = False
        Me.RepositoryItemFTSelect.Name = "RepositoryItemFTSelect"
        Me.RepositoryItemFTSelect.ValueChecked = "1"
        Me.RepositoryItemFTSelect.ValueUnchecked = "0"
        '
        'cFTEmpName
        '
        Me.cFTEmpName.Caption = "FTEmpName"
        Me.cFTEmpName.FieldName = "FTEmpName"
        Me.cFTEmpName.Name = "cFTEmpName"
        Me.cFTEmpName.OptionsColumn.AllowEdit = False
        Me.cFTEmpName.Visible = True
        Me.cFTEmpName.VisibleIndex = 1
        Me.cFTEmpName.Width = 227
        '
        'ColFTStartDate
        '
        Me.ColFTStartDate.AppearanceCell.Options.UseTextOptions = True
        Me.ColFTStartDate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ColFTStartDate.AppearanceHeader.Options.UseTextOptions = True
        Me.ColFTStartDate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ColFTStartDate.Caption = "FTStartDate"
        Me.ColFTStartDate.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.ColFTStartDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.ColFTStartDate.FieldName = "FTStartDate"
        Me.ColFTStartDate.Name = "ColFTStartDate"
        Me.ColFTStartDate.OptionsColumn.AllowEdit = False
        Me.ColFTStartDate.OptionsColumn.AllowMove = False
        Me.ColFTStartDate.OptionsColumn.ReadOnly = True
        Me.ColFTStartDate.Visible = True
        Me.ColFTStartDate.VisibleIndex = 2
        Me.ColFTStartDate.Width = 107
        '
        'ColFTEndDate
        '
        Me.ColFTEndDate.AppearanceCell.Options.UseTextOptions = True
        Me.ColFTEndDate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ColFTEndDate.AppearanceHeader.Options.UseTextOptions = True
        Me.ColFTEndDate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ColFTEndDate.Caption = "FTEndDate"
        Me.ColFTEndDate.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.ColFTEndDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.ColFTEndDate.FieldName = "FTEndDate"
        Me.ColFTEndDate.Name = "ColFTEndDate"
        Me.ColFTEndDate.OptionsColumn.AllowEdit = False
        Me.ColFTEndDate.OptionsColumn.AllowMove = False
        Me.ColFTEndDate.OptionsColumn.ReadOnly = True
        Me.ColFTEndDate.Visible = True
        Me.ColFTEndDate.VisibleIndex = 3
        Me.ColFTEndDate.Width = 114
        '
        'cFNSumTotalLeaveDay
        '
        Me.cFNSumTotalLeaveDay.Caption = "FNSumTotalLeaveDay"
        Me.cFNSumTotalLeaveDay.FieldName = "FNSumTotalLeaveDay"
        Me.cFNSumTotalLeaveDay.Name = "cFNSumTotalLeaveDay"
        Me.cFNSumTotalLeaveDay.OptionsColumn.AllowEdit = False
        Me.cFNSumTotalLeaveDay.Visible = True
        Me.cFNSumTotalLeaveDay.VisibleIndex = 5
        Me.cFNSumTotalLeaveDay.Width = 152
        '
        'FTLeaveType
        '
        Me.FTLeaveType.Caption = "FTLeaveType"
        Me.FTLeaveType.FieldName = "FTLeaveType"
        Me.FTLeaveType.Name = "FTLeaveType"
        Me.FTLeaveType.OptionsColumn.AllowEdit = False
        Me.FTLeaveType.OptionsColumn.AllowMove = False
        Me.FTLeaveType.OptionsColumn.ReadOnly = True
        '
        'FTLeaveTypeName
        '
        Me.FTLeaveTypeName.AppearanceHeader.Options.UseTextOptions = True
        Me.FTLeaveTypeName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTLeaveTypeName.Caption = "FTLeaveTypeName"
        Me.FTLeaveTypeName.FieldName = "FTLeaveTypeName"
        Me.FTLeaveTypeName.Name = "FTLeaveTypeName"
        Me.FTLeaveTypeName.OptionsColumn.AllowEdit = False
        Me.FTLeaveTypeName.OptionsColumn.AllowMove = False
        Me.FTLeaveTypeName.OptionsColumn.ReadOnly = True
        Me.FTLeaveTypeName.Visible = True
        Me.FTLeaveTypeName.VisibleIndex = 4
        Me.FTLeaveTypeName.Width = 151
        '
        'FTStaLeaveDayName
        '
        Me.FTStaLeaveDayName.AppearanceHeader.Options.UseTextOptions = True
        Me.FTStaLeaveDayName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTStaLeaveDayName.Caption = "FTStaLeaveDayName"
        Me.FTStaLeaveDayName.FieldName = "FTStaLeaveDayName"
        Me.FTStaLeaveDayName.Name = "FTStaLeaveDayName"
        Me.FTStaLeaveDayName.OptionsColumn.AllowEdit = False
        Me.FTStaLeaveDayName.OptionsColumn.AllowMove = False
        Me.FTStaLeaveDayName.OptionsColumn.ReadOnly = True
        Me.FTStaLeaveDayName.Visible = True
        Me.FTStaLeaveDayName.VisibleIndex = 6
        Me.FTStaLeaveDayName.Width = 168
        '
        'ColFTHoliday
        '
        Me.ColFTHoliday.AppearanceCell.Options.UseTextOptions = True
        Me.ColFTHoliday.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ColFTHoliday.AppearanceHeader.Options.UseTextOptions = True
        Me.ColFTHoliday.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ColFTHoliday.Caption = "ไม่รวมวันหยุด"
        Me.ColFTHoliday.ColumnEdit = Me.RepColFTHoliday
        Me.ColFTHoliday.FieldName = "FTHoliday"
        Me.ColFTHoliday.Name = "ColFTHoliday"
        Me.ColFTHoliday.OptionsColumn.AllowEdit = False
        Me.ColFTHoliday.OptionsColumn.AllowMove = False
        Me.ColFTHoliday.OptionsColumn.ReadOnly = True
        Me.ColFTHoliday.Visible = True
        Me.ColFTHoliday.VisibleIndex = 7
        Me.ColFTHoliday.Width = 100
        '
        'RepColFTHoliday
        '
        Me.RepColFTHoliday.AutoHeight = False
        Me.RepColFTHoliday.Caption = "Check"
        Me.RepColFTHoliday.Name = "RepColFTHoliday"
        Me.RepColFTHoliday.ValueChecked = "1"
        Me.RepColFTHoliday.ValueUnchecked = "0"
        '
        'FTLeavePay
        '
        Me.FTLeavePay.AppearanceCell.Options.UseTextOptions = True
        Me.FTLeavePay.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTLeavePay.AppearanceHeader.Options.UseTextOptions = True
        Me.FTLeavePay.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTLeavePay.Caption = "ลาจ่าย"
        Me.FTLeavePay.ColumnEdit = Me.RepFTLeavePay
        Me.FTLeavePay.FieldName = "FTLeavePay"
        Me.FTLeavePay.Name = "FTLeavePay"
        Me.FTLeavePay.OptionsColumn.AllowEdit = False
        Me.FTLeavePay.OptionsColumn.AllowMove = False
        Me.FTLeavePay.OptionsColumn.ReadOnly = True
        Me.FTLeavePay.Visible = True
        Me.FTLeavePay.VisibleIndex = 8
        Me.FTLeavePay.Width = 72
        '
        'RepFTLeavePay
        '
        Me.RepFTLeavePay.AutoHeight = False
        Me.RepFTLeavePay.Caption = "Check"
        Me.RepFTLeavePay.Name = "RepFTLeavePay"
        Me.RepFTLeavePay.ValueChecked = "1"
        Me.RepFTLeavePay.ValueUnchecked = "0"
        '
        'FTLeaveStartTime
        '
        Me.FTLeaveStartTime.AppearanceCell.Options.UseTextOptions = True
        Me.FTLeaveStartTime.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTLeaveStartTime.AppearanceHeader.Options.UseTextOptions = True
        Me.FTLeaveStartTime.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTLeaveStartTime.Caption = "เาลา"
        Me.FTLeaveStartTime.FieldName = "FTLeaveStartTime"
        Me.FTLeaveStartTime.Name = "FTLeaveStartTime"
        Me.FTLeaveStartTime.OptionsColumn.AllowEdit = False
        Me.FTLeaveStartTime.OptionsColumn.AllowMove = False
        Me.FTLeaveStartTime.OptionsColumn.ReadOnly = True
        Me.FTLeaveStartTime.Visible = True
        Me.FTLeaveStartTime.VisibleIndex = 9
        Me.FTLeaveStartTime.Width = 77
        '
        'FTLeaveEndTime
        '
        Me.FTLeaveEndTime.AppearanceCell.Options.UseTextOptions = True
        Me.FTLeaveEndTime.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTLeaveEndTime.AppearanceHeader.Options.UseTextOptions = True
        Me.FTLeaveEndTime.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTLeaveEndTime.Caption = "ถึงเวลา"
        Me.FTLeaveEndTime.FieldName = "FTLeaveEndTime"
        Me.FTLeaveEndTime.Name = "FTLeaveEndTime"
        Me.FTLeaveEndTime.OptionsColumn.AllowEdit = False
        Me.FTLeaveEndTime.OptionsColumn.AllowMove = False
        Me.FTLeaveEndTime.OptionsColumn.ReadOnly = True
        Me.FTLeaveEndTime.Visible = True
        Me.FTLeaveEndTime.VisibleIndex = 10
        Me.FTLeaveEndTime.Width = 74
        '
        'FNLeaveTotalTime
        '
        Me.FNLeaveTotalTime.AppearanceCell.Options.UseTextOptions = True
        Me.FNLeaveTotalTime.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNLeaveTotalTime.AppearanceHeader.Options.UseTextOptions = True
        Me.FNLeaveTotalTime.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FNLeaveTotalTime.Caption = "รวม ชม./วัน"
        Me.FNLeaveTotalTime.FieldName = "FNLeaveTotalTime"
        Me.FNLeaveTotalTime.Name = "FNLeaveTotalTime"
        Me.FNLeaveTotalTime.OptionsColumn.AllowEdit = False
        Me.FNLeaveTotalTime.OptionsColumn.AllowMove = False
        Me.FNLeaveTotalTime.OptionsColumn.ReadOnly = True
        Me.FNLeaveTotalTime.Visible = True
        Me.FNLeaveTotalTime.VisibleIndex = 11
        Me.FNLeaveTotalTime.Width = 98
        '
        'FNLeaveTotalDay
        '
        Me.FNLeaveTotalDay.AppearanceCell.Options.UseTextOptions = True
        Me.FNLeaveTotalDay.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNLeaveTotalDay.AppearanceHeader.Options.UseTextOptions = True
        Me.FNLeaveTotalDay.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FNLeaveTotalDay.Caption = "จำนวนวัน"
        Me.FNLeaveTotalDay.FieldName = "FNLeaveTotalDay"
        Me.FNLeaveTotalDay.Name = "FNLeaveTotalDay"
        Me.FNLeaveTotalDay.OptionsColumn.AllowEdit = False
        Me.FNLeaveTotalDay.OptionsColumn.AllowMove = False
        Me.FNLeaveTotalDay.OptionsColumn.ReadOnly = True
        Me.FNLeaveTotalDay.Visible = True
        Me.FNLeaveTotalDay.VisibleIndex = 12
        Me.FNLeaveTotalDay.Width = 78
        '
        'FTStaCalSSO
        '
        Me.FTStaCalSSO.AppearanceCell.Options.UseTextOptions = True
        Me.FTStaCalSSO.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTStaCalSSO.AppearanceHeader.Options.UseTextOptions = True
        Me.FTStaCalSSO.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTStaCalSSO.Caption = "ประกันสังคม"
        Me.FTStaCalSSO.ColumnEdit = Me.RepFTStaCalSSO
        Me.FTStaCalSSO.FieldName = "FTStaCalSSO"
        Me.FTStaCalSSO.Name = "FTStaCalSSO"
        Me.FTStaCalSSO.OptionsColumn.AllowEdit = False
        Me.FTStaCalSSO.OptionsColumn.AllowMove = False
        Me.FTStaCalSSO.OptionsColumn.ReadOnly = True
        Me.FTStaCalSSO.Visible = True
        Me.FTStaCalSSO.VisibleIndex = 13
        Me.FTStaCalSSO.Width = 103
        '
        'RepFTStaCalSSO
        '
        Me.RepFTStaCalSSO.AutoHeight = False
        Me.RepFTStaCalSSO.Caption = "Check"
        Me.RepFTStaCalSSO.Name = "RepFTStaCalSSO"
        Me.RepFTStaCalSSO.ValueChecked = "1"
        Me.RepFTStaCalSSO.ValueUnchecked = "0"
        '
        'FTStaLeaveDay
        '
        Me.FTStaLeaveDay.Caption = "FTStaLeaveDay"
        Me.FTStaLeaveDay.FieldName = "FTStaLeaveDay"
        Me.FTStaLeaveDay.Name = "FTStaLeaveDay"
        Me.FTStaLeaveDay.OptionsColumn.AllowEdit = False
        Me.FTStaLeaveDay.OptionsColumn.AllowMove = False
        Me.FTStaLeaveDay.OptionsColumn.ReadOnly = True
        '
        'FTLeaveNote
        '
        Me.FTLeaveNote.Caption = "FTLeaveNote"
        Me.FTLeaveNote.FieldName = "FTLeaveNote"
        Me.FTLeaveNote.Name = "FTLeaveNote"
        '
        'ColFTStateMedicalCertificate
        '
        Me.ColFTStateMedicalCertificate.Caption = "FTStateMedicalCertificate"
        Me.ColFTStateMedicalCertificate.ColumnEdit = Me.RepColFTHoliday
        Me.ColFTStateMedicalCertificate.FieldName = "FTStateMedicalCertificate"
        Me.ColFTStateMedicalCertificate.Name = "ColFTStateMedicalCertificate"
        Me.ColFTStateMedicalCertificate.OptionsColumn.AllowEdit = False
        Me.ColFTStateMedicalCertificate.OptionsColumn.ReadOnly = True
        Me.ColFTStateMedicalCertificate.Visible = True
        Me.ColFTStateMedicalCertificate.VisibleIndex = 14
        Me.ColFTStateMedicalCertificate.Width = 97
        '
        'ColFTMedicalCertificateName
        '
        Me.ColFTMedicalCertificateName.Caption = "FTMedicalCertificateName"
        Me.ColFTMedicalCertificateName.FieldName = "FTMedicalCertificateName"
        Me.ColFTMedicalCertificateName.Name = "ColFTMedicalCertificateName"
        '
        'CFTInsUser
        '
        Me.CFTInsUser.Caption = "User Create"
        Me.CFTInsUser.FieldName = "FTInsUser"
        Me.CFTInsUser.Name = "CFTInsUser"
        Me.CFTInsUser.OptionsColumn.AllowEdit = False
        Me.CFTInsUser.OptionsColumn.ReadOnly = True
        '
        'CFTInsDate
        '
        Me.CFTInsDate.Caption = "Create Date"
        Me.CFTInsDate.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.CFTInsDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.CFTInsDate.FieldName = "FTInsDate"
        Me.CFTInsDate.Name = "CFTInsDate"
        Me.CFTInsDate.OptionsColumn.AllowEdit = False
        Me.CFTInsDate.OptionsColumn.ReadOnly = True
        '
        'CFTInsTime
        '
        Me.CFTInsTime.Caption = "Time"
        Me.CFTInsTime.FieldName = "FTInsTime"
        Me.CFTInsTime.Name = "CFTInsTime"
        Me.CFTInsTime.OptionsColumn.AllowEdit = False
        Me.CFTInsTime.OptionsColumn.ReadOnly = True
        '
        'CFTStateDeductVacation
        '
        Me.CFTStateDeductVacation.Caption = "หักลาพักร้อน"
        Me.CFTStateDeductVacation.ColumnEdit = Me.RepFTApproveState
        Me.CFTStateDeductVacation.FieldName = "FTStateDeductVacation"
        Me.CFTStateDeductVacation.Name = "CFTStateDeductVacation"
        Me.CFTStateDeductVacation.OptionsColumn.AllowEdit = False
        Me.CFTStateDeductVacation.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFTStateDeductVacation.OptionsColumn.AllowShowHide = False
        Me.CFTStateDeductVacation.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFTStateDeductVacation.OptionsColumn.ReadOnly = True
        Me.CFTStateDeductVacation.Visible = True
        Me.CFTStateDeductVacation.VisibleIndex = 15
        Me.CFTStateDeductVacation.Width = 96
        '
        'RepFTApproveState
        '
        Me.RepFTApproveState.AutoHeight = False
        Me.RepFTApproveState.Caption = "Check"
        Me.RepFTApproveState.Name = "RepFTApproveState"
        Me.RepFTApproveState.ValueChecked = "1"
        Me.RepFTApproveState.ValueUnchecked = "0"
        '
        'cFTStateType
        '
        Me.cFTStateType.Caption = "FTStateType"
        Me.cFTStateType.FieldName = "FTStateType"
        Me.cFTStateType.Name = "cFTStateType"
        Me.cFTStateType.OptionsColumn.AllowEdit = False
        '
        'cFNLeaveTotalTimeMin
        '
        Me.cFNLeaveTotalTimeMin.AppearanceCell.Options.UseTextOptions = True
        Me.cFNLeaveTotalTimeMin.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.cFNLeaveTotalTimeMin.Caption = "FNLeaveTotalTimeMin"
        Me.cFNLeaveTotalTimeMin.FieldName = "FNLeaveTotalTimeMin"
        Me.cFNLeaveTotalTimeMin.Name = "cFNLeaveTotalTimeMin"
        '
        'FTMngApproveState
        '
        Me.FTMngApproveState.Caption = "FTMngApproveState"
        Me.FTMngApproveState.ColumnEdit = Me.RepositoryItemCFTMngApproveState
        Me.FTMngApproveState.FieldName = "FTMngApproveState"
        Me.FTMngApproveState.Name = "FTMngApproveState"
        Me.FTMngApproveState.OptionsColumn.AllowEdit = False
        Me.FTMngApproveState.Visible = True
        Me.FTMngApproveState.VisibleIndex = 16
        Me.FTMngApproveState.Width = 109
        '
        'RepositoryItemCFTMngApproveState
        '
        Me.RepositoryItemCFTMngApproveState.AutoHeight = False
        Me.RepositoryItemCFTMngApproveState.Name = "RepositoryItemCFTMngApproveState"
        Me.RepositoryItemCFTMngApproveState.ValueChecked = "1"
        Me.RepositoryItemCFTMngApproveState.ValueUnchecked = "0"
        '
        'FTMngApproveBy
        '
        Me.FTMngApproveBy.Caption = "FTMngApproveBy"
        Me.FTMngApproveBy.FieldName = "FTMngApproveBy"
        Me.FTMngApproveBy.Name = "FTMngApproveBy"
        Me.FTMngApproveBy.OptionsColumn.AllowEdit = False
        Me.FTMngApproveBy.Visible = True
        Me.FTMngApproveBy.VisibleIndex = 17
        Me.FTMngApproveBy.Width = 92
        '
        'FTMngApproveTime
        '
        Me.FTMngApproveTime.Caption = "FTMngApproveTime"
        Me.FTMngApproveTime.FieldName = "FTMngApproveTime"
        Me.FTMngApproveTime.Name = "FTMngApproveTime"
        Me.FTMngApproveTime.OptionsColumn.AllowEdit = False
        Me.FTMngApproveTime.Visible = True
        Me.FTMngApproveTime.VisibleIndex = 18
        Me.FTMngApproveTime.Width = 94
        '
        'FDMngApproveDate
        '
        Me.FDMngApproveDate.Caption = "FDMngApproveDate"
        Me.FDMngApproveDate.FieldName = "FDMngApproveDate"
        Me.FDMngApproveDate.Name = "FDMngApproveDate"
        Me.FDMngApproveDate.OptionsColumn.AllowEdit = False
        Me.FDMngApproveDate.Visible = True
        Me.FDMngApproveDate.VisibleIndex = 19
        '
        'FTApproveState
        '
        Me.FTApproveState.AppearanceHeader.Options.UseTextOptions = True
        Me.FTApproveState.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTApproveState.Caption = "อนุมัติ"
        Me.FTApproveState.ColumnEdit = Me.RepFTApproveState
        Me.FTApproveState.FieldName = "FTApproveState"
        Me.FTApproveState.Name = "FTApproveState"
        Me.FTApproveState.OptionsColumn.AllowEdit = False
        Me.FTApproveState.OptionsColumn.AllowMove = False
        Me.FTApproveState.OptionsColumn.ReadOnly = True
        Me.FTApproveState.Width = 64
        '
        'SickLeave
        '
        Me.SickLeave.AppearanceCell.Options.UseTextOptions = True
        Me.SickLeave.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.SickLeave.Caption = "SickLeave"
        Me.SickLeave.FieldName = "SickLeave"
        Me.SickLeave.Name = "SickLeave"
        Me.SickLeave.OptionsColumn.AllowEdit = False
        Me.SickLeave.Visible = True
        Me.SickLeave.VisibleIndex = 20
        '
        'BusinessLeave
        '
        Me.BusinessLeave.AppearanceCell.Options.UseTextOptions = True
        Me.BusinessLeave.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.BusinessLeave.Caption = "BusinessLeave"
        Me.BusinessLeave.FieldName = "BusinessLeave"
        Me.BusinessLeave.Name = "BusinessLeave"
        Me.BusinessLeave.OptionsColumn.AllowEdit = False
        Me.BusinessLeave.OptionsColumn.ReadOnly = True
        Me.BusinessLeave.Visible = True
        Me.BusinessLeave.VisibleIndex = 21
        '
        'VacationLeave
        '
        Me.VacationLeave.AppearanceCell.Options.UseTextOptions = True
        Me.VacationLeave.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.VacationLeave.Caption = "VacationLeave"
        Me.VacationLeave.FieldName = "VacationLeave"
        Me.VacationLeave.Name = "VacationLeave"
        Me.VacationLeave.OptionsColumn.AllowEdit = False
        Me.VacationLeave.OptionsColumn.ReadOnly = True
        Me.VacationLeave.Visible = True
        Me.VacationLeave.VisibleIndex = 22
        '
        'FTEmpTypeName
        '
        Me.FTEmpTypeName.Caption = "FTEmpTypeName"
        Me.FTEmpTypeName.FieldName = "FTEmpTypeName"
        Me.FTEmpTypeName.Name = "FTEmpTypeName"
        Me.FTEmpTypeName.OptionsColumn.AllowEdit = False
        Me.FTEmpTypeName.OptionsColumn.ReadOnly = True
        Me.FTEmpTypeName.Visible = True
        Me.FTEmpTypeName.VisibleIndex = 23
        Me.FTEmpTypeName.Width = 100
        '
        'FTSectName
        '
        Me.FTSectName.Caption = "FTSectName"
        Me.FTSectName.FieldName = "FTSectName"
        Me.FTSectName.Name = "FTSectName"
        Me.FTSectName.OptionsColumn.AllowEdit = False
        Me.FTSectName.OptionsColumn.ReadOnly = True
        Me.FTSectName.Visible = True
        Me.FTSectName.VisibleIndex = 24
        Me.FTSectName.Width = 100
        '
        'FTUnitSectName
        '
        Me.FTUnitSectName.Caption = "FTUnitSectName"
        Me.FTUnitSectName.FieldName = "FTUnitSectName"
        Me.FTUnitSectName.Name = "FTUnitSectName"
        Me.FTUnitSectName.OptionsColumn.AllowEdit = False
        Me.FTUnitSectName.OptionsColumn.ReadOnly = True
        Me.FTUnitSectName.Visible = True
        Me.FTUnitSectName.VisibleIndex = 25
        Me.FTUnitSectName.Width = 100
        '
        'RepositoryItemCheckEdit2
        '
        Me.RepositoryItemCheckEdit2.AutoHeight = False
        Me.RepositoryItemCheckEdit2.Caption = "Check"
        Me.RepositoryItemCheckEdit2.Name = "RepositoryItemCheckEdit2"
        Me.RepositoryItemCheckEdit2.ValueChecked = "1"
        Me.RepositoryItemCheckEdit2.ValueUnchecked = "0"
        '
        'RepositoryFTApproveState
        '
        Me.RepositoryFTApproveState.AutoHeight = False
        Me.RepositoryFTApproveState.Caption = "Check"
        Me.RepositoryFTApproveState.Name = "RepositoryFTApproveState"
        Me.RepositoryFTApproveState.ValueChecked = "1"
        Me.RepositoryFTApproveState.ValueUnchecked = "0"
        '
        'RepColFTLeavePay
        '
        Me.RepColFTLeavePay.AutoHeight = False
        Me.RepColFTLeavePay.Caption = "Check"
        Me.RepColFTLeavePay.Name = "RepColFTLeavePay"
        Me.RepColFTLeavePay.ValueChecked = "1"
        Me.RepColFTLeavePay.ValueUnchecked = "0"
        '
        'GridView1
        '
        Me.GridView1.GridControl = Me.ogcleaveapp
        Me.GridView1.Name = "GridView1"
        '
        'FTSelectAllleave
        '
        Me.FTSelectAllleave.Location = New System.Drawing.Point(28, 1)
        Me.FTSelectAllleave.Margin = New System.Windows.Forms.Padding(4)
        Me.FTSelectAllleave.Name = "FTSelectAllleave"
        Me.FTSelectAllleave.Properties.Caption = "Select All"
        Me.FTSelectAllleave.Size = New System.Drawing.Size(230, 20)
        Me.FTSelectAllleave.TabIndex = 1
        Me.FTSelectAllleave.Tag = "2|"
        '
        'ocmapprove
        '
        Me.ocmapprove.Location = New System.Drawing.Point(15, 22)
        Me.ocmapprove.Margin = New System.Windows.Forms.Padding(4)
        Me.ocmapprove.Name = "ocmapprove"
        Me.ocmapprove.Size = New System.Drawing.Size(114, 30)
        Me.ocmapprove.TabIndex = 10
        Me.ocmapprove.Text = "Save"
        '
        'ocmreject
        '
        Me.ocmreject.Location = New System.Drawing.Point(180, 22)
        Me.ocmreject.Margin = New System.Windows.Forms.Padding(4)
        Me.ocmreject.Name = "ocmreject"
        Me.ocmreject.Size = New System.Drawing.Size(114, 30)
        Me.ocmreject.TabIndex = 11
        Me.ocmreject.Text = "Reject"
        '
        'ocmpreview
        '
        Me.ocmpreview.Location = New System.Drawing.Point(319, 22)
        Me.ocmpreview.Margin = New System.Windows.Forms.Padding(4)
        Me.ocmpreview.Name = "ocmpreview"
        Me.ocmpreview.Size = New System.Drawing.Size(132, 30)
        Me.ocmpreview.TabIndex = 12
        Me.ocmpreview.Text = "Preview"
        '
        'ocmsavelayout
        '
        Me.ocmsavelayout.Location = New System.Drawing.Point(477, 21)
        Me.ocmsavelayout.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmsavelayout.Name = "ocmsavelayout"
        Me.ocmsavelayout.Size = New System.Drawing.Size(136, 28)
        Me.ocmsavelayout.TabIndex = 333
        Me.ocmsavelayout.Text = "savelayoutgrid"
        '
        'ogbmainprocbutton
        '
        Me.ogbmainprocbutton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmsavelayout)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmpreview)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmreject)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmapprove)
        Me.ogbmainprocbutton.Location = New System.Drawing.Point(263, 206)
        Me.ogbmainprocbutton.Margin = New System.Windows.Forms.Padding(4)
        Me.ogbmainprocbutton.Name = "ogbmainprocbutton"
        Me.ogbmainprocbutton.Size = New System.Drawing.Size(1130, 112)
        Me.ogbmainprocbutton.TabIndex = 138
        Me.ogbmainprocbutton.Tag = "2|"
        '
        'wDirectorApproved
        '
        Me.Appearance.Options.UseFont = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1687, 908)
        Me.Controls.Add(Me.ogbmainprocbutton)
        Me.Controls.Add(Me.otbmain)
        Me.Controls.Add(Me.StandaloneBarDockControl)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.MainRibbonControl)
        Me.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.IconOptions.Icon = CType(resources.GetObject("wDirectorApproved.IconOptions.Icon"), System.Drawing.Icon)
        Me.IsMdiContainer = True
        Me.Margin = New System.Windows.Forms.Padding(6, 5, 6, 5)
        Me.Name = "wDirectorApproved"
        Me.Ribbon = Me.MainRibbonControl
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.StatusBar = Me.RibbonStatusBar
        Me.Tag = "|WISDOM SYSTEM|WISDOM SYSTEM"
        Me.Text = "WISDOM SYSTEM"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.MainRibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemComboBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.oDockManager, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogApprovedMail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogApprovedMail.ResumeLayout(False)
        CType(Me.ochkselectall.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogDirectorApproved, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvDirectorApproved, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.otbmain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.otbmain.ResumeLayout(False)
        Me.otpordercost.ResumeLayout(False)
        CType(Me.ogrpApprovedordercost, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogrpApprovedordercost.ResumeLayout(False)
        CType(Me.ogcordercost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvordercost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit1FTSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepFNIncomeAfterRawmaterial, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepFNWageCost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepFNProdCost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTSelectAllOrderCost.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.otpapppo.ResumeLayout(False)
        Me.otpappfactory.ResumeLayout(False)
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        CType(Me.ogcdirector, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvdirector, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositorySelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ockdirectorselectall.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.otpappdocument.ResumeLayout(False)
        CType(Me.ogbdocument, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbdocument.ResumeLayout(False)
        CType(Me.ogcdocument, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvdocument, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryFTSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryFTSandApprove, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTSelectAllDC.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.otpleaveapprove.ResumeLayout(False)
        CType(Me.ogbapproveleave, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbapproveleave.ResumeLayout(False)
        CType(Me.ogcleaveapp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvapproveleave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemFTSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepColFTHoliday, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepFTLeavePay, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepFTStaCalSSO, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepFTApproveState, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCFTMngApproveState, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryFTApproveState, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepColFTLeavePay, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTSelectAllleave.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbmainprocbutton.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents MainRibbonControl As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents RibbonStatusBar As DevExpress.XtraBars.Ribbon.RibbonStatusBar
    Friend WithEvents MainDefaultLookAndFeel As DevExpress.LookAndFeel.DefaultLookAndFeel
    Friend WithEvents mnusysabout As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents oDockManager As DevExpress.XtraBars.Docking.DockManager
    Friend WithEvents StandaloneBarDockControl As DevExpress.XtraBars.StandaloneBarDockControl
    Friend WithEvents FTUserLogINIP As DevExpress.XtraBars.BarStaticItem
    Friend WithEvents RepositoryItemComboBox1 As DevExpress.XtraEditors.Repository.RepositoryItemComboBox
    Friend WithEvents ogApprovedMail As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ochkselectall As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents ogDirectorApproved As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvDirectorApproved As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents ColFTStateApproved As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemCheckEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents ColFTPurchaseBy As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFTSuperVisorName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFTPurchaseNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFDPurchaseDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFTPurchaseState As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFTCmpRunCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFTCmpRunName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFTSuplCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFTSuplName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFDDeliveryDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFTCrTermCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFTCrTermDesc As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFNCreditDay As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFTTermOfPMCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFTTermOfPMName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFTDeliveryCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFTDeliveryDesc As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFNExchangeRate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFTContactPerson As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFNDisCountAmt As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFNSurcharge As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFNVatAmt As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFNPOGrandAmt As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFTRemark As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFNDisCountPer As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFNPONetAmt As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFNVatPer As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFTTeamGrpCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFTTeamGrpName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFTPurGrpCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFTPurGrpName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents otmchkpo As System.Windows.Forms.Timer
    Friend WithEvents FTCurCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTQuantityinfo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTPoTypeState As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTStateFree As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents otbmain As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents otpapppo As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents otpappfactory As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ockdirectorselectall As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents ogcdirector As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvdirector As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents C2FNHSysCmpId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTSelect As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositorySelect As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents C2FTCmpCodeTo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents C2FTCmpCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents C2FTCompName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents C2FTPORef As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents C2FNHSysStyleId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents C2FTStyleCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents C2FTOrderNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents C2FNHSysCustId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents C2FTCustCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents C2FTCustName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents C2FNHSysBuyId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents C2FTBuyCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents C2FNHSysSeasonId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents C2FTSeasonCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents C2FNHSysProdTypeId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents C2FTProdTypeCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents C2FTProdTypeName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents C2FDShipDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents C2FTCurCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents C2FNQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemTextEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents C2FNAmt As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents C2FNExtraQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents C2FNAmntExtra As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFNGarmentQtyTest As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNAmntQtyTest As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents C2FNTotalQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNGrandAmnt As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents C2FTStateEmb As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents C2FTStatePrint As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents C2FTStateHeat As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents C2FTStateLaser As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents C2FTStateWindows As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents C2FTStateOrderApp As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents C2FTAppBy As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents C2FDAppDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents C2FTAppTime As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTStateSendDirectorApp As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTStateSendDirectorBy As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FDStateSendDirectorDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTStateSendDirectorTime As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTStateDirectorApp As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTStateDirectorAppBy As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FDStateDirectorAppDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTStateDirectorAppTime As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTStateDirectorReject As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTStateDirectorRejectBy As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FDStateDirectorRejectDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTStateDirectorRejectTime As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNCM As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNGrandAmtCM As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents otpappdocument As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents ogbdocument As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ogcdocument As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvdocument As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents cFTSelect As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryFTSelect As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents cFTDocumentNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFDDocumentDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTDocumentBy As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTCmpCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTCmpName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTDocumentTitle As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTNote As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTBenefit As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNOperActivity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTOperActivityName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTDocName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTDocRefCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTDocTypeName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTFileTypeName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFBDocument As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNDocType As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNHSysCmpId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryFTSandApprove As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents FTSelectAllDC As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents otpordercost As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents ogbmainprocbutton As DevExpress.XtraEditors.PanelControl
    Friend WithEvents ocmsavelayout As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmpreview As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmreject As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmapprove As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ogrpApprovedordercost As DevExpress.XtraEditors.GroupControl
    Friend WithEvents FTSelectAllOrderCost As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents ogcordercost As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvordercost As DevExpress.XtraGrid.Views.BandedGrid.BandedGridView
    Friend WithEvents gridBand3 As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents BandedGridColumn2 As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents RepositoryItemCheckEdit1FTSelect As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents gCmpName As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents FTCmpName As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents gFTInvoice As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents FTInvoiceNo As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents gInvoiceDate As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents FDInvoiceDate As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents gFTOrderNo As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents FTOrderNo As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents gFTPORef As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents FTPORef As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents gFTStyle As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents FTStyleCode As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents gFNExportQuantity As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents FNExportQuantity As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents gFNExportAmt As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents FNExportAmtTHB As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents gFNExportAmtus As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents FNExportAmt As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents gCenter As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents gCenter1 As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents FNFabricCostPer As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents gCenter2 As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents FNFabricCost As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents gCenter3 As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents FNAccessroryCostPer As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents gCenter4 As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents BandedGridColumn1 As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents gFNFabricAccMinCostPer As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents FNFabricAccMinCostPer As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents gFabricAccMin As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents FNFabricAccMinCost As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents gCenter5 As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents FNAccFabStockCostPer As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents gCenter6 As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents FNAccFabStockCost As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents cFNFabricAccStockOtherCostPer As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents FNFabricAccStockOtherCostPer As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents gOtherStock As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents FNFabricAccStockOtherCost As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents gFNOtherServiceCostPer As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents FNOtherServiceCostPer As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents gOtherServiceCost As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents FNOtherServiceCost As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents gFNIncomeAfterRawmaterialPer As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents FNIncomeAfterRawmaterialPer As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents gFNIncomeAferRawmaterial As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents FNIncomeAfterRawmaterial As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents RepFNIncomeAfterRawmaterial As DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit
    Friend WithEvents gCenter7 As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents FNConductedCostPer As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents gCenter8 As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents FNConductedCost As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents gCenter9 As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents FNOtherCostPer As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents gCenter10 As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents FNOtherCost As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents gCenter11 As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents FNEmbFacCostPer As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents gCenter12 As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents FNEmbFacCost As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents gCenter13 As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents FNEmpPrintBranchPer As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents gCenter14 As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents FNEmpPrintBranch As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents gFNWagePullPer As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents FNWagePullPer As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents gFNWagePull As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents FNWagePull As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents gBtanch As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents gBtanch1 As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents FNExportAmtBFPer As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents gBtanch2 As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents FNExportAmtBF As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents gBtanch3 As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents FNWageCostPer As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents gBtanch4 As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents FNWageCost As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents RepFNWageCost As DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit
    Friend WithEvents gFNSewCostPer As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents FNSewCostPer As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents gSewCost As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents FNSewCost As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents gEmbroideryCostPer As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents FNEmbroideryCostPer As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents gEmbroidCost As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents FNEmbroideryCost As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents gFNPrintCostPer As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents FNPrintCostPer As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents gPrintCost As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents FNPrintCost As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents gBtanch5 As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents FNEmpPrintSubPer As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents gBtanch6 As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents FNEmpPrintSub As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents gFNImportCostPer As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents FNImportCostPer As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents gImportCost As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents FNImportCost As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents gBtanch7 As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents FNImportExportCostPer As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents gBtanch8 As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents FNImportExportCost As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents gBtanch9 As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents FNProdCostPer As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents gBtanch10 As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents FNProdCost As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents RepFNProdCost As DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit
    Friend WithEvents gBtanch11 As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents FNCommissionCostPer As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents gBtanch12 As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents FNCommissionCost As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents gBtanch13 As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents FNTransportAirCostPer As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents gBtanch14 As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents FNTransportAirCost As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents gEtcCostper As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents FNEtcCostPer As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents gEtcCost As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents FNEtcCost As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents gProfit As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents gProfit1 As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents FNNetProfitPer As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents gProfit2 As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents FNNetProfit As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents gFNNetProfitRcv As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents FNNetProfitRcv As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents gFNOrderQuantity As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents FNOrderQuantity As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents gFNExport As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents gridBand1 As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents FNExportQuantityTo As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents gridBand2 As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents FNExportQuantityOtherMonth As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents รวม As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents FNTotalExport As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents gdiff As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents FNOrderQuantityBal As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents gridBand4 As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents FNPrice As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents gdiffper As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents FNLostPer As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents CFNStateRow As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents C33FTColorway As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents C33FTNikePOLineItem As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents C33FTSizeBreakDown As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents otpleaveapprove As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents ogbapproveleave As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ogcleaveapp As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvapproveleave As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents FNHSysEmpID As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemFTSelect As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents cFTEmpName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFTStartDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFTEndDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNSumTotalLeaveDay As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTLeaveType As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTLeaveTypeName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTStaLeaveDayName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFTHoliday As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepColFTHoliday As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents FTLeavePay As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepFTLeavePay As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents FTLeaveStartTime As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTLeaveEndTime As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNLeaveTotalTime As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNLeaveTotalDay As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTStaCalSSO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepFTStaCalSSO As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents FTStaLeaveDay As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTLeaveNote As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFTStateMedicalCertificate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFTMedicalCertificateName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFTInsUser As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFTInsDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFTInsTime As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFTStateDeductVacation As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepFTApproveState As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents cFTStateType As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNLeaveTotalTimeMin As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTMngApproveState As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemCFTMngApproveState As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents FTMngApproveBy As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTMngApproveTime As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FDMngApproveDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTApproveState As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents SickLeave As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents BusinessLeave As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents VacationLeave As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTEmpTypeName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTSectName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTUnitSectName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemCheckEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents RepositoryFTApproveState As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents RepColFTLeavePay As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents FTSelectAllleave As DevExpress.XtraEditors.CheckEdit
End Class

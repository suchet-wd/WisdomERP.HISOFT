<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wOrderCostApproved
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
        Me.MainRibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.mnusysabout = New DevExpress.XtraBars.BarButtonItem()
        Me.FTUserLogINIP = New DevExpress.XtraBars.BarStaticItem()
        Me.RepositoryItemComboBox1 = New DevExpress.XtraEditors.Repository.RepositoryItemComboBox()
        Me.RibbonStatusBar = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.MainDefaultLookAndFeel = New DevExpress.LookAndFeel.DefaultLookAndFeel()
        Me.oDockManager = New DevExpress.XtraBars.Docking.DockManager()
        Me.StandaloneBarDockControl = New DevExpress.XtraBars.StandaloneBarDockControl()
        Me.ogbmainprocbutton = New DevExpress.XtraEditors.PanelControl()
        Me.ocmsavelayout = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmreject = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmapprove = New DevExpress.XtraEditors.SimpleButton()
        Me.otmchkpo = New System.Windows.Forms.Timer()
        Me.otbmain = New DevExpress.XtraTab.XtraTabControl()
        Me.otpappcminv = New DevExpress.XtraTab.XtraTabPage()
        Me.ogrpordercosting = New DevExpress.XtraEditors.GroupControl()
        Me.ogdtime = New DevExpress.XtraGrid.GridControl()
        Me.oAdvBandedGridView = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridView()
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
        Me.ogcdocument = New DevExpress.XtraGrid.GridControl()
        Me.ogvdocument = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.cFTSelect = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryFTSelect = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.cFTCmpCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTCmpName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTInvoiceNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTOrderNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTPORef = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTStyleCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTCustName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNTransportAirCost = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNFabricCost = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTCustCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNAccessroryCost = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTStyleName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNWageCost = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNTransportCost = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNHSysCmpId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNNetProfit = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTStateApp = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNPrice = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryFTSandApprove = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.FTSelectAllOrderCosting = New DevExpress.XtraEditors.CheckEdit()
        CType(Me.MainRibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemComboBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.oDockManager, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbmainprocbutton.SuspendLayout()
        CType(Me.otbmain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.otbmain.SuspendLayout()
        Me.otpappcminv.SuspendLayout()
        CType(Me.ogrpordercosting, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogrpordercosting.SuspendLayout()
        CType(Me.ogdtime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.oAdvBandedGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit1FTSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepFNIncomeAfterRawmaterial, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepFNWageCost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepFNProdCost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogcdocument, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvdocument, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryFTSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryFTSandApprove, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTSelectAllOrderCosting.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'MainRibbonControl
        '
        Me.MainRibbonControl.ApplicationButtonText = Nothing
        Me.MainRibbonControl.ExpandCollapseItem.Id = 0
        Me.MainRibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.MainRibbonControl.ExpandCollapseItem, Me.mnusysabout, Me.FTUserLogINIP})
        Me.MainRibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.MainRibbonControl.Margin = New System.Windows.Forms.Padding(6, 5, 6, 5)
        Me.MainRibbonControl.MaxItemId = 5
        Me.MainRibbonControl.Name = "MainRibbonControl"
        Me.MainRibbonControl.PageCategoryAlignment = DevExpress.XtraBars.Ribbon.RibbonPageCategoryAlignment.Left
        Me.MainRibbonControl.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemComboBox1})
        Me.MainRibbonControl.ShowExpandCollapseButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.MainRibbonControl.ShowToolbarCustomizeItem = False
        Me.MainRibbonControl.Size = New System.Drawing.Size(1478, 69)
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
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 711)
        Me.RibbonStatusBar.Margin = New System.Windows.Forms.Padding(6, 5, 6, 5)
        Me.RibbonStatusBar.Name = "RibbonStatusBar"
        Me.RibbonStatusBar.Ribbon = Me.MainRibbonControl
        Me.RibbonStatusBar.Size = New System.Drawing.Size(1478, 35)
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
        Me.StandaloneBarDockControl.Location = New System.Drawing.Point(0, 69)
        Me.StandaloneBarDockControl.Margin = New System.Windows.Forms.Padding(4)
        Me.StandaloneBarDockControl.Name = "StandaloneBarDockControl"
        Me.StandaloneBarDockControl.Size = New System.Drawing.Size(1478, 0)
        Me.StandaloneBarDockControl.Text = "StandaloneBarDockControl1"
        '
        'ogbmainprocbutton
        '
        Me.ogbmainprocbutton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmsavelayout)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmexit)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmreject)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmapprove)
        Me.ogbmainprocbutton.Location = New System.Drawing.Point(229, 191)
        Me.ogbmainprocbutton.Margin = New System.Windows.Forms.Padding(4)
        Me.ogbmainprocbutton.Name = "ogbmainprocbutton"
        Me.ogbmainprocbutton.Size = New System.Drawing.Size(1093, 92)
        Me.ogbmainprocbutton.TabIndex = 138
        Me.ogbmainprocbutton.Tag = "2|"
        '
        'ocmsavelayout
        '
        Me.ocmsavelayout.Location = New System.Drawing.Point(293, 25)
        Me.ocmsavelayout.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmsavelayout.Name = "ocmsavelayout"
        Me.ocmsavelayout.Size = New System.Drawing.Size(136, 28)
        Me.ocmsavelayout.TabIndex = 332
        Me.ocmsavelayout.Text = "savelayoutgrid"
        '
        'ocmexit
        '
        Me.ocmexit.Location = New System.Drawing.Point(492, 22)
        Me.ocmexit.Margin = New System.Windows.Forms.Padding(4)
        Me.ocmexit.Name = "ocmexit"
        Me.ocmexit.Size = New System.Drawing.Size(132, 30)
        Me.ocmexit.TabIndex = 13
        Me.ocmexit.Text = "Close"
        Me.ocmexit.Visible = False
        '
        'ocmreject
        '
        Me.ocmreject.Location = New System.Drawing.Point(161, 22)
        Me.ocmreject.Margin = New System.Windows.Forms.Padding(4)
        Me.ocmreject.Name = "ocmreject"
        Me.ocmreject.Size = New System.Drawing.Size(114, 30)
        Me.ocmreject.TabIndex = 11
        Me.ocmreject.Text = "Reject"
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
        'otmchkpo
        '
        Me.otmchkpo.Interval = 60000
        '
        'otbmain
        '
        Me.otbmain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.otbmain.Location = New System.Drawing.Point(0, 69)
        Me.otbmain.Name = "otbmain"
        Me.otbmain.SelectedTabPage = Me.otpappcminv
        Me.otbmain.Size = New System.Drawing.Size(1478, 642)
        Me.otbmain.TabIndex = 14
        Me.otbmain.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.otpappcminv})
        '
        'otpappcminv
        '
        Me.otpappcminv.Controls.Add(Me.ogrpordercosting)
        Me.otpappcminv.Margin = New System.Windows.Forms.Padding(4)
        Me.otpappcminv.Name = "otpappcminv"
        Me.otpappcminv.Size = New System.Drawing.Size(1468, 605)
        Me.otpappcminv.Text = "Approve Order Costing"
        '
        'ogrpordercosting
        '
        Me.ogrpordercosting.AppearanceCaption.Options.UseTextOptions = True
        Me.ogrpordercosting.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ogrpordercosting.Controls.Add(Me.ogbmainprocbutton)
        Me.ogrpordercosting.Controls.Add(Me.ogdtime)
        Me.ogrpordercosting.Controls.Add(Me.ogcdocument)
        Me.ogrpordercosting.Controls.Add(Me.FTSelectAllOrderCosting)
        Me.ogrpordercosting.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogrpordercosting.Location = New System.Drawing.Point(0, 0)
        Me.ogrpordercosting.Margin = New System.Windows.Forms.Padding(4)
        Me.ogrpordercosting.Name = "ogrpordercosting"
        Me.ogrpordercosting.Size = New System.Drawing.Size(1468, 605)
        Me.ogrpordercosting.TabIndex = 12
        Me.ogrpordercosting.Text = "Approved Order Costing"
        '
        'ogdtime
        '
        Me.ogdtime.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogdtime.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogdtime.Location = New System.Drawing.Point(2, 27)
        Me.ogdtime.MainView = Me.oAdvBandedGridView
        Me.ogdtime.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogdtime.Name = "ogdtime"
        Me.ogdtime.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepFNIncomeAfterRawmaterial, Me.RepFNProdCost, Me.RepFNWageCost, Me.RepositoryItemCheckEdit1FTSelect})
        Me.ogdtime.Size = New System.Drawing.Size(1464, 576)
        Me.ogdtime.TabIndex = 395
        Me.ogdtime.TabStop = False
        Me.ogdtime.Tag = "2|"
        Me.ogdtime.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.oAdvBandedGridView})
        '
        'oAdvBandedGridView
        '
        Me.oAdvBandedGridView.Bands.AddRange(New DevExpress.XtraGrid.Views.BandedGrid.GridBand() {Me.gridBand3, Me.gCmpName, Me.gFTInvoice, Me.gInvoiceDate, Me.gFTOrderNo, Me.gFTPORef, Me.gFTStyle, Me.gFNExportQuantity, Me.gFNExportAmt, Me.gFNExportAmtus, Me.gCenter, Me.gBtanch, Me.gProfit, Me.gFNOrderQuantity, Me.gFNExport, Me.gdiff, Me.gridBand4, Me.gdiffper})
        Me.oAdvBandedGridView.Columns.AddRange(New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn() {Me.BandedGridColumn2, Me.FTInvoiceNo, Me.FTOrderNo, Me.FTPORef, Me.FTStyleCode, Me.FNExportQuantity, Me.FNExportAmtTHB, Me.FNExportAmt, Me.FNFabricCostPer, Me.FNWagePullPer, Me.FNWagePull, Me.FNNetProfitRcv, Me.FNFabricCost, Me.FNAccessroryCostPer, Me.BandedGridColumn1, Me.FNAccFabStockCostPer, Me.FNAccFabStockCost, Me.FNConductedCostPer, Me.FNConductedCost, Me.FNOtherCostPer, Me.FNOtherCost, Me.FNEmbFacCostPer, Me.FNEmbFacCost, Me.FNEmpPrintBranchPer, Me.FNEmpPrintBranch, Me.FNExportAmtBFPer, Me.FNExportAmtBF, Me.FNWageCostPer, Me.FNWageCost, Me.FNEmpPrintSubPer, Me.FNEmpPrintSub, Me.FNImportExportCostPer, Me.FNImportExportCost, Me.FNProdCostPer, Me.FNProdCost, Me.FNCommissionCostPer, Me.FNCommissionCost, Me.FNTransportAirCostPer, Me.FNTransportAirCost, Me.FNNetProfitPer, Me.FNNetProfit, Me.FNOrderQuantity, Me.FNExportQuantityTo, Me.FNExportQuantityOtherMonth, Me.FNTotalExport, Me.FNOrderQuantityBal, Me.FNLostPer, Me.FNFabricAccMinCost, Me.FNFabricAccStockOtherCost, Me.FNOtherServiceCost, Me.FNSewCost, Me.FNEmbroideryCost, Me.FNPrintCost, Me.FNImportCost, Me.FNEtcCost, Me.FNIncomeAfterRawmaterial, Me.FNFabricAccMinCostPer, Me.FNFabricAccStockOtherCostPer, Me.FNOtherServiceCostPer, Me.FNSewCostPer, Me.FNEmbroideryCostPer, Me.FNPrintCostPer, Me.FNImportCostPer, Me.FNEtcCostPer, Me.FNIncomeAfterRawmaterialPer, Me.CFNStateRow, Me.FNPrice, Me.FDInvoiceDate, Me.FTCmpName})
        Me.oAdvBandedGridView.GridControl = Me.ogdtime
        Me.oAdvBandedGridView.Name = "oAdvBandedGridView"
        Me.oAdvBandedGridView.OptionsCustomization.AllowQuickHideColumns = False
        Me.oAdvBandedGridView.OptionsPrint.PrintHeader = False
        Me.oAdvBandedGridView.OptionsSelection.MultiSelect = True
        Me.oAdvBandedGridView.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect
        Me.oAdvBandedGridView.OptionsView.ColumnAutoWidth = False
        Me.oAdvBandedGridView.OptionsView.ShowColumnHeaders = False
        Me.oAdvBandedGridView.OptionsView.ShowGroupPanel = False
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
        'ogcdocument
        '
        Me.ogcdocument.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4)
        Me.ogcdocument.Location = New System.Drawing.Point(111, 416)
        Me.ogcdocument.MainView = Me.ogvdocument
        Me.ogcdocument.Margin = New System.Windows.Forms.Padding(4)
        Me.ogcdocument.MenuManager = Me.MainRibbonControl
        Me.ogcdocument.Name = "ogcdocument"
        Me.ogcdocument.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryFTSelect, Me.RepositoryFTSandApprove})
        Me.ogcdocument.Size = New System.Drawing.Size(1355, 187)
        Me.ogcdocument.TabIndex = 2
        Me.ogcdocument.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvdocument})
        '
        'ogvdocument
        '
        Me.ogvdocument.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.cFTSelect, Me.cFTCmpCode, Me.cFTCmpName, Me.cFTInvoiceNo, Me.cFTOrderNo, Me.cFTPORef, Me.cFTStyleCode, Me.cFTCustName, Me.cFNTransportAirCost, Me.cFNFabricCost, Me.cFTCustCode, Me.FNAccessroryCost, Me.cFTStyleName, Me.cFNWageCost, Me.cFNTransportCost, Me.cFNHSysCmpId, Me.cFNNetProfit, Me.cFTStateApp, Me.cFNPrice})
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
        Me.cFTCmpName.Width = 180
        '
        'cFTInvoiceNo
        '
        Me.cFTInvoiceNo.Caption = "FTInvoiceNo"
        Me.cFTInvoiceNo.FieldName = "FTInvoiceNo"
        Me.cFTInvoiceNo.Name = "cFTInvoiceNo"
        Me.cFTInvoiceNo.OptionsColumn.AllowEdit = False
        Me.cFTInvoiceNo.Width = 122
        '
        'cFTOrderNo
        '
        Me.cFTOrderNo.Caption = "FTOrderNo"
        Me.cFTOrderNo.FieldName = "FTOrderNo"
        Me.cFTOrderNo.Name = "cFTOrderNo"
        Me.cFTOrderNo.OptionsColumn.AllowEdit = False
        Me.cFTOrderNo.Visible = True
        Me.cFTOrderNo.VisibleIndex = 3
        Me.cFTOrderNo.Width = 107
        '
        'cFTPORef
        '
        Me.cFTPORef.Caption = "FTPORef"
        Me.cFTPORef.FieldName = "FTPORef"
        Me.cFTPORef.Name = "cFTPORef"
        Me.cFTPORef.OptionsColumn.AllowEdit = False
        Me.cFTPORef.Visible = True
        Me.cFTPORef.VisibleIndex = 4
        Me.cFTPORef.Width = 129
        '
        'cFTStyleCode
        '
        Me.cFTStyleCode.Caption = "FTStyleCode"
        Me.cFTStyleCode.FieldName = "FTStyleCode"
        Me.cFTStyleCode.Name = "cFTStyleCode"
        Me.cFTStyleCode.OptionsColumn.AllowEdit = False
        Me.cFTStyleCode.Visible = True
        Me.cFTStyleCode.VisibleIndex = 7
        Me.cFTStyleCode.Width = 162
        '
        'cFTCustName
        '
        Me.cFTCustName.Caption = "FTCustName"
        Me.cFTCustName.FieldName = "FTCustName"
        Me.cFTCustName.Name = "cFTCustName"
        Me.cFTCustName.OptionsColumn.AllowEdit = False
        Me.cFTCustName.Visible = True
        Me.cFTCustName.VisibleIndex = 6
        Me.cFTCustName.Width = 183
        '
        'cFNTransportAirCost
        '
        Me.cFNTransportAirCost.Caption = "FNTransportAirCost"
        Me.cFNTransportAirCost.DisplayFormat.FormatString = "{0:n2}"
        Me.cFNTransportAirCost.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cFNTransportAirCost.FieldName = "FNTransportAirCost"
        Me.cFNTransportAirCost.Name = "cFNTransportAirCost"
        Me.cFNTransportAirCost.OptionsColumn.AllowEdit = False
        Me.cFNTransportAirCost.Visible = True
        Me.cFNTransportAirCost.VisibleIndex = 13
        Me.cFNTransportAirCost.Width = 115
        '
        'cFNFabricCost
        '
        Me.cFNFabricCost.Caption = "FNFabricCost"
        Me.cFNFabricCost.DisplayFormat.FormatString = "{0:n2}"
        Me.cFNFabricCost.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cFNFabricCost.FieldName = "FNFabricCost"
        Me.cFNFabricCost.Name = "cFNFabricCost"
        Me.cFNFabricCost.OptionsColumn.AllowEdit = False
        Me.cFNFabricCost.Visible = True
        Me.cFNFabricCost.VisibleIndex = 9
        Me.cFNFabricCost.Width = 114
        '
        'cFTCustCode
        '
        Me.cFTCustCode.Caption = "FTCustCode"
        Me.cFTCustCode.FieldName = "FTCustCode"
        Me.cFTCustCode.Name = "cFTCustCode"
        Me.cFTCustCode.OptionsColumn.AllowEdit = False
        Me.cFTCustCode.Visible = True
        Me.cFTCustCode.VisibleIndex = 5
        Me.cFTCustCode.Width = 121
        '
        'FNAccessroryCost
        '
        Me.FNAccessroryCost.Caption = "FNAccessroryCost"
        Me.FNAccessroryCost.DisplayFormat.FormatString = "{0:n2}"
        Me.FNAccessroryCost.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNAccessroryCost.FieldName = "FNAccessroryCost"
        Me.FNAccessroryCost.Name = "FNAccessroryCost"
        Me.FNAccessroryCost.OptionsColumn.AllowEdit = False
        Me.FNAccessroryCost.Visible = True
        Me.FNAccessroryCost.VisibleIndex = 10
        Me.FNAccessroryCost.Width = 102
        '
        'cFTStyleName
        '
        Me.cFTStyleName.Caption = "FTStyleName"
        Me.cFTStyleName.FieldName = "FTStyleName"
        Me.cFTStyleName.Name = "cFTStyleName"
        Me.cFTStyleName.OptionsColumn.AllowEdit = False
        Me.cFTStyleName.Visible = True
        Me.cFTStyleName.VisibleIndex = 8
        Me.cFTStyleName.Width = 119
        '
        'cFNWageCost
        '
        Me.cFNWageCost.Caption = "FNWageCost"
        Me.cFNWageCost.DisplayFormat.FormatString = "{0:n2}"
        Me.cFNWageCost.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cFNWageCost.FieldName = "FNWageCost"
        Me.cFNWageCost.Name = "cFNWageCost"
        Me.cFNWageCost.OptionsColumn.AllowEdit = False
        Me.cFNWageCost.Visible = True
        Me.cFNWageCost.VisibleIndex = 11
        Me.cFNWageCost.Width = 103
        '
        'cFNTransportCost
        '
        Me.cFNTransportCost.Caption = "FNTransportCost"
        Me.cFNTransportCost.DisplayFormat.FormatString = "{0:n2}"
        Me.cFNTransportCost.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cFNTransportCost.FieldName = "FNTransportCost"
        Me.cFNTransportCost.Name = "cFNTransportCost"
        Me.cFNTransportCost.OptionsColumn.AllowEdit = False
        Me.cFNTransportCost.Visible = True
        Me.cFNTransportCost.VisibleIndex = 12
        Me.cFNTransportCost.Width = 115
        '
        'cFNHSysCmpId
        '
        Me.cFNHSysCmpId.Caption = "FNHSysCmpId"
        Me.cFNHSysCmpId.FieldName = "FNHSysCmpId"
        Me.cFNHSysCmpId.Name = "cFNHSysCmpId"
        '
        'cFNNetProfit
        '
        Me.cFNNetProfit.Caption = "FNNetProfit"
        Me.cFNNetProfit.DisplayFormat.FormatString = "{0:n2}"
        Me.cFNNetProfit.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cFNNetProfit.FieldName = "FNNetProfit"
        Me.cFNNetProfit.Name = "cFNNetProfit"
        Me.cFNNetProfit.OptionsColumn.AllowEdit = False
        Me.cFNNetProfit.Visible = True
        Me.cFNNetProfit.VisibleIndex = 14
        '
        'cFTStateApp
        '
        Me.cFTStateApp.Caption = "FTStateApp"
        Me.cFTStateApp.FieldName = "FTStateApp"
        Me.cFTStateApp.Name = "cFTStateApp"
        '
        'cFNPrice
        '
        Me.cFNPrice.Caption = "FNPrice"
        Me.cFNPrice.FieldName = "FNPrice"
        Me.cFNPrice.Name = "cFNPrice"
        '
        'RepositoryFTSandApprove
        '
        Me.RepositoryFTSandApprove.AutoHeight = False
        Me.RepositoryFTSandApprove.Caption = "Check"
        Me.RepositoryFTSandApprove.Name = "RepositoryFTSandApprove"
        Me.RepositoryFTSandApprove.ValueChecked = "1"
        Me.RepositoryFTSandApprove.ValueUnchecked = "0"
        '
        'FTSelectAllOrderCosting
        '
        Me.FTSelectAllOrderCosting.Location = New System.Drawing.Point(28, 1)
        Me.FTSelectAllOrderCosting.Margin = New System.Windows.Forms.Padding(4)
        Me.FTSelectAllOrderCosting.Name = "FTSelectAllOrderCosting"
        Me.FTSelectAllOrderCosting.Properties.Caption = "Select All"
        Me.FTSelectAllOrderCosting.Size = New System.Drawing.Size(230, 20)
        Me.FTSelectAllOrderCosting.TabIndex = 1
        Me.FTSelectAllOrderCosting.Tag = "2|"
        '
        'wOrderCostApproved
        '
        Me.Appearance.Options.UseFont = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(11.0!, 21.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1478, 746)
        Me.Controls.Add(Me.otbmain)
        Me.Controls.Add(Me.StandaloneBarDockControl)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.MainRibbonControl)
        Me.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.IsMdiContainer = True
        Me.Margin = New System.Windows.Forms.Padding(6, 5, 6, 5)
        Me.Name = "wOrderCostApproved"
        Me.Ribbon = Me.MainRibbonControl
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.StatusBar = Me.RibbonStatusBar
        Me.Tag = "|WISDOM SYSTEM|WISDOM SYSTEM"
        Me.Text = "WISDOM SYSTEM"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.MainRibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemComboBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.oDockManager, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbmainprocbutton.ResumeLayout(False)
        CType(Me.otbmain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.otbmain.ResumeLayout(False)
        Me.otpappcminv.ResumeLayout(False)
        CType(Me.ogrpordercosting, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogrpordercosting.ResumeLayout(False)
        CType(Me.ogdtime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.oAdvBandedGridView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit1FTSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepFNIncomeAfterRawmaterial, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepFNWageCost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepFNProdCost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogcdocument, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvdocument, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryFTSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryFTSandApprove, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTSelectAllOrderCosting.Properties, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents ogbmainprocbutton As DevExpress.XtraEditors.PanelControl
    Friend WithEvents ocmreject As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmapprove As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents otmchkpo As System.Windows.Forms.Timer
    Friend WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmsavelayout As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents otbmain As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents otpappcminv As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents ogrpordercosting As DevExpress.XtraEditors.GroupControl
    Friend WithEvents FTSelectAllOrderCosting As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents ogcdocument As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvdocument As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents cFTSelect As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryFTSelect As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents cFTInvoiceNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTOrderNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTPORef As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTCmpCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTCmpName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTCustName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNTransportAirCost As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNFabricCost As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTCustCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNAccessroryCost As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTStyleCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTStyleName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNWageCost As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNTransportCost As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryFTSandApprove As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents cFNHSysCmpId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNNetProfit As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTStateApp As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNPrice As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ogdtime As DevExpress.XtraGrid.GridControl
    Friend WithEvents oAdvBandedGridView As DevExpress.XtraGrid.Views.BandedGrid.BandedGridView
    Friend WithEvents BandedGridColumn2 As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents RepositoryItemCheckEdit1FTSelect As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents FTInvoiceNo As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents FTOrderNo As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents FTPORef As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents FTStyleCode As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents FNExportQuantity As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents FNExportAmtTHB As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents FNExportAmt As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents FNFabricCostPer As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents FNFabricCost As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents FNAccessroryCostPer As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents BandedGridColumn1 As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents FNFabricAccMinCostPer As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents FNFabricAccMinCost As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents FNAccFabStockCostPer As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents FNAccFabStockCost As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents FNFabricAccStockOtherCostPer As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents FNFabricAccStockOtherCost As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents FNOtherServiceCostPer As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents FNOtherServiceCost As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents FNIncomeAfterRawmaterialPer As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents FNIncomeAfterRawmaterial As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents RepFNIncomeAfterRawmaterial As DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit
    Friend WithEvents FNConductedCostPer As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents FNConductedCost As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents FNOtherCostPer As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents FNOtherCost As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents FNEmbFacCostPer As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents FNEmbFacCost As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents FNEmpPrintBranchPer As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents FNEmpPrintBranch As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents FNWagePullPer As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents FNWagePull As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents FNExportAmtBFPer As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents FNExportAmtBF As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents FNWageCostPer As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents FNWageCost As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents RepFNWageCost As DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit
    Friend WithEvents FNSewCostPer As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents FNSewCost As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents FNEmbroideryCostPer As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents FNEmbroideryCost As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents FNPrintCostPer As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents FNPrintCost As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents FNEmpPrintSubPer As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents FNEmpPrintSub As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents FNImportCostPer As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents FNImportCost As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents FNImportExportCostPer As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents FNImportExportCost As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents FNProdCostPer As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents FNProdCost As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents RepFNProdCost As DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit
    Friend WithEvents FNCommissionCostPer As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents FNCommissionCost As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents FNTransportAirCostPer As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents FNTransportAirCost As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents FNEtcCostPer As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents FNEtcCost As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents FNNetProfitPer As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents FNNetProfit As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents FNNetProfitRcv As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents FNOrderQuantity As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents FNExportQuantityTo As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents FNExportQuantityOtherMonth As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents FNTotalExport As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents FNOrderQuantityBal As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents FNLostPer As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents CFNStateRow As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents FNPrice As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents FDInvoiceDate As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents FTCmpName As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents gridBand3 As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents gCmpName As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents gFTInvoice As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents gInvoiceDate As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents gFTOrderNo As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents gFTPORef As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents gFTStyle As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents gFNExportQuantity As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents gFNExportAmt As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents gFNExportAmtus As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents gCenter As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents gCenter1 As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents gCenter2 As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents gCenter3 As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents gCenter4 As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents gFNFabricAccMinCostPer As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents gFabricAccMin As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents gCenter5 As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents gCenter6 As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents cFNFabricAccStockOtherCostPer As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents gOtherStock As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents gFNOtherServiceCostPer As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents gOtherServiceCost As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents gFNIncomeAfterRawmaterialPer As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents gFNIncomeAferRawmaterial As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents gCenter7 As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents gCenter8 As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents gCenter9 As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents gCenter10 As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents gCenter11 As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents gCenter12 As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents gCenter13 As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents gCenter14 As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents gFNWagePullPer As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents gFNWagePull As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents gBtanch As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents gBtanch1 As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents gBtanch2 As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents gBtanch3 As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents gBtanch4 As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents gFNSewCostPer As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents gSewCost As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents gEmbroideryCostPer As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents gEmbroidCost As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents gFNPrintCostPer As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents gPrintCost As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents gBtanch5 As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents gBtanch6 As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents gFNImportCostPer As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents gImportCost As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents gBtanch7 As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents gBtanch8 As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents gBtanch9 As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents gBtanch10 As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents gBtanch11 As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents gBtanch12 As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents gBtanch13 As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents gBtanch14 As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents gEtcCostper As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents gEtcCost As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents gProfit As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents gProfit1 As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents gProfit2 As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents gFNNetProfitRcv As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents gFNOrderQuantity As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents gFNExport As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents gridBand1 As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents gridBand2 As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents รวม As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents gdiff As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents gridBand4 As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents gdiffper As DevExpress.XtraGrid.Views.BandedGrid.GridBand
End Class

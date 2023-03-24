Imports DevExpress.XtraEditors.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wAccJobCustSummaryBranch
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(wAccJobCustSummaryBranch))
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.ocmdoc = New DevExpress.XtraBars.Docking.DockManager()
        Me.ogbheader = New DevExpress.XtraBars.Docking.DockPanel()
        Me.DockPanel1_Container = New DevExpress.XtraBars.Docking.ControlContainer()
        Me.ProfitNet_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.ProfitBF_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.ogbmainprocbutton = New DevExpress.XtraEditors.PanelControl()
        Me.ocmSendApprove = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmsave = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmpreview = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmload = New DevExpress.XtraEditors.SimpleButton()
        Me.FTStateDirectorApp = New DevExpress.XtraEditors.CheckEdit()
        Me.FTStateApprovedApp = New DevExpress.XtraEditors.CheckEdit()
        Me.FTStateFactoryManagerApp = New DevExpress.XtraEditors.CheckEdit()
        Me.FTStateInspectorApp = New DevExpress.XtraEditors.CheckEdit()
        Me.FTStateSendApp = New DevExpress.XtraEditors.CheckEdit()
        Me.FNHSysCmpId_None = New DevExpress.XtraEditors.TextEdit()
        Me.SFTDateTrans = New DevExpress.XtraEditors.DateEdit()
        Me.FNHSysCmpId_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.SFTDateTrans_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysCmpId = New DevExpress.XtraEditors.ButtonEdit()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.comboChartType = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.checkShowPointLabels = New DevExpress.XtraEditors.CheckEdit()
        Me.ceChartDataVertical = New DevExpress.XtraEditors.CheckEdit()
        Me.ceSelectionOnly = New DevExpress.XtraEditors.CheckEdit()
        Me.ceShowColumnGrandTotals = New DevExpress.XtraEditors.CheckEdit()
        Me.ceShowRowGrandTotals = New DevExpress.XtraEditors.CheckEdit()
        Me.lblUpdateDelay = New DevExpress.XtraEditors.LabelControl()
        Me.seUpdateDelay = New DevExpress.XtraEditors.SpinEdit()
        Me.ogdtime = New DevExpress.XtraGrid.GridControl()
        Me.oAdvBandedGridView = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridView()
        Me.gFTInvoice = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.FTInvoiceNo = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
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
        Me.gCenter = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.gCenter1 = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.FNFabricCostPer = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.gCenter2 = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.FNFabricCost = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.gCenter3 = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.FNAccessroryCostPer = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.gCenter4 = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.FNAccessroryCost = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
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
        Me.FNExportAmt = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.gProfit29 = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.FNNetProfit = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.FNNetProfitByRcv = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.FNNetProfitRcv = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.gFNNetProfitRcv = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.FNNetProfitAct = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
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
        Me.gdiffper = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.FNLostPer = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.CFNStateRow = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.DockManager1 = New DevExpress.XtraBars.Docking.DockManager()
        CType(Me.ocmdoc, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbheader.SuspendLayout()
        Me.DockPanel1_Container.SuspendLayout()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbmainprocbutton.SuspendLayout()
        CType(Me.FTStateDirectorApp.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTStateApprovedApp.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTStateFactoryManagerApp.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTStateInspectorApp.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTStateSendApp.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysCmpId_None.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SFTDateTrans.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SFTDateTrans.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysCmpId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.comboChartType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.checkShowPointLabels.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ceChartDataVertical.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ceSelectionOnly.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ceShowColumnGrandTotals.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ceShowRowGrandTotals.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.seUpdateDelay.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogdtime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.oAdvBandedGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepFNIncomeAfterRawmaterial, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepFNWageCost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepFNProdCost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DockManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ocmdoc
        '
        Me.ocmdoc.Form = Me
        Me.ocmdoc.RootPanels.AddRange(New DevExpress.XtraBars.Docking.DockPanel() {Me.ogbheader})
        Me.ocmdoc.TopZIndexControls.AddRange(New String() {"DevExpress.XtraBars.BarDockControl", "DevExpress.XtraBars.StandaloneBarDockControl", "System.Windows.Forms.StatusBar", "System.Windows.Forms.MenuStrip", "System.Windows.Forms.StatusStrip", "DevExpress.XtraBars.Ribbon.RibbonStatusBar", "DevExpress.XtraBars.Ribbon.RibbonControl"})
        '
        'ogbheader
        '
        Me.ogbheader.Controls.Add(Me.DockPanel1_Container)
        Me.ogbheader.Dock = DevExpress.XtraBars.Docking.DockingStyle.Top
        Me.ogbheader.ID = New System.Guid("6a838d1f-4f6f-4734-9f6d-4c809fcfd587")
        Me.ogbheader.Image = CType(resources.GetObject("ogbheader.Image"), System.Drawing.Image)
        Me.ogbheader.Location = New System.Drawing.Point(0, 0)
        Me.ogbheader.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbheader.Name = "ogbheader"
        Me.ogbheader.Options.AllowDockBottom = False
        Me.ogbheader.Options.AllowDockFill = False
        Me.ogbheader.Options.AllowDockLeft = False
        Me.ogbheader.Options.AllowDockRight = False
        Me.ogbheader.Options.AllowFloating = False
        Me.ogbheader.Options.FloatOnDblClick = False
        Me.ogbheader.Options.ShowCloseButton = False
        Me.ogbheader.Options.ShowMaximizeButton = False
        Me.ogbheader.OriginalSize = New System.Drawing.Size(200, 128)
        Me.ogbheader.Size = New System.Drawing.Size(1195, 128)
        Me.ogbheader.Text = "Criteria"
        '
        'DockPanel1_Container
        '
        Me.DockPanel1_Container.Controls.Add(Me.ProfitNet_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.ProfitBF_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.ogbmainprocbutton)
        Me.DockPanel1_Container.Controls.Add(Me.FTStateDirectorApp)
        Me.DockPanel1_Container.Controls.Add(Me.FTStateApprovedApp)
        Me.DockPanel1_Container.Controls.Add(Me.FTStateFactoryManagerApp)
        Me.DockPanel1_Container.Controls.Add(Me.FTStateInspectorApp)
        Me.DockPanel1_Container.Controls.Add(Me.FTStateSendApp)
        Me.DockPanel1_Container.Controls.Add(Me.FNHSysCmpId_None)
        Me.DockPanel1_Container.Controls.Add(Me.SFTDateTrans)
        Me.DockPanel1_Container.Controls.Add(Me.FNHSysCmpId_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.SFTDateTrans_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FNHSysCmpId)
        Me.DockPanel1_Container.Location = New System.Drawing.Point(3, 32)
        Me.DockPanel1_Container.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.DockPanel1_Container.Name = "DockPanel1_Container"
        Me.DockPanel1_Container.Size = New System.Drawing.Size(1189, 91)
        Me.DockPanel1_Container.TabIndex = 0
        '
        'ProfitNet_lbl
        '
        Me.ProfitNet_lbl.Location = New System.Drawing.Point(461, 47)
        Me.ProfitNet_lbl.Name = "ProfitNet_lbl"
        Me.ProfitNet_lbl.Size = New System.Drawing.Size(97, 17)
        Me.ProfitNet_lbl.TabIndex = 508
        Me.ProfitNet_lbl.Text = "กำไรขาดทุน รวม"
        Me.ProfitNet_lbl.Visible = False
        '
        'ProfitBF_lbl
        '
        Me.ProfitBF_lbl.Location = New System.Drawing.Point(445, 39)
        Me.ProfitBF_lbl.Name = "ProfitBF_lbl"
        Me.ProfitBF_lbl.Size = New System.Drawing.Size(108, 17)
        Me.ProfitBF_lbl.TabIndex = 508
        Me.ProfitBF_lbl.Text = "กำไรขาดทุน สะสม"
        Me.ProfitBF_lbl.Visible = False
        '
        'ogbmainprocbutton
        '
        Me.ogbmainprocbutton.Controls.Add(Me.ocmSendApprove)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmsave)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmpreview)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmexit)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmload)
        Me.ogbmainprocbutton.Location = New System.Drawing.Point(539, 3)
        Me.ogbmainprocbutton.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbmainprocbutton.Name = "ogbmainprocbutton"
        Me.ogbmainprocbutton.Size = New System.Drawing.Size(608, 37)
        Me.ogbmainprocbutton.TabIndex = 391
        '
        'ocmSendApprove
        '
        Me.ocmSendApprove.Location = New System.Drawing.Point(381, 5)
        Me.ocmSendApprove.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmSendApprove.Name = "ocmSendApprove"
        Me.ocmSendApprove.Size = New System.Drawing.Size(111, 31)
        Me.ocmSendApprove.TabIndex = 98
        Me.ocmSendApprove.TabStop = False
        Me.ocmSendApprove.Tag = "2|"
        Me.ocmSendApprove.Text = "Send Approve"
        '
        'ocmsave
        '
        Me.ocmsave.Location = New System.Drawing.Point(281, 6)
        Me.ocmsave.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmsave.Name = "ocmsave"
        Me.ocmsave.Size = New System.Drawing.Size(111, 31)
        Me.ocmsave.TabIndex = 97
        Me.ocmsave.TabStop = False
        Me.ocmsave.Tag = "2|"
        Me.ocmsave.Text = "SAVE"
        '
        'ocmpreview
        '
        Me.ocmpreview.Location = New System.Drawing.Point(163, 6)
        Me.ocmpreview.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmpreview.Name = "ocmpreview"
        Me.ocmpreview.Size = New System.Drawing.Size(111, 31)
        Me.ocmpreview.TabIndex = 97
        Me.ocmpreview.TabStop = False
        Me.ocmpreview.Tag = "2|"
        Me.ocmpreview.Text = "PREVIEW"
        '
        'ocmexit
        '
        Me.ocmexit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmexit.Location = New System.Drawing.Point(422, 5)
        Me.ocmexit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmexit.Name = "ocmexit"
        Me.ocmexit.Size = New System.Drawing.Size(111, 31)
        Me.ocmexit.TabIndex = 96
        Me.ocmexit.TabStop = False
        Me.ocmexit.Tag = "2|"
        Me.ocmexit.Text = "EXIT"
        '
        'ocmload
        '
        Me.ocmload.Location = New System.Drawing.Point(20, 6)
        Me.ocmload.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmload.Name = "ocmload"
        Me.ocmload.Size = New System.Drawing.Size(111, 31)
        Me.ocmload.TabIndex = 93
        Me.ocmload.TabStop = False
        Me.ocmload.Tag = "2|"
        Me.ocmload.Text = "LoadData"
        '
        'FTStateDirectorApp
        '
        Me.FTStateDirectorApp.Location = New System.Drawing.Point(967, 61)
        Me.FTStateDirectorApp.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTStateDirectorApp.Name = "FTStateDirectorApp"
        Me.FTStateDirectorApp.Properties.Caption = "FTStateDirectorApp"
        Me.FTStateDirectorApp.Properties.ReadOnly = True
        Me.FTStateDirectorApp.Size = New System.Drawing.Size(255, 20)
        Me.FTStateDirectorApp.TabIndex = 507
        '
        'FTStateApprovedApp
        '
        Me.FTStateApprovedApp.Location = New System.Drawing.Point(706, 61)
        Me.FTStateApprovedApp.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTStateApprovedApp.Name = "FTStateApprovedApp"
        Me.FTStateApprovedApp.Properties.Caption = "FTStateApprovedApp"
        Me.FTStateApprovedApp.Properties.ReadOnly = True
        Me.FTStateApprovedApp.Size = New System.Drawing.Size(255, 20)
        Me.FTStateApprovedApp.TabIndex = 507
        '
        'FTStateFactoryManagerApp
        '
        Me.FTStateFactoryManagerApp.Location = New System.Drawing.Point(445, 61)
        Me.FTStateFactoryManagerApp.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTStateFactoryManagerApp.Name = "FTStateFactoryManagerApp"
        Me.FTStateFactoryManagerApp.Properties.Caption = "FTStateFactoryManagerApp"
        Me.FTStateFactoryManagerApp.Properties.ReadOnly = True
        Me.FTStateFactoryManagerApp.Size = New System.Drawing.Size(255, 20)
        Me.FTStateFactoryManagerApp.TabIndex = 507
        '
        'FTStateInspectorApp
        '
        Me.FTStateInspectorApp.Location = New System.Drawing.Point(154, 61)
        Me.FTStateInspectorApp.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTStateInspectorApp.Name = "FTStateInspectorApp"
        Me.FTStateInspectorApp.Properties.Caption = "FTStateInspectorApp"
        Me.FTStateInspectorApp.Properties.ReadOnly = True
        Me.FTStateInspectorApp.Size = New System.Drawing.Size(255, 20)
        Me.FTStateInspectorApp.TabIndex = 507
        '
        'FTStateSendApp
        '
        Me.FTStateSendApp.Location = New System.Drawing.Point(301, 30)
        Me.FTStateSendApp.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTStateSendApp.Name = "FTStateSendApp"
        Me.FTStateSendApp.Properties.Caption = "State Send Approve"
        Me.FTStateSendApp.Properties.ReadOnly = True
        Me.FTStateSendApp.Size = New System.Drawing.Size(255, 20)
        Me.FTStateSendApp.TabIndex = 507
        '
        'FNHSysCmpId_None
        '
        Me.FNHSysCmpId_None.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FNHSysCmpId_None.Location = New System.Drawing.Point(302, 4)
        Me.FNHSysCmpId_None.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysCmpId_None.Name = "FNHSysCmpId_None"
        Me.FNHSysCmpId_None.Properties.Appearance.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysCmpId_None.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysCmpId_None.Properties.ReadOnly = True
        Me.FNHSysCmpId_None.Size = New System.Drawing.Size(804, 23)
        Me.FNHSysCmpId_None.TabIndex = 505
        Me.FNHSysCmpId_None.Tag = ""
        '
        'SFTDateTrans
        '
        Me.SFTDateTrans.EditValue = Nothing
        Me.SFTDateTrans.EnterMoveNextControl = True
        Me.SFTDateTrans.Location = New System.Drawing.Point(155, 30)
        Me.SFTDateTrans.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.SFTDateTrans.Name = "SFTDateTrans"
        Me.SFTDateTrans.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.SFTDateTrans.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.SFTDateTrans.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.SFTDateTrans.Properties.DisplayFormat.FormatString = "MM/yyyy"
        Me.SFTDateTrans.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.SFTDateTrans.Properties.EditFormat.FormatString = "MM/yyyy"
        Me.SFTDateTrans.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.SFTDateTrans.Properties.Mask.EditMask = "MM/yyyy"
        Me.SFTDateTrans.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.SFTDateTrans.Properties.NullDate = ""
        Me.SFTDateTrans.Size = New System.Drawing.Size(140, 23)
        Me.SFTDateTrans.TabIndex = 395
        Me.SFTDateTrans.Tag = "2|"
        '
        'FNHSysCmpId_lbl
        '
        Me.FNHSysCmpId_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysCmpId_lbl.Appearance.Options.UseForeColor = True
        Me.FNHSysCmpId_lbl.Appearance.Options.UseTextOptions = True
        Me.FNHSysCmpId_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysCmpId_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysCmpId_lbl.Location = New System.Drawing.Point(7, 4)
        Me.FNHSysCmpId_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysCmpId_lbl.Name = "FNHSysCmpId_lbl"
        Me.FNHSysCmpId_lbl.Size = New System.Drawing.Size(143, 21)
        Me.FNHSysCmpId_lbl.TabIndex = 504
        Me.FNHSysCmpId_lbl.Tag = "2|"
        Me.FNHSysCmpId_lbl.Text = "Company :"
        '
        'SFTDateTrans_lbl
        '
        Me.SFTDateTrans_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.SFTDateTrans_lbl.Appearance.Options.UseForeColor = True
        Me.SFTDateTrans_lbl.Appearance.Options.UseTextOptions = True
        Me.SFTDateTrans_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.SFTDateTrans_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.SFTDateTrans_lbl.Location = New System.Drawing.Point(23, 30)
        Me.SFTDateTrans_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.SFTDateTrans_lbl.Name = "SFTDateTrans_lbl"
        Me.SFTDateTrans_lbl.Size = New System.Drawing.Size(128, 25)
        Me.SFTDateTrans_lbl.TabIndex = 396
        Me.SFTDateTrans_lbl.Tag = "2|"
        Me.SFTDateTrans_lbl.Text = "ประจำเดือน :"
        '
        'FNHSysCmpId
        '
        Me.FNHSysCmpId.Location = New System.Drawing.Point(154, 4)
        Me.FNHSysCmpId.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysCmpId.Name = "FNHSysCmpId"
        Me.FNHSysCmpId.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysCmpId.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysCmpId.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNHSysCmpId.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNHSysCmpId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", "11", Nothing, True)})
        Me.FNHSysCmpId.Properties.Tag = ""
        Me.FNHSysCmpId.Size = New System.Drawing.Size(141, 23)
        Me.FNHSysCmpId.TabIndex = 503
        Me.FNHSysCmpId.Tag = ""
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Options.UseTextOptions = True
        Me.LabelControl1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LabelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LabelControl1.Location = New System.Drawing.Point(16, 15)
        Me.LabelControl1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(146, 21)
        Me.LabelControl1.TabIndex = 2
        '
        'comboChartType
        '
        Me.comboChartType.EditValue = "Line"
        Me.comboChartType.Location = New System.Drawing.Point(169, 12)
        Me.comboChartType.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.comboChartType.Name = "comboChartType"
        Me.comboChartType.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.comboChartType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.comboChartType.Size = New System.Drawing.Size(180, 23)
        Me.comboChartType.TabIndex = 3
        '
        'checkShowPointLabels
        '
        Me.checkShowPointLabels.Location = New System.Drawing.Point(356, 12)
        Me.checkShowPointLabels.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.checkShowPointLabels.Name = "checkShowPointLabels"
        Me.checkShowPointLabels.Properties.AutoWidth = True
        Me.checkShowPointLabels.Properties.Caption = "Show Point Labels"
        Me.checkShowPointLabels.Size = New System.Drawing.Size(127, 20)
        Me.checkShowPointLabels.TabIndex = 4
        Me.checkShowPointLabels.ToolTip = "Toggles whether value labels are shown in the Chart control"
        '
        'ceChartDataVertical
        '
        Me.ceChartDataVertical.Location = New System.Drawing.Point(356, 39)
        Me.ceChartDataVertical.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ceChartDataVertical.Name = "ceChartDataVertical"
        Me.ceChartDataVertical.Properties.AutoWidth = True
        Me.ceChartDataVertical.Properties.Caption = "Generate Series from Columns"
        Me.ceChartDataVertical.Size = New System.Drawing.Size(199, 20)
        Me.ceChartDataVertical.TabIndex = 12
        Me.ceChartDataVertical.ToolTip = "Toggles whether series in a chart control are created based on PivotGrid columns " &
    "or rows"
        '
        'ceSelectionOnly
        '
        Me.ceSelectionOnly.Location = New System.Drawing.Point(12, 39)
        Me.ceSelectionOnly.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ceSelectionOnly.Name = "ceSelectionOnly"
        Me.ceSelectionOnly.Properties.AutoWidth = True
        Me.ceSelectionOnly.Properties.Caption = "Selection Only"
        Me.ceSelectionOnly.Size = New System.Drawing.Size(104, 20)
        Me.ceSelectionOnly.TabIndex = 9
        Me.ceSelectionOnly.ToolTip = "Toggles whether all PivotGrid cells or selected cells only should be represented " &
    "in the Chart"
        '
        'ceShowColumnGrandTotals
        '
        Me.ceShowColumnGrandTotals.Location = New System.Drawing.Point(559, 39)
        Me.ceShowColumnGrandTotals.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ceShowColumnGrandTotals.Name = "ceShowColumnGrandTotals"
        Me.ceShowColumnGrandTotals.Properties.AutoWidth = True
        Me.ceShowColumnGrandTotals.Properties.Caption = "Show Column Grand Totals"
        Me.ceShowColumnGrandTotals.Size = New System.Drawing.Size(179, 20)
        Me.ceShowColumnGrandTotals.TabIndex = 13
        Me.ceShowColumnGrandTotals.ToolTip = "Toggles whether column grand total values are shown in the Chart control"
        '
        'ceShowRowGrandTotals
        '
        Me.ceShowRowGrandTotals.EditValue = True
        Me.ceShowRowGrandTotals.Location = New System.Drawing.Point(559, 12)
        Me.ceShowRowGrandTotals.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ceShowRowGrandTotals.Name = "ceShowRowGrandTotals"
        Me.ceShowRowGrandTotals.Properties.AutoWidth = True
        Me.ceShowRowGrandTotals.Properties.Caption = "Show Row Grand Totals"
        Me.ceShowRowGrandTotals.Size = New System.Drawing.Size(161, 20)
        Me.ceShowRowGrandTotals.TabIndex = 7
        Me.ceShowRowGrandTotals.ToolTip = "Toggles whether row grand total values are shown in the Chart control"
        '
        'lblUpdateDelay
        '
        Me.lblUpdateDelay.Location = New System.Drawing.Point(203, 43)
        Me.lblUpdateDelay.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.lblUpdateDelay.Name = "lblUpdateDelay"
        Me.lblUpdateDelay.Size = New System.Drawing.Size(80, 16)
        Me.lblUpdateDelay.TabIndex = 13
        '
        'seUpdateDelay
        '
        Me.seUpdateDelay.EditValue = New Decimal(New Integer() {500, 0, 0, 0})
        Me.seUpdateDelay.Location = New System.Drawing.Point(293, 39)
        Me.seUpdateDelay.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.seUpdateDelay.Name = "seUpdateDelay"
        Me.seUpdateDelay.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.seUpdateDelay.Properties.Increment = New Decimal(New Integer() {100, 0, 0, 0})
        Me.seUpdateDelay.Properties.IsFloatValue = False
        Me.seUpdateDelay.Properties.Mask.EditMask = "N00"
        Me.seUpdateDelay.Properties.MaxValue = New Decimal(New Integer() {5000, 0, 0, 0})
        Me.seUpdateDelay.Size = New System.Drawing.Size(56, 26)
        Me.seUpdateDelay.TabIndex = 10
        Me.seUpdateDelay.ToolTip = "Sets the Chart update delay when PivotGrid selection changes."
        '
        'ogdtime
        '
        Me.ogdtime.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogdtime.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogdtime.Location = New System.Drawing.Point(0, 128)
        Me.ogdtime.MainView = Me.oAdvBandedGridView
        Me.ogdtime.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogdtime.Name = "ogdtime"
        Me.ogdtime.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepFNIncomeAfterRawmaterial, Me.RepFNProdCost, Me.RepFNWageCost})
        Me.ogdtime.Size = New System.Drawing.Size(1195, 575)
        Me.ogdtime.TabIndex = 394
        Me.ogdtime.TabStop = False
        Me.ogdtime.Tag = "2|"
        Me.ogdtime.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.oAdvBandedGridView})
        '
        'oAdvBandedGridView
        '
        Me.oAdvBandedGridView.Bands.AddRange(New DevExpress.XtraGrid.Views.BandedGrid.GridBand() {Me.gFTInvoice, Me.gFTOrderNo, Me.gFTPORef, Me.gFTStyle, Me.gFNExportQuantity, Me.gFNExportAmt, Me.gCenter, Me.gBtanch, Me.gProfit, Me.gFNOrderQuantity, Me.gFNExport, Me.gdiff, Me.gdiffper})
        Me.oAdvBandedGridView.Columns.AddRange(New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn() {Me.FTInvoiceNo, Me.FTOrderNo, Me.FTPORef, Me.FTStyleCode, Me.FNExportQuantity, Me.FNExportAmtTHB, Me.FNExportAmt, Me.FNFabricCostPer, Me.FNWagePullPer, Me.FNWagePull, Me.FNNetProfitRcv, Me.FNFabricCost, Me.FNAccessroryCostPer, Me.FNAccessroryCost, Me.FNAccFabStockCostPer, Me.FNAccFabStockCost, Me.FNConductedCostPer, Me.FNConductedCost, Me.FNOtherCostPer, Me.FNOtherCost, Me.FNEmbFacCostPer, Me.FNEmbFacCost, Me.FNEmpPrintBranchPer, Me.FNEmpPrintBranch, Me.FNExportAmtBFPer, Me.FNExportAmtBF, Me.FNWageCostPer, Me.FNWageCost, Me.FNEmpPrintSubPer, Me.FNEmpPrintSub, Me.FNImportExportCostPer, Me.FNImportExportCost, Me.FNProdCostPer, Me.FNProdCost, Me.FNCommissionCostPer, Me.FNCommissionCost, Me.FNTransportAirCostPer, Me.FNTransportAirCost, Me.FNNetProfitPer, Me.FNNetProfit, Me.FNOrderQuantity, Me.FNExportQuantityTo, Me.FNExportQuantityOtherMonth, Me.FNTotalExport, Me.FNOrderQuantityBal, Me.FNLostPer, Me.FNFabricAccMinCost, Me.FNFabricAccStockOtherCost, Me.FNOtherServiceCost, Me.FNSewCost, Me.FNEmbroideryCost, Me.FNPrintCost, Me.FNImportCost, Me.FNEtcCost, Me.FNIncomeAfterRawmaterial, Me.FNFabricAccMinCostPer, Me.FNFabricAccStockOtherCostPer, Me.FNOtherServiceCostPer, Me.FNSewCostPer, Me.FNEmbroideryCostPer, Me.FNPrintCostPer, Me.FNImportCostPer, Me.FNEtcCostPer, Me.FNIncomeAfterRawmaterialPer, Me.CFNStateRow, Me.FNNetProfitAct})
        Me.oAdvBandedGridView.GridControl = Me.ogdtime
        Me.oAdvBandedGridView.Name = "oAdvBandedGridView"
        Me.oAdvBandedGridView.OptionsCustomization.AllowQuickHideColumns = False
        Me.oAdvBandedGridView.OptionsPrint.PrintHeader = False
        Me.oAdvBandedGridView.OptionsView.ColumnAutoWidth = False
        Me.oAdvBandedGridView.OptionsView.ShowColumnHeaders = False
        Me.oAdvBandedGridView.OptionsView.ShowGroupPanel = False
        '
        'gFTInvoice
        '
        Me.gFTInvoice.AppearanceHeader.Options.UseTextOptions = True
        Me.gFTInvoice.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gFTInvoice.Caption = "INVOICE"
        Me.gFTInvoice.Columns.Add(Me.FTInvoiceNo)
        Me.gFTInvoice.Name = "gFTInvoice"
        Me.gFTInvoice.RowCount = 2
        Me.gFTInvoice.VisibleIndex = 0
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
        'gFTOrderNo
        '
        Me.gFTOrderNo.AppearanceHeader.Options.UseTextOptions = True
        Me.gFTOrderNo.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gFTOrderNo.Caption = "FO. No."
        Me.gFTOrderNo.Columns.Add(Me.FTOrderNo)
        Me.gFTOrderNo.Name = "gFTOrderNo"
        Me.gFTOrderNo.RowCount = 2
        Me.gFTOrderNo.VisibleIndex = 1
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
        Me.gFTPORef.VisibleIndex = 2
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
        Me.gFTStyle.VisibleIndex = 3
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
        Me.gFNExportQuantity.VisibleIndex = 4
        Me.gFNExportQuantity.Width = 100
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
        Me.FNExportQuantity.Width = 100
        '
        'gFNExportAmt
        '
        Me.gFNExportAmt.AppearanceHeader.Options.UseTextOptions = True
        Me.gFNExportAmt.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gFNExportAmt.Caption = "มูลค่าส่งออก"
        Me.gFNExportAmt.Columns.Add(Me.FNExportAmtTHB)
        Me.gFNExportAmt.Name = "gFNExportAmt"
        Me.gFNExportAmt.RowCount = 2
        Me.gFNExportAmt.VisibleIndex = 5
        Me.gFNExportAmt.Width = 124
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
        Me.FNExportAmtTHB.Width = 124
        '
        'gCenter
        '
        Me.gCenter.AppearanceHeader.Options.UseTextOptions = True
        Me.gCenter.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gCenter.Caption = "ส่วนกลาง"
        Me.gCenter.Children.AddRange(New DevExpress.XtraGrid.Views.BandedGrid.GridBand() {Me.gCenter1, Me.gCenter2, Me.gCenter3, Me.gCenter4, Me.gFNFabricAccMinCostPer, Me.gFabricAccMin, Me.gCenter5, Me.gCenter6, Me.cFNFabricAccStockOtherCostPer, Me.gOtherStock, Me.gFNOtherServiceCostPer, Me.gOtherServiceCost, Me.gFNIncomeAfterRawmaterialPer, Me.gFNIncomeAferRawmaterial, Me.gCenter7, Me.gCenter8, Me.gCenter9, Me.gCenter10, Me.gCenter11, Me.gCenter12, Me.gCenter13, Me.gCenter14, Me.gFNWagePullPer, Me.gFNWagePull})
        Me.gCenter.Name = "gCenter"
        Me.gCenter.VisibleIndex = 6
        Me.gCenter.Width = 1812
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
        Me.gCenter2.Width = 100
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
        Me.FNFabricCost.Width = 100
        '
        'gCenter3
        '
        Me.gCenter3.AppearanceHeader.Options.UseTextOptions = True
        Me.gCenter3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gCenter3.Caption = "%"
        Me.gCenter3.Columns.Add(Me.FNAccessroryCostPer)
        Me.gCenter3.Name = "gCenter3"
        Me.gCenter3.VisibleIndex = 2
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
        Me.gCenter4.Columns.Add(Me.FNAccessroryCost)
        Me.gCenter4.Name = "gCenter4"
        Me.gCenter4.VisibleIndex = 3
        Me.gCenter4.Width = 100
        '
        'FNAccessroryCost
        '
        Me.FNAccessroryCost.AppearanceCell.Options.UseTextOptions = True
        Me.FNAccessroryCost.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNAccessroryCost.Caption = "FNAccessroryCost"
        Me.FNAccessroryCost.DisplayFormat.FormatString = "{0:n2}"
        Me.FNAccessroryCost.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNAccessroryCost.FieldName = "FNAccessroryCost"
        Me.FNAccessroryCost.Name = "FNAccessroryCost"
        Me.FNAccessroryCost.OptionsColumn.AllowEdit = False
        Me.FNAccessroryCost.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNAccessroryCost.OptionsColumn.ReadOnly = True
        Me.FNAccessroryCost.Visible = True
        Me.FNAccessroryCost.Width = 100
        '
        'gFNFabricAccMinCostPer
        '
        Me.gFNFabricAccMinCostPer.Caption = "%"
        Me.gFNFabricAccMinCostPer.Columns.Add(Me.FNFabricAccMinCostPer)
        Me.gFNFabricAccMinCostPer.Name = "gFNFabricAccMinCostPer"
        Me.gFNFabricAccMinCostPer.VisibleIndex = 4
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
        Me.gFabricAccMin.VisibleIndex = 5
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
        Me.gCenter5.VisibleIndex = 6
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
        Me.gCenter6.VisibleIndex = 7
        Me.gCenter6.Width = 100
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
        Me.FNAccFabStockCost.Visible = True
        Me.FNAccFabStockCost.Width = 100
        '
        'cFNFabricAccStockOtherCostPer
        '
        Me.cFNFabricAccStockOtherCostPer.Caption = "%"
        Me.cFNFabricAccStockOtherCostPer.Columns.Add(Me.FNFabricAccStockOtherCostPer)
        Me.cFNFabricAccStockOtherCostPer.Name = "cFNFabricAccStockOtherCostPer"
        Me.cFNFabricAccStockOtherCostPer.VisibleIndex = 8
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
        Me.gOtherStock.VisibleIndex = 9
        Me.gOtherStock.Width = 92
        '
        'FNFabricAccStockOtherCost
        '
        Me.FNFabricAccStockOtherCost.Caption = "FNFabricAccStockOtherCost"
        Me.FNFabricAccStockOtherCost.DisplayFormat.FormatString = "{0:n2}"
        Me.FNFabricAccStockOtherCost.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNFabricAccStockOtherCost.FieldName = "FNFabricAccStockOtherCost"
        Me.FNFabricAccStockOtherCost.Name = "FNFabricAccStockOtherCost"
        Me.FNFabricAccStockOtherCost.OptionsColumn.AllowEdit = False
        Me.FNFabricAccStockOtherCost.Visible = True
        Me.FNFabricAccStockOtherCost.Width = 92
        '
        'gFNOtherServiceCostPer
        '
        Me.gFNOtherServiceCostPer.Caption = "%"
        Me.gFNOtherServiceCostPer.Columns.Add(Me.FNOtherServiceCostPer)
        Me.gFNOtherServiceCostPer.Name = "gFNOtherServiceCostPer"
        Me.gFNOtherServiceCostPer.VisibleIndex = 10
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
        Me.gOtherServiceCost.VisibleIndex = 11
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
        Me.gFNIncomeAfterRawmaterialPer.VisibleIndex = 12
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
        Me.gFNIncomeAferRawmaterial.VisibleIndex = 13
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
        Me.gCenter7.VisibleIndex = 14
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
        Me.gCenter8.VisibleIndex = 15
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
        Me.gCenter11.VisibleIndex = 16
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
        Me.gCenter12.VisibleIndex = 17
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
        Me.gCenter13.VisibleIndex = 18
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
        Me.gCenter14.VisibleIndex = 19
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
        Me.gFNWagePullPer.VisibleIndex = 20
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
        Me.gFNWagePull.VisibleIndex = 21
        Me.gFNWagePull.Width = 75
        '
        'FNWagePull
        '
        Me.FNWagePull.Caption = "FNWagePull"
        Me.FNWagePull.DisplayFormat.FormatString = "{0:n2}"
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
        Me.gBtanch.VisibleIndex = 7
        Me.gBtanch.Width = 1797
        '
        'gBtanch1
        '
        Me.gBtanch1.AppearanceHeader.Options.UseTextOptions = True
        Me.gBtanch1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gBtanch1.Caption = "%"
        Me.gBtanch1.Columns.Add(Me.FNExportAmtBFPer)
        Me.gBtanch1.Name = "gBtanch1"
        Me.gBtanch1.VisibleIndex = 0
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
        Me.gBtanch2.VisibleIndex = 1
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
        Me.gBtanch3.VisibleIndex = 2
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
        Me.gBtanch4.VisibleIndex = 3
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
        Me.gFNSewCostPer.VisibleIndex = 4
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
        Me.gSewCost.VisibleIndex = 5
        Me.gSewCost.Width = 101
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
        Me.FNSewCost.Width = 101
        '
        'gEmbroideryCostPer
        '
        Me.gEmbroideryCostPer.Caption = "%"
        Me.gEmbroideryCostPer.Columns.Add(Me.FNEmbroideryCostPer)
        Me.gEmbroideryCostPer.Name = "gEmbroideryCostPer"
        Me.gEmbroideryCostPer.VisibleIndex = 6
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
        Me.gEmbroidCost.VisibleIndex = 7
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
        Me.gFNPrintCostPer.VisibleIndex = 8
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
        Me.gPrintCost.VisibleIndex = 9
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
        Me.gBtanch5.VisibleIndex = 10
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
        Me.gBtanch6.VisibleIndex = 11
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
        Me.gFNImportCostPer.VisibleIndex = 12
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
        Me.gImportCost.VisibleIndex = 13
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
        Me.gBtanch7.VisibleIndex = 14
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
        Me.gBtanch8.VisibleIndex = 15
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
        Me.gBtanch9.VisibleIndex = 16
        Me.gBtanch9.Width = 50
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
        Me.FNProdCostPer.Width = 50
        '
        'gBtanch10
        '
        Me.gBtanch10.AppearanceHeader.Options.UseTextOptions = True
        Me.gBtanch10.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gBtanch10.Caption = "คชจ. ผลิต"
        Me.gBtanch10.Columns.Add(Me.FNProdCost)
        Me.gBtanch10.Name = "gBtanch10"
        Me.gBtanch10.VisibleIndex = 17
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
        Me.gBtanch11.VisibleIndex = 18
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
        Me.gBtanch12.VisibleIndex = 19
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
        Me.gBtanch13.VisibleIndex = 20
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
        Me.gBtanch14.VisibleIndex = 21
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
        Me.gEtcCostper.VisibleIndex = 22
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
        Me.gEtcCost.VisibleIndex = 23
        Me.gEtcCost.Width = 75
        '
        'FNEtcCost
        '
        Me.FNEtcCost.Caption = "FNEtcCost"
        Me.FNEtcCost.ColumnEdit = Me.RepFNProdCost
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
        Me.gProfit.Children.AddRange(New DevExpress.XtraGrid.Views.BandedGrid.GridBand() {Me.gProfit1, Me.gProfit29, Me.FNNetProfitByRcv, Me.gFNNetProfitRcv})
        Me.gProfit.Name = "gProfit"
        Me.gProfit.VisibleIndex = 8
        Me.gProfit.Width = 582
        '
        'gProfit1
        '
        Me.gProfit1.AppearanceHeader.Options.UseTextOptions = True
        Me.gProfit1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gProfit1.Caption = "%"
        Me.gProfit1.Columns.Add(Me.FNNetProfitPer)
        Me.gProfit1.Columns.Add(Me.FNExportAmt)
        Me.gProfit1.Name = "gProfit1"
        Me.gProfit1.VisibleIndex = 0
        Me.gProfit1.Width = 232
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
        Me.FNNetProfitPer.Width = 232
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
        Me.FNExportAmt.Width = 101
        '
        'gProfit29
        '
        Me.gProfit29.AppearanceHeader.Options.UseTextOptions = True
        Me.gProfit29.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gProfit29.Caption = "มูลค่า"
        Me.gProfit29.Columns.Add(Me.FNNetProfit)
        Me.gProfit29.Name = "gProfit29"
        Me.gProfit29.VisibleIndex = 1
        Me.gProfit29.Width = 147
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
        Me.FNNetProfit.Visible = True
        Me.FNNetProfit.Width = 147
        '
        'FNNetProfitByRcv
        '
        Me.FNNetProfitByRcv.Caption = "FNNetProfitByRcv"
        Me.FNNetProfitByRcv.Columns.Add(Me.FNNetProfitRcv)
        Me.FNNetProfitByRcv.Name = "FNNetProfitByRcv"
        Me.FNNetProfitByRcv.VisibleIndex = 2
        Me.FNNetProfitByRcv.Width = 95
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
        Me.FNNetProfitRcv.Width = 95
        '
        'gFNNetProfitRcv
        '
        Me.gFNNetProfitRcv.Caption = "FNNetProfitRcv"
        Me.gFNNetProfitRcv.Columns.Add(Me.FNNetProfitAct)
        Me.gFNNetProfitRcv.Name = "gFNNetProfitRcv"
        Me.gFNNetProfitRcv.VisibleIndex = 3
        Me.gFNNetProfitRcv.Width = 108
        '
        'FNNetProfitAct
        '
        Me.FNNetProfitAct.Caption = "FNNetProfitAct"
        Me.FNNetProfitAct.DisplayFormat.FormatString = "{0:n2}"
        Me.FNNetProfitAct.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNNetProfitAct.FieldName = "FNNetProfitAct"
        Me.FNNetProfitAct.Name = "FNNetProfitAct"
        Me.FNNetProfitAct.OptionsColumn.AllowEdit = False
        Me.FNNetProfitAct.OptionsColumn.ReadOnly = True
        Me.FNNetProfitAct.Visible = True
        Me.FNNetProfitAct.Width = 108
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
        Me.gFNOrderQuantity.VisibleIndex = 9
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
        Me.gFNExport.VisibleIndex = 10
        Me.gFNExport.Width = 371
        '
        'gridBand1
        '
        Me.gridBand1.AppearanceHeader.Options.UseTextOptions = True
        Me.gridBand1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gridBand1.Caption = "ตัว"
        Me.gridBand1.Columns.Add(Me.FNExportQuantityTo)
        Me.gridBand1.Name = "gridBand1"
        Me.gridBand1.VisibleIndex = 0
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
        Me.gridBand2.VisibleIndex = 1
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
        Me.รวม.VisibleIndex = 2
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
        Me.gdiff.VisibleIndex = 11
        Me.gdiff.Width = 109
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
        Me.FNOrderQuantityBal.Width = 109
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
        Me.gdiffper.VisibleIndex = 12
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
        'DockManager1
        '
        Me.DockManager1.Form = Me
        Me.DockManager1.TopZIndexControls.AddRange(New String() {"DevExpress.XtraBars.BarDockControl", "DevExpress.XtraBars.StandaloneBarDockControl", "System.Windows.Forms.StatusBar", "System.Windows.Forms.MenuStrip", "System.Windows.Forms.StatusStrip", "DevExpress.XtraBars.Ribbon.RibbonStatusBar", "DevExpress.XtraBars.Ribbon.RibbonControl"})
        '
        'wAccJobCustSummaryBranch
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1195, 703)
        Me.Controls.Add(Me.ogdtime)
        Me.Controls.Add(Me.ogbheader)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "wAccJobCustSummaryBranch"
        Me.Text = "สรุปต้นทุนประจำเดือน"
        CType(Me.ocmdoc, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbheader.ResumeLayout(False)
        Me.DockPanel1_Container.ResumeLayout(False)
        Me.DockPanel1_Container.PerformLayout()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbmainprocbutton.ResumeLayout(False)
        CType(Me.FTStateDirectorApp.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTStateApprovedApp.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTStateFactoryManagerApp.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTStateInspectorApp.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTStateSendApp.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysCmpId_None.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SFTDateTrans.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SFTDateTrans.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysCmpId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.comboChartType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.checkShowPointLabels.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ceChartDataVertical.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ceSelectionOnly.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ceShowColumnGrandTotals.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ceShowRowGrandTotals.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.seUpdateDelay.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogdtime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.oAdvBandedGridView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepFNIncomeAfterRawmaterial, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepFNWageCost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepFNProdCost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DockManager1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ocmdoc As DevExpress.XtraBars.Docking.DockManager
    Friend WithEvents ogbheader As DevExpress.XtraBars.Docking.DockPanel
    Friend WithEvents DockPanel1_Container As DevExpress.XtraBars.Docking.ControlContainer
    Friend WithEvents FNHSysCmpId_None As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FNHSysCmpId_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysCmpId As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents ogbmainprocbutton As DevExpress.XtraEditors.PanelControl
    Friend WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmload As DevExpress.XtraEditors.SimpleButton
    Private WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Private WithEvents comboChartType As DevExpress.XtraEditors.ComboBoxEdit
    Private WithEvents checkShowPointLabels As DevExpress.XtraEditors.CheckEdit
    Private WithEvents ceChartDataVertical As DevExpress.XtraEditors.CheckEdit
    Private WithEvents ceSelectionOnly As DevExpress.XtraEditors.CheckEdit
    Private WithEvents ceShowColumnGrandTotals As DevExpress.XtraEditors.CheckEdit
    Private WithEvents ceShowRowGrandTotals As DevExpress.XtraEditors.CheckEdit
    Private WithEvents lblUpdateDelay As DevExpress.XtraEditors.LabelControl
    Private WithEvents seUpdateDelay As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents SFTDateTrans As DevExpress.XtraEditors.DateEdit
    Friend WithEvents SFTDateTrans_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ogdtime As DevExpress.XtraGrid.GridControl
    Friend WithEvents ocmpreview As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents oAdvBandedGridView As DevExpress.XtraGrid.Views.BandedGrid.BandedGridView
    Friend WithEvents FTInvoiceNo As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents FTOrderNo As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents FTPORef As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents FTStyleCode As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents FNExportQuantity As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents FNExportAmtTHB As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents FNFabricCostPer As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents FNFabricCost As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents FNAccessroryCostPer As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents FNAccessroryCost As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents FNAccFabStockCostPer As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents FNAccFabStockCost As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents FNConductedCostPer As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents FNConductedCost As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents FNOtherCostPer As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents FNOtherCost As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents FNEmbFacCostPer As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents FNEmbFacCost As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents FNEmpPrintBranchPer As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents FNEmpPrintBranch As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents FNExportAmtBFPer As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents FNExportAmtBF As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents FNWageCostPer As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents FNWageCost As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents FNEmpPrintSubPer As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents FNEmpPrintSub As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents FNImportExportCostPer As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents FNImportExportCost As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents FNProdCostPer As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents FNProdCost As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents FNCommissionCostPer As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents FNCommissionCost As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents FNTransportAirCostPer As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents FNTransportAirCost As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents FNNetProfitPer As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents FNNetProfit As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents FNOrderQuantity As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents FNExportQuantityTo As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents FNExportQuantityOtherMonth As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents FNTotalExport As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents FNOrderQuantityBal As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents FNLostPer As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents FNFabricAccMinCost As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents FNFabricAccStockOtherCost As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents FNOtherServiceCost As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents FNSewCost As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents FNEmbroideryCost As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents FNPrintCost As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents FNImportCost As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents FNEtcCost As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents DockManager1 As DevExpress.XtraBars.Docking.DockManager
    Friend WithEvents FNFabricAccMinCostPer As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents FNFabricAccStockOtherCostPer As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents FNOtherServiceCostPer As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents FNIncomeAfterRawmaterialPer As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents FNIncomeAfterRawmaterial As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents FNSewCostPer As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents FNEmbroideryCostPer As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents FNPrintCostPer As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents FNImportCostPer As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents FNEtcCostPer As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents ocmsave As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents CFNStateRow As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents RepFNIncomeAfterRawmaterial As DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit
    Friend WithEvents RepFNWageCost As DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit
    Friend WithEvents RepFNProdCost As DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit
    Friend WithEvents ocmSendApprove As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FTStateSendApp As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents FNExportAmt As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents FNWagePull As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents FNWagePullPer As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents FNNetProfitRcv As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents ProfitNet_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ProfitBF_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTStateDirectorApp As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents FTStateApprovedApp As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents FTStateFactoryManagerApp As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents FTStateInspectorApp As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents gFTInvoice As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents gFTOrderNo As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents gFTPORef As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents gFTStyle As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents gFNExportQuantity As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents gFNExportAmt As DevExpress.XtraGrid.Views.BandedGrid.GridBand
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
    Friend WithEvents gProfit29 As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents FNNetProfitByRcv As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents gFNNetProfitRcv As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents FNNetProfitAct As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents gFNOrderQuantity As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents gFNExport As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents gridBand1 As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents gridBand2 As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents รวม As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents gdiff As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents gdiffper As DevExpress.XtraGrid.Views.BandedGrid.GridBand
End Class

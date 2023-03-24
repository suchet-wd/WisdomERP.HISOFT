<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wFactoryManagerApproved
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
        Me.MainRibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.mnusysabout = New DevExpress.XtraBars.BarButtonItem()
        Me.FTUserLogINIP = New DevExpress.XtraBars.BarStaticItem()
        Me.RepositoryItemComboBox1 = New DevExpress.XtraEditors.Repository.RepositoryItemComboBox()
        Me.RibbonStatusBar = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.MainDefaultLookAndFeel = New DevExpress.LookAndFeel.DefaultLookAndFeel(Me.components)
        Me.oDockManager = New DevExpress.XtraBars.Docking.DockManager(Me.components)
        Me.StandaloneBarDockControl = New DevExpress.XtraBars.StandaloneBarDockControl()
        Me.ogbmainprocbutton = New DevExpress.XtraEditors.PanelControl()
        Me.ocmsavelayout = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmpreview = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmreject = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmapprove = New DevExpress.XtraEditors.SimpleButton()
        Me.otmchkpo = New System.Windows.Forms.Timer(Me.components)
        Me.otbmain = New DevExpress.XtraTab.XtraTabControl()
        Me.otpappfactoryCM = New DevExpress.XtraTab.XtraTabPage()
        Me.GroupControl3 = New DevExpress.XtraEditors.GroupControl()
        Me.ogcAppFactoryCM = New DevExpress.XtraGrid.GridControl()
        Me.ogvAppFactoryCM = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cmFTSelect = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCheckEdit3 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.cmFTCmpCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cmFTCompName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cmFTCmpCodeTo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cmFTCompNameTo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cmFTPORef = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn11 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cmFTStyleCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cmFTOrderNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn14 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cmFTCustCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cmFTCustName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn17 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cmFTBuyCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn19 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cmFTSeasonCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn21 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cmFTProdTypeCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cmFTProdTypeName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cmFDShipDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cmFNQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemTextEdit3 = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.cmFNAmt = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cmFNExtraQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cmFNAmntExtra = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cmFNGarmentQtyTest = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cmFNAmntQtyTest = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cmFTNikePOLineItem = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cmFNTotalQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cmFNSeq = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ockAppCMselectall = New DevExpress.XtraEditors.CheckEdit()
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
        Me.CFTColorway = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFTNikePOLineItem = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFTSizeBreakDown = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNCM = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.C2FNTotalQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNGrandAmnt = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNGrandAmtCM = New DevExpress.XtraGrid.Columns.GridColumn()
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
        Me.ockdirectorselectall = New DevExpress.XtraEditors.CheckEdit()
        Me.otpappcminv = New DevExpress.XtraTab.XtraTabPage()
        Me.GroupControl2 = New DevExpress.XtraEditors.GroupControl()
        Me.ogccminv = New DevExpress.XtraGrid.GridControl()
        Me.ogvcminv = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCheckEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.cFTCmpCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTCompName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTCustomerPO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn7 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTStyleCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTInvoiceNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFDInvoiceDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.C2FTColorway = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.C2FTNikePOLineItem = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.C2FTSizeBreakDown = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNCM = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemTextEdit2 = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.cFNAmount = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTStateSendBy = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTSelectAllCMInv = New DevExpress.XtraEditors.CheckEdit()
        Me.otppurchasefacapp = New DevExpress.XtraTab.XtraTabPage()
        Me.ogSupervisorApproved = New DevExpress.XtraGrid.GridControl()
        Me.ogvSupervisorApproved = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.ColFTStateApproved = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCheckEdit2 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.ColFTPurchaseBy = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColFTSuperVisorName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
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
        CType(Me.MainRibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemComboBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.oDockManager, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbmainprocbutton.SuspendLayout()
        CType(Me.otbmain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.otbmain.SuspendLayout()
        Me.otpappfactoryCM.SuspendLayout()
        CType(Me.GroupControl3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl3.SuspendLayout()
        CType(Me.ogcAppFactoryCM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvAppFactoryCM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemTextEdit3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ockAppCMselectall.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.otpappfactory.SuspendLayout()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.ogcdirector, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvdirector, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositorySelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ockdirectorselectall.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.otpappcminv.SuspendLayout()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl2.SuspendLayout()
        CType(Me.ogccminv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvcminv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemTextEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTSelectAllCMInv.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.otppurchasefacapp.SuspendLayout()
        CType(Me.ogSupervisorApproved, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvSupervisorApproved, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'MainRibbonControl
        '
        Me.MainRibbonControl.ApplicationButtonText = Nothing
        Me.MainRibbonControl.ExpandCollapseItem.Id = 0
        Me.MainRibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.MainRibbonControl.ExpandCollapseItem, Me.MainRibbonControl.SearchEditItem, Me.mnusysabout, Me.FTUserLogINIP})
        Me.MainRibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.MainRibbonControl.Margin = New System.Windows.Forms.Padding(6, 5, 6, 5)
        Me.MainRibbonControl.MaxItemId = 5
        Me.MainRibbonControl.Name = "MainRibbonControl"
        Me.MainRibbonControl.OptionsPageCategories.Alignment = DevExpress.XtraBars.Ribbon.RibbonPageCategoryAlignment.Left
        Me.MainRibbonControl.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemComboBox1})
        Me.MainRibbonControl.ShowExpandCollapseButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.MainRibbonControl.ShowToolbarCustomizeItem = False
        Me.MainRibbonControl.Size = New System.Drawing.Size(1478, 54)
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
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 873)
        Me.RibbonStatusBar.Margin = New System.Windows.Forms.Padding(6, 5, 6, 5)
        Me.RibbonStatusBar.Name = "RibbonStatusBar"
        Me.RibbonStatusBar.Ribbon = Me.MainRibbonControl
        Me.RibbonStatusBar.Size = New System.Drawing.Size(1478, 27)
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
        Me.StandaloneBarDockControl.Size = New System.Drawing.Size(1478, 0)
        Me.StandaloneBarDockControl.Text = "StandaloneBarDockControl1"
        '
        'ogbmainprocbutton
        '
        Me.ogbmainprocbutton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmsavelayout)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmexit)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmpreview)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmreject)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmapprove)
        Me.ogbmainprocbutton.Location = New System.Drawing.Point(360, 304)
        Me.ogbmainprocbutton.Margin = New System.Windows.Forms.Padding(4)
        Me.ogbmainprocbutton.Name = "ogbmainprocbutton"
        Me.ogbmainprocbutton.Size = New System.Drawing.Size(802, 108)
        Me.ogbmainprocbutton.TabIndex = 138
        Me.ogbmainprocbutton.Tag = "2|"
        '
        'ocmsavelayout
        '
        Me.ocmsavelayout.Location = New System.Drawing.Point(91, 54)
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
        'ocmpreview
        '
        Me.ocmpreview.Location = New System.Drawing.Point(319, 22)
        Me.ocmpreview.Margin = New System.Windows.Forms.Padding(4)
        Me.ocmpreview.Name = "ocmpreview"
        Me.ocmpreview.Size = New System.Drawing.Size(132, 30)
        Me.ocmpreview.TabIndex = 12
        Me.ocmpreview.Text = "Preview"
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
        Me.otbmain.Location = New System.Drawing.Point(0, 54)
        Me.otbmain.Name = "otbmain"
        Me.otbmain.SelectedTabPage = Me.otpappfactoryCM
        Me.otbmain.Size = New System.Drawing.Size(1478, 819)
        Me.otbmain.TabIndex = 14
        Me.otbmain.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.otpappfactory, Me.otpappcminv, Me.otppurchasefacapp, Me.otpappfactoryCM})
        '
        'otpappfactoryCM
        '
        Me.otpappfactoryCM.Controls.Add(Me.GroupControl3)
        Me.otpappfactoryCM.Name = "otpappfactoryCM"
        Me.otpappfactoryCM.Size = New System.Drawing.Size(1470, 789)
        Me.otpappfactoryCM.Text = "Approve Factory CM"
        '
        'GroupControl3
        '
        Me.GroupControl3.AppearanceCaption.Options.UseTextOptions = True
        Me.GroupControl3.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GroupControl3.Controls.Add(Me.ogcAppFactoryCM)
        Me.GroupControl3.Controls.Add(Me.ockAppCMselectall)
        Me.GroupControl3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl3.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl3.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupControl3.Name = "GroupControl3"
        Me.GroupControl3.Size = New System.Drawing.Size(1470, 789)
        Me.GroupControl3.TabIndex = 12
        Me.GroupControl3.Text = "Approved  Factory CM"
        '
        'ogcAppFactoryCM
        '
        Me.ogcAppFactoryCM.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogcAppFactoryCM.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcAppFactoryCM.Location = New System.Drawing.Point(2, 22)
        Me.ogcAppFactoryCM.MainView = Me.ogvAppFactoryCM
        Me.ogcAppFactoryCM.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcAppFactoryCM.Name = "ogcAppFactoryCM"
        Me.ogcAppFactoryCM.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemTextEdit3, Me.RepositoryItemCheckEdit3})
        Me.ogcAppFactoryCM.Size = New System.Drawing.Size(1466, 765)
        Me.ogcAppFactoryCM.TabIndex = 20
        Me.ogcAppFactoryCM.TabStop = False
        Me.ogcAppFactoryCM.Tag = "2|"
        Me.ogcAppFactoryCM.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvAppFactoryCM})
        '
        'ogvAppFactoryCM
        '
        Me.ogvAppFactoryCM.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn4, Me.cmFTSelect, Me.cmFTCmpCode, Me.cmFTCompName, Me.cmFTCmpCodeTo, Me.cmFTCompNameTo, Me.cmFTPORef, Me.GridColumn11, Me.cmFTStyleCode, Me.cmFTOrderNo, Me.GridColumn14, Me.cmFTCustCode, Me.cmFTCustName, Me.GridColumn17, Me.cmFTBuyCode, Me.GridColumn19, Me.cmFTSeasonCode, Me.GridColumn21, Me.cmFTProdTypeCode, Me.cmFTProdTypeName, Me.cmFDShipDate, Me.cmFNQuantity, Me.cmFNAmt, Me.cmFNExtraQuantity, Me.cmFNAmntExtra, Me.cmFNGarmentQtyTest, Me.cmFNAmntQtyTest, Me.cmFTNikePOLineItem, Me.cmFNTotalQuantity, Me.cmFNSeq})
        Me.ogvAppFactoryCM.CustomizationFormBounds = New System.Drawing.Rectangle(758, 512, 216, 178)
        Me.ogvAppFactoryCM.GridControl = Me.ogcAppFactoryCM
        Me.ogvAppFactoryCM.GroupCount = 1
        Me.ogvAppFactoryCM.GroupSummary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FNQuantity", Me.cmFNQuantity, "{0:n0}"), New DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FNExtraQuantity", Me.cmFNExtraQuantity, "{0:n0}"), New DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FNTotalQuantity", Me.cmFNTotalQuantity, "{0:n0}")})
        Me.ogvAppFactoryCM.Name = "ogvAppFactoryCM"
        Me.ogvAppFactoryCM.OptionsBehavior.AutoExpandAllGroups = True
        Me.ogvAppFactoryCM.OptionsCustomization.AllowQuickHideColumns = False
        Me.ogvAppFactoryCM.OptionsNavigation.EnterMoveNextColumn = True
        Me.ogvAppFactoryCM.OptionsView.AllowCellMerge = True
        Me.ogvAppFactoryCM.OptionsView.ColumnAutoWidth = False
        Me.ogvAppFactoryCM.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.ogvAppFactoryCM.OptionsView.ShowAutoFilterRow = True
        Me.ogvAppFactoryCM.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
        Me.ogvAppFactoryCM.OptionsView.ShowFooter = True
        Me.ogvAppFactoryCM.OptionsView.ShowGroupPanel = False
        Me.ogvAppFactoryCM.SortInfo.AddRange(New DevExpress.XtraGrid.Columns.GridColumnSortInfo() {New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.cmFTCmpCode, DevExpress.Data.ColumnSortOrder.Ascending)})
        Me.ogvAppFactoryCM.Tag = "2|"
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "FNHSysCmpId"
        Me.GridColumn4.FieldName = "FNHSysCmpId"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.OptionsColumn.AllowEdit = False
        Me.GridColumn4.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.GridColumn4.OptionsColumn.ReadOnly = True
        '
        'cmFTSelect
        '
        Me.cmFTSelect.Caption = "  "
        Me.cmFTSelect.ColumnEdit = Me.RepositoryItemCheckEdit3
        Me.cmFTSelect.FieldName = "FTSelect"
        Me.cmFTSelect.Name = "cmFTSelect"
        Me.cmFTSelect.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.cmFTSelect.Visible = True
        Me.cmFTSelect.VisibleIndex = 0
        Me.cmFTSelect.Width = 47
        '
        'RepositoryItemCheckEdit3
        '
        Me.RepositoryItemCheckEdit3.AutoHeight = False
        Me.RepositoryItemCheckEdit3.Caption = "Check"
        Me.RepositoryItemCheckEdit3.Name = "RepositoryItemCheckEdit3"
        Me.RepositoryItemCheckEdit3.ValueChecked = "1"
        Me.RepositoryItemCheckEdit3.ValueUnchecked = "0"
        '
        'cmFTCmpCode
        '
        Me.cmFTCmpCode.Caption = "โรงงาน"
        Me.cmFTCmpCode.FieldName = "FTCmpCode"
        Me.cmFTCmpCode.Name = "cmFTCmpCode"
        Me.cmFTCmpCode.OptionsColumn.AllowEdit = False
        Me.cmFTCmpCode.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.cmFTCmpCode.OptionsColumn.ReadOnly = True
        Me.cmFTCmpCode.Visible = True
        Me.cmFTCmpCode.VisibleIndex = 1
        '
        'cmFTCompName
        '
        Me.cmFTCompName.Caption = "cmFTCompName"
        Me.cmFTCompName.FieldName = "FTCompName"
        Me.cmFTCompName.MinWidth = 25
        Me.cmFTCompName.Name = "cmFTCompName"
        Me.cmFTCompName.OptionsColumn.AllowEdit = False
        Me.cmFTCompName.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.cmFTCompName.OptionsColumn.ReadOnly = True
        Me.cmFTCompName.Visible = True
        Me.cmFTCompName.VisibleIndex = 1
        Me.cmFTCompName.Width = 94
        '
        'cmFTCmpCodeTo
        '
        Me.cmFTCmpCodeTo.Caption = "ถึง โรงงาน"
        Me.cmFTCmpCodeTo.FieldName = "FTCmpCodeTo"
        Me.cmFTCmpCodeTo.Name = "cmFTCmpCodeTo"
        Me.cmFTCmpCodeTo.OptionsColumn.AllowEdit = False
        Me.cmFTCmpCodeTo.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.cmFTCmpCodeTo.OptionsColumn.ReadOnly = True
        Me.cmFTCmpCodeTo.Visible = True
        Me.cmFTCmpCodeTo.VisibleIndex = 2
        '
        'cmFTCompNameTo
        '
        Me.cmFTCompNameTo.Caption = "FTCompNameTo"
        Me.cmFTCompNameTo.FieldName = "FTCompNameTo"
        Me.cmFTCompNameTo.Name = "cmFTCompNameTo"
        Me.cmFTCompNameTo.OptionsColumn.AllowEdit = False
        Me.cmFTCompNameTo.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.cmFTCompNameTo.OptionsColumn.ReadOnly = True
        Me.cmFTCompNameTo.Visible = True
        Me.cmFTCompNameTo.VisibleIndex = 3
        Me.cmFTCompNameTo.Width = 103
        '
        'cmFTPORef
        '
        Me.cmFTPORef.Caption = "FTPORef"
        Me.cmFTPORef.FieldName = "FTPORef"
        Me.cmFTPORef.Name = "cmFTPORef"
        Me.cmFTPORef.OptionsColumn.AllowEdit = False
        Me.cmFTPORef.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.cmFTPORef.OptionsColumn.ReadOnly = True
        Me.cmFTPORef.Visible = True
        Me.cmFTPORef.VisibleIndex = 6
        '
        'GridColumn11
        '
        Me.GridColumn11.Caption = "FNHSysStyleId"
        Me.GridColumn11.FieldName = "FNHSysStyleId"
        Me.GridColumn11.Name = "GridColumn11"
        Me.GridColumn11.OptionsColumn.AllowEdit = False
        Me.GridColumn11.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[True]
        Me.GridColumn11.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.GridColumn11.OptionsColumn.ReadOnly = True
        '
        'cmFTStyleCode
        '
        Me.cmFTStyleCode.Caption = "FTStyleCode"
        Me.cmFTStyleCode.FieldName = "FTStyleCode"
        Me.cmFTStyleCode.Name = "cmFTStyleCode"
        Me.cmFTStyleCode.OptionsColumn.AllowEdit = False
        Me.cmFTStyleCode.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.cmFTStyleCode.OptionsColumn.ReadOnly = True
        Me.cmFTStyleCode.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        Me.cmFTStyleCode.Visible = True
        Me.cmFTStyleCode.VisibleIndex = 7
        '
        'cmFTOrderNo
        '
        Me.cmFTOrderNo.Caption = "FTOrderNo"
        Me.cmFTOrderNo.FieldName = "FTOrderNo"
        Me.cmFTOrderNo.Name = "cmFTOrderNo"
        Me.cmFTOrderNo.OptionsColumn.AllowEdit = False
        Me.cmFTOrderNo.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.cmFTOrderNo.OptionsColumn.ReadOnly = True
        Me.cmFTOrderNo.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        Me.cmFTOrderNo.Visible = True
        Me.cmFTOrderNo.VisibleIndex = 5
        '
        'GridColumn14
        '
        Me.GridColumn14.Caption = "FNHSysCustId"
        Me.GridColumn14.FieldName = "FNHSysCustId"
        Me.GridColumn14.Name = "GridColumn14"
        Me.GridColumn14.OptionsColumn.AllowEdit = False
        Me.GridColumn14.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.GridColumn14.OptionsColumn.ReadOnly = True
        '
        'cmFTCustCode
        '
        Me.cmFTCustCode.Caption = "FTCustCode"
        Me.cmFTCustCode.FieldName = "FTCustCode"
        Me.cmFTCustCode.Name = "cmFTCustCode"
        Me.cmFTCustCode.OptionsColumn.AllowEdit = False
        Me.cmFTCustCode.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.cmFTCustCode.OptionsColumn.ReadOnly = True
        Me.cmFTCustCode.Visible = True
        Me.cmFTCustCode.VisibleIndex = 4
        '
        'cmFTCustName
        '
        Me.cmFTCustName.Caption = "FTCustName"
        Me.cmFTCustName.FieldName = "FTCustName"
        Me.cmFTCustName.Name = "cmFTCustName"
        Me.cmFTCustName.OptionsColumn.AllowEdit = False
        Me.cmFTCustName.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.cmFTCustName.OptionsColumn.ReadOnly = True
        Me.cmFTCustName.Visible = True
        Me.cmFTCustName.VisibleIndex = 8
        '
        'GridColumn17
        '
        Me.GridColumn17.Caption = "FNHSysBuyId"
        Me.GridColumn17.FieldName = "FNHSysBuyId"
        Me.GridColumn17.Name = "GridColumn17"
        Me.GridColumn17.OptionsColumn.AllowEdit = False
        Me.GridColumn17.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.GridColumn17.OptionsColumn.ReadOnly = True
        '
        'cmFTBuyCode
        '
        Me.cmFTBuyCode.Caption = "FTBuyCode"
        Me.cmFTBuyCode.FieldName = "FTBuyCode"
        Me.cmFTBuyCode.Name = "cmFTBuyCode"
        Me.cmFTBuyCode.OptionsColumn.AllowEdit = False
        Me.cmFTBuyCode.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.cmFTBuyCode.OptionsColumn.ReadOnly = True
        Me.cmFTBuyCode.Visible = True
        Me.cmFTBuyCode.VisibleIndex = 9
        '
        'GridColumn19
        '
        Me.GridColumn19.Caption = "FNHSysSeasonId"
        Me.GridColumn19.FieldName = "FNHSysSeasonId"
        Me.GridColumn19.Name = "GridColumn19"
        Me.GridColumn19.OptionsColumn.AllowEdit = False
        Me.GridColumn19.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.GridColumn19.OptionsColumn.ReadOnly = True
        '
        'cmFTSeasonCode
        '
        Me.cmFTSeasonCode.Caption = "FTSeasonCode"
        Me.cmFTSeasonCode.FieldName = "FTSeasonCode"
        Me.cmFTSeasonCode.Name = "cmFTSeasonCode"
        Me.cmFTSeasonCode.OptionsColumn.AllowEdit = False
        Me.cmFTSeasonCode.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.cmFTSeasonCode.OptionsColumn.ReadOnly = True
        Me.cmFTSeasonCode.Visible = True
        Me.cmFTSeasonCode.VisibleIndex = 10
        '
        'GridColumn21
        '
        Me.GridColumn21.Caption = "FNHSysProdTypeId"
        Me.GridColumn21.FieldName = "FNHSysProdTypeId"
        Me.GridColumn21.Name = "GridColumn21"
        Me.GridColumn21.OptionsColumn.AllowEdit = False
        Me.GridColumn21.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.GridColumn21.OptionsColumn.ReadOnly = True
        '
        'cmFTProdTypeCode
        '
        Me.cmFTProdTypeCode.Caption = "FTProdTypeCode"
        Me.cmFTProdTypeCode.FieldName = "FTProdTypeCode"
        Me.cmFTProdTypeCode.Name = "cmFTProdTypeCode"
        Me.cmFTProdTypeCode.OptionsColumn.AllowEdit = False
        Me.cmFTProdTypeCode.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.cmFTProdTypeCode.OptionsColumn.ReadOnly = True
        '
        'cmFTProdTypeName
        '
        Me.cmFTProdTypeName.Caption = "FTProdTypeName"
        Me.cmFTProdTypeName.FieldName = "FTProdTypeName"
        Me.cmFTProdTypeName.Name = "cmFTProdTypeName"
        Me.cmFTProdTypeName.OptionsColumn.AllowEdit = False
        Me.cmFTProdTypeName.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.cmFTProdTypeName.OptionsColumn.ReadOnly = True
        '
        'cmFDShipDate
        '
        Me.cmFDShipDate.Caption = "FDShipDate"
        Me.cmFDShipDate.FieldName = "FDShipDate"
        Me.cmFDShipDate.Name = "cmFDShipDate"
        Me.cmFDShipDate.OptionsColumn.AllowEdit = False
        Me.cmFDShipDate.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.cmFDShipDate.OptionsColumn.ReadOnly = True
        Me.cmFDShipDate.Visible = True
        Me.cmFDShipDate.VisibleIndex = 11
        '
        'cmFNQuantity
        '
        Me.cmFNQuantity.Caption = "FNQuantity"
        Me.cmFNQuantity.ColumnEdit = Me.RepositoryItemTextEdit3
        Me.cmFNQuantity.DisplayFormat.FormatString = "{0:n0}"
        Me.cmFNQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cmFNQuantity.FieldName = "FNQuantity"
        Me.cmFNQuantity.Name = "cmFNQuantity"
        Me.cmFNQuantity.OptionsColumn.AllowEdit = False
        Me.cmFNQuantity.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.cmFNQuantity.OptionsColumn.ReadOnly = True
        Me.cmFNQuantity.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        Me.cmFNQuantity.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FNQuantity", "{0:n0}")})
        '
        'RepositoryItemTextEdit3
        '
        Me.RepositoryItemTextEdit3.AutoHeight = False
        Me.RepositoryItemTextEdit3.DisplayFormat.FormatString = "N0"
        Me.RepositoryItemTextEdit3.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.RepositoryItemTextEdit3.EditFormat.FormatString = "N0"
        Me.RepositoryItemTextEdit3.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.RepositoryItemTextEdit3.Name = "RepositoryItemTextEdit3"
        '
        'cmFNAmt
        '
        Me.cmFNAmt.Caption = "Amount"
        Me.cmFNAmt.DisplayFormat.FormatString = "{0:n2}"
        Me.cmFNAmt.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cmFNAmt.FieldName = "FNAmt"
        Me.cmFNAmt.Name = "cmFNAmt"
        Me.cmFNAmt.OptionsColumn.AllowEdit = False
        Me.cmFNAmt.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.cmFNAmt.OptionsColumn.ReadOnly = True
        Me.cmFNAmt.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FNAmt", "{0:n2}")})
        '
        'cmFNExtraQuantity
        '
        Me.cmFNExtraQuantity.Caption = "FNExtraQuantity"
        Me.cmFNExtraQuantity.ColumnEdit = Me.RepositoryItemTextEdit3
        Me.cmFNExtraQuantity.DisplayFormat.FormatString = "{0:n0}"
        Me.cmFNExtraQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cmFNExtraQuantity.FieldName = "FNExtraQuantity"
        Me.cmFNExtraQuantity.Name = "cmFNExtraQuantity"
        Me.cmFNExtraQuantity.OptionsColumn.AllowEdit = False
        Me.cmFNExtraQuantity.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.cmFNExtraQuantity.OptionsColumn.ReadOnly = True
        Me.cmFNExtraQuantity.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        Me.cmFNExtraQuantity.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FNExtraQuantity", "{0:n0}")})
        '
        'cmFNAmntExtra
        '
        Me.cmFNAmntExtra.Caption = "Extra Amount"
        Me.cmFNAmntExtra.DisplayFormat.FormatString = "{0:n2}"
        Me.cmFNAmntExtra.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cmFNAmntExtra.FieldName = "FNAmntExtra"
        Me.cmFNAmntExtra.Name = "cmFNAmntExtra"
        Me.cmFNAmntExtra.OptionsColumn.AllowEdit = False
        Me.cmFNAmntExtra.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.cmFNAmntExtra.OptionsColumn.ReadOnly = True
        Me.cmFNAmntExtra.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FNAmntExtra", "{0:n2}")})
        '
        'cmFNGarmentQtyTest
        '
        Me.cmFNGarmentQtyTest.Caption = "Garment Qty Test"
        Me.cmFNGarmentQtyTest.DisplayFormat.FormatString = "{0:n0}"
        Me.cmFNGarmentQtyTest.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cmFNGarmentQtyTest.FieldName = "FNGarmentQtyTest"
        Me.cmFNGarmentQtyTest.Name = "cmFNGarmentQtyTest"
        Me.cmFNGarmentQtyTest.OptionsColumn.AllowEdit = False
        Me.cmFNGarmentQtyTest.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.cmFNGarmentQtyTest.OptionsColumn.ReadOnly = True
        Me.cmFNGarmentQtyTest.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FNGarmentQtyTest", "{0:n0}")})
        '
        'cmFNAmntQtyTest
        '
        Me.cmFNAmntQtyTest.Caption = "Test Amount"
        Me.cmFNAmntQtyTest.DisplayFormat.FormatString = "{0:n2}"
        Me.cmFNAmntQtyTest.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cmFNAmntQtyTest.FieldName = "FNAmntQtyTest"
        Me.cmFNAmntQtyTest.Name = "cmFNAmntQtyTest"
        Me.cmFNAmntQtyTest.OptionsColumn.AllowEdit = False
        Me.cmFNAmntQtyTest.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.cmFNAmntQtyTest.OptionsColumn.ReadOnly = True
        Me.cmFNAmntQtyTest.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FNAmntQtyTest", "{0:n2}")})
        '
        'cmFTNikePOLineItem
        '
        Me.cmFTNikePOLineItem.Caption = "PO Line"
        Me.cmFTNikePOLineItem.FieldName = "FTNikePOLineItem"
        Me.cmFTNikePOLineItem.Name = "cmFTNikePOLineItem"
        Me.cmFTNikePOLineItem.OptionsColumn.AllowEdit = False
        Me.cmFTNikePOLineItem.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.cmFTNikePOLineItem.OptionsColumn.ReadOnly = True
        Me.cmFTNikePOLineItem.Visible = True
        Me.cmFTNikePOLineItem.VisibleIndex = 12
        '
        'cmFNTotalQuantity
        '
        Me.cmFNTotalQuantity.Caption = "FNTotalQuantity"
        Me.cmFNTotalQuantity.ColumnEdit = Me.RepositoryItemTextEdit3
        Me.cmFNTotalQuantity.DisplayFormat.FormatString = "{0:n0}"
        Me.cmFNTotalQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cmFNTotalQuantity.FieldName = "FNTotalQuantity"
        Me.cmFNTotalQuantity.Name = "cmFNTotalQuantity"
        Me.cmFNTotalQuantity.OptionsColumn.AllowEdit = False
        Me.cmFNTotalQuantity.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.cmFNTotalQuantity.OptionsColumn.ReadOnly = True
        Me.cmFNTotalQuantity.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FNTotalQuantity", "{0:n0}")})
        Me.cmFNTotalQuantity.Visible = True
        Me.cmFNTotalQuantity.VisibleIndex = 13
        '
        'cmFNSeq
        '
        Me.cmFNSeq.Caption = "cmFNSeq"
        Me.cmFNSeq.FieldName = "FNSeq"
        Me.cmFNSeq.MinWidth = 25
        Me.cmFNSeq.Name = "cmFNSeq"
        Me.cmFNSeq.Width = 94
        '
        'ockAppCMselectall
        '
        Me.ockAppCMselectall.Location = New System.Drawing.Point(28, 1)
        Me.ockAppCMselectall.Margin = New System.Windows.Forms.Padding(4)
        Me.ockAppCMselectall.Name = "ockAppCMselectall"
        Me.ockAppCMselectall.Properties.Caption = "Select All"
        Me.ockAppCMselectall.Size = New System.Drawing.Size(230, 20)
        Me.ockAppCMselectall.TabIndex = 1
        '
        'otpappfactory
        '
        Me.otpappfactory.Controls.Add(Me.GroupControl1)
        Me.otpappfactory.Name = "otpappfactory"
        Me.otpappfactory.PageVisible = False
        Me.otpappfactory.Size = New System.Drawing.Size(1470, 789)
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
        Me.GroupControl1.Size = New System.Drawing.Size(1470, 789)
        Me.GroupControl1.TabIndex = 11
        Me.GroupControl1.Text = "Approved  Factory"
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
        Me.ogcdirector.Size = New System.Drawing.Size(1466, 765)
        Me.ogcdirector.TabIndex = 20
        Me.ogcdirector.TabStop = False
        Me.ogcdirector.Tag = "2|"
        Me.ogcdirector.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvdirector})
        '
        'ogvdirector
        '
        Me.ogvdirector.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.C2FNHSysCmpId, Me.FTSelect, Me.C2FTCmpCodeTo, Me.C2FTCmpCode, Me.C2FTCompName, Me.C2FTPORef, Me.C2FNHSysStyleId, Me.C2FTStyleCode, Me.C2FTOrderNo, Me.C2FNHSysCustId, Me.C2FTCustCode, Me.C2FTCustName, Me.C2FNHSysBuyId, Me.C2FTBuyCode, Me.C2FNHSysSeasonId, Me.C2FTSeasonCode, Me.C2FNHSysProdTypeId, Me.C2FTProdTypeCode, Me.C2FTProdTypeName, Me.C2FDShipDate, Me.C2FTCurCode, Me.C2FNQuantity, Me.C2FNAmt, Me.C2FNExtraQuantity, Me.C2FNAmntExtra, Me.CFNGarmentQtyTest, Me.FNAmntQtyTest, Me.CFTColorway, Me.CFTNikePOLineItem, Me.CFTSizeBreakDown, Me.FNCM, Me.C2FNTotalQuantity, Me.FNGrandAmnt, Me.FNGrandAmtCM, Me.C2FTStateEmb, Me.C2FTStatePrint, Me.C2FTStateHeat, Me.C2FTStateLaser, Me.C2FTStateWindows, Me.C2FTStateOrderApp, Me.C2FTAppBy, Me.C2FDAppDate, Me.C2FTAppTime, Me.FTStateSendDirectorApp, Me.FTStateSendDirectorBy, Me.FDStateSendDirectorDate, Me.FTStateSendDirectorTime, Me.FTStateDirectorApp, Me.FTStateDirectorAppBy, Me.FDStateDirectorAppDate, Me.FTStateDirectorAppTime, Me.FTStateDirectorReject, Me.FTStateDirectorRejectBy, Me.FDStateDirectorRejectDate, Me.FTStateDirectorRejectTime})
        Me.ogvdirector.CustomizationFormBounds = New System.Drawing.Rectangle(758, 512, 216, 178)
        Me.ogvdirector.GridControl = Me.ogcdirector
        Me.ogvdirector.GroupCount = 1
        Me.ogvdirector.GroupSummary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FNQuantity", Me.C2FNQuantity, "{0:n0}"), New DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FNExtraQuantity", Me.C2FNExtraQuantity, "{0:n0}"), New DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FNTotalQuantity", Me.C2FNTotalQuantity, "{0:n0}")})
        Me.ogvdirector.Name = "ogvdirector"
        Me.ogvdirector.OptionsBehavior.AutoExpandAllGroups = True
        Me.ogvdirector.OptionsCustomization.AllowQuickHideColumns = False
        Me.ogvdirector.OptionsNavigation.EnterMoveNextColumn = True
        Me.ogvdirector.OptionsView.AllowCellMerge = True
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
        Me.C2FTCmpCode.VisibleIndex = 4
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
        Me.C2FTPORef.VisibleIndex = 4
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
        Me.C2FTStyleCode.VisibleIndex = 5
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
        Me.C2FTOrderNo.VisibleIndex = 6
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
        Me.C2FTCustCode.Visible = True
        Me.C2FTCustCode.VisibleIndex = 3
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
        Me.C2FTCustName.VisibleIndex = 7
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
        Me.C2FTBuyCode.VisibleIndex = 8
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
        Me.C2FTSeasonCode.VisibleIndex = 9
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
        Me.C2FDShipDate.VisibleIndex = 10
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
        Me.C2FTCurCode.VisibleIndex = 11
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
        'CFTColorway
        '
        Me.CFTColorway.Caption = "Colorway"
        Me.CFTColorway.FieldName = "FTColorway"
        Me.CFTColorway.Name = "CFTColorway"
        Me.CFTColorway.OptionsColumn.AllowEdit = False
        Me.CFTColorway.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFTColorway.OptionsColumn.ReadOnly = True
        Me.CFTColorway.Visible = True
        Me.CFTColorway.VisibleIndex = 12
        '
        'CFTNikePOLineItem
        '
        Me.CFTNikePOLineItem.Caption = "PO Line"
        Me.CFTNikePOLineItem.FieldName = "FTNikePOLineItem"
        Me.CFTNikePOLineItem.Name = "CFTNikePOLineItem"
        Me.CFTNikePOLineItem.OptionsColumn.AllowEdit = False
        Me.CFTNikePOLineItem.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFTNikePOLineItem.OptionsColumn.ReadOnly = True
        Me.CFTNikePOLineItem.Visible = True
        Me.CFTNikePOLineItem.VisibleIndex = 13
        '
        'CFTSizeBreakDown
        '
        Me.CFTSizeBreakDown.Caption = "SizeBreakDown"
        Me.CFTSizeBreakDown.FieldName = "FTSizeBreakDown"
        Me.CFTSizeBreakDown.Name = "CFTSizeBreakDown"
        Me.CFTSizeBreakDown.OptionsColumn.AllowEdit = False
        Me.CFTSizeBreakDown.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFTSizeBreakDown.OptionsColumn.ReadOnly = True
        Me.CFTSizeBreakDown.Visible = True
        Me.CFTSizeBreakDown.VisibleIndex = 14
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
        'C2FNTotalQuantity
        '
        Me.C2FNTotalQuantity.Caption = "FNTotalQuantity"
        Me.C2FNTotalQuantity.ColumnEdit = Me.RepositoryItemTextEdit1
        Me.C2FNTotalQuantity.DisplayFormat.FormatString = "{0:n0}"
        Me.C2FNTotalQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.C2FNTotalQuantity.FieldName = "FNTotalQuantity"
        Me.C2FNTotalQuantity.Name = "C2FNTotalQuantity"
        Me.C2FNTotalQuantity.OptionsColumn.AllowEdit = False
        Me.C2FNTotalQuantity.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.C2FNTotalQuantity.OptionsColumn.ReadOnly = True
        Me.C2FNTotalQuantity.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FNTotalQuantity", "{0:n0}")})
        Me.C2FNTotalQuantity.Visible = True
        Me.C2FNTotalQuantity.VisibleIndex = 15
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
        '
        'FNGrandAmtCM
        '
        Me.FNGrandAmtCM.Caption = "Amount"
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
        '
        'FDStateSendDirectorDate
        '
        Me.FDStateSendDirectorDate.Caption = "Send App Date"
        Me.FDStateSendDirectorDate.FieldName = "FDStateSendDirectorDate"
        Me.FDStateSendDirectorDate.Name = "FDStateSendDirectorDate"
        Me.FDStateSendDirectorDate.OptionsColumn.AllowEdit = False
        Me.FDStateSendDirectorDate.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.FDStateSendDirectorDate.OptionsColumn.ReadOnly = True
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
        'ockdirectorselectall
        '
        Me.ockdirectorselectall.Location = New System.Drawing.Point(28, 1)
        Me.ockdirectorselectall.Margin = New System.Windows.Forms.Padding(4)
        Me.ockdirectorselectall.Name = "ockdirectorselectall"
        Me.ockdirectorselectall.Properties.Caption = "Select All"
        Me.ockdirectorselectall.Size = New System.Drawing.Size(230, 20)
        Me.ockdirectorselectall.TabIndex = 1
        '
        'otpappcminv
        '
        Me.otpappcminv.Controls.Add(Me.GroupControl2)
        Me.otpappcminv.Margin = New System.Windows.Forms.Padding(4)
        Me.otpappcminv.Name = "otpappcminv"
        Me.otpappcminv.Size = New System.Drawing.Size(1470, 770)
        Me.otpappcminv.Text = "Approve Commercial Invoice"
        '
        'GroupControl2
        '
        Me.GroupControl2.AppearanceCaption.Options.UseTextOptions = True
        Me.GroupControl2.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GroupControl2.Controls.Add(Me.ogccminv)
        Me.GroupControl2.Controls.Add(Me.FTSelectAllCMInv)
        Me.GroupControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl2.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl2.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupControl2.Name = "GroupControl2"
        Me.GroupControl2.Size = New System.Drawing.Size(1470, 770)
        Me.GroupControl2.TabIndex = 12
        Me.GroupControl2.Text = "Approved  Commercial Invoice"
        '
        'ogccminv
        '
        Me.ogccminv.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogccminv.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogccminv.Location = New System.Drawing.Point(2, 22)
        Me.ogccminv.MainView = Me.ogvcminv
        Me.ogccminv.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogccminv.Name = "ogccminv"
        Me.ogccminv.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemTextEdit2, Me.RepositoryItemCheckEdit1})
        Me.ogccminv.Size = New System.Drawing.Size(1466, 746)
        Me.ogccminv.TabIndex = 20
        Me.ogccminv.TabStop = False
        Me.ogccminv.Tag = "2|"
        Me.ogccminv.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvcminv})
        '
        'ogvcminv
        '
        Me.ogvcminv.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2, Me.cFTCmpCode, Me.cFTCompName, Me.cFTCustomerPO, Me.GridColumn7, Me.cFTStyleCode, Me.cFTInvoiceNo, Me.cFDInvoiceDate, Me.C2FTColorway, Me.C2FTNikePOLineItem, Me.C2FTSizeBreakDown, Me.cFNCM, Me.cFNQuantity, Me.cFNAmount, Me.cFTStateSendBy})
        Me.ogvcminv.CustomizationFormBounds = New System.Drawing.Rectangle(758, 512, 216, 178)
        Me.ogvcminv.GridControl = Me.ogccminv
        Me.ogvcminv.GroupCount = 1
        Me.ogvcminv.GroupSummary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FNQuantity", Me.cFNQuantity, "{0:n0}"), New DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FNExtraQuantity", Nothing, "{0:n0}"), New DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FNTotalQuantity", Nothing, "{0:n0}"), New DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FNAmount", Me.cFNAmount, "{0:n2}")})
        Me.ogvcminv.Name = "ogvcminv"
        Me.ogvcminv.OptionsBehavior.AutoExpandAllGroups = True
        Me.ogvcminv.OptionsCustomization.AllowQuickHideColumns = False
        Me.ogvcminv.OptionsNavigation.EnterMoveNextColumn = True
        Me.ogvcminv.OptionsView.AllowCellMerge = True
        Me.ogvcminv.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.ogvcminv.OptionsView.ShowAutoFilterRow = True
        Me.ogvcminv.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
        Me.ogvcminv.OptionsView.ShowFooter = True
        Me.ogvcminv.OptionsView.ShowGroupPanel = False
        Me.ogvcminv.SortInfo.AddRange(New DevExpress.XtraGrid.Columns.GridColumnSortInfo() {New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.cFTCmpCode, DevExpress.Data.ColumnSortOrder.Ascending)})
        Me.ogvcminv.Tag = "2|"
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "FNHSysCmpId"
        Me.GridColumn1.FieldName = "FNHSysCmpId"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.OptionsColumn.AllowEdit = False
        Me.GridColumn1.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn1.OptionsColumn.ReadOnly = True
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "  "
        Me.GridColumn2.ColumnEdit = Me.RepositoryItemCheckEdit1
        Me.GridColumn2.FieldName = "FTSelect"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 0
        Me.GridColumn2.Width = 56
        '
        'RepositoryItemCheckEdit1
        '
        Me.RepositoryItemCheckEdit1.AutoHeight = False
        Me.RepositoryItemCheckEdit1.Caption = "Check"
        Me.RepositoryItemCheckEdit1.Name = "RepositoryItemCheckEdit1"
        Me.RepositoryItemCheckEdit1.ValueChecked = "1"
        Me.RepositoryItemCheckEdit1.ValueUnchecked = "0"
        '
        'cFTCmpCode
        '
        Me.cFTCmpCode.Caption = "FTCmpCode"
        Me.cFTCmpCode.FieldName = "FTCmpCode"
        Me.cFTCmpCode.GroupInterval = DevExpress.XtraGrid.ColumnGroupInterval.Value
        Me.cFTCmpCode.Name = "cFTCmpCode"
        Me.cFTCmpCode.OptionsColumn.AllowEdit = False
        Me.cFTCmpCode.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.cFTCmpCode.OptionsColumn.ReadOnly = True
        Me.cFTCmpCode.Visible = True
        Me.cFTCmpCode.VisibleIndex = 4
        '
        'cFTCompName
        '
        Me.cFTCompName.Caption = "FTCompName"
        Me.cFTCompName.FieldName = "FTCmpName"
        Me.cFTCompName.Name = "cFTCompName"
        Me.cFTCompName.OptionsColumn.AllowEdit = False
        Me.cFTCompName.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.cFTCompName.OptionsColumn.ReadOnly = True
        Me.cFTCompName.Visible = True
        Me.cFTCompName.VisibleIndex = 1
        Me.cFTCompName.Width = 184
        '
        'cFTCustomerPO
        '
        Me.cFTCustomerPO.Caption = "FTCustomerPO"
        Me.cFTCustomerPO.FieldName = "FTCustomerPO"
        Me.cFTCustomerPO.Name = "cFTCustomerPO"
        Me.cFTCustomerPO.OptionsColumn.AllowEdit = False
        Me.cFTCustomerPO.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.cFTCustomerPO.OptionsColumn.ReadOnly = True
        Me.cFTCustomerPO.Visible = True
        Me.cFTCustomerPO.VisibleIndex = 2
        Me.cFTCustomerPO.Width = 134
        '
        'GridColumn7
        '
        Me.GridColumn7.Caption = "FNHSysStyleId"
        Me.GridColumn7.FieldName = "FNHSysStyleId"
        Me.GridColumn7.Name = "GridColumn7"
        Me.GridColumn7.OptionsColumn.AllowEdit = False
        Me.GridColumn7.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[True]
        Me.GridColumn7.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.GridColumn7.OptionsColumn.ReadOnly = True
        '
        'cFTStyleCode
        '
        Me.cFTStyleCode.Caption = "FTStyleCode"
        Me.cFTStyleCode.FieldName = "FTStyleCode"
        Me.cFTStyleCode.Name = "cFTStyleCode"
        Me.cFTStyleCode.OptionsColumn.AllowEdit = False
        Me.cFTStyleCode.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.cFTStyleCode.OptionsColumn.ReadOnly = True
        Me.cFTStyleCode.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        Me.cFTStyleCode.Visible = True
        Me.cFTStyleCode.VisibleIndex = 3
        Me.cFTStyleCode.Width = 134
        '
        'cFTInvoiceNo
        '
        Me.cFTInvoiceNo.Caption = "FTInvoiceNo"
        Me.cFTInvoiceNo.FieldName = "FTInvoiceNo"
        Me.cFTInvoiceNo.Name = "cFTInvoiceNo"
        Me.cFTInvoiceNo.OptionsColumn.AllowEdit = False
        Me.cFTInvoiceNo.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.cFTInvoiceNo.Visible = True
        Me.cFTInvoiceNo.VisibleIndex = 4
        Me.cFTInvoiceNo.Width = 134
        '
        'cFDInvoiceDate
        '
        Me.cFDInvoiceDate.Caption = "FDInvoiceDate"
        Me.cFDInvoiceDate.FieldName = "FDInvoiceDate"
        Me.cFDInvoiceDate.Name = "cFDInvoiceDate"
        Me.cFDInvoiceDate.OptionsColumn.AllowEdit = False
        Me.cFDInvoiceDate.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.cFDInvoiceDate.Visible = True
        Me.cFDInvoiceDate.VisibleIndex = 5
        Me.cFDInvoiceDate.Width = 134
        '
        'C2FTColorway
        '
        Me.C2FTColorway.Caption = "Colorway"
        Me.C2FTColorway.FieldName = "FTColorway"
        Me.C2FTColorway.Name = "C2FTColorway"
        Me.C2FTColorway.OptionsColumn.AllowEdit = False
        Me.C2FTColorway.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.C2FTColorway.Visible = True
        Me.C2FTColorway.VisibleIndex = 6
        Me.C2FTColorway.Width = 117
        '
        'C2FTNikePOLineItem
        '
        Me.C2FTNikePOLineItem.Caption = "PO Line"
        Me.C2FTNikePOLineItem.FieldName = "FTNikePOLineItem"
        Me.C2FTNikePOLineItem.Name = "C2FTNikePOLineItem"
        Me.C2FTNikePOLineItem.OptionsColumn.AllowEdit = False
        Me.C2FTNikePOLineItem.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.C2FTNikePOLineItem.Visible = True
        Me.C2FTNikePOLineItem.VisibleIndex = 7
        Me.C2FTNikePOLineItem.Width = 84
        '
        'C2FTSizeBreakDown
        '
        Me.C2FTSizeBreakDown.Caption = "SizeBreakDown"
        Me.C2FTSizeBreakDown.FieldName = "FTSizeBreakDown"
        Me.C2FTSizeBreakDown.Name = "C2FTSizeBreakDown"
        Me.C2FTSizeBreakDown.OptionsColumn.AllowEdit = False
        Me.C2FTSizeBreakDown.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.C2FTSizeBreakDown.Visible = True
        Me.C2FTSizeBreakDown.VisibleIndex = 8
        Me.C2FTSizeBreakDown.Width = 87
        '
        'cFNCM
        '
        Me.cFNCM.Caption = "CM Price"
        Me.cFNCM.DisplayFormat.FormatString = "{0:n4}"
        Me.cFNCM.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cFNCM.FieldName = "FNCM"
        Me.cFNCM.Name = "cFNCM"
        Me.cFNCM.OptionsColumn.AllowEdit = False
        Me.cFNCM.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.cFNCM.OptionsColumn.ReadOnly = True
        Me.cFNCM.Visible = True
        Me.cFNCM.VisibleIndex = 9
        Me.cFNCM.Width = 147
        '
        'cFNQuantity
        '
        Me.cFNQuantity.Caption = "FNQuantity"
        Me.cFNQuantity.ColumnEdit = Me.RepositoryItemTextEdit2
        Me.cFNQuantity.DisplayFormat.FormatString = "{0:n0}"
        Me.cFNQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cFNQuantity.FieldName = "FNQuantity"
        Me.cFNQuantity.Name = "cFNQuantity"
        Me.cFNQuantity.OptionsColumn.AllowEdit = False
        Me.cFNQuantity.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.cFNQuantity.OptionsColumn.ReadOnly = True
        Me.cFNQuantity.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        Me.cFNQuantity.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FNQuantity", "{0:n0}")})
        Me.cFNQuantity.Visible = True
        Me.cFNQuantity.VisibleIndex = 10
        Me.cFNQuantity.Width = 109
        '
        'RepositoryItemTextEdit2
        '
        Me.RepositoryItemTextEdit2.AutoHeight = False
        Me.RepositoryItemTextEdit2.DisplayFormat.FormatString = "N0"
        Me.RepositoryItemTextEdit2.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.RepositoryItemTextEdit2.EditFormat.FormatString = "N0"
        Me.RepositoryItemTextEdit2.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.RepositoryItemTextEdit2.Name = "RepositoryItemTextEdit2"
        '
        'cFNAmount
        '
        Me.cFNAmount.Caption = "Amount"
        Me.cFNAmount.DisplayFormat.FormatString = "{0:n2}"
        Me.cFNAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cFNAmount.FieldName = "FNAmount"
        Me.cFNAmount.Name = "cFNAmount"
        Me.cFNAmount.OptionsColumn.AllowEdit = False
        Me.cFNAmount.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.cFNAmount.OptionsColumn.ReadOnly = True
        Me.cFNAmount.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FNAmount", "{0:n2}")})
        Me.cFNAmount.Visible = True
        Me.cFNAmount.VisibleIndex = 11
        Me.cFNAmount.Width = 123
        '
        'cFTStateSendBy
        '
        Me.cFTStateSendBy.Caption = "FTStateSendBy"
        Me.cFTStateSendBy.FieldName = "FTStateSendBy"
        Me.cFTStateSendBy.Name = "cFTStateSendBy"
        Me.cFTStateSendBy.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        '
        'FTSelectAllCMInv
        '
        Me.FTSelectAllCMInv.Location = New System.Drawing.Point(28, 1)
        Me.FTSelectAllCMInv.Margin = New System.Windows.Forms.Padding(4)
        Me.FTSelectAllCMInv.Name = "FTSelectAllCMInv"
        Me.FTSelectAllCMInv.Properties.Caption = "Select All"
        Me.FTSelectAllCMInv.Size = New System.Drawing.Size(230, 20)
        Me.FTSelectAllCMInv.TabIndex = 1
        Me.FTSelectAllCMInv.Tag = "2|"
        '
        'otppurchasefacapp
        '
        Me.otppurchasefacapp.Controls.Add(Me.ogSupervisorApproved)
        Me.otppurchasefacapp.Name = "otppurchasefacapp"
        Me.otppurchasefacapp.PageVisible = False
        Me.otppurchasefacapp.Size = New System.Drawing.Size(1470, 770)
        Me.otppurchasefacapp.Text = "Approve PO Factory"
        '
        'ogSupervisorApproved
        '
        Me.ogSupervisorApproved.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogSupervisorApproved.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4)
        Me.ogSupervisorApproved.Location = New System.Drawing.Point(0, 0)
        Me.ogSupervisorApproved.MainView = Me.ogvSupervisorApproved
        Me.ogSupervisorApproved.Margin = New System.Windows.Forms.Padding(4)
        Me.ogSupervisorApproved.Name = "ogSupervisorApproved"
        Me.ogSupervisorApproved.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCheckEdit2})
        Me.ogSupervisorApproved.Size = New System.Drawing.Size(1470, 770)
        Me.ogSupervisorApproved.TabIndex = 1
        Me.ogSupervisorApproved.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvSupervisorApproved})
        '
        'ogvSupervisorApproved
        '
        Me.ogvSupervisorApproved.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.ColFTStateApproved, Me.ColFTPurchaseBy, Me.ColFTSuperVisorName, Me.GridColumn3, Me.ColFTPurchaseNo, Me.ColFDPurchaseDate, Me.ColFTPurchaseState, Me.ColFTCmpRunCode, Me.ColFTCmpRunName, Me.ColFTSuplCode, Me.ColFTSuplName, Me.ColFDDeliveryDate, Me.ColFTCrTermCode, Me.ColFTCrTermDesc, Me.ColFNCreditDay, Me.ColFTTermOfPMCode, Me.ColFTTermOfPMName, Me.ColFTDeliveryCode, Me.ColFTDeliveryDesc, Me.FTCurCode, Me.ColFNExchangeRate, Me.ColFTContactPerson, Me.ColFNDisCountAmt, Me.ColFNSurcharge, Me.ColFNVatAmt, Me.ColFNPOGrandAmt, Me.FTQuantityinfo, Me.ColFTRemark, Me.ColFNDisCountPer, Me.ColFNPONetAmt, Me.ColFNVatPer, Me.ColFTTeamGrpCode, Me.ColFTTeamGrpName, Me.ColFTPurGrpCode, Me.ColFTPurGrpName, Me.FTPoTypeState, Me.FTStateFree})
        Me.ogvSupervisorApproved.GridControl = Me.ogSupervisorApproved
        Me.ogvSupervisorApproved.Name = "ogvSupervisorApproved"
        Me.ogvSupervisorApproved.OptionsView.ColumnAutoWidth = False
        Me.ogvSupervisorApproved.OptionsView.ShowGroupPanel = False
        '
        'ColFTStateApproved
        '
        Me.ColFTStateApproved.Caption = "FTStateApproved"
        Me.ColFTStateApproved.ColumnEdit = Me.RepositoryItemCheckEdit2
        Me.ColFTStateApproved.FieldName = "FTStateApproved"
        Me.ColFTStateApproved.Name = "ColFTStateApproved"
        Me.ColFTStateApproved.Visible = True
        Me.ColFTStateApproved.VisibleIndex = 0
        Me.ColFTStateApproved.Width = 50
        '
        'RepositoryItemCheckEdit2
        '
        Me.RepositoryItemCheckEdit2.AutoHeight = False
        Me.RepositoryItemCheckEdit2.Caption = "Check"
        Me.RepositoryItemCheckEdit2.Name = "RepositoryItemCheckEdit2"
        Me.RepositoryItemCheckEdit2.ValueChecked = "1"
        Me.RepositoryItemCheckEdit2.ValueUnchecked = "0"
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
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Style Code"
        Me.GridColumn3.FieldName = "FTStyleCode"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.OptionsColumn.AllowEdit = False
        Me.GridColumn3.OptionsColumn.ReadOnly = True
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 3
        Me.GridColumn3.Width = 120
        '
        'ColFTPurchaseNo
        '
        Me.ColFTPurchaseNo.Caption = "FTPurchaseNo"
        Me.ColFTPurchaseNo.FieldName = "FTPurchaseNo"
        Me.ColFTPurchaseNo.Name = "ColFTPurchaseNo"
        Me.ColFTPurchaseNo.OptionsColumn.AllowEdit = False
        Me.ColFTPurchaseNo.OptionsColumn.ReadOnly = True
        Me.ColFTPurchaseNo.Visible = True
        Me.ColFTPurchaseNo.VisibleIndex = 4
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
        Me.ColFDPurchaseDate.VisibleIndex = 5
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
        Me.ColFTSuplCode.VisibleIndex = 6
        '
        'ColFTSuplName
        '
        Me.ColFTSuplName.Caption = "FTSuplName"
        Me.ColFTSuplName.FieldName = "FTSuplName"
        Me.ColFTSuplName.Name = "ColFTSuplName"
        Me.ColFTSuplName.OptionsColumn.AllowEdit = False
        Me.ColFTSuplName.OptionsColumn.ReadOnly = True
        Me.ColFTSuplName.Visible = True
        Me.ColFTSuplName.VisibleIndex = 7
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
        Me.ColFTDeliveryCode.VisibleIndex = 8
        '
        'ColFTDeliveryDesc
        '
        Me.ColFTDeliveryDesc.Caption = "FTDeliveryDesc"
        Me.ColFTDeliveryDesc.FieldName = "FTDeliveryDesc"
        Me.ColFTDeliveryDesc.Name = "ColFTDeliveryDesc"
        Me.ColFTDeliveryDesc.OptionsColumn.AllowEdit = False
        Me.ColFTDeliveryDesc.OptionsColumn.ReadOnly = True
        Me.ColFTDeliveryDesc.Visible = True
        Me.ColFTDeliveryDesc.VisibleIndex = 9
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
        Me.FTCurCode.VisibleIndex = 10
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
        Me.ColFNExchangeRate.VisibleIndex = 11
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
        Me.ColFNDisCountAmt.VisibleIndex = 12
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
        Me.ColFNSurcharge.VisibleIndex = 13
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
        Me.ColFNVatAmt.VisibleIndex = 14
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
        Me.ColFNPOGrandAmt.VisibleIndex = 15
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
        Me.FTQuantityinfo.VisibleIndex = 16
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
        Me.ColFTRemark.VisibleIndex = 17
        Me.ColFTRemark.Width = 127
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
        'wFactoryManagerApproved
        '
        Me.Appearance.Options.UseFont = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1478, 900)
        Me.Controls.Add(Me.ogbmainprocbutton)
        Me.Controls.Add(Me.otbmain)
        Me.Controls.Add(Me.StandaloneBarDockControl)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.MainRibbonControl)
        Me.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.IsMdiContainer = True
        Me.Margin = New System.Windows.Forms.Padding(6, 5, 6, 5)
        Me.Name = "wFactoryManagerApproved"
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
        Me.otpappfactoryCM.ResumeLayout(False)
        CType(Me.GroupControl3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl3.ResumeLayout(False)
        CType(Me.ogcAppFactoryCM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvAppFactoryCM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemTextEdit3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ockAppCMselectall.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.otpappfactory.ResumeLayout(False)
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        CType(Me.ogcdirector, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvdirector, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositorySelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ockdirectorselectall.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.otpappcminv.ResumeLayout(False)
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl2.ResumeLayout(False)
        CType(Me.ogccminv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvcminv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemTextEdit2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTSelectAllCMInv.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.otppurchasefacapp.ResumeLayout(False)
        CType(Me.ogSupervisorApproved, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvSupervisorApproved, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit2, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents ocmpreview As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmreject As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmapprove As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents otmchkpo As System.Windows.Forms.Timer
    Friend WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmsavelayout As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents otbmain As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents otpappfactory As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
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
    Friend WithEvents ockdirectorselectall As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents FNCM As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNGrandAmtCM As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents otpappcminv As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents GroupControl2 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ogccminv As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvcminv As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemCheckEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents cFTCmpCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTCompName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTCustomerPO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTStyleCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemTextEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents cFNAmount As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNCM As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTSelectAllCMInv As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents cFTInvoiceNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFDInvoiceDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTStateSendBy As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents otppurchasefacapp As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents ogSupervisorApproved As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvSupervisorApproved As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents ColFTStateApproved As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemCheckEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents ColFTPurchaseBy As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFTSuperVisorName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
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
    Friend WithEvents FTCurCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFNExchangeRate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFTContactPerson As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFNDisCountAmt As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFNSurcharge As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFNVatAmt As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFNPOGrandAmt As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTQuantityinfo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFTRemark As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFNDisCountPer As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFNPONetAmt As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFNVatPer As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFTTeamGrpCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFTTeamGrpName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFTPurGrpCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFTPurGrpName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTPoTypeState As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTStateFree As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFTColorway As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFTNikePOLineItem As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFTSizeBreakDown As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents C2FTColorway As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents C2FTNikePOLineItem As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents C2FTSizeBreakDown As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents otpappfactoryCM As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents GroupControl3 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ogcAppFactoryCM As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvAppFactoryCM As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cmFTSelect As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemCheckEdit3 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents cmFTCmpCodeTo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cmFTCmpCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cmFTCompNameTo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cmFTPORef As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn11 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cmFTStyleCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cmFTOrderNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn14 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cmFTCustCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cmFTCustName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn17 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cmFTBuyCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn19 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cmFTSeasonCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn21 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cmFTProdTypeCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cmFTProdTypeName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cmFDShipDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cmFNQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemTextEdit3 As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents cmFNAmt As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cmFNExtraQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cmFNAmntExtra As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cmFNGarmentQtyTest As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cmFNAmntQtyTest As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cmFTNikePOLineItem As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cmFNTotalQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ockAppCMselectall As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents cmFNSeq As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cmFTCompName As DevExpress.XtraGrid.Columns.GridColumn
End Class

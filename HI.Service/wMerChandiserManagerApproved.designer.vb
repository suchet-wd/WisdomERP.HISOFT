<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wMerChandiserManagerApproved
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
        Me.otpappcminv = New DevExpress.XtraTab.XtraTabPage()
        Me.GroupControl2 = New DevExpress.XtraEditors.GroupControl()
        Me.ogccminv = New DevExpress.XtraGrid.GridControl()
        Me.ogvcminv = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.FTSelect = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ReposSelect = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.CFNHSysCmpId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTCmpCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTCompName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTCustomerPO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFNHSysStyleId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTStyleCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTInvoiceNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFDInvoiceDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTCustCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTCustName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTInvoiceExportNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FDInvoiceExportDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemTextEdit2 = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.FNCM = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNCMAmt = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNNetCM = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNNetCMAmt = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNFirstPrice = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNFirstPriceAmt = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTSelectAllCMInv = New DevExpress.XtraEditors.CheckEdit()
        CType(Me.MainRibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemComboBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.oDockManager, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbmainprocbutton.SuspendLayout()
        CType(Me.otbmain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.otbmain.SuspendLayout()
        Me.otpappcminv.SuspendLayout()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl2.SuspendLayout()
        CType(Me.ogccminv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvcminv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ReposSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemTextEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTSelectAllCMInv.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.MainRibbonControl.Size = New System.Drawing.Size(1478, 57)
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
        Me.StandaloneBarDockControl.Location = New System.Drawing.Point(0, 57)
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
        Me.ocmreject.Visible = False
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
        Me.otbmain.Location = New System.Drawing.Point(0, 57)
        Me.otbmain.Name = "otbmain"
        Me.otbmain.SelectedTabPage = Me.otpappcminv
        Me.otbmain.Size = New System.Drawing.Size(1478, 816)
        Me.otbmain.TabIndex = 14
        Me.otbmain.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.otpappcminv})
        '
        'otpappcminv
        '
        Me.otpappcminv.Controls.Add(Me.GroupControl2)
        Me.otpappcminv.Margin = New System.Windows.Forms.Padding(4)
        Me.otpappcminv.Name = "otpappcminv"
        Me.otpappcminv.Size = New System.Drawing.Size(1470, 783)
        Me.otpappcminv.Text = "Approved  Transaction Value Work Sheet"
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
        Me.GroupControl2.Size = New System.Drawing.Size(1470, 783)
        Me.GroupControl2.TabIndex = 12
        Me.GroupControl2.Text = "Approved  Transaction Value Work Sheet"
        '
        'ogccminv
        '
        Me.ogccminv.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogccminv.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogccminv.Location = New System.Drawing.Point(2, 25)
        Me.ogccminv.MainView = Me.ogvcminv
        Me.ogccminv.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogccminv.Name = "ogccminv"
        Me.ogccminv.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemTextEdit2, Me.ReposSelect})
        Me.ogccminv.Size = New System.Drawing.Size(1466, 756)
        Me.ogccminv.TabIndex = 22
        Me.ogccminv.TabStop = False
        Me.ogccminv.Tag = "2|"
        Me.ogccminv.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvcminv})
        '
        'ogvcminv
        '
        Me.ogvcminv.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.FTSelect, Me.CFNHSysCmpId, Me.cFTCmpCode, Me.cFTCompName, Me.cFTCustomerPO, Me.CFNHSysStyleId, Me.cFTStyleCode, Me.cFTInvoiceNo, Me.cFDInvoiceDate, Me.FTCustCode, Me.FTCustName, Me.FTInvoiceExportNo, Me.FDInvoiceExportDate, Me.cFNQuantity, Me.FNCM, Me.FNCMAmt, Me.FNNetCM, Me.FNNetCMAmt, Me.FNFirstPrice, Me.FNFirstPriceAmt})
        Me.ogvcminv.CustomizationFormBounds = New System.Drawing.Rectangle(758, 512, 216, 178)
        Me.ogvcminv.GridControl = Me.ogccminv
        Me.ogvcminv.GroupCount = 1
        Me.ogvcminv.GroupSummary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FNQuantity", Me.cFNQuantity, "{0:n0}"), New DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FNCMAmt", Me.FNCMAmt, "{0:n2}"), New DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FNNetCMAmt", Me.FNNetCMAmt, "{0:n2}"), New DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FNFirstPriceAmt", Me.FNFirstPriceAmt, "{0:n2}")})
        Me.ogvcminv.Name = "ogvcminv"
        Me.ogvcminv.OptionsBehavior.AutoExpandAllGroups = True
        Me.ogvcminv.OptionsCustomization.AllowQuickHideColumns = False
        Me.ogvcminv.OptionsNavigation.EnterMoveNextColumn = True
        Me.ogvcminv.OptionsView.ColumnAutoWidth = False
        Me.ogvcminv.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.ogvcminv.OptionsView.ShowAutoFilterRow = True
        Me.ogvcminv.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
        Me.ogvcminv.OptionsView.ShowFooter = True
        Me.ogvcminv.OptionsView.ShowGroupPanel = False
        Me.ogvcminv.SortInfo.AddRange(New DevExpress.XtraGrid.Columns.GridColumnSortInfo() {New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.cFTCmpCode, DevExpress.Data.ColumnSortOrder.Ascending)})
        Me.ogvcminv.Tag = "2|"
        '
        'FTSelect
        '
        Me.FTSelect.Caption = " "
        Me.FTSelect.ColumnEdit = Me.ReposSelect
        Me.FTSelect.FieldName = "FTSelect"
        Me.FTSelect.Name = "FTSelect"
        Me.FTSelect.OptionsColumn.AllowMove = False
        Me.FTSelect.OptionsColumn.AllowShowHide = False
        Me.FTSelect.OptionsColumn.ShowInCustomizationForm = False
        Me.FTSelect.Visible = True
        Me.FTSelect.VisibleIndex = 0
        '
        'ReposSelect
        '
        Me.ReposSelect.AutoHeight = False
        Me.ReposSelect.Caption = "Check"
        Me.ReposSelect.Name = "ReposSelect"
        Me.ReposSelect.ValueChecked = "1"
        Me.ReposSelect.ValueUnchecked = "0"
        '
        'CFNHSysCmpId
        '
        Me.CFNHSysCmpId.Caption = "FNHSysCmpId"
        Me.CFNHSysCmpId.FieldName = "FNHSysCmpId"
        Me.CFNHSysCmpId.Name = "CFNHSysCmpId"
        Me.CFNHSysCmpId.OptionsColumn.AllowEdit = False
        Me.CFNHSysCmpId.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFNHSysCmpId.OptionsColumn.ReadOnly = True
        '
        'cFTCmpCode
        '
        Me.cFTCmpCode.Caption = "FTCmpCode"
        Me.cFTCmpCode.FieldName = "FTCmpCode"
        Me.cFTCmpCode.GroupInterval = DevExpress.XtraGrid.ColumnGroupInterval.Value
        Me.cFTCmpCode.Name = "cFTCmpCode"
        Me.cFTCmpCode.OptionsColumn.AllowEdit = False
        Me.cFTCmpCode.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.cFTCmpCode.OptionsColumn.ReadOnly = True
        Me.cFTCmpCode.Visible = True
        Me.cFTCmpCode.VisibleIndex = 0
        '
        'cFTCompName
        '
        Me.cFTCompName.Caption = "FTCompName"
        Me.cFTCompName.FieldName = "FTCmpName"
        Me.cFTCompName.Name = "cFTCompName"
        Me.cFTCompName.OptionsColumn.AllowEdit = False
        Me.cFTCompName.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.cFTCompName.OptionsColumn.ReadOnly = True
        Me.cFTCompName.Visible = True
        Me.cFTCompName.VisibleIndex = 1
        Me.cFTCompName.Width = 54
        '
        'cFTCustomerPO
        '
        Me.cFTCustomerPO.Caption = "FTCustomerPO"
        Me.cFTCustomerPO.FieldName = "FTCustomerPO"
        Me.cFTCustomerPO.Name = "cFTCustomerPO"
        Me.cFTCustomerPO.OptionsColumn.AllowEdit = False
        Me.cFTCustomerPO.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.cFTCustomerPO.OptionsColumn.ReadOnly = True
        Me.cFTCustomerPO.Visible = True
        Me.cFTCustomerPO.VisibleIndex = 2
        Me.cFTCustomerPO.Width = 38
        '
        'CFNHSysStyleId
        '
        Me.CFNHSysStyleId.Caption = "FNHSysStyleId"
        Me.CFNHSysStyleId.FieldName = "FNHSysStyleId"
        Me.CFNHSysStyleId.Name = "CFNHSysStyleId"
        Me.CFNHSysStyleId.OptionsColumn.AllowEdit = False
        Me.CFNHSysStyleId.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[True]
        Me.CFNHSysStyleId.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFNHSysStyleId.OptionsColumn.ReadOnly = True
        '
        'cFTStyleCode
        '
        Me.cFTStyleCode.Caption = "FTStyleCode"
        Me.cFTStyleCode.FieldName = "FTStyleCode"
        Me.cFTStyleCode.Name = "cFTStyleCode"
        Me.cFTStyleCode.OptionsColumn.AllowEdit = False
        Me.cFTStyleCode.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.cFTStyleCode.OptionsColumn.ReadOnly = True
        Me.cFTStyleCode.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        Me.cFTStyleCode.Visible = True
        Me.cFTStyleCode.VisibleIndex = 3
        Me.cFTStyleCode.Width = 38
        '
        'cFTInvoiceNo
        '
        Me.cFTInvoiceNo.Caption = "FTInvoiceNo"
        Me.cFTInvoiceNo.FieldName = "FTInvoiceNo"
        Me.cFTInvoiceNo.Name = "cFTInvoiceNo"
        Me.cFTInvoiceNo.OptionsColumn.AllowEdit = False
        Me.cFTInvoiceNo.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.cFTInvoiceNo.OptionsColumn.ReadOnly = True
        Me.cFTInvoiceNo.Visible = True
        Me.cFTInvoiceNo.VisibleIndex = 4
        Me.cFTInvoiceNo.Width = 38
        '
        'cFDInvoiceDate
        '
        Me.cFDInvoiceDate.Caption = "FDInvoiceDate"
        Me.cFDInvoiceDate.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.cFDInvoiceDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.cFDInvoiceDate.FieldName = "FDInvoiceDate"
        Me.cFDInvoiceDate.Name = "cFDInvoiceDate"
        Me.cFDInvoiceDate.OptionsColumn.AllowEdit = False
        Me.cFDInvoiceDate.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.cFDInvoiceDate.OptionsColumn.ReadOnly = True
        Me.cFDInvoiceDate.Visible = True
        Me.cFDInvoiceDate.VisibleIndex = 5
        Me.cFDInvoiceDate.Width = 38
        '
        'FTCustCode
        '
        Me.FTCustCode.Caption = "FTCustCode"
        Me.FTCustCode.FieldName = "FTCustCode"
        Me.FTCustCode.Name = "FTCustCode"
        Me.FTCustCode.OptionsColumn.AllowEdit = False
        Me.FTCustCode.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTCustCode.OptionsColumn.ReadOnly = True
        Me.FTCustCode.Visible = True
        Me.FTCustCode.VisibleIndex = 6
        '
        'FTCustName
        '
        Me.FTCustName.Caption = "FTCustName"
        Me.FTCustName.FieldName = "FTCustName"
        Me.FTCustName.Name = "FTCustName"
        Me.FTCustName.OptionsColumn.AllowEdit = False
        Me.FTCustName.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTCustName.OptionsColumn.ReadOnly = True
        Me.FTCustName.Visible = True
        Me.FTCustName.VisibleIndex = 7
        '
        'FTInvoiceExportNo
        '
        Me.FTInvoiceExportNo.Caption = "FTInvoiceExportNo"
        Me.FTInvoiceExportNo.FieldName = "FTInvoiceExportNo"
        Me.FTInvoiceExportNo.Name = "FTInvoiceExportNo"
        Me.FTInvoiceExportNo.OptionsColumn.AllowEdit = False
        Me.FTInvoiceExportNo.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTInvoiceExportNo.OptionsColumn.ReadOnly = True
        Me.FTInvoiceExportNo.Visible = True
        Me.FTInvoiceExportNo.VisibleIndex = 8
        '
        'FDInvoiceExportDate
        '
        Me.FDInvoiceExportDate.Caption = "FDInvoiceExportDate"
        Me.FDInvoiceExportDate.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.FDInvoiceExportDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.FDInvoiceExportDate.FieldName = "FDInvoiceExportDate"
        Me.FDInvoiceExportDate.Name = "FDInvoiceExportDate"
        Me.FDInvoiceExportDate.OptionsColumn.AllowEdit = False
        Me.FDInvoiceExportDate.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FDInvoiceExportDate.OptionsColumn.ReadOnly = True
        Me.FDInvoiceExportDate.Visible = True
        Me.FDInvoiceExportDate.VisibleIndex = 9
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
        Me.cFNQuantity.Width = 38
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
        'FNCM
        '
        Me.FNCM.Caption = "FNCM"
        Me.FNCM.DisplayFormat.FormatString = "{0:n4}"
        Me.FNCM.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNCM.FieldName = "FNCM"
        Me.FNCM.Name = "FNCM"
        Me.FNCM.OptionsColumn.AllowEdit = False
        Me.FNCM.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNCM.OptionsColumn.ReadOnly = True
        Me.FNCM.Visible = True
        Me.FNCM.VisibleIndex = 11
        '
        'FNCMAmt
        '
        Me.FNCMAmt.Caption = "FNCMAmt"
        Me.FNCMAmt.DisplayFormat.FormatString = "{0:n2}"
        Me.FNCMAmt.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNCMAmt.FieldName = "FNCMAmt"
        Me.FNCMAmt.Name = "FNCMAmt"
        Me.FNCMAmt.OptionsColumn.AllowEdit = False
        Me.FNCMAmt.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNCMAmt.OptionsColumn.ReadOnly = True
        Me.FNCMAmt.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FNCMAmt", "{0:n2}")})
        Me.FNCMAmt.Visible = True
        Me.FNCMAmt.VisibleIndex = 12
        Me.FNCMAmt.Width = 38
        '
        'FNNetCM
        '
        Me.FNNetCM.Caption = "FNNetCM"
        Me.FNNetCM.DisplayFormat.FormatString = "{0:n4}"
        Me.FNNetCM.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNNetCM.FieldName = "FNNetCM"
        Me.FNNetCM.Name = "FNNetCM"
        Me.FNNetCM.OptionsColumn.AllowEdit = False
        Me.FNNetCM.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNNetCM.OptionsColumn.ReadOnly = True
        Me.FNNetCM.Visible = True
        Me.FNNetCM.VisibleIndex = 13
        '
        'FNNetCMAmt
        '
        Me.FNNetCMAmt.Caption = "FNNetCMAmt"
        Me.FNNetCMAmt.DisplayFormat.FormatString = "{0:n2}"
        Me.FNNetCMAmt.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNNetCMAmt.FieldName = "FNNetCMAmt"
        Me.FNNetCMAmt.Name = "FNNetCMAmt"
        Me.FNNetCMAmt.OptionsColumn.AllowEdit = False
        Me.FNNetCMAmt.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNNetCMAmt.OptionsColumn.ReadOnly = True
        Me.FNNetCMAmt.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FNNetCMAmt", "{0:n2}")})
        Me.FNNetCMAmt.Visible = True
        Me.FNNetCMAmt.VisibleIndex = 14
        '
        'FNFirstPrice
        '
        Me.FNFirstPrice.Caption = "FNFirstPrice"
        Me.FNFirstPrice.DisplayFormat.FormatString = "{0:n4}"
        Me.FNFirstPrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNFirstPrice.FieldName = "FNFirstPrice"
        Me.FNFirstPrice.Name = "FNFirstPrice"
        Me.FNFirstPrice.OptionsColumn.AllowEdit = False
        Me.FNFirstPrice.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNFirstPrice.OptionsColumn.ReadOnly = True
        Me.FNFirstPrice.Visible = True
        Me.FNFirstPrice.VisibleIndex = 15
        '
        'FNFirstPriceAmt
        '
        Me.FNFirstPriceAmt.Caption = "FNFirstPriceAmt"
        Me.FNFirstPriceAmt.DisplayFormat.FormatString = "{0:n2}"
        Me.FNFirstPriceAmt.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNFirstPriceAmt.FieldName = "FNFirstPriceAmt"
        Me.FNFirstPriceAmt.Name = "FNFirstPriceAmt"
        Me.FNFirstPriceAmt.OptionsColumn.AllowEdit = False
        Me.FNFirstPriceAmt.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNFirstPriceAmt.OptionsColumn.ReadOnly = True
        Me.FNFirstPriceAmt.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FNFirstPriceAmt", "{0:n2}")})
        Me.FNFirstPriceAmt.Visible = True
        Me.FNFirstPriceAmt.VisibleIndex = 16
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
        'wMerChandiserManagerApproved
        '
        Me.Appearance.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Appearance.Options.UseFont = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(11.0!, 21.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1478, 900)
        Me.Controls.Add(Me.ogbmainprocbutton)
        Me.Controls.Add(Me.otbmain)
        Me.Controls.Add(Me.StandaloneBarDockControl)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.MainRibbonControl)
        Me.IsMdiContainer = True
        Me.Margin = New System.Windows.Forms.Padding(6, 5, 6, 5)
        Me.Name = "wMerChandiserManagerApproved"
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
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl2.ResumeLayout(False)
        CType(Me.ogccminv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvcminv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ReposSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemTextEdit2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTSelectAllCMInv.Properties, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents otpappcminv As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents GroupControl2 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents FTSelectAllCMInv As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents ogccminv As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvcminv As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents CFNHSysCmpId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTCmpCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTCompName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTCustomerPO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFNHSysStyleId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTStyleCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTInvoiceNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFDInvoiceDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTCustCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTCustName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTInvoiceExportNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FDInvoiceExportDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemTextEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents FNCM As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNCMAmt As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNNetCM As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNNetCMAmt As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNFirstPrice As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNFirstPriceAmt As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ReposSelect As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents FTSelect As DevExpress.XtraGrid.Columns.GridColumn


End Class

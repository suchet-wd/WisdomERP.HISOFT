<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wQAFinalLeaderApproved
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
        Me.ocmpreview = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmreject = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmapprove = New DevExpress.XtraEditors.SimpleButton()
        Me.otmchkpo = New System.Windows.Forms.Timer()
        Me.otbmain = New DevExpress.XtraTab.XtraTabControl()
        Me.otpappqafinal = New DevExpress.XtraTab.XtraTabPage()
        Me.GroupControl2 = New DevExpress.XtraEditors.GroupControl()
        Me.ogcqafinal = New DevExpress.XtraGrid.GridControl()
        Me.ogvqafinal = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.FTSelect = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCheckEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.FTQANikeAuditNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FDQANikeAuditqDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTQANikeAuditBy = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FDQADate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTUnitSectCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTStyleCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTSeasonCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTOrderNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTSubOrderNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNINQTY = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNAQL = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNActualQty = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNMajorQty = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNMinorQty = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNAndon = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNReject = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNDefect = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemTextEdit2 = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.ockSelectAll = New DevExpress.XtraEditors.CheckEdit()
        CType(Me.MainRibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemComboBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.oDockManager, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbmainprocbutton.SuspendLayout()
        CType(Me.otbmain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.otbmain.SuspendLayout()
        Me.otpappqafinal.SuspendLayout()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl2.SuspendLayout()
        CType(Me.ogcqafinal, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvqafinal, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemTextEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ockSelectAll.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.MainRibbonControl.Size = New System.Drawing.Size(1478, 52)
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
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 869)
        Me.RibbonStatusBar.Margin = New System.Windows.Forms.Padding(6, 5, 6, 5)
        Me.RibbonStatusBar.Name = "RibbonStatusBar"
        Me.RibbonStatusBar.Ribbon = Me.MainRibbonControl
        Me.RibbonStatusBar.Size = New System.Drawing.Size(1478, 31)
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
        Me.StandaloneBarDockControl.Location = New System.Drawing.Point(0, 52)
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
        Me.ocmpreview.Visible = False
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
        Me.otbmain.Location = New System.Drawing.Point(0, 52)
        Me.otbmain.Name = "otbmain"
        Me.otbmain.SelectedTabPage = Me.otpappqafinal
        Me.otbmain.Size = New System.Drawing.Size(1478, 817)
        Me.otbmain.TabIndex = 14
        Me.otbmain.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.otpappqafinal})
        '
        'otpappqafinal
        '
        Me.otpappqafinal.Controls.Add(Me.GroupControl2)
        Me.otpappqafinal.Margin = New System.Windows.Forms.Padding(4)
        Me.otpappqafinal.Name = "otpappqafinal"
        Me.otpappqafinal.Size = New System.Drawing.Size(1472, 786)
        Me.otpappqafinal.Text = "Approved QA Final"
        '
        'GroupControl2
        '
        Me.GroupControl2.AppearanceCaption.Options.UseTextOptions = True
        Me.GroupControl2.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GroupControl2.Controls.Add(Me.ogcqafinal)
        Me.GroupControl2.Controls.Add(Me.ockSelectAll)
        Me.GroupControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl2.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl2.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupControl2.Name = "GroupControl2"
        Me.GroupControl2.Size = New System.Drawing.Size(1472, 786)
        Me.GroupControl2.TabIndex = 12
        Me.GroupControl2.Text = "Approved QA Final"
        '
        'ogcqafinal
        '
        Me.ogcqafinal.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogcqafinal.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcqafinal.Location = New System.Drawing.Point(2, 24)
        Me.ogcqafinal.MainView = Me.ogvqafinal
        Me.ogcqafinal.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcqafinal.Name = "ogcqafinal"
        Me.ogcqafinal.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemTextEdit2, Me.RepositoryItemCheckEdit1})
        Me.ogcqafinal.Size = New System.Drawing.Size(1468, 760)
        Me.ogcqafinal.TabIndex = 21
        Me.ogcqafinal.TabStop = False
        Me.ogcqafinal.Tag = "2|"
        Me.ogcqafinal.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvqafinal})
        '
        'ogvqafinal
        '
        Me.ogvqafinal.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.FTSelect, Me.FTQANikeAuditNo, Me.FDQANikeAuditqDate, Me.FTQANikeAuditBy, Me.FDQADate, Me.FTUnitSectCode, Me.FTStyleCode, Me.FTSeasonCode, Me.FTOrderNo, Me.FTSubOrderNo, Me.FNINQTY, Me.FNAQL, Me.FNActualQty, Me.FNMajorQty, Me.FNMinorQty, Me.FNAndon, Me.FNReject, Me.FNDefect})
        Me.ogvqafinal.CustomizationFormBounds = New System.Drawing.Rectangle(758, 512, 216, 178)
        Me.ogvqafinal.GridControl = Me.ogcqafinal
        Me.ogvqafinal.GroupSummary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FNExtraQuantity", Nothing, "{0:n0}"), New DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FNTotalQuantity", Nothing, "{0:n0}")})
        Me.ogvqafinal.Name = "ogvqafinal"
        Me.ogvqafinal.OptionsBehavior.AutoExpandAllGroups = True
        Me.ogvqafinal.OptionsCustomization.AllowQuickHideColumns = False
        Me.ogvqafinal.OptionsNavigation.EnterMoveNextColumn = True
        Me.ogvqafinal.OptionsView.ColumnAutoWidth = False
        Me.ogvqafinal.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
        Me.ogvqafinal.OptionsView.ShowFooter = True
        Me.ogvqafinal.OptionsView.ShowGroupPanel = False
        Me.ogvqafinal.Tag = "2|"
        '
        'FTSelect
        '
        Me.FTSelect.Caption = " "
        Me.FTSelect.ColumnEdit = Me.RepositoryItemCheckEdit1
        Me.FTSelect.FieldName = "FTSelect"
        Me.FTSelect.Name = "FTSelect"
        Me.FTSelect.Visible = True
        Me.FTSelect.VisibleIndex = 0
        Me.FTSelect.Width = 62
        '
        'RepositoryItemCheckEdit1
        '
        Me.RepositoryItemCheckEdit1.AutoHeight = False
        Me.RepositoryItemCheckEdit1.Caption = "Check"
        Me.RepositoryItemCheckEdit1.Name = "RepositoryItemCheckEdit1"
        Me.RepositoryItemCheckEdit1.ValueChecked = "1"
        Me.RepositoryItemCheckEdit1.ValueUnchecked = "0"
        '
        'FTQANikeAuditNo
        '
        Me.FTQANikeAuditNo.Caption = "QA Nike Audit No"
        Me.FTQANikeAuditNo.FieldName = "FTQANikeAuditNo"
        Me.FTQANikeAuditNo.Name = "FTQANikeAuditNo"
        Me.FTQANikeAuditNo.OptionsColumn.AllowEdit = False
        Me.FTQANikeAuditNo.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTQANikeAuditNo.OptionsColumn.ReadOnly = True
        Me.FTQANikeAuditNo.Visible = True
        Me.FTQANikeAuditNo.VisibleIndex = 1
        Me.FTQANikeAuditNo.Width = 99
        '
        'FDQANikeAuditqDate
        '
        Me.FDQANikeAuditqDate.Caption = "QA Nike Audit Date"
        Me.FDQANikeAuditqDate.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.FDQANikeAuditqDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.FDQANikeAuditqDate.FieldName = "FDQANikeAuditqDate"
        Me.FDQANikeAuditqDate.Name = "FDQANikeAuditqDate"
        Me.FDQANikeAuditqDate.OptionsColumn.AllowEdit = False
        Me.FDQANikeAuditqDate.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FDQANikeAuditqDate.OptionsColumn.ReadOnly = True
        Me.FDQANikeAuditqDate.Visible = True
        Me.FDQANikeAuditqDate.VisibleIndex = 2
        Me.FDQANikeAuditqDate.Width = 93
        '
        'FTQANikeAuditBy
        '
        Me.FTQANikeAuditBy.Caption = "FTQANikeAuditBy"
        Me.FTQANikeAuditBy.FieldName = "FTQANikeAuditBy"
        Me.FTQANikeAuditBy.Name = "FTQANikeAuditBy"
        Me.FTQANikeAuditBy.OptionsColumn.AllowEdit = False
        Me.FTQANikeAuditBy.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTQANikeAuditBy.OptionsColumn.ReadOnly = True
        Me.FTQANikeAuditBy.Visible = True
        Me.FTQANikeAuditBy.VisibleIndex = 3
        Me.FTQANikeAuditBy.Width = 98
        '
        'FDQADate
        '
        Me.FDQADate.Caption = "FDQADate"
        Me.FDQADate.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.FDQADate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.FDQADate.FieldName = "FDQADate"
        Me.FDQADate.Name = "FDQADate"
        Me.FDQADate.OptionsColumn.AllowEdit = False
        Me.FDQADate.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FDQADate.OptionsColumn.ReadOnly = True
        Me.FDQADate.Visible = True
        Me.FDQADate.VisibleIndex = 4
        Me.FDQADate.Width = 77
        '
        'FTUnitSectCode
        '
        Me.FTUnitSectCode.Caption = "UnitSectCode"
        Me.FTUnitSectCode.FieldName = "FTUnitSectCode"
        Me.FTUnitSectCode.Name = "FTUnitSectCode"
        Me.FTUnitSectCode.OptionsColumn.AllowEdit = False
        Me.FTUnitSectCode.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTUnitSectCode.OptionsColumn.ReadOnly = True
        Me.FTUnitSectCode.Visible = True
        Me.FTUnitSectCode.VisibleIndex = 5
        Me.FTUnitSectCode.Width = 76
        '
        'FTStyleCode
        '
        Me.FTStyleCode.Caption = "FTStyleCode"
        Me.FTStyleCode.FieldName = "FTStyleCode"
        Me.FTStyleCode.Name = "FTStyleCode"
        Me.FTStyleCode.OptionsColumn.AllowEdit = False
        Me.FTStyleCode.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTStyleCode.OptionsColumn.ReadOnly = True
        Me.FTStyleCode.Visible = True
        Me.FTStyleCode.VisibleIndex = 6
        Me.FTStyleCode.Width = 80
        '
        'FTSeasonCode
        '
        Me.FTSeasonCode.Caption = "FTSeasonCode"
        Me.FTSeasonCode.FieldName = "FTSeasonCode"
        Me.FTSeasonCode.Name = "FTSeasonCode"
        Me.FTSeasonCode.OptionsColumn.AllowEdit = False
        Me.FTSeasonCode.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTSeasonCode.OptionsColumn.ReadOnly = True
        Me.FTSeasonCode.Visible = True
        Me.FTSeasonCode.VisibleIndex = 7
        Me.FTSeasonCode.Width = 64
        '
        'FTOrderNo
        '
        Me.FTOrderNo.Caption = "FTOrderNo"
        Me.FTOrderNo.FieldName = "FTOrderNo"
        Me.FTOrderNo.Name = "FTOrderNo"
        Me.FTOrderNo.OptionsColumn.AllowEdit = False
        Me.FTOrderNo.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTOrderNo.OptionsColumn.ReadOnly = True
        Me.FTOrderNo.Visible = True
        Me.FTOrderNo.VisibleIndex = 8
        Me.FTOrderNo.Width = 82
        '
        'FTSubOrderNo
        '
        Me.FTSubOrderNo.Caption = "FTSubOrderNo"
        Me.FTSubOrderNo.FieldName = "FTSubOrderNo"
        Me.FTSubOrderNo.Name = "FTSubOrderNo"
        Me.FTSubOrderNo.OptionsColumn.AllowEdit = False
        Me.FTSubOrderNo.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTSubOrderNo.OptionsColumn.ReadOnly = True
        Me.FTSubOrderNo.Visible = True
        Me.FTSubOrderNo.VisibleIndex = 9
        Me.FTSubOrderNo.Width = 109
        '
        'FNINQTY
        '
        Me.FNINQTY.Caption = "INQTY"
        Me.FNINQTY.DisplayFormat.FormatString = "{0:n0}"
        Me.FNINQTY.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNINQTY.FieldName = "INQTY"
        Me.FNINQTY.Name = "FNINQTY"
        Me.FNINQTY.OptionsColumn.AllowEdit = False
        Me.FNINQTY.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNINQTY.OptionsColumn.ReadOnly = True
        Me.FNINQTY.Visible = True
        Me.FNINQTY.VisibleIndex = 10
        Me.FNINQTY.Width = 80
        '
        'FNAQL
        '
        Me.FNAQL.Caption = "AQL"
        Me.FNAQL.DisplayFormat.FormatString = "{0:n0}"
        Me.FNAQL.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNAQL.FieldName = "AQL"
        Me.FNAQL.Name = "FNAQL"
        Me.FNAQL.OptionsColumn.AllowEdit = False
        Me.FNAQL.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNAQL.OptionsColumn.ReadOnly = True
        Me.FNAQL.Visible = True
        Me.FNAQL.VisibleIndex = 11
        Me.FNAQL.Width = 80
        '
        'FNActualQty
        '
        Me.FNActualQty.Caption = "ActualQty"
        Me.FNActualQty.DisplayFormat.FormatString = "{0:n0}"
        Me.FNActualQty.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNActualQty.FieldName = "ActualQty"
        Me.FNActualQty.Name = "FNActualQty"
        Me.FNActualQty.OptionsColumn.AllowEdit = False
        Me.FNActualQty.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNActualQty.OptionsColumn.ReadOnly = True
        Me.FNActualQty.Visible = True
        Me.FNActualQty.VisibleIndex = 12
        Me.FNActualQty.Width = 80
        '
        'FNMajorQty
        '
        Me.FNMajorQty.Caption = "MajorQty"
        Me.FNMajorQty.DisplayFormat.FormatString = "{0:n0}"
        Me.FNMajorQty.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNMajorQty.FieldName = "MajorQty"
        Me.FNMajorQty.Name = "FNMajorQty"
        Me.FNMajorQty.OptionsColumn.AllowEdit = False
        Me.FNMajorQty.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNMajorQty.OptionsColumn.ReadOnly = True
        Me.FNMajorQty.Visible = True
        Me.FNMajorQty.VisibleIndex = 13
        Me.FNMajorQty.Width = 80
        '
        'FNMinorQty
        '
        Me.FNMinorQty.Caption = "MinorQty"
        Me.FNMinorQty.DisplayFormat.FormatString = "{0:n0}"
        Me.FNMinorQty.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNMinorQty.FieldName = "MinorQty"
        Me.FNMinorQty.Name = "FNMinorQty"
        Me.FNMinorQty.OptionsColumn.AllowEdit = False
        Me.FNMinorQty.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNMinorQty.OptionsColumn.ReadOnly = True
        Me.FNMinorQty.Visible = True
        Me.FNMinorQty.VisibleIndex = 14
        Me.FNMinorQty.Width = 80
        '
        'FNAndon
        '
        Me.FNAndon.Caption = "Andon"
        Me.FNAndon.DisplayFormat.FormatString = "{0:n0}"
        Me.FNAndon.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNAndon.FieldName = "Andon"
        Me.FNAndon.Name = "FNAndon"
        Me.FNAndon.OptionsColumn.AllowEdit = False
        Me.FNAndon.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNAndon.OptionsColumn.ReadOnly = True
        Me.FNAndon.Visible = True
        Me.FNAndon.VisibleIndex = 15
        Me.FNAndon.Width = 80
        '
        'FNReject
        '
        Me.FNReject.Caption = "Reject"
        Me.FNReject.DisplayFormat.FormatString = "{0:n0}"
        Me.FNReject.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNReject.FieldName = "Reject"
        Me.FNReject.Name = "FNReject"
        Me.FNReject.OptionsColumn.AllowEdit = False
        Me.FNReject.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNReject.OptionsColumn.ReadOnly = True
        Me.FNReject.Visible = True
        Me.FNReject.VisibleIndex = 16
        Me.FNReject.Width = 80
        '
        'FNDefect
        '
        Me.FNDefect.Caption = "Total Defect"
        Me.FNDefect.DisplayFormat.FormatString = "{0:n0}"
        Me.FNDefect.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNDefect.FieldName = "Defect"
        Me.FNDefect.Name = "FNDefect"
        Me.FNDefect.OptionsColumn.AllowEdit = False
        Me.FNDefect.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNDefect.OptionsColumn.ReadOnly = True
        Me.FNDefect.Visible = True
        Me.FNDefect.VisibleIndex = 17
        Me.FNDefect.Width = 80
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
        'ockSelectAll
        '
        Me.ockSelectAll.Location = New System.Drawing.Point(28, 1)
        Me.ockSelectAll.Margin = New System.Windows.Forms.Padding(4)
        Me.ockSelectAll.Name = "ockSelectAll"
        Me.ockSelectAll.Properties.Caption = "Select All"
        Me.ockSelectAll.Size = New System.Drawing.Size(230, 20)
        Me.ockSelectAll.TabIndex = 1
        Me.ockSelectAll.Tag = "2|"
        '
        'wQAFinalLeaderApproved
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
        Me.Name = "wQAFinalLeaderApproved"
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
        Me.otpappqafinal.ResumeLayout(False)
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl2.ResumeLayout(False)
        CType(Me.ogcqafinal, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvqafinal, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemTextEdit2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ockSelectAll.Properties, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents otpappqafinal As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents GroupControl2 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ockSelectAll As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents ogcqafinal As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvqafinal As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents FTSelect As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemCheckEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents FTQANikeAuditNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FDQANikeAuditqDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTQANikeAuditBy As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FDQADate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTUnitSectCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTStyleCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTSeasonCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTOrderNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTSubOrderNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNINQTY As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNAQL As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNActualQty As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNMajorQty As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNMinorQty As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNAndon As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNReject As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNDefect As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemTextEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit


End Class

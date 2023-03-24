<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wSafetyApprovedAssetPR
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
        Me.ogApprovedMail = New DevExpress.XtraEditors.GroupControl()
        Me.ogbmainprocbutton = New DevExpress.XtraEditors.PanelControl()
        Me.ocmsavelayout = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmpreview = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmreject = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmapprove = New DevExpress.XtraEditors.SimpleButton()
        Me.ochkselectall = New DevExpress.XtraEditors.CheckEdit()
        Me.ogSupervisorApproved = New DevExpress.XtraGrid.GridControl()
        Me.ogvSupervisorApproved = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.ColFTStateApproved = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCheckEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.ColFTPRPurchaseBy = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColFTPRPurchaseNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FDPRPurchaseDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColFTPurchaseState = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColFTCmpRunCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColFTCmpRunName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColFDDeliveryDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColFTCrTermCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColFTCrTermDesc = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColFNCreditDay = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColFTTermOfPMCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColFTTermOfPMName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColFTContactPerson = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTRemark = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColFNDisCountPer = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColFNPONetAmt = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColFNVatPer = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColFTTeamGrpCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColFTTeamGrpName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColFTPurGrpCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColFTPurGrpName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTPoTypeState = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTStateFree = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNNetAmt = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNPRState = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTSafetyName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.otmchkpo = New System.Windows.Forms.Timer()
        CType(Me.MainRibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemComboBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.oDockManager, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogApprovedMail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogApprovedMail.SuspendLayout()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbmainprocbutton.SuspendLayout()
        CType(Me.ochkselectall.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogSupervisorApproved, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvSupervisorApproved, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 865)
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
        'ogApprovedMail
        '
        Me.ogApprovedMail.AppearanceCaption.Options.UseTextOptions = True
        Me.ogApprovedMail.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ogApprovedMail.Controls.Add(Me.ogbmainprocbutton)
        Me.ogApprovedMail.Controls.Add(Me.ochkselectall)
        Me.ogApprovedMail.Controls.Add(Me.ogSupervisorApproved)
        Me.ogApprovedMail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogApprovedMail.Location = New System.Drawing.Point(0, 69)
        Me.ogApprovedMail.Margin = New System.Windows.Forms.Padding(4)
        Me.ogApprovedMail.Name = "ogApprovedMail"
        Me.ogApprovedMail.Size = New System.Drawing.Size(1478, 796)
        Me.ogApprovedMail.TabIndex = 9
        Me.ogApprovedMail.Text = "Approved  PRPurchase"
        '
        'ogbmainprocbutton
        '
        Me.ogbmainprocbutton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmsavelayout)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmexit)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmpreview)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmreject)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmapprove)
        Me.ogbmainprocbutton.Location = New System.Drawing.Point(54, 385)
        Me.ogbmainprocbutton.Margin = New System.Windows.Forms.Padding(4)
        Me.ogbmainprocbutton.Name = "ogbmainprocbutton"
        Me.ogbmainprocbutton.Size = New System.Drawing.Size(1371, 112)
        Me.ogbmainprocbutton.TabIndex = 138
        Me.ogbmainprocbutton.Tag = "2|"
        '
        'ocmsavelayout
        '
        Me.ocmsavelayout.Location = New System.Drawing.Point(1204, 22)
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
        'ochkselectall
        '
        Me.ochkselectall.Location = New System.Drawing.Point(28, 1)
        Me.ochkselectall.Margin = New System.Windows.Forms.Padding(4)
        Me.ochkselectall.Name = "ochkselectall"
        Me.ochkselectall.Properties.Caption = "Select All"
        Me.ochkselectall.Size = New System.Drawing.Size(230, 20)
        Me.ochkselectall.TabIndex = 1
        '
        'ogSupervisorApproved
        '
        Me.ogSupervisorApproved.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogSupervisorApproved.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4)
        Me.ogSupervisorApproved.Location = New System.Drawing.Point(0, 32)
        Me.ogSupervisorApproved.MainView = Me.ogvSupervisorApproved
        Me.ogSupervisorApproved.Margin = New System.Windows.Forms.Padding(4)
        Me.ogSupervisorApproved.Name = "ogSupervisorApproved"
        Me.ogSupervisorApproved.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCheckEdit1})
        Me.ogSupervisorApproved.Size = New System.Drawing.Size(1473, 757)
        Me.ogSupervisorApproved.TabIndex = 0
        Me.ogSupervisorApproved.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvSupervisorApproved})
        '
        'ogvSupervisorApproved
        '
        Me.ogvSupervisorApproved.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.ColFTStateApproved, Me.ColFTPRPurchaseBy, Me.ColFTPRPurchaseNo, Me.FDPRPurchaseDate, Me.ColFTPurchaseState, Me.ColFTCmpRunCode, Me.ColFTCmpRunName, Me.ColFDDeliveryDate, Me.ColFTCrTermCode, Me.ColFTCrTermDesc, Me.ColFNCreditDay, Me.ColFTTermOfPMCode, Me.ColFTTermOfPMName, Me.ColFTContactPerson, Me.FTRemark, Me.ColFNDisCountPer, Me.ColFNPONetAmt, Me.ColFNVatPer, Me.ColFTTeamGrpCode, Me.ColFTTeamGrpName, Me.ColFTPurGrpCode, Me.ColFTPurGrpName, Me.FTPoTypeState, Me.FTStateFree, Me.FNQuantity, Me.FNNetAmt, Me.FNPRState, Me.FTSafetyName})
        Me.ogvSupervisorApproved.GridControl = Me.ogSupervisorApproved
        Me.ogvSupervisorApproved.Name = "ogvSupervisorApproved"
        Me.ogvSupervisorApproved.OptionsView.ColumnAutoWidth = False
        Me.ogvSupervisorApproved.OptionsView.ShowGroupPanel = False
        '
        'ColFTStateApproved
        '
        Me.ColFTStateApproved.Caption = "FTStateApproved"
        Me.ColFTStateApproved.ColumnEdit = Me.RepositoryItemCheckEdit1
        Me.ColFTStateApproved.FieldName = "FTStateApproved"
        Me.ColFTStateApproved.Name = "ColFTStateApproved"
        Me.ColFTStateApproved.Visible = True
        Me.ColFTStateApproved.VisibleIndex = 0
        Me.ColFTStateApproved.Width = 137
        '
        'RepositoryItemCheckEdit1
        '
        Me.RepositoryItemCheckEdit1.AutoHeight = False
        Me.RepositoryItemCheckEdit1.Caption = "Check"
        Me.RepositoryItemCheckEdit1.Name = "RepositoryItemCheckEdit1"
        Me.RepositoryItemCheckEdit1.ValueChecked = "1"
        Me.RepositoryItemCheckEdit1.ValueUnchecked = "0"
        '
        'ColFTPRPurchaseBy
        '
        Me.ColFTPRPurchaseBy.Caption = "FTPRPurchaseBy"
        Me.ColFTPRPurchaseBy.FieldName = "FTPRPurchaseBy"
        Me.ColFTPRPurchaseBy.Name = "ColFTPRPurchaseBy"
        Me.ColFTPRPurchaseBy.OptionsColumn.AllowEdit = False
        Me.ColFTPRPurchaseBy.OptionsColumn.ReadOnly = True
        Me.ColFTPRPurchaseBy.Visible = True
        Me.ColFTPRPurchaseBy.VisibleIndex = 2
        Me.ColFTPRPurchaseBy.Width = 134
        '
        'ColFTPRPurchaseNo
        '
        Me.ColFTPRPurchaseNo.Caption = "FTPRPurchaseNo"
        Me.ColFTPRPurchaseNo.FieldName = "FTPRPurchaseNo"
        Me.ColFTPRPurchaseNo.Name = "ColFTPRPurchaseNo"
        Me.ColFTPRPurchaseNo.OptionsColumn.AllowEdit = False
        Me.ColFTPRPurchaseNo.OptionsColumn.ReadOnly = True
        Me.ColFTPRPurchaseNo.Visible = True
        Me.ColFTPRPurchaseNo.VisibleIndex = 3
        Me.ColFTPRPurchaseNo.Width = 178
        '
        'FDPRPurchaseDate
        '
        Me.FDPRPurchaseDate.Caption = "FDPRPurchaseDate"
        Me.FDPRPurchaseDate.FieldName = "FDPRPurchaseDate"
        Me.FDPRPurchaseDate.Name = "FDPRPurchaseDate"
        Me.FDPRPurchaseDate.OptionsColumn.AllowEdit = False
        Me.FDPRPurchaseDate.OptionsColumn.ReadOnly = True
        Me.FDPRPurchaseDate.Visible = True
        Me.FDPRPurchaseDate.VisibleIndex = 5
        Me.FDPRPurchaseDate.Width = 142
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
        'ColFTContactPerson
        '
        Me.ColFTContactPerson.Caption = "FTContactPerson"
        Me.ColFTContactPerson.FieldName = "FTContactPerson"
        Me.ColFTContactPerson.Name = "ColFTContactPerson"
        Me.ColFTContactPerson.OptionsColumn.AllowEdit = False
        Me.ColFTContactPerson.OptionsColumn.ReadOnly = True
        Me.ColFTContactPerson.Width = 100
        '
        'FTRemark
        '
        Me.FTRemark.Caption = "FTRemark"
        Me.FTRemark.FieldName = "FTRemark"
        Me.FTRemark.Name = "FTRemark"
        Me.FTRemark.OptionsColumn.AllowEdit = False
        Me.FTRemark.OptionsColumn.ReadOnly = True
        Me.FTRemark.Visible = True
        Me.FTRemark.VisibleIndex = 8
        Me.FTRemark.Width = 146
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
        'FNQuantity
        '
        Me.FNQuantity.Caption = "FNQuantity"
        Me.FNQuantity.DisplayFormat.FormatString = "{0:n2}"
        Me.FNQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNQuantity.FieldName = "FNQuantity"
        Me.FNQuantity.Name = "FNQuantity"
        Me.FNQuantity.OptionsColumn.AllowEdit = False
        Me.FNQuantity.Visible = True
        Me.FNQuantity.VisibleIndex = 6
        Me.FNQuantity.Width = 101
        '
        'FNNetAmt
        '
        Me.FNNetAmt.Caption = "FNNetAmt"
        Me.FNNetAmt.DisplayFormat.FormatString = "{0:n2}"
        Me.FNNetAmt.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNNetAmt.FieldName = "FNNetAmt"
        Me.FNNetAmt.Name = "FNNetAmt"
        Me.FNNetAmt.OptionsColumn.AllowEdit = False
        Me.FNNetAmt.Visible = True
        Me.FNNetAmt.VisibleIndex = 7
        Me.FNNetAmt.Width = 93
        '
        'FNPRState
        '
        Me.FNPRState.Caption = "FNPRState"
        Me.FNPRState.FieldName = "FNPRState"
        Me.FNPRState.Name = "FNPRState"
        Me.FNPRState.OptionsColumn.AllowEdit = False
        Me.FNPRState.Visible = True
        Me.FNPRState.VisibleIndex = 4
        Me.FNPRState.Width = 101
        '
        'FTSafetyName
        '
        Me.FTSafetyName.Caption = "FTSafetyName"
        Me.FTSafetyName.FieldName = "FTSafetyName"
        Me.FTSafetyName.Name = "FTSafetyName"
        Me.FTSafetyName.Visible = True
        Me.FTSafetyName.VisibleIndex = 1
        Me.FTSafetyName.Width = 126
        '
        'otmchkpo
        '
        Me.otmchkpo.Interval = 60000
        '
        'wSafetyApprovedAssetPR
        '
        Me.Appearance.Options.UseFont = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(11.0!, 21.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1478, 900)
        Me.Controls.Add(Me.ogApprovedMail)
        Me.Controls.Add(Me.StandaloneBarDockControl)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.MainRibbonControl)
        Me.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.IsMdiContainer = True
        Me.Margin = New System.Windows.Forms.Padding(6, 5, 6, 5)
        Me.Name = "wSafetyApprovedAssetPR"
        Me.Ribbon = Me.MainRibbonControl
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.StatusBar = Me.RibbonStatusBar
        Me.Tag = "|WISDOM SYSTEM|WISDOM SYSTEM"
        Me.Text = " "
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.MainRibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemComboBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.oDockManager, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogApprovedMail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogApprovedMail.ResumeLayout(False)
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbmainprocbutton.ResumeLayout(False)
        CType(Me.ochkselectall.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogSupervisorApproved, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvSupervisorApproved, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents ogSupervisorApproved As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvSupervisorApproved As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents ColFTStateApproved As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemCheckEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents ColFTPRPurchaseBy As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFTPRPurchaseNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FDPRPurchaseDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFTPurchaseState As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFTCmpRunCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFTCmpRunName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFDDeliveryDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFTCrTermCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFTCrTermDesc As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFNCreditDay As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFTTermOfPMCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFTTermOfPMName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFTContactPerson As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTRemark As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFNDisCountPer As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFNPONetAmt As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFNVatPer As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFTTeamGrpCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFTTeamGrpName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFTPurGrpCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFTPurGrpName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ogbmainprocbutton As DevExpress.XtraEditors.PanelControl
    Friend WithEvents ocmpreview As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmreject As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmapprove As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents otmchkpo As System.Windows.Forms.Timer
    Friend WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmsavelayout As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FTPoTypeState As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTStateFree As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNNetAmt As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNPRState As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTSafetyName As DevExpress.XtraGrid.Columns.GridColumn


End Class

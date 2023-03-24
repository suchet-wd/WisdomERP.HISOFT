<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wPRFactoryApproved
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
        Me.ocmreject = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmapprove = New DevExpress.XtraEditors.SimpleButton()
        Me.otmchkpo = New System.Windows.Forms.Timer(Me.components)
        Me.otbmain = New DevExpress.XtraTab.XtraTabControl()
        Me.otpappcpr = New DevExpress.XtraTab.XtraTabPage()
        Me.ogbdetail = New DevExpress.XtraEditors.GroupControl()
        Me.ogcdetail = New DevExpress.XtraGrid.GridControl()
        Me.ogvdetail = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.cFTSelect = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryFTSelect = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.CFTCmpCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFTCmpName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFTPRPurchaseNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFDPRPurchaseDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFTPRPurchaseBy = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFDPRRequestDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFTRemark = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryFTSandApprove = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.FTSelectAllCMInv = New DevExpress.XtraEditors.CheckEdit()
        CType(Me.MainRibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemComboBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.oDockManager, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbmainprocbutton.SuspendLayout()
        CType(Me.otbmain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.otbmain.SuspendLayout()
        Me.otpappcpr.SuspendLayout()
        CType(Me.ogbdetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbdetail.SuspendLayout()
        CType(Me.ogcdetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvdetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryFTSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryFTSandApprove, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTSelectAllCMInv.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'MainRibbonControl
        '
        Me.MainRibbonControl.ApplicationButtonText = Nothing
        Me.MainRibbonControl.ExpandCollapseItem.Id = 0
        Me.MainRibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.MainRibbonControl.ExpandCollapseItem, Me.mnusysabout, Me.FTUserLogINIP, Me.MainRibbonControl.SearchEditItem})
        Me.MainRibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.MainRibbonControl.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.MainRibbonControl.MaxItemId = 5
        Me.MainRibbonControl.Name = "MainRibbonControl"
        Me.MainRibbonControl.OptionsMenuMinWidth = 240
        Me.MainRibbonControl.OptionsPageCategories.Alignment = DevExpress.XtraBars.Ribbon.RibbonPageCategoryAlignment.Left
        Me.MainRibbonControl.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemComboBox1})
        Me.MainRibbonControl.ShowExpandCollapseButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.MainRibbonControl.ShowToolbarCustomizeItem = False
        Me.MainRibbonControl.Size = New System.Drawing.Size(1075, 54)
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
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 539)
        Me.RibbonStatusBar.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.RibbonStatusBar.Name = "RibbonStatusBar"
        Me.RibbonStatusBar.Ribbon = Me.MainRibbonControl
        Me.RibbonStatusBar.Size = New System.Drawing.Size(1075, 29)
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
        Me.StandaloneBarDockControl.Name = "StandaloneBarDockControl"
        Me.StandaloneBarDockControl.Size = New System.Drawing.Size(1075, 0)
        Me.StandaloneBarDockControl.Text = "StandaloneBarDockControl1"
        '
        'ogbmainprocbutton
        '
        Me.ogbmainprocbutton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmsavelayout)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmexit)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmreject)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmapprove)
        Me.ogbmainprocbutton.Location = New System.Drawing.Point(203, 186)
        Me.ogbmainprocbutton.Name = "ogbmainprocbutton"
        Me.ogbmainprocbutton.Size = New System.Drawing.Size(583, 70)
        Me.ogbmainprocbutton.TabIndex = 138
        Me.ogbmainprocbutton.Tag = "2|"
        '
        'ocmsavelayout
        '
        Me.ocmsavelayout.Location = New System.Drawing.Point(66, 41)
        Me.ocmsavelayout.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.ocmsavelayout.Name = "ocmsavelayout"
        Me.ocmsavelayout.Size = New System.Drawing.Size(99, 21)
        Me.ocmsavelayout.TabIndex = 332
        Me.ocmsavelayout.Text = "savelayoutgrid"
        '
        'ocmexit
        '
        Me.ocmexit.Location = New System.Drawing.Point(358, 17)
        Me.ocmexit.Name = "ocmexit"
        Me.ocmexit.Size = New System.Drawing.Size(96, 23)
        Me.ocmexit.TabIndex = 13
        Me.ocmexit.Text = "Close"
        Me.ocmexit.Visible = False
        '
        'ocmreject
        '
        Me.ocmreject.Location = New System.Drawing.Point(131, 17)
        Me.ocmreject.Name = "ocmreject"
        Me.ocmreject.Size = New System.Drawing.Size(83, 23)
        Me.ocmreject.TabIndex = 11
        Me.ocmreject.Text = "Reject"
        '
        'ocmapprove
        '
        Me.ocmapprove.Location = New System.Drawing.Point(11, 17)
        Me.ocmapprove.Name = "ocmapprove"
        Me.ocmapprove.Size = New System.Drawing.Size(83, 23)
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
        Me.otbmain.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.otbmain.Name = "otbmain"
        Me.otbmain.SelectedTabPage = Me.otpappcpr
        Me.otbmain.Size = New System.Drawing.Size(1075, 485)
        Me.otbmain.TabIndex = 14
        Me.otbmain.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.otpappcpr})
        '
        'otpappcpr
        '
        Me.otpappcpr.Controls.Add(Me.ogbdetail)
        Me.otpappcpr.Name = "otpappcpr"
        Me.otpappcpr.Size = New System.Drawing.Size(1067, 455)
        Me.otpappcpr.Text = "Approved  PR"
        '
        'ogbdetail
        '
        Me.ogbdetail.AppearanceCaption.Options.UseTextOptions = True
        Me.ogbdetail.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ogbdetail.Controls.Add(Me.ogbmainprocbutton)
        Me.ogbdetail.Controls.Add(Me.ogcdetail)
        Me.ogbdetail.Controls.Add(Me.FTSelectAllCMInv)
        Me.ogbdetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogbdetail.Location = New System.Drawing.Point(0, 0)
        Me.ogbdetail.Name = "ogbdetail"
        Me.ogbdetail.Size = New System.Drawing.Size(1067, 455)
        Me.ogbdetail.TabIndex = 12
        Me.ogbdetail.Text = "Approved  PR"
        '
        'ogcdetail
        '
        Me.ogcdetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogcdetail.Location = New System.Drawing.Point(2, 22)
        Me.ogcdetail.MainView = Me.ogvdetail
        Me.ogcdetail.MenuManager = Me.MainRibbonControl
        Me.ogcdetail.Name = "ogcdetail"
        Me.ogcdetail.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryFTSelect, Me.RepositoryFTSandApprove})
        Me.ogcdetail.Size = New System.Drawing.Size(1063, 431)
        Me.ogcdetail.TabIndex = 2
        Me.ogcdetail.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvdetail})
        '
        'ogvdetail
        '
        Me.ogvdetail.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.cFTSelect, Me.CFTCmpCode, Me.CFTCmpName, Me.CFTPRPurchaseNo, Me.CFDPRPurchaseDate, Me.CFTPRPurchaseBy, Me.CFDPRRequestDate, Me.CFTRemark})
        Me.ogvdetail.DetailHeight = 267
        Me.ogvdetail.GridControl = Me.ogcdetail
        Me.ogvdetail.Name = "ogvdetail"
        Me.ogvdetail.OptionsEditForm.ShowUpdateCancelPanel = DevExpress.Utils.DefaultBoolean.[True]
        Me.ogvdetail.OptionsView.ColumnAutoWidth = False
        Me.ogvdetail.OptionsView.ShowGroupPanel = False
        '
        'cFTSelect
        '
        Me.cFTSelect.Caption = "FTSelect"
        Me.cFTSelect.ColumnEdit = Me.RepositoryFTSelect
        Me.cFTSelect.FieldName = "FTSelect"
        Me.cFTSelect.MinWidth = 15
        Me.cFTSelect.Name = "cFTSelect"
        Me.cFTSelect.OptionsColumn.ShowCaption = False
        Me.cFTSelect.Visible = True
        Me.cFTSelect.VisibleIndex = 0
        Me.cFTSelect.Width = 26
        '
        'RepositoryFTSelect
        '
        Me.RepositoryFTSelect.AutoHeight = False
        Me.RepositoryFTSelect.Caption = "Check"
        Me.RepositoryFTSelect.Name = "RepositoryFTSelect"
        Me.RepositoryFTSelect.ValueChecked = "1"
        Me.RepositoryFTSelect.ValueUnchecked = "0"
        '
        'CFTCmpCode
        '
        Me.CFTCmpCode.Caption = "Cmp Code"
        Me.CFTCmpCode.FieldName = "FTCmpCode"
        Me.CFTCmpCode.MinWidth = 15
        Me.CFTCmpCode.Name = "CFTCmpCode"
        Me.CFTCmpCode.OptionsColumn.AllowEdit = False
        Me.CFTCmpCode.OptionsColumn.ReadOnly = True
        Me.CFTCmpCode.Visible = True
        Me.CFTCmpCode.VisibleIndex = 1
        Me.CFTCmpCode.Width = 73
        '
        'CFTCmpName
        '
        Me.CFTCmpName.Caption = "Cmp Name"
        Me.CFTCmpName.FieldName = "FTCmpName"
        Me.CFTCmpName.MinWidth = 15
        Me.CFTCmpName.Name = "CFTCmpName"
        Me.CFTCmpName.OptionsColumn.AllowEdit = False
        Me.CFTCmpName.OptionsColumn.ReadOnly = True
        Me.CFTCmpName.Visible = True
        Me.CFTCmpName.VisibleIndex = 2
        Me.CFTCmpName.Width = 200
        '
        'CFTPRPurchaseNo
        '
        Me.CFTPRPurchaseNo.Caption = "FTPRPurchaseNo"
        Me.CFTPRPurchaseNo.FieldName = "FTPRPurchaseNo"
        Me.CFTPRPurchaseNo.MinWidth = 15
        Me.CFTPRPurchaseNo.Name = "CFTPRPurchaseNo"
        Me.CFTPRPurchaseNo.OptionsColumn.AllowEdit = False
        Me.CFTPRPurchaseNo.OptionsColumn.ReadOnly = True
        Me.CFTPRPurchaseNo.Visible = True
        Me.CFTPRPurchaseNo.VisibleIndex = 3
        Me.CFTPRPurchaseNo.Width = 87
        '
        'CFDPRPurchaseDate
        '
        Me.CFDPRPurchaseDate.Caption = "PR Date"
        Me.CFDPRPurchaseDate.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.CFDPRPurchaseDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.CFDPRPurchaseDate.FieldName = "FDPRPurchaseDate"
        Me.CFDPRPurchaseDate.MinWidth = 15
        Me.CFDPRPurchaseDate.Name = "CFDPRPurchaseDate"
        Me.CFDPRPurchaseDate.OptionsColumn.AllowEdit = False
        Me.CFDPRPurchaseDate.OptionsColumn.ReadOnly = True
        Me.CFDPRPurchaseDate.Visible = True
        Me.CFDPRPurchaseDate.VisibleIndex = 4
        Me.CFDPRPurchaseDate.Width = 73
        '
        'CFTPRPurchaseBy
        '
        Me.CFTPRPurchaseBy.Caption = "PR By"
        Me.CFTPRPurchaseBy.FieldName = "FTPRPurchaseBy"
        Me.CFTPRPurchaseBy.MinWidth = 15
        Me.CFTPRPurchaseBy.Name = "CFTPRPurchaseBy"
        Me.CFTPRPurchaseBy.OptionsColumn.AllowEdit = False
        Me.CFTPRPurchaseBy.OptionsColumn.ReadOnly = True
        Me.CFTPRPurchaseBy.Visible = True
        Me.CFTPRPurchaseBy.VisibleIndex = 5
        Me.CFTPRPurchaseBy.Width = 106
        '
        'CFDPRRequestDate
        '
        Me.CFDPRRequestDate.Caption = "PR Request Date"
        Me.CFDPRRequestDate.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.CFDPRRequestDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.CFDPRRequestDate.FieldName = "FDPRRequestDate"
        Me.CFDPRRequestDate.MinWidth = 15
        Me.CFDPRRequestDate.Name = "CFDPRRequestDate"
        Me.CFDPRRequestDate.OptionsColumn.AllowEdit = False
        Me.CFDPRRequestDate.OptionsColumn.ReadOnly = True
        Me.CFDPRRequestDate.Visible = True
        Me.CFDPRRequestDate.VisibleIndex = 6
        Me.CFDPRRequestDate.Width = 73
        '
        'CFTRemark
        '
        Me.CFTRemark.Caption = "Remark"
        Me.CFTRemark.FieldName = "FTRemark"
        Me.CFTRemark.MinWidth = 15
        Me.CFTRemark.Name = "CFTRemark"
        Me.CFTRemark.OptionsColumn.AllowEdit = False
        Me.CFTRemark.OptionsColumn.ReadOnly = True
        Me.CFTRemark.Visible = True
        Me.CFTRemark.VisibleIndex = 7
        Me.CFTRemark.Width = 329
        '
        'RepositoryFTSandApprove
        '
        Me.RepositoryFTSandApprove.AutoHeight = False
        Me.RepositoryFTSandApprove.Caption = "Check"
        Me.RepositoryFTSandApprove.Name = "RepositoryFTSandApprove"
        Me.RepositoryFTSandApprove.ValueChecked = "1"
        Me.RepositoryFTSandApprove.ValueUnchecked = "0"
        '
        'FTSelectAllCMInv
        '
        Me.FTSelectAllCMInv.Location = New System.Drawing.Point(20, 1)
        Me.FTSelectAllCMInv.Name = "FTSelectAllCMInv"
        Me.FTSelectAllCMInv.Properties.Caption = "Select All"
        Me.FTSelectAllCMInv.Size = New System.Drawing.Size(167, 20)
        Me.FTSelectAllCMInv.TabIndex = 1
        Me.FTSelectAllCMInv.Tag = "2|"
        '
        'wPRFactoryApproved
        '
        Me.Appearance.Options.UseFont = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1075, 568)
        Me.Controls.Add(Me.otbmain)
        Me.Controls.Add(Me.StandaloneBarDockControl)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.MainRibbonControl)
        Me.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.IsMdiContainer = True
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Name = "wPRFactoryApproved"
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
        Me.otpappcpr.ResumeLayout(False)
        CType(Me.ogbdetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbdetail.ResumeLayout(False)
        CType(Me.ogcdetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvdetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryFTSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryFTSandApprove, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents ocmreject As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmapprove As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents otmchkpo As System.Windows.Forms.Timer
    Friend WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmsavelayout As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents otbmain As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents otpappcpr As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents ogbdetail As DevExpress.XtraEditors.GroupControl
    Friend WithEvents FTSelectAllCMInv As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents ogcdetail As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvdetail As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents cFTSelect As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryFTSelect As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents RepositoryFTSandApprove As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents CFTCmpCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFTCmpName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFTPRPurchaseNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFDPRPurchaseDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFTPRPurchaseBy As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFDPRRequestDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFTRemark As DevExpress.XtraGrid.Columns.GridColumn


End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wDCFactoryApproved
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
        Me.otpappcminv = New DevExpress.XtraTab.XtraTabPage()
        Me.GroupControl2 = New DevExpress.XtraEditors.GroupControl()
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
        Me.FTSelectAllCMInv = New DevExpress.XtraEditors.CheckEdit()
        Me.cFTApproveType = New DevExpress.XtraGrid.Columns.GridColumn()
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
        CType(Me.ogcdocument, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvdocument, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryFTSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryFTSandApprove, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.ogbmainprocbutton.Location = New System.Drawing.Point(280, 244)
        Me.ogbmainprocbutton.Margin = New System.Windows.Forms.Padding(4)
        Me.ogbmainprocbutton.Name = "ogbmainprocbutton"
        Me.ogbmainprocbutton.Size = New System.Drawing.Size(802, 92)
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
        Me.otbmain.Location = New System.Drawing.Point(0, 69)
        Me.otbmain.Name = "otbmain"
        Me.otbmain.SelectedTabPage = Me.otpappcminv
        Me.otbmain.Size = New System.Drawing.Size(1478, 642)
        Me.otbmain.TabIndex = 14
        Me.otbmain.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.otpappcminv})
        '
        'otpappcminv
        '
        Me.otpappcminv.Controls.Add(Me.GroupControl2)
        Me.otpappcminv.Margin = New System.Windows.Forms.Padding(4)
        Me.otpappcminv.Name = "otpappcminv"
        Me.otpappcminv.Size = New System.Drawing.Size(1468, 605)
        Me.otpappcminv.Text = "Approve Documentation"
        '
        'GroupControl2
        '
        Me.GroupControl2.AppearanceCaption.Options.UseTextOptions = True
        Me.GroupControl2.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GroupControl2.Controls.Add(Me.ogbmainprocbutton)
        Me.GroupControl2.Controls.Add(Me.ogcdocument)
        Me.GroupControl2.Controls.Add(Me.FTSelectAllCMInv)
        Me.GroupControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl2.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl2.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupControl2.Name = "GroupControl2"
        Me.GroupControl2.Size = New System.Drawing.Size(1468, 605)
        Me.GroupControl2.TabIndex = 12
        Me.GroupControl2.Text = "Approved  Documentation"
        '
        'ogcdocument
        '
        Me.ogcdocument.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogcdocument.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4)
        Me.ogcdocument.Location = New System.Drawing.Point(2, 27)
        Me.ogcdocument.MainView = Me.ogvdocument
        Me.ogcdocument.Margin = New System.Windows.Forms.Padding(4)
        Me.ogcdocument.MenuManager = Me.MainRibbonControl
        Me.ogcdocument.Name = "ogcdocument"
        Me.ogcdocument.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryFTSelect, Me.RepositoryFTSandApprove})
        Me.ogcdocument.Size = New System.Drawing.Size(1464, 576)
        Me.ogcdocument.TabIndex = 2
        Me.ogcdocument.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvdocument})
        '
        'ogvdocument
        '
        Me.ogvdocument.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.cFTSelect, Me.cFTDocumentNo, Me.cFDDocumentDate, Me.cFTDocumentBy, Me.cFTCmpCode, Me.cFTCmpName, Me.cFTDocumentTitle, Me.cFTNote, Me.cFTBenefit, Me.cFNOperActivity, Me.cFTOperActivityName, Me.cFTDocName, Me.cFTDocRefCode, Me.cFTDocTypeName, Me.cFTFileTypeName, Me.cFBDocument, Me.cFNDocType, Me.cFNHSysCmpId, Me.cFTApproveType})
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
        'cFTApproveType
        '
        Me.cFTApproveType.Caption = "FTApproveType"
        Me.cFTApproveType.FieldName = "FTApproveType"
        Me.cFTApproveType.Name = "cFTApproveType"
        '
        'wDCFactoryApproved
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
        Me.Name = "wDCFactoryApproved"
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
        CType(Me.ogcdocument, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvdocument, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents otpappcminv As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents GroupControl2 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents FTSelectAllCMInv As DevExpress.XtraEditors.CheckEdit
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
    Friend WithEvents RepositoryFTSandApprove As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents cFBDocument As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNDocType As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNHSysCmpId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTApproveType As DevExpress.XtraGrid.Columns.GridColumn

End Class

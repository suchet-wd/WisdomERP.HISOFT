Imports DevExpress.XtraEditors.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wReportQADetail_tracking
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(wReportQADetail_tracking))
        Me.ocmdoc = New DevExpress.XtraBars.Docking.DockManager(Me.components)
        Me.ogbheader = New DevExpress.XtraBars.Docking.DockPanel()
        Me.DockPanel1_Container = New DevExpress.XtraBars.Docking.ControlContainer()
        Me.oChkSelectAll = New DevExpress.XtraEditors.CheckEdit()
        Me.ogbcompany = New DevExpress.XtraEditors.GroupControl()
        Me.ogccmp = New DevExpress.XtraGrid.GridControl()
        Me.ogvcmp = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GCmpSelectCmp = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCheckEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTCmpCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTCmpName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTIPServer = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ReposFNCompensationFoundByYearOption = New DevExpress.XtraEditors.Repository.RepositoryItemComboBox()
        Me.FTEndDate = New DevExpress.XtraEditors.DateEdit()
        Me.FTEndDate_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTStartDate = New DevExpress.XtraEditors.DateEdit()
        Me.FTStartDate_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.ogbmainprocbutton = New DevExpress.XtraEditors.PanelControl()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmload = New DevExpress.XtraEditors.SimpleButton()
        Me.otb = New DevExpress.XtraTab.XtraTabControl()
        Me.otpdata = New DevExpress.XtraTab.XtraTabPage()
        Me.ogcDetail = New DevExpress.XtraGrid.GridControl()
        Me.ogvDetail = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.FTVenderPramCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTQADate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTUserQA = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTFactory = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTPORef = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNQAInQty = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNQAActualQty = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTStyleCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTColorway = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTSeasonCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTSubOrderNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTStatePreFinal = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTJobState = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemStateCheck = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.RepositoryItemRandomQty = New DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit()
        Me.ReposFNStateQC = New DevExpress.XtraEditors.Repository.RepositoryItemComboBox()
        Me.ReposDefectQty = New DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit()
        Me.RepositoryFNQCActual = New DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit()
        Me.RepositoryQCDefectQtyNew = New DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit()
        Me.cFTSeasonCodeYear = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.ocmdoc, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbheader.SuspendLayout()
        Me.DockPanel1_Container.SuspendLayout()
        CType(Me.oChkSelectAll.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogbcompany, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbcompany.SuspendLayout()
        CType(Me.ogccmp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvcmp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ReposFNCompensationFoundByYearOption, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTEndDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTEndDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTStartDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTStartDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbmainprocbutton.SuspendLayout()
        CType(Me.otb, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.otb.SuspendLayout()
        Me.otpdata.SuspendLayout()
        CType(Me.ogcDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemStateCheck, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemRandomQty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ReposFNStateQC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ReposDefectQty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryFNQCActual, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryQCDefectQtyNew, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.ogbheader.ImageOptions.Image = CType(resources.GetObject("ogbheader.ImageOptions.Image"), System.Drawing.Image)
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
        Me.ogbheader.OriginalSize = New System.Drawing.Size(200, 198)
        Me.ogbheader.Size = New System.Drawing.Size(1354, 198)
        Me.ogbheader.Text = "Criteria"
        '
        'DockPanel1_Container
        '
        Me.DockPanel1_Container.Controls.Add(Me.oChkSelectAll)
        Me.DockPanel1_Container.Controls.Add(Me.ogbcompany)
        Me.DockPanel1_Container.Controls.Add(Me.FTEndDate)
        Me.DockPanel1_Container.Controls.Add(Me.FTEndDate_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FTStartDate)
        Me.DockPanel1_Container.Controls.Add(Me.FTStartDate_lbl)
        Me.DockPanel1_Container.Location = New System.Drawing.Point(4, 32)
        Me.DockPanel1_Container.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.DockPanel1_Container.Name = "DockPanel1_Container"
        Me.DockPanel1_Container.Size = New System.Drawing.Size(1346, 160)
        Me.DockPanel1_Container.TabIndex = 0
        '
        'oChkSelectAll
        '
        Me.oChkSelectAll.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.oChkSelectAll.Location = New System.Drawing.Point(713, 3)
        Me.oChkSelectAll.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.oChkSelectAll.Name = "oChkSelectAll"
        Me.oChkSelectAll.Properties.AutoHeight = False
        Me.oChkSelectAll.Properties.Caption = "Select All"
        Me.oChkSelectAll.Size = New System.Drawing.Size(144, 20)
        Me.oChkSelectAll.TabIndex = 396
        '
        'ogbcompany
        '
        Me.ogbcompany.Controls.Add(Me.ogccmp)
        Me.ogbcompany.Dock = System.Windows.Forms.DockStyle.Right
        Me.ogbcompany.Location = New System.Drawing.Point(858, 0)
        Me.ogbcompany.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.ogbcompany.Name = "ogbcompany"
        Me.ogbcompany.ShowCaption = False
        Me.ogbcompany.Size = New System.Drawing.Size(488, 160)
        Me.ogbcompany.TabIndex = 507
        Me.ogbcompany.Text = "Select Company"
        '
        'ogccmp
        '
        Me.ogccmp.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogccmp.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogccmp.Location = New System.Drawing.Point(2, 2)
        Me.ogccmp.MainView = Me.ogvcmp
        Me.ogccmp.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogccmp.Name = "ogccmp"
        Me.ogccmp.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCheckEdit1, Me.ReposFNCompensationFoundByYearOption})
        Me.ogccmp.Size = New System.Drawing.Size(484, 156)
        Me.ogccmp.TabIndex = 5
        Me.ogccmp.TabStop = False
        Me.ogccmp.Tag = "3|"
        Me.ogccmp.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvcmp})
        '
        'ogvcmp
        '
        Me.ogvcmp.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GCmpSelectCmp, Me.GridColumn2, Me.FTCmpCode, Me.FTCmpName, Me.FTIPServer})
        Me.ogvcmp.GridControl = Me.ogccmp
        Me.ogvcmp.Name = "ogvcmp"
        Me.ogvcmp.OptionsCustomization.AllowGroup = False
        Me.ogvcmp.OptionsCustomization.AllowQuickHideColumns = False
        Me.ogvcmp.OptionsView.ColumnAutoWidth = False
        Me.ogvcmp.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.ogvcmp.OptionsView.ShowGroupPanel = False
        Me.ogvcmp.Tag = "3|"
        '
        'GCmpSelectCmp
        '
        Me.GCmpSelectCmp.AppearanceHeader.Options.UseTextOptions = True
        Me.GCmpSelectCmp.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GCmpSelectCmp.Caption = " "
        Me.GCmpSelectCmp.ColumnEdit = Me.RepositoryItemCheckEdit1
        Me.GCmpSelectCmp.FieldName = "FTSelect"
        Me.GCmpSelectCmp.Name = "GCmpSelectCmp"
        Me.GCmpSelectCmp.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.GCmpSelectCmp.OptionsColumn.AllowMove = False
        Me.GCmpSelectCmp.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.GCmpSelectCmp.OptionsColumn.ShowCaption = False
        Me.GCmpSelectCmp.Visible = True
        Me.GCmpSelectCmp.VisibleIndex = 0
        Me.GCmpSelectCmp.Width = 53
        '
        'RepositoryItemCheckEdit1
        '
        Me.RepositoryItemCheckEdit1.AutoHeight = False
        Me.RepositoryItemCheckEdit1.Caption = "Check"
        Me.RepositoryItemCheckEdit1.Name = "RepositoryItemCheckEdit1"
        Me.RepositoryItemCheckEdit1.ValueChecked = "1"
        Me.RepositoryItemCheckEdit1.ValueUnchecked = "0"
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "FNHSysCmpId"
        Me.GridColumn2.FieldName = "FNHSysCmpId"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.OptionsColumn.AllowEdit = False
        Me.GridColumn2.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn2.OptionsColumn.AllowMove = False
        Me.GridColumn2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        '
        'FTCmpCode
        '
        Me.FTCmpCode.AppearanceHeader.Options.UseTextOptions = True
        Me.FTCmpCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTCmpCode.Caption = "FTCmpCode"
        Me.FTCmpCode.FieldName = "FTCmpCode"
        Me.FTCmpCode.Name = "FTCmpCode"
        Me.FTCmpCode.OptionsColumn.AllowEdit = False
        Me.FTCmpCode.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTCmpCode.OptionsColumn.AllowMove = False
        Me.FTCmpCode.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTCmpCode.Visible = True
        Me.FTCmpCode.VisibleIndex = 1
        Me.FTCmpCode.Width = 151
        '
        'FTCmpName
        '
        Me.FTCmpName.AppearanceHeader.Options.UseTextOptions = True
        Me.FTCmpName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTCmpName.Caption = "FTCmpName"
        Me.FTCmpName.FieldName = "FTCmpName"
        Me.FTCmpName.Name = "FTCmpName"
        Me.FTCmpName.OptionsColumn.AllowEdit = False
        Me.FTCmpName.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTCmpName.OptionsColumn.AllowMove = False
        Me.FTCmpName.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTCmpName.Visible = True
        Me.FTCmpName.VisibleIndex = 2
        Me.FTCmpName.Width = 388
        '
        'FTIPServer
        '
        Me.FTIPServer.Caption = "FTIPServer"
        Me.FTIPServer.FieldName = "FTIPServer"
        Me.FTIPServer.Name = "FTIPServer"
        '
        'ReposFNCompensationFoundByYearOption
        '
        Me.ReposFNCompensationFoundByYearOption.AutoHeight = False
        Me.ReposFNCompensationFoundByYearOption.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ReposFNCompensationFoundByYearOption.Name = "ReposFNCompensationFoundByYearOption"
        Me.ReposFNCompensationFoundByYearOption.Tag = "FNCompensationFoundByYearOption"
        Me.ReposFNCompensationFoundByYearOption.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        '
        'FTEndDate
        '
        Me.FTEndDate.EditValue = Nothing
        Me.FTEndDate.EnterMoveNextControl = True
        Me.FTEndDate.Location = New System.Drawing.Point(576, 6)
        Me.FTEndDate.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTEndDate.Name = "FTEndDate"
        Me.FTEndDate.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTEndDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FTEndDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.FTEndDate.Properties.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.FTEndDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTEndDate.Properties.EditFormat.FormatString = "dd/MM/yyyy"
        Me.FTEndDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTEndDate.Properties.Mask.EditMask = "dd/MM/yyyy"
        Me.FTEndDate.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.FTEndDate.Properties.NullDate = ""
        Me.FTEndDate.Size = New System.Drawing.Size(131, 22)
        Me.FTEndDate.TabIndex = 478
        Me.FTEndDate.Tag = "2|"
        '
        'FTEndDate_lbl
        '
        Me.FTEndDate_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTEndDate_lbl.Appearance.Options.UseForeColor = True
        Me.FTEndDate_lbl.Appearance.Options.UseTextOptions = True
        Me.FTEndDate_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTEndDate_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTEndDate_lbl.Location = New System.Drawing.Point(451, 5)
        Me.FTEndDate_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTEndDate_lbl.Name = "FTEndDate_lbl"
        Me.FTEndDate_lbl.Size = New System.Drawing.Size(122, 23)
        Me.FTEndDate_lbl.TabIndex = 480
        Me.FTEndDate_lbl.Tag = "2|"
        Me.FTEndDate_lbl.Text = "ถึงวันที่ :"
        '
        'FTStartDate
        '
        Me.FTStartDate.EditValue = Nothing
        Me.FTStartDate.EnterMoveNextControl = True
        Me.FTStartDate.Location = New System.Drawing.Point(154, 6)
        Me.FTStartDate.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTStartDate.Name = "FTStartDate"
        Me.FTStartDate.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTStartDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FTStartDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.FTStartDate.Properties.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.FTStartDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTStartDate.Properties.EditFormat.FormatString = "dd/MM/yyyy"
        Me.FTStartDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTStartDate.Properties.Mask.EditMask = "dd/MM/yyyy"
        Me.FTStartDate.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.FTStartDate.Properties.NullDate = ""
        Me.FTStartDate.Size = New System.Drawing.Size(131, 22)
        Me.FTStartDate.TabIndex = 477
        Me.FTStartDate.Tag = "2|"
        '
        'FTStartDate_lbl
        '
        Me.FTStartDate_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTStartDate_lbl.Appearance.Options.UseForeColor = True
        Me.FTStartDate_lbl.Appearance.Options.UseTextOptions = True
        Me.FTStartDate_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTStartDate_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTStartDate_lbl.Location = New System.Drawing.Point(28, 5)
        Me.FTStartDate_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTStartDate_lbl.Name = "FTStartDate_lbl"
        Me.FTStartDate_lbl.Size = New System.Drawing.Size(122, 23)
        Me.FTStartDate_lbl.TabIndex = 479
        Me.FTStartDate_lbl.Tag = "2|"
        Me.FTStartDate_lbl.Text = "วันที่เริ่มต้น :"
        '
        'ogbmainprocbutton
        '
        Me.ogbmainprocbutton.Controls.Add(Me.ocmexit)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmload)
        Me.ogbmainprocbutton.Location = New System.Drawing.Point(118, 4)
        Me.ogbmainprocbutton.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbmainprocbutton.Name = "ogbmainprocbutton"
        Me.ogbmainprocbutton.Size = New System.Drawing.Size(399, 42)
        Me.ogbmainprocbutton.TabIndex = 391
        '
        'ocmexit
        '
        Me.ocmexit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmexit.Location = New System.Drawing.Point(214, 9)
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
        'otb
        '
        Me.otb.Dock = System.Windows.Forms.DockStyle.Fill
        Me.otb.Location = New System.Drawing.Point(0, 198)
        Me.otb.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.otb.Name = "otb"
        Me.otb.SelectedTabPage = Me.otpdata
        Me.otb.Size = New System.Drawing.Size(1354, 505)
        Me.otb.TabIndex = 394
        Me.otb.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.otpdata})
        '
        'otpdata
        '
        Me.otpdata.Controls.Add(Me.ogcDetail)
        Me.otpdata.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.otpdata.Name = "otpdata"
        Me.otpdata.Size = New System.Drawing.Size(1352, 475)
        Me.otpdata.Text = "Data"
        '
        'ogcDetail
        '
        Me.ogcDetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogcDetail.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcDetail.Location = New System.Drawing.Point(0, 0)
        Me.ogcDetail.MainView = Me.ogvDetail
        Me.ogcDetail.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcDetail.Name = "ogcDetail"
        Me.ogcDetail.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemStateCheck, Me.RepositoryItemRandomQty, Me.ReposFNStateQC, Me.ReposDefectQty, Me.RepositoryFNQCActual, Me.RepositoryQCDefectQtyNew})
        Me.ogcDetail.Size = New System.Drawing.Size(1352, 475)
        Me.ogcDetail.TabIndex = 2
        Me.ogcDetail.Tag = "3|"
        Me.ogcDetail.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvDetail})
        '
        'ogvDetail
        '
        Me.ogvDetail.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.FTVenderPramCode, Me.FTQADate, Me.FTUserQA, Me.FTFactory, Me.FTPORef, Me.FNQAInQty, Me.FNQAActualQty, Me.FTStyleCode, Me.FTColorway, Me.FTSeasonCode, Me.FTSubOrderNo, Me.FTStatePreFinal, Me.FTJobState, Me.cFTSeasonCodeYear})
        Me.ogvDetail.GridControl = Me.ogcDetail
        Me.ogvDetail.Name = "ogvDetail"
        Me.ogvDetail.OptionsView.ColumnAutoWidth = False
        Me.ogvDetail.OptionsView.ShowGroupPanel = False
        '
        'FTVenderPramCode
        '
        Me.FTVenderPramCode.Caption = "Internal Audit"
        Me.FTVenderPramCode.FieldName = "FTVenderPramCode"
        Me.FTVenderPramCode.Name = "FTVenderPramCode"
        Me.FTVenderPramCode.OptionsColumn.AllowEdit = False
        Me.FTVenderPramCode.OptionsColumn.ReadOnly = True
        Me.FTVenderPramCode.Visible = True
        Me.FTVenderPramCode.VisibleIndex = 0
        Me.FTVenderPramCode.Width = 73
        '
        'FTQADate
        '
        Me.FTQADate.Caption = "Audit Date"
        Me.FTQADate.FieldName = "FTQADate"
        Me.FTQADate.Name = "FTQADate"
        Me.FTQADate.OptionsColumn.AllowEdit = False
        Me.FTQADate.OptionsColumn.ReadOnly = True
        Me.FTQADate.Visible = True
        Me.FTQADate.VisibleIndex = 1
        Me.FTQADate.Width = 98
        '
        'FTUserQA
        '
        Me.FTUserQA.Caption = "Auditor"
        Me.FTUserQA.FieldName = "FTUserQA"
        Me.FTUserQA.Name = "FTUserQA"
        Me.FTUserQA.OptionsColumn.AllowEdit = False
        Me.FTUserQA.OptionsColumn.ReadOnly = True
        Me.FTUserQA.Visible = True
        Me.FTUserQA.VisibleIndex = 2
        '
        'FTFactory
        '
        Me.FTFactory.Caption = "Factory"
        Me.FTFactory.FieldName = "FTFactory"
        Me.FTFactory.Name = "FTFactory"
        Me.FTFactory.OptionsColumn.AllowEdit = False
        Me.FTFactory.OptionsColumn.ReadOnly = True
        Me.FTFactory.Visible = True
        Me.FTFactory.VisibleIndex = 3
        Me.FTFactory.Width = 64
        '
        'FTPORef
        '
        Me.FTPORef.Caption = "PO"
        Me.FTPORef.FieldName = "FTPORef"
        Me.FTPORef.Name = "FTPORef"
        Me.FTPORef.OptionsColumn.AllowEdit = False
        Me.FTPORef.OptionsColumn.ReadOnly = True
        Me.FTPORef.Visible = True
        Me.FTPORef.VisibleIndex = 4
        Me.FTPORef.Width = 103
        '
        'FNQAInQty
        '
        Me.FNQAInQty.Caption = "Scan Qty"
        Me.FNQAInQty.FieldName = "FNQAInQty"
        Me.FNQAInQty.Name = "FNQAInQty"
        Me.FNQAInQty.OptionsColumn.AllowEdit = False
        Me.FNQAInQty.OptionsColumn.ReadOnly = True
        Me.FNQAInQty.Visible = True
        Me.FNQAInQty.VisibleIndex = 5
        '
        'FNQAActualQty
        '
        Me.FNQAActualQty.Caption = "Audit Qty"
        Me.FNQAActualQty.FieldName = "FNQAActualQty"
        Me.FNQAActualQty.Name = "FNQAActualQty"
        Me.FNQAActualQty.OptionsColumn.AllowEdit = False
        Me.FNQAActualQty.OptionsColumn.ReadOnly = True
        Me.FNQAActualQty.Visible = True
        Me.FNQAActualQty.VisibleIndex = 6
        '
        'FTStyleCode
        '
        Me.FTStyleCode.Caption = "Style"
        Me.FTStyleCode.FieldName = "FTStyleCode"
        Me.FTStyleCode.Name = "FTStyleCode"
        Me.FTStyleCode.OptionsColumn.AllowEdit = False
        Me.FTStyleCode.OptionsColumn.ReadOnly = True
        Me.FTStyleCode.Visible = True
        Me.FTStyleCode.VisibleIndex = 7
        Me.FTStyleCode.Width = 90
        '
        'FTColorway
        '
        Me.FTColorway.Caption = "Colorway"
        Me.FTColorway.FieldName = "FTColorway"
        Me.FTColorway.Name = "FTColorway"
        Me.FTColorway.OptionsColumn.AllowEdit = False
        Me.FTColorway.OptionsColumn.ReadOnly = True
        Me.FTColorway.Visible = True
        Me.FTColorway.VisibleIndex = 8
        Me.FTColorway.Width = 103
        '
        'FTSeasonCode
        '
        Me.FTSeasonCode.Caption = "Season"
        Me.FTSeasonCode.FieldName = "FTSeasonCode"
        Me.FTSeasonCode.Name = "FTSeasonCode"
        Me.FTSeasonCode.OptionsColumn.AllowEdit = False
        Me.FTSeasonCode.OptionsColumn.ReadOnly = True
        Me.FTSeasonCode.Visible = True
        Me.FTSeasonCode.VisibleIndex = 9
        Me.FTSeasonCode.Width = 71
        '
        'FTSubOrderNo
        '
        Me.FTSubOrderNo.Caption = "Lot"
        Me.FTSubOrderNo.FieldName = "FTSubOrderNo"
        Me.FTSubOrderNo.Name = "FTSubOrderNo"
        Me.FTSubOrderNo.OptionsColumn.AllowEdit = False
        Me.FTSubOrderNo.OptionsColumn.ReadOnly = True
        Me.FTSubOrderNo.Visible = True
        Me.FTSubOrderNo.VisibleIndex = 11
        Me.FTSubOrderNo.Width = 92
        '
        'FTStatePreFinal
        '
        Me.FTStatePreFinal.Caption = "Metal Defect"
        Me.FTStatePreFinal.FieldName = "FTStatePreFinal"
        Me.FTStatePreFinal.Name = "FTStatePreFinal"
        Me.FTStatePreFinal.OptionsColumn.AllowEdit = False
        Me.FTStatePreFinal.OptionsColumn.ReadOnly = True
        Me.FTStatePreFinal.Visible = True
        Me.FTStatePreFinal.VisibleIndex = 12
        Me.FTStatePreFinal.Width = 86
        '
        'FTJobState
        '
        Me.FTJobState.Caption = "Order Status"
        Me.FTJobState.FieldName = "FTJobState"
        Me.FTJobState.Name = "FTJobState"
        Me.FTJobState.OptionsColumn.AllowEdit = False
        Me.FTJobState.OptionsColumn.ReadOnly = True
        Me.FTJobState.Width = 99
        '
        'RepositoryItemStateCheck
        '
        Me.RepositoryItemStateCheck.AutoHeight = False
        Me.RepositoryItemStateCheck.Caption = "Check"
        Me.RepositoryItemStateCheck.Name = "RepositoryItemStateCheck"
        Me.RepositoryItemStateCheck.Tag = "FNStateQC"
        Me.RepositoryItemStateCheck.ValueChecked = 1.0!
        Me.RepositoryItemStateCheck.ValueUnchecked = 0!
        '
        'RepositoryItemRandomQty
        '
        Me.RepositoryItemRandomQty.AutoHeight = False
        Me.RepositoryItemRandomQty.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemRandomQty.Name = "RepositoryItemRandomQty"
        Me.RepositoryItemRandomQty.Precision = 4
        '
        'ReposFNStateQC
        '
        Me.ReposFNStateQC.AutoHeight = False
        Me.ReposFNStateQC.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ReposFNStateQC.Name = "ReposFNStateQC"
        Me.ReposFNStateQC.Tag = "FNStateQC"
        '
        'ReposDefectQty
        '
        Me.ReposDefectQty.AutoHeight = False
        Me.ReposDefectQty.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ReposDefectQty.Name = "ReposDefectQty"
        Me.ReposDefectQty.Precision = 4
        '
        'RepositoryFNQCActual
        '
        Me.RepositoryFNQCActual.AutoHeight = False
        Me.RepositoryFNQCActual.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryFNQCActual.Name = "RepositoryFNQCActual"
        Me.RepositoryFNQCActual.Precision = 4
        '
        'RepositoryQCDefectQtyNew
        '
        Me.RepositoryQCDefectQtyNew.AllowFocused = False
        Me.RepositoryQCDefectQtyNew.AutoHeight = False
        Me.RepositoryQCDefectQtyNew.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryQCDefectQtyNew.Name = "RepositoryQCDefectQtyNew"
        Me.RepositoryQCDefectQtyNew.Precision = 4
        '
        'cFTSeasonCodeYear
        '
        Me.cFTSeasonCodeYear.Caption = "FTSeasonCodeYear"
        Me.cFTSeasonCodeYear.FieldName = "FTSeasonCodeYear"
        Me.cFTSeasonCodeYear.MinWidth = 25
        Me.cFTSeasonCodeYear.Name = "cFTSeasonCodeYear"
        Me.cFTSeasonCodeYear.OptionsColumn.AllowEdit = False
        Me.cFTSeasonCodeYear.Visible = True
        Me.cFTSeasonCodeYear.VisibleIndex = 10
        Me.cFTSeasonCodeYear.Width = 66
        '
        'wReportQADetail_tracking
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1354, 703)
        Me.Controls.Add(Me.otb)
        Me.Controls.Add(Me.ogbmainprocbutton)
        Me.Controls.Add(Me.ogbheader)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "wReportQADetail_tracking"
        Me.Text = "wReportQADetail_tracking"
        CType(Me.ocmdoc, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbheader.ResumeLayout(False)
        Me.DockPanel1_Container.ResumeLayout(False)
        CType(Me.oChkSelectAll.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogbcompany, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbcompany.ResumeLayout(False)
        CType(Me.ogccmp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvcmp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ReposFNCompensationFoundByYearOption, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTEndDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTEndDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTStartDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTStartDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbmainprocbutton.ResumeLayout(False)
        CType(Me.otb, System.ComponentModel.ISupportInitialize).EndInit()
        Me.otb.ResumeLayout(False)
        Me.otpdata.ResumeLayout(False)
        CType(Me.ogcDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemStateCheck, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemRandomQty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ReposFNStateQC, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ReposDefectQty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryFNQCActual, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryQCDefectQtyNew, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ocmdoc As DevExpress.XtraBars.Docking.DockManager
    Friend WithEvents ogbheader As DevExpress.XtraBars.Docking.DockPanel
    Friend WithEvents DockPanel1_Container As DevExpress.XtraBars.Docking.ControlContainer
    Friend WithEvents FTEndDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents FTEndDate_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTStartDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents FTStartDate_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ogbmainprocbutton As DevExpress.XtraEditors.PanelControl
    Friend WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmload As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents otb As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents otpdata As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents ogcDetail As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvDetail As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents FTVenderPramCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTQADate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTUserQA As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTFactory As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTPORef As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNQAInQty As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNQAActualQty As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTStyleCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTColorway As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTSeasonCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTSubOrderNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTStatePreFinal As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemStateCheck As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents RepositoryItemRandomQty As DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit
    Friend WithEvents ReposFNStateQC As DevExpress.XtraEditors.Repository.RepositoryItemComboBox
    Friend WithEvents ReposDefectQty As DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit
    Friend WithEvents RepositoryFNQCActual As DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit
    Friend WithEvents RepositoryQCDefectQtyNew As DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit
    Friend WithEvents FTJobState As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ogbcompany As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ogccmp As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvcmp As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GCmpSelectCmp As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemCheckEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTCmpCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTCmpName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTIPServer As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ReposFNCompensationFoundByYearOption As DevExpress.XtraEditors.Repository.RepositoryItemComboBox
    Friend WithEvents oChkSelectAll As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents cFTSeasonCodeYear As DevExpress.XtraGrid.Columns.GridColumn
End Class

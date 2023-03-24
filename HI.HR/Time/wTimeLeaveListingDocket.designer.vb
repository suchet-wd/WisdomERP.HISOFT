<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wTimeLeaveListingDocket
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(wTimeLeaveListingDocket))
        Me.ogbheader = New DevExpress.XtraBars.Docking.DockPanel()
        Me.DockPanel1_Container = New DevExpress.XtraBars.Docking.ControlContainer()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.ogcNationality = New DevExpress.XtraGrid.GridControl()
        Me.ogvNationality = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.FTSelect = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepC1FTSelect = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.FNHSysNationalityId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTNationalityCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTDescription = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCheckEdit2 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.RepositoryItemCheckEdit3 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.RepFNIncentiveAmt = New DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit()
        Me.FNDayInAdvance_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNDayInAdvance = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.FNEmpSex_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNEmpSex = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.ogbdetail = New DevExpress.XtraEditors.GroupControl()
        Me.ogbmainprocbutton = New DevExpress.XtraEditors.PanelControl()
        Me.ocmpreview = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmsavelayout = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmclear = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmload = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmsavedetail = New DevExpress.XtraEditors.SimpleButton()
        Me.ogcDocket = New DevExpress.XtraGrid.GridControl()
        Me.ogvDocket = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.FNListIndex = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTNameTH = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTNameEN = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.oDockManager = New DevExpress.XtraBars.Docking.DockManager(Me.components)
        Me.hideContainerTop = New DevExpress.XtraBars.Docking.AutoHideContainer()
        Me.ogbheader.SuspendLayout()
        Me.DockPanel1_Container.SuspendLayout()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.ogcNationality, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvNationality, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepC1FTSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepFNIncentiveAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNDayInAdvance.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNEmpSex.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogbdetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbdetail.SuspendLayout()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbmainprocbutton.SuspendLayout()
        CType(Me.ogcDocket, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvDocket, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.oDockManager, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.hideContainerTop.SuspendLayout()
        Me.SuspendLayout()
        '
        'ogbheader
        '
        Me.ogbheader.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.ogbheader.Appearance.Options.UseForeColor = True
        Me.ogbheader.Appearance.Options.UseTextOptions = True
        Me.ogbheader.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ogbheader.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.ogbheader.Controls.Add(Me.DockPanel1_Container)
        Me.ogbheader.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ogbheader.Dock = DevExpress.XtraBars.Docking.DockingStyle.Top
        Me.ogbheader.DockVertical = DevExpress.Utils.DefaultBoolean.[False]
        Me.ogbheader.ID = New System.Guid("77b9346d-8d15-4323-af1e-af82afa9902a")
        Me.ogbheader.ImageOptions.Image = CType(resources.GetObject("ogbheader.ImageOptions.Image"), System.Drawing.Image)
        Me.ogbheader.Location = New System.Drawing.Point(0, 0)
        Me.ogbheader.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbheader.Name = "ogbheader"
        Me.ogbheader.Options.AllowDockBottom = False
        Me.ogbheader.Options.AllowDockFill = False
        Me.ogbheader.Options.FloatOnDblClick = False
        Me.ogbheader.Options.ShowCloseButton = False
        Me.ogbheader.Options.ShowMaximizeButton = False
        Me.ogbheader.OriginalSize = New System.Drawing.Size(375, 375)
        Me.ogbheader.SavedDock = DevExpress.XtraBars.Docking.DockingStyle.Top
        Me.ogbheader.SavedIndex = 0
        Me.ogbheader.Size = New System.Drawing.Size(1671, 375)
        Me.ogbheader.TabStop = False
        Me.ogbheader.Text = "Criteria"
        Me.ogbheader.Visibility = DevExpress.XtraBars.Docking.DockVisibility.AutoHide
        '
        'DockPanel1_Container
        '
        Me.DockPanel1_Container.Controls.Add(Me.GroupControl1)
        Me.DockPanel1_Container.Controls.Add(Me.FNDayInAdvance_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FNDayInAdvance)
        Me.DockPanel1_Container.Controls.Add(Me.FNEmpSex_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FNEmpSex)
        Me.DockPanel1_Container.Location = New System.Drawing.Point(4, 32)
        Me.DockPanel1_Container.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.DockPanel1_Container.Name = "DockPanel1_Container"
        Me.DockPanel1_Container.Size = New System.Drawing.Size(1663, 337)
        Me.DockPanel1_Container.TabIndex = 0
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Me.ogcNationality)
        Me.GroupControl1.Location = New System.Drawing.Point(302, 3)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(320, 323)
        Me.GroupControl1.TabIndex = 337
        Me.GroupControl1.Text = "National"
        '
        'ogcNationality
        '
        Me.ogcNationality.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogcNationality.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcNationality.Location = New System.Drawing.Point(2, 28)
        Me.ogcNationality.MainView = Me.ogvNationality
        Me.ogcNationality.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcNationality.Name = "ogcNationality"
        Me.ogcNationality.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepC1FTSelect, Me.RepositoryItemCheckEdit2, Me.RepositoryItemCheckEdit3, Me.RepFNIncentiveAmt})
        Me.ogcNationality.Size = New System.Drawing.Size(316, 293)
        Me.ogcNationality.TabIndex = 6
        Me.ogcNationality.TabStop = False
        Me.ogcNationality.Tag = "2|"
        Me.ogcNationality.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvNationality})
        '
        'ogvNationality
        '
        Me.ogvNationality.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.FTSelect, Me.FNHSysNationalityId, Me.FTNationalityCode, Me.FTDescription})
        Me.ogvNationality.GridControl = Me.ogcNationality
        Me.ogvNationality.Name = "ogvNationality"
        Me.ogvNationality.OptionsCustomization.AllowGroup = False
        Me.ogvNationality.OptionsCustomization.AllowQuickHideColumns = False
        Me.ogvNationality.OptionsView.AllowCellMerge = True
        Me.ogvNationality.OptionsView.ColumnAutoWidth = False
        Me.ogvNationality.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.ogvNationality.OptionsView.ShowGroupPanel = False
        Me.ogvNationality.Tag = "2|"
        '
        'FTSelect
        '
        Me.FTSelect.AppearanceHeader.Options.UseTextOptions = True
        Me.FTSelect.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTSelect.Caption = "FTSelect"
        Me.FTSelect.ColumnEdit = Me.RepC1FTSelect
        Me.FTSelect.FieldName = "FTSelect"
        Me.FTSelect.Name = "FTSelect"
        Me.FTSelect.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTSelect.OptionsColumn.AllowMove = False
        Me.FTSelect.OptionsColumn.AllowShowHide = False
        Me.FTSelect.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTSelect.OptionsColumn.FixedWidth = True
        Me.FTSelect.OptionsColumn.ShowCaption = False
        Me.FTSelect.OptionsColumn.ShowInCustomizationForm = False
        Me.FTSelect.Visible = True
        Me.FTSelect.VisibleIndex = 0
        Me.FTSelect.Width = 41
        '
        'RepC1FTSelect
        '
        Me.RepC1FTSelect.AutoHeight = False
        Me.RepC1FTSelect.Caption = "Check"
        Me.RepC1FTSelect.Name = "RepC1FTSelect"
        Me.RepC1FTSelect.ValueChecked = "1"
        Me.RepC1FTSelect.ValueUnchecked = "0"
        '
        'FNHSysNationalityId
        '
        Me.FNHSysNationalityId.AppearanceHeader.Options.UseTextOptions = True
        Me.FNHSysNationalityId.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FNHSysNationalityId.Caption = "FNHSysEmpID"
        Me.FNHSysNationalityId.FieldName = "FNHSysNationalityId"
        Me.FNHSysNationalityId.Name = "FNHSysNationalityId"
        Me.FNHSysNationalityId.OptionsColumn.AllowEdit = False
        Me.FNHSysNationalityId.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNHSysNationalityId.OptionsColumn.AllowMove = False
        Me.FNHSysNationalityId.OptionsColumn.AllowShowHide = False
        Me.FNHSysNationalityId.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNHSysNationalityId.OptionsColumn.ReadOnly = True
        Me.FNHSysNationalityId.OptionsColumn.ShowInCustomizationForm = False
        Me.FNHSysNationalityId.OptionsColumn.TabStop = False
        '
        'FTNationalityCode
        '
        Me.FTNationalityCode.AppearanceHeader.Options.UseTextOptions = True
        Me.FTNationalityCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTNationalityCode.Caption = "FTNationalityCode"
        Me.FTNationalityCode.FieldName = "FTNationalityCode"
        Me.FTNationalityCode.Name = "FTNationalityCode"
        Me.FTNationalityCode.OptionsColumn.AllowEdit = False
        Me.FTNationalityCode.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.FTNationalityCode.OptionsColumn.AllowMove = False
        Me.FTNationalityCode.OptionsColumn.AllowShowHide = False
        Me.FTNationalityCode.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTNationalityCode.OptionsColumn.ReadOnly = True
        Me.FTNationalityCode.OptionsColumn.ShowInCustomizationForm = False
        Me.FTNationalityCode.OptionsColumn.TabStop = False
        Me.FTNationalityCode.Width = 105
        '
        'FTDescription
        '
        Me.FTDescription.Caption = "FTDescription"
        Me.FTDescription.FieldName = "FTDescription"
        Me.FTDescription.Name = "FTDescription"
        Me.FTDescription.OptionsColumn.AllowEdit = False
        Me.FTDescription.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTDescription.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTDescription.OptionsColumn.AllowMove = False
        Me.FTDescription.OptionsColumn.ReadOnly = True
        Me.FTDescription.OptionsColumn.ShowInCustomizationForm = False
        Me.FTDescription.OptionsColumn.TabStop = False
        Me.FTDescription.Visible = True
        Me.FTDescription.VisibleIndex = 1
        Me.FTDescription.Width = 150
        '
        'RepositoryItemCheckEdit2
        '
        Me.RepositoryItemCheckEdit2.AutoHeight = False
        Me.RepositoryItemCheckEdit2.Caption = "Check"
        Me.RepositoryItemCheckEdit2.Name = "RepositoryItemCheckEdit2"
        Me.RepositoryItemCheckEdit2.ValueChecked = "1"
        Me.RepositoryItemCheckEdit2.ValueUnchecked = "0"
        '
        'RepositoryItemCheckEdit3
        '
        Me.RepositoryItemCheckEdit3.AutoHeight = False
        Me.RepositoryItemCheckEdit3.Caption = "Check"
        Me.RepositoryItemCheckEdit3.Name = "RepositoryItemCheckEdit3"
        Me.RepositoryItemCheckEdit3.ValueChecked = "1"
        Me.RepositoryItemCheckEdit3.ValueUnchecked = "0"
        '
        'RepFNIncentiveAmt
        '
        Me.RepFNIncentiveAmt.AutoHeight = False
        Me.RepFNIncentiveAmt.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepFNIncentiveAmt.DisplayFormat.FormatString = "{0:n2}"
        Me.RepFNIncentiveAmt.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.RepFNIncentiveAmt.EditFormat.FormatString = "{0:n2}"
        Me.RepFNIncentiveAmt.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.RepFNIncentiveAmt.Name = "RepFNIncentiveAmt"
        Me.RepFNIncentiveAmt.Precision = 0
        '
        'FNDayInAdvance_lbl
        '
        Me.FNDayInAdvance_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FNDayInAdvance_lbl.Appearance.Options.UseForeColor = True
        Me.FNDayInAdvance_lbl.Appearance.Options.UseTextOptions = True
        Me.FNDayInAdvance_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNDayInAdvance_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNDayInAdvance_lbl.Location = New System.Drawing.Point(14, 4)
        Me.FNDayInAdvance_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNDayInAdvance_lbl.Name = "FNDayInAdvance_lbl"
        Me.FNDayInAdvance_lbl.Size = New System.Drawing.Size(125, 25)
        Me.FNDayInAdvance_lbl.TabIndex = 336
        Me.FNDayInAdvance_lbl.Tag = "2|"
        Me.FNDayInAdvance_lbl.Text = "Day In Advance :"
        '
        'FNDayInAdvance
        '
        Me.FNDayInAdvance.EditValue = ""
        Me.FNDayInAdvance.EnterMoveNextControl = True
        Me.FNDayInAdvance.Location = New System.Drawing.Point(140, 1)
        Me.FNDayInAdvance.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.FNDayInAdvance.Name = "FNDayInAdvance"
        Me.FNDayInAdvance.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FNDayInAdvance.Properties.Appearance.Options.UseBackColor = True
        Me.FNDayInAdvance.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FNDayInAdvance.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FNDayInAdvance.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FNDayInAdvance.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FNDayInAdvance.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNDayInAdvance.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNDayInAdvance.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNDayInAdvance.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNDayInAdvance.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNDayInAdvance.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNDayInAdvance.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNDayInAdvance.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNDayInAdvance.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FNDayInAdvance.Properties.Tag = "FNDayInAdvance"
        Me.FNDayInAdvance.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.FNDayInAdvance.Size = New System.Drawing.Size(124, 22)
        Me.FNDayInAdvance.TabIndex = 335
        Me.FNDayInAdvance.Tag = "2|"
        '
        'FNEmpSex_lbl
        '
        Me.FNEmpSex_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FNEmpSex_lbl.Appearance.Options.UseForeColor = True
        Me.FNEmpSex_lbl.Appearance.Options.UseTextOptions = True
        Me.FNEmpSex_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNEmpSex_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNEmpSex_lbl.Location = New System.Drawing.Point(14, 32)
        Me.FNEmpSex_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNEmpSex_lbl.Name = "FNEmpSex_lbl"
        Me.FNEmpSex_lbl.Size = New System.Drawing.Size(125, 25)
        Me.FNEmpSex_lbl.TabIndex = 334
        Me.FNEmpSex_lbl.Tag = "2|"
        Me.FNEmpSex_lbl.Text = "Sex :"
        '
        'FNEmpSex
        '
        Me.FNEmpSex.EditValue = ""
        Me.FNEmpSex.EnterMoveNextControl = True
        Me.FNEmpSex.Location = New System.Drawing.Point(139, 34)
        Me.FNEmpSex.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.FNEmpSex.Name = "FNEmpSex"
        Me.FNEmpSex.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FNEmpSex.Properties.Appearance.Options.UseBackColor = True
        Me.FNEmpSex.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FNEmpSex.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FNEmpSex.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FNEmpSex.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FNEmpSex.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNEmpSex.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNEmpSex.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNEmpSex.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNEmpSex.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNEmpSex.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNEmpSex.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNEmpSex.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNEmpSex.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FNEmpSex.Properties.Tag = "FNEmpSex"
        Me.FNEmpSex.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.FNEmpSex.Size = New System.Drawing.Size(124, 22)
        Me.FNEmpSex.TabIndex = 333
        Me.FNEmpSex.Tag = "2|"
        '
        'ogbdetail
        '
        Me.ogbdetail.Controls.Add(Me.ogbmainprocbutton)
        Me.ogbdetail.Controls.Add(Me.ogcDocket)
        Me.ogbdetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogbdetail.Location = New System.Drawing.Point(0, 42)
        Me.ogbdetail.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbdetail.Name = "ogbdetail"
        Me.ogbdetail.ShowCaption = False
        Me.ogbdetail.Size = New System.Drawing.Size(1671, 750)
        Me.ogbdetail.TabIndex = 0
        '
        'ogbmainprocbutton
        '
        Me.ogbmainprocbutton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmpreview)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmsavelayout)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmexit)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmclear)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmload)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmsavedetail)
        Me.ogbmainprocbutton.Location = New System.Drawing.Point(451, 135)
        Me.ogbmainprocbutton.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbmainprocbutton.Name = "ogbmainprocbutton"
        Me.ogbmainprocbutton.Size = New System.Drawing.Size(1091, 58)
        Me.ogbmainprocbutton.TabIndex = 386
        Me.ogbmainprocbutton.Tag = "2|"
        '
        'ocmpreview
        '
        Me.ocmpreview.Location = New System.Drawing.Point(630, 14)
        Me.ocmpreview.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmpreview.Name = "ocmpreview"
        Me.ocmpreview.Size = New System.Drawing.Size(113, 31)
        Me.ocmpreview.TabIndex = 333
        Me.ocmpreview.TabStop = False
        Me.ocmpreview.Tag = "2|"
        Me.ocmpreview.Text = "PREVIEW"
        '
        'ocmsavelayout
        '
        Me.ocmsavelayout.Location = New System.Drawing.Point(477, 15)
        Me.ocmsavelayout.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmsavelayout.Name = "ocmsavelayout"
        Me.ocmsavelayout.Size = New System.Drawing.Size(136, 28)
        Me.ocmsavelayout.TabIndex = 332
        Me.ocmsavelayout.Text = "savelayoutgrid"
        '
        'ocmexit
        '
        Me.ocmexit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmexit.Location = New System.Drawing.Point(959, 14)
        Me.ocmexit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmexit.Name = "ocmexit"
        Me.ocmexit.Size = New System.Drawing.Size(111, 31)
        Me.ocmexit.TabIndex = 96
        Me.ocmexit.TabStop = False
        Me.ocmexit.Tag = "2|"
        Me.ocmexit.Text = "EXIT"
        '
        'ocmclear
        '
        Me.ocmclear.Location = New System.Drawing.Point(134, 14)
        Me.ocmclear.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmclear.Name = "ocmclear"
        Me.ocmclear.Size = New System.Drawing.Size(111, 31)
        Me.ocmclear.TabIndex = 95
        Me.ocmclear.TabStop = False
        Me.ocmclear.Tag = "2|"
        Me.ocmclear.Text = "CLEAR"
        '
        'ocmload
        '
        Me.ocmload.Location = New System.Drawing.Point(260, 16)
        Me.ocmload.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmload.Name = "ocmload"
        Me.ocmload.Size = New System.Drawing.Size(136, 28)
        Me.ocmload.TabIndex = 329
        Me.ocmload.Text = "Load Data"
        '
        'ocmsavedetail
        '
        Me.ocmsavedetail.Location = New System.Drawing.Point(10, 14)
        Me.ocmsavedetail.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmsavedetail.Name = "ocmsavedetail"
        Me.ocmsavedetail.Size = New System.Drawing.Size(111, 31)
        Me.ocmsavedetail.TabIndex = 93
        Me.ocmsavedetail.TabStop = False
        Me.ocmsavedetail.Tag = "2|"
        Me.ocmsavedetail.Text = "SAVE"
        '
        'ogcDocket
        '
        Me.ogcDocket.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogcDocket.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcDocket.Location = New System.Drawing.Point(2, 2)
        Me.ogcDocket.MainView = Me.ogvDocket
        Me.ogcDocket.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcDocket.Name = "ogcDocket"
        Me.ogcDocket.Size = New System.Drawing.Size(1667, 746)
        Me.ogcDocket.TabIndex = 0
        Me.ogcDocket.TabStop = False
        Me.ogcDocket.Tag = "2|"
        Me.ogcDocket.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvDocket})
        '
        'ogvDocket
        '
        Me.ogvDocket.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.FNListIndex, Me.FTNameTH, Me.FTNameEN})
        Me.ogvDocket.GridControl = Me.ogcDocket
        Me.ogvDocket.Name = "ogvDocket"
        Me.ogvDocket.OptionsCustomization.AllowQuickHideColumns = False
        Me.ogvDocket.OptionsView.ColumnAutoWidth = False
        Me.ogvDocket.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.ogvDocket.OptionsView.ShowGroupPanel = False
        Me.ogvDocket.Tag = "2|"
        '
        'FNListIndex
        '
        Me.FNListIndex.Caption = "FNListIndex"
        Me.FNListIndex.FieldName = "FNListIndex"
        Me.FNListIndex.Name = "FNListIndex"
        '
        'FTNameTH
        '
        Me.FTNameTH.Caption = "FTNameTH"
        Me.FTNameTH.FieldName = "FTNameTH"
        Me.FTNameTH.Name = "FTNameTH"
        Me.FTNameTH.Visible = True
        Me.FTNameTH.VisibleIndex = 0
        '
        'FTNameEN
        '
        Me.FTNameEN.Caption = "FTNameEN"
        Me.FTNameEN.FieldName = "FTNameEN"
        Me.FTNameEN.Name = "FTNameEN"
        '
        'oDockManager
        '
        Me.oDockManager.AutoHideContainers.AddRange(New DevExpress.XtraBars.Docking.AutoHideContainer() {Me.hideContainerTop})
        Me.oDockManager.Form = Me
        Me.oDockManager.TopZIndexControls.AddRange(New String() {"DevExpress.XtraBars.BarDockControl", "DevExpress.XtraBars.StandaloneBarDockControl", "System.Windows.Forms.StatusBar", "DevExpress.XtraBars.Ribbon.RibbonStatusBar", "DevExpress.XtraBars.Ribbon.RibbonControl"})
        '
        'hideContainerTop
        '
        Me.hideContainerTop.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.hideContainerTop.Controls.Add(Me.ogbheader)
        Me.hideContainerTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.hideContainerTop.Location = New System.Drawing.Point(0, 0)
        Me.hideContainerTop.Name = "hideContainerTop"
        Me.hideContainerTop.Size = New System.Drawing.Size(1671, 42)
        '
        'wTimeLeaveListingDocket
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1671, 792)
        Me.Controls.Add(Me.ogbdetail)
        Me.Controls.Add(Me.hideContainerTop)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "wTimeLeaveListingDocket"
        Me.Text = "wTimeLeaveListingDocket"
        Me.ogbheader.ResumeLayout(False)
        Me.DockPanel1_Container.ResumeLayout(False)
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        CType(Me.ogcNationality, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvNationality, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepC1FTSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepFNIncentiveAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNDayInAdvance.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNEmpSex.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogbdetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbdetail.ResumeLayout(False)
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbmainprocbutton.ResumeLayout(False)
        CType(Me.ogcDocket, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvDocket, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.oDockManager, System.ComponentModel.ISupportInitialize).EndInit()
        Me.hideContainerTop.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogbdetail As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ogcDocket As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvDocket As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents ogbmainprocbutton As DevExpress.XtraEditors.PanelControl
    Friend WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmclear As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmsavedetail As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmload As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents oDockManager As DevExpress.XtraBars.Docking.DockManager
    Friend WithEvents ogbheader As DevExpress.XtraBars.Docking.DockPanel
    Friend WithEvents DockPanel1_Container As DevExpress.XtraBars.Docking.ControlContainer
    Friend WithEvents ocmsavelayout As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmpreview As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FNListIndex As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTNameTH As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTNameEN As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNEmpSex_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNEmpSex As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents FNDayInAdvance_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNDayInAdvance As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ogcNationality As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvNationality As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents FTSelect As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepC1FTSelect As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents FNHSysNationalityId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTNationalityCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTDescription As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemCheckEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents RepositoryItemCheckEdit3 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents RepFNIncentiveAmt As DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit
    Friend WithEvents hideContainerTop As DevExpress.XtraBars.Docking.AutoHideContainer
End Class

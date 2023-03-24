<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wRptEmpSocialAndTax
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(wRptEmpSocialAndTax))
        Dim EditorButtonImageOptions1 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject4 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim EditorButtonImageOptions2 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim SerializableAppearanceObject5 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject6 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject7 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject8 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.ogbheader = New DevExpress.XtraBars.Docking.DockPanel()
        Me.DockPanel1_Container = New DevExpress.XtraBars.Docking.ControlContainer()
        Me.FTPayYear = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.FNHSysCmpId = New DevExpress.XtraEditors.ButtonEdit()
        Me.FTPayTerm = New DevExpress.XtraEditors.ButtonEdit()
        Me.FTPayTerm_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysCmpId_None = New DevExpress.XtraEditors.TextEdit()
        Me.FTPayYear_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysCmpId_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.ogbdetail = New DevExpress.XtraEditors.GroupControl()
        Me.ogbmainprocbutton = New DevExpress.XtraEditors.PanelControl()
        Me.ocmpreview = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmsavelayout = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmclear = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmload = New DevExpress.XtraEditors.SimpleButton()
        Me.ogdtime = New DevExpress.XtraGrid.GridControl()
        Me.ogvtime = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.cFDCalDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNSalary = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFCOTBaht = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNIncentiveAmt = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cOtherAdd = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cOtherExpense = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNTotalIncome = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNSocial = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNTax = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNNetpay = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cAvgSalary = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNSocialPack = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNTaxPack = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cEmpTotal = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepCheckEdit = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.oDockManager = New DevExpress.XtraBars.Docking.DockManager(Me.components)
        Me.hideContainerTop = New DevExpress.XtraBars.Docking.AutoHideContainer()
        Me.ogbheader.SuspendLayout()
        Me.DockPanel1_Container.SuspendLayout()
        CType(Me.FTPayYear.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysCmpId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTPayTerm.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysCmpId_None.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogbdetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbdetail.SuspendLayout()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbmainprocbutton.SuspendLayout()
        CType(Me.ogdtime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvtime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepCheckEdit, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.ogbheader.OriginalSize = New System.Drawing.Size(1326, 132)
        Me.ogbheader.SavedDock = DevExpress.XtraBars.Docking.DockingStyle.Top
        Me.ogbheader.SavedIndex = 0
        Me.ogbheader.Size = New System.Drawing.Size(1326, 132)
        Me.ogbheader.TabStop = False
        Me.ogbheader.Text = "Criteria"
        Me.ogbheader.Visibility = DevExpress.XtraBars.Docking.DockVisibility.AutoHide
        '
        'DockPanel1_Container
        '
        Me.DockPanel1_Container.Controls.Add(Me.FTPayYear)
        Me.DockPanel1_Container.Controls.Add(Me.FNHSysCmpId)
        Me.DockPanel1_Container.Controls.Add(Me.FTPayTerm)
        Me.DockPanel1_Container.Controls.Add(Me.FTPayTerm_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FNHSysCmpId_None)
        Me.DockPanel1_Container.Controls.Add(Me.FTPayYear_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FNHSysCmpId_lbl)
        Me.DockPanel1_Container.Location = New System.Drawing.Point(4, 32)
        Me.DockPanel1_Container.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.DockPanel1_Container.Name = "DockPanel1_Container"
        Me.DockPanel1_Container.Size = New System.Drawing.Size(1318, 94)
        Me.DockPanel1_Container.TabIndex = 0
        '
        'FTPayYear
        '
        Me.FTPayYear.EditValue = ""
        Me.FTPayYear.EnterMoveNextControl = True
        Me.FTPayYear.Location = New System.Drawing.Point(129, 31)
        Me.FTPayYear.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTPayYear.Name = "FTPayYear"
        Me.FTPayYear.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FTPayYear.Properties.Appearance.Options.UseBackColor = True
        Me.FTPayYear.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FTPayYear.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FTPayYear.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FTPayYear.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FTPayYear.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FTPayYear.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FTPayYear.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FTPayYear.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FTPayYear.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FTPayYear.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FTPayYear.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FTPayYear.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FTPayYear.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FTPayYear.Properties.Tag = "FNPayYear"
        Me.FTPayYear.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.FTPayYear.Size = New System.Drawing.Size(132, 22)
        Me.FTPayYear.TabIndex = 404
        Me.FTPayYear.Tag = "2|"
        '
        'FNHSysCmpId
        '
        Me.FNHSysCmpId.Location = New System.Drawing.Point(129, 4)
        Me.FNHSysCmpId.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysCmpId.Name = "FNHSysCmpId"
        Me.FNHSysCmpId.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysCmpId.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysCmpId.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNHSysCmpId.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNHSysCmpId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, False, False, EditorButtonImageOptions1, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, SerializableAppearanceObject2, SerializableAppearanceObject3, SerializableAppearanceObject4, "", "11", Nothing, DevExpress.Utils.ToolTipAnchor.[Default])})
        Me.FNHSysCmpId.Properties.ReadOnly = True
        Me.FNHSysCmpId.Properties.Tag = ""
        Me.FNHSysCmpId.Size = New System.Drawing.Size(132, 22)
        Me.FNHSysCmpId.TabIndex = 333
        Me.FNHSysCmpId.Tag = ""
        '
        'FTPayTerm
        '
        Me.FTPayTerm.Location = New System.Drawing.Point(587, 31)
        Me.FTPayTerm.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTPayTerm.Name = "FTPayTerm"
        Me.FTPayTerm.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, EditorButtonImageOptions2, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject5, SerializableAppearanceObject6, SerializableAppearanceObject7, SerializableAppearanceObject8, "", "237", Nothing, DevExpress.Utils.ToolTipAnchor.[Default])})
        Me.FTPayTerm.Properties.Tag = "23"
        Me.FTPayTerm.Size = New System.Drawing.Size(131, 22)
        Me.FTPayTerm.TabIndex = 403
        Me.FTPayTerm.Tag = "2|"
        Me.FTPayTerm.Visible = False
        '
        'FTPayTerm_lbl
        '
        Me.FTPayTerm_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTPayTerm_lbl.Appearance.Options.UseForeColor = True
        Me.FTPayTerm_lbl.Appearance.Options.UseTextOptions = True
        Me.FTPayTerm_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTPayTerm_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTPayTerm_lbl.Location = New System.Drawing.Point(486, 34)
        Me.FTPayTerm_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTPayTerm_lbl.Name = "FTPayTerm_lbl"
        Me.FTPayTerm_lbl.Size = New System.Drawing.Size(98, 18)
        Me.FTPayTerm_lbl.TabIndex = 402
        Me.FTPayTerm_lbl.Tag = "2|"
        Me.FTPayTerm_lbl.Text = "งวดที่ :"
        Me.FTPayTerm_lbl.Visible = False
        '
        'FNHSysCmpId_None
        '
        Me.FNHSysCmpId_None.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FNHSysCmpId_None.Location = New System.Drawing.Point(264, 4)
        Me.FNHSysCmpId_None.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysCmpId_None.Name = "FNHSysCmpId_None"
        Me.FNHSysCmpId_None.Properties.Appearance.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysCmpId_None.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysCmpId_None.Properties.ReadOnly = True
        Me.FNHSysCmpId_None.Size = New System.Drawing.Size(970, 22)
        Me.FNHSysCmpId_None.TabIndex = 366
        Me.FNHSysCmpId_None.Tag = ""
        '
        'FTPayYear_lbl
        '
        Me.FTPayYear_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTPayYear_lbl.Appearance.Options.UseForeColor = True
        Me.FTPayYear_lbl.Appearance.Options.UseTextOptions = True
        Me.FTPayYear_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTPayYear_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTPayYear_lbl.Location = New System.Drawing.Point(2, 32)
        Me.FTPayYear_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTPayYear_lbl.Name = "FTPayYear_lbl"
        Me.FTPayYear_lbl.Size = New System.Drawing.Size(125, 21)
        Me.FTPayYear_lbl.TabIndex = 401
        Me.FTPayYear_lbl.Tag = "2|"
        Me.FTPayYear_lbl.Text = "ปี :"
        '
        'FNHSysCmpId_lbl
        '
        Me.FNHSysCmpId_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysCmpId_lbl.Appearance.Options.UseForeColor = True
        Me.FNHSysCmpId_lbl.Appearance.Options.UseTextOptions = True
        Me.FNHSysCmpId_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysCmpId_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysCmpId_lbl.Location = New System.Drawing.Point(2, 4)
        Me.FNHSysCmpId_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysCmpId_lbl.Name = "FNHSysCmpId_lbl"
        Me.FNHSysCmpId_lbl.Size = New System.Drawing.Size(125, 21)
        Me.FNHSysCmpId_lbl.TabIndex = 365
        Me.FNHSysCmpId_lbl.Tag = "2|"
        Me.FNHSysCmpId_lbl.Text = "Company :"
        '
        'ogbdetail
        '
        Me.ogbdetail.Controls.Add(Me.ogbmainprocbutton)
        Me.ogbdetail.Controls.Add(Me.ogdtime)
        Me.ogbdetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogbdetail.Location = New System.Drawing.Point(0, 42)
        Me.ogbdetail.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbdetail.Name = "ogbdetail"
        Me.ogbdetail.ShowCaption = False
        Me.ogbdetail.Size = New System.Drawing.Size(1326, 737)
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
        Me.ogbmainprocbutton.Location = New System.Drawing.Point(106, 135)
        Me.ogbmainprocbutton.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbmainprocbutton.Name = "ogbmainprocbutton"
        Me.ogbmainprocbutton.Size = New System.Drawing.Size(1091, 58)
        Me.ogbmainprocbutton.TabIndex = 386
        Me.ogbmainprocbutton.Tag = "2|"
        '
        'ocmpreview
        '
        Me.ocmpreview.Location = New System.Drawing.Point(278, 14)
        Me.ocmpreview.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmpreview.Name = "ocmpreview"
        Me.ocmpreview.Size = New System.Drawing.Size(124, 31)
        Me.ocmpreview.TabIndex = 333
        Me.ocmpreview.TabStop = False
        Me.ocmpreview.Tag = "2|"
        Me.ocmpreview.Text = "Preview"
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
        Me.ocmclear.Location = New System.Drawing.Point(16, 12)
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
        Me.ocmload.Location = New System.Drawing.Point(134, 15)
        Me.ocmload.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmload.Name = "ocmload"
        Me.ocmload.Size = New System.Drawing.Size(136, 28)
        Me.ocmload.TabIndex = 329
        Me.ocmload.Text = "Load Data"
        '
        'ogdtime
        '
        Me.ogdtime.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogdtime.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogdtime.Location = New System.Drawing.Point(2, 2)
        Me.ogdtime.MainView = Me.ogvtime
        Me.ogdtime.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogdtime.Name = "ogdtime"
        Me.ogdtime.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepCheckEdit})
        Me.ogdtime.Size = New System.Drawing.Size(1322, 733)
        Me.ogdtime.TabIndex = 0
        Me.ogdtime.TabStop = False
        Me.ogdtime.Tag = "2|"
        Me.ogdtime.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvtime})
        '
        'ogvtime
        '
        Me.ogvtime.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.cFDCalDate, Me.cFNSalary, Me.cFCOTBaht, Me.cFNIncentiveAmt, Me.cOtherAdd, Me.cOtherExpense, Me.cFNTotalIncome, Me.cFNSocial, Me.cFNTax, Me.cFNNetpay, Me.cAvgSalary, Me.cFNSocialPack, Me.cFNTaxPack, Me.cEmpTotal})
        Me.ogvtime.GridControl = Me.ogdtime
        Me.ogvtime.Name = "ogvtime"
        Me.ogvtime.OptionsMenu.ShowGroupSummaryEditorItem = True
        Me.ogvtime.OptionsView.ColumnAutoWidth = False
        Me.ogvtime.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.ogvtime.OptionsView.ShowFooter = True
        Me.ogvtime.Tag = "2|"
        '
        'cFDCalDate
        '
        Me.cFDCalDate.Caption = "FDCalDate"
        Me.cFDCalDate.FieldName = "FDCalDate"
        Me.cFDCalDate.Name = "cFDCalDate"
        Me.cFDCalDate.OptionsColumn.AllowEdit = False
        Me.cFDCalDate.Visible = True
        Me.cFDCalDate.VisibleIndex = 0
        Me.cFDCalDate.Width = 100
        '
        'cFNSalary
        '
        Me.cFNSalary.Caption = "Salary"
        Me.cFNSalary.DisplayFormat.FormatString = "{0:n2}"
        Me.cFNSalary.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cFNSalary.FieldName = "FNSalary"
        Me.cFNSalary.Name = "cFNSalary"
        Me.cFNSalary.OptionsColumn.AllowEdit = False
        Me.cFNSalary.Visible = True
        Me.cFNSalary.VisibleIndex = 1
        Me.cFNSalary.Width = 100
        '
        'cFCOTBaht
        '
        Me.cFCOTBaht.Caption = "FCOTBaht"
        Me.cFCOTBaht.DisplayFormat.FormatString = "{0:n2}"
        Me.cFCOTBaht.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cFCOTBaht.FieldName = "FCOTBaht"
        Me.cFCOTBaht.Name = "cFCOTBaht"
        Me.cFCOTBaht.OptionsColumn.AllowEdit = False
        Me.cFCOTBaht.Visible = True
        Me.cFCOTBaht.VisibleIndex = 2
        Me.cFCOTBaht.Width = 100
        '
        'cFNIncentiveAmt
        '
        Me.cFNIncentiveAmt.Caption = "FNIncentiveAmt"
        Me.cFNIncentiveAmt.DisplayFormat.FormatString = "{0:n2}"
        Me.cFNIncentiveAmt.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cFNIncentiveAmt.FieldName = "FNIncentiveAmt"
        Me.cFNIncentiveAmt.Name = "cFNIncentiveAmt"
        Me.cFNIncentiveAmt.OptionsColumn.AllowEdit = False
        Me.cFNIncentiveAmt.Visible = True
        Me.cFNIncentiveAmt.VisibleIndex = 3
        Me.cFNIncentiveAmt.Width = 100
        '
        'cOtherAdd
        '
        Me.cOtherAdd.Caption = "OtherAdd"
        Me.cOtherAdd.DisplayFormat.FormatString = "{0:n2}"
        Me.cOtherAdd.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cOtherAdd.FieldName = "OtherAdd"
        Me.cOtherAdd.Name = "cOtherAdd"
        Me.cOtherAdd.OptionsColumn.AllowEdit = False
        Me.cOtherAdd.Visible = True
        Me.cOtherAdd.VisibleIndex = 4
        Me.cOtherAdd.Width = 100
        '
        'cOtherExpense
        '
        Me.cOtherExpense.Caption = "OtherExpense"
        Me.cOtherExpense.DisplayFormat.FormatString = "{0:n2}"
        Me.cOtherExpense.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cOtherExpense.FieldName = "OtherExpense"
        Me.cOtherExpense.Name = "cOtherExpense"
        Me.cOtherExpense.OptionsColumn.AllowEdit = False
        Me.cOtherExpense.Visible = True
        Me.cOtherExpense.VisibleIndex = 5
        Me.cOtherExpense.Width = 100
        '
        'cFNTotalIncome
        '
        Me.cFNTotalIncome.Caption = "FNTotalIncome"
        Me.cFNTotalIncome.DisplayFormat.FormatString = "{0:n2}"
        Me.cFNTotalIncome.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cFNTotalIncome.FieldName = "FNTotalIncome"
        Me.cFNTotalIncome.Name = "cFNTotalIncome"
        Me.cFNTotalIncome.OptionsColumn.AllowEdit = False
        Me.cFNTotalIncome.Visible = True
        Me.cFNTotalIncome.VisibleIndex = 6
        Me.cFNTotalIncome.Width = 100
        '
        'cFNSocial
        '
        Me.cFNSocial.Caption = "FNSocial"
        Me.cFNSocial.DisplayFormat.FormatString = "{0:n2}"
        Me.cFNSocial.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cFNSocial.FieldName = "FNSocial"
        Me.cFNSocial.Name = "cFNSocial"
        Me.cFNSocial.OptionsColumn.AllowEdit = False
        Me.cFNSocial.Visible = True
        Me.cFNSocial.VisibleIndex = 7
        Me.cFNSocial.Width = 100
        '
        'cFNTax
        '
        Me.cFNTax.Caption = "FNTax"
        Me.cFNTax.DisplayFormat.FormatString = "{0:n2}"
        Me.cFNTax.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cFNTax.FieldName = "FNTax"
        Me.cFNTax.Name = "cFNTax"
        Me.cFNTax.OptionsColumn.AllowEdit = False
        Me.cFNTax.Visible = True
        Me.cFNTax.VisibleIndex = 8
        Me.cFNTax.Width = 100
        '
        'cFNNetpay
        '
        Me.cFNNetpay.Caption = "FNNetpay"
        Me.cFNNetpay.DisplayFormat.FormatString = "{0:n2}"
        Me.cFNNetpay.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cFNNetpay.FieldName = "FNNetpay"
        Me.cFNNetpay.Name = "cFNNetpay"
        Me.cFNNetpay.OptionsColumn.AllowEdit = False
        Me.cFNNetpay.Visible = True
        Me.cFNNetpay.VisibleIndex = 9
        Me.cFNNetpay.Width = 100
        '
        'cAvgSalary
        '
        Me.cAvgSalary.Caption = "AvgSalary"
        Me.cAvgSalary.DisplayFormat.FormatString = "{0:n2}"
        Me.cAvgSalary.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cAvgSalary.FieldName = "AvgSalary"
        Me.cAvgSalary.Name = "cAvgSalary"
        Me.cAvgSalary.OptionsColumn.AllowEdit = False
        Me.cAvgSalary.Width = 90
        '
        'cFNSocialPack
        '
        Me.cFNSocialPack.Caption = "FNSocialPack"
        Me.cFNSocialPack.DisplayFormat.FormatString = "{0:n2}"
        Me.cFNSocialPack.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cFNSocialPack.FieldName = "FNSocialPack"
        Me.cFNSocialPack.Name = "cFNSocialPack"
        Me.cFNSocialPack.OptionsColumn.AllowEdit = False
        Me.cFNSocialPack.Visible = True
        Me.cFNSocialPack.VisibleIndex = 10
        Me.cFNSocialPack.Width = 100
        '
        'cFNTaxPack
        '
        Me.cFNTaxPack.Caption = "FNTaxPack"
        Me.cFNTaxPack.DisplayFormat.FormatString = "{0:n2}"
        Me.cFNTaxPack.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cFNTaxPack.FieldName = "FNTaxPack"
        Me.cFNTaxPack.Name = "cFNTaxPack"
        Me.cFNTaxPack.OptionsColumn.AllowEdit = False
        Me.cFNTaxPack.Visible = True
        Me.cFNTaxPack.VisibleIndex = 11
        Me.cFNTaxPack.Width = 100
        '
        'cEmpTotal
        '
        Me.cEmpTotal.Caption = "EmpTotal"
        Me.cEmpTotal.FieldName = "EmpTotal"
        Me.cEmpTotal.Name = "cEmpTotal"
        Me.cEmpTotal.OptionsColumn.AllowEdit = False
        Me.cEmpTotal.Visible = True
        Me.cEmpTotal.VisibleIndex = 12
        Me.cEmpTotal.Width = 96
        '
        'RepCheckEdit
        '
        Me.RepCheckEdit.AutoHeight = False
        Me.RepCheckEdit.Caption = "Check"
        Me.RepCheckEdit.Name = "RepCheckEdit"
        Me.RepCheckEdit.ValueChecked = "1"
        Me.RepCheckEdit.ValueUnchecked = "0"
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
        Me.hideContainerTop.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.hideContainerTop.Name = "hideContainerTop"
        Me.hideContainerTop.Size = New System.Drawing.Size(1326, 42)
        '
        'wRptEmpSocialAndTax
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1326, 779)
        Me.Controls.Add(Me.ogbdetail)
        Me.Controls.Add(Me.hideContainerTop)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "wRptEmpSocialAndTax"
        Me.Text = "Report Summary Daily Wage"
        Me.ogbheader.ResumeLayout(False)
        Me.DockPanel1_Container.ResumeLayout(False)
        CType(Me.FTPayYear.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysCmpId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTPayTerm.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysCmpId_None.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogbdetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbdetail.ResumeLayout(False)
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbmainprocbutton.ResumeLayout(False)
        CType(Me.ogdtime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvtime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepCheckEdit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.oDockManager, System.ComponentModel.ISupportInitialize).EndInit()
        Me.hideContainerTop.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogbdetail As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ogdtime As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvtime As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents ogbmainprocbutton As DevExpress.XtraEditors.PanelControl
    Friend WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmclear As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmload As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents oDockManager As DevExpress.XtraBars.Docking.DockManager
    Friend WithEvents ogbheader As DevExpress.XtraBars.Docking.DockPanel
    Friend WithEvents DockPanel1_Container As DevExpress.XtraBars.Docking.ControlContainer
    Friend WithEvents ocmsavelayout As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents RepCheckEdit As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents ocmpreview As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents cFNSalary As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFCOTBaht As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNIncentiveAmt As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cOtherAdd As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNTotalIncome As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNSocial As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNTax As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNNetpay As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cAvgSalary As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cEmpTotal As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNHSysCmpId As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FNHSysCmpId_None As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FNHSysCmpId_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTPayYear As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents FTPayTerm As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FTPayTerm_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTPayYear_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents hideContainerTop As DevExpress.XtraBars.Docking.AutoHideContainer
    Friend WithEvents cFDCalDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNSocialPack As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNTaxPack As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cOtherExpense As DevExpress.XtraGrid.Columns.GridColumn
End Class

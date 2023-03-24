<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wCompensationFundOfYear
    Inherits DevExpress.XtraEditors.XtraForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(wCompensationFundOfYear))
        Me.ogc = New DevExpress.XtraGrid.GridControl()
        Me.ogv = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.FNMonthSeq = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTMonthName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNTotalEmp = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNEmpMTotalSalary = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNEmpDTotalSalary = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNEmpOtherTotalSalary = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNTotalSalary = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNEmpOverTotalSalary = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNTotalPaySalary = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNEmpOTTotalSalary = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNEmpBonusTotalSalary = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNEmpDMinSalary = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNEmpMMinSalary = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.DockManager1 = New DevExpress.XtraBars.Docking.DockManager()
        Me.ogbheader = New DevExpress.XtraBars.Docking.DockPanel()
        Me.DockPanel1_Container = New DevExpress.XtraBars.Docking.ControlContainer()
        Me.ogbcompany = New DevExpress.XtraEditors.GroupControl()
        Me.ogccmp = New DevExpress.XtraGrid.GridControl()
        Me.ogvcmp = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GCmpSelectCmp = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCheckEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTCmpCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTCmpName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTIPServer = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNCompensationFoundByYearOption = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ReposFNCompensationFoundByYearOption = New DevExpress.XtraEditors.Repository.RepositoryItemComboBox()
        Me.FNCompensationFoundByYearOption_Hide = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTDateStart_lb = New DevExpress.XtraEditors.LabelControl()
        Me.FTDateStart = New DevExpress.XtraEditors.DateEdit()
        Me.FNHSysEmpTypeId_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysSectId_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysDivisonId_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysEmpId_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysUnitSectId_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.ogbmainprocbutton = New DevExpress.XtraEditors.PanelControl()
        Me.ocmpreview = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmsavelayout = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmclear = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmload = New DevExpress.XtraEditors.SimpleButton()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.DateEdit1 = New DevExpress.XtraEditors.DateEdit()
        Me.otbmain = New DevExpress.XtraTab.XtraTabControl()
        Me.otpsummary = New DevExpress.XtraTab.XtraTabPage()
        CType(Me.ogc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DockManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbheader.SuspendLayout()
        Me.DockPanel1_Container.SuspendLayout()
        CType(Me.ogbcompany, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbcompany.SuspendLayout()
        CType(Me.ogccmp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvcmp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ReposFNCompensationFoundByYearOption, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTDateStart.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTDateStart.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbmainprocbutton.SuspendLayout()
        CType(Me.DateEdit1.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DateEdit1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.otbmain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.otbmain.SuspendLayout()
        Me.otpsummary.SuspendLayout()
        Me.SuspendLayout()
        '
        'ogc
        '
        Me.ogc.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogc.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4)
        Me.ogc.Location = New System.Drawing.Point(0, 0)
        Me.ogc.MainView = Me.ogv
        Me.ogc.Margin = New System.Windows.Forms.Padding(4)
        Me.ogc.Name = "ogc"
        Me.ogc.Size = New System.Drawing.Size(1144, 395)
        Me.ogc.TabIndex = 0
        Me.ogc.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogv})
        '
        'ogv
        '
        Me.ogv.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.FNMonthSeq, Me.FTMonthName, Me.FNTotalEmp, Me.FNEmpMTotalSalary, Me.FNEmpDTotalSalary, Me.FNEmpOtherTotalSalary, Me.FNTotalSalary, Me.FNEmpOverTotalSalary, Me.FNTotalPaySalary, Me.FNEmpOTTotalSalary, Me.FNEmpBonusTotalSalary, Me.FNEmpDMinSalary, Me.FNEmpMMinSalary})
        Me.ogv.GridControl = Me.ogc
        Me.ogv.Name = "ogv"
        Me.ogv.OptionsCustomization.AllowQuickHideColumns = False
        Me.ogv.OptionsMenu.ShowGroupSummaryEditorItem = True
        Me.ogv.OptionsView.ColumnAutoWidth = False
        Me.ogv.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.ogv.OptionsView.ShowGroupPanel = False
        Me.ogv.Tag = "2|"
        '
        'FNMonthSeq
        '
        Me.FNMonthSeq.Caption = "FNMonthSeq"
        Me.FNMonthSeq.FieldName = "FNMonthSeq"
        Me.FNMonthSeq.Name = "FNMonthSeq"
        Me.FNMonthSeq.OptionsColumn.AllowEdit = False
        Me.FNMonthSeq.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNMonthSeq.OptionsColumn.AllowMove = False
        Me.FNMonthSeq.OptionsColumn.AllowShowHide = False
        Me.FNMonthSeq.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNMonthSeq.OptionsColumn.ReadOnly = True
        '
        'FTMonthName
        '
        Me.FTMonthName.Caption = "Month"
        Me.FTMonthName.FieldName = "FTMonthName"
        Me.FTMonthName.Name = "FTMonthName"
        Me.FTMonthName.OptionsColumn.AllowEdit = False
        Me.FTMonthName.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTMonthName.OptionsColumn.AllowMove = False
        Me.FTMonthName.OptionsColumn.AllowShowHide = False
        Me.FTMonthName.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTMonthName.OptionsColumn.ReadOnly = True
        Me.FTMonthName.Visible = True
        Me.FTMonthName.VisibleIndex = 0
        Me.FTMonthName.Width = 171
        '
        'FNTotalEmp
        '
        Me.FNTotalEmp.Caption = "จำนวนลูกจ้าง"
        Me.FNTotalEmp.DisplayFormat.FormatString = "{0:n0}"
        Me.FNTotalEmp.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNTotalEmp.FieldName = "FNTotalEmp"
        Me.FNTotalEmp.Name = "FNTotalEmp"
        Me.FNTotalEmp.OptionsColumn.AllowEdit = False
        Me.FNTotalEmp.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNTotalEmp.OptionsColumn.AllowMove = False
        Me.FNTotalEmp.OptionsColumn.AllowShowHide = False
        Me.FNTotalEmp.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNTotalEmp.OptionsColumn.ReadOnly = True
        Me.FNTotalEmp.Visible = True
        Me.FNTotalEmp.VisibleIndex = 1
        Me.FNTotalEmp.Width = 98
        '
        'FNEmpMTotalSalary
        '
        Me.FNEmpMTotalSalary.Caption = "เงินเดือน"
        Me.FNEmpMTotalSalary.DisplayFormat.FormatString = "{0:n2}"
        Me.FNEmpMTotalSalary.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNEmpMTotalSalary.FieldName = "FNEmpMTotalSalary"
        Me.FNEmpMTotalSalary.Name = "FNEmpMTotalSalary"
        Me.FNEmpMTotalSalary.OptionsColumn.AllowEdit = False
        Me.FNEmpMTotalSalary.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNEmpMTotalSalary.OptionsColumn.AllowMove = False
        Me.FNEmpMTotalSalary.OptionsColumn.AllowShowHide = False
        Me.FNEmpMTotalSalary.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNEmpMTotalSalary.OptionsColumn.ReadOnly = True
        Me.FNEmpMTotalSalary.Visible = True
        Me.FNEmpMTotalSalary.VisibleIndex = 2
        Me.FNEmpMTotalSalary.Width = 120
        '
        'FNEmpDTotalSalary
        '
        Me.FNEmpDTotalSalary.Caption = "ค่าจ้างรายวัน"
        Me.FNEmpDTotalSalary.DisplayFormat.FormatString = "{0:n2}"
        Me.FNEmpDTotalSalary.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNEmpDTotalSalary.FieldName = "FNEmpDTotalSalary"
        Me.FNEmpDTotalSalary.Name = "FNEmpDTotalSalary"
        Me.FNEmpDTotalSalary.OptionsColumn.AllowEdit = False
        Me.FNEmpDTotalSalary.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNEmpDTotalSalary.OptionsColumn.AllowMove = False
        Me.FNEmpDTotalSalary.OptionsColumn.AllowShowHide = False
        Me.FNEmpDTotalSalary.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNEmpDTotalSalary.OptionsColumn.ReadOnly = True
        Me.FNEmpDTotalSalary.Visible = True
        Me.FNEmpDTotalSalary.VisibleIndex = 3
        Me.FNEmpDTotalSalary.Width = 120
        '
        'FNEmpOtherTotalSalary
        '
        Me.FNEmpOtherTotalSalary.Caption = "ค่าอื่นๆ"
        Me.FNEmpOtherTotalSalary.DisplayFormat.FormatString = "{0:n2}"
        Me.FNEmpOtherTotalSalary.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNEmpOtherTotalSalary.FieldName = "FNEmpOtherTotalSalary"
        Me.FNEmpOtherTotalSalary.Name = "FNEmpOtherTotalSalary"
        Me.FNEmpOtherTotalSalary.OptionsColumn.AllowEdit = False
        Me.FNEmpOtherTotalSalary.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNEmpOtherTotalSalary.OptionsColumn.AllowMove = False
        Me.FNEmpOtherTotalSalary.OptionsColumn.AllowShowHide = False
        Me.FNEmpOtherTotalSalary.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNEmpOtherTotalSalary.OptionsColumn.ReadOnly = True
        Me.FNEmpOtherTotalSalary.Visible = True
        Me.FNEmpOtherTotalSalary.VisibleIndex = 4
        Me.FNEmpOtherTotalSalary.Width = 120
        '
        'FNTotalSalary
        '
        Me.FNTotalSalary.Caption = "รวมค่าจ้าง"
        Me.FNTotalSalary.DisplayFormat.FormatString = "{0:n2}"
        Me.FNTotalSalary.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNTotalSalary.FieldName = "FNTotalSalary"
        Me.FNTotalSalary.Name = "FNTotalSalary"
        Me.FNTotalSalary.OptionsColumn.AllowEdit = False
        Me.FNTotalSalary.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNTotalSalary.OptionsColumn.AllowMove = False
        Me.FNTotalSalary.OptionsColumn.AllowShowHide = False
        Me.FNTotalSalary.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNTotalSalary.OptionsColumn.ReadOnly = True
        Me.FNTotalSalary.Visible = True
        Me.FNTotalSalary.VisibleIndex = 5
        Me.FNTotalSalary.Width = 120
        '
        'FNEmpOverTotalSalary
        '
        Me.FNEmpOverTotalSalary.Caption = "ส่วนที่เกิน 20000"
        Me.FNEmpOverTotalSalary.DisplayFormat.FormatString = "{0:n2}"
        Me.FNEmpOverTotalSalary.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNEmpOverTotalSalary.FieldName = "FNEmpOverTotalSalary"
        Me.FNEmpOverTotalSalary.Name = "FNEmpOverTotalSalary"
        Me.FNEmpOverTotalSalary.OptionsColumn.AllowEdit = False
        Me.FNEmpOverTotalSalary.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNEmpOverTotalSalary.OptionsColumn.AllowMove = False
        Me.FNEmpOverTotalSalary.OptionsColumn.AllowShowHide = False
        Me.FNEmpOverTotalSalary.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNEmpOverTotalSalary.OptionsColumn.ReadOnly = True
        Me.FNEmpOverTotalSalary.Visible = True
        Me.FNEmpOverTotalSalary.VisibleIndex = 6
        Me.FNEmpOverTotalSalary.Width = 120
        '
        'FNTotalPaySalary
        '
        Me.FNTotalPaySalary.Caption = "ค่าจ้างสุทธิที่ต้องแจ้ง"
        Me.FNTotalPaySalary.DisplayFormat.FormatString = "{0:n2}"
        Me.FNTotalPaySalary.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNTotalPaySalary.FieldName = "FNTotalPaySalary"
        Me.FNTotalPaySalary.Name = "FNTotalPaySalary"
        Me.FNTotalPaySalary.OptionsColumn.AllowEdit = False
        Me.FNTotalPaySalary.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNTotalPaySalary.OptionsColumn.AllowMove = False
        Me.FNTotalPaySalary.OptionsColumn.AllowShowHide = False
        Me.FNTotalPaySalary.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNTotalPaySalary.OptionsColumn.ReadOnly = True
        Me.FNTotalPaySalary.Visible = True
        Me.FNTotalPaySalary.VisibleIndex = 7
        Me.FNTotalPaySalary.Width = 170
        '
        'FNEmpOTTotalSalary
        '
        Me.FNEmpOTTotalSalary.Caption = "FNEmpOTTotalSalary"
        Me.FNEmpOTTotalSalary.FieldName = "FNEmpOTTotalSalary"
        Me.FNEmpOTTotalSalary.Name = "FNEmpOTTotalSalary"
        Me.FNEmpOTTotalSalary.OptionsColumn.AllowEdit = False
        Me.FNEmpOTTotalSalary.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNEmpOTTotalSalary.OptionsColumn.AllowMove = False
        Me.FNEmpOTTotalSalary.OptionsColumn.AllowShowHide = False
        Me.FNEmpOTTotalSalary.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNEmpOTTotalSalary.OptionsColumn.ReadOnly = True
        '
        'FNEmpBonusTotalSalary
        '
        Me.FNEmpBonusTotalSalary.Caption = "FNEmpBonusTotalSalary"
        Me.FNEmpBonusTotalSalary.FieldName = "FNEmpBonusTotalSalary"
        Me.FNEmpBonusTotalSalary.Name = "FNEmpBonusTotalSalary"
        Me.FNEmpBonusTotalSalary.OptionsColumn.AllowEdit = False
        Me.FNEmpBonusTotalSalary.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNEmpBonusTotalSalary.OptionsColumn.AllowMove = False
        Me.FNEmpBonusTotalSalary.OptionsColumn.AllowShowHide = False
        Me.FNEmpBonusTotalSalary.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNEmpBonusTotalSalary.OptionsColumn.ReadOnly = True
        '
        'FNEmpDMinSalary
        '
        Me.FNEmpDMinSalary.Caption = "FNEmpDMinSalary"
        Me.FNEmpDMinSalary.FieldName = "FNEmpDMinSalary"
        Me.FNEmpDMinSalary.Name = "FNEmpDMinSalary"
        '
        'FNEmpMMinSalary
        '
        Me.FNEmpMMinSalary.Caption = "FNEmpMMinSalary"
        Me.FNEmpMMinSalary.FieldName = "FNEmpMMinSalary"
        Me.FNEmpMMinSalary.Name = "FNEmpMMinSalary"
        '
        'DockManager1
        '
        Me.DockManager1.Form = Me
        Me.DockManager1.RootPanels.AddRange(New DevExpress.XtraBars.Docking.DockPanel() {Me.ogbheader})
        Me.DockManager1.TopZIndexControls.AddRange(New String() {"DevExpress.XtraBars.BarDockControl", "DevExpress.XtraBars.StandaloneBarDockControl", "System.Windows.Forms.StatusBar", "System.Windows.Forms.MenuStrip", "System.Windows.Forms.StatusStrip", "DevExpress.XtraBars.Ribbon.RibbonStatusBar", "DevExpress.XtraBars.Ribbon.RibbonControl"})
        '
        'ogbheader
        '
        Me.ogbheader.Controls.Add(Me.DockPanel1_Container)
        Me.ogbheader.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ogbheader.Dock = DevExpress.XtraBars.Docking.DockingStyle.Top
        Me.ogbheader.DockVertical = DevExpress.Utils.DefaultBoolean.[False]
        Me.ogbheader.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.ogbheader.ForeColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(31, Byte), Integer), CType(CType(53, Byte), Integer))
        Me.ogbheader.ID = New System.Guid("07aa44b9-5e74-47ef-b37d-869730080592")
        Me.ogbheader.Image = CType(resources.GetObject("ogbheader.Image"), System.Drawing.Image)
        Me.ogbheader.Location = New System.Drawing.Point(0, 0)
        Me.ogbheader.Margin = New System.Windows.Forms.Padding(4)
        Me.ogbheader.Name = "ogbheader"
        Me.ogbheader.Options.AllowDockBottom = False
        Me.ogbheader.Options.AllowDockFill = False
        Me.ogbheader.Options.FloatOnDblClick = False
        Me.ogbheader.Options.ShowCloseButton = False
        Me.ogbheader.Options.ShowMaximizeButton = False
        Me.ogbheader.OriginalSize = New System.Drawing.Size(256, 256)
        Me.ogbheader.Size = New System.Drawing.Size(1182, 256)
        Me.ogbheader.TabStop = False
        Me.ogbheader.Text = "Criteria"
        '
        'DockPanel1_Container
        '
        Me.DockPanel1_Container.Controls.Add(Me.ogbcompany)
        Me.DockPanel1_Container.Controls.Add(Me.FTDateStart_lb)
        Me.DockPanel1_Container.Controls.Add(Me.FTDateStart)
        Me.DockPanel1_Container.Location = New System.Drawing.Point(2, 28)
        Me.DockPanel1_Container.Margin = New System.Windows.Forms.Padding(4)
        Me.DockPanel1_Container.Name = "DockPanel1_Container"
        Me.DockPanel1_Container.Size = New System.Drawing.Size(1178, 226)
        Me.DockPanel1_Container.TabIndex = 0
        '
        'ogbcompany
        '
        Me.ogbcompany.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbcompany.Controls.Add(Me.ogccmp)
        Me.ogbcompany.Location = New System.Drawing.Point(10, 40)
        Me.ogbcompany.Name = "ogbcompany"
        Me.ogbcompany.Size = New System.Drawing.Size(1158, 182)
        Me.ogbcompany.TabIndex = 505
        Me.ogbcompany.Text = "Select Company"
        '
        'ogccmp
        '
        Me.ogccmp.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogccmp.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogccmp.Location = New System.Drawing.Point(2, 25)
        Me.ogccmp.MainView = Me.ogvcmp
        Me.ogccmp.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogccmp.Name = "ogccmp"
        Me.ogccmp.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCheckEdit1, Me.ReposFNCompensationFoundByYearOption})
        Me.ogccmp.Size = New System.Drawing.Size(1154, 155)
        Me.ogccmp.TabIndex = 5
        Me.ogccmp.TabStop = False
        Me.ogccmp.Tag = "3|"
        Me.ogccmp.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvcmp})
        '
        'ogvcmp
        '
        Me.ogvcmp.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GCmpSelectCmp, Me.GridColumn2, Me.FTCmpCode, Me.FTCmpName, Me.FTIPServer, Me.FNCompensationFoundByYearOption, Me.FNCompensationFoundByYearOption_Hide})
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
        'FNCompensationFoundByYearOption
        '
        Me.FNCompensationFoundByYearOption.Caption = "Option"
        Me.FNCompensationFoundByYearOption.ColumnEdit = Me.ReposFNCompensationFoundByYearOption
        Me.FNCompensationFoundByYearOption.FieldName = "FNCompensationFoundByYearOption"
        Me.FNCompensationFoundByYearOption.Name = "FNCompensationFoundByYearOption"
        Me.FNCompensationFoundByYearOption.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.FNCompensationFoundByYearOption.OptionsColumn.AllowMove = False
        Me.FNCompensationFoundByYearOption.OptionsColumn.AllowShowHide = False
        Me.FNCompensationFoundByYearOption.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNCompensationFoundByYearOption.Visible = True
        Me.FNCompensationFoundByYearOption.VisibleIndex = 3
        Me.FNCompensationFoundByYearOption.Width = 180
        '
        'ReposFNCompensationFoundByYearOption
        '
        Me.ReposFNCompensationFoundByYearOption.AutoHeight = False
        Me.ReposFNCompensationFoundByYearOption.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ReposFNCompensationFoundByYearOption.Name = "ReposFNCompensationFoundByYearOption"
        Me.ReposFNCompensationFoundByYearOption.Tag = "FNCompensationFoundByYearOption"
        Me.ReposFNCompensationFoundByYearOption.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        '
        'FNCompensationFoundByYearOption_Hide
        '
        Me.FNCompensationFoundByYearOption_Hide.Caption = "FNCompensationFoundByYearOption"
        Me.FNCompensationFoundByYearOption_Hide.FieldName = "FNCompensationFoundByYearOption_Hide"
        Me.FNCompensationFoundByYearOption_Hide.Name = "FNCompensationFoundByYearOption_Hide"
        '
        'FTDateStart_lb
        '
        Me.FTDateStart_lb.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTDateStart_lb.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTDateStart_lb.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTDateStart_lb.Location = New System.Drawing.Point(8, 8)
        Me.FTDateStart_lb.Margin = New System.Windows.Forms.Padding(4)
        Me.FTDateStart_lb.Name = "FTDateStart_lb"
        Me.FTDateStart_lb.Size = New System.Drawing.Size(125, 25)
        Me.FTDateStart_lb.TabIndex = 504
        Me.FTDateStart_lb.Tag = "2|"
        Me.FTDateStart_lb.Text = "Year :"
        '
        'FTDateStart
        '
        Me.FTDateStart.EditValue = Nothing
        Me.FTDateStart.EnterMoveNextControl = True
        Me.FTDateStart.Location = New System.Drawing.Point(136, 9)
        Me.FTDateStart.Margin = New System.Windows.Forms.Padding(4)
        Me.FTDateStart.Name = "FTDateStart"
        Me.FTDateStart.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTDateStart.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FTDateStart.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.FTDateStart.Properties.DisplayFormat.FormatString = "yyyy"
        Me.FTDateStart.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.FTDateStart.Properties.EditFormat.FormatString = "yyyy"
        Me.FTDateStart.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.FTDateStart.Properties.Mask.EditMask = "yyyy"
        Me.FTDateStart.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.FTDateStart.Properties.NullDate = ""
        Me.FTDateStart.Size = New System.Drawing.Size(130, 22)
        Me.FTDateStart.TabIndex = 503
        Me.FTDateStart.Tag = "2|"
        '
        'FNHSysEmpTypeId_lbl
        '
        Me.FNHSysEmpTypeId_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FNHSysEmpTypeId_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysEmpTypeId_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysEmpTypeId_lbl.Location = New System.Drawing.Point(14, 26)
        Me.FNHSysEmpTypeId_lbl.Name = "FNHSysEmpTypeId_lbl"
        Me.FNHSysEmpTypeId_lbl.Size = New System.Drawing.Size(107, 17)
        Me.FNHSysEmpTypeId_lbl.TabIndex = 418
        Me.FNHSysEmpTypeId_lbl.Tag = "2|"
        Me.FNHSysEmpTypeId_lbl.Text = "Employee Type :"
        '
        'FNHSysSectId_lbl
        '
        Me.FNHSysSectId_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysSectId_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysSectId_lbl.Location = New System.Drawing.Point(13, 89)
        Me.FNHSysSectId_lbl.Name = "FNHSysSectId_lbl"
        Me.FNHSysSectId_lbl.Size = New System.Drawing.Size(107, 20)
        Me.FNHSysSectId_lbl.TabIndex = 395
        Me.FNHSysSectId_lbl.Tag = "2|"
        Me.FNHSysSectId_lbl.Text = "Start Sect :"
        '
        'FNHSysDivisonId_lbl
        '
        Me.FNHSysDivisonId_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysDivisonId_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysDivisonId_lbl.Location = New System.Drawing.Point(13, 68)
        Me.FNHSysDivisonId_lbl.Name = "FNHSysDivisonId_lbl"
        Me.FNHSysDivisonId_lbl.Size = New System.Drawing.Size(107, 20)
        Me.FNHSysDivisonId_lbl.TabIndex = 393
        Me.FNHSysDivisonId_lbl.Tag = "2|"
        Me.FNHSysDivisonId_lbl.Text = "Start Devision :"
        '
        'FNHSysEmpId_lbl
        '
        Me.FNHSysEmpId_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysEmpId_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysEmpId_lbl.Location = New System.Drawing.Point(13, 131)
        Me.FNHSysEmpId_lbl.Name = "FNHSysEmpId_lbl"
        Me.FNHSysEmpId_lbl.Size = New System.Drawing.Size(107, 20)
        Me.FNHSysEmpId_lbl.TabIndex = 412
        Me.FNHSysEmpId_lbl.Tag = "2|"
        Me.FNHSysEmpId_lbl.Text = "Start Employee :"
        '
        'FNHSysUnitSectId_lbl
        '
        Me.FNHSysUnitSectId_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysUnitSectId_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysUnitSectId_lbl.Location = New System.Drawing.Point(13, 110)
        Me.FNHSysUnitSectId_lbl.Name = "FNHSysUnitSectId_lbl"
        Me.FNHSysUnitSectId_lbl.Size = New System.Drawing.Size(107, 20)
        Me.FNHSysUnitSectId_lbl.TabIndex = 397
        Me.FNHSysUnitSectId_lbl.Tag = "2|"
        Me.FNHSysUnitSectId_lbl.Text = "Start Unit Sect :"
        '
        'ogbmainprocbutton
        '
        Me.ogbmainprocbutton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmpreview)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmsavelayout)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmexit)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmclear)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmload)
        Me.ogbmainprocbutton.Location = New System.Drawing.Point(205, 183)
        Me.ogbmainprocbutton.Margin = New System.Windows.Forms.Padding(4)
        Me.ogbmainprocbutton.Name = "ogbmainprocbutton"
        Me.ogbmainprocbutton.Size = New System.Drawing.Size(833, 58)
        Me.ogbmainprocbutton.TabIndex = 388
        Me.ogbmainprocbutton.Tag = "2|"
        '
        'ocmpreview
        '
        Me.ocmpreview.Location = New System.Drawing.Point(553, 16)
        Me.ocmpreview.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmpreview.Name = "ocmpreview"
        Me.ocmpreview.Size = New System.Drawing.Size(111, 31)
        Me.ocmpreview.TabIndex = 334
        Me.ocmpreview.TabStop = False
        Me.ocmpreview.Tag = "2|"
        Me.ocmpreview.Text = "PREVIEW"
        '
        'ocmsavelayout
        '
        Me.ocmsavelayout.Location = New System.Drawing.Point(377, 15)
        Me.ocmsavelayout.Margin = New System.Windows.Forms.Padding(4)
        Me.ocmsavelayout.Name = "ocmsavelayout"
        Me.ocmsavelayout.Size = New System.Drawing.Size(136, 28)
        Me.ocmsavelayout.TabIndex = 333
        Me.ocmsavelayout.Text = "savelayoutgrid"
        '
        'ocmexit
        '
        Me.ocmexit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmexit.Location = New System.Drawing.Point(701, 14)
        Me.ocmexit.Margin = New System.Windows.Forms.Padding(4)
        Me.ocmexit.Name = "ocmexit"
        Me.ocmexit.Size = New System.Drawing.Size(111, 31)
        Me.ocmexit.TabIndex = 96
        Me.ocmexit.TabStop = False
        Me.ocmexit.Tag = "2|"
        Me.ocmexit.Text = "EXIT"
        '
        'ocmclear
        '
        Me.ocmclear.Location = New System.Drawing.Point(246, 14)
        Me.ocmclear.Margin = New System.Windows.Forms.Padding(4)
        Me.ocmclear.Name = "ocmclear"
        Me.ocmclear.Size = New System.Drawing.Size(111, 31)
        Me.ocmclear.TabIndex = 95
        Me.ocmclear.TabStop = False
        Me.ocmclear.Tag = "2|"
        Me.ocmclear.Text = "CLEAR"
        '
        'ocmload
        '
        Me.ocmload.Location = New System.Drawing.Point(6, 16)
        Me.ocmload.Margin = New System.Windows.Forms.Padding(4)
        Me.ocmload.Name = "ocmload"
        Me.ocmload.Size = New System.Drawing.Size(101, 28)
        Me.ocmload.TabIndex = 329
        Me.ocmload.Text = "Load Data"
        '
        'LabelControl3
        '
        Me.LabelControl3.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.LabelControl3.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LabelControl3.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LabelControl3.Location = New System.Drawing.Point(6, 48)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(107, 20)
        Me.LabelControl3.TabIndex = 504
        Me.LabelControl3.Tag = "2|"
        Me.LabelControl3.Text = "Start Date :"
        '
        'LabelControl4
        '
        Me.LabelControl4.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.LabelControl4.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LabelControl4.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LabelControl4.Location = New System.Drawing.Point(6, 48)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Size = New System.Drawing.Size(107, 20)
        Me.LabelControl4.TabIndex = 504
        Me.LabelControl4.Tag = "2|"
        Me.LabelControl4.Text = "Start Date :"
        '
        'DateEdit1
        '
        Me.DateEdit1.EditValue = Nothing
        Me.DateEdit1.EnterMoveNextControl = True
        Me.DateEdit1.Location = New System.Drawing.Point(116, 48)
        Me.DateEdit1.Name = "DateEdit1"
        Me.DateEdit1.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.DateEdit1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.DateEdit1.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.DateEdit1.Properties.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.DateEdit1.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.DateEdit1.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.DateEdit1.Properties.NullDate = ""
        Me.DateEdit1.Size = New System.Drawing.Size(112, 22)
        Me.DateEdit1.TabIndex = 503
        Me.DateEdit1.Tag = "2|"
        '
        'otbmain
        '
        Me.otbmain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.otbmain.Location = New System.Drawing.Point(0, 256)
        Me.otbmain.Name = "otbmain"
        Me.otbmain.SelectedTabPage = Me.otpsummary
        Me.otbmain.Size = New System.Drawing.Size(1182, 459)
        Me.otbmain.TabIndex = 390
        Me.otbmain.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.otpsummary})
        '
        'otpsummary
        '
        Me.otpsummary.Controls.Add(Me.ogc)
        Me.otpsummary.Name = "otpsummary"
        Me.otpsummary.Size = New System.Drawing.Size(1174, 425)
        Me.otpsummary.Text = "สรุปการจ่ายค่าจ้าง"
        '
        'wCompensationFundOfYear
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1182, 715)
        Me.Controls.Add(Me.ogbmainprocbutton)
        Me.Controls.Add(Me.otbmain)
        Me.Controls.Add(Me.ogbheader)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "wCompensationFundOfYear"
        Me.Text = "Compensation Fund Of Year"
        CType(Me.ogc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DockManager1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbheader.ResumeLayout(False)
        Me.DockPanel1_Container.ResumeLayout(False)
        CType(Me.ogbcompany, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbcompany.ResumeLayout(False)
        CType(Me.ogccmp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvcmp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ReposFNCompensationFoundByYearOption, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTDateStart.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTDateStart.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbmainprocbutton.ResumeLayout(False)
        CType(Me.DateEdit1.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DateEdit1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.otbmain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.otbmain.ResumeLayout(False)
        Me.otpsummary.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogc As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogv As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents DockManager1 As DevExpress.XtraBars.Docking.DockManager
    Friend WithEvents ogbheader As DevExpress.XtraBars.Docking.DockPanel
    Friend WithEvents DockPanel1_Container As DevExpress.XtraBars.Docking.ControlContainer
    Friend WithEvents FNHSysEmpTypeId_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysSectId_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysDivisonId_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysEmpId_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysUnitSectId_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ogbmainprocbutton As DevExpress.XtraEditors.PanelControl
    Friend WithEvents ocmsavelayout As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmclear As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmload As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FTDateStart_lb As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTDateStart As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents DateEdit1 As DevExpress.XtraEditors.DateEdit
    Friend WithEvents otbmain As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents otpsummary As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents ogbcompany As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ogccmp As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvcmp As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GCmpSelectCmp As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemCheckEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTCmpCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTCmpName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTIPServer As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNMonthSeq As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTMonthName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNTotalEmp As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNEmpMTotalSalary As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNEmpDTotalSalary As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNEmpOtherTotalSalary As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNTotalSalary As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNEmpOverTotalSalary As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNTotalPaySalary As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNEmpOTTotalSalary As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNEmpBonusTotalSalary As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNEmpDMinSalary As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNEmpMMinSalary As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNCompensationFoundByYearOption As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ReposFNCompensationFoundByYearOption As DevExpress.XtraEditors.Repository.RepositoryItemComboBox
    Friend WithEvents FNCompensationFoundByYearOption_Hide As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ocmpreview As DevExpress.XtraEditors.SimpleButton
End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wCompensationFundCmpAdjust
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(wCompensationFundCmpAdjust))
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.ogc = New DevExpress.XtraGrid.GridControl()
        Me.ogv = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.FNMonthSeq = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTMonthName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNTotalEmp = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ReposFNTotalEmp = New DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit()
        Me.FNEmpMMinSalary = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ReposSalary = New DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit()
        Me.FNEmpMTotalSalary = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNEmpDMinSalary = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNEmpDTotalSalary = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNEmpOtherTotalSalary = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNTotalSalary = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNEmpOverTotalSalary = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ReposOverTotalSalary = New DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit()
        Me.FNTotalPaySalary = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNEmpOTTotalSalary = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ReposFNEmpOTTotalSalary = New DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit()
        Me.FNEmpBonusTotalSalary = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.DockManager1 = New DevExpress.XtraBars.Docking.DockManager()
        Me.ogbheader = New DevExpress.XtraBars.Docking.DockPanel()
        Me.DockPanel1_Container = New DevExpress.XtraBars.Docking.ControlContainer()
        Me.FNHSysCmpId_None = New DevExpress.XtraEditors.TextEdit()
        Me.FTDateStart_lb = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysCmpId = New DevExpress.XtraEditors.ButtonEdit()
        Me.FNHSysCmpId_lbl_ofyear = New DevExpress.XtraEditors.LabelControl()
        Me.FTDateStart = New DevExpress.XtraEditors.DateEdit()
        Me.FNHSysEmpTypeId_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysSectId_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysDivisonId_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysEmpId_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysUnitSectId_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.ogbmainprocbutton = New DevExpress.XtraEditors.PanelControl()
        Me.ocmdelete = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmsave = New DevExpress.XtraEditors.SimpleButton()
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
        CType(Me.ReposFNTotalEmp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ReposSalary, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ReposOverTotalSalary, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ReposFNEmpOTTotalSalary, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DockManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbheader.SuspendLayout()
        Me.DockPanel1_Container.SuspendLayout()
        CType(Me.FNHSysCmpId_None.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysCmpId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.ogc.Location = New System.Drawing.Point(-2, 0)
        Me.ogc.MainView = Me.ogv
        Me.ogc.Margin = New System.Windows.Forms.Padding(4)
        Me.ogc.Name = "ogc"
        Me.ogc.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.ReposFNTotalEmp, Me.ReposSalary, Me.ReposOverTotalSalary, Me.ReposFNEmpOTTotalSalary})
        Me.ogc.Size = New System.Drawing.Size(1144, 551)
        Me.ogc.TabIndex = 0
        Me.ogc.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogv})
        '
        'ogv
        '
        Me.ogv.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.FNMonthSeq, Me.FTMonthName, Me.FNTotalEmp, Me.FNEmpMMinSalary, Me.FNEmpMTotalSalary, Me.FNEmpDMinSalary, Me.FNEmpDTotalSalary, Me.FNEmpOtherTotalSalary, Me.FNTotalSalary, Me.FNEmpOverTotalSalary, Me.FNTotalPaySalary, Me.FNEmpOTTotalSalary, Me.FNEmpBonusTotalSalary})
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
        Me.FNTotalEmp.ColumnEdit = Me.ReposFNTotalEmp
        Me.FNTotalEmp.DisplayFormat.FormatString = "{0:n0}"
        Me.FNTotalEmp.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNTotalEmp.FieldName = "FNTotalEmp"
        Me.FNTotalEmp.Name = "FNTotalEmp"
        Me.FNTotalEmp.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNTotalEmp.OptionsColumn.AllowMove = False
        Me.FNTotalEmp.OptionsColumn.AllowShowHide = False
        Me.FNTotalEmp.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNTotalEmp.Visible = True
        Me.FNTotalEmp.VisibleIndex = 1
        Me.FNTotalEmp.Width = 98
        '
        'ReposFNTotalEmp
        '
        Me.ReposFNTotalEmp.AutoHeight = False
        Me.ReposFNTotalEmp.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ReposFNTotalEmp.DisplayFormat.FormatString = "{0:n0}"
        Me.ReposFNTotalEmp.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.ReposFNTotalEmp.EditFormat.FormatString = "{0:n0}"
        Me.ReposFNTotalEmp.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.ReposFNTotalEmp.Name = "ReposFNTotalEmp"
        Me.ReposFNTotalEmp.Precision = 0
        '
        'FNEmpMMinSalary
        '
        Me.FNEmpMMinSalary.Caption = "ค้างจ้างรายเดือนขั้นต่ำ"
        Me.FNEmpMMinSalary.ColumnEdit = Me.ReposSalary
        Me.FNEmpMMinSalary.FieldName = "FNEmpMMinSalary"
        Me.FNEmpMMinSalary.Name = "FNEmpMMinSalary"
        Me.FNEmpMMinSalary.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNEmpMMinSalary.OptionsColumn.AllowMove = False
        Me.FNEmpMMinSalary.OptionsColumn.AllowShowHide = False
        Me.FNEmpMMinSalary.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNEmpMMinSalary.Visible = True
        Me.FNEmpMMinSalary.VisibleIndex = 2
        Me.FNEmpMMinSalary.Width = 100
        '
        'ReposSalary
        '
        Me.ReposSalary.AutoHeight = False
        Me.ReposSalary.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ReposSalary.DisplayFormat.FormatString = "{0:n2}"
        Me.ReposSalary.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.ReposSalary.EditFormat.FormatString = "{0:n2}"
        Me.ReposSalary.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.ReposSalary.Name = "ReposSalary"
        Me.ReposSalary.Precision = 2
        '
        'FNEmpMTotalSalary
        '
        Me.FNEmpMTotalSalary.Caption = "เงินเดือน"
        Me.FNEmpMTotalSalary.ColumnEdit = Me.ReposSalary
        Me.FNEmpMTotalSalary.DisplayFormat.FormatString = "{0:n2}"
        Me.FNEmpMTotalSalary.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNEmpMTotalSalary.FieldName = "FNEmpMTotalSalary"
        Me.FNEmpMTotalSalary.Name = "FNEmpMTotalSalary"
        Me.FNEmpMTotalSalary.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNEmpMTotalSalary.OptionsColumn.AllowMove = False
        Me.FNEmpMTotalSalary.OptionsColumn.AllowShowHide = False
        Me.FNEmpMTotalSalary.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNEmpMTotalSalary.Visible = True
        Me.FNEmpMTotalSalary.VisibleIndex = 3
        Me.FNEmpMTotalSalary.Width = 120
        '
        'FNEmpDMinSalary
        '
        Me.FNEmpDMinSalary.Caption = "ค้างจ้างรายวันขั้นต่ำ"
        Me.FNEmpDMinSalary.ColumnEdit = Me.ReposSalary
        Me.FNEmpDMinSalary.FieldName = "FNEmpDMinSalary"
        Me.FNEmpDMinSalary.Name = "FNEmpDMinSalary"
        Me.FNEmpDMinSalary.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNEmpDMinSalary.OptionsColumn.AllowShowHide = False
        Me.FNEmpDMinSalary.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNEmpDMinSalary.Visible = True
        Me.FNEmpDMinSalary.VisibleIndex = 4
        Me.FNEmpDMinSalary.Width = 101
        '
        'FNEmpDTotalSalary
        '
        Me.FNEmpDTotalSalary.Caption = "ค่าจ้างรายวัน"
        Me.FNEmpDTotalSalary.ColumnEdit = Me.ReposSalary
        Me.FNEmpDTotalSalary.DisplayFormat.FormatString = "{0:n2}"
        Me.FNEmpDTotalSalary.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNEmpDTotalSalary.FieldName = "FNEmpDTotalSalary"
        Me.FNEmpDTotalSalary.Name = "FNEmpDTotalSalary"
        Me.FNEmpDTotalSalary.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNEmpDTotalSalary.OptionsColumn.AllowShowHide = False
        Me.FNEmpDTotalSalary.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNEmpDTotalSalary.Visible = True
        Me.FNEmpDTotalSalary.VisibleIndex = 5
        Me.FNEmpDTotalSalary.Width = 120
        '
        'FNEmpOtherTotalSalary
        '
        Me.FNEmpOtherTotalSalary.Caption = "ค่าอื่นๆ"
        Me.FNEmpOtherTotalSalary.ColumnEdit = Me.ReposSalary
        Me.FNEmpOtherTotalSalary.DisplayFormat.FormatString = "{0:n2}"
        Me.FNEmpOtherTotalSalary.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNEmpOtherTotalSalary.FieldName = "FNEmpOtherTotalSalary"
        Me.FNEmpOtherTotalSalary.Name = "FNEmpOtherTotalSalary"
        Me.FNEmpOtherTotalSalary.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNEmpOtherTotalSalary.OptionsColumn.AllowMove = False
        Me.FNEmpOtherTotalSalary.OptionsColumn.AllowShowHide = False
        Me.FNEmpOtherTotalSalary.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
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
        Me.FNTotalSalary.VisibleIndex = 6
        Me.FNTotalSalary.Width = 120
        '
        'FNEmpOverTotalSalary
        '
        Me.FNEmpOverTotalSalary.Caption = "ส่วนที่เกิน 20000"
        Me.FNEmpOverTotalSalary.ColumnEdit = Me.ReposOverTotalSalary
        Me.FNEmpOverTotalSalary.DisplayFormat.FormatString = "{0:n2}"
        Me.FNEmpOverTotalSalary.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNEmpOverTotalSalary.FieldName = "FNEmpOverTotalSalary"
        Me.FNEmpOverTotalSalary.Name = "FNEmpOverTotalSalary"
        Me.FNEmpOverTotalSalary.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNEmpOverTotalSalary.OptionsColumn.AllowMove = False
        Me.FNEmpOverTotalSalary.OptionsColumn.AllowShowHide = False
        Me.FNEmpOverTotalSalary.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNEmpOverTotalSalary.Visible = True
        Me.FNEmpOverTotalSalary.VisibleIndex = 7
        Me.FNEmpOverTotalSalary.Width = 120
        '
        'ReposOverTotalSalary
        '
        Me.ReposOverTotalSalary.AutoHeight = False
        Me.ReposOverTotalSalary.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ReposOverTotalSalary.DisplayFormat.FormatString = "{0:n2}"
        Me.ReposOverTotalSalary.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.ReposOverTotalSalary.EditFormat.FormatString = "{0:n2}"
        Me.ReposOverTotalSalary.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.ReposOverTotalSalary.Name = "ReposOverTotalSalary"
        Me.ReposOverTotalSalary.Precision = 2
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
        Me.FNTotalPaySalary.VisibleIndex = 8
        Me.FNTotalPaySalary.Width = 170
        '
        'FNEmpOTTotalSalary
        '
        Me.FNEmpOTTotalSalary.Caption = "OT"
        Me.FNEmpOTTotalSalary.ColumnEdit = Me.ReposFNEmpOTTotalSalary
        Me.FNEmpOTTotalSalary.DisplayFormat.FormatString = "{0:n2}"
        Me.FNEmpOTTotalSalary.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNEmpOTTotalSalary.FieldName = "FNEmpOTTotalSalary"
        Me.FNEmpOTTotalSalary.Name = "FNEmpOTTotalSalary"
        Me.FNEmpOTTotalSalary.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNEmpOTTotalSalary.OptionsColumn.AllowMove = False
        Me.FNEmpOTTotalSalary.OptionsColumn.AllowShowHide = False
        Me.FNEmpOTTotalSalary.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNEmpOTTotalSalary.Visible = True
        Me.FNEmpOTTotalSalary.VisibleIndex = 9
        '
        'ReposFNEmpOTTotalSalary
        '
        Me.ReposFNEmpOTTotalSalary.AutoHeight = False
        Me.ReposFNEmpOTTotalSalary.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ReposFNEmpOTTotalSalary.DisplayFormat.FormatString = "{0:n2}"
        Me.ReposFNEmpOTTotalSalary.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.ReposFNEmpOTTotalSalary.EditFormat.FormatString = "{0:n2}"
        Me.ReposFNEmpOTTotalSalary.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.ReposFNEmpOTTotalSalary.Name = "ReposFNEmpOTTotalSalary"
        Me.ReposFNEmpOTTotalSalary.Precision = 2
        '
        'FNEmpBonusTotalSalary
        '
        Me.FNEmpBonusTotalSalary.Caption = "Bonus"
        Me.FNEmpBonusTotalSalary.DisplayFormat.FormatString = "{0:n2}"
        Me.FNEmpBonusTotalSalary.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNEmpBonusTotalSalary.FieldName = "FNEmpBonusTotalSalary"
        Me.FNEmpBonusTotalSalary.Name = "FNEmpBonusTotalSalary"
        Me.FNEmpBonusTotalSalary.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNEmpBonusTotalSalary.OptionsColumn.AllowMove = False
        Me.FNEmpBonusTotalSalary.OptionsColumn.AllowShowHide = False
        Me.FNEmpBonusTotalSalary.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNEmpBonusTotalSalary.Visible = True
        Me.FNEmpBonusTotalSalary.VisibleIndex = 10
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
        Me.ogbheader.OriginalSize = New System.Drawing.Size(1182, 100)
        Me.ogbheader.Size = New System.Drawing.Size(1182, 100)
        Me.ogbheader.TabStop = False
        Me.ogbheader.Text = "Criteria"
        '
        'DockPanel1_Container
        '
        Me.DockPanel1_Container.Controls.Add(Me.FNHSysCmpId_None)
        Me.DockPanel1_Container.Controls.Add(Me.FTDateStart_lb)
        Me.DockPanel1_Container.Controls.Add(Me.FNHSysCmpId)
        Me.DockPanel1_Container.Controls.Add(Me.FNHSysCmpId_lbl_ofyear)
        Me.DockPanel1_Container.Controls.Add(Me.FTDateStart)
        Me.DockPanel1_Container.Location = New System.Drawing.Point(2, 28)
        Me.DockPanel1_Container.Margin = New System.Windows.Forms.Padding(4)
        Me.DockPanel1_Container.Name = "DockPanel1_Container"
        Me.DockPanel1_Container.Size = New System.Drawing.Size(1178, 70)
        Me.DockPanel1_Container.TabIndex = 0
        '
        'FNHSysCmpId_None
        '
        Me.FNHSysCmpId_None.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FNHSysCmpId_None.Location = New System.Drawing.Point(280, 11)
        Me.FNHSysCmpId_None.Margin = New System.Windows.Forms.Padding(4)
        Me.FNHSysCmpId_None.Name = "FNHSysCmpId_None"
        Me.FNHSysCmpId_None.Properties.Appearance.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysCmpId_None.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysCmpId_None.Properties.ReadOnly = True
        Me.FNHSysCmpId_None.Size = New System.Drawing.Size(876, 22)
        Me.FNHSysCmpId_None.TabIndex = 488
        Me.FNHSysCmpId_None.Tag = ""
        '
        'FTDateStart_lb
        '
        Me.FTDateStart_lb.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTDateStart_lb.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTDateStart_lb.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTDateStart_lb.Location = New System.Drawing.Point(18, 37)
        Me.FTDateStart_lb.Margin = New System.Windows.Forms.Padding(4)
        Me.FTDateStart_lb.Name = "FTDateStart_lb"
        Me.FTDateStart_lb.Size = New System.Drawing.Size(125, 25)
        Me.FTDateStart_lb.TabIndex = 504
        Me.FTDateStart_lb.Tag = "2|"
        Me.FTDateStart_lb.Text = "Year :"
        '
        'FNHSysCmpId
        '
        Me.FNHSysCmpId.Location = New System.Drawing.Point(146, 11)
        Me.FNHSysCmpId.Margin = New System.Windows.Forms.Padding(4)
        Me.FNHSysCmpId.Name = "FNHSysCmpId"
        Me.FNHSysCmpId.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysCmpId.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysCmpId.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNHSysCmpId.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNHSysCmpId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, False, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", "11", Nothing, True)})
        Me.FNHSysCmpId.Properties.ReadOnly = True
        Me.FNHSysCmpId.Properties.Tag = ""
        Me.FNHSysCmpId.Size = New System.Drawing.Size(132, 22)
        Me.FNHSysCmpId.TabIndex = 487
        Me.FNHSysCmpId.Tag = ""
        '
        'FNHSysCmpId_lbl_ofyear
        '
        Me.FNHSysCmpId_lbl_ofyear.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(31, Byte), Integer), CType(CType(53, Byte), Integer))
        Me.FNHSysCmpId_lbl_ofyear.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysCmpId_lbl_ofyear.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysCmpId_lbl_ofyear.Location = New System.Drawing.Point(17, 8)
        Me.FNHSysCmpId_lbl_ofyear.Margin = New System.Windows.Forms.Padding(4)
        Me.FNHSysCmpId_lbl_ofyear.Name = "FNHSysCmpId_lbl_ofyear"
        Me.FNHSysCmpId_lbl_ofyear.Size = New System.Drawing.Size(125, 21)
        Me.FNHSysCmpId_lbl_ofyear.TabIndex = 486
        Me.FNHSysCmpId_lbl_ofyear.Tag = "2|"
        Me.FNHSysCmpId_lbl_ofyear.Text = "Company :"
        '
        'FTDateStart
        '
        Me.FTDateStart.EditValue = Nothing
        Me.FTDateStart.EnterMoveNextControl = True
        Me.FTDateStart.Location = New System.Drawing.Point(148, 39)
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
        Me.ogbmainprocbutton.Controls.Add(Me.ocmdelete)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmsave)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmsavelayout)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmexit)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmclear)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmload)
        Me.ogbmainprocbutton.Location = New System.Drawing.Point(205, 39)
        Me.ogbmainprocbutton.Margin = New System.Windows.Forms.Padding(4)
        Me.ogbmainprocbutton.Name = "ogbmainprocbutton"
        Me.ogbmainprocbutton.Size = New System.Drawing.Size(833, 97)
        Me.ogbmainprocbutton.TabIndex = 388
        Me.ogbmainprocbutton.Tag = "2|"
        '
        'ocmdelete
        '
        Me.ocmdelete.Location = New System.Drawing.Point(142, 52)
        Me.ocmdelete.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmdelete.Name = "ocmdelete"
        Me.ocmdelete.Size = New System.Drawing.Size(111, 31)
        Me.ocmdelete.TabIndex = 335
        Me.ocmdelete.TabStop = False
        Me.ocmdelete.Tag = "2|"
        Me.ocmdelete.Text = "DELETE"
        '
        'ocmsave
        '
        Me.ocmsave.Location = New System.Drawing.Point(24, 52)
        Me.ocmsave.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmsave.Name = "ocmsave"
        Me.ocmsave.Size = New System.Drawing.Size(111, 31)
        Me.ocmsave.TabIndex = 334
        Me.ocmsave.TabStop = False
        Me.ocmsave.Tag = "2|"
        Me.ocmsave.Text = "SAVE"
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
        Me.otbmain.Location = New System.Drawing.Point(0, 100)
        Me.otbmain.Name = "otbmain"
        Me.otbmain.SelectedTabPage = Me.otpsummary
        Me.otbmain.Size = New System.Drawing.Size(1182, 615)
        Me.otbmain.TabIndex = 390
        Me.otbmain.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.otpsummary})
        '
        'otpsummary
        '
        Me.otpsummary.Controls.Add(Me.ogc)
        Me.otpsummary.Name = "otpsummary"
        Me.otpsummary.Size = New System.Drawing.Size(1174, 581)
        Me.otpsummary.Text = "สรุปการจ่ายค่าจ้าง"
        '
        'wCompensationFundCmpAdjust
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1182, 715)
        Me.Controls.Add(Me.ogbmainprocbutton)
        Me.Controls.Add(Me.otbmain)
        Me.Controls.Add(Me.ogbheader)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "wCompensationFundCmpAdjust"
        Me.Text = "Compensation Fund Of Year By Company"
        CType(Me.ogc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ReposFNTotalEmp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ReposSalary, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ReposOverTotalSalary, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ReposFNEmpOTTotalSalary, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DockManager1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbheader.ResumeLayout(False)
        Me.DockPanel1_Container.ResumeLayout(False)
        CType(Me.FNHSysCmpId_None.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysCmpId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents FNHSysCmpId_None As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FNHSysCmpId As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FNHSysCmpId_lbl_ofyear As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ReposFNTotalEmp As DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit
    Friend WithEvents ReposSalary As DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit
    Friend WithEvents ReposOverTotalSalary As DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit
    Friend WithEvents ReposFNEmpOTTotalSalary As DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit
    Friend WithEvents ocmdelete As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmsave As DevExpress.XtraEditors.SimpleButton
End Class

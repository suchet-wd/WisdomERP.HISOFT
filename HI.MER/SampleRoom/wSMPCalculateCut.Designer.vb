<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wSMPCalculateCut
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
        Me.ogbemployee = New DevExpress.XtraEditors.GroupControl()
        Me.ogbmainprocbutton = New DevExpress.XtraEditors.PanelControl()
        Me.ocmSendApprove = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmdelete = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmpreview = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmclear = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmload = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmcalculateincentive = New DevExpress.XtraEditors.SimpleButton()
        Me.otxtabctrl = New DevExpress.XtraTab.XtraTabControl()
        Me.otpcaltype0 = New DevExpress.XtraTab.XtraTabPage()
        Me.ogc = New DevExpress.XtraGrid.GridControl()
        Me.ogv = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.FTSelect = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryFTSelect = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.CFTStateFinishDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFTSMPOrderNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFTTeam = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFTEmpName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFNQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFNAmount = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFTStateCal = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFTStateCalDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFTStateCalBy = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFTStateFinishDateOrg = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFNHSysEmpID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFTStateSendApp = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFTSendAppBy = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFTSendAppDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFTStateApprove = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTApproveBy = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTApproveDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNEmpTeam = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFTStartSewDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFTStateManagerApprove = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFTManagerApproveBy = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFTManagerApproveDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryFTApproveState = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.ReposFTStateDaily = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.RepQuantity = New DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit()
        Me.otpcut = New DevExpress.XtraTab.XtraTabPage()
        Me.ogdempcut = New DevExpress.XtraGrid.GridControl()
        Me.ogvempcut = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.CFTCalculateDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFTEmpCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNTotalTimHRFull = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNTotalTimeOTHRFull = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNNetTotalTimHRFull = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFNCutAmount = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.C2FTStateCalDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.C2FTStateCalBy = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFTCalculateDateOrg = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.c2FNHSysEmpID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.C2FTStateSendApp = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCheckEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.C2FTSendAppBy = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.C2FTSendAppDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.C2FTStateApprove = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.C2FTApproveBy = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.C2FTApproveDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.C2FTStateManagerApprove = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.C2FTManagerApproveBy = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.C2FTManagerApproveDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCheckEdit2 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.RepositoryItemCheckEdit3 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.RepositoryItemCalcEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit()
        Me.ochkselectall = New DevExpress.XtraEditors.CheckEdit()
        Me.ogbreqtime = New DevExpress.XtraEditors.GroupControl()
        Me.FTEndDate = New DevExpress.XtraEditors.DateEdit()
        Me.FTEndDate_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTStartDate = New DevExpress.XtraEditors.DateEdit()
        Me.FTStartDate_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.oDockManager = New DevExpress.XtraBars.Docking.DockManager()
        Me.ogbdetail = New DevExpress.XtraEditors.GroupControl()
        CType(Me.ogbemployee, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbemployee.SuspendLayout()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbmainprocbutton.SuspendLayout()
        CType(Me.otxtabctrl, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.otxtabctrl.SuspendLayout()
        Me.otpcaltype0.SuspendLayout()
        CType(Me.ogc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryFTSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryFTApproveState, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ReposFTStateDaily, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepQuantity, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.otpcut.SuspendLayout()
        CType(Me.ogdempcut, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvempcut, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCalcEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ochkselectall.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogbreqtime, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbreqtime.SuspendLayout()
        CType(Me.FTEndDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTEndDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTStartDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTStartDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.oDockManager, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogbdetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbdetail.SuspendLayout()
        Me.SuspendLayout()
        '
        'ogbemployee
        '
        Me.ogbemployee.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbemployee.AppearanceCaption.Options.UseTextOptions = True
        Me.ogbemployee.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ogbemployee.Controls.Add(Me.ogbmainprocbutton)
        Me.ogbemployee.Controls.Add(Me.otxtabctrl)
        Me.ogbemployee.Controls.Add(Me.ochkselectall)
        Me.ogbemployee.Location = New System.Drawing.Point(5, 90)
        Me.ogbemployee.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbemployee.Name = "ogbemployee"
        Me.ogbemployee.Size = New System.Drawing.Size(1382, 1019)
        Me.ogbemployee.TabIndex = 4
        Me.ogbemployee.Text = "Employee"
        '
        'ogbmainprocbutton
        '
        Me.ogbmainprocbutton.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmSendApprove)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmdelete)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmpreview)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmexit)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmclear)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmload)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmcalculateincentive)
        Me.ogbmainprocbutton.Location = New System.Drawing.Point(30, 197)
        Me.ogbmainprocbutton.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbmainprocbutton.Name = "ogbmainprocbutton"
        Me.ogbmainprocbutton.Size = New System.Drawing.Size(1132, 176)
        Me.ogbmainprocbutton.TabIndex = 100000
        Me.ogbmainprocbutton.Tag = "2|"
        '
        'ocmSendApprove
        '
        Me.ocmSendApprove.Location = New System.Drawing.Point(504, 73)
        Me.ocmSendApprove.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmSendApprove.Name = "ocmSendApprove"
        Me.ocmSendApprove.Size = New System.Drawing.Size(124, 31)
        Me.ocmSendApprove.TabIndex = 332
        Me.ocmSendApprove.TabStop = False
        Me.ocmSendApprove.Tag = "2|"
        Me.ocmSendApprove.Text = "SEND APPROVE"
        '
        'ocmdelete
        '
        Me.ocmdelete.Location = New System.Drawing.Point(641, 17)
        Me.ocmdelete.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmdelete.Name = "ocmdelete"
        Me.ocmdelete.Size = New System.Drawing.Size(127, 28)
        Me.ocmdelete.TabIndex = 331
        Me.ocmdelete.Text = "Delete Data"
        '
        'ocmpreview
        '
        Me.ocmpreview.Location = New System.Drawing.Point(511, 14)
        Me.ocmpreview.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmpreview.Name = "ocmpreview"
        Me.ocmpreview.Size = New System.Drawing.Size(111, 31)
        Me.ocmpreview.TabIndex = 330
        Me.ocmpreview.TabStop = False
        Me.ocmpreview.Tag = "2|"
        Me.ocmpreview.Text = "Preview"
        '
        'ocmexit
        '
        Me.ocmexit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmexit.Location = New System.Drawing.Point(1000, 14)
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
        Me.ocmclear.Location = New System.Drawing.Point(246, 14)
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
        Me.ocmload.Location = New System.Drawing.Point(841, 15)
        Me.ocmload.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmload.Name = "ocmload"
        Me.ocmload.Size = New System.Drawing.Size(127, 28)
        Me.ocmload.TabIndex = 329
        Me.ocmload.Text = "Load Data"
        '
        'ocmcalculateincentive
        '
        Me.ocmcalculateincentive.Location = New System.Drawing.Point(42, 17)
        Me.ocmcalculateincentive.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmcalculateincentive.Name = "ocmcalculateincentive"
        Me.ocmcalculateincentive.Size = New System.Drawing.Size(103, 31)
        Me.ocmcalculateincentive.TabIndex = 93
        Me.ocmcalculateincentive.TabStop = False
        Me.ocmcalculateincentive.Tag = "2|"
        Me.ocmcalculateincentive.Text = "Calculate"
        '
        'otxtabctrl
        '
        Me.otxtabctrl.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.otxtabctrl.Location = New System.Drawing.Point(2, 24)
        Me.otxtabctrl.Name = "otxtabctrl"
        Me.otxtabctrl.SelectedTabPage = Me.otpcaltype0
        Me.otxtabctrl.Size = New System.Drawing.Size(1378, 993)
        Me.otxtabctrl.TabIndex = 100001
        Me.otxtabctrl.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.otpcaltype0, Me.otpcut})
        '
        'otpcaltype0
        '
        Me.otpcaltype0.Controls.Add(Me.ogc)
        Me.otpcaltype0.Name = "otpcaltype0"
        Me.otpcaltype0.Size = New System.Drawing.Size(1368, 956)
        Me.otpcaltype0.Text = "รายได้พนักงานเย็บห้องตัวอย่าง"
        '
        'ogc
        '
        Me.ogc.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogc.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogc.Location = New System.Drawing.Point(0, 0)
        Me.ogc.MainView = Me.ogv
        Me.ogc.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogc.Name = "ogc"
        Me.ogc.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryFTSelect, Me.RepositoryFTApproveState, Me.ReposFTStateDaily, Me.RepQuantity})
        Me.ogc.Size = New System.Drawing.Size(1368, 956)
        Me.ogc.TabIndex = 4
        Me.ogc.TabStop = False
        Me.ogc.Tag = "2|"
        Me.ogc.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogv})
        '
        'ogv
        '
        Me.ogv.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.FTSelect, Me.CFTStateFinishDate, Me.CFTSMPOrderNo, Me.CFTTeam, Me.CFTEmpName, Me.CFNQuantity, Me.CFNAmount, Me.CFTStateCal, Me.CFTStateCalDate, Me.CFTStateCalBy, Me.CFTStateFinishDateOrg, Me.CFNHSysEmpID, Me.CFTStateSendApp, Me.CFTSendAppBy, Me.CFTSendAppDate, Me.CFTStateApprove, Me.FTApproveBy, Me.FTApproveDate, Me.FNEmpTeam, Me.CFTStartSewDate, Me.CFTStateManagerApprove, Me.CFTManagerApproveBy, Me.CFTManagerApproveDate})
        Me.ogv.GridControl = Me.ogc
        Me.ogv.Name = "ogv"
        Me.ogv.OptionsCustomization.AllowGroup = False
        Me.ogv.OptionsCustomization.AllowQuickHideColumns = False
        Me.ogv.OptionsView.ColumnAutoWidth = False
        Me.ogv.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.ogv.OptionsView.ShowGroupPanel = False
        Me.ogv.SortInfo.AddRange(New DevExpress.XtraGrid.Columns.GridColumnSortInfo() {New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.CFTSMPOrderNo, DevExpress.Data.ColumnSortOrder.Ascending)})
        Me.ogv.Tag = "2|"
        '
        'FTSelect
        '
        Me.FTSelect.AppearanceHeader.Options.UseTextOptions = True
        Me.FTSelect.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTSelect.Caption = "FTSelect"
        Me.FTSelect.ColumnEdit = Me.RepositoryFTSelect
        Me.FTSelect.FieldName = "FTSelect"
        Me.FTSelect.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
        Me.FTSelect.Name = "FTSelect"
        Me.FTSelect.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTSelect.OptionsColumn.AllowMove = False
        Me.FTSelect.OptionsColumn.AllowShowHide = False
        Me.FTSelect.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTSelect.OptionsColumn.FixedWidth = True
        Me.FTSelect.OptionsColumn.ShowCaption = False
        Me.FTSelect.OptionsColumn.ShowInCustomizationForm = False
        Me.FTSelect.Width = 41
        '
        'RepositoryFTSelect
        '
        Me.RepositoryFTSelect.AutoHeight = False
        Me.RepositoryFTSelect.Caption = "Check"
        Me.RepositoryFTSelect.Name = "RepositoryFTSelect"
        Me.RepositoryFTSelect.ValueChecked = "1"
        Me.RepositoryFTSelect.ValueUnchecked = "0"
        '
        'CFTStateFinishDate
        '
        Me.CFTStateFinishDate.Caption = "วันที่จบงาน"
        Me.CFTStateFinishDate.FieldName = "FTStateFinishDate"
        Me.CFTStateFinishDate.Name = "CFTStateFinishDate"
        Me.CFTStateFinishDate.OptionsColumn.AllowEdit = False
        Me.CFTStateFinishDate.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFTStateFinishDate.OptionsColumn.AllowMove = False
        Me.CFTStateFinishDate.OptionsColumn.AllowShowHide = False
        Me.CFTStateFinishDate.OptionsColumn.ReadOnly = True
        Me.CFTStateFinishDate.OptionsColumn.ShowInCustomizationForm = False
        Me.CFTStateFinishDate.Visible = True
        Me.CFTStateFinishDate.VisibleIndex = 0
        Me.CFTStateFinishDate.Width = 107
        '
        'CFTSMPOrderNo
        '
        Me.CFTSMPOrderNo.Caption = "SMP OrderNo"
        Me.CFTSMPOrderNo.FieldName = "FTSMPOrderNo"
        Me.CFTSMPOrderNo.Name = "CFTSMPOrderNo"
        Me.CFTSMPOrderNo.OptionsColumn.AllowEdit = False
        Me.CFTSMPOrderNo.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFTSMPOrderNo.OptionsColumn.AllowMove = False
        Me.CFTSMPOrderNo.OptionsColumn.AllowShowHide = False
        Me.CFTSMPOrderNo.OptionsColumn.ReadOnly = True
        Me.CFTSMPOrderNo.OptionsColumn.ShowInCustomizationForm = False
        Me.CFTSMPOrderNo.Visible = True
        Me.CFTSMPOrderNo.VisibleIndex = 1
        Me.CFTSMPOrderNo.Width = 109
        '
        'CFTTeam
        '
        Me.CFTTeam.Caption = "Team"
        Me.CFTTeam.FieldName = "FTTeam"
        Me.CFTTeam.Name = "CFTTeam"
        Me.CFTTeam.OptionsColumn.AllowEdit = False
        Me.CFTTeam.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFTTeam.OptionsColumn.AllowMove = False
        Me.CFTTeam.OptionsColumn.AllowShowHide = False
        Me.CFTTeam.OptionsColumn.ReadOnly = True
        Me.CFTTeam.OptionsColumn.ShowInCustomizationForm = False
        Me.CFTTeam.Visible = True
        Me.CFTTeam.VisibleIndex = 2
        Me.CFTTeam.Width = 123
        '
        'CFTEmpName
        '
        Me.CFTEmpName.Caption = "พนักงาน"
        Me.CFTEmpName.FieldName = "FTEmpName"
        Me.CFTEmpName.Name = "CFTEmpName"
        Me.CFTEmpName.OptionsColumn.AllowEdit = False
        Me.CFTEmpName.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFTEmpName.OptionsColumn.AllowMove = False
        Me.CFTEmpName.OptionsColumn.AllowShowHide = False
        Me.CFTEmpName.OptionsColumn.FixedWidth = True
        Me.CFTEmpName.OptionsColumn.ReadOnly = True
        Me.CFTEmpName.OptionsColumn.ShowInCustomizationForm = False
        Me.CFTEmpName.Visible = True
        Me.CFTEmpName.VisibleIndex = 3
        Me.CFTEmpName.Width = 329
        '
        'CFNQuantity
        '
        Me.CFNQuantity.Caption = "จำนวน"
        Me.CFNQuantity.DisplayFormat.FormatString = "{0:n0}"
        Me.CFNQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CFNQuantity.FieldName = "FNQuantity"
        Me.CFNQuantity.Name = "CFNQuantity"
        Me.CFNQuantity.OptionsColumn.AllowEdit = False
        Me.CFNQuantity.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFNQuantity.OptionsColumn.AllowMove = False
        Me.CFNQuantity.OptionsColumn.AllowShowHide = False
        Me.CFNQuantity.OptionsColumn.FixedWidth = True
        Me.CFNQuantity.OptionsColumn.ReadOnly = True
        Me.CFNQuantity.OptionsColumn.ShowInCustomizationForm = False
        Me.CFNQuantity.Visible = True
        Me.CFNQuantity.VisibleIndex = 4
        Me.CFNQuantity.Width = 97
        '
        'CFNAmount
        '
        Me.CFNAmount.Caption = "FNAmount"
        Me.CFNAmount.DisplayFormat.FormatString = "{0:n2}"
        Me.CFNAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CFNAmount.FieldName = "FNSewAmount"
        Me.CFNAmount.Name = "CFNAmount"
        Me.CFNAmount.OptionsColumn.AllowEdit = False
        Me.CFNAmount.OptionsColumn.ReadOnly = True
        Me.CFNAmount.Visible = True
        Me.CFNAmount.VisibleIndex = 5
        Me.CFNAmount.Width = 103
        '
        'CFTStateCal
        '
        Me.CFTStateCal.Caption = "สถานะการคำนวณ"
        Me.CFTStateCal.ColumnEdit = Me.RepositoryFTSelect
        Me.CFTStateCal.FieldName = "FTStateCal"
        Me.CFTStateCal.Name = "CFTStateCal"
        Me.CFTStateCal.OptionsColumn.AllowEdit = False
        Me.CFTStateCal.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFTStateCal.OptionsColumn.AllowMove = False
        Me.CFTStateCal.OptionsColumn.AllowShowHide = False
        Me.CFTStateCal.OptionsColumn.ReadOnly = True
        Me.CFTStateCal.OptionsColumn.ShowInCustomizationForm = False
        Me.CFTStateCal.Visible = True
        Me.CFTStateCal.VisibleIndex = 6
        Me.CFTStateCal.Width = 126
        '
        'CFTStateCalDate
        '
        Me.CFTStateCalDate.Caption = "คำนวณ ณ. วันที่"
        Me.CFTStateCalDate.FieldName = "FTStateCalDate"
        Me.CFTStateCalDate.Name = "CFTStateCalDate"
        Me.CFTStateCalDate.OptionsColumn.AllowEdit = False
        Me.CFTStateCalDate.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFTStateCalDate.OptionsColumn.AllowMove = False
        Me.CFTStateCalDate.OptionsColumn.AllowShowHide = False
        Me.CFTStateCalDate.OptionsColumn.ReadOnly = True
        Me.CFTStateCalDate.OptionsColumn.ShowInCustomizationForm = False
        Me.CFTStateCalDate.Visible = True
        Me.CFTStateCalDate.VisibleIndex = 7
        Me.CFTStateCalDate.Width = 112
        '
        'CFTStateCalBy
        '
        Me.CFTStateCalBy.Caption = "คำนวณโดย"
        Me.CFTStateCalBy.FieldName = "FTStateCalBy"
        Me.CFTStateCalBy.Name = "CFTStateCalBy"
        Me.CFTStateCalBy.OptionsColumn.AllowEdit = False
        Me.CFTStateCalBy.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFTStateCalBy.OptionsColumn.AllowMove = False
        Me.CFTStateCalBy.OptionsColumn.AllowShowHide = False
        Me.CFTStateCalBy.OptionsColumn.ReadOnly = True
        Me.CFTStateCalBy.OptionsColumn.ShowInCustomizationForm = False
        Me.CFTStateCalBy.Visible = True
        Me.CFTStateCalBy.VisibleIndex = 8
        Me.CFTStateCalBy.Width = 165
        '
        'CFTStateFinishDateOrg
        '
        Me.CFTStateFinishDateOrg.Caption = "FTStateFinishDateOrg"
        Me.CFTStateFinishDateOrg.FieldName = "FTStateFinishDateOrg"
        Me.CFTStateFinishDateOrg.Name = "CFTStateFinishDateOrg"
        '
        'CFNHSysEmpID
        '
        Me.CFNHSysEmpID.Caption = "FNHSysEmpID"
        Me.CFNHSysEmpID.FieldName = "FNHSysEmpID"
        Me.CFNHSysEmpID.Name = "CFNHSysEmpID"
        '
        'CFTStateSendApp
        '
        Me.CFTStateSendApp.Caption = "FTStateSendApp"
        Me.CFTStateSendApp.ColumnEdit = Me.RepositoryFTSelect
        Me.CFTStateSendApp.FieldName = "FTStateSendApp"
        Me.CFTStateSendApp.Name = "CFTStateSendApp"
        Me.CFTStateSendApp.OptionsColumn.AllowEdit = False
        Me.CFTStateSendApp.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.CFTStateSendApp.OptionsColumn.AllowMove = False
        Me.CFTStateSendApp.OptionsColumn.AllowShowHide = False
        Me.CFTStateSendApp.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
        Me.CFTStateSendApp.OptionsColumn.ReadOnly = True
        Me.CFTStateSendApp.Visible = True
        Me.CFTStateSendApp.VisibleIndex = 9
        '
        'CFTSendAppBy
        '
        Me.CFTSendAppBy.Caption = "FTSendAppBy"
        Me.CFTSendAppBy.FieldName = "FTSendAppBy"
        Me.CFTSendAppBy.Name = "CFTSendAppBy"
        Me.CFTSendAppBy.OptionsColumn.AllowEdit = False
        Me.CFTSendAppBy.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.CFTSendAppBy.OptionsColumn.AllowMove = False
        Me.CFTSendAppBy.OptionsColumn.AllowShowHide = False
        Me.CFTSendAppBy.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
        Me.CFTSendAppBy.OptionsColumn.ReadOnly = True
        Me.CFTSendAppBy.Visible = True
        Me.CFTSendAppBy.VisibleIndex = 10
        '
        'CFTSendAppDate
        '
        Me.CFTSendAppDate.Caption = "FTSendAppDate"
        Me.CFTSendAppDate.FieldName = "FTSendAppDate"
        Me.CFTSendAppDate.Name = "CFTSendAppDate"
        Me.CFTSendAppDate.OptionsColumn.AllowEdit = False
        Me.CFTSendAppDate.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.CFTSendAppDate.OptionsColumn.AllowMove = False
        Me.CFTSendAppDate.OptionsColumn.AllowShowHide = False
        Me.CFTSendAppDate.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
        Me.CFTSendAppDate.OptionsColumn.ReadOnly = True
        Me.CFTSendAppDate.Visible = True
        Me.CFTSendAppDate.VisibleIndex = 11
        '
        'CFTStateApprove
        '
        Me.CFTStateApprove.Caption = "FTStateApprove"
        Me.CFTStateApprove.ColumnEdit = Me.RepositoryFTSelect
        Me.CFTStateApprove.FieldName = "FTStateApprove"
        Me.CFTStateApprove.Name = "CFTStateApprove"
        Me.CFTStateApprove.OptionsColumn.AllowEdit = False
        Me.CFTStateApprove.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.CFTStateApprove.OptionsColumn.AllowMove = False
        Me.CFTStateApprove.OptionsColumn.AllowShowHide = False
        Me.CFTStateApprove.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
        Me.CFTStateApprove.OptionsColumn.ReadOnly = True
        Me.CFTStateApprove.Visible = True
        Me.CFTStateApprove.VisibleIndex = 12
        '
        'FTApproveBy
        '
        Me.FTApproveBy.Caption = "FTApproveBy"
        Me.FTApproveBy.FieldName = "FTApproveBy"
        Me.FTApproveBy.Name = "FTApproveBy"
        Me.FTApproveBy.OptionsColumn.AllowEdit = False
        Me.FTApproveBy.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.FTApproveBy.OptionsColumn.AllowMove = False
        Me.FTApproveBy.OptionsColumn.AllowShowHide = False
        Me.FTApproveBy.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
        Me.FTApproveBy.OptionsColumn.ReadOnly = True
        Me.FTApproveBy.Visible = True
        Me.FTApproveBy.VisibleIndex = 13
        '
        'FTApproveDate
        '
        Me.FTApproveDate.Caption = "FTApproveDate"
        Me.FTApproveDate.FieldName = "FTApproveDate"
        Me.FTApproveDate.Name = "FTApproveDate"
        Me.FTApproveDate.OptionsColumn.AllowEdit = False
        Me.FTApproveDate.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.FTApproveDate.OptionsColumn.AllowMove = False
        Me.FTApproveDate.OptionsColumn.AllowShowHide = False
        Me.FTApproveDate.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
        Me.FTApproveDate.OptionsColumn.ReadOnly = True
        Me.FTApproveDate.Visible = True
        Me.FTApproveDate.VisibleIndex = 14
        '
        'FNEmpTeam
        '
        Me.FNEmpTeam.Caption = "FNEmpTeam"
        Me.FNEmpTeam.FieldName = "FNEmpTeam"
        Me.FNEmpTeam.Name = "FNEmpTeam"
        '
        'CFTStartSewDate
        '
        Me.CFTStartSewDate.Caption = "FTStartSewDate"
        Me.CFTStartSewDate.FieldName = "FTStartSewDate"
        Me.CFTStartSewDate.Name = "CFTStartSewDate"
        '
        'CFTStateManagerApprove
        '
        Me.CFTStateManagerApprove.Caption = "FTStateManagerApprove"
        Me.CFTStateManagerApprove.ColumnEdit = Me.RepositoryFTSelect
        Me.CFTStateManagerApprove.FieldName = "FTStateManagerApprove"
        Me.CFTStateManagerApprove.Name = "CFTStateManagerApprove"
        Me.CFTStateManagerApprove.OptionsColumn.AllowEdit = False
        Me.CFTStateManagerApprove.OptionsColumn.ReadOnly = True
        Me.CFTStateManagerApprove.Visible = True
        Me.CFTStateManagerApprove.VisibleIndex = 15
        '
        'CFTManagerApproveBy
        '
        Me.CFTManagerApproveBy.Caption = "FTManagerApproveBy"
        Me.CFTManagerApproveBy.FieldName = "FTManagerApproveBy"
        Me.CFTManagerApproveBy.Name = "CFTManagerApproveBy"
        Me.CFTManagerApproveBy.OptionsColumn.AllowEdit = False
        Me.CFTManagerApproveBy.OptionsColumn.ReadOnly = True
        Me.CFTManagerApproveBy.Visible = True
        Me.CFTManagerApproveBy.VisibleIndex = 16
        '
        'CFTManagerApproveDate
        '
        Me.CFTManagerApproveDate.Caption = "FTManagerApproveDate"
        Me.CFTManagerApproveDate.FieldName = "FTManagerApproveDate"
        Me.CFTManagerApproveDate.Name = "CFTManagerApproveDate"
        Me.CFTManagerApproveDate.OptionsColumn.AllowEdit = False
        Me.CFTManagerApproveDate.OptionsColumn.ReadOnly = True
        Me.CFTManagerApproveDate.Visible = True
        Me.CFTManagerApproveDate.VisibleIndex = 17
        '
        'RepositoryFTApproveState
        '
        Me.RepositoryFTApproveState.AutoHeight = False
        Me.RepositoryFTApproveState.Caption = "Check"
        Me.RepositoryFTApproveState.Name = "RepositoryFTApproveState"
        Me.RepositoryFTApproveState.ValueChecked = "1"
        Me.RepositoryFTApproveState.ValueUnchecked = "0"
        '
        'ReposFTStateDaily
        '
        Me.ReposFTStateDaily.AutoHeight = False
        Me.ReposFTStateDaily.Caption = "Check"
        Me.ReposFTStateDaily.Name = "ReposFTStateDaily"
        Me.ReposFTStateDaily.ValueChecked = "1"
        Me.ReposFTStateDaily.ValueUnchecked = "0"
        '
        'RepQuantity
        '
        Me.RepQuantity.AutoHeight = False
        Me.RepQuantity.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepQuantity.DisplayFormat.FormatString = "{0:n0}"
        Me.RepQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.RepQuantity.EditFormat.FormatString = "{0:n0}"
        Me.RepQuantity.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.RepQuantity.Name = "RepQuantity"
        Me.RepQuantity.Precision = 0
        '
        'otpcut
        '
        Me.otpcut.Controls.Add(Me.ogdempcut)
        Me.otpcut.Name = "otpcut"
        Me.otpcut.Size = New System.Drawing.Size(1376, 962)
        Me.otpcut.Text = "คำนวณพนักงานตัดห้องตัวอย่าง"
        '
        'ogdempcut
        '
        Me.ogdempcut.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogdempcut.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogdempcut.Location = New System.Drawing.Point(0, 0)
        Me.ogdempcut.MainView = Me.ogvempcut
        Me.ogdempcut.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogdempcut.Name = "ogdempcut"
        Me.ogdempcut.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCheckEdit1, Me.RepositoryItemCheckEdit2, Me.RepositoryItemCheckEdit3, Me.RepositoryItemCalcEdit1})
        Me.ogdempcut.Size = New System.Drawing.Size(1376, 962)
        Me.ogdempcut.TabIndex = 5
        Me.ogdempcut.TabStop = False
        Me.ogdempcut.Tag = "2|"
        Me.ogdempcut.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvempcut})
        '
        'ogvempcut
        '
        Me.ogvempcut.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.CFTCalculateDate, Me.CFTEmpCode, Me.GridColumn5, Me.FNTotalTimHRFull, Me.FNTotalTimeOTHRFull, Me.FNNetTotalTimHRFull, Me.CFNCutAmount, Me.C2FTStateCalDate, Me.C2FTStateCalBy, Me.CFTCalculateDateOrg, Me.c2FNHSysEmpID, Me.C2FTStateSendApp, Me.C2FTSendAppBy, Me.C2FTSendAppDate, Me.C2FTStateApprove, Me.C2FTApproveBy, Me.C2FTApproveDate, Me.C2FTStateManagerApprove, Me.C2FTManagerApproveBy, Me.C2FTManagerApproveDate})
        Me.ogvempcut.GridControl = Me.ogdempcut
        Me.ogvempcut.Name = "ogvempcut"
        Me.ogvempcut.OptionsCustomization.AllowGroup = False
        Me.ogvempcut.OptionsCustomization.AllowQuickHideColumns = False
        Me.ogvempcut.OptionsView.ColumnAutoWidth = False
        Me.ogvempcut.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.ogvempcut.OptionsView.ShowGroupPanel = False
        Me.ogvempcut.Tag = "2|"
        '
        'CFTCalculateDate
        '
        Me.CFTCalculateDate.Caption = "วันที่จบงาน"
        Me.CFTCalculateDate.FieldName = "FTCalculateDate"
        Me.CFTCalculateDate.Name = "CFTCalculateDate"
        Me.CFTCalculateDate.OptionsColumn.AllowEdit = False
        Me.CFTCalculateDate.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFTCalculateDate.OptionsColumn.AllowMove = False
        Me.CFTCalculateDate.OptionsColumn.AllowShowHide = False
        Me.CFTCalculateDate.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFTCalculateDate.OptionsColumn.ReadOnly = True
        Me.CFTCalculateDate.OptionsColumn.ShowInCustomizationForm = False
        Me.CFTCalculateDate.Visible = True
        Me.CFTCalculateDate.VisibleIndex = 0
        Me.CFTCalculateDate.Width = 107
        '
        'CFTEmpCode
        '
        Me.CFTEmpCode.Caption = "รหัสพนักงาน"
        Me.CFTEmpCode.FieldName = "FTEmpCode"
        Me.CFTEmpCode.Name = "CFTEmpCode"
        Me.CFTEmpCode.OptionsColumn.AllowEdit = False
        Me.CFTEmpCode.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFTEmpCode.OptionsColumn.AllowMove = False
        Me.CFTEmpCode.OptionsColumn.AllowShowHide = False
        Me.CFTEmpCode.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFTEmpCode.OptionsColumn.ReadOnly = True
        Me.CFTEmpCode.OptionsColumn.ShowInCustomizationForm = False
        Me.CFTEmpCode.Visible = True
        Me.CFTEmpCode.VisibleIndex = 1
        Me.CFTEmpCode.Width = 123
        '
        'GridColumn5
        '
        Me.GridColumn5.Caption = "พนักงาน"
        Me.GridColumn5.FieldName = "FTEmpName"
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.OptionsColumn.AllowEdit = False
        Me.GridColumn5.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn5.OptionsColumn.AllowMove = False
        Me.GridColumn5.OptionsColumn.AllowShowHide = False
        Me.GridColumn5.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn5.OptionsColumn.FixedWidth = True
        Me.GridColumn5.OptionsColumn.ReadOnly = True
        Me.GridColumn5.OptionsColumn.ShowInCustomizationForm = False
        Me.GridColumn5.Visible = True
        Me.GridColumn5.VisibleIndex = 2
        Me.GridColumn5.Width = 329
        '
        'FNTotalTimHRFull
        '
        Me.FNTotalTimHRFull.Caption = "FNTotalTimHRFull"
        Me.FNTotalTimHRFull.FieldName = "FNTotalTimHRFull"
        Me.FNTotalTimHRFull.MinWidth = 25
        Me.FNTotalTimHRFull.Name = "FNTotalTimHRFull"
        Me.FNTotalTimHRFull.OptionsColumn.AllowEdit = False
        Me.FNTotalTimHRFull.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNTotalTimHRFull.OptionsColumn.ReadOnly = True
        Me.FNTotalTimHRFull.Visible = True
        Me.FNTotalTimHRFull.VisibleIndex = 3
        Me.FNTotalTimHRFull.Width = 94
        '
        'FNTotalTimeOTHRFull
        '
        Me.FNTotalTimeOTHRFull.Caption = "FNTotalTimeOTHRFull"
        Me.FNTotalTimeOTHRFull.FieldName = "FNTotalTimeOTHRFull"
        Me.FNTotalTimeOTHRFull.MinWidth = 25
        Me.FNTotalTimeOTHRFull.Name = "FNTotalTimeOTHRFull"
        Me.FNTotalTimeOTHRFull.OptionsColumn.AllowEdit = False
        Me.FNTotalTimeOTHRFull.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNTotalTimeOTHRFull.OptionsColumn.ReadOnly = True
        Me.FNTotalTimeOTHRFull.Visible = True
        Me.FNTotalTimeOTHRFull.VisibleIndex = 4
        Me.FNTotalTimeOTHRFull.Width = 94
        '
        'FNNetTotalTimHRFull
        '
        Me.FNNetTotalTimHRFull.Caption = "FNNetTotalTimHRFull"
        Me.FNNetTotalTimHRFull.FieldName = "FNNetTotalTimHRFull"
        Me.FNNetTotalTimHRFull.MinWidth = 25
        Me.FNNetTotalTimHRFull.Name = "FNNetTotalTimHRFull"
        Me.FNNetTotalTimHRFull.OptionsColumn.AllowEdit = False
        Me.FNNetTotalTimHRFull.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNNetTotalTimHRFull.OptionsColumn.ReadOnly = True
        Me.FNNetTotalTimHRFull.Visible = True
        Me.FNNetTotalTimHRFull.VisibleIndex = 5
        Me.FNNetTotalTimHRFull.Width = 94
        '
        'CFNCutAmount
        '
        Me.CFNCutAmount.Caption = "จำนวนเงิน"
        Me.CFNCutAmount.DisplayFormat.FormatString = "{0:n2}"
        Me.CFNCutAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CFNCutAmount.FieldName = "FNIncAmount"
        Me.CFNCutAmount.Name = "CFNCutAmount"
        Me.CFNCutAmount.OptionsColumn.AllowEdit = False
        Me.CFNCutAmount.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFNCutAmount.OptionsColumn.AllowMove = False
        Me.CFNCutAmount.OptionsColumn.AllowShowHide = False
        Me.CFNCutAmount.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFNCutAmount.OptionsColumn.FixedWidth = True
        Me.CFNCutAmount.OptionsColumn.ReadOnly = True
        Me.CFNCutAmount.OptionsColumn.ShowInCustomizationForm = False
        Me.CFNCutAmount.Visible = True
        Me.CFNCutAmount.VisibleIndex = 6
        Me.CFNCutAmount.Width = 97
        '
        'C2FTStateCalDate
        '
        Me.C2FTStateCalDate.Caption = "คำนวณ ณ. วันที่"
        Me.C2FTStateCalDate.FieldName = "FTStateCalDate"
        Me.C2FTStateCalDate.Name = "C2FTStateCalDate"
        Me.C2FTStateCalDate.OptionsColumn.AllowEdit = False
        Me.C2FTStateCalDate.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.C2FTStateCalDate.OptionsColumn.AllowMove = False
        Me.C2FTStateCalDate.OptionsColumn.AllowShowHide = False
        Me.C2FTStateCalDate.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.C2FTStateCalDate.OptionsColumn.ReadOnly = True
        Me.C2FTStateCalDate.OptionsColumn.ShowInCustomizationForm = False
        Me.C2FTStateCalDate.Visible = True
        Me.C2FTStateCalDate.VisibleIndex = 7
        Me.C2FTStateCalDate.Width = 112
        '
        'C2FTStateCalBy
        '
        Me.C2FTStateCalBy.Caption = "คำนวณโดย"
        Me.C2FTStateCalBy.FieldName = "FTStateCalBy"
        Me.C2FTStateCalBy.Name = "C2FTStateCalBy"
        Me.C2FTStateCalBy.OptionsColumn.AllowEdit = False
        Me.C2FTStateCalBy.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.C2FTStateCalBy.OptionsColumn.AllowMove = False
        Me.C2FTStateCalBy.OptionsColumn.AllowShowHide = False
        Me.C2FTStateCalBy.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.C2FTStateCalBy.OptionsColumn.ReadOnly = True
        Me.C2FTStateCalBy.OptionsColumn.ShowInCustomizationForm = False
        Me.C2FTStateCalBy.Visible = True
        Me.C2FTStateCalBy.VisibleIndex = 8
        Me.C2FTStateCalBy.Width = 165
        '
        'CFTCalculateDateOrg
        '
        Me.CFTCalculateDateOrg.Caption = "FTCalculateDateOrg"
        Me.CFTCalculateDateOrg.FieldName = "FTCalculateDateOrg"
        Me.CFTCalculateDateOrg.Name = "CFTCalculateDateOrg"
        Me.CFTCalculateDateOrg.OptionsColumn.AllowEdit = False
        Me.CFTCalculateDateOrg.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFTCalculateDateOrg.OptionsColumn.AllowMove = False
        Me.CFTCalculateDateOrg.OptionsColumn.AllowShowHide = False
        Me.CFTCalculateDateOrg.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFTCalculateDateOrg.OptionsColumn.ReadOnly = True
        Me.CFTCalculateDateOrg.OptionsColumn.ShowInCustomizationForm = False
        '
        'c2FNHSysEmpID
        '
        Me.c2FNHSysEmpID.Caption = "FNHSysEmpID"
        Me.c2FNHSysEmpID.FieldName = "FNHSysEmpID"
        Me.c2FNHSysEmpID.Name = "c2FNHSysEmpID"
        Me.c2FNHSysEmpID.OptionsColumn.AllowEdit = False
        Me.c2FNHSysEmpID.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.c2FNHSysEmpID.OptionsColumn.AllowMove = False
        Me.c2FNHSysEmpID.OptionsColumn.AllowShowHide = False
        Me.c2FNHSysEmpID.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.c2FNHSysEmpID.OptionsColumn.ReadOnly = True
        Me.c2FNHSysEmpID.OptionsColumn.ShowInCustomizationForm = False
        '
        'C2FTStateSendApp
        '
        Me.C2FTStateSendApp.Caption = "FTStateSendApp"
        Me.C2FTStateSendApp.ColumnEdit = Me.RepositoryItemCheckEdit1
        Me.C2FTStateSendApp.FieldName = "FTStateSendApp"
        Me.C2FTStateSendApp.Name = "C2FTStateSendApp"
        Me.C2FTStateSendApp.OptionsColumn.AllowEdit = False
        Me.C2FTStateSendApp.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.C2FTStateSendApp.OptionsColumn.AllowMove = False
        Me.C2FTStateSendApp.OptionsColumn.AllowShowHide = False
        Me.C2FTStateSendApp.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.C2FTStateSendApp.OptionsColumn.ReadOnly = True
        Me.C2FTStateSendApp.OptionsColumn.ShowInCustomizationForm = False
        Me.C2FTStateSendApp.Visible = True
        Me.C2FTStateSendApp.VisibleIndex = 9
        '
        'RepositoryItemCheckEdit1
        '
        Me.RepositoryItemCheckEdit1.AutoHeight = False
        Me.RepositoryItemCheckEdit1.Caption = "Check"
        Me.RepositoryItemCheckEdit1.Name = "RepositoryItemCheckEdit1"
        Me.RepositoryItemCheckEdit1.ValueChecked = "1"
        Me.RepositoryItemCheckEdit1.ValueUnchecked = "0"
        '
        'C2FTSendAppBy
        '
        Me.C2FTSendAppBy.Caption = "FTSendAppBy"
        Me.C2FTSendAppBy.FieldName = "FTSendAppBy"
        Me.C2FTSendAppBy.Name = "C2FTSendAppBy"
        Me.C2FTSendAppBy.OptionsColumn.AllowEdit = False
        Me.C2FTSendAppBy.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.C2FTSendAppBy.OptionsColumn.AllowMove = False
        Me.C2FTSendAppBy.OptionsColumn.AllowShowHide = False
        Me.C2FTSendAppBy.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.C2FTSendAppBy.OptionsColumn.ReadOnly = True
        Me.C2FTSendAppBy.OptionsColumn.ShowInCustomizationForm = False
        Me.C2FTSendAppBy.Visible = True
        Me.C2FTSendAppBy.VisibleIndex = 10
        '
        'C2FTSendAppDate
        '
        Me.C2FTSendAppDate.Caption = "FTSendAppDate"
        Me.C2FTSendAppDate.FieldName = "FTSendAppDate"
        Me.C2FTSendAppDate.Name = "C2FTSendAppDate"
        Me.C2FTSendAppDate.OptionsColumn.AllowEdit = False
        Me.C2FTSendAppDate.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.C2FTSendAppDate.OptionsColumn.AllowMove = False
        Me.C2FTSendAppDate.OptionsColumn.AllowShowHide = False
        Me.C2FTSendAppDate.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.C2FTSendAppDate.OptionsColumn.ReadOnly = True
        Me.C2FTSendAppDate.OptionsColumn.ShowInCustomizationForm = False
        Me.C2FTSendAppDate.Visible = True
        Me.C2FTSendAppDate.VisibleIndex = 11
        '
        'C2FTStateApprove
        '
        Me.C2FTStateApprove.Caption = "FTStateApprove"
        Me.C2FTStateApprove.ColumnEdit = Me.RepositoryItemCheckEdit1
        Me.C2FTStateApprove.FieldName = "FTStateApprove"
        Me.C2FTStateApprove.Name = "C2FTStateApprove"
        Me.C2FTStateApprove.OptionsColumn.AllowEdit = False
        Me.C2FTStateApprove.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.C2FTStateApprove.OptionsColumn.AllowMove = False
        Me.C2FTStateApprove.OptionsColumn.AllowShowHide = False
        Me.C2FTStateApprove.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.C2FTStateApprove.OptionsColumn.ReadOnly = True
        Me.C2FTStateApprove.OptionsColumn.ShowInCustomizationForm = False
        Me.C2FTStateApprove.Visible = True
        Me.C2FTStateApprove.VisibleIndex = 12
        '
        'C2FTApproveBy
        '
        Me.C2FTApproveBy.Caption = "FTApproveBy"
        Me.C2FTApproveBy.FieldName = "FTApproveBy"
        Me.C2FTApproveBy.Name = "C2FTApproveBy"
        Me.C2FTApproveBy.OptionsColumn.AllowEdit = False
        Me.C2FTApproveBy.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.C2FTApproveBy.OptionsColumn.AllowMove = False
        Me.C2FTApproveBy.OptionsColumn.AllowShowHide = False
        Me.C2FTApproveBy.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.C2FTApproveBy.OptionsColumn.ReadOnly = True
        Me.C2FTApproveBy.OptionsColumn.ShowInCustomizationForm = False
        Me.C2FTApproveBy.Visible = True
        Me.C2FTApproveBy.VisibleIndex = 13
        '
        'C2FTApproveDate
        '
        Me.C2FTApproveDate.Caption = "FTApproveDate"
        Me.C2FTApproveDate.FieldName = "FTApproveDate"
        Me.C2FTApproveDate.Name = "C2FTApproveDate"
        Me.C2FTApproveDate.OptionsColumn.AllowEdit = False
        Me.C2FTApproveDate.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.C2FTApproveDate.OptionsColumn.AllowMove = False
        Me.C2FTApproveDate.OptionsColumn.AllowShowHide = False
        Me.C2FTApproveDate.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.C2FTApproveDate.OptionsColumn.ReadOnly = True
        Me.C2FTApproveDate.OptionsColumn.ShowInCustomizationForm = False
        Me.C2FTApproveDate.Visible = True
        Me.C2FTApproveDate.VisibleIndex = 14
        '
        'C2FTStateManagerApprove
        '
        Me.C2FTStateManagerApprove.Caption = "FTStateManagerApprove"
        Me.C2FTStateManagerApprove.ColumnEdit = Me.RepositoryItemCheckEdit1
        Me.C2FTStateManagerApprove.FieldName = "FTStateManagerApprove"
        Me.C2FTStateManagerApprove.Name = "C2FTStateManagerApprove"
        Me.C2FTStateManagerApprove.OptionsColumn.AllowEdit = False
        Me.C2FTStateManagerApprove.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.C2FTStateManagerApprove.OptionsColumn.AllowMove = False
        Me.C2FTStateManagerApprove.OptionsColumn.AllowShowHide = False
        Me.C2FTStateManagerApprove.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.C2FTStateManagerApprove.OptionsColumn.ReadOnly = True
        Me.C2FTStateManagerApprove.OptionsColumn.ShowInCustomizationForm = False
        Me.C2FTStateManagerApprove.Visible = True
        Me.C2FTStateManagerApprove.VisibleIndex = 15
        '
        'C2FTManagerApproveBy
        '
        Me.C2FTManagerApproveBy.FieldName = "FTManagerApproveBy"
        Me.C2FTManagerApproveBy.Name = "C2FTManagerApproveBy"
        Me.C2FTManagerApproveBy.OptionsColumn.AllowEdit = False
        Me.C2FTManagerApproveBy.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.C2FTManagerApproveBy.OptionsColumn.AllowMove = False
        Me.C2FTManagerApproveBy.OptionsColumn.AllowShowHide = False
        Me.C2FTManagerApproveBy.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.C2FTManagerApproveBy.OptionsColumn.ReadOnly = True
        Me.C2FTManagerApproveBy.OptionsColumn.ShowInCustomizationForm = False
        Me.C2FTManagerApproveBy.Visible = True
        Me.C2FTManagerApproveBy.VisibleIndex = 16
        '
        'C2FTManagerApproveDate
        '
        Me.C2FTManagerApproveDate.Caption = "FTManagerApproveDate"
        Me.C2FTManagerApproveDate.FieldName = "FTManagerApproveDate"
        Me.C2FTManagerApproveDate.Name = "C2FTManagerApproveDate"
        Me.C2FTManagerApproveDate.OptionsColumn.AllowEdit = False
        Me.C2FTManagerApproveDate.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.C2FTManagerApproveDate.OptionsColumn.AllowMove = False
        Me.C2FTManagerApproveDate.OptionsColumn.AllowShowHide = False
        Me.C2FTManagerApproveDate.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.C2FTManagerApproveDate.OptionsColumn.ReadOnly = True
        Me.C2FTManagerApproveDate.OptionsColumn.ShowInCustomizationForm = False
        Me.C2FTManagerApproveDate.Visible = True
        Me.C2FTManagerApproveDate.VisibleIndex = 17
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
        'RepositoryItemCalcEdit1
        '
        Me.RepositoryItemCalcEdit1.AutoHeight = False
        Me.RepositoryItemCalcEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemCalcEdit1.DisplayFormat.FormatString = "{0:n0}"
        Me.RepositoryItemCalcEdit1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.RepositoryItemCalcEdit1.EditFormat.FormatString = "{0:n0}"
        Me.RepositoryItemCalcEdit1.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.RepositoryItemCalcEdit1.Name = "RepositoryItemCalcEdit1"
        Me.RepositoryItemCalcEdit1.Precision = 0
        '
        'ochkselectall
        '
        Me.ochkselectall.Location = New System.Drawing.Point(30, 2)
        Me.ochkselectall.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ochkselectall.Name = "ochkselectall"
        Me.ochkselectall.Properties.Caption = "Select All"
        Me.ochkselectall.Size = New System.Drawing.Size(222, 20)
        Me.ochkselectall.TabIndex = 308
        Me.ochkselectall.Visible = False
        '
        'ogbreqtime
        '
        Me.ogbreqtime.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbreqtime.Controls.Add(Me.FTEndDate)
        Me.ogbreqtime.Controls.Add(Me.FTEndDate_lbl)
        Me.ogbreqtime.Controls.Add(Me.FTStartDate)
        Me.ogbreqtime.Controls.Add(Me.FTStartDate_lbl)
        Me.ogbreqtime.Location = New System.Drawing.Point(5, 6)
        Me.ogbreqtime.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbreqtime.Name = "ogbreqtime"
        Me.ogbreqtime.Size = New System.Drawing.Size(1382, 76)
        Me.ogbreqtime.TabIndex = 5
        Me.ogbreqtime.Text = "Calculate Date"
        '
        'FTEndDate
        '
        Me.FTEndDate.EditValue = Nothing
        Me.FTEndDate.EnterMoveNextControl = True
        Me.FTEndDate.Location = New System.Drawing.Point(466, 35)
        Me.FTEndDate.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTEndDate.Name = "FTEndDate"
        Me.FTEndDate.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTEndDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FTEndDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.FTEndDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTEndDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTEndDate.Properties.NullDate = ""
        Me.FTEndDate.Size = New System.Drawing.Size(132, 23)
        Me.FTEndDate.TabIndex = 285
        Me.FTEndDate.Tag = "2|"
        '
        'FTEndDate_lbl
        '
        Me.FTEndDate_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTEndDate_lbl.Appearance.Options.UseForeColor = True
        Me.FTEndDate_lbl.Appearance.Options.UseTextOptions = True
        Me.FTEndDate_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTEndDate_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTEndDate_lbl.Location = New System.Drawing.Point(343, 35)
        Me.FTEndDate_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTEndDate_lbl.Name = "FTEndDate_lbl"
        Me.FTEndDate_lbl.Size = New System.Drawing.Size(122, 23)
        Me.FTEndDate_lbl.TabIndex = 286
        Me.FTEndDate_lbl.Tag = "2|"
        Me.FTEndDate_lbl.Text = "Date :"
        '
        'FTStartDate
        '
        Me.FTStartDate.EditValue = Nothing
        Me.FTStartDate.EnterMoveNextControl = True
        Me.FTStartDate.Location = New System.Drawing.Point(141, 32)
        Me.FTStartDate.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTStartDate.Name = "FTStartDate"
        Me.FTStartDate.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTStartDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FTStartDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.FTStartDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTStartDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTStartDate.Properties.NullDate = ""
        Me.FTStartDate.Size = New System.Drawing.Size(132, 23)
        Me.FTStartDate.TabIndex = 283
        Me.FTStartDate.Tag = "2|"
        '
        'FTStartDate_lbl
        '
        Me.FTStartDate_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTStartDate_lbl.Appearance.Options.UseForeColor = True
        Me.FTStartDate_lbl.Appearance.Options.UseTextOptions = True
        Me.FTStartDate_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTStartDate_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTStartDate_lbl.Location = New System.Drawing.Point(18, 32)
        Me.FTStartDate_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTStartDate_lbl.Name = "FTStartDate_lbl"
        Me.FTStartDate_lbl.Size = New System.Drawing.Size(122, 23)
        Me.FTStartDate_lbl.TabIndex = 284
        Me.FTStartDate_lbl.Tag = "2|"
        Me.FTStartDate_lbl.Text = "Date :"
        '
        'oDockManager
        '
        Me.oDockManager.Form = Me
        Me.oDockManager.TopZIndexControls.AddRange(New String() {"DevExpress.XtraBars.BarDockControl", "DevExpress.XtraBars.StandaloneBarDockControl", "System.Windows.Forms.StatusBar", "DevExpress.XtraBars.Ribbon.RibbonStatusBar", "DevExpress.XtraBars.Ribbon.RibbonControl"})
        '
        'ogbdetail
        '
        Me.ogbdetail.Controls.Add(Me.ogbreqtime)
        Me.ogbdetail.Controls.Add(Me.ogbemployee)
        Me.ogbdetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogbdetail.Location = New System.Drawing.Point(0, 0)
        Me.ogbdetail.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbdetail.Name = "ogbdetail"
        Me.ogbdetail.ShowCaption = False
        Me.ogbdetail.Size = New System.Drawing.Size(1392, 1116)
        Me.ogbdetail.TabIndex = 100001
        Me.ogbdetail.Text = "GroupControl1"
        '
        'wSMPCalculateCut
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1392, 1116)
        Me.Controls.Add(Me.ogbdetail)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "wSMPCalculateCut"
        Me.Text = "SMP Calculate Incentive Emp Cut"
        CType(Me.ogbemployee, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbemployee.ResumeLayout(False)
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbmainprocbutton.ResumeLayout(False)
        CType(Me.otxtabctrl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.otxtabctrl.ResumeLayout(False)
        Me.otpcaltype0.ResumeLayout(False)
        CType(Me.ogc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryFTSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryFTApproveState, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ReposFTStateDaily, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepQuantity, System.ComponentModel.ISupportInitialize).EndInit()
        Me.otpcut.ResumeLayout(False)
        CType(Me.ogdempcut, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvempcut, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCalcEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ochkselectall.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogbreqtime, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbreqtime.ResumeLayout(False)
        CType(Me.FTEndDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTEndDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTStartDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTStartDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.oDockManager, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogbdetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbdetail.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogbemployee As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ogbreqtime As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ochkselectall As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents oDockManager As DevExpress.XtraBars.Docking.DockManager
    Friend WithEvents ogbmainprocbutton As DevExpress.XtraEditors.PanelControl
    Friend WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmclear As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmload As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmcalculateincentive As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ogbdetail As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ocmpreview As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmdelete As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents otxtabctrl As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents otpcaltype0 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents ogc As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogv As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents FTSelect As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryFTSelect As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents RepQuantity As DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit
    Friend WithEvents RepositoryFTApproveState As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents ReposFTStateDaily As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents CFTStateFinishDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFTSMPOrderNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFTTeam As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFTEmpName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFNQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFTStateCal As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFTStateCalDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFTStateCalBy As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFTStateFinishDateOrg As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFNHSysEmpID As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFTStateSendApp As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFTSendAppBy As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFTSendAppDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFTStateApprove As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTApproveBy As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTApproveDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ocmSendApprove As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FNEmpTeam As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFTStartSewDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTEndDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents FTEndDate_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTStartDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents FTStartDate_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents CFNAmount As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents otpcut As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents ogdempcut As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvempcut As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents CFTCalculateDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFTEmpCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFNCutAmount As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents C2FTStateCalDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents C2FTStateCalBy As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFTCalculateDateOrg As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents c2FNHSysEmpID As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents C2FTStateSendApp As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemCheckEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents C2FTSendAppBy As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents C2FTSendAppDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents C2FTStateApprove As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents C2FTApproveBy As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents C2FTApproveDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemCheckEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents RepositoryItemCheckEdit3 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents RepositoryItemCalcEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit
    Friend WithEvents CFTStateManagerApprove As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFTManagerApproveBy As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFTManagerApproveDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents C2FTStateManagerApprove As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents C2FTManagerApproveBy As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents C2FTManagerApproveDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNTotalTimHRFull As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNTotalTimeOTHRFull As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNNetTotalTimHRFull As DevExpress.XtraGrid.Columns.GridColumn
End Class

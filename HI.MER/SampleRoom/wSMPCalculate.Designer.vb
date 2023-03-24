<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wSMPCalculate
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
        Me.FTStartDate_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTStartDate = New DevExpress.XtraEditors.DateEdit()
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
        Me.CCFTStartSewDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFTFinishSewDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFTSMPOrderNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFTTeam = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFTEmpName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFNQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
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
        Me.RepositoryFTApproveState = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.ReposFTStateDaily = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.RepQuantity = New DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit()
        Me.ochkselectall = New DevExpress.XtraEditors.CheckEdit()
        Me.ogbreqtime = New DevExpress.XtraEditors.GroupControl()
        Me.FTEndDate = New DevExpress.XtraEditors.DateEdit()
        Me.FTEndDate_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.oDockManager = New DevExpress.XtraBars.Docking.DockManager(Me.components)
        Me.ogbdetail = New DevExpress.XtraEditors.GroupControl()
        CType(Me.FTStartDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTStartDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
        CType(Me.ochkselectall.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogbreqtime, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbreqtime.SuspendLayout()
        CType(Me.FTEndDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTEndDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.oDockManager, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogbdetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbdetail.SuspendLayout()
        Me.SuspendLayout()
        '
        'FTStartDate_lbl
        '
        Me.FTStartDate_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTStartDate_lbl.Appearance.Options.UseForeColor = True
        Me.FTStartDate_lbl.Appearance.Options.UseTextOptions = True
        Me.FTStartDate_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTStartDate_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTStartDate_lbl.Location = New System.Drawing.Point(14, 28)
        Me.FTStartDate_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTStartDate_lbl.Name = "FTStartDate_lbl"
        Me.FTStartDate_lbl.Size = New System.Drawing.Size(122, 23)
        Me.FTStartDate_lbl.TabIndex = 280
        Me.FTStartDate_lbl.Tag = "2|"
        Me.FTStartDate_lbl.Text = "Date :"
        '
        'FTStartDate
        '
        Me.FTStartDate.EditValue = Nothing
        Me.FTStartDate.EnterMoveNextControl = True
        Me.FTStartDate.Location = New System.Drawing.Point(136, 28)
        Me.FTStartDate.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTStartDate.Name = "FTStartDate"
        Me.FTStartDate.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTStartDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FTStartDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.FTStartDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTStartDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTStartDate.Properties.NullDate = ""
        Me.FTStartDate.Size = New System.Drawing.Size(132, 23)
        Me.FTStartDate.TabIndex = 0
        Me.FTStartDate.Tag = "2|"
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
        Me.ogbemployee.Location = New System.Drawing.Point(5, 84)
        Me.ogbemployee.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbemployee.Name = "ogbemployee"
        Me.ogbemployee.Size = New System.Drawing.Size(1382, 925)
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
        Me.ocmdelete.Location = New System.Drawing.Point(640, 17)
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
        Me.ocmcalculateincentive.Size = New System.Drawing.Size(111, 31)
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
        Me.otxtabctrl.Location = New System.Drawing.Point(2, 25)
        Me.otxtabctrl.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.otxtabctrl.Name = "otxtabctrl"
        Me.otxtabctrl.SelectedTabPage = Me.otpcaltype0
        Me.otxtabctrl.Size = New System.Drawing.Size(1378, 899)
        Me.otxtabctrl.TabIndex = 100001
        Me.otxtabctrl.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.otpcaltype0})
        '
        'otpcaltype0
        '
        Me.otpcaltype0.Controls.Add(Me.ogc)
        Me.otpcaltype0.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.otpcaltype0.Name = "otpcaltype0"
        Me.otpcaltype0.Size = New System.Drawing.Size(1368, 862)
        Me.otpcaltype0.Text = "คำนวณพนักงานเย็บห้องตัวอย่าง"
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
        Me.ogc.Size = New System.Drawing.Size(1368, 862)
        Me.ogc.TabIndex = 4
        Me.ogc.TabStop = False
        Me.ogc.Tag = "2|"
        Me.ogc.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogv})
        '
        'ogv
        '
        Me.ogv.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.FTSelect, Me.CFTStateFinishDate, Me.CCFTStartSewDate, Me.CFTFinishSewDate, Me.CFTSMPOrderNo, Me.CFTTeam, Me.CFTEmpName, Me.CFNQuantity, Me.CFTStateCal, Me.CFTStateCalDate, Me.CFTStateCalBy, Me.CFTStateFinishDateOrg, Me.CFNHSysEmpID, Me.CFTStateSendApp, Me.CFTSendAppBy, Me.CFTSendAppDate, Me.CFTStateApprove, Me.FTApproveBy, Me.FTApproveDate, Me.FNEmpTeam, Me.CFTStartSewDate})
        Me.ogv.DetailHeight = 431
        Me.ogv.GridControl = Me.ogc
        Me.ogv.Name = "ogv"
        Me.ogv.OptionsCustomization.AllowGroup = False
        Me.ogv.OptionsCustomization.AllowQuickHideColumns = False
        Me.ogv.OptionsView.ColumnAutoWidth = False
        Me.ogv.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.ogv.OptionsView.ShowGroupPanel = False
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
        Me.FTSelect.MinWidth = 23
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
        Me.FTSelect.Width = 48
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
        Me.CFTStateFinishDate.MinWidth = 23
        Me.CFTStateFinishDate.Name = "CFTStateFinishDate"
        Me.CFTStateFinishDate.OptionsColumn.AllowEdit = False
        Me.CFTStateFinishDate.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFTStateFinishDate.OptionsColumn.AllowMove = False
        Me.CFTStateFinishDate.OptionsColumn.AllowShowHide = False
        Me.CFTStateFinishDate.OptionsColumn.ReadOnly = True
        Me.CFTStateFinishDate.OptionsColumn.ShowInCustomizationForm = False
        Me.CFTStateFinishDate.Visible = True
        Me.CFTStateFinishDate.VisibleIndex = 1
        Me.CFTStateFinishDate.Width = 125
        '
        'CCFTStartSewDate
        '
        Me.CCFTStartSewDate.Caption = "วันที่เริ่มเย็บ"
        Me.CCFTStartSewDate.FieldName = "FTStartSewDate"
        Me.CCFTStartSewDate.MinWidth = 23
        Me.CCFTStartSewDate.Name = "CCFTStartSewDate"
        Me.CCFTStartSewDate.OptionsColumn.AllowEdit = False
        Me.CCFTStartSewDate.OptionsColumn.ReadOnly = True
        Me.CCFTStartSewDate.Visible = True
        Me.CCFTStartSewDate.VisibleIndex = 2
        Me.CCFTStartSewDate.Width = 87
        '
        'CFTFinishSewDate
        '
        Me.CFTFinishSewDate.Caption = "วันที่เย็บเสร็จ"
        Me.CFTFinishSewDate.FieldName = "FTFinishSewDateShow"
        Me.CFTFinishSewDate.MinWidth = 23
        Me.CFTFinishSewDate.Name = "CFTFinishSewDate"
        Me.CFTFinishSewDate.OptionsColumn.AllowEdit = False
        Me.CFTFinishSewDate.OptionsColumn.ReadOnly = True
        Me.CFTFinishSewDate.Visible = True
        Me.CFTFinishSewDate.VisibleIndex = 3
        Me.CFTFinishSewDate.Width = 87
        '
        'CFTSMPOrderNo
        '
        Me.CFTSMPOrderNo.Caption = "SMP OrderNo"
        Me.CFTSMPOrderNo.FieldName = "FTSMPOrderNo"
        Me.CFTSMPOrderNo.MinWidth = 23
        Me.CFTSMPOrderNo.Name = "CFTSMPOrderNo"
        Me.CFTSMPOrderNo.OptionsColumn.AllowEdit = False
        Me.CFTSMPOrderNo.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFTSMPOrderNo.OptionsColumn.AllowMove = False
        Me.CFTSMPOrderNo.OptionsColumn.AllowShowHide = False
        Me.CFTSMPOrderNo.OptionsColumn.ReadOnly = True
        Me.CFTSMPOrderNo.OptionsColumn.ShowInCustomizationForm = False
        Me.CFTSMPOrderNo.Visible = True
        Me.CFTSMPOrderNo.VisibleIndex = 4
        Me.CFTSMPOrderNo.Width = 127
        '
        'CFTTeam
        '
        Me.CFTTeam.Caption = "Team"
        Me.CFTTeam.FieldName = "FTTeam"
        Me.CFTTeam.MinWidth = 23
        Me.CFTTeam.Name = "CFTTeam"
        Me.CFTTeam.OptionsColumn.AllowEdit = False
        Me.CFTTeam.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFTTeam.OptionsColumn.AllowMove = False
        Me.CFTTeam.OptionsColumn.AllowShowHide = False
        Me.CFTTeam.OptionsColumn.ReadOnly = True
        Me.CFTTeam.OptionsColumn.ShowInCustomizationForm = False
        Me.CFTTeam.Visible = True
        Me.CFTTeam.VisibleIndex = 5
        Me.CFTTeam.Width = 143
        '
        'CFTEmpName
        '
        Me.CFTEmpName.Caption = "พนักงาน"
        Me.CFTEmpName.FieldName = "FTEmpName"
        Me.CFTEmpName.MinWidth = 23
        Me.CFTEmpName.Name = "CFTEmpName"
        Me.CFTEmpName.OptionsColumn.AllowEdit = False
        Me.CFTEmpName.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFTEmpName.OptionsColumn.AllowMove = False
        Me.CFTEmpName.OptionsColumn.AllowShowHide = False
        Me.CFTEmpName.OptionsColumn.FixedWidth = True
        Me.CFTEmpName.OptionsColumn.ReadOnly = True
        Me.CFTEmpName.OptionsColumn.ShowInCustomizationForm = False
        Me.CFTEmpName.Visible = True
        Me.CFTEmpName.VisibleIndex = 6
        Me.CFTEmpName.Width = 384
        '
        'CFNQuantity
        '
        Me.CFNQuantity.Caption = "จำนวน"
        Me.CFNQuantity.DisplayFormat.FormatString = "{0:n0}"
        Me.CFNQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CFNQuantity.FieldName = "FNQuantity"
        Me.CFNQuantity.MinWidth = 23
        Me.CFNQuantity.Name = "CFNQuantity"
        Me.CFNQuantity.OptionsColumn.AllowEdit = False
        Me.CFNQuantity.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFNQuantity.OptionsColumn.AllowMove = False
        Me.CFNQuantity.OptionsColumn.AllowShowHide = False
        Me.CFNQuantity.OptionsColumn.FixedWidth = True
        Me.CFNQuantity.OptionsColumn.ReadOnly = True
        Me.CFNQuantity.OptionsColumn.ShowInCustomizationForm = False
        Me.CFNQuantity.Visible = True
        Me.CFNQuantity.VisibleIndex = 7
        Me.CFNQuantity.Width = 113
        '
        'CFTStateCal
        '
        Me.CFTStateCal.Caption = "สถานะการคำนวณ"
        Me.CFTStateCal.ColumnEdit = Me.RepositoryFTSelect
        Me.CFTStateCal.FieldName = "FTStateCal"
        Me.CFTStateCal.MinWidth = 23
        Me.CFTStateCal.Name = "CFTStateCal"
        Me.CFTStateCal.OptionsColumn.AllowEdit = False
        Me.CFTStateCal.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFTStateCal.OptionsColumn.AllowMove = False
        Me.CFTStateCal.OptionsColumn.AllowShowHide = False
        Me.CFTStateCal.OptionsColumn.ReadOnly = True
        Me.CFTStateCal.OptionsColumn.ShowInCustomizationForm = False
        Me.CFTStateCal.Visible = True
        Me.CFTStateCal.VisibleIndex = 8
        Me.CFTStateCal.Width = 147
        '
        'CFTStateCalDate
        '
        Me.CFTStateCalDate.Caption = "คำนวณ ณ. วันที่"
        Me.CFTStateCalDate.FieldName = "FTStateCalDate"
        Me.CFTStateCalDate.MinWidth = 23
        Me.CFTStateCalDate.Name = "CFTStateCalDate"
        Me.CFTStateCalDate.OptionsColumn.AllowEdit = False
        Me.CFTStateCalDate.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFTStateCalDate.OptionsColumn.AllowMove = False
        Me.CFTStateCalDate.OptionsColumn.AllowShowHide = False
        Me.CFTStateCalDate.OptionsColumn.ReadOnly = True
        Me.CFTStateCalDate.OptionsColumn.ShowInCustomizationForm = False
        Me.CFTStateCalDate.Visible = True
        Me.CFTStateCalDate.VisibleIndex = 9
        Me.CFTStateCalDate.Width = 131
        '
        'CFTStateCalBy
        '
        Me.CFTStateCalBy.Caption = "คำนวณโดย"
        Me.CFTStateCalBy.FieldName = "FTStateCalBy"
        Me.CFTStateCalBy.MinWidth = 23
        Me.CFTStateCalBy.Name = "CFTStateCalBy"
        Me.CFTStateCalBy.OptionsColumn.AllowEdit = False
        Me.CFTStateCalBy.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFTStateCalBy.OptionsColumn.AllowMove = False
        Me.CFTStateCalBy.OptionsColumn.AllowShowHide = False
        Me.CFTStateCalBy.OptionsColumn.ReadOnly = True
        Me.CFTStateCalBy.OptionsColumn.ShowInCustomizationForm = False
        Me.CFTStateCalBy.Visible = True
        Me.CFTStateCalBy.VisibleIndex = 10
        Me.CFTStateCalBy.Width = 192
        '
        'CFTStateFinishDateOrg
        '
        Me.CFTStateFinishDateOrg.Caption = "FTStateFinishDateOrg"
        Me.CFTStateFinishDateOrg.FieldName = "FTStateFinishDateOrg"
        Me.CFTStateFinishDateOrg.MinWidth = 23
        Me.CFTStateFinishDateOrg.Name = "CFTStateFinishDateOrg"
        Me.CFTStateFinishDateOrg.Width = 87
        '
        'CFNHSysEmpID
        '
        Me.CFNHSysEmpID.Caption = "FNHSysEmpID"
        Me.CFNHSysEmpID.FieldName = "FNHSysEmpID"
        Me.CFNHSysEmpID.MinWidth = 23
        Me.CFNHSysEmpID.Name = "CFNHSysEmpID"
        Me.CFNHSysEmpID.Width = 87
        '
        'CFTStateSendApp
        '
        Me.CFTStateSendApp.Caption = "FTStateSendApp"
        Me.CFTStateSendApp.ColumnEdit = Me.RepositoryFTSelect
        Me.CFTStateSendApp.FieldName = "FTStateSendApp"
        Me.CFTStateSendApp.MinWidth = 23
        Me.CFTStateSendApp.Name = "CFTStateSendApp"
        Me.CFTStateSendApp.OptionsColumn.AllowEdit = False
        Me.CFTStateSendApp.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.CFTStateSendApp.OptionsColumn.AllowMove = False
        Me.CFTStateSendApp.OptionsColumn.AllowShowHide = False
        Me.CFTStateSendApp.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
        Me.CFTStateSendApp.OptionsColumn.ReadOnly = True
        Me.CFTStateSendApp.Visible = True
        Me.CFTStateSendApp.VisibleIndex = 11
        Me.CFTStateSendApp.Width = 87
        '
        'CFTSendAppBy
        '
        Me.CFTSendAppBy.Caption = "FTSendAppBy"
        Me.CFTSendAppBy.FieldName = "FTSendAppBy"
        Me.CFTSendAppBy.MinWidth = 23
        Me.CFTSendAppBy.Name = "CFTSendAppBy"
        Me.CFTSendAppBy.OptionsColumn.AllowEdit = False
        Me.CFTSendAppBy.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.CFTSendAppBy.OptionsColumn.AllowMove = False
        Me.CFTSendAppBy.OptionsColumn.AllowShowHide = False
        Me.CFTSendAppBy.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
        Me.CFTSendAppBy.OptionsColumn.ReadOnly = True
        Me.CFTSendAppBy.Visible = True
        Me.CFTSendAppBy.VisibleIndex = 12
        Me.CFTSendAppBy.Width = 87
        '
        'CFTSendAppDate
        '
        Me.CFTSendAppDate.Caption = "FTSendAppDate"
        Me.CFTSendAppDate.FieldName = "FTSendAppDate"
        Me.CFTSendAppDate.MinWidth = 23
        Me.CFTSendAppDate.Name = "CFTSendAppDate"
        Me.CFTSendAppDate.OptionsColumn.AllowEdit = False
        Me.CFTSendAppDate.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.CFTSendAppDate.OptionsColumn.AllowMove = False
        Me.CFTSendAppDate.OptionsColumn.AllowShowHide = False
        Me.CFTSendAppDate.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
        Me.CFTSendAppDate.OptionsColumn.ReadOnly = True
        Me.CFTSendAppDate.Visible = True
        Me.CFTSendAppDate.VisibleIndex = 13
        Me.CFTSendAppDate.Width = 87
        '
        'CFTStateApprove
        '
        Me.CFTStateApprove.Caption = "FTStateApprove"
        Me.CFTStateApprove.ColumnEdit = Me.RepositoryFTSelect
        Me.CFTStateApprove.FieldName = "FTStateApprove"
        Me.CFTStateApprove.MinWidth = 23
        Me.CFTStateApprove.Name = "CFTStateApprove"
        Me.CFTStateApprove.OptionsColumn.AllowEdit = False
        Me.CFTStateApprove.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.CFTStateApprove.OptionsColumn.AllowMove = False
        Me.CFTStateApprove.OptionsColumn.AllowShowHide = False
        Me.CFTStateApprove.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
        Me.CFTStateApprove.OptionsColumn.ReadOnly = True
        Me.CFTStateApprove.Visible = True
        Me.CFTStateApprove.VisibleIndex = 14
        Me.CFTStateApprove.Width = 87
        '
        'FTApproveBy
        '
        Me.FTApproveBy.Caption = "FTApproveBy"
        Me.FTApproveBy.FieldName = "FTApproveBy"
        Me.FTApproveBy.MinWidth = 23
        Me.FTApproveBy.Name = "FTApproveBy"
        Me.FTApproveBy.OptionsColumn.AllowEdit = False
        Me.FTApproveBy.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.FTApproveBy.OptionsColumn.AllowMove = False
        Me.FTApproveBy.OptionsColumn.AllowShowHide = False
        Me.FTApproveBy.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
        Me.FTApproveBy.OptionsColumn.ReadOnly = True
        Me.FTApproveBy.Visible = True
        Me.FTApproveBy.VisibleIndex = 15
        Me.FTApproveBy.Width = 87
        '
        'FTApproveDate
        '
        Me.FTApproveDate.Caption = "FTApproveDate"
        Me.FTApproveDate.FieldName = "FTApproveDate"
        Me.FTApproveDate.MinWidth = 23
        Me.FTApproveDate.Name = "FTApproveDate"
        Me.FTApproveDate.OptionsColumn.AllowEdit = False
        Me.FTApproveDate.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.FTApproveDate.OptionsColumn.AllowMove = False
        Me.FTApproveDate.OptionsColumn.AllowShowHide = False
        Me.FTApproveDate.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
        Me.FTApproveDate.OptionsColumn.ReadOnly = True
        Me.FTApproveDate.Visible = True
        Me.FTApproveDate.VisibleIndex = 16
        Me.FTApproveDate.Width = 87
        '
        'FNEmpTeam
        '
        Me.FNEmpTeam.Caption = "FNEmpTeam"
        Me.FNEmpTeam.FieldName = "FNEmpTeam"
        Me.FNEmpTeam.MinWidth = 23
        Me.FNEmpTeam.Name = "FNEmpTeam"
        Me.FNEmpTeam.Width = 87
        '
        'CFTStartSewDate
        '
        Me.CFTStartSewDate.Caption = "FTStartSewDate"
        Me.CFTStartSewDate.FieldName = "FTStartSewDate"
        Me.CFTStartSewDate.MinWidth = 23
        Me.CFTStartSewDate.Name = "CFTStartSewDate"
        Me.CFTStartSewDate.Width = 87
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
        'ochkselectall
        '
        Me.ochkselectall.Location = New System.Drawing.Point(30, 2)
        Me.ochkselectall.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ochkselectall.Name = "ochkselectall"
        Me.ochkselectall.Properties.Caption = "Select All"
        Me.ochkselectall.Size = New System.Drawing.Size(222, 20)
        Me.ochkselectall.TabIndex = 308
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
        Me.ogbreqtime.Size = New System.Drawing.Size(1382, 71)
        Me.ogbreqtime.TabIndex = 5
        Me.ogbreqtime.Text = "Calculate Date"
        '
        'FTEndDate
        '
        Me.FTEndDate.EditValue = Nothing
        Me.FTEndDate.EnterMoveNextControl = True
        Me.FTEndDate.Location = New System.Drawing.Point(462, 31)
        Me.FTEndDate.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTEndDate.Name = "FTEndDate"
        Me.FTEndDate.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTEndDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FTEndDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.FTEndDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTEndDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTEndDate.Properties.NullDate = ""
        Me.FTEndDate.Size = New System.Drawing.Size(132, 23)
        Me.FTEndDate.TabIndex = 281
        Me.FTEndDate.Tag = "2|"
        '
        'FTEndDate_lbl
        '
        Me.FTEndDate_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTEndDate_lbl.Appearance.Options.UseForeColor = True
        Me.FTEndDate_lbl.Appearance.Options.UseTextOptions = True
        Me.FTEndDate_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTEndDate_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTEndDate_lbl.Location = New System.Drawing.Point(339, 31)
        Me.FTEndDate_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTEndDate_lbl.Name = "FTEndDate_lbl"
        Me.FTEndDate_lbl.Size = New System.Drawing.Size(122, 23)
        Me.FTEndDate_lbl.TabIndex = 282
        Me.FTEndDate_lbl.Tag = "2|"
        Me.FTEndDate_lbl.Text = "Date :"
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
        Me.ogbdetail.Size = New System.Drawing.Size(1392, 1016)
        Me.ogbdetail.TabIndex = 100001
        Me.ogbdetail.Text = "GroupControl1"
        '
        'wSMPCalculate
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1392, 1016)
        Me.Controls.Add(Me.ogbdetail)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "wSMPCalculate"
        Me.Text = "SMP Calculate Incentive"
        CType(Me.FTStartDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTStartDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
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
        CType(Me.ochkselectall.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogbreqtime, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbreqtime.ResumeLayout(False)
        CType(Me.FTEndDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTEndDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.oDockManager, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogbdetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbdetail.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents FTStartDate_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTStartDate As DevExpress.XtraEditors.DateEdit
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
    Friend WithEvents FTEndDate_lbl As DevExpress.XtraEditors.LabelControl
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
    Friend WithEvents FTEndDate As DevExpress.XtraEditors.DateEdit
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
    Friend WithEvents CFTFinishSewDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CCFTStartSewDate As DevExpress.XtraGrid.Columns.GridColumn
End Class

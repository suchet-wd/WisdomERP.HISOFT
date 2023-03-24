<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wAccountJobCost_CVN
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(wAccountJobCost_CVN))
        Dim GridLevelNode1 As DevExpress.XtraGrid.GridLevelNode = New DevExpress.XtraGrid.GridLevelNode()
        Dim GridLevelNode2 As DevExpress.XtraGrid.GridLevelNode = New DevExpress.XtraGrid.GridLevelNode()
        Dim GridLevelNode3 As DevExpress.XtraGrid.GridLevelNode = New DevExpress.XtraGrid.GridLevelNode()
        Me.DockManager1 = New DevExpress.XtraBars.Docking.DockManager(Me.components)
        Me.ogbheader = New DevExpress.XtraBars.Docking.DockPanel()
        Me.DockPanel1_Container = New DevExpress.XtraBars.Docking.ControlContainer()
        Me.FTPayYear_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTPayPeriod_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.ogbmainprocbutton = New DevExpress.XtraEditors.PanelControl()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmclear = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmload = New DevExpress.XtraEditors.SimpleButton()
        Me.FNMonth = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.FTDateStart = New DevExpress.XtraEditors.DateEdit()
        Me.ogbdetail = New DevExpress.XtraEditors.GroupControl()
        Me.XtraTabControl1 = New DevExpress.XtraTab.XtraTabControl()
        Me.XtraTabPage1 = New DevExpress.XtraTab.XtraTabPage()
        Me.XtraScrollableControl1 = New DevExpress.XtraEditors.XtraScrollableControl()
        Me.ogc = New DevExpress.XtraGrid.GridControl()
        Me.ogv = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.accFTPayYear = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.accFNMonth = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.accFTOrder = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.accFNMinute = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.accFNQty = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.accFNAmt_OverHead = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.accFNAmt_DL = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.accFNAmt_IDL = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.accFNAmt_MNG = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.accFNAmt_None = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.accFNAmt_Summary = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryFTSelect = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.RepositoryFTApproveState = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.XtraTabPage2 = New DevExpress.XtraTab.XtraTabPage()
        Me.XtraScrollableControl2 = New DevExpress.XtraEditors.XtraScrollableControl()
        Me.ogcDtl = New DevExpress.XtraGrid.GridControl()
        Me.odvDtl = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.dtlFTCalDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.dtlFTOrder = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.dtlFNCutMinute = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.dtlFNCutQty = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.dtlFNSewMinute = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.dtlFNSewQty = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.dtlFNPackMinute = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.dtlFNPackQty = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.dtlFNMinute = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.dtlFNQty = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.dtlFNAmt_OverHead = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.dtlFNAmt_DL = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.dtlFNAmt_IDL = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.dtlFNAmt_MNG = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.dtlFNAmt_None = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.dtlFNAmt_Summary = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCheckEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.RepositoryItemCheckEdit2 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.XtraTabPage3 = New DevExpress.XtraTab.XtraTabPage()
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.ogcDtlDaily = New DevExpress.XtraGrid.GridControl()
        Me.odvDtlDaily = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.xFTCalDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.xFTUnitSectCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.xFTOrder = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.xFTSubOrderNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.xFNSeq = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.xFTStartTime = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.xFTEndTime = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.xFNQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.xFNTotalMinute = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.xFTAction = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.xFNEmployeeInLine = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.xFNTotalLateMin = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.xFNEmployeeTotalWorkMin = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.xFNEmployeeLineTotalWorkMin = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.xFNEmployeeTotalWageAmt = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.xFNEmployeeTotalWageAmtAvgPerMin = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.xFNJobCostDLEmptypeDaily = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.xFNEmployeeMonthlyDL = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.xFNEmpTypeMonthlyDLAllMinPerDay = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.xFNAllJobMinutesActionPerDay = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.xFNEmpTypeMonthlyDLAllCostAvgPerMin = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.xFNEmpTypeMonthlyDLAllCostPerDayAction = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.xFNDLAddOtheTotalAmt = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.xFNUniSectTotalWorkMin = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.xFNDLAddOtherAmtAvgMin = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.xFNDLAddOtherAmt = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.xFNMonthlyDLAddOtheTotalAmt = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.xFNMonthlyUniSectTotalWorkMin = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.xFNMonthlyDLAddOtherAmtAvgMin = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.xFNMonthlyDLAddOtherAmt = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCheckEdit3 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.RepositoryItemCheckEdit4 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        CType(Me.DockManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbheader.SuspendLayout()
        Me.DockPanel1_Container.SuspendLayout()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbmainprocbutton.SuspendLayout()
        CType(Me.FNMonth.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTDateStart.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTDateStart.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogbdetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbdetail.SuspendLayout()
        CType(Me.XtraTabControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XtraTabControl1.SuspendLayout()
        Me.XtraTabPage1.SuspendLayout()
        Me.XtraScrollableControl1.SuspendLayout()
        CType(Me.ogc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryFTSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryFTApproveState, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XtraTabPage2.SuspendLayout()
        Me.XtraScrollableControl2.SuspendLayout()
        CType(Me.ogcDtl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.odvDtl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XtraTabPage3.SuspendLayout()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.ogcDtlDaily, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.odvDtlDaily, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
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
        Me.ogbheader.Dock = DevExpress.XtraBars.Docking.DockingStyle.Top
        Me.ogbheader.DockVertical = DevExpress.Utils.DefaultBoolean.[False]
        Me.ogbheader.ID = New System.Guid("15db17cf-1ce9-44e7-94d5-3a2fa8e70d6a")
        Me.ogbheader.ImageOptions.Image = CType(resources.GetObject("ogbheader.ImageOptions.Image"), System.Drawing.Image)
        Me.ogbheader.Location = New System.Drawing.Point(0, 0)
        Me.ogbheader.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbheader.Name = "ogbheader"
        Me.ogbheader.Options.AllowDockBottom = False
        Me.ogbheader.Options.AllowDockFill = False
        Me.ogbheader.Options.ShowCloseButton = False
        Me.ogbheader.Options.ShowMaximizeButton = False
        Me.ogbheader.OriginalSize = New System.Drawing.Size(1452, 114)
        Me.ogbheader.Size = New System.Drawing.Size(1452, 114)
        Me.ogbheader.Text = "Creteria"
        '
        'DockPanel1_Container
        '
        Me.DockPanel1_Container.Controls.Add(Me.FTPayYear_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FTPayPeriod_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.ogbmainprocbutton)
        Me.DockPanel1_Container.Controls.Add(Me.FNMonth)
        Me.DockPanel1_Container.Controls.Add(Me.FTDateStart)
        Me.DockPanel1_Container.Location = New System.Drawing.Point(4, 32)
        Me.DockPanel1_Container.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.DockPanel1_Container.Name = "DockPanel1_Container"
        Me.DockPanel1_Container.Size = New System.Drawing.Size(1444, 76)
        Me.DockPanel1_Container.TabIndex = 0
        '
        'FTPayYear_lbl
        '
        Me.FTPayYear_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTPayYear_lbl.Appearance.Options.UseForeColor = True
        Me.FTPayYear_lbl.Appearance.Options.UseTextOptions = True
        Me.FTPayYear_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTPayYear_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTPayYear_lbl.Location = New System.Drawing.Point(36, 19)
        Me.FTPayYear_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTPayYear_lbl.Name = "FTPayYear_lbl"
        Me.FTPayYear_lbl.Size = New System.Drawing.Size(100, 17)
        Me.FTPayYear_lbl.TabIndex = 507
        Me.FTPayYear_lbl.Tag = "2|"
        Me.FTPayYear_lbl.Text = "ปี :"
        '
        'FTPayPeriod_lbl
        '
        Me.FTPayPeriod_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTPayPeriod_lbl.Appearance.Options.UseForeColor = True
        Me.FTPayPeriod_lbl.Appearance.Options.UseTextOptions = True
        Me.FTPayPeriod_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTPayPeriod_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTPayPeriod_lbl.Location = New System.Drawing.Point(381, 22)
        Me.FTPayPeriod_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTPayPeriod_lbl.Name = "FTPayPeriod_lbl"
        Me.FTPayPeriod_lbl.Size = New System.Drawing.Size(78, 14)
        Me.FTPayPeriod_lbl.TabIndex = 506
        Me.FTPayPeriod_lbl.Tag = "2|"
        Me.FTPayPeriod_lbl.Text = "เดือนที่ :"
        '
        'ogbmainprocbutton
        '
        Me.ogbmainprocbutton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmexit)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmclear)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmload)
        Me.ogbmainprocbutton.Location = New System.Drawing.Point(954, 13)
        Me.ogbmainprocbutton.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbmainprocbutton.Name = "ogbmainprocbutton"
        Me.ogbmainprocbutton.Size = New System.Drawing.Size(464, 59)
        Me.ogbmainprocbutton.TabIndex = 303
        Me.ogbmainprocbutton.Tag = "2|"
        '
        'ocmexit
        '
        Me.ocmexit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmexit.Location = New System.Drawing.Point(273, 14)
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
        Me.ocmclear.Location = New System.Drawing.Point(128, 14)
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
        Me.ocmload.Location = New System.Drawing.Point(10, 14)
        Me.ocmload.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmload.Name = "ocmload"
        Me.ocmload.Size = New System.Drawing.Size(111, 31)
        Me.ocmload.TabIndex = 93
        Me.ocmload.TabStop = False
        Me.ocmload.Tag = "2|"
        Me.ocmload.Text = "Load"
        '
        'FNMonth
        '
        Me.FNMonth.EditValue = ""
        Me.FNMonth.EnterMoveNextControl = True
        Me.FNMonth.Location = New System.Drawing.Point(507, 19)
        Me.FNMonth.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNMonth.Name = "FNMonth"
        Me.FNMonth.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FNMonth.Properties.Appearance.Options.UseBackColor = True
        Me.FNMonth.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FNMonth.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FNMonth.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FNMonth.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FNMonth.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNMonth.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNMonth.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNMonth.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNMonth.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNMonth.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNMonth.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNMonth.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNMonth.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FNMonth.Properties.Tag = "FNMonth"
        Me.FNMonth.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.FNMonth.Size = New System.Drawing.Size(159, 22)
        Me.FNMonth.TabIndex = 505
        Me.FNMonth.Tag = "2|"
        '
        'FTDateStart
        '
        Me.FTDateStart.EditValue = Nothing
        Me.FTDateStart.EnterMoveNextControl = True
        Me.FTDateStart.Location = New System.Drawing.Point(186, 19)
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
        Me.FTDateStart.TabIndex = 504
        Me.FTDateStart.Tag = "2|"
        '
        'ogbdetail
        '
        Me.ogbdetail.Controls.Add(Me.XtraTabControl1)
        Me.ogbdetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogbdetail.Location = New System.Drawing.Point(0, 114)
        Me.ogbdetail.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbdetail.Name = "ogbdetail"
        Me.ogbdetail.Size = New System.Drawing.Size(1452, 488)
        Me.ogbdetail.TabIndex = 1
        Me.ogbdetail.Text = "Over Head Cost"
        '
        'XtraTabControl1
        '
        Me.XtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.XtraTabControl1.Location = New System.Drawing.Point(2, 28)
        Me.XtraTabControl1.Name = "XtraTabControl1"
        Me.XtraTabControl1.SelectedTabPage = Me.XtraTabPage1
        Me.XtraTabControl1.Size = New System.Drawing.Size(1448, 458)
        Me.XtraTabControl1.TabIndex = 0
        Me.XtraTabControl1.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.XtraTabPage1, Me.XtraTabPage2, Me.XtraTabPage3})
        '
        'XtraTabPage1
        '
        Me.XtraTabPage1.Controls.Add(Me.XtraScrollableControl1)
        Me.XtraTabPage1.Name = "XtraTabPage1"
        Me.XtraTabPage1.Size = New System.Drawing.Size(1446, 428)
        Me.XtraTabPage1.Text = "Summary"
        '
        'XtraScrollableControl1
        '
        Me.XtraScrollableControl1.Controls.Add(Me.ogc)
        Me.XtraScrollableControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.XtraScrollableControl1.Location = New System.Drawing.Point(0, 0)
        Me.XtraScrollableControl1.Name = "XtraScrollableControl1"
        Me.XtraScrollableControl1.Size = New System.Drawing.Size(1446, 428)
        Me.XtraScrollableControl1.TabIndex = 0
        '
        'ogc
        '
        Me.ogc.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogc.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        GridLevelNode1.RelationName = "Level1"
        Me.ogc.LevelTree.Nodes.AddRange(New DevExpress.XtraGrid.GridLevelNode() {GridLevelNode1})
        Me.ogc.Location = New System.Drawing.Point(0, 0)
        Me.ogc.MainView = Me.ogv
        Me.ogc.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogc.Name = "ogc"
        Me.ogc.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryFTSelect, Me.RepositoryFTApproveState})
        Me.ogc.Size = New System.Drawing.Size(1446, 428)
        Me.ogc.TabIndex = 4
        Me.ogc.TabStop = False
        Me.ogc.Tag = "2|"
        Me.ogc.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogv})
        '
        'ogv
        '
        Me.ogv.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.accFTPayYear, Me.accFNMonth, Me.accFTOrder, Me.accFNMinute, Me.accFNQty, Me.accFNAmt_OverHead, Me.accFNAmt_DL, Me.accFNAmt_IDL, Me.accFNAmt_MNG, Me.accFNAmt_None, Me.accFNAmt_Summary})
        Me.ogv.GridControl = Me.ogc
        Me.ogv.Name = "ogv"
        Me.ogv.OptionsCustomization.AllowQuickHideColumns = False
        Me.ogv.OptionsView.ColumnAutoWidth = False
        Me.ogv.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.ogv.OptionsView.ShowGroupPanel = False
        Me.ogv.Tag = "2|"
        '
        'accFTPayYear
        '
        Me.accFTPayYear.AppearanceHeader.Options.UseTextOptions = True
        Me.accFTPayYear.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.accFTPayYear.Caption = "FTPayYear"
        Me.accFTPayYear.FieldName = "FTPayYear"
        Me.accFTPayYear.MinWidth = 25
        Me.accFTPayYear.Name = "accFTPayYear"
        Me.accFTPayYear.OptionsColumn.AllowEdit = False
        Me.accFTPayYear.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.accFTPayYear.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.accFTPayYear.OptionsColumn.ReadOnly = True
        Me.accFTPayYear.Visible = True
        Me.accFTPayYear.VisibleIndex = 0
        Me.accFTPayYear.Width = 94
        '
        'accFNMonth
        '
        Me.accFNMonth.AppearanceHeader.Options.UseTextOptions = True
        Me.accFNMonth.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.accFNMonth.Caption = "accFNMonth"
        Me.accFNMonth.FieldName = "FNMonth"
        Me.accFNMonth.MinWidth = 25
        Me.accFNMonth.Name = "accFNMonth"
        Me.accFNMonth.OptionsColumn.AllowEdit = False
        Me.accFNMonth.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.accFNMonth.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.accFNMonth.OptionsColumn.ReadOnly = True
        Me.accFNMonth.Visible = True
        Me.accFNMonth.VisibleIndex = 1
        Me.accFNMonth.Width = 94
        '
        'accFTOrder
        '
        Me.accFTOrder.AppearanceHeader.Options.UseTextOptions = True
        Me.accFTOrder.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.accFTOrder.Caption = "accFTOrder"
        Me.accFTOrder.FieldName = "FTOrder"
        Me.accFTOrder.Name = "accFTOrder"
        Me.accFTOrder.OptionsColumn.AllowEdit = False
        Me.accFTOrder.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[True]
        Me.accFTOrder.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
        Me.accFTOrder.OptionsColumn.ReadOnly = True
        Me.accFTOrder.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "FTOrder", "{0}")})
        Me.accFTOrder.Visible = True
        Me.accFTOrder.VisibleIndex = 2
        Me.accFTOrder.Width = 88
        '
        'accFNMinute
        '
        Me.accFNMinute.AppearanceHeader.Options.UseTextOptions = True
        Me.accFNMinute.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.accFNMinute.Caption = "accFNMinute"
        Me.accFNMinute.FieldName = "FNMinute"
        Me.accFNMinute.Name = "accFNMinute"
        Me.accFNMinute.OptionsColumn.AllowEdit = False
        Me.accFNMinute.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[True]
        Me.accFNMinute.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
        Me.accFNMinute.OptionsColumn.ReadOnly = True
        Me.accFNMinute.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FNMinute", "{0:0.##}")})
        Me.accFNMinute.Visible = True
        Me.accFNMinute.VisibleIndex = 3
        Me.accFNMinute.Width = 171
        '
        'accFNQty
        '
        Me.accFNQty.AppearanceCell.Options.UseTextOptions = True
        Me.accFNQty.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.accFNQty.AppearanceHeader.Options.UseTextOptions = True
        Me.accFNQty.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.accFNQty.Caption = "accFNQty"
        Me.accFNQty.FieldName = "FNQty"
        Me.accFNQty.Name = "accFNQty"
        Me.accFNQty.OptionsColumn.AllowEdit = False
        Me.accFNQty.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[True]
        Me.accFNQty.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
        Me.accFNQty.OptionsColumn.ReadOnly = True
        Me.accFNQty.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FNQty", "{0:0.##}")})
        Me.accFNQty.Visible = True
        Me.accFNQty.VisibleIndex = 4
        '
        'accFNAmt_OverHead
        '
        Me.accFNAmt_OverHead.AppearanceCell.Options.UseTextOptions = True
        Me.accFNAmt_OverHead.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.accFNAmt_OverHead.AppearanceHeader.Options.UseTextOptions = True
        Me.accFNAmt_OverHead.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.accFNAmt_OverHead.Caption = "accFNAmt_OverHead"
        Me.accFNAmt_OverHead.DisplayFormat.FormatString = "{0:n2}"
        Me.accFNAmt_OverHead.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.accFNAmt_OverHead.FieldName = "FNAmt_OverHead"
        Me.accFNAmt_OverHead.Name = "accFNAmt_OverHead"
        Me.accFNAmt_OverHead.OptionsColumn.AllowEdit = False
        Me.accFNAmt_OverHead.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[True]
        Me.accFNAmt_OverHead.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
        Me.accFNAmt_OverHead.OptionsColumn.ReadOnly = True
        Me.accFNAmt_OverHead.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FNAmt_OverHead", "{0:n2}")})
        Me.accFNAmt_OverHead.Visible = True
        Me.accFNAmt_OverHead.VisibleIndex = 5
        '
        'accFNAmt_DL
        '
        Me.accFNAmt_DL.AppearanceCell.Options.UseTextOptions = True
        Me.accFNAmt_DL.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.accFNAmt_DL.AppearanceHeader.Options.UseTextOptions = True
        Me.accFNAmt_DL.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.accFNAmt_DL.Caption = "accFNAmt_DL"
        Me.accFNAmt_DL.DisplayFormat.FormatString = "{0:n2}"
        Me.accFNAmt_DL.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.accFNAmt_DL.FieldName = "FNAmt_DL"
        Me.accFNAmt_DL.Name = "accFNAmt_DL"
        Me.accFNAmt_DL.OptionsColumn.AllowEdit = False
        Me.accFNAmt_DL.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[True]
        Me.accFNAmt_DL.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
        Me.accFNAmt_DL.OptionsColumn.ReadOnly = True
        Me.accFNAmt_DL.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FNAmt_DL", "{0:n2}")})
        Me.accFNAmt_DL.Visible = True
        Me.accFNAmt_DL.VisibleIndex = 6
        '
        'accFNAmt_IDL
        '
        Me.accFNAmt_IDL.AppearanceCell.Options.UseTextOptions = True
        Me.accFNAmt_IDL.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.accFNAmt_IDL.AppearanceHeader.Options.UseTextOptions = True
        Me.accFNAmt_IDL.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.accFNAmt_IDL.Caption = "accFNAmt_IDL"
        Me.accFNAmt_IDL.DisplayFormat.FormatString = "{0:n2}"
        Me.accFNAmt_IDL.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.accFNAmt_IDL.FieldName = "FNAmt_IDL"
        Me.accFNAmt_IDL.Name = "accFNAmt_IDL"
        Me.accFNAmt_IDL.OptionsColumn.AllowEdit = False
        Me.accFNAmt_IDL.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[True]
        Me.accFNAmt_IDL.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
        Me.accFNAmt_IDL.OptionsColumn.ReadOnly = True
        Me.accFNAmt_IDL.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FNAmt_IDL", "{0:n2}")})
        Me.accFNAmt_IDL.Visible = True
        Me.accFNAmt_IDL.VisibleIndex = 7
        '
        'accFNAmt_MNG
        '
        Me.accFNAmt_MNG.Caption = "accFNAmt_MNG"
        Me.accFNAmt_MNG.DisplayFormat.FormatString = "{0:n2}"
        Me.accFNAmt_MNG.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.accFNAmt_MNG.FieldName = "FNAmt_MNG"
        Me.accFNAmt_MNG.MinWidth = 25
        Me.accFNAmt_MNG.Name = "accFNAmt_MNG"
        Me.accFNAmt_MNG.OptionsColumn.AllowEdit = False
        Me.accFNAmt_MNG.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.accFNAmt_MNG.OptionsColumn.ReadOnly = True
        Me.accFNAmt_MNG.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FNAmt_MNG", "{0:n2}")})
        Me.accFNAmt_MNG.Visible = True
        Me.accFNAmt_MNG.VisibleIndex = 9
        Me.accFNAmt_MNG.Width = 94
        '
        'accFNAmt_None
        '
        Me.accFNAmt_None.Caption = "accFNAmt_None"
        Me.accFNAmt_None.DisplayFormat.FormatString = "{0:n2}"
        Me.accFNAmt_None.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.accFNAmt_None.FieldName = "FNAmt_None"
        Me.accFNAmt_None.MinWidth = 25
        Me.accFNAmt_None.Name = "accFNAmt_None"
        Me.accFNAmt_None.OptionsColumn.AllowEdit = False
        Me.accFNAmt_None.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.accFNAmt_None.OptionsColumn.ReadOnly = True
        Me.accFNAmt_None.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FNAmt_None", "{0:n2}")})
        Me.accFNAmt_None.Visible = True
        Me.accFNAmt_None.VisibleIndex = 8
        Me.accFNAmt_None.Width = 94
        '
        'accFNAmt_Summary
        '
        Me.accFNAmt_Summary.Caption = "accFNAmt_Summary"
        Me.accFNAmt_Summary.DisplayFormat.FormatString = "{0:n2}"
        Me.accFNAmt_Summary.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.accFNAmt_Summary.FieldName = "FNAmt_Summary"
        Me.accFNAmt_Summary.MinWidth = 25
        Me.accFNAmt_Summary.Name = "accFNAmt_Summary"
        Me.accFNAmt_Summary.OptionsColumn.AllowEdit = False
        Me.accFNAmt_Summary.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.accFNAmt_Summary.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.accFNAmt_Summary.OptionsColumn.ReadOnly = True
        Me.accFNAmt_Summary.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FNAmt_Summary", "{0:n2}")})
        Me.accFNAmt_Summary.Visible = True
        Me.accFNAmt_Summary.VisibleIndex = 10
        Me.accFNAmt_Summary.Width = 94
        '
        'RepositoryFTSelect
        '
        Me.RepositoryFTSelect.AutoHeight = False
        Me.RepositoryFTSelect.Caption = "Check"
        Me.RepositoryFTSelect.Name = "RepositoryFTSelect"
        Me.RepositoryFTSelect.ValueChecked = "1"
        Me.RepositoryFTSelect.ValueUnchecked = "0"
        '
        'RepositoryFTApproveState
        '
        Me.RepositoryFTApproveState.AutoHeight = False
        Me.RepositoryFTApproveState.Caption = "Check"
        Me.RepositoryFTApproveState.Name = "RepositoryFTApproveState"
        Me.RepositoryFTApproveState.ValueChecked = "1"
        Me.RepositoryFTApproveState.ValueUnchecked = "0"
        '
        'XtraTabPage2
        '
        Me.XtraTabPage2.Controls.Add(Me.XtraScrollableControl2)
        Me.XtraTabPage2.Name = "XtraTabPage2"
        Me.XtraTabPage2.Size = New System.Drawing.Size(1446, 429)
        Me.XtraTabPage2.Text = "Detail"
        '
        'XtraScrollableControl2
        '
        Me.XtraScrollableControl2.Controls.Add(Me.ogcDtl)
        Me.XtraScrollableControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.XtraScrollableControl2.Location = New System.Drawing.Point(0, 0)
        Me.XtraScrollableControl2.Name = "XtraScrollableControl2"
        Me.XtraScrollableControl2.Size = New System.Drawing.Size(1446, 429)
        Me.XtraScrollableControl2.TabIndex = 0
        '
        'ogcDtl
        '
        Me.ogcDtl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogcDtl.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        GridLevelNode2.RelationName = "Level1"
        Me.ogcDtl.LevelTree.Nodes.AddRange(New DevExpress.XtraGrid.GridLevelNode() {GridLevelNode2})
        Me.ogcDtl.Location = New System.Drawing.Point(0, 0)
        Me.ogcDtl.MainView = Me.odvDtl
        Me.ogcDtl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcDtl.Name = "ogcDtl"
        Me.ogcDtl.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCheckEdit1, Me.RepositoryItemCheckEdit2})
        Me.ogcDtl.Size = New System.Drawing.Size(1446, 429)
        Me.ogcDtl.TabIndex = 5
        Me.ogcDtl.TabStop = False
        Me.ogcDtl.Tag = "2|"
        Me.ogcDtl.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.odvDtl})
        '
        'odvDtl
        '
        Me.odvDtl.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.dtlFTCalDate, Me.dtlFTOrder, Me.dtlFNCutMinute, Me.dtlFNCutQty, Me.dtlFNSewMinute, Me.dtlFNSewQty, Me.dtlFNPackMinute, Me.dtlFNPackQty, Me.dtlFNMinute, Me.dtlFNQty, Me.dtlFNAmt_OverHead, Me.dtlFNAmt_DL, Me.dtlFNAmt_IDL, Me.dtlFNAmt_MNG, Me.dtlFNAmt_None, Me.dtlFNAmt_Summary})
        Me.odvDtl.GridControl = Me.ogcDtl
        Me.odvDtl.Name = "odvDtl"
        Me.odvDtl.OptionsCustomization.AllowQuickHideColumns = False
        Me.odvDtl.OptionsView.ColumnAutoWidth = False
        Me.odvDtl.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.odvDtl.OptionsView.ShowGroupPanel = False
        Me.odvDtl.Tag = "2|"
        '
        'dtlFTCalDate
        '
        Me.dtlFTCalDate.AppearanceHeader.Options.UseTextOptions = True
        Me.dtlFTCalDate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.dtlFTCalDate.Caption = "dtlFTCalDate"
        Me.dtlFTCalDate.FieldName = "FTCalDate"
        Me.dtlFTCalDate.MinWidth = 25
        Me.dtlFTCalDate.Name = "dtlFTCalDate"
        Me.dtlFTCalDate.OptionsColumn.AllowEdit = False
        Me.dtlFTCalDate.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.dtlFTCalDate.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.dtlFTCalDate.OptionsColumn.ReadOnly = True
        Me.dtlFTCalDate.Visible = True
        Me.dtlFTCalDate.VisibleIndex = 0
        Me.dtlFTCalDate.Width = 94
        '
        'dtlFTOrder
        '
        Me.dtlFTOrder.AppearanceHeader.Options.UseTextOptions = True
        Me.dtlFTOrder.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.dtlFTOrder.Caption = "dtlFTOrder"
        Me.dtlFTOrder.FieldName = "FTOrder"
        Me.dtlFTOrder.Name = "dtlFTOrder"
        Me.dtlFTOrder.OptionsColumn.AllowEdit = False
        Me.dtlFTOrder.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[True]
        Me.dtlFTOrder.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
        Me.dtlFTOrder.OptionsColumn.ReadOnly = True
        Me.dtlFTOrder.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "FTOrder", "{0}")})
        Me.dtlFTOrder.Visible = True
        Me.dtlFTOrder.VisibleIndex = 1
        Me.dtlFTOrder.Width = 88
        '
        'dtlFNCutMinute
        '
        Me.dtlFNCutMinute.AppearanceCell.Options.UseTextOptions = True
        Me.dtlFNCutMinute.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.dtlFNCutMinute.Caption = "dtlFNCutMinute"
        Me.dtlFNCutMinute.FieldName = "FNCutMinute"
        Me.dtlFNCutMinute.MinWidth = 25
        Me.dtlFNCutMinute.Name = "dtlFNCutMinute"
        Me.dtlFNCutMinute.OptionsColumn.AllowEdit = False
        Me.dtlFNCutMinute.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.dtlFNCutMinute.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.dtlFNCutMinute.OptionsColumn.ReadOnly = True
        Me.dtlFNCutMinute.Visible = True
        Me.dtlFNCutMinute.VisibleIndex = 2
        Me.dtlFNCutMinute.Width = 94
        '
        'dtlFNCutQty
        '
        Me.dtlFNCutQty.AppearanceCell.Options.UseTextOptions = True
        Me.dtlFNCutQty.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.dtlFNCutQty.Caption = "dtlFNCutQty"
        Me.dtlFNCutQty.FieldName = "FNCutQty"
        Me.dtlFNCutQty.MinWidth = 25
        Me.dtlFNCutQty.Name = "dtlFNCutQty"
        Me.dtlFNCutQty.OptionsColumn.AllowEdit = False
        Me.dtlFNCutQty.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.dtlFNCutQty.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.dtlFNCutQty.OptionsColumn.ReadOnly = True
        Me.dtlFNCutQty.Visible = True
        Me.dtlFNCutQty.VisibleIndex = 3
        Me.dtlFNCutQty.Width = 94
        '
        'dtlFNSewMinute
        '
        Me.dtlFNSewMinute.AppearanceCell.Options.UseTextOptions = True
        Me.dtlFNSewMinute.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.dtlFNSewMinute.Caption = "dtlFNSewMinute"
        Me.dtlFNSewMinute.FieldName = "FNSewMinute"
        Me.dtlFNSewMinute.MinWidth = 25
        Me.dtlFNSewMinute.Name = "dtlFNSewMinute"
        Me.dtlFNSewMinute.OptionsColumn.AllowEdit = False
        Me.dtlFNSewMinute.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.dtlFNSewMinute.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.dtlFNSewMinute.OptionsColumn.ReadOnly = True
        Me.dtlFNSewMinute.Visible = True
        Me.dtlFNSewMinute.VisibleIndex = 4
        Me.dtlFNSewMinute.Width = 94
        '
        'dtlFNSewQty
        '
        Me.dtlFNSewQty.AppearanceCell.Options.UseTextOptions = True
        Me.dtlFNSewQty.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.dtlFNSewQty.Caption = "dtlFNSewQty"
        Me.dtlFNSewQty.FieldName = "FNSewQty"
        Me.dtlFNSewQty.MinWidth = 25
        Me.dtlFNSewQty.Name = "dtlFNSewQty"
        Me.dtlFNSewQty.OptionsColumn.AllowEdit = False
        Me.dtlFNSewQty.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.dtlFNSewQty.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
        Me.dtlFNSewQty.OptionsColumn.ReadOnly = True
        Me.dtlFNSewQty.Visible = True
        Me.dtlFNSewQty.VisibleIndex = 5
        Me.dtlFNSewQty.Width = 94
        '
        'dtlFNPackMinute
        '
        Me.dtlFNPackMinute.AppearanceCell.Options.UseTextOptions = True
        Me.dtlFNPackMinute.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.dtlFNPackMinute.Caption = "dtlFNPackMinute"
        Me.dtlFNPackMinute.FieldName = "FNPackMinute"
        Me.dtlFNPackMinute.MinWidth = 25
        Me.dtlFNPackMinute.Name = "dtlFNPackMinute"
        Me.dtlFNPackMinute.OptionsColumn.AllowEdit = False
        Me.dtlFNPackMinute.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.dtlFNPackMinute.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.dtlFNPackMinute.Visible = True
        Me.dtlFNPackMinute.VisibleIndex = 6
        Me.dtlFNPackMinute.Width = 94
        '
        'dtlFNPackQty
        '
        Me.dtlFNPackQty.AppearanceCell.Options.UseTextOptions = True
        Me.dtlFNPackQty.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.dtlFNPackQty.Caption = "dtlFNPackQty"
        Me.dtlFNPackQty.FieldName = "FNPackQty"
        Me.dtlFNPackQty.MinWidth = 25
        Me.dtlFNPackQty.Name = "dtlFNPackQty"
        Me.dtlFNPackQty.OptionsColumn.AllowEdit = False
        Me.dtlFNPackQty.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.dtlFNPackQty.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.dtlFNPackQty.OptionsColumn.ReadOnly = True
        Me.dtlFNPackQty.Visible = True
        Me.dtlFNPackQty.VisibleIndex = 7
        Me.dtlFNPackQty.Width = 94
        '
        'dtlFNMinute
        '
        Me.dtlFNMinute.AppearanceCell.Options.UseTextOptions = True
        Me.dtlFNMinute.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.dtlFNMinute.AppearanceHeader.Options.UseTextOptions = True
        Me.dtlFNMinute.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.dtlFNMinute.Caption = "dtlFNMinute"
        Me.dtlFNMinute.FieldName = "FNMinute"
        Me.dtlFNMinute.Name = "dtlFNMinute"
        Me.dtlFNMinute.OptionsColumn.AllowEdit = False
        Me.dtlFNMinute.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.dtlFNMinute.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
        Me.dtlFNMinute.OptionsColumn.ReadOnly = True
        Me.dtlFNMinute.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FNMinute", "{0:0.##}")})
        Me.dtlFNMinute.Visible = True
        Me.dtlFNMinute.VisibleIndex = 8
        Me.dtlFNMinute.Width = 171
        '
        'dtlFNQty
        '
        Me.dtlFNQty.AppearanceCell.Options.UseTextOptions = True
        Me.dtlFNQty.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.dtlFNQty.AppearanceHeader.Options.UseTextOptions = True
        Me.dtlFNQty.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.dtlFNQty.Caption = "dtlFNQty"
        Me.dtlFNQty.FieldName = "FNQty"
        Me.dtlFNQty.Name = "dtlFNQty"
        Me.dtlFNQty.OptionsColumn.AllowEdit = False
        Me.dtlFNQty.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.dtlFNQty.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
        Me.dtlFNQty.OptionsColumn.ReadOnly = True
        Me.dtlFNQty.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FNQty", "{0:0.##}")})
        Me.dtlFNQty.Visible = True
        Me.dtlFNQty.VisibleIndex = 9
        '
        'dtlFNAmt_OverHead
        '
        Me.dtlFNAmt_OverHead.AppearanceCell.Options.UseTextOptions = True
        Me.dtlFNAmt_OverHead.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.dtlFNAmt_OverHead.AppearanceHeader.Options.UseTextOptions = True
        Me.dtlFNAmt_OverHead.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.dtlFNAmt_OverHead.Caption = "dtlFNAmt_OverHead"
        Me.dtlFNAmt_OverHead.DisplayFormat.FormatString = "{0:n2}"
        Me.dtlFNAmt_OverHead.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.dtlFNAmt_OverHead.FieldName = "FNAmt_OverHead"
        Me.dtlFNAmt_OverHead.Name = "dtlFNAmt_OverHead"
        Me.dtlFNAmt_OverHead.OptionsColumn.AllowEdit = False
        Me.dtlFNAmt_OverHead.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.dtlFNAmt_OverHead.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
        Me.dtlFNAmt_OverHead.OptionsColumn.ReadOnly = True
        Me.dtlFNAmt_OverHead.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FNAmt_OverHead", "{0:n2}")})
        Me.dtlFNAmt_OverHead.Visible = True
        Me.dtlFNAmt_OverHead.VisibleIndex = 10
        '
        'dtlFNAmt_DL
        '
        Me.dtlFNAmt_DL.AppearanceCell.Options.UseTextOptions = True
        Me.dtlFNAmt_DL.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.dtlFNAmt_DL.AppearanceHeader.Options.UseTextOptions = True
        Me.dtlFNAmt_DL.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.dtlFNAmt_DL.Caption = "dtlFNAmt_DL"
        Me.dtlFNAmt_DL.DisplayFormat.FormatString = "{0:n2}"
        Me.dtlFNAmt_DL.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.dtlFNAmt_DL.FieldName = "FNAmt_DL"
        Me.dtlFNAmt_DL.Name = "dtlFNAmt_DL"
        Me.dtlFNAmt_DL.OptionsColumn.AllowEdit = False
        Me.dtlFNAmt_DL.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.dtlFNAmt_DL.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
        Me.dtlFNAmt_DL.OptionsColumn.ReadOnly = True
        Me.dtlFNAmt_DL.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FNAmt_DL", "{0:n2}")})
        Me.dtlFNAmt_DL.Visible = True
        Me.dtlFNAmt_DL.VisibleIndex = 11
        '
        'dtlFNAmt_IDL
        '
        Me.dtlFNAmt_IDL.AppearanceCell.Options.UseTextOptions = True
        Me.dtlFNAmt_IDL.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.dtlFNAmt_IDL.AppearanceHeader.Options.UseTextOptions = True
        Me.dtlFNAmt_IDL.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.dtlFNAmt_IDL.Caption = "dtlFNAmt_IDL"
        Me.dtlFNAmt_IDL.DisplayFormat.FormatString = "{0:n2}"
        Me.dtlFNAmt_IDL.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.dtlFNAmt_IDL.FieldName = "FNAmt_IDL"
        Me.dtlFNAmt_IDL.Name = "dtlFNAmt_IDL"
        Me.dtlFNAmt_IDL.OptionsColumn.AllowEdit = False
        Me.dtlFNAmt_IDL.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.dtlFNAmt_IDL.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
        Me.dtlFNAmt_IDL.OptionsColumn.ReadOnly = True
        Me.dtlFNAmt_IDL.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FNAmt_IDL", "{0:n2}")})
        Me.dtlFNAmt_IDL.Visible = True
        Me.dtlFNAmt_IDL.VisibleIndex = 12
        '
        'dtlFNAmt_MNG
        '
        Me.dtlFNAmt_MNG.AppearanceCell.Options.UseTextOptions = True
        Me.dtlFNAmt_MNG.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.dtlFNAmt_MNG.Caption = "dtlFNAmt_MNG"
        Me.dtlFNAmt_MNG.DisplayFormat.FormatString = "{0:n2}"
        Me.dtlFNAmt_MNG.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.dtlFNAmt_MNG.FieldName = "FNAmt_MNG"
        Me.dtlFNAmt_MNG.MinWidth = 25
        Me.dtlFNAmt_MNG.Name = "dtlFNAmt_MNG"
        Me.dtlFNAmt_MNG.OptionsColumn.AllowEdit = False
        Me.dtlFNAmt_MNG.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.dtlFNAmt_MNG.OptionsColumn.ReadOnly = True
        Me.dtlFNAmt_MNG.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FNAmt_MNG", "{0:n2}")})
        Me.dtlFNAmt_MNG.Visible = True
        Me.dtlFNAmt_MNG.VisibleIndex = 14
        Me.dtlFNAmt_MNG.Width = 94
        '
        'dtlFNAmt_None
        '
        Me.dtlFNAmt_None.AppearanceCell.Options.UseTextOptions = True
        Me.dtlFNAmt_None.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.dtlFNAmt_None.Caption = "dtlFNAmt_None"
        Me.dtlFNAmt_None.DisplayFormat.FormatString = "{0:n2}"
        Me.dtlFNAmt_None.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.dtlFNAmt_None.FieldName = "FNAmt_None"
        Me.dtlFNAmt_None.MinWidth = 25
        Me.dtlFNAmt_None.Name = "dtlFNAmt_None"
        Me.dtlFNAmt_None.OptionsColumn.AllowEdit = False
        Me.dtlFNAmt_None.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.dtlFNAmt_None.OptionsColumn.ReadOnly = True
        Me.dtlFNAmt_None.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FNAmt_None", "{0:n2}")})
        Me.dtlFNAmt_None.Visible = True
        Me.dtlFNAmt_None.VisibleIndex = 13
        Me.dtlFNAmt_None.Width = 94
        '
        'dtlFNAmt_Summary
        '
        Me.dtlFNAmt_Summary.AppearanceCell.Options.UseTextOptions = True
        Me.dtlFNAmt_Summary.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.dtlFNAmt_Summary.Caption = "dtlFNAmt_Summary"
        Me.dtlFNAmt_Summary.DisplayFormat.FormatString = "{0:n2}"
        Me.dtlFNAmt_Summary.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.dtlFNAmt_Summary.FieldName = "FNAmt_Summary"
        Me.dtlFNAmt_Summary.MinWidth = 25
        Me.dtlFNAmt_Summary.Name = "dtlFNAmt_Summary"
        Me.dtlFNAmt_Summary.OptionsColumn.AllowEdit = False
        Me.dtlFNAmt_Summary.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[True]
        Me.dtlFNAmt_Summary.OptionsColumn.ReadOnly = True
        Me.dtlFNAmt_Summary.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FNAmt_Summary", "{0:n2}")})
        Me.dtlFNAmt_Summary.Visible = True
        Me.dtlFNAmt_Summary.VisibleIndex = 15
        Me.dtlFNAmt_Summary.Width = 94
        '
        'RepositoryItemCheckEdit1
        '
        Me.RepositoryItemCheckEdit1.AutoHeight = False
        Me.RepositoryItemCheckEdit1.Caption = "Check"
        Me.RepositoryItemCheckEdit1.Name = "RepositoryItemCheckEdit1"
        Me.RepositoryItemCheckEdit1.ValueChecked = "1"
        Me.RepositoryItemCheckEdit1.ValueUnchecked = "0"
        '
        'RepositoryItemCheckEdit2
        '
        Me.RepositoryItemCheckEdit2.AutoHeight = False
        Me.RepositoryItemCheckEdit2.Caption = "Check"
        Me.RepositoryItemCheckEdit2.Name = "RepositoryItemCheckEdit2"
        Me.RepositoryItemCheckEdit2.ValueChecked = "1"
        Me.RepositoryItemCheckEdit2.ValueUnchecked = "0"
        '
        'XtraTabPage3
        '
        Me.XtraTabPage3.Controls.Add(Me.PanelControl1)
        Me.XtraTabPage3.Name = "XtraTabPage3"
        Me.XtraTabPage3.Size = New System.Drawing.Size(1446, 428)
        Me.XtraTabPage3.Text = "Detail Daily"
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.ogcDtlDaily)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelControl1.Location = New System.Drawing.Point(0, 0)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(1446, 428)
        Me.PanelControl1.TabIndex = 0
        '
        'ogcDtlDaily
        '
        Me.ogcDtlDaily.Cursor = System.Windows.Forms.Cursors.Default
        Me.ogcDtlDaily.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogcDtlDaily.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        GridLevelNode3.RelationName = "Level1"
        Me.ogcDtlDaily.LevelTree.Nodes.AddRange(New DevExpress.XtraGrid.GridLevelNode() {GridLevelNode3})
        Me.ogcDtlDaily.Location = New System.Drawing.Point(2, 2)
        Me.ogcDtlDaily.MainView = Me.odvDtlDaily
        Me.ogcDtlDaily.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcDtlDaily.Name = "ogcDtlDaily"
        Me.ogcDtlDaily.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCheckEdit3, Me.RepositoryItemCheckEdit4})
        Me.ogcDtlDaily.Size = New System.Drawing.Size(1442, 424)
        Me.ogcDtlDaily.TabIndex = 6
        Me.ogcDtlDaily.TabStop = False
        Me.ogcDtlDaily.Tag = "2|"
        Me.ogcDtlDaily.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.odvDtlDaily})
        '
        'odvDtlDaily
        '
        Me.odvDtlDaily.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.xFTCalDate, Me.xFTUnitSectCode, Me.xFTOrder, Me.xFTSubOrderNo, Me.xFNSeq, Me.xFTStartTime, Me.xFTEndTime, Me.xFNQuantity, Me.xFNTotalMinute, Me.xFTAction, Me.xFNEmployeeInLine, Me.xFNTotalLateMin, Me.xFNEmployeeTotalWorkMin, Me.xFNEmployeeLineTotalWorkMin, Me.xFNEmployeeTotalWageAmt, Me.xFNEmployeeTotalWageAmtAvgPerMin, Me.xFNJobCostDLEmptypeDaily, Me.xFNEmployeeMonthlyDL, Me.xFNEmpTypeMonthlyDLAllMinPerDay, Me.xFNAllJobMinutesActionPerDay, Me.xFNEmpTypeMonthlyDLAllCostAvgPerMin, Me.xFNEmpTypeMonthlyDLAllCostPerDayAction, Me.xFNDLAddOtheTotalAmt, Me.xFNUniSectTotalWorkMin, Me.xFNDLAddOtherAmtAvgMin, Me.xFNDLAddOtherAmt, Me.xFNMonthlyDLAddOtheTotalAmt, Me.xFNMonthlyUniSectTotalWorkMin, Me.xFNMonthlyDLAddOtherAmtAvgMin, Me.xFNMonthlyDLAddOtherAmt})
        Me.odvDtlDaily.GridControl = Me.ogcDtlDaily
        Me.odvDtlDaily.Name = "odvDtlDaily"
        Me.odvDtlDaily.OptionsCustomization.AllowQuickHideColumns = False
        Me.odvDtlDaily.OptionsView.ColumnAutoWidth = False
        Me.odvDtlDaily.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.odvDtlDaily.OptionsView.ShowGroupPanel = False
        Me.odvDtlDaily.Tag = "2|"
        '
        'xFTCalDate
        '
        Me.xFTCalDate.AppearanceHeader.Options.UseTextOptions = True
        Me.xFTCalDate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.xFTCalDate.Caption = "xFTCalDate"
        Me.xFTCalDate.FieldName = "FTCalDate"
        Me.xFTCalDate.MinWidth = 25
        Me.xFTCalDate.Name = "xFTCalDate"
        Me.xFTCalDate.OptionsColumn.AllowEdit = False
        Me.xFTCalDate.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.xFTCalDate.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.xFTCalDate.OptionsColumn.ReadOnly = True
        Me.xFTCalDate.Visible = True
        Me.xFTCalDate.VisibleIndex = 3
        Me.xFTCalDate.Width = 94
        '
        'xFTUnitSectCode
        '
        Me.xFTUnitSectCode.Caption = "xFTUnitSectCode"
        Me.xFTUnitSectCode.FieldName = "FTUnitSectCode"
        Me.xFTUnitSectCode.MinWidth = 25
        Me.xFTUnitSectCode.Name = "xFTUnitSectCode"
        Me.xFTUnitSectCode.OptionsColumn.AllowEdit = False
        Me.xFTUnitSectCode.OptionsColumn.ReadOnly = True
        Me.xFTUnitSectCode.Visible = True
        Me.xFTUnitSectCode.VisibleIndex = 0
        Me.xFTUnitSectCode.Width = 94
        '
        'xFTOrder
        '
        Me.xFTOrder.AppearanceHeader.Options.UseTextOptions = True
        Me.xFTOrder.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.xFTOrder.Caption = "xFTOrder"
        Me.xFTOrder.FieldName = "FTOrderNo"
        Me.xFTOrder.Name = "xFTOrder"
        Me.xFTOrder.OptionsColumn.AllowEdit = False
        Me.xFTOrder.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[True]
        Me.xFTOrder.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
        Me.xFTOrder.OptionsColumn.ReadOnly = True
        Me.xFTOrder.Visible = True
        Me.xFTOrder.VisibleIndex = 1
        Me.xFTOrder.Width = 88
        '
        'xFTSubOrderNo
        '
        Me.xFTSubOrderNo.AppearanceCell.Options.UseTextOptions = True
        Me.xFTSubOrderNo.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.xFTSubOrderNo.Caption = "xFTSubOrderNo"
        Me.xFTSubOrderNo.FieldName = "FTSubOrderNo"
        Me.xFTSubOrderNo.MinWidth = 25
        Me.xFTSubOrderNo.Name = "xFTSubOrderNo"
        Me.xFTSubOrderNo.OptionsColumn.AllowEdit = False
        Me.xFTSubOrderNo.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.xFTSubOrderNo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.xFTSubOrderNo.OptionsColumn.ReadOnly = True
        Me.xFTSubOrderNo.Visible = True
        Me.xFTSubOrderNo.VisibleIndex = 2
        Me.xFTSubOrderNo.Width = 94
        '
        'xFNSeq
        '
        Me.xFNSeq.AppearanceCell.Options.UseTextOptions = True
        Me.xFNSeq.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.xFNSeq.Caption = "xFNSeq"
        Me.xFNSeq.FieldName = "FNSeq"
        Me.xFNSeq.MinWidth = 25
        Me.xFNSeq.Name = "xFNSeq"
        Me.xFNSeq.OptionsColumn.AllowEdit = False
        Me.xFNSeq.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.xFNSeq.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.xFNSeq.OptionsColumn.ReadOnly = True
        Me.xFNSeq.Visible = True
        Me.xFNSeq.VisibleIndex = 4
        Me.xFNSeq.Width = 94
        '
        'xFTStartTime
        '
        Me.xFTStartTime.AppearanceCell.Options.UseTextOptions = True
        Me.xFTStartTime.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.xFTStartTime.Caption = "xFTStartTime"
        Me.xFTStartTime.FieldName = "FTStartTime"
        Me.xFTStartTime.MinWidth = 25
        Me.xFTStartTime.Name = "xFTStartTime"
        Me.xFTStartTime.OptionsColumn.AllowEdit = False
        Me.xFTStartTime.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.xFTStartTime.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.xFTStartTime.OptionsColumn.ReadOnly = True
        Me.xFTStartTime.Visible = True
        Me.xFTStartTime.VisibleIndex = 5
        Me.xFTStartTime.Width = 94
        '
        'xFTEndTime
        '
        Me.xFTEndTime.AppearanceCell.Options.UseTextOptions = True
        Me.xFTEndTime.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.xFTEndTime.Caption = "xFTEndTime"
        Me.xFTEndTime.FieldName = "FTEndTime"
        Me.xFTEndTime.MinWidth = 25
        Me.xFTEndTime.Name = "xFTEndTime"
        Me.xFTEndTime.OptionsColumn.AllowEdit = False
        Me.xFTEndTime.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.xFTEndTime.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
        Me.xFTEndTime.OptionsColumn.ReadOnly = True
        Me.xFTEndTime.Visible = True
        Me.xFTEndTime.VisibleIndex = 6
        Me.xFTEndTime.Width = 94
        '
        'xFNQuantity
        '
        Me.xFNQuantity.AppearanceCell.Options.UseTextOptions = True
        Me.xFNQuantity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.xFNQuantity.Caption = "xFNQuantity"
        Me.xFNQuantity.FieldName = "FNQuantity"
        Me.xFNQuantity.MinWidth = 25
        Me.xFNQuantity.Name = "xFNQuantity"
        Me.xFNQuantity.OptionsColumn.AllowEdit = False
        Me.xFNQuantity.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.xFNQuantity.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.xFNQuantity.Visible = True
        Me.xFNQuantity.VisibleIndex = 7
        Me.xFNQuantity.Width = 94
        '
        'xFNTotalMinute
        '
        Me.xFNTotalMinute.AppearanceCell.Options.UseTextOptions = True
        Me.xFNTotalMinute.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.xFNTotalMinute.Caption = "xFNTotalMinute"
        Me.xFNTotalMinute.FieldName = "FNTotalMinute"
        Me.xFNTotalMinute.MinWidth = 25
        Me.xFNTotalMinute.Name = "xFNTotalMinute"
        Me.xFNTotalMinute.OptionsColumn.AllowEdit = False
        Me.xFNTotalMinute.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.xFNTotalMinute.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.xFNTotalMinute.OptionsColumn.ReadOnly = True
        Me.xFNTotalMinute.Visible = True
        Me.xFNTotalMinute.VisibleIndex = 8
        Me.xFNTotalMinute.Width = 94
        '
        'xFTAction
        '
        Me.xFTAction.AppearanceCell.Options.UseTextOptions = True
        Me.xFTAction.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.xFTAction.AppearanceHeader.Options.UseTextOptions = True
        Me.xFTAction.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.xFTAction.Caption = "xFTAction"
        Me.xFTAction.FieldName = "FTAction"
        Me.xFTAction.Name = "xFTAction"
        Me.xFTAction.OptionsColumn.AllowEdit = False
        Me.xFTAction.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.xFTAction.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
        Me.xFTAction.OptionsColumn.ReadOnly = True
        Me.xFTAction.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FNMinute", "{0:0.##}")})
        Me.xFTAction.Visible = True
        Me.xFTAction.VisibleIndex = 9
        Me.xFTAction.Width = 171
        '
        'xFNEmployeeInLine
        '
        Me.xFNEmployeeInLine.AppearanceCell.Options.UseTextOptions = True
        Me.xFNEmployeeInLine.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.xFNEmployeeInLine.AppearanceHeader.Options.UseTextOptions = True
        Me.xFNEmployeeInLine.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.xFNEmployeeInLine.Caption = "xFNEmployeeInLine"
        Me.xFNEmployeeInLine.FieldName = "FNEmployeeInLine"
        Me.xFNEmployeeInLine.Name = "xFNEmployeeInLine"
        Me.xFNEmployeeInLine.OptionsColumn.AllowEdit = False
        Me.xFNEmployeeInLine.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.xFNEmployeeInLine.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
        Me.xFNEmployeeInLine.OptionsColumn.ReadOnly = True
        Me.xFNEmployeeInLine.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FNQty", "{0:0.##}")})
        Me.xFNEmployeeInLine.Visible = True
        Me.xFNEmployeeInLine.VisibleIndex = 10
        '
        'xFNTotalLateMin
        '
        Me.xFNTotalLateMin.AppearanceCell.Options.UseTextOptions = True
        Me.xFNTotalLateMin.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.xFNTotalLateMin.AppearanceHeader.Options.UseTextOptions = True
        Me.xFNTotalLateMin.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.xFNTotalLateMin.Caption = "xFNTotalLateMin"
        Me.xFNTotalLateMin.FieldName = "FNTotalLateMin"
        Me.xFNTotalLateMin.Name = "xFNTotalLateMin"
        Me.xFNTotalLateMin.OptionsColumn.AllowEdit = False
        Me.xFNTotalLateMin.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.xFNTotalLateMin.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
        Me.xFNTotalLateMin.OptionsColumn.ReadOnly = True
        Me.xFNTotalLateMin.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FNAmt_OverHead", "{0:n2}")})
        '
        'xFNEmployeeTotalWorkMin
        '
        Me.xFNEmployeeTotalWorkMin.AppearanceCell.Options.UseTextOptions = True
        Me.xFNEmployeeTotalWorkMin.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.xFNEmployeeTotalWorkMin.AppearanceHeader.Options.UseTextOptions = True
        Me.xFNEmployeeTotalWorkMin.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.xFNEmployeeTotalWorkMin.Caption = "xFNEmployeeTotalWorkMin"
        Me.xFNEmployeeTotalWorkMin.FieldName = "FNEmployeeTotalWorkMin"
        Me.xFNEmployeeTotalWorkMin.Name = "xFNEmployeeTotalWorkMin"
        Me.xFNEmployeeTotalWorkMin.OptionsColumn.AllowEdit = False
        Me.xFNEmployeeTotalWorkMin.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.xFNEmployeeTotalWorkMin.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
        Me.xFNEmployeeTotalWorkMin.OptionsColumn.ReadOnly = True
        Me.xFNEmployeeTotalWorkMin.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FNAmt_DL", "{0:n2}")})
        Me.xFNEmployeeTotalWorkMin.Visible = True
        Me.xFNEmployeeTotalWorkMin.VisibleIndex = 11
        '
        'xFNEmployeeLineTotalWorkMin
        '
        Me.xFNEmployeeLineTotalWorkMin.AppearanceCell.Options.UseTextOptions = True
        Me.xFNEmployeeLineTotalWorkMin.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.xFNEmployeeLineTotalWorkMin.AppearanceHeader.Options.UseTextOptions = True
        Me.xFNEmployeeLineTotalWorkMin.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.xFNEmployeeLineTotalWorkMin.Caption = "xFNEmployeeLineTotalWorkMin"
        Me.xFNEmployeeLineTotalWorkMin.FieldName = "FNEmployeeLineTotalWorkMin"
        Me.xFNEmployeeLineTotalWorkMin.Name = "xFNEmployeeLineTotalWorkMin"
        Me.xFNEmployeeLineTotalWorkMin.OptionsColumn.AllowEdit = False
        Me.xFNEmployeeLineTotalWorkMin.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.xFNEmployeeLineTotalWorkMin.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
        Me.xFNEmployeeLineTotalWorkMin.OptionsColumn.ReadOnly = True
        Me.xFNEmployeeLineTotalWorkMin.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FNAmt_IDL", "{0:n2}")})
        Me.xFNEmployeeLineTotalWorkMin.Visible = True
        Me.xFNEmployeeLineTotalWorkMin.VisibleIndex = 12
        '
        'xFNEmployeeTotalWageAmt
        '
        Me.xFNEmployeeTotalWageAmt.AppearanceCell.Options.UseTextOptions = True
        Me.xFNEmployeeTotalWageAmt.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.xFNEmployeeTotalWageAmt.Caption = "xFNEmployeeTotalWageAmt"
        Me.xFNEmployeeTotalWageAmt.DisplayFormat.FormatString = "{0:n2}"
        Me.xFNEmployeeTotalWageAmt.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.xFNEmployeeTotalWageAmt.FieldName = "FNEmployeeTotalWageAmt"
        Me.xFNEmployeeTotalWageAmt.MinWidth = 25
        Me.xFNEmployeeTotalWageAmt.Name = "xFNEmployeeTotalWageAmt"
        Me.xFNEmployeeTotalWageAmt.OptionsColumn.AllowEdit = False
        Me.xFNEmployeeTotalWageAmt.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.xFNEmployeeTotalWageAmt.OptionsColumn.ReadOnly = True
        Me.xFNEmployeeTotalWageAmt.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FNAmt_MNG", "{0:n2}")})
        Me.xFNEmployeeTotalWageAmt.Visible = True
        Me.xFNEmployeeTotalWageAmt.VisibleIndex = 13
        Me.xFNEmployeeTotalWageAmt.Width = 94
        '
        'xFNEmployeeTotalWageAmtAvgPerMin
        '
        Me.xFNEmployeeTotalWageAmtAvgPerMin.AppearanceCell.Options.UseTextOptions = True
        Me.xFNEmployeeTotalWageAmtAvgPerMin.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.xFNEmployeeTotalWageAmtAvgPerMin.Caption = "xFNEmployeeTotalWageAmtAvgPerMin"
        Me.xFNEmployeeTotalWageAmtAvgPerMin.FieldName = "FNEmployeeTotalWageAmtAvgPerMin"
        Me.xFNEmployeeTotalWageAmtAvgPerMin.MinWidth = 25
        Me.xFNEmployeeTotalWageAmtAvgPerMin.Name = "xFNEmployeeTotalWageAmtAvgPerMin"
        Me.xFNEmployeeTotalWageAmtAvgPerMin.OptionsColumn.AllowEdit = False
        Me.xFNEmployeeTotalWageAmtAvgPerMin.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.xFNEmployeeTotalWageAmtAvgPerMin.OptionsColumn.ReadOnly = True
        Me.xFNEmployeeTotalWageAmtAvgPerMin.Visible = True
        Me.xFNEmployeeTotalWageAmtAvgPerMin.VisibleIndex = 14
        Me.xFNEmployeeTotalWageAmtAvgPerMin.Width = 94
        '
        'xFNJobCostDLEmptypeDaily
        '
        Me.xFNJobCostDLEmptypeDaily.AppearanceCell.Options.UseTextOptions = True
        Me.xFNJobCostDLEmptypeDaily.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.xFNJobCostDLEmptypeDaily.Caption = "xFNJobCostDLEmptypeDaily"
        Me.xFNJobCostDLEmptypeDaily.DisplayFormat.FormatString = "{0:n2}"
        Me.xFNJobCostDLEmptypeDaily.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.xFNJobCostDLEmptypeDaily.FieldName = "FNJobCostDLEmptypeDaily"
        Me.xFNJobCostDLEmptypeDaily.MinWidth = 25
        Me.xFNJobCostDLEmptypeDaily.Name = "xFNJobCostDLEmptypeDaily"
        Me.xFNJobCostDLEmptypeDaily.OptionsColumn.AllowEdit = False
        Me.xFNJobCostDLEmptypeDaily.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[True]
        Me.xFNJobCostDLEmptypeDaily.OptionsColumn.ReadOnly = True
        Me.xFNJobCostDLEmptypeDaily.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FNAmt_Summary", "{0:n2}")})
        Me.xFNJobCostDLEmptypeDaily.Visible = True
        Me.xFNJobCostDLEmptypeDaily.VisibleIndex = 15
        Me.xFNJobCostDLEmptypeDaily.Width = 94
        '
        'xFNEmployeeMonthlyDL
        '
        Me.xFNEmployeeMonthlyDL.Caption = "xFNEmployeeMonthlyDL"
        Me.xFNEmployeeMonthlyDL.FieldName = "FNEmployeeMonthlyDL"
        Me.xFNEmployeeMonthlyDL.MinWidth = 25
        Me.xFNEmployeeMonthlyDL.Name = "xFNEmployeeMonthlyDL"
        Me.xFNEmployeeMonthlyDL.OptionsColumn.AllowEdit = False
        Me.xFNEmployeeMonthlyDL.OptionsColumn.ReadOnly = True
        Me.xFNEmployeeMonthlyDL.Visible = True
        Me.xFNEmployeeMonthlyDL.VisibleIndex = 16
        Me.xFNEmployeeMonthlyDL.Width = 94
        '
        'xFNEmpTypeMonthlyDLAllMinPerDay
        '
        Me.xFNEmpTypeMonthlyDLAllMinPerDay.Caption = "xFNEmpTypeMonthlyDLAllMinPerDay"
        Me.xFNEmpTypeMonthlyDLAllMinPerDay.FieldName = "FNEmpTypeMonthlyDLAllMinPerDay"
        Me.xFNEmpTypeMonthlyDLAllMinPerDay.MinWidth = 25
        Me.xFNEmpTypeMonthlyDLAllMinPerDay.Name = "xFNEmpTypeMonthlyDLAllMinPerDay"
        Me.xFNEmpTypeMonthlyDLAllMinPerDay.OptionsColumn.AllowEdit = False
        Me.xFNEmpTypeMonthlyDLAllMinPerDay.OptionsColumn.ReadOnly = True
        Me.xFNEmpTypeMonthlyDLAllMinPerDay.Visible = True
        Me.xFNEmpTypeMonthlyDLAllMinPerDay.VisibleIndex = 17
        Me.xFNEmpTypeMonthlyDLAllMinPerDay.Width = 94
        '
        'xFNAllJobMinutesActionPerDay
        '
        Me.xFNAllJobMinutesActionPerDay.Caption = "xFNAllJobMinutesActionPerDay"
        Me.xFNAllJobMinutesActionPerDay.FieldName = "FNAllJobMinutesActionPerDay"
        Me.xFNAllJobMinutesActionPerDay.MinWidth = 25
        Me.xFNAllJobMinutesActionPerDay.Name = "xFNAllJobMinutesActionPerDay"
        Me.xFNAllJobMinutesActionPerDay.OptionsColumn.AllowEdit = False
        Me.xFNAllJobMinutesActionPerDay.OptionsColumn.ReadOnly = True
        Me.xFNAllJobMinutesActionPerDay.Visible = True
        Me.xFNAllJobMinutesActionPerDay.VisibleIndex = 18
        Me.xFNAllJobMinutesActionPerDay.Width = 94
        '
        'xFNEmpTypeMonthlyDLAllCostAvgPerMin
        '
        Me.xFNEmpTypeMonthlyDLAllCostAvgPerMin.Caption = "xFNEmpTypeMonthlyDLAllCostAvgPerMin"
        Me.xFNEmpTypeMonthlyDLAllCostAvgPerMin.FieldName = "FNEmpTypeMonthlyDLAllCostAvgPerMin"
        Me.xFNEmpTypeMonthlyDLAllCostAvgPerMin.MinWidth = 25
        Me.xFNEmpTypeMonthlyDLAllCostAvgPerMin.Name = "xFNEmpTypeMonthlyDLAllCostAvgPerMin"
        Me.xFNEmpTypeMonthlyDLAllCostAvgPerMin.OptionsColumn.AllowEdit = False
        Me.xFNEmpTypeMonthlyDLAllCostAvgPerMin.OptionsColumn.ReadOnly = True
        Me.xFNEmpTypeMonthlyDLAllCostAvgPerMin.Visible = True
        Me.xFNEmpTypeMonthlyDLAllCostAvgPerMin.VisibleIndex = 19
        Me.xFNEmpTypeMonthlyDLAllCostAvgPerMin.Width = 94
        '
        'xFNEmpTypeMonthlyDLAllCostPerDayAction
        '
        Me.xFNEmpTypeMonthlyDLAllCostPerDayAction.Caption = "xFNEmpTypeMonthlyDLAllCostPerDayAction"
        Me.xFNEmpTypeMonthlyDLAllCostPerDayAction.FieldName = "FNEmpTypeMonthlyDLAllCostPerDayAction"
        Me.xFNEmpTypeMonthlyDLAllCostPerDayAction.MinWidth = 25
        Me.xFNEmpTypeMonthlyDLAllCostPerDayAction.Name = "xFNEmpTypeMonthlyDLAllCostPerDayAction"
        Me.xFNEmpTypeMonthlyDLAllCostPerDayAction.OptionsColumn.AllowEdit = False
        Me.xFNEmpTypeMonthlyDLAllCostPerDayAction.OptionsColumn.ReadOnly = True
        Me.xFNEmpTypeMonthlyDLAllCostPerDayAction.Visible = True
        Me.xFNEmpTypeMonthlyDLAllCostPerDayAction.VisibleIndex = 20
        Me.xFNEmpTypeMonthlyDLAllCostPerDayAction.Width = 94
        '
        'xFNDLAddOtheTotalAmt
        '
        Me.xFNDLAddOtheTotalAmt.Caption = "xFNDLAddOtheTotalAmt"
        Me.xFNDLAddOtheTotalAmt.FieldName = "FNDLAddOtheTotalAmt"
        Me.xFNDLAddOtheTotalAmt.MinWidth = 25
        Me.xFNDLAddOtheTotalAmt.Name = "xFNDLAddOtheTotalAmt"
        Me.xFNDLAddOtheTotalAmt.OptionsColumn.AllowEdit = False
        Me.xFNDLAddOtheTotalAmt.OptionsColumn.ReadOnly = True
        Me.xFNDLAddOtheTotalAmt.Visible = True
        Me.xFNDLAddOtheTotalAmt.VisibleIndex = 21
        Me.xFNDLAddOtheTotalAmt.Width = 94
        '
        'xFNUniSectTotalWorkMin
        '
        Me.xFNUniSectTotalWorkMin.Caption = "xFNUniSectTotalWorkMin"
        Me.xFNUniSectTotalWorkMin.FieldName = "FNUniSectTotalWorkMin"
        Me.xFNUniSectTotalWorkMin.MinWidth = 25
        Me.xFNUniSectTotalWorkMin.Name = "xFNUniSectTotalWorkMin"
        Me.xFNUniSectTotalWorkMin.OptionsColumn.AllowEdit = False
        Me.xFNUniSectTotalWorkMin.OptionsColumn.ReadOnly = True
        Me.xFNUniSectTotalWorkMin.Visible = True
        Me.xFNUniSectTotalWorkMin.VisibleIndex = 22
        Me.xFNUniSectTotalWorkMin.Width = 94
        '
        'xFNDLAddOtherAmtAvgMin
        '
        Me.xFNDLAddOtherAmtAvgMin.Caption = "xFNDLAddOtherAmtAvgMin"
        Me.xFNDLAddOtherAmtAvgMin.FieldName = "FNDLAddOtherAmtAvgMin"
        Me.xFNDLAddOtherAmtAvgMin.MinWidth = 25
        Me.xFNDLAddOtherAmtAvgMin.Name = "xFNDLAddOtherAmtAvgMin"
        Me.xFNDLAddOtherAmtAvgMin.OptionsColumn.AllowEdit = False
        Me.xFNDLAddOtherAmtAvgMin.OptionsColumn.ReadOnly = True
        Me.xFNDLAddOtherAmtAvgMin.Visible = True
        Me.xFNDLAddOtherAmtAvgMin.VisibleIndex = 23
        Me.xFNDLAddOtherAmtAvgMin.Width = 94
        '
        'xFNDLAddOtherAmt
        '
        Me.xFNDLAddOtherAmt.Caption = "xFNDLAddOtherAmt"
        Me.xFNDLAddOtherAmt.FieldName = "FNDLAddOtherAmt"
        Me.xFNDLAddOtherAmt.MinWidth = 25
        Me.xFNDLAddOtherAmt.Name = "xFNDLAddOtherAmt"
        Me.xFNDLAddOtherAmt.OptionsColumn.AllowEdit = False
        Me.xFNDLAddOtherAmt.OptionsColumn.ReadOnly = True
        Me.xFNDLAddOtherAmt.Visible = True
        Me.xFNDLAddOtherAmt.VisibleIndex = 24
        Me.xFNDLAddOtherAmt.Width = 94
        '
        'xFNMonthlyDLAddOtheTotalAmt
        '
        Me.xFNMonthlyDLAddOtheTotalAmt.Caption = "xFNMonthlyDLAddOtheTotalAmt"
        Me.xFNMonthlyDLAddOtheTotalAmt.FieldName = "FNMonthlyDLAddOtheTotalAmt"
        Me.xFNMonthlyDLAddOtheTotalAmt.MinWidth = 25
        Me.xFNMonthlyDLAddOtheTotalAmt.Name = "xFNMonthlyDLAddOtheTotalAmt"
        Me.xFNMonthlyDLAddOtheTotalAmt.OptionsColumn.AllowEdit = False
        Me.xFNMonthlyDLAddOtheTotalAmt.OptionsColumn.ReadOnly = True
        Me.xFNMonthlyDLAddOtheTotalAmt.Visible = True
        Me.xFNMonthlyDLAddOtheTotalAmt.VisibleIndex = 25
        Me.xFNMonthlyDLAddOtheTotalAmt.Width = 94
        '
        'xFNMonthlyUniSectTotalWorkMin
        '
        Me.xFNMonthlyUniSectTotalWorkMin.Caption = "xFNMonthlyUniSectTotalWorkMin"
        Me.xFNMonthlyUniSectTotalWorkMin.FieldName = "FNMonthlyUniSectTotalWorkMin"
        Me.xFNMonthlyUniSectTotalWorkMin.MinWidth = 25
        Me.xFNMonthlyUniSectTotalWorkMin.Name = "xFNMonthlyUniSectTotalWorkMin"
        Me.xFNMonthlyUniSectTotalWorkMin.OptionsColumn.AllowEdit = False
        Me.xFNMonthlyUniSectTotalWorkMin.OptionsColumn.ReadOnly = True
        Me.xFNMonthlyUniSectTotalWorkMin.Visible = True
        Me.xFNMonthlyUniSectTotalWorkMin.VisibleIndex = 26
        Me.xFNMonthlyUniSectTotalWorkMin.Width = 94
        '
        'xFNMonthlyDLAddOtherAmtAvgMin
        '
        Me.xFNMonthlyDLAddOtherAmtAvgMin.Caption = "xFNMonthlyDLAddOtherAmtAvgMin"
        Me.xFNMonthlyDLAddOtherAmtAvgMin.FieldName = "FNMonthlyDLAddOtherAmtAvgMin"
        Me.xFNMonthlyDLAddOtherAmtAvgMin.MinWidth = 25
        Me.xFNMonthlyDLAddOtherAmtAvgMin.Name = "xFNMonthlyDLAddOtherAmtAvgMin"
        Me.xFNMonthlyDLAddOtherAmtAvgMin.OptionsColumn.AllowEdit = False
        Me.xFNMonthlyDLAddOtherAmtAvgMin.OptionsColumn.ReadOnly = True
        Me.xFNMonthlyDLAddOtherAmtAvgMin.Visible = True
        Me.xFNMonthlyDLAddOtherAmtAvgMin.VisibleIndex = 27
        Me.xFNMonthlyDLAddOtherAmtAvgMin.Width = 94
        '
        'xFNMonthlyDLAddOtherAmt
        '
        Me.xFNMonthlyDLAddOtherAmt.Caption = "xFNMonthlyDLAddOtherAmt"
        Me.xFNMonthlyDLAddOtherAmt.FieldName = "FNMonthlyDLAddOtherAmt"
        Me.xFNMonthlyDLAddOtherAmt.MinWidth = 25
        Me.xFNMonthlyDLAddOtherAmt.Name = "xFNMonthlyDLAddOtherAmt"
        Me.xFNMonthlyDLAddOtherAmt.OptionsColumn.AllowEdit = False
        Me.xFNMonthlyDLAddOtherAmt.OptionsColumn.ReadOnly = True
        Me.xFNMonthlyDLAddOtherAmt.Visible = True
        Me.xFNMonthlyDLAddOtherAmt.VisibleIndex = 28
        Me.xFNMonthlyDLAddOtherAmt.Width = 94
        '
        'RepositoryItemCheckEdit3
        '
        Me.RepositoryItemCheckEdit3.AutoHeight = False
        Me.RepositoryItemCheckEdit3.Caption = "Check"
        Me.RepositoryItemCheckEdit3.Name = "RepositoryItemCheckEdit3"
        Me.RepositoryItemCheckEdit3.ValueChecked = "1"
        Me.RepositoryItemCheckEdit3.ValueUnchecked = "0"
        '
        'RepositoryItemCheckEdit4
        '
        Me.RepositoryItemCheckEdit4.AutoHeight = False
        Me.RepositoryItemCheckEdit4.Caption = "Check"
        Me.RepositoryItemCheckEdit4.Name = "RepositoryItemCheckEdit4"
        Me.RepositoryItemCheckEdit4.ValueChecked = "1"
        Me.RepositoryItemCheckEdit4.ValueUnchecked = "0"
        '
        'wAccountJobCost_CVN
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1452, 602)
        Me.Controls.Add(Me.ogbdetail)
        Me.Controls.Add(Me.ogbheader)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "wAccountJobCost_CVN"
        Me.Text = "wAccountJobCost_CVN"
        CType(Me.DockManager1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbheader.ResumeLayout(False)
        Me.DockPanel1_Container.ResumeLayout(False)
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbmainprocbutton.ResumeLayout(False)
        CType(Me.FNMonth.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTDateStart.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTDateStart.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogbdetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbdetail.ResumeLayout(False)
        CType(Me.XtraTabControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XtraTabControl1.ResumeLayout(False)
        Me.XtraTabPage1.ResumeLayout(False)
        Me.XtraScrollableControl1.ResumeLayout(False)
        CType(Me.ogc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryFTSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryFTApproveState, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XtraTabPage2.ResumeLayout(False)
        Me.XtraScrollableControl2.ResumeLayout(False)
        CType(Me.ogcDtl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.odvDtl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XtraTabPage3.ResumeLayout(False)
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        CType(Me.ogcDtlDaily, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.odvDtlDaily, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents DockManager1 As DevExpress.XtraBars.Docking.DockManager
    Friend WithEvents ogbheader As DevExpress.XtraBars.Docking.DockPanel
    Friend WithEvents DockPanel1_Container As DevExpress.XtraBars.Docking.ControlContainer
    Friend WithEvents ogbdetail As DevExpress.XtraEditors.GroupControl
    Friend WithEvents FTDateStart As DevExpress.XtraEditors.DateEdit
    Friend WithEvents FNMonth As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents FTPayPeriod_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTPayYear_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ogbmainprocbutton As DevExpress.XtraEditors.PanelControl
    Friend WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmclear As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmload As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents XtraTabControl1 As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents XtraTabPage1 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents XtraScrollableControl1 As DevExpress.XtraEditors.XtraScrollableControl
    Friend WithEvents XtraTabPage2 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents ogc As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogv As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents accFTOrder As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents accFTPayYear As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents accFNMonth As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents accFNMinute As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents accFNQty As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents accFNAmt_OverHead As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents accFNAmt_DL As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents accFNAmt_IDL As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents accFNAmt_MNG As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents accFNAmt_None As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryFTSelect As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents RepositoryFTApproveState As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents XtraScrollableControl2 As DevExpress.XtraEditors.XtraScrollableControl
    Friend WithEvents ogcDtl As DevExpress.XtraGrid.GridControl
    Friend WithEvents odvDtl As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents dtlFTOrder As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents dtlFNMinute As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents dtlFNQty As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents dtlFNAmt_OverHead As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents dtlFNAmt_DL As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents dtlFNAmt_IDL As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents dtlFNAmt_MNG As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents dtlFNAmt_None As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemCheckEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents RepositoryItemCheckEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents dtlFTCalDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents accFNAmt_Summary As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents dtlFNAmt_Summary As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents dtlFNCutMinute As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents dtlFNCutQty As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents dtlFNSewMinute As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents dtlFNSewQty As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents dtlFNPackMinute As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents dtlFNPackQty As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents XtraTabPage3 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents ogcDtlDaily As DevExpress.XtraGrid.GridControl
    Friend WithEvents odvDtlDaily As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents xFTCalDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents xFTOrder As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents xFTSubOrderNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents xFNSeq As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents xFTStartTime As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents xFTEndTime As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents xFNQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents xFNTotalMinute As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents xFTAction As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents xFNEmployeeInLine As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents xFNTotalLateMin As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents xFNEmployeeTotalWorkMin As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents xFNEmployeeLineTotalWorkMin As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents xFNEmployeeTotalWageAmt As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents xFNEmployeeTotalWageAmtAvgPerMin As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents xFNJobCostDLEmptypeDaily As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemCheckEdit3 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents RepositoryItemCheckEdit4 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents xFTUnitSectCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents xFNEmployeeMonthlyDL As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents xFNEmpTypeMonthlyDLAllMinPerDay As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents xFNAllJobMinutesActionPerDay As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents xFNEmpTypeMonthlyDLAllCostAvgPerMin As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents xFNEmpTypeMonthlyDLAllCostPerDayAction As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents xFNDLAddOtheTotalAmt As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents xFNUniSectTotalWorkMin As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents xFNDLAddOtherAmtAvgMin As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents xFNDLAddOtherAmt As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents xFNMonthlyDLAddOtheTotalAmt As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents xFNMonthlyUniSectTotalWorkMin As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents xFNMonthlyDLAddOtherAmtAvgMin As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents xFNMonthlyDLAddOtherAmt As DevExpress.XtraGrid.Columns.GridColumn
End Class

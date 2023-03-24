<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wLeaveApproved
    Inherits DevExpress.XtraBars.Ribbon.RibbonForm

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
        Me.MainRibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.mnusysabout = New DevExpress.XtraBars.BarButtonItem()
        Me.FTUserLogINIP = New DevExpress.XtraBars.BarStaticItem()
        Me.RepositoryItemComboBox1 = New DevExpress.XtraEditors.Repository.RepositoryItemComboBox()
        Me.RibbonStatusBar = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.MainDefaultLookAndFeel = New DevExpress.LookAndFeel.DefaultLookAndFeel(Me.components)
        Me.oDockManager = New DevExpress.XtraBars.Docking.DockManager(Me.components)
        Me.StandaloneBarDockControl = New DevExpress.XtraBars.StandaloneBarDockControl()
        Me.otmchkpo = New System.Windows.Forms.Timer(Me.components)
        Me.ogbmainprocbutton = New DevExpress.XtraEditors.PanelControl()
        Me.ocmpreview = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmsavelayout = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmreject = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmapprove = New DevExpress.XtraEditors.SimpleButton()
        Me.otpappcminv = New DevExpress.XtraTab.XtraTabPage()
        Me.ogbapproveleave = New DevExpress.XtraEditors.GroupControl()
        Me.ogcleaveapp = New DevExpress.XtraGrid.GridControl()
        Me.ogvapproveleave = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.FNHSysEmpID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTSelect = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemFTSelect = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.cFTEmpName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColFTStartDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColFTEndDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNSumTotalLeaveDay = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTLeaveType = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTLeaveTypeName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTStaLeaveDayName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColFTHoliday = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepColFTHoliday = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.FTLeavePay = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepFTLeavePay = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.FTLeaveStartTime = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTLeaveEndTime = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNLeaveTotalTime = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNLeaveTotalDay = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTStaCalSSO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepFTStaCalSSO = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.FTStaLeaveDay = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTLeaveNote = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColFTStateMedicalCertificate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColFTMedicalCertificateName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFTInsUser = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFTInsDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFTInsTime = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFTStateDeductVacation = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepFTApproveState = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.cFTStateType = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNLeaveTotalTimeMin = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTMngApproveState = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCFTMngApproveState = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.FTMngApproveBy = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTMngApproveTime = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FDMngApproveDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTApproveState = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.SickLeave = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.BusinessLeave = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.VacationLeave = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTEmpTypeName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTSectName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTUnitSectName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTHoliday = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryFTSelect = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.RepositoryFTApproveState = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.RepColFTLeavePay = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.FTSelectAllleave = New DevExpress.XtraEditors.CheckEdit()
        Me.otbmain = New DevExpress.XtraTab.XtraTabControl()
        CType(Me.MainRibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemComboBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.oDockManager, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbmainprocbutton.SuspendLayout()
        Me.otpappcminv.SuspendLayout()
        CType(Me.ogbapproveleave, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbapproveleave.SuspendLayout()
        CType(Me.ogcleaveapp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvapproveleave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemFTSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepColFTHoliday, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepFTLeavePay, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepFTStaCalSSO, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepFTApproveState, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCFTMngApproveState, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryFTSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryFTApproveState, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepColFTLeavePay, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTSelectAllleave.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.otbmain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.otbmain.SuspendLayout()
        Me.SuspendLayout()
        '
        'MainRibbonControl
        '
        Me.MainRibbonControl.ApplicationButtonText = Nothing
        Me.MainRibbonControl.ExpandCollapseItem.Id = 0
        Me.MainRibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.MainRibbonControl.ExpandCollapseItem, Me.MainRibbonControl.SearchEditItem, Me.mnusysabout, Me.FTUserLogINIP})
        Me.MainRibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.MainRibbonControl.Margin = New System.Windows.Forms.Padding(6, 5, 6, 5)
        Me.MainRibbonControl.MaxItemId = 5
        Me.MainRibbonControl.Name = "MainRibbonControl"
        Me.MainRibbonControl.OptionsPageCategories.Alignment = DevExpress.XtraBars.Ribbon.RibbonPageCategoryAlignment.Left
        Me.MainRibbonControl.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemComboBox1})
        Me.MainRibbonControl.ShowExpandCollapseButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.MainRibbonControl.ShowToolbarCustomizeItem = False
        Me.MainRibbonControl.Size = New System.Drawing.Size(1478, 66)
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
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 710)
        Me.RibbonStatusBar.Margin = New System.Windows.Forms.Padding(6, 5, 6, 5)
        Me.RibbonStatusBar.Name = "RibbonStatusBar"
        Me.RibbonStatusBar.Ribbon = Me.MainRibbonControl
        Me.RibbonStatusBar.Size = New System.Drawing.Size(1478, 36)
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
        Me.StandaloneBarDockControl.Location = New System.Drawing.Point(0, 66)
        Me.StandaloneBarDockControl.Manager = Nothing
        Me.StandaloneBarDockControl.Margin = New System.Windows.Forms.Padding(4)
        Me.StandaloneBarDockControl.Name = "StandaloneBarDockControl"
        Me.StandaloneBarDockControl.Size = New System.Drawing.Size(1478, 0)
        Me.StandaloneBarDockControl.Text = "StandaloneBarDockControl1"
        '
        'otmchkpo
        '
        Me.otmchkpo.Interval = 600000
        '
        'ogbmainprocbutton
        '
        Me.ogbmainprocbutton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmpreview)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmsavelayout)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmexit)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmreject)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmapprove)
        Me.ogbmainprocbutton.Location = New System.Drawing.Point(551, 38)
        Me.ogbmainprocbutton.Margin = New System.Windows.Forms.Padding(4)
        Me.ogbmainprocbutton.Name = "ogbmainprocbutton"
        Me.ogbmainprocbutton.Size = New System.Drawing.Size(802, 92)
        Me.ogbmainprocbutton.TabIndex = 138
        Me.ogbmainprocbutton.Tag = "2|"
        '
        'ocmpreview
        '
        Me.ocmpreview.Location = New System.Drawing.Point(346, 31)
        Me.ocmpreview.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmpreview.Name = "ocmpreview"
        Me.ocmpreview.Size = New System.Drawing.Size(111, 31)
        Me.ocmpreview.TabIndex = 333
        Me.ocmpreview.TabStop = False
        Me.ocmpreview.Tag = "2|"
        Me.ocmpreview.Text = "PREVIEW"
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
        'otpappcminv
        '
        Me.otpappcminv.Controls.Add(Me.ogbapproveleave)
        Me.otpappcminv.Margin = New System.Windows.Forms.Padding(4)
        Me.otpappcminv.Name = "otpappcminv"
        Me.otpappcminv.Size = New System.Drawing.Size(1468, 608)
        Me.otpappcminv.Text = "Approve Documentation"
        '
        'ogbapproveleave
        '
        Me.ogbapproveleave.AppearanceCaption.Options.UseTextOptions = True
        Me.ogbapproveleave.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ogbapproveleave.Controls.Add(Me.ogcleaveapp)
        Me.ogbapproveleave.Controls.Add(Me.FTSelectAllleave)
        Me.ogbapproveleave.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogbapproveleave.Location = New System.Drawing.Point(0, 0)
        Me.ogbapproveleave.Margin = New System.Windows.Forms.Padding(4)
        Me.ogbapproveleave.Name = "ogbapproveleave"
        Me.ogbapproveleave.Size = New System.Drawing.Size(1468, 608)
        Me.ogbapproveleave.TabIndex = 12
        Me.ogbapproveleave.Text = "Approved  Leave"
        '
        'ogcleaveapp
        '
        Me.ogcleaveapp.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogcleaveapp.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcleaveapp.Location = New System.Drawing.Point(2, 27)
        Me.ogcleaveapp.MainView = Me.ogvapproveleave
        Me.ogcleaveapp.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcleaveapp.Name = "ogcleaveapp"
        Me.ogcleaveapp.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryFTSelect, Me.RepositoryFTApproveState, Me.RepColFTLeavePay, Me.RepColFTHoliday, Me.RepFTStaCalSSO, Me.RepFTApproveState, Me.RepFTLeavePay, Me.RepositoryItemFTSelect, Me.RepositoryItemCFTMngApproveState})
        Me.ogcleaveapp.Size = New System.Drawing.Size(1464, 579)
        Me.ogcleaveapp.TabIndex = 4
        Me.ogcleaveapp.TabStop = False
        Me.ogcleaveapp.Tag = "2|"
        Me.ogcleaveapp.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvapproveleave, Me.GridView1})
        '
        'ogvapproveleave
        '
        Me.ogvapproveleave.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.FNHSysEmpID, Me.cFTSelect, Me.cFTEmpName, Me.ColFTStartDate, Me.ColFTEndDate, Me.cFNSumTotalLeaveDay, Me.FTLeaveType, Me.FTLeaveTypeName, Me.FTStaLeaveDayName, Me.ColFTHoliday, Me.FTLeavePay, Me.FTLeaveStartTime, Me.FTLeaveEndTime, Me.FNLeaveTotalTime, Me.FNLeaveTotalDay, Me.FTStaCalSSO, Me.FTStaLeaveDay, Me.FTLeaveNote, Me.ColFTStateMedicalCertificate, Me.ColFTMedicalCertificateName, Me.CFTInsUser, Me.CFTInsDate, Me.CFTInsTime, Me.CFTStateDeductVacation, Me.cFTStateType, Me.cFNLeaveTotalTimeMin, Me.FTMngApproveState, Me.FTMngApproveBy, Me.FTMngApproveTime, Me.FDMngApproveDate, Me.FTApproveState, Me.SickLeave, Me.BusinessLeave, Me.VacationLeave, Me.FTEmpTypeName, Me.FTSectName, Me.FTUnitSectName, Me.FTHoliday})
        Me.ogvapproveleave.GridControl = Me.ogcleaveapp
        Me.ogvapproveleave.Name = "ogvapproveleave"
        Me.ogvapproveleave.OptionsCustomization.AllowGroup = False
        Me.ogvapproveleave.OptionsCustomization.AllowQuickHideColumns = False
        Me.ogvapproveleave.OptionsView.ColumnAutoWidth = False
        Me.ogvapproveleave.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.ogvapproveleave.OptionsView.ShowGroupPanel = False
        Me.ogvapproveleave.Tag = "2|"
        '
        'FNHSysEmpID
        '
        Me.FNHSysEmpID.Caption = "FNHSysEmpID"
        Me.FNHSysEmpID.FieldName = "FNHSysEmpID"
        Me.FNHSysEmpID.Name = "FNHSysEmpID"
        Me.FNHSysEmpID.OptionsColumn.AllowEdit = False
        Me.FNHSysEmpID.Width = 104
        '
        'cFTSelect
        '
        Me.cFTSelect.Caption = "FTSelect"
        Me.cFTSelect.ColumnEdit = Me.RepositoryItemFTSelect
        Me.cFTSelect.FieldName = "FTSelect"
        Me.cFTSelect.Name = "cFTSelect"
        Me.cFTSelect.Visible = True
        Me.cFTSelect.VisibleIndex = 0
        Me.cFTSelect.Width = 40
        '
        'RepositoryItemFTSelect
        '
        Me.RepositoryItemFTSelect.AutoHeight = False
        Me.RepositoryItemFTSelect.Name = "RepositoryItemFTSelect"
        Me.RepositoryItemFTSelect.ValueChecked = "1"
        Me.RepositoryItemFTSelect.ValueUnchecked = "0"
        '
        'cFTEmpName
        '
        Me.cFTEmpName.Caption = "FTEmpName"
        Me.cFTEmpName.FieldName = "FTEmpName"
        Me.cFTEmpName.Name = "cFTEmpName"
        Me.cFTEmpName.OptionsColumn.AllowEdit = False
        Me.cFTEmpName.Visible = True
        Me.cFTEmpName.VisibleIndex = 1
        Me.cFTEmpName.Width = 227
        '
        'ColFTStartDate
        '
        Me.ColFTStartDate.AppearanceCell.Options.UseTextOptions = True
        Me.ColFTStartDate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ColFTStartDate.AppearanceHeader.Options.UseTextOptions = True
        Me.ColFTStartDate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ColFTStartDate.Caption = "FTStartDate"
        Me.ColFTStartDate.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.ColFTStartDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.ColFTStartDate.FieldName = "FTStartDate"
        Me.ColFTStartDate.Name = "ColFTStartDate"
        Me.ColFTStartDate.OptionsColumn.AllowEdit = False
        Me.ColFTStartDate.OptionsColumn.AllowMove = False
        Me.ColFTStartDate.OptionsColumn.ReadOnly = True
        Me.ColFTStartDate.Visible = True
        Me.ColFTStartDate.VisibleIndex = 2
        Me.ColFTStartDate.Width = 107
        '
        'ColFTEndDate
        '
        Me.ColFTEndDate.AppearanceCell.Options.UseTextOptions = True
        Me.ColFTEndDate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ColFTEndDate.AppearanceHeader.Options.UseTextOptions = True
        Me.ColFTEndDate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ColFTEndDate.Caption = "FTEndDate"
        Me.ColFTEndDate.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.ColFTEndDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.ColFTEndDate.FieldName = "FTEndDate"
        Me.ColFTEndDate.Name = "ColFTEndDate"
        Me.ColFTEndDate.OptionsColumn.AllowEdit = False
        Me.ColFTEndDate.OptionsColumn.AllowMove = False
        Me.ColFTEndDate.OptionsColumn.ReadOnly = True
        Me.ColFTEndDate.Visible = True
        Me.ColFTEndDate.VisibleIndex = 3
        Me.ColFTEndDate.Width = 114
        '
        'cFNSumTotalLeaveDay
        '
        Me.cFNSumTotalLeaveDay.Caption = "FNSumTotalLeaveDay"
        Me.cFNSumTotalLeaveDay.FieldName = "FNSumTotalLeaveDay"
        Me.cFNSumTotalLeaveDay.Name = "cFNSumTotalLeaveDay"
        Me.cFNSumTotalLeaveDay.OptionsColumn.AllowEdit = False
        Me.cFNSumTotalLeaveDay.Visible = True
        Me.cFNSumTotalLeaveDay.VisibleIndex = 5
        Me.cFNSumTotalLeaveDay.Width = 152
        '
        'FTLeaveType
        '
        Me.FTLeaveType.Caption = "FTLeaveType"
        Me.FTLeaveType.FieldName = "FTLeaveType"
        Me.FTLeaveType.Name = "FTLeaveType"
        Me.FTLeaveType.OptionsColumn.AllowEdit = False
        Me.FTLeaveType.OptionsColumn.AllowMove = False
        Me.FTLeaveType.OptionsColumn.ReadOnly = True
        '
        'FTLeaveTypeName
        '
        Me.FTLeaveTypeName.AppearanceHeader.Options.UseTextOptions = True
        Me.FTLeaveTypeName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTLeaveTypeName.Caption = "FTLeaveTypeName"
        Me.FTLeaveTypeName.FieldName = "FTLeaveTypeName"
        Me.FTLeaveTypeName.Name = "FTLeaveTypeName"
        Me.FTLeaveTypeName.OptionsColumn.AllowEdit = False
        Me.FTLeaveTypeName.OptionsColumn.AllowMove = False
        Me.FTLeaveTypeName.OptionsColumn.ReadOnly = True
        Me.FTLeaveTypeName.Visible = True
        Me.FTLeaveTypeName.VisibleIndex = 4
        Me.FTLeaveTypeName.Width = 151
        '
        'FTStaLeaveDayName
        '
        Me.FTStaLeaveDayName.AppearanceHeader.Options.UseTextOptions = True
        Me.FTStaLeaveDayName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTStaLeaveDayName.Caption = "FTStaLeaveDayName"
        Me.FTStaLeaveDayName.FieldName = "FTStaLeaveDayName"
        Me.FTStaLeaveDayName.Name = "FTStaLeaveDayName"
        Me.FTStaLeaveDayName.OptionsColumn.AllowEdit = False
        Me.FTStaLeaveDayName.OptionsColumn.AllowMove = False
        Me.FTStaLeaveDayName.OptionsColumn.ReadOnly = True
        Me.FTStaLeaveDayName.Visible = True
        Me.FTStaLeaveDayName.VisibleIndex = 6
        Me.FTStaLeaveDayName.Width = 168
        '
        'ColFTHoliday
        '
        Me.ColFTHoliday.AppearanceCell.Options.UseTextOptions = True
        Me.ColFTHoliday.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ColFTHoliday.AppearanceHeader.Options.UseTextOptions = True
        Me.ColFTHoliday.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ColFTHoliday.Caption = "ไม่รวมวันหยุด"
        Me.ColFTHoliday.ColumnEdit = Me.RepColFTHoliday
        Me.ColFTHoliday.FieldName = "FTHoliday"
        Me.ColFTHoliday.Name = "ColFTHoliday"
        Me.ColFTHoliday.OptionsColumn.AllowEdit = False
        Me.ColFTHoliday.OptionsColumn.AllowMove = False
        Me.ColFTHoliday.OptionsColumn.ReadOnly = True
        Me.ColFTHoliday.Visible = True
        Me.ColFTHoliday.VisibleIndex = 7
        Me.ColFTHoliday.Width = 100
        '
        'RepColFTHoliday
        '
        Me.RepColFTHoliday.AutoHeight = False
        Me.RepColFTHoliday.Caption = "Check"
        Me.RepColFTHoliday.Name = "RepColFTHoliday"
        Me.RepColFTHoliday.ValueChecked = "1"
        Me.RepColFTHoliday.ValueUnchecked = "0"
        '
        'FTLeavePay
        '
        Me.FTLeavePay.AppearanceCell.Options.UseTextOptions = True
        Me.FTLeavePay.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTLeavePay.AppearanceHeader.Options.UseTextOptions = True
        Me.FTLeavePay.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTLeavePay.Caption = "ลาจ่าย"
        Me.FTLeavePay.ColumnEdit = Me.RepFTLeavePay
        Me.FTLeavePay.FieldName = "FTLeavePay"
        Me.FTLeavePay.Name = "FTLeavePay"
        Me.FTLeavePay.OptionsColumn.AllowEdit = False
        Me.FTLeavePay.OptionsColumn.AllowMove = False
        Me.FTLeavePay.OptionsColumn.ReadOnly = True
        Me.FTLeavePay.Visible = True
        Me.FTLeavePay.VisibleIndex = 8
        Me.FTLeavePay.Width = 72
        '
        'RepFTLeavePay
        '
        Me.RepFTLeavePay.AutoHeight = False
        Me.RepFTLeavePay.Caption = "Check"
        Me.RepFTLeavePay.Name = "RepFTLeavePay"
        Me.RepFTLeavePay.ValueChecked = "1"
        Me.RepFTLeavePay.ValueUnchecked = "0"
        '
        'FTLeaveStartTime
        '
        Me.FTLeaveStartTime.AppearanceCell.Options.UseTextOptions = True
        Me.FTLeaveStartTime.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTLeaveStartTime.AppearanceHeader.Options.UseTextOptions = True
        Me.FTLeaveStartTime.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTLeaveStartTime.Caption = "เาลา"
        Me.FTLeaveStartTime.FieldName = "FTLeaveStartTime"
        Me.FTLeaveStartTime.Name = "FTLeaveStartTime"
        Me.FTLeaveStartTime.OptionsColumn.AllowEdit = False
        Me.FTLeaveStartTime.OptionsColumn.AllowMove = False
        Me.FTLeaveStartTime.OptionsColumn.ReadOnly = True
        Me.FTLeaveStartTime.Visible = True
        Me.FTLeaveStartTime.VisibleIndex = 9
        Me.FTLeaveStartTime.Width = 77
        '
        'FTLeaveEndTime
        '
        Me.FTLeaveEndTime.AppearanceCell.Options.UseTextOptions = True
        Me.FTLeaveEndTime.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTLeaveEndTime.AppearanceHeader.Options.UseTextOptions = True
        Me.FTLeaveEndTime.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTLeaveEndTime.Caption = "ถึงเวลา"
        Me.FTLeaveEndTime.FieldName = "FTLeaveEndTime"
        Me.FTLeaveEndTime.Name = "FTLeaveEndTime"
        Me.FTLeaveEndTime.OptionsColumn.AllowEdit = False
        Me.FTLeaveEndTime.OptionsColumn.AllowMove = False
        Me.FTLeaveEndTime.OptionsColumn.ReadOnly = True
        Me.FTLeaveEndTime.Visible = True
        Me.FTLeaveEndTime.VisibleIndex = 10
        Me.FTLeaveEndTime.Width = 74
        '
        'FNLeaveTotalTime
        '
        Me.FNLeaveTotalTime.AppearanceCell.Options.UseTextOptions = True
        Me.FNLeaveTotalTime.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNLeaveTotalTime.AppearanceHeader.Options.UseTextOptions = True
        Me.FNLeaveTotalTime.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FNLeaveTotalTime.Caption = "รวม ชม./วัน"
        Me.FNLeaveTotalTime.FieldName = "FNLeaveTotalTime"
        Me.FNLeaveTotalTime.Name = "FNLeaveTotalTime"
        Me.FNLeaveTotalTime.OptionsColumn.AllowEdit = False
        Me.FNLeaveTotalTime.OptionsColumn.AllowMove = False
        Me.FNLeaveTotalTime.OptionsColumn.ReadOnly = True
        Me.FNLeaveTotalTime.Visible = True
        Me.FNLeaveTotalTime.VisibleIndex = 11
        Me.FNLeaveTotalTime.Width = 98
        '
        'FNLeaveTotalDay
        '
        Me.FNLeaveTotalDay.AppearanceCell.Options.UseTextOptions = True
        Me.FNLeaveTotalDay.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNLeaveTotalDay.AppearanceHeader.Options.UseTextOptions = True
        Me.FNLeaveTotalDay.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FNLeaveTotalDay.Caption = "จำนวนวัน"
        Me.FNLeaveTotalDay.FieldName = "FNLeaveTotalDay"
        Me.FNLeaveTotalDay.Name = "FNLeaveTotalDay"
        Me.FNLeaveTotalDay.OptionsColumn.AllowEdit = False
        Me.FNLeaveTotalDay.OptionsColumn.AllowMove = False
        Me.FNLeaveTotalDay.OptionsColumn.ReadOnly = True
        Me.FNLeaveTotalDay.Visible = True
        Me.FNLeaveTotalDay.VisibleIndex = 12
        Me.FNLeaveTotalDay.Width = 78
        '
        'FTStaCalSSO
        '
        Me.FTStaCalSSO.AppearanceCell.Options.UseTextOptions = True
        Me.FTStaCalSSO.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTStaCalSSO.AppearanceHeader.Options.UseTextOptions = True
        Me.FTStaCalSSO.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTStaCalSSO.Caption = "ประกันสังคม"
        Me.FTStaCalSSO.ColumnEdit = Me.RepFTStaCalSSO
        Me.FTStaCalSSO.FieldName = "FTStaCalSSO"
        Me.FTStaCalSSO.Name = "FTStaCalSSO"
        Me.FTStaCalSSO.OptionsColumn.AllowEdit = False
        Me.FTStaCalSSO.OptionsColumn.AllowMove = False
        Me.FTStaCalSSO.OptionsColumn.ReadOnly = True
        Me.FTStaCalSSO.Visible = True
        Me.FTStaCalSSO.VisibleIndex = 13
        Me.FTStaCalSSO.Width = 103
        '
        'RepFTStaCalSSO
        '
        Me.RepFTStaCalSSO.AutoHeight = False
        Me.RepFTStaCalSSO.Caption = "Check"
        Me.RepFTStaCalSSO.Name = "RepFTStaCalSSO"
        Me.RepFTStaCalSSO.ValueChecked = "1"
        Me.RepFTStaCalSSO.ValueUnchecked = "0"
        '
        'FTStaLeaveDay
        '
        Me.FTStaLeaveDay.Caption = "FTStaLeaveDay"
        Me.FTStaLeaveDay.FieldName = "FTStaLeaveDay"
        Me.FTStaLeaveDay.Name = "FTStaLeaveDay"
        Me.FTStaLeaveDay.OptionsColumn.AllowEdit = False
        Me.FTStaLeaveDay.OptionsColumn.AllowMove = False
        Me.FTStaLeaveDay.OptionsColumn.ReadOnly = True
        '
        'FTLeaveNote
        '
        Me.FTLeaveNote.Caption = "FTLeaveNote"
        Me.FTLeaveNote.FieldName = "FTLeaveNote"
        Me.FTLeaveNote.Name = "FTLeaveNote"
        '
        'ColFTStateMedicalCertificate
        '
        Me.ColFTStateMedicalCertificate.Caption = "FTStateMedicalCertificate"
        Me.ColFTStateMedicalCertificate.ColumnEdit = Me.RepColFTHoliday
        Me.ColFTStateMedicalCertificate.FieldName = "FTStateMedicalCertificate"
        Me.ColFTStateMedicalCertificate.Name = "ColFTStateMedicalCertificate"
        Me.ColFTStateMedicalCertificate.OptionsColumn.AllowEdit = False
        Me.ColFTStateMedicalCertificate.OptionsColumn.ReadOnly = True
        Me.ColFTStateMedicalCertificate.Visible = True
        Me.ColFTStateMedicalCertificate.VisibleIndex = 14
        Me.ColFTStateMedicalCertificate.Width = 97
        '
        'ColFTMedicalCertificateName
        '
        Me.ColFTMedicalCertificateName.Caption = "FTMedicalCertificateName"
        Me.ColFTMedicalCertificateName.FieldName = "FTMedicalCertificateName"
        Me.ColFTMedicalCertificateName.Name = "ColFTMedicalCertificateName"
        '
        'CFTInsUser
        '
        Me.CFTInsUser.Caption = "User Create"
        Me.CFTInsUser.FieldName = "FTInsUser"
        Me.CFTInsUser.Name = "CFTInsUser"
        Me.CFTInsUser.OptionsColumn.AllowEdit = False
        Me.CFTInsUser.OptionsColumn.ReadOnly = True
        '
        'CFTInsDate
        '
        Me.CFTInsDate.Caption = "Create Date"
        Me.CFTInsDate.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.CFTInsDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.CFTInsDate.FieldName = "FTInsDate"
        Me.CFTInsDate.Name = "CFTInsDate"
        Me.CFTInsDate.OptionsColumn.AllowEdit = False
        Me.CFTInsDate.OptionsColumn.ReadOnly = True
        '
        'CFTInsTime
        '
        Me.CFTInsTime.Caption = "Time"
        Me.CFTInsTime.FieldName = "FTInsTime"
        Me.CFTInsTime.Name = "CFTInsTime"
        Me.CFTInsTime.OptionsColumn.AllowEdit = False
        Me.CFTInsTime.OptionsColumn.ReadOnly = True
        '
        'CFTStateDeductVacation
        '
        Me.CFTStateDeductVacation.Caption = "หักลาพักร้อน"
        Me.CFTStateDeductVacation.ColumnEdit = Me.RepFTApproveState
        Me.CFTStateDeductVacation.FieldName = "FTStateDeductVacation"
        Me.CFTStateDeductVacation.Name = "CFTStateDeductVacation"
        Me.CFTStateDeductVacation.OptionsColumn.AllowEdit = False
        Me.CFTStateDeductVacation.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFTStateDeductVacation.OptionsColumn.AllowShowHide = False
        Me.CFTStateDeductVacation.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFTStateDeductVacation.OptionsColumn.ReadOnly = True
        Me.CFTStateDeductVacation.Visible = True
        Me.CFTStateDeductVacation.VisibleIndex = 15
        Me.CFTStateDeductVacation.Width = 96
        '
        'RepFTApproveState
        '
        Me.RepFTApproveState.AutoHeight = False
        Me.RepFTApproveState.Caption = "Check"
        Me.RepFTApproveState.Name = "RepFTApproveState"
        Me.RepFTApproveState.ValueChecked = "1"
        Me.RepFTApproveState.ValueUnchecked = "0"
        '
        'cFTStateType
        '
        Me.cFTStateType.Caption = "FTStateType"
        Me.cFTStateType.FieldName = "FTStateType"
        Me.cFTStateType.Name = "cFTStateType"
        Me.cFTStateType.OptionsColumn.AllowEdit = False
        '
        'cFNLeaveTotalTimeMin
        '
        Me.cFNLeaveTotalTimeMin.AppearanceCell.Options.UseTextOptions = True
        Me.cFNLeaveTotalTimeMin.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.cFNLeaveTotalTimeMin.Caption = "FNLeaveTotalTimeMin"
        Me.cFNLeaveTotalTimeMin.FieldName = "FNLeaveTotalTimeMin"
        Me.cFNLeaveTotalTimeMin.Name = "cFNLeaveTotalTimeMin"
        '
        'FTMngApproveState
        '
        Me.FTMngApproveState.Caption = "FTMngApproveState"
        Me.FTMngApproveState.ColumnEdit = Me.RepositoryItemCFTMngApproveState
        Me.FTMngApproveState.FieldName = "FTMngApproveState"
        Me.FTMngApproveState.Name = "FTMngApproveState"
        Me.FTMngApproveState.OptionsColumn.AllowEdit = False
        Me.FTMngApproveState.Visible = True
        Me.FTMngApproveState.VisibleIndex = 16
        Me.FTMngApproveState.Width = 109
        '
        'RepositoryItemCFTMngApproveState
        '
        Me.RepositoryItemCFTMngApproveState.AutoHeight = False
        Me.RepositoryItemCFTMngApproveState.Name = "RepositoryItemCFTMngApproveState"
        Me.RepositoryItemCFTMngApproveState.ValueChecked = "1"
        Me.RepositoryItemCFTMngApproveState.ValueUnchecked = "0"
        '
        'FTMngApproveBy
        '
        Me.FTMngApproveBy.Caption = "FTMngApproveBy"
        Me.FTMngApproveBy.FieldName = "FTMngApproveBy"
        Me.FTMngApproveBy.Name = "FTMngApproveBy"
        Me.FTMngApproveBy.OptionsColumn.AllowEdit = False
        Me.FTMngApproveBy.Visible = True
        Me.FTMngApproveBy.VisibleIndex = 17
        Me.FTMngApproveBy.Width = 92
        '
        'FTMngApproveTime
        '
        Me.FTMngApproveTime.Caption = "FTMngApproveTime"
        Me.FTMngApproveTime.FieldName = "FTMngApproveTime"
        Me.FTMngApproveTime.Name = "FTMngApproveTime"
        Me.FTMngApproveTime.OptionsColumn.AllowEdit = False
        Me.FTMngApproveTime.Visible = True
        Me.FTMngApproveTime.VisibleIndex = 18
        Me.FTMngApproveTime.Width = 94
        '
        'FDMngApproveDate
        '
        Me.FDMngApproveDate.Caption = "FDMngApproveDate"
        Me.FDMngApproveDate.FieldName = "FDMngApproveDate"
        Me.FDMngApproveDate.Name = "FDMngApproveDate"
        Me.FDMngApproveDate.OptionsColumn.AllowEdit = False
        Me.FDMngApproveDate.Visible = True
        Me.FDMngApproveDate.VisibleIndex = 19
        '
        'FTApproveState
        '
        Me.FTApproveState.AppearanceHeader.Options.UseTextOptions = True
        Me.FTApproveState.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTApproveState.Caption = "อนุมัติ"
        Me.FTApproveState.ColumnEdit = Me.RepFTApproveState
        Me.FTApproveState.FieldName = "FTApproveState"
        Me.FTApproveState.Name = "FTApproveState"
        Me.FTApproveState.OptionsColumn.AllowEdit = False
        Me.FTApproveState.OptionsColumn.AllowMove = False
        Me.FTApproveState.OptionsColumn.ReadOnly = True
        Me.FTApproveState.Width = 64
        '
        'SickLeave
        '
        Me.SickLeave.AppearanceCell.Options.UseTextOptions = True
        Me.SickLeave.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.SickLeave.Caption = "SickLeave"
        Me.SickLeave.FieldName = "SickLeave"
        Me.SickLeave.Name = "SickLeave"
        Me.SickLeave.OptionsColumn.AllowEdit = False
        Me.SickLeave.Visible = True
        Me.SickLeave.VisibleIndex = 20
        '
        'BusinessLeave
        '
        Me.BusinessLeave.AppearanceCell.Options.UseTextOptions = True
        Me.BusinessLeave.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.BusinessLeave.Caption = "BusinessLeave"
        Me.BusinessLeave.FieldName = "BusinessLeave"
        Me.BusinessLeave.Name = "BusinessLeave"
        Me.BusinessLeave.OptionsColumn.AllowEdit = False
        Me.BusinessLeave.OptionsColumn.ReadOnly = True
        Me.BusinessLeave.Visible = True
        Me.BusinessLeave.VisibleIndex = 21
        '
        'VacationLeave
        '
        Me.VacationLeave.AppearanceCell.Options.UseTextOptions = True
        Me.VacationLeave.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.VacationLeave.Caption = "VacationLeave"
        Me.VacationLeave.FieldName = "VacationLeave"
        Me.VacationLeave.Name = "VacationLeave"
        Me.VacationLeave.OptionsColumn.AllowEdit = False
        Me.VacationLeave.OptionsColumn.ReadOnly = True
        Me.VacationLeave.Visible = True
        Me.VacationLeave.VisibleIndex = 22
        '
        'FTEmpTypeName
        '
        Me.FTEmpTypeName.Caption = "FTEmpTypeName"
        Me.FTEmpTypeName.FieldName = "FTEmpTypeName"
        Me.FTEmpTypeName.Name = "FTEmpTypeName"
        Me.FTEmpTypeName.OptionsColumn.AllowEdit = False
        Me.FTEmpTypeName.OptionsColumn.ReadOnly = True
        Me.FTEmpTypeName.Visible = True
        Me.FTEmpTypeName.VisibleIndex = 23
        Me.FTEmpTypeName.Width = 100
        '
        'FTSectName
        '
        Me.FTSectName.Caption = "FTSectName"
        Me.FTSectName.FieldName = "FTSectName"
        Me.FTSectName.Name = "FTSectName"
        Me.FTSectName.OptionsColumn.AllowEdit = False
        Me.FTSectName.OptionsColumn.ReadOnly = True
        Me.FTSectName.Visible = True
        Me.FTSectName.VisibleIndex = 24
        Me.FTSectName.Width = 100
        '
        'FTUnitSectName
        '
        Me.FTUnitSectName.Caption = "FTUnitSectName"
        Me.FTUnitSectName.FieldName = "FTUnitSectName"
        Me.FTUnitSectName.Name = "FTUnitSectName"
        Me.FTUnitSectName.OptionsColumn.AllowEdit = False
        Me.FTUnitSectName.OptionsColumn.ReadOnly = True
        Me.FTUnitSectName.Visible = True
        Me.FTUnitSectName.VisibleIndex = 25
        Me.FTUnitSectName.Width = 100
        '
        'FTHoliday
        '
        Me.FTHoliday.Caption = "FTHoliday"
        Me.FTHoliday.FieldName = "FTHoliday"
        Me.FTHoliday.Name = "FTHoliday"
        Me.FTHoliday.Visible = True
        Me.FTHoliday.VisibleIndex = 26
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
        'RepColFTLeavePay
        '
        Me.RepColFTLeavePay.AutoHeight = False
        Me.RepColFTLeavePay.Caption = "Check"
        Me.RepColFTLeavePay.Name = "RepColFTLeavePay"
        Me.RepColFTLeavePay.ValueChecked = "1"
        Me.RepColFTLeavePay.ValueUnchecked = "0"
        '
        'GridView1
        '
        Me.GridView1.GridControl = Me.ogcleaveapp
        Me.GridView1.Name = "GridView1"
        '
        'FTSelectAllleave
        '
        Me.FTSelectAllleave.Location = New System.Drawing.Point(28, 1)
        Me.FTSelectAllleave.Margin = New System.Windows.Forms.Padding(4)
        Me.FTSelectAllleave.Name = "FTSelectAllleave"
        Me.FTSelectAllleave.Properties.Caption = "Select All"
        Me.FTSelectAllleave.Size = New System.Drawing.Size(230, 20)
        Me.FTSelectAllleave.TabIndex = 1
        Me.FTSelectAllleave.Tag = "2|"
        '
        'otbmain
        '
        Me.otbmain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.otbmain.Location = New System.Drawing.Point(0, 66)
        Me.otbmain.Name = "otbmain"
        Me.otbmain.SelectedTabPage = Me.otpappcminv
        Me.otbmain.Size = New System.Drawing.Size(1478, 644)
        Me.otbmain.TabIndex = 14
        Me.otbmain.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.otpappcminv})
        '
        'wLeaveApproved
        '
        Me.Appearance.Options.UseFont = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(11.0!, 21.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1478, 746)
        Me.Controls.Add(Me.ogbmainprocbutton)
        Me.Controls.Add(Me.otbmain)
        Me.Controls.Add(Me.StandaloneBarDockControl)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.MainRibbonControl)
        Me.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.IsMdiContainer = True
        Me.Margin = New System.Windows.Forms.Padding(6, 5, 6, 5)
        Me.Name = "wLeaveApproved"
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
        Me.otpappcminv.ResumeLayout(False)
        CType(Me.ogbapproveleave, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbapproveleave.ResumeLayout(False)
        CType(Me.ogcleaveapp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvapproveleave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemFTSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepColFTHoliday, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepFTLeavePay, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepFTStaCalSSO, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepFTApproveState, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCFTMngApproveState, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryFTSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryFTApproveState, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepColFTLeavePay, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTSelectAllleave.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.otbmain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.otbmain.ResumeLayout(False)
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
    Friend WithEvents otmchkpo As System.Windows.Forms.Timer
    Friend WithEvents ogbmainprocbutton As DevExpress.XtraEditors.PanelControl
    Friend WithEvents ocmpreview As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmsavelayout As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmreject As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmapprove As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents otbmain As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents otpappcminv As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents ogbapproveleave As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ogcleaveapp As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvapproveleave As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents FNHSysEmpID As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTSelect As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemFTSelect As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents cFTEmpName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFTStartDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFTEndDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNSumTotalLeaveDay As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTLeaveType As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTLeaveTypeName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTStaLeaveDayName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFTHoliday As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepColFTHoliday As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents FTLeavePay As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepFTLeavePay As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents FTLeaveStartTime As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTLeaveEndTime As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNLeaveTotalTime As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNLeaveTotalDay As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTStaCalSSO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepFTStaCalSSO As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents FTStaLeaveDay As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTLeaveNote As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFTStateMedicalCertificate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFTMedicalCertificateName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFTInsUser As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFTInsDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFTInsTime As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFTStateDeductVacation As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepFTApproveState As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents cFTStateType As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNLeaveTotalTimeMin As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTMngApproveState As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemCFTMngApproveState As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents FTMngApproveBy As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTMngApproveTime As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FDMngApproveDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTApproveState As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents SickLeave As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents BusinessLeave As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents VacationLeave As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryFTSelect As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents RepositoryFTApproveState As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents RepColFTLeavePay As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents FTSelectAllleave As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents FTEmpTypeName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTSectName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTUnitSectName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTHoliday As DevExpress.XtraGrid.Columns.GridColumn
End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wRDSamApproved
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
        Me.ogApprovedMail = New DevExpress.XtraEditors.GroupControl()
        Me.ogbmainprocbutton = New DevExpress.XtraEditors.PanelControl()
        Me.ocmsavelayout = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmapprove = New DevExpress.XtraEditors.SimpleButton()
        Me.ogSupervisorApproved = New DevExpress.XtraGrid.GridControl()
        Me.ogvSupervisorApproved = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.CFTSelect = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCheckEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.CFTSMPOrderNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFTStyleCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFTSeasonCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.otmchkpo = New System.Windows.Forms.Timer(Me.components)
        Me.FNHSysStyleId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNHSysSeasonId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFNSam = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFNOperater = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFNCost = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFNMinuteHour = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFNProdPersonPerDay = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFNWorkingTimeMinuteDay = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFNTargetPerDay = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFTSMPOrderNoRef = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.MainRibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemComboBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.oDockManager, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogApprovedMail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogApprovedMail.SuspendLayout()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbmainprocbutton.SuspendLayout()
        CType(Me.ogSupervisorApproved, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvSupervisorApproved, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.MainRibbonControl.Size = New System.Drawing.Size(1478, 54)
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
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 957)
        Me.RibbonStatusBar.Margin = New System.Windows.Forms.Padding(6, 5, 6, 5)
        Me.RibbonStatusBar.Name = "RibbonStatusBar"
        Me.RibbonStatusBar.Ribbon = Me.MainRibbonControl
        Me.RibbonStatusBar.Size = New System.Drawing.Size(1478, 27)
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
        Me.StandaloneBarDockControl.Location = New System.Drawing.Point(0, 54)
        Me.StandaloneBarDockControl.Margin = New System.Windows.Forms.Padding(4)
        Me.StandaloneBarDockControl.Name = "StandaloneBarDockControl"
        Me.StandaloneBarDockControl.Size = New System.Drawing.Size(1478, 0)
        Me.StandaloneBarDockControl.Text = "StandaloneBarDockControl1"
        '
        'ogApprovedMail
        '
        Me.ogApprovedMail.AppearanceCaption.Options.UseTextOptions = True
        Me.ogApprovedMail.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ogApprovedMail.Controls.Add(Me.ogbmainprocbutton)
        Me.ogApprovedMail.Controls.Add(Me.ogSupervisorApproved)
        Me.ogApprovedMail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogApprovedMail.Location = New System.Drawing.Point(0, 54)
        Me.ogApprovedMail.Margin = New System.Windows.Forms.Padding(4)
        Me.ogApprovedMail.Name = "ogApprovedMail"
        Me.ogApprovedMail.Size = New System.Drawing.Size(1478, 903)
        Me.ogApprovedMail.TabIndex = 9
        Me.ogApprovedMail.Text = "Approved  RD Sam"
        '
        'ogbmainprocbutton
        '
        Me.ogbmainprocbutton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmsavelayout)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmexit)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmapprove)
        Me.ogbmainprocbutton.Location = New System.Drawing.Point(54, 385)
        Me.ogbmainprocbutton.Margin = New System.Windows.Forms.Padding(4)
        Me.ogbmainprocbutton.Name = "ogbmainprocbutton"
        Me.ogbmainprocbutton.Size = New System.Drawing.Size(1371, 112)
        Me.ogbmainprocbutton.TabIndex = 138
        Me.ogbmainprocbutton.Tag = "2|"
        '
        'ocmsavelayout
        '
        Me.ocmsavelayout.Location = New System.Drawing.Point(1204, 22)
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
        'ocmapprove
        '
        Me.ocmapprove.Location = New System.Drawing.Point(15, 22)
        Me.ocmapprove.Margin = New System.Windows.Forms.Padding(4)
        Me.ocmapprove.Name = "ocmapprove"
        Me.ocmapprove.Size = New System.Drawing.Size(114, 30)
        Me.ocmapprove.TabIndex = 10
        Me.ocmapprove.Text = "Save"
        '
        'ogSupervisorApproved
        '
        Me.ogSupervisorApproved.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogSupervisorApproved.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4)
        Me.ogSupervisorApproved.Location = New System.Drawing.Point(0, 32)
        Me.ogSupervisorApproved.MainView = Me.ogvSupervisorApproved
        Me.ogSupervisorApproved.Margin = New System.Windows.Forms.Padding(4)
        Me.ogSupervisorApproved.Name = "ogSupervisorApproved"
        Me.ogSupervisorApproved.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCheckEdit1})
        Me.ogSupervisorApproved.Size = New System.Drawing.Size(1473, 864)
        Me.ogSupervisorApproved.TabIndex = 0
        Me.ogSupervisorApproved.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvSupervisorApproved})
        '
        'ogvSupervisorApproved
        '
        Me.ogvSupervisorApproved.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.CFTSelect, Me.CFTSMPOrderNo, Me.CFTStyleCode, Me.CFTSeasonCode, Me.FNHSysStyleId, Me.FNHSysSeasonId, Me.CFNSam, Me.CFNOperater, Me.CFNCost, Me.CFNMinuteHour, Me.CFNProdPersonPerDay, Me.CFNWorkingTimeMinuteDay, Me.CFNTargetPerDay, Me.CFTSMPOrderNoRef})
        Me.ogvSupervisorApproved.GridControl = Me.ogSupervisorApproved
        Me.ogvSupervisorApproved.Name = "ogvSupervisorApproved"
        Me.ogvSupervisorApproved.OptionsView.ColumnAutoWidth = False
        Me.ogvSupervisorApproved.OptionsView.ShowGroupPanel = False
        '
        'CFTSelect
        '
        Me.CFTSelect.Caption = " "
        Me.CFTSelect.ColumnEdit = Me.RepositoryItemCheckEdit1
        Me.CFTSelect.FieldName = "FTSelect"
        Me.CFTSelect.Name = "CFTSelect"
        Me.CFTSelect.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFTSelect.OptionsColumn.AllowMove = False
        Me.CFTSelect.OptionsColumn.AllowShowHide = False
        Me.CFTSelect.OptionsColumn.ReadOnly = True
        Me.CFTSelect.OptionsColumn.ShowInCustomizationForm = False
        Me.CFTSelect.Visible = True
        Me.CFTSelect.VisibleIndex = 0
        Me.CFTSelect.Width = 44
        '
        'RepositoryItemCheckEdit1
        '
        Me.RepositoryItemCheckEdit1.AutoHeight = False
        Me.RepositoryItemCheckEdit1.Caption = "Check"
        Me.RepositoryItemCheckEdit1.Name = "RepositoryItemCheckEdit1"
        Me.RepositoryItemCheckEdit1.ValueChecked = "1"
        Me.RepositoryItemCheckEdit1.ValueUnchecked = "0"
        '
        'CFTSMPOrderNo
        '
        Me.CFTSMPOrderNo.Caption = "FTSMPOrderNo"
        Me.CFTSMPOrderNo.FieldName = "FTSMPOrderNo"
        Me.CFTSMPOrderNo.Name = "CFTSMPOrderNo"
        Me.CFTSMPOrderNo.OptionsColumn.AllowEdit = False
        Me.CFTSMPOrderNo.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFTSMPOrderNo.OptionsColumn.AllowMove = False
        Me.CFTSMPOrderNo.OptionsColumn.AllowShowHide = False
        Me.CFTSMPOrderNo.OptionsColumn.ReadOnly = True
        Me.CFTSMPOrderNo.Visible = True
        Me.CFTSMPOrderNo.VisibleIndex = 1
        Me.CFTSMPOrderNo.Width = 131
        '
        'CFTStyleCode
        '
        Me.CFTStyleCode.Caption = "Style Code"
        Me.CFTStyleCode.FieldName = "FTStyleCode"
        Me.CFTStyleCode.Name = "CFTStyleCode"
        Me.CFTStyleCode.OptionsColumn.AllowEdit = False
        Me.CFTStyleCode.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFTStyleCode.OptionsColumn.AllowMove = False
        Me.CFTStyleCode.OptionsColumn.AllowShowHide = False
        Me.CFTStyleCode.OptionsColumn.ReadOnly = True
        Me.CFTStyleCode.Visible = True
        Me.CFTStyleCode.VisibleIndex = 2
        Me.CFTStyleCode.Width = 100
        '
        'CFTSeasonCode
        '
        Me.CFTSeasonCode.Caption = "Season"
        Me.CFTSeasonCode.FieldName = "FTSeasonCode"
        Me.CFTSeasonCode.Name = "CFTSeasonCode"
        Me.CFTSeasonCode.OptionsColumn.AllowEdit = False
        Me.CFTSeasonCode.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFTSeasonCode.OptionsColumn.AllowMove = False
        Me.CFTSeasonCode.OptionsColumn.AllowShowHide = False
        Me.CFTSeasonCode.OptionsColumn.ReadOnly = True
        Me.CFTSeasonCode.Visible = True
        Me.CFTSeasonCode.VisibleIndex = 3
        Me.CFTSeasonCode.Width = 429
        '
        'otmchkpo
        '
        Me.otmchkpo.Interval = 60000
        '
        'FNHSysStyleId
        '
        Me.FNHSysStyleId.Caption = "FNHSysStyleId"
        Me.FNHSysStyleId.FieldName = "FNHSysStyleId"
        Me.FNHSysStyleId.Name = "FNHSysStyleId"
        Me.FNHSysStyleId.OptionsColumn.AllowEdit = False
        Me.FNHSysStyleId.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNHSysStyleId.OptionsColumn.AllowMove = False
        Me.FNHSysStyleId.OptionsColumn.AllowShowHide = False
        Me.FNHSysStyleId.OptionsColumn.ReadOnly = True
        Me.FNHSysStyleId.OptionsColumn.ShowInCustomizationForm = False
        '
        'FNHSysSeasonId
        '
        Me.FNHSysSeasonId.Caption = "FNHSysSeasonId"
        Me.FNHSysSeasonId.FieldName = "FNHSysSeasonId"
        Me.FNHSysSeasonId.Name = "FNHSysSeasonId"
        Me.FNHSysSeasonId.OptionsColumn.AllowEdit = False
        Me.FNHSysSeasonId.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNHSysSeasonId.OptionsColumn.AllowMove = False
        Me.FNHSysSeasonId.OptionsColumn.AllowShowHide = False
        Me.FNHSysSeasonId.OptionsColumn.ReadOnly = True
        Me.FNHSysSeasonId.OptionsColumn.ShowInCustomizationForm = False
        '
        'CFNSam
        '
        Me.CFNSam.Caption = "Sam"
        Me.CFNSam.DisplayFormat.FormatString = "{0:n4}"
        Me.CFNSam.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CFNSam.FieldName = "FNSam"
        Me.CFNSam.Name = "CFNSam"
        Me.CFNSam.OptionsColumn.AllowEdit = False
        Me.CFNSam.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFNSam.OptionsColumn.AllowMove = False
        Me.CFNSam.OptionsColumn.AllowShowHide = False
        Me.CFNSam.OptionsColumn.ReadOnly = True
        Me.CFNSam.Visible = True
        Me.CFNSam.VisibleIndex = 4
        '
        'CFNOperater
        '
        Me.CFNOperater.Caption = "Operater"
        Me.CFNOperater.DisplayFormat.FormatString = "{0:n2}"
        Me.CFNOperater.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CFNOperater.FieldName = "FNOperater"
        Me.CFNOperater.Name = "CFNOperater"
        Me.CFNOperater.OptionsColumn.AllowEdit = False
        Me.CFNOperater.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFNOperater.OptionsColumn.AllowMove = False
        Me.CFNOperater.OptionsColumn.AllowShowHide = False
        Me.CFNOperater.OptionsColumn.ReadOnly = True
        Me.CFNOperater.Visible = True
        Me.CFNOperater.VisibleIndex = 5
        '
        'CFNCost
        '
        Me.CFNCost.Caption = "FNCost"
        Me.CFNCost.DisplayFormat.FormatString = "{0:n4}"
        Me.CFNCost.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CFNCost.FieldName = "FNCost"
        Me.CFNCost.Name = "CFNCost"
        Me.CFNCost.OptionsColumn.AllowEdit = False
        Me.CFNCost.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFNCost.OptionsColumn.AllowMove = False
        Me.CFNCost.OptionsColumn.AllowShowHide = False
        Me.CFNCost.OptionsColumn.ReadOnly = True
        Me.CFNCost.Visible = True
        Me.CFNCost.VisibleIndex = 6
        '
        'CFNMinuteHour
        '
        Me.CFNMinuteHour.Caption = "Minute"
        Me.CFNMinuteHour.DisplayFormat.FormatString = "{0:n0}"
        Me.CFNMinuteHour.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CFNMinuteHour.FieldName = "FNMinuteHour"
        Me.CFNMinuteHour.Name = "CFNMinuteHour"
        Me.CFNMinuteHour.OptionsColumn.AllowEdit = False
        Me.CFNMinuteHour.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFNMinuteHour.OptionsColumn.AllowMove = False
        Me.CFNMinuteHour.OptionsColumn.AllowShowHide = False
        Me.CFNMinuteHour.OptionsColumn.ReadOnly = True
        Me.CFNMinuteHour.Visible = True
        Me.CFNMinuteHour.VisibleIndex = 7
        '
        'CFNProdPersonPerDay
        '
        Me.CFNProdPersonPerDay.Caption = "Person / Day"
        Me.CFNProdPersonPerDay.DisplayFormat.FormatString = "{0:n0}"
        Me.CFNProdPersonPerDay.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CFNProdPersonPerDay.FieldName = "FNProdPersonPerDay"
        Me.CFNProdPersonPerDay.Name = "CFNProdPersonPerDay"
        Me.CFNProdPersonPerDay.OptionsColumn.AllowEdit = False
        Me.CFNProdPersonPerDay.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFNProdPersonPerDay.OptionsColumn.AllowMove = False
        Me.CFNProdPersonPerDay.OptionsColumn.AllowShowHide = False
        Me.CFNProdPersonPerDay.OptionsColumn.ReadOnly = True
        Me.CFNProdPersonPerDay.Visible = True
        Me.CFNProdPersonPerDay.VisibleIndex = 8
        '
        'CFNWorkingTimeMinuteDay
        '
        Me.CFNWorkingTimeMinuteDay.Caption = "Working Minute / Day"
        Me.CFNWorkingTimeMinuteDay.DisplayFormat.FormatString = "{0:n0}"
        Me.CFNWorkingTimeMinuteDay.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CFNWorkingTimeMinuteDay.FieldName = "FNWorkingTimeMinuteDay"
        Me.CFNWorkingTimeMinuteDay.Name = "CFNWorkingTimeMinuteDay"
        Me.CFNWorkingTimeMinuteDay.OptionsColumn.AllowEdit = False
        Me.CFNWorkingTimeMinuteDay.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFNWorkingTimeMinuteDay.OptionsColumn.AllowMove = False
        Me.CFNWorkingTimeMinuteDay.OptionsColumn.AllowShowHide = False
        Me.CFNWorkingTimeMinuteDay.OptionsColumn.ReadOnly = True
        Me.CFNWorkingTimeMinuteDay.Visible = True
        Me.CFNWorkingTimeMinuteDay.VisibleIndex = 9
        '
        'CFNTargetPerDay
        '
        Me.CFNTargetPerDay.Caption = "Target / Day"
        Me.CFNTargetPerDay.DisplayFormat.FormatString = "{0:n0}"
        Me.CFNTargetPerDay.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CFNTargetPerDay.FieldName = "FNTargetPerDay"
        Me.CFNTargetPerDay.Name = "CFNTargetPerDay"
        Me.CFNTargetPerDay.OptionsColumn.AllowEdit = False
        Me.CFNTargetPerDay.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFNTargetPerDay.OptionsColumn.AllowMove = False
        Me.CFNTargetPerDay.OptionsColumn.AllowShowHide = False
        Me.CFNTargetPerDay.OptionsColumn.ReadOnly = True
        Me.CFNTargetPerDay.Visible = True
        Me.CFNTargetPerDay.VisibleIndex = 10
        '
        'CFTSMPOrderNoRef
        '
        Me.CFTSMPOrderNoRef.Caption = "FTSMPOrderNoRef"
        Me.CFTSMPOrderNoRef.FieldName = "FTSMPOrderNoRef"
        Me.CFTSMPOrderNoRef.Name = "CFTSMPOrderNoRef"
        Me.CFTSMPOrderNoRef.OptionsColumn.AllowEdit = False
        Me.CFTSMPOrderNoRef.OptionsColumn.ReadOnly = True
        '
        'wRDSamApproved
        '
        Me.Appearance.Options.UseFont = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1478, 984)
        Me.Controls.Add(Me.ogApprovedMail)
        Me.Controls.Add(Me.StandaloneBarDockControl)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.MainRibbonControl)
        Me.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.IsMdiContainer = True
        Me.Margin = New System.Windows.Forms.Padding(6, 5, 6, 5)
        Me.Name = "wRDSamApproved"
        Me.Ribbon = Me.MainRibbonControl
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.StatusBar = Me.RibbonStatusBar
        Me.Tag = "|WISDOM SYSTEM|WISDOM SYSTEM"
        Me.Text = "WISDOM SYSTEM"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.MainRibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemComboBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.oDockManager, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogApprovedMail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogApprovedMail.ResumeLayout(False)
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbmainprocbutton.ResumeLayout(False)
        CType(Me.ogSupervisorApproved, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvSupervisorApproved, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents ogApprovedMail As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ogSupervisorApproved As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvSupervisorApproved As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents RepositoryItemCheckEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents ogbmainprocbutton As DevExpress.XtraEditors.PanelControl
    Friend WithEvents ocmapprove As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents otmchkpo As System.Windows.Forms.Timer
    Friend WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmsavelayout As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents CFTSelect As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFTSMPOrderNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFTStyleCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFTSeasonCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNHSysStyleId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNHSysSeasonId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFNSam As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFNOperater As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFNCost As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFNMinuteHour As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFNProdPersonPerDay As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFNWorkingTimeMinuteDay As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFNTargetPerDay As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFTSMPOrderNoRef As DevExpress.XtraGrid.Columns.GridColumn
End Class

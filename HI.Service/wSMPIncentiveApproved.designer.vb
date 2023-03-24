<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wSMPIncentiveApproved
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
        Me.MainRibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.mnusysabout = New DevExpress.XtraBars.BarButtonItem()
        Me.FTUserLogINIP = New DevExpress.XtraBars.BarStaticItem()
        Me.RepositoryItemComboBox1 = New DevExpress.XtraEditors.Repository.RepositoryItemComboBox()
        Me.RibbonStatusBar = New DevExpress.XtraBars.Ribbon.RibbonStatusBar()
        Me.MainDefaultLookAndFeel = New DevExpress.LookAndFeel.DefaultLookAndFeel()
        Me.oDockManager = New DevExpress.XtraBars.Docking.DockManager()
        Me.StandaloneBarDockControl = New DevExpress.XtraBars.StandaloneBarDockControl()
        Me.ogApprovedMail = New DevExpress.XtraEditors.GroupControl()
        Me.ogbmainprocbutton = New DevExpress.XtraEditors.PanelControl()
        Me.ocmsavelayout = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmpreview = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmreject = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmapprove = New DevExpress.XtraEditors.SimpleButton()
        Me.ochkselectall = New DevExpress.XtraEditors.CheckEdit()
        Me.ogSupervisorApproved = New DevExpress.XtraGrid.GridControl()
        Me.ogvSupervisorApproved = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.CFTSelect = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCheckEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.FTStateFinishDateOrg = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFTStateFinishDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFTSMPOrderNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFTTeam = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFTEmpName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFNQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFNAmount = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFTSendAppDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTSendAppBy = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.otmchkpo = New System.Windows.Forms.Timer()
        Me.CFNDataType = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.MainRibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemComboBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.oDockManager, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogApprovedMail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogApprovedMail.SuspendLayout()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbmainprocbutton.SuspendLayout()
        CType(Me.ochkselectall.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.MainRibbonControl.Size = New System.Drawing.Size(1478, 69)
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
        Me.RibbonStatusBar.Location = New System.Drawing.Point(0, 949)
        Me.RibbonStatusBar.Margin = New System.Windows.Forms.Padding(6, 5, 6, 5)
        Me.RibbonStatusBar.Name = "RibbonStatusBar"
        Me.RibbonStatusBar.Ribbon = Me.MainRibbonControl
        Me.RibbonStatusBar.Size = New System.Drawing.Size(1478, 35)
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
        Me.StandaloneBarDockControl.Location = New System.Drawing.Point(0, 69)
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
        Me.ogApprovedMail.Controls.Add(Me.ochkselectall)
        Me.ogApprovedMail.Controls.Add(Me.ogSupervisorApproved)
        Me.ogApprovedMail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogApprovedMail.Location = New System.Drawing.Point(0, 69)
        Me.ogApprovedMail.Margin = New System.Windows.Forms.Padding(4)
        Me.ogApprovedMail.Name = "ogApprovedMail"
        Me.ogApprovedMail.Size = New System.Drawing.Size(1478, 880)
        Me.ogApprovedMail.TabIndex = 9
        Me.ogApprovedMail.Text = "Approved  Purchase"
        '
        'ogbmainprocbutton
        '
        Me.ogbmainprocbutton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmsavelayout)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmexit)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmpreview)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmreject)
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
        'ocmpreview
        '
        Me.ocmpreview.Location = New System.Drawing.Point(319, 22)
        Me.ocmpreview.Margin = New System.Windows.Forms.Padding(4)
        Me.ocmpreview.Name = "ocmpreview"
        Me.ocmpreview.Size = New System.Drawing.Size(132, 30)
        Me.ocmpreview.TabIndex = 12
        Me.ocmpreview.Text = "Preview"
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
        'ochkselectall
        '
        Me.ochkselectall.Location = New System.Drawing.Point(28, 1)
        Me.ochkselectall.Margin = New System.Windows.Forms.Padding(4)
        Me.ochkselectall.Name = "ochkselectall"
        Me.ochkselectall.Properties.Caption = "Select All"
        Me.ochkselectall.Size = New System.Drawing.Size(230, 20)
        Me.ochkselectall.TabIndex = 1
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
        Me.ogSupervisorApproved.Size = New System.Drawing.Size(1473, 841)
        Me.ogSupervisorApproved.TabIndex = 0
        Me.ogSupervisorApproved.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvSupervisorApproved})
        '
        'ogvSupervisorApproved
        '
        Me.ogvSupervisorApproved.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.CFTSelect, Me.FTStateFinishDateOrg, Me.CFTStateFinishDate, Me.CFTSMPOrderNo, Me.CFTTeam, Me.CFTEmpName, Me.CFNQuantity, Me.CFNAmount, Me.CFTSendAppDate, Me.FTSendAppBy, Me.CFNDataType})
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
        'FTStateFinishDateOrg
        '
        Me.FTStateFinishDateOrg.Caption = "FTStateFinishDateOrg"
        Me.FTStateFinishDateOrg.FieldName = "FTStateFinishDateOrg"
        Me.FTStateFinishDateOrg.Name = "FTStateFinishDateOrg"
        Me.FTStateFinishDateOrg.OptionsColumn.AllowEdit = False
        Me.FTStateFinishDateOrg.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTStateFinishDateOrg.OptionsColumn.AllowMove = False
        Me.FTStateFinishDateOrg.OptionsColumn.AllowShowHide = False
        Me.FTStateFinishDateOrg.OptionsColumn.ReadOnly = True
        Me.FTStateFinishDateOrg.OptionsColumn.ShowInCustomizationForm = False
        '
        'CFTStateFinishDate
        '
        Me.CFTStateFinishDate.Caption = "FTStateFinishDate"
        Me.CFTStateFinishDate.FieldName = "FTStateFinishDate"
        Me.CFTStateFinishDate.Name = "CFTStateFinishDate"
        Me.CFTStateFinishDate.OptionsColumn.AllowEdit = False
        Me.CFTStateFinishDate.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFTStateFinishDate.OptionsColumn.AllowMove = False
        Me.CFTStateFinishDate.OptionsColumn.AllowShowHide = False
        Me.CFTStateFinishDate.OptionsColumn.ReadOnly = True
        Me.CFTStateFinishDate.OptionsColumn.ShowInCustomizationForm = False
        Me.CFTStateFinishDate.Visible = True
        Me.CFTStateFinishDate.VisibleIndex = 1
        Me.CFTStateFinishDate.Width = 140
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
        Me.CFTSMPOrderNo.OptionsColumn.ShowInCustomizationForm = False
        Me.CFTSMPOrderNo.Visible = True
        Me.CFTSMPOrderNo.VisibleIndex = 2
        Me.CFTSMPOrderNo.Width = 131
        '
        'CFTTeam
        '
        Me.CFTTeam.Caption = "FTTeam"
        Me.CFTTeam.FieldName = "FTTeam"
        Me.CFTTeam.Name = "CFTTeam"
        Me.CFTTeam.OptionsColumn.AllowEdit = False
        Me.CFTTeam.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFTTeam.OptionsColumn.AllowMove = False
        Me.CFTTeam.OptionsColumn.AllowShowHide = False
        Me.CFTTeam.OptionsColumn.ReadOnly = True
        Me.CFTTeam.OptionsColumn.ShowInCustomizationForm = False
        Me.CFTTeam.Visible = True
        Me.CFTTeam.VisibleIndex = 3
        Me.CFTTeam.Width = 100
        '
        'CFTEmpName
        '
        Me.CFTEmpName.Caption = "FTEmpName"
        Me.CFTEmpName.FieldName = "FTEmpName"
        Me.CFTEmpName.Name = "CFTEmpName"
        Me.CFTEmpName.OptionsColumn.AllowEdit = False
        Me.CFTEmpName.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFTEmpName.OptionsColumn.AllowMove = False
        Me.CFTEmpName.OptionsColumn.AllowShowHide = False
        Me.CFTEmpName.OptionsColumn.ReadOnly = True
        Me.CFTEmpName.OptionsColumn.ShowInCustomizationForm = False
        Me.CFTEmpName.Visible = True
        Me.CFTEmpName.VisibleIndex = 4
        Me.CFTEmpName.Width = 429
        '
        'CFNQuantity
        '
        Me.CFNQuantity.Caption = "FNQuantity"
        Me.CFNQuantity.DisplayFormat.FormatString = "{0:n0}"
        Me.CFNQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CFNQuantity.FieldName = "FNQuantity"
        Me.CFNQuantity.Name = "CFNQuantity"
        Me.CFNQuantity.OptionsColumn.AllowEdit = False
        Me.CFNQuantity.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFNQuantity.OptionsColumn.AllowMove = False
        Me.CFNQuantity.OptionsColumn.AllowShowHide = False
        Me.CFNQuantity.OptionsColumn.ReadOnly = True
        Me.CFNQuantity.OptionsColumn.ShowInCustomizationForm = False
        Me.CFNQuantity.Visible = True
        Me.CFNQuantity.VisibleIndex = 5
        Me.CFNQuantity.Width = 109
        '
        'CFNAmount
        '
        Me.CFNAmount.Caption = "FNAmount"
        Me.CFNAmount.DisplayFormat.FormatString = "{0:n2}"
        Me.CFNAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CFNAmount.FieldName = "FNAmount"
        Me.CFNAmount.Name = "CFNAmount"
        Me.CFNAmount.OptionsColumn.AllowEdit = False
        Me.CFNAmount.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFNAmount.OptionsColumn.AllowMove = False
        Me.CFNAmount.OptionsColumn.AllowShowHide = False
        Me.CFNAmount.OptionsColumn.ReadOnly = True
        Me.CFNAmount.OptionsColumn.ShowInCustomizationForm = False
        Me.CFNAmount.Visible = True
        Me.CFNAmount.VisibleIndex = 6
        Me.CFNAmount.Width = 135
        '
        'CFTSendAppDate
        '
        Me.CFTSendAppDate.Caption = "FTSendAppDate"
        Me.CFTSendAppDate.FieldName = "FTSendAppDate"
        Me.CFTSendAppDate.Name = "CFTSendAppDate"
        Me.CFTSendAppDate.OptionsColumn.AllowEdit = False
        Me.CFTSendAppDate.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFTSendAppDate.OptionsColumn.AllowMove = False
        Me.CFTSendAppDate.OptionsColumn.AllowShowHide = False
        Me.CFTSendAppDate.OptionsColumn.ReadOnly = True
        Me.CFTSendAppDate.OptionsColumn.ShowInCustomizationForm = False
        Me.CFTSendAppDate.Visible = True
        Me.CFTSendAppDate.VisibleIndex = 7
        Me.CFTSendAppDate.Width = 114
        '
        'FTSendAppBy
        '
        Me.FTSendAppBy.Caption = "FTSendAppBy"
        Me.FTSendAppBy.FieldName = "FTSendAppBy"
        Me.FTSendAppBy.Name = "FTSendAppBy"
        Me.FTSendAppBy.OptionsColumn.AllowEdit = False
        Me.FTSendAppBy.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTSendAppBy.OptionsColumn.AllowMove = False
        Me.FTSendAppBy.OptionsColumn.AllowShowHide = False
        Me.FTSendAppBy.OptionsColumn.ReadOnly = True
        Me.FTSendAppBy.OptionsColumn.ShowInCustomizationForm = False
        Me.FTSendAppBy.Visible = True
        Me.FTSendAppBy.VisibleIndex = 8
        Me.FTSendAppBy.Width = 144
        '
        'otmchkpo
        '
        Me.otmchkpo.Interval = 60000
        '
        'CFNDataType
        '
        Me.CFNDataType.Caption = "FNDataType"
        Me.CFNDataType.FieldName = "FNDataType"
        Me.CFNDataType.Name = "CFNDataType"
        '
        'wSMPIncentiveApproved
        '
        Me.Appearance.Options.UseFont = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(11.0!, 21.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1478, 984)
        Me.Controls.Add(Me.ogApprovedMail)
        Me.Controls.Add(Me.StandaloneBarDockControl)
        Me.Controls.Add(Me.RibbonStatusBar)
        Me.Controls.Add(Me.MainRibbonControl)
        Me.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.IsMdiContainer = True
        Me.Margin = New System.Windows.Forms.Padding(6, 5, 6, 5)
        Me.Name = "wSMPIncentiveApproved"
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
        CType(Me.ochkselectall.Properties, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents ochkselectall As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents ogSupervisorApproved As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvSupervisorApproved As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents RepositoryItemCheckEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents ogbmainprocbutton As DevExpress.XtraEditors.PanelControl
    Friend WithEvents ocmpreview As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmreject As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmapprove As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents otmchkpo As System.Windows.Forms.Timer
    Friend WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmsavelayout As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents CFTSelect As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTStateFinishDateOrg As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFTStateFinishDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFTSMPOrderNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFTTeam As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFTEmpName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFNQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFNAmount As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFTSendAppDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTSendAppBy As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFNDataType As DevExpress.XtraGrid.Columns.GridColumn
End Class

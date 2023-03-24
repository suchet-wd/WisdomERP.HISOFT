<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wAccCloseStock
    Inherits DevExpress.XtraEditors.XtraForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.ogbdetail = New DevExpress.XtraEditors.GroupControl()
        Me.ochkselectall = New DevExpress.XtraEditors.CheckEdit()
        Me.ogbmainprocbutton = New DevExpress.XtraEditors.PanelControl()
        Me.ocmreopenstock = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmclosestock = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmclear = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmload = New DevExpress.XtraEditors.SimpleButton()
        Me.ogdtime = New DevExpress.XtraGrid.GridControl()
        Me.ogvtime = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.FTSelect = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ReposFTSelect = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.FNHSysWHId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTWHCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNHSysCmpId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTWHName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTMonth = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTYear = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTYearClose = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTMonthClose = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.oDockManager = New DevExpress.XtraBars.Docking.DockManager()
        CType(Me.ogbdetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbdetail.SuspendLayout()
        CType(Me.ochkselectall.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbmainprocbutton.SuspendLayout()
        CType(Me.ogdtime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvtime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ReposFTSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.oDockManager, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ogbdetail
        '
        Me.ogbdetail.Controls.Add(Me.ochkselectall)
        Me.ogbdetail.Controls.Add(Me.ogbmainprocbutton)
        Me.ogbdetail.Controls.Add(Me.ogdtime)
        Me.ogbdetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogbdetail.Location = New System.Drawing.Point(0, 0)
        Me.ogbdetail.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbdetail.Name = "ogbdetail"
        Me.ogbdetail.ShowCaption = False
        Me.ogbdetail.Size = New System.Drawing.Size(1326, 779)
        Me.ogbdetail.TabIndex = 0
        '
        'ochkselectall
        '
        Me.ochkselectall.Location = New System.Drawing.Point(33, 3)
        Me.ochkselectall.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ochkselectall.Name = "ochkselectall"
        Me.ochkselectall.Properties.Caption = "Select All"
        Me.ochkselectall.Size = New System.Drawing.Size(222, 20)
        Me.ochkselectall.TabIndex = 387
        '
        'ogbmainprocbutton
        '
        Me.ogbmainprocbutton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmreopenstock)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmclosestock)
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
        'ocmreopenstock
        '
        Me.ocmreopenstock.Location = New System.Drawing.Point(449, 13)
        Me.ocmreopenstock.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmreopenstock.Name = "ocmreopenstock"
        Me.ocmreopenstock.Size = New System.Drawing.Size(136, 31)
        Me.ocmreopenstock.TabIndex = 331
        Me.ocmreopenstock.TabStop = False
        Me.ocmreopenstock.Tag = "2|"
        Me.ocmreopenstock.Text = "Reopen Stock"
        '
        'ocmclosestock
        '
        Me.ocmclosestock.Location = New System.Drawing.Point(289, 15)
        Me.ocmclosestock.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmclosestock.Name = "ocmclosestock"
        Me.ocmclosestock.Size = New System.Drawing.Size(136, 31)
        Me.ocmclosestock.TabIndex = 330
        Me.ocmclosestock.TabStop = False
        Me.ocmclosestock.Tag = "2|"
        Me.ocmclosestock.Text = "Close Stock"
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
        Me.ogdtime.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogdtime.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogdtime.Location = New System.Drawing.Point(4, 24)
        Me.ogdtime.MainView = Me.ogvtime
        Me.ogdtime.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogdtime.Name = "ogdtime"
        Me.ogdtime.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.ReposFTSelect})
        Me.ogdtime.Size = New System.Drawing.Size(1317, 755)
        Me.ogdtime.TabIndex = 0
        Me.ogdtime.TabStop = False
        Me.ogdtime.Tag = "2|"
        Me.ogdtime.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvtime})
        '
        'ogvtime
        '
        Me.ogvtime.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.FTSelect, Me.FNHSysWHId, Me.FTWHCode, Me.FNHSysCmpId, Me.FTWHName, Me.FTMonth, Me.FTYear, Me.FTYearClose, Me.FTMonthClose})
        Me.ogvtime.GridControl = Me.ogdtime
        Me.ogvtime.Name = "ogvtime"
        Me.ogvtime.OptionsCustomization.AllowQuickHideColumns = False
        Me.ogvtime.OptionsView.ColumnAutoWidth = False
        Me.ogvtime.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.ogvtime.OptionsView.ShowGroupPanel = False
        Me.ogvtime.Tag = "2|"
        '
        'FTSelect
        '
        Me.FTSelect.Caption = " "
        Me.FTSelect.ColumnEdit = Me.ReposFTSelect
        Me.FTSelect.FieldName = "FTSelect"
        Me.FTSelect.Name = "FTSelect"
        Me.FTSelect.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTSelect.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTSelect.OptionsColumn.AllowMove = False
        Me.FTSelect.Visible = True
        Me.FTSelect.VisibleIndex = 0
        Me.FTSelect.Width = 46
        '
        'ReposFTSelect
        '
        Me.ReposFTSelect.AutoHeight = False
        Me.ReposFTSelect.Caption = "Check"
        Me.ReposFTSelect.Name = "ReposFTSelect"
        Me.ReposFTSelect.ValueChecked = "1"
        Me.ReposFTSelect.ValueUnchecked = "0"
        '
        'FNHSysWHId
        '
        Me.FNHSysWHId.Caption = "FNHSysWHId"
        Me.FNHSysWHId.FieldName = "FNHSysWHId"
        Me.FNHSysWHId.Name = "FNHSysWHId"
        Me.FNHSysWHId.OptionsColumn.AllowEdit = False
        Me.FNHSysWHId.OptionsColumn.AllowMove = False
        Me.FNHSysWHId.OptionsColumn.AllowShowHide = False
        Me.FNHSysWHId.OptionsColumn.ReadOnly = True
        Me.FNHSysWHId.OptionsColumn.ShowInCustomizationForm = False
        '
        'FTWHCode
        '
        Me.FTWHCode.Caption = "FTWHCode"
        Me.FTWHCode.FieldName = "FTWHCode"
        Me.FTWHCode.Name = "FTWHCode"
        Me.FTWHCode.OptionsColumn.AllowEdit = False
        Me.FTWHCode.OptionsColumn.AllowMove = False
        Me.FTWHCode.OptionsColumn.AllowShowHide = False
        Me.FTWHCode.OptionsColumn.ReadOnly = True
        Me.FTWHCode.OptionsColumn.ShowInCustomizationForm = False
        Me.FTWHCode.Visible = True
        Me.FTWHCode.VisibleIndex = 1
        Me.FTWHCode.Width = 112
        '
        'FNHSysCmpId
        '
        Me.FNHSysCmpId.Caption = "FNHSysCmpId"
        Me.FNHSysCmpId.FieldName = "FNHSysCmpId"
        Me.FNHSysCmpId.Name = "FNHSysCmpId"
        Me.FNHSysCmpId.OptionsColumn.AllowEdit = False
        Me.FNHSysCmpId.OptionsColumn.AllowMove = False
        Me.FNHSysCmpId.OptionsColumn.AllowShowHide = False
        Me.FNHSysCmpId.OptionsColumn.ReadOnly = True
        Me.FNHSysCmpId.OptionsColumn.ShowInCustomizationForm = False
        '
        'FTWHName
        '
        Me.FTWHName.Caption = "FTWHName"
        Me.FTWHName.FieldName = "FTWHName"
        Me.FTWHName.Name = "FTWHName"
        Me.FTWHName.OptionsColumn.AllowEdit = False
        Me.FTWHName.OptionsColumn.AllowMove = False
        Me.FTWHName.OptionsColumn.AllowShowHide = False
        Me.FTWHName.OptionsColumn.ReadOnly = True
        Me.FTWHName.OptionsColumn.ShowInCustomizationForm = False
        Me.FTWHName.Visible = True
        Me.FTWHName.VisibleIndex = 2
        Me.FTWHName.Width = 281
        '
        'FTMonth
        '
        Me.FTMonth.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.FTMonth.AppearanceCell.Options.UseBackColor = True
        Me.FTMonth.AppearanceCell.Options.UseTextOptions = True
        Me.FTMonth.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTMonth.Caption = "FTMonth"
        Me.FTMonth.FieldName = "FTMonth"
        Me.FTMonth.Name = "FTMonth"
        Me.FTMonth.OptionsColumn.AllowEdit = False
        Me.FTMonth.OptionsColumn.AllowMove = False
        Me.FTMonth.OptionsColumn.AllowShowHide = False
        Me.FTMonth.OptionsColumn.ReadOnly = True
        Me.FTMonth.OptionsColumn.ShowInCustomizationForm = False
        Me.FTMonth.Visible = True
        Me.FTMonth.VisibleIndex = 3
        Me.FTMonth.Width = 100
        '
        'FTYear
        '
        Me.FTYear.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.FTYear.AppearanceCell.Options.UseBackColor = True
        Me.FTYear.AppearanceCell.Options.UseTextOptions = True
        Me.FTYear.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTYear.Caption = "FTYear"
        Me.FTYear.FieldName = "FTYear"
        Me.FTYear.Name = "FTYear"
        Me.FTYear.OptionsColumn.AllowEdit = False
        Me.FTYear.OptionsColumn.AllowMove = False
        Me.FTYear.OptionsColumn.AllowShowHide = False
        Me.FTYear.OptionsColumn.ReadOnly = True
        Me.FTYear.OptionsColumn.ShowInCustomizationForm = False
        Me.FTYear.Visible = True
        Me.FTYear.VisibleIndex = 4
        Me.FTYear.Width = 100
        '
        'FTYearClose
        '
        Me.FTYearClose.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.FTYearClose.AppearanceCell.Options.UseBackColor = True
        Me.FTYearClose.AppearanceCell.Options.UseTextOptions = True
        Me.FTYearClose.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTYearClose.Caption = "FTYearClose"
        Me.FTYearClose.FieldName = "FTYearClose"
        Me.FTYearClose.Name = "FTYearClose"
        Me.FTYearClose.OptionsColumn.AllowEdit = False
        Me.FTYearClose.OptionsColumn.AllowMove = False
        Me.FTYearClose.OptionsColumn.AllowShowHide = False
        Me.FTYearClose.OptionsColumn.ReadOnly = True
        Me.FTYearClose.OptionsColumn.ShowInCustomizationForm = False
        Me.FTYearClose.Visible = True
        Me.FTYearClose.VisibleIndex = 6
        Me.FTYearClose.Width = 100
        '
        'FTMonthClose
        '
        Me.FTMonthClose.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.FTMonthClose.AppearanceCell.Options.UseBackColor = True
        Me.FTMonthClose.AppearanceCell.Options.UseTextOptions = True
        Me.FTMonthClose.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTMonthClose.Caption = "FTMonthClose"
        Me.FTMonthClose.FieldName = "FTMonthClose"
        Me.FTMonthClose.Name = "FTMonthClose"
        Me.FTMonthClose.OptionsColumn.AllowEdit = False
        Me.FTMonthClose.OptionsColumn.AllowMove = False
        Me.FTMonthClose.OptionsColumn.AllowShowHide = False
        Me.FTMonthClose.OptionsColumn.ReadOnly = True
        Me.FTMonthClose.OptionsColumn.ShowInCustomizationForm = False
        Me.FTMonthClose.Visible = True
        Me.FTMonthClose.VisibleIndex = 5
        Me.FTMonthClose.Width = 100
        '
        'oDockManager
        '
        Me.oDockManager.Form = Me
        Me.oDockManager.TopZIndexControls.AddRange(New String() {"DevExpress.XtraBars.BarDockControl", "DevExpress.XtraBars.StandaloneBarDockControl", "System.Windows.Forms.StatusBar", "DevExpress.XtraBars.Ribbon.RibbonStatusBar", "DevExpress.XtraBars.Ribbon.RibbonControl"})
        '
        'wAccCloseStock
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1326, 779)
        Me.Controls.Add(Me.ogbdetail)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "wAccCloseStock"
        Me.Text = "Close Stock Monthly"
        CType(Me.ogbdetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbdetail.ResumeLayout(False)
        CType(Me.ochkselectall.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbmainprocbutton.ResumeLayout(False)
        CType(Me.ogdtime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvtime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ReposFTSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.oDockManager, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents ocmreopenstock As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmclosestock As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FTSelect As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ReposFTSelect As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents FNHSysWHId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTWHCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNHSysCmpId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTWHName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTMonth As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTYear As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTYearClose As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTMonthClose As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ochkselectall As DevExpress.XtraEditors.CheckEdit
End Class

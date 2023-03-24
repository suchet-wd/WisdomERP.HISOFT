<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Listing
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
        Me.oDockManager = New DevExpress.XtraBars.Docking.DockManager()
        Me.odocpanelheader = New DevExpress.XtraBars.Docking.DockPanel()
        Me.DockPanel1_Container = New DevExpress.XtraBars.Docking.ControlContainer()
        Me.RepositoryFTSelect = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.RepositoryFTApproveState = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.ogblisting = New DevExpress.XtraEditors.GroupControl()
        Me.ogclisting = New DevExpress.XtraGrid.GridControl()
        Me.ogvlisting = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.RepositoryItemCheckEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.RepositoryItemCheckEdit2 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        CType(Me.oDockManager, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.odocpanelheader.SuspendLayout()
        CType(Me.RepositoryFTSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryFTApproveState, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogblisting, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogblisting.SuspendLayout()
        CType(Me.ogclisting, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvlisting, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'oDockManager
        '
        Me.oDockManager.Form = Me
        Me.oDockManager.RootPanels.AddRange(New DevExpress.XtraBars.Docking.DockPanel() {Me.odocpanelheader})
        Me.oDockManager.TopZIndexControls.AddRange(New String() {"DevExpress.XtraBars.BarDockControl", "DevExpress.XtraBars.StandaloneBarDockControl", "System.Windows.Forms.StatusBar", "DevExpress.XtraBars.Ribbon.RibbonStatusBar", "DevExpress.XtraBars.Ribbon.RibbonControl"})
        '
        'odocpanelheader
        '
        Me.odocpanelheader.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.odocpanelheader.Appearance.Options.UseForeColor = True
        Me.odocpanelheader.Appearance.Options.UseTextOptions = True
        Me.odocpanelheader.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.odocpanelheader.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.odocpanelheader.Controls.Add(Me.DockPanel1_Container)
        Me.odocpanelheader.Cursor = System.Windows.Forms.Cursors.Hand
        Me.odocpanelheader.Dock = DevExpress.XtraBars.Docking.DockingStyle.Top
        Me.odocpanelheader.DockVertical = DevExpress.Utils.DefaultBoolean.[False]
        Me.odocpanelheader.ID = New System.Guid("77b9346d-8d15-4323-af1e-af82afa9902a")
        Me.odocpanelheader.Location = New System.Drawing.Point(0, 0)
        Me.odocpanelheader.Name = "odocpanelheader"
        Me.odocpanelheader.Options.AllowDockBottom = False
        Me.odocpanelheader.Options.AllowDockFill = False
        Me.odocpanelheader.Options.FloatOnDblClick = False
        Me.odocpanelheader.Options.ShowCloseButton = False
        Me.odocpanelheader.Options.ShowMaximizeButton = False
        Me.odocpanelheader.OriginalSize = New System.Drawing.Size(1071, 187)
        Me.odocpanelheader.Size = New System.Drawing.Size(1156, 187)
        Me.odocpanelheader.TabStop = False
        Me.odocpanelheader.Text = "Criteria"
        '
        'DockPanel1_Container
        '
        Me.DockPanel1_Container.Location = New System.Drawing.Point(2, 25)
        Me.DockPanel1_Container.Name = "DockPanel1_Container"
        Me.DockPanel1_Container.Size = New System.Drawing.Size(1152, 160)
        Me.DockPanel1_Container.TabIndex = 0
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
        'ogblisting
        '
        Me.ogblisting.AppearanceCaption.Options.UseTextOptions = True
        Me.ogblisting.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ogblisting.Controls.Add(Me.ogclisting)
        Me.ogblisting.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogblisting.Location = New System.Drawing.Point(0, 187)
        Me.ogblisting.Name = "ogblisting"
        Me.ogblisting.Size = New System.Drawing.Size(1156, 382)
        Me.ogblisting.TabIndex = 6
        Me.ogblisting.Text = "Group Listing Detail"
        '
        'ogclisting
        '
        Me.ogclisting.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogclisting.Location = New System.Drawing.Point(4, 23)
        Me.ogclisting.MainView = Me.ogvlisting
        Me.ogclisting.Name = "ogclisting"
        Me.ogclisting.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCheckEdit1, Me.RepositoryItemCheckEdit2})
        Me.ogclisting.Size = New System.Drawing.Size(1150, 354)
        Me.ogclisting.TabIndex = 3
        Me.ogclisting.TabStop = False
        Me.ogclisting.Tag = "2|"
        Me.ogclisting.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvlisting})
        '
        'ogvlisting
        '
        Me.ogvlisting.GridControl = Me.ogclisting
        Me.ogvlisting.GroupSummary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.None, "", Nothing, "")})
        Me.ogvlisting.Name = "ogvlisting"
        Me.ogvlisting.OptionsCustomization.AllowQuickHideColumns = False
        Me.ogvlisting.OptionsMenu.ShowGroupSummaryEditorItem = True
        Me.ogvlisting.OptionsView.ColumnAutoWidth = False
        Me.ogvlisting.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.ogvlisting.OptionsView.ShowFooter = True
        Me.ogvlisting.Tag = "2|"
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
        'Listing
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1156, 569)
        Me.Controls.Add(Me.ogblisting)
        Me.Controls.Add(Me.odocpanelheader)
        Me.Name = "Listing"
        Me.Text = "uXtraForm"
        CType(Me.oDockManager, System.ComponentModel.ISupportInitialize).EndInit()
        Me.odocpanelheader.ResumeLayout(False)
        CType(Me.RepositoryFTSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryFTApproveState, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogblisting, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogblisting.ResumeLayout(False)
        CType(Me.ogclisting, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvlisting, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents oDockManager As DevExpress.XtraBars.Docking.DockManager
    Friend WithEvents odocpanelheader As DevExpress.XtraBars.Docking.DockPanel
    Friend WithEvents DockPanel1_Container As DevExpress.XtraBars.Docking.ControlContainer
    Friend WithEvents ogblisting As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ogclisting As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvlisting As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents RepositoryItemCheckEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents RepositoryItemCheckEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents RepositoryFTSelect As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents RepositoryFTApproveState As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
End Class

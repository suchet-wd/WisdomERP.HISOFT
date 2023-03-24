<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wFactoryHubMappingSize
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
        Me.ogclist = New DevExpress.XtraGrid.GridControl()
        Me.ogvlist = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.CXSIZE_GRID_VALUE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CXFTMapSizeExtension = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ReposCFTSuplCode = New DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit()
        Me.RepositoryItemGridLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.XXFTMapSizeExtension = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepFTSelect = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.ogbcmd = New DevExpress.XtraEditors.GroupControl()
        Me.ocmsave = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.otb = New DevExpress.XtraTab.XtraTabControl()
        Me.otpmappingsize = New DevExpress.XtraTab.XtraTabPage()
        CType(Me.ogclist, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvlist, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ReposCFTSuplCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemGridLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepFTSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogbcmd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbcmd.SuspendLayout()
        CType(Me.otb, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.otb.SuspendLayout()
        Me.otpmappingsize.SuspendLayout()
        Me.SuspendLayout()
        '
        'ogclist
        '
        Me.ogclist.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogclist.Location = New System.Drawing.Point(0, 0)
        Me.ogclist.MainView = Me.ogvlist
        Me.ogclist.Name = "ogclist"
        Me.ogclist.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepFTSelect, Me.ReposCFTSuplCode})
        Me.ogclist.Size = New System.Drawing.Size(523, 513)
        Me.ogclist.TabIndex = 302
        Me.ogclist.TabStop = False
        Me.ogclist.Tag = "2|"
        Me.ogclist.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvlist})
        '
        'ogvlist
        '
        Me.ogvlist.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.CXSIZE_GRID_VALUE, Me.CXFTMapSizeExtension})
        Me.ogvlist.GridControl = Me.ogclist
        Me.ogvlist.Name = "ogvlist"
        Me.ogvlist.OptionsCustomization.AllowGroup = False
        Me.ogvlist.OptionsCustomization.AllowQuickHideColumns = False
        Me.ogvlist.OptionsView.ColumnAutoWidth = False
        Me.ogvlist.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.ogvlist.OptionsView.ShowGroupPanel = False
        Me.ogvlist.Tag = "2|"
        '
        'CXSIZE_GRID_VALUE
        '
        Me.CXSIZE_GRID_VALUE.Caption = "NIKE Size Breakdown"
        Me.CXSIZE_GRID_VALUE.FieldName = "SIZE_GRID_VALUE"
        Me.CXSIZE_GRID_VALUE.Name = "CXSIZE_GRID_VALUE"
        Me.CXSIZE_GRID_VALUE.OptionsColumn.AllowEdit = False
        Me.CXSIZE_GRID_VALUE.OptionsColumn.ReadOnly = True
        Me.CXSIZE_GRID_VALUE.Visible = True
        Me.CXSIZE_GRID_VALUE.VisibleIndex = 0
        Me.CXSIZE_GRID_VALUE.Width = 164
        '
        'CXFTMapSizeExtension
        '
        Me.CXFTMapSizeExtension.Caption = "Wisdom Size"
        Me.CXFTMapSizeExtension.ColumnEdit = Me.ReposCFTSuplCode
        Me.CXFTMapSizeExtension.FieldName = "FTMapSizeExtension"
        Me.CXFTMapSizeExtension.Name = "CXFTMapSizeExtension"
        Me.CXFTMapSizeExtension.Visible = True
        Me.CXFTMapSizeExtension.VisibleIndex = 1
        Me.CXFTMapSizeExtension.Width = 134
        '
        'ReposCFTSuplCode
        '
        Me.ReposCFTSuplCode.AutoHeight = False
        Me.ReposCFTSuplCode.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ReposCFTSuplCode.DisplayMember = "FTMapSizeExtension"
        Me.ReposCFTSuplCode.Name = "ReposCFTSuplCode"
        Me.ReposCFTSuplCode.NullText = ""
        Me.ReposCFTSuplCode.ValueMember = "FTMapSizeExtension"
        Me.ReposCFTSuplCode.View = Me.RepositoryItemGridLookUpEdit1View
        '
        'RepositoryItemGridLookUpEdit1View
        '
        Me.RepositoryItemGridLookUpEdit1View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.XXFTMapSizeExtension})
        Me.RepositoryItemGridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.RepositoryItemGridLookUpEdit1View.Name = "RepositoryItemGridLookUpEdit1View"
        Me.RepositoryItemGridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.RepositoryItemGridLookUpEdit1View.OptionsView.ShowAutoFilterRow = True
        Me.RepositoryItemGridLookUpEdit1View.OptionsView.ShowGroupPanel = False
        '
        'XXFTMapSizeExtension
        '
        Me.XXFTMapSizeExtension.Caption = "Wisdom Size"
        Me.XXFTMapSizeExtension.FieldName = "FTMapSizeExtension"
        Me.XXFTMapSizeExtension.Name = "XXFTMapSizeExtension"
        Me.XXFTMapSizeExtension.OptionsColumn.AllowEdit = False
        Me.XXFTMapSizeExtension.OptionsColumn.ReadOnly = True
        Me.XXFTMapSizeExtension.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        Me.XXFTMapSizeExtension.Visible = True
        Me.XXFTMapSizeExtension.VisibleIndex = 0
        Me.XXFTMapSizeExtension.Width = 120
        '
        'RepFTSelect
        '
        Me.RepFTSelect.AutoHeight = False
        Me.RepFTSelect.Caption = "Check"
        Me.RepFTSelect.Name = "RepFTSelect"
        Me.RepFTSelect.ValueChecked = "1"
        Me.RepFTSelect.ValueUnchecked = "0"
        '
        'ogbcmd
        '
        Me.ogbcmd.Controls.Add(Me.ocmsave)
        Me.ogbcmd.Controls.Add(Me.ocmexit)
        Me.ogbcmd.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ogbcmd.Location = New System.Drawing.Point(0, 541)
        Me.ogbcmd.Name = "ogbcmd"
        Me.ogbcmd.ShowCaption = False
        Me.ogbcmd.Size = New System.Drawing.Size(529, 43)
        Me.ogbcmd.TabIndex = 304
        Me.ogbcmd.Text = "GroupControl1"
        '
        'ocmsave
        '
        Me.ocmsave.Location = New System.Drawing.Point(62, 8)
        Me.ocmsave.Name = "ocmsave"
        Me.ocmsave.Size = New System.Drawing.Size(140, 25)
        Me.ocmsave.TabIndex = 96
        Me.ocmsave.TabStop = False
        Me.ocmsave.Tag = "2|"
        Me.ocmsave.Text = "SAVE"
        '
        'ocmexit
        '
        Me.ocmexit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmexit.Location = New System.Drawing.Point(316, 9)
        Me.ocmexit.Name = "ocmexit"
        Me.ocmexit.Size = New System.Drawing.Size(140, 25)
        Me.ocmexit.TabIndex = 95
        Me.ocmexit.TabStop = False
        Me.ocmexit.Tag = "2|"
        Me.ocmexit.Text = "EXIT"
        '
        'otb
        '
        Me.otb.Dock = System.Windows.Forms.DockStyle.Fill
        Me.otb.Location = New System.Drawing.Point(0, 0)
        Me.otb.Name = "otb"
        Me.otb.SelectedTabPage = Me.otpmappingsize
        Me.otb.Size = New System.Drawing.Size(529, 541)
        Me.otb.TabIndex = 305
        Me.otb.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.otpmappingsize})
        '
        'otpmappingsize
        '
        Me.otpmappingsize.Controls.Add(Me.ogclist)
        Me.otpmappingsize.Name = "otpmappingsize"
        Me.otpmappingsize.Size = New System.Drawing.Size(523, 513)
        Me.otpmappingsize.Text = "Size Mapping"
        '
        'wFactoryHubMappingSize
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(529, 584)
        Me.Controls.Add(Me.otb)
        Me.Controls.Add(Me.ogbcmd)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "wFactoryHubMappingSize"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Mapping Size Breakdown"
        CType(Me.ogclist, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvlist, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ReposCFTSuplCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemGridLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepFTSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogbcmd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbcmd.ResumeLayout(False)
        CType(Me.otb, System.ComponentModel.ISupportInitialize).EndInit()
        Me.otb.ResumeLayout(False)
        Me.otpmappingsize.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RepFTSelect As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents ogbcmd As DevExpress.XtraEditors.GroupControl
    Public WithEvents ocmsave As DevExpress.XtraEditors.SimpleButton
    Public WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
    Public WithEvents ogclist As DevExpress.XtraGrid.GridControl
    Public WithEvents ogvlist As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents CXSIZE_GRID_VALUE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CXFTMapSizeExtension As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ReposCFTSuplCode As DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit
    Friend WithEvents RepositoryItemGridLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents XXFTMapSizeExtension As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents otb As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents otpmappingsize As DevExpress.XtraTab.XtraTabPage
End Class

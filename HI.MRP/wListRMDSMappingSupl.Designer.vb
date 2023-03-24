<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wListRMDSMappingSupl
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
        Me.CFTSupplierLocationCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFTSuplCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ReposCFTSuplCode = New DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit()
        Me.RepositoryItemGridLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.CCFTSuplCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CCFTSuplName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CCFNHSysSuplId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFTSuplName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFNHSysSuplId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepFTSelect = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.ogbcmd = New DevExpress.XtraEditors.GroupControl()
        Me.ocmsave = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.otb = New DevExpress.XtraTab.XtraTabControl()
        Me.otpmappingsupplier = New DevExpress.XtraTab.XtraTabPage()
        Me.otpunitmapping = New DevExpress.XtraTab.XtraTabPage()
        Me.ogclistunit = New DevExpress.XtraGrid.GridControl()
        Me.ogvlistunit = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.CXFTSellingUOM = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CXFNQty = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CXFTUOMDesc = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CXFTUnitCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryFTUnitCode = New DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit()
        Me.GridView3 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.DDFTUnitCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CCFTUnitName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNHSysUnitIdX = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CXFNHSysUnitId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCheckEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.CXFTSupplierLocationName = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.ogclist, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvlist, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ReposCFTSuplCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemGridLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepFTSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogbcmd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbcmd.SuspendLayout()
        CType(Me.otb, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.otb.SuspendLayout()
        Me.otpmappingsupplier.SuspendLayout()
        Me.otpunitmapping.SuspendLayout()
        CType(Me.ogclistunit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvlistunit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryFTUnitCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ogclist
        '
        Me.ogclist.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogclist.Location = New System.Drawing.Point(0, 0)
        Me.ogclist.MainView = Me.ogvlist
        Me.ogclist.Name = "ogclist"
        Me.ogclist.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepFTSelect, Me.ReposCFTSuplCode})
        Me.ogclist.Size = New System.Drawing.Size(716, 513)
        Me.ogclist.TabIndex = 302
        Me.ogclist.TabStop = False
        Me.ogclist.Tag = "2|"
        Me.ogclist.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvlist})
        '
        'ogvlist
        '
        Me.ogvlist.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.CFTSupplierLocationCode, Me.CXFTSupplierLocationName, Me.CFTSuplCode, Me.CFTSuplName, Me.CFNHSysSuplId})
        Me.ogvlist.GridControl = Me.ogclist
        Me.ogvlist.Name = "ogvlist"
        Me.ogvlist.OptionsCustomization.AllowGroup = False
        Me.ogvlist.OptionsCustomization.AllowQuickHideColumns = False
        Me.ogvlist.OptionsView.ColumnAutoWidth = False
        Me.ogvlist.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.ogvlist.OptionsView.ShowGroupPanel = False
        Me.ogvlist.Tag = "2|"
        '
        'CFTSupplierLocationCode
        '
        Me.CFTSupplierLocationCode.Caption = "RMDS Supl Code"
        Me.CFTSupplierLocationCode.FieldName = "FTSupplierLocationCode"
        Me.CFTSupplierLocationCode.Name = "CFTSupplierLocationCode"
        Me.CFTSupplierLocationCode.OptionsColumn.AllowEdit = False
        Me.CFTSupplierLocationCode.OptionsColumn.ReadOnly = True
        Me.CFTSupplierLocationCode.Visible = True
        Me.CFTSupplierLocationCode.VisibleIndex = 0
        Me.CFTSupplierLocationCode.Width = 101
        '
        'CFTSuplCode
        '
        Me.CFTSuplCode.Caption = "SuplCode"
        Me.CFTSuplCode.ColumnEdit = Me.ReposCFTSuplCode
        Me.CFTSuplCode.FieldName = "FTSuplCode"
        Me.CFTSuplCode.Name = "CFTSuplCode"
        Me.CFTSuplCode.Visible = True
        Me.CFTSuplCode.VisibleIndex = 2
        Me.CFTSuplCode.Width = 134
        '
        'ReposCFTSuplCode
        '
        Me.ReposCFTSuplCode.AutoHeight = False
        Me.ReposCFTSuplCode.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ReposCFTSuplCode.DisplayMember = "FTSuplCode"
        Me.ReposCFTSuplCode.Name = "ReposCFTSuplCode"
        Me.ReposCFTSuplCode.NullText = ""
        Me.ReposCFTSuplCode.ValueMember = "FTSuplCode"
        Me.ReposCFTSuplCode.View = Me.RepositoryItemGridLookUpEdit1View
        '
        'RepositoryItemGridLookUpEdit1View
        '
        Me.RepositoryItemGridLookUpEdit1View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.CCFTSuplCode, Me.CCFTSuplName, Me.CCFNHSysSuplId})
        Me.RepositoryItemGridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.RepositoryItemGridLookUpEdit1View.Name = "RepositoryItemGridLookUpEdit1View"
        Me.RepositoryItemGridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.RepositoryItemGridLookUpEdit1View.OptionsView.ShowAutoFilterRow = True
        Me.RepositoryItemGridLookUpEdit1View.OptionsView.ShowGroupPanel = False
        '
        'CCFTSuplCode
        '
        Me.CCFTSuplCode.Caption = "Supl Code"
        Me.CCFTSuplCode.FieldName = "FTSuplCode"
        Me.CCFTSuplCode.Name = "CCFTSuplCode"
        Me.CCFTSuplCode.OptionsColumn.AllowEdit = False
        Me.CCFTSuplCode.OptionsColumn.ReadOnly = True
        Me.CCFTSuplCode.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        Me.CCFTSuplCode.Visible = True
        Me.CCFTSuplCode.VisibleIndex = 0
        Me.CCFTSuplCode.Width = 120
        '
        'CCFTSuplName
        '
        Me.CCFTSuplName.Caption = "Supl Name"
        Me.CCFTSuplName.FieldName = "FTSuplName"
        Me.CCFTSuplName.Name = "CCFTSuplName"
        Me.CCFTSuplName.OptionsColumn.AllowEdit = False
        Me.CCFTSuplName.OptionsColumn.ReadOnly = True
        Me.CCFTSuplName.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        Me.CCFTSuplName.Visible = True
        Me.CCFTSuplName.VisibleIndex = 1
        Me.CCFTSuplName.Width = 200
        '
        'CCFNHSysSuplId
        '
        Me.CCFNHSysSuplId.Caption = "FNHSysSuplId"
        Me.CCFNHSysSuplId.FieldName = "FNHSysSuplId"
        Me.CCFNHSysSuplId.Name = "CCFNHSysSuplId"
        Me.CCFNHSysSuplId.OptionsColumn.AllowEdit = False
        Me.CCFNHSysSuplId.OptionsColumn.ReadOnly = True
        '
        'CFTSuplName
        '
        Me.CFTSuplName.Caption = "Supl Name"
        Me.CFTSuplName.FieldName = "FTSuplName"
        Me.CFTSuplName.Name = "CFTSuplName"
        Me.CFTSuplName.OptionsColumn.AllowEdit = False
        Me.CFTSuplName.OptionsColumn.ReadOnly = True
        Me.CFTSuplName.Visible = True
        Me.CFTSuplName.VisibleIndex = 3
        Me.CFTSuplName.Width = 250
        '
        'CFNHSysSuplId
        '
        Me.CFNHSysSuplId.Caption = "FNHSysSuplId"
        Me.CFNHSysSuplId.FieldName = "FNHSysSuplId"
        Me.CFNHSysSuplId.Name = "CFNHSysSuplId"
        Me.CFNHSysSuplId.OptionsColumn.AllowEdit = False
        Me.CFNHSysSuplId.OptionsColumn.ReadOnly = True
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
        Me.ogbcmd.Size = New System.Drawing.Size(722, 43)
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
        Me.ocmexit.Location = New System.Drawing.Point(509, 9)
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
        Me.otb.SelectedTabPage = Me.otpmappingsupplier
        Me.otb.Size = New System.Drawing.Size(722, 541)
        Me.otb.TabIndex = 305
        Me.otb.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.otpmappingsupplier, Me.otpunitmapping})
        '
        'otpmappingsupplier
        '
        Me.otpmappingsupplier.Controls.Add(Me.ogclist)
        Me.otpmappingsupplier.Name = "otpmappingsupplier"
        Me.otpmappingsupplier.Size = New System.Drawing.Size(716, 513)
        Me.otpmappingsupplier.Text = "Supplier Mapping"
        '
        'otpunitmapping
        '
        Me.otpunitmapping.Controls.Add(Me.ogclistunit)
        Me.otpunitmapping.Name = "otpunitmapping"
        Me.otpunitmapping.Size = New System.Drawing.Size(523, 513)
        Me.otpunitmapping.Text = "Unit Mapping"
        '
        'ogclistunit
        '
        Me.ogclistunit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogclistunit.Location = New System.Drawing.Point(0, 0)
        Me.ogclistunit.MainView = Me.ogvlistunit
        Me.ogclistunit.Name = "ogclistunit"
        Me.ogclistunit.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCheckEdit1, Me.RepositoryFTUnitCode})
        Me.ogclistunit.Size = New System.Drawing.Size(523, 513)
        Me.ogclistunit.TabIndex = 303
        Me.ogclistunit.TabStop = False
        Me.ogclistunit.Tag = "2|"
        Me.ogclistunit.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvlistunit})
        '
        'ogvlistunit
        '
        Me.ogvlistunit.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.CXFTSellingUOM, Me.CXFNQty, Me.CXFTUOMDesc, Me.CXFTUnitCode, Me.GridColumn6, Me.CXFNHSysUnitId})
        Me.ogvlistunit.GridControl = Me.ogclistunit
        Me.ogvlistunit.Name = "ogvlistunit"
        Me.ogvlistunit.OptionsCustomization.AllowGroup = False
        Me.ogvlistunit.OptionsCustomization.AllowQuickHideColumns = False
        Me.ogvlistunit.OptionsView.ColumnAutoWidth = False
        Me.ogvlistunit.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.ogvlistunit.OptionsView.ShowGroupPanel = False
        Me.ogvlistunit.Tag = "2|"
        '
        'CXFTSellingUOM
        '
        Me.CXFTSellingUOM.Caption = "RMDS Selling UOM"
        Me.CXFTSellingUOM.FieldName = "FTSellingUOM"
        Me.CXFTSellingUOM.Name = "CXFTSellingUOM"
        Me.CXFTSellingUOM.OptionsColumn.AllowEdit = False
        Me.CXFTSellingUOM.OptionsColumn.ReadOnly = True
        Me.CXFTSellingUOM.Visible = True
        Me.CXFTSellingUOM.VisibleIndex = 0
        Me.CXFTSellingUOM.Width = 128
        '
        'CXFNQty
        '
        Me.CXFNQty.Caption = "Qty"
        Me.CXFNQty.FieldName = "FNQty"
        Me.CXFNQty.Name = "CXFNQty"
        Me.CXFNQty.Visible = True
        Me.CXFNQty.VisibleIndex = 1
        '
        'CXFTUOMDesc
        '
        Me.CXFTUOMDesc.Caption = "UOM Desc"
        Me.CXFTUOMDesc.FieldName = "FTUOMDesc"
        Me.CXFTUOMDesc.Name = "CXFTUOMDesc"
        Me.CXFTUOMDesc.Visible = True
        Me.CXFTUOMDesc.VisibleIndex = 2
        Me.CXFTUOMDesc.Width = 148
        '
        'CXFTUnitCode
        '
        Me.CXFTUnitCode.Caption = "Unit Code"
        Me.CXFTUnitCode.ColumnEdit = Me.RepositoryFTUnitCode
        Me.CXFTUnitCode.FieldName = "FTUnitCode"
        Me.CXFTUnitCode.Name = "CXFTUnitCode"
        Me.CXFTUnitCode.Visible = True
        Me.CXFTUnitCode.VisibleIndex = 3
        Me.CXFTUnitCode.Width = 120
        '
        'RepositoryFTUnitCode
        '
        Me.RepositoryFTUnitCode.AutoHeight = False
        Me.RepositoryFTUnitCode.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryFTUnitCode.DisplayMember = "FTUnitCode"
        Me.RepositoryFTUnitCode.Name = "RepositoryFTUnitCode"
        Me.RepositoryFTUnitCode.NullText = ""
        Me.RepositoryFTUnitCode.ValueMember = "FTUnitCode"
        Me.RepositoryFTUnitCode.View = Me.GridView3
        '
        'GridView3
        '
        Me.GridView3.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.DDFTUnitCode, Me.CCFTUnitName, Me.FNHSysUnitIdX})
        Me.GridView3.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView3.Name = "GridView3"
        Me.GridView3.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView3.OptionsView.ShowAutoFilterRow = True
        Me.GridView3.OptionsView.ShowGroupPanel = False
        '
        'DDFTUnitCode
        '
        Me.DDFTUnitCode.Caption = "Unit Code"
        Me.DDFTUnitCode.FieldName = "FTUnitCode"
        Me.DDFTUnitCode.Name = "DDFTUnitCode"
        Me.DDFTUnitCode.OptionsColumn.AllowEdit = False
        Me.DDFTUnitCode.OptionsColumn.ReadOnly = True
        Me.DDFTUnitCode.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        Me.DDFTUnitCode.Visible = True
        Me.DDFTUnitCode.VisibleIndex = 0
        Me.DDFTUnitCode.Width = 120
        '
        'CCFTUnitName
        '
        Me.CCFTUnitName.Caption = "FTUnitName"
        Me.CCFTUnitName.FieldName = "FTUnitName"
        Me.CCFTUnitName.Name = "CCFTUnitName"
        Me.CCFTUnitName.OptionsColumn.AllowEdit = False
        Me.CCFTUnitName.OptionsColumn.ReadOnly = True
        Me.CCFTUnitName.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        Me.CCFTUnitName.Width = 200
        '
        'FNHSysUnitIdX
        '
        Me.FNHSysUnitIdX.Caption = "FNHSysUnitId"
        Me.FNHSysUnitIdX.FieldName = "FNHSysUnitId"
        Me.FNHSysUnitIdX.Name = "FNHSysUnitIdX"
        Me.FNHSysUnitIdX.OptionsColumn.AllowEdit = False
        Me.FNHSysUnitIdX.OptionsColumn.ReadOnly = True
        '
        'GridColumn6
        '
        Me.GridColumn6.Caption = "FTUnitName"
        Me.GridColumn6.FieldName = "FTUnitName"
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.OptionsColumn.AllowEdit = False
        Me.GridColumn6.OptionsColumn.ReadOnly = True
        Me.GridColumn6.Width = 250
        '
        'CXFNHSysUnitId
        '
        Me.CXFNHSysUnitId.Caption = "FNHSysUnitId"
        Me.CXFNHSysUnitId.FieldName = "FNHSysUnitId"
        Me.CXFNHSysUnitId.Name = "CXFNHSysUnitId"
        Me.CXFNHSysUnitId.OptionsColumn.AllowEdit = False
        Me.CXFNHSysUnitId.OptionsColumn.ReadOnly = True
        '
        'RepositoryItemCheckEdit1
        '
        Me.RepositoryItemCheckEdit1.AutoHeight = False
        Me.RepositoryItemCheckEdit1.Caption = "Check"
        Me.RepositoryItemCheckEdit1.Name = "RepositoryItemCheckEdit1"
        Me.RepositoryItemCheckEdit1.ValueChecked = "1"
        Me.RepositoryItemCheckEdit1.ValueUnchecked = "0"
        '
        'CXFTSupplierLocationName
        '
        Me.CXFTSupplierLocationName.Caption = "Supplier Location Name"
        Me.CXFTSupplierLocationName.FieldName = "FTSupplierLocationName"
        Me.CXFTSupplierLocationName.Name = "CXFTSupplierLocationName"
        Me.CXFTSupplierLocationName.OptionsColumn.AllowEdit = False
        Me.CXFTSupplierLocationName.OptionsColumn.ReadOnly = True
        Me.CXFTSupplierLocationName.Visible = True
        Me.CXFTSupplierLocationName.VisibleIndex = 1
        Me.CXFTSupplierLocationName.Width = 201
        '
        'wListRMDSMappingSupl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(722, 584)
        Me.Controls.Add(Me.otb)
        Me.Controls.Add(Me.ogbcmd)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "wListRMDSMappingSupl"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "RMDS Mapping Supplier"
        CType(Me.ogclist, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvlist, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ReposCFTSuplCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemGridLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepFTSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogbcmd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbcmd.ResumeLayout(False)
        CType(Me.otb, System.ComponentModel.ISupportInitialize).EndInit()
        Me.otb.ResumeLayout(False)
        Me.otpmappingsupplier.ResumeLayout(False)
        Me.otpunitmapping.ResumeLayout(False)
        CType(Me.ogclistunit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvlistunit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryFTUnitCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RepFTSelect As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents ogbcmd As DevExpress.XtraEditors.GroupControl
    Public WithEvents ocmsave As DevExpress.XtraEditors.SimpleButton
    Public WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
    Public WithEvents ogclist As DevExpress.XtraGrid.GridControl
    Public WithEvents ogvlist As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents CFTSupplierLocationCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFTSuplCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ReposCFTSuplCode As DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit
    Friend WithEvents RepositoryItemGridLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents CCFTSuplCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CCFTSuplName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CCFNHSysSuplId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFTSuplName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFNHSysSuplId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents otb As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents otpmappingsupplier As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents otpunitmapping As DevExpress.XtraTab.XtraTabPage
    Public WithEvents ogclistunit As DevExpress.XtraGrid.GridControl
    Public WithEvents ogvlistunit As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents CXFTSellingUOM As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CXFTUnitCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryFTUnitCode As DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit
    Friend WithEvents GridView3 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents DDFTUnitCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CCFTUnitName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNHSysUnitIdX As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CXFNHSysUnitId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemCheckEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents CXFNQty As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CXFTUOMDesc As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CXFTSupplierLocationName As DevExpress.XtraGrid.Columns.GridColumn
End Class

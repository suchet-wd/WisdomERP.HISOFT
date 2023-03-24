<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wOrganizationChartBelongTo_Select
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
        Dim GridLevelNode1 As DevExpress.XtraGrid.GridLevelNode = New DevExpress.XtraGrid.GridLevelNode()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.ogcorganize = New DevExpress.XtraGrid.GridControl()
        Me.ogvorganize = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.FNHSysOrgId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTCountryName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTCmpCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTCmpName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTDivisonCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTDivisonName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTDeptCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTDeptDesc = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTSectCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTSectName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTUnitSectCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTUnitSectName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTPositCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTPositName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTStateActive = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCheckEdit2 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.FTStateLeader = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCheckEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.GridView2 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GroupControl2 = New DevExpress.XtraEditors.GroupControl()
        Me.FTCLevelName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTCLevelCode = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogcorganize, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvorganize, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl2.SuspendLayout()
        Me.SuspendLayout()
        '
        'GridView1
        '
        Me.GridView1.GridControl = Me.ogcorganize
        Me.GridView1.Name = "GridView1"
        '
        'ogcorganize
        '
        Me.ogcorganize.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogcorganize.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        GridLevelNode1.RelationName = "Level1"
        Me.ogcorganize.LevelTree.Nodes.AddRange(New DevExpress.XtraGrid.GridLevelNode() {GridLevelNode1})
        Me.ogcorganize.Location = New System.Drawing.Point(2, 25)
        Me.ogcorganize.MainView = Me.ogvorganize
        Me.ogcorganize.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcorganize.Name = "ogcorganize"
        Me.ogcorganize.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCheckEdit1, Me.RepositoryItemCheckEdit2})
        Me.ogcorganize.Size = New System.Drawing.Size(1478, 677)
        Me.ogcorganize.TabIndex = 328
        Me.ogcorganize.TabStop = False
        Me.ogcorganize.Tag = "2|"
        Me.ogcorganize.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvorganize, Me.GridView2, Me.GridView1})
        '
        'ogvorganize
        '
        Me.ogvorganize.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.FNHSysOrgId, Me.FTCLevelCode, Me.FTCLevelName, Me.FTCountryName, Me.FTCmpCode, Me.FTCmpName, Me.FTDivisonCode, Me.FTDivisonName, Me.FTDeptCode, Me.FTDeptDesc, Me.FTSectCode, Me.FTSectName, Me.FTUnitSectCode, Me.FTUnitSectName, Me.FTPositCode, Me.FTPositName, Me.FTStateActive, Me.FTStateLeader})
        Me.ogvorganize.GridControl = Me.ogcorganize
        Me.ogvorganize.Name = "ogvorganize"
        Me.ogvorganize.OptionsCustomization.AllowGroup = False
        Me.ogvorganize.OptionsCustomization.AllowQuickHideColumns = False
        Me.ogvorganize.OptionsView.ColumnAutoWidth = False
        Me.ogvorganize.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.ogvorganize.OptionsView.ShowAutoFilterRow = True
        Me.ogvorganize.OptionsView.ShowGroupPanel = False
        Me.ogvorganize.Tag = "2|"
        '
        'FNHSysOrgId
        '
        Me.FNHSysOrgId.Caption = "FNHSysOrgId"
        Me.FNHSysOrgId.FieldName = "FNHSysOrgId"
        Me.FNHSysOrgId.Name = "FNHSysOrgId"
        Me.FNHSysOrgId.OptionsColumn.AllowEdit = False
        '
        'FTCountryName
        '
        Me.FTCountryName.Caption = "FTCountryName"
        Me.FTCountryName.FieldName = "FTCountryName"
        Me.FTCountryName.Name = "FTCountryName"
        Me.FTCountryName.OptionsColumn.AllowEdit = False
        Me.FTCountryName.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        Me.FTCountryName.Visible = True
        Me.FTCountryName.VisibleIndex = 0
        '
        'FTCmpCode
        '
        Me.FTCmpCode.Caption = "FTCmpCode"
        Me.FTCmpCode.FieldName = "FTCmpCode"
        Me.FTCmpCode.Name = "FTCmpCode"
        Me.FTCmpCode.OptionsColumn.AllowEdit = False
        Me.FTCmpCode.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        Me.FTCmpCode.Visible = True
        Me.FTCmpCode.VisibleIndex = 3
        '
        'FTCmpName
        '
        Me.FTCmpName.Caption = "FTCmpName"
        Me.FTCmpName.FieldName = "FTCmpName"
        Me.FTCmpName.Name = "FTCmpName"
        Me.FTCmpName.OptionsColumn.AllowEdit = False
        Me.FTCmpName.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        Me.FTCmpName.Visible = True
        Me.FTCmpName.VisibleIndex = 4
        '
        'FTDivisonCode
        '
        Me.FTDivisonCode.Caption = "FTDivisonCode"
        Me.FTDivisonCode.FieldName = "FTDivisonCode"
        Me.FTDivisonCode.Name = "FTDivisonCode"
        Me.FTDivisonCode.OptionsColumn.AllowEdit = False
        Me.FTDivisonCode.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        Me.FTDivisonCode.Visible = True
        Me.FTDivisonCode.VisibleIndex = 5
        '
        'FTDivisonName
        '
        Me.FTDivisonName.Caption = "FTDivisonName"
        Me.FTDivisonName.FieldName = "FTDivisonName"
        Me.FTDivisonName.Name = "FTDivisonName"
        Me.FTDivisonName.OptionsColumn.AllowEdit = False
        Me.FTDivisonName.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        Me.FTDivisonName.Visible = True
        Me.FTDivisonName.VisibleIndex = 6
        '
        'FTDeptCode
        '
        Me.FTDeptCode.Caption = "FTDeptCode"
        Me.FTDeptCode.FieldName = "FTDeptCode"
        Me.FTDeptCode.Name = "FTDeptCode"
        Me.FTDeptCode.OptionsColumn.AllowEdit = False
        Me.FTDeptCode.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        Me.FTDeptCode.Visible = True
        Me.FTDeptCode.VisibleIndex = 7
        '
        'FTDeptDesc
        '
        Me.FTDeptDesc.Caption = "FTDeptDesc"
        Me.FTDeptDesc.FieldName = "FTDeptDesc"
        Me.FTDeptDesc.Name = "FTDeptDesc"
        Me.FTDeptDesc.OptionsColumn.AllowEdit = False
        Me.FTDeptDesc.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        Me.FTDeptDesc.Visible = True
        Me.FTDeptDesc.VisibleIndex = 8
        '
        'FTSectCode
        '
        Me.FTSectCode.Caption = "FTSectCode"
        Me.FTSectCode.FieldName = "FTSectCode"
        Me.FTSectCode.Name = "FTSectCode"
        Me.FTSectCode.OptionsColumn.AllowEdit = False
        Me.FTSectCode.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        Me.FTSectCode.Visible = True
        Me.FTSectCode.VisibleIndex = 9
        '
        'FTSectName
        '
        Me.FTSectName.Caption = "FTSectName"
        Me.FTSectName.FieldName = "FTSectName"
        Me.FTSectName.Name = "FTSectName"
        Me.FTSectName.OptionsColumn.AllowEdit = False
        Me.FTSectName.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        Me.FTSectName.Visible = True
        Me.FTSectName.VisibleIndex = 10
        '
        'FTUnitSectCode
        '
        Me.FTUnitSectCode.Caption = "FTUnitSectCode"
        Me.FTUnitSectCode.FieldName = "FTUnitSectCode"
        Me.FTUnitSectCode.Name = "FTUnitSectCode"
        Me.FTUnitSectCode.OptionsColumn.AllowEdit = False
        Me.FTUnitSectCode.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        Me.FTUnitSectCode.Visible = True
        Me.FTUnitSectCode.VisibleIndex = 11
        '
        'FTUnitSectName
        '
        Me.FTUnitSectName.Caption = "FTUnitSectName"
        Me.FTUnitSectName.FieldName = "FTUnitSectName"
        Me.FTUnitSectName.Name = "FTUnitSectName"
        Me.FTUnitSectName.OptionsColumn.AllowEdit = False
        Me.FTUnitSectName.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        Me.FTUnitSectName.Visible = True
        Me.FTUnitSectName.VisibleIndex = 12
        '
        'FTPositCode
        '
        Me.FTPositCode.Caption = "FTPositCode"
        Me.FTPositCode.FieldName = "FTPositCode"
        Me.FTPositCode.Name = "FTPositCode"
        Me.FTPositCode.OptionsColumn.AllowEdit = False
        Me.FTPositCode.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        Me.FTPositCode.Visible = True
        Me.FTPositCode.VisibleIndex = 13
        '
        'FTPositName
        '
        Me.FTPositName.Caption = "FTPositName"
        Me.FTPositName.FieldName = "FTPositName"
        Me.FTPositName.Name = "FTPositName"
        Me.FTPositName.OptionsColumn.AllowEdit = False
        Me.FTPositName.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        Me.FTPositName.Visible = True
        Me.FTPositName.VisibleIndex = 14
        '
        'FTStateActive
        '
        Me.FTStateActive.Caption = "FTStateActive"
        Me.FTStateActive.ColumnEdit = Me.RepositoryItemCheckEdit2
        Me.FTStateActive.FieldName = "FTStateActive"
        Me.FTStateActive.Name = "FTStateActive"
        '
        'RepositoryItemCheckEdit2
        '
        Me.RepositoryItemCheckEdit2.AutoHeight = False
        Me.RepositoryItemCheckEdit2.Name = "RepositoryItemCheckEdit2"
        Me.RepositoryItemCheckEdit2.ValueChecked = "1"
        Me.RepositoryItemCheckEdit2.ValueUnchecked = "0"
        '
        'FTStateLeader
        '
        Me.FTStateLeader.Caption = "FTStateLeader"
        Me.FTStateLeader.ColumnEdit = Me.RepositoryItemCheckEdit1
        Me.FTStateLeader.FieldName = "FTStateLeader"
        Me.FTStateLeader.Name = "FTStateLeader"
        Me.FTStateLeader.Visible = True
        Me.FTStateLeader.VisibleIndex = 15
        '
        'RepositoryItemCheckEdit1
        '
        Me.RepositoryItemCheckEdit1.AutoHeight = False
        Me.RepositoryItemCheckEdit1.Name = "RepositoryItemCheckEdit1"
        Me.RepositoryItemCheckEdit1.ValueChecked = "1"
        Me.RepositoryItemCheckEdit1.ValueUnchecked = "0"
        '
        'GridView2
        '
        Me.GridView2.GridControl = Me.ogcorganize
        Me.GridView2.Name = "GridView2"
        '
        'GroupControl2
        '
        Me.GroupControl2.Controls.Add(Me.ogcorganize)
        Me.GroupControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl2.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl2.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupControl2.Name = "GroupControl2"
        Me.GroupControl2.Size = New System.Drawing.Size(1482, 704)
        Me.GroupControl2.TabIndex = 331
        Me.GroupControl2.Tag = "2|"
        Me.GroupControl2.Text = "สายการบังคับบัญชา"
        '
        'FTCLevelName
        '
        Me.FTCLevelName.Caption = "FTCLevelName"
        Me.FTCLevelName.FieldName = "FTCLevelName"
        Me.FTCLevelName.Name = "FTCLevelName"
        Me.FTCLevelName.OptionsColumn.AllowEdit = False
        Me.FTCLevelName.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        Me.FTCLevelName.Visible = True
        Me.FTCLevelName.VisibleIndex = 2
        '
        'FTCLevelCode
        '
        Me.FTCLevelCode.Caption = "FTCLevelCode"
        Me.FTCLevelCode.FieldName = "FTCLevelCode"
        Me.FTCLevelCode.Name = "FTCLevelCode"
        Me.FTCLevelCode.OptionsColumn.AllowEdit = False
        Me.FTCLevelCode.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        Me.FTCLevelCode.Visible = True
        Me.FTCLevelCode.VisibleIndex = 1
        '
        'wOrganizationChartBelongTo_Select
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1482, 704)
        Me.Controls.Add(Me.GroupControl2)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "wOrganizationChartBelongTo_Select"
        Me.Text = "โครงสร้างผังองค์กร"
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogcorganize, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvorganize, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents ogcorganize As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvorganize As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GroupControl2 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents GridView2 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents FNHSysOrgId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTCountryName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTCmpCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTCmpName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTDivisonCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTDivisonName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTDeptCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTDeptDesc As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTSectCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTSectName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTUnitSectCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTUnitSectName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTPositCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTPositName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTStateActive As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTStateLeader As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemCheckEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents RepositoryItemCheckEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents FTCLevelCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTCLevelName As DevExpress.XtraGrid.Columns.GridColumn
End Class

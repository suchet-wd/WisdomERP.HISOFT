<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wOrganizationChartMapping
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
        Me.ogbmainprocbutton = New DevExpress.XtraEditors.PanelControl()
        Me.ocmload = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmadd = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.ogcorganize = New DevExpress.XtraGrid.GridControl()
        Me.ogvorganize = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.FNHSysOrgId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNHSysEmpID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTEmpCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTEmpName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTCLevelCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTCLevelName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTCountryCode = New DevExpress.XtraGrid.Columns.GridColumn()
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
        Me.RepositoryItemCheckEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.FNPercentageQty = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTRemark = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNHSysOrgMapId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridView2 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GroupControl2 = New DevExpress.XtraEditors.GroupControl()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbmainprocbutton.SuspendLayout()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogcorganize, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvorganize, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl2.SuspendLayout()
        Me.SuspendLayout()
        '
        'ogbmainprocbutton
        '
        Me.ogbmainprocbutton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmload)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmadd)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmexit)
        Me.ogbmainprocbutton.Location = New System.Drawing.Point(90, 463)
        Me.ogbmainprocbutton.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbmainprocbutton.Name = "ogbmainprocbutton"
        Me.ogbmainprocbutton.Size = New System.Drawing.Size(828, 64)
        Me.ogbmainprocbutton.TabIndex = 386
        Me.ogbmainprocbutton.Tag = "2|"
        '
        'ocmload
        '
        Me.ocmload.Location = New System.Drawing.Point(162, 17)
        Me.ocmload.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmload.Name = "ocmload"
        Me.ocmload.Size = New System.Drawing.Size(112, 28)
        Me.ocmload.TabIndex = 330
        Me.ocmload.Text = "Load Data"
        '
        'ocmadd
        '
        Me.ocmadd.Location = New System.Drawing.Point(19, 13)
        Me.ocmadd.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmadd.Name = "ocmadd"
        Me.ocmadd.Size = New System.Drawing.Size(111, 31)
        Me.ocmadd.TabIndex = 97
        Me.ocmadd.TabStop = False
        Me.ocmadd.Tag = "2|"
        Me.ocmadd.Text = "ADD"
        '
        'ocmexit
        '
        Me.ocmexit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmexit.Location = New System.Drawing.Point(683, 13)
        Me.ocmexit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmexit.Name = "ocmexit"
        Me.ocmexit.Size = New System.Drawing.Size(111, 31)
        Me.ocmexit.TabIndex = 96
        Me.ocmexit.TabStop = False
        Me.ocmexit.Tag = "2|"
        Me.ocmexit.Text = "EXIT"
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
        Me.ogcorganize.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCheckEdit1})
        Me.ogcorganize.Size = New System.Drawing.Size(1330, 655)
        Me.ogcorganize.TabIndex = 328
        Me.ogcorganize.TabStop = False
        Me.ogcorganize.Tag = " "
        Me.ogcorganize.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvorganize, Me.GridView2, Me.GridView1})
        '
        'ogvorganize
        '
        Me.ogvorganize.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.FNHSysOrgId, Me.FNHSysEmpID, Me.FTEmpCode, Me.FTEmpName, Me.FTCLevelCode, Me.FTCLevelName, Me.FTCountryCode, Me.FTCountryName, Me.FTCmpCode, Me.FTCmpName, Me.FTDivisonCode, Me.FTDivisonName, Me.FTDeptCode, Me.FTDeptDesc, Me.FTSectCode, Me.FTSectName, Me.FTUnitSectCode, Me.FTUnitSectName, Me.FTPositCode, Me.FTPositName, Me.FTStateActive, Me.FNPercentageQty, Me.FTRemark, Me.FNHSysOrgMapId})
        Me.ogvorganize.GridControl = Me.ogcorganize
        Me.ogvorganize.Name = "ogvorganize"
        Me.ogvorganize.OptionsCustomization.AllowGroup = False
        Me.ogvorganize.OptionsCustomization.AllowQuickHideColumns = False
        Me.ogvorganize.OptionsView.AllowCellMerge = True
        Me.ogvorganize.OptionsView.ColumnAutoWidth = False
        Me.ogvorganize.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.ogvorganize.OptionsView.ShowGroupPanel = False
        Me.ogvorganize.Tag = "2|"
        '
        'FNHSysOrgId
        '
        Me.FNHSysOrgId.Caption = "FNHSysOrgId"
        Me.FNHSysOrgId.FieldName = "FNHSysOrgId"
        Me.FNHSysOrgId.Name = "FNHSysOrgId"
        '
        'FNHSysEmpID
        '
        Me.FNHSysEmpID.Caption = "FNHSysEmpID"
        Me.FNHSysEmpID.FieldName = "FNHSysEmpID"
        Me.FNHSysEmpID.Name = "FNHSysEmpID"
        '
        'FTEmpCode
        '
        Me.FTEmpCode.Caption = "FTEmpCode"
        Me.FTEmpCode.FieldName = "FTEmpCode"
        Me.FTEmpCode.Name = "FTEmpCode"
        Me.FTEmpCode.OptionsColumn.AllowEdit = False
        Me.FTEmpCode.Visible = True
        Me.FTEmpCode.VisibleIndex = 16
        '
        'FTEmpName
        '
        Me.FTEmpName.Caption = "FTEmpName"
        Me.FTEmpName.FieldName = "FTEmpName"
        Me.FTEmpName.Name = "FTEmpName"
        Me.FTEmpName.OptionsColumn.AllowEdit = False
        Me.FTEmpName.Visible = True
        Me.FTEmpName.VisibleIndex = 17
        '
        'FTCLevelCode
        '
        Me.FTCLevelCode.Caption = "FTCLevelCode"
        Me.FTCLevelCode.FieldName = "FTCLevelCode"
        Me.FTCLevelCode.Name = "FTCLevelCode"
        Me.FTCLevelCode.OptionsColumn.AllowEdit = False
        Me.FTCLevelCode.Visible = True
        Me.FTCLevelCode.VisibleIndex = 0
        '
        'FTCLevelName
        '
        Me.FTCLevelName.Caption = "FTCLevelName"
        Me.FTCLevelName.FieldName = "FTCLevelName"
        Me.FTCLevelName.Name = "FTCLevelName"
        Me.FTCLevelName.OptionsColumn.AllowEdit = False
        Me.FTCLevelName.Visible = True
        Me.FTCLevelName.VisibleIndex = 1
        '
        'FTCountryCode
        '
        Me.FTCountryCode.Caption = "FTCountryCode"
        Me.FTCountryCode.FieldName = "FTCountryCode"
        Me.FTCountryCode.Name = "FTCountryCode"
        Me.FTCountryCode.OptionsColumn.AllowEdit = False
        Me.FTCountryCode.Visible = True
        Me.FTCountryCode.VisibleIndex = 2
        '
        'FTCountryName
        '
        Me.FTCountryName.Caption = "FTCountryName"
        Me.FTCountryName.FieldName = "FTCountryName"
        Me.FTCountryName.Name = "FTCountryName"
        Me.FTCountryName.OptionsColumn.AllowEdit = False
        Me.FTCountryName.Visible = True
        Me.FTCountryName.VisibleIndex = 3
        '
        'FTCmpCode
        '
        Me.FTCmpCode.Caption = "FTCmpCode"
        Me.FTCmpCode.FieldName = "FTCmpCode"
        Me.FTCmpCode.Name = "FTCmpCode"
        Me.FTCmpCode.OptionsColumn.AllowEdit = False
        Me.FTCmpCode.Visible = True
        Me.FTCmpCode.VisibleIndex = 4
        '
        'FTCmpName
        '
        Me.FTCmpName.Caption = "FTCmpName"
        Me.FTCmpName.FieldName = "FTCmpName"
        Me.FTCmpName.Name = "FTCmpName"
        Me.FTCmpName.OptionsColumn.AllowEdit = False
        Me.FTCmpName.Visible = True
        Me.FTCmpName.VisibleIndex = 5
        '
        'FTDivisonCode
        '
        Me.FTDivisonCode.Caption = "FTDivisonCode"
        Me.FTDivisonCode.FieldName = "FTDivisonCode"
        Me.FTDivisonCode.Name = "FTDivisonCode"
        Me.FTDivisonCode.OptionsColumn.AllowEdit = False
        Me.FTDivisonCode.Visible = True
        Me.FTDivisonCode.VisibleIndex = 6
        '
        'FTDivisonName
        '
        Me.FTDivisonName.Caption = "FTDivisonName"
        Me.FTDivisonName.FieldName = "FTDivisonName"
        Me.FTDivisonName.Name = "FTDivisonName"
        Me.FTDivisonName.OptionsColumn.AllowEdit = False
        Me.FTDivisonName.Visible = True
        Me.FTDivisonName.VisibleIndex = 7
        '
        'FTDeptCode
        '
        Me.FTDeptCode.Caption = "FTDeptCode"
        Me.FTDeptCode.FieldName = "FTDeptCode"
        Me.FTDeptCode.Name = "FTDeptCode"
        Me.FTDeptCode.OptionsColumn.AllowEdit = False
        Me.FTDeptCode.Visible = True
        Me.FTDeptCode.VisibleIndex = 8
        '
        'FTDeptDesc
        '
        Me.FTDeptDesc.Caption = "FTDeptDesc"
        Me.FTDeptDesc.FieldName = "FTDeptDesc"
        Me.FTDeptDesc.Name = "FTDeptDesc"
        Me.FTDeptDesc.OptionsColumn.AllowEdit = False
        Me.FTDeptDesc.Visible = True
        Me.FTDeptDesc.VisibleIndex = 9
        '
        'FTSectCode
        '
        Me.FTSectCode.Caption = "FTSectCode"
        Me.FTSectCode.FieldName = "FTSectCode"
        Me.FTSectCode.Name = "FTSectCode"
        Me.FTSectCode.OptionsColumn.AllowEdit = False
        Me.FTSectCode.Visible = True
        Me.FTSectCode.VisibleIndex = 10
        '
        'FTSectName
        '
        Me.FTSectName.Caption = "FTSectName"
        Me.FTSectName.FieldName = "FTSectName"
        Me.FTSectName.Name = "FTSectName"
        Me.FTSectName.OptionsColumn.AllowEdit = False
        Me.FTSectName.Visible = True
        Me.FTSectName.VisibleIndex = 11
        '
        'FTUnitSectCode
        '
        Me.FTUnitSectCode.Caption = "FTUnitSectCode"
        Me.FTUnitSectCode.FieldName = "FTUnitSectCode"
        Me.FTUnitSectCode.Name = "FTUnitSectCode"
        Me.FTUnitSectCode.OptionsColumn.AllowEdit = False
        Me.FTUnitSectCode.Visible = True
        Me.FTUnitSectCode.VisibleIndex = 12
        '
        'FTUnitSectName
        '
        Me.FTUnitSectName.Caption = "FTUnitSectName"
        Me.FTUnitSectName.FieldName = "FTUnitSectName"
        Me.FTUnitSectName.Name = "FTUnitSectName"
        Me.FTUnitSectName.OptionsColumn.AllowEdit = False
        Me.FTUnitSectName.Visible = True
        Me.FTUnitSectName.VisibleIndex = 13
        '
        'FTPositCode
        '
        Me.FTPositCode.Caption = "FTPositCode"
        Me.FTPositCode.FieldName = "FTPositCode"
        Me.FTPositCode.Name = "FTPositCode"
        Me.FTPositCode.OptionsColumn.AllowEdit = False
        Me.FTPositCode.Visible = True
        Me.FTPositCode.VisibleIndex = 14
        '
        'FTPositName
        '
        Me.FTPositName.Caption = "FTPositName"
        Me.FTPositName.FieldName = "FTPositName"
        Me.FTPositName.Name = "FTPositName"
        Me.FTPositName.OptionsColumn.AllowEdit = False
        Me.FTPositName.Visible = True
        Me.FTPositName.VisibleIndex = 15
        '
        'FTStateActive
        '
        Me.FTStateActive.Caption = "FTStateActive"
        Me.FTStateActive.ColumnEdit = Me.RepositoryItemCheckEdit1
        Me.FTStateActive.FieldName = "FTStateActive"
        Me.FTStateActive.Name = "FTStateActive"
        Me.FTStateActive.OptionsColumn.AllowEdit = False
        Me.FTStateActive.Visible = True
        Me.FTStateActive.VisibleIndex = 18
        '
        'RepositoryItemCheckEdit1
        '
        Me.RepositoryItemCheckEdit1.AutoHeight = False
        Me.RepositoryItemCheckEdit1.Caption = "Check"
        Me.RepositoryItemCheckEdit1.Name = "RepositoryItemCheckEdit1"
        Me.RepositoryItemCheckEdit1.ValueChecked = "1"
        Me.RepositoryItemCheckEdit1.ValueUnchecked = "0"
        '
        'FNPercentageQty
        '
        Me.FNPercentageQty.Caption = "FNPercentageQty"
        Me.FNPercentageQty.FieldName = "FNPercentageQty"
        Me.FNPercentageQty.Name = "FNPercentageQty"
        Me.FNPercentageQty.OptionsColumn.AllowEdit = False
        Me.FNPercentageQty.Visible = True
        Me.FNPercentageQty.VisibleIndex = 19
        '
        'FTRemark
        '
        Me.FTRemark.Caption = "FTRemark"
        Me.FTRemark.FieldName = "FTRemark"
        Me.FTRemark.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
        Me.FTRemark.Name = "FTRemark"
        Me.FTRemark.OptionsColumn.AllowEdit = False
        Me.FTRemark.Visible = True
        Me.FTRemark.VisibleIndex = 20
        '
        'FNHSysOrgMapId
        '
        Me.FNHSysOrgMapId.Caption = "FNHSysOrgMapId"
        Me.FNHSysOrgMapId.FieldName = "FNHSysOrgMapId"
        Me.FNHSysOrgMapId.Name = "FNHSysOrgMapId"
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
        Me.GroupControl2.Size = New System.Drawing.Size(1334, 682)
        Me.GroupControl2.TabIndex = 331
        Me.GroupControl2.Tag = "2|"
        Me.GroupControl2.Text = "รักษาการ"
        '
        'wOrganizationChartMapping
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1334, 682)
        Me.Controls.Add(Me.ogbmainprocbutton)
        Me.Controls.Add(Me.GroupControl2)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "wOrganizationChartMapping"
        Me.Text = "Organization Chart Mapping"
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbmainprocbutton.ResumeLayout(False)
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogcorganize, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvorganize, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogbmainprocbutton As DevExpress.XtraEditors.PanelControl
    Friend WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
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
    Friend WithEvents FTEmpCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTEmpName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTStateActive As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNPercentageQty As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTRemark As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ocmadd As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FTCLevelCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTCLevelName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemCheckEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents FTCountryCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ocmload As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FNHSysEmpID As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNHSysOrgMapId As DevExpress.XtraGrid.Columns.GridColumn
End Class

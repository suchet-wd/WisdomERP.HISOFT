<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wEmployeeLoan_Popup
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
        Me.ogc = New DevExpress.XtraGrid.GridControl()
        Me.ogv = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.FNHSysEmpID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTEmpCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTEmpName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNEmptypeGroupName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTEmpTypeCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FDDateEnd = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FCReqFinAmt112 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FCReqFinAmt113 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FCFinAmt112 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ReposFCFinAmt112 = New DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit()
        Me.FCFinAmt113 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ReposFCFinAmt113 = New DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit()
        Me.Condition_type = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNCalType = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCheckEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.RepositoryItemCheckEdit2 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.GridView2 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GroupControl2 = New DevExpress.XtraEditors.GroupControl()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ReposFCFinAmt112, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ReposFCFinAmt113, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl2.SuspendLayout()
        Me.SuspendLayout()
        '
        'GridView1
        '
        Me.GridView1.GridControl = Me.ogc
        Me.GridView1.Name = "GridView1"
        '
        'ogc
        '
        Me.ogc.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogc.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        GridLevelNode1.RelationName = "Level1"
        Me.ogc.LevelTree.Nodes.AddRange(New DevExpress.XtraGrid.GridLevelNode() {GridLevelNode1})
        Me.ogc.Location = New System.Drawing.Point(2, 25)
        Me.ogc.MainView = Me.ogv
        Me.ogc.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogc.Name = "ogc"
        Me.ogc.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCheckEdit1, Me.RepositoryItemCheckEdit2, Me.ReposFCFinAmt112, Me.ReposFCFinAmt113})
        Me.ogc.Size = New System.Drawing.Size(1264, 462)
        Me.ogc.TabIndex = 328
        Me.ogc.TabStop = False
        Me.ogc.Tag = "2|"
        Me.ogc.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogv, Me.GridView2, Me.GridView1})
        '
        'ogv
        '
        Me.ogv.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.FNHSysEmpID, Me.FTEmpCode, Me.FTEmpName, Me.FNEmptypeGroupName, Me.FTEmpTypeCode, Me.FDDateEnd, Me.FCReqFinAmt112, Me.FCReqFinAmt113, Me.FCFinAmt112, Me.FCFinAmt113, Me.Condition_type, Me.FNCalType})
        Me.ogv.GridControl = Me.ogc
        Me.ogv.Name = "ogv"
        Me.ogv.OptionsCustomization.AllowGroup = False
        Me.ogv.OptionsCustomization.AllowQuickHideColumns = False
        Me.ogv.OptionsView.ColumnAutoWidth = False
        Me.ogv.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.ogv.OptionsView.ShowAutoFilterRow = True
        Me.ogv.OptionsView.ShowGroupPanel = False
        Me.ogv.Tag = "2|"
        '
        'FNHSysEmpID
        '
        Me.FNHSysEmpID.Caption = "FNHSysEmpID"
        Me.FNHSysEmpID.FieldName = "FNHSysEmpID"
        Me.FNHSysEmpID.Name = "FNHSysEmpID"
        Me.FNHSysEmpID.Width = 101
        '
        'FTEmpCode
        '
        Me.FTEmpCode.Caption = "FTEmpCode"
        Me.FTEmpCode.FieldName = "FTEmpCode"
        Me.FTEmpCode.Name = "FTEmpCode"
        Me.FTEmpCode.OptionsColumn.AllowEdit = False
        Me.FTEmpCode.OptionsColumn.ReadOnly = True
        Me.FTEmpCode.Visible = True
        Me.FTEmpCode.VisibleIndex = 0
        Me.FTEmpCode.Width = 175
        '
        'FTEmpName
        '
        Me.FTEmpName.Caption = "FTEmpName"
        Me.FTEmpName.FieldName = "FTEmpName"
        Me.FTEmpName.Name = "FTEmpName"
        Me.FTEmpName.OptionsColumn.AllowEdit = False
        Me.FTEmpName.OptionsColumn.ReadOnly = True
        Me.FTEmpName.Visible = True
        Me.FTEmpName.VisibleIndex = 1
        Me.FTEmpName.Width = 245
        '
        'FNEmptypeGroupName
        '
        Me.FNEmptypeGroupName.Caption = "FNEmptypeGroupName"
        Me.FNEmptypeGroupName.FieldName = "FNEmptypeGroupName"
        Me.FNEmptypeGroupName.Name = "FNEmptypeGroupName"
        Me.FNEmptypeGroupName.OptionsColumn.AllowEdit = False
        Me.FNEmptypeGroupName.OptionsColumn.ReadOnly = True
        Me.FNEmptypeGroupName.Visible = True
        Me.FNEmptypeGroupName.VisibleIndex = 2
        Me.FNEmptypeGroupName.Width = 140
        '
        'FTEmpTypeCode
        '
        Me.FTEmpTypeCode.Caption = "FTEmpTypeCode"
        Me.FTEmpTypeCode.FieldName = "FTEmpTypeCode"
        Me.FTEmpTypeCode.Name = "FTEmpTypeCode"
        Me.FTEmpTypeCode.OptionsColumn.AllowEdit = False
        Me.FTEmpTypeCode.OptionsColumn.ReadOnly = True
        Me.FTEmpTypeCode.Visible = True
        Me.FTEmpTypeCode.VisibleIndex = 3
        Me.FTEmpTypeCode.Width = 138
        '
        'FDDateEnd
        '
        Me.FDDateEnd.Caption = "FDDateEnd"
        Me.FDDateEnd.FieldName = "FDDateEnd"
        Me.FDDateEnd.Name = "FDDateEnd"
        Me.FDDateEnd.OptionsColumn.AllowEdit = False
        Me.FDDateEnd.OptionsColumn.ReadOnly = True
        Me.FDDateEnd.Visible = True
        Me.FDDateEnd.VisibleIndex = 4
        '
        'FCReqFinAmt112
        '
        Me.FCReqFinAmt112.Caption = "FCReqFinAmt112"
        Me.FCReqFinAmt112.DisplayFormat.FormatString = "{0:n2}"
        Me.FCReqFinAmt112.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FCReqFinAmt112.FieldName = "FCReqFinAmt112"
        Me.FCReqFinAmt112.Name = "FCReqFinAmt112"
        Me.FCReqFinAmt112.OptionsColumn.AllowEdit = False
        Me.FCReqFinAmt112.OptionsColumn.ReadOnly = True
        Me.FCReqFinAmt112.Visible = True
        Me.FCReqFinAmt112.VisibleIndex = 5
        Me.FCReqFinAmt112.Width = 126
        '
        'FCReqFinAmt113
        '
        Me.FCReqFinAmt113.Caption = "FCReqFinAmt113"
        Me.FCReqFinAmt113.DisplayFormat.FormatString = "{0:n2}"
        Me.FCReqFinAmt113.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FCReqFinAmt113.FieldName = "FCReqFinAmt113"
        Me.FCReqFinAmt113.Name = "FCReqFinAmt113"
        Me.FCReqFinAmt113.OptionsColumn.AllowEdit = False
        Me.FCReqFinAmt113.OptionsColumn.ReadOnly = True
        Me.FCReqFinAmt113.Visible = True
        Me.FCReqFinAmt113.VisibleIndex = 6
        Me.FCReqFinAmt113.Width = 139
        '
        'FCFinAmt112
        '
        Me.FCFinAmt112.Caption = "FCFinAmt112"
        Me.FCFinAmt112.ColumnEdit = Me.ReposFCFinAmt112
        Me.FCFinAmt112.DisplayFormat.FormatString = "{0:n2}"
        Me.FCFinAmt112.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FCFinAmt112.FieldName = "FCFinAmt112"
        Me.FCFinAmt112.Name = "FCFinAmt112"
        Me.FCFinAmt112.Visible = True
        Me.FCFinAmt112.VisibleIndex = 7
        Me.FCFinAmt112.Width = 120
        '
        'ReposFCFinAmt112
        '
        Me.ReposFCFinAmt112.AutoHeight = False
        Me.ReposFCFinAmt112.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ReposFCFinAmt112.Name = "ReposFCFinAmt112"
        '
        'FCFinAmt113
        '
        Me.FCFinAmt113.Caption = "FCFinAmt113"
        Me.FCFinAmt113.ColumnEdit = Me.ReposFCFinAmt113
        Me.FCFinAmt113.DisplayFormat.FormatString = "{0:n2}"
        Me.FCFinAmt113.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FCFinAmt113.FieldName = "FCFinAmt113"
        Me.FCFinAmt113.Name = "FCFinAmt113"
        Me.FCFinAmt113.Visible = True
        Me.FCFinAmt113.VisibleIndex = 8
        Me.FCFinAmt113.Width = 149
        '
        'ReposFCFinAmt113
        '
        Me.ReposFCFinAmt113.AutoHeight = False
        Me.ReposFCFinAmt113.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ReposFCFinAmt113.Name = "ReposFCFinAmt113"
        '
        'Condition_type
        '
        Me.Condition_type.Caption = "Condition_type"
        Me.Condition_type.FieldName = "Condition_type"
        Me.Condition_type.Name = "Condition_type"
        Me.Condition_type.OptionsColumn.AllowEdit = False
        '
        'FNCalType
        '
        Me.FNCalType.Caption = "FNCalType"
        Me.FNCalType.FieldName = "FNCalType"
        Me.FNCalType.Name = "FNCalType"
        Me.FNCalType.OptionsColumn.AllowEdit = False
        '
        'RepositoryItemCheckEdit1
        '
        Me.RepositoryItemCheckEdit1.AutoHeight = False
        Me.RepositoryItemCheckEdit1.Name = "RepositoryItemCheckEdit1"
        Me.RepositoryItemCheckEdit1.ValueChecked = "1"
        Me.RepositoryItemCheckEdit1.ValueUnchecked = "0"
        '
        'RepositoryItemCheckEdit2
        '
        Me.RepositoryItemCheckEdit2.AutoHeight = False
        Me.RepositoryItemCheckEdit2.Name = "RepositoryItemCheckEdit2"
        Me.RepositoryItemCheckEdit2.ValueChecked = "1"
        Me.RepositoryItemCheckEdit2.ValueUnchecked = "0"
        '
        'GridView2
        '
        Me.GridView2.GridControl = Me.ogc
        Me.GridView2.Name = "GridView2"
        '
        'GroupControl2
        '
        Me.GroupControl2.Controls.Add(Me.ogc)
        Me.GroupControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl2.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl2.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupControl2.Name = "GroupControl2"
        Me.GroupControl2.Size = New System.Drawing.Size(1268, 489)
        Me.GroupControl2.TabIndex = 331
        Me.GroupControl2.Tag = "2|"
        Me.GroupControl2.Text = "ข้อมูลจากกยศกับหน้าทะเบียนประวัติ"
        '
        'wEmployeeLoan_Popup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1268, 489)
        Me.Controls.Add(Me.GroupControl2)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "wEmployeeLoan_Popup"
        Me.Text = "wEmployeeLoan_Popup"
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ReposFCFinAmt112, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ReposFCFinAmt113, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents ogc As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogv As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GroupControl2 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents GridView2 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents RepositoryItemCheckEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents RepositoryItemCheckEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents FTEmpCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNHSysEmpID As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTEmpName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FCFinAmt112 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FCFinAmt113 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FCReqFinAmt112 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FCReqFinAmt113 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ReposFCFinAmt112 As DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit
    Friend WithEvents ReposFCFinAmt113 As DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit
    Friend WithEvents FNEmptypeGroupName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTEmpTypeCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents Condition_type As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNCalType As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FDDateEnd As DevExpress.XtraGrid.Columns.GridColumn
End Class

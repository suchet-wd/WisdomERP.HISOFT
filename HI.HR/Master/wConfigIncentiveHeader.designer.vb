<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wConfigIncentiveHeader
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
        Me.ogc = New DevExpress.XtraGrid.GridControl()
        Me.ogv = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.FNHSysPositId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTPositCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTPositNameTH = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTPositNameEN = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FDDateFrom = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FDDateTo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNINcentiveMultiple = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTStateActive = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCheckEdit2 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.FNHSysCmpId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCheckEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.RepositoryItemCheckEdit3 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.RepositoryItemCheckEdit4 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.RepositoryItemCheckEdit5 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.GridView2 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GroupControl2 = New DevExpress.XtraEditors.GroupControl()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbmainprocbutton.SuspendLayout()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit5, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.GridView1.GridControl = Me.ogc
        Me.GridView1.Name = "GridView1"
        '
        'ogc
        '
        Me.ogc.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogc.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        GridLevelNode1.RelationName = "Level1"
        Me.ogc.LevelTree.Nodes.AddRange(New DevExpress.XtraGrid.GridLevelNode() {GridLevelNode1})
        Me.ogc.Location = New System.Drawing.Point(2, 27)
        Me.ogc.MainView = Me.ogv
        Me.ogc.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogc.Name = "ogc"
        Me.ogc.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCheckEdit1, Me.RepositoryItemCheckEdit2, Me.RepositoryItemCheckEdit3, Me.RepositoryItemCheckEdit4, Me.RepositoryItemCheckEdit5})
        Me.ogc.Size = New System.Drawing.Size(1330, 653)
        Me.ogc.TabIndex = 328
        Me.ogc.TabStop = False
        Me.ogc.Tag = " "
        Me.ogc.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogv, Me.GridView2, Me.GridView1})
        '
        'ogv
        '
        Me.ogv.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.FNHSysPositId, Me.FTPositCode, Me.FTPositNameTH, Me.FTPositNameEN, Me.FDDateFrom, Me.FDDateTo, Me.FNINcentiveMultiple, Me.FTStateActive, Me.FNHSysCmpId})
        Me.ogv.GridControl = Me.ogc
        Me.ogv.Name = "ogv"
        Me.ogv.OptionsCustomization.AllowGroup = False
        Me.ogv.OptionsCustomization.AllowQuickHideColumns = False
        Me.ogv.OptionsView.AllowCellMerge = True
        Me.ogv.OptionsView.ColumnAutoWidth = False
        Me.ogv.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.ogv.OptionsView.ShowGroupPanel = False
        Me.ogv.Tag = "2|"
        '
        'FNHSysPositId
        '
        Me.FNHSysPositId.Caption = "FNHSysPositId"
        Me.FNHSysPositId.FieldName = "FNHSysPositId"
        Me.FNHSysPositId.Name = "FNHSysPositId"
        '
        'FTPositCode
        '
        Me.FTPositCode.Caption = "FTPositCode"
        Me.FTPositCode.FieldName = "FTPositCode"
        Me.FTPositCode.MinWidth = 25
        Me.FTPositCode.Name = "FTPositCode"
        Me.FTPositCode.OptionsColumn.AllowEdit = False
        Me.FTPositCode.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTPositCode.OptionsColumn.ReadOnly = True
        Me.FTPositCode.Visible = True
        Me.FTPositCode.VisibleIndex = 0
        Me.FTPositCode.Width = 94
        '
        'FTPositNameTH
        '
        Me.FTPositNameTH.Caption = "FTPositNameTH"
        Me.FTPositNameTH.FieldName = "FTPositNameTH"
        Me.FTPositNameTH.MinWidth = 25
        Me.FTPositNameTH.Name = "FTPositNameTH"
        Me.FTPositNameTH.OptionsColumn.AllowEdit = False
        Me.FTPositNameTH.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTPositNameTH.OptionsColumn.ReadOnly = True
        Me.FTPositNameTH.Visible = True
        Me.FTPositNameTH.VisibleIndex = 1
        Me.FTPositNameTH.Width = 94
        '
        'FTPositNameEN
        '
        Me.FTPositNameEN.Caption = "FTPositNameEN"
        Me.FTPositNameEN.FieldName = "FTPositNameEN"
        Me.FTPositNameEN.Name = "FTPositNameEN"
        Me.FTPositNameEN.OptionsColumn.AllowEdit = False
        Me.FTPositNameEN.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTPositNameEN.OptionsColumn.ReadOnly = True
        Me.FTPositNameEN.Visible = True
        Me.FTPositNameEN.VisibleIndex = 2
        '
        'FDDateFrom
        '
        Me.FDDateFrom.Caption = "FDDateFrom"
        Me.FDDateFrom.FieldName = "FDDateFrom"
        Me.FDDateFrom.Name = "FDDateFrom"
        Me.FDDateFrom.OptionsColumn.AllowEdit = False
        Me.FDDateFrom.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FDDateFrom.OptionsColumn.ReadOnly = True
        Me.FDDateFrom.Visible = True
        Me.FDDateFrom.VisibleIndex = 3
        '
        'FDDateTo
        '
        Me.FDDateTo.Caption = "FDDateTo"
        Me.FDDateTo.FieldName = "FDDateTo"
        Me.FDDateTo.Name = "FDDateTo"
        Me.FDDateTo.OptionsColumn.AllowEdit = False
        Me.FDDateTo.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FDDateTo.OptionsColumn.ReadOnly = True
        Me.FDDateTo.Visible = True
        Me.FDDateTo.VisibleIndex = 4
        '
        'FNINcentiveMultiple
        '
        Me.FNINcentiveMultiple.Caption = "FNINcentiveMultiple"
        Me.FNINcentiveMultiple.DisplayFormat.FormatString = "{0:n2}"
        Me.FNINcentiveMultiple.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNINcentiveMultiple.FieldName = "FNINcentiveMultiple"
        Me.FNINcentiveMultiple.MinWidth = 25
        Me.FNINcentiveMultiple.Name = "FNINcentiveMultiple"
        Me.FNINcentiveMultiple.OptionsColumn.AllowEdit = False
        Me.FNINcentiveMultiple.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNINcentiveMultiple.OptionsColumn.ReadOnly = True
        Me.FNINcentiveMultiple.Visible = True
        Me.FNINcentiveMultiple.VisibleIndex = 5
        Me.FNINcentiveMultiple.Width = 94
        '
        'FTStateActive
        '
        Me.FTStateActive.Caption = "FTStateActive"
        Me.FTStateActive.ColumnEdit = Me.RepositoryItemCheckEdit2
        Me.FTStateActive.FieldName = "FTStateActive"
        Me.FTStateActive.Name = "FTStateActive"
        Me.FTStateActive.OptionsColumn.AllowEdit = False
        Me.FTStateActive.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTStateActive.OptionsColumn.ReadOnly = True
        Me.FTStateActive.Visible = True
        Me.FTStateActive.VisibleIndex = 6
        '
        'RepositoryItemCheckEdit2
        '
        Me.RepositoryItemCheckEdit2.AutoHeight = False
        Me.RepositoryItemCheckEdit2.Name = "RepositoryItemCheckEdit2"
        Me.RepositoryItemCheckEdit2.ValueChecked = "1"
        Me.RepositoryItemCheckEdit2.ValueUnchecked = "0"
        '
        'FNHSysCmpId
        '
        Me.FNHSysCmpId.Caption = "FNHSysCmpId"
        Me.FNHSysCmpId.FieldName = "FNHSysCmpId"
        Me.FNHSysCmpId.MinWidth = 25
        Me.FNHSysCmpId.Name = "FNHSysCmpId"
        Me.FNHSysCmpId.Width = 94
        '
        'RepositoryItemCheckEdit1
        '
        Me.RepositoryItemCheckEdit1.AutoHeight = False
        Me.RepositoryItemCheckEdit1.Caption = "Check"
        Me.RepositoryItemCheckEdit1.Name = "RepositoryItemCheckEdit1"
        Me.RepositoryItemCheckEdit1.ValueChecked = "1"
        Me.RepositoryItemCheckEdit1.ValueUnchecked = "0"
        '
        'RepositoryItemCheckEdit3
        '
        Me.RepositoryItemCheckEdit3.AutoHeight = False
        Me.RepositoryItemCheckEdit3.Name = "RepositoryItemCheckEdit3"
        Me.RepositoryItemCheckEdit3.ValueChecked = "1"
        Me.RepositoryItemCheckEdit3.ValueUnchecked = "0"
        '
        'RepositoryItemCheckEdit4
        '
        Me.RepositoryItemCheckEdit4.AutoHeight = False
        Me.RepositoryItemCheckEdit4.Name = "RepositoryItemCheckEdit4"
        Me.RepositoryItemCheckEdit4.ValueChecked = "1"
        Me.RepositoryItemCheckEdit4.ValueUnchecked = "0"
        '
        'RepositoryItemCheckEdit5
        '
        Me.RepositoryItemCheckEdit5.AutoHeight = False
        Me.RepositoryItemCheckEdit5.Name = "RepositoryItemCheckEdit5"
        Me.RepositoryItemCheckEdit5.ValueChecked = "1"
        Me.RepositoryItemCheckEdit5.ValueUnchecked = "0"
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
        Me.GroupControl2.Size = New System.Drawing.Size(1334, 682)
        Me.GroupControl2.TabIndex = 331
        Me.GroupControl2.Tag = "2|"
        Me.GroupControl2.Text = "ข้อมูลอัตราแรงจูงใจระดับหัวหน้า"
        '
        'wConfigIncentiveHeader
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1334, 682)
        Me.Controls.Add(Me.ogbmainprocbutton)
        Me.Controls.Add(Me.GroupControl2)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "wConfigIncentiveHeader"
        Me.Text = "ข้อมูลอัตราแรงจูงใจระดับหัวหน้า"
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbmainprocbutton.ResumeLayout(False)
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogbmainprocbutton As DevExpress.XtraEditors.PanelControl
    Friend WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents ogc As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogv As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GroupControl2 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents GridView2 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents FNHSysPositId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ocmadd As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FDDateFrom As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FDDateTo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemCheckEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents FTPositNameEN As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ocmload As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FTStateActive As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemCheckEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents RepositoryItemCheckEdit3 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents RepositoryItemCheckEdit4 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents RepositoryItemCheckEdit5 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents FTPositCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNHSysCmpId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNINcentiveMultiple As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTPositNameTH As DevExpress.XtraGrid.Columns.GridColumn
End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wReopenPayrollPeriod
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
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.ocmclose = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmcalculate = New DevExpress.XtraEditors.SimpleButton()
        Me.ogbheader = New DevExpress.XtraEditors.GroupControl()
        Me.ogd = New DevExpress.XtraGrid.GridControl()
        Me.ogv = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.FTSelect = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepFTSelect = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.FTEmpTypeCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTPayYear = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTPayTerm = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FDPayDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNHSysEmpTypeId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCheckEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.RepositoryItemCheckEdit2 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.RepositoryItemCheckEdit3 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.RepositoryItemCheckEdit4 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.RepositoryItemCheckEdit5 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.RepositoryItemCheckEdit6 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.RepositoryItemCheckEdit7 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.RepFTStaActive = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.ogbheader, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbheader.SuspendLayout()
        CType(Me.ogd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepFTSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepFTStaActive, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupControl1
        '
        Me.GroupControl1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupControl1.Controls.Add(Me.ocmclose)
        Me.GroupControl1.Controls.Add(Me.ocmcalculate)
        Me.GroupControl1.Location = New System.Drawing.Point(3, 282)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.ShowCaption = False
        Me.GroupControl1.Size = New System.Drawing.Size(718, 42)
        Me.GroupControl1.TabIndex = 1
        Me.GroupControl1.Text = "GroupControl1"
        '
        'ocmclose
        '
        Me.ocmclose.Location = New System.Drawing.Point(472, 9)
        Me.ocmclose.Name = "ocmclose"
        Me.ocmclose.Size = New System.Drawing.Size(120, 25)
        Me.ocmclose.TabIndex = 103
        Me.ocmclose.TabStop = False
        Me.ocmclose.Tag = "2|"
        Me.ocmclose.Text = "CANCEL"
        '
        'ocmcalculate
        '
        Me.ocmcalculate.Location = New System.Drawing.Point(122, 9)
        Me.ocmcalculate.Name = "ocmcalculate"
        Me.ocmcalculate.Size = New System.Drawing.Size(120, 25)
        Me.ocmcalculate.TabIndex = 102
        Me.ocmcalculate.TabStop = False
        Me.ocmcalculate.Tag = "2|"
        Me.ocmcalculate.Text = "CLOSE PERIOD"
        '
        'ogbheader
        '
        Me.ogbheader.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbheader.Controls.Add(Me.ogd)
        Me.ogbheader.Location = New System.Drawing.Point(3, 3)
        Me.ogbheader.Name = "ogbheader"
        Me.ogbheader.Size = New System.Drawing.Size(718, 274)
        Me.ogbheader.TabIndex = 2
        '
        'ogd
        '
        Me.ogd.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogd.Location = New System.Drawing.Point(2, 21)
        Me.ogd.MainView = Me.ogv
        Me.ogd.Name = "ogd"
        Me.ogd.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCheckEdit1, Me.RepositoryItemCheckEdit2, Me.RepositoryItemCheckEdit3, Me.RepositoryItemCheckEdit4, Me.RepositoryItemCheckEdit5, Me.RepositoryItemCheckEdit6, Me.RepositoryItemCheckEdit7, Me.RepFTStaActive, Me.RepFTSelect})
        Me.ogd.Size = New System.Drawing.Size(714, 251)
        Me.ogd.TabIndex = 4
        Me.ogd.TabStop = False
        Me.ogd.Tag = "2|"
        Me.ogd.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogv})
        '
        'ogv
        '
        Me.ogv.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.FTSelect, Me.FTEmpTypeCode, Me.FTPayYear, Me.FTPayTerm, Me.FDPayDate, Me.FNHSysEmpTypeId})
        Me.ogv.GridControl = Me.ogd
        Me.ogv.Name = "ogv"
        Me.ogv.OptionsCustomization.AllowGroup = False
        Me.ogv.OptionsCustomization.AllowQuickHideColumns = False
        Me.ogv.OptionsView.ColumnAutoWidth = False
        Me.ogv.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.ogv.OptionsView.ShowGroupPanel = False
        Me.ogv.Tag = "2|"
        '
        'FTSelect
        '
        Me.FTSelect.AppearanceCell.BackColor = System.Drawing.Color.LightCyan
        Me.FTSelect.AppearanceCell.Options.UseBackColor = True
        Me.FTSelect.AppearanceCell.Options.UseTextOptions = True
        Me.FTSelect.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.FTSelect.AppearanceHeader.Options.UseTextOptions = True
        Me.FTSelect.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTSelect.Caption = "FTSelect"
        Me.FTSelect.ColumnEdit = Me.RepFTSelect
        Me.FTSelect.FieldName = "FTSelect"
        Me.FTSelect.Name = "FTSelect"
        Me.FTSelect.OptionsColumn.AllowMove = False
        Me.FTSelect.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTSelect.Visible = True
        Me.FTSelect.VisibleIndex = 0
        '
        'RepFTSelect
        '
        Me.RepFTSelect.AutoHeight = False
        Me.RepFTSelect.Caption = "Check"
        Me.RepFTSelect.Name = "RepFTSelect"
        Me.RepFTSelect.ValueChecked = "1"
        Me.RepFTSelect.ValueUnchecked = "0"
        '
        'FTEmpTypeCode
        '
        Me.FTEmpTypeCode.AppearanceCell.Options.UseTextOptions = True
        Me.FTEmpTypeCode.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.FTEmpTypeCode.AppearanceHeader.Options.UseTextOptions = True
        Me.FTEmpTypeCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTEmpTypeCode.Caption = "FTEmpTypeCode"
        Me.FTEmpTypeCode.FieldName = "FTEmpTypeCode"
        Me.FTEmpTypeCode.Name = "FTEmpTypeCode"
        Me.FTEmpTypeCode.OptionsColumn.AllowEdit = False
        Me.FTEmpTypeCode.OptionsColumn.AllowMove = False
        Me.FTEmpTypeCode.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTEmpTypeCode.OptionsColumn.ReadOnly = True
        Me.FTEmpTypeCode.Visible = True
        Me.FTEmpTypeCode.VisibleIndex = 1
        Me.FTEmpTypeCode.Width = 109
        '
        'FTPayYear
        '
        Me.FTPayYear.AppearanceCell.Options.UseTextOptions = True
        Me.FTPayYear.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.FTPayYear.AppearanceHeader.Options.UseTextOptions = True
        Me.FTPayYear.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTPayYear.Caption = "FTPayYear"
        Me.FTPayYear.FieldName = "FTPayYear"
        Me.FTPayYear.Name = "FTPayYear"
        Me.FTPayYear.OptionsColumn.AllowEdit = False
        Me.FTPayYear.OptionsColumn.AllowMove = False
        Me.FTPayYear.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTPayYear.OptionsColumn.ReadOnly = True
        Me.FTPayYear.Visible = True
        Me.FTPayYear.VisibleIndex = 2
        Me.FTPayYear.Width = 104
        '
        'FTPayTerm
        '
        Me.FTPayTerm.AppearanceCell.Options.UseTextOptions = True
        Me.FTPayTerm.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.FTPayTerm.AppearanceHeader.Options.UseTextOptions = True
        Me.FTPayTerm.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTPayTerm.Caption = "FTPayTerm"
        Me.FTPayTerm.FieldName = "FTPayTerm"
        Me.FTPayTerm.Name = "FTPayTerm"
        Me.FTPayTerm.OptionsColumn.AllowEdit = False
        Me.FTPayTerm.OptionsColumn.AllowMove = False
        Me.FTPayTerm.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTPayTerm.OptionsColumn.ReadOnly = True
        Me.FTPayTerm.Visible = True
        Me.FTPayTerm.VisibleIndex = 3
        Me.FTPayTerm.Width = 100
        '
        'FDPayDate
        '
        Me.FDPayDate.AppearanceCell.Options.UseTextOptions = True
        Me.FDPayDate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FDPayDate.AppearanceHeader.Options.UseTextOptions = True
        Me.FDPayDate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FDPayDate.Caption = "FDPayDate"
        Me.FDPayDate.FieldName = "FDPayDate"
        Me.FDPayDate.Name = "FDPayDate"
        Me.FDPayDate.OptionsColumn.AllowEdit = False
        Me.FDPayDate.OptionsColumn.AllowMove = False
        Me.FDPayDate.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FDPayDate.OptionsColumn.ReadOnly = True
        Me.FDPayDate.Visible = True
        Me.FDPayDate.VisibleIndex = 4
        Me.FDPayDate.Width = 105
        '
        'FNHSysEmpTypeId
        '
        Me.FNHSysEmpTypeId.Caption = "FNHSysEmpTypeId"
        Me.FNHSysEmpTypeId.FieldName = "FNHSysEmpTypeId"
        Me.FNHSysEmpTypeId.Name = "FNHSysEmpTypeId"
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
        'RepositoryItemCheckEdit3
        '
        Me.RepositoryItemCheckEdit3.AutoHeight = False
        Me.RepositoryItemCheckEdit3.Caption = "Check"
        Me.RepositoryItemCheckEdit3.Name = "RepositoryItemCheckEdit3"
        Me.RepositoryItemCheckEdit3.ValueChecked = "1"
        Me.RepositoryItemCheckEdit3.ValueUnchecked = "0"
        '
        'RepositoryItemCheckEdit4
        '
        Me.RepositoryItemCheckEdit4.AutoHeight = False
        Me.RepositoryItemCheckEdit4.Caption = "Check"
        Me.RepositoryItemCheckEdit4.Name = "RepositoryItemCheckEdit4"
        Me.RepositoryItemCheckEdit4.ValueChecked = "1"
        Me.RepositoryItemCheckEdit4.ValueUnchecked = "0"
        '
        'RepositoryItemCheckEdit5
        '
        Me.RepositoryItemCheckEdit5.AutoHeight = False
        Me.RepositoryItemCheckEdit5.Caption = "Check"
        Me.RepositoryItemCheckEdit5.Name = "RepositoryItemCheckEdit5"
        Me.RepositoryItemCheckEdit5.ValueChecked = "1"
        Me.RepositoryItemCheckEdit5.ValueUnchecked = "0"
        '
        'RepositoryItemCheckEdit6
        '
        Me.RepositoryItemCheckEdit6.AutoHeight = False
        Me.RepositoryItemCheckEdit6.Caption = "Check"
        Me.RepositoryItemCheckEdit6.Name = "RepositoryItemCheckEdit6"
        Me.RepositoryItemCheckEdit6.ValueChecked = "1"
        Me.RepositoryItemCheckEdit6.ValueUnchecked = "0"
        '
        'RepositoryItemCheckEdit7
        '
        Me.RepositoryItemCheckEdit7.AutoHeight = False
        Me.RepositoryItemCheckEdit7.Caption = "Check"
        Me.RepositoryItemCheckEdit7.Name = "RepositoryItemCheckEdit7"
        Me.RepositoryItemCheckEdit7.ValueChecked = "1"
        Me.RepositoryItemCheckEdit7.ValueUnchecked = "0"
        '
        'RepFTStaActive
        '
        Me.RepFTStaActive.AutoHeight = False
        Me.RepFTStaActive.Caption = "Check"
        Me.RepFTStaActive.Name = "RepFTStaActive"
        Me.RepFTStaActive.ValueChecked = "1"
        Me.RepFTStaActive.ValueUnchecked = "0"
        '
        'wReopenPayrollPeriod
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(722, 325)
        Me.Controls.Add(Me.ogbheader)
        Me.Controls.Add(Me.GroupControl1)
        Me.Name = "wReopenPayrollPeriod"
        Me.Text = "Re - Open Period"
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        CType(Me.ogbheader, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbheader.ResumeLayout(False)
        CType(Me.ogd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepFTSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepFTStaActive, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ocmclose As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmcalculate As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ogbheader As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ogd As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogv As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents RepositoryItemCheckEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents RepositoryItemCheckEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents RepositoryItemCheckEdit3 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents RepositoryItemCheckEdit4 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents RepositoryItemCheckEdit5 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents RepositoryItemCheckEdit6 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents RepositoryItemCheckEdit7 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents RepFTStaActive As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents FTSelect As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepFTSelect As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents FTEmpTypeCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTPayYear As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTPayTerm As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FDPayDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNHSysEmpTypeId As DevExpress.XtraGrid.Columns.GridColumn
End Class

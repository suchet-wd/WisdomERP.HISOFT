<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wConfigPayrollMonthly_LA
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
        Me.FNHSysEmpID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTEmpCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTEmpNameTH = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTEmpNameEN = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNSalary = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FCOt1_Baht = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNTotalIncome = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNTotalRecalSSO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNSocial = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNSocialCmp = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNTotalRecalTAX = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNTax = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNTax5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNTax10 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNTax15 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNTax20 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNTax25 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNNetpay = New DevExpress.XtraGrid.Columns.GridColumn()
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
        Me.ogv.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.FNHSysEmpID, Me.FTEmpCode, Me.FTEmpNameTH, Me.FTEmpNameEN, Me.FNSalary, Me.FCOt1_Baht, Me.FNTotalIncome, Me.FNTotalRecalSSO, Me.FNSocial, Me.FNSocialCmp, Me.FNTotalRecalTAX, Me.FNTax, Me.FNTax5, Me.FNTax10, Me.FNTax15, Me.FNTax20, Me.FNTax25, Me.FNNetpay, Me.FTStateActive, Me.FNHSysCmpId})
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
        Me.FTEmpCode.MinWidth = 25
        Me.FTEmpCode.Name = "FTEmpCode"
        Me.FTEmpCode.OptionsColumn.AllowEdit = False
        Me.FTEmpCode.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTEmpCode.OptionsColumn.ReadOnly = True
        Me.FTEmpCode.Visible = True
        Me.FTEmpCode.VisibleIndex = 0
        Me.FTEmpCode.Width = 94
        '
        'FTEmpNameTH
        '
        Me.FTEmpNameTH.Caption = "FTEmpNameTH"
        Me.FTEmpNameTH.FieldName = "FTEmpNameTH"
        Me.FTEmpNameTH.MinWidth = 25
        Me.FTEmpNameTH.Name = "FTEmpNameTH"
        Me.FTEmpNameTH.OptionsColumn.AllowEdit = False
        Me.FTEmpNameTH.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTEmpNameTH.OptionsColumn.ReadOnly = True
        Me.FTEmpNameTH.Visible = True
        Me.FTEmpNameTH.VisibleIndex = 1
        Me.FTEmpNameTH.Width = 94
        '
        'FTEmpNameEN
        '
        Me.FTEmpNameEN.Caption = "FTEmpNameEN"
        Me.FTEmpNameEN.FieldName = "FTEmpNameEN"
        Me.FTEmpNameEN.Name = "FTEmpNameEN"
        Me.FTEmpNameEN.OptionsColumn.AllowEdit = False
        Me.FTEmpNameEN.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTEmpNameEN.OptionsColumn.ReadOnly = True
        Me.FTEmpNameEN.Visible = True
        Me.FTEmpNameEN.VisibleIndex = 2
        '
        'FNSalary
        '
        Me.FNSalary.Caption = "FNSalary"
        Me.FNSalary.FieldName = "FNSalary"
        Me.FNSalary.Name = "FNSalary"
        Me.FNSalary.OptionsColumn.AllowEdit = False
        Me.FNSalary.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNSalary.OptionsColumn.ReadOnly = True
        Me.FNSalary.Visible = True
        Me.FNSalary.VisibleIndex = 3
        '
        'FCOt1_Baht
        '
        Me.FCOt1_Baht.Caption = "FCOt1_Baht"
        Me.FCOt1_Baht.FieldName = "FCOt1_Baht"
        Me.FCOt1_Baht.MinWidth = 25
        Me.FCOt1_Baht.Name = "FCOt1_Baht"
        Me.FCOt1_Baht.Visible = True
        Me.FCOt1_Baht.VisibleIndex = 5
        Me.FCOt1_Baht.Width = 94
        '
        'FNTotalIncome
        '
        Me.FNTotalIncome.Caption = "FNTotalIncome"
        Me.FNTotalIncome.FieldName = "FNTotalIncome"
        Me.FNTotalIncome.MinWidth = 25
        Me.FNTotalIncome.Name = "FNTotalIncome"
        Me.FNTotalIncome.Visible = True
        Me.FNTotalIncome.VisibleIndex = 6
        Me.FNTotalIncome.Width = 94
        '
        'FNTotalRecalSSO
        '
        Me.FNTotalRecalSSO.Caption = "FNTotalRecalSSO"
        Me.FNTotalRecalSSO.FieldName = "FNTotalRecalSSO"
        Me.FNTotalRecalSSO.MinWidth = 25
        Me.FNTotalRecalSSO.Name = "FNTotalRecalSSO"
        Me.FNTotalRecalSSO.Visible = True
        Me.FNTotalRecalSSO.VisibleIndex = 7
        Me.FNTotalRecalSSO.Width = 94
        '
        'FNSocial
        '
        Me.FNSocial.Caption = "FNSocial"
        Me.FNSocial.FieldName = "FNSocial"
        Me.FNSocial.MinWidth = 25
        Me.FNSocial.Name = "FNSocial"
        Me.FNSocial.Visible = True
        Me.FNSocial.VisibleIndex = 8
        Me.FNSocial.Width = 94
        '
        'FNSocialCmp
        '
        Me.FNSocialCmp.Caption = "FNSocialCmp"
        Me.FNSocialCmp.FieldName = "FNSocialCmp"
        Me.FNSocialCmp.MinWidth = 25
        Me.FNSocialCmp.Name = "FNSocialCmp"
        Me.FNSocialCmp.Visible = True
        Me.FNSocialCmp.VisibleIndex = 9
        Me.FNSocialCmp.Width = 94
        '
        'FNTotalRecalTAX
        '
        Me.FNTotalRecalTAX.Caption = "FNTotalRecalTAX"
        Me.FNTotalRecalTAX.DisplayFormat.FormatString = "{0:n2}"
        Me.FNTotalRecalTAX.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNTotalRecalTAX.FieldName = "FNTotalRecalTAX"
        Me.FNTotalRecalTAX.MinWidth = 25
        Me.FNTotalRecalTAX.Name = "FNTotalRecalTAX"
        Me.FNTotalRecalTAX.OptionsColumn.AllowEdit = False
        Me.FNTotalRecalTAX.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNTotalRecalTAX.OptionsColumn.ReadOnly = True
        Me.FNTotalRecalTAX.Visible = True
        Me.FNTotalRecalTAX.VisibleIndex = 4
        Me.FNTotalRecalTAX.Width = 94
        '
        'FNTax
        '
        Me.FNTax.Caption = "FNTax"
        Me.FNTax.FieldName = "FNTax"
        Me.FNTax.MinWidth = 25
        Me.FNTax.Name = "FNTax"
        Me.FNTax.Visible = True
        Me.FNTax.VisibleIndex = 11
        Me.FNTax.Width = 94
        '
        'FNTax5
        '
        Me.FNTax5.Caption = "FNTax5"
        Me.FNTax5.FieldName = "FNTax5"
        Me.FNTax5.MinWidth = 25
        Me.FNTax5.Name = "FNTax5"
        Me.FNTax5.Width = 94
        '
        'FNTax10
        '
        Me.FNTax10.Caption = "FNTax10"
        Me.FNTax10.FieldName = "FNTax10"
        Me.FNTax10.MinWidth = 25
        Me.FNTax10.Name = "FNTax10"
        Me.FNTax10.Width = 94
        '
        'FNTax15
        '
        Me.FNTax15.Caption = "FNTax15"
        Me.FNTax15.FieldName = "FNTax15"
        Me.FNTax15.MinWidth = 25
        Me.FNTax15.Name = "FNTax15"
        Me.FNTax15.Width = 94
        '
        'FNTax20
        '
        Me.FNTax20.Caption = "FNTax20"
        Me.FNTax20.FieldName = "FNTax20"
        Me.FNTax20.MinWidth = 25
        Me.FNTax20.Name = "FNTax20"
        Me.FNTax20.Width = 94
        '
        'FNTax25
        '
        Me.FNTax25.Caption = "FNTax25"
        Me.FNTax25.FieldName = "FNTax25"
        Me.FNTax25.MinWidth = 25
        Me.FNTax25.Name = "FNTax25"
        Me.FNTax25.Width = 94
        '
        'FNNetpay
        '
        Me.FNNetpay.Caption = "FNNetpay"
        Me.FNNetpay.FieldName = "FNNetpay"
        Me.FNNetpay.MinWidth = 25
        Me.FNNetpay.Name = "FNNetpay"
        Me.FNNetpay.Visible = True
        Me.FNNetpay.VisibleIndex = 12
        Me.FNNetpay.Width = 94
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
        Me.FTStateActive.VisibleIndex = 10
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
        'wConfigPayrollMonthly_LA
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1334, 682)
        Me.Controls.Add(Me.ogbmainprocbutton)
        Me.Controls.Add(Me.GroupControl2)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "wConfigPayrollMonthly_LA"
        Me.Text = "ข้อมูลรายได้พนักงานรายเดือนไทย  สาขาลาว"
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
    Friend WithEvents FNHSysEmpID As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ocmadd As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FNSalary As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemCheckEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents FTEmpNameEN As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ocmload As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FTStateActive As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemCheckEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents RepositoryItemCheckEdit3 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents RepositoryItemCheckEdit4 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents RepositoryItemCheckEdit5 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents FTEmpCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNHSysCmpId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNTotalRecalTAX As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTEmpNameTH As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FCOt1_Baht As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNTotalIncome As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNTotalRecalSSO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNSocial As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNSocialCmp As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNTax As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNTax5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNTax10 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNTax15 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNTax20 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNTax25 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNNetpay As DevExpress.XtraGrid.Columns.GridColumn
End Class

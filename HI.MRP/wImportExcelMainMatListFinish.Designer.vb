<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wImportExcelMainMatListFinish
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
        Me.ogclist = New DevExpress.XtraGrid.GridControl()
        Me.ogvlist = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.FTStateImport = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepFTSelect = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.xMATERIALCODE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.xMATERIALNAME_EN = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cxMATERIALNAME_TH = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.xzMAINMATTYPE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cxMATGROUP = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cxMATTYPE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cxFabricWidth = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cxCustomer = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cxCustomerRefCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cxSupplier = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cxUnit = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cxCurrency = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ogbcmd = New DevExpress.XtraEditors.GroupControl()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.otb = New DevExpress.XtraTab.XtraTabControl()
        Me.otpmappingsupplier = New DevExpress.XtraTab.XtraTabPage()
        CType(Me.ogclist, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvlist, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepFTSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogbcmd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbcmd.SuspendLayout()
        CType(Me.otb, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.otb.SuspendLayout()
        Me.otpmappingsupplier.SuspendLayout()
        Me.SuspendLayout()
        '
        'ogclist
        '
        Me.ogclist.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogclist.Location = New System.Drawing.Point(0, 0)
        Me.ogclist.MainView = Me.ogvlist
        Me.ogclist.Name = "ogclist"
        Me.ogclist.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepFTSelect})
        Me.ogclist.Size = New System.Drawing.Size(1450, 614)
        Me.ogclist.TabIndex = 302
        Me.ogclist.TabStop = False
        Me.ogclist.Tag = "2|"
        Me.ogclist.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvlist})
        '
        'ogvlist
        '
        Me.ogvlist.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.FTStateImport, Me.xMATERIALCODE, Me.xMATERIALNAME_EN, Me.cxMATERIALNAME_TH, Me.xzMAINMATTYPE, Me.cxMATGROUP, Me.cxMATTYPE, Me.cxFabricWidth, Me.cxCustomer, Me.cxCustomerRefCode, Me.cxSupplier, Me.cxUnit, Me.cxCurrency})
        Me.ogvlist.GridControl = Me.ogclist
        Me.ogvlist.Name = "ogvlist"
        Me.ogvlist.OptionsCustomization.AllowGroup = False
        Me.ogvlist.OptionsCustomization.AllowQuickHideColumns = False
        Me.ogvlist.OptionsView.ColumnAutoWidth = False
        Me.ogvlist.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.ogvlist.OptionsView.ShowGroupPanel = False
        Me.ogvlist.Tag = "2|"
        '
        'FTStateImport
        '
        Me.FTStateImport.Caption = "Import"
        Me.FTStateImport.ColumnEdit = Me.RepFTSelect
        Me.FTStateImport.FieldName = "FTStateImport"
        Me.FTStateImport.Name = "FTStateImport"
        Me.FTStateImport.OptionsColumn.AllowEdit = False
        Me.FTStateImport.OptionsColumn.ReadOnly = True
        Me.FTStateImport.Visible = True
        Me.FTStateImport.VisibleIndex = 0
        Me.FTStateImport.Width = 70
        '
        'RepFTSelect
        '
        Me.RepFTSelect.AutoHeight = False
        Me.RepFTSelect.Caption = "Check"
        Me.RepFTSelect.Name = "RepFTSelect"
        Me.RepFTSelect.ValueChecked = "1"
        Me.RepFTSelect.ValueUnchecked = "0"
        '
        'xMATERIALCODE
        '
        Me.xMATERIALCODE.Caption = "MATERIAL CODE"
        Me.xMATERIALCODE.FieldName = "MATERIALCODE"
        Me.xMATERIALCODE.Name = "xMATERIALCODE"
        Me.xMATERIALCODE.OptionsColumn.AllowEdit = False
        Me.xMATERIALCODE.OptionsColumn.ReadOnly = True
        Me.xMATERIALCODE.Visible = True
        Me.xMATERIALCODE.VisibleIndex = 1
        Me.xMATERIALCODE.Width = 120
        '
        'xMATERIALNAME_EN
        '
        Me.xMATERIALNAME_EN.Caption = "MATERIALNAME EN"
        Me.xMATERIALNAME_EN.FieldName = "MATERIALNAME_EN"
        Me.xMATERIALNAME_EN.Name = "xMATERIALNAME_EN"
        Me.xMATERIALNAME_EN.OptionsColumn.AllowEdit = False
        Me.xMATERIALNAME_EN.OptionsColumn.ReadOnly = True
        Me.xMATERIALNAME_EN.Visible = True
        Me.xMATERIALNAME_EN.VisibleIndex = 2
        Me.xMATERIALNAME_EN.Width = 250
        '
        'cxMATERIALNAME_TH
        '
        Me.cxMATERIALNAME_TH.Caption = "MATERIALNAME TH"
        Me.cxMATERIALNAME_TH.FieldName = "MATERIALNAME_TH"
        Me.cxMATERIALNAME_TH.Name = "cxMATERIALNAME_TH"
        Me.cxMATERIALNAME_TH.OptionsColumn.AllowEdit = False
        Me.cxMATERIALNAME_TH.OptionsColumn.ReadOnly = True
        Me.cxMATERIALNAME_TH.Visible = True
        Me.cxMATERIALNAME_TH.VisibleIndex = 3
        Me.cxMATERIALNAME_TH.Width = 250
        '
        'xzMAINMATTYPE
        '
        Me.xzMAINMATTYPE.Caption = "MAIN MATTYPE"
        Me.xzMAINMATTYPE.FieldName = "MAINMATTYPE"
        Me.xzMAINMATTYPE.Name = "xzMAINMATTYPE"
        Me.xzMAINMATTYPE.OptionsColumn.AllowEdit = False
        Me.xzMAINMATTYPE.OptionsColumn.ReadOnly = True
        Me.xzMAINMATTYPE.Visible = True
        Me.xzMAINMATTYPE.VisibleIndex = 4
        Me.xzMAINMATTYPE.Width = 80
        '
        'cxMATGROUP
        '
        Me.cxMATGROUP.Caption = "MATGROUP"
        Me.cxMATGROUP.FieldName = "MATGROUP"
        Me.cxMATGROUP.Name = "cxMATGROUP"
        Me.cxMATGROUP.OptionsColumn.AllowEdit = False
        Me.cxMATGROUP.OptionsColumn.ReadOnly = True
        Me.cxMATGROUP.Visible = True
        Me.cxMATGROUP.VisibleIndex = 5
        Me.cxMATGROUP.Width = 80
        '
        'cxMATTYPE
        '
        Me.cxMATTYPE.Caption = "MATTYPE"
        Me.cxMATTYPE.FieldName = "MATTYPE"
        Me.cxMATTYPE.Name = "cxMATTYPE"
        Me.cxMATTYPE.OptionsColumn.AllowEdit = False
        Me.cxMATTYPE.OptionsColumn.ReadOnly = True
        Me.cxMATTYPE.Visible = True
        Me.cxMATTYPE.VisibleIndex = 6
        Me.cxMATTYPE.Width = 80
        '
        'cxFabricWidth
        '
        Me.cxFabricWidth.Caption = "FabricWidth"
        Me.cxFabricWidth.FieldName = "FabricWidth"
        Me.cxFabricWidth.Name = "cxFabricWidth"
        Me.cxFabricWidth.OptionsColumn.AllowEdit = False
        Me.cxFabricWidth.OptionsColumn.ReadOnly = True
        Me.cxFabricWidth.Visible = True
        Me.cxFabricWidth.VisibleIndex = 7
        Me.cxFabricWidth.Width = 80
        '
        'cxCustomer
        '
        Me.cxCustomer.Caption = "Customer"
        Me.cxCustomer.FieldName = "Customer"
        Me.cxCustomer.Name = "cxCustomer"
        Me.cxCustomer.OptionsColumn.AllowEdit = False
        Me.cxCustomer.OptionsColumn.ReadOnly = True
        Me.cxCustomer.Visible = True
        Me.cxCustomer.VisibleIndex = 8
        Me.cxCustomer.Width = 80
        '
        'cxCustomerRefCode
        '
        Me.cxCustomerRefCode.Caption = "CustomerRefCode"
        Me.cxCustomerRefCode.FieldName = "CustomerRefCode"
        Me.cxCustomerRefCode.Name = "cxCustomerRefCode"
        Me.cxCustomerRefCode.OptionsColumn.AllowEdit = False
        Me.cxCustomerRefCode.OptionsColumn.ReadOnly = True
        Me.cxCustomerRefCode.Visible = True
        Me.cxCustomerRefCode.VisibleIndex = 9
        Me.cxCustomerRefCode.Width = 80
        '
        'cxSupplier
        '
        Me.cxSupplier.Caption = "Supplier"
        Me.cxSupplier.FieldName = "Supplier"
        Me.cxSupplier.Name = "cxSupplier"
        Me.cxSupplier.OptionsColumn.AllowEdit = False
        Me.cxSupplier.OptionsColumn.ReadOnly = True
        Me.cxSupplier.Visible = True
        Me.cxSupplier.VisibleIndex = 10
        Me.cxSupplier.Width = 80
        '
        'cxUnit
        '
        Me.cxUnit.Caption = "Unit"
        Me.cxUnit.Name = "cxUnit"
        Me.cxUnit.OptionsColumn.AllowEdit = False
        Me.cxUnit.OptionsColumn.ReadOnly = True
        Me.cxUnit.Visible = True
        Me.cxUnit.VisibleIndex = 11
        Me.cxUnit.Width = 60
        '
        'cxCurrency
        '
        Me.cxCurrency.Caption = "Currency"
        Me.cxCurrency.FieldName = "Currency"
        Me.cxCurrency.Name = "cxCurrency"
        Me.cxCurrency.OptionsColumn.AllowEdit = False
        Me.cxCurrency.OptionsColumn.ReadOnly = True
        Me.cxCurrency.Visible = True
        Me.cxCurrency.VisibleIndex = 12
        Me.cxCurrency.Width = 60
        '
        'ogbcmd
        '
        Me.ogbcmd.Controls.Add(Me.ocmexit)
        Me.ogbcmd.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ogbcmd.Location = New System.Drawing.Point(0, 622)
        Me.ogbcmd.Name = "ogbcmd"
        Me.ogbcmd.ShowCaption = False
        Me.ogbcmd.Size = New System.Drawing.Size(1458, 43)
        Me.ogbcmd.TabIndex = 304
        Me.ogbcmd.Text = "GroupControl1"
        '
        'ocmexit
        '
        Me.ocmexit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmexit.Location = New System.Drawing.Point(1327, 10)
        Me.ocmexit.Name = "ocmexit"
        Me.ocmexit.Size = New System.Drawing.Size(100, 25)
        Me.ocmexit.TabIndex = 95
        Me.ocmexit.TabStop = False
        Me.ocmexit.Tag = "2|"
        Me.ocmexit.Text = "OK"
        '
        'otb
        '
        Me.otb.Dock = System.Windows.Forms.DockStyle.Fill
        Me.otb.Location = New System.Drawing.Point(0, 0)
        Me.otb.Name = "otb"
        Me.otb.SelectedTabPage = Me.otpmappingsupplier
        Me.otb.ShowTabHeader = DevExpress.Utils.DefaultBoolean.[False]
        Me.otb.Size = New System.Drawing.Size(1458, 622)
        Me.otb.TabIndex = 305
        Me.otb.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.otpmappingsupplier})
        '
        'otpmappingsupplier
        '
        Me.otpmappingsupplier.Controls.Add(Me.ogclist)
        Me.otpmappingsupplier.Name = "otpmappingsupplier"
        Me.otpmappingsupplier.Size = New System.Drawing.Size(1450, 614)
        Me.otpmappingsupplier.Text = "List Bom"
        '
        'wImportExcelMainMatListFinish
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1458, 665)
        Me.Controls.Add(Me.otb)
        Me.Controls.Add(Me.ogbcmd)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "wImportExcelMainMatListFinish"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ข้อมูลรายการนำเข้า MainMaterial "
        CType(Me.ogclist, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvlist, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepFTSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogbcmd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbcmd.ResumeLayout(False)
        CType(Me.otb, System.ComponentModel.ISupportInitialize).EndInit()
        Me.otb.ResumeLayout(False)
        Me.otpmappingsupplier.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RepFTSelect As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents ogbcmd As DevExpress.XtraEditors.GroupControl
    Public WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
    Public WithEvents ogclist As DevExpress.XtraGrid.GridControl
    Public WithEvents ogvlist As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents otb As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents otpmappingsupplier As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents FTStateImport As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents xMATERIALCODE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents xMATERIALNAME_EN As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cxMATERIALNAME_TH As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents xzMAINMATTYPE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cxMATGROUP As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cxMATTYPE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cxFabricWidth As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cxCustomer As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cxCustomerRefCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cxSupplier As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cxUnit As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cxCurrency As DevExpress.XtraGrid.Columns.GridColumn
End Class

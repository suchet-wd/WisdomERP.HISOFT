<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wInvoiceSelectPoup
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
        Me.ogcdetail = New DevExpress.XtraGrid.GridControl()
        Me.ogvdetail = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.cFTSelect = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryFTSelect = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.cFTInvoiceNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTCustomerPO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTOrderNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTStyleCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNHSysContinentId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTContinentCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNHSysCountryId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTCountryCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNHSysProvinceId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTProvinceCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNHSysShipModeId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTShipModeCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNHSysShipPortId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNTShipPortCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ocmok = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmcancel = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.ogcdetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvdetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryFTSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ogcdetail
        '
        Me.ogcdetail.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogcdetail.Location = New System.Drawing.Point(4, 3)
        Me.ogcdetail.MainView = Me.ogvdetail
        Me.ogcdetail.Name = "ogcdetail"
        Me.ogcdetail.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryFTSelect})
        Me.ogcdetail.Size = New System.Drawing.Size(949, 407)
        Me.ogcdetail.TabIndex = 0
        Me.ogcdetail.Tag = "2|"
        Me.ogcdetail.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvdetail})
        '
        'ogvdetail
        '
        Me.ogvdetail.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.cFTSelect, Me.cFTInvoiceNo, Me.cFTCustomerPO, Me.cFTOrderNo, Me.cFTStyleCode, Me.cFNHSysContinentId, Me.cFTContinentCode, Me.cFNHSysCountryId, Me.cFTCountryCode, Me.cFNHSysProvinceId, Me.cFTProvinceCode, Me.cFNHSysShipModeId, Me.cFTShipModeCode, Me.cFNHSysShipPortId, Me.cFNTShipPortCode})
        Me.ogvdetail.GridControl = Me.ogcdetail
        Me.ogvdetail.Name = "ogvdetail"
        Me.ogvdetail.OptionsView.ColumnAutoWidth = False
        Me.ogvdetail.OptionsView.ShowGroupPanel = False
        '
        'cFTSelect
        '
        Me.cFTSelect.Caption = "FTSelect"
        Me.cFTSelect.ColumnEdit = Me.RepositoryFTSelect
        Me.cFTSelect.FieldName = "FTSelect"
        Me.cFTSelect.Name = "cFTSelect"
        Me.cFTSelect.Visible = True
        Me.cFTSelect.VisibleIndex = 0
        Me.cFTSelect.Width = 63
        '
        'RepositoryFTSelect
        '
        Me.RepositoryFTSelect.AutoHeight = False
        Me.RepositoryFTSelect.Caption = "Check"
        Me.RepositoryFTSelect.Name = "RepositoryFTSelect"
        Me.RepositoryFTSelect.ValueChecked = "1"
        Me.RepositoryFTSelect.ValueUnchecked = "0"
        '
        'cFTInvoiceNo
        '
        Me.cFTInvoiceNo.Caption = "FTInvoiceNo"
        Me.cFTInvoiceNo.FieldName = "FTInvoiceNo"
        Me.cFTInvoiceNo.Name = "cFTInvoiceNo"
        Me.cFTInvoiceNo.OptionsColumn.AllowEdit = False
        Me.cFTInvoiceNo.Visible = True
        Me.cFTInvoiceNo.VisibleIndex = 1
        Me.cFTInvoiceNo.Width = 94
        '
        'cFTCustomerPO
        '
        Me.cFTCustomerPO.Caption = "FTCustomerPO"
        Me.cFTCustomerPO.FieldName = "FTCustomerPO"
        Me.cFTCustomerPO.Name = "cFTCustomerPO"
        Me.cFTCustomerPO.OptionsColumn.AllowEdit = False
        Me.cFTCustomerPO.Visible = True
        Me.cFTCustomerPO.VisibleIndex = 2
        Me.cFTCustomerPO.Width = 84
        '
        'cFTOrderNo
        '
        Me.cFTOrderNo.Caption = "FTOrderNo"
        Me.cFTOrderNo.FieldName = "FTOrderNo"
        Me.cFTOrderNo.Name = "cFTOrderNo"
        Me.cFTOrderNo.OptionsColumn.AllowEdit = False
        Me.cFTOrderNo.Visible = True
        Me.cFTOrderNo.VisibleIndex = 3
        Me.cFTOrderNo.Width = 81
        '
        'cFTStyleCode
        '
        Me.cFTStyleCode.Caption = "FTStyleCode"
        Me.cFTStyleCode.FieldName = "FTStyleCode"
        Me.cFTStyleCode.Name = "cFTStyleCode"
        Me.cFTStyleCode.OptionsColumn.AllowEdit = False
        Me.cFTStyleCode.Visible = True
        Me.cFTStyleCode.VisibleIndex = 4
        Me.cFTStyleCode.Width = 93
        '
        'cFNHSysContinentId
        '
        Me.cFNHSysContinentId.Caption = "FNHSysContinentId"
        Me.cFNHSysContinentId.FieldName = "FNHSysContinentId"
        Me.cFNHSysContinentId.Name = "cFNHSysContinentId"
        Me.cFNHSysContinentId.OptionsColumn.AllowEdit = False
        '
        'cFTContinentCode
        '
        Me.cFTContinentCode.Caption = "FTContinentCode"
        Me.cFTContinentCode.FieldName = "FTContinentCode"
        Me.cFTContinentCode.Name = "cFTContinentCode"
        Me.cFTContinentCode.OptionsColumn.AllowEdit = False
        Me.cFTContinentCode.Visible = True
        Me.cFTContinentCode.VisibleIndex = 5
        Me.cFTContinentCode.Width = 97
        '
        'cFNHSysCountryId
        '
        Me.cFNHSysCountryId.Caption = "FNHSysCountryId"
        Me.cFNHSysCountryId.FieldName = "FNHSysCountryId"
        Me.cFNHSysCountryId.Name = "cFNHSysCountryId"
        Me.cFNHSysCountryId.OptionsColumn.AllowEdit = False
        '
        'cFTCountryCode
        '
        Me.cFTCountryCode.Caption = "FTCountryCode"
        Me.cFTCountryCode.FieldName = "FTCountryCode"
        Me.cFTCountryCode.Name = "cFTCountryCode"
        Me.cFTCountryCode.OptionsColumn.AllowEdit = False
        Me.cFTCountryCode.Visible = True
        Me.cFTCountryCode.VisibleIndex = 6
        Me.cFTCountryCode.Width = 110
        '
        'cFNHSysProvinceId
        '
        Me.cFNHSysProvinceId.Caption = "FNHSysProvinceId"
        Me.cFNHSysProvinceId.FieldName = "FNHSysProvinceId"
        Me.cFNHSysProvinceId.Name = "cFNHSysProvinceId"
        Me.cFNHSysProvinceId.OptionsColumn.AllowEdit = False
        '
        'cFTProvinceCode
        '
        Me.cFTProvinceCode.Caption = "FTProvinceCode"
        Me.cFTProvinceCode.FieldName = "FTProvinceCode"
        Me.cFTProvinceCode.Name = "cFTProvinceCode"
        Me.cFTProvinceCode.OptionsColumn.AllowEdit = False
        Me.cFTProvinceCode.Visible = True
        Me.cFTProvinceCode.VisibleIndex = 7
        Me.cFTProvinceCode.Width = 104
        '
        'cFNHSysShipModeId
        '
        Me.cFNHSysShipModeId.Caption = "FNHSysShipModeId"
        Me.cFNHSysShipModeId.FieldName = "FNHSysShipModeId"
        Me.cFNHSysShipModeId.Name = "cFNHSysShipModeId"
        Me.cFNHSysShipModeId.OptionsColumn.AllowEdit = False
        '
        'cFTShipModeCode
        '
        Me.cFTShipModeCode.Caption = "FTShipModeCode"
        Me.cFTShipModeCode.FieldName = "FTShipModeCode"
        Me.cFTShipModeCode.Name = "cFTShipModeCode"
        Me.cFTShipModeCode.OptionsColumn.AllowEdit = False
        Me.cFTShipModeCode.Visible = True
        Me.cFTShipModeCode.VisibleIndex = 8
        Me.cFTShipModeCode.Width = 110
        '
        'cFNHSysShipPortId
        '
        Me.cFNHSysShipPortId.Caption = "FNHSysShipPortId"
        Me.cFNHSysShipPortId.FieldName = "FNHSysShipPortId"
        Me.cFNHSysShipPortId.Name = "cFNHSysShipPortId"
        Me.cFNHSysShipPortId.OptionsColumn.AllowEdit = False
        '
        'cFNTShipPortCode
        '
        Me.cFNTShipPortCode.Caption = "FNTShipPortCode"
        Me.cFNTShipPortCode.FieldName = "FNTShipPortCode"
        Me.cFNTShipPortCode.Name = "cFNTShipPortCode"
        Me.cFNTShipPortCode.OptionsColumn.AllowEdit = False
        Me.cFNTShipPortCode.Visible = True
        Me.cFNTShipPortCode.VisibleIndex = 9
        Me.cFNTShipPortCode.Width = 95
        '
        'ocmok
        '
        Me.ocmok.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmok.Location = New System.Drawing.Point(788, 416)
        Me.ocmok.Name = "ocmok"
        Me.ocmok.Size = New System.Drawing.Size(75, 23)
        Me.ocmok.TabIndex = 1
        Me.ocmok.Text = "OK"
        '
        'ocmcancel
        '
        Me.ocmcancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmcancel.Location = New System.Drawing.Point(874, 416)
        Me.ocmcancel.Name = "ocmcancel"
        Me.ocmcancel.Size = New System.Drawing.Size(75, 23)
        Me.ocmcancel.TabIndex = 1
        Me.ocmcancel.Text = "Cancel"
        '
        'wInvoiceSelectPoup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(955, 445)
        Me.Controls.Add(Me.ocmcancel)
        Me.Controls.Add(Me.ocmok)
        Me.Controls.Add(Me.ogcdetail)
        Me.Name = "wInvoiceSelectPoup"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "InvoiceSelectPoup"
        CType(Me.ogcdetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvdetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryFTSelect, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogcdetail As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvdetail As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents cFTSelect As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryFTSelect As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents cFTInvoiceNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTCustomerPO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTOrderNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTStyleCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNHSysContinentId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTContinentCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNHSysCountryId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTCountryCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNHSysProvinceId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTProvinceCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNHSysShipModeId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTShipModeCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNHSysShipPortId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNTShipPortCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ocmok As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmcancel As DevExpress.XtraEditors.SimpleButton
End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wGenerateBarcodeAsset
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
        Me.FTAssetCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTPurchaseNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTDocumentNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNPrice = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepFNQuantity = New DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit()
        Me.FNHSysFixedAssetId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNHSysWHId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNHSysUnitId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepFNQuantityBarcode = New DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmgenbarcode = New DevExpress.XtraEditors.SimpleButton()
        Me.ogbdetail = New DevExpress.XtraEditors.GroupControl()
        CType(Me.ogcdetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvdetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepFNQuantity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepFNQuantityBarcode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogbdetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbdetail.SuspendLayout()
        Me.SuspendLayout()
        '
        'ogcdetail
        '
        Me.ogcdetail.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogcdetail.Location = New System.Drawing.Point(5, 24)
        Me.ogcdetail.MainView = Me.ogvdetail
        Me.ogcdetail.Name = "ogcdetail"
        Me.ogcdetail.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepFNQuantity, Me.RepFNQuantityBarcode})
        Me.ogcdetail.Size = New System.Drawing.Size(676, 278)
        Me.ogcdetail.TabIndex = 3
        Me.ogcdetail.TabStop = False
        Me.ogcdetail.Tag = "2|"
        Me.ogcdetail.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvdetail})
        '
        'ogvdetail
        '
        Me.ogvdetail.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.FTAssetCode, Me.FTPurchaseNo, Me.FTDocumentNo, Me.FNPrice, Me.FNQuantity, Me.FNHSysFixedAssetId, Me.FNHSysWHId, Me.FNHSysUnitId})
        Me.ogvdetail.GridControl = Me.ogcdetail
        Me.ogvdetail.Name = "ogvdetail"
        Me.ogvdetail.OptionsCustomization.AllowGroup = False
        Me.ogvdetail.OptionsCustomization.AllowQuickHideColumns = False
        Me.ogvdetail.OptionsView.ColumnAutoWidth = False
        Me.ogvdetail.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.ogvdetail.OptionsView.ShowGroupPanel = False
        Me.ogvdetail.Tag = "2|"
        '
        'FTAssetCode
        '
        Me.FTAssetCode.AppearanceHeader.Options.UseTextOptions = True
        Me.FTAssetCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTAssetCode.Caption = "FTAssetCode"
        Me.FTAssetCode.FieldName = "FTAssetCode"
        Me.FTAssetCode.Name = "FTAssetCode"
        Me.FTAssetCode.OptionsColumn.AllowEdit = False
        Me.FTAssetCode.OptionsColumn.ReadOnly = True
        Me.FTAssetCode.Visible = True
        Me.FTAssetCode.VisibleIndex = 0
        Me.FTAssetCode.Width = 133
        '
        'FTPurchaseNo
        '
        Me.FTPurchaseNo.AppearanceHeader.Options.UseTextOptions = True
        Me.FTPurchaseNo.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTPurchaseNo.Caption = "FTPurchaseNo"
        Me.FTPurchaseNo.FieldName = "FTPurchaseNo"
        Me.FTPurchaseNo.Name = "FTPurchaseNo"
        Me.FTPurchaseNo.OptionsColumn.AllowEdit = False
        Me.FTPurchaseNo.OptionsColumn.ReadOnly = True
        Me.FTPurchaseNo.Visible = True
        Me.FTPurchaseNo.VisibleIndex = 1
        Me.FTPurchaseNo.Width = 168
        '
        'FTDocumentNo
        '
        Me.FTDocumentNo.AppearanceHeader.Options.UseTextOptions = True
        Me.FTDocumentNo.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTDocumentNo.Caption = "FTDocumentNo"
        Me.FTDocumentNo.FieldName = "FTDocumentNo"
        Me.FTDocumentNo.Name = "FTDocumentNo"
        Me.FTDocumentNo.OptionsColumn.AllowEdit = False
        Me.FTDocumentNo.OptionsColumn.ReadOnly = True
        Me.FTDocumentNo.Visible = True
        Me.FTDocumentNo.VisibleIndex = 2
        Me.FTDocumentNo.Width = 117
        '
        'FNPrice
        '
        Me.FNPrice.AppearanceHeader.Options.UseTextOptions = True
        Me.FNPrice.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FNPrice.Caption = "FNPrice"
        Me.FNPrice.DisplayFormat.FormatString = "{0:n4}"
        Me.FNPrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNPrice.FieldName = "FNPricePerStock"
        Me.FNPrice.Name = "FNPrice"
        Me.FNPrice.OptionsColumn.AllowEdit = False
        Me.FNPrice.OptionsColumn.ReadOnly = True
        Me.FNPrice.Visible = True
        Me.FNPrice.VisibleIndex = 3
        Me.FNPrice.Width = 104
        '
        'FNQuantity
        '
        Me.FNQuantity.AppearanceHeader.Options.UseTextOptions = True
        Me.FNQuantity.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FNQuantity.Caption = "FNQuantity"
        Me.FNQuantity.ColumnEdit = Me.RepFNQuantity
        Me.FNQuantity.DisplayFormat.FormatString = "{0:n0}"
        Me.FNQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNQuantity.FieldName = "FNQuantityStock"
        Me.FNQuantity.Name = "FNQuantity"
        Me.FNQuantity.OptionsColumn.AllowEdit = False
        Me.FNQuantity.OptionsColumn.ReadOnly = True
        Me.FNQuantity.Visible = True
        Me.FNQuantity.VisibleIndex = 4
        Me.FNQuantity.Width = 97
        '
        'RepFNQuantity
        '
        Me.RepFNQuantity.AutoHeight = False
        Me.RepFNQuantity.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepFNQuantity.Name = "RepFNQuantity"
        '
        'FNHSysFixedAssetId
        '
        Me.FNHSysFixedAssetId.Caption = "FNHSysFixedAssetId"
        Me.FNHSysFixedAssetId.FieldName = "FNHSysFixedAssetId"
        Me.FNHSysFixedAssetId.Name = "FNHSysFixedAssetId"
        '
        'FNHSysWHId
        '
        Me.FNHSysWHId.Caption = "FNHSysWHId"
        Me.FNHSysWHId.FieldName = "FNHSysWHId"
        Me.FNHSysWHId.Name = "FNHSysWHId"
        '
        'FNHSysUnitId
        '
        Me.FNHSysUnitId.Caption = "FNHSysUnitId"
        Me.FNHSysUnitId.FieldName = "FNHSysUnitIdStock"
        Me.FNHSysUnitId.Name = "FNHSysUnitId"
        '
        'RepFNQuantityBarcode
        '
        Me.RepFNQuantityBarcode.AutoHeight = False
        Me.RepFNQuantityBarcode.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepFNQuantityBarcode.Name = "RepFNQuantityBarcode"
        '
        'ocmexit
        '
        Me.ocmexit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmexit.Location = New System.Drawing.Point(486, 1)
        Me.ocmexit.Name = "ocmexit"
        Me.ocmexit.Size = New System.Drawing.Size(177, 20)
        Me.ocmexit.TabIndex = 95
        Me.ocmexit.TabStop = False
        Me.ocmexit.Tag = "2|"
        Me.ocmexit.Text = "EXIT"
        '
        'ocmgenbarcode
        '
        Me.ocmgenbarcode.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmgenbarcode.Location = New System.Drawing.Point(300, 1)
        Me.ocmgenbarcode.Name = "ocmgenbarcode"
        Me.ocmgenbarcode.Size = New System.Drawing.Size(177, 20)
        Me.ocmgenbarcode.TabIndex = 94
        Me.ocmgenbarcode.TabStop = False
        Me.ocmgenbarcode.Tag = "2|"
        Me.ocmgenbarcode.Text = "GENERATE BARCODE"
        '
        'ogbdetail
        '
        Me.ogbdetail.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbdetail.Controls.Add(Me.ocmexit)
        Me.ogbdetail.Controls.Add(Me.ogcdetail)
        Me.ogbdetail.Controls.Add(Me.ocmgenbarcode)
        Me.ogbdetail.Location = New System.Drawing.Point(4, 5)
        Me.ogbdetail.Name = "ogbdetail"
        Me.ogbdetail.Size = New System.Drawing.Size(686, 307)
        Me.ogbdetail.TabIndex = 5
        Me.ogbdetail.Text = "Detail"
        '
        'wGenerateBarcodeAsset
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(693, 314)
        Me.ControlBox = False
        Me.Controls.Add(Me.ogbdetail)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "wGenerateBarcodeAsset"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "wGenerateBarcode"
        CType(Me.ogcdetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvdetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepFNQuantity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepFNQuantityBarcode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogbdetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbdetail.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogcdetail As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvdetail As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmgenbarcode As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ogbdetail As DevExpress.XtraEditors.GroupControl
    Friend WithEvents FTAssetCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTPurchaseNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepFNQuantity As DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit
    Friend WithEvents RepFNQuantityBarcode As DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit
    Friend WithEvents FNHSysFixedAssetId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNHSysWHId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTDocumentNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNHSysUnitId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNPrice As DevExpress.XtraGrid.Columns.GridColumn
End Class

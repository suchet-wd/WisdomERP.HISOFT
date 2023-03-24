<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wTransferOrderAddBarcode
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
        Me.ogcbarcode = New DevExpress.XtraGrid.GridControl()
        Me.ogvbarcode = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.FTSelect = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ReposFTSelect = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.BFTRawMatCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.BFTRawMatColorCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTRawMatColorName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.BFTRawMatSizeCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTBarcodeNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.BFTOrderNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.BFNQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNReserveQty = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ReposFNReserveQty = New DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit()
        Me.FTDocumentNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTPurchaseNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ogb = New DevExpress.XtraEditors.GroupControl()
        Me.FTStaReceiveAll = New DevExpress.XtraEditors.CheckEdit()
        Me.ocmcancel = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmok = New DevExpress.XtraEditors.SimpleButton()
        Me.FTWHCode = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.ogcbarcode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvbarcode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ReposFTSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ReposFNReserveQty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogb, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogb.SuspendLayout()
        CType(Me.FTStaReceiveAll.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ogcbarcode
        '
        Me.ogcbarcode.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogcbarcode.Location = New System.Drawing.Point(5, 24)
        Me.ogcbarcode.MainView = Me.ogvbarcode
        Me.ogcbarcode.Name = "ogcbarcode"
        Me.ogcbarcode.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.ReposFTSelect, Me.ReposFNReserveQty})
        Me.ogcbarcode.Size = New System.Drawing.Size(1017, 405)
        Me.ogcbarcode.TabIndex = 3
        Me.ogcbarcode.TabStop = False
        Me.ogcbarcode.Tag = "2|"
        Me.ogcbarcode.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvbarcode})
        '
        'ogvbarcode
        '
        Me.ogvbarcode.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.FTSelect, Me.BFTRawMatCode, Me.BFTRawMatColorCode, Me.FTRawMatColorName, Me.BFTRawMatSizeCode, Me.FTBarcodeNo, Me.BFTOrderNo, Me.FTWHCode, Me.BFNQuantity, Me.FNReserveQty, Me.FTDocumentNo, Me.FTPurchaseNo})
        Me.ogvbarcode.GridControl = Me.ogcbarcode
        Me.ogvbarcode.Name = "ogvbarcode"
        Me.ogvbarcode.OptionsCustomization.AllowGroup = False
        Me.ogvbarcode.OptionsCustomization.AllowQuickHideColumns = False
        Me.ogvbarcode.OptionsView.ColumnAutoWidth = False
        Me.ogvbarcode.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.ogvbarcode.OptionsView.ShowGroupPanel = False
        Me.ogvbarcode.Tag = "2|"
        '
        'FTSelect
        '
        Me.FTSelect.AppearanceCell.BackColor = System.Drawing.Color.LightCyan
        Me.FTSelect.AppearanceCell.Options.UseBackColor = True
        Me.FTSelect.AppearanceHeader.Options.UseTextOptions = True
        Me.FTSelect.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTSelect.Caption = " "
        Me.FTSelect.ColumnEdit = Me.ReposFTSelect
        Me.FTSelect.FieldName = "FTSelect"
        Me.FTSelect.Name = "FTSelect"
        Me.FTSelect.OptionsColumn.AllowMove = False
        Me.FTSelect.Visible = True
        Me.FTSelect.VisibleIndex = 0
        Me.FTSelect.Width = 33
        '
        'ReposFTSelect
        '
        Me.ReposFTSelect.AutoHeight = False
        Me.ReposFTSelect.Caption = "Check"
        Me.ReposFTSelect.Name = "ReposFTSelect"
        Me.ReposFTSelect.ValueChecked = "1"
        Me.ReposFTSelect.ValueUnchecked = "0"
        '
        'BFTRawMatCode
        '
        Me.BFTRawMatCode.AppearanceHeader.Options.UseTextOptions = True
        Me.BFTRawMatCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.BFTRawMatCode.Caption = "FTRawMatCode"
        Me.BFTRawMatCode.FieldName = "FTRawMatCode"
        Me.BFTRawMatCode.Name = "BFTRawMatCode"
        Me.BFTRawMatCode.OptionsColumn.AllowEdit = False
        Me.BFTRawMatCode.OptionsColumn.AllowMove = False
        Me.BFTRawMatCode.OptionsColumn.ReadOnly = True
        Me.BFTRawMatCode.Visible = True
        Me.BFTRawMatCode.VisibleIndex = 1
        Me.BFTRawMatCode.Width = 122
        '
        'BFTRawMatColorCode
        '
        Me.BFTRawMatColorCode.AppearanceHeader.Options.UseTextOptions = True
        Me.BFTRawMatColorCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.BFTRawMatColorCode.Caption = "FTRawMatColorCode"
        Me.BFTRawMatColorCode.FieldName = "FTRawMatColorCode"
        Me.BFTRawMatColorCode.Name = "BFTRawMatColorCode"
        Me.BFTRawMatColorCode.OptionsColumn.AllowEdit = False
        Me.BFTRawMatColorCode.OptionsColumn.AllowMove = False
        Me.BFTRawMatColorCode.OptionsColumn.FixedWidth = True
        Me.BFTRawMatColorCode.OptionsColumn.ReadOnly = True
        Me.BFTRawMatColorCode.Visible = True
        Me.BFTRawMatColorCode.VisibleIndex = 2
        Me.BFTRawMatColorCode.Width = 80
        '
        'FTRawMatColorName
        '
        Me.FTRawMatColorName.Caption = "Color Name"
        Me.FTRawMatColorName.FieldName = "FTRawMatColorName"
        Me.FTRawMatColorName.Name = "FTRawMatColorName"
        Me.FTRawMatColorName.OptionsColumn.AllowEdit = False
        Me.FTRawMatColorName.OptionsColumn.FixedWidth = True
        Me.FTRawMatColorName.OptionsColumn.ReadOnly = True
        Me.FTRawMatColorName.Visible = True
        Me.FTRawMatColorName.VisibleIndex = 3
        Me.FTRawMatColorName.Width = 90
        '
        'BFTRawMatSizeCode
        '
        Me.BFTRawMatSizeCode.AppearanceHeader.Options.UseTextOptions = True
        Me.BFTRawMatSizeCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.BFTRawMatSizeCode.Caption = "FTRawMatSizeCode"
        Me.BFTRawMatSizeCode.FieldName = "FTRawMatSizeCode"
        Me.BFTRawMatSizeCode.Name = "BFTRawMatSizeCode"
        Me.BFTRawMatSizeCode.OptionsColumn.AllowEdit = False
        Me.BFTRawMatSizeCode.OptionsColumn.AllowMove = False
        Me.BFTRawMatSizeCode.OptionsColumn.FixedWidth = True
        Me.BFTRawMatSizeCode.OptionsColumn.ReadOnly = True
        Me.BFTRawMatSizeCode.Visible = True
        Me.BFTRawMatSizeCode.VisibleIndex = 4
        Me.BFTRawMatSizeCode.Width = 80
        '
        'FTBarcodeNo
        '
        Me.FTBarcodeNo.AppearanceHeader.Options.UseTextOptions = True
        Me.FTBarcodeNo.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTBarcodeNo.Caption = "FTBarcodeNo"
        Me.FTBarcodeNo.FieldName = "FTBarcodeNo"
        Me.FTBarcodeNo.Name = "FTBarcodeNo"
        Me.FTBarcodeNo.OptionsColumn.AllowEdit = False
        Me.FTBarcodeNo.OptionsColumn.AllowMove = False
        Me.FTBarcodeNo.OptionsColumn.ReadOnly = True
        Me.FTBarcodeNo.Visible = True
        Me.FTBarcodeNo.VisibleIndex = 5
        Me.FTBarcodeNo.Width = 115
        '
        'BFTOrderNo
        '
        Me.BFTOrderNo.AppearanceHeader.Options.UseTextOptions = True
        Me.BFTOrderNo.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.BFTOrderNo.Caption = "FTOrderNo"
        Me.BFTOrderNo.FieldName = "FTOrderNo"
        Me.BFTOrderNo.Name = "BFTOrderNo"
        Me.BFTOrderNo.OptionsColumn.AllowEdit = False
        Me.BFTOrderNo.OptionsColumn.AllowMove = False
        Me.BFTOrderNo.OptionsColumn.FixedWidth = True
        Me.BFTOrderNo.OptionsColumn.ReadOnly = True
        Me.BFTOrderNo.Visible = True
        Me.BFTOrderNo.VisibleIndex = 7
        Me.BFTOrderNo.Width = 90
        '
        'BFNQuantity
        '
        Me.BFNQuantity.AppearanceCell.Options.UseTextOptions = True
        Me.BFNQuantity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.BFNQuantity.AppearanceHeader.Options.UseTextOptions = True
        Me.BFNQuantity.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.BFNQuantity.Caption = "Bal. Quantity"
        Me.BFNQuantity.DisplayFormat.FormatString = "N4"
        Me.BFNQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.BFNQuantity.FieldName = "FNQuantityBal"
        Me.BFNQuantity.Name = "BFNQuantity"
        Me.BFNQuantity.OptionsColumn.AllowEdit = False
        Me.BFNQuantity.OptionsColumn.AllowMove = False
        Me.BFNQuantity.OptionsColumn.ReadOnly = True
        Me.BFNQuantity.Visible = True
        Me.BFNQuantity.VisibleIndex = 9
        Me.BFNQuantity.Width = 101
        '
        'FNReserveQty
        '
        Me.FNReserveQty.AppearanceCell.BackColor = System.Drawing.Color.LightCyan
        Me.FNReserveQty.AppearanceCell.Options.UseBackColor = True
        Me.FNReserveQty.AppearanceHeader.Options.UseTextOptions = True
        Me.FNReserveQty.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FNReserveQty.Caption = "Reserve Qty"
        Me.FNReserveQty.ColumnEdit = Me.ReposFNReserveQty
        Me.FNReserveQty.FieldName = "FNReserveQty"
        Me.FNReserveQty.Name = "FNReserveQty"
        Me.FNReserveQty.OptionsColumn.AllowMove = False
        Me.FNReserveQty.Visible = True
        Me.FNReserveQty.VisibleIndex = 10
        Me.FNReserveQty.Width = 92
        '
        'ReposFNReserveQty
        '
        Me.ReposFNReserveQty.AutoHeight = False
        Me.ReposFNReserveQty.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ReposFNReserveQty.DisplayFormat.FormatString = "N4"
        Me.ReposFNReserveQty.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.ReposFNReserveQty.EditFormat.FormatString = "N4"
        Me.ReposFNReserveQty.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.ReposFNReserveQty.Name = "ReposFNReserveQty"
        '
        'FTDocumentNo
        '
        Me.FTDocumentNo.Caption = "FTDocumentNo"
        Me.FTDocumentNo.FieldName = "FTDocumentNo"
        Me.FTDocumentNo.Name = "FTDocumentNo"
        '
        'FTPurchaseNo
        '
        Me.FTPurchaseNo.Caption = "Purchase No"
        Me.FTPurchaseNo.FieldName = "FTPurchaseNo"
        Me.FTPurchaseNo.Name = "FTPurchaseNo"
        Me.FTPurchaseNo.OptionsColumn.AllowEdit = False
        Me.FTPurchaseNo.OptionsColumn.ReadOnly = True
        Me.FTPurchaseNo.Visible = True
        Me.FTPurchaseNo.VisibleIndex = 6
        Me.FTPurchaseNo.Width = 120
        '
        'ogb
        '
        Me.ogb.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogb.Controls.Add(Me.FTStaReceiveAll)
        Me.ogb.Controls.Add(Me.ocmcancel)
        Me.ogb.Controls.Add(Me.ocmok)
        Me.ogb.Controls.Add(Me.ogcbarcode)
        Me.ogb.Location = New System.Drawing.Point(1, 2)
        Me.ogb.Name = "ogb"
        Me.ogb.Size = New System.Drawing.Size(1022, 433)
        Me.ogb.TabIndex = 4
        Me.ogb.Text = "Barcode Detail"
        '
        'FTStaReceiveAll
        '
        Me.FTStaReceiveAll.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FTStaReceiveAll.Location = New System.Drawing.Point(509, 1)
        Me.FTStaReceiveAll.Name = "FTStaReceiveAll"
        Me.FTStaReceiveAll.Properties.Appearance.Options.UseTextOptions = True
        Me.FTStaReceiveAll.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTStaReceiveAll.Properties.Caption = "Receive All"
        Me.FTStaReceiveAll.Properties.GlyphAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTStaReceiveAll.Properties.ValueChecked = "1"
        Me.FTStaReceiveAll.Properties.ValueUnchecked = "0"
        Me.FTStaReceiveAll.Size = New System.Drawing.Size(151, 19)
        Me.FTStaReceiveAll.TabIndex = 105
        Me.FTStaReceiveAll.Tag = "2|"
        Me.FTStaReceiveAll.Visible = False
        '
        'ocmcancel
        '
        Me.ocmcancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmcancel.Location = New System.Drawing.Point(847, 1)
        Me.ocmcancel.Name = "ocmcancel"
        Me.ocmcancel.Size = New System.Drawing.Size(160, 20)
        Me.ocmcancel.TabIndex = 104
        Me.ocmcancel.TabStop = False
        Me.ocmcancel.Tag = "2|"
        Me.ocmcancel.Text = "CANCEL"
        '
        'ocmok
        '
        Me.ocmok.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmok.Location = New System.Drawing.Point(672, 1)
        Me.ocmok.Name = "ocmok"
        Me.ocmok.Size = New System.Drawing.Size(160, 20)
        Me.ocmok.TabIndex = 103
        Me.ocmok.TabStop = False
        Me.ocmok.Tag = "2|"
        Me.ocmok.Text = "OK"
        '
        'FTWHCode
        '
        Me.FTWHCode.Caption = "WH"
        Me.FTWHCode.FieldName = "FTWHCode"
        Me.FTWHCode.Name = "FTWHCode"
        Me.FTWHCode.OptionsColumn.AllowEdit = False
        Me.FTWHCode.OptionsColumn.ReadOnly = True
        Me.FTWHCode.Visible = True
        Me.FTWHCode.VisibleIndex = 8
        '
        'wTransferOrderAddBarcode
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1025, 437)
        Me.ControlBox = False
        Me.Controls.Add(Me.ogb)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "wTransferOrderAddBarcode"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Transfer Item"
        CType(Me.ogcbarcode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvbarcode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ReposFTSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ReposFNReserveQty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogb, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogb.ResumeLayout(False)
        CType(Me.FTStaReceiveAll.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogcbarcode As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvbarcode As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents FTSelect As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ReposFTSelect As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents FTBarcodeNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents BFTRawMatCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents BFTRawMatColorCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents BFTRawMatSizeCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents BFTOrderNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents BFNQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNReserveQty As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ReposFNReserveQty As DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit
    Friend WithEvents ogb As DevExpress.XtraEditors.GroupControl
    Friend WithEvents FTStaReceiveAll As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents ocmcancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmok As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FTDocumentNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTPurchaseNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTRawMatColorName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTWHCode As DevExpress.XtraGrid.Columns.GridColumn
End Class

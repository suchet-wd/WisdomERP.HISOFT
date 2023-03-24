<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wReportStockOnhandListBarcode
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
        Me.FTWHNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.BFTBarcodeNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTRawMatCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTRawMatName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTRawMatColorCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTRawMatSizeCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTFabricFrontSize = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.BFTOrderNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GFTPurchaseNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNQuantityOut = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNQuantityBal = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.ogcdetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvdetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ogcdetail
        '
        Me.ogcdetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogcdetail.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcdetail.Location = New System.Drawing.Point(0, 0)
        Me.ogcdetail.MainView = Me.ogvdetail
        Me.ogcdetail.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcdetail.Name = "ogcdetail"
        Me.ogcdetail.Size = New System.Drawing.Size(920, 668)
        Me.ogcdetail.TabIndex = 4
        Me.ogcdetail.TabStop = False
        Me.ogcdetail.Tag = "2|"
        Me.ogcdetail.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvdetail})
        '
        'ogvdetail
        '
        Me.ogvdetail.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.FTWHNo, Me.BFTBarcodeNo, Me.FTRawMatCode, Me.FTRawMatName, Me.FTRawMatColorCode, Me.FTRawMatSizeCode, Me.FTFabricFrontSize, Me.BFTOrderNo, Me.GFTPurchaseNo, Me.FNQuantity, Me.FNQuantityOut, Me.FNQuantityBal})
        Me.ogvdetail.GridControl = Me.ogcdetail
        Me.ogvdetail.GroupSummary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.None, "FTRawMatCode", Me.FTRawMatSizeCode, "{0:n0}"), New DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FNQuantityBal", Me.FNQuantityBal, "{0:n4}")})
        Me.ogvdetail.Name = "ogvdetail"
        Me.ogvdetail.OptionsCustomization.AllowGroup = False
        Me.ogvdetail.OptionsCustomization.AllowQuickHideColumns = False
        Me.ogvdetail.OptionsView.ColumnAutoWidth = False
        Me.ogvdetail.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.ogvdetail.OptionsView.ShowAutoFilterRow = True
        Me.ogvdetail.OptionsView.ShowFooter = True
        Me.ogvdetail.OptionsView.ShowGroupPanel = False
        Me.ogvdetail.Tag = "2|"
        '
        'FTWHNo
        '
        Me.FTWHNo.AppearanceHeader.Options.UseTextOptions = True
        Me.FTWHNo.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTWHNo.Caption = "FTWHNo"
        Me.FTWHNo.FieldName = "FTWHNo"
        Me.FTWHNo.Name = "FTWHNo"
        Me.FTWHNo.OptionsColumn.AllowEdit = False
        Me.FTWHNo.OptionsColumn.ReadOnly = True
        Me.FTWHNo.Visible = True
        Me.FTWHNo.VisibleIndex = 1
        '
        'BFTBarcodeNo
        '
        Me.BFTBarcodeNo.AppearanceHeader.Options.UseTextOptions = True
        Me.BFTBarcodeNo.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.BFTBarcodeNo.Caption = "FTBarcodeNo"
        Me.BFTBarcodeNo.FieldName = "FTBarcodeNo"
        Me.BFTBarcodeNo.Name = "BFTBarcodeNo"
        Me.BFTBarcodeNo.OptionsColumn.ReadOnly = True
        Me.BFTBarcodeNo.Visible = True
        Me.BFTBarcodeNo.VisibleIndex = 0
        Me.BFTBarcodeNo.Width = 145
        '
        'FTRawMatCode
        '
        Me.FTRawMatCode.AppearanceHeader.Options.UseTextOptions = True
        Me.FTRawMatCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTRawMatCode.Caption = "FTRawMatCode"
        Me.FTRawMatCode.FieldName = "FTRawMatCode"
        Me.FTRawMatCode.Name = "FTRawMatCode"
        Me.FTRawMatCode.OptionsColumn.AllowEdit = False
        Me.FTRawMatCode.OptionsColumn.ReadOnly = True
        Me.FTRawMatCode.Width = 105
        '
        'FTRawMatName
        '
        Me.FTRawMatName.AppearanceHeader.Options.UseTextOptions = True
        Me.FTRawMatName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTRawMatName.Caption = "FTRawMatName"
        Me.FTRawMatName.FieldName = "FTRawMatName"
        Me.FTRawMatName.Name = "FTRawMatName"
        Me.FTRawMatName.OptionsColumn.AllowEdit = False
        Me.FTRawMatName.OptionsColumn.ReadOnly = True
        Me.FTRawMatName.Width = 184
        '
        'FTRawMatColorCode
        '
        Me.FTRawMatColorCode.AppearanceHeader.Options.UseTextOptions = True
        Me.FTRawMatColorCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTRawMatColorCode.Caption = "FTRawMatColorCode"
        Me.FTRawMatColorCode.FieldName = "FTRawMatColorCode"
        Me.FTRawMatColorCode.Name = "FTRawMatColorCode"
        Me.FTRawMatColorCode.OptionsColumn.AllowEdit = False
        Me.FTRawMatColorCode.OptionsColumn.ReadOnly = True
        Me.FTRawMatColorCode.Width = 95
        '
        'FTRawMatSizeCode
        '
        Me.FTRawMatSizeCode.AppearanceHeader.Options.UseTextOptions = True
        Me.FTRawMatSizeCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTRawMatSizeCode.Caption = "FTRawMatSizeCode"
        Me.FTRawMatSizeCode.FieldName = "FTRawMatSizeCode"
        Me.FTRawMatSizeCode.Name = "FTRawMatSizeCode"
        Me.FTRawMatSizeCode.OptionsColumn.AllowEdit = False
        Me.FTRawMatSizeCode.OptionsColumn.ReadOnly = True
        Me.FTRawMatSizeCode.Width = 93
        '
        'FTFabricFrontSize
        '
        Me.FTFabricFrontSize.AppearanceHeader.Options.UseTextOptions = True
        Me.FTFabricFrontSize.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTFabricFrontSize.Caption = "FTFabricFrontSize"
        Me.FTFabricFrontSize.FieldName = "FTFabricFrontSize"
        Me.FTFabricFrontSize.Name = "FTFabricFrontSize"
        Me.FTFabricFrontSize.OptionsColumn.AllowEdit = False
        Me.FTFabricFrontSize.OptionsColumn.ReadOnly = True
        '
        'BFTOrderNo
        '
        Me.BFTOrderNo.AppearanceHeader.Options.UseTextOptions = True
        Me.BFTOrderNo.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.BFTOrderNo.Caption = "FTOrderNo"
        Me.BFTOrderNo.FieldName = "FTOrderNo"
        Me.BFTOrderNo.Name = "BFTOrderNo"
        Me.BFTOrderNo.OptionsColumn.AllowEdit = False
        Me.BFTOrderNo.OptionsColumn.ReadOnly = True
        Me.BFTOrderNo.Visible = True
        Me.BFTOrderNo.VisibleIndex = 2
        Me.BFTOrderNo.Width = 122
        '
        'GFTPurchaseNo
        '
        Me.GFTPurchaseNo.AppearanceCell.Options.UseTextOptions = True
        Me.GFTPurchaseNo.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GFTPurchaseNo.Caption = "FTPurchaseNo"
        Me.GFTPurchaseNo.FieldName = "FTPurchaseNo"
        Me.GFTPurchaseNo.Name = "GFTPurchaseNo"
        Me.GFTPurchaseNo.OptionsColumn.AllowEdit = False
        Me.GFTPurchaseNo.OptionsColumn.ReadOnly = True
        Me.GFTPurchaseNo.Visible = True
        Me.GFTPurchaseNo.VisibleIndex = 3
        Me.GFTPurchaseNo.Width = 120
        '
        'FNQuantity
        '
        Me.FNQuantity.AppearanceCell.Options.UseTextOptions = True
        Me.FNQuantity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNQuantity.AppearanceHeader.Options.UseTextOptions = True
        Me.FNQuantity.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FNQuantity.Caption = "FNQuantity"
        Me.FNQuantity.DisplayFormat.FormatString = "{0:n4}"
        Me.FNQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNQuantity.FieldName = "FNQuantity"
        Me.FNQuantity.Name = "FNQuantity"
        Me.FNQuantity.OptionsColumn.AllowEdit = False
        Me.FNQuantity.OptionsColumn.ReadOnly = True
        Me.FNQuantity.Visible = True
        Me.FNQuantity.VisibleIndex = 4
        Me.FNQuantity.Width = 96
        '
        'FNQuantityOut
        '
        Me.FNQuantityOut.AppearanceCell.Options.UseTextOptions = True
        Me.FNQuantityOut.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNQuantityOut.Caption = "Quantity Out"
        Me.FNQuantityOut.DisplayFormat.FormatString = "{0:n4}"
        Me.FNQuantityOut.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNQuantityOut.FieldName = "FNQuantityOut"
        Me.FNQuantityOut.Name = "FNQuantityOut"
        Me.FNQuantityOut.OptionsColumn.AllowEdit = False
        Me.FNQuantityOut.OptionsColumn.ReadOnly = True
        Me.FNQuantityOut.Visible = True
        Me.FNQuantityOut.VisibleIndex = 5
        Me.FNQuantityOut.Width = 100
        '
        'FNQuantityBal
        '
        Me.FNQuantityBal.AppearanceCell.Options.UseTextOptions = True
        Me.FNQuantityBal.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNQuantityBal.AppearanceHeader.Options.UseTextOptions = True
        Me.FNQuantityBal.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FNQuantityBal.Caption = "Quantity Bal"
        Me.FNQuantityBal.DisplayFormat.FormatString = "{0:n4}"
        Me.FNQuantityBal.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNQuantityBal.FieldName = "FNQuantityBal"
        Me.FNQuantityBal.Name = "FNQuantityBal"
        Me.FNQuantityBal.OptionsColumn.AllowEdit = False
        Me.FNQuantityBal.OptionsColumn.ReadOnly = True
        Me.FNQuantityBal.Visible = True
        Me.FNQuantityBal.VisibleIndex = 6
        Me.FNQuantityBal.Width = 96
        '
        'wReportStockOnhandListBarcode
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(920, 668)
        Me.Controls.Add(Me.ogcdetail)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "wReportStockOnhandListBarcode"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "List Barcode Onhand"
        CType(Me.ogcdetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvdetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogcdetail As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvdetail As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents FTWHNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents BFTBarcodeNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTRawMatCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTRawMatName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTRawMatColorCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTRawMatSizeCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTFabricFrontSize As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents BFTOrderNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GFTPurchaseNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNQuantityOut As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNQuantityBal As DevExpress.XtraGrid.Columns.GridColumn
End Class

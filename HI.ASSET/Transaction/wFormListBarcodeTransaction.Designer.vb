<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wFormListBarcodeTransaction
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
        Me.FTBarcodeNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTDocumentNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNQuantityIN = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTWHAssetCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNQuantityOUT = New DevExpress.XtraGrid.Columns.GridColumn()
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
        Me.ogcdetail.Size = New System.Drawing.Size(738, 668)
        Me.ogcdetail.TabIndex = 4
        Me.ogcdetail.TabStop = False
        Me.ogcdetail.Tag = "2|"
        Me.ogcdetail.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvdetail})
        '
        'ogvdetail
        '
        Me.ogvdetail.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.FTBarcodeNo, Me.FTDocumentNo, Me.FTWHAssetCode, Me.FNQuantityIN, Me.FNQuantityOUT})
        Me.ogvdetail.GridControl = Me.ogcdetail
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
        'FTBarcodeNo
        '
        Me.FTBarcodeNo.AppearanceHeader.Options.UseTextOptions = True
        Me.FTBarcodeNo.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTBarcodeNo.Caption = "FTBarcodeNo"
        Me.FTBarcodeNo.FieldName = "FTBarcodeNo"
        Me.FTBarcodeNo.Name = "FTBarcodeNo"
        Me.FTBarcodeNo.OptionsColumn.AllowEdit = False
        Me.FTBarcodeNo.OptionsColumn.ReadOnly = True
        Me.FTBarcodeNo.Visible = True
        Me.FTBarcodeNo.VisibleIndex = 0
        Me.FTBarcodeNo.Width = 120
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
        Me.FTDocumentNo.VisibleIndex = 1
        Me.FTDocumentNo.Width = 120
        '
        'FNQuantityIN
        '
        Me.FNQuantityIN.AppearanceCell.Options.UseTextOptions = True
        Me.FNQuantityIN.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNQuantityIN.AppearanceHeader.Options.UseTextOptions = True
        Me.FNQuantityIN.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FNQuantityIN.Caption = "FNQuantityIN"
        Me.FNQuantityIN.DisplayFormat.FormatString = "{0:n4}"
        Me.FNQuantityIN.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNQuantityIN.FieldName = "FNQuantityIN"
        Me.FNQuantityIN.Name = "FNQuantityIN"
        Me.FNQuantityIN.OptionsColumn.AllowEdit = False
        Me.FNQuantityIN.OptionsColumn.ReadOnly = True
        Me.FNQuantityIN.Visible = True
        Me.FNQuantityIN.VisibleIndex = 3
        Me.FNQuantityIN.Width = 120
        '
        'FTWHAssetCode
        '
        Me.FTWHAssetCode.Caption = "FTWHAssetCode"
        Me.FTWHAssetCode.FieldName = "FTWHAssetCode"
        Me.FTWHAssetCode.Name = "FTWHAssetCode"
        Me.FTWHAssetCode.OptionsColumn.AllowEdit = False
        Me.FTWHAssetCode.OptionsColumn.ReadOnly = True
        Me.FTWHAssetCode.Visible = True
        Me.FTWHAssetCode.VisibleIndex = 2
        '
        'FNQuantityOUT
        '
        Me.FNQuantityOUT.Caption = "FNQuantityOUT"
        Me.FNQuantityOUT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNQuantityOUT.FieldName = "FNQuantityOUT"
        Me.FNQuantityOUT.Name = "FNQuantityOUT"
        Me.FNQuantityOUT.OptionsColumn.AllowEdit = False
        Me.FNQuantityOUT.OptionsColumn.ReadOnly = True
        Me.FNQuantityOUT.Visible = True
        Me.FNQuantityOUT.VisibleIndex = 4
        '
        'wFormListBarcodeTransaction
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(738, 668)
        Me.Controls.Add(Me.ogcdetail)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "wFormListBarcodeTransaction"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "wFormListBarcodeTransaction"
        CType(Me.ogcdetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvdetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogcdetail As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvdetail As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents FTBarcodeNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTDocumentNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNQuantityIN As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTWHAssetCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNQuantityOUT As DevExpress.XtraGrid.Columns.GridColumn
End Class

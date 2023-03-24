<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wStockBalanceMonthlyAccLIstDetail
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
        Me.FTDocType = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTYear = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTMonth = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNAmount = New DevExpress.XtraGrid.Columns.GridColumn()
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
        Me.ogcdetail.Size = New System.Drawing.Size(590, 530)
        Me.ogcdetail.TabIndex = 4
        Me.ogcdetail.TabStop = False
        Me.ogcdetail.Tag = "2|"
        Me.ogcdetail.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvdetail})
        '
        'ogvdetail
        '
        Me.ogvdetail.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.FTDocType, Me.FTYear, Me.FTMonth, Me.FNQuantity, Me.FNAmount})
        Me.ogvdetail.GridControl = Me.ogcdetail
        Me.ogvdetail.Name = "ogvdetail"
        Me.ogvdetail.OptionsCustomization.AllowGroup = False
        Me.ogvdetail.OptionsCustomization.AllowQuickHideColumns = False
        Me.ogvdetail.OptionsView.ColumnAutoWidth = False
        Me.ogvdetail.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.ogvdetail.OptionsView.ShowFooter = True
        Me.ogvdetail.OptionsView.ShowGroupPanel = False
        Me.ogvdetail.Tag = "2|"
        '
        'FTDocType
        '
        Me.FTDocType.Caption = "FTDocType"
        Me.FTDocType.FieldName = "FTDocType"
        Me.FTDocType.Name = "FTDocType"
        Me.FTDocType.OptionsColumn.AllowEdit = False
        Me.FTDocType.OptionsColumn.AllowShowHide = False
        Me.FTDocType.OptionsColumn.AllowSize = False
        Me.FTDocType.OptionsColumn.ReadOnly = True
        Me.FTDocType.Visible = True
        Me.FTDocType.VisibleIndex = 0
        Me.FTDocType.Width = 109
        '
        'FTYear
        '
        Me.FTYear.Caption = "FTYear"
        Me.FTYear.FieldName = "FTYear"
        Me.FTYear.Name = "FTYear"
        Me.FTYear.OptionsColumn.AllowEdit = False
        Me.FTYear.OptionsColumn.AllowShowHide = False
        Me.FTYear.OptionsColumn.AllowSize = False
        Me.FTYear.OptionsColumn.ReadOnly = True
        Me.FTYear.Visible = True
        Me.FTYear.VisibleIndex = 1
        Me.FTYear.Width = 104
        '
        'FTMonth
        '
        Me.FTMonth.Caption = "FTMonth"
        Me.FTMonth.FieldName = "FTMonth"
        Me.FTMonth.Name = "FTMonth"
        Me.FTMonth.OptionsColumn.AllowEdit = False
        Me.FTMonth.OptionsColumn.AllowShowHide = False
        Me.FTMonth.OptionsColumn.AllowSize = False
        Me.FTMonth.OptionsColumn.ReadOnly = True
        Me.FTMonth.Visible = True
        Me.FTMonth.VisibleIndex = 2
        Me.FTMonth.Width = 114
        '
        'FNQuantity
        '
        Me.FNQuantity.Caption = "FNQuantity"
        Me.FNQuantity.DisplayFormat.FormatString = "{0:n4}"
        Me.FNQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNQuantity.FieldName = "FNQuantity"
        Me.FNQuantity.Name = "FNQuantity"
        Me.FNQuantity.OptionsColumn.AllowEdit = False
        Me.FNQuantity.OptionsColumn.AllowShowHide = False
        Me.FNQuantity.OptionsColumn.AllowSize = False
        Me.FNQuantity.OptionsColumn.ReadOnly = True
        '
        'FNAmount
        '
        Me.FNAmount.Caption = "FNAmount"
        Me.FNAmount.DisplayFormat.FormatString = "{0:n2}"
        Me.FNAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNAmount.FieldName = "FNAmount"
        Me.FNAmount.Name = "FNAmount"
        Me.FNAmount.OptionsColumn.AllowEdit = False
        Me.FNAmount.OptionsColumn.AllowShowHide = False
        Me.FNAmount.OptionsColumn.AllowSize = False
        Me.FNAmount.OptionsColumn.ReadOnly = True
        Me.FNAmount.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FNAmount", "{0:n2}")})
        Me.FNAmount.Visible = True
        Me.FNAmount.VisibleIndex = 3
        Me.FNAmount.Width = 120
        '
        'wStockBalanceMonthlyAccLIstDetail
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(590, 530)
        Me.Controls.Add(Me.ogcdetail)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "wStockBalanceMonthlyAccLIstDetail"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "List Detail Balance"
        CType(Me.ogcdetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvdetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogcdetail As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvdetail As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents FTDocType As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTYear As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTMonth As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNAmount As DevExpress.XtraGrid.Columns.GridColumn
End Class

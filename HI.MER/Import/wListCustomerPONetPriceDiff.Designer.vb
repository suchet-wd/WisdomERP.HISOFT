<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wListCustomerPONetPriceDiff
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
        Me.ogbFactoryOrderCopy = New DevExpress.XtraEditors.GroupControl()
        Me.ogddetail = New DevExpress.XtraGrid.GridControl()
        Me.ogvdetail = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.CFTCustomerPO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTPOItem = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTColorwayCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTSizeBreakdownCode = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.ogbFactoryOrderCopy, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbFactoryOrderCopy.SuspendLayout()
        CType(Me.ogddetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvdetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ogbFactoryOrderCopy
        '
        Me.ogbFactoryOrderCopy.Controls.Add(Me.ogddetail)
        Me.ogbFactoryOrderCopy.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogbFactoryOrderCopy.Location = New System.Drawing.Point(0, 0)
        Me.ogbFactoryOrderCopy.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbFactoryOrderCopy.Name = "ogbFactoryOrderCopy"
        Me.ogbFactoryOrderCopy.Size = New System.Drawing.Size(615, 525)
        Me.ogbFactoryOrderCopy.TabIndex = 0
        Me.ogbFactoryOrderCopy.Text = "List Customer PO Net Price Exported XML"
        '
        'ogddetail
        '
        Me.ogddetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogddetail.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogddetail.Location = New System.Drawing.Point(2, 25)
        Me.ogddetail.MainView = Me.ogvdetail
        Me.ogddetail.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogddetail.Name = "ogddetail"
        Me.ogddetail.Size = New System.Drawing.Size(611, 498)
        Me.ogddetail.TabIndex = 0
        Me.ogddetail.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvdetail})
        '
        'ogvdetail
        '
        Me.ogvdetail.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.CFTCustomerPO, Me.FTPOItem, Me.FTColorwayCode, Me.FTSizeBreakdownCode})
        Me.ogvdetail.GridControl = Me.ogddetail
        Me.ogvdetail.Name = "ogvdetail"
        Me.ogvdetail.OptionsView.ColumnAutoWidth = False
        Me.ogvdetail.OptionsView.EnableAppearanceEvenRow = True
        Me.ogvdetail.OptionsView.ShowGroupPanel = False
        '
        'CFTCustomerPO
        '
        Me.CFTCustomerPO.Caption = "Customer PO"
        Me.CFTCustomerPO.FieldName = "FTCustomerPO"
        Me.CFTCustomerPO.Name = "CFTCustomerPO"
        Me.CFTCustomerPO.OptionsColumn.AllowEdit = False
        Me.CFTCustomerPO.OptionsColumn.ReadOnly = True
        Me.CFTCustomerPO.Visible = True
        Me.CFTCustomerPO.VisibleIndex = 0
        Me.CFTCustomerPO.Width = 172
        '
        'FTPOItem
        '
        Me.FTPOItem.Caption = "PO Line Item"
        Me.FTPOItem.FieldName = "FTPOItem"
        Me.FTPOItem.Name = "FTPOItem"
        Me.FTPOItem.OptionsColumn.AllowEdit = False
        Me.FTPOItem.OptionsColumn.ReadOnly = True
        Me.FTPOItem.Visible = True
        Me.FTPOItem.VisibleIndex = 1
        Me.FTPOItem.Width = 101
        '
        'FTColorwayCode
        '
        Me.FTColorwayCode.Caption = "Color way"
        Me.FTColorwayCode.FieldName = "FTColorwayCode"
        Me.FTColorwayCode.Name = "FTColorwayCode"
        Me.FTColorwayCode.OptionsColumn.AllowEdit = False
        Me.FTColorwayCode.OptionsColumn.ReadOnly = True
        Me.FTColorwayCode.Visible = True
        Me.FTColorwayCode.VisibleIndex = 2
        Me.FTColorwayCode.Width = 111
        '
        'FTSizeBreakdownCode
        '
        Me.FTSizeBreakdownCode.Caption = "FTSizeBreakdownCode"
        Me.FTSizeBreakdownCode.FieldName = "FTSizeBreakdownCode"
        Me.FTSizeBreakdownCode.Name = "FTSizeBreakdownCode"
        Me.FTSizeBreakdownCode.OptionsColumn.AllowEdit = False
        Me.FTSizeBreakdownCode.OptionsColumn.ReadOnly = True
        Me.FTSizeBreakdownCode.Visible = True
        Me.FTSizeBreakdownCode.VisibleIndex = 3
        Me.FTSizeBreakdownCode.Width = 101
        '
        'wListCustomerPONetPriceDiff
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(615, 525)
        Me.Controls.Add(Me.ogbFactoryOrderCopy)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "wListCustomerPONetPriceDiff"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "List Customer PO Net Price Exported XML"
        CType(Me.ogbFactoryOrderCopy, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbFactoryOrderCopy.ResumeLayout(False)
        CType(Me.ogddetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvdetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogbFactoryOrderCopy As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ogddetail As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvdetail As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents CFTCustomerPO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTPOItem As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTColorwayCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTSizeBreakdownCode As DevExpress.XtraGrid.Columns.GridColumn
End Class

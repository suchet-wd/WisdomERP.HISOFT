<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wListCheckExportXML
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
        Me.ogdlist = New DevExpress.XtraGrid.GridControl()
        Me.ogvlist = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.oColFTOrderNoCopy = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.oColFTOrderNoSubCopy = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cCustomerPO = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.ogbFactoryOrderCopy, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbFactoryOrderCopy.SuspendLayout()
        CType(Me.ogdlist, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvlist, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ogbFactoryOrderCopy
        '
        Me.ogbFactoryOrderCopy.Controls.Add(Me.ogdlist)
        Me.ogbFactoryOrderCopy.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogbFactoryOrderCopy.Location = New System.Drawing.Point(0, 0)
        Me.ogbFactoryOrderCopy.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbFactoryOrderCopy.Name = "ogbFactoryOrderCopy"
        Me.ogbFactoryOrderCopy.Size = New System.Drawing.Size(509, 466)
        Me.ogbFactoryOrderCopy.TabIndex = 0
        Me.ogbFactoryOrderCopy.Text = "List Factory Order No. Copy"
        '
        'ogdlist
        '
        Me.ogdlist.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogdlist.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogdlist.Location = New System.Drawing.Point(2, 25)
        Me.ogdlist.MainView = Me.ogvlist
        Me.ogdlist.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogdlist.Name = "ogdlist"
        Me.ogdlist.Size = New System.Drawing.Size(505, 439)
        Me.ogdlist.TabIndex = 0
        Me.ogdlist.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvlist})
        '
        'ogvlist
        '
        Me.ogvlist.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.oColFTOrderNoCopy, Me.oColFTOrderNoSubCopy, Me.cCustomerPO})
        Me.ogvlist.GridControl = Me.ogdlist
        Me.ogvlist.Name = "ogvlist"
        Me.ogvlist.OptionsView.ColumnAutoWidth = False
        Me.ogvlist.OptionsView.EnableAppearanceEvenRow = True
        Me.ogvlist.OptionsView.ShowGroupPanel = False
        '
        'oColFTOrderNoCopy
        '
        Me.oColFTOrderNoCopy.AppearanceHeader.Options.UseTextOptions = True
        Me.oColFTOrderNoCopy.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.oColFTOrderNoCopy.Caption = "Order No."
        Me.oColFTOrderNoCopy.FieldName = "FTOrderNo"
        Me.oColFTOrderNoCopy.Name = "oColFTOrderNoCopy"
        Me.oColFTOrderNoCopy.OptionsColumn.AllowEdit = False
        Me.oColFTOrderNoCopy.OptionsColumn.ReadOnly = True
        Me.oColFTOrderNoCopy.Visible = True
        Me.oColFTOrderNoCopy.VisibleIndex = 0
        Me.oColFTOrderNoCopy.Width = 163
        '
        'oColFTOrderNoSubCopy
        '
        Me.oColFTOrderNoSubCopy.AppearanceHeader.Options.UseTextOptions = True
        Me.oColFTOrderNoSubCopy.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.oColFTOrderNoSubCopy.Caption = "Sub Order No."
        Me.oColFTOrderNoSubCopy.FieldName = "FTSubOrderNo"
        Me.oColFTOrderNoSubCopy.Name = "oColFTOrderNoSubCopy"
        Me.oColFTOrderNoSubCopy.OptionsColumn.AllowEdit = False
        Me.oColFTOrderNoSubCopy.OptionsColumn.ReadOnly = True
        Me.oColFTOrderNoSubCopy.Visible = True
        Me.oColFTOrderNoSubCopy.VisibleIndex = 1
        Me.oColFTOrderNoSubCopy.Width = 160
        '
        'cCustomerPO
        '
        Me.cCustomerPO.Caption = "Customer PO"
        Me.cCustomerPO.FieldName = "FTCustomerPO"
        Me.cCustomerPO.Name = "cCustomerPO"
        Me.cCustomerPO.OptionsColumn.AllowEdit = False
        Me.cCustomerPO.OptionsColumn.ReadOnly = True
        Me.cCustomerPO.Visible = True
        Me.cCustomerPO.VisibleIndex = 2
        Me.cCustomerPO.Width = 122
        '
        'wListCheckExportXML
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(509, 466)
        Me.Controls.Add(Me.ogbFactoryOrderCopy)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "wListCheckExportXML"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "List Export XML File"
        CType(Me.ogbFactoryOrderCopy, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbFactoryOrderCopy.ResumeLayout(False)
        CType(Me.ogdlist, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvlist, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogbFactoryOrderCopy As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ogdlist As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvlist As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents oColFTOrderNoCopy As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents oColFTOrderNoSubCopy As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cCustomerPO As DevExpress.XtraGrid.Columns.GridColumn
End Class

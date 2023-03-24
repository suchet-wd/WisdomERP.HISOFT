<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wListCompleteCopyOrder
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
        Me.ogdOrderCopy = New DevExpress.XtraGrid.GridControl()
        Me.ogvCopyOrder = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.oColFTOrderNoCopy = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.oColFTOrderNoSubCopy = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.ogbFactoryOrderCopy, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbFactoryOrderCopy.SuspendLayout()
        CType(Me.ogdOrderCopy, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvCopyOrder, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ogbFactoryOrderCopy
        '
        Me.ogbFactoryOrderCopy.Controls.Add(Me.ogdOrderCopy)
        Me.ogbFactoryOrderCopy.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogbFactoryOrderCopy.Location = New System.Drawing.Point(0, 0)
        Me.ogbFactoryOrderCopy.Name = "ogbFactoryOrderCopy"
        Me.ogbFactoryOrderCopy.Size = New System.Drawing.Size(608, 280)
        Me.ogbFactoryOrderCopy.TabIndex = 0
        Me.ogbFactoryOrderCopy.Text = "List Factory Order No. Copy"
        '
        'ogdOrderCopy
        '
        Me.ogdOrderCopy.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogdOrderCopy.Location = New System.Drawing.Point(2, 21)
        Me.ogdOrderCopy.MainView = Me.ogvCopyOrder
        Me.ogdOrderCopy.Name = "ogdOrderCopy"
        Me.ogdOrderCopy.Size = New System.Drawing.Size(604, 257)
        Me.ogdOrderCopy.TabIndex = 0
        Me.ogdOrderCopy.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvCopyOrder})
        '
        'ogvCopyOrder
        '
        Me.ogvCopyOrder.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.oColFTOrderNoCopy, Me.oColFTOrderNoSubCopy})
        Me.ogvCopyOrder.GridControl = Me.ogdOrderCopy
        Me.ogvCopyOrder.Name = "ogvCopyOrder"
        Me.ogvCopyOrder.OptionsView.EnableAppearanceEvenRow = True
        Me.ogvCopyOrder.OptionsView.ShowGroupPanel = False
        '
        'oColFTOrderNoCopy
        '
        Me.oColFTOrderNoCopy.AppearanceHeader.Options.UseTextOptions = True
        Me.oColFTOrderNoCopy.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.oColFTOrderNoCopy.Caption = "Order No."
        Me.oColFTOrderNoCopy.FieldName = "FTOrderNoCopy"
        Me.oColFTOrderNoCopy.Name = "oColFTOrderNoCopy"
        Me.oColFTOrderNoCopy.OptionsColumn.AllowEdit = False
        Me.oColFTOrderNoCopy.OptionsColumn.AllowMove = False
        Me.oColFTOrderNoCopy.OptionsColumn.AllowSize = False
        Me.oColFTOrderNoCopy.OptionsColumn.ReadOnly = True
        Me.oColFTOrderNoCopy.Visible = True
        Me.oColFTOrderNoCopy.VisibleIndex = 0
        '
        'oColFTOrderNoSubCopy
        '
        Me.oColFTOrderNoSubCopy.AppearanceHeader.Options.UseTextOptions = True
        Me.oColFTOrderNoSubCopy.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.oColFTOrderNoSubCopy.Caption = "Sub Order No."
        Me.oColFTOrderNoSubCopy.FieldName = "FTOrderNoSubCopy"
        Me.oColFTOrderNoSubCopy.Name = "oColFTOrderNoSubCopy"
        Me.oColFTOrderNoSubCopy.OptionsColumn.AllowEdit = False
        Me.oColFTOrderNoSubCopy.OptionsColumn.AllowMove = False
        Me.oColFTOrderNoSubCopy.OptionsColumn.AllowSize = False
        Me.oColFTOrderNoSubCopy.OptionsColumn.ReadOnly = True
        Me.oColFTOrderNoSubCopy.Visible = True
        Me.oColFTOrderNoSubCopy.VisibleIndex = 1
        '
        'wListCompleteCopyOrder
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(608, 280)
        Me.Controls.Add(Me.ogbFactoryOrderCopy)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "wListCompleteCopyOrder"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "List Copy Factory Order No.  Complete"
        CType(Me.ogbFactoryOrderCopy, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbFactoryOrderCopy.ResumeLayout(False)
        CType(Me.ogdOrderCopy, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvCopyOrder, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogbFactoryOrderCopy As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ogdOrderCopy As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvCopyOrder As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents oColFTOrderNoCopy As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents oColFTOrderNoSubCopy As DevExpress.XtraGrid.Columns.GridColumn
End Class

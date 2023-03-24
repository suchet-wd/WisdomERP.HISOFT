<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wListCompleteCopySubOrder
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
        Me.ogbListCopySubOrderNo = New DevExpress.XtraEditors.GroupControl()
        Me.ogdOrderSubCopy = New DevExpress.XtraGrid.GridControl()
        Me.ogvCopyOrderSub = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.oColFTOrderNoCopySub = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.oColFTOrderNoSubCopySub = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.ogbListCopySubOrderNo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbListCopySubOrderNo.SuspendLayout()
        CType(Me.ogdOrderSubCopy, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvCopyOrderSub, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ogbListCopySubOrderNo
        '
        Me.ogbListCopySubOrderNo.Controls.Add(Me.ogdOrderSubCopy)
        Me.ogbListCopySubOrderNo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogbListCopySubOrderNo.Location = New System.Drawing.Point(0, 0)
        Me.ogbListCopySubOrderNo.Name = "ogbListCopySubOrderNo"
        Me.ogbListCopySubOrderNo.Size = New System.Drawing.Size(608, 280)
        Me.ogbListCopySubOrderNo.TabIndex = 0
        Me.ogbListCopySubOrderNo.Text = "List Factory Sub Order No. Copy"
        '
        'ogdOrderSubCopy
        '
        Me.ogdOrderSubCopy.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogdOrderSubCopy.Location = New System.Drawing.Point(2, 21)
        Me.ogdOrderSubCopy.MainView = Me.ogvCopyOrderSub
        Me.ogdOrderSubCopy.Name = "ogdOrderSubCopy"
        Me.ogdOrderSubCopy.Size = New System.Drawing.Size(604, 257)
        Me.ogdOrderSubCopy.TabIndex = 0
        Me.ogdOrderSubCopy.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvCopyOrderSub})
        '
        'ogvCopyOrderSub
        '
        Me.ogvCopyOrderSub.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.oColFTOrderNoCopySub, Me.oColFTOrderNoSubCopySub})
        Me.ogvCopyOrderSub.GridControl = Me.ogdOrderSubCopy
        Me.ogvCopyOrderSub.Name = "ogvCopyOrderSub"
        Me.ogvCopyOrderSub.OptionsView.EnableAppearanceEvenRow = True
        Me.ogvCopyOrderSub.OptionsView.ShowGroupPanel = False
        '
        'oColFTOrderNoCopySub
        '
        Me.oColFTOrderNoCopySub.Caption = "FTOrderNo"
        Me.oColFTOrderNoCopySub.FieldName = "FTOrderNo"
        Me.oColFTOrderNoCopySub.Name = "oColFTOrderNoCopySub"
        Me.oColFTOrderNoCopySub.OptionsColumn.AllowEdit = False
        Me.oColFTOrderNoCopySub.OptionsColumn.AllowMove = False
        Me.oColFTOrderNoCopySub.OptionsColumn.AllowSize = False
        Me.oColFTOrderNoCopySub.OptionsColumn.ReadOnly = True
        Me.oColFTOrderNoCopySub.Visible = True
        Me.oColFTOrderNoCopySub.VisibleIndex = 0
        '
        'oColFTOrderNoSubCopySub
        '
        Me.oColFTOrderNoSubCopySub.Caption = "FTOrderNoSub"
        Me.oColFTOrderNoSubCopySub.FieldName = "FTOrderNoSub"
        Me.oColFTOrderNoSubCopySub.Name = "oColFTOrderNoSubCopySub"
        Me.oColFTOrderNoSubCopySub.OptionsColumn.AllowEdit = False
        Me.oColFTOrderNoSubCopySub.OptionsColumn.AllowMove = False
        Me.oColFTOrderNoSubCopySub.OptionsColumn.AllowSize = False
        Me.oColFTOrderNoSubCopySub.OptionsColumn.ReadOnly = True
        Me.oColFTOrderNoSubCopySub.Visible = True
        Me.oColFTOrderNoSubCopySub.VisibleIndex = 1
        '
        'wListCompleteCopySubOrder
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(608, 280)
        Me.Controls.Add(Me.ogbListCopySubOrderNo)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "wListCompleteCopySubOrder"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "wListCompleteCopySubOrder"
        CType(Me.ogbListCopySubOrderNo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbListCopySubOrderNo.ResumeLayout(False)
        CType(Me.ogdOrderSubCopy, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvCopyOrderSub, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogbListCopySubOrderNo As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ogdOrderSubCopy As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvCopyOrderSub As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents oColFTOrderNoCopySub As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents oColFTOrderNoSubCopySub As DevExpress.XtraGrid.Columns.GridColumn
End Class

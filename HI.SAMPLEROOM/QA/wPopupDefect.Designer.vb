<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wPopupDefect
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
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.ogcsubDetail = New DevExpress.XtraGrid.GridControl()
        Me.ogvsubDetail = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.cSeq = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTQADetailName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNQty = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.oClose = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.ogcsubDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvsubDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PanelControl1
        '
        Me.PanelControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PanelControl1.Controls.Add(Me.ogcsubDetail)
        Me.PanelControl1.Location = New System.Drawing.Point(0, 23)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(350, 197)
        Me.PanelControl1.TabIndex = 0
        '
        'ogcsubDetail
        '
        Me.ogcsubDetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogcsubDetail.Location = New System.Drawing.Point(2, 2)
        Me.ogcsubDetail.MainView = Me.ogvsubDetail
        Me.ogcsubDetail.Name = "ogcsubDetail"
        Me.ogcsubDetail.Size = New System.Drawing.Size(346, 193)
        Me.ogcsubDetail.TabIndex = 0
        Me.ogcsubDetail.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvsubDetail})
        '
        'ogvsubDetail
        '
        Me.ogvsubDetail.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.cSeq, Me.cFTQADetailName, Me.cFNQty})
        Me.ogvsubDetail.GridControl = Me.ogcsubDetail
        Me.ogvsubDetail.Name = "ogvsubDetail"
        Me.ogvsubDetail.OptionsView.ShowGroupPanel = False
        '
        'cSeq
        '
        Me.cSeq.Caption = "Seq"
        Me.cSeq.FieldName = "Row"
        Me.cSeq.Name = "cSeq"
        Me.cSeq.Visible = True
        Me.cSeq.VisibleIndex = 0
        Me.cSeq.Width = 54
        '
        'cFTQADetailName
        '
        Me.cFTQADetailName.Caption = "FTQADetailName"
        Me.cFTQADetailName.FieldName = "FTQADetailName"
        Me.cFTQADetailName.Name = "cFTQADetailName"
        Me.cFTQADetailName.Visible = True
        Me.cFTQADetailName.VisibleIndex = 1
        Me.cFTQADetailName.Width = 161
        '
        'cFNQty
        '
        Me.cFNQty.Caption = "Qty"
        Me.cFNQty.FieldName = "Qty"
        Me.cFNQty.Name = "cFNQty"
        Me.cFNQty.Visible = True
        Me.cFNQty.VisibleIndex = 2
        Me.cFNQty.Width = 113
        '
        'oClose
        '
        Me.oClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.oClose.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat

        Me.oClose.Location = New System.Drawing.Point(323, 1)
        Me.oClose.Name = "oClose"
        Me.oClose.Size = New System.Drawing.Size(24, 23)
        Me.oClose.TabIndex = 1
        '
        'wPopupDefect
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(350, 221)
        Me.Controls.Add(Me.PanelControl1)
        Me.Controls.Add(Me.oClose)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "wPopupDefect"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "wPopupDefect"
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        CType(Me.ogcsubDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvsubDetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents oClose As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ogcsubDetail As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvsubDetail As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents cSeq As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTQADetailName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNQty As DevExpress.XtraGrid.Columns.GridColumn
End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wPOSSalePopup
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
        Me.ogcDetail = New DevExpress.XtraGrid.GridControl()
        Me.ogvDetail = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.cFTProdTypeName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTColorway = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTSizeBreakDown = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.ogcDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ogcDetail
        '
        Me.ogcDetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogcDetail.Location = New System.Drawing.Point(0, 0)
        Me.ogcDetail.MainView = Me.ogvDetail
        Me.ogcDetail.Name = "ogcDetail"
        Me.ogcDetail.Size = New System.Drawing.Size(476, 237)
        Me.ogcDetail.TabIndex = 0
        Me.ogcDetail.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvDetail})
        '
        'ogvDetail
        '
        Me.ogvDetail.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.cFTProdTypeName, Me.cFTColorway, Me.cFTSizeBreakDown})
        Me.ogvDetail.GridControl = Me.ogcDetail
        Me.ogvDetail.Name = "ogvDetail"
        Me.ogvDetail.OptionsView.ShowGroupPanel = False
        '
        'cFTProdTypeName
        '
        Me.cFTProdTypeName.Caption = "FTProdTypeName"
        Me.cFTProdTypeName.FieldName = "FTProdTypeName"
        Me.cFTProdTypeName.Name = "cFTProdTypeName"
        Me.cFTProdTypeName.OptionsColumn.AllowEdit = False
        Me.cFTProdTypeName.Visible = True
        Me.cFTProdTypeName.VisibleIndex = 0
        Me.cFTProdTypeName.Width = 543
        '
        'cFTColorway
        '
        Me.cFTColorway.Caption = "FTColorway"
        Me.cFTColorway.FieldName = "FTColorway"
        Me.cFTColorway.Name = "cFTColorway"
        Me.cFTColorway.OptionsColumn.AllowEdit = False
        Me.cFTColorway.Visible = True
        Me.cFTColorway.VisibleIndex = 1
        Me.cFTColorway.Width = 384
        '
        'cFTSizeBreakDown
        '
        Me.cFTSizeBreakDown.Caption = "FTSizeBreakDown"
        Me.cFTSizeBreakDown.FieldName = "FTSizeBreakDown"
        Me.cFTSizeBreakDown.Name = "cFTSizeBreakDown"
        Me.cFTSizeBreakDown.OptionsColumn.AllowEdit = False
        Me.cFTSizeBreakDown.Visible = True
        Me.cFTSizeBreakDown.VisibleIndex = 2
        Me.cFTSizeBreakDown.Width = 385
        '
        'wSalePopup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(476, 237)
        Me.Controls.Add(Me.ogcDetail)
        Me.Name = "wPOSSalePopup"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "wPOSSalePopup"
        CType(Me.ogcDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvDetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogcDetail As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvDetail As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents cFTProdTypeName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTColorway As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTSizeBreakDown As DevExpress.XtraGrid.Columns.GridColumn
End Class

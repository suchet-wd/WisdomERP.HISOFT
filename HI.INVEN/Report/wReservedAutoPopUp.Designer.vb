<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wReservedAutoPopUp
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
        Me.cFTSelect = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryFTSelect = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.cFTDocumentNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTWHCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNHSysWHId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFDRSVDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.oClose = New DevExpress.XtraEditors.SimpleButton()
        Me.oSend = New DevExpress.XtraEditors.SimpleButton()
        Me.oFTSelect = New DevExpress.XtraEditors.CheckEdit()
        CType(Me.ogcdetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvdetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryFTSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.oFTSelect.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ogcdetail
        '
        Me.ogcdetail.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogcdetail.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcdetail.Location = New System.Drawing.Point(1, 33)
        Me.ogcdetail.MainView = Me.ogvdetail
        Me.ogcdetail.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcdetail.Name = "ogcdetail"
        Me.ogcdetail.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryFTSelect})
        Me.ogcdetail.Size = New System.Drawing.Size(528, 458)
        Me.ogcdetail.TabIndex = 1
        Me.ogcdetail.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvdetail})
        '
        'ogvdetail
        '
        Me.ogvdetail.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.cFTSelect, Me.cFTDocumentNo, Me.cFTWHCode, Me.cFNHSysWHId, Me.cFDRSVDate})
        Me.ogvdetail.GridControl = Me.ogcdetail
        Me.ogvdetail.Name = "ogvdetail"
        Me.ogvdetail.OptionsView.RowAutoHeight = True
        Me.ogvdetail.OptionsView.ShowGroupPanel = False
        '
        'cFTSelect
        '
        Me.cFTSelect.Caption = "FTSelect"
        Me.cFTSelect.ColumnEdit = Me.RepositoryFTSelect
        Me.cFTSelect.FieldName = "FTSelect"
        Me.cFTSelect.Name = "cFTSelect"
        Me.cFTSelect.Visible = True
        Me.cFTSelect.VisibleIndex = 0
        Me.cFTSelect.Width = 93
        '
        'RepositoryFTSelect
        '
        Me.RepositoryFTSelect.AutoHeight = False
        Me.RepositoryFTSelect.Caption = "Check"
        Me.RepositoryFTSelect.Name = "RepositoryFTSelect"
        Me.RepositoryFTSelect.ValueChecked = "1"
        Me.RepositoryFTSelect.ValueUnchecked = "0"
        '
        'cFTDocumentNo
        '
        Me.cFTDocumentNo.Caption = "FTDocumentNo"
        Me.cFTDocumentNo.FieldName = "FTDocumentNo"
        Me.cFTDocumentNo.Name = "cFTDocumentNo"
        Me.cFTDocumentNo.OptionsColumn.AllowEdit = False
        Me.cFTDocumentNo.Visible = True
        Me.cFTDocumentNo.VisibleIndex = 1
        Me.cFTDocumentNo.Width = 481
        '
        'cFTWHCode
        '
        Me.cFTWHCode.Caption = "FTWHCode"
        Me.cFTWHCode.FieldName = "FTWHCode"
        Me.cFTWHCode.Name = "cFTWHCode"
        Me.cFTWHCode.OptionsColumn.AllowEdit = False
        Me.cFTWHCode.Visible = True
        Me.cFTWHCode.VisibleIndex = 3
        Me.cFTWHCode.Width = 373
        '
        'cFNHSysWHId
        '
        Me.cFNHSysWHId.Caption = "FNHSysWHId"
        Me.cFNHSysWHId.FieldName = "FNHSysWHId"
        Me.cFNHSysWHId.Name = "cFNHSysWHId"
        '
        'cFDRSVDate
        '
        Me.cFDRSVDate.Caption = "FDRSVDate"
        Me.cFDRSVDate.FieldName = "FDRSVDate"
        Me.cFDRSVDate.Name = "cFDRSVDate"
        Me.cFDRSVDate.OptionsColumn.AllowEdit = False
        Me.cFDRSVDate.Visible = True
        Me.cFDRSVDate.VisibleIndex = 2
        Me.cFDRSVDate.Width = 362
        '
        'oClose
        '
        Me.oClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.oClose.Location = New System.Drawing.Point(439, 4)
        Me.oClose.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.oClose.Name = "oClose"
        Me.oClose.Size = New System.Drawing.Size(87, 28)
        Me.oClose.TabIndex = 2
        Me.oClose.Text = "Close"
        '
        'oSend
        '
        Me.oSend.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.oSend.Location = New System.Drawing.Point(344, 4)
        Me.oSend.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.oSend.Name = "oSend"
        Me.oSend.Size = New System.Drawing.Size(87, 28)
        Me.oSend.TabIndex = 3
        Me.oSend.Text = "Send"
        '
        'oFTSelect
        '
        Me.oFTSelect.Location = New System.Drawing.Point(37, 6)
        Me.oFTSelect.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.oFTSelect.Name = "oFTSelect"
        Me.oFTSelect.Properties.Caption = "Select All"
        Me.oFTSelect.Size = New System.Drawing.Size(233, 20)
        Me.oFTSelect.TabIndex = 4
        '
        'wReservedAutoPopUp
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(530, 494)
        Me.ControlBox = False
        Me.Controls.Add(Me.oFTSelect)
        Me.Controls.Add(Me.oClose)
        Me.Controls.Add(Me.oSend)
        Me.Controls.Add(Me.ogcdetail)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "wReservedAutoPopUp"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "wReservedAutoPopUp"
        CType(Me.ogcdetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvdetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryFTSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.oFTSelect.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogcdetail As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvdetail As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents cFTSelect As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTDocumentNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTWHCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNHSysWHId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents oClose As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents oSend As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents RepositoryFTSelect As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents oFTSelect As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents cFDRSVDate As DevExpress.XtraGrid.Columns.GridColumn
End Class

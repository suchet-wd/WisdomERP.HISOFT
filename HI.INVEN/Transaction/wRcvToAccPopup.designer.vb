<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wRcvToAccPopup
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
        Me.ogrpDetail = New DevExpress.XtraEditors.GroupControl()
        Me.ocmclose = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmSelect = New DevExpress.XtraEditors.SimpleButton()
        Me.oSelectAll = New DevExpress.XtraEditors.CheckEdit()
        Me.ogclistDoc = New DevExpress.XtraGrid.GridControl()
        Me.ogvlistDoc = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.cSelect = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositorySelect = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.cDocumentNO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFDDocumentDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTDocumentBy = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTPurchaseNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTPackNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryPackNo1 = New DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit()
        Me.RepositoryItemPopupContainerEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.FNDocState = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.ogrpDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogrpDetail.SuspendLayout()
        CType(Me.oSelectAll.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogclistDoc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvlistDoc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositorySelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryPackNo1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ogrpDetail
        '
        Me.ogrpDetail.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogrpDetail.Controls.Add(Me.ocmclose)
        Me.ogrpDetail.Controls.Add(Me.ocmSelect)
        Me.ogrpDetail.Controls.Add(Me.oSelectAll)
        Me.ogrpDetail.Controls.Add(Me.ogclistDoc)
        Me.ogrpDetail.Location = New System.Drawing.Point(2, 1)
        Me.ogrpDetail.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogrpDetail.Name = "ogrpDetail"
        Me.ogrpDetail.Size = New System.Drawing.Size(791, 505)
        Me.ogrpDetail.TabIndex = 0
        '
        'ocmclose
        '
        Me.ocmclose.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmclose.Location = New System.Drawing.Point(701, -1)
        Me.ocmclose.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmclose.Name = "ocmclose"
        Me.ocmclose.Size = New System.Drawing.Size(87, 28)
        Me.ocmclose.TabIndex = 3
        Me.ocmclose.Text = "Close"
        '
        'ocmSelect
        '
        Me.ocmSelect.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmSelect.Location = New System.Drawing.Point(607, -1)
        Me.ocmSelect.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmSelect.Name = "ocmSelect"
        Me.ocmSelect.Size = New System.Drawing.Size(87, 28)
        Me.ocmSelect.TabIndex = 2
        Me.ocmSelect.Text = "Select"
        '
        'oSelectAll
        '
        Me.oSelectAll.Location = New System.Drawing.Point(33, 1)
        Me.oSelectAll.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.oSelectAll.Name = "oSelectAll"
        Me.oSelectAll.Properties.Caption = "Select  All"
        Me.oSelectAll.Size = New System.Drawing.Size(181, 20)
        Me.oSelectAll.TabIndex = 1
        '
        'ogclistDoc
        '
        Me.ogclistDoc.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogclistDoc.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogclistDoc.Location = New System.Drawing.Point(2, 25)
        Me.ogclistDoc.MainView = Me.ogvlistDoc
        Me.ogclistDoc.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogclistDoc.Name = "ogclistDoc"
        Me.ogclistDoc.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositorySelect, Me.RepositoryPackNo1, Me.RepositoryItemPopupContainerEdit1})
        Me.ogclistDoc.Size = New System.Drawing.Size(787, 478)
        Me.ogclistDoc.TabIndex = 0
        Me.ogclistDoc.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvlistDoc})
        '
        'ogvlistDoc
        '
        Me.ogvlistDoc.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.cSelect, Me.cDocumentNO, Me.cFDDocumentDate, Me.cFTDocumentBy, Me.cFTPurchaseNo, Me.cFTPackNo, Me.FNDocState})
        Me.ogvlistDoc.GridControl = Me.ogclistDoc
        Me.ogvlistDoc.Name = "ogvlistDoc"
        '
        'cSelect
        '
        Me.cSelect.Caption = "Select"
        Me.cSelect.ColumnEdit = Me.RepositorySelect
        Me.cSelect.FieldName = "FTSelect"
        Me.cSelect.Name = "cSelect"
        Me.cSelect.Visible = True
        Me.cSelect.VisibleIndex = 0
        Me.cSelect.Width = 28
        '
        'RepositorySelect
        '
        Me.RepositorySelect.AutoHeight = False
        Me.RepositorySelect.Caption = "Check"
        Me.RepositorySelect.Name = "RepositorySelect"
        Me.RepositorySelect.ValueChecked = "1"
        Me.RepositorySelect.ValueUnchecked = "0"
        '
        'cDocumentNO
        '
        Me.cDocumentNO.Caption = "DocumentNO"
        Me.cDocumentNO.FieldName = "FTReceiveNo"
        Me.cDocumentNO.Name = "cDocumentNO"
        Me.cDocumentNO.OptionsColumn.AllowEdit = False
        Me.cDocumentNO.Visible = True
        Me.cDocumentNO.VisibleIndex = 1
        Me.cDocumentNO.Width = 152
        '
        'cFDDocumentDate
        '
        Me.cFDDocumentDate.Caption = "FDDocumentDate"
        Me.cFDDocumentDate.FieldName = "FDReceiveDate"
        Me.cFDDocumentDate.Name = "cFDDocumentDate"
        Me.cFDDocumentDate.OptionsColumn.AllowEdit = False
        Me.cFDDocumentDate.Visible = True
        Me.cFDDocumentDate.VisibleIndex = 2
        Me.cFDDocumentDate.Width = 76
        '
        'cFTDocumentBy
        '
        Me.cFTDocumentBy.Caption = "FTDocumentBy"
        Me.cFTDocumentBy.FieldName = "FTReceiveBy"
        Me.cFTDocumentBy.Name = "cFTDocumentBy"
        Me.cFTDocumentBy.OptionsColumn.AllowEdit = False
        Me.cFTDocumentBy.Visible = True
        Me.cFTDocumentBy.VisibleIndex = 3
        Me.cFTDocumentBy.Width = 82
        '
        'cFTPurchaseNo
        '
        Me.cFTPurchaseNo.Caption = "FTPurchaseNo"
        Me.cFTPurchaseNo.FieldName = "FTPurchaseNo"
        Me.cFTPurchaseNo.Name = "cFTPurchaseNo"
        Me.cFTPurchaseNo.OptionsColumn.AllowEdit = False
        Me.cFTPurchaseNo.Visible = True
        Me.cFTPurchaseNo.VisibleIndex = 4
        Me.cFTPurchaseNo.Width = 161
        '
        'cFTPackNo
        '
        Me.cFTPackNo.Caption = "FTPackNo"
        Me.cFTPackNo.FieldName = "FTPackNo"
        Me.cFTPackNo.Name = "cFTPackNo"
        Me.cFTPackNo.Visible = True
        Me.cFTPackNo.VisibleIndex = 5
        Me.cFTPackNo.Width = 136
        '
        'RepositoryPackNo1
        '
        Me.RepositoryPackNo1.Name = "RepositoryPackNo1"
        '
        'RepositoryItemPopupContainerEdit1
        '
        Me.RepositoryItemPopupContainerEdit1.AutoHeight = False
        Me.RepositoryItemPopupContainerEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit1.Name = "RepositoryItemPopupContainerEdit1"
        Me.RepositoryItemPopupContainerEdit1.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Simple
        Me.RepositoryItemPopupContainerEdit1.PopupFormMinSize = New System.Drawing.Size(20, 40)
        Me.RepositoryItemPopupContainerEdit1.PopupFormSize = New System.Drawing.Size(20, 40)
        '
        'FNDocState
        '
        Me.FNDocState.Caption = "FNDocState"
        Me.FNDocState.FieldName = "FNDocState"
        Me.FNDocState.Name = "FNDocState"
        '
        'wRcvToAccPopup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(794, 506)
        Me.ControlBox = False
        Me.Controls.Add(Me.ogrpDetail)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "wRcvToAccPopup"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "wRcvToAccPopup"
        CType(Me.ogrpDetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogrpDetail.ResumeLayout(False)
        CType(Me.oSelectAll.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogclistDoc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvlistDoc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositorySelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryPackNo1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogrpDetail As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ogclistDoc As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvlistDoc As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents cSelect As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositorySelect As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents cDocumentNO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFDDocumentDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTDocumentBy As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents oSelectAll As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents cFTPurchaseNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTPackNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryPackNo1 As DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit
    Friend WithEvents RepositoryItemPopupContainerEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents ocmclose As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmSelect As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FNDocState As DevExpress.XtraGrid.Columns.GridColumn
End Class

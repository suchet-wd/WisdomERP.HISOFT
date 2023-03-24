<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wTransferList
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
        Me.ogrpdetail = New DevExpress.XtraEditors.GroupControl()
        Me.ogcdetail = New DevExpress.XtraGrid.GridControl()
        Me.ogvdetail = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.cFTTransferFGNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTSelect = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryFTSelect = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.cFTBarcodeCarton = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTOrderNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTPORef = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTStyleCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTColorway = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTSizeBreakDown = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.obtclose = New DevExpress.XtraEditors.SimpleButton()
        Me.obtselect = New DevExpress.XtraEditors.SimpleButton()
        Me.ockSelectAll = New DevExpress.XtraEditors.CheckEdit()
        CType(Me.ogrpdetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogrpdetail.SuspendLayout()
        CType(Me.ogcdetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvdetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryFTSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.ockSelectAll.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ogrpdetail
        '
        Me.ogrpdetail.Controls.Add(Me.ockSelectAll)
        Me.ogrpdetail.Controls.Add(Me.ogcdetail)
        Me.ogrpdetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogrpdetail.Location = New System.Drawing.Point(0, 0)
        Me.ogrpdetail.Name = "ogrpdetail"
        Me.ogrpdetail.Size = New System.Drawing.Size(842, 438)
        Me.ogrpdetail.TabIndex = 0
        '
        'ogcdetail
        '
        Me.ogcdetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogcdetail.Location = New System.Drawing.Point(2, 22)
        Me.ogcdetail.MainView = Me.ogvdetail
        Me.ogcdetail.Name = "ogcdetail"
        Me.ogcdetail.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryFTSelect})
        Me.ogcdetail.Size = New System.Drawing.Size(838, 414)
        Me.ogcdetail.TabIndex = 0
        Me.ogcdetail.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvdetail})
        '
        'ogvdetail
        '
        Me.ogvdetail.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.cFTTransferFGNo, Me.cFTSelect, Me.cFTBarcodeCarton, Me.cFTOrderNo, Me.cFTPORef, Me.cFTStyleCode, Me.cFTColorway, Me.cFTSizeBreakDown, Me.cFNQuantity})
        Me.ogvdetail.GridControl = Me.ogcdetail
        Me.ogvdetail.Name = "ogvdetail"
        Me.ogvdetail.OptionsView.ShowGroupPanel = False
        '
        'cFTTransferFGNo
        '
        Me.cFTTransferFGNo.Caption = "FTTransferFGNo"
        Me.cFTTransferFGNo.FieldName = "FTTransferFGNo"
        Me.cFTTransferFGNo.Name = "cFTTransferFGNo"
        '
        'cFTSelect
        '
        Me.cFTSelect.Caption = "FTSelect"
        Me.cFTSelect.ColumnEdit = Me.RepositoryFTSelect
        Me.cFTSelect.FieldName = "FTSelect"
        Me.cFTSelect.Name = "cFTSelect"
        Me.cFTSelect.Visible = True
        Me.cFTSelect.VisibleIndex = 0
        Me.cFTSelect.Width = 74
        '
        'RepositoryFTSelect
        '
        Me.RepositoryFTSelect.AutoHeight = False
        Me.RepositoryFTSelect.Caption = "Check"
        Me.RepositoryFTSelect.Name = "RepositoryFTSelect"
        Me.RepositoryFTSelect.ValueChecked = "1"
        Me.RepositoryFTSelect.ValueUnchecked = "0"
        '
        'cFTBarcodeCarton
        '
        Me.cFTBarcodeCarton.Caption = "FTBarCodeCarton"
        Me.cFTBarcodeCarton.FieldName = "FTBarCodeCarton"
        Me.cFTBarcodeCarton.Name = "cFTBarcodeCarton"
        Me.cFTBarcodeCarton.OptionsColumn.AllowEdit = False
        Me.cFTBarcodeCarton.Visible = True
        Me.cFTBarcodeCarton.VisibleIndex = 1
        Me.cFTBarcodeCarton.Width = 136
        '
        'cFTOrderNo
        '
        Me.cFTOrderNo.Caption = "FTOrderNo"
        Me.cFTOrderNo.FieldName = "FTOrderNo"
        Me.cFTOrderNo.Name = "cFTOrderNo"
        Me.cFTOrderNo.OptionsColumn.AllowEdit = False
        Me.cFTOrderNo.Visible = True
        Me.cFTOrderNo.VisibleIndex = 2
        Me.cFTOrderNo.Width = 234
        '
        'cFTPORef
        '
        Me.cFTPORef.Caption = "FTPORef"
        Me.cFTPORef.FieldName = "FTPORef"
        Me.cFTPORef.Name = "cFTPORef"
        Me.cFTPORef.OptionsColumn.AllowEdit = False
        Me.cFTPORef.Visible = True
        Me.cFTPORef.VisibleIndex = 3
        Me.cFTPORef.Width = 212
        '
        'cFTStyleCode
        '
        Me.cFTStyleCode.Caption = "FTStyleCode"
        Me.cFTStyleCode.FieldName = "FTStyleCode"
        Me.cFTStyleCode.Name = "cFTStyleCode"
        Me.cFTStyleCode.OptionsColumn.AllowEdit = False
        Me.cFTStyleCode.Visible = True
        Me.cFTStyleCode.VisibleIndex = 4
        Me.cFTStyleCode.Width = 207
        '
        'cFTColorway
        '
        Me.cFTColorway.Caption = "FTColorway"
        Me.cFTColorway.FieldName = "FTColorway"
        Me.cFTColorway.Name = "cFTColorway"
        Me.cFTColorway.OptionsColumn.AllowEdit = False
        Me.cFTColorway.Visible = True
        Me.cFTColorway.VisibleIndex = 5
        Me.cFTColorway.Width = 165
        '
        'cFTSizeBreakDown
        '
        Me.cFTSizeBreakDown.Caption = "FTSizeBreakDown"
        Me.cFTSizeBreakDown.FieldName = "FTSizeBreakDown"
        Me.cFTSizeBreakDown.Name = "cFTSizeBreakDown"
        Me.cFTSizeBreakDown.OptionsColumn.AllowEdit = False
        Me.cFTSizeBreakDown.Visible = True
        Me.cFTSizeBreakDown.VisibleIndex = 6
        Me.cFTSizeBreakDown.Width = 160
        '
        'cFNQuantity
        '
        Me.cFNQuantity.Caption = "FNQuantity"
        Me.cFNQuantity.DisplayFormat.FormatString = "{0:n0}"
        Me.cFNQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cFNQuantity.FieldName = "FNQuantity"
        Me.cFNQuantity.Name = "cFNQuantity"
        Me.cFNQuantity.OptionsColumn.AllowEdit = False
        Me.cFNQuantity.Visible = True
        Me.cFNQuantity.VisibleIndex = 7
        Me.cFNQuantity.Width = 124
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Me.obtclose)
        Me.GroupControl1.Controls.Add(Me.obtselect)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GroupControl1.Location = New System.Drawing.Point(0, 438)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.ShowCaption = False
        Me.GroupControl1.Size = New System.Drawing.Size(842, 46)
        Me.GroupControl1.TabIndex = 1
        Me.GroupControl1.Text = "GroupControl1"
        '
        'obtclose
        '
        Me.obtclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.obtclose.Location = New System.Drawing.Point(716, 11)
        Me.obtclose.Name = "obtclose"
        Me.obtclose.Size = New System.Drawing.Size(99, 23)
        Me.obtclose.TabIndex = 1
        Me.obtclose.Text = "Close"
        '
        'obtselect
        '
        Me.obtselect.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.obtselect.Location = New System.Drawing.Point(608, 11)
        Me.obtselect.Name = "obtselect"
        Me.obtselect.Size = New System.Drawing.Size(102, 23)
        Me.obtselect.TabIndex = 0
        Me.obtselect.Text = "Select"
        '
        'ockSelectAll
        '
        Me.ockSelectAll.Location = New System.Drawing.Point(22, 1)
        Me.ockSelectAll.Name = "ockSelectAll"
        Me.ockSelectAll.Properties.Caption = "Select All"
        Me.ockSelectAll.Size = New System.Drawing.Size(130, 20)
        Me.ockSelectAll.TabIndex = 1
        Me.ockSelectAll.Tag = "2|"
        '
        'wTransferList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(842, 484)
        Me.Controls.Add(Me.ogrpdetail)
        Me.Controls.Add(Me.GroupControl1)
        Me.Name = "wTransferList"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "wTransferList"
        CType(Me.ogrpdetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogrpdetail.ResumeLayout(False)
        CType(Me.ogcdetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvdetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryFTSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        CType(Me.ockSelectAll.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogrpdetail As DevExpress.XtraEditors.GroupControl
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ogcdetail As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvdetail As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents cFTOrderNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTPORef As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTStyleCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTColorway As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTSizeBreakDown As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents obtclose As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents obtselect As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents cFTSelect As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryFTSelect As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents cFTBarcodeCarton As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTTransferFGNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ockSelectAll As DevExpress.XtraEditors.CheckEdit
End Class

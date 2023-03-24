<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wExpTruckSheetPopup
    Inherits DevExpress.XtraEditors.XtraForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.ocmcancel = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmsave = New DevExpress.XtraEditors.SimpleButton()
        Me.ogcref = New DevExpress.XtraGrid.GridControl()
        Me.ogvref = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.FTSelect = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemFTSelect = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.cFTInvoiceNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTPORef = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTStyleCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTNikePOLineItem = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNHSysStyleId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTColorway = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTSizeBreakDown = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNCTNS = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTCTNQtyBal = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNQuantityBal = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.oSelectAll = New DevExpress.XtraEditors.CheckEdit()
        Me.cFTBarCodeCarton = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.ogcref, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvref, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemFTSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.oSelectAll.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ocmcancel
        '
        Me.ocmcancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmcancel.Location = New System.Drawing.Point(978, 696)
        Me.ocmcancel.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmcancel.Name = "ocmcancel"
        Me.ocmcancel.Size = New System.Drawing.Size(169, 33)
        Me.ocmcancel.TabIndex = 533
        Me.ocmcancel.Text = "CANCEL"
        '
        'ocmsave
        '
        Me.ocmsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ocmsave.Location = New System.Drawing.Point(62, 696)
        Me.ocmsave.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmsave.Name = "ocmsave"
        Me.ocmsave.Size = New System.Drawing.Size(169, 33)
        Me.ocmsave.TabIndex = 532
        Me.ocmsave.Text = "Select"
        '
        'ogcref
        '
        Me.ogcref.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogcref.Location = New System.Drawing.Point(3, 40)
        Me.ogcref.MainView = Me.ogvref
        Me.ogcref.Name = "ogcref"
        Me.ogcref.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemFTSelect})
        Me.ogcref.Size = New System.Drawing.Size(1187, 649)
        Me.ogcref.TabIndex = 534
        Me.ogcref.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvref})
        '
        'ogvref
        '
        Me.ogvref.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.FTSelect, Me.cFTInvoiceNo, Me.FTPORef, Me.cFTStyleCode, Me.cFTNikePOLineItem, Me.FNHSysStyleId, Me.cFTColorway, Me.cFTSizeBreakDown, Me.cFNCTNS, Me.cFNQuantity, Me.cFTCTNQtyBal, Me.cFNQuantityBal, Me.cFTBarCodeCarton})
        Me.ogvref.GridControl = Me.ogcref
        Me.ogvref.Name = "ogvref"
        Me.ogvref.OptionsView.ColumnAutoWidth = False
        Me.ogvref.OptionsView.ShowGroupPanel = False
        '
        'FTSelect
        '
        Me.FTSelect.Caption = "FTSelect"
        Me.FTSelect.ColumnEdit = Me.RepositoryItemFTSelect
        Me.FTSelect.FieldName = "FTSelect"
        Me.FTSelect.Name = "FTSelect"
        Me.FTSelect.Visible = True
        Me.FTSelect.VisibleIndex = 0
        Me.FTSelect.Width = 59
        '
        'RepositoryItemFTSelect
        '
        Me.RepositoryItemFTSelect.AutoHeight = False
        Me.RepositoryItemFTSelect.Name = "RepositoryItemFTSelect"
        Me.RepositoryItemFTSelect.ValueChecked = "1"
        Me.RepositoryItemFTSelect.ValueUnchecked = "0"
        '
        'cFTInvoiceNo
        '
        Me.cFTInvoiceNo.Caption = "FTInvoiceNo"
        Me.cFTInvoiceNo.FieldName = "FTInvoiceNo"
        Me.cFTInvoiceNo.Name = "cFTInvoiceNo"
        Me.cFTInvoiceNo.OptionsColumn.AllowEdit = False
        Me.cFTInvoiceNo.OptionsColumn.AllowFocus = False
        Me.cFTInvoiceNo.Visible = True
        Me.cFTInvoiceNo.VisibleIndex = 1
        Me.cFTInvoiceNo.Width = 128
        '
        'FTPORef
        '
        Me.FTPORef.Caption = "FTPORef"
        Me.FTPORef.FieldName = "FTPORef"
        Me.FTPORef.Name = "FTPORef"
        Me.FTPORef.OptionsColumn.AllowEdit = False
        Me.FTPORef.OptionsColumn.AllowFocus = False
        Me.FTPORef.Visible = True
        Me.FTPORef.VisibleIndex = 2
        Me.FTPORef.Width = 116
        '
        'cFTStyleCode
        '
        Me.cFTStyleCode.Caption = "FTStyleCode"
        Me.cFTStyleCode.FieldName = "FTStyleCode"
        Me.cFTStyleCode.Name = "cFTStyleCode"
        Me.cFTStyleCode.OptionsColumn.AllowEdit = False
        Me.cFTStyleCode.OptionsColumn.AllowFocus = False
        Me.cFTStyleCode.Visible = True
        Me.cFTStyleCode.VisibleIndex = 3
        Me.cFTStyleCode.Width = 116
        '
        'cFTNikePOLineItem
        '
        Me.cFTNikePOLineItem.Caption = "FTNikePOLineItem"
        Me.cFTNikePOLineItem.FieldName = "FTNikePOLineItem"
        Me.cFTNikePOLineItem.Name = "cFTNikePOLineItem"
        Me.cFTNikePOLineItem.OptionsColumn.AllowEdit = False
        Me.cFTNikePOLineItem.OptionsColumn.AllowFocus = False
        Me.cFTNikePOLineItem.Visible = True
        Me.cFTNikePOLineItem.VisibleIndex = 4
        Me.cFTNikePOLineItem.Width = 83
        '
        'FNHSysStyleId
        '
        Me.FNHSysStyleId.Caption = "FNHSysStyleId"
        Me.FNHSysStyleId.FieldName = "FNHSysStyleId"
        Me.FNHSysStyleId.Name = "FNHSysStyleId"
        Me.FNHSysStyleId.OptionsColumn.AllowEdit = False
        Me.FNHSysStyleId.OptionsColumn.AllowFocus = False
        '
        'cFTColorway
        '
        Me.cFTColorway.Caption = "FTColorway"
        Me.cFTColorway.FieldName = "FTColorway"
        Me.cFTColorway.Name = "cFTColorway"
        Me.cFTColorway.OptionsColumn.AllowEdit = False
        Me.cFTColorway.OptionsColumn.AllowFocus = False
        Me.cFTColorway.Visible = True
        Me.cFTColorway.VisibleIndex = 6
        Me.cFTColorway.Width = 94
        '
        'cFTSizeBreakDown
        '
        Me.cFTSizeBreakDown.Caption = "FTSizeBreakDown"
        Me.cFTSizeBreakDown.FieldName = "FTSizeBreakDown"
        Me.cFTSizeBreakDown.Name = "cFTSizeBreakDown"
        Me.cFTSizeBreakDown.OptionsColumn.AllowEdit = False
        Me.cFTSizeBreakDown.OptionsColumn.AllowFocus = False
        Me.cFTSizeBreakDown.Visible = True
        Me.cFTSizeBreakDown.VisibleIndex = 7
        Me.cFTSizeBreakDown.Width = 71
        '
        'cFNCTNS
        '
        Me.cFNCTNS.Caption = "FNCTNS"
        Me.cFNCTNS.DisplayFormat.FormatString = "N0"
        Me.cFNCTNS.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cFNCTNS.FieldName = "FNCTNS"
        Me.cFNCTNS.Name = "cFNCTNS"
        Me.cFNCTNS.OptionsColumn.AllowEdit = False
        Me.cFNCTNS.OptionsColumn.AllowFocus = False
        Me.cFNCTNS.Width = 90
        '
        'cFNQuantity
        '
        Me.cFNQuantity.Caption = "FNQuantity"
        Me.cFNQuantity.DisplayFormat.FormatString = "N0"
        Me.cFNQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cFNQuantity.FieldName = "FNQuantity"
        Me.cFNQuantity.Name = "cFNQuantity"
        Me.cFNQuantity.OptionsColumn.AllowEdit = False
        Me.cFNQuantity.OptionsColumn.AllowFocus = False
        Me.cFNQuantity.Visible = True
        Me.cFNQuantity.VisibleIndex = 8
        Me.cFNQuantity.Width = 121
        '
        'cFTCTNQtyBal
        '
        Me.cFTCTNQtyBal.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.cFTCTNQtyBal.AppearanceCell.Options.UseBackColor = True
        Me.cFTCTNQtyBal.Caption = "FTCTNQtyBal"
        Me.cFTCTNQtyBal.DisplayFormat.FormatString = "N0"
        Me.cFTCTNQtyBal.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cFTCTNQtyBal.FieldName = "FTCTNQtyBal"
        Me.cFTCTNQtyBal.Name = "cFTCTNQtyBal"
        Me.cFTCTNQtyBal.OptionsColumn.AllowEdit = False
        Me.cFTCTNQtyBal.OptionsColumn.AllowFocus = False
        Me.cFTCTNQtyBal.Width = 93
        '
        'cFNQuantityBal
        '
        Me.cFNQuantityBal.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.cFNQuantityBal.AppearanceCell.Options.UseBackColor = True
        Me.cFNQuantityBal.Caption = "FNQuantityBal"
        Me.cFNQuantityBal.DisplayFormat.FormatString = "N0"
        Me.cFNQuantityBal.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cFNQuantityBal.FieldName = "FNQuantityBal"
        Me.cFNQuantityBal.Name = "cFNQuantityBal"
        Me.cFNQuantityBal.OptionsColumn.AllowEdit = False
        Me.cFNQuantityBal.OptionsColumn.AllowFocus = False
        Me.cFNQuantityBal.Width = 87
        '
        'oSelectAll
        '
        Me.oSelectAll.Location = New System.Drawing.Point(13, 9)
        Me.oSelectAll.Name = "oSelectAll"
        Me.oSelectAll.Properties.AutoHeight = False
        Me.oSelectAll.Properties.Caption = "Select All"
        Me.oSelectAll.Size = New System.Drawing.Size(240, 20)
        Me.oSelectAll.TabIndex = 535
        '
        'cFTBarCodeCarton
        '
        Me.cFTBarCodeCarton.Caption = "FTBarCodeCarton"
        Me.cFTBarCodeCarton.FieldName = "FTBarCodeCarton"
        Me.cFTBarCodeCarton.Name = "cFTBarCodeCarton"
        Me.cFTBarCodeCarton.OptionsColumn.AllowEdit = False
        Me.cFTBarCodeCarton.Visible = True
        Me.cFTBarCodeCarton.VisibleIndex = 5
        Me.cFTBarCodeCarton.Width = 158
        '
        'wExpTruckSheetPopup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1190, 742)
        Me.ControlBox = False
        Me.Controls.Add(Me.oSelectAll)
        Me.Controls.Add(Me.ogcref)
        Me.Controls.Add(Me.ocmcancel)
        Me.Controls.Add(Me.ocmsave)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "wExpTruckSheetPopup"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "On-Hand Checking "
        CType(Me.ogcref, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvref, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemFTSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.oSelectAll.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ocmcancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmsave As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ogcref As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvref As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents FTSelect As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemFTSelect As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents cFTInvoiceNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTPORef As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTStyleCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTNikePOLineItem As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents oSelectAll As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents FNHSysStyleId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTColorway As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTSizeBreakDown As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNCTNS As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTCTNQtyBal As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNQuantityBal As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTBarCodeCarton As DevExpress.XtraGrid.Columns.GridColumn
End Class

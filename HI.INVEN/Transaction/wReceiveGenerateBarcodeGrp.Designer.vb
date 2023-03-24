<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wReceiveGenerateBarcodeGrp
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
        Me.ogbnote = New DevExpress.XtraEditors.GroupControl()
        Me.FTStaSelectAll = New DevExpress.XtraEditors.CheckEdit()
        Me.ogcbarcode = New DevExpress.XtraGrid.GridControl()
        Me.ogvbarcode = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.FTSelect = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepFTSelect = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.FNSeq = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFTBarcodeNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.BFTRawMatCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.BFTDescription = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.BFTRawMatColorCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.BFTRawMatSizeCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.BFTFabricFrontSize = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.BFTOrderNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.BFNQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.BFTBatchNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.BFTGrade = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ocmauto = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmcancel = New DevExpress.XtraEditors.SimpleButton()
        Me.CFTRollNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTBarcodeNo_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTBarcodeNo = New DevExpress.XtraEditors.TextEdit()
        CType(Me.ogbnote, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbnote.SuspendLayout()
        CType(Me.FTStaSelectAll.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogcbarcode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvbarcode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepFTSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTBarcodeNo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ogbnote
        '
        Me.ogbnote.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbnote.Controls.Add(Me.FTBarcodeNo_lbl)
        Me.ogbnote.Controls.Add(Me.FTBarcodeNo)
        Me.ogbnote.Controls.Add(Me.ocmcancel)
        Me.ogbnote.Controls.Add(Me.FTStaSelectAll)
        Me.ogbnote.Controls.Add(Me.ocmauto)
        Me.ogbnote.Location = New System.Drawing.Point(3, 2)
        Me.ogbnote.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbnote.Name = "ogbnote"
        Me.ogbnote.ShowCaption = False
        Me.ogbnote.Size = New System.Drawing.Size(1101, 48)
        Me.ogbnote.TabIndex = 142
        Me.ogbnote.Text = "GroupControl1"
        '
        'FTStaSelectAll
        '
        Me.FTStaSelectAll.Location = New System.Drawing.Point(5, 10)
        Me.FTStaSelectAll.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTStaSelectAll.Name = "FTStaSelectAll"
        Me.FTStaSelectAll.Properties.Appearance.Options.UseTextOptions = True
        Me.FTStaSelectAll.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.FTStaSelectAll.Properties.Caption = "Select All"
        Me.FTStaSelectAll.Properties.ValueChecked = "1"
        Me.FTStaSelectAll.Properties.ValueUnchecked = "0"
        Me.FTStaSelectAll.Size = New System.Drawing.Size(177, 20)
        Me.FTStaSelectAll.TabIndex = 286
        Me.FTStaSelectAll.Tag = "2|"
        '
        'ogcbarcode
        '
        Me.ogcbarcode.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogcbarcode.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcbarcode.Location = New System.Drawing.Point(3, 58)
        Me.ogcbarcode.MainView = Me.ogvbarcode
        Me.ogcbarcode.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcbarcode.Name = "ogcbarcode"
        Me.ogcbarcode.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepFTSelect})
        Me.ogcbarcode.Size = New System.Drawing.Size(1101, 618)
        Me.ogcbarcode.TabIndex = 143
        Me.ogcbarcode.TabStop = False
        Me.ogcbarcode.Tag = "2|"
        Me.ogcbarcode.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvbarcode})
        '
        'ogvbarcode
        '
        Me.ogvbarcode.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.FTSelect, Me.FNSeq, Me.CFTBarcodeNo, Me.BFTRawMatCode, Me.BFTDescription, Me.BFTRawMatColorCode, Me.BFTRawMatSizeCode, Me.BFTFabricFrontSize, Me.BFTOrderNo, Me.BFNQuantity, Me.BFTBatchNo, Me.BFTGrade, Me.CFTRollNo})
        Me.ogvbarcode.GridControl = Me.ogcbarcode
        Me.ogvbarcode.Name = "ogvbarcode"
        Me.ogvbarcode.OptionsCustomization.AllowGroup = False
        Me.ogvbarcode.OptionsCustomization.AllowQuickHideColumns = False
        Me.ogvbarcode.OptionsView.ColumnAutoWidth = False
        Me.ogvbarcode.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.ogvbarcode.OptionsView.ShowGroupPanel = False
        Me.ogvbarcode.Tag = "2|"
        '
        'FTSelect
        '
        Me.FTSelect.Caption = "Select"
        Me.FTSelect.ColumnEdit = Me.RepFTSelect
        Me.FTSelect.FieldName = "FTSelect"
        Me.FTSelect.Name = "FTSelect"
        Me.FTSelect.OptionsColumn.AllowShowHide = False
        Me.FTSelect.Visible = True
        Me.FTSelect.VisibleIndex = 0
        Me.FTSelect.Width = 56
        '
        'RepFTSelect
        '
        Me.RepFTSelect.AutoHeight = False
        Me.RepFTSelect.Caption = "Check"
        Me.RepFTSelect.Name = "RepFTSelect"
        Me.RepFTSelect.ValueChecked = "1"
        Me.RepFTSelect.ValueUnchecked = "0"
        '
        'FNSeq
        '
        Me.FNSeq.AppearanceCell.Options.UseTextOptions = True
        Me.FNSeq.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNSeq.AppearanceHeader.Options.UseTextOptions = True
        Me.FNSeq.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FNSeq.Caption = "ลำดับที่"
        Me.FNSeq.FieldName = "FNSeq"
        Me.FNSeq.Name = "FNSeq"
        Me.FNSeq.OptionsColumn.AllowEdit = False
        Me.FNSeq.OptionsColumn.AllowMove = False
        Me.FNSeq.OptionsColumn.ReadOnly = True
        Me.FNSeq.Visible = True
        Me.FNSeq.VisibleIndex = 1
        Me.FNSeq.Width = 64
        '
        'CFTBarcodeNo
        '
        Me.CFTBarcodeNo.AppearanceHeader.Options.UseTextOptions = True
        Me.CFTBarcodeNo.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.CFTBarcodeNo.Caption = "FTBarcodeNo"
        Me.CFTBarcodeNo.FieldName = "FTBarcodeNo"
        Me.CFTBarcodeNo.Name = "CFTBarcodeNo"
        Me.CFTBarcodeNo.OptionsColumn.AllowEdit = False
        Me.CFTBarcodeNo.OptionsColumn.AllowMove = False
        Me.CFTBarcodeNo.OptionsColumn.ReadOnly = True
        Me.CFTBarcodeNo.Visible = True
        Me.CFTBarcodeNo.VisibleIndex = 2
        Me.CFTBarcodeNo.Width = 134
        '
        'BFTRawMatCode
        '
        Me.BFTRawMatCode.AppearanceHeader.Options.UseTextOptions = True
        Me.BFTRawMatCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.BFTRawMatCode.Caption = "FTRawMatCode"
        Me.BFTRawMatCode.FieldName = "FTRawMatCode"
        Me.BFTRawMatCode.Name = "BFTRawMatCode"
        Me.BFTRawMatCode.OptionsColumn.AllowEdit = False
        Me.BFTRawMatCode.OptionsColumn.AllowMove = False
        Me.BFTRawMatCode.OptionsColumn.ReadOnly = True
        Me.BFTRawMatCode.Visible = True
        Me.BFTRawMatCode.VisibleIndex = 3
        Me.BFTRawMatCode.Width = 112
        '
        'BFTDescription
        '
        Me.BFTDescription.AppearanceHeader.Options.UseTextOptions = True
        Me.BFTDescription.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.BFTDescription.Caption = "FTDescription"
        Me.BFTDescription.FieldName = "FTMatDesc"
        Me.BFTDescription.Name = "BFTDescription"
        Me.BFTDescription.OptionsColumn.AllowEdit = False
        Me.BFTDescription.OptionsColumn.AllowMove = False
        Me.BFTDescription.OptionsColumn.ReadOnly = True
        Me.BFTDescription.Visible = True
        Me.BFTDescription.VisibleIndex = 4
        Me.BFTDescription.Width = 87
        '
        'BFTRawMatColorCode
        '
        Me.BFTRawMatColorCode.AppearanceHeader.Options.UseTextOptions = True
        Me.BFTRawMatColorCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.BFTRawMatColorCode.Caption = "FTRawMatColorCode"
        Me.BFTRawMatColorCode.FieldName = "FTRawMatColorCode"
        Me.BFTRawMatColorCode.Name = "BFTRawMatColorCode"
        Me.BFTRawMatColorCode.OptionsColumn.AllowEdit = False
        Me.BFTRawMatColorCode.OptionsColumn.AllowMove = False
        Me.BFTRawMatColorCode.OptionsColumn.ReadOnly = True
        Me.BFTRawMatColorCode.Visible = True
        Me.BFTRawMatColorCode.VisibleIndex = 5
        Me.BFTRawMatColorCode.Width = 119
        '
        'BFTRawMatSizeCode
        '
        Me.BFTRawMatSizeCode.AppearanceHeader.Options.UseTextOptions = True
        Me.BFTRawMatSizeCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.BFTRawMatSizeCode.Caption = "FTRawMatSizeCode"
        Me.BFTRawMatSizeCode.FieldName = "FTRawMatSizeCode"
        Me.BFTRawMatSizeCode.Name = "BFTRawMatSizeCode"
        Me.BFTRawMatSizeCode.OptionsColumn.AllowEdit = False
        Me.BFTRawMatSizeCode.OptionsColumn.AllowMove = False
        Me.BFTRawMatSizeCode.OptionsColumn.ReadOnly = True
        Me.BFTRawMatSizeCode.Visible = True
        Me.BFTRawMatSizeCode.VisibleIndex = 6
        Me.BFTRawMatSizeCode.Width = 117
        '
        'BFTFabricFrontSize
        '
        Me.BFTFabricFrontSize.AppearanceHeader.Options.UseTextOptions = True
        Me.BFTFabricFrontSize.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.BFTFabricFrontSize.Caption = "FTFabricFrontSize"
        Me.BFTFabricFrontSize.FieldName = "FTFabricFrontSize"
        Me.BFTFabricFrontSize.Name = "BFTFabricFrontSize"
        Me.BFTFabricFrontSize.OptionsColumn.AllowEdit = False
        Me.BFTFabricFrontSize.OptionsColumn.AllowMove = False
        Me.BFTFabricFrontSize.OptionsColumn.ReadOnly = True
        Me.BFTFabricFrontSize.Visible = True
        Me.BFTFabricFrontSize.VisibleIndex = 7
        Me.BFTFabricFrontSize.Width = 98
        '
        'BFTOrderNo
        '
        Me.BFTOrderNo.AppearanceHeader.Options.UseTextOptions = True
        Me.BFTOrderNo.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.BFTOrderNo.Caption = "FTOrderNo"
        Me.BFTOrderNo.FieldName = "FTOrderNo"
        Me.BFTOrderNo.Name = "BFTOrderNo"
        Me.BFTOrderNo.OptionsColumn.AllowEdit = False
        Me.BFTOrderNo.OptionsColumn.AllowMove = False
        Me.BFTOrderNo.OptionsColumn.ReadOnly = True
        Me.BFTOrderNo.Visible = True
        Me.BFTOrderNo.VisibleIndex = 8
        Me.BFTOrderNo.Width = 113
        '
        'BFNQuantity
        '
        Me.BFNQuantity.AppearanceCell.Options.UseTextOptions = True
        Me.BFNQuantity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.BFNQuantity.AppearanceHeader.Options.UseTextOptions = True
        Me.BFNQuantity.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.BFNQuantity.Caption = "FNQuantity"
        Me.BFNQuantity.DisplayFormat.FormatString = "{0:n4}"
        Me.BFNQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.BFNQuantity.FieldName = "FNQuantity"
        Me.BFNQuantity.Name = "BFNQuantity"
        Me.BFNQuantity.OptionsColumn.AllowEdit = False
        Me.BFNQuantity.OptionsColumn.AllowMove = False
        Me.BFNQuantity.OptionsColumn.ReadOnly = True
        Me.BFNQuantity.Visible = True
        Me.BFNQuantity.VisibleIndex = 9
        Me.BFNQuantity.Width = 101
        '
        'BFTBatchNo
        '
        Me.BFTBatchNo.AppearanceHeader.Options.UseTextOptions = True
        Me.BFTBatchNo.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.BFTBatchNo.Caption = "FTBatchNo"
        Me.BFTBatchNo.FieldName = "FTBatchNo"
        Me.BFTBatchNo.Name = "BFTBatchNo"
        Me.BFTBatchNo.OptionsColumn.AllowEdit = False
        Me.BFTBatchNo.OptionsColumn.AllowMove = False
        Me.BFTBatchNo.OptionsColumn.ReadOnly = True
        Me.BFTBatchNo.Visible = True
        Me.BFTBatchNo.VisibleIndex = 10
        Me.BFTBatchNo.Width = 129
        '
        'BFTGrade
        '
        Me.BFTGrade.AppearanceHeader.Options.UseTextOptions = True
        Me.BFTGrade.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.BFTGrade.Caption = "FTGrade"
        Me.BFTGrade.FieldName = "FTGrade"
        Me.BFTGrade.Name = "BFTGrade"
        Me.BFTGrade.OptionsColumn.AllowEdit = False
        Me.BFTGrade.OptionsColumn.AllowMove = False
        Me.BFTGrade.OptionsColumn.ReadOnly = True
        Me.BFTGrade.Visible = True
        Me.BFTGrade.VisibleIndex = 11
        Me.BFTGrade.Width = 118
        '
        'ocmauto
        '
        Me.ocmauto.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmauto.Location = New System.Drawing.Point(700, 6)
        Me.ocmauto.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmauto.Name = "ocmauto"
        Me.ocmauto.Size = New System.Drawing.Size(187, 31)
        Me.ocmauto.TabIndex = 144
        Me.ocmauto.TabStop = False
        Me.ocmauto.Tag = "2|"
        Me.ocmauto.Text = "Generate Barcode Group"
        '
        'ocmcancel
        '
        Me.ocmcancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmcancel.Location = New System.Drawing.Point(906, 6)
        Me.ocmcancel.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmcancel.Name = "ocmcancel"
        Me.ocmcancel.Size = New System.Drawing.Size(187, 31)
        Me.ocmcancel.TabIndex = 145
        Me.ocmcancel.TabStop = False
        Me.ocmcancel.Tag = "2|"
        Me.ocmcancel.Text = "CANCEL"
        '
        'CFTRollNo
        '
        Me.CFTRollNo.Caption = "Roll No"
        Me.CFTRollNo.FieldName = "FTRollNo"
        Me.CFTRollNo.Name = "CFTRollNo"
        Me.CFTRollNo.OptionsColumn.AllowEdit = False
        Me.CFTRollNo.OptionsColumn.AllowMove = False
        Me.CFTRollNo.OptionsColumn.ReadOnly = True
        Me.CFTRollNo.Visible = True
        Me.CFTRollNo.VisibleIndex = 12
        '
        'FTBarcodeNo_lbl
        '
        Me.FTBarcodeNo_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTBarcodeNo_lbl.Appearance.Options.UseForeColor = True
        Me.FTBarcodeNo_lbl.Appearance.Options.UseTextOptions = True
        Me.FTBarcodeNo_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTBarcodeNo_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTBarcodeNo_lbl.Location = New System.Drawing.Point(194, 9)
        Me.FTBarcodeNo_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTBarcodeNo_lbl.Name = "FTBarcodeNo_lbl"
        Me.FTBarcodeNo_lbl.Size = New System.Drawing.Size(146, 23)
        Me.FTBarcodeNo_lbl.TabIndex = 289
        Me.FTBarcodeNo_lbl.Tag = "2|"
        Me.FTBarcodeNo_lbl.Text = "Barcode No :"
        '
        'FTBarcodeNo
        '
        Me.FTBarcodeNo.Location = New System.Drawing.Point(343, 9)
        Me.FTBarcodeNo.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTBarcodeNo.Name = "FTBarcodeNo"
        Me.FTBarcodeNo.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FTBarcodeNo.Properties.Appearance.Options.UseBackColor = True
        Me.FTBarcodeNo.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FTBarcodeNo.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FTBarcodeNo.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FTBarcodeNo.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FTBarcodeNo.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FTBarcodeNo.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FTBarcodeNo.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FTBarcodeNo.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FTBarcodeNo.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FTBarcodeNo.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FTBarcodeNo.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FTBarcodeNo.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FTBarcodeNo.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.FTBarcodeNo.Properties.MaxLength = 30
        Me.FTBarcodeNo.Size = New System.Drawing.Size(255, 22)
        Me.FTBarcodeNo.TabIndex = 288
        Me.FTBarcodeNo.Tag = "2|"
        '
        'wReceiveGenerateBarcodeGrp
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1108, 679)
        Me.Controls.Add(Me.ogcbarcode)
        Me.Controls.Add(Me.ogbnote)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "wReceiveGenerateBarcodeGrp"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Receive Generate Barcode Group"
        CType(Me.ogbnote, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbnote.ResumeLayout(False)
        CType(Me.FTStaSelectAll.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogcbarcode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvbarcode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepFTSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTBarcodeNo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogbnote As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ogcbarcode As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvbarcode As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents FNSeq As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFTBarcodeNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents BFTRawMatCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents BFTDescription As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents BFTRawMatColorCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents BFTRawMatSizeCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents BFTFabricFrontSize As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents BFTOrderNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents BFNQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents BFTBatchNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents BFTGrade As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ocmauto As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmcancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FTSelect As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepFTSelect As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents FTStaSelectAll As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents CFTRollNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTBarcodeNo_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTBarcodeNo As DevExpress.XtraEditors.TextEdit
End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wTransferAssetAddBarRcv
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
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject4 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject5 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.ogbnote = New DevExpress.XtraEditors.GroupControl()
        Me.FNHSysWHAssetId = New DevExpress.XtraEditors.ButtonEdit()
        Me.ocmload = New DevExpress.XtraEditors.SimpleButton()
        Me.FTReceiveNo = New DevExpress.XtraEditors.ButtonEdit()
        Me.FTReceiveNo_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTStaSelectAll = New DevExpress.XtraEditors.CheckEdit()
        Me.ocmok = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmcancel = New DevExpress.XtraEditors.SimpleButton()
        Me.RepFTSelect = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.ogvbarcode = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.FTSelect = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNSeq = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTBarcodeNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTAssetCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTAssetDesc = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.Model = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTProductCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.BFNQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNHSysCmpIdTo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNOrderType = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ogcbarcode = New DevExpress.XtraGrid.GridControl()
        CType(Me.ogbnote, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbnote.SuspendLayout()
        CType(Me.FNHSysWHAssetId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTReceiveNo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTStaSelectAll.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepFTSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvbarcode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogcbarcode, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ogbnote
        '
        Me.ogbnote.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbnote.Controls.Add(Me.FNHSysWHAssetId)
        Me.ogbnote.Controls.Add(Me.ocmload)
        Me.ogbnote.Controls.Add(Me.FTReceiveNo)
        Me.ogbnote.Controls.Add(Me.FTReceiveNo_lbl)
        Me.ogbnote.Controls.Add(Me.FTStaSelectAll)
        Me.ogbnote.Location = New System.Drawing.Point(3, 2)
        Me.ogbnote.Name = "ogbnote"
        Me.ogbnote.ShowCaption = False
        Me.ogbnote.Size = New System.Drawing.Size(750, 55)
        Me.ogbnote.TabIndex = 142
        Me.ogbnote.Text = "GroupControl1"
        '
        'FNHSysWHAssetId
        '
        Me.FNHSysWHAssetId.EnterMoveNextControl = True
        Me.FNHSysWHAssetId.Location = New System.Drawing.Point(117, 34)
        Me.FNHSysWHAssetId.Name = "FNHSysWHAssetId"
        Me.FNHSysWHAssetId.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FNHSysWHAssetId.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysWHAssetId.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysWHAssetId.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysWHAssetId.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FNHSysWHAssetId.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FNHSysWHAssetId.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNHSysWHAssetId.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysWHAssetId.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNHSysWHAssetId.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNHSysWHAssetId.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysWHAssetId.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysWHAssetId.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNHSysWHAssetId.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNHSysWHAssetId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", 15, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", "110", Nothing, True)})
        Me.FNHSysWHAssetId.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.FNHSysWHAssetId.Properties.MaxLength = 30
        Me.FNHSysWHAssetId.Size = New System.Drawing.Size(127, 20)
        Me.FNHSysWHAssetId.TabIndex = 290
        Me.FNHSysWHAssetId.Tag = "2|"
        Me.FNHSysWHAssetId.Visible = False
        '
        'ocmload
        '
        Me.ocmload.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmload.Location = New System.Drawing.Point(534, 11)
        Me.ocmload.Name = "ocmload"
        Me.ocmload.Size = New System.Drawing.Size(160, 22)
        Me.ocmload.TabIndex = 289
        Me.ocmload.TabStop = False
        Me.ocmload.Tag = "2|"
        Me.ocmload.Text = "Load"
        '
        'FTReceiveNo
        '
        Me.FTReceiveNo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FTReceiveNo.EnterMoveNextControl = True
        Me.FTReceiveNo.Location = New System.Drawing.Point(244, 15)
        Me.FTReceiveNo.Name = "FTReceiveNo"
        Me.FTReceiveNo.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FTReceiveNo.Properties.Appearance.Options.UseBackColor = True
        Me.FTReceiveNo.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FTReceiveNo.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FTReceiveNo.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FTReceiveNo.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FTReceiveNo.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FTReceiveNo.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FTReceiveNo.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FTReceiveNo.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FTReceiveNo.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FTReceiveNo.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FTReceiveNo.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FTReceiveNo.Properties.AppearanceReadOnly.Options.UseForeColor = True
        SerializableAppearanceObject2.Options.UseTextOptions = True
        SerializableAppearanceObject2.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        SerializableAppearanceObject3.Options.UseTextOptions = True
        SerializableAppearanceObject3.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        SerializableAppearanceObject4.Options.UseTextOptions = True
        SerializableAppearanceObject4.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        SerializableAppearanceObject5.Options.UseTextOptions = True
        SerializableAppearanceObject5.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTReceiveNo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "...", 15, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, "", New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject2, SerializableAppearanceObject3, SerializableAppearanceObject4, SerializableAppearanceObject5, "", "406", Nothing, True)})
        Me.FTReceiveNo.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.FTReceiveNo.Size = New System.Drawing.Size(265, 20)
        Me.FTReceiveNo.TabIndex = 287
        Me.FTReceiveNo.TabStop = False
        Me.FTReceiveNo.Tag = "2|"
        '
        'FTReceiveNo_lbl
        '
        Me.FTReceiveNo_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTReceiveNo_lbl.Appearance.Options.UseForeColor = True
        Me.FTReceiveNo_lbl.Appearance.Options.UseTextOptions = True
        Me.FTReceiveNo_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTReceiveNo_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTReceiveNo_lbl.Location = New System.Drawing.Point(51, 14)
        Me.FTReceiveNo_lbl.Name = "FTReceiveNo_lbl"
        Me.FTReceiveNo_lbl.Size = New System.Drawing.Size(194, 19)
        Me.FTReceiveNo_lbl.TabIndex = 288
        Me.FTReceiveNo_lbl.Tag = "2|"
        Me.FTReceiveNo_lbl.Text = "Document No. :"
        '
        'FTStaSelectAll
        '
        Me.FTStaSelectAll.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FTStaSelectAll.Location = New System.Drawing.Point(403, 35)
        Me.FTStaSelectAll.Name = "FTStaSelectAll"
        Me.FTStaSelectAll.Properties.Appearance.Options.UseTextOptions = True
        Me.FTStaSelectAll.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.FTStaSelectAll.Properties.Caption = "Select All"
        Me.FTStaSelectAll.Properties.ValueChecked = "1"
        Me.FTStaSelectAll.Properties.ValueUnchecked = "0"
        Me.FTStaSelectAll.Size = New System.Drawing.Size(123, 19)
        Me.FTStaSelectAll.TabIndex = 286
        Me.FTStaSelectAll.Tag = "2|"
        '
        'ocmok
        '
        Me.ocmok.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmok.Location = New System.Drawing.Point(760, 3)
        Me.ocmok.Name = "ocmok"
        Me.ocmok.Size = New System.Drawing.Size(160, 25)
        Me.ocmok.TabIndex = 144
        Me.ocmok.TabStop = False
        Me.ocmok.Tag = "2|"
        Me.ocmok.Text = "Transfer"
        '
        'ocmcancel
        '
        Me.ocmcancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmcancel.Location = New System.Drawing.Point(760, 32)
        Me.ocmcancel.Name = "ocmcancel"
        Me.ocmcancel.Size = New System.Drawing.Size(160, 25)
        Me.ocmcancel.TabIndex = 145
        Me.ocmcancel.TabStop = False
        Me.ocmcancel.Tag = "2|"
        Me.ocmcancel.Text = "CANCEL"
        '
        'RepFTSelect
        '
        Me.RepFTSelect.AutoHeight = False
        Me.RepFTSelect.Caption = "Check"
        Me.RepFTSelect.Name = "RepFTSelect"
        Me.RepFTSelect.ValueChecked = "1"
        Me.RepFTSelect.ValueUnchecked = "0"
        '
        'ogvbarcode
        '
        Me.ogvbarcode.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.FTSelect, Me.FNSeq, Me.FTBarcodeNo, Me.FTAssetCode, Me.FTAssetDesc, Me.Model, Me.FTProductCode, Me.BFNQuantity, Me.FNHSysCmpIdTo, Me.FNOrderType})
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
        'FTBarcodeNo
        '
        Me.FTBarcodeNo.AppearanceHeader.Options.UseTextOptions = True
        Me.FTBarcodeNo.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTBarcodeNo.Caption = "FTBarcodeNo"
        Me.FTBarcodeNo.FieldName = "FTBarcodeNo"
        Me.FTBarcodeNo.Name = "FTBarcodeNo"
        Me.FTBarcodeNo.OptionsColumn.ReadOnly = True
        Me.FTBarcodeNo.Visible = True
        Me.FTBarcodeNo.VisibleIndex = 2
        Me.FTBarcodeNo.Width = 134
        '
        'FTAssetCode
        '
        Me.FTAssetCode.AppearanceHeader.Options.UseTextOptions = True
        Me.FTAssetCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTAssetCode.Caption = "FTAssetCode"
        Me.FTAssetCode.FieldName = "FTAssetCode"
        Me.FTAssetCode.Name = "FTAssetCode"
        Me.FTAssetCode.OptionsColumn.AllowEdit = False
        Me.FTAssetCode.OptionsColumn.ReadOnly = True
        Me.FTAssetCode.Visible = True
        Me.FTAssetCode.VisibleIndex = 3
        Me.FTAssetCode.Width = 112
        '
        'FTAssetDesc
        '
        Me.FTAssetDesc.AppearanceHeader.Options.UseTextOptions = True
        Me.FTAssetDesc.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTAssetDesc.Caption = "FTAssetDesc"
        Me.FTAssetDesc.FieldName = "FTAssetDesc"
        Me.FTAssetDesc.Name = "FTAssetDesc"
        Me.FTAssetDesc.OptionsColumn.AllowEdit = False
        Me.FTAssetDesc.OptionsColumn.ReadOnly = True
        Me.FTAssetDesc.Visible = True
        Me.FTAssetDesc.VisibleIndex = 4
        Me.FTAssetDesc.Width = 87
        '
        'Model
        '
        Me.Model.AppearanceHeader.Options.UseTextOptions = True
        Me.Model.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.Model.Caption = "Model"
        Me.Model.FieldName = "Model"
        Me.Model.Name = "Model"
        Me.Model.OptionsColumn.AllowEdit = False
        Me.Model.OptionsColumn.ReadOnly = True
        Me.Model.Visible = True
        Me.Model.VisibleIndex = 5
        Me.Model.Width = 119
        '
        'FTProductCode
        '
        Me.FTProductCode.AppearanceHeader.Options.UseTextOptions = True
        Me.FTProductCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTProductCode.Caption = "FTProductCode"
        Me.FTProductCode.FieldName = "FTProductCode"
        Me.FTProductCode.Name = "FTProductCode"
        Me.FTProductCode.OptionsColumn.AllowEdit = False
        Me.FTProductCode.OptionsColumn.ReadOnly = True
        Me.FTProductCode.Visible = True
        Me.FTProductCode.VisibleIndex = 6
        Me.FTProductCode.Width = 113
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
        Me.BFNQuantity.OptionsColumn.ReadOnly = True
        Me.BFNQuantity.Visible = True
        Me.BFNQuantity.VisibleIndex = 7
        Me.BFNQuantity.Width = 101
        '
        'FNHSysCmpIdTo
        '
        Me.FNHSysCmpIdTo.Caption = "FNHSysCmpIdTo"
        Me.FNHSysCmpIdTo.FieldName = "FNHSysCmpIdTo"
        Me.FNHSysCmpIdTo.Name = "FNHSysCmpIdTo"
        Me.FNHSysCmpIdTo.OptionsColumn.AllowShowHide = False
        Me.FNHSysCmpIdTo.OptionsColumn.ShowInCustomizationForm = False
        '
        'FNOrderType
        '
        Me.FNOrderType.Caption = "FNOrderType"
        Me.FNOrderType.FieldName = "FNOrderType"
        Me.FNOrderType.Name = "FNOrderType"
        Me.FNOrderType.OptionsColumn.AllowShowHide = False
        Me.FNOrderType.OptionsColumn.AllowSize = False
        Me.FNOrderType.OptionsColumn.ShowInCustomizationForm = False
        '
        'ogcbarcode
        '
        Me.ogcbarcode.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogcbarcode.Location = New System.Drawing.Point(3, 63)
        Me.ogcbarcode.MainView = Me.ogvbarcode
        Me.ogcbarcode.Name = "ogcbarcode"
        Me.ogcbarcode.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepFTSelect})
        Me.ogcbarcode.Size = New System.Drawing.Size(924, 394)
        Me.ogcbarcode.TabIndex = 143
        Me.ogcbarcode.TabStop = False
        Me.ogcbarcode.Tag = "2|"
        Me.ogcbarcode.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvbarcode})
        '
        'wTransferAssetAddBarRcv
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(930, 460)
        Me.Controls.Add(Me.ocmcancel)
        Me.Controls.Add(Me.ocmok)
        Me.Controls.Add(Me.ogcbarcode)
        Me.Controls.Add(Me.ogbnote)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "wTransferAssetAddBarRcv"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Receive Auto Transfer To WH"
        CType(Me.ogbnote, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbnote.ResumeLayout(False)
        CType(Me.FNHSysWHAssetId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTReceiveNo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTStaSelectAll.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepFTSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvbarcode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogcbarcode, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogbnote As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ocmok As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmcancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FTStaSelectAll As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents ocmload As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FTReceiveNo As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FTReceiveNo_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysWHAssetId As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents RepFTSelect As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents ogvbarcode As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents FTSelect As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNSeq As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTBarcodeNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTAssetCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTAssetDesc As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents Model As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTProductCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents BFNQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNHSysCmpIdTo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNOrderType As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ogcbarcode As DevExpress.XtraGrid.GridControl
End Class

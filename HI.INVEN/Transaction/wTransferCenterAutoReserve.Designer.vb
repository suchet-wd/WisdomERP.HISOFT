﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wTransferCenterAutoReserve
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
        Me.ogbnote = New DevExpress.XtraEditors.GroupControl()
        Me.FTStaSelectAll = New DevExpress.XtraEditors.CheckEdit()
        Me.FNHSysCmpId = New DevExpress.XtraEditors.TextEdit()
        Me.FTOrderNo = New DevExpress.XtraEditors.ButtonEdit()
        Me.FTOrderNo_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTRemark_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTRemark = New DevExpress.XtraEditors.MemoEdit()
        Me.ogcbarcode = New DevExpress.XtraGrid.GridControl()
        Me.ogvbarcode = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.FTSelect = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepFTSelect = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.FNSeq = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTWHCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTBarcodeNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.BFTRawMatCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.BFTDescription = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.BFTRawMatColorCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.BFTRawMatSizeCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.BFTFabricFrontSize = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.BFTOrderNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.BFNQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.BFTBatchNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.BFTGrade = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GFNHSysWHId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFNPriceTrans = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ocmauto = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmcancel = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.ogbnote, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbnote.SuspendLayout()
        CType(Me.FTStaSelectAll.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysCmpId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTOrderNo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTRemark.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogcbarcode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvbarcode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepFTSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ogbnote
        '
        Me.ogbnote.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbnote.Controls.Add(Me.FTStaSelectAll)
        Me.ogbnote.Controls.Add(Me.FNHSysCmpId)
        Me.ogbnote.Controls.Add(Me.FTOrderNo)
        Me.ogbnote.Controls.Add(Me.FTOrderNo_lbl)
        Me.ogbnote.Controls.Add(Me.FTRemark_lbl)
        Me.ogbnote.Controls.Add(Me.FTRemark)
        Me.ogbnote.Location = New System.Drawing.Point(3, 2)
        Me.ogbnote.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbnote.Name = "ogbnote"
        Me.ogbnote.ShowCaption = False
        Me.ogbnote.Size = New System.Drawing.Size(766, 94)
        Me.ogbnote.TabIndex = 142
        Me.ogbnote.Text = "GroupControl1"
        '
        'FTStaSelectAll
        '
        Me.FTStaSelectAll.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FTStaSelectAll.Location = New System.Drawing.Point(5, 66)
        Me.FTStaSelectAll.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTStaSelectAll.Name = "FTStaSelectAll"
        Me.FTStaSelectAll.Properties.Appearance.Options.UseTextOptions = True
        Me.FTStaSelectAll.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.FTStaSelectAll.Properties.Caption = "Select All"
        Me.FTStaSelectAll.Properties.ValueChecked = "1"
        Me.FTStaSelectAll.Properties.ValueUnchecked = "0"
        Me.FTStaSelectAll.Size = New System.Drawing.Size(144, 20)
        Me.FTStaSelectAll.TabIndex = 286
        Me.FTStaSelectAll.Tag = "2|"
        '
        'FNHSysCmpId
        '
        Me.FNHSysCmpId.Location = New System.Drawing.Point(14, 36)
        Me.FNHSysCmpId.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysCmpId.Name = "FNHSysCmpId"
        Me.FNHSysCmpId.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FNHSysCmpId.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysCmpId.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysCmpId.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysCmpId.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FNHSysCmpId.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FNHSysCmpId.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNHSysCmpId.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysCmpId.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNHSysCmpId.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNHSysCmpId.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysCmpId.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysCmpId.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNHSysCmpId.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNHSysCmpId.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.FNHSysCmpId.Properties.MaxLength = 30
        Me.FNHSysCmpId.Size = New System.Drawing.Size(65, 23)
        Me.FNHSysCmpId.TabIndex = 285
        Me.FNHSysCmpId.Tag = "|"
        Me.FNHSysCmpId.Visible = False
        '
        'FTOrderNo
        '
        Me.FTOrderNo.EnterMoveNextControl = True
        Me.FTOrderNo.Location = New System.Drawing.Point(153, 11)
        Me.FTOrderNo.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTOrderNo.Name = "FTOrderNo"
        Me.FTOrderNo.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FTOrderNo.Properties.Appearance.Options.UseBackColor = True
        Me.FTOrderNo.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FTOrderNo.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FTOrderNo.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FTOrderNo.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FTOrderNo.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FTOrderNo.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FTOrderNo.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FTOrderNo.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FTOrderNo.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FTOrderNo.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FTOrderNo.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FTOrderNo.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FTOrderNo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", 15, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", "213", Nothing, True)})
        Me.FTOrderNo.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.FTOrderNo.Properties.MaxLength = 30
        Me.FTOrderNo.Size = New System.Drawing.Size(191, 23)
        Me.FTOrderNo.TabIndex = 280
        Me.FTOrderNo.Tag = "2|"
        '
        'FTOrderNo_lbl
        '
        Me.FTOrderNo_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTOrderNo_lbl.Appearance.Options.UseForeColor = True
        Me.FTOrderNo_lbl.Appearance.Options.UseTextOptions = True
        Me.FTOrderNo_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTOrderNo_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTOrderNo_lbl.Location = New System.Drawing.Point(15, 11)
        Me.FTOrderNo_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTOrderNo_lbl.Name = "FTOrderNo_lbl"
        Me.FTOrderNo_lbl.Size = New System.Drawing.Size(134, 23)
        Me.FTOrderNo_lbl.TabIndex = 281
        Me.FTOrderNo_lbl.Tag = "2|"
        Me.FTOrderNo_lbl.Text = "To Order No :"
        '
        'FTRemark_lbl
        '
        Me.FTRemark_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTRemark_lbl.Appearance.Options.UseForeColor = True
        Me.FTRemark_lbl.Appearance.Options.UseTextOptions = True
        Me.FTRemark_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTRemark_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTRemark_lbl.Location = New System.Drawing.Point(13, 37)
        Me.FTRemark_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTRemark_lbl.Name = "FTRemark_lbl"
        Me.FTRemark_lbl.Size = New System.Drawing.Size(135, 23)
        Me.FTRemark_lbl.TabIndex = 272
        Me.FTRemark_lbl.Tag = "2|"
        Me.FTRemark_lbl.Text = "Note :"
        '
        'FTRemark
        '
        Me.FTRemark.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FTRemark.EditValue = ""
        Me.FTRemark.Location = New System.Drawing.Point(153, 37)
        Me.FTRemark.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTRemark.Name = "FTRemark"
        Me.FTRemark.Properties.MaxLength = 500
        Me.FTRemark.Size = New System.Drawing.Size(607, 50)
        Me.FTRemark.TabIndex = 2
        Me.FTRemark.Tag = "2|"
        '
        'ogcbarcode
        '
        Me.ogcbarcode.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogcbarcode.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcbarcode.Location = New System.Drawing.Point(3, 100)
        Me.ogcbarcode.MainView = Me.ogvbarcode
        Me.ogcbarcode.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcbarcode.Name = "ogcbarcode"
        Me.ogcbarcode.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepFTSelect})
        Me.ogcbarcode.Size = New System.Drawing.Size(969, 463)
        Me.ogcbarcode.TabIndex = 143
        Me.ogcbarcode.TabStop = False
        Me.ogcbarcode.Tag = "2|"
        Me.ogcbarcode.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvbarcode})
        '
        'ogvbarcode
        '
        Me.ogvbarcode.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.FTSelect, Me.FNSeq, Me.FTWHCode, Me.FTBarcodeNo, Me.BFTRawMatCode, Me.BFTDescription, Me.BFTRawMatColorCode, Me.BFTRawMatSizeCode, Me.BFTFabricFrontSize, Me.BFTOrderNo, Me.BFNQuantity, Me.BFTBatchNo, Me.BFTGrade, Me.GFNHSysWHId, Me.CFNPriceTrans})
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
        'FTWHCode
        '
        Me.FTWHCode.Caption = "FTWHCode"
        Me.FTWHCode.FieldName = "FTWHCode"
        Me.FTWHCode.Name = "FTWHCode"
        Me.FTWHCode.OptionsColumn.AllowEdit = False
        Me.FTWHCode.OptionsColumn.ReadOnly = True
        Me.FTWHCode.Visible = True
        Me.FTWHCode.VisibleIndex = 2
        Me.FTWHCode.Width = 100
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
        Me.FTBarcodeNo.VisibleIndex = 3
        Me.FTBarcodeNo.Width = 134
        '
        'BFTRawMatCode
        '
        Me.BFTRawMatCode.AppearanceHeader.Options.UseTextOptions = True
        Me.BFTRawMatCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.BFTRawMatCode.Caption = "FTRawMatCode"
        Me.BFTRawMatCode.FieldName = "FTRawMatCode"
        Me.BFTRawMatCode.Name = "BFTRawMatCode"
        Me.BFTRawMatCode.OptionsColumn.AllowEdit = False
        Me.BFTRawMatCode.OptionsColumn.ReadOnly = True
        Me.BFTRawMatCode.Visible = True
        Me.BFTRawMatCode.VisibleIndex = 4
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
        Me.BFTDescription.OptionsColumn.ReadOnly = True
        Me.BFTDescription.Visible = True
        Me.BFTDescription.VisibleIndex = 5
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
        Me.BFTRawMatColorCode.OptionsColumn.ReadOnly = True
        Me.BFTRawMatColorCode.Visible = True
        Me.BFTRawMatColorCode.VisibleIndex = 6
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
        Me.BFTRawMatSizeCode.OptionsColumn.ReadOnly = True
        Me.BFTRawMatSizeCode.Visible = True
        Me.BFTRawMatSizeCode.VisibleIndex = 7
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
        Me.BFTFabricFrontSize.OptionsColumn.ReadOnly = True
        Me.BFTFabricFrontSize.Visible = True
        Me.BFTFabricFrontSize.VisibleIndex = 8
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
        Me.BFTOrderNo.OptionsColumn.ReadOnly = True
        Me.BFTOrderNo.Visible = True
        Me.BFTOrderNo.VisibleIndex = 9
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
        Me.BFNQuantity.OptionsColumn.ReadOnly = True
        Me.BFNQuantity.Visible = True
        Me.BFNQuantity.VisibleIndex = 10
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
        Me.BFTBatchNo.OptionsColumn.ReadOnly = True
        Me.BFTBatchNo.Visible = True
        Me.BFTBatchNo.VisibleIndex = 11
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
        Me.BFTGrade.OptionsColumn.ReadOnly = True
        Me.BFTGrade.Visible = True
        Me.BFTGrade.VisibleIndex = 12
        Me.BFTGrade.Width = 118
        '
        'GFNHSysWHId
        '
        Me.GFNHSysWHId.Caption = "FNHSysWHId"
        Me.GFNHSysWHId.FieldName = "FNHSysWHId"
        Me.GFNHSysWHId.Name = "GFNHSysWHId"
        '
        'CFNPriceTrans
        '
        Me.CFNPriceTrans.Caption = "FNPriceTrans"
        Me.CFNPriceTrans.FieldName = "FNPriceTrans"
        Me.CFNPriceTrans.Name = "CFNPriceTrans"
        '
        'ocmauto
        '
        Me.ocmauto.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmauto.Location = New System.Drawing.Point(778, 15)
        Me.ocmauto.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmauto.Name = "ocmauto"
        Me.ocmauto.Size = New System.Drawing.Size(187, 31)
        Me.ocmauto.TabIndex = 144
        Me.ocmauto.TabStop = False
        Me.ocmauto.Tag = "2|"
        Me.ocmauto.Text = "AUTO"
        '
        'ocmcancel
        '
        Me.ocmcancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmcancel.Location = New System.Drawing.Point(778, 54)
        Me.ocmcancel.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmcancel.Name = "ocmcancel"
        Me.ocmcancel.Size = New System.Drawing.Size(187, 31)
        Me.ocmcancel.TabIndex = 145
        Me.ocmcancel.TabStop = False
        Me.ocmcancel.Tag = "2|"
        Me.ocmcancel.Text = "CANCEL"
        '
        'wTransferCenterAutoReserve
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(976, 566)
        Me.Controls.Add(Me.ocmcancel)
        Me.Controls.Add(Me.ocmauto)
        Me.Controls.Add(Me.ogcbarcode)
        Me.Controls.Add(Me.ogbnote)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "wTransferCenterAutoReserve"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Transfer Center Auto Reserve"
        CType(Me.ogbnote, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbnote.ResumeLayout(False)
        CType(Me.FTStaSelectAll.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysCmpId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTOrderNo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTRemark.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogcbarcode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvbarcode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepFTSelect, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogbnote As DevExpress.XtraEditors.GroupControl
    Friend WithEvents FTOrderNo As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FTOrderNo_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTRemark_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTRemark As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents ogcbarcode As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvbarcode As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents FNSeq As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTBarcodeNo As DevExpress.XtraGrid.Columns.GridColumn
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
    Friend WithEvents FNHSysCmpId As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FTSelect As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepFTSelect As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents FTStaSelectAll As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents FTWHCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GFNHSysWHId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFNPriceTrans As DevExpress.XtraGrid.Columns.GridColumn
End Class

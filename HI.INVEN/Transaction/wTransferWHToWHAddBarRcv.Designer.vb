<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wTransferWHToWHAddBarRcv
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
        Me.FNHSysWHId = New DevExpress.XtraEditors.ButtonEdit()
        Me.ocmload = New DevExpress.XtraEditors.SimpleButton()
        Me.FTReceiveNo = New DevExpress.XtraEditors.ButtonEdit()
        Me.FTReceiveNo_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTStaSelectAll = New DevExpress.XtraEditors.CheckEdit()
        Me.ogcbarcode = New DevExpress.XtraGrid.GridControl()
        Me.ogvbarcode = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.FTSelect = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepFTSelect = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.FNSeq = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTBarcodeNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.BFTRawMatCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.BFTDescription = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.BFTRawMatColorCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.BFTRawMatSizeCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.BFTFabricFrontSize = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.BFTOrderNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFNQuantityOrg = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.BFNQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ReposFNQuantity = New DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit()
        Me.BFTBatchNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.BFTGrade = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNHSysCmpIdTo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNOrderType = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTCmpCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ocmok = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmcancel = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.ogbnote, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbnote.SuspendLayout()
        CType(Me.FNHSysWHId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTReceiveNo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTStaSelectAll.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogcbarcode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvbarcode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepFTSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ReposFNQuantity, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ogbnote
        '
        Me.ogbnote.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbnote.Controls.Add(Me.FNHSysWHId)
        Me.ogbnote.Controls.Add(Me.ocmload)
        Me.ogbnote.Controls.Add(Me.FTReceiveNo)
        Me.ogbnote.Controls.Add(Me.FTReceiveNo_lbl)
        Me.ogbnote.Controls.Add(Me.FTStaSelectAll)
        Me.ogbnote.Location = New System.Drawing.Point(3, 2)
        Me.ogbnote.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbnote.Name = "ogbnote"
        Me.ogbnote.ShowCaption = False
        Me.ogbnote.Size = New System.Drawing.Size(1041, 68)
        Me.ogbnote.TabIndex = 142
        Me.ogbnote.Text = "GroupControl1"
        '
        'FNHSysWHId
        '
        Me.FNHSysWHId.EnterMoveNextControl = True
        Me.FNHSysWHId.Location = New System.Drawing.Point(137, 42)
        Me.FNHSysWHId.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysWHId.Name = "FNHSysWHId"
        Me.FNHSysWHId.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FNHSysWHId.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysWHId.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysWHId.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysWHId.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FNHSysWHId.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FNHSysWHId.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNHSysWHId.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysWHId.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNHSysWHId.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNHSysWHId.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysWHId.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysWHId.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNHSysWHId.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNHSysWHId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", 15, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", "110", Nothing, True)})
        Me.FNHSysWHId.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.FNHSysWHId.Properties.MaxLength = 30
        Me.FNHSysWHId.Size = New System.Drawing.Size(148, 23)
        Me.FNHSysWHId.TabIndex = 290
        Me.FNHSysWHId.Tag = "2|"
        Me.FNHSysWHId.Visible = False
        '
        'ocmload
        '
        Me.ocmload.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmload.Location = New System.Drawing.Point(789, 14)
        Me.ocmload.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmload.Name = "ocmload"
        Me.ocmload.Size = New System.Drawing.Size(187, 27)
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
        Me.FTReceiveNo.Location = New System.Drawing.Point(528, 18)
        Me.FTReceiveNo.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
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
        Me.FTReceiveNo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "...", 15, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, "", New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject2, SerializableAppearanceObject3, SerializableAppearanceObject4, SerializableAppearanceObject5, "", "238", Nothing, True)})
        Me.FTReceiveNo.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.FTReceiveNo.Size = New System.Drawing.Size(233, 23)
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
        Me.FTReceiveNo_lbl.Location = New System.Drawing.Point(59, 17)
        Me.FTReceiveNo_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTReceiveNo_lbl.Name = "FTReceiveNo_lbl"
        Me.FTReceiveNo_lbl.Size = New System.Drawing.Size(463, 23)
        Me.FTReceiveNo_lbl.TabIndex = 288
        Me.FTReceiveNo_lbl.Tag = "2|"
        Me.FTReceiveNo_lbl.Text = "Document No. :"
        '
        'FTStaSelectAll
        '
        Me.FTStaSelectAll.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FTStaSelectAll.Location = New System.Drawing.Point(9, 44)
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
        'ogcbarcode
        '
        Me.ogcbarcode.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogcbarcode.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcbarcode.Location = New System.Drawing.Point(3, 78)
        Me.ogcbarcode.MainView = Me.ogvbarcode
        Me.ogcbarcode.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcbarcode.Name = "ogcbarcode"
        Me.ogcbarcode.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepFTSelect, Me.ReposFNQuantity})
        Me.ogcbarcode.Size = New System.Drawing.Size(1244, 485)
        Me.ogcbarcode.TabIndex = 143
        Me.ogcbarcode.TabStop = False
        Me.ogcbarcode.Tag = "2|"
        Me.ogcbarcode.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvbarcode})
        '
        'ogvbarcode
        '
        Me.ogvbarcode.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.FTSelect, Me.FNSeq, Me.FTBarcodeNo, Me.BFTRawMatCode, Me.BFTDescription, Me.BFTRawMatColorCode, Me.BFTRawMatSizeCode, Me.BFTFabricFrontSize, Me.BFTOrderNo, Me.CFNQuantityOrg, Me.BFNQuantity, Me.BFTBatchNo, Me.BFTGrade, Me.FNHSysCmpIdTo, Me.FNOrderType, Me.FTCmpCode})
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
        Me.BFTOrderNo.OptionsColumn.ReadOnly = True
        Me.BFTOrderNo.Visible = True
        Me.BFTOrderNo.VisibleIndex = 8
        Me.BFTOrderNo.Width = 113
        '
        'CFNQuantityOrg
        '
        Me.CFNQuantityOrg.Caption = "Quantity Bal."
        Me.CFNQuantityOrg.FieldName = "FNQuantityOrg"
        Me.CFNQuantityOrg.Name = "CFNQuantityOrg"
        Me.CFNQuantityOrg.OptionsColumn.AllowEdit = False
        Me.CFNQuantityOrg.OptionsColumn.ReadOnly = True
        Me.CFNQuantityOrg.Visible = True
        Me.CFNQuantityOrg.VisibleIndex = 9
        '
        'BFNQuantity
        '
        Me.BFNQuantity.AppearanceCell.Options.UseTextOptions = True
        Me.BFNQuantity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.BFNQuantity.AppearanceHeader.Options.UseTextOptions = True
        Me.BFNQuantity.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.BFNQuantity.Caption = "FNQuantity"
        Me.BFNQuantity.ColumnEdit = Me.ReposFNQuantity
        Me.BFNQuantity.DisplayFormat.FormatString = "{0:n4}"
        Me.BFNQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.BFNQuantity.FieldName = "FNQuantity"
        Me.BFNQuantity.Name = "BFNQuantity"
        Me.BFNQuantity.Visible = True
        Me.BFNQuantity.VisibleIndex = 10
        Me.BFNQuantity.Width = 101
        '
        'ReposFNQuantity
        '
        Me.ReposFNQuantity.AutoHeight = False
        Me.ReposFNQuantity.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ReposFNQuantity.DisplayFormat.FormatString = "{0:n4}"
        Me.ReposFNQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.ReposFNQuantity.EditFormat.FormatString = "{0:n4}"
        Me.ReposFNQuantity.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.ReposFNQuantity.Name = "ReposFNQuantity"
        Me.ReposFNQuantity.Precision = 4
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
        'FTCmpCode
        '
        Me.FTCmpCode.Caption = "Cmp Code"
        Me.FTCmpCode.FieldName = "FTCmpCode"
        Me.FTCmpCode.Name = "FTCmpCode"
        Me.FTCmpCode.OptionsColumn.AllowEdit = False
        Me.FTCmpCode.OptionsColumn.ReadOnly = True
        Me.FTCmpCode.Visible = True
        Me.FTCmpCode.VisibleIndex = 13
        '
        'ocmok
        '
        Me.ocmok.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmok.Location = New System.Drawing.Point(1053, 4)
        Me.ocmok.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmok.Name = "ocmok"
        Me.ocmok.Size = New System.Drawing.Size(187, 31)
        Me.ocmok.TabIndex = 144
        Me.ocmok.TabStop = False
        Me.ocmok.Tag = "2|"
        Me.ocmok.Text = "Transfer"
        '
        'ocmcancel
        '
        Me.ocmcancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmcancel.Location = New System.Drawing.Point(1053, 39)
        Me.ocmcancel.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmcancel.Name = "ocmcancel"
        Me.ocmcancel.Size = New System.Drawing.Size(187, 31)
        Me.ocmcancel.TabIndex = 145
        Me.ocmcancel.TabStop = False
        Me.ocmcancel.Tag = "2|"
        Me.ocmcancel.Text = "CANCEL"
        '
        'wTransferWHToWHAddBarRcv
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1251, 566)
        Me.Controls.Add(Me.ocmcancel)
        Me.Controls.Add(Me.ocmok)
        Me.Controls.Add(Me.ogcbarcode)
        Me.Controls.Add(Me.ogbnote)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "wTransferWHToWHAddBarRcv"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Receive Auto Transfer To WH"
        CType(Me.ogbnote, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbnote.ResumeLayout(False)
        CType(Me.FNHSysWHId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTReceiveNo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTStaSelectAll.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogcbarcode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvbarcode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepFTSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ReposFNQuantity, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogbnote As DevExpress.XtraEditors.GroupControl
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
    Friend WithEvents ocmok As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmcancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FTSelect As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepFTSelect As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents FTStaSelectAll As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents ocmload As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FTReceiveNo As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FTReceiveNo_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysWHId As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FNHSysCmpIdTo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNOrderType As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTCmpCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFNQuantityOrg As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ReposFNQuantity As DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit
End Class

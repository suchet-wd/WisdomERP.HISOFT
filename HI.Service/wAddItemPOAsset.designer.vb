<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wAddItemPOAsset
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
        Dim SerializableAppearanceObject6 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject7 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.FTAssetCode = New DevExpress.XtraEditors.ButtonEdit()
        Me.FNHSysFixedAssetId_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysUnitId_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysUnitAssetId = New DevExpress.XtraEditors.ButtonEdit()
        Me.FNPrice_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNQuantity_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNQuantity = New DevExpress.XtraEditors.CalcEdit()
        Me.FNPrice = New DevExpress.XtraEditors.CalcEdit()
        Me.FNDisPer = New DevExpress.XtraEditors.CalcEdit()
        Me.FNDisPer_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl5 = New DevExpress.XtraEditors.LabelControl()
        Me.FNNetAmt_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNNetAmt = New DevExpress.XtraEditors.CalcEdit()
        Me.FTRemark_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTRemark = New DevExpress.XtraEditors.MemoEdit()
        Me.ocmadd = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmcancel = New DevExpress.XtraEditors.SimpleButton()
        Me.FNDisAmt = New DevExpress.XtraEditors.CalcEdit()
        Me.FTPurchaseNo = New DevExpress.XtraEditors.TextEdit()
        Me.FNHSysFixedAssetId_None = New DevExpress.XtraEditors.TextEdit()
        Me.FNFixedAssetType = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.FNFixedAssetType_lbl = New DevExpress.XtraEditors.LabelControl()
        CType(Me.FTAssetCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysUnitAssetId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNQuantity.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNPrice.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNDisPer.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNNetAmt.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTRemark.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNDisAmt.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTPurchaseNo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysFixedAssetId_None.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNFixedAssetType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'FTAssetCode
        '
        Me.FTAssetCode.EnterMoveNextControl = True
        Me.FTAssetCode.Location = New System.Drawing.Point(160, 46)
        Me.FTAssetCode.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTAssetCode.Name = "FTAssetCode"
        Me.FTAssetCode.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FTAssetCode.Properties.Appearance.Options.UseBackColor = True
        Me.FTAssetCode.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FTAssetCode.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FTAssetCode.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FTAssetCode.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FTAssetCode.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FTAssetCode.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FTAssetCode.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FTAssetCode.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FTAssetCode.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FTAssetCode.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FTAssetCode.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FTAssetCode.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FTAssetCode.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", 15, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", "476", Nothing, True)})
        Me.FTAssetCode.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.FTAssetCode.Properties.MaxLength = 30
        Me.FTAssetCode.Properties.ReadOnly = True
        Me.FTAssetCode.Size = New System.Drawing.Size(167, 22)
        Me.FTAssetCode.TabIndex = 1
        Me.FTAssetCode.Tag = "2|"
        '
        'FNHSysFixedAssetId_lbl
        '
        Me.FNHSysFixedAssetId_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysFixedAssetId_lbl.Appearance.Options.UseForeColor = True
        Me.FNHSysFixedAssetId_lbl.Appearance.Options.UseTextOptions = True
        Me.FNHSysFixedAssetId_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysFixedAssetId_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysFixedAssetId_lbl.Location = New System.Drawing.Point(14, 47)
        Me.FNHSysFixedAssetId_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysFixedAssetId_lbl.Name = "FNHSysFixedAssetId_lbl"
        Me.FNHSysFixedAssetId_lbl.Size = New System.Drawing.Size(141, 23)
        Me.FNHSysFixedAssetId_lbl.TabIndex = 261
        Me.FNHSysFixedAssetId_lbl.Tag = "2|"
        Me.FNHSysFixedAssetId_lbl.Text = "Assetcode :"
        '
        'FNHSysUnitId_lbl
        '
        Me.FNHSysUnitId_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysUnitId_lbl.Appearance.Options.UseForeColor = True
        Me.FNHSysUnitId_lbl.Appearance.Options.UseTextOptions = True
        Me.FNHSysUnitId_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysUnitId_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysUnitId_lbl.Location = New System.Drawing.Point(50, 75)
        Me.FNHSysUnitId_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysUnitId_lbl.Name = "FNHSysUnitId_lbl"
        Me.FNHSysUnitId_lbl.Size = New System.Drawing.Size(107, 23)
        Me.FNHSysUnitId_lbl.TabIndex = 266
        Me.FNHSysUnitId_lbl.Tag = "2|"
        Me.FNHSysUnitId_lbl.Text = "Unit :"
        '
        'FNHSysUnitAssetId
        '
        Me.FNHSysUnitAssetId.EnterMoveNextControl = True
        Me.FNHSysUnitAssetId.Location = New System.Drawing.Point(160, 75)
        Me.FNHSysUnitAssetId.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysUnitAssetId.Name = "FNHSysUnitAssetId"
        Me.FNHSysUnitAssetId.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FNHSysUnitAssetId.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysUnitAssetId.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysUnitAssetId.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysUnitAssetId.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FNHSysUnitAssetId.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FNHSysUnitAssetId.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNHSysUnitAssetId.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysUnitAssetId.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNHSysUnitAssetId.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNHSysUnitAssetId.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysUnitAssetId.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysUnitAssetId.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNHSysUnitAssetId.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNHSysUnitAssetId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", 15, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject2, "", "449", Nothing, True)})
        Me.FNHSysUnitAssetId.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.FNHSysUnitAssetId.Properties.MaxLength = 30
        Me.FNHSysUnitAssetId.Size = New System.Drawing.Size(167, 22)
        Me.FNHSysUnitAssetId.TabIndex = 7
        Me.FNHSysUnitAssetId.Tag = "2|"
        '
        'FNPrice_lbl
        '
        Me.FNPrice_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNPrice_lbl.Appearance.Options.UseForeColor = True
        Me.FNPrice_lbl.Appearance.Options.UseTextOptions = True
        Me.FNPrice_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNPrice_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNPrice_lbl.Location = New System.Drawing.Point(334, 76)
        Me.FNPrice_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNPrice_lbl.Name = "FNPrice_lbl"
        Me.FNPrice_lbl.Size = New System.Drawing.Size(107, 23)
        Me.FNPrice_lbl.TabIndex = 270
        Me.FNPrice_lbl.Tag = "2|"
        Me.FNPrice_lbl.Text = "Unit Price :"
        '
        'FNQuantity_lbl
        '
        Me.FNQuantity_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNQuantity_lbl.Appearance.Options.UseForeColor = True
        Me.FNQuantity_lbl.Appearance.Options.UseTextOptions = True
        Me.FNQuantity_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNQuantity_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNQuantity_lbl.Location = New System.Drawing.Point(44, 105)
        Me.FNQuantity_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNQuantity_lbl.Name = "FNQuantity_lbl"
        Me.FNQuantity_lbl.Size = New System.Drawing.Size(113, 23)
        Me.FNQuantity_lbl.TabIndex = 271
        Me.FNQuantity_lbl.Tag = "2|"
        Me.FNQuantity_lbl.Text = "Quantity :"
        '
        'FNQuantity
        '
        Me.FNQuantity.EnterMoveNextControl = True
        Me.FNQuantity.Location = New System.Drawing.Point(159, 106)
        Me.FNQuantity.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNQuantity.Name = "FNQuantity"
        Me.FNQuantity.Properties.Appearance.Options.UseTextOptions = True
        Me.FNQuantity.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNQuantity.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNQuantity.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNQuantity.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNQuantity.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNQuantity.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNQuantity.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNQuantity.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNQuantity.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNQuantity.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, True, False, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject3, "", Nothing, Nothing, True)})
        Me.FNQuantity.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNQuantity.Properties.Precision = 0
        Me.FNQuantity.Size = New System.Drawing.Size(168, 22)
        Me.FNQuantity.TabIndex = 10
        Me.FNQuantity.Tag = "2|"
        '
        'FNPrice
        '
        Me.FNPrice.EnterMoveNextControl = True
        Me.FNPrice.Location = New System.Drawing.Point(448, 76)
        Me.FNPrice.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNPrice.Name = "FNPrice"
        Me.FNPrice.Properties.Appearance.Options.UseTextOptions = True
        Me.FNPrice.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNPrice.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNPrice.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNPrice.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNPrice.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNPrice.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNPrice.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNPrice.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNPrice.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNPrice.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, True, False, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject4, "", Nothing, Nothing, True)})
        Me.FNPrice.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNPrice.Properties.Precision = 2
        Me.FNPrice.Size = New System.Drawing.Size(126, 22)
        Me.FNPrice.TabIndex = 8
        Me.FNPrice.Tag = "2|"
        '
        'FNDisPer
        '
        Me.FNDisPer.EnterMoveNextControl = True
        Me.FNDisPer.Location = New System.Drawing.Point(737, 75)
        Me.FNDisPer.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNDisPer.Name = "FNDisPer"
        Me.FNDisPer.Properties.Appearance.Options.UseTextOptions = True
        Me.FNDisPer.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNDisPer.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNDisPer.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNDisPer.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNDisPer.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNDisPer.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNDisPer.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNDisPer.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNDisPer.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNDisPer.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, True, False, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject5, "", Nothing, Nothing, True)})
        Me.FNDisPer.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNDisPer.Properties.MaxLength = 5
        Me.FNDisPer.Properties.Precision = 2
        Me.FNDisPer.Size = New System.Drawing.Size(90, 22)
        Me.FNDisPer.TabIndex = 9
        Me.FNDisPer.Tag = "2|"
        '
        'FNDisPer_lbl
        '
        Me.FNDisPer_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FNDisPer_lbl.Appearance.Options.UseForeColor = True
        Me.FNDisPer_lbl.Appearance.Options.UseTextOptions = True
        Me.FNDisPer_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNDisPer_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNDisPer_lbl.Location = New System.Drawing.Point(623, 75)
        Me.FNDisPer_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNDisPer_lbl.Name = "FNDisPer_lbl"
        Me.FNDisPer_lbl.Size = New System.Drawing.Size(107, 23)
        Me.FNDisPer_lbl.TabIndex = 274
        Me.FNDisPer_lbl.Tag = "2|"
        Me.FNDisPer_lbl.Text = "Discount :"
        '
        'LabelControl5
        '
        Me.LabelControl5.Appearance.ForeColor = System.Drawing.Color.Black
        Me.LabelControl5.Appearance.Options.UseForeColor = True
        Me.LabelControl5.Appearance.Options.UseTextOptions = True
        Me.LabelControl5.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.LabelControl5.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LabelControl5.Location = New System.Drawing.Point(834, 76)
        Me.LabelControl5.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LabelControl5.Name = "LabelControl5"
        Me.LabelControl5.Size = New System.Drawing.Size(40, 23)
        Me.LabelControl5.TabIndex = 276
        Me.LabelControl5.Tag = "2|"
        Me.LabelControl5.Text = "%"
        '
        'FNNetAmt_lbl
        '
        Me.FNNetAmt_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FNNetAmt_lbl.Appearance.Options.UseForeColor = True
        Me.FNNetAmt_lbl.Appearance.Options.UseTextOptions = True
        Me.FNNetAmt_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNNetAmt_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNNetAmt_lbl.Location = New System.Drawing.Point(334, 105)
        Me.FNNetAmt_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNNetAmt_lbl.Name = "FNNetAmt_lbl"
        Me.FNNetAmt_lbl.Size = New System.Drawing.Size(107, 23)
        Me.FNNetAmt_lbl.TabIndex = 278
        Me.FNNetAmt_lbl.Tag = "2|"
        Me.FNNetAmt_lbl.Text = "Net Amount :"
        '
        'FNNetAmt
        '
        Me.FNNetAmt.EnterMoveNextControl = True
        Me.FNNetAmt.Location = New System.Drawing.Point(448, 105)
        Me.FNNetAmt.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNNetAmt.Name = "FNNetAmt"
        Me.FNNetAmt.Properties.Appearance.Options.UseTextOptions = True
        Me.FNNetAmt.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNNetAmt.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNNetAmt.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNNetAmt.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNNetAmt.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNNetAmt.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNNetAmt.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNNetAmt.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNNetAmt.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNNetAmt.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, True, False, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject6, "", Nothing, Nothing, True)})
        Me.FNNetAmt.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNNetAmt.Properties.Precision = 2
        Me.FNNetAmt.Properties.ReadOnly = True
        Me.FNNetAmt.Size = New System.Drawing.Size(126, 22)
        Me.FNNetAmt.TabIndex = 279
        Me.FNNetAmt.Tag = "2|"
        '
        'FTRemark_lbl
        '
        Me.FTRemark_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTRemark_lbl.Appearance.Options.UseForeColor = True
        Me.FTRemark_lbl.Appearance.Options.UseTextOptions = True
        Me.FTRemark_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTRemark_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTRemark_lbl.Location = New System.Drawing.Point(50, 134)
        Me.FTRemark_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTRemark_lbl.Name = "FTRemark_lbl"
        Me.FTRemark_lbl.Size = New System.Drawing.Size(107, 23)
        Me.FTRemark_lbl.TabIndex = 281
        Me.FTRemark_lbl.Tag = "2|"
        Me.FTRemark_lbl.Text = "Remark :"
        '
        'FTRemark
        '
        Me.FTRemark.EditValue = ""
        Me.FTRemark.Location = New System.Drawing.Point(160, 134)
        Me.FTRemark.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTRemark.Name = "FTRemark"
        Me.FTRemark.Properties.MaxLength = 200
        Me.FTRemark.Size = New System.Drawing.Size(705, 128)
        Me.FTRemark.TabIndex = 7
        Me.FTRemark.Tag = "2|"
        '
        'ocmadd
        '
        Me.ocmadd.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.ocmadd.Location = New System.Drawing.Point(157, 274)
        Me.ocmadd.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmadd.Name = "ocmadd"
        Me.ocmadd.Size = New System.Drawing.Size(169, 33)
        Me.ocmadd.TabIndex = 12
        Me.ocmadd.Text = "ADD ASSET"
        '
        'ocmcancel
        '
        Me.ocmcancel.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.ocmcancel.Location = New System.Drawing.Point(695, 274)
        Me.ocmcancel.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmcancel.Name = "ocmcancel"
        Me.ocmcancel.Size = New System.Drawing.Size(169, 33)
        Me.ocmcancel.TabIndex = 13
        Me.ocmcancel.Text = "CANCEL"
        '
        'FNDisAmt
        '
        Me.FNDisAmt.EnterMoveNextControl = True
        Me.FNDisAmt.Location = New System.Drawing.Point(859, 74)
        Me.FNDisAmt.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNDisAmt.Name = "FNDisAmt"
        Me.FNDisAmt.Properties.Appearance.Options.UseTextOptions = True
        Me.FNDisAmt.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNDisAmt.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNDisAmt.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNDisAmt.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNDisAmt.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNDisAmt.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNDisAmt.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNDisAmt.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNDisAmt.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNDisAmt.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, True, False, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject7, "", Nothing, Nothing, True)})
        Me.FNDisAmt.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNDisAmt.Properties.Precision = 5
        Me.FNDisAmt.Size = New System.Drawing.Size(36, 22)
        Me.FNDisAmt.TabIndex = 277
        Me.FNDisAmt.Tag = "2|"
        Me.FNDisAmt.Visible = False
        '
        'FTPurchaseNo
        '
        Me.FTPurchaseNo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FTPurchaseNo.EnterMoveNextControl = True
        Me.FTPurchaseNo.Location = New System.Drawing.Point(897, 209)
        Me.FTPurchaseNo.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTPurchaseNo.Name = "FTPurchaseNo"
        Me.FTPurchaseNo.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FTPurchaseNo.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTPurchaseNo.Properties.Appearance.Options.UseBackColor = True
        Me.FTPurchaseNo.Properties.Appearance.Options.UseForeColor = True
        Me.FTPurchaseNo.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FTPurchaseNo.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FTPurchaseNo.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FTPurchaseNo.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FTPurchaseNo.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FTPurchaseNo.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FTPurchaseNo.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FTPurchaseNo.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FTPurchaseNo.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FTPurchaseNo.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FTPurchaseNo.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FTPurchaseNo.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FTPurchaseNo.Properties.MaxLength = 100
        Me.FTPurchaseNo.Size = New System.Drawing.Size(75, 22)
        Me.FTPurchaseNo.TabIndex = 293
        Me.FTPurchaseNo.TabStop = False
        Me.FTPurchaseNo.Tag = "2|"
        Me.FTPurchaseNo.Visible = False
        '
        'FNHSysFixedAssetId_None
        '
        Me.FNHSysFixedAssetId_None.EnterMoveNextControl = True
        Me.FNHSysFixedAssetId_None.Location = New System.Drawing.Point(334, 46)
        Me.FNHSysFixedAssetId_None.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysFixedAssetId_None.Name = "FNHSysFixedAssetId_None"
        Me.FNHSysFixedAssetId_None.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FNHSysFixedAssetId_None.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FNHSysFixedAssetId_None.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysFixedAssetId_None.Properties.Appearance.Options.UseForeColor = True
        Me.FNHSysFixedAssetId_None.Properties.Appearance.Options.UseTextOptions = True
        Me.FNHSysFixedAssetId_None.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.FNHSysFixedAssetId_None.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysFixedAssetId_None.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysFixedAssetId_None.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FNHSysFixedAssetId_None.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FNHSysFixedAssetId_None.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNHSysFixedAssetId_None.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysFixedAssetId_None.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNHSysFixedAssetId_None.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNHSysFixedAssetId_None.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysFixedAssetId_None.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysFixedAssetId_None.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNHSysFixedAssetId_None.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNHSysFixedAssetId_None.Properties.ReadOnly = True
        Me.FNHSysFixedAssetId_None.Size = New System.Drawing.Size(530, 22)
        Me.FNHSysFixedAssetId_None.TabIndex = 2
        Me.FNHSysFixedAssetId_None.TabStop = False
        Me.FNHSysFixedAssetId_None.Tag = "2|"
        '
        'FNFixedAssetType
        '
        Me.FNFixedAssetType.Location = New System.Drawing.Point(160, 16)
        Me.FNFixedAssetType.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNFixedAssetType.Name = "FNFixedAssetType"
        Me.FNFixedAssetType.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FNFixedAssetType.Properties.Tag = "FNFixedAssetType"
        Me.FNFixedAssetType.Size = New System.Drawing.Size(167, 22)
        Me.FNFixedAssetType.TabIndex = 484
        '
        'FNFixedAssetType_lbl
        '
        Me.FNFixedAssetType_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNFixedAssetType_lbl.Appearance.Options.UseForeColor = True
        Me.FNFixedAssetType_lbl.Appearance.Options.UseTextOptions = True
        Me.FNFixedAssetType_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNFixedAssetType_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNFixedAssetType_lbl.Location = New System.Drawing.Point(15, 16)
        Me.FNFixedAssetType_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNFixedAssetType_lbl.Name = "FNFixedAssetType_lbl"
        Me.FNFixedAssetType_lbl.Size = New System.Drawing.Size(145, 23)
        Me.FNFixedAssetType_lbl.TabIndex = 483
        Me.FNFixedAssetType_lbl.Tag = "2|"
        Me.FNFixedAssetType_lbl.Text = "Asset Type :"
        '
        'wAddItemPOAsset
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(996, 329)
        Me.ControlBox = False
        Me.Controls.Add(Me.FNFixedAssetType)
        Me.Controls.Add(Me.FNFixedAssetType_lbl)
        Me.Controls.Add(Me.FTPurchaseNo)
        Me.Controls.Add(Me.ocmcancel)
        Me.Controls.Add(Me.ocmadd)
        Me.Controls.Add(Me.FTRemark_lbl)
        Me.Controls.Add(Me.FTRemark)
        Me.Controls.Add(Me.FNNetAmt)
        Me.Controls.Add(Me.FNNetAmt_lbl)
        Me.Controls.Add(Me.FNDisAmt)
        Me.Controls.Add(Me.LabelControl5)
        Me.Controls.Add(Me.FNDisPer)
        Me.Controls.Add(Me.FNDisPer_lbl)
        Me.Controls.Add(Me.FNPrice)
        Me.Controls.Add(Me.FNQuantity)
        Me.Controls.Add(Me.FNQuantity_lbl)
        Me.Controls.Add(Me.FNPrice_lbl)
        Me.Controls.Add(Me.FNHSysUnitId_lbl)
        Me.Controls.Add(Me.FNHSysUnitAssetId)
        Me.Controls.Add(Me.FNHSysFixedAssetId_lbl)
        Me.Controls.Add(Me.FNHSysFixedAssetId_None)
        Me.Controls.Add(Me.FTAssetCode)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "wAddItemPOAsset"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Add Item"
        CType(Me.FTAssetCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysUnitAssetId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNQuantity.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNPrice.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNDisPer.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNNetAmt.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTRemark.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNDisAmt.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTPurchaseNo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysFixedAssetId_None.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNFixedAssetType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents FTAssetCode As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FNHSysFixedAssetId_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysUnitId_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysUnitAssetId As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FNPrice_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNQuantity_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNQuantity As DevExpress.XtraEditors.CalcEdit
    Friend WithEvents FNPrice As DevExpress.XtraEditors.CalcEdit
    Friend WithEvents FNDisPer As DevExpress.XtraEditors.CalcEdit
    Friend WithEvents FNDisPer_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl5 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNNetAmt_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNNetAmt As DevExpress.XtraEditors.CalcEdit
    Friend WithEvents FTRemark_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTRemark As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents ocmadd As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmcancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FNDisAmt As DevExpress.XtraEditors.CalcEdit
    Friend WithEvents FTPurchaseNo As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FNHSysFixedAssetId_None As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FNFixedAssetType As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents FNFixedAssetType_lbl As DevExpress.XtraEditors.LabelControl

End Class

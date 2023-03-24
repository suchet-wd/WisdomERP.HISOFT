<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wAddItemPRServiceAsset
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
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject4 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject5 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject6 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject7 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.ocmadd = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmcancel = New DevExpress.XtraEditors.SimpleButton()
        Me.FTDescription = New DevExpress.XtraEditors.MemoEdit()
        Me.FNAmount = New DevExpress.XtraEditors.CalcEdit()
        Me.FNNetAmt_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNPrice = New DevExpress.XtraEditors.CalcEdit()
        Me.FNQuantity = New DevExpress.XtraEditors.CalcEdit()
        Me.FNQuantity_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNPrice_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTNote = New DevExpress.XtraEditors.MemoEdit()
        Me.FTDescription_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTNote_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysFixedAssetId_None = New DevExpress.XtraEditors.TextEdit()
        Me.FNHSysFixedAssetId_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTAssetCode = New DevExpress.XtraEditors.ButtonEdit()
        Me.FNDisAmt_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNDisAmt = New DevExpress.XtraEditors.CalcEdit()
        Me.FNDisPer_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNDisPer = New DevExpress.XtraEditors.CalcEdit()
        Me.FNHSysUnitId_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysUnitAssetId = New DevExpress.XtraEditors.ButtonEdit()
        Me.FNFixedAssetType = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.FNFixedAssetType_lbl = New DevExpress.XtraEditors.LabelControl()
        CType(Me.FTDescription.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNAmount.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNPrice.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNQuantity.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTNote.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysFixedAssetId_None.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTAssetCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNDisAmt.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNDisPer.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysUnitAssetId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNFixedAssetType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ocmadd
        '
        Me.ocmadd.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.ocmadd.Location = New System.Drawing.Point(157, 409)
        Me.ocmadd.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmadd.Name = "ocmadd"
        Me.ocmadd.Size = New System.Drawing.Size(169, 33)
        Me.ocmadd.TabIndex = 12
        Me.ocmadd.Text = "ADD ASSET"
        '
        'ocmcancel
        '
        Me.ocmcancel.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.ocmcancel.Location = New System.Drawing.Point(695, 409)
        Me.ocmcancel.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmcancel.Name = "ocmcancel"
        Me.ocmcancel.Size = New System.Drawing.Size(169, 33)
        Me.ocmcancel.TabIndex = 13
        Me.ocmcancel.Text = "CANCEL"
        '
        'FTDescription
        '
        Me.FTDescription.EditValue = ""
        Me.FTDescription.Location = New System.Drawing.Point(160, 80)
        Me.FTDescription.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTDescription.Name = "FTDescription"
        Me.FTDescription.Properties.MaxLength = 500
        Me.FTDescription.Size = New System.Drawing.Size(702, 161)
        Me.FTDescription.TabIndex = 286
        Me.FTDescription.Tag = "2|"
        '
        'FNAmount
        '
        Me.FNAmount.EnterMoveNextControl = True
        Me.FNAmount.Location = New System.Drawing.Point(736, 284)
        Me.FNAmount.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNAmount.Name = "FNAmount"
        Me.FNAmount.Properties.Appearance.Options.UseTextOptions = True
        Me.FNAmount.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNAmount.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNAmount.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNAmount.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNAmount.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNAmount.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNAmount.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNAmount.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNAmount.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNAmount.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, True, False, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", Nothing, Nothing, True)})
        Me.FNAmount.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNAmount.Properties.Precision = 2
        Me.FNAmount.Properties.ReadOnly = True
        Me.FNAmount.Size = New System.Drawing.Size(126, 23)
        Me.FNAmount.TabIndex = 292
        Me.FNAmount.Tag = "2|"
        '
        'FNNetAmt_lbl
        '
        Me.FNNetAmt_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FNNetAmt_lbl.Appearance.Options.UseForeColor = True
        Me.FNNetAmt_lbl.Appearance.Options.UseTextOptions = True
        Me.FNNetAmt_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNNetAmt_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNNetAmt_lbl.Location = New System.Drawing.Point(622, 284)
        Me.FNNetAmt_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNNetAmt_lbl.Name = "FNNetAmt_lbl"
        Me.FNNetAmt_lbl.Size = New System.Drawing.Size(107, 23)
        Me.FNNetAmt_lbl.TabIndex = 291
        Me.FNNetAmt_lbl.Tag = "2|"
        Me.FNNetAmt_lbl.Text = "Net Amount :"
        '
        'FNPrice
        '
        Me.FNPrice.EnterMoveNextControl = True
        Me.FNPrice.Location = New System.Drawing.Point(464, 252)
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
        Me.FNPrice.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, True, False, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject2, "", Nothing, Nothing, True)})
        Me.FNPrice.Properties.EditFormat.FormatString = "{n:2}"
        Me.FNPrice.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNPrice.Properties.Precision = 2
        Me.FNPrice.Size = New System.Drawing.Size(164, 23)
        Me.FNPrice.TabIndex = 288
        Me.FNPrice.Tag = "2|"
        '
        'FNQuantity
        '
        Me.FNQuantity.EnterMoveNextControl = True
        Me.FNQuantity.Location = New System.Drawing.Point(160, 252)
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
        Me.FNQuantity.Size = New System.Drawing.Size(167, 23)
        Me.FNQuantity.TabIndex = 287
        Me.FNQuantity.Tag = "2|"
        '
        'FNQuantity_lbl
        '
        Me.FNQuantity_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNQuantity_lbl.Appearance.Options.UseForeColor = True
        Me.FNQuantity_lbl.Appearance.Options.UseTextOptions = True
        Me.FNQuantity_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNQuantity_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNQuantity_lbl.Location = New System.Drawing.Point(43, 251)
        Me.FNQuantity_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNQuantity_lbl.Name = "FNQuantity_lbl"
        Me.FNQuantity_lbl.Size = New System.Drawing.Size(112, 23)
        Me.FNQuantity_lbl.TabIndex = 290
        Me.FNQuantity_lbl.Tag = "2|"
        Me.FNQuantity_lbl.Text = "Quantity :"
        '
        'FNPrice_lbl
        '
        Me.FNPrice_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNPrice_lbl.Appearance.Options.UseForeColor = True
        Me.FNPrice_lbl.Appearance.Options.UseTextOptions = True
        Me.FNPrice_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNPrice_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNPrice_lbl.Location = New System.Drawing.Point(351, 252)
        Me.FNPrice_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNPrice_lbl.Name = "FNPrice_lbl"
        Me.FNPrice_lbl.Size = New System.Drawing.Size(107, 23)
        Me.FNPrice_lbl.TabIndex = 289
        Me.FNPrice_lbl.Tag = "2|"
        Me.FNPrice_lbl.Text = "Unit Price :"
        '
        'FTNote
        '
        Me.FTNote.Location = New System.Drawing.Point(160, 320)
        Me.FTNote.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTNote.Name = "FTNote"
        Me.FTNote.Size = New System.Drawing.Size(702, 76)
        Me.FTNote.TabIndex = 293
        Me.FTNote.Tag = "2|"
        '
        'FTDescription_lbl
        '
        Me.FTDescription_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTDescription_lbl.Appearance.Options.UseForeColor = True
        Me.FTDescription_lbl.Appearance.Options.UseTextOptions = True
        Me.FTDescription_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTDescription_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTDescription_lbl.Location = New System.Drawing.Point(44, 78)
        Me.FTDescription_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTDescription_lbl.Name = "FTDescription_lbl"
        Me.FTDescription_lbl.Size = New System.Drawing.Size(113, 23)
        Me.FTDescription_lbl.TabIndex = 294
        Me.FTDescription_lbl.Tag = "2|"
        Me.FTDescription_lbl.Text = "FTDescripton"
        '
        'FTNote_lbl
        '
        Me.FTNote_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTNote_lbl.Appearance.Options.UseForeColor = True
        Me.FTNote_lbl.Appearance.Options.UseTextOptions = True
        Me.FTNote_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTNote_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTNote_lbl.Location = New System.Drawing.Point(43, 321)
        Me.FTNote_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTNote_lbl.Name = "FTNote_lbl"
        Me.FTNote_lbl.Size = New System.Drawing.Size(112, 23)
        Me.FTNote_lbl.TabIndex = 295
        Me.FTNote_lbl.Tag = "2|"
        Me.FTNote_lbl.Text = "FTNote"
        '
        'FNHSysFixedAssetId_None
        '
        Me.FNHSysFixedAssetId_None.Location = New System.Drawing.Point(333, 49)
        Me.FNHSysFixedAssetId_None.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysFixedAssetId_None.Name = "FNHSysFixedAssetId_None"
        Me.FNHSysFixedAssetId_None.Properties.Appearance.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysFixedAssetId_None.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysFixedAssetId_None.Properties.ReadOnly = True
        Me.FNHSysFixedAssetId_None.Size = New System.Drawing.Size(531, 23)
        Me.FNHSysFixedAssetId_None.TabIndex = 298
        Me.FNHSysFixedAssetId_None.Tag = "2|"
        '
        'FNHSysFixedAssetId_lbl
        '
        Me.FNHSysFixedAssetId_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysFixedAssetId_lbl.Appearance.Options.UseForeColor = True
        Me.FNHSysFixedAssetId_lbl.Appearance.Options.UseTextOptions = True
        Me.FNHSysFixedAssetId_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysFixedAssetId_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysFixedAssetId_lbl.Location = New System.Drawing.Point(14, 49)
        Me.FNHSysFixedAssetId_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysFixedAssetId_lbl.Name = "FNHSysFixedAssetId_lbl"
        Me.FNHSysFixedAssetId_lbl.Size = New System.Drawing.Size(143, 23)
        Me.FNHSysFixedAssetId_lbl.TabIndex = 297
        Me.FNHSysFixedAssetId_lbl.Tag = "2|"
        Me.FNHSysFixedAssetId_lbl.Text = "Assetcode :"
        '
        'FTAssetCode
        '
        Me.FTAssetCode.EnterMoveNextControl = True
        Me.FTAssetCode.Location = New System.Drawing.Point(159, 49)
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
        Me.FTAssetCode.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", 15, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject4, "", "415", Nothing, True)})
        Me.FTAssetCode.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.FTAssetCode.Properties.MaxLength = 30
        Me.FTAssetCode.Size = New System.Drawing.Size(167, 23)
        Me.FTAssetCode.TabIndex = 296
        Me.FTAssetCode.Tag = "2|"
        '
        'FNDisAmt_lbl
        '
        Me.FNDisAmt_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FNDisAmt_lbl.Appearance.Options.UseForeColor = True
        Me.FNDisAmt_lbl.Appearance.Options.UseTextOptions = True
        Me.FNDisAmt_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNDisAmt_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNDisAmt_lbl.Location = New System.Drawing.Point(318, 284)
        Me.FNDisAmt_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNDisAmt_lbl.Name = "FNDisAmt_lbl"
        Me.FNDisAmt_lbl.Size = New System.Drawing.Size(143, 23)
        Me.FNDisAmt_lbl.TabIndex = 302
        Me.FNDisAmt_lbl.Tag = "2|"
        Me.FNDisAmt_lbl.Text = "FNDisAmt :"
        '
        'FNDisAmt
        '
        Me.FNDisAmt.EnterMoveNextControl = True
        Me.FNDisAmt.Location = New System.Drawing.Point(464, 284)
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
        Me.FNDisAmt.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, True, False, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject5, "", Nothing, Nothing, True)})
        Me.FNDisAmt.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNDisAmt.Properties.Precision = 2
        Me.FNDisAmt.Size = New System.Drawing.Size(164, 23)
        Me.FNDisAmt.TabIndex = 299
        Me.FNDisAmt.Tag = "2|"
        '
        'FNDisPer_lbl
        '
        Me.FNDisPer_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FNDisPer_lbl.Appearance.Options.UseForeColor = True
        Me.FNDisPer_lbl.Appearance.Options.UseTextOptions = True
        Me.FNDisPer_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNDisPer_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNDisPer_lbl.Location = New System.Drawing.Point(30, 283)
        Me.FNDisPer_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNDisPer_lbl.Name = "FNDisPer_lbl"
        Me.FNDisPer_lbl.Size = New System.Drawing.Size(127, 23)
        Me.FNDisPer_lbl.TabIndex = 301
        Me.FNDisPer_lbl.Tag = "2|"
        Me.FNDisPer_lbl.Text = "FNDisPer :"
        '
        'FNDisPer
        '
        Me.FNDisPer.EnterMoveNextControl = True
        Me.FNDisPer.Location = New System.Drawing.Point(160, 285)
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
        Me.FNDisPer.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, True, False, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject6, "", Nothing, Nothing, True)})
        Me.FNDisPer.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNDisPer.Properties.Precision = 2
        Me.FNDisPer.Size = New System.Drawing.Size(167, 23)
        Me.FNDisPer.TabIndex = 300
        Me.FNDisPer.Tag = "2|"
        '
        'FNHSysUnitId_lbl
        '
        Me.FNHSysUnitId_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysUnitId_lbl.Appearance.Options.UseForeColor = True
        Me.FNHSysUnitId_lbl.Appearance.Options.UseTextOptions = True
        Me.FNHSysUnitId_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysUnitId_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysUnitId_lbl.Location = New System.Drawing.Point(634, 254)
        Me.FNHSysUnitId_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysUnitId_lbl.Name = "FNHSysUnitId_lbl"
        Me.FNHSysUnitId_lbl.Size = New System.Drawing.Size(95, 23)
        Me.FNHSysUnitId_lbl.TabIndex = 304
        Me.FNHSysUnitId_lbl.Tag = "2|"
        Me.FNHSysUnitId_lbl.Text = "Unit :"
        '
        'FNHSysUnitAssetId
        '
        Me.FNHSysUnitAssetId.EnterMoveNextControl = True
        Me.FNHSysUnitAssetId.Location = New System.Drawing.Point(736, 254)
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
        Me.FNHSysUnitAssetId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", 15, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject7, "", "449", Nothing, True)})
        Me.FNHSysUnitAssetId.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.FNHSysUnitAssetId.Properties.MaxLength = 30
        Me.FNHSysUnitAssetId.Size = New System.Drawing.Size(126, 23)
        Me.FNHSysUnitAssetId.TabIndex = 303
        Me.FNHSysUnitAssetId.Tag = "2|"
        '
        'FNFixedAssetType
        '
        Me.FNFixedAssetType.Location = New System.Drawing.Point(160, 18)
        Me.FNFixedAssetType.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNFixedAssetType.Name = "FNFixedAssetType"
        Me.FNFixedAssetType.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FNFixedAssetType.Properties.Tag = "FNFixedAssetType"
        Me.FNFixedAssetType.Size = New System.Drawing.Size(167, 23)
        Me.FNFixedAssetType.TabIndex = 484
        '
        'FNFixedAssetType_lbl
        '
        Me.FNFixedAssetType_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNFixedAssetType_lbl.Appearance.Options.UseForeColor = True
        Me.FNFixedAssetType_lbl.Appearance.Options.UseTextOptions = True
        Me.FNFixedAssetType_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNFixedAssetType_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNFixedAssetType_lbl.Location = New System.Drawing.Point(15, 18)
        Me.FNFixedAssetType_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNFixedAssetType_lbl.Name = "FNFixedAssetType_lbl"
        Me.FNFixedAssetType_lbl.Size = New System.Drawing.Size(145, 23)
        Me.FNFixedAssetType_lbl.TabIndex = 483
        Me.FNFixedAssetType_lbl.Tag = "2|"
        Me.FNFixedAssetType_lbl.Text = "Asset Type :"
        '
        'wAddItemPRServiceAsset
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(917, 457)
        Me.ControlBox = False
        Me.Controls.Add(Me.FNFixedAssetType)
        Me.Controls.Add(Me.FNFixedAssetType_lbl)
        Me.Controls.Add(Me.FNHSysUnitId_lbl)
        Me.Controls.Add(Me.FNHSysUnitAssetId)
        Me.Controls.Add(Me.FNDisAmt_lbl)
        Me.Controls.Add(Me.FNDisAmt)
        Me.Controls.Add(Me.FNDisPer_lbl)
        Me.Controls.Add(Me.FNDisPer)
        Me.Controls.Add(Me.FNHSysFixedAssetId_None)
        Me.Controls.Add(Me.FNHSysFixedAssetId_lbl)
        Me.Controls.Add(Me.FTAssetCode)
        Me.Controls.Add(Me.FTNote_lbl)
        Me.Controls.Add(Me.FTDescription_lbl)
        Me.Controls.Add(Me.FTNote)
        Me.Controls.Add(Me.FTDescription)
        Me.Controls.Add(Me.FNAmount)
        Me.Controls.Add(Me.FNNetAmt_lbl)
        Me.Controls.Add(Me.FNPrice)
        Me.Controls.Add(Me.FNQuantity)
        Me.Controls.Add(Me.FNQuantity_lbl)
        Me.Controls.Add(Me.FNPrice_lbl)
        Me.Controls.Add(Me.ocmcancel)
        Me.Controls.Add(Me.ocmadd)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "wAddItemPRServiceAsset"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Add Item"
        CType(Me.FTDescription.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNAmount.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNPrice.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNQuantity.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTNote.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysFixedAssetId_None.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTAssetCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNDisAmt.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNDisPer.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysUnitAssetId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNFixedAssetType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ocmadd As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmcancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FTDescription As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents FNAmount As DevExpress.XtraEditors.CalcEdit
    Friend WithEvents FNNetAmt_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNPrice As DevExpress.XtraEditors.CalcEdit
    Friend WithEvents FNQuantity As DevExpress.XtraEditors.CalcEdit
    Friend WithEvents FNQuantity_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNPrice_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTNote As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents FTDescription_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTNote_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysFixedAssetId_None As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FNHSysFixedAssetId_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTAssetCode As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FNDisAmt_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNDisAmt As DevExpress.XtraEditors.CalcEdit
    Friend WithEvents FNDisPer_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNDisPer As DevExpress.XtraEditors.CalcEdit
    Friend WithEvents FNHSysUnitId_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysUnitAssetId As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FNFixedAssetType As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents FNFixedAssetType_lbl As DevExpress.XtraEditors.LabelControl
End Class

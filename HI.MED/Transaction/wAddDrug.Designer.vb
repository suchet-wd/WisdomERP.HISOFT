<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wAddDrug
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
        Me.FNHSysDrugId_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysDrugId = New DevExpress.XtraEditors.ButtonEdit()
        Me.FNHSysDrugId_None = New DevExpress.XtraEditors.TextEdit()
        Me.ocmcancel = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmadd = New DevExpress.XtraEditors.SimpleButton()
        Me.FTRemark_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTRemark = New DevExpress.XtraEditors.MemoEdit()
        Me.FNNetAmt = New DevExpress.XtraEditors.CalcEdit()
        Me.FNNetAmt_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNDisAmt = New DevExpress.XtraEditors.CalcEdit()
        Me.LabelControl5 = New DevExpress.XtraEditors.LabelControl()
        Me.FNDisPer = New DevExpress.XtraEditors.CalcEdit()
        Me.FNDisPer_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNPOPrice = New DevExpress.XtraEditors.CalcEdit()
        Me.FNPOQuantity = New DevExpress.XtraEditors.CalcEdit()
        Me.FNPOQuantity_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNPOPrice_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysDrugUnitId_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysDrugUnitId = New DevExpress.XtraEditors.ButtonEdit()
        CType(Me.FNHSysDrugId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysDrugId_None.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTRemark.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNNetAmt.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNDisAmt.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNDisPer.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNPOPrice.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNPOQuantity.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysDrugUnitId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'FNHSysDrugId_lbl
        '
        Me.FNHSysDrugId_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysDrugId_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysDrugId_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysDrugId_lbl.Location = New System.Drawing.Point(25, 12)
        Me.FNHSysDrugId_lbl.Name = "FNHSysDrugId_lbl"
        Me.FNHSysDrugId_lbl.Size = New System.Drawing.Size(101, 19)
        Me.FNHSysDrugId_lbl.TabIndex = 271
        Me.FNHSysDrugId_lbl.Tag = "2|"
        Me.FNHSysDrugId_lbl.Text = "Drug Code :"
        '
        'FNHSysDrugId
        '
        Me.FNHSysDrugId.EnterMoveNextControl = True
        Me.FNHSysDrugId.Location = New System.Drawing.Point(128, 12)
        Me.FNHSysDrugId.Name = "FNHSysDrugId"
        Me.FNHSysDrugId.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FNHSysDrugId.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysDrugId.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysDrugId.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysDrugId.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FNHSysDrugId.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FNHSysDrugId.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNHSysDrugId.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysDrugId.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNHSysDrugId.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNHSysDrugId.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysDrugId.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysDrugId.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNHSysDrugId.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNHSysDrugId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", 15, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", "263", Nothing, True)})
        Me.FNHSysDrugId.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.FNHSysDrugId.Properties.MaxLength = 30
        Me.FNHSysDrugId.Size = New System.Drawing.Size(143, 20)
        Me.FNHSysDrugId.TabIndex = 270
        Me.FNHSysDrugId.Tag = "2|"
        '
        'FNHSysDrugId_None
        '
        Me.FNHSysDrugId_None.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FNHSysDrugId_None.EnterMoveNextControl = True
        Me.FNHSysDrugId_None.Location = New System.Drawing.Point(277, 12)
        Me.FNHSysDrugId_None.Name = "FNHSysDrugId_None"
        Me.FNHSysDrugId_None.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FNHSysDrugId_None.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FNHSysDrugId_None.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysDrugId_None.Properties.Appearance.Options.UseForeColor = True
        Me.FNHSysDrugId_None.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysDrugId_None.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysDrugId_None.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FNHSysDrugId_None.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FNHSysDrugId_None.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNHSysDrugId_None.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysDrugId_None.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNHSysDrugId_None.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNHSysDrugId_None.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysDrugId_None.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysDrugId_None.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNHSysDrugId_None.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNHSysDrugId_None.Properties.ReadOnly = True
        Me.FNHSysDrugId_None.Size = New System.Drawing.Size(455, 20)
        Me.FNHSysDrugId_None.TabIndex = 273
        Me.FNHSysDrugId_None.TabStop = False
        Me.FNHSysDrugId_None.Tag = "2|"
        '
        'ocmcancel
        '
        Me.ocmcancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ocmcancel.Location = New System.Drawing.Point(514, 227)
        Me.ocmcancel.Name = "ocmcancel"
        Me.ocmcancel.Size = New System.Drawing.Size(145, 27)
        Me.ocmcancel.TabIndex = 299
        Me.ocmcancel.Text = "CANCEL"
        '
        'ocmadd
        '
        Me.ocmadd.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ocmadd.Location = New System.Drawing.Point(127, 227)
        Me.ocmadd.Name = "ocmadd"
        Me.ocmadd.Size = New System.Drawing.Size(145, 27)
        Me.ocmadd.TabIndex = 289
        Me.ocmadd.Text = "ADD MATERIAL"
        '
        'FTRemark_lbl
        '
        Me.FTRemark_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTRemark_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTRemark_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTRemark_lbl.Location = New System.Drawing.Point(34, 86)
        Me.FTRemark_lbl.Name = "FTRemark_lbl"
        Me.FTRemark_lbl.Size = New System.Drawing.Size(92, 19)
        Me.FTRemark_lbl.TabIndex = 298
        Me.FTRemark_lbl.Tag = "2|"
        Me.FTRemark_lbl.Text = "Remark :"
        '
        'FTRemark
        '
        Me.FTRemark.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.FTRemark.EditValue = ""
        Me.FTRemark.Location = New System.Drawing.Point(128, 86)
        Me.FTRemark.Name = "FTRemark"
        Me.FTRemark.Properties.MaxLength = 200
        Me.FTRemark.Size = New System.Drawing.Size(604, 135)
        Me.FTRemark.TabIndex = 288
        Me.FTRemark.Tag = "2|"
        Me.FTRemark.UseOptimizedRendering = True
        '
        'FNNetAmt
        '
        Me.FNNetAmt.EnterMoveNextControl = True
        Me.FNNetAmt.Location = New System.Drawing.Point(375, 61)
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
        Me.FNNetAmt.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, True, False, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject2, "", Nothing, Nothing, True)})
        Me.FNNetAmt.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNNetAmt.Properties.Precision = 2
        Me.FNNetAmt.Properties.ReadOnly = True
        Me.FNNetAmt.Size = New System.Drawing.Size(108, 20)
        Me.FNNetAmt.TabIndex = 297
        Me.FNNetAmt.Tag = "2|"
        '
        'FNNetAmt_lbl
        '
        Me.FNNetAmt_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FNNetAmt_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNNetAmt_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNNetAmt_lbl.Location = New System.Drawing.Point(277, 61)
        Me.FNNetAmt_lbl.Name = "FNNetAmt_lbl"
        Me.FNNetAmt_lbl.Size = New System.Drawing.Size(92, 19)
        Me.FNNetAmt_lbl.TabIndex = 296
        Me.FNNetAmt_lbl.Tag = "2|"
        Me.FNNetAmt_lbl.Text = "Net Amount :"
        '
        'FNDisAmt
        '
        Me.FNDisAmt.EnterMoveNextControl = True
        Me.FNDisAmt.Location = New System.Drawing.Point(727, 36)
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
        Me.FNDisAmt.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, True, False, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject3, "", Nothing, Nothing, True)})
        Me.FNDisAmt.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNDisAmt.Properties.Precision = 5
        Me.FNDisAmt.Size = New System.Drawing.Size(31, 20)
        Me.FNDisAmt.TabIndex = 295
        Me.FNDisAmt.Tag = "2|"
        Me.FNDisAmt.Visible = False
        '
        'LabelControl5
        '
        Me.LabelControl5.Appearance.ForeColor = System.Drawing.Color.Black
        Me.LabelControl5.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.LabelControl5.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LabelControl5.Location = New System.Drawing.Point(706, 38)
        Me.LabelControl5.Name = "LabelControl5"
        Me.LabelControl5.Size = New System.Drawing.Size(34, 19)
        Me.LabelControl5.TabIndex = 294
        Me.LabelControl5.Tag = "2|"
        Me.LabelControl5.Text = "%"
        '
        'FNDisPer
        '
        Me.FNDisPer.EnterMoveNextControl = True
        Me.FNDisPer.Location = New System.Drawing.Point(623, 37)
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
        Me.FNDisPer.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, True, False, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject4, "", Nothing, Nothing, True)})
        Me.FNDisPer.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNDisPer.Properties.MaxLength = 5
        Me.FNDisPer.Properties.Precision = 2
        Me.FNDisPer.Size = New System.Drawing.Size(77, 20)
        Me.FNDisPer.TabIndex = 285
        Me.FNDisPer.Tag = "2|"
        '
        'FNDisPer_lbl
        '
        Me.FNDisPer_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FNDisPer_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNDisPer_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNDisPer_lbl.Location = New System.Drawing.Point(525, 37)
        Me.FNDisPer_lbl.Name = "FNDisPer_lbl"
        Me.FNDisPer_lbl.Size = New System.Drawing.Size(92, 19)
        Me.FNDisPer_lbl.TabIndex = 293
        Me.FNDisPer_lbl.Tag = "2|"
        Me.FNDisPer_lbl.Text = "Discount :"
        '
        'FNPOPrice
        '
        Me.FNPOPrice.EnterMoveNextControl = True
        Me.FNPOPrice.Location = New System.Drawing.Point(375, 38)
        Me.FNPOPrice.Name = "FNPOPrice"
        Me.FNPOPrice.Properties.Appearance.Options.UseTextOptions = True
        Me.FNPOPrice.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNPOPrice.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNPOPrice.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNPOPrice.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNPOPrice.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNPOPrice.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNPOPrice.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNPOPrice.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNPOPrice.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNPOPrice.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, True, False, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject5, "", Nothing, Nothing, True)})
        Me.FNPOPrice.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNPOPrice.Properties.Precision = 5
        Me.FNPOPrice.Size = New System.Drawing.Size(108, 20)
        Me.FNPOPrice.TabIndex = 286
        Me.FNPOPrice.Tag = "2|"
        '
        'FNPOQuantity
        '
        Me.FNPOQuantity.EnterMoveNextControl = True
        Me.FNPOQuantity.Location = New System.Drawing.Point(127, 62)
        Me.FNPOQuantity.Name = "FNPOQuantity"
        Me.FNPOQuantity.Properties.Appearance.Options.UseTextOptions = True
        Me.FNPOQuantity.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNPOQuantity.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNPOQuantity.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNPOQuantity.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNPOQuantity.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNPOQuantity.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNPOQuantity.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNPOQuantity.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNPOQuantity.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNPOQuantity.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, True, False, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject6, "", Nothing, Nothing, True)})
        Me.FNPOQuantity.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNPOQuantity.Properties.Precision = 4
        Me.FNPOQuantity.Size = New System.Drawing.Size(144, 20)
        Me.FNPOQuantity.TabIndex = 287
        Me.FNPOQuantity.Tag = "2|"
        '
        'FNPOQuantity_lbl
        '
        Me.FNPOQuantity_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNPOQuantity_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNPOQuantity_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNPOQuantity_lbl.Location = New System.Drawing.Point(29, 61)
        Me.FNPOQuantity_lbl.Name = "FNPOQuantity_lbl"
        Me.FNPOQuantity_lbl.Size = New System.Drawing.Size(97, 19)
        Me.FNPOQuantity_lbl.TabIndex = 292
        Me.FNPOQuantity_lbl.Tag = "2|"
        Me.FNPOQuantity_lbl.Text = "Quantity :"
        '
        'FNPOPrice_lbl
        '
        Me.FNPOPrice_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNPOPrice_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNPOPrice_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNPOPrice_lbl.Location = New System.Drawing.Point(277, 38)
        Me.FNPOPrice_lbl.Name = "FNPOPrice_lbl"
        Me.FNPOPrice_lbl.Size = New System.Drawing.Size(92, 19)
        Me.FNPOPrice_lbl.TabIndex = 291
        Me.FNPOPrice_lbl.Tag = "2|"
        Me.FNPOPrice_lbl.Text = "Unit Price :"
        '
        'FNHSysDrugUnitId_lbl
        '
        Me.FNHSysDrugUnitId_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysDrugUnitId_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysDrugUnitId_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysDrugUnitId_lbl.Location = New System.Drawing.Point(34, 37)
        Me.FNHSysDrugUnitId_lbl.Name = "FNHSysDrugUnitId_lbl"
        Me.FNHSysDrugUnitId_lbl.Size = New System.Drawing.Size(92, 19)
        Me.FNHSysDrugUnitId_lbl.TabIndex = 290
        Me.FNHSysDrugUnitId_lbl.Tag = "2|"
        Me.FNHSysDrugUnitId_lbl.Text = "Unit :"
        '
        'FNHSysDrugUnitId
        '
        Me.FNHSysDrugUnitId.EnterMoveNextControl = True
        Me.FNHSysDrugUnitId.Location = New System.Drawing.Point(128, 37)
        Me.FNHSysDrugUnitId.Name = "FNHSysDrugUnitId"
        Me.FNHSysDrugUnitId.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FNHSysDrugUnitId.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysDrugUnitId.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysDrugUnitId.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysDrugUnitId.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FNHSysDrugUnitId.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FNHSysDrugUnitId.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNHSysDrugUnitId.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysDrugUnitId.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNHSysDrugUnitId.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNHSysDrugUnitId.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysDrugUnitId.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysDrugUnitId.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNHSysDrugUnitId.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNHSysDrugUnitId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", 15, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject7, "", "259", Nothing, True)})
        Me.FNHSysDrugUnitId.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.FNHSysDrugUnitId.Properties.MaxLength = 30
        Me.FNHSysDrugUnitId.Size = New System.Drawing.Size(143, 20)
        Me.FNHSysDrugUnitId.TabIndex = 284
        Me.FNHSysDrugUnitId.Tag = "2|"
        '
        'wAddDrug
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(738, 266)
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
        Me.Controls.Add(Me.FNPOPrice)
        Me.Controls.Add(Me.FNPOQuantity)
        Me.Controls.Add(Me.FNPOQuantity_lbl)
        Me.Controls.Add(Me.FNPOPrice_lbl)
        Me.Controls.Add(Me.FNHSysDrugUnitId_lbl)
        Me.Controls.Add(Me.FNHSysDrugUnitId)
        Me.Controls.Add(Me.FNHSysDrugId_None)
        Me.Controls.Add(Me.FNHSysDrugId_lbl)
        Me.Controls.Add(Me.FNHSysDrugId)
        Me.Name = "wAddDrug"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "wAddDrug"
        CType(Me.FNHSysDrugId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysDrugId_None.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTRemark.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNNetAmt.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNDisAmt.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNDisPer.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNPOPrice.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNPOQuantity.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysDrugUnitId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents FNHSysDrugId_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysDrugId As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FNHSysDrugId_None As DevExpress.XtraEditors.TextEdit
    Friend WithEvents ocmcancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmadd As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FTRemark_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTRemark As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents FNNetAmt As DevExpress.XtraEditors.CalcEdit
    Friend WithEvents FNNetAmt_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNDisAmt As DevExpress.XtraEditors.CalcEdit
    Friend WithEvents LabelControl5 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNDisPer As DevExpress.XtraEditors.CalcEdit
    Friend WithEvents FNDisPer_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNPOPrice As DevExpress.XtraEditors.CalcEdit
    Friend WithEvents FNPOQuantity As DevExpress.XtraEditors.CalcEdit
    Friend WithEvents FNPOQuantity_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNPOPrice_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysDrugUnitId_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysDrugUnitId As DevExpress.XtraEditors.ButtonEdit
End Class

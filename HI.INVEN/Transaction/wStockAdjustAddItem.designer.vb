<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wStockAdjustAddItem
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
        Me.FNHSysRawMatId = New DevExpress.XtraEditors.ButtonEdit()
        Me.FNHSysRawMatId_None = New DevExpress.XtraEditors.MemoEdit()
        Me.FTRawMatColorCode = New DevExpress.XtraEditors.TextEdit()
        Me.FTRawMatSizeCode = New DevExpress.XtraEditors.TextEdit()
        Me.FNHSysRawMatId_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTRawMatColorCode_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTRawMatSizeCode_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNTSysMatId_None_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysUnitId_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysUnitIdPO = New DevExpress.XtraEditors.ButtonEdit()
        Me.FTOrderNo_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTOrderNo = New DevExpress.XtraEditors.ButtonEdit()
        Me.FNPrice_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNQuantity_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNPOQuantity = New DevExpress.XtraEditors.CalcEdit()
        Me.FNPOPrice = New DevExpress.XtraEditors.CalcEdit()
        Me.ocmadd = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmcancel = New DevExpress.XtraEditors.SimpleButton()
        Me.FTFabricFrontSize = New DevExpress.XtraEditors.TextEdit()
        Me.FTFabricFrontSize_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNOrderType = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.FTPurchaseNo = New DevExpress.XtraEditors.TextEdit()
        Me.FTPurchaseNo_lbl = New DevExpress.XtraEditors.LabelControl()
        CType(Me.FNHSysRawMatId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysRawMatId_None.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTRawMatColorCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTRawMatSizeCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysUnitIdPO.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTOrderNo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNPOQuantity.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNPOPrice.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTFabricFrontSize.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNOrderType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTPurchaseNo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'FNHSysRawMatId
        '
        Me.FNHSysRawMatId.EnterMoveNextControl = True
        Me.FNHSysRawMatId.Location = New System.Drawing.Point(137, 37)
        Me.FNHSysRawMatId.Name = "FNHSysRawMatId"
        Me.FNHSysRawMatId.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FNHSysRawMatId.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysRawMatId.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysRawMatId.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysRawMatId.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FNHSysRawMatId.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FNHSysRawMatId.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNHSysRawMatId.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysRawMatId.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNHSysRawMatId.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNHSysRawMatId.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysRawMatId.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysRawMatId.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNHSysRawMatId.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNHSysRawMatId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", 15, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", "106", Nothing, True)})
        Me.FNHSysRawMatId.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.FNHSysRawMatId.Properties.MaxLength = 30
        Me.FNHSysRawMatId.Properties.ReadOnly = True
        Me.FNHSysRawMatId.Size = New System.Drawing.Size(143, 20)
        Me.FNHSysRawMatId.TabIndex = 1
        Me.FNHSysRawMatId.Tag = "2|"
        '
        'FNHSysRawMatId_None
        '
        Me.FNHSysRawMatId_None.EditValue = ""
        Me.FNHSysRawMatId_None.Location = New System.Drawing.Point(137, 60)
        Me.FNHSysRawMatId_None.Name = "FNHSysRawMatId_None"
        Me.FNHSysRawMatId_None.Properties.ReadOnly = True
        Me.FNHSysRawMatId_None.Size = New System.Drawing.Size(604, 57)
        Me.FNHSysRawMatId_None.TabIndex = 3
        Me.FNHSysRawMatId_None.Tag = "2|"
        '
        'FTRawMatColorCode
        '
        Me.FTRawMatColorCode.EnterMoveNextControl = True
        Me.FTRawMatColorCode.Location = New System.Drawing.Point(395, 37)
        Me.FTRawMatColorCode.Name = "FTRawMatColorCode"
        Me.FTRawMatColorCode.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FTRawMatColorCode.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTRawMatColorCode.Properties.Appearance.Options.UseBackColor = True
        Me.FTRawMatColorCode.Properties.Appearance.Options.UseForeColor = True
        Me.FTRawMatColorCode.Properties.Appearance.Options.UseTextOptions = True
        Me.FTRawMatColorCode.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTRawMatColorCode.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FTRawMatColorCode.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FTRawMatColorCode.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FTRawMatColorCode.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FTRawMatColorCode.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FTRawMatColorCode.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FTRawMatColorCode.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FTRawMatColorCode.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FTRawMatColorCode.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FTRawMatColorCode.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FTRawMatColorCode.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FTRawMatColorCode.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FTRawMatColorCode.Properties.ReadOnly = True
        Me.FTRawMatColorCode.Size = New System.Drawing.Size(109, 20)
        Me.FTRawMatColorCode.TabIndex = 1
        Me.FTRawMatColorCode.TabStop = False
        Me.FTRawMatColorCode.Tag = "2|"
        '
        'FTRawMatSizeCode
        '
        Me.FTRawMatSizeCode.EnterMoveNextControl = True
        Me.FTRawMatSizeCode.Location = New System.Drawing.Point(632, 36)
        Me.FTRawMatSizeCode.Name = "FTRawMatSizeCode"
        Me.FTRawMatSizeCode.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FTRawMatSizeCode.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTRawMatSizeCode.Properties.Appearance.Options.UseBackColor = True
        Me.FTRawMatSizeCode.Properties.Appearance.Options.UseForeColor = True
        Me.FTRawMatSizeCode.Properties.Appearance.Options.UseTextOptions = True
        Me.FTRawMatSizeCode.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTRawMatSizeCode.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FTRawMatSizeCode.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FTRawMatSizeCode.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FTRawMatSizeCode.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FTRawMatSizeCode.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FTRawMatSizeCode.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FTRawMatSizeCode.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FTRawMatSizeCode.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FTRawMatSizeCode.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FTRawMatSizeCode.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FTRawMatSizeCode.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FTRawMatSizeCode.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FTRawMatSizeCode.Properties.ReadOnly = True
        Me.FTRawMatSizeCode.Size = New System.Drawing.Size(109, 20)
        Me.FTRawMatSizeCode.TabIndex = 2
        Me.FTRawMatSizeCode.TabStop = False
        Me.FTRawMatSizeCode.Tag = "2|"
        '
        'FNHSysRawMatId_lbl
        '
        Me.FNHSysRawMatId_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysRawMatId_lbl.Appearance.Options.UseForeColor = True
        Me.FNHSysRawMatId_lbl.Appearance.Options.UseTextOptions = True
        Me.FNHSysRawMatId_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysRawMatId_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysRawMatId_lbl.Location = New System.Drawing.Point(12, 38)
        Me.FNHSysRawMatId_lbl.Name = "FNHSysRawMatId_lbl"
        Me.FNHSysRawMatId_lbl.Size = New System.Drawing.Size(121, 19)
        Me.FNHSysRawMatId_lbl.TabIndex = 261
        Me.FNHSysRawMatId_lbl.Tag = "2|"
        Me.FNHSysRawMatId_lbl.Text = "Material Code :"
        '
        'FTRawMatColorCode_lbl
        '
        Me.FTRawMatColorCode_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTRawMatColorCode_lbl.Appearance.Options.UseForeColor = True
        Me.FTRawMatColorCode_lbl.Appearance.Options.UseTextOptions = True
        Me.FTRawMatColorCode_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTRawMatColorCode_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTRawMatColorCode_lbl.Location = New System.Drawing.Point(286, 38)
        Me.FTRawMatColorCode_lbl.Name = "FTRawMatColorCode_lbl"
        Me.FTRawMatColorCode_lbl.Size = New System.Drawing.Size(107, 19)
        Me.FTRawMatColorCode_lbl.TabIndex = 262
        Me.FTRawMatColorCode_lbl.Tag = "2|"
        Me.FTRawMatColorCode_lbl.Text = "Color Code :"
        '
        'FTRawMatSizeCode_lbl
        '
        Me.FTRawMatSizeCode_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTRawMatSizeCode_lbl.Appearance.Options.UseForeColor = True
        Me.FTRawMatSizeCode_lbl.Appearance.Options.UseTextOptions = True
        Me.FTRawMatSizeCode_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTRawMatSizeCode_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTRawMatSizeCode_lbl.Location = New System.Drawing.Point(519, 37)
        Me.FTRawMatSizeCode_lbl.Name = "FTRawMatSizeCode_lbl"
        Me.FTRawMatSizeCode_lbl.Size = New System.Drawing.Size(107, 19)
        Me.FTRawMatSizeCode_lbl.TabIndex = 263
        Me.FTRawMatSizeCode_lbl.Tag = "2|"
        Me.FTRawMatSizeCode_lbl.Text = "Size Code :"
        '
        'FNTSysMatId_None_lbl
        '
        Me.FNTSysMatId_None_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FNTSysMatId_None_lbl.Appearance.Options.UseForeColor = True
        Me.FNTSysMatId_None_lbl.Appearance.Options.UseTextOptions = True
        Me.FNTSysMatId_None_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNTSysMatId_None_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNTSysMatId_None_lbl.Location = New System.Drawing.Point(12, 60)
        Me.FNTSysMatId_None_lbl.Name = "FNTSysMatId_None_lbl"
        Me.FNTSysMatId_None_lbl.Size = New System.Drawing.Size(121, 19)
        Me.FNTSysMatId_None_lbl.TabIndex = 264
        Me.FNTSysMatId_None_lbl.Tag = "2|"
        Me.FNTSysMatId_None_lbl.Text = "Material Code :"
        '
        'FNHSysUnitId_lbl
        '
        Me.FNHSysUnitId_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FNHSysUnitId_lbl.Appearance.Options.UseForeColor = True
        Me.FNHSysUnitId_lbl.Appearance.Options.UseTextOptions = True
        Me.FNHSysUnitId_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysUnitId_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysUnitId_lbl.Location = New System.Drawing.Point(43, 145)
        Me.FNHSysUnitId_lbl.Name = "FNHSysUnitId_lbl"
        Me.FNHSysUnitId_lbl.Size = New System.Drawing.Size(92, 19)
        Me.FNHSysUnitId_lbl.TabIndex = 266
        Me.FNHSysUnitId_lbl.Tag = "2|"
        Me.FNHSysUnitId_lbl.Text = "Unit :"
        '
        'FNHSysUnitIdPO
        '
        Me.FNHSysUnitIdPO.EnterMoveNextControl = True
        Me.FNHSysUnitIdPO.Location = New System.Drawing.Point(137, 145)
        Me.FNHSysUnitIdPO.Name = "FNHSysUnitIdPO"
        Me.FNHSysUnitIdPO.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FNHSysUnitIdPO.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysUnitIdPO.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysUnitIdPO.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysUnitIdPO.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FNHSysUnitIdPO.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FNHSysUnitIdPO.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNHSysUnitIdPO.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysUnitIdPO.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNHSysUnitIdPO.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNHSysUnitIdPO.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysUnitIdPO.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysUnitIdPO.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNHSysUnitIdPO.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNHSysUnitIdPO.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", 15, True, False, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject2, "", "144", Nothing, True)})
        Me.FNHSysUnitIdPO.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.FNHSysUnitIdPO.Properties.MaxLength = 30
        Me.FNHSysUnitIdPO.Properties.ReadOnly = True
        Me.FNHSysUnitIdPO.Size = New System.Drawing.Size(143, 20)
        Me.FNHSysUnitIdPO.TabIndex = 4
        Me.FNHSysUnitIdPO.Tag = "2|"
        '
        'FTOrderNo_lbl
        '
        Me.FTOrderNo_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTOrderNo_lbl.Appearance.Options.UseForeColor = True
        Me.FTOrderNo_lbl.Appearance.Options.UseTextOptions = True
        Me.FTOrderNo_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTOrderNo_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTOrderNo_lbl.Location = New System.Drawing.Point(34, 12)
        Me.FTOrderNo_lbl.Name = "FTOrderNo_lbl"
        Me.FTOrderNo_lbl.Size = New System.Drawing.Size(101, 19)
        Me.FTOrderNo_lbl.TabIndex = 269
        Me.FTOrderNo_lbl.Tag = "2|"
        Me.FTOrderNo_lbl.Text = "Order No :"
        '
        'FTOrderNo
        '
        Me.FTOrderNo.EnterMoveNextControl = True
        Me.FTOrderNo.Location = New System.Drawing.Point(137, 12)
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
        Me.FTOrderNo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", 15, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject3, "", "692", Nothing, True)})
        Me.FTOrderNo.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.FTOrderNo.Properties.MaxLength = 30
        Me.FTOrderNo.Size = New System.Drawing.Size(143, 20)
        Me.FTOrderNo.TabIndex = 0
        Me.FTOrderNo.Tag = "2|"
        '
        'FNPrice_lbl
        '
        Me.FNPrice_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNPrice_lbl.Appearance.Options.UseForeColor = True
        Me.FNPrice_lbl.Appearance.Options.UseTextOptions = True
        Me.FNPrice_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNPrice_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNPrice_lbl.Location = New System.Drawing.Point(286, 146)
        Me.FNPrice_lbl.Name = "FNPrice_lbl"
        Me.FNPrice_lbl.Size = New System.Drawing.Size(92, 19)
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
        Me.FNQuantity_lbl.Location = New System.Drawing.Point(499, 145)
        Me.FNQuantity_lbl.Name = "FNQuantity_lbl"
        Me.FNQuantity_lbl.Size = New System.Drawing.Size(97, 19)
        Me.FNQuantity_lbl.TabIndex = 271
        Me.FNQuantity_lbl.Tag = "2|"
        Me.FNQuantity_lbl.Text = "Quantity :"
        '
        'FNPOQuantity
        '
        Me.FNPOQuantity.EnterMoveNextControl = True
        Me.FNPOQuantity.Location = New System.Drawing.Point(597, 146)
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
        Me.FNPOQuantity.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, True, False, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject4, "", Nothing, Nothing, True)})
        Me.FNPOQuantity.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNPOQuantity.Properties.Precision = 4
        Me.FNPOQuantity.Size = New System.Drawing.Size(144, 20)
        Me.FNPOQuantity.TabIndex = 6
        Me.FNPOQuantity.Tag = "2|"
        '
        'FNPOPrice
        '
        Me.FNPOPrice.EnterMoveNextControl = True
        Me.FNPOPrice.Location = New System.Drawing.Point(384, 146)
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
        Me.FNPOPrice.TabIndex = 5
        Me.FNPOPrice.Tag = "2|"
        '
        'ocmadd
        '
        Me.ocmadd.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ocmadd.Location = New System.Drawing.Point(137, 186)
        Me.ocmadd.Name = "ocmadd"
        Me.ocmadd.Size = New System.Drawing.Size(145, 27)
        Me.ocmadd.TabIndex = 8
        Me.ocmadd.Text = "ADD MATERIAL"
        '
        'ocmcancel
        '
        Me.ocmcancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ocmcancel.Location = New System.Drawing.Point(596, 186)
        Me.ocmcancel.Name = "ocmcancel"
        Me.ocmcancel.Size = New System.Drawing.Size(145, 27)
        Me.ocmcancel.TabIndex = 283
        Me.ocmcancel.Text = "CANCEL"
        '
        'FTFabricFrontSize
        '
        Me.FTFabricFrontSize.EnterMoveNextControl = True
        Me.FTFabricFrontSize.Location = New System.Drawing.Point(453, 122)
        Me.FTFabricFrontSize.Name = "FTFabricFrontSize"
        Me.FTFabricFrontSize.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FTFabricFrontSize.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTFabricFrontSize.Properties.Appearance.Options.UseBackColor = True
        Me.FTFabricFrontSize.Properties.Appearance.Options.UseForeColor = True
        Me.FTFabricFrontSize.Properties.Appearance.Options.UseTextOptions = True
        Me.FTFabricFrontSize.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.FTFabricFrontSize.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FTFabricFrontSize.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FTFabricFrontSize.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FTFabricFrontSize.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FTFabricFrontSize.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FTFabricFrontSize.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FTFabricFrontSize.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FTFabricFrontSize.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FTFabricFrontSize.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FTFabricFrontSize.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FTFabricFrontSize.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FTFabricFrontSize.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FTFabricFrontSize.Properties.MaxLength = 50
        Me.FTFabricFrontSize.Size = New System.Drawing.Size(288, 20)
        Me.FTFabricFrontSize.TabIndex = 2
        Me.FTFabricFrontSize.TabStop = False
        Me.FTFabricFrontSize.Tag = "2|"
        '
        'FTFabricFrontSize_lbl
        '
        Me.FTFabricFrontSize_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTFabricFrontSize_lbl.Appearance.Options.UseForeColor = True
        Me.FTFabricFrontSize_lbl.Appearance.Options.UseTextOptions = True
        Me.FTFabricFrontSize_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTFabricFrontSize_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTFabricFrontSize_lbl.Location = New System.Drawing.Point(330, 122)
        Me.FTFabricFrontSize_lbl.Name = "FTFabricFrontSize_lbl"
        Me.FTFabricFrontSize_lbl.Size = New System.Drawing.Size(121, 19)
        Me.FTFabricFrontSize_lbl.TabIndex = 285
        Me.FTFabricFrontSize_lbl.Tag = "2|"
        Me.FTFabricFrontSize_lbl.Text = "Fabric Front Size :"
        '
        'FNOrderType
        '
        Me.FNOrderType.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FNOrderType.EditValue = ""
        Me.FNOrderType.Enabled = False
        Me.FNOrderType.EnterMoveNextControl = True
        Me.FNOrderType.Location = New System.Drawing.Point(395, 11)
        Me.FNOrderType.Name = "FNOrderType"
        Me.FNOrderType.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FNOrderType.Properties.Appearance.Options.UseBackColor = True
        Me.FNOrderType.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FNOrderType.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FNOrderType.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FNOrderType.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FNOrderType.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNOrderType.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNOrderType.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNOrderType.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNOrderType.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNOrderType.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNOrderType.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNOrderType.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNOrderType.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FNOrderType.Properties.Tag = "FNOrderType"
        Me.FNOrderType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.FNOrderType.Size = New System.Drawing.Size(109, 20)
        Me.FNOrderType.TabIndex = 286
        Me.FNOrderType.Tag = "2|"
        Me.FNOrderType.Visible = False
        '
        'FTPurchaseNo
        '
        Me.FTPurchaseNo.EnterMoveNextControl = True
        Me.FTPurchaseNo.Location = New System.Drawing.Point(138, 121)
        Me.FTPurchaseNo.Name = "FTPurchaseNo"
        Me.FTPurchaseNo.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FTPurchaseNo.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTPurchaseNo.Properties.Appearance.Options.UseBackColor = True
        Me.FTPurchaseNo.Properties.Appearance.Options.UseForeColor = True
        Me.FTPurchaseNo.Properties.Appearance.Options.UseTextOptions = True
        Me.FTPurchaseNo.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
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
        Me.FTPurchaseNo.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.FTPurchaseNo.Properties.MaxLength = 30
        Me.FTPurchaseNo.Size = New System.Drawing.Size(144, 20)
        Me.FTPurchaseNo.TabIndex = 287
        Me.FTPurchaseNo.TabStop = False
        Me.FTPurchaseNo.Tag = "2|"
        '
        'FTPurchaseNo_lbl
        '
        Me.FTPurchaseNo_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTPurchaseNo_lbl.Appearance.Options.UseForeColor = True
        Me.FTPurchaseNo_lbl.Appearance.Options.UseTextOptions = True
        Me.FTPurchaseNo_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTPurchaseNo_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTPurchaseNo_lbl.Location = New System.Drawing.Point(0, 121)
        Me.FTPurchaseNo_lbl.Name = "FTPurchaseNo_lbl"
        Me.FTPurchaseNo_lbl.Size = New System.Drawing.Size(137, 19)
        Me.FTPurchaseNo_lbl.TabIndex = 288
        Me.FTPurchaseNo_lbl.Tag = "2|"
        Me.FTPurchaseNo_lbl.Text = "Purchase Order No :"
        '
        'wStockAdjustAddItem
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(854, 223)
        Me.ControlBox = False
        Me.Controls.Add(Me.FTPurchaseNo_lbl)
        Me.Controls.Add(Me.FTPurchaseNo)
        Me.Controls.Add(Me.FNOrderType)
        Me.Controls.Add(Me.FTFabricFrontSize_lbl)
        Me.Controls.Add(Me.FTFabricFrontSize)
        Me.Controls.Add(Me.ocmcancel)
        Me.Controls.Add(Me.ocmadd)
        Me.Controls.Add(Me.FNPOPrice)
        Me.Controls.Add(Me.FNPOQuantity)
        Me.Controls.Add(Me.FNQuantity_lbl)
        Me.Controls.Add(Me.FNPrice_lbl)
        Me.Controls.Add(Me.FTOrderNo_lbl)
        Me.Controls.Add(Me.FTOrderNo)
        Me.Controls.Add(Me.FNHSysUnitId_lbl)
        Me.Controls.Add(Me.FNHSysUnitIdPO)
        Me.Controls.Add(Me.FNTSysMatId_None_lbl)
        Me.Controls.Add(Me.FTRawMatSizeCode_lbl)
        Me.Controls.Add(Me.FTRawMatColorCode_lbl)
        Me.Controls.Add(Me.FNHSysRawMatId_lbl)
        Me.Controls.Add(Me.FTRawMatSizeCode)
        Me.Controls.Add(Me.FTRawMatColorCode)
        Me.Controls.Add(Me.FNHSysRawMatId_None)
        Me.Controls.Add(Me.FNHSysRawMatId)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "wStockAdjustAddItem"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Add Item"
        CType(Me.FNHSysRawMatId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysRawMatId_None.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTRawMatColorCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTRawMatSizeCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysUnitIdPO.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTOrderNo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNPOQuantity.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNPOPrice.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTFabricFrontSize.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNOrderType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTPurchaseNo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents FNHSysRawMatId As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FNHSysRawMatId_None As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents FTRawMatColorCode As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FTRawMatSizeCode As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FNHSysRawMatId_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTRawMatColorCode_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTRawMatSizeCode_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNTSysMatId_None_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysUnitId_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysUnitIdPO As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FTOrderNo_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTOrderNo As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FNPrice_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNQuantity_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNPOQuantity As DevExpress.XtraEditors.CalcEdit
    Friend WithEvents FNPOPrice As DevExpress.XtraEditors.CalcEdit
    Friend WithEvents ocmadd As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmcancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FTFabricFrontSize As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FTFabricFrontSize_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNOrderType As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents FTPurchaseNo As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FTPurchaseNo_lbl As DevExpress.XtraEditors.LabelControl

End Class

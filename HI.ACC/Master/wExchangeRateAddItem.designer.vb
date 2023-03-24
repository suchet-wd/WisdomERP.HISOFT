<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wExchangeRateAddItem
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
        Me.FTRemark_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTRemark = New DevExpress.XtraEditors.MemoEdit()
        Me.ocmadd = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmcancel = New DevExpress.XtraEditors.SimpleButton()
        Me.FNHSysCurId = New DevExpress.XtraEditors.ButtonEdit()
        Me.FNHSysCurId_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysCurId_None = New DevExpress.XtraEditors.TextEdit()
        Me.EFTDate = New DevExpress.XtraEditors.DateEdit()
        Me.EFTDate_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.SFTDate = New DevExpress.XtraEditors.DateEdit()
        Me.SFTDate_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNBuyingRate = New DevExpress.XtraEditors.CalcEdit()
        Me.FNBuyingRate_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNSellingRate = New DevExpress.XtraEditors.CalcEdit()
        Me.FNSellingRate_lbl = New DevExpress.XtraEditors.LabelControl()
        CType(Me.FTRemark.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysCurId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysCurId_None.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EFTDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EFTDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SFTDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SFTDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNBuyingRate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNSellingRate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'FTRemark_lbl
        '
        Me.FTRemark_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTRemark_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTRemark_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTRemark_lbl.Location = New System.Drawing.Point(43, 93)
        Me.FTRemark_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTRemark_lbl.Name = "FTRemark_lbl"
        Me.FTRemark_lbl.Size = New System.Drawing.Size(155, 23)
        Me.FTRemark_lbl.TabIndex = 281
        Me.FTRemark_lbl.Tag = "2|"
        Me.FTRemark_lbl.Text = "Remark :"
        '
        'FTRemark
        '
        Me.FTRemark.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.FTRemark.EditValue = ""
        Me.FTRemark.Location = New System.Drawing.Point(204, 99)
        Me.FTRemark.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTRemark.Name = "FTRemark"
        Me.FTRemark.Properties.MaxLength = 200
        Me.FTRemark.Size = New System.Drawing.Size(496, 121)
        Me.FTRemark.TabIndex = 7
        Me.FTRemark.Tag = "2|"
        Me.FTRemark.UseOptimizedRendering = True
        '
        'ocmadd
        '
        Me.ocmadd.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ocmadd.Location = New System.Drawing.Point(203, 231)
        Me.ocmadd.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmadd.Name = "ocmadd"
        Me.ocmadd.Size = New System.Drawing.Size(169, 33)
        Me.ocmadd.TabIndex = 8
        Me.ocmadd.Text = "ADD Exchange Rate"
        '
        'ocmcancel
        '
        Me.ocmcancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ocmcancel.Location = New System.Drawing.Point(531, 231)
        Me.ocmcancel.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmcancel.Name = "ocmcancel"
        Me.ocmcancel.Size = New System.Drawing.Size(169, 33)
        Me.ocmcancel.TabIndex = 283
        Me.ocmcancel.Text = "CANCEL"
        '
        'FNHSysCurId
        '
        Me.FNHSysCurId.EnterMoveNextControl = True
        Me.FNHSysCurId.Location = New System.Drawing.Point(201, 13)
        Me.FNHSysCurId.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysCurId.Name = "FNHSysCurId"
        Me.FNHSysCurId.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FNHSysCurId.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysCurId.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysCurId.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysCurId.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FNHSysCurId.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FNHSysCurId.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNHSysCurId.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysCurId.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNHSysCurId.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNHSysCurId.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysCurId.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysCurId.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNHSysCurId.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNHSysCurId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", 15, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", "16", Nothing, True)})
        Me.FNHSysCurId.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.FNHSysCurId.Properties.MaxLength = 30
        Me.FNHSysCurId.Size = New System.Drawing.Size(142, 22)
        Me.FNHSysCurId.TabIndex = 284
        Me.FNHSysCurId.Tag = "2|"
        '
        'FNHSysCurId_lbl
        '
        Me.FNHSysCurId_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysCurId_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysCurId_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysCurId_lbl.Location = New System.Drawing.Point(56, 12)
        Me.FNHSysCurId_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysCurId_lbl.Name = "FNHSysCurId_lbl"
        Me.FNHSysCurId_lbl.Size = New System.Drawing.Size(140, 23)
        Me.FNHSysCurId_lbl.TabIndex = 285
        Me.FNHSysCurId_lbl.Tag = "2|"
        Me.FNHSysCurId_lbl.Text = "Currency :"
        '
        'FNHSysCurId_None
        '
        Me.FNHSysCurId_None.EnterMoveNextControl = True
        Me.FNHSysCurId_None.Location = New System.Drawing.Point(349, 13)
        Me.FNHSysCurId_None.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysCurId_None.Name = "FNHSysCurId_None"
        Me.FNHSysCurId_None.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FNHSysCurId_None.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FNHSysCurId_None.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysCurId_None.Properties.Appearance.Options.UseForeColor = True
        Me.FNHSysCurId_None.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysCurId_None.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysCurId_None.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FNHSysCurId_None.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FNHSysCurId_None.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNHSysCurId_None.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysCurId_None.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNHSysCurId_None.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNHSysCurId_None.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysCurId_None.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysCurId_None.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNHSysCurId_None.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNHSysCurId_None.Properties.ReadOnly = True
        Me.FNHSysCurId_None.Size = New System.Drawing.Size(351, 22)
        Me.FNHSysCurId_None.TabIndex = 286
        Me.FNHSysCurId_None.TabStop = False
        Me.FNHSysCurId_None.Tag = "2|"
        '
        'EFTDate
        '
        Me.EFTDate.EditValue = Nothing
        Me.EFTDate.EnterMoveNextControl = True
        Me.EFTDate.Location = New System.Drawing.Point(560, 41)
        Me.EFTDate.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.EFTDate.Name = "EFTDate"
        Me.EFTDate.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.EFTDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.EFTDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.EFTDate.Properties.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.EFTDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.EFTDate.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.EFTDate.Properties.NullDate = ""
        Me.EFTDate.Size = New System.Drawing.Size(140, 22)
        Me.EFTDate.TabIndex = 289
        Me.EFTDate.Tag = "2|"
        '
        'EFTDate_lbl
        '
        Me.EFTDate_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.EFTDate_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.EFTDate_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.EFTDate_lbl.Location = New System.Drawing.Point(428, 41)
        Me.EFTDate_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.EFTDate_lbl.Name = "EFTDate_lbl"
        Me.EFTDate_lbl.Size = New System.Drawing.Size(128, 25)
        Me.EFTDate_lbl.TabIndex = 290
        Me.EFTDate_lbl.Tag = "2|"
        Me.EFTDate_lbl.Text = "ถึงวันที่ :"
        '
        'SFTDate
        '
        Me.SFTDate.EditValue = Nothing
        Me.SFTDate.EnterMoveNextControl = True
        Me.SFTDate.Location = New System.Drawing.Point(202, 41)
        Me.SFTDate.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.SFTDate.Name = "SFTDate"
        Me.SFTDate.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.SFTDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.SFTDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.SFTDate.Properties.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.SFTDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.SFTDate.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.SFTDate.Properties.NullDate = ""
        Me.SFTDate.Size = New System.Drawing.Size(140, 22)
        Me.SFTDate.TabIndex = 287
        Me.SFTDate.Tag = "2|"
        '
        'SFTDate_lbl
        '
        Me.SFTDate_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.SFTDate_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.SFTDate_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.SFTDate_lbl.Location = New System.Drawing.Point(70, 41)
        Me.SFTDate_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.SFTDate_lbl.Name = "SFTDate_lbl"
        Me.SFTDate_lbl.Size = New System.Drawing.Size(128, 25)
        Me.SFTDate_lbl.TabIndex = 288
        Me.SFTDate_lbl.Tag = "2|"
        Me.SFTDate_lbl.Text = "วันที่ :"
        '
        'FNBuyingRate
        '
        Me.FNBuyingRate.EnterMoveNextControl = True
        Me.FNBuyingRate.Location = New System.Drawing.Point(202, 68)
        Me.FNBuyingRate.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNBuyingRate.Name = "FNBuyingRate"
        Me.FNBuyingRate.Properties.Appearance.Options.UseTextOptions = True
        Me.FNBuyingRate.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNBuyingRate.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNBuyingRate.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNBuyingRate.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNBuyingRate.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNBuyingRate.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNBuyingRate.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNBuyingRate.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNBuyingRate.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNBuyingRate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, True, False, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject2, "", Nothing, Nothing, True)})
        Me.FNBuyingRate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNBuyingRate.Properties.Precision = 4
        Me.FNBuyingRate.Size = New System.Drawing.Size(140, 22)
        Me.FNBuyingRate.TabIndex = 291
        Me.FNBuyingRate.Tag = "2|"
        '
        'FNBuyingRate_lbl
        '
        Me.FNBuyingRate_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNBuyingRate_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNBuyingRate_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNBuyingRate_lbl.Location = New System.Drawing.Point(53, 67)
        Me.FNBuyingRate_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNBuyingRate_lbl.Name = "FNBuyingRate_lbl"
        Me.FNBuyingRate_lbl.Size = New System.Drawing.Size(142, 23)
        Me.FNBuyingRate_lbl.TabIndex = 292
        Me.FNBuyingRate_lbl.Tag = "2|"
        Me.FNBuyingRate_lbl.Text = "Buying Rate :"
        '
        'FNSellingRate
        '
        Me.FNSellingRate.EnterMoveNextControl = True
        Me.FNSellingRate.Location = New System.Drawing.Point(560, 71)
        Me.FNSellingRate.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNSellingRate.Name = "FNSellingRate"
        Me.FNSellingRate.Properties.Appearance.Options.UseTextOptions = True
        Me.FNSellingRate.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNSellingRate.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNSellingRate.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNSellingRate.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNSellingRate.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNSellingRate.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNSellingRate.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNSellingRate.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNSellingRate.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNSellingRate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, True, False, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject3, "", Nothing, Nothing, True)})
        Me.FNSellingRate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNSellingRate.Properties.Precision = 4
        Me.FNSellingRate.Size = New System.Drawing.Size(140, 22)
        Me.FNSellingRate.TabIndex = 293
        Me.FNSellingRate.Tag = "2|"
        '
        'FNSellingRate_lbl
        '
        Me.FNSellingRate_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNSellingRate_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNSellingRate_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNSellingRate_lbl.Location = New System.Drawing.Point(411, 70)
        Me.FNSellingRate_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNSellingRate_lbl.Name = "FNSellingRate_lbl"
        Me.FNSellingRate_lbl.Size = New System.Drawing.Size(142, 23)
        Me.FNSellingRate_lbl.TabIndex = 294
        Me.FNSellingRate_lbl.Tag = "2|"
        Me.FNSellingRate_lbl.Text = "Selling Rate :"
        '
        'wExchangeRateAddItem
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(850, 276)
        Me.Controls.Add(Me.FNSellingRate)
        Me.Controls.Add(Me.FNSellingRate_lbl)
        Me.Controls.Add(Me.FNBuyingRate)
        Me.Controls.Add(Me.FNBuyingRate_lbl)
        Me.Controls.Add(Me.EFTDate)
        Me.Controls.Add(Me.EFTDate_lbl)
        Me.Controls.Add(Me.SFTDate)
        Me.Controls.Add(Me.SFTDate_lbl)
        Me.Controls.Add(Me.FNHSysCurId_None)
        Me.Controls.Add(Me.FNHSysCurId)
        Me.Controls.Add(Me.FNHSysCurId_lbl)
        Me.Controls.Add(Me.ocmcancel)
        Me.Controls.Add(Me.ocmadd)
        Me.Controls.Add(Me.FTRemark_lbl)
        Me.Controls.Add(Me.FTRemark)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "wExchangeRateAddItem"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Add Exchange Rate"
        CType(Me.FTRemark.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysCurId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysCurId_None.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EFTDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EFTDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SFTDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SFTDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNBuyingRate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNSellingRate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents FTRemark_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTRemark As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents ocmadd As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmcancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FNHSysCurId As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FNHSysCurId_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysCurId_None As DevExpress.XtraEditors.TextEdit
    Friend WithEvents EFTDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents EFTDate_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents SFTDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents SFTDate_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNBuyingRate As DevExpress.XtraEditors.CalcEdit
    Friend WithEvents FNBuyingRate_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNSellingRate As DevExpress.XtraEditors.CalcEdit
    Friend WithEvents FNSellingRate_lbl As DevExpress.XtraEditors.LabelControl

End Class

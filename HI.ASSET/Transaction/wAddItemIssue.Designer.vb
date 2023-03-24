<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wAddItemIssue
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
        Me.FTBarcodeNo_None = New DevExpress.XtraEditors.TextEdit()
        Me.FNQuantity = New DevExpress.XtraEditors.CalcEdit()
        Me.FNQuantity_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTBarcodeNo_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTBarcodeNo = New DevExpress.XtraEditors.ButtonEdit()
        Me.ocmcancel = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmadd = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.FTBarcodeNo_None.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNQuantity.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTBarcodeNo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'FTBarcodeNo_None
        '
        Me.FTBarcodeNo_None.Location = New System.Drawing.Point(334, 24)
        Me.FTBarcodeNo_None.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTBarcodeNo_None.Name = "FTBarcodeNo_None"
        Me.FTBarcodeNo_None.Properties.Appearance.BackColor = System.Drawing.Color.LightCyan
        Me.FTBarcodeNo_None.Properties.Appearance.Options.UseBackColor = True
        Me.FTBarcodeNo_None.Properties.ReadOnly = True
        Me.FTBarcodeNo_None.Size = New System.Drawing.Size(408, 22)
        Me.FTBarcodeNo_None.TabIndex = 295
        Me.FTBarcodeNo_None.Tag = "2|"
        '
        'FNQuantity
        '
        Me.FNQuantity.EnterMoveNextControl = True
        Me.FNQuantity.Location = New System.Drawing.Point(160, 52)
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
        Me.FNQuantity.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, True, False, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", Nothing, Nothing, True)})
        Me.FNQuantity.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNQuantity.Properties.Precision = 0
        Me.FNQuantity.Size = New System.Drawing.Size(167, 22)
        Me.FNQuantity.TabIndex = 292
        Me.FNQuantity.Tag = "2|"
        '
        'FNQuantity_lbl
        '
        Me.FNQuantity_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNQuantity_lbl.Appearance.Options.UseForeColor = True
        Me.FNQuantity_lbl.Appearance.Options.UseTextOptions = True
        Me.FNQuantity_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNQuantity_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNQuantity_lbl.Location = New System.Drawing.Point(14, 52)
        Me.FNQuantity_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNQuantity_lbl.Name = "FNQuantity_lbl"
        Me.FNQuantity_lbl.Size = New System.Drawing.Size(143, 23)
        Me.FNQuantity_lbl.TabIndex = 294
        Me.FNQuantity_lbl.Tag = "2|"
        Me.FNQuantity_lbl.Text = "Quantity :"
        '
        'FTBarcodeNo_lbl
        '
        Me.FTBarcodeNo_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTBarcodeNo_lbl.Appearance.Options.UseForeColor = True
        Me.FTBarcodeNo_lbl.Appearance.Options.UseTextOptions = True
        Me.FTBarcodeNo_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTBarcodeNo_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTBarcodeNo_lbl.Location = New System.Drawing.Point(14, 24)
        Me.FTBarcodeNo_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTBarcodeNo_lbl.Name = "FTBarcodeNo_lbl"
        Me.FTBarcodeNo_lbl.Size = New System.Drawing.Size(143, 23)
        Me.FTBarcodeNo_lbl.TabIndex = 293
        Me.FTBarcodeNo_lbl.Tag = "2|"
        Me.FTBarcodeNo_lbl.Text = "Assetcode :"
        '
        'FTBarcodeNo
        '
        Me.FTBarcodeNo.EnterMoveNextControl = True
        Me.FTBarcodeNo.Location = New System.Drawing.Point(160, 24)
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
        Me.FTBarcodeNo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", 15, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject2, "", "508", Nothing, True)})
        Me.FTBarcodeNo.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.FTBarcodeNo.Properties.MaxLength = 30
        Me.FTBarcodeNo.Size = New System.Drawing.Size(167, 22)
        Me.FTBarcodeNo.TabIndex = 291
        Me.FTBarcodeNo.Tag = "2|"
        '
        'ocmcancel
        '
        Me.ocmcancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ocmcancel.Location = New System.Drawing.Point(573, 93)
        Me.ocmcancel.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmcancel.Name = "ocmcancel"
        Me.ocmcancel.Size = New System.Drawing.Size(169, 33)
        Me.ocmcancel.TabIndex = 297
        Me.ocmcancel.Text = "CANCEL"
        '
        'ocmadd
        '
        Me.ocmadd.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ocmadd.Location = New System.Drawing.Point(161, 93)
        Me.ocmadd.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmadd.Name = "ocmadd"
        Me.ocmadd.Size = New System.Drawing.Size(169, 33)
        Me.ocmadd.TabIndex = 296
        Me.ocmadd.Text = "ADD"
        '
        'wAddItemIssue
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(792, 139)
        Me.Controls.Add(Me.ocmcancel)
        Me.Controls.Add(Me.ocmadd)
        Me.Controls.Add(Me.FTBarcodeNo_None)
        Me.Controls.Add(Me.FNQuantity)
        Me.Controls.Add(Me.FNQuantity_lbl)
        Me.Controls.Add(Me.FTBarcodeNo_lbl)
        Me.Controls.Add(Me.FTBarcodeNo)
        Me.Name = "wAddItemIssue"
        Me.Text = "wAddItemIssue"
        CType(Me.FTBarcodeNo_None.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNQuantity.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTBarcodeNo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents FTBarcodeNo_None As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FNQuantity As DevExpress.XtraEditors.CalcEdit
    Friend WithEvents FNQuantity_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTBarcodeNo_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTBarcodeNo As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents ocmcancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmadd As DevExpress.XtraEditors.SimpleButton
End Class

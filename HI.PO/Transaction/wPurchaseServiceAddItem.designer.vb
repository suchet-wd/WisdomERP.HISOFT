<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wPurchaseServiceAddItem
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
        Me.FTOrderNo_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTOrderNo = New DevExpress.XtraEditors.ButtonEdit()
        Me.FNPrice_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNQuantity_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNQuantity = New DevExpress.XtraEditors.CalcEdit()
        Me.FNPrice = New DevExpress.XtraEditors.CalcEdit()
        Me.FNNetAmt_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNAmount = New DevExpress.XtraEditors.CalcEdit()
        Me.ocmadd = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmcancel = New DevExpress.XtraEditors.SimpleButton()
        Me.FTDescription = New DevExpress.XtraEditors.MemoEdit()
        Me.FTDescription_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTNote = New DevExpress.XtraEditors.MemoEdit()
        Me.FTNote_lbl = New DevExpress.XtraEditors.LabelControl()
        CType(Me.FTOrderNo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNQuantity.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNPrice.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNAmount.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTDescription.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTNote.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'FTOrderNo_lbl
        '
        Me.FTOrderNo_lbl.Appearance.ForeColor = System.Drawing.Color.Black
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
        Me.FTOrderNo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", 15, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", "623", Nothing, True)})
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
        Me.FNPrice_lbl.Location = New System.Drawing.Point(303, 189)
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
        Me.FNQuantity_lbl.Location = New System.Drawing.Point(39, 188)
        Me.FNQuantity_lbl.Name = "FNQuantity_lbl"
        Me.FNQuantity_lbl.Size = New System.Drawing.Size(97, 19)
        Me.FNQuantity_lbl.TabIndex = 271
        Me.FNQuantity_lbl.Tag = "2|"
        Me.FNQuantity_lbl.Text = "Quantity :"
        '
        'FNQuantity
        '
        Me.FNQuantity.EnterMoveNextControl = True
        Me.FNQuantity.Location = New System.Drawing.Point(138, 189)
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
        Me.FNQuantity.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, True, False, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject2, "", Nothing, Nothing, True)})
        Me.FNQuantity.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNQuantity.Properties.Precision = 4
        Me.FNQuantity.Size = New System.Drawing.Size(144, 20)
        Me.FNQuantity.TabIndex = 2
        Me.FNQuantity.Tag = "2|"
        '
        'FNPrice
        '
        Me.FNPrice.EnterMoveNextControl = True
        Me.FNPrice.Location = New System.Drawing.Point(400, 189)
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
        Me.FNPrice.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, True, False, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject3, "", Nothing, Nothing, True)})
        Me.FNPrice.Properties.DisplayFormat.FormatString = "{0:n3}"
        Me.FNPrice.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNPrice.Properties.EditFormat.FormatString = "{0:n3}"
        Me.FNPrice.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNPrice.Properties.Precision = 3
        Me.FNPrice.Size = New System.Drawing.Size(108, 20)
        Me.FNPrice.TabIndex = 3
        Me.FNPrice.Tag = "2|"
        '
        'FNNetAmt_lbl
        '
        Me.FNNetAmt_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FNNetAmt_lbl.Appearance.Options.UseForeColor = True
        Me.FNNetAmt_lbl.Appearance.Options.UseTextOptions = True
        Me.FNNetAmt_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNNetAmt_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNNetAmt_lbl.Location = New System.Drawing.Point(535, 189)
        Me.FNNetAmt_lbl.Name = "FNNetAmt_lbl"
        Me.FNNetAmt_lbl.Size = New System.Drawing.Size(92, 19)
        Me.FNNetAmt_lbl.TabIndex = 278
        Me.FNNetAmt_lbl.Tag = "2|"
        Me.FNNetAmt_lbl.Text = "Net Amount :"
        '
        'FNAmount
        '
        Me.FNAmount.EnterMoveNextControl = True
        Me.FNAmount.Location = New System.Drawing.Point(633, 189)
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
        Me.FNAmount.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, True, False, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject4, "", Nothing, Nothing, True)})
        Me.FNAmount.Properties.DisplayFormat.FormatString = "{0:n2}"
        Me.FNAmount.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNAmount.Properties.EditFormat.FormatString = "{0:n2}"
        Me.FNAmount.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNAmount.Properties.Precision = 2
        Me.FNAmount.Properties.ReadOnly = True
        Me.FNAmount.Size = New System.Drawing.Size(108, 20)
        Me.FNAmount.TabIndex = 279
        Me.FNAmount.Tag = "2|"
        '
        'ocmadd
        '
        Me.ocmadd.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ocmadd.Location = New System.Drawing.Point(137, 310)
        Me.ocmadd.Name = "ocmadd"
        Me.ocmadd.Size = New System.Drawing.Size(145, 27)
        Me.ocmadd.TabIndex = 4
        Me.ocmadd.Text = "ADD"
        '
        'ocmcancel
        '
        Me.ocmcancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ocmcancel.Location = New System.Drawing.Point(596, 310)
        Me.ocmcancel.Name = "ocmcancel"
        Me.ocmcancel.Size = New System.Drawing.Size(145, 27)
        Me.ocmcancel.TabIndex = 5
        Me.ocmcancel.Text = "CANCEL"
        '
        'FTDescription
        '
        Me.FTDescription.EditValue = ""
        Me.FTDescription.Location = New System.Drawing.Point(137, 34)
        Me.FTDescription.Name = "FTDescription"
        Me.FTDescription.Properties.MaxLength = 500
        Me.FTDescription.Size = New System.Drawing.Size(604, 149)
        Me.FTDescription.TabIndex = 1
        Me.FTDescription.Tag = "2|"
        '
        'FTDescription_lbl
        '
        Me.FTDescription_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTDescription_lbl.Appearance.Options.UseForeColor = True
        Me.FTDescription_lbl.Appearance.Options.UseTextOptions = True
        Me.FTDescription_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTDescription_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTDescription_lbl.Location = New System.Drawing.Point(10, 36)
        Me.FTDescription_lbl.Name = "FTDescription_lbl"
        Me.FTDescription_lbl.Size = New System.Drawing.Size(122, 19)
        Me.FTDescription_lbl.TabIndex = 285
        Me.FTDescription_lbl.Tag = "2|"
        Me.FTDescription_lbl.Text = "Description :"
        '
        'FTNote
        '
        Me.FTNote.EditValue = ""
        Me.FTNote.Location = New System.Drawing.Point(137, 214)
        Me.FTNote.Name = "FTNote"
        Me.FTNote.Properties.MaxLength = 500
        Me.FTNote.Size = New System.Drawing.Size(604, 75)
        Me.FTNote.TabIndex = 4
        Me.FTNote.TabStop = False
        Me.FTNote.Tag = "2|"
        '
        'FTNote_lbl
        '
        Me.FTNote_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTNote_lbl.Appearance.Options.UseForeColor = True
        Me.FTNote_lbl.Appearance.Options.UseTextOptions = True
        Me.FTNote_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTNote_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTNote_lbl.Location = New System.Drawing.Point(31, 215)
        Me.FTNote_lbl.Name = "FTNote_lbl"
        Me.FTNote_lbl.Size = New System.Drawing.Size(101, 19)
        Me.FTNote_lbl.TabIndex = 287
        Me.FTNote_lbl.Tag = "2|"
        Me.FTNote_lbl.Text = "Remark :"
        '
        'wPurchaseServiceAddItem
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(854, 347)
        Me.ControlBox = False
        Me.Controls.Add(Me.FTNote_lbl)
        Me.Controls.Add(Me.FTNote)
        Me.Controls.Add(Me.FTDescription_lbl)
        Me.Controls.Add(Me.FTDescription)
        Me.Controls.Add(Me.ocmcancel)
        Me.Controls.Add(Me.ocmadd)
        Me.Controls.Add(Me.FNAmount)
        Me.Controls.Add(Me.FNNetAmt_lbl)
        Me.Controls.Add(Me.FNPrice)
        Me.Controls.Add(Me.FNQuantity)
        Me.Controls.Add(Me.FNQuantity_lbl)
        Me.Controls.Add(Me.FNPrice_lbl)
        Me.Controls.Add(Me.FTOrderNo_lbl)
        Me.Controls.Add(Me.FTOrderNo)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "wPurchaseServiceAddItem"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Add Item"
        CType(Me.FTOrderNo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNQuantity.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNPrice.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNAmount.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTDescription.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTNote.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents FTOrderNo_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTOrderNo As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FNPrice_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNQuantity_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNQuantity As DevExpress.XtraEditors.CalcEdit
    Friend WithEvents FNPrice As DevExpress.XtraEditors.CalcEdit
    Friend WithEvents FNNetAmt_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNAmount As DevExpress.XtraEditors.CalcEdit
    Friend WithEvents ocmadd As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmcancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FTDescription As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents FTDescription_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTNote As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents FTNote_lbl As DevExpress.XtraEditors.LabelControl
End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wAddDivertOrderSubComponent
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
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject4 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.FNSeq = New DevExpress.XtraEditors.CalcEdit()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.ocmcancel = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmadd = New DevExpress.XtraEditors.SimpleButton()
        Me.FNConSmp = New DevExpress.XtraEditors.CalcEdit()
        Me.FNConSmp_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTRemark_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTComponent_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTRemark = New DevExpress.XtraEditors.MemoEdit()
        Me.FTComponent = New DevExpress.XtraEditors.MemoEdit()
        Me.FNHSysMerMatId_None_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysMerMatId_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysMerMatId_None = New DevExpress.XtraEditors.MemoEdit()
        Me.FNHSysMerMatId = New DevExpress.XtraEditors.ButtonEdit()
        CType(Me.FNSeq.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNConSmp.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTRemark.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTComponent.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysMerMatId_None.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysMerMatId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'FNSeq
        '
        Me.FNSeq.EnterMoveNextControl = True
        Me.FNSeq.Location = New System.Drawing.Point(188, 13)
        Me.FNSeq.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNSeq.Name = "FNSeq"
        Me.FNSeq.Properties.Appearance.Options.UseTextOptions = True
        Me.FNSeq.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNSeq.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNSeq.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNSeq.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNSeq.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNSeq.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNSeq.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNSeq.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNSeq.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNSeq.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, True, False, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject2, "", Nothing, Nothing, True)})
        Me.FNSeq.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNSeq.Properties.Precision = 0
        Me.FNSeq.Size = New System.Drawing.Size(126, 22)
        Me.FNSeq.TabIndex = 288
        Me.FNSeq.Tag = "2|"
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.LabelControl1.Appearance.Options.UseForeColor = True
        Me.LabelControl1.Appearance.Options.UseTextOptions = True
        Me.LabelControl1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LabelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LabelControl1.Location = New System.Drawing.Point(15, 13)
        Me.LabelControl1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(166, 23)
        Me.LabelControl1.TabIndex = 301
        Me.LabelControl1.Tag = "2|"
        Me.LabelControl1.Text = "Seq :"
        '
        'ocmcancel
        '
        Me.ocmcancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ocmcancel.Location = New System.Drawing.Point(683, 353)
        Me.ocmcancel.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmcancel.Name = "ocmcancel"
        Me.ocmcancel.Size = New System.Drawing.Size(169, 33)
        Me.ocmcancel.TabIndex = 294
        Me.ocmcancel.Text = "CANCEL"
        '
        'ocmadd
        '
        Me.ocmadd.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ocmadd.Location = New System.Drawing.Point(185, 353)
        Me.ocmadd.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmadd.Name = "ocmadd"
        Me.ocmadd.Size = New System.Drawing.Size(169, 33)
        Me.ocmadd.TabIndex = 293
        Me.ocmadd.Text = "ADD MATERIAL"
        '
        'FNConSmp
        '
        Me.FNConSmp.EnterMoveNextControl = True
        Me.FNConSmp.Location = New System.Drawing.Point(633, 41)
        Me.FNConSmp.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNConSmp.Name = "FNConSmp"
        Me.FNConSmp.Properties.Appearance.Options.UseTextOptions = True
        Me.FNConSmp.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNConSmp.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNConSmp.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNConSmp.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNConSmp.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNConSmp.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNConSmp.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNConSmp.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNConSmp.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNConSmp.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, True, False, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject3, "", Nothing, Nothing, True)})
        Me.FNConSmp.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNConSmp.Properties.Precision = 4
        Me.FNConSmp.Size = New System.Drawing.Size(126, 22)
        Me.FNConSmp.TabIndex = 290
        Me.FNConSmp.Tag = "2|"
        '
        'FNConSmp_lbl
        '
        Me.FNConSmp_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNConSmp_lbl.Appearance.Options.UseForeColor = True
        Me.FNConSmp_lbl.Appearance.Options.UseTextOptions = True
        Me.FNConSmp_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNConSmp_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNConSmp_lbl.Location = New System.Drawing.Point(460, 41)
        Me.FNConSmp_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNConSmp_lbl.Name = "FNConSmp_lbl"
        Me.FNConSmp_lbl.Size = New System.Drawing.Size(166, 23)
        Me.FNConSmp_lbl.TabIndex = 300
        Me.FNConSmp_lbl.Tag = "2|"
        Me.FNConSmp_lbl.Text = "Comsumption :"
        '
        'FTRemark_lbl
        '
        Me.FTRemark_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTRemark_lbl.Appearance.Options.UseForeColor = True
        Me.FTRemark_lbl.Appearance.Options.UseTextOptions = True
        Me.FTRemark_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTRemark_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTRemark_lbl.Location = New System.Drawing.Point(12, 240)
        Me.FTRemark_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTRemark_lbl.Name = "FTRemark_lbl"
        Me.FTRemark_lbl.Size = New System.Drawing.Size(171, 23)
        Me.FTRemark_lbl.TabIndex = 299
        Me.FTRemark_lbl.Tag = "2|"
        Me.FTRemark_lbl.Text = "Remark :"
        '
        'FTComponent_lbl
        '
        Me.FTComponent_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTComponent_lbl.Appearance.Options.UseForeColor = True
        Me.FTComponent_lbl.Appearance.Options.UseTextOptions = True
        Me.FTComponent_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTComponent_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTComponent_lbl.Location = New System.Drawing.Point(12, 154)
        Me.FTComponent_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTComponent_lbl.Name = "FTComponent_lbl"
        Me.FTComponent_lbl.Size = New System.Drawing.Size(171, 23)
        Me.FTComponent_lbl.TabIndex = 298
        Me.FTComponent_lbl.Tag = "2|"
        Me.FTComponent_lbl.Text = "Component :"
        '
        'FTRemark
        '
        Me.FTRemark.EditValue = ""
        Me.FTRemark.Location = New System.Drawing.Point(188, 245)
        Me.FTRemark.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTRemark.Name = "FTRemark"
        Me.FTRemark.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FTRemark.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FTRemark.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FTRemark.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FTRemark.Properties.MaxLength = 500
        Me.FTRemark.Size = New System.Drawing.Size(664, 84)
        Me.FTRemark.TabIndex = 292
        Me.FTRemark.Tag = "2|"
        '
        'FTComponent
        '
        Me.FTComponent.EditValue = ""
        Me.FTComponent.Location = New System.Drawing.Point(188, 157)
        Me.FTComponent.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTComponent.Name = "FTComponent"
        Me.FTComponent.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FTComponent.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FTComponent.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FTComponent.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FTComponent.Properties.MaxLength = 500
        Me.FTComponent.Size = New System.Drawing.Size(664, 84)
        Me.FTComponent.TabIndex = 291
        Me.FTComponent.Tag = "2|"
        '
        'FNHSysMerMatId_None_lbl
        '
        Me.FNHSysMerMatId_None_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FNHSysMerMatId_None_lbl.Appearance.Options.UseForeColor = True
        Me.FNHSysMerMatId_None_lbl.Appearance.Options.UseTextOptions = True
        Me.FNHSysMerMatId_None_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysMerMatId_None_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysMerMatId_None_lbl.Location = New System.Drawing.Point(12, 64)
        Me.FNHSysMerMatId_None_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysMerMatId_None_lbl.Name = "FNHSysMerMatId_None_lbl"
        Me.FNHSysMerMatId_None_lbl.Size = New System.Drawing.Size(171, 23)
        Me.FNHSysMerMatId_None_lbl.TabIndex = 297
        Me.FNHSysMerMatId_None_lbl.Tag = "2|"
        Me.FNHSysMerMatId_None_lbl.Text = "Material Description :"
        '
        'FNHSysMerMatId_lbl
        '
        Me.FNHSysMerMatId_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysMerMatId_lbl.Appearance.Options.UseForeColor = True
        Me.FNHSysMerMatId_lbl.Appearance.Options.UseTextOptions = True
        Me.FNHSysMerMatId_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysMerMatId_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysMerMatId_lbl.Location = New System.Drawing.Point(42, 37)
        Me.FNHSysMerMatId_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysMerMatId_lbl.Name = "FNHSysMerMatId_lbl"
        Me.FNHSysMerMatId_lbl.Size = New System.Drawing.Size(141, 23)
        Me.FNHSysMerMatId_lbl.TabIndex = 296
        Me.FNHSysMerMatId_lbl.Tag = "2|"
        Me.FNHSysMerMatId_lbl.Text = "Material Code :"
        '
        'FNHSysMerMatId_None
        '
        Me.FNHSysMerMatId_None.EditValue = ""
        Me.FNHSysMerMatId_None.Location = New System.Drawing.Point(188, 69)
        Me.FNHSysMerMatId_None.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysMerMatId_None.Name = "FNHSysMerMatId_None"
        Me.FNHSysMerMatId_None.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysMerMatId_None.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysMerMatId_None.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNHSysMerMatId_None.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNHSysMerMatId_None.Properties.ReadOnly = True
        Me.FNHSysMerMatId_None.Size = New System.Drawing.Size(664, 84)
        Me.FNHSysMerMatId_None.TabIndex = 295
        Me.FNHSysMerMatId_None.Tag = "2|"
        '
        'FNHSysMerMatId
        '
        Me.FNHSysMerMatId.EnterMoveNextControl = True
        Me.FNHSysMerMatId.Location = New System.Drawing.Point(188, 41)
        Me.FNHSysMerMatId.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysMerMatId.Name = "FNHSysMerMatId"
        Me.FNHSysMerMatId.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FNHSysMerMatId.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysMerMatId.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysMerMatId.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysMerMatId.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FNHSysMerMatId.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FNHSysMerMatId.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNHSysMerMatId.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysMerMatId.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNHSysMerMatId.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNHSysMerMatId.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysMerMatId.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysMerMatId.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNHSysMerMatId.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNHSysMerMatId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", 15, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject4, "", "98", Nothing, True)})
        Me.FNHSysMerMatId.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.FNHSysMerMatId.Properties.MaxLength = 30
        Me.FNHSysMerMatId.Size = New System.Drawing.Size(167, 22)
        Me.FNHSysMerMatId.TabIndex = 289
        Me.FNHSysMerMatId.Tag = "2|"
        '
        'wAddDivertOrderSubComponent
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(976, 406)
        Me.Controls.Add(Me.FNSeq)
        Me.Controls.Add(Me.LabelControl1)
        Me.Controls.Add(Me.ocmcancel)
        Me.Controls.Add(Me.ocmadd)
        Me.Controls.Add(Me.FNConSmp)
        Me.Controls.Add(Me.FNConSmp_lbl)
        Me.Controls.Add(Me.FTRemark_lbl)
        Me.Controls.Add(Me.FTComponent_lbl)
        Me.Controls.Add(Me.FTRemark)
        Me.Controls.Add(Me.FTComponent)
        Me.Controls.Add(Me.FNHSysMerMatId_None_lbl)
        Me.Controls.Add(Me.FNHSysMerMatId_lbl)
        Me.Controls.Add(Me.FNHSysMerMatId_None)
        Me.Controls.Add(Me.FNHSysMerMatId)
        Me.Name = "wAddDivertOrderSubComponent"
        Me.Text = "wAddDivertOrderSubComponent"
        CType(Me.FNSeq.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNConSmp.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTRemark.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTComponent.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysMerMatId_None.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysMerMatId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents FNSeq As DevExpress.XtraEditors.CalcEdit
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ocmcancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmadd As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FNConSmp As DevExpress.XtraEditors.CalcEdit
    Friend WithEvents FNConSmp_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTRemark_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTComponent_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTRemark As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents FTComponent As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents FNHSysMerMatId_None_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysMerMatId_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysMerMatId_None As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents FNHSysMerMatId As DevExpress.XtraEditors.ButtonEdit
End Class

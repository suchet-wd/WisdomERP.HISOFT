<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wCopyDivertOrder
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
        Me.ocmok = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmcancel = New DevExpress.XtraEditors.SimpleButton()
        Me.ogbOptRptOrderNo = New DevExpress.XtraEditors.GroupControl()
        Me.ockcomponent = New DevExpress.XtraEditors.CheckEdit()
        Me.ockSizeSpec = New DevExpress.XtraEditors.CheckEdit()
        Me.ockPacking = New DevExpress.XtraEditors.CheckEdit()
        Me.ockSewing = New DevExpress.XtraEditors.CheckEdit()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.FNDivertSeq = New DevExpress.XtraEditors.ButtonEdit()
        Me.FTSubOrderNo = New DevExpress.XtraEditors.ButtonEdit()
        Me.FTSubOrderNo_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTOrderNo = New DevExpress.XtraEditors.ButtonEdit()
        Me.FTOrderNo_lbl = New DevExpress.XtraEditors.LabelControl()
        CType(Me.ogbOptRptOrderNo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbOptRptOrderNo.SuspendLayout()
        CType(Me.ockcomponent.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ockSizeSpec.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ockPacking.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ockSewing.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.FNDivertSeq.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTSubOrderNo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTOrderNo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ocmok
        '
        Me.ocmok.Location = New System.Drawing.Point(89, 262)
        Me.ocmok.Name = "ocmok"
        Me.ocmok.Size = New System.Drawing.Size(91, 31)
        Me.ocmok.TabIndex = 426
        Me.ocmok.Text = "OK"
        '
        'ocmcancel
        '
        Me.ocmcancel.Location = New System.Drawing.Point(276, 262)
        Me.ocmcancel.Name = "ocmcancel"
        Me.ocmcancel.Size = New System.Drawing.Size(91, 31)
        Me.ocmcancel.TabIndex = 427
        Me.ocmcancel.Text = "Cancel"
        '
        'ogbOptRptOrderNo
        '
        Me.ogbOptRptOrderNo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbOptRptOrderNo.Controls.Add(Me.ockcomponent)
        Me.ogbOptRptOrderNo.Controls.Add(Me.ockSizeSpec)
        Me.ogbOptRptOrderNo.Controls.Add(Me.ockPacking)
        Me.ogbOptRptOrderNo.Controls.Add(Me.ockSewing)
        Me.ogbOptRptOrderNo.Location = New System.Drawing.Point(1, 101)
        Me.ogbOptRptOrderNo.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbOptRptOrderNo.Name = "ogbOptRptOrderNo"
        Me.ogbOptRptOrderNo.Size = New System.Drawing.Size(452, 154)
        Me.ogbOptRptOrderNo.TabIndex = 430
        Me.ogbOptRptOrderNo.Text = "Option"
        '
        'ockcomponent
        '
        Me.ockcomponent.EditValue = "1"
        Me.ockcomponent.Location = New System.Drawing.Point(87, 34)
        Me.ockcomponent.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ockcomponent.Name = "ockcomponent"
        Me.ockcomponent.Properties.Caption = "Component"
        Me.ockcomponent.Properties.Tag = "FTStateEmb"
        Me.ockcomponent.Properties.ValueChecked = "1"
        Me.ockcomponent.Properties.ValueUnchecked = "0"
        Me.ockcomponent.Size = New System.Drawing.Size(280, 20)
        Me.ockcomponent.TabIndex = 0
        Me.ockcomponent.Tag = "2|"
        '
        'ockSizeSpec
        '
        Me.ockSizeSpec.EditValue = "1"
        Me.ockSizeSpec.Location = New System.Drawing.Point(87, 122)
        Me.ockSizeSpec.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ockSizeSpec.Name = "ockSizeSpec"
        Me.ockSizeSpec.Properties.Caption = "Size Spec"
        Me.ockSizeSpec.Properties.Tag = "FTStateEmb"
        Me.ockSizeSpec.Properties.ValueChecked = "1"
        Me.ockSizeSpec.Properties.ValueUnchecked = "0"
        Me.ockSizeSpec.Size = New System.Drawing.Size(280, 20)
        Me.ockSizeSpec.TabIndex = 3
        Me.ockSizeSpec.Tag = "2|"
        '
        'ockPacking
        '
        Me.ockPacking.EditValue = "1"
        Me.ockPacking.Location = New System.Drawing.Point(87, 94)
        Me.ockPacking.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ockPacking.Name = "ockPacking"
        Me.ockPacking.Properties.Caption = "Packing"
        Me.ockPacking.Properties.Tag = "FTStateEmb"
        Me.ockPacking.Properties.ValueChecked = "1"
        Me.ockPacking.Properties.ValueUnchecked = "0"
        Me.ockPacking.Size = New System.Drawing.Size(280, 20)
        Me.ockPacking.TabIndex = 2
        Me.ockPacking.Tag = "2|"
        '
        'ockSewing
        '
        Me.ockSewing.EditValue = "1"
        Me.ockSewing.Location = New System.Drawing.Point(87, 64)
        Me.ockSewing.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ockSewing.Name = "ockSewing"
        Me.ockSewing.Properties.Caption = "Sewing"
        Me.ockSewing.Properties.Tag = "FTStateEmb"
        Me.ockSewing.Properties.ValueChecked = "1"
        Me.ockSewing.Properties.ValueUnchecked = "0"
        Me.ockSewing.Size = New System.Drawing.Size(280, 20)
        Me.ockSewing.TabIndex = 1
        Me.ockSewing.Tag = "2|"
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Me.LabelControl1)
        Me.GroupControl1.Controls.Add(Me.FNDivertSeq)
        Me.GroupControl1.Controls.Add(Me.FTSubOrderNo)
        Me.GroupControl1.Controls.Add(Me.FTSubOrderNo_lbl)
        Me.GroupControl1.Controls.Add(Me.FTOrderNo)
        Me.GroupControl1.Controls.Add(Me.FTOrderNo_lbl)
        Me.GroupControl1.Location = New System.Drawing.Point(1, 1)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.ShowCaption = False
        Me.GroupControl1.Size = New System.Drawing.Size(452, 93)
        Me.GroupControl1.TabIndex = 431
        Me.GroupControl1.Text = "GroupControl1"
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.ForeColor = System.Drawing.Color.Black
        Me.LabelControl1.Appearance.Options.UseForeColor = True
        Me.LabelControl1.Appearance.Options.UseTextOptions = True
        Me.LabelControl1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LabelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LabelControl1.Location = New System.Drawing.Point(27, 64)
        Me.LabelControl1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(152, 22)
        Me.LabelControl1.TabIndex = 435
        Me.LabelControl1.Tag = "2|"
        Me.LabelControl1.Text = "DivertSeq :"
        '
        'FNDivertSeq
        '
        Me.FNDivertSeq.Location = New System.Drawing.Point(183, 64)
        Me.FNDivertSeq.Name = "FNDivertSeq"
        Me.FNDivertSeq.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", "507", Nothing, True)})
        Me.FNDivertSeq.Size = New System.Drawing.Size(169, 22)
        Me.FNDivertSeq.TabIndex = 434
        '
        'FTSubOrderNo
        '
        Me.FTSubOrderNo.Location = New System.Drawing.Point(183, 35)
        Me.FTSubOrderNo.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTSubOrderNo.Name = "FTSubOrderNo"
        Me.FTSubOrderNo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject2, "", "133", Nothing, True), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Plus, "", -1, True, False, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject3, "", "", Nothing, True)})
        Me.FTSubOrderNo.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.FTSubOrderNo.Properties.Tag = ""
        Me.FTSubOrderNo.Size = New System.Drawing.Size(169, 22)
        Me.FTSubOrderNo.TabIndex = 432
        Me.FTSubOrderNo.Tag = "2|"
        '
        'FTSubOrderNo_lbl
        '
        Me.FTSubOrderNo_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTSubOrderNo_lbl.Appearance.Options.UseForeColor = True
        Me.FTSubOrderNo_lbl.Appearance.Options.UseTextOptions = True
        Me.FTSubOrderNo_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTSubOrderNo_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTSubOrderNo_lbl.Location = New System.Drawing.Point(27, 35)
        Me.FTSubOrderNo_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTSubOrderNo_lbl.Name = "FTSubOrderNo_lbl"
        Me.FTSubOrderNo_lbl.Size = New System.Drawing.Size(152, 22)
        Me.FTSubOrderNo_lbl.TabIndex = 433
        Me.FTSubOrderNo_lbl.Tag = "2|"
        Me.FTSubOrderNo_lbl.Text = "FTSubOrderNo :"
        '
        'FTOrderNo
        '
        Me.FTOrderNo.Location = New System.Drawing.Point(183, 5)
        Me.FTOrderNo.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTOrderNo.Name = "FTOrderNo"
        Me.FTOrderNo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject4, "", "121", Nothing, True), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Plus, "", -1, True, False, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.F2), SerializableAppearanceObject5, "", "", Nothing, True)})
        Me.FTOrderNo.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.FTOrderNo.Properties.Tag = ""
        Me.FTOrderNo.Size = New System.Drawing.Size(169, 22)
        Me.FTOrderNo.TabIndex = 430
        Me.FTOrderNo.Tag = "2|"
        '
        'FTOrderNo_lbl
        '
        Me.FTOrderNo_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTOrderNo_lbl.Appearance.Options.UseForeColor = True
        Me.FTOrderNo_lbl.Appearance.Options.UseTextOptions = True
        Me.FTOrderNo_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTOrderNo_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTOrderNo_lbl.Location = New System.Drawing.Point(31, 6)
        Me.FTOrderNo_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTOrderNo_lbl.Name = "FTOrderNo_lbl"
        Me.FTOrderNo_lbl.Size = New System.Drawing.Size(148, 22)
        Me.FTOrderNo_lbl.TabIndex = 431
        Me.FTOrderNo_lbl.Tag = "2|"
        Me.FTOrderNo_lbl.Text = "FTOrderNo :"
        '
        'wCopyDivertOrder
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(456, 304)
        Me.Controls.Add(Me.GroupControl1)
        Me.Controls.Add(Me.ogbOptRptOrderNo)
        Me.Controls.Add(Me.ocmcancel)
        Me.Controls.Add(Me.ocmok)
        Me.Name = "wCopyDivertOrder"
        Me.Text = "CopyDivertOrder"
        CType(Me.ogbOptRptOrderNo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbOptRptOrderNo.ResumeLayout(False)
        CType(Me.ockcomponent.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ockSizeSpec.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ockPacking.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ockSewing.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        CType(Me.FNDivertSeq.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTSubOrderNo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTOrderNo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ocmok As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmcancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ogbOptRptOrderNo As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ockcomponent As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents ockSizeSpec As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents ockPacking As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents ockSewing As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNDivertSeq As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FTSubOrderNo As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FTSubOrderNo_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTOrderNo As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FTOrderNo_lbl As DevExpress.XtraEditors.LabelControl
End Class

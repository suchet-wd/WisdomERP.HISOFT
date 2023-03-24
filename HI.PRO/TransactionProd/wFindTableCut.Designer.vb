<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wFindTableCut
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
        Me.FNSTableCutNo_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTOrderProdNo = New DevExpress.XtraEditors.TextEdit()
        Me.FTOrderProdNo_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNETableCutNo_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNTableCutNo = New DevExpress.XtraEditors.CalcEdit()
        Me.FNTableCutNo_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.ocmcancel = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmfind = New DevExpress.XtraEditors.SimpleButton()
        Me.FNSTableCutNo = New DevExpress.XtraEditors.CalcEdit()
        Me.FNETableCutNo = New DevExpress.XtraEditors.CalcEdit()
        CType(Me.FTOrderProdNo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNTableCutNo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNSTableCutNo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNETableCutNo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'FNSTableCutNo_lbl
        '
        Me.FNSTableCutNo_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNSTableCutNo_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNSTableCutNo_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNSTableCutNo_lbl.Location = New System.Drawing.Point(53, 49)
        Me.FNSTableCutNo_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNSTableCutNo_lbl.Name = "FNSTableCutNo_lbl"
        Me.FNSTableCutNo_lbl.Size = New System.Drawing.Size(177, 25)
        Me.FNSTableCutNo_lbl.TabIndex = 305
        Me.FNSTableCutNo_lbl.Tag = "2|"
        Me.FNSTableCutNo_lbl.Text = "Table Cut Start No :"
        '
        'FTOrderProdNo
        '
        Me.FTOrderProdNo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FTOrderProdNo.Location = New System.Drawing.Point(237, 22)
        Me.FTOrderProdNo.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTOrderProdNo.Name = "FTOrderProdNo"
        Me.FTOrderProdNo.Properties.Appearance.BackColor = System.Drawing.Color.LightCyan
        Me.FTOrderProdNo.Properties.Appearance.Options.UseBackColor = True
        Me.FTOrderProdNo.Properties.ReadOnly = True
        Me.FTOrderProdNo.Size = New System.Drawing.Size(259, 22)
        Me.FTOrderProdNo.TabIndex = 302
        Me.FTOrderProdNo.Tag = "2|"
        '
        'FTOrderProdNo_lbl
        '
        Me.FTOrderProdNo_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTOrderProdNo_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTOrderProdNo_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTOrderProdNo_lbl.Location = New System.Drawing.Point(54, 22)
        Me.FTOrderProdNo_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTOrderProdNo_lbl.Name = "FTOrderProdNo_lbl"
        Me.FTOrderProdNo_lbl.Size = New System.Drawing.Size(177, 25)
        Me.FTOrderProdNo_lbl.TabIndex = 303
        Me.FTOrderProdNo_lbl.Tag = "2|"
        Me.FTOrderProdNo_lbl.Text = "Order Production No :"
        '
        'FNETableCutNo_lbl
        '
        Me.FNETableCutNo_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNETableCutNo_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNETableCutNo_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNETableCutNo_lbl.Location = New System.Drawing.Point(52, 76)
        Me.FNETableCutNo_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNETableCutNo_lbl.Name = "FNETableCutNo_lbl"
        Me.FNETableCutNo_lbl.Size = New System.Drawing.Size(177, 25)
        Me.FNETableCutNo_lbl.TabIndex = 301
        Me.FNETableCutNo_lbl.Tag = "2|"
        Me.FNETableCutNo_lbl.Text = "Table Cut End No :"
        '
        'FNTableCutNo
        '
        Me.FNTableCutNo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FNTableCutNo.EnterMoveNextControl = True
        Me.FNTableCutNo.Location = New System.Drawing.Point(237, 105)
        Me.FNTableCutNo.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNTableCutNo.Name = "FNTableCutNo"
        Me.FNTableCutNo.Properties.Appearance.Options.UseTextOptions = True
        Me.FNTableCutNo.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNTableCutNo.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNTableCutNo.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNTableCutNo.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNTableCutNo.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNTableCutNo.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNTableCutNo.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNTableCutNo.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNTableCutNo.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNTableCutNo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, True, False, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", Nothing, Nothing, True)})
        Me.FNTableCutNo.Properties.DisplayFormat.FormatString = "{0:n0}"
        Me.FNTableCutNo.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNTableCutNo.Properties.EditFormat.FormatString = "{0:n0}"
        Me.FNTableCutNo.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNTableCutNo.Properties.Precision = 0
        Me.FNTableCutNo.Size = New System.Drawing.Size(137, 22)
        Me.FNTableCutNo.TabIndex = 306
        Me.FNTableCutNo.Tag = "2|"
        '
        'FNTableCutNo_lbl
        '
        Me.FNTableCutNo_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNTableCutNo_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNTableCutNo_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNTableCutNo_lbl.Location = New System.Drawing.Point(51, 106)
        Me.FNTableCutNo_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNTableCutNo_lbl.Name = "FNTableCutNo_lbl"
        Me.FNTableCutNo_lbl.Size = New System.Drawing.Size(180, 23)
        Me.FNTableCutNo_lbl.TabIndex = 307
        Me.FNTableCutNo_lbl.Tag = "2|"
        Me.FNTableCutNo_lbl.Text = "ค้นหาโต๊ะหมายเลข :"
        '
        'ocmcancel
        '
        Me.ocmcancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmcancel.Location = New System.Drawing.Point(347, 154)
        Me.ocmcancel.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmcancel.Name = "ocmcancel"
        Me.ocmcancel.Size = New System.Drawing.Size(141, 31)
        Me.ocmcancel.TabIndex = 312
        Me.ocmcancel.TabStop = False
        Me.ocmcancel.Tag = "2|"
        Me.ocmcancel.Text = "Cancel"
        '
        'ocmfind
        '
        Me.ocmfind.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmfind.Location = New System.Drawing.Point(106, 154)
        Me.ocmfind.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmfind.Name = "ocmfind"
        Me.ocmfind.Size = New System.Drawing.Size(138, 31)
        Me.ocmfind.TabIndex = 311
        Me.ocmfind.TabStop = False
        Me.ocmfind.Tag = "2|"
        Me.ocmfind.Text = "Search"
        '
        'FNSTableCutNo
        '
        Me.FNSTableCutNo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FNSTableCutNo.EnterMoveNextControl = True
        Me.FNSTableCutNo.Location = New System.Drawing.Point(238, 50)
        Me.FNSTableCutNo.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNSTableCutNo.Name = "FNSTableCutNo"
        Me.FNSTableCutNo.Properties.Appearance.Options.UseTextOptions = True
        Me.FNSTableCutNo.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNSTableCutNo.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNSTableCutNo.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNSTableCutNo.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNSTableCutNo.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNSTableCutNo.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNSTableCutNo.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNSTableCutNo.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNSTableCutNo.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNSTableCutNo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, True, False, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject2, "", Nothing, Nothing, True)})
        Me.FNSTableCutNo.Properties.DisplayFormat.FormatString = "{0:n0}"
        Me.FNSTableCutNo.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNSTableCutNo.Properties.EditFormat.FormatString = "{0:n0}"
        Me.FNSTableCutNo.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNSTableCutNo.Properties.Precision = 0
        Me.FNSTableCutNo.Properties.ReadOnly = True
        Me.FNSTableCutNo.Size = New System.Drawing.Size(137, 22)
        Me.FNSTableCutNo.TabIndex = 313
        Me.FNSTableCutNo.Tag = "2|"
        '
        'FNETableCutNo
        '
        Me.FNETableCutNo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FNETableCutNo.EnterMoveNextControl = True
        Me.FNETableCutNo.Location = New System.Drawing.Point(238, 77)
        Me.FNETableCutNo.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNETableCutNo.Name = "FNETableCutNo"
        Me.FNETableCutNo.Properties.Appearance.Options.UseTextOptions = True
        Me.FNETableCutNo.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNETableCutNo.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNETableCutNo.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNETableCutNo.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNETableCutNo.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNETableCutNo.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNETableCutNo.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNETableCutNo.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNETableCutNo.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNETableCutNo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, True, False, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject3, "", Nothing, Nothing, True)})
        Me.FNETableCutNo.Properties.DisplayFormat.FormatString = "{0:n0}"
        Me.FNETableCutNo.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNETableCutNo.Properties.EditFormat.FormatString = "{0:n0}"
        Me.FNETableCutNo.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNETableCutNo.Properties.Precision = 0
        Me.FNETableCutNo.Properties.ReadOnly = True
        Me.FNETableCutNo.Size = New System.Drawing.Size(137, 22)
        Me.FNETableCutNo.TabIndex = 314
        Me.FNETableCutNo.Tag = "2|"
        '
        'wFindTableCut
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(575, 209)
        Me.Controls.Add(Me.FNETableCutNo)
        Me.Controls.Add(Me.FNSTableCutNo)
        Me.Controls.Add(Me.ocmcancel)
        Me.Controls.Add(Me.ocmfind)
        Me.Controls.Add(Me.FNTableCutNo)
        Me.Controls.Add(Me.FNTableCutNo_lbl)
        Me.Controls.Add(Me.FNSTableCutNo_lbl)
        Me.Controls.Add(Me.FTOrderProdNo)
        Me.Controls.Add(Me.FTOrderProdNo_lbl)
        Me.Controls.Add(Me.FNETableCutNo_lbl)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "wFindTableCut"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Find Table Cut"
        CType(Me.FTOrderProdNo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNTableCutNo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNSTableCutNo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNETableCutNo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents FNSTableCutNo_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTOrderProdNo As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FTOrderProdNo_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNETableCutNo_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNTableCutNo As DevExpress.XtraEditors.CalcEdit
    Friend WithEvents FNTableCutNo_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ocmcancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmfind As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FNSTableCutNo As DevExpress.XtraEditors.CalcEdit
    Friend WithEvents FNETableCutNo As DevExpress.XtraEditors.CalcEdit
End Class

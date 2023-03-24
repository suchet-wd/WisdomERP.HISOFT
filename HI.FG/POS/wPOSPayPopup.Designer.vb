<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wPOSPayPopup
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
        Me.ogrpdetail = New DevExpress.XtraEditors.GroupControl()
        Me.ocmcancel = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmok = New DevExpress.XtraEditors.SimpleButton()
        Me.FNInvGrandAmt = New DevExpress.XtraEditors.CalcEdit()
        Me.FNChangeAmt = New DevExpress.XtraEditors.CalcEdit()
        Me.FNCashAmt = New DevExpress.XtraEditors.CalcEdit()
        Me.FNChangeAmt_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNCashAmt_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNPOGrandAmt_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        CType(Me.ogrpdetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogrpdetail.SuspendLayout()
        CType(Me.FNInvGrandAmt.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNChangeAmt.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNCashAmt.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ogrpdetail
        '
        Me.ogrpdetail.Controls.Add(Me.LabelControl2)
        Me.ogrpdetail.Controls.Add(Me.LabelControl1)
        Me.ogrpdetail.Controls.Add(Me.ocmcancel)
        Me.ogrpdetail.Controls.Add(Me.ocmok)
        Me.ogrpdetail.Controls.Add(Me.FNInvGrandAmt)
        Me.ogrpdetail.Controls.Add(Me.FNChangeAmt)
        Me.ogrpdetail.Controls.Add(Me.FNCashAmt)
        Me.ogrpdetail.Controls.Add(Me.FNChangeAmt_lbl)
        Me.ogrpdetail.Controls.Add(Me.FNCashAmt_lbl)
        Me.ogrpdetail.Controls.Add(Me.FNPOGrandAmt_lbl)
        Me.ogrpdetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogrpdetail.Location = New System.Drawing.Point(0, 0)
        Me.ogrpdetail.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogrpdetail.Name = "ogrpdetail"
        Me.ogrpdetail.Size = New System.Drawing.Size(612, 214)
        Me.ogrpdetail.TabIndex = 0
        '
        'ocmcancel
        '
        Me.ocmcancel.AllowFocus = False
        Me.ocmcancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmcancel.Cursor = System.Windows.Forms.Cursors.Default
        Me.ocmcancel.Location = New System.Drawing.Point(495, 162)
        Me.ocmcancel.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmcancel.Name = "ocmcancel"
        Me.ocmcancel.Size = New System.Drawing.Size(105, 43)
        Me.ocmcancel.TabIndex = 2
        Me.ocmcancel.TabStop = False
        Me.ocmcancel.Text = "CANCEL"
        '
        'ocmok
        '
        Me.ocmok.AllowFocus = False
        Me.ocmok.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmok.Location = New System.Drawing.Point(383, 162)
        Me.ocmok.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmok.Name = "ocmok"
        Me.ocmok.Size = New System.Drawing.Size(105, 43)
        Me.ocmok.TabIndex = 1
        Me.ocmok.TabStop = False
        Me.ocmok.Text = "OK"
        '
        'FNInvGrandAmt
        '
        Me.FNInvGrandAmt.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FNInvGrandAmt.EnterMoveNextControl = True
        Me.FNInvGrandAmt.Location = New System.Drawing.Point(209, 30)
        Me.FNInvGrandAmt.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNInvGrandAmt.Name = "FNInvGrandAmt"
        Me.FNInvGrandAmt.Properties.AllowFocused = False
        Me.FNInvGrandAmt.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 15.0!)
        Me.FNInvGrandAmt.Properties.Appearance.Options.UseFont = True
        Me.FNInvGrandAmt.Properties.Appearance.Options.UseTextOptions = True
        Me.FNInvGrandAmt.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNInvGrandAmt.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNInvGrandAmt.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Red
        Me.FNInvGrandAmt.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNInvGrandAmt.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNInvGrandAmt.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNInvGrandAmt.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Red
        Me.FNInvGrandAmt.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNInvGrandAmt.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNInvGrandAmt.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, True, False, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", Nothing, Nothing, True)})
        Me.FNInvGrandAmt.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNInvGrandAmt.Properties.Precision = 2
        Me.FNInvGrandAmt.Properties.ReadOnly = True
        Me.FNInvGrandAmt.Size = New System.Drawing.Size(390, 36)
        Me.FNInvGrandAmt.TabIndex = 288
        Me.FNInvGrandAmt.Tag = "2|"
        '
        'FNChangeAmt
        '
        Me.FNChangeAmt.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FNChangeAmt.Location = New System.Drawing.Point(209, 113)
        Me.FNChangeAmt.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNChangeAmt.Name = "FNChangeAmt"
        Me.FNChangeAmt.Properties.AllowFocused = False
        Me.FNChangeAmt.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 15.0!)
        Me.FNChangeAmt.Properties.Appearance.Options.UseFont = True
        Me.FNChangeAmt.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, True, False, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject2, "", Nothing, Nothing, True)})
        Me.FNChangeAmt.Properties.Precision = 2
        Me.FNChangeAmt.Properties.ReadOnly = True
        Me.FNChangeAmt.Size = New System.Drawing.Size(390, 36)
        Me.FNChangeAmt.TabIndex = 292
        Me.FNChangeAmt.Tag = "2|"
        '
        'FNCashAmt
        '
        Me.FNCashAmt.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FNCashAmt.Location = New System.Drawing.Point(209, 73)
        Me.FNCashAmt.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNCashAmt.Name = "FNCashAmt"
        Me.FNCashAmt.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 15.0!)
        Me.FNCashAmt.Properties.Appearance.Options.UseFont = True
        Me.FNCashAmt.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, True, False, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject3, "", Nothing, Nothing, True)})
        Me.FNCashAmt.Properties.Precision = 2
        Me.FNCashAmt.Size = New System.Drawing.Size(390, 36)
        Me.FNCashAmt.TabIndex = 0
        Me.FNCashAmt.Tag = "2|"
        '
        'FNChangeAmt_lbl
        '
        Me.FNChangeAmt_lbl.Appearance.Font = New System.Drawing.Font("Tahoma", 15.0!)
        Me.FNChangeAmt_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FNChangeAmt_lbl.Appearance.Options.UseFont = True
        Me.FNChangeAmt_lbl.Appearance.Options.UseForeColor = True
        Me.FNChangeAmt_lbl.Appearance.Options.UseTextOptions = True
        Me.FNChangeAmt_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNChangeAmt_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNChangeAmt_lbl.Location = New System.Drawing.Point(9, 114)
        Me.FNChangeAmt_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNChangeAmt_lbl.Name = "FNChangeAmt_lbl"
        Me.FNChangeAmt_lbl.Size = New System.Drawing.Size(198, 34)
        Me.FNChangeAmt_lbl.TabIndex = 289
        Me.FNChangeAmt_lbl.Tag = "2|"
        Me.FNChangeAmt_lbl.Text = "Change :"
        '
        'FNCashAmt_lbl
        '
        Me.FNCashAmt_lbl.Appearance.Font = New System.Drawing.Font("Tahoma", 15.0!)
        Me.FNCashAmt_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FNCashAmt_lbl.Appearance.Options.UseFont = True
        Me.FNCashAmt_lbl.Appearance.Options.UseForeColor = True
        Me.FNCashAmt_lbl.Appearance.Options.UseTextOptions = True
        Me.FNCashAmt_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNCashAmt_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNCashAmt_lbl.Location = New System.Drawing.Point(9, 76)
        Me.FNCashAmt_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNCashAmt_lbl.Name = "FNCashAmt_lbl"
        Me.FNCashAmt_lbl.Size = New System.Drawing.Size(198, 33)
        Me.FNCashAmt_lbl.TabIndex = 290
        Me.FNCashAmt_lbl.Tag = "2|"
        Me.FNCashAmt_lbl.Text = "Cash :"
        '
        'FNPOGrandAmt_lbl
        '
        Me.FNPOGrandAmt_lbl.Appearance.Font = New System.Drawing.Font("Tahoma", 15.0!)
        Me.FNPOGrandAmt_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FNPOGrandAmt_lbl.Appearance.Options.UseFont = True
        Me.FNPOGrandAmt_lbl.Appearance.Options.UseForeColor = True
        Me.FNPOGrandAmt_lbl.Appearance.Options.UseTextOptions = True
        Me.FNPOGrandAmt_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNPOGrandAmt_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNPOGrandAmt_lbl.Location = New System.Drawing.Point(7, 28)
        Me.FNPOGrandAmt_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNPOGrandAmt_lbl.Name = "FNPOGrandAmt_lbl"
        Me.FNPOGrandAmt_lbl.Size = New System.Drawing.Size(198, 38)
        Me.FNPOGrandAmt_lbl.TabIndex = 291
        Me.FNPOGrandAmt_lbl.Tag = "2|"
        Me.FNPOGrandAmt_lbl.Text = "Grand Total :"
        '
        'LabelControl1
        '
        Me.LabelControl1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LabelControl1.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.LabelControl1.Appearance.Options.UseForeColor = True
        Me.LabelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LabelControl1.Location = New System.Drawing.Point(12, 176)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(365, 16)
        Me.LabelControl1.TabIndex = 293
        Me.LabelControl1.Text = "* Enter = pay or Ok"
        '
        'LabelControl2
        '
        Me.LabelControl2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LabelControl2.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.LabelControl2.Appearance.Options.UseForeColor = True
        Me.LabelControl2.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LabelControl2.Location = New System.Drawing.Point(12, 193)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(365, 16)
        Me.LabelControl2.TabIndex = 293
        Me.LabelControl2.Text = "* Esc = Cancel or Close"
        '
        'wPOSPayPopup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(612, 214)
        Me.ControlBox = False
        Me.Controls.Add(Me.ogrpdetail)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "wPOSPayPopup"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "wPOSPayPopup"
        CType(Me.ogrpdetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogrpdetail.ResumeLayout(False)
        CType(Me.FNInvGrandAmt.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNChangeAmt.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNCashAmt.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogrpdetail As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ocmcancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmok As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FNInvGrandAmt As DevExpress.XtraEditors.CalcEdit
    Friend WithEvents FNChangeAmt As DevExpress.XtraEditors.CalcEdit
    Friend WithEvents FNCashAmt As DevExpress.XtraEditors.CalcEdit
    Friend WithEvents FNChangeAmt_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNCashAmt_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNPOGrandAmt_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
End Class

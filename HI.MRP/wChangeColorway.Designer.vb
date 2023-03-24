<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wChangeColorway
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
        Me.FTColorway_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTColorwayFrom = New DevExpress.XtraEditors.TextEdit()
        Me.FTColorway = New DevExpress.XtraEditors.ButtonEdit()
        Me.ocmnewcolorway = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmcancel = New DevExpress.XtraEditors.SimpleButton()
        Me.FTColorwayFrom_lbl = New DevExpress.XtraEditors.LabelControl()
        CType(Me.FTColorwayFrom.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTColorway.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'FTColorway_lbl
        '
        Me.FTColorway_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTColorway_lbl.Appearance.Options.UseForeColor = True
        Me.FTColorway_lbl.Appearance.Options.UseTextOptions = True
        Me.FTColorway_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTColorway_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTColorway_lbl.Location = New System.Drawing.Point(12, 50)
        Me.FTColorway_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTColorway_lbl.Name = "FTColorway_lbl"
        Me.FTColorway_lbl.Size = New System.Drawing.Size(127, 23)
        Me.FTColorway_lbl.TabIndex = 518
        Me.FTColorway_lbl.Tag = "2|"
        Me.FTColorway_lbl.Text = "To Colorway  :"
        '
        'FTColorwayFrom
        '
        Me.FTColorwayFrom.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FTColorwayFrom.Location = New System.Drawing.Point(145, 19)
        Me.FTColorwayFrom.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTColorwayFrom.Name = "FTColorwayFrom"
        Me.FTColorwayFrom.Properties.Appearance.BackColor = System.Drawing.Color.LightCyan
        Me.FTColorwayFrom.Properties.Appearance.Options.UseBackColor = True
        Me.FTColorwayFrom.Properties.ReadOnly = True
        Me.FTColorwayFrom.Size = New System.Drawing.Size(317, 23)
        Me.FTColorwayFrom.TabIndex = 519
        Me.FTColorwayFrom.Tag = "2|"
        '
        'FTColorway
        '
        Me.FTColorway.Location = New System.Drawing.Point(145, 50)
        Me.FTColorway.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTColorway.Name = "FTColorway"
        Me.FTColorway.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", "93", Nothing, True)})
        Me.FTColorway.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.FTColorway.Properties.Tag = ""
        Me.FTColorway.Size = New System.Drawing.Size(317, 23)
        Me.FTColorway.TabIndex = 517
        Me.FTColorway.Tag = "2|"
        '
        'ocmnewcolorway
        '
        Me.ocmnewcolorway.Location = New System.Drawing.Point(145, 95)
        Me.ocmnewcolorway.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmnewcolorway.Name = "ocmnewcolorway"
        Me.ocmnewcolorway.Size = New System.Drawing.Size(127, 28)
        Me.ocmnewcolorway.TabIndex = 520
        Me.ocmnewcolorway.Text = "Change Color Way"
        '
        'ocmcancel
        '
        Me.ocmcancel.Location = New System.Drawing.Point(335, 95)
        Me.ocmcancel.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmcancel.Name = "ocmcancel"
        Me.ocmcancel.Size = New System.Drawing.Size(127, 28)
        Me.ocmcancel.TabIndex = 521
        Me.ocmcancel.Text = "Cancel"
        '
        'FTColorwayFrom_lbl
        '
        Me.FTColorwayFrom_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTColorwayFrom_lbl.Appearance.Options.UseForeColor = True
        Me.FTColorwayFrom_lbl.Appearance.Options.UseTextOptions = True
        Me.FTColorwayFrom_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTColorwayFrom_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTColorwayFrom_lbl.Location = New System.Drawing.Point(12, 22)
        Me.FTColorwayFrom_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTColorwayFrom_lbl.Name = "FTColorwayFrom_lbl"
        Me.FTColorwayFrom_lbl.Size = New System.Drawing.Size(127, 23)
        Me.FTColorwayFrom_lbl.TabIndex = 522
        Me.FTColorwayFrom_lbl.Tag = "2|"
        Me.FTColorwayFrom_lbl.Text = "From Colorway  :"
        '
        'wChangeColorway
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(587, 147)
        Me.ControlBox = False
        Me.Controls.Add(Me.FTColorwayFrom_lbl)
        Me.Controls.Add(Me.ocmcancel)
        Me.Controls.Add(Me.ocmnewcolorway)
        Me.Controls.Add(Me.FTColorway_lbl)
        Me.Controls.Add(Me.FTColorwayFrom)
        Me.Controls.Add(Me.FTColorway)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "wChangeColorway"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Change Colorway"
        CType(Me.FTColorwayFrom.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTColorway.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents FTColorway_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTColorwayFrom As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FTColorway As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents ocmnewcolorway As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmcancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FTColorwayFrom_lbl As DevExpress.XtraEditors.LabelControl
End Class

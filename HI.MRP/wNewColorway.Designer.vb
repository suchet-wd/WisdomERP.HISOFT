<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wNewColorway
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
        Me.FTColorway_None = New DevExpress.XtraEditors.TextEdit()
        Me.FTColorway = New DevExpress.XtraEditors.ButtonEdit()
        Me.ocmnewcolorway = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmcancel = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.FTColorway_None.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTColorway.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'FTColorway_lbl
        '
        Me.FTColorway_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTColorway_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTColorway_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTColorway_lbl.Location = New System.Drawing.Point(48, 12)
        Me.FTColorway_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTColorway_lbl.Name = "FTColorway_lbl"
        Me.FTColorway_lbl.Size = New System.Drawing.Size(91, 23)
        Me.FTColorway_lbl.TabIndex = 518
        Me.FTColorway_lbl.Tag = "2|"
        Me.FTColorway_lbl.Text = "FTColorway  :"
        '
        'FTColorway_None
        '
        Me.FTColorway_None.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FTColorway_None.Location = New System.Drawing.Point(274, 12)
        Me.FTColorway_None.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTColorway_None.Name = "FTColorway_None"
        Me.FTColorway_None.Properties.Appearance.BackColor = System.Drawing.Color.LightCyan
        Me.FTColorway_None.Properties.Appearance.Options.UseBackColor = True
        Me.FTColorway_None.Properties.ReadOnly = True
        Me.FTColorway_None.Size = New System.Drawing.Size(269, 22)
        Me.FTColorway_None.TabIndex = 519
        Me.FTColorway_None.Tag = "2|"
        '
        'FTColorway
        '
        Me.FTColorway.Location = New System.Drawing.Point(145, 12)
        Me.FTColorway.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTColorway.Name = "FTColorway"
        Me.FTColorway.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", "93", Nothing, True)})
        Me.FTColorway.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.FTColorway.Properties.Tag = ""
        Me.FTColorway.Size = New System.Drawing.Size(127, 22)
        Me.FTColorway.TabIndex = 517
        Me.FTColorway.Tag = "2|"
        '
        'ocmnewcolorway
        '
        Me.ocmnewcolorway.Location = New System.Drawing.Point(145, 50)
        Me.ocmnewcolorway.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmnewcolorway.Name = "ocmnewcolorway"
        Me.ocmnewcolorway.Size = New System.Drawing.Size(127, 28)
        Me.ocmnewcolorway.TabIndex = 520
        Me.ocmnewcolorway.Text = "New Color Way"
        '
        'ocmcancel
        '
        Me.ocmcancel.Location = New System.Drawing.Point(331, 50)
        Me.ocmcancel.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmcancel.Name = "ocmcancel"
        Me.ocmcancel.Size = New System.Drawing.Size(127, 28)
        Me.ocmcancel.TabIndex = 521
        Me.ocmcancel.Text = "Cancel"
        '
        'wNewColorway
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(587, 91)
        Me.ControlBox = False
        Me.Controls.Add(Me.ocmcancel)
        Me.Controls.Add(Me.ocmnewcolorway)
        Me.Controls.Add(Me.FTColorway_lbl)
        Me.Controls.Add(Me.FTColorway_None)
        Me.Controls.Add(Me.FTColorway)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "wNewColorway"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "New Colorway"
        CType(Me.FTColorway_None.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTColorway.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents FTColorway_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTColorway_None As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FTColorway As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents ocmnewcolorway As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmcancel As DevExpress.XtraEditors.SimpleButton
End Class

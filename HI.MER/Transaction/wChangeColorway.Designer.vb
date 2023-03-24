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
        Me.FTColorway = New DevExpress.XtraEditors.ButtonEdit()
        Me.ocmok = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmcancel = New DevExpress.XtraEditors.SimpleButton()
        Me.FTColorwayFrom = New DevExpress.XtraEditors.TextEdit()
        Me.olbfrom = New DevExpress.XtraEditors.LabelControl()
        CType(Me.FTColorway.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTColorwayFrom.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'FTColorway_lbl
        '
        Me.FTColorway_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTColorway_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTColorway_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTColorway_lbl.Location = New System.Drawing.Point(297, 26)
        Me.FTColorway_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTColorway_lbl.Name = "FTColorway_lbl"
        Me.FTColorway_lbl.Size = New System.Drawing.Size(164, 23)
        Me.FTColorway_lbl.TabIndex = 518
        Me.FTColorway_lbl.Tag = "2|"
        Me.FTColorway_lbl.Text = "To Colorway  :"
        '
        'FTColorway
        '
        Me.FTColorway.Location = New System.Drawing.Point(468, 26)
        Me.FTColorway.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTColorway.Name = "FTColorway"
        Me.FTColorway.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", "93", Nothing, True)})
        Me.FTColorway.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.FTColorway.Properties.Tag = ""
        Me.FTColorway.Size = New System.Drawing.Size(127, 22)
        Me.FTColorway.TabIndex = 517
        Me.FTColorway.Tag = "2|"
        '
        'ocmok
        '
        Me.ocmok.Location = New System.Drawing.Point(166, 66)
        Me.ocmok.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmok.Name = "ocmok"
        Me.ocmok.Size = New System.Drawing.Size(127, 28)
        Me.ocmok.TabIndex = 520
        Me.ocmok.Text = "OK"
        '
        'ocmcancel
        '
        Me.ocmcancel.Location = New System.Drawing.Point(468, 66)
        Me.ocmcancel.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmcancel.Name = "ocmcancel"
        Me.ocmcancel.Size = New System.Drawing.Size(127, 28)
        Me.ocmcancel.TabIndex = 521
        Me.ocmcancel.Text = "Cancel"
        '
        'FTColorwayFrom
        '
        Me.FTColorwayFrom.Location = New System.Drawing.Point(164, 26)
        Me.FTColorwayFrom.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTColorwayFrom.Name = "FTColorwayFrom"
        Me.FTColorwayFrom.Properties.Appearance.BackColor = System.Drawing.Color.LightCyan
        Me.FTColorwayFrom.Properties.Appearance.Options.UseBackColor = True
        Me.FTColorwayFrom.Properties.ReadOnly = True
        Me.FTColorwayFrom.Size = New System.Drawing.Size(127, 22)
        Me.FTColorwayFrom.TabIndex = 522
        Me.FTColorwayFrom.Tag = "2|"
        '
        'olbfrom
        '
        Me.olbfrom.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.olbfrom.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.olbfrom.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.olbfrom.Location = New System.Drawing.Point(20, 26)
        Me.olbfrom.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.olbfrom.Name = "olbfrom"
        Me.olbfrom.Size = New System.Drawing.Size(143, 23)
        Me.olbfrom.TabIndex = 523
        Me.olbfrom.Tag = "2|"
        Me.olbfrom.Text = "From Colorway  :"
        '
        'wChangeColorway
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(727, 121)
        Me.ControlBox = False
        Me.Controls.Add(Me.olbfrom)
        Me.Controls.Add(Me.FTColorwayFrom)
        Me.Controls.Add(Me.ocmcancel)
        Me.Controls.Add(Me.ocmok)
        Me.Controls.Add(Me.FTColorway_lbl)
        Me.Controls.Add(Me.FTColorway)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "wChangeColorway"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Change Colorway"
        CType(Me.FTColorway.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTColorwayFrom.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents FTColorway_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTColorway As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents ocmok As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmcancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FTColorwayFrom As DevExpress.XtraEditors.TextEdit
    Friend WithEvents olbfrom As DevExpress.XtraEditors.LabelControl
End Class

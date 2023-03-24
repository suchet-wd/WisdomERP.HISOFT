<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wNewChangeColorwayDevelop
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
        Me.FTColorway_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.ocmchangecolorway = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmcancel = New DevExpress.XtraEditors.SimpleButton()
        Me.FTColorway = New DevExpress.XtraEditors.TextEdit()
        Me.FTFromColorway_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTFromColorway = New DevExpress.XtraEditors.TextEdit()
        CType(Me.FTColorway.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTFromColorway.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'FTColorway_lbl
        '
        Me.FTColorway_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTColorway_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTColorway_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTColorway_lbl.Location = New System.Drawing.Point(2, 50)
        Me.FTColorway_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTColorway_lbl.Name = "FTColorway_lbl"
        Me.FTColorway_lbl.Size = New System.Drawing.Size(163, 23)
        Me.FTColorway_lbl.TabIndex = 518
        Me.FTColorway_lbl.Tag = "2|"
        Me.FTColorway_lbl.Text = "To FTColorway  :"
        '
        'ocmchangecolorway
        '
        Me.ocmchangecolorway.Location = New System.Drawing.Point(168, 88)
        Me.ocmchangecolorway.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmchangecolorway.Name = "ocmchangecolorway"
        Me.ocmchangecolorway.Size = New System.Drawing.Size(127, 28)
        Me.ocmchangecolorway.TabIndex = 520
        Me.ocmchangecolorway.Text = "Change Color Way"
        '
        'ocmcancel
        '
        Me.ocmcancel.Location = New System.Drawing.Point(327, 88)
        Me.ocmcancel.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmcancel.Name = "ocmcancel"
        Me.ocmcancel.Size = New System.Drawing.Size(127, 28)
        Me.ocmcancel.TabIndex = 521
        Me.ocmcancel.Text = "Cancel"
        '
        'FTColorway
        '
        Me.FTColorway.Location = New System.Drawing.Point(171, 50)
        Me.FTColorway.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTColorway.Name = "FTColorway"
        Me.FTColorway.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.FTColorway.Properties.MaxLength = 30
        Me.FTColorway.Properties.Tag = ""
        Me.FTColorway.Size = New System.Drawing.Size(287, 22)
        Me.FTColorway.TabIndex = 517
        Me.FTColorway.Tag = "2|"
        '
        'FTFromColorway_lbl
        '
        Me.FTFromColorway_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTFromColorway_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTFromColorway_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTFromColorway_lbl.Location = New System.Drawing.Point(2, 13)
        Me.FTFromColorway_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTFromColorway_lbl.Name = "FTFromColorway_lbl"
        Me.FTFromColorway_lbl.Size = New System.Drawing.Size(163, 23)
        Me.FTFromColorway_lbl.TabIndex = 523
        Me.FTFromColorway_lbl.Tag = "2|"
        Me.FTFromColorway_lbl.Text = "From Colorway  :"
        '
        'FTFromColorway
        '
        Me.FTFromColorway.Location = New System.Drawing.Point(171, 13)
        Me.FTFromColorway.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTFromColorway.Name = "FTFromColorway"
        Me.FTFromColorway.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FTFromColorway.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FTFromColorway.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FTFromColorway.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FTFromColorway.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.FTFromColorway.Properties.MaxLength = 30
        Me.FTFromColorway.Properties.ReadOnly = True
        Me.FTFromColorway.Properties.Tag = ""
        Me.FTFromColorway.Size = New System.Drawing.Size(287, 22)
        Me.FTFromColorway.TabIndex = 522
        Me.FTFromColorway.Tag = "2|"
        '
        'wNewChangeColorwayDevelop
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(587, 145)
        Me.ControlBox = False
        Me.Controls.Add(Me.FTFromColorway_lbl)
        Me.Controls.Add(Me.FTFromColorway)
        Me.Controls.Add(Me.ocmcancel)
        Me.Controls.Add(Me.ocmchangecolorway)
        Me.Controls.Add(Me.FTColorway_lbl)
        Me.Controls.Add(Me.FTColorway)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "wNewChangeColorwayDevelop"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Change Colorway"
        CType(Me.FTColorway.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTFromColorway.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents FTColorway_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ocmchangecolorway As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmcancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FTColorway As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FTFromColorway_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTFromColorway As DevExpress.XtraEditors.TextEdit
End Class

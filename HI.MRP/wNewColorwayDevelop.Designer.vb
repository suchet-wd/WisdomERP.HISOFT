<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wNewColorwayDevelop
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
        Me.ocmnewcolorway = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmcancel = New DevExpress.XtraEditors.SimpleButton()
        Me.FTColorway = New DevExpress.XtraEditors.TextEdit()
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
        'FTColorway
        '
        Me.FTColorway.Location = New System.Drawing.Point(145, 12)
        Me.FTColorway.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTColorway.Name = "FTColorway"
        Me.FTColorway.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.FTColorway.Properties.MaxLength = 30
        Me.FTColorway.Properties.Tag = ""
        Me.FTColorway.Size = New System.Drawing.Size(313, 22)
        Me.FTColorway.TabIndex = 517
        Me.FTColorway.Tag = "2|"
        '
        'wNewColorwayDevelop
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(587, 91)
        Me.ControlBox = False
        Me.Controls.Add(Me.ocmcancel)
        Me.Controls.Add(Me.ocmnewcolorway)
        Me.Controls.Add(Me.FTColorway_lbl)
        Me.Controls.Add(Me.FTColorway)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "wNewColorwayDevelop"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "New Colorway"
        CType(Me.FTColorway.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents FTColorway_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ocmnewcolorway As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmcancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FTColorway As DevExpress.XtraEditors.TextEdit
End Class

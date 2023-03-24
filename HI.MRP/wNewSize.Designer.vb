<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wNewSize
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
        Dim EditorButtonImageOptions1 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject4 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.ocmnewsizebreakdown = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmcancel = New DevExpress.XtraEditors.SimpleButton()
        Me.FTSizeBreakDown_None = New DevExpress.XtraEditors.TextEdit()
        Me.FTSizeBreakDown = New DevExpress.XtraEditors.ButtonEdit()
        Me.FTSizeBreakDown_lbl = New DevExpress.XtraEditors.LabelControl()
        CType(Me.FTSizeBreakDown_None.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTSizeBreakDown.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ocmnewsizebreakdown
        '
        Me.ocmnewsizebreakdown.Location = New System.Drawing.Point(124, 41)
        Me.ocmnewsizebreakdown.Name = "ocmnewsizebreakdown"
        Me.ocmnewsizebreakdown.Size = New System.Drawing.Size(109, 23)
        Me.ocmnewsizebreakdown.TabIndex = 520
        Me.ocmnewsizebreakdown.Text = "New Size Breakdown"
        '
        'ocmcancel
        '
        Me.ocmcancel.Location = New System.Drawing.Point(284, 41)
        Me.ocmcancel.Name = "ocmcancel"
        Me.ocmcancel.Size = New System.Drawing.Size(109, 23)
        Me.ocmcancel.TabIndex = 521
        Me.ocmcancel.Text = "Cancel"
        '
        'FTSizeBreakDown_None
        '
        Me.FTSizeBreakDown_None.Location = New System.Drawing.Point(235, 12)
        Me.FTSizeBreakDown_None.Name = "FTSizeBreakDown_None"
        Me.FTSizeBreakDown_None.Properties.Appearance.BackColor = System.Drawing.Color.LightCyan
        Me.FTSizeBreakDown_None.Properties.Appearance.Options.UseBackColor = True
        Me.FTSizeBreakDown_None.Properties.ReadOnly = True
        Me.FTSizeBreakDown_None.Size = New System.Drawing.Size(225, 20)
        Me.FTSizeBreakDown_None.TabIndex = 524
        Me.FTSizeBreakDown_None.Tag = "2|"
        '
        'FTSizeBreakDown
        '
        Me.FTSizeBreakDown.Location = New System.Drawing.Point(124, 12)
        Me.FTSizeBreakDown.Name = "FTSizeBreakDown"
        Me.FTSizeBreakDown.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, EditorButtonImageOptions1, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, SerializableAppearanceObject2, SerializableAppearanceObject3, SerializableAppearanceObject4, "", "95", Nothing, DevExpress.Utils.ToolTipAnchor.[Default])})
        Me.FTSizeBreakDown.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.FTSizeBreakDown.Properties.Tag = ""
        Me.FTSizeBreakDown.Size = New System.Drawing.Size(109, 20)
        Me.FTSizeBreakDown.TabIndex = 523
        Me.FTSizeBreakDown.Tag = "2|"
        '
        'FTSizeBreakDown_lbl
        '
        Me.FTSizeBreakDown_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTSizeBreakDown_lbl.Appearance.Options.UseForeColor = True
        Me.FTSizeBreakDown_lbl.Appearance.Options.UseTextOptions = True
        Me.FTSizeBreakDown_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTSizeBreakDown_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTSizeBreakDown_lbl.Location = New System.Drawing.Point(12, 12)
        Me.FTSizeBreakDown_lbl.Name = "FTSizeBreakDown_lbl"
        Me.FTSizeBreakDown_lbl.Size = New System.Drawing.Size(110, 19)
        Me.FTSizeBreakDown_lbl.TabIndex = 522
        Me.FTSizeBreakDown_lbl.Tag = "2|"
        Me.FTSizeBreakDown_lbl.Text = "FTSizeBreakDown  :"
        '
        'wNewSize
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(503, 79)
        Me.ControlBox = False
        Me.Controls.Add(Me.FTSizeBreakDown_None)
        Me.Controls.Add(Me.FTSizeBreakDown)
        Me.Controls.Add(Me.FTSizeBreakDown_lbl)
        Me.Controls.Add(Me.ocmcancel)
        Me.Controls.Add(Me.ocmnewsizebreakdown)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "wNewSize"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "New Size Breakdown"
        CType(Me.FTSizeBreakDown_None.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTSizeBreakDown.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ocmnewsizebreakdown As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmcancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FTSizeBreakDown_None As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FTSizeBreakDown As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FTSizeBreakDown_lbl As DevExpress.XtraEditors.LabelControl
End Class

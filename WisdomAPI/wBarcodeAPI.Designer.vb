<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wBarcodeAPI
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
        Me.ocmsave = New DevExpress.XtraEditors.SimpleButton()
        Me.FTBarcodeNo_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTBarcodeNo = New DevExpress.XtraEditors.TextEdit()
        Me.SimpleButton1 = New DevExpress.XtraEditors.SimpleButton()
        Me.SimpleButton2 = New DevExpress.XtraEditors.SimpleButton()
        Me.SimpleButton3 = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.FTBarcodeNo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ocmsave
        '
        Me.ocmsave.Location = New System.Drawing.Point(390, 315)
        Me.ocmsave.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmsave.Name = "ocmsave"
        Me.ocmsave.Size = New System.Drawing.Size(111, 31)
        Me.ocmsave.TabIndex = 94
        Me.ocmsave.TabStop = False
        Me.ocmsave.Tag = "2|"
        Me.ocmsave.Text = "SAVE"
        '
        'FTBarcodeNo_lbl
        '
        Me.FTBarcodeNo_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTBarcodeNo_lbl.Appearance.Options.UseForeColor = True
        Me.FTBarcodeNo_lbl.Appearance.Options.UseTextOptions = True
        Me.FTBarcodeNo_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTBarcodeNo_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTBarcodeNo_lbl.Location = New System.Drawing.Point(266, 218)
        Me.FTBarcodeNo_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTBarcodeNo_lbl.Name = "FTBarcodeNo_lbl"
        Me.FTBarcodeNo_lbl.Size = New System.Drawing.Size(146, 23)
        Me.FTBarcodeNo_lbl.TabIndex = 289
        Me.FTBarcodeNo_lbl.Tag = "2|"
        Me.FTBarcodeNo_lbl.Text = "Barcode No :"
        '
        'FTBarcodeNo
        '
        Me.FTBarcodeNo.Location = New System.Drawing.Point(415, 218)
        Me.FTBarcodeNo.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTBarcodeNo.Name = "FTBarcodeNo"
        Me.FTBarcodeNo.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FTBarcodeNo.Properties.Appearance.Options.UseBackColor = True
        Me.FTBarcodeNo.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FTBarcodeNo.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FTBarcodeNo.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FTBarcodeNo.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FTBarcodeNo.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FTBarcodeNo.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FTBarcodeNo.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FTBarcodeNo.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FTBarcodeNo.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FTBarcodeNo.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FTBarcodeNo.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FTBarcodeNo.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FTBarcodeNo.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.FTBarcodeNo.Properties.MaxLength = 30
        Me.FTBarcodeNo.Size = New System.Drawing.Size(201, 22)
        Me.FTBarcodeNo.TabIndex = 288
        Me.FTBarcodeNo.Tag = "2|"
        '
        'SimpleButton1
        '
        Me.SimpleButton1.Location = New System.Drawing.Point(390, 363)
        Me.SimpleButton1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.SimpleButton1.Name = "SimpleButton1"
        Me.SimpleButton1.Size = New System.Drawing.Size(111, 31)
        Me.SimpleButton1.TabIndex = 290
        Me.SimpleButton1.TabStop = False
        Me.SimpleButton1.Tag = "2|"
        Me.SimpleButton1.Text = "POST"
        '
        'SimpleButton2
        '
        Me.SimpleButton2.Location = New System.Drawing.Point(155, 450)
        Me.SimpleButton2.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.SimpleButton2.Name = "SimpleButton2"
        Me.SimpleButton2.Size = New System.Drawing.Size(111, 31)
        Me.SimpleButton2.TabIndex = 291
        Me.SimpleButton2.TabStop = False
        Me.SimpleButton2.Tag = "2|"
        Me.SimpleButton2.Text = "POST"
        '
        'SimpleButton3
        '
        Me.SimpleButton3.Location = New System.Drawing.Point(527, 450)
        Me.SimpleButton3.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.SimpleButton3.Name = "SimpleButton3"
        Me.SimpleButton3.Size = New System.Drawing.Size(111, 31)
        Me.SimpleButton3.TabIndex = 292
        Me.SimpleButton3.TabStop = False
        Me.SimpleButton3.Tag = "2|"
        Me.SimpleButton3.Text = "POST"
        '
        'wBarcodeAPI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(890, 661)
        Me.Controls.Add(Me.SimpleButton3)
        Me.Controls.Add(Me.SimpleButton2)
        Me.Controls.Add(Me.SimpleButton1)
        Me.Controls.Add(Me.FTBarcodeNo_lbl)
        Me.Controls.Add(Me.FTBarcodeNo)
        Me.Controls.Add(Me.ocmsave)
        Me.Name = "wBarcodeAPI"
        Me.Text = "wBarcodeAPI"
        CType(Me.FTBarcodeNo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents ocmsave As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FTBarcodeNo_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTBarcodeNo As DevExpress.XtraEditors.TextEdit
    Friend WithEvents SimpleButton1 As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents SimpleButton2 As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents SimpleButton3 As DevExpress.XtraEditors.SimpleButton
End Class

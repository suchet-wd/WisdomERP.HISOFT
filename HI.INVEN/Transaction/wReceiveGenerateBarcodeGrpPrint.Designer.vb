<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wReceiveGenerateBarcodeGrpPrint
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
        Me.ogbnote = New DevExpress.XtraEditors.GroupControl()
        Me.FTBarcodeGrpNo = New DevExpress.XtraEditors.LabelControl()
        Me.ocmcancel = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmpreview = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.ogbnote, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbnote.SuspendLayout()
        Me.SuspendLayout()
        '
        'ogbnote
        '
        Me.ogbnote.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbnote.Controls.Add(Me.FTBarcodeGrpNo)
        Me.ogbnote.Controls.Add(Me.ocmcancel)
        Me.ogbnote.Controls.Add(Me.ocmpreview)
        Me.ogbnote.Location = New System.Drawing.Point(3, 2)
        Me.ogbnote.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbnote.Name = "ogbnote"
        Me.ogbnote.ShowCaption = False
        Me.ogbnote.Size = New System.Drawing.Size(475, 145)
        Me.ogbnote.TabIndex = 142
        Me.ogbnote.Text = "GroupControl1"
        '
        'FTBarcodeGrpNo
        '
        Me.FTBarcodeGrpNo.Appearance.Font = New System.Drawing.Font("Tahoma", 20.0!, System.Drawing.FontStyle.Bold)
        Me.FTBarcodeGrpNo.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTBarcodeGrpNo.Appearance.Options.UseFont = True
        Me.FTBarcodeGrpNo.Appearance.Options.UseForeColor = True
        Me.FTBarcodeGrpNo.Appearance.Options.UseTextOptions = True
        Me.FTBarcodeGrpNo.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTBarcodeGrpNo.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTBarcodeGrpNo.Location = New System.Drawing.Point(38, 9)
        Me.FTBarcodeGrpNo.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTBarcodeGrpNo.Name = "FTBarcodeGrpNo"
        Me.FTBarcodeGrpNo.Size = New System.Drawing.Size(404, 51)
        Me.FTBarcodeGrpNo.TabIndex = 289
        Me.FTBarcodeGrpNo.Tag = "2|"
        Me.FTBarcodeGrpNo.Text = "XXXXX"
        '
        'ocmcancel
        '
        Me.ocmcancel.Location = New System.Drawing.Point(255, 95)
        Me.ocmcancel.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmcancel.Name = "ocmcancel"
        Me.ocmcancel.Size = New System.Drawing.Size(187, 31)
        Me.ocmcancel.TabIndex = 145
        Me.ocmcancel.TabStop = False
        Me.ocmcancel.Tag = "2|"
        Me.ocmcancel.Text = "CLOSE"
        '
        'ocmpreview
        '
        Me.ocmpreview.Location = New System.Drawing.Point(38, 95)
        Me.ocmpreview.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmpreview.Name = "ocmpreview"
        Me.ocmpreview.Size = New System.Drawing.Size(187, 31)
        Me.ocmpreview.TabIndex = 144
        Me.ocmpreview.TabStop = False
        Me.ocmpreview.Tag = "2|"
        Me.ocmpreview.Text = "PRINT"
        '
        'wReceiveGenerateBarcodeGrpPrint
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(482, 152)
        Me.Controls.Add(Me.ogbnote)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "wReceiveGenerateBarcodeGrpPrint"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = " Barcode Group No"
        CType(Me.ogbnote, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbnote.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogbnote As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ocmpreview As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmcancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FTBarcodeGrpNo As DevExpress.XtraEditors.LabelControl
End Class

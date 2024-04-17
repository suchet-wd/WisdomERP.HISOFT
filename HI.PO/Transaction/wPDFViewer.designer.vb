<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wPDFViewer
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
        Me.components = New System.ComponentModel.Container()
        Me.FilePdf = New DevExpress.XtraPdfViewer.PdfViewer()
        Me.opnheader = New DevExpress.XtraEditors.PanelControl()
        Me.chkaddlicense = New DevExpress.XtraEditors.CheckEdit()
        Me.opnconfirm = New DevExpress.XtraEditors.PanelControl()
        Me.ocmconfirm = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmexporttopdf = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.opnheader, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.opnheader.SuspendLayout()
        CType(Me.chkaddlicense.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.opnconfirm, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.opnconfirm.SuspendLayout()
        Me.SuspendLayout()
        '
        'FilePdf
        '
        Me.FilePdf.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FilePdf.Location = New System.Drawing.Point(0, 31)
        Me.FilePdf.Name = "FilePdf"
        Me.FilePdf.Size = New System.Drawing.Size(854, 635)
        Me.FilePdf.TabIndex = 0
        '
        'opnheader
        '
        Me.opnheader.Controls.Add(Me.ocmexporttopdf)
        Me.opnheader.Controls.Add(Me.chkaddlicense)
        Me.opnheader.Controls.Add(Me.opnconfirm)
        Me.opnheader.Dock = System.Windows.Forms.DockStyle.Top
        Me.opnheader.Location = New System.Drawing.Point(0, 0)
        Me.opnheader.Name = "opnheader"
        Me.opnheader.Size = New System.Drawing.Size(854, 31)
        Me.opnheader.TabIndex = 1
        '
        'chkaddlicense
        '
        Me.chkaddlicense.Dock = System.Windows.Forms.DockStyle.Right
        Me.chkaddlicense.Location = New System.Drawing.Point(327, 2)
        Me.chkaddlicense.Name = "chkaddlicense"
        Me.chkaddlicense.Properties.Appearance.Options.UseTextOptions = True
        Me.chkaddlicense.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.chkaddlicense.Properties.Caption = "Add Signature Confirm PI"
        Me.chkaddlicense.Properties.GlyphAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.chkaddlicense.Size = New System.Drawing.Size(307, 27)
        Me.chkaddlicense.TabIndex = 1
        '
        'opnconfirm
        '
        Me.opnconfirm.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.opnconfirm.Controls.Add(Me.ocmconfirm)
        Me.opnconfirm.Dock = System.Windows.Forms.DockStyle.Right
        Me.opnconfirm.Location = New System.Drawing.Point(634, 2)
        Me.opnconfirm.Name = "opnconfirm"
        Me.opnconfirm.Size = New System.Drawing.Size(218, 27)
        Me.opnconfirm.TabIndex = 0
        '
        'ocmconfirm
        '
        Me.ocmconfirm.Enabled = False
        Me.ocmconfirm.Location = New System.Drawing.Point(55, 0)
        Me.ocmconfirm.Name = "ocmconfirm"
        Me.ocmconfirm.Size = New System.Drawing.Size(132, 27)
        Me.ocmconfirm.TabIndex = 14
        Me.ocmconfirm.Text = "Confirm"
        Me.ocmconfirm.Visible = False
        '
        'ocmexporttopdf
        '
        Me.ocmexporttopdf.Location = New System.Drawing.Point(5, 2)
        Me.ocmexporttopdf.Name = "ocmexporttopdf"
        Me.ocmexporttopdf.Size = New System.Drawing.Size(126, 27)
        Me.ocmexporttopdf.TabIndex = 15
        Me.ocmexporttopdf.Text = "Export PDF"
        '
        'wPDFViewer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(854, 666)
        Me.Controls.Add(Me.FilePdf)
        Me.Controls.Add(Me.opnheader)
        Me.Name = "wPDFViewer"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "PDF File"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.opnheader, System.ComponentModel.ISupportInitialize).EndInit()
        Me.opnheader.ResumeLayout(False)
        CType(Me.chkaddlicense.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.opnconfirm, System.ComponentModel.ISupportInitialize).EndInit()
        Me.opnconfirm.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents FilePdf As DevExpress.XtraPdfViewer.PdfViewer
    Friend WithEvents opnheader As DevExpress.XtraEditors.PanelControl
    Friend WithEvents chkaddlicense As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents opnconfirm As DevExpress.XtraEditors.PanelControl
    Friend WithEvents ocmconfirm As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmexporttopdf As DevExpress.XtraEditors.SimpleButton
End Class

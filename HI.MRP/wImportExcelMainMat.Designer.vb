<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wImportExcelMainMat
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
        Me.ogbselectfile = New DevExpress.XtraEditors.GroupControl()
        Me.FTFilePath = New DevExpress.XtraEditors.ButtonEdit()
        Me.otb = New DevExpress.XtraTab.XtraTabControl()
        Me.otpnormal = New DevExpress.XtraTab.XtraTabPage()
        Me.opshet = New DevExpress.XtraSpreadsheet.SpreadsheetControl()
        Me.ogbmainprocbutton = New DevExpress.XtraEditors.PanelControl()
        Me.ocmimportbimpdf = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmclear = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.ogbselectfile, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbselectfile.SuspendLayout()
        CType(Me.FTFilePath.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.otb, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.otb.SuspendLayout()
        Me.otpnormal.SuspendLayout()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbmainprocbutton.SuspendLayout()
        Me.SuspendLayout()
        '
        'ogbselectfile
        '
        Me.ogbselectfile.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbselectfile.Controls.Add(Me.FTFilePath)
        Me.ogbselectfile.Location = New System.Drawing.Point(3, 3)
        Me.ogbselectfile.Name = "ogbselectfile"
        Me.ogbselectfile.Size = New System.Drawing.Size(1130, 58)
        Me.ogbselectfile.TabIndex = 2
        Me.ogbselectfile.Text = "Select File"
        '
        'FTFilePath
        '
        Me.FTFilePath.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FTFilePath.Location = New System.Drawing.Point(37, 26)
        Me.FTFilePath.Name = "FTFilePath"
        Me.FTFilePath.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FTFilePath.Properties.Appearance.Options.UseBackColor = True
        Me.FTFilePath.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FTFilePath.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FTFilePath.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FTFilePath.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FTFilePath.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FTFilePath.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FTFilePath.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FTFilePath.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FTFilePath.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FTFilePath.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FTFilePath.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FTFilePath.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FTFilePath.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.FTFilePath.Properties.ReadOnly = True
        Me.FTFilePath.Size = New System.Drawing.Size(1079, 20)
        Me.FTFilePath.TabIndex = 1
        Me.FTFilePath.Tag = "2|"
        '
        'otb
        '
        Me.otb.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.otb.Location = New System.Drawing.Point(3, 66)
        Me.otb.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.otb.Name = "otb"
        Me.otb.SelectedTabPage = Me.otpnormal
        Me.otb.Size = New System.Drawing.Size(1130, 503)
        Me.otb.TabIndex = 392
        Me.otb.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.otpnormal})
        '
        'otpnormal
        '
        Me.otpnormal.Controls.Add(Me.opshet)
        Me.otpnormal.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.otpnormal.Name = "otpnormal"
        Me.otpnormal.Size = New System.Drawing.Size(1128, 478)
        Me.otpnormal.Text = "Excel Detail"
        '
        'opshet
        '
        Me.opshet.Dock = System.Windows.Forms.DockStyle.Fill
        Me.opshet.Location = New System.Drawing.Point(0, 0)
        Me.opshet.Name = "opshet"
        Me.opshet.Options.Behavior.Column.Resize = DevExpress.XtraSpreadsheet.DocumentCapability.Enabled
        Me.opshet.ReadOnly = True
        Me.opshet.Size = New System.Drawing.Size(1128, 478)
        Me.opshet.TabIndex = 2
        '
        'ogbmainprocbutton
        '
        Me.ogbmainprocbutton.Controls.Add(Me.ocmimportbimpdf)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmexit)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmclear)
        Me.ogbmainprocbutton.Location = New System.Drawing.Point(77, 268)
        Me.ogbmainprocbutton.Name = "ogbmainprocbutton"
        Me.ogbmainprocbutton.Size = New System.Drawing.Size(736, 114)
        Me.ogbmainprocbutton.TabIndex = 393
        '
        'ocmimportbimpdf
        '
        Me.ocmimportbimpdf.Location = New System.Drawing.Point(213, 45)
        Me.ocmimportbimpdf.Name = "ocmimportbimpdf"
        Me.ocmimportbimpdf.Size = New System.Drawing.Size(203, 25)
        Me.ocmimportbimpdf.TabIndex = 336
        Me.ocmimportbimpdf.TabStop = False
        Me.ocmimportbimpdf.Tag = "2|"
        Me.ocmimportbimpdf.Text = "Importoptiplan"
        '
        'ocmexit
        '
        Me.ocmexit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmexit.Location = New System.Drawing.Point(673, 3)
        Me.ocmexit.Name = "ocmexit"
        Me.ocmexit.Size = New System.Drawing.Size(55, 25)
        Me.ocmexit.TabIndex = 96
        Me.ocmexit.TabStop = False
        Me.ocmexit.Tag = "2|"
        Me.ocmexit.Text = "EXIT"
        '
        'ocmclear
        '
        Me.ocmclear.Location = New System.Drawing.Point(86, 5)
        Me.ocmclear.Name = "ocmclear"
        Me.ocmclear.Size = New System.Drawing.Size(72, 25)
        Me.ocmclear.TabIndex = 95
        Me.ocmclear.TabStop = False
        Me.ocmclear.Tag = "2|"
        Me.ocmclear.Text = "CLEAR"
        '
        'wImportExcelBOM
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1133, 571)
        Me.Controls.Add(Me.ogbmainprocbutton)
        Me.Controls.Add(Me.otb)
        Me.Controls.Add(Me.ogbselectfile)
        Me.Name = "wImportExcelMainMat"
        Me.Text = "Import Excel Main Material"
        CType(Me.ogbselectfile, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbselectfile.ResumeLayout(False)
        CType(Me.FTFilePath.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.otb, System.ComponentModel.ISupportInitialize).EndInit()
        Me.otb.ResumeLayout(False)
        Me.otpnormal.ResumeLayout(False)
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbmainprocbutton.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogbselectfile As DevExpress.XtraEditors.GroupControl
    Friend WithEvents FTFilePath As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents otb As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents otpnormal As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents ogbmainprocbutton As DevExpress.XtraEditors.PanelControl
    Friend WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmclear As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmimportbimpdf As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents opshet As DevExpress.XtraSpreadsheet.SpreadsheetControl
End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wBomDevAddFile
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
        Me.ocmcancel = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmok = New DevExpress.XtraEditors.SimpleButton()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.FTFileName = New DevExpress.XtraEditors.TextEdit()
        Me.FTFileName_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.ocmReadDocumentfile = New DevExpress.XtraEditors.SimpleButton()
        Me.FNFileType = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.FNFileType_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.oGrpdetail = New DevExpress.XtraEditors.GroupControl()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.FTFileName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNFileType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.oGrpdetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ocmcancel
        '
        Me.ocmcancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmcancel.Location = New System.Drawing.Point(973, 5)
        Me.ocmcancel.Name = "ocmcancel"
        Me.ocmcancel.Size = New System.Drawing.Size(145, 51)
        Me.ocmcancel.TabIndex = 1
        Me.ocmcancel.TabStop = False
        Me.ocmcancel.Tag = "2|"
        Me.ocmcancel.Text = "Cancel"
        '
        'ocmok
        '
        Me.ocmok.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmok.Location = New System.Drawing.Point(836, 5)
        Me.ocmok.Name = "ocmok"
        Me.ocmok.Size = New System.Drawing.Size(133, 51)
        Me.ocmok.TabIndex = 0
        Me.ocmok.TabStop = False
        Me.ocmok.Tag = "2|"
        Me.ocmok.Text = "OK"
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Me.FTFileName)
        Me.GroupControl1.Controls.Add(Me.FTFileName_lbl)
        Me.GroupControl1.Controls.Add(Me.ocmcancel)
        Me.GroupControl1.Controls.Add(Me.ocmReadDocumentfile)
        Me.GroupControl1.Controls.Add(Me.ocmok)
        Me.GroupControl1.Controls.Add(Me.FNFileType)
        Me.GroupControl1.Controls.Add(Me.FNFileType_lbl)
        Me.GroupControl1.Controls.Add(Me.oGrpdetail)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl1.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.ShowCaption = False
        Me.GroupControl1.Size = New System.Drawing.Size(1123, 565)
        Me.GroupControl1.TabIndex = 290
        Me.GroupControl1.Text = "GroupControl2"
        '
        'FTFileName
        '
        Me.FTFileName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FTFileName.Location = New System.Drawing.Point(167, 37)
        Me.FTFileName.Name = "FTFileName"
        Me.FTFileName.Properties.MaxLength = 200
        Me.FTFileName.Size = New System.Drawing.Size(492, 20)
        Me.FTFileName.TabIndex = 537
        Me.FTFileName.Tag = "2|"
        '
        'FTFileName_lbl
        '
        Me.FTFileName_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTFileName_lbl.Appearance.Options.UseForeColor = True
        Me.FTFileName_lbl.Appearance.Options.UseTextOptions = True
        Me.FTFileName_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTFileName_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTFileName_lbl.Location = New System.Drawing.Point(32, 37)
        Me.FTFileName_lbl.Name = "FTFileName_lbl"
        Me.FTFileName_lbl.Size = New System.Drawing.Size(126, 19)
        Me.FTFileName_lbl.TabIndex = 536
        Me.FTFileName_lbl.Tag = "2|"
        Me.FTFileName_lbl.Text = "File Name :"
        '
        'ocmReadDocumentfile
        '
        Me.ocmReadDocumentfile.Location = New System.Drawing.Point(307, 12)
        Me.ocmReadDocumentfile.Name = "ocmReadDocumentfile"
        Me.ocmReadDocumentfile.Size = New System.Drawing.Size(143, 21)
        Me.ocmReadDocumentfile.TabIndex = 535
        Me.ocmReadDocumentfile.TabStop = False
        Me.ocmReadDocumentfile.Tag = "2|"
        Me.ocmReadDocumentfile.Text = "Read File"
        '
        'FNFileType
        '
        Me.FNFileType.Location = New System.Drawing.Point(167, 13)
        Me.FNFileType.Name = "FNFileType"
        Me.FNFileType.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FNFileType.Properties.Tag = "FTFileType"
        Me.FNFileType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.FNFileType.Size = New System.Drawing.Size(134, 20)
        Me.FNFileType.TabIndex = 533
        Me.FNFileType.Tag = "2|"
        '
        'FNFileType_lbl
        '
        Me.FNFileType_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNFileType_lbl.Appearance.Options.UseForeColor = True
        Me.FNFileType_lbl.Appearance.Options.UseTextOptions = True
        Me.FNFileType_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNFileType_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNFileType_lbl.Location = New System.Drawing.Point(32, 14)
        Me.FNFileType_lbl.Name = "FNFileType_lbl"
        Me.FNFileType_lbl.Size = New System.Drawing.Size(126, 19)
        Me.FNFileType_lbl.TabIndex = 534
        Me.FNFileType_lbl.Tag = "2|"
        Me.FNFileType_lbl.Text = "FTFileType :"
        '
        'oGrpdetail
        '
        Me.oGrpdetail.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.oGrpdetail.Location = New System.Drawing.Point(2, 62)
        Me.oGrpdetail.Name = "oGrpdetail"
        Me.oGrpdetail.Size = New System.Drawing.Size(1119, 501)
        Me.oGrpdetail.TabIndex = 291
        Me.oGrpdetail.Text = "File"
        '
        'wSMPOrderAddFile
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1123, 565)
        Me.ControlBox = False
        Me.Controls.Add(Me.GroupControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "wSMPOrderAddFile"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Add File"
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        CType(Me.FTFileName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNFileType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.oGrpdetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ocmcancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmok As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents oGrpdetail As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ocmReadDocumentfile As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FNFileType As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents FNFileType_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTFileName_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTFileName As DevExpress.XtraEditors.TextEdit
End Class

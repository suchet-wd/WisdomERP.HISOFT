<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wHRReportVisitor
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
        Me.ogbvisitor = New DevExpress.XtraEditors.GroupControl()
        Me.EndSeq = New DevExpress.XtraEditors.TextEdit()
        Me.StartSeq = New DevExpress.XtraEditors.TextEdit()
        Me.SeqEnd_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.SeqStart_lb = New DevExpress.XtraEditors.LabelControl()
        Me.ogbbutton = New DevExpress.XtraEditors.GroupControl()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmpreview = New DevExpress.XtraEditors.SimpleButton()
        Me.ogbreportname = New DevExpress.XtraEditors.GroupControl()
        Me.FNEmpSex_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNReportname = New DevExpress.XtraEditors.ComboBoxEdit()
        CType(Me.ogbvisitor, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbvisitor.SuspendLayout()
        CType(Me.EndSeq.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StartSeq.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogbbutton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbbutton.SuspendLayout()
        CType(Me.ogbreportname, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbreportname.SuspendLayout()
        CType(Me.FNReportname.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ogbvisitor
        '
        Me.ogbvisitor.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbvisitor.Controls.Add(Me.EndSeq)
        Me.ogbvisitor.Controls.Add(Me.StartSeq)
        Me.ogbvisitor.Controls.Add(Me.SeqEnd_lbl)
        Me.ogbvisitor.Controls.Add(Me.SeqStart_lb)
        Me.ogbvisitor.Location = New System.Drawing.Point(12, 12)
        Me.ogbvisitor.Name = "ogbvisitor"
        Me.ogbvisitor.Size = New System.Drawing.Size(637, 50)
        Me.ogbvisitor.TabIndex = 4
        Me.ogbvisitor.Text = "Visitor"
        '
        'EndSeq
        '
        Me.EndSeq.Location = New System.Drawing.Point(373, 24)
        Me.EndSeq.Name = "EndSeq"
        Me.EndSeq.Size = New System.Drawing.Size(100, 20)
        Me.EndSeq.TabIndex = 294
        '
        'StartSeq
        '
        Me.StartSeq.Location = New System.Drawing.Point(206, 24)
        Me.StartSeq.Name = "StartSeq"
        Me.StartSeq.Size = New System.Drawing.Size(100, 20)
        Me.StartSeq.TabIndex = 293
        '
        'SeqEnd_lbl
        '
        Me.SeqEnd_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.SeqEnd_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.SeqEnd_lbl.Location = New System.Drawing.Point(314, 24)
        Me.SeqEnd_lbl.Name = "SeqEnd_lbl"
        Me.SeqEnd_lbl.Size = New System.Drawing.Size(53, 20)
        Me.SeqEnd_lbl.TabIndex = 292
        Me.SeqEnd_lbl.Tag = "2|"
        Me.SeqEnd_lbl.Text = "ถึงลำดับที่ :"
        '
        'SeqStart_lb
        '
        Me.SeqStart_lb.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.SeqStart_lb.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.SeqStart_lb.Location = New System.Drawing.Point(118, 25)
        Me.SeqStart_lb.Name = "SeqStart_lb"
        Me.SeqStart_lb.Size = New System.Drawing.Size(81, 20)
        Me.SeqStart_lb.TabIndex = 290
        Me.SeqStart_lb.Tag = "2|"
        Me.SeqStart_lb.Text = "จากลำดับที่ :"
        '
        'ogbbutton
        '
        Me.ogbbutton.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbbutton.Controls.Add(Me.ocmexit)
        Me.ogbbutton.Controls.Add(Me.ocmpreview)
        Me.ogbbutton.Location = New System.Drawing.Point(12, 108)
        Me.ogbbutton.Name = "ogbbutton"
        Me.ogbbutton.ShowCaption = False
        Me.ogbbutton.Size = New System.Drawing.Size(637, 42)
        Me.ogbbutton.TabIndex = 5
        Me.ogbbutton.Text = "GroupControl1"
        '
        'ocmexit
        '
        Me.ocmexit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmexit.Location = New System.Drawing.Point(418, 9)
        Me.ocmexit.Name = "ocmexit"
        Me.ocmexit.Size = New System.Drawing.Size(146, 25)
        Me.ocmexit.TabIndex = 1
        Me.ocmexit.Text = "EXIT"
        '
        'ocmpreview
        '
        Me.ocmpreview.Location = New System.Drawing.Point(63, 9)
        Me.ocmpreview.Name = "ocmpreview"
        Me.ocmpreview.Size = New System.Drawing.Size(146, 25)
        Me.ocmpreview.TabIndex = 0
        Me.ocmpreview.Text = "PREVIEW"
        '
        'ogbreportname
        '
        Me.ogbreportname.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbreportname.Controls.Add(Me.FNEmpSex_lbl)
        Me.ogbreportname.Controls.Add(Me.FNReportname)
        Me.ogbreportname.Location = New System.Drawing.Point(12, 63)
        Me.ogbreportname.Name = "ogbreportname"
        Me.ogbreportname.ShowCaption = False
        Me.ogbreportname.Size = New System.Drawing.Size(637, 42)
        Me.ogbreportname.TabIndex = 6
        Me.ogbreportname.Text = "GroupControl1"
        Me.ogbreportname.Visible = False
        '
        'FNEmpSex_lbl
        '
        Me.FNEmpSex_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNEmpSex_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNEmpSex_lbl.Location = New System.Drawing.Point(23, 11)
        Me.FNEmpSex_lbl.Name = "FNEmpSex_lbl"
        Me.FNEmpSex_lbl.Size = New System.Drawing.Size(100, 19)
        Me.FNEmpSex_lbl.TabIndex = 293
        Me.FNEmpSex_lbl.Tag = "2|"
        Me.FNEmpSex_lbl.Text = "Report :"
        '
        'FNReportname
        '
        Me.FNReportname.EditValue = ""
        Me.FNReportname.EnterMoveNextControl = True
        Me.FNReportname.Location = New System.Drawing.Point(159, 11)
        Me.FNReportname.Name = "FNReportname"
        Me.FNReportname.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FNReportname.Properties.Appearance.Options.UseBackColor = True
        Me.FNReportname.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FNReportname.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FNReportname.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FNReportname.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FNReportname.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNReportname.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNReportname.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNReportname.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNReportname.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNReportname.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNReportname.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNReportname.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNReportname.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FNReportname.Properties.Tag = ""
        Me.FNReportname.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.FNReportname.Size = New System.Drawing.Size(312, 20)
        Me.FNReportname.TabIndex = 292
        Me.FNReportname.Tag = "2|"
        '
        'wHRReportVisitor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(661, 151)
        Me.Controls.Add(Me.ogbreportname)
        Me.Controls.Add(Me.ogbbutton)
        Me.Controls.Add(Me.ogbvisitor)
        Me.Name = "wHRReportVisitor"
        Me.Text = "Report"
        CType(Me.ogbvisitor, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbvisitor.ResumeLayout(False)
        CType(Me.EndSeq.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StartSeq.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogbbutton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbbutton.ResumeLayout(False)
        CType(Me.ogbreportname, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbreportname.ResumeLayout(False)
        CType(Me.FNReportname.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogbvisitor As DevExpress.XtraEditors.GroupControl
    Friend WithEvents SeqEnd_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents SeqStart_lb As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ogbbutton As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmpreview As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents EndSeq As DevExpress.XtraEditors.TextEdit
    Friend WithEvents StartSeq As DevExpress.XtraEditors.TextEdit
    Friend WithEvents ogbreportname As DevExpress.XtraEditors.GroupControl
    Friend WithEvents FNEmpSex_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNReportname As DevExpress.XtraEditors.ComboBoxEdit
End Class

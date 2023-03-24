<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wHRReportStatisticExcel
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
        Me.ogbbutton = New DevExpress.XtraEditors.GroupControl()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmpreview = New DevExpress.XtraEditors.SimpleButton()
        Me.ogbreportname = New DevExpress.XtraEditors.GroupControl()
        Me.FNEmpSex_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNReportname = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.ogbgrpcondition = New DevExpress.XtraEditors.GroupControl()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.EFTDateTrans = New DevExpress.XtraEditors.DateEdit()
        Me.EFTDateTrans_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.SFTDateTrans = New DevExpress.XtraEditors.DateEdit()
        Me.SFTDateTrans_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.Condition = New HI.HR.HRCondition()
        CType(Me.ogbbutton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbbutton.SuspendLayout()
        CType(Me.ogbreportname, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbreportname.SuspendLayout()
        CType(Me.FNReportname.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogbgrpcondition, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbgrpcondition.SuspendLayout()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.EFTDateTrans.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EFTDateTrans.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SFTDateTrans.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SFTDateTrans.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ogbbutton
        '
        Me.ogbbutton.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbbutton.Controls.Add(Me.ocmexit)
        Me.ogbbutton.Controls.Add(Me.ocmpreview)
        Me.ogbbutton.Location = New System.Drawing.Point(1, 364)
        Me.ogbbutton.Name = "ogbbutton"
        Me.ogbbutton.ShowCaption = False
        Me.ogbbutton.Size = New System.Drawing.Size(620, 42)
        Me.ogbbutton.TabIndex = 0
        Me.ogbbutton.Text = "GroupControl1"
        '
        'ocmexit
        '
        Me.ocmexit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmexit.Location = New System.Drawing.Point(401, 9)
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
        Me.ocmpreview.Text = "Export Excel"
        '
        'ogbreportname
        '
        Me.ogbreportname.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbreportname.Controls.Add(Me.FNEmpSex_lbl)
        Me.ogbreportname.Controls.Add(Me.FNReportname)
        Me.ogbreportname.Location = New System.Drawing.Point(2, 319)
        Me.ogbreportname.Name = "ogbreportname"
        Me.ogbreportname.ShowCaption = False
        Me.ogbreportname.Size = New System.Drawing.Size(620, 42)
        Me.ogbreportname.TabIndex = 1
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
        'ogbgrpcondition
        '
        Me.ogbgrpcondition.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbgrpcondition.Controls.Add(Me.Condition)
        Me.ogbgrpcondition.Location = New System.Drawing.Point(2, 53)
        Me.ogbgrpcondition.Name = "ogbgrpcondition"
        Me.ogbgrpcondition.Size = New System.Drawing.Size(620, 262)
        Me.ogbgrpcondition.TabIndex = 2
        Me.ogbgrpcondition.Text = "Condition"
        '
        'GroupControl1
        '
        Me.GroupControl1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupControl1.Controls.Add(Me.EFTDateTrans)
        Me.GroupControl1.Controls.Add(Me.EFTDateTrans_lbl)
        Me.GroupControl1.Controls.Add(Me.SFTDateTrans)
        Me.GroupControl1.Controls.Add(Me.SFTDateTrans_lbl)
        Me.GroupControl1.Location = New System.Drawing.Point(2, 3)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(620, 48)
        Me.GroupControl1.TabIndex = 4
        Me.GroupControl1.Text = "Date"
        '
        'EFTDateTrans
        '
        Me.EFTDateTrans.EditValue = Nothing
        Me.EFTDateTrans.EnterMoveNextControl = True
        Me.EFTDateTrans.Location = New System.Drawing.Point(404, 24)
        Me.EFTDateTrans.Name = "EFTDateTrans"
        Me.EFTDateTrans.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.EFTDateTrans.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.EFTDateTrans.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.EFTDateTrans.Properties.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.EFTDateTrans.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.EFTDateTrans.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.EFTDateTrans.Properties.NullDate = ""
        Me.EFTDateTrans.Size = New System.Drawing.Size(120, 20)
        Me.EFTDateTrans.TabIndex = 279
        Me.EFTDateTrans.Tag = "2|"
        '
        'EFTDateTrans_lbl
        '
        Me.EFTDateTrans_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.EFTDateTrans_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.EFTDateTrans_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.EFTDateTrans_lbl.Location = New System.Drawing.Point(291, 24)
        Me.EFTDateTrans_lbl.Name = "EFTDateTrans_lbl"
        Me.EFTDateTrans_lbl.Size = New System.Drawing.Size(110, 20)
        Me.EFTDateTrans_lbl.TabIndex = 280
        Me.EFTDateTrans_lbl.Tag = "2|"
        Me.EFTDateTrans_lbl.Text = "ถึงวันที่ :"
        '
        'SFTDateTrans
        '
        Me.SFTDateTrans.EditValue = Nothing
        Me.SFTDateTrans.EnterMoveNextControl = True
        Me.SFTDateTrans.Location = New System.Drawing.Point(140, 24)
        Me.SFTDateTrans.Name = "SFTDateTrans"
        Me.SFTDateTrans.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.SFTDateTrans.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.SFTDateTrans.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.SFTDateTrans.Properties.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.SFTDateTrans.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.SFTDateTrans.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.SFTDateTrans.Properties.NullDate = ""
        Me.SFTDateTrans.Size = New System.Drawing.Size(120, 20)
        Me.SFTDateTrans.TabIndex = 277
        Me.SFTDateTrans.Tag = "2|"
        '
        'SFTDateTrans_lbl
        '
        Me.SFTDateTrans_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.SFTDateTrans_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.SFTDateTrans_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.SFTDateTrans_lbl.Location = New System.Drawing.Point(27, 24)
        Me.SFTDateTrans_lbl.Name = "SFTDateTrans_lbl"
        Me.SFTDateTrans_lbl.Size = New System.Drawing.Size(110, 20)
        Me.SFTDateTrans_lbl.TabIndex = 278
        Me.SFTDateTrans_lbl.Tag = "2|"
        Me.SFTDateTrans_lbl.Text = "วันที่ :"
        '
        'Condition
        '
        Me.Condition.CommandTextDepartment = Nothing
        Me.Condition.Location = New System.Drawing.Point(28, 21)
        Me.Condition.Name = "Condition"
        Me.Condition.SelectedPageIndex = 0
        Me.Condition.ShowmDepartment = True
        Me.Condition.ShowmDivision = True
        Me.Condition.ShowmEmployee = False
        Me.Condition.ShowmEmployeeType = True
        Me.Condition.ShowmSect = True
        Me.Condition.ShowmUnitSect = True
        Me.Condition.Size = New System.Drawing.Size(572, 239)
        Me.Condition.TabIndex = 0
        '
        'wHRReportStatisticExcel
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(624, 408)
        Me.Controls.Add(Me.GroupControl1)
        Me.Controls.Add(Me.ogbgrpcondition)
        Me.Controls.Add(Me.ogbreportname)
        Me.Controls.Add(Me.ogbbutton)
        Me.Name = "wHRReportStatisticExcel"
        Me.Text = "Report"
        CType(Me.ogbbutton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbbutton.ResumeLayout(False)
        CType(Me.ogbreportname, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbreportname.ResumeLayout(False)
        CType(Me.FNReportname.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogbgrpcondition, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbgrpcondition.ResumeLayout(False)
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        CType(Me.EFTDateTrans.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EFTDateTrans.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SFTDateTrans.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SFTDateTrans.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogbbutton As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmpreview As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ogbreportname As DevExpress.XtraEditors.GroupControl
    Friend WithEvents FNEmpSex_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNReportname As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents ogbgrpcondition As DevExpress.XtraEditors.GroupControl
    Friend WithEvents Condition As HI.HR.HRCondition
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents EFTDateTrans As DevExpress.XtraEditors.DateEdit
    Friend WithEvents EFTDateTrans_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents SFTDateTrans As DevExpress.XtraEditors.DateEdit
    Friend WithEvents SFTDateTrans_lbl As DevExpress.XtraEditors.LabelControl
End Class

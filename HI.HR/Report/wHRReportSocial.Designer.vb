<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wHRReportSocial
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
        Me.Condition = New HI.HR.HRCondition()
        Me.ogbdate = New DevExpress.XtraEditors.GroupControl()
        Me.FTSendSSODate = New DevExpress.XtraEditors.DateEdit()
        Me.FTSendSSODate_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNMonth_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNMonth = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.FTPayYear = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.FTPayYear_lbl = New DevExpress.XtraEditors.LabelControl()
        CType(Me.ogbbutton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbbutton.SuspendLayout()
        CType(Me.ogbreportname, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbreportname.SuspendLayout()
        CType(Me.FNReportname.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogbgrpcondition, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbgrpcondition.SuspendLayout()
        CType(Me.ogbdate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbdate.SuspendLayout()
        CType(Me.FTSendSSODate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTSendSSODate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNMonth.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTPayYear.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ogbbutton
        '
        Me.ogbbutton.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbbutton.Controls.Add(Me.ocmexit)
        Me.ogbbutton.Controls.Add(Me.ocmpreview)
        Me.ogbbutton.Location = New System.Drawing.Point(2, 487)
        Me.ogbbutton.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbbutton.Name = "ogbbutton"
        Me.ogbbutton.ShowCaption = False
        Me.ogbbutton.Size = New System.Drawing.Size(723, 52)
        Me.ogbbutton.TabIndex = 0
        Me.ogbbutton.Text = "GroupControl1"
        '
        'ocmexit
        '
        Me.ocmexit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmexit.Location = New System.Drawing.Point(468, 11)
        Me.ocmexit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmexit.Name = "ocmexit"
        Me.ocmexit.Size = New System.Drawing.Size(170, 31)
        Me.ocmexit.TabIndex = 1
        Me.ocmexit.Text = "EXIT"
        '
        'ocmpreview
        '
        Me.ocmpreview.Location = New System.Drawing.Point(74, 11)
        Me.ocmpreview.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmpreview.Name = "ocmpreview"
        Me.ocmpreview.Size = New System.Drawing.Size(170, 31)
        Me.ocmpreview.TabIndex = 0
        Me.ocmpreview.Text = "PREVIEW"
        '
        'ogbreportname
        '
        Me.ogbreportname.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbreportname.Controls.Add(Me.FNEmpSex_lbl)
        Me.ogbreportname.Controls.Add(Me.FNReportname)
        Me.ogbreportname.Location = New System.Drawing.Point(1, 431)
        Me.ogbreportname.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbreportname.Name = "ogbreportname"
        Me.ogbreportname.ShowCaption = False
        Me.ogbreportname.Size = New System.Drawing.Size(723, 52)
        Me.ogbreportname.TabIndex = 1
        Me.ogbreportname.Text = "GroupControl1"
        '
        'FNEmpSex_lbl
        '
        Me.FNEmpSex_lbl.Appearance.Options.UseTextOptions = True
        Me.FNEmpSex_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNEmpSex_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNEmpSex_lbl.Location = New System.Drawing.Point(27, 14)
        Me.FNEmpSex_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNEmpSex_lbl.Name = "FNEmpSex_lbl"
        Me.FNEmpSex_lbl.Size = New System.Drawing.Size(117, 23)
        Me.FNEmpSex_lbl.TabIndex = 293
        Me.FNEmpSex_lbl.Tag = "2|"
        Me.FNEmpSex_lbl.Text = "Report :"
        '
        'FNReportname
        '
        Me.FNReportname.EditValue = ""
        Me.FNReportname.EnterMoveNextControl = True
        Me.FNReportname.Location = New System.Drawing.Point(185, 14)
        Me.FNReportname.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
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
        Me.FNReportname.Size = New System.Drawing.Size(364, 22)
        Me.FNReportname.TabIndex = 292
        Me.FNReportname.Tag = "2|"
        '
        'ogbgrpcondition
        '
        Me.ogbgrpcondition.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbgrpcondition.Controls.Add(Me.Condition)
        Me.ogbgrpcondition.Location = New System.Drawing.Point(2, 103)
        Me.ogbgrpcondition.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbgrpcondition.Name = "ogbgrpcondition"
        Me.ogbgrpcondition.Size = New System.Drawing.Size(723, 325)
        Me.ogbgrpcondition.TabIndex = 2
        Me.ogbgrpcondition.Text = "Condition"
        '
        'Condition
        '
        Me.Condition.CommandTextDepartment = Nothing
        Me.Condition.Location = New System.Drawing.Point(33, 26)
        Me.Condition.Margin = New System.Windows.Forms.Padding(5)
        Me.Condition.Name = "Condition"
        Me.Condition.SelectedPageIndex = 1
        Me.Condition.ShowmDepartment = True
        Me.Condition.ShowmDivision = True
        Me.Condition.ShowmEmployee = True
        Me.Condition.ShowmEmployeeType = False
        Me.Condition.ShowmSect = True
        Me.Condition.ShowmUnitSect = True
        Me.Condition.Size = New System.Drawing.Size(667, 294)
        Me.Condition.TabIndex = 0
        '
        'ogbdate
        '
        Me.ogbdate.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbdate.Controls.Add(Me.FTSendSSODate)
        Me.ogbdate.Controls.Add(Me.FTSendSSODate_lbl)
        Me.ogbdate.Controls.Add(Me.FNMonth_lbl)
        Me.ogbdate.Controls.Add(Me.FNMonth)
        Me.ogbdate.Controls.Add(Me.FTPayYear)
        Me.ogbdate.Controls.Add(Me.FTPayYear_lbl)
        Me.ogbdate.Location = New System.Drawing.Point(1, 4)
        Me.ogbdate.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbdate.Name = "ogbdate"
        Me.ogbdate.Size = New System.Drawing.Size(723, 97)
        Me.ogbdate.TabIndex = 3
        Me.ogbdate.Text = "Payterm Condition"
        '
        'FTSendSSODate
        '
        Me.FTSendSSODate.EditValue = Nothing
        Me.FTSendSSODate.EnterMoveNextControl = True
        Me.FTSendSSODate.Location = New System.Drawing.Point(154, 62)
        Me.FTSendSSODate.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTSendSSODate.Name = "FTSendSSODate"
        Me.FTSendSSODate.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTSendSSODate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FTSendSSODate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.FTSendSSODate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTSendSSODate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTSendSSODate.Properties.NullDate = ""
        Me.FTSendSSODate.Size = New System.Drawing.Size(148, 22)
        Me.FTSendSSODate.TabIndex = 427
        Me.FTSendSSODate.Tag = "2|"
        '
        'FTSendSSODate_lbl
        '
        Me.FTSendSSODate_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTSendSSODate_lbl.Appearance.Options.UseForeColor = True
        Me.FTSendSSODate_lbl.Appearance.Options.UseTextOptions = True
        Me.FTSendSSODate_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTSendSSODate_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTSendSSODate_lbl.Location = New System.Drawing.Point(29, 60)
        Me.FTSendSSODate_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTSendSSODate_lbl.Name = "FTSendSSODate_lbl"
        Me.FTSendSSODate_lbl.Size = New System.Drawing.Size(122, 23)
        Me.FTSendSSODate_lbl.TabIndex = 428
        Me.FTSendSSODate_lbl.Tag = "2|"
        Me.FTSendSSODate_lbl.Text = "วันที่นำส่ง :"
        '
        'FNMonth_lbl
        '
        Me.FNMonth_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNMonth_lbl.Appearance.Options.UseForeColor = True
        Me.FNMonth_lbl.Appearance.Options.UseTextOptions = True
        Me.FNMonth_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNMonth_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNMonth_lbl.Location = New System.Drawing.Point(313, 36)
        Me.FNMonth_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNMonth_lbl.Name = "FNMonth_lbl"
        Me.FNMonth_lbl.Size = New System.Drawing.Size(125, 21)
        Me.FNMonth_lbl.TabIndex = 426
        Me.FNMonth_lbl.Tag = "2|"
        Me.FNMonth_lbl.Text = "ประจำเดือน :"
        '
        'FNMonth
        '
        Me.FNMonth.EditValue = ""
        Me.FNMonth.EnterMoveNextControl = True
        Me.FNMonth.Location = New System.Drawing.Point(440, 34)
        Me.FNMonth.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNMonth.Name = "FNMonth"
        Me.FNMonth.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FNMonth.Properties.Appearance.Options.UseBackColor = True
        Me.FNMonth.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FNMonth.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FNMonth.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FNMonth.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FNMonth.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNMonth.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNMonth.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNMonth.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNMonth.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNMonth.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNMonth.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNMonth.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNMonth.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FNMonth.Properties.Tag = "FNMonth"
        Me.FNMonth.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.FNMonth.Size = New System.Drawing.Size(199, 22)
        Me.FNMonth.TabIndex = 425
        Me.FNMonth.Tag = "2|"
        '
        'FTPayYear
        '
        Me.FTPayYear.EditValue = ""
        Me.FTPayYear.EnterMoveNextControl = True
        Me.FTPayYear.Location = New System.Drawing.Point(154, 34)
        Me.FTPayYear.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTPayYear.Name = "FTPayYear"
        Me.FTPayYear.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FTPayYear.Properties.Appearance.Options.UseBackColor = True
        Me.FTPayYear.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FTPayYear.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FTPayYear.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FTPayYear.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FTPayYear.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FTPayYear.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FTPayYear.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FTPayYear.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FTPayYear.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FTPayYear.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FTPayYear.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FTPayYear.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FTPayYear.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FTPayYear.Properties.Tag = "FNPayYear"
        Me.FTPayYear.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.FTPayYear.Size = New System.Drawing.Size(148, 22)
        Me.FTPayYear.TabIndex = 400
        Me.FTPayYear.Tag = "2|"
        '
        'FTPayYear_lbl
        '
        Me.FTPayYear_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTPayYear_lbl.Appearance.Options.UseForeColor = True
        Me.FTPayYear_lbl.Appearance.Options.UseTextOptions = True
        Me.FTPayYear_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTPayYear_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTPayYear_lbl.Location = New System.Drawing.Point(22, 36)
        Me.FTPayYear_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTPayYear_lbl.Name = "FTPayYear_lbl"
        Me.FTPayYear_lbl.Size = New System.Drawing.Size(125, 21)
        Me.FTPayYear_lbl.TabIndex = 394
        Me.FTPayYear_lbl.Tag = "2|"
        Me.FTPayYear_lbl.Text = "ปี :"
        '
        'wHRReportSocial
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(728, 542)
        Me.Controls.Add(Me.ogbdate)
        Me.Controls.Add(Me.ogbgrpcondition)
        Me.Controls.Add(Me.ogbreportname)
        Me.Controls.Add(Me.ogbbutton)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "wHRReportSocial"
        Me.Text = "Report"
        CType(Me.ogbbutton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbbutton.ResumeLayout(False)
        CType(Me.ogbreportname, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbreportname.ResumeLayout(False)
        CType(Me.FNReportname.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogbgrpcondition, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbgrpcondition.ResumeLayout(False)
        CType(Me.ogbdate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbdate.ResumeLayout(False)
        CType(Me.FTSendSSODate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTSendSSODate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNMonth.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTPayYear.Properties, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents ogbdate As DevExpress.XtraEditors.GroupControl
    Friend WithEvents FTPayYear_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTPayYear As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents FNMonth_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNMonth As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents FTSendSSODate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents FTSendSSODate_lbl As DevExpress.XtraEditors.LabelControl
End Class

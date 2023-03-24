<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wHRReportLeaveByTerm
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
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.ogbbutton = New DevExpress.XtraEditors.GroupControl()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmpreview = New DevExpress.XtraEditors.SimpleButton()
        Me.ogbreportname = New DevExpress.XtraEditors.GroupControl()
        Me.FNEmpSex_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNReportname = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.ogbgrpcondition = New DevExpress.XtraEditors.GroupControl()
        Me.ogbdate = New DevExpress.XtraEditors.GroupControl()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.FDPayDate = New DevExpress.XtraEditors.TextEdit()
        Me.FTPayYear = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.FDCalDateEnd = New DevExpress.XtraEditors.TextEdit()
        Me.FDCalDateBegin = New DevExpress.XtraEditors.TextEdit()
        Me.FTPayTerm = New DevExpress.XtraEditors.ButtonEdit()
        Me.FTPayTerm_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTPayYear_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.Condition = New HI.HR.HRCondition()
        CType(Me.ogbbutton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbbutton.SuspendLayout()
        CType(Me.ogbreportname, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbreportname.SuspendLayout()
        CType(Me.FNReportname.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogbgrpcondition, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbgrpcondition.SuspendLayout()
        CType(Me.ogbdate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbdate.SuspendLayout()
        CType(Me.FDPayDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTPayYear.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FDCalDateEnd.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FDCalDateBegin.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTPayTerm.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ogbbutton
        '
        Me.ogbbutton.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbbutton.Controls.Add(Me.ocmexit)
        Me.ogbbutton.Controls.Add(Me.ocmpreview)
        Me.ogbbutton.Location = New System.Drawing.Point(2, 420)
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
        Me.ocmpreview.Text = "PREVIEW"
        '
        'ogbreportname
        '
        Me.ogbreportname.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbreportname.Controls.Add(Me.FNEmpSex_lbl)
        Me.ogbreportname.Controls.Add(Me.FNReportname)
        Me.ogbreportname.Location = New System.Drawing.Point(1, 374)
        Me.ogbreportname.Name = "ogbreportname"
        Me.ogbreportname.ShowCaption = False
        Me.ogbreportname.Size = New System.Drawing.Size(620, 42)
        Me.ogbreportname.TabIndex = 1
        Me.ogbreportname.Text = "GroupControl1"
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
        Me.ogbgrpcondition.Location = New System.Drawing.Point(2, 106)
        Me.ogbgrpcondition.Name = "ogbgrpcondition"
        Me.ogbgrpcondition.Size = New System.Drawing.Size(620, 264)
        Me.ogbgrpcondition.TabIndex = 2
        Me.ogbgrpcondition.Text = "Condition"
        '
        'ogbdate
        '
        Me.ogbdate.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbdate.Controls.Add(Me.LabelControl3)
        Me.ogbdate.Controls.Add(Me.LabelControl2)
        Me.ogbdate.Controls.Add(Me.LabelControl1)
        Me.ogbdate.Controls.Add(Me.FDPayDate)
        Me.ogbdate.Controls.Add(Me.FTPayYear)
        Me.ogbdate.Controls.Add(Me.FDCalDateEnd)
        Me.ogbdate.Controls.Add(Me.FDCalDateBegin)
        Me.ogbdate.Controls.Add(Me.FTPayTerm)
        Me.ogbdate.Controls.Add(Me.FTPayTerm_lbl)
        Me.ogbdate.Controls.Add(Me.FTPayYear_lbl)
        Me.ogbdate.Location = New System.Drawing.Point(1, 3)
        Me.ogbdate.Name = "ogbdate"
        Me.ogbdate.Size = New System.Drawing.Size(620, 100)
        Me.ogbdate.TabIndex = 3
        Me.ogbdate.Text = "Payterm Condition"
        '
        'LabelControl3
        '
        Me.LabelControl3.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LabelControl3.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LabelControl3.Location = New System.Drawing.Point(19, 70)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(112, 19)
        Me.LabelControl3.TabIndex = 404
        Me.LabelControl3.Tag = "2|"
        Me.LabelControl3.Text = "วันที่จ่าย :"
        '
        'LabelControl2
        '
        Me.LabelControl2.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LabelControl2.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LabelControl2.Location = New System.Drawing.Point(267, 47)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(89, 19)
        Me.LabelControl2.TabIndex = 403
        Me.LabelControl2.Tag = "2|"
        Me.LabelControl2.Text = "ถึงวันที่ :"
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LabelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LabelControl1.Location = New System.Drawing.Point(19, 48)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(112, 19)
        Me.LabelControl1.TabIndex = 402
        Me.LabelControl1.Tag = "2|"
        Me.LabelControl1.Text = "ประจำวันที่ :"
        '
        'FDPayDate
        '
        Me.FDPayDate.Location = New System.Drawing.Point(132, 69)
        Me.FDPayDate.Name = "FDPayDate"
        Me.FDPayDate.Properties.Appearance.BackColor = System.Drawing.Color.LightCyan
        Me.FDPayDate.Properties.Appearance.Options.UseBackColor = True
        Me.FDPayDate.Properties.ReadOnly = True
        Me.FDPayDate.Size = New System.Drawing.Size(127, 20)
        Me.FDPayDate.TabIndex = 401
        Me.FDPayDate.Tag = "2|"
        '
        'FTPayYear
        '
        Me.FTPayYear.EditValue = ""
        Me.FTPayYear.EnterMoveNextControl = True
        Me.FTPayYear.Location = New System.Drawing.Point(132, 25)
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
        Me.FTPayYear.Size = New System.Drawing.Size(127, 20)
        Me.FTPayYear.TabIndex = 400
        Me.FTPayYear.Tag = "2|"
        '
        'FDCalDateEnd
        '
        Me.FDCalDateEnd.Location = New System.Drawing.Point(359, 48)
        Me.FDCalDateEnd.Name = "FDCalDateEnd"
        Me.FDCalDateEnd.Properties.Appearance.BackColor = System.Drawing.Color.LightCyan
        Me.FDCalDateEnd.Properties.Appearance.Options.UseBackColor = True
        Me.FDCalDateEnd.Properties.ReadOnly = True
        Me.FDCalDateEnd.Size = New System.Drawing.Size(100, 20)
        Me.FDCalDateEnd.TabIndex = 399
        Me.FDCalDateEnd.Tag = "2|"
        '
        'FDCalDateBegin
        '
        Me.FDCalDateBegin.Location = New System.Drawing.Point(132, 47)
        Me.FDCalDateBegin.Name = "FDCalDateBegin"
        Me.FDCalDateBegin.Properties.Appearance.BackColor = System.Drawing.Color.LightCyan
        Me.FDCalDateBegin.Properties.Appearance.Options.UseBackColor = True
        Me.FDCalDateBegin.Properties.ReadOnly = True
        Me.FDCalDateBegin.Size = New System.Drawing.Size(127, 20)
        Me.FDCalDateBegin.TabIndex = 398
        Me.FDCalDateBegin.Tag = "2|"
        '
        'FTPayTerm
        '
        Me.FTPayTerm.Location = New System.Drawing.Point(359, 25)
        Me.FTPayTerm.Name = "FTPayTerm"
        Me.FTPayTerm.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", "237", Nothing, True)})
        Me.FTPayTerm.Properties.Tag = "23"
        Me.FTPayTerm.Size = New System.Drawing.Size(100, 20)
        Me.FTPayTerm.TabIndex = 397
        Me.FTPayTerm.Tag = "2|"
        '
        'FTPayTerm_lbl
        '
        Me.FTPayTerm_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTPayTerm_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTPayTerm_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTPayTerm_lbl.Location = New System.Drawing.Point(272, 27)
        Me.FTPayTerm_lbl.Name = "FTPayTerm_lbl"
        Me.FTPayTerm_lbl.Size = New System.Drawing.Size(84, 15)
        Me.FTPayTerm_lbl.TabIndex = 396
        Me.FTPayTerm_lbl.Tag = "2|"
        Me.FTPayTerm_lbl.Text = "งวดที่ :"
        '
        'FTPayYear_lbl
        '
        Me.FTPayYear_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTPayYear_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTPayYear_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTPayYear_lbl.Location = New System.Drawing.Point(19, 26)
        Me.FTPayYear_lbl.Name = "FTPayYear_lbl"
        Me.FTPayYear_lbl.Size = New System.Drawing.Size(107, 17)
        Me.FTPayYear_lbl.TabIndex = 394
        Me.FTPayYear_lbl.Tag = "2|"
        Me.FTPayYear_lbl.Text = "ปี :"
        '
        'Condition
        '
        Me.Condition.CommandTextDepartment = Nothing
        Me.Condition.Location = New System.Drawing.Point(28, 21)
        Me.Condition.Margin = New System.Windows.Forms.Padding(4)
        Me.Condition.Name = "Condition"
        Me.Condition.SelectedPageIndex = 0
        Me.Condition.ShowmDepartment = False
        Me.Condition.ShowmDivision = False
        Me.Condition.ShowmEmployee = False
        Me.Condition.ShowmEmployeeType = True
        Me.Condition.ShowmSect = False
        Me.Condition.ShowmUnitSect = False
        Me.Condition.Size = New System.Drawing.Size(572, 239)
        Me.Condition.TabIndex = 0
        '
        'wHRReportLeaveByTerm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(624, 464)
        Me.Controls.Add(Me.ogbdate)
        Me.Controls.Add(Me.ogbgrpcondition)
        Me.Controls.Add(Me.ogbreportname)
        Me.Controls.Add(Me.ogbbutton)
        Me.Name = "wHRReportLeaveByTerm"
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
        CType(Me.FDPayDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTPayYear.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FDCalDateEnd.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FDCalDateBegin.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTPayTerm.Properties, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents FDCalDateEnd As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FDCalDateBegin As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FTPayTerm As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FTPayTerm_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTPayYear_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTPayYear As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FDPayDate As DevExpress.XtraEditors.TextEdit

End Class

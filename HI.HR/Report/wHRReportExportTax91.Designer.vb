<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wHRReportExportTax91
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
        Me.Condition = New HI.HR.HRCondition()
        Me.ogbdate = New DevExpress.XtraEditors.GroupControl()
        Me.FNHSysEmpTypeId = New DevExpress.XtraEditors.ButtonEdit()
        Me.FNHSysEmpTypeId_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysEmpTypeId_None = New DevExpress.XtraEditors.TextEdit()
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
        CType(Me.FNHSysEmpTypeId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysEmpTypeId_None.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTPayYear.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ogbbutton
        '
        Me.ogbbutton.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbbutton.Controls.Add(Me.ocmexit)
        Me.ogbbutton.Controls.Add(Me.ocmpreview)
        Me.ogbbutton.Location = New System.Drawing.Point(2, 394)
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
        Me.ogbreportname.Location = New System.Drawing.Point(1, 349)
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
        Me.ogbgrpcondition.Location = New System.Drawing.Point(2, 83)
        Me.ogbgrpcondition.Name = "ogbgrpcondition"
        Me.ogbgrpcondition.Size = New System.Drawing.Size(620, 264)
        Me.ogbgrpcondition.TabIndex = 2
        Me.ogbgrpcondition.Text = "Condition"
        '
        'Condition
        '
        Me.Condition.CommandTextDepartment = Nothing
        Me.Condition.Location = New System.Drawing.Point(28, 21)
        Me.Condition.Name = "Condition"
        Me.Condition.SelectedPageIndex = 1
        Me.Condition.ShowmDepartment = True
        Me.Condition.ShowmDivision = True
        Me.Condition.ShowmEmployee = True
        Me.Condition.ShowmEmployeeType = False
        Me.Condition.ShowmSect = True
        Me.Condition.ShowmUnitSect = True
        Me.Condition.Size = New System.Drawing.Size(572, 239)
        Me.Condition.TabIndex = 0
        '
        'ogbdate
        '
        Me.ogbdate.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbdate.Controls.Add(Me.FNHSysEmpTypeId)
        Me.ogbdate.Controls.Add(Me.FNHSysEmpTypeId_lbl)
        Me.ogbdate.Controls.Add(Me.FNHSysEmpTypeId_None)
        Me.ogbdate.Controls.Add(Me.FTPayYear)
        Me.ogbdate.Controls.Add(Me.FTPayYear_lbl)
        Me.ogbdate.Location = New System.Drawing.Point(1, 3)
        Me.ogbdate.Name = "ogbdate"
        Me.ogbdate.Size = New System.Drawing.Size(620, 78)
        Me.ogbdate.TabIndex = 3
        Me.ogbdate.Text = "Payterm Condition"
        '
        'FNHSysEmpTypeId
        '
        Me.FNHSysEmpTypeId.Location = New System.Drawing.Point(132, 24)
        Me.FNHSysEmpTypeId.Name = "FNHSysEmpTypeId"
        Me.FNHSysEmpTypeId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", "42", Nothing, True)})
        Me.FNHSysEmpTypeId.Properties.Tag = ""
        Me.FNHSysEmpTypeId.Size = New System.Drawing.Size(127, 20)
        Me.FNHSysEmpTypeId.TabIndex = 405
        Me.FNHSysEmpTypeId.Tag = "2|"
        '
        'FNHSysEmpTypeId_lbl
        '
        Me.FNHSysEmpTypeId_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysEmpTypeId_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysEmpTypeId_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysEmpTypeId_lbl.Location = New System.Drawing.Point(25, 24)
        Me.FNHSysEmpTypeId_lbl.Name = "FNHSysEmpTypeId_lbl"
        Me.FNHSysEmpTypeId_lbl.Size = New System.Drawing.Size(107, 17)
        Me.FNHSysEmpTypeId_lbl.TabIndex = 406
        Me.FNHSysEmpTypeId_lbl.Tag = "2|"
        Me.FNHSysEmpTypeId_lbl.Text = "Employee Type :"
        '
        'FNHSysEmpTypeId_None
        '
        Me.FNHSysEmpTypeId_None.Location = New System.Drawing.Point(261, 24)
        Me.FNHSysEmpTypeId_None.Name = "FNHSysEmpTypeId_None"
        Me.FNHSysEmpTypeId_None.Properties.Appearance.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysEmpTypeId_None.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysEmpTypeId_None.Properties.ReadOnly = True
        Me.FNHSysEmpTypeId_None.Size = New System.Drawing.Size(228, 20)
        Me.FNHSysEmpTypeId_None.TabIndex = 407
        Me.FNHSysEmpTypeId_None.Tag = "2|"
        '
        'FTPayYear
        '
        Me.FTPayYear.EditValue = ""
        Me.FTPayYear.EnterMoveNextControl = True
        Me.FTPayYear.Location = New System.Drawing.Point(132, 46)
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
        'FTPayYear_lbl
        '
        Me.FTPayYear_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTPayYear_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTPayYear_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTPayYear_lbl.Location = New System.Drawing.Point(19, 47)
        Me.FTPayYear_lbl.Name = "FTPayYear_lbl"
        Me.FTPayYear_lbl.Size = New System.Drawing.Size(107, 17)
        Me.FTPayYear_lbl.TabIndex = 394
        Me.FTPayYear_lbl.Tag = "2|"
        Me.FTPayYear_lbl.Text = "ปี :"
        '
        'wHRReportExportTax91
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(624, 438)
        Me.Controls.Add(Me.ogbdate)
        Me.Controls.Add(Me.ogbgrpcondition)
        Me.Controls.Add(Me.ogbreportname)
        Me.Controls.Add(Me.ogbbutton)
        Me.Name = "wHRReportExportTax91"
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
        CType(Me.FNHSysEmpTypeId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysEmpTypeId_None.Properties, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents FNHSysEmpTypeId As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FNHSysEmpTypeId_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysEmpTypeId_None As DevExpress.XtraEditors.TextEdit
End Class

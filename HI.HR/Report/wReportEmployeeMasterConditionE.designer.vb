<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wReportEmployeeMasterConditionE
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
        Me.ogbdate = New DevExpress.XtraEditors.GroupControl()
        Me.FDEDateProbation = New DevExpress.XtraEditors.DateEdit()
        Me.FDEDateProbation_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FDSDateProbation = New DevExpress.XtraEditors.DateEdit()
        Me.FDSDateProbation_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.DateEdit2 = New DevExpress.XtraEditors.DateEdit()
        Me.FDBirthEnd = New DevExpress.XtraEditors.DateEdit()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.FDBirthEnd_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FDCard = New DevExpress.XtraEditors.DateEdit()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.FDBirthStart = New DevExpress.XtraEditors.DateEdit()
        Me.FDBirthStart_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FDResignEnd = New DevExpress.XtraEditors.DateEdit()
        Me.FDResignEnd_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FDResignStart = New DevExpress.XtraEditors.DateEdit()
        Me.FDResignStart_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FDWorkEnd = New DevExpress.XtraEditors.DateEdit()
        Me.FDWorkEnd_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FDWorkStart = New DevExpress.XtraEditors.DateEdit()
        Me.FDWorkStart_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.FNEmpStatus = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
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
        CType(Me.FDEDateProbation.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FDEDateProbation.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FDSDateProbation.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FDSDateProbation.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DateEdit2.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DateEdit2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FDBirthEnd.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FDBirthEnd.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FDCard.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FDCard.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FDBirthStart.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FDBirthStart.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FDResignEnd.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FDResignEnd.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FDResignStart.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FDResignStart.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FDWorkEnd.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FDWorkEnd.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FDWorkStart.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FDWorkStart.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.FNEmpStatus.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ogbbutton
        '
        Me.ogbbutton.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbbutton.Controls.Add(Me.ocmexit)
        Me.ogbbutton.Controls.Add(Me.ocmpreview)
        Me.ogbbutton.Location = New System.Drawing.Point(1, 484)
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
        Me.ogbreportname.Location = New System.Drawing.Point(2, 440)
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
        Me.ogbgrpcondition.Location = New System.Drawing.Point(2, 184)
        Me.ogbgrpcondition.Name = "ogbgrpcondition"
        Me.ogbgrpcondition.Size = New System.Drawing.Size(620, 254)
        Me.ogbgrpcondition.TabIndex = 2
        Me.ogbgrpcondition.Text = "Condition"
        '
        'ogbdate
        '
        Me.ogbdate.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbdate.Controls.Add(Me.FDEDateProbation)
        Me.ogbdate.Controls.Add(Me.FDEDateProbation_lbl)
        Me.ogbdate.Controls.Add(Me.FDSDateProbation)
        Me.ogbdate.Controls.Add(Me.FDSDateProbation_lbl)
        Me.ogbdate.Controls.Add(Me.DateEdit2)
        Me.ogbdate.Controls.Add(Me.FDBirthEnd)
        Me.ogbdate.Controls.Add(Me.LabelControl2)
        Me.ogbdate.Controls.Add(Me.FDBirthEnd_lbl)
        Me.ogbdate.Controls.Add(Me.FDCard)
        Me.ogbdate.Controls.Add(Me.LabelControl1)
        Me.ogbdate.Controls.Add(Me.FDBirthStart)
        Me.ogbdate.Controls.Add(Me.FDBirthStart_lbl)
        Me.ogbdate.Controls.Add(Me.FDResignEnd)
        Me.ogbdate.Controls.Add(Me.FDResignEnd_lbl)
        Me.ogbdate.Controls.Add(Me.FDResignStart)
        Me.ogbdate.Controls.Add(Me.FDResignStart_lbl)
        Me.ogbdate.Controls.Add(Me.FDWorkEnd)
        Me.ogbdate.Controls.Add(Me.FDWorkEnd_lbl)
        Me.ogbdate.Controls.Add(Me.FDWorkStart)
        Me.ogbdate.Controls.Add(Me.FDWorkStart_lbl)
        Me.ogbdate.Location = New System.Drawing.Point(1, 3)
        Me.ogbdate.Name = "ogbdate"
        Me.ogbdate.Size = New System.Drawing.Size(620, 139)
        Me.ogbdate.TabIndex = 3
        Me.ogbdate.Text = "Date Condition"
        '
        'FDEDateProbation
        '
        Me.FDEDateProbation.EditValue = Nothing
        Me.FDEDateProbation.EnterMoveNextControl = True
        Me.FDEDateProbation.Location = New System.Drawing.Point(404, 49)
        Me.FDEDateProbation.Name = "FDEDateProbation"
        Me.FDEDateProbation.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.FDEDateProbation.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FDEDateProbation.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.FDEDateProbation.Properties.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.FDEDateProbation.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FDEDateProbation.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.FDEDateProbation.Properties.NullDate = ""
        Me.FDEDateProbation.Size = New System.Drawing.Size(120, 20)
        Me.FDEDateProbation.TabIndex = 3
        Me.FDEDateProbation.Tag = "2|"
        '
        'FDEDateProbation_lbl
        '
        Me.FDEDateProbation_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FDEDateProbation_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FDEDateProbation_lbl.Location = New System.Drawing.Point(291, 48)
        Me.FDEDateProbation_lbl.Name = "FDEDateProbation_lbl"
        Me.FDEDateProbation_lbl.Size = New System.Drawing.Size(110, 20)
        Me.FDEDateProbation_lbl.TabIndex = 296
        Me.FDEDateProbation_lbl.Tag = "2|"
        Me.FDEDateProbation_lbl.Text = "ถึงวันที่ :"
        '
        'FDSDateProbation
        '
        Me.FDSDateProbation.EditValue = Nothing
        Me.FDSDateProbation.EnterMoveNextControl = True
        Me.FDSDateProbation.Location = New System.Drawing.Point(140, 49)
        Me.FDSDateProbation.Name = "FDSDateProbation"
        Me.FDSDateProbation.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.FDSDateProbation.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FDSDateProbation.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.FDSDateProbation.Properties.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.FDSDateProbation.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FDSDateProbation.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.FDSDateProbation.Properties.NullDate = ""
        Me.FDSDateProbation.Size = New System.Drawing.Size(120, 20)
        Me.FDSDateProbation.TabIndex = 2
        Me.FDSDateProbation.Tag = "2|"
        '
        'FDSDateProbation_lbl
        '
        Me.FDSDateProbation_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FDSDateProbation_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FDSDateProbation_lbl.Location = New System.Drawing.Point(27, 47)
        Me.FDSDateProbation_lbl.Name = "FDSDateProbation_lbl"
        Me.FDSDateProbation_lbl.Size = New System.Drawing.Size(110, 20)
        Me.FDSDateProbation_lbl.TabIndex = 294
        Me.FDSDateProbation_lbl.Tag = "2|"
        Me.FDSDateProbation_lbl.Text = "วันที่ผ่านโปร :"
        '
        'DateEdit2
        '
        Me.DateEdit2.EditValue = Nothing
        Me.DateEdit2.EnterMoveNextControl = True
        Me.DateEdit2.Location = New System.Drawing.Point(404, 114)
        Me.DateEdit2.Name = "DateEdit2"
        Me.DateEdit2.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.DateEdit2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.DateEdit2.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.DateEdit2.Properties.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.DateEdit2.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.DateEdit2.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.DateEdit2.Properties.NullDate = ""
        Me.DateEdit2.Size = New System.Drawing.Size(120, 20)
        Me.DateEdit2.TabIndex = 7
        Me.DateEdit2.Tag = "2|"
        '
        'FDBirthEnd
        '
        Me.FDBirthEnd.EditValue = Nothing
        Me.FDBirthEnd.EnterMoveNextControl = True
        Me.FDBirthEnd.Location = New System.Drawing.Point(404, 93)
        Me.FDBirthEnd.Name = "FDBirthEnd"
        Me.FDBirthEnd.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.FDBirthEnd.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FDBirthEnd.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.FDBirthEnd.Properties.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.FDBirthEnd.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FDBirthEnd.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.FDBirthEnd.Properties.NullDate = ""
        Me.FDBirthEnd.Size = New System.Drawing.Size(120, 20)
        Me.FDBirthEnd.TabIndex = 7
        Me.FDBirthEnd.Tag = "2|"
        '
        'LabelControl2
        '
        Me.LabelControl2.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LabelControl2.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LabelControl2.Location = New System.Drawing.Point(291, 113)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(110, 20)
        Me.LabelControl2.TabIndex = 292
        Me.LabelControl2.Tag = "2|"
        Me.LabelControl2.Text = "ถึงวันที่ :"
        '
        'FDBirthEnd_lbl
        '
        Me.FDBirthEnd_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FDBirthEnd_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FDBirthEnd_lbl.Location = New System.Drawing.Point(291, 92)
        Me.FDBirthEnd_lbl.Name = "FDBirthEnd_lbl"
        Me.FDBirthEnd_lbl.Size = New System.Drawing.Size(110, 20)
        Me.FDBirthEnd_lbl.TabIndex = 292
        Me.FDBirthEnd_lbl.Tag = "2|"
        Me.FDBirthEnd_lbl.Text = "ถึงวันที่ :"
        '
        'FDCard
        '
        Me.FDCard.EditValue = Nothing
        Me.FDCard.EnterMoveNextControl = True
        Me.FDCard.Location = New System.Drawing.Point(140, 114)
        Me.FDCard.Name = "FDCard"
        Me.FDCard.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.FDCard.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FDCard.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.FDCard.Properties.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.FDCard.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FDCard.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.FDCard.Properties.NullDate = ""
        Me.FDCard.Size = New System.Drawing.Size(120, 20)
        Me.FDCard.TabIndex = 6
        Me.FDCard.Tag = "2|"
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LabelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LabelControl1.Location = New System.Drawing.Point(27, 113)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(110, 20)
        Me.LabelControl1.TabIndex = 290
        Me.LabelControl1.Tag = "2|"
        Me.LabelControl1.Text = "วันที่เกิด :"
        '
        'FDBirthStart
        '
        Me.FDBirthStart.EditValue = Nothing
        Me.FDBirthStart.EnterMoveNextControl = True
        Me.FDBirthStart.Location = New System.Drawing.Point(140, 93)
        Me.FDBirthStart.Name = "FDBirthStart"
        Me.FDBirthStart.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.FDBirthStart.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FDBirthStart.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.FDBirthStart.Properties.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.FDBirthStart.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FDBirthStart.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.FDBirthStart.Properties.NullDate = ""
        Me.FDBirthStart.Size = New System.Drawing.Size(120, 20)
        Me.FDBirthStart.TabIndex = 6
        Me.FDBirthStart.Tag = "2|"
        '
        'FDBirthStart_lbl
        '
        Me.FDBirthStart_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FDBirthStart_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FDBirthStart_lbl.Location = New System.Drawing.Point(27, 92)
        Me.FDBirthStart_lbl.Name = "FDBirthStart_lbl"
        Me.FDBirthStart_lbl.Size = New System.Drawing.Size(110, 20)
        Me.FDBirthStart_lbl.TabIndex = 290
        Me.FDBirthStart_lbl.Tag = "2|"
        Me.FDBirthStart_lbl.Text = "วันที่เกิด :"
        '
        'FDResignEnd
        '
        Me.FDResignEnd.EditValue = Nothing
        Me.FDResignEnd.EnterMoveNextControl = True
        Me.FDResignEnd.Location = New System.Drawing.Point(404, 71)
        Me.FDResignEnd.Name = "FDResignEnd"
        Me.FDResignEnd.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.FDResignEnd.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FDResignEnd.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.FDResignEnd.Properties.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.FDResignEnd.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FDResignEnd.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.FDResignEnd.Properties.NullDate = ""
        Me.FDResignEnd.Size = New System.Drawing.Size(120, 20)
        Me.FDResignEnd.TabIndex = 5
        Me.FDResignEnd.Tag = "2|"
        '
        'FDResignEnd_lbl
        '
        Me.FDResignEnd_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FDResignEnd_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FDResignEnd_lbl.Location = New System.Drawing.Point(291, 70)
        Me.FDResignEnd_lbl.Name = "FDResignEnd_lbl"
        Me.FDResignEnd_lbl.Size = New System.Drawing.Size(110, 20)
        Me.FDResignEnd_lbl.TabIndex = 288
        Me.FDResignEnd_lbl.Tag = "2|"
        Me.FDResignEnd_lbl.Text = "ถึงวันที่ :"
        '
        'FDResignStart
        '
        Me.FDResignStart.EditValue = Nothing
        Me.FDResignStart.EnterMoveNextControl = True
        Me.FDResignStart.Location = New System.Drawing.Point(140, 71)
        Me.FDResignStart.Name = "FDResignStart"
        Me.FDResignStart.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.FDResignStart.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FDResignStart.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.FDResignStart.Properties.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.FDResignStart.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FDResignStart.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.FDResignStart.Properties.NullDate = ""
        Me.FDResignStart.Size = New System.Drawing.Size(120, 20)
        Me.FDResignStart.TabIndex = 4
        Me.FDResignStart.Tag = "2|"
        '
        'FDResignStart_lbl
        '
        Me.FDResignStart_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FDResignStart_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FDResignStart_lbl.Location = New System.Drawing.Point(27, 70)
        Me.FDResignStart_lbl.Name = "FDResignStart_lbl"
        Me.FDResignStart_lbl.Size = New System.Drawing.Size(110, 20)
        Me.FDResignStart_lbl.TabIndex = 286
        Me.FDResignStart_lbl.Tag = "2|"
        Me.FDResignStart_lbl.Text = "วันที่ลาออก :"
        '
        'FDWorkEnd
        '
        Me.FDWorkEnd.EditValue = Nothing
        Me.FDWorkEnd.EnterMoveNextControl = True
        Me.FDWorkEnd.Location = New System.Drawing.Point(404, 27)
        Me.FDWorkEnd.Name = "FDWorkEnd"
        Me.FDWorkEnd.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.FDWorkEnd.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FDWorkEnd.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.FDWorkEnd.Properties.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.FDWorkEnd.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FDWorkEnd.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.FDWorkEnd.Properties.NullDate = ""
        Me.FDWorkEnd.Size = New System.Drawing.Size(120, 20)
        Me.FDWorkEnd.TabIndex = 1
        Me.FDWorkEnd.Tag = "2|"
        '
        'FDWorkEnd_lbl
        '
        Me.FDWorkEnd_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FDWorkEnd_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FDWorkEnd_lbl.Location = New System.Drawing.Point(291, 26)
        Me.FDWorkEnd_lbl.Name = "FDWorkEnd_lbl"
        Me.FDWorkEnd_lbl.Size = New System.Drawing.Size(110, 20)
        Me.FDWorkEnd_lbl.TabIndex = 280
        Me.FDWorkEnd_lbl.Tag = "2|"
        Me.FDWorkEnd_lbl.Text = "ถึงวันที่ :"
        '
        'FDWorkStart
        '
        Me.FDWorkStart.EditValue = Nothing
        Me.FDWorkStart.EnterMoveNextControl = True
        Me.FDWorkStart.Location = New System.Drawing.Point(140, 27)
        Me.FDWorkStart.Name = "FDWorkStart"
        Me.FDWorkStart.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.FDWorkStart.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FDWorkStart.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.FDWorkStart.Properties.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.FDWorkStart.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FDWorkStart.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.FDWorkStart.Properties.NullDate = ""
        Me.FDWorkStart.Size = New System.Drawing.Size(120, 20)
        Me.FDWorkStart.TabIndex = 0
        Me.FDWorkStart.Tag = "2|"
        '
        'FDWorkStart_lbl
        '
        Me.FDWorkStart_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FDWorkStart_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FDWorkStart_lbl.Location = New System.Drawing.Point(27, 25)
        Me.FDWorkStart_lbl.Name = "FDWorkStart_lbl"
        Me.FDWorkStart_lbl.Size = New System.Drawing.Size(110, 20)
        Me.FDWorkStart_lbl.TabIndex = 278
        Me.FDWorkStart_lbl.Tag = "2|"
        Me.FDWorkStart_lbl.Text = "วันที่เริ่มงาน :"
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Me.FNEmpStatus)
        Me.GroupControl1.Controls.Add(Me.LabelControl3)
        Me.GroupControl1.Location = New System.Drawing.Point(2, 143)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.ShowCaption = False
        Me.GroupControl1.Size = New System.Drawing.Size(619, 37)
        Me.GroupControl1.TabIndex = 4
        Me.GroupControl1.Text = "GroupControl1"
        '
        'FNEmpStatus
        '
        Me.FNEmpStatus.Location = New System.Drawing.Point(277, 9)
        Me.FNEmpStatus.Name = "FNEmpStatus"
        Me.FNEmpStatus.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FNEmpStatus.Properties.Tag = "FNEmpStatusReport"
        Me.FNEmpStatus.Size = New System.Drawing.Size(120, 20)
        Me.FNEmpStatus.TabIndex = 1
        '
        'LabelControl3
        '
        Me.LabelControl3.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LabelControl3.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LabelControl3.Location = New System.Drawing.Point(138, 10)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(136, 18)
        Me.LabelControl3.TabIndex = 0
        Me.LabelControl3.Text = "LabelControl3"
        '
        'Condition
        '
        Me.Condition.CommandTextDepartment = Nothing
        Me.Condition.Location = New System.Drawing.Point(28, 21)
        Me.Condition.Margin = New System.Windows.Forms.Padding(4)
        Me.Condition.Name = "Condition"
        Me.Condition.SelectedPageIndex = 0
        Me.Condition.ShowmDepartment = True
        Me.Condition.ShowmDivision = True
        Me.Condition.ShowmEmployee = True
        Me.Condition.ShowmEmployeeType = True
        Me.Condition.ShowmSect = True
        Me.Condition.ShowmUnitSect = True
        Me.Condition.Size = New System.Drawing.Size(572, 239)
        Me.Condition.TabIndex = 0
        '
        'wReportEmployeeMasterConditionE
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(624, 529)
        Me.Controls.Add(Me.GroupControl1)
        Me.Controls.Add(Me.ogbdate)
        Me.Controls.Add(Me.ogbgrpcondition)
        Me.Controls.Add(Me.ogbreportname)
        Me.Controls.Add(Me.ogbbutton)
        Me.Name = "wReportEmployeeMasterConditionE"
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
        CType(Me.FDEDateProbation.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FDEDateProbation.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FDSDateProbation.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FDSDateProbation.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DateEdit2.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DateEdit2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FDBirthEnd.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FDBirthEnd.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FDCard.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FDCard.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FDBirthStart.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FDBirthStart.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FDResignEnd.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FDResignEnd.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FDResignStart.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FDResignStart.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FDWorkEnd.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FDWorkEnd.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FDWorkStart.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FDWorkStart.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        CType(Me.FNEmpStatus.Properties, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents FDWorkEnd As DevExpress.XtraEditors.DateEdit
    Friend WithEvents FDWorkEnd_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FDWorkStart As DevExpress.XtraEditors.DateEdit
    Friend WithEvents FDWorkStart_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FDResignEnd As DevExpress.XtraEditors.DateEdit
    Friend WithEvents FDResignEnd_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FDResignStart As DevExpress.XtraEditors.DateEdit
    Friend WithEvents FDResignStart_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FDBirthEnd As DevExpress.XtraEditors.DateEdit
    Friend WithEvents FDBirthEnd_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FDBirthStart As DevExpress.XtraEditors.DateEdit
    Friend WithEvents FDBirthStart_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FDEDateProbation As DevExpress.XtraEditors.DateEdit
    Friend WithEvents FDEDateProbation_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FDSDateProbation As DevExpress.XtraEditors.DateEdit
    Friend WithEvents FDSDateProbation_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents DateEdit2 As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FDCard As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents FNEmpStatus As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
End Class

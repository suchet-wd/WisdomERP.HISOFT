<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wAddEmpHealthCost
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
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject4 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.ogbbutton = New DevExpress.XtraEditors.GroupControl()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmsave = New DevExpress.XtraEditors.SimpleButton()
        Me.ogbemployee = New DevExpress.XtraEditors.GroupControl()
        Me.FTNote_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTNote = New DevExpress.XtraEditors.MemoEdit()
        Me.FCDisburse_lbl2 = New DevExpress.XtraEditors.LabelControl()
        Me.FCDisburse_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FCDisburse = New DevExpress.XtraEditors.CalcEdit()
        Me.FCSocial_lbl2 = New DevExpress.XtraEditors.LabelControl()
        Me.FCSocial_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FCSocial = New DevExpress.XtraEditors.CalcEdit()
        Me.FCMedical_lbl2 = New DevExpress.XtraEditors.LabelControl()
        Me.FCMedical_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FCMedical = New DevExpress.XtraEditors.CalcEdit()
        Me.FNPatients_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNPatients = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.FTBillNo_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTBillNo = New DevExpress.XtraEditors.TextEdit()
        Me.FNHSysHospitalId_None = New DevExpress.XtraEditors.TextEdit()
        Me.FNHSysHospitalId_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysHospitalId = New DevExpress.XtraEditors.ButtonEdit()
        Me.FDTreatment = New DevExpress.XtraEditors.DateEdit()
        Me.FDTreatment_lbl = New DevExpress.XtraEditors.LabelControl()
        CType(Me.ogbbutton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbbutton.SuspendLayout()
        CType(Me.ogbemployee, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbemployee.SuspendLayout()
        CType(Me.FTNote.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FCDisburse.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FCSocial.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FCMedical.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNPatients.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTBillNo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysHospitalId_None.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysHospitalId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FDTreatment.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FDTreatment.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ogbbutton
        '
        Me.ogbbutton.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbbutton.Controls.Add(Me.ocmexit)
        Me.ogbbutton.Controls.Add(Me.ocmsave)
        Me.ogbbutton.Location = New System.Drawing.Point(1, 240)
        Me.ogbbutton.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbbutton.Name = "ogbbutton"
        Me.ogbbutton.ShowCaption = False
        Me.ogbbutton.Size = New System.Drawing.Size(966, 52)
        Me.ogbbutton.TabIndex = 17
        Me.ogbbutton.Text = "GroupControl9"
        '
        'ocmexit
        '
        Me.ocmexit.Location = New System.Drawing.Point(672, 15)
        Me.ocmexit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmexit.Name = "ocmexit"
        Me.ocmexit.Size = New System.Drawing.Size(181, 31)
        Me.ocmexit.TabIndex = 1
        Me.ocmexit.TabStop = False
        Me.ocmexit.Tag = "2|"
        Me.ocmexit.Text = "EXIT"
        '
        'ocmsave
        '
        Me.ocmsave.Location = New System.Drawing.Point(142, 10)
        Me.ocmsave.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmsave.Name = "ocmsave"
        Me.ocmsave.Size = New System.Drawing.Size(181, 31)
        Me.ocmsave.TabIndex = 0
        Me.ocmsave.TabStop = False
        Me.ocmsave.Tag = "2|"
        Me.ocmsave.Text = "ADD"
        '
        'ogbemployee
        '
        Me.ogbemployee.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbemployee.AppearanceCaption.Options.UseTextOptions = True
        Me.ogbemployee.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ogbemployee.Controls.Add(Me.FTNote_lbl)
        Me.ogbemployee.Controls.Add(Me.FTNote)
        Me.ogbemployee.Controls.Add(Me.FCDisburse_lbl2)
        Me.ogbemployee.Controls.Add(Me.FCDisburse_lbl)
        Me.ogbemployee.Controls.Add(Me.FCDisburse)
        Me.ogbemployee.Controls.Add(Me.FCSocial_lbl2)
        Me.ogbemployee.Controls.Add(Me.FCSocial_lbl)
        Me.ogbemployee.Controls.Add(Me.FCSocial)
        Me.ogbemployee.Controls.Add(Me.FCMedical_lbl2)
        Me.ogbemployee.Controls.Add(Me.FCMedical_lbl)
        Me.ogbemployee.Controls.Add(Me.FCMedical)
        Me.ogbemployee.Controls.Add(Me.FNPatients_lbl)
        Me.ogbemployee.Controls.Add(Me.FNPatients)
        Me.ogbemployee.Controls.Add(Me.FTBillNo_lbl)
        Me.ogbemployee.Controls.Add(Me.FTBillNo)
        Me.ogbemployee.Controls.Add(Me.FNHSysHospitalId_None)
        Me.ogbemployee.Controls.Add(Me.FNHSysHospitalId_lbl)
        Me.ogbemployee.Controls.Add(Me.FNHSysHospitalId)
        Me.ogbemployee.Controls.Add(Me.FDTreatment)
        Me.ogbemployee.Controls.Add(Me.FDTreatment_lbl)
        Me.ogbemployee.Location = New System.Drawing.Point(3, 5)
        Me.ogbemployee.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbemployee.Name = "ogbemployee"
        Me.ogbemployee.ShowCaption = False
        Me.ogbemployee.Size = New System.Drawing.Size(965, 231)
        Me.ogbemployee.TabIndex = 18
        Me.ogbemployee.Text = "Leave Detail"
        '
        'FTNote_lbl
        '
        Me.FTNote_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTNote_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTNote_lbl.Location = New System.Drawing.Point(12, 92)
        Me.FTNote_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTNote_lbl.Name = "FTNote_lbl"
        Me.FTNote_lbl.Size = New System.Drawing.Size(126, 25)
        Me.FTNote_lbl.TabIndex = 403
        Me.FTNote_lbl.Tag = "2|"
        Me.FTNote_lbl.Text = "หมายเหตุ :"
        '
        'FTNote
        '
        Me.FTNote.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FTNote.EditValue = ""
        Me.FTNote.Location = New System.Drawing.Point(141, 90)
        Me.FTNote.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTNote.Name = "FTNote"
        Me.FTNote.Properties.MaxLength = 500
        Me.FTNote.Size = New System.Drawing.Size(818, 135)
        Me.FTNote.TabIndex = 7
        Me.FTNote.Tag = "2|"
        Me.FTNote.UseOptimizedRendering = True
        '
        'FCDisburse_lbl2
        '
        Me.FCDisburse_lbl2.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.FCDisburse_lbl2.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FCDisburse_lbl2.Location = New System.Drawing.Point(925, 63)
        Me.FCDisburse_lbl2.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FCDisburse_lbl2.Name = "FCDisburse_lbl2"
        Me.FCDisburse_lbl2.Size = New System.Drawing.Size(34, 25)
        Me.FCDisburse_lbl2.TabIndex = 401
        Me.FCDisburse_lbl2.Tag = "2|"
        Me.FCDisburse_lbl2.Text = "บาท"
        '
        'FCDisburse_lbl
        '
        Me.FCDisburse_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FCDisburse_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FCDisburse_lbl.Location = New System.Drawing.Point(608, 62)
        Me.FCDisburse_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FCDisburse_lbl.Name = "FCDisburse_lbl"
        Me.FCDisburse_lbl.Size = New System.Drawing.Size(231, 25)
        Me.FCDisburse_lbl.TabIndex = 400
        Me.FCDisburse_lbl.Tag = "2|"
        Me.FCDisburse_lbl.Text = "จำนวนเงินที่อนุมัติให้เบิก :"
        '
        'FCDisburse
        '
        Me.FCDisburse.EnterMoveNextControl = True
        Me.FCDisburse.Location = New System.Drawing.Point(839, 63)
        Me.FCDisburse.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FCDisburse.Name = "FCDisburse"
        Me.FCDisburse.Properties.Appearance.Options.UseTextOptions = True
        Me.FCDisburse.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FCDisburse.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FCDisburse.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FCDisburse.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FCDisburse.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FCDisburse.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FCDisburse.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FCDisburse.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FCDisburse.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FCDisburse.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, True, False, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", Nothing, Nothing, True)})
        Me.FCDisburse.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FCDisburse.Properties.Precision = 2
        Me.FCDisburse.Size = New System.Drawing.Size(82, 22)
        Me.FCDisburse.TabIndex = 6
        Me.FCDisburse.Tag = "2|"
        '
        'FCSocial_lbl2
        '
        Me.FCSocial_lbl2.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.FCSocial_lbl2.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FCSocial_lbl2.Location = New System.Drawing.Point(572, 62)
        Me.FCSocial_lbl2.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FCSocial_lbl2.Name = "FCSocial_lbl2"
        Me.FCSocial_lbl2.Size = New System.Drawing.Size(34, 25)
        Me.FCSocial_lbl2.TabIndex = 398
        Me.FCSocial_lbl2.Tag = "2|"
        Me.FCSocial_lbl2.Text = "บาท"
        '
        'FCSocial_lbl
        '
        Me.FCSocial_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FCSocial_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FCSocial_lbl.Location = New System.Drawing.Point(272, 60)
        Me.FCSocial_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FCSocial_lbl.Name = "FCSocial_lbl"
        Me.FCSocial_lbl.Size = New System.Drawing.Size(204, 25)
        Me.FCSocial_lbl.TabIndex = 397
        Me.FCSocial_lbl.Tag = "2|"
        Me.FCSocial_lbl.Text = "จำนวนเงินเบิก ปกส. :"
        '
        'FCSocial
        '
        Me.FCSocial.EnterMoveNextControl = True
        Me.FCSocial.Location = New System.Drawing.Point(479, 62)
        Me.FCSocial.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FCSocial.Name = "FCSocial"
        Me.FCSocial.Properties.Appearance.Options.UseTextOptions = True
        Me.FCSocial.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FCSocial.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FCSocial.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FCSocial.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FCSocial.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FCSocial.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FCSocial.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FCSocial.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FCSocial.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FCSocial.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, True, False, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject2, "", Nothing, Nothing, True)})
        Me.FCSocial.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FCSocial.Properties.Precision = 2
        Me.FCSocial.Size = New System.Drawing.Size(82, 22)
        Me.FCSocial.TabIndex = 5
        Me.FCSocial.Tag = "2|"
        '
        'FCMedical_lbl2
        '
        Me.FCMedical_lbl2.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.FCMedical_lbl2.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FCMedical_lbl2.Location = New System.Drawing.Point(227, 62)
        Me.FCMedical_lbl2.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FCMedical_lbl2.Name = "FCMedical_lbl2"
        Me.FCMedical_lbl2.Size = New System.Drawing.Size(34, 25)
        Me.FCMedical_lbl2.TabIndex = 395
        Me.FCMedical_lbl2.Tag = "2|"
        Me.FCMedical_lbl2.Text = "บาท"
        '
        'FCMedical_lbl
        '
        Me.FCMedical_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FCMedical_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FCMedical_lbl.Location = New System.Drawing.Point(12, 60)
        Me.FCMedical_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FCMedical_lbl.Name = "FCMedical_lbl"
        Me.FCMedical_lbl.Size = New System.Drawing.Size(126, 25)
        Me.FCMedical_lbl.TabIndex = 394
        Me.FCMedical_lbl.Tag = "2|"
        Me.FCMedical_lbl.Text = "ค่ารักษาพยาบาล :"
        '
        'FCMedical
        '
        Me.FCMedical.EnterMoveNextControl = True
        Me.FCMedical.Location = New System.Drawing.Point(141, 62)
        Me.FCMedical.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FCMedical.Name = "FCMedical"
        Me.FCMedical.Properties.Appearance.Options.UseTextOptions = True
        Me.FCMedical.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FCMedical.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FCMedical.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FCMedical.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FCMedical.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FCMedical.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FCMedical.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FCMedical.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FCMedical.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FCMedical.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, True, False, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject3, "", Nothing, Nothing, True)})
        Me.FCMedical.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FCMedical.Properties.Precision = 2
        Me.FCMedical.Size = New System.Drawing.Size(82, 22)
        Me.FCMedical.TabIndex = 4
        Me.FCMedical.Tag = "2|"
        '
        'FNPatients_lbl
        '
        Me.FNPatients_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNPatients_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNPatients_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNPatients_lbl.Location = New System.Drawing.Point(307, 34)
        Me.FNPatients_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNPatients_lbl.Name = "FNPatients_lbl"
        Me.FNPatients_lbl.Size = New System.Drawing.Size(168, 25)
        Me.FNPatients_lbl.TabIndex = 392
        Me.FNPatients_lbl.Tag = "2|"
        Me.FNPatients_lbl.Text = "สถานะผู้ป่วย :"
        '
        'FNPatients
        '
        Me.FNPatients.EditValue = ""
        Me.FNPatients.EnterMoveNextControl = True
        Me.FNPatients.Location = New System.Drawing.Point(478, 34)
        Me.FNPatients.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNPatients.Name = "FNPatients"
        Me.FNPatients.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FNPatients.Properties.Appearance.Options.UseBackColor = True
        Me.FNPatients.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FNPatients.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FNPatients.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FNPatients.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FNPatients.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNPatients.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNPatients.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNPatients.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNPatients.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNPatients.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNPatients.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNPatients.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNPatients.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FNPatients.Properties.Tag = "FNPatients"
        Me.FNPatients.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.FNPatients.Size = New System.Drawing.Size(125, 22)
        Me.FNPatients.TabIndex = 3
        Me.FNPatients.Tag = "2|"
        '
        'FTBillNo_lbl
        '
        Me.FTBillNo_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTBillNo_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTBillNo_lbl.Location = New System.Drawing.Point(6, 34)
        Me.FTBillNo_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTBillNo_lbl.Name = "FTBillNo_lbl"
        Me.FTBillNo_lbl.Size = New System.Drawing.Size(133, 25)
        Me.FTBillNo_lbl.TabIndex = 333
        Me.FTBillNo_lbl.Tag = "2|"
        Me.FTBillNo_lbl.Text = "เลขที่/เลขที่บิล :"
        '
        'FTBillNo
        '
        Me.FTBillNo.Location = New System.Drawing.Point(141, 34)
        Me.FTBillNo.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTBillNo.Name = "FTBillNo"
        Me.FTBillNo.Properties.MaxLength = 100
        Me.FTBillNo.Size = New System.Drawing.Size(139, 22)
        Me.FTBillNo.TabIndex = 2
        Me.FTBillNo.Tag = "2|"
        '
        'FNHSysHospitalId_None
        '
        Me.FNHSysHospitalId_None.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FNHSysHospitalId_None.Location = New System.Drawing.Point(608, 7)
        Me.FNHSysHospitalId_None.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysHospitalId_None.Name = "FNHSysHospitalId_None"
        Me.FNHSysHospitalId_None.Properties.Appearance.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysHospitalId_None.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysHospitalId_None.Properties.ReadOnly = True
        Me.FNHSysHospitalId_None.Size = New System.Drawing.Size(351, 22)
        Me.FNHSysHospitalId_None.TabIndex = 320
        Me.FNHSysHospitalId_None.Tag = "2|"
        '
        'FNHSysHospitalId_lbl
        '
        Me.FNHSysHospitalId_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysHospitalId_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysHospitalId_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysHospitalId_lbl.Location = New System.Drawing.Point(289, 9)
        Me.FNHSysHospitalId_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysHospitalId_lbl.Name = "FNHSysHospitalId_lbl"
        Me.FNHSysHospitalId_lbl.Size = New System.Drawing.Size(187, 18)
        Me.FNHSysHospitalId_lbl.TabIndex = 319
        Me.FNHSysHospitalId_lbl.Tag = "2|"
        Me.FNHSysHospitalId_lbl.Text = "โรงพยาบาล :"
        '
        'FNHSysHospitalId
        '
        Me.FNHSysHospitalId.Location = New System.Drawing.Point(478, 7)
        Me.FNHSysHospitalId.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysHospitalId.Name = "FNHSysHospitalId"
        Me.FNHSysHospitalId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject4, "", "47", Nothing, True)})
        Me.FNHSysHospitalId.Properties.Tag = ""
        Me.FNHSysHospitalId.Size = New System.Drawing.Size(126, 22)
        Me.FNHSysHospitalId.TabIndex = 1
        Me.FNHSysHospitalId.Tag = "2|"
        '
        'FDTreatment
        '
        Me.FDTreatment.EditValue = Nothing
        Me.FDTreatment.EnterMoveNextControl = True
        Me.FDTreatment.Location = New System.Drawing.Point(141, 7)
        Me.FDTreatment.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FDTreatment.Name = "FDTreatment"
        Me.FDTreatment.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.FDTreatment.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FDTreatment.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.FDTreatment.Properties.DisplayFormat.FormatString = "d"
        Me.FDTreatment.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FDTreatment.Properties.EditFormat.FormatString = "d"
        Me.FDTreatment.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FDTreatment.Properties.NullDate = ""
        Me.FDTreatment.Size = New System.Drawing.Size(139, 22)
        Me.FDTreatment.TabIndex = 0
        Me.FDTreatment.Tag = "2|"
        '
        'FDTreatment_lbl
        '
        Me.FDTreatment_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FDTreatment_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FDTreatment_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FDTreatment_lbl.Location = New System.Drawing.Point(6, 7)
        Me.FDTreatment_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FDTreatment_lbl.Name = "FDTreatment_lbl"
        Me.FDTreatment_lbl.Size = New System.Drawing.Size(135, 23)
        Me.FDTreatment_lbl.TabIndex = 282
        Me.FDTreatment_lbl.Tag = "2|"
        Me.FDTreatment_lbl.Text = "วันที่ตรวจ :"
        '
        'wAddEmpHealthCost
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(968, 295)
        Me.ControlBox = False
        Me.Controls.Add(Me.ogbemployee)
        Me.Controls.Add(Me.ogbbutton)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "wAddEmpHealthCost"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "wAddEmpHealthCost"
        CType(Me.ogbbutton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbbutton.ResumeLayout(False)
        CType(Me.ogbemployee, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbemployee.ResumeLayout(False)
        CType(Me.FTNote.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FCDisburse.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FCSocial.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FCMedical.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNPatients.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTBillNo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysHospitalId_None.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysHospitalId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FDTreatment.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FDTreatment.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogbbutton As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmsave As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ogbemployee As DevExpress.XtraEditors.GroupControl
    Friend WithEvents FNHSysHospitalId_None As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FNHSysHospitalId_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysHospitalId As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FDTreatment As DevExpress.XtraEditors.DateEdit
    Friend WithEvents FDTreatment_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTBillNo_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTBillNo As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FNPatients_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNPatients As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents FCDisburse_lbl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FCDisburse_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FCDisburse As DevExpress.XtraEditors.CalcEdit
    Friend WithEvents FCSocial_lbl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FCSocial_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FCSocial As DevExpress.XtraEditors.CalcEdit
    Friend WithEvents FCMedical_lbl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FCMedical_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FCMedical As DevExpress.XtraEditors.CalcEdit
    Friend WithEvents FTNote_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTNote As DevExpress.XtraEditors.MemoEdit
End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wReportReservedTracking
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
        Dim SerializableAppearanceObject5 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject6 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.ogcDocument = New DevExpress.XtraEditors.GroupControl()
        Me.FTReserveNoTo = New DevExpress.XtraEditors.ButtonEdit()
        Me.FTReserveNo = New DevExpress.XtraEditors.ButtonEdit()
        Me.FTReserveNoTo_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTOrderNoTo_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTOrderNoTo = New DevExpress.XtraEditors.ButtonEdit()
        Me.FTReserveNo_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTOrderNo_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTOrderNo = New DevExpress.XtraEditors.ButtonEdit()
        Me.FDDateTo = New DevExpress.XtraEditors.DateEdit()
        Me.FDDate = New DevExpress.XtraEditors.DateEdit()
        Me.FDDateE_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FDDateS_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.IcCondition = New HI.INVEN.ICCondition()
        Me.ogbreportname = New DevExpress.XtraEditors.GroupControl()
        Me.FNEmpSex_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNReportname = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.ogbbutton = New DevExpress.XtraEditors.GroupControl()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmpreview = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.ogcDocument, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogcDocument.SuspendLayout()
        CType(Me.FTReserveNoTo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTReserveNo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTOrderNoTo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTOrderNo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FDDateTo.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FDDateTo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FDDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FDDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.ogbreportname, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbreportname.SuspendLayout()
        CType(Me.FNReportname.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogbbutton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbbutton.SuspendLayout()
        Me.SuspendLayout()
        '
        'ogcDocument
        '
        Me.ogcDocument.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogcDocument.Controls.Add(Me.FTReserveNoTo)
        Me.ogcDocument.Controls.Add(Me.FTReserveNo)
        Me.ogcDocument.Controls.Add(Me.FTReserveNoTo_lbl)
        Me.ogcDocument.Controls.Add(Me.FTOrderNoTo_lbl)
        Me.ogcDocument.Controls.Add(Me.FTOrderNoTo)
        Me.ogcDocument.Controls.Add(Me.FTReserveNo_lbl)
        Me.ogcDocument.Controls.Add(Me.FTOrderNo_lbl)
        Me.ogcDocument.Controls.Add(Me.FTOrderNo)
        Me.ogcDocument.Controls.Add(Me.FDDateTo)
        Me.ogcDocument.Controls.Add(Me.FDDate)
        Me.ogcDocument.Controls.Add(Me.FDDateE_lbl)
        Me.ogcDocument.Controls.Add(Me.FDDateS_lbl)
        Me.ogcDocument.Location = New System.Drawing.Point(0, 2)
        Me.ogcDocument.Name = "ogcDocument"
        Me.ogcDocument.Size = New System.Drawing.Size(608, 107)
        Me.ogcDocument.TabIndex = 0
        Me.ogcDocument.Text = "Document No."
        '
        'FTReserveNoTo
        '
        Me.FTReserveNoTo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FTReserveNoTo.EnterMoveNextControl = True
        Me.FTReserveNoTo.Location = New System.Drawing.Point(376, 24)
        Me.FTReserveNoTo.Name = "FTReserveNoTo"
        Me.FTReserveNoTo.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FTReserveNoTo.Properties.Appearance.Options.UseBackColor = True
        Me.FTReserveNoTo.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FTReserveNoTo.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FTReserveNoTo.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FTReserveNoTo.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FTReserveNoTo.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FTReserveNoTo.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FTReserveNoTo.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FTReserveNoTo.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FTReserveNoTo.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FTReserveNoTo.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FTReserveNoTo.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FTReserveNoTo.Properties.AppearanceReadOnly.Options.UseForeColor = True
        SerializableAppearanceObject1.Options.UseTextOptions = True
        SerializableAppearanceObject1.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        SerializableAppearanceObject2.Options.UseTextOptions = True
        SerializableAppearanceObject2.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTReserveNoTo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "...", 15, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", "206", Nothing, True), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Plus, "New", 20, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.F2), SerializableAppearanceObject2, "", "d", Nothing, True)})
        Me.FTReserveNoTo.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.FTReserveNoTo.Size = New System.Drawing.Size(172, 20)
        Me.FTReserveNoTo.TabIndex = 1
        Me.FTReserveNoTo.TabStop = False
        Me.FTReserveNoTo.Tag = "2|"
        '
        'FTReserveNo
        '
        Me.FTReserveNo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FTReserveNo.EnterMoveNextControl = True
        Me.FTReserveNo.Location = New System.Drawing.Point(141, 25)
        Me.FTReserveNo.Name = "FTReserveNo"
        Me.FTReserveNo.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FTReserveNo.Properties.Appearance.Options.UseBackColor = True
        Me.FTReserveNo.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FTReserveNo.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FTReserveNo.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FTReserveNo.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FTReserveNo.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FTReserveNo.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FTReserveNo.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FTReserveNo.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FTReserveNo.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FTReserveNo.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FTReserveNo.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FTReserveNo.Properties.AppearanceReadOnly.Options.UseForeColor = True
        SerializableAppearanceObject3.Options.UseTextOptions = True
        SerializableAppearanceObject3.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        SerializableAppearanceObject4.Options.UseTextOptions = True
        SerializableAppearanceObject4.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTReserveNo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "...", 15, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject3, "", "207", Nothing, True), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Plus, "New", 20, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.F2), SerializableAppearanceObject4, "", "d", Nothing, True)})
        Me.FTReserveNo.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.FTReserveNo.Size = New System.Drawing.Size(172, 20)
        Me.FTReserveNo.TabIndex = 0
        Me.FTReserveNo.TabStop = False
        Me.FTReserveNo.Tag = "2|"
        '
        'FTReserveNoTo_lbl
        '
        Me.FTReserveNoTo_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTReserveNoTo_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTReserveNoTo_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTReserveNoTo_lbl.Location = New System.Drawing.Point(314, 26)
        Me.FTReserveNoTo_lbl.Name = "FTReserveNoTo_lbl"
        Me.FTReserveNoTo_lbl.Size = New System.Drawing.Size(56, 19)
        Me.FTReserveNoTo_lbl.TabIndex = 291
        Me.FTReserveNoTo_lbl.Tag = "2|"
        Me.FTReserveNoTo_lbl.Text = "To  :"
        '
        'FTOrderNoTo_lbl
        '
        Me.FTOrderNoTo_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTOrderNoTo_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTOrderNoTo_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTOrderNoTo_lbl.Location = New System.Drawing.Point(314, 50)
        Me.FTOrderNoTo_lbl.Name = "FTOrderNoTo_lbl"
        Me.FTOrderNoTo_lbl.Size = New System.Drawing.Size(56, 19)
        Me.FTOrderNoTo_lbl.TabIndex = 291
        Me.FTOrderNoTo_lbl.Tag = "2|"
        Me.FTOrderNoTo_lbl.Text = "To  :"
        '
        'FTOrderNoTo
        '
        Me.FTOrderNoTo.EnterMoveNextControl = True
        Me.FTOrderNoTo.Location = New System.Drawing.Point(376, 51)
        Me.FTOrderNoTo.Name = "FTOrderNoTo"
        Me.FTOrderNoTo.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FTOrderNoTo.Properties.Appearance.Options.UseBackColor = True
        Me.FTOrderNoTo.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FTOrderNoTo.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FTOrderNoTo.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FTOrderNoTo.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FTOrderNoTo.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FTOrderNoTo.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FTOrderNoTo.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FTOrderNoTo.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FTOrderNoTo.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FTOrderNoTo.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FTOrderNoTo.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FTOrderNoTo.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FTOrderNoTo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", 15, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject5, "", "123", Nothing, True)})
        Me.FTOrderNoTo.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.FTOrderNoTo.Properties.MaxLength = 30
        Me.FTOrderNoTo.Size = New System.Drawing.Size(172, 20)
        Me.FTOrderNoTo.TabIndex = 3
        Me.FTOrderNoTo.Tag = "2|"
        '
        'FTReserveNo_lbl
        '
        Me.FTReserveNo_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTReserveNo_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTReserveNo_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTReserveNo_lbl.Location = New System.Drawing.Point(12, 24)
        Me.FTReserveNo_lbl.Name = "FTReserveNo_lbl"
        Me.FTReserveNo_lbl.Size = New System.Drawing.Size(128, 19)
        Me.FTReserveNo_lbl.TabIndex = 289
        Me.FTReserveNo_lbl.Tag = "2|"
        Me.FTReserveNo_lbl.Text = "Reserved No :"
        '
        'FTOrderNo_lbl
        '
        Me.FTOrderNo_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTOrderNo_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTOrderNo_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTOrderNo_lbl.Location = New System.Drawing.Point(13, 50)
        Me.FTOrderNo_lbl.Name = "FTOrderNo_lbl"
        Me.FTOrderNo_lbl.Size = New System.Drawing.Size(128, 19)
        Me.FTOrderNo_lbl.TabIndex = 289
        Me.FTOrderNo_lbl.Tag = "2|"
        Me.FTOrderNo_lbl.Text = "From Order No :"
        '
        'FTOrderNo
        '
        Me.FTOrderNo.EnterMoveNextControl = True
        Me.FTOrderNo.Location = New System.Drawing.Point(141, 51)
        Me.FTOrderNo.Name = "FTOrderNo"
        Me.FTOrderNo.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FTOrderNo.Properties.Appearance.Options.UseBackColor = True
        Me.FTOrderNo.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FTOrderNo.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FTOrderNo.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FTOrderNo.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FTOrderNo.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FTOrderNo.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FTOrderNo.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FTOrderNo.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FTOrderNo.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FTOrderNo.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FTOrderNo.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FTOrderNo.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FTOrderNo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", 15, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject6, "", "121", Nothing, True)})
        Me.FTOrderNo.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.FTOrderNo.Properties.MaxLength = 30
        Me.FTOrderNo.Size = New System.Drawing.Size(172, 20)
        Me.FTOrderNo.TabIndex = 2
        Me.FTOrderNo.Tag = "2|"
        '
        'FDDateTo
        '
        Me.FDDateTo.EditValue = Nothing
        Me.FDDateTo.Location = New System.Drawing.Point(376, 77)
        Me.FDDateTo.Name = "FDDateTo"
        Me.FDDateTo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FDDateTo.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FDDateTo.Size = New System.Drawing.Size(172, 20)
        Me.FDDateTo.TabIndex = 5
        '
        'FDDate
        '
        Me.FDDate.EditValue = Nothing
        Me.FDDate.Location = New System.Drawing.Point(141, 77)
        Me.FDDate.Name = "FDDate"
        Me.FDDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FDDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FDDate.Size = New System.Drawing.Size(172, 20)
        Me.FDDate.TabIndex = 4
        '
        'FDDateE_lbl
        '
        Me.FDDateE_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FDDateE_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FDDateE_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FDDateE_lbl.Location = New System.Drawing.Point(319, 77)
        Me.FDDateE_lbl.Name = "FDDateE_lbl"
        Me.FDDateE_lbl.Size = New System.Drawing.Size(51, 19)
        Me.FDDateE_lbl.TabIndex = 266
        Me.FDDateE_lbl.Tag = "2|"
        Me.FDDateE_lbl.Text = "To :"
        '
        'FDDateS_lbl
        '
        Me.FDDateS_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FDDateS_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FDDateS_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FDDateS_lbl.Location = New System.Drawing.Point(13, 77)
        Me.FDDateS_lbl.Name = "FDDateS_lbl"
        Me.FDDateS_lbl.Size = New System.Drawing.Size(128, 19)
        Me.FDDateS_lbl.TabIndex = 266
        Me.FDDateS_lbl.Tag = "2|"
        Me.FDDateS_lbl.Text = "Start Date :"
        '
        'GroupControl1
        '
        Me.GroupControl1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupControl1.Controls.Add(Me.IcCondition)
        Me.GroupControl1.Location = New System.Drawing.Point(1, 115)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(608, 262)
        Me.GroupControl1.TabIndex = 1
        Me.GroupControl1.Text = "Condition"
        '
        'IcCondition
        '
        Me.IcCondition.Location = New System.Drawing.Point(49, 24)
        Me.IcCondition.Name = "IcCondition"
        Me.IcCondition.ShowmColorCode = False
        Me.IcCondition.ShowmItemCode = True
        Me.IcCondition.ShowmSizeCode = False
        Me.IcCondition.ShowmSupl = False
        Me.IcCondition.ShowmUser = False
        Me.IcCondition.ShowWHNo = True
        Me.IcCondition.Size = New System.Drawing.Size(520, 220)
        Me.IcCondition.TabIndex = 0
        '
        'ogbreportname
        '
        Me.ogbreportname.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbreportname.Controls.Add(Me.FNEmpSex_lbl)
        Me.ogbreportname.Controls.Add(Me.FNReportname)
        Me.ogbreportname.Location = New System.Drawing.Point(1, 383)
        Me.ogbreportname.Name = "ogbreportname"
        Me.ogbreportname.ShowCaption = False
        Me.ogbreportname.Size = New System.Drawing.Size(608, 42)
        Me.ogbreportname.TabIndex = 3
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
        Me.FNReportname.TabIndex = 6
        Me.FNReportname.Tag = "2|"
        '
        'ogbbutton
        '
        Me.ogbbutton.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbbutton.Controls.Add(Me.ocmexit)
        Me.ogbbutton.Controls.Add(Me.ocmpreview)
        Me.ogbbutton.Location = New System.Drawing.Point(1, 428)
        Me.ogbbutton.Name = "ogbbutton"
        Me.ogbbutton.ShowCaption = False
        Me.ogbbutton.Size = New System.Drawing.Size(607, 42)
        Me.ogbbutton.TabIndex = 5
        Me.ogbbutton.Text = "GroupControl1"
        '
        'ocmexit
        '
        Me.ocmexit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmexit.Location = New System.Drawing.Point(388, 9)
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
        'wReportReservedTracking
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(609, 473)
        Me.Controls.Add(Me.ogbreportname)
        Me.Controls.Add(Me.ogbbutton)
        Me.Controls.Add(Me.GroupControl1)
        Me.Controls.Add(Me.ogcDocument)
        Me.Name = "wReportReservedTracking"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "wReportReservedTracking"
        CType(Me.ogcDocument, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogcDocument.ResumeLayout(False)
        CType(Me.FTReserveNoTo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTReserveNo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTOrderNoTo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTOrderNo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FDDateTo.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FDDateTo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FDDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FDDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        CType(Me.ogbreportname, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbreportname.ResumeLayout(False)
        CType(Me.FNReportname.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogbbutton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbbutton.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogcDocument As DevExpress.XtraEditors.GroupControl
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents IcCondition As HI.INVEN.ICCondition
    Friend WithEvents ogbreportname As DevExpress.XtraEditors.GroupControl
    Friend WithEvents FNEmpSex_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNReportname As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents ogbbutton As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmpreview As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FDDateTo As DevExpress.XtraEditors.DateEdit
    Friend WithEvents FDDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents FDDateE_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FDDateS_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTOrderNoTo_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTOrderNoTo As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FTOrderNo_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTOrderNo As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FTReserveNoTo As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FTReserveNo As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FTReserveNoTo_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTReserveNo_lbl As DevExpress.XtraEditors.LabelControl
End Class

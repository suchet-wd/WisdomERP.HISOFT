<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wReportPOTracking
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
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.ogcDocument = New DevExpress.XtraEditors.GroupControl()
        Me.FDDeliveryDateTo = New DevExpress.XtraEditors.DateEdit()
        Me.FDDeliveryDate = New DevExpress.XtraEditors.DateEdit()
        Me.FDPODateTo = New DevExpress.XtraEditors.DateEdit()
        Me.FDPODate = New DevExpress.XtraEditors.DateEdit()
        Me.FTPurchaseNoTo = New DevExpress.XtraEditors.ButtonEdit()
        Me.FTPurchaseNo = New DevExpress.XtraEditors.ButtonEdit()
        Me.FTPurchaseNoTo_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FDDeliveryDateTo_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FDPODateE_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FDDeliveryDate_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FDPODateS_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTPurchaseNo_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.ogbreportname = New DevExpress.XtraEditors.GroupControl()
        Me.FNEmpSex_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNReportname = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.ogbbutton = New DevExpress.XtraEditors.GroupControl()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmpreview = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.ogcDocument, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogcDocument.SuspendLayout()
        CType(Me.FDDeliveryDateTo.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FDDeliveryDateTo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FDDeliveryDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FDDeliveryDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FDPODateTo.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FDPODateTo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FDPODate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FDPODate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTPurchaseNoTo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTPurchaseNo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.ogcDocument.Controls.Add(Me.FDDeliveryDateTo)
        Me.ogcDocument.Controls.Add(Me.FDDeliveryDate)
        Me.ogcDocument.Controls.Add(Me.FDPODateTo)
        Me.ogcDocument.Controls.Add(Me.FDPODate)
        Me.ogcDocument.Controls.Add(Me.FTPurchaseNoTo)
        Me.ogcDocument.Controls.Add(Me.FTPurchaseNo)
        Me.ogcDocument.Controls.Add(Me.FTPurchaseNoTo_lbl)
        Me.ogcDocument.Controls.Add(Me.FDDeliveryDateTo_lbl)
        Me.ogcDocument.Controls.Add(Me.FDPODateE_lbl)
        Me.ogcDocument.Controls.Add(Me.FDDeliveryDate_lbl)
        Me.ogcDocument.Controls.Add(Me.FDPODateS_lbl)
        Me.ogcDocument.Controls.Add(Me.FTPurchaseNo_lbl)
        Me.ogcDocument.Location = New System.Drawing.Point(0, 2)
        Me.ogcDocument.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcDocument.Name = "ogcDocument"
        Me.ogcDocument.Size = New System.Drawing.Size(709, 130)
        Me.ogcDocument.TabIndex = 0
        Me.ogcDocument.Text = "Document No."
        '
        'FDDeliveryDateTo
        '
        Me.FDDeliveryDateTo.EditValue = Nothing
        Me.FDDeliveryDateTo.Location = New System.Drawing.Point(434, 100)
        Me.FDDeliveryDateTo.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FDDeliveryDateTo.Name = "FDDeliveryDateTo"
        Me.FDDeliveryDateTo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FDDeliveryDateTo.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FDDeliveryDateTo.Size = New System.Drawing.Size(201, 22)
        Me.FDDeliveryDateTo.TabIndex = 3
        '
        'FDDeliveryDate
        '
        Me.FDDeliveryDate.EditValue = Nothing
        Me.FDDeliveryDate.Location = New System.Drawing.Point(160, 100)
        Me.FDDeliveryDate.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FDDeliveryDate.Name = "FDDeliveryDate"
        Me.FDDeliveryDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FDDeliveryDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FDDeliveryDate.Size = New System.Drawing.Size(201, 22)
        Me.FDDeliveryDate.TabIndex = 2
        '
        'FDPODateTo
        '
        Me.FDPODateTo.EditValue = Nothing
        Me.FDPODateTo.Location = New System.Drawing.Point(434, 68)
        Me.FDPODateTo.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FDPODateTo.Name = "FDPODateTo"
        Me.FDPODateTo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FDPODateTo.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FDPODateTo.Size = New System.Drawing.Size(201, 22)
        Me.FDPODateTo.TabIndex = 3
        '
        'FDPODate
        '
        Me.FDPODate.EditValue = Nothing
        Me.FDPODate.Location = New System.Drawing.Point(160, 68)
        Me.FDPODate.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FDPODate.Name = "FDPODate"
        Me.FDPODate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FDPODate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FDPODate.Size = New System.Drawing.Size(201, 22)
        Me.FDPODate.TabIndex = 2
        '
        'FTPurchaseNoTo
        '
        Me.FTPurchaseNoTo.EnterMoveNextControl = True
        Me.FTPurchaseNoTo.Location = New System.Drawing.Point(434, 38)
        Me.FTPurchaseNoTo.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTPurchaseNoTo.Name = "FTPurchaseNoTo"
        Me.FTPurchaseNoTo.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FTPurchaseNoTo.Properties.Appearance.Options.UseBackColor = True
        Me.FTPurchaseNoTo.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FTPurchaseNoTo.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FTPurchaseNoTo.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FTPurchaseNoTo.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FTPurchaseNoTo.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FTPurchaseNoTo.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FTPurchaseNoTo.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FTPurchaseNoTo.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FTPurchaseNoTo.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FTPurchaseNoTo.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FTPurchaseNoTo.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FTPurchaseNoTo.Properties.AppearanceReadOnly.Options.UseForeColor = True
        SerializableAppearanceObject2.Options.UseTextOptions = True
        SerializableAppearanceObject2.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTPurchaseNoTo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "...", 15, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject2, "", "198", Nothing, True)})
        Me.FTPurchaseNoTo.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.FTPurchaseNoTo.Size = New System.Drawing.Size(201, 22)
        Me.FTPurchaseNoTo.TabIndex = 1
        Me.FTPurchaseNoTo.TabStop = False
        Me.FTPurchaseNoTo.Tag = "2|"
        '
        'FTPurchaseNo
        '
        Me.FTPurchaseNo.EnterMoveNextControl = True
        Me.FTPurchaseNo.Location = New System.Drawing.Point(160, 38)
        Me.FTPurchaseNo.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTPurchaseNo.Name = "FTPurchaseNo"
        Me.FTPurchaseNo.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FTPurchaseNo.Properties.Appearance.Options.UseBackColor = True
        Me.FTPurchaseNo.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FTPurchaseNo.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FTPurchaseNo.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FTPurchaseNo.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FTPurchaseNo.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FTPurchaseNo.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FTPurchaseNo.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FTPurchaseNo.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FTPurchaseNo.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FTPurchaseNo.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FTPurchaseNo.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FTPurchaseNo.Properties.AppearanceReadOnly.Options.UseForeColor = True
        SerializableAppearanceObject3.Options.UseTextOptions = True
        SerializableAppearanceObject3.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTPurchaseNo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "...", 15, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject3, "", "204", Nothing, True)})
        Me.FTPurchaseNo.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.FTPurchaseNo.Size = New System.Drawing.Size(201, 22)
        Me.FTPurchaseNo.TabIndex = 0
        Me.FTPurchaseNo.TabStop = False
        Me.FTPurchaseNo.Tag = "2|"
        '
        'FTPurchaseNoTo_lbl
        '
        Me.FTPurchaseNoTo_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTPurchaseNoTo_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTPurchaseNoTo_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTPurchaseNoTo_lbl.Location = New System.Drawing.Point(367, 38)
        Me.FTPurchaseNoTo_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTPurchaseNoTo_lbl.Name = "FTPurchaseNoTo_lbl"
        Me.FTPurchaseNoTo_lbl.Size = New System.Drawing.Size(59, 23)
        Me.FTPurchaseNoTo_lbl.TabIndex = 266
        Me.FTPurchaseNoTo_lbl.Tag = "2|"
        Me.FTPurchaseNoTo_lbl.Text = "To :"
        '
        'FDDeliveryDateTo_lbl
        '
        Me.FDDeliveryDateTo_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FDDeliveryDateTo_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FDDeliveryDateTo_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FDDeliveryDateTo_lbl.Location = New System.Drawing.Point(367, 98)
        Me.FDDeliveryDateTo_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FDDeliveryDateTo_lbl.Name = "FDDeliveryDateTo_lbl"
        Me.FDDeliveryDateTo_lbl.Size = New System.Drawing.Size(59, 23)
        Me.FDDeliveryDateTo_lbl.TabIndex = 266
        Me.FDDeliveryDateTo_lbl.Tag = "2|"
        Me.FDDeliveryDateTo_lbl.Text = "To :"
        '
        'FDPODateE_lbl
        '
        Me.FDPODateE_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FDPODateE_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FDPODateE_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FDPODateE_lbl.Location = New System.Drawing.Point(367, 68)
        Me.FDPODateE_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FDPODateE_lbl.Name = "FDPODateE_lbl"
        Me.FDPODateE_lbl.Size = New System.Drawing.Size(59, 23)
        Me.FDPODateE_lbl.TabIndex = 266
        Me.FDPODateE_lbl.Tag = "2|"
        Me.FDPODateE_lbl.Text = "To :"
        '
        'FDDeliveryDate_lbl
        '
        Me.FDDeliveryDate_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FDDeliveryDate_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FDDeliveryDate_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FDDeliveryDate_lbl.Location = New System.Drawing.Point(10, 98)
        Me.FDDeliveryDate_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FDDeliveryDate_lbl.Name = "FDDeliveryDate_lbl"
        Me.FDDeliveryDate_lbl.Size = New System.Drawing.Size(149, 23)
        Me.FDDeliveryDate_lbl.TabIndex = 266
        Me.FDDeliveryDate_lbl.Tag = "2|"
        Me.FDDeliveryDate_lbl.Text = "Delivery Date :"
        '
        'FDPODateS_lbl
        '
        Me.FDPODateS_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FDPODateS_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FDPODateS_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FDPODateS_lbl.Location = New System.Drawing.Point(10, 68)
        Me.FDPODateS_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FDPODateS_lbl.Name = "FDPODateS_lbl"
        Me.FDPODateS_lbl.Size = New System.Drawing.Size(149, 23)
        Me.FDPODateS_lbl.TabIndex = 266
        Me.FDPODateS_lbl.Tag = "2|"
        Me.FDPODateS_lbl.Text = "Date PO :"
        '
        'FTPurchaseNo_lbl
        '
        Me.FTPurchaseNo_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTPurchaseNo_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTPurchaseNo_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTPurchaseNo_lbl.Location = New System.Drawing.Point(10, 37)
        Me.FTPurchaseNo_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTPurchaseNo_lbl.Name = "FTPurchaseNo_lbl"
        Me.FTPurchaseNo_lbl.Size = New System.Drawing.Size(149, 23)
        Me.FTPurchaseNo_lbl.TabIndex = 266
        Me.FTPurchaseNo_lbl.Tag = "2|"
        Me.FTPurchaseNo_lbl.Text = "Purchase Order No. :"
        '
        'GroupControl1
        '
        Me.GroupControl1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupControl1.Controls.Add(Me.PoCondition1)
        Me.GroupControl1.Location = New System.Drawing.Point(0, 140)
        Me.GroupControl1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(709, 322)
        Me.GroupControl1.TabIndex = 1
        Me.GroupControl1.Text = "Condition"
        '
        'ogbreportname
        '
        Me.ogbreportname.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbreportname.Controls.Add(Me.FNEmpSex_lbl)
        Me.ogbreportname.Controls.Add(Me.FNReportname)
        Me.ogbreportname.Location = New System.Drawing.Point(0, 468)
        Me.ogbreportname.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbreportname.Name = "ogbreportname"
        Me.ogbreportname.ShowCaption = False
        Me.ogbreportname.Size = New System.Drawing.Size(709, 52)
        Me.ogbreportname.TabIndex = 3
        Me.ogbreportname.Text = "GroupControl1"
        '
        'FNEmpSex_lbl
        '
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
        Me.FNReportname.TabIndex = 6
        Me.FNReportname.Tag = "2|"
        '
        'ogbbutton
        '
        Me.ogbbutton.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbbutton.Controls.Add(Me.ocmexit)
        Me.ogbbutton.Controls.Add(Me.ocmpreview)
        Me.ogbbutton.Location = New System.Drawing.Point(1, 528)
        Me.ogbbutton.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbbutton.Name = "ogbbutton"
        Me.ogbbutton.ShowCaption = False
        Me.ogbbutton.Size = New System.Drawing.Size(708, 52)
        Me.ogbbutton.TabIndex = 5
        Me.ogbbutton.Text = "GroupControl1"
        '
        'ocmexit
        '
        Me.ocmexit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmexit.Location = New System.Drawing.Point(453, 11)
        Me.ocmexit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmexit.Name = "ocmexit"
        Me.ocmexit.Size = New System.Drawing.Size(170, 31)
        Me.ocmexit.TabIndex = 1
        Me.ocmexit.Text = "EXIT"
        '
        'ocmpreview
        '
        Me.ocmpreview.Location = New System.Drawing.Point(73, 11)
        Me.ocmpreview.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmpreview.Name = "ocmpreview"
        Me.ocmpreview.Size = New System.Drawing.Size(170, 31)
        Me.ocmpreview.TabIndex = 0
        Me.ocmpreview.Text = "PREVIEW"
        '
        'wReportPOTracking
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(710, 583)
        Me.Controls.Add(Me.ogbreportname)
        Me.Controls.Add(Me.ogbbutton)
        Me.Controls.Add(Me.GroupControl1)
        Me.Controls.Add(Me.ogcDocument)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "wReportPOTracking"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "wReportPOTracking"
        CType(Me.ogcDocument, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogcDocument.ResumeLayout(False)
        CType(Me.FDDeliveryDateTo.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FDDeliveryDateTo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FDDeliveryDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FDDeliveryDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FDPODateTo.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FDPODateTo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FDPODate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FDPODate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTPurchaseNoTo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTPurchaseNo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents ogbreportname As DevExpress.XtraEditors.GroupControl
    Friend WithEvents FNEmpSex_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNReportname As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents ogbbutton As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmpreview As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FDPODateTo As DevExpress.XtraEditors.DateEdit
    Friend WithEvents FDPODate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents FTPurchaseNoTo As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FTPurchaseNo As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FTPurchaseNoTo_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FDPODateE_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FDPODateS_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTPurchaseNo_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents PoCondition1 As HI.PO.POCondition
    Friend WithEvents FDDeliveryDateTo As DevExpress.XtraEditors.DateEdit
    Friend WithEvents FDDeliveryDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents FDDeliveryDateTo_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FDDeliveryDate_lbl As DevExpress.XtraEditors.LabelControl
End Class

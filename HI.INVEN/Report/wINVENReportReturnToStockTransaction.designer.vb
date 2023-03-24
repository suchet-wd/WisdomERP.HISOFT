<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wINVENReportReturnToStockTransaction
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
        Me.ogcDocument = New DevExpress.XtraEditors.GroupControl()
        Me.FDEReturnStockDateTo = New DevExpress.XtraEditors.DateEdit()
        Me.FDSReturnStockDate = New DevExpress.XtraEditors.DateEdit()
        Me.FTReceiveNoTo_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FDRcvDateE_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FDSReturnStockDateS_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTReturnStockNoTo = New DevExpress.XtraEditors.ButtonEdit()
        Me.FTReturnStockNo = New DevExpress.XtraEditors.ButtonEdit()
        Me.FTReturnStockNo_lbl = New DevExpress.XtraEditors.LabelControl()
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
        CType(Me.FDEReturnStockDateTo.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FDEReturnStockDateTo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FDSReturnStockDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FDSReturnStockDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTReturnStockNoTo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTReturnStockNo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.ogcDocument.Controls.Add(Me.FDEReturnStockDateTo)
        Me.ogcDocument.Controls.Add(Me.FDSReturnStockDate)
        Me.ogcDocument.Controls.Add(Me.FTReceiveNoTo_lbl)
        Me.ogcDocument.Controls.Add(Me.FDRcvDateE_lbl)
        Me.ogcDocument.Controls.Add(Me.FDSReturnStockDateS_lbl)
        Me.ogcDocument.Controls.Add(Me.FTReturnStockNoTo)
        Me.ogcDocument.Controls.Add(Me.FTReturnStockNo)
        Me.ogcDocument.Controls.Add(Me.FTReturnStockNo_lbl)
        Me.ogcDocument.Location = New System.Drawing.Point(0, 2)
        Me.ogcDocument.Name = "ogcDocument"
        Me.ogcDocument.Size = New System.Drawing.Size(608, 106)
        Me.ogcDocument.TabIndex = 0
        Me.ogcDocument.Text = "Document No."
        '
        'FDEReturnStockDateTo
        '
        Me.FDEReturnStockDateTo.EditValue = Nothing
        Me.FDEReturnStockDateTo.Location = New System.Drawing.Point(372, 50)
        Me.FDEReturnStockDateTo.Name = "FDEReturnStockDateTo"
        Me.FDEReturnStockDateTo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FDEReturnStockDateTo.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FDEReturnStockDateTo.Size = New System.Drawing.Size(172, 20)
        Me.FDEReturnStockDateTo.TabIndex = 5
        '
        'FDSReturnStockDate
        '
        Me.FDSReturnStockDate.EditValue = Nothing
        Me.FDSReturnStockDate.Location = New System.Drawing.Point(137, 50)
        Me.FDSReturnStockDate.Name = "FDSReturnStockDate"
        Me.FDSReturnStockDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FDSReturnStockDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FDSReturnStockDate.Size = New System.Drawing.Size(172, 20)
        Me.FDSReturnStockDate.TabIndex = 4
        '
        'FTReceiveNoTo_lbl
        '
        Me.FTReceiveNoTo_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTReceiveNoTo_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTReceiveNoTo_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTReceiveNoTo_lbl.Location = New System.Drawing.Point(315, 27)
        Me.FTReceiveNoTo_lbl.Name = "FTReceiveNoTo_lbl"
        Me.FTReceiveNoTo_lbl.Size = New System.Drawing.Size(51, 19)
        Me.FTReceiveNoTo_lbl.TabIndex = 266
        Me.FTReceiveNoTo_lbl.Tag = "2|"
        Me.FTReceiveNoTo_lbl.Text = "To :"
        '
        'FDRcvDateE_lbl
        '
        Me.FDRcvDateE_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FDRcvDateE_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FDRcvDateE_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FDRcvDateE_lbl.Location = New System.Drawing.Point(315, 50)
        Me.FDRcvDateE_lbl.Name = "FDRcvDateE_lbl"
        Me.FDRcvDateE_lbl.Size = New System.Drawing.Size(51, 19)
        Me.FDRcvDateE_lbl.TabIndex = 266
        Me.FDRcvDateE_lbl.Tag = "2|"
        Me.FDRcvDateE_lbl.Text = "To :"
        '
        'FDSReturnStockDateS_lbl
        '
        Me.FDSReturnStockDateS_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FDSReturnStockDateS_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FDSReturnStockDateS_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FDSReturnStockDateS_lbl.Location = New System.Drawing.Point(9, 50)
        Me.FDSReturnStockDateS_lbl.Name = "FDSReturnStockDateS_lbl"
        Me.FDSReturnStockDateS_lbl.Size = New System.Drawing.Size(128, 19)
        Me.FDSReturnStockDateS_lbl.TabIndex = 266
        Me.FDSReturnStockDateS_lbl.Tag = "2|"
        Me.FDSReturnStockDateS_lbl.Text = "Date Return Stock :"
        '
        'FTReturnStockNoTo
        '
        Me.FTReturnStockNoTo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FTReturnStockNoTo.EnterMoveNextControl = True
        Me.FTReturnStockNoTo.Location = New System.Drawing.Point(372, 24)
        Me.FTReturnStockNoTo.Name = "FTReturnStockNoTo"
        Me.FTReturnStockNoTo.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FTReturnStockNoTo.Properties.Appearance.Options.UseBackColor = True
        Me.FTReturnStockNoTo.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FTReturnStockNoTo.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FTReturnStockNoTo.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FTReturnStockNoTo.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FTReturnStockNoTo.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FTReturnStockNoTo.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FTReturnStockNoTo.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FTReturnStockNoTo.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FTReturnStockNoTo.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FTReturnStockNoTo.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FTReturnStockNoTo.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FTReturnStockNoTo.Properties.AppearanceReadOnly.Options.UseForeColor = True
        SerializableAppearanceObject1.Options.UseTextOptions = True
        SerializableAppearanceObject1.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        SerializableAppearanceObject2.Options.UseTextOptions = True
        SerializableAppearanceObject2.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTReturnStockNoTo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "...", 15, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", "228", Nothing, True), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Plus, "New", 20, True, False, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.F2), SerializableAppearanceObject2, "", "d", Nothing, True)})
        Me.FTReturnStockNoTo.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.FTReturnStockNoTo.Size = New System.Drawing.Size(172, 20)
        Me.FTReturnStockNoTo.TabIndex = 1
        Me.FTReturnStockNoTo.TabStop = False
        Me.FTReturnStockNoTo.Tag = "2|"
        '
        'FTReturnStockNo
        '
        Me.FTReturnStockNo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FTReturnStockNo.EnterMoveNextControl = True
        Me.FTReturnStockNo.Location = New System.Drawing.Point(137, 24)
        Me.FTReturnStockNo.Name = "FTReturnStockNo"
        Me.FTReturnStockNo.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FTReturnStockNo.Properties.Appearance.Options.UseBackColor = True
        Me.FTReturnStockNo.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FTReturnStockNo.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FTReturnStockNo.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FTReturnStockNo.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FTReturnStockNo.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FTReturnStockNo.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FTReturnStockNo.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FTReturnStockNo.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FTReturnStockNo.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FTReturnStockNo.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FTReturnStockNo.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FTReturnStockNo.Properties.AppearanceReadOnly.Options.UseForeColor = True
        SerializableAppearanceObject3.Options.UseTextOptions = True
        SerializableAppearanceObject3.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        SerializableAppearanceObject4.Options.UseTextOptions = True
        SerializableAppearanceObject4.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTReturnStockNo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "...", 15, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject3, "", "127", Nothing, True), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Plus, "New", 20, True, False, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.F2), SerializableAppearanceObject4, "", "d", Nothing, True)})
        Me.FTReturnStockNo.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.FTReturnStockNo.Size = New System.Drawing.Size(172, 20)
        Me.FTReturnStockNo.TabIndex = 0
        Me.FTReturnStockNo.TabStop = False
        Me.FTReturnStockNo.Tag = "2|"
        '
        'FTReturnStockNo_lbl
        '
        Me.FTReturnStockNo_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTReturnStockNo_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTReturnStockNo_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTReturnStockNo_lbl.Location = New System.Drawing.Point(9, 23)
        Me.FTReturnStockNo_lbl.Name = "FTReturnStockNo_lbl"
        Me.FTReturnStockNo_lbl.Size = New System.Drawing.Size(128, 19)
        Me.FTReturnStockNo_lbl.TabIndex = 3
        Me.FTReturnStockNo_lbl.Tag = "2|"
        Me.FTReturnStockNo_lbl.Text = "ReturnStock No. :"
        '
        'GroupControl1
        '
        Me.GroupControl1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupControl1.Controls.Add(Me.IcCondition)
        Me.GroupControl1.Location = New System.Drawing.Point(0, 114)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(608, 262)
        Me.GroupControl1.TabIndex = 1
        Me.GroupControl1.Text = "Condition"
        '
        'IcCondition
        '
        Me.IcCondition.Location = New System.Drawing.Point(49, 24)
        Me.IcCondition.Name = "IcCondition"
        Me.IcCondition.ShowmColorCode = True
        Me.IcCondition.ShowmItemCode = True
        Me.IcCondition.ShowmSizeCode = True
        Me.IcCondition.ShowmSupl = True
        Me.IcCondition.ShowmUser = True
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
        Me.ogbreportname.Location = New System.Drawing.Point(0, 380)
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
        Me.ogbbutton.Location = New System.Drawing.Point(1, 429)
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
        'wINVENReportReturnToStockTransaction
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(609, 474)
        Me.Controls.Add(Me.ogbreportname)
        Me.Controls.Add(Me.ogbbutton)
        Me.Controls.Add(Me.GroupControl1)
        Me.Controls.Add(Me.ogcDocument)
        Me.Name = "wINVENReportReturnToStockTransaction"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "wINVENReportReturnToStockTransaction"
        CType(Me.ogcDocument, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogcDocument.ResumeLayout(False)
        CType(Me.FDEReturnStockDateTo.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FDEReturnStockDateTo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FDSReturnStockDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FDSReturnStockDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTReturnStockNoTo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTReturnStockNo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents FTReceiveNoTo_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTReturnStockNoTo As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FTReturnStockNo As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FTReturnStockNo_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FDEReturnStockDateTo As DevExpress.XtraEditors.DateEdit
    Friend WithEvents FDSReturnStockDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents FDRcvDateE_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FDSReturnStockDateS_lbl As DevExpress.XtraEditors.LabelControl
End Class

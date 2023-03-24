<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wINVENReportAdjustedTransaction
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
        Dim SerializableAppearanceObject5 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject6 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.ogcDocument = New DevExpress.XtraEditors.GroupControl()
        Me.FDEAdjustStockDateTo = New DevExpress.XtraEditors.DateEdit()
        Me.FDSAdjustStockDate = New DevExpress.XtraEditors.DateEdit()
        Me.FTReceiveNoTo_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FDRcvDateE_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FDRcvDateS_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTAdjustStockNoTo = New DevExpress.XtraEditors.ButtonEdit()
        Me.FTAdjustStockNo = New DevExpress.XtraEditors.ButtonEdit()
        Me.FTReceiveNo_lbl = New DevExpress.XtraEditors.LabelControl()
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
        CType(Me.FDEAdjustStockDateTo.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FDEAdjustStockDateTo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FDSAdjustStockDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FDSAdjustStockDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTAdjustStockNoTo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTAdjustStockNo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.ogcDocument.Controls.Add(Me.FDEAdjustStockDateTo)
        Me.ogcDocument.Controls.Add(Me.FDSAdjustStockDate)
        Me.ogcDocument.Controls.Add(Me.FTReceiveNoTo_lbl)
        Me.ogcDocument.Controls.Add(Me.FDRcvDateE_lbl)
        Me.ogcDocument.Controls.Add(Me.FDRcvDateS_lbl)
        Me.ogcDocument.Controls.Add(Me.FTAdjustStockNoTo)
        Me.ogcDocument.Controls.Add(Me.FTAdjustStockNo)
        Me.ogcDocument.Controls.Add(Me.FTReceiveNo_lbl)
        Me.ogcDocument.Location = New System.Drawing.Point(0, 2)
        Me.ogcDocument.Name = "ogcDocument"
        Me.ogcDocument.Size = New System.Drawing.Size(608, 106)
        Me.ogcDocument.TabIndex = 0
        Me.ogcDocument.Text = "Document No."
        '
        'FDEAdjustStockDateTo
        '
        Me.FDEAdjustStockDateTo.EditValue = Nothing
        Me.FDEAdjustStockDateTo.Location = New System.Drawing.Point(372, 47)
        Me.FDEAdjustStockDateTo.Name = "FDEAdjustStockDateTo"
        Me.FDEAdjustStockDateTo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FDEAdjustStockDateTo.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FDEAdjustStockDateTo.Size = New System.Drawing.Size(172, 20)
        Me.FDEAdjustStockDateTo.TabIndex = 5
        '
        'FDSAdjustStockDate
        '
        Me.FDSAdjustStockDate.EditValue = Nothing
        Me.FDSAdjustStockDate.Location = New System.Drawing.Point(137, 47)
        Me.FDSAdjustStockDate.Name = "FDSAdjustStockDate"
        Me.FDSAdjustStockDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FDSAdjustStockDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FDSAdjustStockDate.Size = New System.Drawing.Size(172, 20)
        Me.FDSAdjustStockDate.TabIndex = 4
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
        Me.FDRcvDateE_lbl.Location = New System.Drawing.Point(315, 47)
        Me.FDRcvDateE_lbl.Name = "FDRcvDateE_lbl"
        Me.FDRcvDateE_lbl.Size = New System.Drawing.Size(51, 19)
        Me.FDRcvDateE_lbl.TabIndex = 266
        Me.FDRcvDateE_lbl.Tag = "2|"
        Me.FDRcvDateE_lbl.Text = "To :"
        '
        'FDRcvDateS_lbl
        '
        Me.FDRcvDateS_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FDRcvDateS_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FDRcvDateS_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FDRcvDateS_lbl.Location = New System.Drawing.Point(9, 47)
        Me.FDRcvDateS_lbl.Name = "FDRcvDateS_lbl"
        Me.FDRcvDateS_lbl.Size = New System.Drawing.Size(128, 19)
        Me.FDRcvDateS_lbl.TabIndex = 266
        Me.FDRcvDateS_lbl.Tag = "2|"
        Me.FDRcvDateS_lbl.Text = "Date AdjStock :"
        '
        'FTAdjustStockNoTo
        '
        Me.FTAdjustStockNoTo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FTAdjustStockNoTo.EnterMoveNextControl = True
        Me.FTAdjustStockNoTo.Location = New System.Drawing.Point(372, 24)
        Me.FTAdjustStockNoTo.Name = "FTAdjustStockNoTo"
        Me.FTAdjustStockNoTo.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FTAdjustStockNoTo.Properties.Appearance.Options.UseBackColor = True
        Me.FTAdjustStockNoTo.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FTAdjustStockNoTo.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FTAdjustStockNoTo.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FTAdjustStockNoTo.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FTAdjustStockNoTo.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FTAdjustStockNoTo.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FTAdjustStockNoTo.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FTAdjustStockNoTo.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FTAdjustStockNoTo.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FTAdjustStockNoTo.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FTAdjustStockNoTo.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FTAdjustStockNoTo.Properties.AppearanceReadOnly.Options.UseForeColor = True
        SerializableAppearanceObject5.Options.UseTextOptions = True
        SerializableAppearanceObject5.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        SerializableAppearanceObject6.Options.UseTextOptions = True
        SerializableAppearanceObject6.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTAdjustStockNoTo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "...", 15, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject5, "", "208", Nothing, True), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Plus, "New", 20, True, False, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.F2), SerializableAppearanceObject6, "", "d", Nothing, True)})
        Me.FTAdjustStockNoTo.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.FTAdjustStockNoTo.Size = New System.Drawing.Size(172, 20)
        Me.FTAdjustStockNoTo.TabIndex = 1
        Me.FTAdjustStockNoTo.TabStop = False
        Me.FTAdjustStockNoTo.Tag = "2|"
        '
        'FTAdjustStockNo
        '
        Me.FTAdjustStockNo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FTAdjustStockNo.EnterMoveNextControl = True
        Me.FTAdjustStockNo.Location = New System.Drawing.Point(137, 24)
        Me.FTAdjustStockNo.Name = "FTAdjustStockNo"
        Me.FTAdjustStockNo.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FTAdjustStockNo.Properties.Appearance.Options.UseBackColor = True
        Me.FTAdjustStockNo.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FTAdjustStockNo.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FTAdjustStockNo.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FTAdjustStockNo.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FTAdjustStockNo.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FTAdjustStockNo.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FTAdjustStockNo.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FTAdjustStockNo.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FTAdjustStockNo.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FTAdjustStockNo.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FTAdjustStockNo.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FTAdjustStockNo.Properties.AppearanceReadOnly.Options.UseForeColor = True
        SerializableAppearanceObject1.Options.UseTextOptions = True
        SerializableAppearanceObject1.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        SerializableAppearanceObject2.Options.UseTextOptions = True
        SerializableAppearanceObject2.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTAdjustStockNo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "...", 15, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", "129", Nothing, True), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Plus, "New", 20, True, False, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.F2), SerializableAppearanceObject2, "", "d", Nothing, True)})
        Me.FTAdjustStockNo.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.FTAdjustStockNo.Size = New System.Drawing.Size(172, 20)
        Me.FTAdjustStockNo.TabIndex = 0
        Me.FTAdjustStockNo.TabStop = False
        Me.FTAdjustStockNo.Tag = "2|"
        '
        'FTReceiveNo_lbl
        '
        Me.FTReceiveNo_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTReceiveNo_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTReceiveNo_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTReceiveNo_lbl.Location = New System.Drawing.Point(9, 23)
        Me.FTReceiveNo_lbl.Name = "FTReceiveNo_lbl"
        Me.FTReceiveNo_lbl.Size = New System.Drawing.Size(128, 19)
        Me.FTReceiveNo_lbl.TabIndex = 3
        Me.FTReceiveNo_lbl.Tag = "2|"
        Me.FTReceiveNo_lbl.Text = "Adjust Stock No. :"
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
        'wAdjustedTransaction
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(609, 474)
        Me.Controls.Add(Me.ogbreportname)
        Me.Controls.Add(Me.ogbbutton)
        Me.Controls.Add(Me.GroupControl1)
        Me.Controls.Add(Me.ogcDocument)
        Me.Name = "wAdjustedTransaction"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "wReportAdjustTransaction"
        CType(Me.ogcDocument, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogcDocument.ResumeLayout(False)
        CType(Me.FDEAdjustStockDateTo.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FDEAdjustStockDateTo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FDSAdjustStockDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FDSAdjustStockDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTAdjustStockNoTo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTAdjustStockNo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents FDEAdjustStockDateTo As DevExpress.XtraEditors.DateEdit
    Friend WithEvents FDSAdjustStockDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents FDRcvDateE_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FDRcvDateS_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTReceiveNoTo_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTAdjustStockNoTo As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FTAdjustStockNo As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FTReceiveNo_lbl As DevExpress.XtraEditors.LabelControl
End Class

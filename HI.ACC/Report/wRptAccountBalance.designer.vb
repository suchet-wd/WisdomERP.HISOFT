<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wRptAccountBalance
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
        Me.ogrpDocument = New DevExpress.XtraEditors.GroupControl()
        Me.FNHSysMatTypeIdTo_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysMatTypeIdTo = New DevExpress.XtraEditors.ButtonEdit()
        Me.FNHSysMatTypeId_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysMatTypeId = New DevExpress.XtraEditors.ButtonEdit()
        Me.AsOfDate_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.ogbreportname = New DevExpress.XtraEditors.GroupControl()
        Me.FNEmpSex_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNReportname = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.ogbbutton = New DevExpress.XtraEditors.GroupControl()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmpreview = New DevExpress.XtraEditors.SimpleButton()
        Me.IcCondition = New HI.UCTR.ICCondition()
        Me.AsOfDate = New DevExpress.XtraEditors.DateEdit()
        CType(Me.ogrpDocument, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogrpDocument.SuspendLayout()
        CType(Me.FNHSysMatTypeIdTo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysMatTypeId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.ogbreportname, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbreportname.SuspendLayout()
        CType(Me.FNReportname.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogbbutton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbbutton.SuspendLayout()
        CType(Me.AsOfDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AsOfDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ogrpDocument
        '
        Me.ogrpDocument.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogrpDocument.Controls.Add(Me.AsOfDate)
        Me.ogrpDocument.Controls.Add(Me.FNHSysMatTypeIdTo_lbl)
        Me.ogrpDocument.Controls.Add(Me.FNHSysMatTypeIdTo)
        Me.ogrpDocument.Controls.Add(Me.FNHSysMatTypeId_lbl)
        Me.ogrpDocument.Controls.Add(Me.FNHSysMatTypeId)
        Me.ogrpDocument.Controls.Add(Me.AsOfDate_lbl)
        Me.ogrpDocument.Location = New System.Drawing.Point(1, 1)
        Me.ogrpDocument.Name = "ogrpDocument"
        Me.ogrpDocument.Size = New System.Drawing.Size(606, 79)
        Me.ogrpDocument.TabIndex = 0
        Me.ogrpDocument.Text = "Document"
        '
        'FNHSysMatTypeIdTo_lbl
        '
        Me.FNHSysMatTypeIdTo_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FNHSysMatTypeIdTo_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysMatTypeIdTo_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysMatTypeIdTo_lbl.Location = New System.Drawing.Point(332, 49)
        Me.FNHSysMatTypeIdTo_lbl.Name = "FNHSysMatTypeIdTo_lbl"
        Me.FNHSysMatTypeIdTo_lbl.Size = New System.Drawing.Size(56, 19)
        Me.FNHSysMatTypeIdTo_lbl.TabIndex = 295
        Me.FNHSysMatTypeIdTo_lbl.Tag = "2|"
        Me.FNHSysMatTypeIdTo_lbl.Text = "To  :"
        '
        'FNHSysMatTypeIdTo
        '
        Me.FNHSysMatTypeIdTo.EnterMoveNextControl = True
        Me.FNHSysMatTypeIdTo.Location = New System.Drawing.Point(394, 50)
        Me.FNHSysMatTypeIdTo.Name = "FNHSysMatTypeIdTo"
        Me.FNHSysMatTypeIdTo.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FNHSysMatTypeIdTo.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysMatTypeIdTo.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysMatTypeIdTo.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysMatTypeIdTo.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FNHSysMatTypeIdTo.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FNHSysMatTypeIdTo.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNHSysMatTypeIdTo.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysMatTypeIdTo.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNHSysMatTypeIdTo.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNHSysMatTypeIdTo.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysMatTypeIdTo.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysMatTypeIdTo.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNHSysMatTypeIdTo.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNHSysMatTypeIdTo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", 15, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", "241", Nothing, True)})
        Me.FNHSysMatTypeIdTo.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.FNHSysMatTypeIdTo.Properties.MaxLength = 30
        Me.FNHSysMatTypeIdTo.Size = New System.Drawing.Size(172, 20)
        Me.FNHSysMatTypeIdTo.TabIndex = 293
        Me.FNHSysMatTypeIdTo.Tag = "2|"
        '
        'FNHSysMatTypeId_lbl
        '
        Me.FNHSysMatTypeId_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FNHSysMatTypeId_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysMatTypeId_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysMatTypeId_lbl.Location = New System.Drawing.Point(31, 49)
        Me.FNHSysMatTypeId_lbl.Name = "FNHSysMatTypeId_lbl"
        Me.FNHSysMatTypeId_lbl.Size = New System.Drawing.Size(128, 19)
        Me.FNHSysMatTypeId_lbl.TabIndex = 294
        Me.FNHSysMatTypeId_lbl.Tag = "2|"
        Me.FNHSysMatTypeId_lbl.Text = "FNHSysMatTypeId :"
        '
        'FNHSysMatTypeId
        '
        Me.FNHSysMatTypeId.EnterMoveNextControl = True
        Me.FNHSysMatTypeId.Location = New System.Drawing.Point(159, 50)
        Me.FNHSysMatTypeId.Name = "FNHSysMatTypeId"
        Me.FNHSysMatTypeId.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FNHSysMatTypeId.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysMatTypeId.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysMatTypeId.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysMatTypeId.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FNHSysMatTypeId.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FNHSysMatTypeId.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNHSysMatTypeId.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysMatTypeId.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNHSysMatTypeId.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNHSysMatTypeId.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysMatTypeId.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysMatTypeId.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNHSysMatTypeId.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNHSysMatTypeId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", 15, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject2, "", "109", Nothing, True)})
        Me.FNHSysMatTypeId.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.FNHSysMatTypeId.Properties.MaxLength = 30
        Me.FNHSysMatTypeId.Size = New System.Drawing.Size(172, 20)
        Me.FNHSysMatTypeId.TabIndex = 292
        Me.FNHSysMatTypeId.Tag = "2|"
        '
        'AsOfDate_lbl
        '
        Me.AsOfDate_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.AsOfDate_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.AsOfDate_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.AsOfDate_lbl.Location = New System.Drawing.Point(31, 23)
        Me.AsOfDate_lbl.Name = "AsOfDate_lbl"
        Me.AsOfDate_lbl.Size = New System.Drawing.Size(128, 19)
        Me.AsOfDate_lbl.TabIndex = 268
        Me.AsOfDate_lbl.Tag = "2|"
        Me.AsOfDate_lbl.Text = "As Of Date :"
        '
        'GroupControl1
        '
        Me.GroupControl1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupControl1.Controls.Add(Me.IcCondition)
        Me.GroupControl1.Location = New System.Drawing.Point(1, 84)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(606, 262)
        Me.GroupControl1.TabIndex = 2
        Me.GroupControl1.Text = "Condition"
        '
        'ogbreportname
        '
        Me.ogbreportname.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbreportname.Controls.Add(Me.FNEmpSex_lbl)
        Me.ogbreportname.Controls.Add(Me.FNReportname)
        Me.ogbreportname.Location = New System.Drawing.Point(1, 350)
        Me.ogbreportname.Name = "ogbreportname"
        Me.ogbreportname.ShowCaption = False
        Me.ogbreportname.Size = New System.Drawing.Size(608, 42)
        Me.ogbreportname.TabIndex = 6
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
        Me.ogbbutton.Location = New System.Drawing.Point(1, 396)
        Me.ogbbutton.Name = "ogbbutton"
        Me.ogbbutton.ShowCaption = False
        Me.ogbbutton.Size = New System.Drawing.Size(607, 42)
        Me.ogbbutton.TabIndex = 7
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
        'IcCondition
        '
        Me.IcCondition.Location = New System.Drawing.Point(49, 24)
        Me.IcCondition.Name = "IcCondition"
        Me.IcCondition.ShowmColorCode = False
        Me.IcCondition.ShowmItemCode = False
        Me.IcCondition.ShowmSizeCode = False
        Me.IcCondition.ShowmSupl = False
        Me.IcCondition.ShowmUser = False
        Me.IcCondition.ShowWHNo = True
        Me.IcCondition.Size = New System.Drawing.Size(520, 220)
        Me.IcCondition.TabIndex = 0
        '
        'AsOfDate
        '
        Me.AsOfDate.EditValue = Nothing
        Me.AsOfDate.Location = New System.Drawing.Point(161, 22)
        Me.AsOfDate.Name = "AsOfDate"
        Me.AsOfDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.AsOfDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.AsOfDate.Size = New System.Drawing.Size(170, 20)
        Me.AsOfDate.TabIndex = 0
        '
        'wRptAccountBalance
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(610, 439)
        Me.Controls.Add(Me.ogbreportname)
        Me.Controls.Add(Me.ogbbutton)
        Me.Controls.Add(Me.GroupControl1)
        Me.Controls.Add(Me.ogrpDocument)
        Me.Name = "wRptAccountBalance"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "wRptAccountBalance"
        CType(Me.ogrpDocument, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogrpDocument.ResumeLayout(False)
        CType(Me.FNHSysMatTypeIdTo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysMatTypeId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        CType(Me.ogbreportname, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbreportname.ResumeLayout(False)
        CType(Me.FNReportname.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogbbutton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbbutton.ResumeLayout(False)
        CType(Me.AsOfDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AsOfDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogrpDocument As DevExpress.XtraEditors.GroupControl
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents IcCondition As HI.UCTR.ICCondition
    Friend WithEvents AsOfDate_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ogbreportname As DevExpress.XtraEditors.GroupControl
    Friend WithEvents FNEmpSex_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNReportname As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents ogbbutton As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmpreview As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FNHSysMatTypeIdTo_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysMatTypeIdTo As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FNHSysMatTypeId_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysMatTypeId As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents AsOfDate As DevExpress.XtraEditors.DateEdit
End Class

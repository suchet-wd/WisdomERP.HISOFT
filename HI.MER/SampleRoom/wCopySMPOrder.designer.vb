<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wCopySMPOrder
    Inherits DevExpress.XtraEditors.XtraForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.ogbCopyOrderHeader = New DevExpress.XtraEditors.GroupControl()
        Me.FNOrderSampleType = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.FNOrderSampleType_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNSMPPrototypeNo = New DevExpress.XtraEditors.CalcEdit()
        Me.FNSMPOrderType = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.FNOrderType_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTOrderNo = New DevExpress.XtraEditors.TextEdit()
        Me.FTOrderNo_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysStyleId_None = New DevExpress.XtraEditors.TextEdit()
        Me.FNHSysStyleId = New DevExpress.XtraEditors.ButtonEdit()
        Me.FNHSysStyleId_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysCmpId_None = New DevExpress.XtraEditors.TextEdit()
        Me.FNHSysCmpId = New DevExpress.XtraEditors.ButtonEdit()
        Me.FNHSysCmpId_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.ogbCopyOrderNoConfirm = New DevExpress.XtraEditors.GroupControl()
        Me.ocmcancel = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmok = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.ogbCopyOrderHeader, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbCopyOrderHeader.SuspendLayout()
        CType(Me.FNOrderSampleType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNSMPPrototypeNo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNSMPOrderType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTOrderNo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysStyleId_None.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysStyleId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysCmpId_None.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysCmpId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogbCopyOrderNoConfirm, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbCopyOrderNoConfirm.SuspendLayout()
        Me.SuspendLayout()
        '
        'ogbCopyOrderHeader
        '
        Me.ogbCopyOrderHeader.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbCopyOrderHeader.Controls.Add(Me.FNOrderSampleType)
        Me.ogbCopyOrderHeader.Controls.Add(Me.FNOrderSampleType_lbl)
        Me.ogbCopyOrderHeader.Controls.Add(Me.FNSMPPrototypeNo)
        Me.ogbCopyOrderHeader.Controls.Add(Me.FNSMPOrderType)
        Me.ogbCopyOrderHeader.Controls.Add(Me.FNOrderType_lbl)
        Me.ogbCopyOrderHeader.Controls.Add(Me.FTOrderNo)
        Me.ogbCopyOrderHeader.Controls.Add(Me.FTOrderNo_lbl)
        Me.ogbCopyOrderHeader.Controls.Add(Me.FNHSysStyleId_None)
        Me.ogbCopyOrderHeader.Controls.Add(Me.FNHSysStyleId)
        Me.ogbCopyOrderHeader.Controls.Add(Me.FNHSysStyleId_lbl)
        Me.ogbCopyOrderHeader.Controls.Add(Me.FNHSysCmpId_None)
        Me.ogbCopyOrderHeader.Controls.Add(Me.FNHSysCmpId)
        Me.ogbCopyOrderHeader.Controls.Add(Me.FNHSysCmpId_lbl)
        Me.ogbCopyOrderHeader.Location = New System.Drawing.Point(4, 4)
        Me.ogbCopyOrderHeader.Name = "ogbCopyOrderHeader"
        Me.ogbCopyOrderHeader.Size = New System.Drawing.Size(644, 161)
        Me.ogbCopyOrderHeader.TabIndex = 288
        Me.ogbCopyOrderHeader.Text = "Source Factory Order No."
        '
        'FNOrderSampleType
        '
        Me.FNOrderSampleType.EditValue = ""
        Me.FNOrderSampleType.EnterMoveNextControl = True
        Me.FNOrderSampleType.Location = New System.Drawing.Point(422, 120)
        Me.FNOrderSampleType.Name = "FNOrderSampleType"
        Me.FNOrderSampleType.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FNOrderSampleType.Properties.Appearance.Options.UseBackColor = True
        Me.FNOrderSampleType.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FNOrderSampleType.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FNOrderSampleType.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FNOrderSampleType.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FNOrderSampleType.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNOrderSampleType.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNOrderSampleType.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNOrderSampleType.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNOrderSampleType.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNOrderSampleType.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNOrderSampleType.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNOrderSampleType.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNOrderSampleType.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FNOrderSampleType.Properties.Tag = "FNOrderSampleType"
        Me.FNOrderSampleType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.FNOrderSampleType.Size = New System.Drawing.Size(110, 20)
        Me.FNOrderSampleType.TabIndex = 559
        Me.FNOrderSampleType.Tag = "2|"
        '
        'FNOrderSampleType_lbl
        '
        Me.FNOrderSampleType_lbl.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(31, Byte), Integer), CType(CType(53, Byte), Integer))
        Me.FNOrderSampleType_lbl.Appearance.Options.UseForeColor = True
        Me.FNOrderSampleType_lbl.Appearance.Options.UseTextOptions = True
        Me.FNOrderSampleType_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNOrderSampleType_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNOrderSampleType_lbl.Location = New System.Drawing.Point(337, 120)
        Me.FNOrderSampleType_lbl.Name = "FNOrderSampleType_lbl"
        Me.FNOrderSampleType_lbl.Size = New System.Drawing.Size(80, 18)
        Me.FNOrderSampleType_lbl.TabIndex = 560
        Me.FNOrderSampleType_lbl.Tag = "2|"
        Me.FNOrderSampleType_lbl.Text = "ประเภท :"
        '
        'FNSMPPrototypeNo
        '
        Me.FNSMPPrototypeNo.EnterMoveNextControl = True
        Me.FNSMPPrototypeNo.Location = New System.Drawing.Point(278, 120)
        Me.FNSMPPrototypeNo.Name = "FNSMPPrototypeNo"
        Me.FNSMPPrototypeNo.Properties.Appearance.Options.UseTextOptions = True
        Me.FNSMPPrototypeNo.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNSMPPrototypeNo.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNSMPPrototypeNo.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNSMPPrototypeNo.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNSMPPrototypeNo.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNSMPPrototypeNo.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNSMPPrototypeNo.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNSMPPrototypeNo.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNSMPPrototypeNo.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNSMPPrototypeNo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, True, False, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", Nothing, Nothing, True)})
        Me.FNSMPPrototypeNo.Properties.DisplayFormat.FormatString = "{0:n0}"
        Me.FNSMPPrototypeNo.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNSMPPrototypeNo.Properties.EditFormat.FormatString = "{0:n0}"
        Me.FNSMPPrototypeNo.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNSMPPrototypeNo.Properties.Precision = 0
        Me.FNSMPPrototypeNo.Size = New System.Drawing.Size(53, 20)
        Me.FNSMPPrototypeNo.TabIndex = 558
        Me.FNSMPPrototypeNo.Tag = "2|"
        '
        'FNSMPOrderType
        '
        Me.FNSMPOrderType.EditValue = ""
        Me.FNSMPOrderType.EnterMoveNextControl = True
        Me.FNSMPOrderType.Location = New System.Drawing.Point(139, 120)
        Me.FNSMPOrderType.Name = "FNSMPOrderType"
        Me.FNSMPOrderType.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FNSMPOrderType.Properties.Appearance.Options.UseBackColor = True
        Me.FNSMPOrderType.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FNSMPOrderType.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FNSMPOrderType.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FNSMPOrderType.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FNSMPOrderType.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNSMPOrderType.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNSMPOrderType.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNSMPOrderType.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNSMPOrderType.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNSMPOrderType.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNSMPOrderType.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNSMPOrderType.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNSMPOrderType.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FNSMPOrderType.Properties.Tag = "FNSMPOrderType"
        Me.FNSMPOrderType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.FNSMPOrderType.Size = New System.Drawing.Size(135, 20)
        Me.FNSMPOrderType.TabIndex = 556
        Me.FNSMPOrderType.Tag = "2|"
        '
        'FNOrderType_lbl
        '
        Me.FNOrderType_lbl.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(31, Byte), Integer), CType(CType(53, Byte), Integer))
        Me.FNOrderType_lbl.Appearance.Options.UseForeColor = True
        Me.FNOrderType_lbl.Appearance.Options.UseTextOptions = True
        Me.FNOrderType_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNOrderType_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNOrderType_lbl.Location = New System.Drawing.Point(13, 121)
        Me.FNOrderType_lbl.Name = "FNOrderType_lbl"
        Me.FNOrderType_lbl.Size = New System.Drawing.Size(123, 18)
        Me.FNOrderType_lbl.TabIndex = 557
        Me.FNOrderType_lbl.Tag = "2|"
        Me.FNOrderType_lbl.Text = "FNSMPOrderType :"
        '
        'FTOrderNo
        '
        Me.FTOrderNo.Location = New System.Drawing.Point(140, 23)
        Me.FTOrderNo.Name = "FTOrderNo"
        Me.FTOrderNo.Properties.Appearance.BackColor = System.Drawing.Color.LightCyan
        Me.FTOrderNo.Properties.Appearance.Options.UseBackColor = True
        Me.FTOrderNo.Properties.ReadOnly = True
        Me.FTOrderNo.Size = New System.Drawing.Size(133, 20)
        Me.FTOrderNo.TabIndex = 528
        Me.FTOrderNo.TabStop = False
        Me.FTOrderNo.Tag = ""
        '
        'FTOrderNo_lbl
        '
        Me.FTOrderNo_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTOrderNo_lbl.Appearance.Options.UseForeColor = True
        Me.FTOrderNo_lbl.Appearance.Options.UseTextOptions = True
        Me.FTOrderNo_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTOrderNo_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTOrderNo_lbl.Location = New System.Drawing.Point(6, 23)
        Me.FTOrderNo_lbl.Name = "FTOrderNo_lbl"
        Me.FTOrderNo_lbl.Size = New System.Drawing.Size(128, 17)
        Me.FTOrderNo_lbl.TabIndex = 527
        Me.FTOrderNo_lbl.Tag = "2|"
        Me.FTOrderNo_lbl.Text = "Source Factory Order No :"
        '
        'FNHSysStyleId_None
        '
        Me.FNHSysStyleId_None.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FNHSysStyleId_None.Location = New System.Drawing.Point(273, 46)
        Me.FNHSysStyleId_None.Name = "FNHSysStyleId_None"
        Me.FNHSysStyleId_None.Properties.Appearance.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysStyleId_None.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysStyleId_None.Properties.ReadOnly = True
        Me.FNHSysStyleId_None.Size = New System.Drawing.Size(342, 20)
        Me.FNHSysStyleId_None.TabIndex = 5
        Me.FNHSysStyleId_None.Tag = ""
        '
        'FNHSysStyleId
        '
        Me.FNHSysStyleId.Location = New System.Drawing.Point(140, 46)
        Me.FNHSysStyleId.Name = "FNHSysStyleId"
        Me.FNHSysStyleId.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysStyleId.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysStyleId.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNHSysStyleId.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNHSysStyleId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, False, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject2, "", "89", Nothing, True)})
        Me.FNHSysStyleId.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.FNHSysStyleId.Properties.ReadOnly = True
        Me.FNHSysStyleId.Properties.Tag = ""
        Me.FNHSysStyleId.Size = New System.Drawing.Size(133, 20)
        Me.FNHSysStyleId.TabIndex = 4
        Me.FNHSysStyleId.Tag = ""
        '
        'FNHSysStyleId_lbl
        '
        Me.FNHSysStyleId_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysStyleId_lbl.Appearance.Options.UseForeColor = True
        Me.FNHSysStyleId_lbl.Appearance.Options.UseTextOptions = True
        Me.FNHSysStyleId_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysStyleId_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysStyleId_lbl.Location = New System.Drawing.Point(6, 47)
        Me.FNHSysStyleId_lbl.Name = "FNHSysStyleId_lbl"
        Me.FNHSysStyleId_lbl.Size = New System.Drawing.Size(128, 17)
        Me.FNHSysStyleId_lbl.TabIndex = 439
        Me.FNHSysStyleId_lbl.Tag = "2|"
        Me.FNHSysStyleId_lbl.Text = "FNHSysStyleId :"
        '
        'FNHSysCmpId_None
        '
        Me.FNHSysCmpId_None.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FNHSysCmpId_None.Location = New System.Drawing.Point(273, 94)
        Me.FNHSysCmpId_None.Name = "FNHSysCmpId_None"
        Me.FNHSysCmpId_None.Properties.Appearance.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysCmpId_None.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysCmpId_None.Properties.ReadOnly = True
        Me.FNHSysCmpId_None.Size = New System.Drawing.Size(342, 20)
        Me.FNHSysCmpId_None.TabIndex = 1
        Me.FNHSysCmpId_None.Tag = "2|"
        '
        'FNHSysCmpId
        '
        Me.FNHSysCmpId.Location = New System.Drawing.Point(139, 94)
        Me.FNHSysCmpId.Name = "FNHSysCmpId"
        Me.FNHSysCmpId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject3, "", "11", Nothing, True)})
        Me.FNHSysCmpId.Properties.Tag = ""
        Me.FNHSysCmpId.Size = New System.Drawing.Size(133, 20)
        Me.FNHSysCmpId.TabIndex = 0
        Me.FNHSysCmpId.Tag = "2|"
        '
        'FNHSysCmpId_lbl
        '
        Me.FNHSysCmpId_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysCmpId_lbl.Appearance.Options.UseForeColor = True
        Me.FNHSysCmpId_lbl.Appearance.Options.UseTextOptions = True
        Me.FNHSysCmpId_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysCmpId_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysCmpId_lbl.Location = New System.Drawing.Point(5, 94)
        Me.FNHSysCmpId_lbl.Name = "FNHSysCmpId_lbl"
        Me.FNHSysCmpId_lbl.Size = New System.Drawing.Size(128, 17)
        Me.FNHSysCmpId_lbl.TabIndex = 282
        Me.FNHSysCmpId_lbl.Tag = "2|"
        Me.FNHSysCmpId_lbl.Text = "FNHSysCmpId  :"
        '
        'ogbCopyOrderNoConfirm
        '
        Me.ogbCopyOrderNoConfirm.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbCopyOrderNoConfirm.Controls.Add(Me.ocmcancel)
        Me.ogbCopyOrderNoConfirm.Controls.Add(Me.ocmok)
        Me.ogbCopyOrderNoConfirm.Location = New System.Drawing.Point(4, 170)
        Me.ogbCopyOrderNoConfirm.Name = "ogbCopyOrderNoConfirm"
        Me.ogbCopyOrderNoConfirm.ShowCaption = False
        Me.ogbCopyOrderNoConfirm.Size = New System.Drawing.Size(644, 41)
        Me.ogbCopyOrderNoConfirm.TabIndex = 289
        Me.ogbCopyOrderNoConfirm.Text = "GroupControl2"
        '
        'ocmcancel
        '
        Me.ocmcancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmcancel.Location = New System.Drawing.Point(367, 9)
        Me.ocmcancel.Name = "ocmcancel"
        Me.ocmcancel.Size = New System.Drawing.Size(145, 25)
        Me.ocmcancel.TabIndex = 1
        Me.ocmcancel.TabStop = False
        Me.ocmcancel.Tag = "2|"
        Me.ocmcancel.Text = "Cancel"
        '
        'ocmok
        '
        Me.ocmok.Location = New System.Drawing.Point(133, 9)
        Me.ocmok.Name = "ocmok"
        Me.ocmok.Size = New System.Drawing.Size(133, 25)
        Me.ocmok.TabIndex = 0
        Me.ocmok.TabStop = False
        Me.ocmok.Tag = "2|"
        Me.ocmok.Text = "OK"
        '
        'wCopySMPOrder
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(652, 217)
        Me.ControlBox = False
        Me.Controls.Add(Me.ogbCopyOrderNoConfirm)
        Me.Controls.Add(Me.ogbCopyOrderHeader)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "wCopySMPOrder"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Copy Order"
        CType(Me.ogbCopyOrderHeader, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbCopyOrderHeader.ResumeLayout(False)
        CType(Me.FNOrderSampleType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNSMPPrototypeNo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNSMPOrderType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTOrderNo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysStyleId_None.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysStyleId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysCmpId_None.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysCmpId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogbCopyOrderNoConfirm, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbCopyOrderNoConfirm.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogbCopyOrderHeader As DevExpress.XtraEditors.GroupControl
    Friend WithEvents FNHSysCmpId_None As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FNHSysCmpId As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FNHSysCmpId_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysStyleId_None As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FNHSysStyleId As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FNHSysStyleId_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ogbCopyOrderNoConfirm As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ocmcancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmok As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FTOrderNo_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTOrderNo As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FNOrderSampleType As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents FNOrderSampleType_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNSMPPrototypeNo As DevExpress.XtraEditors.CalcEdit
    Friend WithEvents FNSMPOrderType As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents FNOrderType_lbl As DevExpress.XtraEditors.LabelControl
End Class

﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wCopyRDSamOperation
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
        Dim SerializableAppearanceObject4 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject5 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject6 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject7 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.ogbCopyOrderHeader = New DevExpress.XtraEditors.GroupControl()
        Me.FNHSysSeasonId = New DevExpress.XtraEditors.ButtonEdit()
        Me.FNHSysSeasonId_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysStyleId_None = New DevExpress.XtraEditors.TextEdit()
        Me.FNHSysStyleId_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysStyleId = New DevExpress.XtraEditors.ButtonEdit()
        Me.FNSMPPrototypeNo = New DevExpress.XtraEditors.CalcEdit()
        Me.FNOrderSampleType = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.FNOrderSampleType_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNSMPOrderType = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.FNOrderType_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTSMPOrderNo = New DevExpress.XtraEditors.ButtonEdit()
        Me.FTSMPOrderNo_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.ogbCopyOrderNoConfirm = New DevExpress.XtraEditors.GroupControl()
        Me.ocmcancel = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmok = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.ogbCopyOrderHeader, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbCopyOrderHeader.SuspendLayout()
        CType(Me.FNHSysSeasonId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysStyleId_None.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysStyleId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNSMPPrototypeNo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNOrderSampleType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNSMPOrderType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTSMPOrderNo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogbCopyOrderNoConfirm, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbCopyOrderNoConfirm.SuspendLayout()
        Me.SuspendLayout()
        '
        'ogbCopyOrderHeader
        '
        Me.ogbCopyOrderHeader.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbCopyOrderHeader.Controls.Add(Me.FNHSysSeasonId)
        Me.ogbCopyOrderHeader.Controls.Add(Me.FNHSysSeasonId_lbl)
        Me.ogbCopyOrderHeader.Controls.Add(Me.FNHSysStyleId_None)
        Me.ogbCopyOrderHeader.Controls.Add(Me.FNHSysStyleId_lbl)
        Me.ogbCopyOrderHeader.Controls.Add(Me.FNHSysStyleId)
        Me.ogbCopyOrderHeader.Controls.Add(Me.FNSMPPrototypeNo)
        Me.ogbCopyOrderHeader.Controls.Add(Me.FNOrderSampleType)
        Me.ogbCopyOrderHeader.Controls.Add(Me.FNOrderSampleType_lbl)
        Me.ogbCopyOrderHeader.Controls.Add(Me.FNSMPOrderType)
        Me.ogbCopyOrderHeader.Controls.Add(Me.FNOrderType_lbl)
        Me.ogbCopyOrderHeader.Controls.Add(Me.FTSMPOrderNo)
        Me.ogbCopyOrderHeader.Controls.Add(Me.FTSMPOrderNo_lbl)
        Me.ogbCopyOrderHeader.Location = New System.Drawing.Point(4, 4)
        Me.ogbCopyOrderHeader.Name = "ogbCopyOrderHeader"
        Me.ogbCopyOrderHeader.Size = New System.Drawing.Size(659, 117)
        Me.ogbCopyOrderHeader.TabIndex = 288
        Me.ogbCopyOrderHeader.Text = "Source Factory Order No."
        '
        'FNHSysSeasonId
        '
        Me.FNHSysSeasonId.Location = New System.Drawing.Point(403, 77)
        Me.FNHSysSeasonId.Name = "FNHSysSeasonId"
        Me.FNHSysSeasonId.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysSeasonId.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysSeasonId.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNHSysSeasonId.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNHSysSeasonId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, False, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", "94", Nothing, True)})
        Me.FNHSysSeasonId.Properties.ReadOnly = True
        Me.FNHSysSeasonId.Properties.Tag = ""
        Me.FNHSysSeasonId.Size = New System.Drawing.Size(97, 20)
        Me.FNHSysSeasonId.TabIndex = 581
        Me.FNHSysSeasonId.Tag = "2|"
        '
        'FNHSysSeasonId_lbl
        '
        Me.FNHSysSeasonId_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysSeasonId_lbl.Appearance.Options.UseForeColor = True
        Me.FNHSysSeasonId_lbl.Appearance.Options.UseTextOptions = True
        Me.FNHSysSeasonId_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysSeasonId_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysSeasonId_lbl.Location = New System.Drawing.Point(305, 78)
        Me.FNHSysSeasonId_lbl.Name = "FNHSysSeasonId_lbl"
        Me.FNHSysSeasonId_lbl.Size = New System.Drawing.Size(96, 19)
        Me.FNHSysSeasonId_lbl.TabIndex = 580
        Me.FNHSysSeasonId_lbl.Tag = "2|"
        Me.FNHSysSeasonId_lbl.Text = "Season :"
        '
        'FNHSysStyleId_None
        '
        Me.FNHSysStyleId_None.Location = New System.Drawing.Point(305, 52)
        Me.FNHSysStyleId_None.Name = "FNHSysStyleId_None"
        Me.FNHSysStyleId_None.Properties.Appearance.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysStyleId_None.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysStyleId_None.Properties.ReadOnly = True
        Me.FNHSysStyleId_None.Size = New System.Drawing.Size(285, 20)
        Me.FNHSysStyleId_None.TabIndex = 579
        Me.FNHSysStyleId_None.Tag = "2|"
        '
        'FNHSysStyleId_lbl
        '
        Me.FNHSysStyleId_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysStyleId_lbl.Appearance.Options.UseForeColor = True
        Me.FNHSysStyleId_lbl.Appearance.Options.UseTextOptions = True
        Me.FNHSysStyleId_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysStyleId_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysStyleId_lbl.Location = New System.Drawing.Point(11, 52)
        Me.FNHSysStyleId_lbl.Name = "FNHSysStyleId_lbl"
        Me.FNHSysStyleId_lbl.Size = New System.Drawing.Size(127, 18)
        Me.FNHSysStyleId_lbl.TabIndex = 577
        Me.FNHSysStyleId_lbl.Tag = "2|"
        Me.FNHSysStyleId_lbl.Text = "Style :"
        '
        'FNHSysStyleId
        '
        Me.FNHSysStyleId.Location = New System.Drawing.Point(140, 52)
        Me.FNHSysStyleId.Name = "FNHSysStyleId"
        Me.FNHSysStyleId.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysStyleId.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysStyleId.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNHSysStyleId.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNHSysStyleId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, False, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject2, "", "235", Nothing, True)})
        Me.FNHSysStyleId.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.FNHSysStyleId.Properties.ReadOnly = True
        Me.FNHSysStyleId.Properties.Tag = ""
        Me.FNHSysStyleId.Size = New System.Drawing.Size(162, 20)
        Me.FNHSysStyleId.TabIndex = 578
        Me.FNHSysStyleId.Tag = "2|"
        '
        'FNSMPPrototypeNo
        '
        Me.FNSMPPrototypeNo.EnterMoveNextControl = True
        Me.FNSMPPrototypeNo.Location = New System.Drawing.Point(553, 27)
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
        Me.FNSMPPrototypeNo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, True, False, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject3, "", Nothing, Nothing, True)})
        Me.FNSMPPrototypeNo.Properties.DisplayFormat.FormatString = "{0:n0}"
        Me.FNSMPPrototypeNo.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNSMPPrototypeNo.Properties.EditFormat.FormatString = "{0:n0}"
        Me.FNSMPPrototypeNo.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNSMPPrototypeNo.Properties.Precision = 0
        Me.FNSMPPrototypeNo.Properties.ReadOnly = True
        Me.FNSMPPrototypeNo.Size = New System.Drawing.Size(37, 20)
        Me.FNSMPPrototypeNo.TabIndex = 576
        Me.FNSMPPrototypeNo.Tag = "2|"
        '
        'FNOrderSampleType
        '
        Me.FNOrderSampleType.EditValue = ""
        Me.FNOrderSampleType.EnterMoveNextControl = True
        Me.FNOrderSampleType.Location = New System.Drawing.Point(140, 77)
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
        Me.FNOrderSampleType.Properties.ReadOnly = True
        Me.FNOrderSampleType.Properties.Tag = "FNOrderSampleType"
        Me.FNOrderSampleType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.FNOrderSampleType.Size = New System.Drawing.Size(95, 20)
        Me.FNOrderSampleType.TabIndex = 574
        Me.FNOrderSampleType.Tag = "2|"
        '
        'FNOrderSampleType_lbl
        '
        Me.FNOrderSampleType_lbl.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(31, Byte), Integer), CType(CType(53, Byte), Integer))
        Me.FNOrderSampleType_lbl.Appearance.Options.UseForeColor = True
        Me.FNOrderSampleType_lbl.Appearance.Options.UseTextOptions = True
        Me.FNOrderSampleType_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNOrderSampleType_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNOrderSampleType_lbl.Location = New System.Drawing.Point(55, 77)
        Me.FNOrderSampleType_lbl.Name = "FNOrderSampleType_lbl"
        Me.FNOrderSampleType_lbl.Size = New System.Drawing.Size(80, 18)
        Me.FNOrderSampleType_lbl.TabIndex = 575
        Me.FNOrderSampleType_lbl.Tag = "2|"
        Me.FNOrderSampleType_lbl.Text = "ประเภท :"
        '
        'FNSMPOrderType
        '
        Me.FNSMPOrderType.EditValue = ""
        Me.FNSMPOrderType.EnterMoveNextControl = True
        Me.FNSMPOrderType.Location = New System.Drawing.Point(424, 28)
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
        Me.FNSMPOrderType.Properties.ReadOnly = True
        Me.FNSMPOrderType.Properties.Tag = "FNSMPOrderType"
        Me.FNSMPOrderType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.FNSMPOrderType.Size = New System.Drawing.Size(127, 20)
        Me.FNSMPOrderType.TabIndex = 572
        Me.FNSMPOrderType.Tag = "2|"
        '
        'FNOrderType_lbl
        '
        Me.FNOrderType_lbl.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(31, Byte), Integer), CType(CType(53, Byte), Integer))
        Me.FNOrderType_lbl.Appearance.Options.UseForeColor = True
        Me.FNOrderType_lbl.Appearance.Options.UseTextOptions = True
        Me.FNOrderType_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNOrderType_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNOrderType_lbl.Location = New System.Drawing.Point(305, 28)
        Me.FNOrderType_lbl.Name = "FNOrderType_lbl"
        Me.FNOrderType_lbl.Size = New System.Drawing.Size(113, 18)
        Me.FNOrderType_lbl.TabIndex = 573
        Me.FNOrderType_lbl.Tag = "2|"
        Me.FNOrderType_lbl.Text = "FNSMPOrderType :"
        '
        'FTSMPOrderNo
        '
        Me.FTSMPOrderNo.EnterMoveNextControl = True
        Me.FTSMPOrderNo.Location = New System.Drawing.Point(140, 28)
        Me.FTSMPOrderNo.Name = "FTSMPOrderNo"
        Me.FTSMPOrderNo.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FTSMPOrderNo.Properties.Appearance.Options.UseBackColor = True
        Me.FTSMPOrderNo.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FTSMPOrderNo.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FTSMPOrderNo.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FTSMPOrderNo.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FTSMPOrderNo.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FTSMPOrderNo.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FTSMPOrderNo.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FTSMPOrderNo.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FTSMPOrderNo.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FTSMPOrderNo.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FTSMPOrderNo.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FTSMPOrderNo.Properties.AppearanceReadOnly.Options.UseForeColor = True
        SerializableAppearanceObject4.Options.UseTextOptions = True
        SerializableAppearanceObject4.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        SerializableAppearanceObject5.Options.UseTextOptions = True
        SerializableAppearanceObject5.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        SerializableAppearanceObject6.Options.UseTextOptions = True
        SerializableAppearanceObject6.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        SerializableAppearanceObject7.Options.UseTextOptions = True
        SerializableAppearanceObject7.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTSMPOrderNo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "...", 15, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, "", New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject4, SerializableAppearanceObject5, SerializableAppearanceObject6, SerializableAppearanceObject7, "", "579", Nothing, True)})
        Me.FTSMPOrderNo.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.FTSMPOrderNo.Size = New System.Drawing.Size(162, 20)
        Me.FTSMPOrderNo.TabIndex = 570
        Me.FTSMPOrderNo.TabStop = False
        Me.FTSMPOrderNo.Tag = "2|"
        '
        'FTSMPOrderNo_lbl
        '
        Me.FTSMPOrderNo_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTSMPOrderNo_lbl.Appearance.Options.UseForeColor = True
        Me.FTSMPOrderNo_lbl.Appearance.Options.UseTextOptions = True
        Me.FTSMPOrderNo_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTSMPOrderNo_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTSMPOrderNo_lbl.Location = New System.Drawing.Point(17, 29)
        Me.FTSMPOrderNo_lbl.Name = "FTSMPOrderNo_lbl"
        Me.FTSMPOrderNo_lbl.Size = New System.Drawing.Size(120, 19)
        Me.FTSMPOrderNo_lbl.TabIndex = 571
        Me.FTSMPOrderNo_lbl.Tag = "2|"
        Me.FTSMPOrderNo_lbl.Text = "Sample Order No. :"
        '
        'ogbCopyOrderNoConfirm
        '
        Me.ogbCopyOrderNoConfirm.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbCopyOrderNoConfirm.Controls.Add(Me.ocmcancel)
        Me.ogbCopyOrderNoConfirm.Controls.Add(Me.ocmok)
        Me.ogbCopyOrderNoConfirm.Location = New System.Drawing.Point(4, 126)
        Me.ogbCopyOrderNoConfirm.Name = "ogbCopyOrderNoConfirm"
        Me.ogbCopyOrderNoConfirm.ShowCaption = False
        Me.ogbCopyOrderNoConfirm.Size = New System.Drawing.Size(659, 41)
        Me.ogbCopyOrderNoConfirm.TabIndex = 289
        Me.ogbCopyOrderNoConfirm.Text = "GroupControl2"
        '
        'ocmcancel
        '
        Me.ocmcancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmcancel.Location = New System.Drawing.Point(382, 9)
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
        'wCopyRDSamOperation
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(667, 173)
        Me.ControlBox = False
        Me.Controls.Add(Me.ogbCopyOrderNoConfirm)
        Me.Controls.Add(Me.ogbCopyOrderHeader)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "wCopyRDSamOperation"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Copy RD Sam Operation"
        CType(Me.ogbCopyOrderHeader, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbCopyOrderHeader.ResumeLayout(False)
        CType(Me.FNHSysSeasonId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysStyleId_None.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysStyleId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNSMPPrototypeNo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNOrderSampleType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNSMPOrderType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTSMPOrderNo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogbCopyOrderNoConfirm, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbCopyOrderNoConfirm.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogbCopyOrderHeader As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ogbCopyOrderNoConfirm As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ocmcancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmok As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FNHSysSeasonId As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FNHSysSeasonId_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysStyleId_None As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FNHSysStyleId_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysStyleId As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FNSMPPrototypeNo As DevExpress.XtraEditors.CalcEdit
    Friend WithEvents FNOrderSampleType As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents FNOrderSampleType_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNSMPOrderType As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents FNOrderType_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTSMPOrderNo As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FTSMPOrderNo_lbl As DevExpress.XtraEditors.LabelControl
End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wConfigCom
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
        Me.FTUserWindow = New DevExpress.XtraEditors.TextEdit()
        Me.FTUserWindow_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNLCDType = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.FNLCDType_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysUnitSectIdTo_None = New DevExpress.XtraEditors.TextEdit()
        Me.FNHSysUnitSectId_None = New DevExpress.XtraEditors.TextEdit()
        Me.FNHSysUnitSectId = New DevExpress.XtraEditors.ButtonEdit()
        Me.FNHSysUnitSectIdTo = New DevExpress.XtraEditors.ButtonEdit()
        Me.FNHSysUnitSectIdTo_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysUnitSectId_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTComputerName = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.FTComputerName_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.ogbmainprocbutton = New DevExpress.XtraEditors.PanelControl()
        Me.ocmdelete = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmsave = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmsavelayout = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmclear = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmload = New DevExpress.XtraEditors.SimpleButton()
        Me.ogrpDetail = New DevExpress.XtraEditors.GroupControl()
        Me.ogcDetail = New DevExpress.XtraGrid.GridControl()
        Me.ogvDetail = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.cFTComputerName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTUnitSectCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTUnitSectCodeTo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFNLCDType = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNLCDTypeName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFTUserWindow = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.ogrpDocument, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogrpDocument.SuspendLayout()
        CType(Me.FTUserWindow.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNLCDType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysUnitSectIdTo_None.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysUnitSectId_None.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysUnitSectId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysUnitSectIdTo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTComputerName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbmainprocbutton.SuspendLayout()
        CType(Me.ogrpDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogrpDetail.SuspendLayout()
        CType(Me.ogcDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ogrpDocument
        '
        Me.ogrpDocument.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogrpDocument.Controls.Add(Me.FTUserWindow)
        Me.ogrpDocument.Controls.Add(Me.FTUserWindow_lbl)
        Me.ogrpDocument.Controls.Add(Me.FNLCDType)
        Me.ogrpDocument.Controls.Add(Me.FNLCDType_lbl)
        Me.ogrpDocument.Controls.Add(Me.FNHSysUnitSectIdTo_None)
        Me.ogrpDocument.Controls.Add(Me.FNHSysUnitSectId_None)
        Me.ogrpDocument.Controls.Add(Me.FNHSysUnitSectId)
        Me.ogrpDocument.Controls.Add(Me.FNHSysUnitSectIdTo)
        Me.ogrpDocument.Controls.Add(Me.FNHSysUnitSectIdTo_lbl)
        Me.ogrpDocument.Controls.Add(Me.FNHSysUnitSectId_lbl)
        Me.ogrpDocument.Controls.Add(Me.FTComputerName)
        Me.ogrpDocument.Controls.Add(Me.FTComputerName_lbl)
        Me.ogrpDocument.Location = New System.Drawing.Point(1, 0)
        Me.ogrpDocument.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogrpDocument.Name = "ogrpDocument"
        Me.ogrpDocument.Size = New System.Drawing.Size(790, 148)
        Me.ogrpDocument.TabIndex = 1
        Me.ogrpDocument.Text = "Document"
        '
        'FTUserWindow
        '
        Me.FTUserWindow.EnterMoveNextControl = True
        Me.FTUserWindow.Location = New System.Drawing.Point(541, 33)
        Me.FTUserWindow.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTUserWindow.Name = "FTUserWindow"
        Me.FTUserWindow.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FTUserWindow.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTUserWindow.Properties.Appearance.Options.UseBackColor = True
        Me.FTUserWindow.Properties.Appearance.Options.UseForeColor = True
        Me.FTUserWindow.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FTUserWindow.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FTUserWindow.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FTUserWindow.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FTUserWindow.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FTUserWindow.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FTUserWindow.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FTUserWindow.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FTUserWindow.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FTUserWindow.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FTUserWindow.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FTUserWindow.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FTUserWindow.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.FTUserWindow.Properties.MaxLength = 100
        Me.FTUserWindow.Size = New System.Drawing.Size(233, 23)
        Me.FTUserWindow.TabIndex = 1
        Me.FTUserWindow.TabStop = False
        Me.FTUserWindow.Tag = "2|"
        Me.FTUserWindow.Visible = False
        '
        'FTUserWindow_lbl
        '
        Me.FTUserWindow_lbl.Appearance.Options.UseTextOptions = True
        Me.FTUserWindow_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTUserWindow_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTUserWindow_lbl.Location = New System.Drawing.Point(361, 36)
        Me.FTUserWindow_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTUserWindow_lbl.Name = "FTUserWindow_lbl"
        Me.FTUserWindow_lbl.Size = New System.Drawing.Size(176, 19)
        Me.FTUserWindow_lbl.TabIndex = 292
        Me.FTUserWindow_lbl.Tag = "2|"
        Me.FTUserWindow_lbl.Text = "User Windows Login :"
        Me.FTUserWindow_lbl.Visible = False
        '
        'FNLCDType
        '
        Me.FNLCDType.EditValue = ""
        Me.FNLCDType.EnterMoveNextControl = True
        Me.FNLCDType.Location = New System.Drawing.Point(175, 115)
        Me.FNLCDType.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNLCDType.Name = "FNLCDType"
        Me.FNLCDType.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FNLCDType.Properties.Appearance.Options.UseBackColor = True
        Me.FNLCDType.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FNLCDType.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FNLCDType.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FNLCDType.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FNLCDType.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNLCDType.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNLCDType.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNLCDType.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNLCDType.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNLCDType.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNLCDType.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNLCDType.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNLCDType.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FNLCDType.Properties.Tag = "FNLCDType"
        Me.FNLCDType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.FNLCDType.Size = New System.Drawing.Size(185, 23)
        Me.FNLCDType.TabIndex = 4
        Me.FNLCDType.Tag = "2|"
        '
        'FNLCDType_lbl
        '
        Me.FNLCDType_lbl.Appearance.Options.UseTextOptions = True
        Me.FNLCDType_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNLCDType_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNLCDType_lbl.Location = New System.Drawing.Point(13, 114)
        Me.FNLCDType_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNLCDType_lbl.Name = "FNLCDType_lbl"
        Me.FNLCDType_lbl.Size = New System.Drawing.Size(155, 25)
        Me.FNLCDType_lbl.TabIndex = 289
        Me.FNLCDType_lbl.Text = "LCD Type :"
        '
        'FNHSysUnitSectIdTo_None
        '
        Me.FNHSysUnitSectIdTo_None.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FNHSysUnitSectIdTo_None.EnterMoveNextControl = True
        Me.FNHSysUnitSectIdTo_None.Location = New System.Drawing.Point(360, 86)
        Me.FNHSysUnitSectIdTo_None.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysUnitSectIdTo_None.Name = "FNHSysUnitSectIdTo_None"
        Me.FNHSysUnitSectIdTo_None.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FNHSysUnitSectIdTo_None.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FNHSysUnitSectIdTo_None.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysUnitSectIdTo_None.Properties.Appearance.Options.UseForeColor = True
        Me.FNHSysUnitSectIdTo_None.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysUnitSectIdTo_None.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysUnitSectIdTo_None.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FNHSysUnitSectIdTo_None.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FNHSysUnitSectIdTo_None.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNHSysUnitSectIdTo_None.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysUnitSectIdTo_None.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNHSysUnitSectIdTo_None.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNHSysUnitSectIdTo_None.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysUnitSectIdTo_None.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysUnitSectIdTo_None.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNHSysUnitSectIdTo_None.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNHSysUnitSectIdTo_None.Properties.ReadOnly = True
        Me.FNHSysUnitSectIdTo_None.Size = New System.Drawing.Size(414, 23)
        Me.FNHSysUnitSectIdTo_None.TabIndex = 288
        Me.FNHSysUnitSectIdTo_None.TabStop = False
        Me.FNHSysUnitSectIdTo_None.Tag = "2|"
        '
        'FNHSysUnitSectId_None
        '
        Me.FNHSysUnitSectId_None.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FNHSysUnitSectId_None.EnterMoveNextControl = True
        Me.FNHSysUnitSectId_None.Location = New System.Drawing.Point(360, 59)
        Me.FNHSysUnitSectId_None.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysUnitSectId_None.Name = "FNHSysUnitSectId_None"
        Me.FNHSysUnitSectId_None.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FNHSysUnitSectId_None.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FNHSysUnitSectId_None.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysUnitSectId_None.Properties.Appearance.Options.UseForeColor = True
        Me.FNHSysUnitSectId_None.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysUnitSectId_None.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysUnitSectId_None.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FNHSysUnitSectId_None.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FNHSysUnitSectId_None.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNHSysUnitSectId_None.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysUnitSectId_None.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNHSysUnitSectId_None.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNHSysUnitSectId_None.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysUnitSectId_None.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysUnitSectId_None.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNHSysUnitSectId_None.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNHSysUnitSectId_None.Properties.ReadOnly = True
        Me.FNHSysUnitSectId_None.Size = New System.Drawing.Size(414, 23)
        Me.FNHSysUnitSectId_None.TabIndex = 287
        Me.FNHSysUnitSectId_None.TabStop = False
        Me.FNHSysUnitSectId_None.Tag = "2|"
        '
        'FNHSysUnitSectId
        '
        Me.FNHSysUnitSectId.Location = New System.Drawing.Point(175, 59)
        Me.FNHSysUnitSectId.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysUnitSectId.Name = "FNHSysUnitSectId"
        Me.FNHSysUnitSectId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", "165", Nothing, True)})
        Me.FNHSysUnitSectId.Size = New System.Drawing.Size(185, 23)
        Me.FNHSysUnitSectId.TabIndex = 2
        Me.FNHSysUnitSectId.Tag = "2|"
        '
        'FNHSysUnitSectIdTo
        '
        Me.FNHSysUnitSectIdTo.Location = New System.Drawing.Point(175, 86)
        Me.FNHSysUnitSectIdTo.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysUnitSectIdTo.Name = "FNHSysUnitSectIdTo"
        Me.FNHSysUnitSectIdTo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject2, "", "245", Nothing, True)})
        Me.FNHSysUnitSectIdTo.Size = New System.Drawing.Size(185, 23)
        Me.FNHSysUnitSectIdTo.TabIndex = 3
        Me.FNHSysUnitSectIdTo.Tag = "2|"
        '
        'FNHSysUnitSectIdTo_lbl
        '
        Me.FNHSysUnitSectIdTo_lbl.Appearance.Options.UseTextOptions = True
        Me.FNHSysUnitSectIdTo_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysUnitSectIdTo_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysUnitSectIdTo_lbl.Location = New System.Drawing.Point(13, 89)
        Me.FNHSysUnitSectIdTo_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysUnitSectIdTo_lbl.Name = "FNHSysUnitSectIdTo_lbl"
        Me.FNHSysUnitSectIdTo_lbl.Size = New System.Drawing.Size(155, 25)
        Me.FNHSysUnitSectIdTo_lbl.TabIndex = 3
        Me.FNHSysUnitSectIdTo_lbl.Text = "FNHSysUnitSectId :"
        '
        'FNHSysUnitSectId_lbl
        '
        Me.FNHSysUnitSectId_lbl.Appearance.Options.UseTextOptions = True
        Me.FNHSysUnitSectId_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysUnitSectId_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysUnitSectId_lbl.Location = New System.Drawing.Point(13, 59)
        Me.FNHSysUnitSectId_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysUnitSectId_lbl.Name = "FNHSysUnitSectId_lbl"
        Me.FNHSysUnitSectId_lbl.Size = New System.Drawing.Size(155, 25)
        Me.FNHSysUnitSectId_lbl.TabIndex = 3
        Me.FNHSysUnitSectId_lbl.Text = "FNHSysUnitSectId :"
        '
        'FTComputerName
        '
        Me.FTComputerName.Location = New System.Drawing.Point(175, 32)
        Me.FTComputerName.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTComputerName.Name = "FTComputerName"
        Me.FTComputerName.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FTComputerName.Size = New System.Drawing.Size(185, 23)
        Me.FTComputerName.TabIndex = 0
        Me.FTComputerName.Tag = "2|"
        '
        'FTComputerName_lbl
        '
        Me.FTComputerName_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTComputerName_lbl.Appearance.Options.UseForeColor = True
        Me.FTComputerName_lbl.Appearance.Options.UseTextOptions = True
        Me.FTComputerName_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTComputerName_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTComputerName_lbl.Location = New System.Drawing.Point(6, 31)
        Me.FTComputerName_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTComputerName_lbl.Name = "FTComputerName_lbl"
        Me.FTComputerName_lbl.Size = New System.Drawing.Size(162, 26)
        Me.FTComputerName_lbl.TabIndex = 0
        Me.FTComputerName_lbl.Text = "Computer Name :"
        '
        'ogbmainprocbutton
        '
        Me.ogbmainprocbutton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmdelete)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmsave)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmsavelayout)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmexit)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmclear)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmload)
        Me.ogbmainprocbutton.Location = New System.Drawing.Point(104, 166)
        Me.ogbmainprocbutton.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbmainprocbutton.Name = "ogbmainprocbutton"
        Me.ogbmainprocbutton.Size = New System.Drawing.Size(632, 58)
        Me.ogbmainprocbutton.TabIndex = 389
        Me.ogbmainprocbutton.Tag = "2|"
        '
        'ocmdelete
        '
        Me.ocmdelete.Location = New System.Drawing.Point(565, 14)
        Me.ocmdelete.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmdelete.Name = "ocmdelete"
        Me.ocmdelete.Size = New System.Drawing.Size(68, 31)
        Me.ocmdelete.TabIndex = 334
        Me.ocmdelete.TabStop = False
        Me.ocmdelete.Tag = "2|"
        Me.ocmdelete.Text = "DELETE"
        '
        'ocmsave
        '
        Me.ocmsave.Location = New System.Drawing.Point(427, 12)
        Me.ocmsave.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmsave.Name = "ocmsave"
        Me.ocmsave.Size = New System.Drawing.Size(84, 31)
        Me.ocmsave.TabIndex = 333
        Me.ocmsave.TabStop = False
        Me.ocmsave.Tag = "2|"
        Me.ocmsave.Text = "SAVE"
        '
        'ocmsavelayout
        '
        Me.ocmsavelayout.Location = New System.Drawing.Point(302, 16)
        Me.ocmsavelayout.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmsavelayout.Name = "ocmsavelayout"
        Me.ocmsavelayout.Size = New System.Drawing.Size(136, 28)
        Me.ocmsavelayout.TabIndex = 332
        Me.ocmsavelayout.Text = "savelayoutgrid"
        '
        'ocmexit
        '
        Me.ocmexit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmexit.Location = New System.Drawing.Point(500, 14)
        Me.ocmexit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmexit.Name = "ocmexit"
        Me.ocmexit.Size = New System.Drawing.Size(111, 31)
        Me.ocmexit.TabIndex = 96
        Me.ocmexit.TabStop = False
        Me.ocmexit.Tag = "2|"
        Me.ocmexit.Text = "EXIT"
        '
        'ocmclear
        '
        Me.ocmclear.Location = New System.Drawing.Point(16, 12)
        Me.ocmclear.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmclear.Name = "ocmclear"
        Me.ocmclear.Size = New System.Drawing.Size(111, 31)
        Me.ocmclear.TabIndex = 95
        Me.ocmclear.TabStop = False
        Me.ocmclear.Tag = "2|"
        Me.ocmclear.Text = "CLEAR"
        Me.ocmclear.Visible = False
        '
        'ocmload
        '
        Me.ocmload.Location = New System.Drawing.Point(133, 17)
        Me.ocmload.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmload.Name = "ocmload"
        Me.ocmload.Size = New System.Drawing.Size(136, 28)
        Me.ocmload.TabIndex = 329
        Me.ocmload.Text = "Load Data"
        '
        'ogrpDetail
        '
        Me.ogrpDetail.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogrpDetail.Controls.Add(Me.ogbmainprocbutton)
        Me.ogrpDetail.Controls.Add(Me.ogcDetail)
        Me.ogrpDetail.Location = New System.Drawing.Point(1, 156)
        Me.ogrpDetail.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogrpDetail.Name = "ogrpDetail"
        Me.ogrpDetail.Size = New System.Drawing.Size(790, 428)
        Me.ogrpDetail.TabIndex = 2
        Me.ogrpDetail.Text = "Detail"
        '
        'ogcDetail
        '
        Me.ogcDetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogcDetail.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcDetail.Location = New System.Drawing.Point(2, 25)
        Me.ogcDetail.MainView = Me.ogvDetail
        Me.ogcDetail.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcDetail.Name = "ogcDetail"
        Me.ogcDetail.Size = New System.Drawing.Size(786, 401)
        Me.ogcDetail.TabIndex = 0
        Me.ogcDetail.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvDetail})
        '
        'ogvDetail
        '
        Me.ogvDetail.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.cFTComputerName, Me.cFTUnitSectCode, Me.FTUnitSectCodeTo, Me.CFNLCDType, Me.FNLCDTypeName, Me.CFTUserWindow})
        Me.ogvDetail.GridControl = Me.ogcDetail
        Me.ogvDetail.Name = "ogvDetail"
        Me.ogvDetail.OptionsView.ColumnAutoWidth = False
        '
        'cFTComputerName
        '
        Me.cFTComputerName.Caption = "FTComputerName"
        Me.cFTComputerName.FieldName = "FTComputerName"
        Me.cFTComputerName.Name = "cFTComputerName"
        Me.cFTComputerName.OptionsColumn.AllowEdit = False
        Me.cFTComputerName.Visible = True
        Me.cFTComputerName.VisibleIndex = 0
        Me.cFTComputerName.Width = 180
        '
        'cFTUnitSectCode
        '
        Me.cFTUnitSectCode.Caption = "FTUnitSectCode"
        Me.cFTUnitSectCode.FieldName = "FTUnitSectCode"
        Me.cFTUnitSectCode.Name = "cFTUnitSectCode"
        Me.cFTUnitSectCode.OptionsColumn.AllowEdit = False
        Me.cFTUnitSectCode.Visible = True
        Me.cFTUnitSectCode.VisibleIndex = 1
        Me.cFTUnitSectCode.Width = 122
        '
        'FTUnitSectCodeTo
        '
        Me.FTUnitSectCodeTo.Caption = "FTUnitSectCodeTo"
        Me.FTUnitSectCodeTo.FieldName = "FTUnitSectCodeTo"
        Me.FTUnitSectCodeTo.Name = "FTUnitSectCodeTo"
        Me.FTUnitSectCodeTo.OptionsColumn.AllowEdit = False
        Me.FTUnitSectCodeTo.Visible = True
        Me.FTUnitSectCodeTo.VisibleIndex = 2
        Me.FTUnitSectCodeTo.Width = 151
        '
        'CFNLCDType
        '
        Me.CFNLCDType.Caption = "FNLCDType"
        Me.CFNLCDType.FieldName = "FNLCDType"
        Me.CFNLCDType.Name = "CFNLCDType"
        '
        'FNLCDTypeName
        '
        Me.FNLCDTypeName.Caption = "LCD Type"
        Me.FNLCDTypeName.FieldName = "FNLCDTypeName"
        Me.FNLCDTypeName.Name = "FNLCDTypeName"
        Me.FNLCDTypeName.OptionsColumn.AllowEdit = False
        Me.FNLCDTypeName.OptionsColumn.AllowMove = False
        Me.FNLCDTypeName.OptionsColumn.AllowShowHide = False
        Me.FNLCDTypeName.OptionsColumn.ReadOnly = True
        Me.FNLCDTypeName.Visible = True
        Me.FNLCDTypeName.VisibleIndex = 3
        Me.FNLCDTypeName.Width = 183
        '
        'CFTUserWindow
        '
        Me.CFTUserWindow.Caption = "User Window"
        Me.CFTUserWindow.FieldName = "FTUserWindow"
        Me.CFTUserWindow.Name = "CFTUserWindow"
        Me.CFTUserWindow.OptionsColumn.AllowEdit = False
        Me.CFTUserWindow.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
        Me.CFTUserWindow.OptionsColumn.ReadOnly = True
        Me.CFTUserWindow.Visible = True
        Me.CFTUserWindow.VisibleIndex = 4
        Me.CFTUserWindow.Width = 92
        '
        'wConfigCom
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(790, 583)
        Me.Controls.Add(Me.ogrpDetail)
        Me.Controls.Add(Me.ogrpDocument)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "wConfigCom"
        Me.Text = "wConfigCom"
        CType(Me.ogrpDocument, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogrpDocument.ResumeLayout(False)
        CType(Me.FTUserWindow.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNLCDType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysUnitSectIdTo_None.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysUnitSectId_None.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysUnitSectId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysUnitSectIdTo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTComputerName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbmainprocbutton.ResumeLayout(False)
        CType(Me.ogrpDetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogrpDetail.ResumeLayout(False)
        CType(Me.ogcDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvDetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogrpDocument As DevExpress.XtraEditors.GroupControl
    Friend WithEvents FTComputerName As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents FTComputerName_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ogbmainprocbutton As DevExpress.XtraEditors.PanelControl
    Friend WithEvents ocmdelete As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmsave As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmsavelayout As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmclear As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmload As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FNHSysUnitSectIdTo_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysUnitSectId_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysUnitSectIdTo_None As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FNHSysUnitSectId_None As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FNHSysUnitSectId As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FNHSysUnitSectIdTo As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents ogrpDetail As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ogcDetail As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvDetail As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents cFTComputerName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTUnitSectCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTUnitSectCodeTo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNLCDType_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNLCDType As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents CFNLCDType As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNLCDTypeName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTUserWindow As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FTUserWindow_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents CFTUserWindow As DevExpress.XtraGrid.Columns.GridColumn
End Class

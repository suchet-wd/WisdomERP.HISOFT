<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wConfigTarget
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
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.oGrpDocument = New DevExpress.XtraEditors.GroupControl()
        Me.FNTargetPerHour_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNTargetPerHour = New DevExpress.XtraEditors.CalcEdit()
        Me.FTWorkTime_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTWorkTime = New DevExpress.XtraEditors.TimeEdit()
        Me.FNPercent = New DevExpress.XtraEditors.CalcEdit()
        Me.FNTarget = New DevExpress.XtraEditors.CalcEdit()
        Me.FNHSysUnitSectId = New DevExpress.XtraEditors.ButtonEdit()
        Me.FNHSysUnitSectId_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FDEDate = New DevExpress.XtraEditors.DateEdit()
        Me.FDSDate = New DevExpress.XtraEditors.DateEdit()
        Me.FDEDate_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNPercent_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNTargetPerDay_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FDSDate_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.oGrpDetail = New DevExpress.XtraEditors.GroupControl()
        Me.ogbmainprocbutton = New DevExpress.XtraEditors.PanelControl()
        Me.ocmdelete = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmsave = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmsavelayout = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmclear = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmload = New DevExpress.XtraEditors.SimpleButton()
        Me.ogcDetail = New DevExpress.XtraGrid.GridControl()
        Me.ogvDetail = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.cFDSDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFDEDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNHSysUnitSectId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNTarget = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNPercent = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTUnitSectCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTUnitSectName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNTargetPlane = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gFTWorkTime = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFNTargetPerHour = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNMoneyPackage = New DevExpress.XtraEditors.CalcEdit()
        Me.FNMoneyPackage_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.CFNMoneyPackage = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNPercentPackage = New DevExpress.XtraEditors.CalcEdit()
        Me.FNPercentPackage_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.CFNPercentPackage = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.oGrpDocument, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.oGrpDocument.SuspendLayout()
        CType(Me.FNTargetPerHour.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTWorkTime.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNPercent.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNTarget.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysUnitSectId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FDEDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FDEDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FDSDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FDSDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.oGrpDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.oGrpDetail.SuspendLayout()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbmainprocbutton.SuspendLayout()
        CType(Me.ogcDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNMoneyPackage.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNPercentPackage.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'oGrpDocument
        '
        Me.oGrpDocument.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.oGrpDocument.Controls.Add(Me.FNPercentPackage)
        Me.oGrpDocument.Controls.Add(Me.FNPercentPackage_lbl)
        Me.oGrpDocument.Controls.Add(Me.FNMoneyPackage)
        Me.oGrpDocument.Controls.Add(Me.FNMoneyPackage_lbl)
        Me.oGrpDocument.Controls.Add(Me.FNTargetPerHour_lbl)
        Me.oGrpDocument.Controls.Add(Me.FNTargetPerHour)
        Me.oGrpDocument.Controls.Add(Me.FTWorkTime_lbl)
        Me.oGrpDocument.Controls.Add(Me.FTWorkTime)
        Me.oGrpDocument.Controls.Add(Me.FNPercent)
        Me.oGrpDocument.Controls.Add(Me.FNTarget)
        Me.oGrpDocument.Controls.Add(Me.FNHSysUnitSectId)
        Me.oGrpDocument.Controls.Add(Me.FNHSysUnitSectId_lbl)
        Me.oGrpDocument.Controls.Add(Me.FDEDate)
        Me.oGrpDocument.Controls.Add(Me.FDSDate)
        Me.oGrpDocument.Controls.Add(Me.FDEDate_lbl)
        Me.oGrpDocument.Controls.Add(Me.FNPercent_lbl)
        Me.oGrpDocument.Controls.Add(Me.FNTargetPerDay_lbl)
        Me.oGrpDocument.Controls.Add(Me.FDSDate_lbl)
        Me.oGrpDocument.Location = New System.Drawing.Point(1, 1)
        Me.oGrpDocument.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.oGrpDocument.Name = "oGrpDocument"
        Me.oGrpDocument.Size = New System.Drawing.Size(1063, 152)
        Me.oGrpDocument.TabIndex = 0
        Me.oGrpDocument.Text = "Document"
        '
        'FNTargetPerHour_lbl
        '
        Me.FNTargetPerHour_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNTargetPerHour_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNTargetPerHour_lbl.Location = New System.Drawing.Point(6, 87)
        Me.FNTargetPerHour_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNTargetPerHour_lbl.Name = "FNTargetPerHour_lbl"
        Me.FNTargetPerHour_lbl.Size = New System.Drawing.Size(163, 22)
        Me.FNTargetPerHour_lbl.TabIndex = 13
        Me.FNTargetPerHour_lbl.Tag = "2|"
        Me.FNTargetPerHour_lbl.Text = "Target / Hour"
        '
        'FNTargetPerHour
        '
        Me.FNTargetPerHour.Location = New System.Drawing.Point(175, 88)
        Me.FNTargetPerHour.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNTargetPerHour.Name = "FNTargetPerHour"
        Me.FNTargetPerHour.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FNTargetPerHour.Properties.DisplayFormat.FormatString = "{0:n0}"
        Me.FNTargetPerHour.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNTargetPerHour.Properties.EditFormat.FormatString = "{0:n0}"
        Me.FNTargetPerHour.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNTargetPerHour.Properties.Precision = 0
        Me.FNTargetPerHour.Size = New System.Drawing.Size(111, 22)
        Me.FNTargetPerHour.TabIndex = 3
        Me.FNTargetPerHour.Tag = "2|"
        '
        'FTWorkTime_lbl
        '
        Me.FTWorkTime_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTWorkTime_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTWorkTime_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTWorkTime_lbl.Location = New System.Drawing.Point(288, 88)
        Me.FTWorkTime_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTWorkTime_lbl.Name = "FTWorkTime_lbl"
        Me.FTWorkTime_lbl.Size = New System.Drawing.Size(145, 22)
        Me.FTWorkTime_lbl.TabIndex = 11
        Me.FTWorkTime_lbl.Tag = "2|"
        Me.FTWorkTime_lbl.Text = "ชม. ทำงาน/วัน"
        '
        'FTWorkTime
        '
        Me.FTWorkTime.EditValue = New Date(2015, 4, 25, 0, 0, 0, 0)
        Me.FTWorkTime.Location = New System.Drawing.Point(439, 88)
        Me.FTWorkTime.Name = "FTWorkTime"
        Me.FTWorkTime.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, True, False, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject3, "", Nothing, Nothing, True)})
        Me.FTWorkTime.Properties.DisplayFormat.FormatString = "HH:mm"
        Me.FTWorkTime.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTWorkTime.Properties.EditFormat.FormatString = "HH:mm"
        Me.FTWorkTime.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTWorkTime.Properties.Mask.EditMask = "HH:mm"
        Me.FTWorkTime.Size = New System.Drawing.Size(63, 22)
        Me.FTWorkTime.TabIndex = 4
        '
        'FNPercent
        '
        Me.FNPercent.Location = New System.Drawing.Point(175, 116)
        Me.FNPercent.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNPercent.Name = "FNPercent"
        Me.FNPercent.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FNPercent.Properties.Precision = 2
        Me.FNPercent.Size = New System.Drawing.Size(175, 22)
        Me.FNPercent.TabIndex = 6
        Me.FNPercent.Tag = ""
        '
        'FNTarget
        '
        Me.FNTarget.Location = New System.Drawing.Point(673, 88)
        Me.FNTarget.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNTarget.Name = "FNTarget"
        Me.FNTarget.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNTarget.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNTarget.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNTarget.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNTarget.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FNTarget.Properties.DisplayFormat.FormatString = "{0:n0}"
        Me.FNTarget.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNTarget.Properties.EditFormat.FormatString = "{0:n0}"
        Me.FNTarget.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNTarget.Properties.Precision = 0
        Me.FNTarget.Size = New System.Drawing.Size(129, 22)
        Me.FNTarget.TabIndex = 5
        Me.FNTarget.Tag = "2|"
        '
        'FNHSysUnitSectId
        '
        Me.FNHSysUnitSectId.Location = New System.Drawing.Point(176, 30)
        Me.FNHSysUnitSectId.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysUnitSectId.Name = "FNHSysUnitSectId"
        Me.FNHSysUnitSectId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", "165", Nothing, True)})
        Me.FNHSysUnitSectId.Size = New System.Drawing.Size(181, 22)
        Me.FNHSysUnitSectId.TabIndex = 0
        Me.FNHSysUnitSectId.Tag = "2|"
        '
        'FNHSysUnitSectId_lbl
        '
        Me.FNHSysUnitSectId_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysUnitSectId_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysUnitSectId_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysUnitSectId_lbl.Location = New System.Drawing.Point(13, 30)
        Me.FNHSysUnitSectId_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysUnitSectId_lbl.Name = "FNHSysUnitSectId_lbl"
        Me.FNHSysUnitSectId_lbl.Size = New System.Drawing.Size(156, 25)
        Me.FNHSysUnitSectId_lbl.TabIndex = 9
        Me.FNHSysUnitSectId_lbl.Text = "FNHSysUnitSectId :"
        '
        'FDEDate
        '
        Me.FDEDate.EditValue = Nothing
        Me.FDEDate.Location = New System.Drawing.Point(519, 62)
        Me.FDEDate.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FDEDate.Name = "FDEDate"
        Me.FDEDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FDEDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FDEDate.Size = New System.Drawing.Size(175, 22)
        Me.FDEDate.TabIndex = 2
        Me.FDEDate.Tag = "2|"
        '
        'FDSDate
        '
        Me.FDSDate.EditValue = Nothing
        Me.FDSDate.Location = New System.Drawing.Point(176, 62)
        Me.FDSDate.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FDSDate.Name = "FDSDate"
        Me.FDSDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FDSDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FDSDate.Size = New System.Drawing.Size(181, 22)
        Me.FDSDate.TabIndex = 1
        Me.FDSDate.Tag = "2|"
        '
        'FDEDate_lbl
        '
        Me.FDEDate_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FDEDate_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FDEDate_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FDEDate_lbl.Location = New System.Drawing.Point(367, 63)
        Me.FDEDate_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FDEDate_lbl.Name = "FDEDate_lbl"
        Me.FDEDate_lbl.Size = New System.Drawing.Size(145, 22)
        Me.FDEDate_lbl.TabIndex = 6
        Me.FDEDate_lbl.Tag = "2|"
        Me.FDEDate_lbl.Text = "FDEDate"
        '
        'FNPercent_lbl
        '
        Me.FNPercent_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNPercent_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNPercent_lbl.Location = New System.Drawing.Point(20, 116)
        Me.FNPercent_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNPercent_lbl.Name = "FNPercent_lbl"
        Me.FNPercent_lbl.Size = New System.Drawing.Size(148, 22)
        Me.FNPercent_lbl.TabIndex = 6
        Me.FNPercent_lbl.Tag = "2|"
        Me.FNPercent_lbl.Text = "FNPercent"
        '
        'FNTargetPerDay_lbl
        '
        Me.FNTargetPerDay_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNTargetPerDay_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNTargetPerDay_lbl.Location = New System.Drawing.Point(504, 87)
        Me.FNTargetPerDay_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNTargetPerDay_lbl.Name = "FNTargetPerDay_lbl"
        Me.FNTargetPerDay_lbl.Size = New System.Drawing.Size(163, 22)
        Me.FNTargetPerDay_lbl.TabIndex = 6
        Me.FNTargetPerDay_lbl.Tag = "2|"
        Me.FNTargetPerDay_lbl.Text = "Target / Day"
        '
        'FDSDate_lbl
        '
        Me.FDSDate_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FDSDate_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FDSDate_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FDSDate_lbl.Location = New System.Drawing.Point(6, 62)
        Me.FDSDate_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FDSDate_lbl.Name = "FDSDate_lbl"
        Me.FDSDate_lbl.Size = New System.Drawing.Size(163, 22)
        Me.FDSDate_lbl.TabIndex = 6
        Me.FDSDate_lbl.Tag = "2|"
        Me.FDSDate_lbl.Text = "FDSDate"
        '
        'oGrpDetail
        '
        Me.oGrpDetail.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.oGrpDetail.Controls.Add(Me.ogbmainprocbutton)
        Me.oGrpDetail.Controls.Add(Me.ogcDetail)
        Me.oGrpDetail.Location = New System.Drawing.Point(1, 161)
        Me.oGrpDetail.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.oGrpDetail.Name = "oGrpDetail"
        Me.oGrpDetail.Size = New System.Drawing.Size(1063, 444)
        Me.oGrpDetail.TabIndex = 1
        Me.oGrpDetail.Text = "Detail"
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
        Me.ogbmainprocbutton.Location = New System.Drawing.Point(50, 204)
        Me.ogbmainprocbutton.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbmainprocbutton.Name = "ogbmainprocbutton"
        Me.ogbmainprocbutton.Size = New System.Drawing.Size(961, 58)
        Me.ogbmainprocbutton.TabIndex = 389
        Me.ogbmainprocbutton.Tag = "2|"
        '
        'ocmdelete
        '
        Me.ocmdelete.Location = New System.Drawing.Point(685, 12)
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
        Me.ocmsave.Location = New System.Drawing.Point(594, 10)
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
        Me.ocmsavelayout.Location = New System.Drawing.Point(433, 15)
        Me.ocmsavelayout.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmsavelayout.Name = "ocmsavelayout"
        Me.ocmsavelayout.Size = New System.Drawing.Size(136, 28)
        Me.ocmsavelayout.TabIndex = 332
        Me.ocmsavelayout.Text = "savelayoutgrid"
        '
        'ocmexit
        '
        Me.ocmexit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmexit.Location = New System.Drawing.Point(829, 14)
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
        'ogcDetail
        '
        Me.ogcDetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogcDetail.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcDetail.Location = New System.Drawing.Point(2, 24)
        Me.ogcDetail.MainView = Me.ogvDetail
        Me.ogcDetail.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcDetail.Name = "ogcDetail"
        Me.ogcDetail.Size = New System.Drawing.Size(1059, 418)
        Me.ogcDetail.TabIndex = 0
        Me.ogcDetail.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvDetail})
        '
        'ogvDetail
        '
        Me.ogvDetail.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.cFDSDate, Me.cFDEDate, Me.cFNHSysUnitSectId, Me.cFNTarget, Me.cFNPercent, Me.cFTUnitSectCode, Me.cFTUnitSectName, Me.cFNTargetPlane, Me.gFTWorkTime, Me.CFNTargetPerHour, Me.CFNPercentPackage, Me.CFNMoneyPackage})
        Me.ogvDetail.GridControl = Me.ogcDetail
        Me.ogvDetail.Name = "ogvDetail"
        Me.ogvDetail.OptionsView.ColumnAutoWidth = False
        Me.ogvDetail.OptionsView.ShowGroupPanel = False
        '
        'cFDSDate
        '
        Me.cFDSDate.Caption = "FDSDate"
        Me.cFDSDate.FieldName = "FDSDate"
        Me.cFDSDate.Name = "cFDSDate"
        Me.cFDSDate.OptionsColumn.AllowEdit = False
        Me.cFDSDate.Visible = True
        Me.cFDSDate.VisibleIndex = 2
        Me.cFDSDate.Width = 83
        '
        'cFDEDate
        '
        Me.cFDEDate.Caption = "FDEDate"
        Me.cFDEDate.FieldName = "FDEDate"
        Me.cFDEDate.Name = "cFDEDate"
        Me.cFDEDate.OptionsColumn.AllowEdit = False
        Me.cFDEDate.Visible = True
        Me.cFDEDate.VisibleIndex = 3
        Me.cFDEDate.Width = 82
        '
        'cFNHSysUnitSectId
        '
        Me.cFNHSysUnitSectId.Caption = "FNHSysUnitSectId"
        Me.cFNHSysUnitSectId.FieldName = "FNHSysUnitSectId"
        Me.cFNHSysUnitSectId.Name = "cFNHSysUnitSectId"
        '
        'cFNTarget
        '
        Me.cFNTarget.Caption = "FNTarget"
        Me.cFNTarget.FieldName = "FNTarget"
        Me.cFNTarget.Name = "cFNTarget"
        Me.cFNTarget.OptionsColumn.AllowEdit = False
        Me.cFNTarget.Visible = True
        Me.cFNTarget.VisibleIndex = 6
        Me.cFNTarget.Width = 99
        '
        'cFNPercent
        '
        Me.cFNPercent.Caption = "FNPercent"
        Me.cFNPercent.FieldName = "FNPercent"
        Me.cFNPercent.Name = "cFNPercent"
        Me.cFNPercent.OptionsColumn.AllowEdit = False
        Me.cFNPercent.Visible = True
        Me.cFNPercent.VisibleIndex = 7
        Me.cFNPercent.Width = 95
        '
        'cFTUnitSectCode
        '
        Me.cFTUnitSectCode.Caption = "FTUnitSectCode"
        Me.cFTUnitSectCode.FieldName = "FTUnitSectCode"
        Me.cFTUnitSectCode.Name = "cFTUnitSectCode"
        Me.cFTUnitSectCode.OptionsColumn.AllowEdit = False
        Me.cFTUnitSectCode.Visible = True
        Me.cFTUnitSectCode.VisibleIndex = 0
        '
        'cFTUnitSectName
        '
        Me.cFTUnitSectName.Caption = "FTUnitSectName"
        Me.cFTUnitSectName.FieldName = "FTUnitSectName"
        Me.cFTUnitSectName.Name = "cFTUnitSectName"
        Me.cFTUnitSectName.OptionsColumn.AllowEdit = False
        Me.cFTUnitSectName.Visible = True
        Me.cFTUnitSectName.VisibleIndex = 1
        Me.cFTUnitSectName.Width = 101
        '
        'cFNTargetPlane
        '
        Me.cFNTargetPlane.Caption = "FNTargetPlane"
        Me.cFNTargetPlane.FieldName = "FNTargetPlane"
        Me.cFNTargetPlane.Name = "cFNTargetPlane"
        Me.cFNTargetPlane.OptionsColumn.AllowEdit = False
        Me.cFNTargetPlane.Visible = True
        Me.cFNTargetPlane.VisibleIndex = 8
        Me.cFNTargetPlane.Width = 104
        '
        'gFTWorkTime
        '
        Me.gFTWorkTime.Caption = "ชม.ทำงาน /วัน"
        Me.gFTWorkTime.FieldName = "FTWorkTime"
        Me.gFTWorkTime.Name = "gFTWorkTime"
        Me.gFTWorkTime.OptionsColumn.AllowEdit = False
        Me.gFTWorkTime.OptionsColumn.ReadOnly = True
        Me.gFTWorkTime.Visible = True
        Me.gFTWorkTime.VisibleIndex = 4
        '
        'CFNTargetPerHour
        '
        Me.CFNTargetPerHour.Caption = "Target/Hour"
        Me.CFNTargetPerHour.DisplayFormat.FormatString = "{0:n0}"
        Me.CFNTargetPerHour.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CFNTargetPerHour.FieldName = "FNTargetPerHour"
        Me.CFNTargetPerHour.Name = "CFNTargetPerHour"
        Me.CFNTargetPerHour.OptionsColumn.AllowEdit = False
        Me.CFNTargetPerHour.OptionsColumn.ReadOnly = True
        Me.CFNTargetPerHour.Visible = True
        Me.CFNTargetPerHour.VisibleIndex = 5
        Me.CFNTargetPerHour.Width = 87
        '
        'FNMoneyPackage
        '
        Me.FNMoneyPackage.Location = New System.Drawing.Point(774, 114)
        Me.FNMoneyPackage.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNMoneyPackage.Name = "FNMoneyPackage"
        Me.FNMoneyPackage.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FNMoneyPackage.Properties.Precision = 2
        Me.FNMoneyPackage.Size = New System.Drawing.Size(113, 22)
        Me.FNMoneyPackage.TabIndex = 8
        Me.FNMoneyPackage.Tag = ""
        '
        'FNMoneyPackage_lbl
        '
        Me.FNMoneyPackage_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNMoneyPackage_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNMoneyPackage_lbl.Location = New System.Drawing.Point(619, 114)
        Me.FNMoneyPackage_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNMoneyPackage_lbl.Name = "FNMoneyPackage_lbl"
        Me.FNMoneyPackage_lbl.Size = New System.Drawing.Size(148, 22)
        Me.FNMoneyPackage_lbl.TabIndex = 15
        Me.FNMoneyPackage_lbl.Tag = "2|"
        Me.FNMoneyPackage_lbl.Text = "Money Package"
        '
        'CFNMoneyPackage
        '
        Me.CFNMoneyPackage.Caption = "Money Package"
        Me.CFNMoneyPackage.DisplayFormat.FormatString = "{0:n2}"
        Me.CFNMoneyPackage.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CFNMoneyPackage.FieldName = "FNMoneyPackage"
        Me.CFNMoneyPackage.Name = "CFNMoneyPackage"
        Me.CFNMoneyPackage.OptionsColumn.AllowEdit = False
        Me.CFNMoneyPackage.OptionsColumn.ReadOnly = True
        Me.CFNMoneyPackage.Visible = True
        Me.CFNMoneyPackage.VisibleIndex = 10
        Me.CFNMoneyPackage.Width = 100
        '
        'FNPercentPackage
        '
        Me.FNPercentPackage.Location = New System.Drawing.Point(506, 116)
        Me.FNPercentPackage.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNPercentPackage.Name = "FNPercentPackage"
        Me.FNPercentPackage.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FNPercentPackage.Properties.Precision = 2
        Me.FNPercentPackage.Size = New System.Drawing.Size(113, 22)
        Me.FNPercentPackage.TabIndex = 7
        Me.FNPercentPackage.Tag = ""
        '
        'FNPercentPackage_lbl
        '
        Me.FNPercentPackage_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNPercentPackage_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNPercentPackage_lbl.Location = New System.Drawing.Point(351, 116)
        Me.FNPercentPackage_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNPercentPackage_lbl.Name = "FNPercentPackage_lbl"
        Me.FNPercentPackage_lbl.Size = New System.Drawing.Size(148, 22)
        Me.FNPercentPackage_lbl.TabIndex = 17
        Me.FNPercentPackage_lbl.Tag = "2|"
        Me.FNPercentPackage_lbl.Text = "Percent Package"
        '
        'CFNPercentPackage
        '
        Me.CFNPercentPackage.Caption = "Percent Package"
        Me.CFNPercentPackage.DisplayFormat.FormatString = "{0:n2}"
        Me.CFNPercentPackage.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CFNPercentPackage.FieldName = "FNPercentPackage"
        Me.CFNPercentPackage.Name = "CFNPercentPackage"
        Me.CFNPercentPackage.OptionsColumn.AllowEdit = False
        Me.CFNPercentPackage.OptionsColumn.ReadOnly = True
        Me.CFNPercentPackage.Visible = True
        Me.CFNPercentPackage.VisibleIndex = 9
        Me.CFNPercentPackage.Width = 100
        '
        'wConfigTarget
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1065, 606)
        Me.Controls.Add(Me.oGrpDetail)
        Me.Controls.Add(Me.oGrpDocument)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "wConfigTarget"
        Me.Text = "wConfigTarget"
        CType(Me.oGrpDocument, System.ComponentModel.ISupportInitialize).EndInit()
        Me.oGrpDocument.ResumeLayout(False)
        CType(Me.FNTargetPerHour.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTWorkTime.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNPercent.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNTarget.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysUnitSectId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FDEDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FDEDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FDSDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FDSDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.oGrpDetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.oGrpDetail.ResumeLayout(False)
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbmainprocbutton.ResumeLayout(False)
        CType(Me.ogcDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNMoneyPackage.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNPercentPackage.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents oGrpDocument As DevExpress.XtraEditors.GroupControl
    Friend WithEvents FDEDate_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FDSDate_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FDSDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents FNPercent As DevExpress.XtraEditors.CalcEdit
    Friend WithEvents FNTarget As DevExpress.XtraEditors.CalcEdit
    Friend WithEvents FNHSysUnitSectId As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FNHSysUnitSectId_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FDEDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents FNPercent_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNTargetPerDay_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents oGrpDetail As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ogcDetail As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvDetail As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents cFDSDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFDEDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNHSysUnitSectId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNTarget As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNPercent As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTUnitSectCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTUnitSectName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNTargetPlane As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ogbmainprocbutton As DevExpress.XtraEditors.PanelControl
    Friend WithEvents ocmdelete As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmsave As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmsavelayout As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmclear As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmload As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FTWorkTime_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTWorkTime As DevExpress.XtraEditors.TimeEdit
    Friend WithEvents gFTWorkTime As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNTargetPerHour_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNTargetPerHour As DevExpress.XtraEditors.CalcEdit
    Friend WithEvents CFNTargetPerHour As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNMoneyPackage As DevExpress.XtraEditors.CalcEdit
    Friend WithEvents FNMoneyPackage_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents CFNMoneyPackage As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNPercentPackage As DevExpress.XtraEditors.CalcEdit
    Friend WithEvents FNPercentPackage_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents CFNPercentPackage As DevExpress.XtraGrid.Columns.GridColumn
End Class

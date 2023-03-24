<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wCfgHRMEmpTypeWeekly
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
        Me.ogbdetail = New DevExpress.XtraEditors.GroupControl()
        Me.ogbmainprocbutton = New DevExpress.XtraEditors.PanelControl()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmsave = New DevExpress.XtraEditors.SimpleButton()
        Me.XtraScrollableControl1 = New DevExpress.XtraEditors.XtraScrollableControl()
        Me.DateNavigator1 = New DevExpress.XtraScheduler.DateNavigator()
        Me.FNYear_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNYear = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.FNHSysEmpTypeId = New DevExpress.XtraEditors.ButtonEdit()
        Me.FNHSysEmpTypeId_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysEmpTypeId_None = New DevExpress.XtraEditors.TextEdit()
        Me.FNHSysCmpId_None = New DevExpress.XtraEditors.TextEdit()
        Me.FNHSysCmpId_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysCmpId = New DevExpress.XtraEditors.ButtonEdit()
        CType(Me.ogbdetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbdetail.SuspendLayout()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbmainprocbutton.SuspendLayout()
        Me.XtraScrollableControl1.SuspendLayout()
        CType(Me.DateNavigator1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DateNavigator1.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNYear.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysEmpTypeId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysEmpTypeId_None.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysCmpId_None.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysCmpId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ogbdetail
        '
        Me.ogbdetail.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbdetail.Controls.Add(Me.ogbmainprocbutton)
        Me.ogbdetail.Controls.Add(Me.XtraScrollableControl1)
        Me.ogbdetail.Location = New System.Drawing.Point(0, 58)
        Me.ogbdetail.Name = "ogbdetail"
        Me.ogbdetail.Size = New System.Drawing.Size(886, 514)
        Me.ogbdetail.TabIndex = 13
        Me.ogbdetail.Tag = "2|"
        Me.ogbdetail.Text = "Config Payment"
        '
        'ogbmainprocbutton
        '
        Me.ogbmainprocbutton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmexit)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmsave)
        Me.ogbmainprocbutton.Location = New System.Drawing.Point(67, 189)
        Me.ogbmainprocbutton.Name = "ogbmainprocbutton"
        Me.ogbmainprocbutton.Size = New System.Drawing.Size(716, 68)
        Me.ogbmainprocbutton.TabIndex = 138
        Me.ogbmainprocbutton.Tag = "2|"
        '
        'ocmexit
        '
        Me.ocmexit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmexit.Location = New System.Drawing.Point(574, 11)
        Me.ocmexit.Name = "ocmexit"
        Me.ocmexit.Size = New System.Drawing.Size(95, 25)
        Me.ocmexit.TabIndex = 96
        Me.ocmexit.TabStop = False
        Me.ocmexit.Tag = "2|"
        Me.ocmexit.Text = "EXIT"
        '
        'ocmsave
        '
        Me.ocmsave.Location = New System.Drawing.Point(51, 11)
        Me.ocmsave.Name = "ocmsave"
        Me.ocmsave.Size = New System.Drawing.Size(95, 25)
        Me.ocmsave.TabIndex = 93
        Me.ocmsave.TabStop = False
        Me.ocmsave.Tag = "2|"
        Me.ocmsave.Text = "NEW"
        '
        'XtraScrollableControl1
        '
        Me.XtraScrollableControl1.Controls.Add(Me.DateNavigator1)
        Me.XtraScrollableControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.XtraScrollableControl1.Location = New System.Drawing.Point(2, 20)
        Me.XtraScrollableControl1.Name = "XtraScrollableControl1"
        Me.XtraScrollableControl1.Size = New System.Drawing.Size(882, 492)
        Me.XtraScrollableControl1.TabIndex = 0
        '
        'DateNavigator1
        '
        Me.DateNavigator1.AllowAnimatedContentChange = True
        Me.DateNavigator1.Appearance.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.DateNavigator1.Appearance.Options.UseFont = True
        Me.DateNavigator1.CalendarAppearance.DayCellSelected.FontStyleDelta = System.Drawing.FontStyle.Bold
        Me.DateNavigator1.CalendarAppearance.DayCellSelected.ForeColor = System.Drawing.Color.Blue
        Me.DateNavigator1.CalendarAppearance.DayCellSelected.Options.UseFont = True
        Me.DateNavigator1.CalendarAppearance.DayCellSelected.Options.UseForeColor = True
        Me.DateNavigator1.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.DateNavigator1.Cursor = System.Windows.Forms.Cursors.Default
        Me.DateNavigator1.DateTime = New Date(CType(0, Long))
        Me.DateNavigator1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DateNavigator1.EditValue = New Date(CType(0, Long))
        Me.DateNavigator1.FirstDayOfWeek = System.DayOfWeek.Monday
        Me.DateNavigator1.HighlightHolidays = False
        Me.DateNavigator1.HighlightSelection = False
        Me.DateNavigator1.HighlightTodayCell = DevExpress.Utils.DefaultBoolean.[False]
        Me.DateNavigator1.HighlightTodayCellWhenSelected = False
        Me.DateNavigator1.Location = New System.Drawing.Point(0, 0)
        Me.DateNavigator1.Name = "DateNavigator1"
        Me.DateNavigator1.ShowMonthNavigationButtons = DevExpress.Utils.DefaultBoolean.[False]
        Me.DateNavigator1.ShowTodayButton = False
        Me.DateNavigator1.ShowYearNavigationButtons = DevExpress.Utils.DefaultBoolean.[False]
        Me.DateNavigator1.Size = New System.Drawing.Size(882, 492)
        Me.DateNavigator1.TabIndex = 0
        Me.DateNavigator1.VistaCalendarViewStyle = DevExpress.XtraEditors.VistaCalendarViewStyle.MonthView
        '
        'FNYear_lbl
        '
        Me.FNYear_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNYear_lbl.Appearance.Options.UseForeColor = True
        Me.FNYear_lbl.Appearance.Options.UseTextOptions = True
        Me.FNYear_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNYear_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNYear_lbl.Location = New System.Drawing.Point(29, 34)
        Me.FNYear_lbl.Name = "FNYear_lbl"
        Me.FNYear_lbl.Size = New System.Drawing.Size(107, 17)
        Me.FNYear_lbl.TabIndex = 424
        Me.FNYear_lbl.Tag = "2|"
        Me.FNYear_lbl.Text = "Year :"
        '
        'FNYear
        '
        Me.FNYear.EditValue = ""
        Me.FNYear.EnterMoveNextControl = True
        Me.FNYear.Location = New System.Drawing.Point(138, 33)
        Me.FNYear.Name = "FNYear"
        Me.FNYear.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FNYear.Properties.Appearance.Options.UseBackColor = True
        Me.FNYear.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FNYear.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FNYear.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FNYear.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FNYear.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNYear.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNYear.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNYear.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNYear.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNYear.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNYear.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNYear.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNYear.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FNYear.Properties.Tag = "FNYear"
        Me.FNYear.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.FNYear.Size = New System.Drawing.Size(112, 20)
        Me.FNYear.TabIndex = 423
        Me.FNYear.Tag = "2|"
        '
        'FNHSysEmpTypeId
        '
        Me.FNHSysEmpTypeId.Location = New System.Drawing.Point(383, 34)
        Me.FNHSysEmpTypeId.Name = "FNHSysEmpTypeId"
        Me.FNHSysEmpTypeId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject2, "", "42", Nothing, True)})
        Me.FNHSysEmpTypeId.Properties.Tag = ""
        Me.FNHSysEmpTypeId.Size = New System.Drawing.Size(112, 20)
        Me.FNHSysEmpTypeId.TabIndex = 420
        Me.FNHSysEmpTypeId.Tag = "2|"
        '
        'FNHSysEmpTypeId_lbl
        '
        Me.FNHSysEmpTypeId_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysEmpTypeId_lbl.Appearance.Options.UseForeColor = True
        Me.FNHSysEmpTypeId_lbl.Appearance.Options.UseTextOptions = True
        Me.FNHSysEmpTypeId_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysEmpTypeId_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysEmpTypeId_lbl.Location = New System.Drawing.Point(255, 34)
        Me.FNHSysEmpTypeId_lbl.Name = "FNHSysEmpTypeId_lbl"
        Me.FNHSysEmpTypeId_lbl.Size = New System.Drawing.Size(128, 18)
        Me.FNHSysEmpTypeId_lbl.TabIndex = 421
        Me.FNHSysEmpTypeId_lbl.Tag = "2|"
        Me.FNHSysEmpTypeId_lbl.Text = "Employee Type :"
        '
        'FNHSysEmpTypeId_None
        '
        Me.FNHSysEmpTypeId_None.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FNHSysEmpTypeId_None.Location = New System.Drawing.Point(497, 34)
        Me.FNHSysEmpTypeId_None.Name = "FNHSysEmpTypeId_None"
        Me.FNHSysEmpTypeId_None.Properties.Appearance.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysEmpTypeId_None.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysEmpTypeId_None.Properties.ReadOnly = True
        Me.FNHSysEmpTypeId_None.Size = New System.Drawing.Size(345, 20)
        Me.FNHSysEmpTypeId_None.TabIndex = 422
        Me.FNHSysEmpTypeId_None.Tag = "2|"
        '
        'FNHSysCmpId_None
        '
        Me.FNHSysCmpId_None.Location = New System.Drawing.Point(255, 11)
        Me.FNHSysCmpId_None.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysCmpId_None.Name = "FNHSysCmpId_None"
        Me.FNHSysCmpId_None.Properties.Appearance.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysCmpId_None.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysCmpId_None.Properties.ReadOnly = True
        Me.FNHSysCmpId_None.Size = New System.Drawing.Size(586, 20)
        Me.FNHSysCmpId_None.TabIndex = 427
        Me.FNHSysCmpId_None.Tag = ""
        '
        'FNHSysCmpId_lbl
        '
        Me.FNHSysCmpId_lbl.Appearance.Options.UseTextOptions = True
        Me.FNHSysCmpId_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysCmpId_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysCmpId_lbl.Location = New System.Drawing.Point(9, 10)
        Me.FNHSysCmpId_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysCmpId_lbl.Name = "FNHSysCmpId_lbl"
        Me.FNHSysCmpId_lbl.Size = New System.Drawing.Size(125, 21)
        Me.FNHSysCmpId_lbl.TabIndex = 426
        Me.FNHSysCmpId_lbl.Tag = "2|"
        Me.FNHSysCmpId_lbl.Text = "Company :"
        '
        'FNHSysCmpId
        '
        Me.FNHSysCmpId.Location = New System.Drawing.Point(139, 11)
        Me.FNHSysCmpId.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysCmpId.Name = "FNHSysCmpId"
        Me.FNHSysCmpId.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysCmpId.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysCmpId.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNHSysCmpId.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNHSysCmpId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, False, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject3, "", "11", Nothing, True)})
        Me.FNHSysCmpId.Properties.ReadOnly = True
        Me.FNHSysCmpId.Properties.Tag = ""
        Me.FNHSysCmpId.Size = New System.Drawing.Size(111, 20)
        Me.FNHSysCmpId.TabIndex = 425
        Me.FNHSysCmpId.Tag = ""
        '
        'wCfgHRMEmpTypeWeekly
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(886, 571)
        Me.Controls.Add(Me.FNHSysCmpId_None)
        Me.Controls.Add(Me.FNHSysCmpId_lbl)
        Me.Controls.Add(Me.FNHSysCmpId)
        Me.Controls.Add(Me.ogbdetail)
        Me.Controls.Add(Me.FNYear_lbl)
        Me.Controls.Add(Me.FNYear)
        Me.Controls.Add(Me.FNHSysEmpTypeId_lbl)
        Me.Controls.Add(Me.FNHSysEmpTypeId)
        Me.Controls.Add(Me.FNHSysEmpTypeId_None)
        Me.Name = "wCfgHRMEmpTypeWeekly"
        Me.Text = "wCfgHRMEmpTypeWeekly"
        CType(Me.ogbdetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbdetail.ResumeLayout(False)
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbmainprocbutton.ResumeLayout(False)
        Me.XtraScrollableControl1.ResumeLayout(False)
        CType(Me.DateNavigator1.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DateNavigator1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNYear.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysEmpTypeId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysEmpTypeId_None.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysCmpId_None.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysCmpId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogbdetail As DevExpress.XtraEditors.GroupControl
    Friend WithEvents FNYear_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNYear As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents FNHSysEmpTypeId As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FNHSysEmpTypeId_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysEmpTypeId_None As DevExpress.XtraEditors.TextEdit
    Friend WithEvents ogbmainprocbutton As DevExpress.XtraEditors.PanelControl
    Friend WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmsave As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents XtraScrollableControl1 As DevExpress.XtraEditors.XtraScrollableControl
    Friend WithEvents DateNavigator1 As DevExpress.XtraScheduler.DateNavigator ' HI.HR.wCfgHRMEmpTypeWeekly.MyDateNavigator 'DevExpress.XtraScheduler.DateNavigator
    Friend WithEvents FNHSysCmpId_None As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FNHSysCmpId_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysCmpId As DevExpress.XtraEditors.ButtonEdit
End Class

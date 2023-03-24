<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wInsuranceRate
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
        Me.FNInsuranceVN_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNInsuranceVN = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.FNEmployeeRate = New DevExpress.XtraEditors.CalcEdit()
        Me.FNEmployeeRate_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNEmployerRate = New DevExpress.XtraEditors.CalcEdit()
        Me.FNEmployerRate_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTStateActive = New DevExpress.XtraEditors.CheckEdit()
        Me.FNEmployeeRatePercent_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNEmployerRatePercent_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.ogbInsuranceRate = New DevExpress.XtraEditors.GroupControl()
        Me.ogbmainprocbutton = New DevExpress.XtraEditors.PanelControl()
        Me.ocmSave = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmRefresh = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmExit = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmclearclsr = New DevExpress.XtraEditors.SimpleButton()
        Me.ogdInsuranceRate = New DevExpress.XtraGrid.GridControl()
        Me.ogvInsuranceRate = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.oColFNHSysInsuranceId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.oColFNInsuranceVN = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.oColFTInsuranceDesc = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.oColFNEmployeeRate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.oColFNEmployerRate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.oColFTStateActive = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemFTStateActive = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        CType(Me.FNInsuranceVN.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNEmployeeRate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNEmployerRate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTStateActive.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogbInsuranceRate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbInsuranceRate.SuspendLayout()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbmainprocbutton.SuspendLayout()
        CType(Me.ogdInsuranceRate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvInsuranceRate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemFTStateActive, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'FNInsuranceVN_lbl
        '
        Me.FNInsuranceVN_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNInsuranceVN_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNInsuranceVN_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNInsuranceVN_lbl.Location = New System.Drawing.Point(22, 37)
        Me.FNInsuranceVN_lbl.Name = "FNInsuranceVN_lbl"
        Me.FNInsuranceVN_lbl.Size = New System.Drawing.Size(162, 17)
        Me.FNInsuranceVN_lbl.TabIndex = 426
        Me.FNInsuranceVN_lbl.Tag = "2|"
        Me.FNInsuranceVN_lbl.Text = "Insurance Type :"
        '
        'FNInsuranceVN
        '
        Me.FNInsuranceVN.EditValue = ""
        Me.FNInsuranceVN.EnterMoveNextControl = True
        Me.FNInsuranceVN.Location = New System.Drawing.Point(190, 36)
        Me.FNInsuranceVN.Name = "FNInsuranceVN"
        Me.FNInsuranceVN.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FNInsuranceVN.Properties.Appearance.Options.UseBackColor = True
        Me.FNInsuranceVN.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FNInsuranceVN.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FNInsuranceVN.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FNInsuranceVN.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FNInsuranceVN.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNInsuranceVN.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNInsuranceVN.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNInsuranceVN.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNInsuranceVN.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNInsuranceVN.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNInsuranceVN.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNInsuranceVN.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNInsuranceVN.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FNInsuranceVN.Properties.Tag = "FNInsuranceVN"
        Me.FNInsuranceVN.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.FNInsuranceVN.Size = New System.Drawing.Size(238, 20)
        Me.FNInsuranceVN.TabIndex = 425
        Me.FNInsuranceVN.Tag = "2|"
        '
        'FNEmployeeRate
        '
        Me.FNEmployeeRate.EnterMoveNextControl = True
        Me.FNEmployeeRate.Location = New System.Drawing.Point(190, 59)
        Me.FNEmployeeRate.Name = "FNEmployeeRate"
        Me.FNEmployeeRate.Properties.Appearance.Options.UseTextOptions = True
        Me.FNEmployeeRate.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNEmployeeRate.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNEmployeeRate.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNEmployeeRate.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNEmployeeRate.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNEmployeeRate.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNEmployeeRate.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNEmployeeRate.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNEmployeeRate.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNEmployeeRate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, True, False, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", Nothing, Nothing, True)})
        Me.FNEmployeeRate.Properties.DisplayFormat.FormatString = """C2"""
        Me.FNEmployeeRate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNEmployeeRate.Properties.EditFormat.FormatString = """C2"""
        Me.FNEmployeeRate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNEmployeeRate.Properties.Precision = 2
        Me.FNEmployeeRate.Size = New System.Drawing.Size(55, 20)
        Me.FNEmployeeRate.TabIndex = 522
        Me.FNEmployeeRate.Tag = "2|"
        '
        'FNEmployeeRate_lbl
        '
        Me.FNEmployeeRate_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNEmployeeRate_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNEmployeeRate_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNEmployeeRate_lbl.Location = New System.Drawing.Point(62, 59)
        Me.FNEmployeeRate_lbl.Name = "FNEmployeeRate_lbl"
        Me.FNEmployeeRate_lbl.Size = New System.Drawing.Size(122, 19)
        Me.FNEmployeeRate_lbl.TabIndex = 523
        Me.FNEmployeeRate_lbl.Tag = "2|"
        Me.FNEmployeeRate_lbl.Text = "Employee Rate  :"
        '
        'FNEmployerRate
        '
        Me.FNEmployerRate.EnterMoveNextControl = True
        Me.FNEmployerRate.Location = New System.Drawing.Point(190, 82)
        Me.FNEmployerRate.Name = "FNEmployerRate"
        Me.FNEmployerRate.Properties.Appearance.Options.UseTextOptions = True
        Me.FNEmployerRate.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNEmployerRate.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNEmployerRate.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNEmployerRate.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNEmployerRate.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNEmployerRate.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNEmployerRate.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNEmployerRate.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNEmployerRate.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNEmployerRate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, True, False, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject2, "", Nothing, Nothing, True)})
        Me.FNEmployerRate.Properties.DisplayFormat.FormatString = """C2"""
        Me.FNEmployerRate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNEmployerRate.Properties.EditFormat.FormatString = """C2"""
        Me.FNEmployerRate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNEmployerRate.Properties.Precision = 2
        Me.FNEmployerRate.Size = New System.Drawing.Size(55, 20)
        Me.FNEmployerRate.TabIndex = 524
        Me.FNEmployerRate.Tag = "2|"
        '
        'FNEmployerRate_lbl
        '
        Me.FNEmployerRate_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNEmployerRate_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNEmployerRate_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNEmployerRate_lbl.Location = New System.Drawing.Point(62, 82)
        Me.FNEmployerRate_lbl.Name = "FNEmployerRate_lbl"
        Me.FNEmployerRate_lbl.Size = New System.Drawing.Size(122, 19)
        Me.FNEmployerRate_lbl.TabIndex = 525
        Me.FNEmployerRate_lbl.Tag = "2|"
        Me.FNEmployerRate_lbl.Text = "Employer Rate  :"
        '
        'FTStateActive
        '
        Me.FTStateActive.Location = New System.Drawing.Point(434, 36)
        Me.FTStateActive.Name = "FTStateActive"
        Me.FTStateActive.Properties.Caption = "FTStateActive"
        Me.FTStateActive.Properties.Tag = "FTStateWindows"
        Me.FTStateActive.Properties.ValueChecked = "1"
        Me.FTStateActive.Properties.ValueUnchecked = "0"
        Me.FTStateActive.Size = New System.Drawing.Size(134, 19)
        Me.FTStateActive.TabIndex = 526
        Me.FTStateActive.Tag = "2|"
        '
        'FNEmployeeRatePercent_lbl
        '
        Me.FNEmployeeRatePercent_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNEmployeeRatePercent_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.FNEmployeeRatePercent_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNEmployeeRatePercent_lbl.Location = New System.Drawing.Point(251, 59)
        Me.FNEmployeeRatePercent_lbl.Name = "FNEmployeeRatePercent_lbl"
        Me.FNEmployeeRatePercent_lbl.Size = New System.Drawing.Size(188, 19)
        Me.FNEmployeeRatePercent_lbl.TabIndex = 527
        Me.FNEmployeeRatePercent_lbl.Tag = "2|"
        Me.FNEmployeeRatePercent_lbl.Text = "%"
        '
        'FNEmployerRatePercent_lbl
        '
        Me.FNEmployerRatePercent_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNEmployerRatePercent_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.FNEmployerRatePercent_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNEmployerRatePercent_lbl.Location = New System.Drawing.Point(251, 82)
        Me.FNEmployerRatePercent_lbl.Name = "FNEmployerRatePercent_lbl"
        Me.FNEmployerRatePercent_lbl.Size = New System.Drawing.Size(188, 19)
        Me.FNEmployerRatePercent_lbl.TabIndex = 528
        Me.FNEmployerRatePercent_lbl.Tag = "2|"
        Me.FNEmployerRatePercent_lbl.Text = "%"
        '
        'ogbInsuranceRate
        '
        Me.ogbInsuranceRate.AppearanceCaption.Options.UseTextOptions = True
        Me.ogbInsuranceRate.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ogbInsuranceRate.Controls.Add(Me.ogbmainprocbutton)
        Me.ogbInsuranceRate.Controls.Add(Me.ogdInsuranceRate)
        Me.ogbInsuranceRate.Controls.Add(Me.FNInsuranceVN_lbl)
        Me.ogbInsuranceRate.Controls.Add(Me.FNEmployerRatePercent_lbl)
        Me.ogbInsuranceRate.Controls.Add(Me.FNInsuranceVN)
        Me.ogbInsuranceRate.Controls.Add(Me.FNEmployeeRatePercent_lbl)
        Me.ogbInsuranceRate.Controls.Add(Me.FNEmployeeRate_lbl)
        Me.ogbInsuranceRate.Controls.Add(Me.FTStateActive)
        Me.ogbInsuranceRate.Controls.Add(Me.FNEmployeeRate)
        Me.ogbInsuranceRate.Controls.Add(Me.FNEmployerRate)
        Me.ogbInsuranceRate.Controls.Add(Me.FNEmployerRate_lbl)
        Me.ogbInsuranceRate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogbInsuranceRate.Location = New System.Drawing.Point(0, 0)
        Me.ogbInsuranceRate.Name = "ogbInsuranceRate"
        Me.ogbInsuranceRate.Size = New System.Drawing.Size(1017, 596)
        Me.ogbInsuranceRate.TabIndex = 529
        Me.ogbInsuranceRate.Text = "Insurance Rate"
        '
        'ogbmainprocbutton
        '
        Me.ogbmainprocbutton.Controls.Add(Me.ocmSave)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmRefresh)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmExit)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmclearclsr)
        Me.ogbmainprocbutton.Location = New System.Drawing.Point(562, 39)
        Me.ogbmainprocbutton.Name = "ogbmainprocbutton"
        Me.ogbmainprocbutton.Size = New System.Drawing.Size(418, 39)
        Me.ogbmainprocbutton.TabIndex = 530
        '
        'ocmSave
        '
        Me.ocmSave.Location = New System.Drawing.Point(12, 6)
        Me.ocmSave.Name = "ocmSave"
        Me.ocmSave.Size = New System.Drawing.Size(95, 25)
        Me.ocmSave.TabIndex = 0
        Me.ocmSave.TabStop = False
        Me.ocmSave.Tag = "2|"
        Me.ocmSave.Text = "SAVE"
        '
        'ocmRefresh
        '
        Me.ocmRefresh.Location = New System.Drawing.Point(214, 6)
        Me.ocmRefresh.Name = "ocmRefresh"
        Me.ocmRefresh.Size = New System.Drawing.Size(95, 25)
        Me.ocmRefresh.TabIndex = 95
        Me.ocmRefresh.TabStop = False
        Me.ocmRefresh.Tag = "2|"
        Me.ocmRefresh.Text = "REFRESH"
        '
        'ocmExit
        '
        Me.ocmExit.Location = New System.Drawing.Point(315, 6)
        Me.ocmExit.Name = "ocmExit"
        Me.ocmExit.Size = New System.Drawing.Size(95, 25)
        Me.ocmExit.TabIndex = 96
        Me.ocmExit.TabStop = False
        Me.ocmExit.Tag = "2|"
        Me.ocmExit.Text = "EXIT"
        '
        'ocmclearclsr
        '
        Me.ocmclearclsr.Location = New System.Drawing.Point(113, 6)
        Me.ocmclearclsr.Name = "ocmclearclsr"
        Me.ocmclearclsr.Size = New System.Drawing.Size(95, 25)
        Me.ocmclearclsr.TabIndex = 111
        Me.ocmclearclsr.TabStop = False
        Me.ocmclearclsr.Tag = "2|"
        Me.ocmclearclsr.Text = "CLEAR"
        '
        'ogdInsuranceRate
        '
        Me.ogdInsuranceRate.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogdInsuranceRate.Location = New System.Drawing.Point(12, 108)
        Me.ogdInsuranceRate.MainView = Me.ogvInsuranceRate
        Me.ogdInsuranceRate.Name = "ogdInsuranceRate"
        Me.ogdInsuranceRate.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemFTStateActive})
        Me.ogdInsuranceRate.Size = New System.Drawing.Size(993, 476)
        Me.ogdInsuranceRate.TabIndex = 529
        Me.ogdInsuranceRate.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvInsuranceRate})
        '
        'ogvInsuranceRate
        '
        Me.ogvInsuranceRate.Appearance.EvenRow.BackColor = System.Drawing.Color.LightSteelBlue
        Me.ogvInsuranceRate.Appearance.EvenRow.Options.UseBackColor = True
        Me.ogvInsuranceRate.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.oColFNHSysInsuranceId, Me.oColFNInsuranceVN, Me.oColFTInsuranceDesc, Me.oColFNEmployeeRate, Me.oColFNEmployerRate, Me.oColFTStateActive})
        Me.ogvInsuranceRate.GridControl = Me.ogdInsuranceRate
        Me.ogvInsuranceRate.Name = "ogvInsuranceRate"
        Me.ogvInsuranceRate.OptionsView.EnableAppearanceEvenRow = True
        Me.ogvInsuranceRate.OptionsView.ShowGroupPanel = False
        '
        'oColFNHSysInsuranceId
        '
        Me.oColFNHSysInsuranceId.Caption = "FNHSysInsuranceId"
        Me.oColFNHSysInsuranceId.FieldName = "FNHSysInsuranceId"
        Me.oColFNHSysInsuranceId.Name = "oColFNHSysInsuranceId"
        '
        'oColFNInsuranceVN
        '
        Me.oColFNInsuranceVN.Caption = "FNInsuranceVN"
        Me.oColFNInsuranceVN.FieldName = "FNInsuranceVN"
        Me.oColFNInsuranceVN.Name = "oColFNInsuranceVN"
        '
        'oColFTInsuranceDesc
        '
        Me.oColFTInsuranceDesc.Caption = "FTInsuranceDesc"
        Me.oColFTInsuranceDesc.FieldName = "FTInsuranceDesc"
        Me.oColFTInsuranceDesc.Name = "oColFTInsuranceDesc"
        Me.oColFTInsuranceDesc.OptionsColumn.AllowEdit = False
        Me.oColFTInsuranceDesc.OptionsColumn.AllowMove = False
        Me.oColFTInsuranceDesc.Visible = True
        Me.oColFTInsuranceDesc.VisibleIndex = 0
        '
        'oColFNEmployeeRate
        '
        Me.oColFNEmployeeRate.AppearanceCell.Options.UseTextOptions = True
        Me.oColFNEmployeeRate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.oColFNEmployeeRate.Caption = "FNEmployeeRate"
        Me.oColFNEmployeeRate.FieldName = "FNEmployeeRate"
        Me.oColFNEmployeeRate.Name = "oColFNEmployeeRate"
        Me.oColFNEmployeeRate.OptionsColumn.AllowEdit = False
        Me.oColFNEmployeeRate.OptionsColumn.AllowMove = False
        Me.oColFNEmployeeRate.Visible = True
        Me.oColFNEmployeeRate.VisibleIndex = 1
        '
        'oColFNEmployerRate
        '
        Me.oColFNEmployerRate.AppearanceCell.Options.UseTextOptions = True
        Me.oColFNEmployerRate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.oColFNEmployerRate.Caption = "FNEmployerRate"
        Me.oColFNEmployerRate.FieldName = "FNEmployerRate"
        Me.oColFNEmployerRate.Name = "oColFNEmployerRate"
        Me.oColFNEmployerRate.OptionsColumn.AllowEdit = False
        Me.oColFNEmployerRate.OptionsColumn.AllowMove = False
        Me.oColFNEmployerRate.Visible = True
        Me.oColFNEmployerRate.VisibleIndex = 2
        '
        'oColFTStateActive
        '
        Me.oColFTStateActive.Caption = "FTStateActive"
        Me.oColFTStateActive.ColumnEdit = Me.RepositoryItemFTStateActive
        Me.oColFTStateActive.FieldName = "FTStateActive"
        Me.oColFTStateActive.Name = "oColFTStateActive"
        Me.oColFTStateActive.OptionsColumn.AllowEdit = False
        Me.oColFTStateActive.OptionsColumn.AllowMove = False
        Me.oColFTStateActive.Visible = True
        Me.oColFTStateActive.VisibleIndex = 3
        '
        'RepositoryItemFTStateActive
        '
        Me.RepositoryItemFTStateActive.AutoHeight = False
        Me.RepositoryItemFTStateActive.Caption = "Check"
        Me.RepositoryItemFTStateActive.Name = "RepositoryItemFTStateActive"
        Me.RepositoryItemFTStateActive.ValueChecked = "1"
        Me.RepositoryItemFTStateActive.ValueUnchecked = "0"
        '
        'wInsuranceRate
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1017, 596)
        Me.Controls.Add(Me.ogbInsuranceRate)
        Me.Name = "wInsuranceRate"
        Me.Text = "wInsuranceRate"
        CType(Me.FNInsuranceVN.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNEmployeeRate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNEmployerRate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTStateActive.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogbInsuranceRate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbInsuranceRate.ResumeLayout(False)
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbmainprocbutton.ResumeLayout(False)
        CType(Me.ogdInsuranceRate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvInsuranceRate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemFTStateActive, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents FNInsuranceVN_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNInsuranceVN As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents FNEmployeeRate As DevExpress.XtraEditors.CalcEdit
    Friend WithEvents FNEmployeeRate_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNEmployerRate As DevExpress.XtraEditors.CalcEdit
    Friend WithEvents FNEmployerRate_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTStateActive As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents FNEmployeeRatePercent_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNEmployerRatePercent_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ogbInsuranceRate As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ogdInsuranceRate As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvInsuranceRate As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents ogbmainprocbutton As DevExpress.XtraEditors.PanelControl
    Friend WithEvents ocmSave As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmRefresh As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmExit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmclearclsr As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents oColFNHSysInsuranceId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents oColFNInsuranceVN As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents oColFTInsuranceDesc As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents oColFNEmployeeRate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents oColFNEmployerRate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents oColFTStateActive As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemFTStateActive As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
End Class

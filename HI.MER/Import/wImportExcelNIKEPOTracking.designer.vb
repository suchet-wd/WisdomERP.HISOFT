<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wImportExcelNIKEPOTracking
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
        Me.ogbheader = New DevExpress.XtraEditors.GroupControl()
        Me.olbcustpo = New DevExpress.XtraEditors.LabelControl()
        Me.FTCustomerPO = New DevExpress.XtraEditors.TextEdit()
        Me.FNImportOrderDataType = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.FNImportOrderDataType_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTEndGacDate = New DevExpress.XtraEditors.DateEdit()
        Me.FTEndGacDate_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTStartGacDate = New DevExpress.XtraEditors.DateEdit()
        Me.FTStartGacDate_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTEndDocDate = New DevExpress.XtraEditors.DateEdit()
        Me.FTEndDocDate_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTStartDocDate = New DevExpress.XtraEditors.DateEdit()
        Me.FTStartDocDate_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.XtraTabControl1 = New DevExpress.XtraTab.XtraTabControl()
        Me.XtraTabPage7 = New DevExpress.XtraTab.XtraTabPage()
        Me.ogcdt1 = New DevExpress.XtraGrid.GridControl()
        Me.ogvdt1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.ogbmainprocbutton = New DevExpress.XtraEditors.PanelControl()
        Me.ocmrefresh = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.FTTradigPO_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTTradigPO = New DevExpress.XtraEditors.TextEdit()
        CType(Me.ogbheader, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbheader.SuspendLayout()
        CType(Me.FTCustomerPO.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNImportOrderDataType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTEndGacDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTEndGacDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTStartGacDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTStartGacDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTEndDocDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTEndDocDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTStartDocDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTStartDocDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.XtraTabControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XtraTabControl1.SuspendLayout()
        Me.XtraTabPage7.SuspendLayout()
        CType(Me.ogcdt1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvdt1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbmainprocbutton.SuspendLayout()
        CType(Me.FTTradigPO.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ogbheader
        '
        Me.ogbheader.Controls.Add(Me.FTTradigPO_lbl)
        Me.ogbheader.Controls.Add(Me.FTTradigPO)
        Me.ogbheader.Controls.Add(Me.olbcustpo)
        Me.ogbheader.Controls.Add(Me.FTCustomerPO)
        Me.ogbheader.Controls.Add(Me.FNImportOrderDataType)
        Me.ogbheader.Controls.Add(Me.FNImportOrderDataType_lbl)
        Me.ogbheader.Controls.Add(Me.FTEndGacDate)
        Me.ogbheader.Controls.Add(Me.FTEndGacDate_lbl)
        Me.ogbheader.Controls.Add(Me.FTStartGacDate)
        Me.ogbheader.Controls.Add(Me.FTStartGacDate_lbl)
        Me.ogbheader.Controls.Add(Me.FTEndDocDate)
        Me.ogbheader.Controls.Add(Me.FTEndDocDate_lbl)
        Me.ogbheader.Controls.Add(Me.FTStartDocDate)
        Me.ogbheader.Controls.Add(Me.FTStartDocDate_lbl)
        Me.ogbheader.Dock = System.Windows.Forms.DockStyle.Top
        Me.ogbheader.Location = New System.Drawing.Point(0, 0)
        Me.ogbheader.Name = "ogbheader"
        Me.ogbheader.Size = New System.Drawing.Size(1062, 105)
        Me.ogbheader.TabIndex = 139
        '
        'olbcustpo
        '
        Me.olbcustpo.Appearance.ForeColor = System.Drawing.Color.Black
        Me.olbcustpo.Appearance.Options.UseForeColor = True
        Me.olbcustpo.Appearance.Options.UseTextOptions = True
        Me.olbcustpo.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.olbcustpo.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.olbcustpo.Location = New System.Drawing.Point(564, 26)
        Me.olbcustpo.Name = "olbcustpo"
        Me.olbcustpo.Size = New System.Drawing.Size(140, 19)
        Me.olbcustpo.TabIndex = 564
        Me.olbcustpo.Tag = "2|"
        Me.olbcustpo.Text = "Customer PO:"
        '
        'FTCustomerPO
        '
        Me.FTCustomerPO.Location = New System.Drawing.Point(713, 28)
        Me.FTCustomerPO.Name = "FTCustomerPO"
        Me.FTCustomerPO.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FTCustomerPO.Properties.Appearance.Options.UseBackColor = True
        Me.FTCustomerPO.Size = New System.Drawing.Size(206, 20)
        Me.FTCustomerPO.TabIndex = 563
        Me.FTCustomerPO.Tag = "2|"
        '
        'FNImportOrderDataType
        '
        Me.FNImportOrderDataType.EditValue = ""
        Me.FNImportOrderDataType.EnterMoveNextControl = True
        Me.FNImportOrderDataType.Location = New System.Drawing.Point(153, 74)
        Me.FNImportOrderDataType.Name = "FNImportOrderDataType"
        Me.FNImportOrderDataType.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FNImportOrderDataType.Properties.Appearance.Options.UseBackColor = True
        Me.FNImportOrderDataType.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FNImportOrderDataType.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FNImportOrderDataType.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FNImportOrderDataType.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FNImportOrderDataType.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNImportOrderDataType.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNImportOrderDataType.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNImportOrderDataType.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNImportOrderDataType.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNImportOrderDataType.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNImportOrderDataType.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNImportOrderDataType.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNImportOrderDataType.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FNImportOrderDataType.Properties.Items.AddRange(New Object() {"Recent Import Order Data", "All History Import Data"})
        Me.FNImportOrderDataType.Properties.Tag = ""
        Me.FNImportOrderDataType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.FNImportOrderDataType.Size = New System.Drawing.Size(171, 20)
        Me.FNImportOrderDataType.TabIndex = 561
        Me.FNImportOrderDataType.Tag = "2|"
        '
        'FNImportOrderDataType_lbl
        '
        Me.FNImportOrderDataType_lbl.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(31, Byte), Integer), CType(CType(53, Byte), Integer))
        Me.FNImportOrderDataType_lbl.Appearance.Options.UseForeColor = True
        Me.FNImportOrderDataType_lbl.Appearance.Options.UseTextOptions = True
        Me.FNImportOrderDataType_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNImportOrderDataType_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNImportOrderDataType_lbl.Location = New System.Drawing.Point(27, 75)
        Me.FNImportOrderDataType_lbl.Name = "FNImportOrderDataType_lbl"
        Me.FNImportOrderDataType_lbl.Size = New System.Drawing.Size(123, 18)
        Me.FNImportOrderDataType_lbl.TabIndex = 562
        Me.FNImportOrderDataType_lbl.Tag = "2|"
        Me.FNImportOrderDataType_lbl.Text = "Data Type :"
        '
        'FTEndGacDate
        '
        Me.FTEndGacDate.EditValue = Nothing
        Me.FTEndGacDate.EnterMoveNextControl = True
        Me.FTEndGacDate.Location = New System.Drawing.Point(428, 26)
        Me.FTEndGacDate.Name = "FTEndGacDate"
        Me.FTEndGacDate.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTEndGacDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FTEndGacDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.FTEndGacDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTEndGacDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTEndGacDate.Properties.NullDate = ""
        Me.FTEndGacDate.Size = New System.Drawing.Size(130, 20)
        Me.FTEndGacDate.TabIndex = 559
        Me.FTEndGacDate.Tag = "2|"
        '
        'FTEndGacDate_lbl
        '
        Me.FTEndGacDate_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTEndGacDate_lbl.Appearance.Options.UseForeColor = True
        Me.FTEndGacDate_lbl.Appearance.Options.UseTextOptions = True
        Me.FTEndGacDate_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTEndGacDate_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTEndGacDate_lbl.Location = New System.Drawing.Point(286, 25)
        Me.FTEndGacDate_lbl.Name = "FTEndGacDate_lbl"
        Me.FTEndGacDate_lbl.Size = New System.Drawing.Size(140, 19)
        Me.FTEndGacDate_lbl.TabIndex = 560
        Me.FTEndGacDate_lbl.Tag = "2|"
        Me.FTEndGacDate_lbl.Text = "End Gac Date:"
        '
        'FTStartGacDate
        '
        Me.FTStartGacDate.EditValue = Nothing
        Me.FTStartGacDate.EnterMoveNextControl = True
        Me.FTStartGacDate.Location = New System.Drawing.Point(153, 26)
        Me.FTStartGacDate.Name = "FTStartGacDate"
        Me.FTStartGacDate.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTStartGacDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FTStartGacDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.FTStartGacDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTStartGacDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTStartGacDate.Properties.NullDate = ""
        Me.FTStartGacDate.Size = New System.Drawing.Size(130, 20)
        Me.FTStartGacDate.TabIndex = 557
        Me.FTStartGacDate.Tag = "2|"
        '
        'FTStartGacDate_lbl
        '
        Me.FTStartGacDate_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTStartGacDate_lbl.Appearance.Options.UseForeColor = True
        Me.FTStartGacDate_lbl.Appearance.Options.UseTextOptions = True
        Me.FTStartGacDate_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTStartGacDate_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTStartGacDate_lbl.Location = New System.Drawing.Point(13, 25)
        Me.FTStartGacDate_lbl.Name = "FTStartGacDate_lbl"
        Me.FTStartGacDate_lbl.Size = New System.Drawing.Size(134, 19)
        Me.FTStartGacDate_lbl.TabIndex = 558
        Me.FTStartGacDate_lbl.Tag = "2|"
        Me.FTStartGacDate_lbl.Text = "Start Gac Date :"
        '
        'FTEndDocDate
        '
        Me.FTEndDocDate.EditValue = Nothing
        Me.FTEndDocDate.EnterMoveNextControl = True
        Me.FTEndDocDate.Location = New System.Drawing.Point(428, 50)
        Me.FTEndDocDate.Name = "FTEndDocDate"
        Me.FTEndDocDate.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTEndDocDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FTEndDocDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.FTEndDocDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTEndDocDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTEndDocDate.Properties.NullDate = ""
        Me.FTEndDocDate.Size = New System.Drawing.Size(130, 20)
        Me.FTEndDocDate.TabIndex = 555
        Me.FTEndDocDate.Tag = "2|"
        '
        'FTEndDocDate_lbl
        '
        Me.FTEndDocDate_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTEndDocDate_lbl.Appearance.Options.UseForeColor = True
        Me.FTEndDocDate_lbl.Appearance.Options.UseTextOptions = True
        Me.FTEndDocDate_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTEndDocDate_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTEndDocDate_lbl.Location = New System.Drawing.Point(286, 49)
        Me.FTEndDocDate_lbl.Name = "FTEndDocDate_lbl"
        Me.FTEndDocDate_lbl.Size = New System.Drawing.Size(140, 19)
        Me.FTEndDocDate_lbl.TabIndex = 556
        Me.FTEndDocDate_lbl.Tag = "2|"
        Me.FTEndDocDate_lbl.Text = "End Doc Date:"
        '
        'FTStartDocDate
        '
        Me.FTStartDocDate.EditValue = Nothing
        Me.FTStartDocDate.EnterMoveNextControl = True
        Me.FTStartDocDate.Location = New System.Drawing.Point(153, 50)
        Me.FTStartDocDate.Name = "FTStartDocDate"
        Me.FTStartDocDate.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTStartDocDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FTStartDocDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.FTStartDocDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTStartDocDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTStartDocDate.Properties.NullDate = ""
        Me.FTStartDocDate.Size = New System.Drawing.Size(130, 20)
        Me.FTStartDocDate.TabIndex = 553
        Me.FTStartDocDate.Tag = "2|"
        '
        'FTStartDocDate_lbl
        '
        Me.FTStartDocDate_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTStartDocDate_lbl.Appearance.Options.UseForeColor = True
        Me.FTStartDocDate_lbl.Appearance.Options.UseTextOptions = True
        Me.FTStartDocDate_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTStartDocDate_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTStartDocDate_lbl.Location = New System.Drawing.Point(13, 49)
        Me.FTStartDocDate_lbl.Name = "FTStartDocDate_lbl"
        Me.FTStartDocDate_lbl.Size = New System.Drawing.Size(134, 19)
        Me.FTStartDocDate_lbl.TabIndex = 554
        Me.FTStartDocDate_lbl.Tag = "2|"
        Me.FTStartDocDate_lbl.Text = "Start Doc Date:"
        '
        'XtraTabControl1
        '
        Me.XtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.XtraTabControl1.Location = New System.Drawing.Point(0, 0)
        Me.XtraTabControl1.Name = "XtraTabControl1"
        Me.XtraTabControl1.SelectedTabPage = Me.XtraTabPage7
        Me.XtraTabControl1.Size = New System.Drawing.Size(1064, 609)
        Me.XtraTabControl1.TabIndex = 396
        Me.XtraTabControl1.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.XtraTabPage7})
        '
        'XtraTabPage7
        '
        Me.XtraTabPage7.Controls.Add(Me.ogcdt1)
        Me.XtraTabPage7.Controls.Add(Me.ogbheader)
        Me.XtraTabPage7.Name = "XtraTabPage7"
        Me.XtraTabPage7.Size = New System.Drawing.Size(1062, 584)
        Me.XtraTabPage7.Text = "Orders Data Get"
        '
        'ogcdt1
        '
        Me.ogcdt1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogcdt1.Location = New System.Drawing.Point(0, 105)
        Me.ogcdt1.MainView = Me.ogvdt1
        Me.ogcdt1.Name = "ogcdt1"
        Me.ogcdt1.Size = New System.Drawing.Size(1062, 479)
        Me.ogcdt1.TabIndex = 399
        Me.ogcdt1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvdt1})
        '
        'ogvdt1
        '
        Me.ogvdt1.GridControl = Me.ogcdt1
        Me.ogvdt1.Name = "ogvdt1"
        Me.ogvdt1.OptionsBehavior.ReadOnly = True
        Me.ogvdt1.OptionsView.ColumnAutoWidth = False
        Me.ogvdt1.OptionsView.ShowGroupPanel = False
        '
        'ogbmainprocbutton
        '
        Me.ogbmainprocbutton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmrefresh)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmexit)
        Me.ogbmainprocbutton.Location = New System.Drawing.Point(27, 255)
        Me.ogbmainprocbutton.Name = "ogbmainprocbutton"
        Me.ogbmainprocbutton.Size = New System.Drawing.Size(1011, 98)
        Me.ogbmainprocbutton.TabIndex = 397
        Me.ogbmainprocbutton.Tag = "2|"
        '
        'ocmrefresh
        '
        Me.ocmrefresh.Location = New System.Drawing.Point(412, 26)
        Me.ocmrefresh.Name = "ocmrefresh"
        Me.ocmrefresh.Size = New System.Drawing.Size(95, 25)
        Me.ocmrefresh.TabIndex = 107
        Me.ocmrefresh.TabStop = False
        Me.ocmrefresh.Tag = "2|"
        Me.ocmrefresh.Text = "REFRESH"
        '
        'ocmexit
        '
        Me.ocmexit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmexit.Location = New System.Drawing.Point(936, 11)
        Me.ocmexit.Name = "ocmexit"
        Me.ocmexit.Size = New System.Drawing.Size(70, 25)
        Me.ocmexit.TabIndex = 96
        Me.ocmexit.TabStop = False
        Me.ocmexit.Tag = "2|"
        Me.ocmexit.Text = "EXIT"
        '
        'FTTradigPO_lbl
        '
        Me.FTTradigPO_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTTradigPO_lbl.Appearance.Options.UseForeColor = True
        Me.FTTradigPO_lbl.Appearance.Options.UseTextOptions = True
        Me.FTTradigPO_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTTradigPO_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTTradigPO_lbl.Location = New System.Drawing.Point(564, 54)
        Me.FTTradigPO_lbl.Name = "FTTradigPO_lbl"
        Me.FTTradigPO_lbl.Size = New System.Drawing.Size(140, 19)
        Me.FTTradigPO_lbl.TabIndex = 566
        Me.FTTradigPO_lbl.Tag = "2|"
        Me.FTTradigPO_lbl.Text = "Tradig PO:"
        '
        'FTTradigPO
        '
        Me.FTTradigPO.Location = New System.Drawing.Point(713, 54)
        Me.FTTradigPO.Name = "FTTradigPO"
        Me.FTTradigPO.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FTTradigPO.Properties.Appearance.Options.UseBackColor = True
        Me.FTTradigPO.Size = New System.Drawing.Size(206, 20)
        Me.FTTradigPO.TabIndex = 565
        Me.FTTradigPO.Tag = "2|"
        '
        'wImportExcelNIKEPOTracking
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(1064, 609)
        Me.Controls.Add(Me.ogbmainprocbutton)
        Me.Controls.Add(Me.XtraTabControl1)
        Me.Name = "wImportExcelNIKEPOTracking"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Import Excel NIKE PO Tracking"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.ogbheader, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbheader.ResumeLayout(False)
        CType(Me.FTCustomerPO.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNImportOrderDataType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTEndGacDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTEndGacDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTStartGacDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTStartGacDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTEndDocDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTEndDocDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTStartDocDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTStartDocDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.XtraTabControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XtraTabControl1.ResumeLayout(False)
        Me.XtraTabPage7.ResumeLayout(False)
        CType(Me.ogcdt1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvdt1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbmainprocbutton.ResumeLayout(False)
        CType(Me.FTTradigPO.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogbheader As DevExpress.XtraEditors.GroupControl
    Friend WithEvents FTEndGacDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents FTEndGacDate_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTStartGacDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents FTStartGacDate_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTEndDocDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents FTEndDocDate_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTStartDocDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents FTStartDocDate_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents XtraTabControl1 As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents XtraTabPage7 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents ogcdt1 As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvdt1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents ogbmainprocbutton As DevExpress.XtraEditors.PanelControl
    Friend WithEvents ocmrefresh As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FNImportOrderDataType As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents FNImportOrderDataType_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents olbcustpo As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTCustomerPO As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FTTradigPO_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTTradigPO As DevExpress.XtraEditors.TextEdit
End Class

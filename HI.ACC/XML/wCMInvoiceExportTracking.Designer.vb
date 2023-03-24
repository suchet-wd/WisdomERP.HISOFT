<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wPostXMLJSon
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
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.ogbOrderCriteria = New DevExpress.XtraEditors.GroupControl()
        Me.FTEDate_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTEDate = New DevExpress.XtraEditors.DateEdit()
        Me.FTSDate_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTSDate = New DevExpress.XtraEditors.DateEdit()
        Me.FTInvoiceExportNo_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTInvoiceExportNo = New DevExpress.XtraEditors.ButtonEdit()
        Me.FTPORef_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysPOID = New DevExpress.XtraEditors.ButtonEdit()
        Me.ogbmainprocbutton = New DevExpress.XtraEditors.PanelControl()
        Me.ocmpreview = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmpostdatajson = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmclearclsr = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmrefresh = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.otbmain = New DevExpress.XtraTab.XtraTabControl()
        Me.otpfactorycminvoice = New DevExpress.XtraTab.XtraTabPage()
        Me.ogbdirectorapp = New DevExpress.XtraEditors.GroupControl()
        Me.ogcdirector = New DevExpress.XtraGrid.GridControl()
        Me.ogvdirector = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.CFTSelect = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ReposSelect = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.CFTInvoiceExportNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFDInvoiceExportDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CMCINo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CCustomerPoNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CMIQty = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CXMLQty = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CDiffQty = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CStateXML = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCheckEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.CStateXMLBy = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CStateXMLDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CStateXMLTime = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemTextEdit2 = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.ocmexporttext = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.ogbOrderCriteria, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbOrderCriteria.SuspendLayout()
        CType(Me.FTEDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTEDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTSDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTSDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTInvoiceExportNo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysPOID.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbmainprocbutton.SuspendLayout()
        CType(Me.otbmain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.otbmain.SuspendLayout()
        Me.otpfactorycminvoice.SuspendLayout()
        CType(Me.ogbdirectorapp, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbdirectorapp.SuspendLayout()
        CType(Me.ogcdirector, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvdirector, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ReposSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemTextEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ogbOrderCriteria
        '
        Me.ogbOrderCriteria.Controls.Add(Me.FTEDate_lbl)
        Me.ogbOrderCriteria.Controls.Add(Me.FTEDate)
        Me.ogbOrderCriteria.Controls.Add(Me.FTSDate_lbl)
        Me.ogbOrderCriteria.Controls.Add(Me.FTSDate)
        Me.ogbOrderCriteria.Controls.Add(Me.FTInvoiceExportNo_lbl)
        Me.ogbOrderCriteria.Controls.Add(Me.FTInvoiceExportNo)
        Me.ogbOrderCriteria.Controls.Add(Me.FTPORef_lbl)
        Me.ogbOrderCriteria.Controls.Add(Me.FNHSysPOID)
        Me.ogbOrderCriteria.Dock = System.Windows.Forms.DockStyle.Top
        Me.ogbOrderCriteria.Location = New System.Drawing.Point(0, 0)
        Me.ogbOrderCriteria.Name = "ogbOrderCriteria"
        Me.ogbOrderCriteria.ShowCaption = False
        Me.ogbOrderCriteria.Size = New System.Drawing.Size(1064, 71)
        Me.ogbOrderCriteria.TabIndex = 2
        Me.ogbOrderCriteria.Text = "Criteria"
        '
        'FTEDate_lbl
        '
        Me.FTEDate_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTEDate_lbl.Appearance.Options.UseForeColor = True
        Me.FTEDate_lbl.Appearance.Options.UseTextOptions = True
        Me.FTEDate_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTEDate_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTEDate_lbl.Location = New System.Drawing.Point(397, 34)
        Me.FTEDate_lbl.Name = "FTEDate_lbl"
        Me.FTEDate_lbl.Size = New System.Drawing.Size(138, 19)
        Me.FTEDate_lbl.TabIndex = 535
        Me.FTEDate_lbl.Tag = "2|"
        Me.FTEDate_lbl.Text = "To Invoice Date :"
        '
        'FTEDate
        '
        Me.FTEDate.EditValue = Nothing
        Me.FTEDate.EnterMoveNextControl = True
        Me.FTEDate.Location = New System.Drawing.Point(538, 35)
        Me.FTEDate.Name = "FTEDate"
        Me.FTEDate.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTEDate.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FTEDate.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FTEDate.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FTEDate.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FTEDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FTEDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.FTEDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTEDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTEDate.Properties.NullDate = ""
        Me.FTEDate.Size = New System.Drawing.Size(108, 20)
        Me.FTEDate.TabIndex = 534
        Me.FTEDate.Tag = "2|"
        '
        'FTSDate_lbl
        '
        Me.FTSDate_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTSDate_lbl.Appearance.Options.UseForeColor = True
        Me.FTSDate_lbl.Appearance.Options.UseTextOptions = True
        Me.FTSDate_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTSDate_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTSDate_lbl.Location = New System.Drawing.Point(42, 33)
        Me.FTSDate_lbl.Name = "FTSDate_lbl"
        Me.FTSDate_lbl.Size = New System.Drawing.Size(138, 19)
        Me.FTSDate_lbl.TabIndex = 533
        Me.FTSDate_lbl.Tag = "2|"
        Me.FTSDate_lbl.Text = "Start Invoice Date :"
        '
        'FTSDate
        '
        Me.FTSDate.EditValue = Nothing
        Me.FTSDate.EnterMoveNextControl = True
        Me.FTSDate.Location = New System.Drawing.Point(183, 34)
        Me.FTSDate.Name = "FTSDate"
        Me.FTSDate.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTSDate.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FTSDate.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FTSDate.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FTSDate.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FTSDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FTSDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.FTSDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTSDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTSDate.Properties.NullDate = ""
        Me.FTSDate.Size = New System.Drawing.Size(108, 20)
        Me.FTSDate.TabIndex = 531
        Me.FTSDate.Tag = "2|"
        '
        'FTInvoiceExportNo_lbl
        '
        Me.FTInvoiceExportNo_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTInvoiceExportNo_lbl.Appearance.Options.UseForeColor = True
        Me.FTInvoiceExportNo_lbl.Appearance.Options.UseTextOptions = True
        Me.FTInvoiceExportNo_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTInvoiceExportNo_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTInvoiceExportNo_lbl.Location = New System.Drawing.Point(397, 9)
        Me.FTInvoiceExportNo_lbl.Name = "FTInvoiceExportNo_lbl"
        Me.FTInvoiceExportNo_lbl.Size = New System.Drawing.Size(135, 19)
        Me.FTInvoiceExportNo_lbl.TabIndex = 532
        Me.FTInvoiceExportNo_lbl.Tag = "2|"
        Me.FTInvoiceExportNo_lbl.Text = "Invoice Export No :"
        '
        'FTInvoiceExportNo
        '
        Me.FTInvoiceExportNo.Location = New System.Drawing.Point(538, 9)
        Me.FTInvoiceExportNo.Name = "FTInvoiceExportNo"
        Me.FTInvoiceExportNo.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTInvoiceExportNo.Properties.Appearance.Options.UseForeColor = True
        Me.FTInvoiceExportNo.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FTInvoiceExportNo.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FTInvoiceExportNo.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FTInvoiceExportNo.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FTInvoiceExportNo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject2, "", "676", Nothing, True)})
        Me.FTInvoiceExportNo.Properties.MaxLength = 30
        Me.FTInvoiceExportNo.Size = New System.Drawing.Size(159, 20)
        Me.FTInvoiceExportNo.TabIndex = 530
        Me.FTInvoiceExportNo.Tag = "2|"
        '
        'FTPORef_lbl
        '
        Me.FTPORef_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTPORef_lbl.Appearance.Options.UseForeColor = True
        Me.FTPORef_lbl.Appearance.Options.UseTextOptions = True
        Me.FTPORef_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTPORef_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTPORef_lbl.Location = New System.Drawing.Point(21, 8)
        Me.FTPORef_lbl.Name = "FTPORef_lbl"
        Me.FTPORef_lbl.Size = New System.Drawing.Size(156, 20)
        Me.FTPORef_lbl.TabIndex = 3
        Me.FTPORef_lbl.Tag = "2|"
        Me.FTPORef_lbl.Text = "Cust. PO.:"
        '
        'FNHSysPOID
        '
        Me.FNHSysPOID.Location = New System.Drawing.Point(183, 8)
        Me.FNHSysPOID.Name = "FNHSysPOID"
        Me.FNHSysPOID.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject3, "", "145", Nothing, True)})
        Me.FNHSysPOID.Properties.Tag = ""
        Me.FNHSysPOID.Size = New System.Drawing.Size(130, 20)
        Me.FNHSysPOID.TabIndex = 4
        Me.FNHSysPOID.Tag = "2|"
        '
        'ogbmainprocbutton
        '
        Me.ogbmainprocbutton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmexporttext)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmpreview)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmpostdatajson)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmclearclsr)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmrefresh)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmexit)
        Me.ogbmainprocbutton.Location = New System.Drawing.Point(86, 133)
        Me.ogbmainprocbutton.Name = "ogbmainprocbutton"
        Me.ogbmainprocbutton.Size = New System.Drawing.Size(939, 125)
        Me.ogbmainprocbutton.TabIndex = 138
        Me.ogbmainprocbutton.Tag = "2|"
        '
        'ocmpreview
        '
        Me.ocmpreview.Location = New System.Drawing.Point(422, 50)
        Me.ocmpreview.Name = "ocmpreview"
        Me.ocmpreview.Size = New System.Drawing.Size(95, 25)
        Me.ocmpreview.TabIndex = 111
        Me.ocmpreview.TabStop = False
        Me.ocmpreview.Tag = "2|"
        Me.ocmpreview.Text = "PREVIEW"
        '
        'ocmpostdatajson
        '
        Me.ocmpostdatajson.Location = New System.Drawing.Point(54, 11)
        Me.ocmpostdatajson.Name = "ocmpostdatajson"
        Me.ocmpostdatajson.Size = New System.Drawing.Size(95, 25)
        Me.ocmpostdatajson.TabIndex = 110
        Me.ocmpostdatajson.TabStop = False
        Me.ocmpostdatajson.Tag = "2|"
        Me.ocmpostdatajson.Text = "Export To XML"
        '
        'ocmclearclsr
        '
        Me.ocmclearclsr.Location = New System.Drawing.Point(213, 11)
        Me.ocmclearclsr.Name = "ocmclearclsr"
        Me.ocmclearclsr.Size = New System.Drawing.Size(95, 25)
        Me.ocmclearclsr.TabIndex = 109
        Me.ocmclearclsr.TabStop = False
        Me.ocmclearclsr.Tag = "2|"
        Me.ocmclearclsr.Text = "CLEAR"
        '
        'ocmrefresh
        '
        Me.ocmrefresh.Location = New System.Drawing.Point(415, 11)
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
        Me.ocmexit.Location = New System.Drawing.Point(863, 11)
        Me.ocmexit.Name = "ocmexit"
        Me.ocmexit.Size = New System.Drawing.Size(70, 25)
        Me.ocmexit.TabIndex = 96
        Me.ocmexit.TabStop = False
        Me.ocmexit.Tag = "2|"
        Me.ocmexit.Text = "EXIT"
        '
        'otbmain
        '
        Me.otbmain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.otbmain.Location = New System.Drawing.Point(0, 71)
        Me.otbmain.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.otbmain.Name = "otbmain"
        Me.otbmain.SelectedTabPage = Me.otpfactorycminvoice
        Me.otbmain.Size = New System.Drawing.Size(1064, 538)
        Me.otbmain.TabIndex = 139
        Me.otbmain.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.otpfactorycminvoice})
        '
        'otpfactorycminvoice
        '
        Me.otpfactorycminvoice.Controls.Add(Me.ogbdirectorapp)
        Me.otpfactorycminvoice.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.otpfactorycminvoice.Name = "otpfactorycminvoice"
        Me.otpfactorycminvoice.Size = New System.Drawing.Size(1058, 510)
        Me.otpfactorycminvoice.Text = "Invoice Detail"
        '
        'ogbdirectorapp
        '
        Me.ogbdirectorapp.Controls.Add(Me.ogcdirector)
        Me.ogbdirectorapp.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogbdirectorapp.Location = New System.Drawing.Point(0, 0)
        Me.ogbdirectorapp.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.ogbdirectorapp.Name = "ogbdirectorapp"
        Me.ogbdirectorapp.Size = New System.Drawing.Size(1058, 510)
        Me.ogbdirectorapp.TabIndex = 1
        '
        'ogcdirector
        '
        Me.ogcdirector.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogcdirector.Location = New System.Drawing.Point(2, 20)
        Me.ogcdirector.MainView = Me.ogvdirector
        Me.ogcdirector.Name = "ogcdirector"
        Me.ogcdirector.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemTextEdit2, Me.ReposSelect, Me.RepositoryItemCheckEdit1})
        Me.ogcdirector.Size = New System.Drawing.Size(1054, 488)
        Me.ogcdirector.TabIndex = 21
        Me.ogcdirector.TabStop = False
        Me.ogcdirector.Tag = "2|"
        Me.ogcdirector.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvdirector})
        '
        'ogvdirector
        '
        Me.ogvdirector.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.CFTSelect, Me.CFTInvoiceExportNo, Me.CFDInvoiceExportDate, Me.CMCINo, Me.CCustomerPoNo, Me.CMIQty, Me.CXMLQty, Me.CDiffQty, Me.CStateXML, Me.CStateXMLBy, Me.CStateXMLDate, Me.CStateXMLTime})
        Me.ogvdirector.CustomizationFormBounds = New System.Drawing.Rectangle(758, 512, 216, 178)
        Me.ogvdirector.GridControl = Me.ogcdirector
        Me.ogvdirector.Name = "ogvdirector"
        Me.ogvdirector.OptionsBehavior.AutoExpandAllGroups = True
        Me.ogvdirector.OptionsCustomization.AllowQuickHideColumns = False
        Me.ogvdirector.OptionsNavigation.EnterMoveNextColumn = True
        Me.ogvdirector.OptionsView.ColumnAutoWidth = False
        Me.ogvdirector.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.ogvdirector.OptionsView.ShowAutoFilterRow = True
        Me.ogvdirector.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
        Me.ogvdirector.OptionsView.ShowFooter = True
        Me.ogvdirector.OptionsView.ShowGroupPanel = False
        Me.ogvdirector.Tag = "2|"
        '
        'CFTSelect
        '
        Me.CFTSelect.Caption = "เลือก"
        Me.CFTSelect.ColumnEdit = Me.ReposSelect
        Me.CFTSelect.FieldName = "FTSelect"
        Me.CFTSelect.Name = "CFTSelect"
        Me.CFTSelect.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFTSelect.Visible = True
        Me.CFTSelect.VisibleIndex = 0
        Me.CFTSelect.Width = 40
        '
        'ReposSelect
        '
        Me.ReposSelect.AutoHeight = False
        Me.ReposSelect.Caption = "Check"
        Me.ReposSelect.Name = "ReposSelect"
        Me.ReposSelect.ValueChecked = "1"
        Me.ReposSelect.ValueUnchecked = "0"
        '
        'CFTInvoiceExportNo
        '
        Me.CFTInvoiceExportNo.Caption = "ใบกำกับภาษี"
        Me.CFTInvoiceExportNo.FieldName = "FTInvoiceExportNo"
        Me.CFTInvoiceExportNo.Name = "CFTInvoiceExportNo"
        Me.CFTInvoiceExportNo.OptionsColumn.AllowEdit = False
        Me.CFTInvoiceExportNo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFTInvoiceExportNo.OptionsColumn.ReadOnly = True
        Me.CFTInvoiceExportNo.Visible = True
        Me.CFTInvoiceExportNo.VisibleIndex = 1
        Me.CFTInvoiceExportNo.Width = 106
        '
        'CFDInvoiceExportDate
        '
        Me.CFDInvoiceExportDate.Caption = "วันที่ใบกำกับภาษี"
        Me.CFDInvoiceExportDate.FieldName = "FDInvoiceExportDate"
        Me.CFDInvoiceExportDate.Name = "CFDInvoiceExportDate"
        Me.CFDInvoiceExportDate.OptionsColumn.AllowEdit = False
        Me.CFDInvoiceExportDate.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFDInvoiceExportDate.OptionsColumn.ReadOnly = True
        Me.CFDInvoiceExportDate.Visible = True
        Me.CFDInvoiceExportDate.VisibleIndex = 2
        Me.CFDInvoiceExportDate.Width = 135
        '
        'CMCINo
        '
        Me.CMCINo.Caption = "MCI No"
        Me.CMCINo.FieldName = "MCINo"
        Me.CMCINo.Name = "CMCINo"
        Me.CMCINo.OptionsColumn.AllowEdit = False
        Me.CMCINo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.CMCINo.OptionsColumn.ReadOnly = True
        Me.CMCINo.Visible = True
        Me.CMCINo.VisibleIndex = 3
        Me.CMCINo.Width = 182
        '
        'CCustomerPoNo
        '
        Me.CCustomerPoNo.Caption = "Customer Po No."
        Me.CCustomerPoNo.FieldName = "CustomerPoNo"
        Me.CCustomerPoNo.Name = "CCustomerPoNo"
        Me.CCustomerPoNo.OptionsColumn.AllowEdit = False
        Me.CCustomerPoNo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.CCustomerPoNo.OptionsColumn.ReadOnly = True
        Me.CCustomerPoNo.Visible = True
        Me.CCustomerPoNo.VisibleIndex = 4
        Me.CCustomerPoNo.Width = 196
        '
        'CMIQty
        '
        Me.CMIQty.Caption = "MCI Qty"
        Me.CMIQty.FieldName = "MIQty"
        Me.CMIQty.Name = "CMIQty"
        Me.CMIQty.OptionsColumn.AllowEdit = False
        Me.CMIQty.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.CMIQty.OptionsColumn.ReadOnly = True
        Me.CMIQty.Visible = True
        Me.CMIQty.VisibleIndex = 5
        '
        'CXMLQty
        '
        Me.CXMLQty.Caption = "Post Qty"
        Me.CXMLQty.FieldName = "XMLQty"
        Me.CXMLQty.Name = "CXMLQty"
        Me.CXMLQty.OptionsColumn.AllowEdit = False
        Me.CXMLQty.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.CXMLQty.OptionsColumn.ReadOnly = True
        Me.CXMLQty.Visible = True
        Me.CXMLQty.VisibleIndex = 6
        '
        'CDiffQty
        '
        Me.CDiffQty.Caption = "Diff Qty"
        Me.CDiffQty.FieldName = "DiffQty"
        Me.CDiffQty.Name = "CDiffQty"
        Me.CDiffQty.OptionsColumn.AllowEdit = False
        Me.CDiffQty.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.CDiffQty.OptionsColumn.ReadOnly = True
        Me.CDiffQty.Visible = True
        Me.CDiffQty.VisibleIndex = 7
        '
        'CStateXML
        '
        Me.CStateXML.Caption = "State Post"
        Me.CStateXML.ColumnEdit = Me.RepositoryItemCheckEdit1
        Me.CStateXML.FieldName = "StateXML"
        Me.CStateXML.Name = "CStateXML"
        Me.CStateXML.OptionsColumn.AllowEdit = False
        Me.CStateXML.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.CStateXML.OptionsColumn.ReadOnly = True
        Me.CStateXML.Visible = True
        Me.CStateXML.VisibleIndex = 8
        '
        'RepositoryItemCheckEdit1
        '
        Me.RepositoryItemCheckEdit1.AutoHeight = False
        Me.RepositoryItemCheckEdit1.Name = "RepositoryItemCheckEdit1"
        Me.RepositoryItemCheckEdit1.ValueChecked = "1"
        Me.RepositoryItemCheckEdit1.ValueUnchecked = "0"
        '
        'CStateXMLBy
        '
        Me.CStateXMLBy.Caption = "Post By"
        Me.CStateXMLBy.FieldName = "StateXMLBy"
        Me.CStateXMLBy.Name = "CStateXMLBy"
        Me.CStateXMLBy.OptionsColumn.AllowEdit = False
        Me.CStateXMLBy.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.CStateXMLBy.OptionsColumn.ReadOnly = True
        Me.CStateXMLBy.Visible = True
        Me.CStateXMLBy.VisibleIndex = 9
        Me.CStateXMLBy.Width = 132
        '
        'CStateXMLDate
        '
        Me.CStateXMLDate.Caption = "Post Date"
        Me.CStateXMLDate.FieldName = "StateXMLDate"
        Me.CStateXMLDate.Name = "CStateXMLDate"
        Me.CStateXMLDate.OptionsColumn.AllowEdit = False
        Me.CStateXMLDate.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.CStateXMLDate.OptionsColumn.ReadOnly = True
        Me.CStateXMLDate.Visible = True
        Me.CStateXMLDate.VisibleIndex = 10
        Me.CStateXMLDate.Width = 122
        '
        'CStateXMLTime
        '
        Me.CStateXMLTime.Caption = "Post Time"
        Me.CStateXMLTime.FieldName = "StateXMLTime"
        Me.CStateXMLTime.Name = "CStateXMLTime"
        Me.CStateXMLTime.OptionsColumn.AllowEdit = False
        Me.CStateXMLTime.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.CStateXMLTime.OptionsColumn.ReadOnly = True
        Me.CStateXMLTime.Visible = True
        Me.CStateXMLTime.VisibleIndex = 11
        Me.CStateXMLTime.Width = 94
        '
        'RepositoryItemTextEdit2
        '
        Me.RepositoryItemTextEdit2.AutoHeight = False
        Me.RepositoryItemTextEdit2.DisplayFormat.FormatString = "N0"
        Me.RepositoryItemTextEdit2.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.RepositoryItemTextEdit2.EditFormat.FormatString = "N0"
        Me.RepositoryItemTextEdit2.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.RepositoryItemTextEdit2.Name = "RepositoryItemTextEdit2"
        '
        'ocmexporttext
        '
        Me.ocmexporttext.Location = New System.Drawing.Point(72, 65)
        Me.ocmexporttext.Name = "ocmexporttext"
        Me.ocmexporttext.Size = New System.Drawing.Size(95, 25)
        Me.ocmexporttext.TabIndex = 112
        Me.ocmexporttext.TabStop = False
        Me.ocmexporttext.Tag = "2|"
        Me.ocmexporttext.Text = "Export To Text"
        '
        'wPostXMLJSon
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(1064, 609)
        Me.Controls.Add(Me.ogbmainprocbutton)
        Me.Controls.Add(Me.otbmain)
        Me.Controls.Add(Me.ogbOrderCriteria)
        Me.Name = "wPostXMLJSon"
        Me.Text = "Post Data Json To Nike API"
        CType(Me.ogbOrderCriteria, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbOrderCriteria.ResumeLayout(False)
        CType(Me.FTEDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTEDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTSDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTSDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTInvoiceExportNo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysPOID.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbmainprocbutton.ResumeLayout(False)
        CType(Me.otbmain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.otbmain.ResumeLayout(False)
        Me.otpfactorycminvoice.ResumeLayout(False)
        CType(Me.ogbdirectorapp, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbdirectorapp.ResumeLayout(False)
        CType(Me.ogcdirector, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvdirector, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ReposSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemTextEdit2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogbOrderCriteria As DevExpress.XtraEditors.GroupControl
    Friend WithEvents FTPORef_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysPOID As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents ogbmainprocbutton As DevExpress.XtraEditors.PanelControl
    Friend WithEvents ocmclearclsr As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmrefresh As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents otbmain As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents otpfactorycminvoice As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents ogbdirectorapp As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ogcdirector As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvdirector As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents ReposSelect As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents RepositoryItemTextEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents ocmpostdatajson As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FTEDate_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTEDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents FTSDate_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTSDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents FTInvoiceExportNo_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTInvoiceExportNo As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents CFTSelect As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFTInvoiceExportNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFDInvoiceExportDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CMCINo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CCustomerPoNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CMIQty As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CXMLQty As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CDiffQty As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CStateXML As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CStateXMLBy As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CStateXMLDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CStateXMLTime As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemCheckEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents ocmpreview As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmexporttext As DevExpress.XtraEditors.SimpleButton
End Class

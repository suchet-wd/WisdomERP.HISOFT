<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wFactoryCMInvoiceTracking
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
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject4 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject5 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject6 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.ogbOrderCriteria = New DevExpress.XtraEditors.GroupControl()
        Me.FTEndShipment = New DevExpress.XtraEditors.DateEdit()
        Me.FTEndShipment_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTStartShipment = New DevExpress.XtraEditors.DateEdit()
        Me.FTStartShipment_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysBuyId_None = New DevExpress.XtraEditors.TextEdit()
        Me.FNHSysBuyId_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysBuyId = New DevExpress.XtraEditors.ButtonEdit()
        Me.FNHSysSeasonId_None = New DevExpress.XtraEditors.TextEdit()
        Me.FNHSysSeasonId_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysSeasonId = New DevExpress.XtraEditors.ButtonEdit()
        Me.FNHSysCustId_None = New DevExpress.XtraEditors.TextEdit()
        Me.FNHSysCustId_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysCustId = New DevExpress.XtraEditors.ButtonEdit()
        Me.FNHSysCmpId_None = New DevExpress.XtraEditors.TextEdit()
        Me.FNHSysCmpId_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysCmpId = New DevExpress.XtraEditors.ButtonEdit()
        Me.FNHSysStyleId_None = New DevExpress.XtraEditors.TextEdit()
        Me.FNHSysStyleId_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysStyleId = New DevExpress.XtraEditors.ButtonEdit()
        Me.FTPORef_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysPOID = New DevExpress.XtraEditors.ButtonEdit()
        Me.ogbmainprocbutton = New DevExpress.XtraEditors.PanelControl()
        Me.ocmpreviewtvwthb = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmclearclsr = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmpreview = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmrefresh = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.otbmain = New DevExpress.XtraTab.XtraTabControl()
        Me.otpfactorycminvoice = New DevExpress.XtraTab.XtraTabPage()
        Me.ogbdirectorapp = New DevExpress.XtraEditors.GroupControl()
        Me.ogcdirector = New DevExpress.XtraGrid.GridControl()
        Me.ogvdirector = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.CFNHSysCmpId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFTSelect = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ReposSelect = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.cFTCmpCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTCompName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTCustomerPO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FDShipDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFNHSysStyleId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTStyleCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTInvoiceNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFDInvoiceDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNCM = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemTextEdit2 = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.cFNAmount = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTStateSendApp = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTStateSendBy = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FDStateSendDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTStateApp = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTStateAppBy = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FDStateAppDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTStateReject = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTStateRejectBy = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FDStateRejectDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTStateWHApp = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTStateWHAppBy = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FDStateWHAppDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTStateWHReject = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTStateWHRejectBy = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FDStateWHRejectDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTInvoiceExportNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FDInvoiceExportDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTInvoiceExportNote = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNStateImportNetPrice = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.otpCustomerPowiating = New DevExpress.XtraTab.XtraTabPage()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.ogcpowaiting = New DevExpress.XtraGrid.GridControl()
        Me.ogvpowaiting = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.C2FTCmpCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.C2FTCmpName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTPOref = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFDShipDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFNHSysCustId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTCustCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFTCustName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNHSysContinentId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTContinentCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTContinentName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFNHSysCountryId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFTCountryCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFTCountryName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFNHSysProvinceId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFTProvinceCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTProvinceName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFNHSysShipModeId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFTShipModeCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFNHSysShipPortId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFTShipPortCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.C2FNQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFNStateImportNetPrice = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemTextEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.RepositoryItemCheckEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.ocmcheckpowaiting = New System.Windows.Forms.Timer()
        Me.FTNikePOLineItem = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.ogbOrderCriteria, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbOrderCriteria.SuspendLayout()
        CType(Me.FTEndShipment.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTEndShipment.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTStartShipment.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTStartShipment.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysBuyId_None.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysBuyId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysSeasonId_None.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysSeasonId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysCustId_None.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysCustId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysCmpId_None.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysCmpId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysStyleId_None.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysStyleId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
        CType(Me.RepositoryItemTextEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.otpCustomerPowiating.SuspendLayout()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.ogcpowaiting, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvpowaiting, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ogbOrderCriteria
        '
        Me.ogbOrderCriteria.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbOrderCriteria.Controls.Add(Me.FTEndShipment)
        Me.ogbOrderCriteria.Controls.Add(Me.FTEndShipment_lbl)
        Me.ogbOrderCriteria.Controls.Add(Me.FTStartShipment)
        Me.ogbOrderCriteria.Controls.Add(Me.FTStartShipment_lbl)
        Me.ogbOrderCriteria.Controls.Add(Me.FNHSysBuyId_None)
        Me.ogbOrderCriteria.Controls.Add(Me.FNHSysBuyId_lbl)
        Me.ogbOrderCriteria.Controls.Add(Me.FNHSysBuyId)
        Me.ogbOrderCriteria.Controls.Add(Me.FNHSysSeasonId_None)
        Me.ogbOrderCriteria.Controls.Add(Me.FNHSysSeasonId_lbl)
        Me.ogbOrderCriteria.Controls.Add(Me.FNHSysSeasonId)
        Me.ogbOrderCriteria.Controls.Add(Me.FNHSysCustId_None)
        Me.ogbOrderCriteria.Controls.Add(Me.FNHSysCustId_lbl)
        Me.ogbOrderCriteria.Controls.Add(Me.FNHSysCustId)
        Me.ogbOrderCriteria.Controls.Add(Me.FNHSysCmpId_None)
        Me.ogbOrderCriteria.Controls.Add(Me.FNHSysCmpId_lbl)
        Me.ogbOrderCriteria.Controls.Add(Me.FNHSysCmpId)
        Me.ogbOrderCriteria.Controls.Add(Me.FNHSysStyleId_None)
        Me.ogbOrderCriteria.Controls.Add(Me.FNHSysStyleId_lbl)
        Me.ogbOrderCriteria.Controls.Add(Me.FNHSysStyleId)
        Me.ogbOrderCriteria.Controls.Add(Me.FTPORef_lbl)
        Me.ogbOrderCriteria.Controls.Add(Me.FNHSysPOID)
        Me.ogbOrderCriteria.Location = New System.Drawing.Point(3, 3)
        Me.ogbOrderCriteria.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbOrderCriteria.Name = "ogbOrderCriteria"
        Me.ogbOrderCriteria.ShowCaption = False
        Me.ogbOrderCriteria.Size = New System.Drawing.Size(1202, 133)
        Me.ogbOrderCriteria.TabIndex = 2
        Me.ogbOrderCriteria.Text = "Criteria"
        '
        'FTEndShipment
        '
        Me.FTEndShipment.EditValue = Nothing
        Me.FTEndShipment.EnterMoveNextControl = True
        Me.FTEndShipment.Location = New System.Drawing.Point(819, 87)
        Me.FTEndShipment.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTEndShipment.Name = "FTEndShipment"
        Me.FTEndShipment.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTEndShipment.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FTEndShipment.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.FTEndShipment.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTEndShipment.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTEndShipment.Properties.NullDate = ""
        Me.FTEndShipment.Size = New System.Drawing.Size(152, 22)
        Me.FTEndShipment.TabIndex = 278
        Me.FTEndShipment.Tag = "2|"
        '
        'FTEndShipment_lbl
        '
        Me.FTEndShipment_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTEndShipment_lbl.Appearance.Options.UseForeColor = True
        Me.FTEndShipment_lbl.Appearance.Options.UseTextOptions = True
        Me.FTEndShipment_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTEndShipment_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTEndShipment_lbl.Location = New System.Drawing.Point(653, 86)
        Me.FTEndShipment_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTEndShipment_lbl.Name = "FTEndShipment_lbl"
        Me.FTEndShipment_lbl.Size = New System.Drawing.Size(163, 23)
        Me.FTEndShipment_lbl.TabIndex = 279
        Me.FTEndShipment_lbl.Tag = "2|"
        Me.FTEndShipment_lbl.Text = "End MI Date:"
        '
        'FTStartShipment
        '
        Me.FTStartShipment.EditValue = Nothing
        Me.FTStartShipment.EnterMoveNextControl = True
        Me.FTStartShipment.Location = New System.Drawing.Point(195, 87)
        Me.FTStartShipment.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTStartShipment.Name = "FTStartShipment"
        Me.FTStartShipment.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTStartShipment.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FTStartShipment.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.FTStartShipment.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTStartShipment.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTStartShipment.Properties.NullDate = ""
        Me.FTStartShipment.Size = New System.Drawing.Size(152, 22)
        Me.FTStartShipment.TabIndex = 276
        Me.FTStartShipment.Tag = "2|"
        '
        'FTStartShipment_lbl
        '
        Me.FTStartShipment_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTStartShipment_lbl.Appearance.Options.UseForeColor = True
        Me.FTStartShipment_lbl.Appearance.Options.UseTextOptions = True
        Me.FTStartShipment_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTStartShipment_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTStartShipment_lbl.Location = New System.Drawing.Point(26, 86)
        Me.FTStartShipment_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTStartShipment_lbl.Name = "FTStartShipment_lbl"
        Me.FTStartShipment_lbl.Size = New System.Drawing.Size(162, 23)
        Me.FTStartShipment_lbl.TabIndex = 277
        Me.FTStartShipment_lbl.Tag = "2|"
        Me.FTStartShipment_lbl.Text = "Start MI Date:"
        '
        'FNHSysBuyId_None
        '
        Me.FNHSysBuyId_None.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FNHSysBuyId_None.Location = New System.Drawing.Point(934, 33)
        Me.FNHSysBuyId_None.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysBuyId_None.Name = "FNHSysBuyId_None"
        Me.FNHSysBuyId_None.Properties.Appearance.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysBuyId_None.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysBuyId_None.Properties.ReadOnly = True
        Me.FNHSysBuyId_None.Size = New System.Drawing.Size(260, 22)
        Me.FNHSysBuyId_None.TabIndex = 13
        Me.FNHSysBuyId_None.Tag = "2|"
        '
        'FNHSysBuyId_lbl
        '
        Me.FNHSysBuyId_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysBuyId_lbl.Appearance.Options.UseForeColor = True
        Me.FNHSysBuyId_lbl.Appearance.Options.UseTextOptions = True
        Me.FNHSysBuyId_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysBuyId_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysBuyId_lbl.Location = New System.Drawing.Point(630, 33)
        Me.FNHSysBuyId_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysBuyId_lbl.Name = "FNHSysBuyId_lbl"
        Me.FNHSysBuyId_lbl.Size = New System.Drawing.Size(182, 25)
        Me.FNHSysBuyId_lbl.TabIndex = 11
        Me.FNHSysBuyId_lbl.Tag = "2|"
        Me.FNHSysBuyId_lbl.Text = "Buy :"
        '
        'FNHSysBuyId
        '
        Me.FNHSysBuyId.Location = New System.Drawing.Point(819, 33)
        Me.FNHSysBuyId.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysBuyId.Name = "FNHSysBuyId"
        Me.FNHSysBuyId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", "113", Nothing, True)})
        Me.FNHSysBuyId.Properties.Tag = ""
        Me.FNHSysBuyId.Size = New System.Drawing.Size(112, 22)
        Me.FNHSysBuyId.TabIndex = 12
        Me.FNHSysBuyId.Tag = "2|"
        '
        'FNHSysSeasonId_None
        '
        Me.FNHSysSeasonId_None.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FNHSysSeasonId_None.Location = New System.Drawing.Point(934, 62)
        Me.FNHSysSeasonId_None.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysSeasonId_None.Name = "FNHSysSeasonId_None"
        Me.FNHSysSeasonId_None.Properties.Appearance.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysSeasonId_None.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysSeasonId_None.Properties.ReadOnly = True
        Me.FNHSysSeasonId_None.Size = New System.Drawing.Size(260, 22)
        Me.FNHSysSeasonId_None.TabIndex = 16
        Me.FNHSysSeasonId_None.Tag = "2|"
        '
        'FNHSysSeasonId_lbl
        '
        Me.FNHSysSeasonId_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysSeasonId_lbl.Appearance.Options.UseForeColor = True
        Me.FNHSysSeasonId_lbl.Appearance.Options.UseTextOptions = True
        Me.FNHSysSeasonId_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysSeasonId_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysSeasonId_lbl.Location = New System.Drawing.Point(630, 62)
        Me.FNHSysSeasonId_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysSeasonId_lbl.Name = "FNHSysSeasonId_lbl"
        Me.FNHSysSeasonId_lbl.Size = New System.Drawing.Size(182, 25)
        Me.FNHSysSeasonId_lbl.TabIndex = 14
        Me.FNHSysSeasonId_lbl.Tag = "2|"
        Me.FNHSysSeasonId_lbl.Text = "Season :"
        '
        'FNHSysSeasonId
        '
        Me.FNHSysSeasonId.Location = New System.Drawing.Point(819, 62)
        Me.FNHSysSeasonId.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysSeasonId.Name = "FNHSysSeasonId"
        Me.FNHSysSeasonId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject2, "", "94", Nothing, True)})
        Me.FNHSysSeasonId.Properties.Tag = ""
        Me.FNHSysSeasonId.Size = New System.Drawing.Size(112, 22)
        Me.FNHSysSeasonId.TabIndex = 15
        Me.FNHSysSeasonId.Tag = "2|"
        '
        'FNHSysCustId_None
        '
        Me.FNHSysCustId_None.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FNHSysCustId_None.Location = New System.Drawing.Point(934, 6)
        Me.FNHSysCustId_None.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysCustId_None.Name = "FNHSysCustId_None"
        Me.FNHSysCustId_None.Properties.Appearance.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysCustId_None.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysCustId_None.Properties.ReadOnly = True
        Me.FNHSysCustId_None.Size = New System.Drawing.Size(260, 22)
        Me.FNHSysCustId_None.TabIndex = 10
        Me.FNHSysCustId_None.Tag = "2|"
        '
        'FNHSysCustId_lbl
        '
        Me.FNHSysCustId_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysCustId_lbl.Appearance.Options.UseForeColor = True
        Me.FNHSysCustId_lbl.Appearance.Options.UseTextOptions = True
        Me.FNHSysCustId_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysCustId_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysCustId_lbl.Location = New System.Drawing.Point(630, 6)
        Me.FNHSysCustId_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysCustId_lbl.Name = "FNHSysCustId_lbl"
        Me.FNHSysCustId_lbl.Size = New System.Drawing.Size(182, 25)
        Me.FNHSysCustId_lbl.TabIndex = 8
        Me.FNHSysCustId_lbl.Tag = "2|"
        Me.FNHSysCustId_lbl.Text = "Customer :"
        '
        'FNHSysCustId
        '
        Me.FNHSysCustId.Location = New System.Drawing.Point(819, 6)
        Me.FNHSysCustId.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysCustId.Name = "FNHSysCustId"
        Me.FNHSysCustId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject3, "", "83", Nothing, True)})
        Me.FNHSysCustId.Properties.Tag = ""
        Me.FNHSysCustId.Size = New System.Drawing.Size(112, 22)
        Me.FNHSysCustId.TabIndex = 9
        Me.FNHSysCustId.Tag = "2|"
        '
        'FNHSysCmpId_None
        '
        Me.FNHSysCmpId_None.Location = New System.Drawing.Point(353, 6)
        Me.FNHSysCmpId_None.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysCmpId_None.Name = "FNHSysCmpId_None"
        Me.FNHSysCmpId_None.Properties.Appearance.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysCmpId_None.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysCmpId_None.Properties.ReadOnly = True
        Me.FNHSysCmpId_None.Size = New System.Drawing.Size(255, 22)
        Me.FNHSysCmpId_None.TabIndex = 2
        Me.FNHSysCmpId_None.Tag = "2|"
        '
        'FNHSysCmpId_lbl
        '
        Me.FNHSysCmpId_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysCmpId_lbl.Appearance.Options.UseForeColor = True
        Me.FNHSysCmpId_lbl.Appearance.Options.UseTextOptions = True
        Me.FNHSysCmpId_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysCmpId_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysCmpId_lbl.Location = New System.Drawing.Point(6, 6)
        Me.FNHSysCmpId_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysCmpId_lbl.Name = "FNHSysCmpId_lbl"
        Me.FNHSysCmpId_lbl.Size = New System.Drawing.Size(182, 25)
        Me.FNHSysCmpId_lbl.TabIndex = 0
        Me.FNHSysCmpId_lbl.Tag = "2|"
        Me.FNHSysCmpId_lbl.Text = "Company :"
        '
        'FNHSysCmpId
        '
        Me.FNHSysCmpId.Location = New System.Drawing.Point(195, 6)
        Me.FNHSysCmpId.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysCmpId.Name = "FNHSysCmpId"
        Me.FNHSysCmpId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject4, "", "11", Nothing, True)})
        Me.FNHSysCmpId.Properties.Tag = ""
        Me.FNHSysCmpId.Size = New System.Drawing.Size(152, 22)
        Me.FNHSysCmpId.TabIndex = 1
        Me.FNHSysCmpId.Tag = "2|"
        '
        'FNHSysStyleId_None
        '
        Me.FNHSysStyleId_None.Location = New System.Drawing.Point(353, 59)
        Me.FNHSysStyleId_None.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysStyleId_None.Name = "FNHSysStyleId_None"
        Me.FNHSysStyleId_None.Properties.Appearance.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysStyleId_None.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysStyleId_None.Properties.ReadOnly = True
        Me.FNHSysStyleId_None.Size = New System.Drawing.Size(255, 22)
        Me.FNHSysStyleId_None.TabIndex = 7
        Me.FNHSysStyleId_None.Tag = "2|"
        '
        'FNHSysStyleId_lbl
        '
        Me.FNHSysStyleId_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysStyleId_lbl.Appearance.Options.UseForeColor = True
        Me.FNHSysStyleId_lbl.Appearance.Options.UseTextOptions = True
        Me.FNHSysStyleId_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysStyleId_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysStyleId_lbl.Location = New System.Drawing.Point(6, 62)
        Me.FNHSysStyleId_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysStyleId_lbl.Name = "FNHSysStyleId_lbl"
        Me.FNHSysStyleId_lbl.Size = New System.Drawing.Size(182, 25)
        Me.FNHSysStyleId_lbl.TabIndex = 5
        Me.FNHSysStyleId_lbl.Tag = "2|"
        Me.FNHSysStyleId_lbl.Text = "Style No :"
        '
        'FNHSysStyleId
        '
        Me.FNHSysStyleId.Location = New System.Drawing.Point(195, 59)
        Me.FNHSysStyleId.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysStyleId.Name = "FNHSysStyleId"
        Me.FNHSysStyleId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject5, "", "89", Nothing, True)})
        Me.FNHSysStyleId.Properties.Tag = ""
        Me.FNHSysStyleId.Size = New System.Drawing.Size(152, 22)
        Me.FNHSysStyleId.TabIndex = 6
        Me.FNHSysStyleId.Tag = "2|"
        '
        'FTPORef_lbl
        '
        Me.FTPORef_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTPORef_lbl.Appearance.Options.UseForeColor = True
        Me.FTPORef_lbl.Appearance.Options.UseTextOptions = True
        Me.FTPORef_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTPORef_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTPORef_lbl.Location = New System.Drawing.Point(6, 33)
        Me.FTPORef_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTPORef_lbl.Name = "FTPORef_lbl"
        Me.FTPORef_lbl.Size = New System.Drawing.Size(182, 25)
        Me.FTPORef_lbl.TabIndex = 3
        Me.FTPORef_lbl.Tag = "2|"
        Me.FTPORef_lbl.Text = "Cust. PO.:"
        '
        'FNHSysPOID
        '
        Me.FNHSysPOID.Location = New System.Drawing.Point(195, 33)
        Me.FNHSysPOID.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysPOID.Name = "FNHSysPOID"
        Me.FNHSysPOID.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject6, "", "145", Nothing, True)})
        Me.FNHSysPOID.Properties.Tag = ""
        Me.FNHSysPOID.Size = New System.Drawing.Size(152, 22)
        Me.FNHSysPOID.TabIndex = 4
        Me.FNHSysPOID.Tag = "2|"
        '
        'ogbmainprocbutton
        '
        Me.ogbmainprocbutton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmpreviewtvwthb)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmclearclsr)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmpreview)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmrefresh)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmexit)
        Me.ogbmainprocbutton.Location = New System.Drawing.Point(54, 237)
        Me.ogbmainprocbutton.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbmainprocbutton.Name = "ogbmainprocbutton"
        Me.ogbmainprocbutton.Size = New System.Drawing.Size(1146, 134)
        Me.ogbmainprocbutton.TabIndex = 138
        Me.ogbmainprocbutton.Tag = "2|"
        '
        'ocmpreviewtvwthb
        '
        Me.ocmpreviewtvwthb.Location = New System.Drawing.Point(518, 52)
        Me.ocmpreviewtvwthb.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmpreviewtvwthb.Name = "ocmpreviewtvwthb"
        Me.ocmpreviewtvwthb.Size = New System.Drawing.Size(111, 31)
        Me.ocmpreviewtvwthb.TabIndex = 110
        Me.ocmpreviewtvwthb.TabStop = False
        Me.ocmpreviewtvwthb.Tag = "2|"
        Me.ocmpreviewtvwthb.Text = "PREVIEW"
        '
        'ocmclearclsr
        '
        Me.ocmclearclsr.Location = New System.Drawing.Point(24, 14)
        Me.ocmclearclsr.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmclearclsr.Name = "ocmclearclsr"
        Me.ocmclearclsr.Size = New System.Drawing.Size(111, 31)
        Me.ocmclearclsr.TabIndex = 109
        Me.ocmclearclsr.TabStop = False
        Me.ocmclearclsr.Tag = "2|"
        Me.ocmclearclsr.Text = "CLEAR"
        '
        'ocmpreview
        '
        Me.ocmpreview.Location = New System.Drawing.Point(142, 14)
        Me.ocmpreview.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmpreview.Name = "ocmpreview"
        Me.ocmpreview.Size = New System.Drawing.Size(111, 31)
        Me.ocmpreview.TabIndex = 108
        Me.ocmpreview.TabStop = False
        Me.ocmpreview.Tag = "2|"
        Me.ocmpreview.Text = "PREVIEW"
        '
        'ocmrefresh
        '
        Me.ocmrefresh.Location = New System.Drawing.Point(260, 14)
        Me.ocmrefresh.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmrefresh.Name = "ocmrefresh"
        Me.ocmrefresh.Size = New System.Drawing.Size(111, 31)
        Me.ocmrefresh.TabIndex = 107
        Me.ocmrefresh.TabStop = False
        Me.ocmrefresh.Tag = "2|"
        Me.ocmrefresh.Text = "REFRESH"
        '
        'ocmexit
        '
        Me.ocmexit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmexit.Location = New System.Drawing.Point(1007, 14)
        Me.ocmexit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmexit.Name = "ocmexit"
        Me.ocmexit.Size = New System.Drawing.Size(118, 31)
        Me.ocmexit.TabIndex = 96
        Me.ocmexit.TabStop = False
        Me.ocmexit.Tag = "2|"
        Me.ocmexit.Text = "EXIT"
        '
        'otbmain
        '
        Me.otbmain.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.otbmain.Location = New System.Drawing.Point(2, 4)
        Me.otbmain.Name = "otbmain"
        Me.otbmain.SelectedTabPage = Me.otpfactorycminvoice
        Me.otbmain.Size = New System.Drawing.Size(1234, 734)
        Me.otbmain.TabIndex = 139
        Me.otbmain.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.otpfactorycminvoice, Me.otpCustomerPowiating})
        '
        'otpfactorycminvoice
        '
        Me.otpfactorycminvoice.Controls.Add(Me.ogbdirectorapp)
        Me.otpfactorycminvoice.Controls.Add(Me.ogbOrderCriteria)
        Me.otpfactorycminvoice.Name = "otpfactorycminvoice"
        Me.otpfactorycminvoice.Size = New System.Drawing.Size(1227, 700)
        Me.otpfactorycminvoice.Text = "Factory CM Invoice"
        '
        'ogbdirectorapp
        '
        Me.ogbdirectorapp.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbdirectorapp.Controls.Add(Me.ogcdirector)
        Me.ogbdirectorapp.Location = New System.Drawing.Point(0, 143)
        Me.ogbdirectorapp.Name = "ogbdirectorapp"
        Me.ogbdirectorapp.Size = New System.Drawing.Size(1205, 552)
        Me.ogbdirectorapp.TabIndex = 1
        '
        'ogcdirector
        '
        Me.ogcdirector.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogcdirector.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcdirector.Location = New System.Drawing.Point(2, 25)
        Me.ogcdirector.MainView = Me.ogvdirector
        Me.ogcdirector.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcdirector.Name = "ogcdirector"
        Me.ogcdirector.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemTextEdit2, Me.ReposSelect})
        Me.ogcdirector.Size = New System.Drawing.Size(1201, 525)
        Me.ogcdirector.TabIndex = 21
        Me.ogcdirector.TabStop = False
        Me.ogcdirector.Tag = "2|"
        Me.ogcdirector.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvdirector})
        '
        'ogvdirector
        '
        Me.ogvdirector.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.CFNHSysCmpId, Me.CFTSelect, Me.cFTCmpCode, Me.cFTCompName, Me.cFTCustomerPO, Me.FDShipDate, Me.CFNHSysStyleId, Me.cFTStyleCode, Me.cFTInvoiceNo, Me.cFDInvoiceDate, Me.cFNCM, Me.cFNQuantity, Me.cFNAmount, Me.FTStateSendApp, Me.cFTStateSendBy, Me.FDStateSendDate, Me.FTStateApp, Me.FTStateAppBy, Me.FDStateAppDate, Me.FTStateReject, Me.FTStateRejectBy, Me.FDStateRejectDate, Me.FTStateWHApp, Me.FTStateWHAppBy, Me.FDStateWHAppDate, Me.FTStateWHReject, Me.FTStateWHRejectBy, Me.FDStateWHRejectDate, Me.FTInvoiceExportNo, Me.FDInvoiceExportDate, Me.FTInvoiceExportNote, Me.FNStateImportNetPrice, Me.FTNikePOLineItem})
        Me.ogvdirector.CustomizationFormBounds = New System.Drawing.Rectangle(758, 512, 216, 178)
        Me.ogvdirector.GridControl = Me.ogcdirector
        Me.ogvdirector.GroupCount = 1
        Me.ogvdirector.GroupSummary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FNQuantity", Me.cFNQuantity, "{0:n0}"), New DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FNExtraQuantity", Nothing, "{0:n0}"), New DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FNTotalQuantity", Nothing, "{0:n0}"), New DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FNAmount", Me.cFNAmount, "{0:n2}")})
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
        Me.ogvdirector.SortInfo.AddRange(New DevExpress.XtraGrid.Columns.GridColumnSortInfo() {New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.cFTCmpCode, DevExpress.Data.ColumnSortOrder.Ascending)})
        Me.ogvdirector.Tag = "2|"
        '
        'CFNHSysCmpId
        '
        Me.CFNHSysCmpId.Caption = "FNHSysCmpId"
        Me.CFNHSysCmpId.FieldName = "FNHSysCmpId"
        Me.CFNHSysCmpId.Name = "CFNHSysCmpId"
        Me.CFNHSysCmpId.OptionsColumn.AllowEdit = False
        Me.CFNHSysCmpId.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFNHSysCmpId.OptionsColumn.ReadOnly = True
        '
        'CFTSelect
        '
        Me.CFTSelect.Caption = "  "
        Me.CFTSelect.ColumnEdit = Me.ReposSelect
        Me.CFTSelect.FieldName = "FTSelect"
        Me.CFTSelect.Name = "CFTSelect"
        Me.CFTSelect.OptionsColumn.AllowEdit = False
        Me.CFTSelect.OptionsColumn.ReadOnly = True
        Me.CFTSelect.Width = 60
        '
        'ReposSelect
        '
        Me.ReposSelect.AutoHeight = False
        Me.ReposSelect.Caption = "Check"
        Me.ReposSelect.Name = "ReposSelect"
        Me.ReposSelect.ValueChecked = "1"
        Me.ReposSelect.ValueUnchecked = "0"
        '
        'cFTCmpCode
        '
        Me.cFTCmpCode.Caption = "FTCmpCode"
        Me.cFTCmpCode.FieldName = "FTCmpCode"
        Me.cFTCmpCode.GroupInterval = DevExpress.XtraGrid.ColumnGroupInterval.Value
        Me.cFTCmpCode.Name = "cFTCmpCode"
        Me.cFTCmpCode.OptionsColumn.AllowEdit = False
        Me.cFTCmpCode.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.cFTCmpCode.OptionsColumn.ReadOnly = True
        Me.cFTCmpCode.Visible = True
        Me.cFTCmpCode.VisibleIndex = 4
        '
        'cFTCompName
        '
        Me.cFTCompName.Caption = "FTCompName"
        Me.cFTCompName.FieldName = "FTCmpName"
        Me.cFTCompName.Name = "cFTCompName"
        Me.cFTCompName.OptionsColumn.AllowEdit = False
        Me.cFTCompName.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.cFTCompName.OptionsColumn.ReadOnly = True
        Me.cFTCompName.Visible = True
        Me.cFTCompName.VisibleIndex = 0
        Me.cFTCompName.Width = 54
        '
        'cFTCustomerPO
        '
        Me.cFTCustomerPO.Caption = "FTCustomerPO"
        Me.cFTCustomerPO.FieldName = "FTCustomerPO"
        Me.cFTCustomerPO.Name = "cFTCustomerPO"
        Me.cFTCustomerPO.OptionsColumn.AllowEdit = False
        Me.cFTCustomerPO.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.cFTCustomerPO.OptionsColumn.ReadOnly = True
        Me.cFTCustomerPO.Visible = True
        Me.cFTCustomerPO.VisibleIndex = 1
        Me.cFTCustomerPO.Width = 38
        '
        'FDShipDate
        '
        Me.FDShipDate.Caption = "Ship Date"
        Me.FDShipDate.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.FDShipDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.FDShipDate.FieldName = "FDShipDate"
        Me.FDShipDate.Name = "FDShipDate"
        Me.FDShipDate.OptionsColumn.AllowEdit = False
        Me.FDShipDate.OptionsColumn.ReadOnly = True
        Me.FDShipDate.Visible = True
        Me.FDShipDate.VisibleIndex = 3
        '
        'CFNHSysStyleId
        '
        Me.CFNHSysStyleId.Caption = "FNHSysStyleId"
        Me.CFNHSysStyleId.FieldName = "FNHSysStyleId"
        Me.CFNHSysStyleId.Name = "CFNHSysStyleId"
        Me.CFNHSysStyleId.OptionsColumn.AllowEdit = False
        Me.CFNHSysStyleId.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[True]
        Me.CFNHSysStyleId.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFNHSysStyleId.OptionsColumn.ReadOnly = True
        '
        'cFTStyleCode
        '
        Me.cFTStyleCode.Caption = "FTStyleCode"
        Me.cFTStyleCode.FieldName = "FTStyleCode"
        Me.cFTStyleCode.Name = "cFTStyleCode"
        Me.cFTStyleCode.OptionsColumn.AllowEdit = False
        Me.cFTStyleCode.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.cFTStyleCode.OptionsColumn.ReadOnly = True
        Me.cFTStyleCode.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        Me.cFTStyleCode.Visible = True
        Me.cFTStyleCode.VisibleIndex = 4
        Me.cFTStyleCode.Width = 46
        '
        'cFTInvoiceNo
        '
        Me.cFTInvoiceNo.Caption = "FTInvoiceNo"
        Me.cFTInvoiceNo.FieldName = "FTInvoiceNo"
        Me.cFTInvoiceNo.Name = "cFTInvoiceNo"
        Me.cFTInvoiceNo.OptionsColumn.AllowEdit = False
        Me.cFTInvoiceNo.OptionsColumn.ReadOnly = True
        Me.cFTInvoiceNo.Visible = True
        Me.cFTInvoiceNo.VisibleIndex = 5
        Me.cFTInvoiceNo.Width = 40
        '
        'cFDInvoiceDate
        '
        Me.cFDInvoiceDate.Caption = "FDInvoiceDate"
        Me.cFDInvoiceDate.FieldName = "FDInvoiceDate"
        Me.cFDInvoiceDate.Name = "cFDInvoiceDate"
        Me.cFDInvoiceDate.OptionsColumn.AllowEdit = False
        Me.cFDInvoiceDate.OptionsColumn.ReadOnly = True
        Me.cFDInvoiceDate.Visible = True
        Me.cFDInvoiceDate.VisibleIndex = 6
        Me.cFDInvoiceDate.Width = 38
        '
        'cFNCM
        '
        Me.cFNCM.Caption = "CM Price"
        Me.cFNCM.DisplayFormat.FormatString = "{0:n4}"
        Me.cFNCM.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cFNCM.FieldName = "FNCM"
        Me.cFNCM.Name = "cFNCM"
        Me.cFNCM.OptionsColumn.AllowEdit = False
        Me.cFNCM.OptionsColumn.ReadOnly = True
        Me.cFNCM.Visible = True
        Me.cFNCM.VisibleIndex = 7
        Me.cFNCM.Width = 52
        '
        'cFNQuantity
        '
        Me.cFNQuantity.Caption = "FNQuantity"
        Me.cFNQuantity.ColumnEdit = Me.RepositoryItemTextEdit2
        Me.cFNQuantity.DisplayFormat.FormatString = "{0:n0}"
        Me.cFNQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cFNQuantity.FieldName = "FNQuantity"
        Me.cFNQuantity.Name = "cFNQuantity"
        Me.cFNQuantity.OptionsColumn.AllowEdit = False
        Me.cFNQuantity.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.cFNQuantity.OptionsColumn.ReadOnly = True
        Me.cFNQuantity.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        Me.cFNQuantity.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FNQuantity", "{0:n0}")})
        Me.cFNQuantity.Visible = True
        Me.cFNQuantity.VisibleIndex = 8
        Me.cFNQuantity.Width = 38
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
        'cFNAmount
        '
        Me.cFNAmount.Caption = "Amount"
        Me.cFNAmount.DisplayFormat.FormatString = "{0:n2}"
        Me.cFNAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cFNAmount.FieldName = "FNAmount"
        Me.cFNAmount.Name = "cFNAmount"
        Me.cFNAmount.OptionsColumn.AllowEdit = False
        Me.cFNAmount.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.cFNAmount.OptionsColumn.ReadOnly = True
        Me.cFNAmount.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FNAmount", "{0:n2}")})
        Me.cFNAmount.Visible = True
        Me.cFNAmount.VisibleIndex = 9
        Me.cFNAmount.Width = 38
        '
        'FTStateSendApp
        '
        Me.FTStateSendApp.Caption = "FTStateSendApp"
        Me.FTStateSendApp.ColumnEdit = Me.ReposSelect
        Me.FTStateSendApp.FieldName = "FTStateSendApp"
        Me.FTStateSendApp.Name = "FTStateSendApp"
        Me.FTStateSendApp.OptionsColumn.AllowEdit = False
        Me.FTStateSendApp.OptionsColumn.ReadOnly = True
        Me.FTStateSendApp.Visible = True
        Me.FTStateSendApp.VisibleIndex = 10
        Me.FTStateSendApp.Width = 60
        '
        'cFTStateSendBy
        '
        Me.cFTStateSendBy.Caption = "FTStateSendBy"
        Me.cFTStateSendBy.FieldName = "FTStateSendBy"
        Me.cFTStateSendBy.Name = "cFTStateSendBy"
        Me.cFTStateSendBy.OptionsColumn.AllowEdit = False
        Me.cFTStateSendBy.OptionsColumn.ReadOnly = True
        Me.cFTStateSendBy.Visible = True
        Me.cFTStateSendBy.VisibleIndex = 11
        Me.cFTStateSendBy.Width = 100
        '
        'FDStateSendDate
        '
        Me.FDStateSendDate.Caption = "FDStateSendDate"
        Me.FDStateSendDate.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.FDStateSendDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.FDStateSendDate.FieldName = "FDStateSendDate"
        Me.FDStateSendDate.Name = "FDStateSendDate"
        Me.FDStateSendDate.OptionsColumn.AllowEdit = False
        Me.FDStateSendDate.OptionsColumn.ReadOnly = True
        Me.FDStateSendDate.Visible = True
        Me.FDStateSendDate.VisibleIndex = 12
        Me.FDStateSendDate.Width = 80
        '
        'FTStateApp
        '
        Me.FTStateApp.Caption = "FTStateApp"
        Me.FTStateApp.ColumnEdit = Me.ReposSelect
        Me.FTStateApp.FieldName = "FTStateApp"
        Me.FTStateApp.Name = "FTStateApp"
        Me.FTStateApp.OptionsColumn.AllowEdit = False
        Me.FTStateApp.OptionsColumn.ReadOnly = True
        Me.FTStateApp.Visible = True
        Me.FTStateApp.VisibleIndex = 13
        Me.FTStateApp.Width = 60
        '
        'FTStateAppBy
        '
        Me.FTStateAppBy.Caption = "FTStateAppBy"
        Me.FTStateAppBy.FieldName = "FTStateAppBy"
        Me.FTStateAppBy.Name = "FTStateAppBy"
        Me.FTStateAppBy.OptionsColumn.AllowEdit = False
        Me.FTStateAppBy.OptionsColumn.ReadOnly = True
        Me.FTStateAppBy.Visible = True
        Me.FTStateAppBy.VisibleIndex = 14
        Me.FTStateAppBy.Width = 100
        '
        'FDStateAppDate
        '
        Me.FDStateAppDate.Caption = "FDStateAppDate"
        Me.FDStateAppDate.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.FDStateAppDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.FDStateAppDate.FieldName = "FDStateAppDate"
        Me.FDStateAppDate.Name = "FDStateAppDate"
        Me.FDStateAppDate.OptionsColumn.AllowEdit = False
        Me.FDStateAppDate.OptionsColumn.ReadOnly = True
        Me.FDStateAppDate.Visible = True
        Me.FDStateAppDate.VisibleIndex = 15
        Me.FDStateAppDate.Width = 80
        '
        'FTStateReject
        '
        Me.FTStateReject.Caption = "FTStateReject"
        Me.FTStateReject.ColumnEdit = Me.ReposSelect
        Me.FTStateReject.FieldName = "FTStateReject"
        Me.FTStateReject.Name = "FTStateReject"
        Me.FTStateReject.OptionsColumn.AllowEdit = False
        Me.FTStateReject.OptionsColumn.ReadOnly = True
        Me.FTStateReject.Visible = True
        Me.FTStateReject.VisibleIndex = 16
        Me.FTStateReject.Width = 60
        '
        'FTStateRejectBy
        '
        Me.FTStateRejectBy.Caption = "FTStateRejectBy"
        Me.FTStateRejectBy.FieldName = "FTStateRejectBy"
        Me.FTStateRejectBy.Name = "FTStateRejectBy"
        Me.FTStateRejectBy.OptionsColumn.AllowEdit = False
        Me.FTStateRejectBy.OptionsColumn.ReadOnly = True
        Me.FTStateRejectBy.Visible = True
        Me.FTStateRejectBy.VisibleIndex = 17
        Me.FTStateRejectBy.Width = 100
        '
        'FDStateRejectDate
        '
        Me.FDStateRejectDate.Caption = "FDStateRejectDate"
        Me.FDStateRejectDate.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.FDStateRejectDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.FDStateRejectDate.FieldName = "FDStateRejectDate"
        Me.FDStateRejectDate.Name = "FDStateRejectDate"
        Me.FDStateRejectDate.OptionsColumn.AllowEdit = False
        Me.FDStateRejectDate.OptionsColumn.ReadOnly = True
        Me.FDStateRejectDate.Visible = True
        Me.FDStateRejectDate.VisibleIndex = 18
        Me.FDStateRejectDate.Width = 80
        '
        'FTStateWHApp
        '
        Me.FTStateWHApp.Caption = "FTStateWHApp"
        Me.FTStateWHApp.ColumnEdit = Me.ReposSelect
        Me.FTStateWHApp.FieldName = "FTStateWHApp"
        Me.FTStateWHApp.Name = "FTStateWHApp"
        Me.FTStateWHApp.OptionsColumn.AllowEdit = False
        Me.FTStateWHApp.OptionsColumn.ReadOnly = True
        Me.FTStateWHApp.Visible = True
        Me.FTStateWHApp.VisibleIndex = 19
        Me.FTStateWHApp.Width = 60
        '
        'FTStateWHAppBy
        '
        Me.FTStateWHAppBy.Caption = "FTStateWHAppBy"
        Me.FTStateWHAppBy.FieldName = "FTStateWHAppBy"
        Me.FTStateWHAppBy.Name = "FTStateWHAppBy"
        Me.FTStateWHAppBy.OptionsColumn.AllowEdit = False
        Me.FTStateWHAppBy.OptionsColumn.ReadOnly = True
        Me.FTStateWHAppBy.Visible = True
        Me.FTStateWHAppBy.VisibleIndex = 20
        Me.FTStateWHAppBy.Width = 100
        '
        'FDStateWHAppDate
        '
        Me.FDStateWHAppDate.Caption = "FDStateWHAppDate"
        Me.FDStateWHAppDate.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.FDStateWHAppDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.FDStateWHAppDate.FieldName = "FDStateWHAppDate"
        Me.FDStateWHAppDate.Name = "FDStateWHAppDate"
        Me.FDStateWHAppDate.OptionsColumn.AllowEdit = False
        Me.FDStateWHAppDate.OptionsColumn.ReadOnly = True
        Me.FDStateWHAppDate.Visible = True
        Me.FDStateWHAppDate.VisibleIndex = 21
        Me.FDStateWHAppDate.Width = 80
        '
        'FTStateWHReject
        '
        Me.FTStateWHReject.Caption = "FTStateWHReject"
        Me.FTStateWHReject.ColumnEdit = Me.ReposSelect
        Me.FTStateWHReject.FieldName = "FTStateWHReject"
        Me.FTStateWHReject.Name = "FTStateWHReject"
        Me.FTStateWHReject.OptionsColumn.AllowEdit = False
        Me.FTStateWHReject.OptionsColumn.ReadOnly = True
        Me.FTStateWHReject.Visible = True
        Me.FTStateWHReject.VisibleIndex = 22
        Me.FTStateWHReject.Width = 60
        '
        'FTStateWHRejectBy
        '
        Me.FTStateWHRejectBy.Caption = "FTStateWHRejectBy"
        Me.FTStateWHRejectBy.FieldName = "FTStateWHRejectBy"
        Me.FTStateWHRejectBy.Name = "FTStateWHRejectBy"
        Me.FTStateWHRejectBy.OptionsColumn.AllowEdit = False
        Me.FTStateWHRejectBy.OptionsColumn.ReadOnly = True
        Me.FTStateWHRejectBy.Visible = True
        Me.FTStateWHRejectBy.VisibleIndex = 23
        Me.FTStateWHRejectBy.Width = 100
        '
        'FDStateWHRejectDate
        '
        Me.FDStateWHRejectDate.Caption = "FDStateWHRejectDate"
        Me.FDStateWHRejectDate.FieldName = "FDStateWHRejectDate"
        Me.FDStateWHRejectDate.Name = "FDStateWHRejectDate"
        Me.FDStateWHRejectDate.OptionsColumn.AllowEdit = False
        Me.FDStateWHRejectDate.OptionsColumn.ReadOnly = True
        Me.FDStateWHRejectDate.Visible = True
        Me.FDStateWHRejectDate.VisibleIndex = 24
        Me.FDStateWHRejectDate.Width = 80
        '
        'FTInvoiceExportNo
        '
        Me.FTInvoiceExportNo.Caption = "FTInvoiceExportNo"
        Me.FTInvoiceExportNo.FieldName = "FTInvoiceExportNo"
        Me.FTInvoiceExportNo.Name = "FTInvoiceExportNo"
        Me.FTInvoiceExportNo.OptionsColumn.AllowEdit = False
        Me.FTInvoiceExportNo.OptionsColumn.ReadOnly = True
        Me.FTInvoiceExportNo.Visible = True
        Me.FTInvoiceExportNo.VisibleIndex = 25
        Me.FTInvoiceExportNo.Width = 100
        '
        'FDInvoiceExportDate
        '
        Me.FDInvoiceExportDate.Caption = "FDInvoiceExportDate"
        Me.FDInvoiceExportDate.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.FDInvoiceExportDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.FDInvoiceExportDate.FieldName = "FDInvoiceExportDate"
        Me.FDInvoiceExportDate.Name = "FDInvoiceExportDate"
        Me.FDInvoiceExportDate.OptionsColumn.AllowEdit = False
        Me.FDInvoiceExportDate.OptionsColumn.ReadOnly = True
        Me.FDInvoiceExportDate.Visible = True
        Me.FDInvoiceExportDate.VisibleIndex = 26
        Me.FDInvoiceExportDate.Width = 80
        '
        'FTInvoiceExportNote
        '
        Me.FTInvoiceExportNote.Caption = "FTInvoiceExportNote"
        Me.FTInvoiceExportNote.FieldName = "FTInvoiceExportNote"
        Me.FTInvoiceExportNote.Name = "FTInvoiceExportNote"
        Me.FTInvoiceExportNote.OptionsColumn.AllowEdit = False
        Me.FTInvoiceExportNote.OptionsColumn.ReadOnly = True
        Me.FTInvoiceExportNote.Visible = True
        Me.FTInvoiceExportNote.VisibleIndex = 27
        Me.FTInvoiceExportNote.Width = 150
        '
        'FNStateImportNetPrice
        '
        Me.FNStateImportNetPrice.Caption = "FNStateImportNetPrice"
        Me.FNStateImportNetPrice.FieldName = "FNStateImportNetPrice"
        Me.FNStateImportNetPrice.Name = "FNStateImportNetPrice"
        '
        'otpCustomerPowiating
        '
        Me.otpCustomerPowiating.Controls.Add(Me.GroupControl1)
        Me.otpCustomerPowiating.Name = "otpCustomerPowiating"
        Me.otpCustomerPowiating.Size = New System.Drawing.Size(1227, 700)
        Me.otpCustomerPowiating.Text = "Customer Po Wiating MI Before Ship 15 Day"
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Me.ogcpowaiting)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl1.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(1227, 700)
        Me.GroupControl1.TabIndex = 2
        '
        'ogcpowaiting
        '
        Me.ogcpowaiting.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogcpowaiting.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcpowaiting.Location = New System.Drawing.Point(2, 25)
        Me.ogcpowaiting.MainView = Me.ogvpowaiting
        Me.ogcpowaiting.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcpowaiting.Name = "ogcpowaiting"
        Me.ogcpowaiting.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemTextEdit1, Me.RepositoryItemCheckEdit1})
        Me.ogcpowaiting.Size = New System.Drawing.Size(1223, 673)
        Me.ogcpowaiting.TabIndex = 21
        Me.ogcpowaiting.TabStop = False
        Me.ogcpowaiting.Tag = "2|"
        Me.ogcpowaiting.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvpowaiting})
        '
        'ogvpowaiting
        '
        Me.ogvpowaiting.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.C2FTCmpCode, Me.C2FTCmpName, Me.FTPOref, Me.CFDShipDate, Me.CFNHSysCustId, Me.FTCustCode, Me.CFTCustName, Me.FNHSysContinentId, Me.FTContinentCode, Me.FTContinentName, Me.CFNHSysCountryId, Me.CFTCountryCode, Me.CFTCountryName, Me.CFNHSysProvinceId, Me.CFTProvinceCode, Me.FTProvinceName, Me.CFNHSysShipModeId, Me.CFTShipModeCode, Me.CFNHSysShipPortId, Me.CFTShipPortCode, Me.C2FNQuantity, Me.CFNStateImportNetPrice})
        Me.ogvpowaiting.CustomizationFormBounds = New System.Drawing.Rectangle(758, 512, 216, 178)
        Me.ogvpowaiting.GridControl = Me.ogcpowaiting
        Me.ogvpowaiting.GroupSummary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FNExtraQuantity", Nothing, "{0:n0}"), New DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FNTotalQuantity", Nothing, "{0:n0}")})
        Me.ogvpowaiting.Name = "ogvpowaiting"
        Me.ogvpowaiting.OptionsBehavior.AutoExpandAllGroups = True
        Me.ogvpowaiting.OptionsCustomization.AllowQuickHideColumns = False
        Me.ogvpowaiting.OptionsNavigation.EnterMoveNextColumn = True
        Me.ogvpowaiting.OptionsView.ColumnAutoWidth = False
        Me.ogvpowaiting.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.ogvpowaiting.OptionsView.ShowAutoFilterRow = True
        Me.ogvpowaiting.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
        Me.ogvpowaiting.OptionsView.ShowFooter = True
        Me.ogvpowaiting.OptionsView.ShowGroupPanel = False
        Me.ogvpowaiting.Tag = "2|"
        '
        'C2FTCmpCode
        '
        Me.C2FTCmpCode.Caption = "Cmp Code"
        Me.C2FTCmpCode.FieldName = "FTCmpCode"
        Me.C2FTCmpCode.Name = "C2FTCmpCode"
        Me.C2FTCmpCode.OptionsColumn.AllowEdit = False
        Me.C2FTCmpCode.OptionsColumn.ReadOnly = True
        Me.C2FTCmpCode.Visible = True
        Me.C2FTCmpCode.VisibleIndex = 0
        Me.C2FTCmpCode.Width = 69
        '
        'C2FTCmpName
        '
        Me.C2FTCmpName.Caption = "FTCmpName"
        Me.C2FTCmpName.FieldName = "FTCmpName"
        Me.C2FTCmpName.Name = "C2FTCmpName"
        Me.C2FTCmpName.OptionsColumn.AllowEdit = False
        Me.C2FTCmpName.OptionsColumn.ReadOnly = True
        Me.C2FTCmpName.Visible = True
        Me.C2FTCmpName.VisibleIndex = 1
        Me.C2FTCmpName.Width = 150
        '
        'FTPOref
        '
        Me.FTPOref.Caption = "Customer PO"
        Me.FTPOref.FieldName = "FTPOref"
        Me.FTPOref.Name = "FTPOref"
        Me.FTPOref.OptionsColumn.AllowEdit = False
        Me.FTPOref.OptionsColumn.ReadOnly = True
        Me.FTPOref.Visible = True
        Me.FTPOref.VisibleIndex = 2
        Me.FTPOref.Width = 120
        '
        'CFDShipDate
        '
        Me.CFDShipDate.Caption = "Ship Date"
        Me.CFDShipDate.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.CFDShipDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.CFDShipDate.FieldName = "FDShipDate"
        Me.CFDShipDate.Name = "CFDShipDate"
        Me.CFDShipDate.OptionsColumn.AllowEdit = False
        Me.CFDShipDate.OptionsColumn.ReadOnly = True
        Me.CFDShipDate.Visible = True
        Me.CFDShipDate.VisibleIndex = 3
        Me.CFDShipDate.Width = 100
        '
        'CFNHSysCustId
        '
        Me.CFNHSysCustId.Caption = "FNHSysCustId"
        Me.CFNHSysCustId.FieldName = "FNHSysCustId"
        Me.CFNHSysCustId.Name = "CFNHSysCustId"
        Me.CFNHSysCustId.OptionsColumn.AllowEdit = False
        Me.CFNHSysCustId.OptionsColumn.ReadOnly = True
        '
        'FTCustCode
        '
        Me.FTCustCode.Caption = "Cus Code"
        Me.FTCustCode.FieldName = "FTCustCode"
        Me.FTCustCode.Name = "FTCustCode"
        Me.FTCustCode.OptionsColumn.AllowEdit = False
        Me.FTCustCode.OptionsColumn.ReadOnly = True
        Me.FTCustCode.Visible = True
        Me.FTCustCode.VisibleIndex = 4
        Me.FTCustCode.Width = 80
        '
        'CFTCustName
        '
        Me.CFTCustName.Caption = "Cust Name"
        Me.CFTCustName.FieldName = "FTCustName"
        Me.CFTCustName.Name = "CFTCustName"
        Me.CFTCustName.OptionsColumn.AllowEdit = False
        Me.CFTCustName.OptionsColumn.ReadOnly = True
        Me.CFTCustName.Visible = True
        Me.CFTCustName.VisibleIndex = 5
        Me.CFTCustName.Width = 150
        '
        'FNHSysContinentId
        '
        Me.FNHSysContinentId.Caption = "FNHSysContinentId"
        Me.FNHSysContinentId.FieldName = "FNHSysContinentId"
        Me.FNHSysContinentId.Name = "FNHSysContinentId"
        Me.FNHSysContinentId.OptionsColumn.AllowEdit = False
        Me.FNHSysContinentId.OptionsColumn.ReadOnly = True
        '
        'FTContinentCode
        '
        Me.FTContinentCode.Caption = "FTContinentCode"
        Me.FTContinentCode.FieldName = "FTContinentCode"
        Me.FTContinentCode.Name = "FTContinentCode"
        Me.FTContinentCode.OptionsColumn.AllowEdit = False
        Me.FTContinentCode.OptionsColumn.ReadOnly = True
        Me.FTContinentCode.Visible = True
        Me.FTContinentCode.VisibleIndex = 6
        Me.FTContinentCode.Width = 80
        '
        'FTContinentName
        '
        Me.FTContinentName.Caption = "FTContinentName"
        Me.FTContinentName.FieldName = "FTContinentName"
        Me.FTContinentName.Name = "FTContinentName"
        Me.FTContinentName.OptionsColumn.AllowEdit = False
        Me.FTContinentName.OptionsColumn.ReadOnly = True
        Me.FTContinentName.Visible = True
        Me.FTContinentName.VisibleIndex = 7
        Me.FTContinentName.Width = 120
        '
        'CFNHSysCountryId
        '
        Me.CFNHSysCountryId.Caption = "FNHSysCountryId"
        Me.CFNHSysCountryId.FieldName = "FNHSysCountryId"
        Me.CFNHSysCountryId.Name = "CFNHSysCountryId"
        Me.CFNHSysCountryId.OptionsColumn.AllowEdit = False
        Me.CFNHSysCountryId.OptionsColumn.ReadOnly = True
        '
        'CFTCountryCode
        '
        Me.CFTCountryCode.Caption = "FTCountryCode"
        Me.CFTCountryCode.FieldName = "FTCountryCode"
        Me.CFTCountryCode.Name = "CFTCountryCode"
        Me.CFTCountryCode.OptionsColumn.AllowEdit = False
        Me.CFTCountryCode.OptionsColumn.ReadOnly = True
        Me.CFTCountryCode.Visible = True
        Me.CFTCountryCode.VisibleIndex = 8
        Me.CFTCountryCode.Width = 80
        '
        'CFTCountryName
        '
        Me.CFTCountryName.Caption = "FTCountryName"
        Me.CFTCountryName.FieldName = "FTCountryName"
        Me.CFTCountryName.Name = "CFTCountryName"
        Me.CFTCountryName.OptionsColumn.AllowEdit = False
        Me.CFTCountryName.OptionsColumn.ReadOnly = True
        Me.CFTCountryName.Visible = True
        Me.CFTCountryName.VisibleIndex = 9
        Me.CFTCountryName.Width = 120
        '
        'CFNHSysProvinceId
        '
        Me.CFNHSysProvinceId.Caption = "FNHSysProvinceId"
        Me.CFNHSysProvinceId.FieldName = "FNHSysProvinceId"
        Me.CFNHSysProvinceId.Name = "CFNHSysProvinceId"
        Me.CFNHSysProvinceId.OptionsColumn.AllowEdit = False
        Me.CFNHSysProvinceId.OptionsColumn.ReadOnly = True
        '
        'CFTProvinceCode
        '
        Me.CFTProvinceCode.Caption = "Province"
        Me.CFTProvinceCode.FieldName = "FTProvinceCode"
        Me.CFTProvinceCode.Name = "CFTProvinceCode"
        Me.CFTProvinceCode.OptionsColumn.AllowEdit = False
        Me.CFTProvinceCode.OptionsColumn.ReadOnly = True
        Me.CFTProvinceCode.Visible = True
        Me.CFTProvinceCode.VisibleIndex = 10
        Me.CFTProvinceCode.Width = 80
        '
        'FTProvinceName
        '
        Me.FTProvinceName.Caption = "FTProvinceName"
        Me.FTProvinceName.FieldName = "FTProvinceName"
        Me.FTProvinceName.Name = "FTProvinceName"
        Me.FTProvinceName.OptionsColumn.AllowEdit = False
        Me.FTProvinceName.OptionsColumn.ReadOnly = True
        Me.FTProvinceName.Visible = True
        Me.FTProvinceName.VisibleIndex = 11
        Me.FTProvinceName.Width = 120
        '
        'CFNHSysShipModeId
        '
        Me.CFNHSysShipModeId.Caption = "FNHSysShipModeId"
        Me.CFNHSysShipModeId.FieldName = "FNHSysShipModeId"
        Me.CFNHSysShipModeId.Name = "CFNHSysShipModeId"
        Me.CFNHSysShipModeId.OptionsColumn.AllowEdit = False
        Me.CFNHSysShipModeId.OptionsColumn.ReadOnly = True
        '
        'CFTShipModeCode
        '
        Me.CFTShipModeCode.Caption = "FTShipModeCode"
        Me.CFTShipModeCode.FieldName = "FTShipModeCode"
        Me.CFTShipModeCode.Name = "CFTShipModeCode"
        Me.CFTShipModeCode.OptionsColumn.AllowEdit = False
        Me.CFTShipModeCode.OptionsColumn.ReadOnly = True
        Me.CFTShipModeCode.Visible = True
        Me.CFTShipModeCode.VisibleIndex = 12
        Me.CFTShipModeCode.Width = 100
        '
        'CFNHSysShipPortId
        '
        Me.CFNHSysShipPortId.Caption = "FNHSysShipPortId"
        Me.CFNHSysShipPortId.FieldName = "FNHSysShipPortId"
        Me.CFNHSysShipPortId.Name = "CFNHSysShipPortId"
        Me.CFNHSysShipPortId.OptionsColumn.AllowEdit = False
        Me.CFNHSysShipPortId.OptionsColumn.ReadOnly = True
        '
        'CFTShipPortCode
        '
        Me.CFTShipPortCode.Caption = "ShipPort"
        Me.CFTShipPortCode.FieldName = "FTShipPortCode"
        Me.CFTShipPortCode.Name = "CFTShipPortCode"
        Me.CFTShipPortCode.OptionsColumn.AllowEdit = False
        Me.CFTShipPortCode.OptionsColumn.ReadOnly = True
        Me.CFTShipPortCode.Visible = True
        Me.CFTShipPortCode.VisibleIndex = 13
        Me.CFTShipPortCode.Width = 100
        '
        'C2FNQuantity
        '
        Me.C2FNQuantity.Caption = "Quantity"
        Me.C2FNQuantity.DisplayFormat.FormatString = "{0:n0}"
        Me.C2FNQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.C2FNQuantity.FieldName = "FNQuantity"
        Me.C2FNQuantity.Name = "C2FNQuantity"
        Me.C2FNQuantity.OptionsColumn.AllowEdit = False
        Me.C2FNQuantity.OptionsColumn.ReadOnly = True
        Me.C2FNQuantity.Visible = True
        Me.C2FNQuantity.VisibleIndex = 14
        '
        'CFNStateImportNetPrice
        '
        Me.CFNStateImportNetPrice.Caption = "FNStateImportNetPrice"
        Me.CFNStateImportNetPrice.FieldName = "FNStateImportNetPrice"
        Me.CFNStateImportNetPrice.Name = "CFNStateImportNetPrice"
        '
        'RepositoryItemTextEdit1
        '
        Me.RepositoryItemTextEdit1.AutoHeight = False
        Me.RepositoryItemTextEdit1.DisplayFormat.FormatString = "N0"
        Me.RepositoryItemTextEdit1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.RepositoryItemTextEdit1.EditFormat.FormatString = "N0"
        Me.RepositoryItemTextEdit1.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.RepositoryItemTextEdit1.Name = "RepositoryItemTextEdit1"
        '
        'RepositoryItemCheckEdit1
        '
        Me.RepositoryItemCheckEdit1.AutoHeight = False
        Me.RepositoryItemCheckEdit1.Caption = "Check"
        Me.RepositoryItemCheckEdit1.Name = "RepositoryItemCheckEdit1"
        Me.RepositoryItemCheckEdit1.ValueChecked = "1"
        Me.RepositoryItemCheckEdit1.ValueUnchecked = "0"
        '
        'ocmcheckpowaiting
        '
        Me.ocmcheckpowaiting.Interval = 120000
        '
        'FTNikePOLineItem
        '
        Me.FTNikePOLineItem.Caption = "FTNikePOLineItem"
        Me.FTNikePOLineItem.FieldName = "FTNikePOLineItem"
        Me.FTNikePOLineItem.Name = "FTNikePOLineItem"
        Me.FTNikePOLineItem.OptionsColumn.AllowEdit = False
        Me.FTNikePOLineItem.Visible = True
        Me.FTNikePOLineItem.VisibleIndex = 2
        Me.FTNikePOLineItem.Width = 44
        '
        'wFactoryCMInvoiceTracking
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(1241, 750)
        Me.Controls.Add(Me.ogbmainprocbutton)
        Me.Controls.Add(Me.otbmain)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "wFactoryCMInvoiceTracking"
        Me.Text = "Factory CM Invoice Tracking"
        CType(Me.ogbOrderCriteria, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbOrderCriteria.ResumeLayout(False)
        CType(Me.FTEndShipment.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTEndShipment.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTStartShipment.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTStartShipment.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysBuyId_None.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysBuyId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysSeasonId_None.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysSeasonId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysCustId_None.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysCustId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysCmpId_None.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysCmpId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysStyleId_None.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysStyleId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
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
        CType(Me.RepositoryItemTextEdit2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.otpCustomerPowiating.ResumeLayout(False)
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        CType(Me.ogcpowaiting, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvpowaiting, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogbOrderCriteria As DevExpress.XtraEditors.GroupControl
    Friend WithEvents FNHSysSeasonId_None As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FNHSysSeasonId_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysSeasonId As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FNHSysCustId_None As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FNHSysCustId_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysCustId As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FNHSysCmpId_None As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FNHSysCmpId_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysCmpId As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FNHSysStyleId_None As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FNHSysStyleId_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysStyleId As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FTPORef_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysPOID As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FNHSysBuyId_None As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FNHSysBuyId_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysBuyId As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents ogbmainprocbutton As DevExpress.XtraEditors.PanelControl
    Friend WithEvents ocmclearclsr As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmpreview As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmrefresh As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents otbmain As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents otpfactorycminvoice As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents ogbdirectorapp As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ogcdirector As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvdirector As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents CFNHSysCmpId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFTSelect As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ReposSelect As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents cFTCmpCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTCompName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTCustomerPO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFNHSysStyleId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTStyleCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTInvoiceNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFDInvoiceDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNCM As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemTextEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents cFNAmount As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTStateSendBy As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTStateSendApp As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FDStateSendDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTStateApp As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTStateAppBy As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FDStateAppDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTStateReject As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTStateRejectBy As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FDStateRejectDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTStateWHApp As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTStateWHAppBy As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FDStateWHAppDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTStateWHReject As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTStateWHRejectBy As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FDStateWHRejectDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTInvoiceExportNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FDInvoiceExportDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTInvoiceExportNote As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FDShipDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents otpCustomerPowiating As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ogcpowaiting As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvpowaiting As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents RepositoryItemTextEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents RepositoryItemCheckEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents ocmcheckpowaiting As System.Windows.Forms.Timer
    Friend WithEvents FTPOref As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFDShipDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFNHSysCustId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTCustCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFTCustName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNHSysContinentId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTContinentCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTContinentName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFNHSysCountryId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFTCountryCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFTCountryName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFNHSysProvinceId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFTProvinceCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTProvinceName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFNHSysShipModeId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFTShipModeCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFNHSysShipPortId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFTShipPortCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents C2FNQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents C2FTCmpCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents C2FTCmpName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ocmpreviewtvwthb As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FTEndShipment As DevExpress.XtraEditors.DateEdit
    Friend WithEvents FTEndShipment_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTStartShipment As DevExpress.XtraEditors.DateEdit
    Friend WithEvents FTStartShipment_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNStateImportNetPrice As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFNStateImportNetPrice As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTNikePOLineItem As DevExpress.XtraGrid.Columns.GridColumn
End Class

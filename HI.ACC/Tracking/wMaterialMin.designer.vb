<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wMaterialMin
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
        Me.DockManager1 = New DevExpress.XtraBars.Docking.DockManager()
        Me.oDockPanel = New DevExpress.XtraBars.Docking.DockPanel()
        Me.DockPanel1_Container = New DevExpress.XtraBars.Docking.ControlContainer()
        Me.FTOrderNoTo = New DevExpress.XtraEditors.ButtonEdit()
        Me.FTOrderNo = New DevExpress.XtraEditors.ButtonEdit()
        Me.FTOrderNo_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTOrderNoTo_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.PODateEndDate_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FDPODateEnd = New DevExpress.XtraEditors.DateEdit()
        Me.PODateStart_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FDPODateStart = New DevExpress.XtraEditors.DateEdit()
        Me.ogb = New DevExpress.XtraEditors.GroupControl()
        Me.ogbmainprocbutton = New DevExpress.XtraEditors.PanelControl()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmclear = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmload = New DevExpress.XtraEditors.SimpleButton()
        Me.ogcDetail = New DevExpress.XtraGrid.GridControl()
        Me.ogvDetail = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.FTCmpCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTCustCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTOrderNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FDShipDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTMerTeamCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTPurchaseNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFDPurchaseDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.PoStateName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTRawMatCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTRawMatName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTRawMatColorCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTRawMatColorName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTRawMatSizeCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTFabricFrontSize = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTUnitCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTMatType = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNPrice = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNAllowcate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.TotalMoney = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cRecFNPrice = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cPurFNPrice = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.DockManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.oDockPanel.SuspendLayout()
        Me.DockPanel1_Container.SuspendLayout()
        CType(Me.FTOrderNoTo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTOrderNo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FDPODateEnd.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FDPODateEnd.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FDPODateStart.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FDPODateStart.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogb, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogb.SuspendLayout()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbmainprocbutton.SuspendLayout()
        CType(Me.ogcDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DockManager1
        '
        Me.DockManager1.Form = Me
        Me.DockManager1.RootPanels.AddRange(New DevExpress.XtraBars.Docking.DockPanel() {Me.oDockPanel})
        Me.DockManager1.TopZIndexControls.AddRange(New String() {"DevExpress.XtraBars.BarDockControl", "DevExpress.XtraBars.StandaloneBarDockControl", "System.Windows.Forms.StatusBar", "System.Windows.Forms.MenuStrip", "System.Windows.Forms.StatusStrip", "DevExpress.XtraBars.Ribbon.RibbonStatusBar", "DevExpress.XtraBars.Ribbon.RibbonControl"})
        '
        'oDockPanel
        '
        Me.oDockPanel.Controls.Add(Me.DockPanel1_Container)
        Me.oDockPanel.Dock = DevExpress.XtraBars.Docking.DockingStyle.Top
        Me.oDockPanel.ID = New System.Guid("2e27cc4a-6355-4141-8a0d-352e44581686")
        Me.oDockPanel.Location = New System.Drawing.Point(0, 0)
        Me.oDockPanel.Name = "oDockPanel"
        Me.oDockPanel.Options.AllowDockAsTabbedDocument = False
        Me.oDockPanel.Options.AllowDockBottom = False
        Me.oDockPanel.Options.AllowDockFill = False
        Me.oDockPanel.Options.AllowDockLeft = False
        Me.oDockPanel.Options.AllowDockRight = False
        Me.oDockPanel.Options.AllowFloating = False
        Me.oDockPanel.Options.ShowCloseButton = False
        Me.oDockPanel.Options.ShowMaximizeButton = False
        Me.oDockPanel.OriginalSize = New System.Drawing.Size(200, 99)
        Me.oDockPanel.Size = New System.Drawing.Size(1084, 99)
        Me.oDockPanel.Text = "Criteria"
        '
        'DockPanel1_Container
        '
        Me.DockPanel1_Container.Controls.Add(Me.FTOrderNoTo)
        Me.DockPanel1_Container.Controls.Add(Me.FTOrderNo)
        Me.DockPanel1_Container.Controls.Add(Me.FTOrderNo_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FTOrderNoTo_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.PODateEndDate_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FDPODateEnd)
        Me.DockPanel1_Container.Controls.Add(Me.PODateStart_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FDPODateStart)
        Me.DockPanel1_Container.Location = New System.Drawing.Point(4, 23)
        Me.DockPanel1_Container.Name = "DockPanel1_Container"
        Me.DockPanel1_Container.Size = New System.Drawing.Size(1076, 72)
        Me.DockPanel1_Container.TabIndex = 0
        '
        'FTOrderNoTo
        '
        Me.FTOrderNoTo.Location = New System.Drawing.Point(436, 43)
        Me.FTOrderNoTo.Name = "FTOrderNoTo"
        Me.FTOrderNoTo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", "350", Nothing, True)})
        Me.FTOrderNoTo.Size = New System.Drawing.Size(145, 20)
        Me.FTOrderNoTo.TabIndex = 456
        Me.FTOrderNoTo.Tag = "2|"
        '
        'FTOrderNo
        '
        Me.FTOrderNo.Location = New System.Drawing.Point(155, 43)
        Me.FTOrderNo.Name = "FTOrderNo"
        Me.FTOrderNo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject2, "", "349", Nothing, True)})
        Me.FTOrderNo.Size = New System.Drawing.Size(145, 20)
        Me.FTOrderNo.TabIndex = 455
        Me.FTOrderNo.Tag = "2|"
        '
        'FTOrderNo_lbl
        '
        Me.FTOrderNo_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTOrderNo_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTOrderNo_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTOrderNo_lbl.Location = New System.Drawing.Point(25, 44)
        Me.FTOrderNo_lbl.Name = "FTOrderNo_lbl"
        Me.FTOrderNo_lbl.Size = New System.Drawing.Size(127, 18)
        Me.FTOrderNo_lbl.TabIndex = 454
        Me.FTOrderNo_lbl.Tag = "2|"
        Me.FTOrderNo_lbl.Text = "FTOrderNo :"
        '
        'FTOrderNoTo_lbl
        '
        Me.FTOrderNoTo_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTOrderNoTo_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTOrderNoTo_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTOrderNoTo_lbl.Location = New System.Drawing.Point(306, 44)
        Me.FTOrderNoTo_lbl.Name = "FTOrderNoTo_lbl"
        Me.FTOrderNoTo_lbl.Size = New System.Drawing.Size(127, 18)
        Me.FTOrderNoTo_lbl.TabIndex = 452
        Me.FTOrderNoTo_lbl.Tag = "2|"
        Me.FTOrderNoTo_lbl.Text = "FTOrderNoTo :"
        '
        'PODateEndDate_lbl
        '
        Me.PODateEndDate_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.PODateEndDate_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.PODateEndDate_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.PODateEndDate_lbl.Location = New System.Drawing.Point(306, 20)
        Me.PODateEndDate_lbl.Name = "PODateEndDate_lbl"
        Me.PODateEndDate_lbl.Size = New System.Drawing.Size(127, 19)
        Me.PODateEndDate_lbl.TabIndex = 433
        Me.PODateEndDate_lbl.Tag = "2|"
        Me.PODateEndDate_lbl.Text = "POToDate :"
        '
        'FDPODateEnd
        '
        Me.FDPODateEnd.EditValue = Nothing
        Me.FDPODateEnd.EnterMoveNextControl = True
        Me.FDPODateEnd.Location = New System.Drawing.Point(436, 21)
        Me.FDPODateEnd.Name = "FDPODateEnd"
        Me.FDPODateEnd.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.FDPODateEnd.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FDPODateEnd.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.FDPODateEnd.Properties.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.FDPODateEnd.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FDPODateEnd.Properties.EditFormat.FormatString = "dd/MM/yyyy"
        Me.FDPODateEnd.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FDPODateEnd.Properties.NullDate = ""
        Me.FDPODateEnd.Size = New System.Drawing.Size(145, 20)
        Me.FDPODateEnd.TabIndex = 432
        Me.FDPODateEnd.Tag = "2|"
        '
        'PODateStart_lbl
        '
        Me.PODateStart_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.PODateStart_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.PODateStart_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.PODateStart_lbl.Location = New System.Drawing.Point(25, 20)
        Me.PODateStart_lbl.Name = "PODateStart_lbl"
        Me.PODateStart_lbl.Size = New System.Drawing.Size(127, 19)
        Me.PODateStart_lbl.TabIndex = 433
        Me.PODateStart_lbl.Tag = "2|"
        Me.PODateStart_lbl.Text = "PODate :"
        '
        'FDPODateStart
        '
        Me.FDPODateStart.EditValue = Nothing
        Me.FDPODateStart.EnterMoveNextControl = True
        Me.FDPODateStart.Location = New System.Drawing.Point(155, 21)
        Me.FDPODateStart.Name = "FDPODateStart"
        Me.FDPODateStart.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.FDPODateStart.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FDPODateStart.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.FDPODateStart.Properties.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.FDPODateStart.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FDPODateStart.Properties.EditFormat.FormatString = "dd/MM/yyyy"
        Me.FDPODateStart.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FDPODateStart.Properties.NullDate = ""
        Me.FDPODateStart.Size = New System.Drawing.Size(145, 20)
        Me.FDPODateStart.TabIndex = 432
        Me.FDPODateStart.Tag = "2|"
        '
        'ogb
        '
        Me.ogb.Controls.Add(Me.ogbmainprocbutton)
        Me.ogb.Controls.Add(Me.ogcDetail)
        Me.ogb.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogb.Location = New System.Drawing.Point(0, 99)
        Me.ogb.Name = "ogb"
        Me.ogb.Size = New System.Drawing.Size(1084, 462)
        Me.ogb.TabIndex = 1
        Me.ogb.Text = "GroupControl1"
        '
        'ogbmainprocbutton
        '
        Me.ogbmainprocbutton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmexit)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmclear)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmload)
        Me.ogbmainprocbutton.Location = New System.Drawing.Point(136, 88)
        Me.ogbmainprocbutton.Name = "ogbmainprocbutton"
        Me.ogbmainprocbutton.Size = New System.Drawing.Size(678, 47)
        Me.ogbmainprocbutton.TabIndex = 390
        Me.ogbmainprocbutton.Tag = "2|"
        '
        'ocmexit
        '
        Me.ocmexit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmexit.Location = New System.Drawing.Point(179, 13)
        Me.ocmexit.Name = "ocmexit"
        Me.ocmexit.Size = New System.Drawing.Size(43, 25)
        Me.ocmexit.TabIndex = 96
        Me.ocmexit.TabStop = False
        Me.ocmexit.Tag = "2|"
        Me.ocmexit.Text = "EXIT"
        '
        'ocmclear
        '
        Me.ocmclear.Location = New System.Drawing.Point(71, 11)
        Me.ocmclear.Name = "ocmclear"
        Me.ocmclear.Size = New System.Drawing.Size(60, 25)
        Me.ocmclear.TabIndex = 95
        Me.ocmclear.TabStop = False
        Me.ocmclear.Tag = "2|"
        Me.ocmclear.Text = "CLEAR"
        '
        'ocmload
        '
        Me.ocmload.Location = New System.Drawing.Point(5, 13)
        Me.ocmload.Name = "ocmload"
        Me.ocmload.Size = New System.Drawing.Size(60, 23)
        Me.ocmload.TabIndex = 329
        Me.ocmload.Text = "Load Data"
        '
        'ogcDetail
        '
        Me.ogcDetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogcDetail.Location = New System.Drawing.Point(2, 21)
        Me.ogcDetail.MainView = Me.ogvDetail
        Me.ogcDetail.Name = "ogcDetail"
        Me.ogcDetail.Size = New System.Drawing.Size(1080, 439)
        Me.ogcDetail.TabIndex = 0
        Me.ogcDetail.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvDetail})
        '
        'ogvDetail
        '
        Me.ogvDetail.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.FTCmpCode, Me.FTCustCode, Me.cFTOrderNo, Me.FDShipDate, Me.FTMerTeamCode, Me.FTPurchaseNo, Me.cFDPurchaseDate, Me.PoStateName, Me.FTRawMatCode, Me.FTRawMatName, Me.FTRawMatColorCode, Me.FTRawMatColorName, Me.FTRawMatSizeCode, Me.FTFabricFrontSize, Me.FTUnitCode, Me.FTMatType, Me.FNQuantity, Me.cPurFNPrice, Me.cRecFNPrice, Me.FNPrice, Me.FNAllowcate, Me.TotalMoney})
        Me.ogvDetail.GridControl = Me.ogcDetail
        Me.ogvDetail.Name = "ogvDetail"
        Me.ogvDetail.OptionsView.AllowCellMerge = True
        Me.ogvDetail.OptionsView.ColumnAutoWidth = False
        Me.ogvDetail.OptionsView.ShowFooter = True
        '
        'FTCmpCode
        '
        Me.FTCmpCode.AppearanceHeader.Options.UseTextOptions = True
        Me.FTCmpCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTCmpCode.Caption = "FTCmpCode"
        Me.FTCmpCode.FieldName = "FTCmpCode"
        Me.FTCmpCode.Name = "FTCmpCode"
        Me.FTCmpCode.OptionsColumn.AllowEdit = False
        Me.FTCmpCode.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTCmpCode.OptionsColumn.ReadOnly = True
        Me.FTCmpCode.Visible = True
        Me.FTCmpCode.VisibleIndex = 0
        Me.FTCmpCode.Width = 73
        '
        'FTCustCode
        '
        Me.FTCustCode.AppearanceHeader.Options.UseTextOptions = True
        Me.FTCustCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTCustCode.Caption = "FTCustCode"
        Me.FTCustCode.FieldName = "FTCustCode"
        Me.FTCustCode.Name = "FTCustCode"
        Me.FTCustCode.OptionsColumn.AllowEdit = False
        Me.FTCustCode.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTCustCode.OptionsColumn.ReadOnly = True
        Me.FTCustCode.Visible = True
        Me.FTCustCode.VisibleIndex = 1
        Me.FTCustCode.Width = 73
        '
        'cFTOrderNo
        '
        Me.cFTOrderNo.AppearanceHeader.Options.UseTextOptions = True
        Me.cFTOrderNo.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.cFTOrderNo.Caption = "FTOrderNo"
        Me.cFTOrderNo.FieldName = "FTOrderNo"
        Me.cFTOrderNo.Name = "cFTOrderNo"
        Me.cFTOrderNo.OptionsColumn.AllowEdit = False
        Me.cFTOrderNo.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.cFTOrderNo.OptionsColumn.ReadOnly = True
        Me.cFTOrderNo.Visible = True
        Me.cFTOrderNo.VisibleIndex = 2
        Me.cFTOrderNo.Width = 74
        '
        'FDShipDate
        '
        Me.FDShipDate.AppearanceHeader.Options.UseTextOptions = True
        Me.FDShipDate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FDShipDate.Caption = "FDShipDate"
        Me.FDShipDate.FieldName = "FDShipDate"
        Me.FDShipDate.Name = "FDShipDate"
        Me.FDShipDate.OptionsColumn.AllowEdit = False
        Me.FDShipDate.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.FDShipDate.OptionsColumn.ReadOnly = True
        Me.FDShipDate.Visible = True
        Me.FDShipDate.VisibleIndex = 3
        Me.FDShipDate.Width = 96
        '
        'FTMerTeamCode
        '
        Me.FTMerTeamCode.AppearanceHeader.Options.UseTextOptions = True
        Me.FTMerTeamCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTMerTeamCode.Caption = "FTMerTeamCode"
        Me.FTMerTeamCode.FieldName = "FTMerTeamCode"
        Me.FTMerTeamCode.Name = "FTMerTeamCode"
        Me.FTMerTeamCode.OptionsColumn.AllowEdit = False
        Me.FTMerTeamCode.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTMerTeamCode.OptionsColumn.ReadOnly = True
        Me.FTMerTeamCode.Visible = True
        Me.FTMerTeamCode.VisibleIndex = 4
        '
        'FTPurchaseNo
        '
        Me.FTPurchaseNo.AppearanceHeader.Options.UseTextOptions = True
        Me.FTPurchaseNo.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTPurchaseNo.Caption = "FTPurchaseNo"
        Me.FTPurchaseNo.FieldName = "FTPurchaseNo"
        Me.FTPurchaseNo.Name = "FTPurchaseNo"
        Me.FTPurchaseNo.OptionsColumn.AllowEdit = False
        Me.FTPurchaseNo.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTPurchaseNo.OptionsColumn.ReadOnly = True
        Me.FTPurchaseNo.Visible = True
        Me.FTPurchaseNo.VisibleIndex = 5
        Me.FTPurchaseNo.Width = 157
        '
        'cFDPurchaseDate
        '
        Me.cFDPurchaseDate.AppearanceHeader.Options.UseTextOptions = True
        Me.cFDPurchaseDate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.cFDPurchaseDate.Caption = "cFDPurchaseDate"
        Me.cFDPurchaseDate.FieldName = "cFDPurchaseDate"
        Me.cFDPurchaseDate.Name = "cFDPurchaseDate"
        Me.cFDPurchaseDate.OptionsColumn.AllowEdit = False
        Me.cFDPurchaseDate.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.cFDPurchaseDate.OptionsColumn.ReadOnly = True
        Me.cFDPurchaseDate.Visible = True
        Me.cFDPurchaseDate.VisibleIndex = 6
        Me.cFDPurchaseDate.Width = 81
        '
        'PoStateName
        '
        Me.PoStateName.AppearanceHeader.Options.UseTextOptions = True
        Me.PoStateName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.PoStateName.Caption = "PoStateName"
        Me.PoStateName.FieldName = "PoStateName"
        Me.PoStateName.Name = "PoStateName"
        Me.PoStateName.OptionsColumn.AllowEdit = False
        Me.PoStateName.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.PoStateName.OptionsColumn.ReadOnly = True
        Me.PoStateName.Visible = True
        Me.PoStateName.VisibleIndex = 7
        Me.PoStateName.Width = 87
        '
        'FTRawMatCode
        '
        Me.FTRawMatCode.AppearanceHeader.Options.UseTextOptions = True
        Me.FTRawMatCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTRawMatCode.Caption = "FTRawMatCode"
        Me.FTRawMatCode.FieldName = "FTRawMatCode"
        Me.FTRawMatCode.Name = "FTRawMatCode"
        Me.FTRawMatCode.OptionsColumn.AllowEdit = False
        Me.FTRawMatCode.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTRawMatCode.OptionsColumn.ReadOnly = True
        Me.FTRawMatCode.Visible = True
        Me.FTRawMatCode.VisibleIndex = 8
        Me.FTRawMatCode.Width = 118
        '
        'FTRawMatName
        '
        Me.FTRawMatName.AppearanceHeader.Options.UseTextOptions = True
        Me.FTRawMatName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTRawMatName.Caption = "FTRawMatName"
        Me.FTRawMatName.FieldName = "FTRawMatName"
        Me.FTRawMatName.Name = "FTRawMatName"
        Me.FTRawMatName.OptionsColumn.AllowEdit = False
        Me.FTRawMatName.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTRawMatName.OptionsColumn.ReadOnly = True
        Me.FTRawMatName.Visible = True
        Me.FTRawMatName.VisibleIndex = 9
        Me.FTRawMatName.Width = 208
        '
        'FTRawMatColorCode
        '
        Me.FTRawMatColorCode.AppearanceHeader.Options.UseTextOptions = True
        Me.FTRawMatColorCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTRawMatColorCode.Caption = "FTRawMatColorCode"
        Me.FTRawMatColorCode.FieldName = "FTRawMatColorCode"
        Me.FTRawMatColorCode.Name = "FTRawMatColorCode"
        Me.FTRawMatColorCode.OptionsColumn.AllowEdit = False
        Me.FTRawMatColorCode.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTRawMatColorCode.OptionsColumn.ReadOnly = True
        Me.FTRawMatColorCode.Visible = True
        Me.FTRawMatColorCode.VisibleIndex = 10
        Me.FTRawMatColorCode.Width = 116
        '
        'FTRawMatColorName
        '
        Me.FTRawMatColorName.Caption = "FTRawMatColorName"
        Me.FTRawMatColorName.FieldName = "FTRawMatColorName"
        Me.FTRawMatColorName.Name = "FTRawMatColorName"
        Me.FTRawMatColorName.OptionsColumn.AllowEdit = False
        Me.FTRawMatColorName.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTRawMatColorName.OptionsColumn.ReadOnly = True
        Me.FTRawMatColorName.Visible = True
        Me.FTRawMatColorName.VisibleIndex = 11
        Me.FTRawMatColorName.Width = 77
        '
        'FTRawMatSizeCode
        '
        Me.FTRawMatSizeCode.AppearanceHeader.Options.UseTextOptions = True
        Me.FTRawMatSizeCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTRawMatSizeCode.Caption = "FTRawMatSizeCode"
        Me.FTRawMatSizeCode.FieldName = "FTRawMatSizeCode"
        Me.FTRawMatSizeCode.Name = "FTRawMatSizeCode"
        Me.FTRawMatSizeCode.OptionsColumn.AllowEdit = False
        Me.FTRawMatSizeCode.OptionsColumn.ReadOnly = True
        Me.FTRawMatSizeCode.Visible = True
        Me.FTRawMatSizeCode.VisibleIndex = 12
        '
        'FTFabricFrontSize
        '
        Me.FTFabricFrontSize.AppearanceHeader.Options.UseTextOptions = True
        Me.FTFabricFrontSize.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTFabricFrontSize.Caption = "FTFabricFrontSize"
        Me.FTFabricFrontSize.FieldName = "FTFabricFrontSize"
        Me.FTFabricFrontSize.Name = "FTFabricFrontSize"
        Me.FTFabricFrontSize.OptionsColumn.AllowEdit = False
        Me.FTFabricFrontSize.OptionsColumn.ReadOnly = True
        Me.FTFabricFrontSize.Visible = True
        Me.FTFabricFrontSize.VisibleIndex = 13
        Me.FTFabricFrontSize.Width = 96
        '
        'FTUnitCode
        '
        Me.FTUnitCode.AppearanceHeader.Options.UseTextOptions = True
        Me.FTUnitCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTUnitCode.Caption = "FTUnitCode"
        Me.FTUnitCode.FieldName = "FTUnitCode"
        Me.FTUnitCode.Name = "FTUnitCode"
        Me.FTUnitCode.OptionsColumn.AllowEdit = False
        Me.FTUnitCode.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTUnitCode.OptionsColumn.ReadOnly = True
        Me.FTUnitCode.Visible = True
        Me.FTUnitCode.VisibleIndex = 14
        Me.FTUnitCode.Width = 65
        '
        'FTMatType
        '
        Me.FTMatType.Caption = "FTMatType"
        Me.FTMatType.FieldName = "FTMatType"
        Me.FTMatType.Name = "FTMatType"
        Me.FTMatType.OptionsColumn.AllowEdit = False
        Me.FTMatType.OptionsColumn.ReadOnly = True
        Me.FTMatType.Visible = True
        Me.FTMatType.VisibleIndex = 15
        '
        'FNQuantity
        '
        Me.FNQuantity.AppearanceHeader.Options.UseTextOptions = True
        Me.FNQuantity.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FNQuantity.Caption = "FNQuantity"
        Me.FNQuantity.DisplayFormat.FormatString = "{0:n4}"
        Me.FNQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNQuantity.FieldName = "FNQuantity"
        Me.FNQuantity.Name = "FNQuantity"
        Me.FNQuantity.OptionsColumn.AllowEdit = False
        Me.FNQuantity.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNQuantity.OptionsColumn.ReadOnly = True
        Me.FNQuantity.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FNQuantity", "{0:n4}")})
        Me.FNQuantity.Visible = True
        Me.FNQuantity.VisibleIndex = 16
        Me.FNQuantity.Width = 105
        '
        'FNPrice
        '
        Me.FNPrice.Caption = "FNPrice"
        Me.FNPrice.DisplayFormat.FormatString = "{0:n4}"
        Me.FNPrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNPrice.FieldName = "FNPrice"
        Me.FNPrice.Name = "FNPrice"
        Me.FNPrice.OptionsColumn.AllowEdit = False
        Me.FNPrice.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNPrice.OptionsColumn.ReadOnly = True
        Me.FNPrice.Visible = True
        Me.FNPrice.VisibleIndex = 17
        Me.FNPrice.Width = 80
        '
        'FNAllowcate
        '
        Me.FNAllowcate.AppearanceHeader.Options.UseTextOptions = True
        Me.FNAllowcate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FNAllowcate.Caption = "FNAllowcate"
        Me.FNAllowcate.DisplayFormat.FormatString = "{0:n4}"
        Me.FNAllowcate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNAllowcate.FieldName = "FNAllowcate"
        Me.FNAllowcate.Name = "FNAllowcate"
        Me.FNAllowcate.OptionsColumn.AllowEdit = False
        Me.FNAllowcate.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNAllowcate.OptionsColumn.ReadOnly = True
        Me.FNAllowcate.Visible = True
        Me.FNAllowcate.VisibleIndex = 18
        Me.FNAllowcate.Width = 105
        '
        'TotalMoney
        '
        Me.TotalMoney.AppearanceHeader.Options.UseTextOptions = True
        Me.TotalMoney.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.TotalMoney.Caption = "TotalMoney"
        Me.TotalMoney.DisplayFormat.FormatString = "{0:n2}"
        Me.TotalMoney.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.TotalMoney.FieldName = "TotalMoney"
        Me.TotalMoney.Name = "TotalMoney"
        Me.TotalMoney.OptionsColumn.AllowEdit = False
        Me.TotalMoney.OptionsColumn.ReadOnly = True
        Me.TotalMoney.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "TotalMoney", "{0:n2}")})
        Me.TotalMoney.Visible = True
        Me.TotalMoney.VisibleIndex = 19
        Me.TotalMoney.Width = 120
        '
        'cRecFNPrice
        '
        Me.cRecFNPrice.AppearanceHeader.Options.UseTextOptions = True
        Me.cRecFNPrice.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.cRecFNPrice.Caption = "RecFNPrice"
        Me.cRecFNPrice.DisplayFormat.FormatString = "{0:n4}"
        Me.cRecFNPrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cRecFNPrice.FieldName = "RecFNPrice"
        Me.cRecFNPrice.Name = "cRecFNPrice"
        Me.cRecFNPrice.OptionsColumn.AllowEdit = False
        Me.cRecFNPrice.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.cRecFNPrice.OptionsColumn.AllowMove = False
        Me.cRecFNPrice.OptionsColumn.ReadOnly = True
        Me.cRecFNPrice.Width = 80
        '
        'cPurFNPrice
        '
        Me.cPurFNPrice.AppearanceHeader.Options.UseTextOptions = True
        Me.cPurFNPrice.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.cPurFNPrice.Caption = "PurFNPrice"
        Me.cPurFNPrice.DisplayFormat.FormatString = "{0:n4}"
        Me.cPurFNPrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cPurFNPrice.FieldName = "PurFNPrice"
        Me.cPurFNPrice.Name = "cPurFNPrice"
        Me.cPurFNPrice.OptionsColumn.AllowEdit = False
        Me.cPurFNPrice.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.cPurFNPrice.OptionsColumn.AllowMove = False
        Me.cPurFNPrice.OptionsColumn.ReadOnly = True
        Me.cPurFNPrice.Width = 80
        '
        'wMaterialMin
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1084, 561)
        Me.Controls.Add(Me.ogb)
        Me.Controls.Add(Me.oDockPanel)
        Me.Name = "wMaterialMin"
        Me.Text = "wMaterialMin"
        CType(Me.DockManager1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.oDockPanel.ResumeLayout(False)
        Me.DockPanel1_Container.ResumeLayout(False)
        CType(Me.FTOrderNoTo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTOrderNo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FDPODateEnd.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FDPODateEnd.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FDPODateStart.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FDPODateStart.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogb, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogb.ResumeLayout(False)
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbmainprocbutton.ResumeLayout(False)
        CType(Me.ogcDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvDetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents DockManager1 As DevExpress.XtraBars.Docking.DockManager
    Friend WithEvents oDockPanel As DevExpress.XtraBars.Docking.DockPanel
    Friend WithEvents DockPanel1_Container As DevExpress.XtraBars.Docking.ControlContainer
    Friend WithEvents PODateEndDate_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FDPODateEnd As DevExpress.XtraEditors.DateEdit
    Friend WithEvents PODateStart_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FDPODateStart As DevExpress.XtraEditors.DateEdit
    Friend WithEvents FTOrderNo_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTOrderNoTo_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ogb As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ogbmainprocbutton As DevExpress.XtraEditors.PanelControl
    Friend WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmclear As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmload As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ogcDetail As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvDetail As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents FTCmpCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTCustCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTOrderNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FDShipDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTMerTeamCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTPurchaseNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents PoStateName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTRawMatCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTRawMatName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTRawMatColorCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTUnitCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNAllowcate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTOrderNoTo As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FTOrderNo As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FTRawMatColorName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNPrice As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTMatType As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFDPurchaseDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTRawMatSizeCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents TotalMoney As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTFabricFrontSize As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cPurFNPrice As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cRecFNPrice As DevExpress.XtraGrid.Columns.GridColumn
End Class

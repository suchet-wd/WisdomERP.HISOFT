<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wAccountCloseJobOrder
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(wAccountCloseJobOrder))
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject4 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.DockManager1 = New DevExpress.XtraBars.Docking.DockManager(Me.components)
        Me.ogbheader = New DevExpress.XtraBars.Docking.DockPanel()
        Me.DockPanel1_Container = New DevExpress.XtraBars.Docking.ControlContainer()
        Me.FTStateSelectAll = New DevExpress.XtraEditors.CheckEdit()
        Me.FNOrderType = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.FNOrderType_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.FTCustomerPO_To = New DevExpress.XtraEditors.ButtonEdit()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.FTOrderNo_To = New DevExpress.XtraEditors.ButtonEdit()
        Me.FTEndShipDate_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTEndShipDate = New DevExpress.XtraEditors.DateEdit()
        Me.FTOrderNo_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysCustId_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTStartShipDate_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTStartShipDate = New DevExpress.XtraEditors.DateEdit()
        Me.FTOrderNo = New DevExpress.XtraEditors.ButtonEdit()
        Me.FTCustomerPO = New DevExpress.XtraEditors.ButtonEdit()
        Me.ogbdetail = New DevExpress.XtraEditors.GroupControl()
        Me.ogbmainprocbutton = New DevExpress.XtraEditors.PanelControl()
        Me.ocmreopenjob = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmclosejob = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmclear = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmload = New DevExpress.XtraEditors.SimpleButton()
        Me.ogcdetail = New DevExpress.XtraGrid.GridControl()
        Me.ogvdetail = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.FTSelect = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepFTSelect = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.CFTOrderNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFDShipDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFTStateJobClose = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFTStateClose = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFTStateCloseBy = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFTStateCloseDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFTStateCloseTime = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFTStateReopenBy = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFTStateReopenDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFTStateReopenTime = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFTStateFinish = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepFTStateFinish = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        CType(Me.DockManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbheader.SuspendLayout()
        Me.DockPanel1_Container.SuspendLayout()
        CType(Me.FTStateSelectAll.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNOrderType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTCustomerPO_To.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTOrderNo_To.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTEndShipDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTEndShipDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTStartShipDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTStartShipDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTOrderNo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTCustomerPO.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogbdetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbdetail.SuspendLayout()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbmainprocbutton.SuspendLayout()
        CType(Me.ogcdetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvdetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepFTSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepFTStateFinish, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DockManager1
        '
        Me.DockManager1.Form = Me
        Me.DockManager1.RootPanels.AddRange(New DevExpress.XtraBars.Docking.DockPanel() {Me.ogbheader})
        Me.DockManager1.TopZIndexControls.AddRange(New String() {"DevExpress.XtraBars.BarDockControl", "DevExpress.XtraBars.StandaloneBarDockControl", "System.Windows.Forms.StatusBar", "System.Windows.Forms.MenuStrip", "System.Windows.Forms.StatusStrip", "DevExpress.XtraBars.Ribbon.RibbonStatusBar", "DevExpress.XtraBars.Ribbon.RibbonControl"})
        '
        'ogbheader
        '
        Me.ogbheader.Controls.Add(Me.DockPanel1_Container)
        Me.ogbheader.Dock = DevExpress.XtraBars.Docking.DockingStyle.Top
        Me.ogbheader.DockVertical = DevExpress.Utils.DefaultBoolean.[False]
        Me.ogbheader.ID = New System.Guid("15db17cf-1ce9-44e7-94d5-3a2fa8e70d6a")
        Me.ogbheader.Image = CType(resources.GetObject("ogbheader.Image"), System.Drawing.Image)
        Me.ogbheader.Location = New System.Drawing.Point(0, 0)
        Me.ogbheader.Name = "ogbheader"
        Me.ogbheader.Options.AllowDockBottom = False
        Me.ogbheader.Options.AllowDockFill = False
        Me.ogbheader.Options.ShowCloseButton = False
        Me.ogbheader.Options.ShowMaximizeButton = False
        Me.ogbheader.OriginalSize = New System.Drawing.Size(1241, 142)
        Me.ogbheader.Size = New System.Drawing.Size(1245, 142)
        Me.ogbheader.Text = "Creteria"
        '
        'DockPanel1_Container
        '
        Me.DockPanel1_Container.Controls.Add(Me.FTStateSelectAll)
        Me.DockPanel1_Container.Controls.Add(Me.FNOrderType)
        Me.DockPanel1_Container.Controls.Add(Me.FNOrderType_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.LabelControl3)
        Me.DockPanel1_Container.Controls.Add(Me.FTCustomerPO_To)
        Me.DockPanel1_Container.Controls.Add(Me.LabelControl2)
        Me.DockPanel1_Container.Controls.Add(Me.FTOrderNo_To)
        Me.DockPanel1_Container.Controls.Add(Me.FTEndShipDate_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FTEndShipDate)
        Me.DockPanel1_Container.Controls.Add(Me.FTOrderNo_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FNHSysCustId_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FTStartShipDate_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FTStartShipDate)
        Me.DockPanel1_Container.Controls.Add(Me.FTOrderNo)
        Me.DockPanel1_Container.Controls.Add(Me.FTCustomerPO)
        Me.DockPanel1_Container.Location = New System.Drawing.Point(4, 23)
        Me.DockPanel1_Container.Name = "DockPanel1_Container"
        Me.DockPanel1_Container.Size = New System.Drawing.Size(1237, 114)
        Me.DockPanel1_Container.TabIndex = 0
        '
        'FTStateSelectAll
        '
        Me.FTStateSelectAll.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FTStateSelectAll.EditValue = "0"
        Me.FTStateSelectAll.Location = New System.Drawing.Point(10, 68)
        Me.FTStateSelectAll.Name = "FTStateSelectAll"
        Me.FTStateSelectAll.Properties.Caption = "Select All"
        Me.FTStateSelectAll.Properties.ValueChecked = "1"
        Me.FTStateSelectAll.Properties.ValueUnchecked = "0"
        Me.FTStateSelectAll.Size = New System.Drawing.Size(174, 19)
        Me.FTStateSelectAll.TabIndex = 272
        Me.FTStateSelectAll.Tag = "2|"
        '
        'FNOrderType
        '
        Me.FNOrderType.EditValue = ""
        Me.FNOrderType.EnterMoveNextControl = True
        Me.FNOrderType.Location = New System.Drawing.Point(713, 6)
        Me.FNOrderType.Name = "FNOrderType"
        Me.FNOrderType.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FNOrderType.Properties.Appearance.Options.UseBackColor = True
        Me.FNOrderType.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FNOrderType.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FNOrderType.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FNOrderType.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FNOrderType.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNOrderType.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNOrderType.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNOrderType.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNOrderType.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNOrderType.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNOrderType.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNOrderType.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNOrderType.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FNOrderType.Properties.Tag = "FNOrderType"
        Me.FNOrderType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.FNOrderType.Size = New System.Drawing.Size(145, 20)
        Me.FNOrderType.TabIndex = 436
        Me.FNOrderType.Tag = "2|"
        '
        'FNOrderType_lbl
        '
        Me.FNOrderType_lbl.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(31, Byte), Integer), CType(CType(53, Byte), Integer))
        Me.FNOrderType_lbl.Appearance.Options.UseForeColor = True
        Me.FNOrderType_lbl.Appearance.Options.UseTextOptions = True
        Me.FNOrderType_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNOrderType_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNOrderType_lbl.Location = New System.Drawing.Point(587, 7)
        Me.FNOrderType_lbl.Name = "FNOrderType_lbl"
        Me.FNOrderType_lbl.Size = New System.Drawing.Size(123, 18)
        Me.FNOrderType_lbl.TabIndex = 437
        Me.FNOrderType_lbl.Tag = "2|"
        Me.FNOrderType_lbl.Text = "FNOrderType :"
        '
        'LabelControl3
        '
        Me.LabelControl3.Appearance.Options.UseTextOptions = True
        Me.LabelControl3.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LabelControl3.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LabelControl3.Location = New System.Drawing.Point(288, 7)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(153, 17)
        Me.LabelControl3.TabIndex = 15
        Me.LabelControl3.Text = "CustomerPO. To"
        '
        'FTCustomerPO_To
        '
        Me.FTCustomerPO_To.Location = New System.Drawing.Point(447, 6)
        Me.FTCustomerPO_To.Name = "FTCustomerPO_To"
        Me.FTCustomerPO_To.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", "327", Nothing, True)})
        Me.FTCustomerPO_To.Size = New System.Drawing.Size(127, 20)
        Me.FTCustomerPO_To.TabIndex = 14
        '
        'LabelControl2
        '
        Me.LabelControl2.Appearance.Options.UseTextOptions = True
        Me.LabelControl2.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LabelControl2.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LabelControl2.Location = New System.Drawing.Point(288, 48)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(153, 17)
        Me.LabelControl2.TabIndex = 13
        Me.LabelControl2.Text = "Order to."
        '
        'FTOrderNo_To
        '
        Me.FTOrderNo_To.Location = New System.Drawing.Point(447, 46)
        Me.FTOrderNo_To.Name = "FTOrderNo_To"
        Me.FTOrderNo_To.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject2, "", "325", Nothing, True)})
        Me.FTOrderNo_To.Size = New System.Drawing.Size(127, 20)
        Me.FTOrderNo_To.TabIndex = 11
        '
        'FTEndShipDate_lbl
        '
        Me.FTEndShipDate_lbl.Appearance.Options.UseTextOptions = True
        Me.FTEndShipDate_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTEndShipDate_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTEndShipDate_lbl.Location = New System.Drawing.Point(288, 27)
        Me.FTEndShipDate_lbl.Name = "FTEndShipDate_lbl"
        Me.FTEndShipDate_lbl.Size = New System.Drawing.Size(153, 17)
        Me.FTEndShipDate_lbl.TabIndex = 9
        Me.FTEndShipDate_lbl.Text = "Ship.Date to"
        '
        'FTEndShipDate
        '
        Me.FTEndShipDate.EditValue = Nothing
        Me.FTEndShipDate.Location = New System.Drawing.Point(447, 26)
        Me.FTEndShipDate.Name = "FTEndShipDate"
        Me.FTEndShipDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FTEndShipDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FTEndShipDate.Size = New System.Drawing.Size(127, 20)
        Me.FTEndShipDate.TabIndex = 8
        '
        'FTOrderNo_lbl
        '
        Me.FTOrderNo_lbl.Appearance.Options.UseTextOptions = True
        Me.FTOrderNo_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTOrderNo_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTOrderNo_lbl.Location = New System.Drawing.Point(4, 48)
        Me.FTOrderNo_lbl.Name = "FTOrderNo_lbl"
        Me.FTOrderNo_lbl.Size = New System.Drawing.Size(144, 17)
        Me.FTOrderNo_lbl.TabIndex = 6
        Me.FTOrderNo_lbl.Text = "Order."
        '
        'FNHSysCustId_lbl
        '
        Me.FNHSysCustId_lbl.Appearance.Options.UseTextOptions = True
        Me.FNHSysCustId_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysCustId_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysCustId_lbl.Location = New System.Drawing.Point(3, 6)
        Me.FNHSysCustId_lbl.Name = "FNHSysCustId_lbl"
        Me.FNHSysCustId_lbl.Size = New System.Drawing.Size(145, 17)
        Me.FNHSysCustId_lbl.TabIndex = 6
        Me.FNHSysCustId_lbl.Text = "CustomerPO."
        '
        'FTStartShipDate_lbl
        '
        Me.FTStartShipDate_lbl.Appearance.Options.UseTextOptions = True
        Me.FTStartShipDate_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTStartShipDate_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTStartShipDate_lbl.Location = New System.Drawing.Point(3, 27)
        Me.FTStartShipDate_lbl.Name = "FTStartShipDate_lbl"
        Me.FTStartShipDate_lbl.Size = New System.Drawing.Size(145, 17)
        Me.FTStartShipDate_lbl.TabIndex = 5
        Me.FTStartShipDate_lbl.Text = "Ship.Date from"
        '
        'FTStartShipDate
        '
        Me.FTStartShipDate.EditValue = Nothing
        Me.FTStartShipDate.Location = New System.Drawing.Point(152, 26)
        Me.FTStartShipDate.Name = "FTStartShipDate"
        Me.FTStartShipDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FTStartShipDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FTStartShipDate.Size = New System.Drawing.Size(127, 20)
        Me.FTStartShipDate.TabIndex = 4
        '
        'FTOrderNo
        '
        Me.FTOrderNo.Location = New System.Drawing.Point(152, 46)
        Me.FTOrderNo.Name = "FTOrderNo"
        Me.FTOrderNo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject3, "", "324", Nothing, True)})
        Me.FTOrderNo.Size = New System.Drawing.Size(127, 20)
        Me.FTOrderNo.TabIndex = 3
        '
        'FTCustomerPO
        '
        Me.FTCustomerPO.Location = New System.Drawing.Point(152, 6)
        Me.FTCustomerPO.Name = "FTCustomerPO"
        Me.FTCustomerPO.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject4, "", "326", Nothing, True)})
        Me.FTCustomerPO.Size = New System.Drawing.Size(127, 20)
        Me.FTCustomerPO.TabIndex = 2
        '
        'ogbdetail
        '
        Me.ogbdetail.Controls.Add(Me.ogbmainprocbutton)
        Me.ogbdetail.Controls.Add(Me.ogcdetail)
        Me.ogbdetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogbdetail.Location = New System.Drawing.Point(0, 142)
        Me.ogbdetail.Name = "ogbdetail"
        Me.ogbdetail.Size = New System.Drawing.Size(1245, 467)
        Me.ogbdetail.TabIndex = 1
        Me.ogbdetail.Text = "List Factory No"
        '
        'ogbmainprocbutton
        '
        Me.ogbmainprocbutton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmreopenjob)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmclosejob)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmexit)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmclear)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmload)
        Me.ogbmainprocbutton.Location = New System.Drawing.Point(233, 221)
        Me.ogbmainprocbutton.Name = "ogbmainprocbutton"
        Me.ogbmainprocbutton.Size = New System.Drawing.Size(960, 69)
        Me.ogbmainprocbutton.TabIndex = 303
        Me.ogbmainprocbutton.Tag = "2|"
        '
        'ocmreopenjob
        '
        Me.ocmreopenjob.Location = New System.Drawing.Point(347, 10)
        Me.ocmreopenjob.Name = "ocmreopenjob"
        Me.ocmreopenjob.Size = New System.Drawing.Size(117, 25)
        Me.ocmreopenjob.TabIndex = 333
        Me.ocmreopenjob.TabStop = False
        Me.ocmreopenjob.Tag = "2|"
        Me.ocmreopenjob.Text = "Reopen Job"
        '
        'ocmclosejob
        '
        Me.ocmclosejob.Location = New System.Drawing.Point(210, 11)
        Me.ocmclosejob.Name = "ocmclosejob"
        Me.ocmclosejob.Size = New System.Drawing.Size(117, 25)
        Me.ocmclosejob.TabIndex = 332
        Me.ocmclosejob.TabStop = False
        Me.ocmclosejob.Tag = "2|"
        Me.ocmclosejob.Text = "Close Job"
        '
        'ocmexit
        '
        Me.ocmexit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmexit.Location = New System.Drawing.Point(847, 11)
        Me.ocmexit.Name = "ocmexit"
        Me.ocmexit.Size = New System.Drawing.Size(95, 25)
        Me.ocmexit.TabIndex = 96
        Me.ocmexit.TabStop = False
        Me.ocmexit.Tag = "2|"
        Me.ocmexit.Text = "EXIT"
        '
        'ocmclear
        '
        Me.ocmclear.Location = New System.Drawing.Point(110, 11)
        Me.ocmclear.Name = "ocmclear"
        Me.ocmclear.Size = New System.Drawing.Size(95, 25)
        Me.ocmclear.TabIndex = 95
        Me.ocmclear.TabStop = False
        Me.ocmclear.Tag = "2|"
        Me.ocmclear.Text = "CLEAR"
        '
        'ocmload
        '
        Me.ocmload.Location = New System.Drawing.Point(9, 11)
        Me.ocmload.Name = "ocmload"
        Me.ocmload.Size = New System.Drawing.Size(95, 25)
        Me.ocmload.TabIndex = 93
        Me.ocmload.TabStop = False
        Me.ocmload.Tag = "2|"
        Me.ocmload.Text = "Load"
        '
        'ogcdetail
        '
        Me.ogcdetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogcdetail.Location = New System.Drawing.Point(2, 20)
        Me.ogcdetail.MainView = Me.ogvdetail
        Me.ogcdetail.Name = "ogcdetail"
        Me.ogcdetail.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepFTSelect, Me.RepFTStateFinish})
        Me.ogcdetail.Size = New System.Drawing.Size(1241, 445)
        Me.ogcdetail.TabIndex = 0
        Me.ogcdetail.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvdetail})
        '
        'ogvdetail
        '
        Me.ogvdetail.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.FTSelect, Me.CFTOrderNo, Me.CFDShipDate, Me.CFTStateJobClose, Me.CFTStateClose, Me.CFTStateCloseBy, Me.CFTStateCloseDate, Me.CFTStateCloseTime, Me.CFTStateReopenBy, Me.CFTStateReopenDate, Me.CFTStateReopenTime, Me.CFTStateFinish})
        Me.ogvdetail.GridControl = Me.ogcdetail
        Me.ogvdetail.Name = "ogvdetail"
        Me.ogvdetail.OptionsView.ColumnAutoWidth = False
        Me.ogvdetail.OptionsView.ShowGroupPanel = False
        '
        'FTSelect
        '
        Me.FTSelect.Caption = "FTSelect"
        Me.FTSelect.ColumnEdit = Me.RepFTSelect
        Me.FTSelect.FieldName = "FTSelect"
        Me.FTSelect.Name = "FTSelect"
        Me.FTSelect.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTSelect.OptionsColumn.ShowCaption = False
        Me.FTSelect.Visible = True
        Me.FTSelect.VisibleIndex = 0
        Me.FTSelect.Width = 57
        '
        'RepFTSelect
        '
        Me.RepFTSelect.AutoHeight = False
        Me.RepFTSelect.Caption = "Check"
        Me.RepFTSelect.Name = "RepFTSelect"
        Me.RepFTSelect.ValueChecked = "1"
        Me.RepFTSelect.ValueUnchecked = "0"
        '
        'CFTOrderNo
        '
        Me.CFTOrderNo.Caption = "Order No"
        Me.CFTOrderNo.FieldName = "FTOrderNo"
        Me.CFTOrderNo.Name = "CFTOrderNo"
        Me.CFTOrderNo.OptionsColumn.AllowEdit = False
        Me.CFTOrderNo.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFTOrderNo.OptionsColumn.AllowMove = False
        Me.CFTOrderNo.OptionsColumn.ReadOnly = True
        Me.CFTOrderNo.Visible = True
        Me.CFTOrderNo.VisibleIndex = 1
        Me.CFTOrderNo.Width = 151
        '
        'CFDShipDate
        '
        Me.CFDShipDate.Caption = "Ship Date"
        Me.CFDShipDate.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.CFDShipDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.CFDShipDate.FieldName = "FDShipDate"
        Me.CFDShipDate.Name = "CFDShipDate"
        Me.CFDShipDate.OptionsColumn.AllowEdit = False
        Me.CFDShipDate.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFDShipDate.OptionsColumn.AllowMove = False
        Me.CFDShipDate.OptionsColumn.ReadOnly = True
        Me.CFDShipDate.Visible = True
        Me.CFDShipDate.VisibleIndex = 2
        Me.CFDShipDate.Width = 114
        '
        'CFTStateJobClose
        '
        Me.CFTStateJobClose.Caption = "Closed"
        Me.CFTStateJobClose.ColumnEdit = Me.RepFTSelect
        Me.CFTStateJobClose.FieldName = "FTStateJobClose"
        Me.CFTStateJobClose.Name = "CFTStateJobClose"
        Me.CFTStateJobClose.OptionsColumn.AllowEdit = False
        Me.CFTStateJobClose.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFTStateJobClose.OptionsColumn.AllowMove = False
        Me.CFTStateJobClose.OptionsColumn.ReadOnly = True
        Me.CFTStateJobClose.Visible = True
        Me.CFTStateJobClose.VisibleIndex = 3
        '
        'CFTStateClose
        '
        Me.CFTStateClose.Caption = "FTStateClose"
        Me.CFTStateClose.FieldName = "FTStateClose"
        Me.CFTStateClose.Name = "CFTStateClose"
        Me.CFTStateClose.OptionsColumn.AllowEdit = False
        Me.CFTStateClose.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFTStateClose.OptionsColumn.AllowMove = False
        Me.CFTStateClose.OptionsColumn.ReadOnly = True
        '
        'CFTStateCloseBy
        '
        Me.CFTStateCloseBy.Caption = "Closed By"
        Me.CFTStateCloseBy.FieldName = "FTStateCloseBy"
        Me.CFTStateCloseBy.Name = "CFTStateCloseBy"
        Me.CFTStateCloseBy.OptionsColumn.AllowEdit = False
        Me.CFTStateCloseBy.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFTStateCloseBy.OptionsColumn.AllowMove = False
        Me.CFTStateCloseBy.OptionsColumn.ReadOnly = True
        Me.CFTStateCloseBy.Visible = True
        Me.CFTStateCloseBy.VisibleIndex = 4
        Me.CFTStateCloseBy.Width = 112
        '
        'CFTStateCloseDate
        '
        Me.CFTStateCloseDate.Caption = "Closed Date"
        Me.CFTStateCloseDate.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.CFTStateCloseDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.CFTStateCloseDate.FieldName = "FTStateCloseDate"
        Me.CFTStateCloseDate.Name = "CFTStateCloseDate"
        Me.CFTStateCloseDate.OptionsColumn.AllowEdit = False
        Me.CFTStateCloseDate.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFTStateCloseDate.OptionsColumn.AllowMove = False
        Me.CFTStateCloseDate.OptionsColumn.ReadOnly = True
        Me.CFTStateCloseDate.Visible = True
        Me.CFTStateCloseDate.VisibleIndex = 5
        Me.CFTStateCloseDate.Width = 87
        '
        'CFTStateCloseTime
        '
        Me.CFTStateCloseTime.Caption = "Closed Time"
        Me.CFTStateCloseTime.FieldName = "FTStateCloseTime"
        Me.CFTStateCloseTime.Name = "CFTStateCloseTime"
        Me.CFTStateCloseTime.OptionsColumn.AllowEdit = False
        Me.CFTStateCloseTime.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFTStateCloseTime.OptionsColumn.AllowMove = False
        Me.CFTStateCloseTime.OptionsColumn.ReadOnly = True
        Me.CFTStateCloseTime.Visible = True
        Me.CFTStateCloseTime.VisibleIndex = 6
        '
        'CFTStateReopenBy
        '
        Me.CFTStateReopenBy.Caption = "Re Opened By"
        Me.CFTStateReopenBy.FieldName = "FTStateReopenBy"
        Me.CFTStateReopenBy.Name = "CFTStateReopenBy"
        Me.CFTStateReopenBy.OptionsColumn.AllowEdit = False
        Me.CFTStateReopenBy.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFTStateReopenBy.OptionsColumn.AllowMove = False
        Me.CFTStateReopenBy.OptionsColumn.ReadOnly = True
        Me.CFTStateReopenBy.Visible = True
        Me.CFTStateReopenBy.VisibleIndex = 7
        Me.CFTStateReopenBy.Width = 103
        '
        'CFTStateReopenDate
        '
        Me.CFTStateReopenDate.Caption = "FTStateReopenDate"
        Me.CFTStateReopenDate.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.CFTStateReopenDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.CFTStateReopenDate.FieldName = "FTStateReopenDate"
        Me.CFTStateReopenDate.Name = "CFTStateReopenDate"
        Me.CFTStateReopenDate.OptionsColumn.AllowEdit = False
        Me.CFTStateReopenDate.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFTStateReopenDate.OptionsColumn.AllowMove = False
        Me.CFTStateReopenDate.OptionsColumn.ReadOnly = True
        Me.CFTStateReopenDate.Visible = True
        Me.CFTStateReopenDate.VisibleIndex = 8
        Me.CFTStateReopenDate.Width = 98
        '
        'CFTStateReopenTime
        '
        Me.CFTStateReopenTime.Caption = "FTStateReopenTime"
        Me.CFTStateReopenTime.FieldName = "FTStateReopenTime"
        Me.CFTStateReopenTime.Name = "CFTStateReopenTime"
        Me.CFTStateReopenTime.OptionsColumn.AllowEdit = False
        Me.CFTStateReopenTime.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFTStateReopenTime.OptionsColumn.AllowMove = False
        Me.CFTStateReopenTime.OptionsColumn.ReadOnly = True
        Me.CFTStateReopenTime.Visible = True
        Me.CFTStateReopenTime.VisibleIndex = 9
        '
        'CFTStateFinish
        '
        Me.CFTStateFinish.Caption = "State Cost Finish "
        Me.CFTStateFinish.ColumnEdit = Me.RepFTStateFinish
        Me.CFTStateFinish.FieldName = "FTStateFinish"
        Me.CFTStateFinish.Name = "CFTStateFinish"
        Me.CFTStateFinish.OptionsColumn.AllowEdit = False
        Me.CFTStateFinish.OptionsColumn.ReadOnly = True
        Me.CFTStateFinish.Visible = True
        Me.CFTStateFinish.VisibleIndex = 10
        Me.CFTStateFinish.Width = 100
        '
        'RepFTStateFinish
        '
        Me.RepFTStateFinish.AutoHeight = False
        Me.RepFTStateFinish.Caption = "Check"
        Me.RepFTStateFinish.Name = "RepFTStateFinish"
        Me.RepFTStateFinish.ValueChecked = "1"
        Me.RepFTStateFinish.ValueUnchecked = "0"
        '
        'wAccountCloseJobOrder
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1245, 609)
        Me.Controls.Add(Me.ogbdetail)
        Me.Controls.Add(Me.ogbheader)
        Me.Name = "wAccountCloseJobOrder"
        Me.Text = "Close Job"
        CType(Me.DockManager1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbheader.ResumeLayout(False)
        Me.DockPanel1_Container.ResumeLayout(False)
        CType(Me.FTStateSelectAll.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNOrderType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTCustomerPO_To.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTOrderNo_To.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTEndShipDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTEndShipDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTStartShipDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTStartShipDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTOrderNo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTCustomerPO.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogbdetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbdetail.ResumeLayout(False)
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbmainprocbutton.ResumeLayout(False)
        CType(Me.ogcdetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvdetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepFTSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepFTStateFinish, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents DockManager1 As DevExpress.XtraBars.Docking.DockManager
    Friend WithEvents ogbheader As DevExpress.XtraBars.Docking.DockPanel
    Friend WithEvents DockPanel1_Container As DevExpress.XtraBars.Docking.ControlContainer
    Friend WithEvents FTStartShipDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents FTOrderNo As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FTCustomerPO As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FTStartShipDate_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTEndShipDate_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTEndShipDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents FTOrderNo_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysCustId_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ogbdetail As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ogcdetail As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvdetail As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents ogbmainprocbutton As DevExpress.XtraEditors.PanelControl
    Friend WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmclear As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmload As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTOrderNo_To As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTCustomerPO_To As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents ocmreopenjob As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmclosejob As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FNOrderType As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents FNOrderType_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTSelect As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepFTSelect As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents CFTOrderNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFDShipDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFTStateJobClose As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFTStateClose As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFTStateCloseBy As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFTStateCloseDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFTStateCloseTime As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFTStateReopenBy As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFTStateReopenDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFTStateReopenTime As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFTStateFinish As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepFTStateFinish As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents FTStateSelectAll As DevExpress.XtraEditors.CheckEdit
End Class

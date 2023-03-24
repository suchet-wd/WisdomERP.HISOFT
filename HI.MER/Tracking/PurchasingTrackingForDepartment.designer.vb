<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PurchasingTrackingForDepartment
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PurchasingTrackingForDepartment))
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.DockManager1 = New DevExpress.XtraBars.Docking.DockManager(Me.components)
        Me.ogbheader = New DevExpress.XtraBars.Docking.DockPanel()
        Me.DockPanel1_Container = New DevExpress.XtraBars.Docking.ControlContainer()
        Me.LabelControl6 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl5 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.FTCountryName = New DevExpress.XtraEditors.TextEdit()
        Me.ProvinceName = New DevExpress.XtraEditors.TextEdit()
        Me.FTContiName = New DevExpress.XtraEditors.TextEdit()
        Me.FTStyleCode = New DevExpress.XtraEditors.TextEdit()
        Me.FTCustomerPO = New DevExpress.XtraEditors.ButtonEdit()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.FTSubOrderNo = New DevExpress.XtraEditors.ButtonEdit()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.ogbdetail = New DevExpress.XtraEditors.GroupControl()
        Me.ogbmainprocbutton = New DevExpress.XtraEditors.PanelControl()
        Me.ocmpreview = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmsavelayout = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmclear = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmload = New DevExpress.XtraEditors.SimpleButton()
        Me.ochkselectall = New DevExpress.XtraEditors.CheckEdit()
        Me.ogcTrackingDept = New DevExpress.XtraGrid.GridControl()
        Me.ogvTrackingDept = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.FTSelect = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositorySelect = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.FTPurchaseNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTInvoiceNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTSuplName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTRawMatCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTRawMatColorCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTRawMatSizeCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTUnitCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNPrice = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.TotalPrice = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCheckEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.RepositoryItemCheckEdit2 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        CType(Me.DockManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbheader.SuspendLayout()
        Me.DockPanel1_Container.SuspendLayout()
        CType(Me.FTCountryName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ProvinceName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTContiName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTStyleCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTCustomerPO.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTSubOrderNo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogbdetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbdetail.SuspendLayout()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbmainprocbutton.SuspendLayout()
        CType(Me.ochkselectall.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogcTrackingDept, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvTrackingDept, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositorySelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.ogbheader.ID = New System.Guid("e7519a99-f1f3-467a-9825-b8ac8ec515c2")
        Me.ogbheader.Image = CType(resources.GetObject("ogbheader.Image"), System.Drawing.Image)
        Me.ogbheader.Location = New System.Drawing.Point(0, 0)
        Me.ogbheader.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbheader.Name = "ogbheader"
        Me.ogbheader.Options.AllowDockBottom = False
        Me.ogbheader.Options.AllowDockFill = False
        Me.ogbheader.Options.ShowCloseButton = False
        Me.ogbheader.OriginalSize = New System.Drawing.Size(200, 101)
        Me.ogbheader.Size = New System.Drawing.Size(1227, 101)
        Me.ogbheader.Text = "Creteria"
        '
        'DockPanel1_Container
        '
        Me.DockPanel1_Container.Controls.Add(Me.LabelControl6)
        Me.DockPanel1_Container.Controls.Add(Me.LabelControl5)
        Me.DockPanel1_Container.Controls.Add(Me.LabelControl4)
        Me.DockPanel1_Container.Controls.Add(Me.LabelControl3)
        Me.DockPanel1_Container.Controls.Add(Me.FTCountryName)
        Me.DockPanel1_Container.Controls.Add(Me.ProvinceName)
        Me.DockPanel1_Container.Controls.Add(Me.FTContiName)
        Me.DockPanel1_Container.Controls.Add(Me.FTStyleCode)
        Me.DockPanel1_Container.Controls.Add(Me.FTCustomerPO)
        Me.DockPanel1_Container.Controls.Add(Me.LabelControl2)
        Me.DockPanel1_Container.Controls.Add(Me.FTSubOrderNo)
        Me.DockPanel1_Container.Controls.Add(Me.LabelControl1)
        Me.DockPanel1_Container.Location = New System.Drawing.Point(4, 24)
        Me.DockPanel1_Container.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.DockPanel1_Container.Name = "DockPanel1_Container"
        Me.DockPanel1_Container.Size = New System.Drawing.Size(1219, 73)
        Me.DockPanel1_Container.TabIndex = 0
        '
        'LabelControl6
        '
        Me.LabelControl6.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LabelControl6.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LabelControl6.Location = New System.Drawing.Point(549, 50)
        Me.LabelControl6.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LabelControl6.Name = "LabelControl6"
        Me.LabelControl6.Size = New System.Drawing.Size(114, 21)
        Me.LabelControl6.TabIndex = 10
        Me.LabelControl6.Text = "City."
        '
        'LabelControl5
        '
        Me.LabelControl5.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LabelControl5.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LabelControl5.Location = New System.Drawing.Point(272, 50)
        Me.LabelControl5.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LabelControl5.Name = "LabelControl5"
        Me.LabelControl5.Size = New System.Drawing.Size(126, 21)
        Me.LabelControl5.TabIndex = 9
        Me.LabelControl5.Text = "Country Name."
        '
        'LabelControl4
        '
        Me.LabelControl4.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LabelControl4.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LabelControl4.Location = New System.Drawing.Point(0, 50)
        Me.LabelControl4.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Size = New System.Drawing.Size(120, 21)
        Me.LabelControl4.TabIndex = 8
        Me.LabelControl4.Text = "Zone Name."
        '
        'LabelControl3
        '
        Me.LabelControl3.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LabelControl3.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LabelControl3.Location = New System.Drawing.Point(549, 22)
        Me.LabelControl3.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(112, 25)
        Me.LabelControl3.TabIndex = 7
        Me.LabelControl3.Text = "Style"
        '
        'FTCountryName
        '
        Me.FTCountryName.EditValue = ""
        Me.FTCountryName.Location = New System.Drawing.Point(404, 49)
        Me.FTCountryName.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTCountryName.Name = "FTCountryName"
        Me.FTCountryName.Properties.Appearance.BackColor = System.Drawing.Color.LightCyan
        Me.FTCountryName.Properties.Appearance.Options.UseBackColor = True
        Me.FTCountryName.Properties.ReadOnly = True
        Me.FTCountryName.Size = New System.Drawing.Size(139, 22)
        Me.FTCountryName.TabIndex = 6
        '
        'ProvinceName
        '
        Me.ProvinceName.Location = New System.Drawing.Point(668, 49)
        Me.ProvinceName.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ProvinceName.Name = "ProvinceName"
        Me.ProvinceName.Properties.Appearance.BackColor = System.Drawing.Color.LightCyan
        Me.ProvinceName.Properties.Appearance.Options.UseBackColor = True
        Me.ProvinceName.Properties.ReadOnly = True
        Me.ProvinceName.Size = New System.Drawing.Size(140, 22)
        Me.ProvinceName.TabIndex = 5
        '
        'FTContiName
        '
        Me.FTContiName.Location = New System.Drawing.Point(125, 49)
        Me.FTContiName.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTContiName.Name = "FTContiName"
        Me.FTContiName.Properties.Appearance.BackColor = System.Drawing.Color.LightCyan
        Me.FTContiName.Properties.Appearance.Options.UseBackColor = True
        Me.FTContiName.Properties.ReadOnly = True
        Me.FTContiName.Size = New System.Drawing.Size(140, 22)
        Me.FTContiName.TabIndex = 4
        '
        'FTStyleCode
        '
        Me.FTStyleCode.Location = New System.Drawing.Point(668, 22)
        Me.FTStyleCode.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTStyleCode.Name = "FTStyleCode"
        Me.FTStyleCode.Properties.Appearance.BackColor = System.Drawing.Color.LightCyan
        Me.FTStyleCode.Properties.Appearance.Options.UseBackColor = True
        Me.FTStyleCode.Properties.ReadOnly = True
        Me.FTStyleCode.Size = New System.Drawing.Size(140, 22)
        Me.FTStyleCode.TabIndex = 1
        '
        'FTCustomerPO
        '
        Me.FTCustomerPO.Location = New System.Drawing.Point(125, 22)
        Me.FTCustomerPO.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTCustomerPO.Name = "FTCustomerPO"
        Me.FTCustomerPO.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", "329", Nothing, True)})
        Me.FTCustomerPO.Size = New System.Drawing.Size(140, 22)
        Me.FTCustomerPO.TabIndex = 3
        '
        'LabelControl2
        '
        Me.LabelControl2.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LabelControl2.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LabelControl2.Location = New System.Drawing.Point(272, 26)
        Me.LabelControl2.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(126, 21)
        Me.LabelControl2.TabIndex = 2
        Me.LabelControl2.Text = "SubOrderNo."
        '
        'FTSubOrderNo
        '
        Me.FTSubOrderNo.Location = New System.Drawing.Point(402, 22)
        Me.FTSubOrderNo.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTSubOrderNo.Name = "FTSubOrderNo"
        Me.FTSubOrderNo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject2, "", "330", Nothing, True)})
        Me.FTSubOrderNo.Size = New System.Drawing.Size(140, 22)
        Me.FTSubOrderNo.TabIndex = 1
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.LabelControl1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LabelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LabelControl1.Location = New System.Drawing.Point(0, 23)
        Me.LabelControl1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(119, 21)
        Me.LabelControl1.TabIndex = 0
        Me.LabelControl1.Text = "Customer PO."
        '
        'ogbdetail
        '
        Me.ogbdetail.AppearanceCaption.Options.UseTextOptions = True
        Me.ogbdetail.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ogbdetail.Controls.Add(Me.ogbmainprocbutton)
        Me.ogbdetail.Controls.Add(Me.ochkselectall)
        Me.ogbdetail.Controls.Add(Me.ogcTrackingDept)
        Me.ogbdetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogbdetail.Location = New System.Drawing.Point(0, 101)
        Me.ogbdetail.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbdetail.Name = "ogbdetail"
        Me.ogbdetail.Size = New System.Drawing.Size(1227, 639)
        Me.ogbdetail.TabIndex = 2
        Me.ogbdetail.Text = "Tracking for department detail"
        '
        'ogbmainprocbutton
        '
        Me.ogbmainprocbutton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmpreview)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmsavelayout)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmexit)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmclear)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmload)
        Me.ogbmainprocbutton.Location = New System.Drawing.Point(69, 254)
        Me.ogbmainprocbutton.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbmainprocbutton.Name = "ogbmainprocbutton"
        Me.ogbmainprocbutton.Size = New System.Drawing.Size(1091, 58)
        Me.ogbmainprocbutton.TabIndex = 387
        Me.ogbmainprocbutton.Tag = "2|"
        '
        'ocmpreview
        '
        Me.ocmpreview.Location = New System.Drawing.Point(278, 15)
        Me.ocmpreview.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmpreview.Name = "ocmpreview"
        Me.ocmpreview.Size = New System.Drawing.Size(141, 31)
        Me.ocmpreview.TabIndex = 333
        Me.ocmpreview.TabStop = False
        Me.ocmpreview.Tag = "2|"
        Me.ocmpreview.Text = "PREVIEW"
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
        Me.ocmexit.Location = New System.Drawing.Point(959, 14)
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
        '
        'ocmload
        '
        Me.ocmload.Location = New System.Drawing.Point(134, 15)
        Me.ocmload.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmload.Name = "ocmload"
        Me.ocmload.Size = New System.Drawing.Size(136, 28)
        Me.ocmload.TabIndex = 329
        Me.ocmload.Text = "Load Data"
        '
        'ochkselectall
        '
        Me.ochkselectall.Location = New System.Drawing.Point(34, 2)
        Me.ochkselectall.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ochkselectall.Name = "ochkselectall"
        Me.ochkselectall.Properties.Caption = "Select All"
        Me.ochkselectall.Size = New System.Drawing.Size(121, 20)
        Me.ochkselectall.TabIndex = 1
        '
        'ogcTrackingDept
        '
        Me.ogcTrackingDept.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogcTrackingDept.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcTrackingDept.Location = New System.Drawing.Point(2, 24)
        Me.ogcTrackingDept.MainView = Me.ogvTrackingDept
        Me.ogcTrackingDept.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcTrackingDept.Name = "ogcTrackingDept"
        Me.ogcTrackingDept.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositorySelect, Me.RepositoryItemCheckEdit1, Me.RepositoryItemCheckEdit2})
        Me.ogcTrackingDept.Size = New System.Drawing.Size(1223, 613)
        Me.ogcTrackingDept.TabIndex = 0
        Me.ogcTrackingDept.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvTrackingDept})
        '
        'ogvTrackingDept
        '
        Me.ogvTrackingDept.ActiveFilterEnabled = False
        Me.ogvTrackingDept.Appearance.Row.BackColor = System.Drawing.Color.White
        Me.ogvTrackingDept.Appearance.Row.Options.UseBackColor = True
        Me.ogvTrackingDept.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.FTSelect, Me.FTPurchaseNo, Me.FTInvoiceNo, Me.FTSuplName, Me.FTRawMatCode, Me.FTRawMatColorCode, Me.FTRawMatSizeCode, Me.FNQuantity, Me.FTUnitCode, Me.FNPrice, Me.TotalPrice})
        Me.ogvTrackingDept.GridControl = Me.ogcTrackingDept
        Me.ogvTrackingDept.Name = "ogvTrackingDept"
        Me.ogvTrackingDept.OptionsView.AllowCellMerge = True
        Me.ogvTrackingDept.OptionsView.ColumnAutoWidth = False
        Me.ogvTrackingDept.OptionsView.ShowGroupPanel = False
        '
        'FTSelect
        '
        Me.FTSelect.AppearanceHeader.Options.UseTextOptions = True
        Me.FTSelect.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTSelect.Caption = "FTSelect"
        Me.FTSelect.ColumnEdit = Me.RepositorySelect
        Me.FTSelect.FieldName = "FTSelect"
        Me.FTSelect.Name = "FTSelect"
        Me.FTSelect.OptionsColumn.AllowMove = False
        Me.FTSelect.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTSelect.Width = 40
        '
        'RepositorySelect
        '
        Me.RepositorySelect.AutoHeight = False
        Me.RepositorySelect.Caption = "Check"
        Me.RepositorySelect.Name = "RepositorySelect"
        Me.RepositorySelect.ValueChecked = "1"
        Me.RepositorySelect.ValueUnchecked = "0"
        '
        'FTPurchaseNo
        '
        Me.FTPurchaseNo.AppearanceHeader.Options.UseTextOptions = True
        Me.FTPurchaseNo.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTPurchaseNo.Caption = "FTPurchaseNo"
        Me.FTPurchaseNo.FieldName = "FTPurchaseNo"
        Me.FTPurchaseNo.Name = "FTPurchaseNo"
        Me.FTPurchaseNo.OptionsColumn.AllowEdit = False
        Me.FTPurchaseNo.OptionsColumn.ReadOnly = True
        Me.FTPurchaseNo.Visible = True
        Me.FTPurchaseNo.VisibleIndex = 0
        Me.FTPurchaseNo.Width = 119
        '
        'FTInvoiceNo
        '
        Me.FTInvoiceNo.AppearanceHeader.Options.UseTextOptions = True
        Me.FTInvoiceNo.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTInvoiceNo.Caption = "FTInvoiceNo"
        Me.FTInvoiceNo.FieldName = "FTInvoiceNo"
        Me.FTInvoiceNo.Name = "FTInvoiceNo"
        Me.FTInvoiceNo.OptionsColumn.AllowEdit = False
        Me.FTInvoiceNo.OptionsColumn.ReadOnly = True
        Me.FTInvoiceNo.Visible = True
        Me.FTInvoiceNo.VisibleIndex = 1
        Me.FTInvoiceNo.Width = 90
        '
        'FTSuplName
        '
        Me.FTSuplName.AppearanceHeader.Options.UseTextOptions = True
        Me.FTSuplName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTSuplName.Caption = "FTSuplName"
        Me.FTSuplName.FieldName = "FTSuplName"
        Me.FTSuplName.Name = "FTSuplName"
        Me.FTSuplName.OptionsColumn.AllowEdit = False
        Me.FTSuplName.OptionsColumn.ReadOnly = True
        Me.FTSuplName.Visible = True
        Me.FTSuplName.VisibleIndex = 3
        Me.FTSuplName.Width = 204
        '
        'FTRawMatCode
        '
        Me.FTRawMatCode.AppearanceHeader.Options.UseTextOptions = True
        Me.FTRawMatCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTRawMatCode.Caption = "FTRawMatCode"
        Me.FTRawMatCode.FieldName = "FTRawMatCode"
        Me.FTRawMatCode.Name = "FTRawMatCode"
        Me.FTRawMatCode.OptionsColumn.AllowEdit = False
        Me.FTRawMatCode.OptionsColumn.ReadOnly = True
        Me.FTRawMatCode.Visible = True
        Me.FTRawMatCode.VisibleIndex = 2
        Me.FTRawMatCode.Width = 108
        '
        'FTRawMatColorCode
        '
        Me.FTRawMatColorCode.AppearanceHeader.Options.UseTextOptions = True
        Me.FTRawMatColorCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTRawMatColorCode.Caption = "FTRawMatColorCode"
        Me.FTRawMatColorCode.FieldName = "FTRawMatColorCode"
        Me.FTRawMatColorCode.Name = "FTRawMatColorCode"
        Me.FTRawMatColorCode.OptionsColumn.AllowEdit = False
        Me.FTRawMatColorCode.OptionsColumn.ReadOnly = True
        Me.FTRawMatColorCode.Visible = True
        Me.FTRawMatColorCode.VisibleIndex = 4
        Me.FTRawMatColorCode.Width = 96
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
        Me.FTRawMatSizeCode.VisibleIndex = 5
        Me.FTRawMatSizeCode.Width = 90
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
        Me.FNQuantity.OptionsColumn.ReadOnly = True
        Me.FNQuantity.Visible = True
        Me.FNQuantity.VisibleIndex = 6
        Me.FNQuantity.Width = 85
        '
        'FTUnitCode
        '
        Me.FTUnitCode.AppearanceHeader.Options.UseTextOptions = True
        Me.FTUnitCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTUnitCode.Caption = "FTUnitCode"
        Me.FTUnitCode.FieldName = "FTUnitCode"
        Me.FTUnitCode.Name = "FTUnitCode"
        Me.FTUnitCode.OptionsColumn.AllowEdit = False
        Me.FTUnitCode.OptionsColumn.ReadOnly = True
        Me.FTUnitCode.Visible = True
        Me.FTUnitCode.VisibleIndex = 7
        Me.FTUnitCode.Width = 55
        '
        'FNPrice
        '
        Me.FNPrice.AppearanceHeader.Options.UseTextOptions = True
        Me.FNPrice.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FNPrice.Caption = "FNPrice"
        Me.FNPrice.DisplayFormat.FormatString = "{0:n4}"
        Me.FNPrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNPrice.FieldName = "FNPrice"
        Me.FNPrice.Name = "FNPrice"
        Me.FNPrice.OptionsColumn.AllowEdit = False
        Me.FNPrice.OptionsColumn.ReadOnly = True
        Me.FNPrice.Width = 85
        '
        'TotalPrice
        '
        Me.TotalPrice.AppearanceHeader.Options.UseTextOptions = True
        Me.TotalPrice.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.TotalPrice.Caption = "TotalPrice"
        Me.TotalPrice.DisplayFormat.FormatString = "{0:n4}"
        Me.TotalPrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.TotalPrice.FieldName = "TotalPrice"
        Me.TotalPrice.Name = "TotalPrice"
        Me.TotalPrice.OptionsColumn.AllowEdit = False
        Me.TotalPrice.OptionsColumn.ReadOnly = True
        Me.TotalPrice.Width = 100
        '
        'RepositoryItemCheckEdit1
        '
        Me.RepositoryItemCheckEdit1.AutoHeight = False
        Me.RepositoryItemCheckEdit1.Caption = "Check"
        Me.RepositoryItemCheckEdit1.Name = "RepositoryItemCheckEdit1"
        '
        'RepositoryItemCheckEdit2
        '
        Me.RepositoryItemCheckEdit2.AutoHeight = False
        Me.RepositoryItemCheckEdit2.Caption = "Check"
        Me.RepositoryItemCheckEdit2.Name = "RepositoryItemCheckEdit2"
        '
        'PurchasingTrackingForDepartment
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1227, 740)
        Me.Controls.Add(Me.ogbdetail)
        Me.Controls.Add(Me.ogbheader)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "PurchasingTrackingForDepartment"
        Me.Text = "PurchasingTrackingForDepartment"
        CType(Me.DockManager1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbheader.ResumeLayout(False)
        Me.DockPanel1_Container.ResumeLayout(False)
        CType(Me.FTCountryName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ProvinceName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTContiName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTStyleCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTCustomerPO.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTSubOrderNo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogbdetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbdetail.ResumeLayout(False)
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbmainprocbutton.ResumeLayout(False)
        CType(Me.ochkselectall.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogcTrackingDept, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvTrackingDept, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositorySelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents DockManager1 As DevExpress.XtraBars.Docking.DockManager
    Friend WithEvents ogbheader As DevExpress.XtraBars.Docking.DockPanel
    Friend WithEvents DockPanel1_Container As DevExpress.XtraBars.Docking.ControlContainer
    Friend WithEvents ogbdetail As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ogcTrackingDept As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvTrackingDept As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTCustomerPO As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTSubOrderNo As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FTStyleCode As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FTCountryName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents ProvinceName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FTContiName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents RepositorySelect As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents FTPurchaseNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ochkselectall As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents FTInvoiceNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTRawMatCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTSuplName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTRawMatColorCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTRawMatSizeCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTUnitCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNPrice As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents TotalPrice As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ogbmainprocbutton As DevExpress.XtraEditors.PanelControl
    Friend WithEvents ocmpreview As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmsavelayout As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmclear As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmload As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LabelControl6 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl5 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTSelect As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemCheckEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents RepositoryItemCheckEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
End Class

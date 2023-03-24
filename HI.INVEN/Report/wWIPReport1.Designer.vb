<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wWIPReport1
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(wWIPReport1))
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.ogbheader = New DevExpress.XtraBars.Docking.DockPanel()
        Me.DockPanel1_Container = New DevExpress.XtraBars.Docking.ControlContainer()
        Me.oSelectDate = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.DateEdit1 = New DevExpress.XtraEditors.DateEdit()
        Me.FTMonth = New DevExpress.XtraEditors.DateEdit()
        Me.FTDateE = New DevExpress.XtraEditors.DateEdit()
        Me.FTDateE_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTDateS = New DevExpress.XtraEditors.DateEdit()
        Me.oSelectDate_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTMonth_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTDateS_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysCmpId_None = New DevExpress.XtraEditors.TextEdit()
        Me.FNHSysCmpId_lbl2 = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysCmpId = New DevExpress.XtraEditors.ButtonEdit()
        Me.ogbdetail = New DevExpress.XtraEditors.GroupControl()
        Me.ogbmainprocbutton = New DevExpress.XtraEditors.PanelControl()
        Me.ocmpreview = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmsavelayout = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmclear = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmload = New DevExpress.XtraEditors.SimpleButton()
        Me.ogcDetial = New DevExpress.XtraGrid.GridControl()
        Me.ogvDetail = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.cFTPurchaseNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cStyleNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTOrderNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNOrderQty = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFDDeliveryDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemDateEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemDateEdit()
        Me.cFTRawMatCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTRawMatColorCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTMatSizeCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cMATERIAL_TYPE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTUnitCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTPurchaseNo2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTSuplCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNReserveQty = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTReceiveNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTInvoiceNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTTransferWHNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNPrice = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNQuantityFabric = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNQuantityAcc = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cNQtyFabricCut = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cNQtyAccCut = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNWIPFabricQty = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNWIPAccQty = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNFGFabricQty = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNFGAccQty = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNExport1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNExport2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNExport3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNFinish1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNFinish2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNFinish3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNBalance1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNBalance2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNBalance3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNFabricBalQty = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNAccBalQty = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cCmp = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cDocDateOut = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryFTPurchaseNo = New DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit()
        Me.oDockManager = New DevExpress.XtraBars.Docking.DockManager()
        Me.hideContainerTop = New DevExpress.XtraBars.Docking.AutoHideContainer()
        Me.ogbheader.SuspendLayout()
        Me.DockPanel1_Container.SuspendLayout()
        CType(Me.oSelectDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DateEdit1.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DateEdit1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTMonth.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTMonth.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTDateE.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTDateE.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTDateS.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTDateS.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysCmpId_None.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysCmpId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogbdetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbdetail.SuspendLayout()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbmainprocbutton.SuspendLayout()
        CType(Me.ogcDetial, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemDateEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemDateEdit1.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryFTPurchaseNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.oDockManager, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.hideContainerTop.SuspendLayout()
        Me.SuspendLayout()
        '
        'ogbheader
        '
        Me.ogbheader.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.ogbheader.Appearance.Options.UseForeColor = True
        Me.ogbheader.Appearance.Options.UseTextOptions = True
        Me.ogbheader.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ogbheader.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.ogbheader.Controls.Add(Me.DockPanel1_Container)
        Me.ogbheader.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ogbheader.Dock = DevExpress.XtraBars.Docking.DockingStyle.Top
        Me.ogbheader.DockVertical = DevExpress.Utils.DefaultBoolean.[False]
        Me.ogbheader.ID = New System.Guid("77b9346d-8d15-4323-af1e-af82afa9902a")
        Me.ogbheader.Image = CType(resources.GetObject("ogbheader.Image"), System.Drawing.Image)
        Me.ogbheader.Location = New System.Drawing.Point(0, 34)
        Me.ogbheader.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbheader.Name = "ogbheader"
        Me.ogbheader.Options.AllowDockBottom = False
        Me.ogbheader.Options.AllowDockFill = False
        Me.ogbheader.Options.FloatOnDblClick = False
        Me.ogbheader.Options.ShowCloseButton = False
        Me.ogbheader.Options.ShowMaximizeButton = False
        Me.ogbheader.OriginalSize = New System.Drawing.Size(1529, 148)
        Me.ogbheader.SavedDock = DevExpress.XtraBars.Docking.DockingStyle.Top
        Me.ogbheader.SavedIndex = 0
        Me.ogbheader.Size = New System.Drawing.Size(1529, 148)
        Me.ogbheader.TabStop = False
        Me.ogbheader.Text = "Criteria"
        Me.ogbheader.Visibility = DevExpress.XtraBars.Docking.DockVisibility.AutoHide
        '
        'DockPanel1_Container
        '
        Me.DockPanel1_Container.Controls.Add(Me.oSelectDate)
        Me.DockPanel1_Container.Controls.Add(Me.DateEdit1)
        Me.DockPanel1_Container.Controls.Add(Me.FTMonth)
        Me.DockPanel1_Container.Controls.Add(Me.FTDateE)
        Me.DockPanel1_Container.Controls.Add(Me.FTDateE_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FTDateS)
        Me.DockPanel1_Container.Controls.Add(Me.oSelectDate_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FTMonth_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FTDateS_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FNHSysCmpId_None)
        Me.DockPanel1_Container.Controls.Add(Me.FNHSysCmpId_lbl2)
        Me.DockPanel1_Container.Controls.Add(Me.FNHSysCmpId)
        Me.DockPanel1_Container.Location = New System.Drawing.Point(2, 25)
        Me.DockPanel1_Container.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.DockPanel1_Container.Name = "DockPanel1_Container"
        Me.DockPanel1_Container.Size = New System.Drawing.Size(1525, 121)
        Me.DockPanel1_Container.TabIndex = 0
        '
        'oSelectDate
        '
        Me.oSelectDate.Location = New System.Drawing.Point(153, 28)
        Me.oSelectDate.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.oSelectDate.Name = "oSelectDate"
        Me.oSelectDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.oSelectDate.Properties.Tag = "FNDateType"
        Me.oSelectDate.Size = New System.Drawing.Size(127, 22)
        Me.oSelectDate.TabIndex = 1
        '
        'DateEdit1
        '
        Me.DateEdit1.EditValue = Nothing
        Me.DateEdit1.Enabled = False
        Me.DateEdit1.Location = New System.Drawing.Point(437, 28)
        Me.DateEdit1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.DateEdit1.Name = "DateEdit1"
        Me.DateEdit1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.DateEdit1.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.DateEdit1.Properties.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.TouchUI
        Me.DateEdit1.Properties.DisplayFormat.FormatString = "y"
        Me.DateEdit1.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.DateEdit1.Properties.EditFormat.FormatString = "y"
        Me.DateEdit1.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.DateEdit1.Properties.Mask.EditMask = "y"
        Me.DateEdit1.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.[False]
        Me.DateEdit1.Size = New System.Drawing.Size(152, 22)
        Me.DateEdit1.TabIndex = 294
        '
        'FTMonth
        '
        Me.FTMonth.EditValue = Nothing
        Me.FTMonth.Location = New System.Drawing.Point(437, 28)
        Me.FTMonth.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTMonth.Name = "FTMonth"
        Me.FTMonth.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FTMonth.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FTMonth.Properties.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.TouchUI
        Me.FTMonth.Properties.DisplayFormat.FormatString = "y"
        Me.FTMonth.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.FTMonth.Properties.EditFormat.FormatString = "y"
        Me.FTMonth.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.FTMonth.Properties.Mask.EditMask = "y"
        Me.FTMonth.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTMonth.Size = New System.Drawing.Size(152, 22)
        Me.FTMonth.TabIndex = 294
        '
        'FTDateE
        '
        Me.FTDateE.EditValue = Nothing
        Me.FTDateE.EnterMoveNextControl = True
        Me.FTDateE.Location = New System.Drawing.Point(817, 53)
        Me.FTDateE.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTDateE.Name = "FTDateE"
        Me.FTDateE.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTDateE.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FTDateE.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.FTDateE.Properties.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.Vista
        Me.FTDateE.Properties.DisplayFormat.FormatString = "d"
        Me.FTDateE.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FTDateE.Properties.EditFormat.FormatString = "d"
        Me.FTDateE.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FTDateE.Properties.Mask.EditMask = "m"
        Me.FTDateE.Properties.NullDate = ""
        Me.FTDateE.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.[True]
        Me.FTDateE.Size = New System.Drawing.Size(152, 22)
        Me.FTDateE.TabIndex = 270
        Me.FTDateE.Tag = "2|"
        '
        'FTDateE_lbl
        '
        Me.FTDateE_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTDateE_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTDateE_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTDateE_lbl.Location = New System.Drawing.Point(596, 54)
        Me.FTDateE_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTDateE_lbl.Name = "FTDateE_lbl"
        Me.FTDateE_lbl.Size = New System.Drawing.Size(211, 23)
        Me.FTDateE_lbl.TabIndex = 271
        Me.FTDateE_lbl.Tag = "2|"
        Me.FTDateE_lbl.Text = "FTDateE"
        '
        'FTDateS
        '
        Me.FTDateS.EditValue = New Date(CType(0, Long))
        Me.FTDateS.EnterMoveNextControl = True
        Me.FTDateS.Location = New System.Drawing.Point(437, 54)
        Me.FTDateS.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTDateS.Name = "FTDateS"
        Me.FTDateS.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTDateS.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FTDateS.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.FTDateS.Properties.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.Vista
        Me.FTDateS.Properties.Mask.EditMask = ""
        Me.FTDateS.Properties.NullDate = ""
        Me.FTDateS.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.[True]
        Me.FTDateS.Size = New System.Drawing.Size(152, 22)
        Me.FTDateS.TabIndex = 268
        Me.FTDateS.Tag = "2|"
        '
        'oSelectDate_lbl
        '
        Me.oSelectDate_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.oSelectDate_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.oSelectDate_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.oSelectDate_lbl.Location = New System.Drawing.Point(1, 28)
        Me.oSelectDate_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.oSelectDate_lbl.Name = "oSelectDate_lbl"
        Me.oSelectDate_lbl.Size = New System.Drawing.Size(145, 23)
        Me.oSelectDate_lbl.TabIndex = 269
        Me.oSelectDate_lbl.Tag = "2|"
        Me.oSelectDate_lbl.Text = "Select Date Type :"
        '
        'FTMonth_lbl
        '
        Me.FTMonth_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTMonth_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTMonth_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTMonth_lbl.Location = New System.Drawing.Point(286, 30)
        Me.FTMonth_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTMonth_lbl.Name = "FTMonth_lbl"
        Me.FTMonth_lbl.Size = New System.Drawing.Size(145, 23)
        Me.FTMonth_lbl.TabIndex = 269
        Me.FTMonth_lbl.Tag = "2|"
        Me.FTMonth_lbl.Text = "Month"
        '
        'FTDateS_lbl
        '
        Me.FTDateS_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTDateS_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTDateS_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTDateS_lbl.Location = New System.Drawing.Point(286, 54)
        Me.FTDateS_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTDateS_lbl.Name = "FTDateS_lbl"
        Me.FTDateS_lbl.Size = New System.Drawing.Size(146, 23)
        Me.FTDateS_lbl.TabIndex = 269
        Me.FTDateS_lbl.Tag = "2|"
        Me.FTDateS_lbl.Text = "FTDateS"
        '
        'FNHSysCmpId_None
        '
        Me.FNHSysCmpId_None.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FNHSysCmpId_None.Location = New System.Drawing.Point(596, 1)
        Me.FNHSysCmpId_None.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysCmpId_None.Name = "FNHSysCmpId_None"
        Me.FNHSysCmpId_None.Properties.Appearance.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysCmpId_None.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysCmpId_None.Properties.ReadOnly = True
        Me.FNHSysCmpId_None.Size = New System.Drawing.Size(876, 22)
        Me.FNHSysCmpId_None.TabIndex = 291
        Me.FNHSysCmpId_None.Tag = "2|"
        '
        'FNHSysCmpId_lbl2
        '
        Me.FNHSysCmpId_lbl2.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FNHSysCmpId_lbl2.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysCmpId_lbl2.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysCmpId_lbl2.Location = New System.Drawing.Point(9, 4)
        Me.FNHSysCmpId_lbl2.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysCmpId_lbl2.Name = "FNHSysCmpId_lbl2"
        Me.FNHSysCmpId_lbl2.Size = New System.Drawing.Size(421, 25)
        Me.FNHSysCmpId_lbl2.TabIndex = 293
        Me.FNHSysCmpId_lbl2.Tag = "2|"
        Me.FNHSysCmpId_lbl2.Text = "FNHSysCmpId"
        '
        'FNHSysCmpId
        '
        Me.FNHSysCmpId.Location = New System.Drawing.Point(436, 1)
        Me.FNHSysCmpId.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysCmpId.Name = "FNHSysCmpId"
        Me.FNHSysCmpId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", "11", Nothing, True)})
        Me.FNHSysCmpId.Properties.Tag = ""
        Me.FNHSysCmpId.Size = New System.Drawing.Size(152, 22)
        Me.FNHSysCmpId.TabIndex = 0
        Me.FNHSysCmpId.Tag = "2|"
        '
        'ogbdetail
        '
        Me.ogbdetail.Controls.Add(Me.ogbmainprocbutton)
        Me.ogbdetail.Controls.Add(Me.ogcDetial)
        Me.ogbdetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogbdetail.Location = New System.Drawing.Point(0, 34)
        Me.ogbdetail.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbdetail.Name = "ogbdetail"
        Me.ogbdetail.ShowCaption = False
        Me.ogbdetail.Size = New System.Drawing.Size(1529, 745)
        Me.ogbdetail.TabIndex = 0
        '
        'ogbmainprocbutton
        '
        Me.ogbmainprocbutton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmpreview)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmsavelayout)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmexit)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmclear)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmload)
        Me.ogbmainprocbutton.Location = New System.Drawing.Point(311, 229)
        Me.ogbmainprocbutton.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbmainprocbutton.Name = "ogbmainprocbutton"
        Me.ogbmainprocbutton.Size = New System.Drawing.Size(1091, 58)
        Me.ogbmainprocbutton.TabIndex = 386
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
        'ogcDetial
        '
        Me.ogcDetial.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogcDetial.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcDetial.Location = New System.Drawing.Point(2, 2)
        Me.ogcDetial.MainView = Me.ogvDetail
        Me.ogcDetial.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcDetial.Name = "ogcDetial"
        Me.ogcDetial.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryFTPurchaseNo, Me.RepositoryItemDateEdit1})
        Me.ogcDetial.Size = New System.Drawing.Size(1525, 741)
        Me.ogcDetial.TabIndex = 387
        Me.ogcDetial.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvDetail})
        '
        'ogvDetail
        '
        Me.ogvDetail.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.cFTPurchaseNo, Me.cStyleNo, Me.cFTOrderNo, Me.cFNOrderQty, Me.cFDDeliveryDate, Me.cFTRawMatCode, Me.cFTRawMatColorCode, Me.cFTMatSizeCode, Me.cMATERIAL_TYPE, Me.cFTUnitCode, Me.cFTPurchaseNo2, Me.cFTSuplCode, Me.cFNReserveQty, Me.cFTReceiveNo, Me.cFTInvoiceNo, Me.cFTTransferWHNo, Me.cFNQuantity, Me.cFNPrice, Me.cFNQuantityFabric, Me.cFNQuantityAcc, Me.cNQtyFabricCut, Me.cNQtyAccCut, Me.cFNWIPFabricQty, Me.cFNWIPAccQty, Me.cFNFGFabricQty, Me.cFNFGAccQty, Me.cFNExport1, Me.cFNExport2, Me.cFNExport3, Me.cFNFinish1, Me.cFNFinish2, Me.cFNFinish3, Me.cFNBalance1, Me.cFNBalance2, Me.cFNBalance3, Me.cFNFabricBalQty, Me.cFNAccBalQty, Me.cCmp, Me.cDocDateOut})
        Me.ogvDetail.GridControl = Me.ogcDetial
        Me.ogvDetail.Name = "ogvDetail"
        Me.ogvDetail.OptionsView.AllowCellMerge = True
        Me.ogvDetail.OptionsView.ColumnAutoWidth = False
        Me.ogvDetail.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
        Me.ogvDetail.OptionsView.ShowFooter = True
        Me.ogvDetail.OptionsView.ShowGroupPanel = False
        '
        'cFTPurchaseNo
        '
        Me.cFTPurchaseNo.Caption = "PO"
        Me.cFTPurchaseNo.FieldName = "FTPORef"
        Me.cFTPurchaseNo.Name = "cFTPurchaseNo"
        Me.cFTPurchaseNo.OptionsColumn.AllowEdit = False
        Me.cFTPurchaseNo.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.cFTPurchaseNo.Visible = True
        Me.cFTPurchaseNo.VisibleIndex = 1
        Me.cFTPurchaseNo.Width = 74
        '
        'cStyleNo
        '
        Me.cStyleNo.Caption = "StyleNo"
        Me.cStyleNo.FieldName = "StyleNo"
        Me.cStyleNo.Name = "cStyleNo"
        Me.cStyleNo.OptionsColumn.AllowEdit = False
        Me.cStyleNo.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.cStyleNo.Visible = True
        Me.cStyleNo.VisibleIndex = 2
        Me.cStyleNo.Width = 74
        '
        'cFTOrderNo
        '
        Me.cFTOrderNo.Caption = "FO No."
        Me.cFTOrderNo.FieldName = "FTOrderNo"
        Me.cFTOrderNo.Name = "cFTOrderNo"
        Me.cFTOrderNo.OptionsColumn.AllowEdit = False
        Me.cFTOrderNo.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.cFTOrderNo.Visible = True
        Me.cFTOrderNo.VisibleIndex = 3
        Me.cFTOrderNo.Width = 74
        '
        'cFNOrderQty
        '
        Me.cFNOrderQty.Caption = "ORDER QTY"
        Me.cFNOrderQty.DisplayFormat.FormatString = "{0:n4}"
        Me.cFNOrderQty.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cFNOrderQty.FieldName = "FNOrderQty"
        Me.cFNOrderQty.Name = "cFNOrderQty"
        Me.cFNOrderQty.OptionsColumn.AllowEdit = False
        Me.cFNOrderQty.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.cFNOrderQty.Visible = True
        Me.cFNOrderQty.VisibleIndex = 4
        Me.cFNOrderQty.Width = 74
        '
        'cFDDeliveryDate
        '
        Me.cFDDeliveryDate.Caption = "SHIPDATE"
        Me.cFDDeliveryDate.ColumnEdit = Me.RepositoryItemDateEdit1
        Me.cFDDeliveryDate.FieldName = "FDDeliveryDate"
        Me.cFDDeliveryDate.Name = "cFDDeliveryDate"
        Me.cFDDeliveryDate.OptionsColumn.AllowEdit = False
        Me.cFDDeliveryDate.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.cFDDeliveryDate.Visible = True
        Me.cFDDeliveryDate.VisibleIndex = 5
        Me.cFDDeliveryDate.Width = 74
        '
        'RepositoryItemDateEdit1
        '
        Me.RepositoryItemDateEdit1.AutoHeight = False
        Me.RepositoryItemDateEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemDateEdit1.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemDateEdit1.Name = "RepositoryItemDateEdit1"
        '
        'cFTRawMatCode
        '
        Me.cFTRawMatCode.Caption = "MATERIAL CODE"
        Me.cFTRawMatCode.FieldName = "FTRawMatCode"
        Me.cFTRawMatCode.Name = "cFTRawMatCode"
        Me.cFTRawMatCode.OptionsColumn.AllowEdit = False
        Me.cFTRawMatCode.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.cFTRawMatCode.Visible = True
        Me.cFTRawMatCode.VisibleIndex = 6
        Me.cFTRawMatCode.Width = 74
        '
        'cFTRawMatColorCode
        '
        Me.cFTRawMatColorCode.Caption = "COLOR CODE"
        Me.cFTRawMatColorCode.FieldName = "FTRawMatColorCode"
        Me.cFTRawMatColorCode.Name = "cFTRawMatColorCode"
        Me.cFTRawMatColorCode.OptionsColumn.AllowEdit = False
        Me.cFTRawMatColorCode.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.cFTRawMatColorCode.Visible = True
        Me.cFTRawMatColorCode.VisibleIndex = 7
        Me.cFTRawMatColorCode.Width = 74
        '
        'cFTMatSizeCode
        '
        Me.cFTMatSizeCode.Caption = "SIZE CODE"
        Me.cFTMatSizeCode.FieldName = "FTRawMatSizeCode"
        Me.cFTMatSizeCode.Name = "cFTMatSizeCode"
        Me.cFTMatSizeCode.OptionsColumn.AllowEdit = False
        Me.cFTMatSizeCode.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.cFTMatSizeCode.Visible = True
        Me.cFTMatSizeCode.VisibleIndex = 8
        Me.cFTMatSizeCode.Width = 74
        '
        'cMATERIAL_TYPE
        '
        Me.cMATERIAL_TYPE.Caption = "MATERIAL TYPE"
        Me.cMATERIAL_TYPE.FieldName = "FTMatTypeCode"
        Me.cMATERIAL_TYPE.Name = "cMATERIAL_TYPE"
        Me.cMATERIAL_TYPE.OptionsColumn.AllowEdit = False
        Me.cMATERIAL_TYPE.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.cMATERIAL_TYPE.Visible = True
        Me.cMATERIAL_TYPE.VisibleIndex = 9
        Me.cMATERIAL_TYPE.Width = 74
        '
        'cFTUnitCode
        '
        Me.cFTUnitCode.Caption = "UNIT"
        Me.cFTUnitCode.FieldName = "FTUnitCode"
        Me.cFTUnitCode.Name = "cFTUnitCode"
        Me.cFTUnitCode.OptionsColumn.AllowEdit = False
        Me.cFTUnitCode.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.cFTUnitCode.Visible = True
        Me.cFTUnitCode.VisibleIndex = 10
        Me.cFTUnitCode.Width = 74
        '
        'cFTPurchaseNo2
        '
        Me.cFTPurchaseNo2.Caption = "PO/NO."
        Me.cFTPurchaseNo2.FieldName = "FTPurchaseNo"
        Me.cFTPurchaseNo2.Name = "cFTPurchaseNo2"
        Me.cFTPurchaseNo2.OptionsColumn.AllowEdit = False
        Me.cFTPurchaseNo2.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.cFTPurchaseNo2.Visible = True
        Me.cFTPurchaseNo2.VisibleIndex = 11
        Me.cFTPurchaseNo2.Width = 74
        '
        'cFTSuplCode
        '
        Me.cFTSuplCode.Caption = "SUPPLIER"
        Me.cFTSuplCode.FieldName = "FTSuplCode"
        Me.cFTSuplCode.Name = "cFTSuplCode"
        Me.cFTSuplCode.OptionsColumn.AllowEdit = False
        Me.cFTSuplCode.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.cFTSuplCode.Visible = True
        Me.cFTSuplCode.VisibleIndex = 12
        Me.cFTSuplCode.Width = 74
        '
        'cFNReserveQty
        '
        Me.cFNReserveQty.Caption = "RESERVE QTY."
        Me.cFNReserveQty.DisplayFormat.FormatString = "{0:n4}"
        Me.cFNReserveQty.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cFNReserveQty.FieldName = "ReserveQty"
        Me.cFNReserveQty.Name = "cFNReserveQty"
        Me.cFNReserveQty.OptionsColumn.AllowEdit = False
        Me.cFNReserveQty.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.cFNReserveQty.Visible = True
        Me.cFNReserveQty.VisibleIndex = 13
        Me.cFNReserveQty.Width = 74
        '
        'cFTReceiveNo
        '
        Me.cFTReceiveNo.Caption = "FTReceiveNo"
        Me.cFTReceiveNo.FieldName = "FTReceiveNo"
        Me.cFTReceiveNo.Name = "cFTReceiveNo"
        Me.cFTReceiveNo.OptionsColumn.AllowEdit = False
        Me.cFTReceiveNo.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.cFTReceiveNo.Visible = True
        Me.cFTReceiveNo.VisibleIndex = 14
        Me.cFTReceiveNo.Width = 74
        '
        'cFTInvoiceNo
        '
        Me.cFTInvoiceNo.Caption = "INVOICE NO"
        Me.cFTInvoiceNo.FieldName = "FTInvoiceNo"
        Me.cFTInvoiceNo.Name = "cFTInvoiceNo"
        Me.cFTInvoiceNo.OptionsColumn.AllowEdit = False
        Me.cFTInvoiceNo.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.cFTInvoiceNo.Visible = True
        Me.cFTInvoiceNo.VisibleIndex = 15
        Me.cFTInvoiceNo.Width = 74
        '
        'cFTTransferWHNo
        '
        Me.cFTTransferWHNo.Caption = "TRANFER NO"
        Me.cFTTransferWHNo.FieldName = "FTTransferWHNo"
        Me.cFTTransferWHNo.Name = "cFTTransferWHNo"
        Me.cFTTransferWHNo.OptionsColumn.AllowEdit = False
        Me.cFTTransferWHNo.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.cFTTransferWHNo.Visible = True
        Me.cFTTransferWHNo.VisibleIndex = 16
        Me.cFTTransferWHNo.Width = 74
        '
        'cFNQuantity
        '
        Me.cFNQuantity.Caption = "QTY"
        Me.cFNQuantity.DisplayFormat.FormatString = "{0:n2}"
        Me.cFNQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cFNQuantity.FieldName = "QtyRCV"
        Me.cFNQuantity.Name = "cFNQuantity"
        Me.cFNQuantity.OptionsColumn.AllowEdit = False
        Me.cFNQuantity.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.cFNQuantity.Visible = True
        Me.cFNQuantity.VisibleIndex = 18
        Me.cFNQuantity.Width = 114
        '
        'cFNPrice
        '
        Me.cFNPrice.Caption = "Unit / Price"
        Me.cFNPrice.DisplayFormat.FormatString = "{0:n4}"
        Me.cFNPrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cFNPrice.FieldName = "FNPrice"
        Me.cFNPrice.Name = "cFNPrice"
        Me.cFNPrice.OptionsColumn.AllowEdit = False
        Me.cFNPrice.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.cFNPrice.Visible = True
        Me.cFNPrice.VisibleIndex = 19
        Me.cFNPrice.Width = 74
        '
        'cFNQuantityFabric
        '
        Me.cFNQuantityFabric.Caption = "Fabric Price"
        Me.cFNQuantityFabric.DisplayFormat.FormatString = "{0:n2}"
        Me.cFNQuantityFabric.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cFNQuantityFabric.FieldName = "AmtFabric"
        Me.cFNQuantityFabric.Name = "cFNQuantityFabric"
        Me.cFNQuantityFabric.OptionsColumn.AllowEdit = False
        Me.cFNQuantityFabric.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.cFNQuantityFabric.Visible = True
        Me.cFNQuantityFabric.VisibleIndex = 20
        Me.cFNQuantityFabric.Width = 130
        '
        'cFNQuantityAcc
        '
        Me.cFNQuantityAcc.Caption = "Accessory Price"
        Me.cFNQuantityAcc.DisplayFormat.FormatString = "{0:n2}"
        Me.cFNQuantityAcc.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cFNQuantityAcc.FieldName = "AmtAcc"
        Me.cFNQuantityAcc.Name = "cFNQuantityAcc"
        Me.cFNQuantityAcc.OptionsColumn.AllowEdit = False
        Me.cFNQuantityAcc.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.cFNQuantityAcc.Visible = True
        Me.cFNQuantityAcc.VisibleIndex = 21
        Me.cFNQuantityAcc.Width = 139
        '
        'cNQtyFabricCut
        '
        Me.cNQtyFabricCut.Caption = "Fabric Cut"
        Me.cNQtyFabricCut.DisplayFormat.FormatString = "{0:n2}"
        Me.cNQtyFabricCut.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cNQtyFabricCut.FieldName = "FNCutFabricQty"
        Me.cNQtyFabricCut.Name = "cNQtyFabricCut"
        Me.cNQtyFabricCut.OptionsColumn.AllowEdit = False
        Me.cNQtyFabricCut.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.cNQtyFabricCut.Visible = True
        Me.cNQtyFabricCut.VisibleIndex = 22
        Me.cNQtyFabricCut.Width = 74
        '
        'cNQtyAccCut
        '
        Me.cNQtyAccCut.Caption = "Acc Cut"
        Me.cNQtyAccCut.DisplayFormat.FormatString = "{0:n2}"
        Me.cNQtyAccCut.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cNQtyAccCut.FieldName = "FNCutAccQty"
        Me.cNQtyAccCut.Name = "cNQtyAccCut"
        Me.cNQtyAccCut.OptionsColumn.AllowEdit = False
        Me.cNQtyAccCut.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.cNQtyAccCut.Visible = True
        Me.cNQtyAccCut.VisibleIndex = 23
        Me.cNQtyAccCut.Width = 74
        '
        'cFNWIPFabricQty
        '
        Me.cFNWIPFabricQty.Caption = "WIPFabricQty"
        Me.cFNWIPFabricQty.DisplayFormat.FormatString = "{0:n2}"
        Me.cFNWIPFabricQty.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cFNWIPFabricQty.FieldName = "FNWIPFabricQty"
        Me.cFNWIPFabricQty.Name = "cFNWIPFabricQty"
        Me.cFNWIPFabricQty.OptionsColumn.AllowEdit = False
        Me.cFNWIPFabricQty.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.cFNWIPFabricQty.Visible = True
        Me.cFNWIPFabricQty.VisibleIndex = 24
        Me.cFNWIPFabricQty.Width = 74
        '
        'cFNWIPAccQty
        '
        Me.cFNWIPAccQty.Caption = "WIPAccQty"
        Me.cFNWIPAccQty.DisplayFormat.FormatString = "{0:n2}"
        Me.cFNWIPAccQty.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cFNWIPAccQty.FieldName = "FNWIPAccQty"
        Me.cFNWIPAccQty.Name = "cFNWIPAccQty"
        Me.cFNWIPAccQty.OptionsColumn.AllowEdit = False
        Me.cFNWIPAccQty.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.cFNWIPAccQty.Visible = True
        Me.cFNWIPAccQty.VisibleIndex = 25
        Me.cFNWIPAccQty.Width = 74
        '
        'cFNFGFabricQty
        '
        Me.cFNFGFabricQty.Caption = "FGFabricQty"
        Me.cFNFGFabricQty.DisplayFormat.FormatString = "{0:n2}"
        Me.cFNFGFabricQty.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cFNFGFabricQty.FieldName = "FNFGFabricQty"
        Me.cFNFGFabricQty.Name = "cFNFGFabricQty"
        Me.cFNFGFabricQty.OptionsColumn.AllowEdit = False
        Me.cFNFGFabricQty.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.cFNFGFabricQty.Visible = True
        Me.cFNFGFabricQty.VisibleIndex = 26
        Me.cFNFGFabricQty.Width = 74
        '
        'cFNFGAccQty
        '
        Me.cFNFGAccQty.Caption = "FGAccQty"
        Me.cFNFGAccQty.DisplayFormat.FormatString = "{0:n2}"
        Me.cFNFGAccQty.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cFNFGAccQty.FieldName = "FNFGAccQty"
        Me.cFNFGAccQty.Name = "cFNFGAccQty"
        Me.cFNFGAccQty.OptionsColumn.AllowEdit = False
        Me.cFNFGAccQty.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.cFNFGAccQty.Visible = True
        Me.cFNFGAccQty.VisibleIndex = 27
        Me.cFNFGAccQty.Width = 74
        '
        'cFNExport1
        '
        Me.cFNExport1.Caption = "Export1"
        Me.cFNExport1.DisplayFormat.FormatString = "{0:n2}"
        Me.cFNExport1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cFNExport1.FieldName = "FNExport1"
        Me.cFNExport1.Name = "cFNExport1"
        Me.cFNExport1.OptionsColumn.AllowEdit = False
        Me.cFNExport1.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.cFNExport1.Visible = True
        Me.cFNExport1.VisibleIndex = 28
        Me.cFNExport1.Width = 74
        '
        'cFNExport2
        '
        Me.cFNExport2.Caption = "Export2"
        Me.cFNExport2.DisplayFormat.FormatString = "{0:n2}"
        Me.cFNExport2.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cFNExport2.FieldName = "FNExport2"
        Me.cFNExport2.Name = "cFNExport2"
        Me.cFNExport2.OptionsColumn.AllowEdit = False
        Me.cFNExport2.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.cFNExport2.Visible = True
        Me.cFNExport2.VisibleIndex = 29
        Me.cFNExport2.Width = 74
        '
        'cFNExport3
        '
        Me.cFNExport3.Caption = "Export3"
        Me.cFNExport3.DisplayFormat.FormatString = "{0:n2}"
        Me.cFNExport3.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cFNExport3.FieldName = "FNExport3"
        Me.cFNExport3.Name = "cFNExport3"
        Me.cFNExport3.OptionsColumn.AllowEdit = False
        Me.cFNExport3.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.cFNExport3.Visible = True
        Me.cFNExport3.VisibleIndex = 30
        Me.cFNExport3.Width = 74
        '
        'cFNFinish1
        '
        Me.cFNFinish1.Caption = "Finish1"
        Me.cFNFinish1.DisplayFormat.FormatString = "{0:n2}"
        Me.cFNFinish1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cFNFinish1.FieldName = "FNFinish1"
        Me.cFNFinish1.Name = "cFNFinish1"
        Me.cFNFinish1.OptionsColumn.AllowEdit = False
        Me.cFNFinish1.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.cFNFinish1.Visible = True
        Me.cFNFinish1.VisibleIndex = 31
        Me.cFNFinish1.Width = 74
        '
        'cFNFinish2
        '
        Me.cFNFinish2.Caption = "Finish2"
        Me.cFNFinish2.DisplayFormat.FormatString = "{0:n2}"
        Me.cFNFinish2.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cFNFinish2.FieldName = "FNFinish2"
        Me.cFNFinish2.Name = "cFNFinish2"
        Me.cFNFinish2.OptionsColumn.AllowEdit = False
        Me.cFNFinish2.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.cFNFinish2.Visible = True
        Me.cFNFinish2.VisibleIndex = 32
        Me.cFNFinish2.Width = 74
        '
        'cFNFinish3
        '
        Me.cFNFinish3.Caption = "Finish3"
        Me.cFNFinish3.DisplayFormat.FormatString = "{0:n2}"
        Me.cFNFinish3.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cFNFinish3.FieldName = "FNFinish3"
        Me.cFNFinish3.Name = "cFNFinish3"
        Me.cFNFinish3.OptionsColumn.AllowEdit = False
        Me.cFNFinish3.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.cFNFinish3.Visible = True
        Me.cFNFinish3.VisibleIndex = 33
        Me.cFNFinish3.Width = 74
        '
        'cFNBalance1
        '
        Me.cFNBalance1.Caption = "Balance1"
        Me.cFNBalance1.DisplayFormat.FormatString = "{0:n2}"
        Me.cFNBalance1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cFNBalance1.FieldName = "FNBalance1"
        Me.cFNBalance1.Name = "cFNBalance1"
        Me.cFNBalance1.OptionsColumn.AllowEdit = False
        Me.cFNBalance1.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.cFNBalance1.Visible = True
        Me.cFNBalance1.VisibleIndex = 34
        Me.cFNBalance1.Width = 74
        '
        'cFNBalance2
        '
        Me.cFNBalance2.Caption = "Balance2"
        Me.cFNBalance2.DisplayFormat.FormatString = "{0:n2}"
        Me.cFNBalance2.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cFNBalance2.FieldName = "FNBalance2"
        Me.cFNBalance2.Name = "cFNBalance2"
        Me.cFNBalance2.OptionsColumn.AllowEdit = False
        Me.cFNBalance2.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.cFNBalance2.Visible = True
        Me.cFNBalance2.VisibleIndex = 35
        Me.cFNBalance2.Width = 74
        '
        'cFNBalance3
        '
        Me.cFNBalance3.Caption = "Balance3"
        Me.cFNBalance3.DisplayFormat.FormatString = "{0:n2}"
        Me.cFNBalance3.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cFNBalance3.FieldName = "FNBalance3"
        Me.cFNBalance3.Name = "cFNBalance3"
        Me.cFNBalance3.OptionsColumn.AllowEdit = False
        Me.cFNBalance3.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.cFNBalance3.Visible = True
        Me.cFNBalance3.VisibleIndex = 36
        Me.cFNBalance3.Width = 74
        '
        'cFNFabricBalQty
        '
        Me.cFNFabricBalQty.Caption = "FNFabricBalQty"
        Me.cFNFabricBalQty.DisplayFormat.FormatString = "{0:n2}"
        Me.cFNFabricBalQty.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cFNFabricBalQty.FieldName = "FNFabricBalQty"
        Me.cFNFabricBalQty.Name = "cFNFabricBalQty"
        Me.cFNFabricBalQty.OptionsColumn.AllowEdit = False
        Me.cFNFabricBalQty.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.cFNFabricBalQty.Visible = True
        Me.cFNFabricBalQty.VisibleIndex = 37
        '
        'cFNAccBalQty
        '
        Me.cFNAccBalQty.Caption = "FNAccBalQty"
        Me.cFNAccBalQty.DisplayFormat.FormatString = "{0:n2}"
        Me.cFNAccBalQty.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cFNAccBalQty.FieldName = "FNAccBalQty"
        Me.cFNAccBalQty.Name = "cFNAccBalQty"
        Me.cFNAccBalQty.OptionsColumn.AllowEdit = False
        Me.cFNAccBalQty.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.cFNAccBalQty.Visible = True
        Me.cFNAccBalQty.VisibleIndex = 38
        '
        'cCmp
        '
        Me.cCmp.Caption = "Company"
        Me.cCmp.FieldName = "FTCmpCode"
        Me.cCmp.Name = "cCmp"
        Me.cCmp.OptionsColumn.AllowEdit = False
        Me.cCmp.Visible = True
        Me.cCmp.VisibleIndex = 0
        '
        'cDocDateOut
        '
        Me.cDocDateOut.Caption = "DocDate Out"
        Me.cDocDateOut.FieldName = "FDDocDateOut"
        Me.cDocDateOut.Name = "cDocDateOut"
        Me.cDocDateOut.OptionsColumn.AllowEdit = False
        Me.cDocDateOut.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.cDocDateOut.Visible = True
        Me.cDocDateOut.VisibleIndex = 17
        '
        'RepositoryFTPurchaseNo
        '
        Me.RepositoryFTPurchaseNo.AutoHeight = False
        Me.RepositoryFTPurchaseNo.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryFTPurchaseNo.Name = "RepositoryFTPurchaseNo"
        '
        'oDockManager
        '
        Me.oDockManager.AutoHideContainers.AddRange(New DevExpress.XtraBars.Docking.AutoHideContainer() {Me.hideContainerTop})
        Me.oDockManager.Form = Me
        Me.oDockManager.TopZIndexControls.AddRange(New String() {"DevExpress.XtraBars.BarDockControl", "DevExpress.XtraBars.StandaloneBarDockControl", "System.Windows.Forms.StatusBar", "DevExpress.XtraBars.Ribbon.RibbonStatusBar", "DevExpress.XtraBars.Ribbon.RibbonControl"})
        '
        'hideContainerTop
        '
        Me.hideContainerTop.BackColor = System.Drawing.Color.FromArgb(CType(CType(232, Byte), Integer), CType(CType(232, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.hideContainerTop.Controls.Add(Me.ogbheader)
        Me.hideContainerTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.hideContainerTop.Location = New System.Drawing.Point(0, 0)
        Me.hideContainerTop.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.hideContainerTop.Name = "hideContainerTop"
        Me.hideContainerTop.Size = New System.Drawing.Size(1529, 34)
        '
        'wWIPReport1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1529, 779)
        Me.Controls.Add(Me.ogbdetail)
        Me.Controls.Add(Me.hideContainerTop)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "wWIPReport1"
        Me.Text = "Order Tracking"
        Me.ogbheader.ResumeLayout(False)
        Me.DockPanel1_Container.ResumeLayout(False)
        CType(Me.oSelectDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DateEdit1.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DateEdit1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTMonth.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTMonth.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTDateE.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTDateE.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTDateS.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTDateS.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysCmpId_None.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysCmpId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogbdetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbdetail.ResumeLayout(False)
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbmainprocbutton.ResumeLayout(False)
        CType(Me.ogcDetial, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemDateEdit1.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemDateEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryFTPurchaseNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.oDockManager, System.ComponentModel.ISupportInitialize).EndInit()
        Me.hideContainerTop.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogbdetail As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ogbmainprocbutton As DevExpress.XtraEditors.PanelControl
    Friend WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmclear As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmload As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents oDockManager As DevExpress.XtraBars.Docking.DockManager
    Friend WithEvents ogbheader As DevExpress.XtraBars.Docking.DockPanel
    Friend WithEvents DockPanel1_Container As DevExpress.XtraBars.Docking.ControlContainer
    Friend WithEvents ocmsavelayout As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FNHSysCmpId_None As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FNHSysCmpId_lbl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysCmpId As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FTDateE As DevExpress.XtraEditors.DateEdit
    Friend WithEvents FTDateE_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTDateS As DevExpress.XtraEditors.DateEdit
    Friend WithEvents FTDateS_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ocmpreview As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents hideContainerTop As DevExpress.XtraBars.Docking.AutoHideContainer
    Friend WithEvents ogcDetial As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvDetail As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents cFTPurchaseNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cStyleNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTOrderNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNOrderQty As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFDDeliveryDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTRawMatCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTRawMatColorCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTMatSizeCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cMATERIAL_TYPE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTUnitCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTPurchaseNo2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTSuplCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNReserveQty As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTReceiveNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTInvoiceNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTTransferWHNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNPrice As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNQuantityFabric As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNQuantityAcc As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cNQtyFabricCut As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cNQtyAccCut As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNWIPFabricQty As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNWIPAccQty As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNFGFabricQty As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNFGAccQty As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNExport1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNExport2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNExport3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNFinish1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNFinish2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNFinish3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNBalance1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNBalance2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNBalance3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNFabricBalQty As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNAccBalQty As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryFTPurchaseNo As DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit
    Friend WithEvents RepositoryItemDateEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemDateEdit
    Friend WithEvents cCmp As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cDocDateOut As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTMonth_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTMonth As DevExpress.XtraEditors.DateEdit
    Friend WithEvents DateEdit1 As DevExpress.XtraEditors.DateEdit
    Friend WithEvents oSelectDate As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents oSelectDate_lbl As DevExpress.XtraEditors.LabelControl
End Class

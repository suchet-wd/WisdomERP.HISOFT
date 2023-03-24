Imports DevExpress.XtraEditors.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wPartialShipment
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(wPartialShipment))
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject4 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.ocmdoc = New DevExpress.XtraBars.Docking.DockManager(Me.components)
        Me.ogbheader = New DevExpress.XtraBars.Docking.DockPanel()
        Me.DockPanel1_Container = New DevExpress.XtraBars.Docking.ControlContainer()
        Me.FTColorway = New DevExpress.XtraEditors.ButtonEdit()
        Me.FTColorway_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysStyleId_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTColorway_None = New DevExpress.XtraEditors.TextEdit()
        Me.FNHSysCmpId_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysStyleId = New DevExpress.XtraEditors.ButtonEdit()
        Me.FNHSysStyleId_None = New DevExpress.XtraEditors.TextEdit()
        Me.FTCustomerPO = New DevExpress.XtraEditors.ButtonEdit()
        Me.FNHSysCmpId_None = New DevExpress.XtraEditors.TextEdit()
        Me.FTCustomerPO_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysCmpId = New DevExpress.XtraEditors.ButtonEdit()
        Me.FTNikePOLineItem = New DevExpress.XtraEditors.TextEdit()
        Me.FTNikePOLineItem_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.ogbmainprocbutton = New DevExpress.XtraEditors.PanelControl()
        Me.ocmremove = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmadd = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmexporttoexcel = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmclear = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmdelete = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmapprovepoline = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmapprove = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmreject = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmSendApprove = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmsavedocument = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmsave = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmload = New DevExpress.XtraEditors.SimpleButton()
        Me.FTStatePartialShip = New DevExpress.XtraEditors.CheckEdit()
        Me.FTStateShortShip = New DevExpress.XtraEditors.CheckEdit()
        Me.otb = New DevExpress.XtraTab.XtraTabControl()
        Me.otpdetail = New DevExpress.XtraTab.XtraTabPage()
        Me.ogdColorSizeBreakdown = New DevExpress.XtraGrid.GridControl()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.cFNSeq = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTDescription = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNTotal = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNPercent = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFDShipDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemDateFDShipDate = New DevExpress.XtraEditors.Repository.RepositoryItemDateEdit()
        Me.cFTNewNikePOLineItem = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNRowId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTMainMatCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTRawMatCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTItemCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTColorway = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNRawMatQty = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFDRcvDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTRemark = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemMemoEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit()
        Me.cFTStatePOCancel = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCheckFTStatePOCancel = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.RepositoryItemCalcEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit()
        Me.ogbSubOrder = New DevExpress.XtraEditors.GroupControl()
        Me.XtraScrollableControl1 = New DevExpress.XtraEditors.XtraScrollableControl()
        Me.ogc = New DevExpress.XtraGrid.GridControl()
        Me.ogv = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.FTStateCancel = New DevExpress.XtraEditors.CheckEdit()
        Me.FTStateApp = New DevExpress.XtraEditors.CheckEdit()
        Me.FTStateMngApp = New DevExpress.XtraEditors.CheckEdit()
        Me.FTStateFactoryApp = New DevExpress.XtraEditors.CheckEdit()
        Me.FTSubOrderNo_lbl = New DevExpress.XtraEditors.LabelControl()
        CType(Me.ocmdoc, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbheader.SuspendLayout()
        Me.DockPanel1_Container.SuspendLayout()
        CType(Me.FTColorway.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTColorway_None.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysStyleId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysStyleId_None.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTCustomerPO.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysCmpId_None.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysCmpId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTNikePOLineItem.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbmainprocbutton.SuspendLayout()
        CType(Me.FTStatePartialShip.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTStateShortShip.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.otb, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.otb.SuspendLayout()
        Me.otpdetail.SuspendLayout()
        CType(Me.ogdColorSizeBreakdown, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemDateFDShipDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemDateFDShipDate.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemMemoEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckFTStatePOCancel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCalcEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogbSubOrder, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbSubOrder.SuspendLayout()
        Me.XtraScrollableControl1.SuspendLayout()
        CType(Me.ogc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTStateCancel.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTStateApp.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTStateMngApp.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTStateFactoryApp.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ocmdoc
        '
        Me.ocmdoc.Form = Me
        Me.ocmdoc.RootPanels.AddRange(New DevExpress.XtraBars.Docking.DockPanel() {Me.ogbheader})
        Me.ocmdoc.TopZIndexControls.AddRange(New String() {"DevExpress.XtraBars.BarDockControl", "DevExpress.XtraBars.StandaloneBarDockControl", "System.Windows.Forms.StatusBar", "System.Windows.Forms.MenuStrip", "System.Windows.Forms.StatusStrip", "DevExpress.XtraBars.Ribbon.RibbonStatusBar", "DevExpress.XtraBars.Ribbon.RibbonControl"})
        '
        'ogbheader
        '
        Me.ogbheader.Controls.Add(Me.DockPanel1_Container)
        Me.ogbheader.Dock = DevExpress.XtraBars.Docking.DockingStyle.Top
        Me.ogbheader.ID = New System.Guid("6a838d1f-4f6f-4734-9f6d-4c809fcfd587")
        Me.ogbheader.Image = CType(resources.GetObject("ogbheader.Image"), System.Drawing.Image)
        Me.ogbheader.Location = New System.Drawing.Point(0, 0)
        Me.ogbheader.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbheader.Name = "ogbheader"
        Me.ogbheader.Options.AllowDockBottom = False
        Me.ogbheader.Options.AllowDockFill = False
        Me.ogbheader.Options.AllowDockLeft = False
        Me.ogbheader.Options.AllowDockRight = False
        Me.ogbheader.Options.AllowFloating = False
        Me.ogbheader.Options.FloatOnDblClick = False
        Me.ogbheader.Options.ShowCloseButton = False
        Me.ogbheader.Options.ShowMaximizeButton = False
        Me.ogbheader.OriginalSize = New System.Drawing.Size(200, 140)
        Me.ogbheader.Size = New System.Drawing.Size(1711, 140)
        Me.ogbheader.Text = "Criteria"
        '
        'DockPanel1_Container
        '
        Me.DockPanel1_Container.Controls.Add(Me.FTColorway)
        Me.DockPanel1_Container.Controls.Add(Me.FTColorway_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FNHSysStyleId_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FTColorway_None)
        Me.DockPanel1_Container.Controls.Add(Me.FNHSysCmpId_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FNHSysStyleId)
        Me.DockPanel1_Container.Controls.Add(Me.FNHSysStyleId_None)
        Me.DockPanel1_Container.Controls.Add(Me.FTCustomerPO)
        Me.DockPanel1_Container.Controls.Add(Me.FNHSysCmpId_None)
        Me.DockPanel1_Container.Controls.Add(Me.FTCustomerPO_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FNHSysCmpId)
        Me.DockPanel1_Container.Controls.Add(Me.FTNikePOLineItem)
        Me.DockPanel1_Container.Controls.Add(Me.FTNikePOLineItem_lbl)
        Me.DockPanel1_Container.Location = New System.Drawing.Point(3, 32)
        Me.DockPanel1_Container.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.DockPanel1_Container.Name = "DockPanel1_Container"
        Me.DockPanel1_Container.Size = New System.Drawing.Size(1705, 103)
        Me.DockPanel1_Container.TabIndex = 0
        '
        'FTColorway
        '
        Me.FTColorway.Location = New System.Drawing.Point(766, 65)
        Me.FTColorway.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTColorway.Name = "FTColorway"
        Me.FTColorway.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, False, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", "93", Nothing, True)})
        Me.FTColorway.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.FTColorway.Properties.ReadOnly = True
        Me.FTColorway.Properties.Tag = ""
        Me.FTColorway.Size = New System.Drawing.Size(52, 23)
        Me.FTColorway.TabIndex = 517
        Me.FTColorway.Tag = "2|"
        '
        'FTColorway_lbl
        '
        Me.FTColorway_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTColorway_lbl.Appearance.Options.UseForeColor = True
        Me.FTColorway_lbl.Appearance.Options.UseTextOptions = True
        Me.FTColorway_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTColorway_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTColorway_lbl.Location = New System.Drawing.Point(688, 66)
        Me.FTColorway_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTColorway_lbl.Name = "FTColorway_lbl"
        Me.FTColorway_lbl.Size = New System.Drawing.Size(73, 18)
        Me.FTColorway_lbl.TabIndex = 518
        Me.FTColorway_lbl.Tag = "2|"
        Me.FTColorway_lbl.Text = "FTColorway  :"
        '
        'FNHSysStyleId_lbl
        '
        Me.FNHSysStyleId_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysStyleId_lbl.Appearance.Options.UseForeColor = True
        Me.FNHSysStyleId_lbl.Appearance.Options.UseTextOptions = True
        Me.FNHSysStyleId_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysStyleId_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysStyleId_lbl.Location = New System.Drawing.Point(51, 69)
        Me.FNHSysStyleId_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysStyleId_lbl.Name = "FNHSysStyleId_lbl"
        Me.FNHSysStyleId_lbl.Size = New System.Drawing.Size(148, 22)
        Me.FNHSysStyleId_lbl.TabIndex = 439
        Me.FNHSysStyleId_lbl.Tag = "2|"
        Me.FNHSysStyleId_lbl.Text = "FNHSysStyleId :"
        '
        'FTColorway_None
        '
        Me.FTColorway_None.Location = New System.Drawing.Point(825, 64)
        Me.FTColorway_None.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTColorway_None.Name = "FTColorway_None"
        Me.FTColorway_None.Properties.Appearance.BackColor = System.Drawing.Color.LightCyan
        Me.FTColorway_None.Properties.Appearance.Options.UseBackColor = True
        Me.FTColorway_None.Properties.ReadOnly = True
        Me.FTColorway_None.Size = New System.Drawing.Size(45, 23)
        Me.FTColorway_None.TabIndex = 519
        Me.FTColorway_None.Tag = "2|"
        '
        'FNHSysCmpId_lbl
        '
        Me.FNHSysCmpId_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysCmpId_lbl.Appearance.Options.UseForeColor = True
        Me.FNHSysCmpId_lbl.Appearance.Options.UseTextOptions = True
        Me.FNHSysCmpId_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysCmpId_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysCmpId_lbl.Location = New System.Drawing.Point(19, 6)
        Me.FNHSysCmpId_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysCmpId_lbl.Name = "FNHSysCmpId_lbl"
        Me.FNHSysCmpId_lbl.Size = New System.Drawing.Size(177, 21)
        Me.FNHSysCmpId_lbl.TabIndex = 576
        Me.FNHSysCmpId_lbl.Tag = "2|"
        Me.FNHSysCmpId_lbl.Text = "Company :"
        '
        'FNHSysStyleId
        '
        Me.FNHSysStyleId.Location = New System.Drawing.Point(203, 66)
        Me.FNHSysStyleId.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysStyleId.Name = "FNHSysStyleId"
        Me.FNHSysStyleId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, False, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject2, "", "602", Nothing, True)})
        Me.FNHSysStyleId.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.FNHSysStyleId.Properties.ReadOnly = True
        Me.FNHSysStyleId.Properties.Tag = ""
        Me.FNHSysStyleId.Size = New System.Drawing.Size(220, 23)
        Me.FNHSysStyleId.TabIndex = 440
        Me.FNHSysStyleId.Tag = ""
        '
        'FNHSysStyleId_None
        '
        Me.FNHSysStyleId_None.Location = New System.Drawing.Point(430, 66)
        Me.FNHSysStyleId_None.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysStyleId_None.Name = "FNHSysStyleId_None"
        Me.FNHSysStyleId_None.Properties.Appearance.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysStyleId_None.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysStyleId_None.Properties.ReadOnly = True
        Me.FNHSysStyleId_None.Size = New System.Drawing.Size(241, 23)
        Me.FNHSysStyleId_None.TabIndex = 441
        Me.FNHSysStyleId_None.Tag = ""
        '
        'FTCustomerPO
        '
        Me.FTCustomerPO.Location = New System.Drawing.Point(203, 37)
        Me.FTCustomerPO.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTCustomerPO.Name = "FTCustomerPO"
        Me.FTCustomerPO.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject3, "", CType(674, Short), Nothing, True)})
        Me.FTCustomerPO.Properties.ReadOnly = True
        Me.FTCustomerPO.Size = New System.Drawing.Size(220, 23)
        Me.FTCustomerPO.TabIndex = 402
        Me.FTCustomerPO.Tag = "2|"
        '
        'FNHSysCmpId_None
        '
        Me.FNHSysCmpId_None.Location = New System.Drawing.Point(430, 10)
        Me.FNHSysCmpId_None.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysCmpId_None.Name = "FNHSysCmpId_None"
        Me.FNHSysCmpId_None.Properties.Appearance.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysCmpId_None.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysCmpId_None.Properties.ReadOnly = True
        Me.FNHSysCmpId_None.Size = New System.Drawing.Size(1321, 23)
        Me.FNHSysCmpId_None.TabIndex = 578
        Me.FNHSysCmpId_None.Tag = ""
        '
        'FTCustomerPO_lbl
        '
        Me.FTCustomerPO_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTCustomerPO_lbl.Appearance.Options.UseForeColor = True
        Me.FTCustomerPO_lbl.Appearance.Options.UseTextOptions = True
        Me.FTCustomerPO_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTCustomerPO_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTCustomerPO_lbl.Location = New System.Drawing.Point(19, 39)
        Me.FTCustomerPO_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTCustomerPO_lbl.Name = "FTCustomerPO_lbl"
        Me.FTCustomerPO_lbl.Size = New System.Drawing.Size(178, 16)
        Me.FTCustomerPO_lbl.TabIndex = 401
        Me.FTCustomerPO_lbl.Text = "CustomerPO"
        '
        'FNHSysCmpId
        '
        Me.FNHSysCmpId.Location = New System.Drawing.Point(203, 10)
        Me.FNHSysCmpId.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysCmpId.Name = "FNHSysCmpId"
        Me.FNHSysCmpId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject4, "", "11", Nothing, True)})
        Me.FNHSysCmpId.Properties.Tag = ""
        Me.FNHSysCmpId.Size = New System.Drawing.Size(220, 23)
        Me.FNHSysCmpId.TabIndex = 577
        Me.FNHSysCmpId.Tag = ""
        '
        'FTNikePOLineItem
        '
        Me.FTNikePOLineItem.Location = New System.Drawing.Point(615, 37)
        Me.FTNikePOLineItem.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTNikePOLineItem.Name = "FTNikePOLineItem"
        Me.FTNikePOLineItem.Properties.Appearance.BackColor = System.Drawing.Color.LightCyan
        Me.FTNikePOLineItem.Properties.Appearance.Options.UseBackColor = True
        Me.FTNikePOLineItem.Properties.ReadOnly = True
        Me.FTNikePOLineItem.Size = New System.Drawing.Size(76, 23)
        Me.FTNikePOLineItem.TabIndex = 539
        Me.FTNikePOLineItem.Tag = "2|"
        '
        'FTNikePOLineItem_lbl
        '
        Me.FTNikePOLineItem_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTNikePOLineItem_lbl.Appearance.Options.UseForeColor = True
        Me.FTNikePOLineItem_lbl.Appearance.Options.UseTextOptions = True
        Me.FTNikePOLineItem_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTNikePOLineItem_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTNikePOLineItem_lbl.Location = New System.Drawing.Point(430, 36)
        Me.FTNikePOLineItem_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTNikePOLineItem_lbl.Name = "FTNikePOLineItem_lbl"
        Me.FTNikePOLineItem_lbl.Size = New System.Drawing.Size(169, 23)
        Me.FTNikePOLineItem_lbl.TabIndex = 554
        Me.FTNikePOLineItem_lbl.Tag = "2|"
        Me.FTNikePOLineItem_lbl.Text = "FTNikePOLineItem :"
        '
        'ogbmainprocbutton
        '
        Me.ogbmainprocbutton.Controls.Add(Me.ocmremove)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmadd)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmexporttoexcel)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmclear)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmdelete)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmapprovepoline)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmapprove)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmreject)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmSendApprove)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmsavedocument)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmsave)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmexit)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmload)
        Me.ogbmainprocbutton.Location = New System.Drawing.Point(540, 17)
        Me.ogbmainprocbutton.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbmainprocbutton.Name = "ogbmainprocbutton"
        Me.ogbmainprocbutton.Size = New System.Drawing.Size(1100, 95)
        Me.ogbmainprocbutton.TabIndex = 397
        '
        'ocmremove
        '
        Me.ocmremove.Location = New System.Drawing.Point(974, 58)
        Me.ocmremove.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmremove.Name = "ocmremove"
        Me.ocmremove.Size = New System.Drawing.Size(111, 31)
        Me.ocmremove.TabIndex = 122
        Me.ocmremove.TabStop = False
        Me.ocmremove.Tag = "2|"
        Me.ocmremove.Text = "Removed"
        Me.ocmremove.Visible = False
        '
        'ocmadd
        '
        Me.ocmadd.Location = New System.Drawing.Point(878, 41)
        Me.ocmadd.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmadd.Name = "ocmadd"
        Me.ocmadd.Size = New System.Drawing.Size(111, 31)
        Me.ocmadd.TabIndex = 122
        Me.ocmadd.TabStop = False
        Me.ocmadd.Tag = "2|"
        Me.ocmadd.Text = "Add"
        Me.ocmadd.Visible = False
        '
        'ocmexporttoexcel
        '
        Me.ocmexporttoexcel.Location = New System.Drawing.Point(743, 38)
        Me.ocmexporttoexcel.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmexporttoexcel.Name = "ocmexporttoexcel"
        Me.ocmexporttoexcel.Size = New System.Drawing.Size(111, 31)
        Me.ocmexporttoexcel.TabIndex = 122
        Me.ocmexporttoexcel.TabStop = False
        Me.ocmexporttoexcel.Tag = "2|"
        Me.ocmexporttoexcel.Text = "EXPORT EXCEL FILE"
        '
        'ocmclear
        '
        Me.ocmclear.Location = New System.Drawing.Point(699, 6)
        Me.ocmclear.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmclear.Name = "ocmclear"
        Me.ocmclear.Size = New System.Drawing.Size(111, 31)
        Me.ocmclear.TabIndex = 121
        Me.ocmclear.TabStop = False
        Me.ocmclear.Tag = "2|"
        Me.ocmclear.Text = "CLEAR"
        '
        'ocmdelete
        '
        Me.ocmdelete.Location = New System.Drawing.Point(581, 6)
        Me.ocmdelete.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmdelete.Name = "ocmdelete"
        Me.ocmdelete.Size = New System.Drawing.Size(111, 31)
        Me.ocmdelete.TabIndex = 120
        Me.ocmdelete.TabStop = False
        Me.ocmdelete.Tag = "2|"
        Me.ocmdelete.Text = "DELETE"
        Me.ocmdelete.Visible = False
        '
        'ocmapprovepoline
        '
        Me.ocmapprovepoline.Location = New System.Drawing.Point(551, 46)
        Me.ocmapprovepoline.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmapprovepoline.Name = "ocmapprovepoline"
        Me.ocmapprovepoline.Size = New System.Drawing.Size(124, 31)
        Me.ocmapprovepoline.TabIndex = 119
        Me.ocmapprovepoline.TabStop = False
        Me.ocmapprovepoline.Tag = "2|"
        Me.ocmapprovepoline.Text = "Approved"
        '
        'ocmapprove
        '
        Me.ocmapprove.Location = New System.Drawing.Point(370, 47)
        Me.ocmapprove.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmapprove.Name = "ocmapprove"
        Me.ocmapprove.Size = New System.Drawing.Size(124, 31)
        Me.ocmapprove.TabIndex = 119
        Me.ocmapprove.TabStop = False
        Me.ocmapprove.Tag = "2|"
        Me.ocmapprove.Text = "Approved"
        '
        'ocmreject
        '
        Me.ocmreject.Location = New System.Drawing.Point(212, 34)
        Me.ocmreject.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmreject.Name = "ocmreject"
        Me.ocmreject.Size = New System.Drawing.Size(124, 31)
        Me.ocmreject.TabIndex = 119
        Me.ocmreject.TabStop = False
        Me.ocmreject.Tag = "2|"
        Me.ocmreject.Text = "Approved Cancel"
        '
        'ocmSendApprove
        '
        Me.ocmSendApprove.Location = New System.Drawing.Point(30, 43)
        Me.ocmSendApprove.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmSendApprove.Name = "ocmSendApprove"
        Me.ocmSendApprove.Size = New System.Drawing.Size(124, 31)
        Me.ocmSendApprove.TabIndex = 119
        Me.ocmSendApprove.TabStop = False
        Me.ocmSendApprove.Tag = "2|"
        Me.ocmSendApprove.Text = "SEND APPROVE"
        '
        'ocmsavedocument
        '
        Me.ocmsavedocument.Location = New System.Drawing.Point(331, 9)
        Me.ocmsavedocument.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmsavedocument.Name = "ocmsavedocument"
        Me.ocmsavedocument.Size = New System.Drawing.Size(111, 31)
        Me.ocmsavedocument.TabIndex = 97
        Me.ocmsavedocument.TabStop = False
        Me.ocmsavedocument.Tag = "2|"
        Me.ocmsavedocument.Text = "SAVE"
        Me.ocmsavedocument.Visible = False
        '
        'ocmsave
        '
        Me.ocmsave.Location = New System.Drawing.Point(464, 10)
        Me.ocmsave.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmsave.Name = "ocmsave"
        Me.ocmsave.Size = New System.Drawing.Size(111, 31)
        Me.ocmsave.TabIndex = 97
        Me.ocmsave.TabStop = False
        Me.ocmsave.Tag = "2|"
        Me.ocmsave.Text = "SAVE"
        '
        'ocmexit
        '
        Me.ocmexit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmexit.Location = New System.Drawing.Point(915, 9)
        Me.ocmexit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmexit.Name = "ocmexit"
        Me.ocmexit.Size = New System.Drawing.Size(111, 31)
        Me.ocmexit.TabIndex = 96
        Me.ocmexit.TabStop = False
        Me.ocmexit.Tag = "2|"
        Me.ocmexit.Text = "EXIT"
        '
        'ocmload
        '
        Me.ocmload.Location = New System.Drawing.Point(20, 6)
        Me.ocmload.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmload.Name = "ocmload"
        Me.ocmload.Size = New System.Drawing.Size(111, 31)
        Me.ocmload.TabIndex = 93
        Me.ocmload.TabStop = False
        Me.ocmload.Tag = "2|"
        Me.ocmload.Text = "LoadData"
        '
        'FTStatePartialShip
        '
        Me.FTStatePartialShip.EditValue = "0"
        Me.FTStatePartialShip.Location = New System.Drawing.Point(163, 2)
        Me.FTStatePartialShip.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.FTStatePartialShip.Name = "FTStatePartialShip"
        Me.FTStatePartialShip.Properties.AutoHeight = False
        Me.FTStatePartialShip.Properties.Caption = "Partial Shipmet"
        Me.FTStatePartialShip.Properties.ValueChecked = "1"
        Me.FTStatePartialShip.Properties.ValueUnchecked = "0"
        Me.FTStatePartialShip.Size = New System.Drawing.Size(213, 24)
        Me.FTStatePartialShip.TabIndex = 421
        '
        'FTStateShortShip
        '
        Me.FTStateShortShip.EditValue = "0"
        Me.FTStateShortShip.Location = New System.Drawing.Point(10, 0)
        Me.FTStateShortShip.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.FTStateShortShip.Name = "FTStateShortShip"
        Me.FTStateShortShip.Properties.AutoHeight = False
        Me.FTStateShortShip.Properties.Caption = "SHORT Shipmet"
        Me.FTStateShortShip.Properties.ValueChecked = "1"
        Me.FTStateShortShip.Properties.ValueUnchecked = "0"
        Me.FTStateShortShip.Size = New System.Drawing.Size(146, 24)
        Me.FTStateShortShip.TabIndex = 421
        '
        'otb
        '
        Me.otb.Dock = System.Windows.Forms.DockStyle.Fill
        Me.otb.Location = New System.Drawing.Point(0, 424)
        Me.otb.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.otb.Name = "otb"
        Me.otb.SelectedTabPage = Me.otpdetail
        Me.otb.Size = New System.Drawing.Size(1711, 376)
        Me.otb.TabIndex = 394
        Me.otb.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.otpdetail})
        '
        'otpdetail
        '
        Me.otpdetail.Controls.Add(Me.ogdColorSizeBreakdown)
        Me.otpdetail.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.otpdetail.Name = "otpdetail"
        Me.otpdetail.Size = New System.Drawing.Size(1701, 339)
        Me.otpdetail.Text = "Detail"
        '
        'ogdColorSizeBreakdown
        '
        Me.ogdColorSizeBreakdown.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogdColorSizeBreakdown.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogdColorSizeBreakdown.Location = New System.Drawing.Point(0, 0)
        Me.ogdColorSizeBreakdown.MainView = Me.GridView1
        Me.ogdColorSizeBreakdown.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogdColorSizeBreakdown.Name = "ogdColorSizeBreakdown"
        Me.ogdColorSizeBreakdown.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemDateFDShipDate, Me.RepositoryItemCalcEdit1, Me.RepositoryItemCheckFTStatePOCancel, Me.RepositoryItemMemoEdit1})
        Me.ogdColorSizeBreakdown.Size = New System.Drawing.Size(1701, 339)
        Me.ogdColorSizeBreakdown.TabIndex = 1
        Me.ogdColorSizeBreakdown.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1})
        '
        'GridView1
        '
        Me.GridView1.Appearance.EvenRow.BackColor = System.Drawing.Color.LightSteelBlue
        Me.GridView1.Appearance.EvenRow.Options.UseBackColor = True
        Me.GridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.cFNSeq, Me.cFTDescription, Me.cFNTotal, Me.cFNPercent, Me.cFDShipDate, Me.cFTNewNikePOLineItem, Me.cFNRowId, Me.cFTMainMatCode, Me.cFTRawMatCode, Me.cFTItemCode, Me.cFTColorway, Me.cFNRawMatQty, Me.cFDRcvDate, Me.cFTRemark, Me.cFTStatePOCancel})
        Me.GridView1.GridControl = Me.ogdColorSizeBreakdown
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsView.ColumnAutoWidth = False
        Me.GridView1.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView1.OptionsView.ShowGroupPanel = False
        '
        'cFNSeq
        '
        Me.cFNSeq.Caption = "FNSeq"
        Me.cFNSeq.FieldName = "FNSeq"
        Me.cFNSeq.Name = "cFNSeq"
        '
        'cFTDescription
        '
        Me.cFTDescription.Caption = "Description"
        Me.cFTDescription.FieldName = "FTDescription"
        Me.cFTDescription.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
        Me.cFTDescription.Name = "cFTDescription"
        Me.cFTDescription.OptionsColumn.AllowEdit = False
        Me.cFTDescription.OptionsColumn.AllowFocus = False
        Me.cFTDescription.OptionsColumn.ReadOnly = True
        Me.cFTDescription.Visible = True
        Me.cFTDescription.VisibleIndex = 0
        Me.cFTDescription.Width = 222
        '
        'cFNTotal
        '
        Me.cFNTotal.Caption = "Total"
        Me.cFNTotal.DisplayFormat.FormatString = "N0"
        Me.cFNTotal.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cFNTotal.FieldName = "FNTotal"
        Me.cFNTotal.Name = "cFNTotal"
        Me.cFNTotal.OptionsColumn.AllowEdit = False
        Me.cFNTotal.OptionsColumn.AllowMove = False
        Me.cFNTotal.Visible = True
        Me.cFNTotal.VisibleIndex = 1
        Me.cFNTotal.Width = 126
        '
        'cFNPercent
        '
        Me.cFNPercent.Caption = "%"
        Me.cFNPercent.FieldName = "FNPercent"
        Me.cFNPercent.Name = "cFNPercent"
        Me.cFNPercent.OptionsColumn.AllowMove = False
        Me.cFNPercent.Width = 85
        '
        'cFDShipDate
        '
        Me.cFDShipDate.Caption = "FDShipDate"
        Me.cFDShipDate.ColumnEdit = Me.RepositoryItemDateFDShipDate
        Me.cFDShipDate.DisplayFormat.FormatString = "MM/dd/yyyy"
        Me.cFDShipDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.cFDShipDate.FieldName = "FDShipDate"
        Me.cFDShipDate.Name = "cFDShipDate"
        Me.cFDShipDate.Visible = True
        Me.cFDShipDate.VisibleIndex = 2
        Me.cFDShipDate.Width = 119
        '
        'RepositoryItemDateFDShipDate
        '
        Me.RepositoryItemDateFDShipDate.AutoHeight = False
        Me.RepositoryItemDateFDShipDate.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemDateFDShipDate.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemDateFDShipDate.DisplayFormat.FormatString = "MM/dd/yyyy"
        Me.RepositoryItemDateFDShipDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.RepositoryItemDateFDShipDate.EditFormat.FormatString = "MM/dd/yyyy"
        Me.RepositoryItemDateFDShipDate.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.RepositoryItemDateFDShipDate.Mask.EditMask = "g"
        Me.RepositoryItemDateFDShipDate.Name = "RepositoryItemDateFDShipDate"
        '
        'cFTNewNikePOLineItem
        '
        Me.cFTNewNikePOLineItem.Caption = "FTNewNikePOLineItem"
        Me.cFTNewNikePOLineItem.FieldName = "FTNewNikePOLineItem"
        Me.cFTNewNikePOLineItem.Name = "cFTNewNikePOLineItem"
        Me.cFTNewNikePOLineItem.Visible = True
        Me.cFTNewNikePOLineItem.VisibleIndex = 3
        Me.cFTNewNikePOLineItem.Width = 54
        '
        'cFNRowId
        '
        Me.cFNRowId.Caption = "FNRowId"
        Me.cFNRowId.FieldName = "FNRowId"
        Me.cFNRowId.Name = "cFNRowId"
        '
        'cFTMainMatCode
        '
        Me.cFTMainMatCode.Caption = "FTMainMatCode"
        Me.cFTMainMatCode.FieldName = "FTMainMatCode"
        Me.cFTMainMatCode.Name = "cFTMainMatCode"
        Me.cFTMainMatCode.Visible = True
        Me.cFTMainMatCode.VisibleIndex = 4
        Me.cFTMainMatCode.Width = 50
        '
        'cFTRawMatCode
        '
        Me.cFTRawMatCode.Caption = "FTRawMatCode"
        Me.cFTRawMatCode.FieldName = "FTRawMatCode"
        Me.cFTRawMatCode.Name = "cFTRawMatCode"
        Me.cFTRawMatCode.Visible = True
        Me.cFTRawMatCode.VisibleIndex = 5
        Me.cFTRawMatCode.Width = 59
        '
        'cFTItemCode
        '
        Me.cFTItemCode.Caption = "FTItemCode"
        Me.cFTItemCode.FieldName = "FTItemCode"
        Me.cFTItemCode.Name = "cFTItemCode"
        Me.cFTItemCode.Visible = True
        Me.cFTItemCode.VisibleIndex = 6
        Me.cFTItemCode.Width = 74
        '
        'cFTColorway
        '
        Me.cFTColorway.Caption = "FTColorway"
        Me.cFTColorway.FieldName = "FTColorway"
        Me.cFTColorway.Name = "cFTColorway"
        Me.cFTColorway.Visible = True
        Me.cFTColorway.VisibleIndex = 7
        Me.cFTColorway.Width = 92
        '
        'cFNRawMatQty
        '
        Me.cFNRawMatQty.Caption = "FNRawMatQty"
        Me.cFNRawMatQty.DisplayFormat.FormatString = "N5"
        Me.cFNRawMatQty.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cFNRawMatQty.FieldName = "FNRawMatQty"
        Me.cFNRawMatQty.Name = "cFNRawMatQty"
        Me.cFNRawMatQty.Visible = True
        Me.cFNRawMatQty.VisibleIndex = 8
        Me.cFNRawMatQty.Width = 99
        '
        'cFDRcvDate
        '
        Me.cFDRcvDate.Caption = "FDRcvDate"
        Me.cFDRcvDate.ColumnEdit = Me.RepositoryItemDateFDShipDate
        Me.cFDRcvDate.DisplayFormat.FormatString = "MM/dd/yyyy"
        Me.cFDRcvDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.cFDRcvDate.FieldName = "FDRcvDate"
        Me.cFDRcvDate.Name = "cFDRcvDate"
        Me.cFDRcvDate.Visible = True
        Me.cFDRcvDate.VisibleIndex = 9
        Me.cFDRcvDate.Width = 66
        '
        'cFTRemark
        '
        Me.cFTRemark.Caption = "FTRemark"
        Me.cFTRemark.ColumnEdit = Me.RepositoryItemMemoEdit1
        Me.cFTRemark.FieldName = "FTRemark"
        Me.cFTRemark.Name = "cFTRemark"
        Me.cFTRemark.Visible = True
        Me.cFTRemark.VisibleIndex = 10
        Me.cFTRemark.Width = 169
        '
        'RepositoryItemMemoEdit1
        '
        Me.RepositoryItemMemoEdit1.Name = "RepositoryItemMemoEdit1"
        '
        'cFTStatePOCancel
        '
        Me.cFTStatePOCancel.Caption = "FTStatePOCancel"
        Me.cFTStatePOCancel.ColumnEdit = Me.RepositoryItemCheckFTStatePOCancel
        Me.cFTStatePOCancel.FieldName = "FTStatePOCancel"
        Me.cFTStatePOCancel.Name = "cFTStatePOCancel"
        Me.cFTStatePOCancel.Visible = True
        Me.cFTStatePOCancel.VisibleIndex = 11
        Me.cFTStatePOCancel.Width = 72
        '
        'RepositoryItemCheckFTStatePOCancel
        '
        Me.RepositoryItemCheckFTStatePOCancel.AutoHeight = False
        Me.RepositoryItemCheckFTStatePOCancel.Name = "RepositoryItemCheckFTStatePOCancel"
        Me.RepositoryItemCheckFTStatePOCancel.ValueChecked = "1"
        Me.RepositoryItemCheckFTStatePOCancel.ValueUnchecked = "0"
        '
        'RepositoryItemCalcEdit1
        '
        Me.RepositoryItemCalcEdit1.AutoHeight = False
        Me.RepositoryItemCalcEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemCalcEdit1.Name = "RepositoryItemCalcEdit1"
        '
        'ogbSubOrder
        '
        Me.ogbSubOrder.Controls.Add(Me.XtraScrollableControl1)
        Me.ogbSubOrder.Dock = System.Windows.Forms.DockStyle.Top
        Me.ogbSubOrder.Location = New System.Drawing.Point(0, 140)
        Me.ogbSubOrder.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbSubOrder.Name = "ogbSubOrder"
        Me.ogbSubOrder.Size = New System.Drawing.Size(1711, 284)
        Me.ogbSubOrder.TabIndex = 399
        Me.ogbSubOrder.Text = "Sub Order Information"
        '
        'XtraScrollableControl1
        '
        Me.XtraScrollableControl1.Controls.Add(Me.ogc)
        Me.XtraScrollableControl1.Controls.Add(Me.FTStateCancel)
        Me.XtraScrollableControl1.Controls.Add(Me.FTStateApp)
        Me.XtraScrollableControl1.Controls.Add(Me.FTStateMngApp)
        Me.XtraScrollableControl1.Controls.Add(Me.FTStateFactoryApp)
        Me.XtraScrollableControl1.Controls.Add(Me.FTStatePartialShip)
        Me.XtraScrollableControl1.Controls.Add(Me.FTStateShortShip)
        Me.XtraScrollableControl1.Controls.Add(Me.FTSubOrderNo_lbl)
        Me.XtraScrollableControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.XtraScrollableControl1.Location = New System.Drawing.Point(2, 25)
        Me.XtraScrollableControl1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.XtraScrollableControl1.Name = "XtraScrollableControl1"
        Me.XtraScrollableControl1.Size = New System.Drawing.Size(1707, 257)
        Me.XtraScrollableControl1.TabIndex = 497
        '
        'ogc
        '
        Me.ogc.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogc.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.ogc.Location = New System.Drawing.Point(3, 73)
        Me.ogc.MainView = Me.ogv
        Me.ogc.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.ogc.Name = "ogc"
        Me.ogc.Size = New System.Drawing.Size(1696, 181)
        Me.ogc.TabIndex = 509
        Me.ogc.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogv})
        '
        'ogv
        '
        Me.ogv.GridControl = Me.ogc
        Me.ogv.Name = "ogv"
        Me.ogv.OptionsView.ColumnAutoWidth = False
        Me.ogv.OptionsView.ShowGroupPanel = False
        '
        'FTStateCancel
        '
        Me.FTStateCancel.EditValue = "0"
        Me.FTStateCancel.Location = New System.Drawing.Point(617, 34)
        Me.FTStateCancel.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.FTStateCancel.Name = "FTStateCancel"
        Me.FTStateCancel.Properties.AutoHeight = False
        Me.FTStateCancel.Properties.Caption = "FTStateCancel"
        Me.FTStateCancel.Properties.ReadOnly = True
        Me.FTStateCancel.Properties.ValueChecked = "1"
        Me.FTStateCancel.Properties.ValueUnchecked = "0"
        Me.FTStateCancel.Size = New System.Drawing.Size(204, 24)
        Me.FTStateCancel.TabIndex = 421
        '
        'FTStateApp
        '
        Me.FTStateApp.EditValue = "0"
        Me.FTStateApp.Location = New System.Drawing.Point(388, 34)
        Me.FTStateApp.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.FTStateApp.Name = "FTStateApp"
        Me.FTStateApp.Properties.AutoHeight = False
        Me.FTStateApp.Properties.Caption = "FTStateApp"
        Me.FTStateApp.Properties.ReadOnly = True
        Me.FTStateApp.Properties.ValueChecked = "1"
        Me.FTStateApp.Properties.ValueUnchecked = "0"
        Me.FTStateApp.Size = New System.Drawing.Size(175, 24)
        Me.FTStateApp.TabIndex = 421
        '
        'FTStateMngApp
        '
        Me.FTStateMngApp.EditValue = "0"
        Me.FTStateMngApp.Location = New System.Drawing.Point(163, 34)
        Me.FTStateMngApp.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.FTStateMngApp.Name = "FTStateMngApp"
        Me.FTStateMngApp.Properties.AutoHeight = False
        Me.FTStateMngApp.Properties.Caption = "FTStateMngApp"
        Me.FTStateMngApp.Properties.ReadOnly = True
        Me.FTStateMngApp.Properties.ValueChecked = "1"
        Me.FTStateMngApp.Properties.ValueUnchecked = "0"
        Me.FTStateMngApp.Size = New System.Drawing.Size(195, 24)
        Me.FTStateMngApp.TabIndex = 421
        '
        'FTStateFactoryApp
        '
        Me.FTStateFactoryApp.EditValue = "0"
        Me.FTStateFactoryApp.Location = New System.Drawing.Point(10, 34)
        Me.FTStateFactoryApp.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.FTStateFactoryApp.Name = "FTStateFactoryApp"
        Me.FTStateFactoryApp.Properties.AutoHeight = False
        Me.FTStateFactoryApp.Properties.Caption = "FTStateFactoryApp"
        Me.FTStateFactoryApp.Properties.ReadOnly = True
        Me.FTStateFactoryApp.Properties.ValueChecked = "1"
        Me.FTStateFactoryApp.Properties.ValueUnchecked = "0"
        Me.FTStateFactoryApp.Size = New System.Drawing.Size(146, 24)
        Me.FTStateFactoryApp.TabIndex = 421
        '
        'FTSubOrderNo_lbl
        '
        Me.FTSubOrderNo_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTSubOrderNo_lbl.Appearance.Options.UseForeColor = True
        Me.FTSubOrderNo_lbl.Appearance.Options.UseTextOptions = True
        Me.FTSubOrderNo_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTSubOrderNo_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTSubOrderNo_lbl.Location = New System.Drawing.Point(42, 0)
        Me.FTSubOrderNo_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTSubOrderNo_lbl.Name = "FTSubOrderNo_lbl"
        Me.FTSubOrderNo_lbl.Size = New System.Drawing.Size(10, 6)
        Me.FTSubOrderNo_lbl.TabIndex = 508
        Me.FTSubOrderNo_lbl.Tag = "2|"
        Me.FTSubOrderNo_lbl.Text = "FTSubOrderNo :"
        Me.FTSubOrderNo_lbl.Visible = False
        '
        'wPartialShipment
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1711, 800)
        Me.Controls.Add(Me.ogbmainprocbutton)
        Me.Controls.Add(Me.otb)
        Me.Controls.Add(Me.ogbSubOrder)
        Me.Controls.Add(Me.ogbheader)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "wPartialShipment"
        Me.Text = "Partial Shipment"
        CType(Me.ocmdoc, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbheader.ResumeLayout(False)
        Me.DockPanel1_Container.ResumeLayout(False)
        CType(Me.FTColorway.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTColorway_None.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysStyleId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysStyleId_None.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTCustomerPO.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysCmpId_None.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysCmpId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTNikePOLineItem.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbmainprocbutton.ResumeLayout(False)
        CType(Me.FTStatePartialShip.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTStateShortShip.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.otb, System.ComponentModel.ISupportInitialize).EndInit()
        Me.otb.ResumeLayout(False)
        Me.otpdetail.ResumeLayout(False)
        CType(Me.ogdColorSizeBreakdown, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemDateFDShipDate.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemDateFDShipDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemMemoEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckFTStatePOCancel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCalcEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogbSubOrder, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbSubOrder.ResumeLayout(False)
        Me.XtraScrollableControl1.ResumeLayout(False)
        CType(Me.ogc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTStateCancel.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTStateApp.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTStateMngApp.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTStateFactoryApp.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ocmdoc As DevExpress.XtraBars.Docking.DockManager
    Friend WithEvents ogbheader As DevExpress.XtraBars.Docking.DockPanel
    Friend WithEvents DockPanel1_Container As DevExpress.XtraBars.Docking.ControlContainer
    Friend WithEvents otb As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents otpdetail As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents ogbmainprocbutton As DevExpress.XtraEditors.PanelControl
    Friend WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmload As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmsave As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmsavedocument As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FTStatePartialShip As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents FTStateShortShip As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents ogbSubOrder As DevExpress.XtraEditors.GroupControl
    Friend WithEvents XtraScrollableControl1 As DevExpress.XtraEditors.XtraScrollableControl
    Friend WithEvents FTSubOrderNo_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysCmpId_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysCmpId_None As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FNHSysCmpId As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents ogdColorSizeBreakdown As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents FTCustomerPO As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FTCustomerPO_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTNikePOLineItem As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FTNikePOLineItem_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cFTDescription As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNTotal As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNPercent As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ogc As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogv As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents cFNSeq As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFDShipDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemDateFDShipDate As DevExpress.XtraEditors.Repository.RepositoryItemDateEdit
    Friend WithEvents FTStateCancel As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents FTStateApp As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents FTStateMngApp As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents FTStateFactoryApp As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents ocmSendApprove As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmapprovepoline As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmapprove As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmreject As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents cFTNewNikePOLineItem As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ocmclear As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmdelete As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmexporttoexcel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmadd As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents cFNRowId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemCalcEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit
    Friend WithEvents FNHSysStyleId_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysStyleId As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FNHSysStyleId_None As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FTColorway As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FTColorway_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTColorway_None As DevExpress.XtraEditors.TextEdit
    Friend WithEvents ocmremove As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents cFTMainMatCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTRawMatCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTItemCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTColorway As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNRawMatQty As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFDRcvDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTRemark As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTStatePOCancel As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemCheckFTStatePOCancel As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents RepositoryItemMemoEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit
End Class

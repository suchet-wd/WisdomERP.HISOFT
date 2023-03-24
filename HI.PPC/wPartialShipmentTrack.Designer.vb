Imports DevExpress.XtraEditors.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wPartialShipmentTrack
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(wPartialShipmentTrack))
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject4 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject5 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.ocmdoc = New DevExpress.XtraBars.Docking.DockManager(Me.components)
        Me.ogbheader = New DevExpress.XtraBars.Docking.DockPanel()
        Me.DockPanel1_Container = New DevExpress.XtraBars.Docking.ControlContainer()
        Me.FTCustomerPO = New DevExpress.XtraEditors.ButtonEdit()
        Me.ogc = New DevExpress.XtraGrid.GridControl()
        Me.ogv = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.FTCustomerPO_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysStyleId_None = New DevExpress.XtraEditors.TextEdit()
        Me.FTNikePOLineItem = New DevExpress.XtraEditors.TextEdit()
        Me.FNHSysCustId_None = New DevExpress.XtraEditors.TextEdit()
        Me.FTNikePOLineItem_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysStyleId_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysBuyId_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysCustId_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysStyleId = New DevExpress.XtraEditors.ButtonEdit()
        Me.FNHSysBuyId = New DevExpress.XtraEditors.ButtonEdit()
        Me.FNHSysCmpId_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysBuyId_None = New DevExpress.XtraEditors.TextEdit()
        Me.FNHSysCustId = New DevExpress.XtraEditors.ButtonEdit()
        Me.FTEndShipment = New DevExpress.XtraEditors.DateEdit()
        Me.FTEndShipment_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTStartShipment = New DevExpress.XtraEditors.DateEdit()
        Me.FNHSysCmpId = New DevExpress.XtraEditors.ButtonEdit()
        Me.FTStartShipment_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysCmpId_None = New DevExpress.XtraEditors.TextEdit()
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
        Me.ogctrack = New DevExpress.XtraGrid.GridControl()
        Me.ogvtrack = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.FTCmpCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTStyleCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTOrderNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTSubOrderNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFTPORef = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFTNikePOLineItem = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNGrandQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FDShipDateOrginal = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FDShipDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FDNewGacDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ReposFDShipDateTo = New DevExpress.XtraEditors.Repository.RepositoryItemDateEdit()
        Me.cFNHSysCmpId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTPlantCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTShipModeCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTProdTypeNameEN = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTSeasonCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTCountryNameEN = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTStateShort = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCheckFTStateShort = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.cFTStatePartial = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCheckFTStatePartial = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.cFTStateFactoryApp = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCheckFTStateFactoryApp = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.cFTFactoryAppBy = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFDFactoryAppDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTFactoryAppTime = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTStateMngApp = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTMngAppBy = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FDMngAppDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTMngAppTime = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTStateApp = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTStateAppBy = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FDAppDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTAppTime = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTStateCancel = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTStateCancelBy = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FDCancelDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTCancelTime = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTSelect = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemoFTSelect = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.RepositoryItemFTSelect = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.ogbmainprocbutton = New DevExpress.XtraEditors.PanelControl()
        Me.ocmclear = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmexporttoexcel = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmsavedocument = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmsave = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmload = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.ocmdoc, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbheader.SuspendLayout()
        Me.DockPanel1_Container.SuspendLayout()
        CType(Me.FTCustomerPO.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysStyleId_None.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTNikePOLineItem.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysCustId_None.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysStyleId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysBuyId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysBuyId_None.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysCustId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTEndShipment.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTEndShipment.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTStartShipment.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTStartShipment.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysCmpId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysCmpId_None.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.otb, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.otb.SuspendLayout()
        Me.otpdetail.SuspendLayout()
        CType(Me.ogdColorSizeBreakdown, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemDateFDShipDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemDateFDShipDate.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogctrack, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvtrack, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ReposFDShipDateTo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ReposFDShipDateTo.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckFTStateShort, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckFTStatePartial, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckFTStateFactoryApp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemoFTSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemFTSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbmainprocbutton.SuspendLayout()
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
        Me.ogbheader.OriginalSize = New System.Drawing.Size(200, 162)
        Me.ogbheader.Size = New System.Drawing.Size(1322, 162)
        Me.ogbheader.Text = "Criteria"
        '
        'DockPanel1_Container
        '
        Me.DockPanel1_Container.Controls.Add(Me.FTCustomerPO)
        Me.DockPanel1_Container.Controls.Add(Me.ogc)
        Me.DockPanel1_Container.Controls.Add(Me.FTCustomerPO_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FNHSysStyleId_None)
        Me.DockPanel1_Container.Controls.Add(Me.FTNikePOLineItem)
        Me.DockPanel1_Container.Controls.Add(Me.FNHSysCustId_None)
        Me.DockPanel1_Container.Controls.Add(Me.FTNikePOLineItem_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FNHSysStyleId_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FNHSysBuyId_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FNHSysCustId_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FNHSysStyleId)
        Me.DockPanel1_Container.Controls.Add(Me.FNHSysBuyId)
        Me.DockPanel1_Container.Controls.Add(Me.FNHSysCmpId_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FNHSysBuyId_None)
        Me.DockPanel1_Container.Controls.Add(Me.FNHSysCustId)
        Me.DockPanel1_Container.Controls.Add(Me.FTEndShipment)
        Me.DockPanel1_Container.Controls.Add(Me.FTEndShipment_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FTStartShipment)
        Me.DockPanel1_Container.Controls.Add(Me.FNHSysCmpId)
        Me.DockPanel1_Container.Controls.Add(Me.FTStartShipment_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FNHSysCmpId_None)
        Me.DockPanel1_Container.Location = New System.Drawing.Point(3, 29)
        Me.DockPanel1_Container.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.DockPanel1_Container.Name = "DockPanel1_Container"
        Me.DockPanel1_Container.Size = New System.Drawing.Size(1316, 128)
        Me.DockPanel1_Container.TabIndex = 0
        '
        'FTCustomerPO
        '
        Me.FTCustomerPO.Location = New System.Drawing.Point(194, 89)
        Me.FTCustomerPO.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTCustomerPO.Name = "FTCustomerPO"
        Me.FTCustomerPO.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", CType(674, Short), Nothing, True)})
        Me.FTCustomerPO.Properties.ReadOnly = True
        Me.FTCustomerPO.Size = New System.Drawing.Size(152, 23)
        Me.FTCustomerPO.TabIndex = 556
        Me.FTCustomerPO.Tag = "2|"
        '
        'ogc
        '
        Me.ogc.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogc.Location = New System.Drawing.Point(652, 117)
        Me.ogc.MainView = Me.ogv
        Me.ogc.Name = "ogc"
        Me.ogc.Size = New System.Drawing.Size(632, 23)
        Me.ogc.TabIndex = 511
        Me.ogc.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogv})
        Me.ogc.Visible = False
        '
        'ogv
        '
        Me.ogv.GridControl = Me.ogc
        Me.ogv.Name = "ogv"
        Me.ogv.OptionsView.ColumnAutoWidth = False
        Me.ogv.OptionsView.ShowGroupPanel = False
        '
        'FTCustomerPO_lbl
        '
        Me.FTCustomerPO_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTCustomerPO_lbl.Appearance.Options.UseForeColor = True
        Me.FTCustomerPO_lbl.Appearance.Options.UseTextOptions = True
        Me.FTCustomerPO_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTCustomerPO_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTCustomerPO_lbl.Location = New System.Drawing.Point(10, 92)
        Me.FTCustomerPO_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTCustomerPO_lbl.Name = "FTCustomerPO_lbl"
        Me.FTCustomerPO_lbl.Size = New System.Drawing.Size(178, 16)
        Me.FTCustomerPO_lbl.TabIndex = 555
        Me.FTCustomerPO_lbl.Text = "CustomerPO"
        '
        'FNHSysStyleId_None
        '
        Me.FNHSysStyleId_None.Location = New System.Drawing.Point(973, 29)
        Me.FNHSysStyleId_None.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysStyleId_None.Name = "FNHSysStyleId_None"
        Me.FNHSysStyleId_None.Properties.Appearance.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysStyleId_None.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysStyleId_None.Properties.ReadOnly = True
        Me.FNHSysStyleId_None.Size = New System.Drawing.Size(276, 23)
        Me.FNHSysStyleId_None.TabIndex = 406
        Me.FNHSysStyleId_None.Tag = "2|"
        '
        'FTNikePOLineItem
        '
        Me.FTNikePOLineItem.Location = New System.Drawing.Point(531, 89)
        Me.FTNikePOLineItem.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTNikePOLineItem.Name = "FTNikePOLineItem"
        Me.FTNikePOLineItem.Properties.Appearance.BackColor = System.Drawing.Color.LightCyan
        Me.FTNikePOLineItem.Properties.Appearance.Options.UseBackColor = True
        Me.FTNikePOLineItem.Properties.ReadOnly = True
        Me.FTNikePOLineItem.Size = New System.Drawing.Size(76, 23)
        Me.FTNikePOLineItem.TabIndex = 557
        Me.FTNikePOLineItem.Tag = "2|"
        '
        'FNHSysCustId_None
        '
        Me.FNHSysCustId_None.Location = New System.Drawing.Point(973, 2)
        Me.FNHSysCustId_None.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysCustId_None.Name = "FNHSysCustId_None"
        Me.FNHSysCustId_None.Properties.Appearance.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysCustId_None.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysCustId_None.Properties.ReadOnly = True
        Me.FNHSysCustId_None.Size = New System.Drawing.Size(276, 23)
        Me.FNHSysCustId_None.TabIndex = 401
        Me.FNHSysCustId_None.Tag = "2|"
        '
        'FTNikePOLineItem_lbl
        '
        Me.FTNikePOLineItem_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTNikePOLineItem_lbl.Appearance.Options.UseForeColor = True
        Me.FTNikePOLineItem_lbl.Appearance.Options.UseTextOptions = True
        Me.FTNikePOLineItem_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTNikePOLineItem_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTNikePOLineItem_lbl.Location = New System.Drawing.Point(346, 88)
        Me.FTNikePOLineItem_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTNikePOLineItem_lbl.Name = "FTNikePOLineItem_lbl"
        Me.FTNikePOLineItem_lbl.Size = New System.Drawing.Size(169, 23)
        Me.FTNikePOLineItem_lbl.TabIndex = 558
        Me.FTNikePOLineItem_lbl.Tag = "2|"
        Me.FTNikePOLineItem_lbl.Text = "FTNikePOLineItem :"
        '
        'FNHSysStyleId_lbl
        '
        Me.FNHSysStyleId_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FNHSysStyleId_lbl.Appearance.Options.UseForeColor = True
        Me.FNHSysStyleId_lbl.Appearance.Options.UseTextOptions = True
        Me.FNHSysStyleId_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysStyleId_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysStyleId_lbl.Location = New System.Drawing.Point(629, 29)
        Me.FNHSysStyleId_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysStyleId_lbl.Name = "FNHSysStyleId_lbl"
        Me.FNHSysStyleId_lbl.Size = New System.Drawing.Size(182, 25)
        Me.FNHSysStyleId_lbl.TabIndex = 402
        Me.FNHSysStyleId_lbl.Tag = "2|"
        Me.FNHSysStyleId_lbl.Text = "Style No :"
        '
        'FNHSysBuyId_lbl
        '
        Me.FNHSysBuyId_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FNHSysBuyId_lbl.Appearance.Options.UseForeColor = True
        Me.FNHSysBuyId_lbl.Appearance.Options.UseTextOptions = True
        Me.FNHSysBuyId_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysBuyId_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysBuyId_lbl.Location = New System.Drawing.Point(5, 31)
        Me.FNHSysBuyId_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysBuyId_lbl.Name = "FNHSysBuyId_lbl"
        Me.FNHSysBuyId_lbl.Size = New System.Drawing.Size(182, 25)
        Me.FNHSysBuyId_lbl.TabIndex = 403
        Me.FNHSysBuyId_lbl.Tag = "2|"
        Me.FNHSysBuyId_lbl.Text = "Buy :"
        '
        'FNHSysCustId_lbl
        '
        Me.FNHSysCustId_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FNHSysCustId_lbl.Appearance.Options.UseForeColor = True
        Me.FNHSysCustId_lbl.Appearance.Options.UseTextOptions = True
        Me.FNHSysCustId_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysCustId_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysCustId_lbl.Location = New System.Drawing.Point(629, 2)
        Me.FNHSysCustId_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysCustId_lbl.Name = "FNHSysCustId_lbl"
        Me.FNHSysCustId_lbl.Size = New System.Drawing.Size(182, 25)
        Me.FNHSysCustId_lbl.TabIndex = 399
        Me.FNHSysCustId_lbl.Tag = "2|"
        Me.FNHSysCustId_lbl.Text = "Customer :"
        '
        'FNHSysStyleId
        '
        Me.FNHSysStyleId.Location = New System.Drawing.Point(818, 29)
        Me.FNHSysStyleId.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysStyleId.Name = "FNHSysStyleId"
        Me.FNHSysStyleId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject2, "", "89", Nothing, True)})
        Me.FNHSysStyleId.Properties.Tag = ""
        Me.FNHSysStyleId.Size = New System.Drawing.Size(152, 23)
        Me.FNHSysStyleId.TabIndex = 3
        Me.FNHSysStyleId.Tag = "2|"
        '
        'FNHSysBuyId
        '
        Me.FNHSysBuyId.Location = New System.Drawing.Point(194, 31)
        Me.FNHSysBuyId.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysBuyId.Name = "FNHSysBuyId"
        Me.FNHSysBuyId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject3, "", "113", Nothing, True)})
        Me.FNHSysBuyId.Properties.Tag = ""
        Me.FNHSysBuyId.Size = New System.Drawing.Size(152, 23)
        Me.FNHSysBuyId.TabIndex = 2
        Me.FNHSysBuyId.Tag = "2|"
        '
        'FNHSysCmpId_lbl
        '
        Me.FNHSysCmpId_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FNHSysCmpId_lbl.Appearance.Options.UseForeColor = True
        Me.FNHSysCmpId_lbl.Appearance.Options.UseTextOptions = True
        Me.FNHSysCmpId_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysCmpId_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysCmpId_lbl.Location = New System.Drawing.Point(5, 4)
        Me.FNHSysCmpId_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysCmpId_lbl.Name = "FNHSysCmpId_lbl"
        Me.FNHSysCmpId_lbl.Size = New System.Drawing.Size(182, 25)
        Me.FNHSysCmpId_lbl.TabIndex = 399
        Me.FNHSysCmpId_lbl.Tag = "2|"
        Me.FNHSysCmpId_lbl.Text = "Company :"
        '
        'FNHSysBuyId_None
        '
        Me.FNHSysBuyId_None.Location = New System.Drawing.Point(348, 31)
        Me.FNHSysBuyId_None.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysBuyId_None.Name = "FNHSysBuyId_None"
        Me.FNHSysBuyId_None.Properties.Appearance.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysBuyId_None.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysBuyId_None.Properties.ReadOnly = True
        Me.FNHSysBuyId_None.Size = New System.Drawing.Size(259, 23)
        Me.FNHSysBuyId_None.TabIndex = 407
        Me.FNHSysBuyId_None.Tag = "2|"
        '
        'FNHSysCustId
        '
        Me.FNHSysCustId.Location = New System.Drawing.Point(818, 2)
        Me.FNHSysCustId.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysCustId.Name = "FNHSysCustId"
        Me.FNHSysCustId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject4, "", "83", Nothing, True)})
        Me.FNHSysCustId.Properties.Tag = ""
        Me.FNHSysCustId.Size = New System.Drawing.Size(152, 23)
        Me.FNHSysCustId.TabIndex = 1
        Me.FNHSysCustId.Tag = "2|"
        '
        'FTEndShipment
        '
        Me.FTEndShipment.EditValue = Nothing
        Me.FTEndShipment.EnterMoveNextControl = True
        Me.FTEndShipment.Location = New System.Drawing.Point(818, 61)
        Me.FTEndShipment.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTEndShipment.Name = "FTEndShipment"
        Me.FTEndShipment.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTEndShipment.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FTEndShipment.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.FTEndShipment.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTEndShipment.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTEndShipment.Properties.NullDate = ""
        Me.FTEndShipment.Size = New System.Drawing.Size(152, 23)
        Me.FTEndShipment.TabIndex = 7
        Me.FTEndShipment.Tag = "2|"
        '
        'FTEndShipment_lbl
        '
        Me.FTEndShipment_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTEndShipment_lbl.Appearance.Options.UseForeColor = True
        Me.FTEndShipment_lbl.Appearance.Options.UseTextOptions = True
        Me.FTEndShipment_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTEndShipment_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTEndShipment_lbl.Location = New System.Drawing.Point(652, 60)
        Me.FTEndShipment_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTEndShipment_lbl.Name = "FTEndShipment_lbl"
        Me.FTEndShipment_lbl.Size = New System.Drawing.Size(163, 23)
        Me.FTEndShipment_lbl.TabIndex = 416
        Me.FTEndShipment_lbl.Tag = "2|"
        Me.FTEndShipment_lbl.Text = "End Shipment:"
        '
        'FTStartShipment
        '
        Me.FTStartShipment.EditValue = Nothing
        Me.FTStartShipment.EnterMoveNextControl = True
        Me.FTStartShipment.Location = New System.Drawing.Point(194, 61)
        Me.FTStartShipment.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTStartShipment.Name = "FTStartShipment"
        Me.FTStartShipment.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTStartShipment.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FTStartShipment.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.FTStartShipment.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTStartShipment.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTStartShipment.Properties.NullDate = ""
        Me.FTStartShipment.Size = New System.Drawing.Size(152, 23)
        Me.FTStartShipment.TabIndex = 6
        Me.FTStartShipment.Tag = "2|"
        '
        'FNHSysCmpId
        '
        Me.FNHSysCmpId.Location = New System.Drawing.Point(194, 4)
        Me.FNHSysCmpId.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysCmpId.Name = "FNHSysCmpId"
        Me.FNHSysCmpId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject5, "", "11", Nothing, True)})
        Me.FNHSysCmpId.Properties.Tag = ""
        Me.FNHSysCmpId.Size = New System.Drawing.Size(152, 23)
        Me.FNHSysCmpId.TabIndex = 0
        Me.FNHSysCmpId.Tag = "2|"
        '
        'FTStartShipment_lbl
        '
        Me.FTStartShipment_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTStartShipment_lbl.Appearance.Options.UseForeColor = True
        Me.FTStartShipment_lbl.Appearance.Options.UseTextOptions = True
        Me.FTStartShipment_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTStartShipment_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTStartShipment_lbl.Location = New System.Drawing.Point(25, 60)
        Me.FTStartShipment_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTStartShipment_lbl.Name = "FTStartShipment_lbl"
        Me.FTStartShipment_lbl.Size = New System.Drawing.Size(162, 23)
        Me.FTStartShipment_lbl.TabIndex = 414
        Me.FTStartShipment_lbl.Tag = "2|"
        Me.FTStartShipment_lbl.Text = "Start Shipment:"
        '
        'FNHSysCmpId_None
        '
        Me.FNHSysCmpId_None.Location = New System.Drawing.Point(348, 4)
        Me.FNHSysCmpId_None.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysCmpId_None.Name = "FNHSysCmpId_None"
        Me.FNHSysCmpId_None.Properties.Appearance.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysCmpId_None.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysCmpId_None.Properties.ReadOnly = True
        Me.FNHSysCmpId_None.Size = New System.Drawing.Size(259, 23)
        Me.FNHSysCmpId_None.TabIndex = 401
        Me.FNHSysCmpId_None.Tag = "2|"
        '
        'otb
        '
        Me.otb.Dock = System.Windows.Forms.DockStyle.Fill
        Me.otb.Location = New System.Drawing.Point(0, 162)
        Me.otb.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.otb.Name = "otb"
        Me.otb.SelectedTabPage = Me.otpdetail
        Me.otb.Size = New System.Drawing.Size(1322, 631)
        Me.otb.TabIndex = 394
        Me.otb.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.otpdetail})
        '
        'otpdetail
        '
        Me.otpdetail.Controls.Add(Me.ogdColorSizeBreakdown)
        Me.otpdetail.Controls.Add(Me.ogctrack)
        Me.otpdetail.Name = "otpdetail"
        Me.otpdetail.Size = New System.Drawing.Size(1312, 594)
        Me.otpdetail.Text = "Detail"
        '
        'ogdColorSizeBreakdown
        '
        Me.ogdColorSizeBreakdown.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogdColorSizeBreakdown.Location = New System.Drawing.Point(189, -59)
        Me.ogdColorSizeBreakdown.MainView = Me.GridView1
        Me.ogdColorSizeBreakdown.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogdColorSizeBreakdown.Name = "ogdColorSizeBreakdown"
        Me.ogdColorSizeBreakdown.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemDateFDShipDate})
        Me.ogdColorSizeBreakdown.Size = New System.Drawing.Size(330, 24)
        Me.ogdColorSizeBreakdown.TabIndex = 510
        Me.ogdColorSizeBreakdown.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1})
        Me.ogdColorSizeBreakdown.Visible = False
        '
        'GridView1
        '
        Me.GridView1.Appearance.EvenRow.BackColor = System.Drawing.Color.LightSteelBlue
        Me.GridView1.Appearance.EvenRow.Options.UseBackColor = True
        Me.GridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.cFNSeq, Me.cFTDescription, Me.cFNTotal, Me.cFNPercent, Me.cFDShipDate, Me.cFTNewNikePOLineItem})
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
        Me.cFTDescription.Visible = True
        Me.cFTDescription.VisibleIndex = 0
        Me.cFTDescription.Width = 221
        '
        'cFNTotal
        '
        Me.cFNTotal.Caption = "Total"
        Me.cFNTotal.DisplayFormat.FormatString = "N0"
        Me.cFNTotal.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cFNTotal.FieldName = "FNTotal"
        Me.cFNTotal.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right
        Me.cFNTotal.Name = "cFNTotal"
        Me.cFNTotal.OptionsColumn.AllowMove = False
        Me.cFNTotal.Visible = True
        Me.cFNTotal.VisibleIndex = 1
        Me.cFNTotal.Width = 126
        '
        'cFNPercent
        '
        Me.cFNPercent.Caption = "%"
        Me.cFNPercent.FieldName = "FNPercent"
        Me.cFNPercent.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right
        Me.cFNPercent.Name = "cFNPercent"
        Me.cFNPercent.OptionsColumn.AllowMove = False
        Me.cFNPercent.Width = 85
        '
        'cFDShipDate
        '
        Me.cFDShipDate.Caption = "FDShipDate"
        Me.cFDShipDate.ColumnEdit = Me.RepositoryItemDateFDShipDate
        Me.cFDShipDate.DisplayFormat.FormatString = "d"
        Me.cFDShipDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.cFDShipDate.FieldName = "FDShipDate"
        Me.cFDShipDate.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right
        Me.cFDShipDate.Name = "cFDShipDate"
        Me.cFDShipDate.Visible = True
        Me.cFDShipDate.VisibleIndex = 2
        Me.cFDShipDate.Width = 169
        '
        'RepositoryItemDateFDShipDate
        '
        Me.RepositoryItemDateFDShipDate.AutoHeight = False
        Me.RepositoryItemDateFDShipDate.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemDateFDShipDate.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemDateFDShipDate.Name = "RepositoryItemDateFDShipDate"
        '
        'cFTNewNikePOLineItem
        '
        Me.cFTNewNikePOLineItem.Caption = "FTNewNikePOLineItem"
        Me.cFTNewNikePOLineItem.FieldName = "FTNewNikePOLineItem"
        Me.cFTNewNikePOLineItem.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right
        Me.cFTNewNikePOLineItem.Name = "cFTNewNikePOLineItem"
        Me.cFTNewNikePOLineItem.Visible = True
        Me.cFTNewNikePOLineItem.VisibleIndex = 3
        Me.cFTNewNikePOLineItem.Width = 149
        '
        'ogctrack
        '
        Me.ogctrack.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogctrack.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogctrack.Location = New System.Drawing.Point(0, 0)
        Me.ogctrack.MainView = Me.ogvtrack
        Me.ogctrack.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogctrack.Name = "ogctrack"
        Me.ogctrack.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.ReposFDShipDateTo, Me.RepositoryItemFTSelect, Me.RepositoryItemCheckFTStateShort, Me.RepositoryItemCheckFTStatePartial, Me.RepositoryItemCheckFTStateFactoryApp, Me.RepositoryItemoFTSelect})
        Me.ogctrack.Size = New System.Drawing.Size(1312, 594)
        Me.ogctrack.TabIndex = 1
        Me.ogctrack.TabStop = False
        Me.ogctrack.Tag = "2|"
        Me.ogctrack.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvtrack})
        '
        'ogvtrack
        '
        Me.ogvtrack.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.FTCmpCode, Me.FTStyleCode, Me.FTOrderNo, Me.FTSubOrderNo, Me.CFTPORef, Me.CFTNikePOLineItem, Me.cFNGrandQuantity, Me.FDShipDateOrginal, Me.FDShipDate, Me.FDNewGacDate, Me.cFNHSysCmpId, Me.cFTPlantCode, Me.cFTShipModeCode, Me.cFTProdTypeNameEN, Me.FTSeasonCode, Me.cFTCountryNameEN, Me.cFTStateShort, Me.cFTStatePartial, Me.cFTStateFactoryApp, Me.cFTFactoryAppBy, Me.cFDFactoryAppDate, Me.cFTFactoryAppTime, Me.cFTStateMngApp, Me.cFTMngAppBy, Me.FDMngAppDate, Me.FTMngAppTime, Me.FTStateApp, Me.FTStateAppBy, Me.FDAppDate, Me.FTAppTime, Me.FTStateCancel, Me.FTStateCancelBy, Me.FDCancelDate, Me.FTCancelTime, Me.cFTSelect})
        Me.ogvtrack.CustomizationFormBounds = New System.Drawing.Rectangle(1326, 384, 210, 194)
        Me.ogvtrack.GridControl = Me.ogctrack
        Me.ogvtrack.Name = "ogvtrack"
        Me.ogvtrack.OptionsCustomization.AllowQuickHideColumns = False
        Me.ogvtrack.OptionsSelection.MultiSelect = True
        Me.ogvtrack.OptionsView.ColumnAutoWidth = False
        Me.ogvtrack.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.ogvtrack.OptionsView.ShowGroupPanel = False
        Me.ogvtrack.Tag = "2|"
        '
        'FTCmpCode
        '
        Me.FTCmpCode.Caption = "FTCmpCode"
        Me.FTCmpCode.FieldName = "FTCmpCode"
        Me.FTCmpCode.Name = "FTCmpCode"
        Me.FTCmpCode.OptionsColumn.AllowEdit = False
        Me.FTCmpCode.OptionsColumn.ReadOnly = True
        Me.FTCmpCode.Visible = True
        Me.FTCmpCode.VisibleIndex = 1
        Me.FTCmpCode.Width = 100
        '
        'FTStyleCode
        '
        Me.FTStyleCode.Caption = "FTStyleCode"
        Me.FTStyleCode.FieldName = "FTStyleCode"
        Me.FTStyleCode.Name = "FTStyleCode"
        Me.FTStyleCode.OptionsColumn.AllowEdit = False
        Me.FTStyleCode.OptionsColumn.ReadOnly = True
        Me.FTStyleCode.Visible = True
        Me.FTStyleCode.VisibleIndex = 2
        Me.FTStyleCode.Width = 100
        '
        'FTOrderNo
        '
        Me.FTOrderNo.Caption = "FTOrderNo"
        Me.FTOrderNo.FieldName = "FTOrderNo"
        Me.FTOrderNo.Name = "FTOrderNo"
        Me.FTOrderNo.OptionsColumn.AllowEdit = False
        Me.FTOrderNo.OptionsColumn.ReadOnly = True
        Me.FTOrderNo.Visible = True
        Me.FTOrderNo.VisibleIndex = 3
        Me.FTOrderNo.Width = 120
        '
        'FTSubOrderNo
        '
        Me.FTSubOrderNo.Caption = "FTSubOrderNo"
        Me.FTSubOrderNo.FieldName = "FTSubOrderNo"
        Me.FTSubOrderNo.Name = "FTSubOrderNo"
        Me.FTSubOrderNo.OptionsColumn.AllowEdit = False
        Me.FTSubOrderNo.OptionsColumn.ReadOnly = True
        Me.FTSubOrderNo.Visible = True
        Me.FTSubOrderNo.VisibleIndex = 4
        Me.FTSubOrderNo.Width = 114
        '
        'CFTPORef
        '
        Me.CFTPORef.Caption = "Customer PO"
        Me.CFTPORef.FieldName = "FTPORef"
        Me.CFTPORef.Name = "CFTPORef"
        Me.CFTPORef.OptionsColumn.AllowEdit = False
        Me.CFTPORef.OptionsColumn.ReadOnly = True
        Me.CFTPORef.Visible = True
        Me.CFTPORef.VisibleIndex = 6
        Me.CFTPORef.Width = 100
        '
        'CFTNikePOLineItem
        '
        Me.CFTNikePOLineItem.Caption = "PO Line."
        Me.CFTNikePOLineItem.FieldName = "FTNikePOLineItem"
        Me.CFTNikePOLineItem.Name = "CFTNikePOLineItem"
        Me.CFTNikePOLineItem.OptionsColumn.AllowEdit = False
        Me.CFTNikePOLineItem.OptionsColumn.ReadOnly = True
        Me.CFTNikePOLineItem.Visible = True
        Me.CFTNikePOLineItem.VisibleIndex = 5
        '
        'cFNGrandQuantity
        '
        Me.cFNGrandQuantity.Caption = "FNGrandQuantity"
        Me.cFNGrandQuantity.DisplayFormat.FormatString = "{0:n0}"
        Me.cFNGrandQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cFNGrandQuantity.FieldName = "FNGrandQuantity"
        Me.cFNGrandQuantity.Name = "cFNGrandQuantity"
        Me.cFNGrandQuantity.OptionsColumn.AllowEdit = False
        Me.cFNGrandQuantity.OptionsColumn.ReadOnly = True
        Me.cFNGrandQuantity.Visible = True
        Me.cFNGrandQuantity.VisibleIndex = 7
        '
        'FDShipDateOrginal
        '
        Me.FDShipDateOrginal.Caption = "Shipment Date ORG"
        Me.FDShipDateOrginal.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.FDShipDateOrginal.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.FDShipDateOrginal.FieldName = "FDShipDateOrginal"
        Me.FDShipDateOrginal.Name = "FDShipDateOrginal"
        Me.FDShipDateOrginal.OptionsColumn.AllowEdit = False
        Me.FDShipDateOrginal.OptionsColumn.ReadOnly = True
        Me.FDShipDateOrginal.Visible = True
        Me.FDShipDateOrginal.VisibleIndex = 8
        Me.FDShipDateOrginal.Width = 100
        '
        'FDShipDate
        '
        Me.FDShipDate.Caption = "FDShipDate"
        Me.FDShipDate.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.FDShipDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.FDShipDate.FieldName = "FDShipDate"
        Me.FDShipDate.Name = "FDShipDate"
        Me.FDShipDate.OptionsColumn.AllowEdit = False
        Me.FDShipDate.OptionsColumn.ReadOnly = True
        Me.FDShipDate.Visible = True
        Me.FDShipDate.VisibleIndex = 9
        '
        'FDNewGacDate
        '
        Me.FDNewGacDate.Caption = "FDNewGacDate"
        Me.FDNewGacDate.ColumnEdit = Me.ReposFDShipDateTo
        Me.FDNewGacDate.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.FDNewGacDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.FDNewGacDate.FieldName = "FDNewGacDate"
        Me.FDNewGacDate.Name = "FDNewGacDate"
        Me.FDNewGacDate.OptionsColumn.AllowEdit = False
        Me.FDNewGacDate.OptionsColumn.ReadOnly = True
        Me.FDNewGacDate.Visible = True
        Me.FDNewGacDate.VisibleIndex = 10
        Me.FDNewGacDate.Width = 129
        '
        'ReposFDShipDateTo
        '
        Me.ReposFDShipDateTo.AutoHeight = False
        Me.ReposFDShipDateTo.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ReposFDShipDateTo.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ReposFDShipDateTo.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.ReposFDShipDateTo.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.ReposFDShipDateTo.EditFormat.FormatString = "dd/MM/yyyy"
        Me.ReposFDShipDateTo.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.ReposFDShipDateTo.Name = "ReposFDShipDateTo"
        '
        'cFNHSysCmpId
        '
        Me.cFNHSysCmpId.Caption = "FNHSysCmpId"
        Me.cFNHSysCmpId.FieldName = "FNHSysCmpId"
        Me.cFNHSysCmpId.Name = "cFNHSysCmpId"
        Me.cFNHSysCmpId.OptionsColumn.AllowEdit = False
        Me.cFNHSysCmpId.OptionsColumn.ReadOnly = True
        '
        'cFTPlantCode
        '
        Me.cFTPlantCode.Caption = "FTPlantCode"
        Me.cFTPlantCode.FieldName = "FTPlantCode"
        Me.cFTPlantCode.Name = "cFTPlantCode"
        Me.cFTPlantCode.OptionsColumn.AllowEdit = False
        Me.cFTPlantCode.OptionsColumn.ReadOnly = True
        Me.cFTPlantCode.Visible = True
        Me.cFTPlantCode.VisibleIndex = 11
        '
        'cFTShipModeCode
        '
        Me.cFTShipModeCode.Caption = "FTShipModeCode"
        Me.cFTShipModeCode.FieldName = "FTShipModeCode"
        Me.cFTShipModeCode.Name = "cFTShipModeCode"
        Me.cFTShipModeCode.OptionsColumn.AllowEdit = False
        Me.cFTShipModeCode.OptionsColumn.ReadOnly = True
        Me.cFTShipModeCode.Visible = True
        Me.cFTShipModeCode.VisibleIndex = 12
        '
        'cFTProdTypeNameEN
        '
        Me.cFTProdTypeNameEN.Caption = "FTProdTypeNameEN"
        Me.cFTProdTypeNameEN.FieldName = "FTProdTypeNameEN"
        Me.cFTProdTypeNameEN.Name = "cFTProdTypeNameEN"
        Me.cFTProdTypeNameEN.OptionsColumn.AllowEdit = False
        Me.cFTProdTypeNameEN.OptionsColumn.ReadOnly = True
        Me.cFTProdTypeNameEN.Visible = True
        Me.cFTProdTypeNameEN.VisibleIndex = 13
        '
        'FTSeasonCode
        '
        Me.FTSeasonCode.Caption = "FTSeasonCode"
        Me.FTSeasonCode.FieldName = "FTSeasonCode"
        Me.FTSeasonCode.Name = "FTSeasonCode"
        Me.FTSeasonCode.OptionsColumn.AllowEdit = False
        Me.FTSeasonCode.OptionsColumn.ReadOnly = True
        Me.FTSeasonCode.Visible = True
        Me.FTSeasonCode.VisibleIndex = 14
        '
        'cFTCountryNameEN
        '
        Me.cFTCountryNameEN.Caption = "FTCountryNameEN"
        Me.cFTCountryNameEN.FieldName = "FTCountryNameEN"
        Me.cFTCountryNameEN.Name = "cFTCountryNameEN"
        Me.cFTCountryNameEN.OptionsColumn.AllowEdit = False
        Me.cFTCountryNameEN.OptionsColumn.ReadOnly = True
        Me.cFTCountryNameEN.Visible = True
        Me.cFTCountryNameEN.VisibleIndex = 15
        '
        'cFTStateShort
        '
        Me.cFTStateShort.Caption = "FTStateShort"
        Me.cFTStateShort.ColumnEdit = Me.RepositoryItemCheckFTStateShort
        Me.cFTStateShort.FieldName = "FTStateShort"
        Me.cFTStateShort.Name = "cFTStateShort"
        Me.cFTStateShort.OptionsColumn.AllowEdit = False
        Me.cFTStateShort.OptionsColumn.ReadOnly = True
        Me.cFTStateShort.Visible = True
        Me.cFTStateShort.VisibleIndex = 16
        '
        'RepositoryItemCheckFTStateShort
        '
        Me.RepositoryItemCheckFTStateShort.AutoHeight = False
        Me.RepositoryItemCheckFTStateShort.Name = "RepositoryItemCheckFTStateShort"
        Me.RepositoryItemCheckFTStateShort.ValueChecked = "1"
        Me.RepositoryItemCheckFTStateShort.ValueUnchecked = "0"
        '
        'cFTStatePartial
        '
        Me.cFTStatePartial.Caption = "FTStatePartial"
        Me.cFTStatePartial.ColumnEdit = Me.RepositoryItemCheckFTStatePartial
        Me.cFTStatePartial.FieldName = "FTStatePartial"
        Me.cFTStatePartial.Name = "cFTStatePartial"
        Me.cFTStatePartial.OptionsColumn.AllowEdit = False
        Me.cFTStatePartial.OptionsColumn.ReadOnly = True
        Me.cFTStatePartial.Visible = True
        Me.cFTStatePartial.VisibleIndex = 17
        '
        'RepositoryItemCheckFTStatePartial
        '
        Me.RepositoryItemCheckFTStatePartial.AutoHeight = False
        Me.RepositoryItemCheckFTStatePartial.Name = "RepositoryItemCheckFTStatePartial"
        Me.RepositoryItemCheckFTStatePartial.ValueChecked = "1"
        Me.RepositoryItemCheckFTStatePartial.ValueUnchecked = "0"
        '
        'cFTStateFactoryApp
        '
        Me.cFTStateFactoryApp.Caption = "FTStateFactoryApp"
        Me.cFTStateFactoryApp.ColumnEdit = Me.RepositoryItemCheckFTStateFactoryApp
        Me.cFTStateFactoryApp.FieldName = "FTStateFactoryApp"
        Me.cFTStateFactoryApp.Name = "cFTStateFactoryApp"
        Me.cFTStateFactoryApp.OptionsColumn.AllowEdit = False
        Me.cFTStateFactoryApp.OptionsColumn.ReadOnly = True
        Me.cFTStateFactoryApp.Visible = True
        Me.cFTStateFactoryApp.VisibleIndex = 18
        '
        'RepositoryItemCheckFTStateFactoryApp
        '
        Me.RepositoryItemCheckFTStateFactoryApp.AutoHeight = False
        Me.RepositoryItemCheckFTStateFactoryApp.Name = "RepositoryItemCheckFTStateFactoryApp"
        Me.RepositoryItemCheckFTStateFactoryApp.ValueChecked = "1"
        Me.RepositoryItemCheckFTStateFactoryApp.ValueUnchecked = "0"
        '
        'cFTFactoryAppBy
        '
        Me.cFTFactoryAppBy.Caption = "FTFactoryAppBy"
        Me.cFTFactoryAppBy.FieldName = "FTFactoryAppBy"
        Me.cFTFactoryAppBy.Name = "cFTFactoryAppBy"
        Me.cFTFactoryAppBy.OptionsColumn.AllowEdit = False
        Me.cFTFactoryAppBy.OptionsColumn.ReadOnly = True
        Me.cFTFactoryAppBy.Visible = True
        Me.cFTFactoryAppBy.VisibleIndex = 19
        '
        'cFDFactoryAppDate
        '
        Me.cFDFactoryAppDate.Caption = "FDFactoryAppDate"
        Me.cFDFactoryAppDate.FieldName = "FDFactoryAppDate"
        Me.cFDFactoryAppDate.Name = "cFDFactoryAppDate"
        Me.cFDFactoryAppDate.OptionsColumn.AllowEdit = False
        Me.cFDFactoryAppDate.OptionsColumn.ReadOnly = True
        Me.cFDFactoryAppDate.Visible = True
        Me.cFDFactoryAppDate.VisibleIndex = 20
        '
        'cFTFactoryAppTime
        '
        Me.cFTFactoryAppTime.Caption = "FTFactoryAppTime"
        Me.cFTFactoryAppTime.FieldName = "FTFactoryAppTime"
        Me.cFTFactoryAppTime.Name = "cFTFactoryAppTime"
        Me.cFTFactoryAppTime.OptionsColumn.AllowEdit = False
        Me.cFTFactoryAppTime.OptionsColumn.ReadOnly = True
        Me.cFTFactoryAppTime.Visible = True
        Me.cFTFactoryAppTime.VisibleIndex = 21
        '
        'cFTStateMngApp
        '
        Me.cFTStateMngApp.Caption = "FTStateMngApp"
        Me.cFTStateMngApp.ColumnEdit = Me.RepositoryItemCheckFTStateFactoryApp
        Me.cFTStateMngApp.FieldName = "FTStateMngApp"
        Me.cFTStateMngApp.Name = "cFTStateMngApp"
        Me.cFTStateMngApp.OptionsColumn.AllowEdit = False
        Me.cFTStateMngApp.OptionsColumn.ReadOnly = True
        Me.cFTStateMngApp.Visible = True
        Me.cFTStateMngApp.VisibleIndex = 22
        '
        'cFTMngAppBy
        '
        Me.cFTMngAppBy.Caption = "FTMngAppBy"
        Me.cFTMngAppBy.FieldName = "FTMngAppBy"
        Me.cFTMngAppBy.Name = "cFTMngAppBy"
        Me.cFTMngAppBy.OptionsColumn.AllowEdit = False
        Me.cFTMngAppBy.OptionsColumn.ReadOnly = True
        Me.cFTMngAppBy.Visible = True
        Me.cFTMngAppBy.VisibleIndex = 23
        '
        'FDMngAppDate
        '
        Me.FDMngAppDate.Caption = "FDMngAppDate"
        Me.FDMngAppDate.FieldName = "FDMngAppDate"
        Me.FDMngAppDate.Name = "FDMngAppDate"
        Me.FDMngAppDate.OptionsColumn.AllowEdit = False
        Me.FDMngAppDate.OptionsColumn.ReadOnly = True
        Me.FDMngAppDate.Visible = True
        Me.FDMngAppDate.VisibleIndex = 24
        '
        'FTMngAppTime
        '
        Me.FTMngAppTime.Caption = "FTMngAppTime"
        Me.FTMngAppTime.FieldName = "FTMngAppTime"
        Me.FTMngAppTime.Name = "FTMngAppTime"
        Me.FTMngAppTime.OptionsColumn.AllowEdit = False
        Me.FTMngAppTime.OptionsColumn.ReadOnly = True
        Me.FTMngAppTime.Visible = True
        Me.FTMngAppTime.VisibleIndex = 25
        '
        'FTStateApp
        '
        Me.FTStateApp.Caption = "FTStateApp"
        Me.FTStateApp.ColumnEdit = Me.RepositoryItemCheckFTStateFactoryApp
        Me.FTStateApp.FieldName = "FTStateApp"
        Me.FTStateApp.Name = "FTStateApp"
        Me.FTStateApp.OptionsColumn.AllowEdit = False
        Me.FTStateApp.OptionsColumn.ReadOnly = True
        Me.FTStateApp.Visible = True
        Me.FTStateApp.VisibleIndex = 26
        '
        'FTStateAppBy
        '
        Me.FTStateAppBy.Caption = "FTStateAppBy"
        Me.FTStateAppBy.FieldName = "FTStateAppBy"
        Me.FTStateAppBy.Name = "FTStateAppBy"
        Me.FTStateAppBy.OptionsColumn.AllowEdit = False
        Me.FTStateAppBy.OptionsColumn.ReadOnly = True
        Me.FTStateAppBy.Visible = True
        Me.FTStateAppBy.VisibleIndex = 27
        '
        'FDAppDate
        '
        Me.FDAppDate.Caption = "FDAppDate"
        Me.FDAppDate.FieldName = "FDAppDate"
        Me.FDAppDate.Name = "FDAppDate"
        Me.FDAppDate.OptionsColumn.AllowEdit = False
        Me.FDAppDate.OptionsColumn.ReadOnly = True
        Me.FDAppDate.Visible = True
        Me.FDAppDate.VisibleIndex = 28
        '
        'FTAppTime
        '
        Me.FTAppTime.Caption = "FTAppTime"
        Me.FTAppTime.FieldName = "FTAppTime"
        Me.FTAppTime.Name = "FTAppTime"
        Me.FTAppTime.OptionsColumn.AllowEdit = False
        Me.FTAppTime.OptionsColumn.ReadOnly = True
        Me.FTAppTime.Visible = True
        Me.FTAppTime.VisibleIndex = 29
        '
        'FTStateCancel
        '
        Me.FTStateCancel.Caption = "FTStateCancel"
        Me.FTStateCancel.ColumnEdit = Me.RepositoryItemCheckFTStateFactoryApp
        Me.FTStateCancel.FieldName = "FTStateCancel"
        Me.FTStateCancel.Name = "FTStateCancel"
        Me.FTStateCancel.OptionsColumn.AllowEdit = False
        Me.FTStateCancel.OptionsColumn.ReadOnly = True
        Me.FTStateCancel.Visible = True
        Me.FTStateCancel.VisibleIndex = 30
        '
        'FTStateCancelBy
        '
        Me.FTStateCancelBy.Caption = "FTStateCancelBy"
        Me.FTStateCancelBy.FieldName = "FTStateCancelBy"
        Me.FTStateCancelBy.Name = "FTStateCancelBy"
        Me.FTStateCancelBy.OptionsColumn.AllowEdit = False
        Me.FTStateCancelBy.OptionsColumn.ReadOnly = True
        Me.FTStateCancelBy.Visible = True
        Me.FTStateCancelBy.VisibleIndex = 31
        '
        'FDCancelDate
        '
        Me.FDCancelDate.Caption = "FDCancelDate"
        Me.FDCancelDate.FieldName = "FDCancelDate"
        Me.FDCancelDate.Name = "FDCancelDate"
        Me.FDCancelDate.OptionsColumn.AllowEdit = False
        Me.FDCancelDate.OptionsColumn.ReadOnly = True
        Me.FDCancelDate.Visible = True
        Me.FDCancelDate.VisibleIndex = 32
        '
        'FTCancelTime
        '
        Me.FTCancelTime.Caption = "FTCancelTime"
        Me.FTCancelTime.FieldName = "FTCancelTime"
        Me.FTCancelTime.Name = "FTCancelTime"
        Me.FTCancelTime.OptionsColumn.AllowEdit = False
        Me.FTCancelTime.OptionsColumn.ReadOnly = True
        Me.FTCancelTime.Visible = True
        Me.FTCancelTime.VisibleIndex = 33
        '
        'cFTSelect
        '
        Me.cFTSelect.Caption = "#"
        Me.cFTSelect.ColumnEdit = Me.RepositoryItemoFTSelect
        Me.cFTSelect.FieldName = "FTSelect"
        Me.cFTSelect.Name = "cFTSelect"
        Me.cFTSelect.Visible = True
        Me.cFTSelect.VisibleIndex = 0
        Me.cFTSelect.Width = 47
        '
        'RepositoryItemoFTSelect
        '
        Me.RepositoryItemoFTSelect.AutoHeight = False
        Me.RepositoryItemoFTSelect.Name = "RepositoryItemoFTSelect"
        Me.RepositoryItemoFTSelect.ValueChecked = "1"
        Me.RepositoryItemoFTSelect.ValueUnchecked = "0"
        '
        'RepositoryItemFTSelect
        '
        Me.RepositoryItemFTSelect.AutoHeight = False
        Me.RepositoryItemFTSelect.Name = "RepositoryItemFTSelect"
        Me.RepositoryItemFTSelect.ValueChecked = "1"
        Me.RepositoryItemFTSelect.ValueUnchecked = "0"
        '
        'ogbmainprocbutton
        '
        Me.ogbmainprocbutton.Controls.Add(Me.ocmclear)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmexporttoexcel)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmsavedocument)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmsave)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmexit)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmload)
        Me.ogbmainprocbutton.Location = New System.Drawing.Point(139, 330)
        Me.ogbmainprocbutton.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbmainprocbutton.Name = "ogbmainprocbutton"
        Me.ogbmainprocbutton.Size = New System.Drawing.Size(1027, 140)
        Me.ogbmainprocbutton.TabIndex = 397
        '
        'ocmclear
        '
        Me.ocmclear.Location = New System.Drawing.Point(771, 79)
        Me.ocmclear.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmclear.Name = "ocmclear"
        Me.ocmclear.Size = New System.Drawing.Size(111, 31)
        Me.ocmclear.TabIndex = 124
        Me.ocmclear.TabStop = False
        Me.ocmclear.Tag = "2|"
        Me.ocmclear.Text = "CLEAR"
        '
        'ocmexporttoexcel
        '
        Me.ocmexporttoexcel.Location = New System.Drawing.Point(167, 79)
        Me.ocmexporttoexcel.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmexporttoexcel.Name = "ocmexporttoexcel"
        Me.ocmexporttoexcel.Size = New System.Drawing.Size(111, 31)
        Me.ocmexporttoexcel.TabIndex = 123
        Me.ocmexporttoexcel.TabStop = False
        Me.ocmexporttoexcel.Tag = "2|"
        Me.ocmexporttoexcel.Text = "EXPORT EXCEL FILE"
        '
        'ocmsavedocument
        '
        Me.ocmsavedocument.Location = New System.Drawing.Point(581, 40)
        Me.ocmsavedocument.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmsavedocument.Name = "ocmsavedocument"
        Me.ocmsavedocument.Size = New System.Drawing.Size(111, 31)
        Me.ocmsavedocument.TabIndex = 97
        Me.ocmsavedocument.TabStop = False
        Me.ocmsavedocument.Tag = "2|"
        Me.ocmsavedocument.Text = "SAVE"
        '
        'ocmsave
        '
        Me.ocmsave.Location = New System.Drawing.Point(443, 40)
        Me.ocmsave.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmsave.Name = "ocmsave"
        Me.ocmsave.Size = New System.Drawing.Size(111, 31)
        Me.ocmsave.TabIndex = 97
        Me.ocmsave.TabStop = False
        Me.ocmsave.Tag = "2|"
        Me.ocmsave.Text = "SAVE"
        Me.ocmsave.Visible = False
        '
        'ocmexit
        '
        Me.ocmexit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmexit.Location = New System.Drawing.Point(842, 9)
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
        'wPartialShipmentTrack
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1322, 793)
        Me.Controls.Add(Me.ogbmainprocbutton)
        Me.Controls.Add(Me.otb)
        Me.Controls.Add(Me.ogbheader)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "wPartialShipmentTrack"
        Me.Text = "Partial Ship / Short Tracking"
        CType(Me.ocmdoc, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbheader.ResumeLayout(False)
        Me.DockPanel1_Container.ResumeLayout(False)
        CType(Me.FTCustomerPO.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysStyleId_None.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTNikePOLineItem.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysCustId_None.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysStyleId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysBuyId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysBuyId_None.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysCustId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTEndShipment.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTEndShipment.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTStartShipment.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTStartShipment.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysCmpId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysCmpId_None.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.otb, System.ComponentModel.ISupportInitialize).EndInit()
        Me.otb.ResumeLayout(False)
        Me.otpdetail.ResumeLayout(False)
        CType(Me.ogdColorSizeBreakdown, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemDateFDShipDate.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemDateFDShipDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogctrack, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvtrack, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ReposFDShipDateTo.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ReposFDShipDateTo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckFTStateShort, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckFTStatePartial, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckFTStateFactoryApp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemoFTSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemFTSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbmainprocbutton.ResumeLayout(False)
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
    Friend WithEvents FNHSysCmpId_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTEndShipment As DevExpress.XtraEditors.DateEdit
    Friend WithEvents FTEndShipment_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTStartShipment As DevExpress.XtraEditors.DateEdit
    Friend WithEvents FNHSysCmpId As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FTStartShipment_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysCmpId_None As DevExpress.XtraEditors.TextEdit
    Friend WithEvents ogctrack As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvtrack As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents FTOrderNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTCmpCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTStyleCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FDShipDateOrginal As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNHSysCustId_None As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FNHSysCustId_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysCustId As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FNHSysStyleId_None As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FNHSysStyleId_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysBuyId_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysStyleId As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FNHSysBuyId As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FNHSysBuyId_None As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FTSubOrderNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNGrandQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FDShipDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ReposFDShipDateTo As DevExpress.XtraEditors.Repository.RepositoryItemDateEdit
    Friend WithEvents ocmsave As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents CFTPORef As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFTNikePOLineItem As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FDNewGacDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ocmsavedocument As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents RepositoryItemFTSelect As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents cFNHSysCmpId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTPlantCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTShipModeCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTProdTypeNameEN As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTSeasonCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTCountryNameEN As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTStateShort As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTStatePartial As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTStateFactoryApp As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTFactoryAppBy As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFDFactoryAppDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTFactoryAppTime As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTStateMngApp As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemCheckFTStateShort As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents RepositoryItemCheckFTStatePartial As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents RepositoryItemCheckFTStateFactoryApp As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents cFTMngAppBy As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FDMngAppDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTMngAppTime As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTStateApp As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTStateAppBy As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FDAppDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTAppTime As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTStateCancel As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTStateCancelBy As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FDCancelDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTCancelTime As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTCustomerPO As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FTCustomerPO_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTNikePOLineItem As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FTNikePOLineItem_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ocmexporttoexcel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ogdColorSizeBreakdown As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents cFNSeq As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTDescription As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNTotal As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNPercent As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFDShipDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemDateFDShipDate As DevExpress.XtraEditors.Repository.RepositoryItemDateEdit
    Friend WithEvents cFTNewNikePOLineItem As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ogc As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogv As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents cFTSelect As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemoFTSelect As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents ocmclear As DevExpress.XtraEditors.SimpleButton
End Class

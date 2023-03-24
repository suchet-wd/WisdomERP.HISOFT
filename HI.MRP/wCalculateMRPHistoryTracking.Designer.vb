<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wCalculateMRPHistoryTracking
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(wCalculateMRPHistoryTracking))
        Dim EditorButtonImageOptions1 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject4 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim EditorButtonImageOptions2 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim SerializableAppearanceObject5 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject6 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject7 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject8 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim EditorButtonImageOptions3 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim SerializableAppearanceObject9 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject10 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject11 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject12 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.ogbheader = New DevExpress.XtraBars.Docking.DockPanel()
        Me.DockPanel1_Container = New DevExpress.XtraBars.Docking.ControlContainer()
        Me.FTOrderNo_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNListDocumentTrackPIData_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTOrderNo = New DevExpress.XtraEditors.ButtonEdit()
        Me.FNHSysSeasonId_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysSeasonId = New DevExpress.XtraEditors.ButtonEdit()
        Me.FNListDocumentTrackPIData = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.FTEndPurchaseDate = New DevExpress.XtraEditors.DateEdit()
        Me.FNHSysStyleId_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTEndPurchaseDate_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysStyleId = New DevExpress.XtraEditors.ButtonEdit()
        Me.FTStartPurchaseDate = New DevExpress.XtraEditors.DateEdit()
        Me.FTStartPurchaseDate_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.ogbdetail = New DevExpress.XtraEditors.GroupControl()
        Me.ogbmainprocbutton = New DevExpress.XtraEditors.PanelControl()
        Me.ocmsavelayout = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmclear = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmload = New DevExpress.XtraEditors.SimpleButton()
        Me.ogdtime = New DevExpress.XtraGrid.GridControl()
        Me.ogvtime = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.xFTStyleCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.xFTSeasonCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.xFTOrderNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.xFTSubOrderNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.xFDShipDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.xFDOGacDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.xFTMainMatCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.xFTRawMatColorCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.xFTRawMatColorNameEN = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.xFTRawMatColorNameTH = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.xFTRawMatSizeCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.xFNUsedQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.xFNUsedPlusQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.xFNPRQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.xFTUserLogin = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.xFTCalDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.xFTCalTime = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.xFTCalTypeName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.xFTFilterItem = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.oDockManager = New DevExpress.XtraBars.Docking.DockManager(Me.components)
        Me.ogbheader.SuspendLayout()
        Me.DockPanel1_Container.SuspendLayout()
        CType(Me.FTOrderNo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysSeasonId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNListDocumentTrackPIData.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTEndPurchaseDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTEndPurchaseDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysStyleId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTStartPurchaseDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTStartPurchaseDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogbdetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbdetail.SuspendLayout()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbmainprocbutton.SuspendLayout()
        CType(Me.ogdtime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvtime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.oDockManager, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.ogbheader.ImageOptions.Image = CType(resources.GetObject("ogbheader.ImageOptions.Image"), System.Drawing.Image)
        Me.ogbheader.Location = New System.Drawing.Point(0, 0)
        Me.ogbheader.Name = "ogbheader"
        Me.ogbheader.Options.AllowDockBottom = False
        Me.ogbheader.Options.AllowDockFill = False
        Me.ogbheader.Options.FloatOnDblClick = False
        Me.ogbheader.Options.ShowCloseButton = False
        Me.ogbheader.Options.ShowMaximizeButton = False
        Me.ogbheader.OriginalSize = New System.Drawing.Size(1318, 142)
        Me.ogbheader.Size = New System.Drawing.Size(1318, 142)
        Me.ogbheader.TabStop = False
        Me.ogbheader.Text = "Criteria"
        '
        'DockPanel1_Container
        '
        Me.DockPanel1_Container.Controls.Add(Me.FTOrderNo_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FNListDocumentTrackPIData_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FTOrderNo)
        Me.DockPanel1_Container.Controls.Add(Me.FNHSysSeasonId_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FNHSysSeasonId)
        Me.DockPanel1_Container.Controls.Add(Me.FNListDocumentTrackPIData)
        Me.DockPanel1_Container.Controls.Add(Me.FTEndPurchaseDate)
        Me.DockPanel1_Container.Controls.Add(Me.FNHSysStyleId_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FTEndPurchaseDate_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FNHSysStyleId)
        Me.DockPanel1_Container.Controls.Add(Me.FTStartPurchaseDate)
        Me.DockPanel1_Container.Controls.Add(Me.FTStartPurchaseDate_lbl)
        Me.DockPanel1_Container.Location = New System.Drawing.Point(3, 26)
        Me.DockPanel1_Container.Name = "DockPanel1_Container"
        Me.DockPanel1_Container.Size = New System.Drawing.Size(1312, 112)
        Me.DockPanel1_Container.TabIndex = 0
        '
        'FTOrderNo_lbl
        '
        Me.FTOrderNo_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTOrderNo_lbl.Appearance.Options.UseForeColor = True
        Me.FTOrderNo_lbl.Appearance.Options.UseTextOptions = True
        Me.FTOrderNo_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTOrderNo_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTOrderNo_lbl.Location = New System.Drawing.Point(57, 84)
        Me.FTOrderNo_lbl.Name = "FTOrderNo_lbl"
        Me.FTOrderNo_lbl.Size = New System.Drawing.Size(99, 13)
        Me.FTOrderNo_lbl.TabIndex = 8
        Me.FTOrderNo_lbl.Text = "OrderNo."
        '
        'FNListDocumentTrackPIData_lbl
        '
        Me.FNListDocumentTrackPIData_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FNListDocumentTrackPIData_lbl.Appearance.Options.UseForeColor = True
        Me.FNListDocumentTrackPIData_lbl.Appearance.Options.UseTextOptions = True
        Me.FNListDocumentTrackPIData_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNListDocumentTrackPIData_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNListDocumentTrackPIData_lbl.Location = New System.Drawing.Point(8, 7)
        Me.FNListDocumentTrackPIData_lbl.Name = "FNListDocumentTrackPIData_lbl"
        Me.FNListDocumentTrackPIData_lbl.Size = New System.Drawing.Size(149, 19)
        Me.FNListDocumentTrackPIData_lbl.TabIndex = 265
        Me.FNListDocumentTrackPIData_lbl.Tag = "2|"
        Me.FNListDocumentTrackPIData_lbl.Text = "Data :"
        '
        'FTOrderNo
        '
        Me.FTOrderNo.EditValue = " "
        Me.FTOrderNo.Location = New System.Drawing.Point(158, 81)
        Me.FTOrderNo.Name = "FTOrderNo"
        Me.FTOrderNo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, EditorButtonImageOptions1, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, SerializableAppearanceObject2, SerializableAppearanceObject3, SerializableAppearanceObject4, "", "121", Nothing, DevExpress.Utils.ToolTipAnchor.[Default])})
        Me.FTOrderNo.Size = New System.Drawing.Size(131, 20)
        Me.FTOrderNo.TabIndex = 9
        Me.FTOrderNo.Tag = "2|"
        '
        'FNHSysSeasonId_lbl
        '
        Me.FNHSysSeasonId_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FNHSysSeasonId_lbl.Appearance.Options.UseForeColor = True
        Me.FNHSysSeasonId_lbl.Appearance.Options.UseTextOptions = True
        Me.FNHSysSeasonId_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysSeasonId_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysSeasonId_lbl.Location = New System.Drawing.Point(398, 55)
        Me.FNHSysSeasonId_lbl.Name = "FNHSysSeasonId_lbl"
        Me.FNHSysSeasonId_lbl.Size = New System.Drawing.Size(156, 20)
        Me.FNHSysSeasonId_lbl.TabIndex = 20
        Me.FNHSysSeasonId_lbl.Tag = "2|"
        Me.FNHSysSeasonId_lbl.Text = "Season :"
        '
        'FNHSysSeasonId
        '
        Me.FNHSysSeasonId.Location = New System.Drawing.Point(556, 56)
        Me.FNHSysSeasonId.Name = "FNHSysSeasonId"
        Me.FNHSysSeasonId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, EditorButtonImageOptions2, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject5, SerializableAppearanceObject6, SerializableAppearanceObject7, SerializableAppearanceObject8, "", "94", Nothing, DevExpress.Utils.ToolTipAnchor.[Default])})
        Me.FNHSysSeasonId.Properties.Tag = ""
        Me.FNHSysSeasonId.Size = New System.Drawing.Size(130, 20)
        Me.FNHSysSeasonId.TabIndex = 21
        Me.FNHSysSeasonId.Tag = "2|"
        '
        'FNListDocumentTrackPIData
        '
        Me.FNListDocumentTrackPIData.EditValue = ""
        Me.FNListDocumentTrackPIData.EnterMoveNextControl = True
        Me.FNListDocumentTrackPIData.Location = New System.Drawing.Point(158, 7)
        Me.FNListDocumentTrackPIData.Name = "FNListDocumentTrackPIData"
        Me.FNListDocumentTrackPIData.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FNListDocumentTrackPIData.Properties.Appearance.Options.UseBackColor = True
        Me.FNListDocumentTrackPIData.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FNListDocumentTrackPIData.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FNListDocumentTrackPIData.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FNListDocumentTrackPIData.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FNListDocumentTrackPIData.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNListDocumentTrackPIData.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNListDocumentTrackPIData.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNListDocumentTrackPIData.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNListDocumentTrackPIData.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNListDocumentTrackPIData.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNListDocumentTrackPIData.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNListDocumentTrackPIData.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNListDocumentTrackPIData.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FNListDocumentTrackPIData.Properties.Tag = "FNListDocumentTrackPIData"
        Me.FNListDocumentTrackPIData.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.FNListDocumentTrackPIData.Size = New System.Drawing.Size(396, 20)
        Me.FNListDocumentTrackPIData.TabIndex = 284
        Me.FNListDocumentTrackPIData.Tag = "9|"
        '
        'FTEndPurchaseDate
        '
        Me.FTEndPurchaseDate.EditValue = Nothing
        Me.FTEndPurchaseDate.EnterMoveNextControl = True
        Me.FTEndPurchaseDate.Location = New System.Drawing.Point(556, 31)
        Me.FTEndPurchaseDate.Name = "FTEndPurchaseDate"
        Me.FTEndPurchaseDate.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTEndPurchaseDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FTEndPurchaseDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.FTEndPurchaseDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTEndPurchaseDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTEndPurchaseDate.Properties.NullDate = ""
        Me.FTEndPurchaseDate.Size = New System.Drawing.Size(130, 20)
        Me.FTEndPurchaseDate.TabIndex = 274
        Me.FTEndPurchaseDate.Tag = "2|"
        '
        'FNHSysStyleId_lbl
        '
        Me.FNHSysStyleId_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FNHSysStyleId_lbl.Appearance.Options.UseForeColor = True
        Me.FNHSysStyleId_lbl.Appearance.Options.UseTextOptions = True
        Me.FNHSysStyleId_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysStyleId_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysStyleId_lbl.Location = New System.Drawing.Point(26, 57)
        Me.FNHSysStyleId_lbl.Name = "FNHSysStyleId_lbl"
        Me.FNHSysStyleId_lbl.Size = New System.Drawing.Size(128, 20)
        Me.FNHSysStyleId_lbl.TabIndex = 17
        Me.FNHSysStyleId_lbl.Tag = "2|"
        Me.FNHSysStyleId_lbl.Text = "Style No :"
        '
        'FTEndPurchaseDate_lbl
        '
        Me.FTEndPurchaseDate_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTEndPurchaseDate_lbl.Appearance.Options.UseForeColor = True
        Me.FTEndPurchaseDate_lbl.Appearance.Options.UseTextOptions = True
        Me.FTEndPurchaseDate_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTEndPurchaseDate_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTEndPurchaseDate_lbl.Location = New System.Drawing.Point(373, 30)
        Me.FTEndPurchaseDate_lbl.Name = "FTEndPurchaseDate_lbl"
        Me.FTEndPurchaseDate_lbl.Size = New System.Drawing.Size(181, 19)
        Me.FTEndPurchaseDate_lbl.TabIndex = 275
        Me.FTEndPurchaseDate_lbl.Tag = "2|"
        Me.FTEndPurchaseDate_lbl.Text = "End Calculate Date :"
        '
        'FNHSysStyleId
        '
        Me.FNHSysStyleId.Location = New System.Drawing.Point(159, 56)
        Me.FNHSysStyleId.Name = "FNHSysStyleId"
        Me.FNHSysStyleId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, EditorButtonImageOptions3, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject9, SerializableAppearanceObject10, SerializableAppearanceObject11, SerializableAppearanceObject12, "", "89", Nothing, DevExpress.Utils.ToolTipAnchor.[Default])})
        Me.FNHSysStyleId.Properties.Tag = ""
        Me.FNHSysStyleId.Size = New System.Drawing.Size(130, 20)
        Me.FNHSysStyleId.TabIndex = 18
        Me.FNHSysStyleId.Tag = "2|"
        '
        'FTStartPurchaseDate
        '
        Me.FTStartPurchaseDate.EditValue = Nothing
        Me.FTStartPurchaseDate.EnterMoveNextControl = True
        Me.FTStartPurchaseDate.Location = New System.Drawing.Point(159, 31)
        Me.FTStartPurchaseDate.Name = "FTStartPurchaseDate"
        Me.FTStartPurchaseDate.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTStartPurchaseDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FTStartPurchaseDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.FTStartPurchaseDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTStartPurchaseDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTStartPurchaseDate.Properties.NullDate = ""
        Me.FTStartPurchaseDate.Size = New System.Drawing.Size(130, 20)
        Me.FTStartPurchaseDate.TabIndex = 272
        Me.FTStartPurchaseDate.Tag = "2|"
        '
        'FTStartPurchaseDate_lbl
        '
        Me.FTStartPurchaseDate_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTStartPurchaseDate_lbl.Appearance.Options.UseForeColor = True
        Me.FTStartPurchaseDate_lbl.Appearance.Options.UseTextOptions = True
        Me.FTStartPurchaseDate_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTStartPurchaseDate_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTStartPurchaseDate_lbl.Location = New System.Drawing.Point(8, 30)
        Me.FTStartPurchaseDate_lbl.Name = "FTStartPurchaseDate_lbl"
        Me.FTStartPurchaseDate_lbl.Size = New System.Drawing.Size(149, 19)
        Me.FTStartPurchaseDate_lbl.TabIndex = 273
        Me.FTStartPurchaseDate_lbl.Tag = "2|"
        Me.FTStartPurchaseDate_lbl.Text = "Start Calculate Date :"
        '
        'ogbdetail
        '
        Me.ogbdetail.Controls.Add(Me.ogbmainprocbutton)
        Me.ogbdetail.Controls.Add(Me.ogdtime)
        Me.ogbdetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogbdetail.Location = New System.Drawing.Point(0, 142)
        Me.ogbdetail.Name = "ogbdetail"
        Me.ogbdetail.ShowCaption = False
        Me.ogbdetail.Size = New System.Drawing.Size(1318, 491)
        Me.ogbdetail.TabIndex = 0
        '
        'ogbmainprocbutton
        '
        Me.ogbmainprocbutton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmsavelayout)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmexit)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmclear)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmload)
        Me.ogbmainprocbutton.Location = New System.Drawing.Point(111, 110)
        Me.ogbmainprocbutton.Name = "ogbmainprocbutton"
        Me.ogbmainprocbutton.Size = New System.Drawing.Size(1096, 276)
        Me.ogbmainprocbutton.TabIndex = 386
        Me.ogbmainprocbutton.Tag = "2|"
        '
        'ocmsavelayout
        '
        Me.ocmsavelayout.Location = New System.Drawing.Point(409, 12)
        Me.ocmsavelayout.Name = "ocmsavelayout"
        Me.ocmsavelayout.Size = New System.Drawing.Size(117, 23)
        Me.ocmsavelayout.TabIndex = 332
        Me.ocmsavelayout.Text = "savelayoutgrid"
        '
        'ocmexit
        '
        Me.ocmexit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmexit.Location = New System.Drawing.Point(983, 11)
        Me.ocmexit.Name = "ocmexit"
        Me.ocmexit.Size = New System.Drawing.Size(95, 25)
        Me.ocmexit.TabIndex = 96
        Me.ocmexit.TabStop = False
        Me.ocmexit.Tag = "2|"
        Me.ocmexit.Text = "EXIT"
        '
        'ocmclear
        '
        Me.ocmclear.Location = New System.Drawing.Point(14, 10)
        Me.ocmclear.Name = "ocmclear"
        Me.ocmclear.Size = New System.Drawing.Size(95, 25)
        Me.ocmclear.TabIndex = 95
        Me.ocmclear.TabStop = False
        Me.ocmclear.Tag = "2|"
        Me.ocmclear.Text = "CLEAR"
        '
        'ocmload
        '
        Me.ocmload.Location = New System.Drawing.Point(115, 12)
        Me.ocmload.Name = "ocmload"
        Me.ocmload.Size = New System.Drawing.Size(117, 23)
        Me.ocmload.TabIndex = 329
        Me.ocmload.Text = "Load Data"
        '
        'ogdtime
        '
        Me.ogdtime.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogdtime.Location = New System.Drawing.Point(2, 2)
        Me.ogdtime.MainView = Me.ogvtime
        Me.ogdtime.Name = "ogdtime"
        Me.ogdtime.Size = New System.Drawing.Size(1314, 487)
        Me.ogdtime.TabIndex = 0
        Me.ogdtime.TabStop = False
        Me.ogdtime.Tag = "2|"
        Me.ogdtime.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvtime})
        '
        'ogvtime
        '
        Me.ogvtime.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.xFTStyleCode, Me.xFTSeasonCode, Me.xFTOrderNo, Me.xFTSubOrderNo, Me.xFDShipDate, Me.xFDOGacDate, Me.xFTMainMatCode, Me.xFTRawMatColorCode, Me.xFTRawMatColorNameEN, Me.xFTRawMatColorNameTH, Me.xFTRawMatSizeCode, Me.xFNUsedQuantity, Me.xFNUsedPlusQuantity, Me.xFNPRQuantity, Me.xFTUserLogin, Me.xFTCalDate, Me.xFTCalTime, Me.xFTCalTypeName, Me.xFTFilterItem})
        Me.ogvtime.GridControl = Me.ogdtime
        Me.ogvtime.Name = "ogvtime"
        Me.ogvtime.OptionsCustomization.AllowQuickHideColumns = False
        Me.ogvtime.OptionsView.ColumnAutoWidth = False
        Me.ogvtime.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.ogvtime.OptionsView.ShowGroupPanel = False
        Me.ogvtime.Tag = "2|"
        '
        'xFTStyleCode
        '
        Me.xFTStyleCode.Caption = "Style No"
        Me.xFTStyleCode.FieldName = "FTStyleCode"
        Me.xFTStyleCode.Name = "xFTStyleCode"
        Me.xFTStyleCode.OptionsColumn.AllowEdit = False
        Me.xFTStyleCode.OptionsColumn.ReadOnly = True
        Me.xFTStyleCode.Visible = True
        Me.xFTStyleCode.VisibleIndex = 0
        Me.xFTStyleCode.Width = 100
        '
        'xFTSeasonCode
        '
        Me.xFTSeasonCode.Caption = "Season"
        Me.xFTSeasonCode.FieldName = "FTSeasonCode"
        Me.xFTSeasonCode.Name = "xFTSeasonCode"
        Me.xFTSeasonCode.OptionsColumn.AllowEdit = False
        Me.xFTSeasonCode.OptionsColumn.ReadOnly = True
        Me.xFTSeasonCode.Visible = True
        Me.xFTSeasonCode.VisibleIndex = 1
        Me.xFTSeasonCode.Width = 80
        '
        'xFTOrderNo
        '
        Me.xFTOrderNo.Caption = "Order No"
        Me.xFTOrderNo.FieldName = "FTOrderNo"
        Me.xFTOrderNo.Name = "xFTOrderNo"
        Me.xFTOrderNo.OptionsColumn.AllowEdit = False
        Me.xFTOrderNo.OptionsColumn.ReadOnly = True
        Me.xFTOrderNo.Visible = True
        Me.xFTOrderNo.VisibleIndex = 2
        Me.xFTOrderNo.Width = 120
        '
        'xFTSubOrderNo
        '
        Me.xFTSubOrderNo.Caption = "Sub Order No"
        Me.xFTSubOrderNo.FieldName = "FTSubOrderNo"
        Me.xFTSubOrderNo.Name = "xFTSubOrderNo"
        Me.xFTSubOrderNo.OptionsColumn.AllowEdit = False
        Me.xFTSubOrderNo.OptionsColumn.ReadOnly = True
        Me.xFTSubOrderNo.Visible = True
        Me.xFTSubOrderNo.VisibleIndex = 3
        Me.xFTSubOrderNo.Width = 100
        '
        'xFDShipDate
        '
        Me.xFDShipDate.Caption = "Gac Date"
        Me.xFDShipDate.FieldName = "FDShipDate"
        Me.xFDShipDate.Name = "xFDShipDate"
        Me.xFDShipDate.OptionsColumn.AllowEdit = False
        Me.xFDShipDate.OptionsColumn.ReadOnly = True
        Me.xFDShipDate.Visible = True
        Me.xFDShipDate.VisibleIndex = 4
        Me.xFDShipDate.Width = 100
        '
        'xFDOGacDate
        '
        Me.xFDOGacDate.Caption = "O Gac Date"
        Me.xFDOGacDate.FieldName = "FDOGacDate"
        Me.xFDOGacDate.Name = "xFDOGacDate"
        Me.xFDOGacDate.OptionsColumn.AllowEdit = False
        Me.xFDOGacDate.OptionsColumn.ReadOnly = True
        Me.xFDOGacDate.Visible = True
        Me.xFDOGacDate.VisibleIndex = 5
        Me.xFDOGacDate.Width = 100
        '
        'xFTMainMatCode
        '
        Me.xFTMainMatCode.Caption = "Material Code"
        Me.xFTMainMatCode.FieldName = "FTMainMatCode"
        Me.xFTMainMatCode.Name = "xFTMainMatCode"
        Me.xFTMainMatCode.OptionsColumn.AllowEdit = False
        Me.xFTMainMatCode.OptionsColumn.ReadOnly = True
        Me.xFTMainMatCode.Visible = True
        Me.xFTMainMatCode.VisibleIndex = 6
        Me.xFTMainMatCode.Width = 150
        '
        'xFTRawMatColorCode
        '
        Me.xFTRawMatColorCode.Caption = "Material Color Code"
        Me.xFTRawMatColorCode.FieldName = "FTRawMatColorCode"
        Me.xFTRawMatColorCode.Name = "xFTRawMatColorCode"
        Me.xFTRawMatColorCode.OptionsColumn.AllowEdit = False
        Me.xFTRawMatColorCode.OptionsColumn.ReadOnly = True
        Me.xFTRawMatColorCode.Visible = True
        Me.xFTRawMatColorCode.VisibleIndex = 7
        Me.xFTRawMatColorCode.Width = 100
        '
        'xFTRawMatColorNameEN
        '
        Me.xFTRawMatColorNameEN.Caption = "Color Name EN"
        Me.xFTRawMatColorNameEN.FieldName = "FTRawMatColorNameEN"
        Me.xFTRawMatColorNameEN.Name = "xFTRawMatColorNameEN"
        Me.xFTRawMatColorNameEN.OptionsColumn.AllowEdit = False
        Me.xFTRawMatColorNameEN.OptionsColumn.ReadOnly = True
        Me.xFTRawMatColorNameEN.Visible = True
        Me.xFTRawMatColorNameEN.VisibleIndex = 8
        Me.xFTRawMatColorNameEN.Width = 150
        '
        'xFTRawMatColorNameTH
        '
        Me.xFTRawMatColorNameTH.Caption = "Color Name TH"
        Me.xFTRawMatColorNameTH.Name = "xFTRawMatColorNameTH"
        Me.xFTRawMatColorNameTH.OptionsColumn.AllowEdit = False
        Me.xFTRawMatColorNameTH.OptionsColumn.ReadOnly = True
        Me.xFTRawMatColorNameTH.Visible = True
        Me.xFTRawMatColorNameTH.VisibleIndex = 9
        Me.xFTRawMatColorNameTH.Width = 150
        '
        'xFTRawMatSizeCode
        '
        Me.xFTRawMatSizeCode.Caption = "Size"
        Me.xFTRawMatSizeCode.FieldName = "FTRawMatSizeCode"
        Me.xFTRawMatSizeCode.Name = "xFTRawMatSizeCode"
        Me.xFTRawMatSizeCode.OptionsColumn.AllowEdit = False
        Me.xFTRawMatSizeCode.OptionsColumn.ReadOnly = True
        Me.xFTRawMatSizeCode.Visible = True
        Me.xFTRawMatSizeCode.VisibleIndex = 10
        Me.xFTRawMatSizeCode.Width = 100
        '
        'xFNUsedQuantity
        '
        Me.xFNUsedQuantity.Caption = "FNUsedQuantity"
        Me.xFNUsedQuantity.DisplayFormat.FormatString = "{n0:4}"
        Me.xFNUsedQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.xFNUsedQuantity.FieldName = "FNUsedQuantity"
        Me.xFNUsedQuantity.Name = "xFNUsedQuantity"
        Me.xFNUsedQuantity.OptionsColumn.AllowEdit = False
        Me.xFNUsedQuantity.OptionsColumn.ReadOnly = True
        Me.xFNUsedQuantity.Visible = True
        Me.xFNUsedQuantity.VisibleIndex = 11
        Me.xFNUsedQuantity.Width = 100
        '
        'xFNUsedPlusQuantity
        '
        Me.xFNUsedPlusQuantity.Caption = "FNUsedPlusQuantity"
        Me.xFNUsedPlusQuantity.DisplayFormat.FormatString = "{n0:4}"
        Me.xFNUsedPlusQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.xFNUsedPlusQuantity.FieldName = "FNUsedPlusQuantity"
        Me.xFNUsedPlusQuantity.Name = "xFNUsedPlusQuantity"
        Me.xFNUsedPlusQuantity.OptionsColumn.AllowEdit = False
        Me.xFNUsedPlusQuantity.OptionsColumn.ReadOnly = True
        Me.xFNUsedPlusQuantity.Visible = True
        Me.xFNUsedPlusQuantity.VisibleIndex = 12
        Me.xFNUsedPlusQuantity.Width = 100
        '
        'xFNPRQuantity
        '
        Me.xFNPRQuantity.Caption = "FNPRQuantity"
        Me.xFNPRQuantity.DisplayFormat.FormatString = "{n0:4}"
        Me.xFNPRQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.xFNPRQuantity.FieldName = "FNPRQuantity"
        Me.xFNPRQuantity.Name = "xFNPRQuantity"
        Me.xFNPRQuantity.OptionsColumn.AllowEdit = False
        Me.xFNPRQuantity.OptionsColumn.ReadOnly = True
        Me.xFNPRQuantity.Visible = True
        Me.xFNPRQuantity.VisibleIndex = 13
        Me.xFNPRQuantity.Width = 100
        '
        'xFTUserLogin
        '
        Me.xFTUserLogin.Caption = "Calculate By"
        Me.xFTUserLogin.FieldName = "FTUserLogin"
        Me.xFTUserLogin.Name = "xFTUserLogin"
        Me.xFTUserLogin.OptionsColumn.AllowEdit = False
        Me.xFTUserLogin.OptionsColumn.ReadOnly = True
        Me.xFTUserLogin.Visible = True
        Me.xFTUserLogin.VisibleIndex = 14
        Me.xFTUserLogin.Width = 100
        '
        'xFTCalDate
        '
        Me.xFTCalDate.Caption = "Calculate Date"
        Me.xFTCalDate.FieldName = "FTCalDate"
        Me.xFTCalDate.Name = "xFTCalDate"
        Me.xFTCalDate.OptionsColumn.AllowEdit = False
        Me.xFTCalDate.OptionsColumn.ReadOnly = True
        Me.xFTCalDate.Visible = True
        Me.xFTCalDate.VisibleIndex = 15
        Me.xFTCalDate.Width = 80
        '
        'xFTCalTime
        '
        Me.xFTCalTime.Caption = "Calculate Time"
        Me.xFTCalTime.FieldName = "FTCalTime"
        Me.xFTCalTime.Name = "xFTCalTime"
        Me.xFTCalTime.OptionsColumn.AllowEdit = False
        Me.xFTCalTime.OptionsColumn.ReadOnly = True
        Me.xFTCalTime.Visible = True
        Me.xFTCalTime.VisibleIndex = 16
        Me.xFTCalTime.Width = 80
        '
        'xFTCalTypeName
        '
        Me.xFTCalTypeName.Caption = "Action Type"
        Me.xFTCalTypeName.FieldName = "FTCalTypeName"
        Me.xFTCalTypeName.Name = "xFTCalTypeName"
        Me.xFTCalTypeName.OptionsColumn.AllowEdit = False
        Me.xFTCalTypeName.OptionsColumn.ReadOnly = True
        Me.xFTCalTypeName.Visible = True
        Me.xFTCalTypeName.VisibleIndex = 17
        Me.xFTCalTypeName.Width = 80
        '
        'xFTFilterItem
        '
        Me.xFTFilterItem.Caption = "Filter Calcuate Item"
        Me.xFTFilterItem.FieldName = "FTFilterItem"
        Me.xFTFilterItem.Name = "xFTFilterItem"
        Me.xFTFilterItem.OptionsColumn.AllowEdit = False
        Me.xFTFilterItem.OptionsColumn.ReadOnly = True
        Me.xFTFilterItem.Visible = True
        Me.xFTFilterItem.VisibleIndex = 18
        Me.xFTFilterItem.Width = 150
        '
        'oDockManager
        '
        Me.oDockManager.Form = Me
        Me.oDockManager.RootPanels.AddRange(New DevExpress.XtraBars.Docking.DockPanel() {Me.ogbheader})
        Me.oDockManager.TopZIndexControls.AddRange(New String() {"DevExpress.XtraBars.BarDockControl", "DevExpress.XtraBars.StandaloneBarDockControl", "System.Windows.Forms.StatusBar", "DevExpress.XtraBars.Ribbon.RibbonStatusBar", "DevExpress.XtraBars.Ribbon.RibbonControl"})
        '
        'wCalculateMRPHistoryTracking
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1318, 633)
        Me.Controls.Add(Me.ogbdetail)
        Me.Controls.Add(Me.ogbheader)
        Me.Name = "wCalculateMRPHistoryTracking"
        Me.Text = "Calculate MRP History Tracking"
        Me.ogbheader.ResumeLayout(False)
        Me.DockPanel1_Container.ResumeLayout(False)
        CType(Me.FTOrderNo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysSeasonId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNListDocumentTrackPIData.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTEndPurchaseDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTEndPurchaseDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysStyleId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTStartPurchaseDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTStartPurchaseDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogbdetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbdetail.ResumeLayout(False)
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbmainprocbutton.ResumeLayout(False)
        CType(Me.ogdtime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvtime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.oDockManager, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogbdetail As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ogdtime As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvtime As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents ogbmainprocbutton As DevExpress.XtraEditors.PanelControl
    Friend WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmclear As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmload As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents oDockManager As DevExpress.XtraBars.Docking.DockManager
    Friend WithEvents ogbheader As DevExpress.XtraBars.Docking.DockPanel
    Friend WithEvents DockPanel1_Container As DevExpress.XtraBars.Docking.ControlContainer
    Friend WithEvents ocmsavelayout As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FTEndPurchaseDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents FTEndPurchaseDate_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTStartPurchaseDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents FTStartPurchaseDate_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNListDocumentTrackPIData As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents FNListDocumentTrackPIData_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysSeasonId_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysSeasonId As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FNHSysStyleId_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysStyleId As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FTOrderNo_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTOrderNo As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents xFTStyleCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents xFTSeasonCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents xFTOrderNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents xFTSubOrderNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents xFDShipDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents xFDOGacDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents xFTMainMatCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents xFTRawMatColorCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents xFTRawMatColorNameEN As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents xFTRawMatColorNameTH As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents xFTRawMatSizeCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents xFNUsedQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents xFNUsedPlusQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents xFNPRQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents xFTUserLogin As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents xFTCalDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents xFTCalTime As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents xFTCalTypeName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents xFTFilterItem As DevExpress.XtraGrid.Columns.GridColumn
End Class

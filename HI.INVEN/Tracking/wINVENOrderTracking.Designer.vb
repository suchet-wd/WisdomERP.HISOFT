<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wINVENOrderTracking
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(wINVENOrderTracking))
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject4 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.ogbheader = New DevExpress.XtraBars.Docking.DockPanel()
        Me.DockPanel1_Container = New DevExpress.XtraBars.Docking.ControlContainer()
        Me.FTEndShipment = New DevExpress.XtraEditors.DateEdit()
        Me.FTEndShipment_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTStartShipment = New DevExpress.XtraEditors.DateEdit()
        Me.FTStartShipment_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTOrderNoTo_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysStyleId_None = New DevExpress.XtraEditors.TextEdit()
        Me.FTOrderNoTo = New DevExpress.XtraEditors.ButtonEdit()
        Me.FTOrderNo_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysBuyId_None = New DevExpress.XtraEditors.TextEdit()
        Me.FTOrderNo = New DevExpress.XtraEditors.ButtonEdit()
        Me.FNHSysStyleId_lbl2 = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysBuyId = New DevExpress.XtraEditors.ButtonEdit()
        Me.FNHSysStyleId = New DevExpress.XtraEditors.ButtonEdit()
        Me.FNHSysBuyId_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.ogbdetail = New DevExpress.XtraEditors.GroupControl()
        Me.ogbmainprocbutton = New DevExpress.XtraEditors.PanelControl()
        Me.ocmpreview = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmsavelayout = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmclear = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmload = New DevExpress.XtraEditors.SimpleButton()
        Me.ogdtime = New DevExpress.XtraGrid.GridControl()
        Me.ogvtime = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.CFTOrderNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTRawMatCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTRawMatName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTRawMatColorCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTRawMatSizeCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNUsedQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNUsedPlusQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTUnitCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNRSVQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNPOQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNRCVQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNRTSQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNPOBalQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNRCVStockQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNTROInQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNTROOutQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNISSQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNRETQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNADJInQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNADJOutQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNTRWInQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNTRWOutQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNSaleQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNTerminateQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNTRCQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNRTSAfQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNCNSInQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNCNSOutQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNRSVOutQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNOnhandQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.oDockManager = New DevExpress.XtraBars.Docking.DockManager()
        Me.hideContainerTop = New DevExpress.XtraBars.Docking.AutoHideContainer()
        Me.ogbheader.SuspendLayout()
        Me.DockPanel1_Container.SuspendLayout()
        CType(Me.FTEndShipment.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTEndShipment.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTStartShipment.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTStartShipment.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysStyleId_None.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTOrderNoTo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysBuyId_None.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTOrderNo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysBuyId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysStyleId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogbdetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbdetail.SuspendLayout()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbmainprocbutton.SuspendLayout()
        CType(Me.ogdtime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvtime, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.ogbheader.Location = New System.Drawing.Point(0, 0)
        Me.ogbheader.Name = "ogbheader"
        Me.ogbheader.Options.AllowDockBottom = False
        Me.ogbheader.Options.AllowDockFill = False
        Me.ogbheader.Options.FloatOnDblClick = False
        Me.ogbheader.Options.ShowCloseButton = False
        Me.ogbheader.Options.ShowMaximizeButton = False
        Me.ogbheader.OriginalSize = New System.Drawing.Size(1326, 184)
        Me.ogbheader.SavedDock = DevExpress.XtraBars.Docking.DockingStyle.Top
        Me.ogbheader.SavedIndex = 0
        Me.ogbheader.Size = New System.Drawing.Size(1137, 184)
        Me.ogbheader.TabStop = False
        Me.ogbheader.Text = "Criteria"
        Me.ogbheader.Visibility = DevExpress.XtraBars.Docking.DockVisibility.AutoHide
        '
        'DockPanel1_Container
        '
        Me.DockPanel1_Container.Controls.Add(Me.FTEndShipment)
        Me.DockPanel1_Container.Controls.Add(Me.FTEndShipment_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FTStartShipment)
        Me.DockPanel1_Container.Controls.Add(Me.FTStartShipment_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FTOrderNoTo_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FNHSysStyleId_None)
        Me.DockPanel1_Container.Controls.Add(Me.FTOrderNoTo)
        Me.DockPanel1_Container.Controls.Add(Me.FTOrderNo_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FNHSysBuyId_None)
        Me.DockPanel1_Container.Controls.Add(Me.FTOrderNo)
        Me.DockPanel1_Container.Controls.Add(Me.FNHSysStyleId_lbl2)
        Me.DockPanel1_Container.Controls.Add(Me.FNHSysBuyId)
        Me.DockPanel1_Container.Controls.Add(Me.FNHSysStyleId)
        Me.DockPanel1_Container.Controls.Add(Me.FNHSysBuyId_lbl)
        Me.DockPanel1_Container.Location = New System.Drawing.Point(4, 23)
        Me.DockPanel1_Container.Name = "DockPanel1_Container"
        Me.DockPanel1_Container.Size = New System.Drawing.Size(1129, 156)
        Me.DockPanel1_Container.TabIndex = 0
        '
        'FTEndShipment
        '
        Me.FTEndShipment.EditValue = Nothing
        Me.FTEndShipment.EnterMoveNextControl = True
        Me.FTEndShipment.Location = New System.Drawing.Point(552, 66)
        Me.FTEndShipment.Name = "FTEndShipment"
        Me.FTEndShipment.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTEndShipment.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FTEndShipment.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.FTEndShipment.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTEndShipment.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTEndShipment.Properties.NullDate = ""
        Me.FTEndShipment.Size = New System.Drawing.Size(130, 20)
        Me.FTEndShipment.TabIndex = 270
        Me.FTEndShipment.Tag = "2|"
        '
        'FTEndShipment_lbl
        '
        Me.FTEndShipment_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTEndShipment_lbl.Appearance.Options.UseForeColor = True
        Me.FTEndShipment_lbl.Appearance.Options.UseTextOptions = True
        Me.FTEndShipment_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTEndShipment_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTEndShipment_lbl.Location = New System.Drawing.Point(369, 65)
        Me.FTEndShipment_lbl.Name = "FTEndShipment_lbl"
        Me.FTEndShipment_lbl.Size = New System.Drawing.Size(181, 19)
        Me.FTEndShipment_lbl.TabIndex = 271
        Me.FTEndShipment_lbl.Tag = "2|"
        Me.FTEndShipment_lbl.Text = "Start Shipment:"
        '
        'FTStartShipment
        '
        Me.FTStartShipment.EditValue = Nothing
        Me.FTStartShipment.EnterMoveNextControl = True
        Me.FTStartShipment.Location = New System.Drawing.Point(238, 66)
        Me.FTStartShipment.Name = "FTStartShipment"
        Me.FTStartShipment.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTStartShipment.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FTStartShipment.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.FTStartShipment.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTStartShipment.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTStartShipment.Properties.NullDate = ""
        Me.FTStartShipment.Size = New System.Drawing.Size(130, 20)
        Me.FTStartShipment.TabIndex = 268
        Me.FTStartShipment.Tag = "2|"
        '
        'FTStartShipment_lbl
        '
        Me.FTStartShipment_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTStartShipment_lbl.Appearance.Options.UseForeColor = True
        Me.FTStartShipment_lbl.Appearance.Options.UseTextOptions = True
        Me.FTStartShipment_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTStartShipment_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTStartShipment_lbl.Location = New System.Drawing.Point(8, 65)
        Me.FTStartShipment_lbl.Name = "FTStartShipment_lbl"
        Me.FTStartShipment_lbl.Size = New System.Drawing.Size(228, 19)
        Me.FTStartShipment_lbl.TabIndex = 269
        Me.FTStartShipment_lbl.Tag = "2|"
        Me.FTStartShipment_lbl.Text = "Start Shipment:"
        '
        'FTOrderNoTo_lbl
        '
        Me.FTOrderNoTo_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTOrderNoTo_lbl.Appearance.Options.UseForeColor = True
        Me.FTOrderNoTo_lbl.Appearance.Options.UseTextOptions = True
        Me.FTOrderNoTo_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTOrderNoTo_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTOrderNoTo_lbl.Location = New System.Drawing.Point(374, 45)
        Me.FTOrderNoTo_lbl.Name = "FTOrderNoTo_lbl"
        Me.FTOrderNoTo_lbl.Size = New System.Drawing.Size(176, 19)
        Me.FTOrderNoTo_lbl.TabIndex = 287
        Me.FTOrderNoTo_lbl.Tag = "2|"
        Me.FTOrderNoTo_lbl.Text = "To Order No :"
        '
        'FNHSysStyleId_None
        '
        Me.FNHSysStyleId_None.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FNHSysStyleId_None.Location = New System.Drawing.Point(369, 24)
        Me.FNHSysStyleId_None.Name = "FNHSysStyleId_None"
        Me.FNHSysStyleId_None.Properties.Appearance.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysStyleId_None.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysStyleId_None.Properties.ReadOnly = True
        Me.FNHSysStyleId_None.Size = New System.Drawing.Size(526, 20)
        Me.FNHSysStyleId_None.TabIndex = 291
        Me.FNHSysStyleId_None.Tag = "2|"
        '
        'FTOrderNoTo
        '
        Me.FTOrderNoTo.EnterMoveNextControl = True
        Me.FTOrderNoTo.Location = New System.Drawing.Point(552, 45)
        Me.FTOrderNoTo.Name = "FTOrderNoTo"
        Me.FTOrderNoTo.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FTOrderNoTo.Properties.Appearance.Options.UseBackColor = True
        Me.FTOrderNoTo.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FTOrderNoTo.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FTOrderNoTo.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FTOrderNoTo.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FTOrderNoTo.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FTOrderNoTo.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FTOrderNoTo.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FTOrderNoTo.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FTOrderNoTo.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FTOrderNoTo.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FTOrderNoTo.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FTOrderNoTo.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FTOrderNoTo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", 15, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", "219", Nothing, True)})
        Me.FTOrderNoTo.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.FTOrderNoTo.Properties.MaxLength = 30
        Me.FTOrderNoTo.Size = New System.Drawing.Size(130, 20)
        Me.FTOrderNoTo.TabIndex = 286
        Me.FTOrderNoTo.Tag = "2|"
        '
        'FTOrderNo_lbl
        '
        Me.FTOrderNo_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTOrderNo_lbl.Appearance.Options.UseForeColor = True
        Me.FTOrderNo_lbl.Appearance.Options.UseTextOptions = True
        Me.FTOrderNo_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTOrderNo_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTOrderNo_lbl.Location = New System.Drawing.Point(8, 44)
        Me.FTOrderNo_lbl.Name = "FTOrderNo_lbl"
        Me.FTOrderNo_lbl.Size = New System.Drawing.Size(228, 19)
        Me.FTOrderNo_lbl.TabIndex = 285
        Me.FTOrderNo_lbl.Tag = "2|"
        Me.FTOrderNo_lbl.Text = "From Order No :"
        '
        'FNHSysBuyId_None
        '
        Me.FNHSysBuyId_None.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FNHSysBuyId_None.Location = New System.Drawing.Point(369, 3)
        Me.FNHSysBuyId_None.Name = "FNHSysBuyId_None"
        Me.FNHSysBuyId_None.Properties.Appearance.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysBuyId_None.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysBuyId_None.Properties.ReadOnly = True
        Me.FNHSysBuyId_None.Size = New System.Drawing.Size(526, 20)
        Me.FNHSysBuyId_None.TabIndex = 289
        Me.FNHSysBuyId_None.Tag = "2|"
        '
        'FTOrderNo
        '
        Me.FTOrderNo.EnterMoveNextControl = True
        Me.FTOrderNo.Location = New System.Drawing.Point(238, 45)
        Me.FTOrderNo.Name = "FTOrderNo"
        Me.FTOrderNo.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FTOrderNo.Properties.Appearance.Options.UseBackColor = True
        Me.FTOrderNo.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FTOrderNo.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FTOrderNo.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FTOrderNo.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FTOrderNo.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FTOrderNo.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FTOrderNo.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FTOrderNo.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FTOrderNo.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FTOrderNo.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FTOrderNo.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FTOrderNo.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FTOrderNo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", 15, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject2, "", "218", Nothing, True)})
        Me.FTOrderNo.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.FTOrderNo.Properties.MaxLength = 30
        Me.FTOrderNo.Size = New System.Drawing.Size(130, 20)
        Me.FTOrderNo.TabIndex = 284
        Me.FTOrderNo.Tag = "2|"
        '
        'FNHSysStyleId_lbl2
        '
        Me.FNHSysStyleId_lbl2.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FNHSysStyleId_lbl2.Appearance.Options.UseForeColor = True
        Me.FNHSysStyleId_lbl2.Appearance.Options.UseTextOptions = True
        Me.FNHSysStyleId_lbl2.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysStyleId_lbl2.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysStyleId_lbl2.Location = New System.Drawing.Point(8, 24)
        Me.FNHSysStyleId_lbl2.Name = "FNHSysStyleId_lbl2"
        Me.FNHSysStyleId_lbl2.Size = New System.Drawing.Size(224, 20)
        Me.FNHSysStyleId_lbl2.TabIndex = 293
        Me.FNHSysStyleId_lbl2.Tag = "2|"
        Me.FNHSysStyleId_lbl2.Text = "Style No :"
        '
        'FNHSysBuyId
        '
        Me.FNHSysBuyId.Location = New System.Drawing.Point(238, 3)
        Me.FNHSysBuyId.Name = "FNHSysBuyId"
        Me.FNHSysBuyId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject3, "", "113", Nothing, True)})
        Me.FNHSysBuyId.Properties.Tag = ""
        Me.FNHSysBuyId.Size = New System.Drawing.Size(130, 20)
        Me.FNHSysBuyId.TabIndex = 288
        Me.FNHSysBuyId.Tag = "2|"
        '
        'FNHSysStyleId
        '
        Me.FNHSysStyleId.Location = New System.Drawing.Point(238, 24)
        Me.FNHSysStyleId.Name = "FNHSysStyleId"
        Me.FNHSysStyleId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject4, "", "89", Nothing, True)})
        Me.FNHSysStyleId.Properties.Tag = ""
        Me.FNHSysStyleId.Size = New System.Drawing.Size(130, 20)
        Me.FNHSysStyleId.TabIndex = 290
        Me.FNHSysStyleId.Tag = "2|"
        '
        'FNHSysBuyId_lbl
        '
        Me.FNHSysBuyId_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FNHSysBuyId_lbl.Appearance.Options.UseForeColor = True
        Me.FNHSysBuyId_lbl.Appearance.Options.UseTextOptions = True
        Me.FNHSysBuyId_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysBuyId_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysBuyId_lbl.Location = New System.Drawing.Point(8, 3)
        Me.FNHSysBuyId_lbl.Name = "FNHSysBuyId_lbl"
        Me.FNHSysBuyId_lbl.Size = New System.Drawing.Size(224, 20)
        Me.FNHSysBuyId_lbl.TabIndex = 292
        Me.FNHSysBuyId_lbl.Tag = "2|"
        Me.FNHSysBuyId_lbl.Text = "Buy :"
        '
        'ogbdetail
        '
        Me.ogbdetail.Controls.Add(Me.ogbmainprocbutton)
        Me.ogbdetail.Controls.Add(Me.ogdtime)
        Me.ogbdetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogbdetail.Location = New System.Drawing.Point(0, 38)
        Me.ogbdetail.Name = "ogbdetail"
        Me.ogbdetail.ShowCaption = False
        Me.ogbdetail.Size = New System.Drawing.Size(1137, 595)
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
        Me.ogbmainprocbutton.Location = New System.Drawing.Point(91, 110)
        Me.ogbmainprocbutton.Name = "ogbmainprocbutton"
        Me.ogbmainprocbutton.Size = New System.Drawing.Size(935, 47)
        Me.ogbmainprocbutton.TabIndex = 386
        Me.ogbmainprocbutton.Tag = "2|"
        '
        'ocmpreview
        '
        Me.ocmpreview.Location = New System.Drawing.Point(238, 12)
        Me.ocmpreview.Name = "ocmpreview"
        Me.ocmpreview.Size = New System.Drawing.Size(121, 25)
        Me.ocmpreview.TabIndex = 333
        Me.ocmpreview.TabStop = False
        Me.ocmpreview.Tag = "2|"
        Me.ocmpreview.Text = "PREVIEW"
        '
        'ocmsavelayout
        '
        Me.ocmsavelayout.Location = New System.Drawing.Point(371, 12)
        Me.ocmsavelayout.Name = "ocmsavelayout"
        Me.ocmsavelayout.Size = New System.Drawing.Size(117, 23)
        Me.ocmsavelayout.TabIndex = 332
        Me.ocmsavelayout.Text = "savelayoutgrid"
        '
        'ocmexit
        '
        Me.ocmexit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmexit.Location = New System.Drawing.Point(822, 11)
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
        Me.ogdtime.Size = New System.Drawing.Size(1133, 591)
        Me.ogdtime.TabIndex = 0
        Me.ogdtime.TabStop = False
        Me.ogdtime.Tag = "2|"
        Me.ogdtime.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvtime})
        '
        'ogvtime
        '
        Me.ogvtime.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.CFTOrderNo, Me.FTRawMatCode, Me.FTRawMatName, Me.FTRawMatColorCode, Me.FTRawMatSizeCode, Me.FNUsedQuantity, Me.FNUsedPlusQuantity, Me.FTUnitCode, Me.FNRSVQuantity, Me.FNPOQuantity, Me.FNRCVQuantity, Me.FNRTSQuantity, Me.FNPOBalQuantity, Me.FNRCVStockQuantity, Me.FNTROInQuantity, Me.FNTROOutQuantity, Me.FNISSQuantity, Me.FNRETQuantity, Me.FNADJInQuantity, Me.FNADJOutQuantity, Me.FNTRWInQuantity, Me.FNTRWOutQuantity, Me.FNSaleQuantity, Me.FNTerminateQuantity, Me.FNTRCQuantity, Me.FNRTSAfQuantity, Me.FNCNSInQuantity, Me.FNCNSOutQuantity, Me.FNRSVOutQuantity, Me.FNOnhandQuantity})
        Me.ogvtime.GridControl = Me.ogdtime
        Me.ogvtime.Name = "ogvtime"
        Me.ogvtime.OptionsCustomization.AllowQuickHideColumns = False
        Me.ogvtime.OptionsView.ColumnAutoWidth = False
        Me.ogvtime.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.ogvtime.OptionsView.ShowGroupPanel = False
        Me.ogvtime.Tag = "2|"
        '
        'CFTOrderNo
        '
        Me.CFTOrderNo.AppearanceHeader.Options.UseTextOptions = True
        Me.CFTOrderNo.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.CFTOrderNo.Caption = "FTOrderNo"
        Me.CFTOrderNo.FieldName = "FTOrderNo"
        Me.CFTOrderNo.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
        Me.CFTOrderNo.Name = "CFTOrderNo"
        Me.CFTOrderNo.OptionsColumn.AllowEdit = False
        Me.CFTOrderNo.OptionsColumn.ReadOnly = True
        Me.CFTOrderNo.Visible = True
        Me.CFTOrderNo.VisibleIndex = 0
        Me.CFTOrderNo.Width = 109
        '
        'FTRawMatCode
        '
        Me.FTRawMatCode.AppearanceHeader.Options.UseTextOptions = True
        Me.FTRawMatCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTRawMatCode.Caption = "FTRawMatCode"
        Me.FTRawMatCode.FieldName = "FTRawMatCode"
        Me.FTRawMatCode.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
        Me.FTRawMatCode.Name = "FTRawMatCode"
        Me.FTRawMatCode.OptionsColumn.AllowEdit = False
        Me.FTRawMatCode.OptionsColumn.ReadOnly = True
        Me.FTRawMatCode.Visible = True
        Me.FTRawMatCode.VisibleIndex = 1
        Me.FTRawMatCode.Width = 114
        '
        'FTRawMatName
        '
        Me.FTRawMatName.AppearanceHeader.Options.UseTextOptions = True
        Me.FTRawMatName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTRawMatName.Caption = "FTRawMatName"
        Me.FTRawMatName.FieldName = "FTRawMatName"
        Me.FTRawMatName.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
        Me.FTRawMatName.Name = "FTRawMatName"
        Me.FTRawMatName.OptionsColumn.AllowEdit = False
        Me.FTRawMatName.OptionsColumn.ReadOnly = True
        Me.FTRawMatName.Visible = True
        Me.FTRawMatName.VisibleIndex = 2
        Me.FTRawMatName.Width = 251
        '
        'FTRawMatColorCode
        '
        Me.FTRawMatColorCode.AppearanceHeader.Options.UseTextOptions = True
        Me.FTRawMatColorCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTRawMatColorCode.Caption = "FTRawMatColorCode"
        Me.FTRawMatColorCode.FieldName = "FTRawMatColorCode"
        Me.FTRawMatColorCode.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
        Me.FTRawMatColorCode.Name = "FTRawMatColorCode"
        Me.FTRawMatColorCode.OptionsColumn.AllowEdit = False
        Me.FTRawMatColorCode.OptionsColumn.ReadOnly = True
        Me.FTRawMatColorCode.Visible = True
        Me.FTRawMatColorCode.VisibleIndex = 3
        Me.FTRawMatColorCode.Width = 55
        '
        'FTRawMatSizeCode
        '
        Me.FTRawMatSizeCode.AppearanceHeader.Options.UseTextOptions = True
        Me.FTRawMatSizeCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTRawMatSizeCode.Caption = "FTRawMatSizeCode"
        Me.FTRawMatSizeCode.FieldName = "FTRawMatSizeCode"
        Me.FTRawMatSizeCode.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
        Me.FTRawMatSizeCode.Name = "FTRawMatSizeCode"
        Me.FTRawMatSizeCode.OptionsColumn.AllowEdit = False
        Me.FTRawMatSizeCode.OptionsColumn.ReadOnly = True
        Me.FTRawMatSizeCode.Visible = True
        Me.FTRawMatSizeCode.VisibleIndex = 4
        Me.FTRawMatSizeCode.Width = 52
        '
        'FNUsedQuantity
        '
        Me.FNUsedQuantity.AppearanceCell.Options.UseTextOptions = True
        Me.FNUsedQuantity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNUsedQuantity.AppearanceHeader.Options.UseTextOptions = True
        Me.FNUsedQuantity.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FNUsedQuantity.Caption = "FNUsedQuantity"
        Me.FNUsedQuantity.DisplayFormat.FormatString = "{0:n4}"
        Me.FNUsedQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNUsedQuantity.FieldName = "FNUsedQuantity"
        Me.FNUsedQuantity.Name = "FNUsedQuantity"
        Me.FNUsedQuantity.OptionsColumn.AllowEdit = False
        Me.FNUsedQuantity.OptionsColumn.ReadOnly = True
        Me.FNUsedQuantity.Visible = True
        Me.FNUsedQuantity.VisibleIndex = 5
        Me.FNUsedQuantity.Width = 80
        '
        'FNUsedPlusQuantity
        '
        Me.FNUsedPlusQuantity.AppearanceCell.Options.UseTextOptions = True
        Me.FNUsedPlusQuantity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNUsedPlusQuantity.AppearanceHeader.Options.UseTextOptions = True
        Me.FNUsedPlusQuantity.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FNUsedPlusQuantity.Caption = "FNUsedPlusQuantity"
        Me.FNUsedPlusQuantity.DisplayFormat.FormatString = "{0:n4}"
        Me.FNUsedPlusQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNUsedPlusQuantity.FieldName = "FNUsedPlusQuantity"
        Me.FNUsedPlusQuantity.Name = "FNUsedPlusQuantity"
        Me.FNUsedPlusQuantity.OptionsColumn.AllowEdit = False
        Me.FNUsedPlusQuantity.OptionsColumn.ReadOnly = True
        Me.FNUsedPlusQuantity.Visible = True
        Me.FNUsedPlusQuantity.VisibleIndex = 6
        Me.FNUsedPlusQuantity.Width = 80
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
        Me.FTUnitCode.Width = 70
        '
        'FNRSVQuantity
        '
        Me.FNRSVQuantity.AppearanceCell.Options.UseTextOptions = True
        Me.FNRSVQuantity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNRSVQuantity.AppearanceHeader.Options.UseTextOptions = True
        Me.FNRSVQuantity.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FNRSVQuantity.Caption = "FNRSVQuantity"
        Me.FNRSVQuantity.DisplayFormat.FormatString = "{0:n4}"
        Me.FNRSVQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNRSVQuantity.FieldName = "FNRSVQuantity"
        Me.FNRSVQuantity.Name = "FNRSVQuantity"
        Me.FNRSVQuantity.OptionsColumn.AllowEdit = False
        Me.FNRSVQuantity.OptionsColumn.ReadOnly = True
        Me.FNRSVQuantity.Visible = True
        Me.FNRSVQuantity.VisibleIndex = 8
        Me.FNRSVQuantity.Width = 80
        '
        'FNPOQuantity
        '
        Me.FNPOQuantity.AppearanceCell.Options.UseTextOptions = True
        Me.FNPOQuantity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNPOQuantity.AppearanceHeader.Options.UseTextOptions = True
        Me.FNPOQuantity.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FNPOQuantity.Caption = "FNPOQuantity"
        Me.FNPOQuantity.DisplayFormat.FormatString = "{0:n4}"
        Me.FNPOQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNPOQuantity.FieldName = "FNPOQuantity"
        Me.FNPOQuantity.Name = "FNPOQuantity"
        Me.FNPOQuantity.OptionsColumn.AllowEdit = False
        Me.FNPOQuantity.OptionsColumn.ReadOnly = True
        Me.FNPOQuantity.Visible = True
        Me.FNPOQuantity.VisibleIndex = 9
        Me.FNPOQuantity.Width = 80
        '
        'FNRCVQuantity
        '
        Me.FNRCVQuantity.AppearanceCell.Options.UseTextOptions = True
        Me.FNRCVQuantity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNRCVQuantity.AppearanceHeader.Options.UseTextOptions = True
        Me.FNRCVQuantity.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FNRCVQuantity.Caption = "FNRCVQuantity"
        Me.FNRCVQuantity.DisplayFormat.FormatString = "{0:n4}"
        Me.FNRCVQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNRCVQuantity.FieldName = "FNRCVQuantity"
        Me.FNRCVQuantity.Name = "FNRCVQuantity"
        Me.FNRCVQuantity.OptionsColumn.AllowEdit = False
        Me.FNRCVQuantity.OptionsColumn.ReadOnly = True
        Me.FNRCVQuantity.Visible = True
        Me.FNRCVQuantity.VisibleIndex = 10
        Me.FNRCVQuantity.Width = 80
        '
        'FNRTSQuantity
        '
        Me.FNRTSQuantity.AppearanceCell.Options.UseTextOptions = True
        Me.FNRTSQuantity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNRTSQuantity.AppearanceHeader.Options.UseTextOptions = True
        Me.FNRTSQuantity.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FNRTSQuantity.Caption = "FNRTSQuantity"
        Me.FNRTSQuantity.DisplayFormat.FormatString = "{0:n4}"
        Me.FNRTSQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNRTSQuantity.FieldName = "FNRTSQuantity"
        Me.FNRTSQuantity.Name = "FNRTSQuantity"
        Me.FNRTSQuantity.OptionsColumn.AllowEdit = False
        Me.FNRTSQuantity.OptionsColumn.ReadOnly = True
        Me.FNRTSQuantity.Visible = True
        Me.FNRTSQuantity.VisibleIndex = 11
        Me.FNRTSQuantity.Width = 80
        '
        'FNPOBalQuantity
        '
        Me.FNPOBalQuantity.AppearanceCell.Options.UseTextOptions = True
        Me.FNPOBalQuantity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNPOBalQuantity.AppearanceHeader.Options.UseTextOptions = True
        Me.FNPOBalQuantity.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FNPOBalQuantity.Caption = "FNPOBalQuantity"
        Me.FNPOBalQuantity.DisplayFormat.FormatString = "{0:n4}"
        Me.FNPOBalQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNPOBalQuantity.FieldName = "FNPOBalQuantity"
        Me.FNPOBalQuantity.Name = "FNPOBalQuantity"
        Me.FNPOBalQuantity.OptionsColumn.AllowEdit = False
        Me.FNPOBalQuantity.OptionsColumn.ReadOnly = True
        Me.FNPOBalQuantity.Visible = True
        Me.FNPOBalQuantity.VisibleIndex = 12
        Me.FNPOBalQuantity.Width = 80
        '
        'FNRCVStockQuantity
        '
        Me.FNRCVStockQuantity.AppearanceCell.Options.UseTextOptions = True
        Me.FNRCVStockQuantity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNRCVStockQuantity.AppearanceHeader.Options.UseTextOptions = True
        Me.FNRCVStockQuantity.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FNRCVStockQuantity.Caption = "FNRCVStockQuantity"
        Me.FNRCVStockQuantity.DisplayFormat.FormatString = "{0:n4}"
        Me.FNRCVStockQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNRCVStockQuantity.FieldName = "FNRCVStockQuantity"
        Me.FNRCVStockQuantity.Name = "FNRCVStockQuantity"
        Me.FNRCVStockQuantity.OptionsColumn.AllowEdit = False
        Me.FNRCVStockQuantity.OptionsColumn.ReadOnly = True
        Me.FNRCVStockQuantity.Visible = True
        Me.FNRCVStockQuantity.VisibleIndex = 13
        Me.FNRCVStockQuantity.Width = 80
        '
        'FNTROInQuantity
        '
        Me.FNTROInQuantity.AppearanceCell.Options.UseTextOptions = True
        Me.FNTROInQuantity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNTROInQuantity.AppearanceHeader.Options.UseTextOptions = True
        Me.FNTROInQuantity.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FNTROInQuantity.Caption = "FNTROInQuantity"
        Me.FNTROInQuantity.DisplayFormat.FormatString = "{0:n4}"
        Me.FNTROInQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNTROInQuantity.FieldName = "FNTROInQuantity"
        Me.FNTROInQuantity.Name = "FNTROInQuantity"
        Me.FNTROInQuantity.OptionsColumn.AllowEdit = False
        Me.FNTROInQuantity.OptionsColumn.ReadOnly = True
        Me.FNTROInQuantity.Visible = True
        Me.FNTROInQuantity.VisibleIndex = 14
        Me.FNTROInQuantity.Width = 80
        '
        'FNTROOutQuantity
        '
        Me.FNTROOutQuantity.AppearanceCell.Options.UseTextOptions = True
        Me.FNTROOutQuantity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNTROOutQuantity.AppearanceHeader.Options.UseTextOptions = True
        Me.FNTROOutQuantity.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FNTROOutQuantity.Caption = "FNTROOutQuantity"
        Me.FNTROOutQuantity.DisplayFormat.FormatString = "{0:n4}"
        Me.FNTROOutQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNTROOutQuantity.FieldName = "FNTROOutQuantity"
        Me.FNTROOutQuantity.Name = "FNTROOutQuantity"
        Me.FNTROOutQuantity.OptionsColumn.AllowEdit = False
        Me.FNTROOutQuantity.OptionsColumn.ReadOnly = True
        Me.FNTROOutQuantity.Visible = True
        Me.FNTROOutQuantity.VisibleIndex = 15
        Me.FNTROOutQuantity.Width = 80
        '
        'FNISSQuantity
        '
        Me.FNISSQuantity.AppearanceCell.Options.UseTextOptions = True
        Me.FNISSQuantity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNISSQuantity.AppearanceHeader.Options.UseTextOptions = True
        Me.FNISSQuantity.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FNISSQuantity.Caption = "FNISSQuantity"
        Me.FNISSQuantity.DisplayFormat.FormatString = "{0:n4}"
        Me.FNISSQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNISSQuantity.FieldName = "FNISSQuantity"
        Me.FNISSQuantity.Name = "FNISSQuantity"
        Me.FNISSQuantity.OptionsColumn.AllowEdit = False
        Me.FNISSQuantity.OptionsColumn.ReadOnly = True
        Me.FNISSQuantity.Visible = True
        Me.FNISSQuantity.VisibleIndex = 16
        Me.FNISSQuantity.Width = 80
        '
        'FNRETQuantity
        '
        Me.FNRETQuantity.AppearanceCell.Options.UseTextOptions = True
        Me.FNRETQuantity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNRETQuantity.AppearanceHeader.Options.UseTextOptions = True
        Me.FNRETQuantity.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FNRETQuantity.Caption = "FNRETQuantity"
        Me.FNRETQuantity.DisplayFormat.FormatString = "{0:n4}"
        Me.FNRETQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNRETQuantity.FieldName = "FNRETQuantity"
        Me.FNRETQuantity.Name = "FNRETQuantity"
        Me.FNRETQuantity.OptionsColumn.AllowEdit = False
        Me.FNRETQuantity.OptionsColumn.ReadOnly = True
        Me.FNRETQuantity.Visible = True
        Me.FNRETQuantity.VisibleIndex = 17
        Me.FNRETQuantity.Width = 80
        '
        'FNADJInQuantity
        '
        Me.FNADJInQuantity.AppearanceCell.Options.UseTextOptions = True
        Me.FNADJInQuantity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNADJInQuantity.AppearanceHeader.Options.UseTextOptions = True
        Me.FNADJInQuantity.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FNADJInQuantity.Caption = "FNADJInQuantity"
        Me.FNADJInQuantity.DisplayFormat.FormatString = "{0:n4}"
        Me.FNADJInQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNADJInQuantity.FieldName = "FNADJInQuantity"
        Me.FNADJInQuantity.Name = "FNADJInQuantity"
        Me.FNADJInQuantity.OptionsColumn.AllowEdit = False
        Me.FNADJInQuantity.OptionsColumn.ReadOnly = True
        Me.FNADJInQuantity.Visible = True
        Me.FNADJInQuantity.VisibleIndex = 18
        Me.FNADJInQuantity.Width = 80
        '
        'FNADJOutQuantity
        '
        Me.FNADJOutQuantity.AppearanceCell.Options.UseTextOptions = True
        Me.FNADJOutQuantity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNADJOutQuantity.AppearanceHeader.Options.UseTextOptions = True
        Me.FNADJOutQuantity.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FNADJOutQuantity.Caption = "FNADJOutQuantity"
        Me.FNADJOutQuantity.DisplayFormat.FormatString = "{0:n4}"
        Me.FNADJOutQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNADJOutQuantity.FieldName = "FNADJOutQuantity"
        Me.FNADJOutQuantity.Name = "FNADJOutQuantity"
        Me.FNADJOutQuantity.OptionsColumn.AllowEdit = False
        Me.FNADJOutQuantity.OptionsColumn.ReadOnly = True
        Me.FNADJOutQuantity.Visible = True
        Me.FNADJOutQuantity.VisibleIndex = 19
        Me.FNADJOutQuantity.Width = 80
        '
        'FNTRWInQuantity
        '
        Me.FNTRWInQuantity.AppearanceCell.Options.UseTextOptions = True
        Me.FNTRWInQuantity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNTRWInQuantity.AppearanceHeader.Options.UseTextOptions = True
        Me.FNTRWInQuantity.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FNTRWInQuantity.Caption = "FNTRWInQuantity"
        Me.FNTRWInQuantity.DisplayFormat.FormatString = "{0:n4}"
        Me.FNTRWInQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNTRWInQuantity.FieldName = "FNTRWInQuantity"
        Me.FNTRWInQuantity.Name = "FNTRWInQuantity"
        Me.FNTRWInQuantity.OptionsColumn.AllowEdit = False
        Me.FNTRWInQuantity.OptionsColumn.ReadOnly = True
        Me.FNTRWInQuantity.Visible = True
        Me.FNTRWInQuantity.VisibleIndex = 20
        Me.FNTRWInQuantity.Width = 80
        '
        'FNTRWOutQuantity
        '
        Me.FNTRWOutQuantity.AppearanceCell.Options.UseTextOptions = True
        Me.FNTRWOutQuantity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNTRWOutQuantity.AppearanceHeader.Options.UseTextOptions = True
        Me.FNTRWOutQuantity.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FNTRWOutQuantity.Caption = "FNTRWOutQuantity"
        Me.FNTRWOutQuantity.DisplayFormat.FormatString = "{0:n4}"
        Me.FNTRWOutQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNTRWOutQuantity.FieldName = "FNTRWOutQuantity"
        Me.FNTRWOutQuantity.Name = "FNTRWOutQuantity"
        Me.FNTRWOutQuantity.OptionsColumn.AllowEdit = False
        Me.FNTRWOutQuantity.OptionsColumn.ReadOnly = True
        Me.FNTRWOutQuantity.Visible = True
        Me.FNTRWOutQuantity.VisibleIndex = 21
        Me.FNTRWOutQuantity.Width = 80
        '
        'FNSaleQuantity
        '
        Me.FNSaleQuantity.AppearanceCell.Options.UseTextOptions = True
        Me.FNSaleQuantity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNSaleQuantity.AppearanceHeader.Options.UseTextOptions = True
        Me.FNSaleQuantity.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FNSaleQuantity.Caption = "FNSaleQuantity"
        Me.FNSaleQuantity.DisplayFormat.FormatString = "{0:n4}"
        Me.FNSaleQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNSaleQuantity.FieldName = "FNSaleQuantity"
        Me.FNSaleQuantity.Name = "FNSaleQuantity"
        Me.FNSaleQuantity.OptionsColumn.AllowEdit = False
        Me.FNSaleQuantity.OptionsColumn.ReadOnly = True
        Me.FNSaleQuantity.Visible = True
        Me.FNSaleQuantity.VisibleIndex = 22
        Me.FNSaleQuantity.Width = 80
        '
        'FNTerminateQuantity
        '
        Me.FNTerminateQuantity.AppearanceCell.Options.UseTextOptions = True
        Me.FNTerminateQuantity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNTerminateQuantity.AppearanceHeader.Options.UseTextOptions = True
        Me.FNTerminateQuantity.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FNTerminateQuantity.Caption = "FNTerminateQuantity"
        Me.FNTerminateQuantity.DisplayFormat.FormatString = "{0:n4}"
        Me.FNTerminateQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNTerminateQuantity.FieldName = "FNTerminateQuantity"
        Me.FNTerminateQuantity.Name = "FNTerminateQuantity"
        Me.FNTerminateQuantity.OptionsColumn.AllowEdit = False
        Me.FNTerminateQuantity.OptionsColumn.ReadOnly = True
        Me.FNTerminateQuantity.Visible = True
        Me.FNTerminateQuantity.VisibleIndex = 23
        Me.FNTerminateQuantity.Width = 80
        '
        'FNTRCQuantity
        '
        Me.FNTRCQuantity.AppearanceCell.Options.UseTextOptions = True
        Me.FNTRCQuantity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNTRCQuantity.AppearanceHeader.Options.UseTextOptions = True
        Me.FNTRCQuantity.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FNTRCQuantity.Caption = "FNTRCQuantity"
        Me.FNTRCQuantity.DisplayFormat.FormatString = "{0:n4}"
        Me.FNTRCQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNTRCQuantity.FieldName = "FNTRCQuantity"
        Me.FNTRCQuantity.Name = "FNTRCQuantity"
        Me.FNTRCQuantity.OptionsColumn.AllowEdit = False
        Me.FNTRCQuantity.OptionsColumn.ReadOnly = True
        Me.FNTRCQuantity.Visible = True
        Me.FNTRCQuantity.VisibleIndex = 24
        Me.FNTRCQuantity.Width = 80
        '
        'FNRTSAfQuantity
        '
        Me.FNRTSAfQuantity.AppearanceCell.Options.UseTextOptions = True
        Me.FNRTSAfQuantity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNRTSAfQuantity.AppearanceHeader.Options.UseTextOptions = True
        Me.FNRTSAfQuantity.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FNRTSAfQuantity.Caption = "FNRTSAfQuantity"
        Me.FNRTSAfQuantity.DisplayFormat.FormatString = "{0:n4}"
        Me.FNRTSAfQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNRTSAfQuantity.FieldName = "FNRTSAfQuantity"
        Me.FNRTSAfQuantity.Name = "FNRTSAfQuantity"
        Me.FNRTSAfQuantity.OptionsColumn.AllowEdit = False
        Me.FNRTSAfQuantity.OptionsColumn.ReadOnly = True
        Me.FNRTSAfQuantity.Visible = True
        Me.FNRTSAfQuantity.VisibleIndex = 25
        Me.FNRTSAfQuantity.Width = 80
        '
        'FNCNSInQuantity
        '
        Me.FNCNSInQuantity.Caption = "Conversion In"
        Me.FNCNSInQuantity.DisplayFormat.FormatString = "{0:n4}"
        Me.FNCNSInQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNCNSInQuantity.FieldName = "FNCNSInQuantity"
        Me.FNCNSInQuantity.Name = "FNCNSInQuantity"
        Me.FNCNSInQuantity.OptionsColumn.AllowEdit = False
        Me.FNCNSInQuantity.OptionsColumn.ReadOnly = True
        Me.FNCNSInQuantity.Visible = True
        Me.FNCNSInQuantity.VisibleIndex = 26
        '
        'FNCNSOutQuantity
        '
        Me.FNCNSOutQuantity.Caption = "Conversion Out"
        Me.FNCNSOutQuantity.DisplayFormat.FormatString = "{0:n4}"
        Me.FNCNSOutQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNCNSOutQuantity.FieldName = "FNCNSOutQuantity"
        Me.FNCNSOutQuantity.Name = "FNCNSOutQuantity"
        Me.FNCNSOutQuantity.OptionsColumn.AllowEdit = False
        Me.FNCNSOutQuantity.OptionsColumn.ReadOnly = True
        Me.FNCNSOutQuantity.Visible = True
        Me.FNCNSOutQuantity.VisibleIndex = 27
        '
        'FNRSVOutQuantity
        '
        Me.FNRSVOutQuantity.Caption = "Reserve Out"
        Me.FNRSVOutQuantity.DisplayFormat.FormatString = "{0:n4}"
        Me.FNRSVOutQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNRSVOutQuantity.FieldName = "FNRSVOutQuantity"
        Me.FNRSVOutQuantity.Name = "FNRSVOutQuantity"
        Me.FNRSVOutQuantity.OptionsColumn.AllowEdit = False
        Me.FNRSVOutQuantity.OptionsColumn.ReadOnly = True
        Me.FNRSVOutQuantity.Visible = True
        Me.FNRSVOutQuantity.VisibleIndex = 28
        '
        'FNOnhandQuantity
        '
        Me.FNOnhandQuantity.AppearanceCell.Options.UseTextOptions = True
        Me.FNOnhandQuantity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNOnhandQuantity.AppearanceHeader.Options.UseTextOptions = True
        Me.FNOnhandQuantity.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FNOnhandQuantity.Caption = "FNOnhandQuantity"
        Me.FNOnhandQuantity.DisplayFormat.FormatString = "{0:n4}"
        Me.FNOnhandQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNOnhandQuantity.FieldName = "FNOnhandQuantity"
        Me.FNOnhandQuantity.Name = "FNOnhandQuantity"
        Me.FNOnhandQuantity.OptionsColumn.AllowEdit = False
        Me.FNOnhandQuantity.OptionsColumn.ReadOnly = True
        Me.FNOnhandQuantity.Visible = True
        Me.FNOnhandQuantity.VisibleIndex = 29
        Me.FNOnhandQuantity.Width = 80
        '
        'oDockManager
        '
        Me.oDockManager.AutoHideContainers.AddRange(New DevExpress.XtraBars.Docking.AutoHideContainer() {Me.hideContainerTop})
        Me.oDockManager.Form = Me
        Me.oDockManager.TopZIndexControls.AddRange(New String() {"DevExpress.XtraBars.BarDockControl", "DevExpress.XtraBars.StandaloneBarDockControl", "System.Windows.Forms.StatusBar", "DevExpress.XtraBars.Ribbon.RibbonStatusBar", "DevExpress.XtraBars.Ribbon.RibbonControl"})
        '
        'hideContainerTop
        '
        Me.hideContainerTop.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.hideContainerTop.Controls.Add(Me.ogbheader)
        Me.hideContainerTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.hideContainerTop.Location = New System.Drawing.Point(0, 0)
        Me.hideContainerTop.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.hideContainerTop.Name = "hideContainerTop"
        Me.hideContainerTop.Size = New System.Drawing.Size(1137, 38)
        '
        'wINVENOrderTracking
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1137, 633)
        Me.Controls.Add(Me.ogbdetail)
        Me.Controls.Add(Me.hideContainerTop)
        Me.Name = "wINVENOrderTracking"
        Me.Text = "Order Tracking"
        Me.ogbheader.ResumeLayout(False)
        Me.DockPanel1_Container.ResumeLayout(False)
        CType(Me.FTEndShipment.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTEndShipment.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTStartShipment.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTStartShipment.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysStyleId_None.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTOrderNoTo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysBuyId_None.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTOrderNo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysBuyId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysStyleId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogbdetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbdetail.ResumeLayout(False)
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbmainprocbutton.ResumeLayout(False)
        CType(Me.ogdtime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvtime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.oDockManager, System.ComponentModel.ISupportInitialize).EndInit()
        Me.hideContainerTop.ResumeLayout(False)
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
    Friend WithEvents CFTOrderNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTRawMatCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTRawMatName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTRawMatColorCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTRawMatSizeCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNUsedQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNUsedPlusQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTUnitCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNRSVQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNPOQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNRCVQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNRTSQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNPOBalQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNRCVStockQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNTROInQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNTROOutQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNISSQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNRETQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNADJInQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNADJOutQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNTRWInQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNTRWOutQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNSaleQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNTerminateQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNOnhandQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNHSysStyleId_None As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FNHSysBuyId_None As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FNHSysStyleId_lbl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysBuyId As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FNHSysStyleId As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FNHSysBuyId_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTOrderNoTo_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTOrderNoTo As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FTOrderNo_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTOrderNo As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FTEndShipment As DevExpress.XtraEditors.DateEdit
    Friend WithEvents FTEndShipment_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTStartShipment As DevExpress.XtraEditors.DateEdit
    Friend WithEvents FTStartShipment_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNTRCQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNRTSAfQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ocmpreview As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents hideContainerTop As DevExpress.XtraBars.Docking.AutoHideContainer
    Friend WithEvents FNCNSInQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNCNSOutQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNRSVOutQuantity As DevExpress.XtraGrid.Columns.GridColumn
End Class

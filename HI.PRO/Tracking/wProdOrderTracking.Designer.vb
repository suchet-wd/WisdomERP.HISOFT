<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wProdOrderTracking
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(wProdOrderTracking))
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject4 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject5 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject6 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.ogbheader = New DevExpress.XtraBars.Docking.DockPanel()
        Me.DockPanel1_Container = New DevExpress.XtraBars.Docking.ControlContainer()
        Me.FTCustomerPOTo_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTEndShipment = New DevExpress.XtraEditors.DateEdit()
        Me.FTCustomerPOTo = New DevExpress.XtraEditors.ButtonEdit()
        Me.FTEndShipment_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTCustomerPO_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTCustomerPO = New DevExpress.XtraEditors.ButtonEdit()
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
        Me.ogcdetailcolorsizeline = New DevExpress.XtraTab.XtraTabControl()
        Me.otbsummary = New DevExpress.XtraTab.XtraTabPage()
        Me.ogdtime = New DevExpress.XtraGrid.GridControl()
        Me.ogvtime = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.FTPORef = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTStyleCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFTOrderNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTCmpCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTCmpName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FDShipDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNQuantityExtra = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNGarmentQtyTest = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNGrandQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNCutQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNCutBalQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFNQtyEmbroidery = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFNRcvQtyEmbroidery = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNBalQtyEmbroidery = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFNQtyPrint = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFNRcvQtyPrint = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFNBalQtyPrint = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFNQtyHeat = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFNRcvQtyHeat = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFNBalQtyHeat = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFNQtyLaser = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFNRcvQtyLaser = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFNBalQtyLaser = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFNQtyPadPrint = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFNRcvQtyPadPrint = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFNBalQtyPadPrint = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFNQtyWindow = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFNRcvQtyWindow = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFNBalQtyWindow = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNSendSuplQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNRcvSuplQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNBalSuplQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNSPMKQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNBalCutQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNSewQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNSewOutQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNBalSewQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNPackQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNBalPackQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.otbdetailcolorsize = New DevExpress.XtraTab.XtraTabPage()
        Me.ogcdetailcolorsize = New DevExpress.XtraGrid.GridControl()
        Me.ogvdetailcolorsize = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.AFTPORef = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.AFTStyleCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.AFTOrderNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.AFTCmpCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.AFTCmpName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.AFDShipDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.AFTPOLineItemNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTColorway = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTSizeBreakDown = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.AFNQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.AFNQuantityExtra = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.AFNGarmentQtyTest = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.AFNGrandQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.AFNCutQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.AFNCutBalQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.C2FNQtyEmbroidery = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.C2FNRcvQtyEmbroidery = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.F2NBalQtyEmbroidery = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.C2FNQtyPrint = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.C2FNRcvQtyPrint = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.C2FNBalQtyPrint = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.C2FNQtyHeat = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.C2FNRcvQtyHeat = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.C2FNBalQtyHeat = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.C2FNQtyLaser = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.C2FNRcvQtyLaser = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.C2FNBalQtyLaser = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.C2FNQtyPadPrint = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.C2FNRcvQtyPadPrint = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.C2FNBalQtyPadPrint = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.C2FNQtyWindow = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.C2FNRcvQtyWindow = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.C2FNBalQtyWindow = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.AFNSendSuplQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.AFNRcvSuplQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.AFNBalSuplQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.AFNSPMKQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.AFNBalCutQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.AFNSewQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.AFNSewOutQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.AFNBalSewQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.AFNPackQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.AFNBalPackQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.oDockManager = New DevExpress.XtraBars.Docking.DockManager(Me.components)
        Me.ogbmainprocbutton = New DevExpress.XtraEditors.PanelControl()
        Me.ocmsavelayout = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmclear = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmload = New DevExpress.XtraEditors.SimpleButton()
        Me.ogbheader.SuspendLayout()
        Me.DockPanel1_Container.SuspendLayout()
        CType(Me.FTEndShipment.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTEndShipment.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTCustomerPOTo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTCustomerPO.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
        CType(Me.ogcdetailcolorsizeline, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogcdetailcolorsizeline.SuspendLayout()
        Me.otbsummary.SuspendLayout()
        CType(Me.ogdtime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvtime, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.otbdetailcolorsize.SuspendLayout()
        CType(Me.ogcdetailcolorsize, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvdetailcolorsize, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.oDockManager, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbmainprocbutton.SuspendLayout()
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
        Me.ogbheader.OriginalSize = New System.Drawing.Size(1137, 148)
        Me.ogbheader.Size = New System.Drawing.Size(1137, 148)
        Me.ogbheader.TabStop = False
        Me.ogbheader.Text = "Criteria"
        '
        'DockPanel1_Container
        '
        Me.DockPanel1_Container.Controls.Add(Me.FTCustomerPOTo_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FTEndShipment)
        Me.DockPanel1_Container.Controls.Add(Me.FTCustomerPOTo)
        Me.DockPanel1_Container.Controls.Add(Me.FTEndShipment_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FTCustomerPO_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FTCustomerPO)
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
        Me.DockPanel1_Container.Size = New System.Drawing.Size(1129, 120)
        Me.DockPanel1_Container.TabIndex = 0
        '
        'FTCustomerPOTo_lbl
        '
        Me.FTCustomerPOTo_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTCustomerPOTo_lbl.Appearance.Options.UseForeColor = True
        Me.FTCustomerPOTo_lbl.Appearance.Options.UseTextOptions = True
        Me.FTCustomerPOTo_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTCustomerPOTo_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTCustomerPOTo_lbl.Location = New System.Drawing.Point(374, 86)
        Me.FTCustomerPOTo_lbl.Name = "FTCustomerPOTo_lbl"
        Me.FTCustomerPOTo_lbl.Size = New System.Drawing.Size(176, 19)
        Me.FTCustomerPOTo_lbl.TabIndex = 395
        Me.FTCustomerPOTo_lbl.Tag = "2|"
        Me.FTCustomerPOTo_lbl.Text = "FTCustomerPOTo :"
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
        'FTCustomerPOTo
        '
        Me.FTCustomerPOTo.EnterMoveNextControl = True
        Me.FTCustomerPOTo.Location = New System.Drawing.Point(552, 86)
        Me.FTCustomerPOTo.Name = "FTCustomerPOTo"
        Me.FTCustomerPOTo.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FTCustomerPOTo.Properties.Appearance.Options.UseBackColor = True
        Me.FTCustomerPOTo.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FTCustomerPOTo.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FTCustomerPOTo.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FTCustomerPOTo.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FTCustomerPOTo.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FTCustomerPOTo.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FTCustomerPOTo.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FTCustomerPOTo.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FTCustomerPOTo.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FTCustomerPOTo.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FTCustomerPOTo.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FTCustomerPOTo.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FTCustomerPOTo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", 15, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", "340", Nothing, True)})
        Me.FTCustomerPOTo.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.FTCustomerPOTo.Properties.MaxLength = 30
        Me.FTCustomerPOTo.Size = New System.Drawing.Size(130, 20)
        Me.FTCustomerPOTo.TabIndex = 393
        Me.FTCustomerPOTo.Tag = "2|"
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
        Me.FTEndShipment_lbl.Text = "End Shipment:"
        '
        'FTCustomerPO_lbl
        '
        Me.FTCustomerPO_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTCustomerPO_lbl.Appearance.Options.UseForeColor = True
        Me.FTCustomerPO_lbl.Appearance.Options.UseTextOptions = True
        Me.FTCustomerPO_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTCustomerPO_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTCustomerPO_lbl.Location = New System.Drawing.Point(8, 85)
        Me.FTCustomerPO_lbl.Name = "FTCustomerPO_lbl"
        Me.FTCustomerPO_lbl.Size = New System.Drawing.Size(228, 19)
        Me.FTCustomerPO_lbl.TabIndex = 394
        Me.FTCustomerPO_lbl.Tag = "2|"
        Me.FTCustomerPO_lbl.Text = "FTCustomerPO :"
        '
        'FTCustomerPO
        '
        Me.FTCustomerPO.EnterMoveNextControl = True
        Me.FTCustomerPO.Location = New System.Drawing.Point(238, 86)
        Me.FTCustomerPO.Name = "FTCustomerPO"
        Me.FTCustomerPO.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FTCustomerPO.Properties.Appearance.Options.UseBackColor = True
        Me.FTCustomerPO.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FTCustomerPO.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FTCustomerPO.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FTCustomerPO.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FTCustomerPO.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FTCustomerPO.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FTCustomerPO.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FTCustomerPO.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FTCustomerPO.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FTCustomerPO.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FTCustomerPO.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FTCustomerPO.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FTCustomerPO.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", 15, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject2, "", "329", Nothing, True)})
        Me.FTCustomerPO.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.FTCustomerPO.Properties.MaxLength = 30
        Me.FTCustomerPO.Size = New System.Drawing.Size(130, 20)
        Me.FTCustomerPO.TabIndex = 392
        Me.FTCustomerPO.Tag = "2|"
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
        Me.FNHSysStyleId_None.Size = New System.Drawing.Size(504, 20)
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
        Me.FTOrderNoTo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", 15, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject3, "", "219", Nothing, True)})
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
        Me.FNHSysBuyId_None.Size = New System.Drawing.Size(504, 20)
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
        Me.FTOrderNo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", 15, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject4, "", "218", Nothing, True)})
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
        Me.FNHSysBuyId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject5, "", "113", Nothing, True)})
        Me.FNHSysBuyId.Properties.Tag = ""
        Me.FNHSysBuyId.Size = New System.Drawing.Size(130, 20)
        Me.FNHSysBuyId.TabIndex = 288
        Me.FNHSysBuyId.Tag = "2|"
        '
        'FNHSysStyleId
        '
        Me.FNHSysStyleId.Location = New System.Drawing.Point(238, 24)
        Me.FNHSysStyleId.Name = "FNHSysStyleId"
        Me.FNHSysStyleId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject6, "", "89", Nothing, True)})
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
        Me.ogbdetail.Controls.Add(Me.ogcdetailcolorsizeline)
        Me.ogbdetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogbdetail.Location = New System.Drawing.Point(0, 148)
        Me.ogbdetail.Name = "ogbdetail"
        Me.ogbdetail.ShowCaption = False
        Me.ogbdetail.Size = New System.Drawing.Size(1137, 485)
        Me.ogbdetail.TabIndex = 0
        '
        'ogcdetailcolorsizeline
        '
        Me.ogcdetailcolorsizeline.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogcdetailcolorsizeline.Location = New System.Drawing.Point(2, 2)
        Me.ogcdetailcolorsizeline.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.ogcdetailcolorsizeline.Name = "ogcdetailcolorsizeline"
        Me.ogcdetailcolorsizeline.SelectedTabPage = Me.otbsummary
        Me.ogcdetailcolorsizeline.Size = New System.Drawing.Size(1133, 481)
        Me.ogcdetailcolorsizeline.TabIndex = 387
        Me.ogcdetailcolorsizeline.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.otbsummary, Me.otbdetailcolorsize})
        '
        'otbsummary
        '
        Me.otbsummary.Controls.Add(Me.ogdtime)
        Me.otbsummary.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.otbsummary.Name = "otbsummary"
        Me.otbsummary.Size = New System.Drawing.Size(1127, 453)
        Me.otbsummary.Text = "Summary"
        '
        'ogdtime
        '
        Me.ogdtime.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogdtime.Location = New System.Drawing.Point(0, 0)
        Me.ogdtime.MainView = Me.ogvtime
        Me.ogdtime.Name = "ogdtime"
        Me.ogdtime.Size = New System.Drawing.Size(1127, 453)
        Me.ogdtime.TabIndex = 0
        Me.ogdtime.TabStop = False
        Me.ogdtime.Tag = "2|"
        Me.ogdtime.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvtime})
        '
        'ogvtime
        '
        Me.ogvtime.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.FTPORef, Me.FTStyleCode, Me.CFTOrderNo, Me.FTCmpCode, Me.FTCmpName, Me.FDShipDate, Me.FNQuantity, Me.FNQuantityExtra, Me.FNGarmentQtyTest, Me.FNGrandQuantity, Me.FNCutQuantity, Me.FNCutBalQuantity, Me.CFNQtyEmbroidery, Me.CFNRcvQtyEmbroidery, Me.FNBalQtyEmbroidery, Me.CFNQtyPrint, Me.CFNRcvQtyPrint, Me.CFNBalQtyPrint, Me.CFNQtyHeat, Me.CFNRcvQtyHeat, Me.CFNBalQtyHeat, Me.CFNQtyLaser, Me.CFNRcvQtyLaser, Me.CFNBalQtyLaser, Me.CFNQtyPadPrint, Me.CFNRcvQtyPadPrint, Me.CFNBalQtyPadPrint, Me.CFNQtyWindow, Me.CFNRcvQtyWindow, Me.CFNBalQtyWindow, Me.FNSendSuplQuantity, Me.FNRcvSuplQuantity, Me.FNBalSuplQuantity, Me.FNSPMKQuantity, Me.FNBalCutQuantity, Me.FNSewQuantity, Me.FNSewOutQuantity, Me.FNBalSewQuantity, Me.FNPackQuantity, Me.FNBalPackQuantity})
        Me.ogvtime.GridControl = Me.ogdtime
        Me.ogvtime.Name = "ogvtime"
        Me.ogvtime.OptionsCustomization.AllowQuickHideColumns = False
        Me.ogvtime.OptionsView.ColumnAutoWidth = False
        Me.ogvtime.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.ogvtime.OptionsView.ShowGroupPanel = False
        Me.ogvtime.Tag = "2|"
        '
        'FTPORef
        '
        Me.FTPORef.Caption = "FTPORef"
        Me.FTPORef.FieldName = "FTPORef"
        Me.FTPORef.Name = "FTPORef"
        Me.FTPORef.OptionsColumn.AllowEdit = False
        Me.FTPORef.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTPORef.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTPORef.OptionsColumn.ReadOnly = True
        Me.FTPORef.Visible = True
        Me.FTPORef.VisibleIndex = 0
        Me.FTPORef.Width = 91
        '
        'FTStyleCode
        '
        Me.FTStyleCode.AppearanceHeader.Options.UseTextOptions = True
        Me.FTStyleCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTStyleCode.Caption = "FTStyleCode"
        Me.FTStyleCode.FieldName = "FTStyleCode"
        Me.FTStyleCode.Name = "FTStyleCode"
        Me.FTStyleCode.OptionsColumn.AllowEdit = False
        Me.FTStyleCode.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTStyleCode.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTStyleCode.OptionsColumn.ReadOnly = True
        Me.FTStyleCode.Visible = True
        Me.FTStyleCode.VisibleIndex = 1
        '
        'CFTOrderNo
        '
        Me.CFTOrderNo.AppearanceHeader.Options.UseTextOptions = True
        Me.CFTOrderNo.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.CFTOrderNo.Caption = "FTOrderNo"
        Me.CFTOrderNo.FieldName = "FTOrderNo"
        Me.CFTOrderNo.Name = "CFTOrderNo"
        Me.CFTOrderNo.OptionsColumn.AllowEdit = False
        Me.CFTOrderNo.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFTOrderNo.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFTOrderNo.OptionsColumn.ReadOnly = True
        Me.CFTOrderNo.Visible = True
        Me.CFTOrderNo.VisibleIndex = 2
        Me.CFTOrderNo.Width = 127
        '
        'FTCmpCode
        '
        Me.FTCmpCode.Caption = "FTCmpCode"
        Me.FTCmpCode.FieldName = "FTCmpCode"
        Me.FTCmpCode.Name = "FTCmpCode"
        Me.FTCmpCode.OptionsColumn.AllowEdit = False
        Me.FTCmpCode.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTCmpCode.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTCmpCode.OptionsColumn.ReadOnly = True
        Me.FTCmpCode.Visible = True
        Me.FTCmpCode.VisibleIndex = 3
        Me.FTCmpCode.Width = 100
        '
        'FTCmpName
        '
        Me.FTCmpName.Caption = "FTCmpName"
        Me.FTCmpName.FieldName = "FTCmpName"
        Me.FTCmpName.Name = "FTCmpName"
        Me.FTCmpName.OptionsColumn.AllowEdit = False
        Me.FTCmpName.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTCmpName.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTCmpName.OptionsColumn.ReadOnly = True
        Me.FTCmpName.Visible = True
        Me.FTCmpName.VisibleIndex = 4
        Me.FTCmpName.Width = 120
        '
        'FDShipDate
        '
        Me.FDShipDate.AppearanceCell.Options.UseTextOptions = True
        Me.FDShipDate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FDShipDate.Caption = "FDShipDate"
        Me.FDShipDate.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.FDShipDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.FDShipDate.FieldName = "FDShipDate"
        Me.FDShipDate.Name = "FDShipDate"
        Me.FDShipDate.OptionsColumn.AllowEdit = False
        Me.FDShipDate.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.FDShipDate.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FDShipDate.OptionsColumn.ReadOnly = True
        Me.FDShipDate.Visible = True
        Me.FDShipDate.VisibleIndex = 5
        Me.FDShipDate.Width = 80
        '
        'FNQuantity
        '
        Me.FNQuantity.AppearanceCell.Options.UseTextOptions = True
        Me.FNQuantity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNQuantity.AppearanceHeader.Options.UseTextOptions = True
        Me.FNQuantity.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FNQuantity.Caption = "Order Quantity"
        Me.FNQuantity.DisplayFormat.FormatString = "{0:n0}"
        Me.FNQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNQuantity.FieldName = "FNQuantity"
        Me.FNQuantity.Name = "FNQuantity"
        Me.FNQuantity.OptionsColumn.AllowEdit = False
        Me.FNQuantity.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNQuantity.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNQuantity.OptionsColumn.ReadOnly = True
        Me.FNQuantity.Visible = True
        Me.FNQuantity.VisibleIndex = 6
        Me.FNQuantity.Width = 90
        '
        'FNQuantityExtra
        '
        Me.FNQuantityExtra.AppearanceCell.Options.UseTextOptions = True
        Me.FNQuantityExtra.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNQuantityExtra.AppearanceHeader.Options.UseTextOptions = True
        Me.FNQuantityExtra.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FNQuantityExtra.Caption = "Extra"
        Me.FNQuantityExtra.DisplayFormat.FormatString = "{0:n0}"
        Me.FNQuantityExtra.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNQuantityExtra.FieldName = "FNQuantityExtra"
        Me.FNQuantityExtra.Name = "FNQuantityExtra"
        Me.FNQuantityExtra.OptionsColumn.AllowEdit = False
        Me.FNQuantityExtra.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNQuantityExtra.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNQuantityExtra.OptionsColumn.ReadOnly = True
        Me.FNQuantityExtra.Visible = True
        Me.FNQuantityExtra.VisibleIndex = 7
        Me.FNQuantityExtra.Width = 90
        '
        'FNGarmentQtyTest
        '
        Me.FNGarmentQtyTest.AppearanceCell.Options.UseTextOptions = True
        Me.FNGarmentQtyTest.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNGarmentQtyTest.AppearanceHeader.Options.UseTextOptions = True
        Me.FNGarmentQtyTest.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FNGarmentQtyTest.Caption = "Test"
        Me.FNGarmentQtyTest.DisplayFormat.FormatString = "{0:n0}"
        Me.FNGarmentQtyTest.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNGarmentQtyTest.FieldName = "FNGarmentQtyTest"
        Me.FNGarmentQtyTest.Name = "FNGarmentQtyTest"
        Me.FNGarmentQtyTest.OptionsColumn.AllowEdit = False
        Me.FNGarmentQtyTest.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNGarmentQtyTest.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNGarmentQtyTest.OptionsColumn.ReadOnly = True
        Me.FNGarmentQtyTest.Visible = True
        Me.FNGarmentQtyTest.VisibleIndex = 8
        Me.FNGarmentQtyTest.Width = 90
        '
        'FNGrandQuantity
        '
        Me.FNGrandQuantity.AppearanceCell.Options.UseTextOptions = True
        Me.FNGrandQuantity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNGrandQuantity.AppearanceHeader.Options.UseTextOptions = True
        Me.FNGrandQuantity.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FNGrandQuantity.Caption = "Total Quantity"
        Me.FNGrandQuantity.DisplayFormat.FormatString = "{0:n0}"
        Me.FNGrandQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNGrandQuantity.FieldName = "FNGrandQuantity"
        Me.FNGrandQuantity.Name = "FNGrandQuantity"
        Me.FNGrandQuantity.OptionsColumn.AllowEdit = False
        Me.FNGrandQuantity.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNGrandQuantity.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNGrandQuantity.OptionsColumn.ReadOnly = True
        Me.FNGrandQuantity.Visible = True
        Me.FNGrandQuantity.VisibleIndex = 9
        Me.FNGrandQuantity.Width = 90
        '
        'FNCutQuantity
        '
        Me.FNCutQuantity.AppearanceCell.Options.UseTextOptions = True
        Me.FNCutQuantity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNCutQuantity.AppearanceHeader.Options.UseTextOptions = True
        Me.FNCutQuantity.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FNCutQuantity.Caption = "Cut Quantity"
        Me.FNCutQuantity.DisplayFormat.FormatString = "{0:n0}"
        Me.FNCutQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNCutQuantity.FieldName = "FNCutQuantity"
        Me.FNCutQuantity.Name = "FNCutQuantity"
        Me.FNCutQuantity.OptionsColumn.AllowEdit = False
        Me.FNCutQuantity.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNCutQuantity.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNCutQuantity.OptionsColumn.ReadOnly = True
        Me.FNCutQuantity.Visible = True
        Me.FNCutQuantity.VisibleIndex = 10
        Me.FNCutQuantity.Width = 90
        '
        'FNCutBalQuantity
        '
        Me.FNCutBalQuantity.AppearanceCell.Options.UseTextOptions = True
        Me.FNCutBalQuantity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNCutBalQuantity.AppearanceHeader.Options.UseTextOptions = True
        Me.FNCutBalQuantity.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FNCutBalQuantity.Caption = "Bal Cut Quantity"
        Me.FNCutBalQuantity.DisplayFormat.FormatString = "{0:n0}"
        Me.FNCutBalQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNCutBalQuantity.FieldName = "FNCutBalQuantity"
        Me.FNCutBalQuantity.Name = "FNCutBalQuantity"
        Me.FNCutBalQuantity.OptionsColumn.AllowEdit = False
        Me.FNCutBalQuantity.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNCutBalQuantity.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNCutBalQuantity.OptionsColumn.ReadOnly = True
        Me.FNCutBalQuantity.Visible = True
        Me.FNCutBalQuantity.VisibleIndex = 11
        Me.FNCutBalQuantity.Width = 90
        '
        'CFNQtyEmbroidery
        '
        Me.CFNQtyEmbroidery.Caption = "ส่งปัก"
        Me.CFNQtyEmbroidery.DisplayFormat.FormatString = "{0:n0}"
        Me.CFNQtyEmbroidery.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CFNQtyEmbroidery.FieldName = "FNQtyEmbroidery"
        Me.CFNQtyEmbroidery.Name = "CFNQtyEmbroidery"
        Me.CFNQtyEmbroidery.OptionsColumn.AllowEdit = False
        Me.CFNQtyEmbroidery.OptionsColumn.ReadOnly = True
        Me.CFNQtyEmbroidery.Visible = True
        Me.CFNQtyEmbroidery.VisibleIndex = 12
        '
        'CFNRcvQtyEmbroidery
        '
        Me.CFNRcvQtyEmbroidery.Caption = "รับปัก"
        Me.CFNRcvQtyEmbroidery.DisplayFormat.FormatString = "{0:n0}"
        Me.CFNRcvQtyEmbroidery.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CFNRcvQtyEmbroidery.FieldName = "FNRcvQtyEmbroidery"
        Me.CFNRcvQtyEmbroidery.Name = "CFNRcvQtyEmbroidery"
        Me.CFNRcvQtyEmbroidery.OptionsColumn.AllowEdit = False
        Me.CFNRcvQtyEmbroidery.OptionsColumn.ReadOnly = True
        Me.CFNRcvQtyEmbroidery.Visible = True
        Me.CFNRcvQtyEmbroidery.VisibleIndex = 13
        '
        'FNBalQtyEmbroidery
        '
        Me.FNBalQtyEmbroidery.Caption = "ปักคงค้าง"
        Me.FNBalQtyEmbroidery.DisplayFormat.FormatString = "{0:n0}"
        Me.FNBalQtyEmbroidery.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNBalQtyEmbroidery.FieldName = "FNBalQtyEmbroidery"
        Me.FNBalQtyEmbroidery.Name = "FNBalQtyEmbroidery"
        Me.FNBalQtyEmbroidery.OptionsColumn.AllowEdit = False
        Me.FNBalQtyEmbroidery.OptionsColumn.ReadOnly = True
        Me.FNBalQtyEmbroidery.Visible = True
        Me.FNBalQtyEmbroidery.VisibleIndex = 14
        '
        'CFNQtyPrint
        '
        Me.CFNQtyPrint.Caption = "ส่งพิมพ์"
        Me.CFNQtyPrint.DisplayFormat.FormatString = "{0:n0}"
        Me.CFNQtyPrint.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CFNQtyPrint.FieldName = "FNQtyPrint"
        Me.CFNQtyPrint.Name = "CFNQtyPrint"
        Me.CFNQtyPrint.OptionsColumn.AllowEdit = False
        Me.CFNQtyPrint.OptionsColumn.ReadOnly = True
        Me.CFNQtyPrint.Visible = True
        Me.CFNQtyPrint.VisibleIndex = 15
        '
        'CFNRcvQtyPrint
        '
        Me.CFNRcvQtyPrint.Caption = "รับพิมพ์"
        Me.CFNRcvQtyPrint.DisplayFormat.FormatString = "{0:n0}"
        Me.CFNRcvQtyPrint.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CFNRcvQtyPrint.FieldName = "FNRcvQtyPrint"
        Me.CFNRcvQtyPrint.Name = "CFNRcvQtyPrint"
        Me.CFNRcvQtyPrint.OptionsColumn.AllowEdit = False
        Me.CFNRcvQtyPrint.OptionsColumn.ReadOnly = True
        Me.CFNRcvQtyPrint.Visible = True
        Me.CFNRcvQtyPrint.VisibleIndex = 16
        '
        'CFNBalQtyPrint
        '
        Me.CFNBalQtyPrint.Caption = "พิมคงค้าง"
        Me.CFNBalQtyPrint.DisplayFormat.FormatString = "{0:n0}"
        Me.CFNBalQtyPrint.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CFNBalQtyPrint.FieldName = "FNBalQtyPrint"
        Me.CFNBalQtyPrint.Name = "CFNBalQtyPrint"
        Me.CFNBalQtyPrint.OptionsColumn.AllowEdit = False
        Me.CFNBalQtyPrint.OptionsColumn.ReadOnly = True
        Me.CFNBalQtyPrint.Visible = True
        Me.CFNBalQtyPrint.VisibleIndex = 17
        '
        'CFNQtyHeat
        '
        Me.CFNQtyHeat.Caption = "Heat"
        Me.CFNQtyHeat.DisplayFormat.FormatString = "{0:n0}"
        Me.CFNQtyHeat.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CFNQtyHeat.FieldName = "FNQtyHeat"
        Me.CFNQtyHeat.Name = "CFNQtyHeat"
        Me.CFNQtyHeat.OptionsColumn.AllowEdit = False
        Me.CFNQtyHeat.OptionsColumn.ReadOnly = True
        Me.CFNQtyHeat.Visible = True
        Me.CFNQtyHeat.VisibleIndex = 18
        '
        'CFNRcvQtyHeat
        '
        Me.CFNRcvQtyHeat.Caption = "Heat Rcv."
        Me.CFNRcvQtyHeat.DisplayFormat.FormatString = "{0:n0}"
        Me.CFNRcvQtyHeat.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CFNRcvQtyHeat.FieldName = "FNRcvQtyHeat"
        Me.CFNRcvQtyHeat.Name = "CFNRcvQtyHeat"
        Me.CFNRcvQtyHeat.OptionsColumn.AllowEdit = False
        Me.CFNRcvQtyHeat.OptionsColumn.ReadOnly = True
        Me.CFNRcvQtyHeat.Visible = True
        Me.CFNRcvQtyHeat.VisibleIndex = 19
        '
        'CFNBalQtyHeat
        '
        Me.CFNBalQtyHeat.Caption = "Heat Bal."
        Me.CFNBalQtyHeat.DisplayFormat.FormatString = "{0:n0}"
        Me.CFNBalQtyHeat.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CFNBalQtyHeat.FieldName = "FNBalQtyHeat"
        Me.CFNBalQtyHeat.Name = "CFNBalQtyHeat"
        Me.CFNBalQtyHeat.OptionsColumn.AllowEdit = False
        Me.CFNBalQtyHeat.OptionsColumn.ReadOnly = True
        Me.CFNBalQtyHeat.Visible = True
        Me.CFNBalQtyHeat.VisibleIndex = 20
        '
        'CFNQtyLaser
        '
        Me.CFNQtyLaser.Caption = "Laser"
        Me.CFNQtyLaser.DisplayFormat.FormatString = "{0:n0}"
        Me.CFNQtyLaser.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CFNQtyLaser.FieldName = "FNQtyLaser"
        Me.CFNQtyLaser.Name = "CFNQtyLaser"
        Me.CFNQtyLaser.OptionsColumn.AllowEdit = False
        Me.CFNQtyLaser.OptionsColumn.ReadOnly = True
        Me.CFNQtyLaser.Visible = True
        Me.CFNQtyLaser.VisibleIndex = 21
        '
        'CFNRcvQtyLaser
        '
        Me.CFNRcvQtyLaser.Caption = "Laser Rcv."
        Me.CFNRcvQtyLaser.DisplayFormat.FormatString = "{0:n0}"
        Me.CFNRcvQtyLaser.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CFNRcvQtyLaser.FieldName = "FNRcvQtyLaser"
        Me.CFNRcvQtyLaser.Name = "CFNRcvQtyLaser"
        Me.CFNRcvQtyLaser.OptionsColumn.AllowEdit = False
        Me.CFNRcvQtyLaser.OptionsColumn.ReadOnly = True
        Me.CFNRcvQtyLaser.Visible = True
        Me.CFNRcvQtyLaser.VisibleIndex = 22
        '
        'CFNBalQtyLaser
        '
        Me.CFNBalQtyLaser.Caption = "Laser Bal."
        Me.CFNBalQtyLaser.DisplayFormat.FormatString = "{0:n0}"
        Me.CFNBalQtyLaser.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CFNBalQtyLaser.FieldName = "FNBalQtyLaser"
        Me.CFNBalQtyLaser.Name = "CFNBalQtyLaser"
        Me.CFNBalQtyLaser.OptionsColumn.AllowEdit = False
        Me.CFNBalQtyLaser.OptionsColumn.ReadOnly = True
        Me.CFNBalQtyLaser.Visible = True
        Me.CFNBalQtyLaser.VisibleIndex = 23
        '
        'CFNQtyPadPrint
        '
        Me.CFNQtyPadPrint.Caption = "Pad Print"
        Me.CFNQtyPadPrint.DisplayFormat.FormatString = "{0:n0}"
        Me.CFNQtyPadPrint.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CFNQtyPadPrint.FieldName = "FNQtyPadPrint"
        Me.CFNQtyPadPrint.Name = "CFNQtyPadPrint"
        Me.CFNQtyPadPrint.OptionsColumn.AllowEdit = False
        Me.CFNQtyPadPrint.OptionsColumn.ReadOnly = True
        Me.CFNQtyPadPrint.Visible = True
        Me.CFNQtyPadPrint.VisibleIndex = 24
        '
        'CFNRcvQtyPadPrint
        '
        Me.CFNRcvQtyPadPrint.Caption = "Pad Print Rcv."
        Me.CFNRcvQtyPadPrint.DisplayFormat.FormatString = "{0:n0}"
        Me.CFNRcvQtyPadPrint.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CFNRcvQtyPadPrint.FieldName = "FNRcvQtyPadPrint"
        Me.CFNRcvQtyPadPrint.Name = "CFNRcvQtyPadPrint"
        Me.CFNRcvQtyPadPrint.OptionsColumn.AllowEdit = False
        Me.CFNRcvQtyPadPrint.OptionsColumn.ReadOnly = True
        Me.CFNRcvQtyPadPrint.Visible = True
        Me.CFNRcvQtyPadPrint.VisibleIndex = 25
        '
        'CFNBalQtyPadPrint
        '
        Me.CFNBalQtyPadPrint.Caption = "Pad Print Bal."
        Me.CFNBalQtyPadPrint.DisplayFormat.FormatString = "{0:n0}"
        Me.CFNBalQtyPadPrint.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CFNBalQtyPadPrint.FieldName = "FNBalQtyPadPrint"
        Me.CFNBalQtyPadPrint.Name = "CFNBalQtyPadPrint"
        Me.CFNBalQtyPadPrint.OptionsColumn.AllowEdit = False
        Me.CFNBalQtyPadPrint.OptionsColumn.ReadOnly = True
        Me.CFNBalQtyPadPrint.Visible = True
        Me.CFNBalQtyPadPrint.VisibleIndex = 26
        '
        'CFNQtyWindow
        '
        Me.CFNQtyWindow.Caption = "Window"
        Me.CFNQtyWindow.DisplayFormat.FormatString = "{0:n0}"
        Me.CFNQtyWindow.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CFNQtyWindow.FieldName = "FNQtyWindow"
        Me.CFNQtyWindow.Name = "CFNQtyWindow"
        Me.CFNQtyWindow.OptionsColumn.AllowEdit = False
        Me.CFNQtyWindow.OptionsColumn.ReadOnly = True
        Me.CFNQtyWindow.Visible = True
        Me.CFNQtyWindow.VisibleIndex = 27
        '
        'CFNRcvQtyWindow
        '
        Me.CFNRcvQtyWindow.Caption = "Window Rcv."
        Me.CFNRcvQtyWindow.DisplayFormat.FormatString = "{0:n0}"
        Me.CFNRcvQtyWindow.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CFNRcvQtyWindow.FieldName = "FNRcvQtyWindow"
        Me.CFNRcvQtyWindow.Name = "CFNRcvQtyWindow"
        Me.CFNRcvQtyWindow.OptionsColumn.AllowEdit = False
        Me.CFNRcvQtyWindow.OptionsColumn.ReadOnly = True
        Me.CFNRcvQtyWindow.Visible = True
        Me.CFNRcvQtyWindow.VisibleIndex = 28
        '
        'CFNBalQtyWindow
        '
        Me.CFNBalQtyWindow.Caption = "Window Bal"
        Me.CFNBalQtyWindow.DisplayFormat.FormatString = "{0:n0}"
        Me.CFNBalQtyWindow.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CFNBalQtyWindow.FieldName = "FNBalQtyWindow"
        Me.CFNBalQtyWindow.Name = "CFNBalQtyWindow"
        Me.CFNBalQtyWindow.OptionsColumn.AllowEdit = False
        Me.CFNBalQtyWindow.OptionsColumn.ReadOnly = True
        Me.CFNBalQtyWindow.Visible = True
        Me.CFNBalQtyWindow.VisibleIndex = 29
        '
        'FNSendSuplQuantity
        '
        Me.FNSendSuplQuantity.AppearanceCell.Options.UseTextOptions = True
        Me.FNSendSuplQuantity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNSendSuplQuantity.AppearanceHeader.Options.UseTextOptions = True
        Me.FNSendSuplQuantity.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FNSendSuplQuantity.Caption = "Send Supl Quantity"
        Me.FNSendSuplQuantity.DisplayFormat.FormatString = "{0:n0}"
        Me.FNSendSuplQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNSendSuplQuantity.FieldName = "FNSendSuplQuantity"
        Me.FNSendSuplQuantity.Name = "FNSendSuplQuantity"
        Me.FNSendSuplQuantity.OptionsColumn.AllowEdit = False
        Me.FNSendSuplQuantity.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNSendSuplQuantity.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNSendSuplQuantity.OptionsColumn.ReadOnly = True
        Me.FNSendSuplQuantity.Visible = True
        Me.FNSendSuplQuantity.VisibleIndex = 30
        Me.FNSendSuplQuantity.Width = 90
        '
        'FNRcvSuplQuantity
        '
        Me.FNRcvSuplQuantity.AppearanceCell.Options.UseTextOptions = True
        Me.FNRcvSuplQuantity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNRcvSuplQuantity.AppearanceHeader.Options.UseTextOptions = True
        Me.FNRcvSuplQuantity.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FNRcvSuplQuantity.Caption = "Rcv Supl Quantity"
        Me.FNRcvSuplQuantity.DisplayFormat.FormatString = "{0:n0}"
        Me.FNRcvSuplQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNRcvSuplQuantity.FieldName = "FNRcvSuplQuantity"
        Me.FNRcvSuplQuantity.Name = "FNRcvSuplQuantity"
        Me.FNRcvSuplQuantity.OptionsColumn.AllowEdit = False
        Me.FNRcvSuplQuantity.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNRcvSuplQuantity.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNRcvSuplQuantity.OptionsColumn.ReadOnly = True
        Me.FNRcvSuplQuantity.Visible = True
        Me.FNRcvSuplQuantity.VisibleIndex = 31
        Me.FNRcvSuplQuantity.Width = 90
        '
        'FNBalSuplQuantity
        '
        Me.FNBalSuplQuantity.AppearanceCell.Options.UseTextOptions = True
        Me.FNBalSuplQuantity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNBalSuplQuantity.AppearanceHeader.Options.UseTextOptions = True
        Me.FNBalSuplQuantity.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FNBalSuplQuantity.Caption = "Bal Supl Qty"
        Me.FNBalSuplQuantity.DisplayFormat.FormatString = "{0:n0}"
        Me.FNBalSuplQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNBalSuplQuantity.FieldName = "FNBalSuplQuantity"
        Me.FNBalSuplQuantity.Name = "FNBalSuplQuantity"
        Me.FNBalSuplQuantity.OptionsColumn.AllowEdit = False
        Me.FNBalSuplQuantity.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNBalSuplQuantity.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNBalSuplQuantity.OptionsColumn.ReadOnly = True
        Me.FNBalSuplQuantity.Visible = True
        Me.FNBalSuplQuantity.VisibleIndex = 32
        Me.FNBalSuplQuantity.Width = 90
        '
        'FNSPMKQuantity
        '
        Me.FNSPMKQuantity.AppearanceCell.Options.UseTextOptions = True
        Me.FNSPMKQuantity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNSPMKQuantity.AppearanceHeader.Options.UseTextOptions = True
        Me.FNSPMKQuantity.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FNSPMKQuantity.Caption = "Supper Market"
        Me.FNSPMKQuantity.DisplayFormat.FormatString = "{0:n0}"
        Me.FNSPMKQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNSPMKQuantity.FieldName = "FNSPMKQuantity"
        Me.FNSPMKQuantity.Name = "FNSPMKQuantity"
        Me.FNSPMKQuantity.OptionsColumn.AllowEdit = False
        Me.FNSPMKQuantity.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNSPMKQuantity.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNSPMKQuantity.OptionsColumn.ReadOnly = True
        Me.FNSPMKQuantity.Visible = True
        Me.FNSPMKQuantity.VisibleIndex = 33
        Me.FNSPMKQuantity.Width = 90
        '
        'FNBalCutQuantity
        '
        Me.FNBalCutQuantity.AppearanceCell.Options.UseTextOptions = True
        Me.FNBalCutQuantity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNBalCutQuantity.AppearanceHeader.Options.UseTextOptions = True
        Me.FNBalCutQuantity.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FNBalCutQuantity.Caption = "Bal Cut Qty"
        Me.FNBalCutQuantity.DisplayFormat.FormatString = "{0:n0}"
        Me.FNBalCutQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNBalCutQuantity.FieldName = "FNBalCutQuantity"
        Me.FNBalCutQuantity.Name = "FNBalCutQuantity"
        Me.FNBalCutQuantity.OptionsColumn.AllowEdit = False
        Me.FNBalCutQuantity.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNBalCutQuantity.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNBalCutQuantity.OptionsColumn.ReadOnly = True
        Me.FNBalCutQuantity.Visible = True
        Me.FNBalCutQuantity.VisibleIndex = 34
        Me.FNBalCutQuantity.Width = 90
        '
        'FNSewQuantity
        '
        Me.FNSewQuantity.AppearanceCell.Options.UseTextOptions = True
        Me.FNSewQuantity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNSewQuantity.AppearanceHeader.Options.UseTextOptions = True
        Me.FNSewQuantity.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FNSewQuantity.Caption = "เข้าไลน์"
        Me.FNSewQuantity.DisplayFormat.FormatString = "{0:n0}"
        Me.FNSewQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNSewQuantity.FieldName = "FNSewQuantity"
        Me.FNSewQuantity.Name = "FNSewQuantity"
        Me.FNSewQuantity.OptionsColumn.AllowEdit = False
        Me.FNSewQuantity.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNSewQuantity.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNSewQuantity.OptionsColumn.ReadOnly = True
        Me.FNSewQuantity.Visible = True
        Me.FNSewQuantity.VisibleIndex = 35
        Me.FNSewQuantity.Width = 90
        '
        'FNSewOutQuantity
        '
        Me.FNSewOutQuantity.AppearanceCell.Options.UseTextOptions = True
        Me.FNSewOutQuantity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNSewOutQuantity.AppearanceHeader.Options.UseTextOptions = True
        Me.FNSewOutQuantity.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FNSewOutQuantity.Caption = "ท้ายไลน์"
        Me.FNSewOutQuantity.DisplayFormat.FormatString = "{0:n0}"
        Me.FNSewOutQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNSewOutQuantity.FieldName = "FNSewOutQuantity"
        Me.FNSewOutQuantity.Name = "FNSewOutQuantity"
        Me.FNSewOutQuantity.OptionsColumn.AllowEdit = False
        Me.FNSewOutQuantity.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNSewOutQuantity.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNSewOutQuantity.OptionsColumn.ReadOnly = True
        Me.FNSewOutQuantity.Visible = True
        Me.FNSewOutQuantity.VisibleIndex = 36
        Me.FNSewOutQuantity.Width = 90
        '
        'FNBalSewQuantity
        '
        Me.FNBalSewQuantity.AppearanceCell.Options.UseTextOptions = True
        Me.FNBalSewQuantity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNBalSewQuantity.AppearanceHeader.Options.UseTextOptions = True
        Me.FNBalSewQuantity.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FNBalSewQuantity.Caption = "Bal Sew Qty"
        Me.FNBalSewQuantity.DisplayFormat.FormatString = "{0:n0}"
        Me.FNBalSewQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNBalSewQuantity.FieldName = "FNBalSewQuantity"
        Me.FNBalSewQuantity.Name = "FNBalSewQuantity"
        Me.FNBalSewQuantity.OptionsColumn.AllowEdit = False
        Me.FNBalSewQuantity.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNBalSewQuantity.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNBalSewQuantity.OptionsColumn.ReadOnly = True
        Me.FNBalSewQuantity.Visible = True
        Me.FNBalSewQuantity.VisibleIndex = 37
        Me.FNBalSewQuantity.Width = 90
        '
        'FNPackQuantity
        '
        Me.FNPackQuantity.AppearanceCell.Options.UseTextOptions = True
        Me.FNPackQuantity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNPackQuantity.AppearanceHeader.Options.UseTextOptions = True
        Me.FNPackQuantity.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FNPackQuantity.Caption = "Pack Quantity"
        Me.FNPackQuantity.DisplayFormat.FormatString = "{0:n0}"
        Me.FNPackQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNPackQuantity.FieldName = "FNPackQuantity"
        Me.FNPackQuantity.Name = "FNPackQuantity"
        Me.FNPackQuantity.OptionsColumn.AllowEdit = False
        Me.FNPackQuantity.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNPackQuantity.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNPackQuantity.OptionsColumn.ReadOnly = True
        Me.FNPackQuantity.Visible = True
        Me.FNPackQuantity.VisibleIndex = 38
        Me.FNPackQuantity.Width = 90
        '
        'FNBalPackQuantity
        '
        Me.FNBalPackQuantity.AppearanceCell.Options.UseTextOptions = True
        Me.FNBalPackQuantity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNBalPackQuantity.AppearanceHeader.Options.UseTextOptions = True
        Me.FNBalPackQuantity.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FNBalPackQuantity.Caption = "FNBalPackQuantity"
        Me.FNBalPackQuantity.DisplayFormat.FormatString = "{0:n0}"
        Me.FNBalPackQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNBalPackQuantity.FieldName = "FNBalPackQuantity"
        Me.FNBalPackQuantity.Name = "FNBalPackQuantity"
        Me.FNBalPackQuantity.OptionsColumn.AllowEdit = False
        Me.FNBalPackQuantity.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNBalPackQuantity.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNBalPackQuantity.OptionsColumn.ReadOnly = True
        Me.FNBalPackQuantity.Visible = True
        Me.FNBalPackQuantity.VisibleIndex = 39
        Me.FNBalPackQuantity.Width = 90
        '
        'otbdetailcolorsize
        '
        Me.otbdetailcolorsize.Controls.Add(Me.ogcdetailcolorsize)
        Me.otbdetailcolorsize.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.otbdetailcolorsize.Name = "otbdetailcolorsize"
        Me.otbdetailcolorsize.Size = New System.Drawing.Size(1127, 452)
        Me.otbdetailcolorsize.Text = "Detail By Color And Size"
        '
        'ogcdetailcolorsize
        '
        Me.ogcdetailcolorsize.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogcdetailcolorsize.Location = New System.Drawing.Point(0, 0)
        Me.ogcdetailcolorsize.MainView = Me.ogvdetailcolorsize
        Me.ogcdetailcolorsize.Name = "ogcdetailcolorsize"
        Me.ogcdetailcolorsize.Size = New System.Drawing.Size(1127, 452)
        Me.ogcdetailcolorsize.TabIndex = 1
        Me.ogcdetailcolorsize.TabStop = False
        Me.ogcdetailcolorsize.Tag = "2|"
        Me.ogcdetailcolorsize.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvdetailcolorsize})
        '
        'ogvdetailcolorsize
        '
        Me.ogvdetailcolorsize.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.AFTPORef, Me.AFTStyleCode, Me.AFTOrderNo, Me.AFTCmpCode, Me.AFTCmpName, Me.AFDShipDate, Me.AFTPOLineItemNo, Me.FTColorway, Me.FTSizeBreakDown, Me.AFNQuantity, Me.AFNQuantityExtra, Me.AFNGarmentQtyTest, Me.AFNGrandQuantity, Me.AFNCutQuantity, Me.AFNCutBalQuantity, Me.C2FNQtyEmbroidery, Me.C2FNRcvQtyEmbroidery, Me.F2NBalQtyEmbroidery, Me.C2FNQtyPrint, Me.C2FNRcvQtyPrint, Me.C2FNBalQtyPrint, Me.C2FNQtyHeat, Me.C2FNRcvQtyHeat, Me.C2FNBalQtyHeat, Me.C2FNQtyLaser, Me.C2FNRcvQtyLaser, Me.C2FNBalQtyLaser, Me.C2FNQtyPadPrint, Me.C2FNRcvQtyPadPrint, Me.C2FNBalQtyPadPrint, Me.C2FNQtyWindow, Me.C2FNRcvQtyWindow, Me.C2FNBalQtyWindow, Me.AFNSendSuplQuantity, Me.AFNRcvSuplQuantity, Me.AFNBalSuplQuantity, Me.AFNSPMKQuantity, Me.AFNBalCutQuantity, Me.AFNSewQuantity, Me.AFNSewOutQuantity, Me.AFNBalSewQuantity, Me.AFNPackQuantity, Me.AFNBalPackQuantity})
        Me.ogvdetailcolorsize.GridControl = Me.ogcdetailcolorsize
        Me.ogvdetailcolorsize.Name = "ogvdetailcolorsize"
        Me.ogvdetailcolorsize.OptionsCustomization.AllowQuickHideColumns = False
        Me.ogvdetailcolorsize.OptionsView.ColumnAutoWidth = False
        Me.ogvdetailcolorsize.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.ogvdetailcolorsize.OptionsView.ShowGroupPanel = False
        Me.ogvdetailcolorsize.Tag = "2|"
        '
        'AFTPORef
        '
        Me.AFTPORef.Caption = "FTPORef"
        Me.AFTPORef.FieldName = "FTPORef"
        Me.AFTPORef.Name = "AFTPORef"
        Me.AFTPORef.OptionsColumn.AllowEdit = False
        Me.AFTPORef.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.AFTPORef.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.AFTPORef.OptionsColumn.ReadOnly = True
        Me.AFTPORef.Visible = True
        Me.AFTPORef.VisibleIndex = 0
        Me.AFTPORef.Width = 83
        '
        'AFTStyleCode
        '
        Me.AFTStyleCode.AppearanceHeader.Options.UseTextOptions = True
        Me.AFTStyleCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.AFTStyleCode.Caption = "FTStyleCode"
        Me.AFTStyleCode.FieldName = "FTStyleCode"
        Me.AFTStyleCode.Name = "AFTStyleCode"
        Me.AFTStyleCode.OptionsColumn.AllowEdit = False
        Me.AFTStyleCode.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.AFTStyleCode.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.AFTStyleCode.OptionsColumn.ReadOnly = True
        Me.AFTStyleCode.Visible = True
        Me.AFTStyleCode.VisibleIndex = 1
        '
        'AFTOrderNo
        '
        Me.AFTOrderNo.AppearanceHeader.Options.UseTextOptions = True
        Me.AFTOrderNo.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.AFTOrderNo.Caption = "FTOrderNo"
        Me.AFTOrderNo.FieldName = "FTOrderNo"
        Me.AFTOrderNo.Name = "AFTOrderNo"
        Me.AFTOrderNo.OptionsColumn.AllowEdit = False
        Me.AFTOrderNo.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.AFTOrderNo.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.AFTOrderNo.OptionsColumn.ReadOnly = True
        Me.AFTOrderNo.OptionsColumn.ShowInCustomizationForm = False
        Me.AFTOrderNo.Visible = True
        Me.AFTOrderNo.VisibleIndex = 2
        Me.AFTOrderNo.Width = 127
        '
        'AFTCmpCode
        '
        Me.AFTCmpCode.Caption = "FTCmpCode"
        Me.AFTCmpCode.FieldName = "FTCmpCode"
        Me.AFTCmpCode.Name = "AFTCmpCode"
        Me.AFTCmpCode.OptionsColumn.AllowEdit = False
        Me.AFTCmpCode.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.AFTCmpCode.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.AFTCmpCode.OptionsColumn.ReadOnly = True
        Me.AFTCmpCode.OptionsColumn.ShowInCustomizationForm = False
        Me.AFTCmpCode.Visible = True
        Me.AFTCmpCode.VisibleIndex = 3
        Me.AFTCmpCode.Width = 100
        '
        'AFTCmpName
        '
        Me.AFTCmpName.Caption = "FTCmpName"
        Me.AFTCmpName.FieldName = "FTCmpName"
        Me.AFTCmpName.Name = "AFTCmpName"
        Me.AFTCmpName.OptionsColumn.AllowEdit = False
        Me.AFTCmpName.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.AFTCmpName.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.AFTCmpName.OptionsColumn.ReadOnly = True
        Me.AFTCmpName.OptionsColumn.ShowInCustomizationForm = False
        Me.AFTCmpName.Visible = True
        Me.AFTCmpName.VisibleIndex = 4
        Me.AFTCmpName.Width = 120
        '
        'AFDShipDate
        '
        Me.AFDShipDate.AppearanceCell.Options.UseTextOptions = True
        Me.AFDShipDate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.AFDShipDate.Caption = "FDShipDate"
        Me.AFDShipDate.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.AFDShipDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.AFDShipDate.FieldName = "FDShipDate"
        Me.AFDShipDate.Name = "AFDShipDate"
        Me.AFDShipDate.OptionsColumn.AllowEdit = False
        Me.AFDShipDate.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.AFDShipDate.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.AFDShipDate.OptionsColumn.ReadOnly = True
        Me.AFDShipDate.OptionsColumn.ShowInCustomizationForm = False
        Me.AFDShipDate.Visible = True
        Me.AFDShipDate.VisibleIndex = 5
        Me.AFDShipDate.Width = 80
        '
        'AFTPOLineItemNo
        '
        Me.AFTPOLineItemNo.Caption = "FTPOLineItemNo"
        Me.AFTPOLineItemNo.FieldName = "FTPOLineItemNo"
        Me.AFTPOLineItemNo.Name = "AFTPOLineItemNo"
        Me.AFTPOLineItemNo.OptionsColumn.AllowEdit = False
        Me.AFTPOLineItemNo.OptionsColumn.ReadOnly = True
        Me.AFTPOLineItemNo.OptionsColumn.ShowInCustomizationForm = False
        Me.AFTPOLineItemNo.Visible = True
        Me.AFTPOLineItemNo.VisibleIndex = 6
        Me.AFTPOLineItemNo.Width = 80
        '
        'FTColorway
        '
        Me.FTColorway.Caption = "FTColorway"
        Me.FTColorway.FieldName = "FTColorway"
        Me.FTColorway.Name = "FTColorway"
        Me.FTColorway.OptionsColumn.AllowEdit = False
        Me.FTColorway.OptionsColumn.ReadOnly = True
        Me.FTColorway.OptionsColumn.ShowInCustomizationForm = False
        Me.FTColorway.Visible = True
        Me.FTColorway.VisibleIndex = 7
        Me.FTColorway.Width = 80
        '
        'FTSizeBreakDown
        '
        Me.FTSizeBreakDown.Caption = "FTSizeBreakDown"
        Me.FTSizeBreakDown.FieldName = "FTSizeBreakDown"
        Me.FTSizeBreakDown.Name = "FTSizeBreakDown"
        Me.FTSizeBreakDown.OptionsColumn.AllowEdit = False
        Me.FTSizeBreakDown.OptionsColumn.ReadOnly = True
        Me.FTSizeBreakDown.OptionsColumn.ShowInCustomizationForm = False
        Me.FTSizeBreakDown.Visible = True
        Me.FTSizeBreakDown.VisibleIndex = 8
        Me.FTSizeBreakDown.Width = 60
        '
        'AFNQuantity
        '
        Me.AFNQuantity.AppearanceCell.Options.UseTextOptions = True
        Me.AFNQuantity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.AFNQuantity.AppearanceHeader.Options.UseTextOptions = True
        Me.AFNQuantity.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.AFNQuantity.Caption = "Order Quantity"
        Me.AFNQuantity.DisplayFormat.FormatString = "{0:n0}"
        Me.AFNQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.AFNQuantity.FieldName = "FNQuantity"
        Me.AFNQuantity.Name = "AFNQuantity"
        Me.AFNQuantity.OptionsColumn.AllowEdit = False
        Me.AFNQuantity.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.AFNQuantity.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.AFNQuantity.OptionsColumn.ReadOnly = True
        Me.AFNQuantity.OptionsColumn.ShowInCustomizationForm = False
        Me.AFNQuantity.Visible = True
        Me.AFNQuantity.VisibleIndex = 9
        Me.AFNQuantity.Width = 90
        '
        'AFNQuantityExtra
        '
        Me.AFNQuantityExtra.AppearanceCell.Options.UseTextOptions = True
        Me.AFNQuantityExtra.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.AFNQuantityExtra.AppearanceHeader.Options.UseTextOptions = True
        Me.AFNQuantityExtra.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.AFNQuantityExtra.Caption = "Extra"
        Me.AFNQuantityExtra.DisplayFormat.FormatString = "{0:n0}"
        Me.AFNQuantityExtra.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.AFNQuantityExtra.FieldName = "FNQuantityExtra"
        Me.AFNQuantityExtra.Name = "AFNQuantityExtra"
        Me.AFNQuantityExtra.OptionsColumn.AllowEdit = False
        Me.AFNQuantityExtra.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.AFNQuantityExtra.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.AFNQuantityExtra.OptionsColumn.ReadOnly = True
        Me.AFNQuantityExtra.OptionsColumn.ShowInCustomizationForm = False
        Me.AFNQuantityExtra.Visible = True
        Me.AFNQuantityExtra.VisibleIndex = 10
        Me.AFNQuantityExtra.Width = 90
        '
        'AFNGarmentQtyTest
        '
        Me.AFNGarmentQtyTest.AppearanceCell.Options.UseTextOptions = True
        Me.AFNGarmentQtyTest.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.AFNGarmentQtyTest.AppearanceHeader.Options.UseTextOptions = True
        Me.AFNGarmentQtyTest.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.AFNGarmentQtyTest.Caption = "Test"
        Me.AFNGarmentQtyTest.DisplayFormat.FormatString = "{0:n0}"
        Me.AFNGarmentQtyTest.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.AFNGarmentQtyTest.FieldName = "FNGarmentQtyTest"
        Me.AFNGarmentQtyTest.Name = "AFNGarmentQtyTest"
        Me.AFNGarmentQtyTest.OptionsColumn.AllowEdit = False
        Me.AFNGarmentQtyTest.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.AFNGarmentQtyTest.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.AFNGarmentQtyTest.OptionsColumn.ReadOnly = True
        Me.AFNGarmentQtyTest.OptionsColumn.ShowInCustomizationForm = False
        Me.AFNGarmentQtyTest.Visible = True
        Me.AFNGarmentQtyTest.VisibleIndex = 11
        Me.AFNGarmentQtyTest.Width = 90
        '
        'AFNGrandQuantity
        '
        Me.AFNGrandQuantity.AppearanceCell.Options.UseTextOptions = True
        Me.AFNGrandQuantity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.AFNGrandQuantity.AppearanceHeader.Options.UseTextOptions = True
        Me.AFNGrandQuantity.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.AFNGrandQuantity.Caption = "Total Quantity"
        Me.AFNGrandQuantity.DisplayFormat.FormatString = "{0:n0}"
        Me.AFNGrandQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.AFNGrandQuantity.FieldName = "FNGrandQuantity"
        Me.AFNGrandQuantity.Name = "AFNGrandQuantity"
        Me.AFNGrandQuantity.OptionsColumn.AllowEdit = False
        Me.AFNGrandQuantity.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.AFNGrandQuantity.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.AFNGrandQuantity.OptionsColumn.ReadOnly = True
        Me.AFNGrandQuantity.OptionsColumn.ShowInCustomizationForm = False
        Me.AFNGrandQuantity.Visible = True
        Me.AFNGrandQuantity.VisibleIndex = 12
        Me.AFNGrandQuantity.Width = 90
        '
        'AFNCutQuantity
        '
        Me.AFNCutQuantity.AppearanceCell.Options.UseTextOptions = True
        Me.AFNCutQuantity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.AFNCutQuantity.AppearanceHeader.Options.UseTextOptions = True
        Me.AFNCutQuantity.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.AFNCutQuantity.Caption = "Cut Quantity"
        Me.AFNCutQuantity.DisplayFormat.FormatString = "{0:n0}"
        Me.AFNCutQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.AFNCutQuantity.FieldName = "FNCutQuantity"
        Me.AFNCutQuantity.Name = "AFNCutQuantity"
        Me.AFNCutQuantity.OptionsColumn.AllowEdit = False
        Me.AFNCutQuantity.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.AFNCutQuantity.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.AFNCutQuantity.OptionsColumn.ReadOnly = True
        Me.AFNCutQuantity.OptionsColumn.ShowInCustomizationForm = False
        Me.AFNCutQuantity.Visible = True
        Me.AFNCutQuantity.VisibleIndex = 13
        Me.AFNCutQuantity.Width = 90
        '
        'AFNCutBalQuantity
        '
        Me.AFNCutBalQuantity.AppearanceCell.Options.UseTextOptions = True
        Me.AFNCutBalQuantity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.AFNCutBalQuantity.AppearanceHeader.Options.UseTextOptions = True
        Me.AFNCutBalQuantity.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.AFNCutBalQuantity.Caption = "Bal Cut Quantity"
        Me.AFNCutBalQuantity.DisplayFormat.FormatString = "{0:n0}"
        Me.AFNCutBalQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.AFNCutBalQuantity.FieldName = "FNCutBalQuantity"
        Me.AFNCutBalQuantity.Name = "AFNCutBalQuantity"
        Me.AFNCutBalQuantity.OptionsColumn.AllowEdit = False
        Me.AFNCutBalQuantity.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.AFNCutBalQuantity.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.AFNCutBalQuantity.OptionsColumn.ReadOnly = True
        Me.AFNCutBalQuantity.OptionsColumn.ShowInCustomizationForm = False
        Me.AFNCutBalQuantity.Visible = True
        Me.AFNCutBalQuantity.VisibleIndex = 14
        Me.AFNCutBalQuantity.Width = 90
        '
        'C2FNQtyEmbroidery
        '
        Me.C2FNQtyEmbroidery.Caption = "ส่งปัก"
        Me.C2FNQtyEmbroidery.DisplayFormat.FormatString = "{0:n0}"
        Me.C2FNQtyEmbroidery.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.C2FNQtyEmbroidery.FieldName = "FNQtyEmbroidery"
        Me.C2FNQtyEmbroidery.Name = "C2FNQtyEmbroidery"
        Me.C2FNQtyEmbroidery.OptionsColumn.AllowEdit = False
        Me.C2FNQtyEmbroidery.OptionsColumn.ReadOnly = True
        Me.C2FNQtyEmbroidery.Visible = True
        Me.C2FNQtyEmbroidery.VisibleIndex = 15
        '
        'C2FNRcvQtyEmbroidery
        '
        Me.C2FNRcvQtyEmbroidery.Caption = "รับปัก"
        Me.C2FNRcvQtyEmbroidery.DisplayFormat.FormatString = "{0:n0}"
        Me.C2FNRcvQtyEmbroidery.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.C2FNRcvQtyEmbroidery.FieldName = "FNRcvQtyEmbroidery"
        Me.C2FNRcvQtyEmbroidery.Name = "C2FNRcvQtyEmbroidery"
        Me.C2FNRcvQtyEmbroidery.OptionsColumn.AllowEdit = False
        Me.C2FNRcvQtyEmbroidery.OptionsColumn.ReadOnly = True
        Me.C2FNRcvQtyEmbroidery.Visible = True
        Me.C2FNRcvQtyEmbroidery.VisibleIndex = 16
        '
        'F2NBalQtyEmbroidery
        '
        Me.F2NBalQtyEmbroidery.Caption = "ปักคงค้าง"
        Me.F2NBalQtyEmbroidery.DisplayFormat.FormatString = "{0:n0}"
        Me.F2NBalQtyEmbroidery.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.F2NBalQtyEmbroidery.FieldName = "FNBalQtyEmbroidery"
        Me.F2NBalQtyEmbroidery.Name = "F2NBalQtyEmbroidery"
        Me.F2NBalQtyEmbroidery.OptionsColumn.AllowEdit = False
        Me.F2NBalQtyEmbroidery.OptionsColumn.ReadOnly = True
        Me.F2NBalQtyEmbroidery.Visible = True
        Me.F2NBalQtyEmbroidery.VisibleIndex = 17
        '
        'C2FNQtyPrint
        '
        Me.C2FNQtyPrint.Caption = "ส่งพิมพ์"
        Me.C2FNQtyPrint.DisplayFormat.FormatString = "{0:n0}"
        Me.C2FNQtyPrint.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.C2FNQtyPrint.FieldName = "FNQtyPrint"
        Me.C2FNQtyPrint.Name = "C2FNQtyPrint"
        Me.C2FNQtyPrint.OptionsColumn.AllowEdit = False
        Me.C2FNQtyPrint.OptionsColumn.ReadOnly = True
        Me.C2FNQtyPrint.Visible = True
        Me.C2FNQtyPrint.VisibleIndex = 18
        '
        'C2FNRcvQtyPrint
        '
        Me.C2FNRcvQtyPrint.Caption = "รับพิมพ์"
        Me.C2FNRcvQtyPrint.DisplayFormat.FormatString = "{0:n0}"
        Me.C2FNRcvQtyPrint.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.C2FNRcvQtyPrint.FieldName = "FNRcvQtyPrint"
        Me.C2FNRcvQtyPrint.Name = "C2FNRcvQtyPrint"
        Me.C2FNRcvQtyPrint.OptionsColumn.AllowEdit = False
        Me.C2FNRcvQtyPrint.OptionsColumn.ReadOnly = True
        Me.C2FNRcvQtyPrint.Visible = True
        Me.C2FNRcvQtyPrint.VisibleIndex = 19
        '
        'C2FNBalQtyPrint
        '
        Me.C2FNBalQtyPrint.Caption = "พิมคงค้าง"
        Me.C2FNBalQtyPrint.DisplayFormat.FormatString = "{0:n0}"
        Me.C2FNBalQtyPrint.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.C2FNBalQtyPrint.FieldName = "FNBalQtyPrint"
        Me.C2FNBalQtyPrint.Name = "C2FNBalQtyPrint"
        Me.C2FNBalQtyPrint.OptionsColumn.AllowEdit = False
        Me.C2FNBalQtyPrint.OptionsColumn.ReadOnly = True
        Me.C2FNBalQtyPrint.Visible = True
        Me.C2FNBalQtyPrint.VisibleIndex = 20
        '
        'C2FNQtyHeat
        '
        Me.C2FNQtyHeat.Caption = "Heat"
        Me.C2FNQtyHeat.DisplayFormat.FormatString = "{0:n0}"
        Me.C2FNQtyHeat.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.C2FNQtyHeat.FieldName = "FNQtyHeat"
        Me.C2FNQtyHeat.Name = "C2FNQtyHeat"
        Me.C2FNQtyHeat.OptionsColumn.AllowEdit = False
        Me.C2FNQtyHeat.OptionsColumn.ReadOnly = True
        Me.C2FNQtyHeat.Visible = True
        Me.C2FNQtyHeat.VisibleIndex = 21
        '
        'C2FNRcvQtyHeat
        '
        Me.C2FNRcvQtyHeat.Caption = "Heat Rcv."
        Me.C2FNRcvQtyHeat.DisplayFormat.FormatString = "{0:n0}"
        Me.C2FNRcvQtyHeat.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.C2FNRcvQtyHeat.FieldName = "FNRcvQtyHeat"
        Me.C2FNRcvQtyHeat.Name = "C2FNRcvQtyHeat"
        Me.C2FNRcvQtyHeat.OptionsColumn.AllowEdit = False
        Me.C2FNRcvQtyHeat.OptionsColumn.ReadOnly = True
        Me.C2FNRcvQtyHeat.Visible = True
        Me.C2FNRcvQtyHeat.VisibleIndex = 22
        '
        'C2FNBalQtyHeat
        '
        Me.C2FNBalQtyHeat.Caption = "Heat Bal."
        Me.C2FNBalQtyHeat.DisplayFormat.FormatString = "{0:n0}"
        Me.C2FNBalQtyHeat.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.C2FNBalQtyHeat.FieldName = "FNBalQtyHeat"
        Me.C2FNBalQtyHeat.Name = "C2FNBalQtyHeat"
        Me.C2FNBalQtyHeat.OptionsColumn.AllowEdit = False
        Me.C2FNBalQtyHeat.OptionsColumn.ReadOnly = True
        Me.C2FNBalQtyHeat.Visible = True
        Me.C2FNBalQtyHeat.VisibleIndex = 23
        '
        'C2FNQtyLaser
        '
        Me.C2FNQtyLaser.Caption = "Laser"
        Me.C2FNQtyLaser.DisplayFormat.FormatString = "{0:n0}"
        Me.C2FNQtyLaser.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.C2FNQtyLaser.FieldName = "FNQtyLaser"
        Me.C2FNQtyLaser.Name = "C2FNQtyLaser"
        Me.C2FNQtyLaser.OptionsColumn.AllowEdit = False
        Me.C2FNQtyLaser.OptionsColumn.ReadOnly = True
        Me.C2FNQtyLaser.Visible = True
        Me.C2FNQtyLaser.VisibleIndex = 24
        '
        'C2FNRcvQtyLaser
        '
        Me.C2FNRcvQtyLaser.Caption = "Laser Rcv."
        Me.C2FNRcvQtyLaser.DisplayFormat.FormatString = "{0:n0}"
        Me.C2FNRcvQtyLaser.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.C2FNRcvQtyLaser.FieldName = "FNRcvQtyLaser"
        Me.C2FNRcvQtyLaser.Name = "C2FNRcvQtyLaser"
        Me.C2FNRcvQtyLaser.OptionsColumn.AllowEdit = False
        Me.C2FNRcvQtyLaser.OptionsColumn.ReadOnly = True
        Me.C2FNRcvQtyLaser.Visible = True
        Me.C2FNRcvQtyLaser.VisibleIndex = 25
        '
        'C2FNBalQtyLaser
        '
        Me.C2FNBalQtyLaser.Caption = "Laser Bal."
        Me.C2FNBalQtyLaser.DisplayFormat.FormatString = "{0:n0}"
        Me.C2FNBalQtyLaser.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.C2FNBalQtyLaser.FieldName = "FNBalQtyLaser"
        Me.C2FNBalQtyLaser.Name = "C2FNBalQtyLaser"
        Me.C2FNBalQtyLaser.OptionsColumn.AllowEdit = False
        Me.C2FNBalQtyLaser.OptionsColumn.ReadOnly = True
        Me.C2FNBalQtyLaser.Visible = True
        Me.C2FNBalQtyLaser.VisibleIndex = 26
        '
        'C2FNQtyPadPrint
        '
        Me.C2FNQtyPadPrint.Caption = "Pad Print"
        Me.C2FNQtyPadPrint.DisplayFormat.FormatString = "{0:n0}"
        Me.C2FNQtyPadPrint.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.C2FNQtyPadPrint.FieldName = "FNQtyPadPrint"
        Me.C2FNQtyPadPrint.Name = "C2FNQtyPadPrint"
        Me.C2FNQtyPadPrint.OptionsColumn.AllowEdit = False
        Me.C2FNQtyPadPrint.OptionsColumn.ReadOnly = True
        Me.C2FNQtyPadPrint.Visible = True
        Me.C2FNQtyPadPrint.VisibleIndex = 27
        '
        'C2FNRcvQtyPadPrint
        '
        Me.C2FNRcvQtyPadPrint.FieldName = "FNRcvQtyPadPrint"
        Me.C2FNRcvQtyPadPrint.Name = "C2FNRcvQtyPadPrint"
        Me.C2FNRcvQtyPadPrint.OptionsColumn.AllowEdit = False
        Me.C2FNRcvQtyPadPrint.OptionsColumn.ReadOnly = True
        Me.C2FNRcvQtyPadPrint.Visible = True
        Me.C2FNRcvQtyPadPrint.VisibleIndex = 29
        '
        'C2FNBalQtyPadPrint
        '
        Me.C2FNBalQtyPadPrint.Caption = "Pad Print Rcv."
        Me.C2FNBalQtyPadPrint.DisplayFormat.FormatString = "{0:n0}"
        Me.C2FNBalQtyPadPrint.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.C2FNBalQtyPadPrint.FieldName = "FNBalQtyPadPrint"
        Me.C2FNBalQtyPadPrint.Name = "C2FNBalQtyPadPrint"
        Me.C2FNBalQtyPadPrint.OptionsColumn.AllowEdit = False
        Me.C2FNBalQtyPadPrint.OptionsColumn.ReadOnly = True
        Me.C2FNBalQtyPadPrint.Visible = True
        Me.C2FNBalQtyPadPrint.VisibleIndex = 28
        '
        'C2FNQtyWindow
        '
        Me.C2FNQtyWindow.Caption = "Window"
        Me.C2FNQtyWindow.DisplayFormat.FormatString = "{0:n0}"
        Me.C2FNQtyWindow.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.C2FNQtyWindow.FieldName = "FNQtyWindow"
        Me.C2FNQtyWindow.Name = "C2FNQtyWindow"
        Me.C2FNQtyWindow.OptionsColumn.AllowEdit = False
        Me.C2FNQtyWindow.OptionsColumn.ReadOnly = True
        Me.C2FNQtyWindow.Visible = True
        Me.C2FNQtyWindow.VisibleIndex = 30
        '
        'C2FNRcvQtyWindow
        '
        Me.C2FNRcvQtyWindow.Caption = "Window Rcv."
        Me.C2FNRcvQtyWindow.DisplayFormat.FormatString = "{0:n0}"
        Me.C2FNRcvQtyWindow.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.C2FNRcvQtyWindow.FieldName = "FNRcvQtyWindow"
        Me.C2FNRcvQtyWindow.Name = "C2FNRcvQtyWindow"
        Me.C2FNRcvQtyWindow.OptionsColumn.AllowEdit = False
        Me.C2FNRcvQtyWindow.OptionsColumn.ReadOnly = True
        Me.C2FNRcvQtyWindow.Visible = True
        Me.C2FNRcvQtyWindow.VisibleIndex = 31
        '
        'C2FNBalQtyWindow
        '
        Me.C2FNBalQtyWindow.Caption = "Window Bal"
        Me.C2FNBalQtyWindow.DisplayFormat.FormatString = "{0:n0}"
        Me.C2FNBalQtyWindow.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.C2FNBalQtyWindow.FieldName = "FNBalQtyWindow"
        Me.C2FNBalQtyWindow.Name = "C2FNBalQtyWindow"
        Me.C2FNBalQtyWindow.OptionsColumn.AllowEdit = False
        Me.C2FNBalQtyWindow.OptionsColumn.ReadOnly = True
        Me.C2FNBalQtyWindow.Visible = True
        Me.C2FNBalQtyWindow.VisibleIndex = 32
        '
        'AFNSendSuplQuantity
        '
        Me.AFNSendSuplQuantity.AppearanceCell.Options.UseTextOptions = True
        Me.AFNSendSuplQuantity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.AFNSendSuplQuantity.AppearanceHeader.Options.UseTextOptions = True
        Me.AFNSendSuplQuantity.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.AFNSendSuplQuantity.Caption = "Send Supl Quantity"
        Me.AFNSendSuplQuantity.DisplayFormat.FormatString = "{0:n0}"
        Me.AFNSendSuplQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.AFNSendSuplQuantity.FieldName = "FNSendSuplQuantity"
        Me.AFNSendSuplQuantity.Name = "AFNSendSuplQuantity"
        Me.AFNSendSuplQuantity.OptionsColumn.AllowEdit = False
        Me.AFNSendSuplQuantity.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.AFNSendSuplQuantity.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.AFNSendSuplQuantity.OptionsColumn.ReadOnly = True
        Me.AFNSendSuplQuantity.OptionsColumn.ShowInCustomizationForm = False
        Me.AFNSendSuplQuantity.Visible = True
        Me.AFNSendSuplQuantity.VisibleIndex = 33
        Me.AFNSendSuplQuantity.Width = 90
        '
        'AFNRcvSuplQuantity
        '
        Me.AFNRcvSuplQuantity.AppearanceCell.Options.UseTextOptions = True
        Me.AFNRcvSuplQuantity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.AFNRcvSuplQuantity.AppearanceHeader.Options.UseTextOptions = True
        Me.AFNRcvSuplQuantity.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.AFNRcvSuplQuantity.Caption = "Rcv Supl Quantity"
        Me.AFNRcvSuplQuantity.DisplayFormat.FormatString = "{0:n0}"
        Me.AFNRcvSuplQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.AFNRcvSuplQuantity.FieldName = "FNRcvSuplQuantity"
        Me.AFNRcvSuplQuantity.Name = "AFNRcvSuplQuantity"
        Me.AFNRcvSuplQuantity.OptionsColumn.AllowEdit = False
        Me.AFNRcvSuplQuantity.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.AFNRcvSuplQuantity.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.AFNRcvSuplQuantity.OptionsColumn.ReadOnly = True
        Me.AFNRcvSuplQuantity.OptionsColumn.ShowInCustomizationForm = False
        Me.AFNRcvSuplQuantity.Visible = True
        Me.AFNRcvSuplQuantity.VisibleIndex = 34
        Me.AFNRcvSuplQuantity.Width = 90
        '
        'AFNBalSuplQuantity
        '
        Me.AFNBalSuplQuantity.AppearanceCell.Options.UseTextOptions = True
        Me.AFNBalSuplQuantity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.AFNBalSuplQuantity.AppearanceHeader.Options.UseTextOptions = True
        Me.AFNBalSuplQuantity.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.AFNBalSuplQuantity.Caption = "Bal Supl Qty"
        Me.AFNBalSuplQuantity.DisplayFormat.FormatString = "{0:n0}"
        Me.AFNBalSuplQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.AFNBalSuplQuantity.FieldName = "FNBalSuplQuantity"
        Me.AFNBalSuplQuantity.Name = "AFNBalSuplQuantity"
        Me.AFNBalSuplQuantity.OptionsColumn.AllowEdit = False
        Me.AFNBalSuplQuantity.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.AFNBalSuplQuantity.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.AFNBalSuplQuantity.OptionsColumn.ReadOnly = True
        Me.AFNBalSuplQuantity.OptionsColumn.ShowInCustomizationForm = False
        Me.AFNBalSuplQuantity.Visible = True
        Me.AFNBalSuplQuantity.VisibleIndex = 35
        Me.AFNBalSuplQuantity.Width = 90
        '
        'AFNSPMKQuantity
        '
        Me.AFNSPMKQuantity.AppearanceCell.Options.UseTextOptions = True
        Me.AFNSPMKQuantity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.AFNSPMKQuantity.AppearanceHeader.Options.UseTextOptions = True
        Me.AFNSPMKQuantity.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.AFNSPMKQuantity.Caption = "Supper Market"
        Me.AFNSPMKQuantity.DisplayFormat.FormatString = "{0:n0}"
        Me.AFNSPMKQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.AFNSPMKQuantity.FieldName = "FNSPMKQuantity"
        Me.AFNSPMKQuantity.Name = "AFNSPMKQuantity"
        Me.AFNSPMKQuantity.OptionsColumn.AllowEdit = False
        Me.AFNSPMKQuantity.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.AFNSPMKQuantity.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.AFNSPMKQuantity.OptionsColumn.ReadOnly = True
        Me.AFNSPMKQuantity.OptionsColumn.ShowInCustomizationForm = False
        Me.AFNSPMKQuantity.Visible = True
        Me.AFNSPMKQuantity.VisibleIndex = 36
        Me.AFNSPMKQuantity.Width = 90
        '
        'AFNBalCutQuantity
        '
        Me.AFNBalCutQuantity.AppearanceCell.Options.UseTextOptions = True
        Me.AFNBalCutQuantity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.AFNBalCutQuantity.AppearanceHeader.Options.UseTextOptions = True
        Me.AFNBalCutQuantity.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.AFNBalCutQuantity.Caption = "Bal Cut Qty"
        Me.AFNBalCutQuantity.DisplayFormat.FormatString = "{0:n0}"
        Me.AFNBalCutQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.AFNBalCutQuantity.FieldName = "FNBalCutQuantity"
        Me.AFNBalCutQuantity.Name = "AFNBalCutQuantity"
        Me.AFNBalCutQuantity.OptionsColumn.AllowEdit = False
        Me.AFNBalCutQuantity.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.AFNBalCutQuantity.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.AFNBalCutQuantity.OptionsColumn.ReadOnly = True
        Me.AFNBalCutQuantity.OptionsColumn.ShowInCustomizationForm = False
        Me.AFNBalCutQuantity.Visible = True
        Me.AFNBalCutQuantity.VisibleIndex = 37
        Me.AFNBalCutQuantity.Width = 90
        '
        'AFNSewQuantity
        '
        Me.AFNSewQuantity.AppearanceCell.Options.UseTextOptions = True
        Me.AFNSewQuantity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.AFNSewQuantity.AppearanceHeader.Options.UseTextOptions = True
        Me.AFNSewQuantity.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.AFNSewQuantity.Caption = "เข้าไลน์"
        Me.AFNSewQuantity.DisplayFormat.FormatString = "{0:n0}"
        Me.AFNSewQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.AFNSewQuantity.FieldName = "FNSewQuantity"
        Me.AFNSewQuantity.Name = "AFNSewQuantity"
        Me.AFNSewQuantity.OptionsColumn.AllowEdit = False
        Me.AFNSewQuantity.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.AFNSewQuantity.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.AFNSewQuantity.OptionsColumn.ReadOnly = True
        Me.AFNSewQuantity.OptionsColumn.ShowInCustomizationForm = False
        Me.AFNSewQuantity.Visible = True
        Me.AFNSewQuantity.VisibleIndex = 38
        Me.AFNSewQuantity.Width = 90
        '
        'AFNSewOutQuantity
        '
        Me.AFNSewOutQuantity.AppearanceCell.Options.UseTextOptions = True
        Me.AFNSewOutQuantity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.AFNSewOutQuantity.AppearanceHeader.Options.UseTextOptions = True
        Me.AFNSewOutQuantity.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.AFNSewOutQuantity.Caption = "ท้ายไลน์"
        Me.AFNSewOutQuantity.DisplayFormat.FormatString = "{0:n0}"
        Me.AFNSewOutQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.AFNSewOutQuantity.FieldName = "FNSewOutQuantity"
        Me.AFNSewOutQuantity.Name = "AFNSewOutQuantity"
        Me.AFNSewOutQuantity.OptionsColumn.AllowEdit = False
        Me.AFNSewOutQuantity.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.AFNSewOutQuantity.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.AFNSewOutQuantity.OptionsColumn.ReadOnly = True
        Me.AFNSewOutQuantity.OptionsColumn.ShowInCustomizationForm = False
        Me.AFNSewOutQuantity.Visible = True
        Me.AFNSewOutQuantity.VisibleIndex = 39
        Me.AFNSewOutQuantity.Width = 90
        '
        'AFNBalSewQuantity
        '
        Me.AFNBalSewQuantity.AppearanceCell.Options.UseTextOptions = True
        Me.AFNBalSewQuantity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.AFNBalSewQuantity.AppearanceHeader.Options.UseTextOptions = True
        Me.AFNBalSewQuantity.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.AFNBalSewQuantity.Caption = "Bal Sew Qty"
        Me.AFNBalSewQuantity.DisplayFormat.FormatString = "{0:n0}"
        Me.AFNBalSewQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.AFNBalSewQuantity.FieldName = "FNBalSewQuantity"
        Me.AFNBalSewQuantity.Name = "AFNBalSewQuantity"
        Me.AFNBalSewQuantity.OptionsColumn.AllowEdit = False
        Me.AFNBalSewQuantity.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.AFNBalSewQuantity.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.AFNBalSewQuantity.OptionsColumn.ReadOnly = True
        Me.AFNBalSewQuantity.OptionsColumn.ShowInCustomizationForm = False
        Me.AFNBalSewQuantity.Visible = True
        Me.AFNBalSewQuantity.VisibleIndex = 40
        Me.AFNBalSewQuantity.Width = 90
        '
        'AFNPackQuantity
        '
        Me.AFNPackQuantity.AppearanceCell.Options.UseTextOptions = True
        Me.AFNPackQuantity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.AFNPackQuantity.AppearanceHeader.Options.UseTextOptions = True
        Me.AFNPackQuantity.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.AFNPackQuantity.Caption = "Pack Quantity"
        Me.AFNPackQuantity.DisplayFormat.FormatString = "{0:n0}"
        Me.AFNPackQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.AFNPackQuantity.FieldName = "FNPackQuantity"
        Me.AFNPackQuantity.Name = "AFNPackQuantity"
        Me.AFNPackQuantity.OptionsColumn.AllowEdit = False
        Me.AFNPackQuantity.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.AFNPackQuantity.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.AFNPackQuantity.OptionsColumn.ReadOnly = True
        Me.AFNPackQuantity.OptionsColumn.ShowInCustomizationForm = False
        Me.AFNPackQuantity.Visible = True
        Me.AFNPackQuantity.VisibleIndex = 41
        Me.AFNPackQuantity.Width = 90
        '
        'AFNBalPackQuantity
        '
        Me.AFNBalPackQuantity.AppearanceCell.Options.UseTextOptions = True
        Me.AFNBalPackQuantity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.AFNBalPackQuantity.AppearanceHeader.Options.UseTextOptions = True
        Me.AFNBalPackQuantity.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.AFNBalPackQuantity.Caption = "FNBalPackQuantity"
        Me.AFNBalPackQuantity.DisplayFormat.FormatString = "{0:n0}"
        Me.AFNBalPackQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.AFNBalPackQuantity.FieldName = "FNBalPackQuantity"
        Me.AFNBalPackQuantity.Name = "AFNBalPackQuantity"
        Me.AFNBalPackQuantity.OptionsColumn.AllowEdit = False
        Me.AFNBalPackQuantity.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.AFNBalPackQuantity.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.AFNBalPackQuantity.OptionsColumn.ReadOnly = True
        Me.AFNBalPackQuantity.OptionsColumn.ShowInCustomizationForm = False
        Me.AFNBalPackQuantity.Visible = True
        Me.AFNBalPackQuantity.VisibleIndex = 42
        Me.AFNBalPackQuantity.Width = 90
        '
        'oDockManager
        '
        Me.oDockManager.Form = Me
        Me.oDockManager.RootPanels.AddRange(New DevExpress.XtraBars.Docking.DockPanel() {Me.ogbheader})
        Me.oDockManager.TopZIndexControls.AddRange(New String() {"DevExpress.XtraBars.BarDockControl", "DevExpress.XtraBars.StandaloneBarDockControl", "System.Windows.Forms.StatusBar", "DevExpress.XtraBars.Ribbon.RibbonStatusBar", "DevExpress.XtraBars.Ribbon.RibbonControl"})
        '
        'ogbmainprocbutton
        '
        Me.ogbmainprocbutton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmsavelayout)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmexit)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmclear)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmload)
        Me.ogbmainprocbutton.Location = New System.Drawing.Point(157, 292)
        Me.ogbmainprocbutton.Name = "ogbmainprocbutton"
        Me.ogbmainprocbutton.Size = New System.Drawing.Size(824, 47)
        Me.ogbmainprocbutton.TabIndex = 387
        Me.ogbmainprocbutton.Tag = "2|"
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
        Me.ocmexit.Location = New System.Drawing.Point(711, 11)
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
        Me.ocmload.Location = New System.Drawing.Point(114, 14)
        Me.ocmload.Name = "ocmload"
        Me.ocmload.Size = New System.Drawing.Size(117, 23)
        Me.ocmload.TabIndex = 329
        Me.ocmload.Text = "Load Data"
        '
        'wProdOrderTracking
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1137, 633)
        Me.Controls.Add(Me.ogbmainprocbutton)
        Me.Controls.Add(Me.ogbdetail)
        Me.Controls.Add(Me.ogbheader)
        Me.Name = "wProdOrderTracking"
        Me.Text = "Production Tracking"
        Me.ogbheader.ResumeLayout(False)
        Me.DockPanel1_Container.ResumeLayout(False)
        CType(Me.FTEndShipment.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTEndShipment.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTCustomerPOTo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTCustomerPO.Properties, System.ComponentModel.ISupportInitialize).EndInit()
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
        CType(Me.ogcdetailcolorsizeline, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogcdetailcolorsizeline.ResumeLayout(False)
        Me.otbsummary.ResumeLayout(False)
        CType(Me.ogdtime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvtime, System.ComponentModel.ISupportInitialize).EndInit()
        Me.otbdetailcolorsize.ResumeLayout(False)
        CType(Me.ogcdetailcolorsize, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvdetailcolorsize, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.oDockManager, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbmainprocbutton.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogbdetail As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ogdtime As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvtime As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents oDockManager As DevExpress.XtraBars.Docking.DockManager
    Friend WithEvents ogbheader As DevExpress.XtraBars.Docking.DockPanel
    Friend WithEvents DockPanel1_Container As DevExpress.XtraBars.Docking.ControlContainer
    Friend WithEvents CFTOrderNo As DevExpress.XtraGrid.Columns.GridColumn
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
    Friend WithEvents FNQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNQuantityExtra As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNGarmentQtyTest As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNGrandQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNCutQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNSendSuplQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNRcvSuplQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNSewQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNPackQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FDShipDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNSPMKQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNSewOutQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNBalSuplQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNBalCutQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNBalSewQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNBalPackQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNCutBalQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTCmpCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTCmpName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ogcdetailcolorsizeline As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents otbsummary As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents otbdetailcolorsize As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents ogbmainprocbutton As DevExpress.XtraEditors.PanelControl
    Friend WithEvents ocmsavelayout As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmclear As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmload As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ogcdetailcolorsize As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvdetailcolorsize As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents AFTOrderNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents AFTCmpCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents AFTCmpName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents AFDShipDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents AFNQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents AFNQuantityExtra As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents AFNGarmentQtyTest As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents AFNGrandQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents AFNCutQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents AFNCutBalQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents AFNSendSuplQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents AFNRcvSuplQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents AFNBalSuplQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents AFNSPMKQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents AFNBalCutQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents AFNSewQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents AFNSewOutQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents AFNBalSewQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents AFNPackQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents AFNBalPackQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTColorway As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTSizeBreakDown As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents AFTPOLineItemNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTPORef As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents AFTPORef As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTCustomerPOTo_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTCustomerPOTo As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FTCustomerPO_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTCustomerPO As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FTStyleCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents AFTStyleCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFNQtyEmbroidery As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFNRcvQtyEmbroidery As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNBalQtyEmbroidery As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFNQtyPrint As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFNRcvQtyPrint As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFNBalQtyPrint As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFNQtyHeat As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFNRcvQtyHeat As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFNBalQtyHeat As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFNQtyLaser As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFNRcvQtyLaser As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFNBalQtyLaser As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFNQtyPadPrint As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFNRcvQtyPadPrint As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFNBalQtyPadPrint As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFNQtyWindow As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFNRcvQtyWindow As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFNBalQtyWindow As DevExpress.XtraGrid.Columns.GridColumn

    Friend WithEvents C2FNQtyEmbroidery As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents C2FNRcvQtyEmbroidery As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents F2NBalQtyEmbroidery As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents C2FNQtyPrint As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents C2FNRcvQtyPrint As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents C2FNBalQtyPrint As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents C2FNQtyHeat As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents C2FNRcvQtyHeat As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents C2FNBalQtyHeat As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents C2FNQtyLaser As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents C2FNRcvQtyLaser As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents C2FNBalQtyLaser As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents C2FNQtyPadPrint As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents C2FNRcvQtyPadPrint As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents C2FNBalQtyPadPrint As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents C2FNQtyWindow As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents C2FNRcvQtyWindow As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents C2FNBalQtyWindow As DevExpress.XtraGrid.Columns.GridColumn

End Class

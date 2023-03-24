<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wProdOrderTrackingByLineDaily_CD
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(wProdOrderTrackingByLineDaily_CD))
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
        Me.ogbheader = New DevExpress.XtraBars.Docking.DockPanel()
        Me.DockPanel1_Container = New DevExpress.XtraBars.Docking.ControlContainer()
        Me.FTEndDate = New DevExpress.XtraEditors.DateEdit()
        Me.FTSSMKDate = New DevExpress.XtraEditors.DateEdit()
        Me.FTEndDate_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTSSMKDate1_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTStartDate = New DevExpress.XtraEditors.DateEdit()
        Me.FTStart_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTOrderNoTo_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTOrderNoTo = New DevExpress.XtraEditors.ButtonEdit()
        Me.FTOrderNo_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTOrderNo = New DevExpress.XtraEditors.ButtonEdit()
        Me.ogbdetail = New DevExpress.XtraEditors.GroupControl()
        Me.ogcdetailcolorsizeline = New DevExpress.XtraTab.XtraTabControl()
        Me.otbdetailcolorsizeline = New DevExpress.XtraTab.XtraTabPage()
        Me.ogcdetailcolorsizelineg = New DevExpress.XtraGrid.GridControl()
        Me.ogvdetailcolorsizelineg = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.FTStyleCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.SFTOrderNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTPORef = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.SFTCmpCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.SFTCmpName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.SFDShipDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.SFTPOLineItemNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.SFTColorway = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.SFTSizeBreakDown = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.SFNQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.SFNQuantityExtra = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.SFNGarmentQtyTest = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.SFNGrandQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTUnitSectCodeCut = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.SFNCutQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.SFNCutBalQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
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
        Me.SFNSendSuplQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.SFNRcvSuplQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.SFNBalSuplQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.SFNSPMKQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.SFNBalSPMKQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTUnitSectCodeSew = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.SFNSewQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CXFNSPMKQuantityBal = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.SFNSewOutQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.SFNBalSewQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.SFNPackQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.SFNBalPackQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNTableCut = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNFGInQty = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNFGBalQty = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNExpQty = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNExpBalQty = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.XtraTabPage1 = New DevExpress.XtraTab.XtraTabPage()
        Me.ogcmonthly = New DevExpress.XtraGrid.GridControl()
        Me.ogvmonthly = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.cFTMonthly = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTStyleCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTOrderNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTPORef = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTCmpCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.mFTCmpName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.mFDShipDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.mFTPOLineItemNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.mFTColorway = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.mFTSizeBreakDown = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.mFNQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNOrderOnhand = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.mFNQuantityExtra = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.mFNGarmentQtyTest = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.mFNGrandQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn14 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.mFNCutQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNCutBFQty = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.mFNCutBalQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNQtyEmbroideryBF = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.mFNQtyEmbroidery = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.mFNRcvQtyEmbroidery = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.mFNBalQtyEmbroidery = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNQtyPrintBF = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.mFNQtyPrint = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.mFNRcvQtyPrint = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.mFNBalQtyPrint = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNQtyHeatBF = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.mFNQtyHeat = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.mFNRcvQtyHeat = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.mFNBalQtyHeat = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNQtyLaserBF = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.mFNQtyLaser = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.mFNRcvQtyLaser = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.mFNBalQtyLaser = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNQtyPadPrintBF = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.mFNQtyPadPrint = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.mFNRcvQtyPadPrint = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.mFNBalQtyPadPrint = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNSendSuplQuantityBF = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.mFNSendSuplQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.mFNRcvSuplQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.mFNBalSuplQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNSPMKQuantityBF = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.mFNSPMKQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNBalSPMKQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn40 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNSewQuantityBF = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.mFNSewQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.mFNSPMKQuantityBal = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.mFNSewOutQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.mFNBalSewQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNSewOutQuantityBF = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.mFNPackQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.mFNBalPackQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn47 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.mFNFGInQty = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.mFNFGBalQty = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNFGInQtyBF = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.mFNExpQty = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.mFNExpBalQty = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.oDockManager = New DevExpress.XtraBars.Docking.DockManager(Me.components)
        Me.ogbmainprocbutton = New DevExpress.XtraEditors.PanelControl()
        Me.ocmsavelayout = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmclear = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmexporttoexcel = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmload = New DevExpress.XtraEditors.SimpleButton()
        Me.ogbheader.SuspendLayout()
        Me.DockPanel1_Container.SuspendLayout()
        CType(Me.FTEndDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTEndDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTSSMKDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTSSMKDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTStartDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTStartDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTOrderNoTo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTOrderNo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogbdetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbdetail.SuspendLayout()
        CType(Me.ogcdetailcolorsizeline, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogcdetailcolorsizeline.SuspendLayout()
        Me.otbdetailcolorsizeline.SuspendLayout()
        CType(Me.ogcdetailcolorsizelineg, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvdetailcolorsizelineg, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XtraTabPage1.SuspendLayout()
        CType(Me.ogcmonthly, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvmonthly, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.ogbheader.ImageOptions.Image = CType(resources.GetObject("ogbheader.ImageOptions.Image"), System.Drawing.Image)
        Me.ogbheader.Location = New System.Drawing.Point(0, 0)
        Me.ogbheader.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbheader.Name = "ogbheader"
        Me.ogbheader.Options.AllowDockBottom = False
        Me.ogbheader.Options.AllowDockFill = False
        Me.ogbheader.Options.FloatOnDblClick = False
        Me.ogbheader.Options.ShowCloseButton = False
        Me.ogbheader.Options.ShowMaximizeButton = False
        Me.ogbheader.OriginalSize = New System.Drawing.Size(1326, 151)
        Me.ogbheader.Size = New System.Drawing.Size(1326, 151)
        Me.ogbheader.TabStop = False
        Me.ogbheader.Text = "Criteria"
        '
        'DockPanel1_Container
        '
        Me.DockPanel1_Container.Controls.Add(Me.FTEndDate)
        Me.DockPanel1_Container.Controls.Add(Me.FTSSMKDate)
        Me.DockPanel1_Container.Controls.Add(Me.FTEndDate_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FTSSMKDate1_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FTStartDate)
        Me.DockPanel1_Container.Controls.Add(Me.FTStart_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FTOrderNoTo_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FTOrderNoTo)
        Me.DockPanel1_Container.Controls.Add(Me.FTOrderNo_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FTOrderNo)
        Me.DockPanel1_Container.Location = New System.Drawing.Point(4, 32)
        Me.DockPanel1_Container.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.DockPanel1_Container.Name = "DockPanel1_Container"
        Me.DockPanel1_Container.Size = New System.Drawing.Size(1318, 113)
        Me.DockPanel1_Container.TabIndex = 0
        '
        'FTEndDate
        '
        Me.FTEndDate.EditValue = Nothing
        Me.FTEndDate.EnterMoveNextControl = True
        Me.FTEndDate.Location = New System.Drawing.Point(522, 31)
        Me.FTEndDate.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTEndDate.Name = "FTEndDate"
        Me.FTEndDate.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTEndDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FTEndDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.FTEndDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTEndDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTEndDate.Properties.NullDate = ""
        Me.FTEndDate.Size = New System.Drawing.Size(105, 22)
        Me.FTEndDate.TabIndex = 397
        Me.FTEndDate.Tag = "2|"
        '
        'FTSSMKDate
        '
        Me.FTSSMKDate.EditValue = Nothing
        Me.FTSSMKDate.EnterMoveNextControl = True
        Me.FTSSMKDate.Location = New System.Drawing.Point(263, 4)
        Me.FTSSMKDate.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTSSMKDate.Name = "FTSSMKDate"
        Me.FTSSMKDate.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTSSMKDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FTSSMKDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.FTSSMKDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTSSMKDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTSSMKDate.Properties.NullDate = ""
        Me.FTSSMKDate.Properties.ReadOnly = True
        Me.FTSSMKDate.Size = New System.Drawing.Size(105, 22)
        Me.FTSSMKDate.TabIndex = 6
        Me.FTSSMKDate.Tag = "2|"
        '
        'FTEndDate_lbl
        '
        Me.FTEndDate_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTEndDate_lbl.Appearance.Options.UseForeColor = True
        Me.FTEndDate_lbl.Appearance.Options.UseTextOptions = True
        Me.FTEndDate_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTEndDate_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTEndDate_lbl.Location = New System.Drawing.Point(368, 30)
        Me.FTEndDate_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTEndDate_lbl.Name = "FTEndDate_lbl"
        Me.FTEndDate_lbl.Size = New System.Drawing.Size(152, 23)
        Me.FTEndDate_lbl.TabIndex = 399
        Me.FTEndDate_lbl.Tag = "2|"
        Me.FTEndDate_lbl.Text = "End Date :"
        '
        'FTSSMKDate1_lbl
        '
        Me.FTSSMKDate1_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTSSMKDate1_lbl.Appearance.Options.UseForeColor = True
        Me.FTSSMKDate1_lbl.Appearance.Options.UseTextOptions = True
        Me.FTSSMKDate1_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTSSMKDate1_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTSSMKDate1_lbl.Location = New System.Drawing.Point(74, 4)
        Me.FTSSMKDate1_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTSSMKDate1_lbl.Name = "FTSSMKDate1_lbl"
        Me.FTSSMKDate1_lbl.Size = New System.Drawing.Size(187, 23)
        Me.FTSSMKDate1_lbl.TabIndex = 394
        Me.FTSSMKDate1_lbl.Tag = "2|"
        Me.FTSSMKDate1_lbl.Text = "As Of Date"
        '
        'FTStartDate
        '
        Me.FTStartDate.EditValue = Nothing
        Me.FTStartDate.EnterMoveNextControl = True
        Me.FTStartDate.Location = New System.Drawing.Point(263, 31)
        Me.FTStartDate.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTStartDate.Name = "FTStartDate"
        Me.FTStartDate.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTStartDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FTStartDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.FTStartDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTStartDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTStartDate.Properties.NullDate = ""
        Me.FTStartDate.Size = New System.Drawing.Size(105, 22)
        Me.FTStartDate.TabIndex = 396
        Me.FTStartDate.Tag = "2|"
        '
        'FTStart_lbl
        '
        Me.FTStart_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTStart_lbl.Appearance.Options.UseForeColor = True
        Me.FTStart_lbl.Appearance.Options.UseTextOptions = True
        Me.FTStart_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTStart_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTStart_lbl.Location = New System.Drawing.Point(74, 30)
        Me.FTStart_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTStart_lbl.Name = "FTStart_lbl"
        Me.FTStart_lbl.Size = New System.Drawing.Size(187, 23)
        Me.FTStart_lbl.TabIndex = 398
        Me.FTStart_lbl.Tag = "2|"
        Me.FTStart_lbl.Text = "Start Date:"
        '
        'FTOrderNoTo_lbl
        '
        Me.FTOrderNoTo_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTOrderNoTo_lbl.Appearance.Options.UseForeColor = True
        Me.FTOrderNoTo_lbl.Appearance.Options.UseTextOptions = True
        Me.FTOrderNoTo_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTOrderNoTo_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTOrderNoTo_lbl.Location = New System.Drawing.Point(402, 61)
        Me.FTOrderNoTo_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTOrderNoTo_lbl.Name = "FTOrderNoTo_lbl"
        Me.FTOrderNoTo_lbl.Size = New System.Drawing.Size(118, 23)
        Me.FTOrderNoTo_lbl.TabIndex = 287
        Me.FTOrderNoTo_lbl.Tag = "2|"
        Me.FTOrderNoTo_lbl.Text = "To Order No :"
        '
        'FTOrderNoTo
        '
        Me.FTOrderNoTo.EnterMoveNextControl = True
        Me.FTOrderNoTo.Location = New System.Drawing.Point(522, 61)
        Me.FTOrderNoTo.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
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
        Me.FTOrderNoTo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", 15, True, True, False, EditorButtonImageOptions1, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, SerializableAppearanceObject2, SerializableAppearanceObject3, SerializableAppearanceObject4, "", "219", Nothing, DevExpress.Utils.ToolTipAnchor.[Default])})
        Me.FTOrderNoTo.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.FTOrderNoTo.Properties.MaxLength = 30
        Me.FTOrderNoTo.Size = New System.Drawing.Size(105, 22)
        Me.FTOrderNoTo.TabIndex = 9
        Me.FTOrderNoTo.Tag = "2|"
        '
        'FTOrderNo_lbl
        '
        Me.FTOrderNo_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTOrderNo_lbl.Appearance.Options.UseForeColor = True
        Me.FTOrderNo_lbl.Appearance.Options.UseTextOptions = True
        Me.FTOrderNo_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTOrderNo_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTOrderNo_lbl.Location = New System.Drawing.Point(128, 59)
        Me.FTOrderNo_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTOrderNo_lbl.Name = "FTOrderNo_lbl"
        Me.FTOrderNo_lbl.Size = New System.Drawing.Size(133, 23)
        Me.FTOrderNo_lbl.TabIndex = 285
        Me.FTOrderNo_lbl.Tag = "2|"
        Me.FTOrderNo_lbl.Text = "From Order No :"
        '
        'FTOrderNo
        '
        Me.FTOrderNo.EnterMoveNextControl = True
        Me.FTOrderNo.Location = New System.Drawing.Point(263, 61)
        Me.FTOrderNo.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
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
        Me.FTOrderNo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", 15, True, True, False, EditorButtonImageOptions2, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject5, SerializableAppearanceObject6, SerializableAppearanceObject7, SerializableAppearanceObject8, "", "218", Nothing, DevExpress.Utils.ToolTipAnchor.[Default])})
        Me.FTOrderNo.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.FTOrderNo.Properties.MaxLength = 30
        Me.FTOrderNo.Size = New System.Drawing.Size(105, 22)
        Me.FTOrderNo.TabIndex = 8
        Me.FTOrderNo.Tag = "2|"
        '
        'ogbdetail
        '
        Me.ogbdetail.Controls.Add(Me.ogcdetailcolorsizeline)
        Me.ogbdetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogbdetail.Location = New System.Drawing.Point(0, 151)
        Me.ogbdetail.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbdetail.Name = "ogbdetail"
        Me.ogbdetail.ShowCaption = False
        Me.ogbdetail.Size = New System.Drawing.Size(1326, 628)
        Me.ogbdetail.TabIndex = 0
        '
        'ogcdetailcolorsizeline
        '
        Me.ogcdetailcolorsizeline.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogcdetailcolorsizeline.Location = New System.Drawing.Point(2, 2)
        Me.ogcdetailcolorsizeline.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.ogcdetailcolorsizeline.Name = "ogcdetailcolorsizeline"
        Me.ogcdetailcolorsizeline.SelectedTabPage = Me.otbdetailcolorsizeline
        Me.ogcdetailcolorsizeline.Size = New System.Drawing.Size(1322, 624)
        Me.ogcdetailcolorsizeline.TabIndex = 387
        Me.ogcdetailcolorsizeline.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.otbdetailcolorsizeline, Me.XtraTabPage1})
        '
        'otbdetailcolorsizeline
        '
        Me.otbdetailcolorsizeline.Controls.Add(Me.ogcdetailcolorsizelineg)
        Me.otbdetailcolorsizeline.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.otbdetailcolorsizeline.Name = "otbdetailcolorsizeline"
        Me.otbdetailcolorsizeline.Size = New System.Drawing.Size(1320, 594)
        Me.otbdetailcolorsizeline.Text = "Detail By Color , Size  And Line"
        '
        'ogcdetailcolorsizelineg
        '
        Me.ogcdetailcolorsizelineg.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogcdetailcolorsizelineg.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcdetailcolorsizelineg.Location = New System.Drawing.Point(0, 0)
        Me.ogcdetailcolorsizelineg.MainView = Me.ogvdetailcolorsizelineg
        Me.ogcdetailcolorsizelineg.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcdetailcolorsizelineg.Name = "ogcdetailcolorsizelineg"
        Me.ogcdetailcolorsizelineg.Size = New System.Drawing.Size(1320, 594)
        Me.ogcdetailcolorsizelineg.TabIndex = 2
        Me.ogcdetailcolorsizelineg.TabStop = False
        Me.ogcdetailcolorsizelineg.Tag = "2|"
        Me.ogcdetailcolorsizelineg.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvdetailcolorsizelineg})
        '
        'ogvdetailcolorsizelineg
        '
        Me.ogvdetailcolorsizelineg.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.FTStyleCode, Me.SFTOrderNo, Me.FTPORef, Me.SFTCmpCode, Me.SFTCmpName, Me.SFDShipDate, Me.SFTPOLineItemNo, Me.SFTColorway, Me.SFTSizeBreakDown, Me.SFNQuantity, Me.SFNQuantityExtra, Me.SFNGarmentQtyTest, Me.SFNGrandQuantity, Me.FTUnitSectCodeCut, Me.SFNCutQuantity, Me.SFNCutBalQuantity, Me.CFNQtyEmbroidery, Me.CFNRcvQtyEmbroidery, Me.FNBalQtyEmbroidery, Me.CFNQtyPrint, Me.CFNRcvQtyPrint, Me.CFNBalQtyPrint, Me.CFNQtyHeat, Me.CFNRcvQtyHeat, Me.CFNBalQtyHeat, Me.CFNQtyLaser, Me.CFNRcvQtyLaser, Me.CFNBalQtyLaser, Me.CFNQtyPadPrint, Me.CFNRcvQtyPadPrint, Me.CFNBalQtyPadPrint, Me.CFNQtyWindow, Me.CFNRcvQtyWindow, Me.CFNBalQtyWindow, Me.SFNSendSuplQuantity, Me.SFNRcvSuplQuantity, Me.SFNBalSuplQuantity, Me.SFNSPMKQuantity, Me.SFNBalSPMKQuantity, Me.FTUnitSectCodeSew, Me.SFNSewQuantity, Me.CXFNSPMKQuantityBal, Me.SFNSewOutQuantity, Me.SFNBalSewQuantity, Me.SFNPackQuantity, Me.SFNBalPackQuantity, Me.FNTableCut, Me.cFNFGInQty, Me.cFNFGBalQty, Me.cFNExpQty, Me.cFNExpBalQty})
        Me.ogvdetailcolorsizelineg.GridControl = Me.ogcdetailcolorsizelineg
        Me.ogvdetailcolorsizelineg.Name = "ogvdetailcolorsizelineg"
        Me.ogvdetailcolorsizelineg.OptionsCustomization.AllowQuickHideColumns = False
        Me.ogvdetailcolorsizelineg.OptionsView.AllowCellMerge = True
        Me.ogvdetailcolorsizelineg.OptionsView.ColumnAutoWidth = False
        Me.ogvdetailcolorsizelineg.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.ogvdetailcolorsizelineg.OptionsView.ShowGroupPanel = False
        Me.ogvdetailcolorsizelineg.Tag = "2|"
        '
        'FTStyleCode
        '
        Me.FTStyleCode.AppearanceHeader.Options.UseTextOptions = True
        Me.FTStyleCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTStyleCode.Caption = "FTStyleCode"
        Me.FTStyleCode.FieldName = "FTStyleCode"
        Me.FTStyleCode.Name = "FTStyleCode"
        Me.FTStyleCode.OptionsColumn.AllowEdit = False
        Me.FTStyleCode.OptionsColumn.ReadOnly = True
        Me.FTStyleCode.Visible = True
        Me.FTStyleCode.VisibleIndex = 0
        '
        'SFTOrderNo
        '
        Me.SFTOrderNo.AppearanceHeader.Options.UseTextOptions = True
        Me.SFTOrderNo.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.SFTOrderNo.Caption = "FTOrderNo"
        Me.SFTOrderNo.FieldName = "FTOrderNo"
        Me.SFTOrderNo.Name = "SFTOrderNo"
        Me.SFTOrderNo.OptionsColumn.AllowEdit = False
        Me.SFTOrderNo.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.SFTOrderNo.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.SFTOrderNo.OptionsColumn.AllowShowHide = False
        Me.SFTOrderNo.OptionsColumn.ReadOnly = True
        Me.SFTOrderNo.OptionsColumn.ShowInCustomizationForm = False
        Me.SFTOrderNo.Visible = True
        Me.SFTOrderNo.VisibleIndex = 2
        Me.SFTOrderNo.Width = 127
        '
        'FTPORef
        '
        Me.FTPORef.AppearanceHeader.Options.UseTextOptions = True
        Me.FTPORef.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTPORef.Caption = "FTPORef"
        Me.FTPORef.FieldName = "FTPORef"
        Me.FTPORef.Name = "FTPORef"
        Me.FTPORef.OptionsColumn.AllowEdit = False
        Me.FTPORef.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTPORef.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTPORef.OptionsColumn.AllowShowHide = False
        Me.FTPORef.OptionsColumn.ReadOnly = True
        Me.FTPORef.Visible = True
        Me.FTPORef.VisibleIndex = 1
        Me.FTPORef.Width = 110
        '
        'SFTCmpCode
        '
        Me.SFTCmpCode.AppearanceHeader.Options.UseTextOptions = True
        Me.SFTCmpCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.SFTCmpCode.Caption = "FTCmpCode"
        Me.SFTCmpCode.FieldName = "FTCmpCode"
        Me.SFTCmpCode.Name = "SFTCmpCode"
        Me.SFTCmpCode.OptionsColumn.AllowEdit = False
        Me.SFTCmpCode.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.SFTCmpCode.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.SFTCmpCode.OptionsColumn.AllowShowHide = False
        Me.SFTCmpCode.OptionsColumn.ReadOnly = True
        Me.SFTCmpCode.OptionsColumn.ShowInCustomizationForm = False
        Me.SFTCmpCode.Visible = True
        Me.SFTCmpCode.VisibleIndex = 3
        Me.SFTCmpCode.Width = 100
        '
        'SFTCmpName
        '
        Me.SFTCmpName.AppearanceHeader.Options.UseTextOptions = True
        Me.SFTCmpName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.SFTCmpName.Caption = "FTCmpName"
        Me.SFTCmpName.FieldName = "FTCmpName"
        Me.SFTCmpName.Name = "SFTCmpName"
        Me.SFTCmpName.OptionsColumn.AllowEdit = False
        Me.SFTCmpName.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.SFTCmpName.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.SFTCmpName.OptionsColumn.AllowShowHide = False
        Me.SFTCmpName.OptionsColumn.ReadOnly = True
        Me.SFTCmpName.OptionsColumn.ShowInCustomizationForm = False
        Me.SFTCmpName.Visible = True
        Me.SFTCmpName.VisibleIndex = 4
        Me.SFTCmpName.Width = 120
        '
        'SFDShipDate
        '
        Me.SFDShipDate.AppearanceCell.Options.UseTextOptions = True
        Me.SFDShipDate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.SFDShipDate.AppearanceHeader.Options.UseTextOptions = True
        Me.SFDShipDate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.SFDShipDate.Caption = "FDShipDate"
        Me.SFDShipDate.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.SFDShipDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.SFDShipDate.FieldName = "FDShipDate"
        Me.SFDShipDate.Name = "SFDShipDate"
        Me.SFDShipDate.OptionsColumn.AllowEdit = False
        Me.SFDShipDate.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.SFDShipDate.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.SFDShipDate.OptionsColumn.AllowShowHide = False
        Me.SFDShipDate.OptionsColumn.ReadOnly = True
        Me.SFDShipDate.OptionsColumn.ShowInCustomizationForm = False
        Me.SFDShipDate.Visible = True
        Me.SFDShipDate.VisibleIndex = 5
        Me.SFDShipDate.Width = 80
        '
        'SFTPOLineItemNo
        '
        Me.SFTPOLineItemNo.AppearanceHeader.Options.UseTextOptions = True
        Me.SFTPOLineItemNo.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.SFTPOLineItemNo.Caption = "FTPOLineItemNo"
        Me.SFTPOLineItemNo.FieldName = "FTPOLineItemNo"
        Me.SFTPOLineItemNo.Name = "SFTPOLineItemNo"
        Me.SFTPOLineItemNo.OptionsColumn.AllowEdit = False
        Me.SFTPOLineItemNo.OptionsColumn.AllowShowHide = False
        Me.SFTPOLineItemNo.OptionsColumn.ReadOnly = True
        Me.SFTPOLineItemNo.OptionsColumn.ShowInCustomizationForm = False
        Me.SFTPOLineItemNo.Visible = True
        Me.SFTPOLineItemNo.VisibleIndex = 6
        Me.SFTPOLineItemNo.Width = 80
        '
        'SFTColorway
        '
        Me.SFTColorway.AppearanceHeader.Options.UseTextOptions = True
        Me.SFTColorway.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.SFTColorway.Caption = "FTColorway"
        Me.SFTColorway.FieldName = "FTColorway"
        Me.SFTColorway.Name = "SFTColorway"
        Me.SFTColorway.OptionsColumn.AllowEdit = False
        Me.SFTColorway.OptionsColumn.AllowShowHide = False
        Me.SFTColorway.OptionsColumn.ReadOnly = True
        Me.SFTColorway.OptionsColumn.ShowInCustomizationForm = False
        Me.SFTColorway.Visible = True
        Me.SFTColorway.VisibleIndex = 7
        Me.SFTColorway.Width = 80
        '
        'SFTSizeBreakDown
        '
        Me.SFTSizeBreakDown.AppearanceHeader.Options.UseTextOptions = True
        Me.SFTSizeBreakDown.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.SFTSizeBreakDown.Caption = "FTSizeBreakDown"
        Me.SFTSizeBreakDown.FieldName = "FTSizeBreakDown"
        Me.SFTSizeBreakDown.Name = "SFTSizeBreakDown"
        Me.SFTSizeBreakDown.OptionsColumn.AllowEdit = False
        Me.SFTSizeBreakDown.OptionsColumn.AllowShowHide = False
        Me.SFTSizeBreakDown.OptionsColumn.ReadOnly = True
        Me.SFTSizeBreakDown.OptionsColumn.ShowInCustomizationForm = False
        Me.SFTSizeBreakDown.Visible = True
        Me.SFTSizeBreakDown.VisibleIndex = 8
        Me.SFTSizeBreakDown.Width = 60
        '
        'SFNQuantity
        '
        Me.SFNQuantity.AppearanceCell.Options.UseTextOptions = True
        Me.SFNQuantity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.SFNQuantity.AppearanceHeader.Options.UseTextOptions = True
        Me.SFNQuantity.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.SFNQuantity.Caption = "Order Quantity"
        Me.SFNQuantity.DisplayFormat.FormatString = "{0:n0}"
        Me.SFNQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.SFNQuantity.FieldName = "FNQuantity"
        Me.SFNQuantity.Name = "SFNQuantity"
        Me.SFNQuantity.OptionsColumn.AllowEdit = False
        Me.SFNQuantity.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.SFNQuantity.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.SFNQuantity.OptionsColumn.AllowShowHide = False
        Me.SFNQuantity.OptionsColumn.ReadOnly = True
        Me.SFNQuantity.OptionsColumn.ShowInCustomizationForm = False
        Me.SFNQuantity.Visible = True
        Me.SFNQuantity.VisibleIndex = 9
        Me.SFNQuantity.Width = 90
        '
        'SFNQuantityExtra
        '
        Me.SFNQuantityExtra.AppearanceCell.Options.UseTextOptions = True
        Me.SFNQuantityExtra.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.SFNQuantityExtra.AppearanceHeader.Options.UseTextOptions = True
        Me.SFNQuantityExtra.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.SFNQuantityExtra.Caption = "Extra"
        Me.SFNQuantityExtra.DisplayFormat.FormatString = "{0:n0}"
        Me.SFNQuantityExtra.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.SFNQuantityExtra.FieldName = "FNQuantityExtra"
        Me.SFNQuantityExtra.Name = "SFNQuantityExtra"
        Me.SFNQuantityExtra.OptionsColumn.AllowEdit = False
        Me.SFNQuantityExtra.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.SFNQuantityExtra.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.SFNQuantityExtra.OptionsColumn.AllowShowHide = False
        Me.SFNQuantityExtra.OptionsColumn.ReadOnly = True
        Me.SFNQuantityExtra.OptionsColumn.ShowInCustomizationForm = False
        Me.SFNQuantityExtra.Visible = True
        Me.SFNQuantityExtra.VisibleIndex = 10
        Me.SFNQuantityExtra.Width = 90
        '
        'SFNGarmentQtyTest
        '
        Me.SFNGarmentQtyTest.AppearanceCell.Options.UseTextOptions = True
        Me.SFNGarmentQtyTest.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.SFNGarmentQtyTest.AppearanceHeader.Options.UseTextOptions = True
        Me.SFNGarmentQtyTest.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.SFNGarmentQtyTest.Caption = "Test"
        Me.SFNGarmentQtyTest.DisplayFormat.FormatString = "{0:n0}"
        Me.SFNGarmentQtyTest.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.SFNGarmentQtyTest.FieldName = "FNGarmentQtyTest"
        Me.SFNGarmentQtyTest.Name = "SFNGarmentQtyTest"
        Me.SFNGarmentQtyTest.OptionsColumn.AllowEdit = False
        Me.SFNGarmentQtyTest.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.SFNGarmentQtyTest.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.SFNGarmentQtyTest.OptionsColumn.AllowShowHide = False
        Me.SFNGarmentQtyTest.OptionsColumn.ReadOnly = True
        Me.SFNGarmentQtyTest.OptionsColumn.ShowInCustomizationForm = False
        Me.SFNGarmentQtyTest.Visible = True
        Me.SFNGarmentQtyTest.VisibleIndex = 11
        Me.SFNGarmentQtyTest.Width = 90
        '
        'SFNGrandQuantity
        '
        Me.SFNGrandQuantity.AppearanceCell.Options.UseTextOptions = True
        Me.SFNGrandQuantity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.SFNGrandQuantity.AppearanceHeader.Options.UseTextOptions = True
        Me.SFNGrandQuantity.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.SFNGrandQuantity.Caption = "Total Quantity"
        Me.SFNGrandQuantity.DisplayFormat.FormatString = "{0:n0}"
        Me.SFNGrandQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.SFNGrandQuantity.FieldName = "FNGrandQuantity"
        Me.SFNGrandQuantity.Name = "SFNGrandQuantity"
        Me.SFNGrandQuantity.OptionsColumn.AllowEdit = False
        Me.SFNGrandQuantity.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.SFNGrandQuantity.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.SFNGrandQuantity.OptionsColumn.AllowShowHide = False
        Me.SFNGrandQuantity.OptionsColumn.ReadOnly = True
        Me.SFNGrandQuantity.OptionsColumn.ShowInCustomizationForm = False
        Me.SFNGrandQuantity.Visible = True
        Me.SFNGrandQuantity.VisibleIndex = 12
        Me.SFNGrandQuantity.Width = 90
        '
        'FTUnitSectCodeCut
        '
        Me.FTUnitSectCodeCut.AppearanceHeader.Options.UseTextOptions = True
        Me.FTUnitSectCodeCut.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTUnitSectCodeCut.Caption = "โต๊ะตัด"
        Me.FTUnitSectCodeCut.FieldName = "FTUnitSectCodeCut"
        Me.FTUnitSectCodeCut.Name = "FTUnitSectCodeCut"
        Me.FTUnitSectCodeCut.OptionsColumn.AllowEdit = False
        Me.FTUnitSectCodeCut.OptionsColumn.AllowShowHide = False
        Me.FTUnitSectCodeCut.OptionsColumn.ReadOnly = True
        Me.FTUnitSectCodeCut.OptionsColumn.ShowInCustomizationForm = False
        Me.FTUnitSectCodeCut.Visible = True
        Me.FTUnitSectCodeCut.VisibleIndex = 13
        Me.FTUnitSectCodeCut.Width = 80
        '
        'SFNCutQuantity
        '
        Me.SFNCutQuantity.AppearanceCell.Options.UseTextOptions = True
        Me.SFNCutQuantity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.SFNCutQuantity.AppearanceHeader.Options.UseTextOptions = True
        Me.SFNCutQuantity.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.SFNCutQuantity.Caption = "Cut Quantity"
        Me.SFNCutQuantity.DisplayFormat.FormatString = "{0:n0}"
        Me.SFNCutQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.SFNCutQuantity.FieldName = "FNCutQuantity"
        Me.SFNCutQuantity.Name = "SFNCutQuantity"
        Me.SFNCutQuantity.OptionsColumn.AllowEdit = False
        Me.SFNCutQuantity.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.SFNCutQuantity.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.SFNCutQuantity.OptionsColumn.AllowShowHide = False
        Me.SFNCutQuantity.OptionsColumn.ReadOnly = True
        Me.SFNCutQuantity.OptionsColumn.ShowInCustomizationForm = False
        Me.SFNCutQuantity.Visible = True
        Me.SFNCutQuantity.VisibleIndex = 14
        Me.SFNCutQuantity.Width = 90
        '
        'SFNCutBalQuantity
        '
        Me.SFNCutBalQuantity.AppearanceCell.Options.UseTextOptions = True
        Me.SFNCutBalQuantity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.SFNCutBalQuantity.AppearanceHeader.Options.UseTextOptions = True
        Me.SFNCutBalQuantity.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.SFNCutBalQuantity.Caption = "Bal Cut Quantity"
        Me.SFNCutBalQuantity.DisplayFormat.FormatString = "{0:n0}"
        Me.SFNCutBalQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.SFNCutBalQuantity.FieldName = "FNCutBalQuantity"
        Me.SFNCutBalQuantity.Name = "SFNCutBalQuantity"
        Me.SFNCutBalQuantity.OptionsColumn.AllowEdit = False
        Me.SFNCutBalQuantity.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.SFNCutBalQuantity.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.SFNCutBalQuantity.OptionsColumn.AllowShowHide = False
        Me.SFNCutBalQuantity.OptionsColumn.ReadOnly = True
        Me.SFNCutBalQuantity.OptionsColumn.ShowInCustomizationForm = False
        Me.SFNCutBalQuantity.Visible = True
        Me.SFNCutBalQuantity.VisibleIndex = 15
        Me.SFNCutBalQuantity.Width = 90
        '
        'CFNQtyEmbroidery
        '
        Me.CFNQtyEmbroidery.Caption = "ส่งปัก"
        Me.CFNQtyEmbroidery.DisplayFormat.FormatString = "{0:n0}"
        Me.CFNQtyEmbroidery.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CFNQtyEmbroidery.FieldName = "FNQtyEmbroidery"
        Me.CFNQtyEmbroidery.Name = "CFNQtyEmbroidery"
        Me.CFNQtyEmbroidery.OptionsColumn.AllowEdit = False
        Me.CFNQtyEmbroidery.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.CFNQtyEmbroidery.OptionsColumn.ReadOnly = True
        Me.CFNQtyEmbroidery.Visible = True
        Me.CFNQtyEmbroidery.VisibleIndex = 16
        '
        'CFNRcvQtyEmbroidery
        '
        Me.CFNRcvQtyEmbroidery.Caption = "รับปัก"
        Me.CFNRcvQtyEmbroidery.DisplayFormat.FormatString = "{0:n0}"
        Me.CFNRcvQtyEmbroidery.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CFNRcvQtyEmbroidery.FieldName = "FNRcvQtyEmbroidery"
        Me.CFNRcvQtyEmbroidery.Name = "CFNRcvQtyEmbroidery"
        Me.CFNRcvQtyEmbroidery.OptionsColumn.AllowEdit = False
        Me.CFNRcvQtyEmbroidery.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.CFNRcvQtyEmbroidery.OptionsColumn.ReadOnly = True
        Me.CFNRcvQtyEmbroidery.Visible = True
        Me.CFNRcvQtyEmbroidery.VisibleIndex = 17
        '
        'FNBalQtyEmbroidery
        '
        Me.FNBalQtyEmbroidery.Caption = "ปักคงค้าง"
        Me.FNBalQtyEmbroidery.DisplayFormat.FormatString = "{0:n0}"
        Me.FNBalQtyEmbroidery.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNBalQtyEmbroidery.FieldName = "FNBalQtyEmbroidery"
        Me.FNBalQtyEmbroidery.Name = "FNBalQtyEmbroidery"
        Me.FNBalQtyEmbroidery.OptionsColumn.AllowEdit = False
        Me.FNBalQtyEmbroidery.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.FNBalQtyEmbroidery.OptionsColumn.ReadOnly = True
        Me.FNBalQtyEmbroidery.Visible = True
        Me.FNBalQtyEmbroidery.VisibleIndex = 18
        '
        'CFNQtyPrint
        '
        Me.CFNQtyPrint.Caption = "ส่งพิมพ์"
        Me.CFNQtyPrint.DisplayFormat.FormatString = "{0:n0}"
        Me.CFNQtyPrint.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CFNQtyPrint.FieldName = "FNQtyPrint"
        Me.CFNQtyPrint.Name = "CFNQtyPrint"
        Me.CFNQtyPrint.OptionsColumn.AllowEdit = False
        Me.CFNQtyPrint.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.CFNQtyPrint.OptionsColumn.ReadOnly = True
        Me.CFNQtyPrint.Visible = True
        Me.CFNQtyPrint.VisibleIndex = 19
        '
        'CFNRcvQtyPrint
        '
        Me.CFNRcvQtyPrint.Caption = "รับพิมพ์"
        Me.CFNRcvQtyPrint.DisplayFormat.FormatString = "{0:n0}"
        Me.CFNRcvQtyPrint.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CFNRcvQtyPrint.FieldName = "FNRcvQtyPrint"
        Me.CFNRcvQtyPrint.Name = "CFNRcvQtyPrint"
        Me.CFNRcvQtyPrint.OptionsColumn.AllowEdit = False
        Me.CFNRcvQtyPrint.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.CFNRcvQtyPrint.OptionsColumn.ReadOnly = True
        Me.CFNRcvQtyPrint.Visible = True
        Me.CFNRcvQtyPrint.VisibleIndex = 20
        '
        'CFNBalQtyPrint
        '
        Me.CFNBalQtyPrint.Caption = "พิมคงค้าง"
        Me.CFNBalQtyPrint.DisplayFormat.FormatString = "{0:n0}"
        Me.CFNBalQtyPrint.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CFNBalQtyPrint.FieldName = "FNBalQtyPrint"
        Me.CFNBalQtyPrint.Name = "CFNBalQtyPrint"
        Me.CFNBalQtyPrint.OptionsColumn.AllowEdit = False
        Me.CFNBalQtyPrint.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.CFNBalQtyPrint.OptionsColumn.ReadOnly = True
        Me.CFNBalQtyPrint.Visible = True
        Me.CFNBalQtyPrint.VisibleIndex = 21
        '
        'CFNQtyHeat
        '
        Me.CFNQtyHeat.Caption = "Heat"
        Me.CFNQtyHeat.DisplayFormat.FormatString = "{0:n0}"
        Me.CFNQtyHeat.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CFNQtyHeat.FieldName = "FNQtyHeat"
        Me.CFNQtyHeat.Name = "CFNQtyHeat"
        Me.CFNQtyHeat.OptionsColumn.AllowEdit = False
        Me.CFNQtyHeat.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.CFNQtyHeat.OptionsColumn.ReadOnly = True
        Me.CFNQtyHeat.Visible = True
        Me.CFNQtyHeat.VisibleIndex = 22
        '
        'CFNRcvQtyHeat
        '
        Me.CFNRcvQtyHeat.Caption = "Heat Rcv."
        Me.CFNRcvQtyHeat.DisplayFormat.FormatString = "{0:n0}"
        Me.CFNRcvQtyHeat.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CFNRcvQtyHeat.FieldName = "FNRcvQtyHeat"
        Me.CFNRcvQtyHeat.Name = "CFNRcvQtyHeat"
        Me.CFNRcvQtyHeat.OptionsColumn.AllowEdit = False
        Me.CFNRcvQtyHeat.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.CFNRcvQtyHeat.OptionsColumn.ReadOnly = True
        Me.CFNRcvQtyHeat.Visible = True
        Me.CFNRcvQtyHeat.VisibleIndex = 23
        '
        'CFNBalQtyHeat
        '
        Me.CFNBalQtyHeat.Caption = "Heat Bal."
        Me.CFNBalQtyHeat.DisplayFormat.FormatString = "{0:n0}"
        Me.CFNBalQtyHeat.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CFNBalQtyHeat.FieldName = "FNBalQtyHeat"
        Me.CFNBalQtyHeat.Name = "CFNBalQtyHeat"
        Me.CFNBalQtyHeat.OptionsColumn.AllowEdit = False
        Me.CFNBalQtyHeat.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.CFNBalQtyHeat.OptionsColumn.ReadOnly = True
        Me.CFNBalQtyHeat.Visible = True
        Me.CFNBalQtyHeat.VisibleIndex = 24
        '
        'CFNQtyLaser
        '
        Me.CFNQtyLaser.Caption = "Laser"
        Me.CFNQtyLaser.DisplayFormat.FormatString = "{0:n0}"
        Me.CFNQtyLaser.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CFNQtyLaser.FieldName = "FNQtyLaser"
        Me.CFNQtyLaser.Name = "CFNQtyLaser"
        Me.CFNQtyLaser.OptionsColumn.AllowEdit = False
        Me.CFNQtyLaser.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.CFNQtyLaser.OptionsColumn.ReadOnly = True
        Me.CFNQtyLaser.Visible = True
        Me.CFNQtyLaser.VisibleIndex = 25
        '
        'CFNRcvQtyLaser
        '
        Me.CFNRcvQtyLaser.Caption = "Laser Rcv."
        Me.CFNRcvQtyLaser.DisplayFormat.FormatString = "{0:n0}"
        Me.CFNRcvQtyLaser.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CFNRcvQtyLaser.FieldName = "FNRcvQtyLaser"
        Me.CFNRcvQtyLaser.Name = "CFNRcvQtyLaser"
        Me.CFNRcvQtyLaser.OptionsColumn.AllowEdit = False
        Me.CFNRcvQtyLaser.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.CFNRcvQtyLaser.OptionsColumn.ReadOnly = True
        Me.CFNRcvQtyLaser.Visible = True
        Me.CFNRcvQtyLaser.VisibleIndex = 26
        '
        'CFNBalQtyLaser
        '
        Me.CFNBalQtyLaser.Caption = "Laser Bal."
        Me.CFNBalQtyLaser.DisplayFormat.FormatString = "{0:n0}"
        Me.CFNBalQtyLaser.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CFNBalQtyLaser.FieldName = "FNBalQtyLaser"
        Me.CFNBalQtyLaser.Name = "CFNBalQtyLaser"
        Me.CFNBalQtyLaser.OptionsColumn.AllowEdit = False
        Me.CFNBalQtyLaser.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.CFNBalQtyLaser.OptionsColumn.ReadOnly = True
        Me.CFNBalQtyLaser.Visible = True
        Me.CFNBalQtyLaser.VisibleIndex = 27
        '
        'CFNQtyPadPrint
        '
        Me.CFNQtyPadPrint.Caption = "Pad Print"
        Me.CFNQtyPadPrint.DisplayFormat.FormatString = "{0:n0}"
        Me.CFNQtyPadPrint.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CFNQtyPadPrint.FieldName = "FNQtyPadPrint"
        Me.CFNQtyPadPrint.Name = "CFNQtyPadPrint"
        Me.CFNQtyPadPrint.OptionsColumn.AllowEdit = False
        Me.CFNQtyPadPrint.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.CFNQtyPadPrint.OptionsColumn.ReadOnly = True
        Me.CFNQtyPadPrint.Visible = True
        Me.CFNQtyPadPrint.VisibleIndex = 28
        '
        'CFNRcvQtyPadPrint
        '
        Me.CFNRcvQtyPadPrint.Caption = "Pad Print Rcv."
        Me.CFNRcvQtyPadPrint.DisplayFormat.FormatString = "{0:n0}"
        Me.CFNRcvQtyPadPrint.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CFNRcvQtyPadPrint.FieldName = "FNRcvQtyPadPrint"
        Me.CFNRcvQtyPadPrint.Name = "CFNRcvQtyPadPrint"
        Me.CFNRcvQtyPadPrint.OptionsColumn.AllowEdit = False
        Me.CFNRcvQtyPadPrint.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.CFNRcvQtyPadPrint.OptionsColumn.ReadOnly = True
        Me.CFNRcvQtyPadPrint.Visible = True
        Me.CFNRcvQtyPadPrint.VisibleIndex = 29
        '
        'CFNBalQtyPadPrint
        '
        Me.CFNBalQtyPadPrint.Caption = "Pad Print Bal."
        Me.CFNBalQtyPadPrint.DisplayFormat.FormatString = "{0:n0}"
        Me.CFNBalQtyPadPrint.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CFNBalQtyPadPrint.FieldName = "FNBalQtyPadPrint"
        Me.CFNBalQtyPadPrint.Name = "CFNBalQtyPadPrint"
        Me.CFNBalQtyPadPrint.OptionsColumn.AllowEdit = False
        Me.CFNBalQtyPadPrint.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.CFNBalQtyPadPrint.OptionsColumn.ReadOnly = True
        Me.CFNBalQtyPadPrint.Visible = True
        Me.CFNBalQtyPadPrint.VisibleIndex = 30
        '
        'CFNQtyWindow
        '
        Me.CFNQtyWindow.Caption = "Window"
        Me.CFNQtyWindow.DisplayFormat.FormatString = "{0:n0}"
        Me.CFNQtyWindow.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CFNQtyWindow.FieldName = "FNQtyWindow"
        Me.CFNQtyWindow.Name = "CFNQtyWindow"
        Me.CFNQtyWindow.OptionsColumn.AllowEdit = False
        Me.CFNQtyWindow.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.CFNQtyWindow.OptionsColumn.ReadOnly = True
        '
        'CFNRcvQtyWindow
        '
        Me.CFNRcvQtyWindow.Caption = "Window Rcv."
        Me.CFNRcvQtyWindow.DisplayFormat.FormatString = "{0:n0}"
        Me.CFNRcvQtyWindow.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CFNRcvQtyWindow.FieldName = "FNRcvQtyWindow"
        Me.CFNRcvQtyWindow.Name = "CFNRcvQtyWindow"
        Me.CFNRcvQtyWindow.OptionsColumn.AllowEdit = False
        Me.CFNRcvQtyWindow.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.CFNRcvQtyWindow.OptionsColumn.ReadOnly = True
        '
        'CFNBalQtyWindow
        '
        Me.CFNBalQtyWindow.Caption = "Window Bal"
        Me.CFNBalQtyWindow.DisplayFormat.FormatString = "{0:n0}"
        Me.CFNBalQtyWindow.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CFNBalQtyWindow.FieldName = "FNBalQtyWindow"
        Me.CFNBalQtyWindow.Name = "CFNBalQtyWindow"
        Me.CFNBalQtyWindow.OptionsColumn.AllowEdit = False
        Me.CFNBalQtyWindow.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.CFNBalQtyWindow.OptionsColumn.ReadOnly = True
        '
        'SFNSendSuplQuantity
        '
        Me.SFNSendSuplQuantity.AppearanceCell.Options.UseTextOptions = True
        Me.SFNSendSuplQuantity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.SFNSendSuplQuantity.AppearanceHeader.Options.UseTextOptions = True
        Me.SFNSendSuplQuantity.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.SFNSendSuplQuantity.Caption = "Send Supl Quantity"
        Me.SFNSendSuplQuantity.DisplayFormat.FormatString = "{0:n0}"
        Me.SFNSendSuplQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.SFNSendSuplQuantity.FieldName = "FNSendSuplQuantity"
        Me.SFNSendSuplQuantity.Name = "SFNSendSuplQuantity"
        Me.SFNSendSuplQuantity.OptionsColumn.AllowEdit = False
        Me.SFNSendSuplQuantity.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.SFNSendSuplQuantity.OptionsColumn.AllowShowHide = False
        Me.SFNSendSuplQuantity.OptionsColumn.ReadOnly = True
        Me.SFNSendSuplQuantity.OptionsColumn.ShowInCustomizationForm = False
        Me.SFNSendSuplQuantity.Visible = True
        Me.SFNSendSuplQuantity.VisibleIndex = 31
        Me.SFNSendSuplQuantity.Width = 90
        '
        'SFNRcvSuplQuantity
        '
        Me.SFNRcvSuplQuantity.AppearanceCell.Options.UseTextOptions = True
        Me.SFNRcvSuplQuantity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.SFNRcvSuplQuantity.AppearanceHeader.Options.UseTextOptions = True
        Me.SFNRcvSuplQuantity.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.SFNRcvSuplQuantity.Caption = "Rcv Supl Quantity"
        Me.SFNRcvSuplQuantity.DisplayFormat.FormatString = "{0:n0}"
        Me.SFNRcvSuplQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.SFNRcvSuplQuantity.FieldName = "FNRcvSuplQuantity"
        Me.SFNRcvSuplQuantity.Name = "SFNRcvSuplQuantity"
        Me.SFNRcvSuplQuantity.OptionsColumn.AllowEdit = False
        Me.SFNRcvSuplQuantity.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.SFNRcvSuplQuantity.OptionsColumn.AllowShowHide = False
        Me.SFNRcvSuplQuantity.OptionsColumn.ReadOnly = True
        Me.SFNRcvSuplQuantity.OptionsColumn.ShowInCustomizationForm = False
        Me.SFNRcvSuplQuantity.Visible = True
        Me.SFNRcvSuplQuantity.VisibleIndex = 32
        Me.SFNRcvSuplQuantity.Width = 90
        '
        'SFNBalSuplQuantity
        '
        Me.SFNBalSuplQuantity.AppearanceCell.Options.UseTextOptions = True
        Me.SFNBalSuplQuantity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.SFNBalSuplQuantity.AppearanceHeader.Options.UseTextOptions = True
        Me.SFNBalSuplQuantity.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.SFNBalSuplQuantity.Caption = "Bal Supl Qty"
        Me.SFNBalSuplQuantity.DisplayFormat.FormatString = "{0:n0}"
        Me.SFNBalSuplQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.SFNBalSuplQuantity.FieldName = "FNBalSuplQuantity"
        Me.SFNBalSuplQuantity.Name = "SFNBalSuplQuantity"
        Me.SFNBalSuplQuantity.OptionsColumn.AllowEdit = False
        Me.SFNBalSuplQuantity.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.SFNBalSuplQuantity.OptionsColumn.AllowShowHide = False
        Me.SFNBalSuplQuantity.OptionsColumn.ReadOnly = True
        Me.SFNBalSuplQuantity.OptionsColumn.ShowInCustomizationForm = False
        Me.SFNBalSuplQuantity.Visible = True
        Me.SFNBalSuplQuantity.VisibleIndex = 34
        Me.SFNBalSuplQuantity.Width = 90
        '
        'SFNSPMKQuantity
        '
        Me.SFNSPMKQuantity.AppearanceCell.Options.UseTextOptions = True
        Me.SFNSPMKQuantity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.SFNSPMKQuantity.AppearanceHeader.Options.UseTextOptions = True
        Me.SFNSPMKQuantity.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.SFNSPMKQuantity.Caption = "Supper Market"
        Me.SFNSPMKQuantity.DisplayFormat.FormatString = "{0:n0}"
        Me.SFNSPMKQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.SFNSPMKQuantity.FieldName = "FNSPMKQuantity"
        Me.SFNSPMKQuantity.Name = "SFNSPMKQuantity"
        Me.SFNSPMKQuantity.OptionsColumn.AllowEdit = False
        Me.SFNSPMKQuantity.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.SFNSPMKQuantity.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.SFNSPMKQuantity.OptionsColumn.AllowShowHide = False
        Me.SFNSPMKQuantity.OptionsColumn.ReadOnly = True
        Me.SFNSPMKQuantity.OptionsColumn.ShowInCustomizationForm = False
        Me.SFNSPMKQuantity.Visible = True
        Me.SFNSPMKQuantity.VisibleIndex = 33
        Me.SFNSPMKQuantity.Width = 90
        '
        'SFNBalSPMKQuantity
        '
        Me.SFNBalSPMKQuantity.AppearanceCell.Options.UseTextOptions = True
        Me.SFNBalSPMKQuantity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.SFNBalSPMKQuantity.AppearanceHeader.Options.UseTextOptions = True
        Me.SFNBalSPMKQuantity.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.SFNBalSPMKQuantity.Caption = "FNBalSPMKQuantity"
        Me.SFNBalSPMKQuantity.DisplayFormat.FormatString = "{0:n0}"
        Me.SFNBalSPMKQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.SFNBalSPMKQuantity.FieldName = "FNBalCutQuantity"
        Me.SFNBalSPMKQuantity.Name = "SFNBalSPMKQuantity"
        Me.SFNBalSPMKQuantity.OptionsColumn.AllowEdit = False
        Me.SFNBalSPMKQuantity.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.SFNBalSPMKQuantity.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.SFNBalSPMKQuantity.OptionsColumn.AllowShowHide = False
        Me.SFNBalSPMKQuantity.OptionsColumn.ReadOnly = True
        Me.SFNBalSPMKQuantity.OptionsColumn.ShowInCustomizationForm = False
        Me.SFNBalSPMKQuantity.Visible = True
        Me.SFNBalSPMKQuantity.VisibleIndex = 35
        Me.SFNBalSPMKQuantity.Width = 90
        '
        'FTUnitSectCodeSew
        '
        Me.FTUnitSectCodeSew.AppearanceHeader.Options.UseTextOptions = True
        Me.FTUnitSectCodeSew.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTUnitSectCodeSew.Caption = "ไลน์เย็บ"
        Me.FTUnitSectCodeSew.FieldName = "FTUnitSectCodeSew"
        Me.FTUnitSectCodeSew.Name = "FTUnitSectCodeSew"
        Me.FTUnitSectCodeSew.OptionsColumn.AllowEdit = False
        Me.FTUnitSectCodeSew.OptionsColumn.AllowShowHide = False
        Me.FTUnitSectCodeSew.OptionsColumn.ReadOnly = True
        Me.FTUnitSectCodeSew.OptionsColumn.ShowInCustomizationForm = False
        Me.FTUnitSectCodeSew.Visible = True
        Me.FTUnitSectCodeSew.VisibleIndex = 36
        Me.FTUnitSectCodeSew.Width = 80
        '
        'SFNSewQuantity
        '
        Me.SFNSewQuantity.AppearanceCell.Options.UseTextOptions = True
        Me.SFNSewQuantity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.SFNSewQuantity.AppearanceHeader.Options.UseTextOptions = True
        Me.SFNSewQuantity.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.SFNSewQuantity.Caption = "เข้าไลน์"
        Me.SFNSewQuantity.DisplayFormat.FormatString = "{0:n0}"
        Me.SFNSewQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.SFNSewQuantity.FieldName = "FNSewQuantity"
        Me.SFNSewQuantity.Name = "SFNSewQuantity"
        Me.SFNSewQuantity.OptionsColumn.AllowEdit = False
        Me.SFNSewQuantity.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.SFNSewQuantity.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.SFNSewQuantity.OptionsColumn.AllowShowHide = False
        Me.SFNSewQuantity.OptionsColumn.ReadOnly = True
        Me.SFNSewQuantity.OptionsColumn.ShowInCustomizationForm = False
        Me.SFNSewQuantity.Visible = True
        Me.SFNSewQuantity.VisibleIndex = 37
        Me.SFNSewQuantity.Width = 90
        '
        'CXFNSPMKQuantityBal
        '
        Me.CXFNSPMKQuantityBal.Caption = "SPM Bal."
        Me.CXFNSPMKQuantityBal.DisplayFormat.FormatString = "{0:n0}"
        Me.CXFNSPMKQuantityBal.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CXFNSPMKQuantityBal.FieldName = "FNSPMKQuantityBal"
        Me.CXFNSPMKQuantityBal.Name = "CXFNSPMKQuantityBal"
        Me.CXFNSPMKQuantityBal.OptionsColumn.AllowEdit = False
        Me.CXFNSPMKQuantityBal.OptionsColumn.ReadOnly = True
        Me.CXFNSPMKQuantityBal.Visible = True
        Me.CXFNSPMKQuantityBal.VisibleIndex = 38
        '
        'SFNSewOutQuantity
        '
        Me.SFNSewOutQuantity.AppearanceCell.Options.UseTextOptions = True
        Me.SFNSewOutQuantity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.SFNSewOutQuantity.AppearanceHeader.Options.UseTextOptions = True
        Me.SFNSewOutQuantity.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.SFNSewOutQuantity.Caption = "ท้ายไลน์"
        Me.SFNSewOutQuantity.DisplayFormat.FormatString = "{0:n0}"
        Me.SFNSewOutQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.SFNSewOutQuantity.FieldName = "FNSewOutQuantity"
        Me.SFNSewOutQuantity.Name = "SFNSewOutQuantity"
        Me.SFNSewOutQuantity.OptionsColumn.AllowEdit = False
        Me.SFNSewOutQuantity.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.SFNSewOutQuantity.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.SFNSewOutQuantity.OptionsColumn.AllowShowHide = False
        Me.SFNSewOutQuantity.OptionsColumn.ReadOnly = True
        Me.SFNSewOutQuantity.OptionsColumn.ShowInCustomizationForm = False
        Me.SFNSewOutQuantity.Visible = True
        Me.SFNSewOutQuantity.VisibleIndex = 39
        Me.SFNSewOutQuantity.Width = 90
        '
        'SFNBalSewQuantity
        '
        Me.SFNBalSewQuantity.AppearanceCell.Options.UseTextOptions = True
        Me.SFNBalSewQuantity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.SFNBalSewQuantity.AppearanceHeader.Options.UseTextOptions = True
        Me.SFNBalSewQuantity.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.SFNBalSewQuantity.Caption = "Bal Sew Qty"
        Me.SFNBalSewQuantity.DisplayFormat.FormatString = "{0:n0}"
        Me.SFNBalSewQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.SFNBalSewQuantity.FieldName = "FNBalSewQuantity"
        Me.SFNBalSewQuantity.Name = "SFNBalSewQuantity"
        Me.SFNBalSewQuantity.OptionsColumn.AllowEdit = False
        Me.SFNBalSewQuantity.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.SFNBalSewQuantity.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.SFNBalSewQuantity.OptionsColumn.AllowShowHide = False
        Me.SFNBalSewQuantity.OptionsColumn.ReadOnly = True
        Me.SFNBalSewQuantity.OptionsColumn.ShowInCustomizationForm = False
        Me.SFNBalSewQuantity.Visible = True
        Me.SFNBalSewQuantity.VisibleIndex = 40
        Me.SFNBalSewQuantity.Width = 90
        '
        'SFNPackQuantity
        '
        Me.SFNPackQuantity.AppearanceCell.Options.UseTextOptions = True
        Me.SFNPackQuantity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.SFNPackQuantity.AppearanceHeader.Options.UseTextOptions = True
        Me.SFNPackQuantity.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.SFNPackQuantity.Caption = "Pack Quantity"
        Me.SFNPackQuantity.DisplayFormat.FormatString = "{0:n0}"
        Me.SFNPackQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.SFNPackQuantity.FieldName = "FNPackQuantity"
        Me.SFNPackQuantity.Name = "SFNPackQuantity"
        Me.SFNPackQuantity.OptionsColumn.AllowEdit = False
        Me.SFNPackQuantity.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.SFNPackQuantity.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.SFNPackQuantity.OptionsColumn.AllowShowHide = False
        Me.SFNPackQuantity.OptionsColumn.ReadOnly = True
        Me.SFNPackQuantity.OptionsColumn.ShowInCustomizationForm = False
        Me.SFNPackQuantity.Visible = True
        Me.SFNPackQuantity.VisibleIndex = 41
        Me.SFNPackQuantity.Width = 90
        '
        'SFNBalPackQuantity
        '
        Me.SFNBalPackQuantity.AppearanceCell.Options.UseTextOptions = True
        Me.SFNBalPackQuantity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.SFNBalPackQuantity.AppearanceHeader.Options.UseTextOptions = True
        Me.SFNBalPackQuantity.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.SFNBalPackQuantity.Caption = "FNBalPackQuantity"
        Me.SFNBalPackQuantity.DisplayFormat.FormatString = "{0:n0}"
        Me.SFNBalPackQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.SFNBalPackQuantity.FieldName = "FNBalPackQuantity"
        Me.SFNBalPackQuantity.Name = "SFNBalPackQuantity"
        Me.SFNBalPackQuantity.OptionsColumn.AllowEdit = False
        Me.SFNBalPackQuantity.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.SFNBalPackQuantity.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.SFNBalPackQuantity.OptionsColumn.AllowShowHide = False
        Me.SFNBalPackQuantity.OptionsColumn.ReadOnly = True
        Me.SFNBalPackQuantity.OptionsColumn.ShowInCustomizationForm = False
        Me.SFNBalPackQuantity.Visible = True
        Me.SFNBalPackQuantity.VisibleIndex = 42
        Me.SFNBalPackQuantity.Width = 90
        '
        'FNTableCut
        '
        Me.FNTableCut.Caption = "FNTableCut"
        Me.FNTableCut.FieldName = "FNTableCut"
        Me.FNTableCut.Name = "FNTableCut"
        Me.FNTableCut.OptionsColumn.AllowEdit = False
        Me.FNTableCut.OptionsColumn.AllowShowHide = False
        Me.FNTableCut.OptionsColumn.ShowInCustomizationForm = False
        '
        'cFNFGInQty
        '
        Me.cFNFGInQty.Caption = "FG. In WH"
        Me.cFNFGInQty.DisplayFormat.FormatString = "N0"
        Me.cFNFGInQty.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cFNFGInQty.FieldName = "FNFGInQty"
        Me.cFNFGInQty.Name = "cFNFGInQty"
        Me.cFNFGInQty.OptionsColumn.AllowEdit = False
        Me.cFNFGInQty.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.cFNFGInQty.OptionsColumn.AllowMove = False
        Me.cFNFGInQty.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.cFNFGInQty.OptionsColumn.ReadOnly = True
        Me.cFNFGInQty.OptionsColumn.ShowInCustomizationForm = False
        Me.cFNFGInQty.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FNFGInQty", "{0:N0}")})
        Me.cFNFGInQty.Visible = True
        Me.cFNFGInQty.VisibleIndex = 43
        '
        'cFNFGBalQty
        '
        Me.cFNFGBalQty.Caption = "FG Bal WH"
        Me.cFNFGBalQty.DisplayFormat.FormatString = "N0"
        Me.cFNFGBalQty.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cFNFGBalQty.FieldName = "FNFGBalQty"
        Me.cFNFGBalQty.Name = "cFNFGBalQty"
        Me.cFNFGBalQty.OptionsColumn.AllowEdit = False
        Me.cFNFGBalQty.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.cFNFGBalQty.OptionsColumn.AllowMove = False
        Me.cFNFGBalQty.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.cFNFGBalQty.OptionsColumn.ReadOnly = True
        Me.cFNFGBalQty.OptionsColumn.ShowInCustomizationForm = False
        Me.cFNFGBalQty.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FNFGBalQty", "{0:N0}")})
        Me.cFNFGBalQty.Visible = True
        Me.cFNFGBalQty.VisibleIndex = 44
        '
        'cFNExpQty
        '
        Me.cFNExpQty.Caption = "Export Qty"
        Me.cFNExpQty.DisplayFormat.FormatString = "N0"
        Me.cFNExpQty.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cFNExpQty.FieldName = "FNExpQty"
        Me.cFNExpQty.Name = "cFNExpQty"
        Me.cFNExpQty.OptionsColumn.AllowEdit = False
        Me.cFNExpQty.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.cFNExpQty.OptionsColumn.AllowMove = False
        Me.cFNExpQty.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.cFNExpQty.OptionsColumn.ReadOnly = True
        Me.cFNExpQty.OptionsColumn.ShowInCustomizationForm = False
        Me.cFNExpQty.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FNExpQty", "{0:N0}")})
        Me.cFNExpQty.Visible = True
        Me.cFNExpQty.VisibleIndex = 45
        '
        'cFNExpBalQty
        '
        Me.cFNExpBalQty.Caption = "Export Bal"
        Me.cFNExpBalQty.DisplayFormat.FormatString = "N0"
        Me.cFNExpBalQty.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cFNExpBalQty.FieldName = "FNExpBalQty"
        Me.cFNExpBalQty.Name = "cFNExpBalQty"
        Me.cFNExpBalQty.OptionsColumn.AllowEdit = False
        Me.cFNExpBalQty.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.cFNExpBalQty.OptionsColumn.AllowMove = False
        Me.cFNExpBalQty.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.cFNExpBalQty.OptionsColumn.ReadOnly = True
        Me.cFNExpBalQty.OptionsColumn.ShowInCustomizationForm = False
        Me.cFNExpBalQty.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FNExpBalQty", "{0:N0}")})
        Me.cFNExpBalQty.Visible = True
        Me.cFNExpBalQty.VisibleIndex = 46
        '
        'XtraTabPage1
        '
        Me.XtraTabPage1.Controls.Add(Me.ogcmonthly)
        Me.XtraTabPage1.Name = "XtraTabPage1"
        Me.XtraTabPage1.Size = New System.Drawing.Size(1320, 594)
        Me.XtraTabPage1.Text = "Detail Monthly"
        '
        'ogcmonthly
        '
        Me.ogcmonthly.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogcmonthly.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcmonthly.Location = New System.Drawing.Point(0, 0)
        Me.ogcmonthly.MainView = Me.ogvmonthly
        Me.ogcmonthly.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcmonthly.Name = "ogcmonthly"
        Me.ogcmonthly.Size = New System.Drawing.Size(1320, 594)
        Me.ogcmonthly.TabIndex = 3
        Me.ogcmonthly.TabStop = False
        Me.ogcmonthly.Tag = "2|"
        Me.ogcmonthly.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvmonthly})
        '
        'ogvmonthly
        '
        Me.ogvmonthly.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.cFTMonthly, Me.cFTStyleCode, Me.cFTOrderNo, Me.cFTPORef, Me.cFTCmpCode, Me.mFTCmpName, Me.mFDShipDate, Me.mFTPOLineItemNo, Me.mFTColorway, Me.mFTSizeBreakDown, Me.mFNQuantity, Me.cFNOrderOnhand, Me.mFNQuantityExtra, Me.mFNGarmentQtyTest, Me.mFNGrandQuantity, Me.GridColumn14, Me.mFNCutQuantity, Me.cFNCutBFQty, Me.mFNCutBalQuantity, Me.cFNQtyEmbroideryBF, Me.mFNQtyEmbroidery, Me.mFNRcvQtyEmbroidery, Me.mFNBalQtyEmbroidery, Me.cFNQtyPrintBF, Me.mFNQtyPrint, Me.mFNRcvQtyPrint, Me.mFNBalQtyPrint, Me.cFNQtyHeatBF, Me.mFNQtyHeat, Me.mFNRcvQtyHeat, Me.mFNBalQtyHeat, Me.cFNQtyLaserBF, Me.mFNQtyLaser, Me.mFNRcvQtyLaser, Me.mFNBalQtyLaser, Me.cFNQtyPadPrintBF, Me.mFNQtyPadPrint, Me.mFNRcvQtyPadPrint, Me.mFNBalQtyPadPrint, Me.cFNSendSuplQuantityBF, Me.mFNSendSuplQuantity, Me.mFNRcvSuplQuantity, Me.mFNBalSuplQuantity, Me.cFNSPMKQuantityBF, Me.mFNSPMKQuantity, Me.cFNBalSPMKQuantity, Me.GridColumn40, Me.cFNSewQuantityBF, Me.mFNSewQuantity, Me.mFNSPMKQuantityBal, Me.mFNSewOutQuantity, Me.mFNBalSewQuantity, Me.cFNSewOutQuantityBF, Me.mFNPackQuantity, Me.mFNBalPackQuantity, Me.GridColumn47, Me.mFNFGInQty, Me.mFNFGBalQty, Me.cFNFGInQtyBF, Me.mFNExpQty, Me.mFNExpBalQty})
        Me.ogvmonthly.GridControl = Me.ogcmonthly
        Me.ogvmonthly.Name = "ogvmonthly"
        Me.ogvmonthly.OptionsCustomization.AllowQuickHideColumns = False
        Me.ogvmonthly.OptionsView.AllowCellMerge = True
        Me.ogvmonthly.OptionsView.ColumnAutoWidth = False
        Me.ogvmonthly.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.ogvmonthly.OptionsView.ShowGroupPanel = False
        Me.ogvmonthly.Tag = "2|"
        '
        'cFTMonthly
        '
        Me.cFTMonthly.Caption = "Monthly"
        Me.cFTMonthly.FieldName = "FTMonthly"
        Me.cFTMonthly.MinWidth = 25
        Me.cFTMonthly.Name = "cFTMonthly"
        Me.cFTMonthly.OptionsColumn.AllowEdit = False
        Me.cFTMonthly.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.cFTMonthly.Visible = True
        Me.cFTMonthly.VisibleIndex = 0
        Me.cFTMonthly.Width = 146
        '
        'cFTStyleCode
        '
        Me.cFTStyleCode.AppearanceHeader.Options.UseTextOptions = True
        Me.cFTStyleCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.cFTStyleCode.Caption = "FTStyleCode"
        Me.cFTStyleCode.FieldName = "FTStyleCode"
        Me.cFTStyleCode.Name = "cFTStyleCode"
        Me.cFTStyleCode.OptionsColumn.AllowEdit = False
        Me.cFTStyleCode.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.cFTStyleCode.OptionsColumn.ReadOnly = True
        Me.cFTStyleCode.Visible = True
        Me.cFTStyleCode.VisibleIndex = 1
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
        Me.cFTOrderNo.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.cFTOrderNo.OptionsColumn.AllowShowHide = False
        Me.cFTOrderNo.OptionsColumn.ReadOnly = True
        Me.cFTOrderNo.OptionsColumn.ShowInCustomizationForm = False
        Me.cFTOrderNo.Visible = True
        Me.cFTOrderNo.VisibleIndex = 3
        Me.cFTOrderNo.Width = 127
        '
        'cFTPORef
        '
        Me.cFTPORef.AppearanceHeader.Options.UseTextOptions = True
        Me.cFTPORef.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.cFTPORef.Caption = "FTPORef"
        Me.cFTPORef.FieldName = "FTPORef"
        Me.cFTPORef.Name = "cFTPORef"
        Me.cFTPORef.OptionsColumn.AllowEdit = False
        Me.cFTPORef.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.cFTPORef.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.cFTPORef.OptionsColumn.AllowShowHide = False
        Me.cFTPORef.OptionsColumn.ReadOnly = True
        Me.cFTPORef.Visible = True
        Me.cFTPORef.VisibleIndex = 2
        Me.cFTPORef.Width = 110
        '
        'cFTCmpCode
        '
        Me.cFTCmpCode.AppearanceHeader.Options.UseTextOptions = True
        Me.cFTCmpCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.cFTCmpCode.Caption = "FTCmpCode"
        Me.cFTCmpCode.FieldName = "FTCmpCode"
        Me.cFTCmpCode.Name = "cFTCmpCode"
        Me.cFTCmpCode.OptionsColumn.AllowEdit = False
        Me.cFTCmpCode.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.cFTCmpCode.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.cFTCmpCode.OptionsColumn.AllowShowHide = False
        Me.cFTCmpCode.OptionsColumn.ReadOnly = True
        Me.cFTCmpCode.OptionsColumn.ShowInCustomizationForm = False
        Me.cFTCmpCode.Visible = True
        Me.cFTCmpCode.VisibleIndex = 4
        Me.cFTCmpCode.Width = 100
        '
        'mFTCmpName
        '
        Me.mFTCmpName.AppearanceHeader.Options.UseTextOptions = True
        Me.mFTCmpName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.mFTCmpName.Caption = "FTCmpName"
        Me.mFTCmpName.FieldName = "FTCmpName"
        Me.mFTCmpName.Name = "mFTCmpName"
        Me.mFTCmpName.OptionsColumn.AllowEdit = False
        Me.mFTCmpName.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.mFTCmpName.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.mFTCmpName.OptionsColumn.AllowShowHide = False
        Me.mFTCmpName.OptionsColumn.ReadOnly = True
        Me.mFTCmpName.OptionsColumn.ShowInCustomizationForm = False
        Me.mFTCmpName.Visible = True
        Me.mFTCmpName.VisibleIndex = 5
        Me.mFTCmpName.Width = 120
        '
        'mFDShipDate
        '
        Me.mFDShipDate.AppearanceCell.Options.UseTextOptions = True
        Me.mFDShipDate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.mFDShipDate.AppearanceHeader.Options.UseTextOptions = True
        Me.mFDShipDate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.mFDShipDate.Caption = "FDShipDate"
        Me.mFDShipDate.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.mFDShipDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.mFDShipDate.FieldName = "FDShipDate"
        Me.mFDShipDate.Name = "mFDShipDate"
        Me.mFDShipDate.OptionsColumn.AllowEdit = False
        Me.mFDShipDate.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.mFDShipDate.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.mFDShipDate.OptionsColumn.AllowShowHide = False
        Me.mFDShipDate.OptionsColumn.ReadOnly = True
        Me.mFDShipDate.OptionsColumn.ShowInCustomizationForm = False
        Me.mFDShipDate.Visible = True
        Me.mFDShipDate.VisibleIndex = 6
        Me.mFDShipDate.Width = 80
        '
        'mFTPOLineItemNo
        '
        Me.mFTPOLineItemNo.AppearanceHeader.Options.UseTextOptions = True
        Me.mFTPOLineItemNo.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.mFTPOLineItemNo.Caption = "FTPOLineItemNo"
        Me.mFTPOLineItemNo.FieldName = "FTPOLineItemNo"
        Me.mFTPOLineItemNo.Name = "mFTPOLineItemNo"
        Me.mFTPOLineItemNo.OptionsColumn.AllowEdit = False
        Me.mFTPOLineItemNo.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.mFTPOLineItemNo.OptionsColumn.AllowShowHide = False
        Me.mFTPOLineItemNo.OptionsColumn.ReadOnly = True
        Me.mFTPOLineItemNo.OptionsColumn.ShowInCustomizationForm = False
        Me.mFTPOLineItemNo.Visible = True
        Me.mFTPOLineItemNo.VisibleIndex = 7
        Me.mFTPOLineItemNo.Width = 80
        '
        'mFTColorway
        '
        Me.mFTColorway.AppearanceHeader.Options.UseTextOptions = True
        Me.mFTColorway.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.mFTColorway.Caption = "FTColorway"
        Me.mFTColorway.FieldName = "FTColorway"
        Me.mFTColorway.Name = "mFTColorway"
        Me.mFTColorway.OptionsColumn.AllowEdit = False
        Me.mFTColorway.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.mFTColorway.OptionsColumn.AllowShowHide = False
        Me.mFTColorway.OptionsColumn.ReadOnly = True
        Me.mFTColorway.OptionsColumn.ShowInCustomizationForm = False
        Me.mFTColorway.Visible = True
        Me.mFTColorway.VisibleIndex = 8
        Me.mFTColorway.Width = 80
        '
        'mFTSizeBreakDown
        '
        Me.mFTSizeBreakDown.AppearanceHeader.Options.UseTextOptions = True
        Me.mFTSizeBreakDown.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.mFTSizeBreakDown.Caption = "FTSizeBreakDown"
        Me.mFTSizeBreakDown.FieldName = "FTSizeBreakDown"
        Me.mFTSizeBreakDown.Name = "mFTSizeBreakDown"
        Me.mFTSizeBreakDown.OptionsColumn.AllowEdit = False
        Me.mFTSizeBreakDown.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.mFTSizeBreakDown.OptionsColumn.AllowShowHide = False
        Me.mFTSizeBreakDown.OptionsColumn.ReadOnly = True
        Me.mFTSizeBreakDown.OptionsColumn.ShowInCustomizationForm = False
        Me.mFTSizeBreakDown.Visible = True
        Me.mFTSizeBreakDown.VisibleIndex = 9
        Me.mFTSizeBreakDown.Width = 60
        '
        'mFNQuantity
        '
        Me.mFNQuantity.AppearanceCell.Options.UseTextOptions = True
        Me.mFNQuantity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.mFNQuantity.AppearanceHeader.Options.UseTextOptions = True
        Me.mFNQuantity.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.mFNQuantity.Caption = "Order Quantity"
        Me.mFNQuantity.DisplayFormat.FormatString = "{0:n0}"
        Me.mFNQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.mFNQuantity.FieldName = "FNQuantity"
        Me.mFNQuantity.Name = "mFNQuantity"
        Me.mFNQuantity.OptionsColumn.AllowEdit = False
        Me.mFNQuantity.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.mFNQuantity.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.mFNQuantity.OptionsColumn.AllowShowHide = False
        Me.mFNQuantity.OptionsColumn.ReadOnly = True
        Me.mFNQuantity.OptionsColumn.ShowInCustomizationForm = False
        Me.mFNQuantity.Visible = True
        Me.mFNQuantity.VisibleIndex = 10
        Me.mFNQuantity.Width = 90
        '
        'cFNOrderOnhand
        '
        Me.cFNOrderOnhand.Caption = "Order Onhand"
        Me.cFNOrderOnhand.DisplayFormat.FormatString = "N0"
        Me.cFNOrderOnhand.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cFNOrderOnhand.FieldName = "FNOrderOnhand"
        Me.cFNOrderOnhand.MinWidth = 25
        Me.cFNOrderOnhand.Name = "cFNOrderOnhand"
        Me.cFNOrderOnhand.OptionsColumn.AllowEdit = False
        Me.cFNOrderOnhand.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.cFNOrderOnhand.Visible = True
        Me.cFNOrderOnhand.VisibleIndex = 11
        Me.cFNOrderOnhand.Width = 94
        '
        'mFNQuantityExtra
        '
        Me.mFNQuantityExtra.AppearanceCell.Options.UseTextOptions = True
        Me.mFNQuantityExtra.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.mFNQuantityExtra.AppearanceHeader.Options.UseTextOptions = True
        Me.mFNQuantityExtra.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.mFNQuantityExtra.Caption = "Extra"
        Me.mFNQuantityExtra.DisplayFormat.FormatString = "{0:n0}"
        Me.mFNQuantityExtra.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.mFNQuantityExtra.FieldName = "FNQuantityExtra"
        Me.mFNQuantityExtra.Name = "mFNQuantityExtra"
        Me.mFNQuantityExtra.OptionsColumn.AllowEdit = False
        Me.mFNQuantityExtra.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.mFNQuantityExtra.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.mFNQuantityExtra.OptionsColumn.AllowShowHide = False
        Me.mFNQuantityExtra.OptionsColumn.ReadOnly = True
        Me.mFNQuantityExtra.OptionsColumn.ShowInCustomizationForm = False
        Me.mFNQuantityExtra.Visible = True
        Me.mFNQuantityExtra.VisibleIndex = 12
        Me.mFNQuantityExtra.Width = 90
        '
        'mFNGarmentQtyTest
        '
        Me.mFNGarmentQtyTest.AppearanceCell.Options.UseTextOptions = True
        Me.mFNGarmentQtyTest.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.mFNGarmentQtyTest.AppearanceHeader.Options.UseTextOptions = True
        Me.mFNGarmentQtyTest.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.mFNGarmentQtyTest.Caption = "Test"
        Me.mFNGarmentQtyTest.DisplayFormat.FormatString = "{0:n0}"
        Me.mFNGarmentQtyTest.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.mFNGarmentQtyTest.FieldName = "FNGarmentQtyTest"
        Me.mFNGarmentQtyTest.Name = "mFNGarmentQtyTest"
        Me.mFNGarmentQtyTest.OptionsColumn.AllowEdit = False
        Me.mFNGarmentQtyTest.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.mFNGarmentQtyTest.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.mFNGarmentQtyTest.OptionsColumn.AllowShowHide = False
        Me.mFNGarmentQtyTest.OptionsColumn.ReadOnly = True
        Me.mFNGarmentQtyTest.OptionsColumn.ShowInCustomizationForm = False
        Me.mFNGarmentQtyTest.Visible = True
        Me.mFNGarmentQtyTest.VisibleIndex = 13
        Me.mFNGarmentQtyTest.Width = 90
        '
        'mFNGrandQuantity
        '
        Me.mFNGrandQuantity.AppearanceCell.Options.UseTextOptions = True
        Me.mFNGrandQuantity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.mFNGrandQuantity.AppearanceHeader.Options.UseTextOptions = True
        Me.mFNGrandQuantity.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.mFNGrandQuantity.Caption = "Total Quantity"
        Me.mFNGrandQuantity.DisplayFormat.FormatString = "{0:n0}"
        Me.mFNGrandQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.mFNGrandQuantity.FieldName = "FNGrandQuantity"
        Me.mFNGrandQuantity.Name = "mFNGrandQuantity"
        Me.mFNGrandQuantity.OptionsColumn.AllowEdit = False
        Me.mFNGrandQuantity.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.mFNGrandQuantity.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.mFNGrandQuantity.OptionsColumn.AllowShowHide = False
        Me.mFNGrandQuantity.OptionsColumn.ReadOnly = True
        Me.mFNGrandQuantity.OptionsColumn.ShowInCustomizationForm = False
        Me.mFNGrandQuantity.Visible = True
        Me.mFNGrandQuantity.VisibleIndex = 14
        Me.mFNGrandQuantity.Width = 90
        '
        'GridColumn14
        '
        Me.GridColumn14.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn14.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn14.Caption = "โต๊ะตัด"
        Me.GridColumn14.FieldName = "FTUnitSectCodeCut"
        Me.GridColumn14.Name = "GridColumn14"
        Me.GridColumn14.OptionsColumn.AllowEdit = False
        Me.GridColumn14.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn14.OptionsColumn.AllowShowHide = False
        Me.GridColumn14.OptionsColumn.ReadOnly = True
        Me.GridColumn14.OptionsColumn.ShowInCustomizationForm = False
        Me.GridColumn14.Width = 80
        '
        'mFNCutQuantity
        '
        Me.mFNCutQuantity.AppearanceCell.Options.UseTextOptions = True
        Me.mFNCutQuantity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.mFNCutQuantity.AppearanceHeader.Options.UseTextOptions = True
        Me.mFNCutQuantity.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.mFNCutQuantity.Caption = "Cut Quantity"
        Me.mFNCutQuantity.DisplayFormat.FormatString = "{0:n0}"
        Me.mFNCutQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.mFNCutQuantity.FieldName = "FNCutQuantity"
        Me.mFNCutQuantity.Name = "mFNCutQuantity"
        Me.mFNCutQuantity.OptionsColumn.AllowEdit = False
        Me.mFNCutQuantity.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.mFNCutQuantity.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.mFNCutQuantity.OptionsColumn.AllowShowHide = False
        Me.mFNCutQuantity.OptionsColumn.ReadOnly = True
        Me.mFNCutQuantity.OptionsColumn.ShowInCustomizationForm = False
        Me.mFNCutQuantity.Visible = True
        Me.mFNCutQuantity.VisibleIndex = 16
        Me.mFNCutQuantity.Width = 90
        '
        'cFNCutBFQty
        '
        Me.cFNCutBFQty.Caption = "Cut BF Qty"
        Me.cFNCutBFQty.DisplayFormat.FormatString = "N0"
        Me.cFNCutBFQty.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cFNCutBFQty.FieldName = "FNCutBFQty"
        Me.cFNCutBFQty.MinWidth = 25
        Me.cFNCutBFQty.Name = "cFNCutBFQty"
        Me.cFNCutBFQty.OptionsColumn.AllowEdit = False
        Me.cFNCutBFQty.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.cFNCutBFQty.Visible = True
        Me.cFNCutBFQty.VisibleIndex = 15
        Me.cFNCutBFQty.Width = 94
        '
        'mFNCutBalQuantity
        '
        Me.mFNCutBalQuantity.AppearanceCell.Options.UseTextOptions = True
        Me.mFNCutBalQuantity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.mFNCutBalQuantity.AppearanceHeader.Options.UseTextOptions = True
        Me.mFNCutBalQuantity.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.mFNCutBalQuantity.Caption = "Bal Cut Quantity"
        Me.mFNCutBalQuantity.DisplayFormat.FormatString = "{0:n0}"
        Me.mFNCutBalQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.mFNCutBalQuantity.FieldName = "FNCutBalQuantity"
        Me.mFNCutBalQuantity.Name = "mFNCutBalQuantity"
        Me.mFNCutBalQuantity.OptionsColumn.AllowEdit = False
        Me.mFNCutBalQuantity.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.mFNCutBalQuantity.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.mFNCutBalQuantity.OptionsColumn.AllowShowHide = False
        Me.mFNCutBalQuantity.OptionsColumn.ReadOnly = True
        Me.mFNCutBalQuantity.OptionsColumn.ShowInCustomizationForm = False
        Me.mFNCutBalQuantity.Visible = True
        Me.mFNCutBalQuantity.VisibleIndex = 17
        Me.mFNCutBalQuantity.Width = 90
        '
        'cFNQtyEmbroideryBF
        '
        Me.cFNQtyEmbroideryBF.Caption = " Qty Embroidery BF"
        Me.cFNQtyEmbroideryBF.DisplayFormat.FormatString = "N0"
        Me.cFNQtyEmbroideryBF.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cFNQtyEmbroideryBF.FieldName = "FNQtyEmbroideryBF"
        Me.cFNQtyEmbroideryBF.MinWidth = 25
        Me.cFNQtyEmbroideryBF.Name = "cFNQtyEmbroideryBF"
        Me.cFNQtyEmbroideryBF.OptionsColumn.AllowEdit = False
        Me.cFNQtyEmbroideryBF.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.cFNQtyEmbroideryBF.Visible = True
        Me.cFNQtyEmbroideryBF.VisibleIndex = 18
        Me.cFNQtyEmbroideryBF.Width = 94
        '
        'mFNQtyEmbroidery
        '
        Me.mFNQtyEmbroidery.Caption = "ส่งปัก"
        Me.mFNQtyEmbroidery.DisplayFormat.FormatString = "{0:n0}"
        Me.mFNQtyEmbroidery.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.mFNQtyEmbroidery.FieldName = "FNQtyEmbroidery"
        Me.mFNQtyEmbroidery.Name = "mFNQtyEmbroidery"
        Me.mFNQtyEmbroidery.OptionsColumn.AllowEdit = False
        Me.mFNQtyEmbroidery.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.mFNQtyEmbroidery.OptionsColumn.ReadOnly = True
        Me.mFNQtyEmbroidery.Visible = True
        Me.mFNQtyEmbroidery.VisibleIndex = 19
        '
        'mFNRcvQtyEmbroidery
        '
        Me.mFNRcvQtyEmbroidery.Caption = "รับปัก"
        Me.mFNRcvQtyEmbroidery.DisplayFormat.FormatString = "{0:n0}"
        Me.mFNRcvQtyEmbroidery.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.mFNRcvQtyEmbroidery.FieldName = "FNRcvQtyEmbroidery"
        Me.mFNRcvQtyEmbroidery.Name = "mFNRcvQtyEmbroidery"
        Me.mFNRcvQtyEmbroidery.OptionsColumn.AllowEdit = False
        Me.mFNRcvQtyEmbroidery.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.mFNRcvQtyEmbroidery.OptionsColumn.ReadOnly = True
        Me.mFNRcvQtyEmbroidery.Visible = True
        Me.mFNRcvQtyEmbroidery.VisibleIndex = 20
        '
        'mFNBalQtyEmbroidery
        '
        Me.mFNBalQtyEmbroidery.Caption = "ปักคงค้าง"
        Me.mFNBalQtyEmbroidery.DisplayFormat.FormatString = "{0:n0}"
        Me.mFNBalQtyEmbroidery.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.mFNBalQtyEmbroidery.FieldName = "FNBalQtyEmbroidery"
        Me.mFNBalQtyEmbroidery.Name = "mFNBalQtyEmbroidery"
        Me.mFNBalQtyEmbroidery.OptionsColumn.AllowEdit = False
        Me.mFNBalQtyEmbroidery.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.mFNBalQtyEmbroidery.OptionsColumn.ReadOnly = True
        Me.mFNBalQtyEmbroidery.Visible = True
        Me.mFNBalQtyEmbroidery.VisibleIndex = 21
        '
        'cFNQtyPrintBF
        '
        Me.cFNQtyPrintBF.Caption = "FNQtyPrintBF"
        Me.cFNQtyPrintBF.DisplayFormat.FormatString = "N0"
        Me.cFNQtyPrintBF.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cFNQtyPrintBF.FieldName = "FNQtyPrintBF"
        Me.cFNQtyPrintBF.MinWidth = 25
        Me.cFNQtyPrintBF.Name = "cFNQtyPrintBF"
        Me.cFNQtyPrintBF.OptionsColumn.AllowEdit = False
        Me.cFNQtyPrintBF.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.cFNQtyPrintBF.Visible = True
        Me.cFNQtyPrintBF.VisibleIndex = 22
        Me.cFNQtyPrintBF.Width = 94
        '
        'mFNQtyPrint
        '
        Me.mFNQtyPrint.Caption = "ส่งพิมพ์"
        Me.mFNQtyPrint.DisplayFormat.FormatString = "{0:n0}"
        Me.mFNQtyPrint.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.mFNQtyPrint.FieldName = "FNQtyPrint"
        Me.mFNQtyPrint.Name = "mFNQtyPrint"
        Me.mFNQtyPrint.OptionsColumn.AllowEdit = False
        Me.mFNQtyPrint.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.mFNQtyPrint.OptionsColumn.ReadOnly = True
        Me.mFNQtyPrint.Visible = True
        Me.mFNQtyPrint.VisibleIndex = 23
        '
        'mFNRcvQtyPrint
        '
        Me.mFNRcvQtyPrint.Caption = "รับพิมพ์"
        Me.mFNRcvQtyPrint.DisplayFormat.FormatString = "{0:n0}"
        Me.mFNRcvQtyPrint.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.mFNRcvQtyPrint.FieldName = "FNRcvQtyPrint"
        Me.mFNRcvQtyPrint.Name = "mFNRcvQtyPrint"
        Me.mFNRcvQtyPrint.OptionsColumn.AllowEdit = False
        Me.mFNRcvQtyPrint.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.mFNRcvQtyPrint.OptionsColumn.ReadOnly = True
        Me.mFNRcvQtyPrint.Visible = True
        Me.mFNRcvQtyPrint.VisibleIndex = 24
        '
        'mFNBalQtyPrint
        '
        Me.mFNBalQtyPrint.Caption = "พิมคงค้าง"
        Me.mFNBalQtyPrint.DisplayFormat.FormatString = "{0:n0}"
        Me.mFNBalQtyPrint.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.mFNBalQtyPrint.FieldName = "FNBalQtyPrint"
        Me.mFNBalQtyPrint.Name = "mFNBalQtyPrint"
        Me.mFNBalQtyPrint.OptionsColumn.AllowEdit = False
        Me.mFNBalQtyPrint.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.mFNBalQtyPrint.OptionsColumn.ReadOnly = True
        Me.mFNBalQtyPrint.Visible = True
        Me.mFNBalQtyPrint.VisibleIndex = 25
        '
        'cFNQtyHeatBF
        '
        Me.cFNQtyHeatBF.Caption = "FNQtyHeatBF"
        Me.cFNQtyHeatBF.DisplayFormat.FormatString = "N0"
        Me.cFNQtyHeatBF.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cFNQtyHeatBF.FieldName = "FNQtyHeatBF"
        Me.cFNQtyHeatBF.MinWidth = 25
        Me.cFNQtyHeatBF.Name = "cFNQtyHeatBF"
        Me.cFNQtyHeatBF.OptionsColumn.AllowEdit = False
        Me.cFNQtyHeatBF.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.cFNQtyHeatBF.Visible = True
        Me.cFNQtyHeatBF.VisibleIndex = 26
        Me.cFNQtyHeatBF.Width = 94
        '
        'mFNQtyHeat
        '
        Me.mFNQtyHeat.Caption = "Heat"
        Me.mFNQtyHeat.DisplayFormat.FormatString = "{0:n0}"
        Me.mFNQtyHeat.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.mFNQtyHeat.FieldName = "FNQtyHeat"
        Me.mFNQtyHeat.Name = "mFNQtyHeat"
        Me.mFNQtyHeat.OptionsColumn.AllowEdit = False
        Me.mFNQtyHeat.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.mFNQtyHeat.OptionsColumn.ReadOnly = True
        Me.mFNQtyHeat.Visible = True
        Me.mFNQtyHeat.VisibleIndex = 27
        '
        'mFNRcvQtyHeat
        '
        Me.mFNRcvQtyHeat.Caption = "Heat Rcv."
        Me.mFNRcvQtyHeat.DisplayFormat.FormatString = "{0:n0}"
        Me.mFNRcvQtyHeat.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.mFNRcvQtyHeat.FieldName = "FNRcvQtyHeat"
        Me.mFNRcvQtyHeat.Name = "mFNRcvQtyHeat"
        Me.mFNRcvQtyHeat.OptionsColumn.AllowEdit = False
        Me.mFNRcvQtyHeat.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.mFNRcvQtyHeat.OptionsColumn.ReadOnly = True
        Me.mFNRcvQtyHeat.Visible = True
        Me.mFNRcvQtyHeat.VisibleIndex = 28
        '
        'mFNBalQtyHeat
        '
        Me.mFNBalQtyHeat.Caption = "Heat Bal."
        Me.mFNBalQtyHeat.DisplayFormat.FormatString = "{0:n0}"
        Me.mFNBalQtyHeat.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.mFNBalQtyHeat.FieldName = "FNBalQtyHeat"
        Me.mFNBalQtyHeat.Name = "mFNBalQtyHeat"
        Me.mFNBalQtyHeat.OptionsColumn.AllowEdit = False
        Me.mFNBalQtyHeat.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.mFNBalQtyHeat.OptionsColumn.ReadOnly = True
        Me.mFNBalQtyHeat.Visible = True
        Me.mFNBalQtyHeat.VisibleIndex = 29
        '
        'cFNQtyLaserBF
        '
        Me.cFNQtyLaserBF.Caption = "FNQtyLaserBF"
        Me.cFNQtyLaserBF.DisplayFormat.FormatString = "N0"
        Me.cFNQtyLaserBF.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cFNQtyLaserBF.FieldName = "FNQtyLaserBF"
        Me.cFNQtyLaserBF.MinWidth = 25
        Me.cFNQtyLaserBF.Name = "cFNQtyLaserBF"
        Me.cFNQtyLaserBF.OptionsColumn.AllowEdit = False
        Me.cFNQtyLaserBF.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.cFNQtyLaserBF.Visible = True
        Me.cFNQtyLaserBF.VisibleIndex = 30
        Me.cFNQtyLaserBF.Width = 94
        '
        'mFNQtyLaser
        '
        Me.mFNQtyLaser.Caption = "Laser"
        Me.mFNQtyLaser.DisplayFormat.FormatString = "{0:n0}"
        Me.mFNQtyLaser.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.mFNQtyLaser.FieldName = "FNQtyLaser"
        Me.mFNQtyLaser.Name = "mFNQtyLaser"
        Me.mFNQtyLaser.OptionsColumn.AllowEdit = False
        Me.mFNQtyLaser.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.mFNQtyLaser.OptionsColumn.ReadOnly = True
        Me.mFNQtyLaser.Visible = True
        Me.mFNQtyLaser.VisibleIndex = 31
        '
        'mFNRcvQtyLaser
        '
        Me.mFNRcvQtyLaser.Caption = "Laser Rcv."
        Me.mFNRcvQtyLaser.DisplayFormat.FormatString = "{0:n0}"
        Me.mFNRcvQtyLaser.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.mFNRcvQtyLaser.FieldName = "FNRcvQtyLaser"
        Me.mFNRcvQtyLaser.Name = "mFNRcvQtyLaser"
        Me.mFNRcvQtyLaser.OptionsColumn.AllowEdit = False
        Me.mFNRcvQtyLaser.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.mFNRcvQtyLaser.OptionsColumn.ReadOnly = True
        Me.mFNRcvQtyLaser.Visible = True
        Me.mFNRcvQtyLaser.VisibleIndex = 32
        '
        'mFNBalQtyLaser
        '
        Me.mFNBalQtyLaser.Caption = "Laser Bal."
        Me.mFNBalQtyLaser.DisplayFormat.FormatString = "{0:n0}"
        Me.mFNBalQtyLaser.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.mFNBalQtyLaser.FieldName = "FNBalQtyLaser"
        Me.mFNBalQtyLaser.Name = "mFNBalQtyLaser"
        Me.mFNBalQtyLaser.OptionsColumn.AllowEdit = False
        Me.mFNBalQtyLaser.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.mFNBalQtyLaser.OptionsColumn.ReadOnly = True
        Me.mFNBalQtyLaser.Visible = True
        Me.mFNBalQtyLaser.VisibleIndex = 33
        '
        'cFNQtyPadPrintBF
        '
        Me.cFNQtyPadPrintBF.Caption = "FNQtyPadPrintBF"
        Me.cFNQtyPadPrintBF.DisplayFormat.FormatString = "N0"
        Me.cFNQtyPadPrintBF.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cFNQtyPadPrintBF.FieldName = "FNQtyPadPrintBF"
        Me.cFNQtyPadPrintBF.MinWidth = 25
        Me.cFNQtyPadPrintBF.Name = "cFNQtyPadPrintBF"
        Me.cFNQtyPadPrintBF.OptionsColumn.AllowEdit = False
        Me.cFNQtyPadPrintBF.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.cFNQtyPadPrintBF.Visible = True
        Me.cFNQtyPadPrintBF.VisibleIndex = 34
        Me.cFNQtyPadPrintBF.Width = 94
        '
        'mFNQtyPadPrint
        '
        Me.mFNQtyPadPrint.Caption = "Pad Print"
        Me.mFNQtyPadPrint.DisplayFormat.FormatString = "{0:n0}"
        Me.mFNQtyPadPrint.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.mFNQtyPadPrint.FieldName = "FNQtyPadPrint"
        Me.mFNQtyPadPrint.Name = "mFNQtyPadPrint"
        Me.mFNQtyPadPrint.OptionsColumn.AllowEdit = False
        Me.mFNQtyPadPrint.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.mFNQtyPadPrint.OptionsColumn.ReadOnly = True
        Me.mFNQtyPadPrint.Visible = True
        Me.mFNQtyPadPrint.VisibleIndex = 35
        '
        'mFNRcvQtyPadPrint
        '
        Me.mFNRcvQtyPadPrint.Caption = "Pad Print Rcv."
        Me.mFNRcvQtyPadPrint.DisplayFormat.FormatString = "{0:n0}"
        Me.mFNRcvQtyPadPrint.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.mFNRcvQtyPadPrint.FieldName = "FNRcvQtyPadPrint"
        Me.mFNRcvQtyPadPrint.Name = "mFNRcvQtyPadPrint"
        Me.mFNRcvQtyPadPrint.OptionsColumn.AllowEdit = False
        Me.mFNRcvQtyPadPrint.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.mFNRcvQtyPadPrint.OptionsColumn.ReadOnly = True
        Me.mFNRcvQtyPadPrint.Visible = True
        Me.mFNRcvQtyPadPrint.VisibleIndex = 36
        '
        'mFNBalQtyPadPrint
        '
        Me.mFNBalQtyPadPrint.Caption = "Pad Print Bal."
        Me.mFNBalQtyPadPrint.DisplayFormat.FormatString = "{0:n0}"
        Me.mFNBalQtyPadPrint.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.mFNBalQtyPadPrint.FieldName = "FNBalQtyPadPrint"
        Me.mFNBalQtyPadPrint.Name = "mFNBalQtyPadPrint"
        Me.mFNBalQtyPadPrint.OptionsColumn.AllowEdit = False
        Me.mFNBalQtyPadPrint.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.mFNBalQtyPadPrint.OptionsColumn.ReadOnly = True
        Me.mFNBalQtyPadPrint.Visible = True
        Me.mFNBalQtyPadPrint.VisibleIndex = 37
        '
        'cFNSendSuplQuantityBF
        '
        Me.cFNSendSuplQuantityBF.Caption = "FNSendSuplQuantityBF"
        Me.cFNSendSuplQuantityBF.DisplayFormat.FormatString = "N0"
        Me.cFNSendSuplQuantityBF.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cFNSendSuplQuantityBF.FieldName = "FNSendSuplQuantityBF"
        Me.cFNSendSuplQuantityBF.MinWidth = 25
        Me.cFNSendSuplQuantityBF.Name = "cFNSendSuplQuantityBF"
        Me.cFNSendSuplQuantityBF.OptionsColumn.AllowEdit = False
        Me.cFNSendSuplQuantityBF.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.cFNSendSuplQuantityBF.Visible = True
        Me.cFNSendSuplQuantityBF.VisibleIndex = 38
        Me.cFNSendSuplQuantityBF.Width = 94
        '
        'mFNSendSuplQuantity
        '
        Me.mFNSendSuplQuantity.AppearanceCell.Options.UseTextOptions = True
        Me.mFNSendSuplQuantity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.mFNSendSuplQuantity.AppearanceHeader.Options.UseTextOptions = True
        Me.mFNSendSuplQuantity.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.mFNSendSuplQuantity.Caption = "Send Supl Quantity"
        Me.mFNSendSuplQuantity.DisplayFormat.FormatString = "{0:n0}"
        Me.mFNSendSuplQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.mFNSendSuplQuantity.FieldName = "FNSendSuplQuantity"
        Me.mFNSendSuplQuantity.Name = "mFNSendSuplQuantity"
        Me.mFNSendSuplQuantity.OptionsColumn.AllowEdit = False
        Me.mFNSendSuplQuantity.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.mFNSendSuplQuantity.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.mFNSendSuplQuantity.OptionsColumn.AllowShowHide = False
        Me.mFNSendSuplQuantity.OptionsColumn.ReadOnly = True
        Me.mFNSendSuplQuantity.OptionsColumn.ShowInCustomizationForm = False
        Me.mFNSendSuplQuantity.Visible = True
        Me.mFNSendSuplQuantity.VisibleIndex = 39
        Me.mFNSendSuplQuantity.Width = 90
        '
        'mFNRcvSuplQuantity
        '
        Me.mFNRcvSuplQuantity.AppearanceCell.Options.UseTextOptions = True
        Me.mFNRcvSuplQuantity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.mFNRcvSuplQuantity.AppearanceHeader.Options.UseTextOptions = True
        Me.mFNRcvSuplQuantity.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.mFNRcvSuplQuantity.Caption = "Rcv Supl Quantity"
        Me.mFNRcvSuplQuantity.DisplayFormat.FormatString = "{0:n0}"
        Me.mFNRcvSuplQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.mFNRcvSuplQuantity.FieldName = "FNRcvSuplQuantity"
        Me.mFNRcvSuplQuantity.Name = "mFNRcvSuplQuantity"
        Me.mFNRcvSuplQuantity.OptionsColumn.AllowEdit = False
        Me.mFNRcvSuplQuantity.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.mFNRcvSuplQuantity.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.mFNRcvSuplQuantity.OptionsColumn.AllowShowHide = False
        Me.mFNRcvSuplQuantity.OptionsColumn.ReadOnly = True
        Me.mFNRcvSuplQuantity.OptionsColumn.ShowInCustomizationForm = False
        Me.mFNRcvSuplQuantity.Visible = True
        Me.mFNRcvSuplQuantity.VisibleIndex = 40
        Me.mFNRcvSuplQuantity.Width = 90
        '
        'mFNBalSuplQuantity
        '
        Me.mFNBalSuplQuantity.AppearanceCell.Options.UseTextOptions = True
        Me.mFNBalSuplQuantity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.mFNBalSuplQuantity.AppearanceHeader.Options.UseTextOptions = True
        Me.mFNBalSuplQuantity.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.mFNBalSuplQuantity.Caption = "Bal Supl Qty"
        Me.mFNBalSuplQuantity.DisplayFormat.FormatString = "{0:n0}"
        Me.mFNBalSuplQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.mFNBalSuplQuantity.FieldName = "FNBalSuplQuantity"
        Me.mFNBalSuplQuantity.Name = "mFNBalSuplQuantity"
        Me.mFNBalSuplQuantity.OptionsColumn.AllowEdit = False
        Me.mFNBalSuplQuantity.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.mFNBalSuplQuantity.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.mFNBalSuplQuantity.OptionsColumn.AllowShowHide = False
        Me.mFNBalSuplQuantity.OptionsColumn.ReadOnly = True
        Me.mFNBalSuplQuantity.OptionsColumn.ShowInCustomizationForm = False
        Me.mFNBalSuplQuantity.Visible = True
        Me.mFNBalSuplQuantity.VisibleIndex = 42
        Me.mFNBalSuplQuantity.Width = 90
        '
        'cFNSPMKQuantityBF
        '
        Me.cFNSPMKQuantityBF.Caption = "FNSPMKQuantityBF"
        Me.cFNSPMKQuantityBF.DisplayFormat.FormatString = "N0"
        Me.cFNSPMKQuantityBF.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cFNSPMKQuantityBF.FieldName = "FNSPMKQuantityBF"
        Me.cFNSPMKQuantityBF.MinWidth = 25
        Me.cFNSPMKQuantityBF.Name = "cFNSPMKQuantityBF"
        Me.cFNSPMKQuantityBF.OptionsColumn.AllowEdit = False
        Me.cFNSPMKQuantityBF.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.cFNSPMKQuantityBF.Visible = True
        Me.cFNSPMKQuantityBF.VisibleIndex = 43
        Me.cFNSPMKQuantityBF.Width = 94
        '
        'mFNSPMKQuantity
        '
        Me.mFNSPMKQuantity.AppearanceCell.Options.UseTextOptions = True
        Me.mFNSPMKQuantity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.mFNSPMKQuantity.AppearanceHeader.Options.UseTextOptions = True
        Me.mFNSPMKQuantity.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.mFNSPMKQuantity.Caption = "Supper Market"
        Me.mFNSPMKQuantity.DisplayFormat.FormatString = "{0:n0}"
        Me.mFNSPMKQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.mFNSPMKQuantity.FieldName = "FNSPMKQuantity"
        Me.mFNSPMKQuantity.Name = "mFNSPMKQuantity"
        Me.mFNSPMKQuantity.OptionsColumn.AllowEdit = False
        Me.mFNSPMKQuantity.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.mFNSPMKQuantity.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.mFNSPMKQuantity.OptionsColumn.AllowShowHide = False
        Me.mFNSPMKQuantity.OptionsColumn.ReadOnly = True
        Me.mFNSPMKQuantity.OptionsColumn.ShowInCustomizationForm = False
        Me.mFNSPMKQuantity.Visible = True
        Me.mFNSPMKQuantity.VisibleIndex = 41
        Me.mFNSPMKQuantity.Width = 90
        '
        'cFNBalSPMKQuantity
        '
        Me.cFNBalSPMKQuantity.AppearanceCell.Options.UseTextOptions = True
        Me.cFNBalSPMKQuantity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.cFNBalSPMKQuantity.AppearanceHeader.Options.UseTextOptions = True
        Me.cFNBalSPMKQuantity.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.cFNBalSPMKQuantity.Caption = "FNBalSPMKQuantity"
        Me.cFNBalSPMKQuantity.DisplayFormat.FormatString = "{0:n0}"
        Me.cFNBalSPMKQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cFNBalSPMKQuantity.FieldName = "FNBalCutQuantity"
        Me.cFNBalSPMKQuantity.Name = "cFNBalSPMKQuantity"
        Me.cFNBalSPMKQuantity.OptionsColumn.AllowEdit = False
        Me.cFNBalSPMKQuantity.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.cFNBalSPMKQuantity.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.cFNBalSPMKQuantity.OptionsColumn.AllowShowHide = False
        Me.cFNBalSPMKQuantity.OptionsColumn.ReadOnly = True
        Me.cFNBalSPMKQuantity.OptionsColumn.ShowInCustomizationForm = False
        Me.cFNBalSPMKQuantity.Visible = True
        Me.cFNBalSPMKQuantity.VisibleIndex = 44
        Me.cFNBalSPMKQuantity.Width = 90
        '
        'GridColumn40
        '
        Me.GridColumn40.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn40.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn40.Caption = "ไลน์เย็บ"
        Me.GridColumn40.FieldName = "FTUnitSectCodeSew"
        Me.GridColumn40.Name = "GridColumn40"
        Me.GridColumn40.OptionsColumn.AllowEdit = False
        Me.GridColumn40.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn40.OptionsColumn.AllowShowHide = False
        Me.GridColumn40.OptionsColumn.ReadOnly = True
        Me.GridColumn40.OptionsColumn.ShowInCustomizationForm = False
        Me.GridColumn40.Width = 80
        '
        'cFNSewQuantityBF
        '
        Me.cFNSewQuantityBF.Caption = "FNSewQuantityBF"
        Me.cFNSewQuantityBF.DisplayFormat.FormatString = "N0"
        Me.cFNSewQuantityBF.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cFNSewQuantityBF.FieldName = "FNSewQuantityBF"
        Me.cFNSewQuantityBF.MinWidth = 25
        Me.cFNSewQuantityBF.Name = "cFNSewQuantityBF"
        Me.cFNSewQuantityBF.OptionsColumn.AllowEdit = False
        Me.cFNSewQuantityBF.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.cFNSewQuantityBF.Visible = True
        Me.cFNSewQuantityBF.VisibleIndex = 45
        Me.cFNSewQuantityBF.Width = 94
        '
        'mFNSewQuantity
        '
        Me.mFNSewQuantity.AppearanceCell.Options.UseTextOptions = True
        Me.mFNSewQuantity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.mFNSewQuantity.AppearanceHeader.Options.UseTextOptions = True
        Me.mFNSewQuantity.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.mFNSewQuantity.Caption = "เข้าไลน์"
        Me.mFNSewQuantity.DisplayFormat.FormatString = "{0:n0}"
        Me.mFNSewQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.mFNSewQuantity.FieldName = "FNSewQuantity"
        Me.mFNSewQuantity.Name = "mFNSewQuantity"
        Me.mFNSewQuantity.OptionsColumn.AllowEdit = False
        Me.mFNSewQuantity.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.mFNSewQuantity.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.mFNSewQuantity.OptionsColumn.AllowShowHide = False
        Me.mFNSewQuantity.OptionsColumn.ReadOnly = True
        Me.mFNSewQuantity.OptionsColumn.ShowInCustomizationForm = False
        Me.mFNSewQuantity.Visible = True
        Me.mFNSewQuantity.VisibleIndex = 46
        Me.mFNSewQuantity.Width = 90
        '
        'mFNSPMKQuantityBal
        '
        Me.mFNSPMKQuantityBal.Caption = "SPM Bal."
        Me.mFNSPMKQuantityBal.DisplayFormat.FormatString = "{0:n0}"
        Me.mFNSPMKQuantityBal.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.mFNSPMKQuantityBal.FieldName = "FNSPMKQuantityBal"
        Me.mFNSPMKQuantityBal.Name = "mFNSPMKQuantityBal"
        Me.mFNSPMKQuantityBal.OptionsColumn.AllowEdit = False
        Me.mFNSPMKQuantityBal.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.mFNSPMKQuantityBal.OptionsColumn.ReadOnly = True
        '
        'mFNSewOutQuantity
        '
        Me.mFNSewOutQuantity.AppearanceCell.Options.UseTextOptions = True
        Me.mFNSewOutQuantity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.mFNSewOutQuantity.AppearanceHeader.Options.UseTextOptions = True
        Me.mFNSewOutQuantity.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.mFNSewOutQuantity.Caption = "ท้ายไลน์"
        Me.mFNSewOutQuantity.DisplayFormat.FormatString = "{0:n0}"
        Me.mFNSewOutQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.mFNSewOutQuantity.FieldName = "FNSewOutQuantity"
        Me.mFNSewOutQuantity.Name = "mFNSewOutQuantity"
        Me.mFNSewOutQuantity.OptionsColumn.AllowEdit = False
        Me.mFNSewOutQuantity.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.mFNSewOutQuantity.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.mFNSewOutQuantity.OptionsColumn.AllowShowHide = False
        Me.mFNSewOutQuantity.OptionsColumn.ReadOnly = True
        Me.mFNSewOutQuantity.OptionsColumn.ShowInCustomizationForm = False
        Me.mFNSewOutQuantity.Visible = True
        Me.mFNSewOutQuantity.VisibleIndex = 48
        Me.mFNSewOutQuantity.Width = 90
        '
        'mFNBalSewQuantity
        '
        Me.mFNBalSewQuantity.AppearanceCell.Options.UseTextOptions = True
        Me.mFNBalSewQuantity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.mFNBalSewQuantity.AppearanceHeader.Options.UseTextOptions = True
        Me.mFNBalSewQuantity.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.mFNBalSewQuantity.Caption = "Bal Sew Qty"
        Me.mFNBalSewQuantity.DisplayFormat.FormatString = "{0:n0}"
        Me.mFNBalSewQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.mFNBalSewQuantity.FieldName = "FNBalSewQuantity"
        Me.mFNBalSewQuantity.Name = "mFNBalSewQuantity"
        Me.mFNBalSewQuantity.OptionsColumn.AllowEdit = False
        Me.mFNBalSewQuantity.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.mFNBalSewQuantity.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.mFNBalSewQuantity.OptionsColumn.AllowShowHide = False
        Me.mFNBalSewQuantity.OptionsColumn.ReadOnly = True
        Me.mFNBalSewQuantity.OptionsColumn.ShowInCustomizationForm = False
        Me.mFNBalSewQuantity.Visible = True
        Me.mFNBalSewQuantity.VisibleIndex = 49
        Me.mFNBalSewQuantity.Width = 90
        '
        'cFNSewOutQuantityBF
        '
        Me.cFNSewOutQuantityBF.Caption = "FNSewOutQuantityBF"
        Me.cFNSewOutQuantityBF.DisplayFormat.FormatString = "N0"
        Me.cFNSewOutQuantityBF.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cFNSewOutQuantityBF.FieldName = "FNSewOutQuantityBF"
        Me.cFNSewOutQuantityBF.MinWidth = 25
        Me.cFNSewOutQuantityBF.Name = "cFNSewOutQuantityBF"
        Me.cFNSewOutQuantityBF.OptionsColumn.AllowEdit = False
        Me.cFNSewOutQuantityBF.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.cFNSewOutQuantityBF.Visible = True
        Me.cFNSewOutQuantityBF.VisibleIndex = 47
        Me.cFNSewOutQuantityBF.Width = 94
        '
        'mFNPackQuantity
        '
        Me.mFNPackQuantity.AppearanceCell.Options.UseTextOptions = True
        Me.mFNPackQuantity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.mFNPackQuantity.AppearanceHeader.Options.UseTextOptions = True
        Me.mFNPackQuantity.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.mFNPackQuantity.Caption = "Pack Quantity"
        Me.mFNPackQuantity.DisplayFormat.FormatString = "{0:n0}"
        Me.mFNPackQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.mFNPackQuantity.FieldName = "FNPackQuantity"
        Me.mFNPackQuantity.Name = "mFNPackQuantity"
        Me.mFNPackQuantity.OptionsColumn.AllowEdit = False
        Me.mFNPackQuantity.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.mFNPackQuantity.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.mFNPackQuantity.OptionsColumn.AllowShowHide = False
        Me.mFNPackQuantity.OptionsColumn.ReadOnly = True
        Me.mFNPackQuantity.OptionsColumn.ShowInCustomizationForm = False
        Me.mFNPackQuantity.Visible = True
        Me.mFNPackQuantity.VisibleIndex = 50
        Me.mFNPackQuantity.Width = 90
        '
        'mFNBalPackQuantity
        '
        Me.mFNBalPackQuantity.AppearanceCell.Options.UseTextOptions = True
        Me.mFNBalPackQuantity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.mFNBalPackQuantity.AppearanceHeader.Options.UseTextOptions = True
        Me.mFNBalPackQuantity.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.mFNBalPackQuantity.Caption = "FNBalPackQuantity"
        Me.mFNBalPackQuantity.DisplayFormat.FormatString = "{0:n0}"
        Me.mFNBalPackQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.mFNBalPackQuantity.FieldName = "FNBalPackQuantity"
        Me.mFNBalPackQuantity.Name = "mFNBalPackQuantity"
        Me.mFNBalPackQuantity.OptionsColumn.AllowEdit = False
        Me.mFNBalPackQuantity.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.mFNBalPackQuantity.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.mFNBalPackQuantity.OptionsColumn.AllowShowHide = False
        Me.mFNBalPackQuantity.OptionsColumn.ReadOnly = True
        Me.mFNBalPackQuantity.OptionsColumn.ShowInCustomizationForm = False
        Me.mFNBalPackQuantity.Visible = True
        Me.mFNBalPackQuantity.VisibleIndex = 51
        Me.mFNBalPackQuantity.Width = 90
        '
        'GridColumn47
        '
        Me.GridColumn47.Caption = "FNTableCut"
        Me.GridColumn47.FieldName = "FNTableCut"
        Me.GridColumn47.Name = "GridColumn47"
        Me.GridColumn47.OptionsColumn.AllowEdit = False
        Me.GridColumn47.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn47.OptionsColumn.AllowShowHide = False
        Me.GridColumn47.OptionsColumn.ShowInCustomizationForm = False
        '
        'mFNFGInQty
        '
        Me.mFNFGInQty.Caption = "FG. In WH"
        Me.mFNFGInQty.DisplayFormat.FormatString = "N0"
        Me.mFNFGInQty.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.mFNFGInQty.FieldName = "FNFGInQty"
        Me.mFNFGInQty.Name = "mFNFGInQty"
        Me.mFNFGInQty.OptionsColumn.AllowEdit = False
        Me.mFNFGInQty.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.mFNFGInQty.OptionsColumn.AllowMove = False
        Me.mFNFGInQty.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.mFNFGInQty.OptionsColumn.ReadOnly = True
        Me.mFNFGInQty.OptionsColumn.ShowInCustomizationForm = False
        Me.mFNFGInQty.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FNFGInQty", "{0:N0}")})
        Me.mFNFGInQty.Visible = True
        Me.mFNFGInQty.VisibleIndex = 52
        '
        'mFNFGBalQty
        '
        Me.mFNFGBalQty.Caption = "FG Bal WH"
        Me.mFNFGBalQty.DisplayFormat.FormatString = "N0"
        Me.mFNFGBalQty.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.mFNFGBalQty.FieldName = "FNFGBalQty"
        Me.mFNFGBalQty.Name = "mFNFGBalQty"
        Me.mFNFGBalQty.OptionsColumn.AllowEdit = False
        Me.mFNFGBalQty.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.mFNFGBalQty.OptionsColumn.AllowMove = False
        Me.mFNFGBalQty.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.mFNFGBalQty.OptionsColumn.ReadOnly = True
        Me.mFNFGBalQty.OptionsColumn.ShowInCustomizationForm = False
        Me.mFNFGBalQty.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FNFGBalQty", "{0:N0}")})
        Me.mFNFGBalQty.Visible = True
        Me.mFNFGBalQty.VisibleIndex = 54
        '
        'cFNFGInQtyBF
        '
        Me.cFNFGInQtyBF.Caption = "FNFGInQtyBF"
        Me.cFNFGInQtyBF.DisplayFormat.FormatString = "N0"
        Me.cFNFGInQtyBF.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cFNFGInQtyBF.FieldName = "FNFGInQtyBF"
        Me.cFNFGInQtyBF.MinWidth = 25
        Me.cFNFGInQtyBF.Name = "cFNFGInQtyBF"
        Me.cFNFGInQtyBF.OptionsColumn.AllowEdit = False
        Me.cFNFGInQtyBF.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.cFNFGInQtyBF.Visible = True
        Me.cFNFGInQtyBF.VisibleIndex = 53
        Me.cFNFGInQtyBF.Width = 94
        '
        'mFNExpQty
        '
        Me.mFNExpQty.Caption = "Export Qty"
        Me.mFNExpQty.DisplayFormat.FormatString = "N0"
        Me.mFNExpQty.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.mFNExpQty.FieldName = "FNExpQty"
        Me.mFNExpQty.Name = "mFNExpQty"
        Me.mFNExpQty.OptionsColumn.AllowEdit = False
        Me.mFNExpQty.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.mFNExpQty.OptionsColumn.AllowMove = False
        Me.mFNExpQty.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.mFNExpQty.OptionsColumn.ReadOnly = True
        Me.mFNExpQty.OptionsColumn.ShowInCustomizationForm = False
        Me.mFNExpQty.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FNExpQty", "{0:N0}")})
        Me.mFNExpQty.Visible = True
        Me.mFNExpQty.VisibleIndex = 55
        '
        'mFNExpBalQty
        '
        Me.mFNExpBalQty.Caption = "Export Bal"
        Me.mFNExpBalQty.DisplayFormat.FormatString = "N0"
        Me.mFNExpBalQty.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.mFNExpBalQty.FieldName = "FNExpBalQty"
        Me.mFNExpBalQty.Name = "mFNExpBalQty"
        Me.mFNExpBalQty.OptionsColumn.AllowEdit = False
        Me.mFNExpBalQty.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.mFNExpBalQty.OptionsColumn.AllowMove = False
        Me.mFNExpBalQty.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.mFNExpBalQty.OptionsColumn.ReadOnly = True
        Me.mFNExpBalQty.OptionsColumn.ShowInCustomizationForm = False
        Me.mFNExpBalQty.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FNExpBalQty", "{0:N0}")})
        Me.mFNExpBalQty.Visible = True
        Me.mFNExpBalQty.VisibleIndex = 56
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
        Me.ogbmainprocbutton.Controls.Add(Me.ocmexporttoexcel)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmload)
        Me.ogbmainprocbutton.Location = New System.Drawing.Point(183, 359)
        Me.ogbmainprocbutton.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbmainprocbutton.Name = "ogbmainprocbutton"
        Me.ogbmainprocbutton.Size = New System.Drawing.Size(961, 58)
        Me.ogbmainprocbutton.TabIndex = 387
        Me.ogbmainprocbutton.Tag = "2|"
        '
        'ocmsavelayout
        '
        Me.ocmsavelayout.Location = New System.Drawing.Point(303, 14)
        Me.ocmsavelayout.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmsavelayout.Name = "ocmsavelayout"
        Me.ocmsavelayout.Size = New System.Drawing.Size(136, 28)
        Me.ocmsavelayout.TabIndex = 332
        Me.ocmsavelayout.Text = "savelayoutgrid"
        '
        'ocmexit
        '
        Me.ocmexit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmexit.Location = New System.Drawing.Point(829, 14)
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
        'ocmexporttoexcel
        '
        Me.ocmexporttoexcel.Location = New System.Drawing.Point(478, 12)
        Me.ocmexporttoexcel.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmexporttoexcel.Name = "ocmexporttoexcel"
        Me.ocmexporttoexcel.Size = New System.Drawing.Size(136, 28)
        Me.ocmexporttoexcel.TabIndex = 329
        Me.ocmexporttoexcel.Text = "Export"
        '
        'ocmload
        '
        Me.ocmload.Location = New System.Drawing.Point(147, 14)
        Me.ocmload.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmload.Name = "ocmload"
        Me.ocmload.Size = New System.Drawing.Size(136, 28)
        Me.ocmload.TabIndex = 329
        Me.ocmload.Text = "Load Data"
        '
        'wProdOrderTrackingByLineDaily_CD
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1326, 779)
        Me.Controls.Add(Me.ogbmainprocbutton)
        Me.Controls.Add(Me.ogbdetail)
        Me.Controls.Add(Me.ogbheader)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "wProdOrderTrackingByLineDaily_CD"
        Me.Text = "Production Tracking By Line Daily"
        Me.ogbheader.ResumeLayout(False)
        Me.DockPanel1_Container.ResumeLayout(False)
        CType(Me.FTEndDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTEndDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTSSMKDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTSSMKDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTStartDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTStartDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTOrderNoTo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTOrderNo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogbdetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbdetail.ResumeLayout(False)
        CType(Me.ogcdetailcolorsizeline, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogcdetailcolorsizeline.ResumeLayout(False)
        Me.otbdetailcolorsizeline.ResumeLayout(False)
        CType(Me.ogcdetailcolorsizelineg, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvdetailcolorsizelineg, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XtraTabPage1.ResumeLayout(False)
        CType(Me.ogcmonthly, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvmonthly, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.oDockManager, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbmainprocbutton.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogbdetail As DevExpress.XtraEditors.GroupControl
    Friend WithEvents oDockManager As DevExpress.XtraBars.Docking.DockManager
    Friend WithEvents ogbheader As DevExpress.XtraBars.Docking.DockPanel
    Friend WithEvents DockPanel1_Container As DevExpress.XtraBars.Docking.ControlContainer
    Friend WithEvents FTOrderNoTo_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTOrderNoTo As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FTOrderNo_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTOrderNo As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents ogcdetailcolorsizeline As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents ogbmainprocbutton As DevExpress.XtraEditors.PanelControl
    Friend WithEvents ocmsavelayout As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmclear As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmload As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents otbdetailcolorsizeline As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents ogcdetailcolorsizelineg As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvdetailcolorsizelineg As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents SFTOrderNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents SFTCmpCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents SFTCmpName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents SFDShipDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents SFTColorway As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents SFTSizeBreakDown As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents SFNQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents SFNQuantityExtra As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents SFNGarmentQtyTest As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents SFNGrandQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents SFNCutQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents SFNCutBalQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents SFNSendSuplQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents SFNRcvSuplQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents SFNBalSuplQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents SFNSPMKQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents SFNBalSPMKQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents SFNSewQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents SFNSewOutQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents SFNBalSewQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents SFNPackQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents SFNBalPackQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTUnitSectCodeCut As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTUnitSectCodeSew As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNTableCut As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents SFTPOLineItemNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTPORef As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTStyleCode As DevExpress.XtraGrid.Columns.GridColumn
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
    Friend WithEvents FTSSMKDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents FTSSMKDate1_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents CXFNSPMKQuantityBal As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNFGInQty As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNFGBalQty As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNExpQty As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNExpBalQty As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ocmexporttoexcel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents XtraTabPage1 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents ogcmonthly As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvmonthly As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents cFTStyleCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTOrderNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTPORef As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTCmpCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents mFTCmpName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents mFDShipDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents mFTPOLineItemNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents mFTColorway As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents mFTSizeBreakDown As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents mFNQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents mFNQuantityExtra As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents mFNGarmentQtyTest As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents mFNGrandQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn14 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents mFNCutQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents mFNCutBalQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents mFNQtyEmbroidery As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents mFNRcvQtyEmbroidery As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents mFNBalQtyEmbroidery As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents mFNQtyPrint As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents mFNRcvQtyPrint As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents mFNBalQtyPrint As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents mFNQtyHeat As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents mFNRcvQtyHeat As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents mFNBalQtyHeat As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents mFNQtyLaser As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents mFNRcvQtyLaser As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents mFNBalQtyLaser As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents mFNQtyPadPrint As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents mFNRcvQtyPadPrint As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents mFNBalQtyPadPrint As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents mFNSendSuplQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents mFNRcvSuplQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents mFNBalSuplQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents mFNSPMKQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNBalSPMKQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn40 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents mFNSewQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents mFNSPMKQuantityBal As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents mFNSewOutQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents mFNBalSewQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents mFNPackQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents mFNBalPackQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn47 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents mFNFGInQty As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents mFNFGBalQty As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents mFNExpQty As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents mFNExpBalQty As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTMonthly As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNOrderOnhand As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTEndDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents FTEndDate_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTStartDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents FTStart_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cFNCutBFQty As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNQtyEmbroideryBF As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNQtyPrintBF As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNQtyHeatBF As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNQtyLaserBF As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNQtyPadPrintBF As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNSendSuplQuantityBF As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNSPMKQuantityBF As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNSewQuantityBF As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNSewOutQuantityBF As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNFGInQtyBF As DevExpress.XtraGrid.Columns.GridColumn
End Class

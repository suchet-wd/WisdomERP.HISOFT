<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wPurchaseMaterialTrackingSample
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(wPurchaseMaterialTrackingSample))
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
        Me.FTEndDevCFMDate = New DevExpress.XtraEditors.DateEdit()
        Me.FTEndDEvCFMDate_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTStartDevCFMDate = New DevExpress.XtraEditors.DateEdit()
        Me.FTStartDevCfmDate_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTEndSRConfirmDate = New DevExpress.XtraEditors.DateEdit()
        Me.FTEndOrderDate = New DevExpress.XtraEditors.DateEdit()
        Me.FTEndSRConfirmDate_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTEndOrderDate_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTStartSRConfirmDate = New DevExpress.XtraEditors.DateEdit()
        Me.FNDataType_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTStartDevConfirmDate_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTStartOrderDate = New DevExpress.XtraEditors.DateEdit()
        Me.FNHSysBuyId_None = New DevExpress.XtraEditors.TextEdit()
        Me.FTStartOrderDate_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysBuyId_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysBuyId = New DevExpress.XtraEditors.ButtonEdit()
        Me.FNListDocumentTrackPIData_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNListDocumentTrackPIData = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.FNHSysSuplId = New DevExpress.XtraEditors.ButtonEdit()
        Me.FNHSysSuplId_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysSuplId_None = New DevExpress.XtraEditors.TextEdit()
        Me.FTEndPurchaseDate = New DevExpress.XtraEditors.DateEdit()
        Me.FTEndDelivery = New DevExpress.XtraEditors.DateEdit()
        Me.FTEndPurchaseDate_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTEndDelivery_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTStartPurchaseDate = New DevExpress.XtraEditors.DateEdit()
        Me.FTStartPurchaseDate_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTStartDelivery = New DevExpress.XtraEditors.DateEdit()
        Me.FTStartDelivery_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNDataType = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.ochkselectall = New DevExpress.XtraEditors.CheckEdit()
        Me.ogbdetail = New DevExpress.XtraEditors.GroupControl()
        Me.PopupTrackNote = New DevExpress.XtraEditors.PopupContainerControl()
        Me.FTTRNote = New DevExpress.XtraEditors.MemoEdit()
        Me.ogbmainprocbutton = New DevExpress.XtraEditors.PanelControl()
        Me.ocmholdpurchase = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmDelayReasons = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmFurtherDelayReason = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmproduction = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmpurchase = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmFTFinalOETCDate = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmFTORGOETCDate = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmsavetrack = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmsendmail = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmsavelayout = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmclear = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmload = New DevExpress.XtraEditors.SimpleButton()
        Me.ogdtime = New DevExpress.XtraGrid.GridControl()
        Me.ogvtime = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.FTSelect = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepCheckEdit = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.xFTFirstInputDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTFirstInputBy = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTLastInputDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTLastInputBy = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFTPurchaseNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FDPurchaseDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTPurchaseBy = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.xFTPINo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTSuplCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTSuplName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTSuplCalCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.xFTNikeVenderCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.xFTCOFOCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.xFTCurCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FDDeliveryDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FDCFMDeliveryDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cxFTPOCustPO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.xFTPOOrderNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTOrderDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTDevConfirmDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.xFTPOStyleCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNHSysDeliveryID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CXFTCmpCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTBuyCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTPartCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTPartDetail = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTRawMatCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.xFTMainMatName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.xFTRawMatColorCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.xFTRawMatSizeCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTStateSuperVisorApp = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTStateManagerApp = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTStateSendMail = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTSendMailBy = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTSendMailDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTSendMailTime = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.xFTLastSendMailBy = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.xFTLastSendMailDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.xFTLastSendMailTime = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.xxFNLeadTime = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCalcEditFNLeadTime = New DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit()
        Me.FDSupCFMDelDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.xFTOETCDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.xFTORGOETCDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemDateEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemDateEdit()
        Me.FTORGOETCInputBy = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTORGOETCInputDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTORGOETCInputTime = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTFinalOETCDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTFinalOETCInputBy = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTFinalOETCInputDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTFinalOETCInputTime = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.xFNORGLeadTime = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNFinalLeadTime = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTDelayLangth = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNPOQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNUsedQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTUsedUnit = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNPOCFMQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCalcQty = New DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit()
        Me.xFTPOCFMNote = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemMemoExFTNote = New DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit()
        Me.xFNPROCFMQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.xFTPROCFMNote = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.xFNPOBALQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTUnitCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.xFTDelayReasonsCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemGridLookUpEditFTDelayReasonsCode = New DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit()
        Me.RepositoryItemGridLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.xxxFTDelayReasonsCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.xxxFTDelayReasonsNameEN = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTDelayReasonsName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTFurtherDelayReasonCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemGridLookUpEditFTFurtherDelayReasonCode = New DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.xFTFurtherDelayReasonCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.xFTFurtherDelayReasonNameEN = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTFurtherDelayReasonName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNTrackSeq = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTTrackDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTTrackBy = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTContactName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.xFTNote = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemPopupContainerTrackNote = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.xFNHSysRawMatId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.xFNDocType = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.xFTInvoiceNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemTextFTInvoiceNo = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.xFTInvoiceNote = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.xFTReserveNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.xFTSendSMPDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.xFTSendSMPStatus = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemTextEditFTSendSMPStatus = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.xFTSendSMPRemark = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.xFTSendSMPAWB = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.xFTSendSMPPayType = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.xFTSamplePPC = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTStatePDF = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTStatePDFWaitPrice = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.xFTStateCancel = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.xFTStateHold = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.xFTHoldReason = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNDocType = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemMemoExEditTrackNote = New DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit()
        Me.oDockManager = New DevExpress.XtraBars.Docking.DockManager(Me.components)
        Me.ogbheader.SuspendLayout()
        Me.DockPanel1_Container.SuspendLayout()
        CType(Me.FTEndDevCFMDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTEndDevCFMDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTStartDevCFMDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTStartDevCFMDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTEndSRConfirmDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTEndSRConfirmDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTEndOrderDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTEndOrderDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTStartSRConfirmDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTStartSRConfirmDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTStartOrderDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTStartOrderDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysBuyId_None.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysBuyId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNListDocumentTrackPIData.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysSuplId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysSuplId_None.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTEndPurchaseDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTEndPurchaseDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTEndDelivery.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTEndDelivery.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTStartPurchaseDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTStartPurchaseDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTStartDelivery.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTStartDelivery.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNDataType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ochkselectall.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogbdetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbdetail.SuspendLayout()
        CType(Me.PopupTrackNote, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PopupTrackNote.SuspendLayout()
        CType(Me.FTTRNote.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbmainprocbutton.SuspendLayout()
        CType(Me.ogdtime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvtime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepCheckEdit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCalcEditFNLeadTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemDateEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemDateEdit1.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCalcQty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemMemoExFTNote, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemGridLookUpEditFTDelayReasonsCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemGridLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemGridLookUpEditFTFurtherDelayReasonCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerTrackNote, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemTextFTInvoiceNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemTextEditFTSendSMPStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemMemoExEditTrackNote, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.ogbheader.OriginalSize = New System.Drawing.Size(1318, 154)
        Me.ogbheader.Size = New System.Drawing.Size(1318, 154)
        Me.ogbheader.TabStop = False
        Me.ogbheader.Text = "Criteria"
        '
        'DockPanel1_Container
        '
        Me.DockPanel1_Container.Controls.Add(Me.FTEndDevCFMDate)
        Me.DockPanel1_Container.Controls.Add(Me.FTEndDEvCFMDate_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FTStartDevCFMDate)
        Me.DockPanel1_Container.Controls.Add(Me.FTStartDevCfmDate_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FTEndSRConfirmDate)
        Me.DockPanel1_Container.Controls.Add(Me.FTEndOrderDate)
        Me.DockPanel1_Container.Controls.Add(Me.FTEndSRConfirmDate_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FTEndOrderDate_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FTStartSRConfirmDate)
        Me.DockPanel1_Container.Controls.Add(Me.FNDataType_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FTStartDevConfirmDate_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FTStartOrderDate)
        Me.DockPanel1_Container.Controls.Add(Me.FNHSysBuyId_None)
        Me.DockPanel1_Container.Controls.Add(Me.FTStartOrderDate_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FNHSysBuyId_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FNHSysBuyId)
        Me.DockPanel1_Container.Controls.Add(Me.FNListDocumentTrackPIData_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FNListDocumentTrackPIData)
        Me.DockPanel1_Container.Controls.Add(Me.FNHSysSuplId)
        Me.DockPanel1_Container.Controls.Add(Me.FNHSysSuplId_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FNHSysSuplId_None)
        Me.DockPanel1_Container.Controls.Add(Me.FTEndPurchaseDate)
        Me.DockPanel1_Container.Controls.Add(Me.FTEndDelivery)
        Me.DockPanel1_Container.Controls.Add(Me.FTEndPurchaseDate_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FTEndDelivery_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FTStartPurchaseDate)
        Me.DockPanel1_Container.Controls.Add(Me.FTStartPurchaseDate_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FTStartDelivery)
        Me.DockPanel1_Container.Controls.Add(Me.FTStartDelivery_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FNDataType)
        Me.DockPanel1_Container.Location = New System.Drawing.Point(3, 26)
        Me.DockPanel1_Container.Name = "DockPanel1_Container"
        Me.DockPanel1_Container.Size = New System.Drawing.Size(1312, 124)
        Me.DockPanel1_Container.TabIndex = 0
        '
        'FTEndDevCFMDate
        '
        Me.FTEndDevCFMDate.EditValue = Nothing
        Me.FTEndDevCFMDate.EnterMoveNextControl = True
        Me.FTEndDevCFMDate.Location = New System.Drawing.Point(389, 98)
        Me.FTEndDevCFMDate.Name = "FTEndDevCFMDate"
        Me.FTEndDevCFMDate.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTEndDevCFMDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FTEndDevCFMDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.FTEndDevCFMDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTEndDevCFMDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTEndDevCFMDate.Properties.NullDate = ""
        Me.FTEndDevCFMDate.Size = New System.Drawing.Size(107, 20)
        Me.FTEndDevCFMDate.TabIndex = 9
        Me.FTEndDevCFMDate.Tag = "2|"
        '
        'FTEndDEvCFMDate_lbl
        '
        Me.FTEndDEvCFMDate_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTEndDEvCFMDate_lbl.Appearance.Options.UseForeColor = True
        Me.FTEndDEvCFMDate_lbl.Appearance.Options.UseTextOptions = True
        Me.FTEndDEvCFMDate_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTEndDEvCFMDate_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTEndDEvCFMDate_lbl.Location = New System.Drawing.Point(271, 98)
        Me.FTEndDEvCFMDate_lbl.Name = "FTEndDEvCFMDate_lbl"
        Me.FTEndDEvCFMDate_lbl.Size = New System.Drawing.Size(114, 19)
        Me.FTEndDEvCFMDate_lbl.TabIndex = 563
        Me.FTEndDEvCFMDate_lbl.Tag = "2|"
        Me.FTEndDEvCFMDate_lbl.Text = "End Dev Confirm Date :"
        '
        'FTStartDevCFMDate
        '
        Me.FTStartDevCFMDate.EditValue = Nothing
        Me.FTStartDevCFMDate.EnterMoveNextControl = True
        Me.FTStartDevCFMDate.Location = New System.Drawing.Point(158, 99)
        Me.FTStartDevCFMDate.Name = "FTStartDevCFMDate"
        Me.FTStartDevCFMDate.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTStartDevCFMDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FTStartDevCFMDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.FTStartDevCFMDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTStartDevCFMDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTStartDevCFMDate.Properties.NullDate = ""
        Me.FTStartDevCFMDate.Size = New System.Drawing.Size(107, 20)
        Me.FTStartDevCFMDate.TabIndex = 8
        Me.FTStartDevCFMDate.Tag = "2|"
        '
        'FTStartDevCfmDate_lbl
        '
        Me.FTStartDevCfmDate_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTStartDevCfmDate_lbl.Appearance.Options.UseForeColor = True
        Me.FTStartDevCfmDate_lbl.Appearance.Options.UseTextOptions = True
        Me.FTStartDevCfmDate_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTStartDevCfmDate_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTStartDevCfmDate_lbl.Location = New System.Drawing.Point(4, 98)
        Me.FTStartDevCfmDate_lbl.Name = "FTStartDevCfmDate_lbl"
        Me.FTStartDevCfmDate_lbl.Size = New System.Drawing.Size(149, 19)
        Me.FTStartDevCfmDate_lbl.TabIndex = 562
        Me.FTStartDevCfmDate_lbl.Tag = "2|"
        Me.FTStartDevCfmDate_lbl.Text = "Start Dev Confirm Date :"
        '
        'FTEndSRConfirmDate
        '
        Me.FTEndSRConfirmDate.EditValue = Nothing
        Me.FTEndSRConfirmDate.EnterMoveNextControl = True
        Me.FTEndSRConfirmDate.Location = New System.Drawing.Point(893, 76)
        Me.FTEndSRConfirmDate.Name = "FTEndSRConfirmDate"
        Me.FTEndSRConfirmDate.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTEndSRConfirmDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FTEndSRConfirmDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.FTEndSRConfirmDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTEndSRConfirmDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTEndSRConfirmDate.Properties.NullDate = ""
        Me.FTEndSRConfirmDate.Size = New System.Drawing.Size(106, 20)
        Me.FTEndSRConfirmDate.TabIndex = 11
        Me.FTEndSRConfirmDate.Tag = "2|"
        Me.FTEndSRConfirmDate.Visible = False
        '
        'FTEndOrderDate
        '
        Me.FTEndOrderDate.EditValue = Nothing
        Me.FTEndOrderDate.EnterMoveNextControl = True
        Me.FTEndOrderDate.Location = New System.Drawing.Point(390, 75)
        Me.FTEndOrderDate.Name = "FTEndOrderDate"
        Me.FTEndOrderDate.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTEndOrderDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FTEndOrderDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.FTEndOrderDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTEndOrderDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTEndOrderDate.Properties.NullDate = ""
        Me.FTEndOrderDate.Size = New System.Drawing.Size(106, 20)
        Me.FTEndOrderDate.TabIndex = 7
        Me.FTEndOrderDate.Tag = "2|"
        '
        'FTEndSRConfirmDate_lbl
        '
        Me.FTEndSRConfirmDate_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTEndSRConfirmDate_lbl.Appearance.Options.UseForeColor = True
        Me.FTEndSRConfirmDate_lbl.Appearance.Options.UseTextOptions = True
        Me.FTEndSRConfirmDate_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTEndSRConfirmDate_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTEndSRConfirmDate_lbl.Location = New System.Drawing.Point(773, 76)
        Me.FTEndSRConfirmDate_lbl.Name = "FTEndSRConfirmDate_lbl"
        Me.FTEndSRConfirmDate_lbl.Size = New System.Drawing.Size(114, 19)
        Me.FTEndSRConfirmDate_lbl.TabIndex = 279
        Me.FTEndSRConfirmDate_lbl.Tag = "2|"
        Me.FTEndSRConfirmDate_lbl.Text = "End SR Confirm Date :"
        Me.FTEndSRConfirmDate_lbl.Visible = False
        '
        'FTEndOrderDate_lbl
        '
        Me.FTEndOrderDate_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTEndOrderDate_lbl.Appearance.Options.UseForeColor = True
        Me.FTEndOrderDate_lbl.Appearance.Options.UseTextOptions = True
        Me.FTEndOrderDate_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTEndOrderDate_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTEndOrderDate_lbl.Location = New System.Drawing.Point(271, 75)
        Me.FTEndOrderDate_lbl.Name = "FTEndOrderDate_lbl"
        Me.FTEndOrderDate_lbl.Size = New System.Drawing.Size(114, 19)
        Me.FTEndOrderDate_lbl.TabIndex = 275
        Me.FTEndOrderDate_lbl.Tag = "2|"
        Me.FTEndOrderDate_lbl.Text = "End Order Date :"
        '
        'FTStartSRConfirmDate
        '
        Me.FTStartSRConfirmDate.EditValue = Nothing
        Me.FTStartSRConfirmDate.EnterMoveNextControl = True
        Me.FTStartSRConfirmDate.Location = New System.Drawing.Point(662, 77)
        Me.FTStartSRConfirmDate.Name = "FTStartSRConfirmDate"
        Me.FTStartSRConfirmDate.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTStartSRConfirmDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FTStartSRConfirmDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.FTStartSRConfirmDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTStartSRConfirmDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTStartSRConfirmDate.Properties.NullDate = ""
        Me.FTStartSRConfirmDate.Size = New System.Drawing.Size(106, 20)
        Me.FTStartSRConfirmDate.TabIndex = 10
        Me.FTStartSRConfirmDate.Tag = "2|"
        Me.FTStartSRConfirmDate.Visible = False
        '
        'FNDataType_lbl
        '
        Me.FNDataType_lbl.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(31, Byte), Integer), CType(CType(53, Byte), Integer))
        Me.FNDataType_lbl.Appearance.Options.UseForeColor = True
        Me.FNDataType_lbl.Appearance.Options.UseTextOptions = True
        Me.FNDataType_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNDataType_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNDataType_lbl.Location = New System.Drawing.Point(533, 52)
        Me.FNDataType_lbl.Name = "FNDataType_lbl"
        Me.FNDataType_lbl.Size = New System.Drawing.Size(123, 18)
        Me.FNDataType_lbl.TabIndex = 559
        Me.FNDataType_lbl.Tag = "2|"
        Me.FNDataType_lbl.Text = "Data ype :"
        Me.FNDataType_lbl.Visible = False
        '
        'FTStartDevConfirmDate_lbl
        '
        Me.FTStartDevConfirmDate_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTStartDevConfirmDate_lbl.Appearance.Options.UseForeColor = True
        Me.FTStartDevConfirmDate_lbl.Appearance.Options.UseTextOptions = True
        Me.FTStartDevConfirmDate_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTStartDevConfirmDate_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTStartDevConfirmDate_lbl.Location = New System.Drawing.Point(507, 77)
        Me.FTStartDevConfirmDate_lbl.Name = "FTStartDevConfirmDate_lbl"
        Me.FTStartDevConfirmDate_lbl.Size = New System.Drawing.Size(149, 19)
        Me.FTStartDevConfirmDate_lbl.TabIndex = 278
        Me.FTStartDevConfirmDate_lbl.Tag = "2|"
        Me.FTStartDevConfirmDate_lbl.Text = "Start SR Confirm Date :"
        Me.FTStartDevConfirmDate_lbl.Visible = False
        '
        'FTStartOrderDate
        '
        Me.FTStartOrderDate.EditValue = Nothing
        Me.FTStartOrderDate.EnterMoveNextControl = True
        Me.FTStartOrderDate.Location = New System.Drawing.Point(159, 76)
        Me.FTStartOrderDate.Name = "FTStartOrderDate"
        Me.FTStartOrderDate.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTStartOrderDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FTStartOrderDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.FTStartOrderDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTStartOrderDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTStartOrderDate.Properties.NullDate = ""
        Me.FTStartOrderDate.Size = New System.Drawing.Size(106, 20)
        Me.FTStartOrderDate.TabIndex = 6
        Me.FTStartOrderDate.Tag = "2|"
        '
        'FNHSysBuyId_None
        '
        Me.FNHSysBuyId_None.Location = New System.Drawing.Point(798, 7)
        Me.FNHSysBuyId_None.Name = "FNHSysBuyId_None"
        Me.FNHSysBuyId_None.Properties.Appearance.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysBuyId_None.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysBuyId_None.Properties.ReadOnly = True
        Me.FNHSysBuyId_None.Size = New System.Drawing.Size(219, 20)
        Me.FNHSysBuyId_None.TabIndex = 16
        Me.FNHSysBuyId_None.Tag = "2|"
        '
        'FTStartOrderDate_lbl
        '
        Me.FTStartOrderDate_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTStartOrderDate_lbl.Appearance.Options.UseForeColor = True
        Me.FTStartOrderDate_lbl.Appearance.Options.UseTextOptions = True
        Me.FTStartOrderDate_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTStartOrderDate_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTStartOrderDate_lbl.Location = New System.Drawing.Point(4, 75)
        Me.FTStartOrderDate_lbl.Name = "FTStartOrderDate_lbl"
        Me.FTStartOrderDate_lbl.Size = New System.Drawing.Size(149, 19)
        Me.FTStartOrderDate_lbl.TabIndex = 273
        Me.FTStartOrderDate_lbl.Tag = "2|"
        Me.FTStartOrderDate_lbl.Text = "Start Order Date :"
        '
        'FNHSysBuyId_lbl
        '
        Me.FNHSysBuyId_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FNHSysBuyId_lbl.Appearance.Options.UseForeColor = True
        Me.FNHSysBuyId_lbl.Appearance.Options.UseTextOptions = True
        Me.FNHSysBuyId_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysBuyId_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysBuyId_lbl.Location = New System.Drawing.Point(500, 6)
        Me.FNHSysBuyId_lbl.Name = "FNHSysBuyId_lbl"
        Me.FNHSysBuyId_lbl.Size = New System.Drawing.Size(156, 20)
        Me.FNHSysBuyId_lbl.TabIndex = 14
        Me.FNHSysBuyId_lbl.Tag = "2|"
        Me.FNHSysBuyId_lbl.Text = "Buy :"
        '
        'FNHSysBuyId
        '
        Me.FNHSysBuyId.Location = New System.Drawing.Point(662, 7)
        Me.FNHSysBuyId.Name = "FNHSysBuyId"
        Me.FNHSysBuyId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, EditorButtonImageOptions1, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, SerializableAppearanceObject2, SerializableAppearanceObject3, SerializableAppearanceObject4, "", "113", Nothing, DevExpress.Utils.ToolTipAnchor.[Default])})
        Me.FNHSysBuyId.Properties.Tag = ""
        Me.FNHSysBuyId.Size = New System.Drawing.Size(130, 20)
        Me.FNHSysBuyId.TabIndex = 12
        Me.FNHSysBuyId.Tag = "2|"
        '
        'FNListDocumentTrackPIData_lbl
        '
        Me.FNListDocumentTrackPIData_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FNListDocumentTrackPIData_lbl.Appearance.Options.UseForeColor = True
        Me.FNListDocumentTrackPIData_lbl.Appearance.Options.UseTextOptions = True
        Me.FNListDocumentTrackPIData_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNListDocumentTrackPIData_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNListDocumentTrackPIData_lbl.Location = New System.Drawing.Point(4, 7)
        Me.FNListDocumentTrackPIData_lbl.Name = "FNListDocumentTrackPIData_lbl"
        Me.FNListDocumentTrackPIData_lbl.Size = New System.Drawing.Size(149, 19)
        Me.FNListDocumentTrackPIData_lbl.TabIndex = 265
        Me.FNListDocumentTrackPIData_lbl.Tag = "2|"
        Me.FNListDocumentTrackPIData_lbl.Text = "Data :"
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
        Me.FNListDocumentTrackPIData.Size = New System.Drawing.Size(338, 20)
        Me.FNListDocumentTrackPIData.TabIndex = 1
        Me.FNListDocumentTrackPIData.Tag = "9|"
        '
        'FNHSysSuplId
        '
        Me.FNHSysSuplId.EnterMoveNextControl = True
        Me.FNHSysSuplId.Location = New System.Drawing.Point(663, 29)
        Me.FNHSysSuplId.Name = "FNHSysSuplId"
        Me.FNHSysSuplId.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FNHSysSuplId.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysSuplId.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysSuplId.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysSuplId.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FNHSysSuplId.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FNHSysSuplId.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNHSysSuplId.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysSuplId.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNHSysSuplId.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNHSysSuplId.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysSuplId.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysSuplId.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNHSysSuplId.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNHSysSuplId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", 15, True, True, False, EditorButtonImageOptions2, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject5, SerializableAppearanceObject6, SerializableAppearanceObject7, SerializableAppearanceObject8, "", "99", Nothing, DevExpress.Utils.ToolTipAnchor.[Default])})
        Me.FNHSysSuplId.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.FNHSysSuplId.Properties.MaxLength = 30
        Me.FNHSysSuplId.Size = New System.Drawing.Size(130, 20)
        Me.FNHSysSuplId.TabIndex = 13
        Me.FNHSysSuplId.Tag = "2|"
        '
        'FNHSysSuplId_lbl
        '
        Me.FNHSysSuplId_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FNHSysSuplId_lbl.Appearance.Options.UseForeColor = True
        Me.FNHSysSuplId_lbl.Appearance.Options.UseTextOptions = True
        Me.FNHSysSuplId_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysSuplId_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysSuplId_lbl.Location = New System.Drawing.Point(507, 29)
        Me.FNHSysSuplId_lbl.Name = "FNHSysSuplId_lbl"
        Me.FNHSysSuplId_lbl.Size = New System.Drawing.Size(149, 19)
        Me.FNHSysSuplId_lbl.TabIndex = 264
        Me.FNHSysSuplId_lbl.Tag = "2|"
        Me.FNHSysSuplId_lbl.Text = "Supplier :"
        '
        'FNHSysSuplId_None
        '
        Me.FNHSysSuplId_None.EnterMoveNextControl = True
        Me.FNHSysSuplId_None.Location = New System.Drawing.Point(798, 30)
        Me.FNHSysSuplId_None.Name = "FNHSysSuplId_None"
        Me.FNHSysSuplId_None.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FNHSysSuplId_None.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FNHSysSuplId_None.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysSuplId_None.Properties.Appearance.Options.UseForeColor = True
        Me.FNHSysSuplId_None.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysSuplId_None.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysSuplId_None.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FNHSysSuplId_None.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FNHSysSuplId_None.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNHSysSuplId_None.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysSuplId_None.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNHSysSuplId_None.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNHSysSuplId_None.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysSuplId_None.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysSuplId_None.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNHSysSuplId_None.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNHSysSuplId_None.Properties.ReadOnly = True
        Me.FNHSysSuplId_None.Size = New System.Drawing.Size(219, 20)
        Me.FNHSysSuplId_None.TabIndex = 265
        Me.FNHSysSuplId_None.TabStop = False
        Me.FNHSysSuplId_None.Tag = "2|"
        '
        'FTEndPurchaseDate
        '
        Me.FTEndPurchaseDate.EditValue = Nothing
        Me.FTEndPurchaseDate.EnterMoveNextControl = True
        Me.FTEndPurchaseDate.Location = New System.Drawing.Point(390, 31)
        Me.FTEndPurchaseDate.Name = "FTEndPurchaseDate"
        Me.FTEndPurchaseDate.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTEndPurchaseDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FTEndPurchaseDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.FTEndPurchaseDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTEndPurchaseDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTEndPurchaseDate.Properties.NullDate = ""
        Me.FTEndPurchaseDate.Size = New System.Drawing.Size(106, 20)
        Me.FTEndPurchaseDate.TabIndex = 3
        Me.FTEndPurchaseDate.Tag = "2|"
        '
        'FTEndDelivery
        '
        Me.FTEndDelivery.EditValue = Nothing
        Me.FTEndDelivery.EnterMoveNextControl = True
        Me.FTEndDelivery.Location = New System.Drawing.Point(390, 53)
        Me.FTEndDelivery.Name = "FTEndDelivery"
        Me.FTEndDelivery.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTEndDelivery.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FTEndDelivery.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.FTEndDelivery.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTEndDelivery.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTEndDelivery.Properties.NullDate = ""
        Me.FTEndDelivery.Size = New System.Drawing.Size(106, 20)
        Me.FTEndDelivery.TabIndex = 5
        Me.FTEndDelivery.Tag = "2|"
        '
        'FTEndPurchaseDate_lbl
        '
        Me.FTEndPurchaseDate_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTEndPurchaseDate_lbl.Appearance.Options.UseForeColor = True
        Me.FTEndPurchaseDate_lbl.Appearance.Options.UseTextOptions = True
        Me.FTEndPurchaseDate_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTEndPurchaseDate_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTEndPurchaseDate_lbl.Location = New System.Drawing.Point(271, 32)
        Me.FTEndPurchaseDate_lbl.Name = "FTEndPurchaseDate_lbl"
        Me.FTEndPurchaseDate_lbl.Size = New System.Drawing.Size(114, 19)
        Me.FTEndPurchaseDate_lbl.TabIndex = 275
        Me.FTEndPurchaseDate_lbl.Tag = "2|"
        Me.FTEndPurchaseDate_lbl.Text = "End Purchase Date :"
        '
        'FTEndDelivery_lbl
        '
        Me.FTEndDelivery_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTEndDelivery_lbl.Appearance.Options.UseForeColor = True
        Me.FTEndDelivery_lbl.Appearance.Options.UseTextOptions = True
        Me.FTEndDelivery_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTEndDelivery_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTEndDelivery_lbl.Location = New System.Drawing.Point(271, 53)
        Me.FTEndDelivery_lbl.Name = "FTEndDelivery_lbl"
        Me.FTEndDelivery_lbl.Size = New System.Drawing.Size(114, 19)
        Me.FTEndDelivery_lbl.TabIndex = 271
        Me.FTEndDelivery_lbl.Tag = "2|"
        Me.FTEndDelivery_lbl.Text = "End Delivery Date :"
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
        Me.FTStartPurchaseDate.Size = New System.Drawing.Size(106, 20)
        Me.FTStartPurchaseDate.TabIndex = 2
        Me.FTStartPurchaseDate.Tag = "2|"
        '
        'FTStartPurchaseDate_lbl
        '
        Me.FTStartPurchaseDate_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTStartPurchaseDate_lbl.Appearance.Options.UseForeColor = True
        Me.FTStartPurchaseDate_lbl.Appearance.Options.UseTextOptions = True
        Me.FTStartPurchaseDate_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTStartPurchaseDate_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTStartPurchaseDate_lbl.Location = New System.Drawing.Point(4, 30)
        Me.FTStartPurchaseDate_lbl.Name = "FTStartPurchaseDate_lbl"
        Me.FTStartPurchaseDate_lbl.Size = New System.Drawing.Size(149, 19)
        Me.FTStartPurchaseDate_lbl.TabIndex = 273
        Me.FTStartPurchaseDate_lbl.Tag = "2|"
        Me.FTStartPurchaseDate_lbl.Text = "Start Purchase Date :"
        '
        'FTStartDelivery
        '
        Me.FTStartDelivery.EditValue = Nothing
        Me.FTStartDelivery.EnterMoveNextControl = True
        Me.FTStartDelivery.Location = New System.Drawing.Point(159, 53)
        Me.FTStartDelivery.Name = "FTStartDelivery"
        Me.FTStartDelivery.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTStartDelivery.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FTStartDelivery.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.FTStartDelivery.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTStartDelivery.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTStartDelivery.Properties.NullDate = ""
        Me.FTStartDelivery.Size = New System.Drawing.Size(106, 20)
        Me.FTStartDelivery.TabIndex = 4
        Me.FTStartDelivery.Tag = "2|"
        '
        'FTStartDelivery_lbl
        '
        Me.FTStartDelivery_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTStartDelivery_lbl.Appearance.Options.UseForeColor = True
        Me.FTStartDelivery_lbl.Appearance.Options.UseTextOptions = True
        Me.FTStartDelivery_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTStartDelivery_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTStartDelivery_lbl.Location = New System.Drawing.Point(4, 52)
        Me.FTStartDelivery_lbl.Name = "FTStartDelivery_lbl"
        Me.FTStartDelivery_lbl.Size = New System.Drawing.Size(149, 19)
        Me.FTStartDelivery_lbl.TabIndex = 269
        Me.FTStartDelivery_lbl.Tag = "2|"
        Me.FTStartDelivery_lbl.Text = "Start Delivery Date :"
        '
        'FNDataType
        '
        Me.FNDataType.EditValue = ""
        Me.FNDataType.Location = New System.Drawing.Point(663, 52)
        Me.FNDataType.Name = "FNDataType"
        Me.FNDataType.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FNDataType.Properties.Appearance.Options.UseBackColor = True
        Me.FNDataType.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FNDataType.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FNDataType.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FNDataType.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FNDataType.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNDataType.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNDataType.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNDataType.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNDataType.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNDataType.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNDataType.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNDataType.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNDataType.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FNDataType.Properties.Items.AddRange(New Object() {"Purchase", "Purchase & Reserve", "Purchase & Reserve & Order (Tracking Only)"})
        Me.FNDataType.Properties.Tag = ""
        Me.FNDataType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.FNDataType.Size = New System.Drawing.Size(354, 20)
        Me.FNDataType.TabIndex = 14
        Me.FNDataType.Tag = "2|"
        Me.FNDataType.Visible = False
        '
        'ochkselectall
        '
        Me.ochkselectall.Location = New System.Drawing.Point(5, 198)
        Me.ochkselectall.Name = "ochkselectall"
        Me.ochkselectall.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.ochkselectall.Properties.Appearance.Options.UseForeColor = True
        Me.ochkselectall.Properties.Caption = "Select All"
        Me.ochkselectall.Size = New System.Drawing.Size(142, 20)
        Me.ochkselectall.TabIndex = 309
        Me.ochkselectall.Visible = False
        '
        'ogbdetail
        '
        Me.ogbdetail.Controls.Add(Me.PopupTrackNote)
        Me.ogbdetail.Controls.Add(Me.ogbmainprocbutton)
        Me.ogbdetail.Controls.Add(Me.ogdtime)
        Me.ogbdetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogbdetail.Location = New System.Drawing.Point(0, 154)
        Me.ogbdetail.Name = "ogbdetail"
        Me.ogbdetail.ShowCaption = False
        Me.ogbdetail.Size = New System.Drawing.Size(1318, 500)
        Me.ogbdetail.TabIndex = 0
        '
        'PopupTrackNote
        '
        Me.PopupTrackNote.Controls.Add(Me.FTTRNote)
        Me.PopupTrackNote.Location = New System.Drawing.Point(539, 55)
        Me.PopupTrackNote.Name = "PopupTrackNote"
        Me.PopupTrackNote.Size = New System.Drawing.Size(461, 392)
        Me.PopupTrackNote.TabIndex = 387
        '
        'FTTRNote
        '
        Me.FTTRNote.EditValue = ""
        Me.FTTRNote.Location = New System.Drawing.Point(0, 0)
        Me.FTTRNote.Name = "FTTRNote"
        Me.FTTRNote.Properties.MaxLength = 500
        Me.FTTRNote.Properties.ReadOnly = True
        Me.FTTRNote.Properties.WordWrap = False
        Me.FTTRNote.Size = New System.Drawing.Size(461, 392)
        Me.FTTRNote.TabIndex = 497
        Me.FTTRNote.Tag = "2|"
        '
        'ogbmainprocbutton
        '
        Me.ogbmainprocbutton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmholdpurchase)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmDelayReasons)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmFurtherDelayReason)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmproduction)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmpurchase)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmFTFinalOETCDate)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmFTORGOETCDate)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmsavetrack)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmsendmail)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmsavelayout)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmexit)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmclear)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmload)
        Me.ogbmainprocbutton.Controls.Add(Me.ochkselectall)
        Me.ogbmainprocbutton.Location = New System.Drawing.Point(111, 110)
        Me.ogbmainprocbutton.Name = "ogbmainprocbutton"
        Me.ogbmainprocbutton.Size = New System.Drawing.Size(323, 236)
        Me.ogbmainprocbutton.TabIndex = 386
        Me.ogbmainprocbutton.Tag = "2|"
        '
        'ocmholdpurchase
        '
        Me.ocmholdpurchase.Location = New System.Drawing.Point(163, 135)
        Me.ocmholdpurchase.Name = "ocmholdpurchase"
        Me.ocmholdpurchase.Size = New System.Drawing.Size(142, 25)
        Me.ocmholdpurchase.TabIndex = 347
        Me.ocmholdpurchase.TabStop = False
        Me.ocmholdpurchase.Tag = "2|"
        Me.ocmholdpurchase.Text = "Hold Purchase"
        Me.ocmholdpurchase.Visible = False
        '
        'ocmDelayReasons
        '
        Me.ocmDelayReasons.Location = New System.Drawing.Point(609, 140)
        Me.ocmDelayReasons.Name = "ocmDelayReasons"
        Me.ocmDelayReasons.Size = New System.Drawing.Size(195, 25)
        Me.ocmDelayReasons.TabIndex = 346
        Me.ocmDelayReasons.TabStop = False
        Me.ocmDelayReasons.Tag = "2|"
        Me.ocmDelayReasons.Text = "DelayReasons"
        Me.ocmDelayReasons.Visible = False
        '
        'ocmFurtherDelayReason
        '
        Me.ocmFurtherDelayReason.Location = New System.Drawing.Point(609, 109)
        Me.ocmFurtherDelayReason.Name = "ocmFurtherDelayReason"
        Me.ocmFurtherDelayReason.Size = New System.Drawing.Size(195, 25)
        Me.ocmFurtherDelayReason.TabIndex = 345
        Me.ocmFurtherDelayReason.TabStop = False
        Me.ocmFurtherDelayReason.Tag = "2|"
        Me.ocmFurtherDelayReason.Text = "FurtherDelayReason"
        Me.ocmFurtherDelayReason.Visible = False
        '
        'ocmproduction
        '
        Me.ocmproduction.Location = New System.Drawing.Point(163, 73)
        Me.ocmproduction.Name = "ocmproduction"
        Me.ocmproduction.Size = New System.Drawing.Size(143, 25)
        Me.ocmproduction.TabIndex = 344
        Me.ocmproduction.TabStop = False
        Me.ocmproduction.Tag = "2|"
        Me.ocmproduction.Text = "Production"
        Me.ocmproduction.Visible = False
        '
        'ocmpurchase
        '
        Me.ocmpurchase.Location = New System.Drawing.Point(163, 43)
        Me.ocmpurchase.Name = "ocmpurchase"
        Me.ocmpurchase.Size = New System.Drawing.Size(143, 25)
        Me.ocmpurchase.TabIndex = 343
        Me.ocmpurchase.TabStop = False
        Me.ocmpurchase.Tag = "2|"
        Me.ocmpurchase.Text = "Purchase"
        Me.ocmpurchase.Visible = False
        '
        'ocmFTFinalOETCDate
        '
        Me.ocmFTFinalOETCDate.Location = New System.Drawing.Point(163, 104)
        Me.ocmFTFinalOETCDate.Name = "ocmFTFinalOETCDate"
        Me.ocmFTFinalOETCDate.Size = New System.Drawing.Size(143, 25)
        Me.ocmFTFinalOETCDate.TabIndex = 338
        Me.ocmFTFinalOETCDate.TabStop = False
        Me.ocmFTFinalOETCDate.Tag = "2|"
        Me.ocmFTFinalOETCDate.Text = "FTFinalOETCDate"
        Me.ocmFTFinalOETCDate.Visible = False
        '
        'ocmFTORGOETCDate
        '
        Me.ocmFTORGOETCDate.Location = New System.Drawing.Point(164, 166)
        Me.ocmFTORGOETCDate.Name = "ocmFTORGOETCDate"
        Me.ocmFTORGOETCDate.Size = New System.Drawing.Size(142, 25)
        Me.ocmFTORGOETCDate.TabIndex = 337
        Me.ocmFTORGOETCDate.TabStop = False
        Me.ocmFTORGOETCDate.Tag = "2|"
        Me.ocmFTORGOETCDate.Text = "FTORGOETCDate"
        Me.ocmFTORGOETCDate.Visible = False
        '
        'ocmsavetrack
        '
        Me.ocmsavetrack.Location = New System.Drawing.Point(163, 197)
        Me.ocmsavetrack.Name = "ocmsavetrack"
        Me.ocmsavetrack.Size = New System.Drawing.Size(142, 25)
        Me.ocmsavetrack.TabIndex = 335
        Me.ocmsavetrack.TabStop = False
        Me.ocmsavetrack.Tag = "2|"
        Me.ocmsavetrack.Text = "Save Track"
        Me.ocmsavetrack.Visible = False
        '
        'ocmsendmail
        '
        Me.ocmsendmail.Location = New System.Drawing.Point(163, 12)
        Me.ocmsendmail.Name = "ocmsendmail"
        Me.ocmsendmail.Size = New System.Drawing.Size(142, 25)
        Me.ocmsendmail.TabIndex = 333
        Me.ocmsendmail.TabStop = False
        Me.ocmsendmail.Tag = "2|"
        Me.ocmsendmail.Text = "E-MAIL"
        Me.ocmsendmail.Visible = False
        '
        'ocmsavelayout
        '
        Me.ocmsavelayout.Location = New System.Drawing.Point(14, 41)
        Me.ocmsavelayout.Name = "ocmsavelayout"
        Me.ocmsavelayout.Size = New System.Drawing.Size(142, 27)
        Me.ocmsavelayout.TabIndex = 332
        Me.ocmsavelayout.Text = "savelayoutgrid"
        '
        'ocmexit
        '
        Me.ocmexit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmexit.Location = New System.Drawing.Point(-759, 104)
        Me.ocmexit.Name = "ocmexit"
        Me.ocmexit.Size = New System.Drawing.Size(142, 25)
        Me.ocmexit.TabIndex = 96
        Me.ocmexit.TabStop = False
        Me.ocmexit.Tag = "2|"
        Me.ocmexit.Text = "EXIT"
        '
        'ocmclear
        '
        Me.ocmclear.Location = New System.Drawing.Point(14, 10)
        Me.ocmclear.Name = "ocmclear"
        Me.ocmclear.Size = New System.Drawing.Size(142, 25)
        Me.ocmclear.TabIndex = 95
        Me.ocmclear.TabStop = False
        Me.ocmclear.Tag = "2|"
        Me.ocmclear.Text = "CLEAR"
        '
        'ocmload
        '
        Me.ocmload.Location = New System.Drawing.Point(14, 74)
        Me.ocmload.Name = "ocmload"
        Me.ocmload.Size = New System.Drawing.Size(142, 23)
        Me.ocmload.TabIndex = 329
        Me.ocmload.Text = "Load Data"
        '
        'ogdtime
        '
        Me.ogdtime.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogdtime.Location = New System.Drawing.Point(2, 2)
        Me.ogdtime.MainView = Me.ogvtime
        Me.ogdtime.Name = "ogdtime"
        Me.ogdtime.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepCheckEdit, Me.RepositoryItemDateEdit1, Me.RepositoryItemTextFTInvoiceNo, Me.RepositoryItemMemoExFTNote, Me.RepositoryItemCalcQty, Me.RepositoryItemGridLookUpEditFTDelayReasonsCode, Me.RepositoryItemGridLookUpEditFTFurtherDelayReasonCode, Me.RepositoryItemCalcEditFNLeadTime, Me.RepositoryItemMemoExEditTrackNote, Me.RepositoryItemTextEditFTSendSMPStatus, Me.RepositoryItemPopupContainerTrackNote})
        Me.ogdtime.Size = New System.Drawing.Size(1314, 496)
        Me.ogdtime.TabIndex = 0
        Me.ogdtime.TabStop = False
        Me.ogdtime.Tag = "2|"
        Me.ogdtime.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvtime})
        '
        'ogvtime
        '
        Me.ogvtime.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.FTSelect, Me.xFTFirstInputDate, Me.FTFirstInputBy, Me.FTLastInputDate, Me.FTLastInputBy, Me.CFTPurchaseNo, Me.FDPurchaseDate, Me.FTPurchaseBy, Me.xFTPINo, Me.FTSuplCode, Me.FTSuplName, Me.FTSuplCalCode, Me.xFTNikeVenderCode, Me.xFTCOFOCode, Me.xFTCurCode, Me.FDDeliveryDate, Me.FDCFMDeliveryDate, Me.cxFTPOCustPO, Me.xFTPOOrderNo, Me.FTOrderDate, Me.FTDevConfirmDate, Me.xFTPOStyleCode, Me.FNHSysDeliveryID, Me.CXFTCmpCode, Me.FTBuyCode, Me.FTPartCode, Me.FTPartDetail, Me.FTRawMatCode, Me.xFTMainMatName, Me.xFTRawMatColorCode, Me.xFTRawMatSizeCode, Me.FTStateSuperVisorApp, Me.FTStateManagerApp, Me.FTStateSendMail, Me.FTSendMailBy, Me.FTSendMailDate, Me.FTSendMailTime, Me.xFTLastSendMailBy, Me.xFTLastSendMailDate, Me.xFTLastSendMailTime, Me.xxFNLeadTime, Me.FDSupCFMDelDate, Me.xFTOETCDate, Me.xFTORGOETCDate, Me.FTORGOETCInputBy, Me.FTORGOETCInputDate, Me.FTORGOETCInputTime, Me.FTFinalOETCDate, Me.FTFinalOETCInputBy, Me.FTFinalOETCInputDate, Me.FTFinalOETCInputTime, Me.xFNORGLeadTime, Me.FNFinalLeadTime, Me.FTDelayLangth, Me.FNPOQuantity, Me.FNUsedQuantity, Me.FTUsedUnit, Me.FNPOCFMQuantity, Me.xFTPOCFMNote, Me.xFNPROCFMQuantity, Me.xFTPROCFMNote, Me.xFNPOBALQuantity, Me.FTUnitCode, Me.xFTDelayReasonsCode, Me.FTDelayReasonsName, Me.FTFurtherDelayReasonCode, Me.FTFurtherDelayReasonName, Me.FNTrackSeq, Me.FTTrackDate, Me.FTTrackBy, Me.FTContactName, Me.xFTNote, Me.xFNHSysRawMatId, Me.xFNDocType, Me.xFTInvoiceNo, Me.xFTInvoiceNote, Me.xFTReserveNo, Me.xFTSendSMPDate, Me.xFTSendSMPStatus, Me.xFTSendSMPRemark, Me.xFTSendSMPAWB, Me.xFTSendSMPPayType, Me.xFTSamplePPC, Me.FTStatePDF, Me.FTStatePDFWaitPrice, Me.xFTStateCancel, Me.xFTStateHold, Me.xFTHoldReason, Me.FNDocType})
        Me.ogvtime.GridControl = Me.ogdtime
        Me.ogvtime.Name = "ogvtime"
        Me.ogvtime.OptionsCustomization.AllowQuickHideColumns = False
        Me.ogvtime.OptionsView.ColumnAutoWidth = False
        Me.ogvtime.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.ogvtime.OptionsView.ShowGroupPanel = False
        Me.ogvtime.Tag = "2|"
        '
        'FTSelect
        '
        Me.FTSelect.Caption = "Select"
        Me.FTSelect.ColumnEdit = Me.RepCheckEdit
        Me.FTSelect.FieldName = "FTSelect"
        Me.FTSelect.Name = "FTSelect"
        Me.FTSelect.OptionsColumn.AllowEdit = False
        Me.FTSelect.OptionsColumn.ReadOnly = True
        Me.FTSelect.Width = 59
        '
        'RepCheckEdit
        '
        Me.RepCheckEdit.AutoHeight = False
        Me.RepCheckEdit.Caption = "Check"
        Me.RepCheckEdit.Name = "RepCheckEdit"
        Me.RepCheckEdit.ValueChecked = "1"
        Me.RepCheckEdit.ValueUnchecked = "0"
        '
        'xFTFirstInputDate
        '
        Me.xFTFirstInputDate.Caption = "First Input Date"
        Me.xFTFirstInputDate.FieldName = "FTFirstInputDate"
        Me.xFTFirstInputDate.Name = "xFTFirstInputDate"
        Me.xFTFirstInputDate.OptionsColumn.AllowEdit = False
        Me.xFTFirstInputDate.OptionsColumn.ReadOnly = True
        Me.xFTFirstInputDate.Visible = True
        Me.xFTFirstInputDate.VisibleIndex = 0
        Me.xFTFirstInputDate.Width = 92
        '
        'FTFirstInputBy
        '
        Me.FTFirstInputBy.AppearanceCell.Options.UseTextOptions = True
        Me.FTFirstInputBy.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTFirstInputBy.AppearanceHeader.Options.UseTextOptions = True
        Me.FTFirstInputBy.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTFirstInputBy.Caption = "First Input By"
        Me.FTFirstInputBy.FieldName = "FTFirstInputBy"
        Me.FTFirstInputBy.Name = "FTFirstInputBy"
        Me.FTFirstInputBy.OptionsColumn.AllowEdit = False
        Me.FTFirstInputBy.OptionsColumn.ReadOnly = True
        Me.FTFirstInputBy.Visible = True
        Me.FTFirstInputBy.VisibleIndex = 1
        Me.FTFirstInputBy.Width = 85
        '
        'FTLastInputDate
        '
        Me.FTLastInputDate.AppearanceCell.Options.UseTextOptions = True
        Me.FTLastInputDate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTLastInputDate.AppearanceHeader.Options.UseTextOptions = True
        Me.FTLastInputDate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTLastInputDate.Caption = "Last Input Date"
        Me.FTLastInputDate.FieldName = "FTLastInputDate"
        Me.FTLastInputDate.Name = "FTLastInputDate"
        Me.FTLastInputDate.OptionsColumn.AllowEdit = False
        Me.FTLastInputDate.OptionsColumn.ReadOnly = True
        Me.FTLastInputDate.Visible = True
        Me.FTLastInputDate.VisibleIndex = 2
        Me.FTLastInputDate.Width = 85
        '
        'FTLastInputBy
        '
        Me.FTLastInputBy.AppearanceCell.Options.UseTextOptions = True
        Me.FTLastInputBy.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTLastInputBy.AppearanceHeader.Options.UseTextOptions = True
        Me.FTLastInputBy.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTLastInputBy.Caption = "Last Input By"
        Me.FTLastInputBy.FieldName = "FTLastInputBy"
        Me.FTLastInputBy.Name = "FTLastInputBy"
        Me.FTLastInputBy.OptionsColumn.AllowEdit = False
        Me.FTLastInputBy.OptionsColumn.ReadOnly = True
        Me.FTLastInputBy.Visible = True
        Me.FTLastInputBy.VisibleIndex = 3
        Me.FTLastInputBy.Width = 98
        '
        'CFTPurchaseNo
        '
        Me.CFTPurchaseNo.AppearanceCell.Options.UseTextOptions = True
        Me.CFTPurchaseNo.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.CFTPurchaseNo.AppearanceHeader.Options.UseTextOptions = True
        Me.CFTPurchaseNo.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.CFTPurchaseNo.Caption = "Purchase No"
        Me.CFTPurchaseNo.FieldName = "FTPurchaseNo"
        Me.CFTPurchaseNo.Name = "CFTPurchaseNo"
        Me.CFTPurchaseNo.OptionsColumn.AllowEdit = False
        Me.CFTPurchaseNo.OptionsColumn.ReadOnly = True
        Me.CFTPurchaseNo.Visible = True
        Me.CFTPurchaseNo.VisibleIndex = 4
        Me.CFTPurchaseNo.Width = 100
        '
        'FDPurchaseDate
        '
        Me.FDPurchaseDate.AppearanceCell.Options.UseTextOptions = True
        Me.FDPurchaseDate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FDPurchaseDate.AppearanceHeader.Options.UseTextOptions = True
        Me.FDPurchaseDate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FDPurchaseDate.Caption = "Purchase Date"
        Me.FDPurchaseDate.FieldName = "FDPurchaseDate"
        Me.FDPurchaseDate.Name = "FDPurchaseDate"
        Me.FDPurchaseDate.OptionsColumn.AllowEdit = False
        Me.FDPurchaseDate.OptionsColumn.ReadOnly = True
        Me.FDPurchaseDate.Visible = True
        Me.FDPurchaseDate.VisibleIndex = 5
        Me.FDPurchaseDate.Width = 80
        '
        'FTPurchaseBy
        '
        Me.FTPurchaseBy.AppearanceCell.Options.UseTextOptions = True
        Me.FTPurchaseBy.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTPurchaseBy.Caption = "Purchase By"
        Me.FTPurchaseBy.FieldName = "FTPurchaseBy"
        Me.FTPurchaseBy.Name = "FTPurchaseBy"
        Me.FTPurchaseBy.OptionsColumn.AllowEdit = False
        Me.FTPurchaseBy.OptionsColumn.ReadOnly = True
        Me.FTPurchaseBy.Visible = True
        Me.FTPurchaseBy.VisibleIndex = 6
        Me.FTPurchaseBy.Width = 100
        '
        'xFTPINo
        '
        Me.xFTPINo.Caption = "PI/Invoice No"
        Me.xFTPINo.FieldName = "FTPINo"
        Me.xFTPINo.Name = "xFTPINo"
        Me.xFTPINo.OptionsColumn.AllowEdit = False
        Me.xFTPINo.OptionsColumn.ReadOnly = True
        Me.xFTPINo.Visible = True
        Me.xFTPINo.VisibleIndex = 7
        Me.xFTPINo.Width = 150
        '
        'FTSuplCode
        '
        Me.FTSuplCode.AppearanceCell.Options.UseTextOptions = True
        Me.FTSuplCode.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTSuplCode.AppearanceHeader.Options.UseTextOptions = True
        Me.FTSuplCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTSuplCode.Caption = "Supplier Code"
        Me.FTSuplCode.FieldName = "FTSuplCode"
        Me.FTSuplCode.Name = "FTSuplCode"
        Me.FTSuplCode.OptionsColumn.AllowEdit = False
        Me.FTSuplCode.OptionsColumn.ReadOnly = True
        Me.FTSuplCode.Visible = True
        Me.FTSuplCode.VisibleIndex = 8
        Me.FTSuplCode.Width = 100
        '
        'FTSuplName
        '
        Me.FTSuplName.AppearanceCell.Options.UseTextOptions = True
        Me.FTSuplName.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTSuplName.AppearanceHeader.Options.UseTextOptions = True
        Me.FTSuplName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTSuplName.Caption = "Supplier Name"
        Me.FTSuplName.FieldName = "FTSuplName"
        Me.FTSuplName.Name = "FTSuplName"
        Me.FTSuplName.OptionsColumn.AllowEdit = False
        Me.FTSuplName.OptionsColumn.ReadOnly = True
        Me.FTSuplName.Visible = True
        Me.FTSuplName.VisibleIndex = 9
        Me.FTSuplName.Width = 200
        '
        'FTSuplCalCode
        '
        Me.FTSuplCalCode.AppearanceCell.Options.UseTextOptions = True
        Me.FTSuplCalCode.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTSuplCalCode.AppearanceHeader.Options.UseTextOptions = True
        Me.FTSuplCalCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTSuplCalCode.Caption = "FTSuplCalCode"
        Me.FTSuplCalCode.FieldName = "FTSuplCalCode"
        Me.FTSuplCalCode.Name = "FTSuplCalCode"
        Me.FTSuplCalCode.OptionsColumn.AllowEdit = False
        Me.FTSuplCalCode.OptionsColumn.ReadOnly = True
        Me.FTSuplCalCode.Visible = True
        Me.FTSuplCalCode.VisibleIndex = 10
        '
        'xFTNikeVenderCode
        '
        Me.xFTNikeVenderCode.AppearanceCell.Options.UseTextOptions = True
        Me.xFTNikeVenderCode.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.xFTNikeVenderCode.Caption = "Nike Vender Code"
        Me.xFTNikeVenderCode.FieldName = "FTNikeVenderCode"
        Me.xFTNikeVenderCode.Name = "xFTNikeVenderCode"
        Me.xFTNikeVenderCode.OptionsColumn.AllowEdit = False
        Me.xFTNikeVenderCode.OptionsColumn.ReadOnly = True
        Me.xFTNikeVenderCode.Visible = True
        Me.xFTNikeVenderCode.VisibleIndex = 11
        Me.xFTNikeVenderCode.Width = 80
        '
        'xFTCOFOCode
        '
        Me.xFTCOFOCode.AppearanceCell.Options.UseTextOptions = True
        Me.xFTCOFOCode.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.xFTCOFOCode.Caption = "C OF O"
        Me.xFTCOFOCode.FieldName = "FTCOFOCode"
        Me.xFTCOFOCode.Name = "xFTCOFOCode"
        Me.xFTCOFOCode.OptionsColumn.AllowEdit = False
        Me.xFTCOFOCode.OptionsColumn.ReadOnly = True
        Me.xFTCOFOCode.Visible = True
        Me.xFTCOFOCode.VisibleIndex = 12
        Me.xFTCOFOCode.Width = 80
        '
        'xFTCurCode
        '
        Me.xFTCurCode.AppearanceCell.Options.UseTextOptions = True
        Me.xFTCurCode.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.xFTCurCode.Caption = "Currency"
        Me.xFTCurCode.FieldName = "FTCurCode"
        Me.xFTCurCode.Name = "xFTCurCode"
        Me.xFTCurCode.OptionsColumn.AllowEdit = False
        Me.xFTCurCode.OptionsColumn.ReadOnly = True
        Me.xFTCurCode.Visible = True
        Me.xFTCurCode.VisibleIndex = 13
        '
        'FDDeliveryDate
        '
        Me.FDDeliveryDate.AppearanceCell.Options.UseTextOptions = True
        Me.FDDeliveryDate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FDDeliveryDate.AppearanceHeader.Options.UseTextOptions = True
        Me.FDDeliveryDate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FDDeliveryDate.Caption = "Delivery Date"
        Me.FDDeliveryDate.FieldName = "FDDeliveryDate"
        Me.FDDeliveryDate.Name = "FDDeliveryDate"
        Me.FDDeliveryDate.OptionsColumn.AllowEdit = False
        Me.FDDeliveryDate.OptionsColumn.ReadOnly = True
        Me.FDDeliveryDate.Visible = True
        Me.FDDeliveryDate.VisibleIndex = 15
        Me.FDDeliveryDate.Width = 80
        '
        'FDCFMDeliveryDate
        '
        Me.FDCFMDeliveryDate.Caption = "PI Confirm Delivery Date"
        Me.FDCFMDeliveryDate.FieldName = "FDCFMDeliveryDate"
        Me.FDCFMDeliveryDate.Name = "FDCFMDeliveryDate"
        Me.FDCFMDeliveryDate.OptionsColumn.AllowEdit = False
        Me.FDCFMDeliveryDate.OptionsColumn.ReadOnly = True
        Me.FDCFMDeliveryDate.Visible = True
        Me.FDCFMDeliveryDate.VisibleIndex = 14
        '
        'cxFTPOCustPO
        '
        Me.cxFTPOCustPO.AppearanceCell.Options.UseTextOptions = True
        Me.cxFTPOCustPO.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.cxFTPOCustPO.AppearanceHeader.Options.UseTextOptions = True
        Me.cxFTPOCustPO.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.cxFTPOCustPO.Caption = "Customer Po"
        Me.cxFTPOCustPO.FieldName = "FTPOCustPO"
        Me.cxFTPOCustPO.Name = "cxFTPOCustPO"
        Me.cxFTPOCustPO.OptionsColumn.AllowEdit = False
        Me.cxFTPOCustPO.OptionsColumn.ReadOnly = True
        Me.cxFTPOCustPO.Visible = True
        Me.cxFTPOCustPO.VisibleIndex = 16
        Me.cxFTPOCustPO.Width = 120
        '
        'xFTPOOrderNo
        '
        Me.xFTPOOrderNo.AppearanceCell.Options.UseTextOptions = True
        Me.xFTPOOrderNo.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.xFTPOOrderNo.Caption = "Order No"
        Me.xFTPOOrderNo.FieldName = "FTPOOrderNo"
        Me.xFTPOOrderNo.Name = "xFTPOOrderNo"
        Me.xFTPOOrderNo.OptionsColumn.AllowEdit = False
        Me.xFTPOOrderNo.OptionsColumn.ReadOnly = True
        Me.xFTPOOrderNo.Visible = True
        Me.xFTPOOrderNo.VisibleIndex = 17
        Me.xFTPOOrderNo.Width = 120
        '
        'FTOrderDate
        '
        Me.FTOrderDate.AppearanceCell.Options.UseTextOptions = True
        Me.FTOrderDate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTOrderDate.Caption = "Order Date"
        Me.FTOrderDate.FieldName = "FTOrderDate"
        Me.FTOrderDate.Name = "FTOrderDate"
        Me.FTOrderDate.OptionsColumn.AllowEdit = False
        Me.FTOrderDate.OptionsColumn.ReadOnly = True
        Me.FTOrderDate.Visible = True
        Me.FTOrderDate.VisibleIndex = 18
        '
        'FTDevConfirmDate
        '
        Me.FTDevConfirmDate.AppearanceCell.Options.UseTextOptions = True
        Me.FTDevConfirmDate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTDevConfirmDate.Caption = "Dev CFM Date"
        Me.FTDevConfirmDate.FieldName = "FTDevConfirmDate"
        Me.FTDevConfirmDate.Name = "FTDevConfirmDate"
        Me.FTDevConfirmDate.OptionsColumn.AllowEdit = False
        Me.FTDevConfirmDate.OptionsColumn.ReadOnly = True
        Me.FTDevConfirmDate.Visible = True
        Me.FTDevConfirmDate.VisibleIndex = 19
        '
        'xFTPOStyleCode
        '
        Me.xFTPOStyleCode.AppearanceCell.Options.UseTextOptions = True
        Me.xFTPOStyleCode.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.xFTPOStyleCode.Caption = "Style No"
        Me.xFTPOStyleCode.FieldName = "FTPOStyleCode"
        Me.xFTPOStyleCode.Name = "xFTPOStyleCode"
        Me.xFTPOStyleCode.OptionsColumn.AllowEdit = False
        Me.xFTPOStyleCode.OptionsColumn.ReadOnly = True
        Me.xFTPOStyleCode.Visible = True
        Me.xFTPOStyleCode.VisibleIndex = 20
        Me.xFTPOStyleCode.Width = 120
        '
        'FNHSysDeliveryID
        '
        Me.FNHSysDeliveryID.AppearanceCell.Options.UseTextOptions = True
        Me.FNHSysDeliveryID.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FNHSysDeliveryID.Caption = "Delivery"
        Me.FNHSysDeliveryID.FieldName = "FTDeliveryName"
        Me.FNHSysDeliveryID.Name = "FNHSysDeliveryID"
        Me.FNHSysDeliveryID.OptionsColumn.AllowEdit = False
        Me.FNHSysDeliveryID.OptionsColumn.ReadOnly = True
        Me.FNHSysDeliveryID.Visible = True
        Me.FNHSysDeliveryID.VisibleIndex = 21
        Me.FNHSysDeliveryID.Width = 100
        '
        'CXFTCmpCode
        '
        Me.CXFTCmpCode.AppearanceCell.Options.UseTextOptions = True
        Me.CXFTCmpCode.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.CXFTCmpCode.Caption = "Company"
        Me.CXFTCmpCode.FieldName = "FTCmpCode"
        Me.CXFTCmpCode.Name = "CXFTCmpCode"
        Me.CXFTCmpCode.OptionsColumn.AllowEdit = False
        Me.CXFTCmpCode.OptionsColumn.ReadOnly = True
        Me.CXFTCmpCode.Visible = True
        Me.CXFTCmpCode.VisibleIndex = 22
        '
        'FTBuyCode
        '
        Me.FTBuyCode.AppearanceCell.Options.UseTextOptions = True
        Me.FTBuyCode.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTBuyCode.Caption = "Buy"
        Me.FTBuyCode.FieldName = "FTBuyMonth"
        Me.FTBuyCode.Name = "FTBuyCode"
        Me.FTBuyCode.OptionsColumn.AllowEdit = False
        Me.FTBuyCode.OptionsColumn.ReadOnly = True
        Me.FTBuyCode.Visible = True
        Me.FTBuyCode.VisibleIndex = 23
        '
        'FTPartCode
        '
        Me.FTPartCode.AppearanceCell.Options.UseTextOptions = True
        Me.FTPartCode.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTPartCode.AppearanceHeader.Options.UseTextOptions = True
        Me.FTPartCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTPartCode.Caption = "FTPartCode"
        Me.FTPartCode.FieldName = "FTPartCode"
        Me.FTPartCode.Name = "FTPartCode"
        Me.FTPartCode.OptionsColumn.AllowEdit = False
        Me.FTPartCode.OptionsColumn.ReadOnly = True
        Me.FTPartCode.Visible = True
        Me.FTPartCode.VisibleIndex = 24
        '
        'FTPartDetail
        '
        Me.FTPartDetail.AppearanceCell.Options.UseTextOptions = True
        Me.FTPartDetail.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTPartDetail.AppearanceHeader.Options.UseTextOptions = True
        Me.FTPartDetail.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTPartDetail.Caption = "Part Details"
        Me.FTPartDetail.FieldName = "FTPartDetail"
        Me.FTPartDetail.Name = "FTPartDetail"
        Me.FTPartDetail.OptionsColumn.AllowEdit = False
        Me.FTPartDetail.OptionsColumn.ReadOnly = True
        Me.FTPartDetail.Visible = True
        Me.FTPartDetail.VisibleIndex = 25
        Me.FTPartDetail.Width = 50
        '
        'FTRawMatCode
        '
        Me.FTRawMatCode.Caption = "Material Code"
        Me.FTRawMatCode.FieldName = "FTRawMatCode"
        Me.FTRawMatCode.Name = "FTRawMatCode"
        Me.FTRawMatCode.OptionsColumn.AllowEdit = False
        Me.FTRawMatCode.OptionsColumn.ReadOnly = True
        Me.FTRawMatCode.Visible = True
        Me.FTRawMatCode.VisibleIndex = 26
        Me.FTRawMatCode.Width = 120
        '
        'xFTMainMatName
        '
        Me.xFTMainMatName.Caption = "Desctiption"
        Me.xFTMainMatName.FieldName = "FTMainMatName"
        Me.xFTMainMatName.Name = "xFTMainMatName"
        Me.xFTMainMatName.OptionsColumn.AllowEdit = False
        Me.xFTMainMatName.OptionsColumn.ReadOnly = True
        Me.xFTMainMatName.Visible = True
        Me.xFTMainMatName.VisibleIndex = 27
        Me.xFTMainMatName.Width = 200
        '
        'xFTRawMatColorCode
        '
        Me.xFTRawMatColorCode.Caption = "Color Code"
        Me.xFTRawMatColorCode.FieldName = "FTRawMatColorCode"
        Me.xFTRawMatColorCode.Name = "xFTRawMatColorCode"
        Me.xFTRawMatColorCode.OptionsColumn.AllowEdit = False
        Me.xFTRawMatColorCode.OptionsColumn.ReadOnly = True
        Me.xFTRawMatColorCode.Visible = True
        Me.xFTRawMatColorCode.VisibleIndex = 28
        '
        'xFTRawMatSizeCode
        '
        Me.xFTRawMatSizeCode.Caption = "Size Code"
        Me.xFTRawMatSizeCode.FieldName = "FTRawMatSizeCode"
        Me.xFTRawMatSizeCode.Name = "xFTRawMatSizeCode"
        Me.xFTRawMatSizeCode.OptionsColumn.AllowEdit = False
        Me.xFTRawMatSizeCode.OptionsColumn.ReadOnly = True
        Me.xFTRawMatSizeCode.Visible = True
        Me.xFTRawMatSizeCode.VisibleIndex = 29
        '
        'FTStateSuperVisorApp
        '
        Me.FTStateSuperVisorApp.AppearanceCell.Options.UseTextOptions = True
        Me.FTStateSuperVisorApp.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTStateSuperVisorApp.Caption = "Sup App."
        Me.FTStateSuperVisorApp.ColumnEdit = Me.RepCheckEdit
        Me.FTStateSuperVisorApp.FieldName = "FTStateSuperVisorApp"
        Me.FTStateSuperVisorApp.Name = "FTStateSuperVisorApp"
        Me.FTStateSuperVisorApp.OptionsColumn.AllowEdit = False
        Me.FTStateSuperVisorApp.OptionsColumn.ReadOnly = True
        Me.FTStateSuperVisorApp.Visible = True
        Me.FTStateSuperVisorApp.VisibleIndex = 30
        '
        'FTStateManagerApp
        '
        Me.FTStateManagerApp.AppearanceCell.Options.UseTextOptions = True
        Me.FTStateManagerApp.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTStateManagerApp.Caption = "Manager App."
        Me.FTStateManagerApp.ColumnEdit = Me.RepCheckEdit
        Me.FTStateManagerApp.FieldName = "FTStateManagerApp"
        Me.FTStateManagerApp.Name = "FTStateManagerApp"
        Me.FTStateManagerApp.OptionsColumn.AllowEdit = False
        Me.FTStateManagerApp.OptionsColumn.ReadOnly = True
        Me.FTStateManagerApp.Visible = True
        Me.FTStateManagerApp.VisibleIndex = 31
        '
        'FTStateSendMail
        '
        Me.FTStateSendMail.AppearanceCell.Options.UseTextOptions = True
        Me.FTStateSendMail.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTStateSendMail.Caption = "Send Mail"
        Me.FTStateSendMail.ColumnEdit = Me.RepCheckEdit
        Me.FTStateSendMail.FieldName = "FTStateSendMail"
        Me.FTStateSendMail.Name = "FTStateSendMail"
        Me.FTStateSendMail.OptionsColumn.AllowEdit = False
        Me.FTStateSendMail.OptionsColumn.ReadOnly = True
        Me.FTStateSendMail.Visible = True
        Me.FTStateSendMail.VisibleIndex = 32
        '
        'FTSendMailBy
        '
        Me.FTSendMailBy.AppearanceCell.Options.UseTextOptions = True
        Me.FTSendMailBy.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTSendMailBy.Caption = "Send Mai lBy"
        Me.FTSendMailBy.FieldName = "FTSendMailBy"
        Me.FTSendMailBy.Name = "FTSendMailBy"
        Me.FTSendMailBy.OptionsColumn.AllowEdit = False
        Me.FTSendMailBy.OptionsColumn.ReadOnly = True
        Me.FTSendMailBy.Visible = True
        Me.FTSendMailBy.VisibleIndex = 33
        '
        'FTSendMailDate
        '
        Me.FTSendMailDate.AppearanceCell.Options.UseTextOptions = True
        Me.FTSendMailDate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTSendMailDate.Caption = "Send Mail Date"
        Me.FTSendMailDate.FieldName = "FTSendMailDate"
        Me.FTSendMailDate.Name = "FTSendMailDate"
        Me.FTSendMailDate.OptionsColumn.AllowEdit = False
        Me.FTSendMailDate.OptionsColumn.ReadOnly = True
        Me.FTSendMailDate.Visible = True
        Me.FTSendMailDate.VisibleIndex = 34
        '
        'FTSendMailTime
        '
        Me.FTSendMailTime.AppearanceCell.Options.UseTextOptions = True
        Me.FTSendMailTime.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTSendMailTime.Caption = "Send Mail Time"
        Me.FTSendMailTime.FieldName = "FTSendMailTime"
        Me.FTSendMailTime.Name = "FTSendMailTime"
        Me.FTSendMailTime.OptionsColumn.AllowEdit = False
        Me.FTSendMailTime.OptionsColumn.ReadOnly = True
        Me.FTSendMailTime.Visible = True
        Me.FTSendMailTime.VisibleIndex = 35
        '
        'xFTLastSendMailBy
        '
        Me.xFTLastSendMailBy.Caption = "Last Mai lBy"
        Me.xFTLastSendMailBy.FieldName = "FTLastSendMailBy"
        Me.xFTLastSendMailBy.Name = "xFTLastSendMailBy"
        Me.xFTLastSendMailBy.OptionsColumn.AllowEdit = False
        Me.xFTLastSendMailBy.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.xFTLastSendMailBy.OptionsColumn.ReadOnly = True
        Me.xFTLastSendMailBy.Visible = True
        Me.xFTLastSendMailBy.VisibleIndex = 36
        '
        'xFTLastSendMailDate
        '
        Me.xFTLastSendMailDate.AppearanceCell.Options.UseTextOptions = True
        Me.xFTLastSendMailDate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.xFTLastSendMailDate.Caption = "Last Mail Date"
        Me.xFTLastSendMailDate.FieldName = "FTLastSendMailDate"
        Me.xFTLastSendMailDate.Name = "xFTLastSendMailDate"
        Me.xFTLastSendMailDate.OptionsColumn.AllowEdit = False
        Me.xFTLastSendMailDate.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.xFTLastSendMailDate.OptionsColumn.ReadOnly = True
        Me.xFTLastSendMailDate.Visible = True
        Me.xFTLastSendMailDate.VisibleIndex = 37
        '
        'xFTLastSendMailTime
        '
        Me.xFTLastSendMailTime.AppearanceCell.Options.UseTextOptions = True
        Me.xFTLastSendMailTime.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.xFTLastSendMailTime.Caption = "Last Mail Time"
        Me.xFTLastSendMailTime.FieldName = "FTLastSendMailTime"
        Me.xFTLastSendMailTime.Name = "xFTLastSendMailTime"
        Me.xFTLastSendMailTime.OptionsColumn.AllowEdit = False
        Me.xFTLastSendMailTime.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.xFTLastSendMailTime.OptionsColumn.ReadOnly = True
        Me.xFTLastSendMailTime.Visible = True
        Me.xFTLastSendMailTime.VisibleIndex = 38
        '
        'xxFNLeadTime
        '
        Me.xxFNLeadTime.Caption = "LeadTime"
        Me.xxFNLeadTime.ColumnEdit = Me.RepositoryItemCalcEditFNLeadTime
        Me.xxFNLeadTime.DisplayFormat.FormatString = "{0:n0}"
        Me.xxFNLeadTime.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.xxFNLeadTime.FieldName = "FNLeadTime"
        Me.xxFNLeadTime.Name = "xxFNLeadTime"
        Me.xxFNLeadTime.OptionsColumn.AllowEdit = False
        Me.xxFNLeadTime.OptionsColumn.ReadOnly = True
        Me.xxFNLeadTime.Visible = True
        Me.xxFNLeadTime.VisibleIndex = 39
        '
        'RepositoryItemCalcEditFNLeadTime
        '
        Me.RepositoryItemCalcEditFNLeadTime.AutoHeight = False
        Me.RepositoryItemCalcEditFNLeadTime.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemCalcEditFNLeadTime.DisplayFormat.FormatString = "{0:n0}"
        Me.RepositoryItemCalcEditFNLeadTime.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.RepositoryItemCalcEditFNLeadTime.EditFormat.FormatString = "{0:n0}"
        Me.RepositoryItemCalcEditFNLeadTime.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.RepositoryItemCalcEditFNLeadTime.Name = "RepositoryItemCalcEditFNLeadTime"
        '
        'FDSupCFMDelDate
        '
        Me.FDSupCFMDelDate.AppearanceCell.Options.UseTextOptions = True
        Me.FDSupCFMDelDate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FDSupCFMDelDate.AppearanceHeader.Options.UseTextOptions = True
        Me.FDSupCFMDelDate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FDSupCFMDelDate.Caption = "Sup CFM Del Date"
        Me.FDSupCFMDelDate.FieldName = "FDSupCFMDelDate"
        Me.FDSupCFMDelDate.Name = "FDSupCFMDelDate"
        Me.FDSupCFMDelDate.OptionsColumn.AllowEdit = False
        Me.FDSupCFMDelDate.OptionsColumn.ReadOnly = True
        Me.FDSupCFMDelDate.Visible = True
        Me.FDSupCFMDelDate.VisibleIndex = 40
        '
        'xFTOETCDate
        '
        Me.xFTOETCDate.AppearanceCell.Options.UseTextOptions = True
        Me.xFTOETCDate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.xFTOETCDate.Caption = "OETC Date"
        Me.xFTOETCDate.FieldName = "FTOETCDate"
        Me.xFTOETCDate.Name = "xFTOETCDate"
        Me.xFTOETCDate.OptionsColumn.AllowEdit = False
        Me.xFTOETCDate.OptionsColumn.ReadOnly = True
        Me.xFTOETCDate.Visible = True
        Me.xFTOETCDate.VisibleIndex = 41
        Me.xFTOETCDate.Width = 80
        '
        'xFTORGOETCDate
        '
        Me.xFTORGOETCDate.AppearanceCell.Options.UseTextOptions = True
        Me.xFTORGOETCDate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.xFTORGOETCDate.Caption = "Orig CETC date"
        Me.xFTORGOETCDate.ColumnEdit = Me.RepositoryItemDateEdit1
        Me.xFTORGOETCDate.FieldName = "FTORGOETCDate"
        Me.xFTORGOETCDate.Name = "xFTORGOETCDate"
        Me.xFTORGOETCDate.OptionsColumn.AllowEdit = False
        Me.xFTORGOETCDate.OptionsColumn.ReadOnly = True
        Me.xFTORGOETCDate.Visible = True
        Me.xFTORGOETCDate.VisibleIndex = 42
        '
        'RepositoryItemDateEdit1
        '
        Me.RepositoryItemDateEdit1.AutoHeight = False
        Me.RepositoryItemDateEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemDateEdit1.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemDateEdit1.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.RepositoryItemDateEdit1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.RepositoryItemDateEdit1.EditFormat.FormatString = "dd/MM/yyyy"
        Me.RepositoryItemDateEdit1.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.RepositoryItemDateEdit1.Mask.EditMask = "dd/MM/yyyy"
        Me.RepositoryItemDateEdit1.Name = "RepositoryItemDateEdit1"
        '
        'FTORGOETCInputBy
        '
        Me.FTORGOETCInputBy.Caption = "FTORGOETCInputBy"
        Me.FTORGOETCInputBy.FieldName = "FTORGOETCInputBy"
        Me.FTORGOETCInputBy.Name = "FTORGOETCInputBy"
        Me.FTORGOETCInputBy.OptionsColumn.AllowEdit = False
        Me.FTORGOETCInputBy.OptionsColumn.ReadOnly = True
        '
        'FTORGOETCInputDate
        '
        Me.FTORGOETCInputDate.Caption = "FTORGOETCInputDate"
        Me.FTORGOETCInputDate.FieldName = "FTORGOETCInputDate"
        Me.FTORGOETCInputDate.Name = "FTORGOETCInputDate"
        Me.FTORGOETCInputDate.OptionsColumn.AllowEdit = False
        Me.FTORGOETCInputDate.OptionsColumn.ReadOnly = True
        '
        'FTORGOETCInputTime
        '
        Me.FTORGOETCInputTime.Caption = "FTORGOETCInputTime"
        Me.FTORGOETCInputTime.FieldName = "FTORGOETCInputTime"
        Me.FTORGOETCInputTime.Name = "FTORGOETCInputTime"
        Me.FTORGOETCInputTime.OptionsColumn.AllowEdit = False
        Me.FTORGOETCInputTime.OptionsColumn.ReadOnly = True
        '
        'FTFinalOETCDate
        '
        Me.FTFinalOETCDate.AppearanceCell.Options.UseTextOptions = True
        Me.FTFinalOETCDate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTFinalOETCDate.Caption = "Final CETC date"
        Me.FTFinalOETCDate.ColumnEdit = Me.RepositoryItemDateEdit1
        Me.FTFinalOETCDate.FieldName = "FTFinalOETCDate"
        Me.FTFinalOETCDate.Name = "FTFinalOETCDate"
        Me.FTFinalOETCDate.OptionsColumn.AllowEdit = False
        Me.FTFinalOETCDate.OptionsColumn.ReadOnly = True
        Me.FTFinalOETCDate.Visible = True
        Me.FTFinalOETCDate.VisibleIndex = 43
        '
        'FTFinalOETCInputBy
        '
        Me.FTFinalOETCInputBy.Caption = "FTFinalOETCInputBy"
        Me.FTFinalOETCInputBy.FieldName = "FTFinalOETCInputBy"
        Me.FTFinalOETCInputBy.Name = "FTFinalOETCInputBy"
        Me.FTFinalOETCInputBy.OptionsColumn.AllowEdit = False
        Me.FTFinalOETCInputBy.OptionsColumn.ReadOnly = True
        '
        'FTFinalOETCInputDate
        '
        Me.FTFinalOETCInputDate.Caption = "FTFinalOETCInputDate"
        Me.FTFinalOETCInputDate.FieldName = "FTFinalOETCInputDate"
        Me.FTFinalOETCInputDate.Name = "FTFinalOETCInputDate"
        Me.FTFinalOETCInputDate.OptionsColumn.AllowEdit = False
        Me.FTFinalOETCInputDate.OptionsColumn.ReadOnly = True
        '
        'FTFinalOETCInputTime
        '
        Me.FTFinalOETCInputTime.Caption = "FTFinalOETCInputTime"
        Me.FTFinalOETCInputTime.FieldName = "FTFinalOETCInputTime"
        Me.FTFinalOETCInputTime.Name = "FTFinalOETCInputTime"
        Me.FTFinalOETCInputTime.OptionsColumn.AllowEdit = False
        Me.FTFinalOETCInputTime.OptionsColumn.ReadOnly = True
        '
        'xFNORGLeadTime
        '
        Me.xFNORGLeadTime.Caption = "FNORGLeadTime"
        Me.xFNORGLeadTime.FieldName = "FNORGLeadTime"
        Me.xFNORGLeadTime.Name = "xFNORGLeadTime"
        Me.xFNORGLeadTime.OptionsColumn.AllowEdit = False
        Me.xFNORGLeadTime.OptionsColumn.ReadOnly = True
        Me.xFNORGLeadTime.Visible = True
        Me.xFNORGLeadTime.VisibleIndex = 44
        '
        'FNFinalLeadTime
        '
        Me.FNFinalLeadTime.Caption = "FNFinalLeadTime"
        Me.FNFinalLeadTime.FieldName = "FNFinalLeadTime"
        Me.FNFinalLeadTime.Name = "FNFinalLeadTime"
        Me.FNFinalLeadTime.OptionsColumn.AllowEdit = False
        Me.FNFinalLeadTime.OptionsColumn.ReadOnly = True
        Me.FNFinalLeadTime.Visible = True
        Me.FNFinalLeadTime.VisibleIndex = 45
        '
        'FTDelayLangth
        '
        Me.FTDelayLangth.Caption = "FTDelayLangth"
        Me.FTDelayLangth.FieldName = "FTDelayLangth"
        Me.FTDelayLangth.Name = "FTDelayLangth"
        Me.FTDelayLangth.OptionsColumn.AllowEdit = False
        Me.FTDelayLangth.OptionsColumn.ReadOnly = True
        Me.FTDelayLangth.Visible = True
        Me.FTDelayLangth.VisibleIndex = 46
        '
        'FNPOQuantity
        '
        Me.FNPOQuantity.AppearanceCell.Options.UseTextOptions = True
        Me.FNPOQuantity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNPOQuantity.AppearanceHeader.Options.UseTextOptions = True
        Me.FNPOQuantity.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FNPOQuantity.Caption = "PO Qty"
        Me.FNPOQuantity.DisplayFormat.FormatString = "{0:n4}"
        Me.FNPOQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNPOQuantity.FieldName = "FNPOQuantity"
        Me.FNPOQuantity.Name = "FNPOQuantity"
        Me.FNPOQuantity.OptionsColumn.AllowEdit = False
        Me.FNPOQuantity.OptionsColumn.ReadOnly = True
        Me.FNPOQuantity.Visible = True
        Me.FNPOQuantity.VisibleIndex = 47
        Me.FNPOQuantity.Width = 80
        '
        'FNUsedQuantity
        '
        Me.FNUsedQuantity.Caption = "Used Qty"
        Me.FNUsedQuantity.FieldName = "FNUsedQuantity"
        Me.FNUsedQuantity.Name = "FNUsedQuantity"
        Me.FNUsedQuantity.OptionsColumn.AllowEdit = False
        Me.FNUsedQuantity.OptionsColumn.ReadOnly = True
        Me.FNUsedQuantity.Visible = True
        Me.FNUsedQuantity.VisibleIndex = 48
        '
        'FTUsedUnit
        '
        Me.FTUsedUnit.AppearanceCell.Options.UseTextOptions = True
        Me.FTUsedUnit.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTUsedUnit.AppearanceHeader.Options.UseTextOptions = True
        Me.FTUsedUnit.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTUsedUnit.Caption = "FTUsedUnit"
        Me.FTUsedUnit.FieldName = "FTUsedUnit"
        Me.FTUsedUnit.Name = "FTUsedUnit"
        Me.FTUsedUnit.OptionsColumn.AllowEdit = False
        Me.FTUsedUnit.OptionsColumn.ReadOnly = True
        Me.FTUsedUnit.Visible = True
        Me.FTUsedUnit.VisibleIndex = 49
        '
        'FNPOCFMQuantity
        '
        Me.FNPOCFMQuantity.Caption = "Purchase CFM Qty."
        Me.FNPOCFMQuantity.ColumnEdit = Me.RepositoryItemCalcQty
        Me.FNPOCFMQuantity.DisplayFormat.FormatString = "{0:n4}"
        Me.FNPOCFMQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNPOCFMQuantity.FieldName = "FNPOCFMQuantity"
        Me.FNPOCFMQuantity.Name = "FNPOCFMQuantity"
        Me.FNPOCFMQuantity.OptionsColumn.AllowEdit = False
        Me.FNPOCFMQuantity.OptionsColumn.ReadOnly = True
        Me.FNPOCFMQuantity.Visible = True
        Me.FNPOCFMQuantity.VisibleIndex = 50
        '
        'RepositoryItemCalcQty
        '
        Me.RepositoryItemCalcQty.AutoHeight = False
        Me.RepositoryItemCalcQty.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemCalcQty.DisplayFormat.FormatString = "{0:n4}"
        Me.RepositoryItemCalcQty.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.RepositoryItemCalcQty.EditFormat.FormatString = "{0:n4}"
        Me.RepositoryItemCalcQty.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.RepositoryItemCalcQty.Name = "RepositoryItemCalcQty"
        '
        'xFTPOCFMNote
        '
        Me.xFTPOCFMNote.Caption = "Purchase CFM Note"
        Me.xFTPOCFMNote.ColumnEdit = Me.RepositoryItemMemoExFTNote
        Me.xFTPOCFMNote.DisplayFormat.FormatString = "{0:n4}"
        Me.xFTPOCFMNote.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.xFTPOCFMNote.FieldName = "FTPOCFMNote"
        Me.xFTPOCFMNote.Name = "xFTPOCFMNote"
        Me.xFTPOCFMNote.OptionsColumn.AllowEdit = False
        Me.xFTPOCFMNote.OptionsColumn.ReadOnly = True
        Me.xFTPOCFMNote.Visible = True
        Me.xFTPOCFMNote.VisibleIndex = 51
        '
        'RepositoryItemMemoExFTNote
        '
        Me.RepositoryItemMemoExFTNote.AutoHeight = False
        Me.RepositoryItemMemoExFTNote.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemMemoExFTNote.MaxLength = 500
        Me.RepositoryItemMemoExFTNote.Name = "RepositoryItemMemoExFTNote"
        Me.RepositoryItemMemoExFTNote.ShowIcon = False
        '
        'xFNPROCFMQuantity
        '
        Me.xFNPROCFMQuantity.Caption = "Production CFM Qty."
        Me.xFNPROCFMQuantity.ColumnEdit = Me.RepositoryItemCalcQty
        Me.xFNPROCFMQuantity.FieldName = "FNPROCFMQuantity"
        Me.xFNPROCFMQuantity.Name = "xFNPROCFMQuantity"
        Me.xFNPROCFMQuantity.OptionsColumn.AllowEdit = False
        Me.xFNPROCFMQuantity.OptionsColumn.ReadOnly = True
        Me.xFNPROCFMQuantity.Visible = True
        Me.xFNPROCFMQuantity.VisibleIndex = 52
        '
        'xFTPROCFMNote
        '
        Me.xFTPROCFMNote.Caption = "Production CFM Note"
        Me.xFTPROCFMNote.ColumnEdit = Me.RepositoryItemMemoExFTNote
        Me.xFTPROCFMNote.FieldName = "FTPROCFMNote"
        Me.xFTPROCFMNote.Name = "xFTPROCFMNote"
        Me.xFTPROCFMNote.OptionsColumn.AllowEdit = False
        Me.xFTPROCFMNote.OptionsColumn.ReadOnly = True
        Me.xFTPROCFMNote.Visible = True
        Me.xFTPROCFMNote.VisibleIndex = 53
        '
        'xFNPOBALQuantity
        '
        Me.xFNPOBALQuantity.Caption = "PO BAL Quantity"
        Me.xFNPOBALQuantity.DisplayFormat.FormatString = "{0:n4}"
        Me.xFNPOBALQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.xFNPOBALQuantity.FieldName = "FNPOBALQuantity"
        Me.xFNPOBALQuantity.Name = "xFNPOBALQuantity"
        Me.xFNPOBALQuantity.OptionsColumn.AllowEdit = False
        Me.xFNPOBALQuantity.OptionsColumn.AllowFocus = False
        Me.xFNPOBALQuantity.OptionsColumn.ReadOnly = True
        Me.xFNPOBALQuantity.Visible = True
        Me.xFNPOBALQuantity.VisibleIndex = 54
        '
        'FTUnitCode
        '
        Me.FTUnitCode.AppearanceCell.Options.UseTextOptions = True
        Me.FTUnitCode.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTUnitCode.AppearanceHeader.Options.UseTextOptions = True
        Me.FTUnitCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTUnitCode.Caption = "PO Unit"
        Me.FTUnitCode.FieldName = "FTUnitCode"
        Me.FTUnitCode.Name = "FTUnitCode"
        Me.FTUnitCode.OptionsColumn.AllowEdit = False
        Me.FTUnitCode.OptionsColumn.ReadOnly = True
        Me.FTUnitCode.Visible = True
        Me.FTUnitCode.VisibleIndex = 55
        Me.FTUnitCode.Width = 70
        '
        'xFTDelayReasonsCode
        '
        Me.xFTDelayReasonsCode.Caption = "Delay Reasons"
        Me.xFTDelayReasonsCode.ColumnEdit = Me.RepositoryItemGridLookUpEditFTDelayReasonsCode
        Me.xFTDelayReasonsCode.FieldName = "FTDelayReasonsCode"
        Me.xFTDelayReasonsCode.Name = "xFTDelayReasonsCode"
        Me.xFTDelayReasonsCode.OptionsColumn.AllowEdit = False
        Me.xFTDelayReasonsCode.OptionsColumn.ReadOnly = True
        Me.xFTDelayReasonsCode.Visible = True
        Me.xFTDelayReasonsCode.VisibleIndex = 56
        '
        'RepositoryItemGridLookUpEditFTDelayReasonsCode
        '
        Me.RepositoryItemGridLookUpEditFTDelayReasonsCode.AutoHeight = False
        Me.RepositoryItemGridLookUpEditFTDelayReasonsCode.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemGridLookUpEditFTDelayReasonsCode.DisplayMember = "FTDelayReasonsCode"
        Me.RepositoryItemGridLookUpEditFTDelayReasonsCode.Name = "RepositoryItemGridLookUpEditFTDelayReasonsCode"
        Me.RepositoryItemGridLookUpEditFTDelayReasonsCode.NullText = ""
        Me.RepositoryItemGridLookUpEditFTDelayReasonsCode.PopupView = Me.RepositoryItemGridLookUpEdit1View
        Me.RepositoryItemGridLookUpEditFTDelayReasonsCode.ValueMember = "FTDelayReasonsCode"
        '
        'RepositoryItemGridLookUpEdit1View
        '
        Me.RepositoryItemGridLookUpEdit1View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.xxxFTDelayReasonsCode, Me.xxxFTDelayReasonsNameEN})
        Me.RepositoryItemGridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.RepositoryItemGridLookUpEdit1View.Name = "RepositoryItemGridLookUpEdit1View"
        Me.RepositoryItemGridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.RepositoryItemGridLookUpEdit1View.OptionsView.ColumnAutoWidth = False
        Me.RepositoryItemGridLookUpEdit1View.OptionsView.ShowAutoFilterRow = True
        Me.RepositoryItemGridLookUpEdit1View.OptionsView.ShowGroupPanel = False
        '
        'xxxFTDelayReasonsCode
        '
        Me.xxxFTDelayReasonsCode.Caption = "Delay Reasons Code"
        Me.xxxFTDelayReasonsCode.FieldName = "FTDelayReasonsCode"
        Me.xxxFTDelayReasonsCode.Name = "xxxFTDelayReasonsCode"
        Me.xxxFTDelayReasonsCode.OptionsColumn.AllowEdit = False
        Me.xxxFTDelayReasonsCode.OptionsColumn.ReadOnly = True
        Me.xxxFTDelayReasonsCode.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        Me.xxxFTDelayReasonsCode.Visible = True
        Me.xxxFTDelayReasonsCode.VisibleIndex = 0
        Me.xxxFTDelayReasonsCode.Width = 100
        '
        'xxxFTDelayReasonsNameEN
        '
        Me.xxxFTDelayReasonsNameEN.Caption = "Delay Reasons Name"
        Me.xxxFTDelayReasonsNameEN.FieldName = "FTDelayReasonsName"
        Me.xxxFTDelayReasonsNameEN.Name = "xxxFTDelayReasonsNameEN"
        Me.xxxFTDelayReasonsNameEN.OptionsColumn.AllowEdit = False
        Me.xxxFTDelayReasonsNameEN.OptionsColumn.ReadOnly = True
        Me.xxxFTDelayReasonsNameEN.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        Me.xxxFTDelayReasonsNameEN.Visible = True
        Me.xxxFTDelayReasonsNameEN.VisibleIndex = 1
        Me.xxxFTDelayReasonsNameEN.Width = 200
        '
        'FTDelayReasonsName
        '
        Me.FTDelayReasonsName.Caption = "Delay Reasons Name"
        Me.FTDelayReasonsName.FieldName = "FTDelayReasonsName"
        Me.FTDelayReasonsName.Name = "FTDelayReasonsName"
        Me.FTDelayReasonsName.OptionsColumn.AllowEdit = False
        Me.FTDelayReasonsName.OptionsColumn.ReadOnly = True
        Me.FTDelayReasonsName.Visible = True
        Me.FTDelayReasonsName.VisibleIndex = 57
        '
        'FTFurtherDelayReasonCode
        '
        Me.FTFurtherDelayReasonCode.Caption = "Further Delay Reason Code"
        Me.FTFurtherDelayReasonCode.ColumnEdit = Me.RepositoryItemGridLookUpEditFTFurtherDelayReasonCode
        Me.FTFurtherDelayReasonCode.FieldName = "FTFurtherDelayReasonCode"
        Me.FTFurtherDelayReasonCode.Name = "FTFurtherDelayReasonCode"
        Me.FTFurtherDelayReasonCode.OptionsColumn.AllowEdit = False
        Me.FTFurtherDelayReasonCode.OptionsColumn.ReadOnly = True
        Me.FTFurtherDelayReasonCode.Visible = True
        Me.FTFurtherDelayReasonCode.VisibleIndex = 58
        '
        'RepositoryItemGridLookUpEditFTFurtherDelayReasonCode
        '
        Me.RepositoryItemGridLookUpEditFTFurtherDelayReasonCode.AutoHeight = False
        Me.RepositoryItemGridLookUpEditFTFurtherDelayReasonCode.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemGridLookUpEditFTFurtherDelayReasonCode.DisplayMember = "FTFurtherDelayReasonCode"
        Me.RepositoryItemGridLookUpEditFTFurtherDelayReasonCode.Name = "RepositoryItemGridLookUpEditFTFurtherDelayReasonCode"
        Me.RepositoryItemGridLookUpEditFTFurtherDelayReasonCode.NullText = ""
        Me.RepositoryItemGridLookUpEditFTFurtherDelayReasonCode.PopupView = Me.GridView1
        Me.RepositoryItemGridLookUpEditFTFurtherDelayReasonCode.ValueMember = "FTFurtherDelayReasonCode"
        '
        'GridView1
        '
        Me.GridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.xFTFurtherDelayReasonCode, Me.xFTFurtherDelayReasonNameEN})
        Me.GridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView1.OptionsView.ColumnAutoWidth = False
        Me.GridView1.OptionsView.ShowAutoFilterRow = True
        Me.GridView1.OptionsView.ShowGroupPanel = False
        '
        'xFTFurtherDelayReasonCode
        '
        Me.xFTFurtherDelayReasonCode.Caption = "Further Delay Reason Code"
        Me.xFTFurtherDelayReasonCode.FieldName = "FTFurtherDelayReasonCode"
        Me.xFTFurtherDelayReasonCode.Name = "xFTFurtherDelayReasonCode"
        Me.xFTFurtherDelayReasonCode.OptionsColumn.AllowEdit = False
        Me.xFTFurtherDelayReasonCode.OptionsColumn.ReadOnly = True
        Me.xFTFurtherDelayReasonCode.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        Me.xFTFurtherDelayReasonCode.Visible = True
        Me.xFTFurtherDelayReasonCode.VisibleIndex = 0
        Me.xFTFurtherDelayReasonCode.Width = 100
        '
        'xFTFurtherDelayReasonNameEN
        '
        Me.xFTFurtherDelayReasonNameEN.Caption = "Further Delay Reason Name"
        Me.xFTFurtherDelayReasonNameEN.FieldName = "FTFurtherDelayReasonName"
        Me.xFTFurtherDelayReasonNameEN.Name = "xFTFurtherDelayReasonNameEN"
        Me.xFTFurtherDelayReasonNameEN.OptionsColumn.AllowEdit = False
        Me.xFTFurtherDelayReasonNameEN.OptionsColumn.ReadOnly = True
        Me.xFTFurtherDelayReasonNameEN.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        Me.xFTFurtherDelayReasonNameEN.Visible = True
        Me.xFTFurtherDelayReasonNameEN.VisibleIndex = 1
        Me.xFTFurtherDelayReasonNameEN.Width = 200
        '
        'FTFurtherDelayReasonName
        '
        Me.FTFurtherDelayReasonName.Caption = "Further Delay Reason Name"
        Me.FTFurtherDelayReasonName.FieldName = "FTFurtherDelayReasonName"
        Me.FTFurtherDelayReasonName.Name = "FTFurtherDelayReasonName"
        Me.FTFurtherDelayReasonName.OptionsColumn.AllowEdit = False
        Me.FTFurtherDelayReasonName.OptionsColumn.ReadOnly = True
        Me.FTFurtherDelayReasonName.Visible = True
        Me.FTFurtherDelayReasonName.VisibleIndex = 59
        '
        'FNTrackSeq
        '
        Me.FNTrackSeq.Caption = "Track No."
        Me.FNTrackSeq.FieldName = "FNTrackSeq"
        Me.FNTrackSeq.Name = "FNTrackSeq"
        Me.FNTrackSeq.OptionsColumn.AllowEdit = False
        Me.FNTrackSeq.OptionsColumn.ReadOnly = True
        Me.FNTrackSeq.Visible = True
        Me.FNTrackSeq.VisibleIndex = 60
        '
        'FTTrackDate
        '
        Me.FTTrackDate.AppearanceCell.Options.UseTextOptions = True
        Me.FTTrackDate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTTrackDate.Caption = "Track Date"
        Me.FTTrackDate.FieldName = "FTTrackDate"
        Me.FTTrackDate.Name = "FTTrackDate"
        Me.FTTrackDate.OptionsColumn.AllowEdit = False
        Me.FTTrackDate.OptionsColumn.ReadOnly = True
        Me.FTTrackDate.Visible = True
        Me.FTTrackDate.VisibleIndex = 61
        '
        'FTTrackBy
        '
        Me.FTTrackBy.AppearanceCell.Options.UseTextOptions = True
        Me.FTTrackBy.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTTrackBy.Caption = "Track By"
        Me.FTTrackBy.FieldName = "FTTrackBy"
        Me.FTTrackBy.Name = "FTTrackBy"
        Me.FTTrackBy.OptionsColumn.AllowEdit = False
        Me.FTTrackBy.OptionsColumn.ReadOnly = True
        Me.FTTrackBy.Visible = True
        Me.FTTrackBy.VisibleIndex = 62
        '
        'FTContactName
        '
        Me.FTContactName.Caption = "FTContactName"
        Me.FTContactName.FieldName = "Contact Name"
        Me.FTContactName.Name = "FTContactName"
        Me.FTContactName.OptionsColumn.AllowEdit = False
        Me.FTContactName.OptionsColumn.ReadOnly = True
        Me.FTContactName.Visible = True
        Me.FTContactName.VisibleIndex = 63
        '
        'xFTNote
        '
        Me.xFTNote.Caption = "Track Note"
        Me.xFTNote.ColumnEdit = Me.RepositoryItemPopupContainerTrackNote
        Me.xFTNote.FieldName = "FTTrackNote"
        Me.xFTNote.Name = "xFTNote"
        Me.xFTNote.OptionsColumn.AllowEdit = False
        Me.xFTNote.OptionsColumn.ReadOnly = True
        Me.xFTNote.Visible = True
        Me.xFTNote.VisibleIndex = 64
        Me.xFTNote.Width = 200
        '
        'RepositoryItemPopupContainerTrackNote
        '
        Me.RepositoryItemPopupContainerTrackNote.AutoHeight = False
        Me.RepositoryItemPopupContainerTrackNote.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerTrackNote.Name = "RepositoryItemPopupContainerTrackNote"
        Me.RepositoryItemPopupContainerTrackNote.PopupControl = Me.PopupTrackNote
        '
        'xFNHSysRawMatId
        '
        Me.xFNHSysRawMatId.Caption = "FNHSysRawMatId"
        Me.xFNHSysRawMatId.FieldName = "FNHSysRawMatId"
        Me.xFNHSysRawMatId.Name = "xFNHSysRawMatId"
        '
        'xFNDocType
        '
        Me.xFNDocType.Caption = "FNDocType"
        Me.xFNDocType.FieldName = "FNDocType"
        Me.xFNDocType.Name = "xFNDocType"
        '
        'xFTInvoiceNo
        '
        Me.xFTInvoiceNo.Caption = "Invoice No"
        Me.xFTInvoiceNo.ColumnEdit = Me.RepositoryItemTextFTInvoiceNo
        Me.xFTInvoiceNo.FieldName = "FTInvoiceNo"
        Me.xFTInvoiceNo.Name = "xFTInvoiceNo"
        Me.xFTInvoiceNo.OptionsColumn.AllowEdit = False
        Me.xFTInvoiceNo.OptionsColumn.ReadOnly = True
        Me.xFTInvoiceNo.Visible = True
        Me.xFTInvoiceNo.VisibleIndex = 65
        '
        'RepositoryItemTextFTInvoiceNo
        '
        Me.RepositoryItemTextFTInvoiceNo.AutoHeight = False
        Me.RepositoryItemTextFTInvoiceNo.MaxLength = 100
        Me.RepositoryItemTextFTInvoiceNo.Name = "RepositoryItemTextFTInvoiceNo"
        '
        'xFTInvoiceNote
        '
        Me.xFTInvoiceNote.Caption = "Invoice Note"
        Me.xFTInvoiceNote.ColumnEdit = Me.RepositoryItemMemoExFTNote
        Me.xFTInvoiceNote.FieldName = "FTInvoiceNote"
        Me.xFTInvoiceNote.Name = "xFTInvoiceNote"
        Me.xFTInvoiceNote.OptionsColumn.AllowEdit = False
        Me.xFTInvoiceNote.OptionsColumn.ReadOnly = True
        Me.xFTInvoiceNote.Visible = True
        Me.xFTInvoiceNote.VisibleIndex = 67
        '
        'xFTReserveNo
        '
        Me.xFTReserveNo.Caption = "Reserve No"
        Me.xFTReserveNo.FieldName = "FTReserveNo"
        Me.xFTReserveNo.Name = "xFTReserveNo"
        Me.xFTReserveNo.OptionsColumn.AllowEdit = False
        Me.xFTReserveNo.OptionsColumn.ReadOnly = True
        Me.xFTReserveNo.Visible = True
        Me.xFTReserveNo.VisibleIndex = 66
        '
        'xFTSendSMPDate
        '
        Me.xFTSendSMPDate.Caption = "Send SMP Date"
        Me.xFTSendSMPDate.ColumnEdit = Me.RepositoryItemDateEdit1
        Me.xFTSendSMPDate.FieldName = "FTSendSMPDate"
        Me.xFTSendSMPDate.Name = "xFTSendSMPDate"
        Me.xFTSendSMPDate.OptionsColumn.AllowEdit = False
        Me.xFTSendSMPDate.OptionsColumn.ReadOnly = True
        Me.xFTSendSMPDate.Visible = True
        Me.xFTSendSMPDate.VisibleIndex = 68
        '
        'xFTSendSMPStatus
        '
        Me.xFTSendSMPStatus.Caption = "Send SMP Status"
        Me.xFTSendSMPStatus.ColumnEdit = Me.RepositoryItemTextEditFTSendSMPStatus
        Me.xFTSendSMPStatus.FieldName = "FTSendSMPStatus"
        Me.xFTSendSMPStatus.Name = "xFTSendSMPStatus"
        Me.xFTSendSMPStatus.OptionsColumn.AllowEdit = False
        Me.xFTSendSMPStatus.OptionsColumn.ReadOnly = True
        Me.xFTSendSMPStatus.Visible = True
        Me.xFTSendSMPStatus.VisibleIndex = 69
        '
        'RepositoryItemTextEditFTSendSMPStatus
        '
        Me.RepositoryItemTextEditFTSendSMPStatus.AutoHeight = False
        Me.RepositoryItemTextEditFTSendSMPStatus.MaxLength = 200
        Me.RepositoryItemTextEditFTSendSMPStatus.Name = "RepositoryItemTextEditFTSendSMPStatus"
        '
        'xFTSendSMPRemark
        '
        Me.xFTSendSMPRemark.Caption = "Send SMP Remark"
        Me.xFTSendSMPRemark.ColumnEdit = Me.RepositoryItemMemoExFTNote
        Me.xFTSendSMPRemark.FieldName = "FTSendSMPRemark"
        Me.xFTSendSMPRemark.Name = "xFTSendSMPRemark"
        Me.xFTSendSMPRemark.OptionsColumn.AllowEdit = False
        Me.xFTSendSMPRemark.OptionsColumn.ReadOnly = True
        Me.xFTSendSMPRemark.Visible = True
        Me.xFTSendSMPRemark.VisibleIndex = 70
        '
        'xFTSendSMPAWB
        '
        Me.xFTSendSMPAWB.Caption = "AWB"
        Me.xFTSendSMPAWB.ColumnEdit = Me.RepositoryItemTextEditFTSendSMPStatus
        Me.xFTSendSMPAWB.FieldName = "FTSendSMPAWB"
        Me.xFTSendSMPAWB.Name = "xFTSendSMPAWB"
        Me.xFTSendSMPAWB.OptionsColumn.AllowEdit = False
        Me.xFTSendSMPAWB.OptionsColumn.ReadOnly = True
        Me.xFTSendSMPAWB.Visible = True
        Me.xFTSendSMPAWB.VisibleIndex = 71
        '
        'xFTSendSMPPayType
        '
        Me.xFTSendSMPPayType.Caption = "Pay Type"
        Me.xFTSendSMPPayType.ColumnEdit = Me.RepositoryItemTextEditFTSendSMPStatus
        Me.xFTSendSMPPayType.FieldName = "FTSendSMPPayType"
        Me.xFTSendSMPPayType.Name = "xFTSendSMPPayType"
        Me.xFTSendSMPPayType.OptionsColumn.AllowEdit = False
        Me.xFTSendSMPPayType.OptionsColumn.ReadOnly = True
        Me.xFTSendSMPPayType.Visible = True
        Me.xFTSendSMPPayType.VisibleIndex = 72
        '
        'xFTSamplePPC
        '
        Me.xFTSamplePPC.Caption = "Sample PPC"
        Me.xFTSamplePPC.FieldName = "FTSamplePPC"
        Me.xFTSamplePPC.Name = "xFTSamplePPC"
        Me.xFTSamplePPC.OptionsColumn.AllowEdit = False
        Me.xFTSamplePPC.OptionsColumn.ReadOnly = True
        Me.xFTSamplePPC.Visible = True
        Me.xFTSamplePPC.VisibleIndex = 73
        Me.xFTSamplePPC.Width = 200
        '
        'FTStatePDF
        '
        Me.FTStatePDF.Caption = "FTStatePDF"
        Me.FTStatePDF.FieldName = "FTStatePDF"
        Me.FTStatePDF.Name = "FTStatePDF"
        Me.FTStatePDF.OptionsColumn.AllowEdit = False
        Me.FTStatePDF.OptionsColumn.ReadOnly = True
        '
        'FTStatePDFWaitPrice
        '
        Me.FTStatePDFWaitPrice.Caption = "FTStatePDFWaitPrice"
        Me.FTStatePDFWaitPrice.FieldName = "FTStatePDFWaitPrice"
        Me.FTStatePDFWaitPrice.Name = "FTStatePDFWaitPrice"
        Me.FTStatePDFWaitPrice.OptionsColumn.AllowEdit = False
        Me.FTStatePDFWaitPrice.OptionsColumn.ReadOnly = True
        '
        'xFTStateCancel
        '
        Me.xFTStateCancel.AppearanceCell.Options.UseTextOptions = True
        Me.xFTStateCancel.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.xFTStateCancel.Caption = "Cancelled"
        Me.xFTStateCancel.ColumnEdit = Me.RepCheckEdit
        Me.xFTStateCancel.FieldName = "FTStateCancel"
        Me.xFTStateCancel.MinWidth = 22
        Me.xFTStateCancel.Name = "xFTStateCancel"
        Me.xFTStateCancel.OptionsColumn.AllowEdit = False
        Me.xFTStateCancel.OptionsColumn.ReadOnly = True
        Me.xFTStateCancel.Visible = True
        Me.xFTStateCancel.VisibleIndex = 74
        Me.xFTStateCancel.Width = 83
        '
        'xFTStateHold
        '
        Me.xFTStateHold.AppearanceCell.Options.UseTextOptions = True
        Me.xFTStateHold.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.xFTStateHold.Caption = "Hold PO"
        Me.xFTStateHold.ColumnEdit = Me.RepCheckEdit
        Me.xFTStateHold.FieldName = "FTStateHold"
        Me.xFTStateHold.MinWidth = 22
        Me.xFTStateHold.Name = "xFTStateHold"
        Me.xFTStateHold.OptionsColumn.AllowEdit = False
        Me.xFTStateHold.OptionsColumn.ReadOnly = True
        Me.xFTStateHold.Visible = True
        Me.xFTStateHold.VisibleIndex = 75
        Me.xFTStateHold.Width = 83
        '
        'xFTHoldReason
        '
        Me.xFTHoldReason.AppearanceCell.Options.UseTextOptions = True
        Me.xFTHoldReason.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.xFTHoldReason.Caption = "Hold Reason"
        Me.xFTHoldReason.FieldName = "FTHoldReason"
        Me.xFTHoldReason.MinWidth = 22
        Me.xFTHoldReason.Name = "xFTHoldReason"
        Me.xFTHoldReason.OptionsColumn.AllowEdit = False
        Me.xFTHoldReason.OptionsColumn.ReadOnly = True
        Me.xFTHoldReason.Visible = True
        Me.xFTHoldReason.VisibleIndex = 76
        Me.xFTHoldReason.Width = 250
        '
        'FNDocType
        '
        Me.FNDocType.Caption = "FNDocType"
        Me.FNDocType.FieldName = "FNDocType"
        Me.FNDocType.Name = "FNDocType"
        Me.FNDocType.OptionsColumn.AllowEdit = False
        '
        'RepositoryItemMemoExEditTrackNote
        '
        Me.RepositoryItemMemoExEditTrackNote.AutoHeight = False
        Me.RepositoryItemMemoExEditTrackNote.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemMemoExEditTrackNote.Name = "RepositoryItemMemoExEditTrackNote"
        Me.RepositoryItemMemoExEditTrackNote.ReadOnly = True
        Me.RepositoryItemMemoExEditTrackNote.ShowIcon = False
        '
        'oDockManager
        '
        Me.oDockManager.Form = Me
        Me.oDockManager.RootPanels.AddRange(New DevExpress.XtraBars.Docking.DockPanel() {Me.ogbheader})
        Me.oDockManager.TopZIndexControls.AddRange(New String() {"DevExpress.XtraBars.BarDockControl", "DevExpress.XtraBars.StandaloneBarDockControl", "System.Windows.Forms.StatusBar", "DevExpress.XtraBars.Ribbon.RibbonStatusBar", "DevExpress.XtraBars.Ribbon.RibbonControl"})
        '
        'wPurchaseMaterialTrackingSample
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1318, 654)
        Me.Controls.Add(Me.ogbdetail)
        Me.Controls.Add(Me.ogbheader)
        Me.Name = "wPurchaseMaterialTrackingSample"
        Me.Text = "Purchase Tracking Material Delay (SampleRoom)"
        Me.ogbheader.ResumeLayout(False)
        Me.DockPanel1_Container.ResumeLayout(False)
        CType(Me.FTEndDevCFMDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTEndDevCFMDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTStartDevCFMDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTStartDevCFMDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTEndSRConfirmDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTEndSRConfirmDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTEndOrderDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTEndOrderDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTStartSRConfirmDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTStartSRConfirmDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTStartOrderDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTStartOrderDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysBuyId_None.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysBuyId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNListDocumentTrackPIData.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysSuplId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysSuplId_None.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTEndPurchaseDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTEndPurchaseDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTEndDelivery.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTEndDelivery.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTStartPurchaseDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTStartPurchaseDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTStartDelivery.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTStartDelivery.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNDataType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ochkselectall.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogbdetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbdetail.ResumeLayout(False)
        CType(Me.PopupTrackNote, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PopupTrackNote.ResumeLayout(False)
        CType(Me.FTTRNote.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbmainprocbutton.ResumeLayout(False)
        CType(Me.ogdtime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvtime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepCheckEdit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCalcEditFNLeadTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemDateEdit1.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemDateEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCalcQty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemMemoExFTNote, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemGridLookUpEditFTDelayReasonsCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemGridLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemGridLookUpEditFTFurtherDelayReasonCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerTrackNote, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemTextFTInvoiceNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemTextEditFTSendSMPStatus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemMemoExEditTrackNote, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents CFTPurchaseNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTUnitCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNPOQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTEndDelivery As DevExpress.XtraEditors.DateEdit
    Friend WithEvents FTEndDelivery_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTStartDelivery As DevExpress.XtraEditors.DateEdit
    Friend WithEvents FTStartDelivery_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FDDeliveryDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FDPurchaseDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTSuplCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTSuplName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepCheckEdit As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents FTEndPurchaseDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents FTEndPurchaseDate_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTStartPurchaseDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents FTStartPurchaseDate_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysSuplId As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FNHSysSuplId_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysSuplId_None As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FTPurchaseBy As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNHSysDeliveryID As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cxFTPOCustPO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents xFTPOOrderNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents xFTPOStyleCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTSelect As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ocmsendmail As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmsavetrack As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FNListDocumentTrackPIData As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents FNListDocumentTrackPIData_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents xFTORGOETCDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTORGOETCInputBy As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTORGOETCInputDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTORGOETCInputTime As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTFinalOETCDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTFinalOETCInputBy As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTFinalOETCInputDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTFinalOETCInputTime As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents xFTOETCDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTContactName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemDateEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemDateEdit
    Friend WithEvents ocmFTFinalOETCDate As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmFTORGOETCDate As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents RepositoryItemMemoExFTNote As DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit
    Friend WithEvents RepositoryItemTextFTInvoiceNo As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents ochkselectall As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents FTStateSendMail As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTSendMailBy As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTSendMailDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTSendMailTime As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CXFTCmpCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTBuyCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents xFTCurCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNHSysBuyId_None As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FNHSysBuyId_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysBuyId As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents xFTNikeVenderCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents xFTCOFOCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTRawMatCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents xFTRawMatColorCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents xFTRawMatSizeCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents xFTNote As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents xxFNLeadTime As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents xFNORGLeadTime As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNFinalLeadTime As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTDelayLangth As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNPOCFMQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemCalcQty As DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit
    Friend WithEvents xFTPOCFMNote As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents xFNPROCFMQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents xFTPROCFMNote As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents xFNPOBALQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents xFTDelayReasonsCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemGridLookUpEditFTDelayReasonsCode As DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit
    Friend WithEvents RepositoryItemGridLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents xxxFTDelayReasonsCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents xxxFTDelayReasonsNameEN As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTDelayReasonsName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTFurtherDelayReasonCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemGridLookUpEditFTFurtherDelayReasonCode As DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents xFTFurtherDelayReasonCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents xFTFurtherDelayReasonNameEN As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTFurtherDelayReasonName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNTrackSeq As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTTrackDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTTrackBy As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents xFNHSysRawMatId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents xFTFirstInputDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTFirstInputBy As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTLastInputDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTLastInputBy As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ocmproduction As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmpurchase As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmDelayReasons As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmFurtherDelayReason As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents RepositoryItemCalcEditFNLeadTime As DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit
    Friend WithEvents xFNDocType As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents xFTReserveNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemMemoExEditTrackNote As DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit
    Friend WithEvents xFTInvoiceNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents xFTInvoiceNote As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents xFTPINo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents xFTSendSMPDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents xFTSendSMPStatus As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemTextEditFTSendSMPStatus As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents xFTSendSMPRemark As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents xFTSendSMPAWB As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents xFTSendSMPPayType As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNDataType_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNDataType As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents PopupTrackNote As DevExpress.XtraEditors.PopupContainerControl
    Friend WithEvents FTTRNote As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents RepositoryItemPopupContainerTrackNote As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents xFTSamplePPC As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents xFTMainMatName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTEndOrderDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents FTEndOrderDate_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTStartOrderDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents FTStartOrderDate_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents xFTLastSendMailBy As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents xFTLastSendMailDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents xFTLastSendMailTime As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTStateSuperVisorApp As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTStateManagerApp As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTStatePDF As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTStatePDFWaitPrice As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents xFTStateCancel As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents xFTStateHold As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents xFTHoldReason As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ocmholdpurchase As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FTEndSRConfirmDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents FTEndSRConfirmDate_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTStartSRConfirmDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents FTStartDevConfirmDate_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FDCFMDeliveryDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTDevConfirmDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTPartDetail As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTEndDevCFMDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents FTEndDEvCFMDate_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTStartDevCFMDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents FTStartDevCfmDate_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTOrderDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNUsedQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FDSupCFMDelDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTUsedUnit As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTSuplCalCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTPartCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNDocType As DevExpress.XtraGrid.Columns.GridColumn
End Class

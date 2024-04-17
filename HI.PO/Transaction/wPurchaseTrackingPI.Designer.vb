<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wPurchaseTrackingPI
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
        Dim GridLevelNode1 As DevExpress.XtraGrid.GridLevelNode = New DevExpress.XtraGrid.GridLevelNode()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(wPurchaseTrackingPI))
        Dim EditorButtonImageOptions1 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject4 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.ogvlist = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.sssFTStateNewVenderUpload = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepCheckEdit = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.ssFTPINo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ssFTPIDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ssFTRcvPIDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.sssFTPISuplCFMDeliveryDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNPIPOQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ssFTStatePaid = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.sasFTPaidDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ssFTPaidNote = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ssFTLCNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ssFTStateFile = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ssFNFileID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ssPDF = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemPopupContainerPDFPI = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.PopupPDFPI = New DevExpress.XtraEditors.PopupContainerControl()
        Me.FilePdfViewerPI = New DevExpress.XtraPdfViewer.PdfViewer()
        Me.ssFTStatePIPDFFile = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.sssssPIPDF = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.sFTPurchaseNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ogdtime = New DevExpress.XtraGrid.GridControl()
        Me.ogvtime = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.FTStateNewVenderUpload = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTSelect = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFTPurchaseNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FDPurchaseDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTPurchaseBy = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTSuplCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTSuplName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.xFTCurCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FDDeliveryDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cxFTPOCustPO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.xFTPOOrderNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.xFTPOStyleCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNHSysDeliveryID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CXFTCmpCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTBuyCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNOrderQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTUnitCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNPOQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cxFNPOGrandAmt = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNRCVQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.xFNPOOutBalQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTStateSendApp = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTSendAppDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNRevisedSeq = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTRevisedDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTPurchaseRevisedBy = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTStateSuperVisorApp = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTStateSuperVisorReject = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTSuperVisorAppDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTStateManagerApp = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTStateManagerReject = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTSuperManagerAppDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTStateSendMail = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTSendMailBy = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTSendMailDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTSendMailTime = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTStateClosed = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTClosedBy = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTClosedDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTClosedTime = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTStateFinishPO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTSendFinishPOBy = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTSendFinishPODate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTSendFinishPOTime = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTStateSendPIToAcc = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTSendPIToAccBy = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTSendPIToAccDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTSendPIToAccTime = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTPINoBy = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTPINoDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTPINoTime = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTPINo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTPIDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTRcvPIDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTPISuplCFMDeliveryDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNPIQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNPINetAmt = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.xFNPIGrandNetAmt = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.zFNCNAmt = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNDNAmt = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.zFNSurchargeAmt = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.zFNPIGrandTotalAmt = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTPIRemark = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNPIPayType = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.xFTPIPayDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.xmFTPIPayTypeRemark = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNLeadTime = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTBaseOnLeadTimeDeliveryDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNFNExtendLT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTFacCheckCFMDeliveryDate1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemDateEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemDateEdit()
        Me.FTFacCheckCFMDeliveryDate1By = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTFacCheckCFMDeliveryDate1Date = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTFacCheckCFMDeliveryDate1Time = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTFacCheckCFMDeliveryDate2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTFacCheckCFMDeliveryDate2By = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTFacCheckCFMDeliveryDate2Date = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTFacCheckCFMDeliveryDate2Time = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTFacCheckCFMDeliveryDateFinal = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTFacCheckCFMDeliveryDateFinalBy = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTFacCheckCFMDeliveryDateFinalDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTFacCheckCFMDeliveryDateFinalTime = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTWarehouseDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTWarehouseDateBy = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTWarehouseDateDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTWarehouseDateTime = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTImpactedGacDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTImpactedGacDatey = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTImpactedGacDateDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTImpactedGacDateTime = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTBaseOnLeadTimeDeliveryDateBy = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTBaseOnLeadTimeDeliveryDateDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTBaseOnLeadTimeDeliveryDateTime = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTNote = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemMemoExFTNote = New DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit()
        Me.FTNoteBy = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTNoteDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTNoteTime = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTInvoiceNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemTextFTInvoiceNo = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.FTInvoiceNoBy = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTInvoiceNoDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTInvoiceNoTime = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTETD = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTETDBy = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTETDDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTETDTime = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTETA = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTETABy = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTETADate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTETATime = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTPIPayTypeBy = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTPIPayTypeDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTPIPayTypeTime = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.xFTStatePaid = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTPaidDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTLCNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTPaidNote = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTStateFile = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.sPDF = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemPopupContainerPDF = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.sFNFileID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTStatePaidBy = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTStatePaidDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTStatePaidTime = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNTrackSeq = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTTrackDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTTrackBy = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTContactName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTTrackNote = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.xFNHSysSuplId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cxFNPoState = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cxFNPIPOQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cxFNPIPONetAmt = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.zFNPOBalQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.zFNPOBalGrandAmt = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTStatePIPDFFile = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.SSPIPDF = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.PopupPDF = New DevExpress.XtraEditors.PopupContainerControl()
        Me.FilePdfViewer = New DevExpress.XtraPdfViewer.PdfViewer()
        Me.ogbheader = New DevExpress.XtraBars.Docking.DockPanel()
        Me.DockPanel1_Container = New DevExpress.XtraBars.Docking.ControlContainer()
        Me.chknewupload = New DevExpress.XtraEditors.CheckEdit()
        Me.FTEndPayment = New DevExpress.XtraEditors.DateEdit()
        Me.EndPayment_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.ochkselectall = New DevExpress.XtraEditors.CheckEdit()
        Me.FTStartPayment = New DevExpress.XtraEditors.DateEdit()
        Me.FNListDocumentTrackPIData_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.StartPayment_lbl = New DevExpress.XtraEditors.LabelControl()
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
        Me.ogbdetail = New DevExpress.XtraEditors.GroupControl()
        Me.ogbmainprocbutton = New DevExpress.XtraEditors.PanelControl()
        Me.ocmpreviewpisummary = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmeditpi = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmshowamount = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmsavepipayment = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmsavepaid = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmFTStateSendPIToAcc = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmFTStateFinishPO = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmFTStatePaid = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmFTETA = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmFTETD = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmFTInvoiceNo = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmFTNote = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmFTBaseOnLeadTimeDeliveryDate = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmFTImpactedGacDate = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmFTWarehouseDate = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmFTFacCheckCFMDeliveryDateFinal = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmFTFacCheckCFMDeliveryDate2 = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmFTFacCheckCFMDeliveryDate1 = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmsavetrack = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmsavepi = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmsendmail = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmsavelayout = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmclear = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmload = New DevExpress.XtraEditors.SimpleButton()
        Me.oDockManager = New DevExpress.XtraBars.Docking.DockManager(Me.components)
        CType(Me.ogvlist, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepCheckEdit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerPDFPI, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PopupPDFPI, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PopupPDFPI.SuspendLayout()
        CType(Me.ogdtime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvtime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemDateEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemDateEdit1.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemMemoExFTNote, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemTextFTInvoiceNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerPDF, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PopupPDF, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PopupPDF.SuspendLayout()
        Me.ogbheader.SuspendLayout()
        Me.DockPanel1_Container.SuspendLayout()
        CType(Me.chknewupload.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTEndPayment.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTEndPayment.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ochkselectall.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTStartPayment.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTStartPayment.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
        CType(Me.ogbdetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbdetail.SuspendLayout()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbmainprocbutton.SuspendLayout()
        CType(Me.oDockManager, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ogvlist
        '
        Me.ogvlist.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.sssFTStateNewVenderUpload, Me.ssFTPINo, Me.ssFTPIDate, Me.ssFTRcvPIDate, Me.sssFTPISuplCFMDeliveryDate, Me.FNPIPOQuantity, Me.ssFTStatePaid, Me.sasFTPaidDate, Me.ssFTPaidNote, Me.ssFTLCNo, Me.ssFTStateFile, Me.ssFNFileID, Me.ssPDF, Me.ssFTStatePIPDFFile, Me.sssssPIPDF, Me.sFTPurchaseNo})
        Me.ogvlist.GridControl = Me.ogdtime
        Me.ogvlist.Name = "ogvlist"
        Me.ogvlist.OptionsView.ColumnAutoWidth = False
        Me.ogvlist.OptionsView.ShowGroupPanel = False
        '
        'sssFTStateNewVenderUpload
        '
        Me.sssFTStateNewVenderUpload.Caption = "New"
        Me.sssFTStateNewVenderUpload.ColumnEdit = Me.RepCheckEdit
        Me.sssFTStateNewVenderUpload.FieldName = "FTStateNewVenderUpload"
        Me.sssFTStateNewVenderUpload.Name = "sssFTStateNewVenderUpload"
        Me.sssFTStateNewVenderUpload.OptionsColumn.AllowEdit = False
        Me.sssFTStateNewVenderUpload.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.sssFTStateNewVenderUpload.OptionsColumn.AllowMove = False
        Me.sssFTStateNewVenderUpload.OptionsColumn.ReadOnly = True
        Me.sssFTStateNewVenderUpload.Visible = True
        Me.sssFTStateNewVenderUpload.VisibleIndex = 0
        Me.sssFTStateNewVenderUpload.Width = 60
        '
        'RepCheckEdit
        '
        Me.RepCheckEdit.AutoHeight = False
        Me.RepCheckEdit.Caption = "Check"
        Me.RepCheckEdit.Name = "RepCheckEdit"
        Me.RepCheckEdit.ValueChecked = "1"
        Me.RepCheckEdit.ValueUnchecked = "0"
        '
        'ssFTPINo
        '
        Me.ssFTPINo.Caption = "P/I No"
        Me.ssFTPINo.FieldName = "FTPINo"
        Me.ssFTPINo.Name = "ssFTPINo"
        Me.ssFTPINo.OptionsColumn.AllowEdit = False
        Me.ssFTPINo.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.ssFTPINo.OptionsColumn.ReadOnly = True
        Me.ssFTPINo.Visible = True
        Me.ssFTPINo.VisibleIndex = 1
        Me.ssFTPINo.Width = 120
        '
        'ssFTPIDate
        '
        Me.ssFTPIDate.Caption = "P/I Date"
        Me.ssFTPIDate.FieldName = "FTPIDate"
        Me.ssFTPIDate.Name = "ssFTPIDate"
        Me.ssFTPIDate.OptionsColumn.AllowEdit = False
        Me.ssFTPIDate.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.ssFTPIDate.OptionsColumn.ReadOnly = True
        Me.ssFTPIDate.Visible = True
        Me.ssFTPIDate.VisibleIndex = 2
        Me.ssFTPIDate.Width = 100
        '
        'ssFTRcvPIDate
        '
        Me.ssFTRcvPIDate.Caption = "Rcv P/I Date"
        Me.ssFTRcvPIDate.FieldName = "FTRcvPIDate"
        Me.ssFTRcvPIDate.Name = "ssFTRcvPIDate"
        Me.ssFTRcvPIDate.OptionsColumn.AllowEdit = False
        Me.ssFTRcvPIDate.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.ssFTRcvPIDate.OptionsColumn.ReadOnly = True
        Me.ssFTRcvPIDate.Visible = True
        Me.ssFTRcvPIDate.VisibleIndex = 3
        Me.ssFTRcvPIDate.Width = 100
        '
        'sssFTPISuplCFMDeliveryDate
        '
        Me.sssFTPISuplCFMDeliveryDate.Caption = " CFM Delivery Date"
        Me.sssFTPISuplCFMDeliveryDate.FieldName = "FTPISuplCFMDeliveryDate"
        Me.sssFTPISuplCFMDeliveryDate.Name = "sssFTPISuplCFMDeliveryDate"
        Me.sssFTPISuplCFMDeliveryDate.OptionsColumn.AllowEdit = False
        Me.sssFTPISuplCFMDeliveryDate.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.sssFTPISuplCFMDeliveryDate.OptionsColumn.ReadOnly = True
        Me.sssFTPISuplCFMDeliveryDate.Visible = True
        Me.sssFTPISuplCFMDeliveryDate.VisibleIndex = 4
        Me.sssFTPISuplCFMDeliveryDate.Width = 100
        '
        'FNPIPOQuantity
        '
        Me.FNPIPOQuantity.Caption = "PI Quantity"
        Me.FNPIPOQuantity.DisplayFormat.FormatString = "{0:n2}"
        Me.FNPIPOQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNPIPOQuantity.FieldName = "FNPIPOQuantity"
        Me.FNPIPOQuantity.Name = "FNPIPOQuantity"
        Me.FNPIPOQuantity.OptionsColumn.AllowEdit = False
        Me.FNPIPOQuantity.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNPIPOQuantity.OptionsColumn.ReadOnly = True
        Me.FNPIPOQuantity.Visible = True
        Me.FNPIPOQuantity.VisibleIndex = 5
        Me.FNPIPOQuantity.Width = 100
        '
        'ssFTStatePaid
        '
        Me.ssFTStatePaid.Caption = "Paid"
        Me.ssFTStatePaid.ColumnEdit = Me.RepCheckEdit
        Me.ssFTStatePaid.FieldName = "FTStatePaid"
        Me.ssFTStatePaid.Name = "ssFTStatePaid"
        Me.ssFTStatePaid.OptionsColumn.AllowEdit = False
        Me.ssFTStatePaid.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.ssFTStatePaid.OptionsColumn.ReadOnly = True
        Me.ssFTStatePaid.Visible = True
        Me.ssFTStatePaid.VisibleIndex = 6
        Me.ssFTStatePaid.Width = 80
        '
        'sasFTPaidDate
        '
        Me.sasFTPaidDate.Caption = "Paid ate"
        Me.sasFTPaidDate.FieldName = "FTPaidDate"
        Me.sasFTPaidDate.Name = "sasFTPaidDate"
        Me.sasFTPaidDate.OptionsColumn.AllowEdit = False
        Me.sasFTPaidDate.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.sasFTPaidDate.OptionsColumn.ReadOnly = True
        Me.sasFTPaidDate.Visible = True
        Me.sasFTPaidDate.VisibleIndex = 7
        Me.sasFTPaidDate.Width = 100
        '
        'ssFTPaidNote
        '
        Me.ssFTPaidNote.Caption = "Paid Note"
        Me.ssFTPaidNote.FieldName = "FTPaidNote"
        Me.ssFTPaidNote.Name = "ssFTPaidNote"
        Me.ssFTPaidNote.OptionsColumn.AllowEdit = False
        Me.ssFTPaidNote.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.ssFTPaidNote.OptionsColumn.ReadOnly = True
        Me.ssFTPaidNote.Visible = True
        Me.ssFTPaidNote.VisibleIndex = 8
        Me.ssFTPaidNote.Width = 250
        '
        'ssFTLCNo
        '
        Me.ssFTLCNo.Caption = "LC No"
        Me.ssFTLCNo.FieldName = "FTLCNo"
        Me.ssFTLCNo.Name = "ssFTLCNo"
        Me.ssFTLCNo.OptionsColumn.AllowEdit = False
        Me.ssFTLCNo.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.ssFTLCNo.OptionsColumn.ReadOnly = True
        Me.ssFTLCNo.Visible = True
        Me.ssFTLCNo.VisibleIndex = 9
        Me.ssFTLCNo.Width = 120
        '
        'ssFTStateFile
        '
        Me.ssFTStateFile.Caption = "PDF"
        Me.ssFTStateFile.ColumnEdit = Me.RepCheckEdit
        Me.ssFTStateFile.FieldName = "FTStateFile"
        Me.ssFTStateFile.Name = "ssFTStateFile"
        Me.ssFTStateFile.OptionsColumn.AllowEdit = False
        Me.ssFTStateFile.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.ssFTStateFile.OptionsColumn.ReadOnly = True
        Me.ssFTStateFile.Visible = True
        Me.ssFTStateFile.VisibleIndex = 10
        Me.ssFTStateFile.Width = 80
        '
        'ssFNFileID
        '
        Me.ssFNFileID.Caption = "FNFileID"
        Me.ssFNFileID.FieldName = "FNFileID"
        Me.ssFNFileID.Name = "ssFNFileID"
        Me.ssFNFileID.OptionsColumn.AllowEdit = False
        Me.ssFNFileID.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.ssFNFileID.OptionsColumn.ReadOnly = True
        '
        'ssPDF
        '
        Me.ssPDF.Caption = "PDF Payment"
        Me.ssPDF.ColumnEdit = Me.RepositoryItemPopupContainerPDFPI
        Me.ssPDF.FieldName = "PDF"
        Me.ssPDF.Name = "ssPDF"
        Me.ssPDF.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.ssPDF.Visible = True
        Me.ssPDF.VisibleIndex = 11
        Me.ssPDF.Width = 120
        '
        'RepositoryItemPopupContainerPDFPI
        '
        Me.RepositoryItemPopupContainerPDFPI.AutoHeight = False
        Me.RepositoryItemPopupContainerPDFPI.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerPDFPI.Name = "RepositoryItemPopupContainerPDFPI"
        Me.RepositoryItemPopupContainerPDFPI.PopupControl = Me.PopupPDFPI
        '
        'PopupPDFPI
        '
        Me.PopupPDFPI.Controls.Add(Me.FilePdfViewerPI)
        Me.PopupPDFPI.Location = New System.Drawing.Point(789, 148)
        Me.PopupPDFPI.Name = "PopupPDFPI"
        Me.PopupPDFPI.Size = New System.Drawing.Size(446, 337)
        Me.PopupPDFPI.TabIndex = 302
        '
        'FilePdfViewerPI
        '
        Me.FilePdfViewerPI.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FilePdfViewerPI.Location = New System.Drawing.Point(0, 0)
        Me.FilePdfViewerPI.Name = "FilePdfViewerPI"
        Me.FilePdfViewerPI.Size = New System.Drawing.Size(446, 337)
        Me.FilePdfViewerPI.TabIndex = 0
        '
        'ssFTStatePIPDFFile
        '
        Me.ssFTStatePIPDFFile.Caption = "P/I PDF"
        Me.ssFTStatePIPDFFile.ColumnEdit = Me.RepCheckEdit
        Me.ssFTStatePIPDFFile.FieldName = "FTStatePIPDFFile"
        Me.ssFTStatePIPDFFile.Name = "ssFTStatePIPDFFile"
        Me.ssFTStatePIPDFFile.OptionsColumn.AllowEdit = False
        Me.ssFTStatePIPDFFile.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.ssFTStatePIPDFFile.OptionsColumn.ReadOnly = True
        Me.ssFTStatePIPDFFile.Visible = True
        Me.ssFTStatePIPDFFile.VisibleIndex = 12
        Me.ssFTStatePIPDFFile.Width = 100
        '
        'sssssPIPDF
        '
        Me.sssssPIPDF.Caption = "P/I PDF"
        Me.sssssPIPDF.ColumnEdit = Me.RepositoryItemPopupContainerPDFPI
        Me.sssssPIPDF.FieldName = "PIPDF"
        Me.sssssPIPDF.Name = "sssssPIPDF"
        Me.sssssPIPDF.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.sssssPIPDF.Visible = True
        Me.sssssPIPDF.VisibleIndex = 13
        Me.sssssPIPDF.Width = 120
        '
        'sFTPurchaseNo
        '
        Me.sFTPurchaseNo.Caption = "PurchaseNo"
        Me.sFTPurchaseNo.FieldName = "FTPurchaseNo"
        Me.sFTPurchaseNo.Name = "sFTPurchaseNo"
        Me.sFTPurchaseNo.OptionsColumn.AllowEdit = False
        Me.sFTPurchaseNo.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.sFTPurchaseNo.OptionsColumn.ReadOnly = True
        '
        'ogdtime
        '
        Me.ogdtime.Dock = System.Windows.Forms.DockStyle.Fill
        GridLevelNode1.LevelTemplate = Me.ogvlist
        GridLevelNode1.RelationName = "Item_PoNo"
        Me.ogdtime.LevelTree.Nodes.AddRange(New DevExpress.XtraGrid.GridLevelNode() {GridLevelNode1})
        Me.ogdtime.Location = New System.Drawing.Point(2, 2)
        Me.ogdtime.MainView = Me.ogvtime
        Me.ogdtime.Name = "ogdtime"
        Me.ogdtime.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepCheckEdit, Me.RepositoryItemDateEdit1, Me.RepositoryItemTextFTInvoiceNo, Me.RepositoryItemMemoExFTNote, Me.RepositoryItemPopupContainerPDF, Me.RepositoryItemPopupContainerPDFPI})
        Me.ogdtime.Size = New System.Drawing.Size(1314, 598)
        Me.ogdtime.TabIndex = 0
        Me.ogdtime.TabStop = False
        Me.ogdtime.Tag = "2|"
        Me.ogdtime.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvtime, Me.ogvlist})
        '
        'ogvtime
        '
        Me.ogvtime.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.FTStateNewVenderUpload, Me.FTSelect, Me.CFTPurchaseNo, Me.FDPurchaseDate, Me.FTPurchaseBy, Me.FTSuplCode, Me.FTSuplName, Me.xFTCurCode, Me.FDDeliveryDate, Me.cxFTPOCustPO, Me.xFTPOOrderNo, Me.xFTPOStyleCode, Me.FNHSysDeliveryID, Me.CXFTCmpCode, Me.FTBuyCode, Me.FNOrderQuantity, Me.FTUnitCode, Me.FNPOQuantity, Me.cxFNPOGrandAmt, Me.FNRCVQuantity, Me.xFNPOOutBalQuantity, Me.FTStateSendApp, Me.FTSendAppDate, Me.FNRevisedSeq, Me.FTRevisedDate, Me.FTPurchaseRevisedBy, Me.FTStateSuperVisorApp, Me.FTStateSuperVisorReject, Me.FTSuperVisorAppDate, Me.FTStateManagerApp, Me.FTStateManagerReject, Me.FTSuperManagerAppDate, Me.FTStateSendMail, Me.FTSendMailBy, Me.FTSendMailDate, Me.FTSendMailTime, Me.FTStateClosed, Me.FTClosedBy, Me.FTClosedDate, Me.FTClosedTime, Me.FTStateFinishPO, Me.FTSendFinishPOBy, Me.FTSendFinishPODate, Me.FTSendFinishPOTime, Me.FTStateSendPIToAcc, Me.FTSendPIToAccBy, Me.FTSendPIToAccDate, Me.FTSendPIToAccTime, Me.FTPINoBy, Me.FTPINoDate, Me.FTPINoTime, Me.FTPINo, Me.FTPIDate, Me.FTRcvPIDate, Me.FTPISuplCFMDeliveryDate, Me.FNPIQuantity, Me.FNPINetAmt, Me.xFNPIGrandNetAmt, Me.zFNCNAmt, Me.FNDNAmt, Me.zFNSurchargeAmt, Me.zFNPIGrandTotalAmt, Me.FTPIRemark, Me.FNPIPayType, Me.xFTPIPayDate, Me.xmFTPIPayTypeRemark, Me.FNLeadTime, Me.FTBaseOnLeadTimeDeliveryDate, Me.FNFNExtendLT, Me.FTFacCheckCFMDeliveryDate1, Me.FTFacCheckCFMDeliveryDate1By, Me.FTFacCheckCFMDeliveryDate1Date, Me.FTFacCheckCFMDeliveryDate1Time, Me.FTFacCheckCFMDeliveryDate2, Me.FTFacCheckCFMDeliveryDate2By, Me.FTFacCheckCFMDeliveryDate2Date, Me.FTFacCheckCFMDeliveryDate2Time, Me.FTFacCheckCFMDeliveryDateFinal, Me.FTFacCheckCFMDeliveryDateFinalBy, Me.FTFacCheckCFMDeliveryDateFinalDate, Me.FTFacCheckCFMDeliveryDateFinalTime, Me.FTWarehouseDate, Me.FTWarehouseDateBy, Me.FTWarehouseDateDate, Me.FTWarehouseDateTime, Me.FTImpactedGacDate, Me.FTImpactedGacDatey, Me.FTImpactedGacDateDate, Me.FTImpactedGacDateTime, Me.FTBaseOnLeadTimeDeliveryDateBy, Me.FTBaseOnLeadTimeDeliveryDateDate, Me.FTBaseOnLeadTimeDeliveryDateTime, Me.FTNote, Me.FTNoteBy, Me.FTNoteDate, Me.FTNoteTime, Me.FTInvoiceNo, Me.FTInvoiceNoBy, Me.FTInvoiceNoDate, Me.FTInvoiceNoTime, Me.FTETD, Me.FTETDBy, Me.FTETDDate, Me.FTETDTime, Me.FTETA, Me.FTETABy, Me.FTETADate, Me.FTETATime, Me.FTPIPayTypeBy, Me.FTPIPayTypeDate, Me.FTPIPayTypeTime, Me.xFTStatePaid, Me.FTPaidDate, Me.FTLCNo, Me.FTPaidNote, Me.FTStateFile, Me.sPDF, Me.sFNFileID, Me.FTStatePaidBy, Me.FTStatePaidDate, Me.FTStatePaidTime, Me.FNTrackSeq, Me.FTTrackDate, Me.FTTrackBy, Me.FTContactName, Me.FTTrackNote, Me.xFNHSysSuplId, Me.cxFNPoState, Me.cxFNPIPOQuantity, Me.cxFNPIPONetAmt, Me.zFNPOBalQuantity, Me.zFNPOBalGrandAmt, Me.FTStatePIPDFFile, Me.SSPIPDF})
        Me.ogvtime.GridControl = Me.ogdtime
        Me.ogvtime.Name = "ogvtime"
        Me.ogvtime.OptionsCustomization.AllowQuickHideColumns = False
        Me.ogvtime.OptionsView.ColumnAutoWidth = False
        Me.ogvtime.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.ogvtime.OptionsView.ShowGroupPanel = False
        Me.ogvtime.Tag = "2|"
        '
        'FTStateNewVenderUpload
        '
        Me.FTStateNewVenderUpload.Caption = "New Uploaded"
        Me.FTStateNewVenderUpload.ColumnEdit = Me.RepCheckEdit
        Me.FTStateNewVenderUpload.FieldName = "FTStateNewVenderUpload"
        Me.FTStateNewVenderUpload.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
        Me.FTStateNewVenderUpload.Name = "FTStateNewVenderUpload"
        Me.FTStateNewVenderUpload.OptionsColumn.AllowEdit = False
        Me.FTStateNewVenderUpload.OptionsColumn.ReadOnly = True
        Me.FTStateNewVenderUpload.Visible = True
        Me.FTStateNewVenderUpload.VisibleIndex = 0
        Me.FTStateNewVenderUpload.Width = 80
        '
        'FTSelect
        '
        Me.FTSelect.Caption = "Select"
        Me.FTSelect.ColumnEdit = Me.RepCheckEdit
        Me.FTSelect.FieldName = "FTSelect"
        Me.FTSelect.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
        Me.FTSelect.Name = "FTSelect"
        Me.FTSelect.Visible = True
        Me.FTSelect.VisibleIndex = 1
        Me.FTSelect.Width = 70
        '
        'CFTPurchaseNo
        '
        Me.CFTPurchaseNo.AppearanceHeader.Options.UseTextOptions = True
        Me.CFTPurchaseNo.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.CFTPurchaseNo.Caption = "Purchase No"
        Me.CFTPurchaseNo.FieldName = "FTPurchaseNo"
        Me.CFTPurchaseNo.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
        Me.CFTPurchaseNo.Name = "CFTPurchaseNo"
        Me.CFTPurchaseNo.OptionsColumn.AllowEdit = False
        Me.CFTPurchaseNo.OptionsColumn.ReadOnly = True
        Me.CFTPurchaseNo.Visible = True
        Me.CFTPurchaseNo.VisibleIndex = 2
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
        Me.FDPurchaseDate.VisibleIndex = 3
        Me.FDPurchaseDate.Width = 80
        '
        'FTPurchaseBy
        '
        Me.FTPurchaseBy.Caption = "Purchase By"
        Me.FTPurchaseBy.FieldName = "FTPurchaseBy"
        Me.FTPurchaseBy.Name = "FTPurchaseBy"
        Me.FTPurchaseBy.OptionsColumn.AllowEdit = False
        Me.FTPurchaseBy.OptionsColumn.ReadOnly = True
        Me.FTPurchaseBy.Visible = True
        Me.FTPurchaseBy.VisibleIndex = 4
        Me.FTPurchaseBy.Width = 100
        '
        'FTSuplCode
        '
        Me.FTSuplCode.AppearanceHeader.Options.UseTextOptions = True
        Me.FTSuplCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTSuplCode.Caption = "Supplier Code"
        Me.FTSuplCode.FieldName = "FTSuplCode"
        Me.FTSuplCode.Name = "FTSuplCode"
        Me.FTSuplCode.OptionsColumn.AllowEdit = False
        Me.FTSuplCode.OptionsColumn.ReadOnly = True
        Me.FTSuplCode.Visible = True
        Me.FTSuplCode.VisibleIndex = 5
        Me.FTSuplCode.Width = 100
        '
        'FTSuplName
        '
        Me.FTSuplName.AppearanceHeader.Options.UseTextOptions = True
        Me.FTSuplName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTSuplName.Caption = "Supplier Name"
        Me.FTSuplName.FieldName = "FTSuplName"
        Me.FTSuplName.Name = "FTSuplName"
        Me.FTSuplName.OptionsColumn.AllowEdit = False
        Me.FTSuplName.OptionsColumn.ReadOnly = True
        Me.FTSuplName.Visible = True
        Me.FTSuplName.VisibleIndex = 6
        Me.FTSuplName.Width = 200
        '
        'xFTCurCode
        '
        Me.xFTCurCode.Caption = "Currency"
        Me.xFTCurCode.FieldName = "FTCurCode"
        Me.xFTCurCode.Name = "xFTCurCode"
        Me.xFTCurCode.OptionsColumn.AllowEdit = False
        Me.xFTCurCode.OptionsColumn.ReadOnly = True
        Me.xFTCurCode.Visible = True
        Me.xFTCurCode.VisibleIndex = 7
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
        Me.FDDeliveryDate.VisibleIndex = 8
        Me.FDDeliveryDate.Width = 80
        '
        'cxFTPOCustPO
        '
        Me.cxFTPOCustPO.Caption = "Customer Po"
        Me.cxFTPOCustPO.FieldName = "FTPOCustPO"
        Me.cxFTPOCustPO.Name = "cxFTPOCustPO"
        Me.cxFTPOCustPO.OptionsColumn.AllowEdit = False
        Me.cxFTPOCustPO.OptionsColumn.ReadOnly = True
        Me.cxFTPOCustPO.Visible = True
        Me.cxFTPOCustPO.VisibleIndex = 9
        Me.cxFTPOCustPO.Width = 120
        '
        'xFTPOOrderNo
        '
        Me.xFTPOOrderNo.Caption = "Order No"
        Me.xFTPOOrderNo.FieldName = "FTPOOrderNo"
        Me.xFTPOOrderNo.Name = "xFTPOOrderNo"
        Me.xFTPOOrderNo.OptionsColumn.AllowEdit = False
        Me.xFTPOOrderNo.OptionsColumn.ReadOnly = True
        Me.xFTPOOrderNo.Visible = True
        Me.xFTPOOrderNo.VisibleIndex = 10
        Me.xFTPOOrderNo.Width = 120
        '
        'xFTPOStyleCode
        '
        Me.xFTPOStyleCode.Caption = "Style No"
        Me.xFTPOStyleCode.FieldName = "FTPOStyleCode"
        Me.xFTPOStyleCode.Name = "xFTPOStyleCode"
        Me.xFTPOStyleCode.OptionsColumn.AllowEdit = False
        Me.xFTPOStyleCode.OptionsColumn.ReadOnly = True
        Me.xFTPOStyleCode.Visible = True
        Me.xFTPOStyleCode.VisibleIndex = 11
        Me.xFTPOStyleCode.Width = 120
        '
        'FNHSysDeliveryID
        '
        Me.FNHSysDeliveryID.Caption = "Delivery"
        Me.FNHSysDeliveryID.FieldName = "FNHSysDeliveryID"
        Me.FNHSysDeliveryID.Name = "FNHSysDeliveryID"
        Me.FNHSysDeliveryID.OptionsColumn.AllowEdit = False
        Me.FNHSysDeliveryID.OptionsColumn.ReadOnly = True
        Me.FNHSysDeliveryID.Visible = True
        Me.FNHSysDeliveryID.VisibleIndex = 12
        Me.FNHSysDeliveryID.Width = 100
        '
        'CXFTCmpCode
        '
        Me.CXFTCmpCode.Caption = "Company"
        Me.CXFTCmpCode.FieldName = "FTCmpCode"
        Me.CXFTCmpCode.Name = "CXFTCmpCode"
        Me.CXFTCmpCode.OptionsColumn.AllowEdit = False
        Me.CXFTCmpCode.OptionsColumn.ReadOnly = True
        Me.CXFTCmpCode.Visible = True
        Me.CXFTCmpCode.VisibleIndex = 13
        '
        'FTBuyCode
        '
        Me.FTBuyCode.Caption = "Buy"
        Me.FTBuyCode.FieldName = "FTBuyCode"
        Me.FTBuyCode.Name = "FTBuyCode"
        Me.FTBuyCode.OptionsColumn.AllowEdit = False
        Me.FTBuyCode.OptionsColumn.ReadOnly = True
        Me.FTBuyCode.Visible = True
        Me.FTBuyCode.VisibleIndex = 14
        '
        'FNOrderQuantity
        '
        Me.FNOrderQuantity.Caption = "Order Quantity"
        Me.FNOrderQuantity.DisplayFormat.FormatString = "{0:n0}"
        Me.FNOrderQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNOrderQuantity.FieldName = "FNOrderQuantity"
        Me.FNOrderQuantity.Name = "FNOrderQuantity"
        Me.FNOrderQuantity.OptionsColumn.AllowEdit = False
        Me.FNOrderQuantity.OptionsColumn.ReadOnly = True
        Me.FNOrderQuantity.Visible = True
        Me.FNOrderQuantity.VisibleIndex = 15
        '
        'FTUnitCode
        '
        Me.FTUnitCode.AppearanceHeader.Options.UseTextOptions = True
        Me.FTUnitCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTUnitCode.Caption = "PO Unit"
        Me.FTUnitCode.FieldName = "FTUnitCode"
        Me.FTUnitCode.Name = "FTUnitCode"
        Me.FTUnitCode.OptionsColumn.AllowEdit = False
        Me.FTUnitCode.OptionsColumn.ReadOnly = True
        Me.FTUnitCode.Visible = True
        Me.FTUnitCode.VisibleIndex = 16
        Me.FTUnitCode.Width = 70
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
        Me.FNPOQuantity.VisibleIndex = 17
        Me.FNPOQuantity.Width = 80
        '
        'cxFNPOGrandAmt
        '
        Me.cxFNPOGrandAmt.Caption = "PO Amount"
        Me.cxFNPOGrandAmt.DisplayFormat.FormatString = "{0:n2}"
        Me.cxFNPOGrandAmt.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cxFNPOGrandAmt.FieldName = "FNPOGrandAmt"
        Me.cxFNPOGrandAmt.Name = "cxFNPOGrandAmt"
        Me.cxFNPOGrandAmt.OptionsColumn.AllowEdit = False
        Me.cxFNPOGrandAmt.OptionsColumn.ReadOnly = True
        Me.cxFNPOGrandAmt.Visible = True
        Me.cxFNPOGrandAmt.VisibleIndex = 18
        '
        'FNRCVQuantity
        '
        Me.FNRCVQuantity.AppearanceCell.Options.UseTextOptions = True
        Me.FNRCVQuantity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNRCVQuantity.AppearanceHeader.Options.UseTextOptions = True
        Me.FNRCVQuantity.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FNRCVQuantity.Caption = "RCV Qty"
        Me.FNRCVQuantity.DisplayFormat.FormatString = "{0:n4}"
        Me.FNRCVQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNRCVQuantity.FieldName = "FNRcvQuantity"
        Me.FNRCVQuantity.Name = "FNRCVQuantity"
        Me.FNRCVQuantity.OptionsColumn.AllowEdit = False
        Me.FNRCVQuantity.OptionsColumn.ReadOnly = True
        Me.FNRCVQuantity.Visible = True
        Me.FNRCVQuantity.VisibleIndex = 19
        Me.FNRCVQuantity.Width = 80
        '
        'xFNPOOutBalQuantity
        '
        Me.xFNPOOutBalQuantity.AppearanceCell.Options.UseTextOptions = True
        Me.xFNPOOutBalQuantity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.xFNPOOutBalQuantity.AppearanceHeader.Options.UseTextOptions = True
        Me.xFNPOOutBalQuantity.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.xFNPOOutBalQuantity.Caption = "PO Outstanding Qty."
        Me.xFNPOOutBalQuantity.DisplayFormat.FormatString = "{0:n4}"
        Me.xFNPOOutBalQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.xFNPOOutBalQuantity.FieldName = "FNPOOutBalQuantity"
        Me.xFNPOOutBalQuantity.Name = "xFNPOOutBalQuantity"
        Me.xFNPOOutBalQuantity.OptionsColumn.AllowEdit = False
        Me.xFNPOOutBalQuantity.OptionsColumn.ReadOnly = True
        Me.xFNPOOutBalQuantity.Visible = True
        Me.xFNPOOutBalQuantity.VisibleIndex = 20
        Me.xFNPOOutBalQuantity.Width = 80
        '
        'FTStateSendApp
        '
        Me.FTStateSendApp.AppearanceHeader.Options.UseTextOptions = True
        Me.FTStateSendApp.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTStateSendApp.Caption = "Send App."
        Me.FTStateSendApp.ColumnEdit = Me.RepCheckEdit
        Me.FTStateSendApp.FieldName = "FTStateSendApp"
        Me.FTStateSendApp.Name = "FTStateSendApp"
        Me.FTStateSendApp.OptionsColumn.AllowEdit = False
        Me.FTStateSendApp.OptionsColumn.ReadOnly = True
        Me.FTStateSendApp.Visible = True
        Me.FTStateSendApp.VisibleIndex = 21
        Me.FTStateSendApp.Width = 60
        '
        'FTSendAppDate
        '
        Me.FTSendAppDate.AppearanceHeader.Options.UseTextOptions = True
        Me.FTSendAppDate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTSendAppDate.Caption = "Send App. Date"
        Me.FTSendAppDate.FieldName = "FTSendAppDate"
        Me.FTSendAppDate.Name = "FTSendAppDate"
        Me.FTSendAppDate.OptionsColumn.AllowEdit = False
        Me.FTSendAppDate.OptionsColumn.ReadOnly = True
        Me.FTSendAppDate.Visible = True
        Me.FTSendAppDate.VisibleIndex = 22
        Me.FTSendAppDate.Width = 70
        '
        'FNRevisedSeq
        '
        Me.FNRevisedSeq.Caption = "Revised"
        Me.FNRevisedSeq.DisplayFormat.FormatString = "{0:n0}"
        Me.FNRevisedSeq.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNRevisedSeq.FieldName = "FNRevisedSeq"
        Me.FNRevisedSeq.Name = "FNRevisedSeq"
        Me.FNRevisedSeq.OptionsColumn.AllowEdit = False
        Me.FNRevisedSeq.OptionsColumn.ReadOnly = True
        Me.FNRevisedSeq.Visible = True
        Me.FNRevisedSeq.VisibleIndex = 23
        Me.FNRevisedSeq.Width = 80
        '
        'FTRevisedDate
        '
        Me.FTRevisedDate.Caption = "Revised Date"
        Me.FTRevisedDate.FieldName = "FTRevisedDate"
        Me.FTRevisedDate.Name = "FTRevisedDate"
        Me.FTRevisedDate.OptionsColumn.AllowEdit = False
        Me.FTRevisedDate.OptionsColumn.ReadOnly = True
        Me.FTRevisedDate.Visible = True
        Me.FTRevisedDate.VisibleIndex = 24
        Me.FTRevisedDate.Width = 100
        '
        'FTPurchaseRevisedBy
        '
        Me.FTPurchaseRevisedBy.Caption = "Purchase Revised By"
        Me.FTPurchaseRevisedBy.FieldName = "FTPurchaseRevisedBy"
        Me.FTPurchaseRevisedBy.Name = "FTPurchaseRevisedBy"
        Me.FTPurchaseRevisedBy.OptionsColumn.AllowEdit = False
        Me.FTPurchaseRevisedBy.OptionsColumn.ReadOnly = True
        Me.FTPurchaseRevisedBy.Visible = True
        Me.FTPurchaseRevisedBy.VisibleIndex = 25
        Me.FTPurchaseRevisedBy.Width = 100
        '
        'FTStateSuperVisorApp
        '
        Me.FTStateSuperVisorApp.AppearanceHeader.Options.UseTextOptions = True
        Me.FTStateSuperVisorApp.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTStateSuperVisorApp.Caption = "SuperVisor App."
        Me.FTStateSuperVisorApp.ColumnEdit = Me.RepCheckEdit
        Me.FTStateSuperVisorApp.FieldName = "FTStateSuperVisorApp"
        Me.FTStateSuperVisorApp.Name = "FTStateSuperVisorApp"
        Me.FTStateSuperVisorApp.OptionsColumn.AllowEdit = False
        Me.FTStateSuperVisorApp.OptionsColumn.ReadOnly = True
        Me.FTStateSuperVisorApp.Visible = True
        Me.FTStateSuperVisorApp.VisibleIndex = 26
        Me.FTStateSuperVisorApp.Width = 60
        '
        'FTStateSuperVisorReject
        '
        Me.FTStateSuperVisorReject.Caption = "FTStateSuperVisorReject"
        Me.FTStateSuperVisorReject.ColumnEdit = Me.RepCheckEdit
        Me.FTStateSuperVisorReject.FieldName = "FTStateSuperVisorReject"
        Me.FTStateSuperVisorReject.Name = "FTStateSuperVisorReject"
        Me.FTStateSuperVisorReject.OptionsColumn.AllowEdit = False
        Me.FTStateSuperVisorReject.OptionsColumn.ReadOnly = True
        Me.FTStateSuperVisorReject.Width = 60
        '
        'FTSuperVisorAppDate
        '
        Me.FTSuperVisorAppDate.AppearanceHeader.Options.UseTextOptions = True
        Me.FTSuperVisorAppDate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTSuperVisorAppDate.Caption = "SuperVisor App. Date"
        Me.FTSuperVisorAppDate.FieldName = "FTSuperVisorAppDate"
        Me.FTSuperVisorAppDate.Name = "FTSuperVisorAppDate"
        Me.FTSuperVisorAppDate.OptionsColumn.AllowEdit = False
        Me.FTSuperVisorAppDate.OptionsColumn.ReadOnly = True
        Me.FTSuperVisorAppDate.Visible = True
        Me.FTSuperVisorAppDate.VisibleIndex = 27
        Me.FTSuperVisorAppDate.Width = 70
        '
        'FTStateManagerApp
        '
        Me.FTStateManagerApp.AppearanceHeader.Options.UseTextOptions = True
        Me.FTStateManagerApp.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTStateManagerApp.Caption = "Manager App."
        Me.FTStateManagerApp.ColumnEdit = Me.RepCheckEdit
        Me.FTStateManagerApp.FieldName = "FTStateManagerApp"
        Me.FTStateManagerApp.Name = "FTStateManagerApp"
        Me.FTStateManagerApp.OptionsColumn.AllowEdit = False
        Me.FTStateManagerApp.OptionsColumn.ReadOnly = True
        Me.FTStateManagerApp.Visible = True
        Me.FTStateManagerApp.VisibleIndex = 28
        Me.FTStateManagerApp.Width = 60
        '
        'FTStateManagerReject
        '
        Me.FTStateManagerReject.Caption = "FTStateManagerReject"
        Me.FTStateManagerReject.ColumnEdit = Me.RepCheckEdit
        Me.FTStateManagerReject.FieldName = "FTStateManagerReject"
        Me.FTStateManagerReject.Name = "FTStateManagerReject"
        Me.FTStateManagerReject.OptionsColumn.AllowEdit = False
        Me.FTStateManagerReject.OptionsColumn.ReadOnly = True
        Me.FTStateManagerReject.Width = 60
        '
        'FTSuperManagerAppDate
        '
        Me.FTSuperManagerAppDate.AppearanceHeader.Options.UseTextOptions = True
        Me.FTSuperManagerAppDate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTSuperManagerAppDate.Caption = "Manager App. Date"
        Me.FTSuperManagerAppDate.FieldName = "FTSuperManagerAppDate"
        Me.FTSuperManagerAppDate.Name = "FTSuperManagerAppDate"
        Me.FTSuperManagerAppDate.OptionsColumn.AllowEdit = False
        Me.FTSuperManagerAppDate.OptionsColumn.ReadOnly = True
        Me.FTSuperManagerAppDate.Visible = True
        Me.FTSuperManagerAppDate.VisibleIndex = 29
        Me.FTSuperManagerAppDate.Width = 70
        '
        'FTStateSendMail
        '
        Me.FTStateSendMail.Caption = "Send Mail"
        Me.FTStateSendMail.ColumnEdit = Me.RepCheckEdit
        Me.FTStateSendMail.FieldName = "FTStateSendMail"
        Me.FTStateSendMail.Name = "FTStateSendMail"
        Me.FTStateSendMail.Visible = True
        Me.FTStateSendMail.VisibleIndex = 30
        '
        'FTSendMailBy
        '
        Me.FTSendMailBy.Caption = "Send Mai lBy"
        Me.FTSendMailBy.FieldName = "FTSendMailBy"
        Me.FTSendMailBy.Name = "FTSendMailBy"
        Me.FTSendMailBy.OptionsColumn.AllowEdit = False
        Me.FTSendMailBy.OptionsColumn.ReadOnly = True
        Me.FTSendMailBy.Visible = True
        Me.FTSendMailBy.VisibleIndex = 31
        '
        'FTSendMailDate
        '
        Me.FTSendMailDate.Caption = "Send Mail Date"
        Me.FTSendMailDate.FieldName = "FTSendMailDate"
        Me.FTSendMailDate.Name = "FTSendMailDate"
        Me.FTSendMailDate.OptionsColumn.AllowEdit = False
        Me.FTSendMailDate.OptionsColumn.ReadOnly = True
        Me.FTSendMailDate.Visible = True
        Me.FTSendMailDate.VisibleIndex = 32
        '
        'FTSendMailTime
        '
        Me.FTSendMailTime.Caption = "Send Mail Time"
        Me.FTSendMailTime.FieldName = "FTSendMailTime"
        Me.FTSendMailTime.Name = "FTSendMailTime"
        Me.FTSendMailTime.OptionsColumn.AllowEdit = False
        Me.FTSendMailTime.OptionsColumn.ReadOnly = True
        Me.FTSendMailTime.Visible = True
        Me.FTSendMailTime.VisibleIndex = 33
        '
        'FTStateClosed
        '
        Me.FTStateClosed.Caption = "FTStateClosed"
        Me.FTStateClosed.FieldName = "FTStateClosed"
        Me.FTStateClosed.Name = "FTStateClosed"
        Me.FTStateClosed.OptionsColumn.AllowEdit = False
        Me.FTStateClosed.OptionsColumn.ReadOnly = True
        '
        'FTClosedBy
        '
        Me.FTClosedBy.Caption = "FTClosedBy"
        Me.FTClosedBy.FieldName = "FTClosedBy"
        Me.FTClosedBy.Name = "FTClosedBy"
        Me.FTClosedBy.OptionsColumn.AllowEdit = False
        Me.FTClosedBy.OptionsColumn.ReadOnly = True
        '
        'FTClosedDate
        '
        Me.FTClosedDate.Caption = "FTClosedDate"
        Me.FTClosedDate.FieldName = "FTClosedDate"
        Me.FTClosedDate.Name = "FTClosedDate"
        Me.FTClosedDate.OptionsColumn.AllowEdit = False
        Me.FTClosedDate.OptionsColumn.ReadOnly = True
        '
        'FTClosedTime
        '
        Me.FTClosedTime.Caption = "FTClosedTime"
        Me.FTClosedTime.FieldName = "FTClosedTime"
        Me.FTClosedTime.Name = "FTClosedTime"
        Me.FTClosedTime.OptionsColumn.AllowEdit = False
        Me.FTClosedTime.OptionsColumn.ReadOnly = True
        '
        'FTStateFinishPO
        '
        Me.FTStateFinishPO.Caption = "Finish PO"
        Me.FTStateFinishPO.ColumnEdit = Me.RepCheckEdit
        Me.FTStateFinishPO.FieldName = "FTStateFinishPO"
        Me.FTStateFinishPO.Name = "FTStateFinishPO"
        Me.FTStateFinishPO.Visible = True
        Me.FTStateFinishPO.VisibleIndex = 34
        Me.FTStateFinishPO.Width = 60
        '
        'FTSendFinishPOBy
        '
        Me.FTSendFinishPOBy.Caption = "Finish PO By"
        Me.FTSendFinishPOBy.FieldName = "FTSendFinishPOBy"
        Me.FTSendFinishPOBy.Name = "FTSendFinishPOBy"
        Me.FTSendFinishPOBy.OptionsColumn.AllowEdit = False
        Me.FTSendFinishPOBy.OptionsColumn.ReadOnly = True
        Me.FTSendFinishPOBy.Visible = True
        Me.FTSendFinishPOBy.VisibleIndex = 35
        Me.FTSendFinishPOBy.Width = 80
        '
        'FTSendFinishPODate
        '
        Me.FTSendFinishPODate.Caption = "Finish PO Date"
        Me.FTSendFinishPODate.FieldName = "FTSendFinishPODate"
        Me.FTSendFinishPODate.Name = "FTSendFinishPODate"
        Me.FTSendFinishPODate.OptionsColumn.AllowEdit = False
        Me.FTSendFinishPODate.OptionsColumn.ReadOnly = True
        Me.FTSendFinishPODate.Visible = True
        Me.FTSendFinishPODate.VisibleIndex = 36
        Me.FTSendFinishPODate.Width = 80
        '
        'FTSendFinishPOTime
        '
        Me.FTSendFinishPOTime.Caption = "Finish PO Time"
        Me.FTSendFinishPOTime.FieldName = "FTSendFinishPOTime"
        Me.FTSendFinishPOTime.Name = "FTSendFinishPOTime"
        Me.FTSendFinishPOTime.OptionsColumn.AllowEdit = False
        Me.FTSendFinishPOTime.OptionsColumn.ReadOnly = True
        Me.FTSendFinishPOTime.Visible = True
        Me.FTSendFinishPOTime.VisibleIndex = 37
        Me.FTSendFinishPOTime.Width = 60
        '
        'FTStateSendPIToAcc
        '
        Me.FTStateSendPIToAcc.Caption = "Send PI To Acc"
        Me.FTStateSendPIToAcc.ColumnEdit = Me.RepCheckEdit
        Me.FTStateSendPIToAcc.FieldName = "FTStateSendPIToAcc"
        Me.FTStateSendPIToAcc.Name = "FTStateSendPIToAcc"
        Me.FTStateSendPIToAcc.Visible = True
        Me.FTStateSendPIToAcc.VisibleIndex = 38
        Me.FTStateSendPIToAcc.Width = 60
        '
        'FTSendPIToAccBy
        '
        Me.FTSendPIToAccBy.Caption = "Send PI To Acc By"
        Me.FTSendPIToAccBy.FieldName = "FTSendPIToAccBy"
        Me.FTSendPIToAccBy.Name = "FTSendPIToAccBy"
        Me.FTSendPIToAccBy.OptionsColumn.AllowEdit = False
        Me.FTSendPIToAccBy.OptionsColumn.ReadOnly = True
        Me.FTSendPIToAccBy.Visible = True
        Me.FTSendPIToAccBy.VisibleIndex = 39
        Me.FTSendPIToAccBy.Width = 80
        '
        'FTSendPIToAccDate
        '
        Me.FTSendPIToAccDate.Caption = "Send PI To Acc Date"
        Me.FTSendPIToAccDate.FieldName = "FTSendPIToAccDate"
        Me.FTSendPIToAccDate.Name = "FTSendPIToAccDate"
        Me.FTSendPIToAccDate.OptionsColumn.AllowEdit = False
        Me.FTSendPIToAccDate.OptionsColumn.ReadOnly = True
        Me.FTSendPIToAccDate.Visible = True
        Me.FTSendPIToAccDate.VisibleIndex = 40
        Me.FTSendPIToAccDate.Width = 80
        '
        'FTSendPIToAccTime
        '
        Me.FTSendPIToAccTime.Caption = "Send PI To Acc Time"
        Me.FTSendPIToAccTime.FieldName = "FTSendPIToAccTime"
        Me.FTSendPIToAccTime.Name = "FTSendPIToAccTime"
        Me.FTSendPIToAccTime.OptionsColumn.AllowEdit = False
        Me.FTSendPIToAccTime.OptionsColumn.ReadOnly = True
        Me.FTSendPIToAccTime.Visible = True
        Me.FTSendPIToAccTime.VisibleIndex = 41
        Me.FTSendPIToAccTime.Width = 60
        '
        'FTPINoBy
        '
        Me.FTPINoBy.Caption = "FTPINoBy"
        Me.FTPINoBy.FieldName = "FTPINoBy"
        Me.FTPINoBy.Name = "FTPINoBy"
        Me.FTPINoBy.OptionsColumn.AllowEdit = False
        Me.FTPINoBy.OptionsColumn.ReadOnly = True
        '
        'FTPINoDate
        '
        Me.FTPINoDate.Caption = "FTPINoDate"
        Me.FTPINoDate.FieldName = "FTPINoDate"
        Me.FTPINoDate.Name = "FTPINoDate"
        Me.FTPINoDate.OptionsColumn.AllowEdit = False
        Me.FTPINoDate.OptionsColumn.ReadOnly = True
        '
        'FTPINoTime
        '
        Me.FTPINoTime.Caption = "FTPINoTime"
        Me.FTPINoTime.FieldName = "FTPINoTime"
        Me.FTPINoTime.Name = "FTPINoTime"
        Me.FTPINoTime.OptionsColumn.AllowEdit = False
        Me.FTPINoTime.OptionsColumn.ReadOnly = True
        '
        'FTPINo
        '
        Me.FTPINo.Caption = "PI No."
        Me.FTPINo.FieldName = "FTPINo"
        Me.FTPINo.Name = "FTPINo"
        Me.FTPINo.OptionsColumn.AllowEdit = False
        Me.FTPINo.OptionsColumn.ReadOnly = True
        Me.FTPINo.Visible = True
        Me.FTPINo.VisibleIndex = 42
        '
        'FTPIDate
        '
        Me.FTPIDate.Caption = "PI Date"
        Me.FTPIDate.FieldName = "FTPIDate"
        Me.FTPIDate.Name = "FTPIDate"
        Me.FTPIDate.OptionsColumn.AllowEdit = False
        Me.FTPIDate.OptionsColumn.ReadOnly = True
        Me.FTPIDate.Visible = True
        Me.FTPIDate.VisibleIndex = 43
        '
        'FTRcvPIDate
        '
        Me.FTRcvPIDate.Caption = "Rcv PI Date"
        Me.FTRcvPIDate.FieldName = "FTRcvPIDate"
        Me.FTRcvPIDate.Name = "FTRcvPIDate"
        Me.FTRcvPIDate.OptionsColumn.AllowEdit = False
        Me.FTRcvPIDate.OptionsColumn.ReadOnly = True
        Me.FTRcvPIDate.Visible = True
        Me.FTRcvPIDate.VisibleIndex = 44
        '
        'FTPISuplCFMDeliveryDate
        '
        Me.FTPISuplCFMDeliveryDate.Caption = "PI Supl CFM Delivery Date"
        Me.FTPISuplCFMDeliveryDate.FieldName = "FTPISuplCFMDeliveryDate"
        Me.FTPISuplCFMDeliveryDate.Name = "FTPISuplCFMDeliveryDate"
        Me.FTPISuplCFMDeliveryDate.OptionsColumn.AllowEdit = False
        Me.FTPISuplCFMDeliveryDate.OptionsColumn.ReadOnly = True
        Me.FTPISuplCFMDeliveryDate.Visible = True
        Me.FTPISuplCFMDeliveryDate.VisibleIndex = 45
        '
        'FNPIQuantity
        '
        Me.FNPIQuantity.Caption = "PI Quantity"
        Me.FNPIQuantity.DisplayFormat.FormatString = "{0:n4}"
        Me.FNPIQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNPIQuantity.FieldName = "FNPIPOQuantity"
        Me.FNPIQuantity.Name = "FNPIQuantity"
        Me.FNPIQuantity.OptionsColumn.AllowEdit = False
        Me.FNPIQuantity.OptionsColumn.ReadOnly = True
        Me.FNPIQuantity.Visible = True
        Me.FNPIQuantity.VisibleIndex = 46
        '
        'FNPINetAmt
        '
        Me.FNPINetAmt.Caption = "PI Net Amt"
        Me.FNPINetAmt.DisplayFormat.FormatString = "{0:n2}"
        Me.FNPINetAmt.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNPINetAmt.FieldName = "FNPIPONetAmt"
        Me.FNPINetAmt.Name = "FNPINetAmt"
        Me.FNPINetAmt.OptionsColumn.AllowEdit = False
        Me.FNPINetAmt.OptionsColumn.ReadOnly = True
        Me.FNPINetAmt.Visible = True
        Me.FNPINetAmt.VisibleIndex = 47
        '
        'xFNPIGrandNetAmt
        '
        Me.xFNPIGrandNetAmt.Caption = "PI Doc. Amt."
        Me.xFNPIGrandNetAmt.DisplayFormat.FormatString = "{0:n2}"
        Me.xFNPIGrandNetAmt.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.xFNPIGrandNetAmt.FieldName = "FNPIGrandNetAmt"
        Me.xFNPIGrandNetAmt.Name = "xFNPIGrandNetAmt"
        Me.xFNPIGrandNetAmt.OptionsColumn.AllowEdit = False
        Me.xFNPIGrandNetAmt.OptionsColumn.ReadOnly = True
        Me.xFNPIGrandNetAmt.Visible = True
        Me.xFNPIGrandNetAmt.VisibleIndex = 48
        '
        'zFNCNAmt
        '
        Me.zFNCNAmt.Caption = "PI Doc. CN. Amt."
        Me.zFNCNAmt.DisplayFormat.FormatString = "{0:n2}"
        Me.zFNCNAmt.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.zFNCNAmt.FieldName = "FNCNAmt"
        Me.zFNCNAmt.Name = "zFNCNAmt"
        Me.zFNCNAmt.OptionsColumn.AllowEdit = False
        Me.zFNCNAmt.OptionsColumn.ReadOnly = True
        Me.zFNCNAmt.Visible = True
        Me.zFNCNAmt.VisibleIndex = 49
        '
        'FNDNAmt
        '
        Me.FNDNAmt.Caption = "PI Doc. DN. Amt."
        Me.FNDNAmt.DisplayFormat.FormatString = "{0:n2}"
        Me.FNDNAmt.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNDNAmt.FieldName = "FNDNAmt"
        Me.FNDNAmt.Name = "FNDNAmt"
        Me.FNDNAmt.OptionsColumn.AllowEdit = False
        Me.FNDNAmt.OptionsColumn.ReadOnly = True
        Me.FNDNAmt.Visible = True
        Me.FNDNAmt.VisibleIndex = 50
        '
        'zFNSurchargeAmt
        '
        Me.zFNSurchargeAmt.Caption = "PI Doc. Surcharge Amt."
        Me.zFNSurchargeAmt.DisplayFormat.FormatString = "{0:n2}"
        Me.zFNSurchargeAmt.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.zFNSurchargeAmt.FieldName = "FNSurchargeAmt"
        Me.zFNSurchargeAmt.Name = "zFNSurchargeAmt"
        Me.zFNSurchargeAmt.OptionsColumn.AllowEdit = False
        Me.zFNSurchargeAmt.OptionsColumn.ReadOnly = True
        Me.zFNSurchargeAmt.Visible = True
        Me.zFNSurchargeAmt.VisibleIndex = 51
        '
        'zFNPIGrandTotalAmt
        '
        Me.zFNPIGrandTotalAmt.Caption = "PI Doc. Net Amt."
        Me.zFNPIGrandTotalAmt.DisplayFormat.FormatString = "{0:n2}"
        Me.zFNPIGrandTotalAmt.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.zFNPIGrandTotalAmt.FieldName = "FNPIGrandTotalAmt"
        Me.zFNPIGrandTotalAmt.Name = "zFNPIGrandTotalAmt"
        Me.zFNPIGrandTotalAmt.OptionsColumn.AllowEdit = False
        Me.zFNPIGrandTotalAmt.OptionsColumn.ReadOnly = True
        Me.zFNPIGrandTotalAmt.Visible = True
        Me.zFNPIGrandTotalAmt.VisibleIndex = 52
        '
        'FTPIRemark
        '
        Me.FTPIRemark.Caption = "PI Remark"
        Me.FTPIRemark.FieldName = "FTPIRemark"
        Me.FTPIRemark.Name = "FTPIRemark"
        Me.FTPIRemark.OptionsColumn.AllowEdit = False
        Me.FTPIRemark.OptionsColumn.ReadOnly = True
        Me.FTPIRemark.Visible = True
        Me.FTPIRemark.VisibleIndex = 53
        Me.FTPIRemark.Width = 200
        '
        'FNPIPayType
        '
        Me.FNPIPayType.Caption = "Pay Type"
        Me.FNPIPayType.FieldName = "FNPIPayType"
        Me.FNPIPayType.Name = "FNPIPayType"
        Me.FNPIPayType.OptionsColumn.AllowEdit = False
        Me.FNPIPayType.OptionsColumn.ReadOnly = True
        Me.FNPIPayType.Visible = True
        Me.FNPIPayType.VisibleIndex = 54
        Me.FNPIPayType.Width = 100
        '
        'xFTPIPayDate
        '
        Me.xFTPIPayDate.Caption = "Plan Pay Date"
        Me.xFTPIPayDate.FieldName = "FTPIPayDate."
        Me.xFTPIPayDate.Name = "xFTPIPayDate"
        Me.xFTPIPayDate.OptionsColumn.AllowEdit = False
        Me.xFTPIPayDate.OptionsColumn.ReadOnly = True
        Me.xFTPIPayDate.Visible = True
        Me.xFTPIPayDate.VisibleIndex = 55
        '
        'xmFTPIPayTypeRemark
        '
        Me.xmFTPIPayTypeRemark.Caption = "FTPIPayTypeRemark"
        Me.xmFTPIPayTypeRemark.FieldName = "Plan Pay Remark"
        Me.xmFTPIPayTypeRemark.Name = "xmFTPIPayTypeRemark"
        Me.xmFTPIPayTypeRemark.OptionsColumn.AllowEdit = False
        Me.xmFTPIPayTypeRemark.OptionsColumn.ReadOnly = True
        Me.xmFTPIPayTypeRemark.Visible = True
        Me.xmFTPIPayTypeRemark.VisibleIndex = 56
        '
        'FNLeadTime
        '
        Me.FNLeadTime.Caption = "Lead Time"
        Me.FNLeadTime.FieldName = "FNLeadTime"
        Me.FNLeadTime.Name = "FNLeadTime"
        Me.FNLeadTime.OptionsColumn.AllowEdit = False
        Me.FNLeadTime.OptionsColumn.ReadOnly = True
        Me.FNLeadTime.Visible = True
        Me.FNLeadTime.VisibleIndex = 57
        Me.FNLeadTime.Width = 80
        '
        'FTBaseOnLeadTimeDeliveryDate
        '
        Me.FTBaseOnLeadTimeDeliveryDate.Caption = "Base On LeadTime Delivery Date"
        Me.FTBaseOnLeadTimeDeliveryDate.FieldName = "FTBaseOnLeadTimeDeliveryDate"
        Me.FTBaseOnLeadTimeDeliveryDate.Name = "FTBaseOnLeadTimeDeliveryDate"
        Me.FTBaseOnLeadTimeDeliveryDate.OptionsColumn.AllowEdit = False
        Me.FTBaseOnLeadTimeDeliveryDate.OptionsColumn.ReadOnly = True
        Me.FTBaseOnLeadTimeDeliveryDate.Visible = True
        Me.FTBaseOnLeadTimeDeliveryDate.VisibleIndex = 58
        Me.FTBaseOnLeadTimeDeliveryDate.Width = 80
        '
        'FNFNExtendLT
        '
        Me.FNFNExtendLT.Caption = "Extend L/T"
        Me.FNFNExtendLT.DisplayFormat.FormatString = "{0:n0}"
        Me.FNFNExtendLT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNFNExtendLT.FieldName = "FNFNExtendLT"
        Me.FNFNExtendLT.Name = "FNFNExtendLT"
        Me.FNFNExtendLT.OptionsColumn.AllowEdit = False
        Me.FNFNExtendLT.OptionsColumn.ReadOnly = True
        Me.FNFNExtendLT.Visible = True
        Me.FNFNExtendLT.VisibleIndex = 59
        '
        'FTFacCheckCFMDeliveryDate1
        '
        Me.FTFacCheckCFMDeliveryDate1.Caption = "Vendor confirm delivery"
        Me.FTFacCheckCFMDeliveryDate1.ColumnEdit = Me.RepositoryItemDateEdit1
        Me.FTFacCheckCFMDeliveryDate1.FieldName = "FTFacCheckCFMDeliveryDate1"
        Me.FTFacCheckCFMDeliveryDate1.Name = "FTFacCheckCFMDeliveryDate1"
        Me.FTFacCheckCFMDeliveryDate1.Visible = True
        Me.FTFacCheckCFMDeliveryDate1.VisibleIndex = 60
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
        'FTFacCheckCFMDeliveryDate1By
        '
        Me.FTFacCheckCFMDeliveryDate1By.Caption = "FTFacCheckCFMDeliveryDate1By"
        Me.FTFacCheckCFMDeliveryDate1By.FieldName = "FTFacCheckCFMDeliveryDate1By"
        Me.FTFacCheckCFMDeliveryDate1By.Name = "FTFacCheckCFMDeliveryDate1By"
        Me.FTFacCheckCFMDeliveryDate1By.OptionsColumn.AllowEdit = False
        Me.FTFacCheckCFMDeliveryDate1By.OptionsColumn.ReadOnly = True
        '
        'FTFacCheckCFMDeliveryDate1Date
        '
        Me.FTFacCheckCFMDeliveryDate1Date.Caption = "FTFacCheckCFMDeliveryDate1Date"
        Me.FTFacCheckCFMDeliveryDate1Date.FieldName = "FTFacCheckCFMDeliveryDate1Date"
        Me.FTFacCheckCFMDeliveryDate1Date.Name = "FTFacCheckCFMDeliveryDate1Date"
        Me.FTFacCheckCFMDeliveryDate1Date.OptionsColumn.AllowEdit = False
        Me.FTFacCheckCFMDeliveryDate1Date.OptionsColumn.ReadOnly = True
        '
        'FTFacCheckCFMDeliveryDate1Time
        '
        Me.FTFacCheckCFMDeliveryDate1Time.Caption = "FTFacCheckCFMDeliveryDate1Time"
        Me.FTFacCheckCFMDeliveryDate1Time.FieldName = "FTFacCheckCFMDeliveryDate1Time"
        Me.FTFacCheckCFMDeliveryDate1Time.Name = "FTFacCheckCFMDeliveryDate1Time"
        Me.FTFacCheckCFMDeliveryDate1Time.OptionsColumn.AllowEdit = False
        Me.FTFacCheckCFMDeliveryDate1Time.OptionsColumn.ReadOnly = True
        '
        'FTFacCheckCFMDeliveryDate2
        '
        Me.FTFacCheckCFMDeliveryDate2.Caption = "Vendor confirm delivery -7"
        Me.FTFacCheckCFMDeliveryDate2.ColumnEdit = Me.RepositoryItemDateEdit1
        Me.FTFacCheckCFMDeliveryDate2.FieldName = "FTFacCheckCFMDeliveryDate2"
        Me.FTFacCheckCFMDeliveryDate2.Name = "FTFacCheckCFMDeliveryDate2"
        Me.FTFacCheckCFMDeliveryDate2.Visible = True
        Me.FTFacCheckCFMDeliveryDate2.VisibleIndex = 61
        '
        'FTFacCheckCFMDeliveryDate2By
        '
        Me.FTFacCheckCFMDeliveryDate2By.Caption = "FTFacCheckCFMDeliveryDate2By"
        Me.FTFacCheckCFMDeliveryDate2By.FieldName = "FTFacCheckCFMDeliveryDate2By"
        Me.FTFacCheckCFMDeliveryDate2By.Name = "FTFacCheckCFMDeliveryDate2By"
        Me.FTFacCheckCFMDeliveryDate2By.OptionsColumn.AllowEdit = False
        Me.FTFacCheckCFMDeliveryDate2By.OptionsColumn.ReadOnly = True
        '
        'FTFacCheckCFMDeliveryDate2Date
        '
        Me.FTFacCheckCFMDeliveryDate2Date.Caption = "FTFacCheckCFMDeliveryDate2Date"
        Me.FTFacCheckCFMDeliveryDate2Date.FieldName = "FTFacCheckCFMDeliveryDate2Date"
        Me.FTFacCheckCFMDeliveryDate2Date.Name = "FTFacCheckCFMDeliveryDate2Date"
        Me.FTFacCheckCFMDeliveryDate2Date.OptionsColumn.AllowEdit = False
        Me.FTFacCheckCFMDeliveryDate2Date.OptionsColumn.ReadOnly = True
        '
        'FTFacCheckCFMDeliveryDate2Time
        '
        Me.FTFacCheckCFMDeliveryDate2Time.Caption = "FTFacCheckCFMDeliveryDate2Time"
        Me.FTFacCheckCFMDeliveryDate2Time.FieldName = "FTFacCheckCFMDeliveryDate2Time"
        Me.FTFacCheckCFMDeliveryDate2Time.Name = "FTFacCheckCFMDeliveryDate2Time"
        Me.FTFacCheckCFMDeliveryDate2Time.OptionsColumn.AllowEdit = False
        Me.FTFacCheckCFMDeliveryDate2Time.OptionsColumn.ReadOnly = True
        '
        'FTFacCheckCFMDeliveryDateFinal
        '
        Me.FTFacCheckCFMDeliveryDateFinal.Caption = "final delivery from vendor"
        Me.FTFacCheckCFMDeliveryDateFinal.ColumnEdit = Me.RepositoryItemDateEdit1
        Me.FTFacCheckCFMDeliveryDateFinal.FieldName = "FTFacCheckCFMDeliveryDateFinal"
        Me.FTFacCheckCFMDeliveryDateFinal.Name = "FTFacCheckCFMDeliveryDateFinal"
        Me.FTFacCheckCFMDeliveryDateFinal.Visible = True
        Me.FTFacCheckCFMDeliveryDateFinal.VisibleIndex = 62
        '
        'FTFacCheckCFMDeliveryDateFinalBy
        '
        Me.FTFacCheckCFMDeliveryDateFinalBy.Caption = "FTFacCheckCFMDeliveryDateFinalBy"
        Me.FTFacCheckCFMDeliveryDateFinalBy.FieldName = "FTFacCheckCFMDeliveryDateFinalBy"
        Me.FTFacCheckCFMDeliveryDateFinalBy.Name = "FTFacCheckCFMDeliveryDateFinalBy"
        Me.FTFacCheckCFMDeliveryDateFinalBy.OptionsColumn.AllowEdit = False
        Me.FTFacCheckCFMDeliveryDateFinalBy.OptionsColumn.ReadOnly = True
        '
        'FTFacCheckCFMDeliveryDateFinalDate
        '
        Me.FTFacCheckCFMDeliveryDateFinalDate.Caption = "FTFacCheckCFMDeliveryDateFinalDate"
        Me.FTFacCheckCFMDeliveryDateFinalDate.FieldName = "FTFacCheckCFMDeliveryDateFinalDate"
        Me.FTFacCheckCFMDeliveryDateFinalDate.Name = "FTFacCheckCFMDeliveryDateFinalDate"
        Me.FTFacCheckCFMDeliveryDateFinalDate.OptionsColumn.AllowEdit = False
        Me.FTFacCheckCFMDeliveryDateFinalDate.OptionsColumn.ReadOnly = True
        '
        'FTFacCheckCFMDeliveryDateFinalTime
        '
        Me.FTFacCheckCFMDeliveryDateFinalTime.Caption = "FTFacCheckCFMDeliveryDateFinalTime"
        Me.FTFacCheckCFMDeliveryDateFinalTime.FieldName = "FTFacCheckCFMDeliveryDateFinalTime"
        Me.FTFacCheckCFMDeliveryDateFinalTime.Name = "FTFacCheckCFMDeliveryDateFinalTime"
        Me.FTFacCheckCFMDeliveryDateFinalTime.OptionsColumn.AllowEdit = False
        Me.FTFacCheckCFMDeliveryDateFinalTime.OptionsColumn.ReadOnly = True
        '
        'FTWarehouseDate
        '
        Me.FTWarehouseDate.Caption = "WareHouseDate"
        Me.FTWarehouseDate.ColumnEdit = Me.RepositoryItemDateEdit1
        Me.FTWarehouseDate.FieldName = "FTWarehouseDate"
        Me.FTWarehouseDate.Name = "FTWarehouseDate"
        Me.FTWarehouseDate.Visible = True
        Me.FTWarehouseDate.VisibleIndex = 63
        '
        'FTWarehouseDateBy
        '
        Me.FTWarehouseDateBy.Caption = "FTWarehouseDateBy"
        Me.FTWarehouseDateBy.FieldName = "FTWarehouseDateBy"
        Me.FTWarehouseDateBy.Name = "FTWarehouseDateBy"
        Me.FTWarehouseDateBy.OptionsColumn.AllowEdit = False
        Me.FTWarehouseDateBy.OptionsColumn.ReadOnly = True
        '
        'FTWarehouseDateDate
        '
        Me.FTWarehouseDateDate.Caption = "FTWarehouseDateDate"
        Me.FTWarehouseDateDate.FieldName = "FTWarehouseDateDate"
        Me.FTWarehouseDateDate.Name = "FTWarehouseDateDate"
        Me.FTWarehouseDateDate.OptionsColumn.AllowEdit = False
        Me.FTWarehouseDateDate.OptionsColumn.ReadOnly = True
        '
        'FTWarehouseDateTime
        '
        Me.FTWarehouseDateTime.Caption = "FTWarehouseDateTime"
        Me.FTWarehouseDateTime.FieldName = "FTWarehouseDateTime"
        Me.FTWarehouseDateTime.Name = "FTWarehouseDateTime"
        Me.FTWarehouseDateTime.OptionsColumn.AllowEdit = False
        Me.FTWarehouseDateTime.OptionsColumn.ReadOnly = True
        '
        'FTImpactedGacDate
        '
        Me.FTImpactedGacDate.Caption = "impacted  GAC date "
        Me.FTImpactedGacDate.ColumnEdit = Me.RepositoryItemDateEdit1
        Me.FTImpactedGacDate.FieldName = "FTImpactedGacDate"
        Me.FTImpactedGacDate.Name = "FTImpactedGacDate"
        Me.FTImpactedGacDate.Visible = True
        Me.FTImpactedGacDate.VisibleIndex = 64
        '
        'FTImpactedGacDatey
        '
        Me.FTImpactedGacDatey.Caption = "FTImpactedGacDatey"
        Me.FTImpactedGacDatey.FieldName = "FTImpactedGacDatey"
        Me.FTImpactedGacDatey.Name = "FTImpactedGacDatey"
        Me.FTImpactedGacDatey.OptionsColumn.AllowEdit = False
        Me.FTImpactedGacDatey.OptionsColumn.ReadOnly = True
        '
        'FTImpactedGacDateDate
        '
        Me.FTImpactedGacDateDate.Caption = "FTImpactedGacDateDate"
        Me.FTImpactedGacDateDate.FieldName = "FTImpactedGacDateDate"
        Me.FTImpactedGacDateDate.Name = "FTImpactedGacDateDate"
        Me.FTImpactedGacDateDate.OptionsColumn.AllowEdit = False
        Me.FTImpactedGacDateDate.OptionsColumn.ReadOnly = True
        '
        'FTImpactedGacDateTime
        '
        Me.FTImpactedGacDateTime.Caption = "FTImpactedGacDateTime"
        Me.FTImpactedGacDateTime.FieldName = "FTImpactedGacDateTime"
        Me.FTImpactedGacDateTime.Name = "FTImpactedGacDateTime"
        Me.FTImpactedGacDateTime.OptionsColumn.AllowEdit = False
        Me.FTImpactedGacDateTime.OptionsColumn.ReadOnly = True
        '
        'FTBaseOnLeadTimeDeliveryDateBy
        '
        Me.FTBaseOnLeadTimeDeliveryDateBy.Caption = "FTBaseOnLeadTimeDeliveryDateBy"
        Me.FTBaseOnLeadTimeDeliveryDateBy.FieldName = "FTBaseOnLeadTimeDeliveryDateBy"
        Me.FTBaseOnLeadTimeDeliveryDateBy.Name = "FTBaseOnLeadTimeDeliveryDateBy"
        Me.FTBaseOnLeadTimeDeliveryDateBy.OptionsColumn.AllowEdit = False
        Me.FTBaseOnLeadTimeDeliveryDateBy.OptionsColumn.ReadOnly = True
        '
        'FTBaseOnLeadTimeDeliveryDateDate
        '
        Me.FTBaseOnLeadTimeDeliveryDateDate.Caption = "FTBaseOnLeadTimeDeliveryDateDate"
        Me.FTBaseOnLeadTimeDeliveryDateDate.FieldName = "FTBaseOnLeadTimeDeliveryDateDate"
        Me.FTBaseOnLeadTimeDeliveryDateDate.Name = "FTBaseOnLeadTimeDeliveryDateDate"
        Me.FTBaseOnLeadTimeDeliveryDateDate.OptionsColumn.AllowEdit = False
        Me.FTBaseOnLeadTimeDeliveryDateDate.OptionsColumn.ReadOnly = True
        '
        'FTBaseOnLeadTimeDeliveryDateTime
        '
        Me.FTBaseOnLeadTimeDeliveryDateTime.Caption = "FTBaseOnLeadTimeDeliveryDateTime"
        Me.FTBaseOnLeadTimeDeliveryDateTime.FieldName = "FTBaseOnLeadTimeDeliveryDateTime"
        Me.FTBaseOnLeadTimeDeliveryDateTime.Name = "FTBaseOnLeadTimeDeliveryDateTime"
        Me.FTBaseOnLeadTimeDeliveryDateTime.OptionsColumn.AllowEdit = False
        Me.FTBaseOnLeadTimeDeliveryDateTime.OptionsColumn.ReadOnly = True
        '
        'FTNote
        '
        Me.FTNote.Caption = "Note"
        Me.FTNote.ColumnEdit = Me.RepositoryItemMemoExFTNote
        Me.FTNote.FieldName = "FTNote"
        Me.FTNote.Name = "FTNote"
        Me.FTNote.Visible = True
        Me.FTNote.VisibleIndex = 65
        Me.FTNote.Width = 200
        '
        'RepositoryItemMemoExFTNote
        '
        Me.RepositoryItemMemoExFTNote.AutoHeight = False
        Me.RepositoryItemMemoExFTNote.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemMemoExFTNote.Name = "RepositoryItemMemoExFTNote"
        Me.RepositoryItemMemoExFTNote.ShowIcon = False
        '
        'FTNoteBy
        '
        Me.FTNoteBy.Caption = "FTNoteBy"
        Me.FTNoteBy.FieldName = "FTNoteBy"
        Me.FTNoteBy.Name = "FTNoteBy"
        Me.FTNoteBy.OptionsColumn.AllowEdit = False
        Me.FTNoteBy.OptionsColumn.ReadOnly = True
        '
        'FTNoteDate
        '
        Me.FTNoteDate.Caption = "FTNoteDate"
        Me.FTNoteDate.FieldName = "FTNoteDate"
        Me.FTNoteDate.Name = "FTNoteDate"
        Me.FTNoteDate.OptionsColumn.AllowEdit = False
        Me.FTNoteDate.OptionsColumn.ReadOnly = True
        '
        'FTNoteTime
        '
        Me.FTNoteTime.Caption = "FTNoteTime"
        Me.FTNoteTime.FieldName = "FTNoteTime"
        Me.FTNoteTime.Name = "FTNoteTime"
        Me.FTNoteTime.OptionsColumn.AllowEdit = False
        Me.FTNoteTime.OptionsColumn.ReadOnly = True
        '
        'FTInvoiceNo
        '
        Me.FTInvoiceNo.Caption = "Invoice No"
        Me.FTInvoiceNo.ColumnEdit = Me.RepositoryItemTextFTInvoiceNo
        Me.FTInvoiceNo.FieldName = "FTInvoiceNo"
        Me.FTInvoiceNo.Name = "FTInvoiceNo"
        Me.FTInvoiceNo.Visible = True
        Me.FTInvoiceNo.VisibleIndex = 66
        '
        'RepositoryItemTextFTInvoiceNo
        '
        Me.RepositoryItemTextFTInvoiceNo.AutoHeight = False
        Me.RepositoryItemTextFTInvoiceNo.MaxLength = 30
        Me.RepositoryItemTextFTInvoiceNo.Name = "RepositoryItemTextFTInvoiceNo"
        '
        'FTInvoiceNoBy
        '
        Me.FTInvoiceNoBy.Caption = "FTInvoiceNoBy"
        Me.FTInvoiceNoBy.FieldName = "FTInvoiceNoBy"
        Me.FTInvoiceNoBy.Name = "FTInvoiceNoBy"
        Me.FTInvoiceNoBy.OptionsColumn.AllowEdit = False
        Me.FTInvoiceNoBy.OptionsColumn.ReadOnly = True
        '
        'FTInvoiceNoDate
        '
        Me.FTInvoiceNoDate.Caption = "FTInvoiceNoDate"
        Me.FTInvoiceNoDate.FieldName = "FTInvoiceNoDate"
        Me.FTInvoiceNoDate.Name = "FTInvoiceNoDate"
        Me.FTInvoiceNoDate.OptionsColumn.AllowEdit = False
        Me.FTInvoiceNoDate.OptionsColumn.ReadOnly = True
        '
        'FTInvoiceNoTime
        '
        Me.FTInvoiceNoTime.Caption = "FTInvoiceNoTime"
        Me.FTInvoiceNoTime.FieldName = "FTInvoiceNoTime"
        Me.FTInvoiceNoTime.Name = "FTInvoiceNoTime"
        Me.FTInvoiceNoTime.OptionsColumn.AllowEdit = False
        Me.FTInvoiceNoTime.OptionsColumn.ReadOnly = True
        '
        'FTETD
        '
        Me.FTETD.Caption = "ETD"
        Me.FTETD.ColumnEdit = Me.RepositoryItemDateEdit1
        Me.FTETD.FieldName = "FTETD"
        Me.FTETD.Name = "FTETD"
        Me.FTETD.Visible = True
        Me.FTETD.VisibleIndex = 67
        '
        'FTETDBy
        '
        Me.FTETDBy.Caption = "FTETDBy"
        Me.FTETDBy.FieldName = "FTETDBy"
        Me.FTETDBy.Name = "FTETDBy"
        Me.FTETDBy.OptionsColumn.AllowEdit = False
        Me.FTETDBy.OptionsColumn.ReadOnly = True
        '
        'FTETDDate
        '
        Me.FTETDDate.Caption = "FTETDDate"
        Me.FTETDDate.FieldName = "FTETDDate"
        Me.FTETDDate.Name = "FTETDDate"
        Me.FTETDDate.OptionsColumn.AllowEdit = False
        Me.FTETDDate.OptionsColumn.ReadOnly = True
        '
        'FTETDTime
        '
        Me.FTETDTime.Caption = "FTETDTime"
        Me.FTETDTime.FieldName = "FTETDTime"
        Me.FTETDTime.Name = "FTETDTime"
        Me.FTETDTime.OptionsColumn.AllowEdit = False
        Me.FTETDTime.OptionsColumn.ReadOnly = True
        '
        'FTETA
        '
        Me.FTETA.Caption = "ETA"
        Me.FTETA.ColumnEdit = Me.RepositoryItemDateEdit1
        Me.FTETA.FieldName = "FTETA"
        Me.FTETA.Name = "FTETA"
        Me.FTETA.Visible = True
        Me.FTETA.VisibleIndex = 68
        '
        'FTETABy
        '
        Me.FTETABy.Caption = "FTETABy"
        Me.FTETABy.FieldName = "FTETABy"
        Me.FTETABy.Name = "FTETABy"
        Me.FTETABy.OptionsColumn.AllowEdit = False
        Me.FTETABy.OptionsColumn.ReadOnly = True
        '
        'FTETADate
        '
        Me.FTETADate.Caption = "FTETADate"
        Me.FTETADate.FieldName = "FTETADate"
        Me.FTETADate.Name = "FTETADate"
        Me.FTETADate.OptionsColumn.AllowEdit = False
        Me.FTETADate.OptionsColumn.ReadOnly = True
        '
        'FTETATime
        '
        Me.FTETATime.Caption = "FTETATime"
        Me.FTETATime.FieldName = "FTETATime"
        Me.FTETATime.Name = "FTETATime"
        Me.FTETATime.OptionsColumn.AllowEdit = False
        Me.FTETATime.OptionsColumn.ReadOnly = True
        '
        'FTPIPayTypeBy
        '
        Me.FTPIPayTypeBy.Caption = "FTPIPayTypeBy"
        Me.FTPIPayTypeBy.FieldName = "FTPIPayTypeBy"
        Me.FTPIPayTypeBy.Name = "FTPIPayTypeBy"
        Me.FTPIPayTypeBy.OptionsColumn.AllowEdit = False
        Me.FTPIPayTypeBy.OptionsColumn.ReadOnly = True
        '
        'FTPIPayTypeDate
        '
        Me.FTPIPayTypeDate.Caption = "FTPIPayTypeDate"
        Me.FTPIPayTypeDate.FieldName = "FTPIPayTypeDate"
        Me.FTPIPayTypeDate.Name = "FTPIPayTypeDate"
        Me.FTPIPayTypeDate.OptionsColumn.AllowEdit = False
        Me.FTPIPayTypeDate.OptionsColumn.ReadOnly = True
        '
        'FTPIPayTypeTime
        '
        Me.FTPIPayTypeTime.Caption = "FTPIPayTypeTime"
        Me.FTPIPayTypeTime.FieldName = "FTPIPayTypeTime"
        Me.FTPIPayTypeTime.Name = "FTPIPayTypeTime"
        Me.FTPIPayTypeTime.OptionsColumn.AllowEdit = False
        Me.FTPIPayTypeTime.OptionsColumn.ReadOnly = True
        '
        'xFTStatePaid
        '
        Me.xFTStatePaid.Caption = "Paid"
        Me.xFTStatePaid.ColumnEdit = Me.RepCheckEdit
        Me.xFTStatePaid.FieldName = "FTStatePaid"
        Me.xFTStatePaid.Name = "xFTStatePaid"
        Me.xFTStatePaid.OptionsColumn.AllowEdit = False
        Me.xFTStatePaid.OptionsColumn.ReadOnly = True
        Me.xFTStatePaid.Visible = True
        Me.xFTStatePaid.VisibleIndex = 69
        '
        'FTPaidDate
        '
        Me.FTPaidDate.Caption = "FTPaidDate"
        Me.FTPaidDate.FieldName = "Paid Date"
        Me.FTPaidDate.Name = "FTPaidDate"
        Me.FTPaidDate.OptionsColumn.AllowEdit = False
        Me.FTPaidDate.OptionsColumn.ReadOnly = True
        Me.FTPaidDate.Visible = True
        Me.FTPaidDate.VisibleIndex = 70
        '
        'FTLCNo
        '
        Me.FTLCNo.Caption = "LC No."
        Me.FTLCNo.FieldName = "FTLCNo"
        Me.FTLCNo.Name = "FTLCNo"
        Me.FTLCNo.OptionsColumn.AllowEdit = False
        Me.FTLCNo.OptionsColumn.ReadOnly = True
        Me.FTLCNo.Visible = True
        Me.FTLCNo.VisibleIndex = 71
        Me.FTLCNo.Width = 150
        '
        'FTPaidNote
        '
        Me.FTPaidNote.Caption = "FTPaidNote"
        Me.FTPaidNote.FieldName = "Paid Note"
        Me.FTPaidNote.Name = "FTPaidNote"
        Me.FTPaidNote.OptionsColumn.AllowEdit = False
        Me.FTPaidNote.OptionsColumn.ReadOnly = True
        Me.FTPaidNote.Visible = True
        Me.FTPaidNote.VisibleIndex = 72
        Me.FTPaidNote.Width = 200
        '
        'FTStateFile
        '
        Me.FTStateFile.Caption = "File Payment"
        Me.FTStateFile.ColumnEdit = Me.RepCheckEdit
        Me.FTStateFile.FieldName = "FTStateFile"
        Me.FTStateFile.Name = "FTStateFile"
        Me.FTStateFile.OptionsColumn.AllowEdit = False
        Me.FTStateFile.OptionsColumn.ReadOnly = True
        Me.FTStateFile.Visible = True
        Me.FTStateFile.VisibleIndex = 73
        '
        'sPDF
        '
        Me.sPDF.Caption = "PDF"
        Me.sPDF.ColumnEdit = Me.RepositoryItemPopupContainerPDF
        Me.sPDF.FieldName = "PDF"
        Me.sPDF.Name = "sPDF"
        Me.sPDF.Visible = True
        Me.sPDF.VisibleIndex = 74
        Me.sPDF.Width = 100
        '
        'RepositoryItemPopupContainerPDF
        '
        Me.RepositoryItemPopupContainerPDF.AutoHeight = False
        Me.RepositoryItemPopupContainerPDF.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerPDF.Name = "RepositoryItemPopupContainerPDF"
        '
        'sFNFileID
        '
        Me.sFNFileID.Caption = "FNFileID"
        Me.sFNFileID.FieldName = "FNFileID"
        Me.sFNFileID.Name = "sFNFileID"
        Me.sFNFileID.OptionsColumn.AllowEdit = False
        Me.sFNFileID.OptionsColumn.ReadOnly = True
        '
        'FTStatePaidBy
        '
        Me.FTStatePaidBy.Caption = "FTStatePaidBy"
        Me.FTStatePaidBy.FieldName = "FTStatePaidBy"
        Me.FTStatePaidBy.Name = "FTStatePaidBy"
        Me.FTStatePaidBy.OptionsColumn.AllowEdit = False
        Me.FTStatePaidBy.OptionsColumn.ReadOnly = True
        '
        'FTStatePaidDate
        '
        Me.FTStatePaidDate.Caption = "FTStatePaidDate"
        Me.FTStatePaidDate.FieldName = "FTStatePaidDate"
        Me.FTStatePaidDate.Name = "FTStatePaidDate"
        Me.FTStatePaidDate.OptionsColumn.AllowEdit = False
        Me.FTStatePaidDate.OptionsColumn.ReadOnly = True
        '
        'FTStatePaidTime
        '
        Me.FTStatePaidTime.Caption = "FTStatePaidTime"
        Me.FTStatePaidTime.FieldName = "FTStatePaidTime"
        Me.FTStatePaidTime.Name = "FTStatePaidTime"
        Me.FTStatePaidTime.OptionsColumn.AllowEdit = False
        Me.FTStatePaidTime.OptionsColumn.ReadOnly = True
        '
        'FNTrackSeq
        '
        Me.FNTrackSeq.Caption = "FNTrackSeq"
        Me.FNTrackSeq.FieldName = "Track Seq."
        Me.FNTrackSeq.Name = "FNTrackSeq"
        Me.FNTrackSeq.OptionsColumn.AllowEdit = False
        Me.FNTrackSeq.OptionsColumn.ReadOnly = True
        Me.FNTrackSeq.Visible = True
        Me.FNTrackSeq.VisibleIndex = 75
        '
        'FTTrackDate
        '
        Me.FTTrackDate.Caption = "FTTrackDate"
        Me.FTTrackDate.FieldName = "Track Date"
        Me.FTTrackDate.Name = "FTTrackDate"
        Me.FTTrackDate.OptionsColumn.AllowEdit = False
        Me.FTTrackDate.OptionsColumn.ReadOnly = True
        Me.FTTrackDate.Visible = True
        Me.FTTrackDate.VisibleIndex = 76
        Me.FTTrackDate.Width = 80
        '
        'FTTrackBy
        '
        Me.FTTrackBy.Caption = "FTTrackBy"
        Me.FTTrackBy.FieldName = "Track By"
        Me.FTTrackBy.Name = "FTTrackBy"
        Me.FTTrackBy.OptionsColumn.AllowEdit = False
        Me.FTTrackBy.OptionsColumn.ReadOnly = True
        Me.FTTrackBy.Visible = True
        Me.FTTrackBy.VisibleIndex = 77
        '
        'FTContactName
        '
        Me.FTContactName.Caption = "FTContactName"
        Me.FTContactName.FieldName = "Contact Name"
        Me.FTContactName.Name = "FTContactName"
        Me.FTContactName.OptionsColumn.AllowEdit = False
        Me.FTContactName.OptionsColumn.ReadOnly = True
        Me.FTContactName.Visible = True
        Me.FTContactName.VisibleIndex = 78
        '
        'FTTrackNote
        '
        Me.FTTrackNote.Caption = "FTTrackNote"
        Me.FTTrackNote.FieldName = "Track Note"
        Me.FTTrackNote.Name = "FTTrackNote"
        Me.FTTrackNote.OptionsColumn.AllowEdit = False
        Me.FTTrackNote.OptionsColumn.ReadOnly = True
        Me.FTTrackNote.Visible = True
        Me.FTTrackNote.VisibleIndex = 79
        '
        'xFNHSysSuplId
        '
        Me.xFNHSysSuplId.Caption = "FNHSysSuplId"
        Me.xFNHSysSuplId.FieldName = "FNHSysSuplId"
        Me.xFNHSysSuplId.Name = "xFNHSysSuplId"
        '
        'cxFNPoState
        '
        Me.cxFNPoState.Caption = "FNPoState"
        Me.cxFNPoState.FieldName = "FNPoState"
        Me.cxFNPoState.Name = "cxFNPoState"
        Me.cxFNPoState.OptionsColumn.AllowEdit = False
        Me.cxFNPoState.OptionsColumn.ReadOnly = True
        '
        'cxFNPIPOQuantity
        '
        Me.cxFNPIPOQuantity.Caption = "FNPIPOQuantity"
        Me.cxFNPIPOQuantity.FieldName = "FNPIQuantity"
        Me.cxFNPIPOQuantity.Name = "cxFNPIPOQuantity"
        Me.cxFNPIPOQuantity.OptionsColumn.AllowEdit = False
        Me.cxFNPIPOQuantity.OptionsColumn.ReadOnly = True
        '
        'cxFNPIPONetAmt
        '
        Me.cxFNPIPONetAmt.Caption = "FNPIPONetAmt"
        Me.cxFNPIPONetAmt.FieldName = "FNPINetAmt"
        Me.cxFNPIPONetAmt.Name = "cxFNPIPONetAmt"
        Me.cxFNPIPONetAmt.OptionsColumn.AllowEdit = False
        Me.cxFNPIPONetAmt.OptionsColumn.ReadOnly = True
        '
        'zFNPOBalQuantity
        '
        Me.zFNPOBalQuantity.Caption = "FNPOBalQuantity"
        Me.zFNPOBalQuantity.FieldName = "FNPOBalQuantity"
        Me.zFNPOBalQuantity.Name = "zFNPOBalQuantity"
        '
        'zFNPOBalGrandAmt
        '
        Me.zFNPOBalGrandAmt.Caption = "FNPOBalGrandAmt"
        Me.zFNPOBalGrandAmt.FieldName = "FNPOBalGrandAmt"
        Me.zFNPOBalGrandAmt.Name = "zFNPOBalGrandAmt"
        '
        'FTStatePIPDFFile
        '
        Me.FTStatePIPDFFile.Caption = "File PI"
        Me.FTStatePIPDFFile.ColumnEdit = Me.RepCheckEdit
        Me.FTStatePIPDFFile.FieldName = "FTStatePIPDFFile"
        Me.FTStatePIPDFFile.Name = "FTStatePIPDFFile"
        Me.FTStatePIPDFFile.OptionsColumn.AllowEdit = False
        Me.FTStatePIPDFFile.OptionsColumn.ReadOnly = True
        Me.FTStatePIPDFFile.Visible = True
        Me.FTStatePIPDFFile.VisibleIndex = 80
        '
        'SSPIPDF
        '
        Me.SSPIPDF.Caption = "PI PDF"
        Me.SSPIPDF.ColumnEdit = Me.RepositoryItemPopupContainerPDF
        Me.SSPIPDF.FieldName = "PIPDF"
        Me.SSPIPDF.Name = "SSPIPDF"
        Me.SSPIPDF.Width = 100
        '
        'PopupPDF
        '
        Me.PopupPDF.Controls.Add(Me.FilePdfViewer)
        Me.PopupPDF.Location = New System.Drawing.Point(414, 0)
        Me.PopupPDF.Name = "PopupPDF"
        Me.PopupPDF.Size = New System.Drawing.Size(446, 337)
        Me.PopupPDF.TabIndex = 301
        '
        'FilePdfViewer
        '
        Me.FilePdfViewer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FilePdfViewer.Location = New System.Drawing.Point(0, 0)
        Me.FilePdfViewer.Name = "FilePdfViewer"
        Me.FilePdfViewer.Size = New System.Drawing.Size(446, 337)
        Me.FilePdfViewer.TabIndex = 0
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
        Me.ogbheader.OriginalSize = New System.Drawing.Size(1318, 155)
        Me.ogbheader.Size = New System.Drawing.Size(1318, 155)
        Me.ogbheader.TabStop = False
        Me.ogbheader.Text = "Criteria"
        '
        'DockPanel1_Container
        '
        Me.DockPanel1_Container.Controls.Add(Me.chknewupload)
        Me.DockPanel1_Container.Controls.Add(Me.FTEndPayment)
        Me.DockPanel1_Container.Controls.Add(Me.EndPayment_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.ochkselectall)
        Me.DockPanel1_Container.Controls.Add(Me.FTStartPayment)
        Me.DockPanel1_Container.Controls.Add(Me.FNListDocumentTrackPIData_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.StartPayment_lbl)
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
        Me.DockPanel1_Container.Location = New System.Drawing.Point(2, 27)
        Me.DockPanel1_Container.Name = "DockPanel1_Container"
        Me.DockPanel1_Container.Size = New System.Drawing.Size(1314, 125)
        Me.DockPanel1_Container.TabIndex = 0
        '
        'chknewupload
        '
        Me.chknewupload.EditValue = "0"
        Me.chknewupload.Location = New System.Drawing.Point(640, 34)
        Me.chknewupload.Name = "chknewupload"
        Me.chknewupload.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.chknewupload.Properties.Appearance.Options.UseForeColor = True
        Me.chknewupload.Properties.Caption = "New Vender Upload"
        Me.chknewupload.Properties.ValueChecked = "1"
        Me.chknewupload.Properties.ValueUnchecked = "0"
        Me.chknewupload.Size = New System.Drawing.Size(217, 20)
        Me.chknewupload.TabIndex = 310
        '
        'FTEndPayment
        '
        Me.FTEndPayment.EditValue = Nothing
        Me.FTEndPayment.EnterMoveNextControl = True
        Me.FTEndPayment.Location = New System.Drawing.Point(478, 78)
        Me.FTEndPayment.Name = "FTEndPayment"
        Me.FTEndPayment.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTEndPayment.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FTEndPayment.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.FTEndPayment.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTEndPayment.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTEndPayment.Properties.NullDate = ""
        Me.FTEndPayment.Size = New System.Drawing.Size(130, 21)
        Me.FTEndPayment.TabIndex = 8
        Me.FTEndPayment.Tag = "2|"
        '
        'EndPayment_lbl
        '
        Me.EndPayment_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.EndPayment_lbl.Appearance.Options.UseForeColor = True
        Me.EndPayment_lbl.Appearance.Options.UseTextOptions = True
        Me.EndPayment_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.EndPayment_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.EndPayment_lbl.Location = New System.Drawing.Point(295, 77)
        Me.EndPayment_lbl.Name = "EndPayment_lbl"
        Me.EndPayment_lbl.Size = New System.Drawing.Size(181, 19)
        Me.EndPayment_lbl.TabIndex = 275
        Me.EndPayment_lbl.Tag = "2|"
        Me.EndPayment_lbl.Text = "End Payment Date :"
        '
        'ochkselectall
        '
        Me.ochkselectall.Location = New System.Drawing.Point(27, 103)
        Me.ochkselectall.Name = "ochkselectall"
        Me.ochkselectall.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.ochkselectall.Properties.Appearance.Options.UseForeColor = True
        Me.ochkselectall.Properties.Caption = "Select All"
        Me.ochkselectall.Size = New System.Drawing.Size(190, 20)
        Me.ochkselectall.TabIndex = 309
        '
        'FTStartPayment
        '
        Me.FTStartPayment.EditValue = Nothing
        Me.FTStartPayment.EnterMoveNextControl = True
        Me.FTStartPayment.Location = New System.Drawing.Point(159, 78)
        Me.FTStartPayment.Name = "FTStartPayment"
        Me.FTStartPayment.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTStartPayment.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FTStartPayment.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.FTStartPayment.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTStartPayment.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTStartPayment.Properties.NullDate = ""
        Me.FTStartPayment.Size = New System.Drawing.Size(130, 21)
        Me.FTStartPayment.TabIndex = 7
        Me.FTStartPayment.Tag = "2|"
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
        'StartPayment_lbl
        '
        Me.StartPayment_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.StartPayment_lbl.Appearance.Options.UseForeColor = True
        Me.StartPayment_lbl.Appearance.Options.UseTextOptions = True
        Me.StartPayment_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.StartPayment_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.StartPayment_lbl.Location = New System.Drawing.Point(8, 77)
        Me.StartPayment_lbl.Name = "StartPayment_lbl"
        Me.StartPayment_lbl.Size = New System.Drawing.Size(149, 19)
        Me.StartPayment_lbl.TabIndex = 273
        Me.StartPayment_lbl.Tag = "2|"
        Me.StartPayment_lbl.Text = "Start Payment Date :"
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
        Me.FNListDocumentTrackPIData.Size = New System.Drawing.Size(288, 21)
        Me.FNListDocumentTrackPIData.TabIndex = 0
        Me.FNListDocumentTrackPIData.Tag = "9|"
        '
        'FNHSysSuplId
        '
        Me.FNHSysSuplId.EnterMoveNextControl = True
        Me.FNHSysSuplId.Location = New System.Drawing.Point(605, 7)
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
        Me.FNHSysSuplId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", 15, True, True, False, EditorButtonImageOptions1, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, SerializableAppearanceObject2, SerializableAppearanceObject3, SerializableAppearanceObject4, "", "99", Nothing, DevExpress.Utils.ToolTipAnchor.[Default])})
        Me.FNHSysSuplId.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.FNHSysSuplId.Properties.MaxLength = 30
        Me.FNHSysSuplId.Size = New System.Drawing.Size(131, 21)
        Me.FNHSysSuplId.TabIndex = 1
        Me.FNHSysSuplId.Tag = "2|"
        '
        'FNHSysSuplId_lbl
        '
        Me.FNHSysSuplId_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FNHSysSuplId_lbl.Appearance.Options.UseForeColor = True
        Me.FNHSysSuplId_lbl.Appearance.Options.UseTextOptions = True
        Me.FNHSysSuplId_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysSuplId_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysSuplId_lbl.Location = New System.Drawing.Point(455, 7)
        Me.FNHSysSuplId_lbl.Name = "FNHSysSuplId_lbl"
        Me.FNHSysSuplId_lbl.Size = New System.Drawing.Size(149, 19)
        Me.FNHSysSuplId_lbl.TabIndex = 264
        Me.FNHSysSuplId_lbl.Tag = "2|"
        Me.FNHSysSuplId_lbl.Text = "Supplier :"
        '
        'FNHSysSuplId_None
        '
        Me.FNHSysSuplId_None.EnterMoveNextControl = True
        Me.FNHSysSuplId_None.Location = New System.Drawing.Point(740, 7)
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
        Me.FNHSysSuplId_None.Size = New System.Drawing.Size(414, 21)
        Me.FNHSysSuplId_None.TabIndex = 2
        Me.FNHSysSuplId_None.TabStop = False
        Me.FNHSysSuplId_None.Tag = "2|"
        '
        'FTEndPurchaseDate
        '
        Me.FTEndPurchaseDate.EditValue = Nothing
        Me.FTEndPurchaseDate.EnterMoveNextControl = True
        Me.FTEndPurchaseDate.Location = New System.Drawing.Point(478, 31)
        Me.FTEndPurchaseDate.Name = "FTEndPurchaseDate"
        Me.FTEndPurchaseDate.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTEndPurchaseDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FTEndPurchaseDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.FTEndPurchaseDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTEndPurchaseDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTEndPurchaseDate.Properties.NullDate = ""
        Me.FTEndPurchaseDate.Size = New System.Drawing.Size(130, 21)
        Me.FTEndPurchaseDate.TabIndex = 4
        Me.FTEndPurchaseDate.Tag = "2|"
        '
        'FTEndDelivery
        '
        Me.FTEndDelivery.EditValue = Nothing
        Me.FTEndDelivery.EnterMoveNextControl = True
        Me.FTEndDelivery.Location = New System.Drawing.Point(478, 54)
        Me.FTEndDelivery.Name = "FTEndDelivery"
        Me.FTEndDelivery.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTEndDelivery.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FTEndDelivery.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.FTEndDelivery.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTEndDelivery.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTEndDelivery.Properties.NullDate = ""
        Me.FTEndDelivery.Size = New System.Drawing.Size(130, 21)
        Me.FTEndDelivery.TabIndex = 6
        Me.FTEndDelivery.Tag = "2|"
        '
        'FTEndPurchaseDate_lbl
        '
        Me.FTEndPurchaseDate_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTEndPurchaseDate_lbl.Appearance.Options.UseForeColor = True
        Me.FTEndPurchaseDate_lbl.Appearance.Options.UseTextOptions = True
        Me.FTEndPurchaseDate_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTEndPurchaseDate_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTEndPurchaseDate_lbl.Location = New System.Drawing.Point(295, 30)
        Me.FTEndPurchaseDate_lbl.Name = "FTEndPurchaseDate_lbl"
        Me.FTEndPurchaseDate_lbl.Size = New System.Drawing.Size(181, 19)
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
        Me.FTEndDelivery_lbl.Location = New System.Drawing.Point(295, 53)
        Me.FTEndDelivery_lbl.Name = "FTEndDelivery_lbl"
        Me.FTEndDelivery_lbl.Size = New System.Drawing.Size(181, 19)
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
        Me.FTStartPurchaseDate.Size = New System.Drawing.Size(130, 21)
        Me.FTStartPurchaseDate.TabIndex = 3
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
        Me.FTStartPurchaseDate_lbl.Text = "Start Purchase Date :"
        '
        'FTStartDelivery
        '
        Me.FTStartDelivery.EditValue = Nothing
        Me.FTStartDelivery.EnterMoveNextControl = True
        Me.FTStartDelivery.Location = New System.Drawing.Point(159, 54)
        Me.FTStartDelivery.Name = "FTStartDelivery"
        Me.FTStartDelivery.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTStartDelivery.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FTStartDelivery.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.FTStartDelivery.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTStartDelivery.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTStartDelivery.Properties.NullDate = ""
        Me.FTStartDelivery.Size = New System.Drawing.Size(130, 21)
        Me.FTStartDelivery.TabIndex = 5
        Me.FTStartDelivery.Tag = "2|"
        '
        'FTStartDelivery_lbl
        '
        Me.FTStartDelivery_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTStartDelivery_lbl.Appearance.Options.UseForeColor = True
        Me.FTStartDelivery_lbl.Appearance.Options.UseTextOptions = True
        Me.FTStartDelivery_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTStartDelivery_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTStartDelivery_lbl.Location = New System.Drawing.Point(8, 53)
        Me.FTStartDelivery_lbl.Name = "FTStartDelivery_lbl"
        Me.FTStartDelivery_lbl.Size = New System.Drawing.Size(149, 19)
        Me.FTStartDelivery_lbl.TabIndex = 269
        Me.FTStartDelivery_lbl.Tag = "2|"
        Me.FTStartDelivery_lbl.Text = "Start Delivery Date :"
        '
        'ogbdetail
        '
        Me.ogbdetail.Controls.Add(Me.PopupPDF)
        Me.ogbdetail.Controls.Add(Me.ogbmainprocbutton)
        Me.ogbdetail.Controls.Add(Me.ogdtime)
        Me.ogbdetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogbdetail.Location = New System.Drawing.Point(0, 155)
        Me.ogbdetail.Name = "ogbdetail"
        Me.ogbdetail.ShowCaption = False
        Me.ogbdetail.Size = New System.Drawing.Size(1318, 602)
        Me.ogbdetail.TabIndex = 0
        '
        'ogbmainprocbutton
        '
        Me.ogbmainprocbutton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmpreviewpisummary)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmeditpi)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmshowamount)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmsavepipayment)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmsavepaid)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmFTStateSendPIToAcc)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmFTStateFinishPO)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmFTStatePaid)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmFTETA)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmFTETD)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmFTInvoiceNo)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmFTNote)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmFTBaseOnLeadTimeDeliveryDate)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmFTImpactedGacDate)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmFTWarehouseDate)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmFTFacCheckCFMDeliveryDateFinal)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmFTFacCheckCFMDeliveryDate2)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmFTFacCheckCFMDeliveryDate1)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmsavetrack)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmsavepi)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmsendmail)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmsavelayout)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmexit)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmclear)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmload)
        Me.ogbmainprocbutton.Location = New System.Drawing.Point(111, 298)
        Me.ogbmainprocbutton.Name = "ogbmainprocbutton"
        Me.ogbmainprocbutton.Size = New System.Drawing.Size(1096, 276)
        Me.ogbmainprocbutton.TabIndex = 386
        Me.ogbmainprocbutton.Tag = "2|"
        '
        'ocmpreviewpisummary
        '
        Me.ocmpreviewpisummary.Location = New System.Drawing.Point(841, 124)
        Me.ocmpreviewpisummary.Name = "ocmpreviewpisummary"
        Me.ocmpreviewpisummary.Size = New System.Drawing.Size(180, 25)
        Me.ocmpreviewpisummary.TabIndex = 353
        Me.ocmpreviewpisummary.TabStop = False
        Me.ocmpreviewpisummary.Tag = "2|"
        Me.ocmpreviewpisummary.Text = "PREVIEW PI SUMMARY"
        '
        'ocmeditpi
        '
        Me.ocmeditpi.Location = New System.Drawing.Point(390, 47)
        Me.ocmeditpi.Name = "ocmeditpi"
        Me.ocmeditpi.Size = New System.Drawing.Size(111, 25)
        Me.ocmeditpi.TabIndex = 352
        Me.ocmeditpi.TabStop = False
        Me.ocmeditpi.Tag = "2|"
        Me.ocmeditpi.Text = "Edit PI"
        '
        'ocmshowamount
        '
        Me.ocmshowamount.Location = New System.Drawing.Point(24, 171)
        Me.ocmshowamount.Name = "ocmshowamount"
        Me.ocmshowamount.Size = New System.Drawing.Size(275, 25)
        Me.ocmshowamount.TabIndex = 351
        Me.ocmshowamount.TabStop = False
        Me.ocmshowamount.Tag = "2|"
        Me.ocmshowamount.Text = "Show Price"
        Me.ocmshowamount.Visible = False
        '
        'ocmsavepipayment
        '
        Me.ocmsavepipayment.Location = New System.Drawing.Point(627, 47)
        Me.ocmsavepipayment.Name = "ocmsavepipayment"
        Me.ocmsavepipayment.Size = New System.Drawing.Size(111, 25)
        Me.ocmsavepipayment.TabIndex = 350
        Me.ocmsavepipayment.TabStop = False
        Me.ocmsavepipayment.Tag = "2|"
        Me.ocmsavepipayment.Text = "Save PI Payment"
        '
        'ocmsavepaid
        '
        Me.ocmsavepaid.Location = New System.Drawing.Point(884, 47)
        Me.ocmsavepaid.Name = "ocmsavepaid"
        Me.ocmsavepaid.Size = New System.Drawing.Size(111, 25)
        Me.ocmsavepaid.TabIndex = 349
        Me.ocmsavepaid.TabStop = False
        Me.ocmsavepaid.Tag = "2|"
        Me.ocmsavepaid.Text = "Save Paid"
        '
        'ocmFTStateSendPIToAcc
        '
        Me.ocmFTStateSendPIToAcc.Location = New System.Drawing.Point(24, 109)
        Me.ocmFTStateSendPIToAcc.Name = "ocmFTStateSendPIToAcc"
        Me.ocmFTStateSendPIToAcc.Size = New System.Drawing.Size(275, 25)
        Me.ocmFTStateSendPIToAcc.TabIndex = 348
        Me.ocmFTStateSendPIToAcc.TabStop = False
        Me.ocmFTStateSendPIToAcc.Tag = "2|"
        Me.ocmFTStateSendPIToAcc.Text = "Save FTStateSendPIToAcc"
        Me.ocmFTStateSendPIToAcc.Visible = False
        '
        'ocmFTStateFinishPO
        '
        Me.ocmFTStateFinishPO.Location = New System.Drawing.Point(24, 78)
        Me.ocmFTStateFinishPO.Name = "ocmFTStateFinishPO"
        Me.ocmFTStateFinishPO.Size = New System.Drawing.Size(275, 25)
        Me.ocmFTStateFinishPO.TabIndex = 347
        Me.ocmFTStateFinishPO.TabStop = False
        Me.ocmFTStateFinishPO.Tag = "2|"
        Me.ocmFTStateFinishPO.Text = "Save FTStateFinishPO"
        Me.ocmFTStateFinishPO.Visible = False
        '
        'ocmFTStatePaid
        '
        Me.ocmFTStatePaid.Location = New System.Drawing.Point(507, 202)
        Me.ocmFTStatePaid.Name = "ocmFTStatePaid"
        Me.ocmFTStatePaid.Size = New System.Drawing.Size(231, 25)
        Me.ocmFTStatePaid.TabIndex = 346
        Me.ocmFTStatePaid.TabStop = False
        Me.ocmFTStatePaid.Tag = "2|"
        Me.ocmFTStatePaid.Text = "FTStatePaid"
        Me.ocmFTStatePaid.Visible = False
        '
        'ocmFTETA
        '
        Me.ocmFTETA.Location = New System.Drawing.Point(507, 171)
        Me.ocmFTETA.Name = "ocmFTETA"
        Me.ocmFTETA.Size = New System.Drawing.Size(231, 25)
        Me.ocmFTETA.TabIndex = 345
        Me.ocmFTETA.TabStop = False
        Me.ocmFTETA.Tag = "2|"
        Me.ocmFTETA.Text = "FTETA"
        Me.ocmFTETA.Visible = False
        '
        'ocmFTETD
        '
        Me.ocmFTETD.Location = New System.Drawing.Point(507, 140)
        Me.ocmFTETD.Name = "ocmFTETD"
        Me.ocmFTETD.Size = New System.Drawing.Size(231, 25)
        Me.ocmFTETD.TabIndex = 344
        Me.ocmFTETD.TabStop = False
        Me.ocmFTETD.Tag = "2|"
        Me.ocmFTETD.Text = "FTETD"
        Me.ocmFTETD.Visible = False
        '
        'ocmFTInvoiceNo
        '
        Me.ocmFTInvoiceNo.Location = New System.Drawing.Point(507, 109)
        Me.ocmFTInvoiceNo.Name = "ocmFTInvoiceNo"
        Me.ocmFTInvoiceNo.Size = New System.Drawing.Size(231, 25)
        Me.ocmFTInvoiceNo.TabIndex = 343
        Me.ocmFTInvoiceNo.TabStop = False
        Me.ocmFTInvoiceNo.Tag = "2|"
        Me.ocmFTInvoiceNo.Text = "FTInvoiceNo"
        Me.ocmFTInvoiceNo.Visible = False
        '
        'ocmFTNote
        '
        Me.ocmFTNote.Location = New System.Drawing.Point(507, 78)
        Me.ocmFTNote.Name = "ocmFTNote"
        Me.ocmFTNote.Size = New System.Drawing.Size(231, 25)
        Me.ocmFTNote.TabIndex = 342
        Me.ocmFTNote.TabStop = False
        Me.ocmFTNote.Tag = "2|"
        Me.ocmFTNote.Text = "FTNote"
        Me.ocmFTNote.Visible = False
        '
        'ocmFTBaseOnLeadTimeDeliveryDate
        '
        Me.ocmFTBaseOnLeadTimeDeliveryDate.Location = New System.Drawing.Point(305, 233)
        Me.ocmFTBaseOnLeadTimeDeliveryDate.Name = "ocmFTBaseOnLeadTimeDeliveryDate"
        Me.ocmFTBaseOnLeadTimeDeliveryDate.Size = New System.Drawing.Size(196, 25)
        Me.ocmFTBaseOnLeadTimeDeliveryDate.TabIndex = 341
        Me.ocmFTBaseOnLeadTimeDeliveryDate.TabStop = False
        Me.ocmFTBaseOnLeadTimeDeliveryDate.Tag = "2|"
        Me.ocmFTBaseOnLeadTimeDeliveryDate.Text = "FTBaseOnLeadTimeDeliveryDate"
        Me.ocmFTBaseOnLeadTimeDeliveryDate.Visible = False
        '
        'ocmFTImpactedGacDate
        '
        Me.ocmFTImpactedGacDate.Location = New System.Drawing.Point(305, 202)
        Me.ocmFTImpactedGacDate.Name = "ocmFTImpactedGacDate"
        Me.ocmFTImpactedGacDate.Size = New System.Drawing.Size(196, 25)
        Me.ocmFTImpactedGacDate.TabIndex = 340
        Me.ocmFTImpactedGacDate.TabStop = False
        Me.ocmFTImpactedGacDate.Tag = "2|"
        Me.ocmFTImpactedGacDate.Text = "FTImpactedGacDate"
        Me.ocmFTImpactedGacDate.Visible = False
        '
        'ocmFTWarehouseDate
        '
        Me.ocmFTWarehouseDate.Location = New System.Drawing.Point(305, 171)
        Me.ocmFTWarehouseDate.Name = "ocmFTWarehouseDate"
        Me.ocmFTWarehouseDate.Size = New System.Drawing.Size(196, 25)
        Me.ocmFTWarehouseDate.TabIndex = 339
        Me.ocmFTWarehouseDate.TabStop = False
        Me.ocmFTWarehouseDate.Tag = "2|"
        Me.ocmFTWarehouseDate.Text = "FTWarehouseDate"
        Me.ocmFTWarehouseDate.Visible = False
        '
        'ocmFTFacCheckCFMDeliveryDateFinal
        '
        Me.ocmFTFacCheckCFMDeliveryDateFinal.Location = New System.Drawing.Point(306, 140)
        Me.ocmFTFacCheckCFMDeliveryDateFinal.Name = "ocmFTFacCheckCFMDeliveryDateFinal"
        Me.ocmFTFacCheckCFMDeliveryDateFinal.Size = New System.Drawing.Size(195, 25)
        Me.ocmFTFacCheckCFMDeliveryDateFinal.TabIndex = 338
        Me.ocmFTFacCheckCFMDeliveryDateFinal.TabStop = False
        Me.ocmFTFacCheckCFMDeliveryDateFinal.Tag = "2|"
        Me.ocmFTFacCheckCFMDeliveryDateFinal.Text = "FTFacCheckCFMDeliveryDateFinal"
        Me.ocmFTFacCheckCFMDeliveryDateFinal.Visible = False
        '
        'ocmFTFacCheckCFMDeliveryDate2
        '
        Me.ocmFTFacCheckCFMDeliveryDate2.Location = New System.Drawing.Point(306, 109)
        Me.ocmFTFacCheckCFMDeliveryDate2.Name = "ocmFTFacCheckCFMDeliveryDate2"
        Me.ocmFTFacCheckCFMDeliveryDate2.Size = New System.Drawing.Size(195, 25)
        Me.ocmFTFacCheckCFMDeliveryDate2.TabIndex = 337
        Me.ocmFTFacCheckCFMDeliveryDate2.TabStop = False
        Me.ocmFTFacCheckCFMDeliveryDate2.Tag = "2|"
        Me.ocmFTFacCheckCFMDeliveryDate2.Text = "FTFacCheckCFMDeliveryDate2"
        Me.ocmFTFacCheckCFMDeliveryDate2.Visible = False
        '
        'ocmFTFacCheckCFMDeliveryDate1
        '
        Me.ocmFTFacCheckCFMDeliveryDate1.Location = New System.Drawing.Point(305, 78)
        Me.ocmFTFacCheckCFMDeliveryDate1.Name = "ocmFTFacCheckCFMDeliveryDate1"
        Me.ocmFTFacCheckCFMDeliveryDate1.Size = New System.Drawing.Size(196, 25)
        Me.ocmFTFacCheckCFMDeliveryDate1.TabIndex = 336
        Me.ocmFTFacCheckCFMDeliveryDate1.TabStop = False
        Me.ocmFTFacCheckCFMDeliveryDate1.Tag = "2|"
        Me.ocmFTFacCheckCFMDeliveryDate1.Text = "FTFacCheckCFMDeliveryDate1"
        Me.ocmFTFacCheckCFMDeliveryDate1.Visible = False
        '
        'ocmsavetrack
        '
        Me.ocmsavetrack.Location = New System.Drawing.Point(767, 47)
        Me.ocmsavetrack.Name = "ocmsavetrack"
        Me.ocmsavetrack.Size = New System.Drawing.Size(111, 25)
        Me.ocmsavetrack.TabIndex = 335
        Me.ocmsavetrack.TabStop = False
        Me.ocmsavetrack.Tag = "2|"
        Me.ocmsavetrack.Text = "Save Track"
        '
        'ocmsavepi
        '
        Me.ocmsavepi.Location = New System.Drawing.Point(507, 47)
        Me.ocmsavepi.Name = "ocmsavepi"
        Me.ocmsavepi.Size = New System.Drawing.Size(111, 25)
        Me.ocmsavepi.TabIndex = 334
        Me.ocmsavepi.TabStop = False
        Me.ocmsavepi.Tag = "2|"
        Me.ocmsavepi.Text = "Save PI"
        '
        'ocmsendmail
        '
        Me.ocmsendmail.Location = New System.Drawing.Point(240, 47)
        Me.ocmsendmail.Name = "ocmsendmail"
        Me.ocmsendmail.Size = New System.Drawing.Size(111, 25)
        Me.ocmsendmail.TabIndex = 333
        Me.ocmsendmail.TabStop = False
        Me.ocmsendmail.Tag = "2|"
        Me.ocmsendmail.Text = "E-MAIL"
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
        'oDockManager
        '
        Me.oDockManager.Form = Me
        Me.oDockManager.RootPanels.AddRange(New DevExpress.XtraBars.Docking.DockPanel() {Me.ogbheader})
        Me.oDockManager.TopZIndexControls.AddRange(New String() {"DevExpress.XtraBars.BarDockControl", "DevExpress.XtraBars.StandaloneBarDockControl", "System.Windows.Forms.StatusBar", "DevExpress.XtraBars.Ribbon.RibbonStatusBar", "DevExpress.XtraBars.Ribbon.RibbonControl"})
        '
        'wPurchaseTrackingPI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1318, 757)
        Me.Controls.Add(Me.PopupPDFPI)
        Me.Controls.Add(Me.ogbdetail)
        Me.Controls.Add(Me.ogbheader)
        Me.Name = "wPurchaseTrackingPI"
        Me.Text = "Purchase Order Status Tracking "
        CType(Me.ogvlist, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepCheckEdit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerPDFPI, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PopupPDFPI, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PopupPDFPI.ResumeLayout(False)
        CType(Me.ogdtime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvtime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemDateEdit1.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemDateEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemMemoExFTNote, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemTextFTInvoiceNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerPDF, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PopupPDF, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PopupPDF.ResumeLayout(False)
        Me.ogbheader.ResumeLayout(False)
        Me.DockPanel1_Container.ResumeLayout(False)
        CType(Me.chknewupload.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTEndPayment.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTEndPayment.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ochkselectall.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTStartPayment.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTStartPayment.Properties, System.ComponentModel.ISupportInitialize).EndInit()
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
        CType(Me.ogbdetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbdetail.ResumeLayout(False)
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbmainprocbutton.ResumeLayout(False)
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
    Friend WithEvents FNRCVQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTEndDelivery As DevExpress.XtraEditors.DateEdit
    Friend WithEvents FTEndDelivery_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTStartDelivery As DevExpress.XtraEditors.DateEdit
    Friend WithEvents FTStartDelivery_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FDDeliveryDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FDPurchaseDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTSuplCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTSuplName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents xFNPOOutBalQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTStateSendApp As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepCheckEdit As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents FTSendAppDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTStateSuperVisorApp As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTSuperVisorAppDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTStateManagerApp As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTSuperManagerAppDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTEndPurchaseDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents FTEndPurchaseDate_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTStartPurchaseDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents FTStartPurchaseDate_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysSuplId As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FNHSysSuplId_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysSuplId_None As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FTStateSuperVisorReject As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTStateManagerReject As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTPurchaseBy As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNRevisedSeq As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTRevisedDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTPurchaseRevisedBy As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNHSysDeliveryID As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cxFTPOCustPO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents xFTPOOrderNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents xFTPOStyleCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTSelect As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ocmsendmail As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmFTFacCheckCFMDeliveryDate1 As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmsavetrack As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmsavepi As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FNListDocumentTrackPIData As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents FNListDocumentTrackPIData_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTStateClosed As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTClosedBy As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTClosedDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTClosedTime As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTStateFinishPO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTSendFinishPOBy As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTSendFinishPODate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTSendFinishPOTime As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTStateSendPIToAcc As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTSendPIToAccBy As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTSendPIToAccDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTSendPIToAccTime As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTPINoBy As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTPINoDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTPINoTime As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTPINo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTPIDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTRcvPIDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTPISuplCFMDeliveryDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNPIQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNPINetAmt As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNPIPayType As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTPIRemark As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNLeadTime As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTFacCheckCFMDeliveryDate1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTFacCheckCFMDeliveryDate1By As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTFacCheckCFMDeliveryDate1Date As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTFacCheckCFMDeliveryDate1Time As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTFacCheckCFMDeliveryDate2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTFacCheckCFMDeliveryDate2By As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTFacCheckCFMDeliveryDate2Date As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTFacCheckCFMDeliveryDate2Time As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTFacCheckCFMDeliveryDateFinal As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTFacCheckCFMDeliveryDateFinalBy As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTFacCheckCFMDeliveryDateFinalDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTFacCheckCFMDeliveryDateFinalTime As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTWarehouseDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTWarehouseDateBy As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTWarehouseDateDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTWarehouseDateTime As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTImpactedGacDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTImpactedGacDatey As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTImpactedGacDateDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTImpactedGacDateTime As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTBaseOnLeadTimeDeliveryDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTBaseOnLeadTimeDeliveryDateBy As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTBaseOnLeadTimeDeliveryDateDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTBaseOnLeadTimeDeliveryDateTime As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTNote As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTNoteBy As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTNoteDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTNoteTime As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTInvoiceNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTInvoiceNoBy As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTInvoiceNoDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTInvoiceNoTime As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTETD As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTETDBy As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTETDDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTETDTime As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTETA As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTETABy As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTETADate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTETATime As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTPIPayTypeBy As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTPIPayTypeDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTPIPayTypeTime As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents xFTStatePaid As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTPaidDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTPaidNote As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTStatePaidBy As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTStatePaidDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTStatePaidTime As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNTrackSeq As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTTrackDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTTrackBy As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTContactName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTTrackNote As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemDateEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemDateEdit
    Friend WithEvents ocmFTWarehouseDate As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmFTFacCheckCFMDeliveryDateFinal As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmFTFacCheckCFMDeliveryDate2 As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmFTStateSendPIToAcc As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmFTStateFinishPO As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmFTStatePaid As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmFTETA As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmFTETD As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmFTInvoiceNo As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmFTNote As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmFTBaseOnLeadTimeDeliveryDate As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmFTImpactedGacDate As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmsavepaid As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents cxFNPOGrandAmt As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents xFNHSysSuplId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cxFNPoState As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cxFNPIPOQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cxFNPIPONetAmt As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ocmshowamount As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmsavepipayment As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents RepositoryItemMemoExFTNote As DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit
    Friend WithEvents RepositoryItemTextFTInvoiceNo As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents ochkselectall As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents FTStateSendMail As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTSendMailBy As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTSendMailDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTSendMailTime As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNFNExtendLT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CXFTCmpCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTBuyCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNOrderQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ocmeditpi As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents xFNPIGrandNetAmt As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents zFNCNAmt As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNDNAmt As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents zFNSurchargeAmt As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents zFNPIGrandTotalAmt As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents zFNPOBalQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents zFNPOBalGrandAmt As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents xFTCurCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents xFTPIPayDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents xmFTPIPayTypeRemark As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ocmpreviewpisummary As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FTEndPayment As DevExpress.XtraEditors.DateEdit
    Friend WithEvents EndPayment_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTStartPayment As DevExpress.XtraEditors.DateEdit
    Friend WithEvents StartPayment_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTLCNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTStateFile As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents sFNFileID As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents sPDF As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents PopupPDF As DevExpress.XtraEditors.PopupContainerControl
    Friend WithEvents FilePdfViewer As DevExpress.XtraPdfViewer.PdfViewer
    Friend WithEvents RepositoryItemPopupContainerPDF As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents FTStatePIPDFFile As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents SSPIPDF As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents chknewupload As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents FTStateNewVenderUpload As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ogvlist As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents ssFTPINo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ssFTPIDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ssFTRcvPIDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents sssFTPISuplCFMDeliveryDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNPIPOQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ssFTStatePaid As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents sasFTPaidDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ssFTPaidNote As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ssFTLCNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ssFTStateFile As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ssFNFileID As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ssPDF As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ssFTStatePIPDFFile As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents sssssPIPDF As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents sFTPurchaseNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents sssFTStateNewVenderUpload As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemPopupContainerPDFPI As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents PopupPDFPI As DevExpress.XtraEditors.PopupContainerControl
    Friend WithEvents FilePdfViewerPI As DevExpress.XtraPdfViewer.PdfViewer
End Class

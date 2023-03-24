Imports System.Data
Imports DevExpress.XtraGrid.GridControl
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.Data
Imports System.IO
Imports DevExpress.XtraGrid.Columns



Public Class wSMPOrderStatusTrack

    Private GridDataBefore As String = ""
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        'SetColMaster()
        With ReposDate

            RemoveHandler .Leave, AddressOf HI.TL.HandlerControl.RepositoryItemDate_Leave
            AddHandler .Click, AddressOf HI.TL.HandlerControl.RepositoryItemDate_GotFocus
            AddHandler .Leave, AddressOf ItemDate_Leave
            AddHandler .Click, AddressOf ItemDate_GotFocus

        End With

    End Sub

#Region "Property"
    Private _CallMenuName As String = ""
    Public Property CallMenuName As String
        Get
            Return _CallMenuName
        End Get
        Set(ByVal value As String)
            _CallMenuName = value
        End Set
    End Property

    Private _CallMethodName As String = ""
    Public Property CallMethodName As String
        Get
            Return _CallMethodName
        End Get
        Set(ByVal value As String)
            _CallMethodName = value
        End Set
    End Property

    Private _CallMethodParm As String = ""
    Public Property CallMethodParm As String
        Get
            Return _CallMethodParm
        End Get
        Set(ByVal value As String)
            _CallMethodParm = value
        End Set
    End Property
#End Region


#Region "Procedure"

    Public Sub LoadDataInfo(OrderKey As String, OrderSubKey As String)
        Call LoadData()
    End Sub



    Private Sub LoadData()
        Dim Qry As String = ""




        Qry = " SELECT DISTINCT convert(varchar(100), convert(date , SD.FTProGacDate) ,106) AS FTProGacDate  ,convert(varchar(100), convert(date , O.FDSMPOrderDate) ,106 )  AS  FDSMPOrderDate,  "
        Qry &= vbCrLf & "O.FTSMPOrderNo AS SampleOrderNo,S.FTStyleCode,convert(varchar(100), convert(date , SD.FDSuplCfmDliDate  ),106)  AS FDSuplCfmDliDate,convert(varchar(100), convert(date ,PO.FDDeliveryDate  ),106)  AS FDDeliveryDate"
        Qry &= vbCrLf & ",RV.FTReserveNo AS ReservedNo,convert(varchar(100), convert(date , RV.FDReserveDate) ,106 )  AS FDReserveDate ,O.FTStateFree,FPO.FTPurchaseNo AS FREEPurchaseNo,convert(varchar(100), convert(date , FPO.FDPurchaseDate  ),106)  AS FREEPurchaseDate,FPO.FNQuantity AS FREEQuantity"
        Qry &= vbCrLf & ",O.FTMatPart,O.PART,O.N,R.FTRawMatCode AS MaterialCode,O.Mat AS asd,R.FTRawMatColorCode AS MaterialColorCode,R.FTRawMatColorNameTH AS FTMatColorName,R.FTRawMatSizeCode AS MaterialSizeCode"
        Qry &= vbCrLf & ",O.FNMatQuantity AS UsedQty,U.FNHSysUnitId,U.FTUnitCode AS Unit ,SP.FNHSysSuplId,SP.FTSuplCode,PA.FTPartName,ISNULL(RV.FNQuantity,'0') AS ReservedQty"
        Qry &= vbCrLf & ",O.FTSMPOrderBy,C.FTCmpCode,SS.FTSeasonCode,B.FTColorway, convert(varchar(100), convert(date ,B.FTPatternDate  ),106)   AS FTPatternDate"
        Qry &= vbCrLf & ",H.FTTransferWHNo,convert(varchar(100), convert(date , H.FDApproveDate  ),106)  AS FDTransferWHDate,ISNULL(H.FNQuantity,'0') AS FTTransferWHQty,PO.FTPurchaseNo,convert(varchar(100), convert(date , PO.FDPurchaseDate) ,106 )  AS FDPurchaseDate,ISNULL(PO.FNQuantity,'0') AS PurchaseQty"
        Qry &= vbCrLf & ",PRD.FTReceiveNo AS SMPReceiveNo,convert(varchar(100), convert(date , PRD.FDReceiveDate) ,106 )  AS FDReceiveDate,ISNULL(PRD.FNQuantity,'0') AS SMPReceiveQty"
        Qry &= vbCrLf & ",PIS.FTIssueNo AS SMPIssueNo, convert(varchar(100), convert(date , PIS.FDIssueDate) ,106 )  AS FDIssueDate,ISNULL(PIS.FNQuantity,'0') AS SMPIssueQty"
        Qry &= vbCrLf & ",RT.FTReturnStockNo AS SMPReturnToStockNo,convert(varchar(100), convert(date , RT.FDReturnStockDate) ,106 )  AS FDReturnStockDate ,ISNULL(RT.FNQuantity,'0')AS SMPReturnToStockQty"
        Qry &= vbCrLf & ",RS.FTReturnSuplNo AS SMPReturnToSuplNo ,convert(varchar(100), convert(date , RS.FDReturnSuplDate) ,106 )  AS FDReturnSuplDate,ISNULL(RS.FNQuantity,'0') AS SMPReturnToSuplQty"
        Qry &= vbCrLf & ",AJ.FTAdjustStockNo ,convert(varchar(100), convert(date , AJ.FDAdjustStockDate) ,106 )  AS FDAdjustStockDate,ISNULL(AJ.FNQuantity,'0') AS FTAdjustStockQty,O.FTPgmName"
        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            Qry &= vbCrLf & ",R.FTRawMatColorNameTH AS FTMatColorName,SP.FTSuplNameTH AS FTSuplName,R.FTRawMatNameTH AS MaterialDescription,L.FTNameTH AS JobType"
        Else
            Qry &= vbCrLf & ",R.FTRawMatColorNameEN AS FTMatColorName,SP.FTSuplNameEN AS FTSuplName,R.FTRawMatNameEN AS MaterialDescription,L.FTNameEN AS JobType"
        End If

        Qry &= vbCrLf & "FROM"
        Qry &= vbCrLf & "(SELECT F.FTSMPOrderNo,F.FNHSysRawmatId AS Mat ,O.FNHSysStyleId,O.FNHSysSeasonId,F.FTMatColor ,F.FTMatColorName "
        Qry &= vbCrLf & ",F.FTMatSize ,F.FNMatQuantity ,F.FNHSysUnitId ,O.FNHSysCustId,O.FNHSysMerTeamId,F.FNHSysSuplId ,O.FTSMPOrderBy,"
        Qry &= vbCrLf & " O.FNSMPOrderType, O.FNHSysCmpId, O.FDSMPOrderDate, F.FTStateFree,F.FNMatSeq,O.FTPgmName,O.FTStateReceiptDate"
        Qry &= vbCrLf & ",CASE WHEN ISNULL(F.FNMatSeq,'') = '0'  THEN 'A' + char(30) ELSE  '' END "
        Qry &= vbCrLf & "+   CASE WHEN ISNULL(F.FNMatSeq,'')='1' THEN 'B' ELSE '' END"
        Qry &= vbCrLf & "+   CASE WHEN ISNULL(F.FNMatSeq,'')='2' THEN 'C' ELSE '' END"
        Qry &= vbCrLf & " +   CASE WHEN ISNULL(F.FNMatSeq,'')='3' THEN 'D' ELSE '' END"
        Qry &= vbCrLf & "+   CASE WHEN ISNULL(F.FNMatSeq,'')='4' THEN 'E' ELSE '' END"
        Qry &= vbCrLf & "	+   CASE WHEN ISNULL(F.FNMatSeq,'')='5' THEN 'F' ELSE '' END"
        Qry &= vbCrLf & "	+   CASE WHEN ISNULL(F.FNMatSeq,'')='6' THEN 'G' ELSE '' END"
        Qry &= vbCrLf & "+   CASE WHEN ISNULL(F.FNMatSeq,'')='7' THEN 'H' ELSE '' END AS PART"

        Qry &= vbCrLf & ",CASE WHEN ISNULL(F.FNMatSeq,'') = '0'  THEN  convert( int,'-8')  ELSE  '' END "
        Qry &= vbCrLf & " +   CASE WHEN ISNULL(F.FNMatSeq,'')='1' THEN  convert( int,'-7') ELSE '' END"
        Qry &= vbCrLf & "+   CASE WHEN ISNULL(F.FNMatSeq,'')='2' THEN  convert( int,'-6') ELSE '' END"
        Qry &= vbCrLf & "+   CASE WHEN ISNULL(F.FNMatSeq,'')='3' THEN  convert( int,'-5') ELSE '' END"
        Qry &= vbCrLf & "	+   CASE WHEN ISNULL(F.FNMatSeq,'')='4' THEN  convert( int,'-4') ELSE '' END"
        Qry &= vbCrLf & "	+   CASE WHEN ISNULL(F.FNMatSeq,'')='5' THEN  convert( int,'-3') ELSE '' END"
        Qry &= vbCrLf & "	+   CASE WHEN ISNULL(F.FNMatSeq,'')='6' THEN  convert( int,'-2') ELSE '' END"
        Qry &= vbCrLf & "+   CASE WHEN ISNULL(F.FNMatSeq,'')='7' THEN  convert( int,'-1') ELSE '' END AS N ,'' AS FTMatPart"
        Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPOrder_FabricMatList AS F"
        Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPOrder AS O ON F.FTSMPOrderNo=O.FTSMPOrderNo "
        Qry &= vbCrLf & " UNION"
        Qry &= vbCrLf & "select O.FTSMPOrderNo,FM.FNHSysRawmatId AS Mat ,O.FNHSysStyleId,O.FNHSysSeasonId,FM.FTMatColor,FM.FTMatColorName"
        Qry &= vbCrLf & ",FM.FTMatSize,FM.FNMatQuantity,FM.FNHSysUnitId,O.FNHSysCustId,O.FNHSysMerTeamId,FM.FNHSysSuplId,O.FTSMPOrderBy,O.FNSMPOrderType,O.FNHSysCmpId,O.FDSMPOrderDate"
        Qry &= vbCrLf & ",FM.FTStateFree,FM.FNMatSeq,O.FTPgmName,O.FTStateReceiptDate,  convert( nvarchar(30), FM.FNMatSeq) AS PART,convert( int, FM.FNMatSeq) AS N,FM.FTMatPart"
        Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPOrder AS O LEFT OUTER JOIN"
        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPOrder_MatList AS FM ON O.FTSMPOrderNo=FM.FTSMPOrderNo)AS O "
        Qry &= vbCrLf & "LEFT OUTER JOIN (SELECT D.FTSMPOrderNo,D.FNHSysRawmatId,D.FTMat,D.FTProGacDate,D.FDSuplCfmDliDate"
        Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPOrder_DateSuplPlan AS D)AS SD ON O.FTSMPOrderNo=SD.FTSMPOrderNo AND O.Mat=SD.FNHSysRawmatId"
        Qry &= vbCrLf & "LEFT OUTER JOIN(SELECT M.FNHSysRawMatId,M.FTRawMatCode,M.FTRawMatNameTH,M.FTRawMatNameEN,MM.FTRawMatSizeCode,MC.FTRawMatColorCode,MC.FTRawMatColorNameTH,MC.FTRawMatColorNameEN"
        Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS M WITH(NOLOCK)  LEFT OUTER Join"
        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatSize AS MM WITH(NOLOCK) ON  M.FNHSysRawMatSizeId=MM.FNHSysRawMatSizeId LEFT OUTER Join"
        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatColor AS MC WITH(NOLOCK) ON M.FNHSysRawMatColorId=MC.FNHSysRawMatColorId   )AS R ON O.Mat=R.FNHSysRawMatId "
        Qry &= vbCrLf & "LEFT OUTER JOIN (SELECT S.FNHSysStyleId,S.FTStyleCode FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS S)AS S ON O.FNHSysStyleId=S.FNHSysStyleId"
        Qry &= vbCrLf & "LEFT OUTER JOIN ("
        Qry &= vbCrLf & "SELECT PO.FTOrderNo,P.FTPurchaseNo,P.FDPurchaseDate,P.FDDeliveryDate,P.FNHSysCmpId,PO.FNQuantity,PO.FNHSysRawMatId,PO.FTRawMatColorNameTH"
        Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase AS P "
        Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo AS PO ON P.FTPurchaseNo=PO.FTPurchaseNo"
        Qry &= vbCrLf & ")AS PO ON O.FTSMPOrderNo=PO.FTOrderNo  AND R.FNHSysRawMatId=PO.FNHSysRawMatId AND R.FTRawMatColorNameTH=PO.FTRawMatColorNameTH"
        Qry &= vbCrLf & "LEFT OUTER JOIN("
        Qry &= vbCrLf & "SELECT  RV.FTOrderNo,RV.FTReserveNo ,RV.FDReserveDate ,BO.FNQuantity,M.FNHSysRawMatId,MM.FTMainMatNameTH AS MaterialDescription,M.FNHSysUnitId,ISNULL(MC.FTRawMatColorCode,'')AS FTRawMatColorCode"
        Qry &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReserve AS RV WITH(NOLOCK) LEFT OUTER Join"
        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT AS BO WITH(NOLOCK) ON RV.FTReserveNo = BO.FTDocumentNo    LEFT OUTER Join"
        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode AS B WITH(NOLOCK) ON BO.FTBarcodeNo = B.FTBarcodeNo LEFT OUTER Join"
        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS M WITH(NOLOCK) ON  B.FNHSysRawMatId=M.FNHSysRawMatId LEFT OUTER Join"
        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMainMat AS MM WITH(NOLOCK) ON  M.FTRawMatCode=MM.FTMainMatCode LEFT OUTER Join"
        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatColor AS MC WITH(NOLOCK) ON M.FNHSysRawMatColorId=MC.FNHSysRawMatColorId   "
        Qry &= vbCrLf & ")AS RV ON O.FTSMPOrderNo=RV.FTOrderNo AND O.Mat=RV.FNHSysRawMatId AND O.FTMatColor=RV.FTRawMatColorCode"
        Qry &= vbCrLf & "LEFT OUTER JOIN (SELECT U.FTUnitCode,U.FNHSysUnitId FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit AS U WITH(NOLOCK))AS U ON O.FNHSysUnitId=U.FNHSysUnitId "
        Qry &= vbCrLf & " LEFT OUTER JOIN  (SELECT C.FNHSysCustId,C.FTCustCode FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCustomer AS C) AS CT ON O.FNHSysCustId=CT.FNHSysCustId"
        Qry &= vbCrLf & "LEFT OUTER JOIN (SELECT SP.FNHSysSuplId,SP.FTSuplCode,SP.FTSuplNameTH,SP.FTSuplNameEN FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier AS SP WITH(NOLOCK))AS SP ON O.FNHSysSuplId=SP.FNHSysSuplId"

        Qry &= vbCrLf & "LEFT OUTER JOIN ("
        Qry &= vbCrLf & " SELECT F.FTSMPOrderNo,F.FNHSysRawmatId,F.FTMatColor,F.FNMatSeq,F.PART,ISNULL ((SELECT TOP 1 STUFF "
        Qry &= vbCrLf & "((SELECT  ', ' + t2.FTPartName"
        Qry &= vbCrLf & "FROM      (Select A.FTPartNameTH AS FTPartName ,P.FNMat,P.FTSMPOrderNo,P.FNMatSeq"
        Qry &= vbCrLf & "FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPOrder_MatPart As P With(NOLOCK) "
        Qry &= vbCrLf & "LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMPart As A With(NOLOCK) On P.FNHSysPartId= A.FNHSysPartId"
        Qry &= vbCrLf & ") t2"
        Qry &= vbCrLf & "WHERE   t2.FTSMPOrderNo =  F.FTSMPOrderNo AND t2.FNMat=F.FNMatSeq   FOR XML PATH('')), 1, 2, '')  )"
        Qry &= vbCrLf & ",'') AS FTPartName "
        Qry &= vbCrLf & "FROM (SELECT F.FTSMPOrderNo,F.FNHSysRawmatId,F.FTMatColor,convert( nvarchar(30), F.FNMatSeq)AS FNMatSeq"
        Qry &= vbCrLf & ",CASE WHEN ISNULL(F.FNMatSeq,'') = '0'  THEN 'A' + char(30) ELSE  '' END "
        Qry &= vbCrLf & "+   CASE WHEN ISNULL(F.FNMatSeq,'')='1' THEN 'B' ELSE '' END"
        Qry &= vbCrLf & "+   CASE WHEN ISNULL(F.FNMatSeq,'')='2' THEN 'C' ELSE '' END"
        Qry &= vbCrLf & "+   CASE WHEN ISNULL(F.FNMatSeq,'')='3' THEN 'D' ELSE '' END"
        Qry &= vbCrLf & "+   CASE WHEN ISNULL(F.FNMatSeq,'')='4' THEN 'E' ELSE '' END"
        Qry &= vbCrLf & "+   CASE WHEN ISNULL(F.FNMatSeq,'')='5' THEN 'F' ELSE '' END"
        Qry &= vbCrLf & "+   CASE WHEN ISNULL(F.FNMatSeq,'')='6' THEN 'G' ELSE '' END"
        Qry &= vbCrLf & "+   CASE WHEN ISNULL(F.FNMatSeq,'')='7' THEN 'H' ELSE '' END AS PART"
        Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPOrder_FabricMatList AS F"
        Qry &= vbCrLf & " UNION"
        Qry &= vbCrLf & "SELECT F.FTSMPOrderNo,F.FNHSysRawmatId,F.FTMatColor,F.FTMat AS FNMatSeq,convert( nvarchar(30), F.FNMatSeq) AS PART"
        Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPOrder_MatList AS F )AS F"
        Qry &= vbCrLf & ")AS PA ON O.FTSMPOrderNo=PA.FTSMPOrderNo AND O.Mat=PA.FNHSysRawmatId AND O.FTMatColor=PA.FTMatColor AND O.PART=PA.PART"

        Qry &= vbCrLf & "LEFT OUTER JOIN("
        Qry &= vbCrLf & "SELECT L.FNListIndex,L.FTNameTH,L.FTNameEN"
        Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData AS L"
        Qry &= vbCrLf & "WHERE L.FTListName='FNSMPOrderType'"
        Qry &= vbCrLf & ")AS L ON O.FNSMPOrderType=L.FNListIndex"
        Qry &= vbCrLf & "LEFT OUTER JOIN(SELECT C.FNHSysCmpId,C.FTCmpCode FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp AS C)AS C ON O.FNHSysCmpId=C.FNHSysCmpId"
        Qry &= vbCrLf & "LEFT OUTER JOIN(SELECT B.FTSMPOrderNo,B.FTColorway,B.FTPatternDate FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPOrder_Breakdown AS B GROUP BY B.FTSMPOrderNo,B.FTColorway,B.FTPatternDate )AS B ON O.FTSMPOrderNo=B.FTSMPOrderNo  AND O.FTMatColor=B.FTColorway"
        Qry &= vbCrLf & "LEFT OUTER JOIN(SELECT SS.FNHSysSeasonId,SS.FTSeasonCode FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMSeason AS SS )AS SS ON O.FNHSysSeasonId=SS.FNHSysSeasonId"
        Qry &= vbCrLf & " LEFT OUTER JOIN(SELECT M.FTMerTeamCode,M.FNHSysMerTeamId FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMerTeam AS M)AS ME ON O.FNHSysMerTeamId=ME.FNHSysMerTeamId"
        Qry &= vbCrLf & "LEFT OUTER JOIN(SELECT BO.FTOrderNo,WH.FTTransferWHNo ,WH.FDApproveDate ,BO.FNQuantity,M.FNHSysRawMatId,MM.FTMainMatNameTH AS MaterialDescription,M.FNHSysUnitId,ISNULL(MC.FTRawMatColorCode,'')AS FTRawMatColorCode,BO.FTDocumentRefNo"
        Qry &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENTransferWH AS WH WITH(NOLOCK)  LEFT OUTER Join"
        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT AS BO WITH(NOLOCK) ON WH.FTTransferWHNo = BO.FTDocumentNo    LEFT OUTER Join"
        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode AS B WITH(NOLOCK) ON BO.FTBarcodeNo = B.FTBarcodeNo LEFT OUTER Join"
        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS M WITH(NOLOCK) ON  B.FNHSysRawMatId=M.FNHSysRawMatId LEFT OUTER Join"
        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMainMat AS MM WITH(NOLOCK) ON  M.FTRawMatCode=MM.FTMainMatCode LEFT OUTER Join"
        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatColor AS MC WITH(NOLOCK) ON M.FNHSysRawMatColorId=MC.FNHSysRawMatColorId  "
        Qry &= vbCrLf & ") AS H ON  O.FTSMPOrderNo=H.FTOrderNo  And O.Mat=H.FNHSysRawMatId And O.FTMatColor=H.FTRawMatColorCode AND RV.FTReserveNo=H.FTDocumentRefNo AND RV.FNQuantity=H.FNQuantity "
        Qry &= vbCrLf & "LEFT OUTER JOIN(SELECT R.FTReceiveNo,R.FDReceiveDate,RO.FNHSysRawMatId,RO.FNQuantity,RO.FTOrderNo"
        Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive AS R WITH(NOLOCK)  LEFT OUTER JOIN"
        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail_Order AS RO WITH(NOLOCK) ON R.FTReceiveNo=RO.FTReceiveNo"
        Qry &= vbCrLf & ")AS PRD  ON  O.FTSMPOrderNo=PRD.FTOrderNo And O.Mat=PRD.FNHSysRawMatId"
        Qry &= vbCrLf & " LEFT OUTER JOIN(SELECT S.FTIssueNo,S.FDIssueDate,S.FTOrderNo,BO.FNQuantity,B.FNHSysRawMatId,BO.FTDocumentRefNo"
        Qry &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENIssue AS S WITH (NOLOCK)"
        Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT AS BO WITH (NOLOCK) ON S.FTIssueNo = BO.FTDocumentNo"
        Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode As B With (NOLOCK) ON BO.FTBarcodeNo = B.FTBarcodeNo"
        Qry &= vbCrLf & ")AS PIS  ON  O.FTSMPOrderNo=PIS.FTOrderNo AND O.Mat=PIS.FNHSysRawMatId  AND H.FTTransferWHNo=PIS.FTDocumentRefNo AND H.FNQuantity=PIS.FNQuantity"
        Qry &= vbCrLf & " LEFT OUTER JOIN(SELECT S.FTReturnStockNo,S.FDReturnStockDate,BO.FTOrderNo,BO.FNQuantity,B.FNHSysRawMatId"
        Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReturnToStock AS S WITH (NOLOCK)"
        Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_IN AS BO WITH (NOLOCK) ON S.FTReturnStockNo = BO.FTDocumentNo"
        Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode As B With (NOLOCK) ON BO.FTBarcodeNo = B.FTBarcodeNo"
        Qry &= vbCrLf & ")AS RT ON  O.FTSMPOrderNo=RT.FTOrderNo  AND O.Mat=RT.FNHSysRawMatId"
        Qry &= vbCrLf & " LEFT OUTER JOIN(SELECT S.FTReturnSuplNo,S.FDReturnSuplDate,BO.FTOrderNo,BO.FNQuantity,B.FNHSysRawMatId"
        Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReturnToSupplier AS S WITH (NOLOCK)"
        Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT AS BO WITH (NOLOCK) ON S.FTReturnSuplNo = BO.FTDocumentNo"
        Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode As B With (NOLOCK) ON BO.FTBarcodeNo = B.FTBarcodeNo"
        Qry &= vbCrLf & ")AS RS ON  O.FTSMPOrderNo=RS.FTOrderNo  AND O.Mat=RS.FNHSysRawMatId"
        Qry &= vbCrLf & " LEFT OUTER JOIN(SELECT AJ.FTAdjustStockNo,AJ.FDAdjustStockDate,BO.FTOrderNo,BO.FNQuantity,B.FNHSysRawMatId"
        Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENAdjustStock AS AJ WITH (NOLOCK)"
        Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_IN AS BO WITH (NOLOCK) ON AJ.FTAdjustStockNo = BO.FTDocumentNo"
        Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode As B With (NOLOCK) ON BO.FTBarcodeNo = B.FTBarcodeNo"
        Qry &= vbCrLf & ")AS  AJ ON  O.FTSMPOrderNo=AJ.FTOrderNo  AND O.Mat=AJ.FNHSysRawMatId"
        Qry &= vbCrLf & "Left OUTER JOIN(Select BO.FTOrderNo, W.FTWHCode, B.FNHSysRawMatId, BO.FTDocumentNo"
        Qry &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_IN AS BO WITH (NOLOCK)"
        Qry &= vbCrLf & "Left OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode As B With (NOLOCK) ON BO.FTBarcodeNo = B.FTBarcodeNo"
        Qry &= vbCrLf & "Left OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMWarehouse AS W With (NOLOCK) ON BO.FNHSysWHId=W.FNHSysWHId"
        Qry &= vbCrLf & ")AS W ON O.FTSMPOrderNo=W.FTOrderNo  And O.Mat=W.FNHSysRawMatId And (RV.FTReserveNo=W.FTDocumentNo Or H.FTTransferWHNo=W.FTDocumentNo Or PIS.FTIssueNo=W.FTDocumentNo Or RS.FTReturnSuplNo=W.FTDocumentNo Or RT.FTReturnStockNo=W.FTDocumentNo Or AJ.FTAdjustStockNo=W.FTDocumentNo Or PRD.FTReceiveNo=W.FTDocumentNo)"

        Qry &= vbCrLf & " Left OUTER JOIN(Select FM.FTSMPOrderNo, P.FTPurchaseNo, P.FDPurchaseDate, PO.FNQuantity, FM.FTMatColor, FM.FNHSysRawmatId"
        Qry &= vbCrLf & "From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPOrder_MatList AS FM LEFT OUTER Join"
        Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail_Order AS RO ON FM.FTSMPOrderNo=RO.FTOrderNo LEFT OUTER Join"
        Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive AS R ON  RO.FTReceiveNo=R.FTReceiveNo LEFT OUTER Join"
        Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo AS PO ON FM.FTSMPOrderNo =PO.FTOrderNo And R.FTPurchaseNo=PO.FTPurchaseNo LEFT OUTER Join"
        Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase AS P ON  PO.FTPurchaseNo=P.FTPurchaseNo And R.FTPurchaseNo=P.FTPurchaseNo"
        Qry &= vbCrLf & "Where FM.FTStateFree ='1' AND R.FNRceceiveType='2'"
        Qry &= vbCrLf & "UNION"
        Qry &= vbCrLf & "Select FM.FTSMPOrderNo, P.FTPurchaseNo, P.FDPurchaseDate, PO.FNQuantity, FM.FTMatColor, FM.FNHSysRawmatId"
        Qry &= vbCrLf & "From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPOrder_FabricMatList AS FM LEFT OUTER Join"
        Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail_Order AS RO ON FM.FTSMPOrderNo=RO.FTOrderNo LEFT OUTER Join"
        Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive AS R ON  RO.FTReceiveNo=R.FTReceiveNo LEFT OUTER Join"
        Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo AS PO ON FM.FTSMPOrderNo =PO.FTOrderNo And R.FTPurchaseNo=PO.FTPurchaseNo LEFT OUTER Join"
        Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase AS P ON  PO.FTPurchaseNo=P.FTPurchaseNo And R.FTPurchaseNo=P.FTPurchaseNo"
        Qry &= vbCrLf & "Where FM.FTStateFree ='1' AND R.FNRceceiveType='2' "
        Qry &= vbCrLf & ")AS FPO ON  O.FTSMPOrderNo=FPO.FTSMPOrderNo  And O.Mat=FPO.FNHSysRawMatId And O.FTMatColor=FPO.FTMatColor"




        Qry &= vbCrLf & "WHERE  O.FTSMPOrderNo<>'' AND R.FTRawMatCode<>'' "
        If Me.FTSMPOrderNo.Text <> "" Then
            Qry &= vbCrLf & "    AND O.FTSMPOrderNo>='" & HI.UL.ULF.rpQuoted(Me.FTSMPOrderNo.Text) & "' "
        End If
        If Me.FTSMPOrderNoTo.Text <> "" Then
            Qry &= vbCrLf & "    AND O.FTSMPOrderNo<='" & HI.UL.ULF.rpQuoted(Me.FTSMPOrderNoTo.Text) & "' "
        End If
        If Me.FTStartShipment.Text <> "" Then
            Qry &= vbCrLf & "    AND PO.FDDeliveryDate >='" & HI.UL.ULDate.ConvertEnDB(Me.FTStartShipment.Text) & "' "
        End If
        If Me.FTEndShipment.Text <> "" Then
            Qry &= vbCrLf & "    AND PO.FDDeliveryDate <='" & HI.UL.ULDate.ConvertEnDB(Me.FTEndShipment.Text) & "' "
        End If
        If Me.FTStateReceiptDate.Text <> "" Then
            Qry &= vbCrLf & "    AND O.FTStateReceiptDate >='" & HI.UL.ULDate.ConvertEnDB(Me.FTStateReceiptDate.Text) & "' "
        End If
        If Me.FTStateReceiptDateEnd.Text <> "" Then
            Qry &= vbCrLf & "    AND O.FTStateReceiptDate <='" & HI.UL.ULDate.ConvertEnDB(Me.FTStateReceiptDateEnd.Text) & "' "
        End If
        If Me.FNHSysStyleId.Text <> "" Then
            Qry &= vbCrLf & "    AND S.FTStyleCode='" & HI.UL.ULF.rpQuoted(Me.FNHSysStyleId.Text) & "' "
        End If
        If Me.FNHSysCustId.Text <> "" Then
            Qry &= vbCrLf & "    AND CT.FTCustCode='" & HI.UL.ULF.rpQuoted(Me.FNHSysCustId.Text) & "' "
        End If
        If Me.FNHSysSeasonId.Text <> "" Then
            Qry &= vbCrLf & "    AND SS.FTSeasonCode='" & HI.UL.ULF.rpQuoted(Me.FNHSysSeasonId.Text) & "' "
        End If
        If Me.FNHSysMerTeamId.Text <> "" Then
            Qry &= vbCrLf & "    AND ME.FTMerTeamCode='" & HI.UL.ULF.rpQuoted(Me.FNHSysMerTeamId.Text) & "' "
        End If
        If Me.FNHSysSuplId.Text <> "" Then
            Qry &= vbCrLf & "    AND SP.FTSuplCode ='" & HI.UL.ULF.rpQuoted(Me.FNHSysSuplId.Text) & "' "
        End If
        If Me.FTPgm.Text <> "" Then
            Qry &= vbCrLf & "    AND O.FTPgmName ='" & HI.UL.ULF.rpQuoted(Me.FTPgm.Text) & "' "
        End If
        If Me.FNSMPOrderType.Text <> "" Then
            Qry &= vbCrLf & "    AND L.FTNameTH ='" & HI.UL.ULF.rpQuoted(Me.FNSMPOrderType.Text) & "' "
        End If
        If Me.CFDSMPOrderDate.Text <> "" Then
            Qry &= vbCrLf & "    AND O.FDSMPOrderDate >='" & HI.UL.ULDate.ConvertEnDB(Me.CFDSMPOrderDate.Text) & "' "
        End If
        If Me.FDSMPOrderDateEnd.Text <> "" Then
            Qry &= vbCrLf & "    AND O.FDSMPOrderDate <='" & HI.UL.ULDate.ConvertEnDB(Me.FDSMPOrderDateEnd.Text) & "' "
        End If
        If Me.FDApproveDate.Text <> "" Then
            Qry &= vbCrLf & "    AND H.FDApproveDate >='" & HI.UL.ULDate.ConvertEnDB(Me.FDApproveDate.Text) & "' "
        End If
        If Me.FDApproveDateE.Text <> "" Then
            Qry &= vbCrLf & "    AND H.FDApproveDate <='" & HI.UL.ULDate.ConvertEnDB(Me.FDApproveDateE.Text) & "' "
        End If
        If Me.SFDAdjustStockDate.Text <> "" Then
            Qry &= vbCrLf & "    AND AJ.FDAdjustStockDate >='" & HI.UL.ULDate.ConvertEnDB(Me.SFDAdjustStockDate.Text) & "' "
        End If
        If Me.EFDAdjustStockDate.Text <> "" Then
            Qry &= vbCrLf & "    AND AJ.FDAdjustStockDate <='" & HI.UL.ULDate.ConvertEnDB(Me.EFDAdjustStockDate.Text) & "' "
        End If
        If Me.SFDReceiveDate.Text <> "" Then
            Qry &= vbCrLf & "    AND PRD.FDReceiveDate >='" & HI.UL.ULDate.ConvertEnDB(Me.SFDReceiveDate.Text) & "' "
        End If
        If Me.EFDReceiveDate.Text <> "" Then
            Qry &= vbCrLf & "    AND PRD.FDReceiveDate <='" & HI.UL.ULDate.ConvertEnDB(Me.EFDReceiveDate.Text) & "' "
        End If
        If Me.FNHSysWHId.Text <> "" Then
            Qry &= vbCrLf & "    AND W.FTWHCode ='" & HI.UL.ULF.rpQuoted(Me.FNHSysWHId.Text) & "' "
        End If

        Qry &= vbCrLf & "ORDER BY  O.FTSMPOrderNo,O.N  ASC"


        'ogcDetail.DataSource = HI.Conn.SQLConn.GetDataTable(Qry, Conn.DB.DataBaseName.DB_SAMPLE)
        Dim _dt As DataTable = HI.Conn.SQLConn.GetDataTable(Qry, Conn.DB.DataBaseName.DB_SAMPLE)
        Me.ogcDetail.DataSource = _dt
        _dt.Dispose()
    End Sub
#End Region

#Region "Event"
    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub
    Private Sub ocmload_Click(sender As Object, e As EventArgs) Handles ocmload.Click
        If Verify() Then
            Dim _Spls As New TL.SplashScreen("Please Wait.....Loading Data")
            LoadData()
            _Spls.Close()
        Else
            If ST.Lang.Language = ST.Lang.eLang.TH Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text)
            Else
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text)
            End If

        End If
    End Sub

    Private Sub ocmclear_Click(sender As Object, e As EventArgs) Handles ocmclear.Click

        TL.HandlerControl.ClearControl(Me)
        Me.FNSMPOrderType.Text = ""
    End Sub
#End Region

    Public Function Verify() As Boolean
        If Me.FTSMPOrderNo.Text <> "" Or Me.FTSMPOrderNoTo.Text <> "" Or Me.FTStartShipment.Text <> "" Or Me.FTEndShipment.Text <> "" Or Me.FNHSysStyleId.Text <> "" Or Me.FNHSysCustId.Text <> "" Or Me.FNHSysSeasonId.Text <> "" Or Me.FNHSysMerTeamId.Text <> "" Or Me.CFDSMPOrderDate.Text <> "" Or Me.FDSMPOrderDateEnd.Text <> "" Or Me.FNSMPOrderType.Text <> "" Or Me.FTPgm.Text <> "" Or Me.FNHSysSuplId.Text <> "" Or Me.FTStateReceiptDate.Text <> "" Or Me.FTStateReceiptDateEnd.Text <> "" Or Me.FDApproveDate.Text <> "" Or Me.FDApproveDateE.Text <> "" Or Me.SFDAdjustStockDate.Text <> "" Or Me.EFDAdjustStockDate.Text <> "" Or Me.SFDReceiveDate.Text <> "" Or Me.EFDReceiveDate.Text <> "" Or FNHSysWHId.Text <> "" Then

            Return True
        Else
            Return False
        End If
    End Function



    Private Sub wSMPOrderStatusTrack_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        With Me.ogvDetail
            .Columns.ColumnByFieldName("FTProGacDate").OptionsColumn.AllowEdit = (Me.ocmsave.Enabled)
            .Columns.ColumnByFieldName("FDSuplCfmDliDate").OptionsColumn.AllowEdit = (Me.ocmsave.Enabled)

            '.Columns.ColumnByFieldName("FTProGacDate").OptionsColumn.AllowEdit = False
            '.Columns.ColumnByFieldName("FDSuplCfmDliDate").OptionsColumn.AllowEdit = False
            .OptionsSelection.MultiSelectMode = GridMultiSelectMode.RowSelect
            .OptionsSelection.MultiSelect = False

        End With
        Me.FNSMPOrderType.Text = ""


    End Sub



    Private Sub ItemDate_Leave(sender As Object, e As System.EventArgs)
        Try
            With CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView)

                Dim _TDate As String

                Try

                    _TDate = HI.UL.ULDate.ConvertEnDB(CType(sender, DevExpress.XtraEditors.DateEdit).DateTime)

                    If _TDate = "0001/01/01" Then
                        _TDate = ""
                    End If

                Catch ex As Exception
                    _TDate = ""
                End Try

                CType(sender, DevExpress.XtraEditors.DateEdit).Text = _TDate

                Try
                    If _TDate <> "" Then
                        CType(sender, DevExpress.XtraEditors.DateEdit).DateTime = _TDate
                    Else
                        CType(sender, DevExpress.XtraEditors.DateEdit).DateTime = Nothing
                    End If

                Catch ex As Exception
                End Try

                If _TDate = "" Then
                    .SetRowCellValue(.FocusedRowHandle, .FocusedColumn.FieldName.ToString, "")
                Else
                    .SetRowCellValue(.FocusedRowHandle, .FocusedColumn.FieldName.ToString, HI.UL.ULDate.ConvertEN(_TDate))
                End If

          


                Dim NewData As String = HI.UL.ULDate.ConvertEN(_TDate)
                If NewData <> GridDataBefore Then

                    Dim OrderNo As String = .GetRowCellValue(.FocusedRowHandle, "SampleOrderNo").ToString()
                    Dim Raw As String = .GetRowCellValue(.FocusedRowHandle, "asd").ToString()
                    Dim Mat As String = .GetRowCellValue(.FocusedRowHandle, "MaterialCode").ToString()
                    Dim MatName As String = .GetRowCellValue(.FocusedRowHandle, "MaterialDescription").ToString()
                    Dim MatColor As String = .GetRowCellValue(.FocusedRowHandle, "MaterialColorCode").ToString()
                    Dim MatColorName As String = .GetRowCellValue(.FocusedRowHandle, "FTMatColorName").ToString()
                    Dim MatSize As String = .GetRowCellValue(.FocusedRowHandle, "MaterialSizeCode").ToString()
                    Dim MAtQty As String = .GetRowCellValue(.FocusedRowHandle, "UsedQty").ToString()
                    Dim MAtUnit As String = .GetRowCellValue(.FocusedRowHandle, "FNHSysUnitId").ToString()
                    Dim MAtSupl As String = .GetRowCellValue(.FocusedRowHandle, "FNHSysSuplId").ToString()


                    Dim FieldName As String = .FocusedColumn.FieldName.ToString
                    Dim _DSeq As Integer = 0
                    _DSeq = _DSeq + 1
                  

                    Dim cmdstring As String = ""

                    If _TDate = "" Then
                        cmdstring = "update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPOrder_DateSuplPlan  Set "
                        cmdstring &= vbCrLf & " " & FieldName & "=NULL"
                        cmdstring &= vbCrLf & " WHERE FTSMPOrderNo='" & HI.UL.ULF.rpQuoted(OrderNo) & "' AND  FNHSysRawmatId='" & HI.UL.ULF.rpQuoted(Raw) & "'" ' AND FTColorway='" & HI.UL.ULF.rpQuoted(Color) & "' "
                    Else
                        cmdstring = "update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPOrder_DateSuplPlan  Set "
                        cmdstring &= vbCrLf & " " & FieldName & "='" & HI.UL.ULDate.ConvertEnDB(NewData) & "'"
                        cmdstring &= vbCrLf & " WHERE FTSMPOrderNo='" & HI.UL.ULF.rpQuoted(OrderNo) & "' AND  FNHSysRawmatId='" & HI.UL.ULF.rpQuoted(Raw) & "'" ' AND FTColorway='" & HI.UL.ULF.rpQuoted(Color) & "' "

                    End If

                    'HI.Conn.SQLConn.ExecuteNonQuery(cmdstring, Conn.DB.DataBaseName.DB_SAMPLE)
                    If HI.Conn.SQLConn.ExecuteNonQuery(cmdstring, Conn.DB.DataBaseName.DB_SAMPLE) = False Then

                        cmdstring = "insert into [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPOrder_DateSuplPlan ("
                        cmdstring &= vbCrLf & " FTInsUser, FDInsDate, FTInsTime, FTSMPOrderNo,FNHSysRawmatId, FTMat," & FieldName & ""
                        cmdstring &= vbCrLf & " )"
                        cmdstring &= vbCrLf & " Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ""
                        cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(OrderNo) & "'"
                        cmdstring &= vbCrLf & "," & Val(Raw) & ""
                        cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Mat) & "'"
                        cmdstring &= vbCrLf & ",'" & HI.UL.ULDate.ConvertEnDB(NewData) & "'"


                        HI.Conn.SQLConn.ExecuteNonQuery(cmdstring, Conn.DB.DataBaseName.DB_SAMPLE)

                    End If
                    Dim _GAC As String = HI.Conn.SQLConn.GetField("select FTProGacDate FROM[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPOrder_DateSuplPlan   with(nolock) where FTSMPOrderNo='" & HI.UL.ULF.rpQuoted(OrderNo) & "' AND FNHSysRawmatId='" & HI.UL.ULF.rpQuoted(Raw) & "'", Conn.DB.DataBaseName.DB_MASTER, "")
                    Dim _SUP As String = HI.Conn.SQLConn.GetField("select FDSuplCfmDliDate FROM[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPOrder_DateSuplPlan   with(nolock) where FTSMPOrderNo='" & HI.UL.ULF.rpQuoted(OrderNo) & "' AND FNHSysRawmatId='" & HI.UL.ULF.rpQuoted(Raw) & "'", Conn.DB.DataBaseName.DB_MASTER, "")

                    If _GAC = "" And _SUP = "" Then
                        Dim _Str As String
                        _Str = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPOrder_DateSuplPlan WHERE FTSMPOrderNo='" & HI.UL.ULF.rpQuoted(OrderNo) & "' AND FNHSysRawmatId='" & HI.UL.ULF.rpQuoted(Raw) & "'"
                        HI.Conn.SQLConn.ExecuteNonQuery(_Str, Conn.DB.DataBaseName.DB_SAMPLE)

                    End If




                End If


            End With

        Catch ex As Exception
        End Try


        Call LoadData()

    End Sub
    Private Sub ItemDate_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            With CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView)

                Dim _TDate As String = HI.UL.ULDate.ConvertEnDB(.GetFocusedRowCellValue(.FocusedColumn))
                If _TDate = "" Then
                    Beep()
                End If
                Try
                    CType(sender, DevExpress.XtraEditors.DateEdit).DateTime = _TDate
                Catch ex As Exception
                    CType(sender, DevExpress.XtraEditors.DateEdit).Text = ""
                End Try

                GridDataBefore = _TDate
            End With

        Catch ex As Exception

        End Try
    End Sub
    Private Sub ogvDetail_CustomColumnDisplayText(sender As Object, e As CustomColumnDisplayTextEventArgs) Handles ogvDetail.CustomColumnDisplayText

        Select Case e.Column.FieldName
            Case "FTProGacDate", "FDSuplCfmDliDate"

                If e.DisplayText = "01/01/0001" Then
                    e.DisplayText = ""
                End If
        End Select

    End Sub

    Private Sub ocmrefresh_Click(sender As Object, e As EventArgs)
        Call LoadData()
    End Sub
    Private Function CheckProcess(key As String, Optional showmsg As Boolean = True) As Boolean
        Dim stateprocess As Boolean = False

        Dim cmd As String = ""

        cmd = "select top 1 FE.FTStateFree from(select F.FTStateFree,F.FTSMPOrderNo from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPOrder_FabricMatList AS F with(nolock) union select M.FTStateFree,M.FTSMPOrderNo from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPOrder_MatList AS M )AS FE where FE.FTSMPOrderNo='" & HI.UL.ULF.rpQuoted(Me.FTSMPOrderNo.Text) & "'"

        stateprocess = (HI.Conn.SQLConn.GetField(cmd, Conn.DB.DataBaseName.DB_SAMPLE, "") = "1")

        If stateprocess Then

            If showmsg Then
                HI.MG.ShowMsg.mInfo("พบข้อมูลบันทึก จบ กระบวนการทำงานแล้ว ไม่สามารถลบหรือแก้ไขได้ !!!", 1809210046, Me.Text, key, System.Windows.Forms.MessageBoxIcon.Warning)
            End If

        End If

        Return stateprocess
    End Function

    Private Sub ogcDetail_Click(sender As Object, e As EventArgs) Handles ogcDetail.Click

    End Sub
End Class
Imports DevExpress.Data
Imports DevExpress.XtraGrid.Views.Grid
Public Class wTrackSendSupl
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        'Call SetGridSumery()
        'Call SetformatCell()
    End Sub

    Private Sub LoadData()
        Dim _Qry As String = ""
        Dim _dt As DataTable
        Dim _Cmp As String = HI.ST.SysInfo.CmpID
        'Dim _SupTo As String = HI.Conn.SQLConn.GetField("SELECT H.FNHSysSuplId FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier AS H WITH (NOLOCK) WHERE H.FTSuplCode='" & Me.FNHSysSuplIdTo.Text & "'", Conn.DB.DataBaseName.DB_MASTER, "")

        Dim _Spls As New HI.TL.SplashScreen("Please wait Loading Data.....")

        '_Qry = "  Select SS.FTSendSuplNo,ISNULL(RS.FTPurchaseNo,'') AS FTPurchaseNo"
        '_Qry &= vbCrLf & ",convert(varchar(10),convert(date,SS.FDSendSuplDate),103) as FDSendSuplDate,SS.FTSuplName,SS.FNQuantity AS QtySend"
        '_Qry &= vbCrLf & ",RS.FTRcvSuplNo,ISNULL(RS.FTInvoiceNo,'') AS FTInvoiceNo"
        '_Qry &= vbCrLf & ",CASE WHEN RS.FTInvoiceDate='' then '' else convert(varchar(10),convert(date,RS.FTInvoiceDate),103) end as FTInvoiceDate"
        '_Qry &= vbCrLf & ",RS.FTOperationName,RS.FTPORef,RS.FTStyleCode,RS.FTOrderNo,ISNULL(RS.FNDefectQty,0) AS FNDefectQty,RS.FNQuantity-ISNULL(RS.FNDefectQty,0) AS QtyRcv"
        '_Qry &= vbCrLf & ",ISNULL(RS.FNPrice*RS.FNExchangeRate,0) as FNPrice,ISNULL((RS.FNPrice*RS.FNExchangeRate)*(RS.FNQuantity-ISNULL(RS.FNDefectQty,0)),0) AS Total"

        '_Qry &= vbCrLf & "from"
        '_Qry &= vbCrLf & "(SELECT  A.FTBarcodeSendSuplNo"
        '_Qry &= vbCrLf & ",SSS.FDSendSuplDate,A.FNHSysPartId,A.FNSendSuplType, A.FNHSysSuplId, A.FTBarcodeBundleNo, O.FTOrderNo, A.FTOrderProdNo, A.FTSendSuplRef, A.FNHSysCmpId"
        '_Qry &= vbCrLf & ", S.FTSuplCode, B.FTSendSuplNo, BB.FTColorway, BB.FTSizeBreakDown, BB.FNQuantity, ST.FTStyleCode"
        'If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
        '    _Qry &= vbCrLf & ",S.FTSuplNameTH AS FTSuplName"
        'Else
        '    _Qry &= vbCrLf & ",S.FTSuplNameEN AS FTSuplName"
        'End If
        '_Qry &= vbCrLf & ",MP.FTPartCode,ISNULL(OPP.FNHSysOperationId,OPS.FNHSysOperationId) AS FNHSysOperationId "
        '_Qry &= vbCrLf & ",ISNULL(OPP.FNSeq,OPS.FNSeq) AS FNSeq"
        '_Qry &= vbCrLf & ",ISNULL(OPP.FNHSysOperationIdTo,OPS.FNHSysOperationIdTo) AS FNHSysOperationIdTo"
        '_Qry &= vbCrLf & ",Mpp.FTOperationCode,MP.FTPartNameTH AS FTPartName,MPP.FTOperationNameTH  AS FTOperationName"
        '_Qry &= vbCrLf & " ,ISNULL(("
        '_Qry &= vbCrLf & "SELECT        TOP 1  "
        '_Qry &= vbCrLf & "B.FNHSysMarkId"
        '_Qry &= vbCrLf & "FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle_Detail AS AA  WITH (NOLOCK)  INNER JOIN"
        '_Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTLayCut AS B  WITH (NOLOCK) ON AA.FTLayCutNo = B.FTLayCutNo "
        '_Qry &= vbCrLf & "WHERE(AA.FTBarcodeBundleNo = A.FTBarcodeBundleNo)"
        '_Qry &= vbCrLf & "),0) AS FNHSysMarkId"
        '_Qry &= vbCrLf & ",ISNULL(("
        '_Qry &= vbCrLf & "SELECT        TOP 1  "
        '_Qry &= vbCrLf & "C.FTMarkNameTH AS FTMarkName  "
        '_Qry &= vbCrLf & "FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle_Detail AS AA  WITH (NOLOCK)  INNER JOIN"
        '_Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTLayCut AS B  WITH (NOLOCK) ON AA.FTLayCutNo = B.FTLayCutNo LEFT OUTER JOIN"
        '_Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TPRODMMark AS C  WITH (NOLOCK)  ON B.FNHSysMarkId = C.FNHSysMarkId"
        '_Qry &= vbCrLf & "WHERE(AA.FTBarcodeBundleNo = A.FTBarcodeBundleNo)"
        '_Qry &= vbCrLf & "),'') AS FTMarkName"
        '_Qry &= vbCrLf & "FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH (NOLOCK) INNER JOIN"
        '_Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS ST WITH (NOLOCK)  ON O.FNHSysStyleId = ST.FNHSysStyleId RIGHT OUTER JOIN"
        '_Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd AS ODP WITH (NOLOCK)  ON O.FTOrderNo = ODP.FTOrderNo RIGHT OUTER JOIN"
        '_Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcode_SendSupl AS A WITH (NOLOCK)  ON  ODP.FTOrderProdNo =  A.FTOrderProdNo  INNER JOIN"
        '_Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle AS BB WITH (NOLOCK)  ON A.FTBarcodeBundleNo = BB.FTBarcodeBundleNo INNER JOIN"
        '*******_Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_SendSupl AS SD WITH (NOLOCK)  ON A.FTSendSuplRef = SD.FTSendSuplRef AND ODP.FTOrderProdNo = SD.FTOrderProdNo and A.FNHSysPartId = SD.FNHSysPartId and A.FNSendSuplType = SD.FNSendSuplType LEFT OUTER JOIN"
        '_Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODMOperationByStyle AS OPS WITH (NOLOCK)  ON O.FNHSysStyleId = OPS.FNHSysStyleId AND SD.FNHSysOperationId = OPS.FNHSysOperationId LEFT OUTER JOIN"
        '_Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTSendSuplToBranch_Barcode AS B  WITH (NOLOCK) ON A.FTBarcodeSendSuplNo = B.FTBarcodeSendSuplNo LEFT OUTER JOIN"
        '_Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTSendSuplToBranch AS SSS WITH(NOLOCK) ON B.FTSendSuplNo=SSS.FTSendSuplNo LEFT OUtER JOIN"
        '_Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier AS S WITH (NOLOCK) ON A.FNHSysSuplId = S.FNHSysSuplId"
        '_Qry &= vbCrLf & "LEFT OUTER JOIN"
        '_Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMPart  AS MP WITH (NOLOCK) ON A.FNHSysPartId = MP.FNHSysPartId"
        '_Qry &= vbCrLf & "LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOperationByOrderProd AS OPP WITH (NOLOCK)  ON ODP.FTOrderProdNo = OPP.FTOrderProdNo AND SD.FNHSysOperationId = OPP.FNHSysOperationId "
        '_Qry &= vbCrLf & "LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TPRODMOperation AS MPP WITH (NOLOCK)  ON  ISNULL(OPP.FNHSysOperationId,OPS.FNHSysOperationId)  = MPP.FNHSysOperationId"
        '_Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp AS C WITH(NOLOCK) ON SSS.FNHSysCmpId = C.FNHSysCmpId"
        '_Qry &= vbCrLf & "WHERE B.FTSendSuplNo<>''"
        'If Me.FNHSysCmpId.Text <> "" Then
        '    _Qry &= vbCrLf & "AND C.FTCmpCode='" & Me.FNHSysCmpId.Text & "'"
        'End If
        'If Me.FNHSysStyleId.Text <> "" Then
        '    _Qry &= vbCrLf & "AND ST.FTStyleCode='" & Me.FNHSysStyleId.Text & "'"
        'End If
        'If Me.FTOrderNo.Text <> "" Then
        '    _Qry &= vbCrLf & "AND O.FTOrderNo>='" & Me.FTOrderNo.Text & "'"
        'End If
        'If Me.FTOrderNoTo.Text <> "" Then
        '    _Qry &= vbCrLf & "AND O.FTOrderNo<='" & Me.FTOrderNoTo.Text & "'"
        'End If

        '_Qry &= vbCrLf & ") AS SS INNER JOIN"
        '_Qry &= vbCrLf & "( SELECT  A.FTBarcodeSendSuplNo,R.FTInvoiceNo,R.FTInvoiceDate, A.FNHSysPartId, A.FNSendSuplType, A.FNHSysSuplId, A.FTBarcodeBundleNo, O.FTOrderNo"
        '_Qry &= vbCrLf & ", A.FTOrderProdNo, A.FTSendSuplRef, A.FNHSysCmpId, S.FTSuplCode, B.FTRcvSuplNo, BB.FTColorway"
        '_Qry &= vbCrLf & ", BB.FTSizeBreakDown, BB.FNQuantity, ST.FTStyleCode,MP.FTPartCode,O.FTPORef,DF.FNDefectQty"
        '_Qry &= vbCrLf & ",ISNULL(OPP.FNHSysOperationId,OPS.FNHSysOperationId) AS FNHSysOperationId"
        '_Qry &= vbCrLf & ",ISNULL(OPP.FNSeq,OPS.FNSeq) AS FNSeq"
        '_Qry &= vbCrLf & ",ISNULL(OPP.FNHSysOperationIdTo,OPS.FNHSysOperationIdTo) AS FNHSysOperationIdTo"
        '_Qry &= vbCrLf & ",Mpp.FTOperationCode "
        '_Qry &= vbCrLf & ",PP.FTPurchaseNo"
        'If ST.Lang.Language = ST.Lang.eLang.TH Then
        '    _Qry &= vbCrLf & ",MP.FTPartNameTH AS FTPartName "
        '    _Qry &= vbCrLf & ",MPP.FTOperationNameTH  AS FTOperationName"
        'Else
        '    _Qry &= vbCrLf & ",MP.FTPartNameEN AS FTPartName "
        '    _Qry &= vbCrLf & ",MPP.FTOperationNameEN  AS FTOperationName"
        'End If

        ''_Qry &= vbCrLf & ",MPP.FTOperationNameTH  AS FTOperationName"
        '_Qry &= vbCrLf & ",ISNULL(("
        '_Qry &= vbCrLf & "SELECT        TOP 1  "
        '_Qry &= vbCrLf & "C.FTMarkNameTH AS FTMarkName  "
        '_Qry &= vbCrLf & "FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle_Detail AS AA  WITH (NOLOCK)  INNER JOIN"
        '_Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTLayCut AS B  WITH (NOLOCK) ON AA.FTLayCutNo = B.FTLayCutNo LEFT OUTER JOIN"
        '_Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TPRODMMark AS C  WITH (NOLOCK)  ON B.FNHSysMarkId = C.FNHSysMarkId"
        '_Qry &= vbCrLf & "WHERE(AA.FTBarcodeBundleNo = A.FTBarcodeBundleNo)"
        '_Qry &= vbCrLf & "),'') AS FNHSysMarkId"
        '_Qry &= vbCrLf & ", SSB.FTSendSuplNo,PP.FNExchangeRate,PP.FNPrice "


        '_Qry &= vbCrLf & "FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH (NOLOCK) INNER JOIN"
        '_Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS ST WITH (NOLOCK)  ON O.FNHSysStyleId = ST.FNHSysStyleId RIGHT OUTER JOIN"
        '_Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd AS ODP WITH (NOLOCK)  ON O.FTOrderNo = ODP.FTOrderNo RIGHT OUTER JOIN"
        '_Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcode_SendSupl AS A WITH (NOLOCK)  ON  ODP.FTOrderProdNo =  A.FTOrderProdNo  LEFT OUTER JOIN"
        '_Qry &= vbCrLf & "" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & ".dbo.TPROSendSuplDefect AS DF WITH(NOLOCK) ON A.FTBarcodeSendSuplNo=DF.FTBarcodeSendSuplNo INNER JOIN"
        '_Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle AS BB WITH (NOLOCK)  ON A.FTBarcodeBundleNo = BB.FTBarcodeBundleNo INNER JOIN"
        '********_Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_SendSupl AS SD WITH (NOLOCK)  ON A.FTSendSuplRef = SD.FTSendSuplRef AND ODP.FTOrderProdNo = SD.FTOrderProdNo and A.FNHSysPartId = SD.FNHSysPartId and A.FNSendSuplType = SD.FNSendSuplType LEFT OUTER JOIN"
        '_Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODMOperationByStyle AS OPS WITH (NOLOCK)  ON O.FNHSysStyleId = OPS.FNHSysStyleId AND SD.FNHSysOperationIdTo = OPS.FNHSysOperationId LEFT OUTER JOIN"
        '_Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTReceiveSuplToBranch_Barcode AS B  WITH (NOLOCK) ON A.FTBarcodeSendSuplNo = B.FTBarcodeSendSuplNo LEFT OUTER JOIN"
        '_Qry &= vbCrLf & "" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & ".dbo.TPRODTReceiveSuplToBranch AS R WITH(NOLOCK) ON B.FTRcvSuplNo=R.FTRcvSuplNo LEFT OUTER JOIN"
        '_Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier AS S WITH (NOLOCK) ON A.FNHSysSuplId = S.FNHSysSuplId  LEFT OUTER JOIN "
        '_Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMPart  AS MP WITH (NOLOCK) ON A.FNHSysPartId = MP.FNHSysPartId"
        '********_Qry &= vbCrLf & "LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOperationByOrderProd AS OPP WITH (NOLOCK)  ON ODP.FTOrderProdNo = OPP.FTOrderProdNo AND SD.FNHSysOperationIdTo = OPP.FNHSysOperationId "
        '********_Qry &= vbCrLf & "LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TPRODMOperation AS MPP WITH (NOLOCK)  ON  SD.FNHSysOperationIdTo  = MPP.FNHSysOperationId"
        '_Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTSendSupl_Barcode AS SSB  WITH (NOLOCK)   ON  B.FTBarcodeSendSuplNo = SSB.FTBarcodeSendSuplNo "
        '_Qry &= vbCrLf & "LEFT OUTER JOIN (SELeCT DR.FTSendSuplNo,PS.FNExchangeRate,PSD.FNPrice,PSD.FNHSysPartId,PSD.FTPurchaseNo"
        '_Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPRODTPurchaseSendSupl AS PS WITH(NOLOCK) INNER JOIN"
        '_Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPRODTPurchaseSendSupl_Detail AS PSD WITH(NOLOCK) ON PS.FTPurchaseNo=PSD.FTPurchaseNo INNER JOIN"
        '_Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPRODTPurchaseSendSupl_DocSendRef AS DR WITH(NOLOCK) ON PSD.FTPurchaseNo=DR.FTPurchaseNo"
        '_Qry &= vbCrLf & ") AS PP ON SSB.FTSendSuplNo=PP.FTSendSuplNo AND A.FNHSysPartId=PP.FNHSysPartId"
        '_Qry &= vbCrLf & ") AS RS ON  SS.FTSendSuplNo=RS.FTSendSuplNo AND SS.FTBarcodeSendSuplNo=RS.FTBarcodeSendSuplNo"
        '*********************************************************************************************************************************************************************************************************
        '_Qry &= vbCrLf & " select rec.FTSendSuplNo,rec.FTBarcodeSendSuplNo"
        'If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
        '    _Qry &= vbCrLf & ",rec.FTSuplNameTH AS FTSuplName"
        '    _Qry &= vbCrLf & ",rec.FTOperationNameTH AS FTOperationName"
        'Else
        '    _Qry &= vbCrLf & ",rec.FTSuplNameEN AS FTSuplName"
        '    _Qry &= vbCrLf & ",rec.FTOperationNameEN AS FTOperationName"
        'End If
        '_Qry &= vbCrLf & ",convert(varchar(10),convert(datetime,rec.FDSendSuplDate),103) AS FDSendSuplDate"
        '_Qry &= vbCrLf & ",isnull(Rec.FTInvoiceNo,'') AS FTInvoiceNo"
        '_Qry &= vbCrLf & ",isnull(convert(varchar(10),convert(datetime,rec.FTInvoiceDate),103),'') AS FTInvoiceDate"
        '_Qry &= vbCrLf & ",isnull(DefecQty.BarDefec,'') AS BarDefec,isnull(DefecQty.FNDefectQty,0) AS FNDefectQty"
        '_Qry &= vbCrLf & ",BB.FNQuantity AS QtySend "
        '_Qry &= vbCrLf & ",BB.FNQuantity-isnull(DefecQty.FNDefectQty,0) AS QtyRcv"
        '_Qry &= vbCrLf & ",rec.FTOrderNo"
        '_Qry &= vbCrLf & ",PP.FTPurchaseNo"
        '_Qry &= vbCrLf & ",isnull((PP.FNPrice*PP.FNExchangeRate),0) FNPrice ,ISNULL((PP.FNPrice*PP.FNExchangeRate)*(BB.FNQuantity-ISNULL(DefecQty.FNDefectQty,0)),0) AS Total"
        '_Qry &= vbCrLf & ",rec.FTRcvSuplNo,rec.FTPORef,rec.FTStyleCode"
        '_Qry &= vbCrLf & "from"
        '_Qry &= vbCrLf & "(SELECT DISTINCT  B.FTSendSuplNo"
        '_Qry &= vbCrLf & ",Br.FDSendSuplDate"
        '_Qry &= vbCrLf & ",ST.FTStyleCode"
        '_Qry &= vbCrLf & ",RB.FTInvoiceNo,RB.FTInvoiceDate,RB_B.FTRcvSuplNo"
        '_Qry &= vbCrLf & ", B.FTOrderProdNo, B.FTOrderNo,B.FTBarcodeSendSuplNo,b.FTBarcodeBundleNo,O.FTPORef"
        '_Qry &= vbCrLf & ",B.FNHSysPartId,Com.FTCmpCode,Sulp.FTSuplNameTH,Sulp.FTSuplNameEN"
        '_Qry &= vbCrLf & ",MPP.FTOperationNameTH,MPP.FTOperationNameEN"

        '_Qry &= vbCrLf & "FROM"
        '_Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTSendSuplToBranch_Barcode AS B WITH (NOLOCK) INNER Join"
        '_Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTSendSuplToBranch AS Br WITH(NOLOCK) ON B.FTSendSuplNo=Br.FTSendSuplNo LEFT OUtER JOIN"
        '_Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTReceiveSuplToBranch_Barcode AS RB_B WITH (NOLOCK) ON B.FTBarcodeSendSuplNo=RB_B.FTBarcodeSendSuplNo LEFT OUtER JOIN"
        '_Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTReceiveSuplToBranch AS RB WITh(NOLOCK) ON RB_B.FTRcvSuplNo=Rb.FTRcvSuplNo INNER Join"
        '_Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WItH(NOLOCK) ON B.FTOrderNo=O.FTOrderNo LEFT OUtER JOIN"
        '_Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS ST WITH(NOLOCK) ON O.FNHSysStyleId=ST.FNHSysStyleId LEFT OUtER JOIN"
        '_Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp AS Com WITH(NOLOCK) ON Br.FNHSysCmpId=Com.FNHSysCmpId LEFT OUtER JOIN"
        '_Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMPart AS Part WITH(NOLOCK) ON B.FNHSysPartId=Part.FNHSysPartId LEFT OUtER JOIN"
        '_Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier AS Sulp WITh(NOLOCK) ON Br.FNHSysSuplId=Sulp.FNHSysSuplId"
        '_Qry &= vbCrLf & "LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TPRODMOperation AS MPP WITH (NOLOCK)  ON  B.FNHSysOperationId  = MPP.FNHSysOperationId"
        '_Qry &= vbCrLf & ") AS Rec"
        '_Qry &= vbCrLf & "LEFT OUtER JOIN	(SELECT Defec.FTBarcodeSendSuplNo AS BarDefec ,Defec.FNDefectQty"
        '_Qry &= vbCrLf & "FROM"
        '_Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPROSendSuplDefect AS Defec WITH(NOLOCK) "
        '_Qry &= vbCrLf & ") AS DefecQty ON Rec.FTBarcodeSendSuplNo=DefecQty.BarDefec"
        '_Qry &= vbCrLf & "LEFT OUTER JOIN	   "
        '_Qry &= vbCrLf & "(SELeCT DR.FTSendSuplNo,PS.FNExchangeRate,PSD.FNPrice,PSD.FNHSysPartId,PSD.FTPurchaseNo"
        '_Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPRODTPurchaseSendSupl AS PS WITH(NOLOCK) INNER JOIN"
        '_Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPRODTPurchaseSendSupl_Detail AS PSD WITH(NOLOCK) ON PS.FTPurchaseNo=PSD.FTPurchaseNo INNER JOIN"
        '_Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPRODTPurchaseSendSupl_DocSendRef AS DR WITH(NOLOCK) ON PSD.FTPurchaseNo=DR.FTPurchaseNo"
        '_Qry &= vbCrLf & ") AS PP ON rec.FTSendSuplNo=PP.FTSendSuplNo "
        '_Qry &= vbCrLf & "LeFT OUtER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle AS BB WITH (NOLOCK)  ON Rec.FTBarcodeBundleNo = BB.FTBarcodeBundleNo"
        '_Qry &= vbCrLf & "LEFT OUtER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMPart AS Part WITH(NOLOCK) ON rec.FNHSysPartId=Part.FNHSysPartId"

        '_Qry &= vbCrLf & "WHERE rec.FTSendSuplNo<>''"
        'If Me.FNHSysCmpId.Text <> "" Then
        '    _Qry &= vbCrLf & "AND rec.FTCmpCode='" & Me.FNHSysCmpId.Text & "'"
        'End If
        'If Me.FNHSysStyleId.Text <> "" Then
        '    _Qry &= vbCrLf & "AND rec.FTStyleCode='" & Me.FNHSysStyleId.Text & "'"
        'End If
        'If Me.FTOrderNo.Text <> "" Then
        '    _Qry &= vbCrLf & "AND rec.FTOrderNo>='" & Me.FTOrderNo.Text & "'"
        'End If
        'If Me.FTOrderNoTo.Text <> "" Then
        '    _Qry &= vbCrLf & "AND rec.FTOrderNo<='" & Me.FTOrderNoTo.Text & "'"
        'End If

        '**********************************************************************************************************************************************
        _Qry = "select  case when isnull(XA.FDInvoiceDate,'')<>'' then convert(varchar(10),convert(datetime,XA.FDInvoiceDate),103) end AS FDInvoiceDate,XA.FTInvoiceNo as POSFTInvoiceNo,Sendsulp.FTSendSuplNo"
        _Qry &= vbCrLf & ",CASE WHEN ISDATE(Sendsulp.FDSendSuplDate) = 1 THEN CONVERT(VARCHAR(10), CONVERT(DATETIME, Sendsulp.FDSendSuplDate), 103) ELSE '' END AS FDSendSuplDate"
        _Qry &= vbCrLf & ",Sendsulp.FTSuplNameTH,Sendsulp.FTSendSuplBy,Sendsulp.FTOrderNo,Sendsulp.FTPORef,Sendsulp.FTStyleCode"
        If ST.Lang.Language = ST.Lang.eLang.TH Then
            _Qry &= vbCrLf & ",Sendsulp.FTPartNameTH AS FTPartName"
            _Qry &= vbCrLf & ",Sendsulp.FTSuplNameTH AS FTSuplName"
        Else
            _Qry &= vbCrLf & ",Sendsulp.FTPartNameEN AS FTPartName"
            _Qry &= vbCrLf & ",Sendsulp.FTSuplNameEN AS FTSuplName"
        End If
        _Qry &= vbCrLf & ",sum(Sendsulp.FNQuantity) AS QtySend,RecSulp.FTRcvSuplNo,RecSulp.FTInvoiceNo,case when isnull(RecSulp.FTInvoiceDate,'')<>'' then convert(varchar(10),convert(datetime,RecSulp.FTInvoiceDate),103) end AS FTInvoiceDate,isnull(sum(RecSulp.FNQuantity),0) AS QtyRcv"
        _Qry &= vbCrLf & ",isnull(sum(DefecQty.FNDefectQty),0) AS FNDefectQty,XA.FTPurchaseNo "
        _Qry &= vbCrLf & ",isnull((XA.FNExchangeRate*XA.FNPrice),0) AS FNPrice ,(isnull(sum(RecSulp.FNQuantity),0)-isnull(sum(DefecQty.FNDefectQty),0))*isnull((XA.FNExchangeRate*XA.FNPrice),0) AS Total"
        _Qry &= vbCrLf & "from"
        _Qry &= vbCrLf & "(SELECT DISTINCT "
        _Qry &= vbCrLf & "S.FTSendSuplNo, S.FDSendSuplDate, P.FTSuplNameTH, "
        _Qry &= vbCrLf & "P.FTSuplNameEN, S.FTSendSuplBy, O.FTOrderNo, T.FTPORef, D.FTBarcodeSendSuplNo"
        _Qry &= vbCrLf & ",ST.FTStyleCode,part.FTPartNameTH,part.FTPartNameEN,BB.FNQuantity,B.FNHSysPartId,S.FNHSysCmpId"

        _Qry &= vbCrLf & ",BB.FTSizeBreakDown +'-'+  ISNULL("
        _Qry &= vbCrLf & "(SELECT TOP 1 FTNote"
        _Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_SendSupl AS X WITH(NOLOCK)"
        _Qry &= vbCrLf & "WHERE X.FTSendSuplRef = B.FTSendSuplRef ),'') AS FTNote"

        _Qry &= vbCrLf & "FROM            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTSendSupl AS S WITH (NOLOCK) LEFT OUTER JOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier AS P WITH (NOLOCK) ON S.FNHSysSuplId = P.FNHSysSuplId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTSendSupl_Barcode AS D WITH (NOLOCK) ON S.FTSendSuplNo = D.FTSendSuplNo LEFT OUTER JOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcode_SendSupl AS B WITH (NOLOCK) ON D.FTBarcodeSendSuplNo = B.FTBarcodeSendSuplNo LEFT OUTER JOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd AS O WITH (NOLOCK) ON B.FTOrderProdNo = O.FTOrderProdNo LEFT OUTER JOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS T WITH (NOLOCK) ON O.FTOrderNo = T.FTOrderNo LEFT OUTER JOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.V_TPRODTBundle_MainBarcode AS BB WITH(NOLOCK) ON B.FTBarcodeBundleNo=BB.FTBarcodeBundleNo LEFT OUTER JOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMPart AS part WITH(NOLOCK) ON b.FNHSysPartId=part.FNHSysPartId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS ST WITH(NOLOCK) ON T.FNHSysStyleId=ST.FNHSysStyleId"
        _Qry &= vbCrLf & ") AS Sendsulp LEFT OUTER JOIN"

        _Qry &= vbCrLf & "(SELECT DISTINCT  A.FTRcvSuplNo, AA.FTInvoiceNo,  AA.FTInvoiceDate AS FTInvoiceDate, P.FTOrderNo,A.FTBarcodeSendSuplNo,BB.FNQuantity"
        _Qry &= vbCrLf & "FROM            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTReceiveSupl_Barcode AS A WITH (NOLOCK) LEFT OUTER JOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.V_TPRODTReceiveSupl AS AA WITH(NOLOCK) ON A.FTRcvSuplNo=AA.FTRcvSuplNo LEFT OUTER JOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcode_SendSupl AS B WITH (NOLOCK) ON A.FTBarcodeSendSuplNo = B.FTBarcodeSendSuplNo LEFT OUTER JOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.V_TPRODTBundle_MainBarcode AS BB WITH(NOLOCK) ON B.FTBarcodeBundleNo=BB.FTBarcodeBundleNo LEFT OUTER JOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd AS P WITH (NOLOCK) ON B.FTOrderProdNo = P.FTOrderProdNo"
        _Qry &= vbCrLf & ") AS RecSulp ON Sendsulp.FTBarcodeSendSuplNo=RecSulp.FTBarcodeSendSuplNo and Sendsulp.FTOrderNo=RecSulp.FTOrderNo LEFT OUTER JOIN"

        _Qry &= vbCrLf & "(SELECT Defec.FTBarcodeSendSuplNo,Defec.FNDefectQty"
        _Qry &= vbCrLf & "FROM"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPROSendSuplDefect AS Defec WITH(NOLOCK)"
        _Qry &= vbCrLf & ") AS DefecQty ON RecSulp.FTBarcodeSendSuplNo=DefecQty.FTBarcodeSendSuplNo RIGHT JOIN"


        _Qry &= vbCrLf & "(SELECT X.*,PSD.FNPrice"
        _Qry &= vbCrLf & "FROM"
        _Qry &= vbCrLf & "(SELECT DR.FTSendSuplNo,PS.FNExchangeRate,PS.FTInvoiceNo,PS.FDInvoiceDate,PS.FTPurchaseNo,A.FNHSysPartId,A.FNSendSuplType,A.FTNote,A.FNHSysSuplId"
        _Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPRODTPurchaseSendSupl AS PS WITH(NOLOCK)"
        _Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPRODTPurchaseSendSupl_DocSendRef AS DR WITH(NOLOCK) ON PS.FTPurchaseNo=DR.FTPurchaseNo"
        _Qry &= vbCrLf & "LEFT OUTER JOIN"
        _Qry &= vbCrLf & "( SELECT M.FNHSysPartId,M.FNSendSuplType,M.FTSendSuplNo,FTNote AS FTNote,M.FNHSysSuplId"
        _Qry &= vbCrLf & "FROM"
        _Qry &= vbCrLf & "( SELECT BSS.FNHSysPartId,BSS.FNSendSuplType,O.FTOrderNo,BD.FNQuantity,PS.FTSendSuplNo,PS.FNHSysSuplId,"
        _Qry &= vbCrLf & "BD.FTSizeBreakDown +'-'+  ISNULL("
        _Qry &= vbCrLf & "(SELECT TOP 1 FTNote"
        _Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_SendSupl AS X WITH(NOLOCK)"
        _Qry &= vbCrLf & "WHERE X.FTSendSuplRef = BSS.FTSendSuplRef ),'') AS FTNote"
        _Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTSendSupl AS PS WITH(NOLOCK)"
        _Qry &= vbCrLf & "INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTSendSupl_Barcode AS PSB WITH(NOLOCK) ON PS.FTSendSuplNo = PSB.FTSendSuplNo"
        _Qry &= vbCrLf & "INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcode_SendSupl AS BSS WITH(NOLOCK) ON PSB.FTBarcodeSendSuplNo = BSS.FTBarcodeSendSuplNo"
        _Qry &= vbCrLf & "INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.V_TPRODTBundle_MainBarcode AS BD WITH(NOLOCK) ON BSS.FTBarcodeBundleNo = BD.FTBarcodeBundleNo"
        _Qry &= vbCrLf & "INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd AS ODP WITH(NOLOCK) ON BD.FTOrderProdNo = ODP.FTOrderProdNo"
        _Qry &= vbCrLf & "INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH(NOLOCK) ON ODP.FTOrderNo = O.FTOrderNo ) AS M"
        _Qry &= vbCrLf & "GROUP BY M.FNHSysPartId,M.FNSendSuplType,M.FTNote,M.FTSendSuplNo,M.FNHSysSuplId ) AS A ON DR.FTSendSuplNo=A.FTSendSuplNo ) AS X "
        _Qry &= vbCrLf & "LEFT OUtER jOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPRODTPurchaseSendSupl_Detail AS PSD WITH(NOLOCK) ON X.FNHSysPartId=PSD.FNHSysPartId and X.FNSendSuplType=PSD.FNSendSuplType and X.FTNote=PSD.FTNote "
        _Qry &= vbCrLf & "and X.FTPurchaseNo=PSD.FTPurchaseNo"
        _Qry &= vbCrLf & ") AS XA ON Sendsulp.FTSendSuplNo=XA.FTSendSuplNo and Sendsulp.FNHSysPartId=XA.FNHSysPartId"

        _Qry &= vbCrLf & " and Sendsulp.FTNote = xa.FTNote "

        '_Qry &= vbCrLf & "(SELeCT DR.FTSendSuplNo,PS.FNExchangeRate,PSD.FNPrice,PSD.FTPurchaseNo,PSD.FNHSysPartId,PS.FTInvoiceNo,PS.FDInvoiceDate"
        '_Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPRODTPurchaseSendSupl AS PS WITH(NOLOCK) INNER JOIN"
        '_Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPRODTPurchaseSendSupl_Detail AS PSD WITH(NOLOCK) ON PS.FTPurchaseNo=PSD.FTPurchaseNo INNER JOIN"
        '_Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPRODTPurchaseSendSupl_DocSendRef AS DR WITH(NOLOCK) ON PSD.FTPurchaseNo=DR.FTPurchaseNo"
        '_Qry &= vbCrLf & ") AS PP ON Sendsulp.FTSendSuplNo=PP.FTSendSuplNo and Sendsulp.FNHSysPartId=PP.FNHSysPartId"

        _Qry &= vbCrLf & "WHERE Sendsulp.FTSendSuplNo<>''  AND Sendsulp.FNHSysCmpId='" & _Cmp & "'"
        'If Me.FNHSysCmpId.Text <> "" Then
        '    _Qry &= vbCrLf & "AND Sendsulp.FTCmpCode='" & Me.FNHSysCmpId.Text & "'"
        'End If
        'If Me.FTInvoiceNo.Text <> "" Then
        '    _Qry &= vbCrLf & "AND XA.FTInvoiceNo>='" & Me.FTInvoiceNo.Text & "'"
        'End If
        'If Me.FTInvoiceNoTo.Text <> "" Then
        '    _Qry &= vbCrLf & "AND XA.FTInvoiceNo<='" & Me.FTInvoiceNoTo.Text & "'"
        'End If
        If Me.FTInvoiceNo.Text <> "" Then
            _Qry &= vbCrLf & "AND XA.FTPurchaseNo>='" & Me.FTInvoiceNo.Properties.Tag & "'"
        End If
        If Me.FTInvoiceNoTo.Text <> "" Then
            _Qry &= vbCrLf & "AND XA.FTPurchaseNo<='" & Me.FTInvoiceNoTo.Properties.Tag & "'"
        End If
        If Me.FNHSysSuplId.Text <> "" Then
            _Qry &= vbCrLf & "AND XA.FNHSysSuplId=" & Me.FNHSysSuplId.Properties.Tag & ""
        End If
        'If Me.FNHSysSuplIdTo.Text <> "" Then
        '    _Qry &= vbCrLf & "AND XA.FNHSysSuplId<=" & _SupTo & ""
        'End If
        If Me.SFDSendSuplDate.Text <> "" Then
            _Qry &= vbCrLf & "AND Sendsulp.FDSendSuplDate>='" & HI.UL.ULDate.ConvertEnDB(Me.SFDSendSuplDate.Text) & "'"
        End If
        If Me.EFDSendSuplDate.Text <> "" Then
            _Qry &= vbCrLf & "AND Sendsulp.FDSendSuplDate<='" & HI.UL.ULDate.ConvertEnDB(Me.EFDSendSuplDate.Text) & "'"
        End If
        If Me.SFDRcvInvoiceDate.Text <> "" Then
            _Qry &= vbCrLf & "AND RecSulp.FTInvoiceDate>='" & HI.UL.ULDate.ConvertEnDB(Me.SFDRcvInvoiceDate.Text) & "'"
        End If
        If Me.EFDRcvInvoiceDate.Text <> "" Then
            _Qry &= vbCrLf & "AND RecSulp.FTInvoiceDate<='" & HI.UL.ULDate.ConvertEnDB(Me.EFDRcvInvoiceDate.Text) & "'"
        End If

        _Qry &= vbCrLf & "group by Sendsulp.FTSendSuplNo,Sendsulp.FDSendSuplDate,Sendsulp.FTSuplNameTH,Sendsulp.FTSendSuplBy,Sendsulp.FTOrderNo,Sendsulp.FTPORef,Sendsulp.FTStyleCode,Sendsulp.FTPartNameTH"
        If ST.Lang.Language = ST.Lang.eLang.TH Then
            _Qry &= vbCrLf & ",Sendsulp.FTPartNameTH"
            _Qry &= vbCrLf & ",Sendsulp.FTSuplNameTH"
        Else
            _Qry &= vbCrLf & ",Sendsulp.FTPartNameEN"
            _Qry &= vbCrLf & ",Sendsulp.FTSuplNameEN"
        End If
        _Qry &= vbCrLf & ",RecSulp.FTRcvSuplNo,RecSulp.FTInvoiceNo,RecSulp.FTInvoiceDate,XA.FTPurchaseNo,XA.FNExchangeRate,XA.FNPrice,XA.FDInvoiceDate,XA.FTInvoiceNo"
        _Qry &= vbCrLf & "ORDER BY Sendsulp.FTSendSuplNo ASC,XA.FTPurchaseNo ASC"

        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PUR)
        ogc.DataSource = _dt
        _Spls.Close()

    End Sub

    Private Function CheckCriteria()
        If Me.FTInvoiceNo.Text = "" And Me.FNHSysSuplId.Text = "" And Me.SFDSendSuplDate.Text = "" And Me.EFDSendSuplDate.Text = "" And Me.SFDRcvInvoiceDate.Text = "" And Me.EFDRcvInvoiceDate.Text = "" Then
            Return False
        Else
            Return True
        End If
    End Function

    Private Sub SetGridSumery()
        Dim _FieldSum As String = "FNQuantity|RecFNQuantity"
        Dim _FieldSumCustom As String = "Total"

        With bgv
            For Each Str As String In _FieldSum.Split("|")
                If Str <> "" Then
                    .Columns(Str).Summary.Add(SummaryItemType.Sum, Str)
                    .Columns(Str).SummaryItem.DisplayFormat = "{0:n0}"
                End If
            Next
            For Each Str As String In _FieldSumCustom.Split("|")
                If Str <> "" Then
                    .Columns(Str).Summary.Add(SummaryItemType.Custom, Str)
                    .Columns(Str).SummaryItem.DisplayFormat = "{0:n2}"
                End If
            Next
            .OptionsView.ShowFooter = True
            .OptionsView.GroupFooterShowMode = GroupFooterShowMode.VisibleAlways
        End With
    End Sub

    Private Sub SetformatCell()
        Dim _Column As String = "FNQuantity|RecFNQuantity"

        With bgv
            'For Each Str As String In _Column.Split("|")
            'If Str() <> "" Then
            For i As Integer = .Columns.Count - 1 To 0 Step -1
                Select Case .Columns(i).FieldName.ToString
                    Case "FNQuantity", "RecFNQuantity"
                        .Columns(i).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                        .Columns(i).DisplayFormat.FormatString = "{0:n0}"
                End Select
            Next


            '.Columns.ColumnByFieldName("FNQuantity").DisplayFormat.FormatString = "N0"
            '.Columns.ColumnByFieldName("FNQuantity").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric

            '.Columns.ColumnByFieldName("RecFNQuantity").DisplayFormat.FormatString = "N0"
            '.Columns.ColumnByFieldName("RecFNQuantity").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            'End If
            'Next
        End With

    End Sub

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ocmclear_Click(sender As Object, e As EventArgs) Handles ocmclear.Click
        'HI.TL.HandlerControl.ClearControl(Me)
        '

        Me.FTInvoiceNo.Text = ""
        Me.FTInvoiceNoTo.Text = ""
        Me.FNHSysSuplId.Text = ""
        Me.SFDSendSuplDate.Text = ""
        Me.EFDSendSuplDate.Text = ""
        Me.SFDRcvInvoiceDate.Text = ""
        Me.EFDRcvInvoiceDate.Text = ""
        ogc.DataSource = Nothing
    End Sub

    Private Sub ocmload_Click(sender As Object, e As EventArgs) Handles ocmload.Click

        If CheckCriteria() Then
            Call LoadData()
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text)
        End If
    End Sub

    Private Sub ocmpreview_Click(sender As Object, e As EventArgs) Handles ocmpreview.Click
        Dim _Formular As String = ""
        Try
            If FTInvoiceNo.Text <> "" Or FNHSysSuplId.Text <> "" Then
                With New RP.Report
                    .FormTitle = Me.Text
                    .ReportFolderName = "Account\"
                    .ReportName = "TrackSendSupl.rpt"
                    If FTInvoiceNo.Text <> "" And FNHSysSuplId.Text <> "" Then
                        _Formular = "{V_TrackSendSupl.POSFTInvoiceNo}='" & FTInvoiceNo.Text & "' and {V_TrackSendSupl.FTPurchaseNo}='" & FTInvoiceNo.Properties.Tag & "'"
                        _Formular += " and {V_TrackSendSupl.FTSuplCode}='" & FNHSysSuplId.Text & "'"
                    Else
                        If FTInvoiceNo.Text <> "" Then
                            _Formular = "{V_TrackSendSupl.POSFTInvoiceNo}='" & FTInvoiceNo.Text & "' and {V_TrackSendSupl.FTPurchaseNo}='" & FTInvoiceNo.Properties.Tag & "'"
                        End If
                        If FNHSysSuplId.Text <> "" Then
                            _Formular += " {V_TrackSendSupl.FTSuplCode}='" & FNHSysSuplId.Text & "'"
                        End If
                    End If
                    .Formular = _Formular
                    .Preview()
                End With
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub wTrackSendSupl_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.FNHSysCmpId.Text = HI.ST.SysInfo.CmpCode
        Catch ex As Exception

        End Try
    End Sub
End Class
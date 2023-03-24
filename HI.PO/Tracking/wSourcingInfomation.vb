Public Class wSourcingInfomation

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Function LoadData() As Boolean
        Dim _Qry As String = ""
        Dim dt As DataTable
        'modify 20160701 10.07
        'modify by noh 20190815 add transaction


        Dim StatePORcv As Boolean = False
        _Qry = "    DECLARE @TabOrderNo TABLE(  FTOrderNo nvarchar(30)  UNIQUE NONCLUSTERED ([FTOrderNo])  )   "



        If Me.FTStartPODate.Text <> "" Or Me.FTPODateTo.Text <> "" Or Me.FTStartRCVDate.Text <> "" Or Me.FTRCVDateTo.Text <> "" Then
            StatePORcv = True

            If Me.FTStartPODate.Text <> "" Or Me.FTPODateTo.Text <> "" Then
                _Qry &= vbCrLf & " INSERT INTO @TabOrderNo(FTOrderNo) "
                _Qry &= vbCrLf & " SELECT DISTINCT PD.FTOrderNo "
                _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase AS P with(nolock) "
                _Qry &= vbCrLf & " INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "]..TPURTPurchase_OrderNo AS PD WITH(NOLOCK) ON P.FTPurchaseNo=PD.FTPurchaseNo "
                _Qry &= vbCrLf & " WHERE P.FTPurchaseNo <>'' "

                If Me.FTStartPODate.Text <> "" Then
                    _Qry &= vbCrLf & "AND P.FDPurchaseDate  >='" & HI.UL.ULDate.ConvertEnDB(Me.FTStartPODate.Text) & "'"
                End If
                If Me.FTPODateTo.Text <> "" Then
                    _Qry &= vbCrLf & "AND P.FDPurchaseDate <='" & HI.UL.ULDate.ConvertEnDB(Me.FTPODateTo.Text) & "'"
                End If

            End If

            If Me.FTStartRCVDate.Text <> "" Or Me.FTRCVDateTo.Text <> "" Then

                _Qry &= vbCrLf & " INSERT INTO @TabOrderNo(FTOrderNo) "
                _Qry &= vbCrLf & "SELECT DISTINCT A.FTOrderNo "
                _Qry &= vbCrLf & " FROM ( SELECT DISTINCT RD.FTOrderNo "
                _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive AS R with(nolock) "
                _Qry &= vbCrLf & " INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "]..TINVENReceive_Detail_Order AS RD WITH(NOLOCK) ON R.FTReceiveNo=RD.FTReceiveNo "
                _Qry &= vbCrLf & " WHERE R.FTReceiveNo <>'' "

                If Me.FTStartRCVDate.Text <> "" Then
                    _Qry &= vbCrLf & "AND R.FDReceiveDate >='" & HI.UL.ULDate.ConvertEnDB(Me.FTStartRCVDate.Text) & "'"
                End If

                If Me.FTRCVDateTo.Text <> "" Then
                    _Qry &= vbCrLf & "AND R.FDReceiveDate <='" & HI.UL.ULDate.ConvertEnDB(Me.FTRCVDateTo.Text) & "'"
                End If

                _Qry &= vbCrLf & " ) AS A  LEFT OUTER JOIN @TabOrderNo AS X  ON A.FTOrderNo =X.FTOrderNo   WHERE X.FTOrderNo IS NULL"

            End If
        End If

        _Qry &= vbCrLf & "   DECLARE @Tab TABLE(  FTOrderNo nvarchar(30) ,FTPORef nvarchar(500),FTCustomerPO nvarchar(500) UNIQUE NONCLUSTERED ([FTOrderNo])  )   "
        _Qry &= vbCrLf & " INSERT INTO @Tab(FTOrderNo,FTPORef,FTCustomerPO) "
        _Qry &= vbCrLf & " Select O.FTOrderNo,[HITECH_PURCHASE].dbo.fn_CustomerPO(O.FTOrderNo) AS FTPORef,Max(V.FTPORef) AS FTPORef "
        _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder As O With(NOLOCK) "

        If StatePORcv Then
            _Qry &= vbCrLf & "  INNER JOIN   @TabOrderNo  As Ox1  ON O.FTOrderNo = Ox1.FTOrderNo "
        End If

        _Qry &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMBuy As B With(nolock) On O.FNHSysBuyId = B.FNHSysBuyId"
        _Qry &= vbCrLf & " LEFT OUtER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTStyle As ST With(NOLOCK) ON  O.FNHSysStyleId=ST.FNHSysStyleId"
        _Qry &= vbCrLf & " INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.V_OrderSub_BreakDown_ShipDestination AS V WITH(NOLOCK) ON O.FTOrderNo=V.FTOrderNo"

        _Qry &= vbCrLf & "where O.FTOrderNo Is Not NULL"

        If Me.FNHSysBuyId.Text <> "" Then
            _Qry &= vbCrLf & "And B.FTBuyCode>='" & HI.UL.ULF.rpQuoted(Me.FNHSysBuyId.Text) & "'"
        End If

        If Me.FNHSysBuyIdTo.Text <> "" Then
            _Qry &= vbCrLf & "AND B.FTBuyCode<='" & HI.UL.ULF.rpQuoted(Me.FNHSysBuyIdTo.Text) & "'"
        End If

        If Me.FNHSysStyleId.Text <> "" Then
            _Qry &= vbCrLf & "AND ST.FTStyleCode>= '" & HI.UL.ULF.rpQuoted(Me.FNHSysStyleId.Text) & "'"
        End If

        If Me.FNHSysStyleIdTo.Text <> "" Then
            _Qry &= vbCrLf & "AND ST.FTStyleCode<='" & HI.UL.ULF.rpQuoted(Me.FNHSysStyleIdTo.Text) & "'"
        End If

        If Me.FTCustomerPO.Text <> "" Then
            _Qry &= vbCrLf & "AND V.FTPORef>='" & HI.UL.ULF.rpQuoted(Me.FTCustomerPO.Text) & "'"
        End If

        If Me.FTCustomerPO_To.Text <> "" Then
            _Qry &= vbCrLf & "AND V.FTPORef<='" & HI.UL.ULF.rpQuoted(Me.FTCustomerPO_To.Text) & "'"
        End If

        If Me.FTOrderNo.Text <> "" Then
            _Qry &= vbCrLf & "AND O.FTOrderNo>='" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "'"
        End If

        If Me.FTOrderNoTo.Text <> "" Then
            _Qry &= vbCrLf & "AND O.FTOrderNo<='" & HI.UL.ULF.rpQuoted(Me.FTOrderNoTo.Text) & "'"
        End If
        '--- new Criteria 
        If Me.FTStartShipDate.Text <> "" Then
            _Qry &= vbCrLf & "AND V.FDShipDate >='" & HI.UL.ULDate.ConvertEnDB(Me.FTStartShipDate.Text) & "'"
        End If

        If Me.FTShipDateTo.Text <> "" Then
            _Qry &= vbCrLf & "AND V.FDShipDate <='" & HI.UL.ULDate.ConvertEnDB(Me.FTShipDateTo.Text) & "'"
        End If

        _Qry &= vbCrLf & "  GROUP BY O.FTOrderNo "



        _Qry &= vbCrLf & "    DECLARE @tmpRsv TABLE(  FTReserveNo nvarchar(30) ,FTOrderNo nvarchar(30),FNHSysRawMatId int UNIQUE NONCLUSTERED ([FTReserveNo],[FTOrderNo],[FNHSysRawMatId])  )   "
        _Qry &= vbCrLf & "    DECLARE @tmpRsvTrans TABLE( FTOrderNo nvarchar(30),FNHSysRawMatId int,FTReserveNo  nvarchar(MAX)  UNIQUE NONCLUSTERED ([FTOrderNo],[FNHSysRawMatId])  )   "
        _Qry &= vbCrLf & " INSERT INTO @tmpRsv (FTReserveNo,FTOrderNo,FNHSysRawMatId) "
        _Qry &= vbCrLf & " 	SELECT R.FTReserveNo ,R.FTOrderNo ,B.FNHSysRawMatId "

        _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReserve AS R WITH(NOLOCK)"
        _Qry &= vbCrLf & " INNER JOIN   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT AS BO WITH(NOLOCK) ON R.FTReserveNo = BO.FTDocumentNo "
        _Qry &= vbCrLf & " INNER JOIN   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode AS B WITH(NOLOCK) ON BO.FTBarcodeNo = B.FTBarcodeNo"
        _Qry &= vbCrLf & " 	INNER JOIN @Tab AS T ON R.FTOrderNo = T.FTOrderNo  "
        _Qry &= vbCrLf & " GROUP BY R.FTReserveNo ,R.FTOrderNo ,B.FNHSysRawMatId "
        _Qry &= vbCrLf & " 	order by  R.FTOrderNo asc  ,B.FNHSysRawMatId asc  "




        _Qry &= vbCrLf & " INSERT INTO @tmpRsvTrans (FTOrderNo,FNHSysRawMatId,FTReserveNo) "
        _Qry &= vbCrLf & " Select  FTOrderNo ,FNHSysRawMatId"
        _Qry &= vbCrLf & " , STUFF(    (SELECT ',' + FTReserveNo "
        _Qry &= vbCrLf & "    FROM (Select Distinct  FTReserveNo  From @tmpRsv  t1  "
        _Qry &= vbCrLf & "    WHERE t1.FTOrderNo = D.FTOrderNo and t1.FNHSysRawMatId = D.FNHSysRawMatId      ) t1     FOR XML PATH (''))       , 1, 1, '')   as FTReserveNo   "
        _Qry &= vbCrLf & " 	from @tmpRsv as D "
        _Qry &= vbCrLf & "group by FTOrderNo ,FNHSysRawMatId "
        _Qry &= vbCrLf & " order by FTOrderNo asc  "
        _Qry &= vbCrLf & " "
        _Qry &= vbCrLf & " "


        _Qry &= vbCrLf & "    DECLARE @TmpTrans TABLE(  FTDocumentNo nvarchar(30) ,FTOrderNo nvarchar(30),FNHSysRawMatId int UNIQUE NONCLUSTERED ([FTDocumentNo],[FTOrderNo],[FNHSysRawMatId])  )   "
        _Qry &= vbCrLf & " INSERT INTO @TmpTrans (FTDocumentNo,FTOrderNo,FNHSysRawMatId) "
        _Qry &= vbCrLf & " Select B.FTDocumentNo ,   B.FTOrderNo , BO.FNHSysRawMatId"
        _Qry &= vbCrLf & " From @Tab AS A INNER JOIN  "
        _Qry &= vbCrLf & " " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & ".dbo.TINVENBarcode_OUT AS B  with(nolock) ON A.FTOrderNo = B.FTOrderNo "
        _Qry &= vbCrLf & " INNER JOIN " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & ".dbo. TINVENTransferWH AS T with(nolock) ON B.FTDocumentNo = T.FTTransferWHNo "
        _Qry &= vbCrLf & " INNER JOIN " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & ".dbo.TINVENBarcode  AS BO  with(nolock) ON B.FTBarcodeNo = BO.FTBarcodeNo"
        _Qry &= vbCrLf & " where isnull(B.FTDocumentNo,'') <> ''  "

        If Me.FTStartTRWDate.Text <> "" Then
            _Qry &= vbCrLf & "AND T.FDTransferWHDate >='" & HI.UL.ULDate.ConvertEnDB(Me.FTStartTRWDate.Text) & "'"
        End If
        If Me.FTTRWDateTo.Text <> "" Then
            _Qry &= vbCrLf & "AND T.FDTransferWHDate <='" & HI.UL.ULDate.ConvertEnDB(Me.FTTRWDateTo.Text) & "'"
        End If

        _Qry &= vbCrLf & " group by  B.FTDocumentNo ,   B.FTOrderNo , BO.FNHSysRawMatId "
        _Qry &= vbCrLf & " "

        _Qry &= vbCrLf & "  select FTOrderNo ,FNHSysRawMatId "
        _Qry &= vbCrLf & " into #t"
        _Qry &= vbCrLf & "  from @TmpTrans "
        _Qry &= vbCrLf & "  group by FTOrderNo ,FNHSysRawMatId "
        _Qry &= vbCrLf & " "
        _Qry &= vbCrLf & " "


        _Qry &= vbCrLf & "    DECLARE @TmpTransTRW TABLE( FTOrderNo nvarchar(30),FNHSysRawMatId int,FTDocumentNo  nvarchar(MAX)  UNIQUE NONCLUSTERED ([FTOrderNo],[FNHSysRawMatId])  )   "
        _Qry &= vbCrLf & " INSERT INTO @TmpTransTRW (FTOrderNo,FNHSysRawMatId,FTDocumentNo) "
        '_Qry &= vbCrLf & " Select  FTOrderNo ,FNHSysRawMatId"
        '_Qry &= vbCrLf & " , STUFF(    (SELECT ',' + FTDocumentNo "
        '_Qry &= vbCrLf & "    FROM (Select Distinct  FTDocumentNo , FTOrderNo , FNHSysRawMatId  From @TmpTrans   ) t1    "
        '_Qry &= vbCrLf & "    WHERE t1.FTOrderNo = D.FTOrderNo and t1.FNHSysRawMatId = D.FNHSysRawMatId      FOR XML PATH (''))       , 1, 1, '')   as FTDocumentNo   "

        '_Qry &= vbCrLf & " 	from @TmpTrans as D "
        '_Qry &= vbCrLf & "group by FTOrderNo ,FNHSysRawMatId "
        '_Qry &= vbCrLf & " order by FTOrderNo asc  "

        _Qry &= vbCrLf & " Select  FTOrderNo ,FNHSysRawMatId"
        _Qry &= vbCrLf & " , STUFF(    (SELECT ',' + FTDocumentNo "
        _Qry &= vbCrLf & "    FROM (Select Distinct  FTDocumentNo    From @TmpTrans    t1    "
        _Qry &= vbCrLf & "    WHERE t1.FTOrderNo = D.FTOrderNo and t1.FNHSysRawMatId = D.FNHSysRawMatId  ) t1    FOR XML PATH (''))       , 1, 1, '')   as FTDocumentNo   "

        _Qry &= vbCrLf & " 	from  #t as D "
        _Qry &= vbCrLf & "group by FTOrderNo ,FNHSysRawMatId "
        _Qry &= vbCrLf & " order by FTOrderNo asc  "
        _Qry &= vbCrLf & " drop table  #t"

        _Qry &= vbCrLf & " SELECT Re.FNHSysRawMatId,Re.FTOrderNo,O.FNHSysStyleId,O.FNHSysBuyId,O.FNHSysSeasonId"
        _Qry &= vbCrLf & ",X.FTPORef"
        _Qry &= vbCrLf & ",X.FTPORef AS FTCustomerPO,PO.FTPurchaseNo"
        _Qry &= vbCrLf & " into #TmpOrderNO "
        _Qry &= vbCrLf & "from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder_Resource AS Re WITH(NOLOCK)INNER JOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O with(nolock) ON re.FTOrderNo = O.FTOrderNo"
        _Qry &= vbCrLf & "   LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo AS PO with(nolock) ON  re.FTOrderNo=PO.FTOrderNo and re.FNHSysRawMatId=PO.FNHSysRawMatId"
        _Qry &= vbCrLf & "   LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase AS P with(nolock) ON PO.FTPurchaseNo=P.FTPurchaseNo"
        _Qry &= vbCrLf & "   INNER Join @Tab AS X ON Re.FTOrderNo = X.FTOrderNo "
        _Qry &= vbCrLf & " union "
        _Qry &= vbCrLf & "SELECT PO.FNHSysRawMatId,PO.FTOrderNo,O.FNHSysStyleId,O.FNHSysBuyId,O.FNHSysSeasonId"
        _Qry &= vbCrLf & ",X.FTPORef"
        _Qry &= vbCrLf & ",X.FTPORef AS FTCustomerPO,PO.FTPurchaseNo"
        _Qry &= vbCrLf & "from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo AS PO WITH(NOLOCK) LEFT OUTER JOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O with(nolock) ON PO.FTOrderNo = O.FTOrderNo INNER JOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase AS P with(nolock) ON PO.FTPurchaseNo=P.FTPurchaseNo"
        _Qry &= vbCrLf & "   INNER Join @Tab AS X ON PO.FTOrderNo = X.FTOrderNo "
        _Qry &= vbCrLf & "where PO.FNHSysRawMatId Is Not NULL"




        _Qry &= vbCrLf & " SELECt distinct UN.*,ISNULL(PO.FTPurchaseNo,'') as FTPurchaseNo,ISNULL(SUP.FTSuplCode,'') AS FTSuplCode,ISNULL(SC.FNOptiplanQuantity,0) AS FNOptiplanQuantity"
        '_Qry &= vbCrLf & ",ISNULL((SELECT [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.fn_Sourcing_FTReserveNo(UN.FTOrderNo,UN.FNHSysRawMatId) ),'')   AS FTReserveNo,ST.FTStyleCode"
        _Qry &= vbCrLf & "  ,isnull(T.FTReserveNo,'') as FTReserveNo,ST.FTStyleCode "

        If ST.Lang.Language = ST.Lang.eLang.TH Then

            _Qry &= vbCrLf & ",RM.FTRawMatNameTH  AS FTRawMatName"
            _Qry &= vbCrLf & ",RC.FTRawMatColorNameTH AS FTRawMatColorName"

        Else

            _Qry &= vbCrLf & ",RM.FTRawMatNameEN  AS FTRawMatName"
            _Qry &= vbCrLf & ",RC.FTRawMatColorNameEN AS FTRawMatColorName"

        End If

        _Qry &= vbCrLf & ", RC.FTRawMatColorCode, RS.FTRawMatSizeCode"
        _Qry &= vbCrLf & ",SSA.FTSeasonCode, RM.FTRawMatCode"
        _Qry &= vbCrLf & ",ISNULL(Re.FNUsedQuantity,0) AS FNUsedQuantity,ISNULL(RCV_D.FNQuantity,0) AS FNRevQuantity,ISNULL(PO.FNQuantity,0) AS FNQuantity,PO.FNPrice    , Convert(numeric(18,2),ISNULL(PO.FNQuantity,0) * ISNULL(PO.FNPrice,0))   AS FNAmt"
        _Qry &= vbCrLf & ",ISNULL(PO.FNQuantity,0) - ISNULL(RCV_D.FNQuantity,0) as FNBalance,URE.FTUnitCode as FTUnitCodeUse,UPO.FTUnitCode AS FTUnitCode"
        _Qry &= vbCrLf & ",RE.FTFabricFrontSize,convert(varchar(10),convert(date,OCS.FDShipDate),103) AS FDShipDate"
        _Qry &= vbCrLf & ",convert(varchar(10),convert(date,P.FDDeliveryDate),103) as FDDeliveryDate"
        _Qry &= vbCrLf & "  ,BO.FTDocumentNo ,Cur.FTCurCode"
        _Qry &= vbCrLf & "FROM  #TmpOrderNO AS UN LEFT OUtER JOIN"
        _Qry &= vbCrLf & "(sELeCT PO.FTPurchaseNo,PO.FNHSysRawMatId,PO.FTOrderNo,PO.FNQuantity,PO.FNHSysUnitId,PO.FNPrice"
        _Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "]..TPURTPurchase_OrderNo AS PO WITH(NOLOCK) INNER JOIN @Tab AS X ON PO.FTOrderNo = X.FTOrderNo) AS PO ON UN.FNHSysRawMatId=PO.FNHSysRawMatId AND UN.FTOrderNo=PO.FTOrderNo AND UN.FTPurchaseNo=PO.FTPurchaseNo"
        _Qry &= vbCrLf & "     LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase AS P with(nolock) ON PO.FTPurchaseNo=P.FTPurchaseNo"
        _Qry &= vbCrLf & "     LEFT OUtER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder_Resource AS Re WITH(NOLOCK) ON UN.FTOrderNo=Re.FTOrderNo AND UN.FNHSysRawMatId=Re.FNHSysRawMatId"
        _Qry &= vbCrLf & "     LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTOrder_Sourcing AS SC WItH(NOLOCK) ON UN.FTOrderNo=SC.FTOrderNo AND UN.FNHSysRawMatId=SC.FNHSysRawMatId"
        _Qry &= vbCrLf & "     LEFT OUtER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail_Order AS RCV_D WITH(NOLOCK) ON UN.FNHSysRawMatId=RCV_D.FNHSysRawMatId AND UN.FTOrderNo=RCV_D.FTOrderNo"
        _Qry &= vbCrLf & "     LEFT OUtER jOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS RM WITH(NOLOCK) ON UN.FNHSysRawMatId=RM.FNHSysRawMatId"
        _Qry &= vbCrLf & "     LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatSize AS RS with(nolock) ON RM.FNHSysRawMatSizeId = RS.FNHSysRawMatSizeId  "
        _Qry &= vbCrLf & "     LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatColor AS RC with(nolock) ON RM.FNHSysRawMatColorId = RC.FNHSysRawMatColorId  "
        _Qry &= vbCrLf & "     LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMBuy AS B with(nolock) ON UN.FNHSysBuyId = B.FNHSysBuyId"
        _Qry &= vbCrLf & "     LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier AS SUP WITH(NOLOCK) ON P.FNHSysSuplId=SUP.FNHSysSuplId"
        _Qry &= vbCrLf & "     LEFT OUtER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTStyle AS ST WITH(NOLOCK) ON UN.FNHSysStyleId=ST.FNHSysStyleId"
        _Qry &= vbCrLf & "     LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMSeason AS SSA WITH(NOLOCK) ON UN.FNHSysSeasonId = SSA.FNHSysSeasonId"
        _Qry &= vbCrLf & "     LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.V_TMERTOrder_Cut_ShipDate OCS with(nolock) ON UN.FTOrderNo = OCS.MFTOrderNo"
        _Qry &= vbCrLf & "     LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit AS UPO WITH(NOLOCK) ON PO.FNHSysUnitId=UPO.FNHSysUnitId"
        _Qry &= vbCrLf & "     LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit AS URE WITH(NOLOCK) ON RE.FNHSysUnitId=URE.FNHSysUnitId"
        _Qry &= vbCrLf & "     LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TFINMCurrency AS Cur WITH(NOLOCK) ON P.FNHSysCurId=Cur.FNHSysCurId"
        _Qry &= vbCrLf & "     LEFT OUTER JOIN @tmpRsvTrans AS T ON UN.FTOrderNo = T.FTOrderNo  and UN.FNHSysRawMatId = T.FNHSysRawMatId "
        _Qry &= vbCrLf & "     LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive AS RCV with(nolock) ON RCV_D.FTReceiveNo = RCV.FTReceiveNo "
        _Qry &= vbCrLf & "     LEFT JOIN   @TmpTransTRW AS BO  ON Un.FTOrderNo = BO.FTOrderNo and UN.FNHSysRawMatId = BO.FNHSysRawMatId "
        _Qry &= vbCrLf & " where UN.FNHSysRawMatId Is Not NULL "

        'If Me.FNHSysBuyId.Text <> "" Then
        '    _Qry &= vbCrLf & "AND B.FTBuyCode>='" & HI.UL.ULF.rpQuoted(Me.FNHSysBuyId.Text) & "'"
        'End If
        'If Me.FNHSysBuyIdTo.Text <> "" Then
        '    _Qry &= vbCrLf & "AND B.FTBuyCode<='" & HI.UL.ULF.rpQuoted(Me.FNHSysBuyIdTo.Text) & "'"
        'End If
        'If Me.FNHSysStyleId.Text <> "" Then
        '    _Qry &= vbCrLf & "AND ST.FTStyleCode>= '" & HI.UL.ULF.rpQuoted(Me.FNHSysStyleId.Text) & "'"
        'End If
        'If Me.FNHSysStyleIdTo.Text <> "" Then
        '    _Qry &= vbCrLf & "AND ST.FTStyleCode<='" & HI.UL.ULF.rpQuoted(Me.FNHSysStyleIdTo.Text) & "'"
        'End If
        'If Me.FTCustomerPO.Text <> "" Then
        '    _Qry &= vbCrLf & "AND UN.FTCustomerPO>='" & HI.UL.ULF.rpQuoted(Me.FTCustomerPO.Text) & "'"
        'End If
        'If Me.FTCustomerPO_To.Text <> "" Then
        '    _Qry &= vbCrLf & "AND UN.FTCustomerPO<='" & HI.UL.ULF.rpQuoted(Me.FTCustomerPO_To.Text) & "'"
        'End If
        'If Me.FTOrderNo.Text <> "" Then
        '    _Qry &= vbCrLf & "AND UN.FTOrderNo>='" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "'"
        'End If
        'If Me.FTOrderNoTo.Text <> "" Then
        '    _Qry &= vbCrLf & "AND UN.FTOrderNo<='" & HI.UL.ULF.rpQuoted(Me.FTOrderNoTo.Text) & "'"
        'End If

        If Me.FTStartPODate.Text <> "" Then
            _Qry &= vbCrLf & "AND P.FDPurchaseDate  >='" & HI.UL.ULDate.ConvertEnDB(Me.FTStartPODate.Text) & "'"
        End If
        If Me.FTPODateTo.Text <> "" Then
            _Qry &= vbCrLf & "AND P.FDPurchaseDate <='" & HI.UL.ULDate.ConvertEnDB(Me.FTPODateTo.Text) & "'"
        End If
        If Me.FTStartRCVDate.Text <> "" Then
            _Qry &= vbCrLf & "AND RCV.FDReceiveDate >='" & HI.UL.ULDate.ConvertEnDB(Me.FTStartRCVDate.Text) & "'"
        End If
        If Me.FTRCVDateTo.Text <> "" Then
            _Qry &= vbCrLf & "AND RCV.FDReceiveDate <='" & HI.UL.ULDate.ConvertEnDB(Me.FTRCVDateTo.Text) & "'"
        End If

        _Qry &= vbCrLf & " drop table  #TmpOrderNO"

        dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_INVEN)
        Me.ogcDetail.DataSource = dt
        Return True

    End Function

    Private Function CheckCriteria() As Boolean
        If Me.FNHSysBuyId.Text = "" And Me.FNHSysBuyIdTo.Text = "" And Me.FTCustomerPO.Text = "" And Me.FTCustomerPO_To.Text = "" _
            And FNHSysStyleId.Text = "" And FNHSysStyleIdTo.Text = "" And FTOrderNo.Text = "" And FTOrderNoTo.Text = "" And FTStartShipDate.Text = "" _
            And FTShipDateTo.Text = "" And FTStartPODate.Text = "" And FTPODateTo.Text = "" And FTStartRCVDate.Text = "" And FTRCVDateTo.Text = "" _
            And FTStartTRWDate.Text = "" And FTTRWDateTo.Text = "" Then
            Return False
        Else
            Return True
        End If
    End Function

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ocmload_Click(sender As Object, e As EventArgs) Handles ocmload.Click

        If CheckCriteria() Then
            Dim Spls As New HI.TL.SplashScreen("Please Wait Loading Data.....")
            If LoadData() Then
                Spls.Close()
            Else
                Spls.Close()
            End If
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text)
        End If

    End Sub

    Private Sub ocmclearclsr_Click(sender As Object, e As EventArgs) Handles ocmclearclsr.Click
        HI.TL.HandlerControl.ClearControl(Me)
        Me.ogcDetail.DataSource = Nothing

    End Sub

    Private Sub ogvDetail_RowCellStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles ogvDetail.RowCellStyle
        Try
            With ogvDetail
                If Double.Parse(Val(.GetRowCellValue(e.RowHandle, "FNBalance"))) <= 0 Then
                    e.Appearance.ForeColor = Drawing.Color.Green
                End If
            End With
        Catch ex As Exception

        End Try

    End Sub

    Private Sub ocmRefresh3_Click(sender As Object, e As EventArgs) Handles ocmRefresh3.Click

    End Sub

    Private Sub ocmpreview3_Click(sender As Object, e As EventArgs) Handles ocmpreview3.Click

    End Sub

    Private Sub wSourcingInfomation_Load(sender As Object, e As EventArgs) Handles Me.Load

    End Sub
End Class
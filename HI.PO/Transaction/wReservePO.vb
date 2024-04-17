Public Class wReservePO 

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

#Region "Procedure"

    Private Function Verifydata() As Boolean
        Dim _Pass As Boolean = False

        If FTPurchaseNo.Text <> "" Then
            _Pass = True
        End If

        If FTPurchaseNoTo.Text <> "" Then
            _Pass = True
        End If

        If FTStartPurchaseDate.Text <> "" Then
            _Pass = True
        End If

        If FTEndPurchaseDate.Text <> "" Then
            _Pass = True
        End If

        If FNHSysMainMatId.Text <> "" Then
            _Pass = True
        End If


        If FNHSysBuyId.Text <> "" And FNHSysBuyIdTo.Text <> "" Then
            _Pass = True
        End If

        If FNHSysSeasonId.Text <> "" Then
            _Pass = True
        End If

        Return _Pass
    End Function

    Private Sub Loaddata()
        Dim _Qry As String = ""

        _Qry = " SELECT MM.FTPurchaseNo,Cmpx.FTCmpCode,ISNULL(PHD.FTRemark,'') AS FTPORemark"
        _Qry &= vbCrLf & "  ,MM.FTOrderNo"
        _Qry &= vbCrLf & "   ,MM.FNHSysRawMatId "
        _Qry &= vbCrLf & "   ,MM.FNHSysUnitId"
        _Qry &= vbCrLf & "  ,MM.FNQuantity "
        _Qry &= vbCrLf & "  ,MM.FNQuantityRcv "
        _Qry &= vbCrLf & "   ,MM.FNQuantityRsv "
        _Qry &= vbCrLf & "   ,(MM.FNQuantity- (MM.FNQuantityRcv +MM.FNQuantityRsv )) AS FNQuantityBal"
        _Qry &= vbCrLf & "   ,IMU.FTUnitCode "
        _Qry &= vbCrLf & "   ,IM.FTRawMatCode "
        _Qry &= vbCrLf & "   ,ISNULL(MMT.FTStateNotCheckResuorce,'') AS FTStateNotCheckResuorce "
        _Qry &= vbCrLf & "  ,MM.FNPrice "

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Qry &= vbCrLf & "   ,IM.FTRawMatNameTH AS FTRawMatName "
        Else
            _Qry &= vbCrLf & "   ,IM.FTRawMatNameEN AS FTRawMatName"
        End If

        _Qry &= vbCrLf & "  ,ISNULL(IMC.FTRawMatColorCode,'' ) AS FTRawMatColorCode"
        _Qry &= vbCrLf & "  ,ISNULL(IMS.FTRawMatSizeCode,'' ) AS FTRawMatSizeCode"
        _Qry &= vbCrLf & "  ,MM.FTPurchaseNo +'|' + MM.FTOrderNo +'|' + Convert(nvarchar(30),MM.FNHSysRawMatId)  AS FTKeyRef"
        _Qry &= vbCrLf & "  "
        _Qry &= vbCrLf & "    FROM"
        _Qry &= vbCrLf & "  (SELECT A.FTPurchaseNo"
        _Qry &= vbCrLf & "  ,A.FTOrderNo"
        _Qry &= vbCrLf & "  ,A.FNHSysRawMatId"
        _Qry &= vbCrLf & "  ,ISNULL((SELECT TOP 1 FNPrice FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo AS AA WITH(NOLOCK) WHERE FTPurchaseNo=A.FTPurchaseNo AND FNHSysRawMatId=A.FNHSysRawMatId ),0) AS FNPrice "
        _Qry &= vbCrLf & "  ,ISNULL((SELECT TOP 1 FNHSysUnitId FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo AS AA WITH(NOLOCK) WHERE FTPurchaseNo=A.FTPurchaseNo AND FNHSysRawMatId=A.FNHSysRawMatId ),0) AS FNHSysUnitId"
        _Qry &= vbCrLf & "   ,ISNULL((SELECT TOP 1 FNQuantity FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTReservePurchase AS AA WITH(NOLOCK) WHERE FTPurchaseNo=A.FTPurchaseNo AND FTOrderNo=A.FTOrderNo AND FNHSysRawMatId=A.FNHSysRawMatId ),B.FNQuantity) AS FNQuantity"
        _Qry &= vbCrLf & "   ,ISNULL((SELECT SUM(RD.FNQuantity) AS FNQuantity"
        _Qry &= vbCrLf & "  FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive AS RH WITH(NOLOCK)  INNER JOIN"
        _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail_Order AS RD WITH(NOLOCK)  ON RH.FTReceiveNo = RD.FTReceiveNo"
        _Qry &= vbCrLf & "   WHERE (RH.FTPurchaseNo = A.FTPurchaseNo)"
        _Qry &= vbCrLf & "    AND (RD.FTOrderNo =A.FTOrderNo) "
        _Qry &= vbCrLf & " 	AND (RD.FNHSysRawMatId = A.FNHSysRawMatId)   ),0) AS FNQuantityRcv"
        _Qry &= vbCrLf & " 	,ISNULL((SELECT Sum(FNQuantityTo) FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTReservePurchase AS AA WITH(NOLOCK) WHERE FTPurchaseNo=A.FTPurchaseNo AND FTOrderNo=A.FTOrderNo AND FNHSysRawMatId=A.FNHSysRawMatId ),0) AS FNQuantityRsv"
        _Qry &= vbCrLf & "    FROM"
        _Qry &= vbCrLf & "  (SELECT A.FTPurchaseNo,  A.FTOrderNo,A.FNHSysRawMatId"
        _Qry &= vbCrLf & "  FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo AS A  WITH(NOLOCK) INNER JOIN"
        _Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O  WITH(NOLOCK)  ON A.FTOrderNo = O.FTOrderNo"
        _Qry &= vbCrLf & "   WHERE (O.FNOrderType IN (4,19))"
        _Qry &= vbCrLf & "   UNION "
        _Qry &= vbCrLf & "  SELECT   FTPurchaseNo, FTOrderNo, FNHSysRawMatId"
        _Qry &= vbCrLf & "  FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTReservePurchase AS A WITH(NOLOCK)"
        _Qry &= vbCrLf & "  GROUP BY  FTPurchaseNo, FTOrderNo, FNHSysRawMatId) AS A LEFT JOIN "
        _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo AS B WITH(NOLOCK) ON A.FTPurchaseNo = B.FTPurchaseNo "
        _Qry &= vbCrLf & "  AND A.FTOrderNo = B.FTOrderNo "
        _Qry &= vbCrLf & "  AND A.FNHSysRawMatId = B.FNHSysRawMatId INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase AS H WITH(NOLOCK) "
        _Qry &= vbCrLf & "  ON A.FTPurchaseNo = H.FTPurchaseNo "
        _Qry &= vbCrLf & " WHERE H.FTPurchaseNo <> '' "

        If FTPurchaseNo.Text <> "" Then
            _Qry &= vbCrLf & " AND H.FTPurchaseNo >='" & HI.UL.ULF.rpQuoted(FTPurchaseNo.Text) & "' "
        End If

        If FTPurchaseNoTo.Text <> "" Then
            _Qry &= vbCrLf & " AND H.FTPurchaseNo <='" & HI.UL.ULF.rpQuoted(FTPurchaseNoTo.Text) & "' "
        End If

        If FTStartPurchaseDate.Text <> "" Then
            _Qry &= vbCrLf & " AND H.FDPurchaseDate >='" & HI.UL.ULDate.ConvertEnDB(FTStartPurchaseDate.Text) & "' "
        End If

        If FTEndPurchaseDate.Text <> "" Then
            _Qry &= vbCrLf & " AND H.FDPurchaseDate <='" & HI.UL.ULDate.ConvertEnDB(FTEndPurchaseDate.Text) & "' "
        End If


        If FNHSysBuyId.Text <> "" Or FNHSysBuyIdTo.Text <> "" Or FNHSysSeasonId.Text <> "" Then
            _Qry &= vbCrLf & " AND  H.FTPurchaseNo IN ( "
            _Qry &= vbCrLf & " SELECT DISTINCT A.FTPurchaseNo  "
            _Qry &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase AS A WITH(NOLOCK) "
            _Qry &= vbCrLf & "   INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo As B With(NOLOCK) On A.FTPurchaseNo = B.FTPurchaseNo "
            _Qry &= vbCrLf & "   INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS Ox WITH(NOLOCK)   On B.FTOrderNo = Ox.FTOrderNo "
            _Qry &= vbCrLf & "   INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMBuy AS BxT WITH(NOLOCK)   On Ox.FNHSysBuyId = BxT.FNHSysBuyId "

            _Qry &= vbCrLf & " WHERE A.FTPurchaseNo <> '' "

            If FTPurchaseNo.Text <> "" Then
                _Qry &= vbCrLf & " AND A.FTPurchaseNo >='" & HI.UL.ULF.rpQuoted(FTPurchaseNo.Text) & "' "
            End If

            If FTPurchaseNoTo.Text <> "" Then
                _Qry &= vbCrLf & " AND A.FTPurchaseNo <='" & HI.UL.ULF.rpQuoted(FTPurchaseNoTo.Text) & "' "
            End If

            If FTStartPurchaseDate.Text <> "" Then
                _Qry &= vbCrLf & " AND A.FDPurchaseDate >='" & HI.UL.ULDate.ConvertEnDB(FTStartPurchaseDate.Text) & "' "
            End If

            If FTEndPurchaseDate.Text <> "" Then
                _Qry &= vbCrLf & " AND A.FDPurchaseDate <='" & HI.UL.ULDate.ConvertEnDB(FTEndPurchaseDate.Text) & "' "
            End If

            If FNHSysBuyId.Text <> "" Or FNHSysBuyIdTo.Text <> "" Then
                _Qry &= vbCrLf & " And BxT.FTBuyCode >='" & HI.UL.ULF.rpQuoted(FNHSysBuyId.Text) & "' "
                _Qry &= vbCrLf & " AND BxT.FTBuyCode <='" & HI.UL.ULF.rpQuoted(FNHSysBuyIdTo.Text) & "' "
            End If

            If FNHSysSeasonId.Text <> "" Then
                _Qry &= vbCrLf & " AND Ox.FNHSysSeasonId =" & Val(FNHSysSeasonId.Properties.Tag.ToString) & " "
            End If

            _Qry &= vbCrLf & " ) "

        End If


        _Qry &= vbCrLf & " ) AS MM "
        _Qry &= vbCrLf & "  INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS IM WITH(NOLOCK) ON MM.FNHSysRawMatId = IM.FNHSysRawMatId "
        _Qry &= vbCrLf & "  LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatColor AS IMC WITH(NOLOCK) ON IM.FNHSysRawMatColorId = IMC.FNHSysRawMatColorId "
        _Qry &= vbCrLf & "   LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatSize AS IMS WITH(NOLOCK) ON IM.FNHSysRawMatSizeId  = IMS.FNHSysRawMatSizeId"

        'Edit 20150417 Unit ไม่ตรงกับ PO
        _Qry &= vbCrLf & "  LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit AS IMU WITH(NOLOCK) ON MM.FNHSysUnitId  = IMU.FNHSysUnitId"
        ' _Qry &= vbCrLf & "  LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit AS IMU WITH(NOLOCK) ON IM.FNHSysUnitId  = IMU.FNHSysUnitId"

        _Qry &= vbCrLf & "  LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMainMat AS MMT WITH(NOLOCK) ON IM.FTRawMatCode = MMT.FTMainMatCode "
        _Qry &= vbCrLf & " OUTER APPLY (SELECT TOP 1 Cmp.FTCmpCode  "
        _Qry &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS Ox WITH(NOLOCK) "
        _Qry &= vbCrLf & "  INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp AS Cmp WITH(NOLOCK) ON Ox.FNHSysCmpId = Cmp.FNHSysCmpId "

        _Qry &= vbCrLf & " WHERE Ox.FTOrderNo = MM.FTOrderNo"
        _Qry &= vbCrLf & "   ) As Cmpx "


        _Qry &= vbCrLf & " OUTER APPLY (SELECT TOP 1  PHD.FTRemark  "
        _Qry &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase AS PHD WITH(NOLOCK) "

        _Qry &= vbCrLf & " WHERE PHD.FTPurchaseNo = MM.FTPurchaseNo"
        _Qry &= vbCrLf & "   ) As PHD "


        If FNHSysMainMatId.Text <> "" Then
            _Qry &= vbCrLf & "  WHERE  IM.FTRawMatCode='" & HI.UL.ULF.rpQuoted(FNHSysMainMatId.Text) & "' "
        End If

        _Qry &= vbCrLf & " ORDER BY  MM.FTPurchaseNo"
        _Qry &= vbCrLf & "  ,MM.FTOrderNo"
        _Qry &= vbCrLf & "   ,IM.FTRawMatCode "
        _Qry &= vbCrLf & "  ,ISNULL(IMC.FTRawMatColorCode,'' )"
        _Qry &= vbCrLf & "   ,ISNULL(IMS.FTRawMatSizeCode,'' )"

        Dim dt As DataTable = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PUR)
        ogcdetail.DataSource = dt.Copy()
        ogcdetail.RefreshDataSource()
        ogvdetail.RefreshData()
    End Sub

    Private Sub LoadReserveDetail(FTPurchaseKeyNo As String, FTOrderKeyNo As String, FNHSysRawMatIdKey As Integer)
        Dim _Qry As String = ""
        Try
            _Qry = " SELECT MM.FTPurchaseNo"
            _Qry &= vbCrLf & "  ,MM.FTOrderNo"
            _Qry &= vbCrLf & "   ,MM.FNHSysRawMatId "
            _Qry &= vbCrLf & "   ,MM.FNHSysUnitId"
            _Qry &= vbCrLf & "  ,MM.FNQuantity "
            _Qry &= vbCrLf & "   ,IMU.FTUnitCode "
            _Qry &= vbCrLf & "   ,IM.FTRawMatCode "

            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Qry &= vbCrLf & "   ,IM.FTRawMatNameTH AS FTRawMatName "
            Else
                _Qry &= vbCrLf & "   ,IM.FTRawMatNameEN AS FTRawMatName"
            End If

            _Qry &= vbCrLf & "  ,ISNULL(IMC.FTRawMatColorCode,'' ) AS FTRawMatColorCode"
            _Qry &= vbCrLf & "   ,ISNULL(IMS.FTRawMatSizeCode,'' ) AS FTRawMatSizeCode"
            _Qry &= vbCrLf & "    FROM"
            _Qry &= vbCrLf & "  (SELECT A.FTPurchaseNo"
            _Qry &= vbCrLf & "  ,A.FTOrderNo"
            _Qry &= vbCrLf & "  ,A.FNHSysRawMatId"
            _Qry &= vbCrLf & "  ,ISNULL((SELECT TOP 1 FNHSysUnitId FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo AS AA WITH(NOLOCK) WHERE FTPurchaseNo=A.FTPurchaseNo AND FNHSysRawMatId=A.FNHSysRawMatId ),0) AS FNHSysUnitId"
            _Qry &= vbCrLf & "  ,ISNULL(A.FNQuantity,0) AS FNQuantity"
            _Qry &= vbCrLf & "    FROM"
            _Qry &= vbCrLf & "  ( "
            _Qry &= vbCrLf & "  SELECT   FTPurchaseNo,FNHSysRawMatId,FTOrderNoTo AS FTOrderNo,FNQuantityTo AS FNQuantity "
            _Qry &= vbCrLf & "  FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTReservePurchase AS A WITH(NOLOCK)"

            _Qry &= vbCrLf & " WHERE A.FTPurchaseNo <> '' "
            _Qry &= vbCrLf & " AND A.FTPurchaseNo ='" & HI.UL.ULF.rpQuoted(FTPurchaseKeyNo) & "' "
            _Qry &= vbCrLf & " AND A.FTOrderNo='" & HI.UL.ULF.rpQuoted(FTOrderKeyNo) & "' "
            _Qry &= vbCrLf & " AND A.FNHSysRawMatId=" & FNHSysRawMatIdKey & " "

            _Qry &= vbCrLf & "  ) AS A LEFT JOIN "
            _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo AS B WITH(NOLOCK) ON A.FTPurchaseNo = B.FTPurchaseNo "
            _Qry &= vbCrLf & "  AND A.FTOrderNo = B.FTOrderNo "
            _Qry &= vbCrLf & "  AND A.FNHSysRawMatId = B.FNHSysRawMatId INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase AS H WITH(NOLOCK) "
            _Qry &= vbCrLf & "  ON A.FTPurchaseNo = H.FTPurchaseNo "
            _Qry &= vbCrLf & " ) AS MM "
            _Qry &= vbCrLf & "  INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS IM WITH(NOLOCK) ON MM.FNHSysRawMatId = IM.FNHSysRawMatId "
            _Qry &= vbCrLf & "  LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatColor AS IMC WITH(NOLOCK) ON IM.FNHSysRawMatColorId = IMC.FNHSysRawMatColorId "
            _Qry &= vbCrLf & "   LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatSize AS IMS WITH(NOLOCK) ON IM.FNHSysRawMatSizeId  = IMS.FNHSysRawMatSizeId"
            _Qry &= vbCrLf & "  LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit AS IMU WITH(NOLOCK) ON MM.FNHSysUnitId  = IMU.FNHSysUnitId"
            _Qry &= vbCrLf & " ORDER BY  MM.FTPurchaseNo"
            _Qry &= vbCrLf & "  ,MM.FTOrderNo"
            _Qry &= vbCrLf & "   ,IM.FTRawMatCode "
            _Qry &= vbCrLf & "  ,ISNULL(IMC.FTRawMatColorCode,'' )"
            _Qry &= vbCrLf & "   ,ISNULL(IMS.FTRawMatSizeCode,'' )"

            Dim dt As DataTable = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PUR)
            ogcreservedetail.DataSource = dt.Copy()
            dt.Dispose()
        Catch ex As Exception

        End Try
       
    End Sub

    Private Function SaveReservePurchase(PurchaseKey As String, FromOrderKey As String, RawmatIDKey As Integer, ToOrderKey As String, POQty As Double, BalQty As Double, BfQty As Double, Qty As Double) As Boolean
        Dim _Qry As String = ""
        Dim _FNQuantity As Double = 0

        Dim _FTRawMatColorNameTH As String = ""
        Dim _FTRawMatColorNameEN As String = ""
        Dim _dtRawMatColor As New DataTable

        _Qry = "  SELECT TOP 1 A.FTOrderNo, A.FTRawMatColorNameTH, A.FTRawMatColorNameEN"
        _Qry &= vbCrLf & "   FROM     [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder_Mat_Color AS A WITH(NOLOCK) INNER JOIN"
        _Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS RM WITH(NOLOCK)  ON A.FNHSysRawMatColorId = RM.FNHSysRawMatColorId INNER JOIN"
        _Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMainMat AS MM WITH(NOLOCK)  ON A.FNHSysMainMatId = MM.FNHSysMainMatId AND RM.FTRawMatCode = MM.FTMainMatCode"
        _Qry &= vbCrLf & " WHERE A.FTOrderNo ='" & HI.UL.ULF.rpQuoted(ToOrderKey) & "' "
        _Qry &= vbCrLf & " AND RM.FNHSysRawMatId =" & Val(RawmatIDKey) & " "

        Try
            _dtRawMatColor = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

            For Each RxI As DataRow In _dtRawMatColor.Rows

                _FTRawMatColorNameTH = RxI!FTRawMatColorNameTH.ToString
                _FTRawMatColorNameEN = RxI!FTRawMatColorNameEN.ToString

                Exit For

            Next

        Catch ex As Exception
        End Try

        _dtRawMatColor.Dispose()

        _Qry = " SELECT SUM(FNQuantity) AS FNQuantity"
        _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo"
        _Qry &= vbCrLf & "  WHERE  (FTPurchaseNo ='" & HI.UL.ULF.rpQuoted(PurchaseKey) & "') "
        _Qry &= vbCrLf & "  AND (FTOrderNo = '" & HI.UL.ULF.rpQuoted(ToOrderKey) & "')"
        _Qry &= vbCrLf & "  AND (FNHSysRawMatId =" & RawmatIDKey & ")"

        _FNQuantity = Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PUR, "0"))

        HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PUR)
        HI.Conn.SQLConn.SqlConnectionOpen()
        HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
        HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

        Try

            Select Case True
                Case (BalQty > 0 And BfQty = 0)

                    _Qry = "Insert Into [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTReservePurchase"
                    _Qry &= vbCrLf & "(FTInsUser, FDInsDate, FTInsTime, FTPurchaseNo, FTOrderNo, FNHSysRawMatId, FNQuantity, FTOrderNoTo, FNQuantityTo)"
                    _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & ""
                    _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & ""
                    _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(PurchaseKey) & "'"
                    _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(FromOrderKey) & "'"
                    _Qry &= vbCrLf & " ," & RawmatIDKey & ""
                    _Qry &= vbCrLf & " ," & POQty & ""
                    _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(ToOrderKey) & "'"
                    _Qry &= vbCrLf & " ," & Qty & ""

                    If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        Return False
                    End If

                    If _FNQuantity > 0 Then

                        _Qry = " UPDATE A SET FNQuantity = (FNQuantity - ISNULL(FNReservePOQuantity,0)) + " & Qty & ""
                        _Qry &= vbCrLf & " ,FNReservePOQuantity=" & Qty & ""
                        _Qry &= vbCrLf & " ,FNNetAmt= Convert(numeric(18,2),((FNQuantity - ISNULL(FNReservePOQuantity,0)) + " & Qty & ") * (FNPrice-FNDisAmt)) "
                        _Qry &= vbCrLf & " ,FNGrandNetAmt= Convert(numeric(18,2),((FNQuantity - ISNULL(FNReservePOQuantity,0)) + " & Qty & ") * ((FNPrice-FNDisAmt)+ISNULL(FNSurchangePerUnit,0))) "
                        _Qry &= vbCrLf & ", FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        _Qry &= vbCrLf & ", FDUpdDate=" & HI.UL.ULDate.FormatDateDB & ""
                        _Qry &= vbCrLf & ", FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & ""
                        _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo AS A"
                        _Qry &= vbCrLf & "  WHERE  (FTPurchaseNo ='" & HI.UL.ULF.rpQuoted(PurchaseKey) & "') "
                        _Qry &= vbCrLf & "  AND (FTOrderNo = '" & HI.UL.ULF.rpQuoted(ToOrderKey) & "')"
                        _Qry &= vbCrLf & "  AND (FNHSysRawMatId =" & RawmatIDKey & ")"

                        If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                            HI.Conn.SQLConn.Tran.Rollback()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                            Return False
                        End If

                    Else

                        _Qry = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo "
                        _Qry &= vbCrLf & " (FTInsUser, FDInsDate, FTInsTime, FTPurchaseNo, FTOrderNo, FNHSysRawMatId, FNHSysUnitId, FNPrice, FNDisPer"
                        _Qry &= vbCrLf & " , FNDisAmt, FNQuantity, FNNetAmt, FTRemark, FTFabricFrontSize, FNReservePOQuantity,FTRawMatColorNameTH,FTRawMatColorNameEN"
                        _Qry &= vbCrLf & " , FNSurchangeAmt, FNSurchangePerUnit, FNGrandNetAmt, FTOGacDate) "
                        _Qry &= vbCrLf & " SELECT TOP 1 '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & ""
                        _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & ""
                        _Qry &= vbCrLf & " , FTPurchaseNo"
                        _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(ToOrderKey) & "'"
                        _Qry &= vbCrLf & " , FNHSysRawMatId, FNHSysUnitId, FNPrice, FNDisPer, FNDisAmt"
                        _Qry &= vbCrLf & " ," & Qty & " AS  FNQuantity"
                        _Qry &= vbCrLf & " , Convert(numeric(18,2),(" & Qty & ") * (FNPrice-FNDisAmt))"
                        _Qry &= vbCrLf & " , FTRemark"
                        _Qry &= vbCrLf & " , FTFabricFrontSize"
                        _Qry &= vbCrLf & " , " & Qty & ""
                        _Qry &= vbCrLf & " ,FTRawMatColorNameTH"
                        _Qry &= vbCrLf & " ,FTRawMatColorNameEN"
                        _Qry &= vbCrLf & " , FNSurchangeAmt"
                        _Qry &= vbCrLf & " , FNSurchangePerUnit"
                        _Qry &= vbCrLf & " , Convert(numeric(18,2),(" & Qty & ") * ((FNPrice-FNDisAmt)+ISNULL(FNSurchangePerUnit,0)))"
                        _Qry &= vbCrLf & ", FTOGacDate"
                        '_Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(_FTRawMatColorNameTH) & "'"
                        '_Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(_FTRawMatColorNameEN) & "'"

                        _Qry &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo"
                        _Qry &= vbCrLf & "  WHERE  (FTPurchaseNo ='" & HI.UL.ULF.rpQuoted(PurchaseKey) & "') "
                        _Qry &= vbCrLf & "  AND (FTOrderNo = '" & HI.UL.ULF.rpQuoted(FromOrderKey) & "')"
                        _Qry &= vbCrLf & "  AND (FNHSysRawMatId =" & RawmatIDKey & ")"

                        If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                            HI.Conn.SQLConn.Tran.Rollback()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                            Return False

                        End If

                    End If

                    _Qry = ""
                    Select Case True
                        Case (Qty = BalQty)

                            _Qry = " DELETE A "
                            _Qry &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo AS A"
                            _Qry &= vbCrLf & "  WHERE  (FTPurchaseNo ='" & HI.UL.ULF.rpQuoted(PurchaseKey) & "') "
                            _Qry &= vbCrLf & "  AND (FTOrderNo = '" & HI.UL.ULF.rpQuoted(FromOrderKey) & "')"
                            _Qry &= vbCrLf & "  AND (FNHSysRawMatId =" & RawmatIDKey & ")"

                            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                                HI.Conn.SQLConn.Tran.Rollback()
                                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                                Return False
                            End If

                        Case (Qty < BalQty)

                            _Qry = " UPDATE A SET FNQuantity =" & ((BalQty + BfQty) - Qty) & ""
                            _Qry &= vbCrLf & ",FNNetAmt= Convert(numeric(18,2),(" & ((BalQty + BfQty) - Qty) & ") * (FNPrice-FNDisAmt)) "
                            _Qry &= vbCrLf & " ,FNGrandNetAmt= Convert(numeric(18,2),(" & ((BalQty + BfQty) - Qty) & ") * ((FNPrice-FNDisAmt)+ISNULL(FNSurchangePerUnit,0))) "
                            _Qry &= vbCrLf & ", FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                            _Qry &= vbCrLf & ", FDUpdDate=" & HI.UL.ULDate.FormatDateDB & ""
                            _Qry &= vbCrLf & ", FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & ""
                            _Qry &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo AS A"
                            _Qry &= vbCrLf & "  WHERE  (FTPurchaseNo ='" & HI.UL.ULF.rpQuoted(PurchaseKey) & "') "
                            _Qry &= vbCrLf & "  AND (FTOrderNo = '" & HI.UL.ULF.rpQuoted(FromOrderKey) & "')"
                            _Qry &= vbCrLf & "  AND (FNHSysRawMatId =" & RawmatIDKey & ")"

                            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                                HI.Conn.SQLConn.Tran.Rollback()
                                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                                Return False
                            End If

                    End Select



                Case (BalQty > 0 And BfQty > 0)

                    _Qry = " UPDATE A SET FNQuantityTo=" & Qty & " "
                    _Qry &= vbCrLf & " ,FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Qry &= vbCrLf & ", FDUpdDate=" & HI.UL.ULDate.FormatDateDB & ""
                    _Qry &= vbCrLf & ", FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & ""
                    _Qry &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTReservePurchase AS A "
                    _Qry &= vbCrLf & "  WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(PurchaseKey) & "'"
                    _Qry &= vbCrLf & "  AND FTOrderNo='" & HI.UL.ULF.rpQuoted(FromOrderKey) & "'"
                    _Qry &= vbCrLf & "  AND FNHSysRawMatId=" & RawmatIDKey & ""
                    _Qry &= vbCrLf & "  AND FTOrderNoTo='" & HI.UL.ULF.rpQuoted(ToOrderKey) & "'"

                    If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        Return False
                    End If

                    _Qry = " UPDATE A SET FNQuantity = (FNQuantity - ISNULL(FNReservePOQuantity,0)) + " & Qty & ""
                    _Qry &= vbCrLf & " ,FNReservePOQuantity=" & Qty & ""
                    _Qry &= vbCrLf & " ,FNNetAmt= Convert(numeric(18,2),((FNQuantity - ISNULL(FNReservePOQuantity,0)) + " & Qty & ") * (FNPrice-FNDisAmt)) "
                    _Qry &= vbCrLf & " ,FNGrandNetAmt= Convert(numeric(18,2),((FNQuantity - ISNULL(FNReservePOQuantity,0)) + " & Qty & ") * ((FNPrice-FNDisAmt)+ISNULL(FNSurchangePerUnit,0))) "
                    _Qry &= vbCrLf & ", FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Qry &= vbCrLf & ", FDUpdDate=" & HI.UL.ULDate.FormatDateDB & ""
                    _Qry &= vbCrLf & ", FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & ""
                    _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo AS A"
                    _Qry &= vbCrLf & "  WHERE  (FTPurchaseNo ='" & HI.UL.ULF.rpQuoted(PurchaseKey) & "') "
                    _Qry &= vbCrLf & "  AND (FTOrderNo = '" & HI.UL.ULF.rpQuoted(ToOrderKey) & "')"
                    _Qry &= vbCrLf & "  AND (FNHSysRawMatId =" & RawmatIDKey & ")"

                    If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        Return False
                    End If

                    Select Case True
                        Case (Qty = (BfQty + BalQty))
                            _Qry = " DELETE A "
                            _Qry &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo AS A"
                            _Qry &= vbCrLf & "  WHERE  (FTPurchaseNo ='" & HI.UL.ULF.rpQuoted(PurchaseKey) & "') "
                            _Qry &= vbCrLf & "  AND (FTOrderNo = '" & HI.UL.ULF.rpQuoted(FromOrderKey) & "')"
                            _Qry &= vbCrLf & "  AND (FNHSysRawMatId =" & RawmatIDKey & ")"

                            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                                HI.Conn.SQLConn.Tran.Rollback()
                                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                                Return False
                            End If

                        Case (Qty < (BfQty + BalQty))

                            _Qry = " UPDATE A SET FNQuantity =" & ((BalQty + BfQty) - Qty) & ""
                            _Qry &= vbCrLf & ",FNNetAmt= Convert(numeric(18,2),(" & ((BalQty + BfQty) - Qty) & ") * (FNPrice-FNDisAmt)) "
                            _Qry &= vbCrLf & ",FNGrandNetAmt= Convert(numeric(18,2),(" & ((BalQty + BfQty) - Qty) & ") * ((FNPrice-FNDisAmt)+ISNULL(FNSurchangePerUnit,0))) "
                            _Qry &= vbCrLf & ", FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                            _Qry &= vbCrLf & ", FDUpdDate=" & HI.UL.ULDate.FormatDateDB & ""
                            _Qry &= vbCrLf & ", FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & ""
                            _Qry &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo AS A"
                            _Qry &= vbCrLf & "  WHERE  (FTPurchaseNo ='" & HI.UL.ULF.rpQuoted(PurchaseKey) & "') "
                            _Qry &= vbCrLf & "  AND (FTOrderNo = '" & HI.UL.ULF.rpQuoted(FromOrderKey) & "')"
                            _Qry &= vbCrLf & "  AND (FNHSysRawMatId =" & RawmatIDKey & ")"

                            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                                HI.Conn.SQLConn.Tran.Rollback()
                                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                                Return False
                            End If

                    End Select

                Case (BalQty = 0 And BfQty > 0 And (Qty) < BfQty)

                    _Qry = " UPDATE A SET FNQuantityTo=" & Qty & " "
                    _Qry &= vbCrLf & " ,FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Qry &= vbCrLf & ", FDUpdDate=" & HI.UL.ULDate.FormatDateDB & ""
                    _Qry &= vbCrLf & ", FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & ""
                    _Qry &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTReservePurchase AS A "
                    _Qry &= vbCrLf & "  WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(PurchaseKey) & "'"
                    _Qry &= vbCrLf & "  AND FTOrderNo='" & HI.UL.ULF.rpQuoted(FromOrderKey) & "'"
                    _Qry &= vbCrLf & "  AND FNHSysRawMatId=" & RawmatIDKey & ""
                    _Qry &= vbCrLf & "  AND FTOrderNoTo='" & HI.UL.ULF.rpQuoted(ToOrderKey) & "'"

                    If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        Return False
                    End If

                    _Qry = " UPDATE A SET FNQuantity = (FNQuantity - ISNULL(FNReservePOQuantity,0)) + " & Qty & ""
                    _Qry &= vbCrLf & " ,FNReservePOQuantity=" & Qty & ""
                    _Qry &= vbCrLf & " ,FNNetAmt= Convert(numeric(18,2),((FNQuantity - ISNULL(FNReservePOQuantity,0)) + " & Qty & ") * (FNPrice-FNDisAmt)) "
                    _Qry &= vbCrLf & " ,FNGrandNetAmt= Convert(numeric(18,2),((FNQuantity - ISNULL(FNReservePOQuantity,0)) + " & Qty & ") * ((FNPrice-FNDisAmt)+ISNULL(FNSurchangePerUnit,0))) "
                    _Qry &= vbCrLf & ", FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Qry &= vbCrLf & ", FDUpdDate=" & HI.UL.ULDate.FormatDateDB & ""
                    _Qry &= vbCrLf & ", FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & ""
                    _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo AS A"
                    _Qry &= vbCrLf & "  WHERE  (FTPurchaseNo ='" & HI.UL.ULF.rpQuoted(PurchaseKey) & "') "
                    _Qry &= vbCrLf & "  AND (FTOrderNo = '" & HI.UL.ULF.rpQuoted(ToOrderKey) & "')"
                    _Qry &= vbCrLf & "  AND (FNHSysRawMatId =" & RawmatIDKey & ")"

                    If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        Return False
                    End If

                    _FTRawMatColorNameTH = ""
                    _FTRawMatColorNameEN = ""

                    _Qry = "  SELECT TOP 1 A.FTOrderNo, A.FTRawMatColorNameTH, A.FTRawMatColorNameEN"
                    _Qry &= vbCrLf & "   FROM     [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder_Mat_Color AS A WITH(NOLOCK) INNER JOIN"
                    _Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS RM WITH(NOLOCK)  ON A.FNHSysRawMatColorId = RM.FNHSysRawMatColorId INNER JOIN"
                    _Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMainMat AS MM WITH(NOLOCK)  ON A.FNHSysMainMatId = MM.FNHSysMainMatId AND RM.FTRawMatCode = MM.FTMainMatCode"
                    _Qry &= vbCrLf & " WHERE A.FTOrderNo ='" & HI.UL.ULF.rpQuoted(FromOrderKey) & "' "
                    _Qry &= vbCrLf & " AND RM.FNHSysRawMatId =" & Val(RawmatIDKey) & " "

                    Try
                        _dtRawMatColor = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

                        For Each RxI As DataRow In _dtRawMatColor.Rows

                            _FTRawMatColorNameTH = RxI!FTRawMatColorNameTH.ToString
                            _FTRawMatColorNameEN = RxI!FTRawMatColorNameEN.ToString

                            Exit For

                        Next

                    Catch ex As Exception
                    End Try

                    _dtRawMatColor.Dispose()

                    _Qry = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo "
                    _Qry &= vbCrLf & " (FTInsUser, FDInsDate, FTInsTime, FTPurchaseNo, FTOrderNo, FNHSysRawMatId, FNHSysUnitId, FNPrice, FNDisPer"
                    _Qry &= vbCrLf & " , FNDisAmt, FNQuantity, FNNetAmt, FTRemark, FTFabricFrontSize,FTRawMatColorNameTH,FTRawMatColorNameEN "
                    _Qry &= vbCrLf & " , FNSurchangeAmt, FNSurchangePerUnit, FNGrandNetAmt, FTOGacDate) "
                    _Qry &= vbCrLf & " SELECT TOP 1 '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & ""
                    _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & ""
                    _Qry &= vbCrLf & " , FTPurchaseNo"
                    _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(FromOrderKey) & "'"
                    _Qry &= vbCrLf & " , FNHSysRawMatId, FNHSysUnitId, FNPrice, FNDisPer, FNDisAmt"
                    _Qry &= vbCrLf & " ," & BfQty - Qty & " AS  FNQuantity"
                    _Qry &= vbCrLf & " , Convert(numeric(18,2),(" & BfQty - Qty & ") * (FNPrice-FNDisAmt))"
                    _Qry &= vbCrLf & " , FTRemark"
                    _Qry &= vbCrLf & " , FTFabricFrontSize"
                    '_Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(_FTRawMatColorNameTH) & "'"
                    '_Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(_FTRawMatColorNameEN) & "'"
                    _Qry &= vbCrLf & " ,FTRawMatColorNameTH"
                    _Qry &= vbCrLf & " ,FTRawMatColorNameEN"
                    _Qry &= vbCrLf & " , FNSurchangeAmt"
                    _Qry &= vbCrLf & " , FNSurchangePerUnit"
                    _Qry &= vbCrLf & " , Convert(numeric(18,2),(" & (BfQty - Qty) & ") * ((FNPrice-FNDisAmt)+ISNULL(FNSurchangePerUnit,0)))"
                    _Qry &= vbCrLf & ", FTOGacDate"
                    _Qry &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo"
                    _Qry &= vbCrLf & "  WHERE  (FTPurchaseNo ='" & HI.UL.ULF.rpQuoted(PurchaseKey) & "') "
                    _Qry &= vbCrLf & "  AND (FTOrderNo = '" & HI.UL.ULF.rpQuoted(ToOrderKey) & "')"
                    _Qry &= vbCrLf & "  AND (FNHSysRawMatId =" & RawmatIDKey & ")"

                    If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        Return False

                    End If

            End Select

            If _Qry = "" Then

                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                Return False

            End If


            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return True

        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return False
        End Try

        Return True
    End Function

    Private Function DeleteReservePurchase(PurchaseKey As String, FromOrderKey As String, RawmatIDKey As Integer, ToOrderKey As String, POQty As Double, BalQty As Double, BfQty As Double) As Boolean
        Dim _Qry As String = ""
        Dim _FNPOQuantity As Double = 0
        Dim _FNQuantity As Double = 0



        Dim _FTRawMatColorNameTH As String = ""
        Dim _FTRawMatColorNameEN As String = ""
        Dim _dtRawMatColor As New DataTable

        _Qry = "  SELECT TOP 1 A.FTOrderNo, A.FTRawMatColorNameTH, A.FTRawMatColorNameEN"
        _Qry &= vbCrLf & "   FROM     [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder_Mat_Color AS A WITH(NOLOCK) INNER JOIN"
        _Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS RM WITH(NOLOCK)  ON A.FNHSysRawMatColorId = RM.FNHSysRawMatColorId INNER JOIN"
        _Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMainMat AS MM WITH(NOLOCK)  ON A.FNHSysMainMatId = MM.FNHSysMainMatId AND RM.FTRawMatCode = MM.FTMainMatCode"
        _Qry &= vbCrLf & " WHERE A.FTOrderNo ='" & HI.UL.ULF.rpQuoted(FromOrderKey) & "' "
        _Qry &= vbCrLf & " AND RM.FNHSysRawMatId =" & Val(RawmatIDKey) & " "

        Try
            _dtRawMatColor = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

            For Each RxI As DataRow In _dtRawMatColor.Rows

                _FTRawMatColorNameTH = RxI!FTRawMatColorNameTH.ToString
                _FTRawMatColorNameEN = RxI!FTRawMatColorNameEN.ToString

                Exit For

            Next

        Catch ex As Exception
        End Try

        _dtRawMatColor.Dispose()


        _Qry = " SELECT SUM(FNQuantity) AS FNQuantity"
        _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo WITH(NOLOCK) "
        _Qry &= vbCrLf & "  WHERE  (FTPurchaseNo ='" & HI.UL.ULF.rpQuoted(PurchaseKey) & "') "
        _Qry &= vbCrLf & "  AND (FTOrderNo = '" & HI.UL.ULF.rpQuoted(ToOrderKey) & "')"
        _Qry &= vbCrLf & "  AND (FNHSysRawMatId =" & RawmatIDKey & ")"

        _FNQuantity = Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PUR, "0"))

        _Qry = " SELECT SUM(FNQuantity) AS FNQuantity"
        _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo  WITH(NOLOCK) "
        _Qry &= vbCrLf & "  WHERE  (FTPurchaseNo ='" & HI.UL.ULF.rpQuoted(PurchaseKey) & "') "
        _Qry &= vbCrLf & "  AND (FTOrderNo = '" & HI.UL.ULF.rpQuoted(FromOrderKey) & "')"
        _Qry &= vbCrLf & "  AND (FNHSysRawMatId =" & RawmatIDKey & ")"

        _FNPOQuantity = Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PUR, "0"))

        HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PUR)
        HI.Conn.SQLConn.SqlConnectionOpen()
        HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
        HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

        Try

            _Qry = " DELETE A "
            _Qry &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTReservePurchase AS A "
            _Qry &= vbCrLf & "  WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(PurchaseKey) & "'"
            _Qry &= vbCrLf & "  AND FTOrderNo='" & HI.UL.ULF.rpQuoted(FromOrderKey) & "'"
            _Qry &= vbCrLf & "  AND FNHSysRawMatId=" & RawmatIDKey & ""
            _Qry &= vbCrLf & "  AND FTOrderNoTo='" & HI.UL.ULF.rpQuoted(ToOrderKey) & "'"

            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                Return False
            End If

            If BalQty <= 0 Then


                _Qry = " UPDATE A SET FNQuantity =" & ((BalQty + BfQty)) & ""
                _Qry &= vbCrLf & ",FNNetAmt= Convert(numeric(18,2),(" & ((BalQty + BfQty)) & ") * (FNPrice-FNDisAmt)) "
                _Qry &= vbCrLf & ",FNGrandNetAmt= Convert(numeric(18,2),(" & ((BalQty + BfQty)) & ") * ((FNPrice-FNDisAmt)+ISNULL(FNSurchangePerUnit,0))) "
                _Qry &= vbCrLf & ", FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Qry &= vbCrLf & ", FDUpdDate=" & HI.UL.ULDate.FormatDateDB & ""
                _Qry &= vbCrLf & ", FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & ""
                _Qry &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo AS A"
                _Qry &= vbCrLf & "  WHERE  (FTPurchaseNo ='" & HI.UL.ULF.rpQuoted(PurchaseKey) & "') "
                _Qry &= vbCrLf & "  AND (FTOrderNo = '" & HI.UL.ULF.rpQuoted(FromOrderKey) & "')"
                _Qry &= vbCrLf & "  AND (FNHSysRawMatId =" & RawmatIDKey & ")"

                If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                    _Qry = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo "
                    _Qry &= vbCrLf & " (FTInsUser, FDInsDate, FTInsTime, FTPurchaseNo, FTOrderNo, FNHSysRawMatId, FNHSysUnitId, FNPrice, FNDisPer"
                    _Qry &= vbCrLf & " , FNDisAmt, FNQuantity, FNNetAmt, FTRemark, FTFabricFrontSize,FTRawMatColorNameTH,FTRawMatColorNameEN "
                    _Qry &= vbCrLf & " , FNSurchangeAmt, FNSurchangePerUnit, FNGrandNetAmt, FTOGacDate) "
                    _Qry &= vbCrLf & " SELECT TOP 1 '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & ""
                    _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & ""
                    _Qry &= vbCrLf & " , FTPurchaseNo"
                    _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(FromOrderKey) & "'"
                    _Qry &= vbCrLf & " , FNHSysRawMatId, FNHSysUnitId, FNPrice, FNDisPer, FNDisAmt"
                    _Qry &= vbCrLf & " ," & BfQty & " AS  FNQuantity"
                    _Qry &= vbCrLf & " , Convert(numeric(18,2),((" & BfQty & ") * (FNPrice-FNDisAmt)))"
                    _Qry &= vbCrLf & " , FTRemark"
                    _Qry &= vbCrLf & " , FTFabricFrontSize"
                    '_Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(_FTRawMatColorNameTH) & "'"
                    '_Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(_FTRawMatColorNameEN) & "'"
                    _Qry &= vbCrLf & " ,FTRawMatColorNameTH"
                    _Qry &= vbCrLf & " ,FTRawMatColorNameEN"
                    _Qry &= vbCrLf & " , FNSurchangeAmt"
                    _Qry &= vbCrLf & " , FNSurchangePerUnit"
                    _Qry &= vbCrLf & " , Convert(numeric(18,2),(" & (BfQty) & ") * ((FNPrice-FNDisAmt)+ISNULL(FNSurchangePerUnit,0)))"
                    _Qry &= vbCrLf & ", FTOGacDate"
                    _Qry &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo"
                    _Qry &= vbCrLf & "  WHERE  (FTPurchaseNo ='" & HI.UL.ULF.rpQuoted(PurchaseKey) & "') "
                    _Qry &= vbCrLf & "  AND (FTOrderNo = '" & HI.UL.ULF.rpQuoted(ToOrderKey) & "')"
                    _Qry &= vbCrLf & "  AND (FNHSysRawMatId =" & RawmatIDKey & ")"

                    If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        Return False

                    End If

                End If


            Else

                _Qry = " UPDATE A SET FNQuantity =" & ((BalQty + BfQty)) & ""
                _Qry &= vbCrLf & ",FNNetAmt= Convert(numeric(18,2),(" & ((BalQty + BfQty)) & ") * (FNPrice-FNDisAmt)) "
                _Qry &= vbCrLf & ",FNGrandNetAmt= Convert(numeric(18,2),(" & ((BalQty + BfQty)) & ") * ((FNPrice-FNDisAmt)+ISNULL(FNSurchangePerUnit,0))) "
                _Qry &= vbCrLf & ", FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Qry &= vbCrLf & ", FDUpdDate=" & HI.UL.ULDate.FormatDateDB & ""
                _Qry &= vbCrLf & ", FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & ""
                _Qry &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo AS A"
                _Qry &= vbCrLf & "  WHERE  (FTPurchaseNo ='" & HI.UL.ULF.rpQuoted(PurchaseKey) & "') "
                _Qry &= vbCrLf & "  AND (FTOrderNo = '" & HI.UL.ULF.rpQuoted(FromOrderKey) & "')"
                _Qry &= vbCrLf & "  AND (FNHSysRawMatId =" & RawmatIDKey & ")"

                If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    Return False
                End If

            End If

            _Qry = " DELETE A "
            _Qry &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo AS A"
            _Qry &= vbCrLf & "  WHERE  (FTPurchaseNo ='" & HI.UL.ULF.rpQuoted(PurchaseKey) & "') "
            _Qry &= vbCrLf & "  AND (FTOrderNo = '" & HI.UL.ULF.rpQuoted(ToOrderKey) & "')"
            _Qry &= vbCrLf & "  AND (FNHSysRawMatId =" & RawmatIDKey & ")"

            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                Return False
            End If

         
            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return True

        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return False
        End Try

        Return True
    End Function

    Private Function CheckRcv(PurchaseKey As String, OrderKey As String, RawmatIDKey As Integer) As Boolean

        Dim _Qry As String = ""

        _Qry = " SELECT TOP 1  A.FTPurchaseNo "
        _Qry &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive AS A WITH(NOLOCK) INNER JOIN"
        _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail_Order AS B WITH(NOLOCK)  ON A.FTReceiveNo = B.FTReceiveNo"
        _Qry &= vbCrLf & " WHERE A.FTPurchaseNo='" & HI.UL.ULF.rpQuoted(PurchaseKey) & "'"
        _Qry &= vbCrLf & " AND   B.FTOrderNo='" & HI.UL.ULF.rpQuoted(OrderKey) & "'"
        _Qry &= vbCrLf & " AND   B.FNHSysRawMatId=" & RawmatIDKey & ""

        If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_INVEN, "") <> "" Then
            HI.MG.ShowMsg.mInfo("พบข้อมูลการรับแล้ว ไม่สามารถ ทำการลบ หรือแก้ไขได้ !!!", 1412010101, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
            Return True
        Else
            Return False
        End If

    End Function

#End Region

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ocmload_Click(sender As Object, e As EventArgs) Handles ocmload.Click
        If Me.Verifydata() Then
            Me.Loaddata()
        End If
    End Sub

    Private Sub ocmclear_Click(sender As Object, e As EventArgs) Handles ocmclear.Click
        HI.TL.HandlerControl.ClearControl(Me)
    End Sub

    Private Sub ogvdetail_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles ogvdetail.FocusedRowChanged
        Try
            With ogvdetail
                If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then
                    Me.ogcreservedetail.DataSource = Nothing
                    Exit Sub
                End If

                Dim _FTPurchaseNo As String = "" & .GetFocusedRowCellValue("FTPurchaseNo").ToString
                Dim _FTOrderNo As String = "" & .GetFocusedRowCellValue("FTOrderNo").ToString
                Dim _FNHSysRawMatId As String = "" & .GetFocusedRowCellValue("FNHSysRawMatId").ToString

                Call LoadReserveDetail(_FTPurchaseNo, _FTOrderNo, Integer.Parse(Val(_FNHSysRawMatId)))

            End With

        Catch ex As Exception
        End Try
    End Sub

    Private Sub ogvdetail_RowCellStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles ogvdetail.RowCellStyle
        Try
            With ogvdetail
                Dim _FNQuantityBal As Double = 0
                Dim _FNReserveQuantity As Double = 0
                If IsNumeric("" & .GetRowCellValue(e.RowHandle, "FNQuantityBal").ToString) Then
                    _FNQuantityBal = CDbl("" & .GetRowCellValue(e.RowHandle, "FNQuantityBal").ToString)
                End If

                If IsNumeric("" & .GetRowCellValue(e.RowHandle, "FNQuantityRsv").ToString) Then
                    _FNReserveQuantity = CDbl("" & .GetRowCellValue(e.RowHandle, "FNQuantityRsv").ToString)
                End If

                Try
                 
                    If _FNReserveQuantity > 0 Then
                        e.Appearance.ForeColor = Drawing.Color.Blue
                    End If

                Catch ex As Exception
                End Try

            End With
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ocmsave_Click(sender As Object, e As EventArgs) Handles ocmsave.Click
        Try
            If Me.FTOrderNo.Text.Trim = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, FTOrderNo_lbl.Text)
                FTOrderNo.Focus()
                Exit Sub
            End If

            If FNRsvQuantity.Value <= 0 Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, FNQuantity_lbl.Text)
                FNRsvQuantity.Focus()
                Exit Sub
            End If

            Dim _FNPOQuantity As Double = 0
            Dim _FNQuantity As Double = 0
            Dim _FNReserveQuantityBF As Double = 0
            Dim _FTPurchaseNo As String = ""
            Dim _FTOrderNo As String = ""
            Dim _FNHSysRawMatId As String = ""
            Dim _FTStateNotCheckResuorce As String = ""
            Dim _FNOrderType As Integer = 0

            With ogvdetail
                _FTPurchaseNo = "" & .GetFocusedRowCellValue("FTPurchaseNo").ToString
                _FTOrderNo = "" & .GetFocusedRowCellValue("FTOrderNo").ToString
                _FNHSysRawMatId = "" & .GetFocusedRowCellValue("FNHSysRawMatId").ToString
                _FTStateNotCheckResuorce = "" & .GetFocusedRowCellValue("FTStateNotCheckResuorce").ToString

                If IsNumeric("" & .GetRowCellValue(.FocusedRowHandle, "FNQuantity").ToString) Then
                    _FNPOQuantity = CDbl("" & .GetRowCellValue(.FocusedRowHandle, "FNQuantity").ToString)
                End If

                If IsNumeric("" & .GetRowCellValue(.FocusedRowHandle, "FNQuantityBal").ToString) Then
                    _FNQuantity = CDbl("" & .GetRowCellValue(.FocusedRowHandle, "FNQuantityBal").ToString)
                End If

            End With

            If FTOrderNo.Text.Trim() = _FTOrderNo Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, FTOrderNo_lbl.Text)
                FTOrderNo.Focus()
                Exit Sub
            End If

            If _FTPurchaseNo <> "" And _FTOrderNo <> "" And Integer.Parse(Val(_FNHSysRawMatId)) > 0 Then

                If CheckRcv(_FTPurchaseNo, _FTOrderNo, Integer.Parse(Val(_FNHSysRawMatId))) Then
                    Exit Sub
                End If

                Dim _Qry As String = ""
                If _FTStateNotCheckResuorce <> "1" Then

                    _Qry = " Select TOP 1 FNOrderType"
                    _Qry &= vbCrLf & "    FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS XX WITH(NOLOCK) "
                    _Qry &= vbCrLf & " WHERE (FTOrderNo = N'" & HI.UL.ULF.rpQuoted(FTOrderNo.Text) & "')"
                    _FNOrderType = Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MERCHAN, "99")))

                    Select Case _FNOrderType
                        Case 0, 13, 22

                            If Microsoft.VisualBasic.Right(Microsoft.VisualBasic.Left(FTOrderNo.Text.Trim, 5), 1) = "-" Then

                                _Qry = " Select TOP 1 FTOrderNo"
                                _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTMPR AS X WITH(NOLOCK)"
                                _Qry &= vbCrLf & " WHERE  (FTOrderNo  = N'" & HI.UL.ULF.rpQuoted(FTOrderNo.Text) & "')"
                                _Qry &= vbCrLf & "  AND (FNHSysRawMatId = " & Integer.Parse(Val(_FNHSysRawMatId)) & ")"

                            Else

                                _Qry = " Select TOP 1 FTOrderNo"
                                _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder_Resource AS X WITH(NOLOCK)"
                                _Qry &= vbCrLf & " WHERE  (FTOrderNo = N'" & HI.UL.ULF.rpQuoted(FTOrderNo.Text) & "')"
                                _Qry &= vbCrLf & "  AND (FNHSysRawMatId = " & Integer.Parse(Val(_FNHSysRawMatId)) & ")"

                            End If

                            If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MERCHAN, "") = "" Then
                                HI.MG.ShowMsg.mInfo("ไม่สามารถโอนยอดสั่งซื้อได้ เนื่องจาก ไม่พบรายการการใช้วัตถุดิบนี้ ในใบ สั่งผลิตปลางทาง !!!", 1418910188, Me.Text, FTOrderNo.Text, System.Windows.Forms.MessageBoxIcon.Warning)
                                Exit Sub
                            End If

                        Case Else

                    End Select

                End If

                _Qry = "  SELECT SUM(FNQuantityTo) AS FNQuantityTo"
                _Qry &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTReservePurchase"
                _Qry &= vbCrLf & "  WHERE  (FTPurchaseNo = N'" & HI.UL.ULF.rpQuoted(_FTPurchaseNo) & "') "
                _Qry &= vbCrLf & "  AND (FTOrderNo = N'" & HI.UL.ULF.rpQuoted(_FTOrderNo) & "') "
                _Qry &= vbCrLf & "  AND (FNHSysRawMatId =" & Integer.Parse(Val(_FNHSysRawMatId)) & ") "
                _Qry &= vbCrLf & " AND (FTOrderNoTo = N'" & HI.UL.ULF.rpQuoted(FTOrderNo.Text) & "')"

                _FNReserveQuantityBF = Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PUR, "0"))

                If (_FNQuantity + _FNReserveQuantityBF) >= FNRsvQuantity.Value Then

                    If SaveReservePurchase(_FTPurchaseNo, _FTOrderNo, Integer.Parse(Val(_FNHSysRawMatId)), FTOrderNo.Text, _FNPOQuantity, _FNQuantity, _FNReserveQuantityBF, FNRsvQuantity.Value) Then

                        _Qry = " EXEC  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.USP_CHECKSPPO_CMPO '" & HI.UL.ULF.rpQuoted(_FTPurchaseNo) & "'  "
                        HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_PUR)


                        _Qry = " EXEC  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.USP_CECKSENDREVISEDPO '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(_FTPurchaseNo) & "'  "
                        HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_PUR)

                        HI.MG.ShowMsg.mInfo("ระบบทำการ โอนยอดสั่งซื้อเรียบร้อยแล้ว !!!", 1412010100, Me.Text, , System.Windows.Forms.MessageBoxIcon.Information)
                        FTOrderNo.Text = ""
                        FTOrderNo.Focus()
                        FNRsvQuantity.Value = 0

                        Dim _FocusRowInDex As Integer = -1
                        Dim _FTKeyRef As String = ""

                        Try

                            If ogvdetail.FocusedRowHandle > 0 Then

                                _FocusRowInDex = ogvdetail.FocusedRowHandle
                                _FTKeyRef = ogvdetail.GetRowCellValue(ogvdetail.FocusedRowHandle, "FTKeyRef").ToString

                            End If

                        Catch ex As Exception
                        End Try

                        Call Me.Loaddata()

                        _FocusRowInDex = -1
                        With Me.ogvdetail

                            If .RowCount > 0 And _FTKeyRef <> "" Then

                                Try
                                    _FocusRowInDex = .LocateByValue("FTKeyRef", _FTKeyRef)
                                Catch ex As Exception
                                End Try

                                Try

                                    If _FocusRowInDex <> -1 Then
                                        .FocusedRowHandle = _FocusRowInDex
                                        '.SelectRow(_FocusRowInDex)
                                    End If

                                    _FTPurchaseNo = "" & .GetFocusedRowCellValue("FTPurchaseNo").ToString
                                    _FTOrderNo = "" & .GetFocusedRowCellValue("FTOrderNo").ToString
                                    _FNHSysRawMatId = "" & .GetFocusedRowCellValue("FNHSysRawMatId").ToString

                                    Call LoadReserveDetail(_FTPurchaseNo, _FTOrderNo, Integer.Parse(Val(_FNHSysRawMatId)))

                                Catch ex As Exception
                                End Try

                            End If

                        End With

                    Else

                        HI.MG.ShowMsg.mInfo("ไม่สามารถทำการโอนข้อมูลได้ กรุณาทำการติดต่อ ผู้ดูแลระบบ เพื่อทำการตรวจสอบ !!!", 1412010099, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
                        FTOrderNo.Focus()
                        FTOrderNo.SelectAll()

                    End If

                Else
                    HI.MG.ShowMsg.mInfo("จำนวนไม่พอ สำหรับยอด ที่ต้องการทำการโอน !!!", 1411280100, Me.Text, "Balance = " & Format((_FNQuantity + _FNReserveQuantityBF), HI.ST.Config.QtyFormat), System.Windows.Forms.MessageBoxIcon.Warning)
                End If

            Else
                HI.MG.ShowMsg.mInfo("กรุณาทำการเลือก ข้อมูลต้นทาง ที่ต้องการทำการโอน !!!", 1411280099, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
            End If

        Catch ex As Exception
        End Try

    End Sub

    Private Sub ocmdelete_Click(sender As Object, e As EventArgs) Handles ocmdelete.Click
        Try
            If Me.FTOrderNo.Text = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, FTOrderNo_lbl.Text)
                FTOrderNo.Focus()
                Exit Sub
            End If

            Dim _FNPOQuantity As Double = 0
            Dim _FNQuantity As Double = 0
            Dim _FNReserveQuantityBF As Double = 0
            Dim _FTPurchaseNo As String = ""
            Dim _FTOrderNo As String = ""
            Dim _FNHSysRawMatId As String = ""
            Dim _Qry As String = ""

            With ogvdetail

                _FTPurchaseNo = "" & .GetFocusedRowCellValue("FTPurchaseNo").ToString
                _FTOrderNo = "" & .GetFocusedRowCellValue("FTOrderNo").ToString
                _FNHSysRawMatId = "" & .GetFocusedRowCellValue("FNHSysRawMatId").ToString

                If IsNumeric("" & .GetRowCellValue(.FocusedRowHandle, "FNQuantity").ToString) Then
                    _FNPOQuantity = CDbl("" & .GetRowCellValue(.FocusedRowHandle, "FNQuantity").ToString)
                End If

                If IsNumeric("" & .GetRowCellValue(.FocusedRowHandle, "FNQuantityBal").ToString) Then
                    _FNQuantity = CDbl("" & .GetRowCellValue(.FocusedRowHandle, "FNQuantityBal").ToString)
                End If

                '_Qry = " SELECT SUM(FNQuantity) AS FNQuantity"
                '_Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo WITH(NOLOCK) "
                '_Qry &= vbCrLf & "  WHERE  (FTPurchaseNo ='" & HI.UL.ULF.rpQuoted(_FTPurchaseNo) & "') "
                '_Qry &= vbCrLf & "  AND (FTOrderNo = '" & HI.UL.ULF.rpQuoted(_FTOrderNo) & "')"
                '_Qry &= vbCrLf & "  AND (FNHSysRawMatId =" & _FNHSysRawMatId & ")"

                '_FNQuantity = Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PUR, "0"))
            End With

            If _FTPurchaseNo <> "" And _FTOrderNo <> "" And Integer.Parse(Val(_FNHSysRawMatId)) > 0 Then

                If CheckRcv(_FTPurchaseNo, _FTOrderNo, Integer.Parse(Val(_FNHSysRawMatId))) Then
                    Exit Sub
                End If



                _Qry = "  SELECT SUM(FNQuantityTo) AS FNQuantityTo"
                _Qry &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTReservePurchase"
                _Qry &= vbCrLf & "  WHERE  (FTPurchaseNo = N'" & HI.UL.ULF.rpQuoted(_FTPurchaseNo) & "') "
                _Qry &= vbCrLf & "  AND (FTOrderNo = N'" & HI.UL.ULF.rpQuoted(_FTOrderNo) & "') "
                _Qry &= vbCrLf & "  AND (FNHSysRawMatId =" & Integer.Parse(Val(_FNHSysRawMatId)) & ") "
                _Qry &= vbCrLf & " AND (FTOrderNoTo = N'" & HI.UL.ULF.rpQuoted(FTOrderNo.Text) & "')"

                _FNReserveQuantityBF = Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PUR, "0"))

                If (_FNQuantity + _FNReserveQuantityBF) >= FNRsvQuantity.Value Then

                    If DeleteReservePurchase(_FTPurchaseNo, _FTOrderNo, Integer.Parse(Val(_FNHSysRawMatId)), FTOrderNo.Text, _FNPOQuantity, _FNQuantity, _FNReserveQuantityBF) Then

                        _Qry = " EXEC  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.USP_CHECKSPPO_CMPO '" & HI.UL.ULF.rpQuoted(_FTPurchaseNo) & "'  "
                        HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_PUR)


                        _Qry = " EXEC  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.USP_CECKSENDREVISEDPO '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(_FTPurchaseNo) & "'  "
                        HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_PUR)

                        HI.MG.ShowMsg.mInfo("ระบบทำการ ลบการโอนยอดสั่งซื้อเรียบร้อยแล้ว !!!", 1412010113, Me.Text, , System.Windows.Forms.MessageBoxIcon.Information)
                        FTOrderNo.Text = ""
                        FTOrderNo.Focus()
                        FNRsvQuantity.Value = 0

                        Dim _FocusRowInDex As Integer = -1
                        Dim _FTKeyRef As String = ""

                        Try

                            If ogvdetail.FocusedRowHandle > 0 Then
                                _FocusRowInDex = ogvdetail.FocusedRowHandle
                                _FTKeyRef = ogvdetail.GetRowCellValue(ogvdetail.FocusedRowHandle, "FTKeyRef").ToString
                            End If

                        Catch ex As Exception
                        End Try

                        Call Me.Loaddata()

                        _FocusRowInDex = -1
                        With Me.ogvdetail
                            If .RowCount > 0 And _FTKeyRef <> "" Then

                                Try
                                    _FocusRowInDex = .LocateByValue("FTKeyRef", _FTKeyRef)
                                Catch ex As Exception
                                End Try

                                Try

                                    If _FocusRowInDex <> -1 Then
                                        .FocusedRowHandle = _FocusRowInDex
                                        '.SelectRow(_FocusRowInDex)
                                    End If

                                    _FTPurchaseNo = "" & .GetFocusedRowCellValue("FTPurchaseNo").ToString
                                    _FTOrderNo = "" & .GetFocusedRowCellValue("FTOrderNo").ToString
                                    _FNHSysRawMatId = "" & .GetFocusedRowCellValue("FNHSysRawMatId").ToString

                                    Call LoadReserveDetail(_FTPurchaseNo, _FTOrderNo, Integer.Parse(Val(_FNHSysRawMatId)))

                                Catch ex As Exception
                                End Try

                            End If
                        End With

                        Try
                            ogvdetail_FocusedRowChanged(ogvdetail, New DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(-1, 0))
                        Catch ex As Exception
                        End Try

                        Me.FTOrderNo.Text = ""
                        Me.FNRsvQuantity.Value = 0

                    Else
                        HI.MG.ShowMsg.mInfo("ไม่สามารถทำลบการโอนข้อมูลได้ กรุณาทำการติดต่อ ผู้ดูแลระบบ เพื่อทำการตรวจสอบ !!!", 1412010199, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
                        FTOrderNo.Focus()
                        FTOrderNo.SelectAll()
                    End If

                Else
                    HI.MG.ShowMsg.mInfo("จำนวนไม่พอ สำหรับยอด ที่ต้องการทำการโอน !!!", 1411280107, Me.Text, "Balance = " & Format((_FNQuantity + _FNReserveQuantityBF), HI.ST.Config.QtyFormat), System.Windows.Forms.MessageBoxIcon.Warning)
                End If

            Else
                HI.MG.ShowMsg.mInfo("กรุณาทำการเลือก ข้อมูลต้นทาง ที่ต้องการทำการโอน !!!", 1411280108, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
            End If

        Catch ex As Exception
        End Try

    End Sub

    Private Sub ogvreservedetail_Click(sender As Object, e As EventArgs) Handles ogvreservedetail.Click
        Try
            With ogvreservedetail
                If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then
                    Exit Sub
                End If

                Me.FTOrderNo.Text = "" & .GetFocusedRowCellValue("FTOrderNo").ToString
                Me.FNRsvQuantity.Value = Val("" & .GetFocusedRowCellValue("FNQuantity").ToString)
                FNRsvQuantity.Focus()
                FNRsvQuantity.SelectAll()

            End With

        Catch ex As Exception
        End Try
    End Sub

    Private Sub FTOrderNo_EditValueChanged(sender As Object, e As EventArgs) Handles FTOrderNo.EditValueChanged

    End Sub
End Class
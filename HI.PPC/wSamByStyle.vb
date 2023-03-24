Imports System.Windows.Forms
Imports DevExpress.Data.Helpers
Imports DevExpress.XtraEditors.Controls

Public Class wSamByStyle

    Private _LoadData As Boolean = False

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        'FNSamCut_lbl.Visible = False
        'FNSamCut.Visible = False
        CFNCutPrice.Visible = False
        FNCutPrice_lbl.Visible = False
        ' Add any initialization after the InitializeComponent() call.

    End Sub

#Region "Procedure"
    Public Sub LoadDataInfo(ByVal Key As Object)
        _LoadData = True

        Dim _Qry As String = ""

        _Qry = " SELECT A.FNHSysStyleId "
        _Qry &= vbCrLf & " ,SS.FTSeasonCode "
        _Qry &= vbCrLf & "	, A.FTOrderNo"
        _Qry &= vbCrLf & ", B.FTSubOrderNo,ISNULL(C.FNCostPerMin,0.00) AS FNStyleCostPerMin"
        _Qry &= vbCrLf & ", ISNULL(C.FNSam,0.00) AS FNSamByStyle"
        _Qry &= vbCrLf & ", ISNULL(C.FNPrice,0.00) AS FNSamPrice"
        _Qry &= vbCrLf & ", ISNULL(C.FNMultiple,1.00) AS FNSamByStyleFNMultiple"

        _Qry &= vbCrLf & ", ISNULL(C.FNSam,0)  AS FNSamST"
        _Qry &= vbCrLf & ", ISNULL(C.FNCostPerMin, 0)  AS FNCostPerMinST"

        _Qry &= vbCrLf & ", ISNULL(D.FNSam, 0)  AS FNSam"
        _Qry &= vbCrLf & ", ISNULL(D.FNCostPerMin, 0)  AS FNCostPerMin"
        '' _Qry &= vbCrLf & ", ISNULL(D.FNMultiple, ISNULL(C.FNMultiple,1.00))  AS FNMultiple"
        _Qry &= vbCrLf & ", ISNULL(D.FNMultiple,ISNULL(EC.FNMultiple, ISNULL(C.FNMultiple,1.00)))  AS FNMultiple "


        _Qry &= vbCrLf & ",CASE WHEN ISNULL(B.FTPORef ,'') = '' THEN  A.FTPORef ELSE ISNULL(B.FTPORef ,'') END  AS FTCustomerPO"
        _Qry &= vbCrLf & ",CASE WHEN ISNULL(B.FTPORef ,'') = '' THEN  A.FTPORef ELSE ISNULL(B.FTPORef ,'') END  AS FNHSysBuy"
        _Qry &= vbCrLf & ",CASE WHEN  ISDATE(B.FDShipDate) = 1 THEN Convert(Datetime,B.FDShipDate) ELSE NULL END AS FDShipDate"
        _Qry &= vbCrLf & ",ISNULL(D.FNPrice, 0.0) AS FNPrice"
        _Qry &= vbCrLf & ",ISNULL(D.FNRepackPrice, 0.0) AS FNRepackPrice"


        _Qry &= vbCrLf & ",ISNULL(D.FNSamWring, 0.0) AS FNSamWring"
        _Qry &= vbCrLf & ",ISNULL(D.FNCostWring, 0.0) AS FNCostWring"
        _Qry &= vbCrLf & ",ISNULL(D.FNWringPrice, 0.0) AS FNWringPrice"
        _Qry &= vbCrLf & ",ISNULL(C.FNWringPrice, 0.0) AS FNWringPriceST"

        _Qry &= vbCrLf & ",ISNULL(D.FNSamCut, 0.0) AS FNSamCut"
        _Qry &= vbCrLf & ",ISNULL(D.FNSamEmb, 0.0) AS FNSamEmb"

        _Qry &= vbCrLf & ",ISNULL(C.FNSamCut, 0.0) AS FNSamCutST"
        _Qry &= vbCrLf & ",ISNULL(C.FNSamEmb, 0.0) AS FNSamEmbST"

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Qry &= vbCrLf & ",E.FTBuyGrpNameTH AS FNHSysBuyGrp "
        Else
            _Qry &= vbCrLf & ",E.FTBuyGrpNameEN AS FNHSysBuyGrp "
        End If

        _Qry &= vbCrLf & ",ISNULL(D.FNCutPrice, 0.0) AS FNCutPrice"
        _Qry &= vbCrLf & ",ISNULL(D.FNCostCut, 0.0) AS FNCostCut"
        _Qry &= vbCrLf & ",ISNULL(D.FNNetCostCut, 0.0) AS FNNetCostCut"

        _Qry &= vbCrLf & ",ISNULL(D.FNSamSew, 0.0) AS FNSamSew"
        _Qry &= vbCrLf & ",ISNULL(D.FNCostSew, 0.0) AS FNCostSew"
        _Qry &= vbCrLf & ",ISNULL(D.FNNetCostSew, 0.0) AS FNNetCostSew"


        _Qry &= vbCrLf & ",ISNULL(D.FNSamPack, 0.0) AS FNSamPack"
        _Qry &= vbCrLf & ",ISNULL(D.FNCostPack, 0.0) AS FNCostPack"
        _Qry &= vbCrLf & ",ISNULL(D.FNNetCostPack, 0.0) AS FNNetCostPack"

        _Qry &= vbCrLf & ",ISNULL(D.FTStateSmallLot , '0') AS FTStateSmallLot "

        _Qry &= vbCrLf & ",ISNULL(D.FNCostPerMinSmallLot, 0.0) AS FNCostPerMinSmallLot"
        _Qry &= vbCrLf & ",ISNULL(D.FNNetCostSmallLot, 0.0) AS FNNetCostSmallLot"

        _Qry &= vbCrLf & ",ISNULL(D.FNSamSemiPart, 0.0) AS FNSamSemiPart "
        _Qry &= vbCrLf & ",ISNULL(D.FNCostSemiPart, 0.0) AS  FNCostSemiPart "
        _Qry &= vbCrLf & ",ISNULL(D.FNNetCostSemiPart, 0.0) AS FNNetCostSemiPart  "



        _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS A WITH(NOLOCK) "
        _Qry &= vbCrLf & " INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub AS B WITH(NOLOCK)  ON A.FTOrderNo = B.FTOrderNo "
        _Qry &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TPPCTSamByOrder AS D WITH(NOLOCK)  ON B.FTSubOrderNo = D.FTSubOrderNo AND B.FTOrderNo = D.FTOrderNo AND A.FNHSysStyleId = D.FNHSysStyleId "
        _Qry &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TPPCTSamByStyle AS C WITH(NOLOCK)  ON A.FNHSysStyleId = C.FNHSysStyleId"
        _Qry &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMSeason AS SS WITH(NOLOCK) ON A.FNHSysSeasonId = SS.FNHSysSeasonId "
        _Qry &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMBuyGrp AS E WITH(NOLOCK) ON B.FNHSysBuyGrpId = E.FNHSysBuyGrpId "
        _Qry &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMBuyGrpCmp AS EC WITH(NOLOCK) ON B.FNHSysBuyGrpId = EC.FNHSysBuyGrpId AND A.FNHSysCmpId = EC.FNHSysCmpId "
        _Qry &= vbCrLf & "  WHERE A.FNHSysStyleId='" & Val(Key) & "'"
        _Qry &= vbCrLf & "        AND A.FNHSysCmpId=" & Val(HI.ST.SysInfo.CmpID) & ""

        _Qry &= vbCrLf & "  ORDER BY SS.FTSeasonCode, A.FTOrderNo,B.FTSubOrderNo "

        Dim _dt As DataTable = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

        Me.FNSam.Value = 0
        Me.FNCostPerMin.Value = 0
        Me.FNMultiple.Value = 1
        Me.FNCostSamPrice.Value = 0

        If _dt.Rows.Count > 0 Then
            Me.FNSam.Value = Val(_dt.Rows(0)!FNSamByStyle.ToString)
            Me.FNCostPerMin.Value = Val(_dt.Rows(0)!FNStyleCostPerMin.ToString)
            Me.FNMultiple.Value = Val(_dt.Rows(0)!FNSamByStyleFNMultiple.ToString)
            Me.FNCostSamPrice.Value = Val(_dt.Rows(0)!FNSamPrice.ToString)


            Me.FNSamCut.Value = Val(_dt.Rows(0)!FNSamCutST.ToString)
            Me.FNSamEmb.Value = Val(_dt.Rows(0)!FNSamEmbST.ToString)

            Me.FNCutPrice.Value = Val(_dt.Rows(0)!FNCutPrice.ToString)
            Me.FNCostCut.Value = Val(_dt.Rows(0)!FNCostCut.ToString)
            Me.FNNetCostCut.Value = Val(_dt.Rows(0)!FNNetCostCut.ToString)

            Me.FNSamSew.Value = Val(_dt.Rows(0)!FNSamSew.ToString)
            Me.FNCostSew.Value = Val(_dt.Rows(0)!FNCostSew.ToString)
            Me.FNNetCostSew.Value = Val(_dt.Rows(0)!FNNetCostSew.ToString)


            Me.FNSamWring.Value = Val(_dt.Rows(0)!FNSamWring.ToString)
            Me.FNCostWring.Value = Val(_dt.Rows(0)!FNCostWring.ToString)

            Me.FNWringPrice.Value = Val(_dt.Rows(0)!FNWringPriceST.ToString)


            Me.FNSamSemiPart.Value = Val(_dt.Rows(0)!FNSamSemiPart.ToString)
            Me.FNCostSemiPart.Value = Val(_dt.Rows(0)!FNCostSemiPart.ToString)
            Me.FNNetCostSemiPart.Value = Val(_dt.Rows(0)!FNNetCostSemiPart.ToString)

            Me.FNSamPack.Value = Val(_dt.Rows(0)!FNSamPack.ToString)
            Me.FNCostPack.Value = Val(_dt.Rows(0)!FNCostPack.ToString)
            Me.FNNetCostPack.Value = Val(_dt.Rows(0)!FNNetCostPack.ToString)
            Me.FNCostPerMinSmallLot.Value = Val(_dt.Rows(0)!FNCostPerMinSmallLot.ToString)
            Me.FNNetCostSmallLot.Value = Val(_dt.Rows(0)!FNNetCostSmallLot.ToString)



        End If

        Me.ogcdetail.DataSource = _dt


        ''------------------Semi--------------------------------------
        _Qry = "  SELECT A.FNHSysPartId ,P.FTPartCode  "
        _Qry &= vbCrLf & "  "
        _Qry &= vbCrLf & "  "
        _Qry &= vbCrLf & "  "
        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Qry &= vbCrLf & "   ,ISNULL(P.FTPartNameTH,'') AS FTPartName  "
            _Qry &= vbCrLf & "   ,ISNULL(LN.FTNameTH,'') AS FNSendSuplTypeName "
        Else
            _Qry &= vbCrLf & "   ,ISNULL(P.FTPartNameEN,'') AS FTPartName  "
            _Qry &= vbCrLf & "   ,ISNULL(LN.FTNameTH,'') AS FNSendSuplTypeName "
        End If
        _Qry &= vbCrLf & "  , A.FNSendSuplType "
        _Qry &= vbCrLf & "  ,ISNULL(SPLN.FTNote,'') AS FTNote "

        _Qry &= vbCrLf & "  ,ISNULL(SSP.FNSam,0) AS [FNSam] "
        _Qry &= vbCrLf & " 	,ISNULL(SSP.FNCost,0) [FNCost] "
        _Qry &= vbCrLf & " ,ISNULL(SSP.FNMultiple,1) [FNMultiple] "
        _Qry &= vbCrLf & "  ,ISNULL(SSP.FNNetCost,0) [FNNetCost] "
        _Qry &= vbCrLf & "  ," & Val(Key) & " as FNHSysStyleId "

        _Qry &= vbCrLf & "  FROM"
        _Qry &= vbCrLf & "  (SELECT    FNHSysPartId,0 AS FNSendSuplType "
        _Qry &= vbCrLf & "  FROM            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTStyle_Part WITH(NOLOCK)"
        _Qry &= vbCrLf & "   WHERE FNHSysStyleId = " & Val(Key) & " "

        'If StateSeason Then
        '    _Qry &= vbCrLf & "   And (FNHSysSeasonId =" & _FNHSysSeasonId & " OR ISNULL(FNHSysSeasonId,0)<=0) "
        'End If

        _Qry &= vbCrLf & "   And FTStateEmb = 1"

        _Qry &= vbCrLf & "   UNION"
        _Qry &= vbCrLf & "  SELECT    FNHSysPartId,1 AS FNSendSuplType "
        _Qry &= vbCrLf & "  FROM            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTStyle_Part WITH(NOLOCK)"
        _Qry &= vbCrLf & "    WHERE FNHSysStyleId =  " & Val(Key) & " "

        'If StateSeason Then
        '    _Qry &= vbCrLf & "   And (FNHSysSeasonId =" & _FNHSysSeasonId & " OR ISNULL(FNHSysSeasonId,0)<=0) "
        'End If

        _Qry &= vbCrLf & "   And FTStatePrint = 1"
        _Qry &= vbCrLf & "    UNION"
        _Qry &= vbCrLf & "  SELECT    FNHSysPartId,2 AS FNSendSuplType "
        _Qry &= vbCrLf & "  FROM            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTStyle_Part WITH(NOLOCK)"
        _Qry &= vbCrLf & "   WHERE FNHSysStyleId =  " & Val(Key) & " "

        'If StateSeason Then
        '    _Qry &= vbCrLf & "   And (FNHSysSeasonId =" & _FNHSysSeasonId & " OR ISNULL(FNHSysSeasonId,0)<=0) "
        'End If

        _Qry &= vbCrLf & "   And FTStateHeat = 1"
        _Qry &= vbCrLf & "    UNION"
        _Qry &= vbCrLf & "  SELECT    FNHSysPartId,3 AS FNSendSuplType "
        _Qry &= vbCrLf & "  FROM            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTStyle_Part WITH(NOLOCK)"
        _Qry &= vbCrLf & "   WHERE FNHSysStyleId =  " & Val(Key) & " "

        'If StateSeason Then
        '    _Qry &= vbCrLf & "   And (FNHSysSeasonId =" & _FNHSysSeasonId & " OR ISNULL(FNHSysSeasonId,0)<=0) "
        'End If

        _Qry &= vbCrLf & "   And FTStateLaser = 1"
        _Qry &= vbCrLf & "   UNION"
        _Qry &= vbCrLf & "  SELECT    FNHSysPartId,4 AS FNSendSuplType "
        _Qry &= vbCrLf & "  FROM            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTStyle_Part WITH(NOLOCK)"
        _Qry &= vbCrLf & "   WHERE FNHSysStyleId =  " & Val(Key) & " "

        'If StateSeason Then
        '    _Qry &= vbCrLf & "   And (FNHSysSeasonId =" & _FNHSysSeasonId & " OR ISNULL(FNHSysSeasonId,0)<=0) "
        'End If

        _Qry &= vbCrLf & "   And FTStateWindows = 1"
        _Qry &= vbCrLf & "   UNION"
        _Qry &= vbCrLf & "  SELECT    FNHSysPartId,6 AS FNSendSuplType "
        _Qry &= vbCrLf & "  FROM            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTStyle_Part WITH(NOLOCK)"
        _Qry &= vbCrLf & "   WHERE FNHSysStyleId =  " & Val(Key) & " "

        'If StateSeason Then
        '    _Qry &= vbCrLf & "   And (FNHSysSeasonId =" & _FNHSysSeasonId & " OR ISNULL(FNHSysSeasonId,0)<=0) "
        'End If



        _Qry &= vbCrLf & "   And FTStateNonEmbroidry = 1"
        _Qry &= vbCrLf & "  ) AS A LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMPart AS P WITH(NOLOCK)"
        _Qry &= vbCrLf & "    ON A.FNHSysPartId = P.FNHSysPartId  "
        _Qry &= vbCrLf & "  LEFT OUTER JOIN ( SELECT * FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData WITH(NOLOCK)  WHERE FTListName ='FNSendSuplType') AS LN "
        _Qry &= vbCrLf & "  ON A.FNSendSuplType = LN.FNListIndex "

        _Qry &= vbCrLf & "  LEFT OUTER JOIN ( SELECT * "



        _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.FNT_GetPartSendSuplDesc_SemiPart(" & Val(Key) & ")  "

        _Qry &= vbCrLf & "  ) AS SPLN "
        _Qry &= vbCrLf & "  ON A.FNHSysPartId = SPLN.FNHSysPartId AND A.FNSendSuplType = SPLN.FNSendSuplType "

        _Qry &= vbCrLf & " LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TPPCTSamByStyle_SemiPart SSP ON SSP.FNHSysStyleId= " & Val(Key) & " "
        _Qry &= vbCrLf & " AND SSP.FNHSysPartId= A.FNHSysPartId "
        _Qry &= vbCrLf & "  AND SSP.FNSendSuplType= A.FNSendSuplType "
        _Qry &= vbCrLf & " AND SSP.FTNote=ISNULL(SPLN.FTNote,'')  "

        _Qry &= vbCrLf & " WHERE A.FNHSysPartId > 0 AND ISNULL(P.FTPartCode,'') <>'' "

        Dim _dtStyle As DataTable = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)


        Me.ogcStyle.DataSource = _dtStyle




        ''------------Order level--------------------------------

        _Qry = " SELECT  O.FNHSysStyleId  "
        _Qry &= vbCrLf & " ,SS.FTSeasonCode "
        _Qry &= vbCrLf & "	, O.FTOrderNo "
        _Qry &= vbCrLf & " , OB.FTSubOrderNo "
        _Qry &= vbCrLf & " ,CASE WHEN ISNULL(OB.FTPORef ,'') = '' THEN  O.FTPORef ELSE ISNULL(OB.FTPORef ,'') END  AS FTCustomerPO "
        _Qry &= vbCrLf & " ,CASE WHEN ISNULL(OB.FTPORef ,'') = '' THEN  O.FTPORef ELSE ISNULL(OB.FTPORef ,'') END  AS FNHSysBuy "
        _Qry &= vbCrLf & "  ,CASE WHEN  ISDATE(OB.FDShipDate) = 1 THEN Convert(Datetime,OB.FDShipDate) ELSE NULL END AS FDShipDate "

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Qry &= vbCrLf & ",E.FTBuyGrpNameTH AS FNHSysBuyGrp "
        Else
            _Qry &= vbCrLf & ",E.FTBuyGrpNameEN AS FNHSysBuyGrp "
        End If
        _Qry &= vbCrLf & "   ,SOP.FNHSysPartId ,SOP.FTPartCode "
        _Qry &= vbCrLf & " ,ISNULL(SOP.FTPartName,'') AS FTPartName  "
        _Qry &= vbCrLf & " ,ISNULL(SOP.FNSendSuplTypeName,'') AS FNSendSuplTypeName  "
        _Qry &= vbCrLf & " , SOP.FNSendSuplType "
        _Qry &= vbCrLf & " ,ISNULL(SOP.FTNote,'') AS FTNote "
        _Qry &= vbCrLf & " ,ISNULL(SOP.FNSam,0) AS [FNSam]  "
        _Qry &= vbCrLf & " ,ISNULL(SOP.FNCost,0) [FNCost]  "
        _Qry &= vbCrLf & " ,ISNULL(SOP.FNMultiple,1) [FNMultiple]  "
        _Qry &= vbCrLf & " ,ISNULL(SOP.FNNetCost,0) [FNNetCost]"


        _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH(NOLOCK)  "
        _Qry &= vbCrLf & "  INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub AS OB WITH(NOLOCK)  ON O.FTOrderNo = OB.FTOrderNo "
        _Qry &= vbCrLf & " LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMSeason AS SS WITH(NOLOCK) ON O.FNHSysSeasonId = SS.FNHSysSeasonId "
        _Qry &= vbCrLf & "  LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMBuyGrp AS E WITH(NOLOCK) ON OB.FNHSysBuyGrpId = E.FNHSysBuyGrpId "
        _Qry &= vbCrLf & " "



        _Qry &= vbCrLf & "  CROSS APPLY ( "



        '''---------------------------------------
        _Qry &= vbCrLf & "  SELECT A.FNHSysPartId ,P.FTPartCode  "
        _Qry &= vbCrLf & "  "
        _Qry &= vbCrLf & "  "
        _Qry &= vbCrLf & "  "
        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Qry &= vbCrLf & "   ,ISNULL(P.FTPartNameTH,'') AS FTPartName  "
            _Qry &= vbCrLf & "   ,ISNULL(LN.FTNameTH,'') AS FNSendSuplTypeName "
        Else
            _Qry &= vbCrLf & "   ,ISNULL(P.FTPartNameEN,'') AS FTPartName  "
            _Qry &= vbCrLf & "   ,ISNULL(LN.FTNameTH,'') AS FNSendSuplTypeName "
        End If
        _Qry &= vbCrLf & "  , A.FNSendSuplType "
        _Qry &= vbCrLf & "  ,ISNULL(SPLN.FTNote,'') AS FTNote "

        _Qry &= vbCrLf & "  ,ISNULL(SOSP.FNSam,SSP.FNSam) AS [FNSam]  "
        _Qry &= vbCrLf & " 	,ISNULL(SOSP.FNCost,SSP.FNCost) [FNCost] "
        _Qry &= vbCrLf & " ,ISNULL(SOSP.FNMultiple,SSP.FNMultiple) [FNMultiple] "
        _Qry &= vbCrLf & "  ,ISNULL(SOSP.FNNetCost,SSP.FNNetCost) [FNNetCost] "

        _Qry &= vbCrLf & "  ," & Val(Key) & " as FNHSysStyleId "

        _Qry &= vbCrLf & "  FROM"
        _Qry &= vbCrLf & "  (SELECT    FNHSysPartId,0 AS FNSendSuplType "
        _Qry &= vbCrLf & "  FROM            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTStyle_Part WITH(NOLOCK)"
        _Qry &= vbCrLf & "   WHERE FNHSysStyleId = " & Val(Key) & " "

        'If StateSeason Then
        '    _Qry &= vbCrLf & "   And (FNHSysSeasonId =" & _FNHSysSeasonId & " OR ISNULL(FNHSysSeasonId,0)<=0) "
        'End If

        _Qry &= vbCrLf & "   And FTStateEmb = 1"

        _Qry &= vbCrLf & "   UNION"
        _Qry &= vbCrLf & "  SELECT    FNHSysPartId,1 AS FNSendSuplType "
        _Qry &= vbCrLf & "  FROM            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTStyle_Part WITH(NOLOCK)"
        _Qry &= vbCrLf & "    WHERE FNHSysStyleId =  " & Val(Key) & " "

        'If StateSeason Then
        '    _Qry &= vbCrLf & "   And (FNHSysSeasonId =" & _FNHSysSeasonId & " OR ISNULL(FNHSysSeasonId,0)<=0) "
        'End If

        _Qry &= vbCrLf & "   And FTStatePrint = 1"
        _Qry &= vbCrLf & "    UNION"
        _Qry &= vbCrLf & "  SELECT    FNHSysPartId,2 AS FNSendSuplType "
        _Qry &= vbCrLf & "  FROM            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTStyle_Part WITH(NOLOCK)"
        _Qry &= vbCrLf & "   WHERE FNHSysStyleId =  " & Val(Key) & " "

        'If StateSeason Then
        '    _Qry &= vbCrLf & "   And (FNHSysSeasonId =" & _FNHSysSeasonId & " OR ISNULL(FNHSysSeasonId,0)<=0) "
        'End If

        _Qry &= vbCrLf & "   And FTStateHeat = 1"
        _Qry &= vbCrLf & "    UNION"
        _Qry &= vbCrLf & "  SELECT    FNHSysPartId,3 AS FNSendSuplType "
        _Qry &= vbCrLf & "  FROM            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTStyle_Part WITH(NOLOCK)"
        _Qry &= vbCrLf & "   WHERE FNHSysStyleId =  " & Val(Key) & " "

        'If StateSeason Then
        '    _Qry &= vbCrLf & "   And (FNHSysSeasonId =" & _FNHSysSeasonId & " OR ISNULL(FNHSysSeasonId,0)<=0) "
        'End If

        _Qry &= vbCrLf & "   And FTStateLaser = 1"
        _Qry &= vbCrLf & "   UNION"
        _Qry &= vbCrLf & "  SELECT    FNHSysPartId,4 AS FNSendSuplType "
        _Qry &= vbCrLf & "  FROM            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTStyle_Part WITH(NOLOCK)"
        _Qry &= vbCrLf & "   WHERE FNHSysStyleId =  " & Val(Key) & " "

        'If StateSeason Then
        '    _Qry &= vbCrLf & "   And (FNHSysSeasonId =" & _FNHSysSeasonId & " OR ISNULL(FNHSysSeasonId,0)<=0) "
        'End If

        _Qry &= vbCrLf & "   And FTStateWindows = 1"
        _Qry &= vbCrLf & "   UNION"
        _Qry &= vbCrLf & "  SELECT    FNHSysPartId,6 AS FNSendSuplType "
        _Qry &= vbCrLf & "  FROM            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTStyle_Part WITH(NOLOCK)"
        _Qry &= vbCrLf & "   WHERE FNHSysStyleId =  " & Val(Key) & " "

        'If StateSeason Then
        '    _Qry &= vbCrLf & "   And (FNHSysSeasonId =" & _FNHSysSeasonId & " OR ISNULL(FNHSysSeasonId,0)<=0) "
        'End If



        _Qry &= vbCrLf & "   And FTStateNonEmbroidry = 1"
        _Qry &= vbCrLf & "  ) AS A LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMPart AS P WITH(NOLOCK)"
        _Qry &= vbCrLf & "    ON A.FNHSysPartId = P.FNHSysPartId  "
        _Qry &= vbCrLf & "  LEFT OUTER JOIN ( SELECT * FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData WITH(NOLOCK)  WHERE FTListName ='FNSendSuplType') AS LN "
        _Qry &= vbCrLf & "  ON A.FNSendSuplType = LN.FNListIndex "

        _Qry &= vbCrLf & "  LEFT OUTER JOIN ( SELECT * "

        _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.FNT_GetPartSendSuplDesc_SemiPart(" & Val(Key) & ")  "

        _Qry &= vbCrLf & "  ) AS SPLN "
        _Qry &= vbCrLf & "  ON A.FNHSysPartId = SPLN.FNHSysPartId AND A.FNSendSuplType = SPLN.FNSendSuplType "

        _Qry &= vbCrLf & " LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TPPCTSamByStyle_SemiPart SSP ON SSP.FNHSysStyleId= " & Val(Key) & " "
        _Qry &= vbCrLf & " AND SSP.FNHSysPartId= A.FNHSysPartId "
        _Qry &= vbCrLf & "  AND SSP.FNSendSuplType= A.FNSendSuplType "
        _Qry &= vbCrLf & " AND SSP.FTNote=ISNULL(SPLN.FTNote,'')  "

        _Qry &= vbCrLf & " LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TPPCTSamByOrder_SemiPart SOSP ON SOSP.FNHSysStyleId= " & Val(Key) & " "
        _Qry &= vbCrLf & " AND SOSP.FTOrderNo= O.FTOrderNo "
        _Qry &= vbCrLf & " AND SOSP.FTSubOrderNo=OB.FTSubOrderNo "
        _Qry &= vbCrLf & " AND SOSP.FNHSysPartId= A.FNHSysPartId  "
        _Qry &= vbCrLf & " AND SOSP.FNSendSuplType= A.FNSendSuplType "
        _Qry &= vbCrLf & " AND SOSP.FTNote=ISNULL(SPLN.FTNote,'')   "


        _Qry &= vbCrLf & " WHERE A.FNHSysPartId > 0 AND ISNULL(P.FTPartCode,'') <>'' "
        '''-----------------------------------------

        _Qry &= vbCrLf & "  ) SOP "




        _Qry &= vbCrLf & "  WHERE O.FNHSysStyleId='" & Val(Key) & "'"
        _Qry &= vbCrLf & "        AND O.FNHSysCmpId=" & Val(HI.ST.SysInfo.CmpID) & ""

        _Qry &= vbCrLf & "  ORDER BY SS.FTSeasonCode, O.FTOrderNo,OB.FTSubOrderNo "

        Dim _dtSSO As DataTable = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)
        Me.ogcSemiDetail.DataSource = _dtSSO







        _LoadData = False
    End Sub

    Private Function SaveData(Key As Integer) As Boolean
        Dim _Spls As New HI.TL.SplashScreen("Saving....   , Please wait. ")
        Dim _dt As DataTable = Nothing
        Try
            With DirectCast(Me.ogcdetail.DataSource, DataTable)
                .AcceptChanges()
                _dt = .Copy
            End With
        Catch ex As Exception
        End Try

        Dim _dtStyle As DataTable = Nothing
        Try
            With DirectCast(Me.ogcStyle.DataSource, DataTable)
                .AcceptChanges()
                _dtStyle = .Copy
            End With
        Catch ex As Exception
        End Try
        Dim _dtSSO As DataTable = Nothing
        Try
            With DirectCast(Me.ogcSemiDetail.DataSource, DataTable)
                .AcceptChanges()
                _dtSSO = .Copy
            End With
        Catch ex As Exception
        End Try

        Try
            Dim _Qry As String = ""
            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PLANNING)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            _Qry = " UPDATE A SET "
            _Qry &= vbCrLf & "  FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            _Qry &= vbCrLf & " ,FDUpdDate=" & HI.UL.ULDate.FormatDateDB
            _Qry &= vbCrLf & " ,FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
            _Qry &= vbCrLf & " ,FNSam=" & FNSam.Value & ""
            _Qry &= vbCrLf & " ,FNCostPerMin=" & FNCostPerMin.Value & ""
            _Qry &= vbCrLf & " ,FNMultiple=" & FNMultiple.Value & ""
            '_Qry &= vbCrLf & " ,FNPrice=Convert(numeric(18,4)," & (FNSam.Value * FNCostPerMin.Value * FNMultiple.Value) & ") "
            _Qry &= vbCrLf & " ,FNPrice=" & FNCostSamPrice.Value & " "
            _Qry &= vbCrLf & " ,FNWringPrice=" & FNWringPrice.Value & " "
            _Qry &= vbCrLf & " ,FNSamCut=" & FNSamCut.Value & ""
            _Qry &= vbCrLf & " ,FNSamEmb=" & FNSamEmb.Value & ""
            _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TPPCTSamByStyle AS A"
            _Qry &= vbCrLf & " WHERE FNHSysStyleId=" & Key & ""

            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                _Qry = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TPPCTSamByStyle"
                _Qry &= vbCrLf & "(FTInsUser, FDInsDate, FTInsTime, FNHSysStyleId, FNSam,FNCostPerMin,FNPrice,FNMultiple,FNWringPrice,FNSamCut,FNSamEmb)"
                _Qry &= vbCrLf & "SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB
                _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB
                _Qry &= vbCrLf & " ," & Key & ""
                _Qry &= vbCrLf & " ," & FNSam.Value & ""
                _Qry &= vbCrLf & " ," & FNCostPerMin.Value & ""
                '_Qry &= vbCrLf & " ,Convert(numeric(18,4)," & (FNSam.Value * FNCostPerMin.Value * FNMultiple.Value) & ") "
                _Qry &= vbCrLf & " ," & FNCostSamPrice.Value & " "
                _Qry &= vbCrLf & " ," & FNMultiple.Value & ""
                _Qry &= vbCrLf & " ," & FNWringPrice.Value & ""
                _Qry &= vbCrLf & " ," & FNSamCut.Value & ""
                _Qry &= vbCrLf & " ," & FNSamEmb.Value & ""

                If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    _Spls.Close()
                    Return False
                End If

            End If

            '_Qry = "DELETE FROM A "
            '_Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TPPCTSamByOrder AS A"
            '_Qry &= vbCrLf & " WHERE FNHSysStyleId=" & Key & ""

            'HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

            If Not (_dt Is Nothing) Then

                For Each R As DataRow In _dt.Rows

                    _Qry = " UPDATE A SET "
                    _Qry &= vbCrLf & "  FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Qry &= vbCrLf & " ,FDUpdDate=" & HI.UL.ULDate.FormatDateDB
                    _Qry &= vbCrLf & " ,FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
                    _Qry &= vbCrLf & " ,FNSam=" & Val(R!FNSam.ToString) & ""
                    _Qry &= vbCrLf & " ,FNCostPerMin=" & Val(R!FNCostPerMin.ToString) & ""
                    _Qry &= vbCrLf & " ,FNMultiple=" & Val(R!FNMultiple.ToString) & ""
                    '_Qry &= vbCrLf & " ,FNPrice=Convert(numeric(18,4)," & (Val(R!FNSam.ToString) * Val(R!FNCostPerMin.ToString) * Val(R!FNMultiple.ToString)) & ") "
                    _Qry &= vbCrLf & " ,FNPrice=" & Val(R!FNPrice.ToString) & ""
                    _Qry &= vbCrLf & " ,FNRepackPrice=" & Val(R!FNRepackPrice.ToString) & ""


                    _Qry &= vbCrLf & " ,FNSamWring=" & Val(R!FNSamWring.ToString) & ""
                    _Qry &= vbCrLf & " ,FNCostWring=" & Val(R!FNCostWring.ToString) & ""
                    _Qry &= vbCrLf & " ,FNWringPrice=" & Val(R!FNWringPrice.ToString) & ""


                    _Qry &= vbCrLf & " ,FNSamCut=" & Val(R!FNSamCut.ToString) & ""
                    _Qry &= vbCrLf & " ,FNSamEmb=" & Val(R!FNSamEmb.ToString) & ""

                    _Qry &= vbCrLf & " ,FNCutPrice=" & Val(R!FNCutPrice.ToString) & ""
                    _Qry &= vbCrLf & " ,FNCostCut=" & Val(R!FNCostCut.ToString) & ""
                    _Qry &= vbCrLf & " ,FNNetCostCut=" & Val(R!FNNetCostCut.ToString) & ""
                    _Qry &= vbCrLf & " ,FNSamPack=" & Val(R!FNSamPack.ToString) & ""
                    _Qry &= vbCrLf & " ,FNCostPack=" & Val(R!FNCostPack.ToString) & ""
                    _Qry &= vbCrLf & " ,FNNetCostPack=" & Val(R!FNNetCostPack.ToString) & ""

                    _Qry &= vbCrLf & " ,FTStateSmallLot =" & Val(R!FTStateSmallLot.ToString) & ""
                    _Qry &= vbCrLf & " ,FNCostPerMinSmallLot =" & Val(R!FNCostPerMinSmallLot.ToString) & ""
                    _Qry &= vbCrLf & " ,FNNetCostSmallLot =" & Val(R!FNNetCostSmallLot.ToString) & ""

                    _Qry &= vbCrLf & " ,FNSamSemiPart =" & Val(R!FNSamSemiPart.ToString) & ""
                    _Qry &= vbCrLf & " ,FNCostSemiPart =" & Val(R!FNCostSemiPart.ToString) & ""
                    _Qry &= vbCrLf & " ,FNNetCostSemiPart =" & Val(R!FNNetCostSemiPart.ToString) & ""


                    _Qry &= vbCrLf & " ,FNSamSew =" & Val(R!FNSamSew.ToString) & ""
                    _Qry &= vbCrLf & " ,FNCostSew =" & Val(R!FNCostSew.ToString) & ""
                    _Qry &= vbCrLf & " ,FNNetCostSew =" & Val(R!FNNetCostSew.ToString) & ""


                    _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TPPCTSamByOrder AS A"
                    _Qry &= vbCrLf & " WHERE FNHSysStyleId=" & Key & ""
                    _Qry &= vbCrLf & " AND FTOrderNo='" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "'"
                    _Qry &= vbCrLf & " AND FTSubOrderNo='" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "'"

                    If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                        _Qry = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TPPCTSamByOrder"
                        _Qry &= vbCrLf & "(FTInsUser, FDInsDate, FTInsTime, FNHSysStyleId, FTOrderNo, FTSubOrderNo, FNSam,FNCostPerMin,FNPrice,FNMultiple,FNRepackPrice, FNSamWring, FNCostWring,FNWringPrice,FNSamCut,FNSamEmb, FNCutPrice,FNCostCut,FNNetCostCut,FNSamPack,FNCostPack,FNNetCostPack, FTStateSmallLot,FNCostPerMinSmallLot, FNNetCostSmallLot , FNSamSemiPart,FNCostSemiPart,FNNetCostSemiPart, FNSamSew, FNCostSew , FNNetCostSew  )"
                        _Qry &= vbCrLf & "SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB
                        _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB
                        _Qry &= vbCrLf & " ," & Key & ""
                        _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "'"
                        _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "'"
                        '_Qry &= vbCrLf & " ," & Val(R!FNSam.ToString) & ""
                        '_Qry &= vbCrLf & " ," & Val(R!FNCostPerMin.ToString) & ""
                        '_Qry &= vbCrLf & " ,Convert(numeric(18,4)," & (Val(R!FNSam.ToString) * Val(R!FNCostPerMin.ToString)) & ") "
                        _Qry &= vbCrLf & " ," & Val(R!FNSam.ToString) & ""
                        _Qry &= vbCrLf & " ," & Val(R!FNCostPerMin.ToString) & ""
                        '_Qry &= vbCrLf & " ,Convert(numeric(18,4)," & (Val(R!FNSam.ToString) * Val(R!FNCostPerMin.ToString) * Val(R!FNMultiple.ToString)) & ") "
                        _Qry &= vbCrLf & " ," & Val(R!FNPrice.ToString) & ""
                        _Qry &= vbCrLf & " ," & Val(R!FNMultiple.ToString) & ""
                        _Qry &= vbCrLf & " ," & Val(R!FNRepackPrice.ToString) & ""

                        _Qry &= vbCrLf & " ," & Val(R!FNSamWring.ToString) & ""
                        _Qry &= vbCrLf & " ," & Val(R!FNCostWring.ToString) & ""

                        _Qry &= vbCrLf & " ," & Val(R!FNWringPrice.ToString) & ""
                        _Qry &= vbCrLf & " ," & Val(R!FNSamCut.ToString) & ""
                        _Qry &= vbCrLf & " ," & Val(R!FNSamEmb.ToString) & ""

                        _Qry &= vbCrLf & " ," & Val(R!FNCutPrice.ToString) & ""
                        _Qry &= vbCrLf & " ," & Val(R!FNCostCut.ToString) & ""
                        _Qry &= vbCrLf & " ," & Val(R!FNNetCostCut.ToString) & ""

                        _Qry &= vbCrLf & " ," & Val(R!FNSamPack.ToString) & ""
                        _Qry &= vbCrLf & " ," & Val(R!FNCostPack.ToString) & ""
                        _Qry &= vbCrLf & " ," & Val(R!FNNetCostPack.ToString) & ""

                        _Qry &= vbCrLf & " ," & Val(R!FTStateSmallLot.ToString) & ""
                        _Qry &= vbCrLf & " ," & Val(R!FNCostPerMinSmallLot.ToString) & ""
                        _Qry &= vbCrLf & " ," & Val(R!FNNetCostSmallLot.ToString) & ""

                        _Qry &= vbCrLf & " ," & Val(R!FNSamSemiPart.ToString) & ""
                        _Qry &= vbCrLf & " ," & Val(R!FNCostSemiPart.ToString) & ""
                        _Qry &= vbCrLf & " ," & Val(R!FNNetCostSemiPart.ToString) & ""

                        _Qry &= vbCrLf & " ," & Val(R!FNSamSew.ToString) & ""
                        _Qry &= vbCrLf & " ," & Val(R!FNCostSew.ToString) & ""
                        _Qry &= vbCrLf & " ," & Val(R!FNNetCostSew.ToString) & ""



                        'If FNSmallLot.Checked Then
                        '    _Qry &= vbCrLf & " ,1  "
                        'Else
                        '    _Qry &= vbCrLf & " , 0 "
                        'End If

                        If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                            HI.Conn.SQLConn.Tran.Rollback()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                            _Spls.Close()
                            Return False
                        End If

                    End If

                Next


            End If


            ''---semi-



            If Not (_dtStyle Is Nothing) Then

                For Each R As DataRow In _dtStyle.Rows

                    _Qry = " UPDATE A SET "
                    _Qry &= vbCrLf & "  FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Qry &= vbCrLf & " ,FDUpdDate=" & HI.UL.ULDate.FormatDateDB
                    _Qry &= vbCrLf & " ,FTUpdTime=" & HI.UL.ULDate.FormatTimeDB

                    _Qry &= vbCrLf & " ,FNSam=" & Val(R!FNSam.ToString) & ""
                    _Qry &= vbCrLf & " ,FNCost=" & Val(R!FNCost.ToString) & ""
                    _Qry &= vbCrLf & " ,FNMultiple=" & Val(R!FNMultiple.ToString) & ""
                    _Qry &= vbCrLf & " ,FNNetCost =" & Val(R!FNNetCost.ToString) & ""

                    _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TPPCTSamByStyle_SemiPart AS A"
                    _Qry &= vbCrLf & " WHERE FNHSysStyleId=" & Key & ""
                    _Qry &= vbCrLf & " AND FNHSysPartId=" & Val(R!FNHSysPartId.ToString) & ""
                    _Qry &= vbCrLf & " AND FNSendSuplType=" & Val(R!FNSendSuplType.ToString) & ""
                    _Qry &= vbCrLf & " AND FTNote='" & HI.UL.ULF.rpQuoted(R!FTNote.ToString) & "'"


                    If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                        _Qry = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TPPCTSamByStyle_SemiPart "
                        _Qry &= vbCrLf & "(FTInsUser, FDInsDate, FTInsTime, FNHSysStyleId,FNHSysPartId,FNSendSuplType,FTNote, FNSam,FNCost,FNMultiple,FNNetCost )"
                        _Qry &= vbCrLf & "SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB
                        _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB

                        _Qry &= vbCrLf & " ," & Key & ""
                        _Qry &= vbCrLf & " ," & Val(R!FNHSysPartId.ToString) & ""
                        _Qry &= vbCrLf & " ," & Val(R!FNSendSuplType.ToString) & ""
                        _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!FTNote.ToString) & "'"

                        _Qry &= vbCrLf & " ," & Val(R!FNSam.ToString) & ""
                        _Qry &= vbCrLf & " ," & Val(R!FNCost.ToString) & ""
                        _Qry &= vbCrLf & " ," & Val(R!FNMultiple.ToString) & ""
                        _Qry &= vbCrLf & " ," & Val(R!FNNetCost.ToString) & ""

                        If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                            HI.Conn.SQLConn.Tran.Rollback()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                            _Spls.Close()
                            Return False
                        End If

                    End If

                Next
            End If

            If Not (_dtSSO Is Nothing) Then

                For Each Rt As DataRow In _dtSSO.Rows

                    _Qry = " UPDATE A SET "
                    _Qry &= vbCrLf & "  FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Qry &= vbCrLf & " ,FDUpdDate=" & HI.UL.ULDate.FormatDateDB
                    _Qry &= vbCrLf & " ,FTUpdTime=" & HI.UL.ULDate.FormatTimeDB

                    _Qry &= vbCrLf & " ,FNSam=" & Val(Rt!FNSam.ToString) & ""
                    _Qry &= vbCrLf & " ,FNCost=" & Val(Rt!FNCost.ToString) & ""
                    _Qry &= vbCrLf & " ,FNMultiple=" & Val(Rt!FNMultiple.ToString) & ""
                    _Qry &= vbCrLf & " ,FNNetCost =" & Val(Rt!FNNetCost.ToString) & ""

                    _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TPPCTSamByOrder_SemiPart AS A"
                    _Qry &= vbCrLf & " WHERE FNHSysStyleId=" & Key & ""
                    _Qry &= vbCrLf & " AND FTOrderNo='" & HI.UL.ULF.rpQuoted(Rt!FTOrderNo.ToString) & "'"
                    _Qry &= vbCrLf & " AND FTSubOrderNo='" & HI.UL.ULF.rpQuoted(Rt!FTSubOrderNo.ToString) & "'"
                    _Qry &= vbCrLf & " AND FNHSysPartId=" & Val(Rt!FNHSysPartId.ToString) & ""
                    _Qry &= vbCrLf & " AND FNSendSuplType=" & Val(Rt!FNSendSuplType.ToString) & ""
                    _Qry &= vbCrLf & " AND FTNote='" & HI.UL.ULF.rpQuoted(Rt!FTNote.ToString) & "'"


                    If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                        _Qry = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TPPCTSamByOrder_SemiPart "
                        _Qry &= vbCrLf & "(FTInsUser, FDInsDate, FTInsTime, FNHSysStyleId, FTOrderNo, FTSubOrderNo,FNHSysPartId,FNSendSuplType,FTNote, FNSam,FNCost,FNMultiple,FNNetCost )"
                        _Qry &= vbCrLf & "SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB
                        _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB

                        _Qry &= vbCrLf & " ," & Key & ""
                        _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(Rt!FTOrderNo.ToString) & "'"
                        _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(Rt!FTSubOrderNo.ToString) & "'"
                        _Qry &= vbCrLf & " ," & Val(Rt!FNHSysPartId.ToString) & ""
                        _Qry &= vbCrLf & " ," & Val(Rt!FNSendSuplType.ToString) & ""
                        _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(Rt!FTNote.ToString) & "'"

                        _Qry &= vbCrLf & " ," & Val(Rt!FNSam.ToString) & ""
                        _Qry &= vbCrLf & " ," & Val(Rt!FNCost.ToString) & ""
                        _Qry &= vbCrLf & " ," & Val(Rt!FNMultiple.ToString) & ""
                        _Qry &= vbCrLf & " ," & Val(Rt!FNNetCost.ToString) & ""

                        If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                            HI.Conn.SQLConn.Tran.Rollback()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                            _Spls.Close()
                            Return False
                        End If

                    End If

                Next
            End If



            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            _Spls.Close()
            Return False
        End Try

        _Spls.Close()
        Return True

    End Function

    Private Function DeleteData(Key As Integer) As Boolean
        Dim _Spls As New HI.TL.SplashScreen("Deleting....   , Please wait. ")

        Try
            Dim _Qry As String = ""
            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PLANNING)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            If otbdetail.SelectedTabPage.Equals(otpSAM) Then



                _Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TPPCTSamByStyle  "
                _Qry &= vbCrLf & " WHERE FNHSysStyleId = " & Key & ""

                HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)


                Dim _dt As DataTable = Nothing
                Try
                    With DirectCast(Me.ogcdetail.DataSource, DataTable)
                        .AcceptChanges()
                        _dt = .Copy
                    End With
                Catch ex As Exception
                End Try


                With ogcdetail
                    If Not (.DataSource Is Nothing) And ogvdetail.RowCount > 0 Then

                        With ogvdetail
                            For I As Integer = 0 To .RowCount - 1

                                _Qry = " DELETE FROM A"
                                _Qry &= vbCrLf & "FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TPPCTSamByOrder AS A  "
                                _Qry &= vbCrLf & " WHERE FNHSysStyleId = " & Key & ""
                                _Qry &= vbCrLf & " AND FTOrderNo='" & HI.UL.ULF.rpQuoted(.GetRowCellValue(I, .Columns.ColumnByFieldName("FTOrderNo"))) & "'"
                                _Qry &= vbCrLf & " AND FTSubOrderNo='" & HI.UL.ULF.rpQuoted(.GetRowCellValue(I, .Columns.ColumnByFieldName("FTSubOrderNo"))) & "'"

                                HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

                            Next
                        End With

                    End If

                End With


                'For Each R As DataRow In _dt.Rows

                '    _Qry = " DELETE FROM A"
                '    _Qry &= vbCrLf & "FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TPPCTSamByOrder AS A  "
                '    _Qry &= vbCrLf & " WHERE FNHSysStyleId = " & Key & ""
                '    _Qry &= vbCrLf & " AND FTOrderNo='" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "'"
                '    _Qry &= vbCrLf & " AND FTSubOrderNo='" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "'"

                '    HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

                'Next

            Else


                '' ------------Semi------------------------
                _Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TPPCTSamByStyle_SemiPart  "
                _Qry &= vbCrLf & " WHERE FNHSysStyleId = " & Key & ""

                HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)


                With ogcSemiDetail
                    If Not (.DataSource Is Nothing) And ogvSemiDetail.RowCount > 0 Then

                        With ogvSemiDetail
                            For I As Integer = 0 To .RowCount - 1

                                ''.SetRowCellValue(I, .Columns.ColumnByFieldName("FTSelect"), _State)

                                _Qry = " DELETE FROM A"
                                _Qry &= vbCrLf & "FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TPPCTSamByOrder_SemiPart AS A  "
                                _Qry &= vbCrLf & " WHERE FNHSysStyleId = " & Key & ""
                                _Qry &= vbCrLf & " AND FTOrderNo='" & HI.UL.ULF.rpQuoted(.GetRowCellValue(I, .Columns.ColumnByFieldName("FTOrderNo"))) & "'"
                                _Qry &= vbCrLf & " AND FTSubOrderNo='" & HI.UL.ULF.rpQuoted(.GetRowCellValue(I, .Columns.ColumnByFieldName("FTSubOrderNo"))) & "'"
                                _Qry &= vbCrLf & " AND FNHSysPartId=" & Val(.GetRowCellValue(I, .Columns.ColumnByFieldName("FNHSysPartId"))) & ""
                                _Qry &= vbCrLf & " AND FNSendSuplType=" & Val(.GetRowCellValue(I, .Columns.ColumnByFieldName("FNSendSuplType"))) & ""
                                _Qry &= vbCrLf & " AND FTNote='" & HI.UL.ULF.rpQuoted(.GetRowCellValue(I, .Columns.ColumnByFieldName("FTNote"))) & "'"
                                HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)


                            Next
                        End With

                    End If

                End With



                'Dim _dtSSO As DataTable = Nothing
                'Try
                '    With DirectCast(Me.ogcSemiDetail.DataSource, DataTable)
                '        .AcceptChanges()
                '        _dtSSO = .Copy
                '    End With
                'Catch ex As Exception
                'End Try

                'For Each R As DataRow In _dtSSO.Rows

                '    _Qry = " DELETE FROM A"
                '    _Qry &= vbCrLf & "FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TPPCTSamByOrder_SemiPart AS A  "
                '    _Qry &= vbCrLf & " WHERE FNHSysStyleId = " & Key & ""
                '    _Qry &= vbCrLf & " AND FTOrderNo='" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "'"
                '    _Qry &= vbCrLf & " AND FTSubOrderNo='" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "'"
                '    _Qry &= vbCrLf & " AND FNHSysPartId=" & Val(R!FNHSysPartId.ToString) & ""
                '    _Qry &= vbCrLf & " AND FNSendSuplType=" & Val(R!FNSendSuplType.ToString) & ""
                '    _Qry &= vbCrLf & " AND FTNote='" & HI.UL.ULF.rpQuoted(R!FTNote.ToString) & "'"
                '    HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

                'Next


            End If






            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            _Spls.Close()
            Return False
        End Try

        _Spls.Close()
        Return True

    End Function

#End Region

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub FNHSysStyleId_EditValueChanged(sender As Object, e As EventArgs) Handles FNHSysStyleId.EditValueChanged
        If Me.InvokeRequired Then

            Me.Invoke(New HI.Delegate.Dele.ButtonEdit_ValueChanged(AddressOf FNHSysStyleId_EditValueChanged), New Object() {sender, e})

        Else

            If FNHSysStyleId.Text <> "" Then

                Dim _Qry As String = "SELECT TOP 1 FNHSysStyleId  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS MS WITH (NOLOCK) WHERE FTStyleCode ='" & HI.UL.ULF.rpQuoted(FNHSysStyleId.Text) & "' "
                FNHSysStyleId.Properties.Tag = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MASTER, "")
                Me.ogcdetail.DataSource = Nothing

                Call LoadDataInfo(FNHSysStyleId.Properties.Tag.ToString)

            Else

                Me.ogcdetail.DataSource = Nothing
                Me.ogcStyle.DataSource = Nothing
                Me.ogcSemiDetail.DataSource = Nothing


            End If
        End If

    End Sub

    Private Sub ocmrefresh_Click(sender As Object, e As EventArgs) Handles ocmrefresh.Click
        Me.LoadDataInfo(Me.FNHSysStyleId.Properties.Tag.ToString)
    End Sub

    Private Sub ocmsave_Click(sender As Object, e As EventArgs) Handles ocmsave.Click

        If Not FNHSysStyleId.Text = "" Then
            If Integer.Parse(Val(FNHSysStyleId.Properties.Tag.ToString)) > 0 Then

                If HI.MG.ShowMsg.mConfirmProcess(MG.ShowMsg.ProcessType.mSave) = True Then
                    If Me.SaveData(Integer.Parse(Val(FNHSysStyleId.Properties.Tag.ToString))) Then
                        Call LoadDataInfo(FNHSysStyleId.Properties.Tag.ToString)
                        HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                    Else
                        HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                    End If
                End If

            End If
        Else
            Me.ogcdetail.DataSource = Nothing
            Me.ogcStyle.DataSource = Nothing
            Me.ogcSemiDetail.DataSource = Nothing
        End If
    End Sub

    Private Sub ocmdelete_Click(sender As Object, e As EventArgs) Handles ocmdelete.Click


        If Not FNHSysStyleId.Text = "" Then
            If Integer.Parse(Val(FNHSysStyleId.Properties.Tag.ToString)) > 0 Then

                If HI.MG.ShowMsg.mConfirmProcess(MG.ShowMsg.ProcessType.mDelete) = True Then
                    If DeleteData(Integer.Parse(Val(FNHSysStyleId.Properties.Tag.ToString))) Then
                        Call LoadDataInfo(FNHSysStyleId.Properties.Tag.ToString)
                        HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
                    Else
                        HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
                    End If
                End If

            End If
        Else
            Me.ogcdetail.DataSource = Nothing
            Me.ogcStyle.DataSource = Nothing
            Me.ogcSemiDetail.DataSource = Nothing
        End If
    End Sub

    Private Sub FNCostPerMin_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles FNCostPerMin.EditValueChanging
        If _LoadData = True Then Exit Sub

        Try
            FNCostSamPrice.Value = CDbl(Format(Val(e.NewValue.ToString) * Val(FNSam.Value) * Val(FNMultiple.Value), "0.0000"))

            'With CType(Me.ogcdetail.DataSource, DataTable)
            '    .AcceptChanges()
            '    For Each R As DataRow In .Rows
            '        R!FNCostPerMin = Val(e.NewValue.ToString)
            '        R!FNPrice = CDbl(Format(Val(R!FNMultiple.ToString) * Val(R!FNCostPerMin.ToString) * Val(R!FNSam.ToString), "0.0000"))
            '    Next

            '    .AcceptChanges()
            'End With
            With ogcdetail
                If Not (.DataSource Is Nothing) And ogvdetail.RowCount > 0 Then


                    With ogvdetail
                        For I As Integer = 0 To .RowCount - 1

                            .SetRowCellValue(I, .Columns.ColumnByFieldName("FNCostPerMin"), Val(e.NewValue.ToString))

                            .SetRowCellValue(I, "FNPrice", CDbl(Format(Val(.GetRowCellValue(I, .Columns.ColumnByFieldName("FNMultiple"))) * Val(.GetRowCellValue(I, .Columns.ColumnByFieldName("FNCostPerMin"))) * Val(.GetRowCellValue(I, .Columns.ColumnByFieldName("FNSam"))), "0.0000")))

                        Next
                    End With

                    CType(.DataSource, DataTable).AcceptChanges()
                End If

            End With

        Catch ex As Exception

        End Try
    End Sub

    Private Sub FNCostPerMinSmallLot_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles FNCostPerMinSmallLot.EditValueChanging
        If _LoadData = True Then Exit Sub

        Try
            '' FNCostPerMinSmallLot.Value = CDbl(Format(Val(e.NewValue.ToString), "0.0000"))
            FNNetCostSmallLot.Value = CDbl(Format(Val(e.NewValue.ToString) * Val(FNSam.Value) * Val(FNMultiple.Value), "0.0000"))
            'With CType(Me.ogcdetail.DataSource, DataTable)
            '    .AcceptChanges()
            '    For Each R As DataRow In .Rows
            '        R!FNCostPerMinSmallLot = Val(e.NewValue.ToString)

            '        R!FNNetCostSmallLot = CDbl(Format(Val(R!FNMultiple.ToString) * Val(R!FNCostPerMinSmallLot.ToString) * Val(R!FNSam.ToString), "0.0000"))
            '    Next

            '    .AcceptChanges()
            'End With
            With ogcdetail
                If Not (.DataSource Is Nothing) And ogvdetail.RowCount > 0 Then


                    With ogvdetail
                        For I As Integer = 0 To .RowCount - 1

                            .SetRowCellValue(I, .Columns.ColumnByFieldName("FNCostPerMinSmallLot"), Val(e.NewValue.ToString))

                            .SetRowCellValue(I, "FNNetCostSmallLot", CDbl(Format(Val(.GetRowCellValue(I, .Columns.ColumnByFieldName("FNMultiple"))) * Val(.GetRowCellValue(I, .Columns.ColumnByFieldName("FNCostPerMinSmallLot"))) * Val(.GetRowCellValue(I, .Columns.ColumnByFieldName("FNSam"))), "0.0000")))

                        Next
                    End With

                    CType(.DataSource, DataTable).AcceptChanges()
                End If

            End With

        Catch ex As Exception

        End Try
    End Sub

    Private Sub FNSam_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles FNSam.EditValueChanging
        If _LoadData = True Then Exit Sub

        Try
            FNCostSamPrice.Value = CDbl(Format(Val(e.NewValue.ToString) * Val(FNCostPerMin.Value) * Val(FNMultiple.Value), "0.0000"))

            'With CType(Me.ogcdetail.DataSource, DataTable)
            '    .AcceptChanges()


            '    For Each R As DataRow In .Rows
            '        R!FNSam = Val(e.NewValue.ToString)

            '        R!FNPrice = CDbl(Format(Val(R!FNMultiple.ToString) * Val(R!FNCostPerMin.ToString) * Val(R!FNSam.ToString), "0.0000"))
            '        R!FNNetCostSmallLot = CDbl(Format(Val(R!FNMultiple.ToString) * Val(R!FNCostPerMinSmallLot.ToString) * Val(R!FNSam.ToString), "0.0000"))
            '    Next

            '    .AcceptChanges()
            'End With

            With ogcdetail
                If Not (.DataSource Is Nothing) And ogvdetail.RowCount > 0 Then


                    With ogvdetail
                        For I As Integer = 0 To .RowCount - 1

                            .SetRowCellValue(I, .Columns.ColumnByFieldName("FNSam"), Val(e.NewValue.ToString))

                            .SetRowCellValue(I, "FNPrice", CDbl(Format(Val(.GetRowCellValue(I, .Columns.ColumnByFieldName("FNMultiple"))) * Val(.GetRowCellValue(I, .Columns.ColumnByFieldName("FNCostPerMin"))) * Val(.GetRowCellValue(I, .Columns.ColumnByFieldName("FNSam"))), "0.0000")))
                            .SetRowCellValue(I, "FNNetCostSmallLot", CDbl(Format(Val(.GetRowCellValue(I, .Columns.ColumnByFieldName("FNMultiple"))) * Val(.GetRowCellValue(I, .Columns.ColumnByFieldName("FNCostPerMinSmallLot"))) * Val(.GetRowCellValue(I, .Columns.ColumnByFieldName("FNSam"))), "0.0000")))

                        Next
                    End With

                    CType(.DataSource, DataTable).AcceptChanges()
                End If

            End With

        Catch ex As Exception

        End Try
    End Sub

    Private Sub FNMultiple_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles FNMultiple.EditValueChanging
        If _LoadData = True Then Exit Sub

        Try


            If Val(e.NewValue.ToString) <= 0 Then
                e.Cancel = True
            Else
                e.Cancel = False
                FNCostSamPrice.Value = CDbl(Format(Val(e.NewValue.ToString) * (FNCostPerMin.Value) * Val(FNSam.Value), "0.0000"))

                'With CType(Me.ogcdetail.DataSource, DataTable)
                '    .AcceptChanges()

                '    For Each R As DataRow In .Rows
                '        R!FNMultiple = Val(e.NewValue.ToString)

                '        R!FNPrice = CDbl(Format(Val(R!FNMultiple.ToString) * Val(R!FNCostPerMin.ToString) * Val(R!FNSam.ToString), "0.0000"))
                '        R!FNNetCostSmallLot = CDbl(Format(Val(R!FNMultiple.ToString) * Val(R!FNCostPerMinSmallLot.ToString) * Val(R!FNSam.ToString), "0.0000"))
                '    Next

                '    .AcceptChanges()
                'End With

                With ogcdetail
                    If Not (.DataSource Is Nothing) And ogvdetail.RowCount > 0 Then


                        With ogvdetail
                            For I As Integer = 0 To .RowCount - 1

                                .SetRowCellValue(I, .Columns.ColumnByFieldName("FNMultiple"), Val(e.NewValue.ToString))

                                .SetRowCellValue(I, "FNPrice", CDbl(Format(Val(.GetRowCellValue(I, .Columns.ColumnByFieldName("FNMultiple"))) * Val(.GetRowCellValue(I, .Columns.ColumnByFieldName("FNCostPerMin"))) * Val(.GetRowCellValue(I, .Columns.ColumnByFieldName("FNSam"))), "0.0000")))
                                .SetRowCellValue(I, "FNNetCostSmallLot", CDbl(Format(Val(.GetRowCellValue(I, .Columns.ColumnByFieldName("FNMultiple"))) * Val(.GetRowCellValue(I, .Columns.ColumnByFieldName("FNCostPerMinSmallLot"))) * Val(.GetRowCellValue(I, .Columns.ColumnByFieldName("FNSam"))), "0.0000")))

                            Next
                        End With

                        CType(.DataSource, DataTable).AcceptChanges()
                    End If

                End With


            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub ReposFNCostPerMin_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles ReposFNCostPerMin.EditValueChanging, RepFNSamCut.EditValueChanging, ReposFNMultiple.EditValueChanging
        Try
            If Val(e.NewValue.ToString) < 0 Then
                e.Cancel = True
            Else
                e.Cancel = False

                With Me.ogvdetail

                    Dim _Sam As Double = Val("" & .GetFocusedRowCellValue("FNSam").ToString)
                    Dim _Cost As Double = Val("" & .GetFocusedRowCellValue("FNCostPerMin").ToString)
                    Dim _Multi As Double = Val("" & .GetFocusedRowCellValue("FNMultiple").ToString)

                    Select Case .FocusedColumn.FieldName.ToString.ToLower
                        Case "FNCostPerMin".ToLower
                            _Cost = Val(e.NewValue.ToString)
                        Case "FNSam".ToLower
                            _Sam = Val(e.NewValue.ToString)
                        Case "FNMultiple".ToLower
                            _Multi = Val(e.NewValue.ToString)
                    End Select

                    .SetFocusedRowCellValue("FNPrice", CDbl(Format(_Sam * _Cost * _Multi, "0.0000")))

                End With



            End If
        Catch ex As Exception

        End Try


    End Sub


    Private Sub ReposFTStateSmallLot_CheckedChanged(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles ReposFFTStateSmallLot.EditValueChanging
        Try
            'If Val(e.NewValue.ToString) < 0 Then
            '    e.Cancel = True
            'Else
            '    e.Cancel = False

            Dim _Qry As String
            Dim _NewValue As String

            Dim _FTStateSmallLot As String
            Dim _FTOrderNo As String

            _NewValue = e.NewValue.ToString()
            With Me.ogvdetail

                _FTStateSmallLot = "" & .GetFocusedRowCellValue("FTStateSmallLot").ToString
                _FTOrderNo = "" & .GetFocusedRowCellValue("FTOrderNo").ToString
                Dim _Qry_And As String = ""

                If _NewValue = 1 Then
                    .SetFocusedRowCellValue("FTStateSmallLot", "1")
                Else
                    .SetFocusedRowCellValue("FTStateSmallLot", "0")


                End If




            End With

            With CType(Me.ogcdetail.DataSource, DataTable)
                .AcceptChanges()
                For Each R As DataRow In .Rows
                    If R!FTOrderNo.ToString = _FTOrderNo Then
                        R!FTStateSmallLot = _NewValue
                    End If

                Next

                .AcceptChanges()
            End With

            'End If
        Catch ex As Exception

        End Try


    End Sub


    Private Sub FNCostSamPrice_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles FNCostSamPrice.EditValueChanging
        If _LoadData = True Then Exit Sub

        Try

            'With CType(Me.ogcdetail.DataSource, DataTable)
            '    .AcceptChanges()


            '    For Each R As DataRow In .Rows
            '        R!FNPrice = Val(e.NewValue.ToString)
            '    Next

            '    .AcceptChanges()
            'End With


            With ogcdetail
                If Not (.DataSource Is Nothing) And ogvdetail.RowCount > 0 Then


                    With ogvdetail
                        For I As Integer = 0 To .RowCount - 1

                            .SetRowCellValue(I, .Columns.ColumnByFieldName("FNPrice"), Val(e.NewValue.ToString))


                        Next
                    End With

                    CType(.DataSource, DataTable).AcceptChanges()
                End If

            End With

        Catch ex As Exception

        End Try
    End Sub

    Private Sub RepFNPrice_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles RepFNPrice.EditValueChanging
        Try
            If Val(e.NewValue.ToString) < 0 Then
                e.Cancel = True
            Else
                e.Cancel = False


            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub FNCostSamPrice_EditValueChanged(sender As Object, e As EventArgs) Handles FNCostSamPrice.EditValueChanged

    End Sub

    Private Sub FNWringPrice_EditValueChanged(sender As Object, e As EventArgs) Handles FNWringPrice.EditValueChanged

    End Sub

    Private Sub FNWringPrice_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles FNWringPrice.EditValueChanging
        If _LoadData = True Then Exit Sub

        Try

            'With CType(Me.ogcdetail.DataSource, DataTable)
            '    .AcceptChanges()

            '    For Each R As DataRow In .Rows
            '        R!FNWringPrice = Val(e.NewValue.ToString)
            '    Next

            '    .AcceptChanges()
            'End With

            With ogcdetail
                If Not (.DataSource Is Nothing) And ogvdetail.RowCount > 0 Then


                    With ogvdetail
                        For I As Integer = 0 To .RowCount - 1

                            .SetRowCellValue(I, .Columns.ColumnByFieldName("FNWringPrice"), Val(e.NewValue.ToString))


                        Next
                    End With

                    CType(.DataSource, DataTable).AcceptChanges()
                End If

            End With

        Catch ex As Exception

        End Try
    End Sub

    Private Sub ocmclear_Click(sender As Object, e As EventArgs) Handles ocmclear.Click
        Try
            HI.TL.HandlerControl.ClearControl(Me.ogbStyleHeader)
            Me.ogcdetail.DataSource = Nothing
            Me.ogcStyle.DataSource = Nothing
            Me.ogcSemiDetail.DataSource = Nothing
        Catch ex As Exception

        End Try

    End Sub





    Private Sub FNSamEmb_EditValueChanged(sender As Object, e As EventArgs) Handles FNSamEmb.EditValueChanged

    End Sub

    Private Sub FNSamEmb_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles FNSamEmb.EditValueChanging
        If _LoadData = True Then Exit Sub

        Try

            'With CType(Me.ogcdetail.DataSource, DataTable)
            '    .AcceptChanges()

            '    For Each R As DataRow In .Rows
            '        R!FNSamEmb = Val(e.NewValue.ToString)
            '    Next

            '    .AcceptChanges()
            'End With

            With ogcdetail
                If Not (.DataSource Is Nothing) And ogvdetail.RowCount > 0 Then


                    With ogvdetail
                        For I As Integer = 0 To .RowCount - 1

                            .SetRowCellValue(I, .Columns.ColumnByFieldName("FNSamEmb"), Val(e.NewValue.ToString))

                        Next
                    End With

                    CType(.DataSource, DataTable).AcceptChanges()
                End If

            End With



        Catch ex As Exception

        End Try
    End Sub

    Private Sub FNCutPrice_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles FNCutPrice.EditValueChanging
        If _LoadData = True Then Exit Sub

        Try
            FNNetCostCut.Value = CDbl(Format(Val(e.NewValue.ToString) * Val(FNCostCut.Value), "0.0000"))

            'With CType(Me.ogcdetail.DataSource, DataTable)
            '    .AcceptChanges()


            '    For Each R As DataRow In .Rows
            '        R!FNCutPrice = Val(e.NewValue.ToString)
            '        R!FNNetCostCut = CDbl(Format(Val(R!FNCutPrice.ToString) * Val(R!FNCostCut.ToString), "0.0000"))
            '    Next

            '    .AcceptChanges()
            'End With

            With ogcdetail
                If Not (.DataSource Is Nothing) And ogvdetail.RowCount > 0 Then
                    With ogvdetail
                        For I As Integer = 0 To .RowCount - 1

                            .SetRowCellValue(I, .Columns.ColumnByFieldName("FNCutPrice"), Val(e.NewValue.ToString))

                            .SetRowCellValue(I, "FNNetCostCut", CDbl(Format(Val(.GetRowCellValue(I, .Columns.ColumnByFieldName("FNCutPrice"))) * Val(.GetRowCellValue(I, .Columns.ColumnByFieldName("FNCostCut"))), "0.0000")))

                        Next
                    End With
                    CType(.DataSource, DataTable).AcceptChanges()
                End If

            End With

        Catch ex As Exception

        End Try
    End Sub


    Private Sub FNSamSew_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles FNSamSew.EditValueChanging
        If _LoadData = True Then Exit Sub

        Try
            FNNetCostSew.Value = CDbl(Format(Val(e.NewValue.ToString) * Val(FNCostSew.Value), "0.0000"))

            'With CType(Me.ogcdetail.DataSource, DataTable)
            '    .AcceptChanges()


            '    For Each R As DataRow In .Rows
            '        R!FNSamSew = Val(e.NewValue.ToString)
            '        R!FNNetCostSew = CDbl(Format(Val(R!FNSamSew.ToString) * Val(R!FNCostSew.ToString), "0.0000"))
            '    Next

            '    .AcceptChanges()
            'End With
            With ogcdetail
                If Not (.DataSource Is Nothing) And ogvdetail.RowCount > 0 Then
                    With ogvdetail
                        For I As Integer = 0 To .RowCount - 1

                            .SetRowCellValue(I, .Columns.ColumnByFieldName("FNSamSew"), Val(e.NewValue.ToString))

                            .SetRowCellValue(I, "FNNetCostSew", CDbl(Format(Val(.GetRowCellValue(I, .Columns.ColumnByFieldName("FNSamSew"))) * Val(.GetRowCellValue(I, .Columns.ColumnByFieldName("FNCostSew"))), "0.0000")))

                        Next
                    End With
                    CType(.DataSource, DataTable).AcceptChanges()
                End If

            End With


        Catch ex As Exception

        End Try
    End Sub

    Private Sub FNCostSew_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles FNCostSew.EditValueChanging
        If _LoadData = True Then Exit Sub

        Try
            FNNetCostSew.Value = CDbl(Format(Val(e.NewValue.ToString) * Val(FNSamSew.Value), "0.0000"))

            'With CType(Me.ogcdetail.DataSource, DataTable)
            '    .AcceptChanges()


            '    For Each R As DataRow In .Rows
            '        R!FNCostSew = Val(e.NewValue.ToString)
            '        R!FNNetCostSew = CDbl(Format(Val(R!FNSamSew.ToString) * Val(R!FNCostSew.ToString), "0.0000"))
            '    Next

            '    .AcceptChanges()
            'End With

            With ogcdetail
                If Not (.DataSource Is Nothing) And ogvdetail.RowCount > 0 Then
                    With ogvdetail
                        For I As Integer = 0 To .RowCount - 1

                            .SetRowCellValue(I, .Columns.ColumnByFieldName("FNCostSew"), Val(e.NewValue.ToString))

                            .SetRowCellValue(I, "FNNetCostSew", CDbl(Format(Val(.GetRowCellValue(I, .Columns.ColumnByFieldName("FNSamSew"))) * Val(.GetRowCellValue(I, .Columns.ColumnByFieldName("FNCostSew"))), "0.0000")))

                        Next
                    End With
                    CType(.DataSource, DataTable).AcceptChanges()
                End If

            End With

        Catch ex As Exception

        End Try
    End Sub

    Private Sub FNSamPack_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles FNSamPack.EditValueChanging
        If _LoadData = True Then Exit Sub

        Try
            FNNetCostPack.Value = CDbl(Format(Val(e.NewValue.ToString) * Val(FNCostPack.Value), "0.0000"))

            'With CType(Me.ogcdetail.DataSource, DataTable)
            '    .AcceptChanges()


            '    For Each R As DataRow In .Rows
            '        R!FNSamPack = Val(e.NewValue.ToString)
            '        R!FNNetCostPack = CDbl(Format(Val(R!FNSamPack.ToString) * Val(R!FNCostPack.ToString), "0.0000"))
            '    Next

            '    .AcceptChanges()
            'End With
            With ogcdetail
                If Not (.DataSource Is Nothing) And ogvdetail.RowCount > 0 Then
                    With ogvdetail
                        For I As Integer = 0 To .RowCount - 1

                            .SetRowCellValue(I, .Columns.ColumnByFieldName("FNSamPack"), Val(e.NewValue.ToString))
                            .SetRowCellValue(I, "FNNetCostPack", CDbl(Format(Val(.GetRowCellValue(I, .Columns.ColumnByFieldName("FNSamPack"))) * Val(.GetRowCellValue(I, .Columns.ColumnByFieldName("FNCostPack"))), "0.0000")))

                        Next
                    End With
                    CType(.DataSource, DataTable).AcceptChanges()
                End If

            End With


        Catch ex As Exception

        End Try
    End Sub

    Private Sub FNCostPack_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles FNCostPack.EditValueChanging
        If _LoadData = True Then Exit Sub

        Try
            FNNetCostPack.Value = CDbl(Format(Val(e.NewValue.ToString) * Val(FNSamPack.Value), "0.0000"))

            'With CType(Me.ogcdetail.DataSource, DataTable)
            '    .AcceptChanges()


            '    For Each R As DataRow In .Rows
            '        R!FNCostPack = Val(e.NewValue.ToString)
            '        R!FNNetCostPack = CDbl(Format(Val(R!FNSamPack.ToString) * Val(R!FNCostPack.ToString), "0.0000"))
            '    Next

            '    .AcceptChanges()
            'End With

            With ogcdetail
                If Not (.DataSource Is Nothing) And ogvdetail.RowCount > 0 Then
                    With ogvdetail
                        For I As Integer = 0 To .RowCount - 1

                            .SetRowCellValue(I, .Columns.ColumnByFieldName("FNCostPack"), Val(e.NewValue.ToString))
                            .SetRowCellValue(I, "FNNetCostPack", CDbl(Format(Val(.GetRowCellValue(I, .Columns.ColumnByFieldName("FNSamPack"))) * Val(.GetRowCellValue(I, .Columns.ColumnByFieldName("FNCostPack"))), "0.0000")))

                        Next
                    End With
                    CType(.DataSource, DataTable).AcceptChanges()
                End If

            End With

        Catch ex As Exception

        End Try
    End Sub


    Private Sub FNSamSemiPart_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles FNSamSemiPart.EditValueChanging
        If _LoadData = True Then Exit Sub

        Try
            FNNetCostSemiPart.Value = CDbl(Format(Val(e.NewValue.ToString) * Val(FNCostSemiPart.Value), "0.0000"))

            'With CType(Me.ogcdetail.DataSource, DataTable)
            '    .AcceptChanges()


            '    For Each R As DataRow In .Rows
            '        R!FNSamSemiPart = Val(e.NewValue.ToString)
            '        R!FNNetCostSemiPart = CDbl(Format(Val(R!FNSamSemiPart.ToString) * Val(R!FNCostSemiPart.ToString), "0.0000"))
            '    Next

            '    .AcceptChanges()
            'End With
            With ogcdetail
                If Not (.DataSource Is Nothing) And ogvdetail.RowCount > 0 Then
                    With ogvdetail
                        For I As Integer = 0 To .RowCount - 1

                            .SetRowCellValue(I, .Columns.ColumnByFieldName("FNSamSemiPart"), Val(e.NewValue.ToString))
                            .SetRowCellValue(I, "FNNetCostSemiPart", CDbl(Format(Val(.GetRowCellValue(I, .Columns.ColumnByFieldName("FNSamSemiPart"))) * Val(.GetRowCellValue(I, .Columns.ColumnByFieldName("FNCostSemiPart"))), "0.0000")))

                        Next
                    End With
                    CType(.DataSource, DataTable).AcceptChanges()
                End If

            End With


        Catch ex As Exception

        End Try
    End Sub

    Private Sub FNCostSemiPart_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles FNCostSemiPart.EditValueChanging
        If _LoadData = True Then Exit Sub

        Try
            FNNetCostSemiPart.Value = CDbl(Format(Val(e.NewValue.ToString) * Val(FNSamSemiPart.Value), "0.0000"))

            'With CType(Me.ogcdetail.DataSource, DataTable)
            '    .AcceptChanges()


            '    For Each R As DataRow In .Rows
            '        R!FNCostSemiPart = Val(e.NewValue.ToString)
            '        R!FNNetCostSemiPart = CDbl(Format(Val(R!FNSamSemiPart.ToString) * Val(R!FNCostSemiPart.ToString), "0.0000"))
            '    Next

            '    .AcceptChanges()
            'End With
            With ogcdetail
                If Not (.DataSource Is Nothing) And ogvdetail.RowCount > 0 Then
                    With ogvdetail
                        For I As Integer = 0 To .RowCount - 1

                            .SetRowCellValue(I, .Columns.ColumnByFieldName("FNCostSemiPart"), Val(e.NewValue.ToString))
                            .SetRowCellValue(I, "FNNetCostSemiPart", CDbl(Format(Val(.GetRowCellValue(I, .Columns.ColumnByFieldName("FNSamSemiPart"))) * Val(.GetRowCellValue(I, .Columns.ColumnByFieldName("FNCostSemiPart"))), "0.0000")))

                        Next
                    End With
                    CType(.DataSource, DataTable).AcceptChanges()
                End If

            End With


        Catch ex As Exception

        End Try
    End Sub


    Private Sub ReposFNCutPrice_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles ReposFNCutPrice.EditValueChanging, ReposFNCostCut.EditValueChanging
        Try
            If Val(e.NewValue.ToString) < 0 Then
                e.Cancel = True
            Else
                e.Cancel = False

                With Me.ogvdetail

                    Dim _CutPrice As Double = Val("" & .GetFocusedRowCellValue("FNCutPrice").ToString)
                    Dim _CostCut As Double = Val("" & .GetFocusedRowCellValue("FNCostCut").ToString)

                    Select Case .FocusedColumn.FieldName.ToString.ToLower
                        Case "FNCutPrice".ToLower
                            _CutPrice = Val(e.NewValue.ToString)
                        Case "FNCostCut".ToLower
                            _CostCut = Val(e.NewValue.ToString)
                    End Select

                    .SetFocusedRowCellValue("FNNetCostCut", CDbl(Format(_CutPrice * _CostCut, "0.0000")))

                End With

            End If
        Catch ex As Exception

        End Try

    End Sub
    Private Sub ReposFNSamCut_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles RepFNSamCut.EditValueChanging, ReposFNCostCut.EditValueChanging
        Try
            If Val(e.NewValue.ToString) < 0 Then
                e.Cancel = True
            Else
                e.Cancel = False

                With Me.ogvdetail

                    Dim _SamCut As Double = Val("" & .GetFocusedRowCellValue("FNSamCut").ToString)
                    Dim _CostCut As Double = Val("" & .GetFocusedRowCellValue("FNCostCut").ToString)

                    Select Case .FocusedColumn.FieldName.ToString.ToLower
                        Case "FNSamCut".ToLower
                            _SamCut = Val(e.NewValue.ToString)
                        Case "FNCostCut".ToLower
                            _CostCut = Val(e.NewValue.ToString)
                    End Select

                    .SetFocusedRowCellValue("FNNetCostCut", CDbl(Format(_SamCut * _CostCut, "0.0000")))

                End With

            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub ReposFNNetCostCut_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles ReposFNNetCostCut.EditValueChanging
        Try
            If Val(e.NewValue.ToString) < 0 Then
                e.Cancel = True
            Else
                e.Cancel = False

                With Me.ogvdetail

                    '' Dim _SamCut As Double = Val("" & .GetFocusedRowCellValue("FNSamCut").ToString)

                    Dim _FNNetCostCut As Double
                    Dim _CostCut As Double = Val("" & .GetFocusedRowCellValue("FNCostCut").ToString)

                    Select Case .FocusedColumn.FieldName.ToString.ToLower
                        'Case "FNSamCut".ToLower
                        '    _SamCut = Val(e.NewValue.ToString)
                        Case "FNNetCostCut".ToLower
                            _FNNetCostCut = Val(e.NewValue.ToString)
                    End Select

                    .SetFocusedRowCellValue("FNSamCut", CDbl(Format(_FNNetCostCut / _CostCut, "0.0000")))

                End With

            End If
        Catch ex As Exception

        End Try

    End Sub

    'Private Sub ReposFNCostCut_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles ReposFNCostCut.EditValueChanging

    'End Sub

    Private Sub ReposFNSamSew_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles ReposFNSamSew.EditValueChanging, ReposFNCostSew.EditValueChanging
        Try
            If Val(e.NewValue.ToString) < 0 Then
                e.Cancel = True
            Else
                e.Cancel = False

                With Me.ogvdetail

                    Dim _SamSew As Double = Val("" & .GetFocusedRowCellValue("FNSamSew").ToString)
                    Dim _CostSew As Double = Val("" & .GetFocusedRowCellValue("FNCostSew").ToString)

                    Select Case .FocusedColumn.FieldName.ToString.ToLower
                        Case "FNSamSew".ToLower
                            _SamSew = Val(e.NewValue.ToString)
                        Case "FNCostSew".ToLower
                            _CostSew = Val(e.NewValue.ToString)
                    End Select

                    .SetFocusedRowCellValue("FNNetCostSew", CDbl(Format(_SamSew * _CostSew, "0.0000")))

                End With

            End If
        Catch ex As Exception

        End Try
    End Sub


    Private Sub ReposFNSamPack_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles ReposFNSamPack.EditValueChanging, ReposFNCostPack.EditValueChanging
        Try
            If Val(e.NewValue.ToString) < 0 Then
                e.Cancel = True
            Else
                e.Cancel = False

                With Me.ogvdetail

                    Dim _SamPack As Double = Val("" & .GetFocusedRowCellValue("FNSamPack").ToString)
                    Dim _CostPack As Double = Val("" & .GetFocusedRowCellValue("FNCostPack").ToString)

                    Select Case .FocusedColumn.FieldName.ToString.ToLower
                        Case "FNSamPack".ToLower
                            _SamPack = Val(e.NewValue.ToString)
                        Case "FNCostPack".ToLower
                            _CostPack = Val(e.NewValue.ToString)
                    End Select

                    .SetFocusedRowCellValue("FNNetCostPack", CDbl(Format(_SamPack * _CostPack, "0.0000")))

                End With

            End If
        Catch ex As Exception

        End Try
    End Sub


    Private Sub ReposFNSamSemiPart_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles ReposFNSamSemiPart.EditValueChanging, ReposFNCostSemiPart.EditValueChanging
        Try
            If Val(e.NewValue.ToString) < 0 Then
                e.Cancel = True
            Else
                e.Cancel = False

                With Me.ogvdetail

                    Dim _SamSemiPart As Double = Val("" & .GetFocusedRowCellValue("FNSamSemiPart").ToString)
                    Dim _CostSemiPart As Double = Val("" & .GetFocusedRowCellValue("FNCostSemiPart").ToString)

                    Select Case .FocusedColumn.FieldName.ToString.ToLower
                        Case "FNSamSemiPart".ToLower
                            _SamSemiPart = Val(e.NewValue.ToString)
                        Case "FNCostSemiPart".ToLower
                            _CostSemiPart = Val(e.NewValue.ToString)
                    End Select

                    .SetFocusedRowCellValue("FNNetCostSemipart", CDbl(Format(_SamSemiPart * _CostSemiPart, "0.0000")))

                End With

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub GroupControl1_Paint(sender As Object, e As PaintEventArgs) Handles otbHeader.Paint

    End Sub



    Private Sub ReposSFNSam_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles ReposSFNSam.EditValueChanging, ReposSFNCost.EditValueChanging, ReposSFNMultiple.EditValueChanging

        Try

            Dim _f As String = ""
            Dim FNHSysPartId As Integer = 0
            Dim FNSendSuplType As Integer = 0
            Dim FTNote As String = ""

            If Val(e.NewValue.ToString) < 0 Then
                e.Cancel = True
            Else
                e.Cancel = False

                With Me.ogvStyle

                    Dim _FNSam As Double = Val("" & .GetFocusedRowCellValue("FNSam").ToString)
                    Dim _FNCost As Double = Val("" & .GetFocusedRowCellValue("FNCost").ToString)
                    Dim _FNMultiple As Double = Val("" & .GetFocusedRowCellValue("FNMultiple").ToString)

                    FNHSysPartId = Val("" & .GetFocusedRowCellValue("FNHSysPartId").ToString)
                    FNSendSuplType = Val("" & .GetFocusedRowCellValue("FNSendSuplType").ToString)
                    FTNote = "" & .GetFocusedRowCellValue("FTNote").ToString

                    Select Case .FocusedColumn.FieldName.ToString.ToUpper
                        Case "FNSAM".ToUpper
                            _FNSam = Val(e.NewValue.ToString)
                            _f = "FNSAM"
                        Case "FNCOST".ToUpper
                            _FNCost = Val(e.NewValue.ToString)
                            _f = "FNCOST"
                        Case "FNMULTIPLE".ToUpper
                            _FNMultiple = Val(e.NewValue.ToString)
                            _f = "FNMULTIPLE"
                    End Select

                    .SetFocusedRowCellValue("FNNetCost", CDbl(Format(_FNSam * _FNCost * _FNMultiple, "0.0000")))

                End With

                Dim Row As Integer = 0
                Dim d_FNHSysPartId As Integer = 0
                Dim d_FNSendSuplType As Integer = 0
                Dim d_FTNote As String = ""

                'With DirectCast(Me.ogcSemiDetail.DataSource, DataTable)
                '    .AcceptChanges()
                '    For Each R As DataRow In .Rows


                '        d_FNHSysPartId = ogvSemiDetail.GetRowCellValue(Row, "FNHSysPartId").ToString()
                '        d_FNSendSuplType = ogvSemiDetail.GetRowCellValue(Row, "FNSendSuplType").ToString()
                '        d_FTNote = ogvSemiDetail.GetRowCellValue(Row, "FTNote").ToString()

                '        If FNHSysPartId = d_FNHSysPartId And FNSendSuplType = d_FNSendSuplType And FTNote = d_FTNote Then

                '            Select Case _f
                '                Case "FNSAM"
                '                    R!FNSam = Val(e.NewValue.ToString)
                '                    ogvSemiDetail.SetRowCellValue(Row, "FNSam", Val(e.NewValue.ToString))
                '                Case "FNCOST"
                '                    R!FNCost = Val(e.NewValue.ToString)
                '                    ogvSemiDetail.SetRowCellValue(Row, "FNCost", Val(e.NewValue.ToString))
                '                Case "FNMULTIPLE"
                '                    R!FNMultiple = Val(e.NewValue.ToString)
                '                    ogvSemiDetail.SetRowCellValue(Row, "FNMultiple", Val(e.NewValue.ToString))

                '            End Select

                '            R!FNNetCost = CDbl(Format(Val(R!FNMultiple.ToString) * Val(R!FNCost.ToString) * Val(R!FNSam.ToString), "0.0000"))
                '            ogvSemiDetail.SetRowCellValue(Row, "FNNetCost", CDbl(Format(Val(R!FNMultiple.ToString) * Val(R!FNCost.ToString) * Val(R!FNSam.ToString), "0.0000")))
                '        End If
                '        Row += 1
                '    Next

                '    .AcceptChanges()
                'End With


                With ogcSemiDetail
                    If Not (.DataSource Is Nothing) And ogvSemiDetail.RowCount > 0 Then


                        With ogvSemiDetail
                            For I As Integer = 0 To .RowCount - 1

                                d_FNHSysPartId = ogvSemiDetail.GetRowCellValue(I, "FNHSysPartId").ToString()
                                d_FNSendSuplType = ogvSemiDetail.GetRowCellValue(I, "FNSendSuplType").ToString()
                                d_FTNote = ogvSemiDetail.GetRowCellValue(I, "FTNote").ToString()

                                If FNHSysPartId = d_FNHSysPartId And FNSendSuplType = d_FNSendSuplType And FTNote = d_FTNote Then

                                    Select Case _f
                                        Case "FNSAM"
                                            .SetRowCellValue(I, .Columns.ColumnByFieldName("FNSam"), Val(e.NewValue.ToString))
                                        Case "FNCOST"
                                            .SetRowCellValue(I, .Columns.ColumnByFieldName("FNCost"), Val(e.NewValue.ToString))
                                        Case "FNMULTIPLE"
                                            .SetRowCellValue(I, .Columns.ColumnByFieldName("FNMultiple"), Val(e.NewValue.ToString))

                                    End Select
                                    ogvSemiDetail.SetRowCellValue(I, "FNNetCost", CDbl(Format(Val(.GetRowCellValue(I, .Columns.ColumnByFieldName("FNMultiple"))) * Val(.GetRowCellValue(I, .Columns.ColumnByFieldName("FNCost"))) * Val(.GetRowCellValue(I, .Columns.ColumnByFieldName("FNSam"))), "0.0000")))


                                End If


                            Next
                        End With

                        CType(.DataSource, DataTable).AcceptChanges()
                    End If

                End With


            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub ReposSOFNSam_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles ReposSOFNSam.EditValueChanging, ReposSOFNCost.EditValueChanging, ReposSOFNMultiple.EditValueChanging

        Try
            If Val(e.NewValue.ToString) < 0 Then
                e.Cancel = True
            Else
                e.Cancel = False

                With Me.ogvSemiDetail

                    Dim _FNSam As Double = Val("" & .GetFocusedRowCellValue("FNSam").ToString)
                    Dim _FNCost As Double = Val("" & .GetFocusedRowCellValue("FNCost").ToString)
                    Dim _FNMultiple As Double = Val("" & .GetFocusedRowCellValue("FNMultiple").ToString)

                    Select Case .FocusedColumn.FieldName.ToString.ToUpper
                        Case "FNSAM".ToUpper
                            _FNSam = Val(e.NewValue.ToString)
                        Case "FNCOST".ToUpper
                            _FNCost = Val(e.NewValue.ToString)
                        Case "FNMULTIPLE".ToUpper
                            _FNMultiple = Val(e.NewValue.ToString)
                    End Select

                    .SetFocusedRowCellValue("FNNetCost", CDbl(Format(_FNSam * _FNCost * _FNMultiple, "0.0000")))

                End With

            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub FNSamWring_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles FNSamWring.EditValueChanging
        If _LoadData = True Then Exit Sub

        Try
            FNWringPrice.Value = CDbl(Format(Val(e.NewValue.ToString) * Val(FNCostWring.Value), "0.0000"))


            With ogcdetail
                If Not (.DataSource Is Nothing) And ogvdetail.RowCount > 0 Then
                    With ogvdetail
                        For I As Integer = 0 To .RowCount - 1

                            .SetRowCellValue(I, .Columns.ColumnByFieldName("FNSamWring"), Val(e.NewValue.ToString))

                            .SetRowCellValue(I, "FNWringPrice", CDbl(Format(Val(.GetRowCellValue(I, .Columns.ColumnByFieldName("FNSamWring"))) * Val(.GetRowCellValue(I, .Columns.ColumnByFieldName("FNCostWring"))), "0.0000")))

                        Next
                    End With
                    CType(.DataSource, DataTable).AcceptChanges()
                End If

            End With


        Catch ex As Exception

        End Try
    End Sub

    Private Sub FNCostWring_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles FNCostWring.EditValueChanging
        If _LoadData = True Then Exit Sub

        Try
            FNWringPrice.Value = CDbl(Format(Val(e.NewValue.ToString) * Val(FNSamWring.Value), "0.0000"))



            With ogcdetail
                If Not (.DataSource Is Nothing) And ogvdetail.RowCount > 0 Then
                    With ogvdetail
                        For I As Integer = 0 To .RowCount - 1

                            .SetRowCellValue(I, .Columns.ColumnByFieldName("FNCostWring"), Val(e.NewValue.ToString))

                            .SetRowCellValue(I, "FNWringPrice", CDbl(Format(Val(.GetRowCellValue(I, .Columns.ColumnByFieldName("FNSamWring"))) * Val(.GetRowCellValue(I, .Columns.ColumnByFieldName("FNCostWring"))), "0.0000")))

                        Next
                    End With
                    CType(.DataSource, DataTable).AcceptChanges()
                End If

            End With

        Catch ex As Exception

        End Try
    End Sub

    Private Sub ReposFNSamWring_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles ReposFNSamWring.EditValueChanging, ReposFNCostWring.EditValueChanging
        Try
            If Val(e.NewValue.ToString) < 0 Then
                e.Cancel = True
            Else
                e.Cancel = False

                With Me.ogvdetail

                    Dim _FNSamWring As Double = Val("" & .GetFocusedRowCellValue("FNSamWring").ToString)
                    Dim _FNCostWring As Double = Val("" & .GetFocusedRowCellValue("FNCostWring").ToString)

                    Select Case .FocusedColumn.FieldName.ToString.ToLower
                        Case "FNSamWring".ToLower
                            _FNSamWring = Val(e.NewValue.ToString)
                        Case "FNCostWring".ToLower
                            _FNCostWring = Val(e.NewValue.ToString)
                    End Select

                    .SetFocusedRowCellValue("FNWringPrice", CDbl(Format(_FNSamWring * _FNCostWring, "0.0000")))

                End With

            End If
        Catch ex As Exception

        End Try


    End Sub
    Private Sub FNSamCut_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles FNSamCut.EditValueChanging

        'End Try
        If _LoadData = True Then Exit Sub

        Try
            RemoveHandler FNNetCostCut.EditValueChanging, AddressOf FNNetCostCut_EditValueChanging
            FNNetCostCut.Value = CDbl(Format(Val(e.NewValue.ToString) * Val(FNCostCut.Value), "0.0000"))

            'With CType(Me.ogcdetail.DataSource, DataTable)
            '    .AcceptChanges()


            '    For Each R As DataRow In .Rows
            '        R!FNSamCut = Val(e.NewValue.ToString)
            '        R!FNNetCostCut = CDbl(Format(Val(R!FNSamCut.ToString) * Val(R!FNCostCut.ToString), "0.0000"))
            '    Next

            '    .AcceptChanges()
            'End With

            With ogcdetail
                If Not (.DataSource Is Nothing) And ogvdetail.RowCount > 0 Then


                    With ogvdetail
                        For I As Integer = 0 To .RowCount - 1

                            .SetRowCellValue(I, .Columns.ColumnByFieldName("FNSamCut"), Val(e.NewValue.ToString))

                            .SetRowCellValue(I, "FNNetCostCut", CDbl(Format(Val(.GetRowCellValue(I, .Columns.ColumnByFieldName("FNSamCut"))) * Val(.GetRowCellValue(I, .Columns.ColumnByFieldName("FNCostCut"))), "0.0000")))

                        Next
                    End With

                    CType(.DataSource, DataTable).AcceptChanges()
                End If

            End With
            AddHandler FNNetCostCut.EditValueChanging, AddressOf FNNetCostCut_EditValueChanging
        Catch ex As Exception

        End Try
    End Sub





    Private Sub FNCostCut_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles FNCostCut.EditValueChanging
        If _LoadData = True Then Exit Sub

        Try
            RemoveHandler FNNetCostCut.EditValueChanging, AddressOf FNNetCostCut_EditValueChanging
            FNNetCostCut.Value = CDbl(Format(Val(e.NewValue.ToString) * Val(FNSamCut.Value), "0.0000"))


            With ogcdetail
                If Not (.DataSource Is Nothing) And ogvdetail.RowCount > 0 Then
                    With ogvdetail
                        For I As Integer = 0 To .RowCount - 1

                            .SetRowCellValue(I, .Columns.ColumnByFieldName("FNCostCut"), Val(e.NewValue.ToString))

                            .SetRowCellValue(I, "FNNetCostCut", CDbl(Format(Val(.GetRowCellValue(I, .Columns.ColumnByFieldName("FNSamCut"))) * Val(.GetRowCellValue(I, .Columns.ColumnByFieldName("FNCostCut"))), "0.0000")))

                        Next
                    End With
                    CType(.DataSource, DataTable).AcceptChanges()
                End If

            End With

            AddHandler FNNetCostCut.EditValueChanging, AddressOf FNNetCostCut_EditValueChanging
        Catch ex As Exception

        End Try
    End Sub

    Private Sub FNNetCostCut_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles FNNetCostCut.EditValueChanging
        If _LoadData = True Then Exit Sub

        Try
            RemoveHandler FNSamCut.EditValueChanging, AddressOf FNSamCut_EditValueChanging

            FNSamCut.Value = CDbl(Format(Val(e.NewValue.ToString) / Val(FNCostCut.Value), "0.0000"))



            With ogcdetail
                If Not (.DataSource Is Nothing) And ogvdetail.RowCount > 0 Then
                    With ogvdetail
                        For I As Integer = 0 To .RowCount - 1

                            .SetRowCellValue(I, .Columns.ColumnByFieldName("FNNetCostCut"), Val(e.NewValue.ToString))

                            .SetRowCellValue(I, "FNSamCut", CDbl(Format(Val(.GetRowCellValue(I, .Columns.ColumnByFieldName("FNNetCostCut"))) / Val(.GetRowCellValue(I, .Columns.ColumnByFieldName("FNCostCut"))), "0.0000")))

                        Next
                    End With
                    CType(.DataSource, DataTable).AcceptChanges()
                End If

            End With
            AddHandler FNSamCut.EditValueChanging, AddressOf FNSamCut_EditValueChanging

        Catch ex As Exception

        End Try
    End Sub
End Class
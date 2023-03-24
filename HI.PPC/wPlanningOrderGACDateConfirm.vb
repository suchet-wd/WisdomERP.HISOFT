Imports System.Windows.Forms
Imports DevExpress.Data
Imports DevExpress.Utils
Imports DevExpress.XtraCharts
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo

Public Class wPlanningOrderGACDateConfirm

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.


        Call InitGrid()

    End Sub

#Region "Initial Grid"

    Private Sub InitGrid()
        '------Start Add Summary Grid-------------
        Dim sFieldCount As String = ""
        Dim sFieldSum As String = "FNQuantity|FNQuantityExtra|FNGarmentQtyTest|FNGrandQuantity"
        Dim sFieldSumAmt As String = ""

        Dim sFieldGrpCount As String = ""
        Dim sFieldGrpSum As String = ""

        Dim sFieldGrpSumAmt As String = ""
        Dim sFieldCustomSum As String = ""
        Dim sFieldCustomGrpSum As String = ""

        With ogv
            .ClearGrouping()
            .ClearDocument()

            For Each Str As String In sFieldCount.Split("|")
                If Str <> "" Then
                    .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Count, Str)
                    .Columns(Str).SummaryItem.DisplayFormat = "{0:n0}"
                End If
            Next

            For Each Str As String In sFieldCustomSum.Split("|")
                If Str <> "" Then
                    .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Custom, Str)
                    .Columns(Str).SummaryItem.DisplayFormat = "{0:n0}"
                End If
            Next

            For Each Str As String In sFieldSum.Split("|")
                If Str <> "" Then
                    .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Sum, Str)
                    .Columns(Str).SummaryItem.DisplayFormat = "{0:n0}"
                End If
            Next

            For Each Str As String In sFieldSumAmt.Split("|")
                If Str <> "" Then
                    .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Sum, Str)
                    .Columns(Str).SummaryItem.DisplayFormat = "{0:n2}"
                End If
            Next

            For Each Str As String In sFieldGrpCount.Split("|")
                If Str <> "" Then
                    .GroupSummary.Add(DevExpress.Data.SummaryItemType.Count, Str, Nothing, "(Count by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n0})")
                End If
            Next

            For Each Str As String In sFieldCustomGrpSum.Split("|")
                If Str <> "" Then
                    .GroupSummary.Add(DevExpress.Data.SummaryItemType.Custom, Str, Nothing, "(Sum by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n0})")
                End If
            Next

            For Each Str As String In sFieldGrpSum.Split("|")
                If Str <> "" Then
                    .GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, Str, Nothing, "(Sum by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n0})")
                End If
            Next

            For Each Str As String In sFieldGrpSumAmt.Split("|")
                If Str <> "" Then
                    .GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, Str, Nothing, "(Sum by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n2})")
                End If
            Next

            .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
            .OptionsView.ShowFooter = True
            .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleIfExpanded

            '.OptionsSelection.MultiSelect = True
            '.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect
            .ExpandAllGroups()
            .RefreshData()


        End With

        HI.TL.HandlerControl.AddHandlerGridColumnEdit(ogv)


        With ogvdetailhistory
            .ClearGrouping()
            .ClearDocument()

            For Each Str As String In sFieldCount.Split("|")
                If Str <> "" Then
                    .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Count, Str)
                    .Columns(Str).SummaryItem.DisplayFormat = "{0:n0}"
                End If
            Next

            For Each Str As String In sFieldCustomSum.Split("|")
                If Str <> "" Then
                    .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Custom, Str)
                    .Columns(Str).SummaryItem.DisplayFormat = "{0:n0}"
                End If
            Next

            For Each Str As String In sFieldSum.Split("|")
                If Str <> "" Then
                    .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Sum, Str)
                    .Columns(Str).SummaryItem.DisplayFormat = "{0:n0}"
                End If
            Next

            For Each Str As String In sFieldSumAmt.Split("|")
                If Str <> "" Then
                    .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Sum, Str)
                    .Columns(Str).SummaryItem.DisplayFormat = "{0:n2}"
                End If
            Next

            For Each Str As String In sFieldGrpCount.Split("|")
                If Str <> "" Then
                    .GroupSummary.Add(DevExpress.Data.SummaryItemType.Count, Str, Nothing, "(Count by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n0})")
                End If
            Next

            For Each Str As String In sFieldCustomGrpSum.Split("|")
                If Str <> "" Then
                    .GroupSummary.Add(DevExpress.Data.SummaryItemType.Custom, Str, Nothing, "(Sum by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n0})")
                End If
            Next

            For Each Str As String In sFieldGrpSum.Split("|")
                If Str <> "" Then
                    .GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, Str, Nothing, "(Sum by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n0})")
                End If
            Next

            For Each Str As String In sFieldGrpSumAmt.Split("|")
                If Str <> "" Then
                    .GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, Str, Nothing, "(Sum by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n2})")
                End If
            Next

            .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
            .OptionsView.ShowFooter = True
            .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleIfExpanded

            .ExpandAllGroups()
            .RefreshData()


        End With
        '------End Add Summary Grid-------------
    End Sub
#End Region



#Region "Procedure"

    Private Sub LoadData()
        Dim _Qry As String = ""
        Dim dt As New DataTable

        Dim _Spls As New HI.TL.SplashScreen("Loading Data... Please Wait... ")

        Try

            _Qry = " Select '0' as FTSelect , FTCmpCode,FNHSysCmpId  , A.FTInsUser, A.FDInsDate, A.FTInsTime"
            _Qry &= vbCrLf & " , FTItemNo"
            _Qry &= vbCrLf & " , FNItemQuantity, FNHSysSuplId_Hide ,    FNHSysSuplId ,   FDDaliveryDate, FTRevisedReson "
            _Qry &= vbCrLf & "  ,FTCmpName"
            _Qry &= vbCrLf & "  ,FTOrderNo"
            _Qry &= vbCrLf & "  ,FTSubOrderNo,FTPORef"
            _Qry &= vbCrLf & "   ,FDOrderDate"
            _Qry &= vbCrLf & "   ,FTCustCode"
            _Qry &= vbCrLf & "   ,FTCustName"
            _Qry &= vbCrLf & "   ,FTStyleCode"
            _Qry &= vbCrLf & "   ,FTStyleName"
            _Qry &= vbCrLf & "   ,FNQuantity"
            _Qry &= vbCrLf & "  ,FNQuantityExtra "
            _Qry &= vbCrLf & "   ,FNGarmentQtyTest "
            _Qry &= vbCrLf & "  ,FNGrandQuantity "
            _Qry &= vbCrLf & "  ,CASE WHEN ISDATE(FDOriginalShipDate) = 1 THEN Convert(datetime,FDOriginalShipDate) ELSE NULL END AS FDOriginalShipDate"
            _Qry &= vbCrLf & "  ,CASE WHEN ISDATE(FDShipDateOrginalO) = 1 THEN Convert(datetime,FDShipDateOrginalO) ELSE NULL END AS FDShipDateOrginalO"
            _Qry &= vbCrLf & "  ,FNTotalRevised"
            _Qry &= vbCrLf & "   ,CASE WHEN ISDATE(FDShipDate) = 1 THEN Convert(datetime,FDShipDate) ELSE NULL END AS FDShipDate"
            _Qry &= vbCrLf & "   ,CASE WHEN ISDATE(FDShipDateTo) = 1 THEN Convert(datetime,FDShipDateTo) ELSE NULL END AS FDShipDateTo"
            _Qry &= vbCrLf & "   ,CASE WHEN ISDATE(FDShipDateOrginal) = 1 THEN Convert(datetime,FDShipDateOrginal) ELSE NULL END AS FDShipDateOrginal"
            _Qry &= vbCrLf & "   ,CASE WHEN ISDATE(FDShipDateOrginalTo) = 1 THEN Convert(datetime,FDShipDateOrginalTo) ELSE NULL END AS FDShipDateOrginalTo"
            _Qry &= vbCrLf & "   ,FDOriginalShipDate AS FDOriginalShipDateT"
            _Qry &= vbCrLf & "   ,FDShipDate AS  FDShipDateT"
            _Qry &= vbCrLf & "  ,FDShipDateTo AS FDShipDateToT"
            _Qry &= vbCrLf & "   ,FDShipDateOrginalO AS FDShipDateOrginalOT"
            _Qry &= vbCrLf & "   ,FDShipDateOrginal AS  FDShipDateOrginalT"
            _Qry &= vbCrLf & "  ,FDShipDateOrginalTo AS FDShipDateOrginalToT"
            _Qry &= vbCrLf & "  ,'0' AS FTStateChange"
            _Qry &= vbCrLf & "  ,'0' AS FTStateChangeO"
            _Qry &= vbCrLf & "   ,CASE WHEN ISDATE(FDCfmShipDate) = 1 THEN Convert(datetime,FDCfmShipDate) ELSE NULL END AS FDCfmShipDate"
            _Qry &= vbCrLf & "   ,CASE WHEN ISDATE(FDORShipDate) = 1 THEN Convert(datetime,FDORShipDate) ELSE NULL END AS FDORShipDate"
            _Qry &= vbCrLf & "   ,FTReasonDesc AS FTReasonDesc , FTNikePOLineItem"
            _Qry &= vbCrLf & " ,FTContinentCode , FTCountryCode , FTShipModeCode , FTProvinceCode , FTContinentName ,FTCountryName , FTShipModeName ,FTProvinceName"

            _Qry &= vbCrLf & "  FROM (SELECT Cmp.FTCmpCode,O.FNHSysCmpId"
            _Qry &= vbCrLf & " , Cus.FTCustCode "
            _Qry &= vbCrLf & " , ST.FTStyleCode "
            _Qry &= vbCrLf & " , CT.FTContinentCode "
            _Qry &= vbCrLf & " , CM.FTCountryCode "
            _Qry &= vbCrLf & " , SM.FTShipModeCode "
            _Qry &= vbCrLf & " , PT.FTProvinceCode  "


            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then

                _Qry &= vbCrLf & "  , Cmp.FTCmpNameTH AS FTCmpName "
                _Qry &= vbCrLf & " , Cus.FTCustNameTH AS FTCustName"
                _Qry &= vbCrLf & " , ST.FTStyleNameTH AS FTStyleName"
                _Qry &= vbCrLf & " , CT.FTContinentNameTH as FTContinentName "
                _Qry &= vbCrLf & " , CM.FTCountryNameTH as FTCountryName "
                _Qry &= vbCrLf & " , SM.FTShipModenNameTH as FTShipModeName "
                _Qry &= vbCrLf & " , PT.FTProvinceNameTH as FTProvinceName "
            Else

                _Qry &= vbCrLf & " , Cmp.FTCmpNameEN AS FTCmpName "
                _Qry &= vbCrLf & " , Cus.FTCustNameEN AS FTCustName"
                _Qry &= vbCrLf & " , ST.FTStyleNameEN AS FTStyleName"
                _Qry &= vbCrLf & " , CT.FTContinentNameEN as FTContinentName "
                _Qry &= vbCrLf & " , CM.FTCountryNameEN as FTCountryName "
                _Qry &= vbCrLf & " , SM.FTShipModeNameEN as FTShipModeName "
                _Qry &= vbCrLf & " , PT.FTProvinceNameEN as FTProvinceName "

            End If

            _Qry &= vbCrLf & " , O.FTOrderNo"
            _Qry &= vbCrLf & " , D.FTSubOrderNo"
            _Qry &= vbCrLf & " , O.FDOrderDate"
            _Qry &= vbCrLf & " "
            _Qry &= vbCrLf & " ,ISNULL((SELECT TOP 1 FDShipDate"
            _Qry &= vbCrLf & "    FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TMERTOrderGACDateCfm WITH(NOLOCK)"
            _Qry &= vbCrLf & "    WHERE FTOrderNo = O.FTOrderNo And FTSubOrderNo = D.FTSubOrderNo and FTNikePOLineItem = D.FTNikePOLineItem"
            _Qry &= vbCrLf & " 	  ORDER BY FNSeq"
            _Qry &= vbCrLf & " 	), D.FDShipDate) AS FDOriginalShipDate"
            _Qry &= vbCrLf & " ,ISNULL((SELECT TOP 1 FDOShipDate"
            _Qry &= vbCrLf & "    FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TMERTOrderGACDateCfm WITH(NOLOCK)"
            _Qry &= vbCrLf & "    WHERE FTOrderNo = O.FTOrderNo And FTSubOrderNo = D.FTSubOrderNo and FTNikePOLineItem = D.FTNikePOLineItem"
            _Qry &= vbCrLf & " 	  ORDER BY FNSeq"
            _Qry &= vbCrLf & " 	), D.FDShipDateOrginal) AS FDShipDateOrginalO"

            _Qry &= vbCrLf & "  , D.FDShipDate"
            _Qry &= vbCrLf & "  , D.FDShipDate AS FDShipDateTo"
            _Qry &= vbCrLf & "  , D.FDShipDateOrginal"
            _Qry &= vbCrLf & "  , D.FDShipDateOrginal AS FDShipDateOrginalTo"

            _Qry &= vbCrLf & " ,ISNULL((SELECT TOP 1 FNSeq"
            _Qry &= vbCrLf & "    FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TMERTOrderGACDateCfm WITH(NOLOCK)"
            _Qry &= vbCrLf & "    WHERE FTOrderNo = O.FTOrderNo And FTSubOrderNo = D.FTSubOrderNo and FTNikePOLineItem = D.FTNikePOLineItem"
            _Qry &= vbCrLf & "   ORDER BY FNSeq DESC"
            _Qry &= vbCrLf & " ), null) AS FNTotalRevised"
            _Qry &= vbCrLf & " ,ISNULL(D.FNGrandQuantity,0) AS FNQuantity"
            _Qry &= vbCrLf & " ,0 AS FNQuantityExtra "
            _Qry &= vbCrLf & " ,ISNULL(D.FNGarmentQtyTest,0) AS FNGarmentQtyTest "
            _Qry &= vbCrLf & "  ,ISNULL(D.FNGrandQuantity,0) AS FNGrandQuantity "
            _Qry &= vbCrLf & "  , CASe WHEN ISNULL(D.FTPORef,'') ='' THEN   ISNULL(O.FTPORef,'')  ELSE  ISNULL(D.FTPORef,'')   END AS FTPORef"


            '_Qry &= vbCrLf & "   , ISNULL((  "
            '_Qry &= vbCrLf & "   Select  STUFF((Select  ',' + FTNikePOLineItem  "
            '_Qry &= vbCrLf & "     From(SELECT      Distinct FTNikePOLineItem "
            '_Qry &= vbCrLf & "  	FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_BreakDown With(NOLOCK)  "
            '_Qry &= vbCrLf & "    WHERE(FTOrderNo = O.FTOrderNo)"
            '_Qry &= vbCrLf & "    ) AS T "
            '_Qry &= vbCrLf & "  	For Xml PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)'),1,1,'') "
            '_Qry &= vbCrLf & "  ),'')    AS FTNikePOLineItem "
            _Qry &= vbCrLf & "  ,isnull(D.FTNikePOLineItem,'')  AS FTNikePOLineItem "
            '_Qry &= vbCrLf & " ,ISNULL((SELECT TOP 1 FDCfmShipDate"
            '_Qry &= vbCrLf & "    FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TMERTOrderGACDateCfm WITH(NOLOCK)"
            '_Qry &= vbCrLf & "    WHERE FTOrderNo = O.FTOrderNo And FTSubOrderNo = D.FTSubOrderNo and FTNikePOLineItem = D.FTNikePOLineItem"
            '_Qry &= vbCrLf & " 	  ORDER BY FNSeq desc"
            '_Qry &= vbCrLf & " 	), '') AS FDCfmShipDate"

            '_Qry &= vbCrLf & " ,ISNULL((SELECT TOP 1 FDORShipDate"
            '_Qry &= vbCrLf & "    FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TMERTOrderGACDateCfm WITH(NOLOCK)"
            '_Qry &= vbCrLf & "    WHERE FTOrderNo = O.FTOrderNo And FTSubOrderNo = D.FTSubOrderNo and FTNikePOLineItem = D.FTNikePOLineItem "
            '_Qry &= vbCrLf & " 	  ORDER BY FNSeq desc"
            '_Qry &= vbCrLf & " 	), '') AS FDORShipDate"

            '_Qry &= vbCrLf & " ,ISNULL((SELECT TOP 1 FTReasonDesc"
            '_Qry &= vbCrLf & "    FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TMERTOrderGACDateCfm WITH(NOLOCK)"
            '_Qry &= vbCrLf & "    WHERE FTOrderNo = O.FTOrderNo And FTSubOrderNo = D.FTSubOrderNo and FTNikePOLineItem = D.FTNikePOLineItem"
            '_Qry &= vbCrLf & " 	  ORDER BY FNSeq desc"
            '_Qry &= vbCrLf & " 	), '') AS FTReasonDesc"

            _Qry &= vbCrLf & " ,Tz.FTReasonDesc ,Tz.FDORShipDate ,Tz.FDCfmShipDate   ,Tz.FTInsUser, convert(varchar(10) , convert(date ,  Tz.FDInsDate) ,103) as FDInsDate  , Tz.FTInsTime    "
            _Qry &= vbCrLf & " , Tz.FTItemNo"
            _Qry &= vbCrLf & "   , Tz.FNQuantity as FNItemQuantity, Tz.FNHSysSuplId_Hide ,  Tz.FNHSysSuplId ,  Tz.FDDaliveryDate, Tz.FTRevisedReson "
            _Qry &= vbCrLf & "  FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder As O With(NOLOCK)  INNER JOIN"

            '_Qry &= vbCrLf & " ( Select   FTOrderNo , FTSubOrderNo ,  FDShipDate , (Select Top 1 FDShipDateOrginal From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub WITH(NOLOCK) where FTSubOrderNo = T.FTSubOrderNo) as  FDShipDateOrginal ,FTPORef  From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_Divert as T  With(NOLOCK) "
            '_Qry &= vbCrLf & " UNION "
            '_Qry &= vbCrLf & " Select   FTOrderNo , FTSubOrderNo ,  FDShipDate ,    FDShipDateOrginal ,FTPORef  From   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub  With(NOLOCK)"
            '_Qry &= vbCrLf & " ) as OSub On O.FTOrderNo = OSub.FTOrderNo INNER JOIN  "





            _Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp As Cmp With(NOLOCK)  On O.FNHSysCmpId = Cmp.FNHSysCmpId"


            _Qry &= vbCrLf & " 	  LEFT OUTER JOIN ("
            _Qry &= vbCrLf & " 	Select FTOrderNo , FTSubOrderNo , FDShipDate , FDShipDate as FDShipDateOrginal , FTPOref , FTNikePOLineItem , sum(FNGarmentQtyTest) as FNGarmentQtyTest ,sum(FNGrandQuantity) as  FNGrandQuantity , 0 as FNQuantityExtra ,FNHSysContinentId , FNHSysCountryId ,FNHSysShipModeId ,FNHSysProvinceId "
            _Qry &= vbCrLf & "  From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.V_OrderSub_BreakDown_ShipDestination   where  FNGrandQuantity > 0 "
            _Qry &= vbCrLf & "    group by FTOrderNo , FTSubOrderNo , FDShipDate , FDShipDate , FTPOref , FTNikePOLineItem , FNHSysContinentId , FNHSysCountryId ,FNHSysShipModeId ,FNHSysProvinceId"
            _Qry &= vbCrLf & "   ) AS D ON O.FTOrderNo = D.FTOrderNo AND D.FTSubOrderNo = D.FTSubOrderNo"

            _Qry &= vbCrLf & "  LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCustomer As Cus On O.FNHSysCustId = Cus.FNHSysCustId"
            _Qry &= vbCrLf & "  LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle As ST On O.FNHSysStyleId = ST.FNHSysStyleId"
            _Qry &= vbCrLf & "  LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMContinent As CT On D.FNHSysContinentId = CT.FNHSysContinentId"
            _Qry &= vbCrLf & "  LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCountry As CM On D.FNHSysCountryId = CM.FNHSysCountryId"
            _Qry &= vbCrLf & "  LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMShipMode As SM On D.FNHSysShipModeId = SM.FNHSysShipModeId"
            _Qry &= vbCrLf & "  LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCMMProvince As PT On D.FNHSysProvinceId = PT.FNHSysProvinceId"

            _Qry &= vbCrLf & "  outer apply (  SELECT TOP 1 Tz.FTReasonDesc ,Tz.FDORShipDate ,Tz.FDCfmShipDate  ,Tz.FTInsUser ,  Tz.FDInsDate , Tz.FTInsTime ,Tz.FTItemNo"
            _Qry &= vbCrLf & "   , Tz.FNQuantity, Tz.FNHSysSuplId as FNHSysSuplId_Hide , sp.FTSuplCode  as FNHSysSuplId , Tz.FDDaliveryDate, Tz.FTRevisedReson "
            _Qry &= vbCrLf & "    FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TMERTOrderGACDateCfm as Tz  "
            _Qry &= vbCrLf & "   LEFT OUTER JOIN " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "..TCNMSupplier as sp on Tz.FNHSysSuplId = sp.FNHSysSuplId   "
            _Qry &= vbCrLf & "    WHERE Tz.FTOrderNo = O.FTOrderNo And Tz.FTSubOrderNo = D.FTSubOrderNo And Tz.FTNikePOLineItem = D.FTNikePOLineItem"
            _Qry &= vbCrLf & " 	  ORDER BY Tz.FNSeq desc"
            _Qry &= vbCrLf & " ) AS Tz   "

            'FTInsUser, FDInsDate, FTInsTime

            _Qry &= vbCrLf & "  WHERE  O.FTOrderNo <> '' AND (O.FNJobState = 0 OR O.FNJobState = 1 OR O.FNJobState IS NULL)  "
            _Qry &= vbCrLf & "  AND   Not(O.FNOrderType IN (2,3,4,15) ) "

            'If Not (HI.ST.SysInfo.Admin) Then

            '    _Qry &= vbCrLf & " AND O.FNHSysMerTeamId IN ("
            '    _Qry &= vbCrLf & " SELECT FNHSysMerTeamId "
            '    _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEUserLogin WITH(NOLOCK)"
            '    _Qry &= vbCrLf & " WHERE FTUserName='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
            '    _Qry &= vbCrLf & " )"

            'End If

            If FNHSysCmpId.Text <> "" Then
                _Qry &= vbCrLf & "  AND  O.FNHSysCmpId =" & Integer.Parse(Val(FNHSysCmpId.Properties.Tag.ToString)) & ""
            End If

            If FNHSysBuyId.Text <> "" Then
                _Qry &= vbCrLf & "  AND  O.FNHSysBuyId =" & Integer.Parse(Val(FNHSysBuyId.Properties.Tag.ToString)) & ""
            End If

            If FNHSysStyleId.Text <> "" Then
                _Qry &= vbCrLf & "  AND  O.FNHSysStyleId =" & Integer.Parse(Val(FNHSysStyleId.Properties.Tag.ToString)) & ""
            End If

            If FTStartShipment.Text <> "" Then
                _Qry &= vbCrLf & "  AND D.FDShipDate>='" & HI.UL.ULDate.ConvertEnDB(FTStartShipment.Text) & "' "
            End If

            If FTEndShipment.Text <> "" Then
                _Qry &= vbCrLf & "  AND D.FDShipDate<='" & HI.UL.ULDate.ConvertEnDB(FTEndShipment.Text) & "' "
            End If

            If FTStartOrderDate.Text <> "" Then
                _Qry &= vbCrLf & "  AND O.FDOrderDate>='" & HI.UL.ULDate.ConvertEnDB(FTStartOrderDate.Text) & "' "
            End If

            If FTEndOrderDate.Text <> "" Then
                _Qry &= vbCrLf & "  AND O.FDOrderDate<='" & HI.UL.ULDate.ConvertEnDB(FTEndOrderDate.Text) & "' "
            End If

            If FNHSysCustId.Text <> "" Then
                _Qry &= vbCrLf & "  AND  O.FNHSysCustId =" & Integer.Parse(Val(FNHSysCustId.Properties.Tag.ToString)) & ""
            End If

            _Qry &= vbCrLf & "   ) AS A "
            _Qry &= vbCrLf & "  ORDER BY FTOrderNo "


            dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

            Me.ogc.DataSource = dt.Copy

            '  Call LoadDataHistory()

            _Spls.Close()
        Catch ex As Exception
            _Spls.Close()
        End Try

        dt.Dispose()

    End Sub

    Private Sub LoadDataHistory()

        Dim _Qry As String = ""
        Dim dt As New DataTable

        Try

            _Qry = " Select FTCmpCode,FNHSysCmpId"
            _Qry &= vbCrLf & "  ,FTCmpName"
            _Qry &= vbCrLf & "  ,FTOrderNo"
            _Qry &= vbCrLf & "  ,FTSubOrderNo"
            _Qry &= vbCrLf & "  ,FDOrderDate"
            _Qry &= vbCrLf & "  ,FTCustCode"
            _Qry &= vbCrLf & "  ,FTCustName"
            _Qry &= vbCrLf & "  ,FTStyleCode"
            _Qry &= vbCrLf & "  ,FTStyleName"
            _Qry &= vbCrLf & "  ,FNQuantity"
            _Qry &= vbCrLf & "  ,FNQuantityExtra "
            _Qry &= vbCrLf & "  ,FNGarmentQtyTest "
            _Qry &= vbCrLf & "  ,FNGrandQuantity "
            _Qry &= vbCrLf & "  ,CASE WHEN ISDATE(FDOriginalShipDate) = 1 THEN Convert(datetime,FDOriginalShipDate) ELSE NULL END AS FDOriginalShipDate"
            _Qry &= vbCrLf & "  ,CASE WHEN ISDATE(FDShipDateOrginalO) = 1 THEN Convert(datetime,FDShipDateOrginalO) ELSE NULL END AS FDShipDateOrginalO"
            _Qry &= vbCrLf & "  ,FNSeq"
            _Qry &= vbCrLf & "  ,CASE WHEN ISDATE(FDShipDate) = 1 THEN Convert(datetime,FDShipDate) ELSE NULL END AS FDShipDate"
            _Qry &= vbCrLf & "  ,CASE WHEN ISDATE(FDShipDateTo) = 1 THEN Convert(datetime,FDShipDateTo) ELSE NULL END AS FDShipDateTo"
            _Qry &= vbCrLf & "  ,CASE WHEN ISDATE(FDShipDateOrginal) = 1 THEN Convert(datetime,FDShipDateOrginal) ELSE NULL END AS FDShipDateOrginal"
            _Qry &= vbCrLf & "  ,CASE WHEN ISDATE(FDShipDateOrginalTo) = 1 THEN Convert(datetime,FDShipDateOrginalTo) ELSE NULL END AS FDShipDateOrginalTo"

            _Qry &= vbCrLf & "  FROM (SELECT Cmp.FTCmpCode,O.FNHSysCmpId"
            _Qry &= vbCrLf & " , Cus.FTCustCode "
            _Qry &= vbCrLf & " , ST.FTStyleCode "

            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Qry &= vbCrLf & "  , Cmp.FTCmpNameTH AS FTCmpName "
                _Qry &= vbCrLf & " , Cus.FTCustNameTH AS FTCustName"
                _Qry &= vbCrLf & " , ST.FTStyleNameTH AS FTStyleName"
            Else
                _Qry &= vbCrLf & " , Cmp.FTCmpNameEN AS FTCmpName "
                _Qry &= vbCrLf & " , Cus.FTCustNameEN AS FTCustName"
                _Qry &= vbCrLf & " , ST.FTStyleNameEN AS FTStyleName"
            End If

            _Qry &= vbCrLf & " , O.FTOrderNo"
            _Qry &= vbCrLf & " , OSub.FTSubOrderNo"
            _Qry &= vbCrLf & " , O.FDOrderDate"
            _Qry &= vbCrLf & " "
            _Qry &= vbCrLf & " ,ISNULL((SELECT TOP 1 FDShipDate"
            _Qry &= vbCrLf & "    FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderGACDateCfm WITH(NOLOCK)"
            _Qry &= vbCrLf & "    WHERE FTOrderNo = O.FTOrderNo And FTSubOrderNo = OSub.FTSubOrderNo"
            _Qry &= vbCrLf & " 	  ORDER BY FNSeq"
            _Qry &= vbCrLf & " 	), OSub.FDShipDate) AS FDOriginalShipDate"
            _Qry &= vbCrLf & " ,  OSub.FDShipDateOrginal  AS FDShipDateOrginalO"
            _Qry &= vbCrLf & "  ,GCM.FDShipDate AS FDShipDate"
            _Qry &= vbCrLf & "  ,GCM.FDShipDate AS FDShipDateTo"
            _Qry &= vbCrLf & "  ,GCM.FDOShipDate AS FDShipDateOrginal"
            _Qry &= vbCrLf & "  ,GCM.FDOShipDateTo AS FDShipDateOrginalTo"
            _Qry &= vbCrLf & " ,GCM.FNSeq"
            _Qry &= vbCrLf & " ,ISNULL(D.FNQuantity,0) AS FNQuantity"
            _Qry &= vbCrLf & " ,ISNULL(D.FNQuantityExtra,0) AS FNQuantityExtra "
            _Qry &= vbCrLf & " ,ISNULL(D.FNGarmentQtyTest,0) AS FNGarmentQtyTest "
            _Qry &= vbCrLf & "  ,ISNULL(D.FNGrandQuantity,0) AS FNGrandQuantity "



            _Qry &= vbCrLf & "  FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH(NOLOCK)  INNER JOIN"
            _Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub AS OSub  WITH(NOLOCK) ON O.FTOrderNo = OSub.FTOrderNo INNER JOIN"
            _Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp AS Cmp WITH(NOLOCK)  ON O.FNHSysCmpId = Cmp.FNHSysCmpId"
            _Qry &= vbCrLf & " 	  LEFT OUTER JOIN ("
            _Qry &= vbCrLf & " 	  SELECT FTOrderNo, FTSubOrderNo,FTNikePOLineItem, SUM(FNQuantityExtra) AS FNQuantityExtra, SUM(FNGrandQuantity) AS FNGrandQuantity, SUM(FNGarmentQtyTest) AS FNGarmentQtyTest, SUM(FNQuantity) AS FNQuantity"
            _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_BreakDown WITH(NOLOCK)"
            _Qry &= vbCrLf & "  GROUP BY FTOrderNo, FTSubOrderNo ,FTNikePOLineItem"
            _Qry &= vbCrLf & "   ) AS D ON O.FTOrderNo = D.FTOrderNo AND OSub.FTSubOrderNo = D.FTSubOrderNo"
            _Qry &= vbCrLf & "  LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCustomer AS Cus ON O.FNHSysCustId = Cus.FNHSysCustId"
            _Qry &= vbCrLf & "  LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS ST ON O.FNHSysStyleId = ST.FNHSysStyleId"
            _Qry &= vbCrLf & "  INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderGACDate GCM WITH(NOLOCK) ON OSub.FTOrderNo = GCM.FTOrderNo AND OSub.FTSubOrderNo = GCM.FTSubOrderNo "

            _Qry &= vbCrLf & "  WHERE  O.FTOrderNo <> ''  "
            _Qry &= vbCrLf & "  AND   Not(O.FNOrderType IN (2,3,4,15) ) "

            If Not (HI.ST.SysInfo.Admin) Then
                _Qry &= vbCrLf & " AND O.FNHSysMerTeamId IN ("
                _Qry &= vbCrLf & " SELECT FNHSysMerTeamId "
                _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEUserLogin WITH(NOLOCK)"
                _Qry &= vbCrLf & " WHERE FTUserName='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                _Qry &= vbCrLf & " )"
            End If

            If FNHSysCmpId.Text <> "" Then
                _Qry &= vbCrLf & "  AND  O.FNHSysCmpId =" & Integer.Parse(Val(FNHSysCmpId.Properties.Tag.ToString)) & ""
            End If

            If FNHSysBuyId.Text <> "" Then
                _Qry &= vbCrLf & "  AND  O.FNHSysBuyId =" & Integer.Parse(Val(FNHSysBuyId.Properties.Tag.ToString)) & ""
            End If

            If FNHSysStyleId.Text <> "" Then
                _Qry &= vbCrLf & "  AND  O.FNHSysStyleId =" & Integer.Parse(Val(FNHSysStyleId.Properties.Tag.ToString)) & ""
            End If

            If FTStartShipment.Text <> "" Then
                _Qry &= vbCrLf & "  AND OSub.FDShipDate>='" & HI.UL.ULDate.ConvertEnDB(FTStartShipment.Text) & "' "
            End If

            If FTEndShipment.Text <> "" Then
                _Qry &= vbCrLf & "  AND OSub.FDShipDate<='" & HI.UL.ULDate.ConvertEnDB(FTEndShipment.Text) & "' "
            End If

            If FTStartOrderDate.Text <> "" Then
                _Qry &= vbCrLf & "  AND O.FDOrderDate>='" & HI.UL.ULDate.ConvertEnDB(FTStartOrderDate.Text) & "' "
            End If

            If FTEndOrderDate.Text <> "" Then
                _Qry &= vbCrLf & "  AND O.FDOrderDate<='" & HI.UL.ULDate.ConvertEnDB(FTEndOrderDate.Text) & "' "
            End If

            If FNHSysCustId.Text <> "" Then
                _Qry &= vbCrLf & "  AND  O.FNHSysCustId =" & Integer.Parse(Val(FNHSysCustId.Properties.Tag.ToString)) & ""
            End If

            _Qry &= vbCrLf & "   ) AS A "
            _Qry &= vbCrLf & "  ORDER BY FTOrderNo "


            dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

            Me.ogcdetailhistory.DataSource = dt.Copy

        Catch ex As Exception
        End Try

        dt.Dispose()

    End Sub

    Private Function VerifyData() As Boolean
        Dim _Pass As Boolean = False

        If Me.FNHSysBuyId.Text <> "" And FNHSysBuyId.Properties.Tag.ToString <> "" Then
            _Pass = True
        End If

        If Me.FNHSysStyleId.Text <> "" And FNHSysStyleId.Properties.Tag.ToString <> "" Then
            _Pass = True
        End If

        If Me.FNHSysCmpId.Text <> "" And FNHSysCmpId.Properties.Tag.ToString <> "" Then
            _Pass = True
        End If

        If Me.FNHSysCustId.Text <> "" And FNHSysCustId.Properties.Tag.ToString <> "" Then
            _Pass = True
        End If

        If Me.FTStartShipment.Text <> "" Then
            _Pass = True
        End If

        If Me.FTEndShipment.Text <> "" Then
            _Pass = True
        End If

        If Me.FTStartOrderDate.Text <> "" Then
            _Pass = True
        End If

        If Me.FTEndOrderDate.Text <> "" Then
            _Pass = True
        End If

        If Not (_Pass) Then
            HI.MG.ShowMsg.mInfo("กรุณาทำการเลือกเงื่อไข อย่างน้อย 1 รายการ !!!", 1406170001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
        End If

        Return _Pass
    End Function

    Private Function SaveGacDate() As Boolean

        Dim _Spls As New HI.TL.SplashScreen("Saving....Please Wait.")
        Dim _dt As DataTable
        With CType(Me.ogc.DataSource, DataTable)
            .AcceptChanges()
            _dt = .Copy()
        End With
        Dim _Qry As String = ""

        HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_MERCHAN)
        HI.Conn.SQLConn.SqlConnectionOpen()
        HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
        HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

        Try

            For Each R As DataRow In _dt.Select("FTStateChange='1' OR FTStateChangeO='1'")

                _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TMERTOrderGACDateCfm"
                _Qry &= vbCrLf & "(FTInsUser, FDInsDate, FTInsTime, FTOrderNo, FTSubOrderNo, FNSeq, FDShipDate, FDShipDateTo,FDOShipDate,FDOShipDateTo,FDCfmShipDate,FDORShipDate,FTReasonDesc ,FTNikePOLineItem "
                _Qry &= vbCrLf & "   , FTItemNo, FNQuantity, FNHSysSuplId, FDDaliveryDate, FTRevisedReson)"
                _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ""
                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "'"
                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "'"
                _Qry &= vbCrLf & ",ISNULL(("
                _Qry &= vbCrLf & " SELECT TOP 1 FNSeq "
                _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TMERTOrderGACDateCfm"
                _Qry &= vbCrLf & " WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "'"
                _Qry &= vbCrLf & " AND FTSubOrderNo='" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "'"
                _Qry &= vbCrLf & " ORDER BY FNSeq DESC "
                _Qry &= vbCrLf & "),0) + 1 "
                _Qry &= vbCrLf & ",'" & HI.UL.ULDate.ConvertEnDB(R!FDShipDate.ToString) & "'"
                _Qry &= vbCrLf & ",'" & HI.UL.ULDate.ConvertEnDB(R!FDShipDateTo.ToString) & "'"
                _Qry &= vbCrLf & ",'" & HI.UL.ULDate.ConvertEnDB(R!FDShipDateOrginal.ToString) & "'"
                _Qry &= vbCrLf & ",'" & HI.UL.ULDate.ConvertEnDB(R!FDShipDateOrginalTo.ToString) & "'"
                _Qry &= vbCrLf & ",'" & HI.UL.ULDate.ConvertEnDB(R!FDCfmShipDate.ToString) & "'"
                _Qry &= vbCrLf & ",'" & HI.UL.ULDate.ConvertEnDB(R!FDORShipDate.ToString) & "'"
                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTReasonDesc.ToString) & "'"
                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTNikePOLineItem.ToString) & "'"

                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTItemNo.ToString) & "'"
                _Qry &= vbCrLf & ",'" & Val(R!FNItemQuantity.ToString) & "'"
                _Qry &= vbCrLf & ",'" & Val(R!FNHSysSuplId_Hide.ToString) & "'"
                _Qry &= vbCrLf & ",'" & HI.UL.ULDate.ConvertEnDB(R!FDDaliveryDate.ToString) & "'"
                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTRevisedReson.ToString) & "'"

                If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    _Spls.Close()
                    Return False
                End If


                '_Qry &= vbCrLf & " ,FDShipDateOrginal='" & HI.UL.ULDate.ConvertEnDB(R!FDShipDateOrginalTo.ToString) & "'"

                '_Qry &= vbCrLf & " WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "'"
                '_Qry &= vbCrLf & " AND   FTSubOrderNo='" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "'"
                '_Qry &= vbCrLf & " and  FTNikePOLineItem='" & HI.UL.ULF.rpQuoted(R!FTNikePOLineItem.ToString) & "'"
                '_Qry = "UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TEXPTPackPlan"
                '_Qry &= vbCrLf & "SET FDShipDate='" & HI.UL.ULDate.ConvertEnDB(R!FDCfmShipDate.ToString) & "'"

                _Qry = " UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub"
                _Qry &= vbCrLf & " SET FDShipDate='" & HI.UL.ULDate.ConvertEnDB(R!FDORShipDate.ToString) & "'"
                _Qry &= vbCrLf & " WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "'"
                _Qry &= vbCrLf & " AND   FTSubOrderNo='" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "'"
                _Qry &= vbCrLf & ""
                _Qry &= vbCrLf & " UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_Divert"
                _Qry &= vbCrLf & " SET FDShipDate='" & HI.UL.ULDate.ConvertEnDB(R!FDORShipDate.ToString) & "'"
                _Qry &= vbCrLf & " WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "'"
                _Qry &= vbCrLf & " AND   FTSubOrderNo='" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "'"


                HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)



                _Qry = "UPDATE A  set A.FDShipDate ='" & HI.UL.ULDate.ConvertEnDB(R!FDCfmShipDate.ToString) & "'"
                _Qry &= vbCrLf & "FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TEXPTPackPlan AS A"
                _Qry &= vbCrLf & "LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "]..TEXPTPackPlan_D AS B WITH(NOLOCK) ON A.FTPckPlanNo = B.FTPckPlanNo and A.FTPORef = B.FTPORef and A.FTPORefNo = A.FTPORefNo"
                _Qry &= vbCrLf & " where A.FTPORef='" & HI.UL.ULF.rpQuoted(R!FTPORef.ToString) & "'"
                _Qry &= vbCrLf & "and convert(nvarchar(5) ,convert(int,  B.FTPOLineNo)) ='" & HI.UL.ULF.rpQuoted(R!FTNikePOLineItem.ToString) & "'"
                If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    'HI.Conn.SQLConn.Tran.Rollback()
                    'HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    'HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    _Spls.Close()
                    'Return False
                End If

            Next

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            _Spls.Close()
            Return True
        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            _Spls.Close()
            Return False
        End Try

        Return True
    End Function


    Private Function UpdateCfmDate() As Boolean

        Dim _Spls As New HI.TL.SplashScreen("Saving....Please Wait.")
        Dim _dt As DataTable
        With CType(Me.ogc.DataSource, DataTable)
            .AcceptChanges()
            _dt = .Copy()
        End With
        Dim _Qry As String = ""

        HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_MERCHAN)
        HI.Conn.SQLConn.SqlConnectionOpen()
        HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
        HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

        Try

            For Each R As DataRow In _dt.Select("FTStateChange='1' OR FTStateChangeO='1'")
                _Qry = "UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderGACDate"
                _Qry &= vbCrLf & " SET FDCfmShipDate='" & HI.UL.ULDate.ConvertEnDB(R!FDCfmShipDate.ToString) & "'"
                _Qry &= vbCrLf & ",FDORShipDate='" & HI.UL.ULDate.ConvertEnDB(R!FDORShipDate.ToString) & "'"
                _Qry &= vbCrLf & ",FTReasonDesc='" & HI.UL.ULF.rpQuoted(R!FTReasonDesc.ToString) & "'"
                _Qry &= vbCrLf & "where  FTOrderNo ='" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "'"
                _Qry &= vbCrLf & "and FTSubOrderNo='" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "'"
                _Qry &= vbCrLf & "and FNSeq = ISNULL(("
                _Qry &= vbCrLf & " SELECT TOP 1 FNSeq "
                _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderGACDate"
                _Qry &= vbCrLf & " WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "'"
                _Qry &= vbCrLf & " AND FTSubOrderNo='" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "'"
                _Qry &= vbCrLf & " ORDER BY FNSeq DESC "
                _Qry &= vbCrLf & "),0)  "

                If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                    _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderGACDate"
                    _Qry &= vbCrLf & "(FTInsUser, FDInsDate, FTInsTime, FTOrderNo, FTSubOrderNo, FNSeq, FDShipDate, FDShipDateTo,FDOShipDate,FDOShipDateTo,FDCfmShipDate,FDORShipDate,FTReasonDesc)"
                    _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                    _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ""
                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "'"
                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "'"
                    _Qry &= vbCrLf & ",ISNULL(("
                    _Qry &= vbCrLf & " SELECT TOP 1 FNSeq "
                    _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderGACDate"
                    _Qry &= vbCrLf & " WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "'"
                    _Qry &= vbCrLf & " AND FTSubOrderNo='" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "'"
                    _Qry &= vbCrLf & " ORDER BY FNSeq DESC "
                    _Qry &= vbCrLf & "),0) + 1 "
                    _Qry &= vbCrLf & ",''"
                    _Qry &= vbCrLf & ",''"
                    _Qry &= vbCrLf & ",''"
                    _Qry &= vbCrLf & ",''"
                    _Qry &= vbCrLf & ",'" & HI.UL.ULDate.ConvertEnDB(R!FDCfmShipDate.ToString) & "'"
                    _Qry &= vbCrLf & ",'" & HI.UL.ULDate.ConvertEnDB(R!FDORShipDate.ToString) & "'"
                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTReasonDesc.ToString) & "'"

                    If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        _Spls.Close()
                        Return False
                    End If


                End If

                '_Qry = " UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub"
                '_Qry &= vbCrLf & " SET FDShipDate='" & HI.UL.ULDate.ConvertEnDB(R!FDShipDateTo.ToString) & "'"
                '_Qry &= vbCrLf & " ,FDShipDateOrginal='" & HI.UL.ULDate.ConvertEnDB(R!FDShipDateOrginalTo.ToString) & "'"

                '_Qry &= vbCrLf & " WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "'"
                '_Qry &= vbCrLf & " AND   FTSubOrderNo='" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "'"

                'If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                '    HI.Conn.SQLConn.Tran.Rollback()
                '    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                '    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                '    _Spls.Close()
                '    Return False
                'End If

            Next

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            _Spls.Close()
            Return True
        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            _Spls.Close()
            Return False
        End Try

        Return True
    End Function


    Private Sub SendMailToProductin()
        Dim _Spls As New HI.TL.SplashScreen("Sending Mail....Please Wait.")
        Dim _dt As DataTable
        With CType(Me.ogc.DataSource, DataTable)
            .AcceptChanges()
            _dt = .Copy()
        End With
        Dim _Qry As String = ""
        Dim dtcmp As New DataTable
        Dim dtmailto As DataTable
        dtcmp.Columns.Add("FNHSysCmpId", GetType(Integer))
        dtcmp.Columns.Add("FTCmpCode", GetType(String))
        Try
            For Each R As DataRow In _dt.Select("FTStateChange='1' OR FTStateChangeO='1'")
                If dtcmp.Select("").Length <= 0 Then

                    dtcmp.Rows.Add(Integer.Parse(Val(R!FNHSysCmpId.ToString)), R!FTCmpCode.ToString)

                End If
            Next

            For Each Rcmp As DataRow In dtcmp.Rows

                _Qry = " SELECT DISTINCT MM.FTUserName "
                _Qry &= Environment.NewLine & "FROM (SELECT A.FTUserName, A.FNHSysPermissionID"
                _Qry &= Environment.NewLine & "      FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SECURITY) & "]..TSEUserLoginPermission AS A (NOLOCK)"
                _Qry &= Environment.NewLine & "      WHERE A.FNHSysPermissionID > 0"
                _Qry &= Environment.NewLine & "            AND EXISTS (SELECT 'T'"
                _Qry &= Environment.NewLine & "                        FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SECURITY) & "]..TSEPermissionCmp AS LL (NOLOCK) INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMCmp AS NN (NOLOCK) ON LL.FNHSysCmpId = NN.FNHSysCmpId"
                _Qry &= Environment.NewLine & "                        WHERE LL.FNHSysCmpId = " & Integer.Parse(Val(Rcmp!FNHSysCmpId.ToString)) & " "
                _Qry &= Environment.NewLine & "  			                 AND A.FNHSysPermissionID = LL.FNHSysPermissionID"
                _Qry &= Environment.NewLine & " 		               )"
                _Qry &= Environment.NewLine & "      ) AS MM INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SECURITY) & "]..TSEUserLogin AS KK (NOLOCK) ON MM.FTUserName = KK.FTUserName"
                _Qry &= Environment.NewLine & "              LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMTeamGrp AS PP (NOLOCK) ON KK.FNHSysTeamGrpId = PP.FNHSysTeamGrpId"
                _Qry &= Environment.NewLine & "WHERE KK.FTStateActive = N'1'  AND (PP. FTStatePurchase='1' OR PP.FTStateProd='1' OR PP.FTStateQA='1' OR PP.FTStateQAFinal='1') "

                dtmailto = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_SECURITY)

                If dtmailto.Rows.Count > 0 Then
                    Dim tmpsubject As String = ""
                    Dim tmpmessage As String = ""
                    Dim _UserMailTo As String = ""

                    tmpsubject = "Update GAC Date For Factory " & Rcmp!FTCmpCode.ToString
                    tmpmessage = "Update GAC Date For Factory " & Rcmp!FTCmpCode.ToString

                    For Each R As DataRow In _dt.Select(" ( FTStateChange='1' AND FNHSysCmpId=" & Integer.Parse(Val(Rcmp!FNHSysCmpId.ToString)) & " ) OR (FTStateChangeO='1' AND FNHSysCmpId=" & Integer.Parse(Val(Rcmp!FNHSysCmpId.ToString)) & "  )  ")

                        tmpmessage &= vbCrLf & "Factory Order No : " & R!FTOrderNo.ToString & " Sub Order No : " & R!FTSubOrderNo.ToString
                        If R!FTStateChange.ToString = "1" Then
                            tmpmessage &= vbCrLf & "       Change Shipment From " & HI.UL.ULDate.ConvertEN(R!FDShipDate.ToString) & " To " & HI.UL.ULDate.ConvertEN(R!FDShipDateTo.ToString)
                        End If

                        If R!FTStateChangeO.ToString = "1" Then
                            tmpmessage &= vbCrLf & "       Change O GAC Date From " & HI.UL.ULDate.ConvertEN(R!FDShipDateOrginal.ToString) & " To " & HI.UL.ULDate.ConvertEN(R!FDShipDateOrginalTo.ToString)
                        End If

                    Next

                    For Each Rm As DataRow In dtmailto.Rows

                        _UserMailTo = Rm!FTUserName.ToString
                        HI.Mail.ClsSendMail.SendMail(HI.ST.UserInfo.UserName, _UserMailTo, tmpsubject, tmpmessage, -1, "")

                    Next

                End If

            Next
        Catch ex As Exception

        End Try
        _Spls.Close()
    End Sub
#End Region

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ocmload_Click(sender As Object, e As EventArgs) Handles ocmload.Click
        RemoveHandler ReposFDShipDateTo2.Leave, AddressOf HI.TL.HandlerControl.RepositoryItemDate_Leave
        RemoveHandler RepositoryItemFDDaliveryDate.Leave, AddressOf HI.TL.HandlerControl.RepositoryItemDate_Leave
        RemoveHandler ReposFDShipDateTo2.Click, AddressOf HI.TL.HandlerControl.RepositoryItemDate_GotFocus
        RemoveHandler RepositoryItemFDDaliveryDate.Click, AddressOf HI.TL.HandlerControl.RepositoryItemDate_GotFocus
        RemoveHandler ReposFDShipDate.Leave, AddressOf HI.TL.HandlerControl.RepositoryItemDate_Leave
        RemoveHandler ReposFDShipDate.Click, AddressOf HI.TL.HandlerControl.RepositoryItemDate_GotFocus

        If Me.VerifyData() Then
            Call LoadData()
        End If

    End Sub

    Private Sub ReposFDShipDateTo_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles ReposFDShipDateTo2.EditValueChanging, ReposFDShipDate.EditValueChanging, RepositoryItemFDDaliveryDate.EditValueChanging, RepositoryItemFDDaliveryDate.EditValueChanging, RepositoryItemFNQuantity.EditValueChanging, RepositoryItemButtonFNHSysSuplId.EditValueChanging
        Try

            With Me.ogv
                Select Case .FocusedColumn.FieldName.ToString.ToLower
                    Case "FDShipDateTo".ToLower
                        If HI.UL.ULDate.ConvertEnDB(e.NewValue.ToString) = "" & .GetFocusedRowCellValue("FDShipDateT").ToString Then
                            .SetFocusedRowCellValue("FTStateChange", "0")
                        Else
                            .SetFocusedRowCellValue("FTStateChange", "1")
                        End If
                    Case "FDShipDateOrginalTo".ToLower
                        If HI.UL.ULDate.ConvertEnDB(e.NewValue.ToString) = "" & .GetFocusedRowCellValue("FDShipDateOrginalT").ToString Then
                            .SetFocusedRowCellValue("FTStateChangeO", "0")
                        Else
                            .SetFocusedRowCellValue("FTStateChangeO", "1")
                        End If
                    Case "FDCfmShipDate".ToLower
                        .SetFocusedRowCellValue("FTStateChange", "1")
                    Case "FNHSysSuplId_Hide".ToLower
                        .SetFocusedRowCellValue("FTStateChange", "1")
                    Case "FDDaliveryDate".ToLower
                        .SetFocusedRowCellValue("FTStateChange", "1")
                    Case "FNItemQuantity".ToLower
                        .SetFocusedRowCellValue("FTStateChange", "1")
                    Case "FTItemNo".ToLower, "FNHSysSuplId".ToLower
                        .SetFocusedRowCellValue("FTStateChange", "1")
                    Case "FDORShipDate".ToLower
                        .SetFocusedRowCellValue("FTStateChangeO", "1")
                        .SetRowCellValue(.FocusedRowHandle, "FDCfmShipDate", e.NewValue)

                        'Dim selectedRowHandles As Int32() = .GetSelectedRows()
                        'Dim I As Integer
                        'For I = 0 To selectedRowHandles.Length - 1
                        '    Dim selectedRowHandle As Int32 = selectedRowHandles(I)
                        '    If (selectedRowHandle >= 0) Then
                        '        .SetRowCellValue(selectedRowHandle, "FDCfmShipDate", e.NewValue)
                        '    End If
                        'Next


                        For Ix As Integer = 0 To .RowCount Step 1
                            If .GetRowCellValue(Ix, "FTSelect").ToString = "1" Then
                                .SetRowCellValue(Ix, "FDORShipDate", e.NewValue)
                                .SetRowCellValue(Ix, "FTStateChangeO", "1")
                                .SetRowCellValue(Ix, "FDCfmShipDate", e.NewValue)
                                .SetRowCellValue(Ix, "FTSelect", "0")
                            End If
                        Next
                End Select
            End With
            With CType(Me.ogc.DataSource, DataTable)
                .AcceptChanges()
            End With
        Catch ex As Exception
        End Try

    End Sub

    Private Sub ogv_RowCellStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles ogv.RowCellStyle
        With Me.ogv
            Try

                Select Case True
                    Case (("" & .GetRowCellValue(e.RowHandle, "FTStateChange").ToString = "1") Or ("" & .GetRowCellValue(e.RowHandle, "FTStateChangeO").ToString = "1"))
                        Try
                            e.Appearance.BackColor = System.Drawing.Color.LemonChiffon
                            e.Appearance.ForeColor = System.Drawing.Color.Blue
                        Catch ex As Exception
                        End Try
                    Case ("" & .GetRowCellValue(e.RowHandle, "FTSelect").ToString = "1")
                        e.Appearance.BackColor = System.Drawing.Color.CornflowerBlue
                    Case ("" & .GetRowCellValue(e.RowHandle, "FTSelect").ToString = "0")
                        e.Appearance.BackColor = System.Drawing.Color.Transparent
                End Select

            Catch ex As Exception
            End Try
        End With
    End Sub

    Private Sub ReposFDShipDateTo_Spin(sender As Object, e As SpinEventArgs) Handles ReposFDShipDateTo.Spin
        e.Handled = True
    End Sub

    Private Sub ocmsave_Click(sender As Object, e As EventArgs) Handles ocmsave.Click
        If Not (Me.ogc.DataSource Is Nothing) Then
            Dim _dt As DataTable
            With CType(Me.ogc.DataSource, DataTable)
                .AcceptChanges()
                _dt = .Copy()
            End With

            If _dt.Select("FTStateChange='1' OR FTStateChangeO='1'").Length > 0 Then
                If HI.MG.ShowMsg.mConfirmProcess("คุณต้องการทำการบันทึกข้อมูล GAC Date ใช่หรือไม่ ?", 1513170001) Then

                    If Me.SaveGacDate() Then
                        '  Call SendMailToProductin()
                        HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                        Me.LoadData()
                    Else
                        HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                    End If

                End If
            Else
                HI.MG.ShowMsg.mInfo("ไม่พบข้อมูลที่มีการแก้ไข GAC Date กรุณาทำการตรวจสอบ !!!", 1513170002, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
            End If

        End If
    End Sub

    Private Sub ogvdetailhistory_RowCellStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles ogvdetailhistory.RowCellStyle
        With Me.ogvdetailhistory
            Try

                Select Case e.Column.FieldName.ToString.ToLower
                    Case "FDShipDateOrginal".ToLower, "FDShipDateOrginalTo".ToLower

                        If ("" & .GetRowCellValue(e.RowHandle, "FDShipDateOrginal").ToString <> "" & .GetRowCellValue(e.RowHandle, "FDShipDateOrginalTo").ToString) Then

                            Try
                                e.Appearance.BackColor = System.Drawing.Color.LightCoral
                                e.Appearance.ForeColor = System.Drawing.Color.Blue
                            Catch ex As Exception
                            End Try

                        End If

                    Case "FDShipDate".ToLower, "FDShipDateTo".ToLower

                        If ("" & .GetRowCellValue(e.RowHandle, "FDShipDate").ToString <> "" & .GetRowCellValue(e.RowHandle, "FDShipDateTo").ToString) Then

                            Try
                                e.Appearance.BackColor = System.Drawing.Color.LightCoral
                                e.Appearance.ForeColor = System.Drawing.Color.Blue
                            Catch ex As Exception
                            End Try

                        End If

                End Select

            Catch ex As Exception
            End Try
        End With
    End Sub

    Private Sub ocmsavedocument_Click(sender As Object, e As EventArgs) Handles ocmsavedocument.Click
        If Not (Me.ogc.DataSource Is Nothing) Then
            Dim _dt As DataTable
            With CType(Me.ogc.DataSource, DataTable)
                .AcceptChanges()
                _dt = .Copy()
            End With

            If _dt.Select("FTStateChange='1' OR FTStateChangeO='1'").Length > 0 Then
                If HI.MG.ShowMsg.mConfirmProcess("คุณต้องการทำการบันทึกข้อมูล GAC Date ใช่หรือไม่ ?", 1513170001) Then
                    If Me.SaveGacDate() Then
                        '      Call SendMailToProductin()
                        HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                        Me.LoadData()
                    Else
                        HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                    End If
                End If
            Else
                HI.MG.ShowMsg.mInfo("ไม่พบข้อมูลที่มีการแก้ไข GAC Date กรุณาทำการตรวจสอบ !!!", 1513170002, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
            End If

        End If
    End Sub

    'Private Sub ogv_RowClick(sender As Object, e As RowClickEventArgs) Handles ogv.RowClick
    '    Try
    '        With Me.ogv
    '            If .RowCount > 0 And .FocusedRowHandle > -1 Then
    '                If .GetRowCellValue(.FocusedRowHandle, "FTSelect") = "0" Then
    '                    .SetRowCellValue(.FocusedRowHandle, "FTSelect", "1")
    '                Else
    '                    .SetRowCellValue(.FocusedRowHandle, "FTSelect", "0")
    '                End If
    '            End If
    '        End With
    '    Catch ex As Exception

    '    End Try
    'End Sub

    Private Sub ogv_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles ogv.SelectionChanged
        Try
            With Me.ogv
                If .RowCount > 0 And .FocusedRowHandle > -1 Then
                    For Each i As Integer In .GetSelectedRows()
                        If .GetRowCellValue(i, "FTSelect") = "0" Then
                            .SetRowCellValue(i, "FTSelect", "1")
                        Else
                            .SetRowCellValue(i, "FTSelect", "0")
                        End If
                    Next


                End If
            End With
        Catch ex As Exception

        End Try
    End Sub

    Private Sub RepositoryItemFTItemNo_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles RepositoryItemFTItemNo.EditValueChanging
        Try
            With Me.ogv
                Select Case .FocusedColumn.FieldName.ToString.ToLower

                    Case "FTItemNo".ToLower, "FTRevisedReson".ToLower
                        .SetFocusedRowCellValue("FTStateChange", "1")
                    Case "FDORShipDate".ToLower
                        .SetFocusedRowCellValue("FTStateChangeO", "1")
                        .SetRowCellValue(.FocusedRowHandle, "FDCfmShipDate", e.NewValue)

                        For Ix As Integer = 0 To .RowCount Step 1
                            If .GetRowCellValue(Ix, "FTSelect").ToString = "1" Then
                                .SetRowCellValue(Ix, "FDORShipDate", e.NewValue)
                                .SetRowCellValue(Ix, "FTStateChangeO", "1")
                                .SetRowCellValue(Ix, "FDCfmShipDate", e.NewValue)
                                .SetRowCellValue(Ix, "FTSelect", "0")
                            End If
                        Next
                End Select
            End With
        Catch ex As Exception

        End Try
    End Sub



    'Private Sub ogv_CellValueChanged(sender As Object, e As CellValueChangedEventArgs) Handles ogv.CellValueChanged
    '    OnCellValueChanged(e)
    'End Sub

#Region "Multiselect"




#End Region


End Class
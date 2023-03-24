Imports System.ComponentModel
Imports System.Drawing
Imports System.Globalization
Imports System.IO
Imports System.Threading
Imports System.Windows.Forms
Imports DevExpress.Data
Imports DevExpress.Utils
Imports DevExpress.XtraCharts
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports DevExpress.XtraPrinting
Imports Microsoft.Office.Interop.Excel

Public Class wPartialShipment
    Private _ProcLoad As Boolean = False
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        Call InitGridInfo()
        Call InitGrid()


    End Sub

#Region "Initial Grid"

    Private Sub InitGrid()
        '------Start Add Summary Grid-------------
        Dim sFieldCount As String = ""
        Dim sFieldSum As String = "" ' "FNQuantity|FNQuantityExtra|FNGarmentQtyTest|FNGrandQuantity"
        Dim sFieldSumAmt As String = ""

        Dim sFieldGrpCount As String = ""
        Dim sFieldGrpSum As String = ""

        Dim sFieldGrpSumAmt As String = ""
        Dim sFieldCustomSum As String = ""
        Dim sFieldCustomGrpSum As String = ""

        With GridView1
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



        '------End Add Summary Grid-------------
    End Sub
#End Region

    Private Sub InitRepDate()
        Try
            With Me.RepositoryItemDateFDShipDate
                Dim _Culture As New CultureInfo("en-US", True)
                _Culture.DateTimeFormat.ShortDatePattern = "MM/dd/yyyy"
                _Culture.DateTimeFormat.ShortTimePattern = "HH:mm:ss"

                System.Threading.Thread.CurrentThread.CurrentCulture = _Culture
                System.Threading.Thread.CurrentThread.CurrentUICulture = _Culture

                Thread.CurrentThread.CurrentCulture = New CultureInfo("en-US", True)
                Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern = "MM/dd/yyyy"

                .DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                .DisplayFormat.FormatString = "d"
                .EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                .EditFormat.FormatString = "d"
                .EditMask = "d"
                .DisplayFormat.FormatString = ("d")
                .Mask.Culture = _Culture
                .Mask.UseMaskAsDisplayFormat = False
            End With


        Catch ex As Exception

        End Try
    End Sub

#Region "Procedure"

    Private Sub LoadData()
        Dim _Qry As String = ""
        Dim dt As New System.Data.DataTable

        Dim _Spls As New HI.TL.SplashScreen("Loading Data... Please Wait... ")

        Try

            _Qry = " Select '0' as FTSelect , FTCmpCode,FNHSysCmpId"
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
            _Qry &= vbCrLf & " , PT.FTProvinceCode "


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
            _Qry &= vbCrLf & " ,ISNULL((SELECT TOP 1 FDCfmShipDate"
            _Qry &= vbCrLf & "    FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TMERTOrderGACDateCfm WITH(NOLOCK)"
            _Qry &= vbCrLf & "    WHERE FTOrderNo = O.FTOrderNo And FTSubOrderNo = D.FTSubOrderNo and FTNikePOLineItem = D.FTNikePOLineItem"
            _Qry &= vbCrLf & " 	  ORDER BY FNSeq desc"
            _Qry &= vbCrLf & " 	), '') AS FDCfmShipDate"

            _Qry &= vbCrLf & " ,ISNULL((SELECT TOP 1 FDORShipDate"
            _Qry &= vbCrLf & "    FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TMERTOrderGACDateCfm WITH(NOLOCK)"
            _Qry &= vbCrLf & "    WHERE FTOrderNo = O.FTOrderNo And FTSubOrderNo = D.FTSubOrderNo and FTNikePOLineItem = D.FTNikePOLineItem "
            _Qry &= vbCrLf & " 	  ORDER BY FNSeq desc"
            _Qry &= vbCrLf & " 	), '') AS FDORShipDate"

            _Qry &= vbCrLf & " ,ISNULL((SELECT TOP 1 FTReasonDesc"
            _Qry &= vbCrLf & "    FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TMERTOrderGACDateCfm WITH(NOLOCK)"
            _Qry &= vbCrLf & "    WHERE FTOrderNo = O.FTOrderNo And FTSubOrderNo = D.FTSubOrderNo and FTNikePOLineItem = D.FTNikePOLineItem"
            _Qry &= vbCrLf & " 	  ORDER BY FNSeq desc"
            _Qry &= vbCrLf & " 	), '') AS FTReasonDesc"


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


            _Qry &= vbCrLf & "   ) AS A "
            _Qry &= vbCrLf & "  ORDER BY FTOrderNo "


            dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

            Me.ogdColorSizeBreakdown.DataSource = dt.Copy

            '  Call LoadDataHistory()

            _Spls.Close()
        Catch ex As Exception
            _Spls.Close()
        End Try

        dt.Dispose()

    End Sub

    Private Sub LoadDataHistory()

        Dim _Qry As String = ""
        Dim dt As New System.Data.DataTable

        Try


        Catch ex As Exception
        End Try

        dt.Dispose()

    End Sub

    Private Function VerifyData() As Boolean
        Dim _Pass As Boolean = False

        If Me.FTCustomerPO.Text <> "" Then
            _Pass = True
        End If

        If Not (_Pass) Then
            HI.MG.ShowMsg.mInfo("กรุณาทำการเลือกเงื่อไข อย่างน้อย 1 รายการ !!!", 1406170001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
        End If

        Return _Pass
    End Function


    Private Function DeleteGacDate() As Boolean
        Dim _Cmd As String = ""
        Dim _oDt As System.Data.DataTable
        Dim _Spls As New HI.TL.SplashScreen("Saving....Please Wait.")
        Try




        Catch ex As Exception

        End Try
    End Function

    Private Function SaveGacDate(Optional ByVal _update As Boolean = False) As Boolean

        Dim _Spls As New HI.TL.SplashScreen("Saving....Please Wait.")
        Dim _dt As System.Data.DataTable
        Dim _sRemark As String = ""
        Dim _RawQty As Double = 0 : Dim _FTMainmatCode As String = "" : Dim _FTRawMatCode As String = "" : Dim _ItemCode As String = "" : Dim _RcvDate As String = "" : Dim _FTStatePOCancel As String = ""
        Dim _Qry As String = ""

        HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PLANNING)
        HI.Conn.SQLConn.SqlConnectionOpen()
        HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
        HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction
        Try
            With CType(Me.ogc.DataSource, System.Data.DataTable)
                .AcceptChanges()
                Try
                    _Qry = "Delete FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TPPCTPrePartialShip_H "
                    _Qry &= vbCrLf & "where FTCustomerPO= '" & HI.UL.ULF.rpQuoted(Me.FTCustomerPO.Text) & "'"
                    _Qry &= vbCrLf & " and FTNikePOLineItem ='" & HI.UL.ULF.rpQuoted(Me.FTNikePOLineItem.Text) & "'"
                    If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    End If


                    For Each R As DataRow In .Rows
                        '_Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TPPCTPrePartialShip_H"
                        '_Qry &= vbCrLf & " ( FTInsUser, FDInsDate, FTInsTime, FTCustomerPO, FTNikePOLineItem, FDOGacDate, FTColorway, FNQuantity, FNHSysStyleId, FNHSysCmpId )  "
                        '_Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        '_Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                        '_Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ""
                        '_Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTPOref.ToString) & "'"
                        '_Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTNikePOLineItem.ToString) & "'"
                        '_Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "'"
                        '_Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "'"
                        '_Qry &= vbCrLf & ",'" & HI.UL.ULDate.ConvertEnDB(R!FDShipDate.ToString) & "'"
                        '_Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(.Columns(I).ColumnName.ToString) & "'"
                        '_Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTColorway.ToString) & "'"
                        '_Qry &= vbCrLf & "," & Integer.Parse(Val(R.Item(.Columns(I).ColumnName.ToString).ToString))
                        '_Qry &= vbCrLf & "," & Val(R!FNHSysStyleId.ToString) & ""
                        '_Qry &= vbCrLf & "," & Val(Me.FNHSysCmpId.Properties.Tag) & ""
                    Next



                Catch ex As Exception

                End Try

            End With




            With CType(Me.ogdColorSizeBreakdown.DataSource, System.Data.DataTable)
                .AcceptChanges()

                If Not (_update) Then
                    _Qry = "Delete FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TPPCTPrePartialShip "
                    _Qry &= vbCrLf & "where FTCustomerPO= '" & HI.UL.ULF.rpQuoted(Me.FTCustomerPO.Text) & "'"
                    _Qry &= vbCrLf & " and FTNikePOLineItem ='" & HI.UL.ULF.rpQuoted(Me.FTNikePOLineItem.Text) & "'"
                    If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                    End If
                End If

                Dim _Ind As Integer = 0
                For Each R As DataRow In .Select("FNSeq=1 or FNSeq=2 ")
                    _Ind += +1


                    ''---------
                    '_sRemark = R!FTRemark.ToString : _FTMainmatCode = R!FTMainMatCode.ToString : _FTRawMatCode = R!FTRawMatCode.ToString
                    '_ItemCode = R!FTItemCode.ToString : _RcvDate = R!FDRcvDate.ToString : _FTStatePOCancel = R!FTStatePOCancel
                    '_RawQty = Val(R!FNRawMatQty.ToString)

                    For I As Integer = .Columns.Count - 1 To 0 Step -1
                        Select Case .Columns(I).ColumnName.ToString.ToUpper
                            Case "FNRowId".ToUpper, "FTDescription".ToUpper, "FNTotal".ToUpper, "FNPercent".ToUpper, "FNSeq".ToUpper, "FTPOref".ToUpper, "FTNikePOLineItem".ToUpper, "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FDShipDate".ToUpper, "FTColorway".ToUpper, "FNHSysStyleId".ToUpper, "FTNewNikePOLineItem".ToUpper,
                                   "FTRemark".ToUpper, "FTMainMatCode".ToUpper, "FTColorway".ToUpper, "FTRawMatCode".ToUpper, "FTItemCode".ToUpper, "FDRcvDate".ToUpper, "FTStatePOCancel".ToUpper, "FNRawMatQty".ToUpper
                            Case Else

                                _Qry = "Update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TPPCTPrePartialShip"
                                _Qry &= vbCrLf & " SET  FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                                _Qry &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB & ""
                                _Qry &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & ""
                                _Qry &= vbCrLf & ", FDNewGacDate =convert(varchar(10), convert(date, '" & R!FDShipDate.ToString & "' ) ,111) " ' & HI.UL.ULDate.ConvertEnDB(R!FDShipDate.ToString) & "'"
                                _Qry &= vbCrLf & ",FNQuantity=" & Integer.Parse(Val(R.Item(.Columns(I).ColumnName.ToString).ToString))
                                _Qry &= vbCrLf & ", FTStateShort ='" & Me.FTStateShortShip.EditValue & "'"
                                _Qry &= vbCrLf & ", FTStatePartial ='" & Me.FTStatePartialShip.EditValue & "'"
                                _Qry &= vbCrLf & ",FTNewNikePOLineItem= '" & HI.UL.ULF.rpQuoted(R!FTNewNikePOLineItem.ToString) & "'"
                                _Qry &= vbCrLf & ",FTRemark= '" & HI.UL.ULF.rpQuoted(R!FTRemark.ToString) & "'"
                                _Qry &= vbCrLf & ",FTMainMatCode= '" & HI.UL.ULF.rpQuoted(R!FTMainMatCode.ToString) & "'"
                                _Qry &= vbCrLf & ",FTRawMatCode= '" & HI.UL.ULF.rpQuoted(R!FTRawMatCode.ToString) & "'"
                                _Qry &= vbCrLf & ",FTItemCode= '" & HI.UL.ULF.rpQuoted(R!FTItemCode.ToString) & "'"
                                _Qry &= vbCrLf & ", FDRcvDate ='" & HI.UL.ULDate.ConvertEnDB(R!FDRcvDate.ToString) & "'"
                                _Qry &= vbCrLf & ",FTStatePOCancel= '" & HI.UL.ULF.rpQuoted(R!FTStatePOCancel.ToString) & "'"
                                _Qry &= vbCrLf & ",FNRawMatQty=" & Double.Parse(Val(R!FNRawMatQty))

                                _Qry &= vbCrLf & "where FTCustomerPO= '" & HI.UL.ULF.rpQuoted(R!FTPOref.ToString) & "'"
                                _Qry &= vbCrLf & " and FTNikePOLineItem ='" & HI.UL.ULF.rpQuoted(R!FTNikePOLineItem.ToString) & "'"
                                _Qry &= vbCrLf & " and FTOrderNo='" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "'"
                                _Qry &= vbCrLf & " and FTSubOrderNo='" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "'"
                                _Qry &= vbCrLf & " and FTSizeBreakDown='" & HI.UL.ULF.rpQuoted(.Columns(I).ColumnName.ToString) & "'"
                                _Qry &= vbCrLf & " and FNSeq =" & Val(R!FNSeq.ToString)
                                _Qry &= vbCrLf & " and FTColorway='" & HI.UL.ULF.rpQuoted(R!FTColorway.ToString) & "'"
                                _Qry &= vbCrLf & " and FNRowId =" & Val(R!FNRowId.ToString)
                                If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                                    _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TPPCTPrePartialShip"
                                    _Qry &= vbCrLf & "( FTInsUser, FDInsDate, FTInsTime,  FTCustomerPO, FTNikePOLineItem, FTOrderNo, FTSubOrderNo, FDNewGacDate, FTSizeBreakDown, FTColorway, FNQuantity, FNHSysStyleId,FNHSysCmpId,FNSeq, FTStateShort, FTStatePartial,FNRowId,FTNewNikePOLineItem"
                                    _Qry &= vbCrLf & ",FTRemark,FTMainMatCode,FTRawMatCode,FTItemCode, FDRcvDate,FTStatePOCancel,FNRawMatQty )"
                                    _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                                    _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                                    _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ""
                                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTPOref.ToString) & "'"
                                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTNikePOLineItem.ToString) & "'"
                                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "'"

                                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "'"
                                    _Qry &= vbCrLf & ",convert(varchar(10), convert(date, '" & R!FDShipDate.ToString & "' ) ,111)  "
                                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(.Columns(I).ColumnName.ToString) & "'"
                                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTColorway.ToString) & "'"
                                    _Qry &= vbCrLf & "," & Integer.Parse(Val(R.Item(.Columns(I).ColumnName.ToString).ToString))
                                    _Qry &= vbCrLf & "," & Val(R!FNHSysStyleId.ToString) & ""
                                    _Qry &= vbCrLf & "," & Val(Me.FNHSysCmpId.Properties.Tag) & ""
                                    _Qry &= vbCrLf & "," & Val(R!FNSeq.ToString)
                                    _Qry &= vbCrLf & ",'" & Me.FTStateShortShip.EditValue & "'"
                                    _Qry &= vbCrLf & ", '" & Me.FTStatePartialShip.EditValue & "'"
                                    _Qry &= vbCrLf & "," & _Ind
                                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTNewNikePOLineItem.ToString) & "'"
                                    _Qry &= vbCrLf & ", '" & HI.UL.ULF.rpQuoted(R!FTRemark.ToString) & "'"
                                    _Qry &= vbCrLf & ", '" & HI.UL.ULF.rpQuoted(R!FTMainMatCode.ToString) & "'"
                                    _Qry &= vbCrLf & ",  '" & HI.UL.ULF.rpQuoted(R!FTRawMatCode.ToString) & "'"
                                    _Qry &= vbCrLf & ",  '" & HI.UL.ULF.rpQuoted(R!FTItemCode.ToString) & "'"
                                    _Qry &= vbCrLf & ",  '" & HI.UL.ULDate.ConvertEnDB(R!FDRcvDate.ToString) & "'"
                                    _Qry &= vbCrLf & ",  '" & HI.UL.ULF.rpQuoted(R!FTStatePOCancel.ToString) & "'"
                                    _Qry &= vbCrLf & "," & Double.Parse(Val(R!FNRawMatQty))

                                    If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                                        HI.Conn.SQLConn.Tran.Rollback()
                                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                                        _Spls.Close()
                                        Return False
                                    End If
                                End If


                        End Select

                    Next




                Next

            End With

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
        Dim _dt As System.Data.DataTable
        With CType(Me.ogdColorSizeBreakdown.DataSource, System.Data.DataTable)
            .AcceptChanges()
            _dt = .Copy()
        End With
        Dim _Qry As String = ""
        Dim dtcmp As New System.Data.DataTable
        Dim dtmailto As System.Data.DataTable
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

        If Me.FTCustomerPO.Text <> "" And Me.FTNikePOLineItem.Text <> "" And Me.FNHSysStyleId.Text <> "" And Me.FTColorway.Text <> "" Then
            'W_PRCbShowBrowseDataSubOrderInfo(Me.FTCustomerPO.Text)
            Call loadDataDeail()
        Else
            'loadDataDeail()

        End If

    End Sub



    Private Sub ReposFDShipDateTo_Spin(sender As Object, e As SpinEventArgs)
        e.Handled = True
    End Sub

    Private Sub ocmsave_Click(sender As Object, e As EventArgs) Handles ocmsave.Click
        If Not (Me.ogdColorSizeBreakdown.DataSource Is Nothing) Then
            Dim _dt As System.Data.DataTable
            With CType(Me.ogdColorSizeBreakdown.DataSource, System.Data.DataTable)
                .AcceptChanges()
                _dt = .Copy()
            End With
            If Me.FTStateFactoryApp.Checked Then
                HI.MG.ShowMsg.mInfo("ไม่สามารถแก้ไขข้อมูลได้ เนื่องจากมีการอนุมัติรายการแล้ว กรุณาตรวจสอบ !!!!!", 1909251101, Me.Text)
                Exit Sub
            End If
            If Me.FTStatePartialShip.Checked = False And Me.FTStateShortShip.Checked = False Then
                HI.MG.ShowMsg.mInfo("กรุณาเลือก สถานะแบ่งงานผลิต Short or Partial !!!!!", 1910071637, Me.Text)
                Exit Sub
            End If
            If _dt.Select("FNSeq=2 or FNSeq=1").Length > 0 Then
                If HI.MG.ShowMsg.mConfirmProcess("คุณต้องการทำการบันทึกข้อมูล GAC Date ใช่หรือไม่ ?", 1513170001) Then
                    If Me.SaveGacDate() Then
                        '  Call SendMailToProductin()
                        HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                        Me.loadDataDeail()
                    Else
                        HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                    End If
                End If
            Else

                HI.MG.ShowMsg.mInfo("ไม่พบข้อมูลที่มีการแก้ไข GAC Date กรุณาทำการตรวจสอบ !!!", 1513170002, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
            End If

        End If
    End Sub


    Private Sub FTCustomerPO1_EditValueChanged(sender As Object, e As EventArgs) Handles FTNikePOLineItem.EditValueChanged
        Try
            If Me.FTCustomerPO.Text <> "" And Me.FTNikePOLineItem.Text <> "" And Me.FNHSysStyleId.Text <> "" And Me.FTColorway.Text <> "" Then
                'W_PRCbShowBrowseDataSubOrderInfo(Me.FTCustomerPO.Text)
                Call loadDataDeail()
            Else
                'loadDataDeail()

            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Function Delete() As Boolean

    End Function
    Private Sub loadDataDeail()
        Try
            Dim _Cmd As String = ""
            Dim _oDt As System.Data.DataTable

            _Cmd = "Select Top 1 isnull(FTStateShort,'0') as    FTStateShort, isnull(FTStatePartial,'0') as  FTStatePartial, isnull(FTStateFactoryApp,'0') as  FTStateFactoryApp, FTFactoryAppBy,isnull(FTStateMngApp,'0') as  FTStateMngApp, isnull(FTStateApp,'0') as   FTStateApp,   "
            _Cmd &= vbCrLf & "        isnull(FTStateCancel,'0') as  FTStateCancel    From " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PLANNING) & ".dbo.TPPCTPrePartialShip with(nolock)"
            _Cmd &= vbCrLf & " where FTCustomerPO='" & HI.UL.ULF.rpQuoted(Me.FTCustomerPO.Text) & "'"
            _Cmd &= vbCrLf & " and  FTNikePOLineItem='" & HI.UL.ULF.rpQuoted(Me.FTNikePOLineItem.Text) & "'"
            _Cmd &= vbCrLf & " and FNHSysStyleId=" & Val(Me.FNHSysStyleId.Properties.Tag) & ""
            _Cmd &= vbCrLf & " and  FTColorway='" & HI.UL.ULF.rpQuoted(Me.FTColorway.Text) & "'"

            _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PLANNING)
            Me.FTStateShortShip.Checked = False
            Me.FTStatePartialShip.Checked = False
            Me.FTStateFactoryApp.Checked = False
            Me.FTStateMngApp.Checked = False
            Me.FTStateApp.Checked = False
            Me.FTStateCancel.Checked = False

            For Each R As DataRow In _oDt.Rows
                Me.FTStateShortShip.Checked = R!FTStateShort.ToString = "1"
                Me.FTStatePartialShip.Checked = R!FTStatePartial.ToString = "1"
                Me.FTStateFactoryApp.Checked = R!FTStateFactoryApp.ToString = "1"
                Me.FTStateMngApp.Checked = R!FTStateMngApp.ToString = "1"
                Me.FTStateApp.Checked = R!FTStateApp.ToString = "1"
                Me.FTStateCancel.Checked = R!FTStateCancel.ToString = "1"
                Exit For
            Next

            _Cmd = "Select  * From " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PLANNING) & ".dbo.V_OrderInfo_PartialShipment"
            _Cmd &= vbCrLf & " where FTPOref='" & HI.UL.ULF.rpQuoted(Me.FTCustomerPO.Text) & "'"
            _Cmd &= vbCrLf & " and  FTNikePOLineItem='" & HI.UL.ULF.rpQuoted(Me.FTNikePOLineItem.Text) & "'"
            _Cmd &= vbCrLf & " and FTStyleCode='" & HI.UL.ULF.rpQuoted(Me.FNHSysStyleId.Text) & "'"
            _Cmd &= vbCrLf & " and  FTColorway='" & HI.UL.ULF.rpQuoted(Me.FTColorway.Text) & "'"
            Me.ogc.DataSource = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PLANNING)

            _Cmd = " exec " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PLANNING) & ".dbo.SP_GET_OrderBreak_ForPartial  '" & HI.UL.ULF.rpQuoted(Me.FTCustomerPO.Text) & "','" & HI.UL.ULF.rpQuoted(Me.FTNikePOLineItem.Text) & "'"
            _Cmd &= " ,'" & Val(Me.FNHSysStyleId.Properties.Tag) & "'"
            _Cmd &= ",'" & HI.UL.ULF.rpQuoted(Me.FTColorway.Text) & "'"

            _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_MERCHAN)

            Dim _colcount As Integer = 0

            With Me.GridView1
                .OptionsView.ShowAutoFilterRow = False
                .OptionsView.ColumnAutoWidth = False
                For I As Integer = .Columns.Count - 1 To 0 Step -1

                    Select Case .Columns(I).FieldName.ToString.ToUpper

                        'Case "FNRowId".ToUpper, "FTPOref".ToUpper, "FTNikePOLineItem".ToUpper, "FNTotal".ToUpper, "FTDescription".ToUpper, "FNSeq".ToUpper, "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FDShipDate".ToUpper, "FTColorway".ToUpper, "FNHSysStyleId".ToUpper, "FTNewNikePOLineItem".ToUpper
                        Case "FNRowId".ToUpper, "FTDescription".ToUpper, "FNSeq".ToUpper, "FTPOref".ToUpper, "FTNikePOLineItem".ToUpper, "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FNHSysStyleId".ToUpper


                            .Columns(I).AppearanceCell.BackColor = Color.White
                            .Columns(I).AppearanceCell.ForeColor = Color.Black
                            .Columns(I).OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False

                        Case "FNTotal".ToUpper

                            .Columns(I).AppearanceCell.BackColor = Color.White
                            .Columns(I).AppearanceCell.ForeColor = Color.Black
                            .Columns(I).OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                        Case "FNPercent".ToUpper, "FDShipDate".ToUpper, "FTColorway".ToUpper, "FTNewNikePOLineItem".ToUpper,
                          "FTRemark".ToUpper, "FTMainMatCode".ToUpper, "FTColorway".ToUpper, "FTRawMatCode".ToUpper, "FTItemCode".ToUpper, "FDRcvDate".ToUpper, "FTStatePOCancel".ToUpper, "FNRawMatQty".ToUpper
                            .Columns(I).AppearanceCell.BackColor = Color.White
                            .Columns(I).AppearanceCell.ForeColor = Color.Black
                            .Columns(I).OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                        Case Else
                            .Columns.Remove(.Columns(I))
                    End Select

                Next
                If Not (_oDt Is Nothing) Then
                    For Each Col As DataColumn In _oDt.Columns

                        Select Case Col.ColumnName.ToString.ToUpper
                            ' Case "FNRowId".ToUpper, "FTPOref".ToUpper, "FTNikePOLineItem".ToUpper, "FNTotal".ToUpper, "FTDescription".ToUpper, "FNSeq".ToUpper, "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FDShipDate".ToUpper, "FTColorway".ToUpper, "FNHSysStyleId".ToUpper, "FTNewNikePOLineItem".ToUpper
                            Case "FNRowId".ToUpper, "FTDescription".ToUpper, "FNSeq".ToUpper, "FTPOref".ToUpper, "FTNikePOLineItem".ToUpper, "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FNHSysStyleId".ToUpper

                            Case "FNTotal".ToUpper

                            Case "FNRowId".ToUpper, "FNPercent".ToUpper, "FNSeq".ToUpper, "FTPOref".ToUpper, "FTNikePOLineItem".ToUpper, "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FDShipDate".ToUpper, "FTColorway".ToUpper, "FNHSysStyleId".ToUpper, "FTNewNikePOLineItem".ToUpper,
                                 "FTRemark".ToUpper, "FTMainMatCode".ToUpper, "FTColorway".ToUpper, "FTRawMatCode".ToUpper, "FTItemCode".ToUpper, "FDRcvDate".ToUpper, "FTStatePOCancel".ToUpper, "FNRawMatQty".ToUpper
                                'With Me.GridView1
                                '    .Columns(Col.ColumnName.ToString).Width = 60
                                '    .Columns(Col.ColumnName.ToString).VisibleIndex = .Columns(Col.ColumnName.ToString).VisibleIndex + 99
                                '    _colcount = _colcount + 1
                                'End With


                                '  Case "FNTotal".ToUpper, "FNPercent".ToUpper, "FDShipDate".ToUpper, "FTColorway".ToUpper, "FTNewNikePOLineItem".ToUpper,
                                '"FTRemark".ToUpper, "FTMainMatCode".ToUpper, "FTColorway".ToUpper, "FTRawMatCode".ToUpper, "FTItemCode".ToUpper, "FDRcvDate".ToUpper, "FTStatePOCancel".ToUpper, "FNRawMatQty".ToUpper


                            Case Else
                                _colcount = _colcount + 1
                                Dim ColG As New DevExpress.XtraGrid.Columns.GridColumn
                                With ColG
                                    .Visible = True
                                    .FieldName = Col.ColumnName.ToString
                                    .Name = "c" & Col.ColumnName.ToString
                                    .Caption = Col.ColumnName.ToString
                                    .VisibleIndex = _colcount

                                End With

                                .Columns.Add(ColG)
                                Dim Ctrl As New Object
                                With .Columns(Col.ColumnName.ToString)

                                    .OptionsFilter.AllowAutoFilter = False
                                    .OptionsFilter.AllowFilter = False
                                    .DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                                    .DisplayFormat.FormatString = "{0:n0}"
                                    .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                                    .AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far

                                    With .OptionsColumn
                                        .AllowMove = False
                                        .AllowGroup = DevExpress.Utils.DefaultBoolean.False
                                        .AllowSort = DevExpress.Utils.DefaultBoolean.False
                                        .AllowEdit = False
                                        .ReadOnly = True
                                    End With

                                    Ctrl = New DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit

                                    With CType(Ctrl, DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit)
                                        .Name = "crep" & Col.ColumnName.ToString
                                        .Precision = 0
                                        .DisplayFormat.FormatType = FormatType.Numeric
                                        .DisplayFormat.FormatString = "N0"
                                        .Buttons(0).Visible = False
                                        AddHandler .EditValueChanging, AddressOf CalSet_EditValueChanging
                                        AddHandler .Leave, AddressOf RepositoryItemCalcEdit1_Leave
                                    End With


                                    .ColumnEdit = Ctrl
                                End With

                                .Columns(Col.ColumnName.ToString).Width = 100
                                .Columns(Col.ColumnName.ToString).Summary.Add(DevExpress.Data.SummaryItemType.Sum)
                                .Columns(Col.ColumnName.ToString).SummaryItem.DisplayFormat = "{0:n0}"

                        End Select

                    Next
                    With Me.GridView1
                        .Columns("FNTotal").VisibleIndex = _colcount + 2
                    End With
                    For Each GridCol As DevExpress.XtraGrid.Columns.GridColumn In .Columns
                        With GridCol
                            .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                        End With
                    Next

                End If

            End With
            Call InitRepDate()

            Me.ogdColorSizeBreakdown.DataSource = _oDt.Copy
            Me.GridView1.BestFitColumns()
            'Me.GridView1.RefreshData()

        Catch ex As Exception

        End Try
    End Sub

    Private Function getIndex(sizeCode As String) As Integer
        Try
            Dim _Cmd As String = ""
            _Cmd = "Select top 1 FNMatSizeSeq From  " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TMERMMatSize with(nolock)"
            _Cmd &= vbCrLf & " where FTMatSizeCode='" & sizeCode & "'"
            Return HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_MASTER, "0")
        Catch ex As Exception
            Return 0
        End Try
    End Function


    Private Sub InitGridInfo()
        Try
            Dim _Cmd As String = "" : Dim _Qry As String = ""
            Dim _FieldName As String = ""
            Dim _dt As System.Data.DataTable
            Dim _ColCount As Integer = 0

            _Cmd = "Select   * From " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & ".dbo.HSysObjDynamic_D with(nolock) where  (FNObjID = 99999001)"
            _dt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_SYSTEM)
            With Me.ogv
                .Columns.Clear()
                For Each Row As DataRow In _dt.Select("FNSeq>0", "FNSeq asc")


                    _FieldName = Row!FTFiledName.ToString


                    Dim GridCol As New DevExpress.XtraGrid.Columns.GridColumn
                    Dim Ctrl As New Object
                    With GridCol
                        .Name = _FieldName
                        .FieldName = _FieldName
                        .Width = Val(Row!FNColWidth.ToString)

                        .AppearanceCell.Options.UseTextOptions = True
                        .AppearanceHeader.Options.UseTextOptions = True
                        .AppearanceHeader.TextOptions.HAlignment = VertAlignment.Center
                        .OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
                        .OptionsColumn.ShowInCustomizationForm = False
                        '.OptionsColumn.AllowSize = True
                        .VisibleIndex = _ColCount

                        .Visible = (Row!FTStateShowInGrid.ToString = "Y")
                        .OptionsColumn.ReadOnly = True
                        .OptionsColumn.AllowEdit = False
                        .Caption = Row!FTFiledName.ToString


                        Select Case Row!FTFormControlType.ToString.ToUpper
                            Case "TextEdit".ToUpper
                                Ctrl = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
                                With CType(Ctrl, DevExpress.XtraEditors.Repository.RepositoryItemTextEdit)
                                    .Name = Row!FTFiledName.ToString
                                    .MaxLength = Val(Row!FNMaxLenght.ToString)

                                    If Row!FTStaGridTextUpper.ToString = "Y" Then
                                        .CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
                                    End If
                                End With
                                .AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
                            Case "CalcEdit".ToUpper

                                Ctrl = New DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit

                                With CType(Ctrl, DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit)
                                    .Name = Row!FTFiledName.ToString
                                    .Precision = Val(Row!FNNumericScale.ToString)
                                    .DisplayFormat.FormatType = FormatType.Numeric
                                    .DisplayFormat.FormatString = "N" & Val(Row!FNNumericScale.ToString).ToString

                                End With

                                .AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
                            Case "MemoEdit".ToUpper
                                Ctrl = New DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit
                                With Ctrl
                                    .Name = Row!FTFiledName.ToString
                                    .Properties.MaxLength = Val(Row!FNMaxLenght.ToString)
                                End With

                                .AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
                            Case "DateEdit".ToUpper
                                Ctrl = New DevExpress.XtraEditors.Repository.RepositoryItemDateEdit
                                With CType(Ctrl, DevExpress.XtraEditors.Repository.RepositoryItemDateEdit)
                                    .Name = Row!FTFiledName.ToString
                                    .AllowNullInput = DefaultBoolean.False
                                    .DisplayFormat.FormatString = "d"
                                    .DisplayFormat.FormatType = FormatType.Custom
                                    .EditFormat.FormatString = "d"
                                    .EditFormat.FormatType = FormatType.Custom
                                    .Mask.EditMask = "d"

                                    .ShowClear = True

                                    AddHandler .Leave, AddressOf RepositoryItemDate_Leave
                                    AddHandler .Click, AddressOf RepositoryItemDate_GotFocus

                                End With
                                .AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                            Case "CheckEdit".ToUpper
                                Ctrl = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit

                                With Ctrl
                                    .Name = Row!FTFiledName.ToString
                                    .ValueChecked = "1"
                                    .ValueUnchecked = "0"
                                End With

                                .AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                            Case "ButtonEdit".ToUpper
                                Ctrl = New DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit
                                With CType(Ctrl, DevExpress.XtraEditors.ButtonEdit)
                                    .Name = Row!FTFiledName.ToString
                                    .Properties.Buttons.Item(0).Tag = Row!FNButtonEditBrwID.ToString
                                    .Properties.MaxLength = Val(Row!FNMaxLenght.ToString)


                                    If Row!FTStaTextUpper.ToString = "Y" Then
                                        .Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
                                    End If

                                End With
                                .AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
                            Case "ComboBoxEdit".ToUpper
                                Ctrl = New DevExpress.XtraEditors.Repository.RepositoryItemComboBox
                                With Ctrl
                                    .Name = Row!FTFiledName.ToString
                                End With
                                .AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
                            Case Else
                                Ctrl = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
                                With Ctrl
                                    .Name = Row!FTFiledName.ToString
                                    .Properties.MaxLength = Val(Row!FNMaxLenght.ToString)

                                    If Row!FTStaGridTextUpper.ToString = "Y" Then
                                        .CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
                                    End If

                                End With
                                .AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
                        End Select

                        .ColumnEdit = Ctrl

                    End With

                    .Columns.Add(GridCol)
                    _ColCount = _ColCount + 1

                Next


            End With


        Catch ex As Exception

        End Try
    End Sub

    Public Shared Sub RepositoryItemDate_Leave(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            With CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView)

                Dim _TDate As String
                Try
                    _TDate = HI.UL.ULDate.ConvertEnDB(CType(sender, DevExpress.XtraEditors.DateEdit).DateTime)
                Catch ex As Exception
                    _TDate = ""
                End Try

                CType(sender, DevExpress.XtraEditors.DateEdit).Text = _TDate
                .SetRowCellValue(.FocusedRowHandle, .FocusedColumn.FieldName.ToString, HI.UL.ULDate.ConvertEN(_TDate))

            End With

        Catch ex As Exception
        End Try
    End Sub

    Public Shared Sub RepositoryItemDate_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
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

                If CType(sender, DevExpress.XtraEditors.DateEdit).Text = "" Then
                    Beep()
                End If
            End With

        Catch ex As Exception
        End Try
    End Sub

    Private Sub GridView1_ShowingEditor(sender As Object, e As CancelEventArgs) Handles GridView1.ShowingEditor
        Try
            Dim view As GridView = TryCast(sender, GridView)
            If Me.FTStateFactoryApp.Checked Then Exit Sub
            'Exit Sub
            If view.GetRowCellValue(view.FocusedRowHandle, "FNSeq") = 1 Then
                e.Cancel = False
                Exit Sub
            Else
                If view.GetRowCellValue(view.FocusedRowHandle, "FNSeq") = 2 Then
                    e.Cancel = False

                    'Select Case view.FocusedColumn.Name.ToUpper
                    '    Case "FTRemark".ToUpper, "FTMainMatCode".ToUpper, "FTColorway".ToUpper, "FTRawMatCode".ToUpper, "FTItemCode".ToUpper, "FDRcvDate".ToUpper, "FTStatePOCancel".ToUpper, "FNRawMatQty".ToUpper
                    '        e.Cancel = False
                    '        'Exit Sub
                    '    Case Else

                    'End Select
                    Select Case view.FocusedColumn.Name.ToUpper
                        Case "cFDShipDate".ToUpper, "cFTNewNikePOLineItem".ToUpper,
                                 "cFTRemark".ToUpper, "FTMainMatCode".ToUpper, "FTRawMatCode".ToUpper, "FTItemCode".ToUpper, "FDRcvDate".ToUpper, "FTStatePOCancel".ToUpper, "FNRawMatQty".ToUpper
                            e.Cancel = False
                            Exit Sub
                        Case Else
                            e.Cancel = True
                            Exit Sub
                    End Select

                Else

                    e.Cancel = True
                    Exit Sub


                End If

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub GridView1_MouseDown(sender As Object, e As MouseEventArgs) Handles GridView1.MouseDown
        Try
            Dim view As GridView = TryCast(sender, GridView)
            If Me.FTStateFactoryApp.Checked Then Exit Sub
            If view.GetRowCellValue(view.FocusedRowHandle, "FNSeq") = 1 Then
                'view.Columns("S").OptionsColumn.AllowEdit = True
                'view.Columns("S").OptionsColumn.ReadOnly = False

                With Me.GridView1

                    For I As Integer = .Columns.Count - 1 To 0 Step -1

                        Select Case .Columns(I).FieldName.ToString.ToUpper

                            Case "FNRowId".ToUpper, "FTDescription".ToUpper, "FNTotal".ToUpper, "FNPercent".ToUpper, "FNSeq".ToUpper, "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FDShipDate".ToUpper, "FTColorway".ToUpper, "FNHSysStyleId".ToUpper, "FTNewNikePOLineItem".ToUpper
                                .Columns(I).AppearanceCell.BackColor = Color.White
                                .Columns(I).AppearanceCell.ForeColor = Color.Black
                                .Columns(I).OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                            Case Else
                                .Columns(I).OptionsColumn.AllowEdit = True
                                .Columns(I).OptionsColumn.ReadOnly = False
                        End Select

                    Next
                End With
            ElseIf view.GetRowCellValue(view.FocusedRowHandle, "FNSeq") = 2 Then
                With Me.GridView1

                    For I As Integer = .Columns.Count - 1 To 0 Step -1

                        Select Case .Columns(I).FieldName.ToString.ToUpper

                            Case "FNRowId".ToUpper, "FTDescription".ToUpper, "FNTotal".ToUpper, "FNPercent".ToUpper, "FNSeq".ToUpper, "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FDShipDate".ToUpper, "FTColorway".ToUpper, "FNHSysStyleId".ToUpper, "FTNewNikePOLineItem".ToUpper

                                .Columns(I).AppearanceCell.BackColor = Color.White
                                .Columns(I).AppearanceCell.ForeColor = Color.Black
                                .Columns(I).OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                            Case Else
                                .Columns(I).OptionsColumn.AllowEdit = True
                                .Columns(I).OptionsColumn.ReadOnly = False
                        End Select

                    Next
                End With
            Else

                With Me.GridView1

                    For I As Integer = .Columns.Count - 1 To 0 Step -1

                        Select Case .Columns(I).FieldName.ToString.ToUpper

                            Case "FNRowId".ToUpper, "FTDescription".ToUpper, "FNTotal".ToUpper, "FNPercent".ToUpper, "FNSeq".ToUpper, "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FDShipDate".ToUpper, "FTColorway".ToUpper, "FNHSysStyleId".ToUpper, "FTNewNikePOLineItem".ToUpper
                                .Columns(I).AppearanceCell.BackColor = Color.White
                                .Columns(I).AppearanceCell.ForeColor = Color.Black
                                .Columns(I).OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                            Case Else
                                .Columns(I).OptionsColumn.AllowEdit = False
                                .Columns(I).OptionsColumn.ReadOnly = True
                        End Select

                    Next
                End With
            End If


        Catch ex As Exception

        End Try
    End Sub

    Private Sub CalSet_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs)
        Try
            Dim _NewValue As Double = e.NewValue
            Dim _OrgValue As Double = 0
            Dim _Size As String = ""
            Dim _FTColorway As String = ""

            If e.NewValue < 0 Then
                e.Cancel = True
            Else
                With CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView)

                    .SetFocusedRowCellValue(.FocusedColumn.FieldName, Integer.Parse(Val(e.NewValue)))

                End With

            End If


        Catch ex As Exception
            e.Cancel = True
        End Try
    End Sub


    Private Sub GridView1_RowStyle(sender As Object, e As RowStyleEventArgs) Handles GridView1.RowStyle
        Try
            Dim View As GridView = sender
            If (e.RowHandle >= 0) Then
                Dim category As String = View.GetRowCellDisplayText(e.RowHandle, View.Columns("FNSeq"))
                Select Case category
                    Case "0"
                        e.Appearance.BackColor = Color.Transparent
                        e.HighPriority = True
                        e.Appearance.Font = New System.Drawing.Font("Tahoma", 8, FontStyle.Bold)
                    Case "1"
                        e.Appearance.BackColor = Color.Transparent

                        e.HighPriority = True
                        e.Appearance.Font = New System.Drawing.Font("Tahoma", 8, FontStyle.Bold)
                    Case "2"
                        e.Appearance.BackColor = Color.DodgerBlue
                        e.Appearance.BackColor2 = Color.White
                        e.HighPriority = True
                        e.Appearance.Font = New System.Drawing.Font("Tahoma", 8, FontStyle.Bold)
                    Case "3"
                        e.Appearance.BackColor = Color.Salmon
                        e.Appearance.BackColor2 = Color.SeaShell
                        e.HighPriority = True
                End Select

            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub wPartialShipment_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.FNHSysCmpId.Text = HI.ST.SysInfo.CmpCode
    End Sub

    Private Sub FTCustomerPO_EditValueChanged(sender As Object, e As EventArgs) Handles FTCustomerPO.EditValueChanged
        If Me.FTCustomerPO.Text = "" Then
            Me.FTNikePOLineItem.Text = ""
        End If
    End Sub

    Private Sub OcmSendApprove_Click(sender As Object, e As EventArgs) Handles ocmSendApprove.Click
        Try
            If Me.FTStateFactoryApp.Checked Then Exit Sub
            If HI.MG.ShowMsg.mConfirmProcess("คุณต้องการส่งอนุมัติใช่หรือไม่ !!!", 1909231135, Me.Text) = True Then
                Me.FTStateFactoryApp.Checked = True
                Dim _Qry As String = ""
                _Qry = "Update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TPPCTPrePartialShip"
                _Qry &= vbCrLf & " SET  FTFactoryAppBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Qry &= vbCrLf & ",FDFactoryAppDate=" & HI.UL.ULDate.FormatDateDB & ""
                _Qry &= vbCrLf & ",FTFactoryAppTime=" & HI.UL.ULDate.FormatTimeDB & ""
                _Qry &= vbCrLf & ", FTStateFactoryApp ='" & HI.UL.ULF.rpQuoted(Me.FTStateFactoryApp.EditValue) & "'"
                _Qry &= vbCrLf & "where FTCustomerPO= '" & HI.UL.ULF.rpQuoted(Me.FTCustomerPO.Text) & "'"
                _Qry &= vbCrLf & " and FTNikePOLineItem ='" & HI.UL.ULF.rpQuoted(Me.FTNikePOLineItem.Text) & "'"
                HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_PLANNING)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Ocmapprovepoline_Click(sender As Object, e As EventArgs) Handles ocmapprovepoline.Click
        Try
            If Me.FTStateApp.Checked Then Exit Sub
            If Me.FTStateMngApp.Checked And Me.FTStateFactoryApp.Checked And Me.FTStateCancel.Checked = False Then
                If HI.MG.ShowMsg.mConfirmProcess("คุณต้องการอนุมัติใช่หรือไม่ !!!", 1909231137, Me.Text) = True Then
                    Dim _Qry As String = "" : Dim _OrderNo As String = "" : Dim _OrderNoSub As String = ""

                    With DirectCast(Me.ogdColorSizeBreakdown.DataSource, System.Data.DataTable)
                        .AcceptChanges()
                        For Each R As DataRow In .Rows
                            _OrderNo = R!FTOrderNo.ToString
                            _OrderNoSub = R!FTSubOrderNo.ToString
                            Exit For
                        Next
                    End With
                    Dim NewLineItem As String = "" : Dim NewGacDate As String = ""
                    With DirectCast(Me.ogdColorSizeBreakdown.DataSource, System.Data.DataTable)
                        .AcceptChanges()
                        For Each R As DataRow In .Select("  FNRowId = 2")
                            NewLineItem = R!FTNewNikePOLineItem.ToString
                            NewGacDate = R!FDShipDate.ToString
                            If NewLineItem = "" Or NewGacDate = "" Then
                                HI.MG.ShowMsg.mInfo("กรุณากรอกข้อมูล New LineItem และ New GacDate ให้ครบ กรุณาตรวจสอบ !!!!", 1911260838, Me.Text, "")
                                Exit Sub
                            End If
                        Next
                    End With
                    Call SaveGacDate(True)

                    _Qry &= vbCrLf & " Select  TOP 1 '1' AS FTState"
                    _Qry &= vbCrLf & "           FROM  " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & " .dbo.TPRODTOrderProd AS A (NOLOCK) INNER JOIN  " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & ".dbo.TPRODTOrderProd_Detail AS B (NOLOCK) ON A.FTOrderProdNo = B.FTOrderProdNo"
                    _Qry &= vbCrLf & "               AND A.FTOrderNo = B.FTOrderNo"
                    _Qry &= vbCrLf & "           WHERE A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(_OrderNo) & "'"
                    _Qry &= vbCrLf & "		           AND B.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(_OrderNoSub) & "'"
                    _Qry &= vbCrLf & "	     "
                    Dim _StateCusting As Boolean = False
                    _StateCusting = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PLANNING, "0") = "1"
                    Dim _NewOrderSubRef As String = ""

                    If _StateCusting Then
                        'custing ture   // to  drivert 
                        If Me.FTStatePartialShip.Checked Then
                            PROC_SAVEbDivertSubOrderNo(_OrderNoSub, _OrderNo)


                            _Qry = "Select distinct A.FTOrderNo , A.FTSubOrderNo , A.FDNewGacDate ,B.FDShipDate  "
                            _Qry &= vbCrLf & ",A.FTNewNikePOLineItem , B.FTNikePOLineItem"
                            _Qry &= vbCrLf & ",B.FTOrderNo , B.FTSubOrderNo"
                            _Qry &= vbCrLf & " From " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & ".dbo. TPPCTPrePartialShip AS A with(nolock) "
                            _Qry &= vbCrLf & "  INNER JOIN " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.V_OrderSub_BreakDown_ShipDestination AS B ON A.FTCustomerPO = B.FTPOref and A.FTNewNikePOLineItem = B.FTNikePOLineItem "
                            _Qry &= vbCrLf & "where A.FTCustomerPO= '" & HI.UL.ULF.rpQuoted(Me.FTCustomerPO.Text) & "'"
                            _Qry &= vbCrLf & " and A.FTNewNikePOLineItem ='" & HI.UL.ULF.rpQuoted(Me.FTNikePOLineItem.Text) & "'"


                            Dim _OGac As String = ""
                            _Qry = "Select  top 1 FDShipDateOrginal  "
                            _Qry &= vbCrLf & "from " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.TMERTOrderSub with(nolock)"
                            _Qry &= vbCrLf & "where FTSubOrderNo =  '" & _OrderNoSub & "'"
                            _OGac = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MERCHAN, "")


                            With CType(Me.ogdColorSizeBreakdown.DataSource, System.Data.DataTable)
                                .AcceptChanges()

                                For Each R As DataRow In .Select("FNSeq='1' OR FNSeq='2'")



                                    _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TMERTOrderGACDateCfm"
                                    _Qry &= vbCrLf & "(FTInsUser, FDInsDate, FTInsTime, FTOrderNo, FTSubOrderNo, FNSeq, FDShipDate, FDShipDateTo,FDOShipDate,FDOShipDateTo,FDCfmShipDate,FDORShipDate,FTReasonDesc ,FTNikePOLineItem)"
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
                                    _Qry &= vbCrLf & ",'" & _OGac & "'"
                                    _Qry &= vbCrLf & ",'" & _OGac & "'"
                                    _Qry &= vbCrLf & ",'" & _OGac & "'"
                                    _Qry &= vbCrLf & ",'" & _OGac & "'"
                                    _Qry &= vbCrLf & ",''"
                                    _Qry &= vbCrLf & ",''"
                                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTNikePOLineItem.ToString) & "'"
                                    HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_PLANNING)


                                Next

                            End With
                        Else
                            'PROC_SAVEbDivertSubOrderNo(_OrderNoSub, _OrderNo)
                            PROC_CancelSubOrderNo(_OrderNoSub, _OrderNo, "0")
                        End If
                    Else
                        'custing false   // split order 
                        If Me.FTStatePartialShip.Checked Then
                            CreateNewOrderNo(_OrderNo, _OrderNoSub, _NewOrderSubRef)
                        Else

                            CreateNewOrderNo(_OrderNo, _OrderNoSub, _NewOrderSubRef)
                            PROC_CancelSubOrderNo(_NewOrderSubRef, _OrderNo, "1")
                        End If
                    End If

                    Me.FTStateApp.Checked = True


                    _Qry = "Update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TPPCTPrePartialShip"
                    _Qry &= vbCrLf & " SET  FTStateAppBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Qry &= vbCrLf & ",FDAppDate=" & HI.UL.ULDate.FormatDateDB & ""
                    _Qry &= vbCrLf & ",FTAppTime=" & HI.UL.ULDate.FormatTimeDB & ""
                    _Qry &= vbCrLf & ",FTStateApp ='" & HI.UL.ULF.rpQuoted(Me.FTStateApp.EditValue) & "'"
                    '_Qry &= vbCrLf & ",FTNewNikePOLineItem=ง"
                    '_Qry &= vbCrLf & ","

                    _Qry &= vbCrLf & "where FTCustomerPO= '" & HI.UL.ULF.rpQuoted(Me.FTCustomerPO.Text) & "'"
                    _Qry &= vbCrLf & " and FTNikePOLineItem ='" & HI.UL.ULF.rpQuoted(Me.FTNikePOLineItem.Text) & "'"
                    HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_PLANNING)


                    '_Qry = ""


                    HI.MG.ShowMsg.mInfo("อนุมัติข้อมูลแบ่งผลิตเรียบร้อย !!!!", 1909240907, Me.Text, "")
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Function PROC_CancelSubOrderNo(ByVal paramFTSubOrderNoSrc As String, _FTOrderNoSrc As String, _StateCanSub As String) As Boolean
        Dim _Qry As String = ""
        Dim _QryCheckSeq As String = ""
        Dim bRet As Boolean = False
        Dim Maxleng As String = ""
        Dim SubOrderDivertSeq As Integer = 0
        Dim _odtSubOrderDivert As System.Data.DataTable
        Dim _remark As String = ""
        Dim _OrderNo As String = ""
        Dim _SubOrderNo As String = ""
        Dim _ColorWay As String = ""
        Dim _sRemark As String = ""
        Dim _RawQty As Double = 0 : Dim _FTMainmatCode As String = "" : Dim _FTRawMatCode As String = "" : Dim _ItemCode As String = "" : Dim _RcvDate As String = "" : Dim _FTStatePOCancel As String = ""


        'Sub Order No ที่ทำการ Divert มานั้นจะต้องทำการ Copy รายการต่อไปนี้หรือไม่ ? : Component Info/ Sewing Info/ Packing Info/ Packing Carton Info/ Size Spec Info
        Try
            _Qry = "Select  *      FROM  " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.TMERTOrderSub_Divert with(nolock) "
            _Qry &= vbCrLf & "  WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(_FTOrderNoSrc) & "'"
            _Qry &= vbCrLf & "  AND  FTSubOrderNo='" & HI.UL.ULF.rpQuoted(paramFTSubOrderNoSrc) & "'"
            _Qry &= vbCrLf & " Order by FNDivertSeq desc "
            _odtSubOrderDivert = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)
            If _odtSubOrderDivert.Rows.Count > 0 Then
                SubOrderDivertSeq = Val(_odtSubOrderDivert.Compute("max(FNDivertSeq) ", "FTSubOrderNo <> ''"))
            Else
                SubOrderDivertSeq = 0
            End If



            'If SubOrderDivertSeq <= 0 Then
            '    _QryCheckSeq = "select top 1 Max(FNCancelSeq) AS MaxlengSeq FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub_Cancel"
            '    _QryCheckSeq &= vbCrLf & "  WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(_FTOrderNoSrc) & "'"
            '    _QryCheckSeq &= vbCrLf & "  AND  FTSubOrderNo='" & HI.UL.ULF.rpQuoted(paramFTSubOrderNoSrc) & "'"

            '    Maxleng = Integer.Parse(Val(HI.Conn.SQLConn.GetField(_QryCheckSeq, HI.Conn.DB.DataBaseName.DB_MERCHAN, "0"))) + 1

            '    _Qry = "   INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTOrderSub_Cancel] ([FTInsUser],[FDInsDate],[FTInsTime]"
            '    _Qry &= vbCrLf & "              ,FNCancelSeq"
            '    _Qry &= vbCrLf & "              ,[FTOrderNo],[FTSubOrderNo]"
            '    _Qry &= vbCrLf & "              ,[FTRemark],[FTStateCancenSub])"
            '    _Qry &= vbCrLf & "   SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "', " & HI.UL.ULDate.FormatDateDB & ", " & HI.UL.ULDate.FormatTimeDB & ", "
            '    _Qry &= vbCrLf & " " & Maxleng & " ,"
            '    _Qry &= vbCrLf & "         '" & HI.UL.ULF.rpQuoted(_FTOrderNoSrc) & "',"
            '    _Qry &= vbCrLf & "         '" & HI.UL.ULF.rpQuoted(paramFTSubOrderNoSrc) & "',"
            '    _Qry &= vbCrLf & "         N'" & HI.UL.ULF.rpQuoted(_remark) & "'"
            '    _Qry &= vbCrLf & ",'1'"

            'Else

            '    Maxleng = SubOrderDivertSeq

            '    _Qry = "  UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTOrderSub_Cancel]  SET  "
            '    _Qry &= vbCrLf & " FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            '    _Qry &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB & ""
            '    _Qry &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & ""
            '    _Qry &= vbCrLf & ",[FTRemark]=N'" & HI.UL.ULF.rpQuoted(_remark) & "'"
            '    _Qry &= vbCrLf & ",[FTStateCancenSub]='1'"
            '    _Qry &= vbCrLf & " WHERE FTOrderNo = N'" & HI.UL.ULF.rpQuoted(_FTOrderNoSrc) & "'"
            '    _Qry &= vbCrLf & "       AND FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(paramFTSubOrderNoSrc) & "'"
            '    _Qry &= vbCrLf & "       AND FNCancelSeq =" & Val(Maxleng) & ""

            '    _Qry &= vbCrLf & "  DELETE FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTOrderSub_Cancel_BreakDown]    "
            '    _Qry &= vbCrLf & " WHERE FTOrderNo = N'" & HI.UL.ULF.rpQuoted(_FTOrderNoSrc) & "'"
            '    _Qry &= vbCrLf & "       AND FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(paramFTSubOrderNoSrc) & "'"
            '    _Qry &= vbCrLf & "       AND FNCancelSeq =" & Val(Maxleng) & ""

            'End If

            _QryCheckSeq = "select top 1 Max(FNCancelSeq) AS MaxlengSeq FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub_Cancel"
            _QryCheckSeq &= vbCrLf & "  WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(_FTOrderNoSrc) & "'"
            _QryCheckSeq &= vbCrLf & "  AND  FTSubOrderNo='" & HI.UL.ULF.rpQuoted(paramFTSubOrderNoSrc) & "'"

            Maxleng = Integer.Parse(Val(HI.Conn.SQLConn.GetField(_QryCheckSeq, HI.Conn.DB.DataBaseName.DB_MERCHAN, "0"))) + 1

            _Qry = "   INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTOrderSub_Cancel] ([FTInsUser],[FDInsDate],[FTInsTime]"
            _Qry &= vbCrLf & "              ,FNCancelSeq"
            _Qry &= vbCrLf & "              ,[FTOrderNo],[FTSubOrderNo]"
            _Qry &= vbCrLf & "              ,[FTRemark],[FTStateCancenSub],[FTStateShortShip])"
            _Qry &= vbCrLf & "   SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "', " & HI.UL.ULDate.FormatDateDB & ", " & HI.UL.ULDate.FormatTimeDB & ", "
            _Qry &= vbCrLf & " " & Maxleng & " ,"
            _Qry &= vbCrLf & "         '" & HI.UL.ULF.rpQuoted(_FTOrderNoSrc) & "',"
            _Qry &= vbCrLf & "         '" & HI.UL.ULF.rpQuoted(paramFTSubOrderNoSrc) & "',"
            _Qry &= vbCrLf & "         N'" & HI.UL.ULF.rpQuoted(_remark) & "'"
            _Qry &= vbCrLf & ",'" & _StateCanSub & "'"
            _Qry &= vbCrLf & ",'1'"


            With CType(Me.ogdColorSizeBreakdown.DataSource, System.Data.DataTable)
                .AcceptChanges()

                For Each R As DataRow In .Select(" FNSeq=2 ")

                    _OrderNo = R!FTOrderNo.ToString
                    _SubOrderNo = R!FTSubOrderNo.ToString
                    _ColorWay = R!FTColorway.ToString

                    '---------
                    _sRemark = R!FTRemark.ToString : _FTMainmatCode = R!FTMainMatCode.ToString : _FTRawMatCode = R!FTRawMatCode.ToString
                    _ItemCode = R!FTItemCode.ToString : _RcvDate = R!FDRcvDate.ToString : _FTStatePOCancel = R!FTStatePOCancel
                    _RawQty = Val(R!FNRawMatQty.ToString)

                    For Ix As Integer = .Columns.Count - 1 To 0 Step -1
                        Select Case .Columns(Ix).ColumnName.ToString.ToUpper
                            Case "FNRowId".ToUpper, "FTDescription".ToUpper, "FNTotal".ToUpper, "FNPercent".ToUpper, "FNSeq".ToUpper, "FTPOref".ToUpper, "FTNikePOLineItem".ToUpper, "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FDShipDate".ToUpper, "FTColorway".ToUpper, "FNHSysStyleId".ToUpper, "FTNewNikePOLineItem".ToUpper,
                                 "FTRemark".ToUpper, "FTMainMatCode".ToUpper, "FTColorway".ToUpper, "FTRawMatCode".ToUpper, "FTItemCode".ToUpper, "FDRcvDate".ToUpper, "FTStatePOCancel".ToUpper, "FNRawMatQty".ToUpper

                            Case Else
                                If (Integer.Parse(Val(R.Item(.Columns(Ix).ColumnName.ToString).ToString))) > 0 Then

                                    _Qry &= vbCrLf & "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTOrderSub_Cancel_BreakDown] ([FTInsUser],[FDInsDate],[FTInsTime]"
                                    _Qry &= vbCrLf & ",FNCancelSeq"
                                    _Qry &= vbCrLf & ",[FTOrderNo],[FTSubOrderNo]"
                                    _Qry &= vbCrLf & ",[FTColorway],[FTSizeBreakDown]"
                                    _Qry &= vbCrLf & ",[FNQuantity]"
                                    _Qry &= vbCrLf & ",[FTNikePOLineItem]"
                                    _Qry &= vbCrLf & ")"
                                    _Qry &= vbCrLf & "   SELECT N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "', " & HI.UL.ULDate.FormatDateDB & ", " & HI.UL.ULDate.FormatTimeDB & ", "
                                    _Qry &= vbCrLf & "" & Maxleng & ","
                                    _Qry &= vbCrLf & "          N'" & HI.UL.ULF.rpQuoted(_FTOrderNoSrc) & "',"
                                    _Qry &= vbCrLf & "'" & HI.UL.ULF.rpQuoted(paramFTSubOrderNoSrc) & "',"
                                    _Qry &= vbCrLf & "   N'" & HI.UL.ULF.rpQuoted(_ColorWay) & "', '" & HI.UL.ULF.rpQuoted(.Columns(Ix).ColumnName.ToString) & "', "
                                    _Qry &= vbCrLf & "   " & Integer.Parse(Val(R.Item(.Columns(Ix).ColumnName.ToString).ToString)) & ""
                                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTNikePOLineItem.ToString) & "'"



                                End If
                        End Select
                    Next
                Next

            End With



            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_MERCHAN)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) > 0 Then

                HI.Conn.SQLConn.Tran.Commit()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                bRet = True
            Else
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            End If

        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

        End Try

        Return bRet

    End Function




    Private Sub Ocmapprove_Click(sender As Object, e As EventArgs) Handles ocmapprove.Click
        Try
            If Me.FTStateMngApp.Checked Then Exit Sub
            If HI.MG.ShowMsg.mConfirmProcess("คุณต้องการอนุมัติใช่หรือไม่ !!!", 1909231136, Me.Text) = True Then
                Me.FTStateMngApp.Checked = True
                Dim _Qry As String = ""
                _Qry = "Update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TPPCTPrePartialShip"
                _Qry &= vbCrLf & " SET  FTMngAppBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Qry &= vbCrLf & ",FDMngAppDate=" & HI.UL.ULDate.FormatDateDB & ""
                _Qry &= vbCrLf & ",FTMngAppTime=" & HI.UL.ULDate.FormatTimeDB & ""
                _Qry &= vbCrLf & ", FTStateMngApp ='" & HI.UL.ULF.rpQuoted(Me.FTStateMngApp.EditValue) & "'"
                _Qry &= vbCrLf & "where FTCustomerPO= '" & HI.UL.ULF.rpQuoted(Me.FTCustomerPO.Text) & "'"
                _Qry &= vbCrLf & " and FTNikePOLineItem ='" & HI.UL.ULF.rpQuoted(Me.FTNikePOLineItem.Text) & "'"
                HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_PLANNING)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Ocmrevokeapproval_Click(sender As Object, e As EventArgs) Handles ocmreject.Click
        Try
            If Me.FTStateCancel.Checked Then Exit Sub
            If HI.MG.ShowMsg.mConfirmProcess("คุณต้องการอนุมัติใช่หรือไม่ !!!", 1909231136, Me.Text) = True Then
                Me.FTStateCancel.Checked = True
                Me.FTStateApp.Checked = False
                Dim _Qry As String = ""
                _Qry = "Update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TPPCTPrePartialShip"
                _Qry &= vbCrLf & " SET  FTStateCancelBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Qry &= vbCrLf & ",FDCancelDate=" & HI.UL.ULDate.FormatDateDB & ""
                _Qry &= vbCrLf & ",FTCancelTime=" & HI.UL.ULDate.FormatTimeDB & ""
                _Qry &= vbCrLf & ", FTStateCancel ='" & HI.UL.ULF.rpQuoted(Me.FTStateCancel.EditValue) & "'"
                _Qry &= vbCrLf & ", FTStateApp ='" & HI.UL.ULF.rpQuoted(Me.FTStateApp.EditValue) & "'"
                _Qry &= vbCrLf & "where FTCustomerPO= '" & HI.UL.ULF.rpQuoted(Me.FTCustomerPO.Text) & "'"
                _Qry &= vbCrLf & " and FTNikePOLineItem ='" & HI.UL.ULF.rpQuoted(Me.FTNikePOLineItem.Text) & "'"
                HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_PLANNING)
            End If
        Catch ex As Exception

        End Try
    End Sub



#Region "Function"

    Private Function CreateNewOrderNo(OrderNo As String, SubOrderNo As String, ByRef SubOrderNoRef As String) As Boolean

        Dim _Spls As New HI.TL.SplashScreen("Combine Order Breakdown...Please Wait")
        '  Dim _QryDelete As String = "Delete From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTempCreateJobProd WHERE FTUserLogIn='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
        Dim _Qry As String = ""
        Dim _OrderNo As String = ""
        Dim _SubOrderNo As String = ""
        Dim _ColorWay As String = ""
        Dim _POLine As String = ""
        Dim _tmpOrderProd As String = ""
        Dim _oDtSub As System.Data.DataTable
        Dim _oDtMain As System.Data.DataTable

        Dim I As Integer = 0
        Try

            Dim _KeyNew As String = ""
            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PROD)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction


            Dim _OrderNew As String = ""

            _Qry = "Select  FTOrderNo, FDOrderDate, FTOrderBy, FNOrderType, FTPORef"
            _Qry &= vbCrLf & ",(SELECT Top 1   FTStyleCode  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS T WITH(NOLOCK) WHERE T.FNHSysStyleId = O.FNHSysStyleId) AS FNHSysStyleId "
            _Qry &= vbCrLf & ",(SELECT Top 1  FTCmpCode  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp AS C WITH (NOLOCK) Where C.FNHSysCmpId = O.FNHSysCmpId ) AS FNHSysCmpId "
            _Qry &= vbCrLf & ",(SELECT Top 1  FTSeasonCode  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMSeason AS S WITH (NOLOCK) Where S.FNHSysSeasonId = O.FNHSysSeasonId) AS FNHSysSeasonId "

            _Qry &= vbCrLf & ",(Select Top 1 FTCmpRunCode  From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmpRun AS M   WITH(NOLOCK)Where M.FNHSysCmpRunId = O.FNHSysCmpRunId  ) AS  FNHSysCmpRunId "
            _Qry &= vbCrLf & ",FNHSysCustId, FNHSysAgencyId, FNHSysProdTypeId, FNHSysBuyerId, FTMainMaterial, FTCombination, FTRemark, FTStateOrderApp, FTAppBy, FDAppDate, FTAppTime, FNJobState, FTStateBy, FDStateDate, "
            _Qry &= vbCrLf & " FTStateTime, FTImage1, FTImage2, FTImage3, FTImage4, FNHSysBrandId, FNHSysBuyId, FTCancelAppBy, FDCancelAppDate, FDCancelAppTime, FTCancelAppRemark, FTPOTradingCo, FTPOItem, "
            _Qry &= vbCrLf & " FTPOCreateDate, FNHSysMerTeamId, FNHSysPlantId, FNHSysBuyGrpId, FNHSysMainCategoryId, FNHSysVenderPramId, FTOrderCreateStatus, FTImportUser, FDImportDate, FTImportTime, FPOrderImage1, "
            _Qry &= vbCrLf & "  FPOrderImage2, FPOrderImage3, FPOrderImage4, FNHSysSeasonId, FDDateChangeOrderImage1, FTTimeChangeOrderImage1, FTUserChangeOrderImage1, FDDateChangeOrderImage2, "
            _Qry &= vbCrLf & "  FTTimeChangeOrderImage2, FTUserChangeOrderImage2, FDDateChangeOrderImage3, FTTimeChangeOrderImage3, FTUserChangeOrderImage3, FDDateChangeOrderImage4, FTTimeChangeOrderImage4, "
            _Qry &= vbCrLf & " FTUserChangeOrderImage4, FTOrderNoRef, FTStateSendDirectorApp, FTStateSendDirectorBy, FDStateSendDirectorDate, FTStateSendDirectorTime, FTStateDirectorApp, FTStateDirectorAppBy, "
            _Qry &= vbCrLf & " FDStateDirectorAppDate, FTStateDirectorAppTime, FTStateDirectorReject, FTStateDirectorRejectBy, FDStateDirectorRejectDate, FTStateDirectorRejectTime, FTStateFactoryApp, FTStateFactoryAppBy, "
            _Qry &= vbCrLf & "  FDStateFactoryAppDate, FTStateFactoryAppTime, FTStateFactoryReject, FTStateFactoryRejectBy, FDStateFactoryRejectDate, FTStateFactoryRejectTime, FTChangeCmpBy, FDChangeCmpDate, FTChangeCmpTime,"
            _Qry &= vbCrLf & "FNHSysStyleIdPull "
            _Qry &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH(NOLOCK) "
            _Qry &= vbCrLf & " WHERE FTOrderNo='" & OrderNo.ToString & "' "
            _oDtMain = HI.Conn.SQLConn.GetDataTableOnbeginTrans(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

            _OrderNew = HI.TL.Document.GetDocumentNo("HITECH_MERCHAN", "TMERTOrder", _oDtMain.Rows(0).Item("FNOrderType").ToString, False, HI.TL.CboList.GetListRefer("FNOrderType", _oDtMain.Rows(0).Item("FNOrderType").ToString) & _oDtMain.Rows(0).Item("FNHSysCmpRunId").ToString()).ToString
            _oDtMain.BeginInit()
            For Each R As DataRow In _oDtMain.Rows
                R!FTOrderNo = _OrderNew
            Next
            _oDtMain.EndInit()


            _KeyNew = HI.Conn.SQLConn.GetField("EXEC  SP_GEN_CHARACTER_SubOrderNo '" & IIf(0 = 0, HI.UL.ULF.rpQuoted(OrderNo.ToString), HI.UL.ULF.rpQuoted(_OrderNew)) & "'", Conn.DB.DataBaseName.DB_MERCHAN, "")



            _Qry = "Select  FTOrderNo, FTSubOrderNo, FDSubOrderDate, FTSubOrderBy, FDProDate, FDShipDate "
            _Qry &= vbCrLf & " , (SELECT  Top 1  FTBuyCode FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMBuy AS B WITH(NOLOCK) WHERE B.FNHSysBuyId = O.FNHSysBuyId ) as FNHSysBuyId "
            _Qry &= vbCrLf & " ,(Select Top 1  FTContinentCode From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMContinent AS C WITH(NOLOCK) WHERE C.FNHSysContinentId = O.FNHSysContinentId ) AS FNHSysContinentId "
            _Qry &= vbCrLf & " ,(SELECT Top 1  FTCountryCode  FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCountry AS C WITH (NOLOCK) WHERE C.FNHSysCountryId = O.FNHSysCountryId) AS FNHSysCountryId "
            _Qry &= vbCrLf & " ,(SELECT Top 1  FTProvinceCode  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCMMProvince AS P WITH(NOLOCK) WHERE P.FNHSysProvinceId = O.FNHSysProvinceId) AS FNHSysProvinceId "
            _Qry &= vbCrLf & " ,(SELECT Top 1  FTShipModeCode  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMShipMode AS S WITH(NOLOCK) Where S.FNHSysShipModeId = O.FNHSysShipModeId ) AS FNHSysShipModeId "
            _Qry &= vbCrLf & " ,(SELECT Top 1  FTCurCode  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TFINMCurrency AS C WITH (NOLOCK) Where C.FNHSysCurId = O.FNHSysCurId ) AS FNHSysCurId "
            _Qry &= vbCrLf & " ,(SELECT TOP (1)  FTGenderCode  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMGender AS G WITH(NOLOCK) WHERE G.FNHSysGenderId = O.FNHSysGenderId) AS FNHSysGenderId "
            _Qry &= vbCrLf & " ,(SELECT Top 1   FTUnitCode FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit AS U WITH(NOLOCK) Where U.FNHSysUnitId = O.FNHSysUnitId ) AS FNHSysUnitId "
            _Qry &= vbCrLf & ",  (Select Top 1 FTShipPortCode  From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMShipPort AS S WITH(NOLOCK) WHERE S.FNHSysShipPortId = O.FNHSysShipPortId ) AS FNHSysShipPortId "
            _Qry &= vbCrLf & " , FNSubOrderState, FTStateEmb, FTStatePrint, FTStateHeat, FTStateLaser, FTStateWindows, FTStateOther1, FTOther1Note, FTStateOther2, FTOther2Note, FTStateOther3, "
            _Qry &= vbCrLf & " FTOther3Note1, FTRemark,   FDShipDateOrginal, FTCustRef, FNPackCartonSubType, FNPackPerCarton, FTSubOrderNoDivertRef,   FTStateSendDirectorApp, FTStateSendDirectorBy, "
            _Qry &= vbCrLf & " FDStateSendDirectorDate, FTStateSendDirectorTime, FTStateDirectorApp, FTStateDirectorAppBy, FDStateDirectorAppDate, FTStateDirectorAppTime, FTStateDirectorReject, FTStateDirectorRejectBy, "
            _Qry &= vbCrLf & " FDStateDirectorRejectDate, FTStateDirectorRejectTime, FTStateFactoryApp, FTStateFactoryAppBy, FDStateFactoryAppDate, FTStateFactoryAppTime, FTStateFactoryReject, FTStateFactoryRejectBy, "
            _Qry &= vbCrLf & " FDStateFactoryRejectDate, FTStateFactoryRejectTime --, FNImportGrpSeq"
            _Qry &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub AS O WITH(NOLOCK) "
            _Qry &= vbCrLf & " WHERE FTSubOrderNo='" & OrderNo.ToString & "-A' "
            _oDtSub = HI.Conn.SQLConn.GetDataTableOnbeginTrans(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)
            _oDtSub.BeginInit()
            For Each R As DataRow In _oDtSub.Rows
                R!FTSubOrderNo = _KeyNew
            Next
            _oDtSub.EndInit()



            'New SubOrderNO


            _KeyNew = HI.Conn.SQLConn.GetField("EXEC  SP_GEN_CHARACTER_SubOrderNo '" & IIf(0 = 0, HI.UL.ULF.rpQuoted(OrderNo.ToString), HI.UL.ULF.rpQuoted(_OrderNew)) & "'", Conn.DB.DataBaseName.DB_MERCHAN, "")
            _Qry = "  Select top 1 *   From " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.TMERTOrderSub witH(nolock) where FTOrderNo='" & HI.UL.ULF.rpQuoted(OrderNo.ToString) & "'"
            _Qry &= vbCrLf & " and FTSubOrderNo='" & HI.UL.ULF.rpQuoted(SubOrderNo.ToString) & "'"
            For Each Rx As DataRow In HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN).Rows
                _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub "
                _Qry &= "( [FTInsUser],[FDInsDate],[FTInsTime] ,[FTOrderNo],[FTSubOrderNo],[FDSubOrderDate],[FTSubOrderBy] ,[FDProDate],[FDShipDate],[FNHSysContinentId] ,[FNHSysCountryId],[FNHSysProvinceId],[FNHSysShipModeId],[FNHSysCurId]"
                _Qry &= ",[FNHSysGenderId],[FNHSysUnitId],[FTStateEmb] ,[FTStatePrint],[FTStateHeat],[FTStateLaser],[FTStateWindows] ,[FTRemark],[FNHSysShipPortId]"
                _Qry &= " ,[FTCustRef],[FTPORef], FTStateCombine ,FTOrderNoBombineRef,FNHSysPlantId,FNHSysBuyGrpId,FNOrderSetType)"
                _Qry &= vbCrLf & " Select  N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(OrderNo.ToString) & "'"
                _Qry &= vbCrLf & ",'" & _KeyNew & "'"
                _Qry &= vbCrLf & ",'" & HI.UL.ULDate.ConvertEnDB(Rx!FDSubOrderDate.ToString) & "'"
                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Qry &= vbCrLf & ",'" & HI.UL.ULDate.ConvertEnDB(Rx!FDProDate.ToString) & "'"
                _Qry &= vbCrLf & ",'" & HI.UL.ULDate.ConvertEnDB(Rx!FDShipDate.ToString) & "'"
                _Qry &= vbCrLf & "," & Integer.Parse(Rx!FNHSysContinentId.ToString)
                _Qry &= vbCrLf & "," & Integer.Parse(Rx!FNHSysCountryId.ToString)
                _Qry &= vbCrLf & "," & Integer.Parse(Rx!FNHSysProvinceId.ToString)
                _Qry &= vbCrLf & "," & Integer.Parse(Rx!FNHSysShipModeId.ToString)
                _Qry &= vbCrLf & "," & Integer.Parse(Rx!FNHSysCurId.ToString)
                _Qry &= vbCrLf & "," & Integer.Parse(Rx!FNHSysGenderId.ToString)
                _Qry &= vbCrLf & "," & Integer.Parse(Rx!FNHSysUnitId.ToString)
                _Qry &= vbCrLf & ",'0' , '0','0','0','0'"
                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Rx!FTRemark.ToString) & "'"
                _Qry &= vbCrLf & "," & Integer.Parse(Rx!FNHSysShipPortId.ToString)
                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Rx!FTCustRef.ToString) & "'"
                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Rx!FTPORef.ToString.Trim) & "'"
                _Qry &= vbCrLf & ",'1','" & HI.UL.ULF.rpQuoted(OrderNo.ToString) & "'"
                _Qry &= vbCrLf & "," & Integer.Parse(Rx!FNHSysPlantId.ToString)
                _Qry &= vbCrLf & "," & Integer.Parse(Rx!FNHSysBuyGrpId.ToString)
                _Qry &= vbCrLf & "," & 0


                If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    _Spls.Close()
                    Return False
                End If
            Next


            With CType(Me.ogdColorSizeBreakdown.DataSource, System.Data.DataTable)
                .AcceptChanges()

                For Each R As DataRow In .Select("  FNSeq=2 ")

                    _OrderNo = R!FTOrderNo.ToString
                    _SubOrderNo = R!FTSubOrderNo.ToString
                    _ColorWay = R!FTColorway.ToString

                    For Ix As Integer = .Columns.Count - 1 To 0 Step -1
                        Select Case .Columns(Ix).ColumnName.ToString.ToUpper
                            Case "FNRowId".ToUpper, "FTDescription".ToUpper, "FNTotal".ToUpper, "FNPercent".ToUpper, "FNSeq".ToUpper, "FTPOref".ToUpper, "FTNikePOLineItem".ToUpper, "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FDShipDate".ToUpper, "FTColorway".ToUpper, "FNHSysStyleId".ToUpper, "FTNewNikePOLineItem".ToUpper,
                                   "FTRemark".ToUpper, "FTMainMatCode".ToUpper, "FTColorway".ToUpper, "FTRawMatCode".ToUpper, "FTItemCode".ToUpper, "FDRcvDate".ToUpper, "FTStatePOCancel".ToUpper, "FNRawMatQty".ToUpper
                            Case Else
                                If (Integer.Parse(Val(R.Item(.Columns(Ix).ColumnName.ToString).ToString))) > 0 Then
                                    _Qry = "Update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_BreakDown "
                                    _Qry &= vbCrLf & " Set  FNQuantity = (FNQuantity - " & Integer.Parse(Val(R.Item(.Columns(Ix).ColumnName.ToString).ToString)) & " )"
                                    _Qry &= vbCrLf & ", FNAmt = (FNAmt - ( FNPrice * " & Integer.Parse(Val(R.Item(.Columns(Ix).ColumnName.ToString).ToString)) & "))"
                                    _Qry &= vbCrLf & ", FNGrandQuantity = (FNGrandQuantity -" & Integer.Parse(Val(R.Item(.Columns(Ix).ColumnName.ToString).ToString)) & ") "
                                    _Qry &= vbCrLf & ", FNGrandAmnt = (FNGrandAmnt - ( FNPrice * " & Integer.Parse(Val(R.Item(.Columns(Ix).ColumnName.ToString).ToString)) & "))"
                                    '_Qry &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_BreakDown"
                                    _Qry &= vbCrLf & "Where FTOrderNo='" & HI.UL.ULF.rpQuoted(_OrderNo) & "' "
                                    _Qry &= vbCrLf & " And FTSubOrderNo='" & HI.UL.ULF.rpQuoted(_SubOrderNo) & "' "
                                    _Qry &= vbCrLf & " And FTColorway='" & HI.UL.ULF.rpQuoted(_ColorWay) & "' "
                                    _Qry &= vbCrLf & " And FTSizeBreakDown='" & HI.UL.ULF.rpQuoted(.Columns(Ix).ColumnName.ToString) & "' "

                                    If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                                        HI.Conn.SQLConn.Tran.Rollback()
                                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                                        _Spls.Close()
                                        Return False
                                    End If
                                End If
                        End Select
                    Next
                Next



                For Each R As DataRow In .Select("  FNSeq=2 ")

                    _OrderNo = R!FTOrderNo.ToString
                    _SubOrderNo = R!FTSubOrderNo.ToString
                    _ColorWay = R!FTColorway.ToString
                    _POLine = R!FTNewNikePOLineItem.ToString
                    For Ix As Integer = .Columns.Count - 1 To 0 Step -1
                        Select Case .Columns(Ix).ColumnName.ToString.ToUpper
                            Case "FNRowId".ToUpper, "FTDescription".ToUpper, "FNTotal".ToUpper, "FNPercent".ToUpper, "FNSeq".ToUpper, "FTPOref".ToUpper, "FTNikePOLineItem".ToUpper, "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FDShipDate".ToUpper, "FTColorway".ToUpper, "FNHSysStyleId".ToUpper, "FTNewNikePOLineItem".ToUpper,
                                   "FTRemark".ToUpper, "FTMainMatCode".ToUpper, "FTColorway".ToUpper, "FTRawMatCode".ToUpper, "FTItemCode".ToUpper, "FDRcvDate".ToUpper, "FTStatePOCancel".ToUpper, "FNRawMatQty".ToUpper
                            Case Else
                                If (Integer.Parse(Val(R.Item(.Columns(Ix).ColumnName.ToString).ToString))) > 0 Then
                                    _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_BreakDown"
                                    _Qry &= "(FTInsUser, FDInsDate, FTInsTime,  FTOrderNo, FTSubOrderNo, FTColorway, FTSizeBreakDown, FNPrice, FNQuantity, FNAmt, FNHSysMatColorId, "
                                    _Qry &= "  FNHSysMatSizeId, FNExtraQty, FNQuantityExtra, FNGrandQuantity, FNAmntExtra, FNGrandAmnt, FNGarmentQtyTest, FNAmntQtyTest, FNPriceOrg,  FNCMDisPer, FNCMDisAmt ,FTNikePOLineItem,FNOperateFee,FNOperateFeeAmt,FNNetFOB)"
                                    _Qry &= vbCrLf & "Select TOP 1 N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                                    _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                                    _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(OrderNo.ToString) & "'"
                                    _Qry &= vbCrLf & ",'" & _KeyNew & "'"
                                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_ColorWay) & "'"
                                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(.Columns(Ix).ColumnName.ToString) & "' "
                                    _Qry &= vbCrLf & ",FNPrice"
                                    _Qry &= vbCrLf & "," & Integer.Parse(Val(R.Item(.Columns(Ix).ColumnName.ToString).ToString)) & " "
                                    _Qry &= vbCrLf & ",( FNPrice * " & Integer.Parse(Val(R.Item(.Columns(Ix).ColumnName.ToString).ToString)) & ")"
                                    _Qry &= vbCrLf & ",FNHSysMatColorId,FNHSysMatSizeId,0,0"
                                    _Qry &= vbCrLf & "," & Integer.Parse(Val(R.Item(.Columns(Ix).ColumnName.ToString).ToString)) & " "
                                    _Qry &= vbCrLf & " ,0"
                                    _Qry &= vbCrLf & ",( FNPrice * " & Integer.Parse(Val(R.Item(.Columns(Ix).ColumnName.ToString).ToString)) & ")"
                                    _Qry &= vbCrLf & ",0,0"
                                    _Qry &= vbCrLf & ", FNPriceOrg,  FNCMDisPer, FNCMDisAmt"
                                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_POLine) & "'"
                                    _Qry &= vbCrLf & ",FNOperateFee,FNOperateFeeAmt,FNNetFOB"
                                    _Qry &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_BreakDown"
                                    _Qry &= vbCrLf & "Where FTOrderNo='" & HI.UL.ULF.rpQuoted(_OrderNo) & "' "
                                    ' _Qry &= vbCrLf & " And FTSubOrderNo='" & HI.UL.ULF.rpQuoted(_SubOrderNo) & "' "
                                    _Qry &= vbCrLf & " And FTColorway='" & HI.UL.ULF.rpQuoted(_ColorWay) & "' "
                                    _Qry &= vbCrLf & " And FTSizeBreakDown='" & HI.UL.ULF.rpQuoted(.Columns(Ix).ColumnName.ToString) & "' "

                                    If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                                        HI.Conn.SQLConn.Tran.Rollback()
                                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                                        _Spls.Close()
                                        Return False
                                    End If
                                End If
                        End Select
                    Next
                Next

            End With



            _Qry = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_Component"
            _Qry &= "(FTInsUser, FDInsDate, FTInsTime,FTOrderNo, FTSubOrderNo, FNHSysMerMatId, FNPart, FTComponent, FTRemark, FNConSmp, FNSeq)"
            _Qry &= vbCrLf & "Select N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(OrderNo.ToString) & "'"
            _Qry &= vbCrLf & ",'" & _KeyNew & "'"
            _Qry &= vbCrLf & ", FNHSysMerMatId, FNPart, FTComponent, FTRemark, FNConSmp, FNSeq"
            _Qry &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_Component"
            _Qry &= vbCrLf & " WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(OrderNo.ToString) & "'"
            _Qry &= vbCrLf & " And FTSubOrderNo='" & HI.UL.ULF.rpQuoted(SubOrderNo.ToString) & "'"
            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

            End If

            _Qry = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_Sew"
            _Qry &= "(FTInsUser, FDInsDate, FTInsTime , FTOrderNo, FTSubOrderNo, FNSewSeq, FTSewDescription, FTSewNote, FTImage)"
            _Qry &= vbCrLf & "Select N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(OrderNo.ToString) & "'"
            _Qry &= vbCrLf & ",'" & _KeyNew & "'"
            _Qry &= vbCrLf & ",FNSewSeq, FTSewDescription, FTSewNote, FTImage"
            _Qry &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_Sew"
            _Qry &= vbCrLf & " WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(OrderNo.ToString) & "'"
            _Qry &= vbCrLf & " And FTSubOrderNo='" & HI.UL.ULF.rpQuoted(SubOrderNo.ToString) & "'"
            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

            End If

            _Qry = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_Pack"
            _Qry &= "( FTInsUser, FDInsDate, FTInsTime, FTOrderNo, FTSubOrderNo, FNPackSeq, FTPackDescription, FTPackNote, FTImage)"
            _Qry &= vbCrLf & "Select N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(OrderNo.ToString) & "'"
            _Qry &= vbCrLf & ",'" & _KeyNew & "'"
            _Qry &= vbCrLf & ", FNPackSeq, FTPackDescription, FTPackNote, FTImage"
            _Qry &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_Pack"
            _Qry &= vbCrLf & " WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(OrderNo.ToString) & "'"
            _Qry &= vbCrLf & " And FTSubOrderNo='" & HI.UL.ULF.rpQuoted(SubOrderNo.ToString) & "'"
            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

            End If

            _Qry = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_SizeSpec"
            _Qry &= "( FTInsUser, FDInsDate, FTInsTime,   FTOrderNo, FTSubOrderNo, FNSeq, FNHSysMatSizeId, FTSizeSpecDesc, FTSizeSpecExtension, FTTolerant,FNHSysMeasId)"
            _Qry &= vbCrLf & "Select N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(OrderNo.ToString) & "'"
            _Qry &= vbCrLf & ",'" & _KeyNew & "'"
            _Qry &= vbCrLf & ",  FNSeq, FNHSysMatSizeId, FTSizeSpecDesc, FTSizeSpecExtension, FTTolerant,FNHSysMeasId"
            _Qry &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_SizeSpec"
            _Qry &= vbCrLf & " WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(OrderNo.ToString) & "'"
            _Qry &= vbCrLf & " And FTSubOrderNo='" & HI.UL.ULF.rpQuoted(SubOrderNo.ToString) & "'"
            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

            End If

            SubOrderNoRef = _KeyNew

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            ' HI.Conn.SQLConn.ExecuteOnly(_QryDelete, Conn.DB.DataBaseName.DB_PROD)
            _Spls.Close()
            Return False
        End Try
        ' HI.Conn.SQLConn.ExecuteOnly(_QryDelete, Conn.DB.DataBaseName.DB_PROD)
        _Spls.Close()
        Return True
    End Function

#End Region


#Region "Drivert"

    Private Function PROC_SAVEbDivertSubOrderNo(ByVal paramFTSubOrderNoSrc As String, OrderNo As String) As Boolean
        Dim _Qry As String = ""
        Dim _QryCheckSeq As String = ""
        Dim bRet As Boolean = False
        Dim Maxleng As String = ""
        Dim _NewGacDate As String = ""
        Dim _OrderNo As String = ""
        Dim _SubOrderNo As String = ""
        Dim _ColorWay As String = ""
        'Sub Order No ที่ทำการ Divert มานั้นจะต้องทำการ Copy รายการต่อไปนี้หรือไม่ ? : Component Info/ Sewing Info/ Packing Info/ Packing Carton Info/ Size Spec Info

        Try

            With CType(Me.ogdColorSizeBreakdown.DataSource, System.Data.DataTable)
                .AcceptChanges()
                For Each Rxz As DataRow In .Select(" FNSeq=2 ")
                    _NewGacDate = Rxz!FDShipDate.ToString

                    _QryCheckSeq = "select top 1 Max(FNDivertSeq) AS MaxlengSeq FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub_Divert"
                    _QryCheckSeq &= vbCrLf & "  WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(OrderNo) & "'"
                    _QryCheckSeq &= vbCrLf & "  AND  FTSubOrderNo='" & HI.UL.ULF.rpQuoted(paramFTSubOrderNoSrc) & "'"

                    Maxleng = Integer.Parse(Val(HI.Conn.SQLConn.GetField(_QryCheckSeq, HI.Conn.DB.DataBaseName.DB_MERCHAN, "0"))) + 1
                    _Qry = "  Select top 1 *   From " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.TMERTOrderSub witH(nolock) where FTOrderNo='" & HI.UL.ULF.rpQuoted(OrderNo.ToString) & "'"
                    _Qry &= vbCrLf & " and FTSubOrderNo='" & HI.UL.ULF.rpQuoted(paramFTSubOrderNoSrc.ToString) & "'"
                    For Each Rx As DataRow In HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN).Rows

                        _Qry = "   INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTOrderSub_Divert] ([FTInsUser],[FDInsDate],[FTInsTime]"
                        _Qry &= vbCrLf & "              ,FNDivertSeq"
                        _Qry &= vbCrLf & "              ,[FTOrderNo],[FTSubOrderNo]"
                        _Qry &= vbCrLf & "              ,[FNHSysContinentId],[FNHSysCountryId]"
                        _Qry &= vbCrLf & "              ,[FNHSysProvinceId],[FNHSysShipModeId]"
                        _Qry &= vbCrLf & "              ,[FNHSysShipPortId]"
                        _Qry &= vbCrLf & "              ,FDShipDate"
                        _Qry &= vbCrLf & "              ,[FTRemark],[FTCustRef],[FTPORef],[FNHSysPlantId],[FNHSysBuyGrpId],[FNOrderSetType],FNPackCartonSubType,FNPackPerCarton)"
                        _Qry &= vbCrLf & "   SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "', " & HI.UL.ULDate.FormatDateDB & ", " & HI.UL.ULDate.FormatTimeDB & ", "
                        _Qry &= vbCrLf & " " & Maxleng & " ,"
                        _Qry &= vbCrLf & "   '" & HI.UL.ULF.rpQuoted(OrderNo) & "',"
                        _Qry &= vbCrLf & " '" & HI.UL.ULF.rpQuoted(paramFTSubOrderNoSrc) & "',"
                        _Qry &= vbCrLf & " " & Val(Rx!FNHSysContinentId.ToString) & ", " & Val(Rx!FNHSysCountryId.ToString) & ","
                        _Qry &= vbCrLf & "" & Val(Rx!FNHSysProvinceId.ToString) & "," & Val(Rx!FNHSysShipModeId.ToString) & ","
                        _Qry &= vbCrLf & "" & Val(Rx!FNHSysShipPortId.ToString) & ","
                        _Qry &= vbCrLf & " convert(varchar(10), convert(date, '" & _NewGacDate & "' ) ,111) ,"
                        _Qry &= vbCrLf & "         N'" & HI.UL.ULF.rpQuoted(Rx!FTRemark.ToString) & "', '" & HI.UL.ULF.rpQuoted(Rx!FTCustRef.ToString) & "', '" & HI.UL.ULF.rpQuoted(Rx!FTPORef.ToString.Trim()) & "'"
                        _Qry &= vbCrLf & " ," & Val(Rx!FNHSysPlantId.ToString) & ", " & Val(Rx!FNHSysBuyGrpId.ToString) & "," & Rx!FNOrderSetType.ToString
                        _Qry &= vbCrLf & "," & Val(HI.TL.CboList.GetIndexByText(Rx!FNPackCartonSubType.ToString, Rx!FNPackCartonSubType.ToString)) & "," & Val(Rx!FNPackPerCarton.ToString)

                    Next



                    With CType(Me.ogdColorSizeBreakdown.DataSource, System.Data.DataTable)
                        .AcceptChanges()

                        For Each R As DataRow In .Select("  FNSeq=2  ")

                            _OrderNo = R!FTOrderNo.ToString
                            _SubOrderNo = R!FTSubOrderNo.ToString
                            _ColorWay = R!FTColorway.ToString

                            For Ix As Integer = .Columns.Count - 1 To 0 Step -1
                                Select Case .Columns(Ix).ColumnName.ToString.ToUpper
                                    Case "FNRowId".ToUpper, "FTDescription".ToUpper, "FNTotal".ToUpper, "FNPercent".ToUpper, "FNSeq".ToUpper, "FTPOref".ToUpper, "FTNikePOLineItem".ToUpper, "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FDShipDate".ToUpper, "FTColorway".ToUpper, "FNHSysStyleId".ToUpper, "FTNewNikePOLineItem".ToUpper,
                                           "FTRemark".ToUpper, "FTMainMatCode".ToUpper, "FTColorway".ToUpper, "FTRawMatCode".ToUpper, "FTItemCode".ToUpper, "FDRcvDate".ToUpper, "FTStatePOCancel".ToUpper, "FNRawMatQty".ToUpper
                                    Case Else
                                        If (Integer.Parse(Val(R.Item(.Columns(Ix).ColumnName.ToString).ToString))) > 0 Then


                                            _Qry &= vbCrLf & "   INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTOrderSub_Divert_BreakDown] ([FTInsUser],[FDInsDate],[FTInsTime]"
                                            _Qry &= vbCrLf & ",FNDivertSeq"
                                            _Qry &= vbCrLf & ",[FTOrderNo],[FTSubOrderNo]"
                                            _Qry &= vbCrLf & ",[FTColorway],[FTSizeBreakDown]"
                                            _Qry &= vbCrLf & ",[FNQuantity]"
                                            _Qry &= vbCrLf & ",[FTNikePOLineItem]"
                                            _Qry &= vbCrLf & ",[FTColorwayNew])"
                                            _Qry &= vbCrLf & "   SELECT N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "', " & HI.UL.ULDate.FormatDateDB & ", " & HI.UL.ULDate.FormatTimeDB & ", "
                                            _Qry &= vbCrLf & "" & Maxleng & ","
                                            _Qry &= vbCrLf & "          N'" & HI.UL.ULF.rpQuoted(OrderNo) & "',"
                                            _Qry &= vbCrLf & "'" & HI.UL.ULF.rpQuoted(paramFTSubOrderNoSrc) & "',"
                                            _Qry &= vbCrLf & "   N'" & HI.UL.ULF.rpQuoted(_ColorWay) & "', '" & HI.UL.ULF.rpQuoted(.Columns(Ix).ColumnName.ToString) & "', "
                                            _Qry &= vbCrLf & "   " & Integer.Parse(Val(R.Item(.Columns(Ix).ColumnName.ToString).ToString)) & ""
                                            _Qry &= vbCrLf & ",'" & IIf(HI.UL.ULF.rpQuoted(R!FTNewNikePOLineItem.ToString) = "", HI.UL.ULF.rpQuoted(R!FTNikePOLineItem.ToString), HI.UL.ULF.rpQuoted(R!FTNewNikePOLineItem.ToString)) & "'"
                                            _Qry &= vbCrLf & ",   N'" & HI.UL.ULF.rpQuoted(_ColorWay) & "'"


                                        End If
                                End Select
                            Next
                        Next


                    End With



                    HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_MERCHAN)
                    HI.Conn.SQLConn.SqlConnectionOpen()
                    HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
                    HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

                    If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) > 0 Then

                        _Qry = " UPDATE M SET"
                        _Qry &= vbCrLf & " FNPrice=BB.FNPrice"
                        _Qry &= vbCrLf & " ,FNPriceOrg=BB.FNPriceOrg"
                        _Qry &= vbCrLf & " ,FNCMDisPer=BB.FNCMDisPer"
                        _Qry &= vbCrLf & " ,FNCMDisAmt=BB.FNCMDisAmt"
                        _Qry &= vbCrLf & " ,FNNetPrice=BB.FNNetPrice"
                        _Qry &= vbCrLf & " ,FNOperateFee=BB.FNOperateFee"
                        _Qry &= vbCrLf & " ,FNOperateFeeAmt=BB.FNOperateFeeAmt"
                        _Qry &= vbCrLf & " ,FNNetFOB=BB.FNNetFOB"
                        _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_Divert_BreakDown  AS M INNER JOIN "
                        _Qry &= vbCrLf & " (  SELECT B.FTColorway, B.FTSizeBreakDown"
                        _Qry &= vbCrLf & ", MAX(B.FNPrice) AS FNPrice, MAX(B.FNPriceOrg) AS FNPriceOrg, MAX(B.FNCMDisPer) AS FNCMDisPer"
                        _Qry &= vbCrLf & ", MAX(B.FNCMDisAmt) AS FNCMDisAmt, MAX(B.FNNetPrice)    AS FNNetPrice,MAX(B.FTNikePOLineItem) AS FTNikePOLineItem,MAX(B.FNOperateFee) AS FNOperateFee,MAX(B.FNOperateFeeAmt) AS FNOperateFeeAmt,MAX(B.FNNetFOB) AS FNNetFOB"
                        _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS A WITH(NOLOCK) INNER JOIN"
                        _Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_BreakDown AS B WITH(NOLOCK)  ON A.FTOrderNo = B.FTOrderNo"
                        _Qry &= vbCrLf & "  WHERE A.FTOrderNo='" & HI.UL.ULF.rpQuoted(OrderNo) & "'"
                        _Qry &= vbCrLf & "  AND B.FTSubOrderNo='" & HI.UL.ULF.rpQuoted(paramFTSubOrderNoSrc) & "'"
                        _Qry &= vbCrLf & " GROUP BY B.FTColorway, B.FTSizeBreakDown"
                        _Qry &= vbCrLf & " ) AS BB ON M.FTColorway = BB.FTColorway AND M.FTSizeBreakDown=BB.FTSizeBreakDown"
                        _Qry &= vbCrLf & "  WHERE M.FTOrderNo='" & HI.UL.ULF.rpQuoted(OrderNo) & "'"
                        _Qry &= vbCrLf & "  AND M.FTSubOrderNo='" & HI.UL.ULF.rpQuoted(paramFTSubOrderNoSrc) & "'"
                        _Qry &= vbCrLf & "  AND M.FNDivertSeq=" & Maxleng & ""

                        HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

                        If SaveSubOrder_Component(paramFTSubOrderNoSrc, Maxleng, OrderNo) = False Then

                            HI.Conn.SQLConn.Tran.Rollback()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                            Return False

                        End If


                        If SaveSubOrder_Sew(paramFTSubOrderNoSrc, Maxleng, OrderNo) = False Then

                            HI.Conn.SQLConn.Tran.Rollback()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                            Return False

                        End If


                        If SaveSubOrder_Pack(paramFTSubOrderNoSrc, Maxleng, OrderNo) = False Then

                            HI.Conn.SQLConn.Tran.Rollback()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                            Return False

                        End If

                        If SaveSubOrder_Bundle(paramFTSubOrderNoSrc, Maxleng, OrderNo) = False Then

                            HI.Conn.SQLConn.Tran.Rollback()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                            Return False

                        End If

                        If SaveSubOrder_SizeSpec(paramFTSubOrderNoSrc, Maxleng, OrderNo) = False Then

                            HI.Conn.SQLConn.Tran.Rollback()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                            Return False

                        End If


                        HI.Conn.SQLConn.Tran.Commit()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        bRet = True
                    Else
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    End If

                Next
            End With


        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)


            If System.Diagnostics.Debugger.IsAttached = True Then
                MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.Information + MsgBoxStyle.OkOnly, My.Application.Info.Title.ToString)
            End If

        End Try

        'If bRet = True Then
        '    Call SendMailToProductionStaff(paramFTSubOrderNoSrc)
        'End If

        Return bRet

    End Function

    Private Function SaveSubOrder_Component(ByVal paraFTSubOrderNoSrc As String, ByVal paraMaxleng As Integer, _FTOrderNoSrc As String) As Boolean
        Try
            Dim _Qry As String = ""
            Dim _QryCheckSeq As String = ""
            Dim bRet As Boolean = False
            Dim _Seq As Integer = 0
            Dim _ogc As Object
            Dim Maxleng As Integer
            Dim _Dt As System.Data.DataTable
            Maxleng = paraMaxleng

            _Qry = "Delete From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_Divert_Component"
            _Qry &= vbCrLf & "WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(_FTOrderNoSrc) & "' AND FTSubOrderNo = '" & HI.UL.ULF.rpQuoted(paraFTSubOrderNoSrc) & "' "
            _Qry &= vbCrLf & " AND FNDivertSeq = '" & Val(Maxleng) & "' "
            HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)



            _Qry = "Select *  From " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & " .dbo.TMERTOrderSub_Component with(nolock)"
            _Qry &= vbCrLf & " where  FTOrderNo='" & HI.UL.ULF.rpQuoted(_FTOrderNoSrc) & "' AND FTSubOrderNo = '" & HI.UL.ULF.rpQuoted(paraFTSubOrderNoSrc) & "' "
            For Each R As DataRow In HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN).Select("FNSeq>=0", "FNSeq")
                _Seq = _Seq + 1

                _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_Divert_Component"
                _Qry &= vbCrLf & " (FTInsUser, FDInsDate, FTInsTime,   FTOrderNo, FTSubOrderNo, FNDivertSeq, FNHSysMerMatId"
                _Qry &= vbCrLf & ", FNPart, FTComponent, FTRemark, FNConSmp, FNSeq)"
                _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_FTOrderNoSrc) & "'"
                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(paraFTSubOrderNoSrc) & "'"
                _Qry &= vbCrLf & "," & Maxleng
                _Qry &= vbCrLf & "," & CInt("0" & R!FNHSysMerMatId.ToString)
                _Qry &= vbCrLf & ", 0"
                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTComponent.ToString) & "'"
                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTRemark.ToString) & "'"
                _Qry &= vbCrLf & "," & CDbl("0" & R!FNConSmp.ToString)
                _Qry &= vbCrLf & "," & CInt("0" & R!FNSeq.ToString)

                If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    Return False
                End If

            Next

        Catch ex As Exception
            Return False
        End Try

        Return True
    End Function

    Private Function SaveSubOrder_Sew(ByVal paraFTSubOrderNoSrc As String, ByVal paraMaxleng As Integer, _FTOrderNoSrc As String) As Boolean
        Try
            Dim _Qry As String = ""
            Dim _QryCheckSeq As String = ""
            Dim bRet As Boolean = False
            Dim _Seq As Integer = 0
            Dim _ogc As Object
            Dim Maxleng As Integer = 0
            Dim _Dt As System.Data.DataTable

            Maxleng = paraMaxleng

            _Qry = "Delete From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_Divert_Sew"
            _Qry &= vbCrLf & "WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(_FTOrderNoSrc) & "' AND FTSubOrderNo = '" & HI.UL.ULF.rpQuoted(paraFTSubOrderNoSrc) & "' "
            _Qry &= vbCrLf & " AND FNDivertSeq = '" & Val(Maxleng) & "' "
            HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)


            _Qry = "Select *  From " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.TMERTOrderSub_Sew with(nolock)"
            _Qry &= vbCrLf & " where  FTOrderNo='" & HI.UL.ULF.rpQuoted(_FTOrderNoSrc) & "' AND FTSubOrderNo = '" & HI.UL.ULF.rpQuoted(paraFTSubOrderNoSrc) & "' "

            For Each R As DataRow In HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN).Select("FNSewSeq>=0", "FNSewSeq")

                _Seq += +1

                _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_Divert_Sew"
                _Qry &= vbCrLf & " (FTInsUser, FDInsDate, FTInsTime,   FTOrderNo, FTSubOrderNo, FNDivertSeq,   FNSewSeq, FTSewDescription, FTSewNote, FTImage"
                _Qry &= vbCrLf & ")"
                _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_FTOrderNoSrc) & "'"
                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(paraFTSubOrderNoSrc) & "'"
                _Qry &= vbCrLf & "," & Maxleng
                _Qry &= vbCrLf & "," & CInt("0" & R!FNSewSeq.ToString)
                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTSewDescription.ToString) & "'"
                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTSewNote.ToString) & "'"
                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTImage.ToString) & "'"
                If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                    Return False
                End If

            Next

        Catch ex As Exception
        End Try
        Return True
    End Function

    Private Function SaveSubOrder_Pack(ByVal paraFTSubOrderNoSrc As String, ByVal paraMaxleng As Integer, _FTOrderNoSrc As String) As Boolean
        Try
            Dim _Qry As String = ""
            Dim _QryCheckSeq As String = ""
            Dim bRet As Boolean = False
            Dim _Seq As Integer = 0
            Dim _ogc As Object
            Dim Maxleng As String = ""
            Dim _Dt As System.Data.DataTable
            Maxleng = paraMaxleng


            _Qry = "Delete From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_Divert_Pack"
            _Qry &= vbCrLf & "WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(_FTOrderNoSrc) & "' AND FTSubOrderNo = '" & HI.UL.ULF.rpQuoted(paraFTSubOrderNoSrc) & "' "
            _Qry &= vbCrLf & " AND FNDivertSeq = '" & Val(Maxleng) & "' "
            HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)



            _Qry = "Select *  From " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & " .dbo.TMERTOrderSub_Sew with(nolock)"
            _Qry &= vbCrLf & " where  FTOrderNo='" & HI.UL.ULF.rpQuoted(_FTOrderNoSrc) & "' AND FTSubOrderNo = '" & HI.UL.ULF.rpQuoted(paraFTSubOrderNoSrc) & "' "


            For Each R As DataRow In HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN).Select("FNPackSeq>=0", "FNPackSeq")
                _Seq = _Seq + 1

                _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_Divert_Pack"
                _Qry &= vbCrLf & " (FTInsUser, FDInsDate, FTInsTime,   FTOrderNo, FTSubOrderNo, FNDivertSeq,   FNPackSeq, FTPackDescription, FTPackNote, FTImage"
                _Qry &= vbCrLf & ")"
                _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_FTOrderNoSrc) & "'"
                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(paraFTSubOrderNoSrc) & "'"
                _Qry &= vbCrLf & "," & Maxleng
                _Qry &= vbCrLf & "," & CInt("0" & R!FNPackSeq.ToString)
                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTPackDescription.ToString) & "'"
                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTPackNote.ToString) & "'"
                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTImage.ToString) & "'"
                If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    Return False
                End If


            Next


        Catch ex As Exception
        End Try
        Return True
    End Function

    Private Function SaveSubOrder_Bundle(ByVal paraFTSubOrderNoSrc As String, ByVal paraMaxleng As Integer, _FTOrderNoSrc As String) As Boolean
        Try
            Dim _Qry As String = ""
            Dim _QryCheckSeq As String = ""
            Dim bRet As Boolean = False
            Dim _Seq As Integer = 0
            Dim _ogc As Object
            Dim Maxleng As String = ""
            Dim _Dt As System.Data.DataTable

            Dim oDBdtSizeBreakdown As System.Data.DataTable
            Dim tSql As String
            Maxleng = paraMaxleng
            _Qry = "Delete From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_Divert_Bundle"
            _Qry &= vbCrLf & "WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(_FTOrderNoSrc) & "' AND FTSubOrderNo = '" & HI.UL.ULF.rpQuoted(paraFTSubOrderNoSrc) & "' "
            _Qry &= vbCrLf & " AND FNDivertSeq = '" & Val(Maxleng) & "'"
            HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)





            _Qry = "Select *  From " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & " .dbo.TMERTOrderSub_Bundle with(nolock)"
            _Qry &= vbCrLf & " where  FTOrderNo='" & HI.UL.ULF.rpQuoted(_FTOrderNoSrc) & "' AND FTSubOrderNo = '" & HI.UL.ULF.rpQuoted(paraFTSubOrderNoSrc) & "' "


            For Each R As DataRow In HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN).Select("FNPackSeq>=0", "FNPackSeq")
                _Seq = _Seq + 1

                _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_Divert_Bundle"
                _Qry &= vbCrLf & " (FTInsUser, FDInsDate, FTInsTime,  FTOrderNo, FTSubOrderNo, FTColorway, FTSizeBreakDown, FNQuantity"
                _Qry &= vbCrLf & ")"
                _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_FTOrderNoSrc) & "'"
                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(paraFTSubOrderNoSrc) & "'"
                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTColorway.ToString) & "'"
                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTSizeBreakDown.ToString) & "'"
                _Qry &= vbCrLf & "," & Val(R!FNQuantity.ToString)
                If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    Return False
                End If


            Next







        Catch ex As Exception
        End Try
        Return True
    End Function



    Private Function SaveSubOrder_SizeSpec(ByVal paraFTSubOrderNoSrc As String, ByVal paraMaxleng As Integer, _FTOrderNoSrc As String) As Boolean
        Try
            Dim _Qry As String = ""
            Dim _QryCheckSeq As String = ""
            Dim bRet As Boolean = False
            Dim _Seq As Integer = 0
            Dim _ogc As Object
            Dim Maxleng As String = ""
            Dim _Dt As System.Data.DataTable

            Dim oDBdtSizeBreakdown As System.Data.DataTable
            Dim tSql As String
            Maxleng = paraMaxleng



            tSql = ""
            tSql = "SELECT A.FNHSysMatSizeId, A.FTMatSizeCode, A.FTMatSizeNameEN AS FTMatSizeName"
            tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMatSize AS A WITH(NOLOCK)"
            tSql &= Environment.NewLine & "WHERE  EXISTS (SELECT 'T'"
            tSql &= Environment.NewLine & "               FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub_BreakDown AS L1 WITH(NOLOCK)"
            tSql &= Environment.NewLine & "               WHERE L1.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(_FTOrderNoSrc) & "'"
            tSql &= Environment.NewLine & "                     AND L1.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(paraFTSubOrderNoSrc) & "'"
            tSql &= Environment.NewLine & "                     AND L1.FNHSysMatSizeId = A.FNHSysMatSizeId)"
            tSql &= Environment.NewLine & "ORDER BY A.FNMatSizeSeq ASC;"

            oDBdtSizeBreakdown = HI.Conn.SQLConn.GetDataTable(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN)

            _Qry = "Delete From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_Divert_SizeSpec"
            _Qry &= vbCrLf & "WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(_FTOrderNoSrc) & "' AND FTSubOrderNo = '" & HI.UL.ULF.rpQuoted(paraFTSubOrderNoSrc) & "' "
            _Qry &= vbCrLf & " AND FNDivertSeq = '" & Val(Maxleng) & "' "
            HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)





            _Qry = "Select *  From " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & " .dbo.TMERTOrderSub_Bundle with(nolock)"
            _Qry &= vbCrLf & " where  FTOrderNo='" & HI.UL.ULF.rpQuoted(_FTOrderNoSrc) & "' AND FTSubOrderNo = '" & HI.UL.ULF.rpQuoted(paraFTSubOrderNoSrc) & "' "


            For Each R As DataRow In HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN).Select("FNSeq>=0", "FNSeq")
                _Seq = _Seq + 1

                _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_Divert_SizeSpec"
                _Qry &= vbCrLf & " (FTInsUser, FDInsDate, FTInsTime, FTOrderNo, FTSubOrderNo, FNDivertSeq, FNSeq, FNHSysMatSizeId, FTSizeSpecDesc, FTSizeSpecExtension, FTTolerant, FNHSysMeasId"
                _Qry &= vbCrLf & ")"
                _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_FTOrderNoSrc) & "'"
                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(paraFTSubOrderNoSrc) & "'"
                _Qry &= vbCrLf & "," & Maxleng
                _Qry &= vbCrLf & "," & CInt("0" & R!FNSeq.ToString)
                _Qry &= vbCrLf & "," & Val(R!FNHSysMatSizeId.ToString)
                _Qry &= vbCrLf & ",'" & R!FTSizeSpecDesc.ToString & "'"
                _Qry &= vbCrLf & ",'" & R!FTSizeSpecExtension.ToString & "'"
                _Qry &= vbCrLf & ",'" & R!FTTolerant.ToString & "'"
                _Qry &= vbCrLf & "," & Val(R!FNHSysMeasId.ToString)
                If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    'HI.Conn.SQLConn.Tran.Rollback()
                    'HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    'HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    Return False
                End If



            Next




        Catch ex As Exception
        End Try
        Return True
    End Function

    Private Sub Ocmclear_Click(sender As Object, e As EventArgs) Handles ocmclear.Click
        Try
            Me.FTCustomerPO.Text = ""
            Me.FTNikePOLineItem.Text = ""
            Me.ogc.DataSource = Nothing
            Me.ogdColorSizeBreakdown.DataSource = Nothing
            Me.FTStateApp.Checked = False
            Me.FTStateCancel.Checked = False
            Me.FTStateFactoryApp.Checked = False
            Me.FTStatePartialShip.Checked = False
            Me.FTStateShortShip.Checked = False
            Me.FTStateMngApp.Checked = False
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ocmdelete_Click(sender As Object, e As EventArgs) Handles ocmdelete.Click
        Try
            If Me.FTStateFactoryApp.Checked Then Exit Sub


        Catch ex As Exception

        End Try
    End Sub





    'Private Sub SendMailToProductionStaff(OrderNoKey As String)
    '    Dim _Qry As String = ""
    '    Dim _dt As DataTable
    '    Dim _UserMailTo As String

    '    _Qry = "   SELECT DISTINCT A.FTUserName "
    '    _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEUserLogin AS A WITH(NOLOCK) INNER JOIN"
    '    _Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMTeamGrp AS B WITH(NOLOCK) ON A.FNHSysTeamGrpId = B.FNHSysTeamGrpId INNER JOIN"
    '    _Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEUserLoginPermission AS C WITH(NOLOCK) ON A.FTUserName = C.FTUserName INNER JOIN"
    '    _Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEPermissionCmp AS D WITH(NOLOCK) ON C.FNHSysPermissionID = D.FNHSysPermissionID INNER JOIN"
    '    _Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH(NOLOCK) ON D.FNHSysCmpId = O.FNHSysCmpId INNER JOIN"
    '    _Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEPermission AS SCUT WITH(NOLOCK) ON C.FNHSysPermissionID = SCUT.FNHSysPermissionID"
    '    _Qry &= vbCrLf & " WHERE    (B.FTStateProd = '1') "
    '    _Qry &= vbCrLf & " AND (O.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(OrderNoKey) & "') "
    '    _Qry &= vbCrLf & " AND (SCUT.FTStateStaff = '1')"

    '    _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_SECURITY)

    '    For Each R As DataRow In _dt.Rows
    '        _UserMailTo = R!FTUserName.ToString

    '        If _UserMailTo <> "" Then

    '            Dim tmpsubject As String = ""
    '            Dim tmpmessage As String = ""

    '            tmpsubject = "Divert Factory Order No " & OrderNoKey & " Sub Factory Order No " & FTSubOrderNoSrc.Text
    '            tmpmessage = "Divert Factory Order No " & OrderNoKey & " Sub Factory Order No " & FTSubOrderNoSrc.Text
    '            tmpmessage &= vbCrLf & "Ship Date :" & Me.FDShipDate.Text
    '            tmpmessage &= vbCrLf & "Continent :" & Me.FNHSysContinentId.Text
    '            tmpmessage &= vbCrLf & "Country :" & Me.FNHSysCountryId.Text
    '            tmpmessage &= vbCrLf & "Province :" & Me.FNHSysProvinceId.Text
    '            tmpmessage &= vbCrLf & "Ship Mode :" & Me.FNHSysShipModeId.Text
    '            tmpmessage &= vbCrLf & "Ship Port :" & Me.FNHSysShipPortId.Text
    '            tmpmessage &= vbCrLf & "Note :" & Me.FTRemarkSubOrderNo.Text

    '            If HI.Mail.ClsSendMail.SendMail(HI.ST.UserInfo.UserName, _UserMailTo, tmpsubject, tmpmessage, 9, OrderNoKey & "|" & Me.FTSubOrderNoSrc.Text) Then


    '            End If

    '        End If
    '    Next

    '    _dt.Dispose()
    'End Sub


#End Region

    Private Sub ExitExcel()
        Try

            Dim _GControl As New DevExpress.XtraGrid.GridControl
            Dim _Gcontrol2 As New DevExpress.XtraGrid.GridControl
            Dim _GView As New DevExpress.XtraGrid.Views.Grid.GridView
            ' _GView = DirectCast(DirectCast(xtab.Controls.Find("ogcPlandD" & xtab.Text, True)(0), DevExpress.XtraGrid.GridControl), DevExpress.XtraGrid.Views.Grid.GridView)
            _GControl = Me.ogc
            _Gcontrol2 = Me.ogdColorSizeBreakdown

            Dim CompositeLink As New DevExpress.XtraPrintingLinks.CompositeLink(New PrintingSystem())
            Dim link As New DevExpress.XtraPrinting.PrintableComponentLink()
            link.Component = _GControl
            CompositeLink.Links.Add(link)

            link = New DevExpress.XtraPrinting.PrintableComponentLink()
            link.Component = _Gcontrol2
            CompositeLink.Links.Add(link)

            Dim _GControl3 As New DevExpress.XtraGrid.GridControl
            Dim _Path As String = "C:\ExportDataForm_WISDOM\"


            If Not (Directory.Exists(_Path)) Then
                My.Computer.FileSystem.CreateDirectory(_Path)
            End If

            _Path = _Path & Me.FTCustomerPO.Text & "_" & Me.FTNikePOLineItem.Text & ".xlsx"

            If _Path <> "" Then

                Try
                    If _Path <> "" Then

                        Dim options = New XlsxExportOptions()
                        options.ExportMode = XlsxExportMode.SingleFile

                        CompositeLink.CreateDocument()
                        CompositeLink.ExportToXlsx(_Path, options)

                        Try
                            Process.Start(_Path)
                        Catch ex As Exception
                        End Try
                    End If
                Catch ex As Exception
                End Try

            End If



        Catch ex As Exception

        End Try
    End Sub

    Private Sub Ocmexporttoexcel_Click(sender As Object, e As EventArgs) Handles ocmexporttoexcel.Click
        Try
            If Me.FTCustomerPO.Text <> "" And Me.FTNikePOLineItem.Text <> "" Then
                ' ExitExcel()
                Dim _oDt As System.Data.DataTable
                Dim _oDtD As System.Data.DataTable
                With DirectCast(Me.ogc.DataSource, System.Data.DataTable)
                    .AcceptChanges()
                    _oDt = .Copy

                End With
                With DirectCast(Me.ogdColorSizeBreakdown.DataSource, System.Data.DataTable)
                    .AcceptChanges()
                    _oDtD = .Copy

                End With

                NewExcelNew_Form(_oDt, _oDtD)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ocmadd_Click(sender As Object, e As EventArgs) Handles ocmadd.Click
        Try
            Dim dt As System.Data.DataTable
            Dim dv As DataView
            With DirectCast(Me.ogdColorSizeBreakdown.DataSource, System.Data.DataTable)
                .AcceptChanges()
                If .Rows.Count > 1 Then
                    Dim dr As DataRow
                    dr = .NewRow
                    For Each R As DataRow In .Select("FNSeq=1")
                        dr("FNSeq") = R!FNSeq
                        dr("FTDescription") = R!FTDescription.ToString
                        dr("FTPOref") = R!FTPOref.ToString
                        dr("FTNikePOLineItem") = R!FTNikePOLineItem.ToString
                        dr("FTOrderNo") = R!FTOrderNo.ToString
                        dr("FTSubOrderNo") = R!FTSubOrderNo.ToString
                        dr("FDShipDate") = R!FDShipDate.ToString
                        dr("FTColorway") = R!FTColorway.ToString
                        dr("FNHSysStyleId") = R!FNHSysStyleId.ToString
                        dr("FTNewNikePOLineItem") = R!FTNewNikePOLineItem.ToString

                        For Ix As Integer = .Columns.Count - 1 To 0 Step -1
                            Select Case .Columns(Ix).ColumnName.ToString.ToUpper
                                Case "FNRowId".ToUpper, "FTDescription".ToUpper, "FNTotal".ToUpper, "FNPercent".ToUpper, "FNSeq".ToUpper, "FTPOref".ToUpper, "FTNikePOLineItem".ToUpper, "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FDShipDate".ToUpper, "FTColorway".ToUpper, "FNHSysStyleId".ToUpper, "FTNewNikePOLineItem".ToUpper,
                                       "FTRemark".ToUpper, "FTMainMatCode".ToUpper, "FTColorway".ToUpper, "FTRawMatCode".ToUpper, "FTItemCode".ToUpper, "FDRcvDate".ToUpper, "FTStatePOCancel".ToUpper, "FNRawMatQty".ToUpper
                                Case Else

                                    dr(HI.UL.ULF.rpQuoted(.Columns(Ix).ColumnName.ToString)) = 0

                            End Select
                        Next
                    Next
                    .Rows.Add(dr)
                End If

                .AcceptChanges()



            End With
            With Me.GridView1
                .BeginSort()
                .Columns("FNSeq").SortOrder = ColumnSortOrder.Ascending
                .EndSort()
            End With


        Catch ex As Exception

        End Try
    End Sub

    Private Sub RepositoryItemCalcEdit1_Leave(sender As Object, e As EventArgs) Handles RepositoryItemCalcEdit1.Leave
        Try

            With DirectCast(Me.ogdColorSizeBreakdown.DataSource, System.Data.DataTable)
                .AcceptChanges()
                Dim xTotal As Integer = 0
                For Each R As DataRow In .Rows
                    Select Case R!FNSeq.ToString
                        Case "0"

                        Case "1"
                            xTotal = 0
                            For I As Integer = .Columns.Count - 1 To 0 Step -1
                                Select Case .Columns(I).ColumnName.ToString.ToUpper
                                    Case "FNRowId".ToUpper, "FTDescription".ToUpper, "FNTotal".ToUpper, "FNPercent".ToUpper, "FNSeq".ToUpper, "FTPOref".ToUpper, "FTNikePOLineItem".ToUpper, "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FDShipDate".ToUpper, "FTColorway".ToUpper, "FNHSysStyleId".ToUpper, "FTNewNikePOLineItem".ToUpper,
                                           "FTRemark".ToUpper, "FTMainMatCode".ToUpper, "FTColorway".ToUpper, "FTRawMatCode".ToUpper, "FTItemCode".ToUpper, "FDRcvDate".ToUpper, "FTStatePOCancel".ToUpper, "FNRawMatQty".ToUpper
                                    Case Else
                                        xTotal += +Integer.Parse(Val(R.Item(.Columns(I).ColumnName.ToString).ToString))
                                End Select

                            Next
                            For I As Integer = .Columns.Count - 1 To 0 Step -1
                                Select Case .Columns(I).ColumnName.ToString.ToUpper
                                    Case "FNTotal".ToUpper
                                        R!FNTotal = xTotal
                                    Case Else
                                End Select

                            Next

                        Case "2"

                        Case "3"
                    End Select

                Next


                Dim PoQty As Integer = 0 : Dim _ShipQty As Integer = 0
                xTotal = 0
                For I As Integer = .Columns.Count - 1 To 0 Step -1
                    Select Case .Columns(I).ColumnName.ToString.ToUpper
                        Case "FNRowId".ToUpper, "FTDescription".ToUpper, "FNPercent".ToUpper, "FNSeq".ToUpper, "FTPOref".ToUpper, "FTNikePOLineItem".ToUpper, "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FDShipDate".ToUpper, "FTColorway".ToUpper, "FNHSysStyleId".ToUpper, "FTNewNikePOLineItem".ToUpper,
                             "FTRemark".ToUpper, "FTMainMatCode".ToUpper, "FTColorway".ToUpper, "FTRawMatCode".ToUpper, "FTItemCode".ToUpper, "FDRcvDate".ToUpper, "FTStatePOCancel".ToUpper, "FNRawMatQty".ToUpper

                        Case Else
                            PoQty = 0 : _ShipQty = 0
                            For Each R As DataRow In .Select("FNSeq>=0", "FNSeq asc ,FNRowId asc ")

                                Select Case R!FNSeq.ToString
                                    Case "0"
                                        PoQty = Integer.Parse(Val(R.Item(.Columns(I).ColumnName.ToString).ToString))
                                    Case "1"

                                        _ShipQty += +Integer.Parse(Val(R.Item(.Columns(I).ColumnName.ToString).ToString))
                                    Case "2"
                                        xTotal = PoQty - _ShipQty
                                        R.Item(.Columns(I).ColumnName.ToString) = IIf(xTotal < 0, 0, xTotal)
                                    Case "3"
                                        R.Item(.Columns(I).ColumnName.ToString) = (xTotal / PoQty) * 100
                                End Select
                            Next
                    End Select

                Next


                .AcceptChanges()
            End With



        Catch ex As Exception

        End Try
    End Sub



    Private _FileName As String = ""
    Private Sub NewExcelNew_Form(ByVal _oDt As System.Data.DataTable, _oDtDetail As System.Data.DataTable)
        Try

            Dim _Qry As String = ""
            Dim _DateNow As String = HI.Conn.SQLConn.GetField(HI.UL.ULDate.FormatDateDB, Conn.DB.DataBaseName.DB_PLANNING, "")
            Dim _Rec As Integer = 0
            Dim _RecError As Integer = 0
            Dim _TotalRec As Integer = 0
            Dim _l As Integer = 0
            Dim _SumPQtyDay As Double = 0
            Dim _SumUQtyDay As Double = 0
            Dim misValue As Object = System.Reflection.Missing.Value
            Dim _Path As String = System.Windows.Forms.Application.StartupPath.ToString
            Dim TmpFile As String = _Path & "\Reports\BDPartialShipmentTemp.xlsx"
            Dim BakFile As String = _Path & "\Reports\Blank.xlsx"
            Dim oExcel As New Microsoft.Office.Interop.Excel.Application
            Dim xlBookTmp As Workbook

            Dim Excel As New Microsoft.Office.Interop.Excel.XlDirection

            xlBookTmp = oExcel.Workbooks.Open(TmpFile)


            xlBookTmp.Worksheets(1).copy(After:=xlBookTmp.Worksheets(1))


            With xlBookTmp.Worksheets(1)

                Select Case True
                    Case Me.FTStateShortShip.Checked
                        .Cells(7, 3) = "X"
                    Case Me.FTStatePartialShip.Checked
                        .Cells(7, 7) = "X"
                End Select

            End With

            ' header
            Dim rowInd As Integer = 10
            With xlBookTmp.Worksheets(1)
                If _oDt.Rows.Count > 0 Then
                    For Each R As DataRow In _oDt.Rows
                        .Cells(rowInd, 2) = R!FTCmpCode.ToString
                        .Cells(rowInd, 3) = R!FTPOref.ToString
                        .Cells(rowInd, 4) = R!FTNikePOLineItem.ToString
                        .Cells(rowInd, 5) = R!FTStyleCode.ToString
                        .Cells(rowInd, 6) = ""
                        .Cells(rowInd, 7) = R!FDShipDateOrginal.ToString
                        .Cells(rowInd, 8) = R!FDShipDate.ToString
                        .Cells(rowInd, 9) = ""
                        .Cells(rowInd, 10) = ""
                        .Cells(rowInd, 11) = R!FTPlantCode.ToString
                        .Cells(rowInd, 12) = R!FTShipModeCode.ToString
                        .Cells(rowInd, 13) = R!FTProdTypeNameEN.ToString
                        .Cells(rowInd, 14) = ""
                        .Cells(rowInd, 15) = ""
                        .Cells(rowInd, 16) = R!FTSeasonCode.ToString
                        .Cells(rowInd, 17) = ""
                        .Cells(rowInd, 18) = ""
                        .Cells(rowInd, 19) = "#"
                        .Cells(rowInd, 20) = R!FTCountryNameEN.ToString
                        .Cells(rowInd, 21) = R!FNGrandQuantity.ToString

                        Exit For
                    Next



                End If


            End With


            rowInd = 13
            Dim _RSeq As Integer = 0
            With xlBookTmp.Worksheets(1)
                If _oDt.Rows.Count > 0 Then
                    For Each R As DataRow In _oDtDetail.Select("FNSeq <=2", "FNSeq asc  , FNRowId asc ")
                        If Val(R!FNSeq.ToString) = _RSeq And Val(R!FNSeq.ToString) <> 0 Then
                            .Rows(rowInd).Insert()
                            .Cells(rowInd, 2) = "SHIPABLE QTY SHIP "
                            .Cells(rowInd, 16).Formula = "=SUM(D" & rowInd & ",E" & rowInd & ",F" & rowInd & ",G" & rowInd & ",H" & rowInd & ",I" & rowInd & ",J" & rowInd & ",K" & rowInd & ",L" & rowInd & ",M" & rowInd & ",N" & rowInd & ",O" & rowInd & ")"
                        End If
                        Dim xcolumn As Integer = 4

                        For Ix As Integer = 0 To _oDtDetail.Columns.Count - 1
                            Select Case _oDtDetail.Columns(Ix).ColumnName.ToString.ToUpper
                                Case "FTNewNikePOLineItem".ToUpper, "FNRowId".ToUpper, "FTDescription".ToUpper, "FNTotal".ToUpper, "FNPercent".ToUpper, "FNSeq".ToUpper, "FTPOref".ToUpper, "FTNikePOLineItem".ToUpper, "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FDShipDate".ToUpper, "FTColorway".ToUpper, "FNHSysStyleId".ToUpper,
                                         "FTRemark".ToUpper, "FTMainMatCode".ToUpper, "FTColorway".ToUpper, "FTRawMatCode".ToUpper, "FTItemCode".ToUpper, "FDRcvDate".ToUpper, "FTStatePOCancel".ToUpper, "FNRawMatQty".ToUpper
                                Case Else

                                    If rowInd = 13 Then
                                        .Cells(rowInd - 1, xcolumn) = HI.UL.ULF.rpQuoted(_oDtDetail.Columns(Ix).ColumnName.ToString)
                                        .Cells(rowInd, xcolumn) = Integer.Parse(Val(R.Item(_oDtDetail.Columns(Ix).ColumnName.ToString).ToString))
                                        .Cells(rowInd, 17) = "'" & R.Item("FDShipDate").ToString
                                        .Cells(rowInd, 18) = R.Item("FTMainMatCode").ToString
                                        .Cells(rowInd, 19) = R.Item("FTRawMatCode").ToString
                                        .Cells(rowInd, 20) = R.Item("FTItemCode").ToString
                                        .Cells(rowInd, 21) = R.Item("FTColorway").ToString
                                        .Cells(rowInd, 22) = R.Item("FNRawMatQty").ToString
                                        .Cells(rowInd, 23) = R.Item("FDRcvDate").ToString
                                        .Cells(rowInd, 24) = R.Item("FTRemark").ToString
                                    Else
                                        .Cells(rowInd, xcolumn) = Integer.Parse(Val(R.Item(_oDtDetail.Columns(Ix).ColumnName.ToString).ToString))
                                        .Cells(rowInd, 17) = "'" & R.Item("FDShipDate").ToString
                                        .Cells(rowInd, 18) = R.Item("FTMainMatCode").ToString
                                        .Cells(rowInd, 19) = R.Item("FTRawMatCode").ToString
                                        .Cells(rowInd, 20) = R.Item("FTItemCode").ToString
                                        .Cells(rowInd, 21) = R.Item("FTColorway").ToString
                                        .Cells(rowInd, 22) = R.Item("FNRawMatQty").ToString
                                        .Cells(rowInd, 23) = R.Item("FDRcvDate").ToString
                                        .Cells(rowInd, 24) = R.Item("FTRemark").ToString
                                    End If

                                    xcolumn += +1

                            End Select
                        Next
                        _RSeq = Val(R!FNSeq.ToString)
                        rowInd += +1
                    Next



                End If


            End With



            'Dim i As Integer = 13

            'For Each R As DataRow In _oDt.Rows
            '    i += +1

            '    With xlBookTmp.Worksheets(1)
            '        'Date
            '        .Rows(CStr(i) & ":" & CStr(i)).Insert(Shift:=XlDirection.xlDown)
            '        .Cells(i, 1).Font.Color = 0
            '        .Cells(i, 1) = "'" & R!FNSeq.ToString
            '        .Cells(i, 2).Font.Color = 0
            '        .Cells(i, 2) = "'" & R!Name.ToString
            '        .Cells(i, 3).Font.Color = 0
            '        .Cells(i, 3) = "'" & R!FTAccNo.ToString
            '        '.Cells(i, 4).Font.Color = 0
            '        '.Cells(i, 4) = "'" & R!FTEmpIdNo.ToString
            '        .Cells(i, 4).Font.Color = 0
            '        .Cells(i, 4) = R!FNNetpay.ToString
            '        .Cells(i, 4).NumberFormat = "#,###,###"
            '    End With
            'Next
            'i += +2
            'With xlBookTmp.Worksheets(1)
            '    .Cells(i, 4) = "=SUM(D14:E" & 14 + _oDt.Rows.Count & ")"
            'End With

            'xlBookTmp.Worksheets(1).Select()
            Try
                If oExcel.Application.Sheets.Count > 1 Then
                    For xi As Integer = oExcel.Application.Sheets.Count To 2 Step -1
                        Try
                            CType(oExcel.Application.ActiveWorkbook.Sheets(xi), Worksheet).Delete()
                            oExcel.Application.DisplayAlerts = False
                        Catch ex As Exception
                        End Try
                        Try
                            oExcel.Sheets(xi).delete()
                            oExcel.Application.DisplayAlerts = True
                        Catch ex As Exception
                        End Try
                    Next
                End If
            Catch ex As Exception
            End Try

            Try
                CType(oExcel.Application.ActiveWorkbook.Sheets(1), Worksheet).Select()
            Catch ex As Exception
            End Try

            oExcel.DisplayAlerts = False
            '_FileName = "C:\Users\NOH-NB\Desktop\TestFile.xlsx"
            Dim _Paths As String = "C:\ExportDataForm_WISDOM\"

            If Not (Directory.Exists(_Paths)) Then
                My.Computer.FileSystem.CreateDirectory(_Paths)
            End If

            _FileName = _Paths & Me.FTCustomerPO.Text & "_" & Me.FTNikePOLineItem.Text & ".xlsx"

            xlBookTmp.SaveAs(_FileName)
            xlBookTmp.Close()

            Process.Start(_FileName)
        Catch ex As Exception

            HI.MG.ShowMsg.mProcessError(1505029917, "Writing Excel File Error....." & vbCrLf & ex.Message.ToString, Me.Text, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Ocmremove_Click(sender As Object, e As EventArgs) Handles ocmremove.Click
        Try
            Dim dt As System.Data.DataTable
            Dim dv As DataView
            If Me.FTStateFactoryApp.Checked Then
                HI.MG.ShowMsg.mInfo("ไม่สามารถแก้ไขข้อมูลได้ เนื่องจากมีการอนุมัติรายการแล้ว กรุณาตรวจสอบ !!!!!", 1909251101, Me.Text)
                Exit Sub
            End If
            With Me.GridView1
                If .GetRowCellValue(.FocusedRowHandle, "FNSeq").ToString = "1" Then
                    If HI.MG.ShowMsg.mConfirmProcess("คุณต้องการลบรายการใช่หรือไม่ ?", 1910071005) = False Then
                        Exit Sub
                    End If
                    .DeleteRow(.FocusedRowHandle)
                Else
                    MG.ShowMsg.mInfo("กรุณเลือกรายที่ต้องการลบ", 1910071006, Me.Text)
                    Exit Sub
                End If
            End With

            With Me.GridView1
                .BeginSort()
                .Columns("FNSeq").SortOrder = ColumnSortOrder.Ascending
                .EndSort()
            End With


        Catch ex As Exception

        End Try
    End Sub


End Class
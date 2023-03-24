Imports DevExpress.Data
Imports System.IO
Imports DevExpress.XtraGrid.Columns

Public Class wProdOrderTracking

    Private StateCal As Boolean = False
    Private _RowDataChange As Boolean
    Private _FTStateProdSMKToCutQty As Boolean

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

#Region "Initial Grid"
    Private Sub InitGrid()
        '------Start Add Summary Grid-------------
        Dim sFieldCount As String = ""
        Dim sFieldSum As String = "FNQuantity|FNQuantityExtra|FNGarmentQtyTest|FNGrandQuantity|FNCutQuantity|FNSewQuantity|FNPackQuantity|FNSendSuplQuantity|FNRcvSuplQuantity|FNSPMKQuantity|FNSewOutQuantity|FNBalCutQuantity|FNBalSuplQuantity|FNBalSewQuantity|FNBalPackQuantity|FNCutBalQuantity"
        sFieldSum &= "|FNQtyEmbroidery|FNRcvQtyEmbroidery|FNBalQtyEmbroidery|FNQtyPrint|FNRcvQtyPrint|FNBalQtyPrint|FNQtyHeat|FNRcvQtyHeat|FNBalQtyHeat|FNQtyLaser|FNRcvQtyLaser|FNBalQtyLaser|FNQtyPadPrint|FNRcvQtyPadPrint|FNBalQtyPadPrint|FNQtyWindow|FNRcvQtyWindow|FNBalQtyWindow"

        Dim sFieldGrpCount As String = ""
        Dim sFieldGrpSum As String = "FNQuantity|FNQuantityExtra|FNGarmentQtyTest|FNGrandQuantity|FNCutQuantity|FNSewQuantity|FNPackQuantity|FNSendSuplQuantity|FNRcvSuplQuantity|FNSPMKQuantity|FNSewOutQuantity|FNBalCutQuantity|FNBalSuplQuantity|FNBalSewQuantity|FNBalPackQuantity|FNCutBalQuantity"
        sFieldGrpSum &= "|FNQtyEmbroidery|FNRcvQtyEmbroidery|FNBalQtyEmbroidery|FNQtyPrint|FNRcvQtyPrint|FNBalQtyPrint|FNQtyHeat|FNRcvQtyHeat|FNBalQtyHeat|FNQtyLaser|FNRcvQtyLaser|FNBalQtyLaser|FNQtyPadPrint|FNRcvQtyPadPrint|FNBalQtyPadPrint|FNQtyWindow|FNRcvQtyWindow|FNBalQtyWindow"

        Dim sFieldCustomSum As String = ""
        Dim sFieldCustomGrpSum As String = ""

        Try
            With ogvtime
                .ClearGrouping()
                .ClearDocument()
                '.Columns("FTDateTrans").Group()

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

                .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
                .OptionsView.ShowFooter = True
                .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleIfExpanded

                .ExpandAllGroups()
                .RefreshData()

            End With

        Catch ex As Exception

        End Try

        Try
            With ogvdetailcolorsize
                .ClearGrouping()
                .ClearDocument()
                '.Columns("FTDateTrans").Group()

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

                .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
                .OptionsView.ShowFooter = True
                .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleIfExpanded

                .ExpandAllGroups()
                .RefreshData()

            End With
        Catch ex As Exception

        End Try

        '------End Add Summary Grid-------------
    End Sub
#End Region

#Region "Custom summaries"

    Private totalSum As Integer = 0
    Private GrpSum As Integer = 0

    Private Sub InitStartValue()
        totalSum = 0
        GrpSum = 0
    End Sub

    'Private Sub ogv_CustomDrawGroupRow(sender As Object, e As DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventArgs) Handles ogv.CustomDrawGroupRow

    '    Dim info As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridGroupRowInfo = e.Info
    '    Dim Handle As Integer = ogv.GetDataRowHandleByGroupRowHandle(e.RowHandle)

    '    'Select Case info.Column.FieldName.ToString.ToUpper
    '    '    Case "FNWorkingDay"

    '    Dim GrpDisplayText As String = ogv.GetGroupSummaryText(e.RowHandle)  'ogv.GetGroupRowValue(e.RowHandle, info.Column)
    '    Dim GrpDisplayTextReplace As String = Nothing
    '    Dim GrpDisplayTextReplaceNew As String = Nothing
    '    GrpDisplayTextReplace = GrpDisplayText.Split(")")(1)

    '    If GrpDisplayTextReplace <> "" Then
    '        If GrpDisplayTextReplace.Split("=").Length >= 2 Then
    '            Dim Title1 As String = GrpDisplayTextReplace.Split("=")(0)
    '            Dim Title2 As String = GrpDisplayTextReplace.Split("=")(1)

    '            If IsNumeric(Title2) = False Then
    '                Title2 = "0"
    '            End If
    '            Dim _Sum As Integer = CDbl(Title2)
    '            Dim NetDisplay As String = ""
    '            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
    '                NetDisplay = Format((_Sum \ 480), "00") & " วัน : " & Format(((_Sum Mod 480) \ 60), "00") & " ชั่วโมง : " & Format(((_Sum Mod 480) Mod 60), "00") & " นาที"
    '            Else
    '                NetDisplay = Format((_Sum \ 480), "00") & " Day : " & Format(((_Sum Mod 480) \ 60), "00") & " Hour : " & Format(((_Sum Mod 480) Mod 60), "00") & " Minute"
    '            End If

    '            GrpDisplayTextReplaceNew = Title1 & "=" & NetDisplay
    '            GrpDisplayText = GrpDisplayText.Replace(GrpDisplayTextReplace, GrpDisplayTextReplaceNew)
    '        End If


    '    info.GroupText = info.Column.Caption + ":" + info.GroupValueText + ""
    '    info.GroupText += "" + GrpDisplayText + ""

    '    'End Select

    'End Sub

#End Region

#Region "Property"

    Private _CallMenuName As String = ""
    Public Property CallMenuName As String
        Get
            Return _CallMenuName
        End Get
        Set(value As String)
            _CallMenuName = value
        End Set
    End Property

    Private _CallMethodName As String = ""
    Public Property CallMethodName As String
        Get
            Return _CallMethodName
        End Get
        Set(value As String)
            _CallMethodName = value
        End Set
    End Property

    Private _CallMethodParm As String = ""
    Public Property CallMethodParm As String
        Get
            Return _CallMethodParm
        End Get
        Set(value As String)
            _CallMethodParm = value
        End Set
    End Property

    Private _ActualDate As String = ""
    ReadOnly Property ActualDate As String
        Get
            Return _ActualDate
        End Get
    End Property

    Private _ActualNextDate As String = ""
    ReadOnly Property ActualNextDate As String
        Get
            Return _ActualNextDate
        End Get
    End Property


#End Region

#Region "Procedure"

    Private Sub LoadData()

        ogdtime.DataSource = Nothing
        ogcdetailcolorsize.DataSource = Nothing

        Dim _StartDate As String = ""
        Dim _EndDate As String = ""
        Dim _Qry As String = ""
        Dim _dt As DataTable
        Dim _TotalRow As Integer = 0
        Dim _Rx As Integer = 0

        StateCal = False

        Dim _Spls As New HI.TL.SplashScreen("Loading...   Please Wait   ")
        Try
            _Qry = " SELECT   ST.FTStyleCode,A.FTOrderNo ,A.FTPORef"
            _Qry &= vbCrLf & ",Cmp.FTCmpCode"

            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Qry &= vbCrLf & " ,Cmp.FTCmpNameTH AS FTCmpName"
            Else
                _Qry &= vbCrLf & " ,Cmp.FTCmpNameEN AS FTCmpName"
            End If

            _Qry &= vbCrLf & " ,ISNULL((SELECT  Convert(Datetime,MIN(FDShipDate)) AS FDShipDate  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub AS SS WITH(NOLOCK) WHERE FTOrderNo=A.FTOrderNo),null) AS FDShipDate"
            _Qry &= vbCrLf & ",ISNULL(B.FNQuantity,0) AS FNQuantity "
            _Qry &= vbCrLf & ",ISNULL(B.FNQuantityExtra,0) AS FNQuantityExtra"
            _Qry &= vbCrLf & ",ISNULL(B.FNGarmentQtyTest,0) AS FNGarmentQtyTest"
            _Qry &= vbCrLf & ",ISNULL(B.FNGrandQuantity,0) As FNGrandQuantity"

            '--------------------------ยอด Super Market มาแสดงเป็นยอดตัด-------------------------
            If (_FTStateProdSMKToCutQty) Then
                _Qry &= vbCrLf & ",ISNULL(SPMK.FNQuantity,0) AS FNCutQuantity"
                _Qry &= vbCrLf & ",(ISNULL(B.FNGrandQuantity,0) -ISNULL(SPMK.FNQuantity,0) )  As FNCutBalQuantity"

                _Qry &= vbCrLf & ",ISNULL(SPMK.FNQuantity,0) AS FNSPMKQuantity"
                _Qry &= vbCrLf & ",(ISNULL(B.FNGrandQuantity,0) -ISNULL(SPMK.FNQuantity,0) )  As FNBalCutQuantity"

            Else
                _Qry &= vbCrLf & ",ISNULL(C.FNQuantity,0) As FNCutQuantity"
                _Qry &= vbCrLf & ",(ISNULL(B.FNGrandQuantity,0) -ISNULL(C.FNQuantity,0) )  As FNCutBalQuantity"

                _Qry &= vbCrLf & ",ISNULL(SPMK.FNQuantity,0) AS FNSPMKQuantity"
                _Qry &= vbCrLf & ",(ISNULL(C.FNQuantity,0) -ISNULL(SPMK.FNQuantity,0) )  As FNBalCutQuantity"

            End If
            '--------------------------ยอด Super Market มาแสดงเป็นยอดตัด-------------------------

            _Qry &= vbCrLf & ",ISNULL(SendSupl.FNQuantity,0) AS FNSendSuplQuantity"
            _Qry &= vbCrLf & ",ISNULL(RcvSupl.FNQuantity,0) AS FNRcvSuplQuantity"
            _Qry &= vbCrLf & ",(ISNULL(SendSupl.FNQuantity,0) - ISNULL(RcvSupl.FNQuantity,0)) AS FNBalSuplQuantity"


            _Qry &= vbCrLf & ",ISNULL(SendSupl.FNQtyEmbroidery,0) AS FNQtyEmbroidery"
            _Qry &= vbCrLf & ",ISNULL(RcvSupl.FNRcvQtyEmbroidery,0) AS FNRcvQtyEmbroidery"
            _Qry &= vbCrLf & ",(ISNULL(SendSupl.FNQtyEmbroidery,0) - ISNULL(RcvSupl.FNRcvQtyEmbroidery,0)) AS FNBalQtyEmbroidery"

            _Qry &= vbCrLf & ",ISNULL(SendSupl.FNQtyPrint,0) AS FNQtyPrint"
            _Qry &= vbCrLf & ",ISNULL(RcvSupl.FNRcvQtyPrint,0) AS FNRcvQtyPrint"
            _Qry &= vbCrLf & ",(ISNULL(SendSupl.FNQtyPrint,0) - ISNULL(RcvSupl.FNRcvQtyPrint,0)) AS FNBalQtyPrint"

            _Qry &= vbCrLf & ",ISNULL(SendSupl.FNQtyHeat,0) AS FNQtyHeat"
            _Qry &= vbCrLf & ",ISNULL(RcvSupl.FNRcvQtyHeat,0) AS FNRcvQtyHeat"
            _Qry &= vbCrLf & ",(ISNULL(SendSupl.FNQtyHeat,0) - ISNULL(RcvSupl.FNRcvQtyHeat,0)) AS FNBalQtyHeat"

            _Qry &= vbCrLf & ",ISNULL(SendSupl.FNQtyLaser,0) AS FNQtyLaser"
            _Qry &= vbCrLf & ",ISNULL(RcvSupl.FNRcvQtyLaser,0) AS FNRcvQtyLaser"
            _Qry &= vbCrLf & ",(ISNULL(SendSupl.FNQtyLaser,0) - ISNULL(RcvSupl.FNRcvQtyLaser,0)) AS FNBalQtyLaser"

            _Qry &= vbCrLf & ",ISNULL(SendSupl.FNQtyPadPrint,0) AS FNQtyPadPrint"
            _Qry &= vbCrLf & ",ISNULL(RcvSupl.FNRcvQtyPadPrint,0) AS FNRcvQtyPadPrint"
            _Qry &= vbCrLf & ",(ISNULL(SendSupl.FNQtyPadPrint,0) - ISNULL(RcvSupl.FNRcvQtyPadPrint,0)) AS FNBalQtyPadPrint"

            _Qry &= vbCrLf & ",ISNULL(SendSupl.FNQtyWindow,0) AS FNQtyWindow"
            _Qry &= vbCrLf & ",ISNULL(RcvSupl.FNRcvQtyWindow,0) AS FNRcvQtyWindow"
            _Qry &= vbCrLf & ",(ISNULL(SendSupl.FNQtyWindow,0) - ISNULL(RcvSupl.FNRcvQtyWindow,0)) AS FNBalQtyWindow"



            _Qry &= vbCrLf & ",ISNULL(D.FNQuantity,0) AS FNSewQuantity"
            _Qry &= vbCrLf & ",ISNULL(S.FNScanQuantity,0) AS FNSewOutQuantity"
            _Qry &= vbCrLf & ",(ISNULL(D.FNQuantity,0)-ISNULL(S.FNScanQuantity,0)) AS FNBalSewQuantity"

            _Qry &= vbCrLf & ",ISNULL(PAC.FNQuantity,0) AS FNPackQuantity"
            _Qry &= vbCrLf & ",(ISNULL(S.FNScanQuantity,0)-ISNULL(PAC.FNQuantity,0) ) AS FNBalPackQuantity"

            _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS A WITH(NOLOCK) INNER JOIN "
            _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "]..TMERMStyle AS ST WITH(NOLOCK) ON A.FNHSysStyleId=ST.FNHSysStyleId INNER jOIN"
            _Qry &= vbCrLf & "  ("
            _Qry &= vbCrLf & "	SELECT FTOrderNo, SUM(FNQuantity) AS FNQuantity, SUM(FNQuantityExtra) AS FNQuantityExtra, SUM(FNGarmentQtyTest) AS FNGarmentQtyTest, SUM(FNGrandQuantity) AS FNGrandQuantity"
            _Qry &= vbCrLf & "FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_BreakDown AS SBD WITH(NOLOCK)"
            _Qry &= vbCrLf & "	GROUP BY FTOrderNo"
            _Qry &= vbCrLf & "  ) AS B ON A.FTOrderNo = B.FTOrderNo  "
            _Qry &= vbCrLf & "  LEFT OUTER JOIN ("
            _Qry &= vbCrLf & " SELECT A.FTOrderNo, SUM(B.FNQuantity) AS FNQuantity"
            _Qry &= vbCrLf & "  FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.V_TPRODTBundle_MainBarcode AS B WITH(NOLOCK)  INNER JOIN"
            _Qry &= vbCrLf & "           [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd AS A  WITH(NOLOCK) ON B.FTOrderProdNo = A.FTOrderProdNo "
            '_Qry &= vbCrLf & "           [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.V_TPRODTBundle_MainBarcode_Detail AS BD WITH(NOLOCK)  ON B.FTBarcodeBundleNo = BD.FTBarcodeBundleNo INNER JOIN"
            '_Qry &= vbCrLf & "           [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTLayCut AS LC WITH(NOLOCK)  ON BD.FTLayCutNo = LC.FTLayCutNo INNER JOIN"
            '_Qry &= vbCrLf & "           [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_MarkMain AS PM WITH(NOLOCK)  ON LC.FTOrderProdNo = PM.FTOrderProdNo AND LC.FNHSysMarkId = PM.FNHSysMarkId"
            _Qry &= vbCrLf & " WHERE  (B.FTStateGenBarcode = '1')"
            _Qry &= vbCrLf & "  GROUP BY A.FTOrderNo"
            _Qry &= vbCrLf & "  ) AS C ON A.FTOrderNo = C.FTOrderNo "
            _Qry &= vbCrLf & "   LEFT OUTER JOIN ("
            _Qry &= vbCrLf & "	SELECT P.FTOrderNo, SUM(B.FNQuantity) AS FNQuantity"
            _Qry &= vbCrLf & "	FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScan_Detail AS A WITH(NOLOCK)  INNER JOIN"
            _Qry &= vbCrLf & "		     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS US WITH(NOLOCK) ON A.FNHSysUnitSectId = US.FNHSysUnitSectId INNER JOIN"
            _Qry &= vbCrLf & "		     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.V_TPRODTBundle_MainBarcode AS B WITH(NOLOCK) ON A.FTBarcodeNo = B.FTBarcodeBundleNo INNER JOIN"
            _Qry &= vbCrLf & "		     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd AS P WITH(NOLOCK) ON B.FTOrderProdNo = P.FTOrderProdNo"
            _Qry &= vbCrLf & "	WHERE  (US.FTStateSew = '1') AND (A.FNHSysOperationId = 1405310010)  "
            _Qry &= vbCrLf & "	GROUP BY P.FTOrderNo"
            _Qry &= vbCrLf & "  ) AS D ON A.FTOrderNo = D.FTOrderNo "
            _Qry &= vbCrLf & "  LEFT OUTER JOIN ("
            '_Qry &= vbCrLf & "	SELECT FTOrderNo, SUM(FNScanQuantity) AS FNScanQuantity"
            '_Qry &= vbCrLf & "  FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan AS SC"
            '_Qry &= vbCrLf & "	GROUP BY FTOrderNo"

            '_Qry &= vbCrLf & "   SELECT P.FTOrderNo, SUM(SC.FNQuantity) AS FNScanQuantity"
            '_Qry &= vbCrLf & "  FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScanOutline AS SC WITH(NOLOCK)"
            '_Qry &= vbCrLf & " LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS US WITH(NOLOCK) ON SC.FNHSysUnitSectId = US.FNHSysUnitSectId "
            '_Qry &= vbCrLf & " LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle AS B WITH(NOLOCK) ON SC.FTBarcodeNo = B.FTBarcodeBundleNo"
            '_Qry &= vbCrLf & " INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd AS P WITH(NOLOCK) ON B.FTOrderProdNo = P.FTOrderProdNo"
            '_Qry &= vbCrLf & "   WHERE FTOrderNo <>'' "
            '_Qry &= vbCrLf & "   GROUP BY FTOrderNo"

            _Qry &= vbCrLf & "   SELECT  FTOrderNo,sum(  FNScanQuantity) AS FNScanQuantity  From ("
            '_Qry &= vbCrLf & "   SELECT S.FTOrderNo,sum( S.FNScanQuantity) AS FNScanQuantity "
            '_Qry &= vbCrLf & "FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan_Detail AS S WITH(NOLOCK)  LEFT OUTER JOIN"
            '_Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScanOutline AS O WITH(NOLOCK)  ON S.FTBarcodeNo = O.FTBarcodeNo"
            '_Qry &= vbCrLf & "   WHERE  O.FTBarcodeNo Is NULL "
            '_Qry &= vbCrLf & "   GROUP BY S.FTOrderNo"
            '_Qry &= vbCrLf & "    UNION "
            _Qry &= vbCrLf & "   SELECT P.FTOrderNo, SUM(SC.FNQuantity) AS FNScanQuantity"
            _Qry &= vbCrLf & "  FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScanOutline AS SC WITH(NOLOCK)"
            _Qry &= vbCrLf & " LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS US WITH(NOLOCK) ON SC.FNHSysUnitSectId = US.FNHSysUnitSectId "
            _Qry &= vbCrLf & " LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle AS B WITH(NOLOCK) ON SC.FTBarcodeNo = B.FTBarcodeBundleNo"
            _Qry &= vbCrLf & " INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd AS P WITH(NOLOCK) ON B.FTOrderProdNo = P.FTOrderProdNo"
            _Qry &= vbCrLf & "   WHERE P.FTOrderNo <>'' "
            _Qry &= vbCrLf & "   GROUP BY P.FTOrderNo"
            _Qry &= vbCrLf & ") AS T   Group by FTOrderNo"



            _Qry &= vbCrLf & "  ) AS S ON A.FTOrderNo = S.FTOrderNo "
            _Qry &= vbCrLf & "  LEFT OUTER JOIN ("
            _Qry &= vbCrLf & "   SELECT SUM(B.FNQuantity) AS FNQuantity, P.FTOrderNo"

            _Qry &= vbCrLf & " ,SUM(CASE WHEN SS.FNSendSuplType =0 THEN B.FNQuantity ELSE 0 END ) AS FNQtyEmbroidery"
            _Qry &= vbCrLf & " ,SUM(CASE WHEN SS.FNSendSuplType =1 THEN B.FNQuantity ELSE 0 END ) AS FNQtyPrint"
            _Qry &= vbCrLf & " ,SUM(CASE WHEN SS.FNSendSuplType =2 THEN B.FNQuantity ELSE 0 END ) AS FNQtyHeat"
            _Qry &= vbCrLf & " ,SUM(CASE WHEN SS.FNSendSuplType =3 THEN B.FNQuantity ELSE 0 END ) AS FNQtyLaser"
            _Qry &= vbCrLf & " ,SUM(CASE WHEN SS.FNSendSuplType =4 THEN B.FNQuantity ELSE 0 END ) AS FNQtyPadPrint"
            _Qry &= vbCrLf & " ,SUM(CASE WHEN SS.FNSendSuplType =5 THEN B.FNQuantity ELSE 0 END ) AS FNQtyWindow"

            _Qry &= vbCrLf & "	FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTSendSupl_Barcode AS A WITH(NOLOCK)   INNER JOIN"
            _Qry &= vbCrLf & "	         [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcode_SendSupl AS SS WITH(NOLOCK)   ON A.FTBarcodeSendSuplNo = SS.FTBarcodeSendSuplNo INNER JOIN"
            _Qry &= vbCrLf & "	         [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.V_TPRODTBundle_MainBarcode AS B  WITH(NOLOCK)  ON SS.FTBarcodeBundleNo = B.FTBarcodeBundleNo INNER JOIN"
            _Qry &= vbCrLf & "	         [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd AS P WITH(NOLOCK)   ON SS.FTOrderProdNo = P.FTOrderProdNo"
            _Qry &= vbCrLf & "GROUP BY P.FTOrderNo"
            _Qry &= vbCrLf & " ) AS SendSupl ON A.FTOrderNo = SendSupl.FTOrderNo "
            _Qry &= vbCrLf & "  LEFT OUTER JOIN ("
            _Qry &= vbCrLf & "  SELECT SUM(B.FNQuantity) AS FNQuantity, P.FTOrderNo"

            _Qry &= vbCrLf & " ,SUM(CASE WHEN SS.FNSendSuplType =0 THEN B.FNQuantity ELSE 0 END ) AS FNRcvQtyEmbroidery"
            _Qry &= vbCrLf & " ,SUM(CASE WHEN SS.FNSendSuplType =1 THEN B.FNQuantity ELSE 0 END ) AS FNRcvQtyPrint"
            _Qry &= vbCrLf & " ,SUM(CASE WHEN SS.FNSendSuplType =2 THEN B.FNQuantity ELSE 0 END ) AS FNRcvQtyHeat"
            _Qry &= vbCrLf & " ,SUM(CASE WHEN SS.FNSendSuplType =3 THEN B.FNQuantity ELSE 0 END ) AS FNRcvQtyLaser"
            _Qry &= vbCrLf & " ,SUM(CASE WHEN SS.FNSendSuplType =4 THEN B.FNQuantity ELSE 0 END ) AS FNRcvQtyPadPrint"
            _Qry &= vbCrLf & " ,SUM(CASE WHEN SS.FNSendSuplType =5 THEN B.FNQuantity ELSE 0 END ) AS FNRcvQtyWindow"

            _Qry &= vbCrLf & "	FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTReceiveSupl_Barcode AS A WITH(NOLOCK)   INNER JOIN"
            _Qry &= vbCrLf & "	         [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcode_SendSupl AS SS WITH(NOLOCK)   ON A.FTBarcodeSendSuplNo = SS.FTBarcodeSendSuplNo INNER JOIN"
            _Qry &= vbCrLf & "           [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.V_TPRODTBundle_MainBarcode AS B  WITH(NOLOCK)  ON SS.FTBarcodeBundleNo = B.FTBarcodeBundleNo INNER JOIN"
            _Qry &= vbCrLf & "           [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd AS P WITH(NOLOCK)   ON SS.FTOrderProdNo = P.FTOrderProdNo"
            _Qry &= vbCrLf & "	GROUP BY P.FTOrderNo"
            _Qry &= vbCrLf & "   ) AS RcvSupl ON A.FTOrderNo = RcvSupl.FTOrderNo "

            _Qry &= vbCrLf & "   LEFT OUTER JOIN ("
            _Qry &= vbCrLf & "	SELECT P.FTOrderNo, SUM(B.FNQuantity) AS FNQuantity"
            _Qry &= vbCrLf & "	FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScan_Detail AS A WITH(NOLOCK)  INNER JOIN"
            _Qry &= vbCrLf & "		     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.V_TPRODTBundle_MainBarcode AS B WITH(NOLOCK) ON A.FTBarcodeNo = B.FTBarcodeBundleNo INNER JOIN"
            _Qry &= vbCrLf & "		     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd AS P WITH(NOLOCK) ON B.FTOrderProdNo = P.FTOrderProdNo"
            _Qry &= vbCrLf & "  	INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TPRODMOperation AS OPR WITH(NOLOCK) ON A.FNHSysOperationId = OPR.FNHSysOperationId "
            _Qry &= vbCrLf & "     WHERE (OPR.FTStateSPMK = '1')"
            _Qry &= vbCrLf & "	GROUP BY P.FTOrderNo"
            _Qry &= vbCrLf & "  ) AS SPMK ON A.FTOrderNo = SPMK.FTOrderNo "

            _Qry &= vbCrLf & "  LEFT OUTER JOIN ("
            _Qry &= vbCrLf & "	SELECT SC.FTOrderNo, SUM(SC.FNScanQuantity) AS FNQuantity"
            _Qry &= vbCrLf & "  FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan AS SC WITH(NOLOCK)"
            _Qry &= vbCrLf & "  INNER JOIN   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKCarton AS PC WITH(NOLOCK) ON SC.FTPackNo = PC.FTPackNo AND SC.FNCartonNo = PC.FNCartonNo "
            _Qry &= vbCrLf & "  WHERE PC.FTState='1' "
            _Qry &= vbCrLf & "	GROUP BY SC.FTOrderNo"
            _Qry &= vbCrLf & "  ) AS PAC ON A.FTOrderNo = PAC.FTOrderNo "
            _Qry &= vbCrLf & "  INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp AS Cmp WITH(NOLOCK) ON A.FNHSysCmpId = Cmp.FNHSysCmpId "
            _Qry &= vbCrLf & " WHERE A.FTOrderNo <> ''"
            _Qry &= vbCrLf & "  AND  A.FNHSysCmpId =" & HI.ST.SysInfo.CmpID & ""

            If FNHSysBuyId.Text <> "" Then
                _Qry &= vbCrLf & " AND A.FNHSysBuyId =" & Integer.Parse(Val(FNHSysBuyId.Properties.Tag.ToString)) & "  "
            End If

            If FNHSysStyleId.Text <> "" Then
                _Qry &= vbCrLf & " AND A.FNHSysStyleId =" & Integer.Parse(Val(FNHSysStyleId.Properties.Tag.ToString)) & "  "
            End If

            If FTOrderNo.Text <> "" Then
                _Qry &= vbCrLf & " AND A.FTOrderNo >='" & HI.UL.ULF.rpQuoted(FTOrderNo.Text) & "'  "
            End If

            If FTOrderNoTo.Text <> "" Then
                _Qry &= vbCrLf & " AND A.FTOrderNo <='" & HI.UL.ULF.rpQuoted(FTOrderNoTo.Text) & "'  "
            End If


            If FTCustomerPO.Text <> "" Then
                _Qry &= vbCrLf & " AND A.FTPORef >='" & HI.UL.ULF.rpQuoted(FTCustomerPO.Text) & "'  "
            End If

            If FTCustomerPOTo.Text <> "" Then
                _Qry &= vbCrLf & " AND A.FTPORef <='" & HI.UL.ULF.rpQuoted(FTCustomerPOTo.Text) & "'  "
            End If

            If FTStartShipment.Text <> "" Or FTStartShipment.Text <> "" Then
                _Qry &= vbCrLf & " AND  A.FTOrderNo In ( "
                _Qry &= vbCrLf & " SELECT DISTINCT  FTOrderNo "
                _Qry &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub AS SS WITH(NOLOCK) "
                _Qry &= vbCrLf & " WHERE FTOrderNo <>'' "

                If FTStartShipment.Text <> "" Then
                    _Qry &= vbCrLf & " AND SS.FDShipDate >='" & HI.UL.ULDate.ConvertEnDB(FTStartShipment.Text) & "'  "
                End If

                If FTEndShipment.Text <> "" Then
                    _Qry &= vbCrLf & " AND SS.FDShipDate <='" & HI.UL.ULDate.ConvertEnDB(FTEndShipment.Text) & "'  "
                End If

                _Qry &= vbCrLf & ") "

            End If

            _Qry &= vbCrLf & "  ORDER BY A.FTOrderNo "

            _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)


            Me.ogdtime.DataSource = _dt

            Call LoaddataDetailColorSize()

        Catch ex As Exception
        End Try

        _Spls.Close()
        _RowDataChange = False

    End Sub

    Private Sub LoaddataDetailColorSize()
        Dim _Qry As String
        Dim _dt As DataTable
        ogcdetailcolorsize.DataSource = Nothing
        Try
            _Qry = " SELECT   ST.FTStyleCode,A.FTOrderNo , A.FTPORef"
            _Qry &= vbCrLf & ",Cmp.FTCmpCode,C.FTPOLineItemNo"

            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Qry &= vbCrLf & " ,Cmp.FTCmpNameTH AS FTCmpName"
            Else
                _Qry &= vbCrLf & " ,Cmp.FTCmpNameEN AS FTCmpName"
            End If

            _Qry &= vbCrLf & " ,ISNULL((SELECT  Convert(Datetime,MIN(FDShipDate)) AS FDShipDate  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub AS SS WITH(NOLOCK) WHERE FTOrderNo=A.FTOrderNo),null) AS FDShipDate"
            _Qry &= vbCrLf & " ,B.FTColorway "
            _Qry &= vbCrLf & " ,B.FTSizeBreakDown "

            _Qry &= vbCrLf & ",ISNULL(B.FNQuantity,0) AS FNQuantity "
            _Qry &= vbCrLf & ",ISNULL(B.FNQuantityExtra,0) AS FNQuantityExtra"
            _Qry &= vbCrLf & ",ISNULL(B.FNGarmentQtyTest,0) AS FNGarmentQtyTest"
            _Qry &= vbCrLf & ",ISNULL(B.FNGrandQuantity,0) As FNGrandQuantity"

            '--------------------------ยอด Super Market มาแสดงเป็นยอดตัด-------------------------
            If (_FTStateProdSMKToCutQty) Then
                _Qry &= vbCrLf & ",ISNULL(SPMK.FNQuantity,0) As FNCutQuantity"
                _Qry &= vbCrLf & ",(ISNULL(B.FNGrandQuantity,0) -ISNULL(SPMK.FNQuantity,0) )  As FNCutBalQuantity"


                _Qry &= vbCrLf & ",ISNULL(SPMK.FNQuantity,0) AS FNSPMKQuantity"
                _Qry &= vbCrLf & ",(ISNULL(B.FNGrandQuantity,0) -ISNULL(SPMK.FNQuantity,0) )  As FNBalCutQuantity"
            Else
                _Qry &= vbCrLf & ",ISNULL(C.FNQuantity,0) As FNCutQuantity"
                _Qry &= vbCrLf & ",(ISNULL(B.FNGrandQuantity,0) -ISNULL(C.FNQuantity,0) )  As FNCutBalQuantity"


                _Qry &= vbCrLf & ",ISNULL(SPMK.FNQuantity,0) AS FNSPMKQuantity"
                _Qry &= vbCrLf & ",(ISNULL(C.FNQuantity,0) -ISNULL(SPMK.FNQuantity,0) )  As FNBalCutQuantity"
            End If

            _Qry &= vbCrLf & ",ISNULL(SendSupl.FNQuantity,0) AS FNSendSuplQuantity"
            _Qry &= vbCrLf & ",ISNULL(RcvSupl.FNQuantity,0) AS FNRcvSuplQuantity"
            _Qry &= vbCrLf & ",(ISNULL(SendSupl.FNQuantity,0) - ISNULL(RcvSupl.FNQuantity,0)) AS FNBalSuplQuantity"


            _Qry &= vbCrLf & ",ISNULL(SendSupl.FNQtyEmbroidery,0) AS FNQtyEmbroidery"
            _Qry &= vbCrLf & ",ISNULL(RcvSupl.FNRcvQtyEmbroidery,0) AS FNRcvQtyEmbroidery"
            _Qry &= vbCrLf & ",(ISNULL(SendSupl.FNQtyEmbroidery,0) - ISNULL(RcvSupl.FNRcvQtyEmbroidery,0)) AS FNBalQtyEmbroidery"

            _Qry &= vbCrLf & ",ISNULL(SendSupl.FNQtyPrint,0) AS FNQtyPrint"
            _Qry &= vbCrLf & ",ISNULL(RcvSupl.FNRcvQtyPrint,0) AS FNRcvQtyPrint"
            _Qry &= vbCrLf & ",(ISNULL(SendSupl.FNQtyPrint,0) - ISNULL(RcvSupl.FNRcvQtyPrint,0)) AS FNBalQtyPrint"

            _Qry &= vbCrLf & ",ISNULL(SendSupl.FNQtyHeat,0) AS FNQtyHeat"
            _Qry &= vbCrLf & ",ISNULL(RcvSupl.FNRcvQtyHeat,0) AS FNRcvQtyHeat"
            _Qry &= vbCrLf & ",(ISNULL(SendSupl.FNQtyHeat,0) - ISNULL(RcvSupl.FNRcvQtyHeat,0)) AS FNBalQtyHeat"

            _Qry &= vbCrLf & ",ISNULL(SendSupl.FNQtyLaser,0) AS FNQtyLaser"
            _Qry &= vbCrLf & ",ISNULL(RcvSupl.FNRcvQtyLaser,0) AS FNRcvQtyLaser"
            _Qry &= vbCrLf & ",(ISNULL(SendSupl.FNQtyLaser,0) - ISNULL(RcvSupl.FNRcvQtyLaser,0)) AS FNBalQtyLaser"

            _Qry &= vbCrLf & ",ISNULL(SendSupl.FNQtyPadPrint,0) AS FNQtyPadPrint"
            _Qry &= vbCrLf & ",ISNULL(RcvSupl.FNRcvQtyPadPrint,0) AS FNRcvQtyPadPrint"
            _Qry &= vbCrLf & ",(ISNULL(SendSupl.FNQtyPadPrint,0) - ISNULL(RcvSupl.FNRcvQtyPadPrint,0)) AS FNBalQtyPadPrint"

            _Qry &= vbCrLf & ",ISNULL(SendSupl.FNQtyWindow,0) AS FNQtyWindow"
            _Qry &= vbCrLf & ",ISNULL(RcvSupl.FNRcvQtyWindow,0) AS FNRcvQtyWindow"
            _Qry &= vbCrLf & ",(ISNULL(SendSupl.FNQtyWindow,0) - ISNULL(RcvSupl.FNRcvQtyWindow,0)) AS FNBalQtyWindow"

            _Qry &= vbCrLf & ",ISNULL(D.FNQuantity,0) AS FNSewQuantity"
            _Qry &= vbCrLf & ",ISNULL(S.FNScanQuantity,0) AS FNSewOutQuantity"
            _Qry &= vbCrLf & ",(ISNULL(D.FNQuantity,0)-ISNULL(S.FNScanQuantity,0)) AS FNBalSewQuantity"

            _Qry &= vbCrLf & ",ISNULL(PAC.FNQuantity,0) AS FNPackQuantity"
            _Qry &= vbCrLf & ",(ISNULL(S.FNScanQuantity,0)-ISNULL(PAC.FNQuantity,0) ) AS FNBalPackQuantity"

            _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS A WITH(NOLOCK) INNER JOIN "
            _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "]..TMERMStyle AS ST WITH(NOLOCK) ON A.FNHSysStyleId=ST.FNHSysStyleId INNER jOIN"
            _Qry &= vbCrLf & "  ("
            _Qry &= vbCrLf & "	SELECT FTNikePOLineItem,FTOrderNo,FTColorway,FTSizeBreakDown, SUM(FNQuantity) AS FNQuantity, SUM(FNQuantityExtra) AS FNQuantityExtra, SUM(FNGarmentQtyTest) AS FNGarmentQtyTest, SUM(FNGrandQuantity) AS FNGrandQuantity"
            _Qry &= vbCrLf & "FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_BreakDown AS SBD WITH(NOLOCK)"
            _Qry &= vbCrLf & "	GROUP BY FTNikePOLineItem,FTOrderNo,FTColorway,FTSizeBreakDown"
            _Qry &= vbCrLf & "  ) AS B ON A.FTOrderNo = B.FTOrderNo  "
            _Qry &= vbCrLf & "  LEFT OUTER JOIN ("
            _Qry &= vbCrLf & " SELECT A.FTOrderNo,B.FTColorway,B.FTSizeBreakDown, SUM(B.FNQuantity) AS FNQuantity , B.FTPOLineItemNo "
            _Qry &= vbCrLf & "  FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.V_TPRODTBundle_MainBarcode AS B WITH(NOLOCK)  INNER JOIN"
            _Qry &= vbCrLf & "           [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd AS A  WITH(NOLOCK) ON B.FTOrderProdNo = A.FTOrderProdNo "
            ' _Qry &= vbCrLf & "           [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.V_TPRODTBundle_MainBarcode_Detail AS BD WITH(NOLOCK)  ON B.FTBarcodeBundleNo = BD.FTBarcodeBundleNo INNER JOIN"
            '_Qry &= vbCrLf & "           [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTLayCut AS LC WITH(NOLOCK)  ON BD.FTLayCutNo = LC.FTLayCutNo INNER JOIN"
            '_Qry &= vbCrLf & "           [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_MarkMain AS PM WITH(NOLOCK)  ON LC.FTOrderProdNo = PM.FTOrderProdNo AND LC.FNHSysMarkId = PM.FNHSysMarkId"
            _Qry &= vbCrLf & " WHERE  (B.FTStateGenBarcode = '1')"
            _Qry &= vbCrLf & "  GROUP BY A.FTOrderNo,B.FTColorway,B.FTSizeBreakDown , B.FTPOLineItemNo "
            _Qry &= vbCrLf & "  ) AS C ON A.FTOrderNo = C.FTOrderNo  AND B.FTColorway = C.FTColorway AND B.FTSizeBreakDown = C.FTSizeBreakDown and B.FTNikePOLineItem = C.FTPOLineItemNo "
            _Qry &= vbCrLf & "   LEFT OUTER JOIN ("

            _Qry &= vbCrLf & "	SELECT P.FTOrderNo,B.FTColorway,B.FTSizeBreakDown, SUM(B.FNQuantity) AS FNQuantity , B.FTPOLineItemNo"
            _Qry &= vbCrLf & "	FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScan_Detail AS A WITH(NOLOCK)  INNER JOIN"
            _Qry &= vbCrLf & "		     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS US WITH(NOLOCK) ON A.FNHSysUnitSectId = US.FNHSysUnitSectId INNER JOIN"
            _Qry &= vbCrLf & "		     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.V_TPRODTBundle_MainBarcode AS B WITH(NOLOCK) ON A.FTBarcodeNo = B.FTBarcodeBundleNo INNER JOIN"
            _Qry &= vbCrLf & "		     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd AS P WITH(NOLOCK) ON B.FTOrderProdNo = P.FTOrderProdNo"
            _Qry &= vbCrLf & "	WHERE  (US.FTStateSew = '1') AND (A.FNHSysOperationId = 1405310010) "

            _Qry &= vbCrLf & "	GROUP BY P.FTOrderNo,B.FTColorway,B.FTSizeBreakDown , B.FTPOLineItemNo"
            _Qry &= vbCrLf & "  ) AS D ON A.FTOrderNo = D.FTOrderNo  AND B.FTColorway = D.FTColorway AND B.FTSizeBreakDown = D.FTSizeBreakDown and B.FTNikePOLineItem = D.FTPOLineItemNo"
            _Qry &= vbCrLf & "  LEFT OUTER JOIN ("

            '_Qry &= vbCrLf & "	 SELECT FTOrderNo,SC.FTColorway,SC.FTSizeBreakDown, SUM(FNScanQuantity) AS FNScanQuantity "
            '_Qry &= vbCrLf & "  FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan AS SC "
            '_Qry &= vbCrLf & "	 GROUP BY FTOrderNo,SC.FTColorway,SC.FTSizeBreakDown "

            _Qry &= vbCrLf & "  SELECT  FTOrderNo,  FTColorway, FTSizeBreakDown ,sum(FNScanQuantity) AS FNScanQuantity ,  FTPOLineItemNo From (   "
            '_Qry &= vbCrLf & "  SELECT S.FTOrderNo, S.FTColorway, S.FTSizeBreakDown ,sum( S.FNScanQuantity) AS FNScanQuantity , B.FTPOLineItemNo   "
            '_Qry &= vbCrLf & "  FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan_Detail AS S WITH(NOLOCK)  LEFT OUTER JOIN "
            '_Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScanOutline AS O WITH(NOLOCK)  ON S.FTBarcodeNo = O.FTBarcodeNo " 
            '_Qry &= vbCrLf & "  LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.V_TPRODTBundle_MainBarcode AS B WITH(NOLOCK) ON S.FTBarcodeNo = B.FTBarcodeBundleNo "
            '_Qry &= vbCrLf & "  where  S.FNHSysUnitSectId <> 0"
            '_Qry &= vbCrLf & "  Group by S.FTOrderNo, S.FTColorway, S.FTSizeBreakDown , B.FTPOLineItemNo "
            '_Qry &= vbCrLf & "  UNION "
            _Qry &= vbCrLf & "   SELECT P.FTOrderNo,B.FTColorway,B.FTSizeBreakDown, SUM(SC.FNQuantity) AS FNScanQuantity , B.FTPOLineItemNo"
            _Qry &= vbCrLf & "  FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScanOutline AS SC WITH(NOLOCK)"
            _Qry &= vbCrLf & "         INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle AS B WITH(NOLOCK) ON SC.FTBarcodeNo = B.FTBarcodeBundleNo"
            _Qry &= vbCrLf & " INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd AS P WITH(NOLOCK) ON B.FTOrderProdNo = P.FTOrderProdNo"
            _Qry &= vbCrLf & "   WHERE P.FTOrderNo <>'' "
            _Qry &= vbCrLf & "   GROUP BY P.FTOrderNo, B.FTColorway,B.FTSizeBreakDown , B.FTPOLineItemNo"
            _Qry &= vbCrLf & " ) AS T Group by FTOrderNo, FTColorway,FTSizeBreakDown ,FTPOLineItemNo"

            _Qry &= vbCrLf & "  ) As S On A.FTOrderNo = S.FTOrderNo  And B.FTColorway = S.FTColorway And B.FTSizeBreakDown = S.FTSizeBreakDown And B.FTNikePOLineItem = S.FTPOLineItemNo"
            _Qry &= vbCrLf & "  LEFT OUTER JOIN ("
            _Qry &= vbCrLf & "   Select SUM(B.FNQuantity) As FNQuantity, P.FTOrderNo,B.FTColorway,B.FTSizeBreakDown , B.FTPOLineItemNo"

            _Qry &= vbCrLf & " ,SUM(CASE WHEN SS.FNSendSuplType =0 THEN B.FNQuantity ELSE 0 END ) AS FNQtyEmbroidery"
            _Qry &= vbCrLf & " ,SUM(CASE WHEN SS.FNSendSuplType =1 THEN B.FNQuantity ELSE 0 END ) AS FNQtyPrint"
            _Qry &= vbCrLf & " ,SUM(CASE WHEN SS.FNSendSuplType =2 THEN B.FNQuantity ELSE 0 END ) AS FNQtyHeat"
            _Qry &= vbCrLf & " ,SUM(CASE WHEN SS.FNSendSuplType =3 THEN B.FNQuantity ELSE 0 END ) AS FNQtyLaser"
            _Qry &= vbCrLf & " ,SUM(CASE WHEN SS.FNSendSuplType =4 THEN B.FNQuantity ELSE 0 END ) AS FNQtyPadPrint"
            _Qry &= vbCrLf & " ,SUM(CASE WHEN SS.FNSendSuplType =5 THEN B.FNQuantity ELSE 0 END ) AS FNQtyWindow"

            _Qry &= vbCrLf & "	FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTSendSupl_Barcode As A With(NOLOCK)   INNER JOIN"
            _Qry &= vbCrLf & "	         [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcode_SendSupl As SS With(NOLOCK)   On A.FTBarcodeSendSuplNo = SS.FTBarcodeSendSuplNo INNER JOIN"
            _Qry &= vbCrLf & "	         [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.V_TPRODTBundle_MainBarcode As B  With(NOLOCK)  On SS.FTBarcodeBundleNo = B.FTBarcodeBundleNo INNER JOIN"
            _Qry &= vbCrLf & "	         [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd As P With(NOLOCK)   On SS.FTOrderProdNo = P.FTOrderProdNo"
            _Qry &= vbCrLf & "GROUP BY P.FTOrderNo,B.FTColorway,B.FTSizeBreakDown  , B.FTPOLineItemNo"
            _Qry &= vbCrLf & " ) As SendSupl On A.FTOrderNo = SendSupl.FTOrderNo  And B.FTColorway = SendSupl.FTColorway And B.FTSizeBreakDown = SendSupl.FTSizeBreakDown And B.FTNikePOLineItem = SendSupl.FTPOLineItemNo "
            _Qry &= vbCrLf & "  LEFT OUTER JOIN ("
            _Qry &= vbCrLf & "  Select SUM(B.FNQuantity) As FNQuantity, P.FTOrderNo,B.FTColorway,B.FTSizeBreakDown , B.FTPOLineItemNo"

            _Qry &= vbCrLf & " ,SUM(CASE WHEN SS.FNSendSuplType =0 THEN B.FNQuantity ELSE 0 END ) AS FNRcvQtyEmbroidery"
            _Qry &= vbCrLf & " ,SUM(CASE WHEN SS.FNSendSuplType =1 THEN B.FNQuantity ELSE 0 END ) AS FNRcvQtyPrint"
            _Qry &= vbCrLf & " ,SUM(CASE WHEN SS.FNSendSuplType =2 THEN B.FNQuantity ELSE 0 END ) AS FNRcvQtyHeat"
            _Qry &= vbCrLf & " ,SUM(CASE WHEN SS.FNSendSuplType =3 THEN B.FNQuantity ELSE 0 END ) AS FNRcvQtyLaser"
            _Qry &= vbCrLf & " ,SUM(CASE WHEN SS.FNSendSuplType =4 THEN B.FNQuantity ELSE 0 END ) AS FNRcvQtyPadPrint"
            _Qry &= vbCrLf & " ,SUM(CASE WHEN SS.FNSendSuplType =5 THEN B.FNQuantity ELSE 0 END ) AS FNRcvQtyWindow"

            _Qry &= vbCrLf & "	FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTReceiveSupl_Barcode As A With(NOLOCK)   INNER JOIN"
            _Qry &= vbCrLf & "	         [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcode_SendSupl As SS With(NOLOCK)   On A.FTBarcodeSendSuplNo = SS.FTBarcodeSendSuplNo INNER JOIN"
            _Qry &= vbCrLf & "           [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.V_TPRODTBundle_MainBarcode As B  With(NOLOCK)  On SS.FTBarcodeBundleNo = B.FTBarcodeBundleNo INNER JOIN"
            _Qry &= vbCrLf & "           [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd As P With(NOLOCK)   On SS.FTOrderProdNo = P.FTOrderProdNo"
            _Qry &= vbCrLf & "	GROUP BY P.FTOrderNo,B.FTColorway,B.FTSizeBreakDown , B.FTPOLineItemNo"
            _Qry &= vbCrLf & "   ) As RcvSupl On A.FTOrderNo = RcvSupl.FTOrderNo  And B.FTColorway = RcvSupl.FTColorway And B.FTSizeBreakDown = RcvSupl.FTSizeBreakDown And B.FTNikePOLineItem = RcvSupl.FTPOLineItemNo"

            _Qry &= vbCrLf & "   LEFT OUTER JOIN ("
            _Qry &= vbCrLf & "	Select P.FTOrderNo,B.FTColorway,B.FTSizeBreakDown, SUM(B.FNQuantity) As FNQuantity , B.FTPOLineItemNo"
            _Qry &= vbCrLf & "	FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScan_Detail As A With(NOLOCK)  INNER JOIN"
            _Qry &= vbCrLf & "		     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.V_TPRODTBundle_MainBarcode As B With(NOLOCK) On A.FTBarcodeNo = B.FTBarcodeBundleNo INNER JOIN"
            _Qry &= vbCrLf & "		     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd As P With(NOLOCK) On B.FTOrderProdNo = P.FTOrderProdNo"
            _Qry &= vbCrLf & "  	INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TPRODMOperation As OPR With(NOLOCK) On A.FNHSysOperationId = OPR.FNHSysOperationId "
            _Qry &= vbCrLf & "     WHERE (OPR.FTStateSPMK = '1')"
            _Qry &= vbCrLf & "	GROUP BY P.FTOrderNo,B.FTColorway,B.FTSizeBreakDown , B.FTPOLineItemNo"
            _Qry &= vbCrLf & "  ) AS SPMK ON A.FTOrderNo = SPMK.FTOrderNo  AND B.FTColorway = SPMK.FTColorway AND B.FTSizeBreakDown = SPMK.FTSizeBreakDown and B.FTNikePOLineItem = SPMK.FTPOLineItemNo"


            _Qry &= vbCrLf & "  LEFT OUTER JOIN ("
            _Qry &= vbCrLf & "	SELECT SC.FTOrderNo,B.FTColorway,B.FTSizeBreakDown, SUM(SC.FNScanQuantity) AS FNQuantity, B.FTPOLineItemNo"
            _Qry &= vbCrLf & "  FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan AS SC WITH(NOLOCK)"
            _Qry &= vbCrLf & "  INNER JOIN   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKCarton AS PC ON SC.FTPackNo = PC.FTPackNo AND SC.FNCartonNo = PC.FNCartonNo "
            _Qry &= vbCrLf & "  INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle AS B ON  SC.FTBarcodeNo = B.FTBarcodeBundleNo"
            _Qry &= vbCrLf & "  WHERE PC.FTState='1' "
            _Qry &= vbCrLf & "	GROUP BY SC.FTOrderNo,B.FTColorway,B.FTSizeBreakDown, B.FTPOLineItemNo"
            _Qry &= vbCrLf & "  ) AS PAC ON A.FTOrderNo = PAC.FTOrderNo   AND B.FTColorway = PAC.FTColorway AND B.FTSizeBreakDown = PAC.FTSizeBreakDown and B.FTNikePOLineItem = PAC.FTPOLineItemNo "


            _Qry &= vbCrLf & "  INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp AS Cmp WITH(NOLOCK) ON A.FNHSysCmpId = Cmp.FNHSysCmpId "
            _Qry &= vbCrLf & "  LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMatSize AS SSB WITH(NOLOCK) ON B.FTSizeBreakDown = SSB.FTMatSizeCode "
            _Qry &= vbCrLf & " WHERE A.FTOrderNo <> ''"
            _Qry &= vbCrLf & "  AND  A.FNHSysCmpId =" & HI.ST.SysInfo.CmpID & ""

            If FNHSysBuyId.Text <> "" Then
                _Qry &= vbCrLf & " AND A.FNHSysBuyId =" & Integer.Parse(Val(FNHSysBuyId.Properties.Tag.ToString)) & "  "
            End If

            If FNHSysStyleId.Text <> "" Then
                _Qry &= vbCrLf & " AND A.FNHSysStyleId =" & Integer.Parse(Val(FNHSysStyleId.Properties.Tag.ToString)) & "  "
            End If

            If FTOrderNo.Text <> "" Then
                _Qry &= vbCrLf & " AND A.FTOrderNo >='" & HI.UL.ULF.rpQuoted(FTOrderNo.Text) & "'  "
            End If

            If FTOrderNoTo.Text <> "" Then
                _Qry &= vbCrLf & " AND A.FTOrderNo <='" & HI.UL.ULF.rpQuoted(FTOrderNoTo.Text) & "'  "
            End If

            If FTCustomerPO.Text <> "" Then
                _Qry &= vbCrLf & " AND A.FTPORef >='" & HI.UL.ULF.rpQuoted(FTCustomerPO.Text) & "'  "
            End If

            If FTCustomerPOTo.Text <> "" Then
                _Qry &= vbCrLf & " AND A.FTPORef <='" & HI.UL.ULF.rpQuoted(FTCustomerPOTo.Text) & "'  "
            End If

            If FTStartShipment.Text <> "" Or FTStartShipment.Text <> "" Then

                _Qry &= vbCrLf & " AND  A.FTOrderNo In ( "
                _Qry &= vbCrLf & "  SELECT DISTINCT  FTOrderNo "
                _Qry &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub AS SS WITH(NOLOCK) "
                _Qry &= vbCrLf & "  WHERE FTOrderNo <>'' "

                If FTStartShipment.Text <> "" Then
                    _Qry &= vbCrLf & "  AND SS.FDShipDate >='" & HI.UL.ULDate.ConvertEnDB(FTStartShipment.Text) & "'  "
                End If

                If FTEndShipment.Text <> "" Then
                    _Qry &= vbCrLf & "  AND SS.FDShipDate <='" & HI.UL.ULDate.ConvertEnDB(FTEndShipment.Text) & "'  "
                End If

                _Qry &= vbCrLf & " ) "

            End If

            _Qry &= vbCrLf & "  ORDER BY A.FTOrderNo,B.FTColorWay,SSB.FNMatSizeSeq "

            _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

            Me.ogcdetailcolorsize.DataSource = _dt

        Catch ex As Exception
        End Try

    End Sub

    Private Sub LoaddataDetailColorSize_bak()
        Dim _Qry As String
        Dim _dt As DataTable
        ogcdetailcolorsize.DataSource = Nothing
        Try
            _Qry = " SELECT   A.FTOrderNo"
            _Qry &= vbCrLf & ",Cmp.FTCmpCode"

            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Qry &= vbCrLf & " ,Cmp.FTCmpNameTH AS FTCmpName"
            Else
                _Qry &= vbCrLf & " ,Cmp.FTCmpNameEN AS FTCmpName"
            End If

            _Qry &= vbCrLf & " ,ISNULL((SELECT  Convert(Datetime,MIN(FDShipDate)) AS FDShipDate  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub AS SS WITH(NOLOCK) WHERE FTOrderNo=A.FTOrderNo),null) AS FDShipDate"
            _Qry &= vbCrLf & " ,B.FTColorway "
            _Qry &= vbCrLf & " ,B.FTSizeBreakDown "

            _Qry &= vbCrLf & ",ISNULL(B.FNQuantity,0) AS FNQuantity "
            _Qry &= vbCrLf & ",ISNULL(B.FNQuantityExtra,0) AS FNQuantityExtra"
            _Qry &= vbCrLf & ",ISNULL(B.FNGarmentQtyTest,0) AS FNGarmentQtyTest"
            _Qry &= vbCrLf & ",ISNULL(B.FNGrandQuantity,0) As FNGrandQuantity"

            '--------------------------ยอด Super Market มาแสดงเป็นยอดตัด-------------------------
            If (_FTStateProdSMKToCutQty) Then

                _Qry &= vbCrLf & ",ISNULL(SPMK.FNQuantity,0) As FNCutQuantity"
                _Qry &= vbCrLf & ",(ISNULL(B.FNGrandQuantity,0) -ISNULL(SPMK.FNQuantity,0) )  As FNCutBalQuantity"
                _Qry &= vbCrLf & ",ISNULL(SPMK.FNQuantity,0) AS FNSPMKQuantity"
                _Qry &= vbCrLf & ",(ISNULL(B.FNGrandQuantity,0) -ISNULL(SPMK.FNQuantity,0) )  As FNBalCutQuantity"

            Else

                _Qry &= vbCrLf & ",ISNULL(C.FNQuantity,0) As FNCutQuantity"
                _Qry &= vbCrLf & ",(ISNULL(B.FNGrandQuantity,0) -ISNULL(C.FNQuantity,0) )  As FNCutBalQuantity"
                _Qry &= vbCrLf & ",ISNULL(SPMK.FNQuantity,0) AS FNSPMKQuantity"
                _Qry &= vbCrLf & ",(ISNULL(C.FNQuantity,0) -ISNULL(SPMK.FNQuantity,0) )  As FNBalCutQuantity"

            End If

            _Qry &= vbCrLf & ",ISNULL(SendSupl.FNQuantity,0) AS FNSendSuplQuantity"
            _Qry &= vbCrLf & ",ISNULL(RcvSupl.FNQuantity,0) AS FNRcvSuplQuantity"
            _Qry &= vbCrLf & ",(ISNULL(SendSupl.FNQuantity,0) - ISNULL(RcvSupl.FNQuantity,0)) AS FNBalSuplQuantity"

            _Qry &= vbCrLf & ",ISNULL(D.FNQuantity,0) AS FNSewQuantity"
            _Qry &= vbCrLf & ",ISNULL(S.FNScanQuantity,0) AS FNSewOutQuantity"
            _Qry &= vbCrLf & ",(ISNULL(D.FNQuantity,0)-ISNULL(S.FNScanQuantity,0)) AS FNBalSewQuantity"

            _Qry &= vbCrLf & ",ISNULL(PAC.FNQuantity,0) AS FNPackQuantity"
            _Qry &= vbCrLf & ",(ISNULL(S.FNScanQuantity,0)-ISNULL(PAC.FNQuantity,0) ) AS FNBalPackQuantity"

            _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS A WITH(NOLOCK) INNER JOIN "
            _Qry &= vbCrLf & "  ("
            _Qry &= vbCrLf & "	SELECT FTOrderNo,FTColorway,FTSizeBreakDown, SUM(FNQuantity) AS FNQuantity, SUM(FNQuantityExtra) AS FNQuantityExtra, SUM(FNGarmentQtyTest) AS FNGarmentQtyTest, SUM(FNGrandQuantity) AS FNGrandQuantity"
            _Qry &= vbCrLf & "FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_BreakDown AS SBD WITH(NOLOCK)"
            _Qry &= vbCrLf & "	GROUP BY FTOrderNo,FTColorway,FTSizeBreakDown"
            _Qry &= vbCrLf & "  ) AS B ON A.FTOrderNo = B.FTOrderNo  "
            _Qry &= vbCrLf & "  LEFT OUTER JOIN ("
            _Qry &= vbCrLf & " SELECT A.FTOrderNo,B.FTColorway,B.FTSizeBreakDown, SUM(B.FNQuantity) AS FNQuantity"
            _Qry &= vbCrLf & "  FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.V_TPRODTBundle_MainBarcode AS B WITH(NOLOCK)  INNER JOIN"
            _Qry &= vbCrLf & "           [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd AS A  WITH(NOLOCK) ON B.FTOrderProdNo = A.FTOrderProdNo "
            '_Qry &= vbCrLf & "           [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.V_TPRODTBundle_MainBarcode_Detail AS BD WITH(NOLOCK)  ON B.FTBarcodeBundleNo = BD.FTBarcodeBundleNo INNER JOIN"
            '_Qry &= vbCrLf & "           [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTLayCut AS LC WITH(NOLOCK)  ON BD.FTLayCutNo = LC.FTLayCutNo INNER JOIN"
            '_Qry &= vbCrLf & "           [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_MarkMain AS PM WITH(NOLOCK)  ON LC.FTOrderProdNo = PM.FTOrderProdNo AND LC.FNHSysMarkId = PM.FNHSysMarkId"
            _Qry &= vbCrLf & " WHERE  (B.FTStateGenBarcode = '1')"
            _Qry &= vbCrLf & "  GROUP BY A.FTOrderNo,B.FTColorway,B.FTSizeBreakDown"
            _Qry &= vbCrLf & "  ) AS C ON A.FTOrderNo = C.FTOrderNo  AND B.FTColorway = C.FTColorway AND B.FTSizeBreakDown = C.FTSizeBreakDown  "
            _Qry &= vbCrLf & "   LEFT OUTER JOIN ("
            _Qry &= vbCrLf & "	SELECT P.FTOrderNo,B.FTColorway,B.FTSizeBreakDown, SUM(B.FNQuantity) AS FNQuantity"
            _Qry &= vbCrLf & "	FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScan_Detail AS A WITH(NOLOCK)  INNER JOIN"
            _Qry &= vbCrLf & "		     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS US WITH(NOLOCK) ON A.FNHSysUnitSectId = US.FNHSysUnitSectId INNER JOIN"
            _Qry &= vbCrLf & "		     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.V_TPRODTBundle_MainBarcode AS B WITH(NOLOCK) ON A.FTBarcodeNo = B.FTBarcodeBundleNo INNER JOIN"
            _Qry &= vbCrLf & "		     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd AS P WITH(NOLOCK) ON B.FTOrderProdNo = P.FTOrderProdNo"
            _Qry &= vbCrLf & "	WHERE  (US.FTStateSew = '1') " 'AND (A.FNHSysOperationId = 1405310010) "
            _Qry &= vbCrLf & "	GROUP BY P.FTOrderNo,B.FTColorway,B.FTSizeBreakDown"
            _Qry &= vbCrLf & "  ) AS D ON A.FTOrderNo = D.FTOrderNo  AND B.FTColorway = D.FTColorway AND B.FTSizeBreakDown = D.FTSizeBreakDown "
            _Qry &= vbCrLf & "  LEFT OUTER JOIN ("


            '_Qry &= vbCrLf & "	SELECT FTOrderNo,SC.FTColorway,SC.FTSizeBreakDown, SUM(FNScanQuantity) AS FNScanQuantity"
            '_Qry &= vbCrLf & "  FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan AS SC"
            '_Qry &= vbCrLf & "	GROUP BY FTOrderNo,SC.FTColorway,SC.FTSizeBreakDown"

            _Qry &= vbCrLf & "   SELECT P.FTOrderNo,B.FTColorway,B.FTSizeBreakDown, SUM(SC.FNQuantity) AS FNScanQuantity"
            _Qry &= vbCrLf & "  FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScanOutline AS SC WITH(NOLOCK)"
            _Qry &= vbCrLf & " LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.V_TPRODTBundle_MainBarcode AS B WITH(NOLOCK) ON SC.FTBarcodeNo = B.FTBarcodeBundleNo"
            _Qry &= vbCrLf & " INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd AS P WITH(NOLOCK) ON B.FTOrderProdNo = P.FTOrderProdNo"
            _Qry &= vbCrLf & "   WHERE FTOrderNo <>'' "
            _Qry &= vbCrLf & "   GROUP BY FTOrderNo, B.FTColorway,B.FTSizeBreakDown"

            _Qry &= vbCrLf & "  ) AS S ON A.FTOrderNo = S.FTOrderNo  AND B.FTColorway = S.FTColorway AND B.FTSizeBreakDown = S.FTSizeBreakDown "
            _Qry &= vbCrLf & "  LEFT OUTER JOIN ("
            _Qry &= vbCrLf & "   SELECT SUM(B.FNQuantity) AS FNQuantity, P.FTOrderNo,B.FTColorway,B.FTSizeBreakDown"
            _Qry &= vbCrLf & "	FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTSendSupl_Barcode AS A WITH(NOLOCK)   INNER JOIN"
            _Qry &= vbCrLf & "	         [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcode_SendSupl AS SS WITH(NOLOCK)   ON A.FTBarcodeSendSuplNo = SS.FTBarcodeSendSuplNo INNER JOIN"
            _Qry &= vbCrLf & "	         [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.V_TPRODTBundle_MainBarcode AS B  WITH(NOLOCK)  ON SS.FTBarcodeBundleNo = B.FTBarcodeBundleNo INNER JOIN"
            _Qry &= vbCrLf & "	         [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd AS P WITH(NOLOCK)   ON SS.FTOrderProdNo = P.FTOrderProdNo"
            _Qry &= vbCrLf & "GROUP BY P.FTOrderNo,B.FTColorway,B.FTSizeBreakDown "
            _Qry &= vbCrLf & " ) AS SendSupl ON A.FTOrderNo = SendSupl.FTOrderNo  AND B.FTColorway = SendSupl.FTColorway AND B.FTSizeBreakDown = SendSupl.FTSizeBreakDown  "
            _Qry &= vbCrLf & "  LEFT OUTER JOIN ("
            _Qry &= vbCrLf & "  SELECT SUM(B.FNQuantity) AS FNQuantity, P.FTOrderNo,B.FTColorway,B.FTSizeBreakDown"
            _Qry &= vbCrLf & "	FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTReceiveSupl_Barcode AS A WITH(NOLOCK)   INNER JOIN"
            _Qry &= vbCrLf & "	         [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcode_SendSupl AS SS WITH(NOLOCK)   ON A.FTBarcodeSendSuplNo = SS.FTBarcodeSendSuplNo INNER JOIN"
            _Qry &= vbCrLf & "           [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.V_TPRODTBundle_MainBarcode AS B  WITH(NOLOCK)  ON SS.FTBarcodeBundleNo = B.FTBarcodeBundleNo INNER JOIN"
            _Qry &= vbCrLf & "           [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd AS P WITH(NOLOCK)   ON SS.FTOrderProdNo = P.FTOrderProdNo"
            _Qry &= vbCrLf & "	GROUP BY P.FTOrderNo,B.FTColorway,B.FTSizeBreakDown"
            _Qry &= vbCrLf & "   ) AS RcvSupl ON A.FTOrderNo = RcvSupl.FTOrderNo  AND B.FTColorway = RcvSupl.FTColorway AND B.FTSizeBreakDown = RcvSupl.FTSizeBreakDown "

            _Qry &= vbCrLf & "   LEFT OUTER JOIN ("
            _Qry &= vbCrLf & "	SELECT P.FTOrderNo,B.FTColorway,B.FTSizeBreakDown, SUM(B.FNQuantity) AS FNQuantity"
            _Qry &= vbCrLf & "	FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScan_Detail AS A WITH(NOLOCK)  INNER JOIN"
            _Qry &= vbCrLf & "		     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.V_TPRODTBundle_MainBarcode AS B WITH(NOLOCK) ON A.FTBarcodeNo = B.FTBarcodeBundleNo INNER JOIN"
            _Qry &= vbCrLf & "		     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd AS P WITH(NOLOCK) ON B.FTOrderProdNo = P.FTOrderProdNo"
            _Qry &= vbCrLf & "  	INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TPRODMOperation AS OPR WITH(NOLOCK) ON A.FNHSysOperationId = OPR.FNHSysOperationId "
            _Qry &= vbCrLf & "     WHERE (OPR.FTStateSPMK = '1')"
            _Qry &= vbCrLf & "	GROUP BY P.FTOrderNo,B.FTColorway,B.FTSizeBreakDown"
            _Qry &= vbCrLf & "  ) AS SPMK ON A.FTOrderNo = SPMK.FTOrderNo  AND B.FTColorway = SPMK.FTColorway AND B.FTSizeBreakDown = SPMK.FTSizeBreakDown "

            _Qry &= vbCrLf & "  LEFT OUTER JOIN ("
            _Qry &= vbCrLf & "	SELECT SC.FTOrderNo,SC.FTColorway,SC.FTSizeBreakDown, SUM(SC.FNScanQuantity) AS FNQuantity"
            _Qry &= vbCrLf & "  FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan AS SC"
            _Qry &= vbCrLf & "  INNER JOIN   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKCarton AS PC ON SC.FTPackNo = PC.FTPackNo AND SC.FNCartonNo = PC.FNCartonNo "
            _Qry &= vbCrLf & "  WHERE PC.FTState='1' "
            _Qry &= vbCrLf & "	GROUP BY SC.FTOrderNo,SC.FTColorway,SC.FTSizeBreakDown"
            _Qry &= vbCrLf & "  ) AS PAC ON A.FTOrderNo = PAC.FTOrderNo   AND B.FTColorway = PAC.FTColorway AND B.FTSizeBreakDown = PAC.FTSizeBreakDown "
            _Qry &= vbCrLf & "  INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp AS Cmp WITH(NOLOCK) ON A.FNHSysCmpId = Cmp.FNHSysCmpId "
            _Qry &= vbCrLf & "  LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMatSize AS SSB WITH(NOLOCK) ON B.FTSizeBreakDown = SSB.FTMatSizeCode "
            _Qry &= vbCrLf & " WHERE A.FTOrderNo <> ''"

            If FNHSysBuyId.Text <> "" Then
                _Qry &= vbCrLf & " AND A.FNHSysBuyId =" & Integer.Parse(Val(FNHSysBuyId.Properties.Tag.ToString)) & "  "
            End If

            If FNHSysStyleId.Text <> "" Then
                _Qry &= vbCrLf & " AND A.FNHSysStyleId =" & Integer.Parse(Val(FNHSysStyleId.Properties.Tag.ToString)) & "  "
            End If

            If FTOrderNo.Text <> "" Then
                _Qry &= vbCrLf & " AND A.FTOrderNo >='" & HI.UL.ULF.rpQuoted(FTOrderNo.Text) & "'  "
            End If

            If FTOrderNoTo.Text <> "" Then
                _Qry &= vbCrLf & " AND A.FTOrderNo <='" & HI.UL.ULF.rpQuoted(FTOrderNoTo.Text) & "'  "
            End If

            If FTStartShipment.Text <> "" Or FTStartShipment.Text <> "" Then
                _Qry &= vbCrLf & " AND  A.FTOrderNo In ( "
                _Qry &= vbCrLf & " SELECT DISTINCT  FTOrderNo "
                _Qry &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub AS SS WITH(NOLOCK) "
                _Qry &= vbCrLf & " WHERE FTOrderNo <>'' "

                If FTStartShipment.Text <> "" Then
                    _Qry &= vbCrLf & " AND SS.FDShipDate >='" & HI.UL.ULDate.ConvertEnDB(FTStartShipment.Text) & "'  "
                End If

                If FTEndShipment.Text <> "" Then
                    _Qry &= vbCrLf & " AND SS.FDShipDate <='" & HI.UL.ULDate.ConvertEnDB(FTEndShipment.Text) & "'  "
                End If

                _Qry &= vbCrLf & ") "

            End If

            _Qry &= vbCrLf & "  ORDER BY A.FTOrderNo,B.FTColorWay,SSB.FNMatSizeSeq "

            _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

            Me.ogcdetailcolorsize.DataSource = _dt

        Catch ex As Exception
        End Try

    End Sub

    Private Function VerifyData() As Boolean
        Dim _Pass As Boolean = False

        If Me.FNHSysBuyId.Text <> "" And FNHSysBuyId.Properties.Tag.ToString <> "" Then
            _Pass = True
        End If

        If Me.FNHSysStyleId.Text <> "" And FNHSysStyleId.Properties.Tag.ToString <> "" Then
            _Pass = True
        End If

        If Me.FTOrderNo.Text <> "" And FTOrderNo.Properties.Tag.ToString <> "" Then
            _Pass = True
        End If

        If Me.FTOrderNoTo.Text <> "" And FTOrderNoTo.Properties.Tag.ToString <> "" Then
            _Pass = True
        End If

        If Me.FTStartShipment.Text <> "" Then
            _Pass = True
        End If

        If Me.FTEndShipment.Text <> "" Then
            _Pass = True
        End If

        If Me.FTCustomerPO.Text <> "" And FTCustomerPO.Properties.Tag.ToString <> "" Then
            _Pass = True
        End If

        If Me.FTCustomerPOTo.Text <> "" And FTCustomerPOTo.Properties.Tag.ToString <> "" Then
            _Pass = True
        End If

        If Not (_Pass) Then
            HI.MG.ShowMsg.mInfo("กรุณาทำการเลือกเงื่อไข อย่างน้อย 1 รายการ !!!", 1406170001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
        End If

        Return _Pass
    End Function

#End Region


#Region "General"

    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            Call InitGrid()

            HI.UL.AppRegistry.LoadLayoutGridFromRegistry(Me, Me.ogvtime)
            HI.UL.AppRegistry.LoadLayoutGridFromRegistry(Me, Me.ogvdetailcolorsize)


            StateCal = False
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmload.Click

        If VerifyData() Then

            Dim _Qry As String = ""
            _Qry = "SELECt TOP 1 FTStateProdSMKToCutQty "
            _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEConfig AS S WITH(NOLOCK)"
            _Qry &= vbCrLf & " WHERE FTCmpCode='" & HI.UL.ULF.rpQuoted(HI.ST.SysInfo.CmpCode) & "'"

            _FTStateProdSMKToCutQty = (HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_SECURITY, "") = "1")

            Call LoadData()

        End If

    End Sub

    Private Sub ocmclear_Click(sender As System.Object, e As System.EventArgs) Handles ocmclear.Click
        HI.TL.HandlerControl.ClearControl(Me)
    End Sub

#End Region

    Private Sub ocmsavelayout_Click(sender As Object, e As EventArgs) Handles ocmsavelayout.Click
        HI.UL.AppRegistry.SaveLayoutGridToRegistry(Me, Me.ogvtime)
        HI.UL.AppRegistry.SaveLayoutGridToRegistry(Me, Me.ogvdetailcolorsize)
        HI.MG.ShowMsg.mInfo("Save Layout Grid Complete...", 1404240001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Information)
    End Sub

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ogvtime_RowCellStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles ogvtime.RowCellStyle
        Try
            With Me.ogvtime
                Select Case e.Column.FieldName
                    Case "FNCutQuantity"
                        If Double.Parse(.GetRowCellValue(e.RowHandle, "FNCutQuantity")) < Double.Parse(.GetRowCellValue(e.RowHandle, "FNGrandQuantity")) Then
                            e.Appearance.BackColor = Drawing.Color.LemonChiffon
                            e.Appearance.ForeColor = Drawing.Color.Red
                        Else
                            e.Appearance.BackColor = Drawing.Color.MintCream
                            e.Appearance.ForeColor = Drawing.Color.Blue
                        End If
                    Case "FNRcvSuplQuantity"
                        If Double.Parse(.GetRowCellValue(e.RowHandle, "FNSendSuplQuantity")) > 0 Then
                            If Double.Parse(.GetRowCellValue(e.RowHandle, "FNRcvSuplQuantity")) < Double.Parse(.GetRowCellValue(e.RowHandle, "FNSendSuplQuantity")) Then
                                e.Appearance.BackColor = Drawing.Color.LemonChiffon
                                e.Appearance.ForeColor = Drawing.Color.Red
                            Else
                                e.Appearance.BackColor = Drawing.Color.MintCream
                                e.Appearance.ForeColor = Drawing.Color.Blue
                            End If
                        End If

                    Case "FNSPMKQuantity"
                        If Double.Parse(.GetRowCellValue(e.RowHandle, "FNCutQuantity")) > 0 Then
                            If Double.Parse(.GetRowCellValue(e.RowHandle, "FNSPMKQuantity")) < Double.Parse(.GetRowCellValue(e.RowHandle, "FNCutQuantity")) Then
                                e.Appearance.BackColor = Drawing.Color.LemonChiffon
                                e.Appearance.ForeColor = Drawing.Color.Red
                            Else
                                e.Appearance.BackColor = Drawing.Color.MintCream
                                e.Appearance.ForeColor = Drawing.Color.Blue
                            End If
                        End If
                    Case "FNSewQuantity"
                        If Double.Parse(.GetRowCellValue(e.RowHandle, "FNSPMKQuantity")) > 0 Then
                            If Double.Parse(.GetRowCellValue(e.RowHandle, "FNSewQuantity")) < Double.Parse(.GetRowCellValue(e.RowHandle, "FNSPMKQuantity")) Then
                                e.Appearance.BackColor = Drawing.Color.LemonChiffon
                                e.Appearance.ForeColor = Drawing.Color.Red
                            Else
                                e.Appearance.BackColor = Drawing.Color.MintCream
                                e.Appearance.ForeColor = Drawing.Color.Blue
                            End If
                        End If

                    Case "FNSewOutQuantity"
                        If Double.Parse(.GetRowCellValue(e.RowHandle, "FNSewQuantity")) > 0 Then
                            If Double.Parse(.GetRowCellValue(e.RowHandle, "FNSewOutQuantity")) < Double.Parse(.GetRowCellValue(e.RowHandle, "FNSewQuantity")) Then
                                e.Appearance.BackColor = Drawing.Color.LemonChiffon
                                e.Appearance.ForeColor = Drawing.Color.Red
                            Else
                                e.Appearance.BackColor = Drawing.Color.MintCream
                                e.Appearance.ForeColor = Drawing.Color.Blue
                            End If
                        End If

                    Case "FNPackQuantity"
                        If Double.Parse(.GetRowCellValue(e.RowHandle, "FNSewOutQuantity")) > 0 Then
                            If Double.Parse(.GetRowCellValue(e.RowHandle, "FNPackQuantity")) < Double.Parse(.GetRowCellValue(e.RowHandle, "FNSewOutQuantity")) Then
                                e.Appearance.BackColor = Drawing.Color.LemonChiffon
                                e.Appearance.ForeColor = Drawing.Color.Red

                            Else
                                e.Appearance.BackColor = Drawing.Color.MintCream
                                e.Appearance.ForeColor = Drawing.Color.Blue
                            End If
                        End If
                    Case "FNBalCutQuantity", "FNBalSuplQuantity", "FNBalSewQuantity", "FNBalPackQuantity", "FNCutBalQuantity"
                        If Double.Parse(.GetRowCellValue(e.RowHandle, e.Column.FieldName)) > 0 Then
                            e.Appearance.BackColor = Drawing.Color.LemonChiffon
                            e.Appearance.ForeColor = Drawing.Color.Red
                        Else

                            Select Case e.Column.FieldName
                                Case "FNBalCutQuantity"
                                    If Double.Parse(.GetRowCellValue(e.RowHandle, "FNCutQuantity")) > 0 Then
                                        e.Appearance.BackColor = Drawing.Color.MintCream
                                        e.Appearance.ForeColor = Drawing.Color.Blue
                                    End If
                                Case "FNBalSuplQuantity"
                                    If Double.Parse(.GetRowCellValue(e.RowHandle, "FNRcvSuplQuantity")) > 0 Then
                                        e.Appearance.BackColor = Drawing.Color.MintCream
                                        e.Appearance.ForeColor = Drawing.Color.Blue
                                    End If
                                Case "FNBalSewQuantity"
                                    If Double.Parse(.GetRowCellValue(e.RowHandle, "FNSewQuantity")) > 0 Then
                                        e.Appearance.BackColor = Drawing.Color.MintCream
                                        e.Appearance.ForeColor = Drawing.Color.Blue
                                    End If
                                Case "FNBalPackQuantity"
                                    If Double.Parse(.GetRowCellValue(e.RowHandle, "FNSewOutQuantity")) > 0 Then
                                        e.Appearance.BackColor = Drawing.Color.MintCream
                                        e.Appearance.ForeColor = Drawing.Color.Blue
                                    End If
                                Case "FNCutBalQuantity"
                                    If Double.Parse(.GetRowCellValue(e.RowHandle, "FNGrandQuantity")) > 0 Then
                                        e.Appearance.BackColor = Drawing.Color.MintCream
                                        e.Appearance.ForeColor = Drawing.Color.Blue
                                    End If
                            End Select

                        End If

                End Select
            End With
        Catch ex As Exception

        End Try
    End Sub

    Private Sub oogvdetailcolorsize_RowCellStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles ogvdetailcolorsize.RowCellStyle
        Try
            With Me.ogvdetailcolorsize
                Select Case e.Column.FieldName
                    Case "FNCutQuantity"
                        If Double.Parse(.GetRowCellValue(e.RowHandle, "FNCutQuantity")) < Double.Parse(.GetRowCellValue(e.RowHandle, "FNGrandQuantity")) Then
                            e.Appearance.BackColor = Drawing.Color.LemonChiffon
                            e.Appearance.ForeColor = Drawing.Color.Red
                        Else
                            e.Appearance.BackColor = Drawing.Color.MintCream
                            e.Appearance.ForeColor = Drawing.Color.Blue
                        End If
                    Case "FNRcvSuplQuantity"
                        If Double.Parse(.GetRowCellValue(e.RowHandle, "FNSendSuplQuantity")) > 0 Then
                            If Double.Parse(.GetRowCellValue(e.RowHandle, "FNRcvSuplQuantity")) < Double.Parse(.GetRowCellValue(e.RowHandle, "FNSendSuplQuantity")) Then
                                e.Appearance.BackColor = Drawing.Color.LemonChiffon
                                e.Appearance.ForeColor = Drawing.Color.Red
                            Else
                                e.Appearance.BackColor = Drawing.Color.MintCream
                                e.Appearance.ForeColor = Drawing.Color.Blue
                            End If
                        End If

                    Case "FNSPMKQuantity"
                        If Double.Parse(.GetRowCellValue(e.RowHandle, "FNCutQuantity")) > 0 Then
                            If Double.Parse(.GetRowCellValue(e.RowHandle, "FNSPMKQuantity")) < Double.Parse(.GetRowCellValue(e.RowHandle, "FNCutQuantity")) Then
                                e.Appearance.BackColor = Drawing.Color.LemonChiffon
                                e.Appearance.ForeColor = Drawing.Color.Red
                            Else
                                e.Appearance.BackColor = Drawing.Color.MintCream
                                e.Appearance.ForeColor = Drawing.Color.Blue
                            End If
                        End If
                    Case "FNSewQuantity"
                        If Double.Parse(.GetRowCellValue(e.RowHandle, "FNSPMKQuantity")) > 0 Then
                            If Double.Parse(.GetRowCellValue(e.RowHandle, "FNSewQuantity")) < Double.Parse(.GetRowCellValue(e.RowHandle, "FNSPMKQuantity")) Then
                                e.Appearance.BackColor = Drawing.Color.LemonChiffon
                                e.Appearance.ForeColor = Drawing.Color.Red
                            Else
                                e.Appearance.BackColor = Drawing.Color.MintCream
                                e.Appearance.ForeColor = Drawing.Color.Blue
                            End If
                        End If

                    Case "FNSewOutQuantity"
                        If Double.Parse(.GetRowCellValue(e.RowHandle, "FNSewQuantity")) > 0 Then
                            If Double.Parse(.GetRowCellValue(e.RowHandle, "FNSewOutQuantity")) < Double.Parse(.GetRowCellValue(e.RowHandle, "FNSewQuantity")) Then
                                e.Appearance.BackColor = Drawing.Color.LemonChiffon
                                e.Appearance.ForeColor = Drawing.Color.Red
                            Else
                                e.Appearance.BackColor = Drawing.Color.MintCream
                                e.Appearance.ForeColor = Drawing.Color.Blue
                            End If
                        End If

                    Case "FNPackQuantity"
                        If Double.Parse(.GetRowCellValue(e.RowHandle, "FNSewOutQuantity")) > 0 Then
                            If Double.Parse(.GetRowCellValue(e.RowHandle, "FNPackQuantity")) < Double.Parse(.GetRowCellValue(e.RowHandle, "FNSewOutQuantity")) Then
                                e.Appearance.BackColor = Drawing.Color.LemonChiffon
                                e.Appearance.ForeColor = Drawing.Color.Red

                            Else
                                e.Appearance.BackColor = Drawing.Color.MintCream
                                e.Appearance.ForeColor = Drawing.Color.Blue
                            End If
                        End If
                    Case "FNBalCutQuantity", "FNBalSuplQuantity", "FNBalSewQuantity", "FNBalPackQuantity", "FNCutBalQuantity"
                        If Double.Parse(.GetRowCellValue(e.RowHandle, e.Column.FieldName)) > 0 Then
                            e.Appearance.BackColor = Drawing.Color.LemonChiffon
                            e.Appearance.ForeColor = Drawing.Color.Red
                        Else

                            Select Case e.Column.FieldName
                                Case "FNBalCutQuantity"
                                    If Double.Parse(.GetRowCellValue(e.RowHandle, "FNCutQuantity")) > 0 Then
                                        e.Appearance.BackColor = Drawing.Color.MintCream
                                        e.Appearance.ForeColor = Drawing.Color.Blue
                                    End If
                                Case "FNBalSuplQuantity"
                                    If Double.Parse(.GetRowCellValue(e.RowHandle, "FNRcvSuplQuantity")) > 0 Then
                                        e.Appearance.BackColor = Drawing.Color.MintCream
                                        e.Appearance.ForeColor = Drawing.Color.Blue
                                    End If
                                Case "FNBalSewQuantity"
                                    If Double.Parse(.GetRowCellValue(e.RowHandle, "FNSewQuantity")) > 0 Then
                                        e.Appearance.BackColor = Drawing.Color.MintCream
                                        e.Appearance.ForeColor = Drawing.Color.Blue
                                    End If
                                Case "FNBalPackQuantity"
                                    If Double.Parse(.GetRowCellValue(e.RowHandle, "FNSewOutQuantity")) > 0 Then
                                        e.Appearance.BackColor = Drawing.Color.MintCream
                                        e.Appearance.ForeColor = Drawing.Color.Blue
                                    End If
                                Case "FNCutBalQuantity"
                                    If Double.Parse(.GetRowCellValue(e.RowHandle, "FNGrandQuantity")) > 0 Then
                                        e.Appearance.BackColor = Drawing.Color.MintCream
                                        e.Appearance.ForeColor = Drawing.Color.Blue
                                    End If
                            End Select

                        End If

                End Select
            End With
        Catch ex As Exception

        End Try
    End Sub


End Class
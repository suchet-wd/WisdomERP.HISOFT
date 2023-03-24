Imports DevExpress.XtraCharts
Imports DevExpress.XtraPrinting
Imports DevExpress.XtraPrintingLinks

Public Class wQCSendSuplChart

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub wQCSendSuplChart_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Me.FNHSysCmpId.Text = HI.ST.SysInfo.CmpCode
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ocmload_Click(sender As Object, e As EventArgs) Handles ocmload.Click
        Try
            If Verrify() Then
                Call LoadData()
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Function Verrify() As Boolean
        Try
            If Me.FTStartSendSupl.Text = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.FTStartSendSupl_lbl.Text)
                Me.FTStartSendSupl.Focus()
                Return False
            End If
            If Me.FTEndSendSupl.Text = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.FTEndSendSupl_lbl.Text)
                Me.FTEndSendSupl.Focus()
                Return False
            End If
            If Me.FNHSysSuplId.Text = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.FNHSysSuplId_lbl.Text)
                Me.FNHSysSuplId.Focus()
                Return False
            End If
            Return True
        Catch ex As Exception
        End Try
    End Function

    Private Sub LoadData()
        Dim _Spls As New HI.TL.SplashScreen("Loading Data... Please Wait... ")
        Try

            Dim _Cmd As String = ""
            '_Cmd = " Select  BB.FNQuantity , T.FTBarcodeSendSuplNo , sum(T.FNFacDefectQty) AS FNFacDefectQty  ,R.FDRcvSuplDate ,S.FTSuplCode , T.FTStateFromSupl"
            _Cmd = " Select  sum(BB.FNQuantity) AS FNQuantity ,   sum(T.FNFacDefectQty) AS FNFacDefectQty  ,S.FTSuplCode , T.FTStateFromSupl"
            '_Cmd &= vbCrLf & " ,O.FTOrderNo , L.FTStyleCode"
            _Cmd &= vbCrLf & " ,DATEPART(week,convert(date,T.FDInsDate)) AS FTWeek"
            _Cmd &= vbCrLf & "From (SELECT    Q.FDInsDate"
            '_Cmd &= vbCrLf & ",  (Select Top 1 B.FTBarcodeSendSuplNo From   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcode_SendSupl AS B  WITH (NOLOCK) "
            '_Cmd &= vbCrLf & "LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd AS P  WITH (NOLOCK)  ON B.FTOrderProdNo = P.FTOrderProdNo"
            '_Cmd &= vbCrLf & "INNER JOIN   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH (NOLOCK)  ON P.FTOrderNo = O.FTOrderNo"
            '_Cmd &= vbCrLf & "LEFT OUTER JOIN    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS T WITH (NOLOCK)  ON O.FNHSysStyleId = T.FNHSysStyleId"
            '_Cmd &= vbCrLf & "INNER JOIN   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle AS BB WITH (NOLOCK)  ON B.FTBarcodeBundleNo = BB.FTBarcodeBundleNo"
            '_Cmd &= vbCrLf & " where O.FTOrderNo = D.FTOrderNo"
            '_Cmd &= vbCrLf & "and T.FTStyleCode =D.FTStyleCode"
            '_Cmd &= vbCrLf & "and BB.FNBunbleSeq = D.FNBunbleSeq"
            '_Cmd &= vbCrLf & "and BB.FTColorway =D.FTColorway"
            '_Cmd &= vbCrLf & "and BB.FTSizeBreakDown = D.FTSizeBreakDown)  AS FTBarcodeSendSuplNo"
            _Cmd &= vbCrLf & " , B.FTBarcodeSendSuplNo as FTBarcodeSendSuplNo ,     D.FNDefectQty as FNFacDefectQty  , ISNULL(Q.FTStateFromSupl, '0') as FTStateFromSupl"
            _Cmd &= vbCrLf & "FROM    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPROSendSuplDefect AS Q WITH (NOLOCK)  LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPROSendSuplDefect_Detail AS D  WITH (NOLOCK) ON Q.FTBarcodeSendSuplNo = D.FTBarcodeSendSuplNo"
            _Cmd &= vbCrLf & "  INNER JOIN     [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "]..TPRODTSendSupl_Barcode  AS B WITH(NOLOCK) ON LEFT(Q.FTBarcodeSendSuplNo,16) = B.FTSendSuplNo "
            _Cmd &= vbCrLf & "Where  Q.FTStateFromSupl='1' and  Q.FTStateActive = '1' "
            _Cmd &= vbCrLf & "UNION ALL "
            _Cmd &= vbCrLf & "SELECT     T.FDInsDate,   FTBarcodeSendSuplNo, FNDefectQty , ISNULL(FTStateFromSupl, '0') as FTStateFromSupl"
            _Cmd &= vbCrLf & "FROM    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPROSendSuplDefect AS T WITH (NOLOCK)"
            _Cmd &= vbCrLf & "WHERE        (ISNULL(FTStateFromSupl, '0') = '0')"
            _Cmd &= vbCrLf & ") AS T INNER JOIN (SELECT        R.FDRcvSuplDate, B.FTBarcodeSendSuplNo"
            _Cmd &= vbCrLf & "FROM    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTReceiveSupl AS R WITH (NOLOCK) LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTReceiveSupl_Barcode AS B WITH (NOLOCK) ON R.FTRcvSuplNo = B.FTRcvSuplNo )  AS R   ON T.FTBarcodeSendSuplNo = R.FTBarcodeSendSuplNo and   T.FDInsDate = R.FDRcvSuplDate "
            _Cmd &= vbCrLf & "LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcode_SendSupl AS B WITH(NOLOCK) ON R.FTBarcodeSendSuplNo = B.FTBarcodeSendSuplNo"
            _Cmd &= vbCrLf & "LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.V_TPRODTBundle_MainBarcode AS BB WITH(NOLOCK) ON B.FTBarcodeBundleNo = BB.FTBarcodeBundleNo"
            _Cmd &= vbCrLf & "LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier AS S WITH(NOLOCK) ON B.FNHSysSuplId = S.FNHSysSuplId"
            _Cmd &= vbCrLf & "LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH(NOLOCK) ON REPLACE(B.FTOrderProdNo, RIGHT(B.FTOrderProdNo,4),'') = O.FTOrderNo"
            _Cmd &= vbCrLf & "LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS L WITH(NOLOCK) ON O.FNHSysStyleId = L.FNHSysStyleId"
            _Cmd &= vbCrLf & "where R.FDRcvSuplDate <> ''"
            If Me.FNHSysStyleId.Text <> "" Then
                _Cmd &= vbCrLf & "  AND L.FTStyleCode='" & HI.UL.ULF.rpQuoted(Me.FNHSysStyleId.Text) & "'"
            End If
            If Me.FTOrderNo.Text <> "" Then
                _Cmd &= vbCrLf & " AND O.FTOrderNo >='" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "'"
            End If
            If Me.FTOrderNoTo.Text <> "" Then
                _Cmd &= vbCrLf & " AND O.FTOrderNo <='" & HI.UL.ULF.rpQuoted(Me.FTOrderNoTo.Text) & "'"
            End If
            If Me.FTStartSendSupl.Text <> "" Then
                _Cmd &= vbCrLf & " AND R.FDRcvSuplDate >='" & HI.UL.ULDate.ConvertEnDB(Me.FTStartSendSupl.Text) & "'"
            End If
            If Me.FTEndSendSupl.Text <> "" Then
                _Cmd &= vbCrLf & " AND R.FDRcvSuplDate <='" & HI.UL.ULDate.ConvertEnDB(Me.FTEndSendSupl.Text) & "'"
            End If
            If Me.FNHSysSuplId.Text <> "" Then
                _Cmd &= vbCrLf & " AND S.FTSuplCode ='" & HI.UL.ULF.rpQuoted(Me.FNHSysSuplId.Text) & "'"
            End If
            '_Cmd &= vbCrLf & "Group by  BB.FNQuantity , T.FTBarcodeSendSuplNo ,  R.FDRcvSuplDate,S.FTSuplCode, T.FTStateFromSupl,B.FTOrderProdNo,O.FTOrderNo, L.FTStyleCode"
            _Cmd &= vbCrLf & "Group by S.FTSuplCode , T.FTStateFromSupl ,DATEPART(week,convert(date,T.FDInsDate))  "
            _Cmd &= vbCrLf & "Order by DATEPART(week,convert(date,T.FDInsDate)) asc"
            Dim _oDt As DataTable = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD)
            'Dim _dtChart As New DataTable
            'With _dtChart
            '    .Columns.Add("FTStyleCode", GetType(String))
            '    .Columns.Add("FNQuantity", GetType(Double))
            'End With
            'For Each R As DataRow In _oDt.Rows

            'Next

            _Cmd = "SELECT        SUM(BB.FNQuantity) AS FNQuantity "
            _Cmd &= vbCrLf & "FROM            (SELECT        R.FDRcvSuplDate, B.FTBarcodeSendSuplNo"
            _Cmd &= vbCrLf & " FROM   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTReceiveSupl AS R WITH (NOLOCK) LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTReceiveSupl_Barcode AS B WITH (NOLOCK) ON R.FTRcvSuplNo = B.FTRcvSuplNo) AS R_1 LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcode_SendSupl AS B WITH (NOLOCK) ON R_1.FTBarcodeSendSuplNo = B.FTBarcodeSendSuplNo LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.V_TPRODTBundle_MainBarcode AS BB WITH (NOLOCK) ON B.FTBarcodeBundleNo = BB.FTBarcodeBundleNo LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier AS S WITH (NOLOCK) ON B.FNHSysSuplId = S.FNHSysSuplId LEFT OUTER JOIN"
            _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH (NOLOCK) ON REPLACE(B.FTOrderProdNo, RIGHT(B.FTOrderProdNo, 4), '') = O.FTOrderNo LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS L WITH (NOLOCK) ON O.FNHSysStyleId = L.FNHSysStyleId"
            _Cmd &= vbCrLf & "WHERE  (R_1.FDRcvSuplDate <> '') "
            If Me.FNHSysStyleId.Text <> "" Then
                _Cmd &= vbCrLf & "  AND L.FTStyleCode='" & HI.UL.ULF.rpQuoted(Me.FNHSysStyleId.Text) & "'"
            End If
            If Me.FTOrderNo.Text <> "" Then
                _Cmd &= vbCrLf & " AND O.FTOrderNo >='" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "'"
            End If
            If Me.FTOrderNoTo.Text <> "" Then
                _Cmd &= vbCrLf & " AND O.FTOrderNo <='" & HI.UL.ULF.rpQuoted(Me.FTOrderNoTo.Text) & "'"
            End If
            If Me.FTStartSendSupl.Text <> "" Then
                _Cmd &= vbCrLf & " AND R_1.FDRcvSuplDate >='" & HI.UL.ULDate.ConvertEnDB(Me.FTStartSendSupl.Text) & "'"
            End If
            If Me.FTEndSendSupl.Text <> "" Then
                _Cmd &= vbCrLf & " AND R_1.FDRcvSuplDate <='" & HI.UL.ULDate.ConvertEnDB(Me.FTEndSendSupl.Text) & "'"
            End If
            If Me.FNHSysSuplId.Text <> "" Then
                _Cmd &= vbCrLf & " AND S.FTSuplCode ='" & HI.UL.ULF.rpQuoted(Me.FNHSysSuplId.Text) & "'"
            End If
            Dim GrandTotalRcv As Double = HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_PROD, "0")
            Call SetBarChart(_oDt, GrandTotalRcv)
            Call SetLineChart(_oDt)


            '_Cmd = " Select  sum(BB.FNQuantity) AS FNQuantity ,   sum(T.FNFacDefectQty) AS FNFacDefectQty  ,S.FTSuplCode , T.FTStateFromSupl"
            '_Cmd &= vbCrLf & ",DATEPART(week,convert(date,T.FDInsDate)) AS FTWeek"
            '_Cmd &= vbCrLf & "From (SELECT    Q.FDInsDate,   (Select Top 1 B.FTBarcodeSendSuplNo From   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcode_SendSupl AS B  WITH (NOLOCK) "
            '_Cmd &= vbCrLf & "LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd AS P  WITH (NOLOCK)  ON B.FTOrderProdNo = P.FTOrderProdNo"
            '_Cmd &= vbCrLf & "INNER JOIN   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH (NOLOCK)  ON P.FTOrderNo = O.FTOrderNo"
            '_Cmd &= vbCrLf & "LEFT OUTER JOIN    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS T WITH (NOLOCK)  ON O.FNHSysStyleId = T.FNHSysStyleId"
            '_Cmd &= vbCrLf & "INNER JOIN   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle AS BB WITH (NOLOCK)  ON B.FTBarcodeBundleNo = BB.FTBarcodeBundleNo"
            '_Cmd &= vbCrLf & " where O.FTOrderNo = D.FTOrderNo"
            '_Cmd &= vbCrLf & "and T.FTStyleCode =D.FTStyleCode"
            '_Cmd &= vbCrLf & "and BB.FNBunbleSeq = D.FNBunbleSeq"
            '_Cmd &= vbCrLf & "and BB.FTColorway =D.FTColorway"
            '_Cmd &= vbCrLf & "and BB.FTSizeBreakDown = D.FTSizeBreakDown)  AS FTBarcodeSendSuplNo,     Q.FNFacDefectQty , ISNULL(Q.FTStateFromSupl, '0') as FTStateFromSupl"
            '_Cmd &= vbCrLf & "FROM    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPROSendSuplDefect AS Q WITH (NOLOCK)  LEFT OUTER JOIN"
            '_Cmd &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPROSendSuplDefect_Detail AS D  WITH (NOLOCK) ON Q.FTBarcodeSendSuplNo = D.FTBarcodeSendSuplNo"
            '_Cmd &= vbCrLf & "Where  Q.FTStateFromSupl='1' and  Q.FTStateActive = '1' "
            '_Cmd &= vbCrLf & "UNION ALL "
            '_Cmd &= vbCrLf & "SELECT     FDInsDate,  FTBarcodeSendSuplNo, FNDefectQty , ISNULL(FTStateFromSupl, '0') as FTStateFromSupl"
            '_Cmd &= vbCrLf & "FROM    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPROSendSuplDefect AS T WITH (NOLOCK)"
            '_Cmd &= vbCrLf & "WHERE        (ISNULL(FTStateFromSupl, '0') = '0')"
            '_Cmd &= vbCrLf & ") AS T INNER JOIN (SELECT        R.FDRcvSuplDate, B.FTBarcodeSendSuplNo"
            '_Cmd &= vbCrLf & "FROM    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTReceiveSupl AS R WITH (NOLOCK) LEFT OUTER JOIN"
            '_Cmd &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTReceiveSupl_Barcode AS B WITH (NOLOCK) ON R.FTRcvSuplNo = B.FTRcvSuplNo )  AS R   ON T.FTBarcodeSendSuplNo = R.FTBarcodeSendSuplNo"
            '_Cmd &= vbCrLf & "LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcode_SendSupl AS B WITH(NOLOCK) ON R.FTBarcodeSendSuplNo = B.FTBarcodeSendSuplNo"
            '_Cmd &= vbCrLf & "LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle AS BB WITH(NOLOCK) ON B.FTBarcodeBundleNo = BB.FTBarcodeBundleNo"
            '_Cmd &= vbCrLf & "LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier AS S WITH(NOLOCK) ON B.FNHSysSuplId = S.FNHSysSuplId"
            '_Cmd &= vbCrLf & "LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH(NOLOCK) ON REPLACE(B.FTOrderProdNo, RIGHT(B.FTOrderProdNo,4),'') = O.FTOrderNo"
            '_Cmd &= vbCrLf & "LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS L WITH(NOLOCK) ON O.FNHSysStyleId = L.FNHSysStyleId"
            '_Cmd &= vbCrLf & "where R.FDRcvSuplDate <> ''"
            'If Me.FNHSysStyleId.Text <> "" Then
            '    _Cmd &= vbCrLf & "  AND L.FTStyleCode='" & HI.UL.ULF.rpQuoted(Me.FNHSysStyleId.Text) & "'"
            'End If
            'If Me.FTOrderNo.Text <> "" Then
            '    _Cmd &= vbCrLf & " AND O.FTOrderNo >='" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "'"
            'End If
            'If Me.FTOrderNoTo.Text <> "" Then
            '    _Cmd &= vbCrLf & " AND O.FTOrderNo <='" & HI.UL.ULF.rpQuoted(Me.FTOrderNoTo.Text) & "'"
            'End If
            'If Me.FTStartSendSupl.Text <> "" Then
            '    _Cmd &= vbCrLf & " AND T.FDInsDate  >='" & HI.UL.ULDate.ConvertEnDB(Me.FTStartSendSupl.Text) & "'"
            'End If
            'If Me.FTEndSendSupl.Text <> "" Then
            '    _Cmd &= vbCrLf & " AND T.FDInsDate  <='" & HI.UL.ULDate.ConvertEnDB(Me.FTEndSendSupl.Text) & "'"
            'End If
            'If Me.FNHSysSuplId.Text <> "" Then
            '    _Cmd &= vbCrLf & " AND S.FTSuplCode ='" & HI.UL.ULF.rpQuoted(Me.FNHSysSuplId.Text) & "'"
            'End If

            '_Cmd &= vbCrLf & "Group by S.FTSuplCode , T.FTStateFromSupl ,DATEPART(week,convert(date,T.FDInsDate))  "
            'Dim _oLDt As DataTable = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD)
            'Call SetLineChart(_oLDt)



            _Cmd = "    Select Top 5 T.FNHSysQCSuplDetailId , sum(T.FNDefectQty) AS FNDefectQty "
            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Cmd &= vbCrLf & ",max(Q.FTQCSupDetailNameTH) AS FTQCSupDetailName"
            Else
                _Cmd &= vbCrLf & ",max(Q.FTQCSupDetailNameEN) AS FTQCSupDetailName"
            End If
            _Cmd &= vbCrLf & " From(SELECT        Q.FDInsDate, Q.FTBarcodeSendSuplNo, D.FNHSysQCSuplDetailId , D.FNDefectQty"
            _Cmd &= vbCrLf & "FROM    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPROSendSuplDefect AS Q WITH (NOLOCK) LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPROSendSuplDefect_Detail AS D WITH (NOLOCK) ON Q.FTBarcodeSendSuplNo = D.FTBarcodeSendSuplNo"
            _Cmd &= vbCrLf & "WHERE        (Q.FTStateFromSupl = '1') AND (Q.FTStateActive = '1')"
            _Cmd &= vbCrLf & "and D.FNHSysQCSuplDetailId <> 0"
            _Cmd &= vbCrLf & " UNION ALL"
            _Cmd &= vbCrLf & "SELECT        Q.FDInsDate, Q.FTBarcodeSendSuplNo, D.FNHSysQCSuplDetailId  , 1 as FNDefectQty"
            _Cmd &= vbCrLf & "FROM   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPROSendSuplDefect AS Q WITH (NOLOCK) LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPROSendSuplDefect_Detail AS D WITH (NOLOCK) ON Q.FTBarcodeSendSuplNo = D.FTBarcodeSendSuplNo"
            _Cmd &= vbCrLf & "WHERE        (Isnull(Q.FTStateFromSupl,'0') = '0') ) AS T LEFT OUTER JOIN [HITECH_MASTER]..TQAMQCSuplDetail AS Q WITH(NOLOCK) ON T.FNHSysQCSuplDetailId = Q.FNHSysQCSuplDetailId"
            _Cmd &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcode_SendSupl AS B WITH (NOLOCK)  ON T.FTBarcodeSendSuplNo = B.FTBarcodeSendSuplNo "
            _Cmd &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd AS P WITH(NOLOCK) ON B.FTOrderProdNo = P.FTOrderProdNo"
            _Cmd &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH(NOLOCK) ON P.FTOrderNo = O.FTOrderNo "
            _Cmd &= vbCrLf & "LEFT OUTER JOIN (SELECT        R.FTRcvSuplNo, R.FDRcvSuplDate, R.FNHSysSuplId, B.FTBarcodeSendSuplNo"
            _Cmd &= vbCrLf & "FROM    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTReceiveSupl AS R WITH (NOLOCK) LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTReceiveSupl_Barcode AS B WITH (NOLOCK) ON R.FTRcvSuplNo = B.FTRcvSuplNo) AS RS ON T.FTBarcodeSendSuplNo = RS.FTBarcodeSendSuplNo "
            _Cmd &= vbCrLf & "Where T.FDInsDate <> ''  AND T.FNHSysQCSuplDetailId <>''"

            If Me.FTStartSendSupl.Text <> "" Then
                _Cmd &= vbCrLf & " AND RS.FDRcvSuplDate  >='" & HI.UL.ULDate.ConvertEnDB(Me.FTStartSendSupl.Text) & "'"
            End If

            If Me.FTEndSendSupl.Text <> "" Then
                _Cmd &= vbCrLf & " AND RS.FDRcvSuplDate  <='" & HI.UL.ULDate.ConvertEnDB(Me.FTEndSendSupl.Text) & "'"
            End If

            If Me.FTOrderNo.Text <> "" Then
                _Cmd &= vbCrLf & " AND P.FTOrderNo >='" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "'"
            End If

            If Me.FTOrderNoTo.Text <> "" Then
                _Cmd &= vbCrLf & " AND P.FTOrderNo <='" & HI.UL.ULF.rpQuoted(Me.FTOrderNoTo.Text) & "'"
            End If

            If Me.FNHSysStyleId.Text <> "" Then
                _Cmd &= vbCrLf & "  AND O.FNHSysStyleId=" & Integer.Parse("0" & Me.FNHSysStyleId.Properties.Tag)
            End If

            If Me.FNHSysSuplId.Text <> "" Then
                _Cmd &= vbCrLf & " AND  B.FNHSysSuplId =" & Integer.Parse("0" & Me.FNHSysSuplId.Properties.Tag)
            End If

            _Cmd &= vbCrLf & "Group by   T.FNHSysQCSuplDetailId"
            _Cmd &= vbCrLf & "order by sum(T.FNDefectQty) desc "

            Dim _oTDt As DataTable = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD)
            Call SetTopChart(_oTDt)

            _Spls.Close()
        Catch ex As Exception
            _Spls.close()
        End Try
    End Sub

    Private Sub SetBarChart(_oDt As DataTable, GrandTotalRcv As Double)
        Try
            Dim series1 As Series
            Dim series2 As Series
            Dim Per As Double = 0
            Dim DefectQty As Double = 0
            Me.oBarChart.Series.Clear()
            series1 = New Series(HI.ST.SysInfo.CmpCode, ViewType.Bar3D)
            For Each R As DataRow In _oDt.Select("FTStateFromSupl='0'")
                DefectQty = DefectQty + Double.Parse(R!FNFacDefectQty.ToString)
            Next
            Per = (DefectQty / GrandTotalRcv) * 100
            series1.Points.Add(New SeriesPoint(HI.ST.SysInfo.CmpCode, New Double() {CInt("0" & Per)}))

            For Each R As DataRow In _oDt.Select("FTStateFromSupl='1'")
                series2 = New Series(R!FTSuplCode.ToString, ViewType.Bar3D)
                Per = (Double.Parse(R!FNFacDefectQty.ToString) / GrandTotalRcv) * 100
                series2.Points.Add(New SeriesPoint(R!FTSuplCode.ToString, New Double() {CInt("0" & Per)}))
                Me.oBarChart.Series.Add(series2)

            Next

            'Try
            '    If TypeOf Me.oBarChart.Diagram Is Diagram3D Then
            '        Dim diagram As Diagram3D = CType(Me.oBarChart.Diagram, Diagram3D)
            '        diagram.RuntimeRotation = True
            '        diagram.RuntimeZooming = True
            '        diagram.RuntimeScrolling = True
            '    End If
            'Catch ex As Exception
            'End Try

            ''series1.Label.PointOptions.PointView = PointView.ArgumentAndValues
            'series1.Label.PointOptions.ValueNumericOptions.Format = NumericFormat.Percent
            'series1.Label.PointOptions.ValueNumericOptions.Precision = 2
            ''series2.Label.PointOptions.PointView = PointView.ArgumentAndValues
            'series2.Label.PointOptions.ValueNumericOptions.Format = NumericFormat.Percent
            'series2.Label.PointOptions.ValueNumericOptions.Precision = 2

            Me.oBarChart.Series.Add(series1)
            ' Me.oBarChart.Series.Add(series2)

        Catch ex As Exception
        End Try
    End Sub

    Private Sub SetLineChart(_oDt As DataTable)
        Try
            Dim series1 As Series
            Dim series2 As Series
            Dim _Week As Integer = CInt("0" & _oDt.Rows(0).Item("FTWeek").ToString)
            Me.oChartLine.Series.Clear()


            If TypeOf Me.oChartLine.Diagram Is XYDiagram Then
                Dim diagram As XYDiagram = CType(Me.oChartLine.Diagram, XYDiagram)
                diagram.AxisX.VisualRange.MinValue = 1
                diagram.AxisX.VisualRange.SideMarginsValue = 2
                diagram.AxisX.WholeRange.MinValue = 1
                diagram.AxisX.WholeRange.SideMarginsValue = 2
                For Each R As DataRow In _oDt.Select("FTStateFromSupl<>''", "FTWeek DESC")
                    diagram.AxisX.VisualRange.MaxValue = (CInt("0" & R!FTWeek.ToString) - _Week)
                    diagram.AxisX.WholeRange.MaxValue = (CInt("0" & R!FTWeek.ToString) - _Week)
                    Exit For
                Next
            End If

            series1 = New Series(HI.ST.SysInfo.CmpCode, ViewType.Line)
            For Each R As DataRow In _oDt.Select("FTStateFromSupl='1'")
                series2 = New Series(R!FTSuplCode.ToString, ViewType.Line)
                Exit For
            Next
            For Each R As DataRow In _oDt.Select("FTStateFromSupl='0'")
                series1.Points.Add(New SeriesPoint((CShort("0" & R!FTWeek.ToString) - _Week) + 1, New Double() {Format(((CInt("0" & R!FNFacDefectQty.ToString) / CInt("0" & R!FNQuantity.ToString)) * 100), "0.00")}))
            Next
            For Each R As DataRow In _oDt.Select("FTStateFromSupl='1'")
                'series2 = New Series(R!FTSuplCode.ToString, ViewType.Line)
                series2.Points.Add(New SeriesPoint((CShort("0" & R!FTWeek.ToString) - _Week) + 1, New Double() {Format(((CInt("0" & R!FNFacDefectQty.ToString) / CInt("0" & R!FNQuantity.ToString)) * 100), "0.00")}))

            Next
            Me.oChartLine.Series.Add(series1)
            ' Me.oChartLine.Series.Add(series2)


        Catch ex As Exception

        End Try
    End Sub

    Private Sub SetTopChart(_oDt As DataTable)
        Try
            Dim series1 As New Series("", ViewType.Pie3D)
            Me.ocharttop.Series.Clear()
            For Each R As DataRow In _oDt.Rows
                series1.Points.Add(New SeriesPoint(R!FTQCSupDetailName.ToString, New Double() {CInt("0" & R!FNDefectQty.ToString)}))
            Next
            series1.Label.PointOptions.PointView = PointView.ArgumentAndValues
            series1.Label.PointOptions.ValueNumericOptions.Format = NumericFormat.Percent
            series1.Label.PointOptions.ValueNumericOptions.Precision = 2
            Me.ocharttop.Series.Add(series1)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Try
            Me.Close()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmexporttoexcel_Click(sender As Object, e As EventArgs) Handles ocmexporttoexcel.Click
        Try
            Dim Op As New System.Windows.Forms.SaveFileDialog
            Op.Filter = "Excel Files(.xlsx)|*.xlsx"
            Op.ShowDialog()
            Try
                If Op.FileName <> "" Then
                    Dim prtSys = New PrintingSystemBase()
                    Dim csLink = New CompositeLinkBase()
                    csLink.PrintingSystemBase = prtSys
                    Dim _Plink1 = New PrintableComponentLinkBase()
                    Dim _Plink2 = New PrintableComponentLinkBase()
                    Dim _Plink3 = New PrintableComponentLinkBase()
                    _Plink1.Component = Me.oBarChart
                    _Plink2.Component = Me.oChartLine
                    _Plink3.Component = Me.ocharttop
                    csLink.Links.Add(_Plink1)
                    csLink.Links.Add(_Plink2)
                    csLink.Links.Add(_Plink3)
                    Dim options = New XlsxExportOptions()
                    options.ExportMode = XlsxExportMode.SingleFilePageByPage
                    csLink.CreatePageForEachLink()
                    csLink.ExportToXlsx(Op.FileName, options)
                    Try
                        Process.Start(Op.FileName)
                    Catch ex As Exception
                    End Try
                End If
            Catch ex As Exception
            End Try
        Catch ex As Exception
        End Try
    End Sub
End Class
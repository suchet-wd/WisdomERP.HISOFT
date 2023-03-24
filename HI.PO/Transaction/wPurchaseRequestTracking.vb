Public Class wPurchaseRequestTracking



    Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        Dim oSysLang As New ST.SysLanguage



        ' Add any initialization after the InitializeComponent() call.

    End Sub


    Private Sub wPurchaseOrderByPR_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try

        Catch ex As Exception

        End Try
    End Sub

    Private _pDt As DataTable
    Private Sub LoadGridDetail()
        Dim _Qry As String = ""
        Dim _dt As DataTable

        _Qry = "SELECT   PR.FTOrderNo, PR.FNQuantity, PR.FTRemark,PR.FNHSysRawMatId,PR.FNHSysUnitId "
        _Qry &= vbCrLf & " ,PR.FTPRPurchaseNo "
        _Qry &= vbCrLf & " ,PO.FTPurchaseNo "
        _Qry &= vbCrLf & ",CASE WHEN ISDATE(PO.FDPurchaseDate)=1 THEN convert(nvarchar(10), convert(datetime,PO.FDPurchaseDate),103) ELSE'' END AS FDPurchaseDate"
        _Qry &= vbCrLf & " ,PO.FTPurchaseBy "
        _Qry &= vbCrLf & " ,PR.FNHSysUnitId  as FNHSysUnitIdPO_Hide "

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Qry &= vbCrLf & " , M.FTRawMatNameTH AS FTMatDesc"
            _Qry &= vbCrLf & ",C.FTRawMatColorNameTH AS  FTRawMatColorName "
        Else
            _Qry &= vbCrLf & " , M.FTRawMatNameEN AS FTMatDesc"
            _Qry &= vbCrLf & ",C.FTRawMatColorNameEN AS  FTRawMatColorName "
        End If

        _Qry &= vbCrLf & " , M.FTRawMatColorNameTH  , M.FTRawMatColorNameEN"
        _Qry &= vbCrLf & ", ISNULL(C.FTRawMatColorCode,'') AS FTRawMatColorCode"
        _Qry &= vbCrLf & ", ISNULL(S.FTRawMatSizeCode,'') AS FTRawMatSizeCode"
        _Qry &= vbCrLf & ", ISNULL(PR.FTFabricFrontSize,'') AS FTFabricFrontSize"
        _Qry &= vbCrLf & ", ISNULL(U.FTUnitCode,'') AS FTUnitCode, M.FTRawMatCode , ISNULL(U.FTUnitCode,'') AS FNHSysUnitIdPO "
        _Qry &= vbCrLf & ",CASE WHEN ISDATE(P.FDPRPurchaseDate)=1 THEN convert(nvarchar(10), convert(datetime,P.FDPRPurchaseDate),103) ELSE'' END AS FDPRPurchaseDate"
        _Qry &= vbCrLf & ",CASE WHEN ISDATE(P.FDPRRequestDate)=1 THEN convert(nvarchar(10), convert(datetime,P.FDPRRequestDate),103) ELSE'' END AS FDPRRequestDate"
        _Qry &= vbCrLf & ",CASE WHEN ISDATE(P.FTSendAppDate)=1 THEN convert(nvarchar(10), convert(datetime,P.FTSendAppDate),103) ELSE'' END AS FTSendAppDate"
        _Qry &= vbCrLf & ",CASE WHEN ISDATE(P.FTAppDate)=1 THEN convert(nvarchar(10), convert(datetime,P.FTAppDate),103) ELSE'' END AS FTAppDate"
        _Qry &= vbCrLf & ",T.FNPrice , 0.00 as FNDisPer , '' AS  FTFabricFrontSize  , 0.00 AS FNGrandNetAmt  , 0.00 AS FNDisAmt , 0.00 AS FNNetAmt , 0.00 as FNSurchangeAmt , 0.00 as FNSurchangePerUnit  "
        _Qry &= vbCrLf & " FROM"
        _Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_Request_OrderNo AS PR  WITH (NOLOCK)  LEFT OUTER JOIN"
        _Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_Request AS P  WITH (NOLOCK) ON PR.FTPRPurchaseNo=P.FTPRPurchaseNo LEFT OUTER JOIN"
        _Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS M  WITH (NOLOCK)  ON PR.FNHSysRawMatId = M.FNHSysRawMatId LEFT OUTER JOIN"
        _Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatColor AS C WITH (NOLOCK) ON M.FNHSysRawMatColorId = C.FNHSysRawMatColorId LEFT OUTER JOIN"
        _Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatSize AS S WITH (NOLOCK) ON M.FNHSysRawMatSizeId = S.FNHSysRawMatSizeId LEFT OUTER JOIN"
        _Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit AS U  WITH (NOLOCK) ON PR.FNHSysUnitId = U.FNHSysUnitId"
        _Qry &= vbCrLf & "     INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "]..TMERMMainMat AS T WITH(NOLOCK)  ON M.FTRawMatCode = T.FTMainMatCode "
        _Qry &= vbCrLf & "     LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "]..TPURTPurchase AS PO WITH(NOLOCK)  ON PR.FTPurchaseRefNo = PO.FTPurchaseNo "
        _Qry &= vbCrLf & "     WHERE PR.FTPRPurchaseNo <> '' "

        If FTPRPurchaseNo.Text <> "" Then

            _Qry &= vbCrLf & " AND PR.FTPRPurchaseNo ='" & HI.UL.ULF.rpQuoted(FTPRPurchaseNo.Text) & "' "

        End If

        If FNHSysCmpId.Text <> "" Then

            _Qry &= vbCrLf & " AND PR.FNHSysCmpId =" & Val(FNHSysCmpId.Properties.Tag.ToString) & " "

        End If

        If FTStartPurchaseDate.Text <> "" Then

            _Qry &= vbCrLf & " AND P.FDPRPurchaseDate >='" & HI.UL.ULDate.ConvertEnDB(FTStartPurchaseDate.Text) & "' "

        End If

        If FTEndPurchaseDate.Text <> "" Then

            _Qry &= vbCrLf & " AND P.FDPRPurchaseDate <='" & HI.UL.ULDate.ConvertEnDB(FTEndPurchaseDate.Text) & "' "

        End If

        If FTStartDelivery.Text <> "" Then

            _Qry &= vbCrLf & " AND P.FDPRRequestDate >='" & HI.UL.ULDate.ConvertEnDB(FTStartDelivery.Text) & "' "

        End If

        If FTEndDelivery.Text <> "" Then

            _Qry &= vbCrLf & " AND P.FDPRRequestDate <='" & HI.UL.ULDate.ConvertEnDB(FTEndDelivery.Text) & "' "

        End If

        _Qry &= vbCrLf & " ORDER BY PR.FTPRPurchaseNo,P.FDPRRequestDate, M.FTRawMatCode, ISNULL(C.FTRawMatColorCode,''), ISNULL(S.FTRawMatSizeCode,'') "
        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PUR)
        Me.ogcdetail.DataSource = _dt

    End Sub

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub


    Private Sub ocmload_Click(sender As Object, e As EventArgs) Handles ocmload.Click
        Try
            Call LoadGridDetail()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ogbmainprocbutton_Paint(sender As Object, e As System.Windows.Forms.PaintEventArgs) Handles ogbmainprocbutton.Paint

    End Sub

    Private Sub ogvdetail_RowStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles ogvdetail.RowStyle
        Try

            With Me.ogvdetail

                If "" & .GetRowCellValue(e.RowHandle, "FTPurchaseNo").ToString <> "" Then

                    e.Appearance.BackColor = Drawing.Color.LightYellow
                    e.Appearance.BackColor2 = Drawing.Color.Orange

                End If

            End With

        Catch ex As Exception

        End Try
    End Sub

    Private Sub ogrpdetail_Paint(sender As Object, e As System.Windows.Forms.PaintEventArgs) Handles ogrpdetail.Paint

    End Sub

End Class
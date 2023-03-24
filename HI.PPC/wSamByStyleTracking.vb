Imports System.Windows.Forms
Imports DevExpress.XtraEditors.Controls

Public Class wSamByStyleTracking

    Private _LoadData As Boolean = False

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        'FNSamCut_lbl.Visible = False
        'FNSamCut.Visible = False
        CFNCutPrice.Visible = False


    End Sub

#Region "Procedure"
    Public Sub LoadDataInfo()
        _LoadData = True

        Dim _Qry As String = ""
        Dim Spls As New HI.TL.SplashScreen("Loading Data....please wait")

        _Qry = " SELECT ST.FTStyleCode,A.FNHSysStyleId "
        _Qry &= vbCrLf & ",SS.FTSeasonCode "
        _Qry &= vbCrLf & ", A.FTOrderNo"
        _Qry &= vbCrLf & ", B.FTSubOrderNo,ISNULL(C.FNCostPerMin,0.00) AS FNStyleCostPerMin"
        _Qry &= vbCrLf & ", ISNULL(C.FNSam,0.00) AS FNSamByStyle"
        _Qry &= vbCrLf & ", ISNULL(C.FNPrice,0.00) AS FNSamPrice"
        _Qry &= vbCrLf & ", ISNULL(C.FNMultiple,1.00) AS FNSamByStyleFNMultiple"
        _Qry &= vbCrLf & ", ISNULL(C.FNSam,0)  AS FNSamST"
        _Qry &= vbCrLf & ", ISNULL(C.FNCostPerMin, 0)  AS FNCostPerMinST"
        _Qry &= vbCrLf & ", ISNULL(D.FNSam, 0)  AS FNSam"
        _Qry &= vbCrLf & ", ISNULL(D.FNCostPerMin, 0)  AS FNCostPerMin"
        _Qry &= vbCrLf & ", ISNULL(D.FNMultiple, ISNULL(C.FNMultiple,1.00))  AS FNMultiple"
        _Qry &= vbCrLf & ",CASE WHEN ISNULL(B.FTPORef ,'') = '' THEN  A.FTPORef ELSE ISNULL(B.FTPORef ,'') END  AS FTCustomerPO"
        _Qry &= vbCrLf & ",CASE WHEN ISNULL(B.FTPORef ,'') = '' THEN  A.FTPORef ELSE ISNULL(B.FTPORef ,'') END  AS FNHSysBuy"
        _Qry &= vbCrLf & ",CASE WHEN  ISDATE(B.FDShipDate) = 1 THEN Convert(Datetime,B.FDShipDate) ELSE NULL END AS FDShipDate"
        _Qry &= vbCrLf & ",ISNULL(D.FNPrice, 0.0) AS FNPrice"
        _Qry &= vbCrLf & ",ISNULL(D.FNRepackPrice, 0.0) AS FNRepackPrice"
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
        _Qry &= vbCrLf & ",ISNULL(D.FNSamPack, 0.0) AS FNSamPack"
        _Qry &= vbCrLf & ",ISNULL(D.FNCostPack, 0.0) AS FNCostPack"
        _Qry &= vbCrLf & ",ISNULL(D.FNNetCostPack, 0.0) AS FNNetCostPack"

        _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS A WITH(NOLOCK) "
        _Qry &= vbCrLf & " INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub AS B WITH(NOLOCK)  ON A.FTOrderNo = B.FTOrderNo "
        _Qry &= vbCrLf & " INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS ST WITH(NOLOCK)  ON A.FNHSysStyleId = ST.FNHSysStyleId "
        _Qry &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TPPCTSamByOrder AS D WITH(NOLOCK)  ON B.FTSubOrderNo = D.FTSubOrderNo AND B.FTOrderNo = D.FTOrderNo AND A.FNHSysStyleId = D.FNHSysStyleId "
        _Qry &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TPPCTSamByStyle AS C WITH(NOLOCK)  ON A.FNHSysStyleId = C.FNHSysStyleId"
        _Qry &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMSeason AS SS WITH(NOLOCK) ON A.FNHSysSeasonId = SS.FNHSysSeasonId "
        _Qry &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMBuyGrp AS E WITH(NOLOCK) ON B.FNHSysBuyGrpId = E.FNHSysBuyGrpId "
        _Qry &= vbCrLf & "  WHERE A.FTOrderNo <>'' "

        If FNHSysBuyId.Text <> "" Then
            _Qry &= vbCrLf & "  AND A.FNHSysBuyId =" & Val(FNHSysBuyId.Properties.Tag.ToString()) & " "
        End If

        If FNHSysSeasonId.Text <> "" Then
            _Qry &= vbCrLf & "  AND A.FNHSysSeasonId =" & Val(FNHSysSeasonId.Properties.Tag.ToString()) & " "
        End If

        If FNHSysStyleId.Text <> "" Then
            _Qry &= vbCrLf & "  AND A.FNHSysStyleId =" & Val(FNHSysStyleId.Properties.Tag.ToString()) & " "
        End If

        _Qry &= vbCrLf & "  ORDER BY ST.FTStyleCode,SS.FTSeasonCode, A.FTOrderNo,B.FTSubOrderNo "

        Dim _dt As DataTable = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

        Me.ogcdetail.DataSource = _dt
        Spls.Close()
        _LoadData = False
    End Sub

#End Region

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ocmrefresh_Click(sender As Object, e As EventArgs) Handles ocmrefresh.Click
        Me.LoadDataInfo()
    End Sub

    Private Sub ocmclear_Click(sender As Object, e As EventArgs) Handles ocmclear.Click

        Try

            HI.TL.HandlerControl.ClearControl(Me.ogbStyleHeader)
            Me.ogcdetail.DataSource = Nothing

        Catch ex As Exception
        End Try

    End Sub

    Private Sub FNSamCut_EditValueChanged(sender As Object, e As EventArgs)
    End Sub

End Class
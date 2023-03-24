Imports DevExpress.XtraEditors.Controls

Public Class wRDSamFactoryTracking
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ocmload_Click(sender As Object, e As EventArgs) Handles ocmload.Click

        If VerifyField() Then

            Dim _Spls As New HI.TL.SplashScreen("Loading...Data Please wait.")

            Call LoadData()

            _Spls.Close()

        End If

    End Sub


    Private Sub LoadData()

        Dim Qry As String = ""
        Dim dt As DataTable

        Try

            Qry = " SELECT   ST.FTStyleCode"
            Qry &= vbCrLf & " , SS.FTSeasonCode"
            Qry &= vbCrLf & " , CMP.FTCmpCode"
            Qry &= vbCrLf & " , A.FNHSysStyleId"
            Qry &= vbCrLf & " , A.FNHSysSeasonId"
            Qry &= vbCrLf & " , A.FNSam As FNRDSam"
            Qry &= vbCrLf & " , B.FNSam AS FNGESam"
            Qry &= vbCrLf & " , C.FNSam As FNGTMSam "
            Qry &= vbCrLf & " , case when isnull(B.FNSam,0) > 0 then   cONVERT(NUMERIC(18,2),(( ((isnull(C.FNSam,0) - isnull(b.FNSam,0) ))/  isnull(B.FNSam,0) ) *100.00 )) else 0.00 end  as FNSamDiff "
            Qry &= vbCrLf & "   FROM            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TRDTRDSam As A With(NOLOCK) INNER JOIN "
            Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TRDTGESam AS B WITH(NOLOCK) ON A.FNHSysStyleId = B.FNHSysStyleId AND A.FNHSysSeasonId = B.FNHSysSeasonId INNER JOIN "
            Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TRDTGTMSam As  C With(NOLOCK) On B.FNHSysStyleId = C.FNHSysStyleId And B.FNHSysSeasonId = C.FNHSysSeasonId INNER JOIN "
            Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp AS CMP WITH(NOLOCK) ON C.FNHSysCmpId = CMP.FNHSysCmpId INNER JOIN "
            Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle As ST With(NOLOCK) On A.FNHSysStyleId = ST.FNHSysStyleId INNER JOIN "
            Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMSeason AS SS WITH(NOLOCK) ON A.FNHSysSeasonId = SS.FNHSysSeasonId "
            Qry &= vbCrLf & " ORDER BY ST.FTStyleCode , SS.FTSeasonCode,CMP.FTCmpCode"

            dt = HI.Conn.SQLConn.GetDataTable(Qry, Conn.DB.DataBaseName.DB_MERCHAN)
            ogcCostsheet.DataSource = dt

        Catch ex As Exception
        End Try

    End Sub
    Private Sub ocmclearclsr_Click(sender As Object, e As EventArgs) Handles ocmclearclsr.Click
        HI.TL.HandlerControl.ClearControl(Me)
    End Sub

    Private Function VerifyField() As Boolean
        Return True
    End Function



End Class
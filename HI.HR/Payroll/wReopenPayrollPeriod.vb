Public Class wReopenPayrollPeriod

#Region "Procedure"
    Private Sub LoadPeriodData()

        Dim _Qry As String

        _Qry = "  SELECT     '0' AS FTSelect, ET.FTEmpTypeCode, H.FTPayYear, H.FTPayTerm, CONVERT(varchar(10),Convert(datetime,D.FDPayDate),103) AS FDPayDate,ET.FNHSysEmpTypeId "
        _Qry &= vbCrLf & " FROM         (SELECT        FNHSysEmpTypeId, LEFT(BeforeTerm, 4) AS FTPayYear, RIGHT(BeforeTerm, 2) AS FTPayTerm"
        _Qry &= vbCrLf & "FROM            (SELECT       FNHSysEmpTypeId, ISNULL"
        _Qry &= vbCrLf & "      ((SELECT        TOP (1) FTPayYear + '|' + FTPayTerm AS Expr1"
        _Qry &= vbCrLf & "        FROM            THRMCfgPayDT WITH (NOLOCK)"
        _Qry &= vbCrLf & "        WHERE       (FNHSysEmpTypeId = H.FNHSysEmpTypeId) AND (FTPayYear + FTPayTerm < H.FTPayYear + H.FTPayTerm)"
        _Qry &= vbCrLf & "      ORDER BY FTPayYear + FTPayTerm DESC), '') AS BeforeTerm"
        _Qry &= vbCrLf & "    FROM            THRMCfgPayHD AS H WITH (NOLOCK)) AS M"
        _Qry &= vbCrLf & " WHERE        (BeforeTerm <> '') ) AS H  INNER JOIN"
        _Qry &= vbCrLf & "  THRMCfgPayDT AS D WITH (NOLOCK) ON  H.FNHSysEmpTypeId = D.FNHSysEmpTypeId AND H.FTPayTerm = D.FTPayTerm AND "
        _Qry &= vbCrLf & "  H.FTPayYear = D.FTPayYear INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTPayRoll  AS PP WITH (NOLOCK) "
        _Qry &= vbCrLf & " ON D.FNHSysEmpTypeId = PP.FNHSysEmpTypeId And D.FTPayTerm = PP.FTPayTerm And D.FTPayYear = PP.FTPayYear"
        _Qry &= vbCrLf & "  INNER Join "
        _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS ET WITH(NOLOCK)  ON D.FNHSysEmpTypeId = ET.FNHSysEmpTypeId "

        If Not (HI.ST.SysInfo.Admin) Then

            _Qry &= vbCrLf & "  WHERE ET.FNHSysEmpTypeId IN ("
            _Qry &= vbCrLf & " Select DISTINCT UPT.FNHSysEmpTypeId"
            _Qry &= vbCrLf & " FROM             [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEUserLoginPermission AS UP WITH (NOLOCK) INNER JOIN"
            _Qry &= vbCrLf & "     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEPermissionMenu AS UPM WITH (NOLOCK) ON UP.FNHSysPermissionID = UPM.FNHSysPermissionID INNER JOIN"
            _Qry &= vbCrLf & "     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEPermissionEmployeeType AS UPT WITH (NOLOCK) ON UP.FNHSysPermissionID = UPT.FNHSysPermissionID AND UPM.FNHSysPermissionID = UPT.FNHSysPermissionID"
            _Qry &= vbCrLf & "  WHERE UP.FTUserName='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
            _Qry &= vbCrLf & "  AND UPM.FTMnuName='" & HI.UL.ULF.rpQuoted(HI.ST.SysInfo.MenuName) & "' "
            _Qry &= vbCrLf & "  )     AND  ET.FNHSysCmpId=" & Val(HI.ST.SysInfo.CmpID) & " "
        Else
            _Qry &= vbCrLf & "  WHERE ET.FNHSysCmpId=" & Val(HI.ST.SysInfo.CmpID) & " "
        End If

        _Qry &= vbCrLf & " GROUP BY  ET.FTEmpTypeCode, H.FTPayYear, H.FTPayTerm, D.FDPayDate,ET.FNHSysEmpTypeId "
        _Qry &= vbCrLf & " ORDER BY ET.FNHSysEmpTypeId, H.FTPayYear, H.FTPayTerm, D.FDPayDate"

        Me.ogd.DataSource = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

    End Sub

    Private Function ProcSave() As Boolean
        Try
            Dim _EmpType As String
            Dim _PayYear As String
            Dim _PayTerm As String
            Dim _Qry As String

            CType(ogd.DataSource, DataTable).AcceptChanges()

            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_HR)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            For Each row As DataRow In CType(ogd.DataSource, DataTable).Rows

                _EmpType = row!FNHSysEmpTypeId.ToString
                _PayYear = row!FTPayYear.ToString
                _PayTerm = row!FTPayTerm.ToString

                If row!FTSelect.ToString = "1" Then


                    _Qry = " DELETE PF "
                    _Qry &= vbCrLf & " FROM THRTPayRollFin AS PF ,THRTPayRoll AS  P,THRMCfgPayHD as D"
                    _Qry &= vbCrLf & " WHERE P.FTPayYear = D.FTPayYear "
                    _Qry &= vbCrLf & " AND P.FTPayTerm = D.FTPayTerm "
                    _Qry &= vbCrLf & " AND P.FNHSysEmpTypeId = D.FNHSysEmpTypeId "
                    _Qry &= vbCrLf & " AND P.FTPayYear = PF.FTPayYear "
                    _Qry &= vbCrLf & " AND P.FTPayTerm = PF.FTPayTerm "
                    _Qry &= vbCrLf & " AND  P.FNHSysEmpID = PF.FNHSysEmpID "
                    _Qry &= vbCrLf & " AND   (D.FNHSysEmpTypeId =" & Val(_EmpType) & ") "

                    HI.Conn.SQLConn.ExecuteTran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)


                    _Qry = " DELETE PF "
                    _Qry &= vbCrLf & " FROM THRTPayRollLeave AS PF ,THRTPayRoll AS  P,THRMCfgPayHD as D"
                    _Qry &= vbCrLf & " WHERE P.FTPayYear = D.FTPayYear "
                    _Qry &= vbCrLf & " AND P.FTPayTerm = D.FTPayTerm "
                    _Qry &= vbCrLf & " AND P.FNHSysEmpTypeId = D.FNHSysEmpTypeId "
                    _Qry &= vbCrLf & " AND P.FTPayYear = PF.FTPayYear "
                    _Qry &= vbCrLf & " AND P.FTPayTerm = PF.FTPayTerm "
                    _Qry &= vbCrLf & " AND  P.FNHSysEmpID = PF.FNHSysEmpID "
                    _Qry &= vbCrLf & " AND   (D.FNHSysEmpTypeId =" & Val(_EmpType) & ") "

                    HI.Conn.SQLConn.ExecuteTran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

                    _Qry = " DELETE P "
                    _Qry &= vbCrLf & " FROM THRTPayRoll AS  P,THRMCfgPayHD as D"
                    _Qry &= vbCrLf & " WHERE P.FTPayYear = D.FTPayYear "
                    _Qry &= vbCrLf & " AND P.FTPayTerm = D.FTPayTerm "
                    _Qry &= vbCrLf & " AND  P.FNHSysEmpTypeId = D.FNHSysEmpTypeId "
                    _Qry &= vbCrLf & " AND   (D.FNHSysEmpTypeId =" & Val(_EmpType) & ") "

                    HI.Conn.SQLConn.ExecuteTran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)


                    _Qry = "  UPDATE THRMCfgPayHD SET FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                    _Qry &= vbCrLf & " , FDUpdDate=" & HI.UL.ULDate.FormatDateDB & ", FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & "  "
                    _Qry &= vbCrLf & ",FTPayTerm='" & _PayTerm & "'"
                    _Qry &= vbCrLf & ",FTPayYear='" & _PayYear & "' "
                    _Qry &= vbCrLf & " WHERE  (FNHSysEmpTypeId =" & Val(_EmpType) & ") "

                    HI.Conn.SQLConn.ExecuteTran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

                    _Qry = "  INSERT INTO THTTReOpenPeriodHistory(FTInsUser, FDInsDate, FTInsTime, FNSeq, FNHSysEmpTypeId, FTPayTerm, FTPayYear) "
                    _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & " "
                    _Qry &= vbCrLf & " , ISNULL((SELECT TOP 1 FNSeq FROM THTTReOpenPeriodHistory  ORDER BY FNSeq DESC  ),0) +1"
                    _Qry &= vbCrLf & "," & Val(_EmpType) & ",'" & _PayTerm & "','" & _PayYear & "'"

                    HI.Conn.SQLConn.ExecuteTran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

                End If

            Next


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
    End Function
#End Region

#Region "General "

    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Call LoadPeriodData()
    End Sub

    Private Sub ocmcalculate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmcalculate.Click
        If HI.MG.ShowMsg.mConfirmProcess("", 1005310004, "") = True Then
            Dim _Spls As New HI.TL.SplashScreen("Reopen Period... Please Wait...   ")
            If ProcSave() Then
                _Spls.Close()
                Call LoadPeriodData()

                HI.MG.ShowMsg.mInfo("", 1005310005, Me.Text)
            Else
                _Spls.Close()
            End If
        End If
    End Sub

    Private Sub ocmclose_Click(sender As System.Object, e As System.EventArgs) Handles ocmclose.Click
        Me.Close()
    End Sub

#End Region

End Class
Public Class wClosePayrollPeriod

#Region "Process"

    Private Sub LoadPeriodData()

        Dim _Qry As String


        _Qry = "  SELECT     '0' AS FTSelect, ET.FTEmpTypeCode, H.FTPayYear, H.FTPayTerm, CONVERT(varchar(10),Convert(datetime,D.FDPayDate),103) AS FDPayDate,ET.FNHSysEmpTypeId"
        _Qry &= vbCrLf & "  FROM         THRMCfgPayHD AS H WITH (NOLOCK) INNER JOIN"
        _Qry &= vbCrLf & "  THRMCfgPayDT AS D WITH (NOLOCK) ON H.FNHSysEmpTypeId = D.FNHSysEmpTypeId AND H.FTPayTerm = D.FTPayTerm AND "
        _Qry &= vbCrLf & "  H.FTPayYear = D.FTPayYear INNER JOIN"
        _Qry &= vbCrLf & "  (SELECT      HH.FNHSysEmpTypeId, HH.FTPayTerm, HH.FTPayYear"
        _Qry &= vbCrLf & "  FROM          THRMCfgPayHD AS HH WITH (NOLOCK) INNER JOIN"
        _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTPayRoll AS P WITH (NOLOCK) ON HH.FTPayYear = P.FTPayYear AND HH.FTPayTerm = P.FTPayTerm AND "
        _Qry &= vbCrLf & "  HH.FNHSysEmpTypeId = P.FNHSysEmpTypeId "
        _Qry &= vbCrLf & "  GROUP BY  HH.FNHSysEmpTypeId, HH.FTPayTerm, HH.FTPayYear) AS PP ON  "
        _Qry &= vbCrLf & "  D.FNHSysEmpTypeId = PP.FNHSysEmpTypeId And D.FTPayTerm = PP.FTPayTerm And D.FTPayYear = PP.FTPayYear"
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
            _Qry &= vbCrLf & "    WHERE  ET.FNHSysCmpId=" & Val(HI.ST.SysInfo.CmpID) & " "
        End If

        _Qry &= vbCrLf & " GROUP BY  ET.FTEmpTypeCode, H.FTPayYear, H.FTPayTerm, D.FDPayDate,ET.FNHSysEmpTypeId "
        _Qry &= vbCrLf & "  ORDER BY ET.FTEmpTypeCode, H.FTPayYear, H.FTPayTerm, D.FDPayDate"

        Me.ogd.DataSource = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

    End Sub

    Private Function ProcSave() As Boolean
        Try
            Dim _EmpType As String
            Dim _PayYear As String
            Dim _PayTerm As String
            Dim _NewTerm As String
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
                    _Qry = " SELECT    TOP 1    FTPayYear + '|' + FTPayTerm AS FTTermEndOfYear"
                    _Qry &= vbCrLf & "  FROM   THRMCfgPayDT WITH(NOLOCK)"
                    _Qry &= vbCrLf & " WHERE (FNHSysEmpTypeId ='" & HI.UL.ULF.rpQuoted(_EmpType) & "') "
                    _Qry &= vbCrLf & " AND (FTPayYear + FTPayTerm > '" & HI.UL.ULF.rpQuoted(_PayYear & _PayTerm) & "')"
                    _Qry &= vbCrLf & " ORDER BY FTPayYear ASC, FTPayTerm ASC   "
                    _NewTerm = HI.Conn.SQLConn.GetFieldOnBeginTrans(_Qry, Conn.DB.DataBaseName.DB_HR, "")

                    If _NewTerm <> "" Then

                        _Qry = "  UPDATE THRMCfgPayHD SET  "
                        _Qry &= vbCrLf & " FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                        _Qry &= vbCrLf & " , FDUpdDate=" & HI.UL.ULDate.FormatDateDB & ", FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & "  "
                        _Qry &= vbCrLf & ",FTPayTerm='" & _NewTerm.Split("|")(1).ToString & "'"
                        _Qry &= vbCrLf & ",FTPayYear='" & _NewTerm.Split("|")(0).ToString & "' "
                        _Qry &= vbCrLf & " WHERE  (FNHSysEmpTypeId ='" & HI.UL.ULF.rpQuoted(_EmpType) & "') "

                        HI.Conn.SQLConn.ExecuteTran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

                        _Qry = "  INSERT INTO THTTClosePeriodHistory(FTInsUser, FDInsDate, FTInsTime, FNSeq, FNHSysEmpTypeId, FTPayTerm, FTPayYear) "
                        _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & " "
                        _Qry &= vbCrLf & " , ISNULL((SELECT TOP 1 FNSeq FROM THTTClosePeriodHistory  ORDER BY FNSeq DESC  ),0) +1"
                        _Qry &= vbCrLf & "," & Val(row!FNHSysEmpTypeId.ToString) & ",'" & _PayTerm & "','" & _PayYear & "'"

                        HI.Conn.SQLConn.ExecuteTran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

                        _Qry = "  UPDATE M SET FTStaCalMonthEnd='' "
                        _Qry &= vbCrLf & " FROM            THRMEmployee AS M INNER JOIN"
                        _Qry &= vbCrLf & "  THRMCfgPayDT AS PD WITH (NOLOCK) "
                        _Qry &= vbCrLf & " ON M.FNHSysEmpTypeId = PD.FNHSysEmpTypeId "
                        _Qry &= vbCrLf & " AND  M.FDDateStart <= PD.FDCalDateEnd"
                        _Qry &= vbCrLf & " WHERE M.FTStaCalMonthEnd ='1' "
                        _Qry &= vbCrLf & " AND  M.FNHSysEmpTypeId=" & Val(row!FNHSysEmpTypeId.ToString) & " "
                        _Qry &= vbCrLf & "  AND FTPayTerm='" & HI.UL.ULF.rpQuoted(_PayTerm) & "' "
                        _Qry &= vbCrLf & " AND  FTPayYear='" & HI.UL.ULF.rpQuoted(_PayYear) & "' "
                        HI.Conn.SQLConn.ExecuteTran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

                    End If

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

#Region "General"

    Private Sub wClosePeriod_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Call LoadPeriodData()
    End Sub

    Private Sub ocmcalculate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmcalculate.Click
        If HI.MG.ShowMsg.mConfirmProcess("", 1005310001, "") = True Then
            Dim _Spls As New HI.TL.SplashScreen("Closing Period... Please Wait...   ")
            If ProcSave() Then
                _Spls.Close()
                Call LoadPeriodData()

                HI.MG.ShowMsg.mInfo("", 1005310002, Me.Text)

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
Public NotInheritable Class Time

    Public Shared Sub GETbScanCtrlcfg(ByRef nIDStart As Integer, ByRef nIDLen As Integer, ByRef nYearStart As Integer, ByRef nYearLen As Integer, _
                                          ByRef nMonthStart As Integer, ByRef nMonthLen As Integer, ByRef nDayStart As Integer, ByRef nDayLen As Integer, _
                                           ByRef nHourStart As Integer, ByRef nHourLen As Integer, ByRef nMinuteStart As Integer, ByRef nMinuteLen As Integer, _
                                           ByRef nStationStart As Integer, ByRef nStatainLen As Integer, Optional CmpID As Integer = -1)
        Try
            Dim oDBdt As DataTable
            Dim _QrySql As String

            _QrySql = " SELECT TOP 1 FNHSysCmpId, FNBarcodeStart, FNBarcodeLength"
            _QrySql &= vbCrLf & " , FNDateStart, FNDateLength, FNMonthStart, FNMonthLength"
            _QrySql &= vbCrLf & "  , FNYearStart, FNYearLength, FNHourStart, FNHourLength"
            _QrySql &= vbCrLf & " , FNMinStart, FNMinLength,FNMachineStart, FNMachineLength"
            _QrySql &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMConfigScanCard WITH (NOLOCK) "

            If CmpID = -1 Then
                _QrySql &= vbCrLf & " WHERE FNHSysCmpId=" & HI.ST.SysInfo.CmpID & " "
            Else
                _QrySql &= vbCrLf & " WHERE FNHSysCmpId=" & CmpID & " "
            End If

            oDBdt = HI.Conn.SQLConn.GetDataTable(_QrySql, Conn.DB.DataBaseName.DB_HR)

            For Each R As DataRow In oDBdt.Rows

                nIDStart = Integer.Parse(R!FNBarcodeStart.ToString)
                nIDLen = Integer.Parse(R!FNBarcodeLength.ToString)

                nYearStart = Integer.Parse(R!FNYearStart.ToString)
                nYearLen = Integer.Parse(R!FNYearLength.ToString)

                nMonthStart = Integer.Parse(R!FNMonthStart.ToString)
                nMonthLen = Integer.Parse(R!FNMonthLength.ToString)

                nDayStart = Integer.Parse(R!FNDateStart.ToString)
                nDayLen = Integer.Parse(R!FNDateLength.ToString)

                nHourStart = Integer.Parse(R!FNHourStart.ToString)
                nHourLen = Integer.Parse(R!FNHourLength.ToString)

                nMinuteStart = Integer.Parse(R!FNMinStart.ToString)
                nMinuteLen = Integer.Parse(R!FNMinLength.ToString)

                nStationStart = Integer.Parse(R!FNMachineStart.ToString)
                nStatainLen = Integer.Parse(R!FNMachineLength.ToString)

                Exit For

            Next

        Catch ex As Exception
        End Try
    End Sub

    Public Shared Function VerifyScanData(ByVal ptDateScan As String, ByVal ptTimeScan As String, ByVal ptIdScan As String) As Boolean
        Try
            If HI.UL.ULDate.CheckDate(ptDateScan) = "" Then   '...ตรวจสอบวันที่
                Return False
            End If

            If Len(ptTimeScan) <> 4 Then    '...ตรวจสอบเวลา  ชั่วโมง / นาที
                Return False
            ElseIf Val(Mid(ptTimeScan, 1, 2)) >= 24 Then
                Return False
            ElseIf Val(Mid(ptTimeScan, 3, 2)) >= 60 Then
                Return False
            End If

            If ptIdScan.ToString = "" Then
                Return False
            End If

            Dim _dt As New DataTable
            Dim _Row() As DataRow = _dt.Select

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Shared Function LoadListPayYear() As String()
        Dim _Qry As String
        Dim _List As New ArrayList
        _Qry = "SELECT    FTPayYear"
        _Qry = " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMCfgPayDT  WITH(NOLOCK) "
        _Qry = " GROUP BY FTPayYear Order BY FTPayYear Desc"
        Dim dt As DataTable = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

        For Each R As DataRow In dt.Rows
            _List.Add(R!FTPayYear)
        Next

        Return _List.ToArray(GetType(String))

    End Function

    Public Shared Function CheckClosePeriod(FDDate As String, Optional FNHSysEmpID As Integer = 0, Optional FNHSysEmpTypeID As Integer = 0) As Boolean
        Dim _Qry As String

        _Qry = "    SELECT  TOP 1 PD.FDCalDateBegin"
        _Qry &= vbCrLf & " FROM             [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMCfgPayHD AS PH WITH (NOLOCK) INNER JOIN"
        _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMCfgPayDT AS PD WITH (NOLOCK) ON  PH.FTPayTerm = PD.FTPayTerm  AND PH.FTPayYear = PD.FTPayYear AND PH.FNHSysEmpTypeId = PD.FNHSysEmpTypeId"
        _Qry &= vbCrLf & " INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS M   WITH (NOLOCK) ON PH.FNHSysEmpTypeId = M.FNHSysEmpTypeId"
        _Qry &= vbCrLf & " INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS MTT   WITH (NOLOCK) ON M.FNHSysEmpTypeId = MTT.FNHSysEmpTypeId"

        _Qry &= vbCrLf & "  WHERE PD.FDCalDateBegin > '" & HI.UL.ULDate.ConvertEnDB(FDDate) & "' "
        _Qry &= vbCrLf & "      AND  ISNULL(M.FNHSysCmpId,0)=" & HI.ST.SysInfo.CmpID & ""
        _Qry &= vbCrLf & "      AND (ISNULL(MTT.FNHSysCmpId,0) = 0 OR ISNULL(MTT.FNHSysCmpId,0)=" & HI.ST.SysInfo.CmpID & ") "
        If (FNHSysEmpID > 0) Then
            _Qry &= vbCrLf & "  AND M.FNHSysEmpID =" & Val(FNHSysEmpID) & " "
        End If

        If (FNHSysEmpTypeID > 0) Then
            _Qry &= vbCrLf & "  AND PH.FNHSysEmpTypeId =" & Val(FNHSysEmpTypeID) & " "
        End If

        Return (HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "") <> "")

    End Function


    Public Shared Function LoadTimeColor() As DataTable
        Return HI.Conn.SQLConn.GetDataTable("SELECT  FTLeaveType, FTColor  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMConfigColor AS A WITH(NOLOCK) ", Conn.DB.DataBaseName.DB_HR)
    End Function

End Class

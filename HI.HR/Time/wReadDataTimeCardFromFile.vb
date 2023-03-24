Imports System.IO
Imports System.Windows.Forms

Public Class wReadDataTimeCardFromFile

#Region "Procedure"
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        'If HI.ST.SysInfo.Admin Then
        '    Me.Height = 372
        'Else
        '    Me.Height = 235
        'End If

    End Sub

    Private Sub ReadDataText()
        Dim _Qry As String
        Dim _IDStart As Integer
        Dim _IDLenght As Integer
        Dim _YearStart As Integer
        Dim _YearLenght As Integer
        Dim _CardMonthStart As Integer
        Dim _CardMonthLenght As Integer
        Dim _CardDayStart As Integer
        Dim _CardDayLenght As Integer
        Dim _CardHourStart As Integer
        Dim _CardHourLenght As Integer
        Dim _CardMinuteStart As Integer
        Dim _CardMinuteLen As Integer
        Dim _CardStationStart As Integer
        Dim _CardStatainLenght As Integer

        Call HI.HRCAL.Time.GETbScanCtrlcfg(_IDStart, _IDLenght, _YearStart, _YearLenght, _CardMonthStart, _CardMonthLenght, _CardDayStart, _CardDayLenght, _CardHourStart, _CardHourLenght, _CardMinuteStart, _CardMinuteLen, _CardStationStart, _CardStatainLenght)

        Dim _StationScan As String
        Dim _DateScan As String
        Dim _DateProcessScan As String
        Dim _DataTimeScan As String
        Dim _EmpIDScan As String
        Dim _StrLineInput As String
        Dim _CountWriteToErr As Integer
        Dim _CountWirteToDaily As Integer

        Dim _TimeOver As String = ""
        Dim _data As New DataTable
        Dim _CmpOwner As Boolean
        _CmpOwner = False


        _Qry = "DELETE  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTimecard "
        _Qry &= vbCrLf & " WHERE FTInsUser ='" & HI.ST.UserInfo.UserName & "' "
        HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

        Dim dtsource As New DataTable("ReadTimeCard")
        dtsource.Columns.Add("FTData", GetType(String))
        Dim lines() As String

        Dim _toatlLine As Integer = 0
        Dim _Rec As Integer = 0

        Dim _Spls As New HI.TL.SplashScreen("Reading time Card...   Please Wait   ", "Processing. Please Wait...")
        Try

            lines = File.ReadAllLines(Me.FTFilePaht.Text)

            For linenum As Integer = 0 To lines.Length - 1
                _toatlLine = _toatlLine + 1
                dtsource.Rows.Add(lines(linenum))
            Next

            _Rec = 0
            Dim _CheckDate As String = ""
            Dim _FoundShift As Boolean = True

            For Each R As DataRow In dtsource.Rows
                _Rec = _Rec + 1
                _Spls.UpdateInformation("Check Data Record  " & _Rec.ToString & " Of " & _toatlLine.ToString & "  (" & Format((_Rec * 100.0) / _toatlLine, "0.00") & " % ) ")

                _StrLineInput = R.Item(0).ToString

                If _StrLineInput <> "" Then

                    If _YearLenght = 2 Then
                        _DateScan = Mid(_StrLineInput, _CardDayStart, _CardDayLenght) & "/" & Mid(_StrLineInput, _CardMonthStart, _CardMonthLenght) & "/20" & Mid(_StrLineInput, _YearStart, _YearLenght)
                    Else
                        _DateScan = Mid(_StrLineInput, _CardDayStart, _CardDayLenght) & "/" & Mid(_StrLineInput, _CardMonthStart, _CardMonthLenght) & "/" & Mid(_StrLineInput, _YearStart, _YearLenght)
                    End If

                    _DateProcessScan = _DateScan
                    _DataTimeScan = Mid(_StrLineInput, _CardHourStart, _CardHourLenght) & Mid(_StrLineInput, _CardMinuteStart, _CardMinuteLen)
                    _EmpIDScan = Mid(_StrLineInput, _IDStart, _IDLenght)
                    _StationScan = Mid(_StrLineInput, _CardStationStart, _CardStatainLenght)

                    If HI.HRCAL.Time.VerifyScanData(_DateScan, _DataTimeScan, _EmpIDScan) = True Then

                        _CheckDate = HI.UL.ULDate.ConvertEnDB(_DateScan)

                        '_Qry = " SELECT        TOP 1 S.FTOverClock"
                        '_Qry &= vbCrLf & "  FROM           ( SELECT ISNULL(("
                        '_Qry &= vbCrLf & " SELECT TOP 1  ISNULL(FNHSysShiftID,'')"
                        '_Qry &= vbCrLf & " 	 FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployeeMoveShift WITH(NOLOCK)"
                        '_Qry &= vbCrLf & "  WHERE FNHSysEmpID =M.FNHSysEmpID"
                        '_Qry &= vbCrLf & "    AND FDShiftDate='" & _CheckDate & "'),M.FNHSysShiftID) As  FNHSysShiftID"
                        '_Qry &= vbCrLf & " FROM            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS M  WITH (NOLOCK) "
                        '_Qry &= vbCrLf & " WHERE        (M.FTEmpBarcode =N'" & _EmpIDScan & "') ) AS M  INNER JOIN"
                        '_Qry &= vbCrLf & "    THRMTimeShift AS S WITH (NOLOCK) ON M.FNHSysShiftID = S.FNHSysShiftID "
                        '_Qry &= vbCrLf & "  ORDER BY S.FTOverClock"

                        '_TimeOver = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "")
                        'If _TimeOver <> "" Then
                        '    If Val(_DataTimeScan) < Val(_TimeOver.Replace(":", "")) Then
                        '        _DateProcessScan = HI.UL.ULDate.ConvertEnDB(HI.UL.ULDate.AddDay(_DateScan, -1))
                        '    End If
                        'Else

                        If Val(_DataTimeScan) > Val("0000") And Val(_DataTimeScan) <= Val("0500") Then

                            _DateProcessScan = HI.UL.ULDate.ConvertEnDB(HI.UL.ULDate.AddDay(_DateScan, -1))

                        End If

                        ' End If

                        _DataTimeScan = Microsoft.VisualBasic.Right("0000" & _DataTimeScan, 4)

                        _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTimecard (FNHSysCmpId,FTInsUser,FTInsDate,FTInsTime"
                        _Qry &= ",FTUpdUser,FTUpdDate,FTUpdTime"
                        _Qry &= ",FTDateTrans,FTTimeTrans,FTEmpBarcode,FTDateScan)"
                        _Qry &= vbCrLf & " VALUES (" & HI.ST.SysInfo.CmpID & ",'" & HI.ST.UserInfo.UserName & "'"
                        _Qry &= "," & HI.UL.ULDate.FormatDateDB
                        _Qry &= "," & HI.UL.ULDate.FormatTimeDB
                        _Qry &= ",'" & HI.ST.UserInfo.UserName & "'"
                        _Qry &= "," & HI.UL.ULDate.FormatDateDB
                        _Qry &= "," & HI.UL.ULDate.FormatTimeDB
                        _Qry &= ",'" & HI.UL.ULDate.ConvertEnDB(_DateProcessScan) & "'"
                        _Qry &= ",'" & (_DataTimeScan) & "'"
                        _Qry &= ",'" & _EmpIDScan & "'"
                        _Qry &= ",'" & HI.UL.ULDate.ConvertEnDB(_DateScan) & "')"

                        HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

                        _CountWirteToDaily = _CountWirteToDaily + 1

                    Else

                        _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTimecardError (FTInsUser,FTInsDate,FTInsTime"
                        _Qry &= ",FTUpdUser,FTUpdDate,FTUpdTime"
                        _Qry &= ",FTDateTrans,FTTimeTrans,FTEmpBarcode,FTCmpCode)"
                        _Qry &= vbCrLf & "VALUES ('" & HI.ST.UserInfo.UserName & "'"
                        _Qry &= "," & HI.UL.ULDate.FormatDateDB
                        _Qry &= "," & HI.UL.ULDate.FormatTimeDB
                        _Qry &= ",'" & HI.ST.UserInfo.UserName & "'"
                        _Qry &= "," & HI.UL.ULDate.FormatDateDB
                        _Qry &= "," & HI.UL.ULDate.FormatTimeDB
                        _Qry &= ",'" & HI.UL.ULDate.ConvertEnDB(_DateScan) & "'"
                        _Qry &= ",'" & (_DataTimeScan) & "'"
                        _Qry &= ",'" & _EmpIDScan & "'"
                        _Qry &= ",'" & HI.UL.ULF.rpQuoted("") & "')"

                        HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

                        _CountWriteToErr = _CountWriteToErr + 1

                    End If

                End If

            Next

            'Dim _dttmp As DataTable
            '_Qry = "SELECT  FTDateTrans  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTimecard  WITH(NOLOCK) Group By FTDateTrans "
            '_dttmp = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

            'For Each R As DataRow In _dttmp.Rows

            '    '_Qry = "Delete From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTPullTimeCardHistory WHERE FTDateTrans='" & HI.UL.ULDate.ConvertEnDB(R!FTDateTrans.ToString) & "'   "
            '    'HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

            '    _Qry = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTPullTimeCardHistory( FTInsUser, FTInsDate, FTInsTime,  FTUpdUser, FTUpdDate, FTUpdTime,  FTDateTrans"
            '    _Qry &= vbCrLf & ", FTTimeTrans, FTEmpBarcode, FTIdMac, FTCmpCode,FTDateTimeStemp ) "
            '    _Qry &= vbCrLf & "  SELECT  DISTINCT   FTInsUser, FTInsDate, FTInsTime,  FTUpdUser, FTUpdDate, FTUpdTime,  FTDateTrans"
            '    _Qry &= vbCrLf & ", FTTimeTrans, FTEmpBarcode, FTIdMac, FTCmpCode"
            '    _Qry &= vbCrLf & ", Replace(convert(varchar(10),Getdate(),111),'/','') + '-'  + Replace(convert(varchar(15),Getdate(),114),':','')"
            '    _Qry &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTimecard   "
            '    _Qry &= vbCrLf & "  WHERE FTDateTrans='" & HI.UL.ULDate.ConvertEnDB(R!FTDateTrans.ToString) & "'  "
            '    _Qry &= vbCrLf & "  AND  FTInsUser ='" & HI.ST.UserInfo.UserName & "' "

            '    HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

            'Next

            _Qry = "SELECt DISTINCT FTDateTrans "
            _Qry &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTimecard WITH(NOLOCK) "
            _Qry &= vbCrLf & " WHERE FTInsUser ='" & HI.ST.UserInfo.UserName & "' "
            _Qry &= vbCrLf & "ORDER BY FTDateTrans"

            Dim oDBdt As DataTable
            oDBdt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)
            _toatlLine = oDBdt.Rows.Count
            _Rec = 0
            Dim StrCloseDate As String = ""
            Dim _StrAllDate As String = ""

            For Each Row As DataRow In oDBdt.Rows
                _Rec = _Rec + 1

                'ไม่สามารถทำการแก้ไขข้อมูลหลังการปิดงวดได้ 
                If HI.HRCAL.Time.CheckClosePeriod(Row!FTDateTrans.ToString) = False Then
                    HI.HRCAL.Calculate.TransTimeCard(HI.ST.UserInfo.UserName, Row!FTDateTrans.ToString, 0, _Spls)
                Else
   
                    StrCloseDate &= vbCrLf & "" & HI.UL.ULDate.ConvertEN(Row!FTDateTrans.ToString)

                    Beep()
                End If

                _Qry = "  	 INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTimecardHistory( FNHSysCmpId,FTInsUser, FTInsDate, FTInsTime,"
                _Qry &= vbCrLf & " 	 FTDateTrans, FTTimeTrans, FTEmpBarcode, FTIdMac, FTCmpCode)"
                _Qry &= vbCrLf & "  SELECT 	A.FNHSysCmpId,'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "',CONVERT(varchar(10),GETDATE(),111),CONVERT(varchar(8),GETDATE(),114)	"
                _Qry &= vbCrLf & " ,A.FTDateScan,A.FTTimeTrans,	A.FTEmpBarcode,A.FTIdMac,A.FTCmpCode	 "
                _Qry &= vbCrLf & "  FROM 	[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTimecard AS A WITH(NOLOCK)"
                _Qry &= vbCrLf & " LEFT OUTER JOIN ("
                _Qry &= vbCrLf & " SELECT FNHSysCmpId,FTDateTrans , FTTimeTrans , FTEmpBarcode "
                _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTimecardHistory WITH(NOLOCK)"
                _Qry &= vbCrLf & "   WHERE (FTDateTrans ='" & HI.UL.ULDate.ConvertEnDB(Row!FTDateTrans.ToString) & "') AND FNHSysCmpId = " & HI.ST.SysInfo.CmpID & ""
                _Qry &= vbCrLf & "  ) AS BBX ON  A.FTDateTrans = BBX.FTDateTrans AND A.FTTimeTrans = BBX.FTTimeTrans AND A.FTEmpBarcode =BBX.FTEmpBarcode AND A.FNHSysCmpId=BBX.FNHSysCmpId "
                _Qry &= vbCrLf & " WHERE A.FTInsUser ='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                _Qry &= vbCrLf & " AND  A.FTDateTrans='" & HI.UL.ULDate.ConvertEnDB(Row!FTDateTrans.ToString) & "' "
                _Qry &= vbCrLf & " AND BBX.FTEmpBarcode IS NULL "
                _Qry &= vbCrLf & "  DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTimecard "
                _Qry &= vbCrLf & " WHERE FTInsUser ='" & HI.ST.UserInfo.UserName & "' "
                _Qry &= vbCrLf & " AND  FTDateTrans='" & HI.UL.ULDate.ConvertEnDB(Row!FTDateTrans.ToString) & "' "

                HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_HR)

                If _StrAllDate = "" Then
                    _StrAllDate = Row!FTDateTrans.ToString
                Else
                    _StrAllDate = _StrAllDate & "','" & Row!FTDateTrans.ToString
                End If
            Next



            _Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTimecard "
            _Qry &= vbCrLf & " WHERE FTInsUser ='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
            HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_HR)

            _Spls.Close()
            Dim Msg As String = "Read data Complete : " & _CountWirteToDaily & " records."
            If _CountWriteToErr > 0 Then Msg = Msg & vbCrLf & "Data Invalid : " & _CountWriteToErr & " records."

            MessageBox.Show(Msg)

            If StrCloseDate <> "" Then
                Msg = "Found Close Period Can't Import Data"
                Msg &= vbCrLf & "" & StrCloseDate
                MessageBox.Show(Msg)
            End If

        Catch ex As Exception
            _Spls.Close()
            MessageBox.Show("Could not read file: " & Me.FTFilePaht.Text)
            Exit Sub
        End Try
    End Sub
#End Region

#Region "General"
    Private Sub FTFilePaht_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles FTFilePaht.ButtonClick
        Select Case e.Button.Index
            Case 0
                Try
                    Dim Op As New System.Windows.Forms.OpenFileDialog
                    Op.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*"
                    Op.ShowDialog()

                    Try
                        If Op.FileName <> "" Then
                            Me.FTFilePaht.Text = Op.FileName
                        End If
                    Catch ex As Exception
                    End Try

                Catch ex As Exception
                End Try

        End Select
    End Sub

    Private Sub ocmcancel_Click(sender As System.Object, e As System.EventArgs) Handles ocmcancel.Click
        Me.Close()
    End Sub

    Private Sub ReadTextFile_Click(sender As System.Object, e As System.EventArgs) Handles ocmreceive.Click
        If Me.FTFilePaht.Text <> "" Then
            Call ReadDataText()
        End If
    End Sub

    Private Sub wReadDataTimeCardFromFile_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Me.FNHSysCmpId.Text = HI.ST.SysInfo.CmpCode
    End Sub
#End Region

    Private Sub ocmrepull_Click(sender As Object, e As EventArgs) Handles ocmrepull.Click
        If Me.FTDateStart.Text <> "" And Me.FTDateEnd.Text <> "" Then
            Dim _Spls As New HI.TL.SplashScreen("Reading time Card...   Please Wait   ", "Processing. Please Wait...")
            Dim _EndProcDate As String
            Dim _NextProcDate As String
            Dim _dt As DataTable
            Dim _Qry As String
            Try
                _EndProcDate = HI.UL.ULDate.ConvertEnDB(Me.FTDateEnd.Text)
                _NextProcDate = HI.UL.ULDate.ConvertEnDB(Me.FTDateStart.Text)

                Do While _NextProcDate <= _EndProcDate

                    If HI.HRCAL.Time.CheckClosePeriod(_NextProcDate) = False Then

                        _Qry = "SELECT FNHSysEmpID, FTEmpCode, FDDateStart, FDDateEnd"
                        _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS A With(NOLOCK) "
                        _Qry &= vbCrLf & " WHERE FDDateStart<='" & _NextProcDate & "' "
                        _Qry &= vbCrLf & " AND  (ISNULL(FDDateEnd,'') ='' OR ISNULL(FDDateEnd,'')>'" & _NextProcDate & "') "
                        _Qry &= vbCrLf & " Order By FTEmpCode "

                        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

                        For Each R As DataRow In _dt.Rows
                            HI.HRCAL.Calculate.TransTimeCard(HI.ST.UserInfo.UserName, _NextProcDate, Integer.Parse(Val(R!FNHSysEmpID.ToString)), _Spls)
                        Next

                    End If

                    _NextProcDate = HI.UL.ULDate.ConvertEnDB(HI.UL.ULDate.AddDay(_NextProcDate, 1))

                Loop

            Catch ex As Exception
            End Try

            _Spls.Close()
           
           
        End If
    End Sub
End Class
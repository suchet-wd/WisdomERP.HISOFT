Imports System.Windows.Forms
Imports System.IO

Public Class wConfigScanCard
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub


    Private Sub wConfigScanCard_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try

            Me.ogc.DataSource = Nothing

            Me.FNHSysCmpId.Text = HI.ST.SysInfo.CmpCode


            Dim oDBdt As DataTable
            Dim _Qry As String

            _Qry = " SELECT TOP 1 FNHSysCmpId, FNBarcodeStart, FNBarcodeLength"
            _Qry &= vbCrLf & " , FNDateStart, FNDateLength, FNMonthStart, FNMonthLength"
            _Qry &= vbCrLf & "  , FNYearStart, FNYearLength, FNHourStart, FNHourLength"
            _Qry &= vbCrLf & " , FNMinStart, FNMinLength,FNMachineStart, FNMachineLength"
            _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMConfigScanCard WITH (NOLOCK) "


            _Qry &= vbCrLf & " WHERE FNHSysCmpId=" & HI.ST.SysInfo.CmpID & " "
            oDBdt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

            For Each R As DataRow In oDBdt.Rows

                FNBarcodeStart.Value = Integer.Parse(R!FNBarcodeStart.ToString)
                FNBarcodeLength.Value = Integer.Parse(R!FNBarcodeLength.ToString)

                FNYearStart.Value = Integer.Parse(R!FNYearStart.ToString)
                FNYearLength.Value = Integer.Parse(R!FNYearLength.ToString)

                FNMonthStart.Value = Integer.Parse(R!FNMonthStart.ToString)
                FNMonthLength.Value = Integer.Parse(R!FNMonthLength.ToString)

                FNDateStart.Value = Integer.Parse(R!FNDateStart.ToString)
                FNDateLength.Value = Integer.Parse(R!FNDateLength.ToString)

                FNHourStart.Value = Integer.Parse(R!FNHourStart.ToString)
                FNHourLength.Value = Integer.Parse(R!FNHourLength.ToString)

                FNMinStart.Value = Integer.Parse(R!FNMinStart.ToString)
                FNMinLength.Value = Integer.Parse(R!FNMinLength.ToString)

                FNMachineStart.Value = Integer.Parse(R!FNMachineStart.ToString)
                FNMachineLength.Value = Integer.Parse(R!FNMachineLength.ToString)

                Exit For
            Next

        Catch ex As Exception
        End Try


    End Sub

    Private Sub ocmsave_Click(sender As System.Object, e As System.EventArgs) Handles ocmsave.Click
        Call SaveData()
    End Sub

    Private Sub SaveData()
        Dim _Qry As String

        _Qry = " SELECT TOP 1 FNHSysCmpId, FNBarcodeStart, FNBarcodeLength"
        _Qry &= vbCrLf & " , FNDateStart, FNDateLength, FNMonthStart, FNMonthLength"
        _Qry &= vbCrLf & "  , FNYearStart, FNYearLength, FNHourStart, FNHourLength"
        _Qry &= vbCrLf & " , FNMinStart, FNMinLength,FNMachineStart, FNMachineLength"
        _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMConfigScanCard WITH (NOLOCK) "
        _Qry &= vbCrLf & " WHERE FNHSysCmpId=" & HI.ST.SysInfo.CmpID & " "

        If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_SYSTEM, "") = "" Then
            _Qry = "  INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMConfigScanCard(FTInsUser, FDInsDate, FTInsTime, FNHSysCmpId) "
            _Qry &= vbCrLf & " SELECT  '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
            _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & " "
            _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & " "
            _Qry &= vbCrLf & " ," & HI.ST.SysInfo.CmpID & " "

            HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

        End If

        _Qry = " UPDATE  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMConfigScanCard "
        _Qry &= vbCrLf & " SET FNBarcodeStart=" & FNBarcodeStart.Value & " "
        _Qry &= vbCrLf & ", FNBarcodeLength=" & FNBarcodeLength.Value & " "
        _Qry &= vbCrLf & " , FNDateStart=" & FNDateStart.Value & ""
        _Qry &= vbCrLf & " , FNDateLength=" & FNDateLength.Value & ""
        _Qry &= vbCrLf & " , FNMonthStart=" & FNMonthStart.Value & ""
        _Qry &= vbCrLf & " , FNMonthLength=" & FNMonthLength.Value & ""
        _Qry &= vbCrLf & "  , FNYearStart=" & FNYearStart.Value & ""
        _Qry &= vbCrLf & " , FNYearLength=" & FNYearLength.Value & ""
        _Qry &= vbCrLf & " , FNHourStart=" & FNHourStart.Value & ""
        _Qry &= vbCrLf & " , FNHourLength=" & FNHourLength.Value & ""
        _Qry &= vbCrLf & " , FNMinStart=" & FNMinStart.Value & ""
        _Qry &= vbCrLf & " , FNMinLength=" & FNMinLength.Value & ""
        _Qry &= vbCrLf & " ,FNMachineStart=" & FNMachineStart.Value & ""
        _Qry &= vbCrLf & " , FNMachineLength=" & FNMachineLength.Value & ""
        _Qry &= vbCrLf & " ,FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
        _Qry &= vbCrLf & " ,FDUpdDate=" & HI.UL.ULDate.FormatDateDB & " "
        _Qry &= vbCrLf & " ,FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & " "
        _Qry &= vbCrLf & " WHERE FNHSysCmpId=" & HI.ST.SysInfo.CmpID & " "

        HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)
        HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
        Me.ogc.DataSource = Nothing
    End Sub
    Private Sub ocmtest_Click(sender As System.Object, e As System.EventArgs) Handles ocmtest.Click

        Call SaveData()
        Try
            Dim Op As New System.Windows.Forms.OpenFileDialog
            Dim _FileName As String = ""
            'Op.Filter = "Text Files(*.txt)|*.txt"

            Op.ShowDialog()
            Try
                If Op.FileName <> "" Then

                    _FileName = Op.FileName

                    ' Dim _Qry As String
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
                    Dim _dttime As New DataTable
                    With _dttime
                        .Columns.Add("FTBarcodeNo", GetType(String))
                        .Columns.Add("FTDate", GetType(String))
                        .Columns.Add("FTMonth", GetType(String))
                        .Columns.Add("FTYear", GetType(String))
                        .Columns.Add("FTTimeH", GetType(String))
                        .Columns.Add("FTTimeM", GetType(String))
                        .Columns.Add("FTMachineNo", GetType(String))
                    End With

                    Call HI.HRCAL.Time.GETbScanCtrlcfg(_IDStart, _IDLenght, _YearStart, _YearLenght, _CardMonthStart, _CardMonthLenght, _CardDayStart, _CardDayLenght, _CardHourStart, _CardHourLenght, _CardMinuteStart, _CardMinuteLen, _CardStationStart, _CardStatainLenght)

                    Dim dtsource As New DataTable("ReadTimeCard")
                    dtsource.Columns.Add("FTData", GetType(String))
                    Dim lines() As String

                    Dim fields() As String
                    Dim _toatlLine As Integer = 0
                    Dim _Rec As Integer = 0

                    Dim _Spls As New HI.TL.SplashScreen("Reading time Card...   Please Wait   ")
                    Try

                        lines = File.ReadAllLines(_FileName)
                       
                        For linenum As Integer = 0 To lines.Length - 1
                            _toatlLine = _toatlLine + 1
                            dtsource.Rows.Add(lines(linenum))
                        Next

                        _Rec = 0
                        Dim _CheckDate As String = ""
                        Dim _FoundShift As Boolean = True
                        Dim _StrLineInput As String
                        Dim _StationScan As String
                        Dim _DataTimeScan As String
                        Dim _EmpIDScan As String
            

                        For Each R As DataRow In dtsource.Rows
                            _Rec = _Rec + 1
                            _Spls.UpdateInformation("Check Data Record  " & _Rec.ToString & " Of " & _toatlLine.ToString & "  (" & Format((_Rec * 100.0) / _toatlLine, "0.00") & " % ) ")

                            _StrLineInput = R.Item(0).ToString

                            If _StrLineInput <> "" Then
                   
                                _DataTimeScan = Mid(_StrLineInput, _CardHourStart, _CardHourLenght) & Mid(_StrLineInput, _CardMinuteStart, _CardMinuteLen)
                                _EmpIDScan = Mid(_StrLineInput, _IDStart, _IDLenght)
                                _StationScan = Mid(_StrLineInput, _CardStationStart, _CardStatainLenght)

                                _dttime.Rows.Add(_EmpIDScan, Mid(_StrLineInput, _CardDayStart, _CardDayLenght), Mid(_StrLineInput, _CardMonthStart, _CardMonthLenght), Mid(_StrLineInput, _YearStart, _YearLenght), Mid(_StrLineInput, _CardHourStart, _CardHourLenght), Mid(_StrLineInput, _CardMinuteStart, _CardMinuteLen), _StationScan)
                            End If

                        Next

                        Me.ogc.DataSource = _dttime

                        _Spls.Close()

                    Catch ex As Exception
                        _Spls.Close()
                        MessageBox.Show("Could not read file: " & _FileName)
                        Exit Sub
                    End Try
                End If
            Catch ex As Exception
            End Try
        Catch ex As Exception
        End Try

    End Sub

    Private Function _TimeOver() As String
        Throw New NotImplementedException
    End Function

    Private Sub ocmexit_Click(sender As System.Object, e As System.EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

End Class
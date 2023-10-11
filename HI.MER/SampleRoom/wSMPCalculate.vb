Public Class wSMPCalculate

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        Call InitGrid()


    End Sub

#Region "Initial Grid"
    Private Sub InitGrid()

        '------Start Add Summary Grid-------------
        Dim sFieldCount As String = ""
        Dim sFieldSum As String = "H01|H02|H03|H04|H05|H06|H07|H08|H09|H10|H11|H12|H13|Total|TotalPack"
        Dim sFieldGrpCount As String = ""
        Dim sFieldGrpSum As String = "H01|H02|H03|H04|H05|H06|H07|H08|H09|H10|H11|H12|H13|Total|TotalPack"

        With ogv
            '.ClearGrouping()
            '.ClearDocument()
            '.Columns.ColumnByFieldName("FDScanDateGrp").Group()
            'For Each Str As String In sFieldCount.Split("|")
            '    If Str <> "" Then
            '        .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Count, Str)
            '        .Columns(Str).SummaryItem.DisplayFormat = "{0:n0}"
            '    End If
            'Next

            'For Each Str As String In sFieldSum.Split("|")
            '    If Str <> "" Then
            '        .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Sum, Str)
            '        .Columns(Str).SummaryItem.DisplayFormat = "{0:n0}"
            '    End If
            'Next

            'For Each Str As String In sFieldGrpCount.Split("|")
            '    If Str <> "" Then
            '        .GroupSummary.Add(DevExpress.Data.SummaryItemType.Count, Str, Nothing, "(Count by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n0})")
            '    End If
            'Next

            'For Each Str As String In sFieldGrpSum.Split("|")
            '    If Str <> "" Then
            '        .GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, Str, Nothing, "(Sum by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n0})")
            '    End If
            'Next

            '.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
            '.OptionsView.ShowFooter = True

        End With
        '------End Add Summary Grid-------------
    End Sub


#End Region

#Region "Property"

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

#End Region

#Region "MAIN PROC"

    Private Sub ProcessSave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmcalculateincentive.Click
        Dim _StateProcess As Boolean = False
        If Me.VerrifyData Then

            If HI.MG.ShowMsg.mConfirmProcess("คุณต้องการทำการคำนวณ Incentive ของพนักงาน ใช่หรือไม่ ?", 1604220574, Me.Text) = True Then

                Dim _Spls As New HI.TL.SplashScreen("Calculating...   Please Wait   ")

                Call DeleteData(_Spls)

                _StateProcess = CalculateSewingHour(_Spls)

                If (_StateProcess) Then

                    _Spls.Close()
                    HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)

                    Me.ocmload_Click(ocmload, New System.EventArgs)

                Else

                    _Spls.Close()
                    HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)

                End If

            End If

        End If

    End Sub

    Private Sub ProcessClear(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmclear.Click

        Me.ogc.DataSource = Nothing
        ochkselectall.Checked = False

        HI.TL.HandlerControl.ClearControl(Me)
        Me.FTStartDate.Focus()

    End Sub

    Private Sub ProcessClose(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

#End Region

#Region " Procedure "

    Private Function GetEmpTimeMoveOut(_UnitSectID As Integer, _CalDate As String, Optional _FNHSysEmpID As Integer = 0) As DataTable
        Dim _dt As DataTable
        Dim _Qry As String = ""
        _Qry = "SELECT  FNHSysEmpID, FDDate, FNHSysEmpTypeId, FNHSysUnitSectId, FNHSysEmpTypeIdTo, FNHSysUnitSectIdTo, FTStartTime, FTEndTime,  FNTotalMinute"
        _Qry &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTMoveTeamMoveType AS X WITH(NOLOCK)"

        If _FNHSysEmpID = 0 Then

            _Qry &= vbCrLf & "   WHERE FNHSysUnitSectId=" & _UnitSectID & " "
            _Qry &= vbCrLf & "   AND FDDate='" & HI.UL.ULDate.ConvertEnDB(_CalDate) & "' "
            _Qry &= vbCrLf & "   AND FNHSysEmpTypeIdTo<>" & _UnitSectID & " "

        Else

            _Qry &= vbCrLf & "   WHERE FNHSysEmpID=0 "
            _Qry &= vbCrLf & "   AND FDDate='" & HI.UL.ULDate.ConvertEnDB(_CalDate) & "' "
            _Qry &= vbCrLf & "   AND FNHSysEmpTypeIdTo<>" & _UnitSectID & " "

        End If

        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

        Return _dt

    End Function

    Private Function GetEmpTime(_CalDate As String, _TFNHSysEmpID As Integer) As DataTable
        Dim _dt As DataTable

        Dim _dttime As DataTable
        Dim _dttimeLeave As DataTable
        Dim _dtemptime As New DataTable
        Dim _UnitSectID As Integer = 0

        _dtemptime.Columns.Add("FTEmpCode", GetType(String))
        _dtemptime.Columns.Add("FNHSysEmpID", GetType(Integer))
        _dtemptime.Columns.Add("FNH01", GetType(Integer))
        _dtemptime.Columns.Add("FNH02", GetType(Integer))
        _dtemptime.Columns.Add("FNH03", GetType(Integer))
        _dtemptime.Columns.Add("FNH04", GetType(Integer))
        _dtemptime.Columns.Add("FNH05", GetType(Integer))
        _dtemptime.Columns.Add("FNH06", GetType(Integer))
        _dtemptime.Columns.Add("FNH07", GetType(Integer))
        _dtemptime.Columns.Add("FNH08", GetType(Integer))
        _dtemptime.Columns.Add("FNH09", GetType(Integer))
        _dtemptime.Columns.Add("FNH10", GetType(Integer))
        _dtemptime.Columns.Add("FNH11", GetType(Integer))
        _dtemptime.Columns.Add("FNH12", GetType(Integer))
        _dtemptime.Columns.Add("FNH13", GetType(Integer))
        _dtemptime.Columns.Add("FNLateNormalMin", GetType(Integer))
        _dtemptime.Columns.Add("FNLateNormalCut", GetType(Integer))
        _dtemptime.Columns.Add("FNRetireNormalMin", GetType(Integer))
        _dtemptime.Columns.Add("FNRetireNormalCut", GetType(Integer))
        _dtemptime.Columns.Add("FNLateOtMin", GetType(Integer))
        _dtemptime.Columns.Add("FNLateOtCut", GetType(Integer))
        _dtemptime.Columns.Add("FNRetireOtMin", GetType(Integer))
        _dtemptime.Columns.Add("FNRetireOtCut", GetType(Integer))
        _dtemptime.Columns.Add("FNAbsentCut", GetType(Integer))
        _dtemptime.Columns.Add("FNAbsent", GetType(Integer))
        _dtemptime.Columns.Add("FNTimeMin", GetType(Integer))
        _dtemptime.Columns.Add("FNOT1Min", GetType(Integer))
        _dtemptime.Columns.Add("FNOT1_5Min", GetType(Integer))
        _dtemptime.Columns.Add("FNOT2Min", GetType(Integer))
        _dtemptime.Columns.Add("FNOT3Min", GetType(Integer))
        _dtemptime.Columns.Add("FNOT4Min", GetType(Integer))
        _dtemptime.Columns.Add("FNLateMMin", GetType(Integer))
        _dtemptime.Columns.Add("FNLateAfMin", GetType(Integer))
        _dtemptime.Columns.Add("FNRetireMMin", GetType(Integer))
        _dtemptime.Columns.Add("FNRetireAfMin", GetType(Integer))
        _dtemptime.Columns.Add("FNOTRequestMin", GetType(Integer))
        _dtemptime.Columns.Add("FNCutAbsent", GetType(Integer))
        _dtemptime.Columns.Add("FNLateNormalNotCut", GetType(Integer))
        _dtemptime.Columns.Add("FTStartTime", GetType(String))
        _dtemptime.Columns.Add("FTEndTime", GetType(String))
        _dtemptime.Columns.Add("FNTotalMinute", GetType(Integer))
        _dtemptime.Columns.Add("FTState", GetType(String))
        _dtemptime.Columns.Add("FNSalary", GetType(Double))
        _dtemptime.Columns.Add("FNWorkingMin", GetType(Double))
        _dtemptime.Columns.Add("FNWorkingOTMin", GetType(Double))
        _dtemptime.Columns.Add("FTStateCal", GetType(String))
        _dtemptime.Columns.Add("FNTotalWorkingMin", GetType(Double))
        _dtemptime.Columns.Add("FTStateDaily", GetType(String))
        _dtemptime.Columns.Add("FTStatePlagnent", GetType(String))
        _dtemptime.Columns.Add("FTStateRelease", GetType(String))
        _dtemptime.Columns.Add("FNAmtFixedIncentive", GetType(String))
        _dtemptime.Columns.Add("FTStateTrain", GetType(String))


        _dtemptime.Columns.Add("FNHRH01", GetType(Integer))
        _dtemptime.Columns.Add("FNHRH02", GetType(Integer))
        _dtemptime.Columns.Add("FNHRH03", GetType(Integer))
        _dtemptime.Columns.Add("FNHRH04", GetType(Integer))
        _dtemptime.Columns.Add("FNHRH05", GetType(Integer))
        _dtemptime.Columns.Add("FNHRH06", GetType(Integer))
        _dtemptime.Columns.Add("FNHRH07", GetType(Integer))
        _dtemptime.Columns.Add("FNHRH08", GetType(Integer))
        _dtemptime.Columns.Add("FNHRH09", GetType(Integer))
        _dtemptime.Columns.Add("FNHRH10", GetType(Integer))
        _dtemptime.Columns.Add("FNHRH11", GetType(Integer))
        _dtemptime.Columns.Add("FNHRH12", GetType(Integer))
        _dtemptime.Columns.Add("FNHRH13", GetType(Integer))

        Dim _Qry As String = ""
        Dim _FNHSysEmpID As Integer
        Dim _FTIn1 As String
        Dim _FTOut1 As String
        Dim _FTIn2 As String
        Dim _FTOut2 As String
        Dim _FTIn3 As String
        Dim _FTOut3 As String
        Dim _FTIn4 As String
        Dim _FTOut4 As String

        Dim _FNLateNormalMin As Integer
        Dim _FNLateNormalCut As Integer
        Dim _FNRetireNormalMin As Integer
        Dim _FNRetireNormalCut As Integer
        Dim _FNLateOtMin As Integer
        Dim _FNLateOtCut As Integer
        Dim _FNRetireOtMin As Integer
        Dim _FNRetireOtCut As Integer
        Dim _FNAbsentCut As Integer
        Dim _FNAbsent As Integer
        Dim _FNTimeMin As Integer
        Dim _FNOT1Min As Integer
        Dim _FNOT1_5Min As Integer
        Dim _FNOT2Min As Integer
        Dim _FNOT3Min As Integer
        Dim _FNOT4Min As Integer
        Dim _FNLateMMin As Integer
        Dim _FNLateAfMin As Integer
        Dim _FNRetireMMin As Integer
        Dim _FNRetireAfMin As Integer
        Dim _FNOTRequestMin As Integer
        Dim _FNCutAbsent As Integer
        Dim _FNLateNormalNotCut As Integer
        Dim _FTStartTime As String
        Dim _FTEndTime As String
        Dim _FNTotalMinute As Integer
        Dim _FTState As String
        Dim _FNHour As Integer = 0
        Dim _FNWorkMinute As Integer = 0
        Dim _FNWorkMinuteHR As Integer = 0
        Dim _StartTime As String
        Dim _EndTime As String
        Dim _FNSalary As Double
        Dim _FTStateDaily As String
        Dim _FTStatePlagnent As String
        Dim _FTEmpCode As String = ""
        Dim _FTStateRelease As String = ""
        Dim _FNAmtFixedIncentive As Double = 0
        Dim _FTStateTrain As String = ""
        Dim FNHSysShiftID As Integer = 0
        Dim TimeCheckIn1 As String = ""
        Dim TimeCheckOut1 As String = ""
        Dim TimeCheckIn2 As String = ""
        Dim TimeCheckOut2 As String = ""
        Dim _LeaveStartTime As String = ""
        Dim _LeaveEndTime As String = ""
        Dim _LeaveHomeSpecial As String = ""
        Dim dtcheckTime As New DataTable

        _Qry = "  Select  Top 1 A.FNHSysShiftID,S.FTIn1, S.FTOut1, FTIn2, S.FTOut2,A.FNHSysUnitSectId "
        _Qry &= vbCrLf & "  From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS A With(NOLOCK)  "
        _Qry &= vbCrLf & " INNER JOIN   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMTimeShift AS S With(NOLOCK)  ON A.FNHSysShiftID = S.FNHSysShiftID"
        _Qry &= vbCrLf & " Where (A.FNHSysEmpID = " & _TFNHSysEmpID & ") "
        _Qry &= vbCrLf & " And (A.FNEmpStatus in (0,1) ) "
        _Qry &= vbCrLf & "  Order By FDDateEnd "

        ' FNHSysShiftID = Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "0"))
        dtcheckTime = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

        For Each Rtx As DataRow In dtcheckTime.Rows
            FNHSysShiftID = Val(Rtx!FNHSysShiftID.ToString)
            TimeCheckIn1 = Rtx!FTIn1.ToString
            TimeCheckOut1 = Rtx!FTOut1.ToString
            TimeCheckIn2 = Rtx!FTIn2.ToString
            TimeCheckOut2 = Rtx!FTOut2.ToString
            _UnitSectID = Val(Rtx!FNHSysUnitSectId.ToString)

            Exit For
        Next



        dtcheckTime.Dispose()

        Dim _dtmove As DataTable = GetEmpTimeMoveOut(_UnitSectID, _CalDate, _TFNHSysEmpID)


        _Qry = "Select FTShiftPeriodTimeCode AS FTPeriadOfTimeCode, FTStartTime, FTEndTime,ROW_NUMBER() Over (Order By CASE WHEN ISNULL(FNSeq,0) = 0 THEN FTStartTime ELSE RIGHT('0000' +  Convert(nvarchar(10),FNSeq),4) END) AS FNHour"
        _Qry &= vbCrLf & " FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMShiftPeriodTime As A With(NOLOCK)"
        _Qry &= vbCrLf & " WHERE  FNHSysShiftID =" & FNHSysShiftID & " AND (FTStateActive = '1') AND ISNULL(FTStateBreak,'')<>'1'"
        _Qry &= vbCrLf & " ORDER BY CASE WHEN ISNULL(FNSeq,0) = 0 THEN FTStartTime ELSE RIGHT('0000' +  Convert(nvarchar(10),FNSeq),4) END"
        _dttime = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MASTER)

        If _dttime.Rows.Count <= 0 Then
            _Qry = "Select FTPeriadOfTimeCode, FTStartTime, FTEndTime,ROW_NUMBER() Over (Order By FTStartTime) As FNHour"
            _Qry &= vbCrLf & " FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TPRODMPeiodOfTime As A With(NOLOCK)"
            _Qry &= vbCrLf & " WHERE  (FTStateActive = '1')"
            _Qry &= vbCrLf & " ORDER BY FTStartTime"
            _dttime = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MASTER)
        End If


        _Qry = "EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.SP_GET_EMPTIME_CAL_INCENTIVE " & _UnitSectID & ",'" & HI.UL.ULDate.ConvertEnDB(_CalDate) & "'," & _TFNHSysEmpID & ""
        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

        For Each R As DataRow In _dt.Rows

            _FNHSysEmpID = Integer.Parse(Val(R!FNHSysEmpID.ToString))

            If _FNHSysEmpID = 225710009 Then
                Beep()
            End If

            _FTIn1 = R!FTIn1.ToString
            _FTOut1 = R!FTOut1.ToString
            _FTIn2 = R!FTIn2.ToString
            _FTOut2 = R!FTOut2.ToString
            _FTIn3 = R!FTIn3.ToString
            _FTOut3 = R!FTOut3.ToString
            _FTIn4 = R!FTIn4.ToString
            _FTOut4 = R!FTOut4.ToString
            _FNLateNormalMin = Integer.Parse(Val(R!FNLateNormalMin.ToString))
            _FNLateNormalCut = Integer.Parse(Val(R!FNLateNormalCut.ToString))
            _FNRetireNormalMin = Integer.Parse(Val(R!FNRetireNormalMin.ToString))
            _FNRetireNormalCut = Integer.Parse(Val(R!FNRetireNormalCut.ToString))
            _FNLateOtMin = Integer.Parse(Val(R!FNLateOtMin.ToString))
            _FNLateOtCut = Integer.Parse(Val(R!FNLateOtCut.ToString))
            _FNRetireOtMin = Integer.Parse(Val(R!FNRetireOtMin.ToString))
            _FNRetireOtCut = Integer.Parse(Val(R!FNRetireOtCut.ToString))
            _FNAbsentCut = Integer.Parse(Val(R!FNAbsentCut.ToString))
            _FNAbsent = Integer.Parse(Val(R!FNAbsent.ToString))
            _FNTimeMin = Integer.Parse(Val(R!FNTimeMin.ToString))
            _FNOT1Min = Integer.Parse(Val(R!FNOT1Min.ToString))
            _FNOT1_5Min = Integer.Parse(Val(R!FNOT1_5Min.ToString))
            _FNOT2Min = Integer.Parse(Val(R!FNOT2Min.ToString))
            _FNOT3Min = Integer.Parse(Val(R!FNOT3Min.ToString))
            _FNOT4Min = Integer.Parse(Val(R!FNOT4Min.ToString))
            _FNLateMMin = Integer.Parse(Val(R!FNLateMMin.ToString))
            _FNLateAfMin = Integer.Parse(Val(R!FNLateAfMin.ToString))
            _FNRetireMMin = Integer.Parse(Val(R!FNRetireMMin.ToString))
            _FNRetireAfMin = Integer.Parse(Val(R!FNRetireAfMin.ToString))
            _FNOTRequestMin = Integer.Parse(Val(R!FNOTRequestMin.ToString))
            _FNCutAbsent = Integer.Parse(Val(R!FNCutAbsent.ToString))
            _FNLateNormalNotCut = Integer.Parse(Val(R!FNLateNormalNotCut.ToString))
            _FTStartTime = R!FTStartTime.ToString
            _FTEndTime = R!FTEndTime.ToString
            _FNTotalMinute = Integer.Parse(Val(R!FNTotalMinute.ToString))
            _FTState = R!FTState.ToString
            _FNSalary = Double.Parse(Val(R!FNSalary.ToString))
            _FTStateDaily = R!FTStateDaily.ToString
            _FTStatePlagnent = R!FTStatePlagnent.ToString
            _FTEmpCode = R!FTEmpCode.ToString
            _FTStateRelease = R!FTStateRelease.ToString
            _FNAmtFixedIncentive = Val(R!FNAmtFixedIncentive.ToString)
            _FTStateTrain = R!FTStateTrain.ToString

            _Qry = " SELECT FNHSysEmpID, FTDateTrans, FTLeaveType, FTLeaveStartTime, FTLeaveEndTime"
            _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTransLeave AS X WITH(NOLOCK)"
            _Qry &= vbCrLf & " WHERE FNHSysEmpID= " & _FNHSysEmpID & " "
            _Qry &= vbCrLf & " AND FTDateTrans='" & HI.UL.ULDate.ConvertEnDB(_CalDate) & "'"

            _dttimeLeave = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

            For Each Rxt As DataRow In _dttime.Rows

                _StartTime = Rxt!FTStartTime.ToString
                _EndTime = Rxt!FTEndTime.ToString

                _FNHour = Integer.Parse(Val(Rxt!FNHour.ToString))
                _FNWorkMinute = 0
                _FNWorkMinuteHR = 0


                _LeaveStartTime = ""
                _LeaveEndTime = ""

                If _dttimeLeave.Select("FTLeaveStartTime<='" & _StartTime & "' AND FTLeaveEndTime>='" & _EndTime & "' ").Length <= 0 Then
                    If _dttimeLeave.Select("FTLeaveStartTime<='" & _StartTime & "' AND FTLeaveEndTime<'" & _EndTime & "' ").Length > 0 Then

                        For Each Rxtl As DataRow In _dttimeLeave.Select("FTLeaveStartTime<='" & _StartTime & "' AND FTLeaveEndTime<'" & _EndTime & "' ")
                            _LeaveEndTime = Rxtl!FTLeaveEndTime.ToString()
                            Exit For
                        Next

                    Else

                        If _dttimeLeave.Select("FTLeaveStartTime>'" & _StartTime & "' AND FTLeaveEndTime<'" & _EndTime & "' ").Length > 0 Then

                            For Each Rxtl As DataRow In _dttimeLeave.Select("FTLeaveStartTime>'" & _StartTime & "' AND FTLeaveEndTime<'" & _EndTime & "' ")
                                _LeaveStartTime = Rxtl!FTLeaveStartTime.ToString()
                                _LeaveEndTime = Rxtl!FTLeaveEndTime.ToString()
                                Exit For
                            Next

                        Else

                            If _dttimeLeave.Select("FTLeaveStartTime>'" & _StartTime & "' AND FTLeaveEndTime>='" & _EndTime & "' ").Length > 0 Then
                                For Each Rxtl As DataRow In _dttimeLeave.Select("FTLeaveStartTime>'" & _StartTime & "' AND FTLeaveEndTime<'" & _EndTime & "' ")
                                    _LeaveStartTime = Rxtl!FTLeaveStartTime.ToString()
                                    Exit For
                                Next

                            End If

                        End If

                    End If

                End If

                If _LeaveHomeSpecial <> "" Then
                    If _EndTime > _LeaveHomeSpecial Then
                        _LeaveStartTime = _LeaveHomeSpecial
                        _LeaveEndTime = "19:00"
                    End If

                End If


                If _LeaveEndTime <> "" Or _LeaveStartTime <> "" Then

                    If _LeaveStartTime < _StartTime Then
                        _LeaveStartTime = ""
                    End If
                    If _LeaveEndTime < _StartTime Then
                        _LeaveEndTime = ""
                    End If
                End If


                If _dtmove.Select("FNHSysEmpID=" & _FNHSysEmpID & " AND FTStartTime<='" & _StartTime & "' AND FTEndTime>='" & _EndTime & "'").Length <= 0 Then
                    Select Case _FNHour
                        Case 1, 2, 3, 4

                            If _FTState = "1" Then

                                Select Case True
                                    Case (_FTIn1 <= _StartTime And _FTOut1 >= _EndTime) And (_FTStartTime <= _StartTime And _FTEndTime >= _EndTime)

                                        '_FNWorkMinute = 60
                                        If _dttimeLeave.Select("FTLeaveStartTime<='" & _StartTime & "' AND FTLeaveEndTime>='" & _EndTime & "' ").Length <= 0 Then
                                            _FNWorkMinute = 60
                                            _FNWorkMinuteHR = 60
                                        Else
                                            _FNWorkMinute = 0
                                            _FNWorkMinuteHR = 0
                                        End If

                                    Case (_StartTime < _FTIn1 And _FTOut1 >= _EndTime And _FTIn1 <> "") And (_FTStartTime <= _StartTime And _FTEndTime >= _EndTime)

                                        Try
                                            ' _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn1), CDate(_CalDate & " " & _EndTime))

                                            If _dttimeLeave.Select("FTLeaveStartTime<='" & _StartTime & "' AND FTLeaveEndTime>='" & _EndTime & "' ").Length <= 0 Then

                                                If _LeaveStartTime <> "" OrElse _LeaveEndTime <> "" Then
                                                    Select Case True
                                                        Case (_LeaveStartTime <> "" And _LeaveEndTime <> "")

                                                            _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn1), CDate(_CalDate & " " & _LeaveStartTime))
                                                            _FNWorkMinute = _FNWorkMinute + DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _LeaveEndTime), CDate(_CalDate & " " & _EndTime))
                                                        Case (_LeaveStartTime <> "" And _LeaveEndTime = "")
                                                            _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn1), CDate(_CalDate & " " & _LeaveStartTime))
                                                        Case (_LeaveStartTime = "" And _LeaveEndTime <> "")
                                                            _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _LeaveEndTime), CDate(_CalDate & " " & _EndTime))
                                                    End Select
                                                    If _FNWorkMinute < 0 Then
                                                        _FNWorkMinute = 0
                                                    End If
                                                    _FNWorkMinuteHR = _FNWorkMinute

                                                Else

                                                    _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn1), CDate(_CalDate & " " & _EndTime))
                                                    _FNWorkMinuteHR = 60

                                                End If

                                            Else

                                                _FNWorkMinute = 0
                                                _FNWorkMinuteHR = 0

                                            End If

                                        Catch ex As Exception
                                        End Try

                                    Case (_StartTime < _FTIn1 And _FTOut1 < _EndTime And _FTIn1 <> "" And _FTOut1 <> "" And _FTIn1 < _FTOut1) And (_FTStartTime <= _StartTime And _FTEndTime >= _EndTime)

                                        Try
                                            ' _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn1), CDate(_CalDate & " " & _FTOut1))

                                            If _dttimeLeave.Select("FTLeaveStartTime<='" & _StartTime & "' AND FTLeaveEndTime>='" & _EndTime & "' ").Length <= 0 Then


                                                If _LeaveStartTime <> "" OrElse _LeaveEndTime <> "" Then
                                                    Select Case True
                                                        Case (_LeaveStartTime <> "" And _LeaveEndTime <> "")

                                                            _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn1), CDate(_CalDate & " " & _LeaveStartTime))
                                                            _FNWorkMinute = _FNWorkMinute + DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _LeaveEndTime), CDate(_CalDate & " " & _FTOut1))
                                                        Case (_LeaveStartTime <> "" And _LeaveEndTime = "")
                                                            _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn1), CDate(_CalDate & " " & _LeaveStartTime))
                                                        Case (_LeaveStartTime = "" And _LeaveEndTime <> "")
                                                            _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _LeaveEndTime), CDate(_CalDate & " " & _FTOut1))
                                                    End Select
                                                    If _FNWorkMinute < 0 Then
                                                        _FNWorkMinute = 0
                                                    End If
                                                    _FNWorkMinuteHR = _FNWorkMinute

                                                Else
                                                    _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn1), CDate(_CalDate & " " & _FTOut1))
                                                    _FNWorkMinuteHR = 60
                                                End If

                                            Else
                                                _FNWorkMinute = 0
                                                _FNWorkMinuteHR = 0
                                            End If

                                        Catch ex As Exception
                                        End Try

                                    Case (_StartTime >= _FTIn1 And _FTOut1 < _EndTime And _FTIn1 <> "" And _FTEndTime >= _EndTime)
                                        Try
                                            ' _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _FTOut1))

                                            If _dttimeLeave.Select("FTLeaveStartTime<='" & _StartTime & "' AND FTLeaveEndTime>='" & _EndTime & "' ").Length <= 0 Then


                                                If _LeaveStartTime <> "" OrElse _LeaveEndTime <> "" Then
                                                    Select Case True
                                                        Case (_LeaveStartTime <> "" And _LeaveEndTime <> "")

                                                            _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _LeaveStartTime))
                                                            _FNWorkMinute = _FNWorkMinute + DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _LeaveEndTime), CDate(_CalDate & " " & _FTOut1))
                                                        Case (_LeaveStartTime <> "" And _LeaveEndTime = "")
                                                            _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _LeaveStartTime))
                                                        Case (_LeaveStartTime = "" And _LeaveEndTime <> "")
                                                            _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _LeaveEndTime), CDate(_CalDate & " " & _FTOut1))
                                                    End Select
                                                    If _FNWorkMinute < 0 Then
                                                        _FNWorkMinute = 0
                                                    End If
                                                    _FNWorkMinuteHR = _FNWorkMinute

                                                Else
                                                    _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _FTOut1))
                                                    _FNWorkMinuteHR = 0

                                                End If

                                            Else
                                                _FNWorkMinute = 0
                                                _FNWorkMinuteHR = 0
                                            End If

                                        Catch ex As Exception
                                        End Try
                                End Select

                            Else

                                Select Case True
                                    Case (_FTIn1 <= _StartTime And _FTOut1 >= _EndTime And _FTIn1 <> "")

                                        ' _FNWorkMinute = 60
                                        If _dttimeLeave.Select("FTLeaveStartTime<='" & _StartTime & "' AND FTLeaveEndTime>='" & _EndTime & "' ").Length <= 0 Then
                                            _FNWorkMinute = 60
                                            _FNWorkMinuteHR = 60
                                        Else
                                            _FNWorkMinute = 0
                                            _FNWorkMinuteHR = 0
                                        End If
                                    Case (_StartTime < _FTIn1 And _FTOut1 >= _EndTime And _FTIn1 <> "")

                                        Try
                                            '_FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn1), CDate(_CalDate & " " & _EndTime))

                                            If _dttimeLeave.Select("FTLeaveStartTime<='" & _StartTime & "' AND FTLeaveEndTime>='" & _EndTime & "' ").Length <= 0 Then



                                                If _LeaveStartTime <> "" OrElse _LeaveEndTime <> "" Then
                                                    Select Case True
                                                        Case (_LeaveStartTime <> "" And _LeaveEndTime <> "")

                                                            _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn1), CDate(_CalDate & " " & _LeaveStartTime))
                                                            _FNWorkMinute = _FNWorkMinute + DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _LeaveEndTime), CDate(_CalDate & " " & _EndTime))
                                                        Case (_LeaveStartTime <> "" And _LeaveEndTime = "")
                                                            _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn1), CDate(_CalDate & " " & _LeaveStartTime))
                                                        Case (_LeaveStartTime = "" And _LeaveEndTime <> "")
                                                            _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _LeaveEndTime), CDate(_CalDate & " " & _EndTime))
                                                    End Select
                                                    If _FNWorkMinute < 0 Then
                                                        _FNWorkMinute = 0
                                                    End If
                                                    _FNWorkMinuteHR = _FNWorkMinute

                                                Else
                                                    _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn1), CDate(_CalDate & " " & _EndTime))
                                                    _FNWorkMinuteHR = 60

                                                End If


                                            Else



                                                _FNWorkMinute = 0
                                                _FNWorkMinuteHR = 0
                                            End If

                                        Catch ex As Exception
                                        End Try

                                    Case (_StartTime < _FTIn1 And _FTOut1 < _EndTime And _FTIn1 <> "" And _FTOut1 <> "" And _FTIn1 < _FTOut1)

                                        Try
                                            ' _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn1), CDate(_CalDate & " " & _FTOut1))

                                            If _dttimeLeave.Select("FTLeaveStartTime<='" & _StartTime & "' AND FTLeaveEndTime>='" & _EndTime & "' ").Length <= 0 Then


                                                If _LeaveStartTime <> "" OrElse _LeaveEndTime <> "" Then
                                                    Select Case True
                                                        Case (_LeaveStartTime <> "" And _LeaveEndTime <> "")

                                                            _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn1), CDate(_CalDate & " " & _LeaveStartTime))
                                                            _FNWorkMinute = _FNWorkMinute + DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _LeaveEndTime), CDate(_CalDate & " " & _FTOut1))
                                                        Case (_LeaveStartTime <> "" And _LeaveEndTime = "")
                                                            _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn1), CDate(_CalDate & " " & _LeaveStartTime))
                                                        Case (_LeaveStartTime = "" And _LeaveEndTime <> "")
                                                            _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _LeaveEndTime), CDate(_CalDate & " " & _FTOut1))
                                                    End Select
                                                    If _FNWorkMinute < 0 Then
                                                        _FNWorkMinute = 0
                                                    End If
                                                    _FNWorkMinuteHR = _FNWorkMinute

                                                Else
                                                    _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn1), CDate(_CalDate & " " & _FTOut1))
                                                    _FNWorkMinuteHR = 60


                                                End If


                                            Else
                                                _FNWorkMinute = 0
                                                _FNWorkMinuteHR = 0
                                            End If

                                        Catch ex As Exception
                                        End Try
                                    Case (_StartTime >= _FTIn1 And _FTOut1 < _EndTime And _FTIn1 <> "" And _FTEndTime >= _EndTime)
                                        Try
                                            ' _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _FTOut1))

                                            If _dttimeLeave.Select("FTLeaveStartTime<='" & _StartTime & "' AND FTLeaveEndTime>='" & _EndTime & "' ").Length <= 0 Then


                                                If _LeaveStartTime <> "" OrElse _LeaveEndTime <> "" Then
                                                    Select Case True
                                                        Case (_LeaveStartTime <> "" And _LeaveEndTime <> "")

                                                            _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _LeaveStartTime))
                                                            _FNWorkMinute = _FNWorkMinute + DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _LeaveEndTime), CDate(_CalDate & " " & _FTOut1))
                                                        Case (_LeaveStartTime <> "" And _LeaveEndTime = "")
                                                            _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _LeaveStartTime))
                                                        Case (_LeaveStartTime = "" And _LeaveEndTime <> "")
                                                            _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _LeaveEndTime), CDate(_CalDate & " " & _FTOut1))
                                                    End Select
                                                    If _FNWorkMinute < 0 Then
                                                        _FNWorkMinute = 0
                                                    End If
                                                    _FNWorkMinuteHR = _FNWorkMinute

                                                Else
                                                    _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _FTOut1))
                                                    _FNWorkMinuteHR = 60


                                                End If

                                            Else
                                                _FNWorkMinute = 0
                                                _FNWorkMinuteHR = 0
                                            End If

                                        Catch ex As Exception
                                        End Try
                                End Select

                            End If

                        Case 5, 6, 7, 8

                            If _FTState = "1" Then

                                Select Case True

                                    Case (_FTIn2 <= _StartTime And _FTOut2 >= _EndTime) And (_FTStartTime <= _StartTime And _FTEndTime >= _EndTime) And (_FTIn2 <> "" And _FTOut2 <> "")

                                        ' _FNWorkMinute = 60

                                        If _dttimeLeave.Select("FTLeaveStartTime<='" & _StartTime & "' AND FTLeaveEndTime>='" & _EndTime & "' ").Length <= 0 Then
                                            _FNWorkMinute = 60
                                            _FNWorkMinuteHR = 60
                                        Else
                                            _FNWorkMinute = 0
                                            _FNWorkMinuteHR = 0
                                        End If

                                    Case (_StartTime < _FTIn2 And _FTOut2 >= _EndTime And _FTIn2 <> "") And (_FTStartTime <= _StartTime And _FTEndTime >= _EndTime) And (_FTIn2 <> "" And _FTOut2 <> "")

                                        Try
                                            ' _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn2), CDate(_CalDate & " " & _EndTime))

                                            If _dttimeLeave.Select("FTLeaveStartTime<='" & _StartTime & "' AND FTLeaveEndTime>='" & _EndTime & "' ").Length <= 0 Then

                                                If _LeaveStartTime <> "" OrElse _LeaveEndTime <> "" Then
                                                    Select Case True
                                                        Case (_LeaveStartTime <> "" And _LeaveEndTime <> "")

                                                            _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn2), CDate(_CalDate & " " & _LeaveStartTime))
                                                            _FNWorkMinute = _FNWorkMinute + DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _LeaveEndTime), CDate(_CalDate & " " & _EndTime))
                                                        Case (_LeaveStartTime <> "" And _LeaveEndTime = "")
                                                            _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn2), CDate(_CalDate & " " & _LeaveStartTime))
                                                        Case (_LeaveStartTime = "" And _LeaveEndTime <> "")
                                                            _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _LeaveEndTime), CDate(_CalDate & " " & _EndTime))
                                                    End Select
                                                    If _FNWorkMinute < 0 Then
                                                        _FNWorkMinute = 0
                                                    End If
                                                    _FNWorkMinuteHR = _FNWorkMinute

                                                Else
                                                    _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn2), CDate(_CalDate & " " & _EndTime))
                                                    _FNWorkMinuteHR = 60



                                                End If

                                            Else
                                                _FNWorkMinute = 0
                                                _FNWorkMinuteHR = 0
                                            End If

                                        Catch ex As Exception
                                        End Try

                                    Case (_StartTime < _FTIn2 And _FTOut2 < _EndTime And _FTIn2 <> "" And _FTOut2 <> "" And _FTIn2 < _FTOut2) And (_FTStartTime <= _StartTime And _FTEndTime >= _EndTime) And (_FTIn2 <> "" And _FTOut2 <> "")

                                        Try
                                            '_FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn2), CDate(_CalDate & " " & _FTOut2))
                                            If _dttimeLeave.Select("FTLeaveStartTime<='" & _StartTime & "' AND FTLeaveEndTime>='" & _EndTime & "' ").Length <= 0 Then


                                                If _LeaveStartTime <> "" OrElse _LeaveEndTime <> "" Then
                                                    Select Case True
                                                        Case (_LeaveStartTime <> "" And _LeaveEndTime <> "")

                                                            _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn2), CDate(_CalDate & " " & _LeaveStartTime))
                                                            _FNWorkMinute = _FNWorkMinute + DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _LeaveEndTime), CDate(_CalDate & " " & _FTOut2))
                                                        Case (_LeaveStartTime <> "" And _LeaveEndTime = "")
                                                            _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn2), CDate(_CalDate & " " & _LeaveStartTime))
                                                        Case (_LeaveStartTime = "" And _LeaveEndTime <> "")
                                                            _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _LeaveEndTime), CDate(_CalDate & " " & _FTOut2))
                                                    End Select
                                                    If _FNWorkMinute < 0 Then
                                                        _FNWorkMinute = 0
                                                    End If
                                                    _FNWorkMinuteHR = _FNWorkMinute

                                                Else
                                                    _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn2), CDate(_CalDate & " " & _FTOut2))
                                                    _FNWorkMinuteHR = 60



                                                End If

                                            Else
                                                _FNWorkMinute = 0
                                                _FNWorkMinuteHR = 0
                                            End If
                                        Catch ex As Exception
                                        End Try
                                    Case (_StartTime >= _FTIn2 And _FTOut2 < _EndTime And _FTIn2 <> "") And (_FTIn2 <> "" And _FTOut2 <> "" And _FTEndTime >= _EndTime)
                                        Try
                                            ' _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _FTOut2))

                                            If _dttimeLeave.Select("FTLeaveStartTime<='" & _StartTime & "' AND FTLeaveEndTime>='" & _EndTime & "' ").Length <= 0 Then


                                                If _LeaveStartTime <> "" OrElse _LeaveEndTime <> "" Then
                                                    Select Case True
                                                        Case (_LeaveStartTime <> "" And _LeaveEndTime <> "")

                                                            _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _LeaveStartTime))
                                                            _FNWorkMinute = _FNWorkMinute + DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _LeaveEndTime), CDate(_CalDate & " " & _FTOut2))
                                                        Case (_LeaveStartTime <> "" And _LeaveEndTime = "")
                                                            _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _LeaveStartTime))
                                                        Case (_LeaveStartTime = "" And _LeaveEndTime <> "")
                                                            _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _LeaveEndTime), CDate(_CalDate & " " & _FTOut2))
                                                    End Select
                                                    If _FNWorkMinute < 0 Then
                                                        _FNWorkMinute = 0
                                                    End If
                                                    _FNWorkMinuteHR = _FNWorkMinute

                                                Else
                                                    _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _FTOut2))
                                                    _FNWorkMinuteHR = 60



                                                End If

                                            Else
                                                _FNWorkMinute = 0
                                                _FNWorkMinuteHR = 0
                                            End If

                                        Catch ex As Exception
                                        End Try
                                End Select

                            Else

                                Select Case True

                                    Case (_FTIn2 <= _StartTime And _FTOut2 >= _EndTime) And (_FTIn2 <> "" And _FTOut2 <> "")

                                        '_FNWorkMinute = 60
                                        If _dttimeLeave.Select("FTLeaveStartTime<='" & _StartTime & "' AND FTLeaveEndTime>='" & _EndTime & "' ").Length <= 0 Then
                                            _FNWorkMinute = 60
                                            _FNWorkMinuteHR = 60
                                        Else
                                            _FNWorkMinute = 0
                                            _FNWorkMinuteHR = 0
                                        End If

                                    Case (_StartTime < _FTIn2 And _FTOut2 >= _EndTime And _FTIn2 <> "") And (_FTIn2 <> "" And _FTOut2 <> "")

                                        Try
                                            ' _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn2), CDate(_CalDate & " " & _EndTime))

                                            If _dttimeLeave.Select("FTLeaveStartTime<='" & _StartTime & "' AND FTLeaveEndTime>='" & _EndTime & "' ").Length <= 0 Then


                                                If _LeaveStartTime <> "" OrElse _LeaveEndTime <> "" Then
                                                    Select Case True
                                                        Case (_LeaveStartTime <> "" And _LeaveEndTime <> "")

                                                            _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn2), CDate(_CalDate & " " & _LeaveStartTime))
                                                            _FNWorkMinute = _FNWorkMinute + DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _LeaveEndTime), CDate(_CalDate & " " & _EndTime))
                                                        Case (_LeaveStartTime <> "" And _LeaveEndTime = "")
                                                            _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn2), CDate(_CalDate & " " & _LeaveStartTime))
                                                        Case (_LeaveStartTime = "" And _LeaveEndTime <> "")
                                                            _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _LeaveEndTime), CDate(_CalDate & " " & _EndTime))
                                                    End Select
                                                    If _FNWorkMinute < 0 Then
                                                        _FNWorkMinute = 0
                                                    End If
                                                    _FNWorkMinuteHR = _FNWorkMinute

                                                Else
                                                    _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn2), CDate(_CalDate & " " & _EndTime))
                                                    _FNWorkMinuteHR = 60

                                                End If

                                            Else
                                                _FNWorkMinute = 0
                                                _FNWorkMinuteHR = 0
                                            End If

                                        Catch ex As Exception
                                        End Try

                                    Case (_StartTime < _FTIn2 And _FTOut2 < _EndTime And _FTIn2 <> "" And _FTOut2 <> "" And _FTIn2 < _FTOut2) And (_FTIn2 <> "" And _FTOut2 <> "")

                                        Try
                                            ' _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn2), CDate(_CalDate & " " & _FTOut2))

                                            If _dttimeLeave.Select("FTLeaveStartTime<='" & _StartTime & "' AND FTLeaveEndTime>='" & _EndTime & "' ").Length <= 0 Then


                                                If _LeaveStartTime <> "" OrElse _LeaveEndTime <> "" Then
                                                    Select Case True
                                                        Case (_LeaveStartTime <> "" And _LeaveEndTime <> "")

                                                            _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn2), CDate(_CalDate & " " & _LeaveStartTime))
                                                            _FNWorkMinute = _FNWorkMinute + DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _LeaveEndTime), CDate(_CalDate & " " & _FTOut2))
                                                        Case (_LeaveStartTime <> "" And _LeaveEndTime = "")
                                                            _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn2), CDate(_CalDate & " " & _LeaveStartTime))
                                                        Case (_LeaveStartTime = "" And _LeaveEndTime <> "")
                                                            _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _LeaveEndTime), CDate(_CalDate & " " & _FTOut2))
                                                    End Select
                                                    If _FNWorkMinute < 0 Then
                                                        _FNWorkMinute = 0
                                                    End If
                                                    _FNWorkMinuteHR = _FNWorkMinute

                                                Else
                                                    _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn2), CDate(_CalDate & " " & _FTOut2))
                                                    _FNWorkMinuteHR = 60

                                                End If

                                            Else
                                                _FNWorkMinute = 0
                                                _FNWorkMinuteHR = 0
                                            End If

                                        Catch ex As Exception
                                        End Try

                                    Case (_StartTime >= _FTIn2 And _FTOut2 < _EndTime And _FTIn2 <> "") And (_FTIn2 <> "" And _FTOut2 <> "")

                                        Try
                                            '_FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _FTOut2))
                                            If _dttimeLeave.Select("FTLeaveStartTime<='" & _StartTime & "' AND FTLeaveEndTime>='" & _EndTime & "' ").Length <= 0 Then


                                                If _LeaveStartTime <> "" OrElse _LeaveEndTime <> "" Then
                                                    Select Case True
                                                        Case (_LeaveStartTime <> "" And _LeaveEndTime <> "")

                                                            _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _LeaveStartTime))
                                                            _FNWorkMinute = _FNWorkMinute + DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _LeaveEndTime), CDate(_CalDate & " " & _FTOut2))
                                                        Case (_LeaveStartTime <> "" And _LeaveEndTime = "")
                                                            _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _LeaveStartTime))
                                                        Case (_LeaveStartTime = "" And _LeaveEndTime <> "")
                                                            _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _LeaveEndTime), CDate(_CalDate & " " & _FTOut2))
                                                    End Select

                                                    If _FNWorkMinute < 0 Then
                                                        _FNWorkMinute = 0
                                                    End If
                                                    _FNWorkMinuteHR = _FNWorkMinute

                                                Else
                                                    _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _FTOut2))
                                                    _FNWorkMinuteHR = 60

                                                End If

                                            Else
                                                _FNWorkMinute = 0
                                                _FNWorkMinuteHR = 0
                                            End If
                                        Catch ex As Exception
                                        End Try

                                End Select

                            End If

                        Case 9, 10, 11, 12, 13

                            If _FNHSysEmpID = 1099001858 Then
                                Dim X As String
                                X = "99999"
                            End If

                            If _FNOTRequestMin > 0 Then

                                If _dtmove.Select("FNHSysEmpID=" & _FNHSysEmpID & " AND FTStartTime<='" & _FTIn3 & "' AND FTEndTime>='" & _FTOut3 & "'").Length > 0 Then
                                    _FNWorkMinute = 0
                                    _FNWorkMinuteHR = 0
                                Else
                                    If _FTState = "1" Then

                                        Select Case True

                                            Case (((_FTIn3 <= _StartTime And _FTOut3 >= _EndTime) And (_FTStartTime <= _StartTime And _FTEndTime >= _EndTime)) Or ((_FTIn3 = TimeCheckOut2 And _FNHour = 9)))

                                                ' _FNWorkMinute = 60
                                                If _FTIn3 = TimeCheckOut2 Then
                                                    _FNWorkMinute = 60
                                                    _FNWorkMinuteHR = 60
                                                Else
                                                    If _dttimeLeave.Select("FTLeaveStartTime<='" & _StartTime & "' AND FTLeaveEndTime>='" & _EndTime & "' ").Length <= 0 And _dtmove.Select("FNHSysEmpID=" & _FNHSysEmpID & " AND FTStartTime<='" & _StartTime & "' AND FTEndTime>='" & _FTOut3 & "'").Length <= 0 Then
                                                        _FNWorkMinute = 60
                                                        _FNWorkMinuteHR = 60
                                                    Else
                                                        _FNWorkMinute = 0
                                                        _FNWorkMinuteHR = 0
                                                    End If
                                                End If


                                            Case (_StartTime < _FTIn3 And _FTOut3 >= _EndTime And _FTIn3 <> "") And (_FTStartTime <= _StartTime And _FTEndTime >= _EndTime)

                                                Try
                                                    ' _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn3), CDate(_CalDate & " " & _EndTime))
                                                    If _dttimeLeave.Select("FTLeaveStartTime<='" & _StartTime & "' AND FTLeaveEndTime>='" & _EndTime & "' ").Length <= 0 And _dtmove.Select("FNHSysEmpID=" & _FNHSysEmpID & " AND FTStartTime<='" & _StartTime & "' AND FTEndTime>='" & _FTOut3 & "'").Length <= 0 Then


                                                        If _LeaveStartTime <> "" OrElse _LeaveEndTime <> "" Then
                                                            Select Case True
                                                                Case (_LeaveStartTime <> "" And _LeaveEndTime <> "")

                                                                    _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn3), CDate(_CalDate & " " & _LeaveStartTime))
                                                                    _FNWorkMinute = _FNWorkMinute + DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _LeaveEndTime), CDate(_CalDate & " " & _EndTime))
                                                                Case (_LeaveStartTime <> "" And _LeaveEndTime = "")
                                                                    _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn3), CDate(_CalDate & " " & _LeaveStartTime))
                                                                Case (_LeaveStartTime = "" And _LeaveEndTime <> "")
                                                                    _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _LeaveEndTime), CDate(_CalDate & " " & _EndTime))
                                                            End Select
                                                            If _FNWorkMinute < 0 Then
                                                                _FNWorkMinute = 0
                                                            End If
                                                            _FNWorkMinuteHR = _FNWorkMinute

                                                        Else
                                                            _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn3), CDate(_CalDate & " " & _EndTime))
                                                            _FNWorkMinuteHR = 60

                                                        End If

                                                    Else
                                                        _FNWorkMinute = 0
                                                        _FNWorkMinuteHR = 0
                                                    End If
                                                Catch ex As Exception
                                                End Try

                                            Case (_StartTime < _FTIn3 And _FTOut3 < _EndTime And _FTIn3 <> "" And _FTOut3 <> "" And _FTIn3 < _FTOut2) And (_FTStartTime <= _StartTime And _FTEndTime >= _EndTime)

                                                Try
                                                    '_FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn3), CDate(_CalDate & " " & _FTOut3))

                                                    If _dttimeLeave.Select("FTLeaveStartTime<='" & _StartTime & "' AND FTLeaveEndTime>='" & _EndTime & "' ").Length <= 0 And _dtmove.Select("FNHSysEmpID=" & _FNHSysEmpID & " AND FTStartTime<='" & _StartTime & "' AND FTEndTime>='" & _FTOut3 & "'").Length <= 0 Then


                                                        If _LeaveStartTime <> "" OrElse _LeaveEndTime <> "" Then
                                                            Select Case True
                                                                Case (_LeaveStartTime <> "" And _LeaveEndTime <> "")

                                                                    _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn3), CDate(_CalDate & " " & _LeaveStartTime))
                                                                    _FNWorkMinute = _FNWorkMinute + DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _LeaveEndTime), CDate(_CalDate & " " & _FTOut3))
                                                                Case (_LeaveStartTime <> "" And _LeaveEndTime = "")
                                                                    _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn3), CDate(_CalDate & " " & _LeaveStartTime))
                                                                Case (_LeaveStartTime = "" And _LeaveEndTime <> "")
                                                                    _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _LeaveEndTime), CDate(_CalDate & " " & _FTOut3))
                                                            End Select
                                                            If _FNWorkMinute < 0 Then
                                                                _FNWorkMinute = 0
                                                            End If
                                                            _FNWorkMinuteHR = _FNWorkMinute

                                                        Else
                                                            _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn3), CDate(_CalDate & " " & _FTOut3))
                                                            _FNWorkMinuteHR = 60

                                                        End If

                                                    Else
                                                        _FNWorkMinute = 0
                                                        _FNWorkMinuteHR = 0
                                                    End If

                                                Catch ex As Exception
                                                End Try

                                            Case (_StartTime >= _FTIn3 And _FTOut3 < _EndTime And _FTIn3 <> "" And _FTOut3 <> "" And _FTIn3 > _FTOut2 And _StartTime <= _FTOut3) And (_FTStartTime <= _StartTime And _FTEndTime >= _EndTime)

                                                Try
                                                    '_FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _FTOut3))
                                                    If _dttimeLeave.Select("FTLeaveStartTime<='" & _StartTime & "' AND FTLeaveEndTime>='" & _EndTime & "' ").Length <= 0 And _dtmove.Select("FNHSysEmpID=" & _FNHSysEmpID & " AND FTStartTime<='" & _StartTime & "' AND FTEndTime>='" & _FTOut3 & "'").Length <= 0 Then


                                                        If _LeaveStartTime <> "" OrElse _LeaveEndTime <> "" Then
                                                            Select Case True
                                                                Case (_LeaveStartTime <> "" And _LeaveEndTime <> "")

                                                                    _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _LeaveStartTime))
                                                                    _FNWorkMinute = _FNWorkMinute + DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _LeaveEndTime), CDate(_CalDate & " " & _FTOut3))
                                                                Case (_LeaveStartTime <> "" And _LeaveEndTime = "")
                                                                    _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _LeaveStartTime))
                                                                Case (_LeaveStartTime = "" And _LeaveEndTime <> "")
                                                                    _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _LeaveEndTime), CDate(_CalDate & " " & _FTOut3))
                                                            End Select
                                                            If _FNWorkMinute < 0 Then
                                                                _FNWorkMinute = 0
                                                            End If
                                                            _FNWorkMinuteHR = _FNWorkMinute

                                                        Else
                                                            _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _FTOut3))
                                                            _FNWorkMinuteHR = 60

                                                        End If

                                                    Else
                                                        _FNWorkMinute = 0
                                                        _FNWorkMinuteHR = 0
                                                    End If
                                                Catch ex As Exception
                                                End Try

                                        End Select

                                    Else

                                        Select Case True

                                            Case ((_FTIn3 <= _StartTime And _FTOut3 >= _EndTime) Or (_FTIn3 = TimeCheckOut2 And _FNHour = 9))


                                                If _FTIn3 = TimeCheckOut2 Then
                                                    _FNWorkMinute = 60
                                                    _FNWorkMinuteHR = 60
                                                Else
                                                    ' _FNWorkMinute = 60
                                                    If _dttimeLeave.Select("FTLeaveStartTime<='" & _StartTime & "' AND FTLeaveEndTime>='" & _EndTime & "' ").Length <= 0 And _dtmove.Select("FNHSysEmpID=" & _FNHSysEmpID & " AND FTStartTime<='" & _StartTime & "' AND FTEndTime>='" & _FTOut3 & "'").Length <= 0 Then
                                                        _FNWorkMinute = 60
                                                        _FNWorkMinuteHR = 60
                                                    Else
                                                        _FNWorkMinute = 0
                                                        _FNWorkMinuteHR = 0
                                                    End If
                                                End If


                                            Case (_StartTime < _FTIn3 And _FTOut3 >= _EndTime And _FTIn3 <> "")

                                                Try
                                                    '_FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn3), CDate(_CalDate & " " & _EndTime))
                                                    If _dttimeLeave.Select("FTLeaveStartTime<='" & _StartTime & "' AND FTLeaveEndTime>='" & _EndTime & "' ").Length <= 0 And _dtmove.Select("FNHSysEmpID=" & _FNHSysEmpID & " AND FTStartTime<='" & _StartTime & "' AND FTEndTime>='" & _FTOut3 & "'").Length <= 0 Then


                                                        If _LeaveStartTime <> "" OrElse _LeaveEndTime <> "" Then
                                                            Select Case True
                                                                Case (_LeaveStartTime <> "" And _LeaveEndTime <> "")

                                                                    _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn3), CDate(_CalDate & " " & _LeaveStartTime))
                                                                    _FNWorkMinute = _FNWorkMinute + DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _LeaveEndTime), CDate(_CalDate & " " & _EndTime))
                                                                Case (_LeaveStartTime <> "" And _LeaveEndTime = "")
                                                                    _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn3), CDate(_CalDate & " " & _LeaveStartTime))
                                                                Case (_LeaveStartTime = "" And _LeaveEndTime <> "")
                                                                    _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _LeaveEndTime), CDate(_CalDate & " " & _EndTime))
                                                            End Select
                                                            If _FNWorkMinute < 0 Then
                                                                _FNWorkMinute = 0
                                                            End If
                                                            _FNWorkMinuteHR = _FNWorkMinute

                                                        Else
                                                            _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn3), CDate(_CalDate & " " & _EndTime))
                                                            _FNWorkMinuteHR = 60

                                                        End If

                                                    Else
                                                        _FNWorkMinute = 0
                                                        _FNWorkMinuteHR = 0
                                                    End If
                                                Catch ex As Exception
                                                End Try

                                            Case (_StartTime < _FTIn3 And _FTOut3 < _EndTime And _FTIn3 <> "" And _FTOut3 <> "" And _FTIn3 < _FTOut2)

                                                Try
                                                    '_FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn3), CDate(_CalDate & " " & _FTOut3))
                                                    If _dttimeLeave.Select("FTLeaveStartTime<='" & _StartTime & "' AND FTLeaveEndTime>='" & _EndTime & "' ").Length <= 0 And _dtmove.Select("FNHSysEmpID=" & _FNHSysEmpID & " AND FTStartTime<='" & _StartTime & "' AND FTEndTime>='" & _FTOut3 & "'").Length <= 0 Then



                                                        If _LeaveStartTime <> "" OrElse _LeaveEndTime <> "" Then
                                                            Select Case True
                                                                Case (_LeaveStartTime <> "" And _LeaveEndTime <> "")

                                                                    _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn3), CDate(_CalDate & " " & _LeaveStartTime))
                                                                    _FNWorkMinute = _FNWorkMinute + DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _LeaveEndTime), CDate(_CalDate & " " & _FTOut3))
                                                                Case (_LeaveStartTime <> "" And _LeaveEndTime = "")
                                                                    _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn3), CDate(_CalDate & " " & _LeaveStartTime))
                                                                Case (_LeaveStartTime = "" And _LeaveEndTime <> "")
                                                                    _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _LeaveEndTime), CDate(_CalDate & " " & _FTOut3))
                                                            End Select
                                                            If _FNWorkMinute < 0 Then
                                                                _FNWorkMinute = 0
                                                            End If
                                                            _FNWorkMinuteHR = _FNWorkMinute

                                                        Else
                                                            _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn3), CDate(_CalDate & " " & _FTOut3))
                                                            _FNWorkMinuteHR = 60

                                                        End If

                                                    Else

                                                        _FNWorkMinute = 0
                                                        _FNWorkMinuteHR = 0

                                                    End If

                                                Catch ex As Exception
                                                End Try

                                            Case (_StartTime >= _FTIn3 And _FTOut3 < _EndTime And _FTIn3 <> "" And _FTOut3 <> "" And _FTIn3 > _FTOut2 And _StartTime < _FTOut3)

                                                Try
                                                    '_FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _FTOut3))

                                                    If _dttimeLeave.Select("FTLeaveStartTime<='" & _StartTime & "' AND FTLeaveEndTime>='" & _EndTime & "' ").Length <= 0 And _dtmove.Select("FNHSysEmpID=" & _FNHSysEmpID & " AND FTStartTime<='" & _StartTime & "' AND FTEndTime>='" & _FTOut3 & "'").Length <= 0 Then



                                                        If _LeaveStartTime <> "" OrElse _LeaveEndTime <> "" Then
                                                            Select Case True
                                                                Case (_LeaveStartTime <> "" And _LeaveEndTime <> "")

                                                                    _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _LeaveStartTime))
                                                                    _FNWorkMinute = _FNWorkMinute + DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _LeaveEndTime), CDate(_CalDate & " " & _FTOut3))
                                                                Case (_LeaveStartTime <> "" And _LeaveEndTime = "")
                                                                    _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _LeaveStartTime))
                                                                Case (_LeaveStartTime = "" And _LeaveEndTime <> "")
                                                                    _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _LeaveEndTime), CDate(_CalDate & " " & _FTOut3))
                                                            End Select
                                                            If _FNWorkMinute < 0 Then
                                                                _FNWorkMinute = 0
                                                            End If
                                                            _FNWorkMinuteHR = _FNWorkMinute

                                                        Else
                                                            _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _FTOut3))
                                                            _FNWorkMinuteHR = 60

                                                        End If

                                                    Else

                                                        _FNWorkMinute = 0
                                                        _FNWorkMinuteHR = 0

                                                    End If

                                                Catch ex As Exception
                                                End Try

                                        End Select

                                    End If
                                End If

                            End If

                    End Select
                Else
                    _FNWorkMinute = 0
                    _FNWorkMinuteHR = 0
                End If

                If _FNWorkMinute < 0 Then _FNWorkMinute = 0

                If _dtemptime.Select("FNHSysEmpID=" & _FNHSysEmpID & "").Length <= 0 Then

                    _dtemptime.Rows.Add(_FTEmpCode, _FNHSysEmpID, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                                             _FNLateNormalMin, _FNLateNormalCut, _FNRetireNormalMin,
                                             _FNRetireNormalCut, _FNLateOtMin, _FNLateOtCut, _FNRetireOtMin,
                                             _FNRetireOtCut, _FNAbsentCut, _FNAbsent, _FNTimeMin, _FNOT1Min,
                                             _FNOT1_5Min, _FNOT2Min, _FNOT3Min, _FNOT4Min, _FNLateMMin, _FNLateAfMin,
                                             _FNRetireMMin, _FNRetireAfMin, _FNOTRequestMin, _FNCutAbsent, _FNLateNormalNotCut,
                                             _FTStartTime, _FTEndTime, _FNTotalMinute, _FTState, _FNSalary, 0, 0, "0", 0, _FTStateDaily, _FTStatePlagnent, _FTStateRelease, _FNAmtFixedIncentive, _FTStateTrain, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)

                End If

                For Each Rxm As DataRow In _dtemptime.Select("FNHSysEmpID=" & _FNHSysEmpID & "")

                    Select Case _FNHour
                        Case 0
                            Rxm!FNH01 = _FNWorkMinute
                            Rxm!FNHRH01 = _FNWorkMinuteHR
                        Case 1
                            Rxm!FNH01 = _FNWorkMinute
                            Rxm!FNHRH01 = _FNWorkMinuteHR
                        Case 2
                            Rxm!FNH02 = _FNWorkMinute
                            Rxm!FNHRH02 = _FNWorkMinuteHR
                        Case 3
                            Rxm!FNH03 = _FNWorkMinute
                            Rxm!FNHRH03 = _FNWorkMinuteHR
                        Case 4
                            Rxm!FNH04 = _FNWorkMinute
                            Rxm!FNHRH04 = _FNWorkMinuteHR
                        Case 5
                            Rxm!FNH05 = _FNWorkMinute
                            Rxm!FNHRH05 = _FNWorkMinuteHR
                        Case 6
                            Rxm!FNH06 = _FNWorkMinute
                            Rxm!FNHRH06 = _FNWorkMinuteHR
                        Case 7
                            Rxm!FNH07 = _FNWorkMinute
                            Rxm!FNHRH07 = _FNWorkMinuteHR
                        Case 8
                            Rxm!FNH08 = _FNWorkMinute
                            Rxm!FNHRH08 = _FNWorkMinuteHR
                        Case 9
                            Rxm!FNH09 = _FNWorkMinute
                            Rxm!FNHRH09 = _FNWorkMinuteHR
                        Case 10

                            Rxm!FNH10 = _FNWorkMinute
                            Rxm!FNHRH10 = _FNWorkMinuteHR
                        Case 11
                            Rxm!FNH11 = _FNWorkMinute
                            Rxm!FNHRH11 = _FNWorkMinuteHR
                        Case 12
                            Rxm!FNH12 = _FNWorkMinute
                            Rxm!FNHRH12 = _FNWorkMinuteHR
                        Case 13
                            Rxm!FNH13 = _FNWorkMinute
                            Rxm!FNHRH13 = _FNWorkMinuteHR
                    End Select

                Next

            Next

        Next

        For Each Rxm As DataRow In _dtemptime.Rows

            Rxm!FNWorkingMin = Integer.Parse(Val(Rxm!FNH01.ToString)) + Integer.Parse(Val(Rxm!FNH02.ToString)) + Integer.Parse(Val(Rxm!FNH03.ToString)) + Integer.Parse(Val(Rxm!FNH04.ToString)) + Integer.Parse(Val(Rxm!FNH05.ToString)) + Integer.Parse(Val(Rxm!FNH06.ToString)) + Integer.Parse(Val(Rxm!FNH07.ToString)) + Integer.Parse(Val(Rxm!FNH08.ToString))
            Rxm!FNWorkingOTMin = Integer.Parse(Val(Rxm!FNH09.ToString)) + Integer.Parse(Val(Rxm!FNH10.ToString)) + Integer.Parse(Val(Rxm!FNH11.ToString)) + Integer.Parse(Val(Rxm!FNH12.ToString)) + Integer.Parse(Val(Rxm!FNH13.ToString))
            Rxm!FNTotalWorkingMin = Integer.Parse(Val(Rxm!FNWorkingMin.ToString)) + Integer.Parse(Val(Rxm!FNWorkingOTMin.ToString))

        Next

        Return _dtemptime

    End Function

#Region "Old Process"

    Private Function GetEmpTime_Old(_UnitSectID As Integer, _CalDate As String, Optional _TFNHSysEmpID As Integer = 0) As DataTable
        Dim _dt As DataTable
        Dim _dtmove As DataTable = GetEmpTimeMoveOut(_UnitSectID, _CalDate, _TFNHSysEmpID)
        Dim _dttime As DataTable
        Dim _dtemptime As New DataTable

        _dtemptime.Columns.Add("FTEmpCode", GetType(String))
        _dtemptime.Columns.Add("FNHSysEmpID", GetType(Integer))
        _dtemptime.Columns.Add("FNH01", GetType(Integer))
        _dtemptime.Columns.Add("FNH02", GetType(Integer))
        _dtemptime.Columns.Add("FNH03", GetType(Integer))
        _dtemptime.Columns.Add("FNH04", GetType(Integer))
        _dtemptime.Columns.Add("FNH05", GetType(Integer))
        _dtemptime.Columns.Add("FNH06", GetType(Integer))
        _dtemptime.Columns.Add("FNH07", GetType(Integer))
        _dtemptime.Columns.Add("FNH08", GetType(Integer))
        _dtemptime.Columns.Add("FNH09", GetType(Integer))
        _dtemptime.Columns.Add("FNH10", GetType(Integer))
        _dtemptime.Columns.Add("FNH11", GetType(Integer))
        _dtemptime.Columns.Add("FNH12", GetType(Integer))
        _dtemptime.Columns.Add("FNH13", GetType(Integer))
        _dtemptime.Columns.Add("FNLateNormalMin", GetType(Integer))
        _dtemptime.Columns.Add("FNLateNormalCut", GetType(Integer))
        _dtemptime.Columns.Add("FNRetireNormalMin", GetType(Integer))
        _dtemptime.Columns.Add("FNRetireNormalCut", GetType(Integer))
        _dtemptime.Columns.Add("FNLateOtMin", GetType(Integer))
        _dtemptime.Columns.Add("FNLateOtCut", GetType(Integer))
        _dtemptime.Columns.Add("FNRetireOtMin", GetType(Integer))
        _dtemptime.Columns.Add("FNRetireOtCut", GetType(Integer))
        _dtemptime.Columns.Add("FNAbsentCut", GetType(Integer))
        _dtemptime.Columns.Add("FNAbsent", GetType(Integer))
        _dtemptime.Columns.Add("FNTimeMin", GetType(Integer))
        _dtemptime.Columns.Add("FNOT1Min", GetType(Integer))
        _dtemptime.Columns.Add("FNOT1_5Min", GetType(Integer))
        _dtemptime.Columns.Add("FNOT2Min", GetType(Integer))
        _dtemptime.Columns.Add("FNOT3Min", GetType(Integer))
        _dtemptime.Columns.Add("FNOT4Min", GetType(Integer))
        _dtemptime.Columns.Add("FNLateMMin", GetType(Integer))
        _dtemptime.Columns.Add("FNLateAfMin", GetType(Integer))
        _dtemptime.Columns.Add("FNRetireMMin", GetType(Integer))
        _dtemptime.Columns.Add("FNRetireAfMin", GetType(Integer))
        _dtemptime.Columns.Add("FNOTRequestMin", GetType(Integer))
        _dtemptime.Columns.Add("FNCutAbsent", GetType(Integer))
        _dtemptime.Columns.Add("FNLateNormalNotCut", GetType(Integer))
        _dtemptime.Columns.Add("FTStartTime", GetType(String))
        _dtemptime.Columns.Add("FTEndTime", GetType(String))
        _dtemptime.Columns.Add("FNTotalMinute", GetType(Integer))
        _dtemptime.Columns.Add("FTState", GetType(String))
        _dtemptime.Columns.Add("FNSalary", GetType(Double))
        _dtemptime.Columns.Add("FNWorkingMin", GetType(Double))
        _dtemptime.Columns.Add("FNWorkingOTMin", GetType(Double))
        _dtemptime.Columns.Add("FTStateCal", GetType(String))
        _dtemptime.Columns.Add("FNTotalWorkingMin", GetType(Double))
        _dtemptime.Columns.Add("FTStateDaily", GetType(String))
        _dtemptime.Columns.Add("FTStatePlagnent", GetType(String))

        Dim _Qry As String = ""
        Dim _FNHSysEmpID As Integer
        Dim _FTIn1 As String
        Dim _FTOut1 As String
        Dim _FTIn2 As String
        Dim _FTOut2 As String
        Dim _FTIn3 As String
        Dim _FTOut3 As String
        Dim _FTIn4 As String
        Dim _FTOut4 As String

        Dim _FNLateNormalMin As Integer
        Dim _FNLateNormalCut As Integer
        Dim _FNRetireNormalMin As Integer
        Dim _FNRetireNormalCut As Integer
        Dim _FNLateOtMin As Integer
        Dim _FNLateOtCut As Integer
        Dim _FNRetireOtMin As Integer
        Dim _FNRetireOtCut As Integer
        Dim _FNAbsentCut As Integer
        Dim _FNAbsent As Integer
        Dim _FNTimeMin As Integer
        Dim _FNOT1Min As Integer
        Dim _FNOT1_5Min As Integer
        Dim _FNOT2Min As Integer
        Dim _FNOT3Min As Integer
        Dim _FNOT4Min As Integer
        Dim _FNLateMMin As Integer
        Dim _FNLateAfMin As Integer
        Dim _FNRetireMMin As Integer
        Dim _FNRetireAfMin As Integer
        Dim _FNOTRequestMin As Integer
        Dim _FNCutAbsent As Integer
        Dim _FNLateNormalNotCut As Integer

        Dim _FTStartTime As String
        Dim _FTEndTime As String
        Dim _FNTotalMinute As Integer
        Dim _FTState As String
        Dim _FNHour As Integer = 0
        Dim _FNWorkMinute As Integer = 0
        Dim _StartTime As String
        Dim _EndTime As String
        Dim _FNSalary As Double
        Dim _FTStateDaily As String
        Dim _FTStatePlagnent As String
        Dim _FTEmpCode As String = ""

        _Qry = "SELECT FTPeriadOfTimeCode, FTStartTime, FTEndTime,ROW_NUMBER() Over (Order By FTStartTime) AS FNHour"
        _Qry &= vbCrLf & " FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TPRODMPeiodOfTime AS A WITH(NOLOCK)"
        _Qry &= vbCrLf & " WHERE  (FTStateActive = '1')"
        _Qry &= vbCrLf & " ORDER BY FTStartTime"
        _dttime = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MASTER)

        _Qry = "EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.SP_GET_EMPTIME_CAL_INCENTIVE " & _UnitSectID & ",'" & HI.UL.ULDate.ConvertEnDB(_CalDate) & "'," & _TFNHSysEmpID & ""
        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

        For Each R As DataRow In _dt.Rows

            _FNHSysEmpID = Integer.Parse(Val(R!FNHSysEmpID.ToString))
            _FTIn1 = R!FTIn1.ToString
            _FTOut1 = R!FTOut1.ToString
            _FTIn2 = R!FTIn2.ToString
            _FTOut2 = R!FTOut2.ToString
            _FTIn3 = R!FTIn3.ToString
            _FTOut3 = R!FTOut3.ToString
            _FTIn4 = R!FTIn4.ToString
            _FTOut4 = R!FTOut4.ToString
            _FNLateNormalMin = Integer.Parse(Val(R!FNLateNormalMin.ToString))
            _FNLateNormalCut = Integer.Parse(Val(R!FNLateNormalCut.ToString))
            _FNRetireNormalMin = Integer.Parse(Val(R!FNRetireNormalMin.ToString))
            _FNRetireNormalCut = Integer.Parse(Val(R!FNRetireNormalCut.ToString))
            _FNLateOtMin = Integer.Parse(Val(R!FNLateOtMin.ToString))
            _FNLateOtCut = Integer.Parse(Val(R!FNLateOtCut.ToString))
            _FNRetireOtMin = Integer.Parse(Val(R!FNRetireOtMin.ToString))
            _FNRetireOtCut = Integer.Parse(Val(R!FNRetireOtCut.ToString))
            _FNAbsentCut = Integer.Parse(Val(R!FNAbsentCut.ToString))
            _FNAbsent = Integer.Parse(Val(R!FNAbsent.ToString))
            _FNTimeMin = Integer.Parse(Val(R!FNTimeMin.ToString))
            _FNOT1Min = Integer.Parse(Val(R!FNOT1Min.ToString))
            _FNOT1_5Min = Integer.Parse(Val(R!FNOT1_5Min.ToString))
            _FNOT2Min = Integer.Parse(Val(R!FNOT2Min.ToString))
            _FNOT3Min = Integer.Parse(Val(R!FNOT3Min.ToString))
            _FNOT4Min = Integer.Parse(Val(R!FNOT4Min.ToString))
            _FNLateMMin = Integer.Parse(Val(R!FNLateMMin.ToString))
            _FNLateAfMin = Integer.Parse(Val(R!FNLateAfMin.ToString))
            _FNRetireMMin = Integer.Parse(Val(R!FNRetireMMin.ToString))
            _FNRetireAfMin = Integer.Parse(Val(R!FNRetireAfMin.ToString))
            _FNOTRequestMin = Integer.Parse(Val(R!FNOTRequestMin.ToString))
            _FNCutAbsent = Integer.Parse(Val(R!FNCutAbsent.ToString))
            _FNLateNormalNotCut = Integer.Parse(Val(R!FNLateNormalNotCut.ToString))
            _FTStartTime = R!FTStartTime.ToString
            _FTEndTime = R!FTEndTime.ToString
            _FNTotalMinute = Integer.Parse(Val(R!FNTotalMinute.ToString))
            _FTState = R!FTState.ToString
            _FNSalary = Double.Parse(Val(R!FNSalary.ToString))
            _FTStateDaily = R!FTStateDaily.ToString
            _FTStatePlagnent = R!FTStatePlagnent.ToString
            _FTEmpCode = R!FTEmpCode.ToString

            For Each Rxt As DataRow In _dttime.Rows

                _StartTime = Rxt!FTStartTime.ToString
                _EndTime = Rxt!FTEndTime.ToString

                _FNHour = Integer.Parse(Val(Rxt!FNHour.ToString))
                _FNWorkMinute = 0

                If _dtmove.Select("FNHSysEmpID=" & _FNHSysEmpID & " AND FTStartTime<='" & _StartTime & "' AND FTEndTime>='" & _EndTime & "'").Length <= 0 Then
                    Select Case _FNHour
                        Case 1, 2, 3, 4

                            If _FTState = "1" Then

                                Select Case True
                                    Case (_FTIn1 <= _StartTime And _FTOut1 >= _EndTime) And (_FTStartTime <= _StartTime And _FTEndTime >= _EndTime)

                                        _FNWorkMinute = 60

                                    Case (_StartTime < _FTIn1 And _FTOut1 >= _EndTime And _FTIn1 <> "") And (_FTStartTime <= _StartTime And _FTEndTime >= _EndTime)

                                        Try
                                            _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn1), CDate(_CalDate & " " & _EndTime))
                                        Catch ex As Exception
                                        End Try

                                    Case (_StartTime < _FTIn1 And _FTOut1 < _EndTime And _FTIn1 <> "" And _FTOut1 <> "" And _FTIn1 < _FTOut1) And (_FTStartTime <= _StartTime And _FTEndTime >= _EndTime)

                                        Try
                                            _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn1), CDate(_CalDate & " " & _FTOut1))
                                        Catch ex As Exception
                                        End Try

                                End Select

                            Else

                                Select Case True
                                    Case (_FTIn1 <= _StartTime And _FTOut1 >= _EndTime)

                                        _FNWorkMinute = 60

                                    Case (_StartTime < _FTIn1 And _FTOut1 >= _EndTime And _FTIn1 <> "")

                                        Try
                                            _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn1), CDate(_CalDate & " " & _EndTime))
                                        Catch ex As Exception
                                        End Try

                                    Case (_StartTime < _FTIn1 And _FTOut1 < _EndTime And _FTIn1 <> "" And _FTOut1 <> "" And _FTIn1 < _FTOut1)

                                        Try
                                            _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn1), CDate(_CalDate & " " & _FTOut1))
                                        Catch ex As Exception
                                        End Try

                                End Select

                            End If

                        Case 5, 6, 7, 8

                            If _FTState = "1" Then

                                Select Case True

                                    Case (_FTIn2 <= _StartTime And _FTOut2 >= _EndTime) And (_FTStartTime <= _StartTime And _FTEndTime >= _EndTime)

                                        _FNWorkMinute = 60

                                    Case (_StartTime < _FTIn2 And _FTOut2 >= _EndTime And _FTIn2 <> "") And (_FTStartTime <= _StartTime And _FTEndTime >= _EndTime)

                                        Try
                                            _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn2), CDate(_CalDate & " " & _EndTime))
                                        Catch ex As Exception
                                        End Try

                                    Case (_StartTime < _FTIn2 And _FTOut2 < _EndTime And _FTIn2 <> "" And _FTOut2 <> "" And _FTIn2 < _FTOut2) And (_FTStartTime <= _StartTime And _FTEndTime >= _EndTime)

                                        Try
                                            _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn2), CDate(_CalDate & " " & _FTOut2))
                                        Catch ex As Exception
                                        End Try

                                End Select

                            Else

                                Select Case True

                                    Case (_FTIn2 <= _StartTime And _FTOut2 >= _EndTime)

                                        _FNWorkMinute = 60

                                    Case (_StartTime < _FTIn2 And _FTOut2 >= _EndTime And _FTIn2 <> "")

                                        Try
                                            _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn2), CDate(_CalDate & " " & _EndTime))
                                        Catch ex As Exception
                                        End Try

                                    Case (_StartTime < _FTIn2 And _FTOut2 < _EndTime And _FTIn2 <> "" And _FTOut2 <> "" And _FTIn2 < _FTOut2)

                                        Try
                                            _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn2), CDate(_CalDate & " " & _FTOut2))
                                        Catch ex As Exception
                                        End Try

                                End Select

                            End If

                        Case 9, 10, 11, 12, 13

                            If _FNOTRequestMin > 0 Then
                                If _FTState = "1" Then


                                    Select Case True

                                        Case ((_FTIn3 <= _StartTime And _FTOut3 >= _EndTime) And (_FTStartTime <= _StartTime And _FTEndTime >= _EndTime))

                                            _FNWorkMinute = 60

                                        Case (_StartTime < _FTIn3 And _FTOut3 >= _EndTime And _FTIn3 <> "") And (_FTStartTime <= _StartTime And _FTEndTime >= _EndTime)

                                            Try
                                                _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn3), CDate(_CalDate & " " & _EndTime))
                                            Catch ex As Exception
                                            End Try

                                        Case (_StartTime < _FTIn3 And _FTOut3 < _EndTime And _FTIn3 <> "" And _FTOut3 <> "" And _FTIn3 < _FTOut2) And (_FTStartTime <= _StartTime And _FTEndTime >= _EndTime)

                                            Try
                                                _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn3), CDate(_CalDate & " " & _FTOut3))
                                            Catch ex As Exception
                                            End Try
                                        Case (_StartTime > _FTIn3 And _FTOut3 < _EndTime And _FTIn3 <> "" And _FTOut3 <> "" And _FTIn3 > _FTOut2 And _StartTime < _FTOut3)

                                            Try
                                                _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _FTOut3))
                                            Catch ex As Exception
                                            End Try
                                    End Select

                                Else

                                    Select Case True

                                        Case (_FTIn3 <= _StartTime And _FTOut3 >= _EndTime)

                                            _FNWorkMinute = 60

                                        Case (_StartTime < _FTIn3 And _FTOut3 >= _EndTime And _FTIn3 <> "")

                                            Try
                                                _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn3), CDate(_CalDate & " " & _EndTime))
                                            Catch ex As Exception
                                            End Try

                                        Case (_StartTime < _FTIn3 And _FTOut3 < _EndTime And _FTIn3 <> "" And _FTOut3 <> "" And _FTIn3 < _FTOut2)

                                            Try
                                                _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn3), CDate(_CalDate & " " & _FTOut3))
                                            Catch ex As Exception
                                            End Try
                                        Case (_StartTime > _FTIn3 And _FTOut3 < _EndTime And _FTIn3 <> "" And _FTOut3 <> "" And _FTIn3 > _FTOut2 And _StartTime < _FTOut3)

                                            Try
                                                _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _FTOut3))
                                            Catch ex As Exception
                                            End Try
                                    End Select

                                End If
                            End If

                    End Select
                Else
                    _FNWorkMinute = 0
                End If

                If _dtemptime.Select("FNHSysEmpID=" & _FNHSysEmpID & "").Length <= 0 Then
                    _dtemptime.Rows.Add(_FTEmpCode, _FNHSysEmpID, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                                             _FNLateNormalMin, _FNLateNormalCut, _FNRetireNormalMin,
                                             _FNRetireNormalCut, _FNLateOtMin, _FNLateOtCut, _FNRetireOtMin,
                                             _FNRetireOtCut, _FNAbsentCut, _FNAbsent, _FNTimeMin, _FNOT1Min,
                                             _FNOT1_5Min, _FNOT2Min, _FNOT3Min, _FNOT4Min, _FNLateMMin, _FNLateAfMin,
                                             _FNRetireMMin, _FNRetireAfMin, _FNOTRequestMin, _FNCutAbsent, _FNLateNormalNotCut,
                                             _FTStartTime, _FTEndTime, _FNTotalMinute, _FTState, _FNSalary, 0, 0, "0", 0, _FTStateDaily, _FTStatePlagnent)
                End If

                For Each Rxm As DataRow In _dtemptime.Select("FNHSysEmpID=" & _FNHSysEmpID & "")

                    Select Case _FNHour
                        Case 0
                            Rxm!FNH01 = _FNWorkMinute
                        Case 1
                            Rxm!FNH01 = _FNWorkMinute
                        Case 2
                            Rxm!FNH02 = _FNWorkMinute
                        Case 3
                            Rxm!FNH03 = _FNWorkMinute
                        Case 4
                            Rxm!FNH04 = _FNWorkMinute
                        Case 5
                            Rxm!FNH05 = _FNWorkMinute
                        Case 6
                            Rxm!FNH06 = _FNWorkMinute
                        Case 7
                            Rxm!FNH07 = _FNWorkMinute
                        Case 8
                            Rxm!FNH08 = _FNWorkMinute
                        Case 9
                            Rxm!FNH09 = _FNWorkMinute
                        Case 10
                            Rxm!FNH10 = _FNWorkMinute
                        Case 11
                            Rxm!FNH11 = _FNWorkMinute
                        Case 12
                            Rxm!FNH12 = _FNWorkMinute
                        Case 13
                            Rxm!FNH13 = _FNWorkMinute
                    End Select

                Next

            Next

        Next

        For Each Rxm As DataRow In _dtemptime.Rows

            Rxm!FNWorkingMin = Integer.Parse(Val(Rxm!FNH01.ToString)) + Integer.Parse(Val(Rxm!FNH02.ToString)) + Integer.Parse(Val(Rxm!FNH03.ToString)) + Integer.Parse(Val(Rxm!FNH04.ToString)) + Integer.Parse(Val(Rxm!FNH05.ToString)) + Integer.Parse(Val(Rxm!FNH06.ToString)) + Integer.Parse(Val(Rxm!FNH07.ToString)) + Integer.Parse(Val(Rxm!FNH08.ToString))
            Rxm!FNWorkingOTMin = Integer.Parse(Val(Rxm!FNH09.ToString)) + Integer.Parse(Val(Rxm!FNH10.ToString)) + Integer.Parse(Val(Rxm!FNH11.ToString)) + Integer.Parse(Val(Rxm!FNH12.ToString)) + Integer.Parse(Val(Rxm!FNH13.ToString))
            Rxm!FNTotalWorkingMin = Integer.Parse(Val(Rxm!FNWorkingMin.ToString)) + Integer.Parse(Val(Rxm!FNWorkingOTMin.ToString))

        Next

        Return _dtemptime

    End Function

    Private Function SaveData_Old(Spls As HI.TL.SplashScreen) As Boolean

        Dim _dt As DataTable
        Dim _dtemptime As DataTable
        Dim _dtsewstyle As DataTable
        Dim _dtqa As DataTable
        Dim _Qry As String = ""
        Dim _LineAmt As Double = 0
        Dim _LineNetAmt As Double = 0
        Dim _CountStyle As Integer = 0
        Dim _CountEmp As Integer = 0
        Dim _CountEmpIncentive As Integer = 0
        Dim _TotalTime As Integer = 0
        Dim _TotalTimeOT As Integer = 0
        Dim _TotalTimeHR As Integer = 0
        Dim _TotalTimeOTHR As Integer = 0
        Dim _Salary As Double = 0
        Dim _SalaryPerH As Double = 0
        Dim _SalaryPerOT As Double = 0
        Dim _ProdAmt As Double = 0
        Dim _ProdOTAmt As Double = 0
        Dim _WageAmt As Double = 0
        Dim _WageOTAmt As Double = 0
        Dim _SumSam As Double = 0
        Dim _SumPrice As Double = 0
        Dim _FNQAPer As Double = 0
        Dim _FNQAValue As Double = 0
        Dim _FNAmtNormal As Double
        Dim _FNAmtOT As Double
        Dim _FNNetAmt As Double
        Dim _FoundFixInsurance As Boolean = False
        Dim _ScanQty As Integer = 0
        Dim _IncentiveQty As Integer = 0
        Dim _IncentiveNormalQty As Integer = 0
        Dim _IncentiveOTQty As Integer = 0
        Dim _LineIncentiveAmt As Double = 0
        Dim _EmpIncentiveAmt As Double = 0

        Dim _H01Qty As Integer = 0
        Dim _H02Qty As Integer = 0
        Dim _H03Qty As Integer = 0
        Dim _H04Qty As Integer = 0
        Dim _H05Qty As Integer = 0
        Dim _H06Qty As Integer = 0
        Dim _H07Qty As Integer = 0
        Dim _H08Qty As Integer = 0
        Dim _H09Qty As Integer = 0
        Dim _H10Qty As Integer = 0
        Dim _H11Qty As Integer = 0
        Dim _H12Qty As Integer = 0
        Dim _H13Qty As Integer = 0

        Dim _FTStateInsuranceInH1 As String
        Dim _FTStateInsuranceInH2 As String
        Dim _FTStateInsuranceInH3 As String
        Dim _FTStateInsuranceInH4 As String
        Dim _FTStateInsuranceInH5 As String
        Dim _FTStateInsuranceInH6 As String
        Dim _FTStateInsuranceInH7 As String
        Dim _FTStateInsuranceInH8 As String
        Dim _FTStateInsuranceInH9 As String
        Dim _FTStateInsuranceInH10 As String
        Dim _FTStateInsuranceInH11 As String
        Dim _FTStateInsuranceInH12 As String
        Dim _FTStateInsuranceInH13 As String
        Dim _FTStateInsuranceInDay As String
        Dim _CalDate As String
        Dim _FixHour As Integer = 0
        Dim _DeductAmt As Double = 0
        Dim _FNTotalWorkingMin As Integer
        Dim _FNTotalOTWorkingMin As Integer
        Dim _EmpCountCalincentive As Integer = 0

        With CType(ogc.DataSource, DataTable)
            .AcceptChanges()
            _dt = .Copy()
        End With

        Try
            For Each R As DataRow In _dt.Select("FTSelect='1'", "FDScanDate,FTUnitSectCode")

                _EmpCountCalincentive = 0
                _CalDate = HI.UL.ULDate.ConvertEnDB(R!FDScanDateOrg.ToString)

                _FoundFixInsurance = False

                _FTStateInsuranceInH1 = R!FTStateInsuranceInH1.ToString
                _FTStateInsuranceInH2 = R!FTStateInsuranceInH2.ToString
                _FTStateInsuranceInH3 = R!FTStateInsuranceInH3.ToString
                _FTStateInsuranceInH4 = R!FTStateInsuranceInH4.ToString
                _FTStateInsuranceInH5 = R!FTStateInsuranceInH5.ToString
                _FTStateInsuranceInH6 = R!FTStateInsuranceInH6.ToString
                _FTStateInsuranceInH7 = R!FTStateInsuranceInH7.ToString
                _FTStateInsuranceInH8 = R!FTStateInsuranceInH8.ToString
                _FTStateInsuranceInH9 = R!FTStateInsuranceInH9.ToString
                _FTStateInsuranceInH10 = R!FTStateInsuranceInH10.ToString
                _FTStateInsuranceInH11 = R!FTStateInsuranceInH11.ToString
                _FTStateInsuranceInH12 = R!FTStateInsuranceInH12.ToString
                _FTStateInsuranceInH13 = R!FTStateInsuranceInH13.ToString
                _FTStateInsuranceInDay = R!FTStateInsuranceInDay.ToString

                _Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTIncentive "
                _Qry &= vbCrLf & " WHERE  (FTCalDate = '" & HI.UL.ULDate.ConvertEnDB(R!FDScanDateOrg.ToString) & "') "
                _Qry &= vbCrLf & "   AND (FNHSysUnitSectId = " & Integer.Parse(Val(R!FNHSysUnitSectId.ToString)) & ")"
                HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

                _Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTIncentive_Emp "
                _Qry &= vbCrLf & " WHERE  (FTCalDate = '" & HI.UL.ULDate.ConvertEnDB(R!FDScanDateOrg.ToString) & "') "
                _Qry &= vbCrLf & "   AND (FNHSysUnitSectId = " & Integer.Parse(Val(R!FNHSysUnitSectId.ToString)) & ")"
                HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

                _Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTIncentive_Level "
                _Qry &= vbCrLf & " WHERE  (FTCalDate = '" & HI.UL.ULDate.ConvertEnDB(R!FDScanDateOrg.ToString) & "') "
                _Qry &= vbCrLf & "   AND (FNHSysUnitSectId = " & Integer.Parse(Val(R!FNHSysUnitSectId.ToString)) & ")"
                HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

                _Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTIncentive_Style "
                _Qry &= vbCrLf & " WHERE  (FTCalDate = '" & HI.UL.ULDate.ConvertEnDB(R!FDScanDateOrg.ToString) & "') "
                _Qry &= vbCrLf & "   AND (FNHSysUnitSectId = " & Integer.Parse(Val(R!FNHSysUnitSectId.ToString)) & ")"
                HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

                _Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTIncentive_Emp_Deduct "
                _Qry &= vbCrLf & " WHERE  (FTCalDate = '" & HI.UL.ULDate.ConvertEnDB(R!FDScanDateOrg.ToString) & "') "
                _Qry &= vbCrLf & "   AND (FNHSysUnitSectId = " & Integer.Parse(Val(R!FNHSysUnitSectId.ToString)) & ")"
                HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

                _dtemptime = GetEmpTime(Integer.Parse(Val(R!FNHSysUnitSectId.ToString)), R!FDScanDateOrg.ToString).Copy()

                _Qry = " SELECT M1.FNHSysStyleId,M1.FTOrderNo,M1.FTSubOrderNo,ISNULL(XXA.FNTotalQty,M1.FNScanQuantity) As FNScanQuantity,ISNULL(P.FNSam,0) AS FNSam,ISNULL(P.FNPrice,0) AS FNPrice,ISNULL(P.FNMultiple,0) AS FNMultiple"
                _Qry &= vbCrLf & ",Convert(numeric(18,4),ISNULL(P.FNPrice,0) ) AS FNNetPrice	"
                _Qry &= vbCrLf & ",Convert(numeric(18,2),Convert(numeric(18,4),ISNULL(P.FNPrice,0) ) * ISNULL(XXA.FNTotalQty,M1.FNScanQuantity))  AS FNNetAmt	"
                _Qry &= vbCrLf & " FROM ( "


                _Qry &= vbCrLf & " SELECT  P.FTOrderNo ,P.FTSubOrderNo, A.FNHSysStyleId,Sum(FNScanQuantity) AS FNScanQuantity"
                _Qry &= vbCrLf & "   FROM"
                _Qry &= vbCrLf & "	("
                _Qry &= vbCrLf & " SELECT    S.FTInsUser, S.FDInsDate, S.FTInsTime, S.FTUpdUser, S.FDUpdDate, S.FTUpdTime, S.FTPackNo, S.FNCartonNo, S.FTOrderNo, S.FTSubOrderNo, S.FTColorway, S.FTSizeBreakDown, "
                _Qry &= vbCrLf & "   S.FNHSysUnitSectId, S.FTBarcodeNo, S.FDScanDate, S.FDScanTime, S.FNScanQuantity"
                _Qry &= vbCrLf & "FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan_Detail AS S WITH(NOLOCK)  LEFT OUTER JOIN"
                _Qry &= vbCrLf & "	     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScanOutline AS O WITH(NOLOCK)  ON S.FTBarcodeNo = O.FTBarcodeNo"
                _Qry &= vbCrLf & " WHERE S.FNHSysUnitSectId  =" & Integer.Parse(Val(R!FNHSysUnitSectId.ToString)) & " "
                _Qry &= vbCrLf & "       AND S.FDScanDate='" & _CalDate & "'   "
                _Qry &= vbCrLf & "       AND O.FTBarcodeNo Is NULL"
                _Qry &= vbCrLf & "UNION"
                _Qry &= vbCrLf & "SELECT     O.FTInsUser, O.FDInsDate, O.FTInsTime, O.FTUpdUser, O.FDUpdDate, O.FTUpdTime , '' AS FTPackNo,0 , PP.FTOrderNo ,'', B.FTColorway, B.FTSizeBreakDown"
                _Qry &= vbCrLf & ", O.FNHSysUnitSectId, O.FTBarcodeNo, O.FDDate, O.FTTime, O.FNQuantity"
                _Qry &= vbCrLf & "FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScanOutline AS O WITH(NOLOCK)  LEFT OUTER JOIN"
                _Qry &= vbCrLf & "	      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle AS B WITH(NOLOCK)  ON O.FTBarcodeNo = B.FTBarcodeBundleNo "
                _Qry &= vbCrLf & "	      INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd AS PP WITH(NOLOCK) ON B.FTOrderProdNo = PP.FTOrderProdNo "
                _Qry &= vbCrLf & " WHERE O.FNHSysUnitSectId  =" & Integer.Parse(Val(R!FNHSysUnitSectId.ToString)) & " "
                _Qry &= vbCrLf & " AND O.FDDate='" & _CalDate & "'   "
                _Qry &= vbCrLf & "	) AS P  RIGHT OUTER JOIN"
                _Qry &= vbCrLf & "	[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS A  WITH(NOLOCK) ON P.FTOrderNo = A.FTOrderNo  LEFT OUTER JOIN"
                _Qry &= vbCrLf & "	[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub AS B  WITH(NOLOCK)  ON A.FTOrderNo = B.FTOrderNo  AND P.FTSubOrderNo = B.FTSubOrderNo LEFT OUTER JOIN"
                _Qry &= vbCrLf & "	[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS S  WITH(NOLOCK)   ON A.FNHSysStyleId = S.FNHSysStyleId"
                _Qry &= vbCrLf & "	WHERE (FNHSysUnitSectId = " & Integer.Parse(Val(R!FNHSysUnitSectId.ToString)) & " )"
                _Qry &= vbCrLf & " and P.FDScanDate ='" & _CalDate & "'   "
                _Qry &= vbCrLf & " GROUP BY  P.FTOrderNo ,P.FTSubOrderNo, A.FNHSysStyleId ) AS M1 "
                _Qry &= vbCrLf & " LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TPPCTSamByOrder AS P WITH(NOLOCK) "
                _Qry &= vbCrLf & " ON M1.FNHSysStyleId = P.FNHSysStyleId "
                _Qry &= vbCrLf & " AND M1.FTOrderNo = P.FTOrderNo "
                _Qry &= vbCrLf & " AND M1.FTSubOrderNo = P.FTSubOrderNo "

                _Qry &= vbCrLf & " LEFT OUTER JOIN  ("
                _Qry &= vbCrLf & "   SELECT  FTOrderNo, FTSubOrderNo, SUM(FNTotalQty) AS FNTotalQty"
                _Qry &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTEditScanQuantity AS TXA WITH(NOLOCK)"
                _Qry &= vbCrLf & " WHERE TXA.FNHSysUnitSectId  =" & Integer.Parse(Val(R!FNHSysUnitSectId.ToString)) & " "
                _Qry &= vbCrLf & " AND TXA.FTDate='" & _CalDate & "'  "
                _Qry &= vbCrLf & "  GROUP BY  FTOrderNo, FTSubOrderNo"
                _Qry &= vbCrLf & " ) AS XXA ON "
                _Qry &= vbCrLf & "  M1.FTOrderNo = XXA.FTOrderNo "
                _Qry &= vbCrLf & " AND M1.FTSubOrderNo =XXA.FTSubOrderNo "

                _LineAmt = 0
                _LineIncentiveAmt = 0
                _CountStyle = 0
                _SumSam = 0
                _SumPrice = 0
                _LineNetAmt = 0
                _ScanQty = 0
                _IncentiveQty = 0
                _CountEmpIncentive = 0

                _dtsewstyle = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

                For Each Rxt As DataRow In _dtsewstyle.Rows

                    _CountStyle = _CountStyle + 1
                    _SumSam = _SumSam + Double.Parse(Val(Rxt!FNSam.ToString))
                    _SumPrice = _SumPrice + Double.Parse(Val(Rxt!FNNetPrice.ToString))
                    _LineAmt = _LineAmt + Val(Rxt!FNNetAmt.ToString)

                    _ScanQty = _ScanQty + Integer.Parse(Val(Rxt!FNScanQuantity.ToString))

                    _Qry = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTIncentive_Style ("
                    _Qry &= vbCrLf & "  FTInsUser, FDInsDate, FTInsTime, FTCalDate, FNHSysUnitSectId"
                    _Qry &= vbCrLf & ", FNHSysStyleId, FNSam, FNPricePerSam, FNPriceMultiple"
                    _Qry &= vbCrLf & ", FNNetPrice, FNQuantity, FNNetAmt,FTOrderNo,FTSubOrderNo)"
                    _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & ""
                    _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & ""
                    _Qry &= vbCrLf & " ,'" & _CalDate & "'   "
                    _Qry &= vbCrLf & " ," & Integer.Parse(Val(R!FNHSysUnitSectId.ToString)) & ""
                    _Qry &= vbCrLf & " ," & Integer.Parse(Val(Rxt!FNHSysStyleId.ToString)) & ""
                    _Qry &= vbCrLf & " ," & Double.Parse(Val(Rxt!FNSam.ToString)) & ""
                    _Qry &= vbCrLf & " ," & Double.Parse(Val(Rxt!FNPrice.ToString)) & ""
                    _Qry &= vbCrLf & " ," & Double.Parse(Val(Rxt!FNMultiple.ToString)) & ""
                    _Qry &= vbCrLf & " ," & Double.Parse(Val(Rxt!FNNetPrice.ToString)) & ""
                    _Qry &= vbCrLf & " ," & Integer.Parse(Val(Rxt!FNScanQuantity.ToString)) & ""
                    _Qry &= vbCrLf & " ," & Double.Parse(Val(Rxt!FNNetAmt.ToString)) & ""
                    _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(Rxt!FTOrderNo.ToString) & "'"
                    _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(Rxt!FTSubOrderNo.ToString) & "'"

                    HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

                Next

                _dtsewstyle.Dispose()

                '-----ตรวจสอบประกันรายชั่วโมง ประกันรายวัน ------------------

                _IncentiveNormalQty = 0
                _IncentiveOTQty = 0
                _FixHour = 0

                _H01Qty = 0
                _H02Qty = 0
                _H03Qty = 0
                _H04Qty = 0
                _H05Qty = 0
                _H06Qty = 0
                _H07Qty = 0
                _H08Qty = 0
                _H09Qty = 0
                _H10Qty = 0
                _H11Qty = 0
                _H12Qty = 0
                _H13Qty = 0

                If R!FTStateInsuranceInH1.ToString = "1" _
                    Or R!FTStateInsuranceInH2.ToString = "1" _
                    Or R!FTStateInsuranceInH3.ToString = "1" _
                    Or R!FTStateInsuranceInH4.ToString = "1" _
                    Or R!FTStateInsuranceInH5.ToString = "1" _
                    Or R!FTStateInsuranceInH6.ToString = "1" _
                    Or R!FTStateInsuranceInH7.ToString = "1" _
                    Or R!FTStateInsuranceInH8.ToString = "1" _
                    Or R!FTStateInsuranceInH9.ToString = "1" _
                    Or R!FTStateInsuranceInH10.ToString = "1" _
                    Or R!FTStateInsuranceInH11.ToString = "1" _
                    Or R!FTStateInsuranceInH12.ToString = "1" _
                    Or R!FTStateInsuranceInH13.ToString = "1" _
                    Or R!FTStateInsuranceInDay.ToString = "1" Then

                    If _FTStateInsuranceInDay <> "1" Then

                        If _FTStateInsuranceInH1 <> "1" Then

                            _IncentiveQty = _IncentiveQty + Integer.Parse(Val(R!H01.ToString))
                            _IncentiveNormalQty = _IncentiveNormalQty + Integer.Parse(Val(R!H01.ToString))
                            _FixHour = _FixHour + 1
                            _H01Qty = Integer.Parse(Val(R!H01.ToString))

                        End If

                        If _FTStateInsuranceInH2 <> "1" Then

                            _IncentiveQty = _IncentiveQty + Integer.Parse(Val(R!H02.ToString))
                            _IncentiveNormalQty = _IncentiveNormalQty + Integer.Parse(Val(R!H02.ToString))
                            _FixHour = _FixHour + 1
                            _H02Qty = Integer.Parse(Val(R!H02.ToString))

                        End If

                        If _FTStateInsuranceInH3 <> "1" Then

                            _IncentiveQty = _IncentiveQty + Integer.Parse(Val(R!H03.ToString))
                            _IncentiveNormalQty = _IncentiveNormalQty + Integer.Parse(Val(R!H03.ToString))
                            _FixHour = _FixHour + 1
                            _H03Qty = Integer.Parse(Val(R!H03.ToString))

                        End If

                        If _FTStateInsuranceInH4 <> "1" Then

                            _IncentiveQty = _IncentiveQty + Integer.Parse(Val(R!H04.ToString))
                            _IncentiveNormalQty = _IncentiveNormalQty + Integer.Parse(Val(R!H04.ToString))
                            _FixHour = _FixHour + 1
                            _H04Qty = Integer.Parse(Val(R!H04.ToString))

                        End If

                        If _FTStateInsuranceInH5 <> "1" Then

                            _IncentiveQty = _IncentiveQty + Integer.Parse(Val(R!H05.ToString))
                            _IncentiveNormalQty = _IncentiveNormalQty + Integer.Parse(Val(R!H05.ToString))
                            _FixHour = _FixHour + 1
                            _H05Qty = Integer.Parse(Val(R!H05.ToString))

                        End If

                        If _FTStateInsuranceInH6 <> "1" Then

                            _IncentiveQty = _IncentiveQty + Integer.Parse(Val(R!H06.ToString))
                            _IncentiveNormalQty = _IncentiveNormalQty + Integer.Parse(Val(R!H06.ToString))
                            _FixHour = _FixHour + 1
                            _H06Qty = Integer.Parse(Val(R!H06.ToString))

                        End If

                        If _FTStateInsuranceInH7 <> "1" Then

                            _IncentiveQty = _IncentiveQty + Integer.Parse(Val(R!H07.ToString))
                            _IncentiveNormalQty = _IncentiveNormalQty + Integer.Parse(Val(R!H07.ToString))
                            _FixHour = _FixHour + 1
                            _H07Qty = Integer.Parse(Val(R!H07.ToString))

                        End If

                        If _FTStateInsuranceInH8 <> "1" Then

                            _IncentiveQty = _IncentiveQty + Integer.Parse(Val(R!H08.ToString))
                            _IncentiveNormalQty = _IncentiveNormalQty + Integer.Parse(Val(R!H08.ToString))
                            _FixHour = _FixHour + 1
                            _H08Qty = Integer.Parse(Val(R!H08.ToString))

                        End If

                        If _FTStateInsuranceInH9 <> "1" Then

                            _IncentiveQty = _IncentiveQty + Integer.Parse(Val(R!H09.ToString))
                            _IncentiveOTQty = _IncentiveOTQty + Integer.Parse(Val(R!H09.ToString))
                            _H09Qty = Integer.Parse(Val(R!H09.ToString))

                        End If

                        If _FTStateInsuranceInH10 <> "1" Then

                            _IncentiveQty = _IncentiveQty + Integer.Parse(Val(R!H10.ToString))
                            _IncentiveOTQty = _IncentiveOTQty + Integer.Parse(Val(R!H10.ToString))
                            _H10Qty = Integer.Parse(Val(R!H10.ToString))

                        End If

                        If _FTStateInsuranceInH11 <> "1" Then

                            _IncentiveQty = _IncentiveQty + Integer.Parse(Val(R!H11.ToString))
                            _IncentiveOTQty = _IncentiveOTQty + Integer.Parse(Val(R!H11.ToString))
                            _H11Qty = Integer.Parse(Val(R!H11.ToString))

                        End If

                        If _FTStateInsuranceInH12 <> "1" Then

                            _IncentiveQty = _IncentiveQty + Integer.Parse(Val(R!H12.ToString))
                            _IncentiveOTQty = _IncentiveOTQty + Integer.Parse(Val(R!H12.ToString))
                            _H12Qty = Integer.Parse(Val(R!H12.ToString))

                        End If

                        If _FTStateInsuranceInH13 <> "1" Then

                            _IncentiveQty = _IncentiveQty + Integer.Parse(Val(R!H13.ToString))
                            _IncentiveOTQty = _IncentiveOTQty + Integer.Parse(Val(R!H13.ToString))
                            _H13Qty = Integer.Parse(Val(R!H13.ToString))

                        End If

                    Else

                        _IncentiveNormalQty = Integer.Parse(Val(R!H01.ToString)) + Integer.Parse(Val(R!H02.ToString)) + Integer.Parse(Val(R!H03.ToString)) + Integer.Parse(Val(R!H04.ToString)) + Integer.Parse(Val(R!H05.ToString)) + Integer.Parse(Val(R!H06.ToString)) + Integer.Parse(Val(R!H07.ToString)) + Integer.Parse(Val(R!H08.ToString))
                        _IncentiveOTQty = Integer.Parse(Val(R!H09.ToString)) + Integer.Parse(Val(R!H10.ToString)) + Integer.Parse(Val(R!H11.ToString)) + Integer.Parse(Val(R!H12.ToString)) + Integer.Parse(Val(R!H13.ToString))

                        _LineIncentiveAmt = 0
                        _IncentiveQty = 0

                    End If

                    _FoundFixInsurance = True

                Else

                    _H01Qty = Integer.Parse(Val(R!H01.ToString))
                    _H02Qty = Integer.Parse(Val(R!H02.ToString))
                    _H03Qty = Integer.Parse(Val(R!H03.ToString))
                    _H04Qty = Integer.Parse(Val(R!H04.ToString))
                    _H05Qty = Integer.Parse(Val(R!H05.ToString))
                    _H06Qty = Integer.Parse(Val(R!H06.ToString))
                    _H07Qty = Integer.Parse(Val(R!H07.ToString))
                    _H08Qty = Integer.Parse(Val(R!H08.ToString))
                    _H09Qty = Integer.Parse(Val(R!H09.ToString))
                    _H10Qty = Integer.Parse(Val(R!H10.ToString))
                    _H11Qty = Integer.Parse(Val(R!H11.ToString))
                    _H12Qty = Integer.Parse(Val(R!H12.ToString))
                    _H13Qty = Integer.Parse(Val(R!H13.ToString))

                    _LineIncentiveAmt = _LineAmt
                    _IncentiveQty = _ScanQty

                End If

                If _FoundFixInsurance Then
                    If _IncentiveQty <> _ScanQty Then
                        _LineIncentiveAmt = Double.Parse(Format((_LineAmt / _ScanQty) * _IncentiveQty, "0.00"))
                    End If
                End If

                '-----ตรวจสอบประกันรายชั่วโมง ประกันรายวัน ------------------

                _CountEmp = 0
                _Salary = 0
                _SalaryPerH = 0
                _SalaryPerOT = 0
                _FNQAPer = 0
                _FNQAValue = 0
                _FNAmtNormal = 0
                _FNAmtOT = 0
                _FNNetAmt = 0
                'Calculate Wage HR Normal
                _TotalTime = 0
                _TotalTimeOT = 0

                For Each Rt As DataRow In _dtemptime.Rows
                    _CountEmp = _CountEmp + 1
                    _TotalTime = _TotalTime + Integer.Parse(Val(Rt!FNWorkingMin.ToString))
                    _TotalTimeOT = _TotalTimeOT + Integer.Parse(Val(Rt!FNWorkingOTMin.ToString))

                    _TotalTimeHR = Integer.Parse(Val(Rt!FNTimeMin.ToString))
                    _TotalTimeOTHR = Integer.Parse(Val(Rt!FNOT1Min.ToString))

                    _Salary = Double.Parse(Val(Rt!FNSalary.ToString))
                    _SalaryPerH = Double.Parse(Format(_Salary / 8, "0.00"))
                    _SalaryPerOT = Double.Parse(Format(_SalaryPerH * 1.5, "0.00"))

                    If _TotalTimeHR >= 480 Then
                        _FNAmtNormal = _Salary
                    Else
                        _FNAmtNormal = Double.Parse(Format((_Salary / 480) * _TotalTimeHR, "0.00"))
                    End If

                    _FNAmtOT = Double.Parse(Format(((_Salary / 480) * 1.5) * _TotalTimeOTHR, "0.00"))

                    _FNNetAmt = _FNAmtNormal + _FNAmtOT

                    _Qry = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTIncentive_Emp ("
                    _Qry &= vbCrLf & "  FTInsUser, FDInsDate, FTInsTime, FTCalDate, FNHSysUnitSectId, FNHSysEmpID"
                    _Qry &= vbCrLf & ", FNTimeHour01, FNTimeHour02, FNTimeHour03, FNTimeHour04, FNTimeHour05, FNTimeHour06, FNTimeHour07, FNTimeHour08, "
                    _Qry &= vbCrLf & "   FNTimeHour09, FNTimeHour10, FNTimeHour11, FNTimeHour12, FNTimeHour13, FNTotalTime, FNTotalTimeOT,FNTotalTimHR,FNTotalTimeOTHR"
                    _Qry &= vbCrLf & ", FNSalary, FNAmtNormal, FNAmtOT, FNNetAmt, FNAmtOldIncentive, FNAmtOTOldIncentive,"
                    _Qry &= vbCrLf & "  FNNetAmtOldIncentive, FNAmtNewIncentive, FNAmtOTNewIncentive, FNNetAmtNewIncentive,FNQAValue,FTInsuranceHour,FTInsuranceAmt"


                    _Qry &= vbCrLf & ",FNAmtHour01, FNAmtHour02, FNAmtHour03, FNAmtHour04, FNAmtHour05, FNAmtHour06, FNAmtHour07, FNAmtHour08, FNAmtHour09, FNAmtHour10, FNAmtHour11, FNAmtHour12, FNAmtHour13,"
                    _Qry &= vbCrLf & "FNAmtHour01NewIncentive, FNAmtHour02NewIncentive, FNAmtHour03NewIncentive, FNAmtHour04NewIncentive, FNAmtHour05NewIncentive, FNAmtHour06NewIncentive, FNAmtHour07NewIncentive,"
                    _Qry &= vbCrLf & "FNAmtHour08NewIncentive, FNAmtHour09NewIncentive, FNAmtHour10NewIncentive, FNAmtHour11NewIncentive, FNAmtHour12NewIncentive, FNAmtHour13NewIncentive, FNAmtHour01Insurance,"
                    _Qry &= vbCrLf & "FNAmtHour02Insurance, FNAmtHour03Insurance, FNAmtHour04Insurance, FNAmtHour05Insurance, FNAmtHour06Insurance, FNAmtHour07Insurance, FNAmtHour08Insurance, FNAmtHour09Insurance,"
                    _Qry &= vbCrLf & " FNAmtHour10Insurance, FNAmtHour11Insurance, FNAmtHour12Insurance, FNAmtHour13Insurance, FNAmtHour02Insurance1, FNAmtHour03Insurance1, FNAmtHour04Insurance1, FNAmtHour05Insurance1,"
                    _Qry &= vbCrLf & "FNAmtHour06Insurance1, FNAmtHour07Insurance1, FNAmtHour08Insurance1, FNAmtHour09Insurance1, FNAmtHour10Insurance1, FNAmtHour11Insurance1, FNAmtHour12Insurance1, FNAmtHour13Insurance1,"
                    _Qry &= vbCrLf & " FNAmtHour01HR, FNAmtHour02HR, FNAmtHour03HR, FNAmtHour04HR, FNAmtHour05HR, FNAmtHour06HR, FNAmtHour07HR, FNAmtHour08HR, FNAmtHour09HR, FNAmtHour10HR, FNAmtHour11HR, FNAmtHour12HR,"
                    _Qry &= vbCrLf & " FNAmtHour13HR"


                    _Qry &= vbCrLf & ")"
                    _Qry &= vbCrLf & " Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & ""
                    _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & ""
                    _Qry &= vbCrLf & " ,'" & _CalDate & "'   "
                    _Qry &= vbCrLf & " ," & Integer.Parse(Val(R!FNHSysUnitSectId.ToString)) & ""
                    _Qry &= vbCrLf & " ," & Integer.Parse(Val(Rt!FNHSysEmpID.ToString)) & ""
                    _Qry &= vbCrLf & " ," & Integer.Parse(Val(Rt!FNH01.ToString)) & ""
                    _Qry &= vbCrLf & " ," & Integer.Parse(Val(Rt!FNH02.ToString)) & ""
                    _Qry &= vbCrLf & " ," & Integer.Parse(Val(Rt!FNH03.ToString)) & ""
                    _Qry &= vbCrLf & " ," & Integer.Parse(Val(Rt!FNH04.ToString)) & ""
                    _Qry &= vbCrLf & " ," & Integer.Parse(Val(Rt!FNH05.ToString)) & ""
                    _Qry &= vbCrLf & " ," & Integer.Parse(Val(Rt!FNH06.ToString)) & ""
                    _Qry &= vbCrLf & " ," & Integer.Parse(Val(Rt!FNH07.ToString)) & ""
                    _Qry &= vbCrLf & " ," & Integer.Parse(Val(Rt!FNH08.ToString)) & ""
                    _Qry &= vbCrLf & " ," & Integer.Parse(Val(Rt!FNH09.ToString)) & ""
                    _Qry &= vbCrLf & " ," & Integer.Parse(Val(Rt!FNH10.ToString)) & ""
                    _Qry &= vbCrLf & " ," & Integer.Parse(Val(Rt!FNH11.ToString)) & ""
                    _Qry &= vbCrLf & " ," & Integer.Parse(Val(Rt!FNH12.ToString)) & ""
                    _Qry &= vbCrLf & " ," & Integer.Parse(Val(Rt!FNH13.ToString)) & ""
                    _Qry &= vbCrLf & " ," & Integer.Parse(Val(Rt!FNWorkingMin.ToString)) & ""
                    _Qry &= vbCrLf & " ," & Integer.Parse(Val(Rt!FNWorkingOTMin.ToString)) & ""
                    _Qry &= vbCrLf & " ," & _TotalTimeHR & ""
                    _Qry &= vbCrLf & " ," & _TotalTimeOTHR & ""
                    _Qry &= vbCrLf & " ," & _Salary & ""
                    _Qry &= vbCrLf & " ," & _FNAmtNormal & ""
                    _Qry &= vbCrLf & " ," & _FNAmtOT & ""
                    _Qry &= vbCrLf & " ," & _FNNetAmt & ""
                    _Qry &= vbCrLf & " ,0,0,0,0,0,0,0"
                    _Qry &= vbCrLf & " ," & _FixHour & ""
                    _Qry &= vbCrLf & " ,0"
                    _Qry &= vbCrLf & " ,0,0,0,0,0,0,0,0,0,0,0,0,0"
                    _Qry &= vbCrLf & " ,0,0,0,0,0,0,0,0,0,0,0,0,0"
                    _Qry &= vbCrLf & " ,0,0,0,0,0,0,0,0,0,0,0,0,0"

                    HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

                Next
                'Calculate Wage HR Normal

                'Load Quality QA

                _FNQAPer = 0
                _FNQAValue = 0

                Select Case Val(R!FTIncentiveTypeIdx.ToString)
                    Case 1
                        _Qry = "  SELECT   ISNULL(SUM( A.FNQAActualQty),0) AS FNQAActualQty "
                        _Qry &= vbCrLf & "  , ISNULL((SUM(A.FNMajorQty)+SUM(A.FNMinorQty)),0)  AS FNTotalDefect             "
                        _Qry &= vbCrLf & "   FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTQA AS A WITH (NOLOCK) LEFT OUTER JOIN"
                        _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS B WITH (NOLOCK) ON A.FNHSysStyleId = B.FNHSysStyleId LEFT OUTER JOIN"
                        _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH (NOLOCK) ON A.FTOrderNo = O.FTOrderNo"
                        _Qry &= vbCrLf & "   WHERE (A.FDQADate ='" & _CalDate & "' )"
                        _Qry &= vbCrLf & "   AND FNHSysUnitSectId=" & Integer.Parse(Val(R!FNHSysUnitSectId.ToString)) & ""
                        _dtqa = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

                        For Each Rx As DataRow In _dtqa.Rows

                            If Val(Rx!FNQAActualQty.ToString) > 0 Then
                                _FNQAPer = Double.Parse(Format((((Val(Rx!FNQAActualQty.ToString) - Val(Rx!FNTotalDefect.ToString)) / Val(Rx!FNQAActualQty.ToString)) * 100), "0.00"))
                            End If

                            Exit For
                        Next

                        _Qry = "    SELECT TOP 1 FNAmt"
                        _Qry &= vbCrLf & "   FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODMQualityValue AS X WITH(NOLOCK)"
                        _Qry &= vbCrLf & "  WHERE  (FNStartPer <= " & _FNQAPer & ")"
                        _Qry &= vbCrLf & "  AND (FNEndPer >= " & _FNQAPer & ")"
                        _Qry &= vbCrLf & "  ORDER BY FNAmt DESC"

                        _FNQAValue = Double.Parse(Format(Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PROD, "0")), "0.00"))
                    Case 2
                    Case Else

                End Select


                'Load Quality QA

                _LineNetAmt = (_LineIncentiveAmt + _FNQAValue)

                _Qry = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTIncentive ("
                _Qry &= vbCrLf & "  FTInsUser, FDInsDate, FTInsTime,  FTCalDate, FNHSysUnitSectId, FNHour01Qty, FNHour02Qty"
                _Qry &= vbCrLf & " , FNHour03Qty, FNHour04Qty, FNHour05Qty, FNHour06Qty, FNHour07Qty, "
                _Qry &= vbCrLf & "   FNHour08Qty, FNHour09Qty, FNHour10Qty, FNHour11Qty, FNHour12Qty, FNHour13Qty"
                _Qry &= vbCrLf & " , FNTotalQty, FTStateInsuranceInH1, FTStateInsuranceInH2, FTStateInsuranceInH3, FTStateInsuranceInH4, FTStateInsuranceInH5,"
                _Qry &= vbCrLf & "   FTStateInsuranceInH6, FTStateInsuranceInH7, FTStateInsuranceInH8, FTStateInsuranceInH9, FTStateInsuranceInH10"
                _Qry &= vbCrLf & " , FTStateInsuranceInH11, FTStateInsuranceInH12, FTStateInsuranceInH13,"
                _Qry &= vbCrLf & "   FTStateInsuranceInDay, FNTotalEmp, FNTotalTime, FNTeamAmt, FNQAPer, FNQAValue, FNTeamNetAmt, FNIncentiveType,FNTeamIncentiveQty,FNTeamIncentiveAmt"
                _Qry &= vbCrLf & "  , FNHour01QtySystem, FNHour02QtySystem"
                _Qry &= vbCrLf & "  ,  FNHour03QtySystem, FNHour04QtySystem"
                _Qry &= vbCrLf & "  , FNHour05QtySystem, FNHour06QtySystem"
                _Qry &= vbCrLf & "  , FNHour07QtySystem, FNHour08QtySystem"
                _Qry &= vbCrLf & "  , FNHour09QtySystem, FNHour10QtySystem"
                _Qry &= vbCrLf & " , FNHour11QtySystem, FNHour12QtySystem"
                _Qry &= vbCrLf & "  , FNHour13QtySystem, FNTotalQtySystem"

                _Qry &= vbCrLf & ")"
                _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & ""
                _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & ""
                _Qry &= vbCrLf & " ,'" & _CalDate & "'   "
                _Qry &= vbCrLf & " ," & Integer.Parse(Val(R!FNHSysUnitSectId.ToString)) & ""
                _Qry &= vbCrLf & " ," & Integer.Parse(Val(R!H01.ToString)) & ""
                _Qry &= vbCrLf & " ," & Integer.Parse(Val(R!H02.ToString)) & ""
                _Qry &= vbCrLf & " ," & Integer.Parse(Val(R!H03.ToString)) & ""
                _Qry &= vbCrLf & " ," & Integer.Parse(Val(R!H04.ToString)) & ""
                _Qry &= vbCrLf & " ," & Integer.Parse(Val(R!H05.ToString)) & ""
                _Qry &= vbCrLf & " ," & Integer.Parse(Val(R!H06.ToString)) & ""
                _Qry &= vbCrLf & " ," & Integer.Parse(Val(R!H07.ToString)) & ""
                _Qry &= vbCrLf & " ," & Integer.Parse(Val(R!H08.ToString)) & ""
                _Qry &= vbCrLf & " ," & Integer.Parse(Val(R!H09.ToString)) & ""
                _Qry &= vbCrLf & " ," & Integer.Parse(Val(R!H10.ToString)) & ""
                _Qry &= vbCrLf & " ," & Integer.Parse(Val(R!H11.ToString)) & ""
                _Qry &= vbCrLf & " ," & Integer.Parse(Val(R!H12.ToString)) & ""
                _Qry &= vbCrLf & " ," & Integer.Parse(Val(R!H13.ToString)) & ""
                _Qry &= vbCrLf & " ," & Integer.Parse(Val(R!Total.ToString)) & ""
                _Qry &= vbCrLf & " ,'" & R!FTStateInsuranceInH1.ToString & "'"
                _Qry &= vbCrLf & " ,'" & R!FTStateInsuranceInH2.ToString & "'"
                _Qry &= vbCrLf & " ,'" & R!FTStateInsuranceInH3.ToString & "'"
                _Qry &= vbCrLf & " ,'" & R!FTStateInsuranceInH4.ToString & "'"
                _Qry &= vbCrLf & " ,'" & R!FTStateInsuranceInH5.ToString & "'"
                _Qry &= vbCrLf & " ,'" & R!FTStateInsuranceInH6.ToString & "'"
                _Qry &= vbCrLf & " ,'" & R!FTStateInsuranceInH7.ToString & "'"
                _Qry &= vbCrLf & " ,'" & R!FTStateInsuranceInH8.ToString & "'"
                _Qry &= vbCrLf & " ,'" & R!FTStateInsuranceInH9.ToString & "'"
                _Qry &= vbCrLf & " ,'" & R!FTStateInsuranceInH10.ToString & "'"
                _Qry &= vbCrLf & " ,'" & R!FTStateInsuranceInH11.ToString & "'"
                _Qry &= vbCrLf & " ,'" & R!FTStateInsuranceInH12.ToString & "'"
                _Qry &= vbCrLf & " ,'" & R!FTStateInsuranceInH13.ToString & "'"
                _Qry &= vbCrLf & " ,'" & R!FTStateInsuranceInDay.ToString & "'"
                _Qry &= vbCrLf & " ," & _CountEmp & ""
                _Qry &= vbCrLf & " ," & _TotalTime + _TotalTimeOT & ""
                _Qry &= vbCrLf & " ," & _LineAmt & ""
                _Qry &= vbCrLf & " ," & _FNQAPer & ""
                _Qry &= vbCrLf & " ," & _FNQAValue & ""
                _Qry &= vbCrLf & " ," & _LineNetAmt & ""
                _Qry &= vbCrLf & " ," & Integer.Parse(Val(R!FTIncentiveTypeIdx.ToString)) & ""
                _Qry &= vbCrLf & " ," & _LineIncentiveAmt & ""
                _Qry &= vbCrLf & " ," & _IncentiveQty & ""
                _Qry &= vbCrLf & " ," & Integer.Parse(Val(R!FNHour01QtySystem.ToString)) & ""
                _Qry &= vbCrLf & " ," & Integer.Parse(Val(R!FNHour02QtySystem.ToString)) & ""
                _Qry &= vbCrLf & " ," & Integer.Parse(Val(R!FNHour03QtySystem.ToString)) & ""
                _Qry &= vbCrLf & " ," & Integer.Parse(Val(R!FNHour04QtySystem.ToString)) & ""
                _Qry &= vbCrLf & " ," & Integer.Parse(Val(R!FNHour05QtySystem.ToString)) & ""
                _Qry &= vbCrLf & " ," & Integer.Parse(Val(R!FNHour06QtySystem.ToString)) & ""
                _Qry &= vbCrLf & " ," & Integer.Parse(Val(R!FNHour07QtySystem.ToString)) & ""
                _Qry &= vbCrLf & " ," & Integer.Parse(Val(R!FNHour08QtySystem.ToString)) & ""
                _Qry &= vbCrLf & " ," & Integer.Parse(Val(R!FNHour09QtySystem.ToString)) & ""
                _Qry &= vbCrLf & " ," & Integer.Parse(Val(R!FNHour10QtySystem.ToString)) & ""
                _Qry &= vbCrLf & " ," & Integer.Parse(Val(R!FNHour11QtySystem.ToString)) & ""
                _Qry &= vbCrLf & " ," & Integer.Parse(Val(R!FNHour12QtySystem.ToString)) & ""
                _Qry &= vbCrLf & " ," & Integer.Parse(Val(R!FNHour13QtySystem.ToString)) & ""
                _Qry &= vbCrLf & " ," & Integer.Parse(Val(R!FNTotalQtySystem.ToString)) & ""
                HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)
                _DeductAmt = 0
                If _LineIncentiveAmt > 0 Then

                    _EmpCountCalincentive = _CountEmp

                    For Each Rt As DataRow In _dtemptime.Select("FTStateDaily='1'")
                        Rt!FTStateCal = "1"
                    Next

                    For Each Rt As DataRow In _dtemptime.Select("FTStatePlagnent='1' AND FNTotalWorkingMin >0")
                        Rt!FTStateCal = "1"
                    Next

                    For Each Rt As DataRow In _dtemptime.Select("FTStatePlagnent='1' AND FNTotalWorkingMin >0")
                        _EmpCountCalincentive = _EmpCountCalincentive - 1
                    Next

                    Dim _PriceCost As Double = 0
                    Dim _TotalCountStyle As Integer = _CountStyle
                    Dim _FNSam As Double = 0
                    Dim _TotalCountEmp As Integer = _EmpCountCalincentive
                    Dim _TimeWorlPlanMinute As Integer = _TotalTime + _TotalTimeOT
                    Dim _TotalProd As Integer = _IncentiveQty

                    If _TotalCountStyle > 1 Then
                        _PriceCost = Double.Parse(Format(_SumPrice / _TotalCountStyle, "0.0000"))
                        _FNSam = Double.Parse(Format(_SumSam / _TotalCountStyle, "0.0000"))
                    Else
                        _PriceCost = _SumPrice
                        _FNSam = _SumSam
                    End If

                    _FNTotalOTWorkingMin = 0

                    _FNTotalWorkingMin = 0
                    For Each Rt As DataRow In _dtemptime.Select("FNTotalWorkingMin > 0  AND FTStateCal<>'1'", "FNTotalWorkingMin")
                        _FNTotalWorkingMin = Integer.Parse(Val(Rt!FNTotalWorkingMin.ToString))
                    Next

                    For Each Rt As DataRow In _dtemptime.Select("FNWorkingOTMin > 0  AND FTStateCal<>'1'", "FNWorkingOTMin")
                        _FNTotalOTWorkingMin = Integer.Parse(Val(Rt!FNWorkingOTMin.ToString))
                    Next

                    _CountEmpIncentive = _dtemptime.Select("FNTotalWorkingMin>0 AND FTStateCal<>'1'").Length
                    _DeductAmt = 0

                    '--------คำนวณคนที่มาทพงานไม่ครบ---------------
                    Dim _totalCal As Integer = 0
                    Dim _EmpDedeuctAmt As Double = 0
                    Dim _EmpDedeuctNetAmt As Double = 0

                    '-----------สายเช้ามากกว่า 15 ไม่ได้ Incentive ในชั่วโมงนั้น
                    For Each Rt As DataRow In _dtemptime.Select("FNLateMMin > 15 AND FTStateCal<>'1' AND FNTotalWorkingMin>0")
                        _totalCal = 0

                        _Salary = Double.Parse(Val(Rt!FNSalary.ToString))
                        _SalaryPerH = Double.Parse(Format(_Salary / 8, "0.00"))
                        _SalaryPerOT = Double.Parse(Format(_SalaryPerH * 1.5, "0.00"))

                        If _FixHour > 0 Then

                        End If

                        _FNAmtOT = Double.Parse(Format(((_Salary / 480) * 0.5) * Integer.Parse(Val(Rt!FNWorkingOTMin.ToString)), "0.00"))


                        If Val(Rt!FNLateMMin.ToString) <= 15 And (Val(Rt!FNH01.ToString) > 0) Then
                            _totalCal = _totalCal + _H01Qty
                        End If

                        If (Val(Rt!FNH02.ToString) > 0) Then
                            _totalCal = _totalCal + _H02Qty
                        End If

                        If (Val(Rt!FNH03.ToString) > 0) Then
                            _totalCal = _totalCal + _H03Qty
                        End If

                        If (Val(Rt!FNH04.ToString) > 0) And Val(Rt!FNRetireMMin.ToString) <= 0 Then
                            _totalCal = _totalCal + _H04Qty
                        End If

                        If (Val(Rt!FNH05.ToString) > 0) And Val(Rt!FNLateAfMin.ToString) <= 5 Then
                            _totalCal = _totalCal + _H05Qty
                        End If

                        If (Val(Rt!FNH06.ToString) > 0) Then
                            _totalCal = _totalCal + _H06Qty
                        End If

                        If (Val(Rt!FNH07.ToString) > 0) Then
                            _totalCal = _totalCal + _H07Qty
                        End If

                        If (Val(Rt!FNH08.ToString) > 0) And Val(Rt!FNRetireAfMin.ToString) <= 0 Then
                            _totalCal = _totalCal + _H08Qty
                        End If

                        If (Val(Rt!FNH09.ToString) > 0) And Val(Rt!FNLateOtMin.ToString) <= 5 Then
                            _totalCal = _totalCal + _H09Qty
                        End If

                        If _FNTotalOTWorkingMin > 60 And _FNTotalOTWorkingMin <= 120 Then
                            If (Val(Rt!FNH10.ToString) > 0) And Val(Rt!FNRetireOtMin.ToString) <= 0 Then
                                _totalCal = _totalCal + _H10Qty
                            End If
                        Else
                            If (Val(Rt!FNH10.ToString) > 0) Then
                                _totalCal = _totalCal + _H10Qty
                            End If
                        End If

                        If _FNTotalOTWorkingMin > 120 And _FNTotalOTWorkingMin <= 180 Then
                            If (Val(Rt!FNH11.ToString) > 0) And Val(Rt!FNRetireOtMin.ToString) <= 0 Then
                                _totalCal = _totalCal + _H11Qty
                            End If
                        Else
                            If (Val(Rt!FNH11.ToString) > 0) Then
                                _totalCal = _totalCal + _H11Qty
                            End If
                        End If

                        If _FNTotalOTWorkingMin > 180 And _FNTotalOTWorkingMin <= 240 Then
                            If (Val(Rt!FNH12.ToString) > 0) And Val(Rt!FNRetireOtMin.ToString) <= 0 Then
                                _totalCal = _totalCal + _H12Qty
                            End If
                        Else
                            If (Val(Rt!FNH12.ToString) > 0) Then
                                _totalCal = _totalCal + _H12Qty
                            End If

                        End If

                        If _FNTotalOTWorkingMin > 240 And _FNTotalOTWorkingMin <= 300 Then
                            If (Val(Rt!FNH13.ToString) > 0) And Val(Rt!FNRetireOtMin.ToString) <= 0 Then
                                _totalCal = _totalCal + _H13Qty
                            End If
                        Else
                            If (Val(Rt!FNH13.ToString) > 0) Then
                                _totalCal = _totalCal + _H13Qty
                            End If
                        End If

                        _EmpDedeuctAmt = (_totalCal * _PriceCost)
                        _EmpDedeuctNetAmt = Double.Parse(Format(_EmpDedeuctAmt / _CountEmpIncentive, "0.00"))

                        _DeductAmt = _DeductAmt + _EmpDedeuctNetAmt

                        _Qry = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTIncentive_Emp_Deduct ("
                        _Qry &= vbCrLf & "  FTInsUser, FDInsDate, FTInsTime, FTCalDate, FNHSysUnitSectId, FNHSysEmpID"
                        _Qry &= vbCrLf & ", FNQuantity, FNPrice, FNAmt, FNTotalEmp, FNEmpAmt"
                        _Qry &= vbCrLf & ")"
                        _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & ""
                        _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & ""
                        _Qry &= vbCrLf & " ,'" & _CalDate & "'   "
                        _Qry &= vbCrLf & " ," & Integer.Parse(Val(R!FNHSysUnitSectId.ToString)) & ""
                        _Qry &= vbCrLf & " ," & Integer.Parse(Val(Rt!FNHSysEmpID.ToString)) & ""
                        _Qry &= vbCrLf & " ," & _totalCal & ""
                        _Qry &= vbCrLf & " ," & _PriceCost & ""
                        _Qry &= vbCrLf & " ," & _EmpDedeuctAmt & ""
                        _Qry &= vbCrLf & " ," & _CountEmpIncentive & ""
                        _Qry &= vbCrLf & " ," & _EmpDedeuctNetAmt & ""

                        HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

                        _Qry = " UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTIncentive_Emp "
                        _Qry &= vbCrLf & "  SET  FNAmtOldIncentive=" & _EmpDedeuctNetAmt & ""
                        _Qry &= vbCrLf & "  , FNAmtOTOldIncentive=" & _FNAmtOT & ""
                        _Qry &= vbCrLf & "  , FNNetAmtOldIncentive=" & (_EmpDedeuctNetAmt + _FNQAValue) & ""
                        _Qry &= vbCrLf & "   ,FNQAValue=" & _FNQAValue & " "
                        _Qry &= vbCrLf & "   ,FNAmtNewIncentive = " & _EmpDedeuctNetAmt & ""
                        _Qry &= vbCrLf & "  , FNAmtOTNewIncentive=" & _FNAmtOT & ""
                        _Qry &= vbCrLf & "  , FNNetAmtNewIncentive=" & (_EmpDedeuctNetAmt + _FNQAValue) & ""
                        _Qry &= vbCrLf & "  WHERE FNHSysUnitSectId=" & Integer.Parse(Val(R!FNHSysUnitSectId.ToString)) & ""
                        _Qry &= vbCrLf & "       AND FNHSysEmpID=" & Integer.Parse(Val(Rt!FNHSysEmpID.ToString)) & ""
                        _Qry &= vbCrLf & "       AND FTCalDate='" & _CalDate & "'"

                        HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

                        Rt!FTStateCal = "1"

                    Next

                    '-----------สายบ่าย เกิน 5 นาที ไม่ได้ Incentive ในชั่วโมงนั้น
                    For Each Rt As DataRow In _dtemptime.Select("(FNLateAfMin > 5 AND FTStateCal<>'1'  AND FNTotalWorkingMin>0) OR (FNRetireMMin > 0 AND FTStateCal<>'1'  AND FNTotalWorkingMin>0) OR (FNRetireAfMin > 0 AND FTStateCal<>'1'  AND FNTotalWorkingMin>0) OR (FNLateOtMin > 5 AND FTStateCal<>'1'  AND FNTotalWorkingMin>0)  OR (FNRetireOtMin > 0 AND FTStateCal<>'1'  AND FNTotalWorkingMin>0)")
                        _totalCal = 0

                        _Salary = Double.Parse(Val(Rt!FNSalary.ToString))
                        _SalaryPerH = Double.Parse(Format(_Salary / 8, "0.00"))
                        _SalaryPerOT = Double.Parse(Format(_SalaryPerH * 1.5, "0.00"))

                        If _FixHour > 0 Then

                        End If

                        _FNAmtOT = Double.Parse(Format(((_Salary / 480) * 0.5) * Integer.Parse(Val(Rt!FNWorkingOTMin.ToString)), "0.00"))


                        If Val(Rt!FNLateMMin.ToString) <= 15 And (Val(Rt!FNH01.ToString) > 0) Then
                            _totalCal = _totalCal + _H01Qty
                        End If

                        If (Val(Rt!FNH02.ToString) > 0) Then
                            _totalCal = _totalCal + _H02Qty
                        End If

                        If (Val(Rt!FNH03.ToString) > 0) Then
                            _totalCal = _totalCal + _H03Qty
                        End If

                        If (Val(Rt!FNH04.ToString) > 0) And Val(Rt!FNRetireMMin.ToString) <= 0 Then
                            _totalCal = _totalCal + _H04Qty
                        End If

                        If (Val(Rt!FNH05.ToString) > 0) And Val(Rt!FNLateAfMin.ToString) <= 5 Then
                            _totalCal = _totalCal + _H05Qty
                        End If

                        If (Val(Rt!FNH06.ToString) > 0) Then
                            _totalCal = _totalCal + _H06Qty
                        End If

                        If (Val(Rt!FNH07.ToString) > 0) Then
                            _totalCal = _totalCal + _H07Qty
                        End If

                        If (Val(Rt!FNH08.ToString) > 0) And Val(Rt!FNRetireAfMin.ToString) <= 0 Then
                            _totalCal = _totalCal + _H08Qty
                        End If

                        If (Val(Rt!FNH09.ToString) > 0) And Val(Rt!FNLateOtMin.ToString) <= 5 Then
                            _totalCal = _totalCal + _H09Qty
                        End If

                        If _FNTotalOTWorkingMin > 60 And _FNTotalOTWorkingMin <= 120 Then
                            If (Val(Rt!FNH10.ToString) > 0) And Val(Rt!FNRetireOtMin.ToString) <= 0 Then
                                _totalCal = _totalCal + _H10Qty
                            End If
                        Else
                            If (Val(Rt!FNH10.ToString) > 0) Then
                                _totalCal = _totalCal + _H10Qty
                            End If
                        End If

                        If _FNTotalOTWorkingMin > 120 And _FNTotalOTWorkingMin <= 180 Then
                            If (Val(Rt!FNH11.ToString) > 0) And Val(Rt!FNRetireOtMin.ToString) <= 0 Then
                                _totalCal = _totalCal + _H11Qty
                            End If
                        Else
                            If (Val(Rt!FNH11.ToString) > 0) Then
                                _totalCal = _totalCal + _H11Qty
                            End If
                        End If

                        If _FNTotalOTWorkingMin > 180 And _FNTotalOTWorkingMin <= 240 Then
                            If (Val(Rt!FNH12.ToString) > 0) And Val(Rt!FNRetireOtMin.ToString) <= 0 Then
                                _totalCal = _totalCal + _H12Qty
                            End If
                        Else
                            If (Val(Rt!FNH12.ToString) > 0) Then
                                _totalCal = _totalCal + _H12Qty
                            End If

                        End If

                        If _FNTotalOTWorkingMin > 240 And _FNTotalOTWorkingMin <= 300 Then
                            If (Val(Rt!FNH13.ToString) > 0) And Val(Rt!FNRetireOtMin.ToString) <= 0 Then
                                _totalCal = _totalCal + _H13Qty
                            End If
                        Else
                            If (Val(Rt!FNH13.ToString) > 0) Then
                                _totalCal = _totalCal + _H13Qty
                            End If
                        End If

                        _EmpDedeuctAmt = (_totalCal * _PriceCost)
                        _EmpDedeuctNetAmt = Double.Parse(Format(_EmpDedeuctAmt / _CountEmpIncentive, "0.00"))

                        _DeductAmt = _DeductAmt + _EmpDedeuctNetAmt

                        _Qry = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTIncentive_Emp_Deduct ("
                        _Qry &= vbCrLf & "  FTInsUser, FDInsDate, FTInsTime, FTCalDate, FNHSysUnitSectId, FNHSysEmpID"
                        _Qry &= vbCrLf & ", FNQuantity, FNPrice, FNAmt, FNTotalEmp, FNEmpAmt"
                        _Qry &= vbCrLf & ")"
                        _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & ""
                        _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & ""
                        _Qry &= vbCrLf & " ,'" & _CalDate & "'   "
                        _Qry &= vbCrLf & " ," & Integer.Parse(Val(R!FNHSysUnitSectId.ToString)) & ""
                        _Qry &= vbCrLf & " ," & Integer.Parse(Val(Rt!FNHSysEmpID.ToString)) & ""
                        _Qry &= vbCrLf & " ," & _totalCal & ""
                        _Qry &= vbCrLf & " ," & _PriceCost & ""
                        _Qry &= vbCrLf & " ," & _EmpDedeuctAmt & ""
                        _Qry &= vbCrLf & " ," & _CountEmpIncentive & ""
                        _Qry &= vbCrLf & " ," & _EmpDedeuctNetAmt & ""


                        HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

                        _Qry = " UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTIncentive_Emp "
                        _Qry &= vbCrLf & "  SET  FNAmtOldIncentive=" & _EmpDedeuctNetAmt & ""
                        _Qry &= vbCrLf & "  , FNAmtOTOldIncentive=" & _FNAmtOT & ""
                        _Qry &= vbCrLf & "  , FNNetAmtOldIncentive=" & (_EmpDedeuctNetAmt + _FNQAValue) & ""
                        _Qry &= vbCrLf & "   ,FNQAValue=" & _FNQAValue & " "
                        _Qry &= vbCrLf & "   ,FNAmtNewIncentive = " & _EmpDedeuctNetAmt & """"
                        _Qry &= vbCrLf & "  , FNAmtOTNewIncentive=" & _FNAmtOT & ""
                        _Qry &= vbCrLf & "  , FNNetAmtNewIncentive=" & (_EmpDedeuctNetAmt + _FNQAValue) & ""
                        _Qry &= vbCrLf & "  WHERE FNHSysUnitSectId=" & Integer.Parse(Val(R!FNHSysUnitSectId.ToString)) & ""
                        _Qry &= vbCrLf & "       AND FNHSysEmpID=" & Integer.Parse(Val(Rt!FNHSysEmpID.ToString)) & ""
                        _Qry &= vbCrLf & "       AND FTCalDate='" & _CalDate & "'"

                        HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

                        Rt!FTStateCal = "1"

                    Next

                    For Each Rt As DataRow In _dtemptime.Select(" FNTotalWorkingMin +FNLateMMin <" & _FNTotalWorkingMin & " AND FNLateOtMin<=0 AND FNLateAFMin<=0 AND FTStateCal<>'1'  AND FNTotalWorkingMin>0 ")
                        _totalCal = 0

                        _Salary = Double.Parse(Val(Rt!FNSalary.ToString))
                        _SalaryPerH = Double.Parse(Format(_Salary / 8, "0.00"))
                        _SalaryPerOT = Double.Parse(Format(_SalaryPerH * 1.5, "0.00"))

                        If Rt!FTEmpCode = "91SSA00033" Then
                            Beep()
                        End If
                        If _FixHour > 0 Then

                        End If

                        _FNAmtOT = Double.Parse(Format(((_Salary / 480) * 0.5) * Integer.Parse(Val(Rt!FNWorkingOTMin.ToString)), "0.00"))


                        If Val(Rt!FNLateMMin.ToString) <= 15 And (Val(Rt!FNH01.ToString) > 0) Then
                            _totalCal = _totalCal + _H01Qty
                        End If

                        If (Val(Rt!FNH02.ToString) > 0) Then
                            _totalCal = _totalCal + _H02Qty
                        End If

                        If (Val(Rt!FNH03.ToString) > 0) Then
                            _totalCal = _totalCal + _H03Qty
                        End If

                        If (Val(Rt!FNH04.ToString) > 0) And Val(Rt!FNRetireMMin.ToString) <= 0 Then
                            _totalCal = _totalCal + _H04Qty
                        End If

                        If (Val(Rt!FNH05.ToString) > 0) And Val(Rt!FNLateAfMin.ToString) <= 5 Then
                            _totalCal = _totalCal + _H05Qty
                        End If

                        If (Val(Rt!FNH06.ToString) > 0) Then
                            _totalCal = _totalCal + _H06Qty
                        End If

                        If (Val(Rt!FNH07.ToString) > 0) Then
                            _totalCal = _totalCal + _H07Qty
                        End If

                        If (Val(Rt!FNH08.ToString) > 0) And Val(Rt!FNRetireAfMin.ToString) <= 0 Then
                            _totalCal = _totalCal + _H08Qty
                        End If

                        If (Val(Rt!FNH09.ToString) > 0) And Val(Rt!FNLateOtMin.ToString) <= 5 Then
                            _totalCal = _totalCal + _H09Qty
                        End If

                        If _FNTotalOTWorkingMin > 60 And _FNTotalOTWorkingMin <= 120 Then
                            If (Val(Rt!FNH10.ToString) > 0) And Val(Rt!FNRetireOtMin.ToString) <= 0 Then
                                _totalCal = _totalCal + _H10Qty
                            End If
                        Else
                            If (Val(Rt!FNH10.ToString) > 0) Then
                                _totalCal = _totalCal + _H10Qty
                            End If
                        End If

                        If _FNTotalOTWorkingMin > 120 And _FNTotalOTWorkingMin <= 180 Then
                            If (Val(Rt!FNH11.ToString) > 0) And Val(Rt!FNRetireOtMin.ToString) <= 0 Then
                                _totalCal = _totalCal + _H11Qty
                            End If
                        Else
                            If (Val(Rt!FNH11.ToString) > 0) Then
                                _totalCal = _totalCal + _H11Qty
                            End If
                        End If

                        If _FNTotalOTWorkingMin > 180 And _FNTotalOTWorkingMin <= 240 Then
                            If (Val(Rt!FNH12.ToString) > 0) And Val(Rt!FNRetireOtMin.ToString) <= 0 Then
                                _totalCal = _totalCal + _H12Qty
                            End If
                        Else
                            If (Val(Rt!FNH12.ToString) > 0) Then
                                _totalCal = _totalCal + _H12Qty
                            End If

                        End If

                        If _FNTotalOTWorkingMin > 240 And _FNTotalOTWorkingMin <= 300 Then
                            If (Val(Rt!FNH13.ToString) > 0) And Val(Rt!FNRetireOtMin.ToString) <= 0 Then
                                _totalCal = _totalCal + _H13Qty
                            End If
                        Else
                            If (Val(Rt!FNH13.ToString) > 0) Then
                                _totalCal = _totalCal + _H13Qty
                            End If
                        End If


                        _EmpDedeuctAmt = (_totalCal * _PriceCost)
                        _EmpDedeuctNetAmt = Double.Parse(Format(_EmpDedeuctAmt / _CountEmpIncentive, "0.00"))

                        _DeductAmt = _DeductAmt + _EmpDedeuctNetAmt

                        _Qry = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTIncentive_Emp_Deduct ("
                        _Qry &= vbCrLf & "  FTInsUser, FDInsDate, FTInsTime, FTCalDate, FNHSysUnitSectId, FNHSysEmpID"
                        _Qry &= vbCrLf & ", FNQuantity, FNPrice, FNAmt, FNTotalEmp, FNEmpAmt"
                        _Qry &= vbCrLf & ")"
                        _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & ""
                        _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & ""
                        _Qry &= vbCrLf & " ,'" & _CalDate & "'   "
                        _Qry &= vbCrLf & " ," & Integer.Parse(Val(R!FNHSysUnitSectId.ToString)) & ""
                        _Qry &= vbCrLf & " ," & Integer.Parse(Val(Rt!FNHSysEmpID.ToString)) & ""
                        _Qry &= vbCrLf & " ," & _totalCal & ""
                        _Qry &= vbCrLf & " ," & _PriceCost & ""
                        _Qry &= vbCrLf & " ," & _EmpDedeuctAmt & ""
                        _Qry &= vbCrLf & " ," & _CountEmpIncentive & ""
                        _Qry &= vbCrLf & " ," & _EmpDedeuctNetAmt & ""


                        HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

                        _Qry = " UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTIncentive_Emp "
                        _Qry &= vbCrLf & "  SET  FNAmtOldIncentive=" & _EmpDedeuctNetAmt & ""
                        _Qry &= vbCrLf & "  , FNAmtOTOldIncentive=" & _FNAmtOT & ""
                        _Qry &= vbCrLf & "  , FNNetAmtOldIncentive=" & (_EmpDedeuctNetAmt + _FNQAValue) & ""
                        _Qry &= vbCrLf & "   ,FNQAValue=" & _FNQAValue & " "
                        _Qry &= vbCrLf & "   ,FNAmtNewIncentive = " & _EmpDedeuctNetAmt & ""
                        _Qry &= vbCrLf & "  , FNAmtOTNewIncentive=" & _FNAmtOT & ""
                        _Qry &= vbCrLf & "  , FNNetAmtNewIncentive=" & (_EmpDedeuctNetAmt + _FNQAValue) & ""
                        _Qry &= vbCrLf & "  WHERE FNHSysUnitSectId=" & Integer.Parse(Val(R!FNHSysUnitSectId.ToString)) & ""
                        _Qry &= vbCrLf & "       AND FNHSysEmpID=" & Integer.Parse(Val(Rt!FNHSysEmpID.ToString)) & ""
                        _Qry &= vbCrLf & "       AND FTCalDate='" & _CalDate & "'"

                        HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

                        Rt!FTStateCal = "1"

                    Next

                    '--------คำนวณคนที่มาทพงานไม่ครบ---------------

                    _TotalTime = 0
                    _TotalTimeOT = 0

                    For Each Rt As DataRow In _dtemptime.Rows

                        _TotalTime = _TotalTime + Integer.Parse(Val(Rt!FNWorkingMin.ToString))
                        _TotalTimeOT = _TotalTimeOT + Integer.Parse(Val(Rt!FNWorkingOTMin.ToString))

                    Next

                    _CountEmpIncentive = _dtemptime.Select("FTStateCal<>'1'  AND FNTotalWorkingMin>0").Length

                    _LineIncentiveAmt = _LineIncentiveAmt - _DeductAmt

                    If _CountEmpIncentive > 0 Then
                        _EmpIncentiveAmt = Double.Parse(Format(_LineIncentiveAmt / _CountEmpIncentive, "0.00"))
                    Else
                        _EmpIncentiveAmt = 0
                    End If

                    For Each Rt As DataRow In _dtemptime.Select("FTStateCal<>'1'  AND FNTotalWorkingMin>0 ")

                        _Salary = Double.Parse(Val(Rt!FNSalary.ToString))
                        _SalaryPerH = Double.Parse(Format(_Salary / 8, "0.00"))
                        _SalaryPerOT = Double.Parse(Format(_SalaryPerH * 1.5, "0.00"))

                        If _FixHour > 0 Then
                        End If

                        _FNAmtOT = Double.Parse(Format(((_Salary / 480) * 0.5) * Integer.Parse(Val(Rt!FNWorkingOTMin.ToString)), "0.00"))

                        _FNNetAmt = _EmpIncentiveAmt + _FNAmtOT

                        _Qry = " UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTIncentive_Emp "
                        _Qry &= vbCrLf & "  SET  FNAmtOldIncentive=" & _EmpIncentiveAmt & ""
                        _Qry &= vbCrLf & "  , FNAmtOTOldIncentive=" & _FNAmtOT & ""
                        _Qry &= vbCrLf & "  , FNNetAmtOldIncentive=" & (_FNNetAmt + _FNQAValue) & ""
                        _Qry &= vbCrLf & "   ,FNQAValue=" & _FNQAValue & " "
                        _Qry &= vbCrLf & "   WHERE FNHSysUnitSectId=" & Integer.Parse(Val(R!FNHSysUnitSectId.ToString)) & ""
                        _Qry &= vbCrLf & "       AND FNHSysEmpID=" & Integer.Parse(Val(Rt!FNHSysEmpID.ToString)) & ""
                        _Qry &= vbCrLf & "       AND FTCalDate='" & _CalDate & "'"

                        HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

                    Next

                    Select Case Val(R!FTIncentiveTypeIdx.ToString)
                        Case 1, 2, 3

                            Dim _TotalTarget As Integer = 0
                            Dim _TotalHourTarget As Integer = 0
                            Dim _TotalTargetPerHour As Integer = 0
                            Dim _dttime As DataTable
                            Dim _TimeServer As String = ""
                            Dim _dttimeplan As DataTable
                            Dim _FNMoneyPackage As Double = 0
                            Dim _FNPercentPackage As Double = 0
                            Dim _FNQuantityPackage As Integer = 0

                            _Qry = "SELECT TOP 1 FNTarget,ISNULL(FTWorkTime,'') As FTWorkTime,FNTargetPerHour,ISNULL(FNMoneyPackage,0) AS FNMoneyPackage,ISNULL(FNPercentPackage,0) AS FNPercentPackage "
                            _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODLCDConfigTarget AS T WITH(NOLOCK)"
                            _Qry &= vbCrLf & "  WHERE FNHSysUnitSectId=" & Integer.Parse(Val(R!FNHSysUnitSectId.ToString)) & ""
                            _Qry &= vbCrLf & "  AND FDSDate <='" & _CalDate & "' AND  FDEDate>='" & _CalDate & "'  "
                            _dttimeplan = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

                            For Each Rxpt As DataRow In _dttimeplan.Rows
                                _TotalTarget = Integer.Parse(Val(Rxpt!FNTarget.ToString))
                                _TotalHourTarget = Integer.Parse(Val(Rxpt!FNTargetPerHour.ToString))

                                _FNMoneyPackage = Double.Parse(Val(Rxpt!FNMoneyPackage.ToString))
                                _FNPercentPackage = Double.Parse(Val(Rxpt!FNPercentPackage.ToString))

                                'If R!FTWorkTime.ToString <> "" Then

                                '    Try
                                '        _TimeWorlPlanMinute = ((Val(R!FTWorkTime.ToString.Split(":")(0)) * 60) + (Val(R!FTWorkTime.ToString.Split(":")(1))))
                                '    Catch ex As Exception

                                '    End Try

                                '    If _TotalHourTarget > 0 Then
                                '        _TotalTarget = ((Val(R!FTWorkTime.ToString.Split(":")(0)) * _TotalHourTarget)) + Integer.Parse(((Val(R!FTWorkTime.ToString.Split(":")(1)) * (_TotalHourTarget / 60.0))))
                                '    End If

                                'Else
                                '    Me.olbtime1.Text = "8"
                                '    _TimeWorlPlanMinute = 480

                                '    If _TotalHourTarget > 0 Then
                                '        _TotalTarget = ((8 * _TotalHourTarget))
                                '    End If

                                'End If

                                Exit For
                            Next

                            _dttimeplan.Dispose()
                            Dim _OTRequest As Integer = 0

                            For Each Rt As DataRow In _dtemptime.Select("FTStateCal<>'1'", "FNOTRequestMin")
                                _OTRequest = Integer.Parse(Val(Rt!FNOTRequestMin.ToString))
                            Next

                            Dim _dtPrice As DataTable
                            _Qry = "  Select FNLVSeq"
                            _Qry &= vbCrLf & " , FNStartEff"
                            _Qry &= vbCrLf & "   , FNEndEff"
                            _Qry &= vbCrLf & "   , FNPriceMultiple"
                            _Qry &= vbCrLf & "  ," & _PriceCost & " AS FNPrice"
                            _Qry &= vbCrLf & "  , 0 AS FNTargetQty"
                            _Qry &= vbCrLf & "  , 0 AS FNTargetChkQty"
                            _Qry &= vbCrLf & "  , 0 AS FNActQty"
                            _Qry &= vbCrLf & "  , 0 AS FNActBalQty"
                            _Qry &= vbCrLf & "  , 0.000 AS FNPriceMul"
                            _Qry &= vbCrLf & "  , 0.000 AS FNAmount"
                            _Qry &= vbCrLf & "  , '0' AS FTStateMax"
                            _Qry &= vbCrLf & "  , 0.000 AS FNAmountMax"
                            _Qry &= vbCrLf & "    FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TPRODMEFFLevel AS P WITH(NOLOCK)"
                            _Qry &= vbCrLf & "  ORDER BY FNLVSeq"
                            _dtPrice = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MASTER)

                            _FNSam = 0
                            If _dtPrice.Rows.Count > 0 And _FNSam > 0 Then
                                Dim _TotalTGQty As Integer = (((480 + _OTRequest) * _TotalCountEmp) / _FNSam)
                                Dim _TotalBFQty As Integer = 0
                                Dim _TotalQtyG As Integer = 0
                                Dim _TQty As Integer = 0
                                Dim _RowCount As Integer = _dtPrice.Rows.Count
                                Dim _RowIdx As Integer = 0


                                If _FNPercentPackage > 0 Then
                                    _FNQuantityPackage = (_TotalTGQty * _FNPercentPackage) / 100
                                End If

                                For Each Rxp As DataRow In _dtPrice.Select("FNLVSeq>=0", "FNLVSeq")

                                    _RowIdx = _RowIdx + 1

                                    If _RowIdx = _RowCount Then
                                        _TQty = _TotalBFQty + 1
                                    Else
                                        _TQty = ((_TotalTGQty * Val(Rxp!FNEndEff.ToString) / 100))
                                    End If

                                    Rxp!FNTargetQty = _TQty
                                    If _RowIdx = _RowCount Then
                                        Rxp!FNTargetChkQty = _TQty
                                        Rxp!FNActBalQty = _TQty
                                        Rxp!FTStateMax = "1"
                                    Else
                                        Rxp!FNTargetChkQty = _TQty - _TotalBFQty
                                        Rxp!FNActBalQty = _TQty - _TotalBFQty
                                    End If

                                    Rxp!FNPriceMul = CDbl(Format(Val(Rxp!FNPriceMultiple.ToString) * Val(Rxp!FNPrice.ToString), "0.00"))

                                    _TotalBFQty = _TQty
                                Next
                                Dim _AmtMax As Double = 0

                                _RowIdx = 0
                                _TotalBFQty = 0
                                For Each Rxp As DataRow In _dtPrice.Select("FNLVSeq>=0", "FNLVSeq")
                                    _RowIdx = _RowIdx + 1

                                    If _RowIdx = _RowCount Then
                                        _AmtMax = _AmtMax + CDbl(Format(Val(Rxp!FNPriceMul.ToString) * (Val(Rxp!FNActBalQty.ToString) - _TotalBFQty), "0.00"))

                                    Else
                                        _AmtMax = _AmtMax + CDbl(Format(Val(Rxp!FNPriceMul.ToString) * Val(Rxp!FNActBalQty.ToString), "0.00"))

                                    End If

                                    Rxp!FNAmountMax = CDbl(Format(_AmtMax, "0.00"))
                                    _TotalBFQty = Val(Rxp!FNTargetQty.ToString)
                                Next
                            End If

                            If _TotalProd > 0 Then
                                Dim _TotalActualProd As Integer = _TotalProd
                                Dim _TotalActualQty As Integer = 0

                                Dim _RowCount As Integer = _dtPrice.Rows.Count
                                Dim _RowIdx As Integer = 0
                                For Each Rxp As DataRow In _dtPrice.Select("FNLVSeq>=0", "FNLVSeq")

                                    _RowIdx = _RowIdx + 1

                                    If _RowIdx = _RowCount Then
                                        Rxp!FNActQty = _TotalActualProd
                                        Rxp!FNActBalQty = 0
                                    Else
                                        If Val(Rxp!FNActBalQty) > _TotalActualProd Then
                                            _TotalActualQty = _TotalActualProd
                                            Rxp!FNActQty = _TotalActualQty
                                            Rxp!FNActBalQty = (Val(Rxp!FNActBalQty) - _TotalActualQty)
                                        Else
                                            _TotalActualQty = Val(Rxp!FNActBalQty)
                                            Rxp!FNActQty = _TotalActualQty
                                            Rxp!FNActBalQty = 0
                                        End If
                                    End If

                                    _TotalActualProd = _TotalActualProd - _TotalActualQty


                                    If _TotalActualProd <= 0 Then
                                        Exit For
                                    End If
                                Next

                                For Each Rxp As DataRow In _dtPrice.Select("FTStateMax='1' AND FNActBalQty >0", "FNLVSeq")

                                    If _TotalProd >= Val(Rxp!FNActBalQty) Then
                                        Rxp!FNActBalQty = 0
                                    Else

                                        Rxp!FNActBalQty = (Val(Rxp!FNActBalQty) - _TotalProd)
                                    End If
                                Next

                                Dim _Amount As Double = 0
                                Dim _MaxSeq As Integer = 0
                                For Each Rxp As DataRow In _dtPrice.Select("FNActQty >0", "FNLVSeq")

                                    _Amount = _Amount + CDbl(Format((CDbl(Format(Val(Rxp!FNPriceMul.ToString) * Val(Rxp!FNActQty.ToString), "0.00"))), "0.00"))

                                    _MaxSeq = Val(Rxp!FNLVSeq)
                                    Rxp!FNAmount = _Amount

                                Next

                                If _Amount > 0 And _MaxSeq > 0 Then
                                    If _dtPrice.Select("FNLVSeq =" & _MaxSeq & " AND FNActBalQty>0").Length <= 0 Then
                                        For Each Rxp As DataRow In _dtPrice.Select("FNLVSeq =" & _MaxSeq + 1 & " AND FNTargetChkQty=FNActBalQty AND FTStateMax<>'1'", "FNLVSeq")
                                            Rxp!FNAmount = _Amount
                                            Exit For
                                        Next
                                    End If

                                End If

                            End If

                            _LineAmt = 0

                            If _dtPrice.Select("FNActBalQty >0", "FNLVSeq").Length > 0 Then
                                For Each Rxp As DataRow In _dtPrice.Select("FNActBalQty >0", "FNLVSeq")
                                    _LineAmt = Val(Rxp!FNAmount.ToString)
                                    Exit For
                                Next
                            Else
                                For Each Rxp As DataRow In _dtPrice.Select("FTStateMax='1'", "FNLVSeq")
                                    _LineAmt = Val(Rxp!FNAmount.ToString)
                                    Exit For
                                Next
                            End If

                            Select Case Val(R!FTIncentiveTypeIdx.ToString)
                                Case 3
                                    If _FNQuantityPackage > 0 And _TotalProd > 0 Then
                                        If _FNQuantityPackage <= _TotalProd And _LineAmt < _FNMoneyPackage Then
                                            _LineAmt = _FNMoneyPackage
                                        End If
                                    End If
                            End Select

                            Dim _FNSeq As Integer = 0

                            For Each Rxp As DataRow In _dtPrice.Select("FNTargetChkQty >0", "FNLVSeq")

                                _FNSeq = _FNSeq + 1

                                _Qry = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTIncentive_Level ("
                                _Qry &= vbCrLf & "  FTInsUser, FDInsDate, FTInsTime, FTCalDate, FNHSysUnitSectId"
                                _Qry &= vbCrLf & ", FNlevelSeq, FNNetPrice, FNQuantity, FNNetAmt,FNPriceMultiple"
                                _Qry &= vbCrLf & "  )"
                                _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                                _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & ""
                                _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & ""
                                _Qry &= vbCrLf & " ,'" & _CalDate & "'   "
                                _Qry &= vbCrLf & " ," & Integer.Parse(Val(R!FNHSysUnitSectId.ToString)) & ""
                                _Qry &= vbCrLf & " ," & _FNSeq & ""
                                _Qry &= vbCrLf & " ," & Val(Rxp!FNPriceMul.ToString) & ""
                                _Qry &= vbCrLf & " ," & Val(Rxp!FNActQty.ToString) & ""
                                _Qry &= vbCrLf & " ," & CDbl(Format((CDbl(Format(Val(Rxp!FNPriceMul.ToString) * Val(Rxp!FNActQty.ToString), "0.00"))), "0.00")) & ""
                                _Qry &= vbCrLf & " ," & Val(Rxp!FNPriceMul.ToString) & ""
                                HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)
                            Next

                            If _LineAmt > 0 Then

                                _LineIncentiveAmt = _LineAmt - _DeductAmt

                                _CountEmpIncentive = _dtemptime.Select("FTStateCal<>'1'  AND FNTotalWorkingMin>0").Length

                                If _CountEmpIncentive > 0 Then
                                    _EmpIncentiveAmt = Double.Parse(Format(_LineIncentiveAmt / _CountEmpIncentive, "0.00"))
                                Else
                                    _EmpIncentiveAmt = 0
                                End If

                                For Each Rt As DataRow In _dtemptime.Select("FTStateCal<>'1'  AND FNTotalWorkingMin>0")

                                    _Salary = Double.Parse(Val(Rt!FNSalary.ToString))
                                    _SalaryPerH = Double.Parse(Format(_Salary / 8, "0.00"))
                                    _SalaryPerOT = Double.Parse(Format(_SalaryPerH * 1.5, "0.00"))

                                    _FNAmtOT = Double.Parse(Format(((_Salary / 480) * 0.5) * Integer.Parse(Val(Rt!FNWorkingOTMin.ToString)), "0.00"))

                                    _FNNetAmt = _EmpIncentiveAmt + _FNAmtOT

                                    _Qry = " UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTIncentive_Emp "
                                    _Qry &= vbCrLf & "  SET  FNAmtNewIncentive=" & _EmpIncentiveAmt & ""
                                    _Qry &= vbCrLf & "  , FNAmtOTNewIncentive=" & _FNAmtOT & ""
                                    _Qry &= vbCrLf & "  , FNNetAmtNewIncentive=" & (_FNNetAmt + _FNQAValue) & ""
                                    _Qry &= vbCrLf & "   ,FNQAValue=" & _FNQAValue & " "
                                    _Qry &= vbCrLf & "  WHERE FNHSysUnitSectId=" & Integer.Parse(Val(R!FNHSysUnitSectId.ToString)) & ""
                                    _Qry &= vbCrLf & "       AND FNHSysEmpID=" & Integer.Parse(Val(Rt!FNHSysEmpID.ToString)) & ""
                                    _Qry &= vbCrLf & "       AND FTCalDate='" & _CalDate & "'"

                                    HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

                                Next

                            End If

                    End Select

                End If

                For Each Rt As DataRow In _dtemptime.Rows

                    _Qry = " DELETE FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTWageDaily "
                    _Qry &= vbCrLf & "  WHERE  FNHSysEmpID=" & Integer.Parse(Val(Rt!FNHSysEmpID.ToString)) & ""
                    _Qry &= vbCrLf & "    AND  FTDateTrans='" & _CalDate & "'"
                    HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

                    _Qry = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTWageDaily ( "
                    _Qry &= vbCrLf & "   FTInsUser, FTInsDate, FTInsTime, FNHSysEmpID, FTDateTrans"
                    _Qry &= vbCrLf & " , FNAmtNormal, FNAmtOT, FNNetAmt, FNProNormal, FNProOT"
                    _Qry &= vbCrLf & " , FNProOther, FNNetProAmt,FNQAAmt"
                    _Qry &= vbCrLf & " )"

                    _Qry &= vbCrLf & " SELECT "
                    _Qry &= vbCrLf & "   FTInsUser, FTInsDate, FTInsTime, FNHSysEmpID, FTDateTrans"
                    _Qry &= vbCrLf & " , FNAmtNormal, FNAmtOT, (FNAmtNormal+FNAmtOT) AS  FNNetAmt, FNProNormal, FNProOT"
                    _Qry &= vbCrLf & " , FNProOther, FNNetProAmt,FNQAAmt"
                    _Qry &= vbCrLf & " FROM (Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' AS FTInsUser"
                    _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & " AS FTInsDate"
                    _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & " AS FTInsTime"

                    _Qry &= vbCrLf & " ," & Integer.Parse(Val(Rt!FNHSysEmpID.ToString)) & " AS FNHSysEmpID"
                    _Qry &= vbCrLf & " ,'" & _CalDate & "' ASFTDateTrans   "
                    _Qry &= vbCrLf & "  ,MAX(FNAmtNormal-(FNAmtHour01Insurance+FNAmtHour02Insurance+ FNAmtHour03Insurance+ FNAmtHour04Insurance+FNAmtHour05Insurance+ FNAmtHour07Insurance+FNAmtHour06Insurance+FNAmtHour08Insurance) ) AS FNAmtNormal"
                    _Qry &= vbCrLf & " , MAX(FNAmtOT-(FNAmtHour09Insurance + FNAmtHour10Insurance + FNAmtHour11Insurance + FNAmtHour12Insurance + FNAmtHour13Insurance)) AS FNAmtOT"
                    _Qry &= vbCrLf & " , MAX(FNNetAmt) AS FNNetAmt"

                    Select Case Val(R!FTIncentiveTypeIdx.ToString)
                        Case 1, 2, 3, 4
                            _Qry &= vbCrLf & ", Sum(FNAmtNewIncentive) AS FNProNormal"
                            _Qry &= vbCrLf & ", Sum(FNAmtOTNewIncentive) AS FNProOT,0 AS FNProOther"
                            _Qry &= vbCrLf & ", Sum(FNNetAmtNewIncentive) AS FNNetProAmt "
                        Case Else
                            _Qry &= vbCrLf & ", Sum(FNAmtOldIncentive) AS FNProNormal"
                            _Qry &= vbCrLf & ", Sum(FNAmtOTOldIncentive) AS FNProOT,0 AS FNProOther"
                            _Qry &= vbCrLf & ", Sum(FNNetAmtOldIncentive) AS FNNetProAmt"
                    End Select


                    _Qry &= vbCrLf & ", Sum(FNQAValue) AS FNQAAmt"
                    _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTIncentive_Emp  AS X WITH(NOLOCK) "
                    _Qry &= vbCrLf & "  WHERE  FNHSysEmpID=" & Integer.Parse(Val(Rt!FNHSysEmpID.ToString)) & ""
                    _Qry &= vbCrLf & "       AND FTCalDate='" & _CalDate & "') AS X"

                    HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

                Next

                _dtemptime.Dispose()

            Next

            _dt.Dispose()
        Catch ex As Exception
        End Try
        Return True
    End Function

#End Region



#Region "Process Calculate"


    Private Function CalculateSewingHour(Spls As HI.TL.SplashScreen) As Boolean

        Dim _dt As DataTable
        Dim _dtemptime As DataTable
        Dim dtsam As DataTable
        Dim dtPriceMiltiple As DataTable
        Dim _Qry As String = ""
        Dim _LineAmt As Double = 0

        Dim _CountEmp As Integer = 0
        Dim _CountEmpIncentive As Integer = 0
        Dim _TotalTime As Integer = 0
        Dim _TotalTimeOT As Integer = 0
        Dim _TotalTimeHR As Integer = 0
        Dim _TotalTimeOTHR As Integer = 0

        Dim _FullTotalTimeHR As Integer = 0
        Dim _FullTotalTimeOTHR As Integer = 0


        Dim _Salary As Double = 0
        Dim _SalaryPerH As Double = 0
        Dim _SalaryPerOT As Double = 0

        Dim _SumSam As Double = 0
        Dim _CostPerMin As Double = 0
        Dim _SumPrice As Double = 0
        Dim _CalQty As Integer = 0
        Dim _SumCalQty As Integer = 0
        Dim _Qty As Integer = 0
        Dim _FNSeq As Integer = 0
        Dim _IncentiveAmt As Double = 0
        Dim _EmpIncentiveAmt As Double = 0
        Dim _CalDate As String = ""
        Dim _AllEmp As String = ""
        Dim _FNAmtNormal As Double = 0
        Dim _FNAmtOT As Double = 0
        Dim _FNNetAmt As Double = 0
        Dim _TotalTimeMin As Integer = 0
        Dim _StateSaveDetail As Boolean = False

        Dim _StateCal As Boolean = False

        Dim _SDate As String = HI.UL.ULDate.ConvertEnDB(FTStartDate.Text)
        Dim _EDate As String = HI.UL.ULDate.ConvertEnDB(FTEndDate.Text)
        Dim _SSewDate As String = ""
        Dim _ESewDate As String = ""

        With CType(ogc.DataSource, DataTable)
            .AcceptChanges()
            _dt = .Copy()
        End With

        Dim dtline As DataTable

        Try

            If _dt.Rows.Count > 0 Then
                _Qry = " exec [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.SP_SAMPLEROOM_INCENTIVE_LOAD_SPLIT '" & _SDate & "','" & _EDate & "' "
                dtline = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_SAMPLE)
            End If






            Do While _SDate <= _EDate

                _StateCal = False

                For Each R As DataRow In _dt.Select("FTSelect='1' AND FTStateFinishDateOrg='" & _SDate & "' ", "FTStateFinishDateOrg,FTSMPOrderNo,FTTeam")

                    _StateCal = True

                    _SSewDate = R!FTStartSewDate.ToString
                    _ESewDate = R!FTFinishSewDate.ToString

                    _Qry = " SELECT FNSeq, FNStartQty, FNEndQty, FNMultiple"
                    _Qry &= vbCrLf & " From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPPriceMultiple AS X WITH(NOLOCK)"
                    _Qry &= vbCrLf & " WHERE FNEmpTeam=" & Val(R!FNEmpTeam.ToString) & " "

                    dtPriceMiltiple = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_SAMPLE)


                    'If "91SMP-230100082" = R!FTSMPOrderNo.ToString Then
                    '    MsgBox("91SMP-230100082")
                    'End If

                    _StateSaveDetail = False
                    _dtemptime = New DataTable

                    _CalDate = R!FTStateFinishDateOrg
                    _AllEmp = R!FNHSysEmpID.ToString
                    _Qty = Val(R!FNQuantity.ToString)


                    If _AllEmp <> "" Then

                        _CountEmp = 0



                        Do While _SSewDate <= _ESewDate


                            For Each Emp As String In _AllEmp.Split(",")

                                If _CountEmp <= 0 Then

                                    _dtemptime = GetEmpTime(_SSewDate, Val(Emp)).Copy()

                                Else
                                    _dtemptime.Merge(GetEmpTime(_SSewDate, Val(Emp)).Copy())
                                End If

                                _CountEmp = _CountEmp + 1

                            Next



                            _SSewDate = HI.UL.ULDate.ConvertEnDB(HI.UL.ULDate.AddDay(_SSewDate, 1))
                        Loop

                        ''best 20221018 edit cal start to end       change to  check date qc  
                        'For Each Emp As String In _AllEmp.Split(",")

                        '    If _CountEmp <= 0 Then

                        '        _dtemptime = GetEmpTime(_CalDate, Val(Emp)).Copy()

                        '    Else
                        '        _dtemptime.Merge(GetEmpTime(_CalDate, Val(Emp)).Copy())
                        '    End If

                        '    _CountEmp = _CountEmp + 1

                        'Next

                        If _dtemptime.Rows.Count > 0 Then

                            _Qry = "insert into [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPCalculate_Detail ("
                            _Qry &= vbCrLf & " FTInsUser, FDInsDate, FTInsTime, FTCalculateDate, FTSMPOrderNo"
                            _Qry &= vbCrLf & ", FTTeam, FNSeq, FNMockUpType, FNSam, FNCostPerMin, FNPrice, FNQuantity"
                            _Qry &= vbCrLf & ")"
                            _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                            _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & " "
                            _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & " "
                            _Qry &= vbCrLf & " ,'" & _CalDate & "' "
                            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTSMPOrderNo.ToString) & "','" & HI.UL.ULF.rpQuoted(R!FTTeam.ToString) & "', FNSeq, FNMockUpType, FNSam, FNCostPerMin, FNPrice," & _Qty & " "
                            _Qry &= vbCrLf & " From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPSam AS X WITH(NOLOCK)"
                            _Qry &= vbCrLf & "  Where (FTSMPOrderNo = N'" & HI.UL.ULF.rpQuoted(R!FTSMPOrderNo.ToString) & "')"

                            HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_SAMPLE)

                            _Qry = " SELECT SUM(FNSam) AS FNSam,SUM(FNPrice) AS FNPrice"
                            _Qry &= vbCrLf & " From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPCalculate_Detail AS X WITH(NOLOCK)"
                            _Qry &= vbCrLf & "  Where (FTSMPOrderNo = N'" & HI.UL.ULF.rpQuoted(R!FTSMPOrderNo.ToString) & "')"
                            _Qry &= vbCrLf & "  AND (FTTeam = N'" & HI.UL.ULF.rpQuoted(R!FTTeam.ToString) & "')"

                            dtsam = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_SAMPLE)


                            _SumSam = 0
                            _SumPrice = 0
                            _CostPerMin = 0

                            For Each Rxs As DataRow In dtsam.Rows

                                _SumSam = Val(Rxs!FNSam.ToString)
                                _SumPrice = Val(Rxs!FNPrice.ToString)

                                Exit For
                            Next

                            If _SumSam > 0 Then

                                _CostPerMin = CDbl(Format(_SumPrice / _SumSam, "0.0000"))

                            End If


                            _FNSeq = 0
                            _SumCalQty = 0

                            If dtPriceMiltiple.Select("FNStartQty<=" & _Qty & " ").Length > 0 Then
                                For Each Rxs As DataRow In dtPriceMiltiple.Select("FNStartQty<=" & _Qty & " ", "FNSeq")
                                    _FNSeq = _FNSeq + 1

                                    _CalQty = 0
                                    If Val(Rxs!FNSeq.ToString) = 1 Then
                                        If Val(Rxs!FNEndQty.ToString) <= _Qty Then
                                            _CalQty = Val(Rxs!FNEndQty.ToString)
                                        Else
                                            _CalQty = _Qty
                                        End If
                                    Else
                                        If Val(Rxs!FNEndQty.ToString) <= _Qty Then

                                            If Val(Rxs!FNStartQty.ToString) = 0 Then
                                                _CalQty = Val(Rxs!FNEndQty.ToString) - Val(Rxs!FNStartQty.ToString)
                                            Else
                                                _CalQty = (Val(Rxs!FNEndQty.ToString) - Val(Rxs!FNStartQty.ToString)) + 1
                                            End If

                                        Else
                                            If Val(Rxs!FNStartQty.ToString) = 0 Then
                                                _CalQty = _Qty - Val(Rxs!FNStartQty.ToString)
                                            Else
                                                _CalQty = (_Qty - Val(Rxs!FNStartQty.ToString)) + 1
                                            End If
                                        End If
                                    End If


                                    _SumCalQty = _SumCalQty + _CalQty

                                    _Qry = "insert into [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPCalculate_PriceMultiple ("
                                    _Qry &= vbCrLf & " FTInsUser, FDInsDate, FTInsTime, FTCalculateDate, FTSMPOrderNo"
                                    _Qry &= vbCrLf & ", FTTeam, FNSeq,FNSam, FNCostPerMin, FNPrice, FNMultiple, FNNetPrice, FNQuantity, FNAmount"
                                    _Qry &= vbCrLf & ")"
                                    _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                                    _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & " "
                                    _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & " "
                                    _Qry &= vbCrLf & " ,'" & _CalDate & "' "
                                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTSMPOrderNo.ToString) & "','" & HI.UL.ULF.rpQuoted(R!FTTeam.ToString) & "', " & _FNSeq & ""
                                    _Qry &= vbCrLf & "," & _SumSam & ""
                                    _Qry &= vbCrLf & "," & _CostPerMin & ""
                                    _Qry &= vbCrLf & "," & _SumPrice & ""
                                    _Qry &= vbCrLf & "," & Val(Rxs!FNMultiple.ToString) & ""
                                    _Qry &= vbCrLf & ",Convert(numeric(18,2)," & _SumPrice * Val(Rxs!FNMultiple.ToString) & ")"
                                    _Qry &= vbCrLf & "," & _CalQty & ""
                                    _Qry &= vbCrLf & ",Convert(numeric(18,2),Convert(numeric(18,2)," & _SumPrice * Val(Rxs!FNMultiple.ToString) & ") * " & _CalQty & ")"
                                    HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_SAMPLE)

                                    If _SumCalQty >= _Qty Then
                                        Exit For
                                    End If
                                Next

                            Else
                                _Qry = "insert into [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPCalculate_PriceMultiple ("
                                _Qry &= vbCrLf & " FTInsUser, FDInsDate, FTInsTime, FTCalculateDate, FTSMPOrderNo"
                                _Qry &= vbCrLf & ", FTTeam, FNSeq,FNSam, FNCostPerMin, FNPrice, FNMultiple, FNNetPrice, FNQuantity, FNAmount"
                                _Qry &= vbCrLf & ")"
                                _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                                _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & " "
                                _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & " "
                                _Qry &= vbCrLf & " ,'" & _CalDate & "' "
                                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTSMPOrderNo.ToString) & "','" & HI.UL.ULF.rpQuoted(R!FTTeam.ToString) & "', " & _FNSeq & ""
                                _Qry &= vbCrLf & "," & _SumSam & ""
                                _Qry &= vbCrLf & "," & _CostPerMin & ""
                                _Qry &= vbCrLf & "," & _SumPrice & ""
                                _Qry &= vbCrLf & "," & 1 & ""
                                _Qry &= vbCrLf & ",Convert(numeric(18,2)," & _SumPrice * 1 & ")"
                                _Qry &= vbCrLf & "," & _Qty & ""
                                _Qry &= vbCrLf & ",Convert(numeric(18,2),Convert(numeric(18,2)," & _SumPrice * 1 & ") * " & _Qty & ")"
                                HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_SAMPLE)
                            End If


                            _Qry = " SELECT SUM(FNAmount) AS FNAmount"
                            _Qry &= vbCrLf & " From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPCalculate_PriceMultiple AS X WITH(NOLOCK)"
                            _Qry &= vbCrLf & "  Where (FTSMPOrderNo = N'" & HI.UL.ULF.rpQuoted(R!FTSMPOrderNo.ToString) & "')"
                            _Qry &= vbCrLf & "  AND (FTTeam = N'" & HI.UL.ULF.rpQuoted(R!FTTeam.ToString) & "')"

                            _IncentiveAmt = Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_SAMPLE, "0"))

                            _Qry = "insert into [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPCalculate ("
                            _Qry &= vbCrLf & " FTInsUser, FDInsDate, FTInsTime, FTCalculateDate, FTSMPOrderNo, FTTeam, FNQty, FNAmount"
                            _Qry &= vbCrLf & ")"
                            _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                            _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & " "
                            _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & " "
                            _Qry &= vbCrLf & " ,'" & _CalDate & "' "
                            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTSMPOrderNo.ToString) & "','" & HI.UL.ULF.rpQuoted(R!FTTeam.ToString) & "', " & _Qty & "," & _IncentiveAmt & ""

                            HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_SAMPLE)

                            _TotalTime = 0
                            _TotalTimeMin = 0
                            For Each Rt As DataRow In _dtemptime.Rows
                                _TotalTimeMin = _TotalTimeMin + Integer.Parse(Val(Rt!FNTotalWorkingMin.ToString))
                            Next

                            _FNSeq = 0
                            _CountEmp = 0
                            Dim _EmpTotalTimeMin As Integer = 0
                            For Each Emp As String In _AllEmp.Split(",")
                                _FNSeq = _FNSeq + 1
                                _CountEmp = _CountEmp + 1
                                _TotalTime = 0
                                _TotalTimeOT = 0
                                _EmpTotalTimeMin = 0

                                For Each Rt As DataRow In _dtemptime.Select("FNHSysEmpID=" & Val(Emp) & "")
                                    _EmpTotalTimeMin = _EmpTotalTimeMin + Integer.Parse(Val(Rt!FNTotalWorkingMin.ToString))
                                Next


                                _EmpIncentiveAmt = 0

                                If _TotalTimeMin > 0 Then

                                    _EmpIncentiveAmt = CDbl(Format(((_IncentiveAmt * Integer.Parse(_EmpTotalTimeMin)) / _TotalTimeMin), "0.00"))

                                End If

                                _Salary = 0
                                _SalaryPerH = 0
                                _SalaryPerOT = 0
                                _FNAmtNormal = 0
                                _FNAmtOT = 0
                                _FNNetAmt = 0
                                _FullTotalTimeHR = 0
                                _FullTotalTimeOTHR = 0


                                For Each Rt As DataRow In _dtemptime.Select("FNHSysEmpID=" & Val(Emp) & "")

                                    _FNSeq = _FNSeq + 1
                                    _CountEmp = _CountEmp + 1
                                    _TotalTime = _TotalTime + Integer.Parse(Val(Rt!FNWorkingMin.ToString))
                                    _TotalTimeOT = _TotalTimeOT + Integer.Parse(Val(Rt!FNWorkingOTMin.ToString))

                                    _TotalTimeHR = Integer.Parse(Val(Rt!FNTimeMin.ToString))
                                    _TotalTimeOTHR = Integer.Parse(Val(Rt!FNOT1Min.ToString))


                                    _FullTotalTimeHR = 0

                                    _FullTotalTimeHR = _FullTotalTimeHR + Integer.Parse(Val(Rt!FNHRH01.ToString))
                                    _FullTotalTimeHR = _FullTotalTimeHR + Integer.Parse(Val(Rt!FNHRH02.ToString))
                                    _FullTotalTimeHR = _FullTotalTimeHR + Integer.Parse(Val(Rt!FNHRH03.ToString))
                                    _FullTotalTimeHR = _FullTotalTimeHR + Integer.Parse(Val(Rt!FNHRH04.ToString))
                                    _FullTotalTimeHR = _FullTotalTimeHR + Integer.Parse(Val(Rt!FNHRH05.ToString))
                                    _FullTotalTimeHR = _FullTotalTimeHR + Integer.Parse(Val(Rt!FNHRH06.ToString))
                                    _FullTotalTimeHR = _FullTotalTimeHR + Integer.Parse(Val(Rt!FNHRH07.ToString))
                                    _FullTotalTimeHR = _FullTotalTimeHR + Integer.Parse(Val(Rt!FNHRH08.ToString))

                                    _FullTotalTimeOTHR = 0
                                    _FullTotalTimeOTHR = _FullTotalTimeOTHR + Integer.Parse(Val(Rt!FNHRH09.ToString))
                                    _FullTotalTimeOTHR = _FullTotalTimeOTHR + Integer.Parse(Val(Rt!FNHRH10.ToString))
                                    _FullTotalTimeOTHR = _FullTotalTimeOTHR + Integer.Parse(Val(Rt!FNHRH11.ToString))
                                    _FullTotalTimeOTHR = _FullTotalTimeOTHR + Integer.Parse(Val(Rt!FNHRH12.ToString))
                                    _FullTotalTimeOTHR = _FullTotalTimeOTHR + Integer.Parse(Val(Rt!FNHRH13.ToString))




                                    _Salary = Double.Parse(Val(Rt!FNSalary.ToString))
                                    _SalaryPerH = Double.Parse(Format(_Salary / 8, "0.0000"))
                                    _SalaryPerOT = Double.Parse(Format(_SalaryPerH * 1.5, "0.0000"))

                                    If _FullTotalTimeHR >= 480 Then
                                        _FNAmtNormal = _Salary
                                    Else
                                        _FNAmtNormal = Double.Parse(Format((_Salary / 480) * _FullTotalTimeHR, "0.00"))
                                    End If

                                    _FNAmtOT = Double.Parse(Format(((_Salary / 480) * 1.5) * _FullTotalTimeOTHR, "0.00"))

                                    _FNNetAmt = _FNAmtNormal + _FNAmtOT

                                    Exit For
                                Next

                                _Qry = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPCalculate_Emp ("
                                _Qry &= vbCrLf & "  FTInsUser, FDInsDate, FTInsTime,  FTCalculateDate,FTSMPOrderNo, FTTeam, FNSeq, FNHSysEmpID, FNSalary, FNNormalTime, FNOTTime, FNAmtNormal, FNAmtOT, FNNetAmt, FNIncAmount,FNTotalTimHRFull,FNTotalTimeOTHRFull"
                                _Qry &= vbCrLf & ")"
                                _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                                _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & ""
                                _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & ""
                                _Qry &= vbCrLf & " ,'" & _CalDate & "'   "
                                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTSMPOrderNo.ToString) & "','" & HI.UL.ULF.rpQuoted(R!FTTeam.ToString) & "', " & _FNSeq & ""
                                _Qry &= vbCrLf & " ," & Val(Emp) & ""
                                _Qry &= vbCrLf & " ," & _Salary & ""
                                _Qry &= vbCrLf & " ," & _TotalTimeHR & ""
                                _Qry &= vbCrLf & " ," & _TotalTimeOTHR & ""
                                _Qry &= vbCrLf & " ," & _FNAmtNormal & ""
                                _Qry &= vbCrLf & " ," & _FNAmtOT & ""
                                _Qry &= vbCrLf & " ," & _FNNetAmt & ""
                                _Qry &= vbCrLf & " ," & _EmpIncentiveAmt & ""
                                _Qry &= vbCrLf & " ," & _FullTotalTimeHR & ""
                                _Qry &= vbCrLf & " ," & _FullTotalTimeOTHR & ""


                                If HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_SAMPLE) = True Then
                                    _StateSaveDetail = True
                                End If


                            Next



                        End If

                    End If

                    _dtemptime.Dispose()

                    If _StateSaveDetail Then

                        _Qry = " Update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPSampleTeam "
                        _Qry &= vbCrLf & " SET FTStateCal='1',FTStateCalBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        _Qry &= vbCrLf & " ,FTStateCalDate=" & HI.UL.ULDate.FormatDateDB & ""
                        _Qry &= vbCrLf & " ,FTStateCalTime=" & HI.UL.ULDate.FormatTimeDB & ""
                        _Qry &= vbCrLf & " ,FTStateSendApp='0',FTSendAppBy=''"
                        _Qry &= vbCrLf & " ,FTSendAppDate=''"
                        _Qry &= vbCrLf & " ,FTSendAppTime=''"

                        _Qry &= vbCrLf & " ,FTStateApprove='0',FTApproveBy=''"
                        _Qry &= vbCrLf & " ,FTApproveDate=''"
                        _Qry &= vbCrLf & " ,FTApproveTime=''"
                        _Qry &= vbCrLf & " WHERE FTSMPOrderNo='" & HI.UL.ULF.rpQuoted(R!FTSMPOrderNo.ToString) & "' AND FTTeam='" & HI.UL.ULF.rpQuoted(R!FTTeam.ToString) & "' "

                        HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_SAMPLE)

                        _SSewDate = R!FTStartSewDate.ToString
                        _ESewDate = R!FTFinishSewDate.ToString
                        '' For Each Rline As DataRow In dtline.Select("FTStateFinishDateOrg='" & _SDate & "'  AND FTSMPOrderNo='" & HI.UL.ULF.rpQuoted(R!FTSMPOrderNo.ToString) & "' AND  FNHSysEmpID='" & _AllEmp & "' AND FTStartSewDate='" & _SSewDate & "'  AND FTFinishSewDate='" & _ESewDate & "'")

                        For Each Rline As DataRow In dtline.Select(" FTSMPOrderNo='" & HI.UL.ULF.rpQuoted(R!FTSMPOrderNo.ToString) & "' AND  FNHSysEmpID='" & _AllEmp & "' AND FTStartSewDate='" & _SSewDate & "'  AND FTFinishSewDate='" & _ESewDate & "'")

                            If HI.UL.ULF.rpQuoted(R!FTTeam.ToString) <> HI.UL.ULF.rpQuoted(Rline!FTTeam.ToString) Then
                                _Qry = " Update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPSampleTeam "
                                _Qry &= vbCrLf & " SET FTStateCal='1',FTStateCalBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                                _Qry &= vbCrLf & " ,FTStateCalDate=" & HI.UL.ULDate.FormatDateDB & ""
                                _Qry &= vbCrLf & " ,FTStateCalTime=" & HI.UL.ULDate.FormatTimeDB & ""
                                _Qry &= vbCrLf & " ,FTStateSendApp='0',FTSendAppBy=''"
                                _Qry &= vbCrLf & " ,FTSendAppDate=''"
                                _Qry &= vbCrLf & " ,FTSendAppTime=''"

                                _Qry &= vbCrLf & " ,FTStateApprove='0',FTApproveBy=''"
                                _Qry &= vbCrLf & " ,FTApproveDate=''"
                                _Qry &= vbCrLf & " ,FTApproveTime=''"
                                _Qry &= vbCrLf & " WHERE FTSMPOrderNo='" & HI.UL.ULF.rpQuoted(R!FTSMPOrderNo.ToString) & "' AND FTTeam='" & HI.UL.ULF.rpQuoted(Rline!FTTeam.ToString) & "' "

                                HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_SAMPLE)
                            End If

                        Next




                    End If

                Next


                If _StateCal Then
                    CalculateEmpCut(_SDate)
                End If


                _SDate = HI.UL.ULDate.ConvertEnDB(HI.UL.ULDate.AddDay(_SDate, 1))

            Loop


            _dt.Dispose()
        Catch ex As Exception
        End Try

        dtPriceMiltiple.Dispose()
        Return True
    End Function


    Private Sub CalculateEmpCut(CalDate As String, Optional CutMultiple As Decimal = 10)
        'Dim cmd As String = ""
        'Dim dtemp As New DataTable
        'Dim _dtemptime As New DataTable
        'Dim SewAmt As Decimal = 0
        'Dim CutAmt As Decimal = 0
        'Dim _CountEmp As Integer = 0
        'Dim _TotalTimeMin As Integer = 0
        'Dim _TotalTimeOT As Integer = 0
        'Dim _TotalTimeHR As Integer = 0
        'Dim _TotalTimeOTHR As Integer = 0
        'Dim _Salary As Double = 0
        'Dim _SalaryPerH As Double = 0
        'Dim _SalaryPerOT As Double = 0
        'Dim _TotalTime As Integer = 0
        'Dim _FNSeq As Integer = 0
        'Dim _FNAmtOT As Double = 0
        'Dim _FNAmtNormal As Double = 0
        'Dim _FNNetAmt As Double = 0
        'Dim _EmpIncentiveAmt As Double = 0
        'Dim _Qry As String = ""
        'Dim _StateSaveDetail As Boolean = False

        'cmd = "Select Sum(FNAmount) As FNAmount"
        'cmd &= vbCrLf & "   From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPCalculate As B With(NOLOCK) "
        'cmd &= vbCrLf & "  WHERE FTCalculateDate='" & CalDate & "'"
        'SewAmt = Val(HI.Conn.SQLConn.GetField(cmd, Conn.DB.DataBaseName.DB_SAMPLE, "0"))

        'CutAmt = CDbl(Format(((SewAmt * CutMultiple) / 100.0), "0.00"))

        'cmd = "  Select   B.FNHSysEmpID "
        'cmd &= vbCrLf & "  , B.FTEmpCode "

        'If HI.ST.Lang.Language = ST.Lang.eLang.TH Then

        '    cmd &= vbCrLf & "  , B.FTEmpNameTH + ' ' + B.FTEmpSurnameTH AS FTEmpName  "

        'Else

        '    cmd &= vbCrLf & "  , B.FTEmpNameEN + ' ' +   B.FTEmpSurnameEN AS FTEmpName  "

        'End If

        'cmd &= vbCrLf & "   From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee As B WITH(NOLOCK) "
        'cmd &= vbCrLf & "   INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect As U WITH(NOLOCK) ON B.FNHSysUnitSectId = U.FNHSysUnitSectId "
        'cmd &= vbCrLf & "   Where U.FTStateSampleRoom ='1' AND U.FTStateCut ='1' "
        'cmd &= vbCrLf & "   AND ISNULL(B.FDDateStart,'') >= " & CalDate & " "

        'cmd &= vbCrLf & "   AND (ISNULL(B.FDDateEnd,'') ='' OR ISNULL(B.FDDateEnd,'') <" & CalDate & ") "
        'cmd &= vbCrLf & " ORDER BY  B.FTEmpCode "
        'dtemp = HI.Conn.SQLConn.GetDataTable(cmd, Conn.DB.DataBaseName.DB_HR)



        '_CountEmp = 0

        'For Each R As DataRow In dtemp.Rows

        '    If _CountEmp <= 0 Then

        '        _dtemptime = GetEmpTime(CalDate, Val(R!FNHSysEmpID.ToString)).Copy()

        '    Else
        '        _dtemptime.Merge(GetEmpTime(CalDate, Val(R!FNHSysEmpID.ToString)).Copy())
        '    End If

        '    _CountEmp = _CountEmp + 1

        'Next

        '_TotalTimeMin = 0
        'For Each Rt As DataRow In _dtemptime.Rows
        '    _TotalTimeMin = _TotalTimeMin + Integer.Parse(Val(Rt!FNTotalWorkingMin.ToString))
        'Next


        '_FNSeq = 0
        'For Each Rt As DataRow In _dtemptime.Rows
        '    _FNSeq = _FNSeq + 1
        '    _CountEmp = _CountEmp + 1
        '    _TotalTime = _TotalTime + Integer.Parse(Val(Rt!FNWorkingMin.ToString))
        '    _TotalTimeOT = _TotalTimeOT + Integer.Parse(Val(Rt!FNWorkingOTMin.ToString))

        '    _TotalTimeHR = Integer.Parse(Val(Rt!FNTimeMin.ToString))
        '    _TotalTimeOTHR = Integer.Parse(Val(Rt!FNOT1Min.ToString))

        '    _Salary = Double.Parse(Val(Rt!FNSalary.ToString))
        '    _SalaryPerH = Double.Parse(Format(_Salary / 8, "0.0000"))
        '    _SalaryPerOT = Double.Parse(Format(_SalaryPerH * 1.5, "0.0000"))

        '    If _TotalTimeHR >= 480 Then
        '        _FNAmtNormal = _Salary
        '    Else
        '        _FNAmtNormal = Double.Parse(Format((_Salary / 480) * _TotalTimeHR, "0.00"))
        '    End If

        '    _FNAmtOT = Double.Parse(Format(((_Salary / 480) * 1.5) * _TotalTimeOTHR, "0.00"))

        '    _FNNetAmt = _FNAmtNormal + _FNAmtOT
        '    _EmpIncentiveAmt = 0

        '    If _TotalTimeMin > 0 Then

        '        _EmpIncentiveAmt = CDbl(Format(((CutAmt * Integer.Parse(Val(Rt!FNTotalWorkingMin.ToString))) / _TotalTimeMin), "0.00"))

        '    End If

        '    _Qry = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPCalculateCut_Emp ("
        '    _Qry &= vbCrLf & "  FTInsUser, FDInsDate, FTInsTime,  FTCalculateDate, FNSeq, FNHSysEmpID, FNSalary, FNNormalTime, FNOTTime, FNAmtNormal, FNAmtOT, FNNetAmt, FNIncAmount"
        '    _Qry &= vbCrLf & ")"
        '    _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
        '    _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & ""
        '    _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & ""
        '    _Qry &= vbCrLf & " ,'" & CalDate & "'   "
        '    _Qry &= vbCrLf & ", " & _FNSeq & ""
        '    _Qry &= vbCrLf & " ," & Val(Rt!FNHSysEmpID.ToString) & ""
        '    _Qry &= vbCrLf & " ," & _Salary & ""
        '    _Qry &= vbCrLf & " ," & _TotalTimeHR & ""
        '    _Qry &= vbCrLf & " ," & _TotalTimeOTHR & ""
        '    _Qry &= vbCrLf & " ," & _FNAmtNormal & ""
        '    _Qry &= vbCrLf & " ," & _FNAmtOT & ""
        '    _Qry &= vbCrLf & " ," & _FNNetAmt & ""
        '    _Qry &= vbCrLf & " ," & _EmpIncentiveAmt & ""

        '    If HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_SAMPLE) = True Then
        '        _StateSaveDetail = True
        '    End If

        'Next


        'If _StateSaveDetail Then

        '    _Qry = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPCalculateCut ("
        '    _Qry &= vbCrLf & "  FTInsUser, FDInsDate, FTInsTime,  FTCalculateDate, FNSewAmount, FNCutMulple, FNCutAmount"
        '    _Qry &= vbCrLf & ")"
        '    _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
        '    _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & ""
        '    _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & ""
        '    _Qry &= vbCrLf & " ,'" & CalDate & "'   "
        '    _Qry &= vbCrLf & ", " & SewAmt & ""
        '    _Qry &= vbCrLf & " ," & CutMultiple & ""
        '    _Qry &= vbCrLf & " ," & CutAmt & ""


        '    HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_SAMPLE)

        'End If

        'dtemp.Dispose()
        '_dtemptime.Dispose()

    End Sub

#End Region

    Private Function DeleteData(Spls As HI.TL.SplashScreen) As Boolean
        Dim _dt As DataTable
        Dim dtline As DataTable
        Dim _SSewDate As String = ""
        Dim _ESewDate As String = ""
        Dim _AllEmp As String = ""
        Dim _CalDate As String
        Dim _Qry As String = ""
        With CType(ogc.DataSource, DataTable)
            .AcceptChanges()
            _dt = .Copy()
        End With

        Dim _SDate As String = HI.UL.ULDate.ConvertEnDB(FTStartDate.Text)
        Dim _EDate As String = HI.UL.ULDate.ConvertEnDB(FTEndDate.Text)

        If _dt.Rows.Count > 0 Then
            _Qry = " exec [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.SP_SAMPLEROOM_INCENTIVE_LOAD_SPLIT '" & _SDate & "','" & _EDate & "' "
            dtline = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_SAMPLE)
        End If

        Try

            If _dt.Select("FTSelect <> '1'").Length > 0 Then
                Do While _SDate <= _EDate

                    '_Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPCalculate "
                    '_Qry &= vbCrLf & " WHERE  (FTCalculateDate = '" & _SDate & "') "

                    'HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_SAMPLE)

                    '_Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPCalculate_Detail "
                    '_Qry &= vbCrLf & " WHERE  (FTCalculateDate = '" & _SDate & "') "

                    'HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_SAMPLE)

                    '_Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPCalculate_Emp "
                    '_Qry &= vbCrLf & " WHERE  (FTCalculateDate = '" & _SDate & "') "

                    'HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_SAMPLE)

                    '_Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPCalculate_PriceMultiple "
                    '_Qry &= vbCrLf & " WHERE  (FTCalculateDate = '" & _SDate & "') "

                    'HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_SAMPLE)

                    ''_Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPCalculateCut "
                    ''_Qry &= vbCrLf & " WHERE  (FTCalculateDate = '" & _SDate & "') "
                    ''HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_SAMPLE)


                    ''_Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPCalculateCut_Emp "
                    ''_Qry &= vbCrLf & " WHERE  (FTCalculateDate = '" & _SDate & "') "

                    ''HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_SAMPLE)


                    '_Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTWageDaily "
                    '_Qry &= vbCrLf & " WHERE  (FTCalculateDate = '" & _SDate & "')  AND FTStateSampleRoom='1'"

                    'HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_SAMPLE)


                    For Each R As DataRow In _dt.Select("FTSelect='1' AND FTStateFinishDateOrg='" & _SDate & "'", "FTStateFinishDateOrg,FTSMPOrderNo, FTTeam")



                        _Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPCalculate "
                        _Qry &= vbCrLf & " WHERE  (FTCalculateDate = '" & _SDate & "')   AND FTTeam='" & HI.UL.ULF.rpQuoted(R!FTTeam.ToString) & "' "

                        HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_SAMPLE)

                        _Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPCalculate_Detail "
                        _Qry &= vbCrLf & " WHERE  (FTCalculateDate = '" & _SDate & "')  AND FTTeam='" & HI.UL.ULF.rpQuoted(R!FTTeam.ToString) & "'  "

                        HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_SAMPLE)

                        _Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPCalculate_Emp "
                        _Qry &= vbCrLf & " WHERE  (FTCalculateDate = '" & _SDate & "')   AND FTTeam='" & HI.UL.ULF.rpQuoted(R!FTTeam.ToString) & "'  "

                        HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_SAMPLE)

                        _Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPCalculate_PriceMultiple "
                        _Qry &= vbCrLf & " WHERE  (FTCalculateDate = '" & _SDate & "')  AND FTTeam='" & HI.UL.ULF.rpQuoted(R!FTTeam.ToString) & "'   "

                        HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_SAMPLE)

                        '_Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPCalculateCut "
                        '_Qry &= vbCrLf & " WHERE  (FTCalculateDate = '" & _SDate & "') "
                        'HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_SAMPLE)


                        '_Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPCalculateCut_Emp "
                        '_Qry &= vbCrLf & " WHERE  (FTCalculateDate = '" & _SDate & "') "

                        'HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_SAMPLE)


                        _Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTWageDaily "
                        _Qry &= vbCrLf & " WHERE  (FTDateTrans = '" & _SDate & "')  AND FTStateSampleRoom='1'"
                        _Qry &= vbCrLf & " AND FNHSysEmpID IN (SELECT FNHSysEmpID FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPSampleTeamEmp AS X WHERE FTTeam='" & HI.UL.ULF.rpQuoted(R!FTTeam.ToString) & "'  ) "
                        HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_SAMPLE)



                        _Qry = " Update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPSampleTeam "
                        _Qry &= vbCrLf & " SET FTStateCal='0',FTStateCalBy=''"
                        _Qry &= vbCrLf & " ,FTStateCalDate=''"
                        _Qry &= vbCrLf & " ,FTStateCalTime=''"

                        _Qry &= vbCrLf & " ,FTStateSendApp='0',FTSendAppBy=''"
                        _Qry &= vbCrLf & " ,FTSendAppDate=''"
                        _Qry &= vbCrLf & " ,FTSendAppTime=''"

                        _Qry &= vbCrLf & " ,FTStateApprove='0',FTApproveBy=''"
                        _Qry &= vbCrLf & " ,FTApproveDate=''"
                        _Qry &= vbCrLf & " ,FTApproveTime=''"

                        _Qry &= vbCrLf & " ,FTStateManagerApprove='0',FTManagerApproveBy=''"
                        _Qry &= vbCrLf & " ,FTManagerApproveDate=''"
                        _Qry &= vbCrLf & " ,FTManagerApproveTime=''"

                        _Qry &= vbCrLf & " WHERE FTSMPOrderNo='" & HI.UL.ULF.rpQuoted(R!FTSMPOrderNo.ToString) & "' AND FTTeam='" & HI.UL.ULF.rpQuoted(R!FTTeam.ToString) & "' "

                        HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_SAMPLE)

                        _AllEmp = R!FNHSysEmpID.ToString
                        _SSewDate = R!FTStartSewDate.ToString
                        _ESewDate = R!FTFinishSewDate.ToString

                        For Each Rline As DataRow In dtline.Select(" FTSMPOrderNo='" & HI.UL.ULF.rpQuoted(R!FTSMPOrderNo.ToString) & "' AND  FNHSysEmpID='" & _AllEmp & "' AND FTStartSewDate='" & _SSewDate & "'  AND FTFinishSewDate='" & _ESewDate & "'")

                            If HI.UL.ULF.rpQuoted(R!FTTeam.ToString) <> HI.UL.ULF.rpQuoted(Rline!FTTeam.ToString) Then
                                _Qry = " Update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPSampleTeam "
                                _Qry &= vbCrLf & " SET FTStateCal='0',FTStateCalBy=''"
                                _Qry &= vbCrLf & " ,FTStateCalDate=''"
                                _Qry &= vbCrLf & " ,FTStateCalTime=''"

                                _Qry &= vbCrLf & " ,FTStateSendApp='0',FTSendAppBy=''"
                                _Qry &= vbCrLf & " ,FTSendAppDate=''"
                                _Qry &= vbCrLf & " ,FTSendAppTime=''"

                                _Qry &= vbCrLf & " ,FTStateApprove='0',FTApproveBy=''"
                                _Qry &= vbCrLf & " ,FTApproveDate=''"
                                _Qry &= vbCrLf & " ,FTApproveTime=''"

                                _Qry &= vbCrLf & " ,FTStateManagerApprove='0',FTManagerApproveBy=''"
                                _Qry &= vbCrLf & " ,FTManagerApproveDate=''"
                                _Qry &= vbCrLf & " ,FTManagerApproveTime=''"
                                _Qry &= vbCrLf & " WHERE FTSMPOrderNo='" & HI.UL.ULF.rpQuoted(R!FTSMPOrderNo.ToString) & "' AND FTTeam='" & HI.UL.ULF.rpQuoted(Rline!FTTeam.ToString) & "' "

                                HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_SAMPLE)
                            End If

                        Next

                    Next



                    _SDate = HI.UL.ULDate.ConvertEnDB(HI.UL.ULDate.AddDay(_SDate, 1))

                Loop
            Else


                _Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPCalculate "
                _Qry &= vbCrLf & " WHERE  (FTCalculateDate >= '" & _SDate & "')  AND   (FTCalculateDate <= '" & _EDate & "')   "

                HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_SAMPLE)

                _Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPCalculate_Detail "
                _Qry &= vbCrLf & " WHERE  (FTCalculateDate >= '" & _SDate & "')  AND   (FTCalculateDate <= '" & _EDate & "')   "

                HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_SAMPLE)

                _Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPCalculate_Emp "
                _Qry &= vbCrLf & " WHERE  (FTCalculateDate >= '" & _SDate & "')   AND   (FTCalculateDate <= '" & _EDate & "')    "

                HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_SAMPLE)

                _Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPCalculate_PriceMultiple "
                _Qry &= vbCrLf & " WHERE  (FTCalculateDate >= '" & _SDate & "') AND   (FTCalculateDate <= '" & _EDate & "')    "

                HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_SAMPLE)

                _Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTWageDaily "
                _Qry &= vbCrLf & " WHERE  (FTDateTrans >= '" & _SDate & "'   ) AND  (FTDateTrans <= '" & _EDate & "'   )  AND FTStateSampleRoom='1'"

                HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_SAMPLE)


                _Qry = " Update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPSampleTeam "
                _Qry &= vbCrLf & " SET FTStateCal='0',FTStateCalBy=''"
                _Qry &= vbCrLf & " ,FTStateCalDate=''"
                _Qry &= vbCrLf & " ,FTStateCalTime=''"

                _Qry &= vbCrLf & " ,FTStateSendApp='0',FTSendAppBy=''"
                _Qry &= vbCrLf & " ,FTSendAppDate=''"
                _Qry &= vbCrLf & " ,FTSendAppTime=''"

                _Qry &= vbCrLf & " ,FTStateApprove='0',FTApproveBy=''"
                _Qry &= vbCrLf & " ,FTApproveDate=''"
                _Qry &= vbCrLf & " ,FTApproveTime=''"

                _Qry &= vbCrLf & " ,FTStateManagerApprove='0',FTManagerApproveBy=''"
                _Qry &= vbCrLf & " ,FTManagerApproveDate=''"
                _Qry &= vbCrLf & " ,FTManagerApproveTime=''"

                _Qry &= vbCrLf & " WHERE FTStateFinishDate >='" & _SDate & "' AND FTStateFinishDate <='" & _EDate & "' AND FTStateFinish='1'  "

                HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_SAMPLE)


            End If



            _dt.Dispose()

        Catch ex As Exception
        End Try
        Return True
    End Function


    Private Function SendApproveData(Spls As HI.TL.SplashScreen) As Boolean

        Dim _dt As DataTable
        Dim dtline As DataTable
        Dim _SSewDate As String = ""
        Dim _ESewDate As String = ""
        Dim _AllEmp As String = ""
        Dim _CalDate As String
        Dim _Qry As String = ""

        With CType(ogc.DataSource, DataTable)
            .AcceptChanges()
            _dt = .Copy()
        End With

        Dim StateSendCal As Boolean = False

        Dim _SDate As String = HI.UL.ULDate.ConvertEnDB(FTStartDate.Text)
        Dim _EDate As String = HI.UL.ULDate.ConvertEnDB(FTEndDate.Text)



        If _dt.Rows.Count > 0 Then
            _Qry = " exec [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.SP_SAMPLEROOM_INCENTIVE_LOAD_SPLIT '" & _SDate & "','" & _EDate & "' "
            dtline = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_SAMPLE)
        End If
        Try
            Do While _SDate <= _EDate

                StateSendCal = False
                For Each R As DataRow In _dt.Select("FTSelect='1' AND FTStateFinishDateOrg='" & _SDate & "'", "FTStateFinishDateOrg,FTSMPOrderNo, FTTeam")

                    _Qry = " Update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPSampleTeam "
                    _Qry &= vbCrLf & " SET FTStateSendApp='1',FTSendAppBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Qry &= vbCrLf & " ,FTSendAppDate=" & HI.UL.ULDate.FormatDateDB & ""
                    _Qry &= vbCrLf & " ,FTSendAppTime=" & HI.UL.ULDate.FormatTimeDB & ""

                    _Qry &= vbCrLf & "   WHERE FTSMPOrderNo='" & HI.UL.ULF.rpQuoted(R!FTSMPOrderNo.ToString) & "' AND FTTeam='" & HI.UL.ULF.rpQuoted(R!FTTeam.ToString) & "' "

                    HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_SAMPLE)

                    _AllEmp = R!FNHSysEmpID.ToString
                    _SSewDate = R!FTStartSewDate.ToString
                    _ESewDate = R!FTFinishSewDate.ToString

                    For Each Rline As DataRow In dtline.Select(" FTSMPOrderNo='" & HI.UL.ULF.rpQuoted(R!FTSMPOrderNo.ToString) & "' AND  FNHSysEmpID='" & _AllEmp & "' AND FTStartSewDate='" & _SSewDate & "'  AND FTFinishSewDate='" & _ESewDate & "'")

                        If HI.UL.ULF.rpQuoted(R!FTTeam.ToString) <> HI.UL.ULF.rpQuoted(Rline!FTTeam.ToString) Then
                            _Qry = " Update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPSampleTeam "
                            _Qry &= vbCrLf & " SET FTStateSendApp='1',FTSendAppBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                            _Qry &= vbCrLf & " ,FTSendAppDate=" & HI.UL.ULDate.FormatDateDB & ""
                            _Qry &= vbCrLf & " ,FTSendAppTime=" & HI.UL.ULDate.FormatTimeDB & ""
                            _Qry &= vbCrLf & " WHERE FTSMPOrderNo='" & HI.UL.ULF.rpQuoted(R!FTSMPOrderNo.ToString) & "' AND FTTeam='" & HI.UL.ULF.rpQuoted(Rline!FTTeam.ToString) & "' "

                            HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_SAMPLE)
                        End If

                    Next


                    StateSendCal = True
                Next

                If StateSendCal Then

                    _Qry = " Update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPCalculateCut "
                    _Qry &= vbCrLf & " SET FTStateSendApp='1',FTSendAppBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Qry &= vbCrLf & " ,FTSendAppDate=" & HI.UL.ULDate.FormatDateDB & ""
                    _Qry &= vbCrLf & " ,FTSendAppTime=" & HI.UL.ULDate.FormatTimeDB & ""
                    _Qry &= vbCrLf & "   WHERE FTCalculateDate='" & _SDate & "'  "

                    HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_SAMPLE)

                End If


                _SDate = HI.UL.ULDate.ConvertEnDB(HI.UL.ULDate.AddDay(_SDate, 1))

            Loop




            _dt.Dispose()

        Catch ex As Exception
        End Try

        Return True
    End Function

    Private Function VerrifyData() As Boolean
        Dim _Pass As Boolean = False


        Dim _StateSelect As Integer = 0

        Try

            _StateSelect = CType(ogc.DataSource, DataTable).Select("FTSelect='1'").Length

        Catch ex As Exception
        End Try

        If _StateSelect > 0 Then
            _Pass = True
        Else
            HI.MG.ShowMsg.mInvalidData("กรุณาทำการเลือกข้อมูลที่ต้องการทำกรคำนวณ !!!", 1394730001, Me.Text)
            FTStartDate.Focus()
        End If

        Return _Pass

    End Function



#End Region

#Region "Process Load Data"

    Private Sub LoadDataInfo()
        Me.ogc.DataSource = Nothing
        ochkselectall.Checked = False

        Dim _Qry As String = ""
        Dim _SDate As String = HI.UL.ULDate.ConvertEnDB(FTStartDate.Text)
        Dim _EDate As String = HI.UL.ULDate.ConvertEnDB(FTEndDate.Text)
        Dim dtline As New DataTable
        Dim _Spls As New HI.TL.SplashScreen("Loading Data... Please Wait... ")
        Try

            ''_Qry = " SELECT '1' AS FTSelect,FTStateFinishDate AS FTStateFinishDateOrg"
            ''_Qry &= vbCrLf & " 	,CASE WHEN ISDATE(A.FTStateFinishDate) = 1 THEN  Convert(varchar(10),convert(Datetime,A.FTStateFinishDate) ,103) ELSE '' END AS FTStateFinishDate"
            ''_Qry &= vbCrLf & " 	, A.FTSMPOrderNo, A.FTTeam , A.FTStateFinish "
            ''_Qry &= vbCrLf & ", ISNULL(A.FTStateCal,'0') AS FTStateCal"
            ''_Qry &= vbCrLf & " 	,CASE WHEN ISDATE(A.FTStateCalDate) = 1 THEN  Convert(varchar(10),convert(Datetime,A.FTStateCalDate) ,103) ELSE '' END AS FTStateCalDate"
            ''_Qry &= vbCrLf & " 	, A.FTStateCalBy"
            ''_Qry &= vbCrLf & " 	,ISNULL(Emp.FTEmpName,'') AS FTEmpName"
            ''_Qry &= vbCrLf & ",ISNULL(P.FNQuantity,0) AS FNQuantity"
            ''_Qry &= vbCrLf & " 	,ISNULL(EmpId.FNHSysEmpID,'') AS FNHSysEmpID"

            ''_Qry &= vbCrLf & ", ISNULL(A.FTStateSendApp,'0') AS FTStateSendApp"
            ''_Qry &= vbCrLf & " 	,CASE WHEN ISDATE(A.FTSendAppDate) = 1 THEN  Convert(varchar(10),convert(Datetime,A.FTSendAppDate) ,103) ELSE '' END AS FTSendAppDate"
            ''_Qry &= vbCrLf & " 	, A.FTSendAppBy"

            ''_Qry &= vbCrLf & ", ISNULL(A.FTStateApprove,'0') AS FTStateApprove"
            ''_Qry &= vbCrLf & " 	,CASE WHEN ISDATE(A.FTApproveDate) = 1 THEN  Convert(varchar(10),convert(Datetime,A.FTApproveDate) ,103) ELSE '' END AS FTApproveDate"
            ''_Qry &= vbCrLf & " 	, A.FTApproveBy"

            ''_Qry &= vbCrLf & " 	,ISNULL(EmpCount.FNEmpTeam,0) AS FNEmpTeam"
            ''_Qry &= vbCrLf & " 	,ISNULL(Sew.FTStartSewDate,FTStateFinishDate) AS FTStartSewDate"
            ''_Qry &= vbCrLf & " 	,ISNULL(SewF.FTFinishSewDate,ISNULL(Sew.FTStartSewDate,FTStateFinishDate) ) AS FTFinishSewDate"

            ''_Qry &= vbCrLf & " ,CASE WHEN ISDATE(ISNULL(SewF.FTFinishSewDate,ISNULL(Sew.FTStartSewDate,FTStateFinishDate) )) = 1 THEN  Convert(varchar(10),convert(Datetime,ISNULL(SewF.FTFinishSewDate,ISNULL(Sew.FTStartSewDate,FTStateFinishDate) )) ,103) ELSE '' END AS FTFinishSewDateShow"


            ''_Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPSampleTeam  As A With(NOLOCK)"
            ''_Qry &= vbCrLf & " OUTER APPLY ( "
            ''_Qry &= vbCrLf & "  Select  STUFF((Select  ', ' + FTEmpName  "
            ''_Qry &= vbCrLf & "	From (SELECT Convert(nvarchar(10),Row_number() Over(Order By X1.FNSeq)) + '.'  + X2.FTEmpNameTH + ' ' + FTEmpSurnameTH   AS FTEmpName"
            ''_Qry &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPSampleTeamEmp As X1 INNER JOIN"
            ''_Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS X2 ON X1.FNHSysEmpID = X2.FNHSysEmpID"
            ''_Qry &= vbCrLf & "   WHERE X1.FTSMPOrderNo = A.FTSMPOrderNo "
            ''_Qry &= vbCrLf & "   AND X1.FTTeam  = A.FTTeam  "
            ''_Qry &= vbCrLf & "  ) As T "
            ''_Qry &= vbCrLf & "	FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)'),1,1,'') AS FTEmpName"
            ''_Qry &= vbCrLf & " ) As Emp	"
            ''_Qry &= vbCrLf & " OUTER APPLY ( "
            ''_Qry &= vbCrLf & "  Select  STUFF((Select  ',' + FNHSysEmpID  "
            ''_Qry &= vbCrLf & "	From (SELECT DISTINCT Convert(nvarchar(130),X1.FNHSysEmpID)   AS FNHSysEmpID"
            ''_Qry &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPSampleTeamEmp As X1 INNER JOIN"
            ''_Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS X2 ON X1.FNHSysEmpID = X2.FNHSysEmpID"
            ''_Qry &= vbCrLf & "   WHERE X1.FTSMPOrderNo = A.FTSMPOrderNo "
            ''_Qry &= vbCrLf & "   AND X1.FTTeam  = A.FTTeam  "
            ''_Qry &= vbCrLf & "  ) As T "
            ''_Qry &= vbCrLf & "	FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)'),1,1,'') AS FNHSysEmpID"
            ''_Qry &= vbCrLf & " ) As EmpId	"
            ''_Qry &= vbCrLf & "  OUTER APPLY (SELECT SUM(FNQuantity) AS FNQuantity"
            ''_Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPSampleTeamBreakdown As X1 WITH(NOLOCK)"
            ''_Qry &= vbCrLf & " WHERE X1.FTSMPOrderNo = A.FTSMPOrderNo "
            ''_Qry &= vbCrLf & " And X1.FTTeam  = A.FTTeam"
            ''_Qry &= vbCrLf & "  ) P"


            ''_Qry &= vbCrLf & "  OUTER APPLY (SELECT COUNT(DISTINCT  FNHSysEmpID) AS FNEmpTeam"
            ''_Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPSampleTeamEmp As X1 WITH(NOLOCK)"
            ''_Qry &= vbCrLf & " WHERE X1.FTSMPOrderNo = A.FTSMPOrderNo "
            ''_Qry &= vbCrLf & " And X1.FTTeam  = A.FTTeam"
            ''_Qry &= vbCrLf & "  ) EmpCount"

            ''_Qry &= vbCrLf & "  OUTER APPLY (SELECT MIN(  FTDate) AS FTStartSewDate,MAX(  FTDate) AS FTFinishSewDate"
            ''_Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPSampleProcess As X1 WITH(NOLOCK)"
            ''_Qry &= vbCrLf & " WHERE X1.FTSMPOrderNo = A.FTSMPOrderNo "
            ''_Qry &= vbCrLf & " And X1.FTTeam  = A.FTTeam AND X1.FNSampleState=0"
            ''_Qry &= vbCrLf & "  ) Sew"

            ''_Qry &= vbCrLf & "  OUTER APPLY (SELECT MIN(  FTDate) AS FTStartSewDate,MAX(  FTDate) AS FTFinishSewDate"
            ''_Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPSampleProcess As X1 WITH(NOLOCK)"
            ''_Qry &= vbCrLf & " WHERE X1.FTSMPOrderNo = A.FTSMPOrderNo "
            ''_Qry &= vbCrLf & " And X1.FTTeam  = A.FTTeam AND X1.FNSampleState=1"
            ''_Qry &= vbCrLf & "  ) SewF"

            ''_Qry &= vbCrLf & " WHERE  (A.FTStateFinishDate >= '" & _SDate & "')  "
            ''_Qry &= vbCrLf & "        AND  (A.FTStateFinishDate <= '" & _EDate & "')  "
            ''_Qry &= vbCrLf & "        AND (A.FTStateFinish = '1')"
            ''_Qry &= vbCrLf & " ORDER BY  A.FTStateFinishDate, A.FTSMPOrderNo, A.FTTeam"


            _Qry = " exec [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.SP_SAMPLEROOM_INCENTIVE_LOAD '" & _SDate & "','" & _EDate & "' "

            dtline = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_SAMPLE)

            Me.ogc.DataSource = dtline.Copy
        Catch ex As Exception

        End Try

        _Spls.Close()
    End Sub

#End Region
#Region "General"

    Private Sub ocmload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmload.Click
        If HI.UL.ULDate.CheckDate(FTStartDate.Text) <> "" And HI.UL.ULDate.CheckDate(FTEndDate.Text) <> "" Then
            Call LoadDataInfo()
        Else

            If HI.UL.ULDate.CheckDate(FTStartDate.Text) = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FTStartDate_lbl.Text)
                FTStartDate.Focus()
            Else
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FTEndDate_lbl.Text)
                FTEndDate.Focus()
            End If

        End If
    End Sub

    Private Sub FTDateRequest_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles FTStartDate.EditValueChanged
        Me.ogc.DataSource = Nothing
        ochkselectall.Checked = False
    End Sub

    Private Sub ochkselectall_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles ochkselectall.CheckedChanged
        Try

            Dim _State As String = "0"
            If Me.ochkselectall.Checked Then
                _State = "1"
            End If

            With ogc
                If Not (.DataSource Is Nothing) And ogv.RowCount > 0 Then

                    With ogv
                        For I As Integer = 0 To .RowCount - 1
                            .SetRowCellValue(I, .Columns.ColumnByFieldName("FTSelect"), _State)
                        Next
                    End With

                    CType(.DataSource, DataTable).AcceptChanges()
                End If

            End With


        Catch ex As Exception

        End Try
    End Sub

    Private Sub wOTRequest_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        HI.UL.AppRegistry.LoadLayoutGridFromRegistry(Me, Me.ogv)
    End Sub

#End Region

    Private Sub ogv_RowStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles ogv.RowStyle

        Try

            With CType(sender, DevExpress.XtraGrid.Views.Grid.GridView)

                If "" & .GetRowCellValue(e.RowHandle, "FTStateCal").ToString = "1" Then
                    e.Appearance.BackColor = Drawing.Color.LightYellow
                    e.Appearance.BackColor2 = Drawing.Color.Orange
                End If

            End With

        Catch ex As Exception

        End Try
    End Sub

    Private Sub ocmpreview_Click(sender As Object, e As EventArgs) Handles ocmpreview.Click
        If Not (Me.ogc.DataSource Is Nothing) Then
            With CType(Me.ogc.DataSource, DataTable)
                .AcceptChanges()

                If .Select("FTSelect='1'").Length > 0 Then
                    For Each R As DataRow In .Select("FTSelect='1'")

                        Dim _FM As String = ""
                        _FM = " {TSMPCalculate.FTSMPOrderNo}='" & HI.UL.ULF.rpQuoted(R!FTSMPOrderNo.ToString) & "' AND  {TSMPCalculate.FTTeam}='" & HI.UL.ULF.rpQuoted(R!FTTeam.ToString) & "'"

                        With New HI.RP.Report

                            .FormTitle = Me.Text
                            .ReportFolderName = "Human Report\"
                            .ReportName = "SMPIncentiveV.rpt"
                            .Formular = _FM
                            .Preview()

                        End With



                    Next

                Else
                    HI.MG.ShowMsg.mInfo("ไม่พบข้อมูลที่ต้องการทำการ ออกรายงาน กรุณาทำการเลือกข้อมูล !!!!", 1604220576, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
                End If

            End With
        Else
            HI.MG.ShowMsg.mInfo("ไม่พบข้อมูลที่ต้องการทำการ ออกรายงาน กรุณาทำการเลือกข้อมูล !!!!", 1604220576, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
        End If

    End Sub

    Private Sub ocmdelete_Click(sender As Object, e As EventArgs) Handles ocmdelete.Click
        If Me.VerrifyData Then

            If HI.MG.ShowMsg.mConfirmProcess("คุณต้องการทำการยกเลิกการคำนวณ Incentive ของพนักงาน ใช่หรือไม่ ?", 1604228574, Me.Text) = True Then

                Dim _Spls As New HI.TL.SplashScreen("Calculating...   Please Wait   ")

                If Me.DeleteData(_Spls) Then

                    _Spls.Close()
                    HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)

                    Me.ocmload_Click(ocmload, New System.EventArgs)

                Else

                    _Spls.Close()
                    HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)

                End If

            End If


        End If
    End Sub


    Private Sub ocmSendApprove_Click(sender As Object, e As EventArgs) Handles ocmSendApprove.Click
        If Me.VerrifyData Then

            If HI.MG.ShowMsg.mConfirmProcess("คุณต้องการทำการ ส่งอนุมัติ Incentive ของพนักงาน ใช่หรือไม่ ?", 1604243574, Me.Text) = True Then

                Dim _Spls As New HI.TL.SplashScreen("Sending Approve...   Please Wait   ")

                If Me.SendApproveData(_Spls) Then

                    _Spls.Close()
                    HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)

                    Me.ocmload_Click(ocmload, New System.EventArgs)

                Else

                    _Spls.Close()
                    HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)

                End If

            End If


        End If
    End Sub
End Class
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.Spreadsheet
Imports System.IO
Imports System.Windows.Forms
Imports System.Data.SqlClient
Public Class wEditScanQuantity

    Private _AddNewData As wEditScanQuantityAddOrder


    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        _AddNewData = New wEditScanQuantityAddOrder

        HI.TL.HandlerControl.AddHandlerObj(_AddNewData)

        Dim oSysLang As New ST.SysLanguage
        Try
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _AddNewData.Name.ToString.Trim, _AddNewData)
        Catch ex As Exception
        Finally
        End Try

        Call InitGrid()
    End Sub

#Region "Initial Grid"
    Private Sub InitGrid()

        '------Start Add Summary Grid-------------
        Dim sFieldCount As String = ""
        Dim sFieldSum As String = "H01|H02|H03|H04|H05|H06|H07|H08|H09|H10|H11|H12|H13|Total"
        Dim sFieldGrpCount As String = ""
        Dim sFieldGrpSum As String = "H01|H02|H03|H04|H05|H06|H07|H08|H09|H10|H11|H12|H13|Total"

        With ogv
            .ClearGrouping()
            .ClearDocument()
            .Columns.ColumnByFieldName("FDScanDateGrp").Group()
            For Each Str As String In sFieldCount.Split("|")
                If Str <> "" Then
                    .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Count, Str)
                    .Columns(Str).SummaryItem.DisplayFormat = "{0:n0}"
                End If
            Next

            For Each Str As String In sFieldSum.Split("|")
                If Str <> "" Then
                    .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Sum, Str)
                    .Columns(Str).SummaryItem.DisplayFormat = "{0:n0}"
                End If
            Next

            For Each Str As String In sFieldGrpCount.Split("|")
                If Str <> "" Then
                    .GroupSummary.Add(DevExpress.Data.SummaryItemType.Count, Str, Nothing, "(Count by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n0})")
                End If
            Next

            For Each Str As String In sFieldGrpSum.Split("|")
                If Str <> "" Then
                    .GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, Str, Nothing, "(Sum by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n0})")
                End If
            Next

            .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
            .OptionsView.ShowFooter = True

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

    Private Sub ProcessSave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmsave.Click

        If Me.VerrifyData Then

            If HI.MG.ShowMsg.mConfirmProcess(MG.ShowMsg.ProcessType.mSave, Me.Text) = True Then

                Dim _Spls As New HI.TL.SplashScreen("Saving Data...   Please Wait   ")

                If Me.SaveData(_Spls) Then

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
    'Private Function GetEmpTimeMoveOut_Old(_UnitSectID As Integer, _CalDate As String) As DataTable
    '    Dim _dt As DataTable
    '    Dim _Qry As String = ""
    '    _Qry = "SELECT  FNHSysEmpID, FDDate, FNHSysEmpTypeId, FNHSysUnitSectId, FNHSysEmpTypeIdTo, FNHSysUnitSectIdTo, FTStartTime, FTEndTime,  FNTotalMinute"
    '    _Qry &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTMoveTeamMoveType AS X WITH(NOLOCK)"
    '    _Qry &= vbCrLf & "   WHERE FNHSysUnitSectId=" & _UnitSectID & " "
    '    _Qry &= vbCrLf & "   AND FDDate='" & HI.UL.ULDate.ConvertEnDB(_CalDate) & "' "
    '    _Qry &= vbCrLf & "   AND FNHSysEmpTypeIdTo<>" & _UnitSectID & " "

    '    _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

    '    Return _dt
    'End Function

    Private Function GetEmpTime_OLD(_UnitSectID As Integer, _CalDate As String) As DataTable
        Dim _dt As DataTable
        Dim _dtmove As DataTable = GetEmpTimeMoveOut(_UnitSectID, _CalDate)
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
        Dim _FNHSysShiftID As Integer = 0

        _Qry = "  Select  Top 1 FNHSysShiftID"
        _Qry &= vbCrLf & "  From " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.THRMEmployee With(NOLOCK) "
        _Qry &= vbCrLf & "Where (FNHSysUnitSectId =" & _UnitSectID & ") And (FNEmpStatus = 0)"
        _Qry &= vbCrLf & " Order By FDDateEnd "
        _FNHSysShiftID = Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "0"))

        _Qry = "Select FTShiftPeriodTimeCode AS FTPeriadOfTimeCode, FTStartTime, FTEndTime,ROW_NUMBER() Over (Order By FTStartTime) As FNHour"
        _Qry &= vbCrLf & " FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMShiftPeriodTime As A With(NOLOCK)"
        _Qry &= vbCrLf & " WHERE  FNHSysShiftID =" & _FNHSysShiftID & " AND(FTStateActive = '1')"
        _Qry &= vbCrLf & " ORDER BY FTStartTime"
        _dttime = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MASTER)

        _Qry = "EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.SP_GET_EMPTIME_CAL_INCENTIVE " & _UnitSectID & ",'" & HI.UL.ULDate.ConvertEnDB(_CalDate) & "'"
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

    Private Function GetEmpTime(_UnitSectID As Integer, _CalDate As String, Optional _TFNHSysEmpID As Integer = 0) As DataTable
        Dim _dt As DataTable
        Dim _dtmove As DataTable = GetEmpTimeMoveOut(_UnitSectID, _CalDate, _TFNHSysEmpID)
        Dim _dttime As DataTable
        Dim _dttimeLeave As DataTable
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
        _dtemptime.Columns.Add("FTStateRelease", GetType(String))
        _dtemptime.Columns.Add("FNAmtFixedIncentive", GetType(String))

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
        Dim _FTStateRelease As String = ""
        Dim _FNAmtFixedIncentive As Double = 0

        Dim FNHSysShiftID As Integer = 0
        Dim TimeCheckIn1 As String = ""
        Dim TimeCheckOut1 As String = ""
        Dim TimeCheckIn2 As String = ""
        Dim TimeCheckOut2 As String = ""

        Dim dtcheckTime As New DataTable

        _Qry = "  Select  Top 1 A.FNHSysShiftID,S.FTIn1, S.FTOut1, FTIn2, S.FTOut2 "
        _Qry &= vbCrLf & "  From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS A With(NOLOCK)  "
        _Qry &= vbCrLf & " INNER JOIN   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMTimeShift AS S With(NOLOCK)  ON A.FNHSysShiftID = S.FNHSysShiftID"
        _Qry &= vbCrLf & " Where (A.FNHSysUnitSectId = " & _UnitSectID & ") "
        _Qry &= vbCrLf & " And (A.FNEmpStatus = 0) "
        _Qry &= vbCrLf & "  Order By FDDateEnd "

        ' FNHSysShiftID = Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "0"))
        dtcheckTime = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

        For Each Rtx As DataRow In dtcheckTime.Rows
            FNHSysShiftID = Val(Rtx!FNHSysShiftID.ToString)
            TimeCheckIn1 = Rtx!FTIn1.ToString
            TimeCheckOut1 = Rtx!FTOut1.ToString
            TimeCheckIn2 = Rtx!FTIn2.ToString
            TimeCheckOut2 = Rtx!FTOut2.ToString

            Exit For
        Next




        dtcheckTime.Dispose()

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

            If _FNHSysEmpID = 0 Then
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

                If _dtmove.Select("FNHSysEmpID=" & _FNHSysEmpID & " AND FTStartTime<='" & _StartTime & "' AND FTEndTime>='" & _EndTime & "'").Length <= 0 Then
                    Select Case _FNHour
                        Case 1, 2, 3, 4

                            If _FTState = "1" Then

                                Select Case True
                                    Case (_FTIn1 <= _StartTime And _FTOut1 >= _EndTime) And (_FTStartTime <= _StartTime And _FTEndTime >= _EndTime)
                                        '_FNWorkMinute = 60
                                        If _dttimeLeave.Select("FTLeaveStartTime<='" & _StartTime & "' AND FTLeaveEndTime>='" & _EndTime & "' ").Length <= 0 Then
                                            _FNWorkMinute = 60
                                        Else
                                            _FNWorkMinute = 0
                                        End If

                                    Case (_StartTime < _FTIn1 And _FTOut1 >= _EndTime And _FTIn1 <> "") And (_FTStartTime <= _StartTime And _FTEndTime >= _EndTime)

                                        Try
                                            ' _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn1), CDate(_CalDate & " " & _EndTime))

                                            If _dttimeLeave.Select("FTLeaveStartTime<='" & _StartTime & "' AND FTLeaveEndTime>='" & _EndTime & "' ").Length <= 0 Then
                                                _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn1), CDate(_CalDate & " " & _EndTime))
                                            Else
                                                _FNWorkMinute = 0
                                            End If

                                        Catch ex As Exception
                                        End Try

                                    Case (_StartTime < _FTIn1 And _FTOut1 < _EndTime And _FTIn1 <> "" And _FTOut1 <> "" And _FTIn1 < _FTOut1) And (_FTStartTime <= _StartTime And _FTEndTime >= _EndTime)

                                        Try
                                            ' _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn1), CDate(_CalDate & " " & _FTOut1))

                                            If _dttimeLeave.Select("FTLeaveStartTime<='" & _StartTime & "' AND FTLeaveEndTime>='" & _EndTime & "' ").Length <= 0 Then
                                                _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn1), CDate(_CalDate & " " & _FTOut1))
                                            Else
                                                _FNWorkMinute = 0
                                            End If

                                        Catch ex As Exception
                                        End Try
                                    Case (_StartTime >= _FTIn1 And _FTOut1 < _EndTime And _FTIn1 <> "")
                                        Try
                                            ' _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _FTOut1))

                                            If _dttimeLeave.Select("FTLeaveStartTime<='" & _StartTime & "' AND FTLeaveEndTime>='" & _EndTime & "' ").Length <= 0 Then
                                                _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _FTOut1))
                                            Else
                                                _FNWorkMinute = 0
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
                                        Else
                                            _FNWorkMinute = 0
                                        End If
                                    Case (_StartTime < _FTIn1 And _FTOut1 >= _EndTime And _FTIn1 <> "")

                                        Try
                                            '_FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn1), CDate(_CalDate & " " & _EndTime))

                                            If _dttimeLeave.Select("FTLeaveStartTime<='" & _StartTime & "' AND FTLeaveEndTime>='" & _EndTime & "' ").Length <= 0 Then
                                                _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn1), CDate(_CalDate & " " & _EndTime))
                                            Else
                                                _FNWorkMinute = 0
                                            End If

                                        Catch ex As Exception
                                        End Try

                                    Case (_StartTime < _FTIn1 And _FTOut1 < _EndTime And _FTIn1 <> "" And _FTOut1 <> "" And _FTIn1 < _FTOut1)

                                        Try
                                            ' _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn1), CDate(_CalDate & " " & _FTOut1))

                                            If _dttimeLeave.Select("FTLeaveStartTime<='" & _StartTime & "' AND FTLeaveEndTime>='" & _EndTime & "' ").Length <= 0 Then
                                                _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn1), CDate(_CalDate & " " & _FTOut1))
                                            Else
                                                _FNWorkMinute = 0
                                            End If

                                        Catch ex As Exception
                                        End Try
                                    Case (_StartTime >= _FTIn1 And _FTOut1 < _EndTime And _FTIn1 <> "")
                                        Try
                                            ' _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _FTOut1))

                                            If _dttimeLeave.Select("FTLeaveStartTime<='" & _StartTime & "' AND FTLeaveEndTime>='" & _EndTime & "' ").Length <= 0 Then
                                                _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _FTOut1))
                                            Else
                                                _FNWorkMinute = 0
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
                                        Else
                                            _FNWorkMinute = 0
                                        End If

                                    Case (_StartTime < _FTIn2 And _FTOut2 >= _EndTime And _FTIn2 <> "") And (_FTStartTime <= _StartTime And _FTEndTime >= _EndTime) And (_FTIn2 <> "" And _FTOut2 <> "")

                                        Try
                                            ' _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn2), CDate(_CalDate & " " & _EndTime))

                                            If _dttimeLeave.Select("FTLeaveStartTime<='" & _StartTime & "' AND FTLeaveEndTime>='" & _EndTime & "' ").Length <= 0 Then
                                                _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn2), CDate(_CalDate & " " & _EndTime))
                                            Else
                                                _FNWorkMinute = 0
                                            End If

                                        Catch ex As Exception
                                        End Try

                                    Case (_StartTime < _FTIn2 And _FTOut2 < _EndTime And _FTIn2 <> "" And _FTOut2 <> "" And _FTIn2 < _FTOut2) And (_FTStartTime <= _StartTime And _FTEndTime >= _EndTime) And (_FTIn2 <> "" And _FTOut2 <> "")

                                        Try
                                            '_FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn2), CDate(_CalDate & " " & _FTOut2))
                                            If _dttimeLeave.Select("FTLeaveStartTime<='" & _StartTime & "' AND FTLeaveEndTime>='" & _EndTime & "' ").Length <= 0 Then
                                                _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn2), CDate(_CalDate & " " & _FTOut2))
                                            Else
                                                _FNWorkMinute = 0
                                            End If
                                        Catch ex As Exception
                                        End Try
                                    Case (_StartTime >= _FTIn2 And _FTOut2 < _EndTime And _FTIn2 <> "") And (_FTIn2 <> "" And _FTOut2 <> "")
                                        Try
                                            ' _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _FTOut2))

                                            If _dttimeLeave.Select("FTLeaveStartTime<='" & _StartTime & "' AND FTLeaveEndTime>='" & _EndTime & "' ").Length <= 0 Then
                                                _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _FTOut2))
                                            Else
                                                _FNWorkMinute = 0
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
                                        Else
                                            _FNWorkMinute = 0
                                        End If

                                    Case (_StartTime < _FTIn2 And _FTOut2 >= _EndTime And _FTIn2 <> "") And (_FTIn2 <> "" And _FTOut2 <> "")

                                        Try
                                            ' _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn2), CDate(_CalDate & " " & _EndTime))

                                            If _dttimeLeave.Select("FTLeaveStartTime<='" & _StartTime & "' AND FTLeaveEndTime>='" & _EndTime & "' ").Length <= 0 Then
                                                _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn2), CDate(_CalDate & " " & _EndTime))
                                            Else
                                                _FNWorkMinute = 0
                                            End If

                                        Catch ex As Exception
                                        End Try

                                    Case (_StartTime < _FTIn2 And _FTOut2 < _EndTime And _FTIn2 <> "" And _FTOut2 <> "" And _FTIn2 < _FTOut2) And (_FTIn2 <> "" And _FTOut2 <> "")

                                        Try
                                            ' _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn2), CDate(_CalDate & " " & _FTOut2))

                                            If _dttimeLeave.Select("FTLeaveStartTime<='" & _StartTime & "' AND FTLeaveEndTime>='" & _EndTime & "' ").Length <= 0 Then
                                                _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn2), CDate(_CalDate & " " & _FTOut2))
                                            Else
                                                _FNWorkMinute = 0
                                            End If

                                        Catch ex As Exception
                                        End Try
                                    Case (_StartTime >= _FTIn2 And _FTOut2 < _EndTime And _FTIn2 <> "") And (_FTIn2 <> "" And _FTOut2 <> "")
                                        Try
                                            '_FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _FTOut2))
                                            If _dttimeLeave.Select("FTLeaveStartTime<='" & _StartTime & "' AND FTLeaveEndTime>='" & _EndTime & "' ").Length <= 0 Then
                                                _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _FTOut2))
                                            Else
                                                _FNWorkMinute = 0
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
                                If _FTState = "1" Then

                                    Select Case True

                                        Case (((_FTIn3 <= _StartTime And _FTOut3 >= _EndTime) And (_FTStartTime <= _StartTime And _FTEndTime >= _EndTime)) Or ((_FTIn3 = TimeCheckOut2 And _FNHour = 9)))

                                            ' _FNWorkMinute = 60
                                            If _FTIn3 = TimeCheckOut2 Then
                                                _FNWorkMinute = 60
                                            Else
                                                If _dttimeLeave.Select("FTLeaveStartTime<='" & _StartTime & "' AND FTLeaveEndTime>='" & _EndTime & "' ").Length <= 0 And _dtmove.Select("FNHSysEmpID=" & _FNHSysEmpID & " AND FTStartTime<='" & _StartTime & "' AND FTEndTime>='" & _FTOut3 & "'").Length <= 0 Then
                                                    _FNWorkMinute = 60
                                                Else
                                                    _FNWorkMinute = 0
                                                End If
                                            End If


                                        Case (_StartTime < _FTIn3 And _FTOut3 >= _EndTime And _FTIn3 <> "") And (_FTStartTime <= _StartTime And _FTEndTime >= _EndTime)

                                            Try
                                                ' _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn3), CDate(_CalDate & " " & _EndTime))
                                                If _dttimeLeave.Select("FTLeaveStartTime<='" & _StartTime & "' AND FTLeaveEndTime>='" & _EndTime & "' ").Length <= 0 And _dtmove.Select("FNHSysEmpID=" & _FNHSysEmpID & " AND FTStartTime<='" & _StartTime & "' AND FTEndTime>='" & _FTOut3 & "'").Length <= 0 Then
                                                    _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn3), CDate(_CalDate & " " & _EndTime))
                                                Else
                                                    _FNWorkMinute = 0
                                                End If
                                            Catch ex As Exception
                                            End Try

                                        Case (_StartTime < _FTIn3 And _FTOut3 < _EndTime And _FTIn3 <> "" And _FTOut3 <> "" And _FTIn3 < _FTOut2) And (_FTStartTime <= _StartTime And _FTEndTime >= _EndTime)

                                            Try
                                                '_FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn3), CDate(_CalDate & " " & _FTOut3))

                                                If _dttimeLeave.Select("FTLeaveStartTime<='" & _StartTime & "' AND FTLeaveEndTime>='" & _EndTime & "' ").Length <= 0 And _dtmove.Select("FNHSysEmpID=" & _FNHSysEmpID & " AND FTStartTime<='" & _StartTime & "' AND FTEndTime>='" & _FTOut3 & "'").Length <= 0 Then
                                                    _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn3), CDate(_CalDate & " " & _FTOut3))
                                                Else
                                                    _FNWorkMinute = 0
                                                End If

                                            Catch ex As Exception
                                            End Try
                                        Case (_StartTime >= _FTIn3 And _FTOut3 < _EndTime And _FTIn3 <> "" And _FTOut3 <> "" And _FTIn3 > _FTOut2 And _StartTime <= _FTOut3)

                                            Try
                                                '_FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _FTOut3))
                                                If _dttimeLeave.Select("FTLeaveStartTime<='" & _StartTime & "' AND FTLeaveEndTime>='" & _EndTime & "' ").Length <= 0 And _dtmove.Select("FNHSysEmpID=" & _FNHSysEmpID & " AND FTStartTime<='" & _StartTime & "' AND FTEndTime>='" & _FTOut3 & "'").Length <= 0 Then
                                                    _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _FTOut3))
                                                Else
                                                    _FNWorkMinute = 0
                                                End If
                                            Catch ex As Exception
                                            End Try
                                    End Select

                                Else

                                    Select Case True

                                        Case ((_FTIn3 <= _StartTime And _FTOut3 >= _EndTime) Or (_FTIn3 = TimeCheckOut2 And _FNHour = 9))


                                            If _FTIn3 = TimeCheckOut2 Then
                                                _FNWorkMinute = 60
                                            Else
                                                ' _FNWorkMinute = 60
                                                If _dttimeLeave.Select("FTLeaveStartTime<='" & _StartTime & "' AND FTLeaveEndTime>='" & _EndTime & "' ").Length <= 0 And _dtmove.Select("FNHSysEmpID=" & _FNHSysEmpID & " AND FTStartTime<='" & _StartTime & "' AND FTEndTime>='" & _FTOut3 & "'").Length <= 0 Then
                                                    _FNWorkMinute = 60
                                                Else
                                                    _FNWorkMinute = 0
                                                End If
                                            End If


                                        Case (_StartTime < _FTIn3 And _FTOut3 >= _EndTime And _FTIn3 <> "")

                                            Try
                                                '_FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn3), CDate(_CalDate & " " & _EndTime))
                                                If _dttimeLeave.Select("FTLeaveStartTime<='" & _StartTime & "' AND FTLeaveEndTime>='" & _EndTime & "' ").Length <= 0 And _dtmove.Select("FNHSysEmpID=" & _FNHSysEmpID & " AND FTStartTime<='" & _StartTime & "' AND FTEndTime>='" & _FTOut3 & "'").Length <= 0 Then
                                                    _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn3), CDate(_CalDate & " " & _EndTime))
                                                Else
                                                    _FNWorkMinute = 0
                                                End If
                                            Catch ex As Exception
                                            End Try

                                        Case (_StartTime < _FTIn3 And _FTOut3 < _EndTime And _FTIn3 <> "" And _FTOut3 <> "" And _FTIn3 < _FTOut2)

                                            Try
                                                '_FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn3), CDate(_CalDate & " " & _FTOut3))
                                                If _dttimeLeave.Select("FTLeaveStartTime<='" & _StartTime & "' AND FTLeaveEndTime>='" & _EndTime & "' ").Length <= 0 And _dtmove.Select("FNHSysEmpID=" & _FNHSysEmpID & " AND FTStartTime<='" & _StartTime & "' AND FTEndTime>='" & _FTOut3 & "'").Length <= 0 Then

                                                    _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn3), CDate(_CalDate & " " & _FTOut3))

                                                Else

                                                    _FNWorkMinute = 0

                                                End If

                                            Catch ex As Exception
                                            End Try

                                        Case (_StartTime >= _FTIn3 And _FTOut3 < _EndTime And _FTIn3 <> "" And _FTOut3 <> "" And _FTIn3 > _FTOut2 And _StartTime <= _FTOut3)

                                            Try
                                                '_FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _FTOut3))

                                                If _dttimeLeave.Select("FTLeaveStartTime<='" & _StartTime & "' AND FTLeaveEndTime>='" & _EndTime & "' ").Length <= 0 And _dtmove.Select("FNHSysEmpID=" & _FNHSysEmpID & " AND FTStartTime<='" & _StartTime & "' AND FTEndTime>='" & _FTOut3 & "'").Length <= 0 Then

                                                    _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _FTOut3))

                                                Else

                                                    _FNWorkMinute = 0

                                                End If

                                            Catch ex As Exception
                                            End Try

                                    End Select

                                End If
                            End If

                    End Select
                Else
                    _FNWorkMinute = 0
                End If

                If _FNWorkMinute < 0 Then _FNWorkMinute = 0

                If _dtemptime.Select("FNHSysEmpID=" & _FNHSysEmpID & "").Length <= 0 Then

                    _dtemptime.Rows.Add(_FTEmpCode, _FNHSysEmpID, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                                             _FNLateNormalMin, _FNLateNormalCut, _FNRetireNormalMin,
                                             _FNRetireNormalCut, _FNLateOtMin, _FNLateOtCut, _FNRetireOtMin,
                                             _FNRetireOtCut, _FNAbsentCut, _FNAbsent, _FNTimeMin, _FNOT1Min,
                                             _FNOT1_5Min, _FNOT2Min, _FNOT3Min, _FNOT4Min, _FNLateMMin, _FNLateAfMin,
                                             _FNRetireMMin, _FNRetireAfMin, _FNOTRequestMin, _FNCutAbsent, _FNLateNormalNotCut,
                                             _FTStartTime, _FTEndTime, _FNTotalMinute, _FTState, _FNSalary, 0, 0, "0", 0, _FTStateDaily, _FTStatePlagnent, _FTStateRelease, _FNAmtFixedIncentive)

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


    Private Function ImportData() As Boolean

        Try
            Dim _dt As DataTable
            Dim _Cmd As String



            _Cmd = " exec " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & ".dbo.SP_GET_TmpTPRODTEditScanQuantity '" & HI.ST.UserInfo.UserName & "', " & Val(Me.FNHSysCmpId.Properties.Tag.ToString)

            _dt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD)



            For Each R As DataRow In _dt.Select("FTSelect='1'", "FDScanDate,FTUnitSectCode")




                _Cmd = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderAddEditScanQuantity "
                _Cmd &= vbCrLf & "  ("
                _Cmd &= vbCrLf & " FTInsUser, FDInsDate, FTInsTime, FNHSysUnitSectId, FTDate, FTOrderNo, FTSubOrderNo, FNHSysCmpId,FNStateSewPack,FNHSysPartId,FTSemiPart "
                _Cmd &= vbCrLf & " ) "
                _Cmd &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Cmd &= vbCrLf & ", " & HI.UL.ULDate.FormatDateDB & ""
                _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ""
                _Cmd &= vbCrLf & "," & Val(R!FNHSysUnitSectId.ToString) & " "
                _Cmd &= vbCrLf & ",'" & HI.UL.ULDate.ConvertEnDB(R!FDScanDateOrg.ToString) & "'"
                _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "'"
                _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "'"
                _Cmd &= vbCrLf & "," & Val(HI.ST.SysInfo.CmpID) & " "
                _Cmd &= vbCrLf & "," & Val(R!FNStateSewPack.ToString) & " "
                Try
                    _Cmd &= vbCrLf & "," & Integer.Parse(Val(R!FNHSysPartId.ToString))
                Catch ex As Exception
                    _Cmd &= vbCrLf & ",0"
                End Try

                _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTSemiPart.ToString) & "'"

                If HI.Conn.SQLConn.ExecuteNonQuery(_Cmd, Conn.DB.DataBaseName.DB_PROD) = False Then

                    _Cmd = " UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderAddEditScanQuantity "
                    _Cmd &= vbCrLf & " SET FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "', FDUpdDate=" & HI.UL.ULDate.FormatDateDB & ", FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & ",FNHSysCmpId=" & Val(HI.ST.SysInfo.CmpID) & " "


                    _Cmd &= vbCrLf & " WHERE  FNHSysUnitSectId=" & Val(R!FNHSysUnitSectId.ToString) & " "
                    _Cmd &= vbCrLf & "        AND FTDate='" & HI.UL.ULDate.ConvertEnDB(R!FDScanDateOrg.ToString) & "'"
                    _Cmd &= vbCrLf & "        AND FTOrderNo='" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "'"
                    _Cmd &= vbCrLf & "        AND FTSubOrderNo='" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "'"
                    _Cmd &= vbCrLf & "        AND FNStateSewPack='" & Val(R!FNStateSewPack.ToString) & "'"
                    _Cmd &= vbCrLf & " AND FNHSysPartId=" & Integer.Parse(Val(R!FNHSysPartId.ToString))
                    _Cmd &= vbCrLf & " AND FTSemiPart='" & HI.UL.ULF.rpQuoted(R!FTSemiPart.ToString) & "'"



                    If HI.Conn.SQLConn.ExecuteNonQuery(_Cmd, Conn.DB.DataBaseName.DB_PROD) Then
                        'HI.Conn.SQLConn.Tran.Rollback()
                        'HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        'HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                        'Return False
                    End If
                End If





                _Cmd = "Update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTEditScanQuantity"
                _Cmd &= vbCrLf & " SET FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Cmd &= vbCrLf & ", FDUpdDate=" & HI.UL.ULDate.FormatDateDB & ""
                _Cmd &= vbCrLf & ", FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & ""
                _Cmd &= vbCrLf & ", FNHour01Qty=" & Integer.Parse(Val(R!H01.ToString)) & ""
                _Cmd &= vbCrLf & ", FNHour02Qty=" & Integer.Parse(Val(R!H02.ToString)) & ""
                _Cmd &= vbCrLf & ", FNHour03Qty=" & Integer.Parse(Val(R!H03.ToString)) & ""
                _Cmd &= vbCrLf & ", FNHour04Qty=" & Integer.Parse(Val(R!H04.ToString)) & ""
                _Cmd &= vbCrLf & ", FNHour05Qty=" & Integer.Parse(Val(R!H05.ToString)) & ""
                _Cmd &= vbCrLf & ", FNHour06Qty=" & Integer.Parse(Val(R!H06.ToString)) & ""
                _Cmd &= vbCrLf & ", FNHour07Qty=" & Integer.Parse(Val(R!H07.ToString)) & ""
                _Cmd &= vbCrLf & ", FNHour08Qty=" & Integer.Parse(Val(R!H08.ToString)) & ""
                _Cmd &= vbCrLf & ", FNHour09Qty=" & Integer.Parse(Val(R!H09.ToString)) & ""
                _Cmd &= vbCrLf & ", FNHour10Qty=" & Integer.Parse(Val(R!H10.ToString)) & ""
                _Cmd &= vbCrLf & ", FNHour11Qty=" & Integer.Parse(Val(R!H11.ToString)) & ""
                _Cmd &= vbCrLf & ", FNHour12Qty=" & Integer.Parse(Val(R!H12.ToString)) & ""
                _Cmd &= vbCrLf & ", FNHour13Qty=" & Integer.Parse(Val(R!H13.ToString)) & ""
                _Cmd &= vbCrLf & ", FNTotalQty=" & Integer.Parse(Val(R!Total.ToString)) & ""
                _Cmd &= vbCrLf & " WHERE  FTDate='" & HI.UL.ULDate.ConvertEnDB(R!FDScanDateOrg.ToString) & "'"
                _Cmd &= vbCrLf & "        AND FNHSysUnitSectId=" & Integer.Parse(Val(R!FNHSysUnitSectId.ToString)) & ""
                _Cmd &= vbCrLf & "        AND FTOrderNo='" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "' "
                _Cmd &= vbCrLf & "        AND FTSubOrderNo='" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "'"
                _Cmd &= vbCrLf & "        AND FNStateSewPack=" & Val(R!FNStateSewPack.ToString) & ""
                _Cmd &= vbCrLf & " AND FNHSysPartId=" & Integer.Parse(Val(R!FNHSysPartId.ToString))
                _Cmd &= vbCrLf & " AND FTSemiPart='" & HI.UL.ULF.rpQuoted(R!FTSemiPart.ToString) & "'"

                If HI.Conn.SQLConn.ExecuteNonQuery(_Cmd, Conn.DB.DataBaseName.DB_PROD) = False Then

                    _Cmd = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTEditScanQuantity("
                    _Cmd &= vbCrLf & " FTInsUser, FDInsDate, FTInsTime"
                    _Cmd &= vbCrLf & ", FTDate, FNHSysUnitSectId, FTOrderNo, FTSubOrderNo"
                    _Cmd &= vbCrLf & ", FNHour01Qty, FNHour02Qty, FNHour03Qty, FNHour04Qty, FNHour05Qty "
                    _Cmd &= vbCrLf & ", FNHour06Qty, FNHour07Qty, FNHour08Qty, FNHour09Qty, FNHour10Qty"
                    _Cmd &= vbCrLf & ", FNHour11Qty, FNHour12Qty, FNHour13Qty, FNTotalQty,FNStateSewPack,FNHSysPartId,FTSemiPart "
                    _Cmd &= vbCrLf & " ) "
                    _Cmd &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Cmd &= vbCrLf & ", " & HI.UL.ULDate.FormatDateDB & ""
                    _Cmd &= vbCrLf & ", " & HI.UL.ULDate.FormatTimeDB & ""
                    _Cmd &= vbCrLf & ",'" & HI.UL.ULDate.ConvertEnDB(R!FDScanDateOrg.ToString) & "'"
                    _Cmd &= vbCrLf & "," & Integer.Parse(Val(R!FNHSysUnitSectId.ToString)) & ""
                    _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "' "
                    _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "'"
                    _Cmd &= vbCrLf & ", " & Integer.Parse(Val(R!H01.ToString)) & ""
                    _Cmd &= vbCrLf & ", " & Integer.Parse(Val(R!H02.ToString)) & ""
                    _Cmd &= vbCrLf & ", " & Integer.Parse(Val(R!H03.ToString)) & ""
                    _Cmd &= vbCrLf & ", " & Integer.Parse(Val(R!H04.ToString)) & ""
                    _Cmd &= vbCrLf & ", " & Integer.Parse(Val(R!H05.ToString)) & ""
                    _Cmd &= vbCrLf & ", " & Integer.Parse(Val(R!H06.ToString)) & ""
                    _Cmd &= vbCrLf & ", " & Integer.Parse(Val(R!H07.ToString)) & ""
                    _Cmd &= vbCrLf & ", " & Integer.Parse(Val(R!H08.ToString)) & ""
                    _Cmd &= vbCrLf & ", " & Integer.Parse(Val(R!H09.ToString)) & ""
                    _Cmd &= vbCrLf & ", " & Integer.Parse(Val(R!H10.ToString)) & ""
                    _Cmd &= vbCrLf & ", " & Integer.Parse(Val(R!H11.ToString)) & ""
                    _Cmd &= vbCrLf & ", " & Integer.Parse(Val(R!H12.ToString)) & ""
                    _Cmd &= vbCrLf & ", " & Integer.Parse(Val(R!H13.ToString)) & ""
                    _Cmd &= vbCrLf & ", " & Integer.Parse(Val(R!Total.ToString)) & ""
                    _Cmd &= vbCrLf & ", " & Integer.Parse(Val(R!FNStateSewPack.ToString)) & ""
                    _Cmd &= vbCrLf & "," & Integer.Parse(Val(R!FNHSysPartId.ToString))
                    _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTSemiPart.ToString) & "'"


                    HI.Conn.SQLConn.ExecuteNonQuery(_Cmd, Conn.DB.DataBaseName.DB_PROD)

                End If

            Next

            _dt.Dispose()
        Catch ex As Exception
        End Try
        Return True
    End Function


    Private Function SaveData(Spls As HI.TL.SplashScreen) As Boolean

        Dim _dt As DataTable
        Dim _Cmd As String

        With CType(ogc.DataSource, DataTable)
            .AcceptChanges()
            _dt = .Copy()
        End With

        Try

            For Each R As DataRow In _dt.Select("FTSelect='1'", "FDScanDate,FTUnitSectCode")

                _Cmd = "Update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTEditScanQuantity"
                _Cmd &= vbCrLf & " SET FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Cmd &= vbCrLf & ", FDUpdDate=" & HI.UL.ULDate.FormatDateDB & ""
                _Cmd &= vbCrLf & ", FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & ""
                _Cmd &= vbCrLf & ", FNHour01Qty=" & Integer.Parse(Val(R!H01.ToString)) & ""
                _Cmd &= vbCrLf & ", FNHour02Qty=" & Integer.Parse(Val(R!H02.ToString)) & ""
                _Cmd &= vbCrLf & ", FNHour03Qty=" & Integer.Parse(Val(R!H03.ToString)) & ""
                _Cmd &= vbCrLf & ", FNHour04Qty=" & Integer.Parse(Val(R!H04.ToString)) & ""
                _Cmd &= vbCrLf & ", FNHour05Qty=" & Integer.Parse(Val(R!H05.ToString)) & ""
                _Cmd &= vbCrLf & ", FNHour06Qty=" & Integer.Parse(Val(R!H06.ToString)) & ""
                _Cmd &= vbCrLf & ", FNHour07Qty=" & Integer.Parse(Val(R!H07.ToString)) & ""
                _Cmd &= vbCrLf & ", FNHour08Qty=" & Integer.Parse(Val(R!H08.ToString)) & ""
                _Cmd &= vbCrLf & ", FNHour09Qty=" & Integer.Parse(Val(R!H09.ToString)) & ""
                _Cmd &= vbCrLf & ", FNHour10Qty=" & Integer.Parse(Val(R!H10.ToString)) & ""
                _Cmd &= vbCrLf & ", FNHour11Qty=" & Integer.Parse(Val(R!H11.ToString)) & ""
                _Cmd &= vbCrLf & ", FNHour12Qty=" & Integer.Parse(Val(R!H12.ToString)) & ""
                _Cmd &= vbCrLf & ", FNHour13Qty=" & Integer.Parse(Val(R!H13.ToString)) & ""
                _Cmd &= vbCrLf & ", FNTotalQty=" & Integer.Parse(Val(R!Total.ToString)) & ""
                _Cmd &= vbCrLf & " WHERE  FTDate='" & HI.UL.ULDate.ConvertEnDB(R!FDScanDateOrg.ToString) & "'"
                _Cmd &= vbCrLf & "        AND FNHSysUnitSectId=" & Integer.Parse(Val(R!FNHSysUnitSectId.ToString)) & ""
                _Cmd &= vbCrLf & "        AND FTOrderNo='" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "' "
                _Cmd &= vbCrLf & "        AND FTSubOrderNo='" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "'"
                _Cmd &= vbCrLf & "        AND FNStateSewPack=" & Val(R!FNStateSewPack.ToString) & ""
                _Cmd &= vbCrLf & " AND FNHSysPartId=" & Integer.Parse(Val(R!FNHSysPartId.ToString))
                _Cmd &= vbCrLf & " AND FTSemiPart='" & HI.UL.ULF.rpQuoted(R!FTSemiPart.ToString) & "'"

                If HI.Conn.SQLConn.ExecuteNonQuery(_Cmd, Conn.DB.DataBaseName.DB_PROD) = False Then

                    _Cmd = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTEditScanQuantity("
                    _Cmd &= vbCrLf & " FTInsUser, FDInsDate, FTInsTime"
                    _Cmd &= vbCrLf & ", FTDate, FNHSysUnitSectId, FTOrderNo, FTSubOrderNo"
                    _Cmd &= vbCrLf & ", FNHour01Qty, FNHour02Qty, FNHour03Qty, FNHour04Qty, FNHour05Qty "
                    _Cmd &= vbCrLf & ", FNHour06Qty, FNHour07Qty, FNHour08Qty, FNHour09Qty, FNHour10Qty"
                    _Cmd &= vbCrLf & ", FNHour11Qty, FNHour12Qty, FNHour13Qty, FNTotalQty,FNStateSewPack,FNHSysPartId,FTSemiPart "
                    _Cmd &= vbCrLf & " ) "
                    _Cmd &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Cmd &= vbCrLf & ", " & HI.UL.ULDate.FormatDateDB & ""
                    _Cmd &= vbCrLf & ", " & HI.UL.ULDate.FormatTimeDB & ""
                    _Cmd &= vbCrLf & ",'" & HI.UL.ULDate.ConvertEnDB(R!FDScanDateOrg.ToString) & "'"
                    _Cmd &= vbCrLf & "," & Integer.Parse(Val(R!FNHSysUnitSectId.ToString)) & ""
                    _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "' "
                    _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "'"
                    _Cmd &= vbCrLf & ", " & Integer.Parse(Val(R!H01.ToString)) & ""
                    _Cmd &= vbCrLf & ", " & Integer.Parse(Val(R!H02.ToString)) & ""
                    _Cmd &= vbCrLf & ", " & Integer.Parse(Val(R!H03.ToString)) & ""
                    _Cmd &= vbCrLf & ", " & Integer.Parse(Val(R!H04.ToString)) & ""
                    _Cmd &= vbCrLf & ", " & Integer.Parse(Val(R!H05.ToString)) & ""
                    _Cmd &= vbCrLf & ", " & Integer.Parse(Val(R!H06.ToString)) & ""
                    _Cmd &= vbCrLf & ", " & Integer.Parse(Val(R!H07.ToString)) & ""
                    _Cmd &= vbCrLf & ", " & Integer.Parse(Val(R!H08.ToString)) & ""
                    _Cmd &= vbCrLf & ", " & Integer.Parse(Val(R!H09.ToString)) & ""
                    _Cmd &= vbCrLf & ", " & Integer.Parse(Val(R!H10.ToString)) & ""
                    _Cmd &= vbCrLf & ", " & Integer.Parse(Val(R!H11.ToString)) & ""
                    _Cmd &= vbCrLf & ", " & Integer.Parse(Val(R!H12.ToString)) & ""
                    _Cmd &= vbCrLf & ", " & Integer.Parse(Val(R!H13.ToString)) & ""
                    _Cmd &= vbCrLf & ", " & Integer.Parse(Val(R!Total.ToString)) & ""
                    _Cmd &= vbCrLf & ", " & Integer.Parse(Val(R!FNStateSewPack.ToString)) & ""
                    _Cmd &= vbCrLf & "," & Integer.Parse(Val(R!FNHSysPartId.ToString))
                    _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTSemiPart.ToString) & "'"


                    HI.Conn.SQLConn.ExecuteNonQuery(_Cmd, Conn.DB.DataBaseName.DB_PROD)

                End If

            Next

            _dt.Dispose()
        Catch ex As Exception
        End Try
        Return True
    End Function

    Private Function DeleteData(Spls As HI.TL.SplashScreen) As Boolean
        Dim _dt As DataTable
        Dim _CalDate As String
        Dim _Cmd As String = ""
        With CType(ogc.DataSource, DataTable)
            .AcceptChanges()
            _dt = .Copy()
        End With

        Try
            For Each R As DataRow In _dt.Select("FTSelect='1'", "FDScanDate,FTUnitSectCode")

                _Cmd = "DELETE FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTEditScanQuantity"
                _Cmd &= vbCrLf & " WHERE  FTDate='" & HI.UL.ULDate.ConvertEnDB(R!FDScanDateOrg.ToString) & "'"
                _Cmd &= vbCrLf & "        AND FNHSysUnitSectId=" & Integer.Parse(Val(R!FNHSysUnitSectId.ToString)) & ""
                _Cmd &= vbCrLf & "        AND FTOrderNo='" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "' "
                _Cmd &= vbCrLf & "        AND FTSubOrderNo='" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "'"
                _Cmd &= vbCrLf & "        AND FNStateSewPack=" & Val(R!FNStateSewPack.ToString) & ""

                HI.Conn.SQLConn.ExecuteNonQuery(_Cmd, Conn.DB.DataBaseName.DB_PROD)

                _Cmd = "DELETE FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderAddEditScanQuantity"
                _Cmd &= vbCrLf & " WHERE  FTDate='" & HI.UL.ULDate.ConvertEnDB(R!FDScanDateOrg.ToString) & "'"
                _Cmd &= vbCrLf & "        AND FNHSysUnitSectId=" & Integer.Parse(Val(R!FNHSysUnitSectId.ToString)) & ""
                _Cmd &= vbCrLf & "        AND FTOrderNo='" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "' "
                _Cmd &= vbCrLf & "        AND FTSubOrderNo='" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "'"

                HI.Conn.SQLConn.ExecuteNonQuery(_Cmd, Conn.DB.DataBaseName.DB_PROD)

            Next

            _dt.Dispose()
        Catch ex As Exception
        End Try
        Return True
    End Function

    Private Function VerrifyData() As Boolean
        Dim _Pass As Boolean = False

        If Not (ogc.DataSource Is Nothing) Then

            CType(ogc.DataSource, DataTable).AcceptChanges()

            If CType(ogc.DataSource, DataTable).Rows.Count > 0 Then
                If CType(ogc.DataSource, DataTable).Select("FTSelect='1'").Length > 0 Then
                    _Pass = True
                Else
                    HI.MG.ShowMsg.mInvalidData("กรุณาทำการเลือกข้อมูลที่ต้องการ !!!", 1394738881, Me.Text)
                    FTStartDate.Focus()
                End If
            Else
                HI.MG.ShowMsg.mInvalidData("กรุณาทำการเลือกข้อมูลที่ต้องการ !!!", 1394738881, Me.Text)
                FTStartDate.Focus()
            End If

        Else

            HI.MG.ShowMsg.mInvalidData("กรุณาทำการเลือกข้อมูลที่ต้องการ !!!", 1394738881, Me.Text)
            FTStartDate.Focus()

        End If

        Return _Pass

    End Function
    Private Sub LoadDataInfo()
        Me.ogc.DataSource = Nothing
        ochkselectall.Checked = False

        Dim _Dt As DataTable = Nothing
        Dim _DtShow As DataTable
        Dim _Qry As String = ""

        Dim dt As New DataTable
        Dim dtline As New DataTable
        Dim _Spls As New HI.TL.SplashScreen("Loading Data... Please Wait... ")

        Dim _TotalLine As Integer = 0
        Dim _PLine As Integer = 0
        Dim _DisplayLang As String = "TH"

        If HI.ST.Lang.Language <> ST.Lang.eLang.TH Then
            _DisplayLang = "EN"
        End If

        _Qry = "SELECT FNHSysUnitSectId, FTUnitSectCode"
        _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS X WITH(NOLOCK)"
        _Qry &= vbCrLf & "  WHERE  (FTStateSew = '1') AND (FTStateActive = '1')"

        If FNHSysUnitSectId.Text <> "" Then
            _Qry &= vbCrLf & "  AND FTUnitSectCode >='" & HI.UL.ULF.rpQuoted(FNHSysUnitSectId.Text) & "'"
        End If

        If FNHSysUnitSectIdTo.Text <> "" Then
            _Qry &= vbCrLf & "  AND FTUnitSectCode <='" & HI.UL.ULF.rpQuoted(FNHSysUnitSectIdTo.Text) & "'"
        End If

        If FNHSysCmpId.Text <> "" Then
            _Qry &= vbCrLf & "  AND FNHSysCmpId =" & Val("" & Me.FNHSysCmpId.Properties.Tag.ToString)
        End If


        _Qry &= vbCrLf & "  ORDER BY FTUnitSectCode"
        dtline = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MASTER)
        _TotalLine = dtline.Rows.Count

        For Each Rx As DataRow In dtline.Rows
            _PLine = _PLine + 1

            _Spls.UpdateInformation("Loading Data of Line " & Rx!FTUnitSectCode.ToString & "  ( " & _PLine.ToString & " of  " & _TotalLine.ToString & " )")

            _Qry = "Exec [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.SP_GET_SCANOUTLINE_EDIT_SCANQUANTITY " & Val(Rx!FNHSysUnitSectId.ToString) & ",'" & HI.UL.ULDate.ConvertEnDB(FTStartDate.Text) & "','" & HI.UL.ULDate.ConvertEnDB(FTStartDate.Text) & "' "
            dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

            If _Dt Is Nothing Then
                _Dt = dt.Copy
            Else
                _Dt.Merge(dt.Copy)
            End If

        Next

        _Spls.Close()

        dtline.Dispose()
        dt.Dispose()

        Try
            _DtShow = _Dt.Select("FTUnitSectCode<>''", "FDScanDateOrg,FTUnitSectCode,FTOrderNo,FTSubOrderNo").CopyToDataTable
        Catch ex As Exception
            _DtShow = Nothing
        End Try

        Dim _CalDate As String = ""
        Dim _dtemptime As DataTable
        Dim _MaxOT As Integer = 0
        Dim _QtyOTMo As Integer = 0

        'For Each R As DataRow In _DtShow.Rows

        '    _MaxOT = 0
        '    _CalDate = HI.UL.ULDate.ConvertEnDB(R!FDScanDateOrg.ToString)
        '    _dtemptime = GetEmpTime(Integer.Parse(Val(R!FNHSysUnitSectId.ToString)), R!FDScanDateOrg.ToString).Copy()
        'For Each Rx As DataRow In _dtemptime.Select("FNWorkingOTMin>0", "FNWorkingOTMin DESC ")
        '    _MaxOT = +Integer.Parse(Val(Rx!FNWorkingOTMin.ToString))
        '    Exit For
        'Next

        '    For Each Rx As DataRow In _dtemptime.Select("FNWorkingOTMin>0", "FNOT1Min DESC ")
        '        _MaxOT = +Integer.Parse(Val(Rx!FNOT1Min.ToString))
        '        Exit For
        '    Next

        '    Select Case True
        '        Case (_MaxOT = 0)

        '            _QtyOTMo = Integer.Parse(Val(R!H09.ToString)) + Integer.Parse(Val(R!H10.ToString)) + Integer.Parse(Val(R!H11.ToString)) + Integer.Parse(Val(R!H12.ToString)) + Integer.Parse(Val(R!H13.ToString))

        '            R!H09 = 0
        '            R!H10 = 0
        '            R!H11 = 0
        '            R!H12 = 0
        '            R!H13 = 0
        '            R!H08 = Integer.Parse(Val(R!H08.ToString)) + _QtyOTMo

        '        Case _MaxOT > 0 And _MaxOT <= 60
        '            _QtyOTMo = Integer.Parse(Val(R!H10.ToString)) + Integer.Parse(Val(R!H11.ToString)) + Integer.Parse(Val(R!H12.ToString)) + Integer.Parse(Val(R!H13.ToString))

        '            R!H10 = 0
        '            R!H11 = 0
        '            R!H12 = 0
        '            R!H13 = 0
        '            R!H09 = Integer.Parse(Val(R!H09.ToString)) + _QtyOTMo

        '        Case _MaxOT > 60 And _MaxOT <= 120
        '            _QtyOTMo = Integer.Parse(Val(R!H11.ToString)) + Integer.Parse(Val(R!H12.ToString)) + Integer.Parse(Val(R!H13.ToString))

        '            R!H11 = 0
        '            R!H12 = 0
        '            R!H13 = 0
        '            R!H10 = Integer.Parse(Val(R!H10.ToString)) + _QtyOTMo
        '        Case _MaxOT > 120 And _MaxOT <= 180

        '            _QtyOTMo = Integer.Parse(Val(R!H12.ToString)) + Integer.Parse(Val(R!H13.ToString))

        '            R!H12 = 0
        '            R!H13 = 0
        '            R!H11 = Integer.Parse(Val(R!H11.ToString)) + _QtyOTMo

        '        Case _MaxOT > 180 And _MaxOT <= 240

        '            _QtyOTMo = Integer.Parse(Val(R!H13.ToString))
        '            R!H13 = 0
        '            R!H12 = Integer.Parse(Val(R!H12.ToString)) + _QtyOTMo

        '        Case _MaxOT > 240

        '    End Select
        ' Next

        Me.ogc.DataSource = _DtShow.Copy
        Me.ogv.ExpandAllGroups()
        _DtShow.Dispose()
        _Dt.Dispose()

    End Sub
#End Region

#Region "General"

    Private Sub ocmload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmload.Click
        If HI.UL.ULDate.CheckDate(FTStartDate.Text) <> "" Then

            Call LoadDataInfo()

        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FTStartDate_lbl.Text)
            FTStartDate.Focus()
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

    Private Sub wOTRequest_Load(sender As Object, e As System.EventArgs) Handles MyBase.Load
        Me.FNHSysCmpId.Text = HI.ST.SysInfo.CmpCode
        HI.UL.AppRegistry.LoadLayoutGridFromRegistry(Me, Me.ogv)
    End Sub

#End Region

    Private Sub ogv_RowCellStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles ogv.RowCellStyle
        Try
            With Me.ogv
                Select Case e.Column.FieldName
                    Case "H01", "H02", "H03", "H04", "H05", "H06", "H07", "H08", "H09", "H10", "H11", "H12", "H13", "Total"
                        If Val("" & .GetRowCellValue(e.RowHandle, e.Column.FieldName).ToString) > 0 Then
                            e.Appearance.BackColor = Drawing.Color.GreenYellow
                            e.Appearance.ForeColor = Drawing.Color.Blue
                            e.Appearance.Font = New Drawing.Font("tahoma", 8, Drawing.FontStyle.Bold)
                        Else

                        End If

                    Case Else

                End Select
            End With
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ogv_RowStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles ogv.RowStyle
        Try
            With Me.ogv

                If "" & .GetRowCellValue(e.RowHandle, "FTCalUser").ToString <> "" Then
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
                        _FM = "{THRTIncentive.FNHSysUnitSectId}=" & Val(R!FNHSysUnitSectId.ToString) & " AND {THRTIncentive.FTCalDate}='" & HI.UL.ULDate.ConvertEnDB(R!FDScanDateOrg.ToString) & "'"

                        With New HI.RP.Report

                            .FormTitle = Me.Text
                            .ReportFolderName = "Human Report\"

                            Select Case Val(R!FTIncentiveTypeIdx.ToString)
                                Case 1
                                    .ReportName = "HRIncentiveV1.rpt"
                                Case 2
                                    .ReportName = "HRIncentiveV2.rpt"
                                Case 3
                                    .ReportName = "HRIncentiveV3.rpt"
                                Case Else
                                    .ReportName = "HRIncentiveV0.rpt"
                            End Select

                            .Formular = _FM
                            .Preview()

                        End With

                        Select Case Val(R!FTIncentiveTypeIdx.ToString)
                            Case 1
                                With New HI.RP.Report

                                    .FormTitle = Me.Text
                                    .ReportFolderName = "Human Report\"


                                    .ReportName = "HRIncentiveV1_O.rpt"


                                    .Formular = _FM
                                    .Preview()

                                End With
                            Case 2
                            Case 3
                        End Select


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

            If HI.MG.ShowMsg.mConfirmProcess(MG.ShowMsg.ProcessType.mDelete, Me.Text) = True Then

                Dim _Spls As New HI.TL.SplashScreen("Deletting Data...   Please Wait   ")

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

    Private Sub RepQuantity_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles RepQuantity.EditValueChanging
        Try

            If e.NewValue < 0 Then
                e.Cancel = True
            Else

                With ogv

                    Dim _Qty1 As Integer = Integer.Parse(Val("" & .GetFocusedRowCellValue("H01").ToString))
                    Dim _Qty2 As Integer = Integer.Parse(Val("" & .GetFocusedRowCellValue("H02").ToString))
                    Dim _Qty3 As Integer = Integer.Parse(Val("" & .GetFocusedRowCellValue("H03").ToString))
                    Dim _Qty4 As Integer = Integer.Parse(Val("" & .GetFocusedRowCellValue("H04").ToString))
                    Dim _Qty5 As Integer = Integer.Parse(Val("" & .GetFocusedRowCellValue("H05").ToString))
                    Dim _Qty6 As Integer = Integer.Parse(Val("" & .GetFocusedRowCellValue("H06").ToString))
                    Dim _Qty7 As Integer = Integer.Parse(Val("" & .GetFocusedRowCellValue("H07").ToString))
                    Dim _Qty8 As Integer = Integer.Parse(Val("" & .GetFocusedRowCellValue("H08").ToString))
                    Dim _Qty9 As Integer = Integer.Parse(Val("" & .GetFocusedRowCellValue("H09").ToString))
                    Dim _Qty10 As Integer = Integer.Parse(Val("" & .GetFocusedRowCellValue("H10").ToString))
                    Dim _Qty11 As Integer = Integer.Parse(Val("" & .GetFocusedRowCellValue("H11").ToString))
                    Dim _Qty12 As Integer = Integer.Parse(Val("" & .GetFocusedRowCellValue("H12").ToString))
                    Dim _Qty13 As Integer = Integer.Parse(Val("" & .GetFocusedRowCellValue("H13").ToString))
                    Dim _FNTotal As Integer = 0

                    Select Case .FocusedColumn.FieldName.ToString
                        Case "H01"
                            _Qty1 = e.NewValue
                        Case "H02"
                            _Qty2 = e.NewValue
                        Case "H03"
                            _Qty3 = e.NewValue
                        Case "H04"
                            _Qty4 = e.NewValue
                        Case "H05"
                            _Qty5 = e.NewValue
                        Case "H06"
                            _Qty6 = e.NewValue
                        Case "H07"
                            _Qty7 = e.NewValue
                        Case "H08"
                            _Qty8 = e.NewValue
                        Case "H09"
                            _Qty9 = e.NewValue
                        Case "H10"
                            _Qty10 = e.NewValue
                        Case "H11"
                            _Qty11 = e.NewValue
                        Case "H12"
                            _Qty12 = e.NewValue
                        Case "H13"
                            _Qty13 = e.NewValue
                    End Select

                    _FNTotal = _Qty1 + _Qty2 + _Qty3 + _Qty4 + _Qty5 + _Qty6 + _Qty7 + _Qty8 + _Qty9 + _Qty10 + _Qty11 + _Qty12 + _Qty13
                    .SetFocusedRowCellValue("Total", _FNTotal)

                End With

            End If
        Catch ex As Exception
            e.Cancel = True
        End Try
    End Sub

    Private Sub ocmadd_Click(sender As Object, e As EventArgs) Handles ocmadd.Click
        If FTStartDate.Text <> "" Then
            With _AddNewData
                HI.ST.Lang.SP_SETxLanguage(_AddNewData)
                HI.TL.HandlerControl.ClearControl(_AddNewData)
                .AddNewData = False
                .FTDate.Text = FTStartDate.Text
                .FNStateSewPack.SelectedIndex = 0
                .ShowDialog()

                If .AddNewData Then
                    Call LoadDataInfo()
                End If
            End With
        End If



    End Sub

    Private Sub ocmimport_Click(sender As Object, e As EventArgs) Handles ocmimportexcel.Click
        Try
            Dim _Cmd As String = ""
            Dim _FileName As String = ""
            Dim folderDlg As New OpenFileDialog
            With folderDlg
                '.Filter = "Excel Worksheets(97-2003)" & "|*.xls|Excel Worksheets(2010-2013)|*.xlsx"
                .Filter = "Excel Worksheets(2010-2013)" & "|*.xlsx|Excel Worksheets(97-2003)|*.xls"
                .FilterIndex = 1
                .RestoreDirectory = False
                .Multiselect = True
                Dim dr As DialogResult = .ShowDialog()
                If (dr = System.Windows.Forms.DialogResult.OK) Then
                    Dim _Spls As New HI.TL.SplashScreen("Reading....Please Wait.....", "Import Data From File ")
                    For Each file In .FileNames
                        ' _FileName = .FileName
                        ''  MsgBox("a")
                        Call ReadXlsfile_Multiple(file, _Spls)
                        ''  MsgBox("z")
                    Next
                    _Spls.Close()
                End If
            End With
        Catch ex As Exception

        End Try
    End Sub
    Private Sub ReadXlsfile_Multiple(_fileName As String, ByVal _Spls As HI.TL.SplashScreen)
        Try
            ''Me.oTabPlanGen.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.oTabmaster})
            Dim _strAlert As String = ""
            Dim _Qry As String = ""
            Dim xlsFilename As String = Path.GetFileName(_fileName)
            Dim _dt As System.Data.DataTable
            _dt = HI.UL.ReadExcel.Read(_fileName, "Sheet1", 0)

            Dim _FTStartDate As String = ""


            If (_dt.Rows.Count > 1) Then
                For Each R As DataRow In _dt.Rows

                    If (Val(R!F1.ToString) = False) Then
                        R.Delete()

                        Exit For
                    End If


                Next






                _Qry = " DELETE FROM " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & ".dbo.TmpTPRODTEditScanQuantity WHERE FTUser='" & HI.ST.UserInfo.UserName & "'"

                If HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_PROD) = False Then

                End If

                For i As Integer = _dt.Rows.Count - 1 To 0 Step -1
                    If _dt.Rows(i)(1).ToString().ToLower() = "" Then


                        _dt.Rows.Remove(_dt.Rows(i))



                    End If
                    _FTStartDate = _dt.Rows(1)(1).ToString().ToLower()
                Next


                _dt.Columns.Add("FTUser", GetType(String))
                For Each R As DataRow In _dt.Rows
                    R!FTUser = HI.ST.UserInfo.UserName
                Next

                Using con As New SqlConnection(HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PROD))
                    Using sqlBulkCopy As New SqlBulkCopy(con)
                        'Set the database table name
                        sqlBulkCopy.DestinationTableName = "dbo.TmpTPRODTEditScanQuantity "

                        '[OPTIONAL]: Map the DataTable columns with that of the database table




                        sqlBulkCopy.ColumnMappings.Add("F1", "FTUnitSectCode")
                        sqlBulkCopy.ColumnMappings.Add("F2", "FDScanDate")
                        sqlBulkCopy.ColumnMappings.Add("F3", "FTOrderNo")
                        sqlBulkCopy.ColumnMappings.Add("F4", "FTSubOrderNo")
                        sqlBulkCopy.ColumnMappings.Add("F5", "FTStateSewPack_txt")
                        sqlBulkCopy.ColumnMappings.Add("F6", "H01")
                        sqlBulkCopy.ColumnMappings.Add("F7", "H02")
                        sqlBulkCopy.ColumnMappings.Add("F8", "H03")
                        sqlBulkCopy.ColumnMappings.Add("F9", "H04")
                        sqlBulkCopy.ColumnMappings.Add("F10", "H05")
                        sqlBulkCopy.ColumnMappings.Add("F11", "H06")
                        sqlBulkCopy.ColumnMappings.Add("F12", "H07")
                        sqlBulkCopy.ColumnMappings.Add("F13", "H08")
                        sqlBulkCopy.ColumnMappings.Add("F14", "H09")
                        sqlBulkCopy.ColumnMappings.Add("F15", "H10")
                        sqlBulkCopy.ColumnMappings.Add("F16", "H11")
                        sqlBulkCopy.ColumnMappings.Add("F17", "H12")
                        sqlBulkCopy.ColumnMappings.Add("F18", "H13")
                        sqlBulkCopy.ColumnMappings.Add("F19", "Total")
                        sqlBulkCopy.ColumnMappings.Add("FTUser", "FTUser")




                        con.Open()
                        sqlBulkCopy.WriteToServer(_dt)
                        con.Close()
                    End Using
                End Using



                Call ImportData()
                If _FTStartDate <> "" Then
                    FTStartDate.Text = _FTStartDate
                    Call LoadDataInfo()
                End If


            End If

        Catch ex As Exception
            MsgBox(ex.Message.ToString)

        End Try

    End Sub

    Private Sub ocmimportexcel_Click(sender As Object, e As EventArgs)

    End Sub
End Class
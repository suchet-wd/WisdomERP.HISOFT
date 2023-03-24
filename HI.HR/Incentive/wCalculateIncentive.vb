'Imports System.Windows.Forms
'Imports DevExpress.XtraEditors.Controls
'Imports DevExpress.XtraGrid
'Imports DevExpress.XtraGrid.Views.Grid
'Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
'Imports DevExpress.XtraGrid.Columns
'Imports DevExpress.Utils
'Imports DevExpress.XtraGrid.Views.Layout
'Imports System.Drawing

Public Class wCalculateIncentive

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        Call InitGrid()
        Call InitGridCutBu()

    End Sub

#Region "Initial Grid"
    Private Sub InitGrid()

        '------Start Add Summary Grid-------------
        Dim sFieldCount As String = ""
        Dim sFieldSum As String = "H01|H02|H03|H04|H05|H06|H07|H08|H09|H10|H11|H12|H13|Total|TotalPack"
        Dim sFieldGrpCount As String = ""
        Dim sFieldGrpSum As String = "H01|H02|H03|H04|H05|H06|H07|H08|H09|H10|H11|H12|H13|Total|TotalPack"

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

    Private Sub InitGridCutBu()

        '------Start Add Summary Grid-------------
        Dim sFieldCount As String = ""
        Dim sFieldSum As String = "FNIncentiveAmt"
        Dim sFieldGrpCount As String = ""
        Dim sFieldGrpSum As String = "FNIncentiveAmt"

        With ogv1
            .ClearGrouping()
            .ClearDocument()
            .Columns.ColumnByFieldName("FDScanDateOrg").Group()
            For Each Str As String In sFieldCount.Split("|")
                If Str <> "" Then
                    .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Count, Str)
                    .Columns(Str).SummaryItem.DisplayFormat = "{0:n2}"
                End If
            Next

            For Each Str As String In sFieldSum.Split("|")
                If Str <> "" Then
                    .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Sum, Str)
                    .Columns(Str).SummaryItem.DisplayFormat = "{0:n2}"
                End If
            Next

            For Each Str As String In sFieldGrpCount.Split("|")
                If Str <> "" Then
                    .GroupSummary.Add(DevExpress.Data.SummaryItemType.Count, Str, Nothing, "(Count by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n2})")
                End If
            Next

            For Each Str As String In sFieldGrpSum.Split("|")
                If Str <> "" Then
                    .GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, Str, Nothing, "(Sum by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n2})")
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

    Private Sub ProcessSave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmcalculateincentive.Click
        Dim _StateProcess As Boolean = False
        If Me.VerrifyData Then

            If HI.MG.ShowMsg.mConfirmProcess("คุณต้องการทำการคำนวณ Incentive ของพนักงาน ใช่หรือไม่ ?", 1604220574, Me.Text) = True Then

                Dim _Spls As New HI.TL.SplashScreen("Calculating...   Please Wait   ")

                Call DeleteData(_Spls)

                Select Case Me.FNCalculateIncentiveType.SelectedIndex
                    Case 0
                        _StateProcess = CalculateSewingHour(_Spls)

                    Case 1
                        _StateProcess = CalculateCutBU(_Spls)

                    Case 2
                        _StateProcess = CalculateStockFabric(_Spls)

                    Case 3
                        _StateProcess = CalculateCutAuto(_Spls)

                    Case 4
                        _StateProcess = CalculateHeat(_Spls)

                    Case 5
                        _StateProcess = CalculateEmbPrint(_Spls)

                    Case 6
                        _StateProcess = CalculateLaser(_Spls)

                    Case 7
                        _StateProcess = CalculatePadprint(_Spls)

                    Case 8
                        _StateProcess = CalculateCutCP(_Spls)

                    Case Else
                        _StateProcess = CalculateSewing(_Spls)

                End Select

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
        _dtemptime.Columns.Add("FTStateTrain", GetType(String))
        _dtemptime.Columns.Add("FTStateTimeMax", GetType(String))

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

        '_dtemptime.Columns.Add("FNLeaveTotalPayMinute", GetType(Integer))
        '_dtemptime.Columns.Add("FNLeaveTotalNotPayMinute", GetType(Integer))


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

        Dim _FNLeaveTotalPayMinute As Integer = 0
        Dim _FNLeaveTotalNotPayMinute As Integer = 0

        Dim dtcheckTime As New DataTable

        _Qry = "  Select  Top 1 A.FNHSysShiftID,S.FTIn1, S.FTOut1, FTIn2, S.FTOut2 "
        _Qry &= vbCrLf & "  From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS A With(NOLOCK)  "
        _Qry &= vbCrLf & " INNER JOIN   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMTimeShift AS S With(NOLOCK)  ON A.FNHSysShiftID = S.FNHSysShiftID"
        _Qry &= vbCrLf & " Where (A.FNHSysUnitSectId = " & _UnitSectID & ") "
        _Qry &= vbCrLf & " And (A.FNEmpStatus  in (0,1)) "
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

        _Qry = "  Select  Top 1 MV.FNHSysShiftID,S.FTIn1, S.FTOut1, FTIn2, S.FTOut2 "
        _Qry &= vbCrLf & "  From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS A With(NOLOCK)  "
        _Qry &= vbCrLf & " INNER JOIN   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployeeMoveShift AS MV With(NOLOCK)  ON A.FNHSysEmpID = MV.FNHSysEmpID"
        _Qry &= vbCrLf & " INNER JOIN   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMTimeShift AS S With(NOLOCK)  ON MV.FNHSysShiftID = S.FNHSysShiftID"
        _Qry &= vbCrLf & " Where (A.FNHSysUnitSectId = " & _UnitSectID & ") "
        _Qry &= vbCrLf & "  And (A.FNEmpStatus in (0,1) ) "
        _Qry &= vbCrLf & "  AND (MV.FDShiftDate ='" & HI.UL.ULDate.ConvertEnDB(_CalDate) & "') "
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


        '  _Qry = "EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.SP_GET_EMPTIME_CAL_INCENTIVE " & _UnitSectID & ",'" & HI.UL.ULDate.ConvertEnDB(_CalDate) & "'," & _TFNHSysEmpID & ""
        _Qry = "EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.SP_GET_EMPTIME_CAL_INCENTIVE_PROD_EFF " & _UnitSectID & ",'" & HI.UL.ULDate.ConvertEnDB(_CalDate) & "'," & _TFNHSysEmpID & ""
        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

        For Each R As DataRow In _dt.Rows

            _FNHSysEmpID = Integer.Parse(Val(R!FNHSysEmpID.ToString))

            If _FNHSysEmpID = 1403020124 Then
                Beep()
            End If

            _Qry = " Select  TOP 1 FTTimeOut  "
            _Qry &= vbCrLf & " From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTDailySpecial  AS X WITH(NOLOCK)"
            _Qry &= vbCrLf & " WHERE FNHSysEmpID=" & _FNHSysEmpID & ""
            _Qry &= vbCrLf & " AND FTDate='" & HI.UL.ULDate.ConvertEnDB(_CalDate) & "'"
            _LeaveHomeSpecial = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "")


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

            '_FNLeaveTotalPayMinute = Val(R!FNLeaveTotalPayMinute.ToString)
            '_FNLeaveTotalNotPayMinute = Val(R!FNLeaveTotalNotPayMinute.ToString)

            _Qry = " Select FNHSysEmpID, FTDateTrans, FTLeaveType, FTLeaveStartTime, FTLeaveEndTime"
            _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTransLeave As X With(NOLOCK)"
            _Qry &= vbCrLf & " WHERE FNHSysEmpID= " & _FNHSysEmpID & " "
            _Qry &= vbCrLf & " And FTDateTrans='" & HI.UL.ULDate.ConvertEnDB(_CalDate) & "'"

            _dttimeLeave = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

            For Each Rxt As DataRow In _dttime.Rows

                _StartTime = Rxt!FTStartTime.ToString
                _EndTime = Rxt!FTEndTime.ToString

                _FNHour = Integer.Parse(Val(Rxt!FNHour.ToString))
                _FNWorkMinute = 0
                _FNWorkMinuteHR = 0
                ' And (_FTEndTime >= _EndTime) 

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


                If _dtmove.Select("FNHSysEmpID=" & _FNHSysEmpID & " And FTStartTime<='" & _StartTime & "' AND FTEndTime>='" & _EndTime & "'").Length <= 0 Then

                    Select Case _FNHour
                        Case 1, 2, 3, 4
                            If _FNHSysEmpID = 1959740010 Then
                                Dim X As String
                                X = "99999"
                            End If
                            If _FTState = "1" Then

                                Select Case True
                                    Case (_FTIn1 <= _StartTime And _FTOut1 >= _EndTime) And (_FTStartTime <= _StartTime And _FTEndTime >= _EndTime)

                                        '_FNWorkMinute = 60
                                        If _dttimeLeave.Select("FTLeaveStartTime<='" & _StartTime & "' AND FTLeaveEndTime>='" & _EndTime & "' ").Length <= 0 Then
                                            '_FNWorkMinute = 60
                                            '_FNWorkMinuteHR = 60


                                            _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _EndTime))
                                            _FNWorkMinuteHR = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _EndTime))

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
                                                    ' _FNWorkMinuteHR = 60

                                                    _FNWorkMinuteHR = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _EndTime))
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
                                                    ' _FNWorkMinuteHR = 60


                                                    _FNWorkMinuteHR = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _EndTime))

                                                End If

                                            Else
                                                _FNWorkMinute = 0
                                                _FNWorkMinuteHR = 0
                                            End If

                                        Catch ex As Exception
                                        End Try


                                         '' hour 4  = 12:00   FTState = 0
                                    Case (_StartTime = "12:00" And _StartTime >= _FTIn1 And _FTOut1 < _StartTime And _FTIn1 <> "") And (_StartTime <= _FTIn2 And _FTOut2 > _EndTime And _FTIn2 <> "" And _FTOut2 <> "" And _FTIn2 < _FTOut2)
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
                                                    _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _EndTime))
                                                    ' _FNWorkMinuteHR = 60


                                                    _FNWorkMinuteHR = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _EndTime))
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
                                            '_FNWorkMinute = 60
                                            '_FNWorkMinuteHR = 60

                                            '_FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _EndTime))
                                            '_FNWorkMinuteHR = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _EndTime))

                                            If _dtmove.Select("FNHSysEmpID=" & _FNHSysEmpID & " And FTStartTime > '" & _StartTime & "'   AND FTEndTime>='" & _EndTime & "'").Length > 0 Then

                                                For Each Rz As DataRow In _dtmove.Select("FNHSysEmpID=" & _FNHSysEmpID & " And FTStartTime > '" & _StartTime & "' And FTStartTime < '" & _EndTime & "'  AND FTEndTime>='" & _EndTime & "'")
                                                    _EndTime = Rz!FTStartTime.ToString
                                                    Exit For
                                                Next

                                                _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _EndTime))
                                                _FNWorkMinuteHR = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _EndTime))

                                            Else
                                                _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _EndTime))
                                                _FNWorkMinuteHR = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _EndTime))

                                            End If
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
                                                    ' _FNWorkMinuteHR = 60


                                                    _FNWorkMinuteHR = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _EndTime))
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
                                                    ' _FNWorkMinuteHR = 60


                                                    _FNWorkMinuteHR = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _EndTime))
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
                                                    ' _FNWorkMinuteHR = 60


                                                    _FNWorkMinuteHR = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _EndTime))
                                                End If

                                            Else
                                                _FNWorkMinute = 0
                                                _FNWorkMinuteHR = 0
                                            End If

                                        Catch ex As Exception
                                        End Try

                                        '' hour 4  = 12:00   FTState = 0
                                    Case (_StartTime = "12:00" And _StartTime >= _FTIn1 And _FTOut1 < _StartTime And _FTIn1 <> "") And (_StartTime <= _FTIn2 And _FTOut2 > _EndTime And _FTIn2 <> "" And _FTOut2 <> "" And _FTIn2 < _FTOut2)
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
                                                    _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _EndTime))
                                                    ' _FNWorkMinuteHR = 60


                                                    _FNWorkMinuteHR = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _EndTime))
                                                End If

                                            Else
                                                _FNWorkMinute = 0
                                                _FNWorkMinuteHR = 0
                                            End If

                                        Catch ex As Exception
                                        End Try



                                        'add check ออกงานก่อนเวลา
                                    Case (_StartTime >= _FTIn1 And _FTOut1 < _EndTime And _FTIn1 <> "")
                                        Try
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
                                                    _FNWorkMinuteHR = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _FTOut1))


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
                            If _FNHSysEmpID = 1959740010 Then
                                Dim X As String
                                X = "99999"
                            End If
                            If _FTState = "1" Then

                                Select Case True

                                    Case (_FTIn2 <= _StartTime And _FTOut2 >= _EndTime) And (_FTStartTime <= _StartTime And _FTEndTime >= _EndTime) And (_FTIn2 <> "" And _FTOut2 <> "")

                                        ' _FNWorkMinute = 60

                                        If _dttimeLeave.Select("FTLeaveStartTime<='" & _StartTime & "' AND FTLeaveEndTime>='" & _EndTime & "' ").Length <= 0 Then
                                            '_FNWorkMinute = 60
                                            '_FNWorkMinuteHR = 60

                                            If _dtmove.Select("FNHSysEmpID=" & _FNHSysEmpID & " And FTStartTime > '" & _StartTime & "'   AND FTEndTime>='" & _EndTime & "'").Length > 0 Then

                                                For Each Rz As DataRow In _dtmove.Select("FNHSysEmpID=" & _FNHSysEmpID & " And FTStartTime > '" & _StartTime & "' And FTStartTime < '" & _EndTime & "'  AND FTEndTime>='" & _EndTime & "'")
                                                    _EndTime = Rz!FTStartTime.ToString
                                                    Exit For
                                                Next

                                                _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _EndTime))
                                                _FNWorkMinuteHR = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _EndTime))

                                            Else
                                                _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _EndTime))
                                                _FNWorkMinuteHR = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _EndTime))

                                            End If

                                            '_FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _EndTime))
                                            '_FNWorkMinuteHR = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _EndTime))

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
                                                    ' _FNWorkMinuteHR = 60


                                                    _FNWorkMinuteHR = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _EndTime))

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
                                                    ' _FNWorkMinuteHR = 60


                                                    _FNWorkMinuteHR = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _EndTime))

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
                                                    ' _FNWorkMinuteHR = 60


                                                    _FNWorkMinuteHR = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _EndTime))
                                                End If


                                            Else
                                                _FNWorkMinute = 0
                                                _FNWorkMinuteHR = 0
                                            End If

                                        Catch ex As Exception
                                        End Try
                                    Case (_FTStartTime <= _EndTime And _FTIn2 <> "" And _FTOut2 <> "" And _FTEndTime >= _EndTime)
                                        Try

                                            _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTStartTime), CDate(_CalDate & " " & _EndTime))



                                            _FNWorkMinuteHR = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTStartTime), CDate(_CalDate & " " & _EndTime))
                                        Catch ex As Exception

                                        End Try
                                End Select

                            Else
                                If _dtmove.Select("FNHSysEmpID=" & _FNHSysEmpID & "   AND FTEndTime>='" & _StartTime & "'   AND FTEndTime<='" & _EndTime & "'").Length > 0 Then

                                    For Each Rz As DataRow In _dtmove.Select("FNHSysEmpID=" & _FNHSysEmpID & "  AND FTEndTime>='" & _StartTime & "'   AND FTEndTime<='" & _EndTime & "'")
                                        _StartTime = Rz!FTEndTime.ToString
                                        Exit For
                                    Next
                                End If

                                Select Case True




                                        '_FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _EndTime))
                                        '_FNWorkMinuteHR = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _EndTime))

                                    'Else
                                    '    '_FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _EndTime))
                                    '    '_FNWorkMinuteHR = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _EndTime))

                                    'End If


                                    Case (_FTIn2 <= _StartTime And _FTOut2 >= _EndTime) And (_FTIn2 <> "" And _FTOut2 <> "")

                                        '_FNWorkMinute = 60
                                        If _dttimeLeave.Select("FTLeaveStartTime<='" & _StartTime & "' AND FTLeaveEndTime>='" & _EndTime & "' ").Length <= 0 Then
                                            '_FNWorkMinute = 60
                                            '_FNWorkMinuteHR = 60

                                            If _dtmove.Select("FNHSysEmpID=" & _FNHSysEmpID & " And FTStartTime > '" & _StartTime & "'   AND FTEndTime>='" & _EndTime & "'").Length > 0 Then

                                                For Each Rz As DataRow In _dtmove.Select("FNHSysEmpID=" & _FNHSysEmpID & " And FTStartTime > '" & _StartTime & "' And FTStartTime < '" & _EndTime & "'  AND FTEndTime>='" & _EndTime & "'")
                                                    _EndTime = Rz!FTStartTime.ToString
                                                    Exit For
                                                Next

                                                _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _EndTime))
                                                _FNWorkMinuteHR = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _EndTime))

                                            Else


                                                _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _EndTime))
                                                _FNWorkMinuteHR = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _EndTime))

                                            End If


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
                                                    ' _FNWorkMinuteHR = 60


                                                    _FNWorkMinuteHR = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _EndTime))
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
                                                    ' _FNWorkMinuteHR = 60


                                                    _FNWorkMinuteHR = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _EndTime))
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
                                                    ' _FNWorkMinuteHR = 60


                                                    _FNWorkMinuteHR = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _EndTime))
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

                            If _FNHSysEmpID = 1403020124 Then
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
                                                    '_FNWorkMinute = 60
                                                    '_FNWorkMinuteHR = 60

                                                    _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _EndTime))
                                                    _FNWorkMinuteHR = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _EndTime))

                                                Else
                                                    If _dttimeLeave.Select("FTLeaveStartTime<='" & _StartTime & "' AND FTLeaveEndTime>='" & _EndTime & "' ").Length <= 0 And _dtmove.Select("FNHSysEmpID=" & _FNHSysEmpID & " AND FTStartTime<='" & _StartTime & "' AND FTEndTime>='" & _FTOut3 & "'").Length <= 0 Then
                                                        '_FNWorkMinute = 60
                                                        '_FNWorkMinuteHR = 60

                                                        _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _EndTime))
                                                        _FNWorkMinuteHR = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _EndTime))
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
                                                            '  _FNWorkMinuteHR = 60


                                                            _FNWorkMinuteHR = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _EndTime))
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
                                                            ' _FNWorkMinuteHR = 60


                                                            _FNWorkMinuteHR = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _EndTime))
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


                                                            '  _FNWorkMinuteHR = 60

                                                            _FNWorkMinuteHR = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _EndTime))
                                                        End If

                                                    Else
                                                        _FNWorkMinute = 0
                                                        _FNWorkMinuteHR = 0
                                                    End If
                                                Catch ex As Exception
                                                End Try
                                            Case (_StartTime >= _FTIn3 And _FTOut3 < _EndTime And _FTIn3 <> "" And _FTOut3 <> "" And _FTIn3 > _FTOut2 And _StartTime < _FTOut3 And _EndTime <= _FTEndTime)

                                                Try
                                                    '_FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _FTOut3))
                                                    ''
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
                                                            ' _FNWorkMinuteHR = 60


                                                            _FNWorkMinuteHR = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _EndTime))

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
                                                    '_FNWorkMinute = 60
                                                    '_FNWorkMinuteHR = 60

                                                    '_FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _EndTime))
                                                    '_FNWorkMinuteHR = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _EndTime))

                                                    If _dtmove.Select("FNHSysEmpID=" & _FNHSysEmpID & " And FTStartTime > '" & _StartTime & "'   AND FTEndTime>='" & _EndTime & "'").Length > 0 Then

                                                        For Each Rz As DataRow In _dtmove.Select("FNHSysEmpID=" & _FNHSysEmpID & " And FTStartTime > '" & _StartTime & "' And FTStartTime < '" & _EndTime & "'  AND FTEndTime>='" & _EndTime & "'")
                                                            _EndTime = Rz!FTStartTime.ToString
                                                            Exit For
                                                        Next

                                                        _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _EndTime))
                                                        _FNWorkMinuteHR = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _EndTime))

                                                    Else
                                                        _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _EndTime))
                                                        _FNWorkMinuteHR = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _EndTime))

                                                    End If



                                                Else
                                                    ' _FNWorkMinute = 60
                                                    If _dttimeLeave.Select("FTLeaveStartTime<='" & _StartTime & "' AND FTLeaveEndTime>='" & _EndTime & "' ").Length <= 0 And _dtmove.Select("FNHSysEmpID=" & _FNHSysEmpID & " AND FTStartTime<='" & _StartTime & "' AND FTEndTime>='" & _FTOut3 & "'").Length <= 0 Then
                                                        '_FNWorkMinute = 60
                                                        '_FNWorkMinuteHR = 60

                                                        _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _EndTime))
                                                        _FNWorkMinuteHR = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _EndTime))

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
                                                            If _dtmove.Select("FNHSysEmpID=" & _FNHSysEmpID & " And FTStartTime > '" & _StartTime & "'   AND FTEndTime>='" & _EndTime & "'").Length > 0 Then

                                                                For Each Rz As DataRow In _dtmove.Select("FNHSysEmpID=" & _FNHSysEmpID & " And FTStartTime > '" & _StartTime & "' And FTStartTime < '" & _EndTime & "'  AND FTEndTime>='" & _EndTime & "'")
                                                                    _EndTime = Rz!FTStartTime.ToString
                                                                    Exit For
                                                                Next

                                                                _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _EndTime))
                                                                _FNWorkMinuteHR = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _EndTime))

                                                            Else
                                                                _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _EndTime))
                                                                _FNWorkMinuteHR = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _EndTime))

                                                            End If

                                                            '_FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn3), CDate(_CalDate & " " & _EndTime))
                                                            '' _FNWorkMinuteHR = 60


                                                            '_FNWorkMinuteHR = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _EndTime))

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
                                                            If _dtmove.Select("FNHSysEmpID=" & _FNHSysEmpID & " And FTStartTime > '" & _StartTime & "'   AND FTEndTime>='" & _EndTime & "'").Length > 0 Then

                                                                For Each Rz As DataRow In _dtmove.Select("FNHSysEmpID=" & _FNHSysEmpID & " And FTStartTime > '" & _StartTime & "' And FTStartTime < '" & _EndTime & "'  AND FTEndTime>='" & _EndTime & "'")
                                                                    _EndTime = Rz!FTStartTime.ToString
                                                                    Exit For
                                                                Next

                                                                _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _EndTime))
                                                                _FNWorkMinuteHR = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _EndTime))

                                                            Else
                                                                _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _EndTime))
                                                                _FNWorkMinuteHR = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _EndTime))

                                                            End If

                                                            '_FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn3), CDate(_CalDate & " " & _FTOut3))
                                                            '' _FNWorkMinuteHR = 60


                                                            '_FNWorkMinuteHR = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _EndTime))

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
                                                            ' _FNWorkMinuteHR = 60


                                                            _FNWorkMinuteHR = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _EndTime))

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
                    'Select Case _FNHour
                    '    Case 1, 2, 3, 4

                    '        If _FTState = "1" Then

                    '            Select Case True
                    '                Case (_FTIn1 <= _StartTime And _FTOut1 >= _EndTime) And (_FTStartTime <= _StartTime And _FTEndTime >= _EndTime)
                    '                    '_FNWorkMinute = 60
                    '                    If _dttimeLeave.Select("FTLeaveStartTime<='" & _StartTime & "' AND FTLeaveEndTime>='" & _EndTime & "' ").Length <= 0 Then
                    '                        _FNWorkMinute = 60
                    '                    Else
                    '                        _FNWorkMinute = 0
                    '                    End If

                    '                Case (_StartTime < _FTIn1 And _FTOut1 >= _EndTime And _FTIn1 <> "") And (_FTStartTime <= _StartTime And _FTEndTime >= _EndTime)

                    '                    Try
                    '                        ' _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn1), CDate(_CalDate & " " & _EndTime))

                    '                        If _dttimeLeave.Select("FTLeaveStartTime<='" & _StartTime & "' AND FTLeaveEndTime>='" & _EndTime & "' ").Length <= 0 Then
                    '                            _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn1), CDate(_CalDate & " " & _EndTime))
                    '                        Else
                    '                            _FNWorkMinute = 0
                    '                        End If

                    '                    Catch ex As Exception
                    '                    End Try

                    '                Case (_StartTime < _FTIn1 And _FTOut1 < _EndTime And _FTIn1 <> "" And _FTOut1 <> "" And _FTIn1 < _FTOut1) And (_FTStartTime <= _StartTime And _FTEndTime >= _EndTime)

                    '                    Try
                    '                        ' _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn1), CDate(_CalDate & " " & _FTOut1))

                    '                        If _dttimeLeave.Select("FTLeaveStartTime<='" & _StartTime & "' AND FTLeaveEndTime>='" & _EndTime & "' ").Length <= 0 Then
                    '                            _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn1), CDate(_CalDate & " " & _FTOut1))
                    '                        Else
                    '                            _FNWorkMinute = 0
                    '                        End If

                    '                    Catch ex As Exception
                    '                    End Try
                    '                Case (_StartTime >= _FTIn1 And _FTOut1 < _EndTime And _FTIn1 <> "")
                    '                    Try
                    '                        ' _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _FTOut1))

                    '                        If _dttimeLeave.Select("FTLeaveStartTime<='" & _StartTime & "' AND FTLeaveEndTime>='" & _EndTime & "' ").Length <= 0 Then
                    '                            _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _FTOut1))
                    '                        Else
                    '                            _FNWorkMinute = 0
                    '                        End If

                    '                    Catch ex As Exception
                    '                    End Try
                    '            End Select

                    '        Else

                    '            Select Case True
                    '                Case (_FTIn1 <= _StartTime And _FTOut1 >= _EndTime And _FTIn1 <> "")

                    '                    ' _FNWorkMinute = 60
                    '                    If _dttimeLeave.Select("FTLeaveStartTime<='" & _StartTime & "' AND FTLeaveEndTime>='" & _EndTime & "' ").Length <= 0 Then
                    '                        _FNWorkMinute = 60
                    '                    Else
                    '                        _FNWorkMinute = 0
                    '                    End If
                    '                Case (_StartTime < _FTIn1 And _FTOut1 >= _EndTime And _FTIn1 <> "")

                    '                    Try
                    '                        '_FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn1), CDate(_CalDate & " " & _EndTime))

                    '                        If _dttimeLeave.Select("FTLeaveStartTime<='" & _StartTime & "' AND FTLeaveEndTime>='" & _EndTime & "' ").Length <= 0 Then
                    '                            _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn1), CDate(_CalDate & " " & _EndTime))
                    '                        Else
                    '                            _FNWorkMinute = 0
                    '                        End If

                    '                    Catch ex As Exception
                    '                    End Try

                    '                Case (_StartTime < _FTIn1 And _FTOut1 < _EndTime And _FTIn1 <> "" And _FTOut1 <> "" And _FTIn1 < _FTOut1)

                    '                    Try
                    '                        ' _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn1), CDate(_CalDate & " " & _FTOut1))

                    '                        If _dttimeLeave.Select("FTLeaveStartTime<='" & _StartTime & "' AND FTLeaveEndTime>='" & _EndTime & "' ").Length <= 0 Then
                    '                            _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn1), CDate(_CalDate & " " & _FTOut1))
                    '                        Else
                    '                            _FNWorkMinute = 0
                    '                        End If

                    '                    Catch ex As Exception
                    '                    End Try
                    '                Case (_StartTime >= _FTIn1 And _FTOut1 < _EndTime And _FTIn1 <> "")
                    '                    Try
                    '                        ' _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _FTOut1))

                    '                        If _dttimeLeave.Select("FTLeaveStartTime<='" & _StartTime & "' AND FTLeaveEndTime>='" & _EndTime & "' ").Length <= 0 Then
                    '                            _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _FTOut1))
                    '                        Else
                    '                            _FNWorkMinute = 0
                    '                        End If

                    '                    Catch ex As Exception
                    '                    End Try
                    '            End Select

                    '        End If

                    '    Case 5, 6, 7, 8

                    '        If _FTState = "1" Then

                    '            Select Case True

                    '                Case (_FTIn2 <= _StartTime And _FTOut2 >= _EndTime) And (_FTStartTime <= _StartTime And _FTEndTime >= _EndTime) And (_FTIn2 <> "" And _FTOut2 <> "")

                    '                    ' _FNWorkMinute = 60

                    '                    If _dttimeLeave.Select("FTLeaveStartTime<='" & _StartTime & "' AND FTLeaveEndTime>='" & _EndTime & "' ").Length <= 0 Then
                    '                        _FNWorkMinute = 60
                    '                    Else
                    '                        _FNWorkMinute = 0
                    '                    End If

                    '                Case (_StartTime < _FTIn2 And _FTOut2 >= _EndTime And _FTIn2 <> "") And (_FTStartTime <= _StartTime And _FTEndTime >= _EndTime) And (_FTIn2 <> "" And _FTOut2 <> "")

                    '                    Try
                    '                        ' _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn2), CDate(_CalDate & " " & _EndTime))

                    '                        If _dttimeLeave.Select("FTLeaveStartTime<='" & _StartTime & "' AND FTLeaveEndTime>='" & _EndTime & "' ").Length <= 0 Then
                    '                            _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn2), CDate(_CalDate & " " & _EndTime))
                    '                        Else
                    '                            _FNWorkMinute = 0
                    '                        End If

                    '                    Catch ex As Exception
                    '                    End Try

                    '                Case (_StartTime < _FTIn2 And _FTOut2 < _EndTime And _FTIn2 <> "" And _FTOut2 <> "" And _FTIn2 < _FTOut2) And (_FTStartTime <= _StartTime And _FTEndTime >= _EndTime) And (_FTIn2 <> "" And _FTOut2 <> "")

                    '                    Try
                    '                        '_FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn2), CDate(_CalDate & " " & _FTOut2))
                    '                        If _dttimeLeave.Select("FTLeaveStartTime<='" & _StartTime & "' AND FTLeaveEndTime>='" & _EndTime & "' ").Length <= 0 Then
                    '                            _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn2), CDate(_CalDate & " " & _FTOut2))
                    '                        Else
                    '                            _FNWorkMinute = 0
                    '                        End If
                    '                    Catch ex As Exception
                    '                    End Try
                    '                Case (_StartTime >= _FTIn2 And _FTOut2 < _EndTime And _FTIn2 <> "") And (_FTIn2 <> "" And _FTOut2 <> "")
                    '                    Try
                    '                        ' _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _FTOut2))

                    '                        If _dttimeLeave.Select("FTLeaveStartTime<='" & _StartTime & "' AND FTLeaveEndTime>='" & _EndTime & "' ").Length <= 0 Then
                    '                            _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _FTOut2))
                    '                        Else
                    '                            _FNWorkMinute = 0
                    '                        End If

                    '                    Catch ex As Exception
                    '                    End Try
                    '            End Select

                    '        Else

                    '            Select Case True

                    '                Case (_FTIn2 <= _StartTime And _FTOut2 >= _EndTime) And (_FTIn2 <> "" And _FTOut2 <> "")

                    '                    '_FNWorkMinute = 60
                    '                    If _dttimeLeave.Select("FTLeaveStartTime<='" & _StartTime & "' AND FTLeaveEndTime>='" & _EndTime & "' ").Length <= 0 Then
                    '                        _FNWorkMinute = 60
                    '                    Else
                    '                        _FNWorkMinute = 0
                    '                    End If

                    '                Case (_StartTime < _FTIn2 And _FTOut2 >= _EndTime And _FTIn2 <> "") And (_FTIn2 <> "" And _FTOut2 <> "")

                    '                    Try
                    '                        ' _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn2), CDate(_CalDate & " " & _EndTime))

                    '                        If _dttimeLeave.Select("FTLeaveStartTime<='" & _StartTime & "' AND FTLeaveEndTime>='" & _EndTime & "' ").Length <= 0 Then
                    '                            _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn2), CDate(_CalDate & " " & _EndTime))
                    '                        Else
                    '                            _FNWorkMinute = 0
                    '                        End If

                    '                    Catch ex As Exception
                    '                    End Try

                    '                Case (_StartTime < _FTIn2 And _FTOut2 < _EndTime And _FTIn2 <> "" And _FTOut2 <> "" And _FTIn2 < _FTOut2) And (_FTIn2 <> "" And _FTOut2 <> "")

                    '                    Try
                    '                        ' _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn2), CDate(_CalDate & " " & _FTOut2))

                    '                        If _dttimeLeave.Select("FTLeaveStartTime<='" & _StartTime & "' AND FTLeaveEndTime>='" & _EndTime & "' ").Length <= 0 Then
                    '                            _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn2), CDate(_CalDate & " " & _FTOut2))
                    '                        Else
                    '                            _FNWorkMinute = 0
                    '                        End If

                    '                    Catch ex As Exception
                    '                    End Try

                    '                Case (_StartTime >= _FTIn2 And _FTOut2 < _EndTime And _FTIn2 <> "") And (_FTIn2 <> "" And _FTOut2 <> "")

                    '                    Try
                    '                        '_FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _FTOut2))
                    '                        If _dttimeLeave.Select("FTLeaveStartTime<='" & _StartTime & "' AND FTLeaveEndTime>='" & _EndTime & "' ").Length <= 0 Then
                    '                            _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _FTOut2))
                    '                        Else
                    '                            _FNWorkMinute = 0
                    '                        End If
                    '                    Catch ex As Exception
                    '                    End Try

                    '            End Select

                    '        End If

                    '    Case 9, 10, 11, 12, 13

                    '        If _FNHSysEmpID = 1099001858 Then
                    '            Dim X As String
                    '            X = "99999"
                    '        End If

                    '        If _FNOTRequestMin > 0 Then

                    '            If _dtmove.Select("FNHSysEmpID=" & _FNHSysEmpID & " AND FTStartTime<='" & _FTIn3 & "' AND FTEndTime>='" & _FTOut3 & "'").Length > 0 Then
                    '                _FNWorkMinute = 0
                    '            Else
                    '                If _FTState = "1" Then

                    '                    Select Case True

                    '                        Case (((_FTIn3 <= _StartTime And _FTOut3 >= _EndTime) And (_FTStartTime <= _StartTime And _FTEndTime >= _EndTime)) Or ((_FTIn3 = TimeCheckOut2 And _FNHour = 9)))

                    '                            ' _FNWorkMinute = 60
                    '                            If _FTIn3 = TimeCheckOut2 Then
                    '                                _FNWorkMinute = 60
                    '                            Else
                    '                                If _dttimeLeave.Select("FTLeaveStartTime<='" & _StartTime & "' AND FTLeaveEndTime>='" & _EndTime & "' ").Length <= 0 And _dtmove.Select("FNHSysEmpID=" & _FNHSysEmpID & " AND FTStartTime<='" & _StartTime & "' AND FTEndTime>='" & _FTOut3 & "'").Length <= 0 Then
                    '                                    _FNWorkMinute = 60
                    '                                Else
                    '                                    _FNWorkMinute = 0
                    '                                End If
                    '                            End If


                    '                        Case (_StartTime < _FTIn3 And _FTOut3 >= _EndTime And _FTIn3 <> "") And (_FTStartTime <= _StartTime And _FTEndTime >= _EndTime)

                    '                            Try
                    '                                ' _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn3), CDate(_CalDate & " " & _EndTime))
                    '                                If _dttimeLeave.Select("FTLeaveStartTime<='" & _StartTime & "' AND FTLeaveEndTime>='" & _EndTime & "' ").Length <= 0 And _dtmove.Select("FNHSysEmpID=" & _FNHSysEmpID & " AND FTStartTime<='" & _StartTime & "' AND FTEndTime>='" & _FTOut3 & "'").Length <= 0 Then
                    '                                    _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn3), CDate(_CalDate & " " & _EndTime))
                    '                                Else
                    '                                    _FNWorkMinute = 0
                    '                                End If
                    '                            Catch ex As Exception
                    '                            End Try

                    '                        Case (_StartTime < _FTIn3 And _FTOut3 < _EndTime And _FTIn3 <> "" And _FTOut3 <> "" And _FTIn3 < _FTOut2) And (_FTStartTime <= _StartTime And _FTEndTime >= _EndTime)

                    '                            Try
                    '                                '_FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn3), CDate(_CalDate & " " & _FTOut3))

                    '                                If _dttimeLeave.Select("FTLeaveStartTime<='" & _StartTime & "' AND FTLeaveEndTime>='" & _EndTime & "' ").Length <= 0 And _dtmove.Select("FNHSysEmpID=" & _FNHSysEmpID & " AND FTStartTime<='" & _StartTime & "' AND FTEndTime>='" & _FTOut3 & "'").Length <= 0 Then
                    '                                    _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn3), CDate(_CalDate & " " & _FTOut3))
                    '                                Else
                    '                                    _FNWorkMinute = 0
                    '                                End If

                    '                            Catch ex As Exception
                    '                            End Try

                    '                        Case (_StartTime >= _FTIn3 And _FTOut3 < _EndTime And _FTIn3 <> "" And _FTOut3 <> "" And _FTIn3 > _FTOut2 And _StartTime <= _FTOut3) And (_FTStartTime <= _StartTime And _FTEndTime >= _EndTime)

                    '                            Try
                    '                                '_FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _FTOut3))
                    '                                If _dttimeLeave.Select("FTLeaveStartTime<='" & _StartTime & "' AND FTLeaveEndTime>='" & _EndTime & "' ").Length <= 0 And _dtmove.Select("FNHSysEmpID=" & _FNHSysEmpID & " AND FTStartTime<='" & _StartTime & "' AND FTEndTime>='" & _FTOut3 & "'").Length <= 0 Then
                    '                                    _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _FTOut3))
                    '                                Else
                    '                                    _FNWorkMinute = 0
                    '                                End If
                    '                            Catch ex As Exception
                    '                            End Try

                    '                    End Select

                    '                Else

                    '                    Select Case True

                    '                        Case ((_FTIn3 <= _StartTime And _FTOut3 >= _EndTime) Or (_FTIn3 = TimeCheckOut2 And _FNHour = 9))


                    '                            If _FTIn3 = TimeCheckOut2 Then
                    '                                _FNWorkMinute = 60
                    '                            Else
                    '                                ' _FNWorkMinute = 60
                    '                                If _dttimeLeave.Select("FTLeaveStartTime<='" & _StartTime & "' AND FTLeaveEndTime>='" & _EndTime & "' ").Length <= 0 And _dtmove.Select("FNHSysEmpID=" & _FNHSysEmpID & " AND FTStartTime<='" & _StartTime & "' AND FTEndTime>='" & _FTOut3 & "'").Length <= 0 Then
                    '                                    _FNWorkMinute = 60
                    '                                Else
                    '                                    _FNWorkMinute = 0
                    '                                End If
                    '                            End If


                    '                        Case (_StartTime < _FTIn3 And _FTOut3 >= _EndTime And _FTIn3 <> "")

                    '                            Try
                    '                                '_FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn3), CDate(_CalDate & " " & _EndTime))
                    '                                If _dttimeLeave.Select("FTLeaveStartTime<='" & _StartTime & "' AND FTLeaveEndTime>='" & _EndTime & "' ").Length <= 0 And _dtmove.Select("FNHSysEmpID=" & _FNHSysEmpID & " AND FTStartTime<='" & _StartTime & "' AND FTEndTime>='" & _FTOut3 & "'").Length <= 0 Then
                    '                                    _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn3), CDate(_CalDate & " " & _EndTime))
                    '                                Else
                    '                                    _FNWorkMinute = 0
                    '                                End If
                    '                            Catch ex As Exception
                    '                            End Try

                    '                        Case (_StartTime < _FTIn3 And _FTOut3 < _EndTime And _FTIn3 <> "" And _FTOut3 <> "" And _FTIn3 < _FTOut2)

                    '                            Try
                    '                                '_FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn3), CDate(_CalDate & " " & _FTOut3))
                    '                                If _dttimeLeave.Select("FTLeaveStartTime<='" & _StartTime & "' AND FTLeaveEndTime>='" & _EndTime & "' ").Length <= 0 And _dtmove.Select("FNHSysEmpID=" & _FNHSysEmpID & " AND FTStartTime<='" & _StartTime & "' AND FTEndTime>='" & _FTOut3 & "'").Length <= 0 Then

                    '                                    _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn3), CDate(_CalDate & " " & _FTOut3))

                    '                                Else

                    '                                    _FNWorkMinute = 0

                    '                                End If

                    '                            Catch ex As Exception
                    '                            End Try

                    '                        Case (_StartTime >= _FTIn3 And _FTOut3 < _EndTime And _FTIn3 <> "" And _FTOut3 <> "" And _FTIn3 > _FTOut2 And _StartTime <= _FTOut3)

                    '                            Try
                    '                                '_FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _FTOut3))

                    '                                If _dttimeLeave.Select("FTLeaveStartTime<='" & _StartTime & "' AND FTLeaveEndTime>='" & _EndTime & "' ").Length <= 0 And _dtmove.Select("FNHSysEmpID=" & _FNHSysEmpID & " AND FTStartTime<='" & _StartTime & "' AND FTEndTime>='" & _FTOut3 & "'").Length <= 0 Then

                    '                                    _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _FTOut3))

                    '                                Else

                    '                                    _FNWorkMinute = 0

                    '                                End If

                    '                            Catch ex As Exception
                    '                            End Try

                    '                    End Select

                    '                End If
                    '            End If

                    '        End If

                    'End Select

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
                    ''_FNLeaveTotalPayMinute, _FNLeaveTotalNotPayMinute)

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

    Private Function GetEmpTime_20220705(_UnitSectID As Integer, _CalDate As String, Optional _TFNHSysEmpID As Integer = 0) As DataTable
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
        _dtemptime.Columns.Add("FTStateTrain", GetType(String))
        _dtemptime.Columns.Add("FTStateTimeMax", GetType(String))

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

        _Qry = "  Select  Top 1 MV.FNHSysShiftID,S.FTIn1, S.FTOut1, FTIn2, S.FTOut2 "
        _Qry &= vbCrLf & "  From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS A With(NOLOCK)  "
        _Qry &= vbCrLf & " INNER JOIN   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployeeMoveShift AS MV With(NOLOCK)  ON A.FNHSysEmpID = MV.FNHSysEmpID"
        _Qry &= vbCrLf & " INNER JOIN   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMTimeShift AS S With(NOLOCK)  ON MV.FNHSysShiftID = S.FNHSysShiftID"
        _Qry &= vbCrLf & " Where (A.FNHSysUnitSectId = " & _UnitSectID & ") "
        _Qry &= vbCrLf & "  And (A.FNEmpStatus = 0) "
        _Qry &= vbCrLf & "  AND (MV.FDShiftDate ='" & HI.UL.ULDate.ConvertEnDB(_CalDate) & "') "
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


        '_Qry = "EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.SP_GET_EMPTIME_CAL_INCENTIVE " & _UnitSectID & ",'" & HI.UL.ULDate.ConvertEnDB(_CalDate) & "'," & _TFNHSysEmpID & ""
        '_dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)


        _Qry = "EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.SP_GET_EMPTIME_CAL_INCENTIVE_PROD_EFF " & _UnitSectID & ",'" & HI.UL.ULDate.ConvertEnDB(_CalDate) & "'," & _TFNHSysEmpID & ""
        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)




        Dim _FNHSysEmpID_duplicate_flag As String = ""

        For Each R As DataRow In _dt.Rows

            If _FNHSysEmpID = Integer.Parse(Val(R!FNHSysEmpID.ToString)) Then
                ''ย้ายมาไลน์เดียวกัน สองรอบ
                _FNHSysEmpID_duplicate_flag = "y"
            Else

                _FNHSysEmpID_duplicate_flag = ""
            End If

            _FNHSysEmpID = Integer.Parse(Val(R!FNHSysEmpID.ToString))

            If _FNHSysEmpID = 1772510017 Then
                Beep()
            End If

            _Qry = " Select  TOP 1 FTTimeOut  "
            _Qry &= vbCrLf & " From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTDailySpecial  AS X WITH(NOLOCK)"
            _Qry &= vbCrLf & " WHERE FNHSysEmpID=" & _FNHSysEmpID & ""
            _Qry &= vbCrLf & " AND FTDate='" & HI.UL.ULDate.ConvertEnDB(_CalDate) & "'"
            _LeaveHomeSpecial = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "")


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

            _Qry = " Select FNHSysEmpID, FTDateTrans, FTLeaveType, FTLeaveStartTime, FTLeaveEndTime"
            _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTransLeave As X With(NOLOCK)"
            _Qry &= vbCrLf & " WHERE FNHSysEmpID= " & _FNHSysEmpID & " "
            _Qry &= vbCrLf & " And FTDateTrans='" & HI.UL.ULDate.ConvertEnDB(_CalDate) & "'"

            _dttimeLeave = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

            For Each Rxt As DataRow In _dttime.Rows

                _StartTime = Rxt!FTStartTime.ToString
                _EndTime = Rxt!FTEndTime.ToString

                _FNHour = Integer.Parse(Val(Rxt!FNHour.ToString))
                _FNWorkMinute = 0
                _FNWorkMinuteHR = 0
                ' And (_FTEndTime >= _EndTime) 

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


                If _dtmove.Select("FNHSysEmpID=" & _FNHSysEmpID & " And FTStartTime<='" & _StartTime & "' AND FTEndTime>='" & _EndTime & "'").Length <= 0 Then

                    Select Case _FNHour
                        Case 1, 2, 3, 4

                            If _FTState = "1" Then

                                Select Case True
                                    Case (_FTIn1 <= _StartTime And _FTOut1 >= _EndTime) And (_FTStartTime <= _StartTime And _FTEndTime >= _EndTime)

                                        '_FNWorkMinute = 60
                                        If _dttimeLeave.Select("FTLeaveStartTime<='" & _StartTime & "' AND FTLeaveEndTime>='" & _EndTime & "' ").Length <= 0 Then
                                            '_FNWorkMinute = 60
                                            '_FNWorkMinuteHR = 60


                                            _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _EndTime))
                                            _FNWorkMinuteHR = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _EndTime))

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
                                                    ' _FNWorkMinuteHR = 60

                                                    _FNWorkMinuteHR = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _EndTime))
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
                                                    ' _FNWorkMinuteHR = 60


                                                    _FNWorkMinuteHR = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _EndTime))

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
                                            '_FNWorkMinute = 60
                                            '_FNWorkMinuteHR = 60

                                            _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _EndTime))
                                            _FNWorkMinuteHR = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _EndTime))

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
                                                    ' _FNWorkMinuteHR = 60


                                                    _FNWorkMinuteHR = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _EndTime))
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
                                                    ' _FNWorkMinuteHR = 60


                                                    _FNWorkMinuteHR = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _EndTime))
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
                                                    ' _FNWorkMinuteHR = 60


                                                    _FNWorkMinuteHR = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _EndTime))
                                                End If

                                            Else
                                                _FNWorkMinute = 0
                                                _FNWorkMinuteHR = 0
                                            End If

                                        Catch ex As Exception
                                        End Try
                                        'add check ออกงานก่อนเวลา
                                    Case (_StartTime >= _FTIn1 And _FTOut1 < _EndTime And _FTIn1 <> "")
                                        Try
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
                                                    _FNWorkMinuteHR = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _FTOut1))


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
                                            '_FNWorkMinute = 60
                                            '_FNWorkMinuteHR = 60

                                            _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _EndTime))
                                            _FNWorkMinuteHR = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _EndTime))

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
                                                    ' _FNWorkMinuteHR = 60


                                                    _FNWorkMinuteHR = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _EndTime))

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
                                                    ' _FNWorkMinuteHR = 60


                                                    _FNWorkMinuteHR = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _EndTime))

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
                                                    ' _FNWorkMinuteHR = 60


                                                    _FNWorkMinuteHR = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _EndTime))
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
                                            '_FNWorkMinute = 60
                                            '_FNWorkMinuteHR = 60

                                            _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _EndTime))
                                            _FNWorkMinuteHR = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _EndTime))

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
                                                    ' _FNWorkMinuteHR = 60


                                                    _FNWorkMinuteHR = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _EndTime))
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
                                                    ' _FNWorkMinuteHR = 60


                                                    _FNWorkMinuteHR = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _EndTime))
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
                                                    ' _FNWorkMinuteHR = 60


                                                    _FNWorkMinuteHR = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _EndTime))
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
                                                    '_FNWorkMinute = 60
                                                    '_FNWorkMinuteHR = 60

                                                    _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _EndTime))
                                                    _FNWorkMinuteHR = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _EndTime))

                                                Else
                                                    If _dttimeLeave.Select("FTLeaveStartTime<='" & _StartTime & "' AND FTLeaveEndTime>='" & _EndTime & "' ").Length <= 0 And _dtmove.Select("FNHSysEmpID=" & _FNHSysEmpID & " AND FTStartTime<='" & _StartTime & "' AND FTEndTime>='" & _FTOut3 & "'").Length <= 0 Then
                                                        '_FNWorkMinute = 60
                                                        '_FNWorkMinuteHR = 60

                                                        _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _EndTime))
                                                        _FNWorkMinuteHR = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _EndTime))
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
                                                            '  _FNWorkMinuteHR = 60


                                                            _FNWorkMinuteHR = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _EndTime))
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
                                                            ' _FNWorkMinuteHR = 60


                                                            _FNWorkMinuteHR = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _EndTime))
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


                                                            '  _FNWorkMinuteHR = 60

                                                            _FNWorkMinuteHR = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _EndTime))
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
                                                    '_FNWorkMinute = 60
                                                    '_FNWorkMinuteHR = 60

                                                    _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _EndTime))
                                                    _FNWorkMinuteHR = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _EndTime))

                                                Else
                                                    ' _FNWorkMinute = 60
                                                    If _dttimeLeave.Select("FTLeaveStartTime<='" & _StartTime & "' AND FTLeaveEndTime>='" & _EndTime & "' ").Length <= 0 And _dtmove.Select("FNHSysEmpID=" & _FNHSysEmpID & " AND FTStartTime<='" & _StartTime & "' AND FTEndTime>='" & _FTOut3 & "'").Length <= 0 Then
                                                        '_FNWorkMinute = 60
                                                        '_FNWorkMinuteHR = 60

                                                        _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _EndTime))
                                                        _FNWorkMinuteHR = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _EndTime))

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
                                                            ' _FNWorkMinuteHR = 60


                                                            _FNWorkMinuteHR = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _EndTime))

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
                                                            ' _FNWorkMinuteHR = 60


                                                            _FNWorkMinuteHR = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _EndTime))

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
                                                            ' _FNWorkMinuteHR = 60


                                                            _FNWorkMinuteHR = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _EndTime))

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
                    'Select Case _FNHour
                    '    Case 1, 2, 3, 4

                    '        If _FTState = "1" Then

                    '            Select Case True
                    '                Case (_FTIn1 <= _StartTime And _FTOut1 >= _EndTime) And (_FTStartTime <= _StartTime And _FTEndTime >= _EndTime)
                    '                    '_FNWorkMinute = 60
                    '                    If _dttimeLeave.Select("FTLeaveStartTime<='" & _StartTime & "' AND FTLeaveEndTime>='" & _EndTime & "' ").Length <= 0 Then
                    '                        _FNWorkMinute = 60
                    '                    Else
                    '                        _FNWorkMinute = 0
                    '                    End If

                    '                Case (_StartTime < _FTIn1 And _FTOut1 >= _EndTime And _FTIn1 <> "") And (_FTStartTime <= _StartTime And _FTEndTime >= _EndTime)

                    '                    Try
                    '                        ' _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn1), CDate(_CalDate & " " & _EndTime))

                    '                        If _dttimeLeave.Select("FTLeaveStartTime<='" & _StartTime & "' AND FTLeaveEndTime>='" & _EndTime & "' ").Length <= 0 Then
                    '                            _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn1), CDate(_CalDate & " " & _EndTime))
                    '                        Else
                    '                            _FNWorkMinute = 0
                    '                        End If

                    '                    Catch ex As Exception
                    '                    End Try

                    '                Case (_StartTime < _FTIn1 And _FTOut1 < _EndTime And _FTIn1 <> "" And _FTOut1 <> "" And _FTIn1 < _FTOut1) And (_FTStartTime <= _StartTime And _FTEndTime >= _EndTime)

                    '                    Try
                    '                        ' _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn1), CDate(_CalDate & " " & _FTOut1))

                    '                        If _dttimeLeave.Select("FTLeaveStartTime<='" & _StartTime & "' AND FTLeaveEndTime>='" & _EndTime & "' ").Length <= 0 Then
                    '                            _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn1), CDate(_CalDate & " " & _FTOut1))
                    '                        Else
                    '                            _FNWorkMinute = 0
                    '                        End If

                    '                    Catch ex As Exception
                    '                    End Try
                    '                Case (_StartTime >= _FTIn1 And _FTOut1 < _EndTime And _FTIn1 <> "")
                    '                    Try
                    '                        ' _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _FTOut1))

                    '                        If _dttimeLeave.Select("FTLeaveStartTime<='" & _StartTime & "' AND FTLeaveEndTime>='" & _EndTime & "' ").Length <= 0 Then
                    '                            _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _FTOut1))
                    '                        Else
                    '                            _FNWorkMinute = 0
                    '                        End If

                    '                    Catch ex As Exception
                    '                    End Try
                    '            End Select

                    '        Else

                    '            Select Case True
                    '                Case (_FTIn1 <= _StartTime And _FTOut1 >= _EndTime And _FTIn1 <> "")

                    '                    ' _FNWorkMinute = 60
                    '                    If _dttimeLeave.Select("FTLeaveStartTime<='" & _StartTime & "' AND FTLeaveEndTime>='" & _EndTime & "' ").Length <= 0 Then
                    '                        _FNWorkMinute = 60
                    '                    Else
                    '                        _FNWorkMinute = 0
                    '                    End If
                    '                Case (_StartTime < _FTIn1 And _FTOut1 >= _EndTime And _FTIn1 <> "")

                    '                    Try
                    '                        '_FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn1), CDate(_CalDate & " " & _EndTime))

                    '                        If _dttimeLeave.Select("FTLeaveStartTime<='" & _StartTime & "' AND FTLeaveEndTime>='" & _EndTime & "' ").Length <= 0 Then
                    '                            _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn1), CDate(_CalDate & " " & _EndTime))
                    '                        Else
                    '                            _FNWorkMinute = 0
                    '                        End If

                    '                    Catch ex As Exception
                    '                    End Try

                    '                Case (_StartTime < _FTIn1 And _FTOut1 < _EndTime And _FTIn1 <> "" And _FTOut1 <> "" And _FTIn1 < _FTOut1)

                    '                    Try
                    '                        ' _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn1), CDate(_CalDate & " " & _FTOut1))

                    '                        If _dttimeLeave.Select("FTLeaveStartTime<='" & _StartTime & "' AND FTLeaveEndTime>='" & _EndTime & "' ").Length <= 0 Then
                    '                            _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn1), CDate(_CalDate & " " & _FTOut1))
                    '                        Else
                    '                            _FNWorkMinute = 0
                    '                        End If

                    '                    Catch ex As Exception
                    '                    End Try
                    '                Case (_StartTime >= _FTIn1 And _FTOut1 < _EndTime And _FTIn1 <> "")
                    '                    Try
                    '                        ' _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _FTOut1))

                    '                        If _dttimeLeave.Select("FTLeaveStartTime<='" & _StartTime & "' AND FTLeaveEndTime>='" & _EndTime & "' ").Length <= 0 Then
                    '                            _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _FTOut1))
                    '                        Else
                    '                            _FNWorkMinute = 0
                    '                        End If

                    '                    Catch ex As Exception
                    '                    End Try
                    '            End Select

                    '        End If

                    '    Case 5, 6, 7, 8

                    '        If _FTState = "1" Then

                    '            Select Case True

                    '                Case (_FTIn2 <= _StartTime And _FTOut2 >= _EndTime) And (_FTStartTime <= _StartTime And _FTEndTime >= _EndTime) And (_FTIn2 <> "" And _FTOut2 <> "")

                    '                    ' _FNWorkMinute = 60

                    '                    If _dttimeLeave.Select("FTLeaveStartTime<='" & _StartTime & "' AND FTLeaveEndTime>='" & _EndTime & "' ").Length <= 0 Then
                    '                        _FNWorkMinute = 60
                    '                    Else
                    '                        _FNWorkMinute = 0
                    '                    End If

                    '                Case (_StartTime < _FTIn2 And _FTOut2 >= _EndTime And _FTIn2 <> "") And (_FTStartTime <= _StartTime And _FTEndTime >= _EndTime) And (_FTIn2 <> "" And _FTOut2 <> "")

                    '                    Try
                    '                        ' _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn2), CDate(_CalDate & " " & _EndTime))

                    '                        If _dttimeLeave.Select("FTLeaveStartTime<='" & _StartTime & "' AND FTLeaveEndTime>='" & _EndTime & "' ").Length <= 0 Then
                    '                            _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn2), CDate(_CalDate & " " & _EndTime))
                    '                        Else
                    '                            _FNWorkMinute = 0
                    '                        End If

                    '                    Catch ex As Exception
                    '                    End Try

                    '                Case (_StartTime < _FTIn2 And _FTOut2 < _EndTime And _FTIn2 <> "" And _FTOut2 <> "" And _FTIn2 < _FTOut2) And (_FTStartTime <= _StartTime And _FTEndTime >= _EndTime) And (_FTIn2 <> "" And _FTOut2 <> "")

                    '                    Try
                    '                        '_FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn2), CDate(_CalDate & " " & _FTOut2))
                    '                        If _dttimeLeave.Select("FTLeaveStartTime<='" & _StartTime & "' AND FTLeaveEndTime>='" & _EndTime & "' ").Length <= 0 Then
                    '                            _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn2), CDate(_CalDate & " " & _FTOut2))
                    '                        Else
                    '                            _FNWorkMinute = 0
                    '                        End If
                    '                    Catch ex As Exception
                    '                    End Try
                    '                Case (_StartTime >= _FTIn2 And _FTOut2 < _EndTime And _FTIn2 <> "") And (_FTIn2 <> "" And _FTOut2 <> "")
                    '                    Try
                    '                        ' _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _FTOut2))

                    '                        If _dttimeLeave.Select("FTLeaveStartTime<='" & _StartTime & "' AND FTLeaveEndTime>='" & _EndTime & "' ").Length <= 0 Then
                    '                            _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _FTOut2))
                    '                        Else
                    '                            _FNWorkMinute = 0
                    '                        End If

                    '                    Catch ex As Exception
                    '                    End Try
                    '            End Select

                    '        Else

                    '            Select Case True

                    '                Case (_FTIn2 <= _StartTime And _FTOut2 >= _EndTime) And (_FTIn2 <> "" And _FTOut2 <> "")

                    '                    '_FNWorkMinute = 60
                    '                    If _dttimeLeave.Select("FTLeaveStartTime<='" & _StartTime & "' AND FTLeaveEndTime>='" & _EndTime & "' ").Length <= 0 Then
                    '                        _FNWorkMinute = 60
                    '                    Else
                    '                        _FNWorkMinute = 0
                    '                    End If

                    '                Case (_StartTime < _FTIn2 And _FTOut2 >= _EndTime And _FTIn2 <> "") And (_FTIn2 <> "" And _FTOut2 <> "")

                    '                    Try
                    '                        ' _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn2), CDate(_CalDate & " " & _EndTime))

                    '                        If _dttimeLeave.Select("FTLeaveStartTime<='" & _StartTime & "' AND FTLeaveEndTime>='" & _EndTime & "' ").Length <= 0 Then
                    '                            _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn2), CDate(_CalDate & " " & _EndTime))
                    '                        Else
                    '                            _FNWorkMinute = 0
                    '                        End If

                    '                    Catch ex As Exception
                    '                    End Try

                    '                Case (_StartTime < _FTIn2 And _FTOut2 < _EndTime And _FTIn2 <> "" And _FTOut2 <> "" And _FTIn2 < _FTOut2) And (_FTIn2 <> "" And _FTOut2 <> "")

                    '                    Try
                    '                        ' _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn2), CDate(_CalDate & " " & _FTOut2))

                    '                        If _dttimeLeave.Select("FTLeaveStartTime<='" & _StartTime & "' AND FTLeaveEndTime>='" & _EndTime & "' ").Length <= 0 Then
                    '                            _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn2), CDate(_CalDate & " " & _FTOut2))
                    '                        Else
                    '                            _FNWorkMinute = 0
                    '                        End If

                    '                    Catch ex As Exception
                    '                    End Try

                    '                Case (_StartTime >= _FTIn2 And _FTOut2 < _EndTime And _FTIn2 <> "") And (_FTIn2 <> "" And _FTOut2 <> "")

                    '                    Try
                    '                        '_FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _FTOut2))
                    '                        If _dttimeLeave.Select("FTLeaveStartTime<='" & _StartTime & "' AND FTLeaveEndTime>='" & _EndTime & "' ").Length <= 0 Then
                    '                            _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _FTOut2))
                    '                        Else
                    '                            _FNWorkMinute = 0
                    '                        End If
                    '                    Catch ex As Exception
                    '                    End Try

                    '            End Select

                    '        End If

                    '    Case 9, 10, 11, 12, 13

                    '        If _FNHSysEmpID = 1099001858 Then
                    '            Dim X As String
                    '            X = "99999"
                    '        End If

                    '        If _FNOTRequestMin > 0 Then

                    '            If _dtmove.Select("FNHSysEmpID=" & _FNHSysEmpID & " AND FTStartTime<='" & _FTIn3 & "' AND FTEndTime>='" & _FTOut3 & "'").Length > 0 Then
                    '                _FNWorkMinute = 0
                    '            Else
                    '                If _FTState = "1" Then

                    '                    Select Case True

                    '                        Case (((_FTIn3 <= _StartTime And _FTOut3 >= _EndTime) And (_FTStartTime <= _StartTime And _FTEndTime >= _EndTime)) Or ((_FTIn3 = TimeCheckOut2 And _FNHour = 9)))

                    '                            ' _FNWorkMinute = 60
                    '                            If _FTIn3 = TimeCheckOut2 Then
                    '                                _FNWorkMinute = 60
                    '                            Else
                    '                                If _dttimeLeave.Select("FTLeaveStartTime<='" & _StartTime & "' AND FTLeaveEndTime>='" & _EndTime & "' ").Length <= 0 And _dtmove.Select("FNHSysEmpID=" & _FNHSysEmpID & " AND FTStartTime<='" & _StartTime & "' AND FTEndTime>='" & _FTOut3 & "'").Length <= 0 Then
                    '                                    _FNWorkMinute = 60
                    '                                Else
                    '                                    _FNWorkMinute = 0
                    '                                End If
                    '                            End If


                    '                        Case (_StartTime < _FTIn3 And _FTOut3 >= _EndTime And _FTIn3 <> "") And (_FTStartTime <= _StartTime And _FTEndTime >= _EndTime)

                    '                            Try
                    '                                ' _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn3), CDate(_CalDate & " " & _EndTime))
                    '                                If _dttimeLeave.Select("FTLeaveStartTime<='" & _StartTime & "' AND FTLeaveEndTime>='" & _EndTime & "' ").Length <= 0 And _dtmove.Select("FNHSysEmpID=" & _FNHSysEmpID & " AND FTStartTime<='" & _StartTime & "' AND FTEndTime>='" & _FTOut3 & "'").Length <= 0 Then
                    '                                    _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn3), CDate(_CalDate & " " & _EndTime))
                    '                                Else
                    '                                    _FNWorkMinute = 0
                    '                                End If
                    '                            Catch ex As Exception
                    '                            End Try

                    '                        Case (_StartTime < _FTIn3 And _FTOut3 < _EndTime And _FTIn3 <> "" And _FTOut3 <> "" And _FTIn3 < _FTOut2) And (_FTStartTime <= _StartTime And _FTEndTime >= _EndTime)

                    '                            Try
                    '                                '_FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn3), CDate(_CalDate & " " & _FTOut3))

                    '                                If _dttimeLeave.Select("FTLeaveStartTime<='" & _StartTime & "' AND FTLeaveEndTime>='" & _EndTime & "' ").Length <= 0 And _dtmove.Select("FNHSysEmpID=" & _FNHSysEmpID & " AND FTStartTime<='" & _StartTime & "' AND FTEndTime>='" & _FTOut3 & "'").Length <= 0 Then
                    '                                    _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn3), CDate(_CalDate & " " & _FTOut3))
                    '                                Else
                    '                                    _FNWorkMinute = 0
                    '                                End If

                    '                            Catch ex As Exception
                    '                            End Try

                    '                        Case (_StartTime >= _FTIn3 And _FTOut3 < _EndTime And _FTIn3 <> "" And _FTOut3 <> "" And _FTIn3 > _FTOut2 And _StartTime <= _FTOut3) And (_FTStartTime <= _StartTime And _FTEndTime >= _EndTime)

                    '                            Try
                    '                                '_FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _FTOut3))
                    '                                If _dttimeLeave.Select("FTLeaveStartTime<='" & _StartTime & "' AND FTLeaveEndTime>='" & _EndTime & "' ").Length <= 0 And _dtmove.Select("FNHSysEmpID=" & _FNHSysEmpID & " AND FTStartTime<='" & _StartTime & "' AND FTEndTime>='" & _FTOut3 & "'").Length <= 0 Then
                    '                                    _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _FTOut3))
                    '                                Else
                    '                                    _FNWorkMinute = 0
                    '                                End If
                    '                            Catch ex As Exception
                    '                            End Try

                    '                    End Select

                    '                Else

                    '                    Select Case True

                    '                        Case ((_FTIn3 <= _StartTime And _FTOut3 >= _EndTime) Or (_FTIn3 = TimeCheckOut2 And _FNHour = 9))


                    '                            If _FTIn3 = TimeCheckOut2 Then
                    '                                _FNWorkMinute = 60
                    '                            Else
                    '                                ' _FNWorkMinute = 60
                    '                                If _dttimeLeave.Select("FTLeaveStartTime<='" & _StartTime & "' AND FTLeaveEndTime>='" & _EndTime & "' ").Length <= 0 And _dtmove.Select("FNHSysEmpID=" & _FNHSysEmpID & " AND FTStartTime<='" & _StartTime & "' AND FTEndTime>='" & _FTOut3 & "'").Length <= 0 Then
                    '                                    _FNWorkMinute = 60
                    '                                Else
                    '                                    _FNWorkMinute = 0
                    '                                End If
                    '                            End If


                    '                        Case (_StartTime < _FTIn3 And _FTOut3 >= _EndTime And _FTIn3 <> "")

                    '                            Try
                    '                                '_FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn3), CDate(_CalDate & " " & _EndTime))
                    '                                If _dttimeLeave.Select("FTLeaveStartTime<='" & _StartTime & "' AND FTLeaveEndTime>='" & _EndTime & "' ").Length <= 0 And _dtmove.Select("FNHSysEmpID=" & _FNHSysEmpID & " AND FTStartTime<='" & _StartTime & "' AND FTEndTime>='" & _FTOut3 & "'").Length <= 0 Then
                    '                                    _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn3), CDate(_CalDate & " " & _EndTime))
                    '                                Else
                    '                                    _FNWorkMinute = 0
                    '                                End If
                    '                            Catch ex As Exception
                    '                            End Try

                    '                        Case (_StartTime < _FTIn3 And _FTOut3 < _EndTime And _FTIn3 <> "" And _FTOut3 <> "" And _FTIn3 < _FTOut2)

                    '                            Try
                    '                                '_FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn3), CDate(_CalDate & " " & _FTOut3))
                    '                                If _dttimeLeave.Select("FTLeaveStartTime<='" & _StartTime & "' AND FTLeaveEndTime>='" & _EndTime & "' ").Length <= 0 And _dtmove.Select("FNHSysEmpID=" & _FNHSysEmpID & " AND FTStartTime<='" & _StartTime & "' AND FTEndTime>='" & _FTOut3 & "'").Length <= 0 Then

                    '                                    _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _FTIn3), CDate(_CalDate & " " & _FTOut3))

                    '                                Else

                    '                                    _FNWorkMinute = 0

                    '                                End If

                    '                            Catch ex As Exception
                    '                            End Try

                    '                        Case (_StartTime >= _FTIn3 And _FTOut3 < _EndTime And _FTIn3 <> "" And _FTOut3 <> "" And _FTIn3 > _FTOut2 And _StartTime <= _FTOut3)

                    '                            Try
                    '                                '_FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _FTOut3))

                    '                                If _dttimeLeave.Select("FTLeaveStartTime<='" & _StartTime & "' AND FTLeaveEndTime>='" & _EndTime & "' ").Length <= 0 And _dtmove.Select("FNHSysEmpID=" & _FNHSysEmpID & " AND FTStartTime<='" & _StartTime & "' AND FTEndTime>='" & _FTOut3 & "'").Length <= 0 Then

                    '                                    _FNWorkMinute = DateDiff(DateInterval.Minute, CDate(_CalDate & " " & _StartTime), CDate(_CalDate & " " & _FTOut3))

                    '                                Else

                    '                                    _FNWorkMinute = 0

                    '                                End If

                    '                            Catch ex As Exception
                    '                            End Try

                    '                    End Select

                    '                End If
                    '            End If

                    '        End If

                    'End Select

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
                If _FNHSysEmpID_duplicate_flag = "" Then


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
                Else
                    ''ย้ายเข้าไลน์อีกรอบ

                    For Each Rxm As DataRow In _dtemptime.Select("FNHSysEmpID=" & _FNHSysEmpID & "")

                        Select Case _FNHour
                            Case 0
                                If _FNWorkMinute > 0 Then
                                    Rxm!FNH01 = _FNWorkMinute
                                    Rxm!FNHRH01 = _FNWorkMinuteHR
                                End If
                            Case 1
                                If _FNWorkMinute > 0 Then
                                    Rxm!FNH01 = _FNWorkMinute
                                    Rxm!FNHRH01 = _FNWorkMinuteHR
                                End If
                            Case 2
                                If _FNWorkMinute > 0 Then
                                    Rxm!FNH02 = _FNWorkMinute
                                    Rxm!FNHRH02 = _FNWorkMinuteHR
                                End If
                            Case 3
                                If _FNWorkMinute > 0 Then
                                    Rxm!FNH03 = _FNWorkMinute
                                    Rxm!FNHRH03 = _FNWorkMinuteHR
                                End If
                            Case 4
                                If _FNWorkMinute > 0 Then
                                    Rxm!FNH04 = _FNWorkMinute
                                    Rxm!FNHRH04 = _FNWorkMinuteHR
                                End If
                            Case 5
                                If _FNWorkMinute > 0 Then
                                    Rxm!FNH05 = _FNWorkMinute
                                    Rxm!FNHRH05 = _FNWorkMinuteHR
                                End If
                            Case 6
                                If _FNWorkMinute > 0 Then
                                    Rxm!FNH06 = _FNWorkMinute
                                    Rxm!FNHRH06 = _FNWorkMinuteHR
                                End If
                            Case 7
                                If _FNWorkMinute > 0 Then
                                    Rxm!FNH07 = _FNWorkMinute
                                    Rxm!FNHRH07 = _FNWorkMinuteHR
                                End If
                            Case 8
                                If _FNWorkMinute > 0 Then
                                    Rxm!FNH08 = _FNWorkMinute
                                    Rxm!FNHRH08 = _FNWorkMinuteHR
                                End If
                            Case 9
                                If _FNWorkMinute > 0 Then
                                    Rxm!FNH09 = _FNWorkMinute
                                    Rxm!FNHRH09 = _FNWorkMinuteHR
                                End If
                            Case 10
                                If _FNWorkMinute > 0 Then
                                    Rxm!FNH10 = _FNWorkMinute
                                    Rxm!FNHRH10 = _FNWorkMinuteHR
                                End If
                            Case 11
                                If _FNWorkMinute > 0 Then
                                    Rxm!FNH11 = _FNWorkMinute
                                    Rxm!FNHRH11 = _FNWorkMinuteHR
                                End If
                            Case 12
                                If _FNWorkMinute > 0 Then
                                    Rxm!FNH12 = _FNWorkMinute
                                    Rxm!FNHRH12 = _FNWorkMinuteHR
                                End If
                            Case 13
                                If _FNWorkMinute > 0 Then
                                    Rxm!FNH13 = _FNWorkMinute
                                    Rxm!FNHRH13 = _FNWorkMinuteHR
                                End If
                        End Select

                    Next


                End If

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

    Private Sub CalculateEmpWring(dt As DataTable)

        Dim _Qry As String = ""
        Dim _CalDate As String = ""
        Dim _dtprice As DataTable
        Dim _dtemptime As DataTable
        Dim _TotalTime As Integer = 0
        Dim _TotalTimeOT As Integer = 0
        Dim _TotalTimeHR As Integer = 0
        Dim _TotalTimeOTHR As Integer = 0
        Dim _Salary As Double = 0
        Dim _SalaryPerH As Double = 0
        Dim _SalaryPerOT As Double = 0
        Dim _CountEmp As Integer = 0
        Dim _FNAmtNormal As Double = 0
        Dim _FNAmtOT As Double = 0
        Dim _FNNetAmt As Double = 0
        Dim _FixHour As Integer = 0
        Dim _FNAmtOldIncentive As Double = 0

        For Each R As DataRow In dt.Select("FTSelect='1' AND FNHSysEmpID>0")

            _CalDate = HI.UL.ULDate.ConvertEnDB(R!FDScanDateOrg.ToString)

            _Qry = " SELECT M1.FNHSysStyleId,M1.FTOrderNo,M1.FTSubOrderNo,ISNULL(XXA.FNTotalQty,M1.FNScanQuantity) As FNScanQuantity,ISNULL(P.FNSam,0) AS FNSam"
            _Qry &= vbCrLf & ",Convert(numeric(18,4),ISNULL(P.FNWringPrice,0) ) AS FNNetPrice	"
            _Qry &= vbCrLf & ",Convert(numeric(18,2),Convert(numeric(18,4),ISNULL(P.FNWringPrice,0) ) * ISNULL(XXA.FNTotalQty,M1.FNScanQuantity))  AS FNNetAmt	"
            _Qry &= vbCrLf & " FROM ( "

            _Qry &= vbCrLf & " SELECT  P.FTOrderNo ,P.FTSubOrderNo, A.FNHSysStyleId,Sum(FNScanQuantity) AS FNScanQuantity"
            _Qry &= vbCrLf & "   FROM"
            _Qry &= vbCrLf & "	("
            _Qry &= vbCrLf & " SELECT  MM.FTOrderNo ,MM.FTSubOrderNo, MM.FNHSysStyleId,Sum(MM.FNScanQuantity) AS FNScanQuantity"
            _Qry &= vbCrLf & " FROM (SELECT  P.FTOrderNo ,P.FTSubOrderNo, A.FNHSysStyleId,Sum(FNScanQuantity) AS FNScanQuantity"
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
            _Qry &= vbCrLf & "SELECT     O.FTInsUser, O.FDInsDate, O.FTInsTime, O.FTUpdUser, O.FDUpdDate, O.FTUpdTime , '' AS FTPackNo,0 , O.FTOrderNo ,O.FTSubOrderNo, B.FTColorway, B.FTSizeBreakDown"
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
            _Qry &= vbCrLf & " GROUP BY  P.FTOrderNo ,P.FTSubOrderNo, A.FNHSysStyleId "
            _Qry &= vbCrLf & " UNION ALL "
            _Qry &= vbCrLf & " SELECT  P.FTOrderNo ,P.FTSubOrderNo, A.FNHSysStyleId,Sum(0) AS FNScanQuantity"
            _Qry &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTEditScanQuantity AS P WITH(NOLOCK)"
            _Qry &= vbCrLf & "	INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS A  WITH(NOLOCK) ON P.FTOrderNo = A.FTOrderNo"
            _Qry &= vbCrLf & " WHERE P.FNHSysUnitSectId  =" & Integer.Parse(Val(R!FNHSysUnitSectId.ToString)) & " "
            _Qry &= vbCrLf & " AND P.FTDate='" & _CalDate & "'  "
            _Qry &= vbCrLf & " GROUP BY  P.FTOrderNo, P.FTSubOrderNo, A.FNHSysStyleId ) AS MM GROUP BY MM.FTOrderNo ,MM.FTSubOrderNo, MM.FNHSysStyleId "
            _Qry &= vbCrLf & " ) AS M1 "
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

            _dtprice = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

            _dtemptime = GetEmpTime(Integer.Parse(Val(R!FNHSysUnitSectId.ToString)), R!FDScanDateOrg.ToString, Integer.Parse(Val(R!FNHSysEmpID.ToString))).Copy()

            _TotalTime = 0
            _TotalTimeOT = 0
            _TotalTimeHR = 0
            _TotalTimeOTHR = 0
            _Salary = 0
            _SalaryPerH = 0
            _SalaryPerOT = 0
            _CountEmp = 0
            _FNAmtNormal = 0
            _FNAmtOT = 0
            _FNNetAmt = 0
            _FixHour = 0

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


                _FNAmtOldIncentive = 0
                For Each Rxt As DataRow In _dtprice.Rows

                    _Qry = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTIncentive_Emp_Wring_Detail "
                    _Qry &= vbCrLf & " ("
                    _Qry &= vbCrLf & " FTInsUser, FDInsDate, FTInsTime, FTCalDate, FNHSysUnitSectId, FNHSysEmpID, FTOrderNo, FTSubOrderNo, FNQuantity, FNPrice, FNAmt"
                    _Qry &= vbCrLf & " )"
                    _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & ""
                    _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & ""
                    _Qry &= vbCrLf & " ,'" & _CalDate & "'"
                    _Qry &= vbCrLf & " ," & Integer.Parse(Val(R!FNHSysUnitSectId.ToString)) & " "
                    _Qry &= vbCrLf & " ," & Integer.Parse(Val(R!FNHSysEmpID.ToString)) & " "
                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Rxt!FTOrderNo.ToString) & "'"
                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Rxt!FTSubOrderNo.ToString) & "'"
                    _Qry &= vbCrLf & " ," & Integer.Parse(Val(Rxt!FNScanQuantity.ToString)) & " "
                    _Qry &= vbCrLf & " ," & Double.Parse(Val(Rxt!FNNetPrice.ToString)) & " "
                    _Qry &= vbCrLf & " ," & Double.Parse(Val(Rxt!FNNetAmt.ToString)) & " "

                    HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

                    _FNAmtOldIncentive = _FNAmtOldIncentive + Double.Parse(Val(Rxt!FNNetAmt.ToString))

                Next

                _Qry = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTIncentive_Emp ("
                _Qry &= vbCrLf & "  FTInsUser, FDInsDate, FTInsTime, FTCalDate, FNHSysUnitSectId, FNHSysEmpID"
                _Qry &= vbCrLf & ", FNTimeHour01, FNTimeHour02, FNTimeHour03, FNTimeHour04, FNTimeHour05, FNTimeHour06, FNTimeHour07, FNTimeHour08, "
                _Qry &= vbCrLf & "   FNTimeHour09, FNTimeHour10, FNTimeHour11, FNTimeHour12, FNTimeHour13, FNTotalTime, FNTotalTimeOT,FNTotalTimHR,FNTotalTimeOTHR"
                _Qry &= vbCrLf & ", FNSalary, FNAmtNormal, FNAmtOT, FNNetAmt, FNAmtOldIncentive, FNAmtOTOldIncentive,"
                _Qry &= vbCrLf & "  FNNetAmtOldIncentive, FNAmtNewIncentive, FNAmtOTNewIncentive, FNNetAmtNewIncentive,FNQAValue,FTInsuranceHour,FTInsuranceAmt"
                _Qry &= vbCrLf & ")"
                _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & ""
                _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & ""
                _Qry &= vbCrLf & " ,'" & _CalDate & "'   "
                _Qry &= vbCrLf & " ," & Integer.Parse(Val(R!FNHSysUnitSectId.ToString)) & ""
                _Qry &= vbCrLf & " ," & Integer.Parse(Val(R!FNHSysEmpID.ToString)) & ""
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
                _Qry &= vbCrLf & " ," & _FNAmtOldIncentive & ",0," & _FNAmtOldIncentive & ",0,0,0,0"
                _Qry &= vbCrLf & " ," & _FixHour & ""
                _Qry &= vbCrLf & " ,0"

                HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

                _Qry = " DELETE FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTWageDaily "
                _Qry &= vbCrLf & "  WHERE  FNHSysEmpID=" & Integer.Parse(Val(Rt!FNHSysEmpID.ToString)) & ""
                _Qry &= vbCrLf & "    AND  FTDateTrans='" & _CalDate & "'"
                HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

                _Qry = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTWageDaily ( "
                _Qry &= vbCrLf & "   FTInsUser, FTInsDate, FTInsTime, FNHSysEmpID, FTDateTrans"
                _Qry &= vbCrLf & " , FNAmtNormal, FNAmtOT, FNNetAmt, FNProNormal, FNProOT"
                _Qry &= vbCrLf & " , FNProOther, FNNetProAmt,FNQAAmt"
                _Qry &= vbCrLf & " )"

                _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & ""
                _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & ""

                _Qry &= vbCrLf & " ," & Integer.Parse(Val(Rt!FNHSysEmpID.ToString)) & ""
                _Qry &= vbCrLf & " ,'" & _CalDate & "'   "
                _Qry &= vbCrLf & "  ,MAX(FNAmtNormal) AS FNAmtNormal"
                _Qry &= vbCrLf & " , MAX(FNAmtOT) AS FNAmtOT"
                _Qry &= vbCrLf & " , MAX(FNNetAmt) AS FNNetAmt"

                _Qry &= vbCrLf & ", Sum(FNAmtOldIncentive) AS FNAmtOldIncentive"
                _Qry &= vbCrLf & ", Sum(FNAmtOTOldIncentive) AS FNAmtOTOldIncentive,0 AS FNProOther"
                _Qry &= vbCrLf & ", Sum(FNNetAmtOldIncentive) AS FNNetAmtOldIncentive"

                _Qry &= vbCrLf & ", Sum(FNQAValue) AS FNQAValue"
                _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTIncentive_Emp  AS X WITH(NOLOCK) "
                _Qry &= vbCrLf & "  WHERE  FNHSysEmpID=" & Integer.Parse(Val(Rt!FNHSysEmpID.ToString)) & ""
                _Qry &= vbCrLf & "       AND FTCalDate='" & _CalDate & "'"

                HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

                Exit For

            Next

        Next

    End Sub

#Region "Process Calculate"

    Private Function CalculateSewing(Spls As HI.TL.SplashScreen) As Boolean

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
        Dim _FNHSysIncenFormulaId As Integer = 0
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

                _Qry = "SELECT TOP 1 FNHSysIncenFormulaId FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS X WITH(NOLOCK) WHERE FNHSysUnitSectId=" & Val(R!FNHSysUnitSectId.ToString) & ""
                _FNHSysIncenFormulaId = Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MASTER, "0"))

                '_Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTIncentive "
                '_Qry &= vbCrLf & " WHERE  (FTCalDate = '" & HI.UL.ULDate.ConvertEnDB(R!FDScanDateOrg.ToString) & "') "
                '_Qry &= vbCrLf & "   AND (FNHSysUnitSectId = " & Integer.Parse(Val(R!FNHSysUnitSectId.ToString)) & ")"
                'HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

                '_Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTIncentive_Emp "
                '_Qry &= vbCrLf & " WHERE  (FTCalDate = '" & HI.UL.ULDate.ConvertEnDB(R!FDScanDateOrg.ToString) & "') "
                '_Qry &= vbCrLf & "   AND (FNHSysUnitSectId = " & Integer.Parse(Val(R!FNHSysUnitSectId.ToString)) & ")"
                'HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

                '_Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTIncentive_Level "
                '_Qry &= vbCrLf & " WHERE  (FTCalDate = '" & HI.UL.ULDate.ConvertEnDB(R!FDScanDateOrg.ToString) & "') "
                '_Qry &= vbCrLf & "   AND (FNHSysUnitSectId = " & Integer.Parse(Val(R!FNHSysUnitSectId.ToString)) & ")"
                'HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

                '_Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTIncentive_Style "
                '_Qry &= vbCrLf & " WHERE  (FTCalDate = '" & HI.UL.ULDate.ConvertEnDB(R!FDScanDateOrg.ToString) & "') "
                '_Qry &= vbCrLf & "   AND (FNHSysUnitSectId = " & Integer.Parse(Val(R!FNHSysUnitSectId.ToString)) & ")"
                'HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

                '_Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTIncentive_Emp_Deduct "
                '_Qry &= vbCrLf & " WHERE  (FTCalDate = '" & HI.UL.ULDate.ConvertEnDB(R!FDScanDateOrg.ToString) & "') "
                '_Qry &= vbCrLf & "   AND (FNHSysUnitSectId = " & Integer.Parse(Val(R!FNHSysUnitSectId.ToString)) & ")"
                'HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

                '_Qry = "DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTIncentive_Emp_Wring_Detail "
                '_Qry &= vbCrLf & " WHERE FNHSysUnitSectId  =" & Integer.Parse(Val(R!FNHSysUnitSectId.ToString)) & " "
                '_Qry &= vbCrLf & " AND FTCalDate='" & _CalDate & "'  "
                'HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

                _dtemptime = GetEmpTime(Integer.Parse(Val(R!FNHSysUnitSectId.ToString)), R!FDScanDateOrg.ToString).Copy()


                _Qry = "SELECT FNHSysStyleId, FTOrderNo, FTSubOrderNo, SUM(FNScanQuantity) AS FNScanQuantity, MAX(FNSam) AS FNSam, MAX(FNPrice) AS FNPrice, MAX(FNMultiple) AS FNMultiple, FNNetPrice, SUM(FNNetAmt) AS FNNetAmt"

                _Qry &= vbCrLf & "FROM ( SELECT M1.FNHSysStyleId,M1.FTOrderNo,M1.FTSubOrderNo,ISNULL(XXA.FNTotalQty,M1.FNScanQuantity) As FNScanQuantity,ISNULL(P.FNSam,0) AS FNSam,ISNULL(P.FNPrice,0) AS FNPrice,ISNULL(P.FNMultiple,0) AS FNMultiple"
                _Qry &= vbCrLf & ",Convert(numeric(18,4),ISNULL(P.FNPrice,0) ) AS FNNetPrice	"
                _Qry &= vbCrLf & ",Convert(numeric(18,2),Convert(numeric(18,4),ISNULL(P.FNPrice,0) ) * ISNULL(XXA.FNTotalQty,M1.FNScanQuantity))  AS FNNetAmt	"
                _Qry &= vbCrLf & " FROM ( "
                _Qry &= vbCrLf & " SELECT  MM.FTOrderNo ,MM.FTSubOrderNo, MM.FNHSysStyleId,Sum(MM.FNScanQuantity) AS FNScanQuantity"
                _Qry &= vbCrLf & " FROM (SELECT  P.FTOrderNo ,P.FTSubOrderNo, A.FNHSysStyleId,Sum(FNScanQuantity) AS FNScanQuantity"
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
                _Qry &= vbCrLf & "SELECT     O.FTInsUser, O.FDInsDate, O.FTInsTime, O.FTUpdUser, O.FDUpdDate, O.FTUpdTime , '' AS FTPackNo,0 , O.FTOrderNo ,O.FTSubOrderNo, B.FTColorway, B.FTSizeBreakDown"
                _Qry &= vbCrLf & ", O.FNHSysUnitSectId, O.FTBarcodeNo, O.FDDate, O.FTTime, O.FNQuantity"
                _Qry &= vbCrLf & "FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScanOutline AS O WITH(NOLOCK)  LEFT OUTER JOIN"
                _Qry &= vbCrLf & "	      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle AS B WITH(NOLOCK)  ON O.FTBarcodeNo = B.FTBarcodeBundleNo "
                _Qry &= vbCrLf & "	      INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd AS PP WITH(NOLOCK) ON B.FTOrderProdNo = PP.FTOrderProdNo "
                _Qry &= vbCrLf & " WHERE O.FNHSysUnitSectId  =" & Integer.Parse(Val(R!FNHSysUnitSectId.ToString)) & "  AND ISNULL(O.FNStateSewPack,0) =0 "
                _Qry &= vbCrLf & " AND O.FDDate='" & _CalDate & "'   "
                _Qry &= vbCrLf & "	) AS P  RIGHT OUTER JOIN"
                _Qry &= vbCrLf & "	[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS A  WITH(NOLOCK) ON P.FTOrderNo = A.FTOrderNo  LEFT OUTER JOIN"
                _Qry &= vbCrLf & "	[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub AS B  WITH(NOLOCK)  ON A.FTOrderNo = B.FTOrderNo  AND P.FTSubOrderNo = B.FTSubOrderNo LEFT OUTER JOIN"
                _Qry &= vbCrLf & "	[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS S  WITH(NOLOCK)   ON A.FNHSysStyleId = S.FNHSysStyleId"
                _Qry &= vbCrLf & "	WHERE (FNHSysUnitSectId = " & Integer.Parse(Val(R!FNHSysUnitSectId.ToString)) & " )"
                _Qry &= vbCrLf & " and P.FDScanDate ='" & _CalDate & "'   "
                _Qry &= vbCrLf & " GROUP BY  P.FTOrderNo ,P.FTSubOrderNo, A.FNHSysStyleId "
                _Qry &= vbCrLf & " UNION ALL "
                _Qry &= vbCrLf & " SELECT  P.FTOrderNo ,P.FTSubOrderNo, A.FNHSysStyleId,Sum(0) AS FNScanQuantity"
                _Qry &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTEditScanQuantity AS P WITH(NOLOCK)"
                _Qry &= vbCrLf & "	INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS A  WITH(NOLOCK) ON P.FTOrderNo = A.FTOrderNo"
                _Qry &= vbCrLf & " WHERE P.FNHSysUnitSectId  =" & Integer.Parse(Val(R!FNHSysUnitSectId.ToString)) & " "
                _Qry &= vbCrLf & " AND P.FTDate='" & _CalDate & "'  "
                _Qry &= vbCrLf & " GROUP BY  P.FTOrderNo, P.FTSubOrderNo, A.FNHSysStyleId ) AS MM GROUP BY MM.FTOrderNo ,MM.FTSubOrderNo, MM.FNHSysStyleId "
                _Qry &= vbCrLf & "   ) AS M1 "
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
                _Qry &= vbCrLf & " UNION ALL "

                _Qry &= vbCrLf & " SELECT M1.FNHSysStyleId,M1.FTOrderNo,M1.FTSubOrderNo,M1.FNScanQuantity As FNScanQuantity,ISNULL(P.FNSam,0) AS FNSam,ISNULL(P.FNPrice,0) AS FNPrice,ISNULL(P.FNMultiple,0) AS FNMultiple"
                _Qry &= vbCrLf & ",Convert(numeric(18,4),ISNULL(P.FNPrice,0)- ISNULL(M1.FNDisPrice,0) )  AS FNNetPrice "
                _Qry &= vbCrLf & ",Convert(numeric(18,2),Convert(numeric(18,4),ISNULL(P.FNPrice,0) - ISNULL(M1.FNDisPrice,0)) *M1.FNScanQuantity)  AS FNNetAmt	"
                _Qry &= vbCrLf & " FROM ( "
                _Qry &= vbCrLf & " SELECT  MM.FTOrderNo ,MM.FTSubOrderNo, MM.FNHSysStyleId,Sum(MM.FNScanQuantity) AS FNScanQuantity,FNDisPrice"
                _Qry &= vbCrLf & " FROM ("
                _Qry &= vbCrLf & " SELECT  P.FTOrderNo ,P.FTSubOrderNo, A.FNHSysStyleId,P.FNScanQuantity AS FNScanQuantity"
                _Qry &= vbCrLf & ",CASE WHEN P.FNDisPrice <0 THEN ISNULL((SELECT TOP 1 AA2.FNPrice"
                _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODMOperationByStyle AS AA2 WITH(NOLOCK) INNER JOIN"
                _Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TPRODMOperation AS B  WITH(NOLOCK) ON AA2.FNHSysOperationId = B.FNHSysOperationId"
                _Qry &= vbCrLf & "  WHERE  (B.FTStatePack = '1') AND (AA2.FNHSysStyleId  = A.FNHSysStyleId)),0) ELSE P.FNDisPrice END AS FNDisPrice"
                _Qry &= vbCrLf & "   FROM"
                _Qry &= vbCrLf & "	( "

                _Qry &= vbCrLf & "SELECT     O.FTInsUser, O.FDInsDate, O.FTInsTime, O.FTUpdUser, O.FDUpdDate, O.FTUpdTime , '' AS FTPackNo,0  AS FNCartonNo"
                _Qry &= vbCrLf & " ,O.FTOrderNo ,O.FTSubOrderNo, B.FTColorway, B.FTSizeBreakDown"
                _Qry &= vbCrLf & " ,O.FNHSysUnitSectId, O.FTBarcodeNo, O.FDDate As FDScanDate, O.FTTime AS FDScanTime, O.FNQuantity AS FNScanQuantity"
                _Qry &= vbCrLf & " ,ISNULL((SELECT TOP 1 A.FNPrice"
                _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOperationByOrderProd AS A  WITH(NOLOCK) INNER JOIN"
                _Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TPRODMOperation AS B  WITH(NOLOCK) ON A.FNHSysOperationId = B.FNHSysOperationId"
                _Qry &= vbCrLf & " WHERE  (B.FTStatePack = '1') AND (A.FTOrderProdNo = PP.FTOrderProdNo)) , -1 ) AS FNDisPrice"
                _Qry &= vbCrLf & "FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScanOutline AS O WITH(NOLOCK)  LEFT OUTER JOIN"
                _Qry &= vbCrLf & "	      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle AS B WITH(NOLOCK)  ON O.FTBarcodeNo = B.FTBarcodeBundleNo "
                _Qry &= vbCrLf & "	      INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd AS PP WITH(NOLOCK) ON B.FTOrderProdNo = PP.FTOrderProdNo "
                _Qry &= vbCrLf & " WHERE O.FNHSysUnitSectId  =" & Integer.Parse(Val(R!FNHSysUnitSectId.ToString)) & "  AND ISNULL(O.FNStateSewPack,0) =1 "
                _Qry &= vbCrLf & " AND O.FDDate='" & _CalDate & "'   "
                _Qry &= vbCrLf & "	) AS P  RIGHT OUTER JOIN"
                _Qry &= vbCrLf & "	[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS A  WITH(NOLOCK) ON P.FTOrderNo = A.FTOrderNo  LEFT OUTER JOIN"
                _Qry &= vbCrLf & "	[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub AS B  WITH(NOLOCK)  ON A.FTOrderNo = B.FTOrderNo  AND P.FTSubOrderNo = B.FTSubOrderNo LEFT OUTER JOIN"
                _Qry &= vbCrLf & "	[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS S  WITH(NOLOCK)   ON A.FNHSysStyleId = S.FNHSysStyleId"
                _Qry &= vbCrLf & "	WHERE (FNHSysUnitSectId = " & Integer.Parse(Val(R!FNHSysUnitSectId.ToString)) & " )"
                _Qry &= vbCrLf & " and P.FDScanDate ='" & _CalDate & "'   "
                _Qry &= vbCrLf & " ) AS MM GROUP BY MM.FTOrderNo ,MM.FTSubOrderNo, MM.FNHSysStyleId,FNDisPrice "
                _Qry &= vbCrLf & "   ) AS M1 "
                _Qry &= vbCrLf & " LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TPPCTSamByOrder AS P WITH(NOLOCK) "
                _Qry &= vbCrLf & " ON M1.FNHSysStyleId = P.FNHSysStyleId "
                _Qry &= vbCrLf & " AND M1.FTOrderNo = P.FTOrderNo "
                _Qry &= vbCrLf & " AND M1.FTSubOrderNo = P.FTSubOrderNo "
                _Qry &= vbCrLf & " ) AS X GROUP BY FNHSysStyleId, FTOrderNo, FTSubOrderNo,  FNNetPrice"

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
                    Else
                        _LineIncentiveAmt = _LineAmt
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
                    _Qry &= vbCrLf & ")"
                    _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
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
                _Qry &= vbCrLf & "  , FNHour13QtySystem, FNTotalQtySystem,FNHSysIncenFormulaId"

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
                _Qry &= vbCrLf & " ," & _FNHSysIncenFormulaId & ""

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

                    ''-----------สายเช้ามากกว่า 15 ไม่ได้ Incentive ในชั่วโมงนั้น
                    'For Each Rt As DataRow In _dtemptime.Select("FNLateMMin > 15 AND FTStateCal<>'1' AND FNTotalWorkingMin>0")
                    '    _totalCal = 0

                    '    _Salary = Double.Parse(Val(Rt!FNSalary.ToString))
                    '    _SalaryPerH = Double.Parse(Format(_Salary / 8, "0.00"))
                    '    _SalaryPerOT = Double.Parse(Format(_SalaryPerH * 1.5, "0.00"))

                    '    If _FixHour > 0 Then

                    '    End If

                    '    _FNAmtOT = Double.Parse(Format(((_Salary / 480) * 0.5) * Integer.Parse(Val(Rt!FNWorkingOTMin.ToString)), "0.00"))


                    '    If Val(Rt!FNLateMMin.ToString) <= 15 And (Val(Rt!FNH01.ToString) > 0) Then
                    '        _totalCal = _totalCal + _H01Qty
                    '    End If

                    '    If (Val(Rt!FNH02.ToString) > 0) Then
                    '        _totalCal = _totalCal + _H02Qty
                    '    End If

                    '    If (Val(Rt!FNH03.ToString) > 0) Then
                    '        _totalCal = _totalCal + _H03Qty
                    '    End If

                    '    If (Val(Rt!FNH04.ToString) > 0) And Val(Rt!FNRetireMMin.ToString) <= 0 Then
                    '        _totalCal = _totalCal + _H04Qty
                    '    End If

                    '    If (Val(Rt!FNH05.ToString) > 0) And Val(Rt!FNLateAfMin.ToString) <= 5 Then
                    '        _totalCal = _totalCal + _H05Qty
                    '    End If

                    '    If (Val(Rt!FNH06.ToString) > 0) Then
                    '        _totalCal = _totalCal + _H06Qty
                    '    End If

                    '    If (Val(Rt!FNH07.ToString) > 0) Then
                    '        _totalCal = _totalCal + _H07Qty
                    '    End If

                    '    If (Val(Rt!FNH08.ToString) > 0) And Val(Rt!FNRetireAfMin.ToString) <= 0 Then
                    '        _totalCal = _totalCal + _H08Qty
                    '    End If

                    '    If (Val(Rt!FNH09.ToString) > 0) And Val(Rt!FNLateOtMin.ToString) <= 5 Then
                    '        _totalCal = _totalCal + _H09Qty
                    '    End If

                    '    If _FNTotalOTWorkingMin > 60 And _FNTotalOTWorkingMin <= 120 Then
                    '        If (Val(Rt!FNH10.ToString) > 0) And Val(Rt!FNRetireOtMin.ToString) <= 0 Then
                    '            _totalCal = _totalCal + _H10Qty
                    '        End If
                    '    Else
                    '        If (Val(Rt!FNH10.ToString) > 0) Then
                    '            _totalCal = _totalCal + _H10Qty
                    '        End If
                    '    End If

                    '    If _FNTotalOTWorkingMin > 120 And _FNTotalOTWorkingMin <= 180 Then
                    '        If (Val(Rt!FNH11.ToString) > 0) And Val(Rt!FNRetireOtMin.ToString) <= 0 Then
                    '            _totalCal = _totalCal + _H11Qty
                    '        End If
                    '    Else
                    '        If (Val(Rt!FNH11.ToString) > 0) Then
                    '            _totalCal = _totalCal + _H11Qty
                    '        End If
                    '    End If

                    '    If _FNTotalOTWorkingMin > 180 And _FNTotalOTWorkingMin <= 240 Then
                    '        If (Val(Rt!FNH12.ToString) > 0) And Val(Rt!FNRetireOtMin.ToString) <= 0 Then
                    '            _totalCal = _totalCal + _H12Qty
                    '        End If
                    '    Else
                    '        If (Val(Rt!FNH12.ToString) > 0) Then
                    '            _totalCal = _totalCal + _H12Qty
                    '        End If

                    '    End If

                    '    If _FNTotalOTWorkingMin > 240 And _FNTotalOTWorkingMin <= 300 Then
                    '        If (Val(Rt!FNH13.ToString) > 0) And Val(Rt!FNRetireOtMin.ToString) <= 0 Then
                    '            _totalCal = _totalCal + _H13Qty
                    '        End If
                    '    Else
                    '        If (Val(Rt!FNH13.ToString) > 0) Then
                    '            _totalCal = _totalCal + _H13Qty
                    '        End If
                    '    End If

                    '    _EmpDedeuctAmt = (_totalCal * _PriceCost)
                    '    _EmpDedeuctNetAmt = Double.Parse(Format(_EmpDedeuctAmt / _CountEmpIncentive, "0.00"))

                    '    _DeductAmt = _DeductAmt + _EmpDedeuctNetAmt

                    '    _Qry = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTIncentive_Emp_Deduct ("
                    '    _Qry &= vbCrLf & "  FTInsUser, FDInsDate, FTInsTime, FTCalDate, FNHSysUnitSectId, FNHSysEmpID"
                    '    _Qry &= vbCrLf & ", FNQuantity, FNPrice, FNAmt, FNTotalEmp, FNEmpAmt"
                    '    _Qry &= vbCrLf & ")"
                    '    _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    '    _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & ""
                    '    _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & ""
                    '    _Qry &= vbCrLf & " ,'" & _CalDate & "'   "
                    '    _Qry &= vbCrLf & " ," & Integer.Parse(Val(R!FNHSysUnitSectId.ToString)) & ""
                    '    _Qry &= vbCrLf & " ," & Integer.Parse(Val(Rt!FNHSysEmpID.ToString)) & ""
                    '    _Qry &= vbCrLf & " ," & _totalCal & ""
                    '    _Qry &= vbCrLf & " ," & _PriceCost & ""
                    '    _Qry &= vbCrLf & " ," & _EmpDedeuctAmt & ""
                    '    _Qry &= vbCrLf & " ," & _CountEmpIncentive & ""
                    '    _Qry &= vbCrLf & " ," & _EmpDedeuctNetAmt & ""

                    '    HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

                    '    _Qry = " UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTIncentive_Emp "
                    '    _Qry &= vbCrLf & "  SET  FNAmtOldIncentive=" & _EmpDedeuctNetAmt & ""
                    '    _Qry &= vbCrLf & "  , FNAmtOTOldIncentive=" & _FNAmtOT & ""
                    '    _Qry &= vbCrLf & "  , FNNetAmtOldIncentive=" & (_EmpDedeuctNetAmt + _FNQAValue) & ""
                    '    _Qry &= vbCrLf & "   ,FNQAValue=" & _FNQAValue & " "
                    '    _Qry &= vbCrLf & "   ,FNAmtNewIncentive = " & _EmpDedeuctNetAmt & ""
                    '    _Qry &= vbCrLf & "  , FNAmtOTNewIncentive=" & _FNAmtOT & ""
                    '    _Qry &= vbCrLf & "  , FNNetAmtNewIncentive=" & (_EmpDedeuctNetAmt + _FNQAValue) & ""
                    '    _Qry &= vbCrLf & "  WHERE FNHSysUnitSectId=" & Integer.Parse(Val(R!FNHSysUnitSectId.ToString)) & ""
                    '    _Qry &= vbCrLf & "       AND FNHSysEmpID=" & Integer.Parse(Val(Rt!FNHSysEmpID.ToString)) & ""
                    '    _Qry &= vbCrLf & "       AND FTCalDate='" & _CalDate & "'"

                    '    HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

                    '    Rt!FTStateCal = "1"

                    'Next

                    ''-----------สายบ่าย เกิน 5 นาที ไม่ได้ Incentive ในชั่วโมงนั้น
                    'For Each Rt As DataRow In _dtemptime.Select("(FNLateAfMin > 5 AND FTStateCal<>'1'  AND FNTotalWorkingMin>0) OR (FNRetireMMin > 0 AND FTStateCal<>'1'  AND FNTotalWorkingMin>0) OR (FNRetireAfMin > 0 AND FTStateCal<>'1'  AND FNTotalWorkingMin>0) OR (FNLateOtMin > 5 AND FTStateCal<>'1'  AND FNTotalWorkingMin>0)  OR (FNRetireOtMin > 0 AND FTStateCal<>'1'  AND FNTotalWorkingMin>0)")
                    '    _totalCal = 0

                    '    _Salary = Double.Parse(Val(Rt!FNSalary.ToString))
                    '    _SalaryPerH = Double.Parse(Format(_Salary / 8, "0.00"))
                    '    _SalaryPerOT = Double.Parse(Format(_SalaryPerH * 1.5, "0.00"))


                    '    If _FixHour > 0 Then

                    '    End If

                    '    _FNAmtOT = Double.Parse(Format(((_Salary / 480) * 0.5) * Integer.Parse(Val(Rt!FNWorkingOTMin.ToString)), "0.00"))


                    '    If Val(Rt!FNLateMMin.ToString) <= 15 And (Val(Rt!FNH01.ToString) > 0) Then
                    '        _totalCal = _totalCal + _H01Qty
                    '    End If

                    '    If (Val(Rt!FNH02.ToString) > 0) Then
                    '        _totalCal = _totalCal + _H02Qty
                    '    End If

                    '    If (Val(Rt!FNH03.ToString) > 0) Then
                    '        _totalCal = _totalCal + _H03Qty
                    '    End If

                    '    If (Val(Rt!FNH04.ToString) > 0) And Val(Rt!FNRetireMMin.ToString) <= 0 Then
                    '        _totalCal = _totalCal + _H04Qty
                    '    End If

                    '    If (Val(Rt!FNH05.ToString) > 0) And Val(Rt!FNLateAfMin.ToString) <= 5 Then
                    '        _totalCal = _totalCal + _H05Qty
                    '    End If

                    '    If (Val(Rt!FNH06.ToString) > 0) Then
                    '        _totalCal = _totalCal + _H06Qty
                    '    End If

                    '    If (Val(Rt!FNH07.ToString) > 0) Then
                    '        _totalCal = _totalCal + _H07Qty
                    '    End If

                    '    If (Val(Rt!FNH08.ToString) > 0) And Val(Rt!FNRetireAfMin.ToString) <= 0 Then
                    '        _totalCal = _totalCal + _H08Qty
                    '    End If

                    '    If (Val(Rt!FNH09.ToString) > 0) And Val(Rt!FNLateOtMin.ToString) <= 5 Then
                    '        _totalCal = _totalCal + _H09Qty
                    '    End If

                    '    If _FNTotalOTWorkingMin > 60 And _FNTotalOTWorkingMin <= 120 Then
                    '        If (Val(Rt!FNH10.ToString) > 0) And Val(Rt!FNRetireOtMin.ToString) <= 0 Then
                    '            _totalCal = _totalCal + _H10Qty
                    '        End If
                    '    Else
                    '        If (Val(Rt!FNH10.ToString) > 0) Then
                    '            _totalCal = _totalCal + _H10Qty
                    '        End If
                    '    End If

                    '    If _FNTotalOTWorkingMin > 120 And _FNTotalOTWorkingMin <= 180 Then
                    '        If (Val(Rt!FNH11.ToString) > 0) And Val(Rt!FNRetireOtMin.ToString) <= 0 Then
                    '            _totalCal = _totalCal + _H11Qty
                    '        End If
                    '    Else
                    '        If (Val(Rt!FNH11.ToString) > 0) Then
                    '            _totalCal = _totalCal + _H11Qty
                    '        End If
                    '    End If

                    '    If _FNTotalOTWorkingMin > 180 And _FNTotalOTWorkingMin <= 240 Then
                    '        If (Val(Rt!FNH12.ToString) > 0) And Val(Rt!FNRetireOtMin.ToString) <= 0 Then
                    '            _totalCal = _totalCal + _H12Qty
                    '        End If
                    '    Else
                    '        If (Val(Rt!FNH12.ToString) > 0) Then
                    '            _totalCal = _totalCal + _H12Qty
                    '        End If

                    '    End If

                    '    If _FNTotalOTWorkingMin > 240 And _FNTotalOTWorkingMin <= 300 Then
                    '        If (Val(Rt!FNH13.ToString) > 0) And Val(Rt!FNRetireOtMin.ToString) <= 0 Then
                    '            _totalCal = _totalCal + _H13Qty
                    '        End If
                    '    Else
                    '        If (Val(Rt!FNH13.ToString) > 0) Then
                    '            _totalCal = _totalCal + _H13Qty
                    '        End If
                    '    End If

                    '    _EmpDedeuctAmt = (_totalCal * _PriceCost)
                    '    _EmpDedeuctNetAmt = Double.Parse(Format(_EmpDedeuctAmt / _CountEmpIncentive, "0.00"))

                    '    _DeductAmt = _DeductAmt + _EmpDedeuctNetAmt

                    '    _Qry = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTIncentive_Emp_Deduct ("
                    '    _Qry &= vbCrLf & "  FTInsUser, FDInsDate, FTInsTime, FTCalDate, FNHSysUnitSectId, FNHSysEmpID"
                    '    _Qry &= vbCrLf & ", FNQuantity, FNPrice, FNAmt, FNTotalEmp, FNEmpAmt"
                    '    _Qry &= vbCrLf & ")"
                    '    _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    '    _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & ""
                    '    _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & ""
                    '    _Qry &= vbCrLf & " ,'" & _CalDate & "'   "
                    '    _Qry &= vbCrLf & " ," & Integer.Parse(Val(R!FNHSysUnitSectId.ToString)) & ""
                    '    _Qry &= vbCrLf & " ," & Integer.Parse(Val(Rt!FNHSysEmpID.ToString)) & ""
                    '    _Qry &= vbCrLf & " ," & _totalCal & ""
                    '    _Qry &= vbCrLf & " ," & _PriceCost & ""
                    '    _Qry &= vbCrLf & " ," & _EmpDedeuctAmt & ""
                    '    _Qry &= vbCrLf & " ," & _CountEmpIncentive & ""
                    '    _Qry &= vbCrLf & " ," & _EmpDedeuctNetAmt & ""


                    '    HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

                    '    _Qry = " UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTIncentive_Emp "
                    '    _Qry &= vbCrLf & "  SET  FNAmtOldIncentive=" & _EmpDedeuctNetAmt & ""
                    '    _Qry &= vbCrLf & "  , FNAmtOTOldIncentive=" & _FNAmtOT & ""
                    '    _Qry &= vbCrLf & "  , FNNetAmtOldIncentive=" & (_EmpDedeuctNetAmt + _FNQAValue) & ""
                    '    _Qry &= vbCrLf & "   ,FNQAValue=" & _FNQAValue & " "
                    '    _Qry &= vbCrLf & "   ,FNAmtNewIncentive = " & _EmpDedeuctNetAmt & """"
                    '    _Qry &= vbCrLf & "  , FNAmtOTNewIncentive=" & _FNAmtOT & ""
                    '    _Qry &= vbCrLf & "  , FNNetAmtNewIncentive=" & (_EmpDedeuctNetAmt + _FNQAValue) & ""
                    '    _Qry &= vbCrLf & "  WHERE FNHSysUnitSectId=" & Integer.Parse(Val(R!FNHSysUnitSectId.ToString)) & ""
                    '    _Qry &= vbCrLf & "       AND FNHSysEmpID=" & Integer.Parse(Val(Rt!FNHSysEmpID.ToString)) & ""
                    '    _Qry &= vbCrLf & "       AND FTCalDate='" & _CalDate & "'"

                    '    HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

                    '    Rt!FTStateCal = "1"

                    'Next

                    'For Each Rt As DataRow In _dtemptime.Select(" FNTotalWorkingMin +FNLateMMin <" & _FNTotalWorkingMin & " AND FNLateOtMin<=0 AND FNLateAFMin<=0 AND FTStateCal<>'1'  AND FNTotalWorkingMin>0 ")
                    '    _totalCal = 0

                    '    _Salary = Double.Parse(Val(Rt!FNSalary.ToString))
                    '    _SalaryPerH = Double.Parse(Format(_Salary / 8, "0.00"))
                    '    _SalaryPerOT = Double.Parse(Format(_SalaryPerH * 1.5, "0.00"))

                    '    If Rt!FTEmpCode = "91SSA00033" Then
                    '        Beep()
                    '    End If
                    '    If _FixHour > 0 Then

                    '    End If

                    '    _FNAmtOT = Double.Parse(Format(((_Salary / 480) * 0.5) * Integer.Parse(Val(Rt!FNWorkingOTMin.ToString)), "0.00"))


                    '    If Val(Rt!FNLateMMin.ToString) <= 15 And (Val(Rt!FNH01.ToString) > 0) Then
                    '        _totalCal = _totalCal + _H01Qty
                    '    End If

                    '    If (Val(Rt!FNH02.ToString) > 0) Then
                    '        _totalCal = _totalCal + _H02Qty
                    '    End If

                    '    If (Val(Rt!FNH03.ToString) > 0) Then
                    '        _totalCal = _totalCal + _H03Qty
                    '    End If

                    '    If (Val(Rt!FNH04.ToString) > 0) And Val(Rt!FNRetireMMin.ToString) <= 0 Then
                    '        _totalCal = _totalCal + _H04Qty
                    '    End If

                    '    If (Val(Rt!FNH05.ToString) > 0) And Val(Rt!FNLateAfMin.ToString) <= 5 Then
                    '        _totalCal = _totalCal + _H05Qty
                    '    End If

                    '    If (Val(Rt!FNH06.ToString) > 0) Then
                    '        _totalCal = _totalCal + _H06Qty
                    '    End If

                    '    If (Val(Rt!FNH07.ToString) > 0) Then
                    '        _totalCal = _totalCal + _H07Qty
                    '    End If

                    '    If (Val(Rt!FNH08.ToString) > 0) And Val(Rt!FNRetireAfMin.ToString) <= 0 Then
                    '        _totalCal = _totalCal + _H08Qty
                    '    End If

                    '    If (Val(Rt!FNH09.ToString) > 0) And Val(Rt!FNLateOtMin.ToString) <= 5 Then
                    '        _totalCal = _totalCal + _H09Qty
                    '    End If

                    '    If _FNTotalOTWorkingMin > 60 And _FNTotalOTWorkingMin <= 120 Then
                    '        If (Val(Rt!FNH10.ToString) > 0) And Val(Rt!FNRetireOtMin.ToString) <= 0 Then
                    '            _totalCal = _totalCal + _H10Qty
                    '        End If
                    '    Else
                    '        If (Val(Rt!FNH10.ToString) > 0) Then
                    '            _totalCal = _totalCal + _H10Qty
                    '        End If
                    '    End If

                    '    If _FNTotalOTWorkingMin > 120 And _FNTotalOTWorkingMin <= 180 Then
                    '        If (Val(Rt!FNH11.ToString) > 0) And Val(Rt!FNRetireOtMin.ToString) <= 0 Then
                    '            _totalCal = _totalCal + _H11Qty
                    '        End If
                    '    Else
                    '        If (Val(Rt!FNH11.ToString) > 0) Then
                    '            _totalCal = _totalCal + _H11Qty
                    '        End If
                    '    End If

                    '    If _FNTotalOTWorkingMin > 180 And _FNTotalOTWorkingMin <= 240 Then
                    '        If (Val(Rt!FNH12.ToString) > 0) And Val(Rt!FNRetireOtMin.ToString) <= 0 Then
                    '            _totalCal = _totalCal + _H12Qty
                    '        End If
                    '    Else
                    '        If (Val(Rt!FNH12.ToString) > 0) Then
                    '            _totalCal = _totalCal + _H12Qty
                    '        End If

                    '    End If

                    '    If _FNTotalOTWorkingMin > 240 And _FNTotalOTWorkingMin <= 300 Then
                    '        If (Val(Rt!FNH13.ToString) > 0) And Val(Rt!FNRetireOtMin.ToString) <= 0 Then
                    '            _totalCal = _totalCal + _H13Qty
                    '        End If
                    '    Else
                    '        If (Val(Rt!FNH13.ToString) > 0) Then
                    '            _totalCal = _totalCal + _H13Qty
                    '        End If
                    '    End If


                    '    _EmpDedeuctAmt = (_totalCal * _PriceCost)
                    '    _EmpDedeuctNetAmt = Double.Parse(Format(_EmpDedeuctAmt / _CountEmpIncentive, "0.00"))

                    '    _DeductAmt = _DeductAmt + _EmpDedeuctNetAmt

                    '    _Qry = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTIncentive_Emp_Deduct ("
                    '    _Qry &= vbCrLf & "  FTInsUser, FDInsDate, FTInsTime, FTCalDate, FNHSysUnitSectId, FNHSysEmpID"
                    '    _Qry &= vbCrLf & ", FNQuantity, FNPrice, FNAmt, FNTotalEmp, FNEmpAmt"
                    '    _Qry &= vbCrLf & ")"
                    '    _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    '    _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & ""
                    '    _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & ""
                    '    _Qry &= vbCrLf & " ,'" & _CalDate & "'   "
                    '    _Qry &= vbCrLf & " ," & Integer.Parse(Val(R!FNHSysUnitSectId.ToString)) & ""
                    '    _Qry &= vbCrLf & " ," & Integer.Parse(Val(Rt!FNHSysEmpID.ToString)) & ""
                    '    _Qry &= vbCrLf & " ," & _totalCal & ""
                    '    _Qry &= vbCrLf & " ," & _PriceCost & ""
                    '    _Qry &= vbCrLf & " ," & _EmpDedeuctAmt & ""
                    '    _Qry &= vbCrLf & " ," & _CountEmpIncentive & ""
                    '    _Qry &= vbCrLf & " ," & _EmpDedeuctNetAmt & ""


                    '    HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

                    '    _Qry = " UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTIncentive_Emp "
                    '    _Qry &= vbCrLf & "  SET  FNAmtOldIncentive=" & _EmpDedeuctNetAmt & ""
                    '    _Qry &= vbCrLf & "  , FNAmtOTOldIncentive=" & _FNAmtOT & ""
                    '    _Qry &= vbCrLf & "  , FNNetAmtOldIncentive=" & (_EmpDedeuctNetAmt + _FNQAValue) & ""
                    '    _Qry &= vbCrLf & "   ,FNQAValue=" & _FNQAValue & " "
                    '    _Qry &= vbCrLf & "   ,FNAmtNewIncentive = " & _EmpDedeuctNetAmt & ""
                    '    _Qry &= vbCrLf & "  , FNAmtOTNewIncentive=" & _FNAmtOT & ""
                    '    _Qry &= vbCrLf & "  , FNNetAmtNewIncentive=" & (_EmpDedeuctNetAmt + _FNQAValue) & ""
                    '    _Qry &= vbCrLf & "  WHERE FNHSysUnitSectId=" & Integer.Parse(Val(R!FNHSysUnitSectId.ToString)) & ""
                    '    _Qry &= vbCrLf & "       AND FNHSysEmpID=" & Integer.Parse(Val(Rt!FNHSysEmpID.ToString)) & ""
                    '    _Qry &= vbCrLf & "       AND FTCalDate='" & _CalDate & "'"

                    '    HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

                    '    Rt!FTStateCal = "1"

                    'Next

                    '--------คำนวณคนที่มาทพงานไม่ครบ---------------
                    Dim _TotalTimeMin As Integer = 0
                    Dim _TotalEmpAmt As Double = 0
                    Dim _EmpAmt As Double = 0
                    Dim _TotalEmpCal As Integer = 0

                    _TotalTime = 0
                    _TotalTimeOT = 0

                    'For Each Rt As DataRow In _dtemptime.Rows
                    For Each Rt As DataRow In _dtemptime.Select("FTStateCal<>'1'  AND FNTotalWorkingMin>0 ")

                        _TotalTimeMin = _TotalTimeMin + Integer.Parse(Val(Rt!FNTotalWorkingMin.ToString))
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

                    For Each Rt As DataRow In _dtemptime.Select("FTStateCal<>'1'  AND FNTotalWorkingMin>0 ", "FNTotalWorkingMin")
                        _TotalEmpCal = _TotalEmpCal + 1

                        _Salary = Double.Parse(Val(Rt!FNSalary.ToString))
                        _SalaryPerH = Double.Parse(Format(_Salary / 8, "0.00"))
                        _SalaryPerOT = Double.Parse(Format(_SalaryPerH * 1.5, "0.00"))

                        If _FixHour > 0 Then
                        End If

                        If _TotalEmpCal = _CountEmpIncentive Then
                            _EmpIncentiveAmt = _LineIncentiveAmt - _TotalEmpAmt
                        Else
                            _EmpIncentiveAmt = CDbl(Format((_LineIncentiveAmt * (Integer.Parse(Val(Rt!FNTotalWorkingMin.ToString)))) / _TotalTimeMin, "0.00"))
                        End If

                        _TotalEmpAmt = _TotalEmpAmt + _EmpIncentiveAmt

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
                        Case 1, 2, 3, 4

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



                            If _FNHSysIncenFormulaId <= 0 Then

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

                            Else

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
                                _Qry &= vbCrLf & "    FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TPRODMIncentiveFormulaLevel AS P WITH(NOLOCK)"
                                _Qry &= vbCrLf & " WHERE FNHSysIncenFormulaId=" & _FNHSysIncenFormulaId & ""
                                _Qry &= vbCrLf & "  ORDER BY FNLVSeq"

                            End If

                            _dtPrice = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MASTER)

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

                                'For Each Rt As DataRow In _dtemptime.Select("FTStateCal<>'1'  AND FNTotalWorkingMin>0")

                                '    _Salary = Double.Parse(Val(Rt!FNSalary.ToString))
                                '    _SalaryPerH = Double.Parse(Format(_Salary / 8, "0.00"))
                                '    _SalaryPerOT = Double.Parse(Format(_SalaryPerH * 1.5, "0.00"))

                                '    _FNAmtOT = Double.Parse(Format(((_Salary / 480) * 0.5) * Integer.Parse(Val(Rt!FNWorkingOTMin.ToString)), "0.00"))

                                '    _FNNetAmt = _EmpIncentiveAmt + _FNAmtOT

                                '    _Qry = " UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTIncentive_Emp "
                                '    _Qry &= vbCrLf & "  SET  FNAmtNewIncentive=" & _EmpIncentiveAmt & ""
                                '    _Qry &= vbCrLf & "  , FNAmtOTNewIncentive=" & _FNAmtOT & ""
                                '    _Qry &= vbCrLf & "  , FNNetAmtNewIncentive=" & (_FNNetAmt + _FNQAValue) & ""
                                '    _Qry &= vbCrLf & "   ,FNQAValue=" & _FNQAValue & " "
                                '    _Qry &= vbCrLf & "  WHERE FNHSysUnitSectId=" & Integer.Parse(Val(R!FNHSysUnitSectId.ToString)) & ""
                                '    _Qry &= vbCrLf & "       AND FNHSysEmpID=" & Integer.Parse(Val(Rt!FNHSysEmpID.ToString)) & ""
                                '    _Qry &= vbCrLf & "       AND FTCalDate='" & _CalDate & "'"

                                '    HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

                                'Next

                                _TotalEmpCal = 0
                                _TotalEmpAmt = 0
                                _TotalTimeMin = 0

                                For Each Rt As DataRow In _dtemptime.Select("FTStateCal<>'1'  AND FNTotalWorkingMin>0 ", "FNTotalWorkingMin")
                                    _TotalTimeMin = _TotalTimeMin + Integer.Parse(Val(Rt!FNTotalWorkingMin.ToString))
                                Next

                                For Each Rt As DataRow In _dtemptime.Select("FTStateCal<>'1'  AND FNTotalWorkingMin>0 ", "FNTotalWorkingMin")

                                    _TotalEmpCal = _TotalEmpCal + 1

                                    _Salary = Double.Parse(Val(Rt!FNSalary.ToString))
                                    _SalaryPerH = Double.Parse(Format(_Salary / 8, "0.00"))
                                    _SalaryPerOT = Double.Parse(Format(_SalaryPerH * 1.5, "0.00"))

                                    If _FixHour > 0 Then
                                    End If

                                    If _TotalEmpCal = _CountEmpIncentive Then
                                        _EmpIncentiveAmt = _LineIncentiveAmt - _TotalEmpAmt
                                    Else
                                        _EmpIncentiveAmt = CDbl(Format((_LineIncentiveAmt * (Integer.Parse(Val(Rt!FNTotalWorkingMin.ToString)))) / _TotalTimeMin, "0.00"))
                                    End If

                                    _TotalEmpAmt = _TotalEmpAmt + _EmpIncentiveAmt

                                    _FNAmtOT = Double.Parse(Format(((_Salary / 480) * 0.5) * Integer.Parse(Val(Rt!FNWorkingOTMin.ToString)), "0.00"))

                                    _FNNetAmt = _EmpIncentiveAmt + _FNAmtOT

                                    _Qry = " UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTIncentive_Emp "
                                    _Qry &= vbCrLf & "  SET  FNAmtNewIncentive=" & _EmpIncentiveAmt & ""
                                    _Qry &= vbCrLf & "  , FNAmtOTNewIncentive=" & _FNAmtOT & ""
                                    _Qry &= vbCrLf & "  , FNNetAmtNewIncentive=" & (_FNNetAmt + _FNQAValue) & ""
                                    _Qry &= vbCrLf & "   ,FNQAValue=" & _FNQAValue & " "
                                    _Qry &= vbCrLf & "   WHERE FNHSysUnitSectId=" & Integer.Parse(Val(R!FNHSysUnitSectId.ToString)) & ""
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

                    _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & ""
                    _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & ""

                    _Qry &= vbCrLf & " ," & Integer.Parse(Val(Rt!FNHSysEmpID.ToString)) & ""
                    _Qry &= vbCrLf & " ,'" & _CalDate & "'   "
                    _Qry &= vbCrLf & "  ,MAX(FNAmtNormal) AS FNAmtNormal"
                    _Qry &= vbCrLf & " , MAX(FNAmtOT) AS FNAmtOT"
                    _Qry &= vbCrLf & " , MAX(FNNetAmt) AS FNNetAmt"

                    Select Case Val(R!FTIncentiveTypeIdx.ToString)
                        Case 1, 2, 3, 4
                            _Qry &= vbCrLf & ", Sum(FNAmtNewIncentive) AS FNAmtNewIncentive"
                            _Qry &= vbCrLf & ", Sum(FNAmtOTNewIncentive) AS FNAmtOTNewIncentive,0 AS FNProOther"
                            _Qry &= vbCrLf & ", Sum(FNNetAmtNewIncentive) AS FNNetAmtNewIncentive "
                        Case Else
                            _Qry &= vbCrLf & ", Sum(FNAmtOldIncentive) AS FNAmtOldIncentive"
                            _Qry &= vbCrLf & ", Sum(FNAmtOTOldIncentive) AS FNAmtOTOldIncentive,0 AS FNProOther"
                            _Qry &= vbCrLf & ", Sum(FNNetAmtOldIncentive) AS FNNetAmtOldIncentive"
                    End Select

                    _Qry &= vbCrLf & ", Sum(FNQAValue) AS FNQAValue"
                    _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTIncentive_Emp  AS X WITH(NOLOCK) "
                    _Qry &= vbCrLf & "  WHERE  FNHSysEmpID=" & Integer.Parse(Val(Rt!FNHSysEmpID.ToString)) & ""
                    _Qry &= vbCrLf & "       AND FTCalDate='" & _CalDate & "'"

                    HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

                Next

                _dtemptime.Dispose()

            Next

            If _dt.Select("FTSelect='1' AND FNHSysEmpID>0").Length > 0 Then
                Call CalculateEmpWring(_dt)
            End If

            _dt.Dispose()
        Catch ex As Exception
        End Try
        Return True
    End Function

    Private Function CalculateSewingHour(Spls As HI.TL.SplashScreen) As Boolean

        Dim _dt As DataTable
        Dim _dtemptime As DataTable
        Dim _dtsewstyle As DataTable
        Dim _dtsewstylepack As DataTable
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

        Dim _FullTotalTimeHR As Integer = 0
        Dim _FullTotalTimeOTHR As Integer = 0

        Dim _Salary As Double = 0
        Dim _SalaryPerH As Double = 0
        Dim _SalaryPerOT As Double = 0
        Dim _ProdAmt As Double = 0
        Dim _ProdOTAmt As Double = 0
        Dim _WageAmt As Double = 0
        Dim _WageOTAmt As Double = 0
        Dim _SumSam As Double = 0
        Dim _MaxCalSam As Double = 0
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
        Dim _FNHSysIncenFormulaId As Integer = 0
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
        Dim _PackAmt As Double = 0
        Dim _PackQty As Double = 0

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

                _Qry = "SELECT TOP 1 FNHSysIncenFormulaId FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS X WITH(NOLOCK) WHERE FNHSysUnitSectId=" & Val(R!FNHSysUnitSectId.ToString) & ""
                _FNHSysIncenFormulaId = Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MASTER, "0"))

                _dtemptime = GetEmpTime(Integer.Parse(Val(R!FNHSysUnitSectId.ToString)), R!FDScanDateOrg.ToString).Copy()

                '_Qry = "SELECT FNHSysStyleId, FTOrderNo, FTSubOrderNo, SUM(FNScanQuantity) AS FNScanQuantity, MAX(FNSam) AS FNSam, MAX(FNPrice) AS FNPrice, MAX(FNMultiple) AS FNMultiple, FNNetPrice, SUM(FNNetAmt) AS FNNetAmt"

                '_Qry &= vbCrLf & "FROM ( SELECT M1.FNHSysStyleId,M1.FTOrderNo,M1.FTSubOrderNo,ISNULL(XXA.FNTotalQty,M1.FNScanQuantity) As FNScanQuantity,ISNULL(P.FNSam,0) AS FNSam,ISNULL(P.FNPrice,0) AS FNPrice,ISNULL(P.FNMultiple,0) AS FNMultiple"
                '_Qry &= vbCrLf & ",Convert(numeric(18,4),ISNULL(P.FNPrice,0) ) AS FNNetPrice	"
                '_Qry &= vbCrLf & ",Convert(numeric(18,2),Convert(numeric(18,4),ISNULL(P.FNPrice,0) ) * ISNULL(XXA.FNTotalQty,M1.FNScanQuantity))  AS FNNetAmt	"
                '_Qry &= vbCrLf & " FROM ( "
                '_Qry &= vbCrLf & " SELECT  MM.FTOrderNo ,MM.FTSubOrderNo, MM.FNHSysStyleId,Sum(MM.FNScanQuantity) AS FNScanQuantity"
                '_Qry &= vbCrLf & " FROM (SELECT  P.FTOrderNo ,P.FTSubOrderNo, A.FNHSysStyleId,Sum(FNScanQuantity) AS FNScanQuantity"
                '_Qry &= vbCrLf & "   FROM"
                '_Qry &= vbCrLf & "	("
                '_Qry &= vbCrLf & " SELECT    S.FTInsUser, S.FDInsDate, S.FTInsTime, S.FTUpdUser, S.FDUpdDate, S.FTUpdTime, S.FTPackNo, S.FNCartonNo, S.FTOrderNo, S.FTSubOrderNo, S.FTColorway, S.FTSizeBreakDown, "
                '_Qry &= vbCrLf & "   S.FNHSysUnitSectId, S.FTBarcodeNo, S.FDScanDate, S.FDScanTime, S.FNScanQuantity"
                '_Qry &= vbCrLf & "FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan_Detail AS S WITH(NOLOCK)  LEFT OUTER JOIN"
                '_Qry &= vbCrLf & "	     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScanOutline AS O WITH(NOLOCK)  ON S.FTBarcodeNo = O.FTBarcodeNo"
                '_Qry &= vbCrLf & " WHERE S.FNHSysUnitSectId  =" & Integer.Parse(Val(R!FNHSysUnitSectId.ToString)) & " "
                '_Qry &= vbCrLf & "       AND S.FDScanDate='" & _CalDate & "'   "
                '_Qry &= vbCrLf & "       AND O.FTBarcodeNo Is NULL"
                '_Qry &= vbCrLf & "UNION"
                '_Qry &= vbCrLf & "SELECT     O.FTInsUser, O.FDInsDate, O.FTInsTime, O.FTUpdUser, O.FDUpdDate, O.FTUpdTime , '' AS FTPackNo,0 , O.FTOrderNo ,O.FTSubOrderNo, B.FTColorway, B.FTSizeBreakDown"
                '_Qry &= vbCrLf & ", O.FNHSysUnitSectId, O.FTBarcodeNo, O.FDDate, O.FTTime, O.FNQuantity"
                '_Qry &= vbCrLf & "FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScanOutline AS O WITH(NOLOCK)  LEFT OUTER JOIN"
                '_Qry &= vbCrLf & "	      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle AS B WITH(NOLOCK)  ON O.FTBarcodeNo = B.FTBarcodeBundleNo "
                '_Qry &= vbCrLf & "	      INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd AS PP WITH(NOLOCK) ON B.FTOrderProdNo = PP.FTOrderProdNo "
                '_Qry &= vbCrLf & " WHERE O.FNHSysUnitSectId  =" & Integer.Parse(Val(R!FNHSysUnitSectId.ToString)) & "  AND ISNULL(O.FNStateSewPack,0) =0 "
                '_Qry &= vbCrLf & " AND O.FDDate='" & _CalDate & "'   "
                '_Qry &= vbCrLf & "	) AS P  RIGHT OUTER JOIN"
                '_Qry &= vbCrLf & "	[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS A  WITH(NOLOCK) ON P.FTOrderNo = A.FTOrderNo  LEFT OUTER JOIN"
                '_Qry &= vbCrLf & "	[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub AS B  WITH(NOLOCK)  ON A.FTOrderNo = B.FTOrderNo  AND P.FTSubOrderNo = B.FTSubOrderNo LEFT OUTER JOIN"
                '_Qry &= vbCrLf & "	[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS S  WITH(NOLOCK)   ON A.FNHSysStyleId = S.FNHSysStyleId"
                '_Qry &= vbCrLf & "	WHERE (FNHSysUnitSectId = " & Integer.Parse(Val(R!FNHSysUnitSectId.ToString)) & " )"
                '_Qry &= vbCrLf & " and P.FDScanDate ='" & _CalDate & "'   "
                '_Qry &= vbCrLf & " GROUP BY  P.FTOrderNo ,P.FTSubOrderNo, A.FNHSysStyleId "
                '_Qry &= vbCrLf & " UNION ALL "
                '_Qry &= vbCrLf & " SELECT  P.FTOrderNo ,P.FTSubOrderNo, A.FNHSysStyleId,Sum(0) AS FNScanQuantity"
                '_Qry &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTEditScanQuantity AS P WITH(NOLOCK)"
                '_Qry &= vbCrLf & "	INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS A  WITH(NOLOCK) ON P.FTOrderNo = A.FTOrderNo"
                '_Qry &= vbCrLf & " WHERE P.FNHSysUnitSectId  =" & Integer.Parse(Val(R!FNHSysUnitSectId.ToString)) & " "
                '_Qry &= vbCrLf & " AND P.FTDate='" & _CalDate & "'  "
                '_Qry &= vbCrLf & " GROUP BY  P.FTOrderNo, P.FTSubOrderNo, A.FNHSysStyleId ) AS MM GROUP BY MM.FTOrderNo ,MM.FTSubOrderNo, MM.FNHSysStyleId "
                '_Qry &= vbCrLf & "   ) AS M1 "
                '_Qry &= vbCrLf & " LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TPPCTSamByOrder AS P WITH(NOLOCK) "
                '_Qry &= vbCrLf & " ON M1.FNHSysStyleId = P.FNHSysStyleId "
                '_Qry &= vbCrLf & " AND M1.FTOrderNo = P.FTOrderNo "
                '_Qry &= vbCrLf & " AND M1.FTSubOrderNo = P.FTSubOrderNo "

                '_Qry &= vbCrLf & " LEFT OUTER JOIN  ("
                '_Qry &= vbCrLf & "   SELECT  FTOrderNo, FTSubOrderNo, SUM(FNTotalQty) AS FNTotalQty"
                '_Qry &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTEditScanQuantity AS TXA WITH(NOLOCK)"
                '_Qry &= vbCrLf & " WHERE TXA.FNHSysUnitSectId  =" & Integer.Parse(Val(R!FNHSysUnitSectId.ToString)) & " "
                '_Qry &= vbCrLf & " AND TXA.FTDate='" & _CalDate & "'  "
                '_Qry &= vbCrLf & "  GROUP BY  FTOrderNo, FTSubOrderNo"
                '_Qry &= vbCrLf & " ) AS XXA ON "
                '_Qry &= vbCrLf & "  M1.FTOrderNo = XXA.FTOrderNo "
                '_Qry &= vbCrLf & " AND M1.FTSubOrderNo =XXA.FTSubOrderNo "
                '_Qry &= vbCrLf & " UNION ALL "

                '_Qry &= vbCrLf & " SELECT M1.FNHSysStyleId,M1.FTOrderNo,M1.FTSubOrderNo,M1.FNScanQuantity As FNScanQuantity,ISNULL(P.FNSam,0) AS FNSam,ISNULL(P.FNPrice,0) AS FNPrice,ISNULL(P.FNMultiple,0) AS FNMultiple"
                '_Qry &= vbCrLf & ",Convert(numeric(18,4),ISNULL(P.FNPrice,0)- ISNULL(M1.FNDisPrice,0) )  AS FNNetPrice "
                '_Qry &= vbCrLf & ",Convert(numeric(18,2),Convert(numeric(18,4),ISNULL(P.FNPrice,0) - ISNULL(M1.FNDisPrice,0)) *M1.FNScanQuantity)  AS FNNetAmt	"
                '_Qry &= vbCrLf & " FROM ( "
                '_Qry &= vbCrLf & " SELECT  MM.FTOrderNo ,MM.FTSubOrderNo, MM.FNHSysStyleId,Sum(MM.FNScanQuantity) AS FNScanQuantity,FNDisPrice"
                '_Qry &= vbCrLf & " FROM ("
                '_Qry &= vbCrLf & " SELECT  P.FTOrderNo ,P.FTSubOrderNo, A.FNHSysStyleId,P.FNScanQuantity AS FNScanQuantity"
                '_Qry &= vbCrLf & ",CASE WHEN P.FNDisPrice <0 THEN ISNULL((SELECT TOP 1 AA2.FNPrice"
                '_Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODMOperationByStyle AS AA2 WITH(NOLOCK) INNER JOIN"
                '_Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TPRODMOperation AS B  WITH(NOLOCK) ON AA2.FNHSysOperationId = B.FNHSysOperationId"
                '_Qry &= vbCrLf & "  WHERE  (B.FTStatePack = '1') AND (AA2.FNHSysStyleId  = A.FNHSysStyleId)),0) ELSE P.FNDisPrice END AS FNDisPrice"
                '_Qry &= vbCrLf & "   FROM"
                '_Qry &= vbCrLf & "	("

                '_Qry &= vbCrLf & "SELECT     O.FTInsUser, O.FDInsDate, O.FTInsTime, O.FTUpdUser, O.FDUpdDate, O.FTUpdTime , '' AS FTPackNo,0  AS FNCartonNo"
                '_Qry &= vbCrLf & " ,O.FTOrderNo ,O.FTSubOrderNo, B.FTColorway, B.FTSizeBreakDown"
                '_Qry &= vbCrLf & " ,O.FNHSysUnitSectId, O.FTBarcodeNo, O.FDDate As FDScanDate, O.FTTime AS FDScanTime, O.FNQuantity AS FNScanQuantity"
                '_Qry &= vbCrLf & " ,ISNULL((SELECT TOP 1 A.FNPrice"
                '_Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOperationByOrderProd AS A  WITH(NOLOCK) INNER JOIN"
                '_Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TPRODMOperation AS B  WITH(NOLOCK) ON A.FNHSysOperationId = B.FNHSysOperationId"
                '_Qry &= vbCrLf & " WHERE  (B.FTStatePack = '1') AND (A.FTOrderProdNo = PP.FTOrderProdNo)) , -1 ) AS FNDisPrice"
                '_Qry &= vbCrLf & "FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScanOutline AS O WITH(NOLOCK)  LEFT OUTER JOIN"
                '_Qry &= vbCrLf & "	      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle AS B WITH(NOLOCK)  ON O.FTBarcodeNo = B.FTBarcodeBundleNo "
                '_Qry &= vbCrLf & "	      INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd AS PP WITH(NOLOCK) ON B.FTOrderProdNo = PP.FTOrderProdNo "
                '_Qry &= vbCrLf & " WHERE O.FNHSysUnitSectId  =" & Integer.Parse(Val(R!FNHSysUnitSectId.ToString)) & "  AND ISNULL(O.FNStateSewPack,0) =1 "
                '_Qry &= vbCrLf & " AND O.FDDate='" & _CalDate & "'   "
                '_Qry &= vbCrLf & "	) AS P  RIGHT OUTER JOIN"
                '_Qry &= vbCrLf & "	[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS A  WITH(NOLOCK) ON P.FTOrderNo = A.FTOrderNo  LEFT OUTER JOIN"
                '_Qry &= vbCrLf & "	[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub AS B  WITH(NOLOCK)  ON A.FTOrderNo = B.FTOrderNo  AND P.FTSubOrderNo = B.FTSubOrderNo LEFT OUTER JOIN"
                '_Qry &= vbCrLf & "	[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS S  WITH(NOLOCK)   ON A.FNHSysStyleId = S.FNHSysStyleId"
                '_Qry &= vbCrLf & "	WHERE (FNHSysUnitSectId = " & Integer.Parse(Val(R!FNHSysUnitSectId.ToString)) & " )"
                '_Qry &= vbCrLf & " and P.FDScanDate ='" & _CalDate & "'   "
                '_Qry &= vbCrLf & " ) AS MM GROUP BY MM.FTOrderNo ,MM.FTSubOrderNo, MM.FNHSysStyleId,FNDisPrice "
                '_Qry &= vbCrLf & "   ) AS M1 "
                '_Qry &= vbCrLf & " LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TPPCTSamByOrder AS P WITH(NOLOCK) "
                '_Qry &= vbCrLf & " ON M1.FNHSysStyleId = P.FNHSysStyleId "
                '_Qry &= vbCrLf & " AND M1.FTOrderNo = P.FTOrderNo "
                '_Qry &= vbCrLf & " AND M1.FTSubOrderNo = P.FTSubOrderNo "
                '_Qry &= vbCrLf & " ) AS X GROUP BY FNHSysStyleId, FTOrderNo, FTSubOrderNo,  FNNetPrice"

                _LineAmt = 0
                _LineIncentiveAmt = 0
                _CountStyle = 0
                _SumSam = 0
                _MaxCalSam = 0
                _SumPrice = 0
                _LineNetAmt = 0
                _ScanQty = 0
                _IncentiveQty = 0
                _CountEmpIncentive = 0

                _Qry = " EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..SP_GET_SCANOUTLINE_CALCULATE_INCENTIVE " & Integer.Parse(Val(R!FNHSysUnitSectId.ToString)) & ",'" & _CalDate & "'"
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
                    _Qry &= vbCrLf & " Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
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

                Dim _FTotalAmtH01 As Double = 0
                Dim _FTotalAmtH02 As Double = 0
                Dim _FTotalAmtH03 As Double = 0
                Dim _FTotalAmtH04 As Double = 0
                Dim _FTotalAmtH05 As Double = 0
                Dim _FTotalAmtH06 As Double = 0
                Dim _FTotalAmtH07 As Double = 0
                Dim _FTotalAmtH08 As Double = 0
                Dim _FTotalAmtH09 As Double = 0
                Dim _FTotalAmtH10 As Double = 0
                Dim _FTotalAmtH11 As Double = 0
                Dim _FTotalAmtH12 As Double = 0
                Dim _FTotalAmtH13 As Double = 0

                Dim _NetPrice As Double = 0
                Dim _dtsam As New DataTable

                _dtsam.Columns.Add("FNSam", GetType(Double))
                _dtsam.Columns.Add("FNQuantity", GetType(Integer))
                _dtsam.Columns.Add("FNTotalSam", GetType(Double))

                _Qry = "EXEC  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.SP_GET_SCANOUTLINE_EDIT_SCANQUANTITY " & Integer.Parse(Val(R!FNHSysUnitSectId.ToString)) & ",'" & _CalDate & "','" & _CalDate & "','TH'"
                _dtsewstyle = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

                Dim _MaxOT As Integer = 0
                Dim _QtyOTMo As Integer = 0
                For Each Rmd As DataRow In _dtsewstyle.Rows

                    _MaxOT = 0
                    _CalDate = HI.UL.ULDate.ConvertEnDB(Rmd!FDScanDateOrg.ToString)

                    For Each Rx As DataRow In _dtemptime.Select("FNWorkingOTMin>0", "FNWorkingOTMin DESC ")
                        _MaxOT = +Integer.Parse(Val(Rx!FNWorkingOTMin.ToString))
                        Exit For
                    Next

                    Select Case True
                        Case (_MaxOT = 0)

                            Rmd!H09 = 0
                            Rmd!H10 = 0
                            Rmd!H11 = 0
                            Rmd!H12 = 0
                            Rmd!H13 = 0

                        Case _MaxOT > 0 And _MaxOT <= 60

                            Rmd!H10 = 0
                            Rmd!H11 = 0
                            Rmd!H12 = 0
                            Rmd!H13 = 0

                        Case _MaxOT > 60 And _MaxOT <= 120

                            Rmd!H11 = 0
                            Rmd!H12 = 0
                            Rmd!H13 = 0

                        Case _MaxOT > 120 And _MaxOT <= 180


                            Rmd!H12 = 0
                            Rmd!H13 = 0

                        Case _MaxOT > 180 And _MaxOT <= 240


                            Rmd!H13 = 0

                        Case _MaxOT > 240

                    End Select

                Next

                For Each Rmd As DataRow In _dtsewstyle.Select("FNStateSewPack<>2")

                    If _FTStateInsuranceInH1 = "1" Then
                        Rmd!H01 = 0
                    End If

                    If _FTStateInsuranceInH2 = "1" Then
                        Rmd!H02 = 0
                    End If

                    If _FTStateInsuranceInH3 = "1" Then
                        Rmd!H03 = 0
                    End If

                    If _FTStateInsuranceInH4 = "1" Then
                        Rmd!H04 = 0
                    End If

                    If _FTStateInsuranceInH5 = "1" Then
                        Rmd!H05 = 0
                    End If

                    If _FTStateInsuranceInH6 = "1" Then
                        Rmd!H06 = 0
                    End If

                    If _FTStateInsuranceInH7 = "1" Then
                        Rmd!H07 = 0
                    End If

                    If _FTStateInsuranceInH8 = "1" Then
                        Rmd!H08 = 0
                    End If

                    If _FTStateInsuranceInH9 = "1" Then
                        Rmd!H09 = 0
                    End If

                    If _FTStateInsuranceInH10 = "1" Then
                        Rmd!H10 = 0
                    End If

                    If _FTStateInsuranceInH11 = "1" Then
                        Rmd!H11 = 0
                    End If

                    If _FTStateInsuranceInH12 = "1" Then
                        Rmd!H12 = 0
                    End If

                    If _FTStateInsuranceInH13 = "1" Then
                        Rmd!H13 = 0
                    End If

                Next


                For Each Rmd As DataRow In _dtsewstyle.Rows
                    Rmd!Total = Val(Rmd!H01) + Val(Rmd!H02) + Val(Rmd!H03) + Val(Rmd!H04) + Val(Rmd!H05) + Val(Rmd!H06) + Val(Rmd!H07) + Val(Rmd!H08) + Val(Rmd!H09) + Val(Rmd!H10) + Val(Rmd!H11) + Val(Rmd!H12) + Val(Rmd!H13)
                Next


                For Each Rxt As DataRow In _dtsewstyle.Select("FNStateSewPack<>2")

                    If Double.Parse(Val(Rxt!FNStateSewPack.ToString)) = 0 Then
                        _NetPrice = Double.Parse(Format(Double.Parse(Val(Rxt!FNPrice.ToString)) * Double.Parse(Val(Rxt!FNMultiple.ToString)), "0.0000"))
                    Else
                        _NetPrice = Double.Parse(Format((Double.Parse(Val(Rxt!FNPrice.ToString)) - Double.Parse(Val(Rxt!FNDisPrice.ToString))) * Double.Parse(Val(Rxt!FNMultiple.ToString)), "0.0000"))
                    End If

                    If _dtsam.Select("FNSam = " & Double.Parse(Val(Rxt!FNSam.ToString)) & "").Length > 0 Then

                        For Each Rc2 As DataRow In _dtsam.Select("FNSam = " & Double.Parse(Val(Rxt!FNSam.ToString)) & "")

                            Rc2!FNQuantity = Double.Parse(Val(Rc2!FNQuantity)) + Integer.Parse(Val(Rxt!Total.ToString))
                            Exit For

                        Next

                    Else

                        _dtsam.Rows.Add(Double.Parse(Val(Rxt!FNSam.ToString)), Integer.Parse(Val(Rxt!Total.ToString)), 0.00)

                    End If

                    _Qry = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTIncentive_Style_Hour ("
                    _Qry &= vbCrLf & "  FTInsUser, FDInsDate, FTInsTime, FTCalDate, FNHSysUnitSectId"
                    _Qry &= vbCrLf & ",  FNHSysStyleId, FTOrderNo, FTSubOrderNo, FNStateSewPack, FNSam, FNPricePerSam, FNPriceMultiple,  "
                    _Qry &= vbCrLf & "  FNDisPrice,FNNetPrice, FNHour01Qty, FNHour02Qty, FNHour03Qty, FNHour04Qty, FNHour05Qty, FNHour06Qty, FNHour07Qty"
                    _Qry &= vbCrLf & " , FNHour08Qty, FNHour09Qty, FNHour10Qty, FNHour11Qty, FNHour12Qty, FNHour13Qty, FNTotalQty, "
                    _Qry &= vbCrLf & "  FNHour01Amt, FNHour02Amt, FNHour03Amt, FNHour04Amt, FNHour05Amt, FNHour06Amt, FNHour07Amt"
                    _Qry &= vbCrLf & " , FNHour08Amt, FNHour09Amt, FNHour10Amt, FNHour11Amt, FNHour12Amt, FNHour13Amt, FNNetAmt)"
                    _Qry &= vbCrLf & " Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & ""
                    _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & ""
                    _Qry &= vbCrLf & " ,'" & _CalDate & "'   "
                    _Qry &= vbCrLf & " ," & Integer.Parse(Val(R!FNHSysUnitSectId.ToString)) & ""
                    _Qry &= vbCrLf & " ," & Integer.Parse(Val(Rxt!FNHSysStyleId.ToString)) & ""
                    _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(Rxt!FTOrderNo.ToString) & "'"
                    _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(Rxt!FTSubOrderNo.ToString) & "'"
                    _Qry &= vbCrLf & " ," & Integer.Parse(Val(Rxt!FNStateSewPack.ToString)) & ""
                    _Qry &= vbCrLf & " ," & Double.Parse(Val(Rxt!FNSam.ToString)) & ""
                    _Qry &= vbCrLf & " ," & Double.Parse(Val(Rxt!FNPrice.ToString)) & ""
                    _Qry &= vbCrLf & " ," & Double.Parse(Val(Rxt!FNMultiple.ToString)) & ""
                    _Qry &= vbCrLf & " ," & Double.Parse(Val(Rxt!FNDisPrice.ToString)) & ""
                    _Qry &= vbCrLf & " ," & _NetPrice & ""
                    _Qry &= vbCrLf & " ," & Integer.Parse(Val(Rxt!H01.ToString)) & ""
                    _Qry &= vbCrLf & " ," & Integer.Parse(Val(Rxt!H02.ToString)) & ""
                    _Qry &= vbCrLf & " ," & Integer.Parse(Val(Rxt!H03.ToString)) & ""
                    _Qry &= vbCrLf & " ," & Integer.Parse(Val(Rxt!H04.ToString)) & ""
                    _Qry &= vbCrLf & " ," & Integer.Parse(Val(Rxt!H05.ToString)) & ""
                    _Qry &= vbCrLf & " ," & Integer.Parse(Val(Rxt!H06.ToString)) & ""
                    _Qry &= vbCrLf & " ," & Integer.Parse(Val(Rxt!H07.ToString)) & ""
                    _Qry &= vbCrLf & " ," & Integer.Parse(Val(Rxt!H08.ToString)) & ""
                    _Qry &= vbCrLf & " ," & Integer.Parse(Val(Rxt!H09.ToString)) & ""
                    _Qry &= vbCrLf & " ," & Integer.Parse(Val(Rxt!H10.ToString)) & ""
                    _Qry &= vbCrLf & " ," & Integer.Parse(Val(Rxt!H11.ToString)) & ""
                    _Qry &= vbCrLf & " ," & Integer.Parse(Val(Rxt!H12.ToString)) & ""
                    _Qry &= vbCrLf & " ," & Integer.Parse(Val(Rxt!H13.ToString)) & ""
                    _Qry &= vbCrLf & " ," & Integer.Parse(Val(Rxt!Total.ToString)) & ""

                    _Qry &= vbCrLf & " ," & Double.Parse(Format(Integer.Parse(Val(Rxt!H01.ToString)) * _NetPrice, "0.00")) & ""
                    _Qry &= vbCrLf & " ," & Double.Parse(Format(Integer.Parse(Val(Rxt!H02.ToString)) * _NetPrice, "0.00")) & ""
                    _Qry &= vbCrLf & " ," & Double.Parse(Format(Integer.Parse(Val(Rxt!H03.ToString)) * _NetPrice, "0.00")) & ""
                    _Qry &= vbCrLf & " ," & Double.Parse(Format(Integer.Parse(Val(Rxt!H04.ToString)) * _NetPrice, "0.00")) & ""
                    _Qry &= vbCrLf & " ," & Double.Parse(Format(Integer.Parse(Val(Rxt!H05.ToString)) * _NetPrice, "0.00")) & ""
                    _Qry &= vbCrLf & " ," & Double.Parse(Format(Integer.Parse(Val(Rxt!H06.ToString)) * _NetPrice, "0.00")) & ""
                    _Qry &= vbCrLf & " ," & Double.Parse(Format(Integer.Parse(Val(Rxt!H07.ToString)) * _NetPrice, "0.00")) & ""
                    _Qry &= vbCrLf & " ," & Double.Parse(Format(Integer.Parse(Val(Rxt!H08.ToString)) * _NetPrice, "0.00")) & ""
                    _Qry &= vbCrLf & " ," & Double.Parse(Format(Integer.Parse(Val(Rxt!H09.ToString)) * _NetPrice, "0.00")) & ""
                    _Qry &= vbCrLf & " ," & Double.Parse(Format(Integer.Parse(Val(Rxt!H10.ToString)) * _NetPrice, "0.00")) & ""
                    _Qry &= vbCrLf & " ," & Double.Parse(Format(Integer.Parse(Val(Rxt!H11.ToString)) * _NetPrice, "0.00")) & ""
                    _Qry &= vbCrLf & " ," & Double.Parse(Format(Integer.Parse(Val(Rxt!H12.ToString)) * _NetPrice, "0.00")) & ""
                    _Qry &= vbCrLf & " ," & Double.Parse(Format(Integer.Parse(Val(Rxt!H13.ToString)) * _NetPrice, "0.00")) & ""
                    _Qry &= vbCrLf & " ," & Double.Parse(Format(Integer.Parse(Val(Rxt!Total.ToString)) * _NetPrice, "0.00")) & ""

                    HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

                    _FTotalAmtH01 = _FTotalAmtH01 + Double.Parse(Format(Integer.Parse(Val(Rxt!H01.ToString)) * _NetPrice, "0.00"))
                    _FTotalAmtH02 = _FTotalAmtH02 + Double.Parse(Format(Integer.Parse(Val(Rxt!H02.ToString)) * _NetPrice, "0.00"))
                    _FTotalAmtH03 = _FTotalAmtH03 + Double.Parse(Format(Integer.Parse(Val(Rxt!H03.ToString)) * _NetPrice, "0.00"))
                    _FTotalAmtH04 = _FTotalAmtH04 + Double.Parse(Format(Integer.Parse(Val(Rxt!H04.ToString)) * _NetPrice, "0.00"))
                    _FTotalAmtH05 = _FTotalAmtH05 + Double.Parse(Format(Integer.Parse(Val(Rxt!H05.ToString)) * _NetPrice, "0.00"))
                    _FTotalAmtH06 = _FTotalAmtH06 + Double.Parse(Format(Integer.Parse(Val(Rxt!H06.ToString)) * _NetPrice, "0.00"))
                    _FTotalAmtH07 = _FTotalAmtH07 + Double.Parse(Format(Integer.Parse(Val(Rxt!H07.ToString)) * _NetPrice, "0.00"))
                    _FTotalAmtH08 = _FTotalAmtH08 + Double.Parse(Format(Integer.Parse(Val(Rxt!H08.ToString)) * _NetPrice, "0.00"))
                    _FTotalAmtH09 = _FTotalAmtH09 + Double.Parse(Format(Integer.Parse(Val(Rxt!H09.ToString)) * _NetPrice, "0.00"))
                    _FTotalAmtH10 = _FTotalAmtH10 + Double.Parse(Format(Integer.Parse(Val(Rxt!H10.ToString)) * _NetPrice, "0.00"))
                    _FTotalAmtH11 = _FTotalAmtH11 + Double.Parse(Format(Integer.Parse(Val(Rxt!H11.ToString)) * _NetPrice, "0.00"))
                    _FTotalAmtH12 = _FTotalAmtH12 + Double.Parse(Format(Integer.Parse(Val(Rxt!H12.ToString)) * _NetPrice, "0.00"))
                    _FTotalAmtH13 = _FTotalAmtH13 + Double.Parse(Format(Integer.Parse(Val(Rxt!H13.ToString)) * _NetPrice, "0.00"))

                Next


                For Each Rxt As DataRow In _dtsewstyle.Select("FNStateSewPack=2")

                    _NetPrice = Double.Parse(Val(Rxt!FNDisPrice.ToString))

                    _Qry = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTIncentive_Style_Hour ("
                    _Qry &= vbCrLf & "  FTInsUser, FDInsDate, FTInsTime, FTCalDate, FNHSysUnitSectId"
                    _Qry &= vbCrLf & ",  FNHSysStyleId, FTOrderNo, FTSubOrderNo, FNStateSewPack, FNSam, FNPricePerSam, FNPriceMultiple,  "
                    _Qry &= vbCrLf & "  FNDisPrice,FNNetPrice, FNHour01Qty, FNHour02Qty, FNHour03Qty, FNHour04Qty, FNHour05Qty, FNHour06Qty, FNHour07Qty"
                    _Qry &= vbCrLf & " , FNHour08Qty, FNHour09Qty, FNHour10Qty, FNHour11Qty, FNHour12Qty, FNHour13Qty, FNTotalQty, "
                    _Qry &= vbCrLf & "  FNHour01Amt, FNHour02Amt, FNHour03Amt, FNHour04Amt, FNHour05Amt, FNHour06Amt, FNHour07Amt"
                    _Qry &= vbCrLf & " , FNHour08Amt, FNHour09Amt, FNHour10Amt, FNHour11Amt, FNHour12Amt, FNHour13Amt, FNNetAmt)"
                    _Qry &= vbCrLf & " Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & ""
                    _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & ""
                    _Qry &= vbCrLf & " ,'" & _CalDate & "'   "
                    _Qry &= vbCrLf & " ," & Integer.Parse(Val(R!FNHSysUnitSectId.ToString)) & ""
                    _Qry &= vbCrLf & " ," & Integer.Parse(Val(Rxt!FNHSysStyleId.ToString)) & ""
                    _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(Rxt!FTOrderNo.ToString) & "'"
                    _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(Rxt!FTSubOrderNo.ToString) & "'"
                    _Qry &= vbCrLf & " ," & Integer.Parse(Val(Rxt!FNStateSewPack.ToString)) & ""
                    _Qry &= vbCrLf & " ," & Double.Parse(Val(Rxt!FNSam.ToString)) & ""
                    _Qry &= vbCrLf & " ," & Double.Parse(Val(Rxt!FNPrice.ToString)) & ""
                    _Qry &= vbCrLf & " ," & Double.Parse(Val(Rxt!FNMultiple.ToString)) & ""
                    _Qry &= vbCrLf & " ," & Double.Parse(Val(Rxt!FNDisPrice.ToString)) & ""
                    _Qry &= vbCrLf & " ," & _NetPrice & ""
                    _Qry &= vbCrLf & " ," & Integer.Parse(Val(Rxt!H01.ToString)) & ""
                    _Qry &= vbCrLf & " ," & Integer.Parse(Val(Rxt!H02.ToString)) & ""
                    _Qry &= vbCrLf & " ," & Integer.Parse(Val(Rxt!H03.ToString)) & ""
                    _Qry &= vbCrLf & " ," & Integer.Parse(Val(Rxt!H04.ToString)) & ""
                    _Qry &= vbCrLf & " ," & Integer.Parse(Val(Rxt!H05.ToString)) & ""
                    _Qry &= vbCrLf & " ," & Integer.Parse(Val(Rxt!H06.ToString)) & ""
                    _Qry &= vbCrLf & " ," & Integer.Parse(Val(Rxt!H07.ToString)) & ""
                    _Qry &= vbCrLf & " ," & Integer.Parse(Val(Rxt!H08.ToString)) & ""
                    _Qry &= vbCrLf & " ," & Integer.Parse(Val(Rxt!H09.ToString)) & ""
                    _Qry &= vbCrLf & " ," & Integer.Parse(Val(Rxt!H10.ToString)) & ""
                    _Qry &= vbCrLf & " ," & Integer.Parse(Val(Rxt!H11.ToString)) & ""
                    _Qry &= vbCrLf & " ," & Integer.Parse(Val(Rxt!H12.ToString)) & ""
                    _Qry &= vbCrLf & " ," & Integer.Parse(Val(Rxt!H13.ToString)) & ""
                    _Qry &= vbCrLf & " ," & Integer.Parse(Val(Rxt!Total.ToString)) & ""
                    _Qry &= vbCrLf & " ," & Double.Parse(Format(Integer.Parse(Val(Rxt!H01.ToString)) * _NetPrice, "0.00")) & ""
                    _Qry &= vbCrLf & " ," & Double.Parse(Format(Integer.Parse(Val(Rxt!H02.ToString)) * _NetPrice, "0.00")) & ""
                    _Qry &= vbCrLf & " ," & Double.Parse(Format(Integer.Parse(Val(Rxt!H03.ToString)) * _NetPrice, "0.00")) & ""
                    _Qry &= vbCrLf & " ," & Double.Parse(Format(Integer.Parse(Val(Rxt!H04.ToString)) * _NetPrice, "0.00")) & ""
                    _Qry &= vbCrLf & " ," & Double.Parse(Format(Integer.Parse(Val(Rxt!H05.ToString)) * _NetPrice, "0.00")) & ""
                    _Qry &= vbCrLf & " ," & Double.Parse(Format(Integer.Parse(Val(Rxt!H06.ToString)) * _NetPrice, "0.00")) & ""
                    _Qry &= vbCrLf & " ," & Double.Parse(Format(Integer.Parse(Val(Rxt!H07.ToString)) * _NetPrice, "0.00")) & ""
                    _Qry &= vbCrLf & " ," & Double.Parse(Format(Integer.Parse(Val(Rxt!H08.ToString)) * _NetPrice, "0.00")) & ""
                    _Qry &= vbCrLf & " ," & Double.Parse(Format(Integer.Parse(Val(Rxt!H09.ToString)) * _NetPrice, "0.00")) & ""
                    _Qry &= vbCrLf & " ," & Double.Parse(Format(Integer.Parse(Val(Rxt!H10.ToString)) * _NetPrice, "0.00")) & ""
                    _Qry &= vbCrLf & " ," & Double.Parse(Format(Integer.Parse(Val(Rxt!H11.ToString)) * _NetPrice, "0.00")) & ""
                    _Qry &= vbCrLf & " ," & Double.Parse(Format(Integer.Parse(Val(Rxt!H12.ToString)) * _NetPrice, "0.00")) & ""
                    _Qry &= vbCrLf & " ," & Double.Parse(Format(Integer.Parse(Val(Rxt!H13.ToString)) * _NetPrice, "0.00")) & ""
                    _Qry &= vbCrLf & " ," & Double.Parse(Format(Integer.Parse(Val(Rxt!Total.ToString)) * _NetPrice, "0.00")) & ""

                    HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)


                Next

                _dtsewstyle.Dispose()



                _Qry = " EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..SP_GET_SCANOUTLINE_INCENTIVE_PACK_HOUR " & Integer.Parse(Val(R!FNHSysUnitSectId.ToString)) & ",'" & _CalDate & "','" & _CalDate & "'"
                _dtsewstylepack = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

                If _dtsewstylepack.Rows.Count > 0 Then
                    For Each RxP As DataRow In _dtsewstylepack.Rows
                        _PackQty = Val(RxP!FNQuantity.ToString)
                        _PackAmt = Val(RxP!FNAmt.ToString)
                    Next

                End If

                _dtsewstylepack.Dispose()


                For Each Rc2 As DataRow In _dtsam.Rows()
                    Rc2!FNTotalSam = Double.Parse(Val(Rc2!FNQuantity)) * Double.Parse(Val(Rc2!FNSam))
                Next

                _MaxCalSam = 0

                For Each Rc2 As DataRow In _dtsam.Select("FNTotalSam>0", "FNTotalSam")
                    _MaxCalSam = Double.Parse(Val(Rc2!FNSam.ToString()))
                Next

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
                    Else
                        _LineIncentiveAmt = _LineAmt
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

                Dim _FTotalTimeH01 As Integer = 0
                Dim _FTotalTimeH02 As Integer = 0
                Dim _FTotalTimeH03 As Integer = 0
                Dim _FTotalTimeH04 As Integer = 0
                Dim _FTotalTimeH05 As Integer = 0
                Dim _FTotalTimeH06 As Integer = 0
                Dim _FTotalTimeH07 As Integer = 0
                Dim _FTotalTimeH08 As Integer = 0
                Dim _FTotalTimeH09 As Integer = 0
                Dim _FTotalTimeH10 As Integer = 0
                Dim _FTotalTimeH11 As Integer = 0
                Dim _FTotalTimeH12 As Integer = 0
                Dim _FTotalTimeH13 As Integer = 0

                For Each Rt As DataRow In _dtemptime.Rows

                    _FTotalTimeH01 = _FTotalTimeH01 + Integer.Parse(Val(Rt!FNH01.ToString))
                    _FTotalTimeH02 = _FTotalTimeH02 + Integer.Parse(Val(Rt!FNH02.ToString))
                    _FTotalTimeH03 = _FTotalTimeH03 + Integer.Parse(Val(Rt!FNH03.ToString))
                    _FTotalTimeH04 = _FTotalTimeH04 + Integer.Parse(Val(Rt!FNH04.ToString))
                    _FTotalTimeH05 = _FTotalTimeH05 + Integer.Parse(Val(Rt!FNH05.ToString))
                    _FTotalTimeH06 = _FTotalTimeH06 + Integer.Parse(Val(Rt!FNH06.ToString))
                    _FTotalTimeH07 = _FTotalTimeH07 + Integer.Parse(Val(Rt!FNH07.ToString))
                    _FTotalTimeH08 = _FTotalTimeH08 + Integer.Parse(Val(Rt!FNH08.ToString))
                    _FTotalTimeH09 = _FTotalTimeH09 + Integer.Parse(Val(Rt!FNH09.ToString))
                    _FTotalTimeH10 = _FTotalTimeH10 + Integer.Parse(Val(Rt!FNH10.ToString))
                    _FTotalTimeH11 = _FTotalTimeH11 + Integer.Parse(Val(Rt!FNH11.ToString))
                    _FTotalTimeH12 = _FTotalTimeH12 + Integer.Parse(Val(Rt!FNH12.ToString))
                    _FTotalTimeH13 = _FTotalTimeH13 + Integer.Parse(Val(Rt!FNH13.ToString))

                Next

                For Each Rt As DataRow In _dtemptime.Rows


                    _CountEmp = _CountEmp + 1
                    _TotalTime = _TotalTime + Integer.Parse(Val(Rt!FNWorkingMin.ToString))
                    _TotalTimeOT = _TotalTimeOT + Integer.Parse(Val(Rt!FNWorkingOTMin.ToString))

                    _TotalTimeHR = Integer.Parse(Val(Rt!FNTimeMin.ToString))
                    _TotalTimeOTHR = Integer.Parse(Val(Rt!FNOT1Min.ToString))

                    _TotalTimeHR = 0

                    _TotalTimeHR = _TotalTimeHR + Integer.Parse(Val(Rt!FNH01.ToString))
                    _TotalTimeHR = _TotalTimeHR + Integer.Parse(Val(Rt!FNH02.ToString))
                    _TotalTimeHR = _TotalTimeHR + Integer.Parse(Val(Rt!FNH03.ToString))
                    _TotalTimeHR = _TotalTimeHR + Integer.Parse(Val(Rt!FNH04.ToString))
                    _TotalTimeHR = _TotalTimeHR + Integer.Parse(Val(Rt!FNH05.ToString))
                    _TotalTimeHR = _TotalTimeHR + Integer.Parse(Val(Rt!FNH06.ToString))
                    _TotalTimeHR = _TotalTimeHR + Integer.Parse(Val(Rt!FNH07.ToString))
                    _TotalTimeHR = _TotalTimeHR + Integer.Parse(Val(Rt!FNH08.ToString))

                    _TotalTimeOTHR = 0
                    _TotalTimeOTHR = _TotalTimeOTHR + Integer.Parse(Val(Rt!FNH09.ToString))
                    _TotalTimeOTHR = _TotalTimeOTHR + Integer.Parse(Val(Rt!FNH10.ToString))
                    _TotalTimeOTHR = _TotalTimeOTHR + Integer.Parse(Val(Rt!FNH11.ToString))
                    _TotalTimeOTHR = _TotalTimeOTHR + Integer.Parse(Val(Rt!FNH12.ToString))
                    _TotalTimeOTHR = _TotalTimeOTHR + Integer.Parse(Val(Rt!FNH13.ToString))


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
                    _Qry &= vbCrLf & " FNAmtHour10Insurance, FNAmtHour11Insurance, FNAmtHour12Insurance, FNAmtHour13Insurance"
                    _Qry &= vbCrLf & " ,FNAmtHour01HR, FNAmtHour02HR, FNAmtHour03HR, FNAmtHour04HR, FNAmtHour05HR, FNAmtHour06HR, FNAmtHour07HR, FNAmtHour08HR, FNAmtHour09HR, FNAmtHour10HR, FNAmtHour11HR, FNAmtHour12HR,"
                    _Qry &= vbCrLf & " FNAmtHour13HR,FTStateRelease,FNAmtFixedIncentive"
                    _Qry &= vbCrLf & " ,FNTotalTimHRFull,FNTotalTimeOTHRFull"
                    _Qry &= vbCrLf & ")"
                    _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
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
                    _Qry &= vbCrLf & " ,0,0,0,0,0,0,0,0,0,0,0,0,0"
                    _Qry &= vbCrLf & " ,'" & Rt!FTStateRelease.ToString & "'"
                    _Qry &= vbCrLf & " ,CASE WHEN " & (_TotalTimeHR + _TotalTimeOTHR) & " > 0 THEN " & Double.Parse(Val(Rt!FNAmtFixedIncentive.ToString)) & " ELSE 0.00 END"
                    _Qry &= vbCrLf & " ," & _FullTotalTimeHR & ""
                    _Qry &= vbCrLf & " ," & _FullTotalTimeOTHR & ""

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
                _Qry &= vbCrLf & "  , FNHour13QtySystem, FNTotalQtySystem,FNHSysIncenFormulaId,FNTotalPackQty,FNTotalPackAmt"

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
                _Qry &= vbCrLf & " ," & _IncentiveQty & ""
                _Qry &= vbCrLf & " ," & _LineIncentiveAmt & ""
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
                _Qry &= vbCrLf & " ," & _FNHSysIncenFormulaId & ""
                _Qry &= vbCrLf & " ," & _PackQty & ""
                _Qry &= vbCrLf & " ," & _PackAmt & ""

                HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

                _DeductAmt = 0

                If _LineIncentiveAmt + _PackAmt > 0 Then

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
                        ' _FNSam = Double.Parse(Format(_SumSam / _TotalCountStyle, "0.0000"))
                    Else
                        _PriceCost = _SumPrice
                        ' _FNSam = _SumSam
                    End If

                    ' _PriceCost = 1
                    _FNSam = _MaxCalSam

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


                    '--------คำนวณคนที่มาทพงานไม่ครบ---------------
                    Dim _TotalTimeMin As Integer = 0
                    Dim _TotalEmpAmt As Double = 0
                    Dim _EmpAmt As Double = 0
                    Dim _TotalEmpCal As Integer = 0

                    _TotalTime = 0
                    _TotalTimeOT = 0

                    'For Each Rt As DataRow In _dtemptime.Rows
                    For Each Rt As DataRow In _dtemptime.Select("FTStateCal<>'1'  AND FNTotalWorkingMin>0 ")

                        _TotalTimeMin = _TotalTimeMin + Integer.Parse(Val(Rt!FNTotalWorkingMin.ToString))
                        _TotalTime = _TotalTime + Integer.Parse(Val(Rt!FNWorkingMin.ToString))
                        _TotalTimeOT = _TotalTimeOT + Integer.Parse(Val(Rt!FNWorkingOTMin.ToString))

                    Next

                    Select Case Val(R!FTIncentiveTypeIdx.ToString)
                        Case 4
                            For Each Rt As DataRow In _dtemptime.Select("FTStateCal='1'  AND FNTotalWorkingMin>0 AND FTStateRelease<>'1' AND FTStateTrain='1' ", "FNTotalWorkingMin")
                                _TotalTimeMin = _TotalTimeMin + Integer.Parse(Val(Rt!FNTotalWorkingMin.ToString))
                            Next

                    End Select

                    _CountEmpIncentive = _dtemptime.Select("FTStateCal<>'1'  AND FNTotalWorkingMin>0").Length

                    _LineIncentiveAmt = _LineIncentiveAmt - _DeductAmt
                    _LineIncentiveAmt = _LineIncentiveAmt + _PackAmt

                    If _CountEmpIncentive > 0 Then
                        _EmpIncentiveAmt = Double.Parse(Format(_LineIncentiveAmt / _CountEmpIncentive, "0.00"))
                    Else
                        _EmpIncentiveAmt = 0
                    End If


                    Dim GAmountLine As Double = 0
                    'FTStateTimeMax

                    For Each Rt As DataRow In _dtemptime.Select("FTStateCal<>'1'  AND FNTotalWorkingMin>0 ", "FNTotalWorkingMin")
                        _TotalEmpCal = _TotalEmpCal + 1

                        _Salary = Double.Parse(Val(Rt!FNSalary.ToString))
                        _SalaryPerH = Double.Parse(Format(_Salary / 8, "0.000"))
                        _SalaryPerOT = Double.Parse(Format(_SalaryPerH * 1.5, "0.0000"))

                        If _FixHour > 0 Then
                        End If

                        'If _TotalEmpCal = _CountEmpIncentive Then
                        '    _EmpIncentiveAmt = _LineIncentiveAmt - _TotalEmpAmt
                        'Else
                        _EmpIncentiveAmt = CDbl(Format((_LineIncentiveAmt * (Integer.Parse(Val(Rt!FNTotalWorkingMin.ToString)))) / _TotalTimeMin, "0.00"))
                        'End If

                        _TotalEmpAmt = _TotalEmpAmt + _EmpIncentiveAmt

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

                        GAmountLine = GAmountLine + _EmpIncentiveAmt
                    Next


                    If _LineIncentiveAmt > GAmountLine Then

                        Dim MaxMinute As Integer = 0
                        Dim SPareAmont As Decimal = _LineIncentiveAmt - GAmountLine


                        For Each Rt As DataRow In _dtemptime.Select("FTStateCal<>'1'  AND FNTotalWorkingMin>0 ", "FNTotalWorkingMin ")
                            MaxMinute = Integer.Parse(Val(Rt!FNTotalWorkingMin.ToString))


                        Next

                        If MaxMinute > 0 Then
                            _TotalEmpCal = 0
                            For Each Rt As DataRow In _dtemptime.Select("FTStateCal<>'1'  AND FNTotalWorkingMin>=" & MaxMinute & " ", "FNTotalWorkingMin ")
                                _TotalEmpCal = _TotalEmpCal + 1
                            Next
                            Dim EmpSPareAmont As Decimal = CDbl(Format(SPareAmont / _TotalEmpCal, "0.00"))


                            If (EmpSPareAmont * _TotalEmpCal) < SPareAmont Then
                                EmpSPareAmont = EmpSPareAmont + 0.01
                            End If


                            For Each Rt As DataRow In _dtemptime.Select("FTStateCal<>'1'  AND FNTotalWorkingMin>=" & MaxMinute & " ", "FNTotalWorkingMin ")

                                _Qry = " UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTIncentive_Emp "
                                _Qry &= vbCrLf & "  SET  FNAmtOldIncentive=FNAmtOldIncentive+" & EmpSPareAmont & ""
                                _Qry &= vbCrLf & "  , FNNetAmtOldIncentive=FNNetAmtOldIncentive+" & EmpSPareAmont & ""
                                _Qry &= vbCrLf & "   WHERE FNHSysUnitSectId=" & Integer.Parse(Val(R!FNHSysUnitSectId.ToString)) & ""
                                _Qry &= vbCrLf & "       AND FNHSysEmpID=" & Integer.Parse(Val(Rt!FNHSysEmpID.ToString)) & ""
                                _Qry &= vbCrLf & "       AND FTCalDate='" & _CalDate & "'"

                                HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)
                            Next

                        End If



                    End If

                    Dim _FilterTime As String = ""
                    Dim _HourIncentiveAmount As Double = 0

                    For I As Integer = 1 To 13
                        Select Case I
                            Case 1
                                _FilterTime = "FNH01"
                                _HourIncentiveAmount = _FTotalAmtH01
                            Case 2
                                _FilterTime = "FNH02"
                                _HourIncentiveAmount = _FTotalAmtH02
                            Case 3
                                _FilterTime = "FNH03"
                                _HourIncentiveAmount = _FTotalAmtH03
                            Case 4
                                _FilterTime = "FNH04"
                                _HourIncentiveAmount = _FTotalAmtH04
                            Case 5
                                _FilterTime = "FNH05"
                                _HourIncentiveAmount = _FTotalAmtH05
                            Case 6
                                _FilterTime = "FNH06"
                                _HourIncentiveAmount = _FTotalAmtH06
                            Case 7
                                _FilterTime = "FNH07"
                                _HourIncentiveAmount = _FTotalAmtH07
                            Case 8
                                _FilterTime = "FNH08"
                                _HourIncentiveAmount = _FTotalAmtH08
                            Case 9
                                _FilterTime = "FNH09"
                                _HourIncentiveAmount = _FTotalAmtH09
                            Case 10
                                _FilterTime = "FNH10"
                                _HourIncentiveAmount = _FTotalAmtH10
                            Case 11
                                _FilterTime = "FNH11"
                                _HourIncentiveAmount = _FTotalAmtH11
                            Case 12
                                _FilterTime = "FNH12"
                                _HourIncentiveAmount = _FTotalAmtH12
                            Case 13
                                _FilterTime = "FNH13"
                                _HourIncentiveAmount = _FTotalAmtH13
                        End Select

                        _TotalTimeMin = 0
                        For Each Rt As DataRow In _dtemptime.Select("FTStateCal<>'1'  AND " & _FilterTime & ">0 ")

                            _TotalTimeMin = _TotalTimeMin + Integer.Parse(Val(Rt.Item(_FilterTime).ToString))

                        Next

                        _CountEmpIncentive = _dtemptime.Select("FTStateCal<>'1'  AND " & _FilterTime & ">0").Length
                        _EmpIncentiveAmt = 0

                        If _CountEmpIncentive > 0 Then
                            _EmpIncentiveAmt = Double.Parse(Format(_HourIncentiveAmount / _CountEmpIncentive, "0.00"))
                        Else
                            _EmpIncentiveAmt = 0
                        End If

                        _TotalEmpCal = 0
                        _TotalEmpAmt = 0

                        For Each Rt As DataRow In _dtemptime.Select("FTStateCal<>'1'  AND " & _FilterTime & ">0 ", _FilterTime)
                            _TotalEmpCal = _TotalEmpCal + 1

                            _Salary = Double.Parse(Val(Rt!FNSalary.ToString))
                            _SalaryPerH = Double.Parse(Format(_Salary / 8, "0.000"))
                            _SalaryPerOT = Double.Parse(Format(_SalaryPerH * 1.5, "0.0000"))

                            If _FixHour > 0 Then
                            End If

                            If Integer.Parse(Val(Rt!FNHSysEmpID.ToString)) = 1403020926 Then
                                Beep()
                            End If

                            'If _TotalEmpCal = _CountEmpIncentive Then
                            '    _EmpIncentiveAmt = _HourIncentiveAmount - _TotalEmpAmt
                            'Else
                            '    _EmpIncentiveAmt = CDbl(Format((_HourIncentiveAmount * (Integer.Parse(Val(Rt.Item(_FilterTime).ToString)))) / _TotalTimeMin, "0.00"))
                            'End If
                            _EmpIncentiveAmt = CDbl(Format((_HourIncentiveAmount * (Integer.Parse(Val(Rt.Item(_FilterTime).ToString)))) / _TotalTimeMin, "0.00"))
                            _TotalEmpAmt = _TotalEmpAmt + _EmpIncentiveAmt

                            _FNNetAmt = _EmpIncentiveAmt

                            _Qry = " UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTIncentive_Emp "
                            _Qry &= vbCrLf & "  SET "

                            Select Case I
                                Case 1
                                    If _FTStateInsuranceInH1 <> "1" Then
                                        _Qry &= vbCrLf & "  FNAmtHour01=" & _FNNetAmt & ""
                                        _Qry &= vbCrLf & "  ,FNAmtHour01Insurance=0"
                                    Else
                                        _Qry &= vbCrLf & "  FNAmtHour01=0"
                                        _Qry &= vbCrLf & "  ,FNAmtHour01Insurance=" & _SalaryPerH & ""
                                    End If
                                    _Qry &= vbCrLf & "  ,FNAmtHour01HR=" & CDbl(Format((_SalaryPerH * (Integer.Parse(Val(Rt.Item(_FilterTime).ToString)))) / 60, "0.00")) & ""
                                Case 2
                                    If _FTStateInsuranceInH2 <> "1" Then
                                        _Qry &= vbCrLf & "  FNAmtHour02=" & _FNNetAmt & ""
                                        _Qry &= vbCrLf & "  ,FNAmtHour02Insurance=0"
                                    Else
                                        _Qry &= vbCrLf & "  FNAmtHour02=0"
                                        _Qry &= vbCrLf & "  ,FNAmtHour02Insurance=" & _SalaryPerH & ""
                                    End If
                                    _Qry &= vbCrLf & "  ,FNAmtHour02HR=" & CDbl(Format((_SalaryPerH * (Integer.Parse(Val(Rt.Item(_FilterTime).ToString)))) / 60, "0.00")) & ""
                                Case 3

                                    If _FTStateInsuranceInH3 <> "1" Then
                                        _Qry &= vbCrLf & "  FNAmtHour03=" & _FNNetAmt & ""
                                        _Qry &= vbCrLf & "  ,FNAmtHour03Insurance=0"
                                    Else
                                        _Qry &= vbCrLf & "  FNAmtHour03=0"
                                        _Qry &= vbCrLf & "  ,FNAmtHour03Insurance=" & _SalaryPerH & ""
                                    End If
                                    _Qry &= vbCrLf & "  ,FNAmtHour03HR=" & CDbl(Format((_SalaryPerH * (Integer.Parse(Val(Rt.Item(_FilterTime).ToString)))) / 60, "0.00")) & ""
                                Case 4

                                    If _FTStateInsuranceInH4 <> "1" Then
                                        _Qry &= vbCrLf & "  FNAmtHour04=" & _FNNetAmt & ""
                                        _Qry &= vbCrLf & "  ,FNAmtHour04Insurance=0"
                                    Else
                                        _Qry &= vbCrLf & "  FNAmtHour04=0"
                                        _Qry &= vbCrLf & "  ,FNAmtHour04Insurance=" & _SalaryPerH & ""
                                    End If
                                    _Qry &= vbCrLf & "  ,FNAmtHour04HR=" & CDbl(Format((_SalaryPerH * (Integer.Parse(Val(Rt.Item(_FilterTime).ToString)))) / 60, "0.00")) & ""
                                Case 5

                                    If _FTStateInsuranceInH5 <> "1" Then
                                        _Qry &= vbCrLf & "  FNAmtHour05=" & _FNNetAmt & ""
                                        _Qry &= vbCrLf & "  ,FNAmtHour05Insurance=0"
                                    Else
                                        _Qry &= vbCrLf & "  FNAmtHour05=0"
                                        _Qry &= vbCrLf & "  ,FNAmtHour05Insurance=" & _SalaryPerH & ""
                                    End If
                                    _Qry &= vbCrLf & "  ,FNAmtHour05HR=" & CDbl(Format((_SalaryPerH * (Integer.Parse(Val(Rt.Item(_FilterTime).ToString)))) / 60, "0.00")) & ""
                                Case 6

                                    If _FTStateInsuranceInH6 <> "1" Then
                                        _Qry &= vbCrLf & "  FNAmtHour06=" & _FNNetAmt & ""
                                        _Qry &= vbCrLf & "  ,FNAmtHour06Insurance=0"
                                    Else
                                        _Qry &= vbCrLf & "  FNAmtHour06=0"
                                        _Qry &= vbCrLf & "  ,FNAmtHour06Insurance=" & _SalaryPerH & ""
                                    End If
                                    _Qry &= vbCrLf & "  ,FNAmtHour06HR=" & CDbl(Format((_SalaryPerH * (Integer.Parse(Val(Rt.Item(_FilterTime).ToString)))) / 60, "0.00")) & ""
                                Case 7

                                    If _FTStateInsuranceInH7 <> "1" Then
                                        _Qry &= vbCrLf & "  FNAmtHour07=" & _FNNetAmt & ""
                                        _Qry &= vbCrLf & "  ,FNAmtHour07Insurance=0"
                                    Else
                                        _Qry &= vbCrLf & "  FNAmtHour07=0"
                                        _Qry &= vbCrLf & "  ,FNAmtHour07Insurance=" & _SalaryPerH & ""
                                    End If
                                    _Qry &= vbCrLf & "  ,FNAmtHour07HR=" & CDbl(Format((_SalaryPerH * (Integer.Parse(Val(Rt.Item(_FilterTime).ToString)))) / 60, "0.00")) & ""
                                Case 8

                                    If _FTStateInsuranceInH8 <> "1" Then
                                        _Qry &= vbCrLf & "  FNAmtHour08=" & _FNNetAmt & ""
                                        _Qry &= vbCrLf & "  ,FNAmtHour08Insurance=0"
                                    Else
                                        _Qry &= vbCrLf & "  FNAmtHour08=0"
                                        _Qry &= vbCrLf & "  ,FNAmtHour08Insurance=" & _SalaryPerH & ""
                                    End If
                                    _Qry &= vbCrLf & "  ,FNAmtHour08HR=" & CDbl(Format((_SalaryPerH * (Integer.Parse(Val(Rt.Item(_FilterTime).ToString)))) / 60, "0.00")) & ""
                                Case 9

                                    If _FTStateInsuranceInH9 <> "1" Then
                                        _Qry &= vbCrLf & "  FNAmtHour09=" & _FNNetAmt & ""
                                        _Qry &= vbCrLf & "  ,FNAmtHour09Insurance=0"
                                    Else
                                        _Qry &= vbCrLf & "  FNAmtHour09=0"
                                        _Qry &= vbCrLf & "  ,FNAmtHour09Insurance=" & _SalaryPerOT & ""
                                    End If
                                    _Qry &= vbCrLf & "  ,FNAmtHour09HR=" & CDbl(Format((_SalaryPerOT * (Integer.Parse(Val(Rt.Item(_FilterTime).ToString)))) / 60, "0.00")) & ""
                                Case 10

                                    If _FTStateInsuranceInH10 <> "1" Then
                                        _Qry &= vbCrLf & "  FNAmtHour10=" & _FNNetAmt & ""
                                        _Qry &= vbCrLf & "  ,FNAmtHour10Insurance=0"
                                    Else
                                        _Qry &= vbCrLf & "  FNAmtHour10=0"
                                        _Qry &= vbCrLf & "  ,FNAmtHour10Insurance=" & _SalaryPerOT & ""
                                    End If
                                    _Qry &= vbCrLf & "  ,FNAmtHour10HR=" & CDbl(Format((_SalaryPerOT * (Integer.Parse(Val(Rt.Item(_FilterTime).ToString)))) / 60, "0.00")) & ""
                                Case 11

                                    If _FTStateInsuranceInH11 <> "1" Then
                                        _Qry &= vbCrLf & "  FNAmtHour11=" & _FNNetAmt & ""
                                        _Qry &= vbCrLf & "  ,FNAmtHour11Insurance=0"
                                    Else
                                        _Qry &= vbCrLf & "  FNAmtHour11=0"
                                        _Qry &= vbCrLf & "  ,FNAmtHour11Insurance=" & _SalaryPerOT & ""
                                    End If
                                    _Qry &= vbCrLf & "  ,FNAmtHour11HR=" & CDbl(Format((_SalaryPerOT * (Integer.Parse(Val(Rt.Item(_FilterTime).ToString)))) / 60, "0.00")) & ""
                                Case 12

                                    If _FTStateInsuranceInH12 <> "1" Then
                                        _Qry &= vbCrLf & "  FNAmtHour12=" & _FNNetAmt & ""
                                        _Qry &= vbCrLf & "  ,FNAmtHour12Insurance=0"
                                    Else
                                        _Qry &= vbCrLf & "  FNAmtHour12=0"
                                        _Qry &= vbCrLf & "  ,FNAmtHour12Insurance=" & _SalaryPerOT & ""
                                    End If
                                    _Qry &= vbCrLf & "  ,FNAmtHour12HR=" & CDbl(Format((_SalaryPerOT * (Integer.Parse(Val(Rt.Item(_FilterTime).ToString)))) / 60, "0.00")) & ""
                                Case 13

                                    If _FTStateInsuranceInH13 <> "1" Then
                                        _Qry &= vbCrLf & "  FNAmtHour13=" & _FNNetAmt & ""
                                        _Qry &= vbCrLf & "  ,FNAmtHour13Insurance=0"
                                    Else
                                        _Qry &= vbCrLf & "  FNAmtHour13=0"
                                        _Qry &= vbCrLf & "  ,FNAmtHour13Insurance=" & _SalaryPerOT & ""
                                    End If
                                    _Qry &= vbCrLf & "  ,FNAmtHour13HR=" & CDbl(Format((_SalaryPerOT * (Integer.Parse(Val(Rt.Item(_FilterTime).ToString)))) / 60, "0.00")) & ""
                            End Select

                            _Qry &= vbCrLf & "   WHERE FNHSysUnitSectId=" & Integer.Parse(Val(R!FNHSysUnitSectId.ToString)) & ""
                            _Qry &= vbCrLf & "       AND FNHSysEmpID=" & Integer.Parse(Val(Rt!FNHSysEmpID.ToString)) & ""
                            _Qry &= vbCrLf & "       AND FTCalDate='" & _CalDate & "'"

                            HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

                        Next
                    Next

                    Select Case Val(R!FTIncentiveTypeIdx.ToString)
                        Case 1, 2, 3, 4

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

                            If _FNHSysIncenFormulaId <= 0 Then

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

                            Else

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
                                _Qry &= vbCrLf & "    FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TPRODMIncentiveFormulaLevel AS P WITH(NOLOCK)"
                                _Qry &= vbCrLf & " WHERE FNHSysIncenFormulaId=" & _FNHSysIncenFormulaId & ""
                                _Qry &= vbCrLf & "  ORDER BY FNLVSeq"

                            End If

                            _dtPrice = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MASTER)

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

                            If _dtPrice.Select("FNActQty >0", "FNLVSeq").Length > 0 Then
                                For Each Rxp As DataRow In _dtPrice.Select("FNActQty >0", "FNLVSeq")
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

                            Dim Ih As Integer = 1
                            Dim LevQty As Integer = 0
                            Dim ActQty As Integer = 0
                            Dim SumActQty As Integer = 0
                            Dim dtcallavel As New DataTable
                            _Qry = "  Select  FTCalDate, FNHSysUnitSectId, FNHSysStyleId, FTOrderNo, FTSubOrderNo, FNStateSewPack"
                            _Qry &= vbCrLf & ", FNSam, FNPricePerSam, FNPriceMultiple, FNDisPrice, FNNetPrice, FNHour01Qty, FNHour02Qty, FNHour03Qty"
                            _Qry &= vbCrLf & ", FNHour04Qty, FNHour05Qty, FNHour06Qty, FNHour07Qty, FNHour08Qty, FNHour09Qty, FNHour10Qty, FNHour11Qty"
                            _Qry &= vbCrLf & ", FNHour12Qty, FNHour13Qty, FNTotalQty"
                            _Qry &= vbCrLf & " From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTIncentive_Style_Hour AS X WITH(NOLOCK)"
                            _Qry &= vbCrLf & " WHERE FTCalDate='" & _CalDate & "' AND  FNHSysUnitSectId =" & Integer.Parse(Val(R!FNHSysUnitSectId.ToString)) & " AND FNStateSewPack<>2"

                            dtcallavel = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

                            _FNSeq = 0
                            For Each Rxp As DataRow In _dtPrice.Select("FNTargetChkQty >0", "FNLVSeq")
                                _FNSeq = _FNSeq + 1
                                For Each Rxl As DataRow In dtcallavel.Rows

                                    _Qry = " INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTIncentive_Style_Hour_Level("
                                    _Qry &= vbCrLf & "  FTInsUser, FDInsDate, FTInsTime,  FTCalDate, FNHSysUnitSectId"
                                    _Qry &= vbCrLf & ", FNlevelSeq, FNHSysStyleId, FTOrderNo, FTSubOrderNo, FNStateSewPack, FNPrice, FNPriceMultiple"
                                    _Qry &= vbCrLf & ", FNHour01Qty, FNHour02Qty, FNHour03Qty, FNHour04Qty, FNHour05Qty, FNHour06Qty, FNHour07Qty"
                                    _Qry &= vbCrLf & ", FNHour08Qty, FNHour09Qty, FNHour10Qty, FNHour11Qty, FNHour12Qty, FNHour13Qty, FNQuantity"
                                    _Qry &= vbCrLf & " )"
                                    _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                                    _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & ""
                                    _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & ""
                                    _Qry &= vbCrLf & " ,'" & _CalDate & "'   "
                                    _Qry &= vbCrLf & " ," & Integer.Parse(Val(R!FNHSysUnitSectId.ToString)) & ""
                                    _Qry &= vbCrLf & " ," & _FNSeq & ""
                                    _Qry &= vbCrLf & " ," & Integer.Parse(Val(Rxl!FNHSysStyleId.ToString)) & ""
                                    _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(Rxl!FTOrderNo.ToString) & "'"
                                    _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(Rxl!FTSubOrderNo.ToString) & "'"
                                    _Qry &= vbCrLf & " ," & Integer.Parse(Val(Rxl!FNStateSewPack.ToString)) & ""
                                    _Qry &= vbCrLf & " ," & Double.Parse(Val(Rxl!FNNetPrice.ToString)) & ""
                                    _Qry &= vbCrLf & " ," & Double.Parse(Val(Rxp!FNPriceMultiple.ToString)) & ""
                                    _Qry &= vbCrLf & ",0,0,0,0,0,0,0,0,0,0,0,0,0,0"


                                    HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_HR)

                                Next

                            Next

                            Dim MaxLav As Integer = 0


                            For Each Rxp As DataRow In _dtPrice.Select("FNTargetChkQty >0", "FNLVSeq")
                                MaxLav = MaxLav + 1
                            Next
                            _FNSeq = 0
                            For Each Rxp As DataRow In _dtPrice.Select("FNTargetChkQty >0", "FNLVSeq")

                                _FNSeq = _FNSeq + 1
                                LevQty = Val(Rxp!FNTargetQty)

                                While Ih <= 13

                                    Select Case Ih
                                        Case 1
                                            _FilterTime = "FNHour01Qty"
                                        Case 2
                                            _FilterTime = "FNHour02Qty"
                                        Case 3
                                            _FilterTime = "FNHour03Qty"
                                        Case 4
                                            _FilterTime = "FNHour04Qty"
                                        Case 5
                                            _FilterTime = "FNHour05Qty"
                                        Case 6
                                            _FilterTime = "FNHour06Qty"
                                        Case 7
                                            _FilterTime = "FNHour07Qty"
                                        Case 8
                                            _FilterTime = "FNHour08Qty"
                                        Case 9
                                            _FilterTime = "FNHour09Qty"
                                        Case 10
                                            _FilterTime = "FNHour10Qty"
                                        Case 11
                                            _FilterTime = "FNHour11Qty"
                                        Case 12
                                            _FilterTime = "FNHour12Qty"
                                        Case 13
                                            _FilterTime = "FNHour13Qty"
                                    End Select

                                    dtcallavel.BeginInit()
                                    For Each Rxl As DataRow In dtcallavel.Select(_FilterTime & ">0", "FNNetPrice")
                                        ActQty = Val(Rxl.Item(_FilterTime))

                                        If SumActQty + ActQty > LevQty Then

                                            If _FNSeq < MaxLav Then
                                                ActQty = LevQty - SumActQty

                                            End If

                                            Rxl.Item(_FilterTime) = Val(Rxl.Item(_FilterTime)) - ActQty

                                            If _FNSeq < MaxLav Then
                                                dtcallavel.EndInit()
                                            End If

                                            SumActQty = SumActQty + ActQty
                                            _Qry = " UPDATE A Set "

                                            Select Case Ih
                                                Case 1
                                                    _Qry &= vbCrLf & "FNHour01Qty=" & ActQty & ""
                                                Case 2
                                                    _Qry &= vbCrLf & "FNHour02Qty=" & ActQty & ""
                                                Case 3
                                                    _Qry &= vbCrLf & "FNHour03Qty=" & ActQty & ""
                                                Case 4
                                                    _Qry &= vbCrLf & "FNHour04Qty=" & ActQty & ""
                                                Case 5
                                                    _Qry &= vbCrLf & "FNHour05Qty=" & ActQty & ""
                                                Case 6
                                                    _Qry &= vbCrLf & "FNHour06Qty=" & ActQty & ""
                                                Case 7
                                                    _Qry &= vbCrLf & "FNHour07Qty=" & ActQty & ""
                                                Case 8
                                                    _Qry &= vbCrLf & "FNHour08Qty=" & ActQty & ""
                                                Case 9
                                                    _Qry &= vbCrLf & "FNHour09Qty=" & ActQty & ""
                                                Case 10
                                                    _Qry &= vbCrLf & "FNHour10Qty=" & ActQty & ""
                                                Case 11
                                                    _Qry &= vbCrLf & "FNHour11Qty=" & ActQty & ""
                                                Case 12
                                                    _Qry &= vbCrLf & "FNHour12Qty=" & ActQty & ""
                                                Case 13
                                                    _Qry &= vbCrLf & "FNHour13Qty=" & ActQty & ""
                                            End Select

                                            _Qry &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTIncentive_Style_Hour_Level As A "
                                            _Qry &= vbCrLf & " WHERE FTCalDate='" & _CalDate & "' AND  FNHSysUnitSectId =" & Integer.Parse(Val(R!FNHSysUnitSectId.ToString)) & ""
                                            _Qry &= vbCrLf & " AND FNlevelSeq=" & _FNSeq & ""
                                            _Qry &= vbCrLf & " AND  FNHSysStyleId =" & Integer.Parse(Val(Rxl!FNHSysStyleId.ToString)) & ""
                                            _Qry &= vbCrLf & " AND  FNStateSewPack =" & Integer.Parse(Val(Rxl!FNStateSewPack.ToString)) & ""
                                            _Qry &= vbCrLf & " AND  FTOrderNo ='" & HI.UL.ULF.rpQuoted(Rxl!FTOrderNo.ToString) & "'"
                                            _Qry &= vbCrLf & " AND  FTSubOrderNo ='" & HI.UL.ULF.rpQuoted(Rxl!FTSubOrderNo.ToString) & "'"

                                            HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_HR)

                                            If _FNSeq < MaxLav Then
                                                GoTo SkiptonextLevel
                                            End If


                                        ElseIf SumActQty + ActQty = LevQty Then


                                            Rxl.Item(_FilterTime) = Val(Rxl.Item(_FilterTime)) - ActQty
                                            dtcallavel.EndInit()
                                            SumActQty = SumActQty + ActQty
                                            _Qry = " UPDATE A Set "

                                            Select Case Ih
                                                Case 1
                                                    _Qry &= vbCrLf & "FNHour01Qty=" & ActQty & ""
                                                Case 2
                                                    _Qry &= vbCrLf & "FNHour02Qty=" & ActQty & ""
                                                Case 3
                                                    _Qry &= vbCrLf & "FNHour03Qty=" & ActQty & ""
                                                Case 4
                                                    _Qry &= vbCrLf & "FNHour04Qty=" & ActQty & ""
                                                Case 5
                                                    _Qry &= vbCrLf & "FNHour05Qty=" & ActQty & ""
                                                Case 6
                                                    _Qry &= vbCrLf & "FNHour06Qty=" & ActQty & ""
                                                Case 7
                                                    _Qry &= vbCrLf & "FNHour07Qty=" & ActQty & ""
                                                Case 8
                                                    _Qry &= vbCrLf & "FNHour08Qty=" & ActQty & ""
                                                Case 9
                                                    _Qry &= vbCrLf & "FNHour09Qty=" & ActQty & ""
                                                Case 10
                                                    _Qry &= vbCrLf & "FNHour10Qty=" & ActQty & ""
                                                Case 11
                                                    _Qry &= vbCrLf & "FNHour11Qty=" & ActQty & ""
                                                Case 12
                                                    _Qry &= vbCrLf & "FNHour12Qty=" & ActQty & ""
                                                Case 13
                                                    _Qry &= vbCrLf & "FNHour13Qty=" & ActQty & ""
                                            End Select

                                            _Qry &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTIncentive_Style_Hour_Level As A "
                                            _Qry &= vbCrLf & " WHERE FTCalDate='" & _CalDate & "' AND  FNHSysUnitSectId =" & Integer.Parse(Val(R!FNHSysUnitSectId.ToString)) & ""
                                            _Qry &= vbCrLf & " AND FNlevelSeq=" & _FNSeq & ""
                                            _Qry &= vbCrLf & " AND  FNHSysStyleId =" & Integer.Parse(Val(Rxl!FNHSysStyleId.ToString)) & ""
                                            _Qry &= vbCrLf & " AND  FNStateSewPack =" & Integer.Parse(Val(Rxl!FNStateSewPack.ToString)) & ""
                                            _Qry &= vbCrLf & " AND  FTOrderNo ='" & HI.UL.ULF.rpQuoted(Rxl!FTOrderNo.ToString) & "'"
                                            _Qry &= vbCrLf & " AND  FTSubOrderNo ='" & HI.UL.ULF.rpQuoted(Rxl!FTSubOrderNo.ToString) & "'"

                                            HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_HR)

                                            If _FNSeq < MaxLav Then
                                                GoTo SkiptonextLevel
                                            End If
                                        ElseIf SumActQty + ActQty < LevQty Then

                                            Rxl.Item(_FilterTime) = Val(Rxl.Item(_FilterTime)) - ActQty

                                            SumActQty = SumActQty + ActQty
                                            _Qry = " UPDATE A Set "

                                            Select Case Ih
                                                Case 1
                                                    _Qry &= vbCrLf & "FNHour01Qty=" & ActQty & ""
                                                Case 2
                                                    _Qry &= vbCrLf & "FNHour02Qty=" & ActQty & ""
                                                Case 3
                                                    _Qry &= vbCrLf & "FNHour03Qty=" & ActQty & ""
                                                Case 4
                                                    _Qry &= vbCrLf & "FNHour04Qty=" & ActQty & ""
                                                Case 5
                                                    _Qry &= vbCrLf & "FNHour05Qty=" & ActQty & ""
                                                Case 6
                                                    _Qry &= vbCrLf & "FNHour06Qty=" & ActQty & ""
                                                Case 7
                                                    _Qry &= vbCrLf & "FNHour07Qty=" & ActQty & ""
                                                Case 8
                                                    _Qry &= vbCrLf & "FNHour08Qty=" & ActQty & ""
                                                Case 9
                                                    _Qry &= vbCrLf & "FNHour09Qty=" & ActQty & ""
                                                Case 10
                                                    _Qry &= vbCrLf & "FNHour10Qty=" & ActQty & ""
                                                Case 11
                                                    _Qry &= vbCrLf & "FNHour11Qty=" & ActQty & ""
                                                Case 12
                                                    _Qry &= vbCrLf & "FNHour12Qty=" & ActQty & ""
                                                Case 13
                                                    _Qry &= vbCrLf & "FNHour13Qty=" & ActQty & ""
                                            End Select

                                            _Qry &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTIncentive_Style_Hour_Level As A "
                                            _Qry &= vbCrLf & " WHERE FTCalDate='" & _CalDate & "' AND  FNHSysUnitSectId =" & Integer.Parse(Val(R!FNHSysUnitSectId.ToString)) & ""
                                            _Qry &= vbCrLf & " AND FNlevelSeq=" & _FNSeq & ""
                                            _Qry &= vbCrLf & " AND  FNHSysStyleId =" & Integer.Parse(Val(Rxl!FNHSysStyleId.ToString)) & ""
                                            _Qry &= vbCrLf & " AND  FNStateSewPack =" & Integer.Parse(Val(Rxl!FNStateSewPack.ToString)) & ""
                                            _Qry &= vbCrLf & " AND  FTOrderNo ='" & HI.UL.ULF.rpQuoted(Rxl!FTOrderNo.ToString) & "'"
                                            _Qry &= vbCrLf & " AND  FTSubOrderNo ='" & HI.UL.ULF.rpQuoted(Rxl!FTSubOrderNo.ToString) & "'"

                                            HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_HR)

                                        End If

                                    Next

                                    dtcallavel.EndInit()

                                    If dtcallavel.Select(_FilterTime & ">0", "FNNetPrice").Length <= 0 Then
                                        Ih = Ih + 1
                                    End If

                                End While
SkiptonextLevel:
                                '_Qry = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTIncentive_Style_Hour_Level ("
                                '_Qry &= vbCrLf & "  FTInsUser, FDInsDate, FTInsTime,FTCalDate, FNHSysUnitSectId, FNlevelSeq, FNHSysStyleId, FTOrderNo, FTSubOrderNo, FNStateSewPack, FNPrice, FNPriceMultiple"
                                '_Qry &= vbCrLf & ",  FNHour01Qty, FNHour02Qty, FNHour03Qty, FNHour04Qty, FNHour05Qty, FNHour06Qty, FNHour07Qty, FNHour08Qty"
                                '_Qry &= vbCrLf & ",FNHour09Qty, FNHour10Qty, FNHour11Qty, FNHour12Qty, FNHour13Qty, FNQuantity"
                                '_Qry &= vbCrLf & "  )"
                                '_Qry &= vbCrLf & " Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                                '_Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & ""
                                '_Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & ""
                                '_Qry &= vbCrLf & " ,'" & _CalDate & "'   "
                                '_Qry &= vbCrLf & " ," & Integer.Parse(Val(R!FNHSysUnitSectId.ToString)) & ""
                                '_Qry &= vbCrLf & " ," & _FNSeq & ""
                                '_Qry &= vbCrLf & " ," & Val(Rxp!FNPriceMul.ToString) & ""
                                '_Qry &= vbCrLf & " ," & Val(Rxp!FNActQty.ToString) & ""
                                '_Qry &= vbCrLf & " ," & CDbl(Format((CDbl(Format(Val(Rxp!FNPriceMul.ToString) * Val(Rxp!FNActQty.ToString), "0.00"))), "0.00")) & ""
                                '_Qry &= vbCrLf & " ," & Val(Rxp!FNPriceMul.ToString) & ""

                                'HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

                            Next

                            dtcallavel.Dispose()

                            _Qry = " UPDATE A Set FNQuantity = FNHour01Qty+ FNHour02Qty+ FNHour03Qty+ FNHour04Qty+ FNHour05Qty+ FNHour06Qty+ FNHour07Qty"
                            _Qry &= vbCrLf & "+ FNHour08Qty+ FNHour09Qty+ FNHour10Qty+ FNHour11Qty+ FNHour12Qty+ FNHour13Qty "
                            _Qry &= vbCrLf & ",FNHour01Amt=Convert(numeric(18,2),FNHour01Qty * (FNPrice * FNPriceMultiple) )"
                            _Qry &= vbCrLf & ",FNHour02Amt=Convert(numeric(18,2),FNHour02Qty * (FNPrice * FNPriceMultiple) )"
                            _Qry &= vbCrLf & ",FNHour03Amt=Convert(numeric(18,2),FNHour03Qty * (FNPrice * FNPriceMultiple) )"
                            _Qry &= vbCrLf & ",FNHour04Amt=Convert(numeric(18,2),FNHour04Qty * (FNPrice * FNPriceMultiple) )"
                            _Qry &= vbCrLf & ",FNHour05Amt=Convert(numeric(18,2),FNHour05Qty * (FNPrice * FNPriceMultiple) )"
                            _Qry &= vbCrLf & ",FNHour06Amt=Convert(numeric(18,2),FNHour06Qty * (FNPrice * FNPriceMultiple) )"
                            _Qry &= vbCrLf & ",FNHour07Amt=Convert(numeric(18,2),FNHour07Qty * (FNPrice * FNPriceMultiple) )"
                            _Qry &= vbCrLf & ",FNHour08Amt=Convert(numeric(18,2),FNHour08Qty * (FNPrice * FNPriceMultiple) )"
                            _Qry &= vbCrLf & ",FNHour09Amt=Convert(numeric(18,2),FNHour09Qty * (FNPrice * FNPriceMultiple) )"
                            _Qry &= vbCrLf & ",FNHour10Amt=Convert(numeric(18,2),FNHour10Qty * (FNPrice * FNPriceMultiple) )"
                            _Qry &= vbCrLf & ",FNHour11Amt=Convert(numeric(18,2),FNHour11Qty * (FNPrice * FNPriceMultiple) )"
                            _Qry &= vbCrLf & ",FNHour12Amt=Convert(numeric(18,2),FNHour12Qty * (FNPrice * FNPriceMultiple) )"
                            _Qry &= vbCrLf & ",FNHour13Amt=Convert(numeric(18,2),FNHour13Qty * (FNPrice * FNPriceMultiple) )"
                            _Qry &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTIncentive_Style_Hour_Level As A "
                            _Qry &= vbCrLf & " WHERE FTCalDate='" & _CalDate & "' AND  FNHSysUnitSectId =" & Integer.Parse(Val(R!FNHSysUnitSectId.ToString)) & ""
                            HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_HR)


                            '-----------Modify 2018/11/22 
                            '_Qry = " UPDATE A Set FNNetAmt = FNAmtHour01HR+ FNAmtHour02HR+ FNAmtHour03HR+ FNAmtHour04HR+ FNAmtHour05HR+ FNAmtHour06HR+ FNAmtHour07HR+ FNAmtHour08HR + FNAmtOT"
                            '' _Qry &= vbCrLf & "+ FNAmtHour09HR+ FNAmtHour10HR+ FNAmtHour11HR+ FNAmtHour12HR+ FNAmtHour13HR "
                            '_Qry &= vbCrLf & ",FNAmtNormal =FNAmtHour01HR+ FNAmtHour02HR+ FNAmtHour03HR+ FNAmtHour04HR+ FNAmtHour05HR+ FNAmtHour06HR+ FNAmtHour07HR+ FNAmtHour08HR"
                            '' _Qry &= vbCrLf & ",FNAmtOT =FNAmtHour09HR+ FNAmtHour10HR+ FNAmtHour11HR+ FNAmtHour12HR+ FNAmtHour13HR"


                            _Qry = " UPDATE A Set FNNetAmt = (CASE WHEN FNTotalTimHRFull>=480.0 THEN FNSalary ELSE  Convert(numeric(18,2),(FNSalary /480.0)  * FNTotalTimHRFull) END) +  (CASE WHEN FNTotalTimeOTHRFull <=0 THEN 0.00 ELSE  Convert(numeric(18,2),((FNSalary /480.0) *1.5)  * FNTotalTimeOTHRFull) END)  "
                            _Qry &= vbCrLf & ",FNAmtNormal =(CASE WHEN FNTotalTimHRFull>=480.0 THEN FNSalary ELSE  Convert(numeric(18,2),(FNSalary /480.0)  * FNTotalTimHRFull) END) "
                            _Qry &= vbCrLf & ",FNAmtOT = (CASE WHEN FNTotalTimeOTHRFull <=0 THEN 0.00 ELSE  Convert(numeric(18,2),((FNSalary /480.0) *1.5)  * FNTotalTimeOTHRFull) END) "
                            _Qry &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTIncentive_Emp As A "
                            _Qry &= vbCrLf & " WHERE FTCalDate='" & _CalDate & "' AND  FNHSysUnitSectId =" & Integer.Parse(Val(R!FNHSysUnitSectId.ToString)) & " AND FTStateRelease='1'"
                            HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_HR)
                            '-----------Modify 2018/11/22 

                            _Qry = " DELETE   A  "
                            _Qry &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTIncentive_Style_Hour_Level As A "
                            _Qry &= vbCrLf & " WHERE FTCalDate='" & _CalDate & "' AND  FNHSysUnitSectId =" & Integer.Parse(Val(R!FNHSysUnitSectId.ToString)) & " AND FNQuantity =0"
                            HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_HR)

                            If (_LineAmt + _PackAmt) > 0 Then

                                Dim dtamtlevel As New DataTable
                                _Qry = "    Select  SUM(FNHour01Amt) As FNHour01Amt, SUM(FNHour02Amt) As FNHour02Amt"
                                _Qry &= vbCrLf & " , SUM(FNHour03Amt) As FNHour03Amt, SUM(FNHour04Amt) As FNHour04Amt"
                                _Qry &= vbCrLf & " , SUM(FNHour05Amt) As FNHour05Amt, SUM(FNHour06Amt) AS FNHour06Amt"
                                _Qry &= vbCrLf & " , SUM(FNHour07Amt) AS FNHour07Amt, SUM(FNHour08Amt) AS FNHour08Amt"
                                _Qry &= vbCrLf & " , SUM(FNHour09Amt) AS FNHour09Amt, SUM(FNHour10Amt) AS FNHour10Amt"
                                _Qry &= vbCrLf & " , SUM(FNHour11Amt) AS FNHour11Amt, SUM(FNHour12Amt) As FNHour12Amt, SUM(FNHour13Amt) As FNHour13Amt"
                                _Qry &= vbCrLf & "  From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTIncentive_Style_Hour_Level AS X WITH(NOLOCK) "
                                _Qry &= vbCrLf & " WHERE FTCalDate='" & _CalDate & "' AND  FNHSysUnitSectId =" & Integer.Parse(Val(R!FNHSysUnitSectId.ToString)) & ""

                                dtamtlevel = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

                                _FTotalAmtH01 = 0
                                _FTotalAmtH02 = 0
                                _FTotalAmtH03 = 0
                                _FTotalAmtH04 = 0
                                _FTotalAmtH05 = 0
                                _FTotalAmtH06 = 0
                                _FTotalAmtH07 = 0
                                _FTotalAmtH08 = 0
                                _FTotalAmtH09 = 0
                                _FTotalAmtH10 = 0
                                _FTotalAmtH11 = 0
                                _FTotalAmtH12 = 0
                                _FTotalAmtH13 = 0

                                For Each Rcl As DataRow In dtamtlevel.Rows

                                    _FTotalAmtH01 = Double.Parse(Val(Rcl!FNHour01Amt.ToString))
                                    _FTotalAmtH02 = Double.Parse(Val(Rcl!FNHour02Amt.ToString))
                                    _FTotalAmtH03 = Double.Parse(Val(Rcl!FNHour03Amt.ToString))
                                    _FTotalAmtH04 = Double.Parse(Val(Rcl!FNHour04Amt.ToString))
                                    _FTotalAmtH05 = Double.Parse(Val(Rcl!FNHour05Amt.ToString))
                                    _FTotalAmtH06 = Double.Parse(Val(Rcl!FNHour06Amt.ToString))
                                    _FTotalAmtH07 = Double.Parse(Val(Rcl!FNHour07Amt.ToString))
                                    _FTotalAmtH08 = Double.Parse(Val(Rcl!FNHour08Amt.ToString))
                                    _FTotalAmtH09 = Double.Parse(Val(Rcl!FNHour09Amt.ToString))
                                    _FTotalAmtH10 = Double.Parse(Val(Rcl!FNHour10Amt.ToString))
                                    _FTotalAmtH11 = Double.Parse(Val(Rcl!FNHour11Amt.ToString))
                                    _FTotalAmtH12 = Double.Parse(Val(Rcl!FNHour12Amt.ToString))
                                    _FTotalAmtH13 = Double.Parse(Val(Rcl!FNHour13Amt.ToString))

                                    Exit For
                                Next

                                dtamtlevel.Dispose()



                                Dim dtamtpack As New DataTable
                                _Qry = "    Select  SUM(FNHour01Amt) As FNHour01Amt, SUM(FNHour02Amt) As FNHour02Amt"
                                _Qry &= vbCrLf & " , SUM(FNHour03Amt) As FNHour03Amt, SUM(FNHour04Amt) As FNHour04Amt"
                                _Qry &= vbCrLf & " , SUM(FNHour05Amt) As FNHour05Amt, SUM(FNHour06Amt) AS FNHour06Amt"
                                _Qry &= vbCrLf & " , SUM(FNHour07Amt) AS FNHour07Amt, SUM(FNHour08Amt) AS FNHour08Amt"
                                _Qry &= vbCrLf & " , SUM(FNHour09Amt) AS FNHour09Amt, SUM(FNHour10Amt) AS FNHour10Amt"
                                _Qry &= vbCrLf & " , SUM(FNHour11Amt) AS FNHour11Amt, SUM(FNHour12Amt) As FNHour12Amt, SUM(FNHour13Amt) As FNHour13Amt"
                                _Qry &= vbCrLf & "  From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTIncentive_Style_Hour AS X WITH(NOLOCK) "
                                _Qry &= vbCrLf & " WHERE FTCalDate='" & _CalDate & "' AND  FNHSysUnitSectId =" & Integer.Parse(Val(R!FNHSysUnitSectId.ToString)) & " AND FNStateSewPack=2"

                                dtamtpack = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

                                For Each Rcl As DataRow In dtamtpack.Rows

                                    _FTotalAmtH01 = _FTotalAmtH01 + Double.Parse(Val(Rcl!FNHour01Amt.ToString))
                                    _FTotalAmtH02 = _FTotalAmtH02 + Double.Parse(Val(Rcl!FNHour02Amt.ToString))
                                    _FTotalAmtH03 = _FTotalAmtH03 + Double.Parse(Val(Rcl!FNHour03Amt.ToString))
                                    _FTotalAmtH04 = _FTotalAmtH04 + Double.Parse(Val(Rcl!FNHour04Amt.ToString))
                                    _FTotalAmtH05 = _FTotalAmtH05 + Double.Parse(Val(Rcl!FNHour05Amt.ToString))
                                    _FTotalAmtH06 = _FTotalAmtH06 + Double.Parse(Val(Rcl!FNHour06Amt.ToString))
                                    _FTotalAmtH07 = _FTotalAmtH07 + Double.Parse(Val(Rcl!FNHour07Amt.ToString))
                                    _FTotalAmtH08 = _FTotalAmtH08 + Double.Parse(Val(Rcl!FNHour08Amt.ToString))
                                    _FTotalAmtH09 = _FTotalAmtH09 + Double.Parse(Val(Rcl!FNHour09Amt.ToString))
                                    _FTotalAmtH10 = _FTotalAmtH10 + Double.Parse(Val(Rcl!FNHour10Amt.ToString))
                                    _FTotalAmtH11 = _FTotalAmtH11 + Double.Parse(Val(Rcl!FNHour11Amt.ToString))
                                    _FTotalAmtH12 = _FTotalAmtH12 + Double.Parse(Val(Rcl!FNHour12Amt.ToString))
                                    _FTotalAmtH13 = _FTotalAmtH13 + Double.Parse(Val(Rcl!FNHour13Amt.ToString))

                                    Exit For
                                Next

                                dtamtpack.Dispose()

                                _FilterTime = ""
                                _HourIncentiveAmount = 0

                                For I As Integer = 1 To 13
                                    Select Case I
                                        Case 1

                                            _FilterTime = "FNH01"
                                            _HourIncentiveAmount = _FTotalAmtH01

                                        Case 2

                                            _FilterTime = "FNH02"
                                            _HourIncentiveAmount = _FTotalAmtH02

                                        Case 3

                                            _FilterTime = "FNH03"
                                            _HourIncentiveAmount = _FTotalAmtH03

                                        Case 4

                                            _FilterTime = "FNH04"
                                            _HourIncentiveAmount = _FTotalAmtH04

                                        Case 5

                                            _FilterTime = "FNH05"
                                            _HourIncentiveAmount = _FTotalAmtH05

                                        Case 6

                                            _FilterTime = "FNH06"
                                            _HourIncentiveAmount = _FTotalAmtH06

                                        Case 7

                                            _FilterTime = "FNH07"
                                            _HourIncentiveAmount = _FTotalAmtH07

                                        Case 8

                                            _FilterTime = "FNH08"
                                            _HourIncentiveAmount = _FTotalAmtH08

                                        Case 9

                                            _FilterTime = "FNH09"
                                            _HourIncentiveAmount = _FTotalAmtH09

                                        Case 10

                                            _FilterTime = "FNH10"
                                            _HourIncentiveAmount = _FTotalAmtH10

                                        Case 11

                                            _FilterTime = "FNH11"
                                            _HourIncentiveAmount = _FTotalAmtH11

                                        Case 12

                                            _FilterTime = "FNH12"
                                            _HourIncentiveAmount = _FTotalAmtH12

                                        Case 13

                                            _FilterTime = "FNH13"
                                            _HourIncentiveAmount = _FTotalAmtH13

                                    End Select

                                    _TotalTimeMin = 0

                                    For Each Rt As DataRow In _dtemptime.Select("FTStateCal<>'1'  AND " & _FilterTime & ">0 ")

                                        _TotalTimeMin = _TotalTimeMin + Integer.Parse(Val(Rt.Item(_FilterTime).ToString))


                                    Next

                                    _CountEmpIncentive = _dtemptime.Select("FTStateCal<>'1'  AND " & _FilterTime & ">0").Length
                                    _EmpIncentiveAmt = 0

                                    If _CountEmpIncentive > 0 Then
                                        _EmpIncentiveAmt = Double.Parse(Format(_HourIncentiveAmount / _CountEmpIncentive, "0.00"))
                                    Else
                                        _EmpIncentiveAmt = 0
                                    End If

                                    _TotalEmpCal = 0
                                    _TotalEmpAmt = 0

                                    For Each Rt As DataRow In _dtemptime.Select("FTStateCal<>'1'  AND " & _FilterTime & ">0 ", _FilterTime)
                                        _TotalEmpCal = _TotalEmpCal + 1

                                        _Salary = Double.Parse(Val(Rt!FNSalary.ToString))
                                        _SalaryPerH = Double.Parse(Format(_Salary / 8, "0.000"))
                                        _SalaryPerOT = Double.Parse(Format(_SalaryPerH * 1.5, "0.0000"))

                                        If _FixHour > 0 Then
                                        End If

                                        If Integer.Parse(Val(Rt!FNHSysEmpID.ToString)) = 1403020926 Then
                                            Beep()
                                        End If

                                        'If _TotalEmpCal = _CountEmpIncentive Then
                                        '    _EmpIncentiveAmt = _HourIncentiveAmount - _TotalEmpAmt
                                        'Else
                                        '    _EmpIncentiveAmt = CDbl(Format((_HourIncentiveAmount * (Integer.Parse(Val(Rt.Item(_FilterTime).ToString)))) / _TotalTimeMin, "0.00"))
                                        'End If

                                        _EmpIncentiveAmt = CDbl(Format((_HourIncentiveAmount * (Integer.Parse(Val(Rt.Item(_FilterTime).ToString)))) / _TotalTimeMin, "0.00"))
                                        _TotalEmpAmt = _TotalEmpAmt + _EmpIncentiveAmt

                                        _FNNetAmt = _EmpIncentiveAmt

                                        _Qry = " UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTIncentive_Emp "
                                        _Qry &= vbCrLf & "  SET "

                                        Select Case I
                                            Case 1
                                                _Qry &= vbCrLf & "  FNAmtHour01NewIncentive=" & _FNNetAmt & ""
                                            Case 2
                                                _Qry &= vbCrLf & "  FNAmtHour02NewIncentive=" & _FNNetAmt & ""
                                            Case 3
                                                _Qry &= vbCrLf & "  FNAmtHour03NewIncentive=" & _FNNetAmt & ""
                                            Case 4
                                                _Qry &= vbCrLf & "  FNAmtHour04NewIncentive=" & _FNNetAmt & ""
                                            Case 5
                                                _Qry &= vbCrLf & "  FNAmtHour05NewIncentive=" & _FNNetAmt & ""
                                            Case 6
                                                _Qry &= vbCrLf & "  FNAmtHour06NewIncentive=" & _FNNetAmt & ""
                                            Case 7
                                                _Qry &= vbCrLf & "  FNAmtHour07NewIncentive=" & _FNNetAmt & ""
                                            Case 8
                                                _Qry &= vbCrLf & "  FNAmtHour08NewIncentive=" & _FNNetAmt & ""
                                            Case 9
                                                _Qry &= vbCrLf & "  FNAmtHour09NewIncentive=" & _FNNetAmt & ""
                                            Case 10
                                                _Qry &= vbCrLf & "  FNAmtHour10NewIncentive=" & _FNNetAmt & ""
                                            Case 11
                                                _Qry &= vbCrLf & "  FNAmtHour11NewIncentive=" & _FNNetAmt & ""
                                            Case 12
                                                _Qry &= vbCrLf & "  FNAmtHour12NewIncentive=" & _FNNetAmt & ""
                                            Case 13
                                                _Qry &= vbCrLf & "  FNAmtHour13NewIncentive=" & _FNNetAmt & ""
                                        End Select

                                        _Qry &= vbCrLf & "   WHERE FNHSysUnitSectId=" & Integer.Parse(Val(R!FNHSysUnitSectId.ToString)) & ""
                                        _Qry &= vbCrLf & "       AND FNHSysEmpID=" & Integer.Parse(Val(Rt!FNHSysEmpID.ToString)) & ""
                                        _Qry &= vbCrLf & "       AND FTCalDate='" & _CalDate & "'"

                                        HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

                                    Next
                                Next


                                '-----------------start calculate Release team

                                For Each Rt As DataRow In _dtemptime.Select("FTStateCal<>'1'  AND FNTotalWorkingMin>0  AND FTStateRelease='1' ", "FNTotalWorkingMin")

                                    _TotalEmpCal = _TotalEmpCal + 1

                                    _Salary = Double.Parse(Val(Rt!FNSalary.ToString))
                                    _SalaryPerH = Double.Parse(Format(_Salary / 8, "0.000"))
                                    _SalaryPerOT = Double.Parse(Format(_SalaryPerH * 1.5, "0.0000"))

                                    If _FixHour > 0 Then
                                    End If

                                    _FNAmtOT = Double.Parse(Format(((_Salary / 480) * 0.5) * Integer.Parse(Val(Rt!FNWorkingOTMin.ToString)), "0.00"))

                                    _FNNetAmt = _EmpIncentiveAmt + _FNAmtOT

                                    _Qry = " UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTIncentive_Emp "
                                    _Qry &= vbCrLf & "  SET  FNAmtNewIncentive=FNAmtHour01NewIncentive+FNAmtHour02NewIncentive+FNAmtHour03NewIncentive+FNAmtHour04NewIncentive+FNAmtHour05NewIncentive+FNAmtHour06NewIncentive+FNAmtHour07NewIncentive+FNAmtHour08NewIncentive+FNAmtHour09NewIncentive+FNAmtHour10NewIncentive+FNAmtHour11NewIncentive+FNAmtHour12NewIncentive+FNAmtHour13NewIncentive"
                                    _Qry &= vbCrLf & "  , FNAmtOTNewIncentive=" & _FNAmtOT & ""
                                    _Qry &= vbCrLf & "  , FNNetAmtNewIncentive=(FNAmtHour01NewIncentive+FNAmtHour02NewIncentive+FNAmtHour03NewIncentive+FNAmtHour04NewIncentive+FNAmtHour05NewIncentive+FNAmtHour06NewIncentive+FNAmtHour07NewIncentive+FNAmtHour08NewIncentive+FNAmtHour09NewIncentive+FNAmtHour10NewIncentive+FNAmtHour11NewIncentive+FNAmtHour12NewIncentive+FNAmtHour13NewIncentive)+" & (_FNAmtOT + _FNQAValue) & ""
                                    _Qry &= vbCrLf & "   ,FNQAValue=" & _FNQAValue & " "
                                    _Qry &= vbCrLf & "   WHERE FNHSysUnitSectId=" & Integer.Parse(Val(R!FNHSysUnitSectId.ToString)) & ""
                                    _Qry &= vbCrLf & "       AND FNHSysEmpID=" & Integer.Parse(Val(Rt!FNHSysEmpID.ToString)) & ""
                                    _Qry &= vbCrLf & "       AND FTCalDate='" & _CalDate & "'"

                                    HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

                                Next
                                '---------------end calculate Release team

                                _Qry = "    Select   Sum(FNAmtHour01NewIncentive+FNAmtHour02NewIncentive+FNAmtHour03NewIncentive+FNAmtHour04NewIncentive+FNAmtHour05NewIncentive+FNAmtHour06NewIncentive+FNAmtHour07NewIncentive+FNAmtHour08NewIncentive+FNAmtHour09NewIncentive+FNAmtHour10NewIncentive+FNAmtHour11NewIncentive+FNAmtHour12NewIncentive+FNAmtHour13NewIncentive) As FNNetAmtNewIncentive "
                                _Qry &= vbCrLf & "  From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTIncentive_Emp As X With(NOLOCK) "
                                _Qry &= vbCrLf & " WHERE FTCalDate='" & _CalDate & "' AND  FNHSysUnitSectId =" & Integer.Parse(Val(R!FNHSysUnitSectId.ToString)) & " AND FTStateRelease<>'1'"
                                _LineIncentiveAmt = Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR))

                                ' _LineIncentiveAmt = _LineIncentiveAmt - _DeductAmt

                                _LineIncentiveAmt = _LineIncentiveAmt ' + _PackAmt

                                _CountEmpIncentive = _dtemptime.Select("FTStateCal<>'1'  AND FTStateRelease<>'1'").Length

                                If _CountEmpIncentive > 0 Then
                                    _EmpIncentiveAmt = Double.Parse(Format(_LineIncentiveAmt / _CountEmpIncentive, "0.00"))
                                Else
                                    _EmpIncentiveAmt = 0
                                End If

                                _TotalEmpCal = 0
                                _TotalEmpAmt = 0
                                _TotalTimeMin = 0

                                For Each Rt As DataRow In _dtemptime.Select("FTStateCal<>'1'  AND FNTotalWorkingMin>0 AND FTStateRelease<>'1' ", "FNTotalWorkingMin")
                                    _TotalTimeMin = _TotalTimeMin + Integer.Parse(Val(Rt!FNTotalWorkingMin.ToString))
                                Next

                                Select Case Val(R!FTIncentiveTypeIdx.ToString)
                                    Case 4

                                        For Each Rt As DataRow In _dtemptime.Select("FTStateCal='1'  AND FNTotalWorkingMin>0 AND FTStateRelease<>'1' AND FTStateTrain='1' ", "FNTotalWorkingMin")
                                            _TotalTimeMin = _TotalTimeMin + Integer.Parse(Val(Rt!FNTotalWorkingMin.ToString))
                                        Next

                                End Select

                                GAmountLine = 0

                                For Each Rt As DataRow In _dtemptime.Select("FTStateCal<>'1'  AND FNTotalWorkingMin>0  AND FTStateRelease<>'1' ", "FNTotalWorkingMin")

                                    _TotalEmpCal = _TotalEmpCal + 1

                                    _Salary = Double.Parse(Val(Rt!FNSalary.ToString))
                                    _SalaryPerH = Double.Parse(Format(_Salary / 8, "0.000"))
                                    _SalaryPerOT = Double.Parse(Format(_SalaryPerH * 1.5, "0.0000"))

                                    If _FixHour > 0 Then
                                    End If

                                    ' If _TotalEmpCal = _CountEmpIncentive Then
                                    '      _EmpIncentiveAmt = _LineIncentiveAmt - _TotalEmpAmt
                                    ' Else
                                    _EmpIncentiveAmt = CDbl(Format((_LineIncentiveAmt * (Integer.Parse(Val(Rt!FNTotalWorkingMin.ToString)))) / _TotalTimeMin, "0.00"))
                                    ' End If

                                    _TotalEmpAmt = _TotalEmpAmt + _EmpIncentiveAmt

                                    _FNAmtOT = Double.Parse(Format(((_Salary / 480) * 0.5) * Integer.Parse(Val(Rt!FNWorkingOTMin.ToString)), "0.00"))

                                    _FNNetAmt = _EmpIncentiveAmt + _FNAmtOT

                                    _Qry = " UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTIncentive_Emp "
                                    _Qry &= vbCrLf & "  SET  FNAmtNewIncentive=" & _EmpIncentiveAmt & ""
                                    _Qry &= vbCrLf & "  , FNAmtOTNewIncentive=" & _FNAmtOT & ""
                                    _Qry &= vbCrLf & "  , FNNetAmtNewIncentive=" & (_FNNetAmt + _FNQAValue) & ""
                                    _Qry &= vbCrLf & "   ,FNQAValue=" & _FNQAValue & " "
                                    _Qry &= vbCrLf & "   WHERE FNHSysUnitSectId=" & Integer.Parse(Val(R!FNHSysUnitSectId.ToString)) & ""
                                    _Qry &= vbCrLf & "       AND FNHSysEmpID=" & Integer.Parse(Val(Rt!FNHSysEmpID.ToString)) & ""
                                    _Qry &= vbCrLf & "       AND FTCalDate='" & _CalDate & "'"

                                    HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)
                                    GAmountLine = GAmountLine + _EmpIncentiveAmt
                                Next


                                If _LineIncentiveAmt > GAmountLine Then

                                    Dim MaxMinute As Integer = 0
                                    Dim SPareAmont As Decimal = _LineIncentiveAmt - GAmountLine


                                    For Each Rt As DataRow In _dtemptime.Select("FTStateCal<>'1'  AND FNTotalWorkingMin>0  AND FTStateRelease<>'1' ", "FNTotalWorkingMin")
                                        MaxMinute = Integer.Parse(Val(Rt!FNTotalWorkingMin.ToString))

                                    Next

                                    If MaxMinute > 0 Then
                                        _TotalEmpCal = 0
                                        For Each Rt As DataRow In _dtemptime.Select("FTStateCal<>'1'  AND FNTotalWorkingMin>=" & MaxMinute & " AND FTStateRelease<>'1' ", "FNTotalWorkingMin ")
                                            _TotalEmpCal = _TotalEmpCal + 1
                                        Next
                                        Dim EmpSPareAmont As Decimal = CDbl(Format(SPareAmont / _TotalEmpCal, "0.00"))


                                        If (EmpSPareAmont * _TotalEmpCal) < SPareAmont Then
                                            EmpSPareAmont = EmpSPareAmont + 0.01
                                        End If


                                        For Each Rt As DataRow In _dtemptime.Select("FTStateCal<>'1'  AND FNTotalWorkingMin>=" & MaxMinute & " AND FTStateRelease<>'1' ", "FNTotalWorkingMin ")

                                            _Qry = " UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTIncentive_Emp "
                                            _Qry &= vbCrLf & "  SET  FNAmtNewIncentive=FNAmtNewIncentive+" & EmpSPareAmont & ""
                                            _Qry &= vbCrLf & "  , FNNetAmtNewIncentive=FNNetAmtNewIncentive+" & EmpSPareAmont & ""
                                            _Qry &= vbCrLf & "   WHERE FNHSysUnitSectId=" & Integer.Parse(Val(R!FNHSysUnitSectId.ToString)) & ""
                                            _Qry &= vbCrLf & "       AND FNHSysEmpID=" & Integer.Parse(Val(Rt!FNHSysEmpID.ToString)) & ""
                                            _Qry &= vbCrLf & "       AND FTCalDate='" & _CalDate & "'"

                                            HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)
                                        Next

                                    End If

                                End If

                            End If

                    End Select

                End If

                '-----------Modify 2018/11/22 
                '_Qry = " UPDATE A Set FNNetAmt = ("
                '_Qry &= vbCrLf & " Case when FNAmtHour01HR+ FNAmtHour02HR+ FNAmtHour03HR+ FNAmtHour04HR+ FNAmtHour05HR+ FNAmtHour06HR+ FNAmtHour07HR+ FNAmtHour08HR >  FNSalary "
                '_Qry &= vbCrLf & "then FNSalary else FNAmtHour01HR+ FNAmtHour02HR+ FNAmtHour03HR+ FNAmtHour04HR+ FNAmtHour05HR+ FNAmtHour06HR+ FNAmtHour07HR+ FNAmtHour08HR end ) + (case when FNAmtHour09HR+ FNAmtHour10HR+ FNAmtHour11HR+ FNAmtHour12HR+ FNAmtHour13HR  > FNAmtOT then FNAmtOT else FNAmtHour09HR+ FNAmtHour10HR+ FNAmtHour11HR+ FNAmtHour12HR+ FNAmtHour13HR  end )"
                '' _Qry &= vbCrLf & "+ FNAmtHour09HR+ FNAmtHour10HR+ FNAmtHour11HR+ FNAmtHour12HR+ FNAmtHour13HR "
                '_Qry &= vbCrLf & ",FNAmtNormal = case when FNAmtHour01HR+ FNAmtHour02HR+ FNAmtHour03HR+ FNAmtHour04HR+ FNAmtHour05HR+ FNAmtHour06HR+ FNAmtHour07HR+ FNAmtHour08HR >  FNSalary "
                '_Qry &= vbCrLf & "then FNSalary else FNAmtHour01HR+ FNAmtHour02HR+ FNAmtHour03HR+ FNAmtHour04HR+ FNAmtHour05HR+ FNAmtHour06HR+ FNAmtHour07HR+ FNAmtHour08HR end"
                '_Qry &= vbCrLf & ",FNAmtOT = case when FNAmtHour09HR+ FNAmtHour10HR+ FNAmtHour11HR+ FNAmtHour12HR+ FNAmtHour13HR  > FNAmtOT then FNAmtOT else FNAmtHour09HR+ FNAmtHour10HR+ FNAmtHour11HR+ FNAmtHour12HR+ FNAmtHour13HR  end "


                _Qry = " UPDATE A Set FNNetAmt = (CASE WHEN FNTotalTimHRFull>=480.0 THEN FNSalary ELSE  Convert(numeric(18,2),(FNSalary /480.0)  * FNTotalTimHRFull) END) +  (CASE WHEN FNTotalTimeOTHRFull <=0 THEN 0.00 ELSE  Convert(numeric(18,2),((FNSalary /480.0) *1.5)  * FNTotalTimeOTHRFull) END)  "
                _Qry &= vbCrLf & ",FNAmtNormal =(CASE WHEN FNTotalTimHRFull>=480.0 THEN FNSalary ELSE  Convert(numeric(18,2),(FNSalary /480.0)  * FNTotalTimHRFull) END) "
                _Qry &= vbCrLf & ",FNAmtOT = (CASE WHEN FNTotalTimeOTHRFull <=0 THEN 0.00 ELSE  Convert(numeric(18,2),((FNSalary /480.0) *1.5)  * FNTotalTimeOTHRFull) END) "

                _Qry &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTIncentive_Emp As A "
                _Qry &= vbCrLf & " WHERE FTCalDate='" & _CalDate & "' AND  FNHSysUnitSectId =" & Integer.Parse(Val(R!FNHSysUnitSectId.ToString)) & " AND FTStateRelease='1' "
                HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_HR)
                '-----------Modify 2018/11/22 

                For Each Rt As DataRow In _dtemptime.Rows


                    If (Integer.Parse(Val(Rt!FNHSysEmpID.ToString))) = 1403020341 Then
                        Beep()
                    End If

                    _Qry = " DELETE FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTWageDaily "
                    _Qry &= vbCrLf & "  WHERE  FNHSysEmpID=" & Integer.Parse(Val(Rt!FNHSysEmpID.ToString)) & ""
                    _Qry &= vbCrLf & "    AND  FTDateTrans='" & _CalDate & "'"
                    HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

                    _Qry = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTWageDaily ( "
                    _Qry &= vbCrLf & "   FTInsUser, FTInsDate, FTInsTime, FNHSysEmpID, FTDateTrans"
                    _Qry &= vbCrLf & " , FNAmtNormal, FNAmtOT, FNNetAmt,FNQAAmt,FNAmtFixedIncentive"

                    _Qry &= vbCrLf & ",  FNAmtHour01HR, FNAmtHour02HR, FNAmtHour03HR, FNAmtHour04HR, FNAmtHour05HR, FNAmtHour06HR"
                    _Qry &= vbCrLf & ", FNAmtHour07HR, FNAmtHour08HR, FNAmtHour09HR, FNAmtHour10HR"
                    _Qry &= vbCrLf & ",FNAmtHour11HR, FNAmtHour12HR, FNAmtHour13HR"

                    _Qry &= vbCrLf & " , FNProNormal, FNProOT, FNProOther, FNNetProAmt"

                    _Qry &= vbCrLf & ",  FNAmtHour01Incentive, FNAmtHour02Incentive"
                    _Qry &= vbCrLf & ", FNAmtHour03Incentive, FNAmtHour04Incentive, FNAmtHour05Incentive, FNAmtHour06Incentive"
                    _Qry &= vbCrLf & " ,FNAmtHour07Incentive, FNAmtHour08Incentive, FNAmtHour09Incentive, FNAmtHour10Incentive"
                    _Qry &= vbCrLf & ", FNAmtHour11Incentive, FNAmtHour12Incentive, FNAmtHour13Incentive"

                    _Qry &= vbCrLf & " )"

                    _Qry &= vbCrLf & " Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & ""
                    _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & ""

                    _Qry &= vbCrLf & " ," & Integer.Parse(Val(Rt!FNHSysEmpID.ToString)) & ""
                    _Qry &= vbCrLf & " ,'" & _CalDate & "'   "
                    '_Qry &= vbCrLf & " , MAX(FNAmtNormal) AS FNAmtNormal "
                    '_Qry &= vbCrLf & " , MAX(FNAmtOT) AS FNAmtOT "
                    '_Qry &= vbCrLf & " , MAX(FNNetAmt) AS FNNetAmt "

                    _Qry &= vbCrLf & " , SUM(ISNULL(FNAmtNormal,0)  - (ISNULL(FNAmtHour01Insurance,0) + ISNULL(FNAmtHour02Insurance,0) + ISNULL(FNAmtHour03Insurance,0) + ISNULL(FNAmtHour04Insurance,0) + ISNULL(FNAmtHour05Insurance,0) + ISNULL(FNAmtHour06Insurance,0) + ISNULL(FNAmtHour07Insurance,0) +ISNULL(FNAmtHour08Insurance,0) )) AS FNAmtNormal "
                    _Qry &= vbCrLf & " , SUM(ISNULL(FNAmtOT,0) - (ISNULL(FNAmtHour09Insurance,0) + ISNULL(FNAmtHour10Insurance,0) +ISNULL(FNAmtHour11Insurance,0)+ISNULL(FNAmtHour12Insurance,0)+ ISNULL(FNAmtHour13Insurance,0) )) AS FNAmtOT "
                    _Qry &= vbCrLf & " , SUM( ISNULL(FNNetAmt,0) "
                    _Qry &= vbCrLf & "   -  (ISNULL(FNAmtHour01Insurance,0) + ISNULL(FNAmtHour02Insurance,0) + ISNULL(FNAmtHour03Insurance,0) + ISNULL(FNAmtHour04Insurance,0) + ISNULL(FNAmtHour05Insurance,0) + ISNULL(FNAmtHour06Insurance,0) + ISNULL(FNAmtHour07Insurance,0) +ISNULL(FNAmtHour08Insurance,0) +ISNULL(FNAmtHour09Insurance,0) + ISNULL(FNAmtHour10Insurance,0) +ISNULL(FNAmtHour11Insurance,0)+ISNULL(FNAmtHour12Insurance,0)+ ISNULL(FNAmtHour13Insurance,0)  ) ) AS FNNetAmt "

                    _Qry &= vbCrLf & ", Sum(FNQAValue) AS FNQAValue"
                    _Qry &= vbCrLf & "  ,MAX(FNAmtFixedIncentive) As FNAmtFixedIncentive "

                    _Qry &= vbCrLf & " , MAX(FNAmtHour01HR) AS FNAmtHour01HR, MAX(FNAmtHour02HR) AS FNAmtHour02HR, MAX(FNAmtHour03HR) AS FNAmtHour03HR, "
                    _Qry &= vbCrLf & "  MAX(FNAmtHour04HR) As FNAmtHour04HR, MAX(FNAmtHour05HR) As FNAmtHour05HR, MAX(FNAmtHour06HR) As FNAmtHour06HR, MAX(FNAmtHour07HR) As FNAmtHour07HR, MAX(FNAmtHour08HR) "
                    _Qry &= vbCrLf & " AS FNAmtHour08HR, MAX(FNAmtHour09HR) AS FNAmtHour09HR, MAX(FNAmtHour10HR) AS FNAmtHour10HR, MAX(FNAmtHour11HR) AS FNAmtHour11HR, MAX(FNAmtHour12HR) AS FNAmtHour12HR "
                    _Qry &= vbCrLf & " , MAX(FNAmtHour13HR) As FNAmtHour13HR "


                    Select Case Val(R!FTIncentiveTypeIdx.ToString)
                        Case 1, 2, 3, 4

                            '_Qry &= vbCrLf & ", Sum(FNAmtHour01NewIncentive+FNAmtHour02NewIncentive+FNAmtHour03NewIncentive+FNAmtHour04NewIncentive+FNAmtHour05NewIncentive+FNAmtHour06NewIncentive+FNAmtHour07NewIncentive+FNAmtHour08NewIncentive) As FNAmtNewIncentive"
                            '_Qry &= vbCrLf & ", Sum(FNAmtHour09NewIncentive+FNAmtHour10NewIncentive+FNAmtHour11NewIncentive+FNAmtHour12NewIncentive+FNAmtHour13NewIncentive) As FNAmtOTNewIncentive,0 As FNProOther"
                            '_Qry &= vbCrLf & ", Sum(FNAmtHour01NewIncentive+FNAmtHour02NewIncentive+FNAmtHour03NewIncentive+FNAmtHour04NewIncentive+FNAmtHour05NewIncentive+FNAmtHour06NewIncentive+FNAmtHour07NewIncentive+FNAmtHour08NewIncentive+FNAmtHour09NewIncentive+FNAmtHour10NewIncentive+FNAmtHour11NewIncentive+FNAmtHour12NewIncentive+FNAmtHour13NewIncentive) As FNNetAmtNewIncentive "

                            _Qry &= vbCrLf & ", Sum(FNAmtNewIncentive) As FNAmtNewIncentive"
                            _Qry &= vbCrLf & ", Sum(FNAmtOTNewIncentive) As FNAmtOTNewIncentive,MAX(ISNULL(XXX.XFNAmt,0))+MAX(ISNULL(XXX.XFNAddAmt2,0)) As FNProOther"
                            _Qry &= vbCrLf & ", Sum(FNNetAmtNewIncentive)+MAX(ISNULL(XXX.XFNAmt,0))+MAX(ISNULL(XXX.XFNAddAmt2,0)) As FNNetAmtNewIncentive "

                            _Qry &= vbCrLf & ",  SUM(FNAmtHour01NewIncentive) As FNAmtHour01Incentive, SUM(FNAmtHour02NewIncentive) As FNAmtHour02Incentive, SUM(FNAmtHour03NewIncentive) As FNAmtHour03Incentive, "
                            _Qry &= vbCrLf & " SUM(FNAmtHour04NewIncentive) As FNAmtHour04Incentive, SUM(FNAmtHour05NewIncentive) As FNAmtHour05Incentive, SUM(FNAmtHour06NewIncentive) As FNAmtHour06Incentive, "
                            _Qry &= vbCrLf & "  SUM(FNAmtHour07NewIncentive) As FNAmtHour07Incentive, SUM(FNAmtHour08NewIncentive) As FNAmtHour08Incentive, SUM(FNAmtHour09NewIncentive) As FNAmtHour09Incentive, "
                            _Qry &= vbCrLf & "  SUM(FNAmtHour10NewIncentive) As FNAmtHour10Incentive, SUM(FNAmtHour11NewIncentive) As FNAmtHour11Incentive, SUM(FNAmtHour12NewIncentive) As EFNAmtHour12Incentive, "
                            _Qry &= vbCrLf & " SUM(FNAmtHour13NewIncentive) As FNAmtHour13Incentive"

                        Case Else
                            '_Qry &= vbCrLf & ", Sum(FNAmtHour01+FNAmtHour02+FNAmtHour03+FNAmtHour05+FNAmtHour05+FNAmtHour06+FNAmtHour07+FNAmtHour08) As FNAmtOldIncentive"
                            '_Qry &= vbCrLf & ", Sum(FNAmtHour09+FNAmtHour10+FNAmtHour11+FNAmtHour12+FNAmtHour13) As FNAmtOTOldIncentive,0 As FNProOther"
                            '_Qry &= vbCrLf & ", Sum(FNAmtHour01+FNAmtHour02+FNAmtHour03+FNAmtHour05+FNAmtHour05+FNAmtHour06+FNAmtHour07+FNAmtHour08+FNAmtHour09+FNAmtHour10+FNAmtHour11+FNAmtHour12+FNAmtHour13) As FNNetAmtOldIncentive"

                            _Qry &= vbCrLf & ", Sum(FNAmtOldIncentive) As FNAmtOldIncentive"
                            _Qry &= vbCrLf & ", Sum(FNAmtOTOldIncentive) As FNAmtOTOldIncentive,MAX(ISNULL(XXX.XFNAmt,0))+MAX(ISNULL(XXX.XFNAddAmt2,0)) As FNProOther"
                            _Qry &= vbCrLf & ", Sum(FNNetAmtOldIncentive)+MAX(ISNULL(XXX.XFNAmt,0))+MAX(ISNULL(XXX.XFNAddAmt2,0)) As FNNetAmtOldIncentive"

                            _Qry &= vbCrLf & ",  SUM(FNAmtHour01) As FNAmtHour01Incentive, SUM(FNAmtHour02) As FNAmtHour02Incentive, SUM(FNAmtHour03) As FNAmtHour03Incentive, "
                            _Qry &= vbCrLf & " SUM(FNAmtHour04) As FNAmtHour04Incentive, SUM(FNAmtHour05) As FNAmtHour05Incentive, SUM(FNAmtHour06) As FNAmtHour06Incentive, "
                            _Qry &= vbCrLf & "  SUM(FNAmtHour07) As FNAmtHour07Incentive, SUM(FNAmtHour08) As FNAmtHour08Incentive, SUM(FNAmtHour09) As FNAmtHour09Incentive, "
                            _Qry &= vbCrLf & "  SUM(FNAmtHour10) As FNAmtHour10Incentive, SUM(FNAmtHour11) As FNAmtHour11Incentive, SUM(FNAmtHour12) As EFNAmtHour12Incentive, "
                            _Qry &= vbCrLf & " SUM(FNAmtHour13) As FNAmtHour13Incentive"

                    End Select

                    _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTIncentive_Emp  As X With(NOLOCK) "
                    _Qry &= vbCrLf & " LEFT OUTER JOIN ( Select  XFTCalDate,XFNHSysEmpID,Case When XFNAmt > XFNAmt2 Then XFNAmt-XFNAmt2 Else 0 End As XFNAmt,XFNAddAmt2"
                    _Qry &= vbCrLf & " FROM(Select FTCalDate As XFTCalDate, FNHSysEmpID As XFNHSysEmpID"
                    _Qry &= vbCrLf & "   ,CASE WHEN FNNetAmtNewIncentive > FNNetAmt THEN FNNetAmtNewIncentive-FNNetAmt ELSE 0 END  AS XFNAmt2"
                    _Qry &= vbCrLf & "	,FNAmt As XFNAmt"
                    _Qry &= vbCrLf & "	,XFNAddAmt"
                    _Qry &= vbCrLf & "	,CASE WHEN XFNAddAmt > FNAmt THEN XFNAddAmt - FNAmt  ELSE 0.00 END AS XFNAddAmt2"
                    _Qry &= vbCrLf & " FROM(SELECT FTCalDate, FNHSysEmpID"
                    _Qry &= vbCrLf & "	,SUM(FNNetAmt) As FNNetAmt"
                    _Qry &= vbCrLf & "	,SUM(FNNetAmtNewIncentive) AS FNNetAmtNewIncentive"
                    _Qry &= vbCrLf & ",SUM(FNAmt) As FNAmt"
                    _Qry &= vbCrLf & ",SUM(FNAddAmt) As XFNAddAmt"
                    _Qry &= vbCrLf & "FROM"
                    _Qry &= vbCrLf & "(SELECT FTCalDate, FNHSysEmpID"
                    _Qry &= vbCrLf & " ,FNNetAmt"
                    _Qry &= vbCrLf & " ,FNNetAmtNewIncentive"
                    _Qry &= vbCrLf & " ,CASE WHEN FNNetAmtNewIncentive > FNNetAmt THEN FNNetAmtNewIncentive-FNNetAmt ELSE 0 END  AS FNAmt"
                    _Qry &= vbCrLf & " ,CASE WHEN FNNetAmtNewIncentive < FNNetAmt THEN FNNetAmt-FNNetAmtNewIncentive ELSE 0 END  AS FNAddAmt"
                    _Qry &= vbCrLf & " FROM(Select FTCalDate, FNHSysEmpID"
                    _Qry &= vbCrLf & " , (ISNULL(FNAmtNormal,0)  - (ISNULL(FNAmtHour01Insurance,0) + ISNULL(FNAmtHour02Insurance,0) + ISNULL(FNAmtHour03Insurance,0) + ISNULL(FNAmtHour04Insurance,0) + ISNULL(FNAmtHour05Insurance,0) + ISNULL(FNAmtHour06Insurance,0) + ISNULL(FNAmtHour07Insurance,0) +ISNULL(FNAmtHour08Insurance,0) )) AS FNAmtNormal "
                    _Qry &= vbCrLf & " , (ISNULL(FNAmtOT,0) - (ISNULL(FNAmtHour09Insurance,0) + ISNULL(FNAmtHour10Insurance,0) +ISNULL(FNAmtHour11Insurance,0)+ISNULL(FNAmtHour12Insurance,0)+ ISNULL(FNAmtHour13Insurance,0) )) As FNAmtOT "
                    _Qry &= vbCrLf & "  , (ISNULL(FNNetAmt,0) -  (ISNULL(FNAmtHour01Insurance,0) + ISNULL(FNAmtHour02Insurance,0) + ISNULL(FNAmtHour03Insurance,0) + ISNULL(FNAmtHour04Insurance,0) + ISNULL(FNAmtHour05Insurance,0) + ISNULL(FNAmtHour06Insurance,0) + ISNULL(FNAmtHour07Insurance,0) +ISNULL(FNAmtHour08Insurance,0) +ISNULL(FNAmtHour09Insurance,0) + ISNULL(FNAmtHour10Insurance,0) +ISNULL(FNAmtHour11Insurance,0)+ISNULL(FNAmtHour12Insurance,0)+ ISNULL(FNAmtHour13Insurance,0)  ) ) AS FNNetAmt "
                    _Qry &= vbCrLf & " ,FNAmtNewIncentive "
                    _Qry &= vbCrLf & " ,FNAmtOTNewIncentive"

                    Select Case Val(R!FTIncentiveTypeIdx.ToString)
                        Case 1, 2, 3, 4
                            _Qry &= vbCrLf & " ,FNNetAmtNewIncentive As FNNetAmtNewIncentive"
                        Case Else
                            _Qry &= vbCrLf & " ,FNNetAmtOldIncentive As FNNetAmtNewIncentive"
                    End Select

                    _Qry &= vbCrLf & " From THRTIncentive_Emp As X With( NOLOCK)"
                    _Qry &= vbCrLf & "  WHERE  FNHSysEmpID=" & Integer.Parse(Val(Rt!FNHSysEmpID.ToString)) & ""
                    _Qry &= vbCrLf & "       And FTCalDate='" & _CalDate & "'"
                    _Qry &= vbCrLf & ") As X) As X1"
                    _Qry &= vbCrLf & "Group By FTCalDate, FNHSysEmpID"
                    _Qry &= vbCrLf & " ) AS ZZ) AS ZZZ2 ) XXX ON X.FTCalDate=XXX.XFTCalDate AND X.FNHSysEmpID=XXX.XFNHSysEmpID "


                    _Qry &= vbCrLf & "  WHERE  FNHSysEmpID=" & Integer.Parse(Val(Rt!FNHSysEmpID.ToString)) & ""
                    _Qry &= vbCrLf & "       And FTCalDate='" & _CalDate & "'"


                    HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

                    _Qry = " UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTWageDaily SET "
                    _Qry &= vbCrLf & " FNAmtNormal = CASE WHEN ISNULL(FNAmtNormal,0)<=0 THEN  0 ELSE ISNULL(FNAmtNormal,0) END "
                    _Qry &= vbCrLf & ", FNAmtOT= CASE WHEN ISNULL(FNAmtOT,0)<=0 THEN  0 ELSE ISNULL(FNAmtOT,0) END "
                    _Qry &= vbCrLf & " , FNNetAmt = (CASE WHEN ISNULL(FNAmtNormal,0)<=0 THEN  0 ELSE ISNULL(FNAmtNormal,0) END) + (CASE WHEN ISNULL(FNAmtOT,0)<=0 THEN  0 ELSE ISNULL(FNAmtOT,0) END) "
                    _Qry &= vbCrLf & "  WHERE  FNHSysEmpID=" & Integer.Parse(Val(Rt!FNHSysEmpID.ToString)) & ""
                    _Qry &= vbCrLf & "       And FTDateTrans='" & _CalDate & "'"

                    HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

                Next

                _dtemptime.Dispose()

            Next

            If _dt.Select("FTSelect='1' AND FNHSysEmpID>0").Length > 0 Then
                Call CalculateEmpWring(_dt)
            End If

            _dt.Dispose()
        Catch ex As Exception
        End Try
        Return True
    End Function


    Private Function CalculateCutBU(Spls As HI.TL.SplashScreen) As Boolean

        Dim _State As Boolean = False
        Dim _dt As DataTable
        Dim _UnitSectAmt As Double = 0
        Dim _UnitSectIncentiveAmt As Double = 0
        Dim _dtemptime As New DataTable
        Dim _TotalEmp As Integer = 0
        Dim _TotalTime As Integer = 0
        Dim _TotalTimeOT As Integer = 0
        Dim _TotalTimeHR As Integer = 0
        Dim _TotalTimeOTHR As Integer = 0
        Dim _Salary As Double = 0
        Dim _SalaryPerH As Double = 0
        Dim _SalaryPerOT As Double = 0
        Dim _CountEmp As Integer = 0
        Dim _FNAmtNormal As Double = 0
        Dim _FNAmtOT As Double = 0
        Dim _FNNetAmt As Double = 0
        Dim _FixHour As Integer = 0
        Dim _CountEmpIncentive As Integer
        Dim _TotalEmpCal As Integer = 0
        Dim _EmpIncentiveAmt As Double = 0
        Dim _TotalTimeMin As Integer = 0
        Dim _TotalEmpAmt As Double = 0
        Dim _CountLineSew As Integer = 0

        Dim _Qry As String = ""
        With CType(Me.ogc1.DataSource, DataTable)
            .AcceptChanges()
            _dt = .Copy

            .AcceptChanges()
        End With

        Dim grpfodata As List(Of Integer) = (_dt.Select("FTSelect='1'", "FNHSysUnitSectId").CopyToDataTable).AsEnumerable() _
        .Select(Function(r) r.Field(Of Integer)("FNHSysUnitSectId")) _
        .Distinct() _
        .ToList()

        For Each _UnitSect As Integer In grpfodata



            Dim grpfodataDate As List(Of String) = (_dt.Select("FTSelect='1' AND FNHSysUnitSectId =" & Val(_UnitSect) & "", "FDScanDateOrg").CopyToDataTable).AsEnumerable() _
             .Select(Function(r) r.Field(Of String)("FDScanDateOrg")) _
             .Distinct() _
             .ToList()

            For Each _Date As String In grpfodataDate

                '_Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTIncentive "
                '_Qry &= vbCrLf & " WHERE  (FTCalDate = '" & HI.UL.ULDate.ConvertEnDB(_Date) & "') "
                '_Qry &= vbCrLf & "   AND (FNHSysUnitSectId = " & Integer.Parse(Val(_UnitSect)) & ")"
                'HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

                '_Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTIncentive_Emp "
                '_Qry &= vbCrLf & " WHERE  (FTCalDate = '" & HI.UL.ULDate.ConvertEnDB(_Date) & "') "
                '_Qry &= vbCrLf & "   AND (FNHSysUnitSectId = " & Integer.Parse(Val(_UnitSect)) & ")"
                'HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)


                _dtemptime = GetEmpTime(Integer.Parse(Val(_UnitSect)), _Date).Copy()

                _UnitSectAmt = 0
                _UnitSectIncentiveAmt = 0
                _TotalEmpAmt = 0
                For Each Rx As DataRow In _dt.Select("FNHSysUnitSectId=" & Val(_UnitSect) & " AND FDScanDateOrg='" & HI.UL.ULDate.ConvertEnDB(_Date) & "'")

                    _UnitSectAmt = _UnitSectAmt + Val(Rx!FNIncentiveAmt.ToString)
                    _CountLineSew += +1
                Next
                _TotalEmp = 0
                Try
                    _TotalEmp = _dtemptime.Select(" FNTotalWorkingMin>0 and FTStateDaily='0' and FNSalary >0 ").Length
                Catch ex As Exception

                End Try

                _UnitSectIncentiveAmt = (Double.Parse(Format(_UnitSectAmt / _CountLineSew, "0.00")) * _TotalEmp)



                '-----ตรวจสอบประกันรายชั่วโมง ประกันรายวัน ------------------

                _CountEmp = 0
                _Salary = 0
                _SalaryPerH = 0
                _SalaryPerOT = 0
                _FNAmtNormal = 0
                _FNAmtOT = 0
                _FNNetAmt = 0
                'Calculate Wage HR Normal
                _TotalTime = 0
                _TotalTimeOT = 0
                _TotalTimeMin = 0
                For Each Rt As DataRow In _dtemptime.Rows
                    _CountEmp = _CountEmp + 1
                    _TotalTime = _TotalTime + Integer.Parse(Val(Rt!FNWorkingMin.ToString))
                    _TotalTimeOT = _TotalTimeOT + Integer.Parse(Val(Rt!FNWorkingOTMin.ToString))

                    _TotalTimeHR = Integer.Parse(Val(Rt!FNTimeMin.ToString))
                    _TotalTimeOTHR = Integer.Parse(Val(Rt!FNOT1Min.ToString))


                    If Rt!FTStateDaily.ToString = "0" Then
                        If Double.Parse("0" & Rt!FNSalary.ToString) > 0 Then
                            _TotalTimeMin = _TotalTimeMin + (Val(Integer.Parse(Val(Rt!FNTotalWorkingMin.ToString))))
                        End If
                    End If


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
                    _Qry &= vbCrLf & ")"
                    _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & ""
                    _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & ""
                    _Qry &= vbCrLf & " ,'" & _Date & "'   "
                    _Qry &= vbCrLf & " ," & Integer.Parse(Val(_UnitSect)) & ""
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

                    HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

                Next

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
                _Qry &= vbCrLf & " ,'" & _Date & "'   "
                _Qry &= vbCrLf & " ," & Integer.Parse(Val(_UnitSect)) & ""
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,'0'"
                _Qry &= vbCrLf & " ,'0'"
                _Qry &= vbCrLf & " ,'0'"
                _Qry &= vbCrLf & " ,'0'"
                _Qry &= vbCrLf & " ,'0'"
                _Qry &= vbCrLf & " ,'0'"
                _Qry &= vbCrLf & " ,'0'"
                _Qry &= vbCrLf & " ,'0'"
                _Qry &= vbCrLf & " ,'0'"
                _Qry &= vbCrLf & " ,'0'"
                _Qry &= vbCrLf & " ,'0'"
                _Qry &= vbCrLf & " ,'0'"
                _Qry &= vbCrLf & " ,'0'"
                _Qry &= vbCrLf & " ,'0'"
                _Qry &= vbCrLf & " ," & _CountEmp & ""
                _Qry &= vbCrLf & " ," & _TotalTime + _TotalTimeOT & ""
                _Qry &= vbCrLf & " ," & _UnitSectIncentiveAmt & ""
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ," & _UnitSectIncentiveAmt & ""
                _Qry &= vbCrLf & " ,-1"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ," & _UnitSectIncentiveAmt & ""
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"

                HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

                _CountEmpIncentive = _dtemptime.Select("FNTotalWorkingMin>0").Length


                For Each Rt As DataRow In _dtemptime.Select("FTStateCal <>'1'  AND FNTotalWorkingMin>0 ", "FNTotalWorkingMin")
                    _TotalEmpCal = _TotalEmpCal + 1

                    _Salary = Double.Parse(Val(Rt!FNSalary.ToString))
                    _SalaryPerH = Double.Parse(Format(_Salary / 8, "0.00"))
                    _SalaryPerOT = Double.Parse(Format(_SalaryPerH * 1.5, "0.00"))

                    If _FixHour > 0 Then
                    End If

                    'If _TotalEmpCal = _CountEmpIncentive Then
                    '    _EmpIncentiveAmt = _UnitSectIncentiveAmt - _TotalEmpAmt
                    'Else
                    '    _EmpIncentiveAmt = CDbl(Format((_UnitSectIncentiveAmt * (Integer.Parse(Val(Rt!FNTotalWorkingMin.ToString)))) / _TotalTimeMin, "0.00"))
                    'End If
                    _EmpIncentiveAmt = CDbl(Format((_UnitSectIncentiveAmt * (Integer.Parse(Val(Rt!FNTotalWorkingMin.ToString)))) / _TotalTimeMin, "0.00"))
                    If Rt!FTStateDaily.ToString = "1" Then
                        _EmpIncentiveAmt = 0
                    End If

                    _TotalEmpAmt = _TotalEmpAmt + _EmpIncentiveAmt

                    _FNAmtOT = Double.Parse(Format(((_Salary / 480) * 0.5) * Integer.Parse(Val(Rt!FNWorkingOTMin.ToString)), "0.00"))

                    _FNNetAmt = _EmpIncentiveAmt + _FNAmtOT

                    '_Qry = " UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTIncentive_Emp "
                    '_Qry &= vbCrLf & "  SET  FNAmtOldIncentive=" & _EmpIncentiveAmt & ""
                    '_Qry &= vbCrLf & "  , FNAmtOTOldIncentive=" & _FNAmtOT & ""
                    '_Qry &= vbCrLf & "  , FNNetAmtOldIncentive=" & (_FNNetAmt) & ""
                    '_Qry &= vbCrLf & "   ,FNQAValue=0 "
                    '_Qry &= vbCrLf & "   WHERE FNHSysUnitSectId=" & Integer.Parse(Val(_UnitSect)) & ""
                    '_Qry &= vbCrLf & "       AND FNHSysEmpID=" & Integer.Parse(Val(Rt!FNHSysEmpID.ToString)) & ""
                    '_Qry &= vbCrLf & "       AND FTCalDate='" & _Date & "'"

                    'FNAmtNormal, FNAmtOT, FNNetAmt
                    _Qry = " UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTIncentive_Emp "
                    _Qry &= vbCrLf & "  SET  FNAmtOldIncentive=" & _EmpIncentiveAmt & ""
                    _Qry &= vbCrLf & "  , FNAmtOTOldIncentive=0" ' & _FNAmtOT & ""
                    _Qry &= vbCrLf & "  , FNNetAmtOldIncentive=" & (_EmpIncentiveAmt) & ""
                    _Qry &= vbCrLf & "   ,FNQAValue=0 "
                    _Qry &= vbCrLf & "   ,FNNetAmt=FNNetAmt+" & (_EmpIncentiveAmt) & ""
                    _Qry &= vbCrLf & "   WHERE FNHSysUnitSectId=" & Integer.Parse(Val(_UnitSect)) & ""
                    _Qry &= vbCrLf & "       AND FNHSysEmpID=" & Integer.Parse(Val(Rt!FNHSysEmpID.ToString)) & ""
                    _Qry &= vbCrLf & "       AND FTCalDate='" & _Date & "'"

                    HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)








                    _Qry = " DELETE FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTWageDaily "
                    _Qry &= vbCrLf & "  WHERE  FNHSysEmpID=" & Integer.Parse(Val(Rt!FNHSysEmpID.ToString)) & ""
                    _Qry &= vbCrLf & "    AND  FTDateTrans='" & _Date & "'"
                    HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

                    _Qry = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTWageDaily ( "
                    _Qry &= vbCrLf & "   FTInsUser, FTInsDate, FTInsTime, FNHSysEmpID, FTDateTrans"
                    _Qry &= vbCrLf & " , FNAmtNormal, FNAmtOT, FNNetAmt, FNProNormal, FNProOT"
                    _Qry &= vbCrLf & " , FNProOther, FNNetProAmt,FNQAAmt"
                    _Qry &= vbCrLf & " )"
                    _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & ""
                    _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & ""
                    _Qry &= vbCrLf & " ," & Integer.Parse(Val(Rt!FNHSysEmpID.ToString)) & ""
                    _Qry &= vbCrLf & " ,'" & _Date & "'   "
                    _Qry &= vbCrLf & "  ,MAX(FNAmtNormal) AS FNAmtNormal"
                    _Qry &= vbCrLf & " , MAX(FNAmtOT) AS FNAmtOT"
                    _Qry &= vbCrLf & " , MAX(FNNetAmt) AS FNNetAmt"
                    _Qry &= vbCrLf & ", Sum(FNAmtOldIncentive) AS FNAmtOldIncentive"
                    _Qry &= vbCrLf & ", Sum(FNAmtOTOldIncentive) AS FNAmtOTOldIncentive,0 AS FNProOther"
                    _Qry &= vbCrLf & ", Sum(FNNetAmtOldIncentive) AS FNNetAmtOldIncentive"
                    _Qry &= vbCrLf & ", Sum(FNQAValue) AS FNQAValue"
                    _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTIncentive_Emp  AS X WITH(NOLOCK) "
                    _Qry &= vbCrLf & "  WHERE  FNHSysEmpID=" & Integer.Parse(Val(Rt!FNHSysEmpID.ToString)) & ""
                    _Qry &= vbCrLf & "       AND FTCalDate='" & _Date & "'"

                    HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

                Next



                If _UnitSectIncentiveAmt > _TotalEmpAmt Then

                    Dim MaxMinute As Integer = 0
                    Dim SPareAmont As Decimal = _UnitSectIncentiveAmt - _TotalEmpAmt


                    For Each Rt As DataRow In _dtemptime.Select("FTStateCal<>'1'  AND FNTotalWorkingMin>0 ", "FNTotalWorkingMin ")
                        MaxMinute = Integer.Parse(Val(Rt!FNTotalWorkingMin.ToString))


                    Next

                    If MaxMinute > 0 Then
                        _TotalEmpCal = 0
                        For Each Rt As DataRow In _dtemptime.Select("FTStateCal<>'1'  AND FNTotalWorkingMin>=" & MaxMinute & " ", "FNTotalWorkingMin ")
                            _TotalEmpCal = _TotalEmpCal + 1
                        Next
                        Dim EmpSPareAmont As Decimal = CDbl(Format(SPareAmont / _TotalEmpCal, "0.00"))


                        If (EmpSPareAmont * _TotalEmpCal) < SPareAmont Then
                            EmpSPareAmont = EmpSPareAmont + 0.01
                        End If


                        For Each Rt As DataRow In _dtemptime.Select("FTStateCal<>'1'  AND FNTotalWorkingMin>=" & MaxMinute & " ", "FNTotalWorkingMin ")

                            _Qry = " UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTIncentive_Emp "
                            _Qry &= vbCrLf & "  SET  FNAmtOldIncentive=FNAmtOldIncentive+" & EmpSPareAmont & ""
                            _Qry &= vbCrLf & "  , FNNetAmtOldIncentive=FNNetAmtOldIncentive+" & EmpSPareAmont & ""
                            _Qry &= vbCrLf & "   WHERE FNHSysUnitSectId=" & _UnitSect & ""
                            _Qry &= vbCrLf & "       AND FNHSysEmpID=" & Integer.Parse(Val(Rt!FNHSysEmpID.ToString)) & ""
                            _Qry &= vbCrLf & "       AND FTCalDate='" & _Date & "'"

                            HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)
                        Next

                    End If



                End If



            Next

        Next




        Return True

    End Function


    Private Function CalculateStockFabric(Spls As HI.TL.SplashScreen) As Boolean

        Dim _State As Boolean = False
        Dim _dt As DataTable
        Dim _UnitSectAmt As Double = 0
        Dim _UnitSectIncentiveAmt As Double = 0
        Dim _dtemptime As DataTable
        Dim _TotalEmp As Integer = 0
        Dim _TotalTime As Integer = 0
        Dim _TotalTimeOT As Integer = 0
        Dim _TotalTimeHR As Integer = 0
        Dim _TotalTimeOTHR As Integer = 0
        Dim _Salary As Double = 0
        Dim _SalaryPerH As Double = 0
        Dim _SalaryPerOT As Double = 0
        Dim _CountEmp As Integer = 0
        Dim _FNAmtNormal As Double = 0
        Dim _FNAmtOT As Double = 0
        Dim _FNNetAmt As Double = 0
        Dim _FixHour As Integer = 0
        Dim _CountEmpIncentive As Integer
        Dim _TotalEmpCal As Integer = 0
        Dim _EmpIncentiveAmt As Double = 0
        Dim _TotalTimeMin As Integer = 0
        Dim _TotalEmpAmt As Double = 0



        Dim _Qry As String = ""
        With CType(Me.ogc2.DataSource, DataTable)
            .AcceptChanges()
            _dt = .Copy

            .AcceptChanges()
        End With

        Dim grpfodata As List(Of Integer) = (_dt.Select("FTSelect='1'", "FNHSysUnitSectId").CopyToDataTable).AsEnumerable() _
        .Select(Function(r) r.Field(Of Integer)("FNHSysUnitSectId")) _
        .Distinct() _
        .ToList()

        For Each _UnitSect As Integer In grpfodata



            Dim grpfodataDate As List(Of String) = (_dt.Select("FTSelect='1' AND FNHSysUnitSectId =" & Val(_UnitSect) & "", "FDScanDateOrg").CopyToDataTable).AsEnumerable() _
             .Select(Function(r) r.Field(Of String)("FDScanDateOrg")) _
             .Distinct() _
             .ToList()

            For Each _Date As String In grpfodataDate

                '_Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTIncentive "
                '_Qry &= vbCrLf & " WHERE  (FTCalDate = '" & HI.UL.ULDate.ConvertEnDB(_Date) & "') "
                '_Qry &= vbCrLf & "   AND (FNHSysUnitSectId = " & Integer.Parse(Val(_UnitSect)) & ")"
                'HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

                '_Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTIncentive_Emp "
                '_Qry &= vbCrLf & " WHERE  (FTCalDate = '" & HI.UL.ULDate.ConvertEnDB(_Date) & "') "
                '_Qry &= vbCrLf & "   AND (FNHSysUnitSectId = " & Integer.Parse(Val(_UnitSect)) & ")"
                'HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)


                _dtemptime = GetEmpTime(Integer.Parse(Val(_UnitSect)), _Date).Copy()

                _UnitSectAmt = 0
                _UnitSectIncentiveAmt = 0
                For Each Rx As DataRow In _dt.Select("FNHSysUnitSectId=" & Val(_UnitSect) & " AND FDScanDateOrg='" & HI.UL.ULDate.ConvertEnDB(_Date) & "'")

                    _UnitSectAmt = _UnitSectAmt + Val(Rx!FNNetIncentiveAmt.ToString)

                Next
                _TotalEmp = 0
                Try
                    _TotalEmp = _dtemptime.Select(" FNTotalWorkingMin>=0").Length
                Catch ex As Exception

                End Try

                _UnitSectIncentiveAmt = (Double.Parse(Format(_UnitSectAmt, "0.00")) * _TotalEmp)


                '-----ตรวจสอบประกันรายชั่วโมง ประกันรายวัน ------------------

                _CountEmp = 0
                _Salary = 0
                _SalaryPerH = 0
                _SalaryPerOT = 0
                _FNAmtNormal = 0
                _FNAmtOT = 0
                _FNNetAmt = 0
                'Calculate Wage HR Normal
                _TotalTime = 0
                _TotalTimeOT = 0
                _TotalTimeMin = 0
                For Each Rt As DataRow In _dtemptime.Rows
                    _CountEmp = _CountEmp + 1
                    _TotalTime = _TotalTime + Integer.Parse(Val(Rt!FNWorkingMin.ToString))
                    _TotalTimeOT = _TotalTimeOT + Integer.Parse(Val(Rt!FNWorkingOTMin.ToString))

                    _TotalTimeHR = Integer.Parse(Val(Rt!FNTimeMin.ToString))
                    _TotalTimeOTHR = Integer.Parse(Val(Rt!FNOT1Min.ToString))


                    _TotalTimeMin = _TotalTimeMin + (Val(Integer.Parse(Val(Rt!FNTotalWorkingMin.ToString))))

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
                    _Qry &= vbCrLf & ")"
                    _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & ""
                    _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & ""
                    _Qry &= vbCrLf & " ,'" & _Date & "'   "
                    _Qry &= vbCrLf & " ," & Integer.Parse(Val(_UnitSect)) & ""
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

                    HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

                Next

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
                _Qry &= vbCrLf & " ,'" & _Date & "'   "
                _Qry &= vbCrLf & " ," & Integer.Parse(Val(_UnitSect)) & ""
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,'0'"
                _Qry &= vbCrLf & " ,'0'"
                _Qry &= vbCrLf & " ,'0'"
                _Qry &= vbCrLf & " ,'0'"
                _Qry &= vbCrLf & " ,'0'"
                _Qry &= vbCrLf & " ,'0'"
                _Qry &= vbCrLf & " ,'0'"
                _Qry &= vbCrLf & " ,'0'"
                _Qry &= vbCrLf & " ,'0'"
                _Qry &= vbCrLf & " ,'0'"
                _Qry &= vbCrLf & " ,'0'"
                _Qry &= vbCrLf & " ,'0'"
                _Qry &= vbCrLf & " ,'0'"
                _Qry &= vbCrLf & " ,'0'"
                _Qry &= vbCrLf & " ," & _CountEmp & ""
                _Qry &= vbCrLf & " ," & _TotalTime + _TotalTimeOT & ""
                _Qry &= vbCrLf & " ," & _UnitSectIncentiveAmt & ""
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ," & _UnitSectIncentiveAmt & ""
                _Qry &= vbCrLf & " ,-1"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ," & _UnitSectIncentiveAmt & ""
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"

                HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

                _CountEmpIncentive = _dtemptime.Select("FNTotalWorkingMin>0").Length

                For Each Rt As DataRow In _dtemptime.Select("FTStateCal<>'1'  AND FNTotalWorkingMin>0 ", "FNTotalWorkingMin")
                    _TotalEmpCal = _TotalEmpCal + 1

                    _Salary = Double.Parse(Val(Rt!FNSalary.ToString))
                    _SalaryPerH = Double.Parse(Format(_Salary / 8, "0.00"))
                    _SalaryPerOT = Double.Parse(Format(_SalaryPerH * 1.5, "0.00"))

                    If _FixHour > 0 Then
                    End If

                    If _TotalEmpCal = _CountEmpIncentive Then
                        _EmpIncentiveAmt = _UnitSectIncentiveAmt - _TotalEmpAmt
                    Else
                        _EmpIncentiveAmt = CDbl(Format((_UnitSectIncentiveAmt * (Integer.Parse(Val(Rt!FNTotalWorkingMin.ToString)))) / _TotalTimeMin, "0.00"))
                    End If

                    _TotalEmpAmt = _TotalEmpAmt + _EmpIncentiveAmt

                    _FNAmtOT = Double.Parse(Format(((_Salary / 480) * 0.5) * Integer.Parse(Val(Rt!FNWorkingOTMin.ToString)), "0.00"))

                    _FNNetAmt = _EmpIncentiveAmt + _FNAmtOT

                    'FNAmtNormal, FNAmtOT, FNNetAmt
                    _Qry = " UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTIncentive_Emp "
                    _Qry &= vbCrLf & "  SET  FNAmtOldIncentive=FNAmtNormal+" & _EmpIncentiveAmt & ""
                    _Qry &= vbCrLf & "  , FNAmtOTOldIncentive=FNAmtOT+" & _FNAmtOT & ""
                    _Qry &= vbCrLf & "  , FNNetAmtOldIncentive=FNNetAmt+" & (_FNNetAmt) & ""
                    _Qry &= vbCrLf & "   ,FNQAValue=0 "
                    _Qry &= vbCrLf & "   WHERE FNHSysUnitSectId=" & Integer.Parse(Val(_UnitSect)) & ""
                    _Qry &= vbCrLf & "       AND FNHSysEmpID=" & Integer.Parse(Val(Rt!FNHSysEmpID.ToString)) & ""
                    _Qry &= vbCrLf & "       AND FTCalDate='" & _Date & "'"

                    HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

                    _Qry = " DELETE FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTWageDaily "
                    _Qry &= vbCrLf & "  WHERE  FNHSysEmpID=" & Integer.Parse(Val(Rt!FNHSysEmpID.ToString)) & ""
                    _Qry &= vbCrLf & "    AND  FTDateTrans='" & _Date & "'"
                    HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

                    _Qry = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTWageDaily ( "
                    _Qry &= vbCrLf & "   FTInsUser, FTInsDate, FTInsTime, FNHSysEmpID, FTDateTrans"
                    _Qry &= vbCrLf & " , FNAmtNormal, FNAmtOT, FNNetAmt, FNProNormal, FNProOT"
                    _Qry &= vbCrLf & " , FNProOther, FNNetProAmt,FNQAAmt"
                    _Qry &= vbCrLf & " )"
                    _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & ""
                    _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & ""
                    _Qry &= vbCrLf & " ," & Integer.Parse(Val(Rt!FNHSysEmpID.ToString)) & ""
                    _Qry &= vbCrLf & " ,'" & _Date & "'   "
                    _Qry &= vbCrLf & "  ,MAX(FNAmtNormal) AS FNAmtNormal"
                    _Qry &= vbCrLf & " , MAX(FNAmtOT) AS FNAmtOT"
                    _Qry &= vbCrLf & " , MAX(FNNetAmt) AS FNNetAmt"
                    _Qry &= vbCrLf & ", Sum(FNAmtOldIncentive) AS FNAmtOldIncentive"
                    _Qry &= vbCrLf & ", Sum(FNAmtOTOldIncentive) AS FNAmtOTOldIncentive,0 AS FNProOther"
                    _Qry &= vbCrLf & ", Sum(FNNetAmtOldIncentive) AS FNNetAmtOldIncentive"
                    _Qry &= vbCrLf & ", Sum(FNQAValue) AS FNQAValue"
                    _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTIncentive_Emp  AS X WITH(NOLOCK) "
                    _Qry &= vbCrLf & "  WHERE  FNHSysEmpID=" & Integer.Parse(Val(Rt!FNHSysEmpID.ToString)) & ""
                    _Qry &= vbCrLf & "       AND FTCalDate='" & _Date & "'"

                    HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

                Next

            Next

        Next

        Return True

    End Function

    Private Function CalculateCutAuto(Spls As HI.TL.SplashScreen) As Boolean

        Dim _State As Boolean = False
        Dim _dt As DataTable
        Dim _UnitSectAmt As Double = 0
        Dim _UnitSectIncentiveAmt As Double = 0
        Dim _dtemptime As DataTable
        Dim _TotalEmp As Integer = 0
        Dim _TotalTime As Integer = 0
        Dim _TotalTimeOT As Integer = 0
        Dim _TotalTimeHR As Integer = 0
        Dim _TotalTimeOTHR As Integer = 0
        Dim _Salary As Double = 0
        Dim _SalaryPerH As Double = 0
        Dim _SalaryPerOT As Double = 0
        Dim _CountEmp As Integer = 0
        Dim _FNAmtNormal As Double = 0
        Dim _FNAmtOT As Double = 0
        Dim _FNNetAmt As Double = 0
        Dim _FixHour As Integer = 0
        Dim _CountEmpIncentive As Integer
        Dim _TotalEmpCal As Integer = 0
        Dim _EmpIncentiveAmt As Double = 0
        Dim _TotalTimeMin As Integer = 0
        Dim _TotalEmpAmt As Double = 0

        Dim _Qry As String = ""
        With CType(Me.ogc3.DataSource, DataTable)
            .AcceptChanges()
            _dt = .Copy

            .AcceptChanges()
        End With

        Dim grpfodata As List(Of Integer) = (_dt.Select("FTSelect='1'", "FNHSysUnitSectId").CopyToDataTable).AsEnumerable() _
        .Select(Function(r) r.Field(Of Integer)("FNHSysUnitSectId")) _
        .Distinct() _
        .ToList()

        For Each _UnitSect As Integer In grpfodata



            Dim grpfodataDate As List(Of String) = (_dt.Select("FTSelect='1' AND FNHSysUnitSectId =" & Val(_UnitSect) & "", "FDScanDateOrg").CopyToDataTable).AsEnumerable() _
             .Select(Function(r) r.Field(Of String)("FDScanDateOrg")) _
             .Distinct() _
             .ToList()

            For Each _Date As String In grpfodataDate

                '_Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTIncentive "
                '_Qry &= vbCrLf & " WHERE  (FTCalDate = '" & HI.UL.ULDate.ConvertEnDB(_Date) & "') "
                '_Qry &= vbCrLf & "   AND (FNHSysUnitSectId = " & Integer.Parse(Val(_UnitSect)) & ")"
                'HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

                '_Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTIncentive_Emp "
                '_Qry &= vbCrLf & " WHERE  (FTCalDate = '" & HI.UL.ULDate.ConvertEnDB(_Date) & "') "
                '_Qry &= vbCrLf & "   AND (FNHSysUnitSectId = " & Integer.Parse(Val(_UnitSect)) & ")"
                'HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)


                _dtemptime = GetEmpTime(Integer.Parse(Val(_UnitSect)), _Date).Copy()

                _UnitSectAmt = 0
                _UnitSectIncentiveAmt = 0
                For Each Rx As DataRow In _dt.Select("FNHSysUnitSectId=" & Val(_UnitSect) & " AND FDScanDateOrg='" & HI.UL.ULDate.ConvertEnDB(_Date) & "'")

                    _UnitSectAmt = _UnitSectAmt + Val(Rx!FNIncentiveAmt.ToString)

                Next
                _TotalEmp = 0
                Try
                    _TotalEmp = _dtemptime.Select(" FNTotalWorkingMin>=0").Length
                Catch ex As Exception

                End Try

                _UnitSectIncentiveAmt = _UnitSectAmt '(Double.Parse(Format(_UnitSectAmt / 3.0, "0.00")) * _TotalEmp)



                '-----ตรวจสอบประกันรายชั่วโมง ประกันรายวัน ------------------

                _CountEmp = 0
                _Salary = 0
                _SalaryPerH = 0
                _SalaryPerOT = 0
                _FNAmtNormal = 0
                _FNAmtOT = 0
                _FNNetAmt = 0
                'Calculate Wage HR Normal
                _TotalTime = 0
                _TotalTimeOT = 0
                _TotalTimeMin = 0
                For Each Rt As DataRow In _dtemptime.Rows
                    _CountEmp = _CountEmp + 1
                    _TotalTime = _TotalTime + Integer.Parse(Val(Rt!FNWorkingMin.ToString))
                    _TotalTimeOT = _TotalTimeOT + Integer.Parse(Val(Rt!FNWorkingOTMin.ToString))

                    _TotalTimeHR = Integer.Parse(Val(Rt!FNTimeMin.ToString))
                    _TotalTimeOTHR = Integer.Parse(Val(Rt!FNOT1Min.ToString))


                    _TotalTimeMin = _TotalTimeMin + (Val(Integer.Parse(Val(Rt!FNTotalWorkingMin.ToString))))

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
                    _Qry &= vbCrLf & ")"
                    _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & ""
                    _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & ""
                    _Qry &= vbCrLf & " ,'" & _Date & "'   "
                    _Qry &= vbCrLf & " ," & Integer.Parse(Val(_UnitSect)) & ""
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

                    HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

                Next

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
                _Qry &= vbCrLf & " ,'" & _Date & "'   "
                _Qry &= vbCrLf & " ," & Integer.Parse(Val(_UnitSect)) & ""
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,'0'"
                _Qry &= vbCrLf & " ,'0'"
                _Qry &= vbCrLf & " ,'0'"
                _Qry &= vbCrLf & " ,'0'"
                _Qry &= vbCrLf & " ,'0'"
                _Qry &= vbCrLf & " ,'0'"
                _Qry &= vbCrLf & " ,'0'"
                _Qry &= vbCrLf & " ,'0'"
                _Qry &= vbCrLf & " ,'0'"
                _Qry &= vbCrLf & " ,'0'"
                _Qry &= vbCrLf & " ,'0'"
                _Qry &= vbCrLf & " ,'0'"
                _Qry &= vbCrLf & " ,'0'"
                _Qry &= vbCrLf & " ,'0'"
                _Qry &= vbCrLf & " ," & _CountEmp & ""
                _Qry &= vbCrLf & " ," & _TotalTime + _TotalTimeOT & ""
                _Qry &= vbCrLf & " ," & _UnitSectIncentiveAmt & ""
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ," & _UnitSectIncentiveAmt & ""
                _Qry &= vbCrLf & " ,-1"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ," & _UnitSectIncentiveAmt & ""
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"

                HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

                _CountEmpIncentive = _dtemptime.Select("FNTotalWorkingMin>0").Length


                For Each Rt As DataRow In _dtemptime.Select("FTStateCal<>'1'  AND FNTotalWorkingMin>0 ", "FNTotalWorkingMin")
                    _TotalEmpCal = _TotalEmpCal + 1

                    _Salary = Double.Parse(Val(Rt!FNSalary.ToString))
                    _SalaryPerH = Double.Parse(Format(_Salary / 8, "0.00"))
                    _SalaryPerOT = Double.Parse(Format(_SalaryPerH * 1.5, "0.00"))

                    If _FixHour > 0 Then
                    End If

                    If _TotalEmpCal = _CountEmpIncentive Then
                        _EmpIncentiveAmt = _UnitSectIncentiveAmt - _TotalEmpAmt
                    Else
                        _EmpIncentiveAmt = CDbl(Format((_UnitSectIncentiveAmt * (Integer.Parse(Val(Rt!FNTotalWorkingMin.ToString)))) / _TotalTimeMin, "0.00"))
                    End If

                    _TotalEmpAmt = _TotalEmpAmt + _EmpIncentiveAmt

                    _FNAmtOT = Double.Parse(Format(((_Salary / 480) * 0.5) * Integer.Parse(Val(Rt!FNWorkingOTMin.ToString)), "0.00"))

                    _FNNetAmt = _EmpIncentiveAmt + _FNAmtOT

                    '_Qry = " UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTIncentive_Emp "
                    '_Qry &= vbCrLf & "  SET  FNAmtOldIncentive=" & _EmpIncentiveAmt & ""
                    '_Qry &= vbCrLf & "  , FNAmtOTOldIncentive=" & _FNAmtOT & ""
                    '_Qry &= vbCrLf & "  , FNNetAmtOldIncentive=" & (_FNNetAmt) & ""
                    '_Qry &= vbCrLf & "   ,FNQAValue=0 "
                    '_Qry &= vbCrLf & "   WHERE FNHSysUnitSectId=" & Integer.Parse(Val(_UnitSect)) & ""
                    '_Qry &= vbCrLf & "       AND FNHSysEmpID=" & Integer.Parse(Val(Rt!FNHSysEmpID.ToString)) & ""
                    '_Qry &= vbCrLf & "       AND FTCalDate='" & _Date & "'"


                    'FNAmtNormal, FNAmtOT, FNNetAmt
                    _Qry = " UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTIncentive_Emp "
                    _Qry &= vbCrLf & "  SET  FNAmtOldIncentive=FNAmtNormal+" & _EmpIncentiveAmt & ""
                    _Qry &= vbCrLf & "  , FNAmtOTOldIncentive=FNAmtOT+" & _FNAmtOT & ""
                    _Qry &= vbCrLf & "  , FNNetAmtOldIncentive=FNNetAmt+" & (_FNNetAmt) & ""
                    _Qry &= vbCrLf & "   ,FNQAValue=0 "
                    _Qry &= vbCrLf & "   WHERE FNHSysUnitSectId=" & Integer.Parse(Val(_UnitSect)) & ""
                    _Qry &= vbCrLf & "       AND FNHSysEmpID=" & Integer.Parse(Val(Rt!FNHSysEmpID.ToString)) & ""
                    _Qry &= vbCrLf & "       AND FTCalDate='" & _Date & "'"

                    HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)


                    _Qry = " DELETE FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTWageDaily "
                    _Qry &= vbCrLf & "  WHERE  FNHSysEmpID=" & Integer.Parse(Val(Rt!FNHSysEmpID.ToString)) & ""
                    _Qry &= vbCrLf & "    AND  FTDateTrans='" & _Date & "'"
                    HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

                    _Qry = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTWageDaily ( "
                    _Qry &= vbCrLf & "   FTInsUser, FTInsDate, FTInsTime, FNHSysEmpID, FTDateTrans"
                    _Qry &= vbCrLf & " , FNAmtNormal, FNAmtOT, FNNetAmt, FNProNormal, FNProOT"
                    _Qry &= vbCrLf & " , FNProOther, FNNetProAmt,FNQAAmt"
                    _Qry &= vbCrLf & " )"
                    _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & ""
                    _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & ""
                    _Qry &= vbCrLf & " ," & Integer.Parse(Val(Rt!FNHSysEmpID.ToString)) & ""
                    _Qry &= vbCrLf & " ,'" & _Date & "'   "
                    _Qry &= vbCrLf & "  ,MAX(FNAmtNormal) AS FNAmtNormal"
                    _Qry &= vbCrLf & " , MAX(FNAmtOT) AS FNAmtOT"
                    _Qry &= vbCrLf & " , MAX(FNNetAmt) AS FNNetAmt"
                    _Qry &= vbCrLf & ", Sum(FNAmtOldIncentive) AS FNAmtOldIncentive"
                    _Qry &= vbCrLf & ", Sum(FNAmtOTOldIncentive) AS FNAmtOTOldIncentive,0 AS FNProOther"
                    _Qry &= vbCrLf & ", Sum(FNNetAmtOldIncentive) AS FNNetAmtOldIncentive"
                    _Qry &= vbCrLf & ", Sum(FNQAValue) AS FNQAValue"
                    _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTIncentive_Emp  AS X WITH(NOLOCK) "
                    _Qry &= vbCrLf & "  WHERE  FNHSysEmpID=" & Integer.Parse(Val(Rt!FNHSysEmpID.ToString)) & ""
                    _Qry &= vbCrLf & "       AND FTCalDate='" & _Date & "'"

                    HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

                Next

            Next

        Next

        Return True

    End Function


    Private Function CalculateHeat(Spls As HI.TL.SplashScreen) As Boolean

        'Dim _State As Boolean = False
        'Dim _dt As DataTable
        'Dim _UnitSectAmt As Double = 0
        'Dim _UnitSectIncentiveAmt As Double = 0
        'Dim _dtemptime As DataTable
        'Dim _TotalEmp As Integer = 0
        'Dim _TotalTime As Integer = 0
        'Dim _TotalTimeOT As Integer = 0
        'Dim _TotalTimeHR As Integer = 0
        'Dim _TotalTimeOTHR As Integer = 0
        'Dim _Salary As Double = 0
        'Dim _SalaryPerH As Double = 0
        'Dim _SalaryPerOT As Double = 0
        'Dim _CountEmp As Integer = 0
        'Dim _FNAmtNormal As Double = 0
        'Dim _FNAmtOT As Double = 0
        'Dim _FNNetAmt As Double = 0
        'Dim _FixHour As Integer = 0
        'Dim _CountEmpIncentive As Integer
        'Dim _TotalEmpCal As Integer = 0
        'Dim _EmpIncentiveAmt As Double = 0
        'Dim _TotalTimeMin As Integer = 0
        'Dim _TotalEmpAmt As Double = 0

        'Dim _Qry As String = ""
        'With CType(Me.ogc3.DataSource, DataTable)
        '    .AcceptChanges()
        '    _dt = .Copy

        '    .AcceptChanges()
        'End With

        'Dim grpfodata As List(Of Integer) = (_dt.Select("FTSelect='1'", "FNHSysUnitSectId").CopyToDataTable).AsEnumerable() _
        '.Select(Function(r) r.Field(Of Integer)("FNHSysUnitSectId")) _
        '.Distinct() _
        '.ToList()

        'For Each _UnitSect As Integer In grpfodata



        '    Dim grpfodataDate As List(Of String) = (_dt.Select("FTSelect='1' AND FNHSysUnitSectId =" & Val(_UnitSect) & "", "FDScanDateOrg").CopyToDataTable).AsEnumerable() _
        '     .Select(Function(r) r.Field(Of String)("FDScanDateOrg")) _
        '     .Distinct() _
        '     .ToList()

        '    For Each _Date As String In grpfodataDate

        '        _Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTIncentive "
        '        _Qry &= vbCrLf & " WHERE  (FTCalDate = '" & HI.UL.ULDate.ConvertEnDB(_Date) & "') "
        '        _Qry &= vbCrLf & "   AND (FNHSysUnitSectId = " & Integer.Parse(Val(_UnitSect)) & ")"
        '        HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

        '        _Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTIncentive_Emp "
        '        _Qry &= vbCrLf & " WHERE  (FTCalDate = '" & HI.UL.ULDate.ConvertEnDB(_Date) & "') "
        '        _Qry &= vbCrLf & "   AND (FNHSysUnitSectId = " & Integer.Parse(Val(_UnitSect)) & ")"
        '        HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)


        '        _dtemptime = GetEmpTime(Integer.Parse(Val(_UnitSect)), _Date).Copy()

        '        _UnitSectAmt = 0
        '        _UnitSectIncentiveAmt = 0
        '        For Each Rx As DataRow In _dt.Select("FNHSysUnitSectId=" & Val(_UnitSect) & " AND FDScanDateOrg='" & HI.UL.ULDate.ConvertEnDB(_Date) & "'")

        '            _UnitSectAmt = _UnitSectAmt + Val(Rx!FNIncentiveAmt.ToString)

        '        Next
        '        _TotalEmp = 0
        '        Try
        '            _TotalEmp = _dtemptime.Select(" FNTotalWorkingMin>=0").Length
        '        Catch ex As Exception

        '        End Try

        '        _UnitSectIncentiveAmt = _UnitSectAmt '(Double.Parse(Format(_UnitSectAmt / 3.0, "0.00")) * _TotalEmp)



        '        '-----ตรวจสอบประกันรายชั่วโมง ประกันรายวัน ------------------

        '        _CountEmp = 0
        '        _Salary = 0
        '        _SalaryPerH = 0
        '        _SalaryPerOT = 0
        '        _FNAmtNormal = 0
        '        _FNAmtOT = 0
        '        _FNNetAmt = 0
        '        'Calculate Wage HR Normal
        '        _TotalTime = 0
        '        _TotalTimeOT = 0
        '        _TotalTimeMin = 0
        '        For Each Rt As DataRow In _dtemptime.Rows
        '            _CountEmp = _CountEmp + 1
        '            _TotalTime = _TotalTime + Integer.Parse(Val(Rt!FNWorkingMin.ToString))
        '            _TotalTimeOT = _TotalTimeOT + Integer.Parse(Val(Rt!FNWorkingOTMin.ToString))

        '            _TotalTimeHR = Integer.Parse(Val(Rt!FNTimeMin.ToString))
        '            _TotalTimeOTHR = Integer.Parse(Val(Rt!FNOT1Min.ToString))


        '            _TotalTimeMin = _TotalTimeMin + (Val(Integer.Parse(Val(Rt!FNTotalWorkingMin.ToString))))

        '            _Salary = Double.Parse(Val(Rt!FNSalary.ToString))
        '            _SalaryPerH = Double.Parse(Format(_Salary / 8, "0.00"))
        '            _SalaryPerOT = Double.Parse(Format(_SalaryPerH * 1.5, "0.00"))

        '            If _TotalTimeHR >= 480 Then
        '                _FNAmtNormal = _Salary
        '            Else
        '                _FNAmtNormal = Double.Parse(Format((_Salary / 480) * _TotalTimeHR, "0.00"))
        '            End If

        '            _FNAmtOT = Double.Parse(Format(((_Salary / 480) * 1.5) * _TotalTimeOTHR, "0.00"))

        '            _FNNetAmt = _FNAmtNormal + _FNAmtOT

        '            _Qry = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTIncentive_Emp ("
        '            _Qry &= vbCrLf & "  FTInsUser, FDInsDate, FTInsTime, FTCalDate, FNHSysUnitSectId, FNHSysEmpID"
        '            _Qry &= vbCrLf & ", FNTimeHour01, FNTimeHour02, FNTimeHour03, FNTimeHour04, FNTimeHour05, FNTimeHour06, FNTimeHour07, FNTimeHour08, "
        '            _Qry &= vbCrLf & "   FNTimeHour09, FNTimeHour10, FNTimeHour11, FNTimeHour12, FNTimeHour13, FNTotalTime, FNTotalTimeOT,FNTotalTimHR,FNTotalTimeOTHR"
        '            _Qry &= vbCrLf & ", FNSalary, FNAmtNormal, FNAmtOT, FNNetAmt, FNAmtOldIncentive, FNAmtOTOldIncentive,"
        '            _Qry &= vbCrLf & "  FNNetAmtOldIncentive, FNAmtNewIncentive, FNAmtOTNewIncentive, FNNetAmtNewIncentive,FNQAValue,FTInsuranceHour,FTInsuranceAmt"
        '            _Qry &= vbCrLf & ")"
        '            _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
        '            _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & ""
        '            _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & ""
        '            _Qry &= vbCrLf & " ,'" & _Date & "'   "
        '            _Qry &= vbCrLf & " ," & Integer.Parse(Val(_UnitSect)) & ""
        '            _Qry &= vbCrLf & " ," & Integer.Parse(Val(Rt!FNHSysEmpID.ToString)) & ""
        '            _Qry &= vbCrLf & " ," & Integer.Parse(Val(Rt!FNH01.ToString)) & ""
        '            _Qry &= vbCrLf & " ," & Integer.Parse(Val(Rt!FNH02.ToString)) & ""
        '            _Qry &= vbCrLf & " ," & Integer.Parse(Val(Rt!FNH03.ToString)) & ""
        '            _Qry &= vbCrLf & " ," & Integer.Parse(Val(Rt!FNH04.ToString)) & ""
        '            _Qry &= vbCrLf & " ," & Integer.Parse(Val(Rt!FNH05.ToString)) & ""
        '            _Qry &= vbCrLf & " ," & Integer.Parse(Val(Rt!FNH06.ToString)) & ""
        '            _Qry &= vbCrLf & " ," & Integer.Parse(Val(Rt!FNH07.ToString)) & ""
        '            _Qry &= vbCrLf & " ," & Integer.Parse(Val(Rt!FNH08.ToString)) & ""
        '            _Qry &= vbCrLf & " ," & Integer.Parse(Val(Rt!FNH09.ToString)) & ""
        '            _Qry &= vbCrLf & " ," & Integer.Parse(Val(Rt!FNH10.ToString)) & ""
        '            _Qry &= vbCrLf & " ," & Integer.Parse(Val(Rt!FNH11.ToString)) & ""
        '            _Qry &= vbCrLf & " ," & Integer.Parse(Val(Rt!FNH12.ToString)) & ""
        '            _Qry &= vbCrLf & " ," & Integer.Parse(Val(Rt!FNH13.ToString)) & ""
        '            _Qry &= vbCrLf & " ," & Integer.Parse(Val(Rt!FNWorkingMin.ToString)) & ""
        '            _Qry &= vbCrLf & " ," & Integer.Parse(Val(Rt!FNWorkingOTMin.ToString)) & ""
        '            _Qry &= vbCrLf & " ," & _TotalTimeHR & ""
        '            _Qry &= vbCrLf & " ," & _TotalTimeOTHR & ""
        '            _Qry &= vbCrLf & " ," & _Salary & ""
        '            _Qry &= vbCrLf & " ," & _FNAmtNormal & ""
        '            _Qry &= vbCrLf & " ," & _FNAmtOT & ""
        '            _Qry &= vbCrLf & " ," & _FNNetAmt & ""
        '            _Qry &= vbCrLf & " ,0,0,0,0,0,0,0"
        '            _Qry &= vbCrLf & " ," & _FixHour & ""
        '            _Qry &= vbCrLf & " ,0"

        '            HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

        '        Next

        '        _Qry = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTIncentive ("
        '        _Qry &= vbCrLf & "  FTInsUser, FDInsDate, FTInsTime,  FTCalDate, FNHSysUnitSectId, FNHour01Qty, FNHour02Qty"
        '        _Qry &= vbCrLf & " , FNHour03Qty, FNHour04Qty, FNHour05Qty, FNHour06Qty, FNHour07Qty, "
        '        _Qry &= vbCrLf & "   FNHour08Qty, FNHour09Qty, FNHour10Qty, FNHour11Qty, FNHour12Qty, FNHour13Qty"
        '        _Qry &= vbCrLf & " , FNTotalQty, FTStateInsuranceInH1, FTStateInsuranceInH2, FTStateInsuranceInH3, FTStateInsuranceInH4, FTStateInsuranceInH5,"
        '        _Qry &= vbCrLf & "   FTStateInsuranceInH6, FTStateInsuranceInH7, FTStateInsuranceInH8, FTStateInsuranceInH9, FTStateInsuranceInH10"
        '        _Qry &= vbCrLf & " , FTStateInsuranceInH11, FTStateInsuranceInH12, FTStateInsuranceInH13,"
        '        _Qry &= vbCrLf & "   FTStateInsuranceInDay, FNTotalEmp, FNTotalTime, FNTeamAmt, FNQAPer, FNQAValue, FNTeamNetAmt, FNIncentiveType,FNTeamIncentiveQty,FNTeamIncentiveAmt"
        '        _Qry &= vbCrLf & "  , FNHour01QtySystem, FNHour02QtySystem"
        '        _Qry &= vbCrLf & "  ,  FNHour03QtySystem, FNHour04QtySystem"
        '        _Qry &= vbCrLf & "  , FNHour05QtySystem, FNHour06QtySystem"
        '        _Qry &= vbCrLf & "  , FNHour07QtySystem, FNHour08QtySystem"
        '        _Qry &= vbCrLf & "  , FNHour09QtySystem, FNHour10QtySystem"
        '        _Qry &= vbCrLf & " , FNHour11QtySystem, FNHour12QtySystem"
        '        _Qry &= vbCrLf & "  , FNHour13QtySystem, FNTotalQtySystem"
        '        _Qry &= vbCrLf & ")"
        '        _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
        '        _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & ""
        '        _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & ""
        '        _Qry &= vbCrLf & " ,'" & _Date & "'   "
        '        _Qry &= vbCrLf & " ," & Integer.Parse(Val(_UnitSect)) & ""
        '        _Qry &= vbCrLf & " ,0"
        '        _Qry &= vbCrLf & " ,0"
        '        _Qry &= vbCrLf & " ,0"
        '        _Qry &= vbCrLf & " ,0"
        '        _Qry &= vbCrLf & " ,0"
        '        _Qry &= vbCrLf & " ,0"
        '        _Qry &= vbCrLf & " ,0"
        '        _Qry &= vbCrLf & " ,0"
        '        _Qry &= vbCrLf & " ,0"
        '        _Qry &= vbCrLf & " ,0"
        '        _Qry &= vbCrLf & " ,0"
        '        _Qry &= vbCrLf & " ,0"
        '        _Qry &= vbCrLf & " ,0"
        '        _Qry &= vbCrLf & " ,0"
        '        _Qry &= vbCrLf & " ,'0'"
        '        _Qry &= vbCrLf & " ,'0'"
        '        _Qry &= vbCrLf & " ,'0'"
        '        _Qry &= vbCrLf & " ,'0'"
        '        _Qry &= vbCrLf & " ,'0'"
        '        _Qry &= vbCrLf & " ,'0'"
        '        _Qry &= vbCrLf & " ,'0'"
        '        _Qry &= vbCrLf & " ,'0'"
        '        _Qry &= vbCrLf & " ,'0'"
        '        _Qry &= vbCrLf & " ,'0'"
        '        _Qry &= vbCrLf & " ,'0'"
        '        _Qry &= vbCrLf & " ,'0'"
        '        _Qry &= vbCrLf & " ,'0'"
        '        _Qry &= vbCrLf & " ,'0'"
        '        _Qry &= vbCrLf & " ," & _CountEmp & ""
        '        _Qry &= vbCrLf & " ," & _TotalTime + _TotalTimeOT & ""
        '        _Qry &= vbCrLf & " ," & _UnitSectIncentiveAmt & ""
        '        _Qry &= vbCrLf & " ,0"
        '        _Qry &= vbCrLf & " ,0"
        '        _Qry &= vbCrLf & " ," & _UnitSectIncentiveAmt & ""
        '        _Qry &= vbCrLf & " ,-1"
        '        _Qry &= vbCrLf & " ,0"
        '        _Qry &= vbCrLf & " ," & _UnitSectIncentiveAmt & ""
        '        _Qry &= vbCrLf & " ,0"
        '        _Qry &= vbCrLf & " ,0"
        '        _Qry &= vbCrLf & " ,0"
        '        _Qry &= vbCrLf & " ,0"
        '        _Qry &= vbCrLf & " ,0"
        '        _Qry &= vbCrLf & " ,0"
        '        _Qry &= vbCrLf & " ,0"
        '        _Qry &= vbCrLf & " ,0"
        '        _Qry &= vbCrLf & " ,0"
        '        _Qry &= vbCrLf & " ,0"
        '        _Qry &= vbCrLf & " ,0"
        '        _Qry &= vbCrLf & " ,0"
        '        _Qry &= vbCrLf & " ,0"
        '        _Qry &= vbCrLf & " ,0"

        '        HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

        '        _CountEmpIncentive = _dtemptime.Select("FNTotalWorkingMin>0").Length


        '        For Each Rt As DataRow In _dtemptime.Select("FTStateCal<>'1'  AND FNTotalWorkingMin>0 ", "FNTotalWorkingMin")
        '            _TotalEmpCal = _TotalEmpCal + 1

        '            _Salary = Double.Parse(Val(Rt!FNSalary.ToString))
        '            _SalaryPerH = Double.Parse(Format(_Salary / 8, "0.00"))
        '            _SalaryPerOT = Double.Parse(Format(_SalaryPerH * 1.5, "0.00"))

        '            If _FixHour > 0 Then
        '            End If

        '            If _TotalEmpCal = _CountEmpIncentive Then
        '                _EmpIncentiveAmt = _UnitSectIncentiveAmt - _TotalEmpAmt
        '            Else
        '                _EmpIncentiveAmt = CDbl(Format((_UnitSectIncentiveAmt * (Integer.Parse(Val(Rt!FNTotalWorkingMin.ToString)))) / _TotalTimeMin, "0.00"))
        '            End If

        '            _TotalEmpAmt = _TotalEmpAmt + _EmpIncentiveAmt

        '            _FNAmtOT = Double.Parse(Format(((_Salary / 480) * 0.5) * Integer.Parse(Val(Rt!FNWorkingOTMin.ToString)), "0.00"))

        '            _FNNetAmt = _EmpIncentiveAmt + _FNAmtOT

        '            _Qry = " UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTIncentive_Emp "
        '            _Qry &= vbCrLf & "  SET  FNAmtOldIncentive=" & _EmpIncentiveAmt & ""
        '            _Qry &= vbCrLf & "  , FNAmtOTOldIncentive=" & _FNAmtOT & ""
        '            _Qry &= vbCrLf & "  , FNNetAmtOldIncentive=" & (_FNNetAmt) & ""
        '            _Qry &= vbCrLf & "   ,FNQAValue=0 "
        '            _Qry &= vbCrLf & "   WHERE FNHSysUnitSectId=" & Integer.Parse(Val(_UnitSect)) & ""
        '            _Qry &= vbCrLf & "       AND FNHSysEmpID=" & Integer.Parse(Val(Rt!FNHSysEmpID.ToString)) & ""
        '            _Qry &= vbCrLf & "       AND FTCalDate='" & _Date & "'"


        '            HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)


        '            _Qry = " DELETE FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTWageDaily "
        '            _Qry &= vbCrLf & "  WHERE  FNHSysEmpID=" & Integer.Parse(Val(Rt!FNHSysEmpID.ToString)) & ""
        '            _Qry &= vbCrLf & "    AND  FTDateTrans='" & _Date & "'"
        '            HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

        '            _Qry = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTWageDaily ( "
        '            _Qry &= vbCrLf & "   FTInsUser, FTInsDate, FTInsTime, FNHSysEmpID, FTDateTrans"
        '            _Qry &= vbCrLf & " , FNAmtNormal, FNAmtOT, FNNetAmt, FNProNormal, FNProOT"
        '            _Qry &= vbCrLf & " , FNProOther, FNNetProAmt,FNQAAmt"
        '            _Qry &= vbCrLf & " )"
        '            _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
        '            _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & ""
        '            _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & ""
        '            _Qry &= vbCrLf & " ," & Integer.Parse(Val(Rt!FNHSysEmpID.ToString)) & ""
        '            _Qry &= vbCrLf & " ,'" & _Date & "'   "
        '            _Qry &= vbCrLf & "  ,MAX(FNAmtNormal) AS FNAmtNormal"
        '            _Qry &= vbCrLf & " , MAX(FNAmtOT) AS FNAmtOT"
        '            _Qry &= vbCrLf & " , MAX(FNNetAmt) AS FNNetAmt"
        '            _Qry &= vbCrLf & ", Sum(FNAmtOldIncentive) AS FNAmtOldIncentive"
        '            _Qry &= vbCrLf & ", Sum(FNAmtOTOldIncentive) AS FNAmtOTOldIncentive,0 AS FNProOther"
        '            _Qry &= vbCrLf & ", Sum(FNNetAmtOldIncentive) AS FNNetAmtOldIncentive"
        '            _Qry &= vbCrLf & ", Sum(FNQAValue) AS FNQAValue"
        '            _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTIncentive_Emp  AS X WITH(NOLOCK) "
        '            _Qry &= vbCrLf & "  WHERE  FNHSysEmpID=" & Integer.Parse(Val(Rt!FNHSysEmpID.ToString)) & ""
        '            _Qry &= vbCrLf & "       AND FTCalDate='" & _Date & "'"

        '            HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

        '        Next

        '    Next

        'Next

        Return False

    End Function

    Private Function CalculateEmbPrint(Spls As HI.TL.SplashScreen) As Boolean

        Dim _State As Boolean = False
        Dim _dt As DataTable
        Dim _UnitSectAmt As Double = 0
        Dim _UnitSectIncentiveAmt As Double = 0
        Dim _dtemptime As DataTable
        Dim _TotalEmp As Integer = 0
        Dim _TotalTime As Integer = 0
        Dim _TotalTimeOT As Integer = 0
        Dim _TotalTimeHR As Integer = 0
        Dim _TotalTimeOTHR As Integer = 0
        Dim _Salary As Double = 0
        Dim _SalaryPerH As Double = 0
        Dim _SalaryPerOT As Double = 0
        Dim _CountEmp As Integer = 0
        Dim _FNAmtNormal As Double = 0
        Dim _FNAmtOT As Double = 0
        Dim _FNNetAmt As Double = 0
        Dim _FixHour As Integer = 0
        Dim _CountEmpIncentive As Integer
        Dim _TotalEmpCal As Integer = 0
        Dim _EmpIncentiveAmt As Double = 0
        Dim _TotalTimeMin As Integer = 0
        Dim _TotalEmpAmt As Double = 0

        Dim _Qry As String = ""
        With CType(Me.ogc5.DataSource, DataTable)
            .AcceptChanges()
            _dt = .Copy

            .AcceptChanges()
        End With

        Dim grpfodata As List(Of Integer) = (_dt.Select("FTSelect='1'", "FNHSysUnitSectId").CopyToDataTable).AsEnumerable() _
        .Select(Function(r) r.Field(Of Integer)("FNHSysUnitSectId")) _
        .Distinct() _
        .ToList()

        For Each _UnitSect As Integer In grpfodata



            Dim grpfodataDate As List(Of String) = (_dt.Select("FTSelect='1' AND FNHSysUnitSectId =" & Val(_UnitSect) & "", "FDScanDateOrg").CopyToDataTable).AsEnumerable() _
             .Select(Function(r) r.Field(Of String)("FDScanDateOrg")) _
             .Distinct() _
             .ToList()

            For Each _Date As String In grpfodataDate

                '_Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTIncentive "
                '_Qry &= vbCrLf & " WHERE  (FTCalDate = '" & HI.UL.ULDate.ConvertEnDB(_Date) & "') "
                '_Qry &= vbCrLf & "   AND (FNHSysUnitSectId = " & Integer.Parse(Val(_UnitSect)) & ")"
                'HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

                '_Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTIncentive_Emp "
                '_Qry &= vbCrLf & " WHERE  (FTCalDate = '" & HI.UL.ULDate.ConvertEnDB(_Date) & "') "
                '_Qry &= vbCrLf & "   AND (FNHSysUnitSectId = " & Integer.Parse(Val(_UnitSect)) & ")"
                'HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)


                _dtemptime = GetEmpTime(Integer.Parse(Val(_UnitSect)), _Date).Copy()

                _UnitSectAmt = 0
                _UnitSectIncentiveAmt = 0
                For Each Rx As DataRow In _dt.Select("FNHSysUnitSectId=" & Val(_UnitSect) & " AND FDScanDateOrg='" & HI.UL.ULDate.ConvertEnDB(_Date) & "'")

                    _UnitSectAmt = _UnitSectAmt + Val(Rx!FNIncentiveAmt.ToString)

                    _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTIncentive_EmdPrintLaserOther "
                    _Qry &= vbCrLf & "("
                    _Qry &= vbCrLf & " FTInsUser, FDInsDate, FTInsTime "
                    _Qry &= vbCrLf & ", FTCalDate, FNHSysUnitSectId, FTOrderNo, FNHSysPartId, FNQuantity"
                    _Qry &= vbCrLf & ", FNPrice, FNNetAmt, FNSendSuplType"
                    _Qry &= vbCrLf & ")"
                    _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & ""
                    _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & ""
                    _Qry &= vbCrLf & " ,'" & HI.UL.ULDate.ConvertEnDB(_Date) & "'"
                    _Qry &= vbCrLf & " ," & Integer.Parse(Val(_UnitSect)) & " "
                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Rx!FTOrderNo.ToString) & "'"
                    _Qry &= vbCrLf & "," & Val(Rx!FNHSysPartId.ToString) & ""
                    _Qry &= vbCrLf & " ," & Integer.Parse(Val(Rx!FNQuantity.ToString)) & " "
                    _Qry &= vbCrLf & " ," & Double.Parse(Val(Rx!FNPrice.ToString)) & " "
                    _Qry &= vbCrLf & " ," & Double.Parse(Val(Rx!FNIncentiveAmt.ToString)) & " "
                    _Qry &= vbCrLf & " ," & Integer.Parse(Val(Rx!FNSendSuplType.ToString)) & " "

                    HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)


                Next

                _TotalEmp = 0
                Try
                    _TotalEmp = _dtemptime.Select(" FNTotalWorkingMin>=0").Length
                Catch ex As Exception

                End Try

                _UnitSectIncentiveAmt = _UnitSectAmt '(Double.Parse(Format(_UnitSectAmt / 3.0, "0.00")) * _TotalEmp)



                '-----ตรวจสอบประกันรายชั่วโมง ประกันรายวัน ------------------

                _CountEmp = 0
                _Salary = 0
                _SalaryPerH = 0
                _SalaryPerOT = 0
                _FNAmtNormal = 0
                _FNAmtOT = 0
                _FNNetAmt = 0
                'Calculate Wage HR Normal
                _TotalTime = 0
                _TotalTimeOT = 0
                _TotalTimeMin = 0
                For Each Rt As DataRow In _dtemptime.Rows
                    _CountEmp = _CountEmp + 1
                    _TotalTime = _TotalTime + Integer.Parse(Val(Rt!FNWorkingMin.ToString))
                    _TotalTimeOT = _TotalTimeOT + Integer.Parse(Val(Rt!FNWorkingOTMin.ToString))

                    _TotalTimeHR = Integer.Parse(Val(Rt!FNTimeMin.ToString))
                    _TotalTimeOTHR = Integer.Parse(Val(Rt!FNOT1Min.ToString))


                    _TotalTimeMin = _TotalTimeMin + (Val(Integer.Parse(Val(Rt!FNTotalWorkingMin.ToString))))

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
                    _Qry &= vbCrLf & ")"
                    _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & ""
                    _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & ""
                    _Qry &= vbCrLf & " ,'" & _Date & "'   "
                    _Qry &= vbCrLf & " ," & Integer.Parse(Val(_UnitSect)) & ""
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

                    HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

                Next

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
                _Qry &= vbCrLf & " ,'" & _Date & "'   "
                _Qry &= vbCrLf & " ," & Integer.Parse(Val(_UnitSect)) & ""
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,'0'"
                _Qry &= vbCrLf & " ,'0'"
                _Qry &= vbCrLf & " ,'0'"
                _Qry &= vbCrLf & " ,'0'"
                _Qry &= vbCrLf & " ,'0'"
                _Qry &= vbCrLf & " ,'0'"
                _Qry &= vbCrLf & " ,'0'"
                _Qry &= vbCrLf & " ,'0'"
                _Qry &= vbCrLf & " ,'0'"
                _Qry &= vbCrLf & " ,'0'"
                _Qry &= vbCrLf & " ,'0'"
                _Qry &= vbCrLf & " ,'0'"
                _Qry &= vbCrLf & " ,'0'"
                _Qry &= vbCrLf & " ,'0'"
                _Qry &= vbCrLf & " ," & _CountEmp & ""
                _Qry &= vbCrLf & " ," & _TotalTime + _TotalTimeOT & ""
                _Qry &= vbCrLf & " ," & _UnitSectIncentiveAmt & ""
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ," & _UnitSectIncentiveAmt & ""
                _Qry &= vbCrLf & " ,-1"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ," & _UnitSectIncentiveAmt & ""
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"

                HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

                _CountEmpIncentive = _dtemptime.Select("FNTotalWorkingMin>0").Length


                For Each Rt As DataRow In _dtemptime.Select("FTStateCal<>'1'  AND FNTotalWorkingMin>0 ", "FNTotalWorkingMin")
                    _TotalEmpCal = _TotalEmpCal + 1

                    _Salary = Double.Parse(Val(Rt!FNSalary.ToString))
                    _SalaryPerH = Double.Parse(Format(_Salary / 8, "0.00"))
                    _SalaryPerOT = Double.Parse(Format(_SalaryPerH * 1.5, "0.00"))

                    If _FixHour > 0 Then
                    End If

                    If _TotalEmpCal = _CountEmpIncentive Then
                        _EmpIncentiveAmt = _UnitSectIncentiveAmt - _TotalEmpAmt
                    Else
                        _EmpIncentiveAmt = CDbl(Format((_UnitSectIncentiveAmt * (Integer.Parse(Val(Rt!FNTotalWorkingMin.ToString)))) / _TotalTimeMin, "0.00"))
                    End If

                    _TotalEmpAmt = _TotalEmpAmt + _EmpIncentiveAmt

                    _FNAmtOT = Double.Parse(Format(((_Salary / 480) * 0.5) * Integer.Parse(Val(Rt!FNWorkingOTMin.ToString)), "0.00"))

                    _FNNetAmt = _EmpIncentiveAmt + _FNAmtOT

                    _Qry = " UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTIncentive_Emp "
                    _Qry &= vbCrLf & "  SET  FNAmtOldIncentive=" & _EmpIncentiveAmt & ""
                    _Qry &= vbCrLf & "  , FNAmtOTOldIncentive=" & _FNAmtOT & ""
                    _Qry &= vbCrLf & "  , FNNetAmtOldIncentive=" & (_FNNetAmt) & ""
                    _Qry &= vbCrLf & "   ,FNQAValue=0 "
                    _Qry &= vbCrLf & "   WHERE FNHSysUnitSectId=" & Integer.Parse(Val(_UnitSect)) & ""
                    _Qry &= vbCrLf & "       AND FNHSysEmpID=" & Integer.Parse(Val(Rt!FNHSysEmpID.ToString)) & ""
                    _Qry &= vbCrLf & "       AND FTCalDate='" & _Date & "'"


                    HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)


                    _Qry = " DELETE FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTWageDaily "
                    _Qry &= vbCrLf & "  WHERE  FNHSysEmpID=" & Integer.Parse(Val(Rt!FNHSysEmpID.ToString)) & ""
                    _Qry &= vbCrLf & "    AND  FTDateTrans='" & _Date & "'"
                    HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

                    _Qry = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTWageDaily ( "
                    _Qry &= vbCrLf & "   FTInsUser, FTInsDate, FTInsTime, FNHSysEmpID, FTDateTrans"
                    _Qry &= vbCrLf & " , FNAmtNormal, FNAmtOT, FNNetAmt, FNProNormal, FNProOT"
                    _Qry &= vbCrLf & " , FNProOther, FNNetProAmt,FNQAAmt"
                    _Qry &= vbCrLf & " )"
                    _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & ""
                    _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & ""
                    _Qry &= vbCrLf & " ," & Integer.Parse(Val(Rt!FNHSysEmpID.ToString)) & ""
                    _Qry &= vbCrLf & " ,'" & _Date & "'   "
                    _Qry &= vbCrLf & "  ,MAX(FNAmtNormal) AS FNAmtNormal"
                    _Qry &= vbCrLf & " , MAX(FNAmtOT) AS FNAmtOT"
                    _Qry &= vbCrLf & " , MAX(FNNetAmt) AS FNNetAmt"
                    _Qry &= vbCrLf & ", Sum(FNAmtOldIncentive) AS FNAmtOldIncentive"
                    _Qry &= vbCrLf & ", Sum(FNAmtOTOldIncentive) AS FNAmtOTOldIncentive,0 AS FNProOther"
                    _Qry &= vbCrLf & ", Sum(FNNetAmtOldIncentive) AS FNNetAmtOldIncentive"
                    _Qry &= vbCrLf & ", Sum(FNQAValue) AS FNQAValue"
                    _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTIncentive_Emp  AS X WITH(NOLOCK) "
                    _Qry &= vbCrLf & "  WHERE  FNHSysEmpID=" & Integer.Parse(Val(Rt!FNHSysEmpID.ToString)) & ""
                    _Qry &= vbCrLf & "       AND FTCalDate='" & _Date & "'"

                    HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

                Next

            Next

        Next

        Return True

    End Function

    Private Function CalculateLaser(Spls As HI.TL.SplashScreen) As Boolean

        Dim _State As Boolean = False
        Dim _dt As DataTable
        Dim _UnitSectAmt As Double = 0
        Dim _UnitSectIncentiveAmt As Double = 0
        Dim _dtemptime As DataTable
        Dim _TotalEmp As Integer = 0
        Dim _TotalTime As Integer = 0
        Dim _TotalTimeOT As Integer = 0
        Dim _TotalTimeHR As Integer = 0
        Dim _TotalTimeOTHR As Integer = 0
        Dim _Salary As Double = 0
        Dim _SalaryPerH As Double = 0
        Dim _SalaryPerOT As Double = 0
        Dim _CountEmp As Integer = 0
        Dim _FNAmtNormal As Double = 0
        Dim _FNAmtOT As Double = 0
        Dim _FNNetAmt As Double = 0
        Dim _FixHour As Integer = 0
        Dim _CountEmpIncentive As Integer
        Dim _TotalEmpCal As Integer = 0
        Dim _EmpIncentiveAmt As Double = 0
        Dim _TotalTimeMin As Integer = 0
        Dim _TotalEmpAmt As Double = 0

        Dim _Qry As String = ""
        With CType(Me.ogc6.DataSource, DataTable)
            .AcceptChanges()
            _dt = .Copy

            .AcceptChanges()
        End With

        Dim grpfodata As List(Of Integer) = (_dt.Select("FTSelect='1'", "FNHSysUnitSectId").CopyToDataTable).AsEnumerable() _
        .Select(Function(r) r.Field(Of Integer)("FNHSysUnitSectId")) _
        .Distinct() _
        .ToList()

        For Each _UnitSect As Integer In grpfodata



            Dim grpfodataDate As List(Of String) = (_dt.Select("FTSelect='1' AND FNHSysUnitSectId =" & Val(_UnitSect) & "", "FDScanDateOrg").CopyToDataTable).AsEnumerable() _
             .Select(Function(r) r.Field(Of String)("FDScanDateOrg")) _
             .Distinct() _
             .ToList()

            For Each _Date As String In grpfodataDate

                '_Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTIncentive "
                '_Qry &= vbCrLf & " WHERE  (FTCalDate = '" & HI.UL.ULDate.ConvertEnDB(_Date) & "') "
                '_Qry &= vbCrLf & "   AND (FNHSysUnitSectId = " & Integer.Parse(Val(_UnitSect)) & ")"
                'HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

                '_Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTIncentive_Emp "
                '_Qry &= vbCrLf & " WHERE  (FTCalDate = '" & HI.UL.ULDate.ConvertEnDB(_Date) & "') "
                '_Qry &= vbCrLf & "   AND (FNHSysUnitSectId = " & Integer.Parse(Val(_UnitSect)) & ")"
                'HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

                '_Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTIncentive_EmdPrintLaserOther "
                '_Qry &= vbCrLf & " WHERE  (FTCalDate = '" & HI.UL.ULDate.ConvertEnDB(_Date) & "') "
                '_Qry &= vbCrLf & "   AND (FNHSysUnitSectId = " & Integer.Parse(Val(_UnitSect)) & ")"
                HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)


                _dtemptime = GetEmpTime(Integer.Parse(Val(_UnitSect)), _Date).Copy()

                _UnitSectAmt = 0
                _UnitSectIncentiveAmt = 0
                For Each Rx As DataRow In _dt.Select("FNHSysUnitSectId=" & Val(_UnitSect) & " AND FDScanDateOrg='" & HI.UL.ULDate.ConvertEnDB(_Date) & "'")

                    _UnitSectAmt = _UnitSectAmt + Val(Rx!FNIncentiveAmt.ToString)

                    _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTIncentive_EmdPrintLaserOther "
                    _Qry &= vbCrLf & "("
                    _Qry &= vbCrLf & " FTInsUser, FDInsDate, FTInsTime "
                    _Qry &= vbCrLf & ", FTCalDate, FNHSysUnitSectId, FTOrderNo, FNHSysPartId, FNQuantity"
                    _Qry &= vbCrLf & ", FNPrice, FNNetAmt, FNSendSuplType"
                    _Qry &= vbCrLf & ")"
                    _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & ""
                    _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & ""
                    _Qry &= vbCrLf & " ,'" & HI.UL.ULDate.ConvertEnDB(_Date) & "'"
                    _Qry &= vbCrLf & " ," & Integer.Parse(Val(_UnitSect)) & " "
                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Rx!FTOrderNo.ToString) & "'"
                    _Qry &= vbCrLf & "," & Val(Rx!FNHSysPartId.ToString) & ""
                    _Qry &= vbCrLf & " ," & Integer.Parse(Val(Rx!FNQuantity.ToString)) & " "
                    _Qry &= vbCrLf & " ," & Double.Parse(Val(Rx!FNPrice.ToString)) & " "
                    _Qry &= vbCrLf & " ," & Double.Parse(Val(Rx!FNIncentiveAmt.ToString)) & " "
                    _Qry &= vbCrLf & " ," & Integer.Parse(Val(Rx!FNSendSuplType.ToString)) & " "

                    HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

                Next
                _TotalEmp = 0
                Try
                    _TotalEmp = _dtemptime.Select(" FNTotalWorkingMin>=0").Length
                Catch ex As Exception

                End Try

                _UnitSectIncentiveAmt = _UnitSectAmt '(Double.Parse(Format(_UnitSectAmt / 3.0, "0.00")) * _TotalEmp)



                '-----ตรวจสอบประกันรายชั่วโมง ประกันรายวัน ------------------

                _CountEmp = 0
                _Salary = 0
                _SalaryPerH = 0
                _SalaryPerOT = 0
                _FNAmtNormal = 0
                _FNAmtOT = 0
                _FNNetAmt = 0
                'Calculate Wage HR Normal
                _TotalTime = 0
                _TotalTimeOT = 0
                _TotalTimeMin = 0
                For Each Rt As DataRow In _dtemptime.Rows
                    _CountEmp = _CountEmp + 1
                    _TotalTime = _TotalTime + Integer.Parse(Val(Rt!FNWorkingMin.ToString))
                    _TotalTimeOT = _TotalTimeOT + Integer.Parse(Val(Rt!FNWorkingOTMin.ToString))

                    _TotalTimeHR = Integer.Parse(Val(Rt!FNTimeMin.ToString))
                    _TotalTimeOTHR = Integer.Parse(Val(Rt!FNOT1Min.ToString))


                    _TotalTimeMin = _TotalTimeMin + (Val(Integer.Parse(Val(Rt!FNTotalWorkingMin.ToString))))

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
                    _Qry &= vbCrLf & ")"
                    _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & ""
                    _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & ""
                    _Qry &= vbCrLf & " ,'" & _Date & "'   "
                    _Qry &= vbCrLf & " ," & Integer.Parse(Val(_UnitSect)) & ""
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

                    HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

                Next

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
                _Qry &= vbCrLf & " ,'" & _Date & "'   "
                _Qry &= vbCrLf & " ," & Integer.Parse(Val(_UnitSect)) & ""
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,'0'"
                _Qry &= vbCrLf & " ,'0'"
                _Qry &= vbCrLf & " ,'0'"
                _Qry &= vbCrLf & " ,'0'"
                _Qry &= vbCrLf & " ,'0'"
                _Qry &= vbCrLf & " ,'0'"
                _Qry &= vbCrLf & " ,'0'"
                _Qry &= vbCrLf & " ,'0'"
                _Qry &= vbCrLf & " ,'0'"
                _Qry &= vbCrLf & " ,'0'"
                _Qry &= vbCrLf & " ,'0'"
                _Qry &= vbCrLf & " ,'0'"
                _Qry &= vbCrLf & " ,'0'"
                _Qry &= vbCrLf & " ,'0'"
                _Qry &= vbCrLf & " ," & _CountEmp & ""
                _Qry &= vbCrLf & " ," & _TotalTime + _TotalTimeOT & ""
                _Qry &= vbCrLf & " ," & _UnitSectIncentiveAmt & ""
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ," & _UnitSectIncentiveAmt & ""
                _Qry &= vbCrLf & " ,-1"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ," & _UnitSectIncentiveAmt & ""
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"

                HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

                _CountEmpIncentive = _dtemptime.Select("FNTotalWorkingMin>0").Length


                For Each Rt As DataRow In _dtemptime.Select("FTStateCal<>'1'  AND FNTotalWorkingMin>0 ", "FNTotalWorkingMin")
                    _TotalEmpCal = _TotalEmpCal + 1

                    _Salary = Double.Parse(Val(Rt!FNSalary.ToString))
                    _SalaryPerH = Double.Parse(Format(_Salary / 8, "0.00"))
                    _SalaryPerOT = Double.Parse(Format(_SalaryPerH * 1.5, "0.00"))

                    If _FixHour > 0 Then
                    End If

                    If _TotalEmpCal = _CountEmpIncentive Then
                        _EmpIncentiveAmt = _UnitSectIncentiveAmt - _TotalEmpAmt
                    Else
                        _EmpIncentiveAmt = CDbl(Format((_UnitSectIncentiveAmt * (Integer.Parse(Val(Rt!FNTotalWorkingMin.ToString)))) / _TotalTimeMin, "0.00"))
                    End If

                    _TotalEmpAmt = _TotalEmpAmt + _EmpIncentiveAmt

                    _FNAmtOT = Double.Parse(Format(((_Salary / 480) * 0.5) * Integer.Parse(Val(Rt!FNWorkingOTMin.ToString)), "0.00"))

                    _FNNetAmt = _EmpIncentiveAmt + _FNAmtOT

                    _Qry = " UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTIncentive_Emp "
                    _Qry &= vbCrLf & "  SET  FNAmtOldIncentive=" & _EmpIncentiveAmt & ""
                    _Qry &= vbCrLf & "  , FNAmtOTOldIncentive=" & _FNAmtOT & ""
                    _Qry &= vbCrLf & "  , FNNetAmtOldIncentive=" & (_FNNetAmt) & ""
                    _Qry &= vbCrLf & "   ,FNQAValue=0 "
                    _Qry &= vbCrLf & "   WHERE FNHSysUnitSectId=" & Integer.Parse(Val(_UnitSect)) & ""
                    _Qry &= vbCrLf & "       AND FNHSysEmpID=" & Integer.Parse(Val(Rt!FNHSysEmpID.ToString)) & ""
                    _Qry &= vbCrLf & "       AND FTCalDate='" & _Date & "'"


                    HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)


                    _Qry = " DELETE FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTWageDaily "
                    _Qry &= vbCrLf & "  WHERE  FNHSysEmpID=" & Integer.Parse(Val(Rt!FNHSysEmpID.ToString)) & ""
                    _Qry &= vbCrLf & "    AND  FTDateTrans='" & _Date & "'"
                    HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

                    _Qry = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTWageDaily ( "
                    _Qry &= vbCrLf & "   FTInsUser, FTInsDate, FTInsTime, FNHSysEmpID, FTDateTrans"
                    _Qry &= vbCrLf & " , FNAmtNormal, FNAmtOT, FNNetAmt, FNProNormal, FNProOT"
                    _Qry &= vbCrLf & " , FNProOther, FNNetProAmt,FNQAAmt"
                    _Qry &= vbCrLf & " )"
                    _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & ""
                    _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & ""
                    _Qry &= vbCrLf & " ," & Integer.Parse(Val(Rt!FNHSysEmpID.ToString)) & ""
                    _Qry &= vbCrLf & " ,'" & _Date & "'   "
                    _Qry &= vbCrLf & "  ,MAX(FNAmtNormal) AS FNAmtNormal"
                    _Qry &= vbCrLf & " , MAX(FNAmtOT) AS FNAmtOT"
                    _Qry &= vbCrLf & " , MAX(FNNetAmt) AS FNNetAmt"
                    _Qry &= vbCrLf & ", Sum(FNAmtOldIncentive) AS FNAmtOldIncentive"
                    _Qry &= vbCrLf & ", Sum(FNAmtOTOldIncentive) AS FNAmtOTOldIncentive,0 AS FNProOther"
                    _Qry &= vbCrLf & ", Sum(FNNetAmtOldIncentive) AS FNNetAmtOldIncentive"
                    _Qry &= vbCrLf & ", Sum(FNQAValue) AS FNQAValue"
                    _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTIncentive_Emp  AS X WITH(NOLOCK) "
                    _Qry &= vbCrLf & "  WHERE  FNHSysEmpID=" & Integer.Parse(Val(Rt!FNHSysEmpID.ToString)) & ""
                    _Qry &= vbCrLf & "       AND FTCalDate='" & _Date & "'"

                    HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

                Next

            Next

        Next

        Return True

    End Function

    Private Function CalculatePadprint(Spls As HI.TL.SplashScreen) As Boolean

        Dim _State As Boolean = False
        'Dim _dt As DataTable
        'Dim _UnitSectAmt As Double = 0
        'Dim _UnitSectIncentiveAmt As Double = 0
        'Dim _dtemptime As DataTable
        'Dim _TotalEmp As Integer = 0
        'Dim _TotalTime As Integer = 0
        'Dim _TotalTimeOT As Integer = 0
        'Dim _TotalTimeHR As Integer = 0
        'Dim _TotalTimeOTHR As Integer = 0
        'Dim _Salary As Double = 0
        'Dim _SalaryPerH As Double = 0
        'Dim _SalaryPerOT As Double = 0
        'Dim _CountEmp As Integer = 0
        'Dim _FNAmtNormal As Double = 0
        'Dim _FNAmtOT As Double = 0
        'Dim _FNNetAmt As Double = 0
        'Dim _FixHour As Integer = 0
        'Dim _CountEmpIncentive As Integer
        'Dim _TotalEmpCal As Integer = 0
        'Dim _EmpIncentiveAmt As Double = 0
        'Dim _TotalTimeMin As Integer = 0
        'Dim _TotalEmpAmt As Double = 0

        'Dim _Qry As String = ""
        'With CType(Me.ogc7.DataSource, DataTable)
        '    .AcceptChanges()
        '    _dt = .Copy

        '    .AcceptChanges()
        'End With

        'Dim grpfodata As List(Of Integer) = (_dt.Select("FTSelect='1'", "FNHSysUnitSectId").CopyToDataTable).AsEnumerable() _
        '.Select(Function(r) r.Field(Of Integer)("FNHSysUnitSectId")) _
        '.Distinct() _
        '.ToList()

        'For Each _UnitSect As Integer In grpfodata



        '    Dim grpfodataDate As List(Of String) = (_dt.Select("FTSelect='1' AND FNHSysUnitSectId =" & Val(_UnitSect) & "", "FDScanDateOrg").CopyToDataTable).AsEnumerable() _
        '     .Select(Function(r) r.Field(Of String)("FDScanDateOrg")) _
        '     .Distinct() _
        '     .ToList()

        '    For Each _Date As String In grpfodataDate

        '        '_Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTIncentive "
        '        '_Qry &= vbCrLf & " WHERE  (FTCalDate = '" & HI.UL.ULDate.ConvertEnDB(_Date) & "') "
        '        '_Qry &= vbCrLf & "   AND (FNHSysUnitSectId = " & Integer.Parse(Val(_UnitSect)) & ")"
        '        'HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

        '        '_Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTIncentive_Emp "
        '        '_Qry &= vbCrLf & " WHERE  (FTCalDate = '" & HI.UL.ULDate.ConvertEnDB(_Date) & "') "
        '        '_Qry &= vbCrLf & "   AND (FNHSysUnitSectId = " & Integer.Parse(Val(_UnitSect)) & ")"
        '        'HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)


        '        _dtemptime = GetEmpTime(Integer.Parse(Val(_UnitSect)), _Date).Copy()

        '        _UnitSectAmt = 0
        '        _UnitSectIncentiveAmt = 0
        '        For Each Rx As DataRow In _dt.Select("FNHSysUnitSectId=" & Val(_UnitSect) & " AND FDScanDateOrg='" & HI.UL.ULDate.ConvertEnDB(_Date) & "'")

        '            _UnitSectAmt = _UnitSectAmt + Val(Rx!FNIncentiveAmt.ToString)

        '            _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTIncentive_EmdPrintLaserOther "
        '            _Qry &= vbCrLf & "("
        '            _Qry &= vbCrLf & " FTInsUser, FDInsDate, FTInsTime "
        '            _Qry &= vbCrLf & ", FTCalDate, FNHSysUnitSectId, FTOrderNo, FNHSysPartId, FNQuantity"
        '            _Qry &= vbCrLf & ", FNPrice, FNNetAmt, FNSendSuplType"
        '            _Qry &= vbCrLf & ")"
        '            _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
        '            _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & ""
        '            _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & ""
        '            _Qry &= vbCrLf & " ,'" & HI.UL.ULDate.ConvertEnDB(_Date) & "'"
        '            _Qry &= vbCrLf & " ," & Integer.Parse(Val(_UnitSect)) & " "
        '            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Rx!FTOrderNo.ToString) & "'"
        '            _Qry &= vbCrLf & "," & Val(Rx!FNHSysPartId.ToString) & ""
        '            _Qry &= vbCrLf & " ," & Integer.Parse(Val(Rx!FNQuantity.ToString)) & " "
        '            _Qry &= vbCrLf & " ," & Double.Parse(Val(Rx!FNPrice.ToString)) & " "
        '            _Qry &= vbCrLf & " ," & Double.Parse(Val(Rx!FNIncentiveAmt.ToString)) & " "
        '            _Qry &= vbCrLf & " ," & Integer.Parse(Val(Rx!FNSendSuplType.ToString)) & " "

        '            HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

        '        Next
        '        _TotalEmp = 0
        '        Try
        '            _TotalEmp = _dtemptime.Select(" FNTotalWorkingMin>=0").Length
        '        Catch ex As Exception

        '        End Try

        '        _UnitSectIncentiveAmt = _UnitSectAmt '(Double.Parse(Format(_UnitSectAmt / 3.0, "0.00")) * _TotalEmp)



        '        '-----ตรวจสอบประกันรายชั่วโมง ประกันรายวัน ------------------

        '        _CountEmp = 0
        '        _Salary = 0
        '        _SalaryPerH = 0
        '        _SalaryPerOT = 0
        '        _FNAmtNormal = 0
        '        _FNAmtOT = 0
        '        _FNNetAmt = 0
        '        'Calculate Wage HR Normal
        '        _TotalTime = 0
        '        _TotalTimeOT = 0
        '        _TotalTimeMin = 0
        '        For Each Rt As DataRow In _dtemptime.Rows
        '            _CountEmp = _CountEmp + 1
        '            _TotalTime = _TotalTime + Integer.Parse(Val(Rt!FNWorkingMin.ToString))
        '            _TotalTimeOT = _TotalTimeOT + Integer.Parse(Val(Rt!FNWorkingOTMin.ToString))

        '            _TotalTimeHR = Integer.Parse(Val(Rt!FNTimeMin.ToString))
        '            _TotalTimeOTHR = Integer.Parse(Val(Rt!FNOT1Min.ToString))


        '            _TotalTimeMin = _TotalTimeMin + (Val(Integer.Parse(Val(Rt!FNTotalWorkingMin.ToString))))

        '            _Salary = Double.Parse(Val(Rt!FNSalary.ToString))
        '            _SalaryPerH = Double.Parse(Format(_Salary / 8, "0.00"))
        '            _SalaryPerOT = Double.Parse(Format(_SalaryPerH * 1.5, "0.00"))

        '            If _TotalTimeHR >= 480 Then
        '                _FNAmtNormal = _Salary
        '            Else
        '                _FNAmtNormal = Double.Parse(Format((_Salary / 480) * _TotalTimeHR, "0.00"))
        '            End If

        '            _FNAmtOT = Double.Parse(Format(((_Salary / 480) * 1.5) * _TotalTimeOTHR, "0.00"))

        '            _FNNetAmt = _FNAmtNormal + _FNAmtOT

        '            _Qry = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTIncentive_Emp ("
        '            _Qry &= vbCrLf & "  FTInsUser, FDInsDate, FTInsTime, FTCalDate, FNHSysUnitSectId, FNHSysEmpID"
        '            _Qry &= vbCrLf & ", FNTimeHour01, FNTimeHour02, FNTimeHour03, FNTimeHour04, FNTimeHour05, FNTimeHour06, FNTimeHour07, FNTimeHour08, "
        '            _Qry &= vbCrLf & "   FNTimeHour09, FNTimeHour10, FNTimeHour11, FNTimeHour12, FNTimeHour13, FNTotalTime, FNTotalTimeOT,FNTotalTimHR,FNTotalTimeOTHR"
        '            _Qry &= vbCrLf & ", FNSalary, FNAmtNormal, FNAmtOT, FNNetAmt, FNAmtOldIncentive, FNAmtOTOldIncentive,"
        '            _Qry &= vbCrLf & "  FNNetAmtOldIncentive, FNAmtNewIncentive, FNAmtOTNewIncentive, FNNetAmtNewIncentive,FNQAValue,FTInsuranceHour,FTInsuranceAmt"
        '            _Qry &= vbCrLf & ")"
        '            _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
        '            _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & ""
        '            _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & ""
        '            _Qry &= vbCrLf & " ,'" & _Date & "'   "
        '            _Qry &= vbCrLf & " ," & Integer.Parse(Val(_UnitSect)) & ""
        '            _Qry &= vbCrLf & " ," & Integer.Parse(Val(Rt!FNHSysEmpID.ToString)) & ""
        '            _Qry &= vbCrLf & " ," & Integer.Parse(Val(Rt!FNH01.ToString)) & ""
        '            _Qry &= vbCrLf & " ," & Integer.Parse(Val(Rt!FNH02.ToString)) & ""
        '            _Qry &= vbCrLf & " ," & Integer.Parse(Val(Rt!FNH03.ToString)) & ""
        '            _Qry &= vbCrLf & " ," & Integer.Parse(Val(Rt!FNH04.ToString)) & ""
        '            _Qry &= vbCrLf & " ," & Integer.Parse(Val(Rt!FNH05.ToString)) & ""
        '            _Qry &= vbCrLf & " ," & Integer.Parse(Val(Rt!FNH06.ToString)) & ""
        '            _Qry &= vbCrLf & " ," & Integer.Parse(Val(Rt!FNH07.ToString)) & ""
        '            _Qry &= vbCrLf & " ," & Integer.Parse(Val(Rt!FNH08.ToString)) & ""
        '            _Qry &= vbCrLf & " ," & Integer.Parse(Val(Rt!FNH09.ToString)) & ""
        '            _Qry &= vbCrLf & " ," & Integer.Parse(Val(Rt!FNH10.ToString)) & ""
        '            _Qry &= vbCrLf & " ," & Integer.Parse(Val(Rt!FNH11.ToString)) & ""
        '            _Qry &= vbCrLf & " ," & Integer.Parse(Val(Rt!FNH12.ToString)) & ""
        '            _Qry &= vbCrLf & " ," & Integer.Parse(Val(Rt!FNH13.ToString)) & ""
        '            _Qry &= vbCrLf & " ," & Integer.Parse(Val(Rt!FNWorkingMin.ToString)) & ""
        '            _Qry &= vbCrLf & " ," & Integer.Parse(Val(Rt!FNWorkingOTMin.ToString)) & ""
        '            _Qry &= vbCrLf & " ," & _TotalTimeHR & ""
        '            _Qry &= vbCrLf & " ," & _TotalTimeOTHR & ""
        '            _Qry &= vbCrLf & " ," & _Salary & ""
        '            _Qry &= vbCrLf & " ," & _FNAmtNormal & ""
        '            _Qry &= vbCrLf & " ," & _FNAmtOT & ""
        '            _Qry &= vbCrLf & " ," & _FNNetAmt & ""
        '            _Qry &= vbCrLf & " ,0,0,0,0,0,0,0"
        '            _Qry &= vbCrLf & " ," & _FixHour & ""
        '            _Qry &= vbCrLf & " ,0"

        '            HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

        '        Next

        '        _Qry = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTIncentive ("
        '        _Qry &= vbCrLf & "  FTInsUser, FDInsDate, FTInsTime,  FTCalDate, FNHSysUnitSectId, FNHour01Qty, FNHour02Qty"
        '        _Qry &= vbCrLf & " , FNHour03Qty, FNHour04Qty, FNHour05Qty, FNHour06Qty, FNHour07Qty, "
        '        _Qry &= vbCrLf & "   FNHour08Qty, FNHour09Qty, FNHour10Qty, FNHour11Qty, FNHour12Qty, FNHour13Qty"
        '        _Qry &= vbCrLf & " , FNTotalQty, FTStateInsuranceInH1, FTStateInsuranceInH2, FTStateInsuranceInH3, FTStateInsuranceInH4, FTStateInsuranceInH5,"
        '        _Qry &= vbCrLf & "   FTStateInsuranceInH6, FTStateInsuranceInH7, FTStateInsuranceInH8, FTStateInsuranceInH9, FTStateInsuranceInH10"
        '        _Qry &= vbCrLf & " , FTStateInsuranceInH11, FTStateInsuranceInH12, FTStateInsuranceInH13,"
        '        _Qry &= vbCrLf & "   FTStateInsuranceInDay, FNTotalEmp, FNTotalTime, FNTeamAmt, FNQAPer, FNQAValue, FNTeamNetAmt, FNIncentiveType,FNTeamIncentiveQty,FNTeamIncentiveAmt"
        '        _Qry &= vbCrLf & "  , FNHour01QtySystem, FNHour02QtySystem"
        '        _Qry &= vbCrLf & "  ,  FNHour03QtySystem, FNHour04QtySystem"
        '        _Qry &= vbCrLf & "  , FNHour05QtySystem, FNHour06QtySystem"
        '        _Qry &= vbCrLf & "  , FNHour07QtySystem, FNHour08QtySystem"
        '        _Qry &= vbCrLf & "  , FNHour09QtySystem, FNHour10QtySystem"
        '        _Qry &= vbCrLf & " , FNHour11QtySystem, FNHour12QtySystem"
        '        _Qry &= vbCrLf & "  , FNHour13QtySystem, FNTotalQtySystem"
        '        _Qry &= vbCrLf & ")"
        '        _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
        '        _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & ""
        '        _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & ""
        '        _Qry &= vbCrLf & " ,'" & _Date & "'   "
        '        _Qry &= vbCrLf & " ," & Integer.Parse(Val(_UnitSect)) & ""
        '        _Qry &= vbCrLf & " ,0"
        '        _Qry &= vbCrLf & " ,0"
        '        _Qry &= vbCrLf & " ,0"
        '        _Qry &= vbCrLf & " ,0"
        '        _Qry &= vbCrLf & " ,0"
        '        _Qry &= vbCrLf & " ,0"
        '        _Qry &= vbCrLf & " ,0"
        '        _Qry &= vbCrLf & " ,0"
        '        _Qry &= vbCrLf & " ,0"
        '        _Qry &= vbCrLf & " ,0"
        '        _Qry &= vbCrLf & " ,0"
        '        _Qry &= vbCrLf & " ,0"
        '        _Qry &= vbCrLf & " ,0"
        '        _Qry &= vbCrLf & " ,0"
        '        _Qry &= vbCrLf & " ,'0'"
        '        _Qry &= vbCrLf & " ,'0'"
        '        _Qry &= vbCrLf & " ,'0'"
        '        _Qry &= vbCrLf & " ,'0'"
        '        _Qry &= vbCrLf & " ,'0'"
        '        _Qry &= vbCrLf & " ,'0'"
        '        _Qry &= vbCrLf & " ,'0'"
        '        _Qry &= vbCrLf & " ,'0'"
        '        _Qry &= vbCrLf & " ,'0'"
        '        _Qry &= vbCrLf & " ,'0'"
        '        _Qry &= vbCrLf & " ,'0'"
        '        _Qry &= vbCrLf & " ,'0'"
        '        _Qry &= vbCrLf & " ,'0'"
        '        _Qry &= vbCrLf & " ,'0'"
        '        _Qry &= vbCrLf & " ," & _CountEmp & ""
        '        _Qry &= vbCrLf & " ," & _TotalTime + _TotalTimeOT & ""
        '        _Qry &= vbCrLf & " ," & _UnitSectIncentiveAmt & ""
        '        _Qry &= vbCrLf & " ,0"
        '        _Qry &= vbCrLf & " ,0"
        '        _Qry &= vbCrLf & " ," & _UnitSectIncentiveAmt & ""
        '        _Qry &= vbCrLf & " ,-1"
        '        _Qry &= vbCrLf & " ,0"
        '        _Qry &= vbCrLf & " ," & _UnitSectIncentiveAmt & ""
        '        _Qry &= vbCrLf & " ,0"
        '        _Qry &= vbCrLf & " ,0"
        '        _Qry &= vbCrLf & " ,0"
        '        _Qry &= vbCrLf & " ,0"
        '        _Qry &= vbCrLf & " ,0"
        '        _Qry &= vbCrLf & " ,0"
        '        _Qry &= vbCrLf & " ,0"
        '        _Qry &= vbCrLf & " ,0"
        '        _Qry &= vbCrLf & " ,0"
        '        _Qry &= vbCrLf & " ,0"
        '        _Qry &= vbCrLf & " ,0"
        '        _Qry &= vbCrLf & " ,0"
        '        _Qry &= vbCrLf & " ,0"
        '        _Qry &= vbCrLf & " ,0"

        '        HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

        '        _CountEmpIncentive = _dtemptime.Select("FNTotalWorkingMin>0").Length


        '        For Each Rt As DataRow In _dtemptime.Select("FTStateCal<>'1'  AND FNTotalWorkingMin>0 ", "FNTotalWorkingMin")
        '            _TotalEmpCal = _TotalEmpCal + 1

        '            _Salary = Double.Parse(Val(Rt!FNSalary.ToString))
        '            _SalaryPerH = Double.Parse(Format(_Salary / 8, "0.00"))
        '            _SalaryPerOT = Double.Parse(Format(_SalaryPerH * 1.5, "0.00"))

        '            If _FixHour > 0 Then
        '            End If

        '            If _TotalEmpCal = _CountEmpIncentive Then
        '                _EmpIncentiveAmt = _UnitSectIncentiveAmt - _TotalEmpAmt
        '            Else
        '                _EmpIncentiveAmt = CDbl(Format((_UnitSectIncentiveAmt * (Integer.Parse(Val(Rt!FNTotalWorkingMin.ToString)))) / _TotalTimeMin, "0.00"))
        '            End If

        '            _TotalEmpAmt = _TotalEmpAmt + _EmpIncentiveAmt

        '            _FNAmtOT = Double.Parse(Format(((_Salary / 480) * 0.5) * Integer.Parse(Val(Rt!FNWorkingOTMin.ToString)), "0.00"))

        '            _FNNetAmt = _EmpIncentiveAmt + _FNAmtOT

        '            _Qry = " UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTIncentive_Emp "
        '            _Qry &= vbCrLf & "  SET  FNAmtOldIncentive=" & _EmpIncentiveAmt & ""
        '            _Qry &= vbCrLf & "  , FNAmtOTOldIncentive=" & _FNAmtOT & ""
        '            _Qry &= vbCrLf & "  , FNNetAmtOldIncentive=" & (_FNNetAmt) & ""
        '            _Qry &= vbCrLf & "   ,FNQAValue=0 "
        '            _Qry &= vbCrLf & "   WHERE FNHSysUnitSectId=" & Integer.Parse(Val(_UnitSect)) & ""
        '            _Qry &= vbCrLf & "       AND FNHSysEmpID=" & Integer.Parse(Val(Rt!FNHSysEmpID.ToString)) & ""
        '            _Qry &= vbCrLf & "       AND FTCalDate='" & _Date & "'"


        '            HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)


        '            _Qry = " DELETE FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTWageDaily "
        '            _Qry &= vbCrLf & "  WHERE  FNHSysEmpID=" & Integer.Parse(Val(Rt!FNHSysEmpID.ToString)) & ""
        '            _Qry &= vbCrLf & "    AND  FTDateTrans='" & _Date & "'"
        '            HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

        '            _Qry = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTWageDaily ( "
        '            _Qry &= vbCrLf & "   FTInsUser, FTInsDate, FTInsTime, FNHSysEmpID, FTDateTrans"
        '            _Qry &= vbCrLf & " , FNAmtNormal, FNAmtOT, FNNetAmt, FNProNormal, FNProOT"
        '            _Qry &= vbCrLf & " , FNProOther, FNNetProAmt,FNQAAmt"
        '            _Qry &= vbCrLf & " )"
        '            _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
        '            _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & ""
        '            _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & ""
        '            _Qry &= vbCrLf & " ," & Integer.Parse(Val(Rt!FNHSysEmpID.ToString)) & ""
        '            _Qry &= vbCrLf & " ,'" & _Date & "'   "
        '            _Qry &= vbCrLf & "  ,MAX(FNAmtNormal) AS FNAmtNormal"
        '            _Qry &= vbCrLf & " , MAX(FNAmtOT) AS FNAmtOT"
        '            _Qry &= vbCrLf & " , MAX(FNNetAmt) AS FNNetAmt"
        '            _Qry &= vbCrLf & ", Sum(FNAmtOldIncentive) AS FNAmtOldIncentive"
        '            _Qry &= vbCrLf & ", Sum(FNAmtOTOldIncentive) AS FNAmtOTOldIncentive,0 AS FNProOther"
        '            _Qry &= vbCrLf & ", Sum(FNNetAmtOldIncentive) AS FNNetAmtOldIncentive"
        '            _Qry &= vbCrLf & ", Sum(FNQAValue) AS FNQAValue"
        '            _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTIncentive_Emp  AS X WITH(NOLOCK) "
        '            _Qry &= vbCrLf & "  WHERE  FNHSysEmpID=" & Integer.Parse(Val(Rt!FNHSysEmpID.ToString)) & ""
        '            _Qry &= vbCrLf & "       AND FTCalDate='" & _Date & "'"

        '            HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

        '        Next

        '    Next

        'Next

        Return False

    End Function


    Private Function CalculateCutCP(_Spls As HI.TL.SplashScreen) As Boolean
        Dim _State As Boolean = False
        Dim _dt As DataTable
        Dim _UnitSectAmt As Double = 0
        Dim _UnitSectIncentiveAmt As Double = 0
        Dim _dtemptime As New DataTable
        Dim _TotalEmp As Integer = 0
        Dim _TotalTime As Integer = 0
        Dim _TotalTimeOT As Integer = 0
        Dim _TotalTimeHR As Integer = 0
        Dim _TotalTimeOTHR As Integer = 0

        Dim _FullTotalTimeHR As Integer = 0
        Dim _FullTotalTimeOTHR As Integer = 0

        Dim _Salary As Double = 0
        Dim _SalaryPerH As Double = 0
        Dim _SalaryPerOT As Double = 0
        Dim _CountEmp As Integer = 0
        Dim _FNAmtNormal As Double = 0
        Dim _FNAmtOT As Double = 0
        Dim _FNNetAmt As Double = 0
        Dim _FixHour As Integer = 0
        Dim _CountEmpIncentive As Integer
        Dim _TotalEmpCal As Integer = 0
        Dim _EmpIncentiveAmt As Double = 0
        Dim _TotalTimeMin As Integer = 0
        Dim _TotalEmpAmt As Double = 0
        Dim _CountLineSew As Integer = 0
        Dim _FNTotalWorkingMin As Integer = 0
        Dim _FNTotalWorkingMinAllEmp As Integer = 0

        Dim _Qry As String = ""
        With CType(Me.ogc8.DataSource, DataTable)
            .AcceptChanges()
            _dt = .Copy

            .AcceptChanges()
        End With




        Dim grpfodata As List(Of Integer) = (_dt.Select("FTSelect='1'", "FNHSysUnitSectId").CopyToDataTable).AsEnumerable() _
        .Select(Function(r) r.Field(Of Integer)("FNHSysUnitSectId")) _
        .Distinct() _
        .ToList()

        For Each _UnitSect As Integer In grpfodata



            Dim grpfodataDate As List(Of String) = (_dt.Select("FTSelect='1' AND FNHSysUnitSectId =" & Val(_UnitSect) & "", "FDScanDateOrg").CopyToDataTable).AsEnumerable() _
             .Select(Function(r) r.Field(Of String)("FDScanDateOrg")) _
             .Distinct() _
             .ToList()

            For Each _Date As String In grpfodataDate


                For Each Rxt As DataRow In _dt.Select("FNHSysUnitSectId=" & Val(_UnitSect) & " AND FDScanDateOrg='" & HI.UL.ULDate.ConvertEnDB(_Date) & "'")


                    _Qry = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTIncentive_Style_Cut ("
                    _Qry &= vbCrLf & "  FTInsUser, FDInsDate, FTInsTime, FTCalDate, FNHSysUnitSectId"
                    _Qry &= vbCrLf & ", FNHSysStyleId, FTStyleCode"
                    _Qry &= vbCrLf & ", FNSam "
                    _Qry &= vbCrLf & ", FNMultiple,FNNetCostCut "
                    _Qry &= vbCrLf & ", FNQuantity,FNBudget,FNTotalQuantity,FNTotalBudgetUnitPerDay "
                    _Qry &= vbCrLf & ", FTOrderNo,FTSubOrderNo )"
                    _Qry &= vbCrLf & " Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & ""
                    _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & ""
                    _Qry &= vbCrLf & " ,'" & _Date & "'   "
                    _Qry &= vbCrLf & " ," & Integer.Parse(Val(Rxt!FNHSysUnitSectId.ToString)) & ""
                    _Qry &= vbCrLf & " ," & Integer.Parse(Val(Rxt!FNHSysStyleId.ToString)) & ""
                    _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(Rxt!FTStyleCode.ToString) & "'"

                    _Qry &= vbCrLf & " ," & Double.Parse(Val(Rxt!FNSam.ToString)) & ""
                    _Qry &= vbCrLf & " ," & Double.Parse(Val(Rxt!FNMultiple.ToString)) & ""
                    _Qry &= vbCrLf & " ," & Double.Parse(Val(Rxt!FNNetCostCut.ToString)) & ""
                    _Qry &= vbCrLf & " ," & Double.Parse(Val(Rxt!FNQuantity.ToString)) & ""
                    _Qry &= vbCrLf & " ," & Double.Parse(Val(Rxt!FNBudget.ToString)) & ""
                    _Qry &= vbCrLf & " ," & Integer.Parse(Val(Rxt!FNTotalQuantity.ToString)) & ""
                    _Qry &= vbCrLf & " ," & Double.Parse(Val(Rxt!FNTotalBudgetUnitPerDay.ToString)) & ""

                    _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(Rxt!FTOrderNo.ToString) & "'"
                    _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(Rxt!FTSubOrderNo.ToString) & "'"

                    HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

                Next


                '_Qry = " EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..SP_GET_SCANOUTLINE_CALCULATE_INCENTIVE " & Integer.Parse(Val(R!FNHSysUnitSectId.ToString)) & ",'" & _CalDate & "'"
                'HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

                '_Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTIncentive "
                '_Qry &= vbCrLf & " WHERE  (FTCalDate = '" & HI.UL.ULDate.ConvertEnDB(_Date) & "') "
                '_Qry &= vbCrLf & "   AND (FNHSysUnitSectId = " & Integer.Parse(Val(_UnitSect)) & ")"
                'HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

                '_Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTIncentive_Emp "
                '_Qry &= vbCrLf & " WHERE  (FTCalDate = '" & HI.UL.ULDate.ConvertEnDB(_Date) & "') "
                '_Qry &= vbCrLf & "   AND (FNHSysUnitSectId = " & Integer.Parse(Val(_UnitSect)) & ")"
                'HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)


                _dtemptime = GetEmpTime(Integer.Parse(Val(_UnitSect)), _Date).Copy()

                _UnitSectAmt = 0
                _UnitSectIncentiveAmt = 0
                _TotalEmpAmt = 0
                _FNTotalWorkingMinAllEmp = 0

                For Each Rx As DataRow In _dt.Select("FNHSysUnitSectId=" & Val(_UnitSect) & " AND FDScanDateOrg='" & HI.UL.ULDate.ConvertEnDB(_Date) & "'")
                    _UnitSectIncentiveAmt = Val(Rx!FNTotalBudgetUnitPerDay.ToString)
                    Exit For

                    '' _UnitSectAmt = _UnitSectAmt + Val(Rx!FNIncentiveAmt.ToString)
                    '' _CountLineSew += +1
                Next

                _TotalEmp = 0
                Try
                    _TotalEmp = _dtemptime.Select(" FNTotalWorkingMin>0 and FTStateDaily='0' and FNSalary >0 ").Length
                Catch ex As Exception

                End Try


                For Each T As DataRow In _dtemptime.Rows
                    _FNTotalWorkingMinAllEmp = _FNTotalWorkingMinAllEmp + Val(T!FNTotalWorkingMin.ToString)

                Next

                '' _UnitSectIncentiveAmt = (Double.Parse(Format(_UnitSectAmt / _CountLineSew, "0.00")) * _TotalEmp)



                '-----ตรวจสอบประกันรายชั่วโมง ประกันรายวัน ------------------

                _CountEmp = 0
                _Salary = 0
                _SalaryPerH = 0
                _SalaryPerOT = 0
                _FNAmtNormal = 0
                _FNAmtOT = 0
                _FNNetAmt = 0
                'Calculate Wage HR Normal
                _TotalTime = 0
                _TotalTimeOT = 0
                _TotalTimeMin = 0

                _FNTotalWorkingMin = 0
                For Each Rt As DataRow In _dtemptime.Rows
                    _CountEmp = _CountEmp + 1

                    'Total is product line time
                    _TotalTime = _TotalTime + Integer.Parse(Val(Rt!FNWorkingMin.ToString))
                    _TotalTimeOT = _TotalTimeOT + Integer.Parse(Val(Rt!FNWorkingOTMin.ToString))

                    '_TotalTimeHR = Integer.Parse(Val(Rt!FNTimeMin.ToString))
                    '_TotalTimeOTHR = Integer.Parse(Val(Rt!FNOT1Min.ToString))

                    _TotalTimeHR = 0

                    _TotalTimeHR = _TotalTimeHR + Integer.Parse(Val(Rt!FNH01.ToString))
                    _TotalTimeHR = _TotalTimeHR + Integer.Parse(Val(Rt!FNH02.ToString))
                    _TotalTimeHR = _TotalTimeHR + Integer.Parse(Val(Rt!FNH03.ToString))
                    _TotalTimeHR = _TotalTimeHR + Integer.Parse(Val(Rt!FNH04.ToString))
                    _TotalTimeHR = _TotalTimeHR + Integer.Parse(Val(Rt!FNH05.ToString))
                    _TotalTimeHR = _TotalTimeHR + Integer.Parse(Val(Rt!FNH06.ToString))
                    _TotalTimeHR = _TotalTimeHR + Integer.Parse(Val(Rt!FNH07.ToString))
                    _TotalTimeHR = _TotalTimeHR + Integer.Parse(Val(Rt!FNH08.ToString))

                    _TotalTimeOTHR = 0
                    _TotalTimeOTHR = _TotalTimeOTHR + Integer.Parse(Val(Rt!FNH09.ToString))
                    _TotalTimeOTHR = _TotalTimeOTHR + Integer.Parse(Val(Rt!FNH10.ToString))
                    _TotalTimeOTHR = _TotalTimeOTHR + Integer.Parse(Val(Rt!FNH11.ToString))
                    _TotalTimeOTHR = _TotalTimeOTHR + Integer.Parse(Val(Rt!FNH12.ToString))
                    _TotalTimeOTHR = _TotalTimeOTHR + Integer.Parse(Val(Rt!FNH13.ToString))


                    'Full is HR

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


                    '_FNTotalWorkingMin = เวลาทำงานจริง
                    _FNTotalWorkingMin = (Val(Integer.Parse(Val(Rt!FNTotalWorkingMin.ToString))))

                    If Rt!FTStateDaily.ToString = "0" Then
                        If Double.Parse("0" & Rt!FNSalary.ToString) > 0 Then
                            _TotalTimeMin = _TotalTimeMin + (Val(Integer.Parse(Val(Rt!FNTotalWorkingMin.ToString))))
                        End If
                    End If


                    _Salary = Double.Parse(Val(Rt!FNSalary.ToString))
                    _SalaryPerH = Double.Parse(Format(_Salary / 8, "0.00"))
                    _SalaryPerOT = Double.Parse(Format(_SalaryPerH * 1.5, "0.00"))

                    '  Time HR
                    If _TotalTimeHR >= 480 Then
                        _FNAmtNormal = _Salary
                    Else
                        _FNAmtNormal = Double.Parse(Format((_Salary / 480) * _FullTotalTimeHR, "0.00"))
                    End If

                    _FNAmtOT = Double.Parse(Format(((_Salary / 480) * 1.5) * _FullTotalTimeOTHR, "0.00"))


                    'Time production line
                    'If _TotalTimeHR >= 480 Then
                    '    _FNAmtNormal = _Salary
                    'Else
                    '    _FNAmtNormal = Double.Parse(Format((_Salary / 480) * _TotalTimeHR, "0.00"))
                    'End If

                    '_FNAmtOT = Double.Parse(Format(((_Salary / 480) * 1.5) * _TotalTimeOTHR, "0.00"))

                    _FNNetAmt = _FNAmtNormal + _FNAmtOT

                    _Qry = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTIncentive_Emp ("
                    _Qry &= vbCrLf & "  FTInsUser, FDInsDate, FTInsTime, FTCalDate, FNHSysUnitSectId, FNHSysEmpID"
                    _Qry &= vbCrLf & ", FNTimeHour01, FNTimeHour02, FNTimeHour03, FNTimeHour04, FNTimeHour05, FNTimeHour06, FNTimeHour07, FNTimeHour08, "
                    _Qry &= vbCrLf & "   FNTimeHour09, FNTimeHour10, FNTimeHour11, FNTimeHour12, FNTimeHour13, FNTotalTime, FNTotalTimeOT,FNTotalTimHR,FNTotalTimeOTHR"
                    _Qry &= vbCrLf & ", FNSalary, FNAmtNormal, FNAmtOT, FNNetAmt, FNAmtOldIncentive, FNAmtOTOldIncentive,"
                    _Qry &= vbCrLf & "  FNNetAmtOldIncentive, FNAmtNewIncentive, FNAmtOTNewIncentive, FNNetAmtNewIncentive,FNQAValue,FTInsuranceHour,FTInsuranceAmt"
                    _Qry &= vbCrLf & ", FNTotalTimHRFull, FNTotalTimeOTHRFull "
                    _Qry &= vbCrLf & ")"
                    _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & ""
                    _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & ""
                    _Qry &= vbCrLf & " ,'" & _Date & "'   "
                    _Qry &= vbCrLf & " ," & Integer.Parse(Val(_UnitSect)) & ""
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
                    'Product line Time
                    _Qry &= vbCrLf & " ," & _TotalTimeHR & ""
                    _Qry &= vbCrLf & " ," & _TotalTimeOTHR & ""
                    'HR Time
                    _Qry &= vbCrLf & " ," & Integer.Parse(Val(Rt!FNWorkingMin.ToString)) & ""
                    _Qry &= vbCrLf & " ," & Integer.Parse(Val(Rt!FNWorkingOTMin.ToString)) & ""

                    _Qry &= vbCrLf & " ," & _Salary & ""
                    _Qry &= vbCrLf & " ," & _FNAmtNormal & ""
                    _Qry &= vbCrLf & " ," & _FNAmtOT & ""
                    _Qry &= vbCrLf & " ," & _FNNetAmt & ""
                    _Qry &= vbCrLf & " ,0,0,0,0,0,0,0"
                    _Qry &= vbCrLf & " ," & _FixHour & ""
                    _Qry &= vbCrLf & " ,0"
                    _Qry &= vbCrLf & " ," & _FullTotalTimeHR & ""
                    _Qry &= vbCrLf & " ," & _FullTotalTimeOTHR

                    HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

                Next

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
                _Qry &= vbCrLf & " ,'" & _Date & "'   "
                _Qry &= vbCrLf & " ," & Integer.Parse(Val(_UnitSect)) & ""
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,'0'"
                _Qry &= vbCrLf & " ,'0'"
                _Qry &= vbCrLf & " ,'0'"
                _Qry &= vbCrLf & " ,'0'"
                _Qry &= vbCrLf & " ,'0'"
                _Qry &= vbCrLf & " ,'0'"
                _Qry &= vbCrLf & " ,'0'"
                _Qry &= vbCrLf & " ,'0'"
                _Qry &= vbCrLf & " ,'0'"
                _Qry &= vbCrLf & " ,'0'"
                _Qry &= vbCrLf & " ,'0'"
                _Qry &= vbCrLf & " ,'0'"
                _Qry &= vbCrLf & " ,'0'"
                _Qry &= vbCrLf & " ,'0'"
                _Qry &= vbCrLf & " ," & _CountEmp & ""
                _Qry &= vbCrLf & " ," & _TotalTime + _TotalTimeOT & ""
                _Qry &= vbCrLf & " ," & _UnitSectIncentiveAmt & ""
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ," & _UnitSectIncentiveAmt & ""
                _Qry &= vbCrLf & " ,-1"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ," & _UnitSectIncentiveAmt & ""
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"
                _Qry &= vbCrLf & " ,0"

                HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

                _CountEmpIncentive = _dtemptime.Select("FNTotalWorkingMin>0").Length


                For Each Rt As DataRow In _dtemptime.Select("FTStateCal <>'1'  AND FNTotalWorkingMin>0 ", "FNTotalWorkingMin")
                    _TotalEmpCal = _TotalEmpCal + 1

                    _Salary = Double.Parse(Val(Rt!FNSalary.ToString))
                    _SalaryPerH = Double.Parse(Format(_Salary / 8, "0.00"))
                    _SalaryPerOT = Double.Parse(Format(_SalaryPerH * 1.5, "0.00"))

                    If _FixHour > 0 Then
                    End If

                    'If _TotalEmpCal = _CountEmpIncentive Then
                    '    _EmpIncentiveAmt = _UnitSectIncentiveAmt - _TotalEmpAmt
                    'Else
                    '    _EmpIncentiveAmt = CDbl(Format((_UnitSectIncentiveAmt * (Integer.Parse(Val(Rt!FNTotalWorkingMin.ToString)))) / _TotalTimeMin, "0.00"))
                    'End If
                    '_EmpIncentiveAmt = CDbl(Format((_UnitSectIncentiveAmt * (Integer.Parse(Val(Rt!FNTotalWorkingMin.ToString)))) / _TotalTimeMin, "0.00")
                    If _FNTotalWorkingMinAllEmp > 0 Then
                        '_EmpIncentiveAmt = CDbl(Format((_UnitSectIncentiveAmt / _FNTotalWorkingMinAllEmp) * _FNTotalWorkingMin, "0.0000"))
                        _EmpIncentiveAmt = CDbl(Format((_UnitSectIncentiveAmt / _FNTotalWorkingMinAllEmp) * (Integer.Parse(Val(Rt!FNTotalWorkingMin.ToString))), "0.0000"))
                    End If
                    '_EmpIncentiveAmt = CDbl(Format(((_UnitSectIncentiveAmt / _FNTotalWorkingMinAllEmp) * _TotalTimeMin), "0.00"))

                    If Rt!FTStateDaily.ToString = "1" Then
                        _EmpIncentiveAmt = 0
                    End If

                    _TotalEmpAmt = _TotalEmpAmt + _EmpIncentiveAmt

                    _FNAmtOT = Double.Parse(Format(((_Salary / 480) * 0.5) * Integer.Parse(Val(Rt!FNWorkingOTMin.ToString)), "0.00"))

                    _FNNetAmt = _EmpIncentiveAmt + _FNAmtOT

                    '_Qry = " UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTIncentive_Emp "
                    '_Qry &= vbCrLf & "  SET  FNAmtOldIncentive=" & _EmpIncentiveAmt & ""
                    '_Qry &= vbCrLf & "  , FNAmtOTOldIncentive=" & _FNAmtOT & ""
                    '_Qry &= vbCrLf & "  , FNNetAmtOldIncentive=" & (_FNNetAmt) & ""
                    '_Qry &= vbCrLf & "   ,FNQAValue=0 "
                    '_Qry &= vbCrLf & "   WHERE FNHSysUnitSectId=" & Integer.Parse(Val(_UnitSect)) & ""
                    '_Qry &= vbCrLf & "       AND FNHSysEmpID=" & Integer.Parse(Val(Rt!FNHSysEmpID.ToString)) & ""
                    '_Qry &= vbCrLf & "       AND FTCalDate='" & _Date & "'"

                    'FNAmtNormal, FNAmtOT, FNNetAmt
                    _Qry = " UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTIncentive_Emp "
                    _Qry &= vbCrLf & "  SET  FNAmtOldIncentive=" & _EmpIncentiveAmt & ""
                    _Qry &= vbCrLf & "  , FNAmtOTOldIncentive=" & _FNAmtOT & ""
                    _Qry &= vbCrLf & "  , FNNetAmtOldIncentive=" & (_FNNetAmt) & ""
                    _Qry &= vbCrLf & "   ,FNQAValue=0 "
                    '_Qry &= vbCrLf & "   ,FNNetAmt=FNNetAmt+" & (_EmpIncentiveAmt) & ""
                    _Qry &= vbCrLf & "   WHERE FNHSysUnitSectId=" & Integer.Parse(Val(_UnitSect)) & ""
                    _Qry &= vbCrLf & "       AND FNHSysEmpID=" & Integer.Parse(Val(Rt!FNHSysEmpID.ToString)) & ""
                    _Qry &= vbCrLf & "       AND FTCalDate='" & _Date & "'"

                    HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)


                    _Qry = " DELETE FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTWageDaily "
                    _Qry &= vbCrLf & "  WHERE  FNHSysEmpID=" & Integer.Parse(Val(Rt!FNHSysEmpID.ToString)) & ""
                    _Qry &= vbCrLf & "    AND  FTDateTrans='" & _Date & "'"
                    HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

                    ''
                    ''Dim _EmpTypeId As Integer = HI.Conn.SQLConn.GetField("Select Top 1 FNHSysEmpTypeId From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee with(nolock) where FNHSysEmpID=" & Integer.Parse(Val(Rt!FNHSysEmpID.ToString)) & "", Conn.DB.DataBaseName.DB_HR, "0")
                    ''HI.HRCAL.Calculate.CalculateWageDaily(HI.ST.UserInfo.UserName, Rt!FNHSysEmpID.ToString, _EmpTypeId.ToString(), HI.UL.ULDate.ConvertEnDB(_Date), HI.UL.ULDate.ConvertEnDB(_Date))

                    _Qry = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTWageDaily ( "
                    _Qry &= vbCrLf & "   FTInsUser, FTInsDate, FTInsTime, FNHSysEmpID, FTDateTrans"
                    _Qry &= vbCrLf & " , FNAmtNormal, FNAmtOT, FNNetAmt, FNProNormal, FNProOT"
                    _Qry &= vbCrLf & " , FNProOther, FNNetProAmt,FNQAAmt"
                    _Qry &= vbCrLf & " )"
                    _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & ""
                    _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & ""
                    _Qry &= vbCrLf & " ," & Integer.Parse(Val(Rt!FNHSysEmpID.ToString)) & ""
                    _Qry &= vbCrLf & " ,'" & _Date & "'   "
                    _Qry &= vbCrLf & "  ,MAX(FNAmtNormal) AS FNAmtNormal"
                    _Qry &= vbCrLf & " , MAX(FNAmtOT) AS FNAmtOT"
                    _Qry &= vbCrLf & " , MAX(FNNetAmt) AS FNNetAmt"
                    _Qry &= vbCrLf & ", Sum(FNAmtOldIncentive) AS FNAmtOldIncentive"
                    _Qry &= vbCrLf & ", Sum(FNAmtOTOldIncentive) AS FNAmtOTOldIncentive,0 AS FNProOther"
                    _Qry &= vbCrLf & ", Sum(FNNetAmtOldIncentive) AS FNNetAmtOldIncentive"
                    _Qry &= vbCrLf & ", Sum(FNQAValue) AS FNQAValue"
                    _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTIncentive_Emp  AS X WITH(NOLOCK) "
                    _Qry &= vbCrLf & "  WHERE  FNHSysEmpID=" & Integer.Parse(Val(Rt!FNHSysEmpID.ToString)) & ""
                    _Qry &= vbCrLf & "       AND FTCalDate='" & _Date & "'"

                    HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

                Next



                If _UnitSectIncentiveAmt > _TotalEmpAmt Then

                    Dim MaxMinute As Integer = 0
                    Dim SPareAmont As Decimal = _UnitSectIncentiveAmt - _TotalEmpAmt


                    For Each Rt As DataRow In _dtemptime.Select("FTStateCal<>'1'  AND FNTotalWorkingMin>0 ", "FNTotalWorkingMin ")
                        MaxMinute = Integer.Parse(Val(Rt!FNTotalWorkingMin.ToString))


                    Next

                    If MaxMinute > 0 Then
                        _TotalEmpCal = 0
                        For Each Rt As DataRow In _dtemptime.Select("FTStateCal<>'1'  AND FNTotalWorkingMin>=" & MaxMinute & " ", "FNTotalWorkingMin ")
                            _TotalEmpCal = _TotalEmpCal + 1
                        Next
                        Dim EmpSPareAmont As Decimal = CDbl(Format(SPareAmont / _TotalEmpCal, "0.00"))


                        If (EmpSPareAmont * _TotalEmpCal) < SPareAmont Then
                            EmpSPareAmont = EmpSPareAmont + 0.01
                        End If


                        For Each Rt As DataRow In _dtemptime.Select("FTStateCal<>'1'  AND FNTotalWorkingMin>=" & MaxMinute & " ", "FNTotalWorkingMin ")

                            _Qry = " UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTIncentive_Emp "
                            _Qry &= vbCrLf & "  SET  FNAmtOldIncentive=FNAmtOldIncentive+" & EmpSPareAmont & ""
                            _Qry &= vbCrLf & "  , FNNetAmtOldIncentive=FNNetAmtOldIncentive+" & EmpSPareAmont & ""
                            _Qry &= vbCrLf & "   WHERE FNHSysUnitSectId=" & _UnitSect & ""
                            _Qry &= vbCrLf & "       AND FNHSysEmpID=" & Integer.Parse(Val(Rt!FNHSysEmpID.ToString)) & ""
                            _Qry &= vbCrLf & "       AND FTCalDate='" & _Date & "'"

                            HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

                            _Qry = " UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTWageDaily "
                            _Qry &= vbCrLf & "  SET  FNProNormal=FNProNormal+" & EmpSPareAmont & ""
                            _Qry &= vbCrLf & "  , FNNetProAmt=FNNetProAmt+" & EmpSPareAmont & ""
                            _Qry &= vbCrLf & "   WHERE FNHSysEmpID=" & Integer.Parse(Val(Rt!FNHSysEmpID.ToString)) & ""
                            _Qry &= vbCrLf & "       AND FTDateTrans='" & _Date & "'"

                            HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)
                        Next

                    End If



                End If



            Next

        Next




        Return True

    End Function

#End Region

    Private Function DeleteData(Spls As HI.TL.SplashScreen) As Boolean
        Dim _dt As DataTable
        Dim _CalDate As String
        Dim _Qry As String = ""

        Select Case FNCalculateIncentiveType.SelectedIndex
            Case 0

                With CType(ogc.DataSource, DataTable)
                    .AcceptChanges()
                    _dt = .Copy()
                End With

            Case 1

                With CType(ogc1.DataSource, DataTable)
                    .AcceptChanges()
                    _dt = .Copy()
                End With

            Case 2

                With CType(ogc2.DataSource, DataTable)
                    .AcceptChanges()
                    _dt = .Copy()
                End With

            Case 3

                With CType(ogc3.DataSource, DataTable)
                    .AcceptChanges()
                    _dt = .Copy()
                End With

            Case 4

                With CType(ogc4.DataSource, DataTable)
                    .AcceptChanges()
                    _dt = .Copy()
                End With

            Case 5

                With CType(ogc5.DataSource, DataTable)
                    .AcceptChanges()
                    _dt = .Copy()
                End With

            Case 6

                With CType(ogc6.DataSource, DataTable)
                    .AcceptChanges()
                    _dt = .Copy()
                End With

            Case 7

                With CType(ogc7.DataSource, DataTable)
                    .AcceptChanges()
                    _dt = .Copy()
                End With
            Case 8

                With CType(ogc8.DataSource, DataTable)
                    .AcceptChanges()
                    _dt = .Copy()
                End With

            Case Else

                With CType(ogc.DataSource, DataTable)
                    .AcceptChanges()
                    _dt = .Copy()
                End With

        End Select

        Try

            For Each R As DataRow In _dt.Select("FTSelect='1'", "FDScanDate,FTUnitSectCode")

                _CalDate = HI.UL.ULDate.ConvertEnDB(R!FDScanDateOrg.ToString)

                _Qry = " DELETE  A  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTWageDaily AS A  "
                _Qry &= vbCrLf & " INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTIncentive_Emp  AS X WITH(NOLOCK) ON A.FTDateTrans=X.FTCalDate AND A.FNHSysEmpID =X.FNHSysEmpID "
                _Qry &= vbCrLf & " WHERE  (X.FTCalDate = '" & HI.UL.ULDate.ConvertEnDB(R!FDScanDateOrg.ToString) & "') "
                _Qry &= vbCrLf & "   AND (X.FNHSysUnitSectId = " & Integer.Parse(Val(R!FNHSysUnitSectId.ToString)) & ")"
                HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

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

                _Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTIncentive_Style_Hour "
                _Qry &= vbCrLf & " WHERE  (FTCalDate = '" & HI.UL.ULDate.ConvertEnDB(R!FDScanDateOrg.ToString) & "') "
                _Qry &= vbCrLf & "   AND (FNHSysUnitSectId = " & Integer.Parse(Val(R!FNHSysUnitSectId.ToString)) & ")"
                HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

                _Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTIncentive_Style_Hour_Level "
                _Qry &= vbCrLf & " WHERE  (FTCalDate = '" & HI.UL.ULDate.ConvertEnDB(R!FDScanDateOrg.ToString) & "') "
                _Qry &= vbCrLf & "   AND (FNHSysUnitSectId = " & Integer.Parse(Val(R!FNHSysUnitSectId.ToString)) & ")"
                HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

                _Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTIncentive_Emp_Deduct "
                _Qry &= vbCrLf & " WHERE  (FTCalDate = '" & HI.UL.ULDate.ConvertEnDB(R!FDScanDateOrg.ToString) & "') "
                _Qry &= vbCrLf & "   AND (FNHSysUnitSectId = " & Integer.Parse(Val(R!FNHSysUnitSectId.ToString)) & ")"
                HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

                _Qry = "DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTIncentive_Emp_Wring_Detail "
                _Qry &= vbCrLf & " WHERE FNHSysUnitSectId  =" & Integer.Parse(Val(R!FNHSysUnitSectId.ToString)) & " "
                _Qry &= vbCrLf & " AND FTCalDate='" & _CalDate & "'  "
                HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

                _Qry = "DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTIncentive_EmdPrintLaserOther "
                _Qry &= vbCrLf & " WHERE FNHSysUnitSectId  =" & Integer.Parse(Val(R!FNHSysUnitSectId.ToString)) & " "
                _Qry &= vbCrLf & " AND FTCalDate='" & _CalDate & "'  "
                HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

                _Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTIncentive_Style_Cut "
                _Qry &= vbCrLf & " WHERE  (FTCalDate = '" & HI.UL.ULDate.ConvertEnDB(R!FDScanDateOrg.ToString) & "') "
                _Qry &= vbCrLf & "   AND (FNHSysUnitSectId = " & Integer.Parse(Val(R!FNHSysUnitSectId.ToString)) & ")"
                HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

            Next

            _dt.Dispose()

        Catch ex As Exception
        End Try
        Return True
    End Function

    Private Function VerrifyData() As Boolean
        Dim _Pass As Boolean = False


        Dim _StateSelect As Integer = 0

        Try

            Select Case Me.FNCalculateIncentiveType.SelectedIndex
                Case 0

                    _StateSelect = CType(ogc.DataSource, DataTable).Select("FTSelect='1'").Length

                Case 1

                    _StateSelect = CType(ogc1.DataSource, DataTable).Select("FTSelect='1'").Length

                Case 2

                    _StateSelect = CType(ogc2.DataSource, DataTable).Select("FTSelect='1'").Length

                Case 3

                    _StateSelect = CType(ogc3.DataSource, DataTable).Select("FTSelect='1'").Length

                Case 4

                Case 5

                Case 6

                Case 7

                Case 8
                    _StateSelect = CType(ogc8.DataSource, DataTable).Select("FTSelect='1'").Length
                Case Else

                    _StateSelect = CType(ogc.DataSource, DataTable).Select("FTSelect='1'").Length

            End Select

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

        Dim _Dt As DataTable = Nothing
        Dim _DtShow As DataTable
        Dim _Qry As String = ""
        Dim _SDate As String = HI.UL.ULDate.ConvertEnDB(FTStartDate.Text)
        Dim _EDate As String = HI.UL.ULDate.ConvertEnDB(FTEndDate.Text)

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
        _Qry &= vbCrLf & "  WHERE  (FTStateSew = '1') AND (FTStateActive = '1') AND  (ISNULL(FTStateSampleRoom,'0') <> '1') "

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

            _SDate = HI.UL.ULDate.ConvertEnDB(FTStartDate.Text)
            _EDate = HI.UL.ULDate.ConvertEnDB(FTEndDate.Text)

            _Spls.UpdateInformation("Loading Data of Line " & Rx!FTUnitSectCode.ToString & "  ( " & _PLine.ToString & " of  " & _TotalLine.ToString & " )")

            Do While _SDate <= _EDate

                _Qry = "Exec [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.SP_GET_SCANOUTLINE_INCENTIVE " & Val(Rx!FNHSysUnitSectId.ToString) & ",'" & _SDate & "','" & _SDate & "' "
                dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

                If _Dt Is Nothing Then
                    _Dt = dt.Copy
                Else
                    _Dt.Merge(dt.Copy)
                End If

                _SDate = HI.UL.ULDate.ConvertEnDB(HI.UL.ULDate.AddDay(_SDate, 1))

            Loop

        Next

        _Spls.Close()

        dtline.Dispose()
        dt.Dispose()

        Try
            _DtShow = _Dt.Select("FTUnitSectCode<>''", "FDScanDateOrg,FTUnitSectCode").CopyToDataTable
        Catch ex As Exception
            _DtShow = Nothing
        End Try

        Dim _CalDate As String = ""
        Dim _dtemptime As DataTable
        Dim _MaxOT As Integer = 0
        Dim _QtyOTMo As Integer = 0
        For Each R As DataRow In _DtShow.Rows

            _MaxOT = 0
            _CalDate = HI.UL.ULDate.ConvertEnDB(R!FDScanDateOrg.ToString)
            _dtemptime = GetEmpTime(Integer.Parse(Val(R!FNHSysUnitSectId.ToString)), R!FDScanDateOrg.ToString, 0).Copy()
            For Each Rx As DataRow In _dtemptime.Select("FNWorkingOTMin>0", "FNWorkingOTMin DESC ")
                _MaxOT = +Integer.Parse(Val(Rx!FNWorkingOTMin.ToString))
                Exit For
            Next

            Select Case True
                Case (_MaxOT = 0)

                    ' _QtyOTMo = Integer.Parse(Val(R!H09.ToString)) + Integer.Parse(Val(R!H10.ToString)) + Integer.Parse(Val(R!H11.ToString)) + Integer.Parse(Val(R!H12.ToString)) + Integer.Parse(Val(R!H13.ToString))

                    R!H09 = 0
                    R!H10 = 0
                    R!H11 = 0
                    R!H12 = 0
                    R!H13 = 0
                   ' R!H08 = Integer.Parse(Val(R!H08.ToString)) + _QtyOTMo

                Case _MaxOT > 0 And _MaxOT <= 60
                    ' _QtyOTMo = Integer.Parse(Val(R!H10.ToString)) + Integer.Parse(Val(R!H11.ToString)) + Integer.Parse(Val(R!H12.ToString)) + Integer.Parse(Val(R!H13.ToString))

                    R!H10 = 0
                    R!H11 = 0
                    R!H12 = 0
                    R!H13 = 0
                  '  R!H09 = Integer.Parse(Val(R!H09.ToString)) + _QtyOTMo

                Case _MaxOT > 60 And _MaxOT <= 120
                    ' _QtyOTMo = Integer.Parse(Val(R!H11.ToString)) + Integer.Parse(Val(R!H12.ToString)) + Integer.Parse(Val(R!H13.ToString))

                    R!H11 = 0
                    R!H12 = 0
                    R!H13 = 0
                  '  R!H10 = Integer.Parse(Val(R!H10.ToString)) + _QtyOTMo

                Case _MaxOT > 120 And _MaxOT <= 180

                    ' _QtyOTMo = Integer.Parse(Val(R!H12.ToString)) + Integer.Parse(Val(R!H13.ToString))

                    R!H12 = 0
                    R!H13 = 0
                  '  R!H11 = Integer.Parse(Val(R!H11.ToString)) + _QtyOTMo

                Case _MaxOT > 180 And _MaxOT <= 240

                    ' _QtyOTMo = Integer.Parse(Val(R!H13.ToString))
                    R!H13 = 0
                   ' R!H12 = Integer.Parse(Val(R!H12.ToString)) + _QtyOTMo

                Case _MaxOT > 240

            End Select

        Next

        For Each R As DataRow In _DtShow.Rows
            R!Total = Val(R!H01) + Val(R!H02) + Val(R!H03) + Val(R!H04) + Val(R!H05) + Val(R!H06) + Val(R!H07) + Val(R!H08) + Val(R!H09) + Val(R!H10) + Val(R!H11) + Val(R!H12) + Val(R!H13)
        Next

        Me.ogc.DataSource = _DtShow.Copy
        Me.ogv.ExpandAllGroups()
        _DtShow.Dispose()
        _Dt.Dispose()

    End Sub

    Private Sub LoadDataBUCutInfo()
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
        _Qry &= vbCrLf & "  WHERE  (FTStateCut = '1') AND (FTStateActive = '1')"

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

            _Qry = "Exec [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.SP_GET_CUTBU_CAL_INCENTIVE " & Val(Rx!FNHSysUnitSectId.ToString) & ",'" & HI.UL.ULDate.ConvertEnDB(FTStartDate.Text) & "','" & HI.UL.ULDate.ConvertEnDB(FTEndDate.Text) & "' "
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
            _DtShow = _Dt.Select("FTUnitSectCode<>''", "FTUnitSectCode,FDScanDate,FNIncentiveAmt DESC").CopyToDataTable
        Catch ex As Exception
            _DtShow = Nothing
        End Try

        Me.ogc1.DataSource = _DtShow.Copy
        Me.ogv1.ExpandAllGroups()
        _DtShow.Dispose()
        _Dt.Dispose()

    End Sub

    Private Sub LoadDataStockFabricInfo()
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
        _Qry &= vbCrLf & "  WHERE  (FTStateStockFabric = '1') AND (FTStateActive = '1')"

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

            _Qry = "Exec [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.SP_GET_STOCKFABRIC_CAL_INCENTIVE " & Val(Rx!FNHSysUnitSectId.ToString) & ",'" & HI.UL.ULDate.ConvertEnDB(FTStartDate.Text) & "','" & HI.UL.ULDate.ConvertEnDB(FTEndDate.Text) & "' "
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
            _DtShow = _Dt.Select("FTUnitSectCode<>''", "FTUnitSectCode,FDScanDate").CopyToDataTable
        Catch ex As Exception
            _DtShow = Nothing
        End Try

        Me.ogc2.DataSource = _DtShow.Copy
        Me.ogv2.ExpandAllGroups()

        _DtShow.Dispose()
        _Dt.Dispose()

    End Sub

    Private Sub LoadDataCutAuto()
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
        _Qry &= vbCrLf & "  WHERE  (FTStateCutAuto = '1') AND (FTStateActive = '1')"

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

            _Qry = "Exec [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.SP_GET_CUTAUTO_CAL_INCENTIVE " & Val(Rx!FNHSysUnitSectId.ToString) & ",'" & HI.UL.ULDate.ConvertEnDB(FTStartDate.Text) & "','" & HI.UL.ULDate.ConvertEnDB(FTEndDate.Text) & "' "
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
            _DtShow = _Dt.Select("FTUnitSectCode<>''", "FTUnitSectCode,FDScanDate").CopyToDataTable
        Catch ex As Exception
            _DtShow = Nothing
        End Try

        Me.ogc3.DataSource = _DtShow.Copy
        Me.ogv3.ExpandAllGroups()

        _DtShow.Dispose()
        _Dt.Dispose()

    End Sub

    Private Sub LoadDataHeat()
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
        _Qry &= vbCrLf & "  WHERE  (FTStateHeatTransfer = '1') AND (FTStateActive = '1')"

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

            _Qry = "Exec [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.SP_GET_HEAT_CAL_INCENTIVE " & Val(Rx!FNHSysUnitSectId.ToString) & ",'" & HI.UL.ULDate.ConvertEnDB(FTStartDate.Text) & "','" & HI.UL.ULDate.ConvertEnDB(FTEndDate.Text) & "' "
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
            _DtShow = _Dt.Select("FTUnitSectCode<>''", "FTUnitSectCode,FDScanDate").CopyToDataTable
        Catch ex As Exception
            _DtShow = Nothing
        End Try

        Me.ogc4.DataSource = _DtShow.Copy
        Me.ogv4.ExpandAllGroups()

        _DtShow.Dispose()
        _Dt.Dispose()

    End Sub

    Private Sub LoadDataEmbPrint()
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
        _Qry &= vbCrLf & "  WHERE  (FTStateEmpPrint = '1' OR FTStateEmbroidery='1') AND (FTStateActive = '1')"

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

            _Qry = "Exec [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.SP_GET_EMB_PRINT_CAL_INCENTIVE " & Val(Rx!FNHSysUnitSectId.ToString) & ",'" & HI.UL.ULDate.ConvertEnDB(FTStartDate.Text) & "','" & HI.UL.ULDate.ConvertEnDB(FTEndDate.Text) & "' "
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
            _DtShow = _Dt.Select("FTUnitSectCode<>''", "FTUnitSectCode,FDScanDate").CopyToDataTable
        Catch ex As Exception
            _DtShow = Nothing
        End Try

        Me.ogc5.DataSource = _DtShow.Copy
        Me.ogv5.ExpandAllGroups()

        _DtShow.Dispose()
        _Dt.Dispose()

    End Sub

    Private Sub LoadDataLaser()
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
        _Qry &= vbCrLf & "  WHERE  (FTStateLaser = '1') AND (FTStateActive = '1')"

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

            _Qry = "Exec [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.SP_GET_LASER_CAL_INCENTIVE " & Val(Rx!FNHSysUnitSectId.ToString) & ",'" & HI.UL.ULDate.ConvertEnDB(FTStartDate.Text) & "','" & HI.UL.ULDate.ConvertEnDB(FTEndDate.Text) & "' "
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
            _DtShow = _Dt.Select("FTUnitSectCode<>''", "FTUnitSectCode,FDScanDate").CopyToDataTable
        Catch ex As Exception
            _DtShow = Nothing
        End Try

        Me.ogc6.DataSource = _DtShow.Copy
        Me.ogv6.ExpandAllGroups()

        _DtShow.Dispose()
        _Dt.Dispose()

    End Sub

    Private Sub LoadDataPadPrint()
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
        _Qry &= vbCrLf & "  WHERE  (FTStatePadPrint = '1') AND (FTStateActive = '1')"

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

            _Qry = "Exec [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.SP_GET_PADPRINT_CAL_INCENTIVE " & Val(Rx!FNHSysUnitSectId.ToString) & ",'" & HI.UL.ULDate.ConvertEnDB(FTStartDate.Text) & "','" & HI.UL.ULDate.ConvertEnDB(FTEndDate.Text) & "' "
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
            _DtShow = _Dt.Select("FTUnitSectCode<>''", "FTUnitSectCode,FDScanDate").CopyToDataTable
        Catch ex As Exception
            _DtShow = Nothing
        End Try

        Me.ogc7.DataSource = _DtShow.Copy
        Me.ogv7.ExpandAllGroups()

        _DtShow.Dispose()
        _Dt.Dispose()

    End Sub

    Private Sub LoadDataCPCutInfo()
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

        '_Qry = "SELECT FNHSysUnitSectId, FTUnitSectCode"
        '_Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS X WITH(NOLOCK)"
        '_Qry &= vbCrLf & "  WHERE  (FTStateCut = '1') AND (FTStateActive = '1')"

        'If FNHSysUnitSectId.Text <> "" Then
        '    _Qry &= vbCrLf & "  AND FTUnitSectCode >='" & HI.UL.ULF.rpQuoted(FNHSysUnitSectId.Text) & "'"
        'End If

        'If FNHSysUnitSectIdTo.Text <> "" Then
        '    _Qry &= vbCrLf & "  AND FTUnitSectCode <='" & HI.UL.ULF.rpQuoted(FNHSysUnitSectIdTo.Text) & "'"
        'End If

        '_Qry &= vbCrLf & "  ORDER BY FTUnitSectCode"
        'dtline = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MASTER)
        '_TotalLine = dtline.Rows.Count

        'For Each Rx As DataRow In dtline.Rows
        '    _PLine = _PLine + 1

        '    _Spls.UpdateInformation("Loading Data of Line " & Rx!FTUnitSectCode.ToString & "  ( " & _PLine.ToString & " of  " & _TotalLine.ToString & " )")

        '    _Qry = "Exec [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.SP_CALCULATE_STATIC_INCENTIVE_BUDGET_CPCUT " & Val(Rx!FNHSysUnitSectId.ToString) & ",'" & HI.UL.ULDate.ConvertEnDB(FTStartDate.Text) & "','" & HI.UL.ULDate.ConvertEnDB(FTEndDate.Text) & "' "
        '    dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

        '    If _Dt Is Nothing Then
        '        _Dt = dt.Copy
        '    Else
        '        _Dt.Merge(dt.Copy)
        '    End If

        'Next


        'If FNHSysCmpId.Text <> "" Then
        '    _Qry &= vbCrLf & "  AND FNHSysCmpId =" & Val("" & Me.FNHSysCmpId.Properties.Tag.ToString)
        'End If


        _Spls.UpdateInformation("Loading Data of Line " & FNHSysUnitSectId.Properties.Tag.ToString() & "  ( " & _PLine.ToString & " of  " & _TotalLine.ToString & " )")

        _Qry = "Exec [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.SP_CALCULATE_STATIC_INCENTIVE_BUDGET_CPCUT '" & Val(FNHSysUnitSectId.Properties.Tag.ToString()) & "','" & Val(FNHSysUnitSectIdTo.Properties.Tag.ToString()) & "','" & HI.UL.ULDate.ConvertEnDB(FTStartDate.Text) & "','" & HI.UL.ULDate.ConvertEnDB(FTEndDate.Text) & "', '', " & Val("" & Me.FNHSysCmpId.Properties.Tag.ToString)
        dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

        If _Dt Is Nothing Then
            _Dt = dt.Copy
        Else
            _Dt.Merge(dt.Copy)
        End If



        _Spls.Close()

        dtline.Dispose()
        dt.Dispose()

        'Try
        '    _DtShow = _Dt.Select("FTUnitSectCode<>''", "FTUnitSectCode,FTUnitSectCode").CopyToDataTable
        'Catch ex As Exception
        '    _DtShow = Nothing
        'End Try
        'Me.ogc8.DataSource = _DtShow.Copy
        Me.ogc8.DataSource = dt
        Me.ogv8.ExpandAllGroups()
        '' _DtShow.Dispose()
        _Dt.Dispose()

    End Sub

#End Region
#Region "General"

    Private Sub ocmload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmload.Click
        If HI.UL.ULDate.CheckDate(FTStartDate.Text) <> "" Then

            Select Case Me.FNCalculateIncentiveType.SelectedIndex
                Case 0

                    Call LoadDataInfo()

                Case 1
                    Call LoadDataBUCutInfo()
                Case 2
                    Call LoadDataStockFabricInfo()
                Case 3
                    Call LoadDataCutAuto()
                Case 4
                    Call LoadDataHeat()
                Case 5
                    Call LoadDataEmbPrint()
                Case 6
                    Call LoadDataLaser()
                Case 7
                    Call LoadDataPadPrint()
                Case 8
                    Call LoadDataCPCutInfo()
                Case Else
                    Call LoadDataInfo()
            End Select

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

            Select Case Me.FNCalculateIncentiveType.SelectedIndex
                Case 0

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
                Case 1

                    With ogc1
                        If Not (.DataSource Is Nothing) And ogv1.RowCount > 0 Then

                            With ogv1
                                For I As Integer = 0 To .RowCount - 1
                                    .SetRowCellValue(I, .Columns.ColumnByFieldName("FTSelect"), _State)
                                Next
                            End With

                            CType(.DataSource, DataTable).AcceptChanges()
                        End If

                    End With
                Case 2

                    With ogc2
                        If Not (.DataSource Is Nothing) And ogv2.RowCount > 0 Then

                            With ogv2
                                For I As Integer = 0 To .RowCount - 1
                                    .SetRowCellValue(I, .Columns.ColumnByFieldName("FTSelect"), _State)
                                Next
                            End With

                            CType(.DataSource, DataTable).AcceptChanges()
                        End If

                    End With
                Case 3

                    With ogc3
                        If Not (.DataSource Is Nothing) And ogv3.RowCount > 0 Then

                            With ogv
                                For I As Integer = 0 To .RowCount - 1
                                    .SetRowCellValue(I, .Columns.ColumnByFieldName("FTSelect"), _State)
                                Next
                            End With

                            CType(.DataSource, DataTable).AcceptChanges()
                        End If

                    End With
                Case 4

                    With ogc4
                        If Not (.DataSource Is Nothing) And ogv4.RowCount > 0 Then

                            With ogv4
                                For I As Integer = 0 To .RowCount - 1
                                    .SetRowCellValue(I, .Columns.ColumnByFieldName("FTSelect"), _State)
                                Next
                            End With

                            CType(.DataSource, DataTable).AcceptChanges()
                        End If

                    End With
                Case 5

                    With ogc5
                        If Not (.DataSource Is Nothing) And ogv5.RowCount > 0 Then

                            With ogv5
                                For I As Integer = 0 To .RowCount - 1
                                    .SetRowCellValue(I, .Columns.ColumnByFieldName("FTSelect"), _State)
                                Next
                            End With

                            CType(.DataSource, DataTable).AcceptChanges()
                        End If

                    End With
                Case 6

                    With ogc6
                        If Not (.DataSource Is Nothing) And ogv6.RowCount > 0 Then

                            With ogv6
                                For I As Integer = 0 To .RowCount - 1
                                    .SetRowCellValue(I, .Columns.ColumnByFieldName("FTSelect"), _State)
                                Next
                            End With

                            CType(.DataSource, DataTable).AcceptChanges()
                        End If

                    End With
                Case 7

                    With ogc7
                        If Not (.DataSource Is Nothing) And ogv7.RowCount > 0 Then

                            With ogv7
                                For I As Integer = 0 To .RowCount - 1
                                    .SetRowCellValue(I, .Columns.ColumnByFieldName("FTSelect"), _State)
                                Next
                            End With

                            CType(.DataSource, DataTable).AcceptChanges()
                        End If

                    End With
                Case 8

                    With ogc8
                        If Not (.DataSource Is Nothing) And ogv8.RowCount > 0 Then

                            With ogv8
                                For I As Integer = 0 To .RowCount - 1
                                    .SetRowCellValue(I, .Columns.ColumnByFieldName("FTSelect"), _State)
                                Next
                            End With

                            CType(.DataSource, DataTable).AcceptChanges()
                        End If

                    End With
                Case Else

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
            End Select

        Catch ex As Exception

        End Try
    End Sub

    Private Sub wOTRequest_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Me.FNHSysCmpId.Text = HI.ST.SysInfo.CmpCode
        HI.UL.AppRegistry.LoadLayoutGridFromRegistry(Me, Me.ogv)
    End Sub

#End Region

    Private Sub ogv_RowCellStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles ogv.RowCellStyle
        Try
            With Me.ogv
                Select Case e.Column.FieldName
                    Case "H01", "H02", "H03", "H04", "H05", "H06", "H07", "H08", "H09", "H10", "H11", "H12", "H13", "Total", "TotalPack"
                        If Val("" & .GetRowCellValue(e.RowHandle, e.Column.FieldName).ToString) > 0 Then
                            e.Appearance.BackColor = System.Drawing.Color.GreenYellow
                            e.Appearance.ForeColor = System.Drawing.Color.Blue
                            e.Appearance.Font = New System.Drawing.Font("tahoma", 8, System.Drawing.FontStyle.Bold)
                        Else

                        End If

                    Case Else

                End Select
            End With
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ogv_RowStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles ogv.RowStyle, ogv1.RowStyle, ogv2.RowStyle _
                        , ogv3.RowStyle, ogv4.RowStyle, ogv5.RowStyle, ogv6.RowStyle, ogv7.RowStyle, ogv8.RowStyle
        Try
            With CType(sender, DevExpress.XtraGrid.Views.Grid.GridView)

                If "" & .GetRowCellValue(e.RowHandle, "FTCalUser").ToString <> "" Then
                    e.Appearance.BackColor = System.Drawing.Color.LightYellow
                    e.Appearance.BackColor2 = System.Drawing.Color.Orange
                End If

            End With
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ogv1_RowCellStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles ogv1.RowCellStyle
        Try
            With Me.ogv
                Select Case e.Column.FieldName
                    Case "FNIncentiveAmt"
                        If Val("" & .GetRowCellValue(e.RowHandle, e.Column.FieldName).ToString) > 0 Then
                            e.Appearance.BackColor = System.Drawing.Color.GreenYellow
                            e.Appearance.ForeColor = System.Drawing.Color.Blue
                            e.Appearance.Font = New System.Drawing.Font("tahoma", 8, System.Drawing.FontStyle.Bold)
                        Else

                        End If

                    Case Else

                End Select
            End With
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ocmpreview_Click(sender As Object, e As EventArgs) Handles ocmpreview.Click
        Select Case True
            Case FNCalculateIncentiveType.SelectedIndex = 0
                'เย็บ
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
                                        Case 4
                                            .ReportName = "HRIncentiveV4.rpt"
                                        Case Else
                                            .ReportName = "HRIncentiveV0.rpt"
                                    End Select
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
            Case FNCalculateIncentiveType.SelectedIndex = 1 Or FNCalculateIncentiveType.SelectedIndex = 3
                'ตัด 
                If Not (Me.ogc1.DataSource Is Nothing) Then
                    With CType(Me.ogc1.DataSource, DataTable)
                        .AcceptChanges()
                        If .Select("FTSelect='1'").Length > 0 Then
                            For Each R As DataRow In .Select("FTSelect='1'")

                                Dim _FM As String = ""
                                _FM = "{THRTIncentive.FNHSysUnitSectId}=" & Val(R!FNHSysUnitSectId.ToString) & " AND {THRTIncentive.FTCalDate}='" & HI.UL.ULDate.ConvertEnDB(R!FDScanDateOrg.ToString) & "'"

                                With New HI.RP.Report

                                    .FormTitle = Me.Text
                                    .ReportFolderName = "Human Report\"

                                    .ReportName = "HRIncentiveCUT.rpt"

                                    .Formular = _FM
                                    .Preview()
                                End With
                            Next

                        End If

                    End With
                End If

            Case FNCalculateIncentiveType.SelectedIndex = 8
                Dim _dt As DataTable
                If Not (Me.ogc8.DataSource Is Nothing) Then
                    With CType(Me.ogc8.DataSource, DataTable)
                        .AcceptChanges()
                        _dt = .Copy

                        .AcceptChanges()

                        Dim grpfodata As List(Of Integer) = (_dt.Select("FTSelect='1'", "FNHSysUnitSectId").CopyToDataTable).AsEnumerable() _
                             .Select(Function(r) r.Field(Of Integer)("FNHSysUnitSectId")) _
                             .Distinct() _
                             .ToList()

                        For Each _UnitSect As Integer In grpfodata


                            Dim grpfodataDate As List(Of String) = (_dt.Select("FTSelect='1' AND FNHSysUnitSectId =" & Val(_UnitSect) & "", "FDScanDateOrg").CopyToDataTable).AsEnumerable() _
                             .Select(Function(r) r.Field(Of String)("FDScanDateOrg")) _
                             .Distinct() _
                             .ToList()

                            For Each _Date As String In grpfodataDate

                                'For Each Rxt As DataRow In _dt.Select("FNHSysUnitSectId=" & Val(_UnitSect) & " AND FDScanDateOrg='" & HI.UL.ULDate.ConvertEnDB(_Date) & "'")
                                'If .Select("FTSelect='1'").Length > 0 Then
                                '    For Each R As DataRow In .Select("FTSelect='1'")

                                Dim _FM As String = ""
                                _FM = "{THRTIncentive.FNHSysUnitSectId}=" & Val(_UnitSect) & " AND {THRTIncentive.FTCalDate}='" & HI.UL.ULDate.ConvertEnDB(_Date) & "'"

                                With New HI.RP.Report

                                        .FormTitle = Me.Text
                                        .ReportFolderName = "Human Report\"

                                        .ReportName = "HRIncentiveCUTCP.rpt"

                                        .Formular = _FM
                                        .Preview()
                                    End With
                                    '    Next

                                    'End If
                                    'Next
                                Next
                        Next


                    End With
                End If


        End Select

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

    Private Sub FNCalculateIncentiveType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles FNCalculateIncentiveType.SelectedIndexChanged
        Try

            Dim _BrwID As Integer = 248
            Dim _BrwIDTo As Integer = 255

            Me.ogc.DataSource = Nothing
            Me.ogc1.DataSource = Nothing
            Me.ogc2.DataSource = Nothing
            Me.ogc3.DataSource = Nothing
            Me.ogc4.DataSource = Nothing
            Me.ogc5.DataSource = Nothing
            Me.ogc6.DataSource = Nothing
            Me.ogc7.DataSource = Nothing
            Me.ogc8.DataSource = Nothing

            FNHSysUnitSectId.Text = ""
            FNHSysUnitSectIdTo.Text = ""

            Me.otpcaltype0.PageVisible = (FNCalculateIncentiveType.SelectedIndex = 0)
            Me.otpcaltype1.PageVisible = (FNCalculateIncentiveType.SelectedIndex = 1)
            Me.otpcaltype2.PageVisible = (FNCalculateIncentiveType.SelectedIndex = 2)
            Me.otpcaltype3.PageVisible = (FNCalculateIncentiveType.SelectedIndex = 3)
            Me.otpcaltype4.PageVisible = (FNCalculateIncentiveType.SelectedIndex = 4)
            Me.otpcaltype5.PageVisible = (FNCalculateIncentiveType.SelectedIndex = 5)
            Me.otpcaltype6.PageVisible = (FNCalculateIncentiveType.SelectedIndex = 6)
            Me.otpcaltype7.PageVisible = (FNCalculateIncentiveType.SelectedIndex = 7)
            Me.otpcaltype8.PageVisible = (FNCalculateIncentiveType.SelectedIndex = 8)

            Select Case FNCalculateIncentiveType.SelectedIndex
                Case 0
                    _BrwID = 248
                    _BrwIDTo = 255
                Case 1
                    _BrwID = 376
                    _BrwIDTo = 377
                Case 2
                    _BrwID = 378
                    _BrwIDTo = 379
                Case 3
                    _BrwID = 380
                    _BrwIDTo = 381
                Case 4
                    _BrwID = 382
                    _BrwIDTo = 383
                Case 5
                    _BrwID = 384
                    _BrwIDTo = 385
                Case 6
                    _BrwID = 386
                    _BrwIDTo = 387
                Case 7
                    _BrwID = 388
                    _BrwIDTo = 389
                Case 8
                    _BrwID = 376
                    _BrwIDTo = 377
                Case Else
                    _BrwID = 248
                    _BrwIDTo = 255
            End Select

            Me.FNHSysUnitSectId.Properties.Buttons(0).Tag = _BrwID.ToString
            Me.FNHSysUnitSectIdTo.Properties.Buttons(0).Tag = _BrwIDTo.ToString

        Catch ex As Exception
        End Try

    End Sub

    Private Sub RepC1FTSelect_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles RepC1FTSelect.EditValueChanging
        Try
            Dim _StateCheck As String = "0"
            If e.NewValue.ToString = "1" Then
                _StateCheck = "1"
            End If

            Dim _UnitSectId As Integer = 0
            Dim _CalDate As String = ""

            With Me.ogv1
                _CalDate = "" & .GetFocusedRowCellValue("FDScanDateGrp").ToString
                _UnitSectId = Val("" & .GetFocusedRowCellValue("FNHSysUnitSectId").ToString)

                With Me.ogc1
                    With CType(.DataSource, DataTable)
                        .AcceptChanges()

                        For Each R As DataRow In .Select("FNHSysUnitSectId=" & _UnitSectId & " AND FDScanDateGrp='" & HI.UL.ULDate.ConvertEnDB(_CalDate) & "'")
                            R!FTSelect = _StateCheck
                        Next

                        .AcceptChanges()
                    End With
                End With
            End With
        Catch ex As Exception

        End Try
    End Sub

    'Private Sub ogv8_MouseDown(sender As Object, e As MouseEventArgs) Handles ogv8.MouseDown
    '    Try
    '        Dim view As GridView
    '        Dim hInfo As GridHitInfo
    '        view = sender
    '        hInfo = view.CalcHitInfo(e.X, e.Y)
    '        Dim vInfo As GridViewInfo = view.GetViewInfo()
    '        Dim cInfo As GridCellInfo = vInfo.GetGridCellInfo(hInfo)
    '        If (hInfo.InRowCell) Then
    '            For Each cellInfo As GridCellInfo In (CType(cInfo, GridMergedCellInfo)).MergedCells
    '                With ogv
    '                    Dim newvalue As String = .GetRowCellValue(cellInfo.RowHandle, "FTSelect")
    '                    Dim value As String = IIf(newvalue = "1", "0", "1")
    '                    .SetRowCellValue(cellInfo.RowHandle, "FTSelect", value)
    '                End With
    '            Next
    '        End If

    '    Catch ex As Exception
    '    End Try
    'End Sub

    'Private Sub Ogv8_CellMerge(sender As Object, e As CellMergeEventArgs) Handles ogv8.CellMerge

    'End Sub
End Class
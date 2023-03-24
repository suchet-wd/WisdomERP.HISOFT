
Imports System.Linq

Public Class wEmployeeMoveShift_CVN
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _ActualDate = HI.Conn.SQLConn.GetField("SELECT  CONVERT(varchar(10),GETDATE(),111)", Conn.DB.DataBaseName.DB_HR, "")
        _ActualNextDate = HI.Conn.SQLConn.GetField("SELECT  CONVERT(varchar(10),DateAdd(day,1,GETDATE()),111)", Conn.DB.DataBaseName.DB_HR, "")

    End Sub

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

            If Me.FNHSysEmpTypeId.Text <> "" Then
                If HI.HRCAL.Time.CheckClosePeriod(FTDateEnd.Text, 0, Integer.Parse(Val(Me.FNHSysEmpTypeId.Properties.Tag.ToString))) = True Then
                    HI.MG.ShowMsg.mInfo("ไม่สามารถทำการแก้ไขข้อมูลหลังการปิดงวดได้ กรุณาทำการตรวจสอบ", 14040001, Me.Text)
                    Exit Sub
                End If
            End If

            Dim _Spls As New HI.TL.SplashScreen("Saving...   Please Wait   ")
            Try
                If Me.SaveData(_Spls) Then
                    _Spls.Close()
                    HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                    Me.ocmload_Click(ocmload, New System.EventArgs)
                Else
                    _Spls.Close()
                    HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                End If
            Catch ex As Exception
                _Spls.Close()
            End Try
        End If
    End Sub

    Private Sub ProcessDelete(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmdelete.Click
        If CType(ogc.DataSource, DataTable).Rows.Count > 0 Then

            CType(ogc.DataSource, DataTable).AcceptChanges()


            If Me.FNHSysEmpTypeId.Text <> "" Then
                If HI.HRCAL.Time.CheckClosePeriod(FTDateEnd.Text, 0, Integer.Parse(Val(Me.FNHSysEmpTypeId.Properties.Tag.ToString))) = True Then
                    HI.MG.ShowMsg.mInfo("ไม่สามารถทำการแก้ไขข้อมูลหลังการปิดงวดได้ กรุณาทำการตรวจสอบ", 14040001, Me.Text)
                    Exit Sub
                End If
            End If

            If CType(ogc.DataSource, DataTable).Select("FTSelect='1'").Length > 0 Then
                If HI.MG.ShowMsg.mConfirmProcess(MG.ShowMsg.ProcessType.mDelete, Me.Text) = True Then
                    Dim _Spls As New HI.TL.SplashScreen("Deleting...   Please Wait   ")
                    If Me.DeleteData(_Spls) Then
                        _Spls.Close()
                        HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
                        Me.ocmload_Click(ocmload, New System.EventArgs)

                    Else
                        _Spls.Close()
                        HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
                    End If
                End If
            Else
                HI.MG.ShowMsg.mInvalidData("", 1104030001, Me.Text)
            End If
        Else
            HI.MG.ShowMsg.mInvalidData("", 1104030001, Me.Text)
        End If
    End Sub

    Private Sub ProcessClear(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmclear.Click

        Me.ogc.DataSource = Nothing
        ochkselectall.Checked = False

        HI.TL.HandlerControl.ClearControl(Me)
        Me.FTDateStart.Focus()

    End Sub

    Private Sub ProcessPreview(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmpreview.Click
    End Sub

    Private Sub ProcessClose(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

#End Region

#Region " Procedure "

    Private Function SaveData(ByVal objspl As HI.TL.SplashScreen) As Boolean

        Dim _Qry As String
        CType(ogc.DataSource, DataTable).AcceptChanges()
        Dim _dtMain As DataTable = CType(ogc.DataSource, DataTable)
        Dim _Dt As DataTable = _dtMain.Clone

        Dim _TotalHour As Double = 0
        Dim _Rest As Double = 0
        Dim _FNTotalMonute As Double = 0
        Dim _TotalHour2 As Double = 0
        Dim _Rest2 As Double = 0
        Dim _FNTotalMonute2 As Double = 0
        Dim _TotalNetHour As Double = 0
        Dim _FNTotalNetMonute As Double = 0

        Dim _EndProcDate As String = ""
        Dim _NextProcDate As String = ""
        Dim _dtWeekend As New DataTable
        Dim _dtHoliday As New DataTable
        Dim _SkipProcess As Boolean
        Dim _WeekEnd As Integer

        For Each R As DataRow In _dtMain.Rows
            If R!FTSelect.ToString = "1" Then
                If _Dt.Select("FNHSysEmpID=" & Val(R!FNHSysEmpID.ToString) & "").Length <= 0 Then
                    _Dt.Rows.Add(R.ItemArray())
                End If
            End If
        Next

        _dtMain.Dispose()

        _Qry = "SELECt   FDHolidayDate   "
        _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMHoliday WITH(NOLOCK) "
        _dtHoliday = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MASTER)

        Try

            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_HR)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            For Each R As DataRow In _Dt.Rows

                _Qry = "   SELECT    Top 1   FTSunday,FTMonday, FTTuesday, FTWednesday, "
                _Qry &= vbCrLf & "   FTThursday, FTFriday, FTSaturday"
                _Qry &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployeeWeekly"
                _Qry &= vbCrLf & " WHERE FNHSysEmpID=" & Val(R!FNHSysEmpID) & " "
                _dtWeekend = HI.Conn.SQLConn.GetDataTableOnbeginTrans(_Qry, Conn.DB.DataBaseName.DB_HR)

                If _dtWeekend.Rows.Count <= 0 Then
                    _Qry = "   SELECT    FTSunday, FTMonday, FTTuesday, FTWednesday, FTThursday, FTFriday,"
                    _Qry &= vbCrLf & "    FTSaturday "
                    _Qry &= vbCrLf & "  FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMTimeShift "
                    _Qry &= vbCrLf & "  WHERE FNHSysShiftID =" & Val(R!FNHSysShiftIDOrg.ToString) & " "
                    _dtWeekend = HI.Conn.SQLConn.GetDataTableOnbeginTrans(_Qry, Conn.DB.DataBaseName.DB_HR)
                End If

                If R!FTSelect.ToString = "1" Then

                    _EndProcDate = HI.UL.ULDate.ConvertEnDB(Me.FTDateEnd.Text)
                    _NextProcDate = HI.UL.ULDate.ConvertEnDB(Me.FTDateStart.Text)

                    Do While _NextProcDate <= _EndProcDate
                        If HI.HRCAL.Time.CheckClosePeriod(_NextProcDate, 0, Integer.Parse(Val(R!FNHSysEmpTypeId.ToString))) = False Then
                            objspl.UpdateInformation("Saving Employee " & R!FTEmpCode.ToString & "   Date " & HI.UL.ULDate.ConvertEN(_NextProcDate))

                            _WeekEnd = Weekday(CDate(_NextProcDate), Microsoft.VisualBasic.FirstDayOfWeek.Sunday)
                            _SkipProcess = False

                            'For Each Rday As DataRow In _dtWeekend.Rows

                            '    If Rday.Item(_WeekEnd - 1).ToString = "1" Then
                            '        _SkipProcess = True
                            '    End If

                            '    Exit For
                            'Next

                            'If _SkipProcess = False Then
                            '    For Each Dr As DataRow In _dtHoliday.Select("  FDHolidayDate = '" & HI.UL.ULDate.ConvertEnDB(_NextProcDate) & "'   ")
                            '        _SkipProcess = True
                            '    Next
                            'End If

                            If Not (_SkipProcess) Then
                                _Qry = " SELECT TOP 1 FDShiftDate  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployeeMoveShift WITH (NOLOCK)"
                                _Qry &= vbCrLf & " WHERE FNHSysEmpID =" & Val(R!FNHSysEmpID) & " "
                                _Qry &= vbCrLf & " AND FDShiftDate = '" & HI.UL.ULDate.ConvertEnDB(_NextProcDate) & "'"

                                If HI.Conn.SQLConn.GetFieldOnBeginTrans(_Qry, Conn.DB.DataBaseName.DB_HR, "") <> "" Then

                                    _Qry = " Update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployeeMoveShift "
                                    _Qry &= vbCrLf & "  SET  FTUpdUser='" & HI.ST.UserInfo.UserName & "' "
                                    _Qry &= vbCrLf & " , FDUpdDate=" & HI.UL.ULDate.FormatDateDB & ", FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & "  "
                                    _Qry &= vbCrLf & " ,FNHSysShiftID=" & Val(FNHSysShiftID.Properties.Tag.ToString) & "  "
                                    _Qry &= vbCrLf & " WHERE FNHSysEmpID =" & Val(R!FNHSysEmpID) & " "
                                    _Qry &= vbCrLf & " AND FDShiftDate = '" & HI.UL.ULDate.ConvertEnDB(_NextProcDate) & "'"

                                Else

                                    _Qry = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployeeMoveShift (  FTInsUser, FDInsDate, FTInsTime "
                                    _Qry &= vbCrLf & "  , FNHSysEmpID, FNHSysShiftID,FDShiftDate) "
                                    _Qry &= vbCrLf & " SELECT '" & HI.ST.UserInfo.UserName & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & "  "
                                    _Qry &= vbCrLf & " ," & Val(R!FNHSysEmpID) & ""
                                    _Qry &= vbCrLf & " ," & Val(FNHSysShiftID.Properties.Tag.ToString) & ",'" & HI.UL.ULDate.ConvertEnDB(_NextProcDate) & "' "

                                End If

                                If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                                    HI.Conn.SQLConn.Tran.Rollback()
                                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                                    Return False
                                End If
                            End If

                        End If

                        _NextProcDate = HI.UL.ULDate.ConvertEnDB(HI.UL.ULDate.AddDay(_NextProcDate, 1))

                    Loop
                End If
            Next

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            For Each R As DataRow In _Dt.Rows
                If R!FTSelect.ToString = "1" Then

                    _EndProcDate = HI.UL.ULDate.ConvertEnDB(Me.FTDateEnd.Text)
                    _NextProcDate = HI.UL.ULDate.ConvertEnDB(Me.FTDateStart.Text)

                    Do While _NextProcDate <= _EndProcDate

                        If HI.HRCAL.Time.CheckClosePeriod(_NextProcDate, 0, Integer.Parse(Val(R!FNHSysEmpTypeId.ToString))) = False Then
                            objspl.UpdateInformation("Calculating  Work Time Employee " & R!FTEmpCode.ToString & "   Date " & HI.UL.ULDate.ConvertEN(R!FDShiftDate.ToString))

                            HI.HRCAL.Calculate.TransTimeCard(HI.ST.UserInfo.UserName, HI.UL.ULDate.ConvertEnDB(_NextProcDate), Val(R!FNHSysEmpID))
                            _NextProcDate = HI.UL.ULDate.ConvertEnDB(HI.UL.ULDate.AddDay(_NextProcDate, 1))
                        End If


                    Loop

                End If
            Next

            _dtWeekend.Dispose()
            _dtHoliday.Dispose()

            Return True

        Catch ex As Exception

            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            Return False
        End Try

    End Function

    Private Function DeleteData(ByVal objspl As HI.TL.SplashScreen) As Boolean
        Try

            CType(ogc.DataSource, DataTable).AcceptChanges()
            Dim _Dt As DataTable = CType(ogc.DataSource, DataTable)
            Dim _Qry As String = ""
            Dim _EndProcDate As String = ""
            Dim _NextProcDate As String = ""

            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_HR)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            For Each R As DataRow In _Dt.Select("FTSelect='1'")

                If R!FTSelect.ToString = "1" And R!FDShiftDate.ToString <> "" Then

                    If HI.HRCAL.Time.CheckClosePeriod(R!FDShiftDate.ToString, 0, Integer.Parse(Val(R!FNHSysEmpTypeId.ToString))) = False Then
                        objspl.UpdateInformation("Deleting Employee " & R!FTEmpCode.ToString & "   Date " & HI.UL.ULDate.ConvertEN(R!FDShiftDate.ToString))

                        _Qry = " DELETE  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployeeMoveShift "
                        _Qry &= vbCrLf & " WHERE FNHSysEmpID =" & Val(R!FNHSysEmpID) & " "
                        _Qry &= vbCrLf & " AND FDShiftDate = '" & HI.UL.ULDate.ConvertEnDB(R!FDShiftDate.ToString) & "'"
                        _Qry &= vbCrLf & " AND FNHSysShiftID = " & Val(R!FNHSysShiftID.ToString) & ""

                        HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)
                    End If


                End If
            Next

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            For Each R As DataRow In _Dt.Select("FTSelect='1'")

                If R!FTSelect.ToString = "1" And R!FDShiftDate.ToString <> "" Then
                    If HI.HRCAL.Time.CheckClosePeriod(R!FDShiftDate.ToString, 0, Integer.Parse(Val(R!FNHSysEmpTypeId.ToString))) = False Then
                        objspl.UpdateInformation("Calculating  Work Time Employee " & R!FTEmpCode.ToString & "   Date " & HI.UL.ULDate.ConvertEN(R!FDShiftDate.ToString))
                        HI.HRCAL.Calculate.TransTimeCard(HI.ST.UserInfo.UserName, HI.UL.ULDate.ConvertEnDB(R!FDShiftDate.ToString), Val(R!FNHSysEmpID))
                    End If

                End If

            Next

            Return True
        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return False
        End Try

    End Function

    Private Function VerrifyData() As Boolean
        Dim _Pass As Boolean = False
        If HI.UL.ULDate.CheckDate(FTDateStart.Text) <> "" Then
            If HI.UL.ULDate.CheckDate(FTDateEnd.Text) <> "" Then
                If Me.FNHSysShiftID.Text <> "" And Me.FNHSysShiftID.Properties.Tag.ToString <> "" Then
                    If Not (ogc.DataSource Is Nothing) Then
                        If CType(ogc.DataSource, DataTable).Rows.Count > 0 Then
                            If CType(ogc.DataSource, DataTable).Select("FTSelect='1'").Length > 0 Then
                                _Pass = True
                            Else
                                HI.MG.ShowMsg.mInvalidData("", 1104030001, Me.Text)
                                FTDateStart.Focus()
                            End If
                        Else
                            HI.MG.ShowMsg.mInvalidData("", 1104030001, Me.Text)
                            FTDateStart.Focus()
                        End If
                    Else
                        HI.MG.ShowMsg.mInvalidData("", 1104030001, Me.Text)
                        FTDateStart.Focus()
                    End If
                Else
                    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FNHSysShiftID_lbl.Text)
                    FNHSysShiftID.Focus()
                End If
            Else
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FTDateEnd_lbl.Text)
                FTDateEnd.Focus()
            End If
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FTDateStart_lbl.Text)
            FTDateStart.Focus()
        End If

        Return _Pass
    End Function

#End Region

#Region "General"

    Private Sub ocmload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmload.Click
        If HI.UL.ULDate.CheckDate(FTDateStart.Text) <> "" Then
            Me.ogc.DataSource = Nothing
            ochkselectall.Checked = False

            Dim _Dt As DataTable
            Dim _Qry As String = ""

            _Qry = " SELECT  '0' AS FTSelect,  M.FNHSysEmpID, M.FTEmpCode,M.FNHSysEmpTypeId"

            If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
                _Qry &= vbCrLf & " , P.FTPreNameNameTH + ' ' +  M.FTEmpNameTH + '  ' +  M.FTEmpSurnameTH AS FTEmpName"

            Else
                _Qry &= vbCrLf & " , P.FTPreNameNameEN + ' ' +  M.FTEmpNameEN + '  ' +  M.FTEmpSurnameEN AS FTEmpName"
            End If

            _Qry &= vbCrLf & " , ISNULL(ET.FTEmpTypeCode,'') AS FTEmpTypeCode "
            _Qry &= vbCrLf & " , ISNULL(Dept.FTDeptCode,'') AS FTDeptCode, ISNULL(DI.FTDivisonCode,'') AS FTDivisonCode"
            _Qry &= vbCrLf & " , ISNULL(ST.FTSectCode,'') AS FTSectCode,ISNULL(US.FTUnitSectCode,'') AS FTUnitSectCode"

            _Qry &= vbCrLf & " , CASE WHEN ISDATE(MS.FDShiftDate) =1 THEN  Convert(varchar(10),Convert(datetime,MS.FDShiftDate),103)   ELSE '' END  AS FDShiftDate "
            _Qry &= vbCrLf & " , SHORG.FTShiftCode AS FTShiftOrgName"
            _Qry &= vbCrLf & " , MS.FTShiftCode AS FTShiftName"
            _Qry &= vbCrLf & " ,MS.FNHSysShiftID "
            _Qry &= vbCrLf & " ,M.FNHSysShiftID AS FNHSysShiftIDOrg "
            _Qry &= vbCrLf & " FROM        [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS M WITH (NOLOCK)"
            _Qry &= vbCrLf & "    INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMPrename AS P WITH (NOLOCK) ON M.FNHSysPreNameId = P.FNHSysPreNameId INNER JOIN"
            _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS ET WITH(NOLOCK)  ON M.FNHSysEmpTypeId = ET.FNHSysEmpTypeId "
            _Qry &= vbCrLf & "   LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDepartment AS Dept WITH (Nolock) ON M.FNHSysDeptId = Dept.FNHSysDeptId "
            _Qry &= vbCrLf & "   LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDivision AS DI WITH (NOLOCK) ON M.FNHSysDivisonId = DI.FNHSysDivisonId "
            _Qry &= vbCrLf & "   LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSect AS ST WITH (NOLOCK) ON M.FNHSysSectId = ST.FNHSysSectId "
            _Qry &= vbCrLf & "   LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS US WITH (NOLOCK) ON M.FNHSysUnitSectId = US.FNHSysUnitSectId "
            _Qry &= vbCrLf & "  LEFT OUTER JOIN "
            _Qry &= vbCrLf & " ( SELECT S.* ,SH.FTShiftCode "

            If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
                _Qry &= vbCrLf & ", SH.FTShiftNameTH AS FTShiftName"
            Else
                _Qry &= vbCrLf & ", SH.FTShiftNameEN AS FTShiftName"
            End If

            _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployeeMoveShift S  WITH (NOLOCK)"
            _Qry &= vbCrLf & "  INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMTimeShift AS SH WITH (NOLOCK) ON S.FNHSysShiftID = SH.FNHSysShiftID "
            If Me.FTDateStart.Text <> "" And Me.FTDateEnd.Text <> "" Then
                _Qry &= vbCrLf & "  WHERE FDShiftDate >='" & HI.UL.ULDate.ConvertEnDB(Me.FTDateStart.Text) & "' "
                _Qry &= vbCrLf & "  AND FDShiftDate <='" & HI.UL.ULDate.ConvertEnDB(Me.FTDateEnd.Text) & "'  "
            Else
                _Qry &= vbCrLf & "  WHERE FDShiftDate ='' "
            End If

            _Qry &= vbCrLf & "  "
            _Qry &= vbCrLf & ")  AS MS  ON MS.FNHSysEmpID = M.FNHSysEmpID  "
            _Qry &= vbCrLf & "  INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMTimeShift AS SHORG WITH (NOLOCK) ON M.FNHSysShiftID = SHORG.FNHSysShiftID "

            _Qry &= vbCrLf & " WHERE  M.FTEmpCode <> ''   "
            _Qry &= vbCrLf & " AND  M.FNHSysCmpId =" & HI.ST.SysInfo.CmpID & "  "
            _Qry &= vbCrLf & " AND M.FDDateStart <='" & HI.UL.ULDate.ConvertEnDB(FTDateStart.Text) & "' "
            _Qry &= vbCrLf & " AND (M.FDDateEnd ='' OR M.FDDateEnd >'" & HI.UL.ULDate.ConvertEnDB(FTDateStart.Text) & "' )   "

            _Qry = HI.ST.Security.PermissionFilterEmployee(_Qry)

            If Me.FTDateStart.Text <> "" And Me.FTDateEnd.Text <> "" Then
            Else
                _Qry &= vbCrLf & "  AND  M.FTEmpCode <> M.FTEmpCode  "
            End If

            If Me.FNHSysEmpTypeId.Text <> "" Then
                _Qry &= vbCrLf & " AND ET.FTEmpTypeCode='" & HI.UL.ULF.rpQuoted(Me.FNHSysEmpTypeId.Text) & "' "
            End If

            '------Criteria By Employeee Code
            If Me.FNHSysEmpId.Text <> "" Then
                _Qry &= vbCrLf & " AND M.FTEmpCode >='" & HI.UL.ULF.rpQuoted(Me.FNHSysEmpId.Text) & "' "
            End If

            If Me.FNHSysEmpIdTo.Text <> "" Then
                _Qry &= vbCrLf & " AND M.FTEmpCode <='" & HI.UL.ULF.rpQuoted(Me.FNHSysEmpIdTo.Text) & "' "
            End If

            '------Criteria By Department
            If Me.FNHSysDeptId.Text <> "" Then
                _Qry &= vbCrLf & " AND  Dept.FTDeptCode>='" & HI.UL.ULF.rpQuoted(Me.FNHSysDeptId.Text) & "' "
            End If

            If Me.FNHSysDeptIdTo.Text <> "" Then
                _Qry &= vbCrLf & " AND  Dept.FTDeptCode<='" & HI.UL.ULF.rpQuoted(Me.FNHSysDeptIdTo.Text) & "' "
            End If

            '------Criteria By Division
            If Me.FNHSysDivisonId.Text <> "" Then
                _Qry &= vbCrLf & " AND  DI.FTDivisonCode>='" & HI.UL.ULF.rpQuoted(Me.FNHSysDivisonId.Text) & "' "
            End If

            If Me.FNHSysDivisonIdTo.Text <> "" Then
                _Qry &= vbCrLf & " AND  DI.FTDivisonCode<='" & HI.UL.ULF.rpQuoted(Me.FNHSysDivisonIdTo.Text) & "' "
            End If

            '------Criteria By Sect
            If Me.FNHSysSectId.Text <> "" Then
                _Qry &= vbCrLf & " AND  ST.FTSectCode>='" & HI.UL.ULF.rpQuoted(Me.FNHSysSectId.Text) & "' "
            End If

            If Me.FNHSysSectIdTo.Text <> "" Then
                _Qry &= vbCrLf & " AND  ST.FTSectCode<='" & HI.UL.ULF.rpQuoted(Me.FNHSysSectIdTo.Text) & "' "
            End If

            '------Criteria Unit Sect
            If Me.FNHSysUnitSectId.Text <> "" Then
                _Qry &= vbCrLf & " AND   US.FTUnitSectCode>='" & HI.UL.ULF.rpQuoted(Me.FNHSysUnitSectId.Text) & "' "
            End If

            If Me.FNHSysUnitSectIdTo.Text <> "" Then
                _Qry &= vbCrLf & " AND   US.FTUnitSectCode<='" & HI.UL.ULF.rpQuoted(Me.FNHSysUnitSectIdTo.Text) & "' "
            End If

            _Qry &= vbCrLf & "  ORDER BY M.FTEmpCode ASC "

            _Dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

            Me.ogc.DataSource = _Dt

        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FTDateStart_lbl.Text)
            FTDateStart.Focus()
        End If
    End Sub

    Private Sub FTDateRequest_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles FTDateStart.EditValueChanged, FTDateEnd.EditValueChanged
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
                If Not (.DataSource Is Nothing) Then

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

    Private Sub wMoveShift_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Me.FNHSysCmpId.Text = HI.ST.SysInfo.CmpCode
        HI.UL.AppRegistry.LoadLayoutGridFromRegistry(Me, Me.ogv)
    End Sub

#End Region


    Private Sub ocmsavelayout_Click(sender As Object, e As EventArgs) Handles ocmsavelayout.Click
        HI.UL.AppRegistry.SaveLayoutGridToRegistry(Me, Me.ogv)
        HI.MG.ShowMsg.mInfo("Save Layout Grid Complete...", 1404240001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Information)
    End Sub
End Class
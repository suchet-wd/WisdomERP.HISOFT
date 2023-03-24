Public Class wEmployeeOwnerOutSite
    Private _Prepare As Boolean = False
    'Private _StateEdit As Boolean = False

    Sub New()
        _Prepare = True
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _ActualDate = HI.Conn.SQLConn.GetField("SELECT  CONVERT(varchar(10),GETDATE(),111)", Conn.DB.DataBaseName.DB_HR, "")
        _ActualNaxtDate = HI.Conn.SQLConn.GetField("SELECT  CONVERT(varchar(10),DateAdd(day,1,GETDATE()),111)", Conn.DB.DataBaseName.DB_HR, "")


    End Sub

#Region "Property"
    Private _ActualDate As String = ""
    ReadOnly Property ActualDate As String
        Get
            Return _ActualDate
        End Get
    End Property

    Private _ActualNaxtDate As String = ""
    ReadOnly Property ActualNaxtDate As String
        Get
            Return _ActualNaxtDate
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
        If Not ChkActive() Then
            HI.MG.ShowMsg.mInfo("ไม่สามารถแก้ไขข้อมูลของพนักงานคนนี้ได้ กรุณาตรวจสอบ !!!", 1808141322, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
            Exit Sub
        End If
        If Me.VerrifyData Then
            Dim _Spls As New HI.TL.SplashScreen("Saving...   Please Wait   ")
            If Me.SaveData() Then
                '_StateEdit = False
                _Spls.Close()
                HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                FTStartDate_EditValueChanged(FTStartDate, New System.EventArgs)
                Call ClearScreen()
                Call LoadHistory()
            Else
                _Spls.Close()
                HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
            End If

        End If
    End Sub

    Private Sub ProcessDelete(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmdelete.Click
        If Me.VerrifyData Then
            If HI.MG.ShowMsg.mConfirmProcess(MG.ShowMsg.ProcessType.mSave, Me.Text) = True Then
                If ChkActiveDelete() Then
                    HI.MG.ShowMsg.mInfo("ไม่สามารถแก้ไขข้อมูลของพนักงานคนนี้ได้ กรุณาตรวจสอบ !!!", 1808141322, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
                    Exit Sub
                End If
                Dim _Spls As New HI.TL.SplashScreen("Deleting...   Please Wait   ")
                If Me.DeleteData() Then
                    '_StateEdit = False
                    _Spls.Close()

                    HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
                    FTStartDate_EditValueChanged(FTStartDate, New System.EventArgs)
                    Call ClearScreen()
                    Call LoadHistory()
                Else
                    _Spls.Close()
                    HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
                End If
            End If
        End If
    End Sub

    Private Sub ProcessClear(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmclear.Click
        Me.ogc.DataSource = Nothing
        '  HI.TL.HandlerControl.ClearControl(Me)
        Call ClearScreen()
        Call LoadHistory()
    End Sub

    Private Sub ProcessPreview(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmpreview.Click
    End Sub

    Private Sub ProcessClose(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

#End Region

#Region " Procedure "
    Private Function SaveData() As Boolean
        Try
            Dim _Qry As String


            _Qry = "SELECT TOP 1 FNHSysEmpID FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmpployeeWorkOutSite WITH(NOLOCK)"
            _Qry &= vbCrLf & " WHERE FNHSysEmpID = " & Val(FNHSysEmpId.Properties.Tag.ToString) & ""
            _Qry &= vbCrLf & " AND FTStartDate = '" & HI.UL.ULDate.ConvertEnDB(Me.FTStartDate.Text) & "'"
            _Qry &= vbCrLf & " AND FTEndDate = '" & HI.UL.ULDate.ConvertEnDB(Me.FTEndDate.Text) & "'"
            _Qry &= vbCrLf & " AND FTStartTime = '" & Me.FTStartTime.Text & "'"

            If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "") = "" Then

                _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmpployeeWorkOutSite ( FTInsUser, FTInsDate, FTInsTime"
                _Qry &= vbCrLf & " , FNHSysEmpID, FTStartDate, FTEndDate, FNHSysOffSiteReasonId"
                _Qry &= vbCrLf & ", FTStartTime, FTEndTime, FNTotalHour, FNTotalMin, FTNote)"
                _Qry &= vbCrLf & " VALUES ('" & HI.ST.UserInfo.UserName & "'"
                _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB
                _Qry &= vbCrLf & " ," & Val(FNHSysEmpId.Properties.Tag.ToString) & ""
                _Qry &= vbCrLf & " ,'" & HI.UL.ULDate.ConvertEnDB(Me.FTStartDate.Text) & "'"
                _Qry &= vbCrLf & " ,'" & HI.UL.ULDate.ConvertEnDB(Me.FTEndDate.Text) & "'"
                _Qry &= vbCrLf & " ," & Val(FNHSysOffSiteReasonId.Properties.Tag.ToString) & ""
                _Qry &= vbCrLf & " ,'" & Me.FTStartTime.Text & "'"
                _Qry &= vbCrLf & " ,'" & Me.FTEndTime.Text & "'"
                _Qry &= vbCrLf & " ," & FNNetTime.Value & ""
                _Qry &= vbCrLf & " ," & ocetotaltime.Value & " "
                _Qry &= vbCrLf & " ,N'" & HI.UL.ULF.rpQuoted(Me.FTRemark.Text) & "') "

            Else

                _Qry = "UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmpployeeWorkOutSite SET"
                _Qry &= vbCrLf & "  FTUpdUser = '" & HI.ST.UserInfo.UserName & "'"
                _Qry &= vbCrLf & " ,FTUpdDate = " & HI.UL.ULDate.FormatDateDB
                _Qry &= vbCrLf & " ,FTUpdTime = " & HI.UL.ULDate.FormatTimeDB
                _Qry &= vbCrLf & " ,FNHSysOffSiteReasonId = " & Val(FNHSysOffSiteReasonId.Properties.Tag.ToString) & " "
                _Qry &= vbCrLf & " ,FTNote = N'" & HI.UL.ULF.rpQuoted(Me.FTRemark.Text) & "'"
                _Qry &= vbCrLf & " ,FTStartTime= '" & FTStartTime.Text & "'"
                _Qry &= vbCrLf & ", FTEndTime= '" & FTEndTime.Text & "'"
                _Qry &= vbCrLf & ",FNTotalHour=" & FNNetTime.Value & " "
                _Qry &= vbCrLf & ", FNTotalMin=" & ocetotaltime.Value & " "
                _Qry &= vbCrLf & " WHERE FNHSysEmpID = " & Val(FNHSysEmpId.Properties.Tag.ToString) & ""
                _Qry &= vbCrLf & " AND FTStartDate = '" & HI.UL.ULDate.ConvertEnDB(Me.FTStartDate.Text) & "'"
                _Qry &= vbCrLf & " AND FTEndDate = '" & HI.UL.ULDate.ConvertEnDB(Me.FTEndDate.Text) & "'"
                _Qry &= vbCrLf & " AND FTStartTime = '" & Me.FTStartTime.Text & "'"

            End If

            HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

            _Qry = " DELETE FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTransWorkOffsite "
            _Qry &= vbCrLf & " WHERE FNHSysEmpID = " & Val(FNHSysEmpId.Properties.Tag.ToString) & " "
            _Qry &= vbCrLf & " AND FTDateTrans >= '" & HI.UL.ULDate.ConvertEnDB(Me.FTStartDate.Text) & "'"
            _Qry &= vbCrLf & " AND FTDateTrans <= '" & HI.UL.ULDate.ConvertEnDB(Me.FTEndDate.Text) & "'"
            _Qry &= vbCrLf & " AND FTStartTime = '" & Me.FTStartTime.Text & "'"
            HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

            'Call ApproveData()

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function ApproveData() As Boolean

        Try
            Dim _EndProcDate As String = HI.UL.ULDate.ConvertEnDB(Me.FTEndDate.Text)
            Dim _NextProcDate As String = ""
            Dim _NextDay As Double = 0
            _NextProcDate = HI.UL.ULDate.ConvertEnDB(Me.FTStartDate.Text)

            Dim _TotalHour As Double = 0
            Dim _FNTotalMonute As Double = 0
            Dim _FNTotalPayHour As Double = 0
            Dim _FNTotalPayMonute As Double = 0
            Dim _FNTotalNotPayHour As Double = 0
            Dim _FNTotalNotPayMonute As Double = 0
            Dim _TmpTotalHour As Double = 0
            Dim _TmpFNTotalMonute As Double = 0
            Dim _TmpFNTotalPayHour As Double = 0
            Dim _TmpFNTotalPayMonute As Double = 0
            Dim _TmpFNTotalNotPayHour As Double = 0
            Dim _TmpFNTotalNotPayMonute As Double = 0

            Dim _Qry As String

            _TmpTotalHour = CDbl(Format(Val(0), "0.00"))
            _TmpFNTotalMonute = 0


            Dim _TotalWorkHour As Double

            Do While _NextProcDate <= _EndProcDate

                _TotalWorkHour = 8

                _Qry = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTransWorkOffsite( FTInsUser, FTInsDate, FTInsTime,FNHSysEmpID, FTDateTrans, FNHSysOffSiteReasonId"
                _Qry &= vbCrLf & "  , FTStartTime, FTEndTime, FNTotalHour, FNTotalMin)"
                _Qry &= vbCrLf & "  SELECT '" & HI.ST.UserInfo.UserName & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ""
                _Qry &= vbCrLf & " ," & Val(FNHSysEmpId.Properties.Tag.ToString) & ",'" & _NextProcDate & "'," & Val(FNHSysOffSiteReasonId.Properties.Tag.ToString) & ""
                _Qry &= vbCrLf & " ,'" & Me.FTStartTime.Text & "'"
                _Qry &= vbCrLf & " ,'" & Me.FTEndTime.Text & "'"
                _Qry &= vbCrLf & " ," & FNNetTime.Value & ""
                _Qry &= vbCrLf & " ," & ocetotaltime.Value & " "

                HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

                'HI.HRCAL.Calculate.CalculateWorkTime(HI.ST.UserInfo.UserName, Me.FNHSysEmpId.Properties.Tag.ToString, _NextProcDate, _NextProcDate)
                HI.HRCAL.Calculate.TransTimeCard(HI.ST.UserInfo.UserName, _NextProcDate, Me.FNHSysEmpId.Properties.Tag.ToString)
                _NextProcDate = HI.UL.ULDate.ConvertEnDB(HI.UL.ULDate.AddDay(_NextProcDate, 1))

            Loop
            HI.HRCAL.Calculate.DisposeObject()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function DeleteData() As Boolean
        Try
            Dim _Qry As String


            _Qry = "DELETE FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmpployeeWorkOutSite "
            _Qry &= vbCrLf & " WHERE FNHSysEmpId = " & Val(FNHSysEmpId.Properties.Tag.ToString) & ""
            _Qry &= vbCrLf & " AND FTStartDate = '" & HI.UL.ULDate.ConvertEnDB(Me.FTStartDate.Text) & "'"
            _Qry &= vbCrLf & " AND FTEndDate = '" & HI.UL.ULDate.ConvertEnDB(Me.FTEndDate.Text) & "'"
            _Qry &= vbCrLf & " AND FTStartTime = '" & Me.FTStartTime.Text & "'"

            HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

            _Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTransWorkOffsite "
            _Qry &= vbCrLf & " WHERE FNHSysEmpId = " & Val(FNHSysEmpId.Properties.Tag.ToString) & ""
            _Qry &= vbCrLf & " AND FTDateTrans >= '" & HI.UL.ULDate.ConvertEnDB(Me.FTStartDate.Text) & "'"
            _Qry &= vbCrLf & " AND FTDateTrans <= '" & HI.UL.ULDate.ConvertEnDB(Me.FTEndDate.Text) & "'"
            _Qry &= vbCrLf & " AND FTStartTime = '" & Me.FTStartTime.Text & "'"

            HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

            Dim _EndProcDate As String = HI.UL.ULDate.ConvertEnDB(Me.FTEndDate.Text)
            Dim _NextProcDate As String = ""
            Dim _NextDay As Double = 0
            _NextProcDate = HI.UL.ULDate.ConvertEnDB(Me.FTStartDate.Text)
            Do While _NextProcDate <= _EndProcDate
                'HI.HRCAL.Calculate.CalculateWorkTime(HI.ST.UserInfo.UserName, Me.FNHSysEmpId.Properties.Tag.ToString, _NextProcDate, _NextProcDate)
                'Select Case Me.FNOperating.SelectedIndex
                '    Case 0
                '        _Qry = "update [" & Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRTTrans"
                '        _Qry &= vbCrLf & "set FTIn1='',FTOut1='',FTIn2='',FTOut2='',FTNote='Remove Data From Workout site'"
                '        _Qry &= vbCrLf & "WHERE FNHSysEmpID=" & Me.FNHSysEmpId.Properties.Tag & " and FTDateTrans='" & _NextProcDate & "'"
                '    Case 1
                '        _Qry = "update [" & Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRTTrans"
                '        _Qry &= vbCrLf & "set FTIn1='',FTOut1='',FTIn2='',FTNote='Remove Data From Workout site'"
                '        _Qry &= vbCrLf & "WHERE FNHSysEmpID=" & Me.FNHSysEmpId.Properties.Tag & " and FTDateTrans='" & _NextProcDate & "'"
                '    Case 2
                '        _Qry = "update [" & Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRTTrans"
                '        _Qry &= vbCrLf & "set FTIn2='',FTOut2='',FTNote='Remove Data From Workout site'"
                '        _Qry &= vbCrLf & "WHERE FNHSysEmpID=" & Me.FNHSysEmpId.Properties.Tag & " and FTDateTrans='" & _NextProcDate & "'"
                '    Case 3
                '        ' ถึงตรงนี้ที่ต้อง ทำการ Custom เวลา
                '        If (Me.FTStartTime.Text < Me.FTEndTime.Text) And (Me.FTStartTime.Text >= "08:00" And Me.FTStartTime.Text <= "11:59") And (Me.FTEndTime.Text >= "08:01" And Me.FTEndTime.Text <= "12:00") Then
                '            _Qry = "update [" & Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRTTrans"
                '            _Qry &= vbCrLf & "set FTIn1='',FTOut1='',FTIn2='',FTNote='Remove Data From Workout site'"
                '            _Qry &= vbCrLf & "WHERE FNHSysEmpID=" & Me.FNHSysEmpId.Properties.Tag & " and FTDateTrans='" & _NextProcDate & "'"
                '        ElseIf (Me.FTStartTime.Text < Me.FTEndTime.Text) And (Me.FTStartTime.Text >= "13:00" And Me.FTStartTime.Text <= "16:59") And (Me.FTEndTime.Text >= "13:01" And Me.FTEndTime.Text <= "17:00") Then
                '            _Qry = "update [" & Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRTTrans"
                '            _Qry &= vbCrLf & "set FTIn2='',FTOut2='',FTNote='Remove Data From Workout site'"
                '            _Qry &= vbCrLf & "WHERE FNHSysEmpID=" & Me.FNHSysEmpId.Properties.Tag & " and FTDateTrans='" & _NextProcDate & "'"
                '        ElseIf (Me.FTStartTime.Text < Me.FTEndTime.Text) And (Me.FTStartTime.Text >= "08:00" And Me.FTEndTime.Text <= "17:00") Then
                '            _Qry = "update [" & Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRTTrans"
                '            _Qry &= vbCrLf & "set FTIn1='',FTOut1='',FTIn2='',FTOut2='',FTNote='Remove Data From Workout site'"
                '            _Qry &= vbCrLf & "WHERE FNHSysEmpID=" & Me.FNHSysEmpId.Properties.Tag & " and FTDateTrans='" & _NextProcDate & "'"
                '        End If

                'End Select

                'Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)
                '_NextProcDate = HI.UL.ULDate.ConvertEnDB(HI.UL.ULDate.AddDay(_NextProcDate, 1))


                '' HI.HRCAL.Calculate.CalculateWorkTime(HI.ST.UserInfo.UserName, Me.FNHSysEmpId.Properties.Tag.ToString, _NextProcDate, _NextProcDate)
                Select Case Me.FNOperating.SelectedIndex
                    Case 0
                        _Qry = "update [" & Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRTTrans"
                        _Qry &= vbCrLf & "set FTIn1='',FTOut1='',FTIn2='',FTOut2='',FTNote='Remove Data From Workout site'   , FTScanMIn='',FTScanMOut=''  , FTScanAIn='',FTScanAOut=''  "
                        _Qry &= vbCrLf & "WHERE FNHSysEmpID=" & Me.FNHSysEmpId.Properties.Tag & " and FTDateTrans='" & _NextProcDate & "'"
                    Case 1
                        _Qry = "update [" & Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRTTrans"
                        _Qry &= vbCrLf & "set FTIn1='',FTOut1='',FTIn2='',FTNote='Remove Data From Workout site' , FTScanMIn='',FTScanMOut=''  "
                        _Qry &= vbCrLf & "WHERE FNHSysEmpID=" & Me.FNHSysEmpId.Properties.Tag & " and FTDateTrans='" & _NextProcDate & "'"
                    Case 2
                        _Qry = "update [" & Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRTTrans"
                        _Qry &= vbCrLf & "set FTIn2='',FTOut2='',FTNote='Remove Data From Workout site'   , FTScanAIn='',FTScanAOut='' "
                        _Qry &= vbCrLf & "WHERE FNHSysEmpID=" & Me.FNHSysEmpId.Properties.Tag & " and FTDateTrans='" & _NextProcDate & "'"
                    Case 3
                        ' ถึงตรงนี้ที่ต้อง ทำการ Custom เวลา
                        If (Me.FTStartTime.Text < Me.FTEndTime.Text) And (Me.FTStartTime.Text >= "08:00" And Me.FTStartTime.Text <= "11:59") And (Me.FTEndTime.Text >= "08:01" And Me.FTEndTime.Text <= "12:00") Then
                            _Qry = "update [" & Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRTTrans"
                            _Qry &= vbCrLf & "set FTIn1='',FTOut1='',FTIn2='',FTNote='Remove Data From Workout site'  , FTScanMIn='',FTScanMOut=''  "
                            _Qry &= vbCrLf & "WHERE FNHSysEmpID=" & Me.FNHSysEmpId.Properties.Tag & " and FTDateTrans='" & _NextProcDate & "'"
                        ElseIf (Me.FTStartTime.Text < Me.FTEndTime.Text) And (Me.FTStartTime.Text >= "13:00" And Me.FTStartTime.Text <= "16:59") And (Me.FTEndTime.Text >= "13:01" And Me.FTEndTime.Text <= "17:00") Then
                            _Qry = "update [" & Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRTTrans"
                            _Qry &= vbCrLf & "set FTIn2='',FTOut2='',FTNote='Remove Data From Workout site' , FTScanMIn='',FTScanMOut='' "
                            _Qry &= vbCrLf & "WHERE FNHSysEmpID=" & Me.FNHSysEmpId.Properties.Tag & " and FTDateTrans='" & _NextProcDate & "'"
                        ElseIf (Me.FTStartTime.Text < Me.FTEndTime.Text) And (Me.FTStartTime.Text >= "08:00" And Me.FTEndTime.Text <= "17:00") Then
                            _Qry = "update [" & Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRTTrans"
                            _Qry &= vbCrLf & "set FTIn1='',FTOut1='',FTIn2='',FTOut2='',FTNote='Remove Data From Workout site' , FTScanMIn='',FTScanMOut=''  , FTScanAIn='',FTScanAOut='' "
                            _Qry &= vbCrLf & "WHERE FNHSysEmpID=" & Me.FNHSysEmpId.Properties.Tag & " and FTDateTrans='" & _NextProcDate & "'"
                        End If

                End Select


                Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)


                '' HI.HRCAL.Calculate.CalculateWorkTime(HI.ST.UserInfo.UserName, Me.FNHSysEmpId.Properties.Tag.ToString, _NextProcDate, _NextProcDate)
                HI.HRCAL.Calculate.TransTimeCard(HI.ST.UserInfo.UserName, _NextProcDate, Me.FNHSysEmpId.Properties.Tag.ToString)
                _NextProcDate = HI.UL.ULDate.ConvertEnDB(HI.UL.ULDate.AddDay(_NextProcDate, 1))

            Loop

            HI.HRCAL.Calculate.DisposeObject()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Function ChkActiveDelete() As Boolean
        Try
            If HI.ST.SysInfo.Admin Then
                Return True
            End If
            Dim _Qry As String
            _Qry = "Select Top 1  FTInsUser  FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmpployeeWorkOutSite"
            _Qry &= vbCrLf & " WHERE FNHSysEmpId = " & Val(FNHSysEmpId.Properties.Tag.ToString) & ""
            _Qry &= vbCrLf & " AND FTStartDate = '" & HI.UL.ULDate.ConvertEnDB(Me.FTStartDate.Text) & "'"
            _Qry &= vbCrLf & " AND FTEndDate = '" & HI.UL.ULDate.ConvertEnDB(Me.FTEndDate.Text) & "'"
            If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "0") = HI.ST.UserInfo.UserName.ToString Then
                _Qry = "Select Top 1  FTApproveState  FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmpployeeWorkOutSite"
                _Qry &= vbCrLf & " WHERE FNHSysEmpId = " & Val(FNHSysEmpId.Properties.Tag.ToString) & ""
                _Qry &= vbCrLf & " AND FTStartDate = '" & HI.UL.ULDate.ConvertEnDB(Me.FTStartDate.Text) & "'"
                _Qry &= vbCrLf & " AND FTEndDate = '" & HI.UL.ULDate.ConvertEnDB(Me.FTEndDate.Text) & "'"
                _Qry &= vbCrLf & " And isnull( FTApproveState , '0') = '1'"
                Return HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "0") = "1"
            Else
                Return True
            End If
        Catch ex As Exception
            Return False
        End Try

    End Function
    Private Function ChkActive() As Boolean
        Try
            If HI.ST.SysInfo.Admin Then
                Return True
            End If
            Dim _Qry As String
            _Qry = "Select Top 1  FTInsUser  FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmpployeeWorkOutSite"
            _Qry &= vbCrLf & " WHERE FNHSysEmpId = " & Val(FNHSysEmpId.Properties.Tag.ToString) & ""
            _Qry &= vbCrLf & " AND FTStartDate = '" & HI.UL.ULDate.ConvertEnDB(Me.FTStartDate.Text) & "'"
            _Qry &= vbCrLf & " AND FTEndDate = '" & HI.UL.ULDate.ConvertEnDB(Me.FTEndDate.Text) & "'"
            If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "") = "" Then
                Return True
            Else
                If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "0") = HI.ST.UserInfo.UserName.ToString Then
                    _Qry = "Select Top 1  FTApproveState  FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmpployeeWorkOutSite"
                    _Qry &= vbCrLf & " WHERE FNHSysEmpId = " & Val(FNHSysEmpId.Properties.Tag.ToString) & ""
                    _Qry &= vbCrLf & " AND FTStartDate = '" & HI.UL.ULDate.ConvertEnDB(Me.FTStartDate.Text) & "'"
                    _Qry &= vbCrLf & " AND FTEndDate = '" & HI.UL.ULDate.ConvertEnDB(Me.FTEndDate.Text) & "'"
                    _Qry &= vbCrLf & " And isnull( FTApproveState , '0') = '1'"
                    If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "0") <> "1" Then
                        Return True
                    End If
                Else
                    Return False
                End If
            End If


        Catch ex As Exception
            Return True
        End Try

    End Function
    Private Function VerrifyData() As Boolean
        Dim _Pass As Boolean = False

        If Me.FNHSysEmpId.Text <> "" And FNHSysEmpId.Properties.Tag.ToString <> "" Then
            If HI.UL.ULDate.CheckDate(FTStartDate.Text) <> "" And HI.UL.ULDate.CheckDate(FTEndDate.Text) <> "" Then
                If Me.FTStartTime.Text <> "" And Me.FTEndTime.Text <> "" Then
                    If Me.FTStartTime.Text <> Me.FTEndTime.Text Then
                        If Me.FNHSysOffSiteReasonId.Text <> "" And Me.FNHSysOffSiteReasonId.Properties.Tag.ToString <> "" Then
                            If Me.FTNetDay.Value > 0 Then
                                _Pass = True
                            Else
                                HI.MG.ShowMsg.mInvalidData("กรุณาทำการระบุข้อมูลเวลา !!!", 1808130001, Me.Text)
                                FTEndDate.Focus()
                            End If
                        Else
                            HI.MG.ShowMsg.mInvalidData("", 1104050002, Me.Text)
                            Me.FTEndTime.Focus()
                        End If
                    Else
                        HI.MG.ShowMsg.mInvalidData("", 1104050002, Me.Text)

                        If Me.FTStartTime.Text = "" Then
                            Me.FTStartTime.Focus()
                        ElseIf Me.FTEndTime.Text = "" Then
                            Me.FTEndTime.Focus()
                        End If

                    End If
                Else
                    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, Me.FNHSysOffSiteReasonId_lbl.Text)
                    FNHSysOffSiteReasonId.Focus()
                End If

            Else
                HI.MG.ShowMsg.mInvalidData("กรุณาทำการระบุข้อมูลเวลา !!!", 1808130001, Me.Text)
                If HI.UL.ULDate.CheckDate(FTStartDate.Text) = "" Then
                    FTStartDate.Focus()
                ElseIf HI.UL.ULDate.CheckDate(FTEndDate.Text) = "" Then
                    FTEndDate.Focus()
                End If
            End If
        Else

            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, Me.FNHSysEmpId_lbl.Text)
            FNHSysEmpId.Focus()

        End If


        Return _Pass
    End Function

    Private Sub ClearScreen()
        'modify by JOKER 2017/04/05 16.36
        FTStartDate.Text = ""
        FTEndDate.Text = ""
        FNOperating.SelectedIndex = 0
        FTNetDay.Text = ""
        'FTStartTime.Text = ""
        'FTEndTime.Text = ""
        FNHSysOffSiteReasonId.Text = ""
        FTRemark.Text = ""

    End Sub

#End Region

#Region "General"
    Private Sub wLeave_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.FNHSysCmpId.Text = HI.ST.SysInfo.CmpCode
        _Prepare = False
        Dim _Qry As String = ""
        _Qry = "   SELECT TOP 1 E.FTEmpCode "
        _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEUserLogin AS A WITH(NOLOCK) INNER JOIN"
        _Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS E WITH(NOLOCK) ON A.FNHSysEmpID = E.FNHSysEmpID"
        _Qry &= vbCrLf & "  WHERE A.FTUserName='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
        Me.FNHSysEmpId.Text = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_SECURITY, "")


        Me.FTStartTime.ReadOnly = True : Me.FTEndTime.ReadOnly = True
        Me.FTStartTime.Text = "08:00" : Me.FTEndTime.Text = "17:00"
        With ogv
            .Columns.ColumnByFieldName("FTStartDate").OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
            .Columns.ColumnByFieldName("FTEndDate").OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
        End With
    End Sub
    Public Sub LoadEmpCodeByEmpIDInfo(ByVal Key As Object)
        Dim _Qry As String = "SELECT TOP 1 FTEmpCode   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee WITH(NOLOCK) WHERE FNHSysEmpID =" & Val(Key) & " "
        FNHSysEmpId.Text = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "")
    End Sub


    Private Sub LoadEmpInfo(ByVal FNHSysEmpID As String)
        Dim _dt As DataTable
        Dim _Qry As String = ""
        _Qry = " SELECT    TOP 1     M.FTEmpCode, M.FTEmpCodeRefer, M.FTEmpNameTH, M.FTEmpSurnameTH, M.FTEmpNicknameTH, M.FTEmpNameEN, M.FNHSysEmpTypeId, M.FNHSysDeptId, "
        _Qry &= vbCrLf & "   D.FTDeptCode, Di.FTDivisonCode, M.FNHSysDivisonId, M.FNHSysSectId, S.FTSectCode, ET.FTEmpTypeCode, M.FNHSysUnitSectId, US.FTUnitSectCode,"
        _Qry &= vbCrLf & "  M.FNHSysEmpID, M.FTEmpPicName, M.FNHSysPositId, P.FTPositCode"
        _Qry &= vbCrLf & "  FROM             [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS M WITH (NOLOCK) LEFT OUTER JOIN"
        _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMPosition AS P WITH (NOLOCK) ON M.FNHSysPositId = P.FNHSysPositId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS US WITH (NOLOCK) ON M.FNHSysUnitSectId = US.FNHSysUnitSectId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS ET WITH (NOLOCK) ON M.FNHSysEmpTypeId = ET.FNHSysEmpTypeId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSect AS S WITH (NOLOCK) ON M.FNHSysSectId = S.FNHSysSectId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDivision AS Di WITH (NOLOCK) ON M.FNHSysDivisonId = Di.FNHSysDivisonId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDepartment AS D WITH (NOLOCK) ON M.FNHSysDeptId = D.FNHSysDeptId"
        _Qry &= vbCrLf & "  WHERE  M.FNHSysEmpID  =" & Val(FNHSysEmpID) & ""
        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

        FTEmpPicName.Image = Nothing
        If _dt.Rows.Count > 0 Then
            For Each R As DataRow In _dt.Rows

                FTEmpPicName.Image = HI.UL.ULImage.LoadImage(HI.ST.SysInfo.SysPath & "EmpPicture\" & R!FTEmpPicName.ToString)
                'FNHSysEmpTypeId.Text = R!FTEmpTypeCode.ToString
                'FNHSysDeptId.Text = R!FTDeptCode.ToString
                'FNHSysDivisonId.Text = R!FTDivisonCode.ToString
                'FNHSysSectId.Text = R!FTSectCode.ToString
                'FNHSysUnitSectId.Text = R!FTUnitSectCode.ToString
                'FNHSysPositId.Text = R!FTPositCode.ToString
                'FNHSysEmpTypeId.Properties.Tag = R!FNHSysEmpTypeId.ToString
            Next
        Else
            'FNHSysEmpTypeId.Text = ""
            'FNHSysDeptId.Text = ""
            'FNHSysDivisonId.Text = ""
            'FNHSysSectId.Text = ""
            'FNHSysUnitSectId.Text = ""
            'FNHSysPositId.Text = ""
            'FNHSysEmpTypeId.Properties.Tag = "0"
        End If


    End Sub
    Private Sub LoadHistory()
        Dim _Qry As String = ""
        Dim _dt As DataTable

        _Qry = "    SELECT        E.FNHSysEmpID "
        _Qry &= vbCrLf & " , Convert(varchar(10),Convert(datetime,E.FTStartDate),103) As  FTStartDate "
        _Qry &= vbCrLf & " ,  Convert(varchar(10),Convert(datetime,E.FTEndDate),103) As   FTEndDate "
        _Qry &= vbCrLf & ",case when isnull(A.FTIn1,'')<>'' then A.FTIn1 else TS.FTIn1 end AS FTIn1"
        _Qry &= vbCrLf & " ,case when isnull(A.FTIn1Start,'')<>'' then A.FTIn1Start else TS.FTIn1Start end AS FTIn1Start"
        _Qry &= vbCrLf & " ,case when isnull(A.FTIn1End,'')<>'' then A.FTIn1End else TS.FTIn1End end AS FTIn1End"
        _Qry &= vbCrLf & " ,case when isnull(A.FTOut1,'')<>'' then A.FTOut1 else TS.FTOut1 end AS FTOut1"
        _Qry &= vbCrLf & " ,case when isnull(A.FTOut1Start,'')<>'' then A.FTOut1Start else TS.FTOut1Start end AS FTOut1Start"
        _Qry &= vbCrLf & " ,case when isnull(A.FTOut1End,'')<>'' then A.FTOut1End else TS.FTOut1End end AS FTOut1End"
        _Qry &= vbCrLf & " ,case when isnull(A.FTIn2,'')<>'' then A.FTIn2 else TS.FTIn2 end AS FTIn2"
        _Qry &= vbCrLf & " ,case when isnull(A.FTIn2Start,'')<>'' then A.FTIn2Start else TS.FTIn2Start end AS FTIn2Start"
        _Qry &= vbCrLf & " ,case when isnull(A.FTIn2End,'')<>'' then A.FTIn2End else TS.FTIn2End end AS FTIn2End"
        _Qry &= vbCrLf & " ,case when isnull(A.FTOut2,'')<>'' then A.FTOut2 else TS.FTOut2 end AS FTOut2"
        _Qry &= vbCrLf & " ,case when isnull(A.FTOut2Start,'')<>'' then A.FTOut2Start else TS.FTOut2Start end AS FTOut2Start"
        _Qry &= vbCrLf & " ,case when isnull(A.FTOut2End,'')<>'' then A.FTOut2End else TS.FTOut2End end AS FTOut2End"
        _Qry &= vbCrLf & " , E.FNHSysOffSiteReasonId "
        _Qry &= vbCrLf & " , R.FTOffSiteReasonCode "
        _Qry &= vbCrLf & " , FTStartTime, FTEndTime "

        If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
            _Qry &= vbCrLf & " , R.FTOffSiteReasonDescTH  As FTReason"
        Else
            _Qry &= vbCrLf & " , R.FTOffSiteReasonDescEN As FTReason "
        End If
        _Qry &= vbCrLf & " ,E.FTNote"
        _Qry &= vbCrLf & " ,E.FTInsUser, Convert(varchar(10),Convert(datetime,FTInsDate),103) As  FTInsDate,E.FTInsTime  "
        _Qry &= vbCrLf & ",isnull(FTSendApproveState,0) AS FTSendApproveState"
        _Qry &= vbCrLf & " , Convert(nvarchar(10) ,convert(datetime,FDSendApproveDate) , 103) AS FDSendApproveDate "
        _Qry &= vbCrLf & ",FTSendApproveTime"
        _Qry &= vbCrLf & ",FTSendApproveBy"
        _Qry &= vbCrLf & ",isnull(FTApproveState,0) AS FTApproveState"
        _Qry &= vbCrLf & " , Convert(nvarchar(10) ,convert(datetime,FDApproveDate) , 103) AS FDApproveDate "
        _Qry &= vbCrLf & ",FTApproveTime"
        _Qry &= vbCrLf & ",FTApproveBy"
        _Qry &= vbCrLf & " FROM            [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmpployeeWorkOutSite AS E WITH (NOLOCK) INNER JOIN"
        _Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMWorkOffsiteReason AS R WITH (NOLOCK) ON E.FNHSysOffSiteReasonId = R.FNHSysOffSiteReasonId"

        _Qry &= vbCrLf & "Left OUtER jOIN"
        _Qry &= vbCrLf & "(select MS.FNHSysEmpID,MS.FDShiftDate,TS.FTIn1,TS.FTIn1Start,TS.FTIn1End,TS.FTOut1,TS.FTOut1Start,TS.FTOut1End,TS.FTIn2,TS.FTIn2Start,TS.FTIn2End,TS.FTOut2 ,TS.FTOut2Start,TS.FTOut2End"
        _Qry &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "]..THRMEmployeeMoveShift As MS WITH(NOLOCK) inner Join"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "]..THRMTimeShift AS TS WITH(NOLOCK) ON ms.FNHSysShiftID=TS.FNHSysShiftID"
        _Qry &= vbCrLf & "Where MS.FDShiftDate ='" & Me.FTStartDate.Text & "'  ) AS A ON E.FNHSysEmpID=A.FNHSysEmpID"
        _Qry &= vbCrLf & "Left OUtER jOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "]..THRMEmployee As Emp With(NOLOCK) On E.FNHSysEmpID= Emp.FNHSysEmpID"
        _Qry &= vbCrLf & "Left OUtER jOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "]..THRMTimeShift As TS On Emp.FNHSysShiftID= TS.FNHSysShiftID"

        _Qry &= vbCrLf & "  WHERE E.FNHSysEmpID = " & Val(FNHSysEmpId.Properties.Tag.ToString) & ""
        _Qry &= vbCrLf & " And ISdate(FTStartDate) =1 And ISdate(FTEndDate) =1 "
        _Qry &= vbCrLf & " ORDER BY E.FTStartDate desc"

        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)
        'If _dt.Rows.Count > 0 Then
        '    Me.FTStartTime.Text = "" : Me.FTEndTime.Text = ""
        'Else
        '    Me.FTStartTime.Text = "08:00" : Me.FTEndTime.Text = "17:00"
        'End If
        Me.ogc.DataSource = _dt

    End Sub

    Private Sub FTStartDate_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FTStartDate.EditValueChanged, FTEndDate.EditValueChanged
        If (_Prepare) Then Exit Sub
        'If Me.FTStartDate.Text <> "" And _StateEdit = False Then
        '    FTStartTime.Text = "08:00"
        '    FTEndTime.Text = "17:00"
        'End If
        Static _Proc As Boolean
        If Not (_Proc) Then
            _Proc = True
            'Call LoadHistory()

            Dim _Qry As String
            Dim _dtHoliday As DataTable
            Dim _dtWeekend As DataTable

            _Qry = "   SELECT    Top 1   FTSunday,FTMonday, FTTuesday, FTWednesday, "
            _Qry &= vbCrLf & "   FTThursday, FTFriday, FTSaturday"
            _Qry &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployeeWeekly As W WITH(NOLOCK)"
            _Qry &= vbCrLf & " WHERE FNHSysEmpID=" & Val(FNHSysEmpId.Properties.Tag.ToString) & " "
            _dtWeekend = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

            If _dtWeekend.Rows.Count <= 0 Then
                _Qry = "   SELECT    W.FTSunday, W.FTMonday, W.FTTuesday, W.FTWednesday, W.FTThursday, W.FTFriday,"
                _Qry &= vbCrLf & "    W.FTSaturday "
                _Qry &= vbCrLf & "  FROM            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMTimeShift   As W WITH(NOLOCK)  INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS  M  WITH(NOLOCK) "
                _Qry &= vbCrLf & "   ON W.FNHSysShiftID=M.FNHSysShiftID "
                _Qry &= vbCrLf & " WHERE M.FNHSysEmpID =" & Val(FNHSysEmpId.Properties.Tag.ToString) & " "
                _dtWeekend = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)
            End If

            _Qry = "SELECt   FDHolidayDate  "
            _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMHoliday WITH(NOLOCK) "
            _dtHoliday = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MASTER)

            Dim _EndProcDate As String = HI.UL.ULDate.ConvertEnDB(Me.FTEndDate.Text)
            Dim _NextProcDate As String = HI.UL.ULDate.ConvertEnDB(Me.FTStartDate.Text)
            Dim _NextDay As Double = 0
            Dim _TotalDay As Integer = 0
            Dim _SkipProcess As Boolean = False
            Dim _WeekEnd As Integer

            If _NextProcDate <> "" And _EndProcDate <> "" Then

                Do While _NextProcDate <= _EndProcDate
                    _WeekEnd = Weekday(CDate(_NextProcDate), Microsoft.VisualBasic.FirstDayOfWeek.Sunday)
                    _SkipProcess = False


                    _TotalDay = _TotalDay + 1

                    _NextProcDate = HI.UL.ULDate.ConvertEnDB(HI.UL.ULDate.AddDay(_NextProcDate, 1))

                Loop

            End If

            Me.FTNetDay.Value = _TotalDay

            _Proc = False
        End If
    End Sub

    Private Sub FNHSysEmpId_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If (_Prepare) Then Exit Sub


        Dim _Qry As String = "SELECT TOP 1 FNHSysEmpID  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee WITH(NOLOCK) WHERE FTEmpCode ='" & HI.UL.ULF.rpQuoted(FNHSysEmpId.Text) & "' "
        FNHSysEmpId.Properties.Tag = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "")


        Call LoadEmployeeShift()
        Call LoadHistory()

    End Sub

    Private Sub LoadEmployeeShift()
        Dim _Qry As String = ""
        Dim _dt As DataTable

        _Qry = "  SELECT   TOP 1     M.FNHSysEmpID, S.FTIn1, S.FTOut1, S.FTIn2, S.FTOut2, S.FCHour,M.FNHSysShiftID,M.FTEmpPicName"
        _Qry &= vbCrLf & " FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS M WITH (NOLOCK) INNER JOIN"
        _Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMTimeShift AS S WITH (NOLOCK) ON M.FNHSysShiftID = S.FNHSysShiftID"
        _Qry &= vbCrLf & "   WHERE M.FNHSysEmpID=" & Val(FNHSysEmpId.Properties.Tag.ToString) & " "

        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

        Me.FTIn1.Text = ""
        Me.FTOut1.Text = ""
        Me.FTIn2.Text = ""
        Me.FTOut2.Text = ""
        Me.FTIn1M.Text = ""
        Me.FTOut1M.Text = ""
        Me.FTIn2M.Text = ""
        Me.FTOut2M.Text = ""
        Me.FNHSysShiftID.Text = ""
        FTEmpPicName.Image = Nothing

        If _dt.Rows.Count > 0 Then

            For Each R As DataRow In _dt.Rows
                FTEmpPicName.Image = HI.UL.ULImage.LoadImage(HI.ST.SysInfo.SysPath & "EmpPicture\" & R!FTEmpPicName.ToString)
                Me.FNHSysShiftID.Text = R!FNHSysShiftID.ToString

                Me.FTIn1.Text = R!FTIn1.ToString
                Me.FTOut1.Text = R!FTOut1.ToString
                Me.FTIn2.Text = R!FTIn2.ToString
                Me.FTOut2.Text = R!FTOut2.ToString

                Me.FTIn1M.Text = R!FTIn1.ToString
                Me.FTOut1M.Text = R!FTOut1.ToString
                Me.FTIn2M.Text = R!FTIn2.ToString
                Me.FTOut2M.Text = R!FTOut2.ToString

                Exit For
            Next

        End If

        Call LoadEmployeeMoveShift()

    End Sub

    Private Sub LoadEmployeeMoveShift()
        Dim _Qry As String = ""
        Dim _dt As DataTable


        _Qry = "   SELECT        M.FNHSysEmpID, S.FTIn1, S.FTOut1, S.FTIn2, S.FTOut2, S.FCHour"
        _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployeeMoveShift AS M WITH (NOLOCK) INNER JOIN"
        _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMTimeShift AS S WITH (NOLOCK) ON M.FNHSysShiftID = S.FNHSysShiftID"
        _Qry &= vbCrLf & " WHERE  M.FNHSysEmpID =" & Val(FNHSysEmpId.Properties.Tag.ToString) & " "
        _Qry &= vbCrLf & " AND M.FDShiftDate ='" & HI.UL.ULDate.ConvertEnDB(Me.FTStartDate.Text) & "' "

        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

        Me.FTIn1M.Text = Me.FTIn1.Text
        Me.FTOut1M.Text = Me.FTOut1.Text
        Me.FTIn2M.Text = Me.FTIn2.Text
        Me.FTOut2M.Text = Me.FTOut2.Text

        For Each R As DataRow In _dt.Rows
            Me.FTIn1M.Text = R!FTIn1.ToString
            Me.FTOut1M.Text = R!FTOut1.ToString
            Me.FTIn2M.Text = R!FTIn2.ToString
            Me.FTOut2M.Text = R!FTOut2.ToString
            Exit For
        Next


    End Sub

    Private Sub FTETime_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FTEndTime.EditValueChanged
        If (_Prepare) Then Exit Sub
        If Me.FTStartDate.Text <> "" And Me.FTEndTime.Text <> "" And Me.FTStartTime.Text <> "" And Me.FTEndTime.Text <> "" Then
            Dim _T1 As Integer = 0
            Dim _T2 As Integer = 0
            Dim _Res As Integer = 0
            Dim _Total As Long

            If FTStartTime.Text <> "" And FTEndTime.Text <> "" Then

                'If FTStartTime.Text > FTEndTime.Text Then
                '    _T2 = DateDiff(DateInterval.Minute, CDate(Me.ActualDate & "  " & FTStartTime.Text), CDate(Me.ActualNaxtDate & "  " & FTEndTime.Text))
                'Else
                '    _T2 = DateDiff(DateInterval.Minute, CDate(Me.ActualDate & "  " & FTStartTime.Text), CDate(Me.ActualDate & "  " & FTEndTime.Text))
                'End If


                Dim STime As String = ""
                Dim ETime As String = ""

                If FTStartTime.Text < FTOut1M.Text Then
                    If FTStartTime.Text <= FTIn1M.Text Then
                        STime = FTIn1M.Text
                    Else
                        STime = FTStartTime.Text
                    End If
                End If

                If FTEndTime.Text < FTOut1M.Text Then
                    ETime = FTEndTime.Text
                Else
                    ETime = FTOut1M.Text
                End If

                If STime <> "" And ETime <> "" Then
                    If STime > ETime Then
                        _T1 = DateDiff(DateInterval.Minute, CDate(Me.ActualDate & "  " & STime), CDate(Me.ActualNaxtDate & "  " & ETime))
                    Else
                        _T1 = DateDiff(DateInterval.Minute, CDate(Me.ActualDate & "  " & STime), CDate(Me.ActualDate & "  " & ETime))
                    End If
                End If

                STime = ""
                ETime = ""

                If FTStartTime.Text < FTOut2M.Text And FTEndTime.Text > FTIn2M.Text Then
                    If FTStartTime.Text <= FTIn2M.Text Then
                        STime = FTIn2M.Text
                    Else
                        STime = FTStartTime.Text
                    End If
                End If

                If FTEndTime.Text > FTIn2M.Text Then
                    If FTEndTime.Text < FTOut2M.Text Then
                        ETime = FTEndTime.Text
                    Else
                        ETime = FTOut2M.Text
                    End If
                End If



                If STime <> "" And ETime <> "" Then
                    If STime > ETime Then
                        _T2 = DateDiff(DateInterval.Minute, CDate(Me.ActualDate & "  " & STime), CDate(Me.ActualNaxtDate & "  " & ETime))
                    Else
                        _T2 = DateDiff(DateInterval.Minute, CDate(Me.ActualDate & "  " & STime), CDate(Me.ActualDate & "  " & ETime))
                    End If
                End If

            Else
            End If

            _Total = _T1 + _T2

            ocetotaltime.Value = _Total
            Me.FNNetTime.Value = CDbl(Format((_Total \ 60.0) + ((_Total - ((_Total \ 60.0) * 60.0)) / 100.0), "0.00"))
        Else
            Me.ocetotaltime.Value = 0
            Me.FNNetTime.Value = 0
            'Me.FTNetDay.Value = 0
        End If
    End Sub

    Private Sub ogv_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ogv.DoubleClick

        With ogv
            If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub
            '_StateEdit = True
            Try
                Me.FTStartDate.DateTime = HI.UL.ULDate.ConvertEnDB("" & .GetRowCellValue(.FocusedRowHandle, "FTStartDate").ToString)
            Catch ex As Exception
            End Try

            Try
                Me.FTEndDate.DateTime = HI.UL.ULDate.ConvertEnDB("" & .GetRowCellValue(.FocusedRowHandle, "FTEndDate").ToString)
            Catch ex As Exception
            End Try

            Me.FTStartTime.Text = "" & .GetRowCellValue(.FocusedRowHandle, "FTStartTime").ToString
            Me.FTEndTime.Text = "" & .GetRowCellValue(.FocusedRowHandle, "FTEndTime").ToString
            Me.FNHSysOffSiteReasonId.Text = "" & .GetRowCellValue(.FocusedRowHandle, "FTOffSiteReasonCode").ToString
            Me.FTRemark.Text = "" & .GetRowCellValue(.FocusedRowHandle, "FTNote").ToString
            If Me.FTStartTime.Text = "08:00" And Me.FTEndTime.Text = "17:00" Then
                Me.FNOperating.SelectedIndex = 0
            ElseIf Me.FTStartTime.Text = "08:00" And Me.FTEndTime.Text = "12:00" Then
                Me.FNOperating.SelectedIndex = 1
            ElseIf Me.FTStartTime.Text = "13:00" And Me.FTEndTime.Text = "17:00" Then
                Me.FNOperating.SelectedIndex = 2
            ElseIf (Me.FTStartTime.Text <> "08:00" And FTStartTime.Text <> "13:00") Or (Me.FTEndTime.Text <> "12:00" And Me.FTEndTime.Text <> "17:00") Then
                Me.FNOperating.SelectedIndex = 3
            End If
            Me.FTRemark.Focus()

        End With

    End Sub

    Private Sub FTStartTime_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles FTStartTime.EditValueChanged
        Call FTETime_EditValueChanged(FTStartTime, New System.EventArgs)
    End Sub

    Private Sub FNOperating_SelectedIndexChanged(sender As Object, e As EventArgs) Handles FNOperating.SelectedIndexChanged
        If (_Prepare) Then Exit Sub
        Try
            If Me.FNHSysEmpId.Text <> "" Then
                Select Case Me.FNOperating.SelectedIndex
                    Case 0
                        Me.FTStartTime.ReadOnly = True : Me.FTEndTime.ReadOnly = True
                        Me.FTStartTime.BackColor = Drawing.Color.LightCyan : Me.FTEndTime.BackColor = Drawing.Color.LightCyan
                        Me.FTStartTime.Text = "08:00" : Me.FTEndTime.Text = "17:00"
                    Case 1
                        Me.FTStartTime.ReadOnly = True : Me.FTEndTime.ReadOnly = True
                        Me.FTStartTime.BackColor = Drawing.Color.LightCyan : Me.FTEndTime.BackColor = Drawing.Color.LightCyan
                        Me.FTStartTime.Text = "08:00" : Me.FTEndTime.Text = "12:00"
                    Case 2
                        Me.FTStartTime.ReadOnly = True : Me.FTEndTime.ReadOnly = True
                        Me.FTStartTime.BackColor = Drawing.Color.LightCyan : Me.FTEndTime.BackColor = Drawing.Color.LightCyan
                        Me.FTStartTime.Text = "13:00" : Me.FTEndTime.Text = "17:00"
                    Case 3
                        Me.FTStartTime.ReadOnly = False : Me.FTEndTime.ReadOnly = False
                        Me.FTStartTime.BackColor = Drawing.Color.White : Me.FTEndTime.BackColor = Drawing.Color.White
                End Select

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub FNHSysEmpId_EditValueChanged_1(sender As Object, e As EventArgs) Handles FNHSysEmpId.EditValueChanged
        If (_Prepare) Then Exit Sub


        Dim _Qry As String = "SELECT TOP 1 FNHSysEmpID  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee WITH(NOLOCK) WHERE FTEmpCode ='" & HI.UL.ULF.rpQuoted(FNHSysEmpId.Text) & "' "
        FNHSysEmpId.Properties.Tag = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "")


        Call LoadEmployeeShift()
        Call LoadHistory()
    End Sub

    Private Sub ocmSendApprove_Click(sender As Object, e As EventArgs) Handles ocmSendApprove.Click
        If Not ChkActive() Then
            HI.MG.ShowMsg.mInfo("ไม่สามารถแก้ไขข้อมูลของพนักงานคนนี้ได้ กรุณาตรวจสอบ !!!", 1808141322, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
            Exit Sub
        End If
        If Me.VerrifyData Then
            Dim _Qry As String = ""
            _Qry = "SELECT FNHSysEmpID "
            _Qry &= "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRTEmpployeeWorkOutSite"
            _Qry &= vbCrLf & " Where  FTStartDate='" & HI.UL.ULDate.ConvertEnDB(Me.FTStartDate.Text) & "'"
            _Qry &= vbCrLf & "and  FTEndDate='" & HI.UL.ULDate.ConvertEnDB(Me.FTEndDate.Text) & "'"
            _Qry &= vbCrLf & "and FNHSysEmpID=" & Integer.Parse("0" & Me.FNHSysEmpId.Properties.Tag)
            If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "") <> "" Then
                If SendApprove() Then
                    HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                    Call LoadEmployeeShift()
                    Call LoadHistory()
                End If
            Else
                HI.MG.ShowMsg.mInvalidData("กรุณาทำการบันทึกข้อมูล !!!", 1808140001, Me.Text)
            End If

        End If
    End Sub
    Private Function SendApprove() As Boolean
        Try
            Dim _Cmd As String = ""
            _Cmd = "Update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRTEmpployeeWorkOutSite"
            _Cmd &= vbCrLf & "Set FTSendApproveState='1'"
            _Cmd &= vbCrLf & ", FDSendApproveDate=" & HI.UL.ULDate.FormatDateDB & ""
            _Cmd &= vbCrLf & ", FTSendApproveTime=" & HI.UL.ULDate.FormatTimeDB & ""
            _Cmd &= vbCrLf & ", FTSendApproveBy='" & HI.ST.UserInfo.UserName & "'"
            _Cmd &= vbCrLf & " Where  FTStartDate='" & HI.UL.ULDate.ConvertEnDB(Me.FTStartDate.Text) & "'"
            _Cmd &= vbCrLf & "and  FTEndDate='" & HI.UL.ULDate.ConvertEnDB(Me.FTEndDate.Text) & "'"
            _Cmd &= vbCrLf & "and FNHSysEmpID=" & Integer.Parse("0" & Me.FNHSysEmpId.Properties.Tag)
            HI.Conn.SQLConn.ExecuteNonQuery(_Cmd, Conn.DB.DataBaseName.DB_HR)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
#End Region


End Class
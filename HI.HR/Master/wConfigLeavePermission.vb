Public Class wConfigLeavePermission

    Sub New()

        _ProcPrepare = True
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _ActualDate = HI.Conn.SQLConn.GetField("SELECT  CONVERT(varchar(10),GETDATE(),111)", Conn.DB.DataBaseName.DB_HR, "")
        _ActualNextDate = HI.Conn.SQLConn.GetField("SELECT  CONVERT(varchar(10),DateAdd(day,1,GETDATE()),111)", Conn.DB.DataBaseName.DB_HR, "")

        _ProcPrepare = False

    End Sub

#Region "Property"
    Private _ProcPrepare As Boolean = False

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
#End Region

#Region "MAIN PROC"

    Private Sub ProcessSave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmsave.Click

        If Me.VerrifyData Then
            Dim _Spls As New HI.TL.SplashScreen("Saving...   Please Wait   ")
            If Me.SaveData() Then
                _Spls.Close()
                HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                Me.ProcLoad(FNHSysEmpTypeId.Properties.Tag.ToString)
            Else
                _Spls.Close()
                HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
            End If
        End If
    End Sub

    Private Sub ProcessDelete(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmdelete.Click
        If Me.VerrifyData Then

            If HI.MG.ShowMsg.mConfirmProcess(MG.ShowMsg.ProcessType.mSave, Me.Text) = True Then
                Dim _Spls As New HI.TL.SplashScreen("Deleting...   Please Wait   ")
                If Me.DeleteData() Then
                    _Spls.Close()

                    HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
                    Me.ProcLoad(FNHSysEmpTypeId.Properties.Tag.ToString)
                Else
                    _Spls.Close()
                    HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
                End If
            End If
        End If
    End Sub

    Private Sub ProcessClear(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmclear.Click

        Me.ogc.DataSource = Nothing
        HI.TL.HandlerControl.ClearControl(Me)

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
            Dim oDBdt As DataTable
            With CType(ogc.DataSource, DataTable)
                .AcceptChanges()
                oDBdt = .Copy
            End With

            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_HR)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            Try

                For Each R As DataRow In oDBdt.Rows

                    _Qry = " UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMConfigLeave SET "
                    _Qry &= vbCrLf & " FTLeaveReset='" & (Microsoft.VisualBasic.Right(FTReset.Text, 2) & "/" & Microsoft.VisualBasic.Left(FTReset.Text, 2)) & "'"
                    _Qry &= vbCrLf & ",FNLeaveRight=" & CDbl(R!FNLeaveRight.ToString)
                    _Qry &= vbCrLf & ",FNLeavePay=" & CDbl(R!FNLeavePay.ToString)
                    _Qry &= vbCrLf & ",FNLeaveAge=" & CDbl(R!FNLeaveAge.ToString)
                    _Qry &= vbCrLf & ",FTStaHoliday='" & R!FTStaHoliday.ToString & "'"
                    _Qry &= vbCrLf & ",FTStateDeductVacation='" & R!FTStateDeductVacation.ToString & "'"
                    _Qry &= vbCrLf & ",FTAddAvg24Period='" & R!FTAddAvg24Period.ToString & "'"
                    _Qry &= vbCrLf & ",FTUpdUser = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Qry &= vbCrLf & ",FDUpdDate = " & HI.UL.ULDate.FormatDateDB & ""
                    _Qry &= vbCrLf & ",FTUpdTime = " & HI.UL.ULDate.FormatTimeDB & ""
                    _Qry &= vbCrLf & " WHERE  FNHSysEmpTypeId='" & Val(FNHSysEmpTypeId.Properties.Tag.ToString) & "'"
                    _Qry &= vbCrLf & "    AND FTLeaveCode='" & R!FTLeaveCode.ToString & "'"

                    If HI.Conn.SQLConn.ExecuteTran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                        _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMConfigLeave(FNHSysEmpTypeId,FTLeaveCode,FTLeaveReset,FNLeaveRight"
                        _Qry &= vbCrLf & ",FNLeavePay,FNLeaveAge,FTStaHoliday,FTStateDeductVacation , FTAddAvg24Period"
                        _Qry &= vbCrLf & ", FTInsUser, FDInsDate, FTInsTime)  "
                        _Qry &= vbCrLf & " SELECT " & Val(FNHSysEmpTypeId.Properties.Tag.ToString) & ",'" & R!FTLeaveCode.ToString & "'"
                        _Qry &= vbCrLf & ",'" & (Microsoft.VisualBasic.Right(FTReset.Text, 2) & "/" & Microsoft.VisualBasic.Left(FTReset.Text, 2)) & "'  "
                        _Qry &= vbCrLf & "," & CDbl(R!FNLeaveRight.ToString) & "," & CDbl(R!FNLeavePay.ToString) & ""
                        _Qry &= vbCrLf & "," & CDbl(R!FNLeaveAge.ToString) & ",'" & R!FTStaHoliday.ToString & "','" & R!FTStateDeductVacation.ToString & "','" & R!FTAddAvg24Period.ToString & "'"
                        _Qry &= vbCrLf & ",N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                        _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB

                        If HI.Conn.SQLConn.ExecuteTran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                            HI.Conn.SQLConn.Tran.Rollback()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                            Return False
                        End If

                    End If

                Next

                _Qry = " UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMConfigLeave SET "
                _Qry &= vbCrLf & " FTLeaveReset='" & (Microsoft.VisualBasic.Right(FTReset.Text, 2) & "/" & Microsoft.VisualBasic.Left(FTReset.Text, 2)) & "'"
                _Qry &= vbCrLf & ",FNLeaveRight=0"
                _Qry &= vbCrLf & ",FNLeavePay=0"
                _Qry &= vbCrLf & ",FNLeaveAge=0"
                _Qry &= vbCrLf & ",FTStaHoliday='0'"
                _Qry &= vbCrLf & ",FTUpdUser = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Qry &= vbCrLf & ",FDUpdDate = " & HI.UL.ULDate.FormatDateDB & ""
                _Qry &= vbCrLf & ",FTUpdTime = " & HI.UL.ULDate.FormatTimeDB & ""
                _Qry &= vbCrLf & " WHERE  FNHSysEmpTypeId='" & Val(FNHSysEmpTypeId.Properties.Tag.ToString) & "'"
                _Qry &= vbCrLf & "    AND FTLeaveCode='98'"

                If HI.Conn.SQLConn.ExecuteTran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                    _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMConfigLeave(FNHSysEmpTypeId,FTLeaveCode,FTLeaveReset,FNLeaveRight"
                    _Qry &= vbCrLf & ",FNLeavePay,FNLeaveAge,FTStaHoliday"
                    _Qry &= vbCrLf & ", FTInsUser, FDInsDate, FTInsTime)  "
                    _Qry &= vbCrLf & " SELECT " & Val(FNHSysEmpTypeId.Properties.Tag.ToString) & ",'98'"
                    _Qry &= vbCrLf & ",'" & (Microsoft.VisualBasic.Right(FTReset.Text, 2) & "/" & Microsoft.VisualBasic.Left(FTReset.Text, 2)) & "'  "
                    _Qry &= vbCrLf & ",0,0"
                    _Qry &= vbCrLf & ",0,'0'"
                    _Qry &= vbCrLf & ",N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                    _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB

                    If HI.Conn.SQLConn.ExecuteTran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        Return False
                    End If

                End If

                If Me.FNStartAge.Value > 0 Or Me.FNEndAge.Value > 0 Or Me.FNRight.Value > 0 Then

                    If Me.FTStateMax.Checked Then
                        _Qry = "UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMConfigLeaveVacation SET "
                        _Qry &= vbCrLf & " FTStateMax='0'"
                        _Qry &= vbCrLf & " WHERE  FNHSysEmpTypeId=" & Val(FNHSysEmpTypeId.Properties.Tag.ToString) & " "
                        HI.Conn.SQLConn.ExecuteTran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)
                    End If

                    _Qry = "UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMConfigLeaveVacation SET "
                    _Qry &= vbCrLf & " FNAgeBegin=" & Me.FNStartAge.Value
                    _Qry &= vbCrLf & ",FNAgeEnd=" & Me.FNEndAge.Value
                    _Qry &= vbCrLf & ",FNLeaveRight=" & Me.FNRight.Value
                    _Qry &= vbCrLf & ",FNLeaveReturn=" & Me.FNLeaveReturn.Value
                    _Qry &= vbCrLf & ",FTStateMax='" & IIf(Me.FTStateMax.Checked, "1", "0") & "'"
                    _Qry &= vbCrLf & ",FTUpdUser = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Qry &= vbCrLf & ",FDUpdDate = " & HI.UL.ULDate.FormatDateDB & ""
                    _Qry &= vbCrLf & ",FTUpdTime = " & HI.UL.ULDate.FormatTimeDB & ""
                    _Qry &= vbCrLf & " WHERE  FNHSysEmpTypeId=" & Val(FNHSysEmpTypeId.Properties.Tag.ToString) & " "
                    _Qry &= vbCrLf & "  AND FNSeqNo=" & Me.FNSeq.Value

                    If HI.Conn.SQLConn.ExecuteTran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        _Qry = "SELECT MAX(FNSeqNo) AS FNSeqNo FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMConfigLeaveVacation WHERE  FNHSysEmpTypeId=" & Val(FNHSysEmpTypeId.Properties.Tag.ToString) & " "

                        Dim tSeqNo As String = HI.Conn.SQLConn.GetFieldOnBeginTrans(_Qry, Conn.DB.DataBaseName.DB_HR, "0")
                        tSeqNo = Val(tSeqNo) + 1

                        _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMConfigLeaveVacation(FNHSysEmpTypeId,FNSeqNo,FNAgeBegin,FNAgeEnd,FNLeaveRight,FNLeaveReturn"
                        _Qry &= vbCrLf & ", FTInsUser, FDInsDate, FTInsTime , FTStateMax)  "
                        _Qry &= vbCrLf & " SELECT " & Val(FNHSysEmpTypeId.Properties.Tag.ToString) & "," & tSeqNo & ""
                        _Qry &= vbCrLf & "," & Me.FNStartAge.Value & "," & Me.FNEndAge.Value & "," & Me.FNRight.Value & "," & Me.FNLeaveReturn.Value
                        _Qry &= vbCrLf & ",N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                        _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                        _Qry &= vbCrLf & ",'" & IIf(Me.FTStateMax.Checked, "1", "0") & "'"
                        If HI.Conn.SQLConn.ExecuteTran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                            HI.Conn.SQLConn.Tran.Rollback()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                            Return False
                        End If

                    End If


                    _Qry = "UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.THRMConfigLeaveVacation SET FNSeqNo=FNNo"
                    _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.THRMConfigLeaveVacation INNER JOIN "
                    _Qry &= vbCrLf & "(SELECT ROW_NUMBER() OVER(ORDER BY FNAgeBegin,FNAgeEnd) AS FNNo, FNSeqNo,FNHSysEmpTypeId"
                    _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.THRMConfigLeaveVacation WHERE  FNHSysEmpTypeId=" & Val(FNHSysEmpTypeId.Properties.Tag.ToString) & " "
                    _Qry &= vbCrLf & ") T1 ON THRMConfigLeaveVacation.FNSeqNo=T1.FNSeqNo AND THRMConfigLeaveVacation.FNHSysEmpTypeId=T1.FNHSysEmpTypeId"

                    HI.Conn.SQLConn.ExecuteTran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

                    Me.FNStartAge.Value = 0
                    Me.FNEndAge.Value = 0
                    Me.FNRight.Value = 0
                    Me.FNLeaveReturn.Value = 0
                    Me.FNSeq.Value = 0
                    Me.FTStateMax.Checked = False

                End If

                Me.FNSeq.Value = 0

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
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function DeleteData() As Boolean
        If Me.FNStartAge.Value > 0 Or Me.FNEndAge.Value > 0 Or Me.FNRight.Value > 0 Then


            Dim _Qry As String = ""
            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_HR)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            Try
                _Qry = " Delete  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMConfigLeaveVacation  "
                _Qry &= vbCrLf & " WHERE  FNHSysEmpTypeId=" & Val(FNHSysEmpTypeId.Properties.Tag.ToString) & " "
                _Qry &= vbCrLf & "  AND FNSeqNo=" & Me.FNSeq.Value

                HI.Conn.SQLConn.ExecuteTran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)


                _Qry = "UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.THRMConfigLeaveVacation SET FNSeqNo=FNNo"
                _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.THRMConfigLeaveVacation INNER JOIN "
                _Qry &= vbCrLf & "(SELECT ROW_NUMBER() OVER(ORDER BY FNAgeBegin,FNAgeEnd) AS FNNo, FNSeqNo,FNHSysEmpTypeId"
                _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.THRMConfigLeaveVacation WHERE  FNHSysEmpTypeId=" & Val(FNHSysEmpTypeId.Properties.Tag.ToString) & " "
                _Qry &= vbCrLf & ") T1 ON THRMConfigLeaveVacation.FNSeqNo=T1.FNSeqNo AND THRMConfigLeaveVacation.FNHSysEmpTypeId=T1.FNHSysEmpTypeId"

                HI.Conn.SQLConn.ExecuteTran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

                HI.Conn.SQLConn.Tran.Commit()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                Me.FNSeq.Value = 0

                Return True

            Catch ex As Exception
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                Return False
            End Try
        Else
            Return False
        End If

    End Function

    Private Function VerrifyData() As Boolean
        Dim _Pass As Boolean = False

        If Me.FNHSysEmpTypeId.Text <> "" And FNHSysEmpTypeId.Properties.Tag.ToString <> "" Then
            If HI.UL.ULDate.CheckDate(FTReset.Text & "/" & "2013") <> "" Then

                _Pass = True
            Else
                HI.MG.ShowMsg.mInvalidData("กรุณาระบุวันที่ Clear สิทธิ์", 1104110001, Me.Text)
                FTReset.Focus()
            End If
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, Me.FNHSysEmpTypeId_lbl.Text)
            FNHSysEmpTypeId.Focus()
        End If

        Return _Pass
    End Function

#End Region

#Region "General"
    Private Sub ProcLoad(ByVal FNHSysTypeEmpID As String)

        Dim _DtLeave As DataTable
        Dim _DTvacation As DataTable
        Dim _Qry As String = ""
        Dim _reset As String = ""
        Me.ogc.DataSource = Nothing
        Me.ogcvacation.DataSource = Nothing
        _Qry = "SELECt TOP 1 FTLeaveReset FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMConfigLeave  WHERE FNHSysEmpTypeId=" & Val(FNHSysTypeEmpID) & " "
        _reset = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "")

        If _reset <> "" Then _reset = (Microsoft.VisualBasic.Right(_reset, 2) & "/" & Microsoft.VisualBasic.Left(_reset, 2))

        Me.FTReset.Text = _reset

        _Qry = " SELECT L.FNListIndex As FTLeaveCode"

        If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
            _Qry &= vbCrLf & " ,L.FTNameTH AS FTLeaveName"
        Else
            _Qry &= vbCrLf & " ,L.FTNameEN AS FTLeaveName"
        End If

        _Qry &= vbCrLf & " ,ISNULL(CF.FNLeaveRight,0) AS FNLeaveRight "
        _Qry &= vbCrLf & " ,ISNULL(CF.FNLeavePay,0) As FNLeavePay "
        _Qry &= vbCrLf & " ,ISNULL(CF.FNLeaveAge,0) AS FNLeaveAge "
        _Qry &= vbCrLf & " ,ISNULL(CF.FTStaHoliday,'0') As FTStaHoliday,ISNULL(CF.FTStateDeductVacation,'0') AS FTStateDeductVacation	"
        _Qry &= vbCrLf & "  ,ISNULL(CF.FTAddAvg24Period,'0') AS FTAddAvg24Period   "
        _Qry &= vbCrLf & " FROM V_LeaveType AS L LEFT OUTER JOIN "
        _Qry &= vbCrLf & " (SELECT FTLeaveCode "
        _Qry &= vbCrLf & "  ,FTLeaveReset "
        _Qry &= vbCrLf & "  ,FNLeaveRight "
        _Qry &= vbCrLf & "  ,FNLeavePay "
        _Qry &= vbCrLf & "  ,FNLeaveAge "
        _Qry &= vbCrLf & "  ,FTStaHoliday,ISNULL(FTStateDeductVacation,'0') AS FTStateDeductVacation   "
        _Qry &= vbCrLf & "  ,FTAddAvg24Period  "

        _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMConfigLeave AS CFL WITH(NOLOCK) "
        _Qry &= vbCrLf & " WHERE FNHSysEmpTypeId=" & Val(FNHSysTypeEmpID) & ") AS CF ON L.FNListIndex =CF.FTLeaveCode "
        _Qry &= vbCrLf & " WHERE L.FNListIndex<>98 "
        _Qry &= vbCrLf & " Order By Convert(numeric(18,0),L.FNListIndex) "
        _DtLeave = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

        Me.ogc.DataSource = _DtLeave

        _Qry = " SELECT  FNSeqNo, Convert(numeric(18,1),FNAgeBegin) AS FNAgeBegin, Convert(numeric(18,1),FNAgeEnd) AS FNAgeEnd  "
        _Qry &= vbCrLf & "  , Convert(numeric(18,1),FNLeaveRight) AS FNLeaveRight "
        _Qry &= vbCrLf & "  , Convert(numeric(18,1),ISNULL(FNLeaveReturn,0)) AS FNLeaveReturn  ,isnull( FTStateMax , '0')  as FTStateMax"
        _Qry &= vbCrLf & "  ,ISNULL(FTAddAvg24Period,'0') AS FTAddAvg24Period "
        _Qry &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMConfigLeaveVacation   WHERE FNHSysEmpTypeId=" & Val(FNHSysTypeEmpID) & "  ORDER BY FNSeqNo"
        _DTvacation = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)
        Me.ogcvacation.DataSource = _DTvacation


        '',ISNULL(CF.FTAddAvg24Period,'0') AS FTAddAvg24Period
    End Sub

    Private Sub ogvvacation_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ogvvacation.Click
        With ogvvacation
            If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount Then Exit Sub

            Me.FNSeq.Value = "" & .GetRowCellValue(.FocusedRowHandle, "FNSeqNo").ToString
            Me.FNStartAge.Value = "" & .GetRowCellValue(.FocusedRowHandle, "FNAgeBegin").ToString
            Me.FNEndAge.Value = "" & .GetRowCellValue(.FocusedRowHandle, "FNAgeEnd").ToString
            Me.FNRight.Value = "" & .GetRowCellValue(.FocusedRowHandle, "FNLeaveRight").ToString
            Me.FNLeaveReturn.Value = Val("" & .GetRowCellValue(.FocusedRowHandle, "FNLeaveReturn").ToString)
            Me.FTStateMax.Checked = IIf("" & .GetRowCellValue(.FocusedRowHandle, "FTStateMax").ToString = "1", True, False)
            Me.FNRight.Focus()


        End With
    End Sub

    Private Sub FNHSysEmpTypeId_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles FNHSysEmpTypeId.EditValueChanged


        If Me.InvokeRequired Then
            Me.Invoke(New HI.Delegate.Dele.FNHSysEmpID_EditValueChanged(AddressOf FNHSysEmpTypeId_EditValueChanged), New Object() {sender, e})
        Else
            If FNHSysEmpTypeId.Text <> "" Then


                If (_ProcPrepare) Then Exit Sub
                ''Call ProcLoad()

                Dim _Qry As String = "SELECT TOP 1 FNHSysEmpTypeId  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType WITH(NOLOCK) WHERE FTEmpTypeCode ='" & HI.UL.ULF.rpQuoted(FNHSysEmpTypeId.Text) & "' AND FnhSysCmpID = " & HI.ST.SysInfo.CmpID

                ''  _Qry = HI.ST.Security.PermissionEmpType(_Qry)

                FNHSysEmpTypeId.Properties.Tag = Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MASTER, 0))

                Call ProcLoad(FNHSysEmpTypeId.Properties.Tag.ToString)
            Else

            End If
        End If
    End Sub

    Private Sub wConfigLeave_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        AddHandler RepFNLeaveAge.Leave, AddressOf HI.TL.HandlerControl.CalEdit_Leave
        AddHandler RepFNLeavePay.Leave, AddressOf HI.TL.HandlerControl.CalEdit_Leave
        AddHandler RepFNLeaveRight.Leave, AddressOf HI.TL.HandlerControl.CalEdit_Leave

    End Sub


#End Region

    Private Sub FNStartAge_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles FNStartAge.EditValueChanging, FNEndAge.EditValueChanging, FNRight.EditValueChanging, FNLeaveReturn.EditValueChanging

        Try

            'If e.NewValue Mod 0.5 <> 0 Then
            '    e.Cancel = True
            'Else
            '    e.Cancel = False
            'End If

            e.Cancel = False

        Catch ex As Exception
        End Try

    End Sub

    Private Sub FNLeaveReturn_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles FNLeaveReturn.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If Me.ocmsave.Enabled And Me.ocmsave.Visible Then
                ProcessSave(ocmsave, New System.EventArgs)
            End If
        End If
    End Sub

    Private Sub FNStartAge_EditValueChanged(sender As Object, e As EventArgs) Handles FNStartAge.EditValueChanged
    End Sub

End Class
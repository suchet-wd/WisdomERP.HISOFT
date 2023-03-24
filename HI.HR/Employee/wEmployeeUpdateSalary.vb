Public Class wEmployeeUpdateSalary


#Region "MAIN PROC"

    Private Sub ProcessSave(sender As System.Object, e As System.EventArgs) Handles ocmsave.Click
        If Me.VerrifyData Then
            If HI.MG.ShowMsg.mConfirmProcess("คุณต้องการทำการบันทึกข้อมูลการปรับเงินเดือนพนักงานใช่หรือไม่ ?", 1404300005) Then
                Dim _Spls As New HI.TL.SplashScreen("Saving...   Please Wait   ")
                If Me.SaveData() Then
                    HI.TL.HandlerControl.ClearControl(Me.ogbup)
                    Call LoadPeriod(FNHSysEmpID.Properties.Tag.ToString)
                    _Spls.Close()
                    Call LoadHistory(FNHSysEmpID.Properties.Tag.ToString)
                    HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                Else
                    _Spls.Close()
                    HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                End If
            End If
        End If
    End Sub

    Private Sub ProcessDelete(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmdelete.Click
        If Me.VerrifyData Then

            Dim _Spls As New HI.TL.SplashScreen("Deleting...   Please Wait   ")
            If Me.DeleteData(_Spls) Then
                HI.TL.HandlerControl.ClearControl(Me.ogbup)
                Call LoadPeriod(FNHSysEmpID.Properties.Tag.ToString)
                _Spls.Close()
                Call LoadHistory(FNHSysEmpID.Properties.Tag.ToString)
                HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
            Else
                '  _Spls.Close()
                '  HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
            End If

        End If
    End Sub

    Private Sub ProcessClear(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmclear.Click
        'Me.FormRefresh()

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
            Dim StrLastEffDate As String = ""
            Dim _CurrenAmt As Double = 0
            Dim _NewAmt As Double = 0
            Dim tSeqNo As String = "0"
            Dim _Dt As DataTable
            Dim _DtEff As DataTable
            Dim _EmpNewSalary As Double = 0
            Dim EmpSlary As Double = 0
            Dim _Qry As String = ""

            _Qry = " SELECT TOP 1 FTEffectiveDate,FNCurrentSlary,FNNewSlary FROM THRTEmployeeMasterChangeSlary WITH (NOLOCK)  "
            _Qry &= "  WHERE   FNHSysEmpId=" & Val(FNHSysEmpID.Properties.Tag.ToString) & "  "
            _Qry &= " ORDER By FTEffectiveDate DESC "
            _DtEff = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)
            For Each R As DataRow In _DtEff.Rows
                StrLastEffDate = R!FTEffectiveDate.ToString
                _CurrenAmt = Val(R!FNCurrentSlary.ToString)
                _NewAmt = Val(R!FNNewSlary.ToString)
                Exit For
            Next

            _Qry = " SELECT TOP 1 FNSeq ,FTEffectiveDate,FTCurrentSlary,FTNewSlary FROM THRTEmployeeUpdateSlary WITH (NOLOCK)  "
            _Qry &= "  WHERE   FNHSysEmpId=" & Val(FNHSysEmpID.Properties.Tag.ToString) & "  "
            _Qry &= " AND FTEffectiveDate='" & HI.UL.ULDate.ConvertEnDB(FTEffectiveDate.Text) & "'  "
            _Dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)


            For Each R As DataRow In _Dt.Rows

                tSeqNo = Val(R!FNSeq.ToString)
                StrLastEffDate = R!FTEffectiveDate.ToString
                _CurrenAmt = Val(R!FTCurrentSlary.ToString)
                _NewAmt = Val(R!FTNewSlary.ToString)

                Exit For
            Next

            'If StrLastEffDate > HI.UL.ULDate.ConvertEnDB(FTEffectiveDate.Text) And StrLastEffDate <> "" Then
            '    Return False
            'End If
            EmpSlary = FTCurrentSlary.Value

            Select Case FNCngMinimumWage.SelectedIndex
                Case 0
                    _EmpNewSalary = FTNewSlary.Value
                Case 1
                    _EmpNewSalary = EmpSlary + FTNewSlary.Value
                Case 2
                    _EmpNewSalary = EmpSlary + CDbl(Format(((EmpSlary * FTNewSlary.Value) / 100), "0.00"))
                Case Else
                    _EmpNewSalary = EmpSlary
            End Select

            tSeqNo = FNSeq.Value.ToString
            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_HR)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            Try

                _Qry = "UPDATE  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeUpdateSlary SET "
                _Qry &= vbCrLf & " FNHSysPmtReasoneId=" & Val(FNHSysPmtReasoneId.Properties.Tag.ToString) & " "
                _Qry &= vbCrLf & ", FTCurrentSlary=" & Me.FTCurrentSlary.Value & " "
                _Qry &= vbCrLf & ", FTNewSlary=" & _EmpNewSalary & " "
                _Qry &= vbCrLf & ", FTEffectiveDate='" & HI.UL.ULDate.ConvertEnDB(FTEffectiveDate.Text) & "' "
                _Qry &= vbCrLf & ", FTNote=N'" & HI.UL.ULF.rpQuoted(FTRemark.Text) & "' "
                _Qry &= vbCrLf & ",FTUpdUser = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Qry &= vbCrLf & ",FTUpdDate = " & HI.UL.ULDate.FormatDateDB & ""
                _Qry &= vbCrLf & ",FTUpdTime = " & HI.UL.ULDate.FormatTimeDB & ""
                _Qry &= vbCrLf & " WHERE  FNHSysEmpID=" & Val(FNHSysEmpID.Properties.Tag.ToString) & " "
                _Qry &= vbCrLf & "  AND FNSeq=" & Val(tSeqNo)

                If HI.Conn.SQLConn.ExecuteTran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    _Qry = "SELECT MAX(FNSeq) AS FNSeqNo FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeUpdateSlary WHERE  FNHSysEmpID=" & Val(FNHSysEmpID.Properties.Tag.ToString) & " "

                    tSeqNo = HI.Conn.SQLConn.GetFieldOnBeginTrans(_Qry, Conn.DB.DataBaseName.DB_HR, "0")
                    tSeqNo = Val(tSeqNo) + 1

                    _Qry = "INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeUpdateSlary(FNHSysEmpID, FNSeq, FNHSysPmtReasoneId, FTCurrentSlary, FTNewSlary, FTEffectiveDate, FTNote"
                    _Qry &= vbCrLf & ", FTInsUser, FTInsDate, FTInsTime)  "
                    _Qry &= vbCrLf & " SELECT " & Val(FNHSysEmpID.Properties.Tag.ToString) & "," & tSeqNo & ""
                    _Qry &= vbCrLf & "," & Val(FNHSysPmtReasoneId.Properties.Tag.ToString) & " "
                    _Qry &= vbCrLf & "," & Me.FTCurrentSlary.Value & "," & _EmpNewSalary & ""
                    _Qry &= vbCrLf & ",'" & HI.UL.ULDate.ConvertEnDB(FTEffectiveDate.Text) & "' "
                    _Qry &= vbCrLf & ",N'" & HI.UL.ULF.rpQuoted(FTRemark.Text) & "' "
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


                _Qry = "UPDATE THRTEmployeeUpdateSlary SET FNSeq=FNNo"
                _Qry &= vbCrLf & " FROM THRTEmployeeUpdateSlary INNER JOIN "
                _Qry &= vbCrLf & "(SELECT ROW_NUMBER() OVER(ORDER BY FTEffectiveDate) AS FNNo, FNSeq,FNHSysEmpId"
                _Qry &= vbCrLf & " FROM THRTEmployeeUpdateSlary WHERE  FNHSysEmpId=" & Val(FNHSysEmpID.Properties.Tag.ToString) & " "
                _Qry &= vbCrLf & ") T1 ON THRTEmployeeUpdateSlary.FNSeq=T1.FNSeq AND THRTEmployeeUpdateSlary.FNHSysEmpId=T1.FNHSysEmpId"

                HI.Conn.SQLConn.ExecuteTran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

                HI.Conn.SQLConn.Tran.Commit()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                HI.Conn.SQLConn.ExecuteNonQuery(" EXEC SP_UPDATE_WAGE ", Conn.DB.DataBaseName.DB_HR)

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

    Private Function DeleteData(ByVal _Spls As HI.TL.SplashScreen) As Boolean

        Try
            Dim StrLastEffDate As String = ""
            Dim _CurrenAmt As Double = 0
            Dim _NewAmt As Double = 0

            Dim _Dt As DataTable
            Dim _DtEff As DataTable

            Dim _Qry As String = ""

            _Qry = " SELECT TOP 1 FTEffectiveDate,FNCurrentSlary,FNNewSlary FROM THRTEmployeeMasterChangeSlary WITH (NOLOCK)  "
            _Qry &= "  WHERE   FNHSysEmpId=" & Val(FNHSysEmpID.Properties.Tag.ToString) & "  "
            _Qry &= " ORDER By FTEffectiveDate DESC "
            _DtEff = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)
            For Each R As DataRow In _DtEff.Rows
                StrLastEffDate = R!FTEffectiveDate.ToString
                _CurrenAmt = Val(R!FNCurrentSlary.ToString)
                _NewAmt = Val(R!FNNewSlary.ToString)
                Exit For
            Next

            '_Qry = " SELECT TOP 1 FTEffectiveDate,FTCurrentSlary,FTNewSlary FROM THRTEmployeeUpdateSlary WITH (NOLOCK)  "
            '_Qry &= "  WHERE   FNHSysEmpId=" & Val(FNHSysEmpID.Properties.Tag.ToString) & "  "
            '_Qry &= " ORDER By FTEffectiveDate DESC "
            '_Dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

            'If _Dt.Rows.Count <= 0 Then Return False

            'For Each R As DataRow In _Dt.Rows
            '    StrLastEffDate = R!FTEffectiveDate.ToString
            '    _CurrenAmt = Val(R!FTCurrentSlary.ToString)
            '    _NewAmt = Val(R!FTNewSlary.ToString)
            '    Exit For
            'Next

            If StrLastEffDate > HI.UL.ULDate.ConvertEnDB(FTEffectiveDate.Text) Then
                _Spls.Close()
                HI.MG.ShowMsg.mInfo("กรุณาลบจากวันที่ล่าสุด ถอยกลับ....", 1307080001, Me.Text)
                Return False
            End If

            Dim tSeqNo As String
            tSeqNo = FNSeq.Value.ToString
            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_HR)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            Try

                _Qry = " UPDATE THRMEmployee SET FNSalary=" & FTCurrentSlary.Value & "  "
                _Qry &= vbCrLf & " WHERE  FNHSysEmpId=" & Val(FNHSysEmpID.Properties.Tag.ToString) & " "
                HI.Conn.SQLConn.ExecuteTran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)


                _Qry = " Delete  THRTEmployeeUpdateSlary  "
                _Qry &= vbCrLf & " WHERE  FNHSysEmpId=" & Val(FNHSysEmpID.Properties.Tag.ToString) & " "
                _Qry &= vbCrLf & "  AND FNSeq=" & Val(tSeqNo)

                ' _Qry &= vbCrLf & "  AND FTEffectiveDate='" & HI.UL.ULDate.ConvertEnDB(FTEffectiveDate.Text) & "' "
                HI.Conn.SQLConn.ExecuteTran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

                _Qry = " Delete  THRTEmployeeMasterChangeSlary  "
                _Qry &= vbCrLf & " WHERE  FNHSysEmpId=" & Val(FNHSysEmpID.Properties.Tag.ToString) & " "
                _Qry &= vbCrLf & "  AND FNSeq=" & Val(tSeqNo)
                HI.Conn.SQLConn.ExecuteTran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)


                _Qry = " UPDATE THRTEmployeeUpdateSlary SET FNSeq=FNNo"
                _Qry &= vbCrLf & " FROM THRTEmployeeUpdateSlary INNER JOIN "
                _Qry &= vbCrLf & "(SELECT ROW_NUMBER() OVER(ORDER BY FTEffectiveDate) AS FNNo, FNSeq,FNHSysEmpId"
                _Qry &= vbCrLf & " FROM THRTEmployeeUpdateSlary WHERE  FNHSysEmpId=" & Val(FNHSysEmpID.Properties.Tag.ToString) & " "
                _Qry &= vbCrLf & ") T1 ON THRTEmployeeUpdateSlary.FNSeq=T1.FNSeq AND THRTEmployeeUpdateSlary.FNHSysEmpId=T1.FNHSysEmpId"

                HI.Conn.SQLConn.ExecuteTran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

                HI.Conn.SQLConn.Tran.Commit()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                Return True

            Catch ex As Exception
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                _Spls.Close()
                HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
                Return False
            End Try

        Catch ex As Exception
            _Spls.Close()
            HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
            Return False
        End Try

    End Function

    Private Function VerrifyData() As Boolean
        Dim _Pass As Boolean = False
        If Me.FNHSysEmpID.Text <> "" And FNHSysEmpID.Properties.Tag.ToString <> "" Then
            If HI.UL.ULDate.CheckDate(FTEffectiveDate.Text) <> "" And HI.UL.ULDate.CheckDate(FTEffectiveDate.Text) <> "" Then
                If Me.FNHSysPmtReasoneId.Text <> "" And FNHSysPmtReasoneId.Properties.Tag.ToString <> "" Then
                    If FTNewSlary.Value > 0 Then
                        _Pass = True
                    Else
                        HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FTNewSlary_lbl.Text)
                        FTNewSlary.Focus()
                    End If
                Else
                    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FNHSysPmtReasoneId_lbl.Text)
                    FNHSysPmtReasoneId.Focus()
                End If
            Else
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FTEffectiveDate_lbl.Text)
                FTEffectiveDate.Focus()
            End If
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, Me.FNHSysEmpID_lbl.Text)
            FNHSysEmpID.Focus()
        End If

        Return _Pass
    End Function

#End Region

    Private Sub LoadPeriod(ByVal FNHSysEmpID As String)
        Dim _dt As DataTable
        Dim _pdt As DataTable
        Dim _Qry As String = ""
        Dim _Str As String = ""
        Dim _StateSalary As Integer

        Dim _PathEmpPic As String
        _PathEmpPic = ""
        Dim cmdstring As String = "Select Top 1 FTCfgData FROM  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & ".dbo.TSESystemConfig AS X WITH(NOLOCK) WHERE FTCfgName='PathEmpPic'"

        _PathEmpPic = HI.Conn.SQLConn.GetField(cmdstring, Conn.DB.DataBaseName.DB_SECURITY, "")


        _Qry = " SELECT    TOP 1     M.FTEmpCode, M.FTEmpCodeRefer, M.FTEmpNameTH, M.FTEmpSurnameTH, M.FTEmpNicknameTH, M.FTEmpNameEN, M.FNHSysEmpTypeId, M.FNHSysDeptId, "
        _Qry &= vbCrLf & "   D.FTDeptCode, Di.FTDivisonCode, M.FNHSysDivisonId, M.FNHSysSectId, S.FTSectCode, ET.FTEmpTypeCode, M.FNHSysUnitSectId, US.FTUnitSectCode,"
        _Qry &= vbCrLf & "  M.FNHSysEmpID, M.FTEmpPicName, M.FNHSysPositId, P.FTPositCode,M.FNSalary"
        _Qry &= vbCrLf & "  FROM            THRMEmployee AS M WITH (NOLOCK) LEFT OUTER JOIN"
        _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMPosition AS P WITH (NOLOCK) ON M.FNHSysPositId = P.FNHSysPositId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS US WITH (NOLOCK) ON M.FNHSysUnitSectId = US.FNHSysUnitSectId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS ET WITH (NOLOCK) ON M.FNHSysEmpTypeId = ET.FNHSysEmpTypeId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSect AS S WITH (NOLOCK) ON M.FNHSysSectId = S.FNHSysSectId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDivision AS Di WITH (NOLOCK) ON M.FNHSysDivisonId = Di.FNHSysDivisonId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDepartment AS D WITH (NOLOCK) ON M.FNHSysDeptId = D.FNHSysDeptId"
        _Qry &= vbCrLf & "  WHERE  M.FNHSysEmpID  =" & Val(FNHSysEmpID) & ""
        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

        FTEmpPicName.Image = Nothing
        If _dt.Rows.Count > 0 Then
            For Each R As DataRow In _dt.Rows

                If _PathEmpPic = "" Then
                    FTEmpPicName.Image = HI.UL.ULImage.LoadImage(HI.ST.SysInfo.SysPath & "EmpPicture\" & R!FTEmpPicName.ToString)
                Else
                    FTEmpPicName.Image = HI.UL.ULImage.LoadImage(_PathEmpPic & R!FTEmpPicName.ToString)
                End If
                FNHSysEmpTypeId.Text = R!FTEmpTypeCode.ToString
                FNHSysDeptId.Text = R!FTDeptCode.ToString
                FNHSysDivisonId.Text = R!FTDivisonCode.ToString
                FNHSysSectId.Text = R!FTSectCode.ToString
                FNHSysUnitSectId.Text = R!FTUnitSectCode.ToString
                FNHSysPositId.Text = R!FTPositCode.ToString
                If ((HI.ST.SysInfo.Admin)) Then
                    FTCurrentSlary.Value = Val(R!FNSalary.ToString)
                Else
                    _Str = "Select U.FNHSysPermissionID ,T.FNHSysEmpTypeId ,T.FTStateSalary"
                    _Str &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEUserLoginPermission AS U With(NOLOCK) LEFT OUTER JOIN"
                    _Str &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEPermissionEmployeeType AS T With(NOLOCK) ON U.FNHSysPermissionID = T.FNHSysPermissionID"
                    _Str &= vbCrLf & " WHERE FTUserName = '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' AND FNHSysEmpTypeId = '" & R!FNHSysEmpTypeId.ToString & "'"
                    _Str &= vbCrLf & " ORDER BY FTStateSalary DESC"
                    _pdt = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_SECURITY)
                    For Each pR As DataRow In _pdt.Rows
                        If ((pR!FTStateSalary.ToString) = "1") Then
                            FTCurrentSlary.Value = Val(R!FNSalary.ToString)
                            Exit For
                        ElseIf ((pR!FTStateSalary.ToString) = "0") Then

                        Else
                            FTCurrentSlary.Value = Val(R!FNSalary.ToString)
                        End If
                    Next
                End If

            Next
        Else

            FNHSysEmpTypeId.Text = ""
            FNHSysDeptId.Text = ""
            FNHSysDivisonId.Text = ""
            FNHSysSectId.Text = ""
            FNHSysUnitSectId.Text = ""
            FNHSysPositId.Text = ""
            FTCurrentSlary.Value = 0

        End If

        Call LoadHistory(FNHSysEmpID)

    End Sub


    Private Sub LoadHistory(ByVal FNHSysEmpID As String)

        Dim _Qry As String = ""

        _Qry = " SELECT     CASE WHEN ISDATE(U.FTEffectiveDate) =1 THEN Convert(varchar(10),Convert(DateTime,U.FTEffectiveDate),103) ELSE '' END AS FTEffectiveDate"
        _Qry &= vbCrLf & ", U.FTCurrentSlary  AS FTCurrentSlary"
        _Qry &= vbCrLf & ", U.FTNewSlary  AS FTNewSlary"
        _Qry &= vbCrLf & ", CASE WHEN ISDATE(U.FTInsDate) =1 THEN Convert(varchar(10),Convert(DateTime,U.FTInsDate),103) ELSE '' END AS FTInsDate "
        _Qry &= vbCrLf & ", U.FTInsUser"

        If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
            _Qry &= vbCrLf & ", ISNULL(P.FTPmtReasonNameTH,'') AS FTReasonName"
        Else
            _Qry &= vbCrLf & ", ISNULL(P.FTPmtReasonNameEN,'') AS FTReasonName"
        End If

        _Qry &= vbCrLf & ",P.FTPmtReasonCode"
        _Qry &= vbCrLf & ",U.FTNote"
        _Qry &= vbCrLf & ", U.FNHSysPmtReasoneId,U.FNSeq  "
        _Qry &= vbCrLf & " FROM   (SELECT FTEffectiveDate,FNSeq FROM THRTEmployeeUpdateSlary WITH(NOLOCK)"
        _Qry &= vbCrLf & " WHERE  (FNHSysEmpID = " & Val(FNHSysEmpID) & ") AND ISNULL(FTType,'') ='' "
        _Qry &= vbCrLf & "  ) AS UM1 INNER JOIN  THRTEmployeeUpdateSlary AS U WITH (NOLOCK) ON UM1.FNSeq=U.FNSeq LEFT OUTER JOIN"
        _Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMPromotionReason AS P WITH (NOLOCK) ON U.FNHSysPmtReasoneId = P.FNHSysPmtReasoneId"
        _Qry &= vbCrLf & " WHERE  (U.FNHSysEmpID = " & Val(FNHSysEmpID) & " ) "
        '  _Qry &= vbCrLf & " AND ISNULL(FTType,'') ='' "
        _Qry &= vbCrLf & " ORDER BY U.FNSeq  DESC"

        Me.ogc.DataSource = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

    End Sub

    Private Sub FNHSysEmpID_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FNHSysEmpID.EditValueChanged
        If FNHSysEmpID.Text <> "" Then
            If FNHSysEmpID.Properties.Tag.ToString = "" Then
                Dim _Qry As String = "SELECT TOP 1 FNHSysEmpID  FROM THRMEmployee WITH(NOLOCK) WHERE FTEmpCode ='" & HI.UL.ULF.rpQuoted(FNHSysEmpID.Text) & "' "
                FNHSysEmpID.Properties.Tag = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "")
            End If

            Call LoadPeriod(FNHSysEmpID.Properties.Tag.ToString)

        End If
    End Sub

    Private Sub FTEffectiveDate_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FTEffectiveDate.EditValueChanged
        If FNHSysEmpID.Text <> "" Then
            If FNHSysEmpID.Properties.Tag.ToString <> "" Then
                Dim _Qry As String = ""


                '_Qry = " SELECT TOP 1  FNSeq, FNHSysEmpID, FNHSysPmtReasoneId, FTCurrentSlary, FTNewSlary, FTEffectiveDate, FTNote FROM THRTEmployeeUpdateSlary  WITH (NOLOCK)  "
                '_Qry &= "  WHERE   FNHSysEmpId=" & Val(FNHSysEmpID.Properties.Tag.ToString) & "  "
                '_Qry &= "  AND    FTEffectiveDate='" & HI.UL.ULDate.ConvertEnDB(FTEffectiveDate.Text) & "' "

                '_Dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

                'If _Dt.Rows.Count > 0 Then
                '    For Each R As DataRow In _Dt.Rows

                '        HI.TL.HandlerControl.DynamicButtonediHSysKey_Leave(FNHSysPmtReasoneId, Val(R!FNHSysPmtReasoneId.ToString))
                '        FTCurrentSlary.Value = Val(R!FTCurrentSlary.ToString)
                '        FTNewSlary.Value = Val(R!FTNewSlary.ToString)
                '        FTRemark.Text = R!FTNote.ToString
                '        FNSeq.Value = Val(R!FNSeq.ToString)
                '        FTNewSlary.Focus()
                '        Exit For
                '    Next


            End If
        End If
    End Sub

    Private Sub ogv_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ogv.DoubleClick
        With ogv
            If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount Then Exit Sub

            Try
                FTEffectiveDate.DateTime = HI.UL.ULDate.ConvertEnDB("" & .GetRowCellValue(.FocusedRowHandle, "FTEffectiveDate").ToString)
            Catch ex As Exception
                FTEffectiveDate.DateTime = Nothing
            End Try

            FNHSysPmtReasoneId.Text = "" & .GetRowCellValue(.FocusedRowHandle, "FTPmtReasonCode").ToString
            FTCurrentSlary.Value = "" & .GetRowCellValue(.FocusedRowHandle, "FTCurrentSlary").ToString
            FNCngMinimumWage.SelectedIndex = 0
            FTNewSlary.Value = "" & .GetRowCellValue(.FocusedRowHandle, "FTNewSlary").ToString
            FTRemark.Text = "" & .GetRowCellValue(.FocusedRowHandle, "FTNote").ToString
            FNSeq.Value = Val("" & .GetRowCellValue(.FocusedRowHandle, "FNSeq").ToString)
            FTRemark.Focus()

        End With
    End Sub

    Private Sub ogc_Click(sender As System.Object, e As System.EventArgs) Handles ogc.Click

    End Sub

    Private Sub wEmpUpdateWage_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Me.FNHSysCmpId.Text = HI.ST.SysInfo.CmpCode
    End Sub
End Class
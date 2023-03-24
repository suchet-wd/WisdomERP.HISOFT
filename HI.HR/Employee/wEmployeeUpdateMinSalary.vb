Public Class wEmployeeUpdateMinSalary

    Sub New()
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub ocmexit_Click(sender As System.Object, e As System.EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ocmclear_Click(sender As System.Object, e As System.EventArgs) Handles ocmclear.Click
        HI.TL.HandlerControl.ClearControl(Me)
    End Sub

    Private Sub ocmsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmsave.Click
        If Me.Verifydata Then

            Dim _Qry  As  String
            Dim _Dt As DataTable

            _Qry = " SELECT   M.FNHSysEmpID, M.FTEmpCode,M.FNSalary "
            _Qry &= vbCrLf & " FROM        THRMEmployee AS M WITH (NOLOCK)"
            _Qry &= vbCrLf & "    INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMPrename AS P WITH (NOLOCK) ON M.FNHSysPreNameId = P.FNHSysPreNameId INNER JOIN"
            _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS ET WITH(NOLOCK)  ON M.FNHSysEmpTypeId = ET.FNHSysEmpTypeId "
            _Qry &= vbCrLf & "   LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDepartment AS Dept WITH (Nolock) ON M.FNHSysDeptId = Dept.FNHSysDeptId "
            _Qry &= vbCrLf & "   LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDivision AS OrgDiv WITH (NOLOCK) ON M.FNHSysDivisonId = OrgDiv.FNHSysDivisonId "
            _Qry &= vbCrLf & "   LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSect AS OrgSect WITH (NOLOCK) ON M.FNHSysSectId = OrgSect.FNHSysSectId "
            _Qry &= vbCrLf & "   LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS OrgUnitSect WITH (NOLOCK) ON M.FNHSysUnitSectId = OrgUnitSect.FNHSysUnitSectId "



            _Qry &= vbCrLf & " WHERE  M.FTEmpCode <> '' and M.FNEmpStatus <> '2'  "
            _Qry &= vbCrLf & " AND M.FNHSysCmpId=" & HI.ST.SysInfo.CmpID & " "
            _Qry &= vbCrLf & " AND  M.FNSalary>=" & Me.FNStartSalary.Value & " AND  M.FNSalary <=" & Me.FNEndSalary.Value & ""

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
                _Qry &= vbCrLf & " AND  OrgDiv.FTDivisonCode>='" & HI.UL.ULF.rpQuoted(Me.FNHSysDivisonId.Text) & "' "
            End If

            If Me.FNHSysDivisonIdTo.Text <> "" Then
                _Qry &= vbCrLf & " AND  OrgDiv.FTDivisonCode<='" & HI.UL.ULF.rpQuoted(Me.FNHSysDivisonIdTo.Text) & "' "
            End If

            '------Criteria By Sect
            If Me.FNHSysSectId.Text <> "" Then
                _Qry &= vbCrLf & " AND  OrgSect.FTSectCode>='" & HI.UL.ULF.rpQuoted(Me.FNHSysSectId.Text) & "' "
            End If

            If Me.FNHSysSectIdTo.Text <> "" Then

                _Qry &= vbCrLf & " AND  OrgSect.FTSectCode<='" & HI.UL.ULF.rpQuoted(Me.FNHSysSectIdTo.Text) & "' "

            End If

            '------Criteria Unit Sect
            If Me.FNHSysUnitSectId.Text <> "" Then
                _Qry &= vbCrLf & " AND   OrgUnitSect.FTUnitSectCode>='" & HI.UL.ULF.rpQuoted(Me.FNHSysUnitSectId.Text) & "' "
            End If

            If Me.FNHSysUnitSectIdTo.Text <> "" Then
                _Qry &= vbCrLf & " AND   OrgUnitSect.FTUnitSectCode<='" & HI.UL.ULF.rpQuoted(Me.FNHSysUnitSectIdTo.Text) & "' "
            End If

            _Qry = HI.ST.Security.PermissionFilterEmployee(_Qry)

            _Qry &= vbCrLf & "  ORDER BY M.FTEmpCode ASC "
            _Dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

            If _Dt.Rows.Count > 0 Then
                Dim _Spls As New HI.TL.SplashScreen("Saving...   Please Wait   ")
                If Me.SaveData(_Spls, _Dt) Then
                    HI.Conn.SQLConn.ExecuteNonQuery(" EXEC SP_UPDATE_WAGE ", Conn.DB.DataBaseName.DB_HR)
                    _Spls.Close()
                    HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                Else
                    _Spls.Close()
                    HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                End If
            Else
                HI.MG.ShowMsg.mInvalidData("", 1005150002, Me.Text)
            End If

        End If

    End Sub

    Private Function Verifydata() As Boolean
        Dim _Pass As Boolean = False
        If Me.FNHSysEmpTypeId.Text <> "" And Me.FNHSysEmpTypeId.Properties.Tag.ToString <> "" Then
            If Me.FTEffectiveDate.Text <> "" Then
                If Me.FNStartSalary.Value >= 0 And Me.FNEndSalary.Value > 0 Then
                    If Me.FNEndSalary.Value >= Me.FNStartSalary.Value Then
                        If Me.FNNewSlary.Value > 0 Then
                            If Me.FNHSysPmtReasoneId.Text <> "" And Me.FNHSysPmtReasoneId.Properties.Tag.ToString <> "" Then
                                _Pass = True
                            Else
                                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, Me.FNHSysPmtReasoneId_lbl.Text)
                                FNHSysPmtReasoneId.Focus()
                            End If
                        Else
                            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, Me.FNNewSlary_lbl.Text)
                            FNNewSlary.Focus()
                        End If
                    Else
                        HI.MG.ShowMsg.mInvalidData("", 1005150001, Me.Text)
                        FNEndSalary.Focus()
                    End If
                Else
                    HI.MG.ShowMsg.mInvalidData("", 1005150001, Me.Text)
                    FNStartSalary.Focus()
                End If
            Else
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, Me.FTEffectiveDate_lbl.Text)
                FTEffectiveDate.Focus()
            End If
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, Me.FNHSysEmpTypeId_lbl.Text)
            FNHSysEmpTypeId.Focus()
        End If

        Return _Pass
    End Function

    Private Function SaveData(ByVal ObjSpls As HI.TL.SplashScreen, ByVal _DtEmp As DataTable) As Boolean
        Try
            Dim _TotalRec As Integer = _DtEmp.Rows.Count
            Dim _Rac As Integer = 0

            For Each R As DataRow In _DtEmp.Rows
                _Rac = _Rac + 1

                ObjSpls.UpdateInformation("Saving Data ... Employee : " & R!FTEmpCode.ToString & "  Records " & _Rac.ToString & " Of " & _TotalRec.ToString)

                Call SaveEmpUpdateWage(R!FNHSysEmpID.ToString, Val(R!FNSalary.ToString))

            Next

            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function

    Private Sub SaveEmpUpdateWage(ByVal FNHSysEmpID As String, ByVal EmpSlary As Double)
        Try
            Dim StrLastEffDate As String = ""
            Dim _CurrenAmt As Double = 0
            Dim _NewAmt As Double = 0
            Dim tSeqNo As String = "0"
            Dim _Dt As DataTable
            Dim _DtEff As DataTable
            Dim _EmpNewSalary As Double = 0

            Dim _Qry As String = ""

            _Qry = " SELECT TOP 1 FTEffectiveDate,FNCurrentSlary,FNNewSlary FROM THRTEmployeeMasterChangeSlary WITH (NOLOCK)  "
            _Qry &= "  WHERE   FNHSysEmpId=" & Val(FNHSysEmpID) & "  "
            _Qry &= " ORDER By FTEffectiveDate DESC "
            _DtEff = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

            For Each R As DataRow In _DtEff.Rows
                StrLastEffDate = R!FTEffectiveDate.ToString
                _CurrenAmt = Val(R!FNCurrentSlary.ToString)
                _NewAmt = Val(R!FNNewSlary.ToString)
                Exit For
            Next

            _Qry = " SELECT TOP 1 FNSeq ,FTEffectiveDate,FTCurrentSlary,FTNewSlary FROM THRTEmployeeUpdateSlary WITH (NOLOCK)  "
            _Qry &= "  WHERE   FNHSysEmpId=" & Val(FNHSysEmpID) & "  "
            _Qry &= " AND FTEffectiveDate='" & HI.UL.ULDate.ConvertEnDB(FTEffectiveDate.Text) & "'  "
            _Dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

            For Each R As DataRow In _Dt.Rows
                tSeqNo = Val(R!FNSeq.ToString)
                StrLastEffDate = R!FTEffectiveDate.ToString
                _CurrenAmt = Val(R!FTCurrentSlary.ToString)
                _NewAmt = Val(R!FTNewSlary.ToString)
                Exit For
            Next

            'If StrLastEffDate < HI.UL.ULDate.ConvertEnDB(FTEffectiveDate.Text) And StrLastEffDate <> "" Then
            '    Exit Sub
            'End If

            Select Case FNCngMinimumWage.SelectedIndex
                Case 0
                    _EmpNewSalary = FNNewSlary.Value
                Case 1
                    _EmpNewSalary = EmpSlary + FNNewSlary.Value
                Case 2
                    _EmpNewSalary = EmpSlary + CDbl(Format(((EmpSlary * FNNewSlary.Value) / 100), "0.00"))
                Case Else
                    _EmpNewSalary = EmpSlary
            End Select

            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_HR)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            Try

                _Qry = "UPDATE  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeUpdateSlary SET "
                _Qry &= vbCrLf & " FNHSysPmtReasoneId=" & Val(FNHSysPmtReasoneId.Properties.Tag.ToString) & " "
                _Qry &= vbCrLf & ",FTCurrentSlary=" & EmpSlary & " "
                _Qry &= vbCrLf & ",FTNewSlary=" & _EmpNewSalary & " "
                _Qry &= vbCrLf & ",FTEffectiveDate='" & HI.UL.ULDate.ConvertEnDB(FTEffectiveDate.Text) & "' "
                _Qry &= vbCrLf & ",FTNote=N'" & HI.UL.ULF.rpQuoted(FTRemark.Text) & "',FTType='1' "
                _Qry &= vbCrLf & ",FTUpdUser = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Qry &= vbCrLf & ",FTUpdDate = " & HI.UL.ULDate.FormatDateDB & ""
                _Qry &= vbCrLf & ",FTUpdTime = " & HI.UL.ULDate.FormatTimeDB & ""
                _Qry &= vbCrLf & " WHERE  FNHSysEmpID=" & Val(FNHSysEmpID) & " "
                _Qry &= vbCrLf & "  AND FNSeq=" & Val(tSeqNo)

                If HI.Conn.SQLConn.ExecuteTran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                    _Qry = "SELECT MAX(FNSeq) AS FNSeqNo FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeUpdateSlary WHERE  FNHSysEmpID=" & Val(FNHSysEmpID) & " "
                    tSeqNo = HI.Conn.SQLConn.GetFieldOnBeginTrans(_Qry, Conn.DB.DataBaseName.DB_HR, "0")
                    tSeqNo = Val(tSeqNo) + 1

                    _Qry = "INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeUpdateSlary(FNHSysEmpID, FNSeq, FNHSysPmtReasoneId, FTCurrentSlary, FTNewSlary, FTEffectiveDate, FTNote,FTType"
                    _Qry &= vbCrLf & ", FTInsUser, FTInsDate, FTInsTime)  "
                    _Qry &= vbCrLf & " SELECT " & Val(FNHSysEmpID) & "," & tSeqNo & ""
                    _Qry &= vbCrLf & "," & Val(FNHSysPmtReasoneId.Properties.Tag.ToString) & " "
                    _Qry &= vbCrLf & "," & EmpSlary & "," & _EmpNewSalary & ""
                    _Qry &= vbCrLf & ",'" & HI.UL.ULDate.ConvertEnDB(FTEffectiveDate.Text) & "' "
                    _Qry &= vbCrLf & ",N'" & HI.UL.ULF.rpQuoted(FTRemark.Text) & "','1' "
                    _Qry &= vbCrLf & ",N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                    _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB

                    If HI.Conn.SQLConn.ExecuteTran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        Exit Sub
                    End If

                End If

                
                _Qry = "UPDATE THRTEmployeeUpdateSlary SET FNSeq=FNNo"
                _Qry &= vbCrLf & " FROM THRTEmployeeUpdateSlary INNER JOIN "
                _Qry &= vbCrLf & "(SELECT ROW_NUMBER() OVER(ORDER BY FTEffectiveDate) AS FNNo, FNSeq,FNHSysEmpId"
                _Qry &= vbCrLf & " FROM THRTEmployeeUpdateSlary WHERE  FNHSysEmpId=" & Val(FNHSysEmpID) & " "
                _Qry &= vbCrLf & ") T1 ON THRTEmployeeUpdateSlary.FNSeq=T1.FNSeq AND THRTEmployeeUpdateSlary.FNHSysEmpId=T1.FNHSysEmpId"

                HI.Conn.SQLConn.ExecuteTran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

                HI.Conn.SQLConn.Tran.Commit()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                Exit Sub

            Catch ex As Exception
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                Exit Sub
            End Try

        Catch ex As Exception
            
            Exit Sub
        End Try

    End Sub

    Private Sub wUpdateMinimumWage_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Me.FNHSysCmpId.Text = HI.ST.SysInfo.CmpCode
    End Sub

    Private Sub ocmdelete_Click(sender As Object, e As EventArgs) Handles ocmdelete.Click

    End Sub
End Class
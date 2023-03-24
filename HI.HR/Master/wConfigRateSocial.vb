Public Class wConfigRateSocial 

#Region " Procedure "

    Private Function SaveData() As Boolean
        Try
            Dim _Qry As String


            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_HR)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            Try

                _Qry = " UPDATE  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMCmpSocialDiscountTaxConfig"
                _Qry &= vbCrLf & " SET FNSocialRate = " & FNSocialRate.Value & ""
                _Qry &= vbCrLf & " , FNSocialRateCmp= " & FNSocialRateCmp.Value & ""
                _Qry &= vbCrLf & " , FNSocialMin= " & FNSocialMin.Value & ""
                _Qry &= vbCrLf & " , FNSocialMax= " & FNSocialMax.Value & ""
                _Qry &= vbCrLf & " , FNContributedToTheFund= " & FNContributedToTheFund.Value & ""
                _Qry &= vbCrLf & " , FNContributedIncomeMax= " & FNContributedIncomeMax.Value & ""
                _Qry &= vbCrLf & " , FTContributedDeducIDNo= N'" & FTContributedDeducIDNo.Text & "'"
                _Qry &= vbCrLf & " , FTContributedDeducCmpCode= N'" & FTContributedDeducCmpCode.Text & "'"
                _Qry &= vbCrLf & " , FTContributedDeducBnkCode= N'" & FNHSysBankId.Properties.Tag.ToString & "'"
                _Qry &= vbCrLf & " , FTStateCmpPayOnly = " & IIf(FTStateCmpPayOnly.Checked, "1", "0") & ""
                _Qry &= vbCrLf & " , FNStateCmpFund= '" & FNStateCmpFund.SelectedIndex & "'"
                _Qry &= vbCrLf & "  WHERE FNHSysCmpId=" & Val(HI.ST.SysInfo.CmpID) & " "

                If HI.Conn.SQLConn.ExecuteTran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    Return False
                End If

                If Me.FNStartAge.Value > 0 Or Me.FNEndAge.Value > 0 Or Me.FNEmpPay.Value > 0 Then

                    _Qry = "UPDATE  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMContributions SET "
                    _Qry &= vbCrLf & " FNAgeBegin=" & Me.FNStartAge.Value
                    _Qry &= vbCrLf & ",FNAgeEnd=" & Me.FNEndAge.Value
                    _Qry &= vbCrLf & ",FNEmpPay=" & Me.FNEmpPay.Value
                    _Qry &= vbCrLf & ",FNCmpPay=" & Me.FNCmpPay.Value
                    _Qry &= vbCrLf & ",FTUpdUser = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Qry &= vbCrLf & ",FDUpdDate = " & HI.UL.ULDate.FormatDateDB & ""
                    _Qry &= vbCrLf & ",FTUpdTime = " & HI.UL.ULDate.FormatTimeDB & ""
                    _Qry &= vbCrLf & "  WHERE FNHSysCmpId=" & Val(HI.ST.SysInfo.CmpID) & " AND   FNSeqNo=" & Me.FNSeq.Value

                    If HI.Conn.SQLConn.ExecuteTran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        _Qry = "SELECT MAX(FNSeqNo) AS FNSeqNo FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMContributions  "

                        Dim tSeqNo As String = HI.Conn.SQLConn.GetFieldOnBeginTrans(_Qry, Conn.DB.DataBaseName.DB_HR, "0")
                        tSeqNo = Val(tSeqNo) + 1

                        _Qry = "INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMContributions(FNHSysCmpId,FNSeqNo,FNAgeBegin,FNAgeEnd,FNEmpPay,FNCmpPay"
                        _Qry &= vbCrLf & ", FTInsUser, FDInsDate, FTInsTime)  "
                        _Qry &= vbCrLf & " SELECT " & Val(HI.ST.SysInfo.CmpID) & "," & tSeqNo & ""
                        _Qry &= vbCrLf & "," & Me.FNStartAge.Value & "," & Me.FNEndAge.Value & "," & Me.FNEmpPay.Value & "," & Me.FNCmpPay.Value
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

                    _Qry = "UPDATE THRMContributions SET FNSeqNo=FNNo"
                    _Qry &= vbCrLf & " FROM THRMContributions INNER JOIN "
                    _Qry &= vbCrLf & "(SELECT ROW_NUMBER() OVER(ORDER BY FNAgeBegin,FNAgeEnd) AS FNNo, FNSeqNo"
                    _Qry &= vbCrLf & " FROM THRMContributions  "
                    _Qry &= vbCrLf & "  WHERE FNHSysCmpId=" & Val(HI.ST.SysInfo.CmpID) & ""
                    _Qry &= vbCrLf & ") T1 ON THRMContributions.FNSeqNo=T1.FNSeqNo "
                    _Qry &= vbCrLf & "  WHERE FNHSysCmpId=" & Val(HI.ST.SysInfo.CmpID) & ""
                    HI.Conn.SQLConn.ExecuteTran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

                    Me.FNStartAge.Value = 0
                    Me.FNEndAge.Value = 0
                    Me.FNEmpPay.Value = 0
                    Me.FNCmpPay.Value = 0
                    Me.FNSeq.Value = 0

                End If

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
        If Me.FNStartAge.Value > 0 Or Me.FNEndAge.Value > 0 Or Me.FNEmpPay.Value > 0 Or Me.FNCmpPay.Value > 0 Then


            Dim _Qry As String = ""
            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_HR)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            Try
                _Qry = " Delete  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMContributions  "
                _Qry &= vbCrLf & "  WHERE FNHSysCmpId=" & Val(HI.ST.SysInfo.CmpID) & " And FNSeqNo = " & Me.FNSeq.Value

                HI.Conn.SQLConn.ExecuteTran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)


                _Qry = "UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMContributions SET FNSeqNo=FNNo"
                _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMContributions INNER JOIN "
                _Qry &= vbCrLf & "(SELECT ROW_NUMBER() OVER(ORDER BY FNAgeBegin,FNAgeEnd) AS FNNo, FNSeqNo"
                _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMContributions  "
                _Qry &= vbCrLf & "  WHERE FNHSysCmpId=" & Val(HI.ST.SysInfo.CmpID) & ""
                _Qry &= vbCrLf & ") T1 ON THRMContributions.FNSeqNo=T1.FNSeqNo "
                _Qry &= vbCrLf & "  WHERE FNHSysCmpId=" & Val(HI.ST.SysInfo.CmpID) & ""

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

    Private Sub LoadDataInfo()
        Dim _Qry As String = ""
        Dim _Dt As DataTable

        _Qry = " SELECT  TOP 1 FNSocialRate,FNSocialRateCmp ,isnull(FTStateCmpPayOnly ,'0') as FTStateCmpPayOnly  "
        _Qry &= vbCrLf & " , FNSocialMin"
        _Qry &= vbCrLf & " , FNSocialMax"
        _Qry &= vbCrLf & " , FNContributedToTheFund"
        _Qry &= vbCrLf & " ,FNContributedIncomeMax"
        _Qry &= vbCrLf & " , FTContributedDeducIDNo"
        _Qry &= vbCrLf & " , FTContributedDeducCmpCode"
        _Qry &= vbCrLf & " , FTContributedDeducBnkCode, isnull(FNStateCmpFund,0) FNStateCmpFund "
        _Qry &= vbCrLf & "FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMCmpSocialDiscountTaxConfig AS A WITH(NOLOCK)"
        _Qry &= vbCrLf & "  WHERE FNHSysCmpId=" & Val(HI.ST.SysInfo.CmpID) & " "

        _Dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

        For Each R As DataRow In _Dt.Rows

            FNSocialRate.Value = Val(R!FNSocialRate.ToString)
            FNSocialRateCmp.Value = Val(R!FNSocialRateCmp.ToString)
            FNSocialMin.Value = Val(R!FNSocialMin.ToString)
            FNSocialMax.Value = Val(R!FNSocialMax.ToString)
            FNContributedToTheFund.Value = Val(R!FNContributedToTheFund.ToString)
            FNContributedIncomeMax.Value = Val(R!FNContributedIncomeMax.ToString)
            FTContributedDeducIDNo.Text = R!FTContributedDeducIDNo.ToString
            FTContributedDeducCmpCode.Text = R!FTContributedDeducCmpCode.ToString
            FTStateCmpPayOnly.Checked = R!FTStateCmpPayOnly.ToString = "1"
            FNStateCmpFund.SelectedIndex = R!FNStateCmpFund.ToString
            _Qry = "SELECT TOP 1 FTBankCode FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMBank WITH(NOLOCK) WHERE FNHSysBankId=" & Val(R!FTContributedDeducBnkCode.ToString) & " "
            FNHSysBankId.Text = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR)

        Next

    End Sub

    Private Sub LoadCon()
        Dim _Qry As String = ""
        Dim _Dt As DataTable

        _Qry = " SELECT     FNSeqNo, Convert(numeric(18,0),FNAgeBegin) AS FNAgeBegin, Convert(numeric(18,0),FNAgeEnd) AS FNAgeEnd, Convert(numeric(18,2),FNEmpPay) AS FNEmpPay, Convert(numeric(18,2),FNCmpPay) AS FNCmpPay "
        _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMContributions"
        _Qry &= vbCrLf & "  WHERE FNHSysCmpId=" & Val(HI.ST.SysInfo.CmpID) & ""
        _Qry &= vbCrLf & "  ORDER BY FNSeqNo  "
        _Dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

        ogcvacation.DataSource = _Dt
    End Sub
#End Region

#Region "MAIN PROC"

    Private Sub ProcessSave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmsave.Click
        If Me.SaveData() Then
            HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
            LoadCon()
        Else
            HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
        End If
    End Sub

    Private Sub ProcessDelete(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmdelete.Click
        If HI.MG.ShowMsg.mConfirmProcess(MG.ShowMsg.ProcessType.mSave, Me.Text) = True Then
            Dim _Spls As New HI.TL.SplashScreen("Deleting...   Please Wait   ")
            If Me.DeleteData() Then
                _Spls.Close()

                HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
                Me.LoadCon()
            Else
                _Spls.Close()
                HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
            End If
        End If
    End Sub

    Private Sub ProcessClose(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

#End Region

#Region "General"
    Private Sub ogvvacation_Click(sender As Object, e As System.EventArgs) Handles ogvvacation.Click
        With ogvvacation
            If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount Then Exit Sub

            Me.FNSeq.Value = "" & .GetRowCellValue(.FocusedRowHandle, "FNSeqNo").ToString
            Me.FNStartAge.Value = "" & .GetRowCellValue(.FocusedRowHandle, "FNAgeBegin").ToString
            Me.FNEndAge.Value = "" & .GetRowCellValue(.FocusedRowHandle, "FNAgeEnd").ToString
            Me.FNEmpPay.Value = "" & .GetRowCellValue(.FocusedRowHandle, "FNEmpPay").ToString
            Me.FNCmpPay.Value = "" & .GetRowCellValue(.FocusedRowHandle, "FNCmpPay").ToString

            Me.FNEmpPay.Focus()

        End With
    End Sub

    Private Sub wConfigRateSocial_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        Call LoadDataInfo()
        Call LoadCon()

    End Sub

    Private Sub FNCmpPay_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles FNCmpPay.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If Me.ocmsave.Enabled And Me.ocmsave.Visible Then
                ProcessSave(ocmsave, New System.EventArgs)
            End If
        End If
    End Sub




#End Region

End Class
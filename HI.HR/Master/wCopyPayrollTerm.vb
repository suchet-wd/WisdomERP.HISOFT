Public Class wCopyPayrollTerm 


#Region "Proc"
    Private Function ValidaateData() As Boolean
        Dim _Pass As Boolean = False

        If Me.FNYear.Text <> "" Then
            If Me.FNHSysEmpTypeId.Text <> "" Then

                If Me.FNYearTo.Text <> "" Then
                    If Me.FNHSysEmpTypeIdTo.Text <> "" Then
                        _Pass = True
                    Else
                        HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, Me.FNHSysEmpTypeIdTo_lbl.Text)
                        FNHSysEmpTypeIdTo.Focus()
                    End If
                Else
                    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, Me.FNYearTo_lbl.Text)
                    FNYearTo.Focus()
                End If

            Else

                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, Me.FNHSysEmpTypeId_lbl.Text)
                FNHSysEmpTypeId.Focus()

            End If
        Else

            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, Me.FNYear_lbl.Text)
            FNYear.Focus()

        End If


        Return _Pass
    End Function

    Private Function Copy_Proc() As Boolean
        Dim _Qry  As  String = ""

        HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_HR)
        HI.Conn.SQLConn.SqlConnectionOpen()
        HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
        HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

        Try

            _Qry = "DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMCfgPayDT "
            _Qry &= vbCrLf & " WHERE FTPayYear='" & Me.FNYearTo.Text & "' "
            _Qry &= vbCrLf & " AND  FNHSysEmpTypeId=" & Val(FNHSysEmpTypeIdTo.Properties.Tag.ToString) & " "

            HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)


            _Qry = "Insert Into  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMCfgPayDT(FTInsUser, FDInsDate, FTInsTime,FTPayTerm"
            _Qry &= vbCrLf & ", FTPayYear, FNHSysEmpTypeId, FNMonth, FTTermOfMonth, FDPayDate, FDCalDateBegin, FDCalDateEnd, FDDateClose, "
            _Qry &= vbCrLf & "   FTStateTermEndOfYear, FTStateSpecial, FNExchangeRate, FNExchangeRateTHB, FNWorkDay)"
            _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & " "
            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & " "
            _Qry &= vbCrLf & "  , FTPayTerm, '" & Me.FNYearTo.Text & "', " & Val(FNHSysEmpTypeIdTo.Properties.Tag.ToString) & ", FNMonth, FTTermOfMonth, FDPayDate, FDCalDateBegin, FDCalDateEnd, FDDateClose, "
            _Qry &= vbCrLf & "  FTStateTermEndOfYear, FTStateSpecial, FNExchangeRate, FNExchangeRateTHB, FNWorkDay"
            _Qry &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMCfgPayDT WITH(NOLOCK)"
            _Qry &= vbCrLf & " WHERE FTPayYear='" & Me.FNYear.Text & "' "
            _Qry &= vbCrLf & " AND  FNHSysEmpTypeId=" & Val(FNHSysEmpTypeId.Properties.Tag.ToString) & " "

            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                Return False
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
      

    End Function

#End Region
    Private Sub ocmexit_Click(sender As System.Object, e As System.EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ocmcopy_Click(sender As System.Object, e As System.EventArgs) Handles ocmcopy.Click
        If Me.ValidaateData() Then
            If HI.MG.ShowMsg.mConfirmProcess("คุณต้องการทำการ Copy งวดการจ่ายเงินใช่หรือไม่ ?", 1403100001) = True Then
                If Copy_Proc() Then
                    HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                    Me.Close()
                Else
                    HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                End If
            End If
            
        End If
    End Sub


End Class
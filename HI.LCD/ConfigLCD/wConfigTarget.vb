Public Class wConfigTarget 



    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub LoadData()
        Try
            Dim _Cmd As String = ""
            Dim _oDt As DataTable
            _Cmd = "SELECT      T.FNHSysUnitSectId,T.FNTargetPerHour, T.FNTarget, T.FNPercent"
            _Cmd &= vbCrLf & "	, S.FTUnitSectCode,T.FTWorkTime"
            _Cmd &= vbCrLf & "	,ISNULL(T.FNMoneyPackage,0) AS FNMoneyPackage"
            _Cmd &= vbCrLf & "	,ISNULL(T.FNPercentPackage,0) AS FNPercentPackage"
            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Cmd &= vbCrLf & ", S.FTUnitSectNameTH AS FTUnitSectName "
            Else
                _Cmd &= vbCrLf & ", S.FTUnitSectNameEN AS FTUnitSectName "
            End If

            _Cmd &= vbCrLf & "	,(T.FNTarget * T.FNPercent) / 100 AS FNTargetPlane"
            _Cmd &= vbCrLf & ",Case when ISDATE(T.FDSDate)= 1 Then CONVERT(nvarchar(10),Convert(datetime,T.FDSDate),103) Else '' End  AS FDSDate"
            _Cmd &= vbCrLf & "	,Case when ISDATE(T.FDEDate)= 1 Then CONVERT(nvarchar(10),Convert(datetime,T.FDEDate),103) Else '' End  AS FDEDate"
            _Cmd &= vbCrLf & "	FROM         [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODLCDConfigTarget AS T LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "	                 [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS S ON T.FNHSysUnitSectId = S.FNHSysUnitSectId"
            _Cmd &= vbCrLf & " WHERE T.FNHSysUnitSectId <> 0"
            _Cmd &= vbCrLf & " ORDER BY T.FDSDate DESC,S.FTUnitSectCode ASC "
            'If Me.FNHSysUnitSectId.Text <> "" Then
            '    _Cmd &= vbCrLf & " And T.FNHSysUnitSectId =" & CInt("0" & Me.FNHSysUnitSectId.Properties.Tag)
            'End If
            'If Me.FDSDate.Text <> "" Then
            '    _Cmd &= vbCrLf & "  and (T.FDSDate >= '" & HI.UL.ULDate.ConvertEnDB(Me.FDSDate.Text) & "'"
            '    _Cmd &= vbCrLf & "or T.FDEDate >= '" & HI.UL.ULDate.ConvertEnDB(Me.FDSDate.Text) & "')"
            'End If
            'If Me.FDEDate.Text <> "" Then
            '    _Cmd &= vbCrLf & "and (T.FDSDate <= '" & HI.UL.ULDate.ConvertEnDB(Me.FDEDate.Text) & "' "
            '    _Cmd &= vbCrLf & "or T.FDEDate <= '" & HI.UL.ULDate.ConvertEnDB(Me.FDEDate.Text) & "')"
            'End If
            
            _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD)
            Me.ogcDetail.DataSource = _oDt
        Catch ex As Exception
        End Try
    End Sub

    Private Function SaveData() As Boolean
        Try
            Dim _Cmd As String = ""
            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PROD)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            _Cmd = "Update  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODLCDConfigTarget "
            _Cmd &= vbCrLf & "Set FNTarget =" & Me.FNTarget.Value
            _Cmd &= vbCrLf & ",FNTargetPerHour = " & Me.FNTargetPerHour.Value
            _Cmd &= vbCrLf & ",FNPercent = " & Me.FNPercent.Value
            _Cmd &= vbCrLf & ",FTWorkTime='" & FTWorkTime.Text & "'"
            _Cmd &= vbCrLf & ",FTUpdUser = '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            _Cmd &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB
            _Cmd &= vbCrLf & ",FTUpdTime =" & HI.UL.ULDate.FormatTimeDB
            _Cmd &= vbCrLf & ",FNMoneyPackage = " & FNMoneyPackage.Value
            _Cmd &= vbCrLf & ",FNPercentPackage = " & FNPercentPackage.Value
            _Cmd &= vbCrLf & "Where FNHSysUnitSectId=" & Integer.Parse(Val(Me.FNHSysUnitSectId.Properties.Tag))
            _Cmd &= vbCrLf & "AND FDSDate='" & HI.UL.ULDate.ConvertEnDB(Me.FDSDate.Text) & "'"
            _Cmd &= vbCrLf & "AND FDEDate='" & HI.UL.ULDate.ConvertEnDB(Me.FDEDate.Text) & "'"

            If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                _Cmd = "Select top 1 FNHSysUnitSectId From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODLCDConfigTarget"
                _Cmd &= vbCrLf & "Where FNHSysUnitSectId=" & CInt("0" & Me.FNHSysUnitSectId.Properties.Tag)
                '_Cmd &= vbCrLf & " AND (('" & HI.UL.ULDate.ConvertEnDB(Me.FDSDate.Text) & "' >= FDSDate and '" & HI.UL.ULDate.ConvertEnDB(Me.FDSDate.Text) & "' <=FDEDate)"
                '_Cmd &= vbCrLf & "OR ('" & HI.UL.ULDate.ConvertEnDB(Me.FDEDate.Text) & "' <= FDSDate and '" & HI.UL.ULDate.ConvertEnDB(Me.FDEDate.Text) & "' <=FDEDate) )"
                _Cmd &= vbCrLf & "  and (FDSDate >= '" & HI.UL.ULDate.ConvertEnDB(Me.FDSDate.Text) & "'"
                _Cmd &= vbCrLf & "or FDEDate >= '" & HI.UL.ULDate.ConvertEnDB(Me.FDSDate.Text) & "')"
                _Cmd &= vbCrLf & "and (FDSDate <= '" & HI.UL.ULDate.ConvertEnDB(Me.FDEDate.Text) & "' "
                _Cmd &= vbCrLf & "or FDEDate <= '" & HI.UL.ULDate.ConvertEnDB(Me.FDEDate.Text) & "')"

                If HI.Conn.SQLConn.GetDataTableOnbeginTrans(_Cmd).Rows.Count <= 0 Then

                    _Cmd = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODLCDConfigTarget"
                    _Cmd &= "(FTInsUser, FDInsDate, FTInsTime, FDSDate, FDEDate, FNHSysUnitSectId, FNTarget, FNPercent,FTWorkTime,FNTargetPerHour,FNMoneyPackage,FNPercentPackage)"
                    _Cmd &= vbCrLf & "Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                    _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                    _Cmd &= vbCrLf & ",'" & HI.UL.ULDate.ConvertEnDB(Me.FDSDate.Text) & "'"
                    _Cmd &= vbCrLf & ",'" & HI.UL.ULDate.ConvertEnDB(Me.FDEDate.Text) & "'"
                    _Cmd &= vbCrLf & "," & Integer.Parse(Val(Me.FNHSysUnitSectId.Properties.Tag))
                    _Cmd &= vbCrLf & "," & Me.FNTarget.Value
                    _Cmd &= vbCrLf & "," & Me.FNPercent.Value
                    _Cmd &= vbCrLf & ",'" & FTWorkTime.Text & "'"
                    _Cmd &= vbCrLf & "," & Me.FNTargetPerHour.Value
                    _Cmd &= vbCrLf & "," & Me.FNMoneyPackage.Value
                    _Cmd &= vbCrLf & "," & Me.FNPercentPackage.Value

                    If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        Return False
                    End If

                Else
                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    Return False
                End If

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

    Private Function VerifyData() As Boolean
        Try
            If Me.FNHSysUnitSectId.Text = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.FNHSysUnitSectId_lbl.Text)
                Me.FNHSysUnitSectId.Focus()
                Return False
            End If

            If Me.FDSDate.Text = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.FDSDate_lbl.Text)
                Me.FDSDate.Focus()
                Return False
            End If

            If Me.FDEDate.Text = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.FDEDate_lbl.Text)
                Me.FDEDate.Focus()
                Return False
            End If

            If FTWorkTime.Text = "00:00" Or FTWorkTime.Text = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.FTWorkTime_lbl.Text)
                Me.FDEDate.Focus()
                Return False
            End If

            'If Me.FNTarget.Value <= 0 Then
            '    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.FNTarget_lbl.Text)
            '    Me.FNTarget.Focus()
            '    Return False
            'End If

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub ocmsave_Click(sender As Object, e As EventArgs) Handles ocmsave.Click
        Try
            If Me.VerifyData Then
                If Me.SaveData Then
                    HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                    Me.LoadData()
                    HI.TL.HandlerControl.ClearControl(Me)
                    Me.DefaultPercent()
                Else
                    HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Function DeleteData() As Boolean
        Try
            Dim _Cmd As String = ""
            _Cmd = "Delete From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODLCDConfigTarget "
            _Cmd &= vbCrLf & "Where FNHSysUnitSectId=" & Integer.Parse(Val(Me.FNHSysUnitSectId.Properties.Tag))
            _Cmd &= vbCrLf & "AND FDSDate='" & HI.UL.ULDate.ConvertEnDB(Me.FDSDate.Text) & "'"
            _Cmd &= vbCrLf & "AND FDEDate='" & HI.UL.ULDate.ConvertEnDB(Me.FDEDate.Text) & "'"

            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction
            If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                Return False
            End If

            HI.Auditor.CreateLog.CreateLogDelete(HI.ST.SysInfo.MenuName, Me.Name, _Cmd & " User Delete =" & HI.ST.UserInfo.UserName)

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

    Private Sub ocmdelete_Click(sender As Object, e As EventArgs) Handles ocmdelete.Click
        Try
            If Me.VerifyData() Then
                If Me.DeleteData Then
                    HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
                    Me.LoadData()
                    HI.TL.HandlerControl.ClearControl(Me)
                    Me.DefaultPercent()
                Else
                    HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmload_Click(sender As Object, e As EventArgs) Handles ocmload.Click
        Try
            Me.LoadData()
        Catch ex As Exception
        End Try
    End Sub

 
    Private Sub ogvDetail_DoubleClick(sender As Object, e As EventArgs) Handles ogvDetail.DoubleClick, ogvDetail.Click
        Try
            With ogvDetail
                Me.FDSDate.Text = .GetRowCellValue(.FocusedRowHandle, "FDSDate").ToString
                Me.FDEDate.Text = .GetRowCellValue(.FocusedRowHandle, "FDEDate").ToString
                Me.FNHSysUnitSectId.Text = .GetRowCellValue(.FocusedRowHandle, "FTUnitSectCode").ToString
                Me.FNTargetPerHour.Value = CInt("0" & .GetRowCellValue(.FocusedRowHandle, "FNTargetPerHour").ToString)

                If "" & .GetRowCellValue(.FocusedRowHandle, "FTWorkTime").ToString = "" Then
                    Me.FTWorkTime.EditValue = "00:00"
                Else
                    Me.FTWorkTime.EditValue = "" & .GetRowCellValue(.FocusedRowHandle, "FTWorkTime").ToString
                End If

                Me.FNTarget.Value = CInt("0" & .GetRowCellValue(.FocusedRowHandle, "FNTarget").ToString)
                Me.FNPercent.Value = CDbl("0" & .GetRowCellValue(.FocusedRowHandle, "FNPercent").ToString)
                Me.FNPercentPackage.Value = CDbl("0" & .GetRowCellValue(.FocusedRowHandle, "FNPercentPackage").ToString)
                Me.FNMoneyPackage.Value = CDbl("0" & .GetRowCellValue(.FocusedRowHandle, "FNMoneyPackage").ToString)
              

            End With
        Catch ex As Exception

        End Try

    End Sub

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub wConfigTarget_Load(sender As Object, e As EventArgs) Handles Me.Load

        Try
            Me.DefaultPercent()
            Me.LoadData()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub DefaultPercent()
        Try
            Dim _Cmd As String = ""
            _Cmd = "Select top 1 FTCfgData From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSESystemConfig WITH(NOLOCK) Where FTCfgName ='CfgProd_Lcd' "
            Me.FNPercent.Value = HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_PROD)

            FTWorkTime.EditValue = "00:00"
            FNPercentPackage.Value = 0
            FNMoneyPackage.Value = 0
        Catch ex As Exception
        End Try
    End Sub


    Private Sub FNTargetPerHour_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles FNTargetPerHour.EditValueChanging
        Try
            Dim _TPH As Integer = 0
            Dim _TimePDay As Integer = 0
            Try
                _TPH = Integer.Parse(e.NewValue.ToString())
            Catch ex As Exception
            End Try

            Try
                _TimePDay = ((Val(FTWorkTime.Text.Trim.Split(":")(0)) * _TPH)) + Integer.Parse(((Val(FTWorkTime.Text.Trim.Split(":")(1)) * (_TPH / 60.0))))
            Catch ex As Exception
            End Try

            FNTarget.Value = _TimePDay

        Catch ex As Exception

        End Try
    End Sub

    Private Sub FTWorkTime_EditValueChanged(sender As Object, e As EventArgs) Handles FTWorkTime.EditValueChanged
        Try
            Dim _TPH As Integer = 0
            Dim _TimePDay As Integer = 0
            Try
                _TPH = Integer.Parse(FNTargetPerHour.Value)
            Catch ex As Exception
            End Try

            Try
                _TimePDay = ((Val(FTWorkTime.Text.Trim.Split(":")(0)) * _TPH)) + Integer.Parse(((Val(FTWorkTime.Text.Trim.Split(":")(1)) * (_TPH / 60.0))))
            Catch ex As Exception
            End Try

            FNTarget.Value = _TimePDay

        Catch ex As Exception

        End Try
    End Sub

    Private Sub ocmclear_Click(sender As Object, e As EventArgs) Handles ocmclear.Click

    End Sub
End Class
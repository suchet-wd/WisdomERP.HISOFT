Public Class wConfigCutTarget



    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub LoadData()
        Try
            Dim _Cmd As String = ""
            Dim _oDt As DataTable
            _Cmd = "SELECT      T.FNHSysUnitSectId, T.FNTarget,T.FTJobNo AS FTOrderNo,T.FTColorway"


            _Cmd &= vbCrLf & ",Case when ISDATE(T.FDDate)= 1 Then CONVERT(nvarchar(10),Convert(datetime,T.FDDate),103) Else '' End  AS FDDate"

            _Cmd &= vbCrLf & "	FROM         [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODCuttingLCDConfigTarget AS T LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "	                 [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS S ON T.FNHSysUnitSectId = S.FNHSysUnitSectId"
            _Cmd &= vbCrLf & " WHERE T.FNHSysCmpId = " & Val(FNHSysCmpId.Properties.Tag.ToString) & " AND T.FDDate >=convert(varchar(10),Dateadd(day,-30,getdate()),111)"
            _Cmd &= vbCrLf & " ORDER BY T.FDDate DESC,S.FTUnitSectCode ASC "


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

            _Cmd = "Update  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODCuttingLCDConfigTarget "
            _Cmd &= vbCrLf & "Set FNTarget =" & Me.FNTarget.Value

            _Cmd &= vbCrLf & ",FTUpdUser = '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            _Cmd &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB
            _Cmd &= vbCrLf & ",FTUpdTime =" & HI.UL.ULDate.FormatTimeDB

            _Cmd &= vbCrLf & "Where FNHSysCmpId=" & HI.ST.SysInfo.CmpID & ""
            _Cmd &= vbCrLf & "AND FDDate='" & HI.UL.ULDate.ConvertEnDB(Me.FDSDate.Text) & "'"
            _Cmd &= vbCrLf & "AND FTJobNo='" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "'"
            _Cmd &= vbCrLf & "AND FTColorway='" & HI.UL.ULF.rpQuoted(Me.FTColorway.Text) & "'"

            If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                _Cmd = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODCuttingLCDConfigTarget"
                _Cmd &= "(FTInsUser, FDInsDate, FTInsTime,  FNHSysCmpId,FDDate, FNHSysUnitSectId, FTJobNo,FTColorway,FNTarget)"
                _Cmd &= vbCrLf & "Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                _Cmd &= vbCrLf & "," & HI.ST.SysInfo.CmpID & ""
                _Cmd &= vbCrLf & ",'" & HI.UL.ULDate.ConvertEnDB(Me.FDSDate.Text) & "'"

                _Cmd &= vbCrLf & ",0 "
                _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "'"
                _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTColorway.Text) & "'"
                _Cmd &= vbCrLf & "," & Me.FNTarget.Value

                If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
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

            If Me.FNHSysCmpId.Text = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.FNHSysCmpId_lbl.Text)
                Me.FNHSysCmpId.Focus()
                Return False
            End If


            If Me.FTOrderNo.Text = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.FTOrderNo_lbl.Text)
                Me.FTOrderNo.Focus()
                Return False
            End If

            If Me.FDSDate.Text = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.FDSDate_lbl.Text)
                Me.FDSDate.Focus()
                Return False
            End If

            If Me.FTColorway.Text = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.FTColorway_lbl.Text)
                Me.FTColorway.Focus()
                Return False
            End If


            If Me.FNTarget.Value <= 0 Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.FNTarget_lbl.Text)
                Me.FNTarget.Focus()
                Return False
            End If

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
                    '  HI.TL.HandlerControl.ClearControl(Me)
                    Me.FTColorway.Text = ""
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
            _Cmd &= vbCrLf & "Where FNHSysCmpId=" & HI.ST.SysInfo.CmpID & ""
            _Cmd &= vbCrLf & "AND FDSDate='" & HI.UL.ULDate.ConvertEnDB(Me.FDSDate.Text) & "'"
            _Cmd &= vbCrLf & "AND FTJobNo='" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "'"
            _Cmd &= vbCrLf & "AND FTColorway='" & HI.UL.ULF.rpQuoted(Me.FTColorway.Text) & "'"

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
                    'Me.FTOrderNo.Text = ""
                    Me.FTColorway.Text = ""
                    '  HI.TL.HandlerControl.ClearControl(Me)

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
                Me.FDSDate.Text = .GetRowCellValue(.FocusedRowHandle, "FDDate").ToString

                FTOrderNo.Text = .GetRowCellValue(.FocusedRowHandle, "FTOrderNo").ToString
                FTColorway.Text = .GetRowCellValue(.FocusedRowHandle, "FTColorway").ToString

                Me.FNTarget.Value = CInt("0" & .GetRowCellValue(.FocusedRowHandle, "FNTarget").ToString)


            End With
        Catch ex As Exception

        End Try

    End Sub

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub wConfigTarget_Load(sender As Object, e As EventArgs) Handles Me.Load

        Try

            Me.FNHSysCmpId.Text = HI.ST.SysInfo.CmpCode


            Me.LoadData()
        Catch ex As Exception
        End Try
    End Sub




    Private Sub ocmclear_Click(sender As Object, e As EventArgs) Handles ocmclear.Click

        Me.FNHSysCmpId.Text = HI.ST.SysInfo.CmpCode
    End Sub
End Class
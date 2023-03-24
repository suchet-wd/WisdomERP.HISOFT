Public Class wConfigRateSeniorityInd

    Sub New()
        _ProcPrepare = True
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _ActualDate = HI.Conn.SQLConn.GetField("SELECT  CONVERT(varchar(10),GETDATE(),111)", Conn.DB.DataBaseName.DB_HR, "")
        _ActualNextDate = HI.Conn.SQLConn.GetField("SELECT  CONVERT(varchar(10),DateAdd(day,1,GETDATE()),111)", Conn.DB.DataBaseName.DB_HR, "")
        _ProcPrepare = False
    End Sub

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

    Private Sub BindingGrid(ByVal CmpCode As String, ByVal EmpType As String)

    End Sub

#Region "MAIN PROC"

    Private Sub ProcessSave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmsave.Click

        If Me.DataValidate Then
            Dim _Spls As New HI.TL.SplashScreen("Saving...   Please Wait   ")
            If Me.SaveData() Then
                _Spls.Close()
                'HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                Call ClsEdit()
                Me.ProcLoad()

            Else
                _Spls.Close()
                HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
            End If
        End If
    End Sub

    Private Sub ProcessDelete(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmdelete.Click
        If Me.DataValidate Then

            If HI.MG.ShowMsg.mConfirmProcess(MG.ShowMsg.ProcessType.mSave, Me.Text) = True Then
                Dim _Spls As New HI.TL.SplashScreen("Deleting...   Please Wait   ")
                If Me.DeleteData() Then
                    _Spls.Close()

                    HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
                    Call ClsEdit()
                    Me.ProcLoad()
                Else
                    _Spls.Close()
                    HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
                End If
            End If
        End If
    End Sub

    Private Sub ProcessClear(ByVal sender As System.Object, ByVal e As System.EventArgs)

        HI.TL.HandlerControl.ClearControl(Me)

    End Sub

    Private Sub ProcessPreview(ByVal sender As System.Object, ByVal e As System.EventArgs)
    End Sub

    Private Sub ProcessClose(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

#End Region

#Region " Procedure "

    Private Function SaveData() As Boolean
        Try
            Dim _Qry As String
            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_HR)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            Try


                _Qry = "UPDATE  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMCfgSeniorityIndemnty SET "
                _Qry &= vbCrLf & " FTUpdUser = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Qry &= vbCrLf & ",FDUpdDate = " & HI.UL.ULDate.FormatDateDB & ""
                _Qry &= vbCrLf & ",FTUpdTime = " & HI.UL.ULDate.FormatTimeDB & ""
                _Qry &= vbCrLf & ",FTPaybackYear='" & Me.FTPaybackYear.Text & "'"
                _Qry &= vbCrLf & ",FNSeniorityAmt=" & Me.FNSeniorityAmt.Value
                _Qry &= vbCrLf & ",FNSalaryPerDay=0" ' & Me.FNSalaryPerDay.Value
                _Qry &= vbCrLf & ",FTStateActive='" & Me.FTStateActive.EditValue.ToString & "'"
                _Qry &= vbCrLf & " WHERE  FNHSysEmpTypeId=" & Val(FNHSysEmpTypeId.Properties.Tag.ToString) & " "
                _Qry &= vbCrLf & "and FTPayTerm='" & Me.FTPayTerm.Text & "'"
                _Qry &= vbCrLf & "and FTStatePayback=" & Me.FTStatePayback.SelectedIndex
                If HI.Conn.SQLConn.ExecuteTran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                    _Qry = "INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMCfgSeniorityIndemnty(  FNHSysEmpTypeId, FTPayTerm, FTStatePayback, FTPaybackYear, FNSeniorityAmt, FNSalaryPerDay, FTStateActive"
                    _Qry &= vbCrLf & ", FTInsUser, FDInsDate, FTInsTime)  "
                    _Qry &= vbCrLf & " SELECT " & Val(FNHSysEmpTypeId.Properties.Tag.ToString) & ",'" & Me.FTPayTerm.Text & "'"
                    _Qry &= vbCrLf & "," & Me.FTStatePayback.SelectedIndex
                    _Qry &= vbCrLf & ",'" & Me.FTPaybackYear.Text & "'"

                    _Qry &= vbCrLf & "," & Me.FNSeniorityAmt.Value & ",0,'" & FTStateActive.EditValue.ToString & "' "
                    _Qry &= vbCrLf & ",N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                    _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                    If HI.Conn.SQLConn.ExecuteTran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
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
        Catch ex As Exception

            Return False
        End Try
    End Function

    Private Function DeleteData() As Boolean
        If Me.FNSeniorityAmt.Value > 0 Then


            Dim _Qry As String = ""
            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_HR)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction


            Try
                _Qry = " Delete  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMCfgSeniorityIndemnty  "
                _Qry &= vbCrLf & " WHERE  FNHSysEmpTypeId=" & Val(FNHSysEmpTypeId.Properties.Tag.ToString) & " "
                _Qry &= vbCrLf & "  AND FTPayTerm='" & Me.FTPayTerm.Text & "'"
                HI.Conn.SQLConn.ExecuteTran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)


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
        Else
            Return False
        End If

    End Function



#End Region
    Private Sub ClsEdit()
        Try
            Me.FTPayTerm.Text = ""
            'Me.FNSalaryPerDay.Value = 0
            Me.FNSeniorityAmt.Value = 0

        Catch ex As Exception

        End Try
    End Sub
    Private Sub ProcLoad()

        Dim _Qry As String = ""
        Dim _reset As String = ""

        Call BindingGrid(Me.FNHSysEmpTypeId.Properties.Tag.ToString)
    End Sub

    Private Sub BindingGrid(ByVal EmpType As String)
        Try
            Dim _Cmd As String = ""
            _Cmd = "Select A.FNHSysEmpTypeId, A.FTPayTerm,Z.FTName as  FTStatePayback, A.FTPaybackYear, A.FNSeniorityAmt, A.FNSalaryPerDay, A.FTStateActive"
            _Cmd &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMCfgSeniorityIndemnty AS A WITH(NOLOCK) "
            _Cmd &= vbCrLf & "LEFT OUTER JOIN "
            _Cmd &= vbCrLf & "( SELECT FNListIndex,  FTNameTH, FTNameEN "
            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Cmd &= vbCrLf & "   , FTNameTH as FTName "
            Else
                _Cmd &= vbCrLf & "   , FTNameEN as FTName  "
            End If
            _Cmd &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData   WITH(NOLOCK) "
            _Cmd &= vbCrLf & "WHERE  (FTListName = 'FTStatePayback')"

            _Cmd &= vbCrLf & " ) AS Z ON A.FTStatePayback = Z.FNListIndex "
            _Cmd &= vbCrLf & " where A.FNHSysEmpTypeId=" & Integer.Parse(Val(EmpType))
            _Cmd &= vbCrLf & " OrDER BY A.FTPaybackYear , A.FTPayTerm "

            Me.ogddetail.DataSource = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_HR)

        Catch ex As Exception

        End Try


    End Sub



    Private Sub ogvvacation_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ogvdetail.Click
        With ogvdetail
            If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount Then Exit Sub
            Me.FTStateActive.Checked = ("" & .GetRowCellValue(.FocusedRowHandle, "FTStateActive").ToString = "1")
            Me.FTPaybackYear.EditValue = "" & .GetRowCellValue(.FocusedRowHandle, "FTPaybackYear").ToString
            'Me.FTStatePayback.SelectedIndex = HI.TL.CboList.GetIndexByText("FTStatePayback", "" & .GetRowCellValue(.FocusedRowHandle, "FTStatePayback").ToString)
            Me.FNSeniorityAmt.Value = Double.Parse("0" & .GetRowCellValue(.FocusedRowHandle, "FNSeniorityAmt").ToString)
            Me.FTPayTerm.Text = "" & .GetRowCellValue(.FocusedRowHandle, "FTPayTerm").ToString

        End With
    End Sub

    Private Sub FNHSysEmpTypeId_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles FNHSysEmpTypeId.EditValueChanged
        If (_ProcPrepare) Then Exit Sub

        If Me.InvokeRequired Then
            Me.Invoke(New HI.Delegate.Dele.FNHSysEmpID_EditValueChanged(AddressOf FNHSysEmpTypeId_EditValueChanged), New Object() {sender, e})
        Else
            If FNHSysEmpTypeId.Text <> "" Then
                Dim _Qry As String

                _Qry = "SELECT TOP 1  FNHSysEmpTypeId FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType WITH(NOLOCK) WHERE FTEmpTypeCode='" & HI.UL.ULF.rpQuoted(FNHSysEmpTypeId.Text) & "'   AND FNHSysCmpID =" & HI.ST.SysInfo.CmpID
                FNHSysEmpTypeId.Properties.Tag = Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MASTER, "0"))

                Call ProcLoad()
            End If
        End If

    End Sub



    Private Sub FNRight_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If Me.ocmsave.Enabled And Me.ocmsave.Visible Then
                ProcessSave(ocmsave, New System.EventArgs)
                FNSeniorityAmt.Focus()
                FNSeniorityAmt.SelectAll()
            End If
        End If
    End Sub

    Private Sub FTStatePayback_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            'Me.FTPaybackYear.Properties.Buttons(0).Visible = (FTStatePayback.SelectedIndex = 1)
            'Me.FTPaybackYear.ReadOnly = Not (FTStatePayback.SelectedIndex = 1)
        Catch ex As Exception

        End Try
    End Sub


    Private Function DataValidate() As Boolean
        Try
            If Me.FNHSysEmpTypeId.Text = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.FNHSysEmpTypeId_lbl.Text)
                Me.FNHSysEmpTypeId.Focus()
                Return False
            End If

            If Me.FTPayTerm.Text = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.FTPayTerm_lbl.Text)
                Me.FTPayTerm.Focus()
                Return False
            End If

            If Me.FNSeniorityAmt.Value <= 0 Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.FNSeniorityAmt_lbl.Text)
                Me.FNSeniorityAmt.Focus()
                Return False
            End If

            'If Me.FTStatePayback.SelectedIndex = 1 Then
            '    If Me.FTPaybackYear.Text = "" Then
            '        HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.FTPaybackYear_lbl.Text)
            '        Me.FTPaybackYear.Focus()
            '        Return False
            '    End If

            'End If

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function


End Class
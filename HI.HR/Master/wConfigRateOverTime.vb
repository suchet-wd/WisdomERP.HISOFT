Public Class wConfigRateOverTime

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

        If Me.VerrifyData Then
            Dim _Spls As New HI.TL.SplashScreen("Saving...   Please Wait   ")
            If Me.SaveData() Then
                _Spls.Close()
                HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                Me.ProcLoad()
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
                    FNSeq.Value = 0

                    HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
                    Me.ProcLoad()
                Else
                    _Spls.Close()
                    HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
                End If
            End If
        End If
    End Sub

    Private Sub ProcessClear(ByVal sender As System.Object, ByVal e As System.EventArgs)
  
        Me.ogdForm.DataSource = Nothing
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
            CType(ogdForm.DataSource, DataTable).AcceptChanges()
            Dim oDBdt As DataTable = CType(ogdForm.DataSource, DataTable)

            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_HR)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            Try

                For Each R As DataRow In oDBdt.Rows

                    _Qry = " UPDATE  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMConfigOTSet SET "
                    _Qry &= vbCrLf & " FCCfgOTValue=" & CDbl(R!FCCfgOTValue.ToString)
                    _Qry &= vbCrLf & " ,FCCfgOTNightValue=" & CDbl(R!FCCfgOTNightValue.ToString)
                    _Qry &= vbCrLf & ",FCCfgOTAmtPlus=" & CDbl(R!FCCfgOTAmtPlus.ToString)
                    _Qry &= vbCrLf & ",FTUpdUser = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Qry &= vbCrLf & ",FDUpdDate = " & HI.UL.ULDate.FormatDateDB & ""
                    _Qry &= vbCrLf & ",FTUpdTime = " & HI.UL.ULDate.FormatTimeDB & ""
                    _Qry &= vbCrLf & " WHERE  FNCalType='" & Val(FNHSysEmpTypeId.Properties.Tag.ToString) & "'"
                    _Qry &= vbCrLf & "    AND FTCfgOTCode='" & R!FTCfgOTCode.ToString & "'"

                    If HI.Conn.SQLConn.ExecuteTran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                        _Qry = "INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMConfigOTSet(FNCalType,FTCfgOTCode,FCCfgOTValue,FCCfgOTNightValue,FCCfgOTAmtPlus"
                        _Qry &= vbCrLf & ", FTInsUser, FDInsDate, FTInsTime)  "
                        _Qry &= vbCrLf & " SELECT " & Val(FNHSysEmpTypeId.Properties.Tag.ToString) & ",'" & R!FTCfgOTCode.ToString & "'"
                        _Qry &= vbCrLf & "," & CDbl(R!FCCfgOTValue.ToString) & "," & CDbl(R!FCCfgOTNightValue.ToString) & "," & CDbl(R!FCCfgOTAmtPlus.ToString) & " "
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
                CType(ogdForm1.DataSource, DataTable).AcceptChanges()

                oDBdt = CType(ogdForm1.DataSource, DataTable)

                For Each R As DataRow In oDBdt.Rows

                    _Qry = " UPDATE  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMConfigReturnVacationSet SET "
                    _Qry &= vbCrLf & " FCCfgRetValue=" & CDbl(R!FCCfgRetValue.ToString)
                    _Qry &= vbCrLf & ",FTUpdUser = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Qry &= vbCrLf & ",FDUpdDate = " & HI.UL.ULDate.FormatDateDB & ""
                    _Qry &= vbCrLf & ",FTUpdTime = " & HI.UL.ULDate.FormatTimeDB & ""
                    _Qry &= vbCrLf & ",FTCfgRetTerm='" & HI.UL.ULF.rpQuoted(R!FTCfgRetTerm.ToString) & "' "
                    _Qry &= vbCrLf & " WHERE  FNCalType='" & Val(FNHSysEmpTypeId.Properties.Tag.ToString) & "'"
                    _Qry &= vbCrLf & "    AND FTCfgRetCode='" & R!FTCfgRetCode.ToString & "'"

                    If HI.Conn.SQLConn.ExecuteTran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                        _Qry = "INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMConfigReturnVacationSet(FNCalType,FTCfgRetCode,FCCfgRetValue"
                        _Qry &= vbCrLf & ", FTInsUser, FDInsDate, FTInsTime,FTCfgRetTerm)  "
                        _Qry &= vbCrLf & " SELECT " & Val(FNHSysEmpTypeId.Properties.Tag.ToString) & ",'" & R!FTCfgRetCode.ToString & "'"
                        _Qry &= vbCrLf & "," & CDbl(R!FCCfgRetValue.ToString) & " "
                        _Qry &= vbCrLf & ",N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                        _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                        _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTCfgRetTerm.ToString) & "' "

                        If HI.Conn.SQLConn.ExecuteTran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                            HI.Conn.SQLConn.Tran.Rollback()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                            Return False
                        End If

                    End If
                Next

                If Me.FNStartAge.Value > 0 Or Me.FNEndAge.Value > 0 Or Me.FNRight.Value > 0 Then

                    _Qry = "UPDATE  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMConfigOTRound SET "
                    _Qry &= vbCrLf & " FTCfgOTBegin=" & Me.FNStartAge.Value
                    _Qry &= vbCrLf & ",FTCfgOTEnd=" & Me.FNEndAge.Value
                    _Qry &= vbCrLf & ",FTCfgOTSet=" & Me.FNRight.Value
                    _Qry &= vbCrLf & ",FTStatePay='" & FTStatePay.EditValue.ToString & "' "
                    _Qry &= vbCrLf & ",FTUpdUser = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Qry &= vbCrLf & ",FDUpdDate = " & HI.UL.ULDate.FormatDateDB & ""
                    _Qry &= vbCrLf & ",FTUpdTime = " & HI.UL.ULDate.FormatTimeDB & ""
                    _Qry &= vbCrLf & " WHERE  FNHSysEmpTypeId=" & Val(FNHSysEmpTypeId.Properties.Tag.ToString) & " "
                    _Qry &= vbCrLf & "  AND FTCfgOTSeqNo=" & Me.FNSeq.Value

                    If HI.Conn.SQLConn.ExecuteTran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        _Qry = "SELECT MAX(FTCfgOTSeqNo) AS FNSeqNo FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMConfigOTRound WHERE  FNHSysEmpTypeId=" & Val(FNHSysEmpTypeId.Properties.Tag.ToString) & " "

                        Dim tSeqNo As String = HI.Conn.SQLConn.GetFieldOnBeginTrans(_Qry, Conn.DB.DataBaseName.DB_HR, "0")
                        tSeqNo = Val(tSeqNo) + 1

                        _Qry = "INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMConfigOTRound(FNHSysEmpTypeId,FTCfgOTSeqNo,FTCfgOTBegin,FTCfgOTEnd,FTCfgOTSet,FTStatePay"
                        _Qry &= vbCrLf & ", FTInsUser, FDInsDate, FTInsTime)  "
                        _Qry &= vbCrLf & " SELECT " & Val(FNHSysEmpTypeId.Properties.Tag.ToString) & "," & tSeqNo & ""
                        _Qry &= vbCrLf & "," & Me.FNStartAge.Value & "," & Me.FNEndAge.Value & "," & Me.FNRight.Value & ",'" & FTStatePay.EditValue.ToString & "' "
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


                    _Qry = "UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMConfigOTRound SET FTCfgOTSeqNo=FNNo"
                    _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMConfigOTRound INNER JOIN "
                    _Qry &= vbCrLf & "(SELECT ROW_NUMBER() OVER(ORDER BY FTCfgOTBegin,FTCfgOTEnd) AS FNNo, FTCfgOTSeqNo,FNHSysEmpTypeId"
                    _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMConfigOTRound WHERE  FNHSysEmpTypeId=" & Val(FNHSysEmpTypeId.Properties.Tag.ToString) & " "
                    _Qry &= vbCrLf & ") T1 ON THRMConfigOTRound.FTCfgOTSeqNo=T1.FTCfgOTSeqNo AND THRMConfigOTRound.FNHSysEmpTypeId=T1.FNHSysEmpTypeId"

                    HI.Conn.SQLConn.ExecuteTran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

                    Me.FNStartAge.Value = 0
                    Me.FNEndAge.Value = 0
                    Me.FNRight.Value = 0
                    Me.FNSeq.Value = 0
                    FTStatePay.Checked = False
                End If

                _Qry = " UPDATE  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMConfigOTPayOverRequest SET "
                _Qry &= vbCrLf & " FNTimeSacanMin=" & FNTimeSacanMin.Value
                _Qry &= vbCrLf & " ,FTStatePayOTOverRequest='" & FTStatePayOTOverRequest.EditValue.ToString & "'"
                _Qry &= vbCrLf & ",FTUpdUser = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Qry &= vbCrLf & ",FDUpdDate = " & HI.UL.ULDate.FormatDateDB & ""
                _Qry &= vbCrLf & ",FTUpdTime = " & HI.UL.ULDate.FormatTimeDB & ""
                _Qry &= vbCrLf & " WHERE  FNHSysEmpTypeId='" & Val(FNHSysEmpTypeId.Properties.Tag.ToString) & "'"

                If HI.Conn.SQLConn.ExecuteTran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                    _Qry = "INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMConfigOTPayOverRequest(FNHSysEmpTypeId,FTStatePayOTOverRequest,FNTimeSacanMin"
                    _Qry &= vbCrLf & ", FTInsUser, FDInsDate, FTInsTime)  "
                    _Qry &= vbCrLf & " SELECT " & Val(FNHSysEmpTypeId.Properties.Tag.ToString) & ",'" & FTStatePayOTOverRequest.EditValue.ToString & "'"
                    _Qry &= vbCrLf & "," & FNTimeSacanMin.Value & " "
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
                _Qry = " Delete  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMConfigOTRound  "
                _Qry &= vbCrLf & " WHERE  FNHSysEmpTypeId=" & Val(FNHSysEmpTypeId.Properties.Tag.ToString) & " "
                _Qry &= vbCrLf & "  AND FTCfgOTSeqNo=" & Me.FNSeq.Value

                HI.Conn.SQLConn.ExecuteTran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)


                _Qry = "UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMConfigOTRound SET FTCfgOTSeqNo=FNNo"
                _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMConfigOTRound INNER JOIN "
                _Qry &= vbCrLf & "(SELECT ROW_NUMBER() OVER(ORDER BY FTCfgOTBegin,FTCfgOTEnd) AS FNNo, FTCfgOTSeqNo,FNHSysEmpTypeId"
                _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMConfigOTRound WHERE  FNHSysEmpTypeId=" & Val(FNHSysEmpTypeId.Properties.Tag.ToString) & " "
                _Qry &= vbCrLf & ") T1 ON THRMConfigOTRound.FTCfgOTSeqNo=T1.FTCfgOTSeqNo AND THRMConfigOTRound.FNHSysEmpTypeId=T1.FNHSysEmpTypeId"

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

    Private Function VerrifyData() As Boolean
        Dim _Pass As Boolean = False

        If Me.FNHSysEmpTypeId.Text <> "" And FNHSysEmpTypeId.Properties.Tag.ToString <> "" Then
            _Pass = True
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, Me.FNHSysEmpTypeId_lbl.Text)
            FNHSysEmpTypeId.Focus()
        End If

        Return _Pass
    End Function

#End Region

    Private Sub ProcLoad()


        Dim _Qry  As  String = ""
        Dim _reset As String = ""


        Call BindingGrid(Me.FNHSysEmpTypeId.Properties.Tag.ToString)

    End Sub

    Private Sub BindingGrid(ByVal EmpType As String)
        Dim oDtbl As DataTable
        Dim oDtbl1 As DataTable
        Dim oDtbl2 As DataTable
        Dim oDtbl3 As DataTable
        Dim _Qry As String
        Try

            _Qry = "SELECT THRMConfigOT.FTCfgOTCode"

            If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
                _Qry &= vbCrLf & ",FTCfgOTName2  AS FTCfgOTName"

            Else
                _Qry &= vbCrLf & ",FTCfgOTName1  AS FTCfgOTName"
            End If

            _Qry &= vbCrLf & ",Cast(ISNULL(FCCfgOTValue,0) AS numeric(16,2)) AS FCCfgOTValue"
            _Qry &= vbCrLf & ",Cast(ISNULL(FCCfgOTAmtPlus,0) AS numeric(16,2)) AS FCCfgOTAmtPlus"
            _Qry &= vbCrLf & ",Cast(ISNULL(FCCfgOTNightValue,0) AS numeric(16,2)) AS FCCfgOTNightValue"
            _Qry &= vbCrLf & " FROM THRMConfigOT WITH(NOLOCK)"
            _Qry &= vbCrLf & " LEFT JOIN (SELECT FTCfgOTCode,FCCfgOTValue,FCCfgOTAmtPlus ,FCCfgOTNightValue FROM THRMConfigOTSet WITH(NOLOCK) WHERE  FNCalType=" & Val(EmpType) & ") THRMConfigOTSet"
            _Qry &= vbCrLf & " ON THRMConfigOT.FTCfgOTCode=THRMConfigOTSet.FTCfgOTCode"
            oDtbl = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

            With ogdForm
                .DataSource = oDtbl
            End With

            _Qry = "SELECT THRMConfigReturnVacation.FTCfgRetCode"

            If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
                _Qry &= vbCrLf & ",FTCfgRetName2  AS FTCfgRetName"
            Else
                _Qry &= vbCrLf & ",FTCfgRetName1  AS FTCfgRetName"
            End If

            _Qry &= vbCrLf & ",Cast(ISNULL(FCCfgRetValue,0) AS numeric(16,2)) AS FCCfgRetValue"
            _Qry &= vbCrLf & ",ISNULL(FTCfgRetTerm,'00') AS FTCfgRetTerm"
            _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMConfigReturnVacation WITH(NOLOCK)"
            _Qry &= vbCrLf & " LEFT JOIN (SELECT FTCfgRetCode,FCCfgRetValue,FTCfgRetTerm FROM THRMConfigReturnVacationSet WITH(NOLOCK) WHERE FNCalType=" & Val(EmpType) & ") THRMConfigReturnVacationSet"
            _Qry &= vbCrLf & " ON THRMConfigReturnVacation.FTCfgRetCode=THRMConfigReturnVacationSet.FTCfgRetCode"

            oDtbl1 = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)
            With ogdForm1
                .DataSource = oDtbl1
            End With

            _Qry = " SELECT FTCfgOTSeqNo,FTCfgOTBegin,FTCfgOTEnd,FTCfgOTSet,FTStatePay"
            _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMConfigOTRound WITH (NOLOCK)"
            _Qry &= vbCrLf & " WHERE  FNHSysEmpTypeId = " & Val(EmpType) & " Order by FTCfgOTSeqNo "
            oDtbl2 = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

            With ogdConfigOT
                .DataSource = oDtbl2
            End With

            Me.FTStatePayOTOverRequest.Checked = False
            Me.FNTimeSacanMin.Value = 0

            _Qry = " SELECT FTStatePayOTOverRequest,FNTimeSacanMin "
            _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMConfigOTPayOverRequest WITH (NOLOCK)"
            _Qry &= vbCrLf & " WHERE  FNHSysEmpTypeId = " & Val(EmpType) & " "

            oDtbl3 = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

            For Each R As DataRow In oDtbl3.Rows

                Me.FTStatePayOTOverRequest.Checked = (R!FTStatePayOTOverRequest.ToString = "1")
                Me.FNTimeSacanMin.Value = Integer.Parse(Val(R!FNTimeSacanMin.ToString))
                Exit For
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally

        End Try
    End Sub

    Private Sub ogcvacation_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ogdConfigOT.Click

    End Sub

    Private Sub ogvvacation_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ogvConfigOT.Click
        With ogvConfigOT
            If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount Then Exit Sub

            Me.FNSeq.Value = "" & .GetRowCellValue(.FocusedRowHandle, "FTCfgOTSeqNo").ToString
            Me.FNStartAge.Value = "" & .GetRowCellValue(.FocusedRowHandle, "FTCfgOTBegin").ToString
            Me.FNEndAge.Value = "" & .GetRowCellValue(.FocusedRowHandle, "FTCfgOTEnd").ToString
            Me.FNRight.Value = "" & .GetRowCellValue(.FocusedRowHandle, "FTCfgOTSet").ToString
            FTStatePay.Checked = ("" & .GetRowCellValue(.FocusedRowHandle, "FTStatePay").ToString = "1")
            Me.FNRight.Focus()

        End With
    End Sub

    Private Sub FNHSysEmpTypeId_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles FNHSysEmpTypeId.EditValueChanged

        If (_ProcPrepare) Then Exit Sub

        If Me.InvokeRequired Then
            Me.Invoke(New HI.Delegate.Dele.FNHSysEmpID_EditValueChanged(AddressOf FNHSysEmpTypeId_EditValueChanged), New Object() {sender, e})
        Else
            If FNHSysEmpTypeId.Text <> "" Then
                Dim _Qry As String

                _Qry = "SELECT TOP 1  FNHSysEmpTypeId FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType WITH(NOLOCK) WHERE FTEmpTypeCode='" & HI.UL.ULF.rpQuoted(FNHSysEmpTypeId.Text) & "'   AND  (FNHSysCmpId=" & HI.ST.SysInfo.CmpID & " OR ISNULL(FNHSysCmpId,0)=0) "
                FNHSysEmpTypeId.Properties.Tag = Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MASTER, "0"))

                Call ProcLoad()
            End If
        End If

    End Sub

    Private Sub wConfigLeave_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        AddHandler RepFNLeaveAge.Leave, AddressOf HI.TL.HandlerControl.CalEdit_Leave
        AddHandler RepFNLeavePay.Leave, AddressOf HI.TL.HandlerControl.CalEdit_Leave
        AddHandler RepFNLeaveRight.Leave, AddressOf HI.TL.HandlerControl.CalEdit_Leave

    End Sub

    Private Sub FNRight_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles FNRight.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If Me.ocmsave.Enabled And Me.ocmsave.Visible Then
                ProcessSave(ocmsave, New System.EventArgs)
                FNStartAge.Focus()
                FNStartAge.SelectAll()
            End If
        End If
    End Sub


End Class
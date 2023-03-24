Public Class wConfigPayRollMonthly_LA_Popup
    'Public wEmployee _Main {Get Set}

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        Dim oSysLang As New ST.SysLanguage
        Try

        Catch ex As Exception
        Finally
        End Try
        ' Add any initialization after the InitializeComponent() call.

    End Sub



    Private _FNHSysEmpID_edit As String
    Public Property FNHSysEmpID_edit As String
        Get
            Return _FNHSysEmpID_edit
        End Get
        Set(value As String)
            _FNHSysEmpID_edit = value
        End Set
    End Property






    Private Sub BtnExit_Click(sender As Object, e As EventArgs)
        _FNHSysEmpID_edit = ""
        Me.Close()
    End Sub

    Private Sub GetMaxSeq()

        'Try
        '    Dim _Qry As String

        '    _Qry = "SELECT  max(FNSeq) as m
        '                 FROM   " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.THRMPayroll_Cost "
        '    Dim _d As Double = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MASTER, "0")

        '    FNSeq.Text = Val(_d)


        'Catch ex As Exception

        'End Try

    End Sub
    Private Sub ClearScreen()
        ''FNHSysAccountId = ""
        'FTAccountCode.Text = ""
        'FTAccountNameTH.Text = ""
        'FTAccountNameEN.Text = ""

        FTStateActive.Checked = False


        FNHSysEmpID.Properties.Tag = ""
        FNHSysEmpID.Text = ""
        FNHSysEmpID.Text = ""

    End Sub
    Private Sub BtnAdd_Click(sender As Object, e As EventArgs)
        'If btnOrganize.Text <> "" Then
        '    If btnOrganizeBelongTo.Text <> "" Then
        '        If Me.ValidationAdd() Then
        '            If Me.AddData() Then
        '                HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
        '                FNHSysOrgBelongToId = ""
        '                ClearScreen()

        '                'btnAdd.Visible = False
        '            Else
        '                HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
        '            End If
        '        Else
        '            HI.MG.ShowMsg.mProcessError(201907150000, "", Me.Text)
        '        End If
        '    Else
        '        HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, lblFNHGroupBT.Text)

        '    End If
        'Else
        '    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, lblFNHGroup.Text)

        'End If
    End Sub

    Private Sub BtnSave_Click(sender As Object, e As EventArgs)

        If Me.ValidationSave() Then
            If Me.SaveData() Then
                HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                'btnSave.Visible = False
            Else
                HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
            End If
        Else
            HI.MG.ShowMsg.mProcessError(201907150000, "", Me.Text)
        End If

    End Sub


    Private Function ValidationAdd() As Boolean
        Try
            Dim _Qry As String

            _Qry = "SELECT  COUNT(*) AS N
                         FROM   " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.THRMPayroll_Monthly
                        WHERE FNHSysEmpID = " & Val(FNHSysEmpID.Properties.Tag)
            Dim _n As Integer = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MASTER, "0")

            If _n > 0 Then
                Return False
            Else
                Return True
            End If


        Catch ex As Exception

            Return False

        End Try
    End Function

    Private Function ValidationSave() As Boolean
        Try
            Dim _Qry As String

            _Qry = "SELECT  COUNT(*) AS N "
            _Qry &= vbCrLf & "          FROM   " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TPRODMIncentiveHeader "
            _Qry &= vbCrLf & "        WHERE FNHSysPositId = " & Val(FNHSysEmpID.Properties.Tag)
            _Qry &= vbCrLf & "  AND FNHSysCmpId = " & HI.ST.SysInfo.CmpID
            ''_Qry &= vbCrLf & " And FNHSysAccountId <> " & FNHSysAccountId
            Dim _n As Integer = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MASTER, "0")

            If _n > 0 Then
                Return False
            Else
                Return True
            End If


        Catch ex As Exception

            Return False

        End Try
    End Function

    Private Function AddData() As Boolean
        Try
            Dim _Qry As String

            Dim _ChkAtive As Integer

            Dim _SystemKey As String
            _ChkAtive = 0
            If FTStateActive.Checked = True Then
                _ChkAtive = 1
            End If

            Dim _FNSeq As Integer = 0

            Try

                '' _SystemKey = HI.TL.RunID.GetRunNoID("THRMAccount", "FNHSysAccountId", Conn.DB.DataBaseName.DB_MASTER).ToString()

                _Qry = " INSERT INTO " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.THRMPayroll_Monthly (FTInsUser, FDInsDate, FTInsTime
                        , FNHSysEmpID, FNHSysCmpId
,  FNSalary, FCOt1_Baht, FNTotalIncome
, FNTotalRecalSSO, FNSocial, FNSocialCmp
, FNTotalRecalTAX, FNTax
, FNNetpay , FTStateActive )
                        VALUES(" & "N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' ," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB
                _Qry &= vbCrLf & "," & Val(FNHSysEmpID.Properties.Tag.ToString)
                _Qry &= vbCrLf & "," & HI.ST.SysInfo.CmpID

                _Qry &= vbCrLf & "," & Double.Parse("0" & FNSalary.Text)
                _Qry &= vbCrLf & "," & Double.Parse("0" & FCOt1_Baht.Text)
                _Qry &= vbCrLf & "," & Double.Parse("0" &FNTotalIncome.Text)

                _Qry &= vbCrLf & "," & Double.Parse("0" &FNTotalRecalSSO.Text)
                _Qry &= vbCrLf & "," & Double.Parse("0" & FNSocial.Text)
                _Qry &= vbCrLf & "," & Double.Parse("0" & FNSocialCmp.Text)

                _Qry &= vbCrLf & "," & Double.Parse("0" & FNTotalRecalTAX.Text)
                _Qry &= vbCrLf & "," & Double.Parse("0" & FNTax.Text)


                _Qry &= vbCrLf & "," & Double.Parse("0" & FNNetpay.Text)


                _Qry &= vbCrLf & "," & _ChkAtive

                _Qry &= vbCrLf & ")"


                If (HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)) Then
                    Return True
                Else
                    Return False
                End If

            Catch ex As Exception

                Return False

            End Try
        Catch ex As Exception

            Return False
        End Try
    End Function


    Private Function SaveData() As Boolean
        Try
            Dim _Qry As String
            Dim _ChkAtive As Integer
            _ChkAtive = 0
            If FTStateActive.Checked = True Then
                _ChkAtive = 1
            End If




            Try


                _Qry = " UPDATE " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.THRMPayroll_Monthly SET   "
                _Qry &= vbCrLf & "            FTUpdUser = " & "N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Qry &= vbCrLf & "             , FDUpdDate  = " & HI.UL.ULDate.FormatDateDB
                _Qry &= vbCrLf & "           , FTUpdTime  = " & HI.UL.ULDate.FormatTimeDB

                _Qry &= vbCrLf & ", FTStateActive =" & _ChkAtive
                _Qry &= vbCrLf & ", FNSalary =" & Double.Parse("0" & FNSalary.Text)
                _Qry &= vbCrLf & ", FCOt1_Baht =" & Double.Parse("0" & FCOt1_Baht.Text)
                _Qry &= vbCrLf & ", FNTotalIncome =" & Double.Parse("0" & FNTotalIncome.Text)

                _Qry &= vbCrLf & ", FNTotalRecalSSO =" & Val(FNTotalRecalSSO.Text)
                _Qry &= vbCrLf & ", FNSocial =" & Double.Parse("0" & FNSocial.Text)
                _Qry &= vbCrLf & ", FNSocialCmp =" & Double.Parse("0" & FNSocialCmp.Text)

                _Qry &= vbCrLf & ", FNTotalRecalTAX =" & Double.Parse("0" & FNTotalRecalTAX.Text)
                _Qry &= vbCrLf & ", FNTax =" & Double.Parse("0" & FNTax.Text)

                _Qry &= vbCrLf & ", FNNetpay =" & Double.Parse("0" & FNNetpay.Text)

                _Qry &= vbCrLf & " WHERE FNHSysEmpID = " & Val(FNHSysEmpID.Properties.Tag.ToString)
                _Qry &= vbCrLf & "  AND FNHSysCmpId = " & HI.ST.SysInfo.CmpID


                If (HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)) Then
                    Return True
                Else
                    Return False
                End If

            Catch ex As Exception

                Return False

            End Try
        Catch ex As Exception

            Return False
        End Try
    End Function

    Private Function DeleteData() As Boolean
        Try

            Dim _Qry As String

            _Qry = " DELETE " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.THRMPayroll_Monthly  "
            _Qry &= vbCrLf & "          WHERE FNHSysEmpID = " & Val(FNHSysEmpID.Properties.Tag) & " AND FNHSysCmpId = " & HI.ST.SysInfo.CmpID
            If (HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)) Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception

            Return False
        End Try
    End Function

    Private Sub BtnDelete_Click(sender As Object, e As EventArgs)
        If HI.MG.ShowMsg.mConfirmProcess(HI.MG.ShowMsg.ProcessType.mDelete, GC.Text) = True Then
            If Me.DeleteData() Then
                Me.Close()

            End If

        End If
    End Sub

    Private Sub btnExit_Click_1(sender As Object, e As EventArgs) Handles btnExit.Click
        _FNHSysEmpID_edit = ""
        Me.Close()
    End Sub

    Private Sub btnAdd_Click_1(sender As Object, e As EventArgs) Handles btnAdd.Click
        If FNHSysEmpID.Text <> "" Then
            If Me.ValidationAdd() Then
                If Me.AddData() Then
                    HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                    _FNHSysEmpID_edit = ""

                    ' ClearScreen()

                    'btnAdd.Visible = False
                Else
                    HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                End If
            Else
                HI.MG.ShowMsg.mProcessError(201907150000, "", Me.Text)
            End If
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FNHSysPositId_lbl.Text)

        End If
    End Sub

    Private Sub btnSave_Click_1(sender As Object, e As EventArgs) Handles btnSave.Click
        If FNHSysEmpID.Text <> "" Then
            If Me.ValidationSave() Then
                If Me.SaveData() Then
                    HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                    'btnSave.Visible = False
                Else
                    HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                End If
            Else
                HI.MG.ShowMsg.mProcessError(201907150000, "", Me.Text)
            End If
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FNHSysPositId_lbl.Text)
        End If
    End Sub

    Private Sub btnDelete_Click_1(sender As Object, e As EventArgs) Handles btnDelete.Click
        If HI.MG.ShowMsg.mConfirmProcess(HI.MG.ShowMsg.ProcessType.mDelete, GC.Text) = True Then
            If Me.DeleteData() Then
                Me.Close()

            End If

        End If
    End Sub

    Private Sub wAccountPopup_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try


            If FNHSysEmpID_edit <> "" Then
                btnAdd.Visible = False
                btnSave.Visible = True
                btnDelete.Visible = True

            Else
                ''  FNPercentage.Text = "0.00"
                FTStateActive.Checked = True
                btnAdd.Visible = True
                btnSave.Visible = False
                btnDelete.Visible = False

                GetMaxSeq()

                ClearScreen()
            End If

        Catch ex As Exception
        End Try
    End Sub


End Class
Public Class wAccountGroupPopup
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



    Private _FNHSysAccountGroupId As String
    Public Property FNHSysAccountGroupId As String
        Get
            Return _FNHSysAccountGroupId
        End Get
        Set(value As String)
            _FNHSysAccountGroupId = value
        End Set
    End Property






    Private Sub BtnExit_Click(sender As Object, e As EventArgs)
        FNHSysAccountGroupId = ""
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
        FNHSysAccountGroupId = ""
        FTAccountGroupCode.Text = ""
        FTAccountGroupNameTH.Text = ""
        FTAccountGroupNameEN.Text = ""

        FTStateActive.Checked = False


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

            '_Qry = "SELECT  COUNT(*) AS N
            '             FROM   " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.THRMPayroll_Cost
            '            WHERE FTCostCode = " & FTAccountCode.Text
            'Dim _n As Integer = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MASTER, "0")

            'If _n > 0 Then
            '    Return False
            'Else
            Return True
            'End If


        Catch ex As Exception

            Return False

        End Try
    End Function

    Private Function ValidationSave() As Boolean
        Try
            Dim _Qry As String

            _Qry = "SELECT  COUNT(*) AS N
                        FROM   " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.THRMAccountGroup
                        WHERE FTAccountGroupCode = " & FTAccountGroupCode.Text &
                        " AND FNHSysAccountGroupId <> " & FNHSysAccountGroupId
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

            'Dim _ChkPOAtive As Integer
            '_ChkPOAtive = 0
            'If FTPOStateActive.Checked = True Then
            '    _ChkPOAtive = 1
            'End If

            'Dim _ChkFTOverHeadStateActive As Integer
            '_ChkFTOverHeadStateActive = 0
            'If FTOverHeadStateActive.Checked = True Then
            '    _ChkFTOverHeadStateActive = 1
            'End If

            'Dim _ChkFTParentState As Integer
            '_ChkFTParentState = 0
            'If FTParentState.Checked = True Then
            '    _ChkFTParentState = 1
            'End If

            Try

                _SystemKey = HI.TL.RunID.GetRunNoID("TCNMAccountGroup", "FNHSysAccountGroupId", Conn.DB.DataBaseName.DB_MASTER).ToString()

                _Qry = " INSERT INTO " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".[dbo].[TCNMAccountGroup] (FTInsUser, FDInsDate, FTInsTime
                        , FNHSysAccountGroupId, FTAccountGroupCode, FTAccountGroupNameTH, FTAccountGroupNameEN, FTStateActive )
                        VALUES(" & "N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' ," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB
                _Qry &= vbCrLf & "," & _SystemKey
                _Qry &= vbCrLf & ",'" & FTAccountGroupCode.Text & "','" & FTAccountGroupNameTH.Text & "',N'" & FTAccountGroupNameEN.Text & "'"
                _Qry &= vbCrLf & "," & _ChkAtive
                _Qry &= vbCrLf & ")"


                If (HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_MASTER)) Then
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


                _Qry = " UPDATE " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TCNMAccountGroup SET  
                            FTUpdUser = " & "N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'" &
                            ", FDUpdDate  = " & HI.UL.ULDate.FormatDateDB &
                            ", FTUpdTime  = " & HI.UL.ULDate.FormatTimeDB &
                             ", FTAccountGroupCode =N'" & FTAccountGroupCode.Text & "'" &
                            ", FTAccountGroupNameTH =N'" & FTAccountGroupNameTH.Text & "'" &
                            ", FTAccountGroupNameEN =N'" & FTAccountGroupNameEN.Text & "'" &
                            ", FTStateActive =" & _ChkAtive &
                            " WHERE FNHSysAccountGroupId = " & FNHSysAccountGroupId

                If (HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_MASTER)) Then
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

            _Qry = " DELETE " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.THRMAccountGroup
                          WHERE FNHSysAccountGroupId = " & FNHSysAccountGroupId

            If (HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_MASTER)) Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception

            Return False
        End Try
    End Function

    Private Sub BtnDelete_Click(sender As Object, e As EventArgs)
        If HI.MG.ShowMsg.mConfirmProcess(HI.MG.ShowMsg.ProcessType.mDelete, GroupControl3.Text) = True Then
            If Me.DeleteData() Then
                Me.Close()

            End If

        End If
    End Sub

    Private Sub btnExit_Click_1(sender As Object, e As EventArgs) Handles btnExit.Click
        FNHSysAccountGroupId = ""
        Me.Close()
    End Sub

    Private Sub btnAdd_Click_1(sender As Object, e As EventArgs) Handles btnAdd.Click
        If FTAccountGroupCode.Text <> "" Then
            If Me.ValidationAdd() Then
                If Me.AddData() Then
                    HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                    FNHSysAccountGroupId = ""

                    ' ClearScreen()

                    'btnAdd.Visible = False
                Else
                    HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                End If
            Else
                HI.MG.ShowMsg.mProcessError(201907150000, "", Me.Text)
            End If
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FTAccountCode_lbl.Text)

        End If
    End Sub

    Private Sub btnSave_Click_1(sender As Object, e As EventArgs) Handles btnSave.Click
        If FTAccountGroupCode.Text <> "" Then
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
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FTAccountCode_lbl.Text)
        End If
    End Sub

    Private Sub btnDelete_Click_1(sender As Object, e As EventArgs) Handles btnDelete.Click
        If HI.MG.ShowMsg.mConfirmProcess(HI.MG.ShowMsg.ProcessType.mDelete, GroupControl3.Text) = True Then
            If Me.DeleteData() Then
                Me.Close()

            End If

        End If
    End Sub

    Private Sub wAccountPopup_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try


            If FNHSysAccountGroupId <> "" Then
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
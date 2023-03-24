Public Class wOrganizationChartAccountCostPopup
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



    Private _FNHSysCostId As String
    Public Property FNHSysCostId As String
        Get
            Return _FNHSysCostId
        End Get
        Set(value As String)
            _FNHSysCostId = value
        End Set
    End Property




    Private Sub wOrganizationChartAccountCostPopup_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try



            If FNHSysCostId <> "" Then
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




    Private Sub BtnExit_Click(sender As Object, e As EventArgs)
        FNHSysCostId = ""
        Me.Close()
    End Sub

    Private Sub GetMaxSeq()

        Try
            Dim _Qry As String

            _Qry = "SELECT  max(FNSeq) +1 as m
                         FROM   " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.THRMPayroll_Cost "
            Dim _d As Double = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MASTER, "0")

            FNSeq.Text = Val(_d)


        Catch ex As Exception

        End Try

    End Sub
    Private Sub ClearScreen()
        FNHSysCostId = ""
        FTCostCode.Text = ""
        FTCostNameTH.Text = ""
        FTCostNameEN.Text = ""

        FNHSysDivisonId.Properties.Tag = ""
        FNHSysDivisonId.Text = ""
        FNHSysDivisonId_None.Text = ""

        FNHSysDeptId.Properties.Tag = ""
        FNHSysDeptId.Text = ""
        FNHSysDeptId_None.Text = ""

        FNHSysSectId.Properties.Tag = ""
        FNHSysSectId.Text = ""
        FNHSysSectId_None.Text = ""

        FNHSysUnitSectId.Properties.Tag = ""
        FNHSysUnitSectId.Text = ""
        FNHSysUnitSectId_None.Text = ""

        FNHSysAccountCostId.Properties.Tag = ""
        FNHSysAccountCostId.Text = ""
        FNHSysAccountCostId_None.Text = ""
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
                         FROM   " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.THRMPayroll_Cost
                        WHERE FTCostCode = " & FTCostCode.Text
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

            _Qry = "SELECT  COUNT(*) AS N
                        FROM   " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.THRMPayroll_Cost
                        WHERE FTCostCode = " & FTCostCode.Text &
                        " AND FNHSysCostId <> " & FNHSysCostId
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

            Try

                _SystemKey = HI.TL.RunID.GetRunNoID("THRMPayroll_Cost", "FNHSysCostId", Conn.DB.DataBaseName.DB_MASTER).ToString()

                _Qry = " INSERT INTO " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.THRMPayroll_Cost (FTInsUser, FDInsDate, FTInsTime
                        ,FNHSysCostId, FTCostCode, FTCostNameTH, FTCostNameEN
                        , FNHSysDeptId, FNHSysDivisonId, FNHSysSectId, FNHSysUnitSectId, FNHSysAccountCostId
                        , FTStateActive, FNHSysCmpId, FNSeq )
                        VALUES(" & "N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' ," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB
                _Qry &= vbCrLf & "," & _SystemKey
                _Qry &= vbCrLf & ",'" & FTCostCode.Text & "','" & FTCostNameTH.Text & "',N'" & FTCostNameEN.Text & "'"
                _Qry &= vbCrLf & "," & Val(FNHSysDeptId.Properties.Tag.ToString) & "," & Val(FNHSysDivisonId.Properties.Tag.ToString) & "," & Val(FNHSysSectId.Properties.Tag.ToString) & "," & Val(FNHSysUnitSectId.Properties.Tag.ToString) & "," & Val(FNHSysAccountCostId.Properties.Tag.ToString)
                _Qry &= vbCrLf & "," & _ChkAtive & "," & HI.ST.SysInfo.CmpID & "," & Val(FNSeq.Text) & ")"


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
                '_Qry = "SELECT  FNHSysCostId
                '            FROM    " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.THRMPayroll_Cost
                '            WHERE FTCostCode ='" & FNType.SelectedItem.ToString() & "'"
                'Dim FNHSysCostId As String = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "0")


                _Qry = " UPDATE " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.THRMPayroll_Cost SET  
                            FTUpdUser = " & "N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'" &
                            ", FDUpdDate  = " & HI.UL.ULDate.FormatDateDB &
                            ", FTUpdTime  = " & HI.UL.ULDate.FormatTimeDB &
                             ", FTCostCode =N'" & FTCostCode.Text & "'" &
                            ", FTCostNameTH =N'" & FTCostNameTH.Text & "'" &
                            ", FTCostNameEN =N'" & FTCostNameEN.Text & "'" &
                            ", FTStateActive =" & _ChkAtive &
                            ", FNHSysDeptId = " & Val(FNHSysDeptId.Properties.Tag.ToString) &
                            ", FNHSysDivisonId = " & Val(FNHSysDivisonId.Properties.Tag.ToString) &
                            ", FNHSysSectId = " & Val(FNHSysSectId.Properties.Tag.ToString) &
                            ", FNHSysUnitSectId = " & Val(FNHSysUnitSectId.Properties.Tag.ToString) &
                            ", FNHSysAccountCostId = " & Val(FNHSysAccountCostId.Properties.Tag.ToString) &
                             ", FNSeq = " & Val(FNSeq.Text) &
                            " WHERE FNHSysCostId = " & FNHSysCostId

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

            _Qry = " DELETE " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.THRMPayroll_Cost 
                          WHERE FNHSysCostId = " & FNHSysCostId

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
        FNHSysCostId = ""
        Me.Close()
    End Sub

    Private Sub btnAdd_Click_1(sender As Object, e As EventArgs) Handles btnAdd.Click
        If FTCostCode.Text <> "" Then
            If Me.ValidationAdd() Then
                If Me.AddData() Then
                    HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                    FNHSysCostId = ""

                    ClearScreen()
                    GetMaxSeq()
                    'btnAdd.Visible = False
                Else
                    HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                End If
            Else
                HI.MG.ShowMsg.mProcessError(201907150000, "", Me.Text)
            End If
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, lbl_Cost_Code.Text)

        End If
    End Sub

    Private Sub btnSave_Click_1(sender As Object, e As EventArgs) Handles btnSave.Click
        If FTCostCode.Text <> "" Then
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
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, lbl_Cost_Code.Text)
        End If
    End Sub

    Private Sub btnDelete_Click_1(sender As Object, e As EventArgs) Handles btnDelete.Click
        If HI.MG.ShowMsg.mConfirmProcess(HI.MG.ShowMsg.ProcessType.mDelete, GroupControl3.Text) = True Then
            If Me.DeleteData() Then
                Me.Close()

            End If

        End If
    End Sub
End Class
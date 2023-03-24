Public Class wOrganizationChartBelongToPopup
    Public _wOrganizationChartBelongTo_Select As wOrganizationChartBelongTo_Select
    'Public wEmployee _Main {Get Set}

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        _wOrganizationChartBelongTo_Select = New wOrganizationChartBelongTo_Select
        HI.TL.HandlerControl.AddHandlerObj(_wOrganizationChartBelongTo_Select)

        Dim oSysLang As New ST.SysLanguage
        Try

            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _wOrganizationChartBelongTo_Select.Name.ToString.Trim, _wOrganizationChartBelongTo_Select)
        Catch ex As Exception
        Finally
        End Try
        ' Add any initialization after the InitializeComponent() call.

    End Sub



    Private _FNHSysOrgBelongToId As String
    Public Property FNHSysOrgBelongToId As String
        Get
            Return _FNHSysOrgBelongToId
        End Get
        Set(value As String)
            _FNHSysOrgBelongToId = value
        End Set
    End Property


    Private _FNHSysOrgId As String
    Public Property FNHSysOrgId As String
        Get
            Return _FNHSysOrgId
        End Get
        Set(value As String)
            _FNHSysOrgId = value
        End Set
    End Property


    Private _FNHOrgBelongId As String
    Public Property FNHOrgBelongId As String
        Get
            Return _FNHOrgBelongId
        End Get
        Set(value As String)
            _FNHOrgBelongId = value
        End Set
    End Property






    Private Sub wOrganizationChartBelongToPopup_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            RemoveHandler btnOrganize.EditValueChanged, AddressOf HI.TL.HandlerControl.DynamicButtonedit_EditValueChanged
            RemoveHandler btnOrganize.ButtonClick, AddressOf HI.TL.HandlerControl.DynamicButtone_ButtonClick
            RemoveHandler btnOrganize.Leave, AddressOf HI.TL.HandlerControl.DynamicButtonedit_LeaveOnly


            RemoveHandler btnOrganizeBelongTo.EditValueChanged, AddressOf HI.TL.HandlerControl.DynamicButtonedit_EditValueChanged
            RemoveHandler btnOrganizeBelongTo.ButtonClick, AddressOf HI.TL.HandlerControl.DynamicButtone_ButtonClick
            RemoveHandler btnOrganizeBelongTo.Leave, AddressOf HI.TL.HandlerControl.DynamicButtonedit_LeaveOnly

            'RemoveHandler FNHSysEmpID.EditValueChanged, AddressOf HI.TL.HandlerControl.DynamicButtonedit_EditValueChanged
            'RemoveHandler FNHSysEmpID.ButtonClick, AddressOf HI.TL.HandlerControl.DynamicButtone_ButtonClick
            '   RemoveHandler FNHSysEmpID.Leave, AddressOf HI.TL.HandlerControl.DynamicButtonedit_LeaveOnly
            ' Me.ogcdetail.DataSource = _oDt

            If FNHSysOrgBelongToId <> "" Then
                btnAdd.Visible = False
                btnSave.Visible = True
                btnDelete.Visible = True

            Else
                FNPercentage.Text = "0.00"
                FTStateActive.Checked = True
                btnAdd.Visible = True
                btnSave.Visible = False
                btnDelete.Visible = False
                ClearScreen()
            End If


        Catch ex As Exception
        End Try
    End Sub
    Private Sub BtnOrganize_Click(sender As Object, e As EventArgs) Handles btnOrganize.Click

        Try

            With _wOrganizationChartBelongTo_Select
                .FTDivisonCode_O = ""
                ''  ._oDt = _Dt
                '' .FundRate = ""

                '  Call HI.ST.Lang.SP_SETxLanguage(_wFundRatePopUp)
                .ShowDialog()
                btnOrganize.Text = .FNHSysOrgId_O
                btnOrganize.Properties.Tag = .FNHSysOrgId_O

                FNHGroup.Text = .FNHGroup_O

                FTCLevelCode.Text = .FTCLevelCode_O
                FTCLevelName.Text = .FTCLevelName_O

                FTCountryCode.Text = .FTCountryCode_O
                FTCountryName.Text = .FTCountryName_O

                FTCmpCode.Text = .FTCmpCode_O
                FTCmpName.Text = .FTCmpName_O

                FTDivisonCode.Text = .FTDivisonCode_O
                FTDivisonName.Text = .FTDivisonName_O
                FTDeptCode.Text = .FTDeptCode_O
                FTDeptDesc.Text = .FTDeptDesc_O
                FTSectCode.Text = .FTSectCode_O
                FTSectName.Text = .FTSectName_O
                FTUnitSectCode.Text = .FTUnitSectCode_O
                FTUnitSectName.Text = .FTUnitSectName_O
                FTPositCode.Text = .FTPositCode_O
                FTPositName.Text = .FTPositName_O

            End With
        Catch ex As Exception
        End Try

    End Sub



    Private Sub BtnOrganizeBelongTo_Click(sender As Object, e As EventArgs) Handles btnOrganizeBelongTo.Click
        Try

            With _wOrganizationChartBelongTo_Select
                .FTDivisonCode_O = ""
                ''  ._oDt = _Dt
                '' .FundRate = ""

                '  Call HI.ST.Lang.SP_SETxLanguage(_wFundRatePopUp)
                .ShowDialog()
                btnOrganizeBelongTo.Text = .FNHSysOrgId_O
                btnOrganizeBelongTo.Properties.Tag = .FNHSysOrgId_O


                FNHGroup_B.Text = .FNHGroup_O

                FTCLevelCode_B.Text = .FTCLevelCode_O
                FTCLevelName_B.Text = .FTCLevelName_O

                FTCountryCode_B.Text = .FTCountryCode_O
                FTCountryName_B.Text = .FTCountryName_O

                FTCmpCode_B.Text = .FTCmpCode_O
                FTCmpName_B.Text = .FTCmpName_O

                FTDivisonCode_B.Text = .FTDivisonCode_O
                FTDivisonName_B.Text = .FTDivisonName_O
                FTDeptCode_B.Text = .FTDeptCode_O
                FTDeptDesc_B.Text = .FTDeptDesc_O
                FTSectCode_B.Text = .FTSectCode_O
                FTSectName_B.Text = .FTSectName_O
                FTUnitSectCode_B.Text = .FTUnitSectCode_O
                FTUnitSectName_B.Text = .FTUnitSectName_O
                FTPositCode_B.Text = .FTPositCode_O
                FTPositName_B.Text = .FTPositName_O

            End With
        Catch ex As Exception
        End Try
    End Sub

    Private Sub BtnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        FNHSysOrgBelongToId = ""
        Me.Close()
    End Sub

    Private Sub ClearScreen()

        btnOrganize.Text = ""
        btnOrganize.Properties.Tag = ""

        FNHGroup.Text = ""

        FTCLevelCode.Text = ""
        FTCLevelName.Text = ""

        FTCountryCode.Text = ""
        FTCountryName.Text = ""

        FTCmpCode.Text = ""
        FTCmpName.Text = ""

        FTDivisonCode.Text = ""
        FTDivisonName.Text = ""
        FTDeptCode.Text = ""
        FTDeptDesc.Text = ""
        FTSectCode.Text = ""
        FTSectName.Text = ""
        FTUnitSectCode.Text = ""
        FTUnitSectName.Text = ""
        FTPositCode.Text = ""
        FTPositName.Text = ""


        btnOrganizeBelongTo.Text = ""
        btnOrganizeBelongTo.Properties.Tag = ""

        FNHGroup_B.Text = ""

        FTCLevelCode_B.Text = ""
        FTCLevelName_B.Text = ""

        FTCountryCode_B.Text = ""
        FTCountryName_B.Text = ""

        FTCmpCode_B.Text = ""
        FTCmpName_B.Text = ""

        FTDivisonCode_B.Text = ""
        FTDivisonName_B.Text = ""
        FTDeptCode_B.Text = ""
        FTDeptDesc_B.Text = ""
        FTSectCode_B.Text = ""
        FTSectName_B.Text = ""
        FTUnitSectCode_B.Text = ""
        FTUnitSectName_B.Text = ""
        FTPositCode_B.Text = ""
        FTPositName_B.Text = ""

        FNType.SelectedItem = 1
        FNPercentage.Text = "0.00"
        FNHSysEmpID.Text = ""
        FNHSysEmpID.Properties.Tag = ""
        FTEmpName.Text = ""
        FTRemark.Text = ""

    End Sub


    Private Sub BtnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        If btnOrganize.Text <> "" Then
            If btnOrganizeBelongTo.Text <> "" Then
                If Me.ValidationAdd() Then
                    If Me.AddData() Then
                        HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                        FNHSysOrgBelongToId = ""
                        ClearScreen()

                        'btnAdd.Visible = False
                    Else
                        HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                    End If
                Else
                    HI.MG.ShowMsg.mProcessError(201907150000, "", Me.Text)
                End If
            Else
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, lblFNHGroupBT.Text)

            End If
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, lblFNHGroup.Text)

        End If
    End Sub

    Private Sub BtnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If btnOrganize.Text <> "" Then
            If btnOrganizeBelongTo.Text <> "" Then
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
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, lblFNHGroupBT.Text)
            End If
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, lblFNHGroup.Text)
        End If
    End Sub


    Private Function ValidationAdd() As Boolean
        Try
            Dim _Qry As String

            _Qry = "SELECT  COUNT(*) AS N
                        FROM    HITECH_HR.dbo. THRMOrganizationChartBelongTo
                        WHERE FNHSysOrgId = " & btnOrganize.Text &
                        "And FNHOrgBelongId = " & btnOrganizeBelongTo.Text
            Dim _n As Integer = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "0")

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
                        FROM   " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.THRMOrganizationChartBelongTo
                        WHERE FNHSysOrgId = " & btnOrganize.Text &
                        " And FNHOrgBelongId = " & btnOrganizeBelongTo.Text &
                        " AND FNHSysOrgBelongToId <> " & FNHSysOrgBelongToId
            Dim _n As Integer = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "0")

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
            _ChkAtive = 0
            If FTStateActive.Checked = True Then
                _ChkAtive = 1
            End If

            Try

                _Qry = "SELECT  FNTypeId
                            FROM    " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.THRMOrganizationChartBelongToType
                            WHERE FTTypeName ='" & FNType.SelectedItem.ToString() & "'"
                Dim _FNTypeId As String = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "0")


                _Qry = " INSERT INTO " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.THRMOrganizationChartBelongTo (FTInsUser, FDInsDate, FTInsTime
                        , FNHSysOrgId, FNHOrgBelongId
                        , FTRemark, FTStateActive, FNHSysEmpID, FNTypeId, FNPercentage)
                        VALUES(" & "N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' ," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB
                _Qry &= vbCrLf & "," & btnOrganize.Text & "," & btnOrganizeBelongTo.Text & ",N'" & FTRemark.Text & "'," & _ChkAtive
                _Qry &= vbCrLf & "," & Val(FNHSysEmpID.Properties.Tag.ToString) & "," & _FNTypeId & "," & FNPercentage.Text & ")"

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
                _Qry = "SELECT  FNTypeId
                            FROM    " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.THRMOrganizationChartBelongToType
                            WHERE FTTypeName ='" & FNType.SelectedItem.ToString() & "'"
                Dim _FNTypeId As String = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "0")


                _Qry = " UPDATE " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.THRMOrganizationChartBelongTo SET  
                            FTUpdUser = " & "N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'" &
                            ", FDUpdDate  = " & HI.UL.ULDate.FormatDateDB &
                            ", FTUpdTime  = " & HI.UL.ULDate.FormatTimeDB &
                            ", FNHSysOrgId = " & btnOrganize.Text &
                            ", FNHOrgBelongId = " & btnOrganizeBelongTo.Text &
                            ", FTRemark =N'" & FTRemark.Text & "'" &
                            ", FTStateActive =" & _ChkAtive &
                            ", FNHSysEmpID = " & Val(FNHSysEmpID.Properties.Tag.ToString) &
                            ", FNTypeId = " & _FNTypeId &
                            ", FNPercentage =" & FNPercentage.Text &
                            " WHERE FNHSysOrgBelongToId = " & FNHSysOrgBelongToId

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

            _Qry = " DELETE " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.THRMOrganizationChartBelongTo 
                          WHERE FNHSysOrgBelongToId = " & FNHSysOrgBelongToId

            If (HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)) Then
                    Return True
                Else
                    Return False
                End If

        Catch ex As Exception

            Return False
        End Try
    End Function

    Private Sub BtnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If HI.MG.ShowMsg.mConfirmProcess(HI.MG.ShowMsg.ProcessType.mDelete, GroupControl3.Text) = True Then
            If Me.DeleteData() Then
                Me.Close()

            End If

        End If
    End Sub
End Class
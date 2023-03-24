Public Class wOrganizationChartMappingPopup
    Public _wOrganizationChartMapping_Select As wOrganizationChartMapping_Select
    'Public wEmployee _Main {Get Set}

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        _wOrganizationChartMapping_Select = New wOrganizationChartMapping_Select
        HI.TL.HandlerControl.AddHandlerObj(_wOrganizationChartMapping_Select)

        Dim oSysLang As New ST.SysLanguage
        Try

            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _wOrganizationChartMapping_Select.Name.ToString.Trim, _wOrganizationChartMapping_Select)
        Catch ex As Exception
        Finally
        End Try
        ' Add any initialization after the InitializeComponent() call.

    End Sub



    Private _FNHSysOrgMapId As String
    Public Property FNHSysOrgMapId As String
        Get
            Return _FNHSysOrgMapId
        End Get
        Set(value As String)
            _FNHSysOrgMapId = value
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






    Private Sub wOrganizationChartMappingPopup_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            RemoveHandler btnOrganize.EditValueChanged, AddressOf HI.TL.HandlerControl.DynamicButtonedit_EditValueChanged
            RemoveHandler btnOrganize.ButtonClick, AddressOf HI.TL.HandlerControl.DynamicButtone_ButtonClick
            RemoveHandler btnOrganize.Leave, AddressOf HI.TL.HandlerControl.DynamicButtonedit_LeaveOnly

            If FNHSysOrgMapId <> "" Then
                btnAdd.Visible = False
                btnSave.Visible = True

            Else
                FNPercentageQty.Text = "0.00"
                FTStateActive.Checked = True
                btnAdd.Visible = True
                btnSave.Visible = False
                ClearScreen()
            End If


        Catch ex As Exception
        End Try
    End Sub
    Private Sub BtnOrganize_Click(sender As Object, e As EventArgs) Handles btnOrganize.Click

        Try

            With _wOrganizationChartMapping_Select
                .FTDivisonCode_O = ""
                ''  ._oDt = _Dt
                '' .FundRate = ""

                '  Call HI.ST.Lang.SP_SETxLanguage(_wFundRatePopUp)
                .ShowDialog()
                btnOrganize.Text = .FNHSysOrgId_O
                btnOrganize.Properties.Tag = .FNHSysOrgId_O

                'FNHGroup.Text = .FNHGroup_O

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



    Private Sub BtnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        FNHSysOrgMapId = ""
        Me.Close()
    End Sub

    Private Sub ClearScreen()
        FNHSysOrgMapId = ""

        btnOrganize.Text = ""
        btnOrganize.Properties.Tag = ""

        'FNHGroup.Text = ""

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


        FNPercentageQty.Text = "0.00"
        FNHSysEmpID.Text = ""
        FNHSysEmpID.Properties.Tag = ""
        FTEmpName.Text = ""
        FTRemark.Text = ""

    End Sub


    Private Sub BtnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        If btnOrganize.Text <> "" Then
            If Me.ValidationAdd() Then
                If Me.AddData() Then
                    HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                    FNHSysOrgMapId = ""
                    ClearScreen()

                    'btnAdd.Visible = False
                Else
                    HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                End If
            Else
                HI.MG.ShowMsg.mProcessError(201907150000, "", Me.Text)
            End If

        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, LabelControl5.Text)

        End If
    End Sub

    Private Sub BtnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If btnOrganize.Text <> "" Then
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
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, LabelControl5.Text)
        End If
    End Sub


    Private Function ValidationAdd() As Boolean
        Try
            Dim _Qry As String

            _Qry = "SELECT  COUNT(*) AS N
                        FROM   " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo. TCNMOrgmapping
                        WHERE FNHSysOrgId = " & btnOrganize.Text &
                        " And FNHSysEmpID = " & Val(FNHSysEmpID.Properties.Tag.ToString)
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
                        FROM   " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TCNMOrgmapping
                        WHERE FNHSysOrgId = " & btnOrganize.Text &
                        " And FNHSysEmpID = " & Val(FNHSysEmpID.Properties.Tag.ToString) &
             " AND FNHSysOrgMapId <> " & FNHSysOrgMapId
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
            _ChkAtive = 0
            If FTStateActive.Checked = True Then
                _ChkAtive = 1
            End If

            Try
                Dim _Id As Integer
                _Id = HI.TL.RunID.GetRunNoID("" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "..TCNMOrgmapping", "FNHSysOrgMapId", Conn.DB.DataBaseName.DB_MASTER)


                _Qry = " INSERT INTO " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TCNMOrgmapping (FTInsUser, FDInsDate, FTInsTime
                        ,FNHSysOrgMapId, FNHSysOrgId
                        , FTRemark, FTStateActive, FNHSysEmpID, FNPercentageQty)
                        VALUES(" & "N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' ," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB
                _Qry &= vbCrLf & "," & _Id
                _Qry &= vbCrLf & "," & btnOrganize.Text & ",N'" & FTRemark.Text & "'," & _ChkAtive
                _Qry &= vbCrLf & "," & Val(FNHSysEmpID.Properties.Tag.ToString) & "," & FNPercentageQty.Text & ")"

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


                _Qry = " UPDATE " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TCNMOrgmapping SET  
                            FTUpdUser = " & "N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'" &
                            ", FDUpdDate  = " & HI.UL.ULDate.FormatDateDB &
                            ", FTUpdTime  = " & HI.UL.ULDate.FormatTimeDB &
                            ", FNHSysOrgId = " & btnOrganize.Text &
                ", FTRemark =N'" & FTRemark.Text & "'" &
                ", FTStateActive =" & _ChkAtive &
                ", FNHSysEmpID = " & Val(FNHSysEmpID.Properties.Tag.ToString) &
                ", FNPercentageQty =" & FNPercentageQty.Text &
                " WHERE FNHSysOrgMapId = " & _FNHSysOrgMapId

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


End Class
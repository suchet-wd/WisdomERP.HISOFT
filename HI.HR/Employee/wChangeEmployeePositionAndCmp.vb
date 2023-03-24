Public Class wChangeEmployeePositionAndCmp

#Region "Property"

    Private _CallMenuName As String = ""
    Public Property CallMenuName As String
        Get
            Return _CallMenuName
        End Get
        Set(value As String)
            _CallMenuName = value
        End Set
    End Property

    Private _CallMethodName As String = ""
    Public Property CallMethodName As String
        Get
            Return _CallMethodName
        End Get
        Set(value As String)
            _CallMethodName = value
        End Set
    End Property

    Private _CallMethodParm As String = ""
    Public Property CallMethodParm As String
        Get
            Return _CallMethodParm
        End Get
        Set(value As String)
            _CallMethodParm = value
        End Set
    End Property

#End Region

#Region "Procedure"
    Private Sub LoadEmpData(FNHSysEmpID As String)

        Dim _PathEmpPic As String
        _PathEmpPic = ""
        Dim cmdstring As String = "Select Top 1 FTCfgData FROM  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & ".dbo.TSESystemConfig AS X WITH(NOLOCK) WHERE FTCfgName='PathEmpPic'"

        _PathEmpPic = HI.Conn.SQLConn.GetField(cmdstring, Conn.DB.DataBaseName.DB_SECURITY, "")

        Dim _dt As DataTable
        Dim _Qry As String = ""
        _Qry = " SELECT    TOP 1     M.FTEmpCode, M.FTEmpCodeRefer, M.FTEmpNameTH, M.FTEmpSurnameTH, M.FTEmpNicknameTH, M.FTEmpNameEN, M.FNHSysEmpTypeId, M.FNHSysDeptId, "
        _Qry &= vbCrLf & "   D.FTDeptCode, Di.FTDivisonCode, M.FNHSysDivisonId, M.FNHSysSectId, S.FTSectCode, ET.FTEmpTypeCode, M.FNHSysUnitSectId, US.FTUnitSectCode,"
        _Qry &= vbCrLf & "  M.FNHSysEmpID, M.FTEmpPicName, M.FNHSysPositId, P.FTPositCode"
        _Qry &= vbCrLf & "  FROM            THRMEmployee AS M WITH (NOLOCK) LEFT OUTER JOIN"
        _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMPosition AS P WITH (NOLOCK) ON M.FNHSysPositId = P.FNHSysPositId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS US WITH (NOLOCK) ON M.FNHSysUnitSectId = US.FNHSysUnitSectId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS ET WITH (NOLOCK) ON M.FNHSysEmpTypeId = ET.FNHSysEmpTypeId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSect AS S WITH (NOLOCK) ON M.FNHSysSectId = S.FNHSysSectId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDivision AS Di WITH (NOLOCK) ON M.FNHSysDivisonId = Di.FNHSysDivisonId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDepartment AS D WITH (NOLOCK) ON M.FNHSysDeptId = D.FNHSysDeptId"
        _Qry &= vbCrLf & "  WHERE  M.FNHSysEmpID  =" & Val(FNHSysEmpID) & " "

        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

        FTEmpPicName.Image = Nothing
        If _dt.Rows.Count > 0 Then
            For Each R As DataRow In _dt.Rows

                If _PathEmpPic = "" Then
                    FTEmpPicName.Image = HI.UL.ULImage.LoadImage(HI.ST.SysInfo.SysPath & "EmpPicture\" & R!FTEmpPicName.ToString)
                Else
                    FTEmpPicName.Image = HI.UL.ULImage.LoadImage(_PathEmpPic & R!FTEmpPicName.ToString)
                End If

                FNHSysEmpTypeId.Text = R!FTEmpTypeCode.ToString
                FNHSysDeptId.Text = R!FTDeptCode.ToString
                FNHSysDivisonId.Text = R!FTDivisonCode.ToString
                FNHSysSectId.Text = R!FTSectCode.ToString
                FNHSysUnitSectId.Text = R!FTUnitSectCode.ToString
                FNHSysPositId.Text = R!FTPositCode.ToString

            Next
        Else

            FNHSysEmpTypeId.Text = ""
            FNHSysDeptId.Text = ""
            FNHSysDivisonId.Text = ""
            FNHSysSectId.Text = ""
            FNHSysUnitSectId.Text = ""
            FNHSysPositId.Text = ""

        End If

    End Sub

    Private Sub FNHSysEmpID_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FNHSysEmpID.EditValueChanged
        If FNHSysEmpID.Text <> "" Then
            ' If FNHSysEmpID.Properties.Tag.ToString = "" Then
            Dim _Qry As String = "SELECT TOP 1 FNHSysEmpID  FROM THRMEmployee WITH(NOLOCK) WHERE FTEmpCode ='" & HI.UL.ULF.rpQuoted(FNHSysEmpID.Text) & "' "
            FNHSysEmpID.Properties.Tag = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "")
            'End If

            Call LoadEmpData(FNHSysEmpID.Properties.Tag.ToString)
            Call LoadHistory(FNHSysEmpID.Properties.Tag.ToString)

        End If
    End Sub


    Private Sub LoadHistory(ByVal Key As String)
        Try
            Dim _Qry As String

            _Qry = " SELECT  M.FNSeq, M.FNHSysEmpID, M.FNHSysEmpTypeId, ET1.FTEmpTypeCode"

            If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then

                _Qry &= vbCrLf & ",ET1.FTEmpTypeNameTH AS FTEmpTypeName"
                _Qry &= vbCrLf & ",ET2.FTEmpTypeNameTH AS FTEmpTypeName2 "

                _Qry &= vbCrLf & ",D1.FTDeptDescTH AS FTDeptName "
                _Qry &= vbCrLf & ",D2.FTDeptDescTH AS FTDeptName2 "

                _Qry &= vbCrLf & ",DV1.FTDivisonNameTH AS FTDivisonName "
                _Qry &= vbCrLf & ",DV2.FTDivisonNameTH AS FTDivisonName2 "

                _Qry &= vbCrLf & ",S1.FTSectNameTH AS FTSectName "
                _Qry &= vbCrLf & ",S2.FTSectNameTH AS FTSectName2 "

                _Qry &= vbCrLf & ",US1.FTUnitSectNameTH AS FTUnitSectName "
                _Qry &= vbCrLf & ",US2.FTUnitSectNameTH AS FTUnitSectName2 "

                _Qry &= vbCrLf & ",P1.FTPositNameTH AS FTPositName "
                _Qry &= vbCrLf & ",P2.FTPositNameTH AS FTPositName2 "

                _Qry &= vbCrLf & ",PM.FTPmtReasonNameTH AS FTPmtReasonName "


            Else

                _Qry &= vbCrLf & ",ET1.FTEmpTypeNameEN AS FTEmpTypeName "
                _Qry &= vbCrLf & ",ET2.FTEmpTypeNameEN AS FTEmpTypeName2 "

                _Qry &= vbCrLf & ",D1.FTDeptDescEN AS FTDeptName "
                _Qry &= vbCrLf & ",D2.FTDeptDescEN AS FTDeptName2 "

                _Qry &= vbCrLf & ",DV1.FTDivisonNameEN AS FTDivisonName "
                _Qry &= vbCrLf & ",DV2.FTDivisonNameEN AS FTDivisonName2 "

                _Qry &= vbCrLf & ",S1.FTSectNameEN AS FTSectName "
                _Qry &= vbCrLf & ",S2.FTSectNameEN AS FTSectName2 "

                _Qry &= vbCrLf & ",US1.FTUnitSectNameEN AS FTUnitSectName "
                _Qry &= vbCrLf & ",US2.FTUnitSectNameEN AS FTUnitSectName2 "

                _Qry &= vbCrLf & ",P1.FTPositNameEN AS FTPositName "
                _Qry &= vbCrLf & ",P2.FTPositNameEN AS FTPositName2 "

                _Qry &= vbCrLf & ",PM.FTPmtReasonNameEN AS FTPmtReasonName "
            End If

            _Qry &= vbCrLf & " ,M.FNHSysEmpTypeIdTo,ET2.FTEmpTypeCode AS FTEmpTypeCode2"
            _Qry &= vbCrLf & " ,M.FNHSysDeptId, D1.FTDeptCode"
            _Qry &= vbCrLf & " ,M.FNHSysDeptIdTo, D2.FTDeptCode AS FTDeptCode2"
            _Qry &= vbCrLf & " ,M.FNHSysDivisonId, DV1.FTDivisonCode "
            _Qry &= vbCrLf & " ,M.FNHSysDivisonIdTo, DV2.FTDivisonCode AS FTDivisonCode2"
            _Qry &= vbCrLf & " ,M.FNHSysSectId, S1.FTSectCode"
            _Qry &= vbCrLf & " ,M.FNHSysSectIdTo, S2.FTSectCode AS FTSectCode2"
            _Qry &= vbCrLf & " ,M.FNHSysUnitSectId, US1.FTUnitSectCode "
            _Qry &= vbCrLf & " ,M.FNHSysUnitSectIdTo, US2.FTUnitSectCode As FTUnitSectCode2"
            _Qry &= vbCrLf & " ,M.FNHSysPositId, P1.FTPositCode "
            _Qry &= vbCrLf & " ,M.FNHSysPositIdTo, P2.FTPositCode AS FTPositCode2"
            _Qry &= vbCrLf & " ,M.FNHSysPmtReasoneId, PM.FTPmtReasonCode"
            _Qry &= vbCrLf & "  ,CASE WHEN  ISDATE(M.FTEffectiveDate) = 1 THEN  CONVERT(varchar(10),Convert(Datetime,M.FTEffectiveDate),103) ELSE '' END AS FTEffectiveDate "
            _Qry &= vbCrLf & " , M.FTNote  , isnull(M.FTUpdUser  , M.FTInsUser) as FTInsUser , isnull( M.FTUpdTime ,M.FTInsTime) as FTUpdTime , Isnull(M.FTUpdDate , M.FTInsDate) as FDInsDate"

            _Qry &= vbCrLf & " , M.FNHSysCmpId , M.FNHSysCmpIdTo "
            _Qry &= vbCrLf & ", C1.FTCmpCode , C2.FTCmpCode [FTCmpCodeTo]  "

            _Qry &= vbCrLf & "  FROM    THRTEmployeeMasterChange AS M WITH (NOLOCK) INNER JOIN"
            _Qry &= vbCrLf & " 	[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS ET1 WITH (NOLOCK) ON M.FNHSysEmpTypeId = ET1.FNHSysEmpTypeId INNER JOIN"
            _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS ET2 WITH (NOLOCK) ON M.FNHSysEmpTypeIdTo = ET2.FNHSysEmpTypeId  LEFT OUTER JOIN"
            _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDepartment AS D1 WITH (NOLOCK) ON M.FNHSysDeptId = D1.FNHSysDeptId  LEFT OUTER JOIN"
            _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDepartment AS D2 WITH (NOLOCK) ON M.FNHSysDeptIdTo = D2.FNHSysDeptId  LEFT OUTER JOIN"
            _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDivision AS DV1 WITH (NOLOCK) ON M.FNHSysDivisonId = DV1.FNHSysDivisonId  LEFT OUTER JOIN"
            _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDivision AS DV2 WITH (NOLOCK) ON M.FNHSysDivisonIdTo = DV2.FNHSysDivisonId  LEFT OUTER JOIN"
            _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSect AS S1 WITH (NOLOCK) ON M.FNHSysSectId = S1.FNHSysSectId  LEFT OUTER JOIN"
            _Qry &= vbCrLf & " 	[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSect AS S2 WITH (NOLOCK) ON M.FNHSysSectIdTo = S2.FNHSysSectId  LEFT OUTER JOIN"
            _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS US1 WITH (NOLOCK) ON M.FNHSysUnitSectId = US1.FNHSysUnitSectId LEFT OUTER JOIN"
            _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS US2 WITH (NOLOCK) ON M.FNHSysUnitSectIdTo = US2.FNHSysUnitSectId LEFT OUTER JOIN"
            _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMPosition AS P1 WITH (NOLOCK) ON M.FNHSysPositId = P1.FNHSysPositId  LEFT OUTER JOIN"
            _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMPosition AS P2 WITH (NOLOCK) ON M.FNHSysPositIdTo = P2.FNHSysPositId  LEFT OUTER JOIN"
            _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMPromotionReason AS PM WITH (NOLOCK) ON M.FNHSysPmtReasoneId = PM.FNHSysPmtReasoneId LEFT OUTER JOIN"

            _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp AS C1 WITH (NOLOCK) ON M.FNHSysCmpId = C1.FNHSysCmpId  LEFT OUTER JOIN"
            _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp AS C2 WITH (NOLOCK) ON M.FNHSysCmpIdTo = C2.FNHSysCmpId  "


            _Qry &= vbCrLf & "  WHERE M.FNHSysEmpID=" & Val(Key) & " "
            _Qry &= vbCrLf & " ORDER BY  M.FNSeq Desc , M.FTEffectiveDate Desc "

            Me.ogc.DataSource = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ocmclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmclear.Click
        HI.TL.HandlerControl.ClearControl(Me)
    End Sub

    Private Sub ocmsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmsave.Click
        If Me.VerrifyData(True) Then
            Dim _Spls As New HI.TL.SplashScreen("Saving...   Please Wait   ")
            If Me.SaveData() Then
                _Spls.Close()
                HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                Me.LoadHistory(Me.FNHSysEmpID.Properties.Tag.ToString)
            Else
                _Spls.Close()
                HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
            End If
        End If
    End Sub

    Private Function VerrifyData(Optional ByVal _SaveData As Boolean = False) As Boolean

        Dim _Pass As Boolean = False

        If FNHSysEmpID.Text <> "" And FNHSysEmpID.Properties.Tag.ToString <> "" Then
            If FNHSysCmpIdTo.Text <> "" And FNHSysCmpIdTo.Properties.Tag.ToString <> "" Then
                If FNHSysEmpTypeToId.Text <> "" And FNHSysEmpTypeToId.Properties.Tag.ToString <> "" Then
                    If FNHSysPmtReasoneId.Text <> "" And FNHSysPmtReasoneId.Properties.Tag.ToString <> "" Then
                        If HI.UL.ULDate.CheckDate(FTEffectiveDate.Text) <> "" Then

                            If _SaveData Then
                                _Pass = True
                            Else
                                Dim _Qry As String = ""
                                _Qry = " SELECT TOP 1 ISNULL(FTStateActive,'') As FTStateActive FROM THRTEmployeeMasterChange WITH(NOLOCK) "
                                _Qry &= vbCrLf & " WHERE  FNHSysEmpID=" & Val(FNHSysEmpID.Properties.Tag.ToString) & " "
                                _Qry &= vbCrLf & "  AND FNSeq=" & FNSeq.Value & "  "

                                If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "") = "" Then
                                    _Pass = True
                                Else
                                    HI.MG.ShowMsg.mProcessError(1005280001, "", Me.Text, System.Windows.Forms.MessageBoxIcon.Warning)
                                End If
                            End If


                        Else
                            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FTEffectiveDate_lbl.Text)
                            FTEffectiveDate.Focus()
                        End If
                    Else
                        HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FNHSysPmtReasoneId_lbl.Text)
                        FNHSysPmtReasoneId.Focus()
                    End If

                Else
                    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FNHSysEmpTypeIdTo_lbl.Text)
                    FNHSysEmpTypeToId.Focus()
                End If
            Else
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FNHSysCmpIdTo_lbl.Text)
                FNHSysCmpIdTo.Focus()
            End If

        Else
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FNHSysEmpID_lbl.Text)
            FNHSysEmpID.Focus()
        End If

        Return _Pass

    End Function

    Private Function SaveData() As Boolean
        Dim _Qry As String = ""


        _Qry = " SELECT TOP 1 ISNULL(FTStateActive,'') As FTStateActive FROM THRTEmployeeMasterChange WITH(NOLOCK) "
        _Qry &= vbCrLf & " WHERE  FNHSysEmpID=" & Val(FNHSysEmpID.Properties.Tag.ToString) & " "
        _Qry &= vbCrLf & "  AND FNSeq=" & FNSeq.Value & "  "

        If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "") <> "" Then
            FNSeq.Value = 0
        End If

        HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_HR)
        HI.Conn.SQLConn.SqlConnectionOpen()
        HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
        HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

        Try
            _Qry = "UPDATE THRTEmployeeMasterChange SET "
            _Qry &= vbCrLf & " FNHSysEmpTypeId=" & Val(FNHSysEmpTypeId.Properties.Tag.ToString) & " "
            _Qry &= vbCrLf & ", FNHSysDeptId=" & Val(FNHSysDeptId.Properties.Tag.ToString) & " "
            _Qry &= vbCrLf & ", FNHSysDivisonId=" & Val(FNHSysDivisonId.Properties.Tag.ToString) & " "
            _Qry &= vbCrLf & ", FNHSysSectId=" & Val(FNHSysSectId.Properties.Tag.ToString) & " "
            _Qry &= vbCrLf & ", FNHSysUnitSectId=" & Val(FNHSysUnitSectId.Properties.Tag.ToString) & " "
            _Qry &= vbCrLf & ", FNHSysPositId=" & Val(FNHSysPositId.Properties.Tag.ToString) & " "
            _Qry &= vbCrLf & ", FNHSysEmpTypeIdTo=" & Val(FNHSysEmpTypeToId.Properties.Tag.ToString) & " "
            _Qry &= vbCrLf & ", FNHSysDeptIdTo=" & Val(FNHSysDeptIdTo.Properties.Tag.ToString) & " "
            _Qry &= vbCrLf & ", FNHSysDivisonIdTo=" & Val(FNHSysDivisonIdTo.Properties.Tag.ToString) & " "
            _Qry &= vbCrLf & ", FNHSysSectIdTo=" & Val(FNHSysSectIdTo.Properties.Tag.ToString) & " "
            _Qry &= vbCrLf & ", FNHSysUnitSectIdTo=" & Val(FNHSysUnitSectIdTo.Properties.Tag.ToString) & " "
            _Qry &= vbCrLf & ", FNHSysPositIdTo=" & Val(FNHSysPositIdTo.Properties.Tag.ToString) & " "
            _Qry &= vbCrLf & ", FTNote=N'" & HI.UL.ULF.rpQuoted(FTNote.Text) & "' "
            _Qry &= vbCrLf & ",  FNHSysPmtReasoneId=" & Val(FNHSysPmtReasoneId.Properties.Tag.ToString) & " "
            _Qry &= vbCrLf & ", FTEffectiveDate='" & HI.UL.ULDate.ConvertEnDB(FTEffectiveDate.Text) & "' "
            _Qry &= vbCrLf & ",FTUpdUser = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            _Qry &= vbCrLf & ",FTUpdDate = " & HI.UL.ULDate.FormatDateDB & ""
            _Qry &= vbCrLf & ",FTUpdTime = " & HI.UL.ULDate.FormatTimeDB & ""
            _Qry &= vbCrLf & ",  FNHSysCmpId=" & Val(FNHSysCmpId.Properties.Tag.ToString) & " "
            _Qry &= vbCrLf & ",  FNHSysCmpIdTo=" & Val(FNHSysCmpIdTo.Properties.Tag.ToString) & " "
            _Qry &= vbCrLf & " WHERE  FNHSysEmpID=" & Val(FNHSysEmpID.Properties.Tag.ToString) & " "
            _Qry &= vbCrLf & "  AND FNSeq=" & FNSeq.Value & "  "
            Dim tSeqNo As Integer

            If HI.Conn.SQLConn.ExecuteTran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                _Qry = "SELECT MAX(FNSeq) AS FNSeqNo FROM THRTEmployeeMasterChange WHERE  FNHSysEmpID=" & Val(FNHSysEmpID.Properties.Tag.ToString) & " "

                tSeqNo = HI.Conn.SQLConn.GetFieldOnBeginTrans(_Qry, Conn.DB.DataBaseName.DB_HR, "0")
                tSeqNo = Val(tSeqNo) + 1

                _Qry = "INSERT INTO THRTEmployeeMasterChange( FNHSysEmpID,FNSeq,  FNHSysEmpTypeId, FNHSysDeptId, "
                _Qry &= vbCrLf & " FNHSysDivisonId, FNHSysSectId, FNHSysUnitSectId, FNHSysPositId, FNHSysEmpTypeIdTo, FNHSysDeptIdTo, FNHSysDivisonIdTo, FNHSysSectIdTo, "
                _Qry &= vbCrLf & " FNHSysUnitSectIdTo, FNHSysPositIdTo, FTNote, FTEffectiveDate, FNHSysPmtReasoneId "
                _Qry &= vbCrLf & ", FTInsUser, FTInsDate, FTInsTime, FNHSysCmpId, FNHSysCmpIdTo)  "
                _Qry &= vbCrLf & " SELECT " & Val(FNHSysEmpID.Properties.Tag.ToString) & "," & tSeqNo & ""
                _Qry &= vbCrLf & " ," & Val(FNHSysEmpTypeId.Properties.Tag.ToString) & " "
                _Qry &= vbCrLf & ", " & Val(FNHSysDeptId.Properties.Tag.ToString) & " "
                _Qry &= vbCrLf & ", " & Val(FNHSysDivisonId.Properties.Tag.ToString) & " "
                _Qry &= vbCrLf & ", " & Val(FNHSysSectId.Properties.Tag.ToString) & " "
                _Qry &= vbCrLf & ", " & Val(FNHSysUnitSectId.Properties.Tag.ToString) & " "
                _Qry &= vbCrLf & ", " & Val(FNHSysPositId.Properties.Tag.ToString) & " "
                _Qry &= vbCrLf & ", " & Val(FNHSysEmpTypeToId.Properties.Tag.ToString) & " "
                _Qry &= vbCrLf & ", " & Val(FNHSysDeptIdTo.Properties.Tag.ToString) & " "
                _Qry &= vbCrLf & ", " & Val(FNHSysDivisonIdTo.Properties.Tag.ToString) & " "
                _Qry &= vbCrLf & ", " & Val(FNHSysSectIdTo.Properties.Tag.ToString) & " "
                _Qry &= vbCrLf & ", " & Val(FNHSysUnitSectIdTo.Properties.Tag.ToString) & " "
                _Qry &= vbCrLf & ", " & Val(FNHSysPositIdTo.Properties.Tag.ToString) & " "
                _Qry &= vbCrLf & ", N'" & HI.UL.ULF.rpQuoted(FTNote.Text) & "' "
                _Qry &= vbCrLf & ",'" & HI.UL.ULDate.ConvertEnDB(FTEffectiveDate.Text) & "' "
                _Qry &= vbCrLf & ", " & Val(FNHSysPmtReasoneId.Properties.Tag.ToString) & " "
                _Qry &= vbCrLf & ",N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB

                _Qry &= vbCrLf & ", " & Val(FNHSysCmpId.Properties.Tag.ToString) & " "
                _Qry &= vbCrLf & ", " & Val(FNHSysCmpIdTo.Properties.Tag.ToString) & " "


                If HI.Conn.SQLConn.ExecuteTran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    Return False
                End If

            End If

            _Qry = "UPDATE THRTEmployeeMasterChange SET FNSeq=FNNo"
            _Qry &= vbCrLf & " FROM THRTEmployeeMasterChange INNER JOIN "
            _Qry &= vbCrLf & "(SELECT ROW_NUMBER() OVER(ORDER BY FTEffectiveDate) AS FNNo, FNSeq,FNHSysEmpId"
            _Qry &= vbCrLf & " FROM THRTEmployeeMasterChange WHERE  FNHSysEmpId=" & Val(FNHSysEmpID.Properties.Tag.ToString) & " "
            _Qry &= vbCrLf & ") T1 ON THRTEmployeeMasterChange.FNSeq=T1.FNSeq AND THRTEmployeeMasterChange.FNHSysEmpId=T1.FNHSysEmpId"

            HI.Conn.SQLConn.ExecuteTran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            HI.Conn.SQLConn.ExecuteNonQuery(" EXEC SP_UPDATE_POS_CMP ", Conn.DB.DataBaseName.DB_HR)

            Return True

        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return False
        End Try
    End Function

    Private Function DeleteData() As Boolean
        Dim _Qry As String = ""

        HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_HR)
        HI.Conn.SQLConn.SqlConnectionOpen()
        HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
        HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

        Try
            _Qry = " Delete THRTEmployeeMasterChange "
            _Qry &= vbCrLf & " WHERE  FNHSysEmpID=" & Val(FNHSysEmpID.Properties.Tag.ToString) & " "
            _Qry &= vbCrLf & "  AND FNSeq=" & FNSeq.Value & "  "
            HI.Conn.SQLConn.ExecuteTran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)


            _Qry = "UPDATE THRTEmployeeMasterChange SET FNSeq=FNNo"
            _Qry &= vbCrLf & " FROM THRTEmployeeMasterChange INNER JOIN "
            _Qry &= vbCrLf & "(SELECT ROW_NUMBER() OVER(ORDER BY FTEffectiveDate) AS FNNo, FNSeq,FNHSysEmpId"
            _Qry &= vbCrLf & " FROM THRTEmployeeMasterChange WHERE  FNHSysEmpId=" & Val(FNHSysEmpID.Properties.Tag.ToString) & " "
            _Qry &= vbCrLf & ") T1 ON THRTEmployeeMasterChange.FNSeq=T1.FNSeq AND THRTEmployeeMasterChange.FNHSysEmpId=T1.FNHSysEmpId"

            HI.Conn.SQLConn.ExecuteTran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            HI.Conn.SQLConn.ExecuteNonQuery(" EXEC SP_UPDATE_POS ", Conn.DB.DataBaseName.DB_HR)

            Return True

        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return False
        End Try
    End Function
#End Region

#Region "General"

    Private Sub ogv_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ogv.DoubleClick
        With ogv
            If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount Then Exit Sub
            Try
                FTEffectiveDate.DateTime = HI.UL.ULDate.ConvertEnDB("" & .GetRowCellValue(.FocusedRowHandle, "FTEffectiveDate").ToString)
            Catch ex As Exception
                FTEffectiveDate.DateTime = Nothing
            End Try
            FNHSysCmpIdTo.Text = "" & .GetRowCellValue(.FocusedRowHandle, "FTCmpCodeTo").ToString
            FNHSysEmpTypeToId.Text = "" & .GetRowCellValue(.FocusedRowHandle, "FTEmpTypeCode2").ToString
            FNHSysSectIdTo.Text = "" & .GetRowCellValue(.FocusedRowHandle, "FTSectCode2").ToString
            FNHSysDeptIdTo.Text = "" & .GetRowCellValue(.FocusedRowHandle, "FTDeptCode2").ToString
            FNHSysUnitSectIdTo.Text = "" & .GetRowCellValue(.FocusedRowHandle, "FTUnitSectCode2").ToString
            FNHSysDivisonIdTo.Text = "" & .GetRowCellValue(.FocusedRowHandle, "FTDivisonCode2").ToString
            FNHSysPositIdTo.Text = "" & .GetRowCellValue(.FocusedRowHandle, "FTPositCode2").ToString
            FNHSysPmtReasoneId.Text = "" & .GetRowCellValue(.FocusedRowHandle, "FTPmtReasonCode").ToString
            FTNote.Text = "" & .GetRowCellValue(.FocusedRowHandle, "FTNote").ToString
            FNSeq.Value = Val("" & .GetRowCellValue(.FocusedRowHandle, "FNSeq").ToString)

        End With
    End Sub


    Private Sub ocmdelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmdelete.Click
        If Me.VerrifyData Then
            Dim _Spls As New HI.TL.SplashScreen("Deleting...   Please Wait   ")
            If Me.DeleteData() Then
                _Spls.Close()
                HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                Me.LoadHistory(Me.FNHSysEmpID.Properties.Tag.ToString)
            Else
                _Spls.Close()
                HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
            End If
        End If
    End Sub

    Private Sub wChangePosition_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Me.FNHSysCmpId.Text = HI.ST.SysInfo.CmpCode
        Call HI.UL.AppRegistry.LoadLayoutGridFromRegistry(Me, Me.ogv)
    End Sub

#End Region

    Private Sub ocmsavelayout_Click(sender As Object, e As EventArgs) Handles ocmsavelayout.Click
        HI.UL.AppRegistry.SaveLayoutGridToRegistry(Me, Me.ogv)
        HI.MG.ShowMsg.mInfo("Save Layout Grid Complete...", 1404240001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Information)
    End Sub

    Private Sub FNHSysCmpIdTo_EditValueChanged(sender As Object, e As EventArgs) Handles FNHSysCmpIdTo.EditValueChanged
        Try
            FNHSysEmpTypeToId.Text = ""
            FNHSysSectIdTo.Text = ""
            FNHSysDeptIdTo.Text = ""
            FNHSysUnitSectIdTo.Text = ""
            FNHSysDivisonIdTo.Text = ""
            FNHSysPositIdTo.Text = ""
        Catch ex As Exception

        End Try
    End Sub
End Class
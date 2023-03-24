Imports System.Data.SqlClient
Imports System.IO

Public Class wUser

    Private _HSysID As Integer = 0
    Public Property HSysID() As Integer
        Get
            Return _HSysID
        End Get
        Set(value As Integer)
            _HSysID = value
        End Set
    End Property

    Private _ProcComplete As Boolean = False
    Public Property ProcComplete() As Boolean
        Get
            Return _ProcComplete
        End Get
        Set(value As Boolean)
            _ProcComplete = value
        End Set
    End Property

    Private Sub ocmexit_Click(sender As System.Object, e As System.EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ocmsave_Click(sender As System.Object, e As System.EventArgs) Handles ocmsave.Click
        If Me.VerifyData() Then
            Dim _Str As String = ""
            Dim _pass As Boolean = True

            If Me.FTUserName.Properties.ReadOnly = False Then
                _Str = "SELECT TOP  1  FTUserName FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEUserLogin WHERE FTUserName='" & HI.UL.ULF.rpQuoted(Me.FTUserName.Text.Trim) & "' "

                If HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_SECURITY, "") <> "" Then
                    HI.MG.ShowMsg.mInfo("", 1306070001, Me.Text)
                    _pass = False
                End If

            End If

            If (_pass) Then
                Dim _NewPass As String = HI.Conn.DB.FuncEncryptData(FTPassword.Text.Trim)
                _Str = "SELECT TOP  1  FTUserName FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEUserLogin WHERE FTUserName='" & HI.UL.ULF.rpQuoted(Me.FTUserName.Text.Trim) & "' "

                If HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_SECURITY, "") <> "" Then

                    _Str = " Update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEUserLogin SET "
                    _Str &= vbCrLf & " FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                    _Str &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB & " "
                    _Str &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & " "
                    _Str &= vbCrLf & ", FTPassword=N'" & HI.UL.ULF.rpQuoted(_NewPass) & "' "
                    _Str &= vbCrLf & ", FTUserDescriptionTH='" & HI.UL.ULF.rpQuoted(FTUserDescriptionTH.Text) & "'  "
                    _Str &= vbCrLf & ", FTUserDescriptionEN='" & HI.UL.ULF.rpQuoted(FTUserDescriptionEN.Text) & "' "
                    _Str &= vbCrLf & ", FTStateActive='" & FTStateActive.EditValue.ToString & "'  "
                    _Str &= vbCrLf & ", FNHSysMerTeamId=" & Val(FNHSysMerTeamId.Properties.Tag.ToString) & " "
                    _Str &= vbCrLf & ", FNHSysTeamGrpId=" & Val(FNHSysTeamGrpId.Properties.Tag.ToString) & " "
                    _Str &= vbCrLf & ", FNHSysTeamGrpIdTo=" & Val(FNHSysTeamGrpIdTo.Properties.Tag.ToString) & " "
                    _Str &= vbCrLf & ", FNHSysEmpId=" & Val(FNHSysEmpId.Properties.Tag.ToString) & " "
                    _Str &= vbCrLf & ",FTStateHelp='" & FTStateHelp.EditValue.ToString & "'  "
                    _Str &= vbCrLf & ", FNHSysDirectorGrpId=" & Val(FNHSysDirectorGrpId.Properties.Tag.ToString) & " "
                    _Str &= vbCrLf & ", FTManagerUserName='" & HI.UL.ULF.rpQuoted(FTManagerUserName.Text) & "'  "
                    _Str &= vbCrLf & ", FTStateMobileWH='" & FTStateMobileWH.EditValue.ToString & "'  "
                    _Str &= vbCrLf & ", FTStateMobileWHFG='" & FTStateMobileWHFG.EditValue.ToString & "'  "
                    _Str &= vbCrLf & ", FTUserAD='" & HI.UL.ULF.rpQuoted(FTUserAD.Text) & "' "
                    _Str &= vbCrLf & " WHERE FTUserName='" & HI.UL.ULF.rpQuoted(Me.FTUserName.Text.Trim) & "' "

                Else

                    _Str = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEUserLogin("
                    _Str &= vbCrLf & "FTInsUser, FDInsDate, FTInsTime, FTUserName"
                    _Str &= vbCrLf & ", FTPassword, FTUserDescriptionTH, FTUserDescriptionEN, FTStateActive,FNHSysMerTeamId,FNHSysTeamGrpId,FNHSysEmpId,FTStateHelp,FNHSysDirectorGrpId,FTManagerUserName,FNHSysTeamGrpIdTo,FTStateMobileWH,FTStateMobileWHFG,FTUserAD) "
                    _Str &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                    _Str &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & " "
                    _Str &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ",'" & HI.UL.ULF.rpQuoted(Me.FTUserName.Text.Trim) & "' "
                    _Str &= vbCrLf & ", N'" & HI.UL.ULF.rpQuoted(_NewPass) & "' "
                    _Str &= vbCrLf & ", '" & HI.UL.ULF.rpQuoted(FTUserDescriptionTH.Text) & "'  "
                    _Str &= vbCrLf & ", '" & HI.UL.ULF.rpQuoted(FTUserDescriptionEN.Text) & "' "
                    _Str &= vbCrLf & ", '" & FTStateActive.EditValue.ToString & "'  "
                    _Str &= vbCrLf & ", " & Val(FNHSysMerTeamId.Properties.Tag.ToString) & "  "
                    _Str &= vbCrLf & ", " & Val(FNHSysTeamGrpId.Properties.Tag.ToString) & "  "
                    _Str &= vbCrLf & ", " & Val(FNHSysEmpId.Properties.Tag.ToString) & "  "
                    _Str &= vbCrLf & ", '" & FTStateHelp.EditValue.ToString & "'  "
                    _Str &= vbCrLf & ", " & Val(FNHSysDirectorGrpId.Properties.Tag.ToString) & "  "
                    _Str &= vbCrLf & ", '" & HI.UL.ULF.rpQuoted(FTManagerUserName.Text) & "'  "
                    _Str &= vbCrLf & ", " & Val(FNHSysTeamGrpIdTo.Properties.Tag.ToString) & "  "
                    _Str &= vbCrLf & ", '" & FTStateMobileWH.EditValue.ToString & "'  "
                    _Str &= vbCrLf & ", '" & FTStateMobileWHFG.EditValue.ToString & "'  "
                    _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(FTUserAD.Text) & "' "

                End If

                HI.Conn.SQLConn.ExecuteNonQuery(_Str, Conn.DB.DataBaseName.DB_SECURITY)

                HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(HI.Conn.DB.DataBaseName.DB_SECURITY)
                HI.Conn.SQLConn.SqlConnectionOpen()


                _Str = " Update  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEUserLogin "
                _Str &= vbCrLf & "  SET FPUserImage=@FPUserImage"
                _Str &= vbCrLf & " ,FPUserLicense=@FPUserLicense "
                _Str &= vbCrLf & " WHERE FTUserName=@FTUserName "

                Dim cmd As New SqlCommand(_Str, HI.Conn.SQLConn.Cnn)
                cmd.Parameters.AddWithValue("@FTUserName", FTUserName.Text)

                'Dim ms As New MemoryStream()
                'Me.FTUserPic.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg)
                Dim data As Byte() = HI.UL.ULImage.ConvertImageToByteArray(Me.FTUserPic.Image, UL.ULImage.PicType.Employee) ' ms.GetBuffer()

                Dim p As New SqlParameter("@FPUserImage", SqlDbType.Image)
                p.Value = data

                cmd.Parameters.Add(p)

                Dim data2 As Byte() = HI.UL.ULImage.ConvertImageToByteArray(Me.FTUserLicense.Image, UL.ULImage.PicType.License) 'ms2.GetBuffer()
                Dim p2 As New SqlParameter("@FPUserLicense", SqlDbType.Image)
                p2.Value = data2

                cmd.Parameters.Add(p2)

                cmd.ExecuteNonQuery()
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cnn)

                If Me.otbuser.SelectedTabPage.Name = otpuser.Name Then


                Else
                    _Str = "DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEUserLoginPermission WHERE FTUserName='" & HI.UL.ULF.rpQuoted(Me.FTUserName.Text.Trim) & "' "
                    HI.Conn.SQLConn.ExecuteNonQuery(_Str, Conn.DB.DataBaseName.DB_SECURITY)

                    If Not (ogd.DataSource Is Nothing) Then
                        ogv.FocusedRowHandle = 0
                        ogv.FocusedColumn = ogv.Columns.ColumnByName("FTRoleName")

                        For Each R As DataRow In CType(ogd.DataSource, DataTable).Rows
                            If R!FTSelect.ToString = "1" Then
                                _Str = "INSERT INTO TSEUserLoginPermission( FTInsUser, FDInsDate, FTInsTime, FTUserName, FNHSysPermissionID) "
                                _Str &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                                _Str &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & " "
                                _Str &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ",'" & HI.UL.ULF.rpQuoted(Me.FTUserName.Text.Trim) & "' "
                                _Str &= vbCrLf & "," & Val(R!FNHSysPermissionID.ToString) & " "

                                HI.Conn.SQLConn.ExecuteNonQuery(_Str, Conn.DB.DataBaseName.DB_SECURITY)

                            End If
                        Next
                    End If


                End If

                FTUserName.Properties.ReadOnly = True
            End If

            ProcComplete = True
            HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)

        End If
    End Sub

    Private Function VerifyData()
        Dim _Pass As Boolean = False
        If Me.FTUserName.Text.Trim <> "" Then
            If Me.FTUserDescriptionTH.Text.Trim <> "" Then
                If Me.FTUserDescriptionEN.Text.Trim() <> "" Then

                    If Me.FTPassword.Text <> "" Then
                        If Me.FTPasswordRe.Text <> "" Then
                            If Me.FTPassword.Text = Me.FTPasswordRe.Text Then
                                ' If HI.Conn.DB.FuncDecryptData(HI.Conn.DB.FuncEncryptData(Me.FTPassword.Text)) = Me.FTPassword.Text Then
                                _Pass = True
                                'Else
                                '  HI.MG.ShowMsg.mInfo("ไม่สามารถทำการระบุข้อมูล รหัสผ่าน ที่มีพยัญชนะหรือมีอักษรนี้ได้ !!!", 1406260077, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
                                'End If
                            Else
                                MsgBox("Password Not Match ... !!!")
                                Me.FTPasswordRe.Focus()
                                Me.FTPasswordRe.SelectAll()
                            End If
                        Else
                            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FTPasswordRe_lbl.Text)
                            Me.FTPasswordRe.Focus()
                            Me.FTPasswordRe.SelectAll()
                        End If
                    Else
                        HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FTPassword_lbl.Text)
                        Me.FTPassword.Focus()
                    End If
                Else
                    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FTUserDescriptionEN_lbl.Text)
                    Me.FTUserDescriptionEN.Focus()
                End If
            Else
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FTUserDescriptionTH_lbl.Text)
                Me.FTUserDescriptionTH.Focus()
            End If
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FTUserName_lbl.Text)
            Me.FTUserName.Focus()
        End If

        Return _Pass
    End Function

    Private Sub wUser_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Me.FTStateActive.Checked = False

        Call LoadPermission()

        If FTUserName.Text <> "" And FTUserName.Properties.ReadOnly Then
            Call LoadUserInfo()
        End If

        otbuser.SelectedTabPageIndex = 0
    End Sub

    Private Sub LoadPermission()
        Dim _Str As String

        _Str = "SELECT  ISNULL((SELECT TOP 1 '1' FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEUserLoginPermission WHERE FNHSysPermissionID=R.FNHSysPermissionID AND FTUserName='" & HI.UL.ULF.rpQuoted(FTUserName.Text.Trim) & "' AND FTUserName<>'' ),'0') AS FTSelect "
        _Str &= vbCrLf & "  , R.FNHSysPermissionID, R.FTPermissionCode"

        'If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
        _Str &= vbCrLf & " , R.FTPermissionNameTH AS FTRoleName"
        ' Else
        _Str &= vbCrLf & " , R.FTPermissionNameEN As FTRoleNameEN "
        ' End If

        _Str &= vbCrLf & " FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEPermission AS R"
        _Str &= vbCrLf & " ORDER BY R.FTPermissionCode "

        Me.ogd.DataSource = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_SECURITY)

    End Sub

    Private Sub LoadUserInfo()
        Dim _Str As String

        _Str = "SELECT TOP 1  A.* ,B.FTMerTeamCode ,C.FTTeamGrpCode ,CC2.FTTeamGrpCode AS FTTeamGrpCode2  "
        _Str &= vbCrLf & " ,ISNULL(Emp.FTEmpCode,'') AS FTEmpCode"
        _Str &= vbCrLf & " ,ISNULL(Emp.FNEmpStatus,0) AS FNEmpStatus"

        _Str &= vbCrLf & " ,ISNULL((SELECT TOP 1 H.FTDirectorGrpCode"
        _Str &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDirectorGrp AS H WITH(NOLOCK) "
        _Str &= vbCrLf & "  WHERE H.FNHSysDirectorGrpId=A.FNHSysDirectorGrpId"
        _Str &= vbCrLf & " ),'') AS FNHSysDirectorGrpIdCode"

        _Str &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEUserLogin AS A WITH(NOLOCK) "
        _Str &= vbCrLf & " LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMerTeam AS B WITH(NOLOCK) ON A.FNHSysMerTeamId = B.FNHSysMerTeamId "
        _Str &= vbCrLf & " LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMTeamGrp AS C WITH(NOLOCK) ON A.FNHSysTeamGrpId = C.FNHSysTeamGrpId "
        _Str &= vbCrLf & " LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMTeamGrp AS CC2 WITH(NOLOCK) ON A.FNHSysTeamGrpIdTo = CC2.FNHSysTeamGrpId "


        _Str &= vbCrLf & " OUTER APPLY ( SELECT TOP 1 EH.FTEmpCode ,EH.FNEmpStatus FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS EH WITH(NOLOCK) WHERE EH.FNHSysEmpID=A.FNHSysEmpID ) AS Emp "
        _Str &= vbCrLf & "  WHERE A.FTUserName='" & HI.UL.ULF.rpQuoted(FTUserName.Text.Trim) & "'  "
        Dim _dt As DataTable = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_SECURITY)

        For Each R As DataRow In _dt.Rows

            FTUserDescriptionTH.Text = R!FTUserDescriptionTH.ToString
            FTUserDescriptionEN.Text = R!FTUserDescriptionEN.ToString
            FTStateActive.Checked = (R!FTStateActive.ToString = "1")

            FTUserAD.Text = R!FTUserAD.ToString
            FTStateMobileWH.Checked = (R!FTStateMobileWH.ToString = "1")
            FTStateMobileWHFG.Checked = (R!FTStateMobileWHFG.ToString = "1")

            FTStateHelp.Checked = (R!FTStateHelp.ToString = "1")

            FTPassword.Text = HI.Conn.DB.FuncDecryptData(R!FTPassword.ToString)
            FTPasswordRe.Text = HI.Conn.DB.FuncDecryptData(R!FTPassword.ToString)

            FNHSysMerTeamId.Text = R!FTMerTeamCode.ToString
            FNHSysTeamGrpId.Text = R!FTTeamGrpCode.ToString
            FNHSysTeamGrpIdTo.Text = R!FTTeamGrpCode2.ToString

            FNHSysEmpId.Text = R!FTEmpCode.ToString

            FTManagerUserName.Text = R!FTManagerUserName.ToString

            FNHSysDirectorGrpId.Text = R!FNHSysDirectorGrpIdCode.ToString


            Try
                FTUserPic.Image = HI.UL.ULImage.ConvertByteArrayToImmage(R!FPUserImage)
            Catch ex As Exception
            End Try

            Try
                FTUserLicense.Image = HI.UL.ULImage.ConvertByteArrayToImmage(R!FPUserLicense)
            Catch ex As Exception
            End Try

        Next

    End Sub

    Private Sub FTPassword_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles FTPassword.KeyPress, FTPasswordRe.KeyPress
        Select Case Asc(e.KeyChar)
            Case 48 To 122 ' โค๊ดภาษาอังกฤษ์ตามจริงจะอยู่ที่ 58ถึง122 แต่ที่เอา 48มาเพราะเราต้องการตัวเลข
                e.Handled = False
            Case 8, 13, 46 ' Backspace = 8, Enter = 13, Delete = 46
                e.Handled = False
            Case Else
                e.Handled = True
                HI.MG.ShowMsg.mInfo("กรุณาระบุข้อมูลเป็นภาษาอังกฤษ หรือ ตัวเลบ เท่านั้น !!!", 1501010098, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
        End Select
    End Sub

    Private Sub FTPassword_EditValueChanged(sender As Object, e As EventArgs) Handles FTPassword.EditValueChanged

    End Sub
End Class
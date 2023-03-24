Imports System.Data.SqlClient

Public Class wUserChangePassword

    Private Function VerifyData()
        Dim _Pass As Boolean = False


        If otbpldpassword.Text <> "" Then
            If otbpldpassword.Text = HI.ST.UserInfo.UserPassword Then
                If Me.FTPassword.Text <> "" Then
                    If Me.FTPasswordRe.Text <> "" Then
                        If Me.FTPassword.Text = Me.FTPasswordRe.Text Then
                            _Pass = True

                            'If HI.Conn.DB.FuncDecryptData(HI.Conn.DB.FuncEncryptData(Me.FTPassword.Text)) = Me.FTPassword.Text Then
                            '    _Pass = True
                            'Else
                            '    HI.MG.ShowMsg.mInfo("ไม่สามารถทำการระบุข้อมูล รหัสผ่าน ที่มีพยัญชนะหรือมีอักษรนี้ได้ !!!", 1406260077, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
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
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, otbpldpassword_lbl.Text & " Invalid !!! ")
                Me.otbpldpassword.Focus()
                Me.otbpldpassword.SelectAll()
            End If
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, otbpldpassword_lbl.Text)
            Me.otbpldpassword.Focus()
            Me.otbpldpassword.SelectAll()
        End If

        Return _Pass
    End Function

    Private Sub ocmexit_Click(sender As System.Object, e As System.EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ocmsave_Click(sender As System.Object, e As System.EventArgs) Handles ocmsave.Click
        If Me.VerifyData() Then
            Dim _pass As Boolean = True
            If (_pass) Then
                Dim _Str As String
                Dim _NewPass As String = HI.Conn.DB.FuncEncryptData(FTPassword.Text.Trim)
                _Str = " Update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEUserLogin SET "
                _Str &= vbCrLf & " FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                _Str &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB & " "
                _Str &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & " "
                _Str &= vbCrLf & ", FTPassword=N'" & HI.UL.ULF.rpQuoted(_NewPass) & "' "
                _Str &= vbCrLf & ", FTNickName=N'" & HI.UL.ULF.rpQuoted(FTNickName.Text.Trim()) & "' "
                _Str &= vbCrLf & ", FTTel=N'" & HI.UL.ULF.rpQuoted(FTTel.Text.Trim()) & "' "
                _Str &= vbCrLf & ", FTEmail=N'" & HI.UL.ULF.rpQuoted(FTEmail.Text.Trim()) & "' "
                _Str &= vbCrLf & ", FTFax=N'" & HI.UL.ULF.rpQuoted(FTFax.Text.Trim()) & "' "

                _Str &= vbCrLf & " WHERE FTUserName='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "

                HI.Conn.SQLConn.ExecuteNonQuery(_Str, Conn.DB.DataBaseName.DB_SECURITY)
                HI.ST.UserInfo.UserPassword = Me.FTPassword.Text

                HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)

            End If
        End If
    End Sub

    Private Sub wUserChangePassword_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        otbpldpassword.Focus()
    End Sub

    Private Sub otbpldpassword_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles otbpldpassword.KeyPress, FTPassword.KeyPress, FTPasswordRe.KeyPress
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

    Private Sub wUserChangePassword_Load(sender As Object, e As EventArgs) Handles Me.Load

        Dim _Str As String
        Dim dt As DataTable
        _Str = " SELECT TOP 1 FTNickName"
        _Str &= vbCrLf & ", FTTel"
        _Str &= vbCrLf & ", FTEmail "
        _Str &= vbCrLf & ", FTFax "
        _Str &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEUserLogin WITH(NOLOCK) "

        _Str &= vbCrLf & " WHERE FTUserName='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
        dt = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_SECURITY)

        For Each R As DataRow In dt.Rows
            FTNickName.Text = R!FTNickName.ToString()
            FTTel.Text = R!FTTel.ToString()
            FTEmail.Text = R!FTEmail.ToString()
            FTFax.Text = R!FTFax.ToString()

            Exit For
        Next

    End Sub
End Class
Friend Class wUserLogIN

    Private _Confirm As Boolean
    Public Function GetLogin(ByRef LoginUser As String, ByRef Password As String, ByVal LoginCount As Integer) As Boolean
        Try
            _Confirm = False
            Me.ShowDialog()
            LoginUser = otbLogin.Text.Trim
            Password = otbPassword.Text.Trim
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return _Confirm

    End Function

    Private Sub otbPassword_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles otbPassword.KeyDown
        Select Case e.KeyCode
            Case Windows.Forms.Keys.Enter
                If otbLogin.Text.Trim = "" Then
                    Beep()
                    otbLogin.Focus()
                    Exit Sub
                End If

                If otbPassword.Text.Trim = "" Then
                    Beep()
                    otbPassword.Focus()
                    Exit Sub
                End If

                _Confirm = True
                Me.Close()
            Case Windows.Forms.Keys.Escape
                _Confirm = False
                Me.Close()
        End Select
    End Sub

    Private Sub wLogin_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Windows.Forms.Keys.Enter
                If otbLogin.Text.Trim = "" Then
                    Beep()
                    otbLogin.Focus()
                    Exit Sub
                End If

                If otbPassword.Text.Trim = "" Then
                    Beep()
                    otbPassword.Focus()
                    Exit Sub
                End If

                _Confirm = True
                Me.Close()
            Case Windows.Forms.Keys.Escape
                _Confirm = False
                Me.Close()
        End Select
    End Sub

    Private Sub otbPassword_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles otbPassword.EditValueChanged

    End Sub
End Class
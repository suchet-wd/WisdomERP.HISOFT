Public Class wGenerator

    Private Sub otb1_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles otb1.KeyDown
        Select Case e.KeyCode
            Case Keys.Enter
                If otb1.Text.Trim <> "" Then
                    If TextBox1.Text = "[vdw,jwfh" Then
                        otb2.Text = HI.Conn.DB.FuncEncryptDataServer(otb1.Text.Trim)
                    Else
                        otb2.Text = HI.Conn.DB.FuncEncryptData(otb1.Text.Trim)
                    End If

                End If
        End Select
    End Sub

    Private Sub otb2_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles otb2.KeyDown
        Select Case e.KeyCode
            Case Keys.Enter
                If otb2.Text.Trim <> "" Then
                    If TextBox1.Text = "[vdw,jwfh" Then
                        otb1.Text = HI.Conn.DB.FuncDecryptDataServer(otb2.Text.Trim)
                    Else
                        otb1.Text = HI.Conn.DB.FuncDecryptData(otb2.Text.Trim)
                    End If

                End If
        End Select
    End Sub


    Private Sub otb2_TextChanged(sender As System.Object, e As System.EventArgs) Handles otb2.TextChanged

    End Sub

    Private Sub otb1_TextChanged(sender As Object, e As EventArgs) Handles otb1.TextChanged

    End Sub
End Class

Friend Class wInputUserAndPassword

    Public bW_Confirm As Boolean
    Private Sub obtOk_Click(sender As System.Object, e As System.EventArgs) Handles obtOk.Click
        Try
            If txtmail.Text.Trim = "" Then
                HI.MG.ShowMsg.mInfo("กรุณาระบุ E-mail ที่ใช้ในการการ เชื่อมต่อระบบ !!!", 1305250031, Me.Text,, System.Windows.Forms.MessageBoxIcon.Warning)
                txtmail.Focus()
                Exit Sub
            End If

            If txtpassword.Text.Trim = "" Then
                HI.MG.ShowMsg.mInfo("กรุณาระบุ E-mail ที่ใช้ในการการ เชื่อมต่อระบบ !!!", 1305250031, Me.Text,, System.Windows.Forms.MessageBoxIcon.Warning)
                txtmail.Focus()
                Exit Sub
            End If

            bW_Confirm = True
            Me.Close()

        Catch ex As Exception
            'DK.MG.ShowMsg.ShowError(ex.Message)
        End Try
    End Sub

    'Public Function SetReason() As String
    '    Try
    '        bW_Confirm = False
    '        otbCancelReason.Text = ""
    '        Me.ShowDialog()
    '        If bW_Confirm Then
    '            Return otbCancelReason.Text.Trim
    '        Else
    '            Return ""
    '        End If
    '    Catch ex As Exception
    '        'DK.MG.ShowMsg.ShowError(ex.Message)
    '        Return ""
    '    End Try
    'End Function

    Private Sub obtCancel_Click(sender As System.Object, e As System.EventArgs) Handles obtCancel.Click
        txtmail.Text = ""
        txtpassword.Text = ""
        Me.Close()
    End Sub

    Private Sub FTStateReserve_CheckedChanged(sender As Object, e As EventArgs) Handles FTStateReserve.CheckedChanged
        If FTStateReserve.Checked Then
            txtpassword.Properties.PasswordChar = ""
        Else
            txtpassword.Properties.PasswordChar = "*"
        End If
    End Sub
End Class
Friend Class wInputReason

    Private bW_Confirm As Boolean
    Private Sub obtOk_Click(sender As System.Object, e As System.EventArgs) Handles obtOk.Click
        Try
            If otbCancelReason.Text.Trim = "" Then
                HI.MG.ShowMsg.mInfo("กรุณาระบุเหตุผลในการยกเลิกเอกสาร !!!", 1305250031, Me.Text,, System.Windows.Forms.MessageBoxIcon.Warning)
                otbCancelReason.Focus()
                Exit Sub
            End If

            bW_Confirm = True
            Me.Close()

        Catch ex As Exception
            'DK.MG.ShowMsg.ShowError(ex.Message)
        End Try
    End Sub

    Public Function SetReason() As String
        Try
            bW_Confirm = False
            otbCancelReason.Text = ""
            Me.ShowDialog()
            If bW_Confirm Then
                Return otbCancelReason.Text.Trim
            Else
                Return ""
            End If
        Catch ex As Exception
            'DK.MG.ShowMsg.ShowError(ex.Message)
            Return ""
        End Try
    End Function

    Private Sub obtCancel_Click(sender As System.Object, e As System.EventArgs) Handles obtCancel.Click
        Me.Close()
    End Sub

End Class
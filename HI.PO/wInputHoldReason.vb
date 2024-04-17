Friend Class wInputHoldReason

    Public bW_Confirm As Boolean = False
    Private Sub obtOk_Click(sender As System.Object, e As System.EventArgs) Handles obtOk.Click
        Try

            If FNHSysPOHoldId.Text.Trim = "" OrElse Val(FNHSysPOHoldId.Properties.Tag.ToString) = 0 Then

                HI.MG.ShowMsg.mInfo("กรุณาระบุเหตุผลในการ Hold PO !!!", 13031, Me.Text,, System.Windows.Forms.MessageBoxIcon.Warning)
                FNHSysPOHoldId.Focus()

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
        bW_Confirm = False
        Me.Close()
    End Sub

    Private Sub wInputHoldReason_Load(sender As Object, e As EventArgs) Handles Me.Load
        HI.TL.HandlerControl.AddHandlerObj(Me)
    End Sub
End Class
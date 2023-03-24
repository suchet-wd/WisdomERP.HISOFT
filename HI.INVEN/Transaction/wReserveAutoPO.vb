Public Class wReserveAutoPO

    Private _StateProc As Boolean
    Public Property StateProc As Boolean
        Get
            Return _StateProc
        End Get
        Set(value As Boolean)
            _StateProc = value
        End Set
    End Property


    Private Sub ocmdelete_Click(sender As System.Object, e As System.EventArgs) Handles ocmexit.Click
        Me.StateProc = False
        Me.Close()
    End Sub

    Private Sub ocmsendapprove_Click(sender As System.Object, e As System.EventArgs) Handles cmdok.Click
        Try
            If FNHSysCmpRunId.Text <> "" Then
                If FNHSysPurGrpId.Text <> "" Then

                    Me.StateProc = True
                    Me.Close()

                Else
                    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, FNHSysPurGrpId_lbl.Text)
                    FNHSysPurGrpId.Focus()
                End If
            Else

                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, FNHSysCmpRunId_lbl.Text)
                FNHSysCmpRunId.Focus()

            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub FNHSysPurGrpId_EditValueChanged(sender As Object, e As EventArgs) Handles FNHSysPurGrpId.EditValueChanged

    End Sub

    Private Sub wReserveAutoPO_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class
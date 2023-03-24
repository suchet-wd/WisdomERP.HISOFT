Public Class wAutoGeneratePOChangeUnit 

    Private _ProcOK As Boolean = False
    Public Property ProcOK As Boolean
        Get
            Return _ProcOK
        End Get
        Set(value As Boolean)
            _ProcOK = value
        End Set
    End Property

    Private Sub ocmcancel_Click(sender As Object, e As EventArgs) Handles ocmcancel.Click
        Me.ProcOK = False
        Me.Close()
    End Sub

    Private Sub ocmchange_Click(sender As Object, e As EventArgs) Handles ocmchange.Click

        If Me.FNHSysCurId.Text.Trim <> "" And Val(Me.FNHSysCurId.Properties.Tag.ToString) > 0 Then
            If Me.FNHSysUnitIdPO.Text.Trim <> "" And Val(Me.FNHSysUnitIdPO.Properties.Tag.ToString) > 0 Then
                Me.ProcOK = True
                Me.Close()
            Else
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FNHSysUnitId_lbl.Text)
                FNHSysUnitIdPO.Focus()
            End If
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FNHSysCurId_lbl.Text)
            FNHSysCurId.Focus()
        End If
    End Sub


End Class
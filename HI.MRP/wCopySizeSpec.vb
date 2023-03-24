Public Class wCopySizeSpec

    Private _State As Boolean = False
    Public Property State As Boolean
        Get
            Return _State
        End Get
        Set(value As Boolean)
            _State = value
        End Set
    End Property

    Private Sub obtcancel_Click(sender As Object, e As EventArgs) Handles obtcancel.Click
        _State = False
        Me.Close()
    End Sub

    Private Sub obtcopy_Click(sender As Object, e As EventArgs) Handles obtcopy.Click
        Try
            If Me.FNHSysStyleSSPId.Text = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.FNHSysStyleSSPId_lbl.Text)
                Me.FNHSysStyleSSPId.Focus()
                Exit Sub
            End If
            'If Me.FTSeasonCode.Text = "" Then
            '    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.FTSeasonCode_lbl.Text)
            '    Me.FTSeasonCode.Focus()
            '    Exit Sub
            'End If
            If Me.FTDate.Text = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.FTDate_lbl.Text)
                Me.FTDate.Focus()
                Exit Sub
            End If
            If Me.FTExpCode.Text = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.FTExpCode_lbl.Text)
                Me.FTExpCode.Focus()
                Exit Sub
            End If
            _State = True
            Me.Close()
        Catch ex As Exception
        End Try
    End Sub
End Class
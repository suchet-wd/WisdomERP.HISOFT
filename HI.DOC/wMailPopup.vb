Public Class wMailPopup 

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private _State As Boolean = False
    Public Property State As Boolean
        Get
            Return _State
        End Get
        Set(value As Boolean)
            _State = value
        End Set
    End Property

    Private Sub ocmsend_Click(sender As Object, e As EventArgs) Handles ocmsend.Click
        Try
            If verrifydata() Then
                _State = True
                Me.Close()
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Function verrifydata() As Boolean
        Try
            If Me.FTSubJect.Text = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.FTSubJect_lbl.Text)
                Me.FTSubJect.Focus()
                Return False
            End If
            If Me.FTMessange.Text = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.FTMessange_lbl.Text)
                Me.FTMessange.Focus()
                Return False
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub ocmcancel_Click(sender As Object, e As EventArgs) Handles ocmcancel.Click
        Try
            _State = False
            Me.Close()
        Catch ex As Exception
        End Try
    End Sub
End Class
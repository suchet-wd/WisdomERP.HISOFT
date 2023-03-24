Public Class wListCarton
   
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        HI.TL.HandlerControl.AddHandlerObj(Me)
    End Sub

    Private _Pass As Boolean = False
    Public Property Pass As Boolean
        Get
            Return _Pass
        End Get
        Set(value As Boolean)
            _Pass = value
        End Set
    End Property

    Private Sub ocmOK_Click(sender As Object, e As EventArgs) Handles ocmOK.Click
        Try
            _Pass = True
            Me.Close()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmclose_Click(sender As Object, e As EventArgs) Handles ocmclose.Click
        Try
            _Pass = False
            Me.Close()
        Catch ex As Exception
        End Try
    End Sub
End Class
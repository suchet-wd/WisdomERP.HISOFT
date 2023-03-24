
Public Class wAddConsul


    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub


#Region "Property"
    Public _Proc As Boolean
    Private Property Proc As Boolean
        Get
            Return _Proc
        End Get
        Set(value As Boolean)
            _Proc = value
        End Set
    End Property

    Public _ConsulId As Integer
    Private Property ConsulId As Integer
        Get
            Return _ConsulId
        End Get
        Set(value As Integer)
            _ConsulId = value
        End Set
    End Property
#End Region

    Private Sub ocmsave_Click(sender As Object, e As EventArgs) Handles ocmsave.Click
        Try
            Proc = True
            Me.Close()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmclose_Click(sender As Object, e As EventArgs) Handles ocmclose.Click
        Try
            Proc = False
            Me.Close()
        Catch ex As Exception
        End Try
    End Sub
End Class
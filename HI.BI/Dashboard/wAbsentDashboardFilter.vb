Public Class wAbsentDashboardFilter 

    Private _StateFilter As Boolean = False
    Public Property StateFilter As Boolean
        Get
            Return _StateFilter
        End Get
        Set(value As Boolean)
            _StateFilter = value
        End Set
    End Property

    Private Sub ocmok_Click(sender As Object, e As EventArgs) Handles ocmok.Click
        Me.StateFilter = True
        Me.Close()
    End Sub
End Class
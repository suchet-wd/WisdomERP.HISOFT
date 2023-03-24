Public Class wSizeSpecQCPopUp

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub wSizeSpecQCPopUp_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Me.ogcdetail.DataSource = oDt
        Catch ex As Exception
        End Try
    End Sub

    Public _State As Boolean = False
    Private Property State As Boolean
        Get
            Return _State
        End Get
        Set(value As Boolean)
            _State = value
        End Set
    End Property

    Public _oDt As DataTable
    Private Property oDt As DataTable
        Get
            Return _oDt
        End Get
        Set(value As DataTable)
            _oDt = value
        End Set
    End Property



    Private Sub ocmcancel_Click(sender As Object, e As EventArgs) Handles ocmcancel.Click
        _State = False
        Me.Close()
    End Sub

    Private Sub ocmok_Click(sender As Object, e As EventArgs) Handles ocmok.Click
        _State = True
        Me.Close()
    End Sub

   
End Class
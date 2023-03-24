Public Class wReportExportPopup
    Public _LstReport As HI.RP.ListReport
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        _LstReport = New HI.RP.ListReport(Me.Name.ToString)
        FNReportname.Properties.Items.AddRange(_LstReport.GetList)

        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Private _Proc As Boolean = False
    Public Property Proc As Boolean
        Get
            Return _Proc
        End Get
        Set(value As Boolean)
            _Proc = value
        End Set
    End Property

    Private Sub ocmok_Click(sender As Object, e As EventArgs) Handles ocmok.Click
        Try
            _Proc = True
            Me.Close()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmcancel_Click(sender As Object, e As EventArgs) Handles ocmcancel.Click
        Try
            _Proc = False
            Me.Close()
        Catch ex As Exception
        End Try
    End Sub

End Class
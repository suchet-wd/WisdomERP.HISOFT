Public Class wReportLeavePopup
    Private _LstReport As HI.RP.ListReport
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        _LstReport = New HI.RP.ListReport("wEmployeeLeave")
        FNReportname.Properties.Items.AddRange(_LstReport.GetList)

        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Private _list As Integer = 0
    Public Property List As Integer
        Get
            Return _list
        End Get
        Set(value As Integer)
            _list = value
        End Set
    End Property

    Private _state As Boolean = False
    Public Property state As Boolean
        Get
            Return _state
        End Get
        Set(value As Boolean)
            _state = value
        End Set
    End Property
    Private Sub obtok_Click(sender As Object, e As EventArgs) Handles obtok.Click
        Try
            _list = FNReportname.SelectedIndex
            _state = True
            Me.Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub obtcancel_Click(sender As Object, e As EventArgs) Handles obtcancel.Click
        Try
            _list = 0
            _state = False
            Me.Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub wReportLeavePopup_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        Try
            _list = 0
        Catch ex As Exception
        End Try
    End Sub
End Class
Imports DevExpress.XtraEditors.Controls
Imports Microsoft.Office.Interop

Public Class wPopUpReason


    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.



    End Sub
    Private _proc As Boolean = False
    Property proc As Boolean
        Get
            Return _proc
        End Get
        Set(value As Boolean)
            _proc = value
        End Set
    End Property

    Private Sub otbsave_Click(sender As Object, e As EventArgs) Handles otbsave.Click
        Try
            proc = True
            Me.Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub otbclose_Click(sender As Object, e As EventArgs) Handles otbclose.Click
        Try
            proc = False
            Me.Close()
        Catch ex As Exception

        End Try
    End Sub
End Class
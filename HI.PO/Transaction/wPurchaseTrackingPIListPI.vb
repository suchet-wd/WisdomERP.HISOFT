Public Class wPurchaseTrackingPIListPI 

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.


    End Sub

    Private _PINo As String = ""
    Public Property PINo As String
        Get
            Return _PINo
        End Get
        Set(value As String)
            _PINo = value
        End Set
    End Property



    Private Sub wFormListBarcodeTransaction_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case System.Windows.Forms.Keys.Escape
                Me.PINo = ""
                Me.Close()
        End Select
    End Sub

    Private Sub ogvdetail_DoubleClick(sender As Object, e As EventArgs) Handles ogvdetail.DoubleClick

        Try
            With ogvdetail
                If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub

                Me.PINo = "" & .GetFocusedRowCellValue("FTPINo").ToString


            End With

            Me.Close()

        Catch ex As Exception
            MsgBox(ex.Message())
        End Try

    End Sub
End Class
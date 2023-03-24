Public Class wStockBalanceMonthlyAccLIstDetail

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        With ogvdetail
            .OptionsView.ShowFooter = True
        End With
    End Sub

    Private Sub wFormListBarcodeTransaction_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case System.Windows.Forms.Keys.Escape
                Me.Close()
        End Select
    End Sub
End Class
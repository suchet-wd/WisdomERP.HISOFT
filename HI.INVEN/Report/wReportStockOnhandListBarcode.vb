Public Class wReportStockOnhandListBarcode

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        With ogvdetail
            .Columns("FNQuantity").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "FNQuantity")
            .Columns("FNQuantity").SummaryItem.DisplayFormat = "{0:n" & HI.ST.Config.QtyDigit & "}"

            .Columns("FNQuantityOut").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "FNQuantityOut")
            .Columns("FNQuantityOut").SummaryItem.DisplayFormat = "{0:n" & HI.ST.Config.QtyDigit & "}"

            .Columns("FNQuantityBal").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "FNQuantityBal")
            .Columns("FNQuantityBal").SummaryItem.DisplayFormat = "{0:n" & HI.ST.Config.QtyDigit & "}"

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
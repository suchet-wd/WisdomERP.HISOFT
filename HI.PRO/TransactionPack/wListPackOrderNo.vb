Public Class wListPackOrderNo

    Private _PackOrderNo As String = ""
    Public Property PackOrderNo As String
        Get
            Return _PackOrderNo
        End Get
        Set(value As String)
            _PackOrderNo = value
        End Set
    End Property

    Private _OrderNo As String = ""
    Public Property OrderNo As String
        Get
            Return _OrderNo
        End Get
        Set(value As String)
            _OrderNo = value
        End Set
    End Property


    Private Sub ogvlist_DoubleClick(sender As Object, e As System.EventArgs) Handles ogvlist.DoubleClick
        With ogvlist
            If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub

            Me.PackOrderNo = "" & .GetFocusedRowCellValue("FTPackNo").ToString
            Me.OrderNo = "" & .GetFocusedRowCellValue("FTOrderNo").ToString

            Me.Close()

        End With
    End Sub

End Class
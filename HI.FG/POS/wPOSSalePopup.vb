Public Class wPOSSalePopup

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private _SSize As String = ""
    Public Property SSize As String
        Get
            Return _SSize
        End Get
        Set(value As String)
            _SSize = value
        End Set
    End Property



    Private Sub ogvDetail_DoubleClick(sender As Object, e As EventArgs) Handles ogvDetail.DoubleClick
        Try
            With ogvDetail
                If .RowCount < 0 And .FocusedRowHandle < 0 Then Exit Sub
                _SSize = .GetRowCellValue(.FocusedRowHandle, "FTSizeBreakDown").ToString
            End With
            Me.Close()
        Catch ex As Exception
        End Try
    End Sub
End Class
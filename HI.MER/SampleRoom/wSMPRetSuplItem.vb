Imports DevExpress.XtraEditors.Controls

Public Class wSMPRetSuplItem

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.



    End Sub


    Private _ProcessProc As Boolean = False
    Public Property ProcessProc As Boolean
        Get
            Return _ProcessProc
        End Get
        Set(value As Boolean)
            _ProcessProc = value
        End Set
    End Property

    Private Sub ocmreceive_Click(sender As System.Object, e As System.EventArgs) Handles ocmok.Click
        Me.ProcessProc = True
        Me.Close()
    End Sub

    Private Sub ocmcancel_Click(sender As System.Object, e As System.EventArgs) Handles ocmcancel.Click
        Me.ProcessProc = False
        Me.Close()
    End Sub




    Private Sub ResFNRcvQty_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles ResFNRcvQty.EditValueChanging
        Try
            With ogvrcv

                Dim _Bal As Decimal = Val(.GetFocusedRowCellValue("FNBalQuantity"))

                If Val(e.NewValue) > _Bal Then
                    e.Cancel = True
                Else
                    e.Cancel = False
                End If
            End With
        Catch ex As Exception

        End Try
    End Sub
End Class
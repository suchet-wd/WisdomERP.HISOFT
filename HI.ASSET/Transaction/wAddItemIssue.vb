Public Class wAddItemIssue


#Region "Property"
    Private _AddComplete As Boolean = False
    Public Property AddComplete As Boolean
        Get
            Return _AddComplete
        End Get
        Set(value As Boolean)
            _AddComplete = value
        End Set
    End Property
#End Region

    Private Sub ocmadd_Click(sender As Object, e As EventArgs) Handles ocmadd.Click
        If Verify() Then
            Me.AddComplete = True
            Me.Close()
        End If
    End Sub

    Private Function Verify() As Boolean
        Dim _Pass As Boolean = False
        If Me.FTBarcodeNo.Text <> "" Then
            _Pass = True
        Else
            MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FTBarcodeNo_lbl.Text)
            Me.FTBarcodeNo.Focus()
        End If
        Return _Pass
    End Function

    Private Sub ocmcancel_Click(sender As Object, e As EventArgs) Handles ocmcancel.Click
        Me.Close()
    End Sub
End Class
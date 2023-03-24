Public Class wPurchaserAssetRevise
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

#Region "Property"
    Private _StateProc As Boolean = False
    Public Property StateProc As Boolean
        Get
            Return _StateProc
        End Get
        Set(value As Boolean)
            _StateProc = value
        End Set
    End Property
#End Region


    Private Sub ocmok_Click(sender As Object, e As EventArgs) Handles ocmok.Click
        If Me.FTRemark.Text.Trim <> "" Then
            Me.StateProc = True
            Me.Close()
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, Me.Text)
            Me.FTRemark.Focus()
        End If
    End Sub

    Private Sub ocmcancel_Click(sender As Object, e As EventArgs) Handles ocmcancel.Click
        Me.StateProc = False
        Me.Close()
    End Sub
End Class
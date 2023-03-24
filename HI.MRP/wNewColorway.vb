Public Class wNewColorway 

    Private _ProcNew As Boolean = False
    Public Property ProcNew As Boolean
        Get
            Return _ProcNew
        End Get
        Set(value As Boolean)
            _ProcNew = value
        End Set
    End Property


    Private Sub ocmnewcolorway_Click(sender As Object, e As EventArgs) Handles ocmnewcolorway.Click
        If Me.FTColorway.Text <> "" And Me.FTColorway.Properties.Tag.ToString <> "" Then
            Me.ProcNew = True
            Me.Close()
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, FTColorway_lbl.Text)
            Me.FTColorway.Focus()
        End If
    End Sub

    Private Sub ocmcancel_Click(sender As Object, e As EventArgs) Handles ocmcancel.Click
        Me.ProcNew = False
        Me.Close()
    End Sub

    Private Sub wNewColorway_Load(sender As Object, e As EventArgs) Handles Me.Load
        ocmcancel.Enabled = True
    End Sub
End Class
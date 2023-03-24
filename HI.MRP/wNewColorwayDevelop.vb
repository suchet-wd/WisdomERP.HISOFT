Public Class wNewColorwayDevelop

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
        If Me.FTColorway.Text <> "" Then
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

    Private Sub FTColorway_EditValueChanged(sender As Object, e As EventArgs) Handles FTColorway.EditValueChanged

    End Sub

    Private Sub FTColorway_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles FTColorway.KeyDown

    End Sub

    Private Sub FTColorway_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles FTColorway.KeyPress

        Dim CharInt As Integer = 0
        ' CharInt = Asc(e.KeyChar)
        Select Case Asc(e.KeyChar)
            Case 32, 39, 34, 37
                e.Handled = True
            Case Else
                e.Handled = False
        End Select

    End Sub
End Class
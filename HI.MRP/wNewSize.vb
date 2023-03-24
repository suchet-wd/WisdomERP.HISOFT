Public Class wNewSize

    Private _ProcNew As Boolean = False
    Public Property ProcNew As Boolean
        Get
            Return _ProcNew
        End Get
        Set(value As Boolean)
            _ProcNew = value
        End Set
    End Property


    Private Sub ocmnewcolorway_Click(sender As Object, e As EventArgs) Handles ocmnewsizebreakdown.Click
        If Me.FTSizeBreakDown.Text <> "" And Me.FTSizeBreakDown.Properties.Tag.ToString <> "" Then
            Me.ProcNew = True
            Me.Close()
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, FTSizeBreakDown_lbl.Text)
            Me.FTSizeBreakDown.Focus()
        End If
    End Sub

    Private Sub ocmcancel_Click(sender As Object, e As EventArgs) Handles ocmcancel.Click
        Me.ProcNew = False
        Me.Close()
    End Sub

    Private Sub wNewSize_Load(sender As Object, e As EventArgs) Handles Me.Load
        ocmcancel.Enabled = True
    End Sub
End Class
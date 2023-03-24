Public Class wCopyOperationByStyle 

    Private _Process As Boolean = False
    Public Property Process() As Boolean
        Get
            Return _Process
        End Get
        Set(value As Boolean)
            _Process = value
        End Set
    End Property

    Private Sub ocmcancel_Click(sender As Object, e As EventArgs) Handles ocmcancel.Click
        Me.Process = False
        Me.Close()
    End Sub

    Private Sub ocmcopy_Click(sender As Object, e As EventArgs) Handles ocmcopy.Click

        If Me.FNHSysStyleId.Properties.Tag.ToString <> "" And Me.FNHSysStyleIdTo.Properties.Tag.ToString <> "" And Me.FNHSysStyleId.Text <> Me.FNHSysStyleIdTo.Text Then
            Process = True
            Me.Close()
        End If

    End Sub
End Class
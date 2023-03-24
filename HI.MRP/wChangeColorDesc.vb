Public Class wChangeColorDesc 

    Private _ProcNew As Boolean = False
    Public Property ProcNew As Boolean
        Get
            Return _ProcNew
        End Get
        Set(value As Boolean)
            _ProcNew = value
        End Set
    End Property

    Private _OldDescTH As String = ""
    Public Property OldDescTH As String
        Get
            Return _OldDescTH
        End Get
        Set(value As String)
            _OldDescTH = value
        End Set
    End Property

    Private _OldDescEN As String = ""
    Public Property OldDescEN As String
        Get
            Return _OldDescEN
        End Get
        Set(value As String)
            _OldDescEN = value
        End Set
    End Property

    Private Sub ocmcancel_Click(sender As Object, e As EventArgs) Handles ocmcancel.Click
        Me.ProcNew = False
        Me.Close()
    End Sub

    Private Sub ocmsavedesc_Click(sender As Object, e As EventArgs) Handles ocmsavedesc.Click
        If Me.FTRawMatColorNameTH.Text.Trim <> "" Then

            If Me.FTRawMatColorNameEN.Text.Trim <> "" Then
                Me.ProcNew = True
                Me.Close()
            Else
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, FTRawMatColorNameEN_lbl.Text)
                Me.FTRawMatColorNameEN.Focus()
            End If

        Else

            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, FTRawMatColorNameTH_lbl.Text)
            Me.FTRawMatColorNameTH.Focus()

        End If

    End Sub

    Private Sub wChangeColorDesc_Load(sender As Object, e As EventArgs) Handles Me.Load
        ocmcancel.Enabled = True
    End Sub

End Class
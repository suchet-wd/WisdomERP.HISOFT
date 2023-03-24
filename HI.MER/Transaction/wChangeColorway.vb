Public Class wChangeColorway

    Private _ProcNew As Boolean = False
    Public Property ProcNew As Boolean
        Get
            Return _ProcNew
        End Get
        Set(value As Boolean)
            _ProcNew = value
        End Set
    End Property

    Private _ColorWayCode As String = ""
    Public Property ColorWayCode As String
        Get
            Return _ColorWayCode
        End Get
        Set(value As String)
            _ColorWayCode = value
        End Set
    End Property

    Private _ColorWayID As Integer = 0
    Public Property ColorWayID As Integer
        Get
            Return _ColorWayID
        End Get
        Set(value As Integer)
            _ColorWayID = value
        End Set
    End Property

    Private _OldColorWayCode As String = ""
    Public Property OldColorWayCode As String
        Get
            Return _OldColorWayCode
        End Get
        Set(value As String)
            _OldColorWayCode = value
        End Set
    End Property

    Private _OldColorWayID As Integer = 0
    Public Property OldColorWayID As Integer
        Get
            Return _OldColorWayID
        End Get
        Set(value As Integer)
            _OldColorWayID = value
        End Set
    End Property

    Private Sub ocmnewcolorway_Click(sender As Object, e As EventArgs) Handles ocmok.Click
        If Me.FTColorway.Text <> "" And Me.FTColorway.Properties.Tag.ToString <> "" Then
            Me.ProcNew = True

            Me.ColorWayCode = Me.FTColorway.Text
            Me.ColorWayID = Integer.Parse(Val(Me.FTColorway.Properties.Tag.ToString))

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
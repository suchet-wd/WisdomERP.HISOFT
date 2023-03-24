Public Class wPopupCalc

    Private _CalcValue As Integer = 0
    Public Property CalcValue As Integer
        Get
            Return _CalcValue
        End Get
        Set(value As Integer)
            _CalcValue = value
        End Set
    End Property

    Private _StateEnter As Boolean = False
    Public Property StateEnter As Boolean
        Get
            Return _StateEnter
        End Get
        Set(value As Boolean)
            _StateEnter = value
        End Set
    End Property

    Private Sub oBtnEnter_Click(sender As Object, e As EventArgs) Handles oBtnEnter.Click
        Try

            _CalcValue = Me.oTxtValue.Value
            _StateEnter = True
            Me.Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub oBtn0_Click(sender As Object, e As EventArgs) Handles oBtn0.Click
        Try
            Me.oTxtValue.Value = CInt(Me.oTxtValue.Text & "0")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub oBtn1_Click(sender As Object, e As EventArgs) Handles oBtn1.Click
        Try
            Me.oTxtValue.Value = CInt(Me.oTxtValue.Text & "1")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub oBtn2_Click(sender As Object, e As EventArgs) Handles oBtn2.Click
        Try
            Me.oTxtValue.Value = CInt(Me.oTxtValue.Text & "2")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub oBtn3_Click(sender As Object, e As EventArgs) Handles oBtn3.Click
        Try
            Me.oTxtValue.Value = CInt(Me.oTxtValue.Text & "3")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub oBtn4_Click(sender As Object, e As EventArgs) Handles oBtn4.Click
        Try
            Me.oTxtValue.Value = CInt(Me.oTxtValue.Text & "4")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub oBtn5_Click(sender As Object, e As EventArgs) Handles oBtn5.Click
        Try
            Me.oTxtValue.Value = CInt(Me.oTxtValue.Text & "5")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub oBtn6_Click(sender As Object, e As EventArgs) Handles oBtn6.Click
        Try
            Me.oTxtValue.Value = CInt(Me.oTxtValue.Text & "6")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub oBtn7_Click(sender As Object, e As EventArgs) Handles oBtn7.Click
        Try
            Me.oTxtValue.Value = CInt(Me.oTxtValue.Text & "7")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub oBtn8_Click(sender As Object, e As EventArgs) Handles oBtn8.Click
        Try
            Me.oTxtValue.Value = CInt(Me.oTxtValue.Text & "8")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub oBtn9_Click(sender As Object, e As EventArgs) Handles oBtn9.Click
        Try
            Me.oTxtValue.Value = CInt(Me.oTxtValue.Text & "9")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub oBtnC_Click(sender As Object, e As EventArgs) Handles oBtnC.Click
        Try
            Dim loc As Integer

            loc = oTxtValue.Text.Length
            If loc <= 1 Then
                Me.oTxtValue.Value = 0
            Else
                Me.oTxtValue.Value = CInt(Me.oTxtValue.Text.Remove(loc - 1, 1))
            End If
            If Me.oTxtValue.Text = "" Then Me.oTxtValue.Value = 0
        Catch ex As Exception

        End Try
    End Sub

    Private Sub oBtnCE_Click(sender As Object, e As EventArgs) Handles oBtnCE.Click
        Try
            Me.oTxtValue.Value = 0
        Catch ex As Exception

        End Try
    End Sub

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        Me.oTxtValue.Value = 0
        _StateEnter = False
    End Sub


End Class
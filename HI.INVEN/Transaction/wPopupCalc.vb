Public Class wPopupCalc



    Private _CalcValue As Decimal = 0
    Public Property CalcValue As Decimal
        Get
            Return _CalcValue
        End Get
        Set(value As Decimal)
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
            If Validatedata() = False Then Exit Sub
            Me.otbcal.Text = Val(Me.otbcal.Text & "0")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub oBtn1_Click(sender As Object, e As EventArgs) Handles oBtn1.Click
        Try
            If Validatedata() = False Then Exit Sub
            Me.otbcal.Text = Val(Me.otbcal.Text & "1")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub oBtn2_Click(sender As Object, e As EventArgs) Handles oBtn2.Click
        Try
            If Validatedata() = False Then Exit Sub
            Me.otbcal.Text = Val(Me.otbcal.Text & "2")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub oBtn3_Click(sender As Object, e As EventArgs) Handles oBtn3.Click
        Try
            If Validatedata() = False Then Exit Sub
            Me.otbcal.Text = Val(Me.otbcal.Text & "3")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub oBtn4_Click(sender As Object, e As EventArgs) Handles oBtn4.Click
        Try
            If Validatedata() = False Then Exit Sub
            Me.otbcal.Text = Val(Me.otbcal.Text & "4")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub oBtn5_Click(sender As Object, e As EventArgs) Handles oBtn5.Click
        Try
            If Validatedata() = False Then Exit Sub
            Me.otbcal.Text = Val(Me.otbcal.Text & "5")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub oBtn6_Click(sender As Object, e As EventArgs) Handles oBtn6.Click
        Try
            If Validatedata() = False Then Exit Sub
            Me.otbcal.Text = Val(Me.otbcal.Text & "6")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub oBtn7_Click(sender As Object, e As EventArgs) Handles oBtn7.Click
        Try
            If Validatedata() = False Then Exit Sub
            Me.otbcal.Text = Val(Me.otbcal.Text & "7")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub oBtn8_Click(sender As Object, e As EventArgs) Handles oBtn8.Click
        Try
            If Validatedata() = False Then Exit Sub
            Me.otbcal.Text = Val(Me.otbcal.Text & "8")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub oBtn9_Click(sender As Object, e As EventArgs) Handles oBtn9.Click
        Try
            If Validatedata() = False Then Exit Sub
            Me.otbcal.Text = (Me.otbcal.Text & "9")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub oBtnC_Click(sender As Object, e As EventArgs) Handles oBtnC.Click
        Try
            Dim loc As Integer

            loc = otbcal.Text.Length
            If loc <= 1 Then
                Me.otbcal.Text = "0"
            Else
                Me.otbcal.Text = (Me.otbcal.Text.Remove(loc - 1, 1))
            End If
            If Me.otbcal.Text = "" Then Me.otbcal.Text = 0

        Catch ex As Exception
        End Try

    End Sub

    Private Sub oBtnCE_Click(sender As Object, e As EventArgs) Handles oBtnCE.Click
        Try
            Me.otbcal.Text = "0"
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

    Private Sub otbcal_EditValueChanged(sender As Object, e As EventArgs) Handles otbcal.EditValueChanged
        Me.oTxtValue.Value = Val(otbcal.Text)
    End Sub

    Private Sub obtndot_Click(sender As Object, e As EventArgs) Handles obtndot.Click
        Try

            If otbcal.Text.Contains(".") = False Then
                Me.otbcal.Text = (Me.otbcal.Text & ".")
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Function Validatedata()
        Dim state As Boolean = False

        If otbcal.Text.Contains(".") Then

            Dim Idx As Integer = otbcal.Text.IndexOf(".")

            If otbcal.Text.Length > Idx + 1 Then
                Dim Data As String = Microsoft.VisualBasic.Right(otbcal.Text, otbcal.Text.Length - (Idx + 1))
                If Data.Length >= 2 Then
                    state = False
                Else
                    state = True
                End If

            Else
                state = True
            End If

        Else
            state = True
        End If

        Return state
    End Function

    Private Sub wPopupCalc_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            FTEmpPicName.Image = HI.UL.ULImage.LoadImage(My.Application.Info.DirectoryPath & "\Images\System\QCFabricInfo.jpg")
            Dim _SuperToolTip As New DevExpress.Utils.SuperToolTip()
            Dim _ToolTipTitleItem As New DevExpress.Utils.ToolTipTitleItem()

            _ToolTipTitleItem.Appearance.Image = FTEmpPicName.Image
            _ToolTipTitleItem.Appearance.Options.UseImage = True
            _ToolTipTitleItem.Image = FTEmpPicName.Image
            _ToolTipTitleItem.Text = ""

            With _SuperToolTip
                .Items.Add(_ToolTipTitleItem)
            End With

            FTEmpPicName.SuperTip = _SuperToolTip

        Catch ex As Exception

        End Try
    End Sub
End Class
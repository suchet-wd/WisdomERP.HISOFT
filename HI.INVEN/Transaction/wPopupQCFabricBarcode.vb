Imports System.Windows.Forms

Public Class wPopupQCFabricBarcode
    Private CalcPopup As wPopupCalc

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
            _StateEnter = True
            Me.Close()
        Catch ex As Exception

        End Try
    End Sub

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.



    End Sub

    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        Try


            _StateEnter = False
            Me.Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub wPopupQCFabricBarcode_Load(sender As Object, e As EventArgs) Handles Me.Load
        _StateEnter = False
    End Sub

    Private Sub FNYardNo_EditValueChanged(sender As Object, e As EventArgs) Handles FNYardNo.EditValueChanged

    End Sub

    Private Sub FNYardNo_MouseDown(sender As Object, e As MouseEventArgs) Handles FNYardNo.MouseDown
        Try
            CalcPopup = New wPopupCalc
            With CalcPopup
                .ShowDialog()
                If .StateEnter = True Then
                    Me.FNYardNo.Value = .CalcValue
                End If
            End With
        Catch ex As Exception

        End Try
    End Sub

    Private Sub FNSize_MouseDown(sender As Object, e As MouseEventArgs) Handles FNSize.MouseDown
        Try
            CalcPopup = New wPopupCalc
            With CalcPopup
                .ShowDialog()
                If .StateEnter = True Then
                    Me.FNSize.Value = .CalcValue
                End If
            End With
        Catch ex As Exception

        End Try
    End Sub

    Private Sub FNPoint_MouseDown(sender As Object, e As MouseEventArgs) Handles FNPoint.MouseDown
        Try
            CalcPopup = New wPopupCalc
            With CalcPopup
                .ShowDialog()
                If .StateEnter = True Then
                    Me.FNPoint.Value = .CalcValue
                End If
            End With
        Catch ex As Exception

        End Try
    End Sub
End Class
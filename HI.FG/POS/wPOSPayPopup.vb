Imports System.Windows.Forms
Public Class wPOSPayPopup
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        Me.FNCashAmt.Focus()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private _Proc As Boolean = False
    Public Property Proc As Boolean
        Get
            Return _Proc
        End Get
        Set(value As Boolean)
            _Proc = value
        End Set
    End Property

    Private Sub ocmok_Click(sender As Object, e As EventArgs) Handles ocmok.Click
        Try
            If FNCashAmt.Value >= Me.FNInvGrandAmt.Value Then
                _Proc = True
                Me.Close()
            Else
                HI.MG.ShowMsg.mInfo("ยอดเงินชำระน้อยกว่ายอดสินค้า กรุณาตรวจสอบ", 1603161438, Me.Text)
                Me.FNCashAmt.Focus()
                Exit Sub
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmcancel_Click(sender As Object, e As EventArgs) Handles ocmcancel.Click
        Try
            _Proc = False
            Me.Close()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub FNCashAmt_EditValueChanged(sender As Object, e As EventArgs) Handles FNCashAmt.EditValueChanged
        Try
            If FNInvGrandAmt.Value > 0 And FNCashAmt.Value > 0 Then
                Me.FNChangeAmt.Value = FNCashAmt.Value - FNInvGrandAmt.Value
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub wPOSPayPopup_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Me.FNCashAmt.Value = 0
            Me.FNCashAmt.Focus()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub FNCashAmt_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles FNCashAmt.KeyDown
        Try
            Select Case e.KeyCode
                Case Keys.KeyCode.Enter
                    Call ocmok_Click(Nothing, Nothing)
                Case Keys.Escape
                    Call ocmcancel_Click(Nothing, Nothing)
            End Select
        Catch ex As Exception

        End Try
    End Sub
End Class
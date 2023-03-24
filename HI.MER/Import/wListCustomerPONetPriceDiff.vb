Option Explicit On
Option Strict Off

Imports System
Imports System.Data
Imports System.Windows.Forms
Imports System.Windows.Forms.Control

Public Class wListCustomerPONetPriceDiff


    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub wListCustomerPONetPriceDiff_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Try
            Select Case e.KeyCode

                Case System.Windows.Forms.Keys.Escape
                    Me.Close()
            End Select
        Catch ex As Exception
        End Try
    End Sub

    Private Sub wListCompleteCopyOrder_Load(sender As Object, e As EventArgs) Handles Me.Load
    End Sub

    Private Sub ogvCopyOrder_KeyDown(sender As Object, e As KeyEventArgs) Handles ogvdetail.KeyDown

        Try

            Select Case e.KeyCode
                Case System.Windows.Forms.Keys.Escape
                    Me.Close()
            End Select

        Catch ex As Exception
        End Try

    End Sub
End Class
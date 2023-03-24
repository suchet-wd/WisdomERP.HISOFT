Option Explicit On
Option Strict Off

Imports System
Imports System.Data
Imports System.Windows.Forms
Imports System.Windows.Forms.Control

Public Class wFactoryHubListOrderNo

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub ogvlist_KeyDown(sender As Object, e As KeyEventArgs)
        Select Case e.KeyCode
            Case Keys.Escape
                Me.Close()
        End Select
    End Sub
End Class
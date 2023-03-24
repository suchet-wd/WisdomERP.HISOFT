Option Explicit On
Option Strict Off

Imports System
Imports System.Data
Imports System.Collections.Generic
Imports Microsoft.VisualBasic

Public Class wFactoryHubStylePopup



#Region "Event Handle"

    Private Sub ocmok_Click(sender As Object, e As EventArgs) Handles ocmok.Click

        DialogResult = System.Windows.Forms.DialogResult.OK
            Me.Close()

    End Sub



#End Region

End Class
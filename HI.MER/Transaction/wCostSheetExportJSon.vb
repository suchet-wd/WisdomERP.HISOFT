Imports System.Drawing

Public Class wCostSheetExportJSon

    Sub New()
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Public StateSave As Boolean = False

    Private Sub ocmCancel_Click(sender As Object, e As EventArgs) Handles ocmCancel.Click
        StateSave = False
        Me.Close()
    End Sub

    Private Sub ocmOK_Click(sender As Object, e As EventArgs) Handles ocmOK.Click

        StateSave = True
        Me.Close()

    End Sub

    Private Sub ogvdetail_RowStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles ogvdetail.RowStyle
        Try
            Select Case "" & ogvdetail.GetRowCellValue(e.RowHandle, "FTSendStatus").ToString
                Case "True"
                    e.Appearance.BackColor = Color.GreenYellow
                Case "False"
                    e.Appearance.BackColor = Color.OrangeRed
                Case Else

            End Select
        Catch ex As Exception

        End Try
    End Sub
End Class
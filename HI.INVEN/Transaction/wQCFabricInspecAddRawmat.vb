Imports System.Drawing
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports System.Windows.Forms

Public Class wQCFabricInspecAddRawmat

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private _StateProc As Boolean = False
    Public Property StateProc As Boolean
        Get
            Return _StateProc
        End Get
        Set(value As Boolean)
            _StateProc = value
        End Set
    End Property

    Private Sub wQCFabricInspecAddRawmat_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try

            Select Case e.KeyCode
                Case System.Windows.Forms.Keys.Escape
                    Me.Close()
            End Select

        Catch ex As Exception
        End Try
    End Sub


    Private Sub ogvrawmat_DoubleClick(sender As Object, e As EventArgs) Handles ogvrawmat.DoubleClick
        Try
            With ogvrawmat
                Dim pt As Point = ogvrawmat.GridControl.PointToClient(Control.MousePosition)
                Dim info As GridHitInfo = ogvrawmat.CalcHitInfo(pt)

                If (info.InRow Or info.InRowCell) Then
                    If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub
                    If .GetRowCellValue(.FocusedRowHandle, "FTStateQC").ToString = "1" Then Exit Sub

                    StateProc = True
                    Me.Close()
                End If

            End With
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub ogvrawmat_RowCellStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles ogvrawmat.RowCellStyle
        Try
            With ogvrawmat
                Try
                    If .GetRowCellValue(e.RowHandle, "FTStateQC") = "1" Then
                        e.Appearance.ForeColor = Drawing.Color.Green
                    End If
                Catch ex As Exception
                End Try

            End With
        Catch ex As Exception

        End Try
    End Sub
End Class
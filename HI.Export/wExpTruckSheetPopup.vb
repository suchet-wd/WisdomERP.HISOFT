Imports System.Drawing
Imports DevExpress.XtraGrid.Views.Grid

Public Class wExpTruckSheetPopup

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

    Private Sub ocmcancel_Click(sender As Object, e As EventArgs) Handles ocmcancel.Click
        Me.StateProc = False
        Me.Close()
    End Sub

    Private Sub ocmsave_Click(sender As Object, e As EventArgs) Handles ocmsave.Click
        Try
            Me.StateProc = True
            Me.Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub oSelectAll_CheckedChanged(sender As Object, e As EventArgs) Handles oSelectAll.CheckedChanged
        Try
            Dim _State As String = "0"
            If Me.oSelectAll.Checked Then
                _State = "1"
            End If
            With ogcref
                If Not (.DataSource Is Nothing) And ogvref.RowCount > 0 Then
                    With ogvref
                        For I As Integer = 0 To .RowCount - 1
                            .SetRowCellValue(I, "FTSelect", _State)
                        Next
                    End With
                    CType(.DataSource, System.Data.DataTable).AcceptChanges()
                End If
            End With
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ogvref_RowStyle(sender As Object, e As RowStyleEventArgs) Handles ogvref.RowStyle
        Try
            Dim View As GridView = sender
            If (e.RowHandle >= 0) Then
                Dim QtyRequire As Integer = View.GetRowCellDisplayText(e.RowHandle, View.Columns("FNCTNS"))
                Dim QtyBal As Integer = View.GetRowCellDisplayText(e.RowHandle, View.Columns("FTCTNQtyBal"))
                If QtyRequire > QtyBal Then
                    e.Appearance.BackColor = Color.Salmon
                    e.Appearance.BackColor2 = Color.SeaShell
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub
End Class
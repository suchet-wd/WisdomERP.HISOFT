Public Class wListEmplyeeOTOver

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub wListEmplyeeOTOver_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case System.Windows.Forms.Keys.Escape
                Me.Close()
        End Select
    End Sub

    Private Sub ogvlist_RowCellStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles ogvlist.RowCellStyle
        With ogvlist
            Try

                Select Case True
                    Case ("" & .GetRowCellValue(e.RowHandle, "FTStateLockNotOver").ToString = "1")
                        e.Appearance.BackColor = Drawing.Color.FromArgb(255, 192, 128)
                  
                End Select

            Catch ex As Exception
            End Try

        End With
    End Sub

End Class
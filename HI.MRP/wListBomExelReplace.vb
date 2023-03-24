Public Class wListBomExelReplace

    Public StateOK As Boolean = False

    Private Sub ocmdelete_Click(sender As System.Object, e As System.EventArgs) Handles ocmexit.Click
        StateOK = False
        Me.Close()

    End Sub

    Private Sub wListBomExelReplace_Load(sender As Object, e As EventArgs) Handles Me.Load
        StateOK = False
    End Sub

    Private Sub ocmsave_Click(sender As Object, e As EventArgs) Handles ocmsave.Click
        With CType(Me.ogclist.DataSource, DataTable)
            If .Select("FTStateImport='1'").Length > 0 Then
                StateOK = True
                Me.Close()

            Else
                HI.MG.ShowMsg.mInfo("กรุณาทำการเลือกรายกาาร ซ้ำ ที่ต้องการ Import เข้าไปใหม่ !!!", 2002548784, Me.Text,, System.Windows.Forms.MessageBoxIcon.Warning)
            End If
        End With
    End Sub

    Private Sub FTStaReceiveAll_CheckedChanged(sender As Object, e As EventArgs) Handles FTStaReceiveAll.CheckedChanged
        Try
            Dim State As String = "0"

            If FTStaReceiveAll.Checked Then
                State = "1"
            End If


            With ogclist
                If Not (.DataSource Is Nothing) And ogvlist.RowCount > 0 Then

                    With ogvlist
                        For I As Integer = 0 To .RowCount - 1

                            .SetRowCellValue(I, .Columns.ColumnByFieldName("FTStateImport"), State)
                        Next
                    End With

                    CType(.DataSource, DataTable).AcceptChanges()
                End If

            End With


        Catch ex As Exception

        End Try
    End Sub
End Class
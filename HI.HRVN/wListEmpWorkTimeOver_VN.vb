Public Class wListEmpWorkTimeOver_VN

    Private _ProcessOK As Boolean = False
    Public Property ProcessOK As Boolean
        Get
            Return _ProcessOK
        End Get
        Set(value As Boolean)
            _ProcessOK = value
        End Set
    End Property

    Private _ProcessDate As String = ""
    Public Property ProcessDate As String
        Get
            Return _ProcessDate
        End Get
        Set(value As String)
            _ProcessDate = value
        End Set
    End Property

    Private Sub ochkselectall_CheckedChanged(sender As Object, e As EventArgs) Handles ochkselectall.CheckedChanged
        Try
            Dim _State As String = "0"
            If Me.ochkselectall.Checked Then
                _State = "1"
            End If

            With ogc
                If Not (.DataSource Is Nothing) And ogv.RowCount > 0 Then

                    With ogv
                        For I As Integer = 0 To .RowCount - 1
                            .SetRowCellValue(I, .Columns.ColumnByFieldName("FTSelect"), _State)
                        Next
                    End With

                    CType(.DataSource, System.Data.DataTable).AcceptChanges()
                End If

            End With
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ocmsave_Click(sender As Object, e As EventArgs) Handles ocmsave.Click

        CType(ogc.DataSource, System.Data.DataTable).AcceptChanges()

        If CType(ogc.DataSource, System.Data.DataTable).Select("FTSelect='1'").Length > 0 Then
            Me.ProcessOK = True
            Me.Close()
        Else
            HI.MG.ShowMsg.mInfo("กรุณาทำการระบุข้อมูลพนักงาน !!!", 1404290001, Me.Text)
        End If

    End Sub

    Private Sub ocmcancel_Click(sender As Object, e As EventArgs) Handles ocmcancel.Click
        Me.ProcessOK = False
        Me.Close()
    End Sub
End Class
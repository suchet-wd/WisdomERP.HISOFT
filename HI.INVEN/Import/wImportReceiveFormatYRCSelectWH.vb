Public Class wImportReceiveFormatYRCSelectWH

    Private ProOK As Boolean = False
    Property ProcessOK As Boolean

        Get
            Return ProOK
        End Get
        Set(value As Boolean)
            ProOK = value
        End Set
    End Property
    Private Sub ocmdelete_Click(sender As System.Object, e As System.EventArgs) Handles ocmexit.Click
        ProOK = False
        Me.Close()
    End Sub

    Private Sub ocmsendapprove_Click(sender As System.Object, e As System.EventArgs) Handles ocmok.Click
        Try

            Dim dt As DataTable
            With CType(ogclist.DataSource, DataTable)
                .AcceptChanges()
                dt = .Copy
            End With

            If dt.Select("FNHSysWHId=''").Length > 0 Then
                HI.MG.ShowMsg.mProcessError(1403043331, "กรุณาทำการเลือกข้อมูล Warehouse สำหรับจัดเก็บ !!!", Me.Text, System.Windows.Forms.MessageBoxIcon.Warning)
                Exit Sub
            End If

            ProOK = True
            Me.Close()
        Catch ex As Exception
        End Try


    End Sub



End Class
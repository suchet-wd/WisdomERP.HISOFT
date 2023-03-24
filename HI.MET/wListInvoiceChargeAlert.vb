Public Class wListInvoiceChargeAlert

    Private _TmpDataPO As DataTable
    Public Property DataPO As DataTable
        Get
            Return _TmpDataPO
        End Get
        Set(value As DataTable)
            _TmpDataPO = value
        End Set
    End Property

    Public Sub RefreshDataPO()

        ogclist.DataSource = DataPO
        ogclist.RefreshDataSource()

    End Sub

    Private Sub ocmdelete_Click(sender As System.Object, e As System.EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ocmsendapprove_Click(sender As System.Object, e As System.EventArgs)
        Try
            With ogvlist
                .FocusedRowHandle = 0
                .FocusedColumn = .Columns.ColumnByName("FTSelect")
            End With
            CType(Me.ogclist.DataSource, DataTable).AcceptChanges()
            If CType(Me.ogclist.DataSource, DataTable).Select("FTSelect='1'").Length <= 0 Then
                HI.MG.ShowMsg.mProcessError(1403040001, "กรุณาทำการเลือกข้อมูล !!!", Me.Text, System.Windows.Forms.MessageBoxIcon.Error)
                Exit Sub
            End If
            Dim _Str As String = ""
            For Each R As DataRow In CType(Me.ogclist.DataSource, DataTable).Select("FTSelect='1'")

                _Str = "Update [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase "
                _Str &= vbCrLf & " SET FTStateSendApp='1'"
                _Str &= vbCrLf & ", FTSendAppBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Str &= vbCrLf & ", FTSendAppDate=" & HI.UL.ULDate.FormatDateDB & ""
                _Str &= vbCrLf & ", FTSendAppTime=" & HI.UL.ULDate.FormatTimeDB & ""
                _Str &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(R!FTPurchaseNo.ToString) & "' AND ISNULL(FTStateSendApp,'')<>'1' "

                HI.Conn.SQLConn.ExecuteNonQuery(_Str, Conn.DB.DataBaseName.DB_PUR)
            Next

            HI.MG.ShowMsg.mInfo("Send Approve Complete ", 1403040002, Me.Text)
        Catch ex As Exception

        End Try



    End Sub

    Private Sub ogclist_Click(sender As System.Object, e As System.EventArgs) Handles ogclist.Click

    End Sub

    Private Sub ogvlist_DoubleClick(sender As Object, e As System.EventArgs) Handles ogvlist.DoubleClick
        With ogvlist
            If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub

            Dim _FTInvoiceNo As String = "" & .GetFocusedRowCellValue("FTInvoiceNo").ToString
            Dim _FTSuplCode As String = "" & .GetFocusedRowCellValue("FTSuplCode").ToString
            Dim _FFNHSysSuplId As String = "" & .GetFocusedRowCellValue("FNHSysSuplId").ToString

            Dim _WInvoiceCharge As New HI.ACC.wInvoiceCharge

            With _WInvoiceCharge
                .ocmclear.Visible = False
                .FTInvoiceNo.Properties.ReadOnly = True
                .FTInvoiceNo.Properties.Buttons(0).Enabled = False
                .FTInvoiceNo.Text = _FTInvoiceNo
                .FNHSysSuplId.Text = _FTSuplCode
            End With

            Dim _TmpModule As String = HI.ST.SysInfo.ModuleName
            Dim _TmpMenu As String = HI.ST.SysInfo.MenuName

            HI.ST.SysInfo.MenuName = "MnuAccInvoiceCharge"
            Dim _WShow As New HI.TLF.wShowData(_WInvoiceCharge, _FTInvoiceNo & "|" & _FFNHSysSuplId)
            HI.ST.SysInfo.MenuName = _TmpMenu

            With _WShow
                .WindowState = System.Windows.Forms.FormWindowState.Maximized
                .StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
                .ShowDialog()
            End With

        End With

    End Sub

End Class
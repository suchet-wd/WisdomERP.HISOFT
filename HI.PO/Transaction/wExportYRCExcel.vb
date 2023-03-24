Public Class wExportYRCExcel 

    Private Sub LoadDataInfo(Optional ExportExcel As Boolean = False)
        Me.ogcdetail.DataSource = Nothing
        Dim dt As New DataTable
        Dim _Qry As String

        Dim _Spls As New HI.TL.SplashScreen("Loading...")

        Try
            _Qry = " Exec  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.SP_Generate_ExportYRC '" & HI.UL.ULF.rpQuoted(Me.FNHSysSuplId.Text) & "','','','" & HI.UL.ULDate.ConvertEnDB(Me.FTSPODate.Text) & "','" & HI.UL.ULDate.ConvertEnDB(Me.FTEPODate.Text) & "' "
            dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PUR)

            Me.ogcdetail.DataSource = dt.Copy
            Me.ogvdetail.BestFitColumns()

            dt.Dispose()
        Catch ex As Exception
        End Try

        _Spls.Close()

        'If ExportExcel Then

        '    'ogvdetail.ClearColumnsFilter()
        '    'ogvdetail.ActiveFilter.Clear()

        '    If dt.Rows.Count <= 0 Then
        '        HI.MG.ShowMsg.mInfo("ไม่พบข้อมูลที่ต้องการทำการ Export กรุณาทำการตรวจสอบ !!!", 1505140001, Me.Text)
        '    Else
        '        Try
        '            Dim Op As New System.Windows.Forms.SaveFileDialog
        '            Op.Filter = "Excel Files(.xlsx)|*.xlsx"
        '            Op.ShowDialog()
        '            Try
        '                If Op.FileName <> "" Then
        '                    With ogcdetail
        '                        .ExportToXlsx(Op.FileName)
        '                        Try
        '                            Process.Start(Op.FileName)
        '                        Catch ex As Exception
        '                        End Try
        '                    End With
        '                End If
        '            Catch ex As Exception
        '            End Try
        '        Catch ex As Exception
        '        End Try
        '    End If
        'End If

    End Sub

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ocmload_Click(sender As Object, e As EventArgs) Handles ocmload.Click
        If Me.FNHSysSuplId.Text <> "" Then
            Call LoadDataInfo()
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, FNHSysSuplId_lbl.Text)
            FNHSysSuplId.Focus()
        End If
    End Sub

    Private Sub ocmexportrycexcel_Click(sender As Object, e As EventArgs) Handles ocmexportrycexcel.Click
        If Me.FNHSysSuplId.Text <> "" Then
            'Call LoadDataInfo(True)

            If Me.ogvdetail.RowCount <= 0 Then
                HI.MG.ShowMsg.mInfo("ไม่พบข้อมูลที่ต้องการทำการ Export กรุณาทำการตรวจสอบ !!!", 1505140001, Me.Text)
            Else
                Try
                    Dim Op As New System.Windows.Forms.SaveFileDialog
                    Op.Filter = "Excel Files(.xlsx)|*.xlsx"
                    Op.ShowDialog()
                    Try
                        If Op.FileName <> "" Then
                            With ogcdetail
                                .ExportToXlsx(Op.FileName)
                                Try
                                    Process.Start(Op.FileName)
                                Catch ex As Exception
                                End Try
                            End With
                        End If
                    Catch ex As Exception
                    End Try
                Catch ex As Exception
                End Try
            End If
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, FNHSysSuplId_lbl.Text)
            FNHSysSuplId.Focus()
        End If
    End Sub

    Private Sub wExportYRCExcel_Load(sender As Object, e As EventArgs) Handles Me.Load
        FNHSysSuplId.Text = "YRCTH0F"
    End Sub

    Private Sub ogvdetail_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles ogvdetail.KeyDown
        Try
            Select Case e.KeyCode
                Case System.Windows.Forms.Keys.Delete
                    With Me.ogvdetail
                        If .FocusedRowHandle < 0 Then Exit Sub

                        Dim _PoNo As String = "" & .GetFocusedRowCellValue("FTPurchaseNo").ToString

                        If HI.MG.ShowMsg.mConfirmProcess("คุณต้องการทำการลบรายการ PO นี้ออกจากรายการ Export ใช่หรือไม่ ?", 1512240574, _PoNo) = True Then
                            Dim _dt As DataTable

                            With CType(Me.ogcdetail.DataSource, DataTable)
                                .AcceptChanges()
                                _dt = .Copy
                            End With

                            _dt.BeginInit()
                            For Each R As DataRow In _dt.Select("FTPurchaseNo='" & HI.UL.ULF.rpQuoted(_PoNo) & "'")
                                R.Delete()
                            Next
                            _dt.EndInit()

                            Me.ogcdetail.DataSource = _dt.Copy
                            Me.ogvdetail.RefreshData()

                            _dt.Dispose()
                        End If
                    End With
            End Select
        Catch ex As Exception

        End Try
    End Sub
End Class
Public Class wListRMDSMappingSupl


    Private Sub ocmdelete_Click(sender As System.Object, e As System.EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub


    Public Sub LoadSuplier()
        Dim cmdstring As String = ""

        cmdstring = " Select  FTSuplCode, FTSuplNameEN As FTSuplName, FNHSysSuplId "
        cmdstring &= vbCrLf & "    From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier AS S WITH(NOLOCK) "
        cmdstring &= vbCrLf & "  WHERE FTStateActive='1'  "
        cmdstring &= vbCrLf & "  Order By FTSuplCode "

        Dim dt As DataTable = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_MASTER)

        ReposCFTSuplCode.DataSource = dt.Copy()

        dt.Dispose()

    End Sub


    Public Sub LoadUnit()
        Dim cmdstring As String = ""

        cmdstring = " Select  FTUnitCode, FTUnitNameEN As FTUnitName, FNHSysUnitId "
        cmdstring &= vbCrLf & "    From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit AS S WITH(NOLOCK) "
        cmdstring &= vbCrLf & "  WHERE FTStateActive='1' AND FTStateUnitPurchase='1' "
        cmdstring &= vbCrLf & "  Order By FTUnitCode "

        Dim dt As DataTable = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_MASTER)

        RepositoryFTUnitCode.DataSource = dt.Copy()

        dt.Dispose()

    End Sub

    Private Sub ocmsendapprove_Click(sender As System.Object, e As System.EventArgs) Handles ocmsave.Click
        Try

            Select Case otb.SelectedTabPageIndex
                Case 0
                    With ogvlist
                        .FocusedRowHandle = 0
                        .FocusedColumn = .Columns.ColumnByName("FTSuplCode")
                    End With
                    Dim dtmap As DataTable

                    With CType(Me.ogclist.DataSource, DataTable)
                        .AcceptChanges()
                        dtmap = .Copy

                    End With

                    If dtmap.Select("FTSuplCode<>''").Length <= 0 Then
                        HI.MG.ShowMsg.mProcessError(1403040001, "กรุณาทำการเลือกข้อมูล !!!", Me.Text, System.Windows.Forms.MessageBoxIcon.Error)
                        Exit Sub
                    End If

                    Dim _Str As String = ""
                    For Each R As DataRow In dtmap.Select("FTSuplCode<>''")

                        _Str = "Update [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.THITRMDSMasterFileSuplMap "
                        _Str &= vbCrLf & " SET FNHSysSuplId=" & Val(R!FNHSysSuplId.ToString) & ""
                        _Str &= vbCrLf & ", FTSuplCode='" & HI.UL.ULF.rpQuoted(R!FTSuplCode.ToString) & "'"
                        _Str &= vbCrLf & " WHERE FTSupplierLocationCode='" & HI.UL.ULF.rpQuoted(R!FTSupplierLocationCode.ToString) & "' "

                        If HI.Conn.SQLConn.ExecuteNonQuery(_Str, Conn.DB.DataBaseName.DB_MERCHAN) = False Then
                            _Str = " insert into [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.THITRMDSMasterFileSuplMap (FTSupplierLocationCode, FTSuplCode, FNHSysSuplId)"
                            _Str &= vbCrLf & " select '" & HI.UL.ULF.rpQuoted(R!FTSupplierLocationCode.ToString) & "','" & HI.UL.ULF.rpQuoted(R!FTSuplCode.ToString) & "'," & Val(R!FNHSysSuplId.ToString) & " "
                            HI.Conn.SQLConn.ExecuteNonQuery(_Str, Conn.DB.DataBaseName.DB_MERCHAN)
                        End If

                    Next

                    HI.MG.ShowMsg.mInfo("Save Data Supplier Mapping Complete ", 1499940002, Me.Text)

                Case 1
                    With ogvlistunit
                        .FocusedRowHandle = 0
                        .FocusedColumn = .Columns.ColumnByName("FTUnitCode")
                    End With
                    Dim dtmap As DataTable

                    With CType(Me.ogclistunit.DataSource, DataTable)
                        .AcceptChanges()
                        dtmap = .Copy

                    End With

                    If dtmap.Select("FTUnitCode<>''").Length <= 0 Then
                        HI.MG.ShowMsg.mProcessError(1403040001, "กรุณาทำการเลือกข้อมูล !!!", Me.Text, System.Windows.Forms.MessageBoxIcon.Error)
                        Exit Sub
                    End If

                    Dim _Str As String = ""
                    For Each R As DataRow In dtmap.Select("FTUnitCode<>''")

                        _Str = "Update [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.THITRMDSMasterFileUnitMap "
                        _Str &= vbCrLf & " SET FNHSysUnitId=" & Val(R!FNHSysUnitId.ToString) & ""
                        _Str &= vbCrLf & ", FTUnitCode='" & HI.UL.ULF.rpQuoted(R!FTUnitCode.ToString) & "'"
                        _Str &= vbCrLf & " WHERE FTSellingUOM='" & HI.UL.ULF.rpQuoted(R!FTSellingUOM.ToString) & "' "
                        _Str &= vbCrLf & "        AND FNQty=" & Val(R!FNQty.ToString) & " "
                        _Str &= vbCrLf & "        AND FTUOMDesc='" & HI.UL.ULF.rpQuoted(R!FTUOMDesc.ToString) & "' "


                        If HI.Conn.SQLConn.ExecuteNonQuery(_Str, Conn.DB.DataBaseName.DB_MERCHAN) = False Then

                            _Str = " insert into [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.THITRMDSMasterFileUnitMap (FTSellingUOM, FNQty, FTUOMDesc, FTUnitCode, FNHSysUnitId)"
                            _Str &= vbCrLf & " select '" & HI.UL.ULF.rpQuoted(R!FTSellingUOM.ToString) & "'," & Val(R!FNQty.ToString) & ",'" & HI.UL.ULF.rpQuoted(R!FTUOMDesc.ToString) & "','" & HI.UL.ULF.rpQuoted(R!FTUnitCode.ToString) & "'," & Val(R!FNHSysUnitId.ToString) & " "
                            HI.Conn.SQLConn.ExecuteNonQuery(_Str, Conn.DB.DataBaseName.DB_MERCHAN)

                        End If
                    Next

                    HI.MG.ShowMsg.mInfo("Save Data Unit Mapping Complete ", 1499940003, Me.Text)
            End Select

        Catch ex As Exception

        End Try

    End Sub

    Private Sub ReposCFTSuplCode_EditValueChanged(sender As Object, e As EventArgs) Handles ReposCFTSuplCode.EditValueChanged
        Try

            With Me.ogvlist
                If .FocusedRowHandle < 0 Then Exit Sub

                Dim obj As DevExpress.XtraEditors.GridLookUpEdit = DirectCast(sender, DevExpress.XtraEditors.GridLookUpEdit)
                .SetFocusedRowCellValue("FNHSysSuplId", obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FNHSysSuplId").ToString)
                .SetFocusedRowCellValue("FTSuplName", obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTSuplName").ToString)


                Try
                    obj.Properties.View.ClearColumnsFilter()
                Catch ex As Exception

                End Try

            End With

            CType(Me.ogclist.DataSource, DataTable).AcceptChanges()

        Catch ex As Exception
        End Try

    End Sub

    Private Sub RepositoryFTUnitCode_EditValueChanged(sender As Object, e As EventArgs) Handles RepositoryFTUnitCode.EditValueChanged
        Try

            With Me.ogvlistunit
                If .FocusedRowHandle < 0 Then Exit Sub

                Dim obj As DevExpress.XtraEditors.GridLookUpEdit = DirectCast(sender, DevExpress.XtraEditors.GridLookUpEdit)
                .SetFocusedRowCellValue("FNHSysUnitId", obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FNHSysUnitId").ToString)
                .SetFocusedRowCellValue("FTUnitName", obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTUnitName").ToString)


                Try
                    obj.Properties.View.ClearColumnsFilter()
                Catch ex As Exception

                End Try

            End With

            CType(Me.ogclistunit.DataSource, DataTable).AcceptChanges()

        Catch ex As Exception
        End Try
    End Sub
End Class
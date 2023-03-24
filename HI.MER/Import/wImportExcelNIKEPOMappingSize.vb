Public Class wImportExcelNIKEPOMappingSize


    Private Sub ocmexit_Click(sender As System.Object, e As System.EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub


    Public Sub LoadWisdomSize()
        Dim cmdstring As String = ""

        cmdstring = " Select  FTMatSizeCode AS FTMapSizeExtension "
        cmdstring &= vbCrLf & "    From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMatSize AS S WITH(NOLOCK) "
        cmdstring &= vbCrLf & "  WHERE FTStateActive='1'  "
        cmdstring &= vbCrLf & "  Order By FNMatSizeSeq "

        Dim dt As DataTable = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_MASTER)

        ReposCFTSuplCode.DataSource = dt.Copy()

        dt.Dispose()

    End Sub

    Private Sub ocmsendapprove_Click(sender As System.Object, e As System.EventArgs) Handles ocmsave.Click
        Try

            Select Case otb.SelectedTabPageIndex
                Case 0
                    With ogvlist
                        .FocusedRowHandle = 0
                        .FocusedColumn = .Columns.ColumnByName("SIZE_GRID_VALUE")
                    End With
                    Dim dtmap As DataTable

                    With CType(Me.ogclist.DataSource, DataTable)
                        .AcceptChanges()
                        dtmap = .Copy

                    End With

                    If dtmap.Select("FTMapSizeExtension=''").Length > 0 Then
                        HI.MG.ShowMsg.mProcessError(1403049001, "กรุณาทำการเลือกข้อมูล Mapping Size  !!!", Me.Text, System.Windows.Forms.MessageBoxIcon.Error)
                        Exit Sub
                    End If

                    Dim _Str As String = ""
                    For Each R As DataRow In dtmap.Select("FTMapSizeExtension<>''")

                        _Str = "Update [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERMMapSize "
                        _Str &= vbCrLf & " SET  FTMapSizeExtension='" & HI.UL.ULF.rpQuoted(R!FTMapSizeExtension.ToString) & "'"
                        _Str &= vbCrLf & " WHERE FTMapSize='" & HI.UL.ULF.rpQuoted(R!SIZE_GRID_VALUE.ToString) & "' "

                        If HI.Conn.SQLConn.ExecuteNonQuery(_Str, Conn.DB.DataBaseName.DB_MERCHAN) = False Then
                            _Str = " insert into [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERMMapSize (FTMapSize, FTMapSizeExtension)"
                            _Str &= vbCrLf & " select '" & HI.UL.ULF.rpQuoted(R!SIZE_GRID_VALUE.ToString) & "','" & HI.UL.ULF.rpQuoted(R!FTMapSizeExtension.ToString) & "' "
                            HI.Conn.SQLConn.ExecuteNonQuery(_Str, Conn.DB.DataBaseName.DB_MERCHAN)
                        End If


                        _Str = "Update [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTImportOrderSizeBreakdownTemp "
                        _Str &= vbCrLf & " SET  FTSizeBreakdownCode='" & HI.UL.ULF.rpQuoted(R!FTMapSizeExtension.ToString) & "'"
                        _Str &= vbCrLf & " WHERE FTUserLogin='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' AND  FTSizeBreakdownCode='" & HI.UL.ULF.rpQuoted(R!SIZE_GRID_VALUE.ToString) & "' "
                        HI.Conn.SQLConn.ExecuteNonQuery(_Str, Conn.DB.DataBaseName.DB_MERCHAN)


                    Next

                    Dim nFNHSysMatSizeId As Integer = 0
                    For Each R As DataRow In dtmap.Select("FTMapSizeExtension=''")



                        nFNHSysMatSizeId = Val(HI.TL.RunID.GetRunNoID("TMERMMatSize", "FNHSysMatSizeId", HI.Conn.DB.DataBaseName.DB_MASTER).ToString())

                        _Str = ""
                        _Str = "DECLARE @FNMatSizeSeqMax AS numeric(18,2);"
                        _Str &= Environment.NewLine & "SELECT @FNMatSizeSeqMax = MAX(A.FNMatSizeSeq)"
                        _Str &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMatSize] AS A WITH(NOLOCK)"
                        _Str &= Environment.NewLine & "GROUP BY A.FNHSysMatSizeId;"
                        'sSQL &= Environment.NewLine & "--PRINT 'FNMatSizeSeqMax Current : ' + CONVERT(VARCHAR(10),ISNULL(@FNMatSizeSeqMax, 1));"
                        'sSQL &= Environment.NewLine & "--PRINT 'FNMatSizeSeq Max Next : ' + CONVERT(VARCHAR(10),(ISNULL(@FNMatSizeSeqMax, 1) + 1));"
                        _Str &= Environment.NewLine & "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMatSize] ([FTInsUser],[FDInsDate],[FTInsTime]"
                        _Str &= Environment.NewLine & "                                                                                                    ,[FTUpdUser],[FDUpdDate],[FTUpdTime]"
                        _Str &= Environment.NewLine & "                                                                                                    ,[FNHSysMatSizeId],[FTMatSizeCode],[FNMatSizeSeq]"
                        _Str &= Environment.NewLine & "							                                                                           ,[FTMatSizeNameTH],[FTMatSizeNameEN],[FTRemark],[FTStateActive])"
                        _Str &= Environment.NewLine & "VALUES(NULL, NULL, NULL"
                        _Str &= ",NULL, NULL, NULL"
                        _Str &= ", " & nFNHSysMatSizeId & ", N'" & HI.UL.ULF.rpQuoted(R!SIZE_GRID_VALUE.ToString) & "', (ISNULL(@FNMatSizeSeqMax, 0) + 1)"
                        _Str &= ", N'" & HI.UL.ULF.rpQuoted(R!SIZE_GRID_VALUE.ToString) & "', N'" & HI.UL.ULF.rpQuoted(R!SIZE_GRID_VALUE.ToString) & "', '', '1');"

                        If HI.Conn.SQLConn.ExecuteNonQuery(_Str, HI.Conn.DB.DataBaseName.DB_MASTER) = True Then

                        End If


                    Next





                    HI.MG.ShowMsg.mInfo("Save Data Size Breakdown Mapping Complete ", 1499940882, Me.Text)

                Case 1

            End Select

        Catch ex As Exception
        End Try

    End Sub

    Private Sub ReposCFTSuplCode_EditValueChanged(sender As Object, e As EventArgs) Handles ReposCFTSuplCode.EditValueChanged
    End Sub


End Class
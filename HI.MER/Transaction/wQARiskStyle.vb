Public Class wQARiskStyle
    Private Sub ocmExit_Click(sender As Object, e As EventArgs) Handles ocmExit.Click
        Me.Close()
    End Sub

    Private Sub LoadData()
        Dim cmdstring As String = ""

        cmdstring = "SELECT CASE WHEN ISNULL(B.FNHSysStyleId,0) > 0 THEN '1' ELSE '0' END AS FTSelect  "
        cmdstring &= vbCrLf & ",A.FTStyleCode"
        cmdstring &= vbCrLf & ",A.FTStyleName"
        cmdstring &= vbCrLf & ",A.FTSeasonCode"
        cmdstring &= vbCrLf & ",A.FNHSysStyleId"
        cmdstring &= vbCrLf & ",A.FNHSysSeasonId"
        cmdstring &= vbCrLf & ",CASE WHEN ISNULL(B.FNHSysStyleId,0) > 0 THEN '1' ELSE '0' END AS FTSelectOrg"
        cmdstring &= vbCrLf & ",ISNULL(B.FTNote,'') AS FTNote"
        cmdstring &= vbCrLf & ",ISNULL(B.FTNote,'') AS FTNoteORG"
        cmdstring &= vbCrLf & " FROM (  Select  ST.FTStyleCode "

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            cmdstring &= vbCrLf & "  , MAX(ST.FTStyleNameTH) AS FTStyleName"
        Else
            cmdstring &= vbCrLf & "  , MAX(ST.FTStyleNameEN) As FTStyleName"
        End If

        cmdstring &= vbCrLf & "  , SS.FTSeasonCode"
        cmdstring &= vbCrLf & " , ST.FNHSysStyleId "
        cmdstring &= vbCrLf & " , SS.FNHSysSeasonId"
        cmdstring &= vbCrLf & " From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder As A WITH(NOLOCK) INNER Join"
        cmdstring &= vbCrLf & "       [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS ST  WITH(NOLOCK)  ON A.FNHSysStyleId = ST.FNHSysStyleId INNER Join"
        cmdstring &= vbCrLf & "       [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMSeason As SS  WITH(NOLOCK) On A.FNHSysSeasonId = SS.FNHSysSeasonId"
        cmdstring &= vbCrLf & "  WHERE A.FNHSysProdTypeId=" & Val(FNHSysProdTypeId.Properties.Tag.ToString) & " "

        If Me.FNHSysBuyId.Text <> "" Then
            cmdstring &= vbCrLf & "  AND  A.FNHSysBuyId=" & Val(FNHSysBuyId.Properties.Tag.ToString) & " "
        End If

        cmdstring &= vbCrLf & " Group By ST.FTStyleCode, ST.FNHSysStyleId, SS.FNHSysSeasonId, SS.FTSeasonCode) AS A LEFT OUTER JOIN "
        cmdstring &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTQARiskStyle As B WITH(NOLOCK) ON A.FNHSysStyleId = B.FNHSysStyleId AND A.FNHSysSeasonId = B.FNHSysSeasonId "
        cmdstring &= vbCrLf & " ORDER BY A.FTStyleCode,A.FTSeasonCode "

        Dim dt As DataTable = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_MERCHAN)
        Me.ogddetail.DataSource = dt.Copy()
        dt.Dispose()

    End Sub
    Private Sub ocmloaddata_Click(sender As Object, e As EventArgs) Handles ocmloaddata.Click
        If Me.FNHSysProdTypeId.Text <> "" Then

            Call LoadData()


        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FNHSysProdTypeId_lbl.Text)
            FNHSysProdTypeId.Focus()
        End If
    End Sub

    Private Sub ocmSave_Click(sender As Object, e As EventArgs) Handles ocmSave.Click
        If Me.FNHSysProdTypeId.Text <> "" Then
            If Not (Me.ogddetail.DataSource Is Nothing) Then

                Dim dt As DataTable
                With CType(ogddetail.DataSource, DataTable)
                    .AcceptChanges()
                    dt = .Copy
                End With

                If dt.Rows.Count > 0 Then
                    If HI.MG.ShowMsg.mConfirmProcess(MG.ShowMsg.ProcessType.mSave,, Me.Text) = True Then

                        Dim spls As New HI.TL.SplashScreen("Saving....")
                        Try

                            Dim cmdstring As String = ""
                            For Each R As DataRow In dt.Select("(FTSelect <> FTSelectOrg) OR (FTSelect='1' AND FTNote<>FTNoteORG)")
                                Select Case R!FTSelect.ToString
                                    Case "1"

                                        If R!FTSelectOrg.ToString = "1" Then

                                            cmdstring = "UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTQARiskStyle "
                                            cmdstring &= vbCrLf & " SET FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                                            cmdstring &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB & " "
                                            cmdstring &= vbCrLf & ",FTUpdTime" & HI.UL.ULDate.FormatTimeDB & " "
                                            cmdstring &= vbCrLf & ",FTNote'" & HI.UL.ULF.rpQuoted(R!FTNote.ToString) & "'"
                                            cmdstring &= vbCrLf & " WHERE FNHSysStyleId=" & Val(R!FNHSysStyleId.ToString) & " "
                                            cmdstring &= vbCrLf & "       AND FNHSysSeasonId=" & Val(R!FNHSysSeasonId.ToString) & " "

                                            HI.Conn.SQLConn.ExecuteOnly(cmdstring, Conn.DB.DataBaseName.DB_MERCHAN)

                                        Else

                                            cmdstring = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTQARiskStyle "
                                            cmdstring &= vbCrLf & "  (FTInsUser, FDInsDate, FTInsTime,  FNHSysStyleId, FNHSysSeasonId, FTNote)"
                                            cmdstring &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                                            cmdstring &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & " "
                                            cmdstring &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & " "
                                            cmdstring &= vbCrLf & "," & Val(R!FNHSysStyleId.ToString) & " "
                                            cmdstring &= vbCrLf & "," & Val(R!FNHSysSeasonId.ToString) & " "
                                            cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTNote.ToString) & "'"

                                            HI.Conn.SQLConn.ExecuteOnly(cmdstring, Conn.DB.DataBaseName.DB_MERCHAN)

                                        End If

                                    Case Else

                                        cmdstring = " DELETE FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTQARiskStyle "
                                        cmdstring &= vbCrLf & " WHERE FNHSysStyleId=" & Val(R!FNHSysStyleId.ToString) & " "
                                        cmdstring &= vbCrLf & "       AND FNHSysSeasonId=" & Val(R!FNHSysSeasonId.ToString) & " "

                                        HI.Conn.SQLConn.ExecuteOnly(cmdstring, Conn.DB.DataBaseName.DB_MERCHAN)

                                End Select

                            Next

                            Call LoadData()
                            spls.Close()
                            HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)

                        Catch ex As Exception
                            spls.Close()
                            HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                        End Try

                        dt.Dispose()
                    Else
                        dt.Dispose()
                    End If
                Else
                    dt.Dispose()
                    HI.MG.ShowMsg.mInfo("ไม่พบข้อมูลที่ต้องการทำการบันทึก !!!", 1705541248, Me.Text,, System.Windows.Forms.MessageBoxIcon.Warning)
                    FNHSysProdTypeId.Focus()
                End If


            Else
                HI.MG.ShowMsg.mInfo("ไม่พบข้อมูลที่ต้องการทำการบันทึก !!!", 1705541248, Me.Text,, System.Windows.Forms.MessageBoxIcon.Warning)
                FNHSysProdTypeId.Focus()
            End If
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FNHSysProdTypeId_lbl.Text)
            FNHSysProdTypeId.Focus()
        End If

    End Sub

    Private Sub ocmclearclsr_Click(sender As Object, e As EventArgs) Handles ocmclearclsr.Click
        HI.TL.HandlerControl.ClearControl(Me)
        FNHSysProdTypeId.Focus()
    End Sub
End Class
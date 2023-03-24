Imports DevExpress.XtraEditors.Controls

Public Class wRDSamAppTracking
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ocmload_Click(sender As Object, e As EventArgs) Handles ocmload.Click

        If VerifyField() Then

            Dim _Spls As New HI.TL.SplashScreen("Loading...Data Please wait.")

            Call LoadData()

            _Spls.Close()

        End If

    End Sub


    Private Sub LoadData()

        Dim Qry As String = ""
        Dim dt As DataTable

        Try

            Qry = " Select  '0' AS FTSelect"
            Qry &= vbCrLf & ", A.FTSMPOrderNo"
            Qry &= vbCrLf & ", A.FNHSysSeasonId"
            Qry &= vbCrLf & ", A.FNHSysStyleId"
            Qry &= vbCrLf & ", A.FTRemark"
            Qry &= vbCrLf & ", A.FNSam"
            Qry &= vbCrLf & ", A.FNOperater"
            Qry &= vbCrLf & ", A.FNCost"
            Qry &= vbCrLf & ", A.FNMinuteHour"
            Qry &= vbCrLf & ", A.FNProdPersonPerDay"
            Qry &= vbCrLf & ", A.FNWorkingTimeMinuteDay"
            Qry &= vbCrLf & ", A.FNTargetPerDay"
            Qry &= vbCrLf & ", A.FTStateApp"
            Qry &= vbCrLf & ", A.FNSamCut"
            Qry &= vbCrLf & ", A.FNCostCut"
            Qry &= vbCrLf & ", A.FNNetCostCut"
            Qry &= vbCrLf & ", A.FNSamPack"
            Qry &= vbCrLf & ", A.FNCostPack  "
            Qry &= vbCrLf & ", A.FNNetCostPack "
            Qry &= vbCrLf & ", A.FTStateSendMerApp "
            Qry &= vbCrLf & ", A.FTStateMerApp "
            Qry &= vbCrLf & ", A.FTStateToGE "
            Qry &= vbCrLf & ", SMP.FTSMPOrderBy "
            Qry &= vbCrLf & ", ST.FTStyleCode "
            Qry &= vbCrLf & ", SS.FTSeasonCode "
            Qry &= vbCrLf & " From " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & ".dbo.TRDTRDSam As A With(NOLOCK) INNER Join "
            Qry &= vbCrLf & "  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & ".dbo.TSMPOrder As SMP With(NOLOCK) On A.FTSMPOrderNo = SMP.FTSMPOrderNo LEFT OUTER Join "
            Qry &= vbCrLf & "  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TMERMSeason As SS With(NOLOCK) On A.FNHSysSeasonId = SS.FNHSysSeasonId LEFT OUTER Join "
            Qry &= vbCrLf & "  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TMERMStyle As ST With(NOLOCK) On A.FNHSysStyleId = ST.FNHSysStyleId "

            If HI.ST.SysInfo.Admin Then
                Qry &= vbCrLf & "	 WHERE   A.FTStateApp='1' AND  A.FTStateSendMerApp='1'  AND ISNULL(A.FTStateMerApp,'')<>'1'"
            Else
                Qry &= vbCrLf & "	 WHERE   SMP.FTSMPOrderBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'  AND A.FTStateApp='1' AND  A.FTStateSendMerApp='1'  AND ISNULL(A.FTStateMerApp,'')<>'1'"
            End If

            Qry &= vbCrLf & " ORDER BY ST.FTStyleCode , SS.FTSeasonCode,A.FTSMPOrderNo"

            dt = HI.Conn.SQLConn.GetDataTable(Qry, Conn.DB.DataBaseName.DB_MERCHAN)
            ogcCostsheet.DataSource = dt

        Catch ex As Exception
        End Try

    End Sub
    Private Sub ocmclearclsr_Click(sender As Object, e As EventArgs) Handles ocmclearclsr.Click
        HI.TL.HandlerControl.ClearControl(Me)
    End Sub

    Private Function VerifyField() As Boolean
        Return True
    End Function

    Private Sub ocmapprove_Click(sender As Object, e As EventArgs) Handles ocmapprove.Click
        Try
            Dim dt As New DataTable

            With CType(Me.ogcCostsheet.DataSource, DataTable)
                .AcceptChanges()
                dt = .Copy()
            End With

            If dt.Select("FTSelect='1'").Length > 0 Then

                Dim Spls As New HI.TL.SplashScreen("Saviing Data ...")

                Try

                    Dim styleid As Integer = 0
                    Dim seasonid As Integer = 0
                    Dim StateGE As Boolean = False

                    Dim StateSamStyle As Boolean = False

                    For Each R As DataRow In dt.Select("FTSelect='1'")


                        styleid = Val(R!FNHSysStyleId.ToString)
                        seasonid = Val(R!FNHSysSeasonId.ToString)

                        Dim cmd As String = ""
                        cmd = "select top 1 FNHSysStyleId  From  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & ".dbo.TRDTGESam  AS x with(nolock) where  (FNHSysStyleId = " & styleid & ")  AND (FNHSysSeasonId = " & seasonid & ")  "

                        StateGE = (HI.Conn.SQLConn.GetField(cmd, Conn.DB.DataBaseName.DB_PLANNING, "") = "")


                        cmd = "select top 1 FNHSysStyleId  From  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & ".dbo.TRDMSamStyle  AS x with(nolock) where  (FNHSysStyleId = " & styleid & ")   "
                        StateSamStyle = (HI.Conn.SQLConn.GetField(cmd, Conn.DB.DataBaseName.DB_PLANNING, "") = "")

                        cmd = ""


                        If StateGE Then
                            cmd = "  UPDATE A SET   FTStateMerApp ='1' , FTStateMerAppBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "', FTStateMerAppDate =" & HI.UL.ULDate.FormatDateDB & ", FTStateMerAppTime=" & HI.UL.ULDate.FormatTimeDB & ", FTStateToGE ='1' "
                            cmd &= vbCrLf & "  From  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & ".dbo.TRDTRDSam  AS A  "
                            cmd &= vbCrLf & " Where (FTSMPOrderNo = N'" & HI.UL.ULF.rpQuoted(R!FTSMPOrderNo.ToString()) & "') "
                            cmd &= vbCrLf & " AND (FNHSysStyleId = " & styleid & ") "
                            cmd &= vbCrLf & " AND (FNHSysSeasonId = " & seasonid & ")  AND ISNULL(FTStateMerApp,'')<>'1' "
                        Else
                            cmd = "  UPDATE A SET   FTStateMerApp ='1' , FTStateMerAppBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "', FTStateMerAppDate =" & HI.UL.ULDate.FormatDateDB & ", FTStateMerAppTime=" & HI.UL.ULDate.FormatTimeDB & ", FTStateToGE ='0' "
                            cmd &= vbCrLf & "  From  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & ".dbo.TRDTRDSam  AS A  "
                            cmd &= vbCrLf & " Where (FTSMPOrderNo = N'" & HI.UL.ULF.rpQuoted(R!FTSMPOrderNo.ToString()) & "') "
                            cmd &= vbCrLf & " AND (FNHSysStyleId = " & styleid & ") "
                            cmd &= vbCrLf & " AND (FNHSysSeasonId = " & seasonid & ") AND ISNULL(FTStateMerApp,'')<>'1' "
                        End If


                        cmd &= vbCrLf & "  UPDATE AX SET   FTStateMerApp ='1' , FTStateMerAppBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "', FTStateMerAppDate =" & HI.UL.ULDate.FormatDateDB & ", FTStateMerAppTime=" & HI.UL.ULDate.FormatTimeDB & ", FTStateToGE ='0' "
                        cmd &= vbCrLf & "  From  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & ".dbo.TRDTRDSam  AS AX  "
                        cmd &= vbCrLf & " Where (FTSMPOrderNo <> N'" & HI.UL.ULF.rpQuoted(R!FTSMPOrderNo.ToString()) & "') "
                        cmd &= vbCrLf & " AND (FNHSysStyleId = " & styleid & ") "
                        cmd &= vbCrLf & " AND (FNHSysSeasonId = " & seasonid & ") AND ISNULL(FTStateMerApp,'')<>'1' "


                        If StateGE Then

                            cmd &= vbCrLf & "  INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TRDTGESam ( "
                            cmd &= vbCrLf & "   FNHSysStyleId, FNHSysSeasonId, FTRemark, FNSam, FNOperater, FNCost, FNMinuteHour, FNProdPersonPerDay, FNWorkingTimeMinuteDay, FNTargetPerDay, FTStateNewFromRD, FTStateFromRDBy "
                            cmd &= vbCrLf & " ,FTStateFromRDDate, FTStateFromRDTime, FNRDSam, FNSamCut, FNCostCut, FNNetCostCut, FNSamPack, FNCostPack, FNNetCostPack "
                            cmd &= vbCrLf & "  ) "
                            cmd &= vbCrLf & "  SELECT TOP 1  FNHSysStyleId, FNHSysSeasonId, FTRemark, FNSam, FNOperater, FNCost, FNMinuteHour, FNProdPersonPerDay, FNWorkingTimeMinuteDay, FNTargetPerDay,'1' AS  FTStateNewFromRD,'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' AS FTStateFromRDBy "
                            cmd &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & " AS FTStateFromRDDate, " & HI.UL.ULDate.FormatTimeDB & " AS FTStateFromRDTime, FNSam, FNSamCut, FNCostCut, FNNetCostCut, FNSamPack, FNCostPack, FNNetCostPack "
                            cmd &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TRDTRDSam "
                            cmd &= vbCrLf & " Where (FTSMPOrderNo = N'" & HI.UL.ULF.rpQuoted(R!FTSMPOrderNo.ToString()) & "') "
                            cmd &= vbCrLf & "  INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TRDTGESam_Detail  ( "
                            cmd &= vbCrLf & "   FNHSysStyleId, FNHSysSeasonId, FNSeq, FNHSysRDOperationId, FNHSysRDMachineTypeId, FNSam, FNOperater, FNCost, FNOutputPer1Hour, FNOutputPer8Hour, FTRemark, FTStateNoSew, FNHSysRDPositionPartId, FTLenght, FTAdditionalInfo, FTAttachment,FNSamBF"
                            cmd &= vbCrLf & "  ) "
                            cmd &= vbCrLf & "  SELECT  " & styleid & " AS FNHSysStyleId," & seasonid & " AS  FNHSysSeasonId, FNSeq, FNHSysRDOperationId, FNHSysRDMachineTypeId, FNSam, FNOperater, FNCost, FNOutputPer1Hour, FNOutputPer8Hour, FTRemark, FTStateNoSew, FNHSysRDPositionPartId, FTLenght, FTAdditionalInfo, FTAttachment,FNSam"
                            cmd &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TRDTRDSam_Detail "
                            cmd &= vbCrLf & " Where (FTSMPOrderNo = N'" & HI.UL.ULF.rpQuoted(R!FTSMPOrderNo.ToString()) & "') "

                        End If


                        If StateSamStyle Then

                            cmd &= vbCrLf & "  INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TRDMSamStyle ( "
                            cmd &= vbCrLf & "  FTInsUser, FDInsDate, FTInsTime, FNHSysStyleId, FNSampleSam, FNGTMSam, FN1stProdSam, FNBulkSam, FNSMVSam "
                            cmd &= vbCrLf & "  ) "

                            cmd &= vbCrLf & "  SELECT TOP 1  '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ", FNHSysStyleId,  FNSam,0,0,0,0 "
                            cmd &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TRDTRDSam "
                            cmd &= vbCrLf & " Where (FTSMPOrderNo = N'" & HI.UL.ULF.rpQuoted(R!FTSMPOrderNo.ToString()) & "') "



                        End If


                        If HI.Conn.SQLConn.ExecuteNonQuery(cmd, Conn.DB.DataBaseName.DB_PLANNING) Then

                        End If

                    Next

                Catch ex As Exception
                End Try


                Call LoadData()

                Spls.Close()

            Else

                HI.MG.ShowMsg.mInfo("กรุณาทำการ เลือกรายการ เพื่อทำการ อนุมัติ ส่งค่า ไป ยัง แผนก GE !!! ", 1910050678, Me.Text,, System.Windows.Forms.MessageBoxIcon.Warning)

            End If

        Catch ex As Exception
        End Try

    End Sub

    Private Sub RepFTSelect_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles RepFTSelect.EditValueChanging
        Try

            Dim styleid As Integer = 0
            Dim seasonid As Integer = 0
            Dim SmpOrderNo As String = ""
            Dim State As String = "0"

            If e.NewValue.ToString = "1" Then
                State = "1"
            End If

            With Me.ogvCostsheet
                styleid = Val(.GetFocusedRowCellValue("FNHSysStyleId").ToString)
                seasonid = Val(.GetFocusedRowCellValue("FNHSysSeasonId").ToString)
                SmpOrderNo = .GetFocusedValue("FTSMPOrderNo").ToString()

            End With


            With CType(Me.ogcCostsheet.DataSource, DataTable)
                .AcceptChanges()

                For Each R As DataRow In .Select("FTSMPOrderNo<>'" & HI.UL.ULF.rpQuoted(SmpOrderNo) & "' AND FNHSysStyleId=" & styleid & " AND  FNHSysSeasonId=" & seasonid & "")
                    R!FTSelect = "0"
                Next

                Me.ogvCostsheet.SetFocusedRowCellValue("FTSelect", State)


                .AcceptChanges()
            End With


        Catch ex As Exception

        End Try
    End Sub


End Class
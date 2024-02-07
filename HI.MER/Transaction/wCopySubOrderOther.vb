Public Class wCopySubOrderOther

    Sub New()
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private _AddComponent As Boolean = False
    Public Property AddComponent As Boolean
        Get
            Return _AddComponent
        End Get
        Set(value As Boolean)
            _AddComponent = value
        End Set
    End Property

    Private Sub ocmCancel_Click(sender As Object, e As EventArgs) Handles ocmCancel.Click
        Me.Close()
    End Sub

    Private Sub ocmOK_Click(sender As Object, e As EventArgs) Handles ocmOK.Click

        If Me.FTSubOrderNoSource.Text.Trim <> "" Then
            If Me.FTSubOrderNo.Text <> "" Then
                If FTSubOrderNoTo.ToString <> "" Then
                    If Me.FTSubOrderNoSource.Text <> FTSubOrderNoTo.ToString Then
                        If Me.ockcomponent.Checked Or Me.ockPacking.Checked Or Me.ockSewing.Checked Or Me.ockSizeSpec.Checked Or Me.ockPackRatio.Checked Then
                            ' Add ockPackRatio.Checked By Chet [22 Jan 2024]

                            If HI.MG.ShowMsg.mConfirmProcess("คุณต้องการ ทำการ Copy ใช่หรือไม่ (ข้อมูลปลายทางจะถูกลบ)", 1407100003, Me.FTSubOrderNoSource.Text & " Copy To" & Me.FTSubOrderNo.Text & "  To  " & Me.FTSubOrderNoTo.Text) Then

                                Dim _Qry As String = ""
                                Dim _Qrychk As String = ""
                                Dim _dt As DataTable
                                Dim tSqlRevised As String = ""

                                _Qry = "  SELECT FTSubOrderNo"
                                _Qry &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub AS X WITH(NOLOCK)"
                                _Qry &= vbCrLf & " WHERE  (FTOrderNo = N'" & HI.UL.ULF.rpQuoted(FTOrderNo.Text.Trim()) & "')"
                                _Qry &= vbCrLf & " AND FTSubOrderNo>='" & HI.UL.ULF.rpQuoted(Me.FTSubOrderNo.Text.Trim()) & "'"
                                _Qry &= vbCrLf & " AND FTSubOrderNo<='" & HI.UL.ULF.rpQuoted(Me.FTSubOrderNoTo.Text.Trim()) & "'"
                                _Qry &= vbCrLf & " AND FTSubOrderNo<>'" & HI.UL.ULF.rpQuoted(Me.FTSubOrderNoSource.Text.Trim()) & "'"
                                _Qry &= vbCrLf & " ORDER BY FTSubOrderNo"
                                _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

                                ' Add Check Pack Ratio Information By Chet [22 Jan 2024]
                                If Me.ockPackRatio.Checked Then
                                    Dim _dtPackType As DataTable
                                    Dim _dtChkBreakDown As DataTable

                                    _Qry = "  SELECT ISNULL(FNPackCartonSubType,0) AS 'FNPackCartonSubType'"
                                    _Qry &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub AS X WITH(NOLOCK)"
                                    _Qry &= vbCrLf & " WHERE  (FTOrderNo = N'" & HI.UL.ULF.rpQuoted(FTOrderNo.Text.Trim()) & "')"
                                    _Qry &= vbCrLf & " AND FTSubOrderNo>='" & HI.UL.ULF.rpQuoted(Me.FTSubOrderNo.Text.Trim()) & "'"
                                    _Qry &= vbCrLf & " AND FTSubOrderNo<='" & HI.UL.ULF.rpQuoted(Me.FTSubOrderNoTo.Text.Trim()) & "'"
                                    _dtPackType = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

                                    If (_dtPackType.DefaultView.ToTable(True, "FNPackCartonSubType")).Rows.Count <> 1 Then
                                        HI.MG.ShowMsg.mInfo("Please checking Pack Type !!!", 1000000005, "Please checking Pack Type !!!")
                                        Exit Sub
                                    Else
                                        For Each Rp As DataRow In (_dtPackType.DefaultView.ToTable(True, "FNPackCartonSubType")).Rows
                                            If Rp!FNPackCartonSubType.ToString <> "0" Then

                                                _Qry = "SELECT FTSubOrderNo, FTColorway, FTSizeBreakDown  "
                                                _Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_BreakDown  With(NOLOCK) "
                                                _Qry &= vbCrLf & "WHERE FTOrderNo = '" & HI.UL.ULF.rpQuoted(FTOrderNo.Text.Trim()) & "'"
                                                _dtChkBreakDown = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)
                                                For Each R As DataRow In _dtChkBreakDown.Select("FTSubOrderNo = '" & Me.FTSubOrderNoSource.Text.Trim() & "'")
                                                    For Each Rc As DataRow In _dt.Rows
                                                        If (_dtChkBreakDown.Select("FTSubOrderNo = '" & Rc!FTSubOrderNo.ToString & "'").Count = 0) Then
                                                            HI.MG.ShowMsg.mInfo("Please checking Suborder Breakdown !!!", 1000000005, "Please checking Suborder Breakdown !!!")
                                                            Exit Sub
                                                        Else
                                                            For Each RR As DataRow In _dtChkBreakDown.Select("FTSubOrderNo = '" & Rc!FTSubOrderNo.ToString & "'")
                                                                If R!FTColorway.ToString <> RR!FTColorway.ToString Then
                                                                    HI.MG.ShowMsg.mInfo("Please checking Colorway !!!", 1000000005, "Please checking Colorway !!!")
                                                                    Exit Sub
                                                                End If
                                                                If R!FTSizeBreakDown.ToString <> RR!FTSizeBreakDown.ToString Then
                                                                    HI.MG.ShowMsg.mInfo("Please checking SizeBreakDown !!!", 1000000005, "Please checking SizeBreakDown !!!")
                                                                    Exit Sub
                                                                End If
                                                            Next
                                                        End If
                                                    Next
                                                Next
                                            End If
                                        Next
                                    End If
                                    _dtPackType.Dispose()
                                    _dtChkBreakDown.Dispose()
                                End If
                                ' End Add Check Pack Ratio Information By Chet [22 Jan 2024]

                                _Qry = ""
                                For Each R As DataRow In _dt.Rows
                                    If Me.ockcomponent.Checked Then

                                        _Qry &= vbCrLf & " DELETE FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_Component WHERE FTSubOrderNo='" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "' "
                                        _Qry &= vbCrLf & "INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_Component  ("
                                        _Qry &= vbCrLf & "FTInsUser, FDInsDate, FTInsTime,FTOrderNo, FTSubOrderNo, FNHSysMerMatId, FNPart, FTComponent, FTRemark, FNConSmp, FNSeq,FNDataSeq"
                                        _Qry &= vbCrLf & " )"
                                        _Qry &= vbCrLf & " SELECT "
                                        _Qry &= vbCrLf & "'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                                        _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                                        _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ""
                                        _Qry &= vbCrLf & ",CASE WHEN Charindex('-','" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "') > 2 THEN LEFT('" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "',Charindex('-','" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "')-1) ELSE '" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "' END"
                                        _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "'"
                                        _Qry &= vbCrLf & ", FNHSysMerMatId, FNPart, FTComponent, FTRemark, FNConSmp, FNSeq,FNDataSeq"
                                        _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_Component "
                                        _Qry &= vbCrLf & " WHERE FTSubOrderNo='" & HI.UL.ULF.rpQuoted(Me.FTSubOrderNoSource.Text.Trim()) & "' "

                                        tSqlRevised &= Environment.NewLine & " UPDATE A"
                                        tSqlRevised &= Environment.NewLine & "SET A.FTStateApprovedComponent = N'0'"
                                        tSqlRevised &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub_ApprovedInfo AS A"
                                        tSqlRevised &= Environment.NewLine & "WHERE A.FTOrderNo = '" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text.Trim) & "'"
                                        tSqlRevised &= Environment.NewLine & "      AND A.FTSubOrderNo ='" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "' AND A.FTStateApprovedComponent<>'0' ;"

                                    End If

                                    If Me.ockSewing.Checked Then

                                        _Qry &= vbCrLf & " DELETE FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_Sew WHERE FTSubOrderNo='" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "' "
                                        _Qry &= vbCrLf & "INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_Sew  ("
                                        _Qry &= vbCrLf & "FTInsUser, FDInsDate, FTInsTime,FTOrderNo, FTSubOrderNo,FNSewSeq, FTSewDescription, FTSewNote, FTImage"
                                        _Qry &= vbCrLf & " )"
                                        _Qry &= vbCrLf & " SELECT "
                                        _Qry &= vbCrLf & "'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                                        _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                                        _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ""
                                        _Qry &= vbCrLf & ",CASE WHEN Charindex('-','" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "') > 2 THEN LEFT('" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "',Charindex('-','" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "')-1) ELSE '" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "' END"
                                        _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "'"
                                        _Qry &= vbCrLf & ",FNSewSeq, FTSewDescription, FTSewNote, FTImage"
                                        _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_Sew "
                                        _Qry &= vbCrLf & " WHERE FTSubOrderNo='" & HI.UL.ULF.rpQuoted(Me.FTSubOrderNoSource.Text.Trim()) & "' "


                                        tSqlRevised &= Environment.NewLine & "UPDATE A"
                                        tSqlRevised &= Environment.NewLine & "SET A.FTStateApprovedSewing = N'0'"
                                        tSqlRevised &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub_ApprovedInfo AS A"
                                        tSqlRevised &= Environment.NewLine & "WHERE A.FTOrderNo = '" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text.Trim) & "'"
                                        tSqlRevised &= Environment.NewLine & "      AND A.FTSubOrderNo ='" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "' AND A.FTStateApprovedSewing<>'0';"

                                    End If

                                    If Me.ockPacking.Checked Then

                                        _Qry &= vbCrLf & " DELETE FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_Pack WHERE FTSubOrderNo='" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "' "
                                        _Qry &= vbCrLf & "INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_Pack  ("
                                        _Qry &= vbCrLf & "FTInsUser, FDInsDate, FTInsTime,FTOrderNo, FTSubOrderNo, FNPackSeq, FTPackDescription, FTPackNote, FTImage"
                                        _Qry &= vbCrLf & " )"
                                        _Qry &= vbCrLf & " SELECT "
                                        _Qry &= vbCrLf & "'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                                        _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                                        _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ""
                                        _Qry &= vbCrLf & ",CASE WHEN Charindex('-','" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "') > 2 THEN LEFT('" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "',Charindex('-','" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "')-1) ELSE '" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "' END"
                                        _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "'"
                                        _Qry &= vbCrLf & ", FNPackSeq, FTPackDescription, FTPackNote, FTImage"
                                        _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_Pack "
                                        _Qry &= vbCrLf & " WHERE FTSubOrderNo='" & HI.UL.ULF.rpQuoted(Me.FTSubOrderNoSource.Text.Trim()) & "' "

                                        tSqlRevised &= Environment.NewLine & "UPDATE A"
                                        tSqlRevised &= Environment.NewLine & "SET A.FTStateApprovedPacking = N'0'"
                                        tSqlRevised &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub_ApprovedInfo AS A"
                                        tSqlRevised &= Environment.NewLine & "WHERE A.FTOrderNo = '" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text.Trim) & "'"
                                        tSqlRevised &= Environment.NewLine & "      AND A.FTSubOrderNo ='" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "' AND A.FTStateApprovedPacking<>'0' ;"

                                    End If

                                    If Me.ockSizeSpec.Checked Then

                                        _Qry &= vbCrLf & " DELETE FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_SizeSpec WHERE FTSubOrderNo='" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "' "
                                        _Qry &= vbCrLf & "INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_SizeSpec  ("
                                        _Qry &= vbCrLf & "FTInsUser, FDInsDate, FTInsTime,FTOrderNo, FTSubOrderNo, FNSeq, FNHSysMatSizeId, FTSizeSpecDesc, FTSizeSpecExtension,FNHSysMeasId"
                                        _Qry &= vbCrLf & " )"
                                        _Qry &= vbCrLf & " SELECT "
                                        _Qry &= vbCrLf & "'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                                        _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                                        _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ""
                                        _Qry &= vbCrLf & ",CASE WHEN Charindex('-','" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "') > 2 THEN LEFT('" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "',Charindex('-','" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "')-1) ELSE '" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "' END"
                                        _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "'"
                                        _Qry &= vbCrLf & ",FNSeq, FNHSysMatSizeId, FTSizeSpecDesc, FTSizeSpecExtension , FNHSysMeasId"
                                        _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_SizeSpec AS A "
                                        _Qry &= vbCrLf & " WHERE FTSubOrderNo='" & HI.UL.ULF.rpQuoted(Me.FTSubOrderNoSource.Text.Trim()) & "' "

                                        _Qry &= vbCrLf & "  AND (A.FNHSysMatSizeId IN  (SELECT A.FNHSysMatSizeId"
                                        _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TMERMMatSize AS A WITH(NOLOCK)"
                                        _Qry &= vbCrLf & " WHERE  EXISTS (SELECT 'T'"
                                        _Qry &= vbCrLf & "              FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub_BreakDown AS L1 WITH(NOLOCK)"
                                        _Qry &= vbCrLf & "              WHERE  L1.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "'"
                                        _Qry &= vbCrLf & "                   AND L1.FNHSysMatSizeId = A.FNHSysMatSizeId)))"


                                        tSqlRevised &= Environment.NewLine & "UPDATE A"
                                        tSqlRevised &= Environment.NewLine & "SET A.FTStateApprovedSizeSpec = N'0'"
                                        tSqlRevised &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub_ApprovedInfo AS A"
                                        tSqlRevised &= Environment.NewLine & "WHERE A.FTOrderNo = '" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text.Trim) & "'"
                                        tSqlRevised &= Environment.NewLine & "      AND A.FTSubOrderNo ='" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "' AND A.FTStateApprovedSizeSpec<>'0' ;"

                                    End If

                                    ' Add ockPackRatio.Checked By Chet [22 Jan 2024]
                                    If Me.ockPackRatio.Checked Then

                                        _Qry &= vbCrLf & "DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_Bundle WHERE FTSubOrderNo='" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "' "
                                        _Qry &= vbCrLf & "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_Bundle  ("
                                        _Qry &= vbCrLf & "FTInsUser, FDInsDate, FTInsTime,FTOrderNo, FTSubOrderNo, FTColorway, FTSizeBreakDown, FNQuantity"
                                        _Qry &= vbCrLf & " )"
                                        _Qry &= vbCrLf & " SELECT "
                                        _Qry &= vbCrLf & "'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                                        _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                                        _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ""
                                        _Qry &= vbCrLf & ", FTOrderNo, '" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "', FTColorway, FTSizeBreakDown, FNQuantity "
                                        _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_Bundle "
                                        _Qry &= vbCrLf & " WHERE FTSubOrderNo = '" & HI.UL.ULF.rpQuoted(Me.FTSubOrderNoSource.Text.Trim()) & "'"

                                        tSqlRevised &= Environment.NewLine & "UPDATE A"
                                        tSqlRevised &= Environment.NewLine & "SET A.FNPackPerCarton = (SELECT FNPackPerCarton FROM  HITECH_MERCHAN.dbo.TMERTOrderSub "
                                        tSqlRevised &= Environment.NewLine & "WHERE FTSubOrderNo = '" & HI.UL.ULF.rpQuoted(Me.FTSubOrderNoSource.Text.Trim()) & "') "
                                        tSqlRevised &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub AS A"
                                        tSqlRevised &= Environment.NewLine & "WHERE A.FTOrderNo = '" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text.Trim) & "'"
                                        tSqlRevised &= Environment.NewLine & " AND A.FTSubOrderNo ='" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "' ;"

                                    End If

                                Next
                                _dt.Dispose()


                                If _Qry <> "" Then
                                    HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

                                    If tSqlRevised <> "" Then
                                        If HI.Conn.SQLConn.ExecuteNonQuery(tSqlRevised, HI.Conn.DB.DataBaseName.DB_MERCHAN) = True Then
                                        End If
                                    End If

                                    AddComponent = True
                                    HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                                End If

                            End If
                        Else
                            HI.MG.ShowMsg.mInvalidData("กรุณาทำการระบุข้อมูลที่ต้องการ Copy !!!", 1407100002, Me.Text)
                        End If
                    Else
                        HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, FTSubOrderNoTo_lbl.Text)
                        FTSubOrderNoTo.Focus()
                    End If
                Else
                    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, FTSubOrderNoTo_lbl.Text)
                    FTSubOrderNoTo.Focus()
                End If
            Else
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, FTSubOrderNo_lbl.Text)
                FTSubOrderNo.Focus()
            End If
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, FTSubOrderNoSource_lbl.Text)
            FTSubOrderNoSource.Focus()
        End If


    End Sub

End Class
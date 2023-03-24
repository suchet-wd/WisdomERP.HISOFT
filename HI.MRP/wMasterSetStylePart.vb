Public Class wMasterSetStylePart


#Region "Procedure"

    Private Sub LoadStyleInfo(ByVal FNHSysStyleId As String, Optional SysSeasonId As Integer = 0)

        Dim _dt As DataTable
        Dim _Str As String = ""
        _Str = "SELECT MS.FNHSysStyleId, MS.FTStyleCode, MS.FTStyleNameTH, MS.FTStyleNameEN, T1.FNHSysCustId, T1.FNHSysSeasonId, T1.FTUpdUser, CONVERT(VARCHAR(10), CONVERT(DATETIME, T1.FDUpdDate, 120), 103) AS FDUpdDate, T1.FTUpdTime"
        _Str &= vbCrLf & "  , T7.FTSeasonCode, T7.FTSeasonNameEN, T8.FTCustCode, T8.FTCustNameEN"
        _Str &= vbCrLf & " ,XZ.FTInsUser,XZ.FDInsDate,XZ.FTInsTime"
        _Str &= vbCrLf & " ,Bom.FTBomInsUser,Bom.FDBomInsDate,Bom.FTBomInsTime"
        _Str &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS MS WITH(NOLOCK)  "
        _Str &= vbCrLf & " LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTStyle AS T1 WITH(NOLOCK) ON MS.FNHSysStyleId = T1.FNHSysStyleId"
        _Str &= vbCrLf & " LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMSeason T7 WITH(NOLOCK) ON T1.FNHSysSeasonId = T7.FNHSysSeasonId"
        _Str &= vbCrLf & " LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCustomer T8 WITH(NOLOCK) ON T1.FNHSysCustId = T8.FNHSysCustId"

        _Str &= vbCrLf & " OUTER APPLY (SELECT TOP 1 ISNULL(X.FTUpdUser,X.FTInsUser) AS FTInsUser "
        _Str &= vbCrLf & " ,ISNULL(X.FDUpdDate,X.FDInsDate) AS FDInsDate  "
        _Str &= vbCrLf & " ,ISNULL(X.FTUpdTime,X.FTInsTime) AS FTInsTime  "
        _Str &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTStyle_Part AS X WITH(NOLOCK) "
        _Str &= vbCrLf & " WHERE X.FNHSysStyleId = MS.FNHSysStyleId "
        _Str &= vbCrLf & "  AND X.FNHSysSeasonId = " & SysSeasonId & ""
        _Str &= vbCrLf & "  ORDER BY ISNULL(X.FDUpdDate,X.FDInsDate) DESC ,ISNULL(X.FTUpdTime,X.FTInsTime)  DESC"
        _Str &= vbCrLf & " ) As XZ"

        _Str &= vbCrLf & " OUTER APPLY (SELECT TOP 1 ISNULL(X.FTUpdUser,X.FTInsUser) AS FTBomInsUser "
        _Str &= vbCrLf & " ,ISNULL(X.FDUpdDate,X.FDInsDate) AS FDBomInsDate  "
        _Str &= vbCrLf & " ,ISNULL(X.FTUpdTime,X.FTInsTime) AS FTBomInsTime  "
        _Str &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERMStyle_Part AS X WITH(NOLOCK) "
        _Str &= vbCrLf & " WHERE X.FNHSysStyleId = MS.FNHSysStyleId "
        _Str &= vbCrLf & "  AND X.FNHSysSeasonId = " & SysSeasonId & ""
        _Str &= vbCrLf & "  ORDER BY ISNULL(X.FDUpdDate,X.FDInsDate) DESC ,ISNULL(X.FTUpdTime,X.FTInsTime)  DESC"
        _Str &= vbCrLf & " ) As Bom"


        _Str &= vbCrLf & " WHERE (MS.FNHSysStyleId  =" & Val(FNHSysStyleId) & ")"


        _Str &= vbCrLf & " ORDER BY MS.FNHSysStyleId"
        _dt = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_MERCHAN)

        If _dt.Rows.Count > 0 Then

            For Each R As DataRow In _dt.Rows

                FTStyleNameTH.Text = R!FTStyleNameTH.ToString
                FTStyleNameEN.Text = R!FTStyleNameEN.ToString
                FNHSysCustId.Text = R!FTCustCode.ToString
                FNHSysCustId_None.Text = R!FTCustNameEN.ToString


                If SysSeasonId <= 0 Then


                    FTUpdUser.Text = ""
                    FDUpdDate.Text = ""
                    FTUpdTime.Text = ""

                Else

                    FTUpdUser.Text = R!FTInsUser.ToString
                    FDUpdDate.Text = HI.UL.ULDate.ConvertEN(R!FDInsDate.ToString)
                    FTUpdTime.Text = R!FTInsTime.ToString

                End If

            Next

        Else


            FTStyleNameTH.Text = ""
            FTStyleNameEN.Text = ""
            FNHSysCustId.Text = ""
            FNHSysCustId_None.Text = ""


            FTUpdUser.Text = ""
            FDUpdDate.Text = ""
            FTUpdTime.Text = ""

        End If

        Me.otbmain.SelectedTabPageIndex = 0

    End Sub

    Private Sub SetDataInfo(_FNHSysStyleId As Integer, _FNHSysSeasonId As Integer)
        Dim dt As New DataTable
        Dim dt2 As New DataTable
        Dim dt3 As New DataTable
        Dim _Qry As String = ""

        dt.Columns.Add("FNHSysPartId", GetType(Integer))
        dt.Columns.Add("FTPartCode", GetType(String))
        dt.Columns.Add("FTPartName", GetType(String))
        dt.Columns.Add("FTStateEmb", GetType(String))
        dt.Columns.Add("FTStatePrint", GetType(String))
        dt.Columns.Add("FTStateHeat", GetType(String))
        dt.Columns.Add("FTStateLaser", GetType(String))
        dt.Columns.Add("FTStateWindows", GetType(String))
        dt.Columns.Add("FTEmbNote", GetType(String))
        dt.Columns.Add("FTPrintNote", GetType(String))
        dt.Columns.Add("FTHeatNote", GetType(String))
        dt.Columns.Add("FTLaserNote", GetType(String))
        dt.Columns.Add("FTWindowsNote", GetType(String))

        _Qry = "   Select    FTPositionPartId"
        _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERMStyle_Part As A With(NOLOCK)"
        _Qry &= vbCrLf & " WHERE FNHSysStyleId=" & _FNHSysStyleId & " "

        ' If StateSeason Then
        _Qry &= vbCrLf & "And  (FNHSysSeasonId=" & _FNHSysSeasonId & " Or ISNULL(FNHSysSeasonId,0)<=0) "

        ' End If

        _Qry &= vbCrLf & " And ISNULL(FTPositionPartId,'') <> '' "
        _Qry &= vbCrLf & "   GROUP BY  FTPositionPartId"
        dt2 = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

        For Each R As DataRow In dt2.Rows
            For Each Str As String In R!FTPositionPartId.ToString.Split("|")
                If dt.Select("FNHSysPartId=" & Integer.Parse(Val(Str)) & "").Length <= 0 Then

                    _Qry = "   SELECT    TOP 1    A.FNHSysPartId, A.FTPartCode"

                    If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                        _Qry &= vbCrLf & " , A.FTPartNameTH AS FTPartName"
                    Else
                        _Qry &= vbCrLf & " , A.FTPartNameEN As FTPartName "
                    End If

                    _Qry &= vbCrLf & " , ISNULL(B.FTStateEmb,'0') AS FTStateEmb"
                    _Qry &= vbCrLf & " , ISNULL(B.FTStatePrint,'0') AS FTStatePrint"
                    _Qry &= vbCrLf & " , ISNULL(B.FTStateHeat,'0') AS FTStateHeat"
                    _Qry &= vbCrLf & " , ISNULL(B.FTStateLaser,'0') AS FTStateLaser"
                    _Qry &= vbCrLf & " , ISNULL(B.FTStateWindows,'0') AS FTStateWindows"
                    _Qry &= vbCrLf & " , ISNULL(B.FTEmbNote,'') AS FTEmbNote"
                    _Qry &= vbCrLf & " , ISNULL(B.FTPrintNote,'') AS FTPrintNote"
                    _Qry &= vbCrLf & " , ISNULL(B.FTHeatNote,'') AS FTHeatNote"
                    _Qry &= vbCrLf & " , ISNULL(B.FTLaserNote,'') AS FTLaserNote"
                    _Qry &= vbCrLf & " , ISNULL(B.FTWindowsNote,'') AS FTWindowsNote"

                    _Qry &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMPart AS A LEFT OUTER JOIN"
                    _Qry &= vbCrLf & "     (SELECT * FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTStyle_Part WITH(NOLOCK) WHERE FNHSysStyleId=" & _FNHSysStyleId & ""

                    'If StateSeason Then
                    _Qry &= vbCrLf & "AND ( FNHSysSeasonId=" & _FNHSysSeasonId & " OR ISNULL(FNHSysSeasonId,0)<=0) "
                    ' End If

                    _Qry &= vbCrLf & " ) AS B ON A.FNHSysPartId = B.FNHSysPartId"
                    _Qry &= vbCrLf & "   WHERE A.FNHSysPartId=" & Integer.Parse(Val(Str)) & " "

                    ' If StateSeason Then


                    _Qry &= vbCrLf & " ORDER BY ISNULL(FNHSysSeasonId,0) DESC "

                    ' End If

                    dt3 = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

                    For Each R2 As DataRow In dt3.Rows

                        dt.Rows.Add(Integer.Parse(Val(Str)), R2!FTPartCode.ToString, R2!FTPartName.ToString, R2!FTStateEmb.ToString, R2!FTStatePrint.ToString, R2!FTStateHeat.ToString, R2!FTStateLaser.ToString, R2!FTStateWindows.ToString, R2!FTEmbNote.ToString, R2!FTPrintNote.ToString, R2!FTHeatNote.ToString, R2!FTLaserNote.ToString, R2!FTWindowsNote.ToString)

                        Exit For
                    Next

                End If
            Next
        Next

        Me.ogc.DataSource = dt.Copy
        dt.Dispose()
        dt2.Dispose()
        dt3.Dispose()

    End Sub

    Private Sub SetDataSizeInfo(_FNHSysStyleId As Integer, _FNHSysSeasonId As Integer)


        Dim _Qry As String = ""
        Dim _dt As DataTable
        Dim _dtData As DataTable
        Dim _dtsize As DataTable

        _Qry = " SELECT A.FNHSysPartId"
        _Qry &= vbCrLf & " 	,P.FTPartCode "

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then

            _Qry &= vbCrLf & " ,ISNULL(P.FTPartNameTH,'') AS FTPartName"
            _Qry &= vbCrLf & " ,ISNULL(LN.FTNameTH,'') AS FNSendSuplTypeName"

        Else
            _Qry &= vbCrLf & " ,ISNULL(P.FTPartNameEN,'') AS FTPartName"
            _Qry &= vbCrLf & " ,ISNULL(LN.FTNameEN,'') AS FNSendSuplTypeName "

        End If

        _Qry &= vbCrLf & " , A.FNSendSuplType"
        _Qry &= vbCrLf & " , ISNULL(KX.FTSizeBreakDown,'') AS FTSizeBreakDown, ISNULL(KX.FTSizeNote,'') AS FTSizeNote,ISNULL(KX.FNHSysMatSizeId,0) AS FNHSysMatSizeId"
        _Qry &= vbCrLf & " FROM (SELECT    FNHSysPartId,0 AS FNSendSuplType "
        _Qry &= vbCrLf & "  FROM            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTStyle_Part WITH(NOLOCK)"
        _Qry &= vbCrLf & "   WHERE FNHSysStyleId = " & _FNHSysStyleId & " "

        'If StateSeason Then
        _Qry &= vbCrLf & "   And (FNHSysSeasonId =" & _FNHSysSeasonId & " OR ISNULL(FNHSysSeasonId,0)<=0) "

        '  End If

        _Qry &= vbCrLf & "   And FTStateEmb = 1"

        _Qry &= vbCrLf & "   UNION"
        _Qry &= vbCrLf & "  SELECT    FNHSysPartId,1 AS FNSendSuplType "
        _Qry &= vbCrLf & "  FROM            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTStyle_Part WITH(NOLOCK)"
        _Qry &= vbCrLf & "    WHERE FNHSysStyleId =  " & _FNHSysStyleId & " "

        ' If StateSeason Then
        _Qry &= vbCrLf & "   And (FNHSysSeasonId =" & _FNHSysSeasonId & " OR ISNULL(FNHSysSeasonId,0)<=0)"

        ' End If

        _Qry &= vbCrLf & "   And FTStatePrint = 1"

        _Qry &= vbCrLf & "    UNION"
        _Qry &= vbCrLf & "  SELECT    FNHSysPartId,2 AS FNSendSuplType "
        _Qry &= vbCrLf & "  FROM            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTStyle_Part WITH(NOLOCK)"
        _Qry &= vbCrLf & "   WHERE FNHSysStyleId =  " & _FNHSysStyleId & " "

        'If StateSeason Then
        _Qry &= vbCrLf & "   And (FNHSysSeasonId =" & _FNHSysSeasonId & " OR ISNULL(FNHSysSeasonId,0)<=0) "

        ' End If

        _Qry &= vbCrLf & "   And FTStateHeat = 1"
        _Qry &= vbCrLf & "    UNION"
        _Qry &= vbCrLf & "  SELECT    FNHSysPartId,3 AS FNSendSuplType "
        _Qry &= vbCrLf & "  FROM            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTStyle_Part WITH(NOLOCK)"
        _Qry &= vbCrLf & "   WHERE FNHSysStyleId =  " & _FNHSysStyleId & " "

        'If StateSeason Then
        _Qry &= vbCrLf & "   And (FNHSysSeasonId =" & _FNHSysSeasonId & " OR ISNULL(FNHSysSeasonId,0)<=0)"

        ' End If

        _Qry &= vbCrLf & "   And FTStateLaser = 1"
        _Qry &= vbCrLf & "   UNION"
        _Qry &= vbCrLf & "  SELECT    FNHSysPartId,4 AS FNSendSuplType "
        _Qry &= vbCrLf & "  FROM            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTStyle_Part WITH(NOLOCK)"
        _Qry &= vbCrLf & "   WHERE FNHSysStyleId =  " & _FNHSysStyleId & " "
        ' If StateSeason Then
        _Qry &= vbCrLf & "   And (FNHSysSeasonId =" & _FNHSysSeasonId & " OR ISNULL(FNHSysSeasonId,0)<=0) "

        ' End If

        _Qry &= vbCrLf & "   And FTStateWindows = 1"
        _Qry &= vbCrLf & "  ) AS A LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMPart AS P WITH(NOLOCK)"
        _Qry &= vbCrLf & "    ON A.FNHSysPartId = P.FNHSysPartId  "
        _Qry &= vbCrLf & "  LEFT OUTER JOIN ( SELECT * FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData WITH(NOLOCK)  WHERE FTListName ='FNSendSuplType') AS LN  ON A.FNSendSuplType = LN.FNListIndex"

        _Qry &= vbCrLf & " LEFT OUTER JOIN ( SELECT A.FNHSysStyleId, A.FNHSysPartId, A.FNSendSuplType, A.FTSizeBreakDown, A.FTSizeNote,MS.FNHSysMatSizeId "
        _Qry &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTStyle_Part_SizeNote AS A WITH(NOLOCK) "
        _Qry &= vbCrLf & "       INNER JOIN    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMatSize AS MS WITH(NOLOCK) ON A.FTSizeBreakDown = MS.FTMatSizeCode"

        _Qry &= vbCrLf & "   WHERE A.FNHSysStyleId =  " & _FNHSysStyleId & ""
        'If StateSeason Then
        _Qry &= vbCrLf & "   And (A.FNHSysSeasonId =" & _FNHSysSeasonId & " OR ISNULL(A.FNHSysSeasonId,0)<=0) "

        ' End If

        _Qry &= vbCrLf & " ) AS KX ON A.FNHSysPartId=KX.FNHSysPartId AND A.FNSendSuplType=KX.FNSendSuplType"

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Qry &= vbCrLf & "  ORDER BY ISNULL(P.FTPartNameTH,''),ISNULL(LN.FTNameTH,'')"
        Else
            _Qry &= vbCrLf & "  ORDER BY ISNULL(P.FTPartNameEN,''),ISNULL(LN.FTNameEN,'')"
        End If

        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)
        _dtData = _dt.Clone()

        _dtData.BeginInit()
        _dtData.Columns.Remove("FTSizeBreakDown")
        _dtData.Columns.Remove("FTSizeNote")
        _dtData.Columns.Remove("FNHSysMatSizeId")
        _dtData.EndInit()

        _Qry = "   SELECT A.FNHSysStyleId, B.FTSizeBreakDown, MS.FNMatSizeSeq,MS.FNHSysMatSizeId"
        _Qry &= vbCrLf & "  FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS A WITH(NOLOCK) INNER JOIN"
        _Qry &= vbCrLf & "           [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_BreakDown AS B WITH(NOLOCK) ON A.FTOrderNo = B.FTOrderNo INNER JOIN"
        _Qry &= vbCrLf & "           [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMatSize AS MS WITH(NOLOCK) ON B.FTSizeBreakDown = MS.FTMatSizeCode"
        _Qry &= vbCrLf & "  WHERE (A.FNHSysStyleId = " & Integer.Parse(Val(_FNHSysStyleId)) & ")"

        'If StateSeason Then
        _Qry &= vbCrLf & "   And A.FNHSysSeasonId =" & _FNHSysSeasonId & ""
        'End If

        _Qry &= vbCrLf & " GROUP BY A.FNHSysStyleId, B.FTSizeBreakDown, MS.FNMatSizeSeq,MS.FNHSysMatSizeId"
        _Qry &= vbCrLf & " ORDER BY MS.FNMatSizeSeq "


        _dtsize = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

        Dim _StrFilter As String = ""
        For Each R As DataRow In _dtsize.Rows

            If _dtData.Columns.IndexOf("C" & R!FNHSysMatSizeId.ToString) < 0 Then
                _dtData.Columns.Add("C" & R!FNHSysMatSizeId.ToString, GetType(String))
            End If

        Next

        For Each R As DataRow In _dt.Rows

            _StrFilter = "FNHSysPartId=" & Integer.Parse(Val(R!FNHSysPartId.ToString)) & " AND FNSendSuplType=" & Integer.Parse(Val(R!FNSendSuplType.ToString)) & ""

            If _dtData.Select(_StrFilter).Length <= 0 Then
                _dtData.Rows.Add(Integer.Parse(Val(R!FNHSysPartId.ToString)), R!FTPartCode.ToString, R!FTPartName.ToString, R!FNSendSuplTypeName.ToString, Integer.Parse(Val(R!FNSendSuplType.ToString)))
            End If

            If Integer.Parse(Val(R!FNHSysMatSizeId.ToString)) > 0 Then

                For Each Rc As DataRow In _dtData.Select(_StrFilter)

                    Rc.Item("C" & R!FNHSysMatSizeId.ToString) = R!FTSizeNote.ToString
                    Exit For

                Next

            End If
        Next

    End Sub



#End Region

    Private Sub FNHSysStyleId_EditValueChanged(sender As Object, e As EventArgs) Handles FNHSysStyleId.EditValueChanged
        If (Me.InvokeRequired) Then
            Me.Invoke(New HI.Delegate.Dele.ButtonEdit_ValueChanged(AddressOf FNHSysStyleId_EditValueChanged), New Object() {sender, e})
        Else
            If FNHSysStyleId.Text <> "" Then
                FNHSysStyleId.Properties.Tag = HI.Conn.SQLConn.GetField("Select Top 1 FNHSysStyleId From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTStyle AS A WITH(NOLOCK) WHERE FTStyleCode='" & HI.UL.ULF.rpQuoted(FNHSysStyleId.Text) & "' ", Conn.DB.DataBaseName.DB_MERCHAN, "")
                Call LoadStyleInfo(FNHSysStyleId.Properties.Tag.ToString)

                'If StateSeason = False Then
                'Call SetDataInfo(Integer.Parse(Val(FNHSysStyleId.Properties.Tag.ToString)), Val(FNHSysSeasonId.Properties.Tag.ToString))
                ' End If


            End If
        End If

    End Sub

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ocmsave_Click(sender As Object, e As EventArgs) Handles ocmsave.Click
        Try

            Dim _Qry As String = ""

            Select Case otbmain.SelectedTabPage.Name
                Case otpsensupl.Name

                    If ogc.DataSource Is Nothing Then
                        Exit Sub
                    End If
                    CType(ogc.DataSource, DataTable).AcceptChanges()

                    If CType(ogc.DataSource, DataTable).Rows.Count > 0 Then
                        For Each R As DataRow In CType(ogc.DataSource, DataTable).Rows

                            _Qry = "Update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTStyle_Part "
                            _Qry &= vbCrLf & " SET FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                            _Qry &= vbCrLf & " ,FDUpdDate=" & HI.UL.ULDate.FormatDateDB & ""
                            _Qry &= vbCrLf & " ,FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & ""
                            _Qry &= vbCrLf & " ,FTStateEmb='" & R!FTStateEmb.ToString & "'"
                            _Qry &= vbCrLf & " ,FTStatePrint='" & R!FTStatePrint.ToString & "'"
                            _Qry &= vbCrLf & " ,FTStateHeat='" & R!FTStateHeat.ToString & "'"
                            _Qry &= vbCrLf & " ,FTStateLaser='" & R!FTStateLaser.ToString & "'"
                            _Qry &= vbCrLf & " ,FTStateWindows='" & R!FTStateWindows.ToString & "'"
                            _Qry &= vbCrLf & ", FTEmbNote='" & HI.UL.ULF.rpQuoted(R!FTEmbNote.ToString) & "'"
                            _Qry &= vbCrLf & " , FTPrintNote='" & HI.UL.ULF.rpQuoted(R!FTPrintNote.ToString) & "'"
                            _Qry &= vbCrLf & " , FTHeatNote='" & HI.UL.ULF.rpQuoted(R!FTHeatNote.ToString) & "'"
                            _Qry &= vbCrLf & " , FTLaserNote='" & HI.UL.ULF.rpQuoted(R!FTLaserNote.ToString) & "'"
                            _Qry &= vbCrLf & " , FTWindowsNote='" & HI.UL.ULF.rpQuoted(R!FTWindowsNote.ToString) & "'"
                            _Qry &= vbCrLf & "  WHERE FNHSysStyleId=" & Integer.Parse(Val(FNHSysStyleId.Properties.Tag.ToString)) & ""
                            _Qry &= vbCrLf & "  AND FNHSysPartId=" & Integer.Parse(Val(R!FNHSysPartId.ToString)) & ""

                            ' If StateSeason Then
                            _Qry &= vbCrLf & "   And FNHSysSeasonId =0"
                            'End If

                            If HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_MERCHAN) = False Then
                                _Qry = " INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTStyle_Part"
                                _Qry &= vbCrLf & " (FTInsUser, FDInsDate, FTInsTime, FNHSysStyleId, FNHSysPartId, FTStateEmb, FTStatePrint, FTStateHeat, FTStateLaser,FTStateWindows "
                                _Qry &= vbCrLf & ", FTEmbNote, FTPrintNote , FTHeatNote, FTLaserNote"
                                _Qry &= vbCrLf & ", FTWindowsNote"
                                ' If StateSeason Then
                                _Qry &= vbCrLf & ",FNHSysSeasonId"
                                ' End If

                                _Qry &= vbCrLf & ")"
                                _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                                _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                                _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ""
                                _Qry &= vbCrLf & "," & Integer.Parse(Val(FNHSysStyleId.Properties.Tag.ToString)) & ""
                                _Qry &= vbCrLf & "," & Integer.Parse(Val(R!FNHSysPartId.ToString)) & ""
                                _Qry &= vbCrLf & ",'" & R!FTStateEmb.ToString & "'"
                                _Qry &= vbCrLf & ",'" & R!FTStatePrint.ToString & "'"
                                _Qry &= vbCrLf & ",'" & R!FTStateHeat.ToString & "'"
                                _Qry &= vbCrLf & ",'" & R!FTStateLaser.ToString & "'"
                                _Qry &= vbCrLf & ",'" & R!FTStateWindows.ToString & "'"
                                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTEmbNote.ToString) & "'"
                                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTPrintNote.ToString) & "'"
                                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTHeatNote.ToString) & "'"
                                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTLaserNote.ToString) & "'"
                                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTWindowsNote.ToString) & "'"

                                ' If StateSeason Then
                                _Qry &= vbCrLf & "," & 0 & ""
                                ' End If

                                HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)
                            End If


                        Next

                        Call LoadStyleInfo(FNHSysStyleId.Properties.Tag.ToString, 0)

                        HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                    End If


            End Select

        Catch ex As Exception
        End Try

    End Sub

    Private Sub ocmrefresh_Click(sender As Object, e As EventArgs) Handles ocmrefresh.Click
        If FNHSysStyleId.Text <> "" Then
            FNHSysStyleId.Properties.Tag = HI.Conn.SQLConn.GetField("Select Top 1 FNHSysStyleId From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTStyle AS A WITH(NOLOCK) WHERE FTStyleCode='" & HI.UL.ULF.rpQuoted(FNHSysStyleId.Text) & "' ", Conn.DB.DataBaseName.DB_MERCHAN, "")
            Call LoadStyleInfo(FNHSysStyleId.Properties.Tag.ToString, 0)

            'If StateSeason Then
            Call SetDataInfo(Integer.Parse(Val(FNHSysStyleId.Properties.Tag.ToString)), 0)
            'Else
            '    Call SetDataInfo(Integer.Parse(Val(FNHSysStyleId.Properties.Tag.ToString)), 0)
            'End If

        End If
    End Sub

    Private Sub ocmclear_Click(sender As Object, e As EventArgs) Handles ocmclear.Click
        HI.TL.HandlerControl.ClearControl(Me)
    End Sub



    Private Sub ReposNote_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles ReposNote.EditValueChanging
        Try
6:          CType(Me.ogc.DataSource, DataTable).AcceptChanges()
            With Me.ogv
                Select Case .FocusedColumn.FieldName.ToString.ToLower
                    Case "FTEmbNote".ToLower

                        If .GetFocusedRowCellValue("FTStateEmb").ToString <> "1" Then
                            e.Cancel = True
                        End If

                    Case "FTPrintNote".ToLower

                        If .GetFocusedRowCellValue("FTStatePrint").ToString <> "1" Then
                            e.Cancel = True
                        End If

                    Case "FTHeatNote".ToLower

                        If .GetFocusedRowCellValue("FTStateHeat").ToString <> "1" Then
                            e.Cancel = True
                        End If

                    Case "FTLaserNote".ToLower

                        If .GetFocusedRowCellValue("FTStateLaser").ToString <> "1" Then
                            e.Cancel = True
                        End If

                    Case "FTWindowsNote".ToLower

                        If .GetFocusedRowCellValue("FTStateWindows").ToString <> "1" Then
                            e.Cancel = True
                        End If

                End Select
            End With
        Catch ex As Exception

        End Try
    End Sub

    Private Sub RepositoryItemCheckEdit1_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs)
        Try
            CType(Me.ogc.DataSource, DataTable).AcceptChanges()
            With Me.ogv

                If e.NewValue = "0" Then

                    Select Case .FocusedColumn.FieldName.ToString.ToLower
                        Case "FTStateEmb".ToLower
                            .SetFocusedRowCellValue("FTEmbNote", "")
                        Case "FTStatePrint".ToLower
                            .SetFocusedRowCellValue("FTPrintNote", "")
                        Case "FTStateHeat".ToLower
                            .SetFocusedRowCellValue("FTHeatNote", "")
                        Case "FTStateLaser".ToLower
                            .SetFocusedRowCellValue("FTLaserNote", "")
                        Case "FTStateWindows".ToLower
                            .SetFocusedRowCellValue("FTWindowsNote", "")
                    End Select

                End If

            End With

            CType(Me.ogc.DataSource, DataTable).AcceptChanges()
        Catch ex As Exception

        End Try
    End Sub

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        Dim _cmd As String = ""
        _cmd = " SELECT TOP 1 [COLUMN_NAME]"
        _cmd &= vbCrLf & " FROM INFORMATION_SCHEMA.COLUMNS"
        _cmd &= vbCrLf & "   WHERE [TABLE_NAME] = 'TMERTStyle_Part'"
        _cmd &= vbCrLf & "   AND [COLUMN_NAME] = 'FNHSysSeasonId'"

        Me.StateSeason = (HI.Conn.SQLConn.GetField(_cmd, Conn.DB.DataBaseName.DB_MERCHAN) <> "")



    End Sub

    Private _StateSeason As Boolean = False
    Property StateSeason As Boolean
        Get
            Return _StateSeason
        End Get
        Set(value As Boolean)
            _StateSeason = value
        End Set
    End Property

    Private Sub FNHSysSeasonId_EditValueChanged(sender As System.Object, e As System.EventArgs)
        If (Me.InvokeRequired) Then
            Me.Invoke(New HI.Delegate.Dele.ButtonEdit_ValueChanged(AddressOf FNHSysStyleId_EditValueChanged), New Object() {sender, e})
        Else



            Dim _Spls As New HI.TL.SplashScreen("Loading.... ,Please Wait.")
            LoadStyleInfo(FNHSysStyleId.Properties.Tag.ToString, 0)

            Try
                Call SetDataInfo(Integer.Parse(Val(FNHSysStyleId.Properties.Tag.ToString)), 0)
            Catch ex As Exception
                End Try

                _Spls.Close()


        End If

    End Sub

    Private Sub ocmposttoorder_Click(sender As Object, e As EventArgs)

        If FNHSysStyleId.Text <> "" Then

        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FNHSysStyleId_lbl.Text)
            FNHSysStyleId.Focus()
        End If

    End Sub
End Class
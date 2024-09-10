Imports System.IO
Imports System.Windows.Forms
Imports DevExpress.Spreadsheet
Imports DevExpress.Spreadsheet.Export

Public Class wImportExcelBOMDev

    Private WBOMReplace As wListBomExelReplace
    Private WBOMFinish As wListBomExelImportFinish
    'Private WBOMReplaceOriginal As wListBomExelReplace
    'Private WBOMFinishOriginal As wListBomExelImportFinish
    Private PathFileExcel As String = ""

    Sub New()

        InitializeComponent()

        WBOMReplace = New wListBomExelReplace
        HI.TL.HandlerControl.AddHandlerObj(WBOMReplace)
        'WBOMReplaceOriginal = New wListBomExelReplace
        'HI.TL.HandlerControl.AddHandlerObj(WBOMReplaceOriginal)

        Dim oSysLang As New HI.ST.SysLanguage

        Try
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleID, WBOMReplace.Name.ToString.Trim, WBOMReplace)
        Catch ex As Exception
        End Try
        'Try
        '    Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleID, WBOMReplaceOriginal.Name.ToString.Trim, WBOMReplaceOriginal)
        'Catch ex As Exception
        'End Try


        WBOMFinish = New wListBomExelImportFinish
        HI.TL.HandlerControl.AddHandlerObj(WBOMFinish)
        'WBOMFinishOriginal = New wListBomExelImportFinish
        'HI.TL.HandlerControl.AddHandlerObj(WBOMFinishOriginal)

        Try
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleID, WBOMFinish.Name.ToString.Trim, WBOMFinish)
        Catch ex As Exception
        End Try

        'Try
        '    Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleID, WBOMFinishOriginal.Name.ToString.Trim, WBOMFinishOriginal)
        'Catch ex As Exception
        'End Try

    End Sub


    Private Sub FTFilePath_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles FTFilePath.ButtonClick
        Select Case e.Button.Index
            Case 0
                Try
                    Dim opFileDialog As New System.Windows.Forms.OpenFileDialog
                    opFileDialog.Filter = "Excel Worksheets (*.xls, *.xlsx, *.xlsm)|*.xls;*.xlsx;*.xlsm"
                    opFileDialog.ShowDialog()

                    Try

                        If opFileDialog.FileName <> "" Then
                            Dim _FileName As String = opFileDialog.FileName
                            Me.FTFilePath.Text = _FileName
                            Dim Spls As New HI.TL.SplashScreen("Loading...")

                            'Me.opshet.Document.History.IsEnabled = False
                            'Me.opshet.Document.BeginUpdate()

                            'Dim bta As Byte() = File.ReadAllBytes(_FileName)

                            Try

                                Select Case Path.GetExtension(_FileName)
                                    Case ".xls"
                                        opshet.LoadDocument(File.ReadAllBytes(_FileName), DevExpress.Spreadsheet.DocumentFormat.Xls)

                                    Case ".xlsx"
                                        opshet.LoadDocument(File.ReadAllBytes(_FileName), DevExpress.Spreadsheet.DocumentFormat.Xlsx)

                                    'opshet.LoadDocument(_FileName, DevExpress.Spreadsheet.DocumentFormat.Xlsx)

                                    Case ".xlsm"
                                        opshet.LoadDocument(File.ReadAllBytes(_FileName), DevExpress.Spreadsheet.DocumentFormat.Xlsm)

                                    Case Else
                                        opshet.LoadDocument(File.ReadAllBytes(_FileName), DevExpress.Spreadsheet.DocumentFormat.Xlsx)

                                End Select

                            Catch ex As Exception

                            End Try


                            'Me.opshet.Document.EndUpdate()
                            'Me.opshet.Document.History.IsEnabled = True

                            'Dim usedRange As DevExpress.Spreadsheet.CellRange = opshet.ActiveWorksheet.GetUsedRange()
                            'usedRange.NumberFormat = "@"

                            For CIdx As Integer = 0 To opshet.ActiveWorksheet.GetUsedRange().ColumnCount - 1
                                Try
                                    'If opshet.ActiveWorksheet.Columns(CIdx).Value.Type = DevExpress.Spreadsheet.CellValueType.DateTime Then
                                    'Else
                                    '    If opshet.ActiveWorksheet.Columns(CIdx).Value.Type = DevExpress.Spreadsheet.CellValueType.Numeric Then
                                    '        opshet.ActiveWorksheet.Columns(CIdx).NumberFormat = "@"
                                    '    End If
                                    'End If

                                    If opshet.ActiveWorksheet.Cells(1, CIdx).Value.Type = DevExpress.Spreadsheet.CellValueType.DateTime Then

                                    Else
                                        If opshet.ActiveWorksheet.Cells(1, CIdx).Value.Type = DevExpress.Spreadsheet.CellValueType.Numeric Then
                                            opshet.ActiveWorksheet.Columns(CIdx).NumberFormat = "@"
                                        End If
                                    End If

                                Catch ex As Exception
                                    MsgBox(ex.Message)
                                    'Throw New Exception(ex.Message().ToString() & Environment.NewLine & ex.StackTrace.ToString())
                                End Try


                            Next

                            Spls.Close()
                        End If
                    Catch ex As Exception
                        MsgBox(ex.Message)
                        ' Throw New Exception(ex.Message().ToString() & Environment.NewLine & ex.StackTrace.ToString())
                    End Try

                Catch ex As Exception
                    MsgBox(ex.Message)
                    ' Throw New Exception(ex.Message().ToString() & Environment.NewLine & ex.StackTrace.ToString())
                End Try

            Case Else
                '...do nothing
        End Select
    End Sub

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ocmclear_Click(sender As Object, e As EventArgs) Handles ocmclear.Click
        HI.TL.HandlerControl.ClearControl(Me)
        For i As Int32 = 1 To opshet.Document.Worksheets.Count
            opshet.Document.Worksheets.RemoveAt(0)
        Next i
    End Sub

    Private Function CheckWriteFile() As Boolean

        Try

            If (Not System.IO.Directory.Exists(PathFileExcel)) Then
                System.IO.Directory.CreateDirectory(PathFileExcel)
            End If

            If (Not System.IO.Directory.Exists(PathFileExcel & "\TestExcel")) Then
                System.IO.Directory.CreateDirectory(PathFileExcel & "\TestExcel")
            End If
            System.IO.Directory.Delete(PathFileExcel & "\TestExcel")

            Return True
        Catch ex As Exception
            Return False
        End Try


    End Function

    Private Sub ocmimportbimpdf_Click(sender As Object, e As EventArgs) Handles ocmimportbimpdf.Click

        Dim StateImport As Boolean = False
        Dim msgshow As String = ""
        Dim cmdstring As String = ""

        If FNHSysCustId.Text = "" Then
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FNHSysCustId_lbl.Text)
            FNHSysCustId.Focus()
            Exit Sub
        End If
        If FTFilePath.Text <> "" Then

            PathFileExcel = HI.Conn.SQLConn.GetField("SELECT TOP 1  FTCfgData FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSESystemConfig AS X WITH(NOLOCK) WHERE X.FTCfgName='ImportBomExcel'", Conn.DB.DataBaseName.DB_SYSTEM)

            If CheckWriteFile() = False Then
                HI.MG.ShowMsg.mInfo("ไม่สามารถเข้าถึงที่เก็บ File สำหรับ Import BOM Dev. ได้ !!!!", 1909270019, Me.Text)
            End If

            Dim Splsx As New HI.TL.SplashScreen("Reading Data BOM Dev. From Excel....")
            Try
                Dim FileName As String = PathFileExcel & "\" & HI.ST.UserInfo.UserName & ".xlsx"
                opshet.SaveDocument(PathFileExcel & "\" & HI.ST.UserInfo.UserName & ".xlsx")
                'Dim FileName As String = FTFilePath.Text

                ' Original
                cmdstring = "EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FHS) & "].dbo.USP_IMPORTFILEEXCEL_BOMORIGINAL  '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(FileName) & "'"
                Dim dtimportOriginal As DataTable = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_FHS)

                'Normal
                cmdstring = "EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FHS) & "].dbo.USP_IMPORTFILEEXCEL_BOM  '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(FileName) & "'"
                ' StateImport = (HI.Conn.SQLConn.GetField(cmdstring, Conn.DB.DataBaseName.DB_FHS, "") = "1")
                Dim dtimport As DataTable = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_FHS)

                Try
                    If HI.ST.SysInfo.Admin Then
                    Else
                        System.IO.File.Delete(FileName)
                    End If
                Catch ex As Exception
                End Try

                If dtimportOriginal.Rows.Count > 0 Then
                    StateImport = (dtimportOriginal.Rows(0)!FTStetInsert.ToString = "1")
                    Dim msgerror As String = ""
                    Try
                        msgerror = dtimportOriginal.Rows(0)!FTMessage.ToString
                    Catch ex As Exception
                    End Try

                    If StateImport Then


                        cmdstring = "Select STYLE_NBR, STYLE_NM, SEASON_CD, SEASON_YR, FTSeason, FTStatus, ISNULL(SST.FTStateReplace,'0') AS FTStateReplace "
                        cmdstring &= vbCrLf & ", ISNULL(SST.FNHSysStyleDevId,0) AS FNHSysStyleDevId, ISNULL(SST2.FNHSysStyleDevId2,0) AS FNHSysStyleDevId2 "
                        cmdstring &= vbCrLf & ", ISNULL(SST.FTStatePost,'0') AS FTStatePost, ISNULL(SST2.FNVersion,1) AS FNVersion"
                        cmdstring &= vbCrLf & ", CASE WHEN ISNULL(SST.FTStateReplace,'0') ='1' THEN '0' ELSE '1' END AS FTStateImport "
                        cmdstring &= vbCrLf
                        cmdstring &= vbCrLf & "FROM (Select STYLE_NBR, STYLE_NM, SEASON_CD, SEASON_YR"
                        cmdstring &= vbCrLf & "   , SEASON_CD + Right(SEASON_YR, 2) As FTSeason, MIN(A.Seq) As Seq "
                        cmdstring &= vbCrLf & "   , A.[STATUS] AS FTStatus, MAX(ISNULL(BType.FNListIndex,0)) AS FNListIndex"
                        cmdstring &= vbCrLf
                        cmdstring &= vbCrLf & "   From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FHS) & "].dbo.TTMPImportBomExcel_Original As A WITH(NOLOCK)"
                        cmdstring &= vbCrLf
                        cmdstring &= vbCrLf & "   Outer Apply ( SELECT TOP 1 BType.FNListIndex FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData AS BType WITH(NOLOCK) WHERE BType.FTListName ='FNBomDevType' AND BType.FTNameEN  = A.[STATUS]) AS BType "
                        cmdstring &= vbCrLf & "      Where (FTUserLogIn ='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "')"
                        cmdstring &= vbCrLf & "      Group By STYLE_NBR, STYLE_NM, SEASON_CD, SEASON_YR, SEASON_CD + Right(SEASON_YR, 2),[STATUS]) AS A"
                        cmdstring &= vbCrLf
                        cmdstring &= vbCrLf & "   OUTER APPLY ( Select TOP 1  '1' AS FTStateReplace, X2.FNHSysStyleDevId, X2.FTStatePost "
                        cmdstring &= vbCrLf & "      From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTDevelopStyleOriginal AS X2 WITH(NOLOCK)"
                        cmdstring &= vbCrLf & "      Where X2.FTStyleDevCode = A.STYLE_NBR And X2.FTSeason = A.FTSeason  And ISNULL(X2.FNBomDevType,0) = ISNULL(A.FNListIndex,0)  AND ISNULL(X2.FNVersion,0) = 0"
                        cmdstring &= vbCrLf & ") AS SST "
                        cmdstring &= vbCrLf
                        cmdstring &= vbCrLf & "OUTER APPLY ( Select TOP 1 '1' AS FTStateReplace2, MAX(X2.FNHSysStyleDevId) AS FNHSysStyleDevId2, "
                        cmdstring &= vbCrLf & "   X2.FTStatePost AS FTStatePost2, MAX(X2.FNVersion) AS FNVersion  "
                        cmdstring &= vbCrLf
                        cmdstring &= vbCrLf & "   From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTDevelopStyleOriginal AS X2 WITH(NOLOCK)"
                        cmdstring &= vbCrLf & "   Where X2.FTStyleDevCode = A.STYLE_NBR And X2.FTSeason = A.FTSeason  And ISNULL(X2.FNBomDevType,0) = ISNULL(A.FNListIndex,0)   And ISNULL(X2.FNVersion,0) > 0"
                        cmdstring &= vbCrLf & "   GROUP BY X2.FTStatePost "
                        cmdstring &= vbCrLf & ") AS SST2 "
                        cmdstring &= vbCrLf
                        cmdstring &= vbCrLf & "ORDER BY Seq"
                        Dim dtstyledevOriginal As DataTable = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_FHS)

                        Dim dtstyledevimportOriginal As DataTable = dtstyledevOriginal.Clone


                        If dtstyledevOriginal.Select("FTStateImport='1'").Length > 0 Then
                            dtstyledevimportOriginal.Merge(dtstyledevOriginal.Select("FTStateImport='1'").CopyToDataTable)
                        End If

                        Dim dtstyledevreplace As DataTable

                        Splsx.Close()

                        If dtstyledevimportOriginal.Select("FTStateReplace='1'").Length > 0 Then
                            dtstyledevreplace = dtstyledevimportOriginal.Select("FTStateReplace='1'").CopyToDataTable
                            'With WBOMReplaceOriginal
                            '    .ogclist.DataSource = dtstyledevreplace.Copy
                            '    .ocmexit.Enabled = True
                            '    .ocmsave.Enabled = True
                            '    .StateOK = False
                            '    .ShowDialog()
                            '    If .StateOK = True Then
                            '        With CType(.ogclist.DataSource, DataTable)
                            '            .AcceptChanges()
                            '            dtstyledevreplace = .Copy
                            '            If dtstyledevreplace.Select("FTStateImport='1'").Length > 0 Then
                            '                dtstyledevimportOriginal.Merge(dtstyledevreplace.Select("FTStateImport='1'").CopyToDataTable)
                            '            End If
                            '            dtstyledevreplace.Dispose()
                            '        End With
                            '    End If
                            'End With
                        End If
                        dtstyledevimportOriginal.Dispose()


                        '----------------- Start Import Original BOM -----------------
                        Try
                            If dtstyledevimportOriginal.Rows.Count > 0 Then
                                Dim TotalBom As Integer = dtstyledevimportOriginal.Rows.Count
                                Dim CountBom As Integer = 0
                                Dim Version As Integer = 0
                                Dim Splsx2 As New HI.TL.SplashScreen("Importing Bom Original Total....")
                                Try
                                    Dim DevID As Integer = 0
                                    For Each R As DataRow In dtstyledevimportOriginal.Rows
                                        CountBom = CountBom + 1
                                        Splsx2.UpdateInformation("Importing Original Boma.... Style  " & R!STYLE_NBR.ToString & " (" & R!FTSeason.ToString & ")" & "  Status " & R!FTStatus.ToString & "    Row " & CountBom & " of  " & TotalBom)
                                        DevID = Val(R!FNHSysStyleDevId.ToString)
                                        Version = Val(R!FNVersion.ToString) + 1
                                        If R!FTStatePost.ToString = "1" Then
                                            DevID = Val(R!FNHSysStyleDevId2.ToString)
                                            If DevID = 0 Then
                                                DevID = HI.TL.RunID.GetRunNoID("TMERTDevelopStyleOriginal", "FNHSysStyleDevId", Conn.DB.DataBaseName.DB_MERCHAN)
                                            End If
                                        Else
                                            If DevID = 0 Then
                                                DevID = HI.TL.RunID.GetRunNoID("TMERTDevelopStyleOriginal", "FNHSysStyleDevId", Conn.DB.DataBaseName.DB_MERCHAN)
                                            End If
                                        End If
                                        cmdstring = "EXEC  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FHS) & "].dbo.USP_IMPORTBOMEXCEL_ORIGINAL " &
                                            "@User='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," &
                                            "@DevId=" & DevID & "," &
                                            "@StyleNo='" & HI.UL.ULF.rpQuoted(R!STYLE_NBR.ToString) & "'," &
                                            "@Season='" & HI.UL.ULF.rpQuoted(R!FTSeason.ToString) & "'," &
                                            "@pSeason='" & HI.UL.ULF.rpQuoted(R!SEASON_CD.ToString) & "'," &
                                            "@pYear='" & HI.UL.ULF.rpQuoted(R!SEASON_YR.ToString) & "'," &
                                            "@Version=" & Version & "," &
                                            "@pType='" & HI.UL.ULF.rpQuoted(R!FTStatus.ToString) & "'," &
                                            "@CustID=" & Val(FNHSysCustId.Properties.Tag.ToString) & " "
                                        HI.Conn.SQLConn.ExecuteOnly(cmdstring, Conn.DB.DataBaseName.DB_FHS)
                                    Next

                                    cmdstring = "EXEC  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FHS) & "].dbo.USP_MOVE_IMPORTBOMDEVEXCEL_Original '" &
                                        HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                                    HI.Conn.SQLConn.ExecuteOnly(cmdstring, Conn.DB.DataBaseName.DB_FHS)
                                    Splsx2.Close()
                                    HI.MG.ShowMsg.mInfo("Import Data Bom Original Complete !!!", 1099154872, Me.Text, " Total  " & TotalBom.ToString, MessageBoxIcon.Information)
                                    'With WBOMFinishOriginal
                                    '    .ogclist.DataSource = dtstyledevimportOriginal.Copy
                                    '    .ocmexit.Enabled = True
                                    '    .ShowDialog()
                                    'End With
                                Catch ex As Exception
                                    Splsx2.Close()
                                End Try
                            End If
                        Catch ex As Exception
                        End Try
                        '----------------- End Import Original BOM -----------------
                    Else
                        Splsx.Close()
                        msgshow = msgerror
                    End If

                Else
                    Splsx.Close()
                    msgshow = "ข้อมูล Format File Excel ไม่ถูกต้องกรุณาทำการตรวจสอบ !!!"
                End If


                If dtimport.Rows.Count > 0 Then
                    StateImport = (dtimport.Rows(0)!FTStetInsert.ToString = "1")
                    Dim msgerror As String = ""
                    Try
                        msgerror = dtimport.Rows(0)!FTMessage.ToString
                    Catch ex As Exception
                    End Try

                    If StateImport Then

                        cmdstring = "Select STYLE_NBR, STYLE_NM, SEASON_CD, SEASON_YR, FTSeason, FTStatus, ISNULL(SST.FTStateReplace,'0') AS FTStateReplace"
                        cmdstring &= vbCrLf & ", ISNULL(SST.FNHSysStyleDevId,0) AS FNHSysStyleDevId, ISNULL(SST2.FNHSysStyleDevId2,0) AS FNHSysStyleDevId2 "
                        cmdstring &= vbCrLf & ", ISNULL(SST.FTStatePost,'0') AS FTStatePost, ISNULL(SST2.FNVersion,0) AS FNVersion "
                        cmdstring &= vbCrLf & ", CASE WHEN ISNULL(SST.FTStateReplace,'0') ='1' THEN '0' ELSE '1' END AS FTStateImport "
                        cmdstring &= vbCrLf
                        cmdstring &= vbCrLf & "FROM (Select STYLE_NBR, STYLE_NM, SEASON_CD, SEASON_YR, SEASON_CD + Right(SEASON_YR, 2) As FTSeason "
                        cmdstring &= vbCrLf & "   , MIN(A.Seq) As Seq,A.[STATUS] AS FTStatus, MAX(ISNULL(BType.FNListIndex,0)) AS FNListIndex "
                        cmdstring &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FHS) & "].dbo.TTMPImportBomExcel As A WITH(NOLOCK)"
                        cmdstring &= vbCrLf
                        cmdstring &= vbCrLf & "   OUTER APPLY (SELECT TOP 1 BType.FNListIndex FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData AS BType WITH(NOLOCK) WHERE BType.FTListName ='FNBomDevType' AND BType.FTNameEN  = A.[STATUS]) AS BType "
                        cmdstring &= vbCrLf & "      Where (FTUserLogIn ='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "')"
                        cmdstring &= vbCrLf & "      Group By STYLE_NBR, STYLE_NM, SEASON_CD, SEASON_YR, SEASON_CD + Right(SEASON_YR, 2),[STATUS]) AS A"
                        cmdstring &= vbCrLf
                        cmdstring &= vbCrLf & "   OUTER APPLY (Select TOP 1  '1' AS FTStateReplace,X2.FNHSysStyleDevId,X2.FTStatePost "
                        cmdstring &= vbCrLf & "      From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTDevelopStyle AS X2 WITH(NOLOCK)"
                        cmdstring &= vbCrLf & "      Where X2.FTStyleDevCode = A.STYLE_NBR And X2.FTSeason = A.FTSeason  And ISNULL(X2.FNBomDevType,0) = ISNULL(A.FNListIndex,0)  AND ISNULL(X2.FNVersion,0) = 0"
                        cmdstring &= vbCrLf & ") AS SST "
                        cmdstring &= vbCrLf
                        cmdstring &= vbCrLf & "OUTER APPLY(Select TOP 1 '1' AS FTStateReplace2, MAX(X2.FNHSysStyleDevId) AS FNHSysStyleDevId2, X2.FTStatePost AS FTStatePost2 , MAX(X2.FNVersion) AS FNVersion  "
                        cmdstring &= vbCrLf & "   From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTDevelopStyle AS X2 WITH(NOLOCK)"
                        cmdstring &= vbCrLf & "   Where X2.FTStyleDevCode = A.STYLE_NBR And X2.FTSeason = A.FTSeason  And ISNULL(X2.FNBomDevType,0) = ISNULL(A.FNListIndex,0)   AND ISNULL(X2.FNVersion,0) > 0"
                        cmdstring &= vbCrLf & "   GROUP BY X2.FTStatePost "
                        cmdstring &= vbCrLf & ") AS SST2 "
                        cmdstring &= vbCrLf
                        cmdstring &= vbCrLf & "ORDER BY Seq"
                        Dim dtstyledev As DataTable = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_FHS)

                        Dim dtstyledevimport As DataTable = dtstyledev.Clone

                        If dtstyledev.Select("FTStateImport='1'").Length > 0 Then
                            dtstyledevimport.Merge(dtstyledev.Select("FTStateImport='1'").CopyToDataTable)
                        End If

                        Dim dtstyledevreplace As DataTable

                        Splsx.Close()

                        If dtstyledev.Select("FTStateReplace='1'").Length > 0 Then
                            dtstyledevreplace = dtstyledev.Select("FTStateReplace='1'").CopyToDataTable
                            With WBOMReplace
                                .ogclist.DataSource = dtstyledevreplace.Copy
                                .ocmexit.Enabled = True
                                .ocmsave.Enabled = True
                                .StateOK = False
                                .ShowDialog()
                                If .StateOK = True Then
                                    With CType(.ogclist.DataSource, DataTable)
                                        .AcceptChanges()
                                        dtstyledevreplace = .Copy
                                        If dtstyledevreplace.Select("FTStateImport='1'").Length > 0 Then
                                            dtstyledevimport.Merge(dtstyledevreplace.Select("FTStateImport='1'").CopyToDataTable)
                                        End If
                                        dtstyledevreplace.Dispose()
                                    End With
                                End If
                            End With
                        End If
                        dtstyledev.Dispose()

                        '----------------- Start Import Normal BOM -----------------
                        Try
                            With WBOMFinish
                                .ogclist.DataSource = dtstyledevimport.Copy
                                .ocmexit.Enabled = True

                                .ShowDialog()
                                'If .StateOK = True Then
                                With CType(.ogclist.DataSource, DataTable)
                                        .AcceptChanges()
                                    dtstyledevimport = .Copy
                                    If dtstyledevimport.Select("FTStateImport='1'").Length > 0 Then
                                        dtstyledevimport = dtstyledevimport.Select("FTStateImport='1'").CopyToDataTable
                                        'dtstyledevimport.Merge(dtstyledevreplace.Select("FTStateImport='1'").CopyToDataTable)
                                    Else
                                        dtstyledevimport = Nothing
                                    End If
                                    'dtstyledevreplace.Dispose()
                                End With
                                'End If
                            End With

                            If dtstyledevimport.Rows.Count > 0 Then
                                Dim TotalBom As Integer = dtstyledevimport.Rows.Count
                                Dim CountBom As Integer = 0
                                Dim Version As Integer = 0
                                Dim Splsx2 As New HI.TL.SplashScreen("Importing Bom Total....")

                                Try


                                    Dim DevID As Integer = 0
                                    For Each R As DataRow In dtstyledevimport.Rows
                                        CountBom = CountBom + 1
                                        Splsx2.UpdateInformation("Importing Boma.... Style  " & R!STYLE_NBR.ToString & " (" & R!FTSeason.ToString & ")" & "  Status " & R!FTStatus.ToString & "    Row " & CountBom & " of  " & TotalBom)
                                        DevID = Val(R!FNHSysStyleDevId.ToString)
                                        Version = Val(R!FNVersion.ToString) + 1

                                        If R!FTStatePost.ToString = "1" Then
                                            DevID = Val(R!FNHSysStyleDevId2.ToString)
                                            If DevID = 0 Then
                                                DevID = HI.TL.RunID.GetRunNoID("TMERTDevelopStyle", "FNHSysStyleDevId", Conn.DB.DataBaseName.DB_MERCHAN)
                                            End If
                                        Else
                                            If DevID = 0 Then
                                                DevID = HI.TL.RunID.GetRunNoID("TMERTDevelopStyle", "FNHSysStyleDevId", Conn.DB.DataBaseName.DB_MERCHAN)
                                            End If
                                        End If

                                        cmdstring = "EXEC  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FHS) & "].dbo.USP_IMPORTBOMEXCEL_V2 '" &
                                            HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," &
                                            DevID & ",'" &
                                            HI.UL.ULF.rpQuoted(R!STYLE_NBR.ToString) & "','" &
                                            HI.UL.ULF.rpQuoted(R!FTSeason.ToString) & "','" &
                                            HI.UL.ULF.rpQuoted(R!SEASON_CD.ToString) & "','" &
                                            HI.UL.ULF.rpQuoted(R!SEASON_YR.ToString) & "'," & Version & ",'" &
                                            HI.UL.ULF.rpQuoted(R!FTStatus.ToString) & "'," &
                                            Val(FNHSysCustId.Properties.Tag.ToString) & " "
                                        HI.Conn.SQLConn.ExecuteOnly(cmdstring, Conn.DB.DataBaseName.DB_FHS)
                                    Next
                                    cmdstring = "EXEC  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FHS) & "].dbo.USP_MOVE_IMPORTBOMDEVEXCEL '" &
                                        HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                                    HI.Conn.SQLConn.ExecuteOnly(cmdstring, Conn.DB.DataBaseName.DB_FHS)
                                    Splsx2.Close()
                                    'HI.MG.ShowMsg.mInfo("Import Data Bom Complete !!!", 1099154871, Me.Text, " Total  " & TotalBom.ToString, MessageBoxIcon.Information)


                                Catch ex As Exception
                                    Splsx2.Close()
                                End Try
                            End If
                        Catch ex As Exception
                        End Try
                        '----------------- End Import Normal BOM -----------------

                    Else
                        Splsx.Close()
                        msgshow = msgerror
                    End If

                Else
                    Splsx.Close()
                    msgshow = "ข้อมูล Format File Excel ไม่ถูกต้องกรุณาทำการตรวจสอบ !!!"
                End If

                'Else
                '    Splsx.Close()
                '    msgshow = "ข้อมูล File Excel ไม่ถูกต้องกรุณาทำการตรวจสอบ !!!"
                'End If
                ' End If

            Catch ex2 As Exception
                Splsx.Close()
            End Try

            If StateImport = False Then
                MessageBox.Show(msgshow)
            End If

        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, ogbselectfile.Text)
        End If

    End Sub

    Private Sub exporter_CellValueConversionError(ByVal sender As Object, ByVal e As CellValueConversionErrorEventArgs)
        MessageBox.Show("Error In cell " & e.Cell.GetReferenceA1())
        e.DataTableValue = Nothing
        e.Action = DataTableExporterAction.Continue
    End Sub

    Private Function ImportDataToTemp(dt As DataTable, spls As HI.TL.SplashScreen) As Boolean

        spls.UpdateInformation("Importing Data....")
        Dim cmdstring As String = ""
        cmdstring = "delete from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FHS) & "].dbo.TTMPImportBomExcel where FTUserLogIn='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
        HI.Conn.SQLConn.ExecuteOnly(cmdstring, Conn.DB.DataBaseName.DB_FHS)

        cmdstring = "delete from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FHS) & "].dbo.TTMPImportBomDevExcel where FTUserLogIn='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
        HI.Conn.SQLConn.ExecuteOnly(cmdstring, Conn.DB.DataBaseName.DB_FHS)

        Dim Ridx As Integer = 0
        Dim TotalR As Integer = dt.Select("STATUS='P' OR STATUS='K'").Length

        For Each R As DataRow In dt.Select("STATUS='P' OR STATUS='K'")

            Try
                Ridx = Ridx + 1

                spls.UpdateInformation("Importing Data.... Row " & Ridx & " of  " & TotalR)

                cmdstring = " insert into [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FHS) & "].dbo.TTMPImportBomExcel ( "
                cmdstring &= vbCrLf & " FTUserLogIn, Seq, BOM_ID, BOM_ITM_ID, BOM_ROW_NBR, MSC_CODE, MSC_LEVEL_1, MSC_LEVEL_2 "
                cmdstring &= vbCrLf & " , MSC_LEVEL_3, SILHOUETTE, SEASON_CD, SEASON_YR, STYLE_NM, STYLE_NBR, STYLE_CW_CD, "
                cmdstring &= vbCrLf & "  PLUG_CW_CD, PRMRY, SCNDY, TRTRY, LOGO, ADDENDUM, FACTORY, [STATUS], COMPONENT_ORD "
                cmdstring &= vbCrLf & " , [USE], [DESCRIPTION], ITEM_TYPE_1, ITEM_TYPE_2, ITEM_TYPE_3, ITEM_TYPE_4, [IS], IT, ITEM_NBR, "
                cmdstring &= vbCrLf & " ITEM_COLOR_ORD, GCW, GCW_ORD, GCW_ART_DESCRIPTION, ITEM_COLOR_CD, ITEM_COLOR_NM, ITEM_COLOR_ABRV "
                cmdstring &= vbCrLf & " , VEND_CD, VEND_LO, VEND_NM, QTY, UOM, DEVELOPER, BOM_UPDATE_DT, "
                cmdstring &= vbCrLf & " BOM_ITM_UPDATE_DT, BOM_ITM_SETUP_DT, HK_BOM_UPDATE_DT, HK_BOM_ITM_UPDATE_DT, HK_BOM_ITM_SETPUP_DT, ReportSection "
                cmdstring &= vbCrLf & " ) "
                cmdstring &= vbCrLf & " select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & Ridx & ""
                cmdstring &= vbCrLf & " ," & Val(R!BOM_ID.ToString) & ""
                cmdstring &= vbCrLf & " ," & Val(R!BOM_ITM_ID.ToString) & ""
                cmdstring &= vbCrLf & " ," & Val(R!BOM_ROW_NBR.ToString) & ""
                cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!MSC_CODE.ToString) & "'"
                cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!MSC_LEVEL_1.ToString) & "'"
                cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!MSC_LEVEL_2.ToString) & "' "
                cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!MSC_LEVEL_3.ToString) & "'"
                cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!SILHOUETTE.ToString) & "'"
                cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!SEASON_CD.ToString) & "'"
                cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!SEASON_YR.ToString) & "'"
                cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!STYLE_NM.ToString) & "'"
                cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!STYLE_NBR.ToString) & "'"
                cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!STYLE_CW_CD.ToString) & "'"
                cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!PLUG_CW_CD.ToString) & "'"
                cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!PRMRY.ToString) & "'"
                cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!SCNDY.ToString) & "'"
                cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!TRTRY.ToString) & "'"
                cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!LOGO.ToString) & "'"
                cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!ADDENDUM.ToString) & "'"
                cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!FACTORY.ToString) & "'"
                cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!STATUS.ToString) & "'"

                If (R!QTY.ToString.Trim = "") Then
                    cmdstring &= vbCrLf & ",NULL"
                Else
                    cmdstring &= vbCrLf & "," & Val(R!COMPONENT_ORD.ToString) & ""
                End If

                cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!USE.ToString) & "'"
                cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!DESCRIPTION.ToString) & "'"
                cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!ITEM_TYPE_1.ToString) & "'"
                cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!ITEM_TYPE_2.ToString) & "'"
                cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!ITEM_TYPE_3.ToString) & "'"
                cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!ITEM_TYPE_4.ToString) & "'"
                cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!IS.ToString) & "'"
                cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!IT.ToString) & "'"
                cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!ITEM_NBR.ToString) & "'"

                If (R!QTY.ToString.Trim = "") Then
                    cmdstring &= vbCrLf & ",NULL"
                Else
                    cmdstring &= vbCrLf & "," & Val(R!ITEM_COLOR_ORD.ToString) & ""
                End If

                cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R.Item("GCW#").ToString) & "'"
                cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!GCW_ORD.ToString) & "'"
                cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!GCW_ART_DESCRIPTION.ToString) & "'"
                cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!ITEM_COLOR_CD.ToString) & "'"
                cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!ITEM_COLOR_NM.ToString) & "'"
                cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!ITEM_COLOR_ABRV.ToString) & "'"
                cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!VEND_CD.ToString) & "'"
                cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!VEND_LO.ToString) & "'"
                cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!VEND_NM.ToString) & "'"

                If (R!QTY.ToString.Trim = "") Then
                    cmdstring &= vbCrLf & ",NULL"
                Else
                    cmdstring &= vbCrLf & "," & Val(R!QTY.ToString) & ""
                End If

                cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!UOM.ToString) & "'"
                cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!DEVELOPER.ToString) & "'"
                cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!BOM_UPDATE_DT.ToString) & "'"
                cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!BOM_ITM_UPDATE_DT.ToString) & "'"
                cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!BOM_ITM_SETUP_DT.ToString) & "'"
                cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!HK_BOM_UPDATE_DT.ToString) & "'"
                cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!HK_BOM_ITM_UPDATE_DT.ToString) & "'"
                cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!HK_BOM_ITM_SETPUP_DT.ToString) & "'"
                cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!ReportSection.ToString) & "' "

                If HI.Conn.SQLConn.ExecuteNonQuery(cmdstring, Conn.DB.DataBaseName.DB_FHS) = False Then

                    cmdstring = "delete from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FHS) & "].dbo.TTMPImportBomExcel where FTUserLogIn='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    HI.Conn.SQLConn.ExecuteOnly(cmdstring, Conn.DB.DataBaseName.DB_FHS)

                    Return False
                End If

                '------------------------------------------ Original ------------------------------------------
                cmdstring = " insert into [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FHS) & "].dbo.TTMPImportBomDevExcel ( "
                cmdstring &= vbCrLf & " FTUserLogIn, Seq, BOM_ID, BOM_ITM_ID, BOM_ROW_NBR, MSC_CODE, MSC_LEVEL_1, MSC_LEVEL_2 "
                cmdstring &= vbCrLf & " , MSC_LEVEL_3, SILHOUETTE, SEASON_CD, SEASON_YR, STYLE_NM, STYLE_NBR, STYLE_CW_CD, "
                cmdstring &= vbCrLf & "  PLUG_CW_CD, PRMRY, SCNDY, TRTRY, LOGO, ADDENDUM, FACTORY, [STATUS], COMPONENT_ORD "
                cmdstring &= vbCrLf & " , [USE], [DESCRIPTION], ITEM_TYPE_1, ITEM_TYPE_2, ITEM_TYPE_3, ITEM_TYPE_4, [IS], IT, ITEM_NBR, "
                cmdstring &= vbCrLf & " ITEM_COLOR_ORD, GCW, GCW_ORD, GCW_ART_DESCRIPTION, ITEM_COLOR_CD, ITEM_COLOR_NM, ITEM_COLOR_ABRV "
                cmdstring &= vbCrLf & " , VEND_CD, VEND_LO, VEND_NM, QTY, UOM, DEVELOPER, BOM_UPDATE_DT, "
                cmdstring &= vbCrLf & " BOM_ITM_UPDATE_DT, BOM_ITM_SETUP_DT, HK_BOM_UPDATE_DT, HK_BOM_ITM_UPDATE_DT, HK_BOM_ITM_SETPUP_DT, ReportSection "
                cmdstring &= vbCrLf & " ) "
                cmdstring &= vbCrLf & " select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & Ridx & ""
                cmdstring &= vbCrLf & " ," & Val(R!BOM_ID.ToString) & ""
                cmdstring &= vbCrLf & " ," & Val(R!BOM_ITM_ID.ToString) & ""
                cmdstring &= vbCrLf & " ," & Val(R!BOM_ROW_NBR.ToString) & ""
                cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!MSC_CODE.ToString) & "'"
                cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!MSC_LEVEL_1.ToString) & "'"
                cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!MSC_LEVEL_2.ToString) & "' "
                cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!MSC_LEVEL_3.ToString) & "'"
                cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!SILHOUETTE.ToString) & "'"
                cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!SEASON_CD.ToString) & "'"
                cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!SEASON_YR.ToString) & "'"
                cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!STYLE_NM.ToString) & "'"
                cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!STYLE_NBR.ToString) & "'"
                cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!STYLE_CW_CD.ToString) & "'"
                cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!PLUG_CW_CD.ToString) & "'"
                cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!PRMRY.ToString) & "'"
                cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!SCNDY.ToString) & "'"
                cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!TRTRY.ToString) & "'"
                cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!LOGO.ToString) & "'"
                cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!ADDENDUM.ToString) & "'"
                cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!FACTORY.ToString) & "'"
                cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!STATUS.ToString) & "'"

                If (R!QTY.ToString.Trim = "") Then
                    cmdstring &= vbCrLf & ",NULL"
                Else
                    cmdstring &= vbCrLf & "," & Val(R!COMPONENT_ORD.ToString) & ""
                End If

                cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!USE.ToString) & "'"
                cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!DESCRIPTION.ToString) & "'"
                cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!ITEM_TYPE_1.ToString) & "'"
                cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!ITEM_TYPE_2.ToString) & "'"
                cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!ITEM_TYPE_3.ToString) & "'"
                cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!ITEM_TYPE_4.ToString) & "'"
                cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!IS.ToString) & "'"
                cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!IT.ToString) & "'"
                cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!ITEM_NBR.ToString) & "'"

                If (R!QTY.ToString.Trim = "") Then
                    cmdstring &= vbCrLf & ",NULL"
                Else
                    cmdstring &= vbCrLf & "," & Val(R!ITEM_COLOR_ORD.ToString) & ""
                End If

                cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R.Item("GCW#").ToString) & "'"
                cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!GCW_ORD.ToString) & "'"
                cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!GCW_ART_DESCRIPTION.ToString) & "'"
                cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!ITEM_COLOR_CD.ToString) & "'"
                cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!ITEM_COLOR_NM.ToString) & "'"
                cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!ITEM_COLOR_ABRV.ToString) & "'"
                cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!VEND_CD.ToString) & "'"
                cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!VEND_LO.ToString) & "'"
                cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!VEND_NM.ToString) & "'"

                If (R!QTY.ToString.Trim = "") Then
                    cmdstring &= vbCrLf & ",NULL"
                Else
                    cmdstring &= vbCrLf & "," & Val(R!QTY.ToString) & ""
                End If

                cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!UOM.ToString) & "'"
                cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!DEVELOPER.ToString) & "'"
                cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!BOM_UPDATE_DT.ToString) & "'"
                cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!BOM_ITM_UPDATE_DT.ToString) & "'"
                cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!BOM_ITM_SETUP_DT.ToString) & "'"
                cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!HK_BOM_UPDATE_DT.ToString) & "'"
                cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!HK_BOM_ITM_UPDATE_DT.ToString) & "'"
                cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!HK_BOM_ITM_SETPUP_DT.ToString) & "'"
                cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!ReportSection.ToString) & "' "

                If HI.Conn.SQLConn.ExecuteNonQuery(cmdstring, Conn.DB.DataBaseName.DB_FHS) = False Then

                    cmdstring = "delete from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FHS) & "].dbo.TTMPImportBomExcel_Original where FTUserLogIn='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    HI.Conn.SQLConn.ExecuteOnly(cmdstring, Conn.DB.DataBaseName.DB_FHS)

                    Return False
                End If
                '------------------------------------------ Original ------------------------------------------

            Catch ex As Exception
                cmdstring = "delete from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FHS) & "].dbo.TTMPImportBomExcel where FTUserLogIn='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                HI.Conn.SQLConn.ExecuteOnly(cmdstring, Conn.DB.DataBaseName.DB_FHS)

                cmdstring = "delete from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FHS) & "].dbo.TTMPImportBomExcel_Original where FTUserLogIn='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                HI.Conn.SQLConn.ExecuteOnly(cmdstring, Conn.DB.DataBaseName.DB_FHS)
                Return False
            End Try

        Next

        Dim StateCheckImport As Boolean = ImportData(spls)

        If StateCheckImport = False Then
            cmdstring = "delete from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FHS) & "].dbo.TTMPImportBomExcel where FTUserLogIn='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            HI.Conn.SQLConn.ExecuteOnly(cmdstring, Conn.DB.DataBaseName.DB_FHS)

            cmdstring = "delete from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FHS) & "].dbo.TTMPImportBomExcel_Original where FTUserLogIn='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            HI.Conn.SQLConn.ExecuteOnly(cmdstring, Conn.DB.DataBaseName.DB_FHS)
            Return False
        End If
        Return True
    End Function

    Private Function ImportData(spls As HI.TL.SplashScreen) As Boolean
        Dim _StateImport As Boolean = True
        Dim _SysStyleDevId As Integer = 0
        Dim cmdstring As String = ""
        Dim Ridx As Integer = 0
        Dim TotalR As Integer = 0
        spls.UpdateInformation("Creating BOM Develope Data...")

        Try

            Dim _dtcheckmat As DataTable
            Dim _dtstyle As DataTable
            Dim _dtstyledetail As DataTable
            Dim _dtstyledetailOriginal As DataTable

            cmdstring = "SELECT STYLE_NBR, STYLE_NM, SEASON_CD, SEASON_YR,SEASON_CD + RIGHT(SEASON_YR,2) AS FTSeason,MSC_CODE, MSC_LEVEL_1, MSC_LEVEL_2, MSC_LEVEL_3, SILHOUETTE,MAX(DEVELOPER) As DEVELOPER  "
            cmdstring &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FHS) & "].dbo.TTMPImportBomExcel WITH(NOLOCK) "
            cmdstring &= vbCrLf & " where FTUserLogIn='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
            cmdstring &= vbCrLf & " GROUP Byte STYLE_NBR, STYLE_NM, SEASON_CD, SEASON_YR,SEASON_CD + RIGHT(SEASON_YR,2) AS FTSeason,MSC_CODE, MSC_LEVEL_1, MSC_LEVEL_2, MSC_LEVEL_3, SILHOUETTE "
            cmdstring &= vbCrLf & " ORDER Byte STYLE_NBR,SEASON_YR,SEASON_CD "

            _dtstyle = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_FHS)

            TotalR = _dtstyle.Rows.Count

            Dim _StyleCode As String = ""
            Dim _SeasonCode As String = ""
            Dim _StyleName As String = ""
            Dim pSeason As String = ""
            Dim pYear As String = ""
            Dim pMSC As String = ""
            Dim pMSC1 As String = ""
            Dim pMSC2 As String = ""
            Dim pMSC3 As String = ""
            Dim pDeveloper As String = ""
            Dim pSilhouette As String = ""
            Dim _MatCode As String = ""
            Dim _Unit As String = ""
            Dim _Suplier As String = ""
            Dim _UnitM As String = ""
            Dim _SuplierM As String = ""

            For Each R As DataRow In _dtstyle.Rows

                Ridx = Ridx + 1

                _StyleCode = R!STYLE_NBR.ToString
                _StyleName = R!STYLE_NM.ToString
                _SeasonCode = R!FTSeason.ToString

                pSeason = R!SEASON_CD.ToString
                pYear = R!SEASON_CD.ToString
                pMSC = R!MSC_CODE.ToString
                pMSC1 = R!MSC_LEVEL_1.ToString
                pMSC2 = R!MSC_LEVEL_2.ToString
                pMSC3 = R!MSC_LEVEL_3.ToString
                pDeveloper = R!DEVELOPER.ToString
                pSilhouette = R!SILHOUETTE.ToString

                spls.UpdateInformation("Creating BOM Data.a.... Row " & Ridx & " of  " & TotalR + " Style " + _StyleCode + " (" + _SeasonCode + ")")

                cmdstring = "Select TOP 1 FNHSysStyleDevId"
                cmdstring &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTDevelopStyle As A With(NOLOCK)"
                cmdstring &= vbCrLf & " WHERE (FTStyleDevCode = N'" & HI.UL.ULF.rpQuoted(_StyleCode) & "') "
                cmdstring &= vbCrLf & " AND (FTSeason = N'" & HI.UL.ULF.rpQuoted(_SeasonCode) & "')"
                _SysStyleDevId = Integer.Parse(Val(HI.Conn.SQLConn.GetField(cmdstring, Conn.DB.DataBaseName.DB_MERCHAN, "0")))

                If _SysStyleDevId <= 0 Then
                    _SysStyleDevId = HI.TL.RunID.GetRunNoID("TMERTDevelopStyle", "FNHSysStyleDevId", Conn.DB.DataBaseName.DB_MERCHAN)

                    cmdstring = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTDevelopStyle "
                    cmdstring &= vbCrLf & " ("
                    cmdstring &= vbCrLf & " FTInsUser, FDInsDate, FTInsTime, FNHSysStyleDevId, FTStyleDevCode, FTStyleDevNameTH, FTStyleDevNameEN, FTSeason,FTNikeDeveloperName,FTMSCCode,FTMSCLevel1,FTMSCLevel2,FTMSCLevel3,FTSilhouette "
                    cmdstring &= vbCrLf & " )"
                    cmdstring &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    cmdstring &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                    cmdstring &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ""
                    cmdstring &= vbCrLf & "," & _SysStyleDevId & ""
                    cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_StyleCode) & "'"
                    cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_StyleName) & "'"
                    cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_StyleName) & "'"
                    cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_SeasonCode) & "'"
                    cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(pMSC) & "'"
                    cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(pMSC1) & "'"
                    cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(pMSC2) & "'"
                    cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(pMSC3) & "'"
                    cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(pSilhouette) & "'"

                    If HI.Conn.SQLConn.ExecuteNonQuery(cmdstring, Conn.DB.DataBaseName.DB_MERCHAN) = False Then
                        Return False
                    End If

                    cmdstring = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTDevelopStyleOriginal "
                    cmdstring &= vbCrLf & " ("
                    cmdstring &= vbCrLf & " FTInsUser, FDInsDate, FTInsTime, FNHSysStyleDevId, FTStyleDevCode, FTStyleDevNameTH, FTStyleDevNameEN, FTSeason,FTNikeDeveloperName,FTMSCCode,FTMSCLevel1,FTMSCLevel2,FTMSCLevel3,FTSilhouette "
                    cmdstring &= vbCrLf & " )"
                    cmdstring &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    cmdstring &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                    cmdstring &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ""
                    cmdstring &= vbCrLf & "," & _SysStyleDevId & ""
                    cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_StyleCode) & "'"
                    cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_StyleName) & "'"
                    cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_StyleName) & "'"
                    cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_SeasonCode) & "'"
                    cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(pMSC) & "'"
                    cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(pMSC1) & "'"
                    cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(pMSC2) & "'"
                    cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(pMSC3) & "'"
                    cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(pSilhouette) & "'"

                    If HI.Conn.SQLConn.ExecuteNonQuery(cmdstring, Conn.DB.DataBaseName.DB_MERCHAN) = False Then
                        Return False
                    End If

                End If

                If _SysStyleDevId > 0 Then
                    cmdstring = "  Select BOM_ROW_NBR, MSC_CODE, MSC_LEVEL_1, MSC_LEVEL_2, MSC_LEVEL_3, SILHOUETTE, SEASON_CD, SEASON_YR, STYLE_NM, STYLE_NBR, STYLE_CW_CD, PLUG_CW_CD, PRMRY, SCNDY, TRTRY,  "
                    cmdstring &= vbCrLf & " LOGO, ADDENDUM, COMPONENT_ORD, [USE], DESCRIPTION, ITEM_TYPE_1, ITEM_TYPE_2, ITEM_TYPE_3, ITEM_TYPE_4, [IS], IT, ITEM_NBR, ITEM_COLOR_ORD, GCW, GCW_ORD, GCW_ART_DESCRIPTION,"
                    cmdstring &= vbCrLf & " ITEM_COLOR_CD, ITEM_COLOR_NM, ITEM_COLOR_ABRV, VEND_CD, VEND_LO, VEND_NM, QTY, UOM"
                    cmdstring &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FHS) & "].dbo.TTMPImportBomExcel WITH(NOLOCK) "
                    cmdstring &= vbCrLf & " where FTUserLogIn='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                    cmdstring &= vbCrLf & " AND STYLE_NBR='" & HI.UL.ULF.rpQuoted(_StyleCode) & "'"
                    cmdstring &= vbCrLf & " AND SEASON_CD='" & HI.UL.ULF.rpQuoted(pSeason) & "'"
                    cmdstring &= vbCrLf & " AND SEASON_YR='" & HI.UL.ULF.rpQuoted(pYear) & "'"
                    cmdstring &= vbCrLf & " GROUP BY   BOM_ROW_NBR, MSC_CODE, MSC_LEVEL_1, MSC_LEVEL_2, MSC_LEVEL_3, SILHOUETTE, SEASON_CD, SEASON_YR, STYLE_NM, STYLE_NBR, STYLE_CW_CD, PLUG_CW_CD, PRMRY, SCNDY, TRTRY,  "
                    cmdstring &= vbCrLf & " LOGO, ADDENDUM, COMPONENT_ORD, [USE], DESCRIPTION, ITEM_TYPE_1, ITEM_TYPE_2, ITEM_TYPE_3, ITEM_TYPE_4, [IS], IT, ITEM_NBR, ITEM_COLOR_ORD, GCW, GCW_ORD, GCW_ART_DESCRIPTION,"
                    cmdstring &= vbCrLf & " ITEM_COLOR_CD, ITEM_COLOR_NM, ITEM_COLOR_ABRV, VEND_CD, VEND_LO, VEND_NM, QTY, UOM"
                    cmdstring &= vbCrLf & " ORDER BY  BOM_ROW_NBR,STYLE_CW_CD "

                    _dtstyledetail = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_FHS)

                    cmdstring = "  Select BOM_ROW_NBR, MSC_CODE, MSC_LEVEL_1, MSC_LEVEL_2, MSC_LEVEL_3, SILHOUETTE, SEASON_CD, SEASON_YR, STYLE_NM, STYLE_NBR, STYLE_CW_CD, PLUG_CW_CD, PRMRY, SCNDY, TRTRY,  "
                    cmdstring &= vbCrLf & " LOGO, ADDENDUM, COMPONENT_ORD, [USE], DESCRIPTION, ITEM_TYPE_1, ITEM_TYPE_2, ITEM_TYPE_3, ITEM_TYPE_4, [IS], IT, ITEM_NBR, ITEM_COLOR_ORD, GCW, GCW_ORD, GCW_ART_DESCRIPTION,"
                    cmdstring &= vbCrLf & " ITEM_COLOR_CD, ITEM_COLOR_NM, ITEM_COLOR_ABRV, VEND_CD, VEND_LO, VEND_NM, QTY, UOM"
                    cmdstring &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FHS) & "].dbo.TTMPImportBomExcel_Original WITH(NOLOCK) "
                    cmdstring &= vbCrLf & " where FTUserLogIn='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                    cmdstring &= vbCrLf & " AND STYLE_NBR='" & HI.UL.ULF.rpQuoted(_StyleCode) & "'"
                    cmdstring &= vbCrLf & " AND SEASON_CD='" & HI.UL.ULF.rpQuoted(pSeason) & "'"
                    cmdstring &= vbCrLf & " AND SEASON_YR='" & HI.UL.ULF.rpQuoted(pYear) & "'"
                    cmdstring &= vbCrLf & " GROUP BY   BOM_ROW_NBR, MSC_CODE, MSC_LEVEL_1, MSC_LEVEL_2, MSC_LEVEL_3, SILHOUETTE, SEASON_CD, SEASON_YR, STYLE_NM, STYLE_NBR, STYLE_CW_CD, PLUG_CW_CD, PRMRY, SCNDY, TRTRY,  "
                    cmdstring &= vbCrLf & " LOGO, ADDENDUM, COMPONENT_ORD, [USE], DESCRIPTION, ITEM_TYPE_1, ITEM_TYPE_2, ITEM_TYPE_3, ITEM_TYPE_4, [IS], IT, ITEM_NBR, ITEM_COLOR_ORD, GCW, GCW_ORD, GCW_ART_DESCRIPTION,"
                    cmdstring &= vbCrLf & " ITEM_COLOR_CD, ITEM_COLOR_NM, ITEM_COLOR_ABRV, VEND_CD, VEND_LO, VEND_NM, QTY, UOM"
                    cmdstring &= vbCrLf & " ORDER BY  BOM_ROW_NBR,STYLE_CW_CD "

                    _dtstyledetailOriginal = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_FHS)


                    cmdstring = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempDevelopStyle_Mat  WHERE  FNHSysStyleDevId=" & _SysStyleDevId & " "
                    cmdstring &= vbCrLf & " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempDevelopStyle_ColorWay  WHERE  FNHSysStyleDevId=" & _SysStyleDevId & " "
                    cmdstring &= vbCrLf & " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempDevelopStyle_SizeBreakDown  WHERE  FNHSysStyleDevId=" & _SysStyleDevId & " "
                    HI.Conn.SQLConn.ExecuteNonQuery(cmdstring, Conn.DB.DataBaseName.DB_MERCHAN)

                    cmdstring = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempDevelopStyleOriginal_Mat  WHERE  FNHSysStyleDevId=" & _SysStyleDevId & " "
                    cmdstring &= vbCrLf & " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempDevelopStyleOriginal_ColorWay  WHERE  FNHSysStyleDevId=" & _SysStyleDevId & " "
                    cmdstring &= vbCrLf & " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempDevelopStyleOriginal_SizeBreakDown  WHERE  FNHSysStyleDevId=" & _SysStyleDevId & " "
                    HI.Conn.SQLConn.ExecuteNonQuery(cmdstring, Conn.DB.DataBaseName.DB_MERCHAN)

                    Dim _matseq As Integer = 0

                    For Each Rx As DataRow In _dtstyledetail.Rows
                        _matseq = _matseq + 1

                        _MatCode = R!ITEM_NBR.ToString()

                        Select Case R!UOM.ToString.ToLower
                            Case "ly", "yd", "yds"
                                _Unit = "YDS"
                            Case Else
                                _Unit = "PCS"
                        End Select

                        _Suplier = R!VEND_CD.ToString()

                        _SuplierM = _Suplier
                        _UnitM = _Unit

                        cmdstring = "SELECT TOP 1 A.FTMainMatCode, A.FTCusItemCodeRef, B.FTSuplCode, C.FTUnitCode"
                        cmdstring &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMainMat AS A WITH(NOLOCK) "
                        cmdstring &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit AS C WITH(NOLOCK) ON A.FNHSysUnitId = C.FNHSysUnitId "
                        cmdstring &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier AS B WITH(NOLOCK) ON A.FNHSysSuplId = B.FNHSysSuplId"
                        cmdstring &= vbCrLf & " WHERE A.FTCusItemCodeRef='" & HI.UL.ULF.rpQuoted(_MatCode) & "'"

                        _dtcheckmat = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_MASTER)

                        If _dtcheckmat.Rows.Count > 0 Then
                            For Each Rxm As DataRow In _dtcheckmat.Rows

                                If _UnitM = "" Then
                                    If Rxm!FTUnitCode.ToString <> _Unit And _Unit <> "" Then
                                        _Unit = ""
                                    ElseIf _Unit = "" Then
                                        _Unit = Rxm!FTUnitCode.ToString
                                    End If
                                End If

                                If _SuplierM = "" Then
                                    _Suplier = Rxm!FTSuplCode.ToString
                                End If

                                Exit For
                            Next

                        Else
                            _Unit = _UnitM
                            _Suplier = _SuplierM
                        End If

                        cmdstring = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempDevelopStyle_Mat "
                        cmdstring &= vbCrLf & " ("
                        cmdstring &= vbCrLf & " FTInsUser, FDInsDate, FTInsTime,  FNHSysStyleDevId, FNSeq"
                        cmdstring &= vbCrLf & " , FNMerMatSeq, FTItemNo, FTItemDesc, FTPartNameEN, FTPartNameTH, FTSuplCode, FTStateNominate "
                        cmdstring &= vbCrLf & " , FTUnitCode, FNPrice, FNHSysCurId, FNConSmp, FTComponent, FTStateActive"
                        cmdstring &= vbCrLf & " , FTStateCombination, FTStateMainMaterial, FTStateFree,FNPart"
                        cmdstring &= vbCrLf & " )"

                        cmdstring &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        cmdstring &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & ""
                        cmdstring &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & ""
                        cmdstring &= vbCrLf & " ," & _SysStyleDevId & ""
                        cmdstring &= vbCrLf & " ," & _matseq & ""
                        cmdstring &= vbCrLf & " ," & _matseq & ""
                        cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(_MatCode) & "'"
                        cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!FTRawMatDesc.ToString) & "'"
                        cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!FTPosittion.ToString) & "'"
                        cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!FTPosittion.ToString) & "'"
                        cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(_Suplier) & "'"

                        If R!FTSupplier.ToString.Contains("CONT") = True Then
                            cmdstring &= vbCrLf & ",'1'"
                        Else
                            cmdstring &= vbCrLf & ",'0'"
                        End If

                        cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_Unit) & "'"
                        cmdstring &= vbCrLf & ",0"
                        cmdstring &= vbCrLf & ",0"
                        cmdstring &= vbCrLf & ", " & Val(R!FNUsedQuantity.ToString) & ""
                        cmdstring &= vbCrLf & ",'','1','0','0','0',1"
                        HI.Conn.SQLConn.ExecuteNonQuery(cmdstring, Conn.DB.DataBaseName.DB_MERCHAN)

                        cmdstring = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempDevelopStyleOriginal_Mat "
                        cmdstring &= vbCrLf & " ("
                        cmdstring &= vbCrLf & " FTInsUser, FDInsDate, FTInsTime,  FNHSysStyleDevId, FNSeq"
                        cmdstring &= vbCrLf & " , FNMerMatSeq, FTItemNo, FTItemDesc, FTPartNameEN, FTPartNameTH, FTSuplCode, FTStateNominate "
                        cmdstring &= vbCrLf & " , FTUnitCode, FNPrice, FNHSysCurId, FNConSmp, FTComponent, FTStateActive"
                        cmdstring &= vbCrLf & " , FTStateCombination, FTStateMainMaterial, FTStateFree,FNPart"
                        cmdstring &= vbCrLf & " )"

                        cmdstring &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        cmdstring &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & ""
                        cmdstring &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & ""
                        cmdstring &= vbCrLf & " ," & _SysStyleDevId & ""
                        cmdstring &= vbCrLf & " ," & _matseq & ""
                        cmdstring &= vbCrLf & " ," & _matseq & ""
                        cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(_MatCode) & "'"
                        cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!FTRawMatDesc.ToString) & "'"
                        cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!FTPosittion.ToString) & "'"
                        cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!FTPosittion.ToString) & "'"
                        cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(_Suplier) & "'"

                        If R!FTSupplier.ToString.Contains("CONT") = True Then
                            cmdstring &= vbCrLf & ",'1'"
                        Else
                            cmdstring &= vbCrLf & ",'0'"
                        End If

                        cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_Unit) & "'"
                        cmdstring &= vbCrLf & ",0"
                        cmdstring &= vbCrLf & ",0"
                        cmdstring &= vbCrLf & ", " & Val(R!FNUsedQuantity.ToString) & ""
                        cmdstring &= vbCrLf & ",'','1','0','0','0',1"
                        HI.Conn.SQLConn.ExecuteNonQuery(cmdstring, Conn.DB.DataBaseName.DB_MERCHAN)

                        Dim FNColorWaySeq As Integer = 1
                        Dim _Color As String = ""
                        Dim _ColorCode As String = ""
                        Dim _ColorCode1 As String = ""
                        Dim _ColorDesc As String = ""
                        Dim _RunCount As Integer = 0

                        For Each Col As DataColumn In _dtstyledetail.Columns
                            Select Case Col.ColumnName.ToString.ToUpper
                                Case "FTUnit".ToUpper, "FNUsedQuantity".ToUpper, "FTPosittion".ToUpper, "FTRawMatDesc".ToUpper, "FTRawMatCode".ToUpper, "FTSupplier".ToUpper, "FTStyleName".ToUpper, "FTStyleNo".ToUpper, "FTSeason".ToUpper
                                Case Else

                                    _ColorDesc = ""
                                    _ColorCode = ""
                                    _RunCount = 0
                                    _ColorCode1 = ""

                                    If R.Item(Col.ColumnName.ToString).ToString.Trim() <> "" Then
                                        _Color = R.Item(Col.ColumnName.ToString).ToString.Trim()
                                        _RunCount = 0

                                        If Microsoft.VisualBasic.Left(_Color.ToUpper, 3) = "GCW" Then

                                            _ColorCode = _Color.Split(" ")(0)

                                            _ColorDesc = _Color.Replace(_ColorCode, "").Trim

                                            If _ColorDesc = "" Then
                                                _ColorDesc = _ColorCode
                                            End If

                                            _ColorDesc = ""

                                        Else

                                            For Each Str As String In _Color.Split(" ")

                                                _RunCount = _RunCount + 1

                                                If _RunCount Mod 2 = 1 Then

                                                    _ColorCode1 = Str.Trim()

                                                    If _ColorCode = "" Then
                                                        _ColorCode = Str.Trim()
                                                    Else
                                                        _ColorCode = _ColorCode & "/" & Str.Trim()
                                                    End If

                                                Else

                                                    If _ColorDesc = "" Then
                                                        _ColorDesc = _ColorCode1 & " " & Str.Trim()
                                                    Else
                                                        _ColorDesc = _ColorDesc & "/" & _ColorCode1 & " " & Str.Trim()
                                                    End If

                                                End If

                                            Next

                                            If _ColorDesc = "" Then

                                                _ColorDesc = _ColorCode

                                            End If

                                        End If

                                    End If

                                    cmdstring = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempDevelopStyle_ColorWay "
                                    cmdstring &= vbCrLf & " ("
                                    cmdstring &= vbCrLf & " FTInsUser, FDInsDate, FTInsTime, FNHSysStyleDevId, FNSeq, FNMerMatSeq"
                                    cmdstring &= vbCrLf & ", FTColorWay, FTColorCode, FTColorNameTH, FTColorNameEN, FNColorWaySeq"
                                    cmdstring &= vbCrLf & ", FTRunColor"
                                    cmdstring &= vbCrLf & " )"
                                    cmdstring &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                                    cmdstring &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                                    cmdstring &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ""
                                    cmdstring &= vbCrLf & "," & _SysStyleDevId & ""
                                    cmdstring &= vbCrLf & "," & _matseq & ""
                                    cmdstring &= vbCrLf & "," & _matseq & ""
                                    cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Microsoft.VisualBasic.Right(Col.ColumnName.ToString, Col.ColumnName.ToString.Length - 1)) & "'"
                                    cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_ColorCode) & "'"
                                    cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_ColorDesc) & "'"
                                    cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_ColorDesc) & "'"
                                    cmdstring &= vbCrLf & "," & FNColorWaySeq & ""
                                    cmdstring &= vbCrLf & ",'1'"

                                    HI.Conn.SQLConn.ExecuteNonQuery(cmdstring, Conn.DB.DataBaseName.DB_MERCHAN)

                                    cmdstring = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempDevelopStyleOriginal_ColorWay "
                                    cmdstring &= vbCrLf & " ("
                                    cmdstring &= vbCrLf & " FTInsUser, FDInsDate, FTInsTime, FNHSysStyleDevId, FNSeq, FNMerMatSeq"
                                    cmdstring &= vbCrLf & ", FTColorWay, FTColorCode, FTColorNameTH, FTColorNameEN, FNColorWaySeq"
                                    cmdstring &= vbCrLf & ", FTRunColor"
                                    cmdstring &= vbCrLf & " )"
                                    cmdstring &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                                    cmdstring &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                                    cmdstring &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ""
                                    cmdstring &= vbCrLf & "," & _SysStyleDevId & ""
                                    cmdstring &= vbCrLf & "," & _matseq & ""
                                    cmdstring &= vbCrLf & "," & _matseq & ""
                                    cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Microsoft.VisualBasic.Right(Col.ColumnName.ToString, Col.ColumnName.ToString.Length - 1)) & "'"
                                    cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_ColorCode) & "'"
                                    cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_ColorDesc) & "'"
                                    cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_ColorDesc) & "'"
                                    cmdstring &= vbCrLf & "," & FNColorWaySeq & ""
                                    cmdstring &= vbCrLf & ",'1'"

                                    HI.Conn.SQLConn.ExecuteNonQuery(cmdstring, Conn.DB.DataBaseName.DB_MERCHAN)
                                    FNColorWaySeq = FNColorWaySeq + 1

                            End Select
                        Next
                    Next

                    cmdstring = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempDevelopStyle_SizeBreakDown "
                    cmdstring &= vbCrLf & "("
                    cmdstring &= vbCrLf & "FTInsUser, FDInsDate, FTInsTime, FNHSysStyleDevId, FNSeq, FNMerMatSeq, FTSizeBreakDown"
                    cmdstring &= vbCrLf & ", FTSizeCode,FNSieBreakDownSeq,FTRunSize"
                    cmdstring &= vbCrLf & ")"
                    cmdstring &= vbCrLf & "SELECT A.FTInsUser, A.FDInsDate, A.FTInsTime, A.FNHSysStyleDevId, A.FNSeq "
                    cmdstring &= vbCrLf & ", A.FNMerMatSeq, B.FTSizeCode,'' AS FTSizeCode, B.FNSieBreakDownSeq, '1' "
                    cmdstring &= vbCrLf & "FROM"
                    cmdstring &= vbCrLf & "(SELECT FTInsUser, FDInsDate, FTInsTime, FNHSysStyleDevId, FNSeq, FNMerMatSeq "
                    cmdstring &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempDevelopStyle_Mat  WHERE  FNHSysStyleDevId=" & _SysStyleDevId & "  ) AS A CROSS JOIN"
                    cmdstring &= vbCrLf & "(SELECT 'S' AS FTSizeCode, 1 AS FNSieBreakDownSeq"
                    cmdstring &= vbCrLf & "UNION"
                    cmdstring &= vbCrLf & "SELECT 'M' AS FTSizeCode, 2 AS FNSieBreakDownSeq"
                    cmdstring &= vbCrLf & "UNION"
                    cmdstring &= vbCrLf & "SELECT 'L' AS FTSizeCode, 3 AS FNSieBreakDownSeq"
                    cmdstring &= vbCrLf & "UNION"
                    cmdstring &= vbCrLf & "SELECT 'XL' AS FTSizeCode, 4 AS FNSieBreakDownSeq) AS B"

                    HI.Conn.SQLConn.ExecuteNonQuery(cmdstring, Conn.DB.DataBaseName.DB_MERCHAN)

                    cmdstring = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempDevelopStyleOriginal_SizeBreakDown "
                    cmdstring &= vbCrLf & "("
                    cmdstring &= vbCrLf & "FTInsUser, FDInsDate, FTInsTime, FNHSysStyleDevId, FNSeq, FNMerMatSeq, FTSizeBreakDown"
                    cmdstring &= vbCrLf & ", FTSizeCode,FNSieBreakDownSeq,FTRunSize"
                    cmdstring &= vbCrLf & ")"
                    cmdstring &= vbCrLf & "SELECT A.FTInsUser, A.FDInsDate, A.FTInsTime, A.FNHSysStyleDevId, A.FNSeq "
                    cmdstring &= vbCrLf & ", A.FNMerMatSeq, B.FTSizeCode,'' AS FTSizeCode, B.FNSieBreakDownSeq, '1' "
                    cmdstring &= vbCrLf & "FROM"
                    cmdstring &= vbCrLf & "(SELECT FTInsUser, FDInsDate, FTInsTime, FNHSysStyleDevId, FNSeq, FNMerMatSeq "
                    cmdstring &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempDevelopStyleOriginal_Mat  WHERE  FNHSysStyleDevId=" & _SysStyleDevId & "  ) AS A CROSS JOIN"
                    cmdstring &= vbCrLf & "(SELECT 'S' AS FTSizeCode, 1 AS FNSieBreakDownSeq"
                    cmdstring &= vbCrLf & "UNION"
                    cmdstring &= vbCrLf & "SELECT 'M' AS FTSizeCode, 2 AS FNSieBreakDownSeq"
                    cmdstring &= vbCrLf & "UNION"
                    cmdstring &= vbCrLf & "SELECT 'L' AS FTSizeCode, 3 AS FNSieBreakDownSeq"
                    cmdstring &= vbCrLf & "UNION"
                    cmdstring &= vbCrLf & "SELECT 'XL' AS FTSizeCode, 4 AS FNSieBreakDownSeq) AS B"

                    HI.Conn.SQLConn.ExecuteNonQuery(cmdstring, Conn.DB.DataBaseName.DB_MERCHAN)

                    cmdstring = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTDevelopStyle_Mat"
                    cmdstring &= vbCrLf & " ("
                    cmdstring &= vbCrLf & " FTInsUser, FDInsDate, FTInsTime, FTUpdUser, FDUpdDate, FTUpdTime, FNHSysStyleDevId"
                    cmdstring &= vbCrLf & ", FNSeq, FNMerMatSeq, FTItemNo, FTItemDesc, FNPart, FTPartNameEN, FTPartNameTH, FTSuplCode "
                    cmdstring &= vbCrLf & ", FTStateNominate, FTUnitCode, FNPrice, FNHSysCurId, FNConSmp, FNConSmpPlus, FTComponent "
                    cmdstring &= vbCrLf & ", FTStateActive, FTStateCombination, FTStateMainMaterial, FTStateFree, FTPositionPartId "
                    cmdstring &= vbCrLf & ", FTPart"
                    cmdstring &= vbCrLf & " )"
                    cmdstring &= vbCrLf & " SELECT  A.FTInsUser, A.FDInsDate, A.FTInsTime, A.FTUpdUser, A.FDUpdDate, A.FTUpdTime, A.FNHSysStyleDevId, A.FNSeq, A.FNMerMatSeq, A.FTItemNo, A.FTItemDesc, A.FNPart, A.FTPartNameEN, A.FTPartNameTH, "
                    cmdstring &= vbCrLf & " A.FTSuplCode, A.FTStateNominate, A.FTUnitCode, A.FNPrice, A.FNHSysCurId, A.FNConSmp, A.FNConSmpPlus, A.FTComponent, A.FTStateActive, A.FTStateCombination, A.FTStateMainMaterial, A.FTStateFree,"
                    cmdstring &= vbCrLf & " A.FTPositionPartId, A.FTPart"
                    cmdstring &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempDevelopStyle_Mat AS A LEFT OUTER JOIN"
                    cmdstring &= vbCrLf & "       [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTDevelopStyle_Mat AS B ON A.FNHSysStyleDevId = B.FNHSysStyleDevId AND A.FNSeq = B.FNSeq AND A.FNMerMatSeq = B.FNMerMatSeq AND A.FTItemNo = B.FTItemNo AND A.FNPart = B.FNPart"
                    cmdstring &= vbCrLf & " WHERE  A.FNHSysStyleDevId=" & _SysStyleDevId & " "
                    cmdstring &= vbCrLf & " AND  (B.FNHSysStyleDevId Is NULL) "

                    HI.Conn.SQLConn.ExecuteNonQuery(cmdstring, Conn.DB.DataBaseName.DB_MERCHAN)

                    cmdstring = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTDevelopStyleOriginal_Mat"
                    cmdstring &= vbCrLf & " ("
                    cmdstring &= vbCrLf & " FTInsUser, FDInsDate, FTInsTime, FTUpdUser, FDUpdDate, FTUpdTime, FNHSysStyleDevId"
                    cmdstring &= vbCrLf & ", FNSeq, FNMerMatSeq, FTItemNo, FTItemDesc, FNPart, FTPartNameEN, FTPartNameTH, FTSuplCode "
                    cmdstring &= vbCrLf & ", FTStateNominate, FTUnitCode, FNPrice, FNHSysCurId, FNConSmp, FNConSmpPlus, FTComponent "
                    cmdstring &= vbCrLf & ", FTStateActive, FTStateCombination, FTStateMainMaterial, FTStateFree, FTPositionPartId "
                    cmdstring &= vbCrLf & ", FTPart"
                    cmdstring &= vbCrLf & " )"
                    cmdstring &= vbCrLf & " SELECT  A.FTInsUser, A.FDInsDate, A.FTInsTime, A.FTUpdUser, A.FDUpdDate, A.FTUpdTime, A.FNHSysStyleDevId, A.FNSeq, A.FNMerMatSeq, A.FTItemNo, A.FTItemDesc, A.FNPart, A.FTPartNameEN, A.FTPartNameTH, "
                    cmdstring &= vbCrLf & " A.FTSuplCode, A.FTStateNominate, A.FTUnitCode, A.FNPrice, A.FNHSysCurId, A.FNConSmp, A.FNConSmpPlus, A.FTComponent, A.FTStateActive, A.FTStateCombination, A.FTStateMainMaterial, A.FTStateFree,"
                    cmdstring &= vbCrLf & " A.FTPositionPartId, A.FTPart"
                    cmdstring &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempDevelopStyleOriginal_Mat AS A LEFT OUTER JOIN"
                    cmdstring &= vbCrLf & "       [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTDevelopStyleOriginal_Mat AS B ON A.FNHSysStyleDevId = B.FNHSysStyleDevId AND A.FNSeq = B.FNSeq AND A.FNMerMatSeq = B.FNMerMatSeq AND A.FTItemNo = B.FTItemNo AND A.FNPart = B.FNPart"
                    cmdstring &= vbCrLf & " WHERE  A.FNHSysStyleDevId=" & _SysStyleDevId & " "
                    cmdstring &= vbCrLf & " AND  (B.FNHSysStyleDevId Is NULL) "

                    HI.Conn.SQLConn.ExecuteNonQuery(cmdstring, Conn.DB.DataBaseName.DB_MERCHAN)

                    cmdstring = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTDevelopStyle_ColorWay"
                    cmdstring &= vbCrLf & "("
                    cmdstring &= vbCrLf & "FTInsUser, FDInsDate, FTInsTime, FTUpdUser, FDUpdDate, FTUpdTime, FNHSysStyleDevId"
                    cmdstring &= vbCrLf & ", FNSeq, FNMerMatSeq, FTColorWay, FNColorWaySeq, FTRunColor, FTColorCode, FTColorNameTH,  FTColorNameEN"
                    cmdstring &= vbCrLf & ")"
                    cmdstring &= vbCrLf & "SELECT  A.FTInsUser, A.FDInsDate, A.FTInsTime, A.FTUpdUser, A.FDUpdDate, A.FTUpdTime"
                    cmdstring &= vbCrLf & ", A.FNHSysStyleDevId, A.FNSeq, A.FNMerMatSeq, A.FTColorWay, A.FNColorWaySeq"
                    cmdstring &= vbCrLf & ", A.FTRunColor, A.FTColorCode, A.FTColorNameTH, A.FTColorNameEN"
                    cmdstring &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempDevelopStyle_ColorWay AS A "
                    cmdstring &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTDevelopStyle_ColorWay AS B "
                    cmdstring &= vbCrLf & "ON A.FNHSysStyleDevId = B.FNHSysStyleDevId AND A.FNSeq = B.FNSeq "
                    cmdstring &= vbCrLf & "And A.FNMerMatSeq = B.FNMerMatSeq And A.FTColorWay = B.FTColorWay "
                    cmdstring &= vbCrLf & "WHERE A.FNHSysStyleDevId=" & _SysStyleDevId & " "
                    cmdstring &= vbCrLf & "AND (B.FNHSysStyleDevId Is NULL) "

                    HI.Conn.SQLConn.ExecuteNonQuery(cmdstring, Conn.DB.DataBaseName.DB_MERCHAN)

                    cmdstring = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTDevelopStyleOriginal_ColorWay"
                    cmdstring &= vbCrLf & "("
                    cmdstring &= vbCrLf & "FTInsUser, FDInsDate, FTInsTime, FTUpdUser, FDUpdDate, FTUpdTime, FNHSysStyleDevId"
                    cmdstring &= vbCrLf & ", FNSeq, FNMerMatSeq, FTColorWay, FNColorWaySeq, FTRunColor, FTColorCode, FTColorNameTH,  FTColorNameEN"
                    cmdstring &= vbCrLf & ")"
                    cmdstring &= vbCrLf & "SELECT  A.FTInsUser, A.FDInsDate, A.FTInsTime, A.FTUpdUser, A.FDUpdDate, A.FTUpdTime"
                    cmdstring &= vbCrLf & ", A.FNHSysStyleDevId, A.FNSeq, A.FNMerMatSeq, A.FTColorWay, A.FNColorWaySeq"
                    cmdstring &= vbCrLf & ", A.FTRunColor, A.FTColorCode, A.FTColorNameTH, A.FTColorNameEN"
                    cmdstring &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempDevelopStyleOriginal_ColorWay AS A "
                    cmdstring &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTDevelopStyleOriginal_ColorWay AS B "
                    cmdstring &= vbCrLf & "ON A.FNHSysStyleDevId = B.FNHSysStyleDevId AND A.FNSeq = B.FNSeq "
                    cmdstring &= vbCrLf & "And A.FNMerMatSeq = B.FNMerMatSeq And A.FTColorWay = B.FTColorWay "
                    cmdstring &= vbCrLf & "WHERE A.FNHSysStyleDevId=" & _SysStyleDevId & " "
                    cmdstring &= vbCrLf & "AND (B.FNHSysStyleDevId Is NULL) "

                    HI.Conn.SQLConn.ExecuteNonQuery(cmdstring, Conn.DB.DataBaseName.DB_MERCHAN)

                    cmdstring = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTDevelopStyleOriginal_SizeBreakDown"
                    cmdstring &= vbCrLf & "( "
                    cmdstring &= vbCrLf & "FTInsUser, FDInsDate, FTInsTime, FTUpdUser, FDUpdDate, FTUpdTime, FNHSysStyleDevId"
                    cmdstring &= vbCrLf & ", FNSeq, FNMerMatSeq, FTSizeBreakDown, FNSieBreakDownSeq, FTRunSize, FTSizeCode"
                    cmdstring &= vbCrLf & ") "
                    cmdstring &= vbCrLf & "SELECT A.FTInsUser, A.FDInsDate, A.FTInsTime, A.FTUpdUser, A.FDUpdDate, A.FTUpdTime"
                    cmdstring &= vbCrLf & ", A.FNHSysStyleDevId, A.FNSeq, A.FNMerMatSeq, A.FTSizeBreakDown, A.FNSieBreakDownSeq"
                    cmdstring &= vbCrLf & ", A.FTRunSize, A.FTSizeCode "
                    cmdstring &= vbCrLf
                    cmdstring &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempDevelopStyleOriginal_SizeBreakDown AS A "
                    cmdstring &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTDevelopStyleOriginal_SizeBreakDown AS B "
                    cmdstring &= vbCrLf & "ON A.FNHSysStyleDevId = B.FNHSysStyleDevId AND A.FNSeq = B.FNSeq AND A.FNMerMatSeq = B.FNMerMatSeq "
                    cmdstring &= vbCrLf & "And A.FTSizeBreakDown = B.FTSizeBreakDown "
                    cmdstring &= vbCrLf & "WHERE  A.FNHSysStyleDevId=" & _SysStyleDevId & " "
                    cmdstring &= vbCrLf & "AND  (B.FNHSysStyleDevId Is NULL) "

                    HI.Conn.SQLConn.ExecuteNonQuery(cmdstring, Conn.DB.DataBaseName.DB_MERCHAN)

                    cmdstring = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTDevelopStyle_SizeBreakDown"
                    cmdstring &= vbCrLf & "( "
                    cmdstring &= vbCrLf & "FTInsUser, FDInsDate, FTInsTime, FTUpdUser, FDUpdDate, FTUpdTime, FNHSysStyleDevId"
                    cmdstring &= vbCrLf & ", FNSeq, FNMerMatSeq, FTSizeBreakDown, FNSieBreakDownSeq, FTRunSize, FTSizeCode"
                    cmdstring &= vbCrLf & ") "
                    cmdstring &= vbCrLf & "SELECT A.FTInsUser, A.FDInsDate, A.FTInsTime, A.FTUpdUser, A.FDUpdDate, A.FTUpdTime"
                    cmdstring &= vbCrLf & ", A.FNHSysStyleDevId, A.FNSeq, A.FNMerMatSeq, A.FTSizeBreakDown, A.FNSieBreakDownSeq"
                    cmdstring &= vbCrLf & ", A.FTRunSize, A.FTSizeCode "
                    cmdstring &= vbCrLf
                    cmdstring &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempDevelopStyle_SizeBreakDown AS A "
                    cmdstring &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTDevelopStyle_SizeBreakDown AS B "
                    cmdstring &= vbCrLf & "ON A.FNHSysStyleDevId = B.FNHSysStyleDevId AND A.FNSeq = B.FNSeq AND A.FNMerMatSeq = B.FNMerMatSeq "
                    cmdstring &= vbCrLf & "And A.FTSizeBreakDown = B.FTSizeBreakDown "
                    cmdstring &= vbCrLf & "WHERE  A.FNHSysStyleDevId=" & _SysStyleDevId & " "
                    cmdstring &= vbCrLf & "AND  (B.FNHSysStyleDevId Is NULL) "

                    HI.Conn.SQLConn.ExecuteNonQuery(cmdstring, Conn.DB.DataBaseName.DB_MERCHAN)

                End If
            Next
        Catch ex As Exception
            Return False
        End Try

        Return _StateImport
    End Function

End Class
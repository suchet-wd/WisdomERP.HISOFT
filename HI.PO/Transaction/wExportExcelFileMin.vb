Imports Microsoft.Office.Interop.Excel
Imports System.Windows.Forms
Imports System.Text
Imports System.Drawing
Imports System
Imports System.IO


Public Class wExportExcelFileMin


    Private PathFileExcel As String = ""
    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ocmexportrycexcel_Click(sender As Object, e As EventArgs) Handles ocmexporttoexcel.Click

        If Me.FNHSysBuyId.Text <> "" Then
            Select Case FNFileMinimumType.SelectedIndex
                Case 0
                    If Me.FTFilePath.Text.Trim = "" Then
                        HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, ogbselectfile.Text)
                        FTFilePath.Focus()
                        Exit Sub
                    End If
                Case 1

                Case 2

                    PathFileExcel = HI.Conn.SQLConn.GetField("SELECT TOP 1  FTCfgData FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSESystemConfig AS X WITH(NOLOCK) WHERE X.FTCfgName='ImportBomExcel'", Conn.DB.DataBaseName.DB_SYSTEM)

                    If CheckWriteFile() = False Then
                        HI.MG.ShowMsg.mInfo("ไม่สามารถเข้าถึงที่เก็บ File สำหรับ Import ได้ !!!!", 1909270019, Me.Text)
                        Exit Sub
                    End If
            End Select


            Dim _Spls As New HI.TL.SplashScreen("Loading...Data Pleas wait.")
                Dim _Cmd As String = ""
                Dim _dt As System.Data.DataTable
                Dim _UserName As String = HI.ST.UserInfo.UserName

            ' _UserName = "mlpsirikanya"
            Select Case FNFileMinimumType.SelectedIndex
                    Case 0
                    '_Cmd = "EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.SP_GET_DATA_SURCHARGE '" & HI.UL.ULF.rpQuoted(_UserName) & "'," & Val(FNHSysBuyId.Properties.Tag.ToString) & ""
                    _Cmd = "EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.USP_EXPORT_MIN_ORDERPROD '" & HI.UL.ULF.rpQuoted(_UserName) & "'," & Val(FNHSysBuyId.Properties.Tag.ToString) & ""
                Case 1
                    '_Cmd = "EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.SP_EXPORT_MIN_CRAFT_SALEMAN '" & HI.UL.ULF.rpQuoted(_UserName) & "'," & Val(FNHSysBuyId.Properties.Tag.ToString) & ""
                    _Cmd = "EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.USP_EXPORT_MIN_ORDER_CRAFT_SALEMAN '" & HI.UL.ULF.rpQuoted(_UserName) & "'," & Val(FNHSysBuyId.Properties.Tag.ToString) & ""

                Case 2


                    Dim FileName As String = PathFileExcel & "\" & HI.ST.UserInfo.UserName & ".xlsx"
                    opshet.SaveDocument(PathFileExcel & "\" & HI.ST.UserInfo.UserName & ".xlsx")


                    '_Cmd = "EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.SP_GET_DATA_SURCHARGE '" & HI.UL.ULF.rpQuoted(_UserName) & "'," & Val(FNHSysBuyId.Properties.Tag.ToString) & ""
                    _Cmd = "EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.USP_EXPORT_MIN_ORDERPROD_NOORDER '" & HI.UL.ULF.rpQuoted(_UserName) & "'," & Val(FNHSysBuyId.Properties.Tag.ToString) & ",'" & HI.UL.ULF.rpQuoted(FileName) & "'"
            End Select

            _dt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PUR)

                If _dt.Rows.Count > 0 Then
                    _Spls.Close()


                Select Case FNFileMinimumType.SelectedIndex
                    Case 0
                        Dim _SplsWriteData As New HI.TL.SplashScreen("Writing...Data Pleas wait.")
                        'Call WriteDataToExcel(_dt, _SplsWriteData)
                        Call WriteDataToExcelSpret(_dt, _SplsWriteData)
                    Case 2
                        Dim _SplsWriteData As New HI.TL.SplashScreen("Writing...Data Pleas wait.")
                        'Call WriteDataToExcel(_dt, _SplsWriteData)
                        Call WriteDataToExcelSpretNOOrder(_dt, _SplsWriteData)

                    Case 1

                        Try
                            Dim Op As New System.Windows.Forms.SaveFileDialog
                            Op.Filter = "Excel Files(.xlsx)|*.xlsx"
                            Op.ShowDialog()

                            Try
                                If Op.FileName <> "" Then

                                    Dim pathfilemin As String = Op.FileName
                                    Dim _SplsWriteData As New HI.TL.SplashScreen("Writing...Data Pleas wait.")
                                    Call WriteDataCraftSaleManMinToExcel(_dt, pathfilemin, _SplsWriteData)

                                End If

                            Catch ex As Exception
                            End Try
                        Catch ex As Exception
                        End Try

                End Select

                Else

                    _Spls.Close()
                    HI.MG.ShowMsg.mInfo("ไม่พบข้อมูลที่ต้องการทำการ Export กรุณาทำการตรวจสอบ !!!", 1610013254, Me.Text, , MessageBoxIcon.Warning)

                End If



        Else

            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, FNHSysBuyId_lbl.Text)
            FNHSysBuyId.Focus()

        End If

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

    Private Sub wExportYRCExcel_Load(sender As Object, e As EventArgs) Handles Me.Load

    End Sub

    Private PathExcelName As String = ""
    Private PathFilelName As String = ""
    Private PathExtensionName As String = ""

    Private Sub FTFilePath_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles FTFilePath.ButtonClick
        Select Case e.Button.Index
            Case 0
                Try
                    Dim opFileDialog As New System.Windows.Forms.OpenFileDialog
                    opFileDialog.Filter = "Excel Files(*.xls;*.xlsx;*.csv)|*.xls;*.xlsx;*.csv"
                    opFileDialog.ShowDialog()

                    Try
                        If opFileDialog.FileName <> "" Then
                            Dim _Pls As New HI.TL.SplashScreen("Reading...File Please Wait...")

                            Try
                                Dim _FileName As String = opFileDialog.FileName
                                FTFilePath.Text = _FileName

                                PathExcelName = System.IO.Path.GetFullPath(opFileDialog.FileName)
                                PathFilelName = System.IO.Path.GetFileName(opFileDialog.FileName)
                                PathExtensionName = System.IO.Path.GetExtension(opFileDialog.FileName)

                                Try
                                    Dim proc = Process.GetProcessesByName("excel")
                                    For i As Integer = 0 To proc.Count - 1
                                        proc(i).Kill()
                                    Next i
                                Catch ex As Exception
                                End Try

                                'Dim stream As New FileStream(_FileName, FileMode.Open)
                                'Dim length As Long = stream.Length
                                'Dim data(length) As Byte 'New Byte(length)
                                'stream.Read(data, 0, Integer.Parse(length))

                                opshet.LoadDocument(File.ReadAllBytes(_FileName), DevExpress.Spreadsheet.DocumentFormat.Xlsx)

                            Catch ex As Exception
                            End Try

                            _Pls.Close()

                        End If
                    Catch ex As Exception
                    End Try

                Catch ex As Exception
                    Throw New Exception(ex.Message().ToString() & Environment.NewLine & ex.StackTrace.ToString())
                End Try

            Case Else
                '...do nothing
        End Select
    End Sub

    Private Sub WriteDataToExcel(ByVal _oDt As System.Data.DataTable, _Spls As HI.TL.SplashScreen)
        Dim WriteLoop As Integer = 0

        Try

LoopWrite:

            Dim _Qry As String = ""
            Dim _Rec As Integer = 0
            Dim _RecError As Integer = 0
            Dim _TotalRec As Integer = 0
            Dim _l As Integer = 0
            Dim _SumPQtyDay As Double = 0
            Dim _SumUQtyDay As Double = 0
            Dim misValue As Object = System.Reflection.Missing.Value
            Dim TmpFile As String = FTFilePath.Text
            Dim BakFile As String = FTFilePath.Text
            Dim _FileName As String = FTFilePath.Text
            Dim _dtWrite As System.Data.DataTable = _oDt.Copy

            Dim oExcel As New Microsoft.Office.Interop.Excel.Application
            Dim xlBookTmp As Workbook
            Dim Excel As New Microsoft.Office.Interop.Excel.XlDirection
            xlBookTmp = oExcel.Workbooks.Open(TmpFile)



            Dim _Style As String = ""
            Dim _Colorway As String = ""
            Dim _CusPo As String = ""
            Dim _PoLine As String = ""
            Dim _Season As String = ""
            Dim _Year As String = ""
            Dim _Part As String = ""
            Dim _MatCode As String = ""
            Dim _CusItemCodeRef As String = ""
            Dim _RawMatColor As String = ""
            Dim _RawMatColorName As String = ""
            Dim _Vender As String = ""

            Dim i As Integer = 4
            Dim _StartColMatA As Integer = 64
            Dim _StartColMatB As Integer = 75
            Dim _StartColMatC As Integer = 86
            Dim _StartColMatD As Integer = 97
            Dim _StartColMatE As Integer = 108
            Dim _StartColMatF As Integer = 119
            Dim _StartColMatG As Integer = 130
            Dim _StartColMatH As Integer = 141
            Dim _StartColMat As Integer = 64
            Dim _WriteColData As Integer = 64
            Dim _StateWriteData As Boolean = False
            Dim _AddColMat As Integer = 11
            Dim _optiplan As Double = 0
            Dim _RoundWirteDataBlank As Integer = 0
            Dim _TotalExcelRow As Integer = opshet.ActiveWorksheet.GetUsedRange().RowCount - 1
            With xlBookTmp.Sheets(opshet.ActiveWorksheet.Index + 1)

                opshet.BeginUpdate()
                For z As Integer = i To opshet.ActiveWorksheet.GetUsedRange().RowCount - 1

                    _Spls.UpdateInformation("Wiriting Data Excel Row  " & z & "  of  " & _TotalExcelRow & "")

                    Try
                        _Style = .Cells(z, 3).Value.ToString.Trim
                    Catch ex As Exception
                        _Style = ""
                    End Try

                    Try
                        _Colorway = Microsoft.VisualBasic.Right(_Style, 3)
                    Catch ex As Exception
                        _Colorway = ""
                    End Try

                    Try
                        _Style = Microsoft.VisualBasic.Left(_Style, 6)
                    Catch ex As Exception
                        _Style = ""
                    End Try

                    Try
                        _CusPo = .Cells(z, 8).Value.ToString.Trim
                    Catch ex As Exception
                        _CusPo = ""
                    End Try

                    Try
                        _PoLine = Val(.Cells(z, 9).Value.ToString.Trim).ToString
                    Catch ex As Exception
                        _PoLine = ""
                    End Try

                    Try
                        _Season = .Cells(z, 15).Value.ToString.Trim
                    Catch ex As Exception
                        _Season = ""
                    End Try

                    Try
                        _Year = .Cells(z, 16).Value.ToString.Trim
                    Catch ex As Exception
                        _Year = ""
                    End Try

                    Try
                        _Year = Microsoft.VisualBasic.Right(_Year, 2)
                    Catch ex As Exception
                        _Year = ""
                    End Try

                    _StartColMat = 64
                    _RoundWirteDataBlank = 0

                    Dim StateWrite As Boolean = False
                    For Each R As DataRow In _oDt.Select("FTStyleCode ='" & HI.UL.ULF.rpQuoted(_Style) & "' AND FTMatColorCode='" & HI.UL.ULF.rpQuoted(_Colorway) & "' AND FTPOref='" & HI.UL.ULF.rpQuoted(_CusPo) & "' AND FTNikePOLineItem='" & HI.UL.ULF.rpQuoted(_PoLine) & "'", "FNSeq,FTPart")

                        _MatCode = R!FTMainMatCode.ToString
                        _Part = R!FTPart.ToString

                        For Each Rx As DataRow In _dtWrite.Select("FTStyleCode ='" & HI.UL.ULF.rpQuoted(_Style) & "' AND FTMatColorCode='" & HI.UL.ULF.rpQuoted(_Colorway) & "' AND FTPOref='" & HI.UL.ULF.rpQuoted(_CusPo) & "' AND FTNikePOLineItem='" & HI.UL.ULF.rpQuoted(_PoLine) & "' AND FTMainMatCode='" & HI.UL.ULF.rpQuoted(_MatCode) & "' AND FTPart='" & HI.UL.ULF.rpQuoted(_Part) & "' AND FTStateWrite<>'1'", "FNSeq,FTPart")

                            _CusItemCodeRef = Rx!FTCusItemCodeRef.ToString
                            _RawMatColor = Rx!FTRawMatColorCode.ToString
                            _RawMatColorName = Rx!FTRawMatColorNameEN.ToString
                            _Vender = Rx!FTSuplCode.ToString
                            _optiplan = Val(Rx!FNOptiplan.ToString)

                            StateWrite = True

                            Select Case _Part.ToUpper
                                Case "A"

                                    _WriteColData = _StartColMatA
                                    _StartColMat = _StartColMatB

                                Case "B"

                                    _WriteColData = _StartColMatB
                                    _StartColMat = _StartColMatC

                                Case "C"

                                    _WriteColData = _StartColMatC
                                    _StartColMat = _StartColMatD

                                Case "D"

                                    _WriteColData = _StartColMatD
                                    _StartColMat = _StartColMatE

                                Case "E"

                                    _WriteColData = _StartColMatE
                                    _StartColMat = _StartColMatF

                                Case "F"

                                    _WriteColData = _StartColMatF
                                    _StartColMat = _StartColMatG

                                Case "G"

                                    _WriteColData = _StartColMatG
                                    _StartColMat = _StartColMatH

                                Case "H"

                                    _WriteColData = _StartColMatH

                                Case Else

                                    If _StateWriteData = True Then

                                        If _RoundWirteDataBlank > 0 Then
                                            _StartColMat = _StartColMat + _AddColMat
                                        End If

                                        _RoundWirteDataBlank = _RoundWirteDataBlank + 1

                                    End If

                                    _WriteColData = _StartColMat

                            End Select

                            '.Cells(z, _WriteColData) = "'" & _CusItemCodeRef
                            '.Cells(z, _WriteColData + 1) = "'" & _RawMatColor
                            '.Cells(z, _WriteColData + 2) = "'" & _RawMatColorName
                            '.Cells(z, _WriteColData + 3) = "'" & _Vender

                            If _WriteColData <= _StartColMatH Then

                                .Cells(z, _WriteColData) = _CusItemCodeRef
                                .Cells(z, _WriteColData + 1) = _RawMatColor
                                .Cells(z, _WriteColData + 2) = _RawMatColorName
                                .Cells(z, _WriteColData + 3) = _Vender
                                .Cells(z, _WriteColData + 6) = _optiplan

                            End If

                            _StateWriteData = True
                            Exit For
                        Next

                        For Each Rx2 As DataRow In _dtWrite.Select("FTStyleCode ='" & HI.UL.ULF.rpQuoted(_Style) & "' AND FTMatColorCode='" & HI.UL.ULF.rpQuoted(_Colorway) & "' AND FTPOref='" & HI.UL.ULF.rpQuoted(_CusPo) & "' AND FTNikePOLineItem='" & HI.UL.ULF.rpQuoted(_PoLine) & "' AND FTMainMatCode='" & HI.UL.ULF.rpQuoted(_MatCode) & "' AND FTPart='" & HI.UL.ULF.rpQuoted(_Part) & "' AND FTStateWrite<>'1'", "FNSeq,FTPart")
                            Rx2!FTStateWrite = "1"
                        Next

                    Next

                Next

                opshet.EndUpdate()
            End With

            Try
                CType(oExcel.Application.ActiveWorkbook.Sheets(1), Worksheet).Select()
            Catch ex As Exception
            End Try

            oExcel.DisplayAlerts = False
            xlBookTmp.Save()
            xlBookTmp.Close()
            _Spls.Close()


            Try

                Try
                    Dim proc = Process.GetProcessesByName("excel")
                    For ix As Integer = 0 To proc.Count - 1
                        proc(ix).Kill()
                    Next ix
                Catch ex As Exception
                End Try

                Dim stream As New FileStream(_FileName, FileMode.Open)
                Dim length As Long = stream.Length
                Dim data(length) As Byte 'New Byte(length)
                stream.Read(data, 0, Integer.Parse(length))

                opshet.LoadDocument(data, DevExpress.Spreadsheet.DocumentFormat.Xlsx)

                Try

                    Dim proc = Process.GetProcessesByName("excel")
                    For ix As Integer = 0 To proc.Count - 1
                        proc(ix).Kill()
                    Next ix

                Catch ex As Exception
                End Try
            Catch ex As Exception
            End Try

            HI.MG.ShowMsg.mInfo("Write Data Complete !!!", 1610100587, Me.Text, , MessageBoxIcon.Information)

            Process.Start(_FileName)

        Catch ex As Exception

            If WriteLoop <= 0 Then
                WriteLoop = 1
                GoTo LoopWrite
            End If

            _Spls.Close()
            HI.MG.ShowMsg.mProcessError(15066029917, "Writing Excel File Error....." & vbCrLf & ex.Message.ToString, Me.Text, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub WriteDataToExcelSpret(ByVal _oDt As System.Data.DataTable, _Spls As HI.TL.SplashScreen)
        Dim WriteLoop As Integer = 0

        Try

LoopWrite:

            Dim _Qry As String = ""
            Dim _Rec As Integer = 0
            Dim _RecError As Integer = 0
            Dim _TotalRec As Integer = 0
            Dim _l As Integer = 0
            Dim _SumPQtyDay As Double = 0
            Dim _SumUQtyDay As Double = 0
            Dim misValue As Object = System.Reflection.Missing.Value
            Dim TmpFile As String = FTFilePath.Text
            Dim BakFile As String = FTFilePath.Text
            Dim _FileName As String = FTFilePath.Text
            Dim _dtWrite As System.Data.DataTable = _oDt.Copy



            Dim _Style As String = ""
            Dim _Colorway As String = ""
            Dim _CusPo As String = ""
            Dim _PoLine As String = ""
            Dim _Season As String = ""
            Dim _Year As String = ""
            Dim _Part As String = ""
            Dim _MatCode As String = ""
            Dim _CusItemCodeRef As String = ""
            Dim _RawMatColor As String = ""
            Dim _RawMatColorName As String = ""
            Dim _Vender As String = ""

            Dim i As Integer = 3
            Dim _StartColMatA As Integer = 63
            Dim _StartColMatB As Integer = 74
            Dim _StartColMatC As Integer = 85
            Dim _StartColMatD As Integer = 96
            Dim _StartColMatE As Integer = 107
            Dim _StartColMatF As Integer = 118
            Dim _StartColMatG As Integer = 129
            Dim _StartColMatH As Integer = 140
            Dim _StartColMat As Integer = 63
            Dim _WriteColData As Integer = 63
            Dim _StateWriteData As Boolean = False
            Dim _AddColMat As Integer = 11
            Dim _optiplan As Double = 0
            Dim _RoundWirteDataBlank As Integer = 0
            Dim _TotalExcelRow As Integer = opshet.ActiveWorksheet.GetUsedRange().RowCount - 1

            opshet.BeginUpdate()
            With opshet.ActiveWorksheet

                For z As Integer = i To opshet.ActiveWorksheet.GetUsedRange().RowCount - 1

                    _Spls.UpdateInformation("Wiriting Data Excel Row  " & z & "  of  " & _TotalExcelRow & "")

                    Try
                        _Style = .Cells(z, 2).Value.ToString.Trim
                    Catch ex As Exception
                        _Style = ""
                    End Try

                    Try
                        _Colorway = Microsoft.VisualBasic.Right(_Style, 3)
                    Catch ex As Exception
                        _Colorway = ""
                    End Try

                    Try
                        _Style = Microsoft.VisualBasic.Left(_Style, 6)
                    Catch ex As Exception
                        _Style = ""
                    End Try

                    Try
                        _CusPo = .Cells(z, 7).Value.ToString.Trim
                    Catch ex As Exception
                        _CusPo = ""
                    End Try

                    Try
                        _PoLine = Val(.Cells(z, 8).Value.ToString.Trim).ToString
                    Catch ex As Exception
                        _PoLine = ""
                    End Try

                    Try
                        _Season = .Cells(z, 14).Value.ToString.Trim
                    Catch ex As Exception
                        _Season = ""
                    End Try

                    Try
                        _Year = .Cells(z, 15).Value.ToString.Trim
                    Catch ex As Exception
                        _Year = ""
                    End Try

                    Try
                        _Year = Microsoft.VisualBasic.Right(_Year, 2)
                    Catch ex As Exception
                        _Year = ""
                    End Try

                    _StartColMat = 63
                    _RoundWirteDataBlank = 0

                    Dim StateWrite As Boolean = False


                    For Each R As DataRow In _oDt.Select("FTStyleCode ='" & HI.UL.ULF.rpQuoted(_Style) & "' AND FTMatColorCode='" & HI.UL.ULF.rpQuoted(_Colorway) & "' AND FTPOref='" & HI.UL.ULF.rpQuoted(_CusPo) & "' AND FTNikePOLineItem='" & HI.UL.ULF.rpQuoted(_PoLine) & "'", "FNSeq,FTPart")

                        _MatCode = R!FTMainMatCode.ToString
                        _Part = R!FTPart.ToString

                        For Each Rx As DataRow In _dtWrite.Select("FTStyleCode ='" & HI.UL.ULF.rpQuoted(_Style) & "' AND FTMatColorCode='" & HI.UL.ULF.rpQuoted(_Colorway) & "' AND FTPOref='" & HI.UL.ULF.rpQuoted(_CusPo) & "' AND FTNikePOLineItem='" & HI.UL.ULF.rpQuoted(_PoLine) & "' AND FTMainMatCode='" & HI.UL.ULF.rpQuoted(_MatCode) & "' AND FTPart='" & HI.UL.ULF.rpQuoted(_Part) & "' AND FTStateWrite<>'1'", "FNSeq,FTPart")

                            _CusItemCodeRef = Rx!FTCusItemCodeRef.ToString
                            _RawMatColor = Rx!FTRawMatColorCode.ToString
                            _RawMatColorName = Rx!FTRawMatColorNameEN.ToString
                            _Vender = Rx!FTSuplCode.ToString
                            _optiplan = Val(Rx!FNOptiplan.ToString)

                            StateWrite = True

                            Select Case _Part.ToUpper
                                Case "A"

                                    _WriteColData = _StartColMatA
                                    _StartColMat = _StartColMatB

                                Case "B"

                                    _WriteColData = _StartColMatB
                                    _StartColMat = _StartColMatC

                                Case "C"

                                    _WriteColData = _StartColMatC
                                    _StartColMat = _StartColMatD

                                Case "D"

                                    _WriteColData = _StartColMatD
                                    _StartColMat = _StartColMatE

                                Case "E"

                                    _WriteColData = _StartColMatE
                                    _StartColMat = _StartColMatF

                                Case "F"

                                    _WriteColData = _StartColMatF
                                    _StartColMat = _StartColMatG

                                Case "G"

                                    _WriteColData = _StartColMatG
                                    _StartColMat = _StartColMatH

                                Case "H"

                                    _WriteColData = _StartColMatH

                                Case Else

                                    If _StateWriteData = True Then

                                        If _RoundWirteDataBlank > 0 Then
                                            _StartColMat = _StartColMat + _AddColMat
                                        End If

                                        _RoundWirteDataBlank = _RoundWirteDataBlank + 1

                                    End If

                                    _WriteColData = _StartColMat

                            End Select

                            '.Cells(z, _WriteColData) = "'" & _CusItemCodeRef
                            '.Cells(z, _WriteColData + 1) = "'" & _RawMatColor
                            '.Cells(z, _WriteColData + 2) = "'" & _RawMatColorName
                            '.Cells(z, _WriteColData + 3) = "'" & _Vender

                            If _WriteColData <= _StartColMatH Then

                                .Rows(z).Item(_WriteColData).Value = _CusItemCodeRef
                                .Rows(z).Item(_WriteColData + 1).Value = _RawMatColor
                                .Rows(z).Item(_WriteColData + 2).Value = _RawMatColorName
                                .Rows(z).Item(_WriteColData + 3).Value = _Vender
                                .Rows(z).Item(_WriteColData + 6).Value = _optiplan


                                '.Cells(z, _WriteColData) = _CusItemCodeRef
                                '.Cells(z, _WriteColData + 1) = _RawMatColor
                                '.Cells(z, _WriteColData + 2) = _RawMatColorName
                                '.Cells(z, _WriteColData + 3) = _Vender
                                '.Cells(z, _WriteColData + 6) = _optiplan

                            End If

                            _StateWriteData = True
                            Exit For
                        Next

                        For Each Rx2 As DataRow In _dtWrite.Select("FTStyleCode ='" & HI.UL.ULF.rpQuoted(_Style) & "' AND FTMatColorCode='" & HI.UL.ULF.rpQuoted(_Colorway) & "' AND FTPOref='" & HI.UL.ULF.rpQuoted(_CusPo) & "' AND FTNikePOLineItem='" & HI.UL.ULF.rpQuoted(_PoLine) & "' AND FTMainMatCode='" & HI.UL.ULF.rpQuoted(_MatCode) & "' AND FTPart='" & HI.UL.ULF.rpQuoted(_Part) & "' AND FTStateWrite<>'1'", "FNSeq,FTPart")
                            Rx2!FTStateWrite = "1"
                        Next

                    Next

                Next

            End With

            opshet.EndUpdate()

            _Spls.UpdateInformation("Exporting.... Please wait")
            Dim FileName As String = _FileName.ToUpper().Replace(PathExtensionName.ToUpper(), "_" & HI.ST.UserInfo.UserName & PathExtensionName)
            opshet.SaveDocument(FileName)

            _Spls.Close()

            Try

                Try
                    Dim proc = Process.GetProcessesByName("excel")
                    For ix As Integer = 0 To proc.Count - 1
                        proc(ix).Kill()
                    Next ix
                Catch ex As Exception
                End Try

                Dim stream As New FileStream(_FileName, FileMode.Open)
                Dim length As Long = stream.Length
                Dim data(length) As Byte 'New Byte(length)
                stream.Read(data, 0, Integer.Parse(length))

                opshet.LoadDocument(data, DevExpress.Spreadsheet.DocumentFormat.Xlsx)

                Try

                    Dim proc = Process.GetProcessesByName("excel")
                    For ix As Integer = 0 To proc.Count - 1
                        proc(ix).Kill()
                    Next ix

                Catch ex As Exception
                End Try
            Catch ex As Exception
            End Try

            HI.MG.ShowMsg.mInfo("Write Data Complete !!!", 1610100587, Me.Text, , MessageBoxIcon.Information)

            ' Process.Start(_FileName)
            Process.Start(FileName)


        Catch ex As Exception

            If WriteLoop <= 0 Then
                WriteLoop = 1
                GoTo LoopWrite
            End If

            _Spls.Close()
            HI.MG.ShowMsg.mProcessError(15066029917, "Writing Excel File Error....." & vbCrLf & ex.Message.ToString, Me.Text, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub WriteDataToExcelSpretNOOrder(ByVal _oDt As System.Data.DataTable, _Spls As HI.TL.SplashScreen)
        Dim WriteLoop As Integer = 0

        Try

LoopWrite:

            Dim _Qry As String = ""
            Dim _Rec As Integer = 0
            Dim _RecError As Integer = 0
            Dim _TotalRec As Integer = 0
            Dim _l As Integer = 0
            Dim _SumPQtyDay As Double = 0
            Dim _SumUQtyDay As Double = 0
            Dim misValue As Object = System.Reflection.Missing.Value
            Dim TmpFile As String = FTFilePath.Text
            Dim BakFile As String = FTFilePath.Text
            Dim _FileName As String = FTFilePath.Text
            Dim _dtWrite As System.Data.DataTable = _oDt.Copy



            Dim _Style As String = ""
            Dim _Colorway As String = ""
            Dim _CusPo As String = ""
            Dim _PoLine As String = ""
            Dim _Season As String = ""
            Dim _Year As String = ""
            Dim _Part As String = ""
            Dim _MatCode As String = ""
            Dim _CusItemCodeRef As String = ""
            Dim _RawMatColor As String = ""
            Dim _RawMatColorName As String = ""
            Dim _Vender As String = ""

            Dim i As Integer = 3
            Dim _StartColMatA As Integer = 63
            Dim _StartColMatB As Integer = 74
            Dim _StartColMatC As Integer = 85
            Dim _StartColMatD As Integer = 96
            Dim _StartColMatE As Integer = 107
            Dim _StartColMatF As Integer = 118
            Dim _StartColMatG As Integer = 129
            Dim _StartColMatH As Integer = 140
            Dim _StartColMat As Integer = 63
            Dim _WriteColData As Integer = 63
            Dim _StateWriteData As Boolean = False
            Dim _AddColMat As Integer = 11
            Dim _optiplan As Double = 0
            Dim _RoundWirteDataBlank As Integer = 0
            Dim _TotalExcelRow As Integer = opshet.ActiveWorksheet.GetUsedRange().RowCount - 1

            opshet.BeginUpdate()
            With opshet.ActiveWorksheet

                For z As Integer = i To opshet.ActiveWorksheet.GetUsedRange().RowCount - 1

                    _Spls.UpdateInformation("Wiriting Data Excel Row  " & z & "  of  " & _TotalExcelRow & "")

                    Try
                        _Style = .Cells(z, 2).Value.ToString.Trim
                    Catch ex As Exception
                        _Style = ""
                    End Try

                    Try
                        _Colorway = Microsoft.VisualBasic.Right(_Style, 3)
                    Catch ex As Exception
                        _Colorway = ""
                    End Try

                    Try
                        _Style = Microsoft.VisualBasic.Left(_Style, 6)
                    Catch ex As Exception
                        _Style = ""
                    End Try

                    Try
                        _CusPo = .Cells(z, 7).Value.ToString.Trim
                    Catch ex As Exception
                        _CusPo = ""
                    End Try

                    Try
                        _PoLine = Val(.Cells(z, 8).Value.ToString.Trim).ToString
                    Catch ex As Exception
                        _PoLine = ""
                    End Try

                    Try
                        _Season = .Cells(z, 14).Value.ToString.Trim
                    Catch ex As Exception
                        _Season = ""
                    End Try

                    Try
                        _Year = .Cells(z, 15).Value.ToString.Trim
                    Catch ex As Exception
                        _Year = ""
                    End Try

                    Try
                        _Year = Microsoft.VisualBasic.Right(_Year, 2)
                    Catch ex As Exception
                        _Year = ""
                    End Try

                    _StartColMat = 63
                    _RoundWirteDataBlank = 0

                    Dim StateWrite As Boolean = False


                    For Each R As DataRow In _oDt.Select("FTStyleCode ='" & HI.UL.ULF.rpQuoted(_Style) & "' AND FTMatColorCode='" & HI.UL.ULF.rpQuoted(_Colorway) & "' AND FTPOref='' AND FTNikePOLineItem=''", "FNSeq,FTPart")

                        _MatCode = R!FTMainMatCode.ToString
                        _Part = R!FTPart.ToString

                        For Each Rx As DataRow In _dtWrite.Select("FTStyleCode ='" & HI.UL.ULF.rpQuoted(_Style) & "' AND FTMatColorCode='" & HI.UL.ULF.rpQuoted(_Colorway) & "' AND FTPOref='' AND FTNikePOLineItem='' AND FTMainMatCode='" & HI.UL.ULF.rpQuoted(_MatCode) & "' AND FTPart='" & HI.UL.ULF.rpQuoted(_Part) & "' AND FTStateWrite<>'1'", "FNSeq,FTPart")

                            _CusItemCodeRef = Rx!FTCusItemCodeRef.ToString
                            _RawMatColor = Rx!FTRawMatColorCode.ToString
                            _RawMatColorName = Rx!FTRawMatColorNameEN.ToString
                            _Vender = Rx!FTSuplCode.ToString
                            _optiplan = Val(Rx!FNOptiplan.ToString)

                            StateWrite = True

                            Select Case _Part.ToUpper
                                Case "A"

                                    _WriteColData = _StartColMatA
                                    _StartColMat = _StartColMatB

                                Case "B"

                                    _WriteColData = _StartColMatB
                                    _StartColMat = _StartColMatC

                                Case "C"

                                    _WriteColData = _StartColMatC
                                    _StartColMat = _StartColMatD

                                Case "D"

                                    _WriteColData = _StartColMatD
                                    _StartColMat = _StartColMatE

                                Case "E"

                                    _WriteColData = _StartColMatE
                                    _StartColMat = _StartColMatF

                                Case "F"

                                    _WriteColData = _StartColMatF
                                    _StartColMat = _StartColMatG

                                Case "G"

                                    _WriteColData = _StartColMatG
                                    _StartColMat = _StartColMatH

                                Case "H"

                                    _WriteColData = _StartColMatH

                                Case Else

                                    If _StateWriteData = True Then

                                        If _RoundWirteDataBlank > 0 Then
                                            _StartColMat = _StartColMat + _AddColMat
                                        End If

                                        _RoundWirteDataBlank = _RoundWirteDataBlank + 1

                                    End If

                                    _WriteColData = _StartColMat

                            End Select

                            '.Cells(z, _WriteColData) = "'" & _CusItemCodeRef
                            '.Cells(z, _WriteColData + 1) = "'" & _RawMatColor
                            '.Cells(z, _WriteColData + 2) = "'" & _RawMatColorName
                            '.Cells(z, _WriteColData + 3) = "'" & _Vender

                            If _WriteColData <= _StartColMatH Then

                                .Rows(z).Item(_WriteColData).Value = _CusItemCodeRef
                                .Rows(z).Item(_WriteColData + 1).Value = _RawMatColor
                                .Rows(z).Item(_WriteColData + 2).Value = _RawMatColorName
                                .Rows(z).Item(_WriteColData + 3).Value = _Vender
                                .Rows(z).Item(_WriteColData + 6).Value = _optiplan


                                '.Cells(z, _WriteColData) = _CusItemCodeRef
                                '.Cells(z, _WriteColData + 1) = _RawMatColor
                                '.Cells(z, _WriteColData + 2) = _RawMatColorName
                                '.Cells(z, _WriteColData + 3) = _Vender
                                '.Cells(z, _WriteColData + 6) = _optiplan

                            End If

                            _StateWriteData = True
                            Exit For
                        Next

                        For Each Rx2 As DataRow In _dtWrite.Select("FTStyleCode ='" & HI.UL.ULF.rpQuoted(_Style) & "' AND FTMatColorCode='" & HI.UL.ULF.rpQuoted(_Colorway) & "' AND FTPOref='' AND FTNikePOLineItem='' AND FTMainMatCode='" & HI.UL.ULF.rpQuoted(_MatCode) & "' AND FTPart='" & HI.UL.ULF.rpQuoted(_Part) & "' AND FTStateWrite<>'1'", "FNSeq,FTPart")
                            Rx2!FTStateWrite = "1"
                        Next

                    Next

                Next

            End With

            opshet.EndUpdate()

            _Spls.UpdateInformation("Exporting.... Please wait")
            Dim FileName As String = _FileName.ToUpper().Replace(PathExtensionName.ToUpper(), "_" & HI.ST.UserInfo.UserName & PathExtensionName)
            opshet.SaveDocument(FileName)

            _Spls.Close()

            Try

                Try
                    Dim proc = Process.GetProcessesByName("excel")
                    For ix As Integer = 0 To proc.Count - 1
                        proc(ix).Kill()
                    Next ix
                Catch ex As Exception
                End Try

                Dim stream As New FileStream(_FileName, FileMode.Open)
                Dim length As Long = stream.Length
                Dim data(length) As Byte 'New Byte(length)
                stream.Read(data, 0, Integer.Parse(length))

                opshet.LoadDocument(data, DevExpress.Spreadsheet.DocumentFormat.Xlsx)

                Try

                    Dim proc = Process.GetProcessesByName("excel")
                    For ix As Integer = 0 To proc.Count - 1
                        proc(ix).Kill()
                    Next ix

                Catch ex As Exception
                End Try
            Catch ex As Exception
            End Try

            HI.MG.ShowMsg.mInfo("Write Data Complete !!!", 1610100587, Me.Text, , MessageBoxIcon.Information)

            ' Process.Start(_FileName)
            Process.Start(FileName)


        Catch ex As Exception

            If WriteLoop <= 0 Then
                WriteLoop = 1
                GoTo LoopWrite
            End If

            _Spls.Close()
            HI.MG.ShowMsg.mProcessError(15066029917, "Writing Excel File Error....." & vbCrLf & ex.Message.ToString, Me.Text, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub WriteDataCraftSaleManMinToExcel(ByVal _oDt As System.Data.DataTable, pathfilemin As String, _Spls As HI.TL.SplashScreen)

        opshetsale.BeginUpdate()
        With opshetsale.ActiveWorksheet

            Dim RowIndx As Integer = 0
            Dim StartRow As Integer = 2
            Dim Qty As Decimal = 0.00
            Dim Amt As Decimal = 0.00
            Dim TotalQty As Decimal = 0.00

            For Each RXi As System.Data.DataRow In _oDt.Rows



                .Rows(StartRow + RowIndx).Insert()
                .Rows(StartRow + RowIndx).CopyFrom(.Rows(StartRow + RowIndx + 1), DevExpress.Spreadsheet.PasteSpecial.All)

                .Rows(StartRow + RowIndx).Item(0).Value = RXi!FTStyleCode.ToString 'Style
                .Rows(StartRow + RowIndx).Item(1).Value = RXi!FTColorWay.ToString 'Cway
                .Rows(StartRow + RowIndx).Item(2).Value = RXi!FTRawMatColorName.ToString 'Color
                .Rows(StartRow + RowIndx).Item(3).Value = Val(RXi!FNQuantityPO.ToString()) 'Qty Minimum
                .Rows(StartRow + RowIndx).Item(4).Value = Val(RXi![FNQuantity].ToString()) 'Usage
                .Rows(StartRow + RowIndx).Item(5).Value = (Val(RXi!FNQuantityPO.ToString()) - Val(RXi![FNQuantity].ToString())) 'Qty Surcharge
                .Rows(StartRow + RowIndx).Item(6).Value = Val(RXi!FNPrice.ToString())  'Price/ usd$
                ' .Rows(StartRow + RowIndx).Item(7).Value = Val(RXi!FNOrderQuantity.ToString) 'Surcharge (MOQ) 
                ' .Rows(StartRow + RowIndx).Item(8).Value = Val(RXi!FNOrderQuantity.ToString) 'TOTAL SURCHARGE  (Amount $ ) 
                .Rows(StartRow + RowIndx).Item(9).Value = RXi!FTCusItemCodeRef.ToString 'Item
                .Rows(StartRow + RowIndx).Item(10).Value = RXi!FTSuplCode.ToString 'Vendor.
                .Rows(StartRow + RowIndx).Item(11).Value = RXi!FTPurchaseNo.ToString 'PO NO.

                RowIndx = RowIndx + 1

            Next
            .Rows(StartRow + (RowIndx + 3)).Item(8).Formula = "SUM(I3:I" & (StartRow + (RowIndx + 3)).ToString & ")"
        End With

        opshetsale.EndUpdate()
        opshetsale.SaveDocument(pathfilemin)

        _Spls.Close()


        'Try
        '    Try

        '        Dim proc = Process.GetProcessesByName("excel")
        '        For ix As Integer = 0 To proc.Count - 1
        '            proc(ix).Kill()
        '        Next ix

        '    Catch ex As Exception
        '    End Try
        'Catch ex As Exception
        'End Try

        HI.MG.ShowMsg.mInfo("Write Data Complete !!!", 1610100587, Me.Text, , MessageBoxIcon.Information)

        Call LoadFormatFile()
        Try
            Process.Start(pathfilemin)
        Catch ex As Exception

        End Try


    End Sub

    Private Sub FNFileMinimumType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles FNFileMinimumType.SelectedIndexChanged
        Select Case FNFileMinimumType.SelectedIndex
            Case 0

                FTFilePath.Text = ""
                opshet.CloseCellEditor(DevExpress.XtraSpreadsheet.CellEditorEnterValueMode.Default)
                opshet.CreateNewDocument()

                otpexportprod.PageVisible = True
                otpexportsaleman.PageVisible = False
            Case 1
                otpexportprod.PageVisible = False
                otpexportsaleman.PageVisible = True
                Call LoadFormatFile()
            Case Else

                FTFilePath.Text = ""
                opshet.CloseCellEditor(DevExpress.XtraSpreadsheet.CellEditorEnterValueMode.Default)
                opshet.CreateNewDocument()

                otpexportprod.PageVisible = True
                otpexportsaleman.PageVisible = False

        End Select
    End Sub

    Private Sub LoadFormatFile()
        Dim _FileName As String = System.Windows.Forms.Application.StartupPath & "\ExportPOFormat\"

        _FileName = _FileName & "MinimumSalesman.xlsx"

        Try

            Dim proc = Process.GetProcessesByName("excel")

            For i As Integer = 0 To proc.Count - 1
                proc(i).Kill()
            Next i

        Catch ex As Exception
        End Try

        Select Case Path.GetExtension(_FileName)
            Case ".xls"
                opshetsale.LoadDocument(File.ReadAllBytes(_FileName), DevExpress.Spreadsheet.DocumentFormat.Xls)

            Case ".xlsx"
                opshetsale.LoadDocument(File.ReadAllBytes(_FileName), DevExpress.Spreadsheet.DocumentFormat.Xlsx)

            Case ".xlsm"
                opshetsale.LoadDocument(File.ReadAllBytes(_FileName), DevExpress.Spreadsheet.DocumentFormat.Xlsm)

            Case Else
                opshetsale.LoadDocument(File.ReadAllBytes(_FileName), DevExpress.Spreadsheet.DocumentFormat.Xlsx)

        End Select
    End Sub

    Private Sub FTFilePath_EditValueChanged(sender As Object, e As EventArgs) Handles FTFilePath.EditValueChanged

    End Sub
End Class
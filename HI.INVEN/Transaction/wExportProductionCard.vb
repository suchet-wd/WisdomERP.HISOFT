Imports Microsoft.Office.Interop.Excel
Imports System.Windows.Forms
Imports System.Text
Imports System.Drawing
Imports System
Imports System.IO
Imports DevExpress.XtraSpreadsheet.Model
Imports DevExpress.Spreadsheet

Public Class wExportProductionCard
    Private _DefailtPath As String

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ocmloaddata_Click(sender As Object, e As EventArgs) Handles ocmloaddata.Click



        With CType(ogcpurchase.DataSource, System.Data.DataTable)
            .AcceptChanges()
            If .Rows.Count <= 0 Then
                HI.MG.ShowMsg.mInfo("กรุณาทำการระบุหมายเลย PO !!!", 1612813254, Me.Text, , MessageBoxIcon.Warning)
                Exit Sub
            End If

        End With


        Dim Op As New System.Windows.Forms.FolderBrowserDialog

        If _DefailtPath <> "" Then
            Op.SelectedPath = _DefailtPath
        End If

        Dim FileNameRef As String = ""
        If Op.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            If _DefailtPath <> Op.SelectedPath Then

                WriteRegistry(Op.SelectedPath)
                _DefailtPath = Op.SelectedPath

            End If
            Dim _Spls As New HI.TL.SplashScreen("Loading...Data Pleas wait.")

            Dim StateExpoort As Boolean = False

            StateExpoort = LoadData()

            _Spls.Close()

            If StateExpoort Then
                HI.MG.ShowMsg.mInfo("Export Data Complete !!!", 1614413254, Me.Text, , MessageBoxIcon.Information)
                Process.Start("explorer.exe", _DefailtPath)
            Else
                HI.MG.ShowMsg.mInfo("Not Found Data For Export !!!", 1619922254, Me.Text, , MessageBoxIcon.Warning)
            End If

            'Try
            '    Process.Start("explorer.exe", _DefailtPath)
            'Catch ex As Exception

            'End Try
        End If




    End Sub


    Private Sub wExportYRCExcel_Load(sender As Object, e As EventArgs) Handles Me.Load

        _DefailtPath = ""

        Try
            LoadPurchaseNo("")
            _DefailtPath = ReadRegistry()
        Catch ex As Exception

        End Try

    End Sub

    Public Shared Function ReadRegistry() As String
        Dim regKey As Microsoft.Win32.RegistryKey
        Dim valreturn As String = ""

        regKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software\HI SOFT", True)

        If regKey Is Nothing Then

            Microsoft.Win32.Registry.CurrentUser.CreateSubKey("Software\HI SOFT", Microsoft.Win32.RegistryKeyPermissionCheck.ReadWriteSubTree)
            regKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software\HI SOFT", True)

        End If

        valreturn = regKey.GetValue("PathExportPDItem", "")
        regKey.Close()

        Return valreturn
    End Function

    Public Shared Sub WriteRegistry(ByVal value As Object)

        Dim regKey As Microsoft.Win32.RegistryKey
        regKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software\HI SOFT", True)

        If regKey Is Nothing Then

            Microsoft.Win32.Registry.CurrentUser.CreateSubKey("Software\HI SOFT", True)
            regKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software\HI SOFT", True)

        End If

        regKey.SetValue("PathExportPDItem", value.ToString)
        regKey.Close()

    End Sub

    Private Sub ClearTab()
        Me.otb.TabPages.Clear()
    End Sub

    Private Function LoadData() As Boolean
        Dim _State As Boolean = False
        Call ClearTab()

        Dim _Spls As New HI.TL.SplashScreen("Generating Data.. Please Wait ")
        Try

            Dim _Rec As Integer = 0

            Dim purchaseno As String
            Dim FTStyleCode As String
            Dim _Qry As String = ""
            Dim dt As New DataTable
            Dim dtbreakdown As New DataTable
            Dim dtsub As New DataTable
            Dim dtall As New DataTable
            Dim dtitem As New DataTable
            Dim MatID As Integer = 0
            Dim dts As New DataSet
            Dim _OrderList As String = ""
            Dim _dtponoAll As DataTable
            Dim _dtpono As DataTable
            Dim _FNSTQuantity As Integer = 0
            Dim _ProdCode As String = ""
            Dim cmdString As String = ""


            With CType(ogcpurchase.DataSource, DataTable)
                .AcceptChanges()
                _dtponoAll = .Copy
            End With


            Dim _TotalRec As Integer = _dtponoAll.Rows.Count

            For Each Rpx As DataRow In _dtponoAll.Rows
                purchaseno = Rpx!FTPurchaseNo.ToString
                _ProdCode = ""
                MatID = 0

                _Rec = _Rec + 1

                'cmdString = "Select        P.FNHSysRawMatId "
                'cmdString &= vbCrLf & "	, MAX(IM.FTRawMatCode + Case When   ISNULL(IMC.FTRawMatColorCode,'')='' THEN '' ELSE '-'+ ISNULL(IMC.FTRawMatColorCode,'') END"
                'cmdString &= vbCrLf & " +  CASE WHEN  ISNULL(IMS.FTRawMatSizeCode,'') ='' THEN '' ELSE '-'+ISNULL(IMS.FTRawMatSizeCode,'') END) AS FTRawMatCode"
                'cmdString &= vbCrLf & " From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo AS P WITH(NOLOCK) INNER Join"
                'cmdString &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial As IM WITH(NOLOCK) On P.FNHSysRawMatId = IM.FNHSysRawMatId LEFT OUTER Join"
                'cmdString &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatSize AS IMS WITH(NOLOCK) ON IM.FNHSysRawMatSizeId = IMS.FNHSysRawMatSizeId LEFT OUTER Join"
                'cmdString &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatColor As IMC WITH(NOLOCK) On IM.FNHSysRawMatColorId = IMC.FNHSysRawMatColorId"
                'cmdString &= vbCrLf & " Where P.FTPurchaseNo ='" & HI.UL.ULF.rpQuoted(purchaseno) & "' "
                'cmdString &= vbCrLf & " GROUP BY  P.FNHSysRawMatId "

                '_dtpono = HI.Conn.SQLConn.GetDataTable(cmdString, Conn.DB.DataBaseName.DB_PUR)

                'For Each Rx As DataRow In _dtpono.Rows

                '    MatID = Val(Rx!FNHSysRawMatId.ToString)
                '    _ProdCode = Rx!FTRawMatCode.ToString


                _Spls.UpdateInformation("Generating Data " & purchaseno & " Rawmat Code   " & _ProdCode & " Record  " & _Rec.ToString & " Of " & _TotalRec.ToString & "  (" & Format((_Rec * 100.0) / _TotalRec, "0.00") & " % ) ")


                    _Qry = "Exec [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.USP_EXPORT_PRODCARD '" & HI.UL.ULF.rpQuoted(purchaseno) & "' "

                ' _Qry = "Exec [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.USP_EXPORT_PRODCARD_BY_MATID '" & HI.UL.ULF.rpQuoted(purchaseno) & "'," & MatID & " "


                dts = New DataSet
                    HI.Conn.SQLConn.GetDataSet(_Qry, Conn.DB.DataBaseName.DB_INVEN, dts)

                    If dts.Tables.Count >= 4 Then
                        _ProdCode = _ProdCode.Replace("\", "").Replace("/", "")
                        dtbreakdown = dts.Tables(0).Copy
                        dtsub = dts.Tables(1).Copy
                        dtall = dts.Tables(2).Copy
                        dtitem = dts.Tables(3).Copy


                        If dtbreakdown.Rows.Count > 0 And dtsub.Rows.Count > 0 Then

                            Dim MatCode As String = ""
                            Dim MatPOQty As Decimal = 0
                            Dim UnitCode As String = ""

                            Try
                                MatCode = dtitem.Rows(0)!FTMatCode.ToString
                                MatPOQty = Val(dtitem.Rows(0)!FNQuantity.ToString)
                                UnitCode = dtitem.Rows(0)!FTUnitCode.ToString
                            Catch ex As Exception

                            End Try

                            Dim Otp As New DevExpress.XtraTab.XtraTabPage()
                            Dim ogcSpreadsheet As New DevExpress.XtraSpreadsheet.SpreadsheetControl()

                            With Otp
                                .Name = "T" & purchaseno.ToString & purchaseno.ToString & "" & MatID.ToString
                                .Text = purchaseno & "  " & _ProdCode
                            End With
                            With ogcSpreadsheet
                                .Name = "spc" & purchaseno.ToString & purchaseno.ToString & "" & MatID.ToString

                            End With
                            Otp.Controls.Add(ogcSpreadsheet)

                            otb.TabPages.Add(Otp)
                            ogcSpreadsheet.Dock = DockStyle.Fill

                            Dim TotalSize As Integer = (dtbreakdown.Columns.Count - 4)

                            Dim StartRow As Integer = 5
                            Dim StartCol1 As Integer = 0
                            Dim StartCol2 As Integer = 6 + TotalSize
                            Dim RowIdx As Integer = 0
                            Dim ColIdx As Integer = 0

                            With ogcSpreadsheet.ActiveWorksheet


                                .Rows(0).Item(9).Value = "Production Order"
                                .Rows(0).Item(9).Font.Bold = True

                                .Rows(2).Item(0).Value = "PO. No. :"
                                .Rows(2).Item(0).Font.Bold = True
                                .Rows(2).Item(0).FillColor = Color.LightCyan

                                .Rows(2).Item(1).Value = purchaseno
                                .Rows(2).Item(1).Font.Bold = True
                                .Rows(2).Item(1).FillColor = Color.LightCyan
                                .Rows(2).Item(2).FillColor = Color.LightCyan

                                .Rows(2).Item(3).Value = "QTY :"
                                .Rows(2).Item(3).Font.Bold = True
                                .Rows(2).Item(3).FillColor = Color.LightCyan

                                .Rows(2).Item(4).Value = MatPOQty.ToString()
                                .Rows(2).Item(4).Font.Bold = True
                                .Rows(2).Item(4).FillColor = Color.LightCyan

                                .Rows(2).Item(5).Value = "'" & UnitCode.ToString
                                .Rows(2).Item(5).Font.Bold = True
                                .Rows(2).Item(5).FillColor = Color.LightCyan


                                .Rows(2).Item(8).Value = "Mat Code :"
                                .Rows(2).Item(8).Font.Bold = True

                                .Rows(2).Item(9).Value = "'" & MatCode.ToString
                                .Rows(2).Item(9).Font.Bold = True

                                .Rows(3).Item(0).Value = "จำนวน PCS"
                                .Rows(3).Item(StartCol2 + 1).Value = "จำนวนห่อ/เศษ"

                                .Rows(4).RowHeight = 100

                                .Rows(4).Item(0).Value = "Style No."
                                .Rows(4).Item(0).Font.Bold = True
                                '.Rows(4).Item(0).Alignment.Horizontal = True
                                '.Rows(4).Item(0).Alignment.Vertical = True
                                .Rows(4).Item(0).ColumnWidth = 120
                                .Rows(4).Item(0).FillColor = Color.LightCyan

                                .Rows(4).Item(1).Value = "Job No."
                                .Rows(4).Item(1).Font.Bold = True
                                '.Rows(4).Item(1).Alignment.Horizontal = True
                                '.Rows(4).Item(1).Alignment.Vertical = True
                                .Rows(4).Item(1).ColumnWidth = 120
                                .Rows(4).Item(1).FillColor = Color.LightCyan

                                .Rows(4).Item(2).Value = "Color Way"
                                .Rows(4).Item(2).Font.Bold = True
                                '.Rows(4).Item(2).Alignment.Horizontal = True
                                '.Rows(4).Item(2).Alignment.Vertical = True
                                .Rows(4).Item(2).ColumnWidth = 100
                                .Rows(4).Item(2).FillColor = Color.LightCyan

                                .Rows(4).Item(2 + TotalSize + 1).Value = "Total"
                                .Rows(4).Item(2 + TotalSize + 1).Font.Bold = True
                                '.Rows(4).Item(2 + TotalSize+1).Alignment.Horizontal = True
                                '.Rows(4).Item(2 + TotalSize+1).Alignment.Vertical = True
                                .Rows(4).Item(2 + TotalSize + 1).ColumnWidth = 100
                                .Rows(4).Item(2 + TotalSize + 1).FillColor = Color.LightCyan

                                .Rows(4).Item(2 + TotalSize + 2).Value = "Shipment Date"
                                .Rows(4).Item(2 + TotalSize + 2).Font.Bold = True
                                '.Rows(4).Item(2 + TotalSize + 2).Alignment.Horizontal = True
                                '.Rows(4).Item(2 + TotalSize + 2).Alignment.Vertical = True
                                '.Rows(4).Item(2 + TotalSize + 1).Alignment.WrapText = True
                                .Rows(4).Item(2 + TotalSize + 2).ColumnWidth = 100
                                .Rows(4).Item(2 + TotalSize + 2).FillColor = Color.LightCyan

                                .Rows(4).Item(2 + TotalSize + 3).Value = "จำนวน/กล่อง"
                                .Rows(4).Item(2 + TotalSize + 3).Font.Bold = True
                                '.Rows(4).Item(2 + TotalSize + 3).Alignment.Horizontal = True
                                '.Rows(4).Item(2 + TotalSize + 3).Alignment.Vertical = True
                                '.Rows(4).Item(2 + TotalSize + 3).Alignment.WrapText = True
                                .Rows(4).Item(2 + TotalSize + 3).ColumnWidth = 80
                                .Rows(4).Item(2 + TotalSize + 3).FillColor = Color.LightCyan


                                .Rows(4).Item(StartCol2 + TotalSize + 1).Value = "Remark"
                                .Rows(4).Item(StartCol2 + TotalSize + 1).Font.Bold = True
                                '.Rows(4).Item(StartCol2 + TotalSize+1).Alignment.Horizontal = True
                                '.Rows(4).Item(StartCol2 + TotalSize+1).Alignment.Vertical = True
                                '.Rows(4).Item(StartCol2 + TotalSize+1).Alignment.WrapText = True
                                .Rows(4).Item(StartCol2 + TotalSize + 1).ColumnWidth = 80
                                .Rows(4).Item(StartCol2 + TotalSize + 1).FillColor = Color.LightCyan

                                ColIdx = 0

                                For Each Col As DataColumn In dtbreakdown.Columns
                                    Select Case Col.ColumnName
                                        Case "FTOrderNo", "FTSubOrderNo", "FTColorway", "Total"

                                        Case Else
                                            ColIdx = ColIdx + 1

                                            .Rows(4).Item(2 + ColIdx).Value = "'" & Col.ColumnName
                                            .Rows(4).Item(2 + ColIdx).Font.Bold = True
                                            '.Rows(4).Item(2 + ColIdx).Alignment.Horizontal = True
                                            '.Rows(4).Item(2 + ColIdx).Alignment.Vertical = True
                                            .Rows(4).Item(2 + ColIdx).ColumnWidth = 100
                                            .Rows(4).Item(2 + ColIdx).FillColor = Color.LightCyan

                                            .Rows(4).Item(StartCol2 + ColIdx).Value = "'" & Col.ColumnName
                                            .Rows(4).Item(StartCol2 + ColIdx).Font.Bold = True
                                            '.Rows(4).Item(StartCol2 + ColIdx).Alignment.Horizontal = True
                                            '.Rows(4).Item(StartCol2 + ColIdx).Alignment.Vertical = True
                                            .Rows(4).Item(StartCol2 + ColIdx).ColumnWidth = 100
                                            .Rows(4).Item(StartCol2 + ColIdx).FillColor = Color.LightCyan



                                    End Select
                                Next

                                RowIdx = 0

                            For Each R As DataRow In dtsub.Select("FTSubOrderNo<>''", "FNMatColorSeq,FTSubOrderNo")

                                Dim PackQty As Integer = CInt(Val(R!FNPackQty.ToString))

                                .Rows(StartRow + RowIdx).Item(0).Value = "'" & R!FTStyleCode.ToString
                                .Rows(StartRow + RowIdx).Item(1).Value = "'" & R!FTSubOrderNo.ToString
                                .Rows(StartRow + RowIdx).Item(2).Value = "'" & R!FTColorway.ToString
                                .Rows((StartRow + RowIdx)).Item(2 + TotalSize + 2).Value = "'" & HI.UL.ULDate.ConvertEN(R!FTShipment.ToString)
                                .Rows((StartRow + RowIdx)).Item(2 + TotalSize + 3).Value = PackQty

                                ColIdx = 0

                                For Each Rx2 As DataRow In dtbreakdown.Select("FTSubOrderNo<>'' AND FTSubOrderNo='" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "'  AND FTColorway='" & HI.UL.ULF.rpQuoted(R!FTColorway.ToString) & "' ", "FTSubOrderNo")

                                    For Each Col As DataColumn In dtbreakdown.Columns
                                        Select Case Col.ColumnName
                                            Case "FTOrderNo", "FTSubOrderNo", "FTColorway"
                                            Case "Total"

                                                Dim Qty As Integer = CInt(Val(Rx2.Item(Col).ToString))

                                                .Rows(StartRow + RowIdx).Item(2 + TotalSize + 1).Value = Qty
                                                '.Rows(StartRow + RowIdx).Item(2 + TotalSize + 1).Alignment.Horizontal = True
                                                '.Rows(StartRow + RowIdx).Item(2 + TotalSize + 1).Alignment.Vertical = True
                                                .Rows(StartRow + RowIdx).Item(2 + TotalSize + 1).FillColor = Color.LightCyan
                                                .Rows(StartRow + RowIdx).Item(2 + TotalSize + 1).NumberFormat = "#,#"

                                            Case Else

                                                ColIdx = ColIdx + 1

                                                Dim Qty As Integer = CInt(Val(Rx2.Item(Col).ToString))

                                                If CInt(Val(Rx2.Item(Col).ToString)) > 0 Then

                                                    .Rows(StartRow + RowIdx).Item(2 + ColIdx).Value = CInt(Val(Rx2.Item(Col).ToString))
                                                    '.Rows(StartRow + RowIdx).Item(2 + ColIdx).Alignment.Horizontal = True
                                                    '.Rows(StartRow + RowIdx).Item(2 + ColIdx).Alignment.Vertical = True
                                                    .Rows(StartRow + RowIdx).Item(2 + ColIdx).NumberFormat = "#,#"

                                                    Dim StrPack As String = ""

                                                    If PackQty > 0 Then
                                                        StrPack = (Qty \ PackQty).ToString() & "/" & (Qty Mod PackQty).ToString()
                                                    End If

                                                    .Rows(StartRow + RowIdx).Item(StartCol2 + ColIdx).Value = "'" & StrPack
                                                    '.Rows(StartRow + RowIdx).Item(StartCol2 + ColIdx).Alignment.Horizontal = True
                                                    '.Rows(StartRow + RowIdx).Item(StartCol2 + ColIdx).Alignment.Vertical = True

                                                End If

                                        End Select

                                    Next

                                Next

                                RowIdx = RowIdx + 1

                            Next

                            .Rows(StartRow + RowIdx).Item(0).Value = "Total"

                            Dim rangeFormatting As DevExpress.Spreadsheet.Formatting = .Range("A" & (StartRow + RowIdx + 1).ToString & ":C" & (StartRow + RowIdx + 1).ToString).BeginUpdateFormatting
                            rangeFormatting.Fill.BackgroundColor = Color.LightCyan
                            rangeFormatting.Font.Bold = True

                            .Range("A" & (StartRow + RowIdx + 1).ToString & ":C" & (StartRow + RowIdx + 1).ToString).EndUpdateFormatting(rangeFormatting)
                            .MergeCells(.Range("A" & (StartRow + RowIdx + 1).ToString & ":C" & (StartRow + RowIdx + 1).ToString))

                            Dim SumQty As Integer = 0
                            Dim GSumQty As Integer = 0

                            For I As Integer = 0 To TotalSize - 1
                                SumQty = 0

                                For Each Rx2 As DataRow In dtbreakdown.Select("FTSubOrderNo<>''", "FTSubOrderNo")
                                    SumQty = SumQty + Val(Rx2.Item(3 + I).ToString)
                                Next

                                .Rows(StartRow + RowIdx).Item(2 + I + 1).Value = SumQty
                                .Rows(StartRow + RowIdx).Item(2 + I + 1).Font.Bold = True
                                '.Rows(StartRow + RowIdx).Item(2 + I + 1).Alignment.Horizontal = True
                                '.Rows(StartRow + RowIdx).Item(2 + I + 1).Alignment.Vertical = True
                                .Rows(StartRow + RowIdx).Item(2 + I + 1).FillColor = Color.LightCyan
                                .Rows(StartRow + RowIdx).Item(2 + I + 1).NumberFormat = "#,#"

                                GSumQty = GSumQty + SumQty
                            Next

                            .Rows(StartRow + RowIdx).Item(2 + TotalSize + 1).Value = GSumQty
                            .Rows(StartRow + RowIdx).Item(2 + TotalSize + 1).Font.Bold = True
                            '.Rows(StartRow + RowIdx).Item(2 + I + 1).Alignment.Horizontal = True
                            '.Rows(StartRow + RowIdx).Item(2 + I + 1).Alignment.Vertical = True
                            .Rows(StartRow + RowIdx).Item(2 + TotalSize + 1).FillColor = Color.LightCyan
                            .Rows(StartRow + RowIdx).Item(2 + TotalSize + 1).NumberFormat = "#,#"

                            RowIdx = RowIdx + 1
                            For Each R As DataRow In dtsub.Select("FTSubOrderNo=''", "FNMatColorSeq,FTSubOrderNo")

                                .Rows(StartRow + RowIdx).Item(0).Value = "'" & R!FTStyleCode.ToString
                                .Rows(StartRow + RowIdx).Item(1).Value = "'" & R!FTOrderNo.ToString
                                .Rows((StartRow + RowIdx)).Item(2 + TotalSize + 1).Value = Val(R!FNPOQty.ToString)
                                .Rows((StartRow + RowIdx)).Item(2 + TotalSize + 1).NumberFormat = "#,#"

                                RowIdx = RowIdx + 1

                            Next

                            .Columns(0).Width = 300
                                .Columns(1).Width = 400
                                .Columns(2).Width = 300

                                .Columns(2 + TotalSize + 1).Width = 350
                                .Columns(2 + TotalSize + 2).Width = 350
                                .Columns(2 + TotalSize + 3).Width = 250
                                .Columns(StartCol2 + TotalSize + 1).Width = 1000

                                ColIdx = 0

                                For Each Col As DataColumn In dtbreakdown.Columns
                                    Select Case Col.ColumnName
                                        Case "FTOrderNo", "FTSubOrderNo", "FTColorway", "Total"

                                        Case Else
                                            ColIdx = ColIdx + 1

                                            .Columns(2 + ColIdx).Width = 200

                                            .Columns(StartCol2 + ColIdx).Width = 200
                                    End Select
                                Next

                            Dim DataCell As DevExpress.Spreadsheet.CellRange = .Range("A" & (StartRow).ToString & ":" & GetColumnName(StartCol2) & (StartRow + RowIdx).ToString)
                            Dim rangeFormatting2 As DevExpress.Spreadsheet.Formatting = DataCell.BeginUpdateFormatting
                            Dim range4Borders As Borders = rangeFormatting2.Borders
                            ' range4Borders.SetOutsideBorders(Color.Black, BorderLineStyle.Thin)
                            range4Borders.SetAllBorders(Color.Black, BorderLineStyle.Thin)
                            'range4Borders.LeftBorder.Color = Color.Black
                            'range4Borders.TopBorder.Color = Color.Black
                            'range4Borders.RightBorder.Color = Color.Black
                            'range4Borders.BottomBorder.Color = Color.Black
                            'range4Borders.DiagonalBorderType = DiagonalBorderType.UpAndDown
                            'range4Borders.DiagonalBorderLineStyle = BorderLineStyle.MediumDashed
                            'range4Borders.DiagonalBorderColor = Color.Black
                            DataCell.EndUpdateFormatting(rangeFormatting2)

                            DataCell = .Range("" & GetColumnName(StartCol2 + 2) & (StartRow).ToString & ":" & GetColumnName(StartCol2 + ColIdx + 2) & (StartRow + RowIdx).ToString)
                            rangeFormatting2 = DataCell.BeginUpdateFormatting
                            range4Borders = rangeFormatting2.Borders
                            range4Borders.SetAllBorders(Color.Black, BorderLineStyle.Thin)
                            DataCell.EndUpdateFormatting(rangeFormatting2)

                        End With

                            Dim FileName As String = _DefailtPath & "\" & purchaseno & _ProdCode & ".xls"
                            ogcSpreadsheet.SaveDocument(FileName)

                            '                        // Format an individual cell.
                            'worksheet.Cells["A1"].Font.Color = Color.Red;
                            'worksheet.Cells["A1"].FillColor = Color.Yellow;
                            '                        // Format a range of cells.
                            'CellRange Range = worksheet.Range["C3:D4"];
                            'Formatting rangeFormatting = Range.BeginUpdateFormatting();
                            'rangeFormatting.Font.Color = Color.Blue;
                            'rangeFormatting.Fill.BackgroundColor = Color.LightBlue;
                            'rangeFormatting.Fill.PatternType = PatternType.LightHorizontal;
                            'rangeFormatting.Fill.PatternColor = Color.Violet;
                            'Range.EndUpdateFormatting(rangeFormatting);

                            _State = True
                        End If

                    End If

                ' Next


            Next



            dt.Dispose()
            _Spls.Close()

        Catch ex As Exception
            _Spls.Close()
        End Try

        Return _State

    End Function

    Private Function GetColumnName(ColIdx As Integer) As String
        Dim d As Integer
        Dim m As Integer
        Dim name As String
        d = ColIdx
        name = ""
        Do While (d > 0)
            m = (d - 1) Mod 26
            name = Chr(65 + m) + name
            d = Int((d - m) / 26)
        Loop
        Return name
    End Function
    Private Function LoadPurchaseNo(PoKey As String) As Boolean

        Dim _UserName As String = HI.ST.UserInfo.UserName
        Dim CusItemCodeRef As String = ""
        Dim PurchaseNo As String = ""
        Dim DeliveryDate As String = ""


        Dim _Qry As String = ""
        Dim _dt As System.Data.DataTable
        Dim _dtmain As System.Data.DataTable

        If PoKey = "" Then
            _Qry = "  Select TOP 0 "
        Else
            _Qry = "  Select TOP 1 "
        End If

        _Qry &= vbCrLf & "	  PH.FTPurchaseNo "
        _Qry &= vbCrLf & "		, Case When ISDATE(PH.FDDeliveryDate) = 1 Then Convert(nvarchar(10), Convert(Datetime,FDDeliveryDate)  ,103) Else '' END AS  FDDeliveryDate,PH.FTPurchaseBy "
        _Qry &= vbCrLf & "  From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase As PH With(NOLOCK)  "
        _Qry &= vbCrLf & "		  INNER Join [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier As SS With(NOLOCK) On PH.FNHSysSuplId = SS.FNHSysSuplId  "
        _Qry &= vbCrLf & " WHERE PH.FTPurchaseNo='" & HI.UL.ULF.rpQuoted(PoKey) & "' "
        _Qry &= vbCrLf & " GROUP BY PH.FTPurchaseNo, PH.FDDeliveryDate,PH.FTPurchaseBy  "
        _Qry &= vbCrLf & " ORDER BY PH.FTPurchaseNo "

        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PUR)


        If PoKey <> "" Then
            If _dt.Rows.Count <= 0 Then

                Return False
            End If
        End If

        If PoKey <> "" Then

            _dtmain = CType(ogcpurchase.DataSource, DataTable).Copy

            If _dtmain.Select("FTPurchaseNo='" & HI.UL.ULF.rpQuoted(PoKey) & "'").Length <= 0 Then

                For Each Rx As DataRow In _dt.Rows
                    _dtmain.Rows.Add(PoKey, Rx!FDDeliveryDate.ToString, Rx!FTPurchaseBy.ToString)
                Next

            End If

        Else
            _dtmain = _dt.Copy
        End If


        ogcpurchase.DataSource = _dtmain.Copy()

        _dtmain.Dispose()

        Return True
    End Function

    Private Sub FNHSysBuyId_EditValueChanged(sender As Object, e As EventArgs)

        ogcpurchase.DataSource = Nothing
    End Sub

    Private Sub FNHSysSuplId_EditValueChanged(sender As Object, e As EventArgs)


    End Sub


    Private Sub FTPurchaseNo_KeyDown(sender As Object, e As KeyEventArgs) Handles FTPurchaseNo.KeyDown
        Select Case e.KeyCode
            Case Keys.Enter
                If FTPurchaseNo.Text <> "" Then
                    If LoadPurchaseNo(FTPurchaseNo.Text) Then
                        FTPurchaseNo.Text = ""
                    Else

                        HI.MG.ShowMsg.mInfo("ไม่พบข้อมูลหมายเลขใบสั่งซื้อนี้ !!!", 171542181, Me.Text,, MessageBoxIcon.Warning)

                    End If

                    FTPurchaseNo.Focus()
                    FTPurchaseNo.SelectAll()
                End If
        End Select
    End Sub

    Private Sub ocmclear_Click(sender As Object, e As EventArgs) Handles ocmclear.Click
        Me.ogcpurchase.DataSource = Nothing
        LoadPurchaseNo("")
        ClearTab()
        FTPurchaseNo.Text = ""
    End Sub

    Private Sub FTPurchaseNo_EditValueChanged(sender As Object, e As EventArgs) Handles FTPurchaseNo.EditValueChanged

    End Sub

    Private Sub ogvpurchase_KeyDown(sender As Object, e As KeyEventArgs) Handles ogvpurchase.KeyDown
        Try
            Select Case e.KeyCode

                Case System.Windows.Forms.Keys.Delete


                    With Me.ogvpurchase
                        If .FocusedRowHandle < 0 Then Exit Sub
                        .DeleteRow(.FocusedRowHandle)

                    End With

                    With CType(Me.ogcpurchase.DataSource, DataTable)

                        .AcceptChanges()

                    End With

                Case System.Windows.Forms.Keys.Enter

            End Select
        Catch ex As Exception

        End Try

    End Sub
End Class
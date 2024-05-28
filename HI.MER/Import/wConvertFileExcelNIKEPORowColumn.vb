Imports System.IO
Imports DevExpress.Pdf
Imports DevExpress.XtraBars
Imports DevExpress.XtraEditors
Imports System.Text
Imports System.Windows.Forms
Imports System
Imports System.Collections.Generic
Imports System.Diagnostics
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraGrid.Views.Grid
Imports System.Drawing
Imports DevExpress.Spreadsheet
Imports DevExpress.Spreadsheet.Export

Public Class wConvertFileExcelNIKEPORowColumn


    Private PathFileExcel As String = ""
    Private tSql As String = ""

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub




    Private Sub SetGridColumn()

        Try
            With ogvdt1
                .BeginInit()

                Try
                    For I As Integer = .Columns.Count - 1 To 0 Step -1
                        Select Case Microsoft.VisualBasic.Left(.Columns(I).Name, 4).ToString.ToUpper
                            Case "CFIX"
                            Case Else

                                Dim FName As String = .Columns(I).FieldName

                                .Columns.Remove(.Columns(I))
                        End Select

                    Next
                Catch ex As Exception

                End Try

                .EndInit()
            End With
        Catch ex As Exception

        End Try



    End Sub
    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ocmclear_Click(sender As Object, e As EventArgs) Handles ocmclear.Click
        HI.TL.HandlerControl.ClearControl(Me)
        ogc.DataSource = Nothing

        'Try
        '    CType(ogc.DataSource, DataTable).Rows.Clear()
        'Catch ex As Exception

        'End Try


        Me.ogcdt1.DataSource = Nothing
        Me.ogvdt1.Columns.Clear()

    End Sub


    Private Sub Form_Load(sender As Object, e As EventArgs) Handles Me.Load

        Try

            ogv.OptionsView.ShowAutoFilterRow = False

            HI.UL.AppRegistry.LoadLayoutGridFromRegistry(Me, Me.ogvdt1)

        Catch ex As Exception
        End Try
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

    Private Sub exporter_CellValueConversionError(ByVal sender As Object, ByVal e As CellValueConversionErrorEventArgs)
        MessageBox.Show("Error In cell " & e.Cell.GetReferenceA1())
        e.DataTableValue = Nothing
        e.Action = DataTableExporterAction.Continue
    End Sub

    Private Sub FTFilePath_EditValueChanged(sender As Object, e As EventArgs)

    End Sub


    Private Function W_PRCbAddNewStyleImport() As Boolean
        Dim _bRet As Boolean = False

        Return _bRet

    End Function

    Private Sub LoadFilePath()
        Dim dt As New DataTable

        dt.Columns.Add("FTFileName", GetType(String))
        dt.Columns.Add("FTStateComplete", GetType(String))
        dt.Columns.Add("FTErrorMessage", GetType(String))


        Me.ogc.DataSource = dt.Copy

        dt.Dispose()

    End Sub

    Private Sub ocmselectfileexcel_Click(sender As Object, e As EventArgs) Handles ocmselectfileexcel.Click
        If Me.ogc.DataSource Is Nothing Then
            Call LoadFilePath()
        End If

        Try
            Dim opFileDialog As New System.Windows.Forms.OpenFileDialog
            opFileDialog.Filter = "Excel Worksheets (*.xls, *.xlsx, *.xlsm, *.xlsb)|*.xls;*.xlsx;*.xlsm;*.xlsb"
            opFileDialog.Multiselect = True



            Try
                If opFileDialog.ShowDialog() = DialogResult.OK Then
                    With CType(Me.ogc.DataSource, DataTable)
                        .AcceptChanges()

                        For Each mFile As String In opFileDialog.FileNames

                            If .Select("FTFileName='" & HI.UL.ULF.rpQuoted(mFile) & "'").Length <= 0 Then
                                .Rows.Add(mFile, "0", "")
                            End If

                        Next

                        .AcceptChanges()
                    End With


                End If
            Catch ex As Exception

            End Try
        Catch ex As Exception

        End Try

    End Sub


    Private Function ReadFileExcel(FileIndex As Integer, StateLast As Integer, _FileName As String, Spls As HI.TL.SplashScreen) As String
        Try




            Spls.UpdateInformation("Reading Data From Excel....")

            Select Case Path.GetExtension(_FileName)
                Case ".xls"
                    opshet.LoadDocument(File.ReadAllBytes(_FileName), DevExpress.Spreadsheet.DocumentFormat.Xls)

                Case ".xlsx"
                    opshet.LoadDocument(File.ReadAllBytes(_FileName), DevExpress.Spreadsheet.DocumentFormat.Xlsx)

                Case ".xlsm"
                    opshet.LoadDocument(File.ReadAllBytes(_FileName), DevExpress.Spreadsheet.DocumentFormat.Xlsm)
                Case ".xlsb"
                    opshet.LoadDocument(File.ReadAllBytes(_FileName), DevExpress.Spreadsheet.DocumentFormat.Xlsb)

                Case Else
                    opshet.LoadDocument(File.ReadAllBytes(_FileName), DevExpress.Spreadsheet.DocumentFormat.Xlsx)

            End Select



            'For CIdx As Integer = 0 To opshet.ActiveWorksheet.GetUsedRange().ColumnCount - 1
            '    Try
            '        'If opshet.ActiveWorksheet.Columns(CIdx).Value.Type = DevExpress.Spreadsheet.CellValueType.DateTime Then
            '        'Else
            '        '    If opshet.ActiveWorksheet.Columns(CIdx).Value.Type = DevExpress.Spreadsheet.CellValueType.Numeric Then
            '        '        opshet.ActiveWorksheet.Columns(CIdx).NumberFormat = "@"
            '        '    End If
            '        'End If



            '        If opshet.ActiveWorksheet.Cells(1, CIdx).Value.Type = DevExpress.Spreadsheet.CellValueType.DateTime Then

            '        Else

            '            If opshet.ActiveWorksheet.Cells(1, CIdx).Value.Type = DevExpress.Spreadsheet.CellValueType.Numeric AndAlso opshet.ActiveWorksheet.Cells(0, CIdx).Value.ToString.ToUpper <> "UPC".ToUpper Then
            '                opshet.ActiveWorksheet.Columns(CIdx).NumberFormat = "@"

            '            Else

            '                Select Case opshet.ActiveWorksheet.Cells(0, CIdx).Value.ToString.ToUpper
            '                    Case "Purchase Order Number".ToUpper, "Trading Co PO Number".ToUpper, "PO Line Item Number".ToUpper
            '                        opshet.ActiveWorksheet.Columns(CIdx).NumberFormat = "@"
            '                    Case "UPC".ToUpper

            '                        If opshet.ActiveWorksheet.Cells(1, CIdx).Value.Type = DevExpress.Spreadsheet.CellValueType.None Then
            '                            opshet.ActiveWorksheet.Columns(CIdx).NumberFormat = "0"
            '                        End If


            '                End Select

            '            End If

            '        End If

            '    Catch ex As Exception

            '    End Try


            'Next


            opshet.BeginUpdate()
            For CIdx As Integer = opshet.ActiveWorksheet.GetUsedRange().ColumnCount - 1 To 0 Step -1 ' opshet.ActiveWorksheet.GetUsedRange().ColumnCount - 1
                Try

                    Select Case opshet.ActiveWorksheet.Cells(0, CIdx).Value.ToString.ToUpper
                        Case "Purchase Order Number".ToUpper, "Trading Co PO Number".ToUpper, "PO Line Item Number".ToUpper, "Ship To Customer Number".ToUpper, "UPC".ToUpper
                            opshet.ActiveWorksheet.Columns(CIdx).NumberFormat = "@"
                        Case "UPC".ToUpper

                            If opshet.ActiveWorksheet.Cells(1, CIdx).Value.Type = DevExpress.Spreadsheet.CellValueType.None Then
                                opshet.ActiveWorksheet.Columns(CIdx).NumberFormat = "0"
                            End If

                        Case Else
                            If opshet.ActiveWorksheet.Cells(1, CIdx).Value.Type = DevExpress.Spreadsheet.CellValueType.DateTime Then

                            Else

                                If opshet.ActiveWorksheet.Cells(1, CIdx).Value.Type = DevExpress.Spreadsheet.CellValueType.Numeric AndAlso opshet.ActiveWorksheet.Cells(0, CIdx).Value.ToString.ToUpper <> "UPC".ToUpper Then

                                    opshet.ActiveWorksheet.Columns(CIdx).NumberFormat = "@"

                                Else

                                End If

                            End If
                    End Select
                Catch ex As Exception
                End Try

            Next

            opshet.EndUpdate()

            Dim msgshow As String = ""

            Try
                Dim mdt As DataTable
                Dim pworkbook As IWorkbook
                pworkbook = opshet.Document
                pworkbook.Worksheets(0).Name = "Sheet1"


                Dim FileName As String = PathFileExcel & "\" & HI.ST.UserInfo.UserName & ".xlsx"
                opshet.SaveDocument(PathFileExcel & "\" & HI.ST.UserInfo.UserName & ".xlsx")

                Dim strSheetName As String = opshet.ActiveSheet.Name.Trim

                Dim cmdstring As String = "  EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_TEMPDB) & "].dbo.USP_CONVERTFILEEXCEL_NIKEPO  '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(FileName) & "','" & HI.UL.ULF.rpQuoted(strSheetName) & "'," & FileIndex & "," & StateLast & ""

                ' StateImport = (HI.Conn.SQLConn.GetField(cmdstring, Conn.DB.DataBaseName.DB_FHS, "") = "1")
                Dim ds As New DataSet

                HI.Conn.SQLConn.GetDataSet(cmdstring, Conn.DB.DataBaseName.DB_TEMPDB, ds)


                Try
                    System.IO.File.Delete(FileName)
                Catch ex As Exception
                End Try

                Try
                    msgshow = ds.Tables(0).Rows(0).Item("FTMessage").ToString

                    If StateLast = 1 Then

                        If ds.Tables.Count = 2 Then

                            Dim dtExcel As DataTable = ds.Tables(1).Copy

                            dtExcel.BeginInit()

                            ' dtExcel.Columns.Remove("FTUserLogIn")
                            ' dtExcel.Columns.Remove("FNRowSeq")


                            Dim pRemoveCol As String = "VenderCode,ProductCode,PO_Number,TradePO_Number,PO_Item,Plant,TradePlant,OGacDate,GacDate,ShiptoAcc"


                            For Each Str As String In pRemoveCol.Split(",")
                                dtExcel.Columns.Remove(Str)

                                If dtExcel.Columns.IndexOf(Str & "1") > 0 Then
                                    dtExcel.Columns.Remove(Str & "1")
                                End If

                                If dtExcel.Columns.IndexOf(Str & "2") > 0 Then
                                    dtExcel.Columns.Remove(Str & "2")
                                End If

                                If dtExcel.Columns.IndexOf(Str & "3") > 0 Then
                                    dtExcel.Columns.Remove(Str & "3")
                                End If

                                If dtExcel.Columns.IndexOf(Str & "4") > 0 Then
                                    dtExcel.Columns.Remove(Str & "4")
                                End If

                                If dtExcel.Columns.IndexOf(Str & "5") > 0 Then
                                    dtExcel.Columns.Remove(Str & "5")
                                End If

                                If dtExcel.Columns.IndexOf(Str & "6") > 0 Then
                                    dtExcel.Columns.Remove(Str & "6")
                                End If


                                If dtExcel.Columns.IndexOf(Str & "7") > 0 Then
                                    dtExcel.Columns.Remove(Str & "7")
                                End If


                                If dtExcel.Columns.IndexOf(Str & "8") > 0 Then
                                    dtExcel.Columns.Remove(Str & "8")
                                End If


                                If dtExcel.Columns.IndexOf(Str & "9") > 0 Then
                                    dtExcel.Columns.Remove(Str & "9")
                                End If


                                If dtExcel.Columns.IndexOf(Str & "10") > 0 Then
                                    dtExcel.Columns.Remove(Str & "10")
                                End If

                            Next

                            dtExcel.EndInit()

                            Dim ColInteger As Integer = 1

                            With Me.ogvdt1
                                .BeginInit()


                                For Each Col As DataColumn In dtExcel.Columns

                                    Select Case Col.ColumnName
                                        Case "FTUserLogIn", "FNRowSeq", "OGAC", "GAC", "Purchase Order Number", "Trading Co PO Number", "Style Number", "Product Code", "PO Line Item Number", "Vendor Code", "Mode Of Transportation",
                                                     "Planning Season Code", "Planning Season Year", "Plant Code", "Plant Name", "Ship To Customer Number", "Total Item Quantity", "Gender Age", "Gender Age Description", "Schedule Line Item Number",
                                                     "Vendor Name", "DPOM Line Item Status", "Document Date", "Doc Type", "Doc Type Description", "Ship To Customer Name", "PMO/DEC code", "PMO/DEC Name", "Customer PO",
                                                      "Destination Country Code", "Destination Country Name"
                                        Case Else



                                            Dim ColG As New DevExpress.XtraGrid.Columns.GridColumn
                                            With ColG

                                                .FieldName = Col.ColumnName
                                                .Name = "gView1r" & ColInteger.ToString
                                                .Caption = Col.ColumnName
                                                .Visible = True

                                                .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                                                .OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains



                                                With .OptionsColumn
                                                    .AllowMove = False
                                                    .AllowShowHide = False
                                                    .AllowGroup = DevExpress.Utils.DefaultBoolean.False
                                                    .AllowSort = DevExpress.Utils.DefaultBoolean.False
                                                    .AllowMerge = DevExpress.Utils.DefaultBoolean.False
                                                    .AllowEdit = False
                                                    .ReadOnly = False
                                                End With

                                                With .OptionsFilter
                                                    .AutoFilterCondition = AutoFilterCondition.Contains
                                                    .FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList
                                                End With

                                                .Width = 100

                                            End With

                                            .Columns.Add(ColG)


                                            ColInteger = ColInteger + 1

                                    End Select

                                Next

                                .EndInit()
                            End With


                            ' Call SetGridColumn()
                            Me.ogcdt1.DataSource = dtExcel.Copy
                            dtExcel.Dispose()


                            For Each gCol As DevExpress.XtraGrid.Columns.GridColumn In ogvdt1.Columns
                                gCol.OptionsColumn.AllowEdit = False


                                If gCol.FieldName = "FNRowSeq" Then
                                    gCol.Visible = False
                                End If
                            Next


                        Else


                        End If


                    End If

                Catch ex As Exception

                    msgshow = "ข้อมูล File Excel ไม่ถูกต้องกรุณาทำการตรวจสอบ !!!"
                End Try

                ds.Dispose()

                Return msgshow

            Catch ex2 As Exception
            End Try




        Catch ex As Exception
        End Try
    End Function

    Private Sub ocmReadExcel_Click(sender As Object, e As EventArgs) Handles ocmReadExcel.Click

        If ogc.DataSource Is Nothing Then

            Exit Sub
        End If

        Dim TotalRowCount As Integer = 0
        With CType(ogc.DataSource, DataTable)
            .AcceptChanges()
            If .Rows.Count <= 0 Then
                Exit Sub
            End If

            For Each R As DataRow In .Rows
                R!FTStateComplete = ""
                R!FTErrorMessage = ""
            Next

            .AcceptChanges()

            TotalRowCount = .Rows.Count
        End With


        PathFileExcel = HI.Conn.SQLConn.GetField("Select TOP 1  FTCfgData FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSESystemConfig As X With(NOLOCK) WHERE X.FTCfgName='ImportBomExcel'", Conn.DB.DataBaseName.DB_SYSTEM)

        If CheckWriteFile() = False Then
            HI.MG.ShowMsg.mInfo("ไม่สามารถเข้าถึงที่เก็บ File สำหรับ Import ได้ !!!!", 1909270019, Me.Text)
        End If


        SetGridColumn()

        Dim Spls As New HI.TL.SplashScreen("Reading Data Excel File....")
        Dim RIndx As Integer = 0
        Dim MsgError As String = ""
        Dim FileState As Integer = 0
        With CType(ogc.DataSource, DataTable)
            .AcceptChanges()


            For Each R As DataRow In .Rows

                RIndx = RIndx + 1

                If RIndx = TotalRowCount Then
                    FileState = 1
                End If
                MsgError = ReadFileExcel(RIndx, FileState, R!FTFileName.ToString, Spls)

                If MsgError = "" Then
                    R!FTStateComplete = "1"
                    R!FTErrorMessage = ""
                Else
                    R!FTStateComplete = "0"
                    R!FTErrorMessage = MsgError
                End If

            Next

            .AcceptChanges()

        End With

        Spls.Close()


    End Sub

    Private Sub ogv_KeyDown(sender As Object, e As KeyEventArgs) Handles ogv.KeyDown
        Try
            Select Case e.KeyCode
                Case Keys.Delete

                    With ogv
                        .DeleteRow(.FocusedRowHandle)
                    End With

                    With CType(ogc.DataSource, DataTable)
                        .AcceptChanges()
                    End With
            End Select

        Catch ex As Exception

        End Try
    End Sub

    Private Sub ocmsavelayout_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub ocmsavelayout_Click_1(sender As Object, e As EventArgs) Handles ocmsavelayout.Click
        HI.UL.AppRegistry.SaveLayoutGridToRegistry(Me, Me.ogvdt1)

        HI.MG.ShowMsg.mInfo("Save Layout Grid Complete...", 1404240001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Information)
    End Sub
End Class
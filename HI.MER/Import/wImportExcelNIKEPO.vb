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

Public Class wImportExcelNIKEPO


    Private PathFileExcel As String = ""
    Private tSql As String = ""
    Private MappIngSize As wImportExcelNIKEPOMappingSize

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.



        Dim oSysLang As New HI.ST.SysLanguage



        MappIngSize = New wImportExcelNIKEPOMappingSize
        HI.TL.HandlerControl.AddHandlerObj(MappIngSize)


        Try
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleID, MappIngSize.Name.ToString.Trim, MappIngSize)
        Catch ex As Exception
        End Try


    End Sub


    Private Sub FTFilePath_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles FTFilePath.ButtonClick
        Select Case e.Button.Index
            Case 0
                Try
                    Dim opFileDialog As New System.Windows.Forms.OpenFileDialog
                    opFileDialog.Filter = "Excel Worksheets (*.xls, *.xlsx, *.xlsm, *.xlsb)|*.xls;*.xlsx;*.xlsm;*.xlsb"
                    opFileDialog.ShowDialog()

                    Try

                        If opFileDialog.FileName <> "" Then

                            ogcdt1.DataSource = Nothing
                            SetGridColumn()


                            Dim _FileName As String = opFileDialog.FileName
                            Me.FTFilePath.Text = _FileName

                            Dim Spls As New HI.TL.SplashScreen("Loading...")

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

                            'Dim usedRange As DevExpress.Spreadsheet.CellRange = opshet.ActiveWorksheet.GetUsedRange()
                            'usedRange.NumberFormat = "@"

                            Dim cmdsrtring As String = ""


                            cmdsrtring = " Select  TableName, ColumnName, FTStateNotPivot "
                            cmdsrtring &= vbCrLf & "  From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysTableTempColumn WITH(NOLOCK)"
                            cmdsrtring &= vbCrLf & "  Where (TableName = N'TTMPCheckExcelImportNIKEPO') "

                            Dim dt As DataTable = HI.Conn.SQLConn.GetDataTable(cmdsrtring, Conn.DB.DataBaseName.DB_SYSTEM)

                            opshet.BeginUpdate()
                            For CIdx As Integer = opshet.ActiveWorksheet.GetUsedRange().ColumnCount - 1 To 0 Step -1 ' opshet.ActiveWorksheet.GetUsedRange().ColumnCount - 1
                                Try

                                    'If opshet.ActiveWorksheet.Columns(CIdx).Value.Type = DevExpress.Spreadsheet.CellValueType.DateTime Then
                                    'Else
                                    '    If opshet.ActiveWorksheet.Columns(CIdx).Value.Type = DevExpress.Spreadsheet.CellValueType.Numeric Then
                                    '        opshet.ActiveWorksheet.Columns(CIdx).NumberFormat = "@"
                                    '    End If
                                    'End If

                                    If CIdx = 129 Then
                                        Beep()
                                    End If

                                    If FNFileFormatType.SelectedIndex = 0 Then


                                        If dt.Select("ColumnName='" & HI.UL.ULF.rpQuoted(opshet.ActiveWorksheet.Cells(0, CIdx).Value.ToString) & "'").Length <= 0 Then

                                            opshet.ActiveWorksheet.Columns(CIdx).Delete()

                                        Else

                                            If opshet.ActiveWorksheet.Cells(1, CIdx).Value.Type = DevExpress.Spreadsheet.CellValueType.DateTime Then

                                            Else

                                                If opshet.ActiveWorksheet.Cells(1, CIdx).Value.Type = DevExpress.Spreadsheet.CellValueType.Numeric AndAlso opshet.ActiveWorksheet.Cells(0, CIdx).Value.ToString.ToUpper <> "UPC".ToUpper Then

                                                    opshet.ActiveWorksheet.Columns(CIdx).NumberFormat = "@"

                                                Else

                                                    Select Case opshet.ActiveWorksheet.Cells(0, CIdx).Value.ToString.ToUpper
                                                        Case "Purchase Order Number".ToUpper, "Trading Co PO Number".ToUpper, "PO Line Item Number".ToUpper, "Ship To Customer Number"
                                                            opshet.ActiveWorksheet.Columns(CIdx).NumberFormat = "@"
                                                        Case "UPC".ToUpper

                                                            If opshet.ActiveWorksheet.Cells(1, CIdx).Value.Type = DevExpress.Spreadsheet.CellValueType.None Then
                                                                opshet.ActiveWorksheet.Columns(CIdx).NumberFormat = "0"
                                                            End If

                                                    End Select

                                                End If

                                            End If

                                        End If


                                    Else

                                        If opshet.ActiveWorksheet.Cells(1, CIdx).Value.Type = DevExpress.Spreadsheet.CellValueType.DateTime Then

                                        Else

                                            If opshet.ActiveWorksheet.Cells(1, CIdx).Value.Type = DevExpress.Spreadsheet.CellValueType.Numeric AndAlso opshet.ActiveWorksheet.Cells(0, CIdx).Value.ToString.ToUpper <> "Order Lines/Variant EAN".ToUpper Then
                                                opshet.ActiveWorksheet.Columns(CIdx).NumberFormat = "@"

                                            Else

                                                Select Case opshet.ActiveWorksheet.Cells(0, CIdx).Value.ToString.ToUpper

                                                    Case "Order Reference".ToUpper, "Trading Co PO Number".ToUpper, "PO Line Item Number".ToUpper, "Ship To Customer Number"
                                                        opshet.ActiveWorksheet.Columns(CIdx).NumberFormat = "@"
                                                    Case "Order Lines/Variant EAN".ToUpper

                                                        If opshet.ActiveWorksheet.Cells(1, CIdx).Value.Type = DevExpress.Spreadsheet.CellValueType.None Then
                                                            opshet.ActiveWorksheet.Columns(CIdx).NumberFormat = "0"
                                                        End If

                                                End Select

                                            End If

                                        End If
                                    End If

                                Catch ex As Exception
                                End Try

                            Next

                            opshet.EndUpdate()

                            cmdsrtring = "SELECT TOP 1  FTCfgData FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSESystemConfig AS X WITH(NOLOCK) WHERE X.FTCfgName='ImportBomExcel'"
                            PathFileExcel = HI.Conn.SQLConn.GetField(cmdsrtring, Conn.DB.DataBaseName.DB_SYSTEM)

                            If CheckWriteFile() = False Then

                                                HI.MG.ShowMsg.mInfo("ไม่สามารถเข้าถึงที่เก็บ File สำหรับ Import ได้ !!!!", 1909270019, Me.Text)

                                            End If

                                            Spls.UpdateInformation("Reading Data From Excel....")

                                            Dim msgshow As String = ""

                                            Try

                                                Dim mdt As DataTable
                                                Dim pworkbook As IWorkbook
                                                pworkbook = opshet.Document
                                                pworkbook.Worksheets(0).Name = "Sheet1"

                                                Dim FileName As String = PathFileExcel & "\" & HI.ST.UserInfo.UserName & ".xlsx"
                                                opshet.SaveDocument(PathFileExcel & "\" & HI.ST.UserInfo.UserName & ".xlsx")

                                                Dim strSheetName As String = opshet.ActiveSheet.Name.Trim

                                                Dim cmdstring As String = ""

                                                If FNFileFormatType.SelectedIndex = 0 Then
                                                    cmdstring = "  EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_TEMPDB) & "].dbo.USP_IMPORTFILEEXCEL_NIKEPO  '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(FileName) & "','" & HI.UL.ULF.rpQuoted(strSheetName) & "'"

                                                Else
                                                    cmdstring = "  EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_TEMPDB) & "].dbo.USP_IMPORTFILEEXCEL_ZELUSPO  '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(FileName) & "','" & HI.UL.ULF.rpQuoted(strSheetName) & "'"
                                                End If

                                                ' StateImport = (HI.Conn.SQLConn.GetField(cmdstring, Conn.DB.DataBaseName.DB_FHS, "") = "1")
                                                Dim ds As New DataSet

                                                HI.Conn.SQLConn.GetDataSet(cmdstring, Conn.DB.DataBaseName.DB_TEMPDB, ds)


                                                Try
                                                    System.IO.File.Delete(FileName)
                                                Catch ex As Exception
                                                End Try

                                                Try
                                                    msgshow = ds.Tables(0).Rows(0).Item("FTMessage").ToString

                                                    If ds.Tables.Count = 2 Then

                                                        If ds.Tables(0).Rows(0).Item("FTStetInsert").ToString = "1" Then

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
                                                                        Case "FTUserLogIn", "FNRowSeq", "FTOrderNo", "FTSubOrderNo", "Purchase Order Number", "Trading Co PO Number", "PO Line Item Number", "Vendor Code", "Vendor Name", "PMO/DEC code",
                                                                  "PMO/DEC Name", "DPOM Line Item Status", "Doc Type", "Doc Type Description", "Document Date", "Change Date", "Planning Priority Number", "Planning Priority Description", "OGAC", "GAC",
                                                                  "GAC Reason Code", "GAC Reason Description", "Previous GAC", "Previous GAC Reason Code", "Previous GAC Reason Description", "Initial GAC", "Initial GAC Reason Code",
                                                                  "Initial GAC Reason Description", "OGAC vs MRGAC", "GAC vs MRGAC", "GAC/OGAC Difference", "MRGAC Cpty Cnsmptn Wk", "GAC Slippage", "Global Planning Product Classification",
                                                                  "Global Planning Product Classification Description", "Global Planning Product Group", "Global Planning Product Group Description", "Order Reason Code", "Order Reason Description",
                                                                  "Planning Season Code", "Product Code", "Mode of Transportation", "Plant Code", "Plant Name", "Trading Company Plant Code", "Trading Company Plant Name", "Shipping Type",
                                                                  "Ship To Customer Number", "Ship To Customer Name", "Customer PO", "Direct Ship Sales Order Item", "Direct Ship Sales Order Number", "Distribution Channel", "Distribution Channel Description",
                                                                  "Total Item Quantity", "Launch Code", "Category", "Category Description", "Sub Category", "Sub Category Description", "Athlete ID", "Athlete Name", "VAS - Size", "Item Vas Text", "Sales Organization Code",
                                                                  "Color Description", "Gender Age", "Gender Age Description", "Global Category Core Focus", "Global Category Core Focus Description", "Global Category Summary",
                                                                  "Global Category Summary Description", "Launch Date", "League ID", "League ID Description", "Marketing Type Identifier", "Marketing Type Description", "Midsole Code", "Outsole Code",
                                                                  "Product Refill Class", "Product Refill Class Description", "Silhouette", "Silhouette Description", "Silo", "Silo Description", "Sport Activity", "Style Number", "Team ID", "Team Name", "Team Silhouette",
                                                                  "Team Silhouette Description", "Size Mismatch Indicator", "Destination Country Code", "Destination Country Name", "Purchase Org", "Purchase Org Name", "PO Rejection Code",
                                                                  "PO Rejection Code Description", "Inventory Segment Code", "Company Code", "Company Code Description", "Created By", "Division", "In Co Terms", "Item Text", "MCO Code", "MCO Name",
                                                                  "Product Name", "Purchase Group", "Prchase Group Name", "Trading Company Code", "TTMI", "TTMI Description", "Unit of Measure", "Planning Season Year", "MRGAC", "Create Date", "Cmp", "Sub PGM"
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
                                                    Else


                                                    End If

                                                Catch ex As Exception

                                                    msgshow = "ข้อมูล File Excel ไม่ถูกต้องกรุณาทำการตรวจสอบ !!!"
                                                End Try

                                                ds.Dispose()

                                            Catch ex2 As Exception
                                            End Try

                                            Spls.Close()

                                            If msgshow <> "" Then
                                                MessageBox.Show(msgshow)
                                            End If

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
        Me.ogcdt1.DataSource = Nothing
        Me.ogvdt1.Columns.Clear()

        FNHSysMerTeamId.Text = "MP"
    End Sub


    Private Sub Form_Load(sender As Object, e As EventArgs) Handles Me.Load

        Try

            FNHSysMerTeamId.Text = "MP"

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

    'Private Sub ocmimportbimpdf_Click(sender As Object, e As EventArgs) Handles ocmimportbimpdf.Click

    '    Dim StateImport As Boolean = False
    '    Dim msgshow As String = ""
    '    Dim cmdstring As String = ""


    '    If FTFilePath.Text <> "" Then



    '        PathFileExcel = HI.Conn.SQLConn.GetField("SELECT TOP 1  FTCfgData FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSESystemConfig AS X WITH(NOLOCK) WHERE X.FTCfgName='ImportBomExcel'", Conn.DB.DataBaseName.DB_SYSTEM)

    '        If CheckWriteFile() = False Then
    '            HI.MG.ShowMsg.mInfo("ไม่สามารถเข้าถึงที่เก็บ File สำหรับ Import ได้ !!!!", 1909270019, Me.Text)
    '        End If


    '        Dim Splsx As New HI.TL.SplashScreen("Reading Data From Excel....")
    '        Try
    '            Dim mdt As DataTable

    '            ' Try

    '            '    Dim worksheet As DevExpress.Spreadsheet.Worksheet = opshet.Document.Worksheets.ActiveWorksheet
    '            '    Dim range As DevExpress.Spreadsheet.Range = worksheet.GetUsedRange
    '            '    Dim rangeHasHeaders As Boolean = True

    '            '    ' Create a data table with column names obtained from the first row in a range if it has headers.
    '            '    ' Column data types are obtained from cell value types of cells in the first data row of the worksheet range.
    '            '    Dim dataTable As DataTable = worksheet.CreateDataTable(range, rangeHasHeaders)

    '            '    'Validate cell value types. If cell value types in a column are different, the column values are exported as text.
    '            '    For col As Integer = 0 To range.ColumnCount - 1
    '            '        Dim cellType As CellValueType = range(0, col).Value.Type
    '            '        For r As Integer = 1 To range.RowCount - 1
    '            '            If cellType <> range(r, col).Value.Type Then
    '            '                dataTable.Columns(col).DataType = GetType(String)
    '            '                Exit For
    '            '            End If
    '            '        Next r
    '            '    Next col

    '            '    ' Create the exporter that obtains data from the specified range, 
    '            '    ' skips the header row (if required) and populates the previously created data table. 
    '            '    Dim exporter As DataTableExporter = worksheet.CreateDataTableExporter(range, dataTable, rangeHasHeaders)
    '            '    ' Handle value conversion errors.
    '            '    AddHandler exporter.CellValueConversionError, AddressOf exporter_CellValueConversionError

    '            '    ' Perform the export.

    '            '    exporter.Export()

    '            '    mdt = dataTable.Copy

    '            'Catch ex As Exception
    '            '    mdt = Nothing
    '            'End Try

    '            'If mdt Is Nothing Then
    '            '    Splsx.Close()
    '            '    msgshow = "ข้อมูล File Excel ไม่ถูกต้องกรุณาทำการตรวจสอบ !!!"
    '            'Else


    '            'If mdt.Rows.Count > 0 And mdt.Columns.Count = 51 Then
    '            '        StateImport = ImportDataToTemp(mdt, Splsx)


    '            Dim FileName As String = PathFileExcel & "\" & HI.ST.UserInfo.UserName & ".xlsx"
    '            opshet.SaveDocument(PathFileExcel & "\" & HI.ST.UserInfo.UserName & ".xlsx")

    '            Dim strSheetName As String = opshet.ActiveSheet.Name.Trim

    '            cmdstring = "  EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.USP_IMPORTFILEEXCEL_NIKEPO  '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(FileName) & "','" & HI.UL.ULF.rpQuoted(strSheetName) & "'"

    '            ' StateImport = (HI.Conn.SQLConn.GetField(cmdstring, Conn.DB.DataBaseName.DB_FHS, "") = "1")

    '            Dim dtimport As DataTable = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_FHS)


    '            Try
    '                If HI.ST.SysInfo.Admin Then
    '                Else

    '                    System.IO.File.Delete(FileName)
    '                End If

    '            Catch ex As Exception
    '            End Try

    '            Splsx.Close()
    '            'Else
    '            '    Splsx.Close()
    '            '    msgshow = "ข้อมูล File Excel ไม่ถูกต้องกรุณาทำการตรวจสอบ !!!"
    '            'End If

    '            ' End If

    '        Catch ex2 As Exception
    '            Splsx.Close()
    '        End Try

    '        If StateImport = False Then
    '            MessageBox.Show(msgshow)
    '        End If

    '    Else
    '            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, ogbselectfile.Text)
    '    End If

    'End Sub

    Private Sub exporter_CellValueConversionError(ByVal sender As Object, ByVal e As CellValueConversionErrorEventArgs)
        MessageBox.Show("Error In cell " & e.Cell.GetReferenceA1())
        e.DataTableValue = Nothing
        e.Action = DataTableExporterAction.Continue
    End Sub

    Private Sub FTFilePath_EditValueChanged(sender As Object, e As EventArgs) Handles FTFilePath.EditValueChanged

    End Sub


    Private Function W_PRCbAddNewStyleImport() As Boolean
        Dim _bRet As Boolean = False
        'Try
        '    _bRet = True
        '    If Not oDBdtStyle Is Nothing And oDBdtStyle.Rows.Count > 0 Then
        '        Dim nFNHSysStyleId As Integer
        '        Dim nFNHSysCustId As Integer
        '        Dim tStyleImport As String
        '        Dim tStyleImportDesc As String

        '        nFNHSysCustId = Val(Me.FNHSysCustId.Properties.Tag)

        '        If nFNHSysCustId = 0 And Me.FNHSysCustId.Text.Trim() <> "" Then

        '            tSql = "SELECT TOP 1 A.FNHSysCustId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCNMCustomer] AS A WITH(NOLOCK) WHERE A.FTCustCode = N'" & HI.UL.ULF.rpQuoted(Me.FNHSysCustId.Text.Trim()) & "';"
        '            nFNHSysCustId = Val(HI.Conn.SQLConn.GetField(tSql, HI.Conn.DB.DataBaseName.DB_MASTER, "0"))

        '        End If

        '        Dim oStrBuilder As New System.Text.StringBuilder()

        '        Dim nLoopStyle As Integer

        '        For nLoopStyle = 0 To oDBdtStyle.Rows.Count - 1
        '            oStrBuilder.Remove(0, oStrBuilder.Length)

        '            tStyleImport = ""
        '            tStyleImport = oDBdtStyle.Rows(nLoopStyle).Item("FTStyleCode")

        '            tStyleImportDesc = ""
        '            tStyleImportDesc = oDBdtStyle.Rows(nLoopStyle).Item("FTStyleName")

        '            tSql = "SELECT TOP 1 A.FTStyleCode FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMStyle] AS A WITH(NOLOCK) WHERE A.FTStyleCode = N'" & HI.UL.ULF.rpQuoted(tStyleImport) & "'"

        '            If HI.Conn.SQLConn.GetField(tSql, HI.Conn.DB.DataBaseName.DB_MASTER, "") = "" Then
        '                nFNHSysStyleId = Val(HI.TL.RunID.GetRunNoID("TMERMStyle", "FNHSysStyleId", Conn.DB.DataBaseName.DB_MASTER).ToString())

        '                oStrBuilder.AppendLine("INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMStyle]([FTInsUser]")
        '                oStrBuilder.AppendLine("											  ,[FDInsDate]")
        '                oStrBuilder.AppendLine("											  ,[FTInsTime]")
        '                oStrBuilder.AppendLine("											  ,[FTUpdUser]")
        '                oStrBuilder.AppendLine("											  ,[FDUpdDate]")
        '                oStrBuilder.AppendLine("											  ,[FTUpdTime]")
        '                oStrBuilder.AppendLine("											  ,[FNHSysStyleId]")
        '                oStrBuilder.AppendLine("											  ,[FTStyleCode]")
        '                oStrBuilder.AppendLine("											  ,[FTStyleNameTH]")
        '                oStrBuilder.AppendLine("											  ,[FTStyleNameEN]")
        '                oStrBuilder.AppendLine("											  ,[FTRemark]")
        '                oStrBuilder.AppendLine("											  ,[FTStateActive],[FNCMDisPer]")
        '                oStrBuilder.AppendLine("											  ,[FNHSysCustId])")
        '                oStrBuilder.AppendLine("VALUES(NULL")
        '                oStrBuilder.AppendLine("      ,CONVERT(VARCHAR(10),GETDATE(),111)")
        '                oStrBuilder.AppendLine("      ,CONVERT(VARCHAR(8),GETDATE(),114)")
        '                oStrBuilder.AppendLine("      ,NULL")
        '                oStrBuilder.AppendLine("      ,NULL")
        '                oStrBuilder.AppendLine("      ,NULL")
        '                oStrBuilder.AppendLine(String.Format("      ,{0}", nFNHSysStyleId))
        '                oStrBuilder.AppendLine("      ,N'" & HI.UL.ULF.rpQuoted(tStyleImport) & "'")
        '                oStrBuilder.AppendLine("      ,N'" & HI.UL.ULF.rpQuoted(tStyleImportDesc) & "'")
        '                oStrBuilder.AppendLine("      ,N'" & HI.UL.ULF.rpQuoted(tStyleImportDesc) & "'")
        '                oStrBuilder.AppendLine("      ,''")
        '                oStrBuilder.AppendLine("      ,'1',5.0")
        '                REM 2014/05/24 oStrBuilder.AppendLine("      ,COALESCE((SELECT TOP 1 A.FNHSysCustId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCNMCustomer] AS A WITH(NOLOCK) WHERE A.FTCustCode = N'" & HI.UL.ULF.rpQuoted(tCustCodeStyle) & "'), NULL))")
        '                oStrBuilder.AppendLine(String.Format("       ,{0})", nFNHSysCustId))

        '                tSql = ""
        '                tSql = oStrBuilder.ToString()

        '                If HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MASTER) Then
        '                    'MsgBox("Execute data complete...", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
        '                End If

        '            End If

        '        Next nLoopStyle

        '    Else
        '        '...do nothing
        '    End If

        'Catch ex As Exception
        '    If System.Diagnostics.Debugger.IsAttached = True Then
        '        Throw New Exception(ex.Message().ToString() & ex.StackTrace.ToString())
        '    End If
        '    _bRet = False
        'End Try

        Return _bRet

    End Function


    Private Function W_PRCbValidateConfirmGenerateFactoryOrder() As Boolean
        Dim _bRet As Boolean = False
        Try
            If Me.FNHSysCustId.Text.Trim() <> "" Then

                If Me.FNHSysCmpRunId.Text.Trim() <> "" Then
                    If Me.FNHSysBuyId.Text.Trim() <> "" Then
                        If Me.FNHSysMerTeamId.Text.Trim() <> "" Then

                            If Me.FNHSysUnitId.Text.Trim() <> "" Then


                                _bRet = True

                            Else

                                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.FNHSysUnitId_lbl.Text)
                                Me.FNHSysUnitId.Focus()
                            End If
                        Else

                            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.FNHSysMerTeamId_lbl.Text)
                            Me.FNHSysMerTeamId.Focus()

                        End If




                    Else
                        HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.FNHSysBuyId_lbl.Text)
                        Me.FNHSysBuyId.Focus()
                    End If
                Else
                    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.FNHSysCmpRunId_lbl.Text)
                    Me.FNHSysCmpRunId.Focus()
                End If

            Else
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.FNHSysCustId_lbl.Text)
                Me.FNHSysCustId.Focus()
            End If
        Catch ex As Exception
            If System.Diagnostics.Debugger.IsAttached = True Then
                MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), My.Application.Info.Title)
            End If
        End Try

        Return _bRet

    End Function


    Private Sub CheckMasterFileData()

        Call W_PRCbValidateExistsMasterSeason()
        Call W_PRCbValidateExistsMasterStyle()
        Call W_PRCbValidateExistsMatchColor()
        Call W_PRCbValidateExistsMasterGender()
        Call W_PRCbValidateExistsMasterShipMode()

    End Sub


    Private Function W_PRCbValidateExistsMasterSeason() As Boolean
        Dim bRet As Boolean = False
        Try

            Dim oDBdt As New DataTable

            tSql = " SELECT A.FTSeasonCode "
            tSql &= vbCrLf & " FROM (Select        FTPlanningSeason + RIGHT(FTYear,2) As FTSeasonCode "
            tSql &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTImportOrderTemp As A With(NOLOCK) "
            tSql &= vbCrLf & "  WHERE A.FTUserLogin='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            tSql &= vbCrLf & " GROUP BY FTPlanningSeason + RIGHT(FTYear,2)  ) AS A  "
            tSql &= vbCrLf & " OUTER APPLY (Select TOP 1  FTSeasonCode FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMSeason As SS With (NOLOCK) WHERE SS.FTSeasonCode = A.FTSeasonCode) As SS"
            tSql &= vbCrLf & " WHERE ISNULL(SS.FTSeasonCode ,'') ='' "

            oDBdt = HI.Conn.SQLConn.GetDataTable(tSql, Conn.DB.DataBaseName.DB_MERCHAN)

            Dim tSeason As String
            For Each R As DataRow In oDBdt.Rows
                tSeason = R!FTSeasonCode.ToString

                Dim nFNHSysSeasonId As Integer

                nFNHSysSeasonId = Val(HI.TL.RunID.GetRunNoID("TMERMSeason", "FNHSysSeasonId", HI.Conn.DB.DataBaseName.DB_MASTER).ToString())
                Dim oStrBuilder As New System.Text.StringBuilder()

                oStrBuilder.Remove(0, oStrBuilder.Length)

                oStrBuilder.AppendLine("INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMSeason]([FTInsUser]")
                oStrBuilder.AppendLine("			,[FDInsDate]")
                oStrBuilder.AppendLine("			,[FTInsTime]")
                oStrBuilder.AppendLine("			,[FTUpdUser]")
                oStrBuilder.AppendLine("			,[FDUpdDate]")
                oStrBuilder.AppendLine("			,[FTUpdTime]")
                oStrBuilder.AppendLine("			,[FNHSysSeasonId]")
                oStrBuilder.AppendLine("			,[FTSeasonCode]")
                oStrBuilder.AppendLine("			,[FTSeasonNameTH]")
                oStrBuilder.AppendLine("			,[FTSeasonNameEN]")
                oStrBuilder.AppendLine("			,[FTRemark]")
                oStrBuilder.AppendLine("			,[FTStateActive])")
                oStrBuilder.AppendLine("VALUES(NULL")
                oStrBuilder.AppendLine("      ,NULL")
                oStrBuilder.AppendLine("      ,NULL")
                oStrBuilder.AppendLine("      ,NULL")
                oStrBuilder.AppendLine("      ,NULL")
                oStrBuilder.AppendLine("      ,NULL")
                oStrBuilder.AppendLine(String.Format("      ,{0}", nFNHSysSeasonId))
                oStrBuilder.AppendLine("      ,N'" & HI.UL.ULF.rpQuoted(tSeason) & "'")
                oStrBuilder.AppendLine("      ,N'" & HI.UL.ULF.rpQuoted(tSeason) & "'")
                oStrBuilder.AppendLine("      ,N'" & HI.UL.ULF.rpQuoted(tSeason) & "'")
                oStrBuilder.AppendLine("      ,''")
                oStrBuilder.AppendLine("      ,'1')")
                tSql = ""
                tSql = oStrBuilder.ToString()

                If HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MASTER) Then
                End If

            Next




        Catch ex As Exception
            'If System.Diagnostics.Debugger.IsAttached = True Then
            MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
            ' End If
        End Try

        Return bRet

    End Function

    Private Function W_PRCbValidateExistsMasterStyle() As Boolean
        Dim bRet As Boolean = False
        Try


            Dim oDBdt As New DataTable


            tSql = "  Select  A.FTStyleCode,A.Material_Description "
            tSql &= vbCrLf & " FROM ( Select   A.FTStyle AS FTStyleCode "
            tSql &= vbCrLf & ", MAX(A.FTStyleDesc) As Material_Description	"
            tSql &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTImportOrderTemp As A With(NOLOCK) "
            tSql &= vbCrLf & "  WHERE A.FTUserLogin='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            tSql &= vbCrLf & " Group By A.FTStyle  ) AS A "
            tSql &= vbCrLf & "OUTER APPLY (Select TOP 1  FTStyleCode FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle As SS With (NOLOCK) WHERE SS.FTStyleCode = A.FTStyleCode) As SS "
            tSql &= vbCrLf & " Where ISNULL(SS.FTStyleCode,'') ='' "

            oDBdt = HI.Conn.SQLConn.GetDataTable(tSql, Conn.DB.DataBaseName.DB_MERCHAN)


            Dim tStyleCode As String
            Dim tStyleDesc As String

            For Each R As DataRow In oDBdt.Rows

                tStyleCode = R!FTStyleCode.ToString
                tStyleDesc = R!Material_Description.ToString

                Dim nFNHSysId As Integer

                nFNHSysId = Val(HI.TL.RunID.GetRunNoID("TMERMStyle", "FNHSysStyleId", HI.Conn.DB.DataBaseName.DB_MASTER).ToString())
                Dim oStrBuilder As New System.Text.StringBuilder()

                oStrBuilder.Remove(0, oStrBuilder.Length)

                oStrBuilder.AppendLine("INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMStyle]([FTInsUser]")
                oStrBuilder.AppendLine("			,[FDInsDate]")
                oStrBuilder.AppendLine("			,[FTInsTime]")
                oStrBuilder.AppendLine("			,[FTUpdUser]")
                oStrBuilder.AppendLine("			,[FDUpdDate]")
                oStrBuilder.AppendLine("			,[FTUpdTime]")
                oStrBuilder.AppendLine("			,[FNHSysStyleId]")
                oStrBuilder.AppendLine("			,[FTStyleCode]")
                oStrBuilder.AppendLine("			,[FTStyleNameTH]")
                oStrBuilder.AppendLine("			,[FTStyleNameEN]")
                oStrBuilder.AppendLine("			,[FTRemark]")
                oStrBuilder.AppendLine("			,[FTStateActive])")
                oStrBuilder.AppendLine("VALUES(NULL")
                oStrBuilder.AppendLine("      ,NULL")
                oStrBuilder.AppendLine("      ,NULL")
                oStrBuilder.AppendLine("      ,NULL")
                oStrBuilder.AppendLine("      ,NULL")
                oStrBuilder.AppendLine("      ,NULL")
                oStrBuilder.AppendLine(String.Format("      ,{0}", nFNHSysId))
                oStrBuilder.AppendLine("      ,N'" & HI.UL.ULF.rpQuoted(tStyleCode) & "'")
                oStrBuilder.AppendLine("      ,N'" & HI.UL.ULF.rpQuoted(tStyleDesc) & "'")
                oStrBuilder.AppendLine("      ,N'" & HI.UL.ULF.rpQuoted(tStyleDesc) & "'")
                oStrBuilder.AppendLine("      ,''")
                oStrBuilder.AppendLine("      ,'1')")

                tSql = ""
                tSql = oStrBuilder.ToString()

                If HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MASTER) Then
                End If

            Next

        Catch ex As Exception
            'If System.Diagnostics.Debugger.IsAttached = True Then
            MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
            'End If
        End Try

        Return bRet

    End Function

    Private Function W_PRCbValidateExistsMatchColor() As Boolean
        Dim bRet As Boolean = False

        Try

            Dim cmdstring As String = ""
            cmdstring = " EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.USP_IMPORTFILEEXCELNIKEPO_COLORMAPPING '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            HI.Conn.SQLConn.ExecuteOnly(cmdstring, Conn.DB.DataBaseName.DB_MERCHAN)

        Catch ex As Exception
            'If System.Diagnostics.Debugger.IsAttached = True Then
            MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), My.Application.Info.Title)
            'End If
        End Try

        Return bRet

    End Function

    Private Function W_PRCbValidateExistsMasterGender() As Boolean
        Dim bRet As Boolean = False
        Try


            Dim oDBdt As New DataTable


            tSql = " SELECT A.FTGender "
            tSql &= vbCrLf & " FROM (Select   A.FTGenderNameDesc  AS FTGender "
            tSql &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTImportOrderTemp As A With(NOLOCK) "
            tSql &= vbCrLf & "  WHERE A.FTUserLogin='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' AND ISNULL(A.FNHSysGenderId,0) =0 AND ISNULL(A.FTGenderNameDesc,'') <>'' "
            tSql &= vbCrLf & " GROUP BY A.FTGenderNameDesc ) As A "
            tSql &= vbCrLf & " OUTER APPLY (SELECT TOP 1  FTGenderNameEN FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMGender AS SS WITH (NOLOCK) WHERE SS.FTGenderNameEN = A.FTGender) AS SS "
            tSql &= vbCrLf & " WHERE ISNULL(SS.FTGenderNameEN ,'') ='' "


            oDBdt = HI.Conn.SQLConn.GetDataTable(tSql, Conn.DB.DataBaseName.DB_MERCHAN)


            Dim tMatchGender As String

            For Each R As DataRow In oDBdt.Rows

                tMatchGender = R!FTGender.ToString


                Dim nFNHSysGenderId As Integer

                nFNHSysGenderId = Val(HI.TL.RunID.GetRunNoID("TMERMGender", "FNHSysGenderId", Conn.DB.DataBaseName.DB_MASTER).ToString())

                tSql = " Declare @RecCount int =0 "
                tSql &= " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMGender]([FTInsUser],[FDInsDate],[FTInsTime]"
                tSql &= Environment.NewLine & "                        ,[FTUpdUser],[FDUpdDate],[FTUpdTime]"
                tSql &= Environment.NewLine & "                        ,[FNHSysGenderId],[FTGenderCode],[FTGenderNameTH]"
                tSql &= Environment.NewLine & "                        ,[FTGenderNameEN],[FTRemark],[FTStateActive])"
                tSql &= Environment.NewLine & "VALUES(NULL, NULL, NULL"
                tSql &= "                            ,NULL, NULL , NULL"
                tSql &= "                            ," & nFNHSysGenderId & ", N'" & HI.UL.ULF.rpQuoted(tMatchGender) & "', N'" & HI.UL.ULF.rpQuoted(tMatchGender) & "'"
                tSql &= "                            ,N'" & HI.UL.ULF.rpQuoted(tMatchGender) & "', '', '1')"
                tSql &= "  SET   @RecCount  = @@ROWCOUNT  "
                tSql &= "  IF  @RecCount > 0 "
                tSql &= "   BEGIN "
                tSql &= "           UPDATE A  SET  A.FNHSysGenderId=" & nFNHSysGenderId & ""
                tSql &= "           FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTImportOrderTemp As A "
                tSql &= "            WHERE A.FTUserLogin='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' AND ISNULL(A.FNHSysGenderId,0) =0  AND ISNULL(A.FTGenderNameDesc,'') ='" & HI.UL.ULF.rpQuoted(tMatchGender) & "'"

                tSql &= "   END "


                If HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MASTER) = True Then



                    'MsgBox("Execuete data complete !!!", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
                End If

            Next


        Catch ex As Exception
            'If System.Diagnostics.Debugger.IsAttached = True Then
            MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
            ' End If
        End Try

        Return bRet

    End Function

    Private Function W_PRCbValidateExistsMasterShipMode() As Boolean
        Dim bRet As Boolean = False
        Try

            Dim oDBdt As New DataTable


            tSql = " SELECT A.FTMode "
            tSql &= vbCrLf & " FROM (SELECT   B.FTMode "

            tSql &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTImportOrderSizeBreakdownTemp As A With(NOLOCK) "
            tSql &= vbCrLf & "  WHERE A.FTUserLogin='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'  "

            tSql &= vbCrLf & " WHERE ISNULL(B.FTMode,'') <> '' "
            tSql &= vbCrLf & " GROUP BY B.FTMode ) AS A "
            tSql &= vbCrLf & " OUTER APPLY (SELECT TOP 1  FTShipModeCode FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMShipMode AS SS WITH (NOLOCK) WHERE SS.FTShipModeCode = A.FTMode) AS SS "
            tSql &= vbCrLf & " WHERE ISNULL(SS.FTShipModeCode ,'') ='' "


            oDBdt = HI.Conn.SQLConn.GetDataTable(tSql, Conn.DB.DataBaseName.DB_MERCHAN)


            Dim tShipMode As String
            Dim nFNHSysShipModeId As Integer


            For Each R As DataRow In oDBdt.Rows


                tShipMode = R!Mode_Code.ToString()



                nFNHSysShipModeId = Val(HI.TL.RunID.GetRunNoID("TCNMShipMode", "FNHSysShipModeId", Conn.DB.DataBaseName.DB_MASTER).ToString())

                Dim oStrBuilder As New System.Text.StringBuilder()

                oStrBuilder.Remove(0, oStrBuilder.Length)

                oStrBuilder.AppendLine("INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCNMShipMode]")
                oStrBuilder.AppendLine("           ([FTInsUser]")
                oStrBuilder.AppendLine("           ,[FDInsDate]")
                oStrBuilder.AppendLine("           ,[FTInsTime]")
                oStrBuilder.AppendLine("           ,[FTUpdUser]")
                oStrBuilder.AppendLine("           ,[FDUpdDate]")
                oStrBuilder.AppendLine("           ,[FTUpdTime]")
                oStrBuilder.AppendLine("           ,[FNHSysShipModeId]")
                oStrBuilder.AppendLine("           ,[FTShipModeCode]")
                oStrBuilder.AppendLine("           ,[FTShipModenNameTH]")
                oStrBuilder.AppendLine("           ,[FTShipModeNameEN]")
                oStrBuilder.AppendLine("           ,[FTRemark]")
                oStrBuilder.AppendLine("           ,[FTStateActive])")
                oStrBuilder.AppendLine("VALUES (NULL")
                oStrBuilder.AppendLine("       ,NULL")
                oStrBuilder.AppendLine("       ,NULL")
                oStrBuilder.AppendLine("       ,NULL")
                oStrBuilder.AppendLine("       ,NULL")
                oStrBuilder.AppendLine("       ,NULL")
                oStrBuilder.AppendLine(String.Format("       ,{0}", nFNHSysShipModeId))
                oStrBuilder.AppendLine("       ,N'" & HI.UL.ULF.rpQuoted(tShipMode) & "'")
                oStrBuilder.AppendLine("       ,N'" & HI.UL.ULF.rpQuoted(tShipMode) & "'")
                oStrBuilder.AppendLine("       ,N'" & HI.UL.ULF.rpQuoted(tShipMode) & "'")
                oStrBuilder.AppendLine("       ,''")
                oStrBuilder.AppendLine("       ,'1')")

                tSql = ""
                tSql = oStrBuilder.ToString()

                If HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MASTER) = True Then
                    'MsgBox("Execute data complete ...", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
                End If

            Next


        Catch ex As Exception
            'If System.Diagnostics.Debugger.IsAttached = True Then
            MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
            'End If
        End Try

        Return bRet

    End Function

    Private Function MappingSuplierData(Optional NetPrice As Boolean = False) As Boolean

        Dim cmdstring As String = ""
        Dim dt As New DataTable
        Dim dtunit As New DataTable

        Dim pSate As Integer = 0

        If NetPrice Then
            pSate = 1
        End If

        Call CheckMasterFileData()

        cmdstring = " EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.USP_IMPORTFILEEXCEL_NIKEPO_SIZEMAPPING '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & pSate & ""
        dt = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_MERCHAN)

        If NetPrice = False Then
            If dt.Rows.Count > 0 Then
                With MappIngSize

                    .LoadWisdomSize()
                    .ogclist.DataSource = dt.Copy()
                    .ShowDialog()

                End With

                Return False
            Else
                Return True
            End If
        End If

    End Function

    Private Sub ocmImportOrder_Click(sender As Object, e As EventArgs) Handles ocmImportOrder.Click

        Try

            With CType(ogcdt1.DataSource, DataTable)
                .AcceptChanges()

                If .Rows.Count <= 0 Then
                    HI.MG.ShowMsg.mInfo("Not Found Data For Import !!!", 2205034784, Me.Text)
                    Exit Sub
                End If
            End With

            If MappingSuplierData() Then



                If W_PRCbValidateConfirmGenerateFactoryOrder() = True Then



                    tSql = ""
                    tSql = "select Top 1 '1' AS DataImport "
                    tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTImportOrderTemp] AS A"
                    tSql &= Environment.NewLine & "WHERE A.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "';"

                    If HI.Conn.SQLConn.GetField(tSql, Conn.DB.DataBaseName.DB_MERCHAN, "") = "" Then
                        HI.MG.ShowMsg.mInfo("Not Found Data For Import !!!", 2205034784, Me.Text)
                        Exit Sub
                    End If


                    If HI.MG.ShowMsg.mConfirmProcess("", 1404240002, "Confirm") = True Then

                        tSql = ""
                        tSql = "DELETE A"
                        tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTImportOrderUpdExtraQtyTemp] AS A"
                        tSql &= Environment.NewLine & "WHERE A.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "';"

                        If HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN) = True Then
                        End If

                        Dim _Spls As New HI.TL.SplashScreen("Generate Factory Order No .....Please Wait")

                        If csOrder.ImportFactoryOrder(_Spls, FNImportOrderType.SelectedIndex, Val(FNHSysCustId.Properties.Tag.ToString), Val(FNHSysCmpRunId.Properties.Tag.ToString), FNHSysCmpRunId.Text, Val(FNHSysBuyId.Properties.Tag.ToString), Val(FNHSysMerTeamId.Properties.Tag.ToString), FNHSysCustId.Text.Trim, FNHSysMerTeamId.Text.Trim, FNHSysBuyId.Text.Trim, True) = True Then

                            '...clear temp after process import order complete
                            '---------------------------------------------------------------------------------------------------------------------------------------------------------
                            '  If Not System.Diagnostics.Debugger.IsAttached = True Then
                            Application.DoEvents()

                            ' Call W_PRCbShowBrowseDataImportFactoryOrder()

                            _Spls.UpdateInformation("Finishing Generate Order .....Please Wait")

                            Try
                                tSql = ""

                                REM 2014/12/25 Copy Image from TMERMStyle {MASTER} by FNHSysStyleId
                                '============================================================================================================================================================================================
                                'Dim dttempdata As DataTable

                                'tSql = "SELECT  B.FTOrderNo,C.FPStyleImage1,C.FPStyleImage2, C.FPStyleImage3, C.FPStyleImage4"
                                'tSql &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTImportOrderTemp] AS A"
                                'tSql &= vbCrLf & "     INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrder] AS B WITH(NOLOCK) ON A.FTGenerateOrderNo = B.FTOrderNo"
                                'tSql &= vbCrLf & "     INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMStyle] AS C WITH(NOLOCK) ON B.FNHSysStyleId = C.FNHSysStyleId"
                                'tSql &= vbCrLf & "WHERE A.FTUserLogin = '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"

                                'dttempdata = HI.Conn.SQLConn.GetDataTable(tSql, Conn.DB.DataBaseName.DB_MERCHAN)

                                'For Each Rx As DataRow In dttempdata.Rows
                                '    tSql = ""
                                '    tSql = "UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrder]"
                                '    tSql &= vbCrLf & "SET FTImage1 = '" & HI.UL.ULImage.SaveImage(HI.UL.ULImage.ConvertByteArrayToImmage(Rx!FPStyleImage1), Rx!FTOrderNo.ToString & "_" & "FTImage1", "" & _SystemFilePath) & "'"
                                '    tSql &= vbCrLf & "   ,FTImage2 = '" & HI.UL.ULImage.SaveImage(HI.UL.ULImage.ConvertByteArrayToImmage(Rx!FPStyleImage2), Rx!FTOrderNo.ToString & "_" & "FTImage2", "" & _SystemFilePath) & "'"
                                '    tSql &= vbCrLf & "   ,FTImage3 = '" & HI.UL.ULImage.SaveImage(HI.UL.ULImage.ConvertByteArrayToImmage(Rx!FPStyleImage3), Rx!FTOrderNo.ToString & "_" & "FTImage3", "" & _SystemFilePath) & "'"
                                '    tSql &= vbCrLf & "   ,FTImage4 = '" & HI.UL.ULImage.SaveImage(HI.UL.ULImage.ConvertByteArrayToImmage(Rx!FPStyleImage4), Rx!FTOrderNo.ToString & "_" & "FTImage4", "" & _SystemFilePath) & "'"
                                '    tSql &= vbCrLf & "WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(Rx!FTOrderNo.ToString) & "'"
                                '    HI.Conn.SQLConn.ExecuteNonQuery(tSql, Conn.DB.DataBaseName.DB_MERCHAN)
                                'Next

                                'dttempdata.Dispose()
                                '============================================================================================================================================================================================

                                tSql = "UPDATE A"
                                tSql &= Environment.NewLine & "SET A.FPOrderImage1 = C.FPStyleImage1,"
                                tSql &= Environment.NewLine & "    A.FPOrderImage2 = C.FPStyleImage2,"
                                tSql &= Environment.NewLine & "    A.FPOrderImage3 = C.FPStyleImage3,"
                                tSql &= Environment.NewLine & "    A.FPOrderImage4 = C.FPStyleImage4"
                                tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrder] AS A LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTImportOrderTemp] AS B ON A.FTOrderNo = B.FTGenerateOrderNo"
                                tSql &= Environment.NewLine & "                                        LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMStyle] AS C ON A.FNHSysStyleId = C.FNHSysStyleId"
                                tSql &= Environment.NewLine & "WHERE B.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "';"

                                If HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN) = True Then
                                Else
                                End If

                            Catch ex As Exception
                            End Try

                            tSql = " select   FNRowImport, FTPONo, FTPOTrading, FTPOItem, FTGenerateOrderNo, FTGenerateOrderSubNo   "
                            tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTImportOrderTemp] AS A"
                            tSql &= Environment.NewLine & "WHERE A.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "';"


                            Dim dt As DataTable = HI.Conn.SQLConn.GetDataTable(tSql, Conn.DB.DataBaseName.DB_MERCHAN)


                            With CType(Me.ogcdt1.DataSource, DataTable)
                                .AcceptChanges()

                                For Each Rx As DataRow In dt.Rows

                                    For Each R As DataRow In .Select("FNRowSeq=" & Val(Rx!FNRowImport) & "")
                                        R!FTOrderNo = Rx!FTGenerateOrderNo.ToString
                                        R!FTSubOrderNo = Rx!FTGenerateOrderSubNo.ToString
                                    Next

                                Next


                                .AcceptChanges()
                            End With


                            tSql = ""
                            tSql = "DELETE A"
                            tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTImportOrderUpdExtraQtyTemp] AS A"
                            tSql &= Environment.NewLine & "WHERE A.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "';"

                            If HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN) = True Then
                            End If

                            tSql = ""
                            tSql = "DELETE A"
                            tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTImportOrderSizeBreakdownTemp] AS A"
                            tSql &= Environment.NewLine & "WHERE A.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "';"

                            If HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN) = True Then
                            End If

                            tSql = ""
                            tSql = "DELETE A"
                            tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTImportOrderTemp] AS A"
                            tSql &= Environment.NewLine & "WHERE A.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "';"

                            If HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN) = True Then
                            End If

                            'Else
                            '    '...developer test temp data
                            'End If
                            '---------------------------------------------------------------------------------------------------------------------------------------------------------

                            'Me.ogcdt1.DataSource = Nothing

                            _Spls.Close()
                            Select Case HI.ST.Lang.Language

                                Case HI.ST.Lang.eLang.TH
                                    HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, "นำเข้าข้อมูลรายการใบสั่งผลิต เรียบร้อยแล้ว...")

                                Case Else
                                    HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, "Import Factory Order No. Complete...")
                            End Select

                        Else

                            ' If Not System.Diagnostics.Debugger.IsAttached = True Then
                            tSql = ""
                            tSql = "DELETE A"
                            tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTImportOrderUpdExtraQtyTemp] AS A"
                            tSql &= Environment.NewLine & "WHERE A.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "';"

                            If HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN) = True Then
                            End If

                            tSql = ""
                            tSql = "DELETE A"
                            tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTImportOrderSizeBreakdownTemp] AS A"
                            tSql &= Environment.NewLine & "WHERE A.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "';"

                            If HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN) = True Then
                            End If

                            tSql = ""
                            tSql = "DELETE A"
                            tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTImportOrderTemp] AS A"
                            tSql &= Environment.NewLine & "WHERE A.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "';"

                            If HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN) = True Then
                            End If

                            ' Else
                            '...developer test temp data
                            ' End If
                            _Spls.Close()

                            MsgBox("พบปัญหาในการบันทึกรายการนำเข้าข้อมูลใบสั่งผลิต !!!", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, My.Application.Info.Title)

                        End If

                    Else
                        '...do nothing
                    End If

                Else
                    '...do nothing
                End If

            Else
                MsgBox("Cannot Import to Factory Order No. , Please validate source file for import data !!!", vbOKOnly + MsgBoxStyle.Information, "Information")
            End If

        Catch ex As Exception
            ' If System.Diagnostics.Debugger.IsAttached = True Then
            MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.OkOnly, My.Application.Info.Title)
            ' End If
            tSql = ""
            tSql = "DELETE A FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTImportOrderTemp AS A WHERE A.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "';"
            tSql &= Environment.NewLine & "DELETE A FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTImportOrderSizeBreakdownTemp AS A WHERE A.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "';"
            tSql &= Environment.NewLine & "DELETE A FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTImportOrderUpdExtraQtyTemp AS A WHERE A.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "';"

            If HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN) = True Then
                If System.Diagnostics.Debugger.IsAttached = True Then
                    MsgBox("Clear Transaction temporary import factory order complete...", MsgBoxStyle.Information + MsgBoxStyle.Information, My.Application.Info.Title)
                End If
            End If

        End Try

    End Sub

    Private Sub ocmsavelayout_Click(sender As Object, e As EventArgs) Handles ocmsavelayout.Click
        HI.UL.AppRegistry.SaveLayoutGridToRegistry(Me, Me.ogvdt1)

        HI.MG.ShowMsg.mInfo("Save Layout Grid Complete...", 1404240001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Information)
    End Sub

    Private Sub ocmImportnetprice_Click(sender As Object, e As EventArgs) Handles ocmImportnetprice.Click
        Try

            With CType(ogcdt1.DataSource, DataTable)
                .AcceptChanges()

                If .Rows.Count <= 0 Then
                    HI.MG.ShowMsg.mInfo("Not Found Data For Import !!!", 2205034784, Me.Text)
                    Exit Sub
                End If
            End With

            If MappingSuplierData() Then





                tSql = ""
                    tSql = "select Top 1 '1' AS DataImport "
                    tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTImportOrderTemp] AS A"
                    tSql &= Environment.NewLine & "WHERE A.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "';"

                    If HI.Conn.SQLConn.GetField(tSql, Conn.DB.DataBaseName.DB_MERCHAN, "") = "" Then
                        HI.MG.ShowMsg.mInfo("Not Found Data For Import !!!", 2205034784, Me.Text)
                        Exit Sub
                    End If

                If HI.MG.ShowMsg.mConfirmProcess("You would to Import Net Price !!!", 1404244582, "Confirm") = True Then

                    Dim _Spls As New HI.TL.SplashScreen("Importing Net Price Data .....Please Wait")

                    If csOrder.ImportFactoryOrderNetPrice(_Spls, FNImportOrderType.SelectedIndex, Val(FNHSysCustId.Properties.Tag.ToString), Val(FNHSysCmpRunId.Properties.Tag.ToString), FNHSysCmpRunId.Text, Val(FNHSysBuyId.Properties.Tag.ToString), Val(FNHSysMerTeamId.Properties.Tag.ToString), True) = True Then

                        '...clear temp after process import order complete
                        '---------------------------------------------------------------------------------------------------------------------------------------------------------
                        '  If Not System.Diagnostics.Debugger.IsAttached = True Then
                        Application.DoEvents()

                        ' Call W_PRCbShowBrowseDataImportFactoryOrder()

                        _Spls.UpdateInformation("Finishing Import Net Price Data .....Please Wait")


                        tSql = ""
                        tSql = "DELETE A"
                        tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTImportOrderSizeBreakdownTemp] AS A"
                        tSql &= Environment.NewLine & "WHERE A.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "';"

                        If HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN) = True Then
                        End If

                        tSql = ""
                        tSql = "DELETE A"
                        tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTImportOrderTemp] AS A"
                        tSql &= Environment.NewLine & "WHERE A.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "';"

                        If HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN) = True Then
                        End If

                        'Else
                        '    '...developer test temp data
                        'End If
                        '---------------------------------------------------------------------------------------------------------------------------------------------------------

                        'Me.ogcdt1.DataSource = Nothing

                        _Spls.Close()
                        Select Case HI.ST.Lang.Language

                            Case HI.ST.Lang.eLang.TH
                                HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, "นำเข้าข้อมูลรายการใบสั่งผลิต เรียบร้อยแล้ว...")

                            Case Else
                                HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, "Import Factory Order No. Complete...")
                        End Select

                    Else



                        tSql = ""
                        tSql = "DELETE A"
                        tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTImportOrderSizeBreakdownTemp] AS A"
                        tSql &= Environment.NewLine & "WHERE A.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "';"

                        If HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN) = True Then
                        End If

                        tSql = ""
                        tSql = "DELETE A"
                        tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTImportOrderTemp] AS A"
                        tSql &= Environment.NewLine & "WHERE A.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "';"

                        If HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN) = True Then
                        End If

                        ' Else
                        '...developer test temp data
                        ' End If
                        _Spls.Close()

                        MsgBox("พบปัญหาในการบันทึกรายการนำเข้าข้อมูลใบสั่งผลิต !!!", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, My.Application.Info.Title)

                    End If

                Else
                    '...do nothing
                End If

                Else
                    '...do nothing
                End If



        Catch ex As Exception
            ' If System.Diagnostics.Debugger.IsAttached = True Then
            MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.OkOnly, My.Application.Info.Title)
            ' End If
            tSql = ""
            tSql = "DELETE A FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTImportOrderTemp AS A WHERE A.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "';"
            tSql &= Environment.NewLine & "DELETE A FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTImportOrderSizeBreakdownTemp AS A WHERE A.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "';"
            tSql &= Environment.NewLine & "DELETE A FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTImportOrderUpdExtraQtyTemp AS A WHERE A.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "';"

            If HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN) = True Then
                If System.Diagnostics.Debugger.IsAttached = True Then
                    MsgBox("Clear Transaction temporary import factory order complete...", MsgBoxStyle.Information + MsgBoxStyle.Information, My.Application.Info.Title)
                End If
            End If

        End Try
    End Sub

    Private Sub FNFileFormatType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles FNFileFormatType.SelectedIndexChanged
        Try


            ogcdt1.DataSource = Nothing
            SetGridColumn()
            FTFilePath.Text = ""

        Catch ex As Exception

        End Try
    End Sub
End Class
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

Public Class wCalculateExcelForecast

    Private PathFileExcel As String = ""
    Private CalculateFinish As wCalculateExcelForecastFinish

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.


        CalculateFinish = New wCalculateExcelForecastFinish
        HI.TL.HandlerControl.AddHandlerObj(CalculateFinish)

        Dim oSysLang As New HI.ST.SysLanguage

        Try
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleID, CalculateFinish.Name.ToString.Trim, CalculateFinish)
        Catch ex As Exception
        End Try


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

                            Select Case Path.GetExtension(_FileName)
                                Case ".xls"
                                    opshet.LoadDocument(File.ReadAllBytes(_FileName), DevExpress.Spreadsheet.DocumentFormat.Xls)

                                Case ".xlsx"
                                    opshet.LoadDocument(File.ReadAllBytes(_FileName), DevExpress.Spreadsheet.DocumentFormat.Xlsx)

                                Case ".xlsm"
                                    opshet.LoadDocument(File.ReadAllBytes(_FileName), DevExpress.Spreadsheet.DocumentFormat.Xlsm)

                                Case Else
                                    opshet.LoadDocument(File.ReadAllBytes(_FileName), DevExpress.Spreadsheet.DocumentFormat.Xlsx)

                            End Select

                            'Dim usedRange As DevExpress.Spreadsheet.CellRange = opshet.ActiveWorksheet.GetUsedRange()
                            'usedRange.NumberFormat = "@"

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
                            '            If opshet.ActiveWorksheet.Cells(1, CIdx).Value.Type = DevExpress.Spreadsheet.CellValueType.Numeric Then
                            '                opshet.ActiveWorksheet.Columns(CIdx).NumberFormat = "@"
                            '            End If
                            '        End If

                            '    Catch ex As Exception

                            '    End Try


                            'Next

                            Spls.Close()
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

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ocmclear_Click(sender As Object, e As EventArgs) Handles ocmclear.Click
        HI.TL.HandlerControl.ClearControl(Me)


    End Sub


    Private Sub Form_Load(sender As Object, e As EventArgs) Handles Me.Load

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

    Private Sub ocmimportbimpdf_Click(sender As Object, e As EventArgs) Handles ocmcalculateforecast.Click

        Dim StateImport As Boolean = False
        Dim msgshow As String = ""
        Dim cmdstring As String = ""


        If FTFilePath.Text <> "" Then



            PathFileExcel = HI.Conn.SQLConn.GetField("SELECT TOP 1  FTCfgData FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSESystemConfig AS X WITH(NOLOCK) WHERE X.FTCfgName='ImportBomExcel'", Conn.DB.DataBaseName.DB_SYSTEM)

            If CheckWriteFile() = False Then
                HI.MG.ShowMsg.mInfo("ไม่สามารถเข้าถึงที่เก็บ File สำหรับ Import ได้ !!!!", 1909270019, Me.Text)
            End If


            Dim Splsx As New HI.TL.SplashScreen("Reading Data From Excel....")

            Try
                Dim mdt As DataTable

                ' Try

                '    Dim worksheet As DevExpress.Spreadsheet.Worksheet = opshet.Document.Worksheets.ActiveWorksheet
                '    Dim range As DevExpress.Spreadsheet.Range = worksheet.GetUsedRange
                '    Dim rangeHasHeaders As Boolean = True

                '    ' Create a data table with column names obtained from the first row in a range if it has headers.
                '    ' Column data types are obtained from cell value types of cells in the first data row of the worksheet range.
                '    Dim dataTable As DataTable = worksheet.CreateDataTable(range, rangeHasHeaders)

                '    'Validate cell value types. If cell value types in a column are different, the column values are exported as text.
                '    For col As Integer = 0 To range.ColumnCount - 1
                '        Dim cellType As CellValueType = range(0, col).Value.Type
                '        For r As Integer = 1 To range.RowCount - 1
                '            If cellType <> range(r, col).Value.Type Then
                '                dataTable.Columns(col).DataType = GetType(String)
                '                Exit For
                '            End If
                '        Next r
                '    Next col

                '    ' Create the exporter that obtains data from the specified range, 
                '    ' skips the header row (if required) and populates the previously created data table. 
                '    Dim exporter As DataTableExporter = worksheet.CreateDataTableExporter(range, dataTable, rangeHasHeaders)
                '    ' Handle value conversion errors.
                '    AddHandler exporter.CellValueConversionError, AddressOf exporter_CellValueConversionError

                '    ' Perform the export.

                '    exporter.Export()

                '    mdt = dataTable.Copy

                'Catch ex As Exception
                '    mdt = Nothing
                'End Try

                'If mdt Is Nothing Then
                '    Splsx.Close()
                '    msgshow = "ข้อมูล File Excel ไม่ถูกต้องกรุณาทำการตรวจสอบ !!!"
                'Else


                'If mdt.Rows.Count > 0 And mdt.Columns.Count = 51 Then
                '        StateImport = ImportDataToTemp(mdt, Splsx)


                Dim FileName As String = PathFileExcel & "\" & HI.ST.UserInfo.UserName & ".xlsx"
                opshet.SaveDocument(PathFileExcel & "\" & HI.ST.UserInfo.UserName & ".xlsx")


                cmdstring = "  EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.USP_IMPORTFILEEXCEL_FORCAST  '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(FileName) & "'"

                Dim ds As New DataSet
                HI.Conn.SQLConn.GetDataSet(cmdstring, Conn.DB.DataBaseName.DB_PUR, ds)

                Try
                    System.IO.File.Delete(FileName)
                Catch ex As Exception
                End Try

                Try
                    msgshow = ds.Tables(0).Rows(0).Item("FTMessage").ToString

                    If ds.Tables.Count = 4 Then

                        If ds.Tables(0).Rows(0).Item("FTStetInsert").ToString = "1" Then
                            StateImport = True
                            Splsx.Close()

                            With CalculateFinish
                                .ogc1.DataSource = ds.Tables(1).Copy
                                .ogc3.DataSource = ds.Tables(2).Copy
                                .ogc2.DataSource = ds.Tables(3).Copy
                                .otb.SelectedTabPageIndex = 0

                                .ShowDialog()
                            End With


                        Else
                            Splsx.Close()
                            StateImport = False

                        End If
                    Else
                        Splsx.Close()
                        StateImport = False

                    End If



                Catch ex As Exception
                    Splsx.Close()
                    StateImport = False
                    msgshow = "ข้อมูล File Excel ไม่ถูกต้องกรุณาทำการตรวจสอบ !!!"
                End Try


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

        Dim Ridx As Integer = 0
        Dim TotalR As Integer = dt.Select("STATUS='P' OR STATUS='K'").Length


        For Each R As DataRow In dt.Select("STATUS='P' OR STATUS='K'")

            Try
                Ridx = Ridx + 1

                spls.UpdateInformation("Importing Data.... Row " & Ridx & " of  " & TotalR)

                cmdstring = " insert into [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FHS) & "].dbo.TTMPImportBomExcel ( "
                cmdstring &= vbCrLf & "   FTUserLogIn, Seq, BOM_ID, BOM_ITM_ID, BOM_ROW_NBR, MSC_CODE, MSC_LEVEL_1, MSC_LEVEL_2 "
                cmdstring &= vbCrLf & "  , MSC_LEVEL_3, SILHOUETTE, SEASON_CD, SEASON_YR, STYLE_NM, STYLE_NBR, STYLE_CW_CD, "
                cmdstring &= vbCrLf & "   PLUG_CW_CD, PRMRY, SCNDY, TRTRY, LOGO, ADDENDUM, FACTORY, [STATUS], COMPONENT_ORD "
                cmdstring &= vbCrLf & "  , [USE], [DESCRIPTION], ITEM_TYPE_1, ITEM_TYPE_2, ITEM_TYPE_3, ITEM_TYPE_4, [IS], IT, ITEM_NBR, "
                cmdstring &= vbCrLf & "   ITEM_COLOR_ORD, GCW, GCW_ORD, GCW_ART_DESCRIPTION, ITEM_COLOR_CD, ITEM_COLOR_NM, ITEM_COLOR_ABRV "
                cmdstring &= vbCrLf & "  , VEND_CD, VEND_LO, VEND_NM, QTY, UOM, DEVELOPER, BOM_UPDATE_DT, "
                cmdstring &= vbCrLf & "   BOM_ITM_UPDATE_DT, BOM_ITM_SETUP_DT, HK_BOM_UPDATE_DT, HK_BOM_ITM_UPDATE_DT, HK_BOM_ITM_SETPUP_DT, ReportSection "
                cmdstring &= vbCrLf & " ) "
                cmdstring &= vbCrLf & " select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & Ridx & ""
                cmdstring &= vbCrLf & "," & Val(R!BOM_ID.ToString) & ""
                cmdstring &= vbCrLf & "," & Val(R!BOM_ITM_ID.ToString) & ""
                cmdstring &= vbCrLf & "," & Val(R!BOM_ROW_NBR.ToString) & ""
                cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!MSC_CODE.ToString) & "'"
                cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!MSC_LEVEL_1.ToString) & "'"
                cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!MSC_LEVEL_2.ToString) & "' "
                cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!MSC_LEVEL_3.ToString) & "'"
                cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!SILHOUETTE.ToString) & "'"
                cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!SEASON_CD.ToString) & "'"
                cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!SEASON_YR.ToString) & "'"
                cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!STYLE_NM.ToString) & "'"
                cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!STYLE_NBR.ToString) & "'"
                cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!STYLE_CW_CD.ToString) & "'"
                cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!PLUG_CW_CD.ToString) & "'"
                cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!PRMRY.ToString) & "'"
                cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!SCNDY.ToString) & "'"
                cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!TRTRY.ToString) & "'"
                cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!LOGO.ToString) & "'"
                cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!ADDENDUM.ToString) & "'"
                cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FACTORY.ToString) & "'"
                cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!STATUS.ToString) & "'"

                If (R!QTY.ToString.Trim = "") Then
                    cmdstring &= vbCrLf & ",NULL"
                Else
                    cmdstring &= vbCrLf & "," & Val(R!COMPONENT_ORD.ToString) & ""
                End If

                cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!USE.ToString) & "'"
                cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!DESCRIPTION.ToString) & "'"
                cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!ITEM_TYPE_1.ToString) & "'"
                cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!ITEM_TYPE_2.ToString) & "'"
                cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!ITEM_TYPE_3.ToString) & "'"
                cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!ITEM_TYPE_4.ToString) & "'"
                cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!IS.ToString) & "'"
                cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!IT.ToString) & "'"
                cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!ITEM_NBR.ToString) & "'"

                If (R!QTY.ToString.Trim = "") Then
                    cmdstring &= vbCrLf & ",NULL"
                Else
                    cmdstring &= vbCrLf & "," & Val(R!ITEM_COLOR_ORD.ToString) & ""
                End If

                cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R.Item("GCW#").ToString) & "'"
                cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!GCW_ORD.ToString) & "'"
                cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!GCW_ART_DESCRIPTION.ToString) & "'"
                cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!ITEM_COLOR_CD.ToString) & "'"
                cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!ITEM_COLOR_NM.ToString) & "'"
                cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!ITEM_COLOR_ABRV.ToString) & "'"
                cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!VEND_CD.ToString) & "'"
                cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!VEND_LO.ToString) & "'"
                cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!VEND_NM.ToString) & "'"

                If (R!QTY.ToString.Trim = "") Then
                    cmdstring &= vbCrLf & ",NULL"
                Else
                    cmdstring &= vbCrLf & "," & Val(R!QTY.ToString) & ""
                End If

                cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!UOM.ToString) & "'"
                cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!DEVELOPER.ToString) & "'"
                cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!BOM_UPDATE_DT.ToString) & "'"
                cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!BOM_ITM_UPDATE_DT.ToString) & "'"
                cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!BOM_ITM_SETUP_DT.ToString) & "'"
                cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!HK_BOM_UPDATE_DT.ToString) & "'"
                cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!HK_BOM_ITM_UPDATE_DT.ToString) & "'"
                cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!HK_BOM_ITM_SETPUP_DT.ToString) & "'"
                cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!ReportSection.ToString) & "' "

                If HI.Conn.SQLConn.ExecuteNonQuery(cmdstring, Conn.DB.DataBaseName.DB_FHS) = False Then

                    cmdstring = "delete from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FHS) & "].dbo.TTMPImportBomExcel where FTUserLogIn='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    HI.Conn.SQLConn.ExecuteOnly(cmdstring, Conn.DB.DataBaseName.DB_FHS)

                    Return False
                End If


            Catch ex As Exception

                cmdstring = "delete from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FHS) & "].dbo.TTMPImportBomExcel where FTUserLogIn='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                HI.Conn.SQLConn.ExecuteOnly(cmdstring, Conn.DB.DataBaseName.DB_FHS)

                Return False
            End Try


        Next

        Dim StateCheckImport As Boolean = ImportData(spls)

        If StateCheckImport = False Then

            cmdstring = "delete from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FHS) & "].dbo.TTMPImportBomExcel where FTUserLogIn='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
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

        spls.UpdateInformation("Creating BOM Data...")

        Try

            Dim _dtcheckmat As DataTable
            Dim _dtstyle As DataTable
            Dim _dtstyledetail As DataTable

            cmdstring = "SEELCT  STYLE_NBR, STYLE_NM, SEASON_CD, SEASON_YR,SEASON_CD + RIGHT(SEASON_YR,2) AS FTSeason,MSC_CODE, MSC_LEVEL_1, MSC_LEVEL_2, MSC_LEVEL_3, SILHOUETTE,MAX(DEVELOPER) As DEVELOPER  "
            cmdstring &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FHS) & "].dbo.TTMPImportBomExcel WITH(NOLOCK) "
            cmdstring &= vbCrLf & "  where FTUserLogIn='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
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


                cmdstring = "Select TOP 1   FNHSysStyleDevId"
                cmdstring &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTDevelopStyle As A With(NOLOCK)"
                cmdstring &= vbCrLf & "  WHERE  (FTStyleDevCode = N'" & HI.UL.ULF.rpQuoted(_StyleCode) & "') "
                cmdstring &= vbCrLf & "  AND (FTSeason = N'" & HI.UL.ULF.rpQuoted(_SeasonCode) & "')"
                _SysStyleDevId = Integer.Parse(Val(HI.Conn.SQLConn.GetField(cmdstring, Conn.DB.DataBaseName.DB_MERCHAN, "0")))

                If _SysStyleDevId <= 0 Then
                    _SysStyleDevId = HI.TL.RunID.GetRunNoID("TMERTDevelopStyle", "FNHSysStyleDevId", Conn.DB.DataBaseName.DB_MERCHAN)

                    cmdstring = " INSERT INTO   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTDevelopStyle "
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


                    cmdstring = "  Select   BOM_ROW_NBR, MSC_CODE, MSC_LEVEL_1, MSC_LEVEL_2, MSC_LEVEL_3, SILHOUETTE, SEASON_CD, SEASON_YR, STYLE_NM, STYLE_NBR, STYLE_CW_CD, PLUG_CW_CD, PRMRY, SCNDY, TRTRY,  "
                    cmdstring &= vbCrLf & "  LOGO, ADDENDUM, COMPONENT_ORD, [USE], DESCRIPTION, ITEM_TYPE_1, ITEM_TYPE_2, ITEM_TYPE_3, ITEM_TYPE_4, [IS], IT, ITEM_NBR, ITEM_COLOR_ORD, GCW, GCW_ORD, GCW_ART_DESCRIPTION,"
                    cmdstring &= vbCrLf & "  ITEM_COLOR_CD, ITEM_COLOR_NM, ITEM_COLOR_ABRV, VEND_CD, VEND_LO, VEND_NM, QTY, UOM"
                    cmdstring &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FHS) & "].dbo.TTMPImportBomExcel WITH(NOLOCK) "
                    cmdstring &= vbCrLf & "  where FTUserLogIn='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                    cmdstring &= vbCrLf & "   AND STYLE_NBR='" & HI.UL.ULF.rpQuoted(_StyleCode) & "'"
                    cmdstring &= vbCrLf & "   AND SEASON_CD='" & HI.UL.ULF.rpQuoted(pSeason) & "'"
                    cmdstring &= vbCrLf & "   AND SEASON_YR='" & HI.UL.ULF.rpQuoted(pYear) & "'"
                    cmdstring &= vbCrLf & " GROUP BY   BOM_ROW_NBR, MSC_CODE, MSC_LEVEL_1, MSC_LEVEL_2, MSC_LEVEL_3, SILHOUETTE, SEASON_CD, SEASON_YR, STYLE_NM, STYLE_NBR, STYLE_CW_CD, PLUG_CW_CD, PRMRY, SCNDY, TRTRY,  "
                    cmdstring &= vbCrLf & "     LOGO, ADDENDUM, COMPONENT_ORD, [USE], DESCRIPTION, ITEM_TYPE_1, ITEM_TYPE_2, ITEM_TYPE_3, ITEM_TYPE_4, [IS], IT, ITEM_NBR, ITEM_COLOR_ORD, GCW, GCW_ORD, GCW_ART_DESCRIPTION,"
                    cmdstring &= vbCrLf & "     ITEM_COLOR_CD, ITEM_COLOR_NM, ITEM_COLOR_ABRV, VEND_CD, VEND_LO, VEND_NM, QTY, UOM"
                    cmdstring &= vbCrLf & " ORDER BY  BOM_ROW_NBR,STYLE_CW_CD "

                    _dtstyledetail = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_FHS)


                    cmdstring = " DELETE FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempDevelopStyle_Mat  WHERE  FNHSysStyleDevId=" & _SysStyleDevId & " "
                    cmdstring &= vbCrLf & " DELETE FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempDevelopStyle_ColorWay  WHERE  FNHSysStyleDevId=" & _SysStyleDevId & " "
                    cmdstring &= vbCrLf & " DELETE FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempDevelopStyle_SizeBreakDown  WHERE  FNHSysStyleDevId=" & _SysStyleDevId & " "
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

                        cmdstring = "SELECT  TOP 1 A.FTMainMatCode, A.FTCusItemCodeRef, B.FTSuplCode, C.FTUnitCode"
                        cmdstring &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMainMat AS A WITH(NOLOCK) LEFT OUTER JOIN"
                        cmdstring &= vbCrLf & "       [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit AS C WITH(NOLOCK) ON A.FNHSysUnitId = C.FNHSysUnitId LEFT OUTER JOIN"
                        cmdstring &= vbCrLf & "       [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier AS B WITH(NOLOCK) ON A.FNHSysSuplId = B.FNHSysSuplId"
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

                        cmdstring = " INSERT INTO   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempDevelopStyle_Mat "
                        cmdstring &= vbCrLf & " ("
                        cmdstring &= vbCrLf & " FTInsUser, FDInsDate, FTInsTime,  FNHSysStyleDevId, FNSeq"
                        cmdstring &= vbCrLf & " , FNMerMatSeq, FTItemNo, FTItemDesc, FTPartNameEN, FTPartNameTH, FTSuplCode, FTStateNominate "
                        cmdstring &= vbCrLf & "  ,FTUnitCode, FNPrice, FNHSysCurId, FNConSmp, FTComponent, FTStateActive"
                        cmdstring &= vbCrLf & ", FTStateCombination, FTStateMainMaterial, FTStateFree,FNPart"

                        cmdstring &= vbCrLf & " )"
                        cmdstring &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        cmdstring &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                        cmdstring &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ""
                        cmdstring &= vbCrLf & "," & _SysStyleDevId & ""
                        cmdstring &= vbCrLf & "," & _matseq & ""
                        cmdstring &= vbCrLf & "," & _matseq & ""
                        cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_MatCode) & "'"
                        cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTRawMatDesc.ToString) & "'"
                        cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTPosittion.ToString) & "'"
                        cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTPosittion.ToString) & "'"
                        cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_Suplier) & "'"

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

                                    cmdstring = " INSERT INTO   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempDevelopStyle_ColorWay "
                                    cmdstring &= vbCrLf & " ("
                                    cmdstring &= vbCrLf & " FTInsUser, FDInsDate, FTInsTime, FNHSysStyleDevId, FNSeq, FNMerMatSeq, FTColorWay, FTColorCode, FTColorNameTH, FTColorNameEN,FNColorWaySeq,FTRunColor"
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

                    cmdstring = " INSERT INTO   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempDevelopStyle_SizeBreakDown "
                    cmdstring &= vbCrLf & " ("
                    cmdstring &= vbCrLf & " FTInsUser, FDInsDate, FTInsTime, FNHSysStyleDevId, FNSeq, FNMerMatSeq, FTSizeBreakDown, FTSizeCode,FNSieBreakDownSeq,FTRunSize"
                    cmdstring &= vbCrLf & " )"
                    cmdstring &= vbCrLf & " SELECT A.FTInsUser, A.FDInsDate, A.FTInsTime, A.FNHSysStyleDevId, A.FNSeq, A.FNMerMatSeq ,B.FTSizeCode,'' AS FTSizeCode,B.FNSieBreakDownSeq,'1'"
                    cmdstring &= vbCrLf & "  FROM"
                    cmdstring &= vbCrLf & " (SELECT      FTInsUser, FDInsDate, FTInsTime, FNHSysStyleDevId, FNSeq, FNMerMatSeq "
                    cmdstring &= vbCrLf & " FROM      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempDevelopStyle_Mat  WHERE  FNHSysStyleDevId=" & _SysStyleDevId & "  ) AS A CROSS JOIN"
                    cmdstring &= vbCrLf & " (SELECT 'S' AS FTSizeCode,1 AS FNSieBreakDownSeq"
                    cmdstring &= vbCrLf & "    UNION"
                    cmdstring &= vbCrLf & " SELECT 'M' AS FTSizeCode,2 AS FNSieBreakDownSeq"
                    cmdstring &= vbCrLf & "  UNION"
                    cmdstring &= vbCrLf & " SELECT 'L' AS FTSizeCode,3 AS FNSieBreakDownSeq"
                    cmdstring &= vbCrLf & " UNION"
                    cmdstring &= vbCrLf & " SELECT 'XL' AS FTSizeCode,4 AS FNSieBreakDownSeq) AS B"

                    HI.Conn.SQLConn.ExecuteNonQuery(cmdstring, Conn.DB.DataBaseName.DB_MERCHAN)

                    cmdstring = " INSERT INTO   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTDevelopStyle_Mat"
                    cmdstring &= vbCrLf & " ("
                    cmdstring &= vbCrLf & " FTInsUser, FDInsDate, FTInsTime, FTUpdUser, FDUpdDate, FTUpdTime, FNHSysStyleDevId, FNSeq, FNMerMatSeq, FTItemNo, FTItemDesc, FNPart, FTPartNameEN, FTPartNameTH, FTSuplCode,"
                    cmdstring &= vbCrLf & " FTStateNominate, FTUnitCode, FNPrice, FNHSysCurId, FNConSmp, FNConSmpPlus, FTComponent, FTStateActive, FTStateCombination, FTStateMainMaterial, FTStateFree, FTPositionPartId, FTPart"
                    cmdstring &= vbCrLf & " )"
                    cmdstring &= vbCrLf & "   SELECT  A.FTInsUser, A.FDInsDate, A.FTInsTime, A.FTUpdUser, A.FDUpdDate, A.FTUpdTime, A.FNHSysStyleDevId, A.FNSeq, A.FNMerMatSeq, A.FTItemNo, A.FTItemDesc, A.FNPart, A.FTPartNameEN, A.FTPartNameTH, "
                    cmdstring &= vbCrLf & " A.FTSuplCode, A.FTStateNominate, A.FTUnitCode, A.FNPrice, A.FNHSysCurId, A.FNConSmp, A.FNConSmpPlus, A.FTComponent, A.FTStateActive, A.FTStateCombination, A.FTStateMainMaterial, A.FTStateFree,"
                    cmdstring &= vbCrLf & "   A.FTPositionPartId, A.FTPart"
                    cmdstring &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempDevelopStyle_Mat AS A LEFT OUTER JOIN"
                    cmdstring &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTDevelopStyle_Mat AS B ON A.FNHSysStyleDevId = B.FNHSysStyleDevId AND A.FNSeq = B.FNSeq AND A.FNMerMatSeq = B.FNMerMatSeq AND A.FTItemNo = B.FTItemNo AND A.FNPart = B.FNPart"
                    cmdstring &= vbCrLf & "  WHERE  A.FNHSysStyleDevId=" & _SysStyleDevId & " "
                    cmdstring &= vbCrLf & "  AND  (B.FNHSysStyleDevId Is NULL) "

                    HI.Conn.SQLConn.ExecuteNonQuery(cmdstring, Conn.DB.DataBaseName.DB_MERCHAN)

                    cmdstring = " INSERT INTO   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTDevelopStyle_ColorWay"
                    cmdstring &= vbCrLf & " ("
                    cmdstring &= vbCrLf & " FTInsUser, FDInsDate, FTInsTime, FTUpdUser, FDUpdDate, FTUpdTime, FNHSysStyleDevId, FNSeq, FNMerMatSeq, FTColorWay, FNColorWaySeq, FTRunColor, FTColorCode, FTColorNameTH,  FTColorNameEN"
                    cmdstring &= vbCrLf & " )"
                    cmdstring &= vbCrLf & "   SELECT  A.FTInsUser, A.FDInsDate, A.FTInsTime, A.FTUpdUser, A.FDUpdDate, A.FTUpdTime, A.FNHSysStyleDevId, A.FNSeq, A.FNMerMatSeq, A.FTColorWay, A.FNColorWaySeq, A.FTRunColor, A.FTColorCode, A.FTColorNameTH, A.FTColorNameEN"
                    cmdstring &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempDevelopStyle_ColorWay AS A LEFT OUTER JOIN"
                    cmdstring &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTDevelopStyle_ColorWay AS B ON A.FNHSysStyleDevId = B.FNHSysStyleDevId AND A.FNSeq = B.FNSeq AND A.FNMerMatSeq = B.FNMerMatSeq AND A.FTColorWay = B.FTColorWay "
                    cmdstring &= vbCrLf & "  WHERE  A.FNHSysStyleDevId=" & _SysStyleDevId & " "
                    cmdstring &= vbCrLf & "  AND  (B.FNHSysStyleDevId Is NULL) "

                    HI.Conn.SQLConn.ExecuteNonQuery(cmdstring, Conn.DB.DataBaseName.DB_MERCHAN)

                    cmdstring = " INSERT INTO   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTDevelopStyle_SizeBreakDown"
                    cmdstring &= vbCrLf & " ("
                    cmdstring &= vbCrLf & " FTInsUser, FDInsDate, FTInsTime, FTUpdUser, FDUpdDate, FTUpdTime, FNHSysStyleDevId, FNSeq, FNMerMatSeq, FTSizeBreakDown, FNSieBreakDownSeq, FTRunSize, FTSizeCode"
                    cmdstring &= vbCrLf & " )"
                    cmdstring &= vbCrLf & "   SELECT  A.FTInsUser, A.FDInsDate, A.FTInsTime, A.FTUpdUser, A.FDUpdDate, A.FTUpdTime, A.FNHSysStyleDevId, A.FNSeq, A.FNMerMatSeq, A.FTSizeBreakDown, A.FNSieBreakDownSeq, A.FTRunSize, A.FTSizeCode "
                    cmdstring &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempDevelopStyle_SizeBreakDown AS A LEFT OUTER JOIN"
                    cmdstring &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTDevelopStyle_SizeBreakDown AS B ON A.FNHSysStyleDevId = B.FNHSysStyleDevId AND A.FNSeq = B.FNSeq AND A.FNMerMatSeq = B.FNMerMatSeq AND A.FTSizeBreakDown = B.FTSizeBreakDown "
                    cmdstring &= vbCrLf & "  WHERE  A.FNHSysStyleDevId=" & _SysStyleDevId & " "
                    cmdstring &= vbCrLf & "  AND  (B.FNHSysStyleDevId Is NULL) "

                    HI.Conn.SQLConn.ExecuteNonQuery(cmdstring, Conn.DB.DataBaseName.DB_MERCHAN)

                End If

            Next


        Catch ex As Exception
            Return False
        End Try


        Return _StateImport
    End Function

    Private Sub FTFilePath_EditValueChanged(sender As Object, e As EventArgs) Handles FTFilePath.EditValueChanged

    End Sub
End Class
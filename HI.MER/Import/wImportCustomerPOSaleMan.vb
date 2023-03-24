Imports System
Imports System.Data
Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Data.OleDb
Imports System.Linq
Imports System.Collections.Generic
Imports System.Windows
Imports System.Windows.Forms
Imports System.Windows.Forms.Control
Imports System.Reflection
Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Configuration
Imports System.Globalization
Imports System.Threading
Imports DevExpress
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Columns

Imports DevExpress.Utils
Imports DevExpress.XtraEditors.ButtonEdit
Imports DevExpress.XtraEditors.SimpleButton
Imports System.Drawing
Imports Microsoft.Win32.SafeHandles
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Interaction
Imports System.Security.AccessControl
'Imports DevExpress.XtraSpreadsheet
'Imports DevExpress.Spreadsheet

Public Class wImportCustomerPOSaleMan

#Region "Variable Declaration"
    Private Const _DBEnum As Integer = HI.Conn.DB.DataBaseName.DB_MERCHAN
    Private Const _nRowHeader As Integer = 3
    Private Const _nStartRowImportExcel As Integer = 3
    Private Const _nStartColImportSizeValExcel As Integer = 49
    Private Const _nStartRowImportCSV As Integer = 3
    Private Const _nStartColImporSizeValCSV As Integer = 49
    Private Const _tTextMatchSizeTerminate As String = "Result"
    Private Const _tTextRowDelimeter As String = "Overall Result|Result"
    Private Const _tHyphenSign As String = "-"
    Private Const _tNotAssigned As String = "Not assigned"
    Private Const _tNA As String = "#"
    Private Const _tUnitImport As String = "UOM"
    Private _tCurtImport As Integer = 0
    Private _tSubPgmImport As Integer = 0

    '...form dialog confirm map size not exists in system master file
    Private _wMapSizeImportOrder As wMapSizeImportOrder

    Private _bInitialGridBandView As Boolean
    Private _bLoadFirstTime As Boolean = True

    Private tSql As String
    Private _SystemFilePath As String = Application.StartupPath & IIf(Microsoft.VisualBasic.Right(Application.StartupPath, 1) = "\", "", "\") & "Order"

    Private oDBdtImport As System.Data.DataTable
    Private oDBdtSrcOrder As System.Data.DataTable
    Private oDBdtMapSize As System.Data.DataTable
    Private oDBdtStyle As System.Data.DataTable
    Private _oDBdtUserImportMapSizeManual As System.Data.DataTable

    Private oGridViewSrcOrder As DevExpress.XtraGrid.Views.Grid.GridView
    Private oGridViewConfirmImport As DevExpress.XtraGrid.Views.Grid.GridView

    Private _bCatchSrcFile As Boolean

    Private startTime As DateTime

    Private _oSplash As HI.TL.SplashScreen = Nothing
    Private _tSplashText As String

    Dim stopWatch As Stopwatch = New Stopwatch()
    Dim days As System.Int64
    Dim Hours As System.Int64
    Dim Minutes As System.Int64
    Dim Seconds As System.Int64

    '...validate file is opened
    Private Declare Function CreateFile Lib "kernel32" Alias "CreateFileA" (ByVal lpFileName As String,
                                                                            ByVal dwDesiredAccess As Long,
                                                                            ByVal dwShareMode As Long,
                                                                            ByVal lpSecurityAttributes As Long,
                                                                            ByVal dwCreationDisposition As Long,
                                                                            ByVal dwFlagsAndAttributes As Long,
                                                                            ByVal hTemplateFile As Long) As Long

    Private Declare Function CloseHandle Lib "kernel32" (ByVal hFile As Long) As Long

#End Region

#Region "Method"
    ' Method
    Shared Function FileIsOpen4(ByVal pathfile As String) As Boolean
        Const GENERIC_READ As Long = &H80000000
        Const INVALID_HANDLE_VALUE As Long = -1
        Const OPEN_EXISTING As Long = 3
        Const FILE_ATTRIBUTE_NORMAL As Long = &H80
        Dim hFile As Long

        If System.IO.File.Exists(pathfile) Then
            Try
                ' note that FILE_ATTRIBUTE_NORMAL (&H80) has a different value than VB's constant vbNormal (0)!
                hFile = CreateFile(pathfile, GENERIC_READ, 0, 0, OPEN_EXISTING, FILE_ATTRIBUTE_NORMAL, 0&)
                ' this will evaluate to either -1 (File in use) or 0 (File free)
                Return hFile = INVALID_HANDLE_VALUE
            Catch ex As Exception
                MessageBox.Show(ex.ToString)
            Finally
                CloseHandle(hFile)
            End Try
        Else
            Return True
        End If
    End Function

    <DllImport("kernel32.dll", CharSet:=CharSet.Auto, SetLastError:=True)>
    Private Shared Function CreateFile(lpFileName As String, dwDesiredAccess As FileSystemRights, dwShareMode As FileShare, securityAttrs As IntPtr, dwCreationDisposition As FileMode, dwFlagsAndAttributes As FileOptions,
    hTemplateFile As IntPtr) As SafeFileHandle
    End Function

    Const ERROR_SHARING_VIOLATION As Integer = 32

    Private Function IsFileInUse(fileName As String) As Boolean
        Dim inUse As Boolean = False

        Dim fileHandle As SafeFileHandle = CreateFile(fileName, FileSystemRights.Modify, FileShare.Write, IntPtr.Zero, FileMode.OpenOrCreate, FileOptions.None, IntPtr.Zero)

        If fileHandle.IsInvalid Then
            If Marshal.GetLastWin32Error() = ERROR_SHARING_VIOLATION Then
                inUse = True
            End If
        End If

        fileHandle.Close()

        Return inUse

    End Function

#End Region

#Region "Property"

    Private _ProcComplete As Boolean = False
    Public Property ProcComplete As Boolean
        Get
            Return _ProcComplete
        End Get
        Set(ByVal value As Boolean)
            _ProcComplete = value
        End Set
    End Property

    Private _bPromptImport As Boolean = False
    Public Property bPromptImport As Boolean
        Get
            Return bPromptImport
        End Get
        Set(value As Boolean)
            _bPromptImport = value
        End Set
    End Property

#End Region

#Region "Method"

    Private Shared Function W_PRCbValidateFileIsOpen(ByVal ptPathfile As String) As Boolean
        Const GENERIC_READ As Long = &H80000000
        Const INVALID_HANDLE_VALUE As Long = -1
        Const OPEN_EXISTING As Long = 3
        Const FILE_ATTRIBUTE_NORMAL As Long = &H80
        Dim hFile As Long

        If System.IO.File.Exists(ptPathfile) Then
            Try
                ' note that FILE_ATTRIBUTE_NORMAL (&H80) has a different value than VB's constant vbNormal (0)!
                hFile = CreateFile(ptPathfile, GENERIC_READ, 0, 0, OPEN_EXISTING, FILE_ATTRIBUTE_NORMAL, 0&)
                ' this will evaluate to either -1 (File in use) or 0 (File free)
                Return hFile = INVALID_HANDLE_VALUE
            Catch ex As Exception
                MessageBox.Show(ex.ToString)
                Return False
            Finally
                CloseHandle(hFile)
            End Try
        Else
            Return True
        End If

    End Function

#End Region

#Region "Procedure And Function"

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.

        '===================================================== _wMapSizeImportOrder =======================================================
        _wMapSizeImportOrder = New wMapSizeImportOrder(Nothing)

        HI.TL.HandlerControl.AddHandlerObj(_wMapSizeImportOrder)

        Dim oSysLang As New HI.ST.SysLanguage

        Try
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleID, _wMapSizeImportOrder.Name.ToString.Trim, _wMapSizeImportOrder)
        Catch ex As Exception
            '...Nothing 
        End Try
        '==================================================================================================================================

    End Sub

    'Public Function IsFileOpen(filename As String)
    '    Dim FileNum As Integer, errnum As Integer

    '    On Error Resume Next   ' Turn error checking off.
    '    FileNum = FreeFile()   ' Get a free file number.
    '    ' Attempt to open the file and lock it.
    '    Open filename For Input Lock Read As #FileNum
    '    Close FileNum          ' Close the file.
    '    errnum = Err           ' Save the error number that occurred.
    '    On Error GoTo 0        ' Turn error checking back on.

    '    ' Check to see which error occurred.
    '    Select Case errnum
    '        Case 0
    '            ' No error occurred. File is NOT already open by another user.
    '            IsFileOpen = False
    '        Case 70
    '            ' Error number for "Permission Denied. File is already opened by another user.
    '            IsFileOpen = True
    '        Case Else
    '            ' Another error occurred.
    '            Error errnum
    '    End Select

    'End Function

    Private Function W_PRCbValidateProgrammeVendor() As Boolean
        Dim _bRetProgrammeVendor As Boolean = True


        Return _bRetProgrammeVendor

    End Function

    Private Function W_PRCbValidateConfirmGenerateFactoryOrder() As Boolean
        Dim _bRet As Boolean = False
        Try

            _bRet = True

        Catch ex As Exception

        End Try

        Return _bRet

    End Function

    Private Function PRCnUpdateImportTime(ByVal paramImportTime As Integer) As Integer
        Static nCntImport As Integer = 0 'The Static variable nCntImport is initialized to 0 only one time
        System.Diagnostics.Debug.Print(String.Format("nCntImport before update : {0}", nCntImport))
        nCntImport = nCntImport + paramImportTime
        System.Diagnostics.Debug.Print(String.Format("nCntImport after update : {0}", nCntImport))
        Return nCntImport
    End Function

    Private Function W_PRCbClsr() As Boolean
        Try
            'HI.TL.HandlerControl.ClearControl(Me.ogbHeader)
            'HI.TL.HandlerControl.ClearControl(Me.ogbBrowseFile)

            'I
            'oGridViewSrcOrder = Me.ogdImportOrder.Views(0)
            'Me.ogdImportOrder.DataSource = Nothing
            'Call W_PRCbRemoveGridViewColumn(Me.ogvImportOrder)

            'I

            _bCatchSrcFile = False

            HI.TL.HandlerControl.ClearControl(Me)

        Catch ex As Exception
        End Try

        Return True

    End Function

    Private Shared Function W_PRCbValidateIsFileLocked(exception As Exception) As Boolean
        Dim errorCode As Integer = Marshal.GetHRForException(exception) And ((1 << 16) - 1)
        Return errorCode = 32 OrElse errorCode = 33
    End Function



    Private Function W_PRCbImportToGridView(ByVal ptFilePath As String, ByVal ptExtension As String, ByVal ptHDR As String) As Boolean
        Dim _bImportSuccess As Boolean = False

        Try
            If Me.ogvImportOrder.RowCount <= _nRowHeader Then
                HI.MG.ShowMsg.mInfo("", 77889, Me.FTFilePath.Text)
            Else
                If Not HI.MG.ShowMsg.mConfirmProcess(MG.ShowMsg.ProcessType.mDelete, "Are you sure, do you want to import order no", "Information") = True Then
                    Me.FTFilePath.Focus()
                Else
                    '...process generate factory order no. (comfirm import data to system)

                    _bImportSuccess = True
                End If

            End If

        Catch ex As Exception
            'If System.Diagnostics.Debugger.IsAttached = True Then
            Throw New Exception(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString())
            'End If
        End Try

        Return _bImportSuccess

    End Function

    Private Function W_GETdtReadExcelData(ptFilePath As String, _extension As String) As System.Data.DataTable
        Dim oDBdtExcel As New System.Data.DataTable()
        Dim hasHeaders As Boolean = False
        Dim HDR As String = If(hasHeaders, "Yes", "No")
        Dim strConn As String

        Try
            If ptFilePath.Substring(ptFilePath.LastIndexOf("."c)).ToLower() = ".xlsx" Then
                strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & ptFilePath & ";Extended Properties=""Excel 12.0;HDR=" & HDR & ";IMEX=1"""
            Else
                'strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & ptFilePath & ";Extended Properties=""Excel 8.0;HDR=" & HDR & ";IMEX=1"""
                strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & ptFilePath & ";Extended Properties=""Excel 8.0;HDR=" & HDR & ";IMEX=1"""
            End If

            MsgBox("Step before connected excel file...", , MsgBoxStyle.OkOnly)

            Dim conn As New OleDbConnection(strConn)

            Try
                conn.Open()

                MsgBox("Connected excel file complete...", , MsgBoxStyle.OkOnly)

            Catch ex As Exception
                MsgBox("Cannot connected excel file !!!", , MsgBoxStyle.OkOnly)
            End Try


            MsgBox("Step after connected excel file complete...", , MsgBoxStyle.OkOnly)

            Dim schemaTable As DataTable = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, New Object() {Nothing, Nothing, Nothing, "TABLE"})

            'Looping Total Sheet of Xl File
            'foreach (DataRow schemaRow in schemaTable.Rows)
            '                {
            '                }

            'Looping a first Sheet of Xl File
            Dim schemaRow As DataRow = schemaTable.Rows(0)

            Dim sheet As String = "Sheet1" REM 2014/06/05 schemaRow("TABLE_NAME").ToString()

            If Not sheet.EndsWith("_") Then
                MsgBox("Step Adapter fill rocord set to datatable", MsgBoxStyle.OkOnly)

                Dim query As String = "SELECT  *  FROM [" & sheet & "$]"
                Dim daexcel As New OleDbDataAdapter(query, conn)
                'oDBdtExcel.Locale = CultureInfo.CurrentCulture
                daexcel.Fill(oDBdtExcel)
            End If

            conn.Close()

        Catch ex As Exception
        End Try

        If oDBdtExcel.Rows.Count > 0 Then
            MsgBox("Record Count : " & oDBdtExcel.Rows.Count, , MsgBoxStyle.OkOnly)
        Else
            MsgBox("Not have record !!!", , MsgBoxStyle.OkOnly)
        End If

        Return oDBdtExcel

    End Function

    Private Function W_PRCdtReadExcelIntoDataTable(ByVal ptFileName As String, ByVal ptSheetName As String) As DataTable
        Dim RetVal As New DataTable

        Dim tConnString As String

        tConnString = "Driver={Microsoft Excel Driver (*.xls, *.xlsx, *.xlsm, *.xlsb)};DBQ=" & ptFileName & ";Extended Properties=""IMEX=1"""

        Dim tSQL As String

        tSQL = "SELECT * FROM [" & ptSheetName & "$]"

        Dim oAdapter As New System.Data.Odbc.OdbcDataAdapter(tSQL, tConnString)

        oAdapter.Fill(RetVal)

        Return RetVal

    End Function

    Private Function W_PRCdtReadCSV(ByVal ptPath As String) As System.Data.DataTable
        Try
            Dim sr As New StreamReader(ptPath)

            Dim fullFileStr As String = sr.ReadToEnd()

            sr.Close()
            sr.Dispose()

            Dim lines As String() = fullFileStr.Split(ControlChars.Lf)
            Dim recs As New DataTable()
            Dim sArr As String() = lines(0).Split(","c)

            For Each s As String In sArr
                recs.Columns.Add(New DataColumn())
            Next

            Dim row As DataRow
            Dim finalLine As String = ""

            For Each line As String In lines
                row = recs.NewRow()
                finalLine = line.Replace(Convert.ToString(ControlChars.Cr), "")
                row.ItemArray = finalLine.Split(","c)
                recs.Rows.Add(row)
            Next

            Return recs

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Private Function W_PRCdtRead_CSV(ByVal ptFilePath As String) As DataTable
        '----------------------------------------------------
        'Reads a csv file into a datatable, with the first row as column headers

        REM Dim myFile As String = FilePath & "\" & FileName
        Dim myFile As String = ptFilePath
        'Dim myTable As DataTable = New DataTable("MyTable")
        Dim myTable As DataTable = New System.Data.DataTable
        Dim i As Integer
        Dim myRow As DataRow
        Dim myColumn As DataColumn
        Dim MyType As String
        Dim fieldValues As String()

        Dim ColumnNames As String()
        Dim ColumnTypes As String()
        Dim myReader As New StreamReader(myFile)
        Try

            'Open file and read first two lines
            ColumnNames = myReader.ReadLine().Split(",")
            ColumnTypes = myReader.ReadLine().Split(",")

            'Create data columns named according to first line of data, with type according to second line
            For i = 0 To ColumnNames.Length() - 1
                myColumn = New DataColumn()
                MyType = "System." & ColumnTypes(i)
                myColumn.DataType = System.Type.GetType(MyType)

                myColumn.ColumnName = ColumnNames(i)
                myTable.Columns.Add(myColumn)
            Next

            'Read the body of the data to data table
            While myReader.Peek() <> -1
                fieldValues = myReader.ReadLine().Split(",")
                myRow = myTable.NewRow
                For i = 0 To fieldValues.Length() - 1
                    myRow.Item(i) = fieldValues(i).ToString
                Next
                myTable.Rows.Add(myRow)
            End While
        Catch ex As Exception
            MsgBox("Error building datatable: " & ex.Message)
            Return New DataTable("Empty")
        Finally
            myReader.Close()
        End Try

        Return myTable

    End Function

    Private Sub Resolve_Duplicate_Names(ByRef source() As String)
        ' Resolves the possibility of duplicated names by appending "Duplicate Name" and a number at the end of any duplicates
        Dim i, n, dnum As Integer
        dnum = 1
        For n = 0 To source.Length - 1
            For i = n + 1 To source.Length - 1
                If source(i) = source(n) Then
                    source(i) = source(i) & "Duplicate Name " & dnum
                    dnum += 1
                End If
            Next
        Next

        Return

    End Sub

    Private Function AddValuesToTable(ByRef source() As String, ByVal destination As DataTable, Optional ByVal HeaderFlag As Boolean = False) As Boolean
        'Ensures a datatable can hold an array of values and then adds a new row 
        Try
            Dim existing As Integer = destination.Columns.Count

            If HeaderFlag Then

                Resolve_Duplicate_Names(source)

                For i As Integer = 0 To source.Length - existing - 1
                    destination.Columns.Add(source(i).ToString, GetType(String))
                Next i

                Return True

            End If

            For i As Integer = 0 To source.Length - existing - 1
                REM destination.Columns.Add("Column" & (existing + 1 + i).ToString, GetType(String))
                destination.Columns.Add("F" & (existing + 1 + i).ToString, GetType(String))
            Next

            destination.Rows.Add(source)

            Return True

        Catch ex As Exception
            Console.WriteLine(ex.ToString())
            Return False
        End Try

    End Function

    Private Sub SaveQuantityByStyle(FTPONo As String, StyleCode As String, SeasonCode As String, StyleId As Integer, SeasonId As Integer, _FNImportOrderType As Integer, _FixCmpByProgram As Integer, nFNHSysVenderPramId As Integer, nFNHSysCustId As Integer, nFNHSysCmpRunId As Integer, nFNHSysBuyId As Integer)
        Dim _dtdata As DataTable
        Dim _Qry As String
        Dim _Total As Integer = 0

        _Qry = " Exec [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.[SP_Get_Style_BreakDown_DemandPull_CheckImport] " & StyleId & " "
        _dtdata = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

        If _dtdata.Rows.Count > 0 Then

            Dim sql As String = ""
            Dim dt As DataTable

            sql = "   SELECT A.FTPONo, A.FTOrderDate, A.FTStyle, A.FTPlanningSeason + RIGHT(A.FTYear, 2) AS FTSeason, MAX(A.FNHSysMainCategoryId) AS FNHSysMainCategoryId, MAX(A.FNHSysPlantId) AS FNHSysPlantId  "
            sql &= vbCrLf & " , MAX(A.FNHSysBuyGrpId)  As FNHSysBuyGrpId, MAX(A.FNHSysGenderId) As FNHSysGenderId, MAX(A.FDShipDateOrginal) As FDShipDateOrginal, MAX(A.FDShipDate) As FDShipDate, MAX(A.FNHSysMerTeamId) As FNHSysMerTeamId, "
            sql &= vbCrLf & "   MAX(A.FNHSysProdTypeId) AS FNHSysProdTypeId, MAX(A.FNHSysCountryId) AS FNHSysCountryId, MAX(A.FNHSysBuyerId) AS FNHSysBuyerId, MAX(A.FNHSysProvinceId) AS FNHSysProvinceId, B.FTColorwayCode, "
            sql &= vbCrLf & "   B.FTSizeBreakdownCode, SUM(B.FNQuantity) As FNQuantity, MAX(B.FNPrice) As FNPrice ,0 AS FNCreateData,0 AS FNGenOrderData,MAX(B.FTPOItem) AS FTPOItem"
            sql &= vbCrLf & " From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTImportOrderTemp As A WITH(NOLOCK) INNER Join "
            sql &= vbCrLf & "      [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTImportOrderSizeBreakdownTemp As B  WITH(NOLOCK) On A.FTUserLogin = B.FTUserLogin And A.FNRowImport = B.FNRowImport And A.FTPONo = B.FTPONo And A.FTPOTrading = B.FTPOTrading And A.FTPOItem = B.FTPOItem"
            sql &= vbCrLf & "      And  A.FTStyle = B.FTStyle "
            sql &= vbCrLf & "  Where  (A.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "') "
            sql &= vbCrLf & "         And A.FTPONo ='" & HI.UL.ULF.rpQuoted(FTPONo) & "' "
            sql &= vbCrLf & "         And A.FTStyle ='" & HI.UL.ULF.rpQuoted(StyleCode) & "' "
            sql &= vbCrLf & "	      And A.FTPlanningSeason + RIGHT(A.FTYear, 2)='" & HI.UL.ULF.rpQuoted(SeasonCode) & "' "
            sql &= vbCrLf & "	     And ISNULL(B.FNQuantity,0) >0 "
            sql &= vbCrLf & "  GROUP BY A.FTPONo, A.FTOrderDate, A.FTStyle, A.FTPlanningSeason + Right(A.FTYear, 2), B.FTColorwayCode, B.FTSizeBreakdownCode "

            dt = HI.Conn.SQLConn.GetDataTable(sql, Conn.DB.DataBaseName.DB_MERCHAN)

            Dim BalQty As Integer = 0
            Dim MainBalQty As Integer = 0
            Dim StateCheck As Boolean = True
            Dim MainJobRef As String = ""
            Dim OrderDate As String, MainCategoryId As Integer, PlantId As Integer, BuyGrpId As Integer, GenderId As Integer, ShipDateOrginal As String, ShipDate As String, MerTeamId As Integer, ProdTypeId As Integer, CountryId As Integer, BuyerId As Integer, ProvinceId As Integer

            Do While dt.Select("FNQuantity-FNCreateData >0").Length > 0 And StateCheck

                MainJobRef = ""


                For Each R As DataRow In dt.Select("FNQuantity-FNCreateData >0", "FTColorwayCode,FTSizeBreakdownCode")


                    If MainJobRef = "" Then

                        OrderDate = R!FTOrderDate.ToString
                        MainCategoryId = Val(R!FNHSysMainCategoryId.ToString)
                        PlantId = Val(R!FNHSysPlantId.ToString)
                        BuyGrpId = Val(R!FNHSysBuyGrpId.ToString)
                        GenderId = Val(R!FNHSysGenderId.ToString)
                        ShipDateOrginal = R!FDShipDateOrginal.ToString
                        ShipDate = R!FDShipDate.ToString
                        MerTeamId = Val(R!FNHSysMerTeamId.ToString)
                        ProdTypeId = Val(R!FNHSysProdTypeId.ToString)
                        CountryId = Val(R!FNHSysCountryId.ToString)
                        BuyerId = Val(R!FNHSysBuyerId.ToString)
                        ProvinceId = Val(R!FNHSysProvinceId.ToString)

                        For Each Rx As DataRow In _dtdata.Select("FTColorway='" & HI.UL.ULF.rpQuoted(R!FTColorwayCode.ToString) & "' AND FTSizeBreakDown='" & HI.UL.ULF.rpQuoted(R!FTSizeBreakdownCode.ToString) & "' AND FNQuantity-FNCreateData >0 AND FNQuantity>0", "FTOrderNo")
                            MainJobRef = Rx!FTOrderNo.ToString
                            Exit For
                        Next
                    End If


                    If MainJobRef <> "" Then

                        BalQty = (Val(R!FNQuantity.ToString) - Val(R!FNCreateData.ToString))

                        '   Do While StateCheck And BalQty > 0

                        For Each Rx As DataRow In _dtdata.Select("FTColorway='" & HI.UL.ULF.rpQuoted(R!FTColorwayCode.ToString) & "' AND FTSizeBreakDown='" & HI.UL.ULF.rpQuoted(R!FTSizeBreakdownCode.ToString) & "' AND FNQuantity-FNCreateData >0 AND FNQuantity>0 AND FTOrderNo='" & HI.UL.ULF.rpQuoted(MainJobRef) & "'")
                            MainBalQty = (Val(Rx!FNQuantity.ToString) - Val(Rx!FNCreateData.ToString))

                            If MainBalQty > BalQty Then

                                Rx!FNGenOrderData = BalQty
                                Rx!FNCreateData = Val(Rx!FNCreateData.ToString) + BalQty
                                BalQty = 0

                                R!FNCreateData = Val(R!FNCreateData.ToString) + BalQty
                                R!FNGenOrderData = BalQty

                            Else

                                Rx!FNGenOrderData = MainBalQty
                                Rx!FNCreateData = Val(Rx!FNCreateData.ToString) + MainBalQty
                                BalQty = BalQty - MainBalQty
                                R!FNCreateData = Val(R!FNCreateData.ToString) + MainBalQty
                                R!FNGenOrderData = MainBalQty

                            End If

                        Next

                        '  Loop

                    End If

                Next

                If MainJobRef <> "" Then

                    If dt.Select(" FNGenOrderData>0 ").Length > 0 And MainJobRef <> "" Then

                        Call SaveDemandPull(dt, MainJobRef, FTPONo, StyleCode, SeasonCode, StyleId, SeasonId, _FNImportOrderType, _FixCmpByProgram, nFNHSysVenderPramId, nFNHSysCustId, nFNHSysCmpRunId, nFNHSysBuyId, OrderDate, MainCategoryId, PlantId, BuyGrpId, GenderId, ShipDateOrginal, ShipDate, MerTeamId, ProdTypeId, CountryId, BuyerId, ProvinceId)

                    End If

                End If

                If dt.Select("FNQuantity-FNCreateData >0", "FTColorwayCode,FTSizeBreakdownCode").Length <= 0 Then
                End If

                If MainJobRef = "" Then
                    StateCheck = False
                End If

            Loop

            If dt.Select("FNQuantity-FNCreateData >0").Length > 0 Then
                MainJobRef = ""

                For Each R As DataRow In dt.Select("FNQuantity-FNCreateData >0", "FTColorwayCode,FTSizeBreakdownCode")

                    If MainJobRef = "" Then

                        OrderDate = R!FTOrderDate.ToString
                        MainCategoryId = Val(R!FNHSysMainCategoryId.ToString)
                        PlantId = Val(R!FNHSysPlantId.ToString)
                        BuyGrpId = Val(R!FNHSysBuyGrpId.ToString)
                        GenderId = Val(R!FNHSysGenderId.ToString)
                        ShipDateOrginal = R!FDShipDateOrginal.ToString
                        ShipDate = R!FDShipDate.ToString
                        MerTeamId = Val(R!FNHSysMerTeamId.ToString)
                        ProdTypeId = Val(R!FNHSysProdTypeId.ToString)
                        CountryId = Val(R!FNHSysCountryId.ToString)
                        BuyerId = Val(R!FNHSysBuyerId.ToString)
                        ProvinceId = Val(R!FNHSysProvinceId.ToString)

                        For Each Rx As DataRow In _dtdata.Select("FTColorway='" & HI.UL.ULF.rpQuoted(R!FTColorwayCode.ToString) & "' AND FTSizeBreakDown='" & HI.UL.ULF.rpQuoted(R!FTSizeBreakdownCode.ToString) & "' ", "FTOrderNo")
                            MainJobRef = Rx!FTOrderNo.ToString
                            Exit For
                        Next
                    End If
                    MainBalQty = Val(R!FNQuantity.ToString) - Val(R!FNCreateData.ToString)

                    If MainBalQty > 0 Then
                        R!FNCreateData = Val(R!FNCreateData.ToString) + MainBalQty
                        R!FNGenOrderData = MainBalQty
                    Else
                        R!FNGenOrderData = 0
                    End If

                Next

                If dt.Select(" FNGenOrderData>0 ").Length > 0 And MainJobRef <> "" Then
                    Call SaveDemandPull(dt, MainJobRef, FTPONo, StyleCode, SeasonCode, StyleId, SeasonId, _FNImportOrderType, _FixCmpByProgram, nFNHSysVenderPramId, nFNHSysCustId, nFNHSysCmpRunId, nFNHSysBuyId, OrderDate, MainCategoryId, PlantId, BuyGrpId, GenderId, ShipDateOrginal, ShipDate, MerTeamId, ProdTypeId, CountryId, BuyerId, ProvinceId)
                End If

            End If


        End If

    End Sub


    Private Function SaveDemandPull(dtimport As DataTable, MainJobRef As String, FTPONo As String, StyleCode As String, SeasonCode As String,
                                        StyleId As Integer, SeasonId As Integer, _FNImportOrderType As Integer, _FixCmpByProgram As Integer,
                                        nFNHSysVenderPramId As Integer, nFNHSysCustId As Integer, nFNHSysCmpRunId As Integer, nFNHSysBuyId As Integer,
                                        OrderDate As String, MainCategoryId As Integer, PlantId As Integer, BuyGrpId As Integer, GenderId As Integer,
                                        ShipDateOrginal As String, ShipDate As String, MerTeamId As Integer, ProdTypeId As Integer, CountryId As Integer,
                                        BuyerId As Integer, ProvinceId As Integer
                                    ) As String
        Dim _DmP As String = ""
        Dim _DmPSub As String = ""
        Dim _Key As String = ""
        Dim _Qry As String = ""
        Dim _FTOrderNo As String = ""
        Dim _FTOrderNoMain As String = ""
        Dim _FTSubOrderNo As String = ""
        Dim _Dt As DataTable


        _FTOrderNo = MainJobRef
        _FTOrderNoMain = ""

        _Qry = " SELECT  TOP 1 FTSubOrderNo"
        _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub AS X"
        _Qry &= vbCrLf & "  WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(_FTOrderNo) & "'"
        _FTSubOrderNo = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MERCHAN, "")


        _DmPSub = _DmP & "-A"

        HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_SYSTEM)
        HI.Conn.SQLConn.SqlConnectionOpen()
        HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
        HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

        Try

            'If _FTOrderNoMain = "" Then

            _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder("
            _Qry &= vbCrLf & "  FTInsUser, FDInsDate, FTInsTime, FTOrderNo, FDOrderDate, FTOrderBy, FNOrderType, FNHSysCmpId"
            _Qry &= vbCrLf & " , FNHSysCmpRunId, FNHSysStyleId, FTPORef, FNHSysCustId, FNHSysAgencyId, FNHSysProdTypeId, "
            _Qry &= vbCrLf & "       FNHSysBuyerId, FTMainMaterial, FTCombination, FTRemark "
            _Qry &= vbCrLf & "  ,FTImage1, FTImage2, FTImage3, FTImage4, FNHSysBrandId, FNHSysBuyId, FNHSysMerTeamId, "
            _Qry &= vbCrLf & "    FNHSysPlantId, FNHSysBuyGrpId, FNHSysMainCategoryId, FNHSysVenderPramId, FTOrderCreateStatus"
            _Qry &= vbCrLf & " , FPOrderImage1, FPOrderImage2, FPOrderImage3, FPOrderImage4, FNHSysSeasonId, FTOrderNoRef, FNJobState,FNHSysStyleIdPull,FNHSysCmpIdCreate, FTImportUser, FDImportDate, FTImportTime"
            _Qry &= vbCrLf & " )"
            _Qry &= vbCrLf & "   SELECT Top 1  "
            _Qry &= vbCrLf & " '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_DmP) & "' AS  FTOrderNo"
            _Qry &= vbCrLf & ",'" & OrderDate & "' AS  FDOrderDate"
            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' AS  FTOrderBy"
            _Qry &= vbCrLf & ",0 AS  FNOrderType, FNHSysCmpId," & nFNHSysCmpRunId & " AS  FNHSysCmpRunId, " & StyleId & " AS  FNHSysStyleId"
            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(FTPONo) & "' AS FTPORef"
            _Qry &= vbCrLf & " ," & nFNHSysCustId & " AS  FNHSysCustId, FNHSysAgencyId," & ProdTypeId & " AS  FNHSysProdTypeId, "
            _Qry &= vbCrLf & "   FNHSysBuyerId, FTMainMaterial, FTCombination, FTRemark, FTImage1, FTImage2"
            _Qry &= vbCrLf & " , FTImage3, FTImage4, FNHSysBrandId," & nFNHSysBuyId & " AS  FNHSysBuyId," & MerTeamId & " AS  FNHSysMerTeamId,"
            _Qry &= vbCrLf & "   FNHSysPlantId, FNHSysBuyGrpId," & MainCategoryId & " AS  FNHSysMainCategoryId," & nFNHSysVenderPramId & " AS  FNHSysVenderPramId"
            _Qry &= vbCrLf & " ,'Y' AS  FTOrderCreateStatus, FPOrderImage1"
            _Qry &= vbCrLf & " , FPOrderImage2, FPOrderImage3, FPOrderImage4," & SeasonId & " AS  FNHSysSeasonId"
            _Qry &= vbCrLf & ",FTOrderNo AS  FTOrderNoRef,"
            _Qry &= vbCrLf & " 0 AS  FNJobState"
            _Qry &= vbCrLf & " ,FNHSysStyleId," & Val(HI.ST.SysInfo.CmpID) & ""
            _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
            _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS A WITH(NOLOCK)"
            _Qry &= vbCrLf & " WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(_FTOrderNo) & "'"

            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                Return ""
            End If

            ' End If

            _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub("
            _Qry &= vbCrLf & "  FTInsUser, FDInsDate, FTInsTime, FTOrderNo, FTSubOrderNo, FDSubOrderDate, FTSubOrderBy"
            _Qry &= vbCrLf & ", FDProDate, FDShipDate, FNHSysBuyId, FNHSysContinentId,"
            _Qry &= vbCrLf & "FNHSysCountryId, FNHSysProvinceId, FNHSysShipModeId, FNHSysCurId"
            _Qry &= vbCrLf & ", FNHSysGenderId, FNHSysUnitId, FTStateEmb, FTStatePrint"
            _Qry &= vbCrLf & ", FTStateHeat, FTStateLaser, FTStateWindows, FTStateOther1,"
            _Qry &= vbCrLf & "  FTOther1Note, FTStateOther2, FTOther2Note, FTStateOther3"
            _Qry &= vbCrLf & ", FTOther3Note1, FTRemark, FNHSysShipPortId, FDShipDateOrginal, FTCustRef,FNHSysPlantId"
            _Qry &= vbCrLf & " )"
            _Qry &= vbCrLf & "   SELECT Top 1  "
            _Qry &= vbCrLf & " '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_DmP) & "' AS  FTOrderNo"
            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_DmPSub) & "' AS FTSubOrderNo"
            _Qry &= vbCrLf & ",'" & OrderDate & "' AS FDSubOrderDate"
            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' AS FTSubOrderBy"
            _Qry &= vbCrLf & ",FDProDate"
            _Qry &= vbCrLf & ",'" & ShipDate & "' AS FDShipDate"
            _Qry &= vbCrLf & "," & nFNHSysBuyId & " AS  FNHSysBuyId"
            _Qry &= vbCrLf & " ,FNHSysContinentId"
            _Qry &= vbCrLf & "," & CountryId & " AS FNHSysCountryId"
            _Qry &= vbCrLf & "," & ProvinceId & " AS FNHSysProvinceId"
            _Qry &= vbCrLf & ",FNHSysShipModeId"
            _Qry &= vbCrLf & ",FNHSysCurId"
            _Qry &= vbCrLf & "," & GenderId & " AS FNHSysGenderId"

            _Qry &= vbCrLf & ",FTStateEmb"
            _Qry &= vbCrLf & ",FTStatePrint"
            _Qry &= vbCrLf & ",FTStateHeat"
            _Qry &= vbCrLf & ",FTStateLaser"
            _Qry &= vbCrLf & ",FTStateWindows"
            _Qry &= vbCrLf & ", FTStateOther1"
            _Qry &= vbCrLf & ",FTOther1Note"
            _Qry &= vbCrLf & ", FTStateOther2"
            _Qry &= vbCrLf & ", FTOther2Note"
            _Qry &= vbCrLf & ", FTStateOther3"
            _Qry &= vbCrLf & ", FTOther3Note1"
            _Qry &= vbCrLf & ",FTRemark"
            _Qry &= vbCrLf & ",FNHSysShipPortId"
            _Qry &= vbCrLf & ",'" & ShipDateOrginal & "' AS FDShipDateOrginal"
            _Qry &= vbCrLf & ",FTCustRef"
            _Qry &= vbCrLf & "," & PlantId & " AS FNHSysPlantId"
            _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub AS X"
            _Qry &= vbCrLf & "  WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(_FTOrderNo) & "'"
            _Qry &= vbCrLf & "  AND FTSubOrderNo='" & HI.UL.ULF.rpQuoted(_FTSubOrderNo) & "'"

            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                Return ""

            End If

            Dim _ColorWay, _FTSizeBreakDown, _FTNikePOLineItem As String
            Dim Quantity As Integer = 0

            dtimport.BeginInit()
            For Each Rx As DataRow In dtimport.Select(" FNGenOrderData>0 ")

                _ColorWay = Rx!FTColorwayCode.ToString
                _FTNikePOLineItem = Rx!FTPOItem.ToString
                _FTSizeBreakDown = Rx!FTSizeBreakdownCode.ToString
                Quantity = Val(Rx!FNGenOrderData.ToString)


                _Qry = "Insert into [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_BreakDown "
                _Qry &= vbCrLf & " (  FTInsUser, FDInsDate, FTInsTime"
                _Qry &= vbCrLf & ",  FTOrderNo, FTSubOrderNo, FTColorway, FTSizeBreakDown"
                _Qry &= vbCrLf & ", FNQuantity,FNGrandQuantity, FNHSysMatColorId, FNHSysMatSizeId, "
                _Qry &= vbCrLf & "  FNExtraQty, FNQuantityExtra, FNGarmentQtyTest,FTNikePOLineItem)"
                _Qry &= vbCrLf & "   SELECT Top 1  "
                _Qry &= vbCrLf & " '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_DmP) & "' AS  FTOrderNo"
                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_DmPSub) & "' AS FTSubOrderNo"
                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_ColorWay) & "' "
                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_FTSizeBreakDown) & "' "
                _Qry &= vbCrLf & "," & Quantity & " "
                _Qry &= vbCrLf & "," & Quantity & " "
                _Qry &= vbCrLf & ",ISNULL((SELECT TOP 1 FNHSysMatColorId FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMatColor WITH(NOLOCK) WHERE FTMatColorCode='" & HI.UL.ULF.rpQuoted(_ColorWay) & "' ),0)"
                _Qry &= vbCrLf & ",ISNULL((SELECT TOP 1 FNHSysMatSizeId FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMatSize WITH(NOLOCK) WHERE FTMatSizeCode='" & HI.UL.ULF.rpQuoted(_FTSizeBreakDown) & "' ),0)"
                _Qry &= vbCrLf & ",0"
                _Qry &= vbCrLf & ",0"
                _Qry &= vbCrLf & ",0"
                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_FTNikePOLineItem) & "' "

                If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                    Return ""

                End If


                Rx!FNGenOrderData = 0

            Next

            dtimport.EndInit()
            _Qry = " UPDATE M SET"
            _Qry &= vbCrLf & " FNPrice=BB.FNPrice"
            _Qry &= vbCrLf & " ,FNPriceOrg=BB.FNPriceOrg"
            _Qry &= vbCrLf & " ,FNCMDisPer=BB.FNCMDisPer"
            _Qry &= vbCrLf & " ,FNCMDisAmt=BB.FNCMDisAmt"
            _Qry &= vbCrLf & " ,FNNetPrice=BB.FNNetPrice"
            _Qry &= vbCrLf & "  ,FNAmt=Convert(numeric(18,2),(M.FNQuantity * BB.FNPrice))"
            _Qry &= vbCrLf & ", FNAmntExtra=0"
            _Qry &= vbCrLf & ", FNGrandAmnt=Convert(numeric(18,2),(M.FNQuantity * BB.FNPrice))"
            _Qry &= vbCrLf & ", FNAmntQtyTest=0"
            '_Qry &= vbCrLf & ", FTNikePOLineItem = BB.FTNikePOLineItem"
            _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_BreakDown  AS M INNER JOIN "
            _Qry &= vbCrLf & " (  SELECT B.FTColorway, B.FTSizeBreakDown"
            _Qry &= vbCrLf & ", MAX(B.FNPrice) AS FNPrice, MAX(B.FNPriceOrg) AS FNPriceOrg, MAX(B.FNCMDisPer) AS FNCMDisPer"
            _Qry &= vbCrLf & ", MAX(B.FNCMDisAmt) AS FNCMDisAmt, MAX(B.FNNetPrice)    AS FNNetPrice,MAX(B.FTNikePOLineItem) AS FTNikePOLineItem"
            _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS A WITH(NOLOCK) INNER JOIN"
            _Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_BreakDown AS B WITH(NOLOCK)  ON A.FTOrderNo = B.FTOrderNo"
            _Qry &= vbCrLf & " WHERE  (A.FNOrderType = 22) "
            _Qry &= vbCrLf & " AND  A.FNHSysStyleId =" & StyleId & ""
            _Qry &= vbCrLf & " GROUP BY B.FTColorway, B.FTSizeBreakDown"
            _Qry &= vbCrLf & " ) AS BB ON M.FTColorway = BB.FTColorway AND M.FTSizeBreakDown=BB.FTSizeBreakDown"
            _Qry &= vbCrLf & "  WHERE M.FTOrderNo='" & HI.UL.ULF.rpQuoted(_DmP) & "'"
            _Qry &= vbCrLf & "  AND M.FTSubOrderNo='" & HI.UL.ULF.rpQuoted(_DmPSub) & "'"

            HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            Return "FO. No. " & _DmP & "  SUB FO. No. " & _DmPSub

        Catch ex As Exception

            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            Return ""

        End Try

        Return "FO. No. " & _DmP & "  SUB FO. No. " & _DmPSub
    End Function
    Private Function W_PRCbImportFactoryOrder(_Spls) As Boolean
        '...last modify 2014/12/19 when drop field Amount, Qty TMERTOrder_BreakDown, TMERTOrder, TMERTOrderSub

        Dim _bImportComplete As Boolean = False

        Dim tMsgSplash As String = ""

        Try
            Dim nFNHSysCustId As Integer
            Dim nFNHSysCmpRunId As Integer
            Dim nFNHSysBuyId As Integer
            Dim nFNHSysVenderPramId As Integer
            Dim _FNImportOrderType As Integer = 0
            Dim _FixCmpByProgram As Integer = 0

            _FNImportOrderType = 0



            If nFNHSysVenderPramId <= 0 Then Return False

            Dim oDBdtImport As System.Data.DataTable


            tSql = ""
            tSql &= Environment.NewLine & "     Select  A.FTPONo,A.FTOrderDate,A.FTStyle,A.FTSeason,ST.FNHSysStyleId ,SS.FNHSysSeasonId  "
            tSql &= Environment.NewLine & "    FROM(SELECT  FTPONo, "
            tSql &= Environment.NewLine & "     FTOrderDate "
            tSql &= Environment.NewLine & "   , FTStyle "
            tSql &= Environment.NewLine & "   , FTPlanningSeason+ RIGHT(FTYear,2) As FTSeason "
            tSql &= Environment.NewLine & "    From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTImportOrderTemp As A "
            tSql &= Environment.NewLine & "    Where (FTUserLogin =N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "') "
            tSql &= Environment.NewLine & "    Group By FTPONo, "
            tSql &= Environment.NewLine & "    FTOrderDate "
            tSql &= Environment.NewLine & "   	, FTStyle "
            tSql &= Environment.NewLine & "   , FTPlanningSeason+ RIGHT(FTYear,2) ) As A "
            tSql &= Environment.NewLine & "   INNER Join [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TMERMStyle As ST With(NOLOCK) On  A.FTStyle = ST.FTStyleCode "
            tSql &= Environment.NewLine & "   	INNER Join [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TMERMSeason As SS With(NOLOCK) On  A.FTSeason = SS.FTSeasonCode  "



            oDBdtImport = HI.Conn.SQLConn.GetDataTable(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN)

            If Not oDBdtImport Is Nothing AndAlso oDBdtImport.Rows.Count > 0 Then

                For Each R As DataRow In oDBdtImport.Rows

                    Call SaveQuantityByStyle(R!FTPONo.ToString, R!FTStyle.ToString, R!FTSeason.ToString, Val(R!FNHSysStyleId.ToString), Val(R!FNHSysSeasonId.ToString), _FNImportOrderType, _FixCmpByProgram, nFNHSysVenderPramId, nFNHSysCustId, nFNHSysCmpRunId, nFNHSysBuyId)

                Next

                _bImportComplete = True
            Else
                _bImportComplete = False
            End If

        Catch ex As Exception

        End Try

        Return _bImportComplete

    End Function

    ' http://social.msdn.microsoft.com/Forums/en-US/vblanguage/thread/4c376915-f0cd-4411-affb-e825c9b1c524/
    '
    ' Original functionality by Reed Kimble.
    ' The following modifications written by Rob Sherratt,
    '       1. "hasHeader" modifications to allow for header row in the csv files.
    '       2. Exception handling, prints exception stack to console.
    '       3. Resolves problem that some names in the header row may be duplicates.

    Private Function W_GETdtCSVToDataTable(ByVal filePathName As String, Optional ByVal hasHeader As Boolean = False) As DataTable

        REM If Not W_PRCbValidateFileIsOpen(filePathName) Then Return Nothing

        ' Parses a csv into a datatable.
        Try
            Dim oDBdt As New DataTable
            If System.IO.File.Exists(filePathName) Then
                Dim parser As New Microsoft.VisualBasic.FileIO.TextFieldParser(filePathName)
                parser.Delimiters = New String() {","}
                parser.HasFieldsEnclosedInQuotes = True 'use if data may contain delimiters 
                parser.TextFieldType = FileIO.FieldType.Delimited
                parser.TrimWhiteSpace = True

                Dim HeaderFlag As Boolean
                If hasHeader Then HeaderFlag = True

                While Not parser.EndOfData
                    If AddValuesToTable(parser.ReadFields, oDBdt, HeaderFlag) Then
                        HeaderFlag = False
                    Else
                        parser.Close()
                        Return Nothing
                    End If

                End While

                parser.Close()

                Return oDBdt

            Else : Return Nothing
            End If

        Catch ex As Exception
            Console.WriteLine(ex.ToString())
            Return Nothing
        End Try

    End Function

#End Region

    Private Sub FTFilePath_ButtonClick(sender As Object, e As XtraEditors.Controls.ButtonPressedEventArgs) Handles FTFilePath.ButtonClick

        If W_PRCbValidateProgrammeVendor() = True Then
            '...default when user click button select file format excel
            Select Case e.Button.Index
                Case 0
                    Try
                        Dim opFileDialog As New System.Windows.Forms.OpenFileDialog
                        REM 2014/04/29 opFileDialog.Filter = "Excel Worksheets (*.xls, *.xlsx)|*.xls;*.xlsx"
                        opFileDialog.Filter = "Excel Worksheets (*.xls, *.xlsx)|*.xls;*.xlsx|csv files (*.csv)|*.csv"
                        REM opFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)

                        Me.FTFilePath.Text = ""

                        Dim oDialogResult As DialogResult = opFileDialog.ShowDialog()

                        If oDialogResult = System.Windows.Forms.DialogResult.OK Then
                            Try
                                If opFileDialog.FileName <> "" Then
                                    Me.FTFilePath.Text = opFileDialog.FileName.ToString()
                                End If
                            Catch ex As Exception
                            End Try
                        End If

                    Catch ex As Exception
                        'If System.Diagnostics.Debugger.IsAttached = True Then
                        Throw New Exception(ex.Message().ToString() & Environment.NewLine & ex.StackTrace.ToString())
                        'End If
                    End Try

                Case Else
                    '...do nothing
            End Select

            Call Me.ocmReadExcel.PerformClick()

        End If

    End Sub

    Private Sub wImportOrder_Activated(sender As Object, e As EventArgs) Handles Me.Activated

    End Sub

    Private Sub wImportOrder_Load(sender As Object, e As EventArgs) Handles Me.Load
        Timer1.Enabled = True


        ogvImportOrder.OptionsView.ShowAutoFilterRow = False




    End Sub


    Private Sub ocmReadExcel_Click(sender As Object, e As EventArgs) Handles ocmReadExcel.Click
        Try

            Dim _CheckOrderQtyZero As Boolean = False

            Dim oDBdtExcel As New System.Data.DataTable
            If Me.FTFilePath.Text.Trim() <> "" Then

                If IO.File.Exists(Me.FTFilePath.Text.Trim().ToString()) Then

                    _bCatchSrcFile = False

                    Dim _FilePath As String
                    _FilePath = Me.FTFilePath.Text.Trim()

                    Dim tFileExtension As String

                    tFileExtension = IO.Path.GetExtension(_FilePath)

                    '=====================================================================================================================================================================================================================
                    'DEVEXPRESS SPREAD SHEET XXX Modify 2015/01/31
                    '------------------------------------------------------------------------------------------------------------------------
                    Dim oSpreadSheetCtrl As DevExpress.XtraSpreadsheet.SpreadsheetControl = New DevExpress.XtraSpreadsheet.SpreadsheetControl()
                    Dim oWorkSheetItem As DevExpress.Spreadsheet.WorksheetCollection = Nothing


                    '=====================================================================================================================================================================================================================
                    ogvImportOrder.Columns.Clear()

                    Select Case HI.ST.Lang.Language

                        Case HI.ST.Lang.eLang.TH
                            _tSplashText = "กรุณารอสักครู่.....กำลังดำเนินการอ่านข้อมูล"
                        Case Else
                            _tSplashText = "Read data.....please wait"
                    End Select

                    If System.Diagnostics.Debugger.IsAttached = True Then
                        'Me.ogbmainprocbutton.Enabled = False
                        Me.Cursor = Cursors.WaitCursor

                        Me.ogbBrowseFile.Enabled = False
                        Me.ogbViewDetail.Enabled = False
                    End If

                    _oSplash = New HI.TL.SplashScreen(_tSplashText, "", True)

                    Application.DoEvents()

                    Dim _FoundPrm As Boolean = False '...กำหนดโครงการ Program HIT/HIG/HIP...

                    _oSplash.UpdateInformation(_tSplashText & Environment.NewLine & "Read Data From Excel File.......")


                    oDBdtExcel = HI.UL.ReadExcel.Read(_FilePath, "Sheet1", 0)

                    '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                    'Overall Result'

                    oSpreadSheetCtrl.Document.Dispose()
                    Me.ogdImportOrder.DataSource = Nothing
                    If oDBdtExcel.Rows.Count <= 0 Then

                        Exit Sub
                    End If

                    tSql = ""
                    tSql = "DELETE A"
                    tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTImportSalesmanCustomerPOTemp] AS A"
                    tSql &= Environment.NewLine & "WHERE A.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "';"

                    If HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN) = True Then
                    End If

                    Dim datapotable As DataTable = oDBdtExcel.Copy

                    datapotable.BeginInit()
                    datapotable.Rows.RemoveAt(0)
                    datapotable.Rows.RemoveAt(1)
                    datapotable.Rows.RemoveAt(0)
                    datapotable.EndInit()
                    Dim RowCount As Integer = 0
                    Dim Colorway As String = ""
                    For Each R As DataRow In datapotable.Rows

                        If R!F4.ToString.Trim() <> "" And R!F5.ToString.Trim() <> "" Then

                            Colorway = Microsoft.VisualBasic.Right("000" + R!F7.ToString.Trim(), 3)

                            tSql = " UPDATE  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTImportSalesmanCustomerPOTemp] "
                            tSql &= Environment.NewLine & " SET FTPONo='" & HI.UL.ULF.rpQuoted(R!F5.ToString.Trim()) & "',FTPONo='" & HI.UL.ULF.rpQuoted(R!F8.ToString.Trim()) & "'"
                            tSql &= Environment.NewLine & " WHERE  FTUserLogIn =N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                            tSql &= Environment.NewLine & " AND FTSubOrderNo='" & HI.UL.ULF.rpQuoted(R!F4.ToString.Trim()) & "'"
                            tSql &= Environment.NewLine & " AND FTColorway='" & HI.UL.ULF.rpQuoted(Colorway) & "'"

                            If HI.Conn.SQLConn.ExecuteNonQuery(tSql, Conn.DB.DataBaseName.DB_MERCHAN) = True Then
                                RowCount = RowCount + 1
                            Else
                                tSql = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTImportSalesmanCustomerPOTemp](FTUserLogIn,FTSubOrderNo,FTPONo,FTStyleNo,FTColorway,FTPOLine)"
                                tSql &= Environment.NewLine & " SELECT  N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                                tSql &= Environment.NewLine & ",'" & HI.UL.ULF.rpQuoted(R!F4.ToString.Trim()) & "'"
                                tSql &= Environment.NewLine & ",'" & HI.UL.ULF.rpQuoted(R!F5.ToString.Trim()) & "'"
                                tSql &= Environment.NewLine & ",'" & HI.UL.ULF.rpQuoted(R!F6.ToString.Trim()) & "'"
                                tSql &= Environment.NewLine & ",'" & HI.UL.ULF.rpQuoted(Colorway) & "'"
                                tSql &= Environment.NewLine & ",'" & HI.UL.ULF.rpQuoted(R!F8.ToString.Trim()) & "'"

                                If HI.Conn.SQLConn.ExecuteNonQuery(tSql, Conn.DB.DataBaseName.DB_MERCHAN) = True Then
                                    RowCount = RowCount + 1
                                End If
                            End If



                        End If

                    Next

                    datapotable.Dispose()

                    If RowCount <= 0 Then
                        oDBdtExcel.Dispose()
                        Exit Sub
                    End If

                    oSpreadSheetCtrl.LoadDocument(_FilePath)
                    _FoundPrm = True
                    _bCatchSrcFile = True



                    Me.ogdImportOrder.DataSource = oDBdtExcel
                    Me.ogdImportOrder.Refresh()
                    Me.ogvImportOrder.RefreshData()
                    Me.ogvImportOrder.OptionsView.ColumnAutoWidth = False
                    Me.ogvImportOrder.OptionsBehavior.AllowAddRows = DefaultBoolean.False


                    For Each GridCol As DevExpress.XtraGrid.Columns.GridColumn In ogvImportOrder.Columns
                        GridCol.OptionsColumn.AllowEdit = False
                        GridCol.OptionsColumn.ReadOnly = True
                        GridCol.OptionsColumn.AllowSort = DefaultBoolean.False

                    Next

                Else
                    Me.ogdImportOrder.DataSource = Nothing
                            Me.ogdImportOrder.Refresh()
                            Me.ogvImportOrder.RefreshData()
                        End If



                        _oSplash.Close()

                If System.Diagnostics.Debugger.IsAttached = True Then
                    'Me.ogbmainprocbutton.Enabled = True
                    Me.Cursor = Cursors.Default

                    Me.ogbBrowseFile.Enabled = True
                    Me.ogbViewDetail.Enabled = True
                End If



            Else
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.FTFilePath_lbl.Text)
                Me.ogdImportOrder.DataSource = Nothing
                Me.ogvImportOrder.RefreshData()
                Me.FTFilePath.Focus()
            End If

        Catch ex As Exception

            _oSplash.Close()

            If System.Diagnostics.Debugger.IsAttached = True Then
                'Me.ogbmainprocbutton.Enabled = True
                Me.Cursor = Cursors.Default

                Me.ogbBrowseFile.Enabled = True
                Me.ogbViewDetail.Enabled = True
            End If

            If System.Diagnostics.Debugger.IsAttached = True Then
                MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString())
            End If

        End Try

    End Sub

    Private Sub ocmclearclsr_Click(sender As Object, e As EventArgs) Handles ocmclearclsr.Click
        Call W_PRCbClsr()
        'Dim nFNCntImport As Integer = PRCnUpdateImportTime(1)
        'MsgBox(String.Format("Import time's : {0}", nFNCntImport), MsgBoxStyle.Information + MsgBoxStyle.OkOnly, My.Application.Info.Title)
    End Sub

    Private Sub ocmExit_Click(sender As Object, e As EventArgs) Handles ocmExit.Click
        Me.Close()
    End Sub

    Private Sub ocmImportOrder_Click(sender As Object, e As EventArgs) Handles ocmImportOrder.Click

        Try
            If Me.ogvImportOrder.RowCount > 0 Then

                If Not _bCatchSrcFile Then

                    Select Case HI.ST.Lang.Language

                        Case HI.ST.Lang.eLang.TH
                            MsgBox("การกำหนดข้อมูลทีมเมอร์แชนไดเซอร์บางรายการของไฟล์ต้นฉบับไม่ถูกต้อง " & Environment.NewLine & "หรือยังไม่มีการกำหนดข้อมูลหลักรายการทีมเมอร์แชนไดเซอร์ !!!" & Environment.NewLine & "กรุณาทำการตรวจสอบอีกครั้ง", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
                        Case Else
                            MsgBox("Invalid Merchandiser Team " & Environment.NewLine & "Or Not provide for merchandiser team master file !!!", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
                    End Select

                    Exit Sub

                End If

                If W_PRCbValidateConfirmGenerateFactoryOrder() = True Then

                    If HI.MG.ShowMsg.mConfirmProcess("คุณต้องการทำการ Import ข้อมูล Customer PO Order Saleman ใช่หรือไม่ ?", 1404247882, "Confirm") = True Then



                        Dim _Spls As New HI.TL.SplashScreen("Update Data Customer PO of Order Sale Man .....Please Wait")


                        Dim Cmdstring As String = ""
                        Dim dtstss As DataTable

                        Cmdstring = " SELECT DISTINCT A.FTOrderNo  "
                        Cmdstring &= Environment.NewLine & "    From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub As A INNER Join "
                        Cmdstring &= Environment.NewLine & "         [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTImportSalesmanCustomerPOTemp As B On A.FTSubOrderNo = B.FTSubOrderNo "
                        Cmdstring &= Environment.NewLine & "  Where(B.FTUserLogIn =  N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "') "

                        dtstss = HI.Conn.SQLConn.GetDataTable(Cmdstring, Conn.DB.DataBaseName.DB_MERCHAN)

                        For Each R As DataRow In dtstss.Rows

                            Cmdstring = "EXEC [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.USP_GEN_BREAKDOWN_SHIPDESINATION '" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString()) & "'"
                            HI.Conn.SQLConn.ExecuteOnly(Cmdstring, Conn.DB.DataBaseName.DB_MERCHAN)

                        Next

                        tSql = ""
                        tSql &= Environment.NewLine & " UPDATE A SET A.FTPORef = B.FTPONo "
                        tSql &= Environment.NewLine & "  ,A.FDUpdDate = " & HI.UL.ULDate.FormatDateDB & ",A.FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        tSql &= Environment.NewLine & "    From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub As A INNER Join "
                        tSql &= Environment.NewLine & "         [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTImportSalesmanCustomerPOTemp As B On A.FTSubOrderNo = B.FTSubOrderNo "
                        tSql &= Environment.NewLine & "  Where(B.FTUserLogIn =  N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "') "

                        tSql &= Environment.NewLine & " UPDATE A SET A.FTNikePOLineItem = B.FTPOLine "
                        tSql &= Environment.NewLine & "  ,A.FDUpdDate = " & HI.UL.ULDate.FormatDateDB & ",A.FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        tSql &= Environment.NewLine & "    From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub_BreakDown As A INNER Join "
                        tSql &= Environment.NewLine & "         [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTImportSalesmanCustomerPOTemp As B On A.FTSubOrderNo = B.FTSubOrderNo AND A.FTColorway = B.FTColorway "
                        tSql &= Environment.NewLine & "  Where(B.FTUserLogIn =  N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "') "


                        tSql &= Environment.NewLine & " DELETE A"
                        tSql &= Environment.NewLine & " FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTImportSalesmanCustomerPOTemp] As A"
                        tSql &= Environment.NewLine & " WHERE A.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "';"

                        If HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN) = True Then
                        End If

                        'Else
                        '    '...developer test temp data
                        'End If
                        '---------------------------------------------------------------------------------------------------------------------------------------------------------

                        _bCatchSrcFile = False

                        Me.ogdImportOrder.DataSource = Nothing
                        Me.ogdImportOrder.Refresh()

                        _Spls.Close()
                        Select Case HI.ST.Lang.Language
                            Case HI.ST.Lang.eLang.TH
                                HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, "นำเข้าข้อมูล Data Customer PO of Order Sale Man Complete ...")

                            Case Else
                                HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, "Import Data Customer PO of Order Sale Man  Complete...")
                        End Select

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

    Private Sub ogvImportOrder_RowCellStyle(sender As Object, e As RowCellStyleEventArgs) Handles ogvImportOrder.RowCellStyle
        Try
            If e.RowHandle > -1 And e.RowHandle < 3 Then
                e.Appearance.BackColor = Color.YellowGreen
                e.Appearance.ForeColor = Color.DarkBlue
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString())
        End Try
    End Sub

    Private Sub FNHSysBuyId_KeyPress(sender As Object, e As KeyPressEventArgs)
        Try
            If Asc(e.KeyChar) = 13 Then
                Me.FTFilePath.Focus()
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Try

            'Dim span As TimeSpan = DateTime.Now.Subtract(startTime)

            If Not (_oSplash Is Nothing) And (_oSplash.IsDisposed = False) Then
                _oSplash.Updatetime(Format(Now, "HH:mm:ss"))
                ';  _oSplash = New HI.TL.SplashScreen(_tSplashText)
            End If

            ''_oSplash.UpdateInformation(_tSplashText & " " & String.Format("{0:00}", span.Minutes.ToString) & ":" & String.Format("{0:00}", span.Seconds.ToString) & ":" & String.Format("{0:00}", span.Milliseconds.ToString))

            'Dim TotalSeconds As System.Int64
            'TotalSeconds = TimeSpan.FromMilliseconds(stopWatch.ElapsedMilliseconds).TotalSeconds
            'Seconds = TotalSeconds Mod 60
            'If (TotalSeconds >= 60) Then
            '    Minutes = (TotalSeconds / 60) Mod 60
            'End If
            'If (TotalSeconds >= 60 * 60) Then
            '    Hours = (TotalSeconds / (60 * 60) Mod 24)
            'End If

            'If (TotalSeconds >= 24 * 60 * 60) Then
            '    days = (TotalSeconds / (24 * 60 * 60) Mod 365)
            'End If

            ''Label1.Text = (" days " & days & " Hours " & Hours & " Minutes " & Minutes & " Seconds " & Seconds)

            '_oSplash.UpdateInformation(_tSplashText & " " & " days " & days & " Hours " & Hours & " Minutes " & Minutes & " Seconds " & Seconds)

            REM Label1.Text = Now.TimeOfDay.ToString()

        Catch ex As Exception
        End Try

    End Sub

    Private Sub FTFilePath_EditValueChanged(sender As Object, e As EventArgs) Handles FTFilePath.EditValueChanged

    End Sub



End Class